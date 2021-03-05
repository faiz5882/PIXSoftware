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
    public partial class usaStandardParamAssistant : Form
    {
        DataTable usaStandardParametersTable;

        #region column indexes
        int oogPercentColumnIndex = -1;
        int osPercentColumnIndex = -1;
        int edsScreenRatePerMinColumnIndex = -1;
        int edsScreenRatePerHourColumnIndex = -1;
        int falseAlarmEdsDomColumnIndex = -1;
        int falseAlarmEdsIntColumnIndex = -1;
        int osrProcessingRatePerHourColumnIndex = -1;
        int clearRateColumnIndex = -1;
        int etdScreenRateDomWithImageColumnIndex = -1;
        int etdScreenRateDomNoImageColumnIndex = -1;
        int etdScreenRateIntWithImageColumnIndex = -1;
        int etdScreenRateIntNoImageColumnIndex = -1;
        int lostTrackOSColumnIndex = -1;
        int lostTrackOOGColumnIndex = -1;
        int etdScreenRateOSBagsPerHourColumnIndex = -1;
        #endregion

        public usaStandardParamAssistant(DataTable pUsaStandardParametersTable)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            usaStandardParametersTable = pUsaStandardParametersTable;
            setColumnIndexesForUsaStandardParametersTable();
            loadDataFromTable();
        }

        private void setColumnIndexesForUsaStandardParametersTable()
        {
            if (usaStandardParametersTable != null)
            {
                oogPercentColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_OOG_PERCENT_PARAM_NAME);
                osPercentColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_OS_PERCENT_PARAM_NAME);
                edsScreenRatePerMinColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_EDS_SCREENING_RATE_PER_MINUTE_PARAM_NAME);
                edsScreenRatePerHourColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_EDS_SCREENING_RATE_PER_HOUR_PARAM_NAME);
                falseAlarmEdsDomColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_FALSE_ALARM_EDS_DOM_PARAM_NAME);
                falseAlarmEdsIntColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_FALSE_ALARM_EDS_INT_PARAM_NAME);
                osrProcessingRatePerHourColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_OSR_PROCESSING_RATE_BPH_PARAM_NAME);
                clearRateColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_CLEAR_RATE_PARAM_NAME);
                etdScreenRateDomWithImageColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_ETD_SCREENING_RATE_DOM_WITH_IMAGE_PARAM_NAME);
                etdScreenRateDomNoImageColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_ETD_SCREENING_RATE_DOM_NO_IMAGE_PARAM_NAME);
                etdScreenRateIntWithImageColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_ETD_SCREENING_RATE_INT_WITH_IMAGE_PARAM_NAME);
                etdScreenRateIntNoImageColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_ETD_SCREENING_RATE_INT_NO_IMAGE_PARAM_NAME);
                lostTrackOSColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_RATE_LOST_TRACK_OS_PARAM_NAME);
                //lostTrackOOGColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_RATE_LOST_TRACK_OOG_PARAM_NAME);
                etdScreenRateOSBagsPerHourColumnIndex = usaStandardParametersTable.Columns.IndexOf(GlobalNames.USA_STANDARD_ETD_SCREENING_RATE_OS_BAGS_BPH_PARAM_NAME);
            }
        }

        private bool allCollumnsPresentInUsaStandardParametersTable()
        {
            if (oogPercentColumnIndex == -1 || osPercentColumnIndex == -1 || edsScreenRatePerMinColumnIndex == -1 || edsScreenRatePerHourColumnIndex == -1
                || falseAlarmEdsDomColumnIndex == -1 || falseAlarmEdsIntColumnIndex == -1 || osrProcessingRatePerHourColumnIndex == -1 || clearRateColumnIndex == -1
                || etdScreenRateDomWithImageColumnIndex == -1 || etdScreenRateDomNoImageColumnIndex == -1 || etdScreenRateIntWithImageColumnIndex == -1
                || etdScreenRateIntNoImageColumnIndex == -1 || lostTrackOSColumnIndex == -1 
                //|| lostTrackOOGColumnIndex == -1 
                || etdScreenRateOSBagsPerHourColumnIndex == -1)
            {
                return false;
            }
            return true;
        }

        private void loadDataFromTable()
        {
            if (usaStandardParametersTable != null && usaStandardParametersTable.Rows.Count == 1
                && allCollumnsPresentInUsaStandardParametersTable())
            {
                DataRow row = usaStandardParametersTable.Rows[0];

                tb_oogPercent.Text = row[oogPercentColumnIndex].ToString();
                tb_osPercent.Text = row[osPercentColumnIndex].ToString();
                tb_edsScreenRatePerMin.Text = row[edsScreenRatePerMinColumnIndex].ToString();
                tb_edsScreenRatePerHour.Text = row[edsScreenRatePerHourColumnIndex].ToString();
                tb_falseAlarmEdsDom.Text = row[falseAlarmEdsDomColumnIndex].ToString();
                tb_falseAlarmEdsInt.Text = row[falseAlarmEdsIntColumnIndex].ToString();
                tb_osrProcessingRatePerHour.Text = row[osrProcessingRatePerHourColumnIndex].ToString();
                tb_clearRate.Text = row[clearRateColumnIndex].ToString();
                tb_etdScreenRateDomImg.Text = row[etdScreenRateDomWithImageColumnIndex].ToString();
                tb_etdScreenRateDomNoImg.Text = row[etdScreenRateDomNoImageColumnIndex].ToString();
                tb_etdScreenRateIntImg.Text = row[etdScreenRateIntWithImageColumnIndex].ToString();
                tb_etdScreenRateIntNoImg.Text = row[etdScreenRateIntNoImageColumnIndex].ToString();
                //tb_lostInTrackingOOG.Text = row[lostTrackOOGColumnIndex].ToString();
                tb_lostInTrackingOS.Text = row[lostTrackOSColumnIndex].ToString();
                tb_etdScreenRateForOS.Text = row[etdScreenRateOSBagsPerHourColumnIndex].ToString();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (usaStandardParametersTable != null)
            {
                if (DialogResult == DialogResult.OK)
                {
                    DialogResult drSave = MessageBox
                        .Show("Do you want to save the changes ? ", "Information"
                        , MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (drSave == DialogResult.Cancel)
                    {
                        if (e != null)
                            DialogResult = DialogResult.None;
                    }
                    else if (drSave == DialogResult.Yes)
                    {
                        if (!saveTableChanges())
                            DialogResult = DialogResult.None;
                    }
                }
            }
        }

        private bool saveTableChanges()
        {
            if (usaStandardParametersTable != null && usaStandardParametersTable.Rows.Count > 0
                && allCollumnsPresentInUsaStandardParametersTable())
            {
                
                Double oogPercent = 0;
                Double osPercent = 0;
                Double edsScreenRatePerMin = 0;
                Double edsScreenRatePerHour = 0;
                Double falseAlarmEdsDom = 0;
                Double falseAlarmEdsInt = 0;
                Double osrProcessingRatePerHour = 0;
                Double clearRate = 0;
                Double etdScreenRateDomWithImage = 0;
                Double etdScreenRateDomNoImage = 0;
                Double etdScreenRateIntWithImage = 0;
                Double etdScreenRateIntNoImage = 0;
                Double lostTrackOS = 0;
                Double lostTrackOOG = 0;
                Double etdScreenRateOSBagsPerHour = 0;

                if (Double.TryParse(tb_oogPercent.Text, out oogPercent)
                    && Double.TryParse(tb_osPercent.Text, out osPercent)
                    && Double.TryParse(tb_edsScreenRatePerMin.Text, out edsScreenRatePerMin)
                    && Double.TryParse(tb_edsScreenRatePerHour.Text, out edsScreenRatePerHour)
                    && Double.TryParse(tb_falseAlarmEdsDom.Text, out falseAlarmEdsDom)
                    && Double.TryParse(tb_falseAlarmEdsInt.Text, out falseAlarmEdsInt)
                    && Double.TryParse(tb_osrProcessingRatePerHour.Text, out osrProcessingRatePerHour)
                    && Double.TryParse(tb_clearRate.Text, out clearRate)
                    && Double.TryParse(tb_etdScreenRateDomImg.Text, out etdScreenRateDomWithImage)
                    && Double.TryParse(tb_etdScreenRateDomNoImg.Text, out etdScreenRateDomNoImage)
                    && Double.TryParse(tb_etdScreenRateIntImg.Text, out etdScreenRateIntWithImage)
                    && Double.TryParse(tb_etdScreenRateIntNoImg.Text, out etdScreenRateIntNoImage)
                    //&& Double.TryParse(tb_lostInTrackingOOG.Text, out lostTrackOOG)
                    && Double.TryParse(tb_lostInTrackingOS.Text, out lostTrackOS)
                    && Double.TryParse(tb_etdScreenRateForOS.Text, out etdScreenRateOSBagsPerHour)
                    )
                {


                    usaStandardParametersTable.Rows[0][oogPercentColumnIndex] = oogPercent;
                    usaStandardParametersTable.Rows[0][osPercentColumnIndex] = osPercent;
                    usaStandardParametersTable.Rows[0][edsScreenRatePerMinColumnIndex] = edsScreenRatePerMin;
                    usaStandardParametersTable.Rows[0][edsScreenRatePerHourColumnIndex] = edsScreenRatePerHour;

                    usaStandardParametersTable.Rows[0][falseAlarmEdsDomColumnIndex] = falseAlarmEdsDom;
                    usaStandardParametersTable.Rows[0][falseAlarmEdsIntColumnIndex] = falseAlarmEdsInt;
                    usaStandardParametersTable.Rows[0][osrProcessingRatePerHourColumnIndex] = osrProcessingRatePerHour;
                    usaStandardParametersTable.Rows[0][clearRateColumnIndex] = clearRate;

                    usaStandardParametersTable.Rows[0][etdScreenRateDomWithImageColumnIndex] = etdScreenRateDomWithImage;
                    usaStandardParametersTable.Rows[0][etdScreenRateDomNoImageColumnIndex] = etdScreenRateDomNoImage;
                    usaStandardParametersTable.Rows[0][etdScreenRateIntWithImageColumnIndex] = etdScreenRateIntWithImage;
                    usaStandardParametersTable.Rows[0][etdScreenRateIntNoImageColumnIndex] = etdScreenRateIntNoImage;

                    //usaStandardParametersTable.Rows[0][lostTrackOOGColumnIndex] = lostTrackOOG;
                    usaStandardParametersTable.Rows[0][lostTrackOSColumnIndex] = lostTrackOS;
                    usaStandardParametersTable.Rows[0][etdScreenRateOSBagsPerHourColumnIndex] = etdScreenRateOSBagsPerHour;                    
                }
                else
                {
                    MessageBox.Show("Please use only numbers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (sender != null && (TextBox)sender != null)
            {
                String value = ((TextBox)sender).Text;
                Double numberValue = 0;
                if (!Double.TryParse(value, out numberValue))
                    MessageBox.Show("Please use only numbers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// The user set the Screen Rate per Hour. Therefor the input per minute will be autocompleted.
        /// </summary>
        /// <param name="sender">The text box for Screen Rate per Hour</param>
        /// <param name="e"></param>
        private void edsScreenRatesPerHourInput(object sender, EventArgs e)
        {
            if (sender != null && (TextBox)sender != null)
            {
                TextBox screenRatePerHourTextBox = (TextBox)sender;
                String rawScreenRatePerHour = screenRatePerHourTextBox.Text;
                Double screenRatePerHour = 0;

                if (Double.TryParse(rawScreenRatePerHour, out screenRatePerHour))
                {
                    Double screenRatePerMinute = Math.Round(screenRatePerHour / 60, 2);
                    tb_edsScreenRatePerMin.Text = screenRatePerMinute.ToString();                    
                }
            }
        }

        /// <summary>
        /// The user set the Screen Rate per Minute. Therefor the input per hour will be autocompleted.
        /// </summary>
        /// <param name="sender">The text box for Screen Rate per Minute</param>
        /// <param name="e"></param>
        private void edsScreenRatesPerMinuteInput(object sender, EventArgs e)
        {
            if (sender != null && (TextBox)sender != null)
            {
                TextBox screenRatePerMinTextBox = (TextBox)sender;
                String rawScreenRatePerMin = screenRatePerMinTextBox.Text;
                Double screenRatePerMin = 0;

                if (Double.TryParse(rawScreenRatePerMin, out screenRatePerMin))
                {
                    Double screenRatePerHour = Math.Round(screenRatePerMin * 60, 2);
                    tb_edsScreenRatePerHour.Text = screenRatePerHour.ToString();                    
                }
            }
        }


    }
}
