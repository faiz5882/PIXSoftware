using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PresentationControls;


namespace SIMCORE_TOOL.Assistant
{
    public partial class Edit_Assistant : Form
    {
        List<Form> lfContent_;

        private void Initialize(String sTitle,
                              List<Form> lfContent)
        {
            InitializeComponent();
            lfContent_ = lfContent;
            OverallTools.FonctionUtiles.MajBackground(this);
            this.Text = sTitle;

            //panel1.VerticalScroll.Visible = true;
            //panel1.HorizontalScroll.Visible = false;
            Point pLocation = new Point(0, 0);
            Form fLastForm = null;
            foreach (Form fForm in lfContent)
            {
                fForm.TopLevel = false;
                fForm.Parent = panel1;
                fForm.Show();
                fForm.Size = fForm.MinimumSize;
                fForm.Location = new Point(pLocation.X,pLocation.Y);
                if ((panel1.Width - 12) < fForm.Size.Width)
                {
                    this.Width += (fForm.Size.Width - (panel1.Width - 12));
                }
                else
                {
                    fForm.Width = (panel1.Width - 12);
                }
                //fForm.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                fForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right|System.Windows.Forms.AnchorStyles.Left)));
                pLocation.Y += fForm.Height;
                OverallTools.FonctionUtiles.MajBackground(fForm);
                fLastForm = fForm;
            }
            this.MinimumSize = new Size(this.Size.Width, this.MinimumSize.Height);
            if (pLocation.Y == panel1.Size.Height)
            {
            }
            else if (pLocation.Y > panel1.Size.Height)
            {
                int iMissingSize = pLocation.Y - panel1.Size.Height;
                if ((this.Height + iMissingSize) < this.MaximumSize.Height)
                {
                    this.Height += iMissingSize;
                    this.MinimumSize = this.Size;
                }
                else
                {
                    this.Height = this.MaximumSize.Height;
                }
            }
            else
            {
                if (fLastForm != null)
                {
                    int iMissingSize = panel1.Size.Height - (fLastForm.Height + fLastForm.Location.Y);
                    fLastForm.Height = fLastForm.Height + iMissingSize;
                }
            }

        }
        public Edit_Assistant(String sTitle,
                              List<Form> lfContent)
        {
            Initialize(sTitle, lfContent);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            foreach (Form fForm in lfContent_)
            {
                fForm.DialogResult = DialogResult.OK;
            }
        }
    }
}