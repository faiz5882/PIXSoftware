using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Resources;
using System.Reflection;

namespace SIMCORE_TOOL.Assistant
{
    public partial class FP_Assistant : Form
    {
        private DataTable laTable;

        //Pour la gestion des fichiers resssource
        private ResourceManager manager;
        //Pour les messages à afficher dans les MessageBox
        private string[] messages;

        #region Les constructeurs et les fonctions d'initialisation de la classe.
        private void initialiserFormulaire()
        {
            OverallTools.FonctionUtiles.MajBackground(this);
            txt_Clef.ReadOnly = false;
            txt_Clef.Clear();
            txt_Value.Clear();
            txt_Clef.Focus();
            txt_Clef.BackColor = Color.White;
            txt_Value.BackColor = Color.White;
        }

        private void initialiserLabels(String title)
        {
            this.Text = title;
            //Le label de titre.
            lbl_Titre.Text = title;
            lbl_Titre.Refresh();

            //Le label de la clef
            lbl_clef.Text = laTable.Columns[0].ColumnName;
            lbl_clef.Refresh();

            //Le label de la valeur
            lbl_value.Text = laTable.Columns[1].ColumnName;
            lbl_value.Refresh();

            if ((laTable.Columns[1].DataType == typeof(Int32)) ||
                (laTable.Columns[1].DataType == typeof(Int16)) ||
                (laTable.Columns[1].DataType == typeof(Int64)) ||
                (laTable.Columns[1].DataType == typeof(int)))
            {
                this.lbl_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            }
            if (laTable.Columns.Count == 3)
            {
                lbl_GroundHandlers.Visible = true;
                cb_GroundHandlers.Visible = true;
                this.Height += 50;
                if (laTable.Columns[2].ColumnName != GlobalNames.sTableColumn_ULDLoose)
                {
                    ArrayList alList = new ArrayList();
                    foreach (DataRow drRow in laTable.Rows)
                    {
                        if (drRow[2].ToString() == "")
                            continue;
                        if (!alList.Contains(drRow[2].ToString()))
                            alList.Add(drRow[2].ToString());
                    }
                    alList.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                    cb_GroundHandlers.Items.AddRange(alList.ToArray());
                }
                else
                {
                    cb_GroundHandlers.Items.Add(GlobalNames.sTableContent_ULD);
                    cb_GroundHandlers.Items.Add(GlobalNames.sTableContent_Loose);
                    cb_GroundHandlers.SelectedIndex = 0;
                    cb_GroundHandlers.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                lbl_GroundHandlers.Text = laTable.Columns[2].ColumnName;
                
            }


            if ((lbl_clef.Width > txt_Clef.Left - 10) || (lbl_value.Width > txt_Value.Left - 10) ||
                (this.Width < (lbl_Titre.Width+25+lbl_Titre.Left)))
            {
                int deplacement;
                if (this.Width < lbl_Titre.Width + 25)
                {
                    deplacement = (lbl_Titre.Width + 25) - this.Width;
                }else if (lbl_clef.Width > lbl_value.Width)
                {
                    deplacement = -(txt_Clef.Left - lbl_clef.Width - 10);
                }
                else
                {
                    deplacement = -(txt_Clef.Left - lbl_value.Width - 10);
                }
                this.Width += deplacement;
                btn_Cancel.Left += deplacement;
                txt_Clef.Left += deplacement;
                txt_Value.Left += deplacement;
                cb_GroundHandlers.Left += deplacement;
            }
            lbl_Titre.Left = (this.Width - lbl_Titre.Width) / 2;
            lbl_clef.Left = txt_Clef.Left - 5 - lbl_clef.Width;
            lbl_value.Left = txt_Value.Left - 5 - lbl_value.Width;
            lbl_GroundHandlers.Left = cb_GroundHandlers.Left - 5 - lbl_GroundHandlers.Width;
        }

        public FP_Assistant(DataTable laTable_, String title)
        {
            InitMessage();
            InitializeComponent();
            initialiserFormulaire();

            laTable = laTable_;
            initialiserLabels(title);
        }

        //Initialisation des messages utlisateur et du ResourceManager
        private void InitMessage()
        {
            /////////////////////////////
            //Creation du resourceManager
            ////////////////////////////
            manager = new ResourceManager("SIMCORE_TOOL.Assistant.FP_Assistant", Assembly.GetExecutingAssembly());
            //////////////////////////////
            //Initialisation des messages
            /////////////////////////////
            messages = new string[5];
            for (int i = 0; i < messages.Length; i++)
            {
                messages[i] = manager.GetString("message" + (i + 1));
            }
        }

        public FP_Assistant(DataTable laTable_, String title, String Clef)
        {
            InitMessage();
            InitializeComponent();
            initialiserFormulaire();
            laTable = laTable_;
            initialiserLabels(title);
            btn_OkAdd.Visible = false;
            txt_Clef.Text = Clef;
            txt_Clef.ReadOnly = true;
            foreach (DataRow ligne in laTable.Rows)
            {
                if (ligne.ItemArray[0].ToString() == Clef)
                {
                    txt_Value.Text = ligne.ItemArray[1].ToString();
                    break;
                }
            }
        }
        #endregion

        #region Gestion des boutons et de l'affichage du formulaire.
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (!DonneesValides())
            {
                DialogResult = DialogResult.None;
                return;
            }
            enregistreDonnees();
        }
        private void enregistreDonnees()
        {
            /*if (!txt_Clef.ReadOnly)
            {
                DataRow ligne = laTable.NewRow();
                ligne[0] = txt_Clef.Text;
                if ((laTable.Columns[1].DataType == typeof(Int32)) ||
                    (laTable.Columns[1].DataType == typeof(Int16)) ||
                    (laTable.Columns[1].DataType == typeof(Int64)) ||
                    (laTable.Columns[1].DataType == typeof(int)))
                {
                    int value;
                    if ((txt_Value.Text != "") && (Int32.TryParse(txt_Value.Text, out value)))
                    {
                        ligne[1] = value;
                    }
                }
                else
                {
                    ligne[1] = txt_Value.Text;
                }
                if (laTable.Columns.Count == 3)
                    ligne[2] = cb_GroundHandlers.Text;
                laTable.Rows.Add(ligne);
            }
            else
            {
                foreach (DataRow ligne in laTable.Rows)
                {
                    if (txt_Clef.Text == ligne.ItemArray[0].ToString())
                    {
                        ligne.BeginEdit();
                        ligne[1] = txt_Value.Text;
                        if (laTable.Columns.Count == 3)
                            ligne[2] = cb_GroundHandlers.Text;
                    }
                }
            }*/
            OverallTools.DataFunctions.AddLine_FP(laTable, txt_Clef.Text, txt_Value.Text, cb_GroundHandlers.Text, txt_Clef.ReadOnly);
        }
        private bool DonneesValides()
        {
            if ((txt_Clef.Text == ""))
            {
                //Aucune des valeurs ne peut être nulle.
                MessageBox.Show("Please fill the " + lbl_clef.Text + " Information");
                //MessageBox.Show(messages[0]+ lbl_clef.Text + messages[1]);
                txt_Clef.Focus();
                return false;
            }
            if ((txt_Value.Text == "") && (lbl_value.Font.Bold))
            {
                //Aucune des valeurs ne peut être nulle.
                MessageBox.Show("Please fill the " + lbl_value.Text + " Information");
                //MessageBox.Show(messages[0] + lbl_value.Text + messages[1]);
                txt_Value.Focus();
                return false;
            }
            if ((laTable.Columns[1].DataType == typeof(Int32)) ||
                (laTable.Columns[1].DataType == typeof(Int16)) ||
                (laTable.Columns[1].DataType == typeof(Int64)) ||
                (laTable.Columns[1].DataType == typeof(int)))
            {
                int value;
                if ((txt_Value.Text == "") || (!Int32.TryParse(txt_Value.Text, out value)))
                {
                    //Aucune des valeurs ne peut être nulle.
                    MessageBox.Show("Please enter a valid number for the " + lbl_value.Text);
                    //MessageBox.Show(messages[2] + lbl_value.Text);
                    txt_Value.Focus();
                    return false;
                }
            }
            if (!txt_Clef.ReadOnly)
            {
                //Il faut vérifier que le contenu de cette case n'est pas vide, il ne faut pas non plus qu'il
                //soit déjà présent dans la table.
                foreach (DataRow ligne in laTable.Rows) 
                {
                    if (txt_Clef.Text == ligne.ItemArray[0].ToString())
                    {
                        MessageBox.Show("The item \""+txt_Clef.Text+"\" already exist in the table. Please choose another name.");
                        //MessageBox.Show(messages[3]+" "+ txt_Clef.Text + messages[4]);
                        return false;
                    }
                }
            }
            return true;
        }

        private void btn_OkAdd_Click(object sender, EventArgs e)
        {
            if (DonneesValides())
            {
                enregistreDonnees();
                initialiserFormulaire();
            }
        }
        private void FP_Assistant_Shown(object sender, EventArgs e)
        {
            if (txt_Clef.ReadOnly)
            {
                txt_Value.Focus();
            }
        }
        #endregion

        private void lbl_GroundHandlers_Click(object sender, EventArgs e)
        {

        }

        private void FP_Assistant_Load(object sender, EventArgs e)
        {

        }

    }
}