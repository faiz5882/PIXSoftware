using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace SIMCORE_TOOL.Prompt.Vinci
{
    class TargetOverview
    {
        internal const string TARGET_OVERVIEW_TABLE_NAME = "Airport All Targets";

        public static DataTable refreshTargetOverviewTable(TreeNode CurrentNode, GestionDonneesHUB2SIM donnees)
        {
            DataTable overviewTable = null;
            TreeNode rootNode = null;
            if (CurrentNode.Text.Equals(GlobalNames.AIRPORT_REPORTS_NODE_NAME))
                rootNode = CurrentNode;
            else
            {
                if (PAX2SIM.nodeBelongsToPAXAnalysis(CurrentNode))
                    rootNode = getAirportReportsNode(CurrentNode);
                else if (PAX2SIM.nodeBelongsToBHSAnalysis(CurrentNode))
                    rootNode = PAX2SIM.getBaggageHandlingSystemNode(CurrentNode);
            }

            if (rootNode != null)
            {
                TreeNode scenarioNode = getScenarioNode(rootNode);
                TreeNode tnTarget = OverallTools.TreeViewFunctions.RechercherNomEnfants(GlobalNames.TARGET_DIRECTORY_NAME, rootNode);
                if (tnTarget != null && scenarioNode != null)
                {
                    overviewTable = donnees.getTable(scenarioNode.Name, TARGET_OVERVIEW_TABLE_NAME);
                    if (overviewTable != null)
                    {
                        overviewTable.Rows.Clear();
                        fillOverviewTableVithTargets(overviewTable, tnTarget, scenarioNode.Name, donnees);
                    }
                    else
                    {
                        overviewTable = createOverviewTableStructure();
                        fillOverviewTableVithTargets(overviewTable, tnTarget, scenarioNode.Name, donnees);
                    }
                }
            }
            return overviewTable;
        }

        private static DataTable createOverviewTableStructure()
        {
            DataTable overviewTable = new DataTable(TARGET_OVERVIEW_TABLE_NAME);
            overviewTable.Columns.Add(GlobalNames.target_scenarioName_columnName, typeof(String));
            overviewTable.Columns.Add(GlobalNames.target_processObserved_columnName, typeof(String));
            overviewTable.Columns.Add(GlobalNames.target_statisticType_columnName, typeof(String));
            overviewTable.Columns.Add(GlobalNames.target_attributeDegree_columnName, typeof(String));
            overviewTable.Columns.Add(GlobalNames.target_statisticAttribute_columnName, typeof(String));
            overviewTable.Columns.Add(GlobalNames.target_comparisonType_columnName, typeof(String));
            overviewTable.Columns.Add(GlobalNames.target_targetValue_columnName, typeof(Double));
            overviewTable.Columns.Add(GlobalNames.target_valueObserved_columnName, typeof(Double));
            overviewTable.Columns.Add(GlobalNames.target_targetAchived_columnName, typeof(String));
            overviewTable.Columns.Add(GlobalNames.target_difference_columnName, typeof(Double));
            overviewTable.Columns.Add(GlobalNames.target_percentSuccess_columnName, typeof(Double));
            return overviewTable;
        }

        private static void fillOverviewTableVithTargets(DataTable overviewTable, TreeNode tnTarget,
            string scenarioName, GestionDonneesHUB2SIM donnees)
        {
            foreach (TreeNode target in tnTarget.Nodes)
            {
                if (target != null && target.Name != TARGET_OVERVIEW_TABLE_NAME)
                {
                    DataTable targetTable = donnees.getTable(scenarioName, target.Name);
                    if (targetTable != null)
                    {
                        foreach (DataRow row in targetTable.Rows)
                        {
                            overviewTable.Rows.Add(row.ItemArray);
                        }
                    }
                }
            }
        }

        private static TreeNode getAirportReportsNode(TreeNode CurrentNode)
        {
            if (CurrentNode == null || CurrentNode.Parent == null)
                return null;

            TreeNode airportReportsNode = null;

            while (!CurrentNode.Parent.Name.Equals(GlobalNames.PAX_CAPACITY_ANALYSIS_NODE_NAME))
            {
                airportReportsNode = CurrentNode.Parent;
                CurrentNode = CurrentNode.Parent;

                if (CurrentNode.Parent == null)
                    return airportReportsNode;
            }
            return airportReportsNode;
        }

        private static TreeNode getScenarioNode(TreeNode CurrentNode)
        {
            if (CurrentNode == null || CurrentNode.Parent == null)
                return null;

            TreeNode scenarioNode = null;

            while (!CurrentNode.Parent.Name.Equals("Analysis"))
            {
                scenarioNode = CurrentNode.Parent;
                CurrentNode = CurrentNode.Parent;

                if (CurrentNode.Parent == null)
                    return scenarioNode;
            }
            return scenarioNode;
        }
    }
}
