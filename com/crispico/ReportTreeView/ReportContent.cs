using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.com.crispico.ReportTreeView
{
    public class ReportContent
    {
        public TreeNode reportNode;
        public List<TreeNode> tableNodes = new List<TreeNode>();
        public List<TreeNode> filterNodes = new List<TreeNode>();
        public List<TreeNode> paragraphNodes = new List<TreeNode>();
        public List<TreeNode> chartNodes = new List<TreeNode>();
        public List<TreeNode> globalChartNodes = new List<TreeNode>();
        
        /// <summary>
        /// K = global chart unique name
        /// V = dictionary holding the data about the global chart's tables - scenarios links
        /// </summary>
        internal Dictionary<string, Dictionary<int, SIMCORE_TOOL.PAX2SIM.TableScenarioInfoHolder>> modifiedGlobalCharts
            = new Dictionary<string, Dictionary<int, PAX2SIM.TableScenarioInfoHolder>>();

        public ReportContent(TreeNode pReportNode, List<TreeNode> pTableNodes, List<TreeNode> pFilterNodes,
            List<TreeNode> pParagraphNodes, List<TreeNode> pChartNodes, List<TreeNode> pGlobalChartNodes)
        {
            reportNode = pReportNode;
            tableNodes = pTableNodes;
            filterNodes = pFilterNodes;
            paragraphNodes = pParagraphNodes;
            chartNodes = pChartNodes;
            globalChartNodes = pGlobalChartNodes;
        }

    }
}
