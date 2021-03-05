using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Open_Partial_Project : Form
    {
        private void InitializeForm(TreeNodeCollection Collection, ImageList imgList)
        {
            InitializeComponent();
            tv_Reports.ImageList = imgList;
            OverallTools.FonctionUtiles.MajBackground(this);
            int i;
            for (i = 1; i < Collection.Count; i++)
            {
                TreeNode tnTemp = ((TreeNode)Collection[i]);
                TreeNode tnNewNode = OverallTools.TreeViewFunctions.createBranch(tnTemp.Name, tnTemp.Text, tnTemp.Tag, null);
                tv_Reports.Nodes.Add(tnNewNode);
                OverallTools.TreeViewFunctions.copyChilds(tnNewNode, tnTemp,false,null,true,false);
            }
        }
        public SIM_Open_Partial_Project(TreeNodeCollection Collection, ImageList imgList)
        {
            InitializeForm(Collection, imgList);
        }
        public TreeNodeCollection getTreeView()
        {
            return tv_Reports.Nodes;
        }

        private void tv_Reports_AfterCheck(object sender, TreeViewEventArgs e)
        {
            bool value = e.Node.Checked;
            foreach (TreeNode childs in e.Node.Nodes)
            {
                childs.Checked = value;
            }
        }
    }
}