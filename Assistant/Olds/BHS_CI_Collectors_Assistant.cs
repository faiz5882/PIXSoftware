using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SIMCORE_TOOL.Assistant
{
    public partial class BHS_CI_Collectors_Assistant : Form
    {
        private DataTable dtBHS_CICollTable;
        private int GroupSize = 30;
        private int iNbCollectors = 5;
        private Label[] lbl_Collectors;
        private ComboBox[] cb_CheckInStart;
        private ComboBox[] cb_CheckInEnd;
        private TreeNode tnStructureAeroport;
        private ArrayList htCheckIn;
        private int iTerminal;

        private void Initialize(DataTable dtBHS_CICollTable_,TreeNode tnStructureAeroport_)
        {
            InitializeComponent();
            dtBHS_CICollTable = dtBHS_CICollTable_;
            tnStructureAeroport = tnStructureAeroport_;

            iTerminal = GestionDonneesHUB2SIM.AnalyseTerminalName(dtBHS_CICollTable_.TableName);
            iNbCollectors = dtBHS_CICollTable_.Rows.Count;

            OverallTools.FonctionUtiles.MajBackground(this);
            //p_Content.BackgroundImage = OverallTools.FonctionUtiles.dessinerFondEcran(300, GroupSize * (iNbCollectors + 1), PAX2SIM.Couleur1, PAX2SIM.Couleur2, PAX2SIM.Angle);

            TreeNode tnTerminal = null;
            if ((tnStructureAeroport.Tag.GetType() == typeof(TreeViewTag)) && (!((TreeViewTag)tnStructureAeroport.Tag).isTerminal))
            {
                foreach (TreeNode tnChild in tnStructureAeroport.Nodes)
                {
                    if ((tnChild.Tag.GetType() == typeof(TreeViewTag)) && (((TreeViewTag)tnChild.Tag).isTerminal))
                    {
                        if (((TreeViewTag)tnChild.Tag).Index == iTerminal)
                        {
                            tnTerminal = tnChild;
                            break;
                        }
                    }
                }
            }
            tnStructureAeroport = tnTerminal;
            if (tnStructureAeroport != null)
            {
                //On remplit la table de correspondance.
                htCheckIn = new ArrayList();
                foreach (TreeNode tnChild in tnTerminal.Nodes)
                {
                    if (tnChild.Tag.GetType() != typeof(TreeViewTag))
                        continue;
                    if (!((TreeViewTag)tnChild.Tag).isLevel)
                        continue;
                    foreach (TreeNode tnChildChild in tnChild.Nodes)
                    {
                        if (tnChildChild.Tag.GetType() != typeof(TreeViewTag))
                            continue;
                        if (((TreeViewTag)tnChildChild.Tag).AirportObjectType != PAX2SIM.sCheckInGroup)
                            continue;
                        foreach (TreeNode tnCheckIn in tnChildChild.Nodes)
                        {
                            if (tnCheckIn.Tag.GetType() != typeof(TreeViewTag))
                                continue;
                            htCheckIn.Add(((TreeViewTag)tnCheckIn.Tag).Index);
                        }
                    }
                }
                htCheckIn.Sort();
            }


            LoadTable();
        }

        private void LoadTable()
        {
            this.Text = "Modify " + dtBHS_CICollTable.TableName;

            lbl_Collectors = new Label[iNbCollectors];
            cb_CheckInStart = new ComboBox[iNbCollectors];
            cb_CheckInEnd = new ComboBox[iNbCollectors];
            int i;
            for(i=0;i<dtBHS_CICollTable.Rows.Count;i++)
            {
                InitializeCollector(i, dtBHS_CICollTable.Rows[i]);
            }

            p_Content.ResumeLayout(false);
            p_Content.PerformLayout();
        }
        private void InitializeCollector(int iIndex, DataRow drLine)
        {
            /// Le label
            lbl_Collectors[iIndex] = new Label();
            lbl_Collectors[iIndex].BackColor = Color.Transparent;
            lbl_Collectors[iIndex].AutoSize = true;
            lbl_Collectors[iIndex].Location = new System.Drawing.Point(20, 17 + iIndex * GroupSize);
            lbl_Collectors[iIndex].Text = "Check-In Collector " + drLine[0].ToString();
            lbl_Collectors[iIndex].Name = "lbl_Coll" + iIndex.ToString();
            lbl_Collectors[iIndex].TabIndex = iIndex * 3;

            ///Le combo box pour l'index de début.
            cb_CheckInStart[iIndex] = new ComboBox();
            cb_CheckInStart[iIndex].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cb_CheckInStart[iIndex].FormattingEnabled = true;
            cb_CheckInStart[iIndex].Location = new System.Drawing.Point(151, 15 + iIndex * GroupSize);
            cb_CheckInStart[iIndex].Name = "cb_Start_" + iIndex.ToString();
            cb_CheckInStart[iIndex].Size = new System.Drawing.Size(64, 21);
            cb_CheckInStart[iIndex].TabIndex = iIndex * 3 + 1;
            InitializeCombox(cb_CheckInStart[iIndex], 0, (int)drLine[1]);

            cb_CheckInEnd[iIndex] = new ComboBox();
            cb_CheckInEnd[iIndex].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cb_CheckInEnd[iIndex].FormattingEnabled = true;
            cb_CheckInEnd[iIndex].Location = new System.Drawing.Point(258, 15 + iIndex * GroupSize);
            cb_CheckInEnd[iIndex].Name = "cb_End_" + iIndex.ToString();
            cb_CheckInEnd[iIndex].Size = new System.Drawing.Size(64, 21);
            cb_CheckInEnd[iIndex].TabIndex = iIndex * 3 + 2;
            InitializeCombox(cb_CheckInEnd[iIndex], 0, (int)drLine[2]);

            p_Content.Controls.Add(lbl_Collectors[iIndex]);
            p_Content.Controls.Add(cb_CheckInStart[iIndex]);
            p_Content.Controls.Add(cb_CheckInEnd[iIndex]);
        }

        private void InitializeCombox(ComboBox cbTmp, int iStart, int iOldValue)
        {
            cbTmp.Items.Clear();
            cbTmp.Text = "";
            cbTmp.Items.Add("");
            int iIndex=-1;
            foreach (int iValue in htCheckIn)
            {
                if (iValue <= iStart)
                    continue;
                cbTmp.Items.Add(iValue);
                if (iValue == iOldValue)
                    iIndex = cbTmp.Items.Count - 1;
            }
            if (iIndex != -1)
                cbTmp.SelectedIndex = iIndex;
        }

        private void SaveTable()
        {
            for (int i = 0; i < cb_CheckInStart.Length; i++)
            {
                int iStart, iEnd;
                Int32.TryParse(cb_CheckInStart[i].Text, out iStart);
                Int32.TryParse(cb_CheckInEnd[i].Text, out iEnd);
                dtBHS_CICollTable.Rows[i][1] = iStart;
                dtBHS_CICollTable.Rows[i][2] = iEnd;
            }
            dtBHS_CICollTable.AcceptChanges();
        }

        public BHS_CI_Collectors_Assistant(DataTable dtBHS_CICollTable_, TreeNode tnStructureAeroport_)
        {
            Initialize(dtBHS_CICollTable_, tnStructureAeroport_);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            bool bInvalid = false;
            ComboBox cbTmp = null;
            for (int i = 0; i < cb_CheckInStart.Length; i++)
            {
                if (cb_CheckInStart[i].Text == "")
                {
                    bInvalid = true;
                    cbTmp = cb_CheckInStart[i];
                    break;
                }
                if (cb_CheckInEnd[i].Text == "")
                {
                    bInvalid = true;
                    cbTmp = cb_CheckInEnd[i];
                    break;
                }
            }
            if (bInvalid)
            {
                if (MessageBox.Show("The table contains some errors, do you wich to continue?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    cbTmp.Focus();
                    DialogResult = DialogResult.None;
                    return;
                }
            }
            SaveTable();
        }

        private void BHS_CI_Collectors_Assistant_Load(object sender, EventArgs e)
        {

        }

    }
}