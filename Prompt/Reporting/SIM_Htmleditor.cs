using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mshtml;



namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Htmleditor : Form
    {
        private IHTMLDocument2 doc;
        private SIM_Htmleditor_AddTable HEAddTable;
        private String sOriginalNote;

        private void Initialize(String sNote)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            try
            {
                if (sNote != null)
                    Note = sNote;
                else
                    Note = "";
                sOriginalNote = Note;
                StyleCB.SelectedIndex = 1;
                SizeCB.SelectedIndex = 2;
            }
            catch (Exception e)
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02063: (Unable to open the Html Editor) " + this.GetType().ToString() + " throw an exception: " + e.Message);
                this.Close();
                this.Dispose();
            }
        }

        public SIM_Htmleditor(String sNote)
        {
            Initialize(sNote);
        }

        /// <summary>
        /// Constructor used to put the editor in a window.
        /// </summary>
        public SIM_Htmleditor(String sNote, bool isInWindows)
        {
            Initialize(sNote);
            if (isInWindows)
            {
                this.HTMLEditor.Dock = DockStyle.Fill;
                OkBtn.Visible = false;
                CancelBtn.Visible = false;
            }
        }

        public String Note
        {
            get
            {
                return HTMLEditor.DocumentText;
            }
            set
            {
                try
                {
                    //We set a value to the document (It's needed to be sure that the document contains something.)
                    HTMLEditor.DocumentText = "<html><body></body></html>";
                    doc = HTMLEditor.Document.DomDocument as IHTMLDocument2;

                    doc.designMode = "On";
                    if ((value != "") && (value != null))
                    {

                        doc.write(value);
                    }
                }
                catch (Exception e)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Except02064: (Unable to open the Edit the html editor area) " + this.GetType().ToString() + " throw an exception: " + e.Message);
                    this.Close();
                    this.Dispose();
                }
            }
        }

        

        private void CutButton_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("Cut", false, null);
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("Copy", false, null);
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            // on s'assure qu'il n'y ai que du texte brut dans le press-papier
            try
            {
                String CutText = Clipboard.GetText();
                if (CutText != null)
                    Clipboard.SetText(CutText);
            }
            catch (System.Runtime.InteropServices.ExternalException exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02065: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                return;
            }
            HTMLEditor.Document.ExecCommand("Paste", false, null);
        }

        private void imageButton_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("InsertImage", true, null);
        }

        private void LinkButton_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("CreateLink", true, null);
        }

        private void BoldButton_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("Bold", false, null);
        }

        private void ItalicButton_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("Italic", false, null);
        }

        private void UnderlineButton_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("Underline", false, null);
        }

        private void StrikeThroughButton_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("StrikeThrough", false, null);
        }

        private void StyleCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("FontName", false, StyleCB.SelectedItem.ToString());
        }

        private void SizeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("FontSize", false, SizeCB.SelectedItem.ToString());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("SaveAs", true, null);
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IndentsBtn_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("Indent", true, null);
        }

        private void TextColorBtn_Click(object sender, EventArgs e)
        {
            String color = "";
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() != DialogResult.Cancel)
            {
                if (colorDialog.Color.IsKnownColor)
                    color = colorDialog.Color.Name.ToString();
                else
                    color = colorDialog.Color.Name.Substring(2);
            }
            HTMLEditor.Document.ExecCommand("ForeColor", true, color);
        }

        private void JustifyCenterBtn_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("JustifyCenter", true, null);
        }

        private void JustifyLeftBtn_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("JustifyLeft", true, null);
        }

        private void JustifyRightBtn_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("JustifyRight", true, null);
        }

        private void OutdentBtn_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("Outdent", true, null);
        }

        private void BackColorBtn_Click(object sender, EventArgs e)
        {
            String color = "";
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() != DialogResult.Cancel)
            {
                if (colorDialog.Color.IsKnownColor)
                    color = colorDialog.Color.Name.ToString();
                else
                    color = colorDialog.Color.Name.Substring(2);
            }
            HTMLEditor.Document.ExecCommand("BackColor", true, color);
        }

        private void UndoBtn_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("Undo", false, null);
        }

        private void RedoBtn_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("Redo", false, null);
        }

        private void HorizontalRuleBtn_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("InsertHorizontalRule", false, null);
        }

        private void ChipBtn_Click(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("InsertUnorderedList", false, null);
        }

        private void TableBtn_Click(object sender, EventArgs e)
        {
            int nbColumns, nbRows;
            int ColumnWidth;
            HEAddTable = new SIM_Htmleditor_AddTable();
            if (HEAddTable.ShowDialog() == DialogResult.OK)
            {
                nbColumns = HEAddTable.NbColumns;
                nbRows = HEAddTable.NbRows;

                HtmlElement tableRow = null;

                HtmlDocument doc = HTMLEditor.Document;
                HtmlElement tableElem = doc.CreateElement("TABLE");
                ColumnWidth = 60 * nbColumns;
                tableElem.Style = "width:" + ColumnWidth + "px;border-collapse: collapse;border: thin solid " + HEAddTable.tableColor + ";";
                tableElem.SetAttribute("border", "1");
                doc.Body.AppendChild(tableElem);

                if (HEAddTable.tableTitle != "")
                {
                    HtmlElement TableTitle = doc.CreateElement("CAPTION");
                    if (HEAddTable.titleBorder)
                        TableTitle.Style = "border: thin solid " + HEAddTable.titleBorderColor + ";";
                    TableTitle.InnerHtml = HEAddTable.tableTitle;
                    tableElem.AppendChild(TableTitle);
                }

                HtmlElement tableHeader = doc.CreateElement("THEAD");
                tableElem.AppendChild(tableHeader);
                tableRow = doc.CreateElement("TR");
                tableHeader.AppendChild(tableRow);

                // Create table rows.
                HtmlElement tableBody = doc.CreateElement("TBODY");
                tableElem.AppendChild(tableBody);
                for (int i = 0; i < nbRows; i++)
                {
                    tableRow = doc.CreateElement("TR");
                    tableBody.AppendChild(tableRow);
                    for (int j = 0; j < nbColumns; j++)
                    {
                        HtmlElement tableCell = doc.CreateElement("TD");
                        tableCell.Style = "width:10px;border: thin solid " + HEAddTable.tableColor + ";";
                        tableRow.AppendChild(tableCell);
                    }
                }

            }
        }

        /// <summary>
        /// Convert cut elements into brut text before paste them.
        /// Prevent the paste of html code.
        /// </summary>
        private void HTMLEditor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control && e.KeyValue == 86)
            {
                try
                {
                    String CutText = Clipboard.GetText();
                    if (CutText != null)
                        Clipboard.SetText(CutText);
                }
                catch (System.Runtime.InteropServices.ExternalException exc)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Except02066: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                    return;
                }
            }
        }

        private void SIM_Htmleditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Si l'utilisateur a cliqué sur "Ok" on quitte la fenêtre.
            if (this.DialogResult == DialogResult.OK)
                return;

            if (sOriginalNote == Note)
                return;
            // Sinon on demande si il veut sauvegarder
            DialogResult rep = MessageBox.Show("Do you want to save changes ?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (rep == DialogResult.Yes)
            {
                // Savegarde
                this.DialogResult = DialogResult.OK;
            }
            else if (rep == DialogResult.No)
            {
                // Ferme sans sauvegarder
                this.DialogResult = DialogResult.Cancel;
            }
            else // if (rep == DialogResult.Cancel)
            {
                // Annule la fermeture
                this.DialogResult = DialogResult.None;
            }
        }
    }
}