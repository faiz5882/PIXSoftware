using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_doc_versionning : Form
    {
        List<String[]> versionList;
        int[] vl_width;
        List<String[]> approvalList;
        int[] al_width;
        String[] docControlElt;

        public SIM_doc_versionning(List<String[]> versionList, int[] vl_width, List<String[]> approvalList, int[] al_width, String[] docControlElt)
        {
            InitializeComponent();
            this.versionList = versionList;
            this.vl_width = vl_width;
            this.approvalList = approvalList;
            this.al_width = al_width;
            this.docControlElt = docControlElt;
            LoadData();
            OverallTools.FonctionUtiles.MajBackground(this);
        }

        /// <summary>
        /// Fill the DataGridViews and the document control field.
        /// </summary>
        private void LoadData ()
        {
            // Load version table
            DataGridViewRowCollection rows = dgv_versionning.Rows;
            foreach (String[] versionLog in versionList)
                rows.Add(versionLog);
            DataGridViewColumnCollection cc = this.dgv_versionning.Columns;
            for (int i = 0; (i < cc.Count && i < vl_width.Length); i++)
                cc[i].Width = vl_width[i];

            // load control doc table
            rows = dgv_approval.Rows;
            foreach (String[] approval in approvalList)
                rows.Add(approval);
            cc = this.dgv_approval.Columns;
            for (int i = 0; (i < cc.Count && i < al_width.Length); i++)
                cc[i].Width = al_width[i];

            // set approval kind proposal
            DataGridViewComboBoxColumn comboboxColumn = dgv_approval.Columns[0] as DataGridViewComboBoxColumn;
            comboboxColumn.ReadOnly = false;
            comboboxColumn.DataSource = new String[] {"Released by", "Reviewed by", "Prepared by"};

            // load doc control field
            tb_projStep.Text = docControlElt[0];
            tb_partOf.Text = docControlElt[1];
            tb_issuedDept.Text = docControlElt[2];
            tb_intDocRef.Text = docControlElt[3];
            tb_extDocRef.Text = docControlElt[4];
            tb_docStatus.Text = docControlElt[5];
            tb_appendices.Text = docControlElt[6];
        }

        /// <summary>
        /// Fonction qui enregistre les données dans le ReportParameters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveData(object sender, FormClosingEventArgs e)
        {
            // save version table content
            lb_appendices.Focus();
            DataGridViewRowCollection rows = dgv_versionning.Rows;
            if (versionList.Count > 0)
                versionList.RemoveRange(0, versionList.Count);
            for (int i = 0; i < rows.Count - 1; i++)
            {
                String[] tmp = new String[5];
                for (int j = 0; j < 5; j++)
                    if (rows[i].Cells[j].Value == null)
                        tmp[j] = "";
                    else
                        tmp[j] = rows[i].Cells[j].Value.ToString();
                versionList.Add(tmp);
            }

            // save version column width
            DataGridViewColumnCollection cc = this.dgv_versionning.Columns;
            for (int i = 0; (i < cc.Count && i < vl_width.Length); i++)
                vl_width[i] = cc[i].Width;

            // save approval table content
            rows = dgv_approval.Rows;
            if (approvalList.Count > 0)
                approvalList.RemoveRange(0, approvalList.Count);
            for (int i = 0; i < rows.Count - 1; i++)
            {
                String[] tmp = new String[5];
                for (int j = 0; j < 5; j++)
                    if (rows[i].Cells[j].Value == null)
                        tmp[j] = "";
                    else
                        tmp[j] = rows[i].Cells[j].Value.ToString();
                approvalList.Add(tmp);
            }
            // save approval column width
            cc = this.dgv_approval.Columns;
            for (int i = 0; (i < cc.Count && i < al_width.Length); i++)
                al_width[i] = cc[i].Width;

            // save doc control field
            docControlElt[0] = tb_projStep.Text;
            docControlElt[1] = tb_partOf.Text;
            docControlElt[2] = tb_issuedDept.Text;
            docControlElt[3] = tb_intDocRef.Text;
            docControlElt[4] = tb_extDocRef.Text;
            docControlElt[5] = tb_docStatus.Text;
            docControlElt[6] = tb_appendices.Text;
        }
    }
}
