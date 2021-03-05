using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_AutomodGraphic : Form
    {
        DataTable dtTable_;
        private void Initialize(DataTable dtTable)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            dtTable_ = dtTable;
            txt_Title.Text = dtTable.TableName;
            foreach(DataColumn dcColumn in dtTable.Columns)
            {
                cb_Columns.Items.Add(dcColumn.ColumnName);
            }
            if(cb_Columns.Items.Count>0)
                cb_Columns.SelectedIndex = 0;
        }
        public SIM_AutomodGraphic(DataTable dtTable)
        {
            Initialize(dtTable);
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            ArrayList alColumnsNames = new ArrayList();
            for (int i = 0; i < cb_Columns.Items.Count; i++)
            {
                if(alColumnsNames.Contains(cb_Columns.Items[i].ToString()))
                {
                    MessageBox.Show("The names for the columns must be unique in the table. \""+cb_Columns.Items[i].ToString()+"\" already appear in the list.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    DialogResult = DialogResult.None;
                    return;
                }
                alColumnsNames.Add(cb_Columns.Items[i].ToString());
            }
            dtTable_.TableName = txt_Title.Text;
            for (int i = 0; i < alColumnsNames.Count; i++)
            {
                dtTable_.Columns[i].ColumnName = alColumnsNames[i].ToString();
            }
        }

        private void cb_Columns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cb_Columns.Text != txt_NewName.Text)
                txt_NewName.Text = cb_Columns.Text;
        }

        private void txt_NewName_TextChanged(object sender, EventArgs e)
        {
            if (txt_NewName.Text.Length > 0)
            {
                cb_Columns.Items[cb_Columns.SelectedIndex] = txt_NewName.Text;
                cb_Columns.Text = txt_NewName.Text;
            }
        }
    }
}