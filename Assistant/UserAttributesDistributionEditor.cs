using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant
{
    public partial class UserAttributesDistributionEditor : Form
    {
        DataTable distributionTable;
        Label[] lbls_Nb;
        TextBox[] mtb_Values;
        bool bAModifieValeurs;
        bool bAModifieStructure;
        int iStartIndex;
        const int startXFirst = 65;
        const int startXThird = 202;
        const int startY = 10;
        const int IncrementY = 20;
        String ancienneSelection;
        int nbOfLabels = 0;
        // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
        string[] nonFlightCategoryColumnNamesList = new string[] { GlobalNames.sUserAttributes_DistributionTableColumnName_Value,
            GlobalNames.sUserAttributesBaggLoadingDelayValueColumnName, GlobalNames.sUserAttributesBaggLoadingRateValueColumnName,
            GlobalNames.userAttributesReclaimLogValueColumnName,
            GlobalNames.userAttributesEBSInputRateValueColumnName, GlobalNames.userAttributesEBSOutputRateValueColumnName,
            GlobalNames.flightSubcategoryColumnName};
        // >> Task #9504 Pax2Sim - flight subCategories - peak flow segregation

        private void Initialize(DataTable table_)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            this.cbFlightCategories.SelectedIndexChanged += new System.EventHandler(this.cb_FC_SelectedIndexChanged);
            this.btnOk.Click += new System.EventHandler(this.BT_OK_Click);
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            distributionTable = table_;

            iStartIndex = 0;
            this.Text = "Modify " + distributionTable.TableName;
            lbls_Nb = new Label[100];
            mtb_Values = new TextBox[100];

            newValue.BackColor = Color.White;

            if (distributionTable.Columns.Count != 1)
            {
                foreach (DataColumn colonne in distributionTable.Columns)
                {
                    if (onlyFlightCategoriesFromDistributionTableColumns(colonne.ColumnName))
                        cbFlightCategories.Items.Add(colonne.ColumnName);
                }
            }
            ancienneSelection = "";

            bAModifieValeurs = true;
            bAModifieStructure = true;

            bAModifieValeurs = false;
            bAModifieStructure = false;
            // >> Task #10069 Pax2Sim - no BNP development
            if (PAX2SIM.usaMode)
            {
                // >> Task #9915 Pax2Sim - BNP development - Peak Flows - USA Standard directory
                if (distributionTable.TableName.Equals(GlobalNames.flightSubcategoriesTableName))
                {
                    label4.Visible = false;
                    newValue.Visible = false;
                    btnAddNewValue.Visible = false;
                    btnReset.Visible = false;
                }

                // << Task #9915 Pax2Sim - BNP development - Peak Flows - USA Standard directory
            }
        }

        private bool onlyFlightCategoriesFromDistributionTableColumns(String distributionTableColumnName)
        {
            if (!nonFlightCategoryColumnNamesList.Contains(distributionTableColumnName))    // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
                return true;
            return false;
        }

        public UserAttributesDistributionEditor(DataTable table)
        {
            Initialize(table);
            cbFlightCategories.SelectedIndex = 0;
        }

        public UserAttributesDistributionEditor(DataTable table, String FC)
        {
            Initialize(table);
            cbFlightCategories.SelectedItem = FC;
        }


        #region Functions used to validate the value obtained from the procentage text boxes
        private void Poucentage_Validating(object sender, CancelEventArgs e)
        {
            Double dValue = ParsePourcentage(((TextBox)sender).Text);
            if ((dValue > 100) || (dValue < 0))
            {
                ((TextBox)sender).BackColor = Color.Red;
                //MessageBox.Show("You must select a value between 0% and 100%", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
            else
            {
                if (((TextBox)sender).BackColor == Color.Red)
                    ((TextBox)sender).BackColor = Color.White;
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

        #region Function used to add a new line
        private Label createLabel(int iIndex)
        {

            int x = startXFirst;
            int y = startY + (iIndex * IncrementY);
            Label result = new Label();
            result.BackColor = Color.Transparent;
            result.ForeColor = Color.Black;
            result.Location = new Point(x, y);
            result.Parent = pContent;
            result.Size = new Size(120, 13);
            return result;
        }
        private TextBox createTextBox(int iIndex)
        {
            int x = startXThird;
            int y = startY + (iIndex * IncrementY) - 3;
            TextBox result = new TextBox();
            result.BackColor = Color.White;
            result.ForeColor = Color.Black;
            result.Location = new Point(x, y);
            result.Parent = pContent;
            result.Size = new Size(54, 20);
            result.TabIndex = iIndex + 5;
            result.Text = "0";
            result.TextChanged += new EventHandler(CheckSum);
            result.Validating += new CancelEventHandler(Poucentage_Validating);
            return result;
        }
        private void CheckSum(object sender, EventArgs e)
        {
            bAModifieValeurs = true;
            double value = 0;
            Double mean = 0;
            Double dTmp;
            for (int i = 0; i < mtb_Values.Length; i++)
            {
                if (mtb_Values[i] != null)
                {
                    dTmp = ParsePourcentage(mtb_Values[i].Text);
                    if (dTmp != -1)
                    {
                        value += dTmp;
                        mean += dTmp * i;
                        mtb_Values[i].BackColor = Color.White;
                    }
                    else
                    {

                        mtb_Values[i].BackColor = Color.Red;
                    }
                }
            }
            if (mtb_Values.Length > 0 && mtb_Values[0] != null)
                mean = mean / (100);
            if (value > 100)
            {
                ((TextBox)sender).BackColor = Color.Red;
            }
            else
            {
                // >> Task #9915 Pax2Sim - BNP development - Peak Flows - USA Standard directory
                // need to bypass this check because for BNP FC are FSC.
                if (!distributionTable.TableName.Equals(GlobalNames.flightSubcategoriesTableName))
                    ((TextBox)sender).BackColor = Color.White;
                //((TextBox)sender).BackColor = Color.White;
                // << Task #9915 Pax2Sim - BNP development - Peak Flows - USA Standard directory
            }
            if (value == 100
                || distributionTable.TableName.Equals(GlobalNames.flightSubcategoriesTableName)) // >> Task #9915 Pax2Sim - BNP development - Peak Flows - USA Standard directory
            {
                foreach (TextBox mtb in mtb_Values)
                {
                    if (mtb != null)
                        mtb.BackColor = Color.LightGreen;
                }
            }
            else if (value > 100)
            {
                foreach (TextBox mtb in mtb_Values)
                {
                    mtb.BackColor = Color.Red;
                }
            }


            lbl_Sum.Text = "Sum" + ": " + Math.Round(value, 2).ToString();
            lbl_Mean.Text = "Average Index" + ": " + Math.Round(mean, 2).ToString();
        }
        #endregion

        #region Functions used to manage the interaction with the Flight categories list
        private void cb_FC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ancienneSelection == cbFlightCategories.Text)
                return;
            BT_OK_Click(null, null);
            if (ancienneSelection == cbFlightCategories.Text)
                return;
            if (!distributionTable.Columns.Contains(cbFlightCategories.Text))
            {
                MessageBox.Show("Please check the Flight Categories.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int iIndexLigne = distributionTable.Columns.IndexOf(cbFlightCategories.Text);
            int nbOfRows = distributionTable.Rows.Count;
            resetNumberOfRows(nbOfRows);
            nbOfLabels = nbOfRows;
            for (int i = 0; i < distributionTable.Rows.Count; i++)
            {
                lbls_Nb[i].Text = distributionTable.Rows[i].ItemArray[0].ToString();
                mtb_Values[i].Text = ConvertToPercent(distributionTable.Rows[i].ItemArray[iIndexLigne].ToString());
            }
            ancienneSelection = cbFlightCategories.Text;
            bAModifieValeurs = false;
        }

        private String ConvertToPercent(String sValue)
        {
            Double Value;
            if (!Double.TryParse(sValue, out Value))
            {
                return "0";
            }
            return Value.ToString();
        }
        #endregion

        #region Functions used to save the information in the table.
        private void BT_OK_Click(object sender, EventArgs e)
        {
            if ((ancienneSelection != "") && (bAModifieValeurs))
            {
                if (mtb_Values[0].BackColor != Color.LightGreen)
                {
                    MessageBox.Show("Please check the distribution.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (e != null)
                        DialogResult = DialogResult.None;
                    return;
                }
                if ((ancienneSelection != cbFlightCategories.Text) || (DialogResult == DialogResult.OK))
                {
                    DialogResult drSave = MessageBox.Show("Do you want to save changes?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (drSave == DialogResult.Cancel)
                    {
                        cbFlightCategories.Text = ancienneSelection;
                        if (e != null)
                            DialogResult = DialogResult.None;
                    }
                    else if (drSave == DialogResult.No)
                    {
                        if (e != null)
                            DialogResult = DialogResult.None;
                        int nbOfRows = distributionTable.Rows.Count;
                        // >> Task #10069 Pax2Sim - no BNP development
                        if (!PAX2SIM.usaMode)
                            resetNumberOfRows(nbOfRows);// >> Task #10035 Pax2Sim - BNP development - Data Input tables

                    }
                    else if (drSave == DialogResult.Yes)
                    {
                        SaveFlightCategorie(ancienneSelection);
                    }

                }
            }
        }

        private void SaveFlightCategorie(string ancienneSelection)
        {
            if (bAModifieStructure)
            {
                DataTable ancienneTable = distributionTable.Copy();
                distributionTable.Clear();
                for (int i = 0; i < mtb_Values.Length; i++)
                {
                    if (mtb_Values[i] != null)
                    {
                        DataRow newLigne = distributionTable.NewRow();
                        newLigne[0] = lbls_Nb[i].Text;
                        for (int j = 1; j < distributionTable.Columns.Count; j++)
                        {
                            if (ancienneTable.Rows.Count > i)
                                newLigne[j] = ancienneTable.Rows[i].ItemArray[j];
                            else
                                newLigne[j] = 0;
                        }
                        distributionTable.Rows.Add(newLigne);
                    }
                }
                bAModifieStructure = false;
            }
            int iIndexColonne = distributionTable.Columns.IndexOf(ancienneSelection);
            for (int i = 0; i < mtb_Values.Length; i++)
            {
                if (mtb_Values[i] != null)
                {
                    if (i < distributionTable.Rows.Count)    // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
                    {
                        distributionTable.Rows[i].BeginEdit();
                        distributionTable.Rows[i][iIndexColonne] = Math.Round(ParsePourcentage(mtb_Values[i].Text) /*/ 100*/, 2);
                    }
                }
            }
            distributionTable.AcceptChanges();

        }

        private void resetNumberOfRows(int nbOfRows)
        {
            if (lbls_Nb.Length > 0)
                for (int i = 0; i < lbls_Nb.Length; i++)
                    if (lbls_Nb[i] != null)
                        lbls_Nb[i].Dispose();
            if (mtb_Values.Length > 0)
                for (int i = 0; i < mtb_Values.Length; i++)
                    if (mtb_Values[i] != null)
                        mtb_Values[i].Dispose();
            for (int i = 0; i < nbOfRows; i++)
            {
                lbls_Nb[i] = createLabel(i);
                mtb_Values[i] = createTextBox(i);
            }
            bAModifieStructure = true;
        }
        #endregion

        Label[] labelList = new Label[100];
        TextBox[] textBoxList = new TextBox[100];
        private void btnAddNewValue_Click(object sender, EventArgs e)
        {
            nbOfLabels++;
            //check that the value exists and it doesn't repeat            
            if (newValue.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please insert a value.");
                return;
            }
            if (nbOfLabels > 1)
            {
                for (int i = 0; i < nbOfLabels; i++)
                {
                    if (lbls_Nb[i] != null && lbls_Nb[i].Text == newValue.Text)
                    {
                        MessageBox.Show("The value has already been inserted.");
                        nbOfLabels--;
                        return;
                    }
                }
            }
            this.SuspendLayout();
            //if the table has data we store them in the labelList and textBoxList
            //these variables then are used to add the new data from the user and
            // then store everything back in the lbls_Nb and mtb_Values which are used 
            // for other calculations
            labelList = lbls_Nb;
            textBoxList = mtb_Values;
            labelList[nbOfLabels - 1] = createLabel(nbOfLabels - 1);
            textBoxList[nbOfLabels - 1] = createTextBox(nbOfLabels - 1);
            lbls_Nb = labelList;
            mtb_Values = textBoxList;

            if (lbls_Nb != null && nbOfLabels > 8)
            {
                pContent.VerticalScroll.Visible = true;
                pContent.VerticalScroll.Enabled = true;
                pContent.ScrollControlIntoView(lbls_Nb[0]);
            }
            lbls_Nb[nbOfLabels - 1].Text = newValue.Text;

            this.ResumeLayout();
            CheckSum(mtb_Values[0], null);
            bAModifieStructure = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            nbOfLabels = 0;
            resetNumberOfRows(0);
            lbls_Nb = new Label[100];
            mtb_Values = new TextBox[100];
        }


    }
}