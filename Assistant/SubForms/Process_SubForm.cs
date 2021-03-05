using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant.SubForms
{
    public partial class Process_SubForm : Form
    {
        DataTable ProcessTable;
        DataTable dtOneofSpecification_;
        String ancienneSelection = null;
        private SubForms.Distribution_SubForm d_subForm;

        bool bUseSecondDistribution;
        private SubForms.Distribution_SubForm d_subForm2;

        // << Task #8758 Pax2Sim - Editor for Groups
        private SubForms.Distribution_SubForm d_subForm_waitingTimes;
        bool referenceTimeChanged = false;
        // >> Task #8758 Pax2Sim - Editor for Groups

        #region Constructeurs et fonctions pour l'initialisation de la classe.
        private void Initialize(DataTable table, DataTable dtOneofSpecification)
        {
            InitializeComponent();
            dtOneofSpecification_ = dtOneofSpecification;
            OverallTools.FonctionUtiles.MajBackground(this, this.Width,this.Height);
            // << Task #8758 Pax2Sim - Editor for Groups
            referenceTimeChanged = false;
            // >> Task #8758 Pax2Sim - Editor for Groups

            
            /*test*/
            d_subForm = new SubForms.Distribution_SubForm(null, dtOneofSpecification, "", 0, 0, 0);
            d_subForm.TopLevel = false;
            d_subForm.Top = -25;
            //Bitmap.
            Rectangle recDest = new Rectangle(0, 0, d_subForm.Width, d_subForm.Height);
            Rectangle recCrop = new Rectangle(p_Distribution.Left + gb_FirstDistribution.Left, p_Distribution.Top + gb_FirstDistribution.Top, d_subForm.Width, d_subForm.Height);
            d_subForm.BackgroundImage = new Bitmap(d_subForm.Width, d_subForm.Height);
            d_subForm.BackgroundImageLayout = ImageLayout.Stretch;
            Graphics gphCrop = Graphics.FromImage(d_subForm.BackgroundImage);
            gphCrop.DrawImage(this.BackgroundImage, recDest, recCrop, GraphicsUnit.Pixel);


            //((Bitmap)this.BackgroundImage).ro
            //d_subForm.BackColor = PAX2SIM.Couleur1;
            d_subForm.Parent = p_Distribution;
            d_subForm.HideLabel();
            d_subForm.Show();

            // << Task #8758 Pax2Sim - Editor for Groups
            /* The Waiting Times Distribution form*/
            d_subForm_waitingTimes = new SubForms.Distribution_SubForm(null, dtOneofSpecification, "", 0, 0, 0);
            d_subForm_waitingTimes.TopLevel = false;
            d_subForm_waitingTimes.Top = -25;
            recDest = new Rectangle(0, 0, d_subForm_waitingTimes.Width, d_subForm_waitingTimes.Height);
            recCrop = new Rectangle(p_waitingTimeDistribution.Left + gb_waitingTimesDistribution.Left,
                p_waitingTimeDistribution.Top + gb_waitingTimesDistribution.Top,
                d_subForm_waitingTimes.Width, d_subForm_waitingTimes.Height);
            d_subForm_waitingTimes.BackgroundImage = new Bitmap(d_subForm_waitingTimes.Width, d_subForm_waitingTimes.Height);
            d_subForm_waitingTimes.BackgroundImageLayout = ImageLayout.Stretch;
            gphCrop = Graphics.FromImage(d_subForm_waitingTimes.BackgroundImage);
            gphCrop.DrawImage(this.BackgroundImage, recDest, recCrop, GraphicsUnit.Pixel);

            d_subForm_waitingTimes.Parent = p_waitingTimeDistribution;
            d_subForm_waitingTimes.HideLabel();
            d_subForm_waitingTimes.Show();
            // >> Task #8758 Pax2Sim - Editor for Groups

            /*Pour le second sous formulaire*/
            bUseSecondDistribution = false;
            d_subForm2 = new SubForms.Distribution_SubForm(null, dtOneofSpecification, "", 0, 0, 0);
            d_subForm2.TopLevel = false;
            d_subForm2.Top = -25;
            //d_subForm2.Size = new Size(p_SecondDistribution.Size.Width, p_SecondDistribution.Size.Height);
            //d_subForm2.BackColor = PAX2SIM.Couleur1;

            recDest = new Rectangle(0, 0, d_subForm2.Width, d_subForm2.Height);
            recCrop = new Rectangle(p_SecondDistribution.Left + gb_SecondDistribution.Left, p_SecondDistribution.Top + gb_SecondDistribution.Top, d_subForm.Width, d_subForm2.Height);
            d_subForm2.BackgroundImage = new Bitmap(d_subForm2.Width, d_subForm2.Height);
            d_subForm2.BackgroundImageLayout = ImageLayout.Stretch;
            gphCrop = Graphics.FromImage(d_subForm2.BackgroundImage);
            gphCrop.DrawImage(this.BackgroundImage, recDest, recCrop, GraphicsUnit.Pixel);

            d_subForm2.Parent = p_SecondDistribution;
            d_subForm2.HideLabel();
            d_subForm2.Show();
            /*test*/



            ProcessTable = table;
            int iIndexGroupe = ProcessTable.Columns.IndexOf("Items");
            if (iIndexGroupe < 0)
                return;
            foreach (DataRow line in ProcessTable.Rows)
            {
                cb_GroupID.Items.Add(line.ItemArray[iIndexGroupe]);
            }
            // << Task #8758 Pax2Sim - Editor for Groups
            referenceTimeComboBox.Items.Clear();
            referenceTimeComboBox.Items.Add("");
            referenceTimeComboBox.Items.Add(GlobalNames.STA);
            referenceTimeComboBox.Items.Add(GlobalNames.STD);
            // >> Task #8758 Pax2Sim - Editor for Groups
        }
        public Process_SubForm(DataTable table, DataTable dtOneofSpecification)
        {
            Initialize(table, dtOneofSpecification);
        }
        public Process_SubForm(DataTable table, DataTable dtOneofSpecification, String selectedGroup)
        {
            Initialize(table, dtOneofSpecification);
            if (cb_GroupID.Items.Contains(selectedGroup))
            {
                cb_GroupID.Text = selectedGroup;
                cb_GroupID.Enabled = false;
            }
        }
        #endregion

        #region La fonction pour afficher dans la fenêtre les informations contenues dans la table.
        private void cb_GroupID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iIndexNomDistribution;
            int iIndexParam1;
            int iIndexParam2;
            int iIndexParam3;
            DataRow ligne;
            // << Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)            
            if ((ancienneSelection != cb_GroupID.Text) && (ancienneSelection != null)
                && (d_subForm.getValeursOntChangees() || d_subForm2.getValeursOntChangees()
                    || d_subForm_waitingTimes.getValeursOntChangees() || referenceTimeChanged))
            // >> Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
            {
                DialogResult dg = MessageBox.Show("Do you want to save the changes for \"" + ancienneSelection + "\" ?", "Save changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dg == DialogResult.Cancel)
                {
                    cb_GroupID.Text = ancienneSelection;
                    return;
                }
                else if (dg == DialogResult.Yes)
                {
                    if (!saveChanges())
                    {
                        cb_GroupID.Text = ancienneSelection;
                        return;
                    }
                }
            }
            else if (ancienneSelection == cb_GroupID.Text)
            {
                return;
            }


            int[] SelectedGroup = OverallTools.DataFunctions.AnalyzeGroupName(cb_GroupID.Text);
            if (SelectedGroup == null)
                return;

            bUseSecondDistribution = (SelectedGroup[2] == GestionDonneesHUB2SIM.BaggageClaimGroup) ||
                (SelectedGroup[2] == GestionDonneesHUB2SIM.CheckInGroup) ||
                (SelectedGroup[2] == GestionDonneesHUB2SIM.PassportCheckGroup);
            gb_SecondDistribution.Visible = bUseSecondDistribution;
            iIndexNomDistribution = ProcessTable.Columns.IndexOf("Distrib_1");
            iIndexParam1 = ProcessTable.Columns.IndexOf("Param1_1");
            iIndexParam2 = ProcessTable.Columns.IndexOf("Param2_1");
            iIndexParam3 = ProcessTable.Columns.IndexOf("Param3_1");
            ligne = GestionDonneesHUB2SIM.getLine(ProcessTable, 0, cb_GroupID.Text);
            if (ligne == null)
                return;
            d_subForm.setValue(ligne.ItemArray[iIndexNomDistribution].ToString(),
                (Double)ligne.ItemArray[iIndexParam1],
                (Double)ligne.ItemArray[iIndexParam2],
                (Double)ligne.ItemArray[iIndexParam3]);

            // << Task #8758 Pax2Sim - Editor for Groups
            int indexReferenceTime = ProcessTable.Columns.IndexOf(GlobalNames.sProcessTable_WaitingTimeReference);
            iIndexNomDistribution = ProcessTable.Columns.IndexOf(GlobalNames.sProcessTable_Distrib_3);
            iIndexParam1 = ProcessTable.Columns.IndexOf(GlobalNames.sProcessTable_Param1_3);
            iIndexParam2 = ProcessTable.Columns.IndexOf(GlobalNames.sProcessTable_Param2_3);
            iIndexParam3 = ProcessTable.Columns.IndexOf(GlobalNames.sProcessTable_Param3_3);

            d_subForm_waitingTimes.setValue(ligne.ItemArray[iIndexNomDistribution].ToString(),
                (Double)ligne.ItemArray[iIndexParam1],
                (Double)ligne.ItemArray[iIndexParam2],
                (Double)ligne.ItemArray[iIndexParam3]);
            if (indexReferenceTime != -1)
            {
                String referenceTime = ligne.ItemArray[indexReferenceTime].ToString();
                if (referenceTime == GlobalNames.STA || referenceTime == GlobalNames.STD)
                    referenceTimeComboBox.Text = referenceTime;
                else
                    referenceTimeComboBox.Text = "";
                referenceTimeChanged = false;
            }
            // >> Task #8758 Pax2Sim - Editor for Groups

            if (bUseSecondDistribution)
            {
                if ((SelectedGroup[2] == GestionDonneesHUB2SIM.BaggageClaimGroup) ||
                (SelectedGroup[2] == GestionDonneesHUB2SIM.CheckInGroup))
                {
                    gb_FirstDistribution.Text = "Passenger distribution (s)"  ;
                    gb_SecondDistribution.Text = "Baggage distribution (s)";
                }
                else
                {
                    gb_FirstDistribution.Text = "Local Passenger distribution (s)";
                    gb_SecondDistribution.Text = "Not Local Passenger distribution (s)";
                }

                iIndexNomDistribution = ProcessTable.Columns.IndexOf("Distrib_2");
                iIndexParam1 = ProcessTable.Columns.IndexOf("Param1_2");
                iIndexParam2 = ProcessTable.Columns.IndexOf("Param2_2");
                iIndexParam3 = ProcessTable.Columns.IndexOf("Param3_2");

                d_subForm2.setValue(ligne.ItemArray[iIndexNomDistribution].ToString(),
                    (Double)ligne.ItemArray[iIndexParam1],
                    (Double)ligne.ItemArray[iIndexParam2],
                    (Double)ligne.ItemArray[iIndexParam3]);
            }
            else
            {
                d_subForm2.setValue("",
                    0,
                    0,
                    0);
            }
            ancienneSelection = cb_GroupID.Text;
        }
        #endregion

        #region Fonctions pour vérifier la sauvegarde des informations
        internal bool saveChanges()
        {
            DataRow ligne = GestionDonneesHUB2SIM.getLine(ProcessTable, 0, ancienneSelection);
            int iIndexNomDistributionF = ProcessTable.Columns.IndexOf("Distrib_1");
            int iIndexParam1F = ProcessTable.Columns.IndexOf("Param1_1");
            int iIndexParam2F = ProcessTable.Columns.IndexOf("Param2_1");
            int iIndexParam3F = ProcessTable.Columns.IndexOf("Param3_1");

            int iIndexNomDistributionS = ProcessTable.Columns.IndexOf("Distrib_2");
            int iIndexParam1S = ProcessTable.Columns.IndexOf("Param1_2");
            int iIndexParam2S = ProcessTable.Columns.IndexOf("Param2_2");
            int iIndexParam3S = ProcessTable.Columns.IndexOf("Param3_2");

            // << Task #8758 Pax2Sim - Editor for Groups
            int indexReferenceTime = ProcessTable.Columns.IndexOf(GlobalNames.sProcessTable_WaitingTimeReference);
            int indexWaitingTimeDistributionName = ProcessTable.Columns.IndexOf(GlobalNames.sProcessTable_Distrib_3);
            int indexParam1_3 = ProcessTable.Columns.IndexOf(GlobalNames.sProcessTable_Param1_3);
            int indexParam2_3 = ProcessTable.Columns.IndexOf(GlobalNames.sProcessTable_Param2_3);
            int indexParam3_3 = ProcessTable.Columns.IndexOf(GlobalNames.sProcessTable_Param3_3);
            // >> Task #8758 Pax2Sim - Editor for Groups
            
            if ((!d_subForm.estValide()) || (bUseSecondDistribution && (!d_subForm2.estValide()))
                || !d_subForm_waitingTimes.estValide())     // << Task #8758 Pax2Sim - Editor for Groups
            {
                MessageBox.Show("Please fill all the blank with numerical values", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            ligne[iIndexNomDistributionF] = d_subForm.getDistribution();
            ligne[iIndexParam1F] = d_subForm.getFirstParam();
            ligne[iIndexParam2F] = d_subForm.getSecondParam();
            ligne[iIndexParam3F] = d_subForm.getThirdParam();

            if (bUseSecondDistribution)
            {
                ligne[iIndexNomDistributionS] = d_subForm2.getDistribution();
                ligne[iIndexParam1S] = d_subForm2.getFirstParam();
                ligne[iIndexParam2S] = d_subForm2.getSecondParam();
                ligne[iIndexParam3S] = d_subForm2.getThirdParam();
            }
            else
            {
                ligne[iIndexNomDistributionS] = "";
                ligne[iIndexParam1S] =0;
                ligne[iIndexParam2S] = 0;
                ligne[iIndexParam3S] = 0;
            }
            // << Task #8758 Pax2Sim - Editor for Groups
            ligne[indexReferenceTime] = referenceTimeComboBox.Text;
            ligne[indexWaitingTimeDistributionName] = d_subForm_waitingTimes.getDistribution();
            ligne[indexParam1_3] = d_subForm_waitingTimes.getFirstParam();
            ligne[indexParam2_3] = d_subForm_waitingTimes.getSecondParam();
            ligne[indexParam3_3] = d_subForm_waitingTimes.getThirdParam();

            referenceTimeChanged = false;
            // >> Task #8758 Pax2Sim - Editor for Groups

            return true;
        }
        #endregion

        #region Fonction pour sauvegarder les informartions lorssque l'on quitte.
        private void btn_Ok_Click(object sender, EventArgs e)
        {            
            // << Task #8758 Pax2Sim - Editor for Groups            
            if ((ancienneSelection != null) 
                && ((d_subForm.getValeursOntChangees() || d_subForm2.getValeursOntChangees())
                    || d_subForm_waitingTimes.getValeursOntChangees() || referenceTimeChanged))
            // >> Task #8758 Pax2Sim - Editor for Groups
            {
                DialogResult dg = MessageBox.Show("Do you want to save the changes for \"" + cb_GroupID.Text + "\" ?", "Save changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dg == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.None;
                    return;
                }
                else if (dg == DialogResult.Yes)
                {
                    if (!saveChanges())
                    {
                        DialogResult = DialogResult.None;
                        return;
                    }
                }
            }
        }
        #endregion

        private void Process_SubForm_Validating(object sender, CancelEventArgs e)
        {            
            // << Task #8758 Pax2Sim - Editor for Groups            
            if ((!d_subForm.estValide()) || (gb_SecondDistribution.Visible && (!d_subForm2.estValide()))
                || !d_subForm_waitingTimes.estValide())
            // >> Task #8758 Pax2Sim - Editor for Groups
            {                
                MessageBox.Show("Please fill all the blank with numerical values" 
                    , "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel= true;
            }
        }

        // << Task #8758 Pax2Sim - Editor for Groups                
        private void referenceTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            referenceTimeChanged = true;
            if (referenceTimeComboBox.Text != GlobalNames.STA && referenceTimeComboBox.Text != GlobalNames.STD)
            {
                p_waitingTimeDistribution.Enabled = false;
                d_subForm_waitingTimes.setValue(GlobalNames.CONSTANT_DISTRIBUTION, 0, 0, 0);
            }
            else
            {
                p_waitingTimeDistribution.Enabled = true;
            }
        }
        // >> Task #8758 Pax2Sim - Editor for Groups
    }
}