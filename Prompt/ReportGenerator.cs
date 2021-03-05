using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PDFjet.NET;
using System.Collections;
using SIMCORE_TOOL.Classes;
using System.IO;

namespace SIMCORE_TOOL.Prompt
{
    public partial class ReportGenerator : Form
    {
        private Dictionary<string, ReportParameters> ReportParametersList;
        private GestionDonneesHUB2SIM Donnees;
        private TreeNodeCollection Collection;
        private ImageList imgList;
        private System.Drawing.Font nodeFontBold;
        private List<String[]> versionList;
        private int[] versionColumnWidth = new int[5] {70, 100, 200, 100, 130};
        private List<String[]> approvalList;
        private int[] approvalColumnWidth = new int[5] { 100, 150, 150, 110, 100 };
        String[] docControlElt = new String[7] { "", "", "", "", "", "", "" };
        //private const String defaultTemplateString = "[Default]";
        private bool saved = false;
        /// <summary>
        /// Liste des tables qui doivent être redimensionnées.
        /// Les tables sont referencées par une chaîne de caractere qui est 
        /// la concatenation du nom de la table et du nom du scenario.
        /// la table est mise à jour par l'utilisateur dans la fenetre TreeEditor.
        /// </summary>
        private Dictionary<String, ReportParameters.ColumnSizePolicy> lsResizeTable;

        /// <summary>
        /// Index unique pour permettre la génération du preview dans un nouveau dossier temporaire pour PAX2SIM (lié à un 
        /// problème d'accès à un fichier PDF où Adobe reader est resté ouvert)
        /// </summary>
        private static int iPreviewTmp = 1;

        private ContextMenuStrip reportsNodeContextMenu;    // >> Task #13384 Report Tree-view

        #region Constructeurs et fonctions pour initialiser le formulaire.
        private void InitializeForm()
        {
            InitializeComponent();
            tv_Reports.ImageList = imgList;
            OverallTools.FonctionUtiles.MajBackground(this);
            // old version (before tree editor 11/07/2011)
            //int i;
            //for (i = 1; i < Collection.Count; i++) // collection[1] is the airport structure
            //{
            //    TreeNode tnTemp = ((TreeNode)Collection[i]);
            //    TreeNode tnNewNode = OverallTools.TreeViewFunctions.createBranch(tnTemp.Name, tnTemp.Text, tnTemp.Tag, null);
            //    tv_Reports.Nodes.Add(tnNewNode);
            //    OverallTools.TreeViewFunctions.copyChilds(tnNewNode, tnTemp, true, Donnees, true);
            //}
            // Initialize pdf page format comboBox
            PageFormatBox.DataSource = ReportParameters.PageFormatsNames;
            PageFormatBox.SelectedItem = "A4 Portrait"; // default value
            cb_dividerLevel.SelectedIndex = 0; // default value
            // initialize report list
            LoadReportComboBox();
            ParamChange(null, null);
        }

        public ReportGenerator(TreeNodeCollection Collection, ImageList imgList,
            GestionDonneesHUB2SIM Donnees, ContextMenuStrip _reportsNodeContextMenu)
        {
            this.Donnees = Donnees;
            this.Collection = Collection;
            this.imgList = imgList;
            if (Donnees.htReportParametres == null)
                Donnees.htReportParametres = new Dictionary<string, ReportParameters>(); // create a parameters list if doesn't exist
            ReportParametersList = Donnees.htReportParametres;
            InitializeForm();
            reportsNodeContextMenu = _reportsNodeContextMenu;
        }

        public ReportGenerator(TreeNodeCollection Collection, ImageList imgList,
            GestionDonneesHUB2SIM Donnees, ContextMenuStrip _reportsNodeContextMenu,    // >> Task #13384 Report Tree-view
            string _loadedReportName)
        {            
            this.Donnees = Donnees;
            this.Collection = Collection;
            this.imgList = imgList;
            if (Donnees.htReportParametres == null)
                Donnees.htReportParametres = new Dictionary<string, ReportParameters>();
            ReportParametersList = Donnees.htReportParametres;
            InitializeForm();
            reportsNodeContextMenu = _reportsNodeContextMenu;
            
            // >> Task #17969 PAX2SIM - Reports improvements            
            selectLoadedReport(_loadedReportName);
            /*ReportNameComboBox.SelectedIndexChanged -= ReportNameComboBox_changed;
            selectLoadedReport(_loadedReportName);
            ReportNameComboBox.SelectedIndexChanged += ReportNameComboBox_changed;
            if (ReportParametersList.ContainsKey(ReportNameComboBox.Text))
                LoadParameters(ReportParametersList[ReportNameComboBox.Text] as ReportParameters);*/
            // << Task #17969 PAX2SIM - Reports improvements
        }

