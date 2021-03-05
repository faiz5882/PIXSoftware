using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SIMCORE_TOOL.Assistant
{

    public partial class Queue_Assistant : Form
    {
        private DataTable QueueTable;
        private bool bValueHadChange;

        private void Initialize(DataTable QueueTable_)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            QueueTable = QueueTable_.Clone();
            foreach (DataRow ligne in QueueTable_.Rows)
            {
                QueueTable.Rows.Add(new Object[] { ligne[0], ligne[1] });
            }
            bValueHadChange = false;

            txt_DeskCapacity.BackColor = Color.White;

            foreach (String Name in PAX2SIM.ListeNomObjet)
            {
                if ((Name != "Shuttle") && (Name != "Arrival Gate") && (Name != "Baggage Claim")
                    && (Name != PAX2SIM.sLevelName) && (Name != PAX2SIM.sTerminalName))
                {
                    cb_Group.Items.Add(Name);
                }
            }

        }
        public Queue_Assistant(DataTable QueueTable_)
        {
            Initialize(QueueTable_);
        }

        private void cb_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bValueHadChange)
            {
                if (MessageBox.Show("Would you like to save the change for this desk ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    btn_Submit_Click(null, null);
                }
                bValueHadChange = false;
            }
            int iIndexLigne =OverallTools.DataFunctions.indexLigne(QueueTable,0,cb_Group.Text);
            cb_Desks.Items.Clear();
            ArrayList alQueues = new ArrayList();
            foreach (DataRow ligne in QueueTable.Rows)
            {
                if (((String)ligne.ItemArray[0]).Contains(cb_Group.Text))
                {
                    alQueues.Add(ligne[0].ToString());
                    //cb_Desks.Items.Add(ligne[0]);
                }
            }
            alQueues.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
            cb_Desks.Items.AddRange(alQueues.ToArray());
            chk_ApplyToAll_CheckedChanged(null, null);
        }


        private void chk_ApplyToAll_CheckedChanged(object sender, EventArgs e)
        {
            bValueHadChange = true;
            btn_Submit.Enabled = true;
            lbl_Desk.Enabled = !chk_ApplyToAll.Checked;
            cb_Desks.Enabled = !chk_ApplyToAll.Checked;
            if (cb_Desks.Items.Count == 0)
            {
                txt_DeskCapacity.Enabled = false;
                lbl_DeskCapacity.Enabled = false;
                return;
            }
            else
            {
                txt_DeskCapacity.Enabled = true;
                lbl_DeskCapacity.Enabled = true;
            }
            if (cb_Desks.SelectedIndex == -1)
                cb_Desks.SelectedIndex = 0;
            if (!cb_Desks.Enabled)
            {
                cb_Desks_SelectedIndexChanged(null, null);
                int iValue = toStorageValue(txt_DeskCapacity.Text);
                foreach (String Name in cb_Desks.Items)
                {
                    if (DeskValue(Name) != iValue)
                    {
                        txt_DeskCapacity.Text = "";
                        bValueHadChange = false;
                        break;
                    }
                }
            }
            else
            {
                cb_Desks_SelectedIndexChanged(null, null);
            }
        }

        private void cb_Desks_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iDeskValue = DeskValue(cb_Desks.Text);
            if (iDeskValue == -2)
            {
                txt_DeskCapacity.Text = "";
            }
            else if (iDeskValue == -1)
            {
                txt_DeskCapacity.Text = "Infinite";
            }
            else
            {
                txt_DeskCapacity.Text = iDeskValue.ToString();
            }
            bValueHadChange = false;
        }
        private int DeskValue(String NomDesk)
        {
            if ((NomDesk == null) || (NomDesk == ""))
                return -2;
            int iIndexLigne = OverallTools.DataFunctions.indexLigne(QueueTable, 0, cb_Desks.Text);
            if (iIndexLigne == -1)
            {
                return -2;
            }
            if (QueueTable.Rows[iIndexLigne][1].ToString() != "")
            {
                return (Int32)QueueTable.Rows[iIndexLigne][1];
            }
            else
            {
                return 0;
            }
        }
        private int toStorageValue(String Value)
        {
            if (Value == "Infinite")
                return -1;
            Int32 iValue;
            if (Int32.TryParse(Value, out iValue))
                return iValue;
            return 0;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            btn_Submit.Enabled = false;
            bValueHadChange = false;

            Int32 storageValue = toStorageValue(txt_DeskCapacity.Text);
            int iIndexLigne;
            if (chk_ApplyToAll.Checked)
            {
                foreach (String Names in cb_Desks.Items)
                {
                    iIndexLigne = OverallTools.DataFunctions.indexLigne(QueueTable, 0, Names);
                    if (iIndexLigne != -1)
                    {
                        QueueTable.Rows[iIndexLigne][1] = storageValue;
                    }
                }
            }
            else
            {
                iIndexLigne = OverallTools.DataFunctions.indexLigne(QueueTable, 0, cb_Desks.Text);
                if (iIndexLigne != -1)
                {
                    QueueTable.Rows[iIndexLigne][1] = storageValue;
                }
            }
        }
        public DataTable getQueueTable()
        {
            return QueueTable;
        }

        private void txt_DeskCapacity_TextChanged(object sender, EventArgs e)
        {
            bValueHadChange = true;
            btn_Submit.Enabled = true;
            Int32 iNewValue;
            if (!Int32.TryParse(txt_DeskCapacity.Text, out iNewValue))
            {
                if (txt_DeskCapacity.Text.Length == 0)
                    return;
                if ((txt_DeskCapacity.Text[0] == 'i') || (txt_DeskCapacity.Text[0] == 'I') || (txt_DeskCapacity.Text[0] == '-'))
                {
                    txt_DeskCapacity.Text = "Infinite";
                }
                else
                {
                    txt_DeskCapacity.Text = "";
                }
            }
        }
    }
}