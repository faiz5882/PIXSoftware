using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.com.crispico.generalUse
{
    public partial class OverwriteFilesDialog : Form
    {
        public bool applyAll
        {
            get
            {
                return applyAllCheckBox.Checked;
            }
        }

        public OverwriteFilesDialog(string fileName)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            messageLabel.Text = "The file \"" + fileName + "\" already exists. Overwrite?";
        }

        public OverwriteFilesDialog(string fileName, Point location)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            messageLabel.Text = "The file \"" + fileName + "\" already exists. Overwrite?";

            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Location = location;
        }
    }
}
