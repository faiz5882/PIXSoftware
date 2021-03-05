using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PresentationControls;
using System.Xml;


namespace SIMCORE_TOOL.Assistant.SubForms
{
    public partial class Capacity_SubForm : Form
    {

        /*/// <summary>
        /// Class used for demo purposes. This could be anything listed in a CheckBoxComboBox.
        /// </summary>
        public class Status
        {
            public Status(int id, string name) { _Id = id; _Name = name; }

            private int _Id;
            private string _Name;

            public int Id { get { return _Id; } set { _Id = value; } }
            public string Name { get { return _Name; } set { _Name = value; } }

            /// <summary>
            /// Now used to return the Name.
            /// </summary>
            /// <returns></returns>
            public override string ToString() { return Name; }
        }
        /// <summary>
        /// Class used for demo purposes. A list of "Status". 
        /// This represents the custom "IList" datasource of anything listed in a CheckBoxComboBox.
        /// </summary>
        public class StatusList : List<Status>
        {
        }
        private StatusList _StatusList;*/
        private static String sAll = "All";
        private static String sInfinite = "Infinite";
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
                Dictionary<String, List<SubForms.Capacity_SubForm.Node>> lnContent = SubForms.Capacity_SubForm.Node.GetGroups(xnAirport, dtGroupCapacity, sSearchedName);
                if ((lnContent == null) || (lnContent.Count != 1))
                    return null;
                foreach(String sKey in lnContent.Keys)
                {
                    List<SubForms.Capacity_SubForm.Node> lnContent2 = lnContent[sKey];
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

        DataTable dtGroupCapacity_;
        DataTable dtStationsCapacity_;
        // << Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table
        DataTable dtStationsProcessCapacity_;
        // >> Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table


        List<Node> sType;
        List<Node> sTerminal;
        List<Node> sGroup;
        List<Node> sStations;


        private void Initialize(Node slContent, DataTable dtGroupCapacity, DataTable dtStationCapacity, bool bLocked,
            DataTable dtStationProcessCapacity)   // << Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table
        {
            InitializeComponent();
            dtGroupCapacity_ = dtGroupCapacity;
            dtStationsCapacity_ = dtStationCapacity;
            // << Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table
            dtStationsProcessCapacity_ = dtStationProcessCapacity;
            // >> Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table

            /*We configure here some dependances between the differents combobox.*/
            cb_Type.Tag = cb_Terminal;
            cb_Terminal.Tag = cb_Group;
            cb_Group.Tag = cb_Stations;


            setLists(slContent, bLocked);
        }

        // << Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table -> added the stationProcessCapacity parameter
        internal Capacity_SubForm(Node slContent, DataTable dtGroupCapacity, DataTable dtStationCapacity,
            DataTable dtStationProcessCapacity)
        {
            Initialize(slContent, dtGroupCapacity, dtStationCapacity, false, dtStationProcessCapacity);
        }

        internal Capacity_SubForm(Node slContent, DataTable dtGroupCapacity, DataTable dtStationCapacity, bool bLocked,
            DataTable dtStationProcessCapacity)
        {
            Initialize(slContent, dtGroupCapacity, dtStationCapacity, bLocked, dtStationProcessCapacity);
        }
        // >> Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table



        private void UpdateCapacity(List<String> lsSelectedItem, DataTable dtCapacityTable, TextBox tbCapacity)
        {
            if ((lsSelectedItem==null)|| (lsSelectedItem.Count == 0))
                return;
            if ((dtCapacityTable == null) || (dtCapacityTable.Rows.Count == 0))
                return;
            if (tbCapacity == null)
                return;
            foreach (String sObject in lsSelectedItem)
            {
                String sValue = OverallTools.DataFunctions.getValue(dtCapacityTable, sObject, 0, 1);
                if (sValue != null)
                {
                    if (sValue != "-1")
                        tbCapacity.Text = sValue;
                    else
                        tbCapacity.Text = sInfinite;
                    return;
                }
            }
        }
        private Boolean UpdateTable(List<String> lsSelectedItem, DataTable dtCapacityTable, TextBox tbCapacity)
        {
            if ((lsSelectedItem == null) || (lsSelectedItem.Count == 0))
                return false;
            if ((dtCapacityTable == null) || (dtCapacityTable.Rows.Count == 0))
                return false;
            if (tbCapacity == null)
                return false;
            String sValue = tbCapacity.Text;
            int iValue = 0;
            if (sValue == sInfinite)
            {
                iValue = -1;
            }
            else
            {
                if (!Int32.TryParse(sValue, out iValue))
                    return false;
            }
            foreach (String sObject in lsSelectedItem)
            {
                int iRow = OverallTools.DataFunctions.indexLigne(dtCapacityTable, 0, sObject);
                if (iRow == -1)
                    continue;
                dtCapacityTable.Rows[iRow][1] = iValue;
            }
            return true;
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
                if(cbcbComboBox.CheckBoxItems.Count>0)
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
                if(cbcbComboBox.Items.Count == 0)
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
                    /*if ((!lsToPresent.Contains(nValueFils._sKey)) && (bChangeList))
                    {
                        cbcbComboBox.Items.Add(nValueFils._sKey);
                        cbcbComboBox.CheckBoxItems[nValueFils._sKey].Checked = bAllChecked;

                        cbcbComboBox.CheckBoxItems[nValueFils._sKey].CheckedChanged += ehCheckedChanged;
                    }*/
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

        internal void setLists(Node slContent, bool bLocked)
        {
            cb_Type.CheckBoxItems.Clear();
            cb_Stations.CheckBoxItems.Clear();
            cb_Group.CheckBoxItems.Clear();
            cb_Terminal.CheckBoxItems.Clear();
            txt_GroupCapacity.Text = "-1";
            txt_StationCapacity.Text = "-1";
            if (slContent == null)
                return;
            if (slContent._sFils == null)
                return;
            sType = new List<Node>();
            sType.Add(slContent);

            sTerminal = setLists(cb_Type, sType, true, true, true, bLocked, cb_Type_Content_SubForm_CheckedChanged);
            sGroup = setLists(cb_Terminal, sTerminal, true, true, true, bLocked, cb_Terminal_Content_SubForm_CheckedChanged);
            sStations = setLists(cb_Group, sGroup, true, true, true, bLocked, cb_Group_Content_SubForm_CheckedChanged);
            setLists(cb_Stations, sStations, true, true, true, bLocked, cb_Stations_Content_SubForm_CheckedChanged);
            UpdateCapacity(getSelected(cb_Group.CheckBoxItems), dtGroupCapacity_, txt_GroupCapacity);
            UpdateCapacity(getSelected(cb_Stations.CheckBoxItems), dtStationsCapacity_, txt_StationCapacity);
            // << Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table
            if (dtStationsProcessCapacity_ != null)
            {
                UpdateCapacity(getSelected(cb_Stations.CheckBoxItems), dtStationsProcessCapacity_,
                    stationProcessCapaTextBox);
            }
            // >> Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table
        }

        internal Boolean Save()
        {
            Boolean bResult = true;
            bResult = UpdateTable(getSelected(cb_Group.CheckBoxItems), dtGroupCapacity_, txt_GroupCapacity) && bResult;
            bResult = UpdateTable(getSelected(cb_Stations.CheckBoxItems), dtStationsCapacity_, txt_StationCapacity) && bResult;
            // << Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table
            if (dtStationsProcessCapacity_ != null)
            {
                bResult = UpdateTable(getSelected(cb_Stations.CheckBoxItems), dtStationsProcessCapacity_,
                    stationProcessCapaTextBox) && bResult;
            }
            // >> Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table
            return bResult;
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

        internal List<String> Type
        {
            get
            {
                return getSelected(cb_Type.CheckBoxItems);
            }
            set
            {
            }
        }
        internal List<String> Terminal
        {
            get
            {
                return getSelected(cb_Terminal.CheckBoxItems);
            }
            set
            {
            }
        }
        internal List<String> Group
        {
            get
            {
                return getSelected(cb_Group.CheckBoxItems);
            }
            set
            {
            }
        }
        internal List<String> Station
        {
            get
            {
                return getSelected(cb_Stations.CheckBoxItems);
            }
            set
            {
            }
        }
        internal Int32 GroupCapacity
        {
            get
            {
                int iValue;
                if (!Int32.TryParse(txt_GroupCapacity.Text, out iValue))
                    return -1;
                return iValue;
            }
            set
            {
                txt_GroupCapacity.Text = value.ToString();
            }
        }
        internal Int32 StationCapacity
        {
            get
            {
                int iValue;
                if (!Int32.TryParse(txt_StationCapacity.Text, out iValue))
                    return -1;
                return iValue;
            }
            set
            {
                txt_StationCapacity.Text = value.ToString();
            }
        }

        // << Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table
        internal Int32 stationProcessCapacity
        {
            get
            {
                int capacity;
                if (!Int32.TryParse(stationProcessCapaTextBox.Text, out capacity))
                    return -1;
                return capacity;
            }
            set { stationProcessCapaTextBox.Text = value.ToString(); }
        }
        // >> Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table

        private void txt_GroupCapacity_Validating(object sender, CancelEventArgs e)
        {

            if ((sender == null) || (sender.GetType() != typeof(TextBox)))
            {
                e.Cancel = true;
                return;
            }

            int iValue;
            if ((((TextBox)sender).Text != sInfinite) &&
                ((!Int32.TryParse(((TextBox)sender).Text, out iValue)) ||
                (iValue <=0)))
            {
                e.Cancel = true;
                ((TextBox)sender).BackColor = Color.Red;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.White;
            }
        }

        //Booléen permettant d'être sur que la fonction ne sera pas exécutée en boucle.
        bool bLock;
        private void ChangeState(object sender, CheckBoxComboBox cbcbParent)
        {

            /*if (bLock)
                return;*/
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

        private void UpdateComboBox(CheckBoxComboBox cbcbParent, CheckBoxComboBox cbcbFils)
        {

        }
        private void cb_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void cb_Type_Content_SubForm_CheckedChanged(object sender, EventArgs e)
        {
            if (bLock)
                return;
            bLock = true;
            try
            {
                this.SuspendLayout();
                ChangeState(sender, cb_Type);
                sTerminal = setLists(cb_Type, sType, true, false, false, !cb_Type.Enabled, cb_Type_Content_SubForm_CheckedChanged);
                sGroup = setLists(cb_Terminal, sTerminal, true, false, true, !cb_Terminal.Enabled, cb_Terminal_Content_SubForm_CheckedChanged);
                sStations = setLists(cb_Group, sGroup, true, false, true, !cb_Group.Enabled, cb_Group_Content_SubForm_CheckedChanged);
                setLists(cb_Stations, sStations, true, false, true, !cb_Stations.Enabled, cb_Stations_Content_SubForm_CheckedChanged);/**/
            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02003: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
            }
            this.ResumeLayout();
            bLock = false;
        }

        private void cb_Terminal_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.OK;
        }

        void cb_Terminal_Content_SubForm_CheckedChanged(object sender, EventArgs e)
        {
            if (bLock)
                return;
            bLock = true;
            try
            {
                this.SuspendLayout();
                ChangeState(sender, cb_Terminal);
                sGroup = setLists(cb_Terminal, sTerminal, true, false, false, !cb_Terminal.Enabled, cb_Terminal_Content_SubForm_CheckedChanged);
                sStations = setLists(cb_Group, sGroup, true, false, true, !cb_Group.Enabled, cb_Group_Content_SubForm_CheckedChanged);
                setLists(cb_Stations, sStations, true, false, true, !cb_Stations.Enabled, cb_Stations_Content_SubForm_CheckedChanged);
            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02004: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
            }
            this.ResumeLayout();
            bLock = false;
        }

        private void cb_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            // << Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table
            if (dtGroupCapacity_ != null && cb_Group.CheckBoxItems != null && cb_Group.CheckBoxItems.Count > 0)
                UpdateCapacity(getSelected(cb_Group.CheckBoxItems), dtGroupCapacity_, txt_GroupCapacity);
            // >> Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table
        }


        void cb_Group_Content_SubForm_CheckedChanged(object sender, EventArgs e)
        {
            if (bLock)
                return;
            bLock = true;
            try
            {
                this.SuspendLayout();
                ChangeState(sender, cb_Group);
                sStations = setLists(cb_Group, sGroup, true, false, false, !cb_Group.Enabled, cb_Group_Content_SubForm_CheckedChanged);
                setLists(cb_Stations, sStations, true, false, true, !cb_Stations.Enabled, cb_Stations_Content_SubForm_CheckedChanged);
            }
            catch (Exception exc)
            {
                OverallTools.ExternFunctions.PrintLogFile("Except02005: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
            }
            this.ResumeLayout();
            bLock = false;

        }
        private void cb_Stations_SelectedIndexChanged(object sender, EventArgs e)
        {
            // << Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table
            if (dtStationsCapacity_ != null && dtStationsProcessCapacity_ != null && cb_Stations.CheckBoxItems != null 
                && cb_Stations.CheckBoxItems.Count > 0)
            {
                UpdateCapacity(getSelected(cb_Stations.CheckBoxItems), dtStationsCapacity_, txt_StationCapacity);
                UpdateCapacity(getSelected(cb_Stations.CheckBoxItems), dtStationsProcessCapacity_, stationProcessCapaTextBox);                
            }
            // >> Task #8759 Pax2Sim - Editor for Pax Process (Capacities) table
        }

        void cb_Stations_Content_SubForm_CheckedChanged(object sender, EventArgs e)
        {
            if (bLock)
                return;
            bLock = true;
            ChangeState(sender, cb_Stations);
            bLock = false;
        }

        private void txt_GroupCapacity_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
                return;
            TextBox txtTmp = (TextBox)sender;
            if (txtTmp.Text.Length == 0)
                return;
            if (sInfinite.ToLower().StartsWith(txtTmp.Text.ToLower()))
                txtTmp.Text = sInfinite;
            //txt_GroupCapacity_Validating(sender,new );
        }

        private void Capacity_SubForm_Validating(object sender, CancelEventArgs e)
        {
            if ((txt_GroupCapacity.BackColor == Color.Red) || (txt_StationCapacity.BackColor == Color.Red))
            {
                MessageBox.Show("Invalid data for the queues capacity. The data much be bigger than 0 or Infinite", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }
}