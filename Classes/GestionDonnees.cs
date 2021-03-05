using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Xml;
using System.Windows.Forms;
using SIMCORE_TOOL.Prompt;

namespace SIMCORE_TOOL.Classes
{
    #region La classe permettant la gestion des tables.
    /// <summary>
    /// La class GestionDonnees est conçue pour supporter le système de données de Pax2Sim.
    /// Elle permet ainsi de stocké des tables avec toutes les informations qui les concerne.
    /// (mode de visualisation, filtres, graphique associé)
    /// </summary>
    public class GestionDonnees
    {
        #region Fonction which returns the number of table present in the class
        public int getTableNumber()
        {
            return StockageTables.Tables.Count + _htPartialOpened.Keys.Count;
        }
        #endregion

        #region Les variables statiques de la classe.
        //La liste permettant d'exporter les éventuelles erreurs.
        public static ArrayList ListeErreurs;
        #endregion

        #region Les différentes variables de la classe.
        /// <summary>
        /// Le nom de ce système de données.
        /// </summary>
        private String _Nom;

        /// <summary>
        /// Définition du dataset contenant toutes les tables de la base de données.
        /// </summary>
        private DataSet _StockageTables;

        /// <summary>
        /// Liste des filtres créés par l'utilisateur sur les tables.
        /// </summary>
        private Hashtable _ListFilters;

        /// <summary>
        /// Liste des filtres d'affichage du graphique.
        /// </summary>
        private Hashtable _ListGraphicFilters;

        /// <summary>
        /// Liste des différents mode de visualisation des tables.
        /// </summary>
        private Hashtable _ListModeVisualisation;

        private XmlDocument _xdProjectUserData;
        private List<String> _ListUserData;

        private XmlNode _UserStructure;

        private ArrayList _ListAutomodGraphic;

        private Dictionary<String, String> _htPartialOpened;

        /// <summary>
        /// Contains the names of the table that can be calculated from the traces.
        /// </summary>
        private List<String> _lsListTableCalculatedFromTrace;
        private Boolean bUseTraceSaving;
        private String sBagTraceLocation;
        private String sPaxTraceLocation;

        public String Nom
        {
            get
            {
                return _Nom;
            }
            set
            {
                _Nom = value;
            }
        }
        public DataSet StockageTables
        {
            get
            {
                return _StockageTables;
            }
            set
            {
                _StockageTables = value;
            }
        }
        public Hashtable ListFilters
        {
            get
            {
                return _ListFilters;
            }
            set
            {
                _ListFilters = value;
            }
        }
        public Hashtable ListGraphicFilters
        {
            get
            {
                return _ListGraphicFilters;
            }
            set
            {
                _ListGraphicFilters = value;
            }
        }
        public Hashtable ListModeVisualisation
        {
            get
            {
                return _ListModeVisualisation;
            }
            set
            {
                _ListModeVisualisation = value;
            }
        }

        internal String BagTraceLocation
        {
            get
            {
                return sBagTraceLocation;
            }
            set
            {
                sBagTraceLocation = value;
                if (value != null)
                    bUseTraceSaving = true;
            }
        }
        internal String PaxTraceLocation
        {
            get
            {
                return sPaxTraceLocation;
            }
            set
            {
                sPaxTraceLocation = value;
                if (value != null)
                    bUseTraceSaving = true;
            }
        }

        public List<String> ListUserData
        {
            get
            {
                return _ListUserData;
            }
            set
            {
                _ListUserData = value;
            }
        }
        public XmlNode UserDataStructure
        {
            get
            {
                if (_xdProjectUserData == null)
                    _xdProjectUserData = new XmlDocument();
                if (_UserStructure == null)
                    _UserStructure = _xdProjectUserData.CreateElement("UserData");
                return _UserStructure;
            }
        }
        public ArrayList ListAutomodGraphic
        {
            get
            {
                return _ListAutomodGraphic;
            }
        }

        internal Dictionary<String, String> dssNotes;



        #endregion

        #region Le constructeur de cette classe.
        /// <summary>
        /// Constructeur de la classe GestionDonnees
        /// </summary>
        /// <param name="Nom_">Le nom de ce système de données</param>
        /// <param name="ListeErreurs_">Le lien vers la classe statique des erreurs.</param>
        internal GestionDonnees(String Nom_, AnalyseTraces atAnalyseTraces_, ArrayList ListeErreurs_)
        {
            ListeErreurs = ListeErreurs_;
            Nom = Nom_;
            atAnalyseTraces = atAnalyseTraces_;
            _StockageTables = new DataSet(Nom);
            ListFilters = new Hashtable();
            ListGraphicFilters = new Hashtable();
            ListModeVisualisation = new Hashtable();
            _ListUserData = new List<String>();
            _ListAutomodGraphic = new ArrayList();
            _UserStructure = null;
            _xdProjectUserData = null;

            _htPartialOpened = new Dictionary<String, String>();
            _lsListTableCalculatedFromTrace = new List<string>();
            bUseTraceSaving = false;
            sBagTraceLocation = "";
            sPaxTraceLocation = "";
        }
        #endregion

        #region Les fonctions pour l'enregistrement et le chargement en XML des informations.

        #region Pour les filtres graphiques
        /// <summary>
        /// Fonction pour générer à partir du système de données la représentation sous forme 
        /// XML des filtres graphiques.
        /// </summary>
        /// <param name="projet">Le document xml est nécessaire pour la génération des noeuds.</param>
        /// <returns></returns>
        public XmlNode generateGraphicFiltersXml(XmlDocument projet)
        {
            XmlElement arbreFiltres = projet.CreateElement("GraphicFilters");
            foreach (GraphicFilter filtre in ListGraphicFilters.Values)
            {
                arbreFiltres.AppendChild(filtre.creerArbreXml(projet));
            }
            return arbreFiltres;
        }
        /// <summary>
        /// Fonction pour le chargement des filtres graphiques.
        /// </summary>
        /// <param name="xmlElement"></param>
        internal void loadGraphicFilters(XmlElement xmlElement)
        {
            GraphicFilter filtre;
            foreach (XmlElement xmlfiltre in xmlElement.ChildNodes)
            {
                filtre = new GraphicFilter(xmlfiltre);
                AddGraphicFilter(filtre);
            }
        }
        #endregion

        #region Pour les filtres
        public bool IsFilter(String sFilterName)
        {
            return _ListFilters.ContainsKey(sFilterName);
        }

        /// <summary>
        /// Fonction pour générer le code XML correspondant à tous les filtres du système de données.
        /// </summary>
        /// <param name="projet">Le document XML source. il est nécessaire pour générer les noeuds XML</param>
        /// <returns></returns>
        public XmlNode generateFiltersXml(XmlDocument projet)
        {
            XmlElement arbreFiltres = projet.CreateElement("Filters");
            foreach (Filter filtre in ListFilters.Values)
            {
                arbreFiltres.AppendChild(filtre.creerArbreXml(projet));
            }
            return arbreFiltres;
        }
        /// <summary>
        /// Fonction pour charger les filtres du système de données.
        /// </summary>
        /// <param name="arbreFiltres"></param>
        /// <returns></returns>
        public bool loadFiltersXML(XmlElement arbreFiltres)
        {
            if (arbreFiltres.Name != "Filters")
                return false;
            foreach (XmlElement node in arbreFiltres.ChildNodes)
            {
                Filter filtre = new Filter(node);
                if (filtre != null)
                    AddFilter(filtre);
            }
            return true;
        }
        /// <summary>
        /// Fonction pour sauvegarder les filtres copiés dans des fichiers.
        /// </summary>
        /// <param name="parent">Le parent XML sur lequel ajouté les noeuds pour les filtres copiés.</param>
        /// <param name="projet">Le document XML source. il est nécessaire pour générer les noeuds XML</param>
        /// <param name="parentTable">La table parente qui a appelé cette sauvegarde.</param>
        /// <param name="SavePath">Chemin relatif de l'endroit où sauvegarder les fichiers</param>
        /// <param name="SaveProject">Chemin absolu du répertoire du projet</param>
        private void saveFiltersCopy(XmlNode parent, XmlDocument projet, String parentTable, String SavePath, String SaveProject, Prompt.SIM_LoadingForm chForm)
        {
            String sPath = "";
            foreach (Filter filtre in ListFilters.Values)
            {
                if ((filtre.MotherTableName == parentTable) || (parentTable == ""))
                {
                    if (filtre.copyTable)
                    {
                        if (chForm != null)
                            chForm.ChargementFichier("Saving project : " + filtre.Name);
                        XmlElement newNoeud = projet.CreateElement("FilterTable");
                        newNoeud.SetAttribute("Name", filtre.Name);
                        sPath = SavePath + filtre.Name + ".txt";
                        newNoeud.AppendChild(projet.CreateTextNode(sPath));
                        parent.AppendChild(newNoeud);
                        if (!isOpenned(filtre.Name))
                        {
                            String sOriginFile = (String)_htPartialOpened[filtre.Name];
                            if (sOriginFile == sPath)
                            {
                                //We have to look for .bak directory.
                                sOriginFile = sOriginFile.Replace("\\Output\\" + Nom + "\\FiltersTable", ".bak2\\Output\\" + Nom + "\\FiltersTable");
                            }
                            if (!System.IO.File.Exists(sOriginFile))
                            {
                                System.IO.StreamWriter swFile = new System.IO.StreamWriter(sPath);
                                swFile.Close();
                            }
                            else
                            {
                                try
                                {
                                    System.IO.File.Copy(sOriginFile, sPath);
                                    _htPartialOpened[filtre.Name] = sPath;
                                }
                                catch (Exception exc)
                                {
                                    ListeErreurs.Add("Err00015 : Unable to copy file \"" + sOriginFile + "\" to destination file \"" + sPath + "\".");
                                    OverallTools.ExternFunctions.PrintLogFile("Err00015: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                                }
                            }
                        }
                        else
                        {
                            //Enregistrement des fichiers en mémoire system
                            OverallTools.FonctionUtiles.EcritureFichier(GetTable(filtre.Name), SaveProject + newNoeud.FirstChild.Value, "\t", true);
                        }
                    }
                    if (parentTable != "")
                    {
                        saveFiltersCopy(parent, projet, filtre.Name, SavePath, SaveProject, chForm);
                    }
                }
            }
        }
        public void saveFiltersCopy(XmlNode parent, XmlDocument projet, String SavePath, String SaveProject, Prompt.SIM_LoadingForm chForm)
        {
            saveFiltersCopy(parent, projet, "", SavePath, SaveProject, chForm);
        }
        #endregion

        #region Pour les graphiques extérieurs.
        public XmlNode GenerateAutomodGraphics(XmlDocument projet)
        {
            if (ListAutomodGraphic.Count == 0)
                return null;
            XmlElement arbreGraphics = projet.CreateElement("AutomodGraphic");
            foreach (String sName in ListAutomodGraphic)
            {
                arbreGraphics.AppendChild(OverallTools.FonctionUtiles.CreateElement(projet, "Graphic", sName));
            }
            return arbreGraphics;
        }
        /// <summary>
        /// Fonction pour charger les filtres du système de données.
        /// </summary>
        /// <param name="arbreFiltres"></param>
        /// <returns></returns>
        public bool LoadAutomodGraphics(XmlElement arbreGraphics)
        {
            if (arbreGraphics.Name != "AutomodGraphic")
                return false;
            foreach (XmlElement node in arbreGraphics.ChildNodes)
            {
                String sName = node.FirstChild.Value;
                ListAutomodGraphic.Add(sName);
            }
            return true;
        }
        #endregion

        #endregion

        #region Les fonctions utiles uniquement pour le User data
        public void SaveUserData(String Directory, XmlNode xnUserData)
        {
            if (ListUserData.Count == 0)
                return;
            OverallTools.ExternFunctions.CheckCreateDirectory(Directory);
            if (xnUserData == null)
            {
                xnUserData = _UserStructure;
            }
            if (xnUserData == null)
                return;
            foreach (XmlNode xnElem in xnUserData.ChildNodes)
            {
                if (xnElem.FirstChild == null)
                    continue;
                SaveUserData(Directory + "\\" + xnElem.FirstChild.Value, xnElem);
            }
            foreach (XmlAttribute xnElem in xnUserData.Attributes)
            {
                String sNomTable = xnElem.Value;

                if (!estPresent(sNomTable))
                    continue;
                DataTable Table = GetTable(sNomTable);
                OverallTools.FonctionUtiles.SaveUserData(Table, Directory);
            }
        }
        public void SaveUserDataForScenario(String Directory, Hashtable htDefaults)
        {
            if (ListUserData.Count == 0)
                return;
            OverallTools.ExternFunctions.CheckCreateDirectory(Directory);
            //On sauvegarde les tables en racine.
            foreach (XmlAttribute xnElem in _UserStructure.Attributes)
            {
                String sNomTable = xnElem.Value;

                if (!estPresent(sNomTable))
                    continue;
                DataTable Table = GetTable(sNomTable);
                OverallTools.FonctionUtiles.SaveUserData(Table, Directory);
            }
            if (htDefaults != null)
            {
                foreach (String Skey in htDefaults.Keys)
                {
                    String sTableName = (String)htDefaults[Skey];
                    if (!estPresent(sTableName))
                        continue;
                    OverallTools.FonctionUtiles.SaveUserData(GetTable(sTableName), Skey, Directory);
                }
            }
            else
            {
                foreach (XmlNode xnElem in _UserStructure.ChildNodes)
                {
                    if (xnElem.Attributes.Count < 1)
                        continue;
                    if (xnElem.Attributes[0].FirstChild == null)
                        continue;
                    if (xnElem.FirstChild == null)
                        continue;
                    String sTableName = xnElem.Attributes[0].FirstChild.Value;
                    if (!estPresent(sTableName))
                        continue;

                    OverallTools.FonctionUtiles.SaveUserData(GetTable(sTableName), xnElem.FirstChild.Value, Directory);
                }
            }
        }
        public bool DeleteUserData(String sNomTable, bool bDirectory)
        {
            return DeleteUserData(sNomTable, _UserStructure, bDirectory);
        }
        public bool DeleteUserData(String sNomTable, XmlNode xnLocation, bool bDirectory)
        {

            if (!bDirectory)
            {
                XmlAttribute xnAttribute = OverallTools.FonctionUtiles.getAttributeWithValue(xnLocation, sNomTable);
                if (xnAttribute != null)
                {
                    if (!_ListUserData.Contains(sNomTable))
                        return false;
                    removeTable(sNomTable);
                    _ListUserData.Remove(sNomTable);
                    xnLocation.Attributes.Remove(xnAttribute);
                    xnAttribute = null;
                    return true;
                }
                bool bResult = false;
                foreach (XmlNode xnNDirectory in xnLocation.ChildNodes)
                {
                    bResult = DeleteUserData(sNomTable, xnNDirectory, false);
                    if (bResult == true)
                        break;
                }
                return bResult;
            }
            else
            {
                XmlNode xnTmp = OverallTools.FonctionUtiles.getChildWithValue(xnLocation, sNomTable);
                if (xnTmp != null)
                {
                    while (xnTmp.Attributes.Count > 0)
                    {
                        DeleteUserData(xnTmp.Attributes[0].Value, xnTmp, false);
                    }
                    xnLocation.RemoveChild(xnTmp);
                    return true;
                }
            }
            return false;
        }
        public int LoadUserData(String Directory, XmlNode xnDirectory)
        {
            if (_xdProjectUserData == null)
                _xdProjectUserData = new XmlDocument();
            if (!System.IO.Directory.Exists(Directory))
                return 0;
            bool bRoot = (xnDirectory == null);
            if (bRoot)
            {
                if (_UserStructure == null)
                    _UserStructure = _xdProjectUserData.CreateElement("UserData");
                String[] Directories = System.IO.Directory.GetDirectories(Directory);
                if (Directories.Length != 0)
                {
                    foreach (String sDirectory in Directories)
                    {
                        String sDirectoryName = sDirectory.Substring(sDirectory.LastIndexOf("\\") + 1);
                        xnDirectory = _xdProjectUserData.CreateElement("UserData");
                        xnDirectory.AppendChild(_xdProjectUserData.CreateTextNode(sDirectoryName));
                        _UserStructure.AppendChild(xnDirectory);
                        LoadUserData(sDirectory, xnDirectory);
                    }
                }
                xnDirectory = _UserStructure;
            }
            String[] Files = System.IO.Directory.GetFiles(Directory);
            if (Files.Length == 0)
                return 0;
            foreach (String File in Files)
            {
                String sNomTable = File;
                if (sNomTable.Contains("\\"))
                    sNomTable = sNomTable.Substring(File.LastIndexOf("\\") + 1);
                if ((sNomTable == "UserData.ud") && bRoot)
                    continue;
                if (estPresent(sNomTable))
                {
                    ListeErreurs.Add("Err00750 : The user data \"" + sNomTable + "\" can't be added to the project. The name is already in use.");
                    continue;
                }
                ((XmlElement)xnDirectory).SetAttribute(sNomTable.Replace(' ', '_'), sNomTable);
                //xnDirectory.Attributes.Append(_xdProjectUserData.CreateAttribute(sNomTable));
                if (bRoot)
                    AddUserData(File, sNomTable, null);
                else
                    AddUserData(File, sNomTable, xnDirectory.FirstChild.Value);
            }
            return 0;
        }
        public bool AddUserData(String file, String sTableName, String sDirectory)
        {
            if (_xdProjectUserData == null)
                _xdProjectUserData = new XmlDocument();
            DataTable tmp = new DataTable(sTableName);
            if (!OverallTools.FonctionUtiles.lectureUserData(tmp, file))
                return false;
            if (!estPresent(sTableName))
            {
                XmlElement xnDirectory = (XmlElement)_UserStructure;
                if (sDirectory != null)
                {
                    if (sDirectory == "User Data")
                    {
                        sDirectory = sTableName;
                    }

                    xnDirectory = (XmlElement)OverallTools.FonctionUtiles.getChildWithValue(_UserStructure, sDirectory);
                    if (xnDirectory == null)
                    {

                        xnDirectory = _xdProjectUserData.CreateElement("UserData");

                        xnDirectory.AppendChild(_xdProjectUserData.CreateTextNode(sDirectory));
                        //xnDirectory.Value = sDirectory;
                        _UserStructure.AppendChild(xnDirectory);
                    }
                }
                if (!AddTable(tmp, new VisualisationMode(true, false, true, null, null)))
                    return false;
                if (OverallTools.FonctionUtiles.getAttributeWithValue(xnDirectory, sTableName) == null)
                {
                    xnDirectory.SetAttribute(sTableName.Replace(' ', '_'), sTableName);
                }
                _ListUserData.Add(sTableName);
            }
            else
            {
                if (!_ListUserData.Contains(tmp.TableName))
                    return false;
                AddReplaceTable(tmp);
            }
            return true;
        }
        #endregion

        #region Fonctions utiles pour les graphiques importés d'Automod
        public bool AddAutomodGraphic(DataTable dtTable)
        {
            if ((estPresent(dtTable.TableName)) && (!ListAutomodGraphic.Contains(dtTable.TableName)))
                return false;
            AddReplaceTable(dtTable);
            if (!ListAutomodGraphic.Contains(dtTable.TableName))
            {
                ListAutomodGraphic.Add(dtTable.TableName);
                AddReplaceModeVisualisation(dtTable.TableName, new VisualisationMode(false, false, false, null, null));
            }
            else
            {
                ListGraphicFilters.Remove(dtTable.TableName);
            }
            AddGraphicFilter(GraphicFilter.getGraphicFilter(dtTable));
            return true;
        }
        public void DeleteAutomodGraphic(String sTable)
        {
            if (!ListAutomodGraphic.Contains(sTable))
                return;
            removeTable(sTable);
            ListAutomodGraphic.Remove(sTable);
        }
        #endregion

        #region La liste des Getteurs pour les différentes informations.

        #region Les fonctions pour les tables.

        public List<String> TablesNames
        {
            get
            {
                List<String> lsResult = new List<string>();
                IEnumerator it = _StockageTables.Tables.GetEnumerator();
                it.Reset();
                while (it.MoveNext())
                {
                    lsResult.Add(((DataTable)it.Current).TableName);
                }
                lsResult.AddRange(_htPartialOpened.Keys);
                return lsResult;
            }
        }
        /// <summary>
        /// Fonction pour déterminer si une table est présente dans le système de données.
        /// </summary>
        /// <param name="Name">Nom de la table recherchée.</param>
        /// <returns>Booléen indiquant la présence ou non de la table.</returns>
        public bool estPresent(String Name)
        {
            if (!_StockageTables.Tables.Contains(Name))
            {
                if (_htPartialOpened.ContainsKey(Name))
                {
                    return true;
                }
                if (ListFilters.ContainsKey(Name))
                {
                    String sMother = GestionDonneesHUB2SIM.motherTable(Name, ListFilters);
                    if (estPresent(sMother))
                    {
                        aEteModifiee(sMother);
                        return true;
                    }

                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// Fonction qui renvoie un lien vers la table spécifiée.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public DataTable GetTable(String Name)
        {
            if (!estPresent(Name))
            {
                ListeErreurs.Add("Err00083 : The table \"" + Name + "\"does not exist in the dataset.");
                return null;
            }
            if (!isOpenned(Name))
                loadPartialOpened(Name);
            return _StockageTables.Tables[Name];
        }
        /// <summary>
        /// Fonction pour ajouter une table au système de données.
        /// </summary>
        /// <param name="laTable">La nouvelle table a ajoutée</param>
        /// <param name="Mode">Le mode de visualisation de cette table (ne doit pas être null)</param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé ou non.</returns>
        public bool AddTable(DataTable laTable, VisualisationMode Mode)
        {
            return AddTable(laTable, Mode, false);
        }
        public bool AddTable(DataTable laTable, VisualisationMode Mode, bool CalculatedFromTrace)
        {
            if ((laTable == null) || estPresent(laTable.TableName) || (Mode == null))
            {
                return false;
            }
            if (CalculatedFromTrace)
                AddCalculatedFromTrace(laTable.TableName);
            _StockageTables.Tables.Add(laTable);
            AddReplaceModeVisualisation(laTable.TableName, Mode);
            return true;
        }
        /// <summary>
        /// Fonction pour supprimer du système de données une table et tous ses filtres enfants.
        /// </summary>
        /// <param name="Name">Le nom de la table à supprimer.</param>
        public void removeTable(String Name)
        {
            if (estPresent(Name))
            {
                if (_StockageTables.Tables.Contains(Name))
                {
                    _StockageTables.Tables.Remove(Name);
                }
                else
                {
                    _htPartialOpened.Remove(Name);
                }
            }
            if (IsCalculatedFromTrace(Name))
                RemoveCalculatedFromTrace(Name);

            if (ListModeVisualisation.ContainsKey(Name))
                ListModeVisualisation.Remove(Name);

            if (ListGraphicFilters.ContainsKey(Name))
                ListGraphicFilters.Remove(Name);

            if (ListFilters.ContainsKey(Name))
                ListFilters.Remove(Name);

            ArrayList listASupprime = new ArrayList();
            foreach (Filter filtre in ListFilters.Values)
            {
                if (filtre.MotherTableName == Name)
                {
                    listASupprime.Add(filtre.Name);
                }
            }
            foreach (String FiltreName in listASupprime)
            {
                removeTable(FiltreName);
            }
        }

        /// <summary>
        /// Fonction pour remplacer une table par une nouvelle.
        /// Si la table n'était pas présente, alors aucun changement n'est effectué.
        /// </summary>
        /// <param name="laTable">La table à ajouter.</param>
        /// <returns></returns>
        public bool ReplaceTable(DataTable laTable)
        {
            return ReplaceTable(laTable, false);
        }

        public bool ReplaceTable(DataTable laTable, bool CalculatedFromTrace)
        {
            if ((laTable == null) || (!estPresent(laTable.TableName)))
            {
                return false;
            }
            if (_StockageTables.Tables.Contains(laTable.TableName))
            {
                _StockageTables.Tables.Remove(laTable.TableName);
            }
            else
            {
                _htPartialOpened.Remove(laTable.TableName);
            }
            _StockageTables.Tables.Add(laTable);
            bool bAlreadyPresentInCalculated = IsCalculatedFromTrace(laTable.TableName);
            if (IsCalculatedFromTrace(laTable.TableName) && !CalculatedFromTrace)
                RemoveCalculatedFromTrace(laTable.TableName);
            else if ((!bAlreadyPresentInCalculated) && CalculatedFromTrace)
                AddCalculatedFromTrace(laTable.TableName);
            return true;
        }
        /// <summary>
        /// Fonction qui se charge d'ajouter ou de remplacer une table présente.
        /// </summary>
        /// <param name="laTable"></param>
        public void AddReplaceTable(DataTable laTable)
        {
            AddReplaceTable(laTable, false);
        }

        internal void AddReplaceTable(DataTable laTable, bool CalculatedFromTrace)
        {
            if (laTable == null)
                return;
            bool bAlreadyPresentInCalculated = IsCalculatedFromTrace(laTable.TableName);
            if (estPresent(laTable.TableName))
            {
                if (_StockageTables.Tables.Contains(laTable.TableName))
                {
                    _StockageTables.Tables.Remove(laTable.TableName);
                }
                else
                {
                    _htPartialOpened.Remove(laTable.TableName);
                }
                _StockageTables.Tables.Add(laTable);
                if (bAlreadyPresentInCalculated && !CalculatedFromTrace)
                    RemoveCalculatedFromTrace(laTable.TableName);
            }
            else
            {
                if (ListModeVisualisation.Contains(laTable.TableName))
                {
                    _StockageTables.Tables.Add(laTable);
                }
                else
                {
                    AddTable(laTable, new VisualisationMode(true, true, true, null, null));
                }
            }
            if ((!bAlreadyPresentInCalculated) && CalculatedFromTrace)
                AddCalculatedFromTrace(laTable.TableName);
        }

        public bool SaveTable(String sTableName, String sPath)
        {
            if (!estPresent(sTableName))
                return false;

            if (IsCalculatedFromTrace(sTableName))
                return true;
            if (_htPartialOpened.ContainsKey(sTableName))
            {
                String sOriginFile = (String)_htPartialOpened[sTableName];
                if (sOriginFile == sPath)
                {
                    //We have to look for .bak directory.
                    sOriginFile = sOriginFile.Replace("\\Output\\" + Nom + "\\", ".bak2\\Output\\" + Nom + "\\");
                }
                if (!System.IO.File.Exists(sOriginFile))
                {
                    System.IO.StreamWriter swFile = new System.IO.StreamWriter(sPath);
                    swFile.Close();
                }
                else
                {
                    try
                    {
                        System.IO.File.Copy(sOriginFile, sPath);
                        _htPartialOpened[sTableName] = sPath;
                    }
                    catch (Exception exc)
                    {
                        ListeErreurs.Add("Err00015 : Unable to copy file \"" + sOriginFile + "\" to destination file \"" + sPath + "\".");
                        OverallTools.ExternFunctions.PrintLogFile("Err00015: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                    }
                }
            }
            else
            {
                OverallTools.FonctionUtiles.EcritureFichier(GetTable(sTableName), sPath, "\t", true);
            }
            return true;
        }


        internal string getNote(string nomTable)
        {
            if (dssNotes == null)
                return null;
            if (nomTable == null)
                return null;
            if (!dssNotes.ContainsKey(nomTable))
                return null;
            return dssNotes[nomTable];
        }

        internal void setNote(string nomTable, string sNote)
        {
            if (nomTable == null)
                return;

            if (dssNotes == null)
                dssNotes = new Dictionary<string, string>();
            if ((sNote == null) || (sNote == ""))
            {
                if (dssNotes.ContainsKey(nomTable))
                    dssNotes.Remove(nomTable);
                return;
            }
            if (dssNotes.ContainsKey(nomTable))
                dssNotes[nomTable] = sNote;
            else
                dssNotes.Add(nomTable, sNote);
        }

        internal XmlNode saveNotes(XmlDocument xdProjet)
        {
            if (dssNotes == null)
                return null;
            if (dssNotes.Count == 0)
                return null;
            XmlNode xnNotes = xdProjet.CreateElement("Notes");
            foreach (String sKey in dssNotes.Keys)
            {
                XmlElement xnNote = xdProjet.CreateElement("Notes");
                xnNote.SetAttribute("TableName", sKey);
                xnNote.AppendChild(xdProjet.CreateCDataSection(dssNotes[sKey]));
                xnNotes.AppendChild(xnNote);
            }
            return xnNotes;
        }
        internal void openNotes(XmlNode xnNodes)
        {
            if (dssNotes != null)
                dssNotes.Clear();
            dssNotes = new Dictionary<string, string>();
            foreach (XmlNode xnTmp in xnNodes.ChildNodes)
            {
                if (!OverallTools.FonctionUtiles.hasNamedAttribute(xnTmp, "TableName"))
                    continue;
                String sKey = xnTmp.Attributes["TableName"].Value;
                string sValue = xnTmp.FirstChild.Value;
                setNote(sKey, sValue);
            }
        }
        #endregion

        #region Pour les filtres
        /// <summary>
        /// Fonction pour retourner le filtre désigné.
        /// </summary>
        /// <param name="Name">Le nom du filtre recherché.</param>
        /// <returns></returns>
        internal Filter getFilter(String Name)
        {
            if (!ListFilters.ContainsKey(Name))
            {
                return null;
            }
            return (Filter)ListFilters[Name];
        }
        /// <summary>
        /// Fonction pour ajouter un filtre au système de données.
        /// </summary>
        /// <param name="filtre">Le nouveau filtre.</param>
        private void AddFilter(Filter filtre)
        {
            if (ListFilters.Contains(filtre.Name))
                return;
            ListFilters.Add(filtre.Name, filtre);
        }
        /// <summary>
        /// Fonction qui s'occupe de calculé le nouveau filtre.
        /// </summary>
        /// <param name="filtre">Le nouveau filtre.</param>
        /// <returns></returns>
        internal DataTable CreateReplaceFilter(Filter filtre)
        {
            DataTable NouvelleTable = filtre.applyFilter(GetTable(filtre.MotherTableName), getFilter(filtre.MotherTableName));
            if (NouvelleTable == null)
            {
                return null;
            }
            NouvelleTable.TableName = filtre.Name;
            if (ListFilters.Contains(filtre.Name))
            {
                ListFilters.Remove(filtre.Name);
            }
            AddFilter(filtre);
            VisualisationMode mode = getModeVisualisation(filtre.MotherTableName);
            AddReplaceTable(NouvelleTable);
            if (mode == null)
                return NouvelleTable;
            mode = mode.Clone();
            if (filtre.Blocked)
            {
                mode.Modifiable = false;
                mode.AllowAddLine = false;
            }
            if (!filtre.copyTable)
                mode.AllowAddLine = false;
            AddReplaceModeVisualisation(filtre.Name, mode);
            return NouvelleTable;
        }
        /// <summary>
        /// This function return the origin table name for the root parent
        /// </summary>
        /// <param name="sFilterName">The name of the filter</param>
        /// <returns>Return null if the name is not a valid name for a filter. Or the root parent table name.</returns>
        public String getFilterRootName(String sFilterName)
        {
            if (!_ListFilters.ContainsKey(sFilterName))
                return null;
            String sMotherTable = ((Filter)_ListFilters[sFilterName]).MotherTableName;
            if (!_ListFilters.ContainsKey(sMotherTable))
                return sMotherTable;
            return getFilterRootName(sMotherTable);
        }


        internal List<String> getTableFilters(String sMainTableName)
        {
            List<String> lsResult = new List<string>();
            lsResult.Add(sMainTableName);
            foreach (Filter fFilter in _ListFilters.Values)
            {
                if (!fFilter.copyTable)
                    continue;
                if (GestionDonneesHUB2SIM.motherTable(fFilter.Name, _ListFilters) == sMainTableName)
                {
                    lsResult.Add(fFilter.Name);
                }
            }
            return lsResult;
        }

        #endregion

        #region Pour les filtres graphiques
        /// <summary>
        /// Fonction pour récupérer le filtre pour l'affichage graphique de la table spécifiée.
        /// </summary>
        /// <param name="Name">Nom de la table.</param>
        /// <returns></returns>
        internal GraphicFilter getGraphicFilter(String Name)
        {
            if (!ListGraphicFilters.ContainsKey(Name))
            {
                return null;
            }
            return (GraphicFilter)ListGraphicFilters[Name];
        }

        /// <summary>
        /// Fonction pour ajouter un filtre graphique au système de données.
        /// </summary>
        /// <param name="filtre">Le nouveau filtre graphique à ajouter.</param>
        internal bool AddGraphicFilter(GraphicFilter filtre)
        {
            if (filtre == null)
                return false;
            if (ListGraphicFilters.Contains(filtre.Name))
                return false;
            ListGraphicFilters.Add(filtre.Name, filtre);
            return true;
        }

        public void removeGraphicFilter(String Name)
        {
            if (ListGraphicFilters.ContainsKey(Name))
            {
                ListGraphicFilters.Remove(Name);
            }
        }
        #endregion

        #region Pour les modes de visualisation
        /// <summary>
        /// Fonction pour récupérer le mode de visualisation de la table désignée.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public VisualisationMode getModeVisualisation(String Name)
        {
            if (!ListModeVisualisation.ContainsKey(Name))
            {
                return null;
            }
            return (VisualisationMode)ListModeVisualisation[Name];
        }
        /// <summary>
        /// Fonction pour ajouter ou remplacer le mode de visualisation de la table désignée.
        /// </summary>
        /// <param name="nomTable"></param>
        /// <param name="mode"></param>
        public void AddReplaceModeVisualisation(String nomTable, VisualisationMode mode)
        {
            if (ListModeVisualisation.ContainsKey(nomTable))
            {
                ListModeVisualisation.Remove(nomTable);
            }
            ListModeVisualisation.Add(nomTable, mode);
        }
        #endregion

        #endregion

        #region Les fonctions pour répercuter les changements sur les tables

        #region Fonction qui met à jour toutes les tables générée à partir de la table passée en paramètre.
        private void UpdateChilds(String CurrentTable)
        {
            UpdateChilds(CurrentTable, false);
        }
        /// <summary>
        /// Fonction qui se charge de répercuter les modifications d'une table sur toutes les tables concernées par
        /// ces changements
        /// </summary>
        /// <param name="CurrentTable">La table qui vient d'être modifiée</param>
        private void UpdateChilds(String CurrentTable, bool bChargement) //The modified table
        {
            VisualisationMode mode = getModeVisualisation(CurrentTable);
            // La table passée en paramètres a été modifiée, donc les données enregistrées ne sont plus valides.
            foreach (String NameFilter in ListFilters.Keys)
            {
                Filter filtre = (Filter)ListFilters[NameFilter];
                if (filtre.MotherTableName == CurrentTable)
                {
                    DataTable dtCurrenttable = GetTable(CurrentTable);
                    if (bChargement)
                    {
                        Filter FilterParent = getFilter(CurrentTable);
                        filtre.isBlocked(dtCurrenttable, FilterParent, true);
                    }
                    if (!((Filter)ListFilters[NameFilter]).copyTable)
                    {
                        DataTable result = ((Filter)ListFilters[NameFilter]).applyFilter(dtCurrenttable, getFilter(CurrentTable));
                        DataTable table = GetTable(CurrentTable);
                        if (result != null)
                        {
                            if (_StockageTables.Tables.Contains(NameFilter))
                            {
                                _StockageTables.Tables.Remove(NameFilter);
                            }
                            _StockageTables.Tables.Add(result);
                            if (_htPartialOpened.ContainsKey(NameFilter))
                                _htPartialOpened.Remove(NameFilter);
                            if (!this._ListModeVisualisation.Contains(NameFilter))
                            {
                                if (mode != null)
                                {
                                    mode = mode.Clone();
                                    if (((Filter)ListFilters[NameFilter]).Blocked)
                                    {
                                        mode.Modifiable = false;
                                        mode.AllowAddLine = false;
                                    }
                                    if (!((Filter)ListFilters[NameFilter]).copyTable)
                                        mode.AllowAddLine = false;
                                }
                                mode.ConditionnalFormatClass = null;
                                AddReplaceModeVisualisation(NameFilter, mode);
                            }
                            else
                            {
                                if (mode != null)
                                {
                                    mode = mode.Clone();
                                    if (((Filter)ListFilters[NameFilter]).Blocked)
                                    {
                                        mode.Modifiable = false;
                                        mode.AllowAddLine = false;
                                    }
                                    if (!((Filter)ListFilters[NameFilter]).copyTable)
                                        mode.AllowAddLine = false;
                                }
                                mode.ConditionnalFormatClass = null;
                                AddReplaceModeVisualisation(NameFilter, mode);
                            }
                            UpdateChilds(NameFilter);
                        }
                    }
                    else
                    {
                        if (mode != null)
                        {
                            mode = mode.Clone();
                            if (((Filter)ListFilters[NameFilter]).Blocked)
                            {
                                mode.Modifiable = false;
                                mode.AllowAddLine = false;
                            }
                            if (!((Filter)ListFilters[NameFilter]).copyTable)
                                mode.AllowAddLine = false;
                            mode.ConditionnalFormatClass = null;
                            AddReplaceModeVisualisation(NameFilter, mode);
                        }
                    }
                }
            }
        }
        #endregion

        #region Fonction qui modifie la table parente (et les filtres parents) du filtre qui a été modifié.
        public String ModifyToOriginTable(String CurrentTable)
        {
            //La table courante est djà la table originale.
            if (!ListFilters.Contains(CurrentTable))
                return CurrentTable;

            //S'il s'agit d'un filtre qui a copie les données dans une nouvelle table, alors on 
            //est arrivé à la racine des changements.
            Filter filtre = (Filter)ListFilters[CurrentTable];
            if (filtre.copyTable)
                return CurrentTable;
            if (filtre.Blocked)
                return CurrentTable;
            int i;
            String MotherTableName = ((Filter)ListFilters[CurrentTable]).MotherTableName;
            //Ici il faut répercuter les changements sur les tables parentes.
            DataTable dtMotherTable = GetTable(MotherTableName);
            DataTable dtCurrentTable = GetTable(CurrentTable);
            if ((dtMotherTable == null) || (dtCurrentTable == null))
                return null;

            int[] iIndexPrimaryKey = new int[dtCurrentTable.PrimaryKey.Length];
            for (i = 0; i < dtCurrentTable.PrimaryKey.Length; i++)
            {
                iIndexPrimaryKey[i] = dtMotherTable.Columns.IndexOf(dtCurrentTable.PrimaryKey[i].ColumnName);
                if (iIndexPrimaryKey[i] == -1)
                    return null;
            }
            int[] iIndexColumns = new int[dtCurrentTable.Columns.Count];
            for (i = 0; i < dtCurrentTable.Columns.Count; i++)
            {
                iIndexColumns[i] = dtMotherTable.Columns.IndexOf(dtCurrentTable.Columns[i].ColumnName);
                if (iIndexColumns[i] == -1)
                    return null;
            }
            DataRow lineToModify;

            foreach (DataRow ligne in dtCurrentTable.Rows)
            {
                Object[] key = new Object[dtCurrentTable.PrimaryKey.Length];
                for (i = 0; i < dtCurrentTable.PrimaryKey.Length; i++)
                {
                    key[i] = ligne[dtCurrentTable.PrimaryKey[i]];
                }
                lineToModify = null;
                foreach (DataRow ligneMother in dtMotherTable.Rows)
                {
                    int nbFind = 0;
                    for (i = 0; i < dtCurrentTable.PrimaryKey.Length; i++)
                    {
                        if (ligneMother[iIndexPrimaryKey[i]].ToString() == key[i].ToString())
                        {
                            nbFind++;
                        }
                    }
                    if (nbFind == dtCurrentTable.PrimaryKey.Length)
                    {
                        lineToModify = ligneMother;
                        break;
                    }
                }
                if (lineToModify == null)
                    continue;
                lineToModify.BeginEdit();
                for (i = 0; i < dtCurrentTable.Columns.Count; i++)
                {
                    lineToModify[iIndexColumns[i]] = ligne[i];
                }
            }
            dtMotherTable.AcceptChanges();
            //On indique que la table parente a été modifiée.
            return ModifyToOriginTable(MotherTableName);
        }
        #endregion

        #region Fonction qui est appelée lorsqu'une table est modifiée dans le système de données courant.
        public void aEteModifiee(String tableCourante)
        {
            aEteModifiee(tableCourante, false);
        }
        public void aEteModifiee(String tableCourante, bool bChargement)
        {
            String parentTable = ModifyToOriginTable(tableCourante);
            if (parentTable == null)
            {
                ListeErreurs.Add("Err00123 : Some problems appear during the updating of the table from it filters");
                return;
            }
            UpdateChilds(parentTable, bChargement);
        }
        public void HasBeenUpdated(String tableCourante, bool bChargement)
        {
            UpdateChilds(tableCourante, bChargement);
        }
        #endregion

        #region Fonction pour remettre à jour tous les filtres du système de données.
        public void RecalcFilters()
        {
            String[] tableauNoms = new String[_StockageTables.Tables.Count];
            for (int i = 0; i < _StockageTables.Tables.Count; i++)
            {
                tableauNoms[i] = _StockageTables.Tables[i].TableName;
            }
            for (int i = 0; i < tableauNoms.Length; i++)
            {
                Filter filtre = getFilter(tableauNoms[i]);
                if ((filtre == null) || (filtre.copyTable))
                {
                    aEteModifiee(tableauNoms[i]);
                }
            }
        }
        #endregion

        public void UpdateFilters(TreeNode node, ContextMenuStrip cmsMenuFilter)
        {
            foreach (DataTable dtTable in _StockageTables.Tables)
            {
                String sName = dtTable.TableName;
                if (getFilter(sName) == null)
                {
                    TreeNode noeudTmp = OverallTools.TreeViewFunctions.RechercherNom(sName, node);
                    if (noeudTmp != null)
                    {
                        Maj_Filtres(sName, noeudTmp, cmsMenuFilter);
                    }
                }
            }
        }

        #endregion

        #region Fonction pour générer toute l'arborescence pour les filtres.
        public void Maj_Filtres(String NomTable, TreeNode parent, ContextMenuStrip cmsMenuFilter)
        {
            foreach (Filter filtre in ListFilters.Values)
            {
                if (filtre.MotherTableName == NomTable)
                {
                    TreeNode noeudFiltre = OverallTools.TreeViewFunctions.RechercherNomEnfantStrict(filtre.Name, parent);
                    if (noeudFiltre == null)
                    {
                        TreeViewTag Tag = TreeViewTag.getFilterNode(((TreeViewTag)parent.Tag).ScenarioName, filtre.Name, filtre.Blocked, filtre.copyTable);

                        noeudFiltre = OverallTools.TreeViewFunctions.createBranch(filtre.Name, filtre.Name, Tag, cmsMenuFilter);
                        if (noeudFiltre == null)
                            continue;
                        OverallTools.TreeViewFunctions.AddSortedNode(parent, noeudFiltre, true);
                    }
                    Maj_Filtres(filtre.Name, noeudFiltre, cmsMenuFilter);
                }
            }
        }
        #endregion

        #region Fonction qui désalloue la mémoire pour cette classe.
        public void Dispose()
        {
            _StockageTables.Dispose();
            _StockageTables = null;
            _ListFilters.Clear();
            _ListFilters = null;
            _ListGraphicFilters.Clear();
            _ListGraphicFilters = null;
            _ListModeVisualisation.Clear();
            _ListModeVisualisation = null;
            _lsListTableCalculatedFromTrace.Clear();
            _lsListTableCalculatedFromTrace = null;

            //If the paxtrace was located in a tempory directory
            /*if (PaxTraceLocation.StartsWith(OverallTools.ExternFunctions.getUserDirectoryForPax2sim()))
                OverallTools.ExternFunctions.DeleteFile(PaxTraceLocation);
            if (BagTraceLocation.StartsWith(OverallTools.ExternFunctions.getUserDirectoryForPax2sim()))
                OverallTools.ExternFunctions.DeleteFile(BagTraceLocation);*/
        }
        #endregion

        #region Gestion de l'ouverture partielle du projet.
        public bool isOpenned(String sTableName)
        {
            if (_StockageTables.Tables.Contains(sTableName))
                return true;
            if (_htPartialOpened.ContainsKey(sTableName))
            {
                return false;
            }
            return true;
        }
        internal String PathPartialOpened(String sTableName)
        {
            if (_htPartialOpened.ContainsKey(sTableName))
                return _htPartialOpened[sTableName];
            return null;
        }
        internal delegate void AnalyseTraces(String sScenarioName, String sPaxTraceLocation, String sBagTraceLocation);
        AnalyseTraces atAnalyseTraces;

        public void loadPartialOpened(String sTableName)
        {
            if (isOpenned(sTableName))
                return;
            if (IsCalculatedFromTrace(sTableName))
            {
                if (atAnalyseTraces == null)
                    return;
                //We have to recalculate from the paxtrace.
                atAnalyseTraces(this.Nom, PaxTraceLocation, BagTraceLocation);
                return;
            }
            String sFilePath = (String)_htPartialOpened[sTableName];
            _htPartialOpened.Remove(sTableName);
            DataTable tmp = null;
            //Il faut charger la table et l'intégrer dans la base de données.
            if (!System.IO.File.Exists(sFilePath))
            {
                return;
            }
            tmp = new DataTable(sTableName);
            OverallTools.FonctionUtiles.LectureFichier(tmp, sFilePath, "\t", ListeErreurs);
            if (tmp == null)
                return;
            AddTable(tmp, new VisualisationMode(false, false, true, null, new int[1] { 0 }));
            if (sTableName.Contains("Utilization"))
            {
                if (sTableName.Contains("Group"))
                {
                    AddReplaceModeVisualisation(sTableName, GestionDonneesHUB2SIM.UtilizationGroupTableVisualisationMode);
                }
                else
                {
                    AddReplaceModeVisualisation(sTableName, GestionDonneesHUB2SIM.UtilizationTableVisualisationMode);
                }
            }
            else
            {
                if ((sTableName == "FPD_Peak_Stats") || (sTableName == "FPA_Peak_Stats") || (sTableName == "FPDFPA_Peak_Stats"))
                {
                    AddReplaceModeVisualisation(sTableName, GestionDonneesHUB2SIM.StaticRapportVisualisationMode);
                }
                else if (sTableName.EndsWith("_Statistics"))
                {
                    AddReplaceModeVisualisation(sTableName, GestionDonneesHUB2SIM.StaticRapportVisualisationMode);
                }
                else
                {
                    if (GestionDonneesHUB2SIM.modeVisualisation.ContainsKey(sTableName))
                        AddReplaceModeVisualisation(sTableName, (VisualisationMode)GestionDonneesHUB2SIM.modeVisualisation[sTableName]);
                }
            }
            aEteModifiee(sTableName);
        }
        public bool AddPartialTable(String sTableName)
        {
            if (_htPartialOpened.ContainsKey(sTableName))
                return false;
            if (_StockageTables.Tables.Contains(sTableName))
                return false;
            _htPartialOpened.Add(sTableName, "");
            AddCalculatedFromTrace(sTableName);
            return true;
        }
        public bool AddPartialTable(String sTableName, String sLocation)
        {
            if (_htPartialOpened.ContainsKey(sTableName))
                return false;
            if (_StockageTables.Tables.Contains(sTableName))
                return false;
            _htPartialOpened.Add(sTableName, sLocation);
            return true;
        }
        #endregion

        #region Gestion de l'ouverture des fichiers qui doivent être recalculés à partir de la trace.
        internal bool IsCalculatedFromTrace(String TableName)
        {
            if (_lsListTableCalculatedFromTrace == null)
                return false;
            if (!bUseTraceSaving)
                return false;
            return _lsListTableCalculatedFromTrace.Contains(TableName);
        }
        internal void RemoveCalculatedFromTrace(String TableName)
        {
            if (_lsListTableCalculatedFromTrace == null)
                return;
            _lsListTableCalculatedFromTrace.Remove(TableName);
        }

        internal void AddCalculatedFromTrace(String TableName)
        {
            if (_lsListTableCalculatedFromTrace == null)
                _lsListTableCalculatedFromTrace = new List<string>();
            if (IsCalculatedFromTrace(TableName))
                return;
            _lsListTableCalculatedFromTrace.Add(TableName);
        }


        #endregion

        #region Pour l'outil d'allocation.
        class AllocatedTable
        {
            #region Les Objets
            private String sTableName;
            public String TableName
            {
                get
                {
                    return sTableName;
                }
                set
                {
                    sTableName = value;
                }
            }
            private String sAllocatedObject;
            public String AllocatedObject
            {
                get
                {
                    return sAllocatedObject;
                }
                set
                {
                    sAllocatedObject = value;
                }
            }
            #endregion

            public AllocatedTable(String sTableName_, String sAllocatedObject_)
            {
                sTableName = sTableName_;
                sAllocatedObject = sAllocatedObject_;
            }
            public AllocatedTable(XmlNode xnChild)
            {
                sTableName = "";
                sAllocatedObject = "";
                if ((xnChild.Name == "Table") && (xnChild.HasChildNodes))
                {
                    sTableName = xnChild.FirstChild.Value;
                }
                else
                {
                    sTableName = xnChild.Name;
                }
                if (OverallTools.FonctionUtiles.hasNamedAttribute(xnChild, "AllocatedObject"))
                {
                    sAllocatedObject = xnChild.Attributes["AllocatedObject"].Value;
                }
            }
            public XmlNode ToXml(XmlDocument xdDocument)
            {
                if ((sAllocatedObject == null) || (sAllocatedObject.Length == 0))
                    return null;
                XmlElement xeTable = xdDocument.CreateElement("Table");
                xeTable.AppendChild(xdDocument.CreateTextNode(sTableName));
                xeTable.SetAttribute("AllocatedObject", sAllocatedObject);
                return xeTable;
            }
        }
        ArrayList alAllocationTables;
        public bool isAllocatedTable(String sNomTable)
        {
            if (alAllocationTables == null)
                return false;
            foreach (AllocatedTable atTable in alAllocationTables)
            {
                if (atTable.TableName == sNomTable)
                    return true;
            }
            return false;
        }
        public void deleteAllocatedTable(String sNomTable)
        {
            if (alAllocationTables == null)
                return;
            for (int i = 0; i < alAllocationTables.Count; i++)
            {
                if (((AllocatedTable)alAllocationTables[i]).TableName == sNomTable)
                {
                    alAllocationTables.RemoveAt(i);
                }
            }
        }

        public void UpdateTreeMakeUpAllocatedTables(TreeNode tnAllocateDirectory, ContextMenuStrip contextMenuInput)
        {
            UpdateTreeAllocateTables(tnAllocateDirectory, contextMenuInput, GlobalNames.sBHS_MakeUpObject);
        }
        public void UpdateTreeReclaimAllocatedTables(TreeNode tnAllocateDirectory, ContextMenuStrip contextMenuInput)
        {
            UpdateTreeAllocateTables(tnAllocateDirectory, contextMenuInput, GlobalNames.sFPA_Column_ReclaimObject);
        }
        public void UpdateTreeTransferInfeedAllocatedTables(TreeNode tnAllocateDirectory, ContextMenuStrip contextMenuInput)
        {
            UpdateTreeAllocateTables(tnAllocateDirectory, contextMenuInput, "Transfer Infeed");
        }
        public bool HasReclaimAllocation()
        {
            if (alAllocationTables == null)
                return false;
            foreach (AllocatedTable atTable in alAllocationTables)
            {
                if (atTable.AllocatedObject == GlobalNames.sFPA_Column_ReclaimObject)
                    return true;
            }
            return false;
        }
        private void UpdateTreeAllocateTables(TreeNode tnAllocateDirectory, ContextMenuStrip contextMenuInput, String Object)
        {
            if (alAllocationTables == null)
                return;
            foreach (AllocatedTable atTable in alAllocationTables)
            {
                if (atTable.AllocatedObject != Object)
                    continue;
                OverallTools.TreeViewFunctions.AddSortedNode(tnAllocateDirectory,
                    OverallTools.TreeViewFunctions.createBranch(atTable.TableName,
                        atTable.TableName,
                        TreeViewTag.getTableNode("Input", atTable.TableName),
                        contextMenuInput));
            }
        }
        public XmlNode ExportAllocateTables(XmlDocument xdDocument)
        {
            if ((alAllocationTables == null) || (alAllocationTables.Count == 0))
                return null;
            XmlNode xnResult = xdDocument.CreateElement("AllocateTables");

            foreach (AllocatedTable sAllocated in alAllocationTables)
            {
                XmlNode xeTable = sAllocated.ToXml(xdDocument);
                if (xeTable != null)
                    xnResult.AppendChild(xeTable);
            }
            return xnResult;
        }
        public void LoadAllocateTables(XmlNode xnResult)
        {
            if (!OverallTools.FonctionUtiles.hasNamedChild(xnResult, "AllocateTables"))
                return;
            alAllocationTables = new ArrayList();
            foreach (XmlNode xnChild in xnResult["AllocateTables"].ChildNodes)
            {
                AllocatedTable atChild = new AllocatedTable(xnChild);
                if ((atChild.AllocatedObject == null) || (atChild.AllocatedObject.Length == 0))
                {
                    removeTable(atChild.TableName);
                    atChild = null;
                    continue;
                }

                alAllocationTables.Add(new AllocatedTable(xnChild));
            }
        }
        public void AddAllocateTable(String sTableName, String AllocatedObject)
        {
            if (alAllocationTables == null)
                alAllocationTables = new ArrayList();
            if (!alAllocationTables.Contains(sTableName))
                alAllocationTables.Add(new AllocatedTable(sTableName, AllocatedObject));
        }
        public void SaveAllocateTable(String sDiretory, XmlDocument xdDocument, XmlNode xnDateSource, SIM_LoadingForm chForm)
        {
            if (alAllocationTables == null)
                return;
            for (int i = 0; i < alAllocationTables.Count; i++)
            {
                AllocatedTable atTable = (AllocatedTable)alAllocationTables[i];
                if (chForm != null)
                    chForm.ChargementFichier("Saving file : " + atTable.TableName);
                XmlElement newNoeud = xdDocument.CreateElement("Table");
                newNoeud.SetAttribute("Name", atTable.TableName);
                newNoeud.AppendChild(xdDocument.CreateTextNode("\\Data\\" + i.ToString() + "_" + atTable.TableName + ".txt"));
                xnDateSource.AppendChild(newNoeud);
                //Enregistrement des fichiers en mémoire systeme
                OverallTools.FonctionUtiles.EcritureFichier(GetTable(atTable.TableName), sDiretory + "\\" + newNoeud.FirstChild.Value, "\t", true);
            }
        }
        #endregion

    }
    #endregion
}
