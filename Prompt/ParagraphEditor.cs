using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class ParagraphEditor : Form
    {
        private SIM_Htmleditor htmlEditorForm;
        private GestionDonneesHUB2SIM.Paragraph pParag;
        private GestionDonneesHUB2SIM Donnees;

        #region Field accessor
        /// <summary>
        /// Obtient le nom du Paragraph créé ou édité.
        /// </summary>
        public String ParagraphName
        {
            get 
            {
                if (pParag == null)
                    return null;
                else
                    return pParag.Name; 
            }
        }
        #endregion

        /// <summary>
        /// Constructor used to create a new paragraph.
        /// </summary>
        /// <param name="Donnees"></param>
        public ParagraphEditor(GestionDonneesHUB2SIM Donnees)
        {
            this.Donnees = Donnees;
            InitializeComponent();
            InitializeWindow();
            InsertHtmlEditor();
        }

        /// <summary>
        /// Constructor used to edit an existing paragraph.
        /// </summary>
        /// <param name="Donnees"></param>
        /// <param name="sParagraphName">Paragraph name</param>
        public ParagraphEditor(GestionDonneesHUB2SIM Donnees, String sParagraphName)
        {
            this.Donnees = Donnees;
            pParag = Donnees.getParagraph(sParagraphName);
            InitializeComponent();
            InitializeWindow();
            InsertHtmlEditor();
            if (pParag != null) // if the paragraph doesn't exist, editor will be in create mode
            {
                tb_name.Text = pParag.Name;
                tb_name.Enabled = false;
                btn_change.Visible = true;
            }
            else
                MessageBox.Show(
                    "Err0770 : Paragraph doesn't exist !", 
                    "error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Initaialize some parameters.
        /// </summary>
        private void InitializeWindow()
        {
            OverallTools.FonctionUtiles.MajBackground(this);
        }

        /// <summary>
        /// Put a SIM_HtmlEditor in the pHtmlEditor panel.
        /// </summary>
        private void InsertHtmlEditor()
        {
            if (inEditMode()) // edit paragraph mode
            {
                tb_name.Text = pParag.Name;
                htmlEditorForm = new SIM_Htmleditor(pParag.Content, true);
            }
            else // create paragraph mode
                htmlEditorForm = new SIM_Htmleditor(null, true);
            htmlEditorForm.FormBorderStyle = FormBorderStyle.None; // suppress the Window border
            htmlEditorForm.TopLevel = false;
            htmlEditorForm.Parent = pHtmlEditor;
            htmlEditorForm.Location = new Point(0, 0);
            htmlEditorForm.Dock = DockStyle.Fill;
            htmlEditorForm.Show();
        }

        /// <summary>
        /// Show an error message if the paragraph already exist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_name_Leave(object sender, EventArgs e)
        {
            CheckParagExistError (tb_name.Text);
            CheckEmptyNameError();
        }

        /// <summary>
        /// Show an error message if paragraph already exist.
        /// </summary>
        /// <param name="name">Paragraph name</param>
        /// <returns>return true if there is an error</returns>
        private bool CheckParagExistError (String name)
        {
            if (!inEditMode() && Donnees.getParagraph(name) != null)
            {
                MessageBox.Show(
                    "Paragraph already exist. Please change the name.",
                    "Bad name error", 
                    MessageBoxButtons.OK);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check the Name TextBox value
        /// </summary>
        private bool CheckEmptyNameError ()
        {
            if (tb_name.Text == null || tb_name.Text == "")
            {
                MessageBox.Show(
                    "Paragraph name is empty. Please choose a name.",
                    "Bad name error",
                    MessageBoxButtons.OK);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Save the Paragraph or create a new one and exit.
        /// In case the paragraph already exist show an error message.
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CheckEmptyNameError() || CheckParagExistError(tb_name.Text))
                return;
            if (inEditMode()) // edit paragraph mode
            {
                pParag.Content = htmlEditorForm.Note;
                pParag.Title = tb_title.Text;
            }
            else // create a new paragrame mode 
            {
                pParag = new GestionDonneesHUB2SIM.Paragraph(
                    tb_name.Text,
                    tb_title.Text,
                    htmlEditorForm.Note);
                Donnees.setParagraph(pParag.Name, pParag);
            }
            this.Close();
        }

        /// <summary>
        /// Function to know if we edit an existing paragraph or if
        /// we create a new one.
        /// </summary>
        /// <returns>is in edit mode or not</returns>
        private bool inEditMode ()
        {
            return pParag != null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                            "Change the name will result in the creation of a new paragraph, " +
                            "do you want to create a new Paragraph ?",
                            "Change name",
                            MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                btn_change.Visible = false;
                tb_name.Enabled = true;
                pParag = null;
            }
        }
    }
}