        private void selectLoadedReport(string loadedReportName)
        {
            if (ReportParametersList.ContainsKey(loadedReportName)
                && ReportNameComboBox.Items.Contains(loadedReportName))
            {
                ReportNameComboBox.SelectedItem = loadedReportName;
            }
        }

        /// <summary>
        /// Removed from the tree all the elements that no longer exist.
        /// </summary>
        /// <param name="nodes">Nodes to check</param>
        private void RemovedDeletedElement (TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                TreeViewTag tag = node.Tag as TreeViewTag;

                /// Remove missing ParagraphNode
                if (tag.isParagraphNode)
                    if ((Donnees.getParagraph(node.Name) == null) &&
                        (Donnees.getNote(tag.ScenarioName, node.Name) == null) &&
                        (Donnees.getExNote(tag.ScenarioName, tag.Name, tag.ExceptionName) == null) &&
                        (Donnees.getGanttNote(tag.ScenarioName, tag.Name) == null) &&     //>>GanttNote for Report
                        (Donnees.getDashboardNote(tag.ScenarioName, tag.Name) == null)     // >> Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports
                        )
                    {
                        node.Remove();
                    }

                /// Check the child nodes
                RemovedDeletedElement(node.Nodes);
            }
        }
        #endregion

        #region parameters accessor       
        public TreeNodeCollection TreeView
        {
            get { return tv_Reports.Nodes; }
        }
        #endregion parameters accessor

        private void tv_Reports_AfterCheck(object sender, TreeViewEventArgs e)
        {
            saved = false;
            bool Checked = e.Node.Checked;
            TreeNode parent = e.Node.Parent;

            // check exists font
            if (nodeFontBold == null)
                nodeFontBold = new System.Drawing.Font(e.Node.TreeView.Font, System.Drawing.FontStyle.Bold);

            if (Checked) // node was just checked
            {
                while (parent != null)
                {
                    parent.NodeFont = nodeFontBold; // si le noeud a des enfants selectionnés alors il est mit en gras
                    parent = parent.Parent;
                }
            }
            else // node was just unchecked
            {
                while (parent != null)
                {
                    bool hasCheckedDescendants = false;
                    foreach (TreeNode child in parent.Nodes)
                        if (child.Checked)
                        {
                            hasCheckedDescendants = true; // if one child is checked the parent node has checked descendant
                            break;
                        }
                        else
                            if (child.NodeFont != null)
                                if (child.NodeFont.Bold)
                                {
                                    hasCheckedDescendants = true; // if one child is bolded the parent node has checked descendant
                                    break;
                                }
                    if (!hasCheckedDescendants)
                    {
                        parent.NodeFont = parent.TreeView.Font; // if the node hasn't child node ckecked, then unbold it
                    }
                    parent = parent.Parent;
                }
            }
        }

        public ReportParameters parameters
        {
            get { return SaveParameters(); }
        }

        /// <summary>
        /// Function to check the treeNode elements from checkedTreeNode
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="checkedTreeNode"></param>
        private void RecheckTree(TreeNode treeNode, TreeNode checkedTreeNode)
        {
            treeNode.Checked = checkedTreeNode.Checked;
            foreach (TreeNode node in checkedTreeNode.Nodes)
            {
                if (treeNode.Nodes[node.Name] != null)
                    RecheckTree(treeNode.Nodes[node.Name], node);
            }
        }
        /// <summary>
        /// Uncheck all TreeNode.
        /// </summary>
        /// <param name="tv_Reports">TreeView to affect.</param>
        private void CleanCheckBox(TreeView tv_Reports)
        {
            foreach (TreeNode node in tv_Reports.Nodes)
                CleanCheckBox(node);
        }
        /// <summary>
        /// Uncheck a TreeNode and all its child node.
        /// </summary>
        /// <param name="tv_Reports">TreeNode to affect.</param>
        private void CleanCheckBox(TreeNode node)
        {
            node.Checked = false;
            foreach (TreeNode child in node.Nodes)
                CleanCheckBox(child);
        }
        /// <summary>
        /// Load parameters from a ReportParameters if the combo box contain an 
        /// existing report name.
        /// </summary>
        private void ReportNameComboBox_changed(object sender, EventArgs e)
        {
            /*if (!saved) // >> Task #17969 PAX2SIM - Reports improvements
            {
                DialogResult result = MessageBox.Show("Report configuration was not saved. Do you want to save ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SaveButton_Click(sender, e);
                    saved = true;
                }
                if (result == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.None;
                }
            }*/
            //checkSavedStatus();
            // << Task #17969 PAX2SIM - Reports improvements

            if (ReportParametersList.ContainsKey(ReportNameComboBox.Text))
                LoadParameters(ReportParametersList[ReportNameComboBox.Text] as ReportParameters);
        }
        /// <summary>
        /// Save paramters in the ReportParameters (create a new one in case).
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Check entry
            CheckFormError();

            // check if report name is fulfilled before saving
            if (ReportNameComboBox.Text.Equals(""))
            {
                MessageBox.Show("Report name is empty. This configuration will not be saved !", "Impossible to save", MessageBoxButtons.OK);
                return;
            }

            ReportParameters reportParam = SaveParameters();

            // if the report exist -> change it, else -> add a new one 
            if (ReportParametersList.ContainsKey(ReportNameComboBox.Text))
                ReportParametersList[ReportNameComboBox.Text] = reportParam;
            else
            {
                ReportParametersList.Add(ReportNameComboBox.Text, reportParam);
                LoadReportComboBox(); // to make the new report appears in report box
                ReportNameComboBox.SelectedItem = reportParam.Name;
            }
            saved = true;
        }
        /// <summary>
        /// Save the report parameters.
        /// </summary>
        /// <returns>ReportParameters containing all the saved parameters</returns>
        private ReportParameters SaveParameters()
        {
            ReportParameters reportParam = new ReportParameters();

            /// Save the text box parameters.
            reportParam.GenerateHTML = generateHTMLCheckBox.Checked;
            reportParam.GeneratePDF = generatePDFCheckBox.Checked;
            reportParam.IncludeOnlyChecked = !purgedTreeCheckBox.Checked;
            // >> Task #17969 PAX2SIM - Reports improvements
            reportParam.exportSourceTextFiles = exportSourceTextFilesCheckBox.Checked;
            reportParam.exportXmlConfiguration = exportXmlConfigurationCheckBox.Checked;           
            // << Task #17969 PAX2SIM - Reports improvements
            reportParam.ExportNote = exportNoteCheckBox.Checked;
            reportParam.PageFormat = PageFormatBox.SelectedIndex;
            reportParam.Name = ReportNameComboBox.Text;
            reportParam.Author = AutorTextBox.Text;
            if (!cb_useTemplate.Checked)
                reportParam.Template = null;
            else if (tb_template.Text == null ||
                tb_template.Text.Equals(ReportParameters.defaultTemplateString) || 
                tb_template.Text == "" )
                reportParam.Template = ReportParameters.defaultTemplateString;
            else
                reportParam.Template = tb_template.Text;
            reportParam.UserLogo = tb_logo.Text;
            reportParam.Titre = titleTextBox.Text;
            reportParam.Version = versionTextBox.Text;
            reportParam.Date = dateTextBox.Text;
            // Bug Divider level
            int dividerLevel = 0;
            if (cb_dividerLevel.SelectedItem != null
                && Int32.TryParse(cb_dividerLevel.SelectedItem.ToString(), out dividerLevel))
            {
                reportParam.DividerLevel = dividerLevel;
            }
            reportParam.DividerLevel = dividerLevel;
            // Bug Divider level
            

            /// Adjustement mode for table
            reportParam.dResizeTable = lsResizeTable;

            /// Save "version & doc control" parameters.
            reportParam.VersionList = versionList;
            reportParam.VersionColumnWidth = versionColumnWidth;
            reportParam.ApprovalList = approvalList;
            reportParam.ApprovalColumnWidth = approvalColumnWidth;
            reportParam.ProjStep = docControlElt[0] == "" ? null : docControlElt[0];
            reportParam.PartOf = docControlElt[1] == "" ? null : docControlElt[1];
            reportParam.IssuedDept = docControlElt[2] == "" ? null : docControlElt[2];
            reportParam.IntDocRef = docControlElt[3] == "" ? null : docControlElt[3];
            reportParam.ExtDocRef = docControlElt[4] == "" ? null : docControlElt[4];
            reportParam.DocStatus = docControlElt[5] == "" ? null : docControlElt[5];
            reportParam.Appendices = docControlElt[6] == "" ? null : docControlElt[6];

            reportParam.tv_CustomTree = new TreeView();
            reportParam.tv_CustomTree.ImageList = tv_Reports.ImageList;
            for (int i = 0; i < tv_Reports.Nodes.Count; i++)
            {
                TreeNode tnTemp = ((TreeNode)tv_Reports.Nodes[i]);
                TreeNode tnNewNode = OverallTools.TreeViewFunctions.createBranch(tnTemp.Name, tnTemp.Text, tnTemp.Tag, null, false, true);
                tnNewNode.Checked = tnTemp.Checked;
                reportParam.tv_CustomTree.Nodes.Add(tnNewNode);
                OverallTools.TreeViewFunctions.copyChilds(tnNewNode, tnTemp, false, Donnees, true, true);
            }

            if (lsResizeTable != null && lsResizeTable.Count > 0)
                reportParam.dResizeTable = lsResizeTable;

            reportParam.reportGroupsList = tempCurrentReportGroups;

            return reportParam;
        }

        private List<string> tempCurrentReportGroups = new List<string>();  // >> Task #17969 PAX2SIM - Reports improvements
        /// <summary>
        /// Set the window form check box and text field from a ReportParameters.
        /// </summary>
        /// <param name="newParameters"></param>
        private void LoadParameters(ReportParameters newParameters)
        {
            exportNoteCheckBox.Checked = newParameters.ExportNote;
            generateHTMLCheckBox.Checked = newParameters.GenerateHTML;
            generatePDFCheckBox.Checked = newParameters.GeneratePDF;
            purgedTreeCheckBox.Checked = !newParameters.IncludeOnlyChecked;
            // >> Task #17969 PAX2SIM - Reports improvements
            exportSourceTextFilesCheckBox.Checked = newParameters.exportSourceTextFiles;
            exportXmlConfigurationCheckBox.Checked = newParameters.exportXmlConfiguration;
            
            // << Task #17969 PAX2SIM - Reports improvements
            PageFormatBox.SelectedIndex = newParameters.PageFormat;
            tb_template.Text = "";
            cb_useTemplate.Checked = false;
            if (newParameters.Template != null) // no template
            {
                cb_useTemplate.Checked = true;
                if (newParameters.Template == ReportParameters.defaultTemplateString) // default template
                    tb_template.Text = ReportParameters.defaultTemplateString;
                else if (System.IO.File.Exists(newParameters.Template)) // custom template
                    tb_template.Text = newParameters.Template;
            }
            if (System.IO.File.Exists(newParameters.UserLogo))
                tb_logo.Text = newParameters.UserLogo;
            else
                tb_template.Text = "";
            AutorTextBox.Text = newParameters.Author;
            titleTextBox.Text = newParameters.Titre;
            versionTextBox.Text = newParameters.Version;
            dateTextBox.Text = newParameters.Date;
            cb_dividerLevel.SelectedIndex = newParameters.DividerLevel;// + 1;  // Bug Divider level
            
            // load "version & doc control" parameters
            versionList = newParameters.VersionList;
            approvalList = newParameters.ApprovalList;
            if (newParameters.VersionColumnWidth != null)
                versionColumnWidth = newParameters.VersionColumnWidth;
            if (newParameters.ApprovalColumnWidth != null)
                approvalColumnWidth = newParameters.ApprovalColumnWidth;
            docControlElt[0] = newParameters.ProjStep;
            docControlElt[1] = newParameters.PartOf;
            docControlElt[2] = newParameters.IssuedDept;
            docControlElt[3] = newParameters.IntDocRef;
            docControlElt[4] = newParameters.ExtDocRef;
            docControlElt[5] = newParameters.DocStatus;
            docControlElt[6] = newParameters.Appendices;

            // Recheck tree elements according to the new parameters
            lsResizeTable = newParameters.dResizeTable;

            if (newParameters.tv_CustomTree != null) // Load the custom tree
            {
                tv_Reports.Nodes.Clear();
                for (int i = 0; i < newParameters.tv_CustomTree.Nodes.Count; i++)
                {
                    TreeNode tnTemp = ((TreeNode)newParameters.tv_CustomTree.Nodes[i]);
                    TreeNode tnNewNode = OverallTools.TreeViewFunctions.createBranch(tnTemp.Name, tnTemp.Text, tnTemp.Tag, null);
                    tv_Reports.Nodes.Add(tnNewNode);
                    OverallTools.TreeViewFunctions.copyChilds(tnNewNode, tnTemp, false, Donnees, true, true);
                }
                RemovedDeletedElement(tv_Reports.Nodes);
            }
            tempCurrentReportGroups = newParameters.reportGroupsList;
            ParamChange(null, null);
            saved = true;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete the configuration " + ReportNameComboBox.Text + ". This action can't be canceled", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                ReportParametersList.Remove(ReportNameComboBox.Text); // remove parameters from parameters list
                LoadReportComboBox(); // reload report list
            }
        }

        /// <summary>
        /// Reload the report name box
        /// </summary>
        private void LoadReportComboBox()
        {
            const String DefaultName = "New report ";

            ReportNameComboBox.Items.Clear();
            if (ReportParametersList.Count != 0)
                foreach(string sName in ReportParametersList.Keys)
                    ReportNameComboBox.Items.Add(sName);

            // put default report name
            int n = 1;
            string sNom = DefaultName + n.ToString();
            while (ReportParametersList.ContainsKey(sNom))
                sNom = DefaultName + (n++).ToString();
            ReportNameComboBox.Text = sNom;
            generateHTMLCheckBox.Checked = false;
            //generateHTMLCheckBox.Enabled = false; // on peut plus faire de html pour le moment
            generatePDFCheckBox.Checked = true; // generate pdf by default
            cb_useTemplate.Checked = true;

            // TODO uncheck all tree
            foreach (TreeNode node in tv_Reports.Nodes)
                checkAllChild(false, node);
        }

        /// <summary>
        /// Check if one of the form entry is in a bad state. In case show an error message 
        /// and return false.
        /// </summary>
        private bool CheckFormError()
        {
            bool err = false;
            String errMessage = "";
            String errWindowName = "Bad field information";

            // check if pdf page format
            if (generatePDFCheckBox.Checked && PageFormatBox.SelectedIndex == -1)
            {
                err = true;
                errMessage += "The PDF page format is missing\n";
            }
            // check custom logo image doen't exist
            if (!tb_logo.Text.Equals("") && !System.IO.File.Exists(tb_logo.Text))
            {
                err = true;
                errMessage += "Unable to find Logo file.\n";
            }
            // check template image doen't exist
            if (!System.IO.File.Exists(tb_template.Text))
                if (lb_template.Text.Equals(ReportParameters.defaultTemplateString))
                {
                    err = true;
                    errMessage += "Unable to find template file.\n";
                }
            if (ReportNameComboBox.Text.Split(System.IO.Path.GetInvalidFileNameChars()).Length > 1)
            {
                err = true;
                errMessage += "The report name contains invalid caracters.\n";
                //foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                //    errMessage += " " + c + " ";
                //errMessage += " )";
            }

            //display error
            if (err)
                MessageBox.Show(errMessage, errWindowName, MessageBoxButtons.OK);
            return err;
        }

        /// <summary>
        /// Launch a window to select an image file for template.
        /// </summary>
        private void btn_browse1_Click(object sender, EventArgs e)
        {
            tb_template.Text = Browse();
            ParamChange(sender, e);
        }

        /// <summary>
        /// Launch a window to select an image file for client logo.
        /// </summary>
        private void btn_browse2_Click(object sender, EventArgs e)
        {
            tb_logo.Text = Browse();
            ParamChange(sender, e);
        }

        /// <summary>
        /// Launch the file browser with image filter (bmp, jpg, png).
        /// </summary>
        /// <returns>Selected image path</returns>
        private String Browse()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "Image files (*.jpg,*.png,*.bmp)|All files (*.*)";
            ofd.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.jpeg;*.png|All Files (*.*)|*.*";
            ofd.ShowDialog();
            return ofd.FileName;
        }

        #region Right clic on TreeView
        private void tv_Reports_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.Clicks == 1) // If simple right click
            {
                tv_Reports.SelectedNode = tv_Reports.GetNodeAt(e.Location);
            }
        }

        private void checkAllChildNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tv_Reports.SelectedNode != null)
            {
                tv_Reports.SelectedNode.Checked = true;
                checkAllChild(true, tv_Reports.SelectedNode);
            }
        }

        private void unckeckAllChildNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tv_Reports.SelectedNode != null)
            {
                tv_Reports.SelectedNode.Checked = false;
                checkAllChild(false, tv_Reports.SelectedNode);
            }
        }

        private void invertCheckedNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tv_Reports.SelectedNode != null)
            {
                tv_Reports.SelectedNode.Checked = !tv_Reports.SelectedNode.Checked;
                reverseCheckAllChild(tv_Reports.SelectedNode);
            }
        }

        /// <summary>
        /// Change the Check state of all the "node" children to the specified value.
        /// </summary>
        private void checkAllChild(bool value, TreeNode node)
        {
            foreach (TreeNode childs in node.Nodes)
            {
                childs.Checked = value;
                checkAllChild(value, childs);
            }
        }
        /// <summary>
        /// Change the Check state of all the "node" children.
        /// </summary>
        private void reverseCheckAllChild(TreeNode node)
        {
            foreach (TreeNode childs in node.Nodes)
            {
                childs.Checked = !childs.Checked;
                reverseCheckAllChild(childs);
            }
        }
        #endregion Right clic on TreeView

        /// <summary>
        /// Function launched when a parameters change in order to remember to ask for 
        /// saving befor exit.
        /// </summary>
        private void ParamChange(object sender, EventArgs e)
        {
            saved = false;

            // Check PDF GroupBox
            pdfGroupBox.Enabled = generatePDFCheckBox.Checked;

            // Check Generate Button
            bool generatable = true;
            if (!generateHTMLCheckBox.Checked && !generatePDFCheckBox.Checked
                && !exportSourceTextFilesCheckBox.Checked && !exportXmlConfigurationCheckBox.Checked)   // >> Task #17969 PAX2SIM - Reports improvements
            {
                generatable = false;
            }
            if (tv_Reports.Nodes.Count < 1)
            {
                lbl_editTree.Visible = true;
                generatable = false;
            }
            else
                lbl_editTree.Visible = false;
            btn_Generate.Enabled = generatable;

            // Check Template parameters
            tb_template.Enabled = cb_useTemplate.Checked;
            bt_template.Enabled = cb_useTemplate.Checked;
            if (tb_template.Text == null || tb_template.Text == "")
                tb_template.Text = ReportParameters.defaultTemplateString;

            // Enable preview LinkLabel
            ll_htmlPreview.Enabled = generateHTMLCheckBox.Checked && generatable;
            ll_pdfPreview.Enabled = generatePDFCheckBox.Checked && generatable;
        }

        /// <summary>
        /// Open a window to edit the version overview table.
        /// </summary>
        private void bt_versionning_Click(object sender, EventArgs e)
        {
            if (versionList == null)
                versionList = new List<string[]>();
            if (approvalList == null)
                approvalList = new List<string[]>();
            SIM_doc_versionning vers = new SIM_doc_versionning(versionList, versionColumnWidth, approvalList, approvalColumnWidth, docControlElt);
            vers.ShowDialog();
            ParamChange(sender, e);
        }
        /// <summary>
        /// Open the /ref TreeEditor to manipulate the tree view content.
        /// </summary>
        private void EditTree ()
        {
            TreeEditor editor;

            string selectedReportName = "";     // >> Task #16728 PAX2SIM Improvements (Recurring) C#16
            if (ReportNameComboBox.Text != null)
                selectedReportName = ReportNameComboBox.Text;

            if (tv_Reports.Nodes.Count == 0)
                editor = new TreeEditor(selectedReportName,Collection, imgList, Donnees);
            else
                editor = new TreeEditor(selectedReportName, tv_Reports.Nodes, Collection, imgList, Donnees);
            if (lsResizeTable == null)
                lsResizeTable = new Dictionary<String, ReportParameters.ColumnSizePolicy>();
            editor.lsResizeTable = lsResizeTable;
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                tv_Reports.Nodes.Clear();
                tv_Reports.Nodes.AddRange(editor.TreeNodes);
                lsResizeTable = editor.lsResizeTable;
            }
            ParamChange(null, null);
            this.Focus();
        }

        /// <summary>
        /// Open the /ref TreeEditor to manipulate the tree view content on double clic.
        /// </summary>
        private void tv_Reports_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 2)
                    EditTree ();
            }
        }

        /// <summary>
        /// Fonction pour lancer l'editeur de TreeView sur un evenement.
        /// </summary>
        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditTree();
        }

        #region fonctions pour le preview
        internal static Interface.Help hPdf;
        internal static Interface.Help hHtml;
        /// <summary>
        /// Fonction pour lancer la previsualisation du rapport en HTML sur un evenement.
        /// </summary>
        private void ll_htmlPreview_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Preview(true);
        }
        /// <summary>
        /// Fonction pour lancer la previsualisation du rapport en PDF sur un evenement.
        /// </summary>
        private void ll_pdfPreview_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Preview(false);
        }
        /// <summary>
        /// Fontion pour afficher un aperçu du pdf ou du html
        /// </summary>
        /// <param name="isHtml">Previsualisation du html, sinon du pdf</param>
        private void Preview(bool isHtml)
        {
            if (CheckFormError())
                return;
                        
            generateReportPreview(this.parameters, tv_Reports.Nodes, Donnees, isHtml, !isHtml, 
                this.Location, this.Size);
        }
        internal static Prompt.SIM_Chargement cht = null;
        internal static void ShowChargement(Object cht)
        {
            ((Prompt.SIM_Chargement)cht).ShowDialog();
            return;
        }

        // >> Task #13384 Report Tree-view
        internal static string REPORTS_ROOT_DIRECTORY_NAME = "Reports";
        internal static string SINGLE_REPORTS_DIRECTORY_NAME = "Single Reports";
        internal static string GROUPED_REPORTS_DIRECTORY_NAME = "Grouped Reports";
        public static void generateReportPreview(ReportParameters reportParameters,
            TreeNodeCollection allReports, GestionDonneesHUB2SIM Donnees,
            bool generateHtmlFormat, bool generatePdfFormat,
            System.Drawing.Point loadingWindowLocation, Size loadingWindowSize)
        {            
            String sPath = OverallTools.ExternFunctions.getTempDirectoryForPax2sim();
            // >> Task #17969 PAX2SIM - Reports improvements            
            sPath += REPORTS_ROOT_DIRECTORY_NAME + "\\" + reportParameters.Name + "_" + PAX2SIM.reportPreviewSequence.ToString() + "\\";
            /*if (reportParameters.reportGroup == "")
            {
                sPath += REPORTS_ROOT_DIRECTORY_NAME + "\\" + SINGLE_REPORTS_DIRECTORY_NAME + "\\" + reportParameters.Name + "_" + PAX2SIM.reportPreviewSequence.ToString() + "\\";
            }
            else
            {
                sPath += REPORTS_ROOT_DIRECTORY_NAME + "\\" + GROUPED_REPORTS_DIRECTORY_NAME + "\\" + reportParameters.reportGroup + "\\"
                        + reportParameters.Name + "_" + PAX2SIM.reportPreviewSequence.ToString() + "\\";
            }*/
            // << Task #17969 PAX2SIM - Reports improvements

            PAX2SIM.reportPreviewSequence++;
            OverallTools.ExternFunctions.CheckCreateDirectory(sPath);
            String fileName = reportParameters.Name;
            String windowTitle = "Report Preview";
            bool reportGererated = false;
            
            // Initialisation de la fenetre de chargement
#if(!DEBUG)
            cht = new SIMCORE_TOOL.Prompt.SIM_Chargement(loadingWindowLocation, loadingWindowSize);
            System.Threading.Thread tSimulation =
                new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ShowChargement));
            tSimulation.Start(cht);
            System.Threading.Thread.Sleep(100);
#endif

            // lance la generation du rapport
            if (generateHtmlFormat)
            {
                if (hHtml == null || hHtml.Visible == false) // si la fenetre est fermée
                {
                    reportGererated = Donnees.generateReport(
                        allReports,
                        sPath, null, cht,
                        reportParameters);
                    fileName += ".html";
                    windowTitle = "HTML Report Preview";
                }
                else
                    hHtml.BringToFront();
            }
            else if (generatePdfFormat)
            {
                if (hPdf == null || hPdf.Visible == false)
                {
                    reportGererated = Donnees.generatePDFReport(
                        allReports,
                        sPath, null, cht,
                        reportParameters);
                    fileName += ".pdf";
                    windowTitle = "PDF Report Preview";
                }
                else
                    hPdf.BringToFront();
            }

            // ferme la fenetre de chergement
            if (cht != null)
                cht.KillWindow();

            // lance la fenetre de previsualisation
            if (System.IO.File.Exists(sPath + fileName) && reportGererated)
            {
                Interface.Help helpWin = Interface.Help.OpenFile(sPath + fileName);
                helpWin.Text = windowTitle;
                if (generateHtmlFormat)
                    hHtml = helpWin;
                else if (generatePdfFormat)
                    hPdf = helpWin;
            }
        }
        // << Task #13384 Report Tree-view
        #endregion

        private void ReportGenerator_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (!saved)
            {
                DialogResult result = MessageBox.Show("Report configuration was not saved. Do you want to save ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SaveButton_Click(sender, e);
                    saved = true;
                }
                if (result == DialogResult.Cancel)
                    DialogResult = DialogResult.None;
            }*/
            checkSavedStatus();
            if (CheckFormError())
                DialogResult = DialogResult.None;
            if (!PAX2SIM.liegeMode)
            {
                OverallTools.TreeViewFunctions.refreshMainReportsNode(ReportParametersList, Collection, Donnees, reportsNodeContextMenu);   // >> Task #13384 Report Tree-view
            }
        }

        private void button2_Click(object sender, EventArgs e)  // >> Task #17969 PAX2SIM - Reports improvements
        {
            checkSavedStatus();
            resetReportEditor();
            LoadReportComboBox();
            
        }

        private void resetReportEditor()
        {
            exportNoteCheckBox.Checked = false;
            generateHTMLCheckBox.Checked = false;
            generatePDFCheckBox.Checked = true;
            purgedTreeCheckBox.Checked = false;            
            exportSourceTextFilesCheckBox.Checked = false;
            exportXmlConfigurationCheckBox.Checked = false;
            if (PageFormatBox.Items.Count > 0)
            {
                PageFormatBox.SelectedIndex = 0;
            }
            tb_template.Text = ReportParameters.defaultTemplateString;
            cb_useTemplate.Checked = true;
            tb_logo.Text = "";

            AutorTextBox.Text = "";
            titleTextBox.Text = "";
            versionTextBox.Text = "";
            dateTextBox.Text = "";
            if (cb_dividerLevel.Items.Count > 0)
            {
                cb_dividerLevel.SelectedIndex = 0;
            }            
            versionList = new List<string[]>();
            approvalList = new List<string[]>();
            docControlElt = new String[7] { "", "", "", "", "", "", "" };
            lsResizeTable = new Dictionary<String, ReportParameters.ColumnSizePolicy>();
            tv_Reports.Nodes.Clear();
            tempCurrentReportGroups = new List<string>();

            ParamChange(null, null);
        }

        private void checkSavedStatus()
        {
            if (!saved)
            {
                DialogResult result = MessageBox.Show("Report configuration was not saved. Do you want to save ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SaveButton_Click(null, null);   //(sender, e);
                    saved = true;
                }
                if (result == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.None;
                }
            }
        }

        // >> Task #17969 PAX2SIM - Reports improvements
        internal static Dictionary<string, List<string>> getAvailableReportGroups(string projectPath, Dictionary<string, ReportParameters> reportParameters)
        {
            Dictionary<string, List<string>> reportGroups = new Dictionary<string, List<string>>();
            if (reportParameters == null)
            {
                return reportGroups;
            }
            string reportGroupsRootDirectory = projectPath + "Output\\" + ReportGenerator.REPORTS_ROOT_DIRECTORY_NAME + "\\" 
                + ReportGenerator.GROUPED_REPORTS_DIRECTORY_NAME;
            updateAvailableReportGroupsFromDirectory(reportGroupsRootDirectory, reportGroups);
            updateAvailableReportGroupsWithReportParameters(reportParameters, reportGroups);
            return reportGroups;
        }

        private static void updateAvailableReportGroupsFromDirectory(string reportGroupsRootDirectory, Dictionary<string, List<string>> allReportGroups)
        {
            if (Directory.Exists(reportGroupsRootDirectory))
            {
                try
                {
                    string[] reportGroupsDirectoriesPaths = Directory.GetDirectories(reportGroupsRootDirectory);
                    foreach (string reportGroupDirectoryPath in reportGroupsDirectoriesPaths)
                    {
                        List<string> reports = new List<string>();
                        string[] reportDirectoriesPaths = Directory.GetDirectories(reportGroupDirectoryPath);
                        foreach (string reportDirectoryPath in reportDirectoriesPaths)
                        {
                            reports.Add(System.IO.Path.GetFileName(reportDirectoryPath));
                        }
                        allReportGroups.Add(System.IO.Path.GetFileName(reportGroupDirectoryPath), reports);

                    }
                }
                catch (Exception exception)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while retrieving subdirectorie from \"" + reportGroupsRootDirectory + "\". " + exception.Message);
                }
            }
        }

        private static void updateAvailableReportGroupsWithReportParameters(Dictionary<string, ReportParameters> allReportParameters, 
            Dictionary<string, List<string>> allReportGroups)
        {
            /*
            foreach (KeyValuePair<string, List<string>> reportGroupPair in allReportGroups)
            {
                foreach (KeyValuePair<string, ReportParameters> reportParameterPair in allReportParameters)
                {
                    if (reportParameterPair.Value.reportGroupsList.Contains(reportGroupPair.Key)
                        && !reportGroupPair.Value.Contains(reportParameterPair.Key))
                    {
                        reportGroupPair.Value.Add(reportParameterPair.Key);
                    }
                }
            }*/

            foreach (KeyValuePair<string, ReportParameters> reportParameterPair in allReportParameters)
            {
                string reportName = reportParameterPair.Key;
                ReportParameters reportParameters = reportParameterPair.Value;
                if (reportParameters.reportGroupsList == null)
                {
                    continue;
                }
                foreach (string reportGroupName in reportParameters.reportGroupsList)
                {
                    if (allReportGroups.ContainsKey(reportGroupName))
                    {
                        if (!allReportGroups[reportGroupName].Contains(reportName))
                        {
                            allReportGroups[reportGroupName].Add(reportName);
                        }
                    }
                    else
                    {
                        allReportGroups.Add(reportGroupName, new List<string> { reportName });
                    }
                }
            }
        }

        private void assignToReportGroupsButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<string>> availableReportGroups = getAvailableReportGroups(Donnees.getDossierEnregistrement(), Donnees.htReportParametres);            
            string reportGroupsRootDirectory = Donnees.getDossierEnregistrement() + "Output\\" + ReportGenerator.REPORTS_ROOT_DIRECTORY_NAME + "\\"
                                                + ReportGenerator.GROUPED_REPORTS_DIRECTORY_NAME;

            PAX2SIM.assignReportToReportGroups(this.parameters.Name, this.parameters.reportGroupsList, availableReportGroups, reportGroupsRootDirectory, Donnees.htReportParametres);
        }

        // << Task #17969 PAX2SIM - Reports improvements

    }
}