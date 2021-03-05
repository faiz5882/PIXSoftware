using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.com.crispico.ReportTreeView
{
    public partial class CloneGraphicFilterNameEditor : Form
    {
        internal string chartName
        {
            get
            {
                return chartNameTextBox.Text;
            }
        }

        internal string chartTitle
        {
            get
            {
                return chartTitleTextBox.Text;
            }
            set
            {
                chartTitleTextBox.Text = value;
            }
        }

        internal bool changeChartTitle
        {
            get
            {
                return changeTitleCheckBox.Checked;
            }
        }

        private GestionDonneesHUB2SIM donnees;
        private string initialChartTitle;

        public CloneGraphicFilterNameEditor(string _prototypeChartName, string _chartProposedName, string _chartTitle,
            GestionDonneesHUB2SIM _donnees)
        {
            initComp();
            chartTitle = _chartTitle;
            donnees = _donnees;

            this.Text = "Chart name editor - cloned using: " + _prototypeChartName;

            proposeChartName(_chartProposedName);
            setChartTitleOptions(chartTitle);
            initialChartTitle = _chartTitle;
        }

        private void initComp()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
        }
        
        private void proposeChartName(string chartInitialName)
        {
            if (chartInitialName != null && chartInitialName != "")
                chartNameTextBox.Text = chartInitialName;            
        }

        private void setChartTitleOptions(string chartTitle)
        {
            if (chartTitle != null && chartTitle != "")
            {
                chartTitleTextBox.Text = chartTitle;
                changeTitleCheckBox.Checked = false;
                chartTitleTextBox.Enabled = false;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (donnees != null)
            {
                if (donnees.GraphicFilterExist(chartName))
                {
                    MessageBox.Show(this, "The selected name for the chart already exist. Please change the chart name.",
                        "Unique Chart Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    DialogResult = DialogResult.None;
                }
            }
        }

        private void changeTitleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            chartTitleTextBox.Enabled = changeTitleCheckBox.Checked;
            if (!chartTitleTextBox.Enabled)
            {
                chartTitleTextBox.Text = initialChartTitle;
            }
        }

        public class Parameters
        {
            public string chartName { get; set; }
            public string chartTitle { get; set; }
            public bool changeChartTitle { get; set; }

            private static Parameters DEFAULT;

            public static Parameters getDefault()
            {
                if (DEFAULT == null)
                    DEFAULT = new Parameters();
                return DEFAULT;
            }

            public Parameters()
            {
            }

            public Parameters(string chartName, string chartTitle, bool changeChartTitle)
            {
                this.chartName = chartName;
                this.chartTitle = chartTitle;
                this.changeChartTitle = changeChartTitle;
            }
        }
    }
}
