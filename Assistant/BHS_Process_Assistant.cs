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
    public partial class BHS_Process_Assistant : Form
    {
        #region Les différentes variables de la classe.
        private DataTable dtTable;
        //Manager de ressources pour la langue
        private ResourceManager manager;
        //Tableau contenu tous les messages à mettre dans les MessageBox
        //Extrait depuis le fichier de ressource associé à la fenêtre
        string[] messages;
        #endregion

        #region Constructeurs et fonctions pour initialiser la classe.
        private void Initialize(DataTable dtTable_)
        {
            //Création du manager
            manager = new ResourceManager("SIMCORE_TOOL.Assistant.BHS_Process_Assistant", Assembly.GetExecutingAssembly());
            //////////////////////////////
            //Initialisation des messages
            /////////////////////////////
            messages = new string[11];
            for (int i = 0; i < messages.Length; i++)
            {
                messages[i] = manager.GetString("message" + (i + 1));
            }

            InitializeComponent();
            setDelhi();
            OverallTools.FonctionUtiles.MajBackground(this);
            dtTable = dtTable_;
            this.Text = messages[10] + dtTable.TableName;
            OverallTools.FormFunctions.FillControl(this, dtTable, 0, 1);
            txt_HBS2_Min_Leave(null, null);
            txt_HBS3_Min_Leave(null, null);
            txt_HBS4_Min_Leave(null, null);
            txt_HBS5_Min_Leave(null, null);
        }
        public BHS_Process_Assistant(DataTable dtTable_)
        {
            Initialize(dtTable_);
        }
        #endregion

        


        #region Fonction lorsque le bouton ok est cliqué
        private void BT_OK_Click(object sender, EventArgs e)
        {
            if (!OverallTools.FormFunctions.VerifData(this))
            {
                MessageBox.Show(messages[0], "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.None;
                return;
            }
            OverallTools.FormFunctions.SaveControl(this, dtTable, 0, 1);
        }
        #endregion

  
        #region Fonctons pour vérifier la validité des informations entrées dans le formulaire.

        private void textBoxTextChanged_Percent(object sender, EventArgs e)
        {
            String sValue = ((TextBox)sender).Text;
            Double dValue;
            if (sValue == "")
            {
                ((TextBox)sender).Text = "0";
            }
            else if (!Double.TryParse(sValue, out dValue))
            {
                ((TextBox)sender).BackColor = Color.Red;
                tt_Tooltips.SetToolTip((Control)sender, messages[1]);
                return;
            }
            else if ((dValue > 100) || (dValue < 0))
            {
                ((TextBox)sender).BackColor = Color.Red;
                tt_Tooltips.SetToolTip((Control)sender, messages[2]);
                return;
            }
            ((TextBox)sender).BackColor = Color.White;
        }
        private void textBoxTextChanged_Number(object sender, EventArgs e)
        {
            String sValue = ((TextBox)sender).Text;
            Double dValue=0;
            if (sValue == "")
            {
                ((TextBox)sender).Text = "0";
            }
            else if (!Double.TryParse(sValue, out dValue))
            {
                ((TextBox)sender).BackColor = Color.Red;
                tt_Tooltips.SetToolTip((Control)sender, messages[1]);
                return;
            }
            if (dValue < 0)
            {
                ((TextBox)sender).BackColor = Color.Red;
                tt_Tooltips.SetToolTip((Control)sender, messages[3]);
                return;
            }
            tt_Tooltips.SetToolTip((Control)sender, dValue.ToString());
            ((TextBox)sender).BackColor = Color.White;
        }
        #endregion


        private void txt_HBS2_Min_Leave(object sender, EventArgs e)
        {
            if ((txt_HBS2_Min.BackColor != Color.White) ||
               (txt_HBS2_Mode.BackColor != Color.White) ||
               (txt_HBS2_Max.BackColor != Color.White))
            {
                txt_HBS2_Mean.Text = "0";
                return;
            }
            Double dtotal = 0;
            Double dValue;
            Double.TryParse(txt_HBS2_Min.Text, out dValue);
            dtotal += dValue;
            Double.TryParse(txt_HBS2_Mode.Text, out dValue);
            dtotal += dValue;
            Double.TryParse(txt_HBS2_Max.Text, out dValue);
            dtotal += dValue;
            txt_HBS2_Mean.Text = Math.Round((dtotal/3),2).ToString();
        }

        private void txt_HBS3_Min_Leave(object sender, EventArgs e)
        {
            if ((txt_HBS3_Min.BackColor != Color.White) ||
               (txt_HBS3_Mode.BackColor != Color.White) ||
               (txt_HBS3_Max.BackColor != Color.White))
            {
                txt_HBS3_Mean.Text = "0";
                return;
            }
            Double dtotal = 0;
            Double dValue;
            Double.TryParse(txt_HBS3_Min.Text, out dValue);
            dtotal += dValue;
            Double.TryParse(txt_HBS3_Mode.Text, out dValue);
            dtotal += dValue;
            Double.TryParse(txt_HBS3_Max.Text, out dValue);
            dtotal += dValue;
            txt_HBS3_Mean.Text = Math.Round((dtotal/3),2).ToString();

        }

        private void txt_HBS4_Min_Leave(object sender, EventArgs e)
        {
            if ((txt_HBS4_Min.BackColor != Color.White) ||
               (txt_HBS4_Mode.BackColor != Color.White) ||
               (txt_HBS4_Max.BackColor != Color.White))
            {
                txt_HBS4_Mean.Text = "0";
                return;
            }
            Double dtotal = 0;
            Double dValue;
            Double.TryParse(txt_HBS4_Min.Text, out dValue);
            dtotal += dValue;
            Double.TryParse(txt_HBS4_Mode.Text, out dValue);
            dtotal += dValue;
            Double.TryParse(txt_HBS4_Max.Text, out dValue);
            dtotal += dValue;
            txt_HBS4_Mean.Text = Math.Round((dtotal/3),2).ToString();
        }

        private void txt_HBS5_Min_Leave(object sender, EventArgs e)
        {
            if ((txt_HBS5_Min.BackColor != Color.White) ||
               (txt_HBS5_Mode.BackColor != Color.White) ||
               (txt_HBS5_Max.BackColor != Color.White))
            {
                txt_HBS5_Mean.Text = "0";
                return;
            }
            Double dtotal = 0;
            Double dValue;
            Double.TryParse(txt_HBS5_Min.Text, out dValue);
            dtotal += dValue;
            Double.TryParse(txt_HBS5_Mode.Text, out dValue);
            dtotal += dValue;
            Double.TryParse(txt_HBS5_Max.Text, out dValue);
            dtotal += dValue;
            txt_HBS5_Mean.Text = Math.Round((dtotal / 3), 2).ToString();
        }

        private void txt_MakeUp_H_Leave(object sender, EventArgs e)
        {
            textBoxTextChanged_Number(sender, e);
            Double dFlow;
            if ((txt_MakeUp_H.BackColor != Color.White) ||
                (!Double.TryParse(txt_MakeUp_H.Text, out dFlow)))
            {
                txt_MakeUp_S.Text = "0";
                return;
            }
            if (dFlow == 0)
                txt_MakeUp_S.Text = "0";
            else
                txt_MakeUp_S.Text = ((Double)Math.Round(3600 / dFlow, 2)).ToString();
        }

        private void txt_Transf_H_TextChanged(object sender, EventArgs e)
        {
            textBoxTextChanged_Number(sender, e);
            Double dFlow;
            if ((txt_Transf_H.BackColor != Color.White)||
                (!Double.TryParse(txt_Transf_H.Text,out dFlow)))
            {
                txt_Transf_S.Text = "0";
                return;
            }
            if (dFlow == 0)
                txt_Transf_S.Text = "0";
            else
                txt_Transf_S.Text = ((Double)Math.Round(3600/dFlow,2)).ToString();
        }

        private void txt_Arriv_H_TextChanged(object sender, EventArgs e)
        {
            textBoxTextChanged_Number(sender, e);
            Double dFlow;
            if ((txt_Arriv_H.BackColor != Color.White) ||
                (!Double.TryParse(txt_Arriv_H.Text, out dFlow)))
            {
                txt_Arriv_S.Text = "0";
                return;
            }
            if (dFlow == 0)
                txt_Arriv_S.Text = "0";
            else
                txt_Arriv_S.Text = ((Double)Math.Round(3600 / dFlow, 2)).ToString();
        }


        private void txt_Transf2_H_TextChanged(object sender, EventArgs e)
        {
            textBoxTextChanged_Number(sender, e);
            Double dFlow = 0;
            if ((txt_Transf2_H.BackColor != Color.White) ||
                (!Double.TryParse(txt_Transf2_H.Text, out dFlow)))
            {
                txt_Transf2_S.Text = "0";
                return;
            }
            if (dFlow == 0)
                txt_Transf2_S.Text = "0";
            else
                txt_Transf2_S.Text = ((Double)Math.Round(3600 / dFlow, 2)).ToString();
        }

        private void txt_DMRC_H_TextChanged(object sender, EventArgs e)
        {

            textBoxTextChanged_Number(sender, e);
            Double dFlow = 0;
            if ((txt_DMRC_H.BackColor != Color.White) ||
                (!Double.TryParse(txt_DMRC_H.Text, out dFlow)))
            {
                txt_DMRC_S.Text = "0";
                return;
            }
            if (dFlow == 0)
                txt_DMRC_S.Text = "0";
            else
                txt_DMRC_S.Text = ((Double)Math.Round(3600 / dFlow, 2)).ToString();
        }
        private void setDelhi()
        {
            if (!PAX2SIM.bDelhi)
                return;
            cb_HBS4_HoldInside.Visible = false;
            gb_HBS3.Text = messages[4];
            gb_HBS4.Text = messages[5];
            gb_HBS5.Text = messages[6];

            txt_HBS3_Min.Tag = "HBS Lev.3 Automatic process time Min (sec)";
            txt_HBS3_Mode.Tag = "HBS Lev.3 Automatic process time Mode (sec)";
            txt_HBS3_Max.Tag = "HBS Lev.3 Automatic process time Max (sec)";

            txt_HBS4_Min.Tag = "HBS Lev.3 Manual process time Min (sec)";
            txt_HBS4_Mode.Tag = "HBS Lev.3 Manual process time Mode (sec)";
            txt_HBS4_Max.Tag = "HBS Lev.3 Manual process time Max (sec)";
            txt_HBS4_Operators.Tag = "HBS Lev.3 Operators";

            txt_HBS5_Min.Tag = "HBS Lev.4 process time Min (sec)";
            txt_HBS5_Mode.Tag = "HBS Lev.4 process time Mode (sec)";
            txt_HBS5_Max.Tag = "HBS Lev.4 process time Max (sec)";

            

            BT_CANCEL.Location = new Point(BT_CANCEL.Location.X, BT_CANCEL.Location.Y + 25);
            
            BT_OK.Location = new Point(BT_OK.Location.X, BT_OK.Location.Y + 25);

            #region Anciennes modifications réalisées pour le mode Delhi
            //this.Size = new Size(this.Size.Width, this.Size.Height + 25);
            //gb_HBS2.Size = new Size(gb_HBS2.Size.Width, gb_HBS2.Size.Height + 25);
            //gb_HBS3.Location = new Point(gb_HBS3.Location.X, gb_HBS3.Location.Y + 25);
            //gb_HBS4.Location = new Point(gb_HBS4.Location.X, gb_HBS4.Location.Y + 25);
            //gb_HBS5.Location = new Point(gb_HBS5.Location.X, gb_HBS5.Location.Y + 25);
            //lbl_HBS2_LossRate.Visible = true;
            //txt_HBS2_LossRate.Visible = true;
            #endregion

            gb_HBS4.Size = new Size(322, 87);
            gb_HBS5.Location = new Point(297, 442);
            lbl_HBS4_TimeOut.Visible = false;
            txt_HBS4_TimeOut.Visible = false;

            lbl_HBS2_LossRate.Text = messages[7];
            txt_HBS2_LossRate.Tag = "HBS Lev.2 tracking loss rate (%)";
            txt_HBS2_LossRate.TextChanged -= textBoxTextChanged_Number;
            txt_HBS2_LossRate.TextChanged += new EventHandler(textBoxTextChanged_Percent);
            
            lbl_HBS2_LossRate.Location = new Point(0, 86);

            gb_Infeed.Size = new Size(gb_Infeed.Size.Width, gb_Infeed.Size.Height + 50);

            txt_Transf_H.Tag = "Transfer Infeed 1 operator throughput (bag/h)";
            txt_Arriv_H.Tag = "Arrival Infeed operator throughput (bag/h)";
            lbl_Transfer2.Visible = true;
            lbl_DMRC.Visible = true;
            txt_Transf2_H.Visible = true;
            txt_Transf2_S.Visible = true;
            txt_DMRC_H.Visible = true;
            txt_DMRC_S.Visible = true;

            lbl_Arrival.Location = new Point(lbl_Arrival.Location.X, lbl_Arrival.Location.Y + 25);
            txt_Arriv_H.Location = new Point(157, 84);
            txt_Arriv_S.Location = new Point(209, 84);

            lbl_Transfer1.Location = new Point(4, 35);
            lbl_Transfer1.Text = messages[8];

            gb_Infeed.Text += messages[9];
        }

        #region Gestion des Throughput HBS1 et 3
        private Double CalcThroughput(String sMoyenne, String sSpacing, String sVelocity)
        {
            Double dMoyenne, dSpacing, dVelocity;
            if ((!Double.TryParse(sMoyenne, out dMoyenne)) ||
                (!Double.TryParse(sSpacing, out dSpacing)) ||
                (!Double.TryParse(sVelocity, out dVelocity)))
                return 0;
            if (dVelocity <= 0)
                return 0;
            if (dSpacing <= 0)
                return 0;
            return (3600 / (dMoyenne + (dSpacing / dVelocity)));
        }
        private Double CalcVelocity(String sMoyenne, String sSpacing, String sThroughput)
        {
            Double dMoyenne, dSpacing, dThroughput;
            if ((!Double.TryParse(sMoyenne, out dMoyenne)) ||
                (!Double.TryParse(sSpacing, out dSpacing)) ||
                (!Double.TryParse(sThroughput, out dThroughput)))
                return 0;
            if (dThroughput <= 0)
                return 0;
            Double dTmp = (3600 / dThroughput) - dMoyenne;
            if (dTmp <= 0)
                return 0;
            return dSpacing / dTmp;
        }
        private bool bHBS1 = false;
        private void txt_HBS1_spacing_TextChanged(object sender, EventArgs e)
        {
            if (bHBS1)
                return;
            bHBS1 = true;
            textBoxTextChanged_Number(txt_HBS1_spacing, e);
            textBoxTextChanged_Number(txt_HBS1_Throughput, e);
            textBoxTextChanged_Number(txt_HBS1_Velocity, e);

            if ((txt_HBS1_spacing.BackColor == Color.Red) ||
                (txt_HBS1_Throughput.BackColor == Color.Red) ||
                (txt_HBS1_Velocity.BackColor == Color.Red))
            {
                bHBS1 = false;
                return;
            }
            try
            {
                if (((TextBox)sender).Name == txt_HBS1_Throughput.Name)
                {
                    txt_HBS1_Velocity.Text = Math.Round(CalcVelocity("0", txt_HBS1_spacing.Text, txt_HBS1_Throughput.Text), 2).ToString();
                }
                else
                {
                    txt_HBS1_Throughput.Text = Math.Round(CalcThroughput("0", txt_HBS1_spacing.Text, txt_HBS1_Velocity.Text), 2).ToString();
                }
            }
            catch (Exception exc) 
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02001: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
            }
            bHBS1 = false;
        }

        private bool bHBS3 = false;
        private void txt_HBS3_Spacing_TextChanged(object sender, EventArgs e)
        {
            if (bHBS3)
                return;
            bHBS3 = true;
            textBoxTextChanged_Number(txt_HBS3_Spacing, e);
            textBoxTextChanged_Number(txt_HBS3_throughput, e);
            textBoxTextChanged_Number(txt_HBS3_Velocity, e);
            if ((txt_HBS3_Spacing.BackColor == Color.Red) ||
                (txt_HBS3_throughput.BackColor == Color.Red) ||
                (txt_HBS3_Velocity.BackColor == Color.Red))
            {
                bHBS3 = false;
                return;
            }
            try
            {
                if (((TextBox)sender).Name == txt_HBS3_throughput.Name)
                {
                    txt_HBS3_Velocity.Text = Math.Round(CalcVelocity(txt_HBS3_Mean.Text, txt_HBS3_Spacing.Text, txt_HBS3_throughput.Text), 2).ToString();
                }
                else
                {
                    txt_HBS3_throughput.Text = Math.Round(CalcThroughput(txt_HBS3_Mean.Text, txt_HBS3_Spacing.Text, txt_HBS3_Velocity.Text), 2).ToString();
                }
            }
            catch (Exception exc) 
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02002: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
            }
            bHBS3 = false;
        }
        #endregion

        private void BHS_Process_Assistant_Load(object sender, EventArgs e)
        {

        }

    }
}