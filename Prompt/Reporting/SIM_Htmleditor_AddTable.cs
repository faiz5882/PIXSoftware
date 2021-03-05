using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Htmleditor_AddTable : Form
    {

        private String TableColor;
        private String TableTitle;
        private Boolean TitleBorder;
        private String TitleBorderColor;
        private int nbColumns;
        private int nbRows;

        public SIM_Htmleditor_AddTable()
        {
            InitializeComponent();
            nbColumns = 1;
            nbRows = 1;
            TableTitle = "";
            TitleBorderColor = "";
            BorderPictureBox.Enabled = false;
            TitleTextBox.Enabled = false;
            BorderCheckBox.Enabled = false;
        }

        internal int NbColumns
        {
            get
            {
                return nbColumns;
            }
            set
            {
                nbColumns = value;
            }
        }
        internal int NbRows
        {
            get
            {
                return nbRows;
            }
            set
            {
                nbRows = value;
            }
        }
        internal String tableColor
        {
            get
            {
                return TableColor;
            }
            set
            {
                TableColor = value;
            }
        }
        internal String tableTitle
        {
            get
            {
                return TableTitle;
            }
            set
            {
                TableTitle = value;
            }
        }
        internal Boolean titleBorder
        {
            get
            {
                return TitleBorder;
            }
            set
            {
                TitleBorder = value;
            }
        }
        internal String titleBorderColor
        {
            get
            {
                return TitleBorderColor;
            }
            set
            {
                TitleBorderColor = value;
            }
        }




        private void OkBtn_Click(object sender, EventArgs e)
        {
            int.TryParse(numericUpDown1.Text.ToString(), out nbColumns);
            int.TryParse(numericUpDown2.Text.ToString(), out nbRows);
            TableTitle = TitleTextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() != DialogResult.Cancel)
            {

                if (colorDialog.Color.IsKnownColor)
                    tableColor = string.Concat("#", (colorDialog.Color.ToArgb() & 0x00FFFFFF).ToString("X6"));
                else
                    TableColor = "#" + colorDialog.Color.Name.Substring(2);
                pictureBox1.BackColor = colorDialog.Color;
            }
        }

        private void TitleCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            TitleTextBox.Enabled = BorderCheckBox.Enabled = TitleCheckbox.Checked;
        }

        private void BorderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TitleBorder = BorderPictureBox.Enabled = BorderCheckBox.Checked;
        }

        private void BorderPictureBox_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColorDialog = new ColorDialog();
            if (BorderColorDialog.ShowDialog() != DialogResult.Cancel)
            {

                if (BorderColorDialog.Color.IsKnownColor)
                    TitleBorderColor = string.Concat("#", (BorderColorDialog.Color.ToArgb() & 0x00FFFFFF).ToString("X6"));
                else
                    TitleBorderColor = "#" + BorderColorDialog.Color.Name.Substring(2);
                BorderPictureBox.BackColor = BorderColorDialog.Color;
            }
        }
    }
}
