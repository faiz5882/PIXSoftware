using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.Resources;
using System.Collections;
using System.Reflection;
//Excel
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace SIMCORE_TOOL
{
    class Message 
    {
        private Form fenetre;
        private string msg;
        private static CultureInfo langue = System.Threading.Thread.CurrentThread.CurrentUICulture;
        private string fichier;
        private int i;
        private static ResourceManager manager;
        private static ArrayList res;
        private static ArrayList nomsCtl;
        private static List<TreeNode> noeuds;
        private static string[] langues;//Tableau regroupant les langues 
        public Message(String contenu,Form fen,CultureInfo culture)
        {
            fenetre = fen;
            langue = culture;
            msg = contenu;
            manager = new ResourceManager(fenetre.Name, Assembly.GetExecutingAssembly());
        }

        public void Envoi()
        {
            ResXResourceWriter writer=null;
            i = 1;
            foreach (MessageBox box in fenetre.Controls)
            {
                i++;
                MessageBox.Show(fenetre, msg);
                if (System.Threading.Thread.CurrentThread.CurrentUICulture == CultureInfo.GetCultureInfo("fr"))
                {
                    fichier = fenetre.Name + ".fr.resx";
                }
                else
                {
                    fichier = fenetre.Name + ".resx";
                }
                writer = new ResXResourceWriter(fichier);
                //Si la ressource n'existe pas on la crée 
                if(PAX2SIM.manager.GetString("message"+i)==null)
                {
                    writer.AddResource("message" +i, msg);
                }
                                
            }
            //fermeture du curseur
            writer.Close();
        }

        #region Fonctions de convertion de types

        //Object-->TreeNode
        public static TreeNode ObjectToTreeNode(object obj)
        {
            string typeName = obj.GetType().ToString();
            Assembly a = Assembly.GetAssembly(typeof(Form));
            Type t = a.GetType(typeName);
            TreeNode node = (TreeNode)Activator.CreateInstance(t);
            node = (TreeNode)Convert.ChangeType(obj, t);
            
            return node;
        }

        //Object-->ToolStripSplitButton
        public static ToolStripSplitButton ObjectToToolStripSplitButton(object obj)
        {
            string typeName = obj.GetType().ToString();
            Assembly a = Assembly.GetAssembly(typeof(Form));
            Type t = a.GetType(typeName);
            ToolStripSplitButton boutton = (ToolStripSplitButton)Activator.CreateInstance(t);
            boutton = (ToolStripSplitButton)Convert.ChangeType(obj, t);

            return boutton;
        }

        //Object-->ToolStripMenuItem
        public static ToolStripMenuItem ObjectToToolStripMenuItem(object obj)
        {
            string typeName = obj.GetType().ToString();
            Assembly a = Assembly.GetAssembly(typeof(Form));
            Type t = a.GetType(typeName);
            ToolStripMenuItem b = (ToolStripMenuItem)Activator.CreateInstance(t);
            b = (ToolStripMenuItem)Convert.ChangeType(obj, t);

            return b;
        }

        //Object-->ToolStripButton
        public static ToolStripButton ObjectToToolStripButton(object obj)
        {
            string typeName = obj.GetType().ToString();
            Assembly a = Assembly.GetAssembly(typeof(Form));
            Type t = a.GetType(typeName);
            ToolStripButton b = (ToolStripButton)Activator.CreateInstance(t);
            b = (ToolStripButton)Convert.ChangeType(obj, t);

            return b;
        }

        //Object-->ToolStripLabel
        public static ToolStripLabel ObjectToToolStripLabel(object obj)
        {
            string typeName = obj.GetType().ToString();
            Assembly a = Assembly.GetAssembly(typeof(Form));
            Type t = a.GetType(typeName);
            ToolStripLabel lb = (ToolStripLabel)Activator.CreateInstance(t);
            lb = (ToolStripLabel)Convert.ChangeType(obj, t);

            return lb;
        }

        //Object-->ToolStripStatusLabel
        public static ToolStripStatusLabel ObjectToToolStripStatusLabel(object obj)
        {
            string typeName = obj.GetType().ToString();
            Assembly a = Assembly.GetAssembly(typeof(Form));
            Type t = a.GetType(typeName);
            ToolStripStatusLabel lb = (ToolStripStatusLabel)Activator.CreateInstance(t);
            lb = (ToolStripStatusLabel)Convert.ChangeType(obj, t);

            return lb;
        }

        //Object-->ToolStripSeparator
        public static ToolStripSeparator ObjectToToolStripSeparator(object obj)
        {
            string typeName = obj.GetType().ToString();
            Assembly a = Assembly.GetAssembly(typeof(Form));
            Type t = a.GetType(typeName);
            ToolStripSeparator lb = (ToolStripSeparator)Activator.CreateInstance(t);
            lb = (ToolStripSeparator)Convert.ChangeType(obj, t);

            return lb;
        }

        
        //Object-->CheckBox
        public static System.Windows.Forms.CheckBox ObjectToCheckBox(object obj)
        {
            string typeName = obj.GetType().ToString();
            Assembly a = Assembly.GetAssembly(typeof(Form));
            Type t = a.GetType(typeName);
            System.Windows.Forms.CheckBox lb = (System.Windows.Forms.CheckBox)Activator.CreateInstance(t);
            lb = (System.Windows.Forms.CheckBox)Convert.ChangeType(obj, t);

            return lb;
        }

        //Object-->TabPage
        public static TabPage ObjectToTabPage(object obj)
        {
            string typeName = obj.GetType().ToString();
            Assembly a = Assembly.GetAssembly(typeof(Form));
            Type t = a.GetType(typeName);
            TabPage tp = (TabPage)Activator.CreateInstance(t);
            tp = (TabPage)Convert.ChangeType(obj, t);

            return tp;
        }

        //Object-->GroupBox
        public static System.Windows.Forms.GroupBox ObjectToGroupBox(object obj)
        {
            string typeName = obj.GetType().ToString();
            Assembly a = Assembly.GetAssembly(typeof(Form));
            Type t = a.GetType(typeName);
            System.Windows.Forms.GroupBox gb = (System.Windows.Forms.GroupBox)Activator.CreateInstance(t);
            gb = (System.Windows.Forms.GroupBox)Convert.ChangeType(obj, t);

            return gb;
        }
   
        //Object-->DatagridView
        public static DataGridView ObjectToDatagridView(object obj)
        {
            string typeName = obj.GetType().ToString();
            Assembly a = Assembly.GetAssembly(typeof(Form));
            Type t = a.GetType(typeName);
            DataGridView gv = (DataGridView)Activator.CreateInstance(t);
            gv = (DataGridView)Convert.ChangeType(obj, t);

            return gv;
        }

        //Object-->Control
        public static Control ObjectToControl(object obj)
        {
            Control c = null;
            string typeName = obj.GetType().ToString();
            Assembly a = Assembly.GetAssembly(typeof(Form));
            Type t = a.GetType(typeName);
            if (t != null)
            {
                c = (Control)Activator.CreateInstance(t);
                c = (Control)Convert.ChangeType(obj, t);
            }
            return c;
        }

        #endregion
        
        #region Fonctions de parcours recursifs

        //Methode récursive de parcours d'un controle de type Treeview
        private static List<TreeNode> getNodes(TreeNode node)
        {
            if (node.Nodes.Count != 0)
            {
                foreach (TreeNode tn in node.Nodes)
                {
                    noeuds.Add(tn);
                    getNodes(tn);
                }
            }
            else
            {
                noeuds.Add(node);
            }
            return noeuds;
        }

        //Méthode recursive de parcours tous les controles 
        private static void getControls(Control controle)
        {
            //
            if (controle.HasChildren)
            {
                //Si le controle n'a pas été ajouté
                //et si son type est différent de SplitterPanel
                if(!res.Contains(controle) && (!(controle is SplitterPanel)))
                    //Ajout du controle
                    res.Add(controle);
                foreach (Control c in controle.Controls)
                {
                    getControls(c);
                }
            }
            else
            {
                //Gestion des Treeview
                if (controle is TreeView)
                {
                    TreeView t = (TreeView) controle;
                    //Menu contextuel ?
                    if (t.ContextMenuStrip != null)
                    {
                        foreach (ToolStripMenuItem ctx in t.ContextMenuStrip.Items)
                        {
                            if(!res.Contains(ctx))
                                res.Add(ctx);
                        }
                    }
                    foreach (TreeNode noeud in t.Nodes)
                    {
                        noeuds = new List<TreeNode>();
                        getNodes(noeud);
                        //Si le noeud courant possède des noeuds
                        if (noeuds.Count != 0)
                        {
                            foreach (TreeNode n in noeuds)
                            {
                                //Gestion des menus contextuels
                                 if (n.ContextMenuStrip != null)
                                {
                                    foreach (ToolStripMenuItem ctxm in n.ContextMenuStrip.Items)
                                    {
                                        if(!res.Contains(ctxm))
                                            res.Add(ctxm);
                                    }
                                }
                                if(!res.Contains(n))
                                    res.Add(n);
                            }
                        }
                        //Gestion des menus contextuels
                        if (noeud.ContextMenuStrip != null)
                        {
                            foreach (ToolStripMenuItem ctx in noeud.ContextMenuStrip.Items)
                            {
                                if (!res.Contains(ctx))
                                    res.Add(ctx);
                            }
                        }
                        if (!res.Contains(noeud))
                            res.Add(noeud);
                    }
                }

                //Gestion des controle de type ToolStrip
                if (controle is ToolStrip)
                {
                    ToolStrip tool = (ToolStrip)controle;
                    foreach (ToolStripItem item in tool.Items)
                    {
                        if (!res.Contains(item))
                            res.Add(item);
                    }
                }

                //Gestion des controle de type MenuStrip
                if (controle is MenuStrip)
                {
                    MenuStrip menu=(MenuStrip) controle;
                    foreach (ToolStripMenuItem m in menu.Items)
                    {
                        if (!res.Contains(m))
                            res.Add(m);
                        //S'il ya des sous menus
                        if (m.DropDownItems.Count != 0)
                        {
                            //Parcours des sous menus
                            foreach (Object leSousMenu in m.DropDownItems)
                            {
                                if (!(leSousMenu is ToolStripSeparator))
                                {
                                    ToolStripMenuItem tsm = (ToolStripMenuItem)leSousMenu;
                                    //Ajout du sous menu dans l'arrayList
                                        if (!res.Contains(tsm))
                                    res.Add(tsm);
                                }
                            }
                        }
                    }
                }

                //Si le controle est de type GroupBox
                if(controle is System.Windows.Forms.GroupBox)
                {
                    System.Windows.Forms.GroupBox grpBox = (System.Windows.Forms.GroupBox)controle;
                    res.Add(grpBox);
                    /*foreach (Control grpCtrl in grpBox.Controls)
                    {
                        getControls(grpCtrl);
                    }*/
                }
                //TabPage
                if (controle is TabPage)
                {
                    TabPage page = (TabPage)controle;
                    foreach (Control elt in page.Controls)
                    {
                        if (controle.ContextMenuStrip != null)
                        {
                            foreach (ToolStripMenuItem it in controle.ContextMenuStrip.Items)
                            {
                                res.Add(it);
                            }
                        }
                        getControls(page);
                    }
                    if(!res.Contains(page))
                    {
                        res.Add(page);
                    }
                }

                //Si le controle est un panel
                if (controle is Panel)
                {
                    Panel monPanel = (Panel)controle;
                    foreach (Control element in monPanel.Controls)
                    {
                        getControls(element);
                    }
                }
                if (controle is DataGridView)
                {
                    if (controle.ContextMenu != null)
                    {

                    }
                }
                else
                {
                    if (!res.Contains(controle))
                    {
                        res.Add(controle);
                        if (controle.ContextMenuStrip != null)
                        {
                            foreach (Object it in controle.ContextMenuStrip.Items)
                            {
                                if (!(it is ToolStripSeparator))
                                {
                                    if (!res.Contains(it))
                                    {
                                        ToolStripMenuItem tsmi = it as ToolStripMenuItem;
                                        res.Add(tsmi);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Fonction de recherche de répertoire dans l'arborescence

        public static void DisplaySubDirectories(string strDirectory)
        {
            foreach (string currentSubDirectory in System.IO.Directory.GetDirectories(strDirectory))
            {
                DisplaySubDirectories(currentSubDirectory);
            }
        }

        ///////////////////////////////////////////////
        //Recuperation de toutes les chaines à traduire
        //////////////////////////////////////////////
        public static ArrayList collectStrings()
        {
            ArrayList resultat = new ArrayList();
            //Assembly a = System.Reflection.Assembly.GetAssembly(typeof(ResXFileRef));
            Assembly[] asb = AppDomain.CurrentDomain.GetAssemblies();
            
            foreach (Assembly a in asb)
            {
                bool b = a.GetName().CultureInfo.IsNeutralCulture;
                if ((a.FullName.StartsWith("PAX2SIM")) && (!a.GetName().CultureInfo.IsNeutralCulture))
                {
                    //On récupère tous les noms de ressources
                    string[] tab = a.GetManifestResourceNames();
                    for (int i = 0; i < tab.Length; i++)
                    {
                        string name=tab[i];
                        //resultat.Add(name);//nom fenetre
                        if ((name.EndsWith(".resources"))&& (!name.Contains("fr")))
                        {
                            Stream st = a.GetManifestResourceStream(name);
                            ResourceReader r = new ResourceReader(st);
                            // Ouverture d'un ResourceReader et obtention d'un enumérateur.
                            IDictionaryEnumerator en = r.GetEnumerator();

                            // Parcours et ajout du contenu dans la liste
                            while (en.MoveNext())
                            {
                                string valeur="";
                                if ((en.Key.ToString().EndsWith(".Text")) ||
                                    en.Key.ToString().StartsWith("message") ||
                                    (en.Key.ToString().StartsWith("ligne")) ||
                                    (en.Key.ToString().StartsWith("entete")))
                                {
                                    //Enlever le "&" devant les mots
                                    if (en.Value.ToString().StartsWith("&"))
                                    {
                                        valeur = en.Value.ToString().Substring(1, en.Value.ToString().Length - 1);
                                    }
                                    else
                                    {
                                        valeur = en.Value.ToString();
                                    }
                                    //Vérifier la présence de la chaine
                                    if (resultat.Contains(valeur))
                                    {
                                        string tmp = name + "." + en.Key.ToString();
                                        ArrayList arrTemp = new ArrayList();
                                        //Conversion de la valeur en object
                                        Object obj = valeur as Object;
                                        int pos = resultat.LastIndexOf(obj);
                                        for (int j = 0; j <= pos; j++)
                                        {
                                            //Copie d'une partie de la liste dans une liste temp.
                                            arrTemp.Add(resultat[j]);
                                        }
                                        //Ajout de la nouvelle chaine portant la meme valeur
                                        //Séparateur ','
                                        
                                        arrTemp.Add(tmp);
                                        arrTemp.Add(valeur);
                                        //Ajout du reste de la liste
                                        for (int k = pos+3 ; k < resultat.Count; k++)
                                        {
                                            arrTemp.Add(resultat[k]);
                                        }
                                        resultat = arrTemp;
                                    }
                                    else
                                    {
                                        //Ajout dans la liste
                                        resultat.Add(name+"."+en.Key.ToString());
                                        resultat.Add(valeur);
                                    }
                                }
                            }
                            //fermeture du curseur
                            r.Close();
                        }
                    }
                }
            }
            return resultat;
        }

        public static string getDirectory(Form form)
        {
            //nom du fichier de ressources en fonction de la langue

            string fic = "";
            string resultat = "";
            switch (langue.IsNeutralCulture)
            {
                case true:
                    fic = form.Name + ".resx";
                    break;
                default:
                    fic = form.Name + "." + langue.Name + ".resx";
                    break;
            }

            string rep = "C:/Stage_2010/SIMCORE_TOOL_VS09_26.03/SIMCORE_TOOL_VS08_26.03.2010/SIMCORE_TOOL";
            
            string[] tabFile = Directory.GetFiles(rep);
            resultat = rep + "\\" + fic;
            for (int i = 0; i < tabFile.Length;i++ )
            {
                //Fichier trouvé
                if (tabFile[i].Equals(resultat))
                {
                    resultat = resultat + tabFile[i];
                    break;
                }
                //Sinon
                else
                {
                    
                }
            }
            
            return resultat;
        }

        public static void Dir(string directory)
        {
            string[] files;

            // pour avoir les noms des fichiers et sous-répertoires
            files = Directory.GetFileSystemEntries(directory);

            int filecount = files.GetUpperBound(0) + 1;
            for (int i = 0; i < filecount; i++)
                Console.WriteLine(files[i]);
        }

        #endregion


        #region Fonction de traduction des différents controles d'une fenêtre

        public static void traduire(string fichier,Form fenetre,ResourceManager m)
        {
            //Assemblage à recupérer
            Assembly assemblage;
            //Nom de la ressource
            string ressource;
            //Lecture des ressources
            ResourceReader r=null;

            //Culture courante
            string langue = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            CultureInfo culture = System.Threading.Thread.CurrentThread.CurrentUICulture;

            //liste des assemblages du domaine d'application 
            Assembly[] asb = AppDomain.CurrentDomain.GetAssemblies();

            //Nom du fichier de ressources
            foreach (Assembly a in asb)
            {
                if (a.FullName.StartsWith("PAX2SIM") /*&& (a.GetName().CultureInfo.Name.Equals(langue))*/)
                {
                    //Langue par défaut (en)
                    if (langue.Equals("en"))
                    {
                        ressource = fichier + ".resources";
                        assemblage = a;
                    }
                    //Autres langues
                    else
                    {
                        ressource =fichier + "." + langue + ".resources";
                        assemblage = a.GetSatelliteAssembly(culture);
                    }
                    string[] tab = assemblage.GetManifestResourceNames();
                    foreach (string nom in tab)
                    {
                        if (nom.Equals(ressource))
                        {
                            Stream st = assemblage.GetManifestResourceStream(nom);
                            r = new ResourceReader(st);
                            break;
                        }
                    }
                    break;
                }
            }
            
            //ResXResourceReader reader = new ResXResourceReader(System.IO.Directory.GetCurrentDirectory() + "/" + fichier);
            //ResXResourceReader reader = new ResXResourceReader(fichier);

            //Si la ressource existe
            if (r!=null)
            {
                string nom ="";
                nomsCtl = new ArrayList();
                res = new ArrayList();
                // noeuds = new List<TreeNode>();

                // Parcours du fichier .resources
                foreach (DictionaryEntry d in r)
                {
                    string cle = d.Key.ToString();
                    string valeur = d.Value.ToString();
                    //On récupere les chaines se terminant par ".Text"
                    //Ensuite extraction du nom du controle
                    if (cle.EndsWith(".Text"))
                    {
                        nom = cle.Substring(0, cle.Length - 5);
                        if (!nomsCtl.Contains(nom))
                        //Ajout du nom du ctl dans la liste
                        nomsCtl.Add(nom);
                    }

                   if (cle.EndsWith(".ToolTipText"))
                    {
                        nom = cle.Substring(0, cle.Length - 12);
                        if (!nomsCtl.Contains(nom))
                        //Ajout du nom du ctl dans la liste
                        nomsCtl.Add(nom);
                    }
                }

                //Parcours des controles de la fenetre
                foreach (Control ctl in fenetre.Controls)
                {
                    //Parcours des sous controles
                    getControls(ctl);
                }
                    if (res.Count != 0)
                    {
                        for (int j = 0; j < res.Count; j++)
                        {
                            //Conversion de l'objet en son type d'origine
                            switch (res[j].GetType().ToString())
                            {
                                case "System.Windows.Forms.TreeNode":
                                    TreeNode tmpN = ObjectToTreeNode(res[j]);
                                    //Recherche de l'élément dans la liste
                                    for (int k = 0; k < nomsCtl.Count; k++)
                                    {
                                        //Si le controle a été trouvé
                                        if ((tmpN != null) && (tmpN.Name.Equals(nomsCtl[k])))
                                        {
                                            tmpN.Text = m.GetString(nomsCtl[k] + ".Text");
                                            //tmpN.Text = m.GetString(tmpN.Name + ".Text");
                                            break;
                                        }
                                    }
                                    break;

                                case "System.Windows.Forms.ToolStripSplitButton":
                                    ToolStripSplitButton tmpB = ObjectToToolStripSplitButton(res[j]);

                                    //Recherche de l'élément dans la liste
                                    for (int k = 0; k < nomsCtl.Count; k++)
                                    {
                                        //Si le controle a été trouvé
                                        if ((tmpB != null) && (tmpB.Name.Equals(nomsCtl[k])))
                                        {
                                            tmpB.Text = m.GetString(nomsCtl[k] + ".Text");
                                            //tmpB.Text = m.GetString(tmpB.Name + ".Text");
                                            break;
                                        }
                                    }
                                    break;
                                
                                case "System.Windows.Forms.ToolStripButton":
                                    ToolStripButton tmpTb = ObjectToToolStripButton(res[j]);
                                    //Recherche de l'élément dans la liste
                                    for (int k = 0; k < nomsCtl.Count; k++)
                                    {
                                        //Si le controle a été trouvé
                                        if ((tmpTb != null) && (tmpTb.Name.Equals(nomsCtl[k])))
                                        {
                                            tmpTb.ToolTipText = m.GetString(nomsCtl[k] + ".ToolTipText");
                                            tmpTb.Text = m.GetString(nomsCtl[k] + ".Text");
                                            break;
                                        }
                                    }
                                    break;

                                case "System.Windows.Forms.ToolStripMenuItem":
                                    ToolStripMenuItem tmpMI = ObjectToToolStripMenuItem(res[j]);
                                    //Recherche de l'élément dans la liste
                                    for (int k = 0; k < nomsCtl.Count; k++)
                                    {

                                        if ((tmpMI != null) && (tmpMI.Name.Equals(nomsCtl[k])))
                                        {
                                            tmpMI.Text = m.GetString(nomsCtl[k] + ".Text");
                                            tmpMI.ToolTipText = m.GetString(nomsCtl[k] + ".ToolTipText");
                                            break;
                                        }
                                    }
                                    break;

                                case "System.Windows.Forms.ToolStripLabel":
                                    ToolStripLabel tmpL = ObjectToToolStripLabel(res[j]);
                                    //Recherche de l'élément dans la liste
                                    for (int k = 0; k < nomsCtl.Count; k++)
                                    {
                                        //Si le controle a été trouvé
                                        if ((tmpL != null) && (tmpL.Name.Equals(nomsCtl[k])))
                                        {
                                            tmpL.Text = m.GetString(nomsCtl[k] + ".Text");
                                            //tmpL.Text = m.GetString(tmpL.Name + ".Text");
                                            break;
                                        }
                                    }
                                    break;

                                case "System.Windows.Forms.ToolStripStatusLabel":
                                    ToolStripStatusLabel tmpLb = ObjectToToolStripStatusLabel(res[j]);
                                    //Recherche de l'élément dans la liste
                                    for (int k = 0; k < nomsCtl.Count; k++)
                                    {
                                        //Si le controle a été trouvé
                                        if ((tmpLb != null) && (tmpLb.Name.Equals(nomsCtl[k])))
                                        {
                                            tmpLb.Text = m.GetString(nomsCtl[k] + ".Text");
                                            //tmpLb.Text = m.GetString(tmpLb.Name + ".Text");
                                            break;
                                        }
                                    }
                                    break;
                                case "System.Windows.Forms.CheckBox":
                                    System.Windows.Forms.CheckBox cb = ObjectToCheckBox(res[j]);
                                    //Recherche de l'élément dans la liste
                                    for (int k = 0; k < nomsCtl.Count; k++)
                                    {
                                        //Si le controle a été trouvé
                                        if ((cb != null) && (cb.Name.Equals(nomsCtl[k])))
                                        {
                                            cb.Text = m.GetString(nomsCtl[k] + ".Text");
                                            //tmpLb.Text = m.GetString(tmpLb.Name + ".Text");
                                            break;
                                        }
                                    }
                                    break;
                                case "System.Windows.Forms.TabPage":
                                    TabPage tmpTab = ObjectToTabPage(res[j]);
                                    //Recherche de l'élément dans la liste
                                    for (int k = 0; k < nomsCtl.Count; k++)
                                    {
                                        //Si le controle a été trouvé
                                        if ((tmpTab != null) && (tmpTab.Name.Equals(nomsCtl[k])))
                                        {
                                            tmpTab.Text = m.GetString(nomsCtl[k] + ".Text");
                                            //tmpLb.Text = m.GetString(tmpLb.Name + ".Text");
                                            break;
                                        }
                                    }
                                    break;

                                case "System.Windows.Forms.GroupBox":
                                    System.Windows.Forms.GroupBox tmpGb = ObjectToGroupBox(res[j]);
                                    //Recherche de l'élément dans la liste
                                    for (int k = 0; k < nomsCtl.Count; k++)
                                    {
                                        //Si le controle a été trouvé
                                        if ((tmpGb != null) && (tmpGb.Name.Equals(nomsCtl[k])))
                                        {
                                            tmpGb.Text = m.GetString(nomsCtl[k] + ".Text");
                                            break;
                                        }
                                    }
                                    break;

                                case "DataGridView":
                                    DataGridView dgv = ObjectToDatagridView(res[j]);
                                    //Recherche de l'élément dans la liste
                                    for (int k = 0; k < nomsCtl.Count; k++)
                                    {
                                        //Si le controle a été trouvé
                                        if ((dgv != null) && (dgv.Name.Equals(nomsCtl[k])))
                                        {
                                            dgv.Text = m.GetString(nomsCtl[k] + ".Text");
                                            break;
                                        }
                                    }
                                    break;
                                    
                                default:
                                    Control tmp = ObjectToControl(res[j]);
                                    //Recherche de l'élément dans la liste
                                    for (int k = 0; k < nomsCtl.Count; k++)
                                    {
                                        //Si le controle a été trouvé
                                        if ((tmp != null) && (tmp.Name.Equals(nomsCtl[k])))
                                        {
                                            tmp.Text = m.GetString(nomsCtl[k] + ".Text");
                                            //tmp.Text = m.GetString(tmp.Name + ".Text");
                                            break;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                //}for(res) comment le 15/06/2010
                //Fermeture du curseur
                r.Close(); 
            }
            
        }

        #endregion

        #region Fonction export des ressources vers format xls

        public static void exportXls(string fichier)
        {
            ArrayList arrChaines = collectStrings();  
            Workbooks oBooks = null;
            ApplicationClass application = new ApplicationClass();
            application.Visible = false; // l’afficher ou pas
            application.ScreenUpdating = false;
            application.Interactive = false;
            application.DisplayAlerts = false;
            
            //Création du fichier Excel
            oBooks = application.Workbooks;
            //Workbook workbook = oBooks.Add(Missing.Value);

            //Ouverture du fichier
            Workbook workbook = application.Workbooks.Open(fichier, Type.Missing, false, Type.Missing,
                                                            Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                            Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                            Type.Missing, Type.Missing, Type.Missing);
            //Activation de la feuille
            _Worksheet worksheet = (_Worksheet)workbook.ActiveSheet;

            //Ecriture 
            ((Range)worksheet.Cells[1, 2]).Interior.Color = System.Drawing.Color.Green.ToArgb();
            ((Range)worksheet.Cells[1, 2]).Value2 = langue.DisplayName;

            for (int i = 0; i < arrChaines.Count; i++)
            {
                if (!(arrChaines[i].ToString().EndsWith(".Text")||
                    arrChaines[i].ToString().Contains("message")||
                    (arrChaines[i].ToString().Contains("ligne"))||
                    (arrChaines[i].ToString().Contains("entete"))))
                {
                    ((Range)worksheet.Cells[i + 3, 2]).Value2 = arrChaines[i];
                }
                else
                {
                    ((Range)worksheet.Cells[i + 4, 1]).Value2 = arrChaines[i];
                }
            }
            
            /*for (int i = 0; i < arrChaines.Count; i++)
            {
                if ((arrChaines[i].ToString().EndsWith(".Text")) ||
                                    arrChaines[i].ToString().StartsWith("message") ||
                                    (arrChaines[i].ToString().StartsWith("ligne")) ||
                                    (arrChaines[i].ToString().StartsWith("entete")) ||
                                    (arrChaines[i].ToString().EndsWith(".resources")))
                {
                    ((Range)worksheet.Cells[i + 4, 1]).Borders.Color = System.Drawing.Color.Green.ToArgb();
                    ((Range)worksheet.Cells[i + 4, 1]).Value2 = arrChaines[i];
                }
                else
                {
                    ((Range)worksheet.Cells[i + 3, 2]).Value2 = arrChaines[i];
                }
            }*/
            //Enregistrement des modifications
            workbook.Save();

            //Fermeture
            workbook.Close(false, Type.Missing, Type.Missing);

            if (worksheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                worksheet = null;
            }
            if (workbook != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                workbook = null;
            }
            if (oBooks != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
                oBooks = null;
            }
            if (application != null)
            {
                application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
                application = null;
            }
           
            GC.Collect();
        }

        public static void writeExcel(string fichier)
        {
            ArrayList arrChaines = collectStrings();  
            /*** EXCELWRITER ***/
            // Déclarer et ouvrir l'écrivain :
            ExcelWriter EW = new ExcelWriter();
            EW.Open(fichier);

           // string[] mesValeurs = new string[6] { "Pilotage", "de", "l'application", "Office", "Excel", "(Automation Excel)" };

            // Je peux parcourir ce tableau pour renseigner mon fichier Excel :
            foreach (string str in arrChaines)
            {
                EW.Write(str);
                EW.NewLine();
            }

            // Je peux envoyer le tableau entier pour écrire une ligne :
            EW.WriteLine(arrChaines);

            // Ne pas oublier de fermer l'écrivain, c'est là qu'il enregistre :
            EW.Close();
        }

        #endregion


        #region Fonction import d'un fichier xls vers format resx

        //Le nombre de lignes renseignées 
        private static int GetRowCount(int FirstRowIndex, int LastRowIndex, int MiddleRowIndex, _Worksheet ws)
        {
            if ((Range)ws.Cells[MiddleRowIndex, 1] != null &&
                ((Range)ws.Cells[MiddleRowIndex, 1]).Value2 != null)
            {
                if ((Range)ws.Cells[MiddleRowIndex + 1, 1] != null &&
                    ((Range)ws.Cells[MiddleRowIndex + 1, 1]).Value2 == null)
                {
                    if (((Range)ws.Cells[MiddleRowIndex + 2, 1]).Value2 != null)
                    {
                        //MiddleRowIndex = MiddleRowIndex + 2;
                       // return GetRowCount(MiddleRowIndex, LastRowIndex, (int)Math.Ceiling((double)(MiddleRowIndex + ((LastRowIndex - MiddleRowIndex) / 2))), ws);
                    }
                    // La ligne suivante n'est pas renseignée, le résultat est :
                    return MiddleRowIndex;
                }
                else
                {
                    // La ligne suivante est renseignée, rechercher dans l'intervalle supérieur :
                    return GetRowCount(MiddleRowIndex, LastRowIndex, (int)Math.Ceiling((double)(MiddleRowIndex + ((LastRowIndex - MiddleRowIndex) / 2))),ws);
                }
            }
            else
            {
                // La ligne n'est pas renseignée, rechercher dans l'intervalle inférieur :
                return GetRowCount(FirstRowIndex, MiddleRowIndex, (int)Math.Ceiling((double)(FirstRowIndex + ((MiddleRowIndex - FirstRowIndex) / 2))),ws);
            }
        }

        public static int getRowCount(_Worksheet ws,int nbElements)
        {
            int compteur = 0;
            for (int i = 1; i < ws.Rows.Count;i++ )
            {
                if (i <= nbElements)
                {
                    if ((Range)ws.Cells[i, 1] != null &&
                    ((Range)ws.Cells[i, 1]).Value2 != null)
                    {
                        compteur++;
                    }
                }
                else
                {
                    break;
                }
                
            }
            return compteur;
        }

        //Le nombre de colonnes renseignées
        private static int GetColumnCount(int FirstColumnIndex, int LastColumnIndex, int MiddleColumnIndex, _Worksheet ws)
        {
            if ((Range)ws.Cells[1, MiddleColumnIndex] != null &&
                ((Range)ws.Cells[1, MiddleColumnIndex]).Value2 != null&&
                (Range)ws.Cells[5, MiddleColumnIndex] != null&&
                ((Range)ws.Cells[5, MiddleColumnIndex]).Value2 != null)
            {
                if ((Range)ws.Cells[1, MiddleColumnIndex + 1] != null &&
                    ((Range)ws.Cells[1, MiddleColumnIndex + 1]).Value2 == null)
                {
                    // La ligne suivante n'est pas renseignée, le résultat est :
                    return MiddleColumnIndex;
                }
                else
                {
                    // La ligne suivante est renseignée, rechercher dans l'intervalle supérieur :
                    return GetColumnCount(MiddleColumnIndex, LastColumnIndex, (int)Math.Ceiling((double)(MiddleColumnIndex + ((LastColumnIndex - MiddleColumnIndex) / 2))),ws);
                }
            }
            else
            {
                // La ligne n'est pas renseignée, rechercher dans l'intervalle inférieur :
                return GetColumnCount(FirstColumnIndex, MiddleColumnIndex, (int)Math.Ceiling((double)(FirstColumnIndex + ((MiddleColumnIndex - FirstColumnIndex) / 2))),ws);
            }
        }

        //Importer un fichier excel au format resx v1
        public static void importXls(string fichier)
        {
            ArrayList contenu = new ArrayList();
            ArrayList arr = collectStrings();
            FileInfo info = new FileInfo(fichier);
            ResXResourceWriter writer = new ResXResourceWriter("");
            ResXResourceWriter rw=null;
            //Si le fichier existe
            if (info.Exists)
            {  
                Workbooks oBooks = null;
                ApplicationClass application = new ApplicationClass();
                application.Visible = false; // l’afficher ou pas
                application.ScreenUpdating = false;
                application.Interactive = false;
                application.DisplayAlerts = false;
                
                //Création du fichier Excel
                //oBooks = application.Workbooks;
                //Workbook workbook = oBooks.Add(Missing.Value);

                //Ouverture du fichier
                Workbook workbook = application.Workbooks.Open(fichier, Type.Missing, false, Type.Missing,
                                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                                Type.Missing, Type.Missing, Type.Missing);
                //Activation de la feuille
                _Worksheet worksheet = (_Worksheet)workbook.ActiveSheet;
                //nb lignes et colonnes non vides
                int debut = 1;
                int finCol = worksheet.Columns.Count;
                int milieuCol = (debut + finCol) / 2;
                
                int nbColonnes = GetColumnCount(debut,finCol,milieuCol,worksheet);
                int fin = worksheet.Rows.Count;
                int milieu = (debut + fin) / 2;
                //int nbLignes = GetRowCount(debut, fin, milieu, worksheet);
                int nbLignes = getRowCount(worksheet,arr.Count);
                //Lecture
                //Parcours des colonnes
                //modif j=3
                for (int j = 1; j <= nbColonnes; j++)
                {
                    //Parcours des lignes
                    for (int i = 1; i <= nbLignes; i++)
                    {
                        //Si la valeur n'est pas vide
                        if (((Range)worksheet.Cells[1, j+2]).Value2 != null)
                        {
                            if (((Range)worksheet.Cells[i + 4, j+2]).Value2 != null)
                            {
                                if (!((Range)worksheet.Cells[i + 4, j+2]).Value2.ToString().Equals(string.Empty))
                                {
                                    if (((Range)worksheet.Cells[i + 4, j+2]).Value2 != null && (((Range)worksheet.Cells[i + 4, j]).Value2!=null))
                                    {
                                        if (((Range)worksheet.Cells[i + 3, 1]).Value2 != null &&
                                            (!((Range)worksheet.Cells[i + 3, 1]).Value2.ToString().Equals(string.Empty))&&
                                            ((Range)worksheet.Cells[i + 3, 1]).Value2.ToString().EndsWith(".resources"))
                                        {
                                            //On recupère le nom de la fenetre
                                            string nomFen = ((Range)worksheet.Cells[i + 3, 1]).Value2.ToString();
                                            contenu.Add(nomFen);
                                        }
                                        string valeur = ((Range)worksheet.Cells[i + 4, j+2]).Value2.ToString();
                                        string cle = ((Range)worksheet.Cells[i + 4, j]).Value2.ToString();
                                        //Ajout de la valeur precedée de la clé
                                        contenu.Add(cle);
                                        contenu.Add(valeur);
                                    }
                                }
                            }
                        }
                    }
                }
                //Fermeture
                workbook.Close(false, Type.Missing, Type.Missing);

                if (worksheet != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    worksheet = null;
                }
                if (workbook != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    workbook = null;
                }
                if (oBooks != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
                    oBooks = null;
                }
                if (application != null)
                {
                    application.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
                    application = null;
                }
                GC.Collect();

                //Ajout des chaines dans les fichiers de ressources
                for (int k = 0; k < contenu.Count; k++)
                {
                    if (contenu[k].ToString().EndsWith(".resources"))
                    {
                        string nomFenetre = contenu[k].ToString().Substring(0, (contenu[k].ToString().Length - 10));
                        string[] mots = nomFenetre.Split('.');
                        string chemin = @"C:/Stage_2010/SIMCORE_TOOL_VS09_26.03/SIMCORE_TOOL_VS08_26.03.2010/";
                        for (int cpt = 0; cpt < mots.Length; cpt++)
                        {
                            if (cpt != mots.Length - 1)
                            {
                                chemin += mots[cpt] + "/";
                            }
                            else
                            {
                                chemin += mots[cpt];
                            }
                        }
                        string nomFichier = chemin + ".es" + ".resx";
                        
                        FileInfo inf = new FileInfo(nomFichier);
                        //Si le fichier existe
                        if (inf.Exists)
                        {
                            //Ouverture et ecriture
                            rw = new ResXResourceWriter(inf.OpenWrite());
                            rw.AddResource(contenu[k - 1].ToString(), contenu[k].ToString());
                        }
                        else
                        {
                            //création du fichier de ressources
                            rw = new ResXResourceWriter(nomFichier);
                        }
                        
                        writer = rw;
                    }
                    else
                    {
                        //Ajout de la chaine dans le fichier
                        writer.AddResource(contenu[k-1].ToString(), contenu[k].ToString());
                    }
                }
                //Fermeture du curseur
                writer.Close();
                rw.Close();
            }
        }

        //Importer un fichier excel au format resx v2
        //Utilisation de la classe ExcelReader
        public static void readExcel1(string fichier)
        {
            ArrayList resExcel = new ArrayList();
            ResXResourceWriter rw = null;
            ResXResourceWriter writer = new ResXResourceWriter("");

            /*** EXCELREADER ***/
            // Déclarer et ouvrir le lecteur :
            ExcelReader ER = new ExcelReader();
            ER.Open(@"C:\Stage_2010\classeur.xls");

            // Affichage des valeurs :
            while (ER.Peek() >= 0)
            {
                resExcel.Add(ER.ReadLine());
            }

            //fermeture du lecteur :
            ER.Close();

             //Contenu du fichier Excel --> fichier ressources

            //Récupération des entetes de colonnes donc des langues
            //langues = resExcel[0].ToString().Split(';');
           /* for (int k = 0; k < langues.Length; k++)
            {

            }  */
                //Parcours de la liste 
                for (int i = 0; i < resExcel.Count; i++)
                {
                    //Récupération des entetes de colonnes donc des langues
                    if (i == 0)
                    {
                        //string val = resExcel[0].ToString().Substring(resExcel[0].ToString().Length);
                        langues = resExcel[0].ToString().Split(';');
                        continue;
                    }
                    //On récupère la ligne en cours
                    string ligne = resExcel[i].ToString();
                    //Séparation du contenu de la ligne
                    //contenu sous la forme C11;C21;C31;C41 avec C11=col1,ligne1
                    string[] contenuLigne = ligne.Split(';');
                    //Si la ligne n'est pas vide
                    if (!(contenuLigne[0].Equals(string.Empty) && contenuLigne[contenuLigne.Length - 1].Equals(string.Empty)))
                    {
                        string cle = "";
                        string valeur = "";
                        for (int k = 1; k < langues.Length; k++)//DebFor langues
                        {
                            //S'il la langue en cours n'est pas la langue par défaut
                            if (!langues[k].ToLower().StartsWith("en") && (!langues[k].Equals(string.Empty)))
                            {
                                for (int j = 0; j < contenuLigne.Length; j++)//DebFor contenuLigne
                                {
                                    //SI la chaine est une propriété d'un controle
                                    if (contenuLigne[j].EndsWith(".Text") || (contenuLigne[j].StartsWith("message")) ||
                                        contenuLigne[j].StartsWith("entete") || (contenuLigne[j].StartsWith("ligne")))
                                    {
                                        //On recupère la propriété du controle
                                        cle = contenuLigne[j];
                                        valeur = contenuLigne[k];
                                        break;
                                    }

                                    string encours = contenuLigne[j];
                                    //Si la valeur a été renseignée
                                    if (!encours.Equals(string.Empty))
                                    {
                                        //Extraction du nom de la fenetre 
                                        if (encours.EndsWith(".resources"))
                                        {
                                            //Fermeture du curseur
                                            if (writer != null)
                                            {
                                                writer.Close();
                                            }
                                            string nomFenetre = ligne.Substring(0, (ligne.Length - 13));
                                            string[] mots = nomFenetre.Split('.', ';');
                                            string chemin = @"C:/Stage_2010/SIMCORE_TOOL_VS09_26.03/SIMCORE_TOOL_VS08_26.03.2010/";
                                            for (int cpt = 0; cpt < mots.Length; cpt++)
                                            {
                                                if (cpt != mots.Length - 1)
                                                {
                                                    chemin += mots[cpt] + "/";
                                                }
                                                else
                                                {
                                                    chemin += mots[cpt];
                                                }
                                            }
                                            string nomFichier = chemin + "." + langues[k] + ".resx";
                                            FileInfo inf = new FileInfo(nomFichier);
                                            //Si le fichier existe
                                            if (inf.Exists)
                                            {
                                                //Ouverture et ecriture
                                                //rw = new ResXResourceWriter(inf.OpenWrite());
                                                //rw.AddResource(contenuLigne[j - 1], encours);
                                            }
                                            else
                                            {
                                                //création du fichier de ressources
                                                rw = new ResXResourceWriter(nomFichier);
                                            }
                                            writer = rw;
                                        }
                                        //Si la valeur est une chaine ordinaire
                                        else
                                        {
                                            //Ajout de la chaine dans le fichier
                                            writer.AddResource(cle, encours);
                                            //writer.AddResource(contenuLigne[j - 1], encours);
                                        }
                                    }
                                }//Ffor contenuLigne  
                                writer.AddResource(cle, valeur);
                                break;
                            }//FinIf
                        }//Ffor langues 
                    }//FinIf non vide
                }
            //Fermeture des curseurs
            writer.Close();
            rw.Close();
        }


        //**********************************************************
        //Importer un fichier excel au format resx v3
        //Utilisation de la classe ExcelReader
        //Bonne version
        //**********************************************************
        public static void readExcel(string fichier)
        {
            ArrayList resExcel = new ArrayList();
            ResXResourceWriter rw = null;
            ResXResourceWriter writer = new ResXResourceWriter("");
            //Nom de la fenetre encours lors de la lecture du fichier
            string fenetreEncours = "";
            string fenetrePrec = "";
            //Detection d'un changement de fenetre
            bool changement = false;

            /*** EXCELREADER ***/
            // Déclarer et ouvrir le lecteur :
            ExcelReader ER = new ExcelReader();
            ER.Open(@"C:\Stage_2010\classeur.xls");

            // Affichage des valeurs :
            while (ER.Peek() >= 0)
            {
                //string[] tab=ER.ReadLine().Split(';');
                //if(tab[1]!="")
                    resExcel.Add(ER.ReadLine());
            }

            //fermeture du lecteur :
            ER.Close();
            //Tri des éléments et suppression des lignes vides
            resExcel.Sort();
            string val = ";en;es";
            Object obj = val as Object;
            int index = resExcel.IndexOf(val, 0);
            resExcel.RemoveRange(0, index);
            //Récupération des entetes de colonnes :les langues
            langues = resExcel[0].ToString().Split(';');
            //Parcours par langue
            for (int k = 1; k <langues.Length; k++)
            {
                //S'il la langue en cours n'est pas la langue par défaut
                if (!langues[k].ToLower().StartsWith("en") && (!langues[k].Equals(string.Empty)))
                {
                    //Parcours de la liste 
                    for (int i = 1; i < resExcel.Count; i++)
                    {
                        //On récupère la ligne en cours
                        string ligne = resExcel[i].ToString();
                        //Séparation du contenu de la ligne
                        //contenu sous la forme C11;C21;C31;C41 avec C11=col1,ligne1
                        string[] contenuLigne = ligne.Split(';');

                        //Récuperation du nom de fenetre encours
                        fenetreEncours = contenuLigne[0].Substring(0,contenuLigne[0].LastIndexOf("resources")-1);
                        if (i != 1)
                        {
                            fenetrePrec = resExcel[i - 1].ToString().Substring(0, resExcel[i - 1].ToString().IndexOf("resources") - 1);
                        }
                        else
                        {
                            fenetrePrec = fenetreEncours;
                        }
                        //détection d'un changement de fenetre
                        if (fenetrePrec==fenetreEncours)
                        {
                            changement = false;
                        }
                        else
                        {
                            changement = true;
                        }

                        if (changement)
                        {

                            //Fermeture du curseur
                            writer.Close();
                            rw.Close();
                        }
                        //Si la ligne n'est pas vide
                        if (!(contenuLigne[0].Equals(string.Empty) && contenuLigne[contenuLigne.Length - 1].Equals(string.Empty)))
                        {
                            string cle = "";
                            string valeur = "";
                            for (int j = 0; j < contenuLigne.Length; j++)//DebFor contenuLigne
                            {
                                //Découpage de la chaine
                                string[] leContenu = contenuLigne[0].Split('.');
                                string propCtl = ""; //proprété du controle
                                string fenetre = ""; //Nom de la fenetre
                                if (leContenu.Length != 0)
                                {
                                    if (contenuLigne[j].EndsWith(".Text"))
                                        propCtl = leContenu[leContenu.Length - 2] + "." + leContenu[leContenu.Length - 1];
                                    else
                                        propCtl = leContenu[leContenu.Length - 1];

                                    int pos = contenuLigne[0].LastIndexOf("resources") - 1;
                                    fenetre = contenuLigne[0].Substring(0, pos).Replace(".", "/");
                                    string chemin1 = @"C:/Stage_2010/SIMCORE_TOOL_VS09_26.03/SIMCORE_TOOL_VS08_26.03.2010/";
                                    string nomFichier = chemin1 + fenetre + "." + langues[k] + ".resx";
                                    //Création du fichier
                                    FileInfo inf = new FileInfo(nomFichier);
                                    //Si le fichier existe
                                    if (inf.Exists)
                                    {
                                        //Ouverture et ecriture
                                        //rw = new ResXResourceWriter(inf.OpenWrite());
                                        //rw.AddResource(cle, valeur);
                                        //rw.AddResource(contenuLigne[j - 1], encours);
                                    }
                                    else
                                    {
                                        //création du fichier de ressources
                                        rw = new ResXResourceWriter(nomFichier);
                                    }
                                    writer = rw;
                                }
                                //SI la chaine est une propriété de controle
                                if (propCtl.EndsWith(".Text") || (contenuLigne[j].Contains("message")) ||
                                    propCtl.Contains("entete") || (propCtl.Contains("ligne")))
                                {
                                    //On recupère la propriété du controle
                                    //cle = contenuLigne[j];
                                    cle = propCtl;
                                    valeur = contenuLigne[k];
                                    break;
                                }
                            }//FinFor contenuLigne
                            if (!cle.Equals(string.Empty))
                                writer.AddResource(cle, valeur);
                            //continue;
                        }//FinIf ligne vide 
                    }
                } //FinIf 
            } //FinFor langues
        }

        #endregion
    }
}
