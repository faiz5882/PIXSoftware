using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SIMCORE_TOOL.com.crispico.ReportTreeView
{
    public partial class ReportGroupEditor : Form
    {
        private ContextMenuStrip checkedListBoxMenu;
        private string selectedReportGroupFromMenu;

        private string reportGroupsRootDirectory;

        string reportName;
        List<string> reportGroupsListForGivenReport;
        Dictionary<string, List<string>> availableReportGroups;
        List<ReportParameters> allReportParameters;

        List<string> reportGroupsMarkedForDeletion = new List<string>();

        public ReportGroupEditor(string reportName, List<string> reportGroupsListForGivenReport, Dictionary<string, List<string>> availableReportGroups,
            string reportGroupsRootDirectory, List<ReportParameters> allReportParameters)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            this.Text = reportName;//"Assign the \"" + reportParameters.Name + "\" to report groups.";

            this.reportName = reportName;
            this.reportGroupsListForGivenReport = reportGroupsListForGivenReport;
            this.availableReportGroups = availableReportGroups;
            this.reportGroupsRootDirectory = reportGroupsRootDirectory;
            this.allReportParameters = allReportParameters;

            if (reportGroupsListForGivenReport == null)
            {
                DialogResult = DialogResult.Cancel;
            }
            setContextMenuForReportGroupsCheckedListBox();
            fillReportGroupsCheckedListBox(availableReportGroups, reportGroupsListForGivenReport);
        }

        private void setContextMenuForReportGroupsCheckedListBox()
        {
            var deleteToolStripMenuItem = new ToolStripMenuItem
            { 
                Text = "Delete Report Group"
            };
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;

            checkedListBoxMenu = new ContextMenuStrip();
            checkedListBoxMenu.Items.Add(deleteToolStripMenuItem);
            reportGroupsCheckedListBox.MouseDown += reportGroupsListBox_MouseDown;
        }

        private void reportGroupsListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }
            int index = reportGroupsCheckedListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                reportGroupsCheckedListBox.SelectedIndex = index;
                selectedReportGroupFromMenu = reportGroupsCheckedListBox.Items[index].ToString();
                checkedListBoxMenu.Show(Cursor.Position);
                checkedListBoxMenu.Visible = true;
            }
            else
            {
                checkedListBoxMenu.Visible = false;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!availableReportGroups.ContainsKey(selectedReportGroupFromMenu))
            {
                return;
            }
            List<string> reports = availableReportGroups[selectedReportGroupFromMenu];
            DialogResult deleteDialogResult = DialogResult.Yes;
            if (reports.Count > 0)
            {
                string reportsWarning = "Are you sure you want to delete the report group \"" + selectedReportGroupFromMenu + "\"?"
                    + Environment.NewLine + "It contains the following generated reports:";
                foreach (string report in reports)
                {
                    reportsWarning += Environment.NewLine + "   " + report;
                }
               deleteDialogResult = MessageBox.Show(reportsWarning, "Delete Report Group", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
            if (deleteDialogResult == DialogResult.Yes
                && !reportGroupsMarkedForDeletion.Contains(selectedReportGroupFromMenu))
            {
                reportGroupsMarkedForDeletion.Add(selectedReportGroupFromMenu);
                fillReportGroupsCheckedListBox(availableReportGroups, reportGroupsListForGivenReport);
            }
        }

        private void fillReportGroupsCheckedListBox(Dictionary<string, List<string>> availableReportGroups, List<string> reportGroupsListForGivenReport)
        {
            reportGroupsCheckedListBox.Items.Clear();
            foreach (string availableReportGroup in availableReportGroups.Keys)
            {
                if (!reportGroupsMarkedForDeletion.Contains(availableReportGroup))
                {
                    reportGroupsCheckedListBox.Items.Add(availableReportGroup, reportGroupsListForGivenReport.Contains(availableReportGroup));
                }
            }
            foreach (string currentReportGroup in reportGroupsListForGivenReport)
            {
                if (!availableReportGroups.Keys.Contains(currentReportGroup)
                    && !reportGroupsMarkedForDeletion.Contains(currentReportGroup))
                {
                    reportGroupsCheckedListBox.Items.Add(currentReportGroup, true);
                }
            }
        }

        private void newGroupTextBox_Leave(object sender, EventArgs e)
        {
            string allInvalidCharsForDirName = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string text = newGroupTextBox.Text;
            List<string> invalidCharsFound = new List<string>();
            foreach (char invalidChar in allInvalidCharsForDirName)
            {
                if (text.Contains(invalidChar))
                {
                    text = text.Replace(invalidChar.ToString(), "");
                    invalidCharsFound.Add(invalidChar.ToString());
                }
            }
            newGroupTextBox.Text = text;
            if (invalidCharsFound.Count > 0)
            {
                string charsFound = "";
                foreach (string str in invalidCharsFound)
                {
                    charsFound += Environment.NewLine + " " + str;
                }
                MessageBox.Show("The group name should contain only characters allowed in a directory name or in a file name." +
                    Environment.NewLine + "The following illegal characters have been removed:" + charsFound, "Illegal characters");
            }
        }

        private void addNewGroupButton_Click(object sender, EventArgs e)
        {
            bool groupExists = false;
            for (int i = 0; i < reportGroupsCheckedListBox.Items.Count; i++)
            {
                string groupFromCheckList = reportGroupsCheckedListBox.Items[i].ToString();
                if (groupFromCheckList == newGroupTextBox.Text)
                {
                    groupExists = true;
                }
            }
            if (!groupExists)
            {
                string reportGroupPath = "";
                try
                {
                    reportGroupPath = reportGroupsRootDirectory + "\\" + newGroupTextBox.Text;
                    if (!Directory.Exists(reportGroupPath))
                    {
                        Directory.CreateDirectory(reportGroupPath);
                    }
                }
                catch (IOException ex)
                {
                    string message = "Failed to create the directory \"" + reportGroupPath + "\".";
                    MessageBox.Show(message);
                    OverallTools.ExternFunctions.PrintLogFile(message + Environment.NewLine + ex.Message);
                    newGroupTextBox.Focus();
                    return;
                }
                if (!availableReportGroups.ContainsKey(newGroupTextBox.Text))
                {
                    availableReportGroups.Add(newGroupTextBox.Text, new List<string> { reportName });
                }
                else
                {
                    availableReportGroups[newGroupTextBox.Text].Add(reportName);
                }

                reportGroupsCheckedListBox.Items.Add(newGroupTextBox.Text, true);
                newGroupTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("The group already exists. Please choose a new name.", "Duplicate group name.");
                newGroupTextBox.Focus();
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            resetReportGroupListForCurrentReport();
            if (reportGroupsMarkedForDeletion.Count > 0)
            {
                deleteReportGroupsMarkedForDeletion();
            }
        }

        private void resetReportGroupListForCurrentReport()
        {
            List<string> initialReportGroups = new List<string>();
            initialReportGroups.AddRange(reportGroupsListForGivenReport.ToArray());

            reportGroupsListForGivenReport.Clear();
            for (int i = 0; i < reportGroupsCheckedListBox.Items.Count; i++)
            {
                CheckState checkedState = reportGroupsCheckedListBox.GetItemCheckState(i);
                if (checkedState == CheckState.Checked)
                {
                    reportGroupsListForGivenReport.Add(reportGroupsCheckedListBox.Items[i].ToString());
                }
            }

            foreach (string initialRepGr in initialReportGroups)
            {
                if (!reportGroupsListForGivenReport.Contains(initialRepGr))
                {
                    try
                    {
                        string geenratedReportPath = reportGroupsRootDirectory + "\\" + initialRepGr + "\\" + reportName;
                        if (Directory.Exists(geenratedReportPath))
                        {
                            Directory.Delete(geenratedReportPath, true);
                        }
                    }
                    catch (IOException ex)
                    {
                        OverallTools.ExternFunctions.PrintLogFile("Error while deleting the report \"" + reportName + "\" from a report group." + ex.Message);
                    }
                }
            }
        }

        private void deleteReportGroupsMarkedForDeletion()
        {
            if (!Directory.Exists(reportGroupsRootDirectory))
            {
                return;
            }            
            /*string warning = "Are you sure you want to delete the following report groups?";
            foreach (string reportGroup in reportGroupsMarkedForDeletion)
            {
                warning += Environment.NewLine + "   " + reportGroup;
            }
            if (MessageBox.Show(warning, "Delete Report Groups", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }*/
            try
            {
                foreach (string reportGroup in reportGroupsMarkedForDeletion)
                {
                    string reportGroupPath = reportGroupsRootDirectory + "\\" + reportGroup;
                    if (Directory.Exists(reportGroupPath))
                    {   
                        var dir = new DirectoryInfo(reportGroupPath);
                        dir.Delete(true);
                    }
                    foreach (ReportParameters reportParam in allReportParameters)
                    {
                        if (reportParam.reportGroupsList.Contains(reportGroup))
                        {
                            reportParam.reportGroupsList.Remove(reportGroup);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                OverallTools.ExternFunctions.PrintLogFile("Error while deleting a report group." + ex.Message);
            }
        }

        private void reportGroupsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index >= reportGroupsCheckedListBox.Items.Count || reportGroupsCheckedListBox.Items[e.Index] == null)
            {
                return;
            }
            string currentReportGroupName = reportGroupsCheckedListBox.Items[e.Index].ToString();
            if (e.NewValue == CheckState.Checked)
            {
                if (availableReportGroups.ContainsKey(currentReportGroupName)
                    && !availableReportGroups[currentReportGroupName].Contains(reportName))
                {
                    availableReportGroups[currentReportGroupName].Add(reportName);
                }
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                if (availableReportGroups.ContainsKey(currentReportGroupName)
                    && availableReportGroups[currentReportGroupName].Contains(reportName))
                {
                    availableReportGroups[currentReportGroupName].Remove(reportName);
                }
            }
        }

    }
}
