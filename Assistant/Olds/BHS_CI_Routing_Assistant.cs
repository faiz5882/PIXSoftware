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
    public partial class BHS_CI_Routing_Assistant : Form
    {
        private DataTable dtBHS_CIRoutingTable;
        private int GroupSize = 30;
        private int iNbGroup = 5;
        private int iTerminal;
        private bool bTransfer_;

        private Label[] lbl_Group;
        private ComboBox[] cb_HBS1Start;
        private ComboBox[] cb_HBS1End;
        private ComboBox[] cb_HBS3Start;
        private ComboBox[] cb_HBS3End;
        private ComboBox[] cb_MESStart;
        private ComboBox[] cb_MESEnd;
        private ComboBox[] cb_MUStart;
        private ComboBox[] cb_MUEnd;

        private TreeNode tnStructureAeroport;

        private ArrayList alHBS1;
        private ArrayList alHBS3;
        private ArrayList alMES;
        private ArrayList alMU;

        private void Initialize(DataTable dtBHS_CIRoutingTable_,TreeNode tnStructureAeroport_, bool bTransfer)
        {
            InitializeComponent();
            dtBHS_CIRoutingTable = dtBHS_CIRoutingTable_;
            tnStructureAeroport = tnStructureAeroport_;
            bTransfer_ = bTransfer;

            iTerminal = GestionDonneesHUB2SIM.AnalyseTerminalName(dtBHS_CIRoutingTable_.TableName);
            iNbGroup = dtBHS_CIRoutingTable_.Rows.Count;

            OverallTools.FonctionUtiles.MajBackground(this);
            //p_Content.BackgroundImage = OverallTools.FonctionUtiles.dessinerFondEcran(300, GroupSize * (iNbGroup + 1), PAX2SIM.Couleur1, PAX2SIM.Couleur2, PAX2SIM.Angle);

            /*Recherche du terminal.*/
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
                alHBS1 = new ArrayList();
                alHBS3 = new ArrayList();
                alMES = new ArrayList();
                alMU = new ArrayList();
                foreach (TreeNode tnChild in tnTerminal.Nodes)
                {
                    if (tnChild.Tag.GetType() != typeof(TreeViewTag))
                        continue;
                    if (!((TreeViewTag)tnChild.Tag).isBHS)
                        continue;
                    foreach (TreeNode tnChildChild in tnChild.Nodes)
                    {
                        if (tnChildChild.Tag.GetType() != typeof(TreeViewTag))
                            continue;
                        switch (((TreeViewTag)tnChildChild.Tag).AirportObjectType)
                        {
                            case "HBS Lev1 Group":
                                fillArrayList(alHBS1,tnChildChild);
                                break;
                            case "HBS Lev3 Group":
                                fillArrayList(alHBS3,tnChildChild);
                                break;
                            case "Make-Up Group":
                                fillArrayList(alMU,tnChildChild);
                                break;
                            case "MES Group":
                                fillArrayList(alMES, tnChildChild);
                                break;
                        }
                    }
                } 
                alHBS1.Sort();
                alHBS3.Sort();
                alMES.Sort();
                alMU.Sort();
            }
            LoadTable();
        }
        private void fillArrayList(ArrayList alList, TreeNode tnNode)
        {
            foreach (TreeNode tnChild in tnNode.Nodes)
            {
                if (tnChild.Tag.GetType() != typeof(TreeViewTag))
                    continue;
                alList.Add(((TreeViewTag)tnChild.Tag).Index);
            }
        }


        private void LoadTable()
        {
            this.Text = "Modify " + dtBHS_CIRoutingTable.TableName;

            lbl_Group = new Label[iNbGroup];
            cb_HBS1Start = new ComboBox[iNbGroup];
            cb_HBS1End = new ComboBox[iNbGroup];
            cb_HBS3Start = new ComboBox[iNbGroup];
            cb_HBS3End = new ComboBox[iNbGroup];
            cb_MESStart = new ComboBox[iNbGroup];
            cb_MESEnd = new ComboBox[iNbGroup];
            cb_MUStart = new ComboBox[iNbGroup];
            cb_MUEnd = new ComboBox[iNbGroup];
            int i;
            for(i=0;i<dtBHS_CIRoutingTable.Rows.Count;i++)
            {
                InitializeCollector(i, dtBHS_CIRoutingTable.Rows[i]);
            }

            p_Content.ResumeLayout(false);
            p_Content.PerformLayout();
        }
        private void InitializeCollector(int iIndex, DataRow drLine)
        {
            /// Le label
            lbl_Group[iIndex] = new Label();
            lbl_Group[iIndex].BackColor = Color.Transparent;
            lbl_Group[iIndex].AutoSize = true;
            lbl_Group[iIndex].Location = new System.Drawing.Point(20, 17 + iIndex * GroupSize);
            lbl_Group[iIndex].Name = "lbl_Coll" + iIndex.ToString();
            lbl_Group[iIndex].TabIndex = iIndex * 3;
            if (bTransfer_)
            {
                lbl_Group[iIndex].Text = "Transfer Infeed Group " + drLine[0].ToString();
            }
            else
            {
                lbl_Group[iIndex].Text = "Check-In Group " + drLine[0].ToString();
            }

            p_Content.Controls.Add(lbl_Group[iIndex]);

            ///HBS1
            cb_HBS1Start[iIndex] = new ComboBox();
            cb_HBS1End[iIndex] = new ComboBox();
            InitializeDesks(cb_HBS1Start[iIndex], cb_HBS1End[iIndex],alHBS1, (int)drLine[1], (int)drLine[2], 0,iIndex);
            p_Content.Controls.Add(cb_HBS1Start[iIndex]);
            p_Content.Controls.Add(cb_HBS1End[iIndex]);
            ///HBS3
            cb_HBS3Start[iIndex] = new ComboBox();
            cb_HBS3End[iIndex] = new ComboBox();
            InitializeDesks(cb_HBS3Start[iIndex], cb_HBS3End[iIndex], alHBS3, (int)drLine[3], (int)drLine[4], 1, iIndex);
            p_Content.Controls.Add(cb_HBS3Start[iIndex]);
            p_Content.Controls.Add(cb_HBS3End[iIndex]);
            ///MES
            cb_MESStart[iIndex] = new ComboBox();
            cb_MESEnd[iIndex] = new ComboBox();
            InitializeDesks(cb_MESStart[iIndex], cb_MESEnd[iIndex], alMES, (int)drLine[5], (int)drLine[6], 2, iIndex);
            p_Content.Controls.Add(cb_MESStart[iIndex]);
            p_Content.Controls.Add(cb_MESEnd[iIndex]);
            ///MU
            cb_MUStart[iIndex] = new ComboBox();
            cb_MUEnd[iIndex] = new ComboBox();
            InitializeDesks(cb_MUStart[iIndex], cb_MUEnd[iIndex], alMU, (int)drLine[7], (int)drLine[8], 3, iIndex);
            p_Content.Controls.Add(cb_MUStart[iIndex]);
            p_Content.Controls.Add(cb_MUEnd[iIndex]);

        }
        private void InitializeDesks(ComboBox cbStart, ComboBox cbEnd,ArrayList alList, int iStartValue, int iEndValue, int iLocation, int iIndex)
        {

            cbStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbStart.FormattingEnabled = true;
            cbStart.Location = new System.Drawing.Point(153 + iLocation * 180, 15 + iIndex * GroupSize);
            cbStart.Name = "cb_Start_" +iLocation.ToString()+"_"+ iIndex.ToString();
            cbStart.Size = new System.Drawing.Size(64, 21);
            cbStart.TabIndex = iIndex * 8 + 1 + iLocation *2;
            InitializeCombox(cbStart,alList, 0, iStartValue);

            cbEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbEnd.FormattingEnabled = true;
            cbEnd.Location = new System.Drawing.Point(230 + iLocation*180, 15 + iIndex * GroupSize);
            cbEnd.Name = "cb_End_" + iLocation.ToString() + "_" + iIndex.ToString();
            cbEnd.Size = new System.Drawing.Size(64, 21);
            cbEnd.TabIndex = iIndex * 8 + 2 + iLocation * 2;
            InitializeCombox(cbEnd, alList, 0, iEndValue);
        }

        private static void InitializeCombox(ComboBox cbTmp,ArrayList alList, int iStart, int iOldValue)
        {
            cbTmp.Items.Clear();
            cbTmp.Text = "";
            cbTmp.Items.Add("");
            int iIndex=-1;
            foreach (int iValue in alList)
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
            for (int i = 0; i < cb_HBS1Start.Length; i++)
            {
                int iStart, iEnd;
                Int32.TryParse(cb_HBS1Start[i].Text, out iStart);
                Int32.TryParse(cb_HBS1End[i].Text, out iEnd);
                dtBHS_CIRoutingTable.Rows[i][1] = iStart;
                dtBHS_CIRoutingTable.Rows[i][2] = iEnd;
                Int32.TryParse(cb_HBS3Start[i].Text, out iStart);
                Int32.TryParse(cb_HBS3End[i].Text, out iEnd);
                dtBHS_CIRoutingTable.Rows[i][3] = iStart;
                dtBHS_CIRoutingTable.Rows[i][4] = iEnd;
                Int32.TryParse(cb_MESStart[i].Text, out iStart);
                Int32.TryParse(cb_MESEnd[i].Text, out iEnd);
                dtBHS_CIRoutingTable.Rows[i][5] = iStart;
                dtBHS_CIRoutingTable.Rows[i][6] = iEnd;
                Int32.TryParse(cb_MUStart[i].Text, out iStart);
                Int32.TryParse(cb_MUEnd[i].Text, out iEnd);
                dtBHS_CIRoutingTable.Rows[i][7] = iStart;
                dtBHS_CIRoutingTable.Rows[i][8] = iEnd;
            }
            dtBHS_CIRoutingTable.AcceptChanges();
        }

        public BHS_CI_Routing_Assistant(DataTable dtBHS_CIRoutingTable_, TreeNode tnStructureAeroport_, bool bTransfer)
        {
            Initialize(dtBHS_CIRoutingTable_, tnStructureAeroport_, bTransfer);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            bool bInvalid = false;
            ComboBox cbTmp = null;
            for (int i = 0; i < cb_HBS1Start.Length; i++)
            {
                if (cb_HBS1Start[i].Text == "")
                {
                    bInvalid = true;
                    cbTmp = cb_HBS1Start[i];
                    break;
                }
                if (cb_HBS1End[i].Text == "")
                {
                    bInvalid = true;
                    cbTmp = cb_HBS1End[i];
                    break;
                }
                if (cb_HBS1Start[i].Text == "")
                {
                    bInvalid = true;
                    cbTmp = cb_HBS1Start[i];
                    break;
                }
                if (cb_HBS3End[i].Text == "")
                {
                    bInvalid = true;
                    cbTmp = cb_HBS3End[i];
                    break;
                }
                if (cb_MESStart[i].Text == "")
                {
                    bInvalid = true;
                    cbTmp = cb_MESStart[i];
                    break;
                }
                if (cb_MESEnd[i].Text == "")
                {
                    bInvalid = true;
                    cbTmp = cb_MESEnd[i];
                    break;
                }
                if (cb_MUStart[i].Text == "")
                {
                    bInvalid = true;
                    cbTmp = cb_MUStart[i];
                    break;
                }
                if (cb_MUEnd[i].Text == "")
                {
                    bInvalid = true;
                    cbTmp = cb_MUEnd[i];
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

    }
}