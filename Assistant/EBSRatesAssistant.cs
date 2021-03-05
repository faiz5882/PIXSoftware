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
    public partial class EBSRatesAssistant : Form
    {
        internal const string INFINITE = "Infinite";
        private const string DEFAULT_VALUE = INFINITE;
        DataTable ebsRatesTable;
        Label[] terminalNbLabelsList;
        TextBox[] terminalBagRateValueTextBoxesList;

        /// <summary>
        /// Key: T + terminal nb (ex.: T0, T1)
        /// Value: bag per minute rate
        /// </summary>
        Dictionary<String, String> terminalNbEBSRateDictionary = new Dictionary<String, String>();

        const String terminalAbbreviation = "T";
        const int MIN_NB_OF_TERMINALS = 1;
        const int MAX_NB_OF_TERMINALS = 100;
        const int startXFirst = 65;
        const int startXThird = 202;
        const int startY = 10;
        const int IncrementY = 20;

        public EBSRatesAssistant(DataTable _ebsRatesTable)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            ebsRatesTable = _ebsRatesTable;
            terminalNbLabelsList = new Label[0];
            terminalBagRateValueTextBoxesList = new TextBox[0];

            if (ebsRatesTable != null)
            {
                if (ebsRatesTable.TableName.Equals(GlobalNames.userAttributesEBSInputRateTableName))
                {
                    this.Text = "Modify EBS Bags Input Rates";
                }
                else if (ebsRatesTable.TableName.Equals(GlobalNames.userAttributesEBSOutputRateTableName))
                {
                    this.Text = "Modify EBS Bags Output Rates";
                }
                int currentNbOfTerminals = ebsRatesTable.Columns.Count - 1;
                if (currentNbOfTerminals < 1)
                    maxNbOfTerminalsTextBox.Text = MIN_NB_OF_TERMINALS.ToString();
                else
                    maxNbOfTerminalsTextBox.Text = currentNbOfTerminals.ToString();

                getEBSRatesTableData(currentNbOfTerminals);
            }
                        
            modifyContentPanelStructure(maxNbOfTerminalsTextBox, null);
        }

        private void getEBSRatesTableData(int nbOfTerminals)
        {
            if (ebsRatesTable != null)
            {
                if (ebsRatesTable.Rows.Count == 1 && ebsRatesTable.Columns.Count - 1 == nbOfTerminals)
                {
                    DataRow row = ebsRatesTable.Rows[0];
                    for (int i = 0; i <= nbOfTerminals; i++)
                    {
                        String key = terminalAbbreviation + i.ToString();
                        String ebsRate = row[i].ToString();
                        if (ebsRate == "-1")
                            ebsRate = INFINITE;

                        if (!terminalNbEBSRateDictionary.ContainsKey(key))
                            terminalNbEBSRateDictionary.Add(key, ebsRate);
                        else
                            terminalNbEBSRateDictionary[key] = ebsRate;
                    }
                }
            }
        }

        private void modifyContentPanelStructure(object sender, EventArgs e)
        {
            int givenNbOfTerminals = 0;

            if (!validateMaxNbOfTerminalTextBoxInput())
                return;
            if (!Int32.TryParse(maxNbOfTerminalsTextBox.Text, out givenNbOfTerminals))
                return;

            if (givenNbOfTerminals > 100)
            {
                maxNbOfTerminalsTextBox.Text = MAX_NB_OF_TERMINALS.ToString();
                givenNbOfTerminals = MAX_NB_OF_TERMINALS;
            }
            
            addRemoveTerminalRatesObjects(givenNbOfTerminals + 1, sender); //+1 to add terminals(not counting the implicit T0)     
        }

        private bool validateMaxNbOfTerminalTextBoxInput()
        {  
            int maxNb = 0;

            if (maxNbOfTerminalsTextBox == null)
                return false;
            if (maxNbOfTerminalsTextBox.Text == "")
            {
                maxNbOfTerminalsTextBox.Text = MIN_NB_OF_TERMINALS.ToString();
                return false;
            }
            if (!Int32.TryParse(maxNbOfTerminalsTextBox.Text, out maxNb))
            {
                maxNbOfTerminalsTextBox.Text = MIN_NB_OF_TERMINALS.ToString();
                return false;
            }
            if (maxNb <= 0)
            {
                maxNbOfTerminalsTextBox.Text = MIN_NB_OF_TERMINALS.ToString();
                return false;
            }
            return true;
        }

        private void addRemoveTerminalRatesObjects(int maxNbOfTerminals, object sender)
        {
            if (((TextBox)sender).Name == maxNbOfTerminalsTextBox.Name)
            {
                this.SuspendLayout();
                if (maxNbOfTerminals > terminalNbLabelsList.Length)
                {
                    if (terminalNbLabelsList != null && terminalNbLabelsList.Length > 0)
                        contentPanel.ScrollControlIntoView(terminalNbLabelsList[0]);

                    Label[] labelTempList = new Label[maxNbOfTerminals];
                    TextBox[] textBoxTempList = new TextBox[maxNbOfTerminals];

                    for (int i = 0; i < maxNbOfTerminals; i++)
                    {
                        if (i < terminalNbLabelsList.Length)
                        {
                            labelTempList[i] = terminalNbLabelsList[i];
                            textBoxTempList[i] = terminalBagRateValueTextBoxesList[i];
                            
                            textBoxTempList[i].Tag = labelTempList[i].Text;

                            //set the text box value if known
                            if (terminalNbEBSRateDictionary.ContainsKey(textBoxTempList[i].Tag.ToString()))
                            {
                                String ebsRate = "";
                                if (terminalNbEBSRateDictionary.TryGetValue(textBoxTempList[i].Tag.ToString(), out ebsRate))
                                {
                                    if (ebsRate == "-1")
                                        ebsRate = INFINITE;
                                    textBoxTempList[i].Text = ebsRate;
                                }
                            } else
                                terminalNbEBSRateDictionary.Add(textBoxTempList[i].Tag.ToString(), textBoxTempList[i].Text);                            
                        }
                        else
                        {
                            labelTempList[i] = createLabel(i);
                            textBoxTempList[i] = createTextBox(i);

                            textBoxTempList[i].Tag = labelTempList[i].Text;

                            //set the text box value if known
                            if (terminalNbEBSRateDictionary.ContainsKey(textBoxTempList[i].Tag.ToString()))
                            {
                                String ebsRate = "";
                                if (terminalNbEBSRateDictionary.TryGetValue(textBoxTempList[i].Tag.ToString(), out ebsRate))
                                {
                                    if (ebsRate == "-1")
                                        ebsRate = INFINITE;
                                    textBoxTempList[i].Text = ebsRate;
                                }
                            }
                            else
                                terminalNbEBSRateDictionary.Add(textBoxTempList[i].Tag.ToString(), textBoxTempList[i].Text);
                        }
                    }
                    terminalNbLabelsList = labelTempList;
                    terminalBagRateValueTextBoxesList = textBoxTempList;
                }
                else
                {
                    Label[] labelTempList = new Label[maxNbOfTerminals];
                    TextBox[] textBoxTempList = new TextBox[maxNbOfTerminals];
                    for (int i = 0; i < terminalNbLabelsList.Length; i++)
                    {
                        if (i < maxNbOfTerminals)
                        {
                            labelTempList[i] = terminalNbLabelsList[i];
                            textBoxTempList[i] = terminalBagRateValueTextBoxesList[i];

                            textBoxTempList[i].Tag = labelTempList[i].Text;

                            //set the text box value if known
                            if (terminalNbEBSRateDictionary.ContainsKey(textBoxTempList[i].Tag.ToString()))
                            {
                                String ebsRate = "";                                
                                if (terminalNbEBSRateDictionary.TryGetValue(textBoxTempList[i].Tag.ToString(), out ebsRate))
                                {
                                    if (ebsRate == "-1")
                                        ebsRate = INFINITE;
                                    textBoxTempList[i].Text = ebsRate;
                                }
                            }
                            else
                                terminalNbEBSRateDictionary.Add(textBoxTempList[i].Tag.ToString(), textBoxTempList[i].Text);
                        }
                        else
                        {
                            if (terminalNbEBSRateDictionary.ContainsKey(terminalBagRateValueTextBoxesList[i].Tag.ToString()))
                                terminalNbEBSRateDictionary.Remove(terminalBagRateValueTextBoxesList[i].Tag.ToString());

                            terminalNbLabelsList[i].Dispose();
                            terminalBagRateValueTextBoxesList[i].Dispose();
                        }
                    }
                    terminalNbLabelsList = labelTempList;
                    terminalBagRateValueTextBoxesList = textBoxTempList;
                }
                this.ResumeLayout();
            }            
        }

        private Label createLabel(int iIndex)
        {
            int x = startXFirst;
            int y = startY + (iIndex * IncrementY);
            Label result = new Label();
            result.BackColor = Color.Transparent;
            result.ForeColor = Color.Black;
            result.Location = new Point(x, y);
            result.Parent = contentPanel;
            result.Size = new Size(35, 13);
            result.Text = terminalAbbreviation + iIndex.ToString();
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
            result.Parent = contentPanel;
            result.Size = new Size(54, 20);
            result.TabIndex = iIndex + 5;
            result.Text = DEFAULT_VALUE;
            result.Leave += new EventHandler(ebsRateTextBoxLeave);
            result.KeyUp += new KeyEventHandler(ebsRateTextBox_KeyUp);
            return result;
        }

        private void ebsRateTextBoxLeave(object sender, EventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;
            double ebsRate = 0; // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates

            if (currentTextBox != null)
            {
                if (currentTextBox.Text == "")
                {
                    currentTextBox.Text = DEFAULT_VALUE;
                    return;
                }
                if (currentTextBox.Text != INFINITE)
                {
                    if (!double.TryParse(currentTextBox.Text, out ebsRate))
                    {
                        currentTextBox.Text = INFINITE;
                        return;
                    }
                    if (ebsRate <= 0 && ebsRate != -1)
                    {
                        currentTextBox.Text = DEFAULT_VALUE;
                        return;
                    }
                }
                else
                    ebsRate = -1;
                String dictionaryKey = currentTextBox.Tag.ToString();
                if (terminalNbEBSRateDictionary.ContainsKey(dictionaryKey))
                {
                    terminalNbEBSRateDictionary[dictionaryKey] = ebsRate.ToString();
                }
            }
        }

        private void ebsRateTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
                return;
            TextBox txtTmp = (TextBox)sender;
            if (txtTmp.Text.Length == 0)
                return;
            double value = 0;
            if (!double.TryParse(txtTmp.Text, out value))   // >> Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
                txtTmp.Text = INFINITE;            
        }

        private void buttonOKClick(object sender, EventArgs e)
        {
            if (ebsRatesTable != null)
            {
                if (DialogResult == DialogResult.OK)
                {
                    DialogResult drSave = MessageBox
                        .Show("Do you want to save the changes into the EBS Rates table ? ", "Information"
                        , MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (drSave == DialogResult.Cancel)
                    {
                        if (e != null)
                            DialogResult = DialogResult.None;
                    }
                    else if (drSave == DialogResult.No)
                    {
                        int currentNbOfTerminals = ebsRatesTable.Columns.Count;
                        if (currentNbOfTerminals < 1)
                            maxNbOfTerminalsTextBox.Text = MIN_NB_OF_TERMINALS.ToString();
                        else
                            maxNbOfTerminalsTextBox.Text = currentNbOfTerminals.ToString();

                        modifyContentPanelStructure(maxNbOfTerminalsTextBox, null);
                    }
                    else if (drSave == DialogResult.Yes)
                    {
                        saveEBSRatesTableChanges();
                    }
                }
            }
        }

        private void saveEBSRatesTableChanges()
        {
            if (ebsRatesTable != null)
            {
                ebsRatesTable.Rows.Clear();
                ebsRatesTable.Columns.Clear();

                int nbOfTerminals = terminalNbEBSRateDictionary.Count;
                List<String> ebsRatesList = new List<String>();

                for (int i = 0; i < nbOfTerminals; i++)
                {
                    String key = terminalAbbreviation + i.ToString();
                    String ebsRate = "";

                    if (terminalNbEBSRateDictionary.TryGetValue(key, out ebsRate))
                    {
                        DataColumn column = new DataColumn(key);
                        column.DataType = System.Type.GetType("System.String");
                        ebsRatesTable.Columns.Add(column);
                        ebsRatesList.Add(ebsRate);
                    }
                }
                DataRow dr = ebsRatesTable.NewRow();
                for (int i = 0; i < nbOfTerminals; i++)
                {
                    String key = terminalAbbreviation + i.ToString();
                    String ebsRate = "";

                    if (terminalNbEBSRateDictionary.TryGetValue(key, out ebsRate))
                    {
                        if (ebsRate == INFINITE)
                            ebsRate = "-1";
                        dr[key] = ebsRate;
                    }
                }
                ebsRatesTable.Rows.Add(dr);
                ebsRatesTable.AcceptChanges();
            }
        }

    }
}
