using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace SIMCORE_TOOL.Assistant
{
    public partial class BHS_General_Assistant : Form
    {
        private DataTable dtBHS_General;

        //Manager de ressources pour la langue
        private ResourceManager manager;
        //Tableau contenu tous les messages à mettre dans les MessageBox
        //Extrait depuis le fichier de ressource associé à la fenêtre
        string[] messages;

        private void Initialize(DataTable dtBHS_General_)
        {
            //Création du manager
            manager = new ResourceManager("SIMCORE_TOOL.Assistant.BHS_General_Assistant", Assembly.GetExecutingAssembly());
            //////////////////////////////
            //Initialisation des messages
            /////////////////////////////
            messages = new string[2];
            for (int i = 0; i < messages.Length; i++)
            {
                messages[i] = manager.GetString("message" + (i + 1));
            }

            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            dtBHS_General = dtBHS_General_; 
            LoadTable();
        }

        private void LoadTable()
        {
            this.Text = messages[1]+ dtBHS_General.TableName;
            if (dtBHS_General.Rows.Count != 18)
                return;

            txt_NArrIF.Text = dtBHS_General.Rows[0][1].ToString();
            txt_MArrIF.Text = dtBHS_General.Rows[1][1].ToString();
            txt_NCI.Text = dtBHS_General.Rows[2][1].ToString();
            txt_MCI.Text = dtBHS_General.Rows[3][1].ToString();
            txt_NTrIF.Text = dtBHS_General.Rows[4][1].ToString();
            txt_MTrIF.Text = dtBHS_General.Rows[5][1].ToString();
            txt_NHBS1.Text = dtBHS_General.Rows[6][1].ToString();
            txt_MHBS1.Text = dtBHS_General.Rows[7][1].ToString();
            txt_NHBS3.Text = dtBHS_General.Rows[8][1].ToString();
            txt_MHBS3.Text = dtBHS_General.Rows[9][1].ToString();
            txt_NMES.Text = dtBHS_General.Rows[10][1].ToString();
            txt_MMES.Text = dtBHS_General.Rows[11][1].ToString();
            txt_NMU.Text = dtBHS_General.Rows[12][1].ToString();
            txt_MMU.Text = dtBHS_General.Rows[13][1].ToString();
            txt_MCIColl.Text = dtBHS_General.Rows[14][1].ToString();
            txt_NCIColl.Text = dtBHS_General.Rows[15][1].ToString();
            cb_WindowReservationCollector.Checked = dtBHS_General.Rows[16][1].ToString() == "1";
            if (cb_WindowReservationCollector.Checked)
                txt_WindowSize.Text = dtBHS_General.Rows[17][1].ToString();
            else
                txt_WindowSize.Text = "0";
            
        }
        private void SaveTable()
        {
            if(cb_WindowReservationCollector.Checked)
            {
                dtBHS_General.Rows[16][1] = 1.0d;
                Double dValue;
                double.TryParse(txt_WindowSize.Text, out dValue);
                dtBHS_General.Rows[17][1] = dValue;

            }
            else
            {
                dtBHS_General.Rows[16][1] = 0.0d;
                dtBHS_General.Rows[17][1] = 0.0d;
            }
            dtBHS_General.AcceptChanges();
        }

        public BHS_General_Assistant(DataTable dtBHS_General_Information)
        {
            Initialize(dtBHS_General_Information);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (txt_WindowSize.BackColor != Color.White)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show(messages[0], "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txt_WindowSize.Focus();
                return;
            }
            SaveTable();
        }

        private void cb_WindowReservationCollector_CheckedChanged(object sender, EventArgs e)
        {
            lbl_WindowSize.Enabled = cb_WindowReservationCollector.Checked;
            txt_WindowSize.Enabled = cb_WindowReservationCollector.Checked;
            if (cb_WindowReservationCollector.Checked && (dtBHS_General.Rows.Count==18))
                txt_WindowSize.Text = dtBHS_General.Rows[17][1].ToString();
            else
                txt_WindowSize.Text = "0";
        }


        private void txt_WindowSize_TextChanged(object sender, EventArgs e)
        {
            Double dValue;
            bool bValid = true;
            if (txt_WindowSize.Text == "")
                bValid = false;
            else if (!Double.TryParse(txt_WindowSize.Text, out dValue))
                bValid = false;
            if (!bValid)
            {
                txt_WindowSize.BackColor = Color.LightGoldenrodYellow;
            }
            else
            {
                txt_WindowSize.BackColor = Color.White;
            }
        }

        private void BHS_General_Assistant_Load(object sender, EventArgs e)
        {

        }
    }
}