using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.Classes;
using SIMCORE_TOOL.com.crispico.gantt;

namespace SIMCORE_TOOL.Prompt
{
    public partial class TreeEditor : Form
    {
        private String paragNodeName = "Report notes";
        private String assumptionPrefix = "Assumption";
        private String notePrefix = "Note";
        private String chartPrefix = "Chart";
        /// <summary>
        /// Repertoire temporaire ou sauverager les images des preview.
        /// </summary>
        private String imageTempPath = null;
        /// <summary>
        /// Variable indiquant le TreeView actuellement actif.
        /// </summary>
        private TreeView ActifTree = null;
        /// <summary>
        /// Jeu de données utilisées.
        /// </summary>
        private GestionDonneesHUB2SIM Donnees;

        /// <summary>
        /// La classe qui est utilisée pour voir les aperçu des tables.
        /// </summary>
        Prompt.IgnoredLigne TableViewer;

        /// <summary>
        /// La classe permettant de visualiser une image.
        /// </summary>
        List<Interface.Help> imageViewer;

        /// <summary>
        /// La classe permettant de visualiser les notes.
        /// </summary>
        List<Interface.Help> NoteViewer;

        #region Attributs pour la coloration des nodes
        /// <summary>
        /// Couleur utilisé pour le fond des nodes selectionnées
        /// </summary>
        private Color selectedBackColor = Color.RoyalBlue;
        /// <summary>
        /// Couleur utilisé pour la police des nodes selectionnées
        /// </summary>
        private Color selectedForeColor = Color.White;
        /// <summary>
        /// Couleur utilisé pour le fond des nodes non selectionnées
        /// </summary>
        private Color normalBackColor = Color.White;
        /// <summary>
        /// Couleur utilisé pour la police des nodes non selectionnées
        /// </summary>
        private Color normalForeColor = Color.Black;
        #endregion

        #region Constructors and initializations
        /// <summary>
        /// Constructor used to build a custom tree.
        /// </summary>
        /// <param name="Collection"></param>
        /// <param name="imgList"></param>
        /// <param name="Donnees"></param>
        public TreeEditor(string reportName, TreeNodeCollection Collection, ImageList imgList, GestionDonneesHUB2SIM Donnees)
        {
            this.Donnees = Donnees;
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            InitializeMainTree(Collection, imgList, Donnees);
            InitializeFinalTree(null, imgList, Donnees);

            this.Text = "Edit \"" + reportName + "\"";
        }

        /// <summary>
        /// Constructor used to edit a custom tree.
        /// </summary>
        /// <param name="customTree">Tree to edit</param>
        /// <param name="Collection"></param>
        /// <param name="imgList"></param>
        /// <param name="Donnees"></param>
        public TreeEditor(string reportName, TreeNodeCollection customTree, TreeNodeCollection Collection, ImageList imgList, GestionDonneesHUB2SIM Donnees)
        {
            this.Donnees = Donnees;
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            InitializeMainTree(Collection, imgList, Donnees);
            InitializeFinalTree(customTree, imgList, Donnees);
            
            this.Text = "Edit \"" + reportName + "\"";
        }

        /// <summary>
        /// Fill the main tree with the content on Pax2Sim main TreeView.
        /// </summary>
        /// <param name="Collection">Collection of Pax2Sim main TreeView nodes</param>
        /// <param name="imgList">Icones of the nodes in TreeView.</param>
        /// <param name="Donnees">Donnees en cours</param>
        private void InitializeMainTree (TreeNodeCollection Collection, ImageList imgList, GestionDonneesHUB2SIM Donnees)
        {
            // initialize main tree
            tv_original.ImageList = imgList;
            int i;
            for (i = 1; i < Collection.Count; i++) // collection[1] is the airport structure
            {
                TreeNode tnTemp = ((TreeNode)Collection[i]);
                TreeNode tnNewNode = OverallTools.TreeViewFunctions.createBranch(tnTemp.Name, tnTemp.Text, tnTemp.Tag, null);
                tv_original.Nodes.Add(tnNewNode);
                OverallTools.TreeViewFunctions.copyChilds(tnNewNode, tnTemp, true, Donnees, true, true);
            }
            SetOriginalTreeNode(tv_original.Nodes);
            //SetChartScenario();
            LoadAssumption();
            LoadParagraph();
            LoadResultNode(tv_original.Nodes);
            imageViewer = new List<Interface.Help>();
            NoteViewer = new List<Interface.Help>();
        }

        /// <summary>
        /// Set the chart scenario and name to the upper node value in order to be able found 
        /// the related data at generation time.
        /// </summary>
        private void SetChartScenario()
        {
            List<TreeNode> nodes = new List<TreeNode>();
            OverallTools.TreeViewFunctions.RechercheNodes("Chart", tv_original.Nodes, ref nodes);
            if (nodes.Count <= 0)
                return;
            foreach (TreeNode node in nodes)
            {
                TreeNode parent = node.Parent;
                if (parent == null)
                    continue;
                TreeViewTag tag = node.Tag as TreeViewTag;

                // set scenario name for all elements
                tag.IsUnleashed = true; // need to be donne before change the scenario name
                tag.ScenarioName = (parent.Tag as TreeViewTag).ScenarioName;
                node.Name = parent.Name;

                // Add note
                String note = Donnees.getNote(tag.ScenarioName, node.Name);
                if (note != null)
                {
                    TreeNode noteNode = new TreeNode(notePrefix + "_" + node.Text);
                    noteNode.Name = node.Name;
                    TreeViewTag noteTag = TreeViewTag.getBlockedParagraphNode(notePrefix);
                    node.Nodes.Add(noteNode);
                }
            }
        }

        /// <summary>
        /// Scan the tree to set node.
        /// - Set the chart scenario and name to the upper node value in order to be able found 
        /// the related data at generation time.
        /// - Create a Paragraph node for each note.
        /// </summary>
        private void SetOriginalTreeNode(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                TreeViewTag tag = node.Tag as TreeViewTag;

                // Add note
                if (!tag.isParagraphNode)
                {
                    if (tag == null)
                        continue;
                    
                    if (Donnees.getNote(tag.ScenarioName, node.Name) != null ||
                        Donnees.getExNote(tag.ScenarioName, tag.Name, tag.ExceptionName) != null ||
                        Donnees.getGeneralNote(node.Name) != null)
                    {
                        TreeNode noteNode = new TreeNode(notePrefix + "_" + node.Text);
                        noteNode.Name = node.Name;
                        TreeViewTag noteTag = TreeViewTag.getBlockedParagraphNode(tag.Name);
                        //TreeViewTag noteTag = TreeViewTag.getBlockedParagraphNode(notePrefix);
                        noteNode.Tag = noteTag;
                        noteTag.IsUnleashed = true;
                        noteTag.ScenarioName = tag.ScenarioName;
                        noteTag.ExceptionName = tag.ExceptionName;
                        noteTag.IsExceptionNode = tag.IsExceptionNode;
                        noteNode.ImageIndex = noteTag.ImageIndex;
                        noteNode.SelectedImageIndex = noteTag.SelectedImageIndex;
                        node.Nodes.Add(noteNode);
                    }
                    //>>GanttNote for Report                    
                    if (Donnees.getGanttNote(tag.ScenarioName, node.Name) != null)
                    {
                        // << Task #6386 Itinerary process                        
                        //TreeNode ganttNoteNode = new TreeNode("Gantt " + notePrefix + "_" + node.Text);
                        //TreeNode ganttNoteNode = new TreeNode("Image " + notePrefix + "_" + node.Text);
                        TreeNode ganttNoteNode = new TreeNode(Model.GANTT_PREFIX + " " + notePrefix + "_" + node.Text);
                        // >> Task #6386 Itinerary process

                        ganttNoteNode.Name = node.Name;

                        TreeViewTag ganttNoteTag = TreeViewTag.getGanttParagraphNode(tag.Name);
                        ganttNoteTag.IsUnleashed = true;
                        ganttNoteTag.ScenarioName = tag.ScenarioName;
                        ganttNoteTag.ExceptionName = tag.ExceptionName;
                        ganttNoteTag.IsExceptionNode = tag.IsExceptionNode;

                        ganttNoteNode.Tag = ganttNoteTag;
                        ganttNoteNode.ImageIndex = ganttNoteTag.ImageIndex;
                        ganttNoteNode.SelectedImageIndex = ganttNoteTag.SelectedImageIndex;

                        node.Nodes.Add(ganttNoteNode);
                    }
                    // >> Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports
                    if (Donnees.getDashboardNote(tag.ScenarioName, node.Name) != null)
                    {
                        TreeNode dashboardNoteNode = new TreeNode(GlobalNames.DASHBOARD_NODE_PREFIX + " " + notePrefix + "_" + node.Text);
                        dashboardNoteNode.Name = node.Name;

                        TreeViewTag dashboardNoteTag = TreeViewTag.getDashboardParagraphNode(tag.Name);
                        dashboardNoteTag.IsUnleashed = true;
                        dashboardNoteTag.ScenarioName = tag.ScenarioName;
                        dashboardNoteTag.ExceptionName = tag.ExceptionName;
                        dashboardNoteTag.IsExceptionNode = tag.IsExceptionNode;

                        dashboardNoteNode.Tag = dashboardNoteTag;
                        dashboardNoteNode.ImageIndex = dashboardNoteTag.ImageIndex;
                        dashboardNoteNode.SelectedImageIndex = dashboardNoteTag.SelectedImageIndex;

                        node.Nodes.Add(dashboardNoteNode);
                    }
                    // << Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports
                }

                // set name and scenario name for all charts
                if (node.Name == "Chart")
                {
                    TreeNode parent = node.Parent;
                    if (parent == null || parent.Name == "Charts")
                        continue; // don't rename chart from Charts part
                    tag.IsUnleashed = true; // need to be donne before change the scenario name
                    tag.ScenarioName = (parent.Tag as TreeViewTag).ScenarioName;
                    node.Name = parent.Name;
                }

                // scan child
                SetOriginalTreeNode(node.Nodes);
            }
        }

        /// <summary>
        /// Scan the tree to set node.
        /// - Add cmsTable ContextMenuStrip to all table.
        /// </summary>
        private void SetFinalTreeNode(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                TreeViewTag tag = node.Tag as TreeViewTag;

                // add cmsTable ContextMenuStrip to all table
                if (tag.isTableNode || tag.isFilterNode || tag.isResultNode)
                {
                    //node.ContextMenuStrip = cmsTable;
                }

                // scan child
                SetFinalTreeNode(node.Nodes);
            }
        }

        /// <summary>
        /// For each scenario, add a fake paragraph node in order to let user to create a real
        /// assumption paragraph by adding it in the final tree.
        /// </summary>
        private void LoadAssumption()
        {
            List<String> scenarioNames = Donnees.getScenarioNames ();
            foreach (String scenarioName in scenarioNames) /// Pour chaqu'un des scenarios,
            {
                foreach (TreeNode tn in tv_original.Nodes)
                {
                    TreeNode scenarioNode = OverallTools.TreeViewFunctions.RechercherNomEnfantStrict(
                        scenarioName, tn); /// on cherche la TreeNode associée,
                    if (scenarioNode != null)
                    {
                        TreeNode assumptionNode = new TreeNode(assumptionPrefix + "_" + scenarioName);
                        assumptionNode.Name = assumptionNode.Text;
                        TreeViewTag tag = TreeViewTag.getParagraphNode(assumptionPrefix);
                        tag.IsUnleashed = true; // need to be donne before change the scenario name
                        tag.ScenarioName = scenarioName; 
                        assumptionNode.ImageIndex = tag.ImageIndex;
                        assumptionNode.SelectedImageIndex = tag.SelectedImageIndex;
                        assumptionNode.Tag = tag;
                        scenarioNode.Nodes.Insert(0, assumptionNode); /// puis on ajoute un paragraph assumption
                    }
                }
            }
        }

        /// <summary>
        /// Devide ResultNode in DirectoryNode and TableNode.
        /// </summary>
        private void LoadResultNode(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                TreeViewTag tag = node.Tag as TreeViewTag;
                if (tag != null && tag.isResultNode)
                {
                    // set the table node
                    TreeViewTag tableTag = TreeViewTag.getTableNode (tag.ScenarioName, node.Name);
                    TreeNode tableNode = new TreeNode(node.Text, tableTag.ImageIndex, tableTag.SelectedImageIndex);
                    tableNode.Tag = tableTag;
                    tableNode.Name = node.Name;
                    tableNode.Text = "Table_"  + node.Text;

                    // set the directory node
                    node.Tag = TreeViewTag.getDirectoryNode (node.Name);
                    node.SelectedImageIndex = tag.SelectedImageIndex;
                    node.ImageIndex = tag.ImageIndex;
                    node.Nodes.Insert(0, tableNode);
                }
                LoadResultNode(node.Nodes);
            }
        }

        /// <summary>
        /// Fill the final tree with the given custom tree.
        /// </summary>
        /// <param name="customTree">Collection of custom TreeView nodes</param>
        /// <param name="imgList">Icones of the nodes in TreeView.</param>
        /// <param name="Donnees">Donnees en cours</param>
        private void InitializeFinalTree(TreeNodeCollection customTree, ImageList imgList, GestionDonneesHUB2SIM Donnees)
        {
            // initialize final tree
            tv_final.ImageList = imgList;
            InitializeContextMenuStrip();
            if (customTree == null) return;
            for (int i=0; i < customTree.Count; i++) 
            {
                TreeNode tnTemp = ((TreeNode)customTree[i]);
                TreeNode tnNewNode = OverallTools.TreeViewFunctions.createBranch(tnTemp.Name, tnTemp.Text, tnTemp.Tag, null);
                tv_final.Nodes.Add(tnNewNode);
                OverallTools.TreeViewFunctions.copyChilds(tnNewNode, tnTemp, false, Donnees, true, true);
            }
            SetFinalTreeNode(tv_final.Nodes);
        }
        #endregion Constructors and initializations

        #region Insert
        /// <summary>
        /// Add the selected node of the main tree in the report tree. 
        /// It will be added as child of the selected node of the report tree.
        /// It will be added to the root if no node is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_insert_Click(object sender, EventArgs e)
        {
            if (tv_original.SelectedNode != null)
            {
                if (tv_final.SelectedNode == null)
                    CopyNodeInto(tv_original.SelectedNode, null, tv_final.Nodes, true);
                else
                    CopyNodeInto(tv_original.SelectedNode, tv_final.SelectedNode, tv_final.SelectedNode.Nodes, true);
            }
            tv_original.Focus();
        }
        /// <summary>
        /// Add the selected node of the report tree in the main tree. 
        /// It will be added to root nodes of the report tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_insertRoot_Click(object sender, EventArgs e)
        {
            if (tv_original.SelectedNode != null)
                CopyNodeInto(tv_original.SelectedNode, null, tv_final.Nodes, true);
            tv_original.Focus();
        }
        /// <summary>
        /// Recursive copy of a node and its child into the collection.
        /// Do a specific process for Chart node (put an explicit text)
        /// dans Assumption node (build the associated Paragraph).
        /// </summary>
        /// <param name="node">Node to copy</param>
        /// <param name="destinationNode">Node where to add the copy)</param>
        /// <param name="destinationCollection">Collection where to add the copy</param>
        /// <param name="bHierarchy">if false, stop respecting the tree hierarchy when copy children</param>
        private TreeNode CopyNodeInto(TreeNode node, TreeNode destinationNode, TreeNodeCollection destinationCollection, bool bHierarchy)
        {
            TreeNode tnNewNode = OverallTools.TreeViewFunctions.createBranch(node.Name, node.Text, node.Tag, null, false, true);
            TreeViewTag tag = node.Tag as TreeViewTag;
            TreeNode childDestinationNode;
            TreeNodeCollection childDestinationCollection;
            bool hierarchy = bHierarchy;
            
            /// if the destination node is not a directory then copy in its parents directory or directly in the tree root
            if (destinationNode != null && destinationNode.Tag != null && !(destinationNode.Tag as TreeViewTag).isDirectoryNode)
            {
                if (destinationNode.Parent == null)
                {
                    return CopyNodeInto(node, null, tv_final.Nodes, hierarchy);
                }
                else
                {
                    if (destinationNode.Parent.Tag != null && (destinationNode.Parent.Tag as TreeViewTag).isDirectoryNode)
                    {
                        return CopyNodeInto(node, destinationNode.Parent, destinationNode.Parent.Nodes, hierarchy);
                    }
                }
            }

            /// If the node is a chart then give it a more understandable name
            if (tag.isChartNode)
            {
                if (node.Text == "Chart")
                    tnNewNode.Text = chartPrefix + "_" + tnNewNode.Name;
            }
            /// If the node is an assumption then create the related Paragraph
            else if (tag.isParagraphNode)
            {
                if (tnNewNode.Name.StartsWith(assumptionPrefix))
                {
                    if (node.Parent == null) return null;
                    tnNewNode.Text = AssumptionToParagraph(node);
                    tnNewNode.Name = tnNewNode.Text;
                }
            }
            /// If the node is a table then apply the cmsTable ContextMenuStrip
            else if (tag.isFilterNode || tag.isResultNode || tag.isTableNode)
            {
                //node.ContextMenuStrip = cmsTable;
            }

            /// Set the destination of child node copy
            if (hierarchy)
            {
                /// if the node isn't a directory then create a directory and copy all the child without taking care of the hierarchy
                if (tag.isDirectoryNode)
                {
                    destinationCollection.Add(tnNewNode);
                    childDestinationNode = tnNewNode;
                    childDestinationCollection = tnNewNode.Nodes;
                }
                else
                {
                    hierarchy = false;
                    TreeNode dirNode = new TreeNode(node.Text);
                    dirNode.Name = node.Text;
                    TreeViewTag dirTag = TreeViewTag.getDirectoryNode(node.Name);
                    dirNode.Tag = dirTag;
                    destinationCollection.Add(dirNode);
                    dirNode.Nodes.Add(tnNewNode);
                    childDestinationNode = dirNode;
                    childDestinationCollection = dirNode.Nodes;
                }
            }
            else
            {
                if (tag.isDirectoryNode)
                {
                    hierarchy = true;
                    destinationCollection.Add(tnNewNode);
                    childDestinationNode = tnNewNode;
                    childDestinationCollection = tnNewNode.Nodes;
                }
                else
                {
                    destinationCollection.Add(tnNewNode);
                    childDestinationNode = destinationNode;
                    childDestinationCollection = destinationCollection;
                }
            }   

            /// Add its children
            foreach (TreeNode childNode in node.Nodes)
            {
                CopyNodeInto(childNode, childDestinationNode, childDestinationNode.Nodes, hierarchy);
            }

            return tnNewNode;
        }
        /// <summary>
        /// Create a Paragraph according to the AssumptionNode.
        /// </summary>
        /// <param name="assumptionNode">La node de l'arbre d'origine</param>
        /// <returns>Name of the created Paragraph</returns>
        private String AssumptionToParagraph(TreeNode assumptionNode)
        {
            if (assumptionNode.Parent == null) return null;
            String scenarioName = assumptionNode.Parent.Name;
            DialogResult result = DialogResult.Cancel;
            /// on verifie qu'elle n'existe pas deja dans les paragraphs (recherche de assumption_"scenario" dans les paragraphs)
            if (Donnees.getParagraphList().ContainsKey(assumptionNode.Name))
            { /// _ si elle existe
                /// __ on previent que la note existe et on demande si l'utilisateur veux créer une nouvelle ou inserer celle-la
                result = MessageBox.Show(
                      "Paragraph '" + assumptionNode.Name + "' already exist, do you want to edit it ?",
                      "Paragraph: " + assumptionNode.Name,
                      MessageBoxButtons.YesNo);
            }
            else
            { 
                /// si elle n'existe pas
                /// __ on crée le Paragraph
                CreateParagraphNode(assumptionNode.Name);
                List<String> review = null;
                if (Donnees.GetScenario(scenarioName) != null)
                {
                    review = Donnees.GetScenario(scenarioName).getReview(false);
                }
                else
                {
                    DataManagement.DataManager dm = Donnees.GetDataManager(scenarioName);
                    if(dm == null)
                        return null;
                    if (typeof(DataManagement.DataManagerParking).IsInstanceOfType(dm))
                        review = ((DataManagement.DataManagerParking)dm).getReview(false);
                    else if (typeof(DataManagement.DataManagerAllocation).IsInstanceOfType(dm))
                        review = ((DataManagement.DataManagerAllocation)dm).getReview(false);
                    else
                        return null;
                }
                String content = OverallTools.FonctionUtiles.AsumptionToHtml(review);
                GestionDonneesHUB2SIM.Paragraph pParag = new GestionDonneesHUB2SIM.Paragraph(
                                    assumptionNode.Name, null, content);
                Donnees.setParagraph(assumptionNode.Name, pParag);
                /// __ puis on demande si il veut editer
                result = MessageBox.Show(
                    "New paragraph has been created, do you want to edit it ?",
                    "Paragraph: " + assumptionNode.Name,
                    MessageBoxButtons.YesNo);
            }
            /// Edition du paragraphe
            ParagraphEditor pe;
            if (result == DialogResult.Yes)
            {
                pe = new ParagraphEditor(Donnees, assumptionNode.Name);
                pe.ShowDialog();
                if (pe.ParagraphName != assumptionNode.Name) // un nouveau paragraph a été créé
                {
                    CreateParagraphNode(pe.ParagraphName);
                    return pe.ParagraphName;
                }
            }
            return assumptionNode.Name;
        }
        #endregion Insert

        #region Gestion du déplacement des nodes à l'aide des fleches.
        /// <summary>
        /// Move the selected node up. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_up_Click(object sender, EventArgs e)
        {

            if (tv_final.SelectedNode == null) return;
            ChangeNodeOrder(tv_final.SelectedNode, -1);
        }
        /// <summary>
        /// Move the selected node down. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_down_Click(object sender, EventArgs e)
        {
            if (tv_final.SelectedNode == null) return;
            ChangeNodeOrder(tv_final.SelectedNode, +1);
        }
        /// <summary>
        /// Move the node up or down according to 'shift'.
        /// </summary>
        /// <param name="toMove">Node to move</param>
        /// <param name="shift">number and direction to move (up: - / down: +)</param>
        private void ChangeNodeOrder(TreeNode toMove, int shift)
        {
            TreeNode parent = toMove.Parent;
            TreeNodeCollection list;
            if (parent != null)
                list = parent.Nodes;
            else
                list = tv_final.Nodes;
            int index = list.IndexOf(toMove) + shift;
            if (index >= 0 && index < list.Count)
            {
                list.Remove(toMove);
                list.Insert(index, toMove);
                tv_final.SelectedNode = toMove;
            }
            tv_final.Focus();
        }
        /// <summary>
        /// Move the selected node to the parent list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_left_Click(object sender, EventArgs e)
        {
            if (tv_final.SelectedNode == null) return;
            TreeNode toMove = tv_final.SelectedNode;
            if (toMove.Parent == null) return;
            TreeNode parent = toMove.Parent;
            TreeNode grandparent = parent.Parent;
            if (grandparent != null)
            {
                int index = grandparent.Nodes.IndexOf(parent);
                parent.Nodes.Remove(toMove);
                grandparent.Nodes.Insert(index + 1, toMove);
                tv_final.SelectedNode = toMove;
            }
            else
            {
                int index = tv_final.Nodes.IndexOf(parent);
                parent.Nodes.Remove(toMove);
                tv_final.Nodes.Insert(index + 1, toMove);
                tv_final.SelectedNode = toMove;
            }
            tv_final.Focus();
        }
        /// <summary>
        /// Move the selected node into its previous node.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_right_Click(object sender, EventArgs e)
        {
            if (tv_final.SelectedNode == null) return;
            TreeNode toMove = tv_final.SelectedNode;
            TreeNode parent = toMove.Parent;
            TreeNode newParent = toMove.PrevNode;
            if (newParent != null) 
            {
                // delete from parent list
                if (parent != null) 
                    parent.Nodes.Remove(toMove);
                else
                    tv_final.Nodes.Remove(toMove);
                // add to new parent list
                newParent.Nodes.Add(toMove);
                tv_final.SelectedNode = toMove;
            }
            tv_final.Focus();
        }
        #endregion Gestion du déplacement des nodes à l'aide des fleches.

        #region Create / Delete
        /// <summary>
        /// Add a directory node in the report treeview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_CreateDirectory_Click(object sender, EventArgs e)
        {
            TreeNodeCollection list = tv_final.Nodes;
            if (tv_final.SelectedNode != null)
                list = tv_final.SelectedNode.Nodes;
            list.Add(
                OverallTools.TreeViewFunctions.CreateDirectory("new directory"));
            tv_final.Focus();
        }
        /// <summary>
        /// Remove selected node from rigtht tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_delete_Click(object sender, EventArgs e)
        {
            if (tv_final.SelectedNode == null) return;

            TreeNode toDelete = tv_final.SelectedNode;
            TreeNode parent = toDelete.Parent;
            if (parent == null)
                tv_final.Nodes.Remove(toDelete);
            else
                parent.Nodes.Remove(toDelete);
            tv_final.Focus();
        }

        /// <summary>
        /// Fonction pour supprimer une note à l'appuis sur la touche "suppr."
        /// </summary>
        private void tv_final_KeyDown(object sender, KeyEventArgs e)
        {
            // si la touche suppr. a été enfoncé, 
            // que le TreeView 'report content' à le focus 
            // et qu'une node est selectionnée : on supprime la node selectionnée
            if (e.KeyValue == 46 &&
                tv_final.Focused &&
                tv_final.SelectedNode != null)
            {
                TreeNode toDelete = tv_final.SelectedNode;
                TreeNode parent = toDelete.Parent;
                if (parent == null)
                    tv_final.Nodes.Remove(toDelete);
                else
                    parent.Nodes.Remove(toDelete);
                //tv_final.Focus();
            }
        }
        #endregion Create / Delete

        #region Paragraph management
        /// <summary>
        /// Add a Paragraph directory in the main tree and load the paragraph
        /// </summary>
        private void LoadParagraph()
        {
            TreeNode paragNode;
            if (tv_original.Nodes[paragNodeName] == null)
            {
                paragNode = OverallTools.TreeViewFunctions.CreateDirectory(paragNodeName);
                tv_original.Nodes.Add(paragNode);
            }
            else
            {
                paragNode = tv_original.Nodes[paragNodeName];
                paragNode.Nodes.Clear();
            }

            Dictionary<String, GestionDonneesHUB2SIM.Paragraph> dicParag = Donnees.getParagraphList();
            foreach (String paragName in dicParag.Keys)
            {
                this.CreateParagraphNode(paragName);
            }
        }

        /// <summary>
        /// Create a Paragraph node in the main tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_CreateNote_Click(object sender, EventArgs e)
        {
            /// Launch the paragraph editor tool (\ref TreeEditor) in a separate window.
            ParagraphEditor pe = new ParagraphEditor(Donnees);
            pe.ShowDialog ();

            /// When it close, retrieve the paragraph name 
            /// and add the according node into the paragraph list
            String sParag = pe.ParagraphName;
            if (sParag != null)
            {
                CreateParagraphNode(sParag);
            }

            //String paragraphName = "introPart1";
            //Donnees.setParagraph(paragraphName, new GestionDonneesHUB2SIM.Paragraph(paragraphName, null, "html content"));

            //TreeViewTag Tag = TreeViewTag.getParagraphNode(paragraphName);
            //TreeNode Node = OverallTools.TreeViewFunctions.createBranch(paragraphName, paragraphName, Tag, null);

            //GestionDonneesHUB2SIM.Paragraph p = Donnees.getParagraph(Node.Name);

            //list.Add(Node);
        }

        /// <summary>
        /// Create a Paragraph node according to the given Paragraph.
        /// </summary>
        /// <param name="pParag">The paragraph name.</param>
        /// <returns>The created node.</returns>
        private TreeNode CreateParagraphNode(String sParagName)
        {
            if (sParagName == null)
                return null;
            TreeNodeCollection paragList = tv_original.Nodes[paragNodeName].Nodes;
            TreeNode tnParag = new TreeNode(sParagName);
            tnParag.Tag = TreeViewTag.getParagraphNode(sParagName);
            tnParag.ImageIndex = (tnParag.Tag as TreeViewTag).ImageIndex;
            tnParag.SelectedImageIndex = (tnParag.Tag as TreeViewTag).SelectedImageIndex;
            //tnParag.ContextMenuStrip = cmsParagraph;
            tnParag.Name = (tnParag.Tag as TreeViewTag).Name;
            paragList.Add(tnParag);
            return tnParag;
        }

        /// <summary>
        /// Edit a Paragraph node of the main tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditParagraph_Click(object sender, EventArgs e)
        {
            /// Launch the paragraph editor tool (\ref TreeEditor) in a separate window.
            TreeNode tn = tv_original.SelectedNode;
            EditParagraph (tn);
        }

        /// <summary>
        /// Edit a Paragraph node of the main tree.
        /// </summary>
        private void EditParagraph (TreeNode ParagraphNode)
        {
            ParagraphEditor pe = new ParagraphEditor(Donnees, ParagraphNode.Name);
            pe.ShowDialog();

            /// If the Paraghraph doesn't exist then create one.
            if (!tv_original.Nodes[paragNodeName].Nodes.ContainsKey(pe.ParagraphName))
                CreateParagraphNode(pe.ParagraphName);

            /// if the name has been change then rename the node. (only if it comes from report tree)
            if (pe.ParagraphName != ParagraphNode.Name && ParagraphNode.TreeView == tv_final)
            {
                ParagraphNode.Name = pe.ParagraphName;
                ParagraphNode.Text = ParagraphNode.Name;
            }
        }

        /// <summary>
        /// Delete a Paragraph from the main tree.
        /// It's deleted forever.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteParagraph_Click(object sender, EventArgs e)
        {
            TreeNode node = tv_original.SelectedNode;
            DialogResult result = MessageBox.Show(
                      "Paragraph '" + node.Name + "' will be removed from all reports, do you want to delete it ?",
                      "Paragraph: " + node.Name,
                      MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Donnees.deleteParagraph(node.Name);
                tv_original.Nodes.Remove(node);
                List<TreeNode> nodes = new List<TreeNode>();
                OverallTools.TreeViewFunctions.RechercheNodes(node.Name, tv_final.Nodes, ref nodes);
                foreach (TreeNode tn in nodes)
                        tn.Remove();
            }
        }
        #endregion Paragraph management

        #region Node selection and color
        /// <summary>
        /// Color the selected node to keep highlighted even if the TreeView lost 
        /// the focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selected = e.Node.TreeView.SelectedNode;
            if (selected != null)
            {
                // color the selected node
                ColorNode(selected, true);
            }

            // make remane possible if its a directory node
            if (selected.Tag != null && selected.TreeView == tv_final)
                if (tv_final.LabelEdit != (selected.Tag as TreeViewTag).isDirectoryNode)
                    tv_final.LabelEdit = (selected.Tag as TreeViewTag).isDirectoryNode;
        }

        /// <summary>
        /// Uncolor the node when its deselected.
        /// </summary>
        private void tv_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode selected = e.Node.TreeView.SelectedNode;
            if (selected != null)
            {
                // uncolor the selected node
                ColorNode(selected, false);
            }
        }

        /// <summary>
        /// Function to color and uncolor the node.
        /// </summary>
        /// <param name="node">node that need to be colored</param>
        /// <param name="color">boolean to set if the node ad to be colored or uncolored</param>
        private void ColorNode(TreeNode node, bool color)
        {
            if (color)
            {
                // uncolor the selected node
                node.BackColor = selectedBackColor;
                node.ForeColor = selectedForeColor;
            }
            else
            {
                // uncolor the node
                node.BackColor = normalBackColor;
                node.ForeColor = normalForeColor;
            }
        }

        /// <summary>
        /// Select the node under the mouse on right clic.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tv_original_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                tv_original.SelectedNode = e.Node;
        }
        #endregion

        #region Gestion du clic droit
        /// <summary>
        /// Liste des tables qui doivent être redimensionnées.
        /// Les tables sont referencées par une chaîne de caractere qui est 
        /// la concatenation du nom de la table, du nom du scenario et du type 
        /// d'exception si c'est une table d'exception.
        /// </summary>
        internal Dictionary<String, ReportParameters.ColumnSizePolicy> lsResizeTable = new Dictionary<String, ReportParameters.ColumnSizePolicy>();
        /// <summary>
        /// Fonction ajouter une table à la liste des tables a redimensionner.
        /// </summary>
        /// <param name="dataSet">Nom du scenario</param>
        /// <param name="name">Nom de la table</param>
        /// <param name="exceptionType">Type d'exception</param>
        /// <param name="resizeMode">String</param>
        internal void ResizeTable(String dataSet, String name, String exceptionType, String resizeMode)
        {
            String key = dataSet + name + exceptionType;
            ReportParameters.ColumnSizePolicy policy;
            
            // define the policy
            if (resizeMode == sAdjustContent)
                policy = ReportParameters.ColumnSizePolicy.AdjustContent;
            else if (resizeMode == sAdjustPage)
                policy = ReportParameters.ColumnSizePolicy.AdjustPage;
            else
            {
                lsResizeTable.Remove(key);
                return;
            }

            // if the key exists then update the value
            if (lsResizeTable.ContainsKey(key))
                lsResizeTable[key] = policy;
            else
                lsResizeTable.Add(key, policy);
        }
        /// <summary>
        /// text du combo box du click droit sur les tables
        /// </summary>
        private String sAdjustPage = "Adjust size on page";
        /// <summary>
        /// text du combo box du click droit sur les tables
        /// </summary>
        private String sAdjustContent = "Adjust size on content";
        /// <summary>
        /// text du combo box du click droit sur les tables
        /// </summary>
        private String sAdjustDefault = "Adjust default";
        private void InitializeContextMenuStrip()
        {
            tscResizeTable.Items.Add(sAdjustPage);
            tscResizeTable.Items.Add(sAdjustContent);
            tscResizeTable.Items.Add(sAdjustDefault);
            tscResizeTable.AutoSize = true;

        }
        /// <summary>
        /// Fonction pour mettre à jour la liste des tables a redimensionner,
        /// en fonction du changement qui vient d'être effectué.
        /// (prevu pour être appelée sur l'évenement SelectedIndexChanged du 
        /// ComboBox du click droit sur une table)
        /// </summary>
        private void tscResizeTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNode node = tv_final.SelectedNode;
            TreeViewTag tag = null;
            if (node.Tag != null && node.Tag.GetType() == typeof(TreeViewTag))
                tag = (TreeViewTag)node.Tag;

            ResizeTable(tag.ScenarioName, tag.Name, tag.ExceptionName, tscResizeTable.SelectedItem.ToString());

            cmsTable.Close();
        }

        /// <summary>
        /// Fonction qui ouvre le menu contextuel approprié en fonction de la node selectionnée.
        /// </summary>
        private void tv_final_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            TreeNode node = tv_final.GetNodeAt(e.X, e.Y);
            tv_final.SelectedNode = node;
            TreeViewTag tag = null;
            if (node.Tag != null && node.Tag.GetType() == typeof(TreeViewTag))
                tag = (TreeViewTag) node.Tag;
            
            // The node is a table
            if (tag.isTableNode || tag.isFilterNode || tag.isResultNode || (tag.IsExceptionNode && (!tag.isParagraphNode) && (!tag.isChartNode))) // tag.IsExceptionNode à remplacer par (tag.IsExceptionNode && !tag.isParagraphNode) partout
            {
                ReportParameters.ColumnSizePolicy p;
                if (!lsResizeTable.TryGetValue(tag.ScenarioName + tag.Name + tag.ExceptionName, out p))
                    p = ReportParameters.ColumnSizePolicy.AdjustDefault;
                if (p == ReportParameters.ColumnSizePolicy.AdjustPage)
                    tscResizeTable.SelectedItem = sAdjustPage;
                else if (p == ReportParameters.ColumnSizePolicy.AdjustContent)
                    tscResizeTable.SelectedItem = sAdjustContent;
                else
                    tscResizeTable.SelectedItem = sAdjustDefault;

                cmsTable.Show(tv_final.PointToScreen(e.Location));
            }
            // The node is a Directory
            else if (tag.isDirectoryNode)
            {
                cmsfolder.Show(tv_final.PointToScreen(e.Location));
            }
            // The node is a chart
            else if (tag.isChartNode)
            {
                cmsView.Show(tv_final.PointToScreen(e.Location));
            }
            // The node is a Paragraph
            else if (tag.isParagraphNode)
            {
                cmsView.Show(tv_final.PointToScreen(e.Location));
            }
        }

        /// <summary>
        /// Fonction qui ouvre le menu contextuel approprié en fonction de la node selectionnée.
        /// </summary>
        private void tv_original_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            TreeNode node = tv_original.GetNodeAt(e.X, e.Y);
            tv_original.SelectedNode = node;
            TreeViewTag tag = null;
            if (node.Tag != null && node.Tag.GetType() == typeof(TreeViewTag))
                tag = (TreeViewTag)node.Tag;

            if (tag.isChartNode || tag.isTableNode || tag.isFilterNode || tag.IsExceptionNode)
            {
                cmsView.Show(tv_original.PointToScreen(e.Location));
            }
            else if (tag.isParagraphNode)
            {
                // cas d'un paragraphe de la section "Report note"
                if (node.Parent != null && node.Parent == tv_original.Nodes[paragNodeName])
                {
                    cmsParagraph.Show(tv_original.PointToScreen(e.Location));
                }
                // cas d'una "assumption" non généré
                else if (node.Name.StartsWith(assumptionPrefix))
                {
                    return;
                }
                else
                {
                    cmsView.Show(tv_original.PointToScreen(e.Location));
                }
            }
        }

        /// <summary>
        /// Fonction pour maintenir à jour la variable qui définie quelle TreeView est actif.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tv_EnterTree(object sender, EventArgs e)
        {
            ActifTree = sender as TreeView;
        }
        #endregion

        #region gestion des Drag-and-drop
        #region De TreeView à TreeView et à l'interieur d'un TreeView
        /// <summary>
        /// Set the drop effect.
        /// </summary>
        private void tv_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (sender == tv_final)
                DoDragDrop(e.Item, DragDropEffects.Move);
            else if (sender == tv_original)
                DoDragDrop(e.Item, DragDropEffects.Copy);
        }
        /// <summary>
        /// Set the drag effect.
        /// </summary>
        private void tv_DragEnter(object sender, DragEventArgs e)
        {
            TreeNode node = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if (node != null)
            {
                // icône de copy si la node provient de l'arbre original ou d'un bouton "Create"
                if (node.TreeView == tv_original || node.TreeView == null)
                    e.Effect = DragDropEffects.Copy;
                // icône de deplacement si elle provient du même arbre
                else if (node.TreeView == tv_final)
                    e.Effect = DragDropEffects.Move;
            }
            else
                e.Effect = DragDropEffects.None;
        }
        /// <summary>
        /// Action on drag-and-drop.
        /// </summary>
        private void tv_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode movedNode;
            TreeNodeCollection destination;
            int position = 0;

            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode DestinationNode = ((TreeView)sender).GetNodeAt(pt);
                movedNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                if (DestinationNode == movedNode || sender == tv_original) 
                    return;
                if(OverallTools.TreeViewFunctions.ContainsNode(movedNode,DestinationNode))
                    return;

                if (DestinationNode == null)
                    destination = tv_final.Nodes;
                else
                {
                    destination = DestinationNode.Nodes;
                    DestinationNode.Expand();
                }

                /// In case of moving a node in the final tree:
                /// __ It's removed from its parent list and add to the final position.
                if (sender == tv_final &&
                    movedNode.TreeView == tv_final)
                {
                    TreeNodeCollection nodes = tv_final.Nodes;
                    if (movedNode.Parent != null)
                        nodes = movedNode.Parent.Nodes; // "nodes" is the collection where we'll removed the node
                    // if the destination node is not a directory
                    if ((DestinationNode != null)&& ( DestinationNode.Tag != null && !(DestinationNode.Tag as TreeViewTag).isDirectoryNode))
                    {
                        if (DestinationNode.Parent != null)
                        {
                            destination = DestinationNode.Parent.Nodes;
                            if (DestinationNode.Parent == movedNode.Parent)
                            {
                                position = destination.IndexOf(DestinationNode) + 1;
                                if (DestinationNode.Parent == movedNode.Parent &&
                                    destination.IndexOf(DestinationNode) > destination.IndexOf(movedNode))
                                    position--;
                            }
                            else
                            {
                                position = destination.IndexOf(DestinationNode) + 1;
                            }
                        }
                        else
                            destination = tv_final.Nodes;
                    }
                    nodes.Remove(movedNode);
                    if (position == 0)
                        destination.Add(movedNode);
                    else
                        destination.Insert(position, movedNode);
                }

                /// In case of moving a node from the origanal tree to the final tree:
                /// __ the node is copied whith /ref CopyNodeInto.
                else if (sender == tv_final &&
                    movedNode.TreeView == tv_original)
                {
                    CopyNodeInto(movedNode, DestinationNode, destination, true);
                }
                else if (sender == tv_final && movedNode.TreeView == null)
                {
                    /// In case of moving a node from a create note button:
                    if (movedNode.Name == "note")
                    {
                        /// __ Launch the paragraph editor tool (\ref TreeEditor) in a separate window.
                        ParagraphEditor pe = new ParagraphEditor(Donnees);
                        pe.ShowDialog();

                        /// __ When it close, retrieve the paragraph name and add the
                        /// __ according node into the paragraph list and in the final tree.
                        String sParag = pe.ParagraphName;
                        if (sParag != null)
                        {
                            TreeNode noteNote = CreateParagraphNode(sParag);
                            CopyNodeInto(noteNote, DestinationNode, destination, false);
                        }
                    }
                    /// In case of moving a node from a create note button:
                    /// __ Add a folder.
                    else
                    {
                        TreeNode newNode = CopyNodeInto(movedNode, DestinationNode, destination, true);
                        tv_final.Focus();
                        tv_final.SelectedNode = newNode;
                        tv_final.LabelEdit = true;
                        newNode.BeginEdit();
                    }
                }
            }
        }
        #endregion

        #region Du TreeView "Report content" au bouton delete
        /// <summary>
        /// Determine si le Drag & drop et autorisé.
        /// Oui si l'objet deplacé est une TreeNode et qu'elle provient de l'arbre 
        /// "Report content".
        /// </summary>
        private void bt_delete_DragEnter(object sender, DragEventArgs e)
        {
            TreeNode node = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if (node != null && node.TreeView == tv_final)
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// Supprime le noeud qui a été glissé.
        /// </summary>
        private void bt_delete_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode node = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if (node == null)
                return;

            node.Remove();
        }
        #endregion

        #region De l'icône "Create directory" au TreeView "Report content"

        bool isDragging = false;

        private void DragDrop_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
        }

        private void DragDrop_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void DragDrop_MouseLeave(object sender, EventArgs e)
        {
            isDragging = false;
        }

        private void DragDrop_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                TreeNode node;

                if (sender == bt_CreateDirectory)
                    node = OverallTools.TreeViewFunctions.CreateDirectory("new directory");
                else
                    node = OverallTools.TreeViewFunctions.CreateDirectory("note");

                DoDragDrop(node, DragDropEffects.Copy);
            }
        }


        #endregion
        #endregion gestion du Drag-and-drop

        #region Pour la visualisation des elements
        /// <summary>
        /// Fonction pour la visualisation d'une table, d'une note ou d'un graphique.
        /// </summary>
        private void tsmiView_Click(object sender, EventArgs e)
        {
            TreeNode node = null;
            if (ActifTree != null)
                node = ActifTree.SelectedNode;
            else
                return;
            TreeViewTag Tag = null;
            if (node.Tag != null && node.Tag.GetType() == typeof(TreeViewTag))
                Tag = (TreeViewTag)node.Tag;

            // Si il s'agit d'un table
            if ((Tag.isTableNode) || (Tag.isResultNode) || (Tag.isFilterNode) || (Tag.IsExceptionNode && (!Tag.isParagraphNode) &&(!Tag.isChartNode)))
            {
                DataTable table = null;
                if (Tag.IsExceptionNode)
                {
                    DataManagement.NormalTable nt = Donnees.GetExceptionTable(Tag.ScenarioName, Tag.Name, Tag.ExceptionName);
                    if (nt != null) table = nt.Table;
                }
                else
                    table = Donnees.getTable(Tag.ScenarioName, Tag.Name);

                if (table != null)
                {
                    if (TableViewer == null)
                    {
                        TableViewer = new Prompt.IgnoredLigne(table, Tag.Name);
                        TableViewer.Show();
                    }
                    else
                    {
                        TableViewer.UpdateContent(table, "");
                    }
                    TableViewer.Text = "Table preview: " + node.Text;
                    TableViewer.Show();
                }
            }

            // Si il s'agit d'un graphique
            else if (Tag.isChartNode)
            {
                String imagePath = GetImagePath(node);
                //if (imageViewer == null)
                //{
                    imageViewer.Add(new Interface.Help("<img src=\"" + imagePath + "\">"));
                //}
                //else
                //{
                //    imageViewer.Update("<img src=\"" + imagePath + "\">");
                //}
                imageViewer[imageViewer.Count-1].Text = "Chart preview: " + node.Text;
            }

            // Si il s'agit d'une note
            else if (Tag.isParagraphNode)
            {
                String note = null;
                if (Tag.IsExceptionNode)
                {
                    note = Donnees.getExNote(Tag.ScenarioName, Tag.Name, Tag.ExceptionName);
                }
                else
                {
                    note = Donnees.getNote(Tag.ScenarioName, node.Name);
                    if (note == null)
                        note = Donnees.getGeneralNote(node.Name);
                }
                //>>GanttNote for Report
                // check what kind of Note does the Node hold(normal Note or Gantt Note) and view it
                if (note != null && Tag.ImageIndex != Model.GANTT_NOTE_IMAGE_INDEX)
                    NoteViewer.Add(new Interface.Help(note));
                else
                {
                    note = Donnees.getGanttNote(Tag.ScenarioName, node.Name);
                    if (note != null && Tag.ImageIndex == Model.GANTT_NOTE_IMAGE_INDEX)
                        NoteViewer.Add(new Interface.Help(note));
                    // >> Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports
                    note = Donnees.getDashboardNote(Tag.ScenarioName, node.Name);
                    if (note != null && Tag.ImageIndex == GlobalNames.DASHBOARD_NOTE_IMAGE_INDEX)
                    {
                        NoteViewer.Add(new Interface.Help(note));
                    }
                    // << Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports
                }
                if (note == null)
                    NoteViewer.Add(new Interface.Help(note));
                NoteViewer[NoteViewer.Count - 1].Text = "Chart preview: " + node.Text;
            }
        }

        /// <summary>
        /// Fonction qui génère le graphique associé à la node et l'enregistre dans un dossier temporaire.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private String GetImagePath(TreeNode Node)
        {
            TreeViewTag Tag = null;
            if (Node.Tag != null && Node.Tag.GetType() == typeof(TreeViewTag))
                Tag = (TreeViewTag)Node.Tag;
            if (imageTempPath == null)
                imageTempPath = OverallTools.ExternFunctions.getTempDirectoryForPax2sim() + "\\PreviewImages\\";

            OverallTools.ExternFunctions.CheckCreateDirectory(imageTempPath);
            String fileName = Tag.Name + Tag.ScenarioName + Tag.ExceptionName;
            Filter fFilter = Donnees.GetFilter(Node.Name);
            if ((Tag.ScenarioName == null || Tag.ScenarioName == "") && (Donnees.GetGeneralGraphicFilter(Node.Name) != null) && (fFilter == null))
            {
                fileName += "_Chart.png";
                // >> Report generate/preview error - Illegal char in path
                foreach (string illegalChar in GlobalNames.ILLEGAL_CHARACTERS_IN_FILE_PATH)
                {
                    if (fileName.Contains(illegalChar))
                        fileName = fileName.Replace(illegalChar, GlobalNames.SPECIAL_CHAR_UNDERSCORE);
                }                
                // Report generate/preview error - Illegal char in path
                Donnees.SaveGraphique(Node.Name, imageTempPath + fileName);
            }
            else
            {
                fileName += ".png";
                // >> Report generate/preview error - Illegal char in path
                foreach (string illegalChar in GlobalNames.ILLEGAL_CHARACTERS_IN_FILE_PATH)
                {
                    if (fileName.Contains(illegalChar))
                        fileName = fileName.Replace(illegalChar, GlobalNames.SPECIAL_CHAR_UNDERSCORE);
                }                
                // Report generate/preview error - Illegal char in path
                if (Tag.IsExceptionNode)
                    Donnees.SaveGraphique(Tag.ScenarioName, Tag.Name,Tag.ExceptionName, imageTempPath + fileName);
                else if (Donnees.getGraphicFilter(Tag.ScenarioName, Node.Name) != null)
                    Donnees.SaveGraphique(Tag.ScenarioName, Node.Name, imageTempPath + fileName);
                else
                    return null;
            }
            return imageTempPath + fileName;
        }
        #endregion

        #region Menu de suppression des tables
        /// <summary>
        /// Suppression de tables.
        /// </summary>
        private void tsmiRemove_Click(object sender, EventArgs e)
        {
            TreeNode node = tv_final.SelectedNode;
            TreeViewTag tag = null;
            if (node.Tag != null && node.Tag.GetType() == typeof(TreeViewTag))
                tag = (TreeViewTag)node.Tag;

            DialogResult result = MessageBox.Show("This action will remove some element in you report.", "Remove", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.No)
                return;

            int removed = 0;
            if (sender == this.tsmiRemoveBagPaxPlan)
            {
                removed += RemoveNamed(node, GlobalNames.BagPlanName, false);
                removed += RemoveNamed(node, "PaxPlanTable", false);
            }
            else if (sender == this.tsmiRemoveIST)
            {
                removed += RemoveNamed(node, "_IST", false);
            }
            else if (sender == this.tsmi_RemoveISTTable)
            {
                removed += RemoveNamed(node, "_IST", false,true);
            }
            else if (sender == this.tsmiRemoveDetails)
            {
                removed += RemoveNamed(node, "Details", true);
            }

            MessageBox.Show(removed.ToString() + " Element" + (removed > 1 ? "s" : "") + " found and removed.", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Fonction pour supprimer les nodes dont le nom est ou termine par 'endName'.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="endName"></param>
        /// <param name="folder"></param>
        /// <returns>le nombre de nodes supprimées</returns>
        private int RemoveNamed(TreeNode node, String endName, bool folder)
        {
            return RemoveNamed(node, endName, folder, false);
        }
        /// <summary>
        /// Fonction pour supprimer les nodes dont le nom est ou termine par 'endName'.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="endName"></param>
        /// <param name="folder"></param>
        /// <param name="bIgnoreCharts"></param>
        /// <returns>le nombre de nodes supprimées</returns>
        private int RemoveNamed(TreeNode node, String endName, bool folder, bool bIgnoreCharts)
        {
            TreeViewTag tag = null;
            if (node.Tag != null && node.Tag.GetType() == typeof(TreeViewTag))
                tag = (TreeViewTag)node.Tag;



            int nbRemoved = 0;
            if (node.Text.EndsWith(endName))
            {
                if (!bIgnoreCharts)
                {
                    nbRemoved = 1 + OverallTools.TreeViewFunctions.CountNodes(node.Nodes);
                    node.Remove();
                }
                else
                {
                    //On doit parcourir l'intégralité des noeuds pour savoir si on conserve le contenu ou pas.
                    if (!tag.isChartNode)
                    {
                        if (node.Nodes.Count>0)
                        {
                            foreach (TreeNode child in node.Nodes)
                                nbRemoved += RemoveNamed(child, endName, folder, bIgnoreCharts);
                        }
                        if (node.Nodes.Count == 0)
                        {
                            nbRemoved += 1;
                            node.Remove();
                        }
                    }
                }
            }
            else
            {
                if (node.Nodes != null)
                {
                    foreach (TreeNode child in node.Nodes)
                        nbRemoved += RemoveNamed(child, endName, folder, bIgnoreCharts);
                }
            }

            return nbRemoved;
        }
        #endregion Menu de suppression des tables  

        /// <summary>
        /// Retrieve the node collection created.
        /// </summary>
        public TreeNode[] TreeNodes
        {
            get 
            {
                TreeNode[] nodes = new TreeNode[tv_final.Nodes.Count];
                int i = 0;
                foreach (TreeNode tn in tv_final.Nodes)
                    nodes[i++] = tn;
                tv_final.Nodes.Clear();
                return nodes; 
            }
        }

        /// <summary>
        /// Close the window on "ok" button clic.
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            // decolor la node precedement selectionné
            if (tv_final.SelectedNode != null)
                ColorNode(tv_final.SelectedNode, false);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// Close the window on "Cancel" button clic.
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Fonction qui lance une action lors d'un double clic.
        /// Edition si le double clic concerne un paragraphe.
        /// Rien si il s'agit d'un dossier.
        /// Visualisation pour tout autre element.
        /// </summary>
        private void tv_DoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            TreeView tv = ActifTree;
            if (tv == null || tv.SelectedNode == null) return;
            TreeNode node = tv.SelectedNode;
            bool bDone = false; // Definie si une action a était effectué

            TreeViewTag tag = node.Tag as TreeViewTag;
            if (tag != null && tag.isParagraphNode)
            {
                SIMCORE_TOOL.GestionDonneesHUB2SIM.Paragraph p = null;
                if ((p = Donnees.getParagraph(node.Name)) != null) // It's a paragraph
                {
                    EditParagraph(node);
                    bDone = true;
                }
            }

            if (!bDone)
            {
                tsmiView_Click(null, null);
            }
        }

        /// <summary>
        /// Force the tool tip to display message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tv_final_MouseMove(object sender, MouseEventArgs e)
        {
            TreeNode node = this.tv_final.GetNodeAt(e.X, e.Y);
            if ((node != null))
            {
                if (node.Tag.ToString() != this.toolTip1.GetToolTip(this.tv_final))
                {
                    DefineToolTipText(node);
                    //this.toolTip1.SetToolTip(this.tv_final, DefineToolTipText(node));
                }
            }
            else 
            {
                this.toolTip1.SetToolTip(this.tv_final, "");
            }
        }

        /// <summary>
        /// If node is a chart or a table it return the Scenario name.
        /// </summary>
        private String DefineToolTipText(TreeNode node)
        {
            TreeViewTag tag = node.Tag as TreeViewTag;
            StringBuilder str = new StringBuilder();

            /// set type
            if (tag.isChartNode)
            {
                str.Append("Chart");
            }
            else if (tag.isFilterNode || tag.isResultNode || tag.isTableNode)
            {
                str.Append("Table");
            }
            else if (tag.isParagraphNode)
            {
                str.Append("Note");
            }
            else if (tag.isDirectoryNode)
            {
                str.Append("Chapter");
            }
            else
            {
                str.Append("Element");
            }

            /// set node location
            if (tag.ScenarioName == "Input")
            {
                str.Append(" from \"Input Data\"");
            }
            else if (tag.ScenarioName == "Charts")
            {
                str.Append(" from \"Charts\"");
            }
            else if (tag.ScenarioName != null && tag.ScenarioName != "")
            {
                str.Append(" from scenario: \"" + tag.ScenarioName + "\"");
            }

            /// other info
            if (tag.isParagraphNode)
            {
                //Paragraph p = null;
                String note = null;
                String ganttNote = null;    //>>GanttNote for Report
                if (Donnees.getParagraph(node.Name) != null)
                {
                    note = Donnees.getParagraph(node.Name).Content;
                }
                else if ((note = Donnees.getNote(tag.ScenarioName, node.Name)) != null ||
                        (note = Donnees.getGeneralNote(node.Name)) != null ||
                        (note = Donnees.getExNote(tag.ScenarioName, tag.Name, tag.ExceptionName)) != null)
                {
                    ;
                }
                str.Append("\nContent: \"");
                //str.Append(System.Web.HttpUtility.HtmlDecode(note.Substring(0, 200)));
                ganttNote = Donnees.getGanttNote(tag.ScenarioName, node.Name);      //>>GanttNote for Report
                // use the Gantt Note if the Node holds the Gantt image
                if (ganttNote != null && tag.ImageIndex == Model.GANTT_NOTE_IMAGE_INDEX)
                    note = ganttNote;

                // >> Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports
                if (tag.ImageIndex == GlobalNames.DASHBOARD_NOTE_IMAGE_INDEX
                    && Donnees.getDashboardNote(tag.ScenarioName, node.Name) != null)
                {
                    note = Donnees.getDashboardNote(tag.ScenarioName, node.Name);
                }
                // << Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports

                if (note != null)
                {
                    int previewLength = note.Length < 400 ? note.Length : 400;
                    str.Append(OverallTools.FonctionUtiles.HTMLToPlainText(note.Substring(0, previewLength)));
                    if (previewLength < 400)
                        str.Append("\"");
                    else
                        str.Append("... \"");
                }
            }

            node.ToolTipText = str.ToString();
            return str.ToString();
        }

        /// <summary>
        /// Fonction pour renomer un dossier de l'arbre final.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRename_Click(object sender, EventArgs e)
        {
            TreeNode node = tv_final.SelectedNode;
            TreeViewTag tag = null;
            if (node.Tag != null && node.Tag.GetType() == typeof(TreeViewTag))
                tag = (TreeViewTag)node.Tag;

            if (tag.isDirectoryNode)
            {
                node.BeginEdit();
            }
        }

        #region Pour fermer automatiquement les ToolStripMenu
        /// <summary>
        /// Fonction qui se charge de fermer le ContextMenuStrip lorsque la sourie quitte le control.
        /// </summary>
        private void cms_MouseLeave(object sender, EventArgs e)
        {
            ContextMenuStrip cms = sender as ContextMenuStrip;
            if (cms == null)
                return;
            Rectangle rec = cms.DisplayRectangle;

            Point LimiteTopLeft = cms.PointToScreen(new Point(0, 0));
            Point LimiteBottomRight = cms.PointToScreen(new Point(rec.Size.Width, rec.Size.Height));

            if (MousePosition.X < LimiteTopLeft.X || 
                MousePosition.X > LimiteBottomRight.X ||
                MousePosition.Y < LimiteTopLeft.Y ||
                MousePosition.Y > LimiteBottomRight.Y)
                cms.Close();
        }
        #endregion

        private void TreeEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (TableViewer != null)
            {
                TableViewer.Close();
                TableViewer = null;
            }
            if (NoteViewer != null)
            {
                foreach (Interface.Help hp in NoteViewer)
                {
                    hp.Close();
                }
            }
            if (imageViewer != null)
            {
                foreach (Interface.Help hp in imageViewer)
                {
                    hp.Close();
                }
                imageViewer = null;
            }
        }

    }
}
