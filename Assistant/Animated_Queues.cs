using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using SIMCORE_TOOL.Classes;

namespace SIMCORE_TOOL.Assistant
{
    public partial class Animated_Queues : Form
    {
        DataTable dtAnimatedQueues_;
        #region Fonctions pour l'initialisation et la construction de la classe
        private void Initialize(String sTitle, DataTable dtAnimatedQueues, TreeNode tnAirportStructure, ImageList imgList)
        {
            ResourceManager manager = new ResourceManager("SIMCORE_TOOL.Assistant.Animated_Queues",Assembly.GetExecutingAssembly());
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            this.Text = manager.GetString("S_MainTitle") + sTitle;
            dtAnimatedQueues_ = dtAnimatedQueues;
            tv_Reports.ImageList = imgList;
            InitializeTree(tnAirportStructure);
        }
        #region pour Initialiser le formulaire
        private void InitializeTree(TreeNode tnAirportStructure)
        {
            int i;
            for (i = 0; i < tnAirportStructure.Nodes.Count; i++)
            {
                TreeNode tnTemp = tnAirportStructure.Nodes[i];
                if (tnTemp.Tag.GetType() != typeof(SIMCORE_TOOL.Classes.TreeViewTag))
                    continue;
                if (!((SIMCORE_TOOL.Classes.TreeViewTag)tnTemp.Tag).isTerminal)
                    continue;
                TreeNode tnNewNode = OverallTools.TreeViewFunctions.createBranch(tnTemp.Name, tnTemp.Text, tnTemp.Tag, null);
                tv_Reports.Nodes.Add(tnNewNode);
                CopyLevel(tnTemp, tnNewNode);
                tnNewNode.Expand();
            }
        }
        private void CopyLevel(TreeNode tnTerminal, TreeNode tnCurrentTerminal)
        {
            int i;
            int iTerminal;
            if (tnTerminal.Tag.GetType() != typeof(TreeViewTag))
                return;
            if (!((TreeViewTag)tnTerminal.Tag).isTerminal)
                return;
            iTerminal = ((TreeViewTag)tnTerminal.Tag).Index;

            for (i = 0; i < tnTerminal.Nodes.Count; i++)
            {
                TreeNode tnTemp = tnTerminal.Nodes[i];

                if (tnTemp.Tag.GetType() != typeof(TreeViewTag))
                    continue;
                if (!((TreeViewTag)tnTemp.Tag).isLevel)
                    continue;
                TreeNode tnNewNode = OverallTools.TreeViewFunctions.createBranch(tnTemp.Name, tnTemp.Text, tnTemp.Tag, null);
                tnCurrentTerminal.Nodes.Add(tnNewNode);
                CopyGroup(tnTemp, iTerminal, tnNewNode);
                tnNewNode.Expand();
            }
        }
        private void CopyGroup(TreeNode tnLevel, int iTerminal, TreeNode tnCurrentLevel)
        {
            int i;
            if (tnLevel.Tag.GetType() != typeof(TreeViewTag))
                return;
            if (!((TreeViewTag)tnLevel.Tag).isLevel)
                return;
            int iLevel = ((TreeViewTag)tnLevel.Tag).Index;
            for (i = 0; i < tnLevel.Nodes.Count; i++)
            {
                TreeNode tnTemp = tnLevel.Nodes[i];
                if (tnTemp.Tag.GetType() != typeof(TreeViewTag))
                    continue;
                TreeNode tnNewNode = OverallTools.TreeViewFunctions.createBranch(tnTemp.Name, tnTemp.Text, tnTemp.Tag, null);
                int iGroup = ((TreeViewTag)tnTemp.Tag).Index;
                tnCurrentLevel.Nodes.Add(tnNewNode);
                //dtAnimatedQueues
                String sGroup = "T" + iTerminal.ToString() + "L" + iLevel.ToString() + "_" + tnTemp.Name + " " + iGroup.ToString();
                if (OverallTools.DataFunctions.getValue(dtAnimatedQueues_, sGroup, 0, 0) != null)
                    tnNewNode.Checked = true;
            }
        }
        #endregion
        #region pour sauvegarder les valeurs

        private void parcoursTree(TreeNodeCollection tnAirportStructure)
        {
            dtAnimatedQueues_.Rows.Clear();
            int i;
            for (i = 0; i < tnAirportStructure.Count; i++)
            {
                TreeNode tnTemp = (TreeNode)tnAirportStructure[i];
                SaveLevel(tnTemp);
            }
        }
        private void SaveLevel(TreeNode tnTerminal)
        {
            int i;
            int iTerminal;
            if (tnTerminal.Tag.GetType() != typeof(TreeViewTag))
                return;
            if (!((TreeViewTag)tnTerminal.Tag).isTerminal)
                return;
            iTerminal = ((TreeViewTag)tnTerminal.Tag).Index;

            for (i = 0; i < tnTerminal.Nodes.Count; i++)
            {
                TreeNode tnTemp = tnTerminal.Nodes[i];
                SaveGroup(tnTemp, iTerminal);
            }
        }
        private void SaveGroup(TreeNode tnLevel, int iTerminal)
        {
            int i;
            if (tnLevel.Tag.GetType() != typeof(TreeViewTag))
                return;
            if (!((TreeViewTag)tnLevel.Tag).isLevel)
                return;
            int iLevel = ((TreeViewTag)tnLevel.Tag).Index;
            for (i = 0; i < tnLevel.Nodes.Count; i++)
            {
                TreeNode tnTemp = tnLevel.Nodes[i];
                if (tnTemp.Tag.GetType() != typeof(TreeViewTag))
                    continue;
                if (!tnTemp.Checked)
                    continue;
                int iGroup = ((TreeViewTag)tnTemp.Tag).Index;
                //dtAnimatedQueues
                String sGroup = "T" + iTerminal.ToString() + "L" + iLevel.ToString() + "_" + tnTemp.Name + " " + iGroup.ToString();
                Int32 iValue = OverallTools.FonctionUtiles.toGroupRepresentation(sGroup);
                if (iValue == -1)
                    continue;
                dtAnimatedQueues_.Rows.Add(new Object[] { sGroup, iValue });
            }
        }
        #endregion

        #endregion
        public Animated_Queues(String sTitle, DataTable dtAnimatedQueues, TreeNode tnAirportStructure, ImageList imgList)
        {
            Initialize(sTitle, dtAnimatedQueues,tnAirportStructure,imgList);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            parcoursTree(tv_Reports.Nodes);
        }

        private void tv_Reports_AfterCheck(object sender, TreeViewEventArgs e)
        {
            bool value = e.Node.Checked;
            foreach (TreeNode childs in e.Node.Nodes)
            {
                childs.Checked = value;
            }
        }

        private void Animated_Queues_Load(object sender, EventArgs e)
        {

        }
    }
}