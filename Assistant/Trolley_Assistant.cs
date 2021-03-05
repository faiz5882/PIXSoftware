using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.Assistant.SubForms;
namespace SIMCORE_TOOL.Assistant
{
    public partial class Trolley_Assistant : Form
    {
        private DataTable dtTrolley_;
        private DataTable dtTrolley_Project_;
        private int iIndexBaggage_;
        private int iIndexTrolley_;
        private int iNbBaggageMax_;
        private int iLastSelected;
        private bool bAllowChangeFlightCategorie;
        private static int GroupSize= 105;

        #region Constructeurs et fonctions d'initialisation du formulaire.
        #region Fonction d'initialisation du formulaire
        private void Initialize(DataTable dtTrolley)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            dtTrolley_Project_ = dtTrolley;
            dtTrolley_ = dtTrolley.Copy();
            if(dtTrolley_ == null)
                return;
            iIndexBaggage_ = dtTrolley_.Columns.IndexOf("NbBags");
            iIndexTrolley_ = dtTrolley_.Columns.IndexOf("NbTrolley");
            if (iIndexBaggage_ == -1)
                return;
            if (iIndexTrolley_ == -1)
                return;
            iNbBaggageMax_ = OverallTools.DataFunctions.valeurMaximaleDansColonne(dtTrolley_, iIndexBaggage_);
            if (iNbBaggageMax_ < 0)
                return;
            panel1.BackgroundImage = OverallTools.FonctionUtiles.dessinerFondEcran(300, GroupSize * (iNbBaggageMax_+1), PAX2SIM.Couleur1, PAX2SIM.Couleur2, PAX2SIM.Angle);

            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            gb_BaggageDefinition = new GroupBox[iNbBaggageMax_+1];
            tb_Percentage0 = new TextBox[iNbBaggageMax_+1];
            tb_Percentage1 = new TextBox[iNbBaggageMax_+1];
            tb_Percentage2 = new TextBox[iNbBaggageMax_+1];
            lbl_Percentage0 = new Label[iNbBaggageMax_+1];
            lbl_Percentage1 = new Label[iNbBaggageMax_+1];
            lbl_Percentage2 = new Label[iNbBaggageMax_+1];


            for (int i = 0; i <= iNbBaggageMax_; i++)
            {
                InitializeBaggageBox(i);
                gb_BaggageDefinition[i].Enabled = false;
            }

            if (dtTrolley.Columns.Count > 2)
            {
                foreach (DataColumn colonne in dtTrolley.Columns)
                {
                    if ((colonne.ColumnName != "NbBags") && (colonne.ColumnName != "NbTrolley"))
                    {
                        cb_FC.Items.Add(colonne.ColumnName);
                    }
                }
            }
            if(cb_FC.Items.Count == 0)
                return;            
            this.cb_FC.SelectedIndex = 0;
            setSelected(this.cb_FC.Text);
            this.cb_FC.SelectedIndexChanged += new System.EventHandler(this.cb_FC_SelectedIndexChanged);
            iLastSelected = 0;
            bAllowChangeFlightCategorie = true;
        }
        #endregion
        #region Constructeur du formulaire.
        public Trolley_Assistant(DataTable dtTrolley)
        {
            Initialize(dtTrolley);
        }
        public Trolley_Assistant(DataTable dtTrolley, String selectedColumn)
        {
            Initialize(dtTrolley);
            if (cb_FC.Items.Contains(selectedColumn))
            {
                cb_FC.Text = selectedColumn;
            }
        }
        #endregion 
        #endregion

        #region Fonction utilisée lorsque la sélection dans la liste est modifiée.
        private void cb_FC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bAllowChangeFlightCategorie)
                return;
            bool bSauvegarde = true;
            /*if (!isValid())
            {
                DialogResult drResult = MessageBox.Show("The data contained for the previous flight categorie have some mistake. Do you want to validate them ?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (drResult == DialogResult.Cancel)
                {
                    bAllowChangeFlightCategorie = false;
                    if ((iLastSelected != -1) && (iLastSelected < cb_FC.Items.Count))
                    {
                        setSelected(cb_FC.Items[iLastSelected].ToString());
                        cb_FC.SelectedIndex = iLastSelected;
                    }
                    bAllowChangeFlightCategorie = true;
                    return;
                }
                else if (drResult == DialogResult.Yes)
                {
                    bSauvegarde = true;
                }
                else //if (drResult == DialogResult.No)
                {
                    bSauvegarde = false;
                }
            }*/
            bSauvegarde = SaveChanges();
            if (!bAllowChangeFlightCategorie) //Le cancel.
            {
                bAllowChangeFlightCategorie = true;
                return;
            }
            if (bSauvegarde && (iLastSelected != -1) && (iLastSelected < cb_FC.Items.Count) )
            {
                SaveDatas(cb_FC.Items[iLastSelected].ToString());
            }
            setSelected(cb_FC.Text);
            iLastSelected = cb_FC.SelectedIndex;
        }
        private bool SaveChanges()
        {
            if (isValid())
                return true;
            DialogResult drResult = MessageBox.Show("The data contained for the previous flight categorie have some mistake. Do you want to validate them ?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (drResult == DialogResult.Cancel)
            {
                bAllowChangeFlightCategorie = false;
                if ((iLastSelected != -1) && 
                    (iLastSelected < cb_FC.Items.Count) && 
                    (iLastSelected != cb_FC.SelectedIndex))
                {
                    setSelected(cb_FC.Items[iLastSelected].ToString());
                    cb_FC.SelectedIndex = iLastSelected;
                }
                //bAllowChangeFlightCategorie = true;
                return false;
            }
            else if (drResult == DialogResult.Yes)
            {
                return true;
            }
            //if (drResult == DialogResult.No)
            return false;
        }
        #endregion

        #region Fonction pour sauvegarder le contenu des champs du formulaire
        private void SaveDatas(String sFlightCategorie)
        {
            if (gb_BaggageDefinition == null)
                return;
            if (gb_BaggageDefinition.Length == 0)
                return;
            int iColumn = dtTrolley_.Columns.IndexOf(sFlightCategorie);
            if ((iColumn == -1))
                return;
            Double[] dValues = null;
            int j;
            for (int i = 0; i < gb_BaggageDefinition.Length; i++)
            {
                dValues = getValues(i);
                if ((dValues == null)||(dValues.Length != 3))
                    continue;
                int[] iLignes = OverallTools.DataFunctions.indexLignes(dtTrolley_, iIndexBaggage_, i.ToString());
                if ((iLignes == null) || (iLignes.Length != 3))
                    continue;
                for (j = 0; j < iLignes.Length; j++)
                {
                    int iValue;
                    if ((Int32.TryParse(dtTrolley_.Rows[iLignes[j]][iIndexTrolley_].ToString(), out iValue)) &&
                        (iValue < 3) && (iValue >= 0))
                    {
                        dtTrolley_.Rows[iLignes[j]].BeginEdit();
                        dtTrolley_.Rows[iLignes[j]][iColumn] = dValues[j];
                        dtTrolley_.Rows[iLignes[j]].EndEdit();
                    }
                }
            }
        }
        #endregion

        #region Fonction qui vérifie la validité du contenu du formulaire.
        private bool isValid()
        {
            if (gb_BaggageDefinition == null)
                return true;
            if (gb_BaggageDefinition.Length == 0)
                return true;
            for (int i = 0; i < gb_BaggageDefinition.Length; i++)
            {
                if (!isValid(i))
                    return false;
            }
            return true;
        }
        #endregion

        #region Fonction pour mettre à jour l'affichage d'une flight categorie.
        private void setSelected(String sFlightCategorie)
        {
            if (gb_BaggageDefinition == null)
                return;
            if (gb_BaggageDefinition.Length == 0)
                return;

            int iColumn = dtTrolley_.Columns.IndexOf(sFlightCategorie);
            if ((iColumn == -1))
            {
                for(int i=0; i<gb_BaggageDefinition.Length;i++)
                {
                    setValues(i,100, 0, 0);
                    gb_BaggageDefinition[i].Enabled = false;
                }
                return;
            }
            Double[] dValues = new Double[3];
            int j;
            for (int i = 0; i < gb_BaggageDefinition.Length; i++)
            {
                gb_BaggageDefinition[i].Enabled = true;
                int[] iLignes = OverallTools.DataFunctions.indexLignes(dtTrolley_, iIndexBaggage_, i.ToString());
                if ((iLignes == null) || (iLignes.Length != 3))
                {
                    setValues(i, 100, 0, 0);
                    continue;
                }
                for (j = 0; j < 3; j++)
                    dValues[j] = -1;
                for (j = 0; j < iLignes.Length; j++)
                {
                    int iValue;
                    if ((!Int32.TryParse(dtTrolley_.Rows[iLignes[j]][iIndexTrolley_].ToString(), out iValue)) ||
                        (iValue >= 3) || (iValue < 0) ||
                        (!Double.TryParse(dtTrolley_.Rows[iLignes[j]][iColumn].ToString(), out dValues[iValue])))
                    {
                        dValues[0] = -1;
                        break;
                    }
                }
                if (dValues[0] == -1)
                    continue;
                setValues(i, dValues[0], dValues[1], dValues[2]);
            }
        }
        #endregion

        #region Partie de code qui remplace le subform.
        private System.Windows.Forms.GroupBox[] gb_BaggageDefinition = null;
        private System.Windows.Forms.TextBox[] tb_Percentage0 = null;
        private System.Windows.Forms.TextBox[] tb_Percentage1 = null;
        private System.Windows.Forms.TextBox[] tb_Percentage2 = null;
        private System.Windows.Forms.Label[] lbl_Percentage0 = null;
        private System.Windows.Forms.Label[] lbl_Percentage1 = null;
        private System.Windows.Forms.Label[] lbl_Percentage2 = null;

        private void InitializeBaggageBox(int iIndexBaggageBox)
        {
            gb_BaggageDefinition[iIndexBaggageBox] = new System.Windows.Forms.GroupBox();
            tb_Percentage2[iIndexBaggageBox] = new System.Windows.Forms.TextBox();
            tb_Percentage1[iIndexBaggageBox] = new System.Windows.Forms.TextBox();
            tb_Percentage0[iIndexBaggageBox] = new System.Windows.Forms.TextBox();
            lbl_Percentage2[iIndexBaggageBox] = new System.Windows.Forms.Label();
            lbl_Percentage1[iIndexBaggageBox] = new System.Windows.Forms.Label();
            lbl_Percentage0[iIndexBaggageBox] = new System.Windows.Forms.Label();

            // 
            // groupBox1
            // 
            gb_BaggageDefinition[iIndexBaggageBox].BackColor = System.Drawing.Color.Transparent;
            gb_BaggageDefinition[iIndexBaggageBox].Controls.Add(tb_Percentage2[iIndexBaggageBox]);
            gb_BaggageDefinition[iIndexBaggageBox].Controls.Add(tb_Percentage1[iIndexBaggageBox]);
            gb_BaggageDefinition[iIndexBaggageBox].Controls.Add(tb_Percentage0[iIndexBaggageBox]);
            gb_BaggageDefinition[iIndexBaggageBox].Controls.Add(lbl_Percentage2[iIndexBaggageBox]);
            gb_BaggageDefinition[iIndexBaggageBox].Controls.Add(lbl_Percentage1[iIndexBaggageBox]);
            gb_BaggageDefinition[iIndexBaggageBox].Controls.Add(lbl_Percentage0[iIndexBaggageBox]);

            gb_BaggageDefinition[iIndexBaggageBox].Location = new System.Drawing.Point(20, GroupSize * iIndexBaggageBox+20);
            gb_BaggageDefinition[iIndexBaggageBox].Name = "gb_BaggageDefinition" + iIndexBaggageBox.ToString();
            gb_BaggageDefinition[iIndexBaggageBox].Size = new System.Drawing.Size(307, 97);
            gb_BaggageDefinition[iIndexBaggageBox].TabIndex = iIndexBaggageBox + 1;
            gb_BaggageDefinition[iIndexBaggageBox].TabStop = false;
            String sAdd = "";
            if (iIndexBaggageBox > 1)
                sAdd = "s";
            gb_BaggageDefinition[iIndexBaggageBox].Text = "Passengers with " + iIndexBaggageBox.ToString() + " Bag" + sAdd + " in the baggage hold";
            // 
            // lbl_Percentage2
            // 
            lbl_Percentage2[iIndexBaggageBox].AutoSize = true;
            lbl_Percentage2[iIndexBaggageBox].Location = new System.Drawing.Point(6, 69);
            lbl_Percentage2[iIndexBaggageBox].Name = "lbl_Percentage2" + iIndexBaggageBox.ToString();
            lbl_Percentage2[iIndexBaggageBox].Size = new System.Drawing.Size(216, 13);
            lbl_Percentage2[iIndexBaggageBox].TabIndex = 4;
            lbl_Percentage2[iIndexBaggageBox].Text = "Percentage of passenger which use 2 trolleys";
            // 
            // lbl_Percentage1
            // 
            lbl_Percentage1[iIndexBaggageBox].AutoSize = true;
            lbl_Percentage1[iIndexBaggageBox].Location = new System.Drawing.Point(6, 48);
            lbl_Percentage1[iIndexBaggageBox].Name = "lbl_Percentage1" + iIndexBaggageBox.ToString();
            lbl_Percentage1[iIndexBaggageBox].Size = new System.Drawing.Size(216, 13);
            lbl_Percentage1[iIndexBaggageBox].TabIndex = 2;
            lbl_Percentage1[iIndexBaggageBox].Text = "Percentage of passenger which use 1 trolley";
            // 
            // lbl_Percentage0
            // 
            lbl_Percentage0[iIndexBaggageBox].AutoSize = true;
            lbl_Percentage0[iIndexBaggageBox].Location = new System.Drawing.Point(6, 26);
            lbl_Percentage0[iIndexBaggageBox].Name = "lbl_Percentage0" + iIndexBaggageBox.ToString();
            lbl_Percentage0[iIndexBaggageBox].Size = new System.Drawing.Size(216, 13);
            lbl_Percentage0[iIndexBaggageBox].TabIndex = 0;
            lbl_Percentage0[iIndexBaggageBox].Text = "Percentage of passenger which use 0 trolley";

            // 
            // tb_Percentage2
            // 
            tb_Percentage2[iIndexBaggageBox].Location = new System.Drawing.Point(251, 66);
            tb_Percentage2[iIndexBaggageBox].Name = "tb_Percentage2" + iIndexBaggageBox.ToString();
            tb_Percentage2[iIndexBaggageBox].Size = new System.Drawing.Size(47, 20);
            tb_Percentage2[iIndexBaggageBox].TabIndex = 5;
            tb_Percentage2[iIndexBaggageBox].TextChanged += new System.EventHandler(this.tb_Percentage0_TextChanged);
            tb_Percentage2[iIndexBaggageBox].Tag = iIndexBaggageBox;
            tb_Percentage2[iIndexBaggageBox].Text= "0";
            // 
            // tb_Percentage1
            // 
            tb_Percentage1[iIndexBaggageBox].Location = new System.Drawing.Point(251, 45);
            tb_Percentage1[iIndexBaggageBox].Name = "tb_Percentage1" + iIndexBaggageBox.ToString();
            tb_Percentage1[iIndexBaggageBox].Size = new System.Drawing.Size(47, 20);
            tb_Percentage1[iIndexBaggageBox].TabIndex = 3;
            tb_Percentage1[iIndexBaggageBox].TextChanged += new System.EventHandler(this.tb_Percentage0_TextChanged);
            tb_Percentage1[iIndexBaggageBox].Tag = iIndexBaggageBox;
            tb_Percentage1[iIndexBaggageBox].Text = "0";
            // 
            // tb_Percentage0
            // 
            tb_Percentage0[iIndexBaggageBox].Location = new System.Drawing.Point(251, 23);
            tb_Percentage0[iIndexBaggageBox].Name = "tb_Percentage0" + iIndexBaggageBox.ToString();
            tb_Percentage0[iIndexBaggageBox].Size = new System.Drawing.Size(47, 20);
            tb_Percentage0[iIndexBaggageBox].TabIndex = 1;
            tb_Percentage0[iIndexBaggageBox].TextChanged += new System.EventHandler(this.tb_Percentage0_TextChanged);
            tb_Percentage0[iIndexBaggageBox].Tag = iIndexBaggageBox;
            tb_Percentage0[iIndexBaggageBox].Text = "100";

            panel1.Controls.Add(gb_BaggageDefinition[iIndexBaggageBox]);

            gb_BaggageDefinition[iIndexBaggageBox].ResumeLayout(false);
            gb_BaggageDefinition[iIndexBaggageBox].PerformLayout();
        }

        private void tb_Percentage0_TextChanged(object sender, EventArgs e)
        {
            Double dValue;
            if ((!Double.TryParse(((TextBox)sender).Text, out dValue)) || (dValue < 0) || (dValue > 100))
            {
                ((TextBox)sender).BackColor = Color.Red;
            }
            else
            {
                int iTag = (int)((TextBox)sender).Tag;
                ((TextBox)sender).BackColor = Color.White;
                if (!CheckPercentage(iTag))
                {
                    gb_BaggageDefinition[iTag].ForeColor = Color.Red;
                }
                else
                {
                    gb_BaggageDefinition[iTag].ForeColor = Color.Black;
                }
            }
        }

        private bool CheckPercentage(int iTag)
        {
            if (!ValuesAreValid(iTag))
                return false;
            Double dValue0, dValue1, dValue2;
            if ((!Double.TryParse(tb_Percentage0[iTag].Text, out dValue0)) ||
                (!Double.TryParse(tb_Percentage1[iTag].Text, out dValue1)) ||
                (!Double.TryParse(tb_Percentage2[iTag].Text, out dValue2)))
            {
                return false;
            }
            return ((dValue0 + dValue1 + dValue2) == 100);
        }

        private bool ValuesAreValid(int iTag)
        {
            return ((tb_Percentage0[iTag].BackColor == Color.White) &&
                    (tb_Percentage1[iTag].BackColor == Color.White) &&
                    (tb_Percentage2[iTag].BackColor == Color.White));
        }

        public bool isValid(int iTag)
        {
            return (ValuesAreValid(iTag) &&
                    (gb_BaggageDefinition[iTag].ForeColor == Color.Black));
        }


        public void setValues(int iTag,Double Value0, Double Value1, Double Value2)
        {
            tb_Percentage0[iTag].Text = Value0.ToString();
            tb_Percentage1[iTag].Text = Value1.ToString();
            tb_Percentage2[iTag].Text = Value2.ToString();
        }
        public Double[] getValues(int iTag)
        {
            Double[] dResult = new double[3] { 0, 0, 0 };
            if (tb_Percentage0[iTag].BackColor == Color.White)
                Double.TryParse(tb_Percentage0[iTag].Text, out dResult[0]);
            if (tb_Percentage1[iTag].BackColor == Color.White)
                Double.TryParse(tb_Percentage1[iTag].Text, out dResult[1]);
            if (tb_Percentage2[iTag].BackColor == Color.White)
                Double.TryParse(tb_Percentage2[iTag].Text, out dResult[2]);
            return dResult;
        }
        #endregion

        #region Fonction pour la gestion du clic sur le bouton ok.
        private void BT_OK_Click(object sender, EventArgs e)
        {
            bool bSauvegarde = SaveChanges();
            if (!bAllowChangeFlightCategorie) //Le cancel.
            {
                bAllowChangeFlightCategorie = true;
                DialogResult = DialogResult.None;
                return;
            }
            if (bSauvegarde)
            {
                SaveDatas(cb_FC.Items[iLastSelected].ToString());
            }
            //Ici recopie dans la table du projet du contenu de la table temporaire du formulaire.

            dtTrolley_Project_.Rows.Clear();
            foreach (DataRow row in dtTrolley_.Rows)
            {
                dtTrolley_Project_.Rows.Add(row.ItemArray);
            }
        }
        #endregion

    }
}