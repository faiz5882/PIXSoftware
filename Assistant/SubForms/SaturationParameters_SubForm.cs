using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PresentationControls;

namespace SIMCORE_TOOL.Assistant.SubForms
{
    public partial class SaturationParameters_SubForm : Form
    {
        private static String sAll = "All";

        internal class Node
        {
            internal String _sKey;
            internal List<Node> _sFils;

            internal Node(String sKey, List<Node> sFils)
            {
                _sFils = sFils;
                _sKey = sKey;
            }

            internal static int CompareNode(Node nNode1, Node nNode2)
            {
                return OverallTools.FonctionUtiles.CompareString(nNode1._sKey, nNode2._sKey, true);
            }
            
            internal static Dictionary<String, List<Node>> GetGroups(XmlNode xnAirport, DataTable dtGroupCapacity)
            {
                return GetGroups(xnAirport, dtGroupCapacity, null);
            }

            internal static Node GetGroup(XmlNode xnAirport, DataTable dtGroupCapacity, String sSearchedName)
            {
                Dictionary<String, List<SubForms.SaturationParameters_SubForm.Node>> lnContent 
                    = SubForms.SaturationParameters_SubForm.Node.GetGroups(xnAirport, dtGroupCapacity, sSearchedName);
                if ((lnContent == null) || (lnContent.Count != 1))
                    return null;
                foreach (String sKey in lnContent.Keys)
                {
                    List<SubForms.SaturationParameters_SubForm.Node> lnContent2 = lnContent[sKey];
                    if ((lnContent2 == null) || (lnContent2.Count != 1))
                        return null;
                    return lnContent2[0];
                }
                return null;
            }
            
            internal static Dictionary<String, List<Node>> GetGroups(XmlNode xnAirport, DataTable dtGroupCapacity, String sSearchedName)
            {
                Dictionary<String, List<Node>> lnResult;
                lnResult = new Dictionary<string, List<Node>>();
                foreach (XmlNode enfant in xnAirport.ChildNodes)
                {
                    if (enfant.Attributes["Type"].Value != PAX2SIM.ListeNomObjet[0]) // Terminal
                        continue;
                    String sTerminal = "T" + enfant.Attributes["Index"].Value;
                    foreach (XmlNode enfant2 in enfant.ChildNodes)
                    {
                        if (enfant2.Attributes["Type"].Value != PAX2SIM.ListeNomObjet[1]) // Level
                            continue;
                        String sLevel = "L" + enfant2.Attributes["Index"].Value;
                        foreach (XmlNode enfant3 in enfant2.ChildNodes)
                        {
                            String sType = enfant3.Attributes["Type"].Value;
                            String sGroupName = sTerminal + sLevel + "_" + sType + " " + enfant3.Attributes["Index"].Value;
                            if (OverallTools.DataFunctions.indexLigne(dtGroupCapacity, 0, sGroupName) == -1)
                                continue;
                            if ((sSearchedName != null) && (sSearchedName != sGroupName))
                                continue;
                            if (!lnResult.ContainsKey(sType))
                            {
                                lnResult.Add(sType, new List<Node>());
                            }
                            Node nGroup = new Node(sGroupName, new List<Node>());
                            lnResult[sType].Add(nGroup);
                            foreach (XmlNode enfant4 in enfant3.ChildNodes)
                            {
                                String sTypeStation = enfant4.Attributes["Type"].Value;
                                String sStationName = sTerminal + sLevel + "_" + sTypeStation + " " + enfant4.Attributes["Index"].Value;
                                nGroup._sFils.Add(new Node(sStationName, null));
                            }
                        }
                    }
                }
                return lnResult;
            }
            
            internal static Node GetTree(XmlNode xnAirport, DataTable dtGroupCapacity, String sSearchedGroup)
            {
                Dictionary<String, List<Node>> dsln_Groups = GetGroups(xnAirport, dtGroupCapacity, sSearchedGroup);

                if ((sSearchedGroup != null) && ((dsln_Groups == null) || (dsln_Groups.Count != 1)))
                    return null;
                Node nResult = new Node("Global", new List<Node>());
                foreach (String sKey in dsln_Groups.Keys)
                {
                    Node nType = new Node(sKey, new List<Node>());
                    nResult._sFils.Add(nType);
                    List<Node> ln_Groups = dsln_Groups[sKey];
                    foreach (Node nNode in ln_Groups)
                    {
                        String sTerminal = nNode._sKey.Substring(1, 1);
                        Node nTerminal = null;
                        foreach (Node nTmp in nType._sFils)
                        {
                            if (nTmp._sKey != PAX2SIM.ListeNomObjet[0] + " " + sTerminal)
                                continue;
                            nTerminal = nTmp;
                            break;
                        }
                        if (nTerminal == null)
                        {
                            nTerminal = new Node(PAX2SIM.ListeNomObjet[0] + " " + sTerminal, new List<Node>());
                            nType._sFils.Add(nTerminal);
                        }
                        nTerminal._sFils.Add(nNode);
                    }
                }

                return nResult;
            }
            
            internal static Node GetTree(XmlNode xnAirport, DataTable dtGroupCapacity)
            {
                return GetTree(xnAirport, dtGroupCapacity, null);
            }
        }

        DataTable dtGroupSaturationParams_;

        List<Node> sType;
        List<Node> sTerminal;
        List<Node> sGroup;

        internal SaturationParameters_SubForm(Node slContent, DataTable dtGroupSaturationParams)
        {
            initialize(slContent, dtGroupSaturationParams);
        }

        private void initialize(Node slContent, DataTable dtGroupSaturationParams)
        {
            InitializeComponent();

            dtGroupSaturationParams_ = dtGroupSaturationParams;

            /*We configure here some dependances between the differents comboboxes.*/
            cb_groupType.Tag = cb_terminal;
            cb_terminal.Tag = cb_group;

            setLists(slContent, false);
        }

        private static List<String> getSelected(CheckBoxComboBoxItemList cbcbilList)
        {
            List<String> lSelected = new List<string>();
            foreach (CheckBoxComboBoxItem cbcbi_Tmp in cbcbilList)
            {
                if ((cbcbi_Tmp.Text == sAll) || (cbcbi_Tmp.Text == ""))
                    continue;
                if (cbcbi_Tmp.Checked)
                    lSelected.Add(cbcbi_Tmp.Text);
            }
            return lSelected;
        }

        private void setUpParametersValues(object sender, EventArgs e)
        {
            List<String> selectedGroupsList = getSelected(cb_group.CheckBoxItems);

            if (selectedGroupsList.Count == 1)
            {
                String selectedGroup = selectedGroupsList[0];
                if (selectedGroup != null && dtGroupSaturationParams_ != null)                    
                {
                    Dictionary<String, String> saturationParametersDictionary = getParametersFromTableByGroup(selectedGroup);
                    if (saturationParametersDictionary.Count > 0)
                    {
                        String applyRules = "";
                        String fillingType = "";
                        saturationParametersDictionary.TryGetValue(GlobalNames.sSaturation_ApplyRules, out applyRules);
                        saturationParametersDictionary.TryGetValue(GlobalNames.sSaturation_Filling, out fillingType);

                        bool rules = true;
                        Boolean.TryParse(applyRules, out rules);
                        cb_applySaturationRules.Checked = rules;

                        String openedPerc = "";
                        saturationParametersDictionary.TryGetValue(GlobalNames.sSaturation_Opened, out openedPerc);
                        tb_percentOpened.Text = openedPerc;

                        String accumulation = "";
                        saturationParametersDictionary.TryGetValue(GlobalNames.sSaturation_Accumulation, out accumulation);
                        tb_accumulation.Text = accumulation;

                        String reactionTime = "";
                        saturationParametersDictionary.TryGetValue(GlobalNames.sSaturation_ReactionTime, out reactionTime);
                        tb_reactionTime.Text = reactionTime;

                        if (fillingType.Equals(GlobalNames.FIRST_STATION_FILLING_TYPE))
                        {
                            rb_firstStation.Checked = true;
                            rb_saturateStation.Checked = false;
                            rb_randomStation.Checked = false;
                        }
                        else if (fillingType.Equals(GlobalNames.SATURATE_STATION_FILLING_TYPE))
                        {
                            rb_firstStation.Checked = false;
                            rb_saturateStation.Checked = true;
                            rb_randomStation.Checked = false;
                        }
                        else if (fillingType.Equals(GlobalNames.RANDOM_STATION_FILLING_TYPE))
                        {
                            rb_firstStation.Checked = false;
                            rb_saturateStation.Checked = false;
                            rb_randomStation.Checked = true;
                        }                        
                    }
                }
            }
            else
            {
                setParametersDefaultValues();
            }
        }

