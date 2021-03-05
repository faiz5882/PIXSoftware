using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;
using SIMCORE_TOOL.Classes;


namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Add_Edit_Elem_Airport : Form
    {
        // Node contenant le parent du nouvel élément.
        TreeNode leparent;
        private String[] Groups;
        private String[] Desks;


        //Structure d'un nouvel objet. elle contient toutes les données sur le nouvel élément
        // qui doit être insérer dans l'arbre.
        public struct NewObjet
        {
            public String NewObjetType;
            public int VI_From;
            public int VI_To;
            public string VS_GroupeDesc;
            public string VS_ItemDec;
            public TreeNode PointedNode;
        };

        //Booleen indiquant si la fenêtre a été ouverte en mode édition ou non.
        bool b_ModeEdition;

        #region Fonction d'initialisation de la classe.
        private void Initialize()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            TXT_From.BackColor = Color.White;
            TXT_ItemDesc.BackColor = Color.White;
            TXT_To.BackColor = Color.White;
        }
        #endregion

        #region Constructeurs de la classe.

        /// <summary>
        /// Constructeur pour les cas où l'on édite un noeud déjà existant.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="bEdition"></param>
        public SIM_Add_Edit_Elem_Airport(TreeNode CurrentNode, bool bEdition)
        {
            Initialize();
            if ((CurrentNode == null) || (CurrentNode.Tag.GetType() != typeof(TreeViewTag)))
                return;
            b_ModeEdition = bEdition;
            leparent = CurrentNode;
            TreeViewTag ObjectAirport = (TreeViewTag)leparent.Tag;

            #region if (b_ModeEdition)
            if (b_ModeEdition)
            {
                this.Text = "Edit an element";
                EnabledDisabledControls(false, false);
                CB_ObjectType.DropDownStyle = ComboBoxStyle.DropDown;
                CB_ObjectType.Text = ObjectAirport.AirportObjectType;
                if (ObjectAirport.isGroup)
                {
                    CB_GroupDesc.Text = ObjectAirport.Name;
                    L_A2.Enabled = true;
                    CB_GroupDesc.Enabled = true;
                    CB_GroupDesc.Focus();
                }
                else
                {
                    L_A3.Enabled = true;
                    TXT_ItemDesc.Text = ObjectAirport.Name;
                    TXT_ItemDesc.Enabled = true;
                    TXT_ItemDesc.Focus();
                }
                return;
            }
            #endregion
            #region } else {
            #endregion

            #region (!b_ModeEdition)
            if (ObjectAirport.isGroup)
            {
                //On remonte d'un index car on était dans un groupe.
                leparent = leparent.Parent;
            }
            String[] Objets = getAllowedObjects(leparent, false, 0);
            if (Objets == null)
                return;
            Groups = getGroups(Objets);
            Desks = getNoGroups(Objets);

            foreach (String Value in Objets)
            {
                String sTmp = Value;
                if (Value.EndsWith(" Group"))
                    continue;
                //                    sTmp = Value.Remove(Value.LastIndexOf(" Group"));
                if (!CB_ObjectType.Items.Contains(sTmp))
                    CB_ObjectType.Items.Add(sTmp);
            }
            TXT_From.TextChanged -= TXT_From_TextChanged;
            if (leparent != CurrentNode)
            {
                String sNomTmp = ObjectAirport.AirportObjectType;
                if (sNomTmp.EndsWith(" Group"))
                    sNomTmp = sNomTmp.Remove(sNomTmp.LastIndexOf(" Group"));
                //if (CB_ObjectType.Items.Contains(sNomTmp))
                CB_ObjectType.SelectedIndex = CB_ObjectType.Items.IndexOf(sNomTmp);
            }
            if (CB_ObjectType.SelectedIndex == -1)
            {
                CB_ObjectType.SelectedIndex = 0;
            }
            MAJListeGroupeDescription(CB_ObjectType.Text, leparent);
            TXT_From.TextChanged += TXT_From_TextChanged;
            if (leparent != CurrentNode)
            {
                CB_GroupDesc.SelectedIndex = CB_GroupDesc.Items.IndexOf(ObjectAirport.Name);
            }
            #endregion

        }

        public static string[] getGroups(string[] Objets)
        {
            ArrayList List = new ArrayList();
            foreach (String obj in Objets)
            {
                if (obj.EndsWith(" Group"))
                    List.Add(obj);
            }
            if (List.Count == 0)
                return null;
            string[] sResult = new string[List.Count];
            for (int i = 0; i < sResult.Length; i++)
            {
                sResult[i] = (String)List[i];
            }
            return sResult;
        }

        public static string[] getNoGroups(string[] Objets)
        {
            ArrayList List = new ArrayList();
            foreach (String obj in Objets)
            {
                if (!obj.EndsWith(" Group"))
                    List.Add(obj);
            }
            if (List.Count == 0)
                return null;
            string[] sResult = new string[List.Count];
            for (int i = 0; i < sResult.Length; i++)
            {
                sResult[i] = (String)List[i];
            }
            return sResult;
        }
        #endregion

        private void MAJListeGroupeDescription(string NewObjetType, TreeNode PointedNode)
        {
            int i;
            String sNomGroupe = null;
            CB_GroupDesc.Items.Clear();
            CB_GroupDesc.ResetText();
            if (!OverallTools.FonctionUtiles.estPresentDansListe(NewObjetType, PAX2SIM.ListeNomsObjetsGroup))
            {
                CB_GroupDesc.Text = "";
                return;
            }
            for (i = 0; i < PAX2SIM.ListeNomsObjets.Length; i++)
            {
                if (PAX2SIM.ListeNomsObjets[i][1][0] == NewObjetType)
                {
                    sNomGroupe = PAX2SIM.ListeNomsObjets[i][0][0];
                    break;
                }
            }
            if (i == PAX2SIM.ListeNomsObjets.Length)
                return;

            if (PointedNode == null)
                return;
            String[] groupes = OverallTools.TreeViewFunctions.getNomGroupes(PointedNode, sNomGroupe);
            if (groupes.Length != 0)
            {
                for (i = 0; i < groupes.Length; i++)
                {
                    CB_GroupDesc.Items.Add(groupes[i]);
                }
            }
            int iIndexGroupe = CB_GroupDesc.Items.IndexOf(sNomGroupe);
            bool ancienneValeur = b_ModeEdition;
            b_ModeEdition = true;
            if (Groups != null)
            {
                for (i = 0; i < Groups.Length; i++)
                {
                    if (Groups[i] == sNomGroupe)
                        break;
                }
            }
            if ((Groups != null) && (i == Groups.Length))
            {
                //On ne peut pas ajouter de nouveaux groupes.
                CB_GroupDesc.DropDownStyle = ComboBoxStyle.DropDownList;
                if (iIndexGroupe == -1)
                    iIndexGroupe = 0;
            }
            else
            {
                //On peut créer des groupes personnalisés.
                CB_GroupDesc.DropDownStyle = ComboBoxStyle.DropDown;
                if ((iIndexGroupe == -1))
                    CB_GroupDesc.Text = sNomGroupe;
            }
            b_ModeEdition = ancienneValeur;
            if (iIndexGroupe != -1)
                CB_GroupDesc.SelectedIndex = iIndexGroupe;
            else
            {
                CB_GroupDesc.Text = "";
                CB_GroupDesc.Text = sNomGroupe;
            }
        }

        #region Fonction pou activer et désactiver les controles du formulaire.
        private void EnabledDisabledControls(bool bValue, bool bTxtBox)
        {
            L_A0.Enabled = bValue;
            L_A2.Enabled = bValue;
            L_A3.Enabled = bValue;
            L_A4.Enabled = bValue;
            L_A5.Enabled = bValue;
            TXT_To.Enabled = bValue;
            TXT_From.Enabled = bValue;
            CHK_Multiple.Enabled = bValue;

            CB_GroupDesc.Enabled = bTxtBox;
            CB_ObjectType.Enabled = bTxtBox;
            TXT_ItemDesc.Enabled = bTxtBox;
        }
        #endregion

        #region Fonction statique qu'il faudra déplacer.
        public static String[] getAllowedObjects(TreeNode selectedNode, Boolean bLevel, int iIndexLevel)
        {
            if (selectedNode == null)
                return null;
            if (selectedNode.Tag.GetType() != typeof(TreeViewTag))
                return null;
            TreeViewTag CurrentTag = (TreeViewTag)selectedNode.Tag;
            if (CurrentTag.AirportObjectType == PAX2SIM.sLevelName)
            {
                /*The level had been removed, we have to check in the terminal the element which can be added.
                        - If in that level there is */
                return getAllowedObjects(selectedNode.Parent, true, CurrentTag.Index);
            }
            String sCurrentNom = CurrentTag.AirportObjectType;
            if (bLevel)
                sCurrentNom = PAX2SIM.sLevelName;

            //On détermine l'index de l'objet sélectionné dans la table d'arborescence
            //afin de savoir quels sont les objets qui sont ajoutable.
            int iIndex = -1;
            for (int i = 0; i < PAX2SIM.ListeNomsObjets.Length; i++)
            {
                if (PAX2SIM.ListeNomsObjets[i][0][0] == sCurrentNom)
                {
                    iIndex = i;
                    break;
                }
            }
            if (iIndex == -1)
                return null;

            if (PAX2SIM.ListeNomsObjets[iIndex][1].Length == 0)
                return null;
            if (PAX2SIM.ListeNomsObjets[iIndex][1].Length == 1)
            {
            }
            ArrayList list = new ArrayList();
            ArrayList Group = new ArrayList();
            Hashtable htPresent = null;
            if (!bLevel)
            {
                htPresent = LookForPresent(selectedNode, 0);
            }
            else
            {
                Hashtable htTmp;
                htPresent = new Hashtable();
                for (int i = 0; i < selectedNode.Nodes.Count; i++)
                {
                    TreeViewTag tvtTag = (TreeViewTag)selectedNode.Nodes[i].Tag;
                    htTmp = LookForPresent(selectedNode.Nodes[i], tvtTag.Index);
                    foreach (String sKey in htTmp.Keys)
                    {
                        if (!htPresent.Contains(sKey))
                        {
                            htPresent.Add(sKey, htTmp[sKey]);
                        }
                        else
                        {
                            ((GroupInformation)htPresent[sKey]).addGroupInformation((GroupInformation)htTmp[sKey]);
                        }
                    }
                }
            }
            if (htPresent == null)
                return null;
            foreach (String sNom in PAX2SIM.ListeNomsObjets[iIndex][1])
            {
                if (htPresent.Contains(sNom))
                {
                    if (!GestionDonneesHUB2SIM.htSizeAutomodModel.ContainsKey(sNom))
                    {
                        if (OverallTools.FonctionUtiles.estPresentDansListe(sNom, PAX2SIM.ListeNomsObjetWithoutIndex))
                            continue;
                        /*Il n'y a pas de limitation du nombre d'objets de ce type.*/
                        list.Add(sNom);
                        if (sNom.LastIndexOf(" Group") == -1)
                            continue;
                        list.Add(sNom.Remove(sNom.LastIndexOf(" Group")));
                        continue;
                    }
                    if (bLevel)
                    {
                        bool bContainGroup = true;
                        if ((((GroupInformation)htPresent[sNom]).NumberGroup < (int)GestionDonneesHUB2SIM.htSizeAutomodModel[sNom]))
                        {
                            list.Add(sNom);
                        }
                        else
                        {
                            bContainGroup = ((GroupInformation)htPresent[sNom]).ContainLevel(iIndexLevel);
                        }
                        if (sNom.LastIndexOf(" Group") == -1)
                            continue;
                        if (!bContainGroup)
                            continue;
                        if (((GroupInformation)htPresent[sNom]).Total < (int)GestionDonneesHUB2SIM.htSizeAutomodModel[sNom.Remove(sNom.LastIndexOf(" Group"))])
                        {
                            list.Add(sNom.Remove(sNom.LastIndexOf(" Group")));
                        }
                    }
                    else if (bLevel) /*Ici mettre bGroup pour le cas ou c'est un groupe et ou l'on veut voir si l'on peut ajouter des objets*/
                    {
                    }
                    else
                    {
                        if (((GroupInformation)htPresent[sNom]).NumberGroup < (int)GestionDonneesHUB2SIM.htSizeAutomodModel[sNom])
                        {
                            list.Add(sNom);
                        }
                    }
                }
                else
                {

                    if ((GestionDonneesHUB2SIM.htSizeAutomodModel.ContainsKey(sNom)) && ((int)GestionDonneesHUB2SIM.htSizeAutomodModel[sNom] == 0))
                        continue;
                    /*Si le nom n'est pas présent dans le groupe, cela veut dire qu'il peut être ajouté sans 
                     restrictions.*/
                    list.Add(sNom);
                    if (sNom.LastIndexOf(" Group") == -1)
                        continue;
                    list.Add(sNom.Remove(sNom.LastIndexOf(" Group")));
                }
            }
            if (list.Count == 0)
                return null;
            string[] sResult = new string[list.Count];
            for (int i = 0; i < sResult.Length; i++)
            {
                sResult[i] = (String)list[i];
            }
            return sResult;
        }

        private class GroupInformation
        {
            private ArrayList alGroupList;
            private ArrayList alLevelList;
            public int IndexMin
            {
                get
                {
                    int iMin = -1;
                    foreach (NodeInformation niTmp in alGroupList)
                    {
                        if ((iMin > niTmp.IndexMin) || (iMin == -1))
                            iMin = niTmp.IndexMin;
                    }
                    if (iMin == -1)
                        return 0;
                    return iMin;
                }
            }
            public int IndexMax
            {
                get
                {
                    int iMax = 0;
                    foreach (NodeInformation niTmp in alGroupList)
                    {
                        if (iMax < niTmp.IndexMax)
                            iMax = niTmp.IndexMax;
                    }
                    return iMax;
                }
            }
            public int Total
            {
                get
                {
                    int iTotal = 0;
                    foreach (NodeInformation niTmp in alGroupList)
                    {
                        iTotal += niTmp.Total;
                    }
                    return iTotal;
                }
            }
            public int NumberGroup
            {
                get
                {
                    return alGroupList.Count;
                }
            }
            public ArrayList GroupList
            {
                get
                {
                    return alGroupList;
                }
            }
            public GroupInformation()
            {
                alGroupList = new ArrayList();
                alLevelList = new ArrayList();
            }
            public void addNodeInformation(int iIndexLevel, NodeInformation niNewGroup)
            {
                alGroupList.Add(niNewGroup);
                alLevelList.Add(iIndexLevel);
            }
            public void addGroupInformation(GroupInformation giNewGroup)
            {
                for (int i = 0; i < giNewGroup.GroupList.Count; i++)
                {
                    alGroupList.Add(giNewGroup.GroupList[i]);
                    alLevelList.Add(giNewGroup.alLevelList[i]);
                }
            }
            public bool ContainLevel(int iIndexLevel)
            {
                foreach (int i in alLevelList)
                {
                    if (i == iIndexLevel)
                        return true;
                }
                return false;
            }
        }
        private class NodeInformation
        {
            private int iIndexMin_;
            private int iIndexMax_;
            public int IndexMin
            {
                get
                {
                    return iIndexMin_;
                }
            }
            public int IndexMax
            {
                get
                {
                    return iIndexMax_;
                }
            }
            public int Total
            {
                get
                {
                    if (iIndexMax_ == 0)
                        return 0;
                    return iIndexMax_ - iIndexMin_ + 1;
                }
            }
            public NodeInformation(int iIndexMin, int iIndexMax)
            {
                iIndexMin_ = iIndexMin;
                iIndexMax_ = iIndexMax;
            }
        }

        private static Hashtable LookForPresent(TreeNode tnNode, int iIndexLevel)
        {
            Hashtable htResults = new Hashtable();
            for (int i = 0; i < tnNode.Nodes.Count; i++)
            {
                String sNodeType = ((TreeViewTag)tnNode.Nodes[i].Tag).AirportObjectType;

                if (!htResults.Contains(sNodeType))
                {
                    htResults.Add(sNodeType, new GroupInformation());
                }
                int iMin = 0;
                int iMax = 0;
                if (tnNode.Nodes[i].Nodes.Count > 0)
                {
                    string sLookedName = sNodeType;
                    if (sNodeType.EndsWith(" Group"))
                    {
                        sLookedName = sNodeType.Remove(sNodeType.LastIndexOf(" Group"));
                    }
                    iMin = minIndex(tnNode.Nodes[i], sLookedName);
                    iMax = maxIndex(tnNode.Nodes[i], sLookedName);
                }
                ((GroupInformation)htResults[sNodeType]).addNodeInformation(iIndexLevel, new NodeInformation(iMin, iMax));
            }
            return htResults;
        }

        private static int minIndex(TreeNode NoeudParent, String sNomObject)
        {
            int min = -1;
            foreach (TreeNode Noeud in NoeudParent.Nodes)
            {
                TreeViewTag tvtTag = (TreeViewTag)Noeud.Tag;
                if (((tvtTag.Index < min) || (min == -1)) && (tvtTag.AirportObjectType == sNomObject))
                {
                    min = tvtTag.Index;
                }
                else
                {
                    int iTmp = minIndex(Noeud, sNomObject);
                    if (((iTmp!=-1)&&(iTmp < min))||(min==-1))
                        min = iTmp;
                }
            }
            return min;
        }
        private static int minIndexLevel(TreeNode NoeudParent, String sNomObject)
        {
            int iMin = -1;
            foreach (TreeNode Noeud in NoeudParent.Parent.Nodes)
            {
                TreeViewTag tvtTag = (TreeViewTag)Noeud.Tag;
                if (tvtTag.AirportObjectType == PAX2SIM.sLevelName)
                {
                    int iTmp = minIndex(Noeud, sNomObject);
                    if ((iTmp < iMin)||(iMin==-1))
                        iMin = iTmp;
                }
            }
            return iMin;
        }

        private static int maxIndex(TreeNode NoeudParent, String sNomObject)
        {
            int max = 0;
            foreach (TreeNode Noeud in NoeudParent.Nodes)
            {
                TreeViewTag tvtTag = (TreeViewTag)Noeud.Tag;
                if ((tvtTag.Index > max) && (tvtTag.AirportObjectType == sNomObject))
                {
                    max = tvtTag.Index;
                }
                else
                {
                    int iTmp = maxIndex(Noeud, sNomObject);
                    if (iTmp > max)
                        max = iTmp;
                }
            }
            return max;
        }
        private static int maxIndexLevel(TreeNode NoeudParent, String sNomObject)
        {
            int iMax = 0;
            foreach (TreeNode Noeud in NoeudParent.Parent.Nodes)
            {
                TreeViewTag tvtTag = (TreeViewTag)Noeud.Tag;
                if (tvtTag.AirportObjectType == PAX2SIM.sLevelName)
                {
                    int iTmp = maxIndex(Noeud, sNomObject);
                    if (iTmp > iMax)
                        iMax = iTmp;
                }
            }
            return iMax;
        }
        #endregion

        private void CB_ObjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///Mise à jour de la liste Groupe Description
            if (CB_ObjectType.SelectedItem != null)
            {
                string CurrentObject = CB_ObjectType.SelectedItem.ToString();
                TXT_ItemDesc.Text = CB_ObjectType.Text;
                EnabledDisabledControls(true, true);
                if (OverallTools.FonctionUtiles.estPresentDansListe(CurrentObject, PAX2SIM.ListeNomsObjetWithoutIndex))
                {
                    EnabledDisabledControls(false, true);
                    CB_GroupDesc.Enabled = false;
                    CB_GroupDesc.Items.Clear();
                    CB_GroupDesc.ResetText();
                }
                else if (!(OverallTools.FonctionUtiles.estPresentDansListe(CurrentObject, PAX2SIM.ListeNomsObjetsGroup)))
                {
                    CB_GroupDesc.Enabled = false;
                    CB_GroupDesc.Items.Clear();
                    CB_GroupDesc.ResetText();
                }
                else
                {
                    CB_GroupDesc.Enabled = true;
                    CB_GroupDesc.Items.Clear();
                    MAJListeGroupeDescription(CurrentObject, leparent);
                    //Un seul groupe transfer (et Shuttle) par terminal.
                    if ((CurrentObject == "Transfer")) //|| (CurrentObject == "Shuttle"))     // << Task #7570 new Desk and extra information for Pax -Phase I B
                    {
                        if (((OverallTools.TreeViewFunctions.RechercherNom("Transfer", leparent.Parent) != null) ||
                            (OverallTools.TreeViewFunctions.RechercherNom("Shuttle", leparent.Parent) != null)) &&
                            (CB_GroupDesc.Items.Count > 0))
                        {
                            CB_GroupDesc.SelectedIndex = 0;
                            CB_GroupDesc.Enabled = false;
                        }
                        if (CurrentObject == "Shuttle")
                        {
                            CHK_Multiple.Checked = false;
                            CHK_Multiple.Enabled = false;
                            TXT_From.Enabled = false;
                            TXT_From.Text = "1";
                        }
                    }
                    else
                    {
                        //CHK_Multiple.Checked = true;
                        CHK_Multiple.Enabled = true;
                        TXT_From.Enabled = true;
                        TXT_From.Text = "";
                    }
                }
                if (CHK_Multiple.Enabled)
                {
                    bool bFind = false;
                    foreach (String text in CB_GroupDesc.Items)
                    {
                        if (text == CB_GroupDesc.Text)
                        {
                            bFind = true;
                            break;
                        }
                    }
                    if (bFind)
                    {
                        TXT_From.Text = "1";
                    }
                    else
                    {
                        TXT_From.Text = "";
                        int iMax = 0;
                        if (((TreeViewTag)leparent.Tag).AirportObjectType == PAX2SIM.sLevelName)
                        {
                            iMax = maxIndexLevel(leparent, CB_ObjectType.Text);
                        }
                        else
                        {
                            iMax = maxIndex(leparent, CB_ObjectType.Text);
                        }

                        TXT_From.Text = (iMax+1).ToString();
                    }
                }
            }
        }

        private void CB_GroupDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (b_ModeEdition)
                return;
            changeTypeSaisie(CB_GroupDesc.SelectedIndex != -1);
        }

        private void changeTypeSaisie(bool state)
        {
            CHK_Multiple.Visible = !state;
            if(!CHK_Multiple.Visible)
                CHK_Multiple.Checked = false;
            TXT_To.Visible = (!state) && (CHK_Multiple.Checked);
            L_A5.Visible = (!state) && (CHK_Multiple.Checked);
            if (state)
            {//On doit simplement demander combien d'objets ajouter
                L_A4.Text = "Number of objects :";
                TXT_From.Text = "1";
                BT_OK.Enabled = true;
            }
            else
            {
                int iMax = 0;
                if (((TreeViewTag)leparent.Tag).AirportObjectType == PAX2SIM.sLevelName)
                {
                    iMax = maxIndexLevel(leparent, CB_ObjectType.Text);
                }
                else
                {
                    iMax = maxIndex(leparent, CB_ObjectType.Text);
                }
                TXT_From.Text = (iMax + 1).ToString();
                L_A4.Text = "From";
                if ((GestionDonneesHUB2SIM.htSizeAutomodModel.ContainsKey(CB_ObjectType.Text)) &&
                    (iMax > (int)GestionDonneesHUB2SIM.htSizeAutomodModel[CB_ObjectType.Text]))
                {
                    BT_OK.Enabled = false;
                }
                else
                {
                    BT_OK.Enabled = true; 
                }
            }
            L_A4.Left = TXT_From.Left - L_A4.Width - 3;
        }

        private void CB_GroupDesc_TextChanged(object sender, EventArgs e)
        {
            if (b_ModeEdition) return;
            String text = CB_GroupDesc.Text;
            int iIndex = CB_GroupDesc.Items.IndexOf(text);
            if (iIndex != -1)
            {
                CB_GroupDesc.SelectedIndex = iIndex;
            }
            changeTypeSaisie(iIndex != -1);
        }

        private void CHK_Multiple_CheckedChanged(object sender, EventArgs e)
        {
            //On change l'état de la text box to. Afin d'être sur qu'aucune valeur ne sera entrée.
            TXT_To.Visible = CHK_Multiple.Checked;
            L_A5.Visible = CHK_Multiple.Checked;

            TXT_To.Text = TXT_From.Text;    // >> Task #15781 Various TreeView assistans improvements
            int fromValue = -1;
            if (Int32.TryParse(TXT_From.Text, out fromValue))
                TXT_To.Text = (fromValue + 1).ToString();
        }

        private void TXT_From_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b') && (e.KeyChar != '\n'))
            {
                MessageBox.Show("Choose some numerical character");
                e.KeyChar = '\0';
            }
        }

        private void TXT_To_TextChanged(object sender, EventArgs e)
        {
            if (!CHK_Multiple.Checked)
                return;
            if (TXT_To.Text.Length == 0)
                return;
            if (!GestionDonneesHUB2SIM.htSizeAutomodModel.ContainsKey(CB_ObjectType.Text))
                return;
            int iToValue;
            if (!Int32.TryParse(TXT_To.Text, out iToValue))
            {
                TXT_To.Text = "";
                return;
            }
            int iMax = (int)GestionDonneesHUB2SIM.htSizeAutomodModel[CB_ObjectType.Text];
            if (iToValue > iMax)
            {
                MessageBox.Show("The index max for that type of object is " + iMax.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TXT_To.Text = iMax.ToString();
            }
        }

        private void TXT_From_TextChanged(object sender, EventArgs e)
        {
            if (CHK_Multiple.Checked)
                return;
            int iFromValue;
            if (!GestionDonneesHUB2SIM.htSizeAutomodModel.ContainsKey(CB_ObjectType.Text))
                return;
            if (!Int32.TryParse(TXT_From.Text, out iFromValue))
                return;
            int iMax = (int)GestionDonneesHUB2SIM.htSizeAutomodModel[CB_ObjectType.Text];

            if (!CHK_Multiple.Visible)
            {
                if (((TreeViewTag)leparent.Tag).AirportObjectType == PAX2SIM.sLevelName)
                {
                    iMax = iMax - maxIndexLevel(leparent, CB_ObjectType.Text);
                }
                else
                {
                    iMax = maxIndex(leparent, CB_ObjectType.Text);
                }
            }

            if ((iFromValue <= 0) || (iFromValue > iMax))
            {
                if (iMax == 0)
                    return;
                MessageBox.Show("The from value must be greater than 0 and lower than " + iMax.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                            TXT_From.Text = ((int)(GestionDonneesHUB2SIM.NbTerminaux - maxIndex() + 1)).ToString();
            }
        }

        private void BT_OK_Click(object sender, EventArgs e)
        {
            if (b_ModeEdition)//Nous sommes en mode édition.
            {
                TreeViewTag ObjectAirportGroup = (TreeViewTag)leparent.Tag;
                if (ObjectAirportGroup.isGroup)
                {
                    ObjectAirportGroup.Name = CB_GroupDesc.Text;
                    leparent.Tag = ObjectAirportGroup;
                    leparent.Text = ObjectAirportGroup.ToString();
                }
                else
                {
                    ObjectAirportGroup.Name = TXT_ItemDesc.Text;
                    leparent.Tag = ObjectAirportGroup;
                    leparent.Text = ObjectAirportGroup.ToString();
                }
                return;
            }
            //Nous sommes en mode création de nouveaux objets.
            if (CB_ObjectType.SelectedIndex == -1)
            {
                MessageBox.Show("The object type must be defined!", "PAX2SIM Alert",MessageBoxButtons.OK,MessageBoxIcon.Error);
                CB_ObjectType.Focus();
                this.DialogResult = DialogResult.None;
                return;
            }
            if (TXT_ItemDesc.Text.Length == 0)
            {
                MessageBox.Show("The Item description must be defined!", "PAX2SIM Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TXT_ItemDesc.Focus();
                this.DialogResult = DialogResult.None;
                return;
            }
            if (TXT_From.Text.Length == 0)
            {
                TXT_From.Text = "1";
            }
            if (TXT_From.Enabled)
            {
                int iFrom;
                if ((!Int32.TryParse(TXT_From.Text, out iFrom))||(iFrom <= 0))
                {
                    MessageBox.Show("Value must be an integer greater than 0.", "PAX2SIM Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TXT_From.Focus();
                    this.DialogResult = DialogResult.None;
                }
                int iMax = 0;
                int iMin = 0;
                if (((TreeViewTag)leparent.Tag).AirportObjectType == PAX2SIM.sLevelName)
                {
                    iMax = maxIndexLevel(leparent, CB_ObjectType.Text);
                    iMin = minIndexLevel(leparent, CB_ObjectType.Text);
                }
                else
                {
                    iMax = maxIndex(leparent, CB_ObjectType.Text);
                    iMin = minIndex(leparent, CB_ObjectType.Text);
                }
                int iMaxDesks=0;
                if (GestionDonneesHUB2SIM.htSizeAutomodModel.ContainsKey(CB_ObjectType.Text))
                    iMaxDesks = (int)GestionDonneesHUB2SIM.htSizeAutomodModel[CB_ObjectType.Text];
                if ((iMin < iFrom) && 
                    (iMax > iFrom) && (CHK_Multiple.Visible))
                {
                    MessageBox.Show("The start index is already define in another group.", "PAX2SIM Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TXT_From.Focus();
                    this.DialogResult = DialogResult.None;
                    return;
                }
                else if ((!CHK_Multiple.Visible) && ((iMax + iFrom - 1) > iMaxDesks) && (iMaxDesks != 0))
                {
                    MessageBox.Show("You cannot add all these desks.", "PAX2SIM Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                    TXT_From.Focus();
                    return;
                }

                if (CHK_Multiple.Checked && CHK_Multiple.Visible)
                {
                    TXT_To_TextChanged(sender, null);
                    int iTo;
                    Int32.TryParse(TXT_To.Text, out iTo);
                    if (TXT_To.Text.Length == 0)
                    {
                        MessageBox.Show("Choose an index for your list of new objects", "PAX2SIM Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TXT_To.Focus();
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    else if (iFrom > iTo)
                    {
                        {
                            TXT_From.Focus();
                            MessageBox.Show("Please set a valid interval for the object indexes", "PAX2SIM Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.DialogResult = DialogResult.None;
                            return;
                        }
                    }
                }
            }
        }

        public NewObjet getNouvelElement()
        {
            //Création d'une nouvelle structure qui permettra de renvoyé toutes les
            //inforations utiles.
            NewObjet obj = new NewObjet();
            obj.NewObjetType = CB_ObjectType.Text;
            obj.VI_From = 0;
            if (!OverallTools.FonctionUtiles.estPresentDansListe(obj.NewObjetType, PAX2SIM.ListeNomsObjetWithoutIndex))
            {
                int iFrom;
                if ((!Int32.TryParse(TXT_From.Text, out iFrom)) || (iFrom <= 0))
                {
                    iFrom = 1;
                }
                obj.VI_From = iFrom;

                if (CHK_Multiple.Checked && CB_GroupDesc.SelectedIndex == -1)
                {
                    int iTo;
                    if ((Int32.TryParse(TXT_To.Text, out iTo)) && (iTo > 0))
                    {
                        obj.VI_To = iTo;
                    }
                }
                else
                {
                    obj.VI_To = obj.VI_From;
                }
            }
            obj.VS_GroupeDesc = CB_GroupDesc.Text;
            obj.VS_ItemDec = TXT_ItemDesc.Text;
            obj.PointedNode = leparent;
            return obj;
        }
    }
}