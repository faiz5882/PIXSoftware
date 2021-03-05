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
    public partial class FP_AssistantAircraft : Form
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
            cb_GroundHandlers.ResetText();
            cb_GroundHandlers.Items.Clear();
            cb_Body.ResetText();
            cb_Body.Items.Clear();
            textBox4.Clear();
            comboBox3.SelectedIndex = 0;
            txt_Clef.Focus();
            txt_Clef.BackColor = Color.White;
            txt_Value.BackColor = Color.White;
        }

        private void initialiserLabels(String title)
        {
            this.Text = title;
            //Le label de titre.
            lbl_Titre.Text = title;
            lbl_Titre.Location = new Point(this.Location.X + 15, lbl_Titre.Location.Y); ;
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
            if (laTable.Columns.Count == 6)
            {
                lbl_GroundHandlers.Visible = true;
                cb_GroundHandlers.Visible = true;
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
                    if(!cb_GroundHandlers.Items.Contains("L"))
                        cb_GroundHandlers.Items.Add("L");
                    if(!cb_GroundHandlers.Items.Contains("M"))
                        cb_GroundHandlers.Items.Add("M");
                    if(!cb_GroundHandlers.Items.Contains("H"))
                        cb_GroundHandlers.Items.Add("H");
                    if(!cb_GroundHandlers.Items.Contains("S"))
                        cb_GroundHandlers.Items.Add("S");

                    if (!cb_Body.Items.Contains("A"))
                        cb_Body.Items.Add("A");
                    if (!cb_Body.Items.Contains("B"))
                        cb_Body.Items.Add("B");
                    if (!cb_Body.Items.Contains("C"))
                        cb_Body.Items.Add("C");
                    if (!cb_Body.Items.Contains("D"))
                        cb_Body.Items.Add("D");
                    if (!cb_Body.Items.Contains("E"))
                        cb_Body.Items.Add("E");
                    if (!cb_Body.Items.Contains("F"))
                        cb_Body.Items.Add("F");

                }
                else
                {
                    cb_GroundHandlers.Items.Add(GlobalNames.sTableContent_Loose);
                    cb_GroundHandlers.Items.Add(GlobalNames.sTableContent_ULD);                    
                    cb_GroundHandlers.SelectedIndex = 0;
                    cb_GroundHandlers.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                lbl_GroundHandlers.Text = laTable.Columns[2].ColumnName;
                
            }
        }

        public FP_AssistantAircraft(DataTable laTable_, String title)
        {
            InitMessage();
            InitializeComponent();
            initialiserFormulaire();
            laTable = laTable_;
            comboBox3.SelectedIndex = 0;

            foreach (DataRow ligne in laTable.Rows)
            {
                String stB = ligne["Body"].ToString();
                if (stB != "" && !cb_Body.Items.Contains(stB))
                    cb_Body.Items.Add(stB);
                String stW = ligne["Wake"].ToString();
                if (stW != "" && !cb_GroundHandlers.Items.Contains(stW))
                    cb_GroundHandlers.Items.Add(stW);

            }

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

        public FP_AssistantAircraft(DataTable laTable_, String title, DataGridViewRow Clef)
        {
            InitMessage();
            InitializeComponent();
            initialiserFormulaire();
            laTable = laTable_;
            initialiserLabels(title);
            btn_OkAdd.Visible = false;
            txt_Clef.Text = Clef.Cells[0].Value.ToString();
            txt_Clef.ReadOnly = true;

            foreach (DataRow ligne in laTable.Rows)
            {
                String stB = ligne["Body"].ToString();
                if (stB != "" && !cb_Body.Items.Contains(stB))
                    cb_Body.Items.Add(stB);
                String stW = ligne["Wake"].ToString();
                if (stW != "" && !cb_GroundHandlers.Items.Contains(stW))
                    cb_GroundHandlers.Items.Add(stW);
                                
            }


            foreach (DataRow ligne in laTable.Rows)
            {
                if (ligne.ItemArray[0].ToString() == Clef.Cells[0].Value.ToString())
                {
                    txt_Value.Text = ligne.ItemArray[1].ToString();
                    textBox4.Text = ligne.ItemArray[4].ToString();
                    comboBox3.Text = ligne.ItemArray[5].ToString();
                    cb_GroundHandlers.Text = ligne.ItemArray[2].ToString();
                    cb_Body.Text = ligne.ItemArray[3].ToString();
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
            OverallTools.DataFunctions.AddLine_FP_Aircraft(laTable,
                txt_Clef.Text,
                txt_Value.Text,
                cb_GroundHandlers.Text,
                cb_Body.Text,
                textBox4.Text,
                comboBox3.Text,
                txt_Clef.ReadOnly);
        }
        private bool DonneesValides()
        {
            int value;
            if (txt_Clef.Text == "")
            {
                //Aucune des valeurs ne peut être nulle.
                MessageBox.Show("Please fill the " + lbl_clef.Text + " Information");
                //MessageBox.Show(messages[0]+ lbl_clef.Text + messages[1]);
                txt_Clef.Focus();
                return false;
            }
            if (textBox4.Text == "")
            {
                //Aucune des valeurs ne peut être nulle.
                MessageBox.Show("Please fill the Number of seats Information");
                //MessageBox.Show(messages[0]+ lbl_clef.Text + messages[1]);
                return false;
            }
            if (!Int32.TryParse(textBox4.Text, out value))
            {
                //Aucune des valeurs ne peut être nulle.
                MessageBox.Show("Please enter a valid number for the number of seats");
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
                    if (txt_Clef.Text.ToUpper() == ligne.ItemArray[0].ToString().ToUpper())
                    {
                        MessageBox.Show("The item \""+txt_Clef.Text+"\" already exist in the table. Please choose another name.");
                        return false;
                    }
                }
            }
            if (cb_GroundHandlers.Text == "" || cb_Body.Text == "" || txt_Value.Text == "")
            {
                //Aucune des valeurs ne peut être nulle.
                DialogResult res = MessageBox.Show("Are you sure you want to quit? some of the fields are not filled ","Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                switch (res)
                {
                    case DialogResult.Yes: return true;
                    case DialogResult.No: return false;

                }
                //MessageBox.Show(messages[0]+ lbl_clef.Text + messages[1]);
                return false;
            }
            return true;
        }

        private void btn_OkAdd_Click(object sender, EventArgs e)
        {

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

        private void btn_OkAdd_Click_1(object sender, EventArgs e)
        {
            if (DonneesValides())
            {
                enregistreDonnees();
                initialiserFormulaire();
            }
        }

        private void btn_Ok_Click_1(object sender, EventArgs e)
        {
            if (!DonneesValides())
            {
                DialogResult = DialogResult.None;
                return;
            }
            enregistreDonnees();
        }

    }
}