using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.Classes;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Import_Table : Form
    {
        public void Initialize(TreeView tvTree, ImageList imgList)
        {
            InitializeComponent();
            tv_Reports.ImageList = imgList;
            OverallTools.FonctionUtiles.MajBackground(this);
            for(int i=1;i<tvTree.Nodes.Count;i++)
            {

                TreeNode tnTemp = ((TreeNode)tvTree.Nodes[i]);
                TreeNode tnNewNode = OverallTools.TreeViewFunctions.createBranch(tnTemp.Name, tnTemp.Text, tnTemp.Tag, null);
                tv_Reports.Nodes.Add(tnNewNode);
                OverallTools.TreeViewFunctions.copyChilds(tnNewNode, tnTemp, false,null,true,false);
            }
        }
        public SIM_Import_Table(TreeView tvTree, ImageList imgList)
        {
            Initialize(tvTree, imgList);
        }

        private void tv_Reports_AfterSelect(object sender, TreeViewEventArgs e)
        {

            btn_Import.Enabled = (tv_Reports.SelectedNode != null);
            if (btn_Import.Enabled)
            {
                bool Ena = false;
                if (tv_Reports.SelectedNode.Tag != null)
                {
                    if ((((TreeViewTag)tv_Reports.SelectedNode.Tag).isFilterNode) ||
                        (((TreeViewTag)tv_Reports.SelectedNode.Tag).isTableNode))
                    {
                        Ena = true;
                    }
                }
                btn_Import.Enabled = Ena;

            }
        }
        public TreeNode getSelectedNode()
        {
            return tv_Reports.SelectedNode;
        }
    }
}