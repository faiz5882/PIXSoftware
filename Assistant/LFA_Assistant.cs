using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant
{
    public partial class LFA_Assistant :Form
    {
        #region Initialisation de la classe.
        DataTable LFTable;
        String ancienneSelection;
        bool bAModifieTable;
        bool bAModifieValeurs;
        public bool AModifieTable
        {
            get
            {
                return bAModifieTable;
            }
        }
        private void Initialize(DataTable LFTable_)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            tb_LoadFactor.BackColor = Color.White;
            tb_FirstClass.BackColor = Color.White;
            tb_Local.BackColor = Color.White;
            tb_Terminating.BackColor = Color.White;
            tb_Transfer.BackColor = Color.White;
            tb_ReCheck.BackColor = Color.White;
            txt_NotLocal.BackColor = Color.White;
            txt_SecondClass.BackColor =Color.White;
            txt_Transferring.BackColor = Color.White;
            txt_OOGTransf.BackColor = Color.White;
            txt_OOGTerm.BackColor = Color.White;
            txt_PaxPerCar.BackColor = Color.White;

            tb_LoadFactor.Text = "  0.00";
            tb_FirstClass.Text = "  0.00";
            tb_Local.Text = "  0.00";
            tb_Terminating.Text = "  0.00";
            tb_Transfer.Text = "  0.00";
            tb_ReCheck.Text = "  0.00";
            txt_OOGTransf.Text = "  0.00";
            txt_PaxPerCar.Text = "  1.00";  // AndreiZaharia / Mulhouse project 

            txt_NotLocal.BackColor = Color.White;
            txt_SecondClass.BackColor = Color.White;
            txt_Transferring.BackColor = Color.White;
            LFTable = LFTable_;
            if (LFTable.Columns.Count != 1)
            {
                foreach (DataColumn colonne in LFTable.Columns)
                {
                    if (colonne.ColumnName != "Select a category")
                    {
                        cb_FC.Items.Add(colonne.ColumnName);
                    }
                }
            }
            ancienneSelection = "";
            bAModifieTable = false;
            bAModifieValeurs = false;
        }
        public LFA_Assistant(DataTable LFtable_)
        {
            Initialize(LFtable_);
            setFlightCategorie(cb_FC.Items[0].ToString());
        }
        public LFA_Assistant(DataTable LFtable_, String FC)
        {
            Initialize(LFtable_);
            setFlightCategorie(FC);
        }
        #endregion

        #region Fonction pour valider le contenu d'un masqued text box.
        private void Poucentage_Validating(object sender, CancelEventArgs e)
        {
            Double dValue = ParsePourcentage(((TextBox)sender).Text);

            if ((dValue < 0) || ((dValue > 100) && ( (((TextBox)sender).Name!= tb_LoadFactor.Name) && (((TextBox)sender).Name!= txt_PaxPerCar.Name)  ) ))  // AndreiZaharia / Mulhouse project
            {
                MessageBox.Show("You must select a value between 0% and 100%", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
            bAModifieValeurs = true;
        }
        private double ParsePourcentage(String pourcentage)
        {
            Double dvalue;
            if (!Double.TryParse(pourcentage, out dvalue))
            {
                return -1;
            }
            return dvalue;
        }
        #endregion

        #region Gestion des balances
        private void mtb_FirstClass_TextChanged(object sender, EventArgs e)
        {
            Double value = ParsePourcentage(tb_FirstClass.Text);
            if (value == -1)
                txt_SecondClass.Text = "0";
            else
                txt_SecondClass.Text = ((Double)(100.0 - value)).ToString() + "%";
        }

        private void mtb_Terminating_TextChanged(object sender, EventArgs e)
        {
            Double value = ParsePourcentage(tb_Terminating.Text);
            if (value == -1)
                txt_Transferring.Text = "0";
            else
                txt_Transferring.Text = ((Double)(100.0 - value)).ToString() + "%";
        }

        private void mtb_Local_TextChanged(object sender, EventArgs e)
        {
            Double value = ParsePourcentage(tb_Local.Text);
            if (value == -1)
                txt_NotLocal.Text = "0";
            else
                txt_NotLocal.Text = ((Double)(100.0 - value)).ToString() + "%";

        }
        #endregion

        #region Fonctions pour charger les données d'une catégorie de vol.
        private void setFlightCategorie(String FC)
        {
            if (!LFTable.Columns.Contains(FC))
                return;
            ancienneSelection = FC;
            cb_FC.Text = FC;
            int iIndex =OverallTools.DataFunctions.indexLigne(LFTable, 0, GlobalNames.sLFD_A_Line_Full);
            if(iIndex== -1)
                return;
            DataRow ligne = LFTable.Rows[iIndex];
            Double dFull = (Double)ligne[FC];
            tb_LoadFactor.Text = /*ConvertToPercent(*/dFull.ToString()/*)*/;


            iIndex = OverallTools.DataFunctions.indexLigne(LFTable, 0, GlobalNames.sLFD_A_Line_C);
            if (iIndex == -1)
                return;
            ligne = LFTable.Rows[iIndex];
            Double dC = (Double)ligne[FC];
            tb_FirstClass.Text = /*ConvertToPercent(*/dC.ToString()/*)*/;
            iIndex = OverallTools.DataFunctions.indexLigne(LFTable, 0, GlobalNames.sLFD_A_Line_Y);
            if (iIndex == -1)
                return;
            ligne = LFTable.Rows[iIndex];
            Double dY = (Double)ligne[FC];


            iIndex = OverallTools.DataFunctions.indexLigne(LFTable, 0, GlobalNames.sLFD_A_Line_Local);
            if (iIndex == -1)
                return;
            ligne = LFTable.Rows[iIndex];
            Double dLocal = (Double)ligne[FC];
            tb_Local.Text = /*ConvertToPercent(*/dLocal.ToString()/*)*/;
            iIndex = OverallTools.DataFunctions.indexLigne(LFTable, 0, GlobalNames.sLFD_A_Line_NotLocal);
            if (iIndex == -1)
                return;
            ligne = LFTable.Rows[iIndex];
            Double dNotLocal = (Double)ligne[FC];



            iIndex = OverallTools.DataFunctions.indexLigne(LFTable, 0, GlobalNames.sLFA_Line_Terminating);
            if (iIndex == -1)
            {
                return;
            }
            ligne = LFTable.Rows[iIndex];
            Double dTerminating = (Double)ligne[FC];
            tb_Terminating.Text = /*ConvertToPercent(*/dTerminating.ToString()/*)*/;
            iIndex = OverallTools.DataFunctions.indexLigne(LFTable, 0, GlobalNames.sLFD_A_Line_Transferring);
            if (iIndex == -1)
                return;
            ligne = LFTable.Rows[iIndex];
            Double dTransferring = (Double)ligne[FC];



            iIndex = OverallTools.DataFunctions.indexLigne(LFTable, 0, GlobalNames.sLFD_A_Line_ReCheck);
            if (iIndex == -1)
            {
                return;
            }
            ligne = LFTable.Rows[iIndex];
            Double dReCheck = (Double)ligne[FC];
            tb_ReCheck.Text = /*ConvertToPercent(*/dReCheck.ToString()/*)*/;



            iIndex = OverallTools.DataFunctions.indexLigne(LFTable, 0, GlobalNames.sLFD_A_Line_TransferDesk);
            if (iIndex == -1)
            {
                return;
            }
            ligne = LFTable.Rows[iIndex];
            Double dTransfer = (Double)ligne[FC];
            tb_Transfer.Text = /*ConvertToPercent(*/dTransfer.ToString()/*)*/;



            iIndex = OverallTools.DataFunctions.indexLigne(LFTable, 0, GlobalNames.sLFD_A_Line_OOGTransf);
            if (iIndex == -1)
            {
                return;
            }
            ligne = LFTable.Rows[iIndex];
            Double dOOG = (Double)ligne[FC];
            txt_OOGTransf.Text = dOOG.ToString();

            iIndex = OverallTools.DataFunctions.indexLigne(LFTable, 0, GlobalNames.sLFA_Line_OOGTerm);
            if (iIndex == -1)
            {
                return;
            }
            ligne = LFTable.Rows[iIndex];
            dOOG = (Double)ligne[FC];
            txt_OOGTerm.Text = dOOG.ToString();

            //<< AndreiZaharia / Mulhouse project
            iIndex = OverallTools.DataFunctions.indexLigne(LFTable, 0, GlobalNames.sLFD_A_Line_NbPaxPerCar);
            if (iIndex == -1)
            {
                return;
            }
            ligne = LFTable.Rows[iIndex];
            Double dPaxPerCar = (Double)ligne[FC];
            txt_PaxPerCar.Text = dPaxPerCar.ToString();
            //>> AndreiZaharia / Mulhouse project

            bAModifieValeurs = false;
            if (!OverallTools.TableCheck.CheckArrivalLoadFactors(
               dFull,
               dC,dY,dLocal,dNotLocal,dTerminating,dTransferring,dReCheck,dTransfer, dPaxPerCar, 0, "", "",  null, null))   // AndreiZaharia / Mulhouse project
            {
                bAModifieValeurs = true;
            }
        }
        private String ConvertToPercent(String sValue)
        {
            Double Value;
            if (!Double.TryParse(sValue, out Value))
            {
                return "0";
            }
            Value *= 100;
            return Value.ToString();
        }
        #endregion

        #region Les différentes interaction disponibles.
        private void cb_FC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CheckDatas())
                return;
            setFlightCategorie(cb_FC.Text);
        }
        private Boolean CheckDatas()
        {

            if (!OverallTools.TableCheck.CheckArrivalLoadFactors(
                Math.Round(ParsePourcentage(tb_LoadFactor.Text) /*/ 100*/, 4),
                Math.Round(ParsePourcentage(tb_FirstClass.Text) /*/ 100*/, 4),
                Math.Round(100 - (ParsePourcentage(tb_FirstClass.Text) /*/ 100*/), 4),
                Math.Round(ParsePourcentage(tb_Local.Text) /*/ 100*/, 4),
                Math.Round(100 - (ParsePourcentage(tb_Local.Text) /*/ 100*/), 4),
                Math.Round(ParsePourcentage(tb_Terminating.Text) /*/ 100*/, 4),
                Math.Round(100 - (ParsePourcentage(tb_Terminating.Text) /*/ 100*/), 4),
                Math.Round(ParsePourcentage(tb_ReCheck.Text) /*/ 100*/, 4),
                Math.Round(ParsePourcentage(tb_Transfer.Text)/*/100*/, 4),
                //>> AndreiZaharia Mulhouse project
                Math.Round(ParsePourcentage(txt_PaxPerCar.Text) /*/ 100*/, 4),
                //<< AndreiZaharia Mulhouse project
                0, "", "", null, null))
            {
                MessageBox.Show("The values are not valid. The values can't be bigger than 100%.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if ((ancienneSelection != "") && (bAModifieValeurs))
            {
                if ((ancienneSelection != cb_FC.Text) || (DialogResult == DialogResult.OK))
                {
                    DialogResult drSave = MessageBox.Show("Do you want to save the changes to the Flight category table ? ", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (drSave == DialogResult.Cancel)
                    {
                        cb_FC.Text = ancienneSelection;
                    }
                    else if (drSave == DialogResult.Yes)
                    {
                        SaveFlightCategorie(ancienneSelection);
                    }
                }
            }
            return true;
        }

        private void SaveFlightCategorie(string FC)
        {
            int iIndexColonne = LFTable.Columns.IndexOf(FC);
            if (iIndexColonne == -1)
            {
                return;
            }
            foreach (DataRow ligne in LFTable.Rows)
            {
                ligne.BeginEdit();
                switch (ligne.ItemArray[0].ToString())
                {
                    case GlobalNames.sLFD_A_Line_Full:
                        ligne[iIndexColonne] = Math.Round(ParsePourcentage(tb_LoadFactor.Text) /*/ 100*/, 4);
                        break;
                    case GlobalNames.sLFD_A_Line_C:
                        ligne[iIndexColonne] = Math.Round(ParsePourcentage(tb_FirstClass.Text) /*/ 100*/, 4);
                        break;
                    case GlobalNames.sLFD_A_Line_Y:
                        ligne[iIndexColonne] = Math.Round(100 - (ParsePourcentage(tb_FirstClass.Text) /*/ 100*/), 4);
                        break;
                    case GlobalNames.sLFD_A_Line_Local:
                        ligne[iIndexColonne] = Math.Round(ParsePourcentage(tb_Local.Text) /*/ 100*/, 4);
                        break;
                    case GlobalNames.sLFD_A_Line_NotLocal:
                        ligne[iIndexColonne] = Math.Round(100 - (ParsePourcentage(tb_Local.Text) /*/ 100)*/), 4);
                        break;
                    case GlobalNames.sLFA_Line_Terminating:
                        ligne[iIndexColonne] = Math.Round(ParsePourcentage(tb_Terminating.Text) /*/ 100*/, 4);
                        break;
                    case GlobalNames.sLFD_A_Line_Transferring:
                        ligne[iIndexColonne] = Math.Round(100 - (ParsePourcentage(tb_Terminating.Text) /*/ 100*/), 4);
                        break;
                    case GlobalNames.sLFD_A_Line_ReCheck:
                        ligne[iIndexColonne] = Math.Round(ParsePourcentage(tb_ReCheck.Text) /*/ 100*/, 4);
                        break;
                    case GlobalNames.sLFD_A_Line_TransferDesk:
                        ligne[iIndexColonne] = Math.Round(ParsePourcentage(tb_Transfer.Text) /*/ 100*/, 4);
                        break;
                    case GlobalNames.sLFD_A_Line_OOGTransf:
                        ligne[iIndexColonne] = Math.Round(ParsePourcentage(txt_OOGTransf.Text) /*/ 100*/, 4);
                        break;
                    case GlobalNames.sLFA_Line_OOGTerm:
                        ligne[iIndexColonne] = Math.Round(ParsePourcentage(txt_OOGTerm.Text) /*/ 100*/, 4);
                        break;
                    //>> AndreiZaharia Mulhouse project
                    case GlobalNames.sLFD_A_Line_NbPaxPerCar:
                        ligne[iIndexColonne] = Math.Round(ParsePourcentage(txt_PaxPerCar.Text) /*/ 100*/, 4);
                        break;
                    //<< AndreiZaharia Mulhouse project
                    default: 
                        break;
                }
            }
            LFTable.AcceptChanges();
            bAModifieTable = true;
        }
        private void BT_OK_Click(object sender, EventArgs e)
        {
            if (!CheckDatas())
                DialogResult = DialogResult.None;
        }
        #endregion

    }
}