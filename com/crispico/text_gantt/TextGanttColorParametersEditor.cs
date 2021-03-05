using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.com.crispico.text_gantt
{
    public partial class TextGanttColorParametersEditor : Form
    {
        private TextGanttColorParameters parameters;

        private bool byDefaultColor
        {
            get
            {
                return byDefaultColorRadioButton.Checked;
            }
        }

        private bool byAirline
        {
            get
            {
                return byAirlineRadioButton.Checked;
            }
        }

        private bool byFlightCategory
        {
            get
            {
                return byFlightCategoryRadioButton.Checked;
            }
        }

        private bool byGroundHandler
        {
            get
            {
                return byGroundHandlerRadioButton.Checked;
            }
        }

        private bool byOverlap
        {
            get
            {
                return byOverlapRadioButton.Checked;
            }
        }

        public TextGanttColorParameters getParameters()
        {
            return parameters;
        }

        public TextGanttColorParametersEditor(TextGanttColorParameters parameters)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            this.parameters = parameters;

            if (parameters != null)
                updateEditorWithInitialParameters(parameters);
            else
                updateEditorWithInitialParameters(TextGanttColorParameters.getDefaultParameters());
        }

        private void updateEditorWithInitialParameters(TextGanttColorParameters initialParameters)
        {
            this.byDefaultColorRadioButton.Checked = initialParameters.getByDefaultColor();
            this.byAirlineRadioButton.Checked = initialParameters.getByAirline();
            this.byFlightCategoryRadioButton.Checked = initialParameters.getByFlightCategory();
            this.byGroundHandlerRadioButton.Checked = initialParameters.getByGroundHandler();
            this.byOverlapRadioButton.Checked = initialParameters.getByOverlap();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            parameters = new TextGanttColorParameters(byDefaultColor, byAirline, byFlightCategory, byGroundHandler, byOverlap);
        }


    }
}