        private void setParametersDefaultValues()
        {
            cb_applySaturationRules.Checked = true;

            tb_percentOpened.Text = "0";
            tb_accumulation.Text = "0";
            tb_reactionTime.Text = "0";

            rb_firstStation.Checked = true;
            rb_saturateStation.Checked = false;
            rb_randomStation.Checked = false;
        }

        /// <summary>
        /// The function receives the selected group name as parameter and returns 
        /// the parameters for this group in a dictionary(K: parameterName; V: parameterValue)
        /// </summary>
        /// <param name="selectedGroup"></param>
        /// <returns></returns>
        private Dictionary<String, String> getParametersFromTableByGroup(String selectedGroup)
        {
            Dictionary<String, String> saturationParametersDictionary = new Dictionary<String, String>();
            
            if (dtGroupSaturationParams_ != null)
            {
                int iIndex_Element = dtGroupSaturationParams_.Columns.IndexOf(GlobalNames.sCapaQueue_Element);
                int iIndex_ApplyRules = dtGroupSaturationParams_.Columns.IndexOf(GlobalNames.sSaturation_ApplyRules);
                int iIndex_Filling = dtGroupSaturationParams_.Columns.IndexOf(GlobalNames.sSaturation_Filling);
                int iIndex_Opened = dtGroupSaturationParams_.Columns.IndexOf(GlobalNames.sSaturation_Opened);
                int iIndex_Accumulation = dtGroupSaturationParams_.Columns.IndexOf(GlobalNames.sSaturation_Accumulation);
                int iIndex_ReactionTime = dtGroupSaturationParams_.Columns.IndexOf(GlobalNames.sSaturation_ReactionTime);

                if (iIndex_Element == -1 || iIndex_ApplyRules == -1 || iIndex_Filling == -1 ||
                    iIndex_Opened == -1 || iIndex_Accumulation == -1 || iIndex_ReactionTime == -1)
                {
                    return saturationParametersDictionary;
                }

                foreach (DataRow row in dtGroupSaturationParams_.Rows)
                {
                    String groupFromTable = FonctionsType.getString(row[iIndex_Element]);

                    if (groupFromTable != null && groupFromTable.Equals(selectedGroup))
                    {
                        String applyRules = FonctionsType.getString(row[iIndex_ApplyRules]);
                        saturationParametersDictionary.Add(GlobalNames.sSaturation_ApplyRules, applyRules);

                        String fillingType = FonctionsType.getString(row[iIndex_Filling]);
                        saturationParametersDictionary.Add(GlobalNames.sSaturation_Filling, fillingType);

                        String percentOpened = FonctionsType.getString(row[iIndex_Opened]);
                        saturationParametersDictionary.Add(GlobalNames.sSaturation_Opened, percentOpened);

                        String accumulation = FonctionsType.getString(row[iIndex_Accumulation]);
                        saturationParametersDictionary.Add(GlobalNames.sSaturation_Accumulation, accumulation);

                        String reactionTime = FonctionsType.getString(row[iIndex_ReactionTime]);
                        saturationParametersDictionary.Add(GlobalNames.sSaturation_ReactionTime, reactionTime);                        
                    }
                }
            }
            return saturationParametersDictionary;
        }

        #region Group types, terminal and groups Lists management
        private void setLists(Node slContent, bool bLocked)
        {
            cb_groupType.CheckBoxItems.Clear();
            cb_terminal.CheckBoxItems.Clear();
            cb_group.CheckBoxItems.Clear();

            if (slContent == null)
                return;
            if (slContent._sFils == null)
                return;

            sType = new List<Node>();
            sType.Add(slContent);

            sTerminal = setLists(cb_groupType, sType, true, true, true, bLocked, cb_Type_Content_SubForm_CheckedChanged);
            sGroup = setLists(cb_terminal, sTerminal, true, true, true, bLocked, cb_Terminal_Content_SubForm_CheckedChanged);
            setLists(cb_group, sGroup, true, true, true, bLocked, cb_Group_Content_SubForm_CheckedChanged);
        }
        
        internal static List<Node> setLists(CheckBoxComboBox cbcbComboBox,
                                            List<Node> slContent,
                                            bool bAddAllItem,
                                            bool bInitialize,
                                            bool bChangeList,
                                            bool bLocked,
                                            EventHandler ehCheckedChanged)
        {
            bAddAllItem = !bLocked && bAddAllItem;
            if ((slContent == null) || (slContent.Count == 0))
            {
                if (!bChangeList)
                    return null;
                if (cbcbComboBox.CheckBoxItems.Count > 0)
                    cbcbComboBox.CheckBoxItems[0].Checked = false;
                //cbcbComboBox.CheckBoxItems.Clear();
                while (cbcbComboBox.Items.Count > 1)
                {
                    cbcbComboBox.CheckBoxItems[1].CheckedChanged -= ehCheckedChanged;
                    cbcbComboBox.CheckBoxItems[1].Checked = false;
                    cbcbComboBox.Items.RemoveAt(1);
                }
                if (cbcbComboBox.CheckBoxItems.Count > 0)
                {
                    cbcbComboBox.CheckBoxItems[1].Enabled = true;
                    cbcbComboBox.CheckBoxItems[0].Checked = false;
                    cbcbComboBox.CheckBoxItems[0].Text = "";
                    cbcbComboBox.CheckBoxItems[0].Name = "";
                }
                cbcbComboBox.Text = "";
                return null;
            }
            List<Node> lnResults = new List<Node>();

            bool bAllChecked = bInitialize;
            if (bChangeList && bAddAllItem && !cbcbComboBox.Items.Contains(sAll))
            {
                if (cbcbComboBox.Items.Count == 0)
                    cbcbComboBox.Items.Add(sAll);
                else
                    cbcbComboBox.Items.Insert(1, sAll);
                if (bInitialize)
                    cbcbComboBox.CheckBoxItems[sAll].Checked = true;
            }
            else if (cbcbComboBox.Items.Contains(sAll))
            {
                bAllChecked = bAllChecked || cbcbComboBox.CheckBoxItems[sAll].Checked;
            }

            List<String> lsToPresent = new List<string>();
            int iIgnored = 0;
            foreach (CheckBoxComboBoxItem cbcbiTmp in cbcbComboBox.CheckBoxItems)
            {
                if ((cbcbiTmp.Text == sAll) || (cbcbiTmp.Text == ""))
                {
                    iIgnored++;
                    continue;
                }
                lsToPresent.Add(cbcbiTmp.Text);
            }

            List<String> lsNewList = new List<string>();
            List<Node> lsNewListNode = new List<Node>();
            foreach (Node nValue in slContent)
            {
                if ((nValue == null) || (nValue._sFils == null))
                    continue;
                foreach (Node nValueFils in nValue._sFils)
                {
                    if (nValueFils == null)
                        continue;

                    if (!lsNewList.Contains(nValueFils._sKey))
                        lsNewList.Add(nValueFils._sKey);
                    lsNewListNode.Add(nValueFils);

                    if ((nValueFils._sFils != null) && ((
                        cbcbComboBox.Items.Contains(nValueFils._sKey) && cbcbComboBox.CheckBoxItems[nValueFils._sKey].Checked
                        ) || bAllChecked))
                        lnResults.Add(nValueFils);
                }
            }
            if (bChangeList)
            {
                lsNewListNode.Sort(new Comparison<Node>(Node.CompareNode));
                foreach (String sTmp in lsToPresent)
                {
                    if (lsNewList.Contains(sTmp))
                        continue;
                    cbcbComboBox.CheckBoxItems[sTmp].CheckedChanged -= ehCheckedChanged;
                    cbcbComboBox.CheckBoxItems[sTmp].Checked = false;
                    cbcbComboBox.Items.Remove(sTmp);
                }
                int iInsertPosition = 0;
                for (int i = 0; i < lsNewListNode.Count; i++)
                {
                    if (cbcbComboBox.Items.Contains(lsNewListNode[i]._sKey))
                        continue;
                    cbcbComboBox.Items.Insert(iInsertPosition + iIgnored, lsNewListNode[i]._sKey);
                    cbcbComboBox.CheckBoxItems[lsNewListNode[i]._sKey].Checked = bAllChecked;
                    cbcbComboBox.CheckBoxItems[lsNewListNode[i]._sKey].CheckedChanged += ehCheckedChanged;
                    iInsertPosition++;
                }
                bAllChecked = true;
                foreach (CheckBoxComboBoxItem cbcbiTmp in cbcbComboBox.CheckBoxItems)
                {
                    if ((cbcbiTmp.Text == sAll) || (cbcbiTmp.Text == ""))
                        continue;
                    bAllChecked = bAllChecked && cbcbiTmp.Checked;
                }
                if (bAddAllItem)
                {
                    cbcbComboBox.CheckBoxItems[sAll].Checked = bAllChecked;
                    cbcbComboBox.CheckBoxItems[sAll].CheckedChanged += ehCheckedChanged;
                }

                cbcbComboBox.Enabled = !bLocked;
            }
            if (lnResults.Count == 0)
                return null;
            return lnResults;
        }

        //Used to avoid looping
        bool bLock;
        private void ChangeState(object sender, CheckBoxComboBox cbcbParent)
        {
            if (sender.GetType() != typeof(CheckBoxComboBoxItem))
                return;
            CheckBox cb_Sender = (CheckBox)sender;
            bool bValue = cb_Sender.Checked;
            if (cb_Sender.Text == sAll)
            {
                //On doit cocher ou décocher tous les controle du controle parent.
                foreach (CheckBox cb in cbcbParent.CheckBoxItems)
                {
                    cb.Checked = bValue;
                }
            }
            else
            {
                int iNbTrue = 0;
                CheckBox cb_All = null;
                foreach (CheckBox cb in cbcbParent.CheckBoxItems)
                {
                    if (cb.Text == sAll)
                        cb_All = cb;
                    else if ((cb.Checked) || (cb.Text == ""))
                        iNbTrue++;
                }
                if (cb_All != null)
                    iNbTrue++;
                if ((iNbTrue == cbcbParent.CheckBoxItems.Count) && (cb_All != null))
                {
                    cb_All.Checked = true;
                }
                else if ((iNbTrue != cbcbParent.CheckBoxItems.Count) && (cb_All != null))
                {
                    cb_All.Checked = false;
                }
            }
        }

        void cb_Group_Content_SubForm_CheckedChanged(object sender, EventArgs e)
        {
            if (bLock)
                return;
            bLock = true;
            try
            {
                this.SuspendLayout();
                ChangeState(sender, cb_group);
                setLists(cb_group, sGroup, true, false, false, !cb_group.Enabled, cb_Group_Content_SubForm_CheckedChanged);                
            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02005: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
            }
            this.ResumeLayout();
            bLock = false;
        }

        void cb_Terminal_Content_SubForm_CheckedChanged(object sender, EventArgs e)
        {
            if (bLock)
                return;
            bLock = true;
            try
            {
                this.SuspendLayout();
                ChangeState(sender, cb_terminal);
                sGroup = setLists(cb_terminal, sTerminal, true, false, false, !cb_terminal.Enabled, cb_Terminal_Content_SubForm_CheckedChanged);
                setLists(cb_group, sGroup, true, false, true, !cb_group.Enabled, cb_Group_Content_SubForm_CheckedChanged);
            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02004: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
            }
            this.ResumeLayout();
            bLock = false;
        }
                
        void cb_Type_Content_SubForm_CheckedChanged(object sender, EventArgs e)
        {
            if (bLock)
                return;
            bLock = true;

            try
            {
                this.SuspendLayout();
                ChangeState(sender, cb_groupType);
                sTerminal = setLists(cb_groupType, sType, true, false, false, !cb_groupType.Enabled, cb_Type_Content_SubForm_CheckedChanged);
                sGroup = setLists(cb_terminal, sTerminal, true, false, true, !cb_terminal.Enabled, cb_Terminal_Content_SubForm_CheckedChanged);
                setLists(cb_group, sGroup, true, false, true, !cb_group.Enabled, cb_Group_Content_SubForm_CheckedChanged);
            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02003: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
            }
            this.ResumeLayout();
            bLock = false;
        }

        #endregion

        private void cb_applySaturationRules_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_applySaturationRules.Checked)
            {
                gb_saturationParameters.Enabled = true;
                gb_queueManagement.Enabled = true;
            }
            else
            {
                gb_saturationParameters.Enabled = false;
                gb_queueManagement.Enabled = false;
            }
        }

        internal Boolean save()
        {
            bool isSaveOk = true;
            List<String> selectedGroupsList = getSelected(cb_group.CheckBoxItems);

            if (selectedGroupsList != null)
                isSaveOk = updateSaturationParametersTable(selectedGroupsList);
            else
                isSaveOk = false;

            return isSaveOk;
        }

        private Boolean updateSaturationParametersTable(List<String> selectedGroupsList)
        {
            bool isUpdateOk = true;

            if (dtGroupSaturationParams_ == null)
                isUpdateOk = false;
            else
            {
                int iIndex_Element = dtGroupSaturationParams_.Columns.IndexOf(GlobalNames.sCapaQueue_Element);
                int iIndex_ApplyRules = dtGroupSaturationParams_.Columns.IndexOf(GlobalNames.sSaturation_ApplyRules);
                int iIndex_Filling = dtGroupSaturationParams_.Columns.IndexOf(GlobalNames.sSaturation_Filling);
                int iIndex_Opened = dtGroupSaturationParams_.Columns.IndexOf(GlobalNames.sSaturation_Opened);
                int iIndex_Accumulation = dtGroupSaturationParams_.Columns.IndexOf(GlobalNames.sSaturation_Accumulation);
                int iIndex_ReactionTime = dtGroupSaturationParams_.Columns.IndexOf(GlobalNames.sSaturation_ReactionTime);

                if (iIndex_Element == -1 || iIndex_ApplyRules == -1 || iIndex_Filling == -1 ||
                    iIndex_Opened == -1 || iIndex_Accumulation == -1 || iIndex_ReactionTime == -1)
                {
                    isUpdateOk = false;
                }
                else
                {
                    foreach (String selectedGroup in selectedGroupsList)
                    {
                        int indexSelectedGroupRow = OverallTools.DataFunctions.indexLigne(dtGroupSaturationParams_,
                            iIndex_Element, selectedGroup);

                        if (indexSelectedGroupRow == -1)
                            continue;
                                                
                        Boolean applyRules = cb_applySaturationRules.Checked;                        
                        String accumulation = tb_accumulation.Text;
                        String percentOpened = tb_percentOpened.Text;
                        String reactionTime = tb_reactionTime.Text;
                        String fillingType = "";

                        if (rb_firstStation.Checked)
                            fillingType = GlobalNames.FIRST_STATION_FILLING_TYPE;
                        else if (rb_saturateStation.Checked)
                            fillingType = GlobalNames.SATURATE_STATION_FILLING_TYPE;
                        else if (rb_randomStation.Checked)
                            fillingType = GlobalNames.RANDOM_STATION_FILLING_TYPE;

                        dtGroupSaturationParams_.Rows[indexSelectedGroupRow][iIndex_ApplyRules] = applyRules;                                                
                        dtGroupSaturationParams_.Rows[indexSelectedGroupRow][iIndex_Filling] = fillingType;
                        dtGroupSaturationParams_.Rows[indexSelectedGroupRow][iIndex_Accumulation] = accumulation;
                        dtGroupSaturationParams_.Rows[indexSelectedGroupRow][iIndex_Opened] = percentOpened;
                        dtGroupSaturationParams_.Rows[indexSelectedGroupRow][iIndex_ReactionTime] = reactionTime;
                    }
                }
            }
            return isUpdateOk;
        }

    }
}
