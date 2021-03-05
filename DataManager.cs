using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using SIMCORE_TOOL.Classes;
using System.Collections;
using SIMCORE_TOOL.Prompt.Athens;
using System.IO;
using SIMCORE_TOOL.com.crispico.generalUse;
using SIMCORE_TOOL.com.crispico.FlightPlansUtils;
using SIMCORE_TOOL.com.crispico.inputs.deterministic_transfer_distribution;
using SIMCORE_TOOL.com.crispico.inputs.flight_group_rules;

namespace SIMCORE_TOOL.DataManagement
{
    #region Les classes représentants les différentes tables que l'on peur rencontrer dans PAX2SIM
    #region internal interface IFilterTable
    /// <summary>
    /// Interface qui définit les fonctions qui doivent être présentes pour les filtres.
    /// </summary>
    internal interface IFilterTable
    {
        /// <summary>
        /// Obtient ou définit la définition du filtre sélectionné.
        /// </summary>
        Filter Definition { get; set; }
        /// <summary>
        /// Obtient la table parente auquel ce filtre se rattache.
        /// </summary>
        NormalTable Parent { get; set; }
        /// <summary>
        /// Fonction qui met à jour la table parente en fonction du contenu de la table courante.
        /// </summary>
        /// <returns>Booléen indiquant si la mise à jour s'est bien déroulée.</returns>
        Boolean UpdateParent();
        /// <summary>
        /// Get the root table for the current filter (if the parent of the current filter is a filter, then it takes the parent of the parent ...)
        /// </summary>
        NormalTable Root { get; }
        /// <summary>
        /// Function to set the VisualisationMode according to the parent table
        /// and the filter state.
        /// </summary>
        void SetVisuMode();

        /// <summary>
        /// Fonction qui supprime la table calculé, afin de forcer un nouveau calcul.
        /// </summary>
        void ParentTableModified();

        /// <summary>
        /// Fonction qui calcule la table pour le filtre courant. Si le filtre courant n'est pas valide, alors elle renverra null.
        /// </summary>
        /// <returns>Table calculée ou alors NULL.</returns>
        DataTable CalcFilter();
    }

    #endregion

    #region internal class NormalTable
    /// <summary>
    /// Table basique de PAX2SIM. Cette table contient un certain nombre de paramêtres, dont ses filtres,
    /// le graphique associé, la note associée, le mode de visualisation, si la table est chargée...
    /// </summary>
    internal class NormalTable
    {
        #region Variables
        /// <summary>
        /// Nom de la table qui est représentée par l'implémentation de la classe.
        /// </summary>
        protected String sMainTableName;

        /// <summary>
        /// La table stockée dans cette classe. Peut être NULL dans certains cas.
        /// </summary>
        protected DataTable dtTable;

        /// <summary>
        /// Information sur le fait que la table a été chargée en mémoire ou non. Les tables seront alors chargées uniquement
        /// à la demande.
        /// </summary>
        protected bool bLoaded;

        /// <summary>
        /// Booléen permettant le chargement des tables présentes dans les resources.
        /// </summary>
        private bool bPresentInResources;

        /// <summary>
        /// Chemin d'accès à la table sur le disque (dans le cas où la table n'est pas chargée en mémoire (\ref bLoaded))
        /// </summary>
        protected String sPath;

        /// <summary>
        /// La liste des filtres liés à la table courante. Les filtres sont représentés par les classes \ref FilterTable ou \ref ResultsFilterTable
        /// ils doivent également avoir hérité de la classe \ref NormalTable et implémenté l'interface \ref IFilterTable
        /// </summary>
        protected Dictionary<String, NormalTable> lfInheritedFilters;

        /// <summary>
        /// La définition du graphique associé à la table. Cette définition peut être NULL lorsque qu'aucun graphique n'est lié à la table.
        /// </summary>
        protected GraphicFilter gfGraphicDefinition;

        /// <summary>
        /// Le mode de représentation de la table. Ceci est appliqué à la table lorsqu'elle est affichée dans le DataGridView (fenêtre principale pour l'affichage
        /// des tables). Cela permet notamment de mettre en forme suivant le contenu de la cellule, ou suivant la position de la cellule dans la table.
        /// </summary>
        protected VisualisationMode vmMode;

        /// <summary>
        /// Note html associée à la table. Cette note peut être NULL.
        /// </summary>
        protected String sNote;

        /// <summary>
        /// Les informations sur les modalités de chargement de la table. Colonnes par défauts, lignes ...
        /// </summary>
        protected DataManagerInput.LoadParameters lpParameters;

        /// <summary>
        /// Pointeur vers la fonction qui permet de récupérer le chemin vers le dossier principal du projet. (Pour le cas où le fichier
        /// est ouvert partiellement (pas chargé en mémoire)).
        /// </summary>
        protected DataManager.GetPathDelegate gpdGetPath;

        /// <summary>
        /// Fonction appelée pour pouvoir vérifier que la table courante est conforme aux attentes. Valide pour en fait l'utilisation
        /// choisie par l'utilisateur.
        /// </summary>
        protected DataManagerInput.CheckTableDelegate fCheckTable;

        /// <summary>
        /// Variable permettant de savoir dans quelle version la table est enregistrée.
        /// </summary>
        protected VersionManager vmVersion;

        /// <summary>
        /// This variable holds the String representation of the Gantt 
        /// created with the data from this table. The Gantt Note uses
        /// HTML code similar to the normal Note
        /// </summary>
        public String ganttNote { get; set; } //>>GanttNote for Report

        /// <summary>
        /// The String representation of the image of the Dashboard.
        /// It uses similar HTML code as the Notmal Note.
        /// </summary>
        public String dashboardNote { get; set; } // >> Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur simple de cette classe, il permet d'initialiser les structures de données. 
        /// </summary>
        /// <param name="Name">Nom de la table que cette structure de données devra représenter.</param>
        internal NormalTable(String Name)
        {
            sMainTableName = Name;
            sNote = null;
            lfInheritedFilters = null;
            gfGraphicDefinition = null;
            vmMode = VisualisationMode.LockMode.Clone();
            bLoaded = false;
            sPath = "";
            fCheckTable = null;
        }
        // >> Task #12741 Pax2Sim - Dubai - flight exceptions
        internal NormalTable(String Name, DataTable _dtTable)
        {
            sMainTableName = Name;
            dtTable = _dtTable.Copy();
            sNote = null;
            lfInheritedFilters = null;
            gfGraphicDefinition = null;
            vmMode = VisualisationMode.LockMode.Clone();
            bLoaded = false;
            sPath = "";
            fCheckTable = null;
        }
        // << Task #12741 Pax2Sim - Dubai - flight exceptions

        /// <summary>
        /// Constructeur de la classe qui permet d'initialiser les structures de données. il initialise également 
        /// le mode de visualisation avec la valeur passée en paramètre.
        /// </summary>
        /// <param name="_Table">La table qui doit être stockée par ce système de donnée.</param>
        /// <param name="Mode">Le mode de visualisation. (\ref VisualisationMode)</param>
        internal NormalTable(DataTable _Table, VisualisationMode Mode)
            : this(_Table, Mode, null)
        {
        }

        /// <summary>
        /// Constructeur de la classe qui permet d'initialiser les structures de données. il initialise également 
        /// le mode de visualisation avec la valeur passée en paramètre et la définition du graphique.
        /// </summary>
        /// <param name="_Table">La table qui doit être stockée par ce système de donnée.</param>
        /// <param name="Mode">Le mode de visualisation. (\ref VisualisationMode)</param>
        /// <param name="GraphicDefinition">Définition du graphique associé à cette table.</param>
        protected NormalTable(String sName, VisualisationMode Mode, GraphicFilter GraphicDefinition)
            : this(sName)
        {
            dtTable = null;
            vmMode = Mode;
            gfGraphicDefinition = GraphicDefinition;
        }

        /// <summary>
        /// Constructeur de la classe qui permet d'initialiser les structures de données. il initialise également 
        /// le mode de visualisation avec la valeur passée en paramètre et le filtre graphique associé.
        /// </summary>
        /// <param name="_Table">La table qui doit être stockée par ce système de donnée.</param>
        /// <param name="Mode">Le mode de visualisation. (\ref VisualisationMode)</param>
        /// <param name="GraphicDefinition">La définition graphique pour cette table. (\ref GraphicFilter)</param>
        internal NormalTable(DataTable _Table, VisualisationMode Mode, GraphicFilter GraphicDefinition)
            : this(_Table.TableName)
        {
            bLoaded = true;
            UpdateTable(_Table);
            vmMode = Mode;
            gfGraphicDefinition = GraphicDefinition;
        }

        /// <summary>
        /// Constructeur qui charge la table à partir d'un noeud XML. Ce noeud XML contient l'ensemble des informations
        /// relatives au chargement de la table.
        /// </summary>
        /// <param name="xnNode">Noeud représentant un \ref NormalTable qui stocke les informations sur la table.</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (permet de faire des modifications liées à la version de la table)</param>
        /// <param name="sRootDirectory">Chemin vers le dossier racine du projet.</param>
        internal NormalTable(XmlElement xnNode, VersionManager vmVersion, String sRootDirectory)
        {
            bLoaded = false;
            sPath = "";
            Load(xnNode, vmVersion, sRootDirectory);
            fCheckTable = null;
        }

        /// <summary>
        /// Constructeur de NormalTable qui permet de faire une copie d'un \ref NormalTable. Le constructeur copie alors
        /// l'intégralité des informations contenues dans le \ref NormalTable dans la nouvelle structure. Les filtres ne 
        /// sont pas eux copiés
        /// </summary>
        /// <param name="ntOldTable">La structure de donnée modèle pour le nouveau \ref NormalTable</param>
        protected NormalTable(NormalTable ntOldTable)
        {
            sMainTableName = ntOldTable.Name;
            sNote = ntOldTable.Note;
            lfInheritedFilters = new Dictionary<string, NormalTable>();
            if (ntOldTable.GraphicDefinition != null)
                gfGraphicDefinition = new GraphicFilter(ntOldTable.GraphicDefinition.creerArbreXml(new XmlDocument()));
            else
                gfGraphicDefinition = null;
            if (ntOldTable.Mode != null)
                vmMode = ntOldTable.Mode.Clone();
            else
                Mode = null;//VisualisationMode.LockMode.Clone();
            bLoaded = ntOldTable.bLoaded;
            sPath = ntOldTable.Path;
            dtTable = ntOldTable.dtTable;
            fCheckTable = ntOldTable.fCheckTable;
            gpdGetPath = ntOldTable.gpdGetPath;
            lpParameters = ntOldTable.lpParameters;

        }
        #endregion

        #region Fonctions Delete, toString et Copy
        /// <summary>
        /// Fonction qui permet de supprimer la structure de donnée courante. Cette fonction se chargera notamment de rendre la mémoire
        /// pour les données incluses dans la structures. Elle parcourera aussi les noeuds filtres qui sont présents sur cette table
        /// et les supprimera également. \nElle retournera enfin tous les noms des filtres qui ont été supprimés.
        /// </summary>
        /// <returns>Liste des noms de tous les filtres qui ont été supprimés suite à la suppression du noeud courant.</returns>
        internal virtual List<String> Delete()
        {
            List<String> lsFilters = new List<String>();
            if (lfInheritedFilters != null)
            {
                ///On intialise la liste qui sera retournée avec les noms des filtres présents dans la liste des filtres du noeud courant.
                lsFilters.AddRange(lfInheritedFilters.Keys);

                ///Pour chaque filtre du noeud courant, on appelle la méthode \ref Delete() du filtre. Et on stocke le résultat de cette fonction 
                ///dans la liste qui sera retournée.
                int iNumber = lsFilters.Count;

                for (int i = 0; i < iNumber; i++)
                {
                    lsFilters.AddRange(lfInheritedFilters[lsFilters[i]].Delete());
                }
                lfInheritedFilters.Clear();
                lfInheritedFilters = null;
            }

            ///On désalloue les différentes variables de la classe.
            dtTable = null;
            sPath = null;
            gfGraphicDefinition = null;
            vmMode = null;
            sNote = null;
            ///On renvoie la liste mise à jour avec les noms des filtres supprimés.
            return lsFilters;
        }

        /// <summary>
        /// Fonction qui permet de faire une copie d'un \ref NormalTable
        /// </summary>
        /// <returns>Le nouveau \ref NormalTable créé.</returns>
        internal virtual Object Copy()
        {
            ///Appel du constructeur de copie \ref NormalTable(NormalTable ntOldTable) et Renvoie du résultat.
            return new NormalTable(this);
        }

        /// <summary>
        /// Renvoie le nom de la table.
        /// </summary>
        /// <returns>Le nom de la table représentée par l'instance courante</returns>
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region Accesseurs
        /// <summary>
        /// Obtient ou définit le nom de la table représentée par cette instance de la classe.
        /// </summary>
        internal virtual String Name
        {
            get
            {
                return sMainTableName;
            }
            set
            {
                sMainTableName = value;
            }
        }

        /// <summary>
        /// Obtient la table contenue dans cette classe.
        /// </summary>
        internal virtual DataTable Table
        {
            get
            {
                if (!bLoaded)
                {
                    if ((isFilter) && (sPath == null))
                    {
                        ///Si la classe courante représente un filtre, et que cette fonction est appelée, cela veut dire qu'il
                        ///s'agit d'un filtre copie. De plus dans cette partie, \ref bLoaded à FAUX indique que la table n'a pas
                        ///été recalculée.
                        DataTable dtTmp = ((IFilterTable)this).CalcFilter();
                        UpdateTable(dtTmp);
                        return dtTable;
                    }
                    if ((lpParameters != null) && (!lpParameters.bSaved))
                    {
                        ///Si la table n'est pas chargée et que la table n'avait pas été sauvegardée,
                        ///alors on initialize celle ci avec les données contenues dans \ref lpParameters
                        dtTable = lpParameters.InitializeTable();
                        dtTable.TableName = Name;
                        bLoaded = true;
                        return dtTable;
                    }

                    if ((sPath == null) || (sPath == ""))
                    {
                        ///Si le chemin d'accès est null et que \ref lpParameters est null, alors on renvoie le contenu de la table (NULL à priori).
                        if (lpParameters != null)
                        {
                            ///Si \ref lpParameters n'était pas null, alors on initialise la table à l'aide de la fonction \ref DataManagerInput.LoadParameters.InitializeTable().
                            dtTable = lpParameters.InitializeTable();
                            dtTable.TableName = Name;
                            bLoaded = true;
                        }
                        return dtTable;
                    }

                    ///Cas normal où la table doit être chargée depuis le disque.
                    dtTable = new DataTable(Name);
                    String sTmpPath = sPath;
                    if ((!System.IO.File.Exists(sPath)) && (!bPresentInResources))
                        if (gpdGetPath != null)
                            sTmpPath = gpdGetPath() + sPath;
                    if (lpParameters != null)
                    {
                        String sParentName = null;
                        if (lpParameters.sName != Name)
                            sParentName = lpParameters.sName;
                        ArrayList errors = new ArrayList();
                        dtTable = ConvertTables.getConvertTable.LectureFichier(Name,
                                                        sParentName,
                                                        lpParameters.dstEntetes,
                                                        lpParameters.lsPrimaryKeys,
                                                       sTmpPath,
                                                       "\t",
                                                       lpParameters.tTypeNewColumns,
                                                       bPresentInResources,
                                                       false,
                                                       errors,
                                                       vmVersion);
                        if (errors.Count > 0)                        // >> Bug #14618 Pax2Sim - User Data update
                            PAX2SIM.errorList.AddRange(errors);
                        else
                            PAX2SIM.errorList = new ArrayList();

                        bPresentInResources = false;
                        bLoaded = true;
                        if ((!CheckTable) || (dtTable == null))
                        {
                            dtTable = lpParameters.InitializeTable();
                            dtTable.TableName = Name;
                        }
                    }
                    else
                    {
                        //Une table qui n'a pas de format prédéfini.
                        //On peut donc la charger de manière transparente.
                        if (!OverallTools.FonctionUtiles.LectureFichier(dtTable, sTmpPath, "\t", null))
                        {
                            dtTable = null;
                            return null;
                        }
                        bLoaded = true;
                    }
                }
                return dtTable;
            }
        }

        /// <summary>
        /// Obtient ou définit le mode de visualisation souhaité pour cette table.
        /// </summary>
        internal virtual VisualisationMode Mode
        {
            get
            {
                /*if (vmMode == null)
                    return VisualisationMode.LockMode;*/
                return vmMode;
            }
            set
            {
                vmMode = value;
                if (lfInheritedFilters != null)
                {
                    VisualisationMode vmModeTmp = null;
                    if (vmMode != null)
                        vmModeTmp = vmMode.Clone();
                    if (vmModeTmp != null)
                    {
                        if ((vmModeTmp.ConditionnalFormatClass != null) && (vmModeTmp.ConditionnalFormatClass.Length > 0))
                        {
                            if (vmModeTmp.ConditionnalFormatClass[0] is ConditionnalFormatErrors)
                                vmModeTmp.ConditionnalFormatClass = null;
                        }
                    }

                    foreach (String sKey in lfInheritedFilters.Keys)
                    {
                        NormalTable ntTmp = lfInheritedFilters[sKey];
                        if (!ntTmp.isFilter)
                            continue;
                        if (!((IFilterTable)ntTmp).Definition.InheritedVisualisationMode)
                            continue;
                        if (vmModeTmp != null)
                            ntTmp.Mode = vmModeTmp.Clone();
                    }
                }
            }
        }

        /// <summary>
        /// Obtient ou définit la représentation graphique de cette table.
        /// </summary>
        internal virtual GraphicFilter GraphicDefinition
        {
            get
            {
                return gfGraphicDefinition;
            }
            set
            {
                if (gfGraphicDefinition != null)
                    gfGraphicDefinition = null;
                gfGraphicDefinition = value;
            }
        }

        /// <summary>
        /// Obtient ou définit la note html associée à cette table.
        /// </summary>
        internal virtual String Note
        {
            get
            {
                return sNote;
            }
            set
            {
                sNote = value;
            }
        }

        /// <summary>
        /// Obtient ou définit un booléen indiquant si la table est actuellement chargée en mémoire ou non.
        /// </summary>
        internal virtual Boolean Loaded
        {
            get
            {
                return bLoaded;
            }
            set
            {
                bLoaded = value;
                bPresentInResources = false;
            }
        }

        // >> Task #13955 Pax2Sim -BHS trace loading issue
        /// <summary>
        /// Used to bypass the error triggered by calling the Loaded getter.
        /// For some reason it triggers a memory access error: Attempted to read or write protected memory. This is often an indication that other memory is corrupt.
        /// </summary>
        internal bool tableIsLoaded
        {
            get
            {
                return bLoaded;
            }
        }
        // << Task #13955 Pax2Sim -BHS trace loading issue

        /// <summary>
        /// Définit un Booléen permettant le chargement des tables présentes dans les resources.
        /// </summary>
        internal virtual Boolean PresentInResources
        {
            set
            {
                bPresentInResources = value;
            }
        }

        /// <summary>
        /// Obtient ou définit le chemin d'enregistrement et/ou d'ouverture de cette table.
        /// </summary>
        internal virtual String Path
        {
            get
            {
                return sPath;
            }
            set
            {
                sPath = value;
            }
        }

        /// <summary>
        /// Renvoie un booléen indiquant si la classe courante est un filtre ou non.
        /// </summary>
        internal virtual bool isFilter
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Obtient le nombre de filtres ayant pour parent la table courante.
        /// </summary>
        internal virtual int Filters
        {
            get
            {
                if (lfInheritedFilters == null)
                    return 0;
                return lfInheritedFilters.Count;
            }
        }

        /// <summary>
        /// Obtient le nombre de filtres total qui sont associé à la table courante. Ajoute au nombre de filtres
        /// sur la table courante le nombre de filtres sur les filtres ...
        /// </summary>
        internal virtual int FiltersTotal
        {
            get
            {
                if (lfInheritedFilters == null)
                    return 0;
                int iResult = lfInheritedFilters.Count;
                foreach (String sTableName in lfInheritedFilters.Keys)
                {
                    iResult += lfInheritedFilters[sTableName].FiltersTotal;
                }
                return iResult;
            }
        }

        /// <summary>
        /// Les informations sur les modalités de chargement de la table. Colonnes par défauts, lignes ...
        /// </summary>
        internal virtual DataManagerInput.LoadParameters TableParameters
        {
            get
            {
                return lpParameters;
            }
            set
            {
                lpParameters = value;
                if (lpParameters != null)
                    Mode = lpParameters.vmDefaultVisualisationMode;
            }
        }

        /// <summary>
        /// Obtient ou définit la fonction qui permet de récupérer le chemin vers le dossier principal du projet. (Pour le cas où le fichier
        /// est ouvert partiellement (pas chargé en mémoire)). (\ref DataManager.GetPathDelegate)
        /// </summary>
        internal virtual DataManager.GetPathDelegate GetPath
        {
            get
            {
                return gpdGetPath;
            }
            set
            {
                gpdGetPath = value;
            }
        }

        /// <summary>
        /// Définit la fonction à utiliser pour vérifier la validité d'une table (\ref IsValid).
        /// </summary>
        internal virtual DataManagerInput.CheckTableDelegate CheckTableFunction
        {
            set
            {
                fCheckTable = value;
            }
        }

        /// <summary>
        /// Variable permettant de savoir dans quelle version la table est enregistrée.
        /// </summary>
        internal virtual VersionManager Version
        {
            set
            {
                vmVersion = value;
            }
        }

        /// <summary>
        /// Obtient un booléen qui représente si la table est valide ou non.
        /// </summary>
        internal virtual Boolean IsValid
        {
            get
            {
                ///\ref IsValid retourne une booléen indiquant is la fonction \ref getErrors à renvoyer des erreurs ou non.
                return getErrors(null, SortOrder.None) == null;
            }
        }

        /// <summary>
        /// Cette fonction permet de vérifier que les entetes de lignes sont bien renseignés (si cette table avait des entetes de lignes).
        /// </summary>
        internal Boolean CheckTable
        {
            get
            {
                if (lpParameters == null)
                    return true;
                DataTable dtTmp = Table;
                if (dtTmp == null)
                    return false;
                if (lpParameters.oDefaultFirstColumn != null)
                {
                    if (lpParameters.oDefaultFirstColumn.Length != dtTmp.Rows.Count)
                        return false;
                    foreach (DataRow ligne in dtTmp.Rows)
                    {
                        if (!OverallTools.FonctionUtiles.estPresentDansListe(ligne.ItemArray[0].ToString(), lpParameters.oDefaultFirstColumn))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Renvoie la liste des erreurs qui ont été rencontrées lors de la vérification de la table. Si des Warning sont trouvés
        /// alors ceux ci seront présents dans la table, mais ne seront pas bloquant (pas considérer comme des erreurs).
        /// </summary>
        /// <param name="sortedColumn">Colonne suivant laquelle la table doit être triée</param>
        /// <param name="Order"></param>
        /// <returns></returns>
        internal virtual ArrayList getErrors(String sortedColumn, SortOrder Order)
        {
            ///Si la fonction \ref fCheckTable est null, alors \ref getErrors() renvoie NULL (aucune erreur trouvée dans la table)
            if (fCheckTable == null)
                return null;
            ///Si le parametre \ref lpParameters est null, alors \ref getErrors() renvoie NULL (aucune erreur trouvée dans la table)
            if (lpParameters == null)
                return null;
            ///Si le parametre \ref lpParameters.tcChecks est null, alors \ref getErrors() renvoie NULL (aucune erreur trouvée dans la table)
            if (lpParameters.tcChecks == null)
                return null;
            ///\ref getErrors() renvoie le résultat de la fonction \ref fCheckTable appelée avec \ref lpParameters.tcChecks et la table courante comme parametres.
            return fCheckTable(lpParameters.tcChecks, this, sortedColumn, Order);
        }
        #endregion

        #region For the filters

        /// <summary>
        /// Permet d'ajouter un Filtre sur la table courante. Le filtre est représenté par un NormalTable, mais doit être d'un type qui
        /// implémente l'interface \ref IFilterTable. En effet cette interface permet d'être sur que le filtre contient les fonctions 
        /// utilisées pour un filtre.
        /// </summary>
        /// <param name="NewFilter">Le nouveau filtre que l'on souhaite ajouter (ou mettre à jour) </param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé ou non.</returns>
        internal virtual bool AddFilter(NormalTable NewFilter)
        {
            ///Si le nouveau filtre est NULL, alors on arrète la fonction et on renvoie NULL
            if (NewFilter == null)
                return false;
            ///On vérifie que le filtre que l'on souhaite ajouter est bien un filtre, il doit implémenter l'interface \ref IFilterTable
            ///Si ce n'est pas le cas, alors on retourne False, car l'objet que l'on souhaitait ajouter n'est pas valide pour représenter
            ///un filtre.
            if (!NewFilter.isFilter)
                return false;
            ///Si la liste des filtres (\ref lfInheritedFilters) n'a jamais été initialisée, alors on l'initialise.
            if (lfInheritedFilters == null)
                lfInheritedFilters = new Dictionary<String, NormalTable>();
            ///Si le filtre est déjà existant dans la liste, alors on le met à jour, sinon on l'ajoute aux filtres.
            if (lfInheritedFilters.ContainsKey(NewFilter.Name))
                lfInheritedFilters[NewFilter.Name] = NewFilter;
            else
                lfInheritedFilters.Add(NewFilter.Name, NewFilter);
            ///On renvoie Vrai pour indiquer que l'ajout (ou la mise à jour) s'est bien déroulée.
            return true;
        }

        /// <summary>
        /// Fonction qui permet de supprimer un filtre de la liste des filtres de la table courante.
        /// </summary>
        /// <param name="Name">Nom du filtre qui doit être supprimé.</param>
        internal void RemoveFilter(String Name)
        {
            ///Si le nom est invalide ou s'il n'y a aucun filtre de définis sur cette table,
            ///alors on stoppe la fonction.
            if (Name == null)
                return;
            if (lfInheritedFilters == null)
                return;
            ///Si le filtre existe dans la liste des filtres (\ref lfInheritedFilters), alors on le supprime,
            ///sinon on ne fait rien.
            if (lfInheritedFilters.ContainsKey(Name))
                lfInheritedFilters.Remove(Name);
        }

        /// <summary>
        /// Fonction qui permet de récupérer le filtre portant le nom passé en paramètre.
        /// </summary>
        /// <param name="Name">Nom du filtre désiré</param>
        /// <returns>NULL ou alors le filtre qui était voulu</returns>
        internal NormalTable GetFilter(String Name)
        {
            ///Si le nom est invalide ou s'il n'y a aucun filtre de définis sur cette table,
            ///alors on stoppe la fonction et on renvoie NULL
            if (Name == null)
                return null;
            if (lfInheritedFilters == null)
                return null;
            ///Si le filtre existe dans la liste des filtres (\ref lfInheritedFilters), alors on renvoie ce filtre
            ///sinon on renvoie NULL.
            if (lfInheritedFilters.ContainsKey(Name))
                return lfInheritedFilters[Name];
            return null;
        }

        /// <summary>
        /// Fonction qui permet de récupérer les noms de tous les filtres directs de la table courante.
        /// </summary>
        /// <returns>Retourne une liste contenant tous les noms des filtres de la table courante</returns>
        internal List<String> GetFilters()
        {
            ///S'il n'y a aucun filtre, alors Renvoyer une liste vide.
            List<String> lsReturn = new List<string>();
            if (lfInheritedFilters == null)
                return lsReturn;
            /// Parcourir l'ensemble des filtres et ajouter à une liste le nom de chacun d'entre eux.
            foreach (String sKey in lfInheritedFilters.Keys)
            {
                lsReturn.Add(sKey);
            }
            ///Renvoyer la liste ainsi calculée.
            return lsReturn;
        }

        /// <summary>
        /// Fonction qui permet de récupérer les noms de tous les filtres fils de la table courante. (Ainsi que les filtres des filtres...)
        /// </summary>
        /// <returns>Retourne la liste des noms des filtres liés à cette table, ainsi que tous les filtres des filtres.</returns>
        internal List<String> GetAllFilters()
        {
            ///S'il n'y a aucun filtre, alors Renvoyer une liste vide.
            List<String> lsReturn = new List<string>();
            if (lfInheritedFilters == null)
                return lsReturn;
            /// Parcourir l'ensemble des filtres et ajouter à une liste le nom de chacun d'entre eux et de leur filtre à l'aide de \ref GetAllFilters().
            foreach (String sKey in lfInheritedFilters.Keys)
            {
                lsReturn.Add(sKey);
                lsReturn.AddRange(lfInheritedFilters[sKey].GetAllFilters());
            }
            ///Renvoyer la liste ainsi calculée.
            return lsReturn;
        }
        #endregion

        #region For saving and loading the table
        /// <summary>
        /// Fonction permettant de charger une table à partir du noeud XML passé en paramètre. Il initialise les données de la table avec les 
        /// informations stockées dans le XML.
        /// </summary>
        /// <param name="xeElement">Noeud XML représentant un \ref NormalTable.</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (permet de faire des modifications liées à la version de la table)</param>
        /// <param name="sRootDirectory">Chemin vers le dossier racine du projet.</param>
        /// <returns>Booléen indiquant si l'ouverture s'est bien déroulée ou non.</returns>
        internal virtual bool Load(XmlElement xeElement, VersionManager vmVersion, String sRootDirectory)
        {
            ///Recherche de l'attribut Name dans le noeud XML. S'il n'est pas présent, alors la fonction s'arrète et 
            ///renvoie FAUX.
            if (!OverallTools.FonctionUtiles.hasNamedAttribute(xeElement, "Name"))
                return false;

            ///Initialisation de \ref sMainTableName avec le contenu de l'attribut Name.
            sMainTableName = xeElement.Attributes["Name"].Value;

            ///Recherche du VisualisationMode
            ///vmMode = D...

            ///Appel de \ref LoadTable() pour charger la table en tant que telle.
            LoadTable(xeElement, vmVersion);

            ///Si le noeud XML contient une définition pour le graphique, alors chargement de celle ci. (\ref GraphicFilter)
            if (OverallTools.FonctionUtiles.hasNamedChild(xeElement, "GraphicFilter"))
                gfGraphicDefinition = new GraphicFilter(xeElement["GraphicFilter"]);
            ///Si le noeud XML contient une définition pour une note, alors chargement de la note (\ref sNode).
            if (OverallTools.FonctionUtiles.hasNamedChild(xeElement, "Note"))
                sNote = xeElement["Note"].FirstChild.Value;
            //>>GanttNote for Report
            if (OverallTools.FonctionUtiles.hasNamedChild(xeElement, "GanttNote"))
                ganttNote = xeElement["GanttNote"].FirstChild.Value;
            // >> Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports
            if (OverallTools.FonctionUtiles.hasNamedChild(xeElement, GlobalNames.DASHBOARD_NOTE_XML_ELEMENT_NAME))
            {
                dashboardNote = xeElement[GlobalNames.DASHBOARD_NOTE_XML_ELEMENT_NAME].FirstChild.Value;
            }
            // << Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports
            return true;
        }

        /// <summary>
        /// Fonction qui charge la table en mémoire. (Si le noeud XML contient une définition pour celle ci.
        /// </summary>
        /// <param name="xeElement">Noeud XML représentant le \ref NormalTable.</param>
        /// <param name="vmVersion">Version dans laquelle le projet avait été enregistré.</param>
        /// <returns>Booléen indiquant si le chargement de la table s'est bien déroulé ou non.</returns>
        internal virtual bool LoadTable(XmlElement xeElement, VersionManager vmVersion)
        {
            ///On met \ref bLoaded à faux pour indiquer que la table n'est pas chargée.
            bLoaded = false;
            ///Si le noeud XML ne contient pas d'enfant se nommant Table, alors la fonction s'arrète et FAUX est retourné.
            if (!OverallTools.FonctionUtiles.hasNamedChild(xeElement, "Table"))
                return false;

            ///Si le Noeud table n'a pas d'attribut Path, alors la fonction s'arrète et FAUX est retourné.
            if (!OverallTools.FonctionUtiles.hasNamedAttribute(xeElement["Table"], "Path"))
                return false;
            ///Initialisation de \ref sPath avec la valeur de l'attribut Path.
            sPath = xeElement["Table"].Attributes["Path"].Value;
            ///Remise à NULL de la variable \ref dtTable.
            dtTable = null;
            ///Renvoie de VRAI.
            return true;
        }

        /// <summary>
        /// Cette fonction permet de sauvegarder la table courante dans le noeud XML passé en paramètre.
        /// </summary>
        /// <param name="xeElement">Noeud Xml ou doit être sauvegardée la table courante.</param>
        /// <param name="projet">Projet XML père de tous les noeuds XML (Requis pour pouvoir ajouter un noeud XML au projet global)</param>
        /// <param name="sRootDirectory">Dossier principal d'enregistrement du projet (Dossier où le .pax sera sauvegardé).</param>
        /// <param name="sOldRootDirectory">Ancien dossier principal d'enregistrement (SaveAs : Dossier de l'ancien projet, Save : Dossier .bak2 stockant l'ancienne version.)</param>
        /// <param name="sSavingDirectory">Dossier relatif où seront stockés les tables.</param>
        /// <returns>Booléen indiquant si l'enregistrement s'est bien déroulé ou non.</returns>
        internal virtual bool Save(XmlElement xeElement, XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Ajout d'un attribut Name au noeud courant (\ref xeElement) avec comme valeur le contenu de \ref sMainTableName.
            xeElement.SetAttribute("Name", sMainTableName);

            ///Appel de la fonction \ref SaveTable() pour sauvegarder l'intégralité des tables contenues dans cette classe.
            ///Si la fonction \ref SaveTable renvoie Null, alors rien n'est ajouté au noeud XML.
            XmlElement xeTable = SaveTable(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
            if (xeTable != null)
                xeElement.AppendChild(xeTable);

            ///Si la table contient une définition pour un graphique, alors cette définition est ajoutée sous forme XML au noeud XML.
            if (gfGraphicDefinition != null)
                xeElement.AppendChild(gfGraphicDefinition.creerArbreXml(projet));

            ///Si la table contient une note associée, alors celle ci est ajoutée au noeud XML.
            if ((sNote != null) && (sNote != ""))
            {
                XmlElement xeNote = projet.CreateElement("Note");
                xeNote.AppendChild(projet.CreateCDataSection(sNote));
                xeElement.AppendChild(xeNote);
            }
            //>>GanttNote for Report
            if ((ganttNote != null) && (ganttNote != ""))
            {
                XmlElement xeNote = projet.CreateElement("GanttNote");
                xeNote.AppendChild(projet.CreateCDataSection(ganttNote));
                xeElement.AppendChild(xeNote);
            }

            // >> Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports
            if (dashboardNote != null && dashboardNote != "")
            {
                XmlElement xeDashboardNote = projet.CreateElement(GlobalNames.DASHBOARD_NOTE_XML_ELEMENT_NAME);
                xeDashboardNote.AppendChild(projet.CreateCDataSection(dashboardNote));
                xeElement.AppendChild(xeDashboardNote);
            }
            // << Task #10645 Pax2Sim - Pax analysis - Summary: dashboard image for Reports

            ///Si la table possède des filtres
            if ((lfInheritedFilters != null) && (lfInheritedFilters.Count > 0))
            {
                ///__ Pour chaque filtre de la table.
                XmlElement xeFilters = projet.CreateElement("Filters");
                foreach (String sFilter in lfInheritedFilters.Keys)
                {
                    ///____ Appel de \ref Save sur le filtre.
                    XmlElement xnSaved = lfInheritedFilters[sFilter].Save(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
                    ///____ Ajout du noeud XML du filtre à la liste des Filtres de la table courante.
                    xeFilters.AppendChild(xnSaved);
                }
                xeElement.AppendChild(xeFilters);
            }
            return true;
        }

        /// <summary>
        /// Fonction qui se charge de sauvegarder la représentation de la table courante.
        /// </summary>
        /// <param name="projet">Projet XML auquel doivent être reliés tous les noeuds XML nécessaires pour représenté cette classe.</param>
        /// <param name="sRootDirectory">Noeud principal où doit être sauvegardé la table.</param>
        /// <param name="sOldRootDirectory">Ancien noeud principal où avait été sauvegardé la table. (dans le cas où la table n'a pas été chargée.</param>
        /// <param name="sSavingDirectory">Sous dossier dans lequel la table doit être engeristrée.</param>
        /// <returns>Noeud XML représentant la table et tous ses filtres associés.</returns>
        internal virtual XmlElement Save(XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Creation d'un noeud XML pour la table courante (\ref NormalTable)
            XmlElement xeNormalTable = projet.CreateElement("NormalTable");
            ///Appel de la fonction \ref Save avec en parametre le noeud créé
            Save(xeNormalTable, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
            ///Renvoie du noeud Créer.
            return xeNormalTable;
        }

        /// <summary>
        /// Fonction qui se charge de sauvegarder la table courante.
        /// </summary>
        /// <param name="projet">Projet XML auquel doivent être reliés tous le noeud XML qui contiendra le chemin vers le fichier.</param>
        /// <param name="sRootDirectory">Noeud principal où doit être sauvegardé la table.</param>
        /// <param name="sOldRootDirectory">Ancien noeud principal où avait été sauvegardé la table. (dans le cas où la table n'a pas été chargée.</param>
        /// <param name="sSavingDirectory">Sous dossier dans lequel la table doit être engeristrée.</param>
        /// <returns>Noeud XML contenant le chemin d'enregistrement de la table.</returns>
        internal virtual XmlElement SaveTable(XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Si \ref lpParameters est différent de null et qu'il indique que la table ne doit pas être sauvée, alors
            ///la fonction retourne NULL.
            if ((lpParameters != null) && (!lpParameters.bSaved))
                return null;

            if (!bLoaded)
            {
                String stmp = sPath;
                if (!System.IO.File.Exists(sPath))
                    stmp = sOldRootDirectory + sPath;
                ///Si la table n'est pas chargée, alors on copie le fichier dans le dossier de sauvegarde (\ref  OverallTools.ExternFunctions.CopyFile() )
                OverallTools.ExternFunctions.CopyFile(sRootDirectory, stmp, sSavingDirectory + sMainTableName + ".txt", "", null, null);
            }
            else
            {
                ///Sinon on enregistre le contenu de la table dans le fichier désigné.
                OverallTools.FonctionUtiles.EcritureFichier(Table, sRootDirectory + sSavingDirectory + sMainTableName + ".txt", "\t", true);
            }
            ///On crée un noeud XML avec le chemin relatif d'accès au fichier.
            XmlElement xeTable = projet.CreateElement("Table");
            xeTable.SetAttribute("Path", sSavingDirectory + sMainTableName + ".txt");
            return xeTable;
        }
        #endregion

        #region To update the table, or the filters of the current table.
        /// <summary>
        /// Fonction qui permet de mettre à jour le contenu de la table. La table passée en paramètre ira remplacé celle 
        /// déjà présente dans le système de données.
        /// </summary>
        /// <param name="dtNewTable">La table (à jour) que l'on souhaite placer dans le système de données</param>
        /// <returns>Booléen indiquant si la mise à jour s'est bien déroulée ou non.</returns>
        internal virtual bool UpdateTable(DataTable dtNewTable)
        {
            ///On sauvegarde l'ancienne version de la table.
            DataTable dtOld = dtTable;
            bool bOldLoaded = bLoaded;
            String sOldPath = sPath;

            ///On place la table à jour dans la classe.
            dtTable = dtNewTable;
            bLoaded = true;
            sPath = "";
            if (!CheckTable)
            {
                ///Si la table n'est pas valide (\ref CheckTable) alors on remet l'ancienne version et on renvoie FAUX.
                dtTable = dtOld;
                bLoaded = bOldLoaded;
                sPath = sOldPath;
                return false;
            }
            ///We update the filters and let the information about the update of the table.
            if (lfInheritedFilters != null)
            {
                foreach (NormalTable ntTmp in lfInheritedFilters.Values)
                {
                    if (ntTmp.isFilter)
                        ((IFilterTable)ntTmp).ParentTableModified();
                }
            }
            ///On renvoie Vrai
            return true;
        }

        /// <summary>
        /// Fonction qui se charge de mettre à jour le TreeView des tables pour les filtres de cette table. 
        /// </summary>
        /// <param name="sScenarioName">Nom du scénario courant auquel la table appartient.</param>
        /// <param name="tnTable">TreeNode de la table courante.</param>
        /// <param name="cmsMenuFilter">Menu déroulant devant être ajouté aux nouveaux noeuds.</param>
        internal virtual void UpdateFilters(String sScenarioName, TreeNode tnTable, ContextMenuStrip cmsMenuFilter, ContextMenuStrip cmsMenuException)
        {
            ///Si la table n'a aucun filtre, alors la fonction ne fait rien.
            if (lfInheritedFilters == null)
                return;
            ///Pour chaque filtre présent dans la liste.
            foreach (String sKey in lfInheritedFilters.Keys)
            {
                NormalTable ntTable = lfInheritedFilters[sKey];
                ///__ Recherche du noeud associé au filtre.
                TreeNode noeudFiltre = OverallTools.TreeViewFunctions.RechercherNomEnfantStrict(sKey, tnTable);
                if (noeudFiltre == null)
                {
                    IFilterTable ift = (IFilterTable)ntTable;

                    ///____ Si le noeud n'existe pas pour le filtre. alors création d'un noeud pour ce filtre et ajout de celui
                    ///ci à la liste des noeuds de la table mère.
                    TreeViewTag Tag = TreeViewTag.getFilterNode(sScenarioName, sKey, ift.Definition.Blocked, ift.Definition.copyTable);

                    ///____ On Applique le VisualisationMode du parent. Puis on le bloque si ne peux connaitre
                    noeudFiltre = OverallTools.TreeViewFunctions.createBranch(sKey, sKey, Tag, cmsMenuFilter);
                    if (noeudFiltre == null)
                        continue;
                    OverallTools.TreeViewFunctions.AddSortedNode(tnTable, noeudFiltre, true, true, false);
                }

                ///__ Appel de \ref UpdateFilters() sur le filtre.
                ntTable.UpdateFilters(sScenarioName, noeudFiltre, cmsMenuFilter, cmsMenuException);
            }
        }

        /// <summary>
        /// Fonction pour mettre à jour le blockage (editable ou non).
        /// Utilisée pour remettre à jour la variable bBlocked d'un Filter,
        /// donc il n'y a rien à faire pour tout autre table.
        /// </summary>
        /// <param name="reference">Table de reference pour la verification du filtre</param>
        public virtual void UpdateBlocking(DataTable reference)
        {
        }
        #endregion

        #region Fonctions qui permettent de récupérer les lignes de la table correspondant à un critère (ou des) donné.
        /// <summary>
        /// Fonction qui renvoit la première ligne rencontrée correspondants aux paramètres passés.
        /// </summary>
        /// <param name="dssParameters">Ensemble des clefs (Colonne,Valeur) devant être rencontré dans la table.</param>
        /// <returns>La première ligne correspondant aux critères des recherche.</returns>
        internal DataRow GetRow(Dictionary<String, String> dssParameters)
        {
            ///Appel de la fonction \ref OverallTools.DataFunctions.getRow().
            return OverallTools.DataFunctions.getRow(Table, dssParameters);
        }

        /// <summary>
        /// Fonction qui renvoit la première ligne rencontrée correspondants aux paramètres passés.
        /// </summary>
        /// <param name="dssParameters">Ensemble des clefs (Colonne,Valeur) devant être rencontré dans la table.</param>
        /// <returns>Les lignes correspondantes aux critères des recherche.</returns>
        internal List<DataRow> GetRows(Dictionary<String, String> dssParameters)
        {
            ///Appel de la fonction \ref OverallTools.DataFunctions.getRows().
            return OverallTools.DataFunctions.getRows(Table, dssParameters);
        }
        #endregion

        #region Fonction pour la récupération des noms des tables valide pour les simulations. 
        /// <summary>
        /// Fonction pour la récupération des noms des tables valide pour les simulations. (Tables qui ont un format
        /// correspondant au format nécessaire pour ce type de table. (lié dans \ref lpParameters)).
        /// </summary>
        /// <param name="bCheckShowUp">Booléen indiquant s'il faut également vérifier la validité des tables
        /// de ShowUp Profile</param>
        /// <returns>La liste des noms des tables (et filtres) qui sont valides pour représenter la table mère pour
        /// une simulation.</returns>
        internal List<String> getValidTables(bool bCheckShowUp)
        {
            ///Si la table est un filtre(\ref IsFilter()), alors il n'est pas possible de déterminer les tables valides, et la fonction renvoie alors 
            ///une liste vide.
            if (isFilter)
                return new List<string>();
            ///Si \ref lpParameters est null, alors il n'est pas possible de déterminer les tables valides, et la fonction renvoie alors une liste
            ///vide.
            if (lpParameters == null)
                return new List<string>();
            ///Si \ref lpParameter.dstEntetes est null, alors il n'est pas possible de déterminer les tables valides, et la fonction renvoie alors 
            ///une liste vide.
            if (lpParameters.dstEntetes == null)
                return new List<string>();
            ///La fonction renvoie le résultat de la fonction \ref getValidTables avec comme paramètres \ref lpParameters.dstEntetes et bCheckShowUp.
            return getValidTables(lpParameters.dstEntetes, bCheckShowUp);
        }

        /// <summary>
        /// Fonction pour la récupération des noms des tables valide pour les simulations. (Tables qui ont un format
        /// correspondant au format nécessaire pour ce type de table. (lié dans \ref lpParameters)).
        /// </summary>
        /// <param name="lsColumn">La liste des entetes de colonnes devant être présents dans la table.</param>
        /// <param name="bCheckShowUp">Booléen indiquant s'il faut également vérifier la validité des tables
        /// de ShowUp Profile</param>
        /// <returns>La liste des noms des tables (et filtres) qui sont valides pour représenter la table mère pour
        /// une simulation.</returns>
        internal List<String> getValidTables(List<DataManagerInput.LoadParameters.Entete> lsColumn, bool bCheckShowUp)
        {
            List<String> lsReturn = new List<string>();
            bool bValid = true;
            ///Récupération de la table courante.
            DataTable dtTmp = Table;
            if (dtTmp != null)
            {
                ///Si la table courante n'est pas null, il faut parcourir l'ensemble des \ref DataManagerInput.LoadParameters.Entete de \ref lsColumn
                ///et vérifier que ces entetes sont présents dans la table courante.
                foreach (DataManagerInput.LoadParameters.Entete Entete in lsColumn)
                {
                    if (!dtTmp.Columns.Contains(Entete.sName))
                    {
                        bValid = false;
                        break;
                    }
                }
                ///Si tous les \ref DataManagerInput.LoadParameters.Entete de \ref lsColumn sont présents dans la table, alors le nom de la table est 
                ///ajouté à la liste qui sera renvoyée.
                if (bValid && ((!bCheckShowUp) || (GestionDonneesHUB2SIM.IsValidShowUp(dtTable))))
                    lsReturn.Add(sMainTableName);
            }

            if (lfInheritedFilters != null)
            {
                ///Si la table possède des filtres (\ref lfInheritedFilters), alors la fonction parcour l'ensemble des filtres et appelle la fonction
                ///\ref getValidTables avec comme paramètres \ref lpParameters.dstEntetes et bCheckShowUp. Le résultat de cette fonction est alors
                ///ajouté à la liste qui sera renvoyé.
                foreach (String sKey in lfInheritedFilters.Keys)
                {
                    NormalTable ntFilter = lfInheritedFilters[sKey];
                    lsReturn.AddRange(ntFilter.getValidTables(lsColumn, bCheckShowUp));
                }
            }
            ///Renvoie de la liste ainsi obtenue.
            return lsReturn;
        }
        #endregion
    }
    #endregion

    #region internal class ExceptionTable : NormalTable
    /// <summary>
    /// Table basique qui peut avoir des Exceptions. la table est identique à la \ref NormalTable, cependant elle
    /// contient également un certain nombre liens vers des tables d'exceptions. Ces tables d'exceptions, sont des 
    /// cas particuliers appliqués à la table mère.
    /// </summary>
    internal class ExceptionTable : NormalTable
    {
        /// <summary>
        /// Enumération des types d'exceptions qui peuvent être ajoutées à la table représentée par ExceptionTable.
        /// </summary>
        internal enum ExceptionTableParameters
        {
            /// <summary>
            /// Pas d'exceptions possibles sur la table courante (valeur par défaut)
            /// </summary>
            None = 0,
            /// <summary>
            /// Exceptions possibles sur les classes des passagers.
            /// </summary>
            FirstAndBusiness = 1,
            /// <summary>
            /// Exceptions possibles sur les catégories de vols.
            /// </summary>
            FlightCategory = 2,
            /// <summary>
            /// Exceptions possibles sur les compagnies aériennes.
            /// </summary>
            Airline = 4,
            /// <summary>
            /// Exceptions possibles sur les vols.
            /// </summary>
            Flight = 8
        };

        /// <summary>
        /// Enumération du format des exceptions qui seront appliquées sur la table. Le format indique si les exceptions sont
        /// en ligne ou en colonne. Cela permet de déterminer les fonctions qui pourront être appliquées sur les tables
        /// d'exceptions.
        /// </summary>
        internal enum ExceptionTableFormat
        {
            /// <summary>
            /// Les exceptions sont ajoutées en colonnes (chaque nouvelle exception est représentée par une nouvelle colonne.)
            /// </summary>
            Column,
            /// <summary>
            /// les exceptions sont ajoutées en lignes (chaque nouvelle exception est représentée par un certain nombre de lignes)
            /// </summary>
            Line
        };

        #region Variables
        #region Statiques / Const
        /// <summary>
        /// Nom usuel pour la table d'exception First & Bussiness pour la sauvegarde en XML.
        /// </summary>
        private const String ExceptionFBText = "ExceptionFB";

        /// <summary>
        /// Nom usuel pour la table d'exception flight category pour la sauvegarde en XML.
        /// </summary>
        private const String ExceptionFCText = "ExceptionFC";

        /// <summary>
        /// Nom usuel pour la table d'exception First & Bussiness pour la flight category pour la sauvegarde en XML.
        /// </summary>
        private const String ExceptionFCFBText = "ExceptionFCFB";

        /// <summary>
        /// Nom usuel pour la table d'exception Airline pour la sauvegarde en XML.
        /// </summary>
        private const String ExceptionAirlineText = "ExceptionAirline";

        /// <summary>
        /// Nom usuel pour la table d'exception First & Bussiness pour les airline pour la sauvegarde en XML.
        /// </summary>
        private const String ExceptionAirlineFBText = "ExceptionAirlineFB";

        /// <summary>
        /// Nom usuel pour la table d'exception Flight pour la sauvegarde en XML.
        /// </summary>
        private const String ExceptionFlightText = "ExceptionFlight";

        /// <summary>
        /// Nom usuel pour la table d'exception First & Bussiness pour les flight pour la sauvegarde en XML.
        /// </summary>
        private const String ExceptionFlightFBText = "ExceptionFlightFB";

        #endregion

        /// <summary>
        /// La table d'exceptions liée aux classes (directement située sous la table à laquelle elle s'applique)
        /// </summary>
        NormalTable sExceptionFB;

        /// <summary>
        /// La table d'exceptions liée aux catégories de vols (directement située sous la table à laquelle elle s'applique)
        /// </summary>
        NormalTable sExceptionFC;

        /// <summary>
        /// La table d'exceptions liée aux catégories de vols et aux classes (Directement située sous la table d'exception liées aux 
        /// catégories de vols)
        /// </summary>
        NormalTable sExceptionFCFB;

        /// <summary>
        /// La table d'exceptions liée aux compagnies aériennes. (Directement située sous la table d'exception liées aux catégories de vols, 
        /// ou alors directement sous la table à laquelle elle s'applique).
        /// </summary>
        NormalTable sExceptionAirline;

        /// <summary>
        /// La table d'exceptions liée aux compagnies aériennes est aux classes. (Directement située sous la table d'exception liées aux compagnies
        /// aériennes).
        /// </summary>
        NormalTable sExceptionAirlineFB;

        /// <summary>
        /// La table d'exceptions liée aux vols. (Directement située sous la table d'exception liées aux compagnies aériennes )
        /// </summary>
        NormalTable sExceptionFlight;

        /// <summary>
        /// La table d'exceptions liée aux vols et aux classes. (Directement située sous la table d'exception liées aux vols)
        /// </summary>
        NormalTable sExceptionFlightFB;

        /// <summary>
        /// Le type d'exception qui est mis en place pour la table courante. (FB / Flight / FC / Airline)
        /// </summary>
        ExceptionTableParameters etpExceptionType;

        /// <summary>
        /// Le format des exceptions (Ligne ou colonnes)
        /// </summary>
        ExceptionTableFormat etfExceptionFormat;

        /// <summary>
        /// Parametre indiquant s'il faut utiliser les valeurs des tables d'exception
        /// pour la simulation.
        /// </summary>
        protected bool bUseException = true;

        #endregion

        #region Constructeurs


        /// <summary>
        /// Constructeur de la classe qui permet d'initialiser les structures de données. il initialise également 
        /// le mode de visualisation avec la valeur passée en paramètre et le filtre graphique associé.
        /// </summary>
        /// <param name="_Table">La table qui doit être stockée par ce système de donnée.</param>
        /// <param name="Mode">Le mode de visualisation. (\ref VisualisationMode)</param>
        /// <param name="GraphicDefinition">La définition graphique pour cette table. (\ref GraphicFilter)</param>
        internal ExceptionTable(DataTable _Table, VisualisationMode Mode, GraphicFilter GraphicDefinition)
            : base(_Table, Mode, GraphicDefinition)
        {
            etpExceptionType = ExceptionTableParameters.None;
            etfExceptionFormat = ExceptionTableFormat.Line;
        }
        /// <summary>
        /// Constructeur de la classe qui permet d'initialiser les structures de données. il initialise également 
        /// le mode de visualisation avec la valeur passée en paramètre et le filtre graphique associé.
        /// </summary>
        /// <param name="sName">Le nom de la table qui doit être construite</param>
        /// <param name="Mode">Le mode de visualisation. (\ref VisualisationMode)</param>
        /// <param name="GraphicDefinition">La définition graphique pour cette table. (\ref GraphicFilter)</param>
        protected ExceptionTable(String sName, VisualisationMode Mode, GraphicFilter GraphicDefinition)
            : base(sName, Mode, GraphicDefinition)
        {
            etpExceptionType = ExceptionTableParameters.None;
            etfExceptionFormat = ExceptionTableFormat.Line;
        }

        /// <summary>
        /// Constructeur qui charge la table à partir d'un noeud XML. Ce noeud XML contient l'ensemble des informations
        /// relatives au chargement de la table.
        /// </summary>
        /// <param name="xnNode">Le noeud XML représentant l'instance que l'on souhaite charger.</param>
        /// <param name="vmVersion">La version d'enregistrement de sauvegarde du système de données</param>
        /// <param name="sRootDirectory">Le chemin d'accès vers le dossier qui contient le .pax. (Permet ensuite 
        /// de retrouver les élements souhaités)</param>
        internal ExceptionTable(XmlElement xnNode, VersionManager vmVersion, String sRootDirectory)
            : base(xnNode, vmVersion, sRootDirectory)
        {
            etpExceptionType = ExceptionTableParameters.None;
            etfExceptionFormat = ExceptionTableFormat.Line;
        }
        /// <summary>
        /// Constructeur copie pour la classe courante. Ce constructeur permet d'initialiser une nouvelle instance
        /// de \ref ExceptionTable en faisant une copie exacte de la table passée en paramètre.
        /// </summary>
        /// <param name="etTable">La table mère qui doit être copiée.</param>
        internal ExceptionTable(ExceptionTable etTable)
            : base(etTable)
        {
            if (etTable.sExceptionFB != null)
                sExceptionFB = (NormalTable)etTable.sExceptionFB.Copy();
            if (etTable.sExceptionFC != null)
                sExceptionFC = (NormalTable)etTable.sExceptionFC.Copy();
            if (etTable.sExceptionFCFB != null)
                sExceptionFCFB = (NormalTable)etTable.sExceptionFCFB.Copy();
            if (etTable.sExceptionAirline != null)
                sExceptionAirline = (NormalTable)etTable.sExceptionAirline.Copy();
            if (etTable.sExceptionAirlineFB != null)
                sExceptionAirlineFB = (NormalTable)etTable.sExceptionAirlineFB.Copy();
            if (etTable.sExceptionFlight != null)
                sExceptionFlight = (NormalTable)etTable.sExceptionFlight.Copy();
            if (etTable.sExceptionFlightFB != null)
                sExceptionFlightFB = (NormalTable)etTable.sExceptionFlightFB.Copy();
            etpExceptionType = etTable.etpExceptionType;
            etfExceptionFormat = etTable.etfExceptionFormat;
        }

        /// <summary>
        /// Constructeur copie pour la classe courante. Ce constructeur permet d'initialiser une nouvelle instance
        /// de \ref ExceptionTable en faisant une copie exacte de la table \ref NormalTable passée en paramètre.
        /// Ce constructeur permet notamment de transformer un \ref NormalTable en \ref ExceptionTable
        /// </summary>
        /// <param name="etTable"></param>
        internal ExceptionTable(NormalTable etTable)
            : base(etTable)
        {
            if (etTable is ExceptionTable)
            {
                ExceptionTable tTable = (ExceptionTable)etTable;
                sExceptionFB = tTable.sExceptionFB;
                sExceptionFC = tTable.sExceptionFC;
                sExceptionFCFB = tTable.sExceptionFCFB;
                sExceptionAirline = tTable.sExceptionAirline;
                sExceptionAirlineFB = tTable.sExceptionAirlineFB;
                sExceptionFlight = tTable.sExceptionFlight;
                sExceptionFlightFB = tTable.sExceptionFlightFB;
                etpExceptionType = tTable.etpExceptionType;
                etfExceptionFormat = tTable.etfExceptionFormat;
            }
            else
            {
                sExceptionFB = null;
                sExceptionFC = null;
                sExceptionFCFB = null;
                sExceptionAirline = null;
                sExceptionAirlineFB = null;
                sExceptionFlight = null;
                sExceptionFlightFB = null;
                etpExceptionType = ExceptionTableParameters.None;
                etfExceptionFormat = ExceptionTableFormat.Line;
            }
        }
        #endregion

        #region Accesseurs
        /// <summary>
        /// Obtient ou définit la table d'exceptions liée aux classes (directement située sous la table à laquelle elle s'applique)
        /// </summary>
        internal NormalTable ExceptionFB
        {
            get
            {
                if ((etpExceptionType & ExceptionTableParameters.FirstAndBusiness) == 0)
                    return null;
                return sExceptionFB;
            }
            set
            {
                if (sExceptionFB != null)
                {
                    sExceptionFB.Delete();
                    sExceptionFB = null;
                }
                if ((etpExceptionType & ExceptionTableParameters.FirstAndBusiness) == 0)
                    return;
                sExceptionFB = value;
                if (value != null)
                    value.GetPath = GetPath;
            }
        }

        /// <summary>
        /// Obtient ou définit la table d'exceptions liée aux catégories de vols (directement située sous la table à laquelle elle s'applique)
        /// </summary>
        internal NormalTable ExceptionFC
        {
            get
            {
                if ((etpExceptionType & ExceptionTableParameters.FlightCategory) == 0)
                    return null;
                return sExceptionFC;
            }
            set
            {
                if (sExceptionFC != null)
                {
                    sExceptionFC.Delete();
                    sExceptionFC = null;
                }
                if ((etpExceptionType & ExceptionTableParameters.FlightCategory) == 0)
                    return;
                sExceptionFC = value;
                if (value != null)
                    value.GetPath = GetPath;
            }
        }

        /// <summary>
        /// Obtient ou définit la table d'exceptions liée aux catégories de vols et aux classes (Directement située sous la table d'exception liées aux 
        /// catégories de vols)
        /// </summary>
        internal NormalTable ExceptionFCFB
        {
            get
            {
                if ((etpExceptionType & ExceptionTableParameters.FirstAndBusiness) == 0)
                    return null;
                if ((etpExceptionType & ExceptionTableParameters.FlightCategory) == 0)
                    return null;
                return sExceptionFCFB;
            }
            set
            {
                if (sExceptionFCFB != null)
                {
                    sExceptionFCFB.Delete();
                    sExceptionFCFB = null;
                }
                if ((etpExceptionType & ExceptionTableParameters.FirstAndBusiness) == 0)
                    return;
                if ((etpExceptionType & ExceptionTableParameters.FlightCategory) == 0)
                    return;
                sExceptionFCFB = value;
                if (value != null)
                    value.GetPath = GetPath;
            }
        }

        /// <summary>
        /// Obtient ou définit la table d'exceptions liée aux compagnies aériennes. (Directement située sous la table d'exception liées aux catégories de vols, 
        /// ou alors directement sous la table à laquelle elle s'applique).
        /// </summary>
        internal NormalTable ExceptionAirline
        {
            get
            {
                if ((etpExceptionType & ExceptionTableParameters.Airline) == 0)
                    return null;
                return sExceptionAirline;
            }
            set
            {
                if (sExceptionAirline != null)
                {
                    sExceptionAirline.Delete();
                    sExceptionAirline = null;
                }
                if ((etpExceptionType & ExceptionTableParameters.Airline) == 0)
                    return;
                sExceptionAirline = value;
                if (value != null)
                    value.GetPath = GetPath;
            }
        }

        /// <summary>
        /// Obtient ou définit la table d'exceptions liée aux compagnies aériennes est aux classes. (Directement située sous la table d'exception liées aux compagnies
        /// aériennes).
        /// </summary>
        internal NormalTable ExceptionAirlineFB
        {
            get
            {
                if ((etpExceptionType & ExceptionTableParameters.FirstAndBusiness) == 0)
                    return null;
                if ((etpExceptionType & ExceptionTableParameters.Airline) == 0)
                    return null;
                return sExceptionAirlineFB;
            }
            set
            {
                if (sExceptionAirlineFB != null)
                {
                    sExceptionAirlineFB.Delete();
                    sExceptionAirlineFB = null;
                }
                if ((etpExceptionType & ExceptionTableParameters.FirstAndBusiness) == 0)
                    return;
                if ((etpExceptionType & ExceptionTableParameters.Airline) == 0)
                    return;
                sExceptionAirlineFB = value;
                if (value != null)
                    value.GetPath = GetPath;
            }
        }

        /// <summary>
        /// Obtient ou définit la table d'exceptions liée aux vols. (Directement située sous la table d'exception liées aux compagnies aériennes )
        /// </summary>
        internal NormalTable ExceptionFlight
        {
            get
            {
                if ((etpExceptionType & ExceptionTableParameters.Flight) == 0)
                    return null;
                return sExceptionFlight;
            }
            set
            {
                if (sExceptionFlight != null)
                {
                    sExceptionFlight.Delete();
                    sExceptionFlight = null;
                }
                if ((etpExceptionType & ExceptionTableParameters.Flight) == 0)
                    return;
                sExceptionFlight = value;
                if (value != null)
                    value.GetPath = GetPath;
            }
        }

        /// <summary>
        /// Obtient ou définit la table d'exceptions liée aux vols et aux classes. (Directement située sous la table d'exception liées aux vols)
        /// </summary>
        internal NormalTable ExceptionFlightFB
        {
            get
            {
                if ((etpExceptionType & ExceptionTableParameters.FirstAndBusiness) == 0)
                    return null;
                if ((etpExceptionType & ExceptionTableParameters.Flight) == 0)
                    return null;
                return sExceptionFlightFB;
            }
            set
            {
                if (sExceptionFlightFB != null)
                {
                    sExceptionFlightFB.Delete();
                    sExceptionFlightFB = null;
                }
                if ((etpExceptionType & ExceptionTableParameters.FirstAndBusiness) == 0)
                    return;
                if ((etpExceptionType & ExceptionTableParameters.Flight) == 0)
                    return;
                sExceptionFlightFB = value;
                if (value != null)
                    value.GetPath = GetPath;
            }
        }

        /// <summary>
        /// Le type d'exception qui est mis en place pour la table courante. (FB / Flight / FC / Airline)
        /// </summary>
        internal ExceptionTableParameters ExceptionType
        {
            get
            {
                return etpExceptionType;
            }
            set
            {
                etpExceptionType = value;
            }
        }

        /// <summary>
        /// Le format des exceptions (Ligne ou colonnes)
        /// </summary>
        internal ExceptionTableFormat ExceptionFormat
        {
            get { return etfExceptionFormat; }
            set { etfExceptionFormat = value; }
        }

        /// <summary>
        /// Obtient ou définit la fonction qui permet de récupérer le chemin vers le dossier principal du projet. (Pour le cas où le fichier
        /// est ouvert partiellement (pas chargé en mémoire)). (\ref DataManager.GetPathDelegate)
        /// </summary>
        internal override DataManager.GetPathDelegate GetPath
        {
            get
            {
                return gpdGetPath;
            }
            set
            {
                gpdGetPath = value;
                if (sExceptionFB != null)
                    sExceptionFB.GetPath = value;
                if (sExceptionFC != null)
                    sExceptionFC.GetPath = value;
                if (sExceptionFCFB != null)
                    sExceptionFCFB.GetPath = value;
                if (sExceptionAirline != null)
                    sExceptionAirline.GetPath = value;
                if (sExceptionAirlineFB != null)
                    sExceptionAirlineFB.GetPath = value;
                if (sExceptionFlight != null)
                    sExceptionFlight.GetPath = value;
                if (sExceptionFlightFB != null)
                    sExceptionFlightFB.GetPath = value;
            }
        }

        /// <summary>
        /// Obtient ou définit le mode de visualisation souhaité pour cette table.
        /// </summary>
        internal override VisualisationMode Mode
        {
            get
            {
                return base.Mode;
            }
            set
            {
                base.Mode = value;
                ///On répercute le changement de mode de visualisation sur l'ensemble des tables d'exceptions présentes 
                ///dans ce \ref ExceptionTable.
                if (ExceptionFB != null)
                    ExceptionFB.Mode = value;
                if (ExceptionFC != null)
                    ExceptionFC.Mode = value;
                if (ExceptionFCFB != null)
                    ExceptionFCFB.Mode = value;
                if (ExceptionAirline != null)
                    ExceptionAirline.Mode = value;
                if (ExceptionAirlineFB != null)
                    ExceptionAirlineFB.Mode = value;
                if (ExceptionFlight != null)
                    ExceptionFlight.Mode = value;
                if (ExceptionFlightFB != null)
                    ExceptionFlightFB.Mode = value;
            }
        }

        /// <summary>
        /// Obtient ou definie le parametre indiquant s'il faut utiliser les valeurs des tables d'exception
        /// pour la simulation.
        /// </summary>
        internal bool UseException
        {
            get { return bUseException; }
            set
            {
                bUseException = value;
                if (lfInheritedFilters == null)
                    return;
                foreach (NormalTable ntTable in lfInheritedFilters.Values)
                {
                    if (!ntTable.isFilter)
                        continue;
                    ((FilterTable)ntTable).UseException = value;
                }
            }
        }

        /// <summary>
        /// Variable permettant de savoir dans quelle version la table est enregistrée.
        /// </summary>
        internal virtual VersionManager Version
        {
            set
            {
                base.Version = value;
                if (ExceptionFB != null)
                    ExceptionFB.Version = value;

                if (ExceptionFC != null)
                    ExceptionFC.Version = value;

                if (ExceptionFCFB != null)
                    ExceptionFCFB.Version = value;

                if (ExceptionAirline != null)
                    ExceptionAirline.Version = value;

                if (ExceptionAirlineFB != null)
                    ExceptionAirlineFB.Version = value;

                if (ExceptionFlight != null)
                    ExceptionFlight.Version = value;

                if (ExceptionFlightFB != null)
                    ExceptionFlightFB.Version = value;

            }
        }
        #endregion

        #region Fonctions Delete, AddFilter et Copy

        /// <summary>
        /// Fonction qui permet de supprimer la structure de donnée courante. Cette fonction se chargera notamment de rendre la mémoire
        /// pour les données incluses dans la structures (toutes les tables d'exceptions présentes). 
        /// Elle parcourera aussi les noeuds filtres qui sont présents sur cette table
        /// et les supprimera également. \nElle retournera enfin tous les noms des filtres qui ont été supprimés.
        /// </summary>
        /// <returns>Liste des noms de tous les filtres qui ont été supprimés suite à la suppression du noeud courant.</returns>
        internal override List<String> Delete()
        {
            ///Suppression des tables d'exceptions.
            if (sExceptionFB != null)
                sExceptionFB.Delete();
            sExceptionFB = null;

            if (sExceptionFC != null)
                sExceptionFC.Delete();
            sExceptionFC = null;

            if (sExceptionFCFB != null)
                sExceptionFCFB.Delete();
            sExceptionFCFB = null;

            if (sExceptionAirline != null)
                sExceptionAirline.Delete();
            sExceptionAirline = null;

            if (sExceptionAirlineFB != null)
                sExceptionAirlineFB.Delete();
            sExceptionAirlineFB = null;

            if (sExceptionFlight != null)
                sExceptionFlight.Delete();
            sExceptionFlight = null;

            if (sExceptionFlightFB != null)
                sExceptionFlightFB.Delete();
            sExceptionFlightFB = null;

            ///Appel de la fonction de base \ref NormalTable.Delete()
            return base.Delete();
        }

        /// <summary>
        /// Fonction héritée de Object qui permet de copier l'élément courant.
        /// </summary>
        /// <returns>Une instance de \ref ExceptionTable</returns>
        internal override Object Copy()
        {
            return new ExceptionTable(this);
        }

        /// <summary>
        /// Permet d'ajouter un Filtre sur la table courante. Le filtre est représenté par un NormalTable, mais doit être d'un type qui
        /// implémente l'interface \ref IFilterTable. En effet cette interface permet d'être sur que le filtre contient les fonctions 
        /// utilisées pour un filtre.
        /// </summary>
        /// <param name="NewFilter">Le nouveau filtre que l'on souhaite ajouter (ou mettre à jour) </param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé ou non.</returns>
        internal override bool AddFilter(NormalTable NewFilter)
        {
            ///Si le nouveau filtre hérite de ExceptionTable, alors les définitions pour les tables d'exception de la
            ///table courante sont copiées dans le filtre.
            if (NewFilter is ExceptionTable)
            {
                ((ExceptionTable)NewFilter).etpExceptionType = etpExceptionType;
                ((ExceptionTable)NewFilter).etfExceptionFormat = etfExceptionFormat;
            }
            ///Appel de la fonction de base \ref AddFilter().
            return base.AddFilter(NewFilter);
        }
        #endregion

        #region Fonctions pour récupérer les informations en fonction des différentes exceptions présentes dans la tables et des informations passées en paramètres.
        /// <summary>
        /// Fonction qui permet de récupérer la ligne correspondant à l'information recherchée. Elle appliquera en priorité les 
        /// rêgles d'exceptions mises en places pour la table courante. Si aucune des tables d'exceptions ne renvoie d'informations
        /// alors les informations contenues dans la table mère seront renvoyées. \n Attention cette fonction n'est applicable que sur 
        /// les tables ayant des exceptions par ligne (en opposition aux tables avec exceptions par colonnes.
        /// </summary>
        /// <param name="iClasse">La classe correspondant aux données qui doivent être collectées. (1==> F&B / Autre ==> Eco.)</param>
        /// <param name="sFlight">Le vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sAirline">La compagnie aérienne du vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sFlightCategory">La catégorie de vol du vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sInformation">L'information recherchée dans la table.</param>
        /// <returns></returns>
        internal DataRow GetInformationsRow(int iClasse, String sFlight, String sAirline, String sFlightCategory, String sInformation)
        {
            ///Récupération de la table courante.
            DataTable dtTmp = Table;
            ///Si la table courante est NULL, renvoie de NULL
            if (dtTmp == null)
                return null;
            ///Si la table courante ne possède aucune colonne, renvoie de NULL.
            if (dtTmp.Columns.Count == 0)
                return null;
            ///Renvoie du résultat de la fonction \ref GetInformationsRow().
            return GetInformationsRow(iClasse, sFlight, sAirline, sFlightCategory, sInformation, dtTmp.Columns[0].ColumnName);
        }
        /// <summary>
        /// Fonction qui permet de récupérer la ligne correspondant à l'information recherchée. Elle appliquera en priorité les 
        /// rêgles d'exceptions mises en places pour la table courante. Si aucune des tables d'exceptions ne renvoie d'informations
        /// alors les informations contenues dans la table mère seront renvoyées. \n Attention cette fonction n'est applicable que sur 
        /// les tables ayant des exceptions par ligne (en opposition aux tables avec exceptions par colonnes.
        /// </summary>
        /// <param name="iClasse">La classe correspondant aux données qui doivent être collectées. (1==> F&B / Autre ==> Eco.)</param>
        /// <param name="sFlight">Le vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sAirline">La compagnie aérienne du vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sFlightCategory">La catégorie de vol du vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sInformation">L'information recherchée dans la table.</param>
        /// <param name="sColumnName">Nom de la colonne dans laquelle où rechercher l'information \ref sInformation</param>
        /// <returns></returns>
        internal DataRow GetInformationsRow(int iClasse, String sFlight, String sAirline, String sFlightCategory, String sInformation, String sColumnName)
        {
            ///Préparation de la variable qui sera renvoyée.
            DataRow drResult = null;
            ///Préparation du paramétrage qui sera passé aux fonctions.
            Dictionary<String, String> dssParameters = new Dictionary<string, string>();

            if (bUseException)
            {
                #region FlightExceptions
                dssParameters.Add(GlobalNames.Flight, sFlight);
                dssParameters.Add(sColumnName, sInformation);
                if ((iClasse == 1) && (sExceptionFlightFB != null))
                {
                    ///Si la classe regardée est la première et que \ref sExceptionFlightFB (les exceptions par vol et 
                    ///pour la première classe) est différent de null. Alors On tente de récupérer la ligne correspondant
                    ///aux données recherchées : Ligne avec le numéro de vol et l'information \ref sInformation dans la 
                    ///colonne \ref sColumnName. (\ref GetRow())
                    drResult = sExceptionFlightFB.GetRow(dssParameters);
                    ///Si une ligne a été récupérée, alors cette ligne est renvoyée et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                if (sExceptionFlight != null)
                {
                    ///Si \ref sExceptionFlight (les exceptions par vol ) est différent de null. Alors On tente de 
                    ///récupérer la ligne correspondant aux données recherchées : Ligne avec le numéro de vol et 
                    ///l'information \ref sInformation dans la colonne \ref sColumnName. (\ref GetRow())
                    drResult = sExceptionFlight.GetRow(dssParameters);
                    ///Si une ligne a été récupérée, alors cette ligne est renvoyée et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                #endregion

                dssParameters.Clear();
                #region Airline Exceptions

                dssParameters.Add(GlobalNames.Airline, sAirline);
                dssParameters.Add(sColumnName, sInformation);
                if ((iClasse == 1) && (sExceptionAirlineFB != null))
                {
                    ///Si la classe regardée est la première et que \ref sExceptionAirlineFB  (les exceptions par Airline 
                    ///et pour la première classe) est différent de null. Alors On tente de récupérer la ligne correspondant 
                    ///aux données recherchées : Ligne avec l'airline \ref SAirline et l'information \ref sInformation dans la 
                    ///colonne \ref sColumnName. (\ref GetRow())
                    drResult = sExceptionAirlineFB.GetRow(dssParameters);
                    ///Si une ligne a été récupérée, alors cette ligne est renvoyée et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                if (sExceptionAirline != null)
                {
                    ///Si \ref sExceptionAirline  (les exceptions par Airline) est différent de null. Alors On tente de 
                    ///récupérer la ligne correspondant aux données recherchées : Ligne avec l'airline \ref SAirline et 
                    ///l'information \ref sInformation dans la colonne \ref sColumnName. (\ref GetRow())
                    drResult = sExceptionAirline.GetRow(dssParameters);
                    ///Si une ligne a été récupérée, alors cette ligne est renvoyée et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                #endregion

                dssParameters.Clear();
                #region FlightCategoryExceptions
                dssParameters.Add(GlobalNames.sFPD_A_Column_FlightCategory, sFlightCategory);
                dssParameters.Add(sColumnName, sInformation);
                if ((iClasse == 1) && (sExceptionFCFB != null))
                {
                    ///Si la classe regardée est la première et que \ref sExceptionFCFB (les exceptions par categories de vols
                    ///et pour la première classe) est différent de null. Alors On tente de récupérer la ligne correspondant 
                    ///aux données recherchées : Ligne avec la catégories de vol égale à \ref sFlightCategory et l'information 
                    ///\ref sInformation dans la colonne \ref sColumnName. (\ref GetRow())
                    drResult = sExceptionFCFB.GetRow(dssParameters);
                    ///Si une ligne a été récupérée, alors cette ligne est renvoyée et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                if (sExceptionFC != null)
                {
                    ///Si \ref sExceptionFC (les exceptions par categories de vols) est différent de null. Alors On tente de 
                    ///récupérer la ligne correspondant aux données recherchées : Ligne avec la catégories de vol égale à 
                    ///\ref sFlightCategory et l'information \ref sInformation dans la colonne \ref sColumnName. (\ref GetRow())
                    drResult = sExceptionFC.GetRow(dssParameters);
                    ///Si une ligne a été récupérée, alors cette ligne est renvoyée et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                #endregion

                dssParameters.Clear();
                #region First and business
                dssParameters.Add(sColumnName, sInformation);
                if ((iClasse == 1) && (sExceptionFB != null))
                {
                    ///Si \ref sExceptionFB (les exceptions pour la première classe) est différent de null. Alors On tente de 
                    ///récupérer la ligne correspondant aux données recherchées : Ligne avec l'information \ref sInformation 
                    ///dans la colonne \ref sColumnName. (\ref GetRow())
                    drResult = sExceptionFB.GetRow(dssParameters);
                    ///Si une ligne a été récupérée, alors cette ligne est renvoyée et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                #endregion
            }

            dssParameters.Clear();
            #region Normal BehaviorException
            dssParameters.Add(sColumnName, sInformation);
            ///Finalement, si aucune ligne n'a été trouvée après avoir parcouru les tables d'exceptions, on récupère
            ///alors la ligne correspondant au cas normal dans la table courante. (\ref GetRow())
            drResult = this.GetRow(dssParameters);
            #endregion

            return drResult;
        }

        /// <summary>
        /// Fonction qui permet de récupérer les lignes correspondantes à l'information recherchée. Elle appliquera en priorité les 
        /// rêgles d'exceptions mises en places pour la table courante. Si aucune des tables d'exceptions ne renvoie d'informations
        /// alors les informations contenues dans la table mère seront renvoyées. \n Attention cette fonction n'est applicable que sur 
        /// les tables ayant des exceptions par ligne (en opposition aux tables avec exceptions par colonnes.
        /// </summary>
        /// <param name="iClasse">La classe correspondant aux données qui doivent être collectées. (1==> F&B / Autre ==> Eco.)</param>
        /// <param name="sFlight">Le vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sAirline">La compagnie aérienne du vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sFlightCategory">La catégorie de vol du vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sInformation">L'information recherchée dans la table.</param>
        /// <returns></returns>
        internal List<DataRow> GetInformationsRows(int iClasse, String sFlight, String sAirline, String sFlightCategory, String sInformation)
        {
            ///Récupération de la table courante \ref Table
            DataTable dtTmp = Table;
            ///Si la table est null, alors la fonction renvoie NULL.
            if (dtTmp == null)
                return null;
            ///Si la table ne contient aucune colonne, alors la fonction renvoie NULL.
            if (dtTmp.Columns.Count == 0)
                return null;
            ///Renvoi du résultat de la fonction \ref GetInformationsRows().
            return GetInformationsRows(iClasse, sFlight, sAirline, sFlightCategory, sInformation, Table.Columns[0].ColumnName);
        }
        /// <summary>
        /// Fonction qui permet de récupérer les lignes correspondantes à l'information recherchée. Elle appliquera en priorité les 
        /// rêgles d'exceptions mises en places pour la table courante. Si aucune des tables d'exceptions ne renvoie d'informations
        /// alors les informations contenues dans la table mère seront renvoyées. \n Attention cette fonction n'est applicable que sur 
        /// les tables ayant des exceptions par ligne (en opposition aux tables avec exceptions par colonnes.
        /// </summary>
        /// <param name="iClasse">La classe correspondant aux données qui doivent être collectées. (1==> F&B / Autre ==> Eco.)</param>
        /// <param name="sFlight">Le vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sAirline">La compagnie aérienne du vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sFlightCategory">La catégorie de vol du vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sInformation">L'information recherchée dans la table.</param>
        /// <param name="sColumnName">Nom de la colonne dans laquelle où rechercher l'information \ref sInformation</param>
        /// <returns>La liste des lignes correspondantes à la recherche demandée par l'utilisateur.</returns>
        internal List<DataRow> GetInformationsRows(int iClasse, String sFlight, String sAirline, String sFlightCategory, String sInformation, String sColumnName)
        {///Préparation de la variable qui sera renvoyée.
            List<DataRow> drResult = null;
            ///Préparation du paramétrage qui sera passé aux fonctions.
            Dictionary<String, String> dssParameters = new Dictionary<string, string>();

            if (bUseException)
            {
                #region FlightExceptions
                dssParameters.Add(GlobalNames.Flight, sFlight);
                dssParameters.Add(sColumnName, sInformation);
                if ((iClasse == 1) && (sExceptionFlightFB != null))
                {
                    ///Si la classe regardée est la première et que \ref sExceptionFlightFB (les exceptions par vol et 
                    ///pour la première classe) est différent de null. Alors On tente de récupérer les lignes correspondantes
                    ///aux données recherchées : Ligne avec le numéro de vol et l'information \ref sInformation dans la 
                    ///colonne \ref sColumnName. (\ref GetRows())
                    drResult = sExceptionFlightFB.GetRows(dssParameters);
                    ///Si une ou plusieurs lignes ont été récupérées, alors cette(s) ligne(s) est(sont) renvoyée(s) et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                if (sExceptionFlight != null)
                {
                    ///Si \ref sExceptionFlight (les exceptions par vol ) est différent de null. Alors On tente de 
                    ///récupérer les lignes correspondantes aux données recherchées : Lignes avec le numéro de vol et 
                    ///l'information \ref sInformation dans la colonne \ref sColumnName. (\ref GetRows())
                    drResult = sExceptionFlight.GetRows(dssParameters);
                    ///Si une ou plusieurs lignes ont été récupérées, alors cette(s) ligne(s) est(sont) renvoyée(s) et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                #endregion

                dssParameters.Clear();
                #region Airline Exceptions
                dssParameters.Add("Airline", sAirline);
                dssParameters.Add(sColumnName, sInformation);
                if ((iClasse == 1) && (sExceptionAirlineFB != null))
                {
                    ///Si la classe regardée est la première et que \ref sExceptionAirlineFB  (les exceptions par Airline 
                    ///et pour la première classe) est différent de null. Alors On tente de récupérer les lignes correspondantes
                    ///aux données recherchées : Ligne avec l'airline \ref SAirline et l'information \ref sInformation dans la 
                    ///colonne \ref sColumnName. (\ref GetRows())
                    drResult = sExceptionAirlineFB.GetRows(dssParameters);
                    ///Si une ou plusieurs lignes ont été récupérées, alors cette(s) ligne(s) est(sont) renvoyée(s) et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                if (sExceptionAirline != null)
                {
                    ///Si \ref sExceptionAirline  (les exceptions par Airline) est différent de null. Alors On tente de 
                    ///récupérer les lignes correspondantes aux données recherchées : Ligne avec l'airline \ref SAirline et 
                    ///l'information \ref sInformation dans la colonne \ref sColumnName. (\ref GetRows())
                    drResult = sExceptionAirline.GetRows(dssParameters);
                    ///Si une ou plusieurs lignes ont été récupérées, alors cette(s) ligne(s) est(sont) renvoyée(s) et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                #endregion

                dssParameters.Clear();
                #region FlightCategoryExceptions
                dssParameters.Add("FlightCategory", sFlightCategory);
                dssParameters.Add(sColumnName, sInformation);
                if ((iClasse == 1) && (sExceptionFCFB != null))
                {
                    ///Si la classe regardée est la première et que \ref sExceptionFCFB (les exceptions par categories de vols
                    ///et pour la première classe) est différent de null. Alors On tente de récupérer les lignes correspondantes 
                    ///aux données recherchées : Ligne avec la catégories de vol égale à \ref sFlightCategory et l'information 
                    ///\ref sInformation dans la colonne \ref sColumnName. (\ref GetRows())
                    drResult = sExceptionFCFB.GetRows(dssParameters);
                    ///Si une ou plusieurs lignes ont été récupérées, alors cette(s) ligne(s) est(sont) renvoyée(s) et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                if (sExceptionFC != null)
                {
                    ///Si \ref sExceptionFC (les exceptions par categories de vols) est différent de null. Alors On tente de 
                    ///récupérer les lignes correspondantes aux données recherchées : Ligne avec la catégories de vol égale à 
                    ///\ref sFlightCategory et l'information \ref sInformation dans la colonne \ref sColumnName. (\ref GetRows())
                    drResult = sExceptionFC.GetRows(dssParameters);
                    ///Si une ou plusieurs lignes ont été récupérées, alors cette(s) ligne(s) est(sont) renvoyée(s) et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                #endregion

                dssParameters.Clear();
                #region First and business
                dssParameters.Add(sColumnName, sInformation);
                if ((iClasse == 1) && (sExceptionFB != null))
                {
                    ///Si \ref sExceptionFB (les exceptions pour la première classe) est différent de null. Alors On tente de 
                    ///récupérer les lignes correspondantes aux données recherchées : Ligne avec l'information \ref sInformation 
                    ///dans la colonne \ref sColumnName. (\ref GetRows())
                    drResult = sExceptionFB.GetRows(dssParameters);
                    ///Si une ou plusieurs lignes ont été récupérées, alors cette(s) ligne(s) est(sont) renvoyée(s) et la fonction s'arrète.
                    if (drResult != null)
                        return drResult;
                }
                #endregion
            }
            dssParameters.Clear();
            #region Normal BehaviorException
            dssParameters.Add(sColumnName, sInformation);
            ///Finalement, si aucune ligne n'a été trouvée après avoir parcouru les tables d'exceptions, on récupère
            ///alors les lignes correspondantes au cas normal dans la table courante. (\ref GetRows())
            drResult = this.GetRows(dssParameters);
            #endregion
            return drResult;
        }

        /// <summary>
        /// Fonction qui permet de récupérer les données correspondantes à l'information recherchée. Elle appliquera en priorité les 
        /// rêgles d'exceptions mises en places pour la table courante. Si aucune des tables d'exceptions ne renvoie d'informations
        /// alors les informations contenues dans la table mère seront renvoyées. \n Attention cette fonction n'est applicable que sur 
        /// les tables ayant des exceptions par colonne (en opposition aux tables avec exceptions par lignes).
        /// </summary>
        /// <param name="iClasse">La classe correspondant aux données qui doivent être collectées. (1==> F&B / Autre ==> Eco.)</param>
        /// <param name="sFlight">Le vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sAirline">La compagnie aérienne du vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sInformation">L'information recherchée dans la table.</param>
        /// <param name="iIndexColumn">Index de la colonne souhaitée dans la table renvoyée.</param>
        /// <returns>La table contenant les informations recherchées.</returns>
        internal DataTable GetInformationsColumns(int iClasse, String sFlight, String sAirline, String sInformation, out int iIndexColumn)
        {
            DataTable dtTmp;
            if (bUseException)
            {
                #region FlightExceptions
                if ((iClasse == 1) && (sExceptionFlightFB != null))
                {
                    //sExceptionFlightFB
                    dtTmp = sExceptionFlightFB.Table;
                    if (dtTmp != null)
                    {
                        if (dtTmp.Columns.Contains(sFlight))
                        {
                            iIndexColumn = dtTmp.Columns.IndexOf(sFlight);
                            return dtTmp;
                        }
                    }
                }
                if (sExceptionFlight != null)
                {
                    //sExceptionFlight
                    dtTmp = sExceptionFlight.Table;
                    if (dtTmp != null)
                    {
                        if (dtTmp.Columns.Contains(sFlight))
                        {
                            iIndexColumn = dtTmp.Columns.IndexOf(sFlight);
                            return dtTmp;
                        }
                    }
                }
                #endregion

                #region Airline Exceptions
                if ((iClasse == 1) && (sExceptionAirlineFB != null))
                {
                    //sExceptionAirlineFB
                    dtTmp = sExceptionAirlineFB.Table;
                    if (dtTmp != null)
                    {
                        if (dtTmp.Columns.Contains(sAirline))
                        {
                            iIndexColumn = dtTmp.Columns.IndexOf(sAirline);
                            return dtTmp;
                        }
                    }
                }
                if (sExceptionAirline != null)
                {
                    //sExceptionAirline
                    dtTmp = sExceptionAirline.Table;
                    if (dtTmp != null)
                    {
                        if (dtTmp.Columns.Contains(sAirline))
                        {
                            iIndexColumn = dtTmp.Columns.IndexOf(sAirline);
                            return dtTmp;
                        }
                    }
                }
                #endregion

                #region First and business
                if ((iClasse == 1) && (sExceptionFB != null))
                {
                    //sExceptionFB
                    dtTmp = sExceptionFB.Table;
                    if (dtTmp != null)
                    {
                        if (dtTmp.Columns.Contains(sInformation))
                        {
                            iIndexColumn = dtTmp.Columns.IndexOf(sInformation);
                            return dtTmp;
                        }
                    }
                }
                #endregion
            }
            #region Normal BehaviorException
            //this
            dtTmp = this.Table;
            if (dtTmp != null)
            {
                if (dtTmp.Columns.Contains(sInformation))
                {
                    iIndexColumn = dtTmp.Columns.IndexOf(sInformation);
                    return dtTmp;
                }
            }
            #endregion
            iIndexColumn = -1;
            return null;
        }

        /// <summary>
        /// Fonction qui permet de récupérer les données correspondantes à l'information recherchée. Elle appliquera en priorité les 
        /// rêgles d'exceptions mises en places pour la table courante. Si aucune des tables d'exceptions ne renvoie d'informations
        /// alors les informations contenues dans la table mère seront renvoyées. \n Attention cette fonction n'est applicable que sur 
        /// les tables ayant des exceptions par colonne (en opposition aux tables avec exceptions par lignes).
        /// </summary>
        /// <param name="iClasse">La classe correspondant aux données qui doivent être collectées. (1==> F&B / Autre ==> Eco.)</param>
        /// <param name="sFlight">Le vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sAirline">La compagnie aérienne du vol pour lequel on souhaite les informations de la table courante.</param>
        /// <param name="sInformation">L'information recherchée dans la table.</param>
        /// <returns></returns>
        internal Dictionary<String, String> GetInformationsColumns(int iClasse, String sFlight, String sAirline, String sInformation)
        {
            DataTable dtTmp = Table;
            if (dtTmp == null)
                return null;
            if (dtTmp.PrimaryKey == null)
                return null;
            String sPrimaryKey = "";
            ///Vérification que l'on puisse récupérer les informations concernant la clef primaire de la table. Si on ne peut pas,
            ///alors on récupère la clef primaire de la table mère de la table courante.
            if (dtTmp.PrimaryKey.Length == 1)
                sPrimaryKey = dtTmp.PrimaryKey[0].ColumnName;
            else
            {
                if (typeof(FilterTable).IsInstanceOfType(this))
                {
                    NormalTable ntTmp = ((FilterTable)this).Root;
                    dtTmp = ntTmp.Table;
                    if (dtTmp.PrimaryKey.Length == 1)
                        sPrimaryKey = dtTmp.PrimaryKey[0].ColumnName;
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            int iIndexColumn;
            dtTmp = GetInformationsColumns(iClasse, sFlight, sAirline, sInformation, out iIndexColumn);
            if ((dtTmp == null) || (iIndexColumn > dtTmp.Columns.Count))
                return null;

            Dictionary<String, String> dssResult = new Dictionary<string, string>();
            int iIndexPrimary = dtTmp.Columns.IndexOf(sPrimaryKey);
            if (iIndexPrimary == -1)
                return null;
            foreach (DataRow drRow in dtTmp.Rows)
            {
                dssResult.Add(drRow[iIndexPrimary].ToString(), drRow[iIndexColumn].ToString());
            }
            return dssResult;
        }
        // >> Task #13361 FP AutoMod Data tables V3
        internal Dictionary<String, String> GetInformationsColumnsOnlyForFBExceptions(int iClasse, String sFlight, String sAirline, String sInformation)
        {
            DataTable dtTmp = Table;
            if (dtTmp == null)
                return null;
            if (dtTmp.PrimaryKey == null)
                return null;
            String sPrimaryKey = "";
            ///Vérification que l'on puisse récupérer les informations concernant la clef primaire de la table. Si on ne peut pas,
            ///alors on récupère la clef primaire de la table mère de la table courante.
            if (dtTmp.PrimaryKey.Length == 1)
                sPrimaryKey = dtTmp.PrimaryKey[0].ColumnName;
            else
            {
                if (typeof(FilterTable).IsInstanceOfType(this))
                {
                    NormalTable ntTmp = ((FilterTable)this).Root;
                    dtTmp = ntTmp.Table;
                    if (dtTmp.PrimaryKey.Length == 1)
                        sPrimaryKey = dtTmp.PrimaryKey[0].ColumnName;
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            int iIndexColumn;
            dtTmp = GetInformationsColumnsOnlyForFBExceptions(iClasse, sFlight, sAirline, sInformation, out iIndexColumn);
            if ((dtTmp == null) || (iIndexColumn > dtTmp.Columns.Count))
                return null;

            Dictionary<String, String> dssResult = new Dictionary<string, string>();
            int iIndexPrimary = dtTmp.Columns.IndexOf(sPrimaryKey);
            if (iIndexPrimary == -1)
                return null;
            foreach (DataRow drRow in dtTmp.Rows)
            {
                dssResult.Add(drRow[iIndexPrimary].ToString(), drRow[iIndexColumn].ToString());
            }
            return dssResult;
        }
        internal DataTable GetInformationsColumnsOnlyForFBExceptions(int iClasse, String sFlight, String sAirline, String sInformation, out int iIndexColumn)
        {
            DataTable dtTmp;
            if (bUseException)
            {
                #region FlightExceptions
                if ((iClasse == 1) && (sExceptionFlightFB != null))
                {
                    //sExceptionFlightFB
                    dtTmp = sExceptionFlightFB.Table;
                    if (dtTmp != null)
                    {
                        if (dtTmp.Columns.Contains(sFlight))
                        {
                            iIndexColumn = dtTmp.Columns.IndexOf(sFlight);
                            return dtTmp;
                        }
                    }
                }/*
                else if (sExceptionFlight != null 
                    && sExceptionAirlineFB == null && sExceptionFB == null)
                {
                    //sExceptionFlight
                    dtTmp = sExceptionFlight.Table;
                    if (dtTmp != null)
                    {
                        if (dtTmp.Columns.Contains(sFlight))
                        {
                            iIndexColumn = dtTmp.Columns.IndexOf(sFlight);
                            return dtTmp;
                        }
                    }
                }*/
                #endregion

                #region Airline Exceptions
                if ((iClasse == 1) && (sExceptionAirlineFB != null))
                {
                    //sExceptionAirlineFB
                    dtTmp = sExceptionAirlineFB.Table;
                    if (dtTmp != null)
                    {
                        if (dtTmp.Columns.Contains(sAirline))
                        {
                            iIndexColumn = dtTmp.Columns.IndexOf(sAirline);
                            return dtTmp;
                        }
                    }
                }/*
                else if (sExceptionAirline != null
                         && sExceptionFB == null)
                {
                    //sExceptionAirline
                    dtTmp = sExceptionAirline.Table;
                    if (dtTmp != null)
                    {
                        if (dtTmp.Columns.Contains(sAirline))
                        {
                            iIndexColumn = dtTmp.Columns.IndexOf(sAirline);
                            return dtTmp;
                        }
                    }
                }*/
                #endregion

                #region First and business
                if ((iClasse == 1) && (sExceptionFB != null))
                {
                    //sExceptionFB
                    dtTmp = sExceptionFB.Table;
                    if (dtTmp != null)
                    {
                        if (dtTmp.Columns.Contains(sInformation))
                        {
                            iIndexColumn = dtTmp.Columns.IndexOf(sInformation);
                            return dtTmp;
                        }
                    }
                }
                #endregion
            }
            #region Normal BehaviorException
            //this
            dtTmp = this.Table;
            if (dtTmp != null)
            {
                if (dtTmp.Columns.Contains(sInformation))
                {
                    iIndexColumn = dtTmp.Columns.IndexOf(sInformation);
                    return dtTmp;
                }
            }
            #endregion
            iIndexColumn = -1;
            return null;
        }
        // << Task #13361 FP AutoMod Data tables V3

        /// <summary>
        /// Fonction pour récupérer la NormalTable correspondant à l'ExceptionType 
        /// </summary>
        /// <param name="sExceptionType">Type d'exception</param>
        /// <returns></returns>
        internal NormalTable GetExceptionTable(String sExceptionType)
        {
            if (sExceptionType.StartsWith(GlobalNames.FirstAndBusiness))
            {
                if (sExceptionType.EndsWith(GlobalNames.Flight))
                {
                    return this.ExceptionFlightFB;
                }
                else if (sExceptionType.EndsWith(GlobalNames.Airline))
                {
                    return this.ExceptionAirlineFB;
                }
                else if (sExceptionType.EndsWith(GlobalNames.FlightCategory))
                {
                    return this.ExceptionFCFB;
                }
                else
                {
                    return this.ExceptionFB;
                }
            }
            else
            {
                if (sExceptionType.EndsWith(GlobalNames.Flight))
                {
                    return this.ExceptionFlight;
                }
                else if (sExceptionType.EndsWith(GlobalNames.Airline))
                {
                    return this.ExceptionAirline;
                }
                else if (sExceptionType.EndsWith(GlobalNames.FlightCategory))
                {
                    return this.ExceptionFC;
                }
            }
            return null;
        }
        #endregion

        #region For saving and loading the table

        /// <summary>
        /// Fonction qui permet de forcer la redéfinition des clefs primaire pour la table d'exception passée en paramètre. Elle se base sur la table
        /// mère et le contenu de la table fille. Cette fonction est surtout utilisée à la réouverture, pour remettre les clefs primaires proprement.
        /// </summary>
        /// <param name="ntTmp">Table sur laquelle il faut remettre les clefs primaire.</param>
        private void SetPrimaryKey(NormalTable ntTmp)
        {
            ///Si la table est NULL, la fonction ne fait rien.
            if (ntTmp == null)
                return;
            DataTable dtTmp = null;
            if ((!ntTmp.Loaded) && (this.etfExceptionFormat == ExceptionTableFormat.Column))
            {
                //Chargement de la table avec conversion de celle ci vers le nouveau format s'il y a lieu.
                ntTmp.TableParameters = this.TableParameters;
                ntTmp.Version = vmVersion;
                dtTmp = ntTmp.Table;
                ntTmp.TableParameters = null;
                ntTmp.Version = null;
            }
            else
                ///Récupération de la table passée en paramètre.
                dtTmp = ntTmp.Table;
            if ((dtTmp == null) || (dtTmp.Columns.Count == 0))
                return;
            ///Si le format des exceptions \ref etfExceptionFormat est en colonne.
            if (etfExceptionFormat == ExceptionTableFormat.Column)
            {
                ///__ Récupération des clefs primaire pour la table mère. 
                DataColumn[] dtColumns = Table.PrimaryKey;
                DataColumn[] dtColumnsPrimary = new DataColumn[dtColumns.Length];
                ///__ Pour chacune des clefs primaire de la table mère, la fonction essaye de récupérer la colonne qui porte le même nom.

                for (int i = 0; i < dtColumns.Length; i++)
                {
                    if (dtTmp.Columns.Contains(dtColumns[i].ColumnName))
                        dtColumnsPrimary[i] = dtTmp.Columns[dtTmp.Columns.IndexOf(dtColumns[i].ColumnName)];
                    else
                    {
                        ///____ Si une des colonne pour la clef primaire de la table mère n'apparait pas dans la table d'exception, alors la clef primaire 
                        ///est fixée sur la première colonne et la fonction s'arrète.
                        ntTmp.Table.PrimaryKey = new DataColumn[] { ntTmp.Table.Columns[0] };
                        return;
                    }
                }
                ///__ La table d'exception reçoit comme clef primaire la même que pour la table mère.
                dtTmp.PrimaryKey = dtColumnsPrimary;
            }
            else
            {
                ///Si le format des exceptions \ref etfExceptionFormat est en Ligne.

                ///__ Si le nombre de colonnes de la table d'exception est différent de celui de la table mère, alors la clef primaire
                ///est fixée aux 2 premières colonnes.
                if (Table.Columns.Count < dtTmp.Columns.Count)
                    dtTmp.PrimaryKey = new DataColumn[] { dtTmp.Columns[0], dtTmp.Columns[1] };
                else
                    ///__ Sinon la clef primaire est fixée sur la première colonne uniquement.
                    dtTmp.PrimaryKey = new DataColumn[] { dtTmp.Columns[0] };
            }

            ///Finalement on met à la jour le mode de visualisation \ref VisualisationMode de la table afin d'être
            ///sur qu'au rechargement les tables d'exceptions se comportent comme les tables par défaut.
            if (this.lpParameters != null)
                ntTmp.Mode = this.lpParameters.vmDefaultVisualisationMode;
        }
        /// <summary>
        /// Fonction qui se charge d'appeler la fonction \ref SetPrimaryKey() pour chacune des tables d'exceptions présente dans la table courante.
        /// </summary>
        internal void SetPrimaryKey()
        {
            SetPrimaryKey(sExceptionFB);
            SetPrimaryKey(sExceptionFC);
            SetPrimaryKey(sExceptionFCFB);
            SetPrimaryKey(sExceptionAirline);
            SetPrimaryKey(sExceptionAirlineFB);
            SetPrimaryKey(sExceptionFlight);
            SetPrimaryKey(sExceptionFlightFB);
        }

        /// <summary>
        /// Fonction permettant de charger une table à partir du noeud XML passé en paramètre. Il initialise les données de la table avec les 
        /// informations stockées dans le XML.
        /// </summary>
        /// <param name="xeElement">Noeud XML représentant un \ref NormalTable.</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (permet de faire des modifications liées à la version de la table)</param>
        /// <param name="sRootDirectory">Chemin vers le dossier racine du projet.</param>
        /// <returns>Booléen indiquant si l'ouverture s'est bien déroulée ou non.</returns>  
        internal override bool Load(XmlElement xeElement, VersionManager vmVersion, String sRootDirectory)
        {
            ///Appel de la fonction \ref NormalTable.Load();
            base.Load(xeElement, vmVersion, sRootDirectory);

            ///Pour chaque différent type d'exception présente dans le XML, chargement de la table d'exception qui avait été sauvegardée.
            if (OverallTools.FonctionUtiles.hasNamedChild(xeElement, ExceptionFBText))
            {
                sExceptionFB = new NormalTable(xeElement[ExceptionFBText], vmVersion, sRootDirectory);
                //SetPrimaryKey(sExceptionFB);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(xeElement, ExceptionFCText))
            {
                sExceptionFC = new NormalTable(xeElement[ExceptionFCText], vmVersion, sRootDirectory);
                //SetPrimaryKey(sExceptionFC);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(xeElement, ExceptionFCFBText))
            {
                sExceptionFCFB = new NormalTable(xeElement[ExceptionFCFBText], vmVersion, sRootDirectory);
                //SetPrimaryKey(sExceptionFCFB);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(xeElement, ExceptionAirlineText))
            {
                sExceptionAirline = new NormalTable(xeElement[ExceptionAirlineText], vmVersion, sRootDirectory);
                //SetPrimaryKey(sExceptionAirline);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(xeElement, ExceptionAirlineFBText))
            {
                sExceptionAirlineFB = new NormalTable(xeElement[ExceptionAirlineFBText], vmVersion, sRootDirectory);
                //SetPrimaryKey(sExceptionAirlineFB);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(xeElement, ExceptionFlightText))
            {
                sExceptionFlight = new NormalTable(xeElement[ExceptionFlightText], vmVersion, sRootDirectory);
                //SetPrimaryKey(sExceptionFlight);
            }
            if (OverallTools.FonctionUtiles.hasNamedChild(xeElement, ExceptionFlightFBText))
            {
                sExceptionFlightFB = new NormalTable(xeElement[ExceptionFlightFBText], vmVersion, sRootDirectory);
                //SetPrimaryKey(sExceptionFlightFB);
            }
            return true;
        }

        /// <summary>
        /// Cette fonction permet de sauvegarder la table courante dans le noeud XML passé en paramètre.
        /// </summary>
        /// <param name="xeElement">Noeud Xml ou doit être sauvegardée la table courante.</param>
        /// <param name="projet">Projet XML père de tous les noeuds XML (Requis pour pouvoir ajouter un noeud XML au projet global)</param>
        /// <param name="sRootDirectory">Dossier principal d'enregistrement du projet (Dossier où le .pax sera sauvegardé).</param>
        /// <param name="sOldRootDirectory">Ancien dossier principal d'enregistrement (SaveAs : Dossier de l'ancien projet, Save : Dossier .bak2 stockant l'ancienne version.)</param>
        /// <param name="sSavingDirectory">Dossier relatif où seront stockés les tables.</param>
        /// <returns>Booléen indiquant si l'enregistrement s'est bien déroulé ou non.</returns>
        internal override bool Save(XmlElement xeElement, XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Appel de la fonction \ref NormalTable.Save()
            base.Save(xeElement, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);

            ///Pour chaque table d'exception présente dans la table courante, sauvegarde de celle ci et ajout d'un noeud 
            ///XML fils à la table courante pour indiquer le type d'exception.
            #region Sauvegarde des tables d'exceptions
            if (sExceptionFB != null)
            {
                sExceptionFB.Name = this.Name + ExceptionFBText;
                XmlElement xnSave = projet.CreateElement(ExceptionFBText);
                sExceptionFB.Save(xnSave, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
                xeElement.AppendChild(xnSave);
            }

            if (sExceptionFC != null)
            {
                sExceptionFC.Name = this.Name + ExceptionFCText;
                XmlElement xnSave = projet.CreateElement(ExceptionFCText);
                sExceptionFC.Save(xnSave, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
                xeElement.AppendChild(xnSave);
            }

            if (sExceptionFCFB != null)
            {
                sExceptionFCFB.Name = this.Name + ExceptionFCFBText;
                XmlElement xnSave = projet.CreateElement(ExceptionFCFBText);
                sExceptionFCFB.Save(xnSave, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
                xeElement.AppendChild(xnSave);
            }

            if (sExceptionAirline != null)
            {
                sExceptionAirline.Name = this.Name + ExceptionAirlineText;
                XmlElement xnSave = projet.CreateElement(ExceptionAirlineText);
                sExceptionAirline.Save(xnSave, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
                xeElement.AppendChild(xnSave);
            }

            if (sExceptionAirlineFB != null)
            {
                sExceptionAirlineFB.Name = this.Name + ExceptionAirlineFBText;
                XmlElement xnSave = projet.CreateElement(ExceptionAirlineFBText);
                sExceptionAirlineFB.Save(xnSave, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
                xeElement.AppendChild(xnSave);
            }

            if (sExceptionFlight != null)
            {
                sExceptionFlight.Name = this.Name + ExceptionFlightText;
                XmlElement xnSave = projet.CreateElement(ExceptionFlightText);
                sExceptionFlight.Save(xnSave, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
                xeElement.AppendChild(xnSave);
            }

            if (sExceptionFlightFB != null)
            {
                sExceptionFlightFB.Name = this.Name + ExceptionFlightFBText;
                XmlElement xnSave = projet.CreateElement(ExceptionFlightFBText);
                sExceptionFlightFB.Save(xnSave, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
                xeElement.AppendChild(xnSave);
            }
            #endregion

            return true;
        }
        /// <summary>
        /// Fonction qui se charge de sauvegarder la représentation de la table courante.
        /// </summary>
        /// <param name="projet">Projet XML auquel doivent être reliés tous les noeuds XML nécessaires pour représenté cette classe.</param>
        /// <param name="sRootDirectory">Noeud principal où doit être sauvegardé la table.</param>
        /// <param name="sOldRootDirectory">Ancien noeud principal où avait été sauvegardé la table. (dans le cas où la table n'a pas été chargée.</param>
        /// <param name="sSavingDirectory">Sous dossier dans lequel la table doit être engeristrée.</param>
        /// <returns>Noeud XML représentant la table et tous ses filtres associés.</returns>
        internal override XmlElement Save(XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Création d'un nouveau Noeud XML "ExceptionTable"
            XmlElement xeNormalTable = projet.CreateElement("ExceptionTable");
            ///Appel de la fonction \ref Save() sur le nouveau noeud XML.
            Save(xeNormalTable, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
            return xeNormalTable;
        }
        #endregion

        #region To update the table, or the Exceptions of the current table.
        /// <summary>
        /// Fonction qui se charge de mettre à jour le TreeView des tables pour les filtres de cette table. 
        /// </summary>
        /// <param name="sScenarioName">Nom du scénario courant auquel la table appartient.</param>
        /// <param name="tnTable">TreeNode de la table courante.</param>
        /// <param name="cmsMenuFilter">Menu déroulant devant être ajouté aux nouveaux noeuds.</param>
        internal override void UpdateFilters(String sScenarioName, TreeNode tnTable, ContextMenuStrip cmsMenuFilter, ContextMenuStrip cmsMenuException)
        {
            ///Appel de la fonction \ref NormalTable.UpdateFilters pour mettre a jour les filtres de la table.
            base.UpdateFilters(sScenarioName, tnTable, cmsMenuFilter, cmsMenuException);
            ///Appel de \ref UpdateExceptionTables pour chacune des tables d'exceptions pouvant potentiellement 
            ///être présente pour la table. Si une table d'exception avait été enregistré, alors un noeud TreeNode 
            ///sera ajouté à la racine de la table. (Les noeuds d'exception sont par définitions placés avant les filtres).
            UpdateExceptionTables(sScenarioName, tnTable, sExceptionFB, GlobalNames.FirstAndBusiness, cmsMenuException);
            UpdateExceptionTables(sScenarioName, tnTable, sExceptionAirline, GlobalNames.Airline, cmsMenuException);
            UpdateExceptionTables(sScenarioName, tnTable, sExceptionAirlineFB, GlobalNames.FirstAndBusiness + " - " + GlobalNames.Airline, cmsMenuException);
            UpdateExceptionTables(sScenarioName, tnTable, sExceptionFC, GlobalNames.FlightCategory, cmsMenuException);
            UpdateExceptionTables(sScenarioName, tnTable, sExceptionFCFB, GlobalNames.FirstAndBusiness + " - " + GlobalNames.FlightCategory, cmsMenuException);
            UpdateExceptionTables(sScenarioName, tnTable, sExceptionFlight, GlobalNames.Flight, cmsMenuException);
            UpdateExceptionTables(sScenarioName, tnTable, sExceptionFlightFB, GlobalNames.FirstAndBusiness + " - " + GlobalNames.Flight, cmsMenuException);
        }

        /// <summary>
        /// Fonction qui permet d'ajouter au noeud du treeview représentant la table courante la table d'exception qui lui est passée en
        /// paramètre.
        /// </summary>
        /// <param name="sScenarioName">Le nom du scénario auquel appartient la table courante (Pour la génération des \ref TreeViewTag)</param>
        /// <param name="tnTable">Le noeud de la table parente.</param>
        /// <param name="ntTable">La table d'exception que l'on souhaite ajouter à l'arbre.</param>
        /// <param name="sExceptionType">Le type d'exception qui est représenté par la table d'exception \ref ntTable.</param>
        private void UpdateExceptionTables(String sScenarioName, TreeNode tnTable, NormalTable ntTable, String sExceptionType, ContextMenuStrip cmsExceptionMenu)
        {
            ///Si la table est null, la fonction ne fait rien.
            if (ntTable == null)
                return;
            ///Si le noeud parent est null, la fonction ne fait rien.
            if (tnTable == null)
                return;

            ///Recherche dans les enfant du noeuds courant, de la table d'exception qui doit être ajoutée.
            TreeNode tnNoeudException = OverallTools.TreeViewFunctions.RechercherNomEnfantStrict(sExceptionType, tnTable);

            ///Si un noeud existe déjà pour cette exception, alors la fonction ne fait rien.
            if (tnNoeudException != null)
                return;
            ///Création du \ref TreeViewTag pour le noeud qui représentera la table d'exception.
            TreeViewTag Tag = null;
            if (this is IFilterTable)
            {
                Tag = TreeViewTag.getExceptionFilterNode(sScenarioName, this.Name, sExceptionType, false, true);
            }
            else
            {
                Tag = TreeViewTag.getExceptionTableNode(sScenarioName, this.Name, sExceptionType);
            }

            ///Création du noeud pour la table d'exception.
            tnNoeudException = OverallTools.TreeViewFunctions.createBranch(sExceptionType, sExceptionType, Tag, cmsExceptionMenu);

            ///Si la création du noeud s'est mal déroulée, alors la fonction ne fait rien de plus.
            if (tnNoeudException == null)
                return;
            ///Ajout du noeud créer aux enfants de la table courante. (Ajout de manière ordonnée avec les \ref ExceptionTable
            ///placées au début des enfants).
            OverallTools.TreeViewFunctions.AddSortedNode(tnTable, tnNoeudException, true, true, true);
        }
        #endregion
    }
    #endregion

    #region internal class FilterTable : ExceptionTable, IFilterTable
    /// <summary>
    /// Les filter tables sont des tables qui sont filtres d'une table principale. Cela veut dire qu'elles sont
    /// définies à partir d'une table mère (qui peut elle même être un filtre). Un filtre hérite de la classe
    /// \ref ExceptionTable car les filtres peuvent également être dotés d'exceptions.
    /// </summary>
    internal class FilterTable : ExceptionTable, IFilterTable
    {
        #region Variables
        /// <summary>
        /// La définition du filtre courant( \ref Filter).
        /// </summary>
        Filter fFilterDefinition;

        /// <summary>
        /// Le lien vers le noeud parent de ce filtre.
        /// </summary>
        NormalTable ntParent;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la classe qui permet d'initialiser les structures de données. il initialise également 
        /// le mode de visualisation avec la valeur passée en paramètre et le filtre graphique associé.
        /// </summary>
        /// <param name="FilterDefinition">La définition du filtre que l'on veut créer.</param>
        /// <param name="_Table">La table qui doit être stockée par ce système de donnée.</param>
        /// <param name="Parent">La table parente à laquelle ce filtre est liée.</param>
        /// <param name="Mode">Le mode de visualisation. (\ref VisualisationMode)</param>
        /// <param name="GraphicDefinition">La définition graphique pour cette table. (\ref GraphicFilter)</param>
        internal FilterTable(Filter FilterDefinition/*, DataTable _Table*/, NormalTable Parent, VisualisationMode Mode, GraphicFilter GraphicDefinition)
            : base(FilterDefinition.Name, Mode, GraphicDefinition)
        {
            fFilterDefinition = FilterDefinition;
            ntParent = Parent;
            dtTable = Table;
        }

        /// <summary>
        /// Constructeur qui charge la table à partir d'un noeud XML. Ce noeud XML contient l'ensemble des informations
        /// relatives au chargement de la table.
        /// </summary>
        /// <param name="xnNode"></param>
        /// <param name="Parents"></param>
        internal FilterTable(XmlElement xnNode, VersionManager vmVersion, String sRootDirectory, NormalTable Parents)
            : base(xnNode, vmVersion, sRootDirectory)
        {
            ntParent = Parents;
        }

        /// <summary>
        /// Constructeur de copie. Ce constructeur permet de dupliquer un FilterTable
        /// </summary>
        /// <param name="ftTable">Le filter table qui doit être dupliquer.</param>
        protected FilterTable(FilterTable ftTable)
            : base(ftTable)
        {
            ntParent = null;
            fFilterDefinition = new Filter(ftTable.fFilterDefinition.creerArbreXml(new XmlDocument()));
        }
        #endregion

        #region Accesseurs

        /// <summary>
        /// Obtient ou définit le filtre courant pour ce noeud.
        /// </summary>
        public Filter Definition
        {
            get
            {
                return fFilterDefinition;
            }
            set
            {
                if (fFilterDefinition != null)
                    fFilterDefinition = null;
                fFilterDefinition = value;
            }
        }

        /// <summary>
        /// Obtient la table parente de ce noeud.
        /// </summary>
        public NormalTable Parent
        {
            get
            {
                return ntParent;
            }
            set
            {
                ntParent = value;
            }
        }

        /// <summary>
        /// Get the root table for the current filter (if the parent of the current filter is a filter, then it takes the parent of the parent ...)
        /// </summary>
        public NormalTable Root
        {
            get
            {
                NormalTable ntTable = Parent;
                Type tType = ntTable.GetType();
                if (tType.GetInterface("IFilterTable") == null)
                    return ntTable;
                return ((IFilterTable)ntTable).Root;
            }
        }

        /// <summary>
        /// Calcule la table générée en appliquant ce filtre sur la table parente.
        /// </summary>
        internal override DataTable Table
        {
            get
            {
                if (Definition == null)
                    return null;

                if (Definition.copyTable)
                {
                    return base.Table;
                }
                else
                {
                    if (dtTable == null)
                        dtTable = CalcFilter();
                    return dtTable;
                }
            }
        }

        /// <summary>
        /// Renvoie un booléen indiquant si la classe courante est un filtre ou non.
        /// </summary>
        internal override bool isFilter
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region Fonctions Delete et Copy
        /// <summary>
        /// Fonction qui permet de supprimer la structure de donnée courante. Cette fonction se chargera notamment de rendre la mémoire
        /// pour les données incluses dans la structures. Elle parcourera aussi les noeuds filtres qui sont présents sur cette table
        /// et les supprimera également. \nElle retournera enfin tous les noms des filtres qui ont été supprimés.
        /// </summary>
        /// <returns>Liste des noms de tous les filtres qui ont été supprimés suite à la suppression du noeud courant.</returns>
        internal override List<String> Delete()
        {
            ///Suppression du lien entre le filtre et la table parente. (\ref NormalTable.RemoveFilter())
            ntParent.RemoveFilter(this.Name);
            ///Suppression de la définition 
            fFilterDefinition = null;
            ///Appel de la fonction de base \ref ExceptionTable.Delete()
            return base.Delete();
        }

        /// <summary>
        /// Fonction héritée de Object qui permet de copier l'élément courant.
        /// </summary>
        /// <returns>Une instance de \ref FilterTable</returns>
        internal override Object Copy()
        {
            ///Appel du constructeur de copie \ref FilterTable()
            return new FilterTable(this);
        }
        #endregion

        #region For saving and loading the table
        /// <summary>
        /// Fonction permettant de charger une table à partir du noeud XML passé en paramètre. Il initialise les données de la table avec les 
        /// informations stockées dans le XML.
        /// </summary>
        /// <param name="xeElement">Noeud XML représentant un \ref NormalTable.</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (permet de faire des modifications liées à la version de la table)</param>
        /// <param name="sRootDirectory">Chemin vers le dossier racine du projet.</param>
        /// <returns>Booléen indiquant si l'ouverture s'est bien déroulée ou non.</returns>
        internal override bool Load(XmlElement xeElement, VersionManager vmVersion, String sRootDirectory)
        {
            ///Si le noeud XML n'a pas d'enfant nommé Filter, alors il n'est pas valide, et la fonction s'arrète.
            if (!OverallTools.FonctionUtiles.hasNamedChild(xeElement, "Filter"))
                return false;
            ///Chargement de la définition du filtre avant de chargé les autres composants de cette table.
            fFilterDefinition = new Filter(xeElement["Filter"]);

            ///Appel de la fonction mère \ref ExceptionTable.Load();
            base.Load(xeElement, vmVersion, sRootDirectory);
            return true;
        }

        /// <summary>
        /// Cette fonction permet de sauvegarder la table courante dans le noeud XML passé en paramètre.
        /// </summary>
        /// <param name="xeElement">Noeud Xml ou doit être sauvegardée la table courante.</param>
        /// <param name="projet">Projet XML père de tous les noeuds XML (Requis pour pouvoir ajouter un noeud XML au projet global)</param>
        /// <param name="sRootDirectory">Dossier principal d'enregistrement du projet (Dossier où le .pax sera sauvegardé).</param>
        /// <param name="sOldRootDirectory">Ancien dossier principal d'enregistrement (SaveAs : Dossier de l'ancien projet, Save : Dossier .bak2 stockant l'ancienne version.)</param>
        /// <param name="sSavingDirectory">Dossier relatif où seront stockés les tables.</param>
        /// <returns>Booléen indiquant si l'enregistrement s'est bien déroulé ou non.</returns>
        internal override bool Save(XmlElement xeElement, XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Création du noeud XML représentant la définition du filtre (\ref Filter).
            XmlNode xnFilter = fFilterDefinition.creerArbreXml(projet);
            ///Si ce noeud XML est null, alors la fonction retourne FAUX (un problème est apparu)
            if (xnFilter == null)
                return false;

            ///Ajout du noeud XML à \ref xeElement.
            xeElement.AppendChild(xnFilter);

            ///Appel de la fonction de la classe de base \ref Save().
            base.Save(xeElement, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);

            return true;
        }

        /// <summary>
        /// Fonction qui se charge de sauvegarder la représentation de la table courante.
        /// </summary>
        /// <param name="projet">Projet XML auquel doivent être reliés tous les noeuds XML nécessaires pour représenté cette classe.</param>
        /// <param name="sRootDirectory">Noeud principal où doit être sauvegardé la table.</param>
        /// <param name="sOldRootDirectory">Ancien noeud principal où avait été sauvegardé la table. (dans le cas où la table n'a pas été chargée.</param>
        /// <param name="sSavingDirectory">Sous dossier dans lequel la table doit être engeristrée.</param>
        /// <returns>Noeud XML représentant la table et tous ses filtres associés.</returns>
        internal override XmlElement Save(XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Création d'un noeud XML qui représentera la table courante.
            XmlElement xeNormalTable = projet.CreateElement("FilterTable");
            ///Appel de la fonction \ref Save().
            Save(xeNormalTable, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
            ///Renvoie du noeud XML nouvellement créer.
            return xeNormalTable;
        }

        /// <summary>
        /// Fonction qui charge la table en mémoire. (Si le noeud XML contient une définition pour celle ci.
        /// </summary>
        /// <param name="xeElement">Noeud XML représentant le \ref NormalTable.</param>
        /// <param name="vmVersion">Version dans laquelle le projet avait été enregistré.</param>
        /// <returns>Booléen indiquant si le chargement de la table s'est bien déroulé ou non.</returns>
        internal override bool LoadTable(XmlElement xeElement, VersionManager vmVersion)
        {
            ///Si la définition du filtre (\ref fFilterDefinition)est null, alors renvoie de FAUX.
            if (fFilterDefinition == null)
                return false;
            ///Si le filtre n'est pas un filtre copie, (il doit donc être recalculé à partir de la table mère.
            ///alors la fonction initialise \ref bLoaded à VRAI et met le chemin \ref sPath à null.
            if (!fFilterDefinition.copyTable)
            {
                bLoaded = true;
                sPath = "";
                return true;
            }
            ///Sinon, la fonction appelle la fonction de la classe parente \ref LoadTable
            return base.LoadTable(xeElement, vmVersion);
        }

        /// <summary>
        /// Fonction qui se charge de sauvegarder la table courante.
        /// </summary>
        /// <param name="projet">Projet XML auquel doivent être reliés tous le noeud XML qui contiendra le chemin vers le fichier.</param>
        /// <param name="sRootDirectory">Noeud principal où doit être sauvegardé la table.</param>
        /// <param name="sOldRootDirectory">Ancien noeud principal où avait été sauvegardé la table. (dans le cas où la table n'a pas été chargée.</param>
        /// <param name="sSavingDirectory">Sous dossier dans lequel la table doit être engeristrée.</param>
        /// <returns>Noeud XML contenant le chemin d'enregistrement de la table.</returns>
        internal override XmlElement SaveTable(XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Si la définition du filtre (\ref fFilterDefinition)est null, alors renvoie de NULL.
            if (fFilterDefinition == null)
                return null;
            ///Si le filtre est un filtre copie, alors la table n'a pas besoin d'être sauvegardée, alors renvoie de NULL.
            if (!fFilterDefinition.copyTable)
                return null;
            ///Appel de la fonction de la classe parente \ref SaveTable().
            return base.SaveTable(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
        }
        #endregion

        #region To update the parent when the filter is modified (or to calc the content of the table).

        /// <summary>
        /// Fonction qui met à jour la table parente en fonction du contenu de la table courante.
        /// </summary>
        /// <returns>Booléen indiquant si la mise à jour s'est bien déroulée.</returns>
        public Boolean UpdateParent()
        {
            ///Si le filtre est un filtre copie, alors on ne modifie pas la table parente.
            if (this.Definition.copyTable)
                return true;

            ///Si le filtre est un filtre bloqué, alors on ne modifie pas la table parente.
            if (this.Definition.Blocked)
                return true;

            ///Si la table parente n'existe pas, alors on renvoie FAUX.
            if (ntParent == null)
                return false;

            ///On récupére la définition de la table mère et de la table fille. Si l'une de ces 2 tables est nulle, 
            ///alors on arrète la fonction et on renvoie FAUX.
            DataTable dtTableMere = ntParent.Table;
            if (dtTableMere == null)
                return false;

            ///Si \ref dtTable est null, alors la fonction s'arrète et renvoie FAUX.
            if (dtTable == null)
                return false;

            ///Pour chaque colonne de la clef primaire de la table courante, on essaye de trouver la colonne dans la table mère.
            ///Si une colonne de la clef primaire de la table courante n'apparait pas dans la table mère, alors la fonction
            ///s'arrète et renvoie FAUX.
            int i;
            int[] iIndexPrimaryKey = new int[dtTable.PrimaryKey.Length];
            for (i = 0; i < dtTable.PrimaryKey.Length; i++)
            {
                iIndexPrimaryKey[i] = dtTableMere.Columns.IndexOf(dtTable.PrimaryKey[i].ColumnName);
                if (iIndexPrimaryKey[i] == -1)
                    return false;
            }


            ///Pour chaque colonne de la table courante, on essaye de trouver la colonne dans la table mère.
            ///Si une colonne de la table courante n'apparait pas dans la table mère, alors la fonction
            ///s'arrète et renvoie FAUX.
            int[] iIndexColumns = new int[dtTable.Columns.Count];
            for (i = 0; i < dtTable.Columns.Count; i++)
            {
                iIndexColumns[i] = dtTableMere.Columns.IndexOf(dtTable.Columns[i].ColumnName);
                if (iIndexColumns[i] == -1)
                    return false;
            }

            DataRow lineToModify;

            ///Pour chaque ligne dans la table courante
            foreach (DataRow ligne in dtTable.Rows)
            {
                ///__ Récupération de la clef de la ligne (les valeurs contenues dans les colonnes représentant la clef primaire).
                Object[] key = new Object[dtTable.PrimaryKey.Length];
                for (i = 0; i < dtTable.PrimaryKey.Length; i++)
                {
                    key[i] = ligne[dtTable.PrimaryKey[i]];
                }
                ///__ Recherche dans la table mère de la ligne qui correspond à la clef primaire trouvée dans la table courante.
                lineToModify = null;
                foreach (DataRow ligneMother in dtTableMere.Rows)
                {
                    int nbFind = 0;
                    for (i = 0; i < dtTable.PrimaryKey.Length; i++)
                    {
                        if (ligneMother[iIndexPrimaryKey[i]].ToString() == key[i].ToString())
                        {
                            nbFind++;
                        }
                    }
                    if (nbFind == dtTable.PrimaryKey.Length)
                    {
                        lineToModify = ligneMother;
                        break;
                    }
                }
                ///__ Si aucune ligne trouvée correspondant à la clef primaire, alors la fonction passe à la ligne suivante de la table courante.
                if (lineToModify == null)
                    continue;
                ///__ Sinon la table mère est mise à jour avec les données contenues dans la table courante.
                lineToModify.BeginEdit();
                for (i = 0; i < dtTable.Columns.Count; i++)
                {
                    lineToModify[iIndexColumns[i]] = ligne[i];
                }
            }

            dtTableMere.AcceptChanges();
            ///Si la table parente est également un filtre, alors appel de la fonction \ref UpdateParent() sur la table parente.
            if (ntParent.isFilter)
                return ((IFilterTable)ntParent).UpdateParent();
            return true;
        }

        /// <summary>
        /// Fonction qui calcule la table pour le filtre courant. Si le filtre courant n'est pas valide, alors elle renverra null.
        /// </summary>
        /// <returns>Table calculée ou alors NULL.</returns>
        public DataTable CalcFilter()
        {
            try
            {
                DataTable dtParent = new DataTable();
                if(ntParent!=null)
                {
                dtParent = ntParent.Table;
                }

                ///Si la table parente n'existe pas, alors la fonction renvoie NULL.
                if (dtParent == null)
                    return null;
                Filter fParent = null;
                if (ntParent is IFilterTable)
                    fParent = ((IFilterTable)ntParent).Definition;
                return Definition.applyFilter(dtParent, fParent);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error for CalcFilter method is: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        /// <summary>
        /// Fonction pour mettre à jour le blockage (éditable ou non).
        /// Utilisée pour remettre à jour la variable bBlocked de Filter.
        /// </summary>
        /// <param name="reference">Table de reference pour la verification du filtre</param>
        public override void UpdateBlocking(DataTable reference)
        {
            Filter filtreParent = null;
            if (Parent is IFilterTable)
                filtreParent = (Parent as IFilterTable).Definition;
            Definition.isBlocked(reference, filtreParent, true);
        }

        /// <summary>
        /// Function to set the VisualisationMode according to the parent table
        /// and the filter state.
        /// </summary>
        public void SetVisuMode()
        {
            // >> Task #13384 Report Tree-view            
            //if (!PAX2SIM.loadingScenarioToShowObject)
            //{
            //    if (!Root.tableIsLoaded && Root is ResultsTable)
            //    {
            //        ResultsTable resultsTable = (ResultsTable)Root;
            //        if (resultsTable != null && resultsTable.CalculatedFromTrace)
            //        {                        
            //            string warningMessage = "This is a table based on a scenario. " + Environment.NewLine
            //                + "The loading procedure may take a few minutes. Do you want to continue?";
            //            BHSCustomMessageBox customMessageBox = new BHSCustomMessageBox(warningMessage, true, null, null, null);
            //            DialogResult dr = customMessageBox.ShowDialog();
            //            if (dr == DialogResult.No || dr == DialogResult.Cancel)
            //            {
            //                PAX2SIM.stoppedLoadingScenario = true;
            //                PAX2SIM.loadingScenarioToShowObject = false;    // >> Reload scenario Question bug
            //                return;
            //            }
            //            PAX2SIM.stoppedLoadingScenario = false;
            //            PAX2SIM.loadingScenarioToShowObject = true;                        

            //            PAX2SIM.generateLocalISTForBHS = customMessageBox.generateLocalIST;
            //            PAX2SIM.generateGroupISTForBHS = customMessageBox.generateGroupIST; // >> Task #14280 Bag Trace Loading time too long
            //            PAX2SIM.generateMUPSegregationForBHS = customMessageBox.generateMUPSegregation; // >> Task #14280 Bag Trace Loading time too long
            //            PAX2SIM.copyOutputTables = customMessageBox.copyOutputTables;                                                
            //        }
            //    }
            //}
            //// << Task #13384 Report Tree-view

            if (vmMode != null)
                return;
            if ((Root.Mode == null) || (!fFilterDefinition.InheritedVisualisationMode))
            {
                vmMode = VisualisationMode.LockMode;
            }
            else
            {
                // on applique le VisualisationMode du parent
                vmMode = Root.Mode.Clone();
            }
            if (!Definition.copyTable)
                vmMode.AllowAddLine = false; // on ne peut ajouter une ligne sur un filtre

            // puis on block en modification si nécessaire
            bool toLock = false;
            if (Parent.isFilter)
            {
                IFilterTable parent = (IFilterTable)Parent;
                if (parent.Definition.copyTable)
                    toLock = Definition.isBlocked(Root.Table, parent.Definition, true);
                else
                    toLock = true;
            }
            else
            {
                toLock = Definition.isBlocked(Root.Table, null, true);
            }

            if (toLock)
            {
                vmMode.AllowAddLine = false;
                vmMode.AllowEditColumn = false;
                vmMode.AllowEditRow = false;
                vmMode.Modifiable = false;
            }
        }

        /// <summary>
        /// Fonction qui supprime la table calculé, afin de forcer un nouveau calcul.
        /// </summary>
        public void ParentTableModified()
        {
            if (Definition == null || Definition.copyTable)
                return;
            dtTable = null;
            List<String> childFilter = this.GetFilters();
            foreach (String child in childFilter)
            {
                IFilterTable filter = GetFilter(child) as IFilterTable;
                if (filter != null)
                    filter.ParentTableModified();
            }
        }
        #endregion
    }
    #endregion

    #region internal class ResultsTable : NormalTable
    /// <summary>
    /// Cette classe est utilisées pour stocker les informations sur les résultats de simulation. Cette distinction est 
    /// faite pour 2 raisons principales : \n- Ces tables n'ont pas à gérer de tables d'exceptions.\n- Ces tables peuvent 
    /// être partiellement ouvertes à l'ouverture d'un projet, et recalculées à partir d'une trace préalablement sauvée.
    /// </summary>
    internal class ResultsTable : NormalTable
    {
        #region Variables
        /// <summary>
        /// Booléen indiquant si la table représentée par l'instance de la classe courante est calculée à partir d'un fichier trace ou non.
        /// Cette information est cruciale pour déterminée si le recalcul des tables doit être lancé ou non.
        /// </summary>
        protected bool bCalculatedFromTrace;

        /// <summary>
        /// Le delégué de la fonction qui servira à ouvrir la trace (ou les traces). Cette fonction sera lancée dans le cas où une table demandée
        /// n'est pas encore chargée et que cette table est caclulée à partir de la trace (PAX ou BAG).
        /// </summary>
        protected DataManagerPaxBHS.OpenTraceDelegate otdOpenTrace;
        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur de la classe qui permet d'initialiser les structures de données. il initialise également 
        /// le mode de visualisation avec la valeur passée en paramètre et le filtre graphique associé.
        /// </summary>
        /// <param name="sName">Nom de la table qui doit être stockée par ce système de donnée.</param>
        /// <param name="Mode">Le mode de visualisation. (\ref VisualisationMode)</param>
        /// <param name="GraphicDefinition">La définition graphique pour cette table. (\ref GraphicFilter)</param>
        internal ResultsTable(String sName, VisualisationMode Mode, GraphicFilter GraphicDefinition)
            : base(sName)
        {
            vmMode = Mode;
            gfGraphicDefinition = GraphicDefinition;
            bCalculatedFromTrace = false;
            otdOpenTrace = null;
        }

        /// <summary>
        /// Constructeur de la classe qui permet d'initialiser les structures de données. il initialise également 
        /// le mode de visualisation avec la valeur passée en paramètre et le filtre graphique associé.
        /// </summary>
        /// <param name="dtTable">La table qui doit être stockée par ce système de donnée.</param>
        /// <param name="Mode">Le mode de visualisation. (\ref VisualisationMode)</param>
        /// <param name="GraphicDefinition">La définition graphique pour cette table. (\ref GraphicFilter)</param>
        internal ResultsTable(DataTable dtTable, VisualisationMode Mode, GraphicFilter GraphicDefinition)
            : base(dtTable, Mode, GraphicDefinition)
        {
            bCalculatedFromTrace = false;
            otdOpenTrace = null;
        }

        /// <summary>
        /// Constructeur qui charge la table à partir d'un noeud XML. Ce noeud XML contient l'ensemble des informations
        /// relatives au chargement de la table.
        /// </summary>
        /// <param name="xnNode"></param>
        internal ResultsTable(XmlElement xnNode, VersionManager vmVersion, String sRootDirectory)
            : base(xnNode, vmVersion, sRootDirectory)
        {
            otdOpenTrace = null;
        }

        /// <summary>
        /// Constructeur de copie pour la classe \ref ResultsTable qui permet de copier l'intégralité de la table
        /// passée en paramètre dans une nouvelle instance de \ref ResultsTable. Attention, la fonction pour le 
        /// recalcul des tables à partir de la trace n'est elle pas initialisé (\ref otdOpenTrace).
        /// </summary>
        /// <param name="rtTable"></param>
        protected ResultsTable(ResultsTable rtTable)
            : base(rtTable)
        {
            bCalculatedFromTrace = rtTable.bCalculatedFromTrace;
            otdOpenTrace = null;
        }
        /// <summary>
        /// Constructeur copie pour la classe courante. Ce constructeur permet d'initialiser une nouvelle instance
        /// de \ref ResultsTable en faisant une copie exacte de la table \ref NormalTable passée en paramètre.
        /// Ce constructeur permet notamment de transformer un \ref NormalTable en \ref ResultsTable
        /// </summary>
        /// <param name="etTable">La table d'origine qui doit être transformée.</param>
        internal ResultsTable(NormalTable etTable)
            : base(etTable)
        {
            if (etTable is ResultsTable)
            {
                bCalculatedFromTrace = ((ResultsTable)etTable).bCalculatedFromTrace;
                otdOpenTrace = null;
            }
        }
        #endregion

        #region Accesseurs
        /// <summary>
        /// Obtient la table souhaitée. Si la table est calculée à partir de la trace (\ref bCalculatedFromTrace) et qu'elle n'a pas
        /// été recalculée, alors le recalcul est lancé avant de renvoyé cette table.
        /// </summary>
        internal override DataTable Table
        {
            get
            {
                if ((!bLoaded) && bCalculatedFromTrace)
                {
                    if (otdOpenTrace != null)
                        otdOpenTrace();
                    //Appel de la fonciton qui se chargera de recalculé les informations.
                    if (!bLoaded)
                        return null;
                }
                return base.Table;
            }
        }

        /// <summary>
        /// Obtient ou définit l'information sur le fait que la table est calculée à partir de la trace ou non.
        /// </summary>
        internal Boolean CalculatedFromTrace
        {
            get
            {
                return bCalculatedFromTrace;
            }
            set
            {
                bCalculatedFromTrace = value;
            }
        }

        /// <summary>
        /// Définit la fonction qui permet de recalculer le contenu de la table à partir de la trace. (ou des traces). Cette fonction sera utilisée
        /// uniquement si \ref CalculatedFromTrace est à VRAI.
        /// </summary>
        internal DataManagerPaxBHS.OpenTraceDelegate OpenTrace
        {
            set
            {
                otdOpenTrace = value;
            }
        }
        #endregion

        #region For saving and loading the table
        /// <summary>
        /// Fonction permettant de charger une table à partir du noeud XML passé en paramètre. Il initialise les données de la table avec les 
        /// informations stockées dans le XML.
        /// </summary>
        /// <param name="xeElement">Noeud XML représentant un \ref NormalTable.</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (permet de faire des modifications liées à la version de la table)</param>
        /// <param name="sRootDirectory">Chemin vers le dossier racine du projet.</param>
        /// <returns>Booléen indiquant si l'ouverture s'est bien déroulée ou non.</returns>
        internal override bool Load(XmlElement xeElement, VersionManager vmVersion, String sRootDirectory)
        {
            ///Si le noeud XML \ref xeElement  possède un attribut nommé "FromTrace", alors \ref bCalculatedFromTrace est mis à VRAI.
            bCalculatedFromTrace = OverallTools.FonctionUtiles.hasNamedAttribute(xeElement, "FromTrace");
            ///Appel de la fonction mère \ref NormalTable.Load();
            return base.Load(xeElement, vmVersion, sRootDirectory);
        }

        /// <summary>
        /// Cette fonction permet de sauvegarder la table courante dans le noeud XML passé en paramètre.
        /// </summary>
        /// <param name="xeElement">Noeud Xml ou doit être sauvegardée la table courante.</param>
        /// <param name="projet">Projet XML père de tous les noeuds XML (Requis pour pouvoir ajouter un noeud XML au projet global)</param>
        /// <param name="sRootDirectory">Dossier principal d'enregistrement du projet (Dossier où le .pax sera sauvegardé).</param>
        /// <param name="sOldRootDirectory">Ancien dossier principal d'enregistrement (SaveAs : Dossier de l'ancien projet, Save : Dossier .bak2 stockant l'ancienne version.)</param>
        /// <param name="sSavingDirectory">Dossier relatif où seront stockés les tables.</param>
        /// <returns>Booléen indiquant si l'enregistrement s'est bien déroulé ou non.</returns>
        internal override bool Save(XmlElement xeElement, XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Si \ref bCalculatedFromTrace est à vrai, alors ajout d'un attribut "FromTrace" au noeud \ref xeElement.
            if (bCalculatedFromTrace)
                xeElement.SetAttribute("FromTrace", "True");
            ///Renvoie du résultat de l'appel à la fonction de base \ref Save.
            return base.Save(xeElement, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
        }

        /// <summary>
        /// Fonction qui se charge de sauvegarder la représentation de la table courante.
        /// </summary>
        /// <param name="projet">Projet XML auquel doivent être reliés tous les noeuds XML nécessaires pour représenté cette classe.</param>
        /// <param name="sRootDirectory">Noeud principal où doit être sauvegardé la table.</param>
        /// <param name="sOldRootDirectory">Ancien noeud principal où avait été sauvegardé la table. (dans le cas où la table n'a pas été chargée.</param>
        /// <param name="sSavingDirectory">Sous dossier dans lequel la table doit être engeristrée.</param>
        /// <returns>Noeud XML représentant la table et tous ses filtres associés.</returns>
        internal override XmlElement Save(XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Création d'un noeud XML ResultsTable.
            XmlElement xeNormalTable = projet.CreateElement("ResultsTable");
            ///Appel à la fonction \ref Save sur ce noeud XML créer
            Save(xeNormalTable, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
            ///Renvoie du noeud XML créer.
            return xeNormalTable;
        }

        /// <summary>
        /// Fonction qui charge la table en mémoire. (Si le noeud XML contient une définition pour celle ci.
        /// </summary>
        /// <param name="xeElement">Noeud XML représentant le \ref NormalTable.</param>
        /// <param name="vmVersion">Version dans laquelle le projet avait été enregistré.</param>
        /// <returns>Booléen indiquant si le chargement de la table s'est bien déroulé ou non.</returns>
        internal override bool LoadTable(XmlElement xeElement, VersionManager vmVersion)
        {
            ///Si \ref bCalculatedFromTrace est à vrai, alors la fonction initialise la table avec : \ref bLoaded = FAUX et \ref sPath = ""
            if (bCalculatedFromTrace)
            {
                bLoaded = false;
                sPath = "";
                return true;
            }
            ///Sinon elle renvoie l'appel à la fonction de la classe de base \ref LoadTable
            return base.LoadTable(xeElement, vmVersion);
        }

        /// <summary>
        /// Fonction qui se charge de sauvegarder la table courante.
        /// </summary>
        /// <param name="projet">Projet XML auquel doivent être reliés tous le noeud XML qui contiendra le chemin vers le fichier.</param>
        /// <param name="sRootDirectory">Noeud principal où doit être sauvegardé la table.</param>
        /// <param name="sOldRootDirectory">Ancien noeud principal où avait été sauvegardé la table. (dans le cas où la table n'a pas été chargée.</param>
        /// <param name="sSavingDirectory">Sous dossier dans lequel la table doit être engeristrée.</param>
        /// <returns>Noeud XML contenant le chemin d'enregistrement de la table.</returns>
        internal override XmlElement SaveTable(XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Si \ref bCalculatedFromTrace est à VRAI, alors cette fonction ne fait rien et retourne NULL.
            if (bCalculatedFromTrace)
                return null;
            ///Sinon elle renvoie l'appel à la fonction de la classe de base \ref SaveTable
            return base.SaveTable(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
        }

        #endregion

        #region Fonctions Copy
        /// <summary>
        /// Fonction héritée de Object qui permet de copier l'élément courant.
        /// </summary>
        /// <returns>Une instance de \ref ResultsTable</returns>
        internal override Object Copy()
        {
            ///Appel du constructeur de copie \ref ResultsTable avec comme paramètre this.
            return new ResultsTable(this);
        }
        #endregion
    }
    #endregion

    #region internal class ResultsFilterTable : ResultsTable, IFilterTable
    /// <summary>
    /// Cette classe est utilisée pour les filtres appliqués sur des tables placées dans des scénarios. Cette classe contient notamment
    /// un lien vers la table parente
    /// </summary>
    internal class ResultsFilterTable : ResultsTable, IFilterTable
    {
        #region Variables
        /// <summary>
        /// La définition du filtre courant( \ref Filter).
        /// </summary>
        Filter fFilterDefinition;

        /// <summary>
        /// Le lien vers le noeud parent de ce filtre.
        /// </summary>
        NormalTable ntParent;
        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur de la classe qui permet d'initialiser les structures de données. il initialise également 
        /// le mode de visualisation avec la valeur passée en paramètre et le filtre graphique associé.
        /// </summary>
        /// <param name="FilterDefinition">La définition du filtre que l'on veut créer.</param>
        /// <param name="Parent">La table parente à laquelle ce filtre est liée.</param>
        /// <param name="Mode">Le mode de visualisation. (\ref VisualisationMode)</param>
        /// <param name="GraphicDefinition">La définition graphique pour cette table. (\ref GraphicFilter)</param>
        internal ResultsFilterTable(Filter FilterDefinition, NormalTable Parent, VisualisationMode Mode, GraphicFilter GraphicDefinition)
            : base(FilterDefinition.Name, Mode, GraphicDefinition)
        {
            fFilterDefinition = FilterDefinition;
            ntParent = Parent;
            vmMode = Mode;
        }

        /// <summary>
        /// Constructeur qui charge la table à partir d'un noeud XML. Ce noeud XML contient l'ensemble des informations
        /// relatives au chargement de la table.
        /// </summary>
        /// <param name="xnNode"></param>
        /// <param name="Parents"></param>
        internal ResultsFilterTable(XmlElement xnNode, VersionManager vmVersion, String sRootDirectory, NormalTable Parents)
            : base(xnNode, vmVersion, sRootDirectory)
        {
            ntParent = Parents;
            //TODO : Mettre le code de chargement XML ici.
        }
        internal ResultsFilterTable(ResultsFilterTable rftTable)
            : base(rftTable)
        {
            ntParent = null;
            fFilterDefinition = new Filter(rftTable.fFilterDefinition.creerArbreXml(new XmlDocument()));
        }

        /// <summary>
        /// Constructeur copie pour la classe courante. Ce constructeur permet d'initialiser une nouvelle instance
        /// de \ref ResultsFilterTable en faisant une copie exacte de la table \ref NormalTable passée en paramètre.
        /// Ce constructeur permet notamment de transformer un \ref NormalTable en \ref ResultsFilterTable
        /// </summary>
        /// <param name="etTable">La table d'origine qui doit être transformée.</param>
        internal ResultsFilterTable(NormalTable etTable)
            : base(etTable)
        {
            if (etTable.isFilter)
            {
                fFilterDefinition = ((IFilterTable)etTable).Definition;
                ntParent = ((IFilterTable)etTable).Parent;
            }
        }
        #endregion

        #region Accesseurs
        /// <summary>
        /// Obtient ou définit le filtre courant pour ce noeud.
        /// </summary>
        public Filter Definition
        {
            get
            {
                return fFilterDefinition;
            }
            set
            {
                if (fFilterDefinition != null)
                    fFilterDefinition = null;
                fFilterDefinition = value;
            }
        }

        /// <summary>
        /// Obtient la table parente de ce noeud.
        /// </summary>
        public NormalTable Parent
        {
            get
            {
                return ntParent;
            }
            set
            {
                ntParent = value;
            }
        }

        /// <summary>
        /// Get the root table for the current filter (if the parent of the current filter is a filter, then it takes the parent of the parent ...)
        /// </summary>
        public NormalTable Root
        {
            get
            {
                NormalTable ntTable = Parent;
                Type tType = ntTable.GetType();
                if (tType.GetInterface("IFilterTable") == null)
                    return ntTable;
                return ((IFilterTable)ntTable).Root;
            }
        }

        /// <summary>
        /// Calcule la table générée en appliquant ce filtre sur la table parente.
        /// </summary>
        internal override DataTable Table
        {
            get
            {
                if (Definition == null)
                    return null;
                if (Definition.copyTable)
                {
                    return base.Table;
                }
                else
                {
                    if (dtTable == null)
                        dtTable = CalcFilter();
                    return dtTable;
                }
            }
        }

        /// <summary>
        /// Renvoie un booléen indiquant si la classe courante est un filtre ou non.
        /// </summary>
        internal override bool isFilter
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region Fonctions Delete et Copy
        /// <summary>
        /// Fonction qui permet de supprimer la structure de donnée courante. Cette fonction se chargera notamment de rendre la mémoire
        /// pour les données incluses dans la structures. Elle parcourera aussi les noeuds filtres qui sont présents sur cette table
        /// et les supprimera également. \nElle retournera enfin tous les noms des filtres qui ont été supprimés.
        /// </summary>
        /// <returns>Liste des noms de tous les filtres qui ont été supprimés suite à la suppression du noeud courant.</returns>
        internal override List<String> Delete()
        {
            ///Suppression du lien entre le filtre et la table parente. (\ref NormalTable.RemoveFilter())
            ntParent.RemoveFilter(this.Name);
            ///Suppression de la définition 
            fFilterDefinition = null;
            ///Appel de la fonction de base \ref ExceptionTable.Delete()
            return base.Delete();
        }

        /// <summary>
        /// Fonction héritée de Object qui permet de copier l'élément courant.
        /// </summary>
        /// <returns>Une instance de \ref FilterTable</returns>
        internal override Object Copy()
        {
            ///Appel du constructeur de copie \ref ResultsFilterTable()
            return new ResultsFilterTable(this);
        }
        #endregion

        #region For saving and loading the table
        /// <summary>
        /// Fonction permettant de charger une table à partir du noeud XML passé en paramètre. Il initialise les données de la table avec les 
        /// informations stockées dans le XML.
        /// </summary>
        /// <param name="xeElement">Noeud XML représentant un \ref NormalTable.</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (permet de faire des modifications liées à la version de la table)</param>
        /// <param name="sRootDirectory">Chemin vers le dossier racine du projet.</param>
        /// <returns>Booléen indiquant si l'ouverture s'est bien déroulée ou non.</returns>
        internal override bool Load(XmlElement xeElement, VersionManager vmVersion, String sRootDirectory)
        {

            if (!OverallTools.FonctionUtiles.hasNamedChild(xeElement, "Filter"))
                return false;
            ///Chargement de la définition du filtre avant de chargé les autres composants de cette table.
            fFilterDefinition = new Filter(xeElement["Filter"]);

            ///Appel de la fonction mère \ref ExceptionTable.Load();
            return base.Load(xeElement, vmVersion, sRootDirectory);
        }

        /// <summary>
        /// Cette fonction permet de sauvegarder la table courante dans le noeud XML passé en paramètre.
        /// </summary>
        /// <param name="xeElement">Noeud Xml ou doit être sauvegardée la table courante.</param>
        /// <param name="projet">Projet XML père de tous les noeuds XML (Requis pour pouvoir ajouter un noeud XML au projet global)</param>
        /// <param name="sRootDirectory">Dossier principal d'enregistrement du projet (Dossier où le .pax sera sauvegardé).</param>
        /// <param name="sOldRootDirectory">Ancien dossier principal d'enregistrement (SaveAs : Dossier de l'ancien projet, Save : Dossier .bak2 stockant l'ancienne version.)</param>
        /// <param name="sSavingDirectory">Dossier relatif où seront stockés les tables.</param>
        /// <returns>Booléen indiquant si l'enregistrement s'est bien déroulé ou non.</returns>
        internal override bool Save(XmlElement xeElement, XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Création du noeud XML représentant la définition du filtre (\ref Filter).
            XmlNode xnFilter = fFilterDefinition.creerArbreXml(projet);
            ///Si ce noeud XML est null, alors la fonction retourne FAUX (un problème est apparu)
            if (xnFilter == null)
                return false;
            ///Ajout du noeud XML à \ref xeElement.
            xeElement.AppendChild(xnFilter);

            ///Appel de la fonction de la classe de base \ref Save().
            return base.Save(xeElement, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
        }

        /// <summary>
        /// Fonction qui se charge de sauvegarder la représentation de la table courante.
        /// </summary>
        /// <param name="projet">Projet XML auquel doivent être reliés tous les noeuds XML nécessaires pour représenté cette classe.</param>
        /// <param name="sRootDirectory">Noeud principal où doit être sauvegardé la table.</param>
        /// <param name="sOldRootDirectory">Ancien noeud principal où avait été sauvegardé la table. (dans le cas où la table n'a pas été chargée.</param>
        /// <param name="sSavingDirectory">Sous dossier dans lequel la table doit être engeristrée.</param>
        /// <returns>Noeud XML représentant la table et tous ses filtres associés.</returns>
        internal override XmlElement Save(XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Création d'un noeud XML qui représentera la table courante.
            XmlElement xeNormalTable = projet.CreateElement("ResultsFilterTable");
            ///Appel de la fonction \ref Save().
            Save(xeNormalTable, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
            ///Renvoie du noeud XML nouvellement créer.
            return xeNormalTable;
        }

        /// <summary>
        /// Fonction qui charge la table en mémoire. (Si le noeud XML contient une définition pour celle ci.
        /// </summary>
        /// <param name="xeElement">Noeud XML représentant le \ref NormalTable.</param>
        /// <param name="vmVersion">Version dans laquelle le projet avait été enregistré.</param>
        /// <returns>Booléen indiquant si le chargement de la table s'est bien déroulé ou non.</returns>
        internal override bool LoadTable(XmlElement xeElement, VersionManager vmVersion)
        {
            ///Si la définition du filtre (\ref fFilterDefinition)est null, alors renvoie de FAUX.
            if (fFilterDefinition == null)
                return false;
            ///Si le filtre n'est pas un filtre copie, (il doit donc être recalculé à partir de la table mère.
            ///alors la fonction initialise \ref bLoaded à VRAI et met le chemin \ref sPath à null.
            if (!fFilterDefinition.copyTable)
            {
                bLoaded = true;
                sPath = "";
                return true;
            }
            ///Sinon, la fonction appelle la fonction de la classe parente \ref LoadTable
            return base.LoadTable(xeElement, vmVersion);
        }

        /// <summary>
        /// Fonction qui se charge de sauvegarder la table courante.
        /// </summary>
        /// <param name="projet">Projet XML auquel doivent être reliés tous le noeud XML qui contiendra le chemin vers le fichier.</param>
        /// <param name="sRootDirectory">Noeud principal où doit être sauvegardé la table.</param>
        /// <param name="sOldRootDirectory">Ancien noeud principal où avait été sauvegardé la table. (dans le cas où la table n'a pas été chargée.</param>
        /// <param name="sSavingDirectory">Sous dossier dans lequel la table doit être engeristrée.</param>
        /// <returns>Noeud XML contenant le chemin d'enregistrement de la table.</returns>
        internal override XmlElement SaveTable(XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Si la définition du filtre (\ref fFilterDefinition)est null, alors renvoie de NULL.
            if (fFilterDefinition == null)
                return null;
            ///Si le filtre est un filtre copie, alors la table n'a pas besoin d'être sauvegardée, alors renvoie de NULL.
            if (!fFilterDefinition.copyTable)
                return null;
            ///Appel de la fonction de la classe parente \ref SaveTable().
            return base.SaveTable(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
        }
        #endregion

        #region To update the parent when the filter is modified (or to calc the content of the table).

        /// <summary>
        /// Fonction qui met à jour la table parente en fonction du contenu de la table courante.
        /// </summary>
        /// <returns>Booléen indiquant si la mise à jour s'est bien déroulée.</returns>
        public Boolean UpdateParent()
        {
            ///Si le filtre est un filtre copie, alors on ne modifie pas la table parente.
            if (this.Definition.copyTable)
                return true;

            ///Si le filtre est un filtre bloqué, alors on ne modifie pas la table parente.
            if (this.Definition.Blocked)
                return true;

            ///Si la table parente n'existe pas, alors on renvoie FAUX.
            if (ntParent == null)
                return false;

            ///On récupére la définition de la table mère et de la table fille. Si l'une de ces 2 tables est nulle, 
            ///alors on arrète la fonction et on renvoie FAUX.
            DataTable dtTableMere = ntParent.Table;
            if (dtTableMere == null)
                return false;

            ///Si \ref dtTable est null, alors la fonction s'arrète et renvoie FAUX.
            if (dtTable == null)
                return false;

            ///Pour chaque colonne de la clef primaire de la table courante, on essaye de trouver la colonne dans la table mère.
            ///Si une colonne de la clef primaire de la table courante n'apparait pas dans la table mère, alors la fonction
            ///s'arrète et renvoie FAUX.
            int i;
            int[] iIndexPrimaryKey = new int[dtTable.PrimaryKey.Length];
            for (i = 0; i < dtTable.PrimaryKey.Length; i++)
            {
                iIndexPrimaryKey[i] = dtTableMere.Columns.IndexOf(dtTable.PrimaryKey[i].ColumnName);
                if (iIndexPrimaryKey[i] == -1)
                    return false;
            }


            ///Pour chaque colonne de la table courante, on essaye de trouver la colonne dans la table mère.
            ///Si une colonne de la table courante n'apparait pas dans la table mère, alors la fonction
            ///s'arrète et renvoie FAUX.
            int[] iIndexColumns = new int[dtTable.Columns.Count];
            for (i = 0; i < dtTable.Columns.Count; i++)
            {
                iIndexColumns[i] = dtTableMere.Columns.IndexOf(dtTable.Columns[i].ColumnName);
                if (iIndexColumns[i] == -1)
                    return false;
            }

            DataRow lineToModify;

            ///Pour chaque ligne dans la table courante
            foreach (DataRow ligne in dtTable.Rows)
            {
                ///__ Récupération de la clef de la ligne (les valeurs contenues dans les colonnes représentant la clef primaire).
                Object[] key = new Object[dtTable.PrimaryKey.Length];
                for (i = 0; i < dtTable.PrimaryKey.Length; i++)
                {
                    key[i] = ligne[dtTable.PrimaryKey[i]];
                }
                ///__ Recherche dans la table mère de la ligne qui correspond à la clef primaire trouvée dans la table courante.
                lineToModify = null;
                foreach (DataRow ligneMother in dtTableMere.Rows)
                {
                    int nbFind = 0;
                    for (i = 0; i < dtTable.PrimaryKey.Length; i++)
                    {
                        if (ligneMother[iIndexPrimaryKey[i]].ToString() == key[i].ToString())
                        {
                            nbFind++;
                        }
                    }
                    if (nbFind == dtTable.PrimaryKey.Length)
                    {
                        lineToModify = ligneMother;
                        break;
                    }
                }
                ///__ Si aucune ligne trouvée correspondant à la clef primaire, alors la fonction passe à la ligne suivante de la table courante.
                if (lineToModify == null)
                    continue;
                ///__ Sinon la table mère est mise à jour avec les données contenues dans la table courante.
                lineToModify.BeginEdit();
                for (i = 0; i < dtTable.Columns.Count; i++)
                {
                    lineToModify[iIndexColumns[i]] = ligne[i];
                }
            }

            dtTableMere.AcceptChanges();
            ///Si la table parente est également un filtre, alors appel de la fonction \ref UpdateParent() sur la table parente.
            if (ntParent.isFilter)
                return ((IFilterTable)ntParent).UpdateParent();
            return true;
        }

        /// <summary>
        /// Fonction qui calcule la table pour le filtre courant. Si le filtre courant n'est pas valide, alors elle renverra null.
        /// </summary>
        /// <returns>Table calculée ou alors NULL.</returns>
        public DataTable CalcFilter()
        {
            DataTable dtParent = ntParent.Table;
            ///Si la table parente n'existe pas, alors la fonction renvoie NULL.
            if (dtParent == null)
                return null;
            Filter fParent = null;
            if (ntParent.isFilter)
                fParent = ((IFilterTable)ntParent).Definition;
            return Definition.applyFilter(dtParent, fParent);
        }
        #endregion

        /// <summary>
        /// Function to set the VisualisationMode according to the parent table
        /// and the filter state.
        /// </summary>
        public void SetVisuMode()
        {
            if (vmMode != null)
                return;
            // on applique le VisualisationMode du parent
            vmMode = Root.Mode.Clone();
            // puis on block en modification si nécessaire
            bool toLock = false;
            if (Parent.isFilter)
            {
                IFilterTable parent = (IFilterTable)Parent;
                if (!parent.Definition.copyTable)
                {
                    toLock = Definition.isBlocked(Root.Table, parent.Definition, true);
                }
            }

            if (toLock)
            {
                vmMode.AllowAddLine = false;
                vmMode.AllowEditColumn = false;
                vmMode.AllowEditRow = false;
                vmMode.Modifiable = false;
            }
        }

        /// <summary>
        /// Fonction qui supprime la table calculé, afin de forcer un nouveau calcul.
        /// </summary>
        public void ParentTableModified()
        {
            if (Definition == null || Definition.copyTable)
                return;
            dtTable = null;
            List<String> childFilter = this.GetFilters();
            foreach (String child in childFilter)
            {
                IFilterTable filtre = GetFilter(child) as IFilterTable;
                if (filtre != null)
                    filtre.ParentTableModified();
            }
        }
    }
    #endregion

    #region internal class AutomodGraphicsTable : ResultsTable
    /// <summary>
    /// Cette classe permet de stocké les informations relatives aux graphiques automod Chargés. Ces graphiques sont en premier
    /// lieu représentés par une table. Cette classe permet
    /// </summary>
    internal class AutomodGraphicsTable : ResultsTable
    {
        #region Constructeurs

        /// <summary>
        /// Constructeur de la classe qui permet d'initialiser les structures de données. il initialise également 
        /// le mode de visualisation avec la valeur passée en paramètre et le filtre graphique associé.
        /// </summary>
        /// <param name="Table">La table qui doit être stockée par ce système de donnée.</param>
        /// <param name="GraphicDefinition">La définition graphique pour cette table. (\ref GraphicFilter)</param>
        internal AutomodGraphicsTable(DataTable Table, GraphicFilter GraphicDefinition)
            : base(Table, VisualisationMode.LockMode.Clone(), GraphicDefinition)
        {
        }

        /// <summary>
        /// Constructeur qui charge la table à partir d'un noeud XML. Ce noeud XML contient l'ensemble des informations
        /// relatives au chargement de la table.
        /// </summary>
        /// <param name="xnNode"></param>
        internal AutomodGraphicsTable(XmlElement xnNode, VersionManager vmVersion, String sRootDirectory)
            : base(xnNode, vmVersion, sRootDirectory)
        {

            //TODO : Mettre le code de chargement XML ici.
        }

        /// <summary>
        /// Constructeur de copie pour copier une instance de \ref AutomodGraphicsTable dans une nouvelle instance.
        /// </summary>
        /// <param name="agtTable"></param>
        protected AutomodGraphicsTable(AutomodGraphicsTable agtTable)
            : base(agtTable)
        {
        }
        #endregion

        #region For saving and loading the table
        /// <summary>
        /// Fonction qui se charge de sauvegarder la représentation de la table courante.
        /// </summary>
        /// <param name="projet">Projet XML auquel doivent être reliés tous les noeuds XML nécessaires pour représenté cette classe.</param>
        /// <param name="sRootDirectory">Noeud principal où doit être sauvegardé la table.</param>
        /// <param name="sOldRootDirectory">Ancien noeud principal où avait été sauvegardé la table. (dans le cas où la table n'a pas été chargée.</param>
        /// <param name="sSavingDirectory">Sous dossier dans lequel la table doit être engeristrée.</param>
        /// <returns>Noeud XML représentant la table et tous ses filtres associés.</returns>
        internal override XmlElement Save(XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory)
        {
            ///Création d'un noeud XML AutomodGraphicsTable.
            XmlElement xeNormalTable = projet.CreateElement("AutomodGraphicsTable");
            ///Appel à la fonction \ref Save sur ce noeud XML créer
            Save(xeNormalTable, projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
            ///Renvoie du noeud XML créer.
            return xeNormalTable;
        }
        #endregion

        #region Fonctions Copy et Delete
        internal override Object Copy()
        {
            ///Appel du constructeur de copie \ref AutomodGraphicsTable avec comme paramètre this.
            return new AutomodGraphicsTable(this);
        }
        internal override List<string> Delete()
        {
            return base.Delete();
        }
        #endregion
    }
    #endregion

    #region internal class UserDataTable
    /// <summary>
    /// Cette classe représente une entrée dans le dossier des données Utilisateurs. Une entrée est représentée
    /// par un nom de table, et un certain nombre de tables pouvant être utilisées. Ainsi lors du lancement d'une
    /// simulation, l'utilisateur peut alors décider quelle soustable il souhaite utiliser pour la simulation.
    /// </summary>
    internal class UserDataTable
    {
        #region Variables
        /// <summary>
        /// Ce nom représente le nom que le fichier doit porter lors de l'exportation vers le dossier de simulation.
        /// Il s'agira aussi du nom porté par le dossier où seront enregistrées toutes les représentations de cette
        /// table.
        /// </summary>
        String sMainFileName;

        /// <summary>
        /// Ce dictionnaire contiendra toutes les tables de données utilisateur correspondant à cette donnée utilisateur.
        /// </summary>
        Dictionary<String, NormalTable> dsntTables;

        /// <summary>
        /// Obtient ou définit le nom principal de cette donnée utilisateur.
        /// </summary>
        internal String MainFileName
        {
            get
            {
                return sMainFileName;
            }
            set
            {
                sMainFileName = value;
            }
        }
        #endregion

        #region Accesseurs
        /// <summary>
        /// Obtient le nombre de tables présentes dans ce UserData.
        /// </summary>
        internal Int32 Tables
        {
            get
            {
                if (dsntTables == null)
                    return 0;
                return dsntTables.Count;
            }
        }

        /// <summary>
        /// Renvoie la liste des noms des tables présentes dans ce UserData.
        /// </summary>
        internal List<String> TablesNames
        {
            get
            {
                if (dsntTables == null)
                    return null;
                return new List<string>(dsntTables.Keys);
            }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'un user Data à partir de ce nom. Initialise les différentes variables de la classe, et le nom de la table représentée.
        /// </summary>
        /// <param name="Name">Nom de la donnée utilisateur que l'on souhaite ajouter.</param>
        internal UserDataTable(String Name)
        {
            sMainFileName = Name;
            dsntTables = null;
        }

        /// <summary>
        /// Initialise un UserData à partir d'un noeud XML représentant celui ci.
        /// </summary>
        /// <param name="xnNode"></param>
        internal UserDataTable(XmlNode xnNode)
        {

            //TODO : Mettre le code de chargement XML ici.
        }
        #endregion

        #region Pour ajouter ou supprimer des tables de cette donnée utilisateur.

        /// <summary>
        /// Permet d'ajouter une table à la donnée utilisateur courante
        /// </summary>
        /// <param name="NewFilter">La nouvelle table que l'on souhaite ajouter (ou mettre à jour) </param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé ou non.</returns>
        internal bool AddTable(NormalTable NewTable)
        {
            ///Si la nouvelle table est NULL, alors on arrète la fonction et on renvoie NULL
            if (NewTable == null)
                return false;
            ///On vérifie que la table que l'on souhaite ajouter est bien une simple table, il doit être du type \ref NormalTable
            Type tType = NewTable.GetType();
            if (tType != typeof(NormalTable))
                return false;
            ///Si la liste des tables (\ref dsntTables) n'a jamais été initialisée, alors on l'initialise.
            if (dsntTables == null)
                dsntTables = new Dictionary<String, NormalTable>();
            ///Si la table est déjà existante dans la liste, alors on le met à jour, sinon on l'ajoute à la liste.
            if (dsntTables.ContainsKey(NewTable.Name))
                dsntTables[NewTable.Name] = NewTable;
            else
                dsntTables.Add(NewTable.Name, NewTable);
            ///On renvoie Vrai pour indiquer que l'ajout (ou la mise à jour) s'est bien déroulée.
            return true;
        }

        /// <summary>
        /// Fonction qui permet de supprimer une table de la liste des tables de la donnée utilisateur courante.
        /// </summary>
        /// <param name="Name">Nom de la table qui doit être supprimée.</param>
        internal void RemoveTable(String Name)
        {
            ///Si le nom est invalide ou s'il n'y a aucune table de définie sur cette table,
            ///alors on stoppe la fonction.
            if (Name == null)
                return;
            if (dsntTables == null)
                return;
            ///Si la table existe dans la liste des tables (\ref dsntTables), alors on la supprime,
            ///sinon on ne fait rien.
            if (dsntTables.ContainsKey(Name))
                dsntTables.Remove(Name);
        }

        /// <summary>
        /// Fonction qui permet de récupérer la table portant le nom passé en paramètre.
        /// </summary>
        /// <param name="Name">Nom de la table désirée</param>
        /// <returns>NULL ou alors la table qui était voulue</returns>
        internal NormalTable GetTable(String Name)
        {
            ///Si le nom est invalide ou s'il n'y a aucune table dans la donnée utilisateur,
            ///alors on stoppe la fonction et on renvoie NULL
            if (Name == null)
                return null;
            if (dsntTables == null)
                return null;
            ///Si la table existe dans la liste des tables (\ref dsntTables), alors on renvoie cette table
            ///sinon on renvoie NULL.
            if (dsntTables.ContainsKey(Name))
                return dsntTables[Name];
            return null;
        }


        /// <summary>
        /// Fonction qui permet de récupérer la table portant le nom passé en paramètre. Cette table sera 
        /// converti en une table avec des entêtes de colonnes et des colonnes typées (si les paramètres
        /// sont renseignés).
        /// </summary>
        /// <param name="Name">Nom de la table désirée</param>
        /// <returns>NULL ou alors la table qui était voulue</returns>
        internal NormalTable GetFormatedTable(String Name)
        {
            ///Si le nom est invalide ou s'il n'y a aucune table dans la donnée utilisateur,
            ///alors on stoppe la fonction et on renvoie NULL
            if (Name == null)
                return null;
            if (dsntTables == null)
                return null;
            ///Si la table n'existe pas dans la liste des tables (\ref dsntTables), alors on renvoie null
            if (!dsntTables.ContainsKey(Name))
                return null;
            DataTable dtNewTable = null;
            try
            {
                ///On récupère la table au format UserData.
                NormalTable ntTable = dsntTables[Name];
                if (ntTable == null)
                    return null;
                DataTable dtTable = ntTable.Table;
                if (dtTable == null)
                    return null;
                bool bType = false;
                if (dtTable.Rows.Count > 2)
                {
                    if (dtTable.Rows[1][0].ToString().StartsWith("System."))
                        bType = true;
                }
                dtNewTable = new DataTable(dtTable.TableName);
                for (int i = 0; i < dtTable.Columns.Count; i++)
                {
                    Type tType = typeof(String);
                    if (bType)
                        tType = Type.GetType(dtTable.Rows[1][i].ToString());
                    dtNewTable.Columns.Add(dtTable.Rows[0][i].ToString(), tType);
                }

                for (int j = 1; j < dtTable.Rows.Count; j++)
                {
                    DataRow drRow = dtNewTable.NewRow();
                    dtNewTable.Rows.Add(drRow);
                    for (int i = 0; i < dtTable.Columns.Count; i++)
                    {
                        ///On ignore la ligne des types également (si elle est utilisée).
                        if (bType && (j == 1))
                            continue;
                        dtNewTable.Rows[j - 1][i] = FonctionsType.getType(dtTable.Rows[j][i], dtNewTable.Columns[i].DataType);
                    }
                }
            }
            catch (Exception except)
            {
                OverallTools.ExternFunctions.PrintLogFile("Err00331 : Unable to convert a userData table(" + Name + ") to a normal table." + except.Message);
                return dsntTables[Name];
            }
            return new NormalTable(dtNewTable, null);
        }
        // >> Task #12393 Pax2Sim - File conversion for Athens
        const String ARRIVAL_PREFIX = "A_";
        const String DEPARTURE_PREFIX = "D_";
        internal NormalTable GetFormatedFlightPlanTable(String Name)
        {
            ///Si le nom est invalide ou s'il n'y a aucune table dans la donnée utilisateur,
            ///alors on stoppe la fonction et on renvoie NULL
            if (Name == null)
                return null;
            if (dsntTables == null)
                return null;
            ///Si la table n'existe pas dans la liste des tables (\ref dsntTables), alors on renvoie null
            if (!dsntTables.ContainsKey(Name))
                return null;
            DataTable dtNewTable = null;
            try
            {
                ///On récupère la table au format UserData.
                NormalTable ntTable = dsntTables[Name];
                if (ntTable == null)
                    return null;
                DataTable dtTable = ntTable.Table;
                if (dtTable == null)
                    return null;
                bool bType = false;
                if (dtTable.Rows.Count > 2)
                {
                    if (dtTable.Rows[1][0].ToString().StartsWith("System."))
                        bType = true;
                }

                //remove first row - the one with Flight schedule database export info                
                object[] headerExportInfoRow = dtTable.Rows[0].ItemArray;
                //dtTable.Rows.RemoveAt(0);

                dtNewTable = new DataTable(dtTable.TableName);
                String prefix = ARRIVAL_PREFIX;
                for (int i = 0; i < dtTable.Columns.Count; i++)
                {
                    Type tType = typeof(String);
                    if (bType)
                        tType = Type.GetType(dtTable.Rows[1][i].ToString());

                    String value = dtTable.Rows[1][i].ToString().Trim();
                    if (i > 0
                    && !value.Equals(AthensTools.flightPlanFileDepartureFlightNumberColumnName))
                    {
                        value = prefix + value;
                    }
                    dtNewTable.Columns.Add(value, tType);
                    if (value.Equals(AthensTools.flightPlanFileDepartureFlightNumberColumnName))
                    {
                        prefix = DEPARTURE_PREFIX;
                    }
                }

                dtNewTable.Rows.Add(headerExportInfoRow);
                dtNewTable.Namespace = headerExportInfoRow[0].ToString();

                for (int j = 1; j < dtTable.Rows.Count; j++)
                {
                    DataRow drRow = dtNewTable.NewRow();
                    dtNewTable.Rows.Add(drRow);
                    for (int i = 0; i < dtTable.Columns.Count; i++)
                    {
                        ///On ignore la ligne des types également (si elle est utilisée).
                        if (bType && (j == 1))
                            continue;
                        dtNewTable.Rows[j - 1][i] = FonctionsType.getType(dtTable.Rows[j][i], dtNewTable.Columns[i].DataType);
                    }
                }
            }
            catch (Exception except)
            {
                OverallTools.ExternFunctions.PrintLogFile("Err00331 : Unable to convert a Flight plan userData table (" + Name + ") to a normal table." + except.Message);
                return dsntTables[Name];
            }
            return new NormalTable(dtNewTable, null);
        }
        // << Task #12393 Pax2Sim - File conversion for Athens
        #endregion

        #region Fonctions pour la sauvegarde
        /// <summary>
        /// Fonction qui permet de sauvegarder la totalité du UserData représenté par l'instance de la classe. \n
        /// Elle se charge notamment de créer un dossier dans le dossier \sDirectory qui porte le nom du User Data
        /// sélectionné, et ensuite sauvegarde la totalité des tables contenues dans ce UserData dans le dossier ainsi
        /// nouvellement créer.
        /// </summary>
        /// <param name="sDirectory">Le dossier de sauvegarde des UserData.</param>
        internal void SaveUserData(String sDirectory)
        {
            ///Vérification que le dossier de la sauvegarde existe \ref sDirectory
            if (!OverallTools.ExternFunctions.CheckCreateDirectory(sDirectory))
                return;

            ///Création du dossier de sauvegarde pour la donnée utilisateur.
            String sDirectorySavingPath = sDirectory + "\\" + sMainFileName;
            if (!OverallTools.ExternFunctions.CheckCreateDirectory(sDirectorySavingPath))
                return;
            ///Pour chaque table présente dans la classe, sauvegarde de celle ci dans le dossier spécialement créer.
            foreach (String sTableName in dsntTables.Keys)
            {
                NormalTable ntTable = dsntTables[sTableName];
                OverallTools.FonctionUtiles.SaveUserData(ntTable.Table, sDirectorySavingPath);
                //sauvegarde de la note
                if (ntTable.Note != null)
                    System.IO.File.WriteAllText(sDirectorySavingPath + "\\" + ntTable.Name + ".html", ntTable.Note);
            }
        }

        /// <summary>
        /// Fonction qui se charge de sauvegarder un fichier UserData dans le dossier spécifié. Cette fonction sauvegardera le fichier
        /// spécifié par \ref TableName dans le dossier spécifié. Ceci dans le but de préparer les fichiers d'entrée pour une simulation.
        /// </summary>
        /// <param name="sDirectory">Dossier où doit être enregistré le fichier.</param>
        /// <param name="sTableName">Table qui doit être enregistrée dans le dossier sous le nom de cette donnée utilisateur.</param>
        internal void SaveUserDataForSimulation(String sDirectory, String sTableName)
        {
            ///Vérification que le dossier de la sauvegarde existe \ref sDirectory
            if (!OverallTools.ExternFunctions.CheckCreateDirectory(sDirectory))
                return;
            ///Si la table que l'utilisateur n'existe pas, alors la fonction ne fait rien.
            if (!dsntTables.ContainsKey(sTableName))
                return;
            ///Sauvegarde de la table dans le dossier spécifié.
            OverallTools.FonctionUtiles.SaveUserData(dsntTables[sTableName].Table, sMainFileName, sDirectory);
        }
        #endregion

        #region Fonction pour mettre à jour l'arborescence.
        /// <summary>
        /// Fonction qui permet de générer le TreeView associé aux données utilisateurs représentées par cette classe.
        /// Elle permet de générer la totalité de l'arbre avec les tables et d'initialiser les menus contextuels.
        /// </summary>
        /// <param name="tnScenarioNode">Noeud parent ou devra être ajouté le noeud représentant ce UserData</param>
        /// <param name="cmsUserDataMenu">Menu contextuel à ajouter aux différentes tables et noeuds de ce UserData.</param>
        internal void UpdateUserDataTree(TreeNode tnNode, ContextMenuStrip cmsUserDataMenu)
        {
            ///Si \ref dsntTables est null, la fonction ne fait rien.
            if (dsntTables == null)
                return;
            ///Pour chaque table présente dans \ref dsntTables.
            foreach (String sKey in dsntTables.Keys)
            {
                ///__ Création d'un noeud d'arbre au format Table avec comme menu contextuel \ref cmsUserDataMenu.
                TreeNode tmp = OverallTools.TreeViewFunctions.createBranch(sKey, sKey, TreeViewTag.getTableNode("Input", sKey), cmsUserDataMenu);
                ///__ si il y a une note alors l'icone est mis à jour en fonction.
                if (dsntTables[sKey] != null && dsntTables[sKey].Note != null)
                {
                    TreeViewTag tvtTag = tmp.Tag as TreeViewTag;
                    tvtTag.HasNote = true;
                    //tvtTag.ImageIndex = 
                    //tvtTag.SelectedImageIndex = 
                    tmp.ImageIndex = tvtTag.ImageIndex;
                    tmp.SelectedImageIndex = tvtTag.SelectedImageIndex;
                }
                ///__ Ajout de ce noeud aux enfants du noeud \ref tnScenarioNode de manière classé.
                OverallTools.TreeViewFunctions.AddSortedNode(tnNode, tmp);
            }
        }
        #endregion

        internal void Delete()
        {
            foreach (NormalTable dt in dsntTables.Values)
                dt.Delete();
            dsntTables.Clear();
            dsntTables = null;

            this.sMainFileName = null;
        }
    }
    #endregion
    #endregion

    #region Nouvelles classes pour la gestion des exceptions

    #region public class DataManager
    /// <summary>
    /// Nouvelle class GestionDonnées, cette classe s'occupe de regrouper toutes les données relatives à un
    /// scénario, ou aux données d'entrées. Cette classe ne peut être implémentée directement.
    /// </summary>
    public class DataManager
    {
        #region Les différentes variables de la classe (New)
        /// <summary>
        /// Le nom de ce système de données.
        /// </summary>
        protected String sName;

        /// <summary>
        /// Temporary solution - when we create a new scenario by copying an older one we store here the new name which otherwise is not accesible at the moment we copy the trace.
        /// </summary>
        public string tempName { get; set; }    // >> Task #1954_bagTrace

        /// <summary>
        /// Définition du dataset contenant toutes les tables de la base de données.
        /// </summary>
        //private DataSet _StockageTables; // pas utilisé

        /// <summary>
        /// Dictionnaire de données qui contient toutes les tables de ce \ref DataManager.
        /// </summary>
        internal Dictionary<String, NormalTable> dsntTables;

        /// <summary>
        /// Dictionnaire de données qui contient tous les filtres présents dans ce \ref DataManager. 
        /// </summary>
        internal Dictionary<String, NormalTable> dsntFilters;

        /// <summary>
        /// Liste générale qui permet de déterminer si un nom est déjà utilisé dans cette classe ou non.
        /// </summary>
        protected List<String> lsListUsedNames;

        /// <summary>
        /// Nom du système de données courant.
        /// </summary>
        public virtual String Name
        {
            get
            {
                return sName;
            }
            set
            {
                sName = value;
            }
        }

        /// <summary>
        /// Fonction délégué qui permet de récupérer le chemin courant du projet. Ce chemin est un chemin absolu qui, ajouté au chemin relatif des tables,
        /// permet de récupérer le chemin complet des fichiers utilisés dans les projets. Cette fonction est passé aux tables présentes dans ce système de 
        /// données.
        /// </summary>
        internal GetPathDelegate gpdPath;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'un système de données vierge avec simplement l'initialisation du nom et des différents dictionnaires et listes
        /// </summary>
        /// <param name="_Name">Nom que le système de données doit porter</param>
        protected DataManager(String _Name)
        {
            sName = _Name;
            //_StockageTables = new DataSet();
            dsntTables = new Dictionary<string, NormalTable>();
            dsntFilters = new Dictionary<string, NormalTable>();
            lsListUsedNames = new List<string>();
        }

        /// <summary>
        /// Constructeur d'un système données à partir d'un noeud XML.
        /// </summary>
        /// <param name="Node">Noeud représentant le système de données.</param>
        protected DataManager(XmlNode Node)
        {
            //TODO : Mettre le code de chargement XML ici.
        }

        /// <summary>
        /// Constructeur de copie pour un \ref DataManager. Permet de créer une nouvelle instance de \ref DataManager avec les informations 
        /// contenues dans le \ref dmOldDataManager
        /// </summary>
        /// <param name="dmOldDataManager">Instance de \ref DataManager qui doit être copiée.</param>
        protected DataManager(DataManager dmOldDataManager)
        {
            ///Initialisation des différentes variables de ce \ref DataManager
            sName = dmOldDataManager.Name;
            dsntTables = new Dictionary<string, NormalTable>();
            dsntFilters = new Dictionary<string, NormalTable>();
            lsListUsedNames = new List<string>();
            ///Pour chaque table contenue dans le \ref dsntTables du \ref dmOldDataManager,
            ///appel de la fonction \ref CopyTableFilters.
            foreach (String sKey in dmOldDataManager.dsntTables.Keys)
            {
                CopyTableFilters(dmOldDataManager.dsntTables[sKey], false);
            }
            ///Copie de la définition de la fonction \ref gpdPath
            gpdPath = dmOldDataManager.gpdPath;
        }

        /// <summary>
        /// Fonction qui permet de copier les tables et ses filtres associés depuis un \ref DataManager différent.
        /// </summary>
        /// <param name="ntCopy">Table qui doit être copiée.</param>
        /// <param name="bFilter">Booléen indiquant si cette table est un filtre ou non.</param>
        private NormalTable CopyTableFilters(NormalTable ntCopy, bool bFilter)
        {
            ///Copie de la table passée en paramètre.
            NormalTable ntNewTable = (NormalTable)ntCopy.Copy();
            ///Si le booléen \ref bFiltre est à VRAI, alors appel de la fonction \ref AddFilter, Sinon appel de la fonction \ref AddTable.
            if (!bFilter)
                AddTable(ntNewTable);
            else
            {
                AddFilter(ntNewTable);
            }
            ///Pour chaque filtre de la table passée en paramètre, appel de la fonction \ref CopyTableFilters.
            foreach (String sKey in ntCopy.GetFilters())
            {
                NormalTable ntTmp = ntCopy.GetFilter(sKey);
                NormalTable ntNewFilter = CopyTableFilters(ntTmp, true);
                if (ntNewFilter != null)
                {
                    ntNewTable.AddFilter(ntNewFilter);
                    ((IFilterTable)ntNewFilter).Parent = ntNewTable;
                }
            }
            return ntNewTable;
        }
        #endregion

        #region Gestion des ajouts et suppression des tables (conservation des noms présents dans le système de données)
        /// <summary>
        /// Fonction qui se base sur la liste \ref lsListUsedNames pour déterminer si un nom est déjà utiliser dans le système de données
        /// ou non.
        /// </summary>
        /// <param name="Name">Nom recherché</param>
        /// <returns>Booléen indiquant si le nom est utilisé ou pas par le système de données.</returns>
        public virtual bool Exist(String Name)
        {
            if (lsListUsedNames == null)
                return false;
            return lsListUsedNames.Contains(Name);
        }

        /// <summary>
        /// Fonction qui ajoute dans la liste \ref lsListUsedNames pour indiquer que ce nom est utiliser dans le système de données.
        /// </summary>
        /// <param name="Name">Nom à ajouter.</param>
        public virtual void AddUsedName(String Name)
        {
            ///Si le nom existe déjà, alors on ne fait rien.
            if (Exist(Name))
                return;
            ///Si la liste \ref lsListUsedNames est NULL, alors on initialise la liste.
            if (lsListUsedNames == null)
                lsListUsedNames = new List<string>();
            ///On ajoute le nomveau nom dans la liste \ref lsListUsedNames
            lsListUsedNames.Add(Name);
        }

        /// <summary>
        /// Fonction qui permet de supprimer un nom utiliser de la liste \ref lsListUsedNames
        /// </summary>
        /// <param name="Name">Nom à supprimer.</param>
        public virtual void RemoveUsedName(String Name)
        {
            ///Si le nom n'existe pas, alors on ne fait rien.
            if (!Exist(Name))
                return;
            if (lsListUsedNames == null)
                return;
            ///On supprime le nom de la liste \ref lsListUsedNames
            lsListUsedNames.Remove(Name);
        }
        #endregion

        #region Gestion des TABLES

        /// <summary>
        /// Fonction qui ajoute le \ref NormalTable passé en paramètre au dictionnaire \ref dsntTables des tables.
        /// </summary>
        /// <param name="ntTable">Table à ajouter.</param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé ou non.</returns>
        internal virtual bool AddTable(NormalTable ntTable)
        {
            if (ntTable == null)
                return false;
            ///Si la table existe déjà dans le système de donnée, alors on Renvoie FAUX. (appel de \Exist()).
            if (Exist(ntTable.Name))
                return false;
            ntTable.GetPath = new GetPathDelegate(GetPathFunction);
            ///Mise à jour de la liste des noms utilisés grace à la fonction \ref AddUsedName().
            AddUsedName(ntTable.Name);
            ///Ajout dans le dictionnaire \ref dsntTables de la table.
            dsntTables.Add(ntTable.Name, ntTable);
            return true;
        }

        /// <summary>
        /// Fonction qui se charge d'ajouter une nouvelle table au système de données. Si cette table est déjà présente
        /// dans le système de données, alors elle ne fait rien et renvoie FAUX.
        /// </summary>
        /// <param name="dtTable">Table qui doit être ajouter.</param>
        /// <param name="vmMode">Mode de représentation de la table.</param>
        /// <param name="gfGraphic">Filtre associé à la table.</param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé ou non.</returns>
        internal virtual bool AddTable(DataTable dtTable, VisualisationMode vmMode, GraphicFilter gfGraphic)
        {
            ///Si la table (ou le nom) existe déjà dans le système de donnée, alors Renvoyer NULL. (appel de \ref Exist())
            if (Exist(dtTable.TableName))
            {
                return false;
            }
            ///Créer une nouvelle représentation de \ref NormalTable avec les paramètres de la fonction.
            NormalTable nt = new NormalTable(dtTable, vmMode, gfGraphic);
            ///Ajoute de la table grace à \ref AddTable()
            return AddTable(nt);
        }

        /// <summary>
        /// Fonction qui permet l'ajout d'une table au système de données courant. Il initialise les valeurs \ref VisualisationMode et le
        /// \ref GraphicFilter avec des valeurs null ou défault.
        /// </summary>
        /// <param name="dtTable">La table à ajouter.</param>
        /// <returns>Booléen indiquant comment l'ajout s'est déroulé.</returns>
        internal virtual bool AddTable(DataTable dtTable)
        {
            return AddTable(dtTable, VisualisationMode.LockMode.Clone(), null);
        }

        /// <summary>
        /// Fonction qui permet l'ajout d'une table au système de données courant. Il initialise la valeur de \ref GraphicFilter à null.
        /// </summary>
        /// <param name="dtTable">La table à ajouter.</param>
        /// <param name="vmMode">Mode de représentation de la table.</param>
        /// <returns>Booléen indiquant comment l'ajout s'est déroulé.</returns>
        internal virtual bool AddTable(DataTable dtTable, VisualisationMode vmMode)
        {
            return AddTable(dtTable, vmMode, null);
        }

        /// <summary>
        /// Fonction qui permet l'ajout d'une table au système de données courant. Il initialise la valeur de \ref VisualisationMode à une valeur par défaut.
        /// </summary>
        /// <param name="dtTable">La table à ajouter.</param>
        /// <param name="gfGraphic">Filtre associé à la table.</param>
        /// <returns>Booléen indiquant comment l'ajout s'est déroulé.</returns>
        internal virtual bool AddTable(DataTable dtTable, GraphicFilter gfGraphic)
        {
            return AddTable(dtTable, VisualisationMode.LockMode.Clone(), gfGraphic);
        }

        /// <summary>
        /// Fonction qui permet de mettre à jour la table contenue dans le système de données avec les valeurs contenues dans la nouvelle
        /// table passée en paramètres.
        /// </summary>
        /// <param name="Table">Nom de la table à mettre à jour</param>
        /// <param name="dtTable">Table contenant les mises à jour que l'on souhaite ajouter.</param>
        /// <returns>Booléen indiquant si la mise à jour s'est bien déroulée ou non.</returns>
        internal virtual bool UpdateTable(String TableName, DataTable Table)
        {
            ///Si la table à mettre à jour n'existe pas dans le système de données, alors Renvoyer FAUX.
            if (!Exist(TableName))
                return false;

            ///On copie la table à mettre dans une nouvelle structure afin de ne pas écraser son nom.
            DataTable dtTmp = Table.Copy();

            if (dtTmp.TableName != TableName)
                dtTmp.TableName = TableName;
            ///Récupération du \ref NormalTable représentant la table à mettre à jour.
            NormalTable ntTmp = GetTable(TableName);
            ///Appel de la fonction \ref NormalTable.UpdateTable() pour mettre à jour la table.
            return ntTmp.UpdateTable(dtTmp);
        }

        /// <summary>
        /// Fonction qui permet de mettre à jour la table contenue dans le système de données avec les valeurs contenues dans la nouvelle
        /// table passée en paramètres.
        /// </summary>
        /// <param name="dtTable">Table contenant les mises à jour que l'on souhaite ajouter.</param>
        /// <returns>Booléen indiquant si la mise à jour s'est bien déroulée ou non.</returns>
        internal virtual bool UpdateTable(DataTable dtTable)
        {
            ///Appel de \ref UpdateTable()
            return UpdateTable(dtTable.TableName, dtTable);
        }

        /// <summary>
        /// Fonction qui permet de signifier que la table passée en paramètres a été modifiée.
        /// </summary>
        /// <param name="dtTable">Nom de table modifiée</param>
        /// <returns>Booléen indiquant si la mise à jour s'est bien déroulée ou non.</returns>
        internal virtual bool UpdateTable(String sNomTable)
        {
            ///Récupération de la table passée en paramètre.
            NormalTable ntTmp = GetTable(sNomTable);
            ///Si la table n'existe pas dans le système de données courant, alors la fonction renvoie FAUX.
            if (ntTmp == null)
                return false;
            ///Informe les table filtre que la table parente a été modifiée.
            List<String> filtersName = ntTmp.GetFilters();
            foreach (String filterName in filtersName)
            {
                IFilterTable filter = (IFilterTable)this.GetFilter(filterName);
                if (filter != null)
                    filter.ParentTableModified();
            }
            ///Si la table n'est pas un filtre, alors la fonction s'arrete et renvoie VRAI.
            if (!ntTmp.isFilter)
                return true;
            ///Sinon la fonction appelle la fonction de mise à jour de la table parente à partir d'un filtre \ref IFilterTable.UpdateParent().
            return ((IFilterTable)ntTmp).UpdateParent();
        }

        /// <summary>
        /// Fonction permettant la suppression d'une table du système de données.
        /// </summary>
        /// <param name="TableName">Table à supprimer</param>
        /// <returns>Booléen indiquant comment la suppression s'est déroulée.</returns>
        internal virtual bool RemoveTable(String TableName)
        {
            ///Si la table n'existe pas dans le système de données, alors Renvoyer FAUX.
            if (!Exist(TableName))
                return false;

            ///Récupération du noeud représentant cette table.
            NormalTable ntTmp = GetTable(TableName);
            ///Appel sur le noeud récupéré de la méthode \ref NormalTable.Delete() qui renverra la liste des
            ///filtres qui étaient associés à cette table et qui sont donc à supprimer.
            List<String> lsNames = ntTmp.Delete();
            ///Parcours de la liste récupérer et suppression des dictionnaires \ref dsntTables et \ref dsntFilters des occurances
            ///des noms contenus dans cette liste. (appel également de \ref RemoveUsedName()
            foreach (String sName in lsNames)
            {
                if (!Exist(sName))
                    continue;
                RemoveUsedName(sName);
                if (dsntTables.ContainsKey(sName))
                    dsntTables.Remove(sName);
                else
                    dsntFilters.Remove(sName);
            }
            ///Appel de \ref RemoveUsedName() sur la table qui vient d'être supprimée.
            RemoveUsedName(TableName);
            ///Suppression de cette table des dictionnaires \ref dsntTables et \ref dsntFilters
            if (dsntTables.ContainsKey(TableName))
                dsntTables.Remove(TableName);
            else
                dsntFilters.Remove(TableName);
            ///Renvoie de VRAI, indiquant que la suppression s'est bien déroulée.
            return true;
        }

        /// <summary>
        /// Retourne le nombre de tables présentes dans le système de données (Filtres et tables comprises).
        /// </summary>
        internal virtual Int32 TableNumber
        {
            get
            {
                return /*dsntFilters.Count +*/ dsntTables.Count;
            }
        }

        #endregion

        #region Gestion des FILTRES
        /// <summary>
        /// Fonction permettant d'ajouter un nouveau filtre à ce système de données
        /// </summary>
        /// <param name="fFilter">Filter à ajouter.</param>
        /// <returns>Booléen indiquant si l' ajout s'est bien déroulé.</returns>
        internal virtual bool AddFilter(Filter fFilter)
        {
            return AddFilter(fFilter, null, null, null);
        }

        /// <summary>
        /// Fonction permettant d'ajouter un nouveau filtre à ce système de données
        /// </summary>
        /// <param name="fFilter">Filter à ajouter.</param>
        /// <param name="dtTable">Table associée au filtre.</param>
        /// <returns>Booléen indiquant si l' ajout s'est bien déroulé.</returns>
        internal virtual bool AddFilter(Filter fFilter, DataTable dtTable)
        {
            return AddFilter(fFilter, dtTable, null, null);
        }

        /// <summary>
        /// Fonction permettant d'ajouter un nouveau filtre à ce système de données
        /// </summary>
        /// <param name="fFilter">Filter à ajouter.</param>
        /// <param name="dtTable">Table associée au filtre.</param>
        /// <param name="vmMode">Mode de visualisation associée au filtre.</param>
        /// <returns>Booléen indiquant si l' ajout s'est bien déroulé.</returns>
        internal virtual bool AddFilter(Filter fFilter, DataTable dtTable, VisualisationMode vmMode)
        {
            return AddFilter(fFilter, dtTable, vmMode, null);
        }

        /// <summary>
        /// Fonction permettant d'ajouter un nouveau filtre à ce système de données
        /// </summary>
        /// <param name="fFilter">Filter à ajouter.</param>
        /// <param name="dtTable">Table associée au filtre.</param>
        /// <param name="gfGraphic">Représentation graphique associée au filtre.</param>
        /// <returns>Booléen indiquant si l' ajout s'est bien déroulé.</returns>
        internal virtual bool AddFilter(Filter fFilter, DataTable dtTable, GraphicFilter gfGraphic)
        {
            return AddFilter(fFilter, dtTable, null, gfGraphic);
        }
        /// <summary>
        /// Fonction permettant d'ajouter un nouveau filtre à ce système de données
        /// </summary>
        /// <param name="fFilter">Filter à ajouter.</param>
        /// <param name="dtTable">Table associée au filtre.</param>
        /// <param name="vmMode">Mode de visualisation associée au filtre.</param>
        /// <param name="gfGraphic">Représentation graphique associée au filtre.</param>
        /// <returns>Booléen indiquant si l' ajout s'est bien déroulé.</returns>
        internal virtual bool AddFilter(Filter fFilter, DataTable dtTable, VisualisationMode vmMode, GraphicFilter gfGraphic)
        {
            ///Si la table mère de ce filtre n'existe pas, alors la fonction est arrétée et renvoie FAUX.
            if (!Exist(fFilter.MotherTableName))
                return false;

            ///Si un objet présent dans ce système de données porte le même nom que le filtre à ajouter , alors la fonction est arrétée et renvoie FAUX.
            if (Exist(fFilter.Name))
                return false;

            ///Récupération de la table mère avec \ref getTable().
            NormalTable ntTmp = GetTable(fFilter.MotherTableName);
            if (ntTmp == null)
                return false;

            DataManagerInput.LoadParameters lpTmp = null;
            if (ntTmp.Name.StartsWith("PKG_"))
            {
                if (ntTmp.isFilter)
                    lpTmp = DataManagerInput.getParameters(((FilterTable)ntTmp).Root.Name);
                else
                    lpTmp = DataManagerInput.getParameters(ntTmp.Name);
            }

            ///Création d'un \ref FilterTable avec les paramètres de la fonction.
            FilterTable nt = new FilterTable(fFilter, ntTmp, vmMode, gfGraphic);
            nt.TableParameters = lpTmp;
            if (dtTable != null)
            {
                nt.UpdateTable(dtTable);
            }
            else
            {
                if (fFilter.copyTable)
                {
                    nt.UpdateTable(((IFilterTable)nt).CalcFilter());
                }
            }
            ///Ajout du filtre à la table mère. (\ref NormalTable.AddFilter).
            ntTmp.AddFilter(nt);

            nt.GetPath = new GetPathDelegate(GetPathFunction);

            ///Ajout du nom du filtre à la liste des noms utilisés (\ref AddUsedName)
            AddUsedName(nt.Name);
            ///Ajout du filtre dans le dictionnaire \ref dsntFilters
            dsntFilters.Add(nt.Name, nt);
            return true;
        }

        /// <summary>
        /// Fonction qui permet d'ajouter un nouveau filtre à la liste des filtres. Ce filtre doit déjà être définit (parent assigné).
        /// </summary>
        /// <param name="ntFilter">Le filtre que l'on souhaite ajouter</param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé ou non.</returns>
        internal virtual bool AddFilter(NormalTable ntFilter)
        {
            ///Si le filtre existe déjà dans le système, alors on ne fait rien.
            if (Exist(ntFilter.Name))
                return false;
            ///On vérifie que le filtre que l'on souhaite ajouter implémente bien l'interface \ref IFilterTable
            Type tType = ntFilter.GetType();
            if (tType.GetInterface("IFilterTable") == null)
                return false;

            ///On vérifie que la table parente existe dans le système de données.
            IFilterTable iftFilter = (IFilterTable)ntFilter;
            if (!Exist(iftFilter.Definition.MotherTableName))
                return false;

            ntFilter.GetPath = new GetPathDelegate(GetPathFunction);
            ///Ajout du nom du filtre à la liste des noms utilisés (\ref AddUsedName)
            AddUsedName(ntFilter.Name);
            ///Ajout du filtre dans le dictionnaire \ref dsntFilters
            dsntFilters.Add(ntFilter.Name, ntFilter);
            return true;
        }

        /// <summary>
        /// Fonction permettant de mettre à jour la définition d'un filtre.
        /// </summary>
        /// <param name="fFilter">Nouvelle définition pour le filtre</param>
        /// <returns></returns>
        /// <returns>Booléen indiquant si l' ajout s'est bien déroulé.</returns>
        internal virtual bool UpdateFilter(Filter fFilter)
        {
            return UpdateFilter(fFilter.Name, fFilter);
        }

        /// <summary>
        /// Fonction permettant de mettre à jour la définition d'un filtre.
        /// </summary>
        /// <param name="FilterName">Nom du filtre à mettre à jour</param>
        /// <param name="fFilter">Nouvelle définition pour le filtre</param>
        /// <returns></returns>
        /// <returns>Booléen indiquant si l' ajout s'est bien déroulé.</returns>
        internal virtual bool UpdateFilter(String FilterName, Filter fFilter)
        {
            ///Si le filtre n'existe pas, alors la fonction est arrétée et renvoie FAUX.
            if (!Exist(fFilter.Name))
                return false;
            if (fFilter.Name != FilterName)
                fFilter.Name = FilterName;
            ///Récupération du filtre dans un \ref NormalTable
            NormalTable ntTmp = GetFilter(FilterName);

            ///Si le filtre récupéré dans le \ref NormalTable n'implémente pas l'interface \ref IFilterTable, alors 
            ///la fonction est arrétée et renvoie FAUX. Cela veut dire que le noeud récupéré n'est pas un filtre valide.
            if (!ntTmp.isFilter)
                return false;
            ///Mise à jour du filtre et Renvoie de VRAI pour indiquer que tout s'est bien passé.
            ((IFilterTable)ntTmp).Definition = fFilter;

            if (fFilter.copyTable)
            {
                ///Si le filtre est un filtre copie, on met à jour le contenu de la table. (\ref IFilterTable.CalcFilter())
                ntTmp.UpdateTable(((IFilterTable)ntTmp).CalcFilter());
            }
            else
            {
                ///Sinon on signal à la table parente la modification du filtre
                (ntTmp as IFilterTable).ParentTableModified();
            }

            return true;
        }

        /// <summary>
        /// Fonction permettant la suppression d'un Filtre.
        /// </summary>
        /// <param name="FilterName">Nom du filtre à supprimer.</param>
        /// <returns>Booléen indiquant si l' ajout s'est bien déroulé.</returns>
        internal virtual bool RemoveFilter(String FilterName)
        {
            ///Si le filtre n'existe pas dans le système, alors Renvoyer FAUX
            if (!Exist(FilterName))
                return false;
            NormalTable ntTmp = GetFilter(FilterName);
            if (ntTmp == null)
                return false;
            ///Appel de la fonction \ref RemoveTable(), qui se chargera de supprimer le filtre également.
            return RemoveTable(FilterName);
        }

        /// <summary>
        /// Fonction qui permet de récupérer le nom de la table Racine du filtre passé en parametre.
        /// Si le nom passé en parametre est déjà une table racine, alors la fonction renvoie NULL.
        /// Si le nom passé en parametre est le nom d'un filtre situé dans un autre filtre, alors la fonction renverra
        /// le nom de la table mère, c'est à dire elle remontera jusqu'à trouver une table qui ne soit pas un filtre.
        /// </summary>
        /// <param name="nomFiltre">Table pour laquelle on souhaite connaître la table racine.</param>
        /// <returns>Nom de la table racine, ou alors NULL.</returns>
        public String getFilterRootName(String nomFiltre)
        {
            ///Si \ref nomFiltre n'existe pas dans le système de données, la fonction renvoie NULL
            if (!Exist(nomFiltre))
                return null;

            ///Récupération du filtre, si le filtre n'existe pas, la fonction renvoie NULL.
            NormalTable ntFilter = GetFilter(nomFiltre);
            if (ntFilter == null)
                return null;
            if (!ntFilter.isFilter)
                return null;
            ///Renvoie du résultat de la propriété \ref IFilterTable.Root.Name
            return ((IFilterTable)ntFilter).Root.Name;
        }
        #endregion

        #region SETTEURS et GETTEURS

        /// <summary>
        /// Fonction permettant de mettre à jour le filtre graphique de la table passée en parametre.
        /// </summary>
        /// <param name="TableName">Le nom de la table à laquelle on veut mettre à jour sa définition de FiltreGraphique</param>
        /// <param name="Definition">La nouvelle défintion de son FiltreGraphique.</param>
        /// <returns>Booléen indiquant si la mise à jour s'est bien déroulé.</returns>
        internal virtual bool UpdateGraphique(String TableName, GraphicFilter Definition)
        {
            ///Si la table n'existe pas dans le système actuel de données. Renvoyer FAUX.
            NormalTable ntTmp = GetTable(TableName);
            if (ntTmp == null)
                return false;
            ///Mettre à jour la définition du graphique pour la table sélectionnée, et Renvoyer VRAI.
            ntTmp.GraphicDefinition = Definition;
            return true;
        }

        /// <summary>
        /// Fonction permettant de mettre à jour la note de la table passée en parametre.
        /// </summary>
        /// <param name="TableName">Le nom de la table à laquelle on veut mettre à jour sa note</param>
        /// <param name="Note">La nouvelle note</param>
        /// <returns>Booléen indiquant si la mise à jour s'est bien déroulé.</returns>
        internal virtual bool UpdateNote(String TableName, String Note)
        {
            ///Si la table n'existe pas dans le système actuel de données. Renvoyer FAUX.
            NormalTable ntTmp = GetTable(TableName);
            if (ntTmp == null)
                return false;
            ///Mettre à jour la note pour la table sélectionnée, et Renvoyer VRAI.
            ntTmp.Note = Note;
            return true;
        }

        /// <summary>
        /// Fonction permettant de mettre à jour le mode de visualisation de la table passée en parametre.
        /// </summary>
        /// <param name="TableName">Le nom de la table à laquelle on veut mettre à jour son mode de visualisatio</param>
        /// <param name="Mode">Le nouveau mode de visualisation</param>
        /// <returns>Booléen indiquant si la mise à jour s'est bien déroulé.</returns>
        internal virtual bool UpdateModeVisualisation(String TableName, VisualisationMode Mode)
        {
            ///Si la table n'existe pas dans le système actuel de données. Renvoyer FAUX.
            NormalTable ntTmp = GetTable(TableName);
            if (ntTmp == null)
                return false;
            ///Mettre à jour du mode de visualisation pour la table sélectionnée, et Renvoyer VRAI.
            ntTmp.Mode = Mode;
            return true;
        }

        /// <summary>
        /// Fonction permettant de récupérer le \ref NormalTable associé à la table passée en parametre.
        /// </summary>
        /// <param name="sTableName">Nom de la table recherchée</param>
        /// <returns>La table recherchée ou NULL si celle ci n'existe pas.</returns>
        internal virtual NormalTable GetTable(String sTableName)
        {
            ///Si la table n'existe pas dans le système actuel de données. Renvoyer NULL.
            if (!Exist(sTableName))
                return null;
            ///Renvoyer la table demandée.
            if (!dsntTables.ContainsKey(sTableName))
            {
                return GetFilter(sTableName);
            }
            else
            {
                return dsntTables[sTableName];
            }
        }

        /// <summary>
        /// Fonction permettant de récupérer le \ref NormalTable associé au filtre passé en parametre.
        /// </summary>
        /// <param name="sTableName">Nom du filtre recherché</param>
        /// <returns>Le filtre recherché ou NULL si celui ci n'existe pas.</returns>
        internal virtual NormalTable GetFilter(String FilterName)
        {
            ///Si le filtre n'existe pas dans le système actuel de données. Renvoyer NULL.
            if (!dsntFilters.ContainsKey(FilterName))
                return null;
            ///Renvoyer le filtre demandé.
            return dsntFilters[FilterName];

        }

        /// <summary>
        /// Obtient ou définit la fonction qui permet de récupérer le chemin absolu du projet qui est actuellement ouvert. (\ref gpdPath)
        /// </summary>
        internal DataManager.GetPathDelegate GetPath
        {
            get
            {
                return gpdPath;
            }
            set
            {
                gpdPath = value;
            }
        }
        #endregion

        #region Fonctions pour ouvrir et fermer le DataManager
        /// <summary>
        /// Fonction pour sauver le \ref DataManager courant sous forme XML.
        /// </summary>
        /// <param name="projet"></param>
        /// <param name="chForm">La fenêtre de sauvegarde pour pouvoir faire progresser l'état de la sauvegarde.</param>
        /// <param name="xnNode">Noeud auquel ajouter les informations de la classe.</param>
        internal virtual void Save(XmlDocument projet, XmlElement xnNode, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory, Prompt.SIM_LoadingForm chForm)
        {
            ///Appel de la fonction \ref Save
            XmlElement arbreFiltres = Save(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory, chForm);
            ///Ajout au noeux XML xnNode du résultat de la fonction \ref Save
            xnNode.AppendChild(arbreFiltres);
        }

        /// <summary>
        /// Fonction qui s'occupe de générer un noeud et de sauvegarder la totalité des tables présentes dans le système de données.
        /// </summary>
        /// <param name="projet"></param>
        /// <param name="chForm">La fenêtre de sauvegarde pour pouvoir faire progresser l'état de la sauvegarde.</param>
        /// <returns></returns>
        internal virtual XmlElement Save(XmlDocument projet, String sRootDirectory, String sOldRootDirectory, String sSavingDirectory, Prompt.SIM_LoadingForm chForm)
        {
            ///Création d'un noeud XML DataManager qui représentera le système de données courant.
            XmlElement arbreFiltres = projet.CreateElement("DataManager");

            ///Ajout d'un attribut au noeud XML avec le nom du système de données courant.
            arbreFiltres.SetAttribute("Name", sName);
            ///Pour chaque table présente dans le système de données
            foreach (String sTableName in dsntTables.Keys)
            {
                if (chForm != null)
                {
                    chForm.ChargementFichier("Saving (" + Name + ") table " + sTableName);
                }
                ///__ Appel de la fonction \ref NormalTable.Save().
                NormalTable ntTmp = dsntTables[sTableName];
                XmlElement xnTmp = ntTmp.Save(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
                ///__ Ajout du résultat de cette fonction au noeud XML représentant le système de données.
                if (xnTmp != null)
                    arbreFiltres.AppendChild(xnTmp);
            }
            ///Renvoie du noeud XML représentant le DataManager.
            return arbreFiltres;
        }

        /// <summary>
        /// Fonction qui permet de chargement des données à partir d'un noeud XML.
        /// </summary>
        /// <param name="xmlElement">Noeud XML représentant le \ref DataManager</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (afin de gérer les modifications dans les informations stockées).</param>
        /// <param name="sRootDirectory">Le dossier où se trouvent le fichier .pax (dossier racine du projet).</param>
        /// <param name="chForm">La fenêtre de chargement pour pouvoir faire progresser l'état du chargement.</param>
        /// <returns>Booléen indiquant si l'ouverture s'est bien déroulée ou non.</returns>
        internal virtual bool LoadInputData(XmlElement xmlElement, VersionManager vmVersion, String sRootDirectory, Prompt.SIM_LoadingForm chForm)
        {
            ///Si le noeud XML possède un attribut Name, alors \ref sName est initialisé avec la valeur contenue dans cet attribut.
            if (OverallTools.FonctionUtiles.hasNamedAttribute(xmlElement, "Name"))
                sName = xmlElement.Attributes["Name"].Value;
            bool bReturn = true;
            ///Pour chaque noeud xml contenu dans le noeud courant, appel de la fonction \ref LoadInputData
            foreach (XmlElement xnTmp in xmlElement.ChildNodes)
            {
                if (chForm != null)
                {
                    if (xnTmp.HasAttribute("Name"))
                        chForm.ChargementFichier("Loading (" + this.Name + ") table " + xnTmp.Attributes["Name"].InnerText);
                    else
                        chForm.ChargementFichier("Loading tables");
                }
                bReturn = LoadInputData(null, xnTmp, vmVersion, sRootDirectory);
                if (!bReturn)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Fonction qui charge une table dans le système de données à partir de sa représentation XML.
        /// </summary>
        /// <param name="ntParent">Parent de la table (ou filtre) qui doit être chargé, NULL, lorsque la table n'a pas de parent.</param>
        /// <param name="xmlElement">Représentation de la table en XML.</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (afin de gérer les modifications dans les informations stockées).</param>
        /// <param name="sRootDirectory">Le dossier où se trouvent le fichier .pax (dossier racine du projet).</param>
        /// <returns>Booléen indiquant si l'ouverture s'est bien déroulée ou non.</returns>
        private bool LoadInputData(NormalTable ntParent, XmlElement xmlElement, VersionManager vmVersion, String sRootDirectory)
        {
            ///Suivant le type d'élement représenté par le noeud XML.
            NormalTable ntTmp = null;
            switch (xmlElement.Name)
            {
                case "NormalTable":
                    ///case "NormalTable":
                    ///__ Création d'un \ref NormalTable 
                    ntTmp = new NormalTable(xmlElement, vmVersion, sRootDirectory);
                    ///Ajout des  \ref NormalTable.TableParameters avec la fonction \ref DataManagerInput.getParameters.
                    ntTmp.TableParameters = DataManagerInput.getParameters(ntTmp.Name);
                    break;
                case "ExceptionTable":
                    ///case "ExceptionTable":
                    ///__ Création d'un \ref ExceptionTable 
                    ntTmp = new ExceptionTable(xmlElement, vmVersion, sRootDirectory);
                    ///Ajout des  \ref NormalTable.TableParameters avec la fonction \ref DataManagerInput.getParameters.
                    ntTmp.TableParameters = DataManagerInput.getParameters(ntTmp.Name);
                    break;
                case "FilterTable":
                    ///case "FilterTable":
                    ///__ Création d'un \ref FilterTable 
                    ntTmp = new FilterTable(xmlElement, vmVersion, sRootDirectory, ntParent);
                    if (ntTmp.Name.StartsWith("PKG_"))
                    {
                        //DataManagerInput.LoadParameters lpForDistributionTable = DataManagerInput.getParameters(((FilterTable)ntTmp).Root.Name);
                        ntTmp.TableParameters = DataManagerInput.getParameters(((FilterTable)ntTmp).Root.Name);
                    }

                    break;
                case "ResultsTable":
                    ///case "ResultsTable":
                    ///__ Création d'un \ref ResultsTable 
                    ntTmp = new ResultsTable(xmlElement, vmVersion, sRootDirectory);
                    ///Ajout des  \ref NormalTable.TableParameters avec la fonction \ref DataManagerInput.getParameters.
                    DataManagerInput.LoadParameters lpTmp = DataManagerInput.getParameters(ntTmp.Name);
                    if (lpTmp != null)
                    {
                        if (ntTmp.Mode == null)
                            ntTmp.Mode = lpTmp.vmDefaultVisualisationMode;
                    }
                    if (ntTmp.Mode == null)
                        ntTmp.Mode = Get_VisualisationMode(ntTmp, lpTmp);
                    break;
                case "AutomodGraphicsTable":
                    ///case "AutomodGraphicsTable":
                    ///__ Création d'un \ref AutomodGraphicsTable 
                    ntTmp = new AutomodGraphicsTable(xmlElement, vmVersion, sRootDirectory);
                    break;
                case "ResultsFilterTable":
                    ///case "ResultsFilterTable":
                    ///__ Création d'un \ref ResultsFilterTable 
                    ntTmp = new ResultsFilterTable(xmlElement, vmVersion, sRootDirectory, ntParent);
                    break;
                default:
                    break;
            }
            ///Si la table chargée est null (un problème est survenue lors du chargement ) Renvoie de FAUX.
            if (ntTmp == null)
                return false;
            ntTmp.Version = vmVersion;
            ///Si la table parente était null, alors utilisation de \ref AddTable pour ajouter la table nouvellement créer.
            if (ntParent == null)
            {
                ///__ Si l'ajout ne se passe bien, Renvoie de FAUX.
                if (!AddTable(ntTmp))
                    return false;
            }
            else
            {
                ///Si la table parente n'est pas null, alors le filtre est ajouté à la liste des filtes de la table 
                if (!ntParent.AddFilter(ntTmp))
                    return false;
                ///__ Le filtre est également ajouté à la liste des filtres du système de données avec \ref AddFilter
                if (!AddFilter(ntTmp))
                    return false;
                ///Ajout des propriété de la table parente si le filtre courant est appliqué à une table ayant des parametres $
                ///de visualisation et que le filtre courant est une table valide pour la simulation.
                List<String> lsValid = ((FilterTable)ntTmp).Root.getValidTables(false);
                if (lsValid.Contains(ntTmp.Name))
                {
                    ntTmp.TableParameters = DataManagerInput.getParameters(((FilterTable)ntTmp).Root.Name);
                }
            }
            ///Si la table chargée est une table d'exception (\ref ExceptionTable) alors appel de la fonction \ref ExceptionTable.SetPrimaryKey()
            ///pour fixer les clefs primaire pour les tables d'exceptions.
            if (ntTmp is ExceptionTable)
            {
                ExceptionTable etTable = (ExceptionTable)ntTmp;
                etTable.SetPrimaryKey();
            }

            ///Si le noeud courant a un noeud Filters, alors il faut charger tous ses filtres à l'aide de la fonction \ref LoadInputData().
            if (OverallTools.FonctionUtiles.hasNamedChild(xmlElement, "Filters"))
            {
                foreach (XmlElement xnFilter in xmlElement["Filters"].ChildNodes)
                {
                    if (!LoadInputData(ntTmp, xnFilter, vmVersion, sRootDirectory))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Fonction pour obtenir le mode de visualisation des tables "utilization"
        /// </summary>
        private VisualisationMode Get_VisualisationMode(NormalTable ntTable, DataManagerInput.LoadParameters lpTmp)
        {
            if (ntTable.Name.IndexOf("Utilization") != -1)
            {
                if (ntTable.Name.IndexOf("Group") != -1)
                {
                    return GestionDonneesHUB2SIM.UtilizationGroupTableVisualisationMode;
                }
                else
                {
                    return GestionDonneesHUB2SIM.UtilizationTableVisualisationMode;
                }
            }

            return ntTable.Mode; ;
        }

        /// <summary>
        /// Fonction qui permet de mettre à jour les filtres dans l'arborescence. Cela permet d'ajouter les filtres aux tables qui en
        /// possèdent.
        /// </summary>
        /// <param name="treeNode">Noeud racine des données contenus dans le système de données courant.</param>
        /// <param name="cmsMenuFilter">Menu contextuel qui doit être ajouté aux filtes ainsi ajoutés.</param>
        internal virtual void UpdateFilters(System.Windows.Forms.TreeNode treeNode, System.Windows.Forms.ContextMenuStrip cmsMenuFilter, ContextMenuStrip cmsMenuException)
        {
            ///Pour chaque table contenue dans le système de données \ref dsntTables.
            foreach (String sKey in dsntTables.Keys)
            {
                ///__ Recherche de la table où se trouve le filtre courant.
                TreeNode tnTable = OverallTools.TreeViewFunctions.RechercherNom(sKey, treeNode);
                ///__ Si il n'y a aucun noeud associé à la table recherchée, alors on passe à la table suivante.
                if (tnTable == null)
                    continue;
                ///__ Appel de la fonction \ref NormalTable.UpdateFilters() pour mettre à jour l'arborescence.
                dsntTables[sKey].UpdateFilters(sName, tnTable, cmsMenuFilter, cmsMenuException);
            }
        }

        /// <summary>
        /// Fonction qui permet de convertir l'ancienne version du système de données dans cette toute nouvelle version.
        /// </summary>
        /// <param name="gdData">Ancienne version du système de données.</param>
        internal virtual void ConvertGestionDonnees(GestionDonnees gdData)
        {
            ///Si \ref gdData est null, la fonction s'arrete.
            if (gdData == null)
                return;

            List<String> lsTables = gdData.TablesNames;

            List<String> lsFilters = new List<string>();
            foreach (String sKey in gdData.ListFilters.Keys)
                lsFilters.Add(sKey);
            List<String> lsUserData = gdData.ListUserData;
            ///Pour chaque table présente dans l'ancien système de données.
            foreach (String sTable in gdData.TablesNames)
            {
                ///__ S'il s'agit d'un filtre, alors passé à la table suivante.
                if (lsFilters.Contains(sTable))
                    continue;
                ///__ S'il s'agit d'un UserData, alors passé à la table suivante.
                if (lsUserData.Contains(sTable))
                    continue;
                ///__ On ignore les tables qui s'appellent AircragtLinksTable, car ancienne version.
                if (sTable == "AircraftLinksTable")
                    continue;
                ///__ Conversion et création d'un \ref NormalTable
                NormalTable ntTmp = new NormalTable(gdData.GetTable(sTable), gdData.getModeVisualisation(sTable), gdData.getGraphicFilter(sTable));
                ///__ Mise à jour des parametres \ref NormaTable.TableParameters avec la fonction \ref  DataManagerInput.getParameters
                ntTmp.TableParameters = DataManagerInput.getParameters(sTable);
                ///__ Ajout de la note (si présente).
                ntTmp.Note = gdData.getNote(sTable);
                ///__ Ajout de la table au système de données avec la fonction \ref AddTable
                AddTable(ntTmp);
            }

            ///Pour chaque filtre présent dans la liste des filtres de l'ancien système de données.
            String sFirstAdded = "";
            List<String> lsMoved = new List<string>();
            //bool bHasProblem=false;
            for (int i = 0; i < lsFilters.Count; i++)
            {
                String sFilter = lsFilters[i];
                ///__ Récupération de la définition du filtre \ref Filter.
                Filter fFilter = gdData.getFilter(sFilter);
                ///__ Si la table parente de ce filtre n'est pas encore présente dans le système de données courant,
                ///alors cette table est mise en fin de liste, afin d'être traitée après tous les autres filtres.
                if (!Exist(fFilter.MotherTableName))
                {
                    //Si bHasProblem est vrai et que le filtre courant avait déjà été mis à la fin de la liste, cela signifie qu'il 
                    //y a un problème. Des tables manquent (ou des filtres) et ne permettent pas l'ajout de la totalité des filtres
                    //présents dans l'ancien système de données.
                    if (lsMoved.Contains(sFilter))
                        break;
                    /*if ((bHasProblem) && (sFilter == sFirstAdded))
                        break;*/
                    //bHasProblem est mis à vrai pour indiquer que l'on vient de déplacer un élément.
                    //bHasProblem = true;
                    //Si sFirstAdded est vide, cela veut dire que c'est la première fois qu'un filtre est déplacé, (ou alors que le premier
                    //filtre déplacé a finalement été ajouté). On met donc dans cette variable le nom du filtre courant.
                    lsMoved.Add(sFilter);
                    /*if (sFirstAdded == "")
                        sFirstAdded = sFilter;*/
                    //On déplace le filtre et on passe au filtre suivant.
                    lsFilters.Add(sFilter);
                    lsFilters.RemoveAt(i);
                    i--;
                    continue;
                }
                //Si le filtre courant est également le premier filtre qui avait posé problème, alors on remet à "" la variable qui stockait son
                //nom.
                if (sFirstAdded == sFilter)
                    sFilter = "";
                //On remet à FAUX bHasProblem
                //bHasProblem = false;
                ///__ Si le filtre est un filtre copie alors on ajoute également son DataTable
                DataTable dtValue = null;
                if (fFilter.copyTable)
                    dtValue = gdData.GetTable(sFilter);
                ///__ Appel de la fonction \ref AddFilter.
                AddFilter(fFilter, dtValue, gdData.getModeVisualisation(sFilter), gdData.getGraphicFilter(sFilter));
            }
        }
        #endregion

        #region Fonctions Copy et Dispose
        /// <summary>
        /// Fonction qui permet la copie du système de données. La fonction copie l'intégralité des tables et des filtes.
        /// </summary>
        /// <returns>Nouvelle instance qui est une copie de l'objet à copier.</returns>
        internal virtual Object Copy()
        {
            /// Appel du constructeur par copie \ref DataManager avec this en parametre.
            return new DataManager(this);
        }

        /// <summary>
        /// Fonction pour rendre l'espace mémoire occupé par cette classe.
        /// </summary>
        internal virtual void Dispose()
        {
            /*foreach (Dictionary<String, NormalTable> dico in new Dictionary<String, NormalTable>[] { dsntTables, dsntFilters })
                foreach (NormalTable table in dico.Values)
                    table.Delete();*/

            foreach (NormalTable table in dsntTables.Values)
                table.Delete();
            lsListUsedNames = null;
            sName = null;
            gpdPath = null;
        }

        #endregion

        #region Gestion des délégués pour GetPath
        /// <summary>
        /// Délégué représentant la fonction que les tables appeleront pour déterminer le chemin du dossier où se trouve le .pax (dossier racine du projet).
        /// </summary>
        /// <returns>Chemin absolu permettant d'accéder au dossier racine du projet.</returns>
        internal delegate String GetPathDelegate();

        /// <summary>
        /// Fonction qui permet de récupérer le dossier racine du projet. Cette fonction fait appel à la fonction stockée dans \ref gpdPath.
        /// </summary>
        /// <returns>Chemin absolu permettant d'accéder au dossier racine du projet.</returns>
        internal virtual String GetPathFunction()
        {
            ///Si gpdPath est null, la fonction renvoie "".
            if (gpdPath == null)
                return "";
            ///la fonction renvoie le résultat de la fonction \ref  gpdPath.
            return gpdPath();
            ///---Ce fonctionnement a été décidé, car il permet de ne pas mettre à jour l'intégralité des tables pour leur propre définition de la 
            ///fonction pour récupérer la racine du projet. En effet lorsque le \ref DataManager est copié, il faut redéfinir la fonction appelée 
            ///par les tables pour que celles ci utilisent la bonne fonction.
        }
        #endregion
    }
    #endregion

    #region public class DataManagerInput : DataManager
    /// <summary>
    /// Classe qui contient toutes les informations concernant les données d'entrée d'un projet. Elle regroupe notamment
    /// les tables et les filtres.
    /// </summary>
    public class DataManagerInput : DataManager
    {
        #region Gestion des tables pouvant avoir des exceptions et gestions des représentations par défaut des tables Input.

        #region ExceptionTableParameters
        /// <summary>
        /// Fonction qui permet d'initialiser les variables statiques \ref dsetpExceptionParameters et \ref lsExceptionFormatWithRow. Elle
        /// renseigne les informations concernant la mise en place des exceptions sur ces tables.
        /// </summary>
        private static void InitializeExceptionParameters()
        {
            dsetpExceptionParameters = new Dictionary<string, ExceptionTable.ExceptionTableParameters>();
            dsetpExceptionParameters.Add(GlobalNames.FP_AircraftTypesTableName, ExceptionTable.ExceptionTableParameters.FlightCategory |
                ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.Airline);

            dsetpExceptionParameters.Add(GlobalNames.Times_ProcessTableName, ExceptionTable.ExceptionTableParameters.FlightCategory |
                ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);



#if(PAXINOUTUTILISATION)
            dsetpExceptionParameters.Add(GlobalNames.ProcessDistributionPaxInName, ExceptionTable.ExceptionTableParameters.FlightCategory |
                ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);
            dsetpExceptionParameters.Add(GlobalNames.ProcessDistributionPaxOutName, ExceptionTable.ExceptionTableParameters.FlightCategory |
                ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);
#endif
            dsetpExceptionParameters.Add(GlobalNames.FPD_LoadFactorsTableName, ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.Airline);
            dsetpExceptionParameters.Add(GlobalNames.FPA_LoadFactorsTableName, ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.Airline);
            dsetpExceptionParameters.Add(GlobalNames.Transfer_ICTTableName, ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);

            dsetpExceptionParameters.Add(GlobalNames.CI_ShowUpTableName, ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);

            dsetpExceptionParameters.Add(GlobalNames.NbBagsTableName, ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);
            dsetpExceptionParameters.Add(GlobalNames.NbVisitorsTableName, ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);
            dsetpExceptionParameters.Add(GlobalNames.NbTrolleyTableName, ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);


            dsetpExceptionParameters.Add(GlobalNames.OCT_CITableName, ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);


            #region 26/03/2012 - SGE - Parking Mulhouse
            dsetpExceptionParameters.Add(GlobalNames.Parking_LongStayTableName, ExceptionTable.ExceptionTableParameters.FlightCategory |
                ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);

            dsetpExceptionParameters.Add(GlobalNames.Parking_ShortStayTableName, ExceptionTable.ExceptionTableParameters.FlightCategory |
                ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);
            #endregion //26/03/2012 - SGE - Parking Mulhouse




            dsetpExceptionParameters.Add(GlobalNames.OCT_BoardGateTableName, ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.FirstAndBusiness |    // >> Task #13361 FP AutoMod Data tables V3
                ExceptionTable.ExceptionTableParameters.Airline);
            //arrGate
            dsetpExceptionParameters.Add(GlobalNames.OCT_ArrivalGateTableName, ExceptionTable.ExceptionTableParameters.Flight |
                //ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);
            //baggDrop  // << Task #9413 Pax2sim -Scenario properties - FPD export table - OCT for CheckIn and BagDrop
            dsetpExceptionParameters.Add(GlobalNames.OCT_BaggDropTableName, ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);
            dsetpExceptionParameters.Add(GlobalNames.OCT_BaggageClaimTableName, ExceptionTable.ExceptionTableParameters.Flight |
                //ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);
            dsetpExceptionParameters.Add(GlobalNames.OCT_ParkingTableName, ExceptionTable.ExceptionTableParameters.Flight |
                //ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);
            //runway
            dsetpExceptionParameters.Add(GlobalNames.OCT_RunwayTableName, ExceptionTable.ExceptionTableParameters.Flight |
                ExceptionTable.ExceptionTableParameters.Airline);

            dsetpExceptionParameters.Add(GlobalNames.OCT_MakeUpTableName, ExceptionTable.ExceptionTableParameters.Flight |
                //ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);
            dsetpExceptionParameters.Add(GlobalNames.SegregationName, ExceptionTable.ExceptionTableParameters.Flight |
                //ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);
            dsetpExceptionParameters.Add(GlobalNames.TransferInfeedAllocationRulesTableName, ExceptionTable.ExceptionTableParameters.Flight |
                //ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);
            //arrInf
            dsetpExceptionParameters.Add(GlobalNames.OCT_ArrivalInfeedTableName, ExceptionTable.ExceptionTableParameters.Flight |
                //ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                ExceptionTable.ExceptionTableParameters.Airline);

            lsExceptionFormatWithRow = new List<string>();
            lsExceptionFormatWithRow.Add(GlobalNames.FP_AircraftTypesTableName);
            lsExceptionFormatWithRow.Add(GlobalNames.Times_ProcessTableName);
#if(PAXINOUTUTILISATION)
            lsExceptionFormatWithRow.Add(GlobalNames.ProcessDistributionPaxInName);
            lsExceptionFormatWithRow.Add(GlobalNames.ProcessDistributionPaxOutName);

#endif
        }
        /// <summary>
        /// Liste des tables ayant \ref ExceptionTable.ExceptionTableParameters. Cette contient aussi l'application de ces exceptions, Flight/ Airline ...
        /// </summary>
        private static Dictionary<String, ExceptionTable.ExceptionTableParameters> dsetpExceptionParameters;
        /// <summary>
        /// Fonction qui renvoie le paramétrage (\ref ExceptionTable.ExceptionTableParameters) pour la table passée en paramètre.
        /// </summary>
        /// <param name="sTableName">Nom de la table pour laquelle on souhaite avoir un paramétrage.</param>
        /// <returns></returns>
        internal static ExceptionTable.ExceptionTableParameters GetExceptionParameters(String sTableName)
        {
            ///Si \ref dsetpExceptionParameters est null, alors appel de la fonction \ref InitializeExceptionParameters.
            if (dsetpExceptionParameters == null)
                InitializeExceptionParameters();
            ///Si \ref dsetpExceptionParameters est null, alors renvoie de \ref ExceptionTable.ExceptionTableParameters.None.
            if (dsetpExceptionParameters == null)
                return ExceptionTable.ExceptionTableParameters.None;
            ///Si \ref dsetpExceptionParameters ne contient pas définition pour la table passée en paramètre, 
            /// alors renvoie de \ref ExceptionTable.ExceptionTableParameters.None.
            if (!dsetpExceptionParameters.ContainsKey(sTableName))
                return ExceptionTable.ExceptionTableParameters.None;
            ///Sinon renvoie du paramétrage demandé
            return dsetpExceptionParameters[sTableName];
        }
        #endregion

        #region ExceptionTableFormat

        /// <summary>
        /// Liste des tables ayant des exceptions par colonne.
        /// </summary>
        private static List<String> lsExceptionFormatWithRow;

        /// <summary>
        /// Fonction qui renvoie le paramétrage (\ref ExceptionTable.ExceptionTableFormat) pour la table passée en paramètre.
        /// </summary>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        internal static ExceptionTable.ExceptionTableFormat GetExceptionFormats(String sTableName)
        {
            ///Si \ref dsetpExceptionParameters est null, alors appel de la fonction \ref InitializeExceptionParameters.
            if (dsetpExceptionParameters == null)
                InitializeExceptionParameters();
            ///Si \ref dsetpExceptionParameters est null, alors renvoie de \ref ExceptionTable.ExceptionTableFormat.Column.
            if (dsetpExceptionParameters == null)
                return ExceptionTable.ExceptionTableFormat.Column;
            ///Si \ref lsExceptionFormatWithRow ne contient pas définition pour la table passée en paramètre, 
            /// alors renvoie de \ref ExceptionTable.ExceptionTableFormat.Column.
            if (!lsExceptionFormatWithRow.Contains(sTableName))
                return ExceptionTable.ExceptionTableFormat.Column;
            ///Sinon renvoie de ExceptionTable.ExceptionTableFormat.Line
            return ExceptionTable.ExceptionTableFormat.Line;
        }
        #endregion

        #region Table Parameters
        /// <summary>
        /// Variable statique qui contient la totalité des paramétrage pour les tables par défaut pour PAX2SIM.
        /// </summary>
        private static Dictionary<String, LoadParameters> dslpTableParameters;

        /// <summary>
        /// Arborescence par défaut qui doit être donnée aux tables en entrées pour PAX2SIM (tables uniquement passagers).
        /// </summary>
        private static ArboInputTable aitInputData;

        #region internal class LoadParameters & internal class ArboInputTable
        /// <summary>
        /// Les définitions des comportements des table ayant un comportement spécifique lié à son utilisation.
        /// </summary>
        internal class LoadParameters
        {
            /// <summary>
            /// Nom de la table visée par ce comportement spécifique.
            /// </summary>
            internal String sName;
            /// <summary>
            /// Nom complet de la table visée par ce comportement (Nom affiché).
            /// </summary>
            internal String sFullName;
            /// <summary>
            /// Information pour savoir si la table sera sauvegardée sur disque.
            /// </summary>
            internal bool bSaved;
            /// <summary>
            /// Information pour savoir si la table doit être visible ou non dans l'arborescence.
            /// </summary>
            internal bool bVisible;
            /// <summary>
            /// Information pour savoir si la table est active ou non. Si non active, alors cela ne sert à rien de la créer.
            /// </summary>
            internal bool bActive;

            /// <summary>
            /// Classe qui représente une Entete de table. le nom de la colonne et son type.
            /// </summary>
            internal class Entete
            {
                /// <summary>
                /// Nom de d'une colonne pour la table.
                /// </summary>
                internal String sName;
                /// <summary>
                /// Type de la colonne.
                /// </summary>
                internal Type tType;
                /// <summary>
                /// Constructeur de \ref Entete qui permet d'inialiser les couples Name, Type.
                /// </summary>
                /// <param name="Name">Nom de d'une colonne pour la table.</param>
                /// <param name="Type_">Type de la colonne.</param>
                internal Entete(String Name, Type Type_)
                {
                    sName = Name;
                    tType = Type_;
                }
            }

            /// <summary>
            /// Liste des noms des entetes de colonnes présentes dans la table. (avec leur type).
            /// </summary>
            internal List<Entete> dstEntetes;

            /// <summary>
            /// List des noms des colonnes qui font parti de la clef primaire.
            /// </summary>
            internal List<String> lsPrimaryKeys;

            /// <summary>
            /// Liste des valeurs que doit impérativement prendre la première colonne.
            /// </summary>
            internal String[] oDefaultFirstColumn;

            /// <summary>
            /// Liste des valeurs par défaut à ajouter dans la table lors de l'ajout d'une nouvelle ligne.
            /// </summary>
            internal Object[] oDefaultLine;

            /// <summary>
            /// Liste des valeurs par défaut à ajouter dans la table lors de l'ajout d'une nouvelle colonne.
            /// </summary>
            internal Double[] oDefaultColumn;

            /// <summary>
            /// Valeur par défaut à ajouter à la table lors de l'ajout d'une nouvelle cellule.
            /// </summary>
            internal Object oDefaultValue;

            /// <summary>
            /// Booléen indiquant si la table à un nombre de colonnes (et leurs noms) défini ou non. Si ce booléen est à vrai,
            /// cela veut dire que la table qui sera chargée pourra avoir plus de colonnes que le nombre de colonnes présentes
            /// dans \ref dstEntetes. Cette option est habituellement associées aux tables qui ont leurs colonnes liées au
            /// nombre de catégories de vols présents dans le projet. 
            /// Cela ne veut pas dire que l'utilisateur final peut modifier directement le nombre de colonnes.
            /// </summary>
            internal bool bAllowAddColumns;
            /// <summary>
            /// Si \ref bAllowAddColumns est à Vrai, alors cette variable contient le type que doivent avoir les nouvelles colonnes.
            /// </summary>
            internal Type tTypeNewColumns;

            /// <summary>
            /// Booléen indiquant si la table est liée aux catégories de vol (Flight categories) ou non. Si c'est le cas, alors à chaque 
            /// fois que la table des catégories de vol est modifiée, on doit venir actualiser la table courante.
            /// </summary>
            internal bool bFlightCategoryTable;

            /// <summary>
            /// Booléen indiquant si la table est liée à la structeur de l'aéroport (auquel cas, lors du changement de la structure de 
            /// l'aéroport, on doit recalculer la table et vérifier qu'elle est toujours conforme.)
            /// </summary>
            internal bool bShemaAirportTable;

            /// <summary>
            /// Booléen indiquant si la table qui est liée à la structure de l'aéroport a ses lignes qui sont liées à la structure
            /// de l'aéroport. Utilisé uniquement si \ref bShemaAirportTable est à vrai.
            /// </summary>
            internal bool bSAirport_AnalyseLigne;

            /// <summary>
            /// Booléen indiquant si la table qui est liée à la structure de l'aéroport a ses colonnes qui sont liées à la structure
            /// de l'aéroport. Utilisé uniquement si \ref bShemaAirportTable est à vrai.
            /// </summary>
            internal bool bSAirport_AnalyseColonne;

            /// <summary>
            /// Entier permettant de connaître quel est le type de recherche qui doit être effectué lors de l'analyse de 
            /// la structure de l'aéroport.
            /// </summary>
            internal int iSAirport_TypeRecherche;

            /// <summary>
            /// Booléen indiquant si la table est une table d'allocation ou non. (Si oui, cela veut dire qu'elle est calculée à partir d'autres tables.
            /// </summary>
            internal bool bAllocationTable;

            /// <summary>
            /// Booléen indiquant si la table est une table BHS ou non.
            /// </summary>
            internal bool bBHS;

            /// <summary>
            /// Booléen indiquant si la table BHS (\ref bBHS est à vrai) est indexée sur un numéro de terminal.
            /// </summary>
            internal bool bIndexedOnTerminal;

            /// <summary>
            /// Mode de visualisation par défaut pour la table.
            /// </summary>
            internal VisualisationMode vmDefaultVisualisationMode;

            /// <summary>
            /// Variable contenant la structure qui permet de vérifier que le contenu de la table est valide.
            /// </summary>
            internal OverallTools.TableCheck tcChecks;

            /// <summary>
            /// Constructeur par défaut.
            /// </summary>
            internal LoadParameters()
            {
                sName = null;
                sFullName = null;
                bSaved = false;
                bVisible = false;
                bActive = false;
                dstEntetes = null;
                lsPrimaryKeys = null;
                oDefaultFirstColumn = null;
                oDefaultLine = null;
                oDefaultColumn = null;
                oDefaultValue = null;
                bAllowAddColumns = false;
                tTypeNewColumns = null;
                bFlightCategoryTable = false;
                bShemaAirportTable = false;
                bAllocationTable = false;
                bBHS = false;
                bIndexedOnTerminal = false;
                vmDefaultVisualisationMode = null;
                tcChecks = null;
            }

            /// <summary>
            /// Fonction qui permet d'initialiser la table à partir des informations sur les entetes de colonnes et les clefs primaires.
            /// </summary>
            /// <returns>Nouvelle table initialisée avec les paramètres de la classe.</returns>
            internal DataTable InitializeTable()
            {
                DataTable dtTable = new DataTable(sName);
                Int32[] iPrimary = new Int32[lsPrimaryKeys.Count];
                String[] tsEntete = new String[dstEntetes.Count];
                Type[] ttEntete = new Type[dstEntetes.Count];
                for (int i = 0; i < dstEntetes.Count; i++)
                {
                    tsEntete[i] = dstEntetes[i].sName;
                    ttEntete[i] = dstEntetes[i].tType;
                }
                for (int i = 0; i < lsPrimaryKeys.Count; i++)
                {
                    iPrimary[i] = OverallTools.FonctionUtiles.indexDansListe(lsPrimaryKeys[i], tsEntete);
                }
                OverallTools.FonctionUtiles.initialiserTable(dtTable, tsEntete, ttEntete, iPrimary);
                return dtTable;
            }
        }

        /// <summary>
        /// Classe qui représente un noeud de l'arborescence.
        /// </summary>
        internal class ArboInputTable
        {
            /// <summary>
            /// Enumération permetant de savoir si le noeud est un dossier ou une table.
            /// </summary>
            private enum ArboType { Directory, Table };

            /// <summary>
            /// Type de noeud.
            /// </summary>
            private ArboType atType;

            /// <summary>
            /// Nom du noeud actuel
            /// </summary>
            private String sName;

            /// <summary>
            /// Parametres de la table courante.
            /// </summary>
            private LoadParameters lpParameters;

            /// <summary>
            /// Booléen permettant de savoir si le noeud doit être visible ou pas.
            /// </summary>
            private Boolean Visible_;

            /// <summary>
            /// La liste des noeuds qui sont des noeuds fils du noeud courant.
            /// </summary>
            List<ArboInputTable> laitChild;

            /// <summary>
            /// Propriété permetant de savoir si le noeud courant est une table ou non.
            /// </summary>
            internal Boolean isTable
            {
                get
                {
                    return (atType == ArboType.Table);
                }
            }

            /// <summary>
            /// Propriété permetant de savoir si le noeud courant est un dossier ou non.
            /// </summary>
            internal Boolean isDirectory
            {
                get
                {
                    return (atType == ArboType.Directory);
                }
            }

            /// <summary>
            /// Propriété permetant de savoir si le noeud courant est visible ou non.
            /// </summary>
            internal Boolean Visible
            {
                get
                {
                    return Visible_;
                }
                set
                {
                    Visible_ = value;
                    if (lpParameters != null)
                        lpParameters.bVisible = value;
                }
            }

            /// <summary>
            /// Fonction permetant de créer un \ref ArboInputTable de type dossier avec comme nom \ref sNameDirectory.
            /// </summary>
            /// <param name="sNameDirectory">Nom que le dossier doit avoir.</param>
            /// <returns>Nouveau \ref ArboInputTable représentant un dossier.</returns>
            internal static ArboInputTable getDirectory(String sNameDirectory)
            {
                ///Appel du constructeur \ref ArboInputTable
                return new ArboInputTable(sNameDirectory, ArboType.Directory);
            }

            /// <summary>
            /// Fonction permetant de créer un \ref ArboInputTable de type table avec comme nom \ref lpTableParameters.Name.
            /// </summary>
            /// <param name="lpTableParameters">Paramétrage de la table.</param>
            /// <returns>Nouveau \ref ArboInputTable représentant un dossier.</returns>
            internal static ArboInputTable getTable(LoadParameters lpTableParameters)
            {
                ///Appel du constructeur \ref ArboInputTable
                return new ArboInputTable(lpTableParameters.sFullName, ArboType.Table, lpTableParameters);
            }

            /// <summary>
            /// Constructeur privé d'un nouveau ArboInputTable.
            /// </summary>
            /// <param name="sName_"></param>
            /// <param name="atType_"></param>
            private ArboInputTable(String sName_, ArboType atType_)
            {
                Visible_ = true;
                sName = sName_;
                atType = atType_;
                lpParameters = null;
            }

            /// <summary>
            /// Constructeur privé d'un nouveau ArboInputTable.
            /// </summary>
            /// <param name="sName_"></param>
            /// <param name="atType_"></param>
            /// <param name="lpParameters_"></param>
            private ArboInputTable(String sName_, ArboType atType_, LoadParameters lpParameters_)
            {
                sName = sName_;
                atType = atType_;
                lpParameters = lpParameters_;
                Visible_ = lpParameters_.bVisible;
            }

            /// <summary>
            /// Fonction qui permet d'ajouter un enfant au noeud courant.
            /// </summary>
            /// <param name="lpTableParameters">Parametres de la table à ajouter.</param>
            internal void AddChild(LoadParameters lpTableParameters)
            {
                ///Si le noeud courant n'est pas une dossier, Arreter la fonction.
                if (!isDirectory)
                    return;
                ///Si la liste \ref laitChild est NULL, alors la fonction l'initialise.
                if (laitChild == null)
                    laitChild = new List<ArboInputTable>();
                ///Création d'un nouveau \ref ArboInputTable avec \ref getTable.
                ArboInputTable aitTmp = getTable(lpTableParameters);
                ///Ajout de ce nouveau noeud à la liste \ref laitChild
                if (aitTmp != null)
                    laitChild.Add(aitTmp);
            }

            /// <summary>
            /// Fonction qui permet d'ajouter un enfant au noeud courant.
            /// </summary>
            /// <param name="aitNewObject">Nouveau noeud à ajouter</param>
            internal void AddChild(ArboInputTable aitNewObject)
            {
                ///Si le noeud courant n'est pas une dossier, Arreter la fonction.
                if (!isDirectory)
                    return;
                ///Si la liste \ref laitChild est NULL, alors la fonction l'initialise.
                if (laitChild == null)
                    laitChild = new List<ArboInputTable>();
                ///Ajout de ce nouveau noeud à la liste \ref laitChild
                if (aitNewObject != null)
                    laitChild.Add(aitNewObject);
            }

            /// <summary>
            /// Fonction qui permet d'ajouter un enfant au noeud courant.
            /// </summary>
            /// <param name="sDirectoryName">Nom du dossier à ajouter</param>
            internal void AddDirectoryChild(String sDirectoryName)
            {
                ///Si le noeud courant n'est pas une dossier, Arreter la fonction.
                if (!isDirectory)
                    return;
                ///Si la liste \ref laitChild est NULL, alors la fonction l'initialise.
                if (laitChild == null)
                    laitChild = new List<ArboInputTable>();
                ///Création d'un nouveau \ref ArboInputTable avec \ref getDirectory.
                ArboInputTable aitTmp = getDirectory(sDirectoryName);
                ///Ajout de ce nouveau noeud à la liste \ref laitChild
                if (aitTmp != null)
                    laitChild.Add(aitTmp);

            }

            /// <summary>
            /// Fonction qui permet de mettre à jour l'arbre en fonction du noeud courant. Cette fonction mettre à jour
            /// l'arbre passé en parametre avec les noeuds manquants.
            /// </summary>
            /// <param name="tnParentNode">Noeud Parent du noeud courant. </param>
            /// <param name="cmsTableContextMenu">Menu contextuel qui doit être ajouter sur les tables.</param>
            /// <returns>TreeNode représentant l'arborescence de ce noeud.</returns>
            internal TreeNode UpdateTreeNode(TreeNode tnParentNode, ContextMenuStrip cmsTableContextMenu)
            {
                ///On recherche le noeud \ref sName dans \ref tnParentNode.
                TreeNode tnTmp = OverallTools.TreeViewFunctions.RechercherNom(sName, tnParentNode);
                if (!Visible)
                {
                    ///Si le noeud courant ne doit pas être visible, mais qu'il apparait dans \ref tnParentNode, alors on supprime le noeud trouvé et la fonction renvoie NULL.
                    if (tnTmp != null)
                    {
                        tnParentNode.Nodes.Remove(tnTmp);
                    }
                    return null;
                }
                ///Si le noeud courant est différent de NULL, appel de la fonction \ref UpdateTreeNodeWithChilds et la fonction renvoie NULL.
                if (tnTmp != null)
                {
                    UpdateTreeNodeWithChilds(tnTmp, cmsTableContextMenu);
                    return null;
                }
                ///Création d'un TreeNode du type du noeud courant (Table ou Dossier).
                if ((isDirectory) && (tnTmp == null))
                    tnTmp = OverallTools.TreeViewFunctions.createBranch(sName, sName, TreeViewTag.getDirectoryNode(sName), null);
                else if ((isTable) && (tnTmp == null))
                    tnTmp = OverallTools.TreeViewFunctions.createBranch(lpParameters.sName, lpParameters.sFullName, TreeViewTag.getTableNode("Input", lpParameters.sName), cmsTableContextMenu);
                ///Mise à jour des noeuds enfant à l'aide de la fonction \ref UpdateTreeNodeWithChilds 
                UpdateTreeNodeWithChilds(tnTmp, cmsTableContextMenu);
                ///Renvoie du noeud nouvellement créer.
                return tnTmp;
            }

            /// <summary>
            /// Fonction qui permet de générer l'arborescence des enfants du noeud courant.
            /// </summary>
            /// <param name="tnCurrentNode">Représentation du noeud courant.</param>
            /// <param name="cmsTableContextMenu">Menu contextuel à ajouter pour les tables.</param>
            internal void UpdateTreeNodeWithChilds(TreeNode tnCurrentNode, ContextMenuStrip cmsTableContextMenu)
            {
                ///Si \ref laitChild est null, la fonction s'arrete.
                if (laitChild == null)
                    return;
                int iVisible = 0;
                ///Pour chaque \ref ArboInputTable contenue dans \ref laitChild
                for (int i = 0; i < laitChild.Count; i++)
                {
                    ///Appel de la fonction \ref UpdateTreeNode.
                    ///Si cette fonction renvoie NULL, alors il faut passer au \ref ArboInputTable suivant.
                    TreeNode tnChild = laitChild[i].UpdateTreeNode(tnCurrentNode, cmsTableContextMenu);
                    if (laitChild[i].Visible)
                        iVisible++;
                    if (tnChild == null)
                        continue;
                    ///Insertion du TreeNode avec les enfants de \ref tnCurrentNode
                    if (iVisible > tnCurrentNode.Nodes.Count)
                    {
                        tnCurrentNode.Nodes.Insert(iVisible, tnChild);
                    }
                    else
                    {
                        iVisible = tnCurrentNode.Nodes.Count;
                        tnCurrentNode.Nodes.Add(tnChild);
                    }
                }

            }

            /// <summary>
            /// Fonction qui permet de trouver l'enfant du noeud courant portant le nom \ref sChildName. Si aucun
            /// enfant ne porte ce nom, alors la fonction renverra NULL.
            /// </summary>
            /// <param name="sChildName">Le nom de l'enfant recherché, ou alors NULL.</param>
            /// <returns>L'enfant qui était recherché, ou alors NULL.</returns>
            internal ArboInputTable GetChildNamed(String sChildName)
            {
                //<< Task #7405 - new Desk and extra information for Pax                
                //if the ArboInputTable doesn't have any content return null
                if (laitChild == null)
                    return null;
                //>> Task #7405 - new Desk and extra information for Pax
                ///Pour chaque \ref ArboInputTable contenue dans \ref laitChild
                for (int i = 0; i < laitChild.Count; i++)
                {
                    ///__ Si le \ref ArboInputTable est celui recherché, alors la fonction retourne ce noeud.
                    if (laitChild[i].sName == sChildName)
                        return laitChild[i];
                }
                ///Si aucun enfant n'a été trouvé portant ce nom, alors la fonction renvoie null.
                return null;
            }
            //<< Task #7405 - new Desk and extra information for Pax
            internal Dictionary<string, ArboInputTable> getChildrenDictionary()
            {
                if (laitChild == null)
                    return null;
                Dictionary<string, ArboInputTable> distributionTablesDictionary = new Dictionary<string, ArboInputTable>();
                for (int i = 0; i < laitChild.Count; i++)
                    distributionTablesDictionary.Add(laitChild[i].sName, laitChild[i]);
                return distributionTablesDictionary;
            }
            //>> Task #7405 - new Desk and extra information for Pax

            /// <summary>
            /// Fonction qui permet de trier l'ensemble des enfants présent dans le dossier courant.
            /// </summary>
            /// <param name="bPutDirectoryFirst">Indique si les dossiers doivent être placés au début ou à la fin lors du tri.</param>
            internal void Sort(bool bPutDirectoryFirst)
            {
                ///Récupération des listes des dossiers et des tables enfant du noeud courant.
                List<String> lsDirectory = new List<string>();
                List<String> lsTable = new List<string>();
                foreach (ArboInputTable aitTmp in laitChild)
                {
                    if (aitTmp.isDirectory)
                        lsDirectory.Add(aitTmp.sName);
                    else
                        lsTable.Add(aitTmp.sName);
                }

                ///Tri des noms des tables et des dossiers.
                lsDirectory.Sort();
                lsTable.Sort();
                if (bPutDirectoryFirst)
                    lsDirectory.AddRange(lsTable);
                else
                {
                    lsTable.AddRange(lsDirectory);
                    lsDirectory.Clear();
                    lsDirectory = lsTable;
                }
                lsTable = null;

                ///Réorganisation des enfants dans l'ordre recalculé.
                List<ArboInputTable> laitTmp = new List<ArboInputTable>();
                foreach (String sTmp in lsDirectory)
                {
                    laitTmp.Add(GetChildNamed(sTmp));
                }
                laitChild = null;
                laitChild = laitTmp;
            }

            /// <summary>
            /// Fonction qui se charge de supprimer l'enfant portant le nom passé en paramètre. S'il n'existe pas,
            /// alors la fonction ne fera rien.
            /// </summary>
            /// <param name="sChildName">Nom de l'enfant qu'il faut supprimer.</param>
            internal void RemoveChild(string sChildName)
            {
                ///Pour chaque \ref ArboInputTable contenue dans \ref laitChild
                for (int i = 0; i < laitChild.Count; i++)
                {
                    ///__ Si le \ref ArboInputTable est celui recherché, alors la fonction retourne ce noeud.
                    if (laitChild[i].sName == sChildName)
                    {
                        laitChild.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Fonction qui permet d'initialiser les \ref LoadParameters et \ref ArboInputTable pour les tables en entrées pour PAX2SIM.
        /// </summary>
        private static void InitializeTableParameters()
        {
            dslpTableParameters = new Dictionary<string, LoadParameters>();
            LoadParameters lpTmp;
            ConditionnalFormatCharacter cfcAllocation = null;
            aitInputData = ArboInputTable.getDirectory("Input");
            ArboInputTable aitTmp;
            if (PAX2SIM.bPAX)
            {
                #region Flight Plans
                aitTmp = ArboInputTable.getDirectory("Flight Plans");
                aitInputData.AddChild(aitTmp);

                #region FPATableName
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.FPATableName;
                lpTmp.sFullName = "Arrival Flight Plans";
                lpTmp.bSaved = true;
                lpTmp.bActive = true;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_ID, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_DATE, typeof(DateTime)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPA_Column_STA, typeof(TimeSpan)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_AirlineCode, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_FlightN, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_AirportCode, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_FlightCategory, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_AircraftType, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPA_Column_NoBSM, typeof(Boolean)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPA_Column_CBP, typeof(Boolean)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_NbSeats, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_TerminalGate, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPA_Column_ArrivalGate, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPA_Column_TerminalReclaim, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPA_Column_ReclaimObject, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPA_Column_TerminalInfeedObject, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPA_Column_StartArrivalInfeedObject, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPA_Column_EndArrivalInfeedObject, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPA_Column_TransferInfeedObject, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_TerminalParking, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_Parking, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_RunWay, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User1, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User2, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User3, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User4, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User5, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sFPD_A_Column_ID);

                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = new Object[] { null, null, null, "", "", "", "", "", false, false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", "", "" };
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;

                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true,
                                            true,
                                            true,
                                            new int[] { 0 },
                                            new int[] { 0 },
                                            false,
                                            false,
                                            Color.White,
                                            Color.Blue,
                                            Color.Black,
                                            Color.White,
                                            VisualisationMode.SelectionModeEnum.Row,
                                            VisualisationMode.EditModeEnum.Row,
                                            new int[] { 0 },
                                            new int[] { 32, 70, 60, 56, 60, 56, 70, 64, 35 },
                                            null,
                                            null,
                                            null);
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = true;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName,
                    new String[] { GlobalNames.sFPD_A_Column_DATE },
                    new String[] { GlobalNames.sFPD_A_Column_FlightCategory, GlobalNames.sFPD_A_Column_AircraftType, GlobalNames.sFPD_A_Column_AirlineCode },
                    new String[] { GlobalNames.FP_FlightCategoriesTableName, GlobalNames.FP_AircraftTypesTableName, GlobalNames.FP_AirlineCodesTableName },
                     new string[] { GlobalNames.sFPD_A_Column_ID, GlobalNames.sFPD_A_Column_NbSeats });
                lpTmp.tcChecks.TableLinkedWarn = new bool[] { false, false, true };

                // << Task #9238 Pax2Sim - Validations - Validate flight plan Parking                
                lpTmp.tcChecks.ObjectType = new String[][] {
                    new String[]{"Arrival Gate"},
                    new String[]{"Baggage Claim"},
                    new String[]{GlobalNames.AIRCRAFT_PARKING_STAND_OBJECT_NAME}
                                              };
                lpTmp.tcChecks.CanBeNull = new Boolean[] { false, false, true };
                lpTmp.tcChecks.ObjectColumns = new String[][] {
                    new String[]{GlobalNames.sFPD_A_Column_TerminalGate, "", GlobalNames.sFPA_Column_ArrivalGate},
                    new String[]{GlobalNames.sFPA_Column_TerminalReclaim, "", GlobalNames.sFPA_Column_ReclaimObject},
                    new String[]{GlobalNames.sFPD_A_Column_TerminalParking, "", GlobalNames.sFPD_A_Column_Parking}
                                              };
                /*
                                lpTmp.tcChecks.ObjectType = new String[][] { new String[]{"Arrival Gate"},
                                                                new String[]{"Baggage Claim"}
                                                              };
                                lpTmp.tcChecks.CanBeNull = new Boolean[] { false, false };
                                lpTmp.tcChecks.ObjectColumns = new String[][] { new String[]{GlobalNames.sFPD_A_Column_TerminalGate, "", GlobalNames.sFPA_Column_ArrivalGate},
                                                                new String[]{GlobalNames.sFPA_Column_TerminalReclaim, "", GlobalNames.sFPA_Column_ReclaimObject}
                                                              };
                */
                // >> Task #9238 Pax2Sim - Validations - Validate flight plan Parking
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region FPDTableName
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.FPDTableName;
                lpTmp.sFullName = "Departure Flight Plans";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_ID, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_DATE, typeof(DateTime)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_STD, typeof(TimeSpan)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_AirlineCode, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_FlightN, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_AirportCode, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_FlightCategory, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_AircraftType, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_TSA, typeof(Boolean)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_NbSeats, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_TerminalCI, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_Eco_CI_Start, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_Eco_CI_End, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_FB_CI_Start, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_FB_CI_End, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_Eco_Drop_Start, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_Eco_Drop_End, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_FB_Drop_Start, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_FB_Drop_End, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_TerminalGate, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_BoardingGate, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_TerminalMup, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_Eco_Mup_Start, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_Eco_Mup_End, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_First_Mup_Start, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_First_Mup_End, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_TerminalParking, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_Parking, typeof(String)));
                //lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_Parking + "2", typeof(String))); // >> Task #11326 Pax2Sim - Allocation - add Parking allocation option
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_RunWay, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User1, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User2, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User3, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User4, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User5, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sFPD_A_Column_ID);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = new Object[] { null, null, null, "", "", "", "", "", false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", "", "" };
                //lpTmp.oDefaultLine = new Object[] { null, null, null, "", "", "", "", "", false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", "", "" };    // >> Task #11326 Pax2Sim - Allocation - add Parking allocation option
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true,
                                            true,
                                            true,
                                            new int[] { 0 },
                                            new int[] { 0 },
                                            false,
                                            false,
                                            Color.White,
                                            Color.Blue,
                                            Color.Black,
                                            Color.White,
                                            VisualisationMode.SelectionModeEnum.Row,
                                            VisualisationMode.EditModeEnum.Row,
                                            new int[] { 0 },
                                            new int[] { 32, 70, 60, 56, 60, 56, 70, 64, 35 },
                                            null,
                                            null,
                                            null);
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = true; ;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName,
                           new String[] { GlobalNames.sFPD_A_Column_DATE },
                           new String[] { GlobalNames.sFPD_A_Column_FlightCategory, GlobalNames.sFPD_A_Column_AircraftType, GlobalNames.sFPD_A_Column_AirlineCode },
                           new String[] { GlobalNames.FP_FlightCategoriesTableName, GlobalNames.FP_AircraftTypesTableName, GlobalNames.FP_AirlineCodesTableName },
                           new string[] { GlobalNames.sFPD_A_Column_ID, GlobalNames.sFPD_A_Column_NbSeats });

                lpTmp.tcChecks.TableLinkedWarn = new bool[] { false, false, true };

                // << Task #9238 Pax2Sim - Validations - Validate flight plan Parking
                lpTmp.tcChecks.ObjectType = new String[][] { new String[]{"Check In"},
                                                new String[]{"Check In"},
                                                new String[]{GlobalNames.sFPD_Column_BoardingGate},
                                                new String[]{GlobalNames.AIRCRAFT_PARKING_STAND_OBJECT_NAME}
                                              };
                lpTmp.tcChecks.CanBeNull = new Boolean[] { false, false, false, true };
                lpTmp.tcChecks.ObjectColumns = new String[][] { new String[]{GlobalNames.sFPD_Column_TerminalCI,"", GlobalNames.sFPD_Column_Eco_CI_Start, GlobalNames.sFPD_Column_Eco_CI_End},
                                                new String[]{GlobalNames.sFPD_Column_TerminalCI,"", GlobalNames.sFPD_Column_FB_CI_Start, GlobalNames.sFPD_Column_FB_CI_End},
                                                new String[]{GlobalNames.sFPD_A_Column_TerminalGate, "", GlobalNames.sFPD_Column_BoardingGate},
                                                new String[]{GlobalNames.sFPD_A_Column_TerminalParking, "", GlobalNames.sFPD_A_Column_Parking}
                                              };
                /*                
                                lpTmp.tcChecks.ObjectType = new String[][] { new String[]{"Check In"},
                                                                new String[]{"Check In"},
                                                                new String[]{GlobalNames.sFPD_Column_BoardingGate}
                                                              };
                                lpTmp.tcChecks.CanBeNull = new Boolean[] { false, false, false };
                                lpTmp.tcChecks.ObjectColumns = new String[][] { new String[]{GlobalNames.sFPD_Column_TerminalCI,"", GlobalNames.sFPD_Column_Eco_CI_Start, GlobalNames.sFPD_Column_Eco_CI_End},
                                                                new String[]{GlobalNames.sFPD_Column_TerminalCI,"", GlobalNames.sFPD_Column_FB_CI_Start, GlobalNames.sFPD_Column_FB_CI_End},
                                                                new String[]{GlobalNames.sFPD_A_Column_TerminalGate, "", GlobalNames.sFPD_Column_BoardingGate}
                                                              };
                */
                // >> Task #9238 Pax2Sim - Validations - Validate flight plan Parking
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                // >> Task #17690 PAX2SIM - Flight Plan Parameters table
                #region Flight Plan Parameters
                lpTmp = new LoadParameters();

                lpTmp.sName = FPParametersTableConstants.TABLE_TECHNICAL_NAME;    //technical name (no special chars)
                lpTmp.sFullName = FPParametersTableConstants.TABLE_DISPLAY_NAME;    //display name

                lpTmp.bSaved = true;
                lpTmp.bActive = true;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_ARR_OR_DEP, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_FLIGHT_ID, typeof(Int32)));

                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_DATE, typeof(DateTime)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_FLIGHT_TIME, typeof(TimeSpan)));

                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_AirlineCode, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_FlightN, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_AirportCode, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_FlightCategory, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_AircraftType, typeof(String)));

                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPA_Column_NoBSM, typeof(Boolean)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPA_Column_CBP, typeof(Boolean)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_Column_TSA, typeof(Boolean)));

                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User1, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User2, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User3, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User4, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPD_A_Column_User5, typeof(String)));

                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_NB_ORIG_ECO_PAX, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_NB_ORIG_FB_PAX, typeof(Double)));

                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_NB_TRANSFER_ECO_PAX, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_NB_TRANSFER_FB_PAX, typeof(Double)));

                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_NB_TERM_ECO_PAX, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_NB_TERM_FB_PAX, typeof(Double)));

                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_NB_ORIG_ECO_BAGS, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_NB_ORIG_FB_BAGS, typeof(Double)));

                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_NB_TRANSFER_ECO_BAGS, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_NB_TRANSFER_FB_BAGS, typeof(Double)));

                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_NB_TERM_ECO_BAGS, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FPParametersTableConstants.COLUMN_NAME_NB_TERM_FB_BAGS, typeof(Double)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add("Arrival or Departure");
                lpTmp.lsPrimaryKeys.Add("Flight Id");

                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;

                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;
                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, true,
                                            new int[] { 0, 1 }, new int[] { 0, 1 },
                                            false, false,
                                            Color.White, Color.Blue, Color.Black, Color.White,
                                            VisualisationMode.SelectionModeEnum.Row, VisualisationMode.EditModeEnum.Row,
                                            new int[] { 0, 1 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion
                // << Task #17690 PAX2SIM - Flight Plan Parameters table

                #region FPLinksTable
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.FPLinksTableName;
                lpTmp.sFullName = "Aircraft Links";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPLinks_Column_FPAID, typeof(int)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPLinks_Column_STA, typeof(DateTime)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPLinks_Column_FPAName, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPLinks_Column_FPDID, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPLinks_Column_STD, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPLinks_Column_FPDName, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPLinks_Column_RotationDuration, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sFPLinks_Column_FPAID);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, true, null, new int[] { 0 }); ;   // >> Bug #13367 Liege allocation
                //lpForDistributionTable.tcChecks = new OverallTools.TableCheck(lpForDistributionTable.sName, OverallTools.TableCheck.eTypeAnalyze.FPLinksTable);
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName,
                    new String[] { GlobalNames.sFPLinks_Column_STA },
                    new String[] { GlobalNames.sFPLinks_Column_FPAID, GlobalNames.sFPLinks_Column_FPDID },
                    new String[] { GlobalNames.FPATableName, GlobalNames.FPDTableName },
                     null, null, null, -1, null, OverallTools.TableCheck.eTypeAnalyze.FPLinksTable);


                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Aircraft Types
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.FP_AircraftTypesTableName;
                lpTmp.sFullName = "Aircraft Types";
                lpTmp.bSaved = true;
                lpTmp.bActive = true;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPAircraft_AircraftTypes, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPAircraft_Description, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPAircraft_Wake, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPAircraft_Body, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPAircraft_NumberSeats, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sTableColumn_ULDLoose, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sFPAircraft_AircraftTypes);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, true, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row,
                    VisualisationMode.EditModeEnum.Row, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = true;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, new string[] { GlobalNames.sFPAircraft_NumberSeats });
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Airline Codes
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.FP_AirlineCodesTableName;
                lpTmp.sFullName = "Airline Codes";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPAirline_AirlineCode, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPAirline_Description, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPAirline_GroundHandlers, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sFPAirline_AirlineCode);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, true, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row,
                    VisualisationMode.EditModeEnum.Row, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Airport Codes
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.FP_AirportCodesTableName;
                lpTmp.sFullName = "Airport Codes";
                lpTmp.bSaved = true;
                lpTmp.bActive = true;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPAirport_AirportCode, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPAirport_Description, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sFPAirport_AirportCode);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, true, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row,
                    VisualisationMode.EditModeEnum.Row, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Flight Categories
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.FP_FlightCategoriesTableName;
                lpTmp.sFullName = "Flight Categories";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPFlightCategory_FC, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sFPFlightCategory_Description, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sFPFlightCategory_FC);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, true, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row,
                    VisualisationMode.EditModeEnum.Row, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Arrival Load Factors
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.FPA_LoadFactorsTableName;
                lpTmp.sFullName = "Arrival Load Factors";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] {GlobalNames.sLFD_A_Line_Full,
                                GlobalNames.sLFD_A_Line_C,
                                GlobalNames.sLFD_A_Line_Y,
                                GlobalNames.sLFD_A_Line_Local,
                                GlobalNames.sLFD_A_Line_NotLocal,
                                GlobalNames.sLFA_Line_Terminating,
                                GlobalNames.sLFD_A_Line_Transferring,
                                GlobalNames.sLFD_A_Line_ReCheck,
                                GlobalNames.sLFD_A_Line_TransferDesk,
                                GlobalNames.sLFD_A_Line_OOGTransf,
                                GlobalNames.sLFA_Line_OOGTerm,
                                GlobalNames.sLFD_A_Line_NbPaxPerCar}; // AndreiZaharia / Mulhouse project
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 90, 10, 90, 50, 50, 100, 0, 50, 0, 0, 0, 1 }; // AndreiZaharia / Mulhouse project
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, OverallTools.TableCheck.eTypeAnalyze.LoadFactorArrival);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Departure Load Factors
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.FPD_LoadFactorsTableName;
                lpTmp.sFullName = "Departure Load Factors";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] {GlobalNames.sLFD_A_Line_Full,
                                            GlobalNames.sLFD_A_Line_C,
                                            GlobalNames.sLFD_A_Line_Y,
                                            GlobalNames.sLFD_A_Line_Local,
                                            GlobalNames.sLFD_A_Line_NotLocal,
                                            GlobalNames.sLFD_Line_SelfCI,
                                            GlobalNames.sLFD_Line_Originating,
                                            GlobalNames.sLFD_A_Line_Transferring,
                                            GlobalNames.sLFD_A_Line_ReCheck,
                                            GlobalNames.sLFD_A_Line_TransferDesk,
                                            GlobalNames.sLFD_A_Line_OOGTransf,
                                            GlobalNames.sLFD_Line_OOGOrig,
                                            GlobalNames.sLFD_A_Line_NbPaxPerCar};  // AndreiZaharia / Mulhouse project
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 90.0, 10.0, 90.0, 50.0, 50.0, 50.0, 100.0, 0.0, 10.0, 0.0, 0.0, 0.0, 1.0 };  // AndreiZaharia / Mulhouse project
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, OverallTools.TableCheck.eTypeAnalyze.LoadFactorDeparture);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion
                #endregion

                #region Passenger
                aitTmp = ArboInputTable.getDirectory("Passenger");
                aitInputData.AddChild(aitTmp);

                #region Transfer_ICTTable
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Transfer_ICTTableName;
                lpTmp.sFullName = "Transfer InterConnecting Times";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnBegin, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnEnd, typeof(Int32)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnBegin);
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnEnd);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 0, 120, 100 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, false, new int[] { 0, 1 }, new int[] { 0, 1 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, new int[] { 0, 1 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, null, null, null, 2, null, OverallTools.TableCheck.eTypeAnalyze.None);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Transfer Terminal Distribution
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Transfer_TerminalDitributionTableName;
                lpTmp.sFullName = "Transfer Terminal Distribution";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                //lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTo, typeof(String)));   // >> Task #15867 Transfer Distribution Tables/Charts improvement
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnFrom, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnFrom);    // (GlobalNames.sColumnTo);  // >> Task #15867 Transfer Distribution Tables/Charts improvement
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = 0;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = true;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 1;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row,
                    VisualisationMode.EditModeEnum.Row, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = true;

                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, null, null, null, -1, null, OverallTools.TableCheck.eTypeAnalyze.TransfertDistribution);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Transfer Flight Category Distribution
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Transfer_FlightCategoryDitributionTableName;
                lpTmp.sFullName = "Transfer Flight Category Distribution";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                //lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTo, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnFrom, typeof(String))); // >> Task #15867 Transfer Distribution Tables/Charts improvement

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnFrom);//lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTo);    // >> Task #15867 Transfer Distribution Tables/Charts improvement
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = 0;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row,
                    VisualisationMode.EditModeEnum.Row, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = true;

                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, null, null, null, -1, null, OverallTools.TableCheck.eTypeAnalyze.TransfertDistribution);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Flight Group Rules
                lpTmp = new LoadParameters();

                lpTmp.sName = FlightGroupRulesEditor.FLIGHT_GROUP_RULES_TABLE_TECHNICAL_NAME;    //technical name (no special chars)
                lpTmp.sFullName = FlightGroupRulesEditor.FLIGHT_GROUP_RULES_TABLE_DISPLAYED_NAME;    //display name

                lpTmp.bSaved = true;
                lpTmp.bActive = true;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FlightGroupRulesEditor.FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_RULE_NAME, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FlightGroupRulesEditor.FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_FLIGHT_CATEGORIES, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FlightGroupRulesEditor.FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_AIRLINES, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FlightGroupRulesEditor.FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_AIRPORTS, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FlightGroupRulesEditor.FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_1, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FlightGroupRulesEditor.FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_2, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FlightGroupRulesEditor.FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_3, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FlightGroupRulesEditor.FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_4, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(FlightGroupRulesEditor.FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_USER_5, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(FlightGroupRulesEditor.FLIGHT_GROUP_RULES_TABLE_COLUMN_NAME_RULE_NAME);

                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;

                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;
                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, true,
                                            new int[] { 0 }, new int[] { 0 },
                                            false, false,
                                            Color.White, Color.Blue, Color.Black, Color.White,
                                            VisualisationMode.SelectionModeEnum.Row, VisualisationMode.EditModeEnum.Row,
                                            new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Deterministic Transfer Distribution
                lpTmp = new LoadParameters();

                lpTmp.sName = DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_TECHNICAL_NAME;    //technical name (no special chars)
                lpTmp.sFullName = DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_DISPLAYED_NAME;    //display name

                lpTmp.bSaved = true;
                lpTmp.bActive = true;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(
                    new LoadParameters.Entete(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_PLAN_NAME, typeof(String)));
                lpTmp.dstEntetes.Add(
                    new LoadParameters.Entete(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_ID, typeof(Int32)));
                lpTmp.dstEntetes.Add(
                    new LoadParameters.Entete(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_COMPLETE_DATE, typeof(DateTime)));
                lpTmp.dstEntetes.Add(
                    new LoadParameters.Entete(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_NB, typeof(String)));
                lpTmp.dstEntetes.Add(
                    new LoadParameters.Entete(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_CATEGORY, typeof(String)));
                lpTmp.dstEntetes.Add(
                    new LoadParameters.Entete(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_AIRLINE, typeof(String)));
                lpTmp.dstEntetes.Add(
                    new LoadParameters.Entete(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_AIRPORT, typeof(String)));
                lpTmp.dstEntetes.Add(
                    new LoadParameters.Entete(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_1, typeof(String)));
                lpTmp.dstEntetes.Add(
                    new LoadParameters.Entete(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_2, typeof(String)));
                lpTmp.dstEntetes.Add(
                    new LoadParameters.Entete(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_3, typeof(String)));
                lpTmp.dstEntetes.Add(
                    new LoadParameters.Entete(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_4, typeof(String)));
                lpTmp.dstEntetes.Add(
                    new LoadParameters.Entete(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_USER_5, typeof(String)));
                lpTmp.dstEntetes.Add(
                    new LoadParameters.Entete(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_TRANSFER_BAGS_DISTRIBUTION, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_PLAN_NAME);
                lpTmp.lsPrimaryKeys.Add(DeterministicTransferDistributionEditor.DETERMINISTIC_TRANSFER_DISTRIBUTION_TABLE_COLUMN_NAME_FLIGHT_ID);

                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;

                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;
                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, true,
                                            new int[] { 0, 1 }, new int[] { 0, 1 },
                                            false, false,
                                            Color.White, Color.Blue, Color.Black, Color.White,
                                            VisualisationMode.SelectionModeEnum.Row, VisualisationMode.EditModeEnum.Row,
                                            new int[] { 0, 1 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region CI Show-Up Distribution
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.CI_ShowUpTableName;
                lpTmp.sFullName = "CI Show-Up Distribution";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnBegin, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnEnd, typeof(Int32)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnBegin);
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnEnd);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 0, 120, 100 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, false, new int[] { 0, 1 }, new int[] { 0, 1 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, new int[] { 0, 1 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, null, null, null, 2, null, OverallTools.TableCheck.eTypeAnalyze.None);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Bags Distribution
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.NbBagsTableName;
                lpTmp.sFullName = "Bags Distribution";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnNbBags, typeof(Int32)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnNbBags);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 0, 100 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, false, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, null, null, null, 1, null, OverallTools.TableCheck.eTypeAnalyze.None);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Visitors Distribution
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.NbVisitorsTableName;
                lpTmp.sFullName = "Visitors Distribution";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnNbVisitors, typeof(Int32)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnNbVisitors);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 0, 100 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, false, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, null, null, null, 1, null, OverallTools.TableCheck.eTypeAnalyze.None);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Trolley Distribution
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.NbTrolleyTableName;
                lpTmp.sFullName = "Trolley Distribution";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnNbBags, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnNbTrolley, typeof(Int32)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnNbBags);
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnNbTrolley);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 0, 100 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, false, new int[] { 0, 1 }, new int[] { 0, 1 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, new int[] { 0, 1 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, null, null, null, 2, GlobalNames.sColumnNbBags, OverallTools.TableCheck.eTypeAnalyze.None);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region 26/03/2012 - SGE - Parking Mulhouse
                #region Parking_InitialStateTableName
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Parking_InitialStateTableName;
                lpTmp.sFullName = "Initial state for car parks";
                lpTmp.bSaved = PAX2SIM.bPKG;
                lpTmp.bActive = PAX2SIM.bPKG; lpTmp.bVisible = PAX2SIM.bPKG;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sInitialStateColumn_PaxInOut, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sInitialStateColumn_StartUpValue, typeof(Int32)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sInitialStateColumn_PaxInOut);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = 0;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = true;
                lpTmp.bSAirport_AnalyseColonne = false;
                lpTmp.iSAirport_TypeRecherche = 41;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, new String[] { GlobalNames.sInitialStateColumn_StartUpValue }, null, null, 2, null, OverallTools.TableCheck.eTypeAnalyze.None);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Parking_ShortStayTableName
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Parking_ShortStayTableName;
                lpTmp.sFullName = "Visitors Parking Occupation";
                lpTmp.bSaved = PAX2SIM.bPKG;
                lpTmp.bActive = PAX2SIM.bPKG; lpTmp.bVisible = PAX2SIM.bPKG;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnBegin, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnEnd, typeof(Int32)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnBegin);
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnEnd);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 0, 30, 100 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                /*SGE : 12/06/2012*/
                /*lpForDistributionTable.bFlightCategoryTable = true;
                lpForDistributionTable.bShemaAirportTable = false;*/

                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.iSAirport_TypeRecherche = 41;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                /* fin SGE : 12/06/2012*/
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, false, new int[] { 0, 1 }, new int[] { 0, 1 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, new int[] { 0, 1 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, null, null, null, 2, null, OverallTools.TableCheck.eTypeAnalyze.None);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Parking_LongStayTableName
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Parking_LongStayTableName;
                lpTmp.sFullName = "Pax Parking Occupation";
                lpTmp.bSaved = PAX2SIM.bPKG;
                lpTmp.bActive = PAX2SIM.bPKG; lpTmp.bVisible = PAX2SIM.bPKG;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnBegin, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnEnd, typeof(Int32)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnBegin);
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnEnd);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 0, 360, 100 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                /*SGE : 12/06/2012*/
                /*lpForDistributionTable.bFlightCategoryTable = true;
                lpForDistributionTable.bShemaAirportTable = false;*/

                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 41;
                /* fin SGE : 12/06/2012*/
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, false, new int[] { 0, 1 }, new int[] { 0, 1 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, new int[] { 0, 1 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, null, null, null, 2, null, OverallTools.TableCheck.eTypeAnalyze.None);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion
                #endregion //26/03/2012 - SGE - Parking Mulhouse

                //<< Task #7405 - new Desk and extra information for Pax
                #region User Attributes
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.sUserAttributesTableName;
                lpTmp.sFullName = "User Attributes";
                lpTmp.bSaved = true;
                lpTmp.bActive = true;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sUserAttributes_ColumnName, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sUserAttributes_ColumnName);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White,
                    VisualisationMode.SelectionModeEnum.Row, //VisualisationMode.SelectionModeEnum.Row,  // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                    VisualisationMode.EditModeEnum.None,    //VisualisationMode.EditModeEnum.Row,           // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
                    new int[] { 0 }, new int[] { 150 }, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = true;
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);

                ArboInputTable aitTmpUserAtt = ArboInputTable.getDirectory(GlobalNames.sUserAttributes_DirectoryName);
                aitTmp.AddChild(aitTmpUserAtt);
                #endregion
                //>> Task #7405 - new Desk and extra information for Pax
                #endregion

                #region Allocation & Planning
                aitTmp = ArboInputTable.getDirectory("Allocation & Planning");
                aitInputData.AddChild(aitTmp);

                #region OCT_BaggageClaimTable
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.OCT_BaggageClaimTableName;
                lpTmp.sFullName = "Baggage Claim (Open/Close Times)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] {GlobalNames.sOCT_Baggage_Line_Opening,
                                                      GlobalNames.sOCT_Baggage_Line_Closing,
                                                      GlobalNames.sOCT_Baggage_Line_ContainerSize,
                                                      GlobalNames.sOCT_Baggage_Line_NumberProcessedContainer,
                                                      GlobalNames.sOCT_Baggage_Line_DeadTime};

                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 0, 60, 40, 1, 0 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, false, true);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Alloc_BaggageClaimTable
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Alloc_BaggageClaimTableName;
                lpTmp.sFullName = "Baggage Claim (Allocation)";
                lpTmp.bSaved = false;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(String);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 6;

                lpTmp.bAllocationTable = true;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(false, false, false, null, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.None, new int[] { 0 }, null, null, null, null);

                cfcAllocation = new ConditionnalFormatCharacter(',', true);
                cfcAllocation.setCondition(1, Color.FromArgb(0, 255, 0));
                cfcAllocation.setCondition(2, Color.FromArgb(192, 255, 0));
                cfcAllocation.setCondition(3, Color.FromArgb(255, 255, 0));
                cfcAllocation.setCondition(4, Color.FromArgb(255, 128, 0));
                cfcAllocation.setCondition(5, Color.FromArgb(255, 0, 0));

                lpTmp.vmDefaultVisualisationMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfcAllocation };
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                //baggDrop (departure)
                #region OCT_BaggageDropTable
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.OCT_BaggDropTableName;
                lpTmp.sFullName = "Baggage Drop (Open/Close Times)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] { GlobalNames.sOCT_BagDropOpening,
                GlobalNames.sOCT_BagDropClosing };
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 120, 30 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, true, false);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                //arrGate
                #region OCT_ArrivalGateTable
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.OCT_ArrivalGateTableName;
                lpTmp.sFullName = "Arrival Gates (Open/Close Times)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] { GlobalNames.sOCT_Arrival_Gate_Line_Opening,
                GlobalNames.sOCT_Arrival_Gate_Line_Closing };
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 0, 30 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, false, true);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                //runway
                #region OCT_Runway
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.OCT_RunwayTableName;
                lpTmp.sFullName = "Runway (Open/Close Times)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);

                lpTmp.oDefaultFirstColumn = new String[] { GlobalNames.sOCT_RunwayOpening,
                                                           GlobalNames.sOCT_RunwayClosing };
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 0, 25 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);

                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White,
                    VisualisationMode.SelectionModeEnum.Column, VisualisationMode.EditModeEnum.Column,
                    null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;

                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, true, true);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion


                #region Passport Allocation
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Alloc_PassportCheckTableName;
                lpTmp.sFullName = "Passport Check (Planning)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;

#if (NEWALLOCATIONSECU)
                lpTmp.oDefaultValue = "0;" + GlobalNames.sAllocation_NotApplicable + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All;
#else
                lpTmp.oDefaultValue = "3,0";
#endif

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(String);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 7;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                //<< Task #7405 - new Desk and extra information for Pax                
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, GlobalNames.sUserAttributesTableName, OverallTools.TableCheck.eTypeAnalyze.PassportAllocation);
                //>> Task #7405 - new Desk and extra information for Pax
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Security Allocation
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Alloc_SecurityCheckTableName;
                lpTmp.sFullName = "Security Check (Planning)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;

#if(NEWALLOCATIONSECU)
                lpTmp.oDefaultValue = "0;" + GlobalNames.sAllocation_NotApplicable + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All;
#else
                lpTmp.oDefaultValue = "3,0";
#endif
                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(String);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 10;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                //<< Task #7405 - new Desk and extra information for Pax                
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, GlobalNames.sUserAttributesTableName, OverallTools.TableCheck.eTypeAnalyze.SecurityAllocation);
                //>> Task #7405 - new Desk and extra information for Pax
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                // << Task #7570 new Desk and extra information for Pax -Phase I B
                #region User Process Allocation
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Alloc_UserProcessCheckTableName;
                lpTmp.sFullName = "User Process (Planning)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;

#if(NEWALLOCATIONSECU)
                lpTmp.oDefaultValue = "0;" + GlobalNames.sAllocation_NotApplicable + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All;
#else
                lpTmp.oDefaultValue = "3,0";
#endif
                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(String);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 99;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;

                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, GlobalNames.sUserAttributesTableName, OverallTools.TableCheck.eTypeAnalyze.SecurityAllocation);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion
                // >> Task #7570 new Desk and extra information for Pax -Phase I B

                #region Transfer allocation
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Alloc_TransferDeskTableName;
                lpTmp.sFullName = "Transfer desks (Planning)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
#if(NEWALLOCATIONSECU)
                lpTmp.oDefaultValue = "0;" + GlobalNames.sAllocation_NotApplicable + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All + ";" +
                                GlobalNames.sAllocation_All;
#else
                lpTmp.oDefaultValue = "3,0";
#endif

                lpTmp.bAllowAddColumns = true;

#if (NEWALLOCATIONSECU)
                lpTmp.tTypeNewColumns = typeof(String);
#else
                lpTmp.tTypeNewColumns = typeof(Double);
#endif
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 8;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                //<< Task #7405 - new Desk and extra information for Pax                
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, GlobalNames.sUserAttributesTableName, OverallTools.TableCheck.eTypeAnalyze.TransferAllocation);
                //>> Task #7405 - new Desk and extra information for Pax
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Saturation Parameters
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Alloc_SaturationTableName;
                lpTmp.sFullName = "Saturation Parameters";
                lpTmp.bSaved = true;
                lpTmp.bActive = true;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sCapaQueue_Element, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sSaturation_ApplyRules, typeof(Boolean)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sSaturation_Filling, typeof(String)));  // << Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sSaturation_Opened, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sSaturation_Accumulation, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sSaturation_ReactionTime, typeof(Int32)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sCapaQueue_Element);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = true;
                lpTmp.bSAirport_AnalyseColonne = false;
                lpTmp.iSAirport_TypeRecherche = 40;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                // << Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation                
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, null,
                    null, null, -1, null, OverallTools.TableCheck.eTypeAnalyze.SaturationTable);
                // >> Task #9412 Pax2Sim - Scenario parameters files - Settings and OpeningOnSaturation

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                // >> Bug #13367 Liege allocation
                ArboInputTable checkInAllocationDirectory = ArboInputTable.getDirectory("Check-In Allocation");
                aitTmp.AddChild(checkInAllocationDirectory);

                #region OCT_CITable
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.OCT_CITableName;
                lpTmp.sFullName = "Check-In (Open/Close Times)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] { GlobalNames.sOCT_CI_Line_Opening,
                    GlobalNames.sOCT_CI_Line_Closing,
                    GlobalNames.sOCT_CI_Line_PartialOpeningTime,    // >> Bug #13367 Liege allocation
                    GlobalNames.sOCT_CI_Line_Allocated,
                    GlobalNames.sOCT_CI_Line_NbStationsOpenedAtPartial,
                    GlobalNames.sOCT_CI_Line_NbAdditionalStationsForOverlappingFlights};
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 120, 30, 120, 1, 1, -1 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, true, false);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                checkInAllocationDirectory.AddChild(lpTmp);
                #endregion

                #region Alloc_CheckIn
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Alloc_CITableName;
                lpTmp.sFullName = "Check-In (Allocation)";
                lpTmp.bSaved = false;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(String);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 4;

                lpTmp.bAllocationTable = true;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(false, false, false, null, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.None, new int[] { 0 }, null, null, null, null);

                cfcAllocation = new ConditionnalFormatCharacter(',', true);
                cfcAllocation.setCondition(1, Color.FromArgb(0, 255, 0));
                cfcAllocation.setCondition(2, Color.FromArgb(192, 255, 0));
                cfcAllocation.setCondition(3, Color.FromArgb(255, 255, 0));
                cfcAllocation.setCondition(4, Color.FromArgb(255, 192, 0));
                cfcAllocation.setCondition(5, Color.FromArgb(255, 128, 0));
                cfcAllocation.setCondition(6, Color.FromArgb(255, 64, 0));
                cfcAllocation.setCondition(7, Color.FromArgb(255, 0, 0));

                lpTmp.vmDefaultVisualisationMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfcAllocation };
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                checkInAllocationDirectory.AddChild(lpTmp);
                #endregion

                #region Check-In (Opening)
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Opening_CITableName;
                lpTmp.sFullName = "Check-In (Opening)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Boolean);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 4;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                checkInAllocationDirectory.AddChild(lpTmp);
                #endregion

                ArboInputTable boardingGatesAllocationDirectory = ArboInputTable.getDirectory("Boarding Gates Allocation");
                aitTmp.AddChild(boardingGatesAllocationDirectory);

                #region OCT_BoardGateTable
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.OCT_BoardGateTableName;
                lpTmp.sFullName = "Boarding Gates (Open/Close Times)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] { GlobalNames.sOCT_Board_Line_Opening,
                GlobalNames.sOCT_Board_Line_Closing };
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 30, 0 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, true, false);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                boardingGatesAllocationDirectory.AddChild(lpTmp);
                #endregion

                #region Boarding Gates (Allocation)
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Alloc_BoardGateTableName;
                lpTmp.sFullName = "Boarding Gates (Allocation)";
                lpTmp.bSaved = false;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(String);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 5;

                lpTmp.bAllocationTable = true;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(false, false, false, null, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.None, new int[] { 0 }, null, null, null, null);

                cfcAllocation = new ConditionnalFormatCharacter(',', true);
                cfcAllocation.setCondition(1, Color.FromArgb(0, 255, 0));
                cfcAllocation.setCondition(2, Color.FromArgb(192, 255, 0));
                cfcAllocation.setCondition(3, Color.FromArgb(255, 255, 0));
                cfcAllocation.setCondition(4, Color.FromArgb(255, 128, 0));
                cfcAllocation.setCondition(5, Color.FromArgb(255, 0, 0));

                lpTmp.vmDefaultVisualisationMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfcAllocation };
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                boardingGatesAllocationDirectory.AddChild(lpTmp);
                #endregion

                #region Boarding Gates Priority
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.boardingGatesPrioritiesTableName;
                lpTmp.sFullName = "Boarding Gates Priorities";
                lpTmp.bSaved = true;
                lpTmp.bActive = true;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] {GlobalNames.boardingGatesPrioritiesRowBG1, GlobalNames.boardingGatesPrioritiesRowBG2,
                    GlobalNames.boardingGatesPrioritiesRowBG3, GlobalNames.boardingGatesPrioritiesRowBG4};
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 1, 2, 3, 4 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = false;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;
                //lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, true, true);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                //aitTmp.AddChild(lpTmp);
                boardingGatesAllocationDirectory.AddChild(lpTmp);
                #endregion

                ArboInputTable parkingAllocationDirectory = ArboInputTable.getDirectory("Parking Allocation");
                aitTmp.AddChild(parkingAllocationDirectory);

                #region OCT_ParkingTable
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.OCT_ParkingTableName;
                lpTmp.sFullName = "Parking (Open/Close Times)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] {GlobalNames.sOCT_ParkingOpening,
                                            GlobalNames.sOCT_ParkingClosing};
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 25, 0 };  // { 0, 25 }; // >> Bug #13367 Liege allocation
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;
                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, true, true);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                //aitTmp.AddChild(lpTmp);
                parkingAllocationDirectory.AddChild(lpTmp);
                #endregion

                #region Parking (Allocation)
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Alloc_ParkingTableName;
                lpTmp.sFullName = "Parking (Allocation)";
                lpTmp.bSaved = false;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(String);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 18;

                lpTmp.bAllocationTable = true;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(false, false, false, null, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.None, new int[] { 0 }, null, null, null, null);

                cfcAllocation = new ConditionnalFormatCharacter(',', true);
                cfcAllocation.setCondition(1, Color.FromArgb(0, 255, 0));
                cfcAllocation.setCondition(2, Color.FromArgb(192, 255, 0));
                cfcAllocation.setCondition(3, Color.FromArgb(255, 255, 0));
                cfcAllocation.setCondition(4, Color.FromArgb(255, 128, 0));
                cfcAllocation.setCondition(5, Color.FromArgb(255, 0, 0));

                lpTmp.vmDefaultVisualisationMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfcAllocation };
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                //aitTmp.AddChild(lpTmp);
                parkingAllocationDirectory.AddChild(lpTmp);
                #endregion

                #region Parking Priority
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.parkingPrioritiesTableName;
                lpTmp.sFullName = "Parking Priorities";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] {GlobalNames.parkingPrioritiesRowP20, GlobalNames.parkingPrioritiesRowP21, GlobalNames.parkingPrioritiesRowP22,
                    GlobalNames.parkingPrioritiesRowP23, GlobalNames.parkingPrioritiesRowP24, GlobalNames.parkingPrioritiesRowP25, GlobalNames.parkingPrioritiesRowP26,
                    GlobalNames.parkingPrioritiesRowP27, GlobalNames.parkingPrioritiesRowP28, GlobalNames.parkingPrioritiesRowP41, GlobalNames.parkingPrioritiesRowP42,
                    GlobalNames.parkingPrioritiesRowP43};
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = false;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;
                //lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, true, true);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                //aitTmp.AddChild(lpTmp);
                parkingAllocationDirectory.AddChild(lpTmp);
                #endregion
                // << Bug #13367 Liege allocation

                ArboInputTable aitTmp2 = ArboInputTable.getDirectory("Make-Up Allocation");
                aitTmp.AddChild(aitTmp2);
                #region OCT_MakeUp
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.OCT_MakeUpTableName;
                lpTmp.sFullName = "Make-Up (Open/Close Times)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] {
                                            GlobalNames. sOCT_MakeUpOpening ,
                                            GlobalNames. sOCT_MakeUpClosing ,
                                            GlobalNames. sOCT_MakeUpEBS_Delivery ,
                                            GlobalNames. sOCT_MakeUpAllocatedMup,
                                            GlobalNames. sOCT_MakeUpPartialOpening ,
                                            GlobalNames. sOCT_MakeUpPartialAllocatedMup  ,
                                            GlobalNames. sOCT_MakeUpSegregationNumber,
                                            GlobalNames. sOCT_MakeUpContainerSize,
                                            GlobalNames. sOCT_MakeUpDeadTime,
                                            GlobalNames. sOCT_MakeUpNumberContainerLateral
                                            ,GlobalNames.OCT_MAKE_UP_LATE_TIME_COLUMN_NAME};// New Mup OCT parameter
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 120, 30, 90, 3, 120, 3, 3, 40, 60, 3, 15 };// New Mup OCT parameter
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp2.AddChild(lpTmp);
                #endregion

                #region Segregation
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.SegregationName;
                lpTmp.sFullName = "Segregation";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSegregation, typeof(Int32)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSegregation);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 1, 100 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, false, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp2.AddChild(lpTmp);
                #endregion

                //arrInf
                aitTmp2 = ArboInputTable.getDirectory("Arrival Infeed Allocation");
                aitTmp.AddChild(aitTmp2);
                #region Arrival Infeed OCT
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.OCT_ArrivalInfeedTableName;
                lpTmp.sFullName = "Arrival Infeed (Open/Close Times)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] {
                                            GlobalNames.sOCT_Arrival_Infeed_Line_Opening ,
                                            GlobalNames.sOCT_Arrival_Infeed_Line_Closing };
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 10, 30 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;


                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;

                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, false, true);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp2.AddChild(lpTmp);
                #endregion

                aitTmp2 = ArboInputTable.getDirectory("Transfer Infeed Allocation");
                aitTmp.AddChild(aitTmp2);
                #region TransferInfeedAllocationRulesTable
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.TransferInfeedAllocationRulesTableName;
                lpTmp.sFullName = "Transfer Infeed (Open/Close Times)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] {
                                            GlobalNames.sOCT_TransferInfeedOpening ,
                                            GlobalNames.sOCT_TransferInfeedClosing ,
                                            GlobalNames.sOCT_TransferInfeedAllocatedInfeed};
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] { 10, 30, 2 };
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = true;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;


                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp2.AddChild(lpTmp);
                #endregion

                #region Baggage_Claim_Constraint
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Baggage_Claim_ConstraintName;
                lpTmp.sFullName = "Baggage Claim Constraint";
                lpTmp.bSaved = false;// PAX2SIM.bShowAllocationTools;
                lpTmp.bActive = true;
                lpTmp.bVisible = false;// PAX2SIM.bShowAllocationTools;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
                lpTmp.oDefaultFirstColumn = new String[] {
                                            GlobalNames.sBaggageClaimConstraintInfeedNumber,
                                            GlobalNames.sBaggageClaimConstraintInfeedSpeed,
                                            GlobalNames.sBaggageClaimConstraintLimitBags,
                                            GlobalNames.sBaggageClaimConstraintMinNumberBags ,
                                            GlobalNames.sBaggageClaimConstraintMaxNumberBag ,
                                            GlobalNames.sBaggageClaimConstraintExcludedCategories ,
                                            GlobalNames.sBaggageClaimConstraintExcludedGroundHandler,
                                            GlobalNames.sBaggageClaimConstraintExcludedAirline,
                                            GlobalNames.sBaggageClaimConstraintExcludedFlight ,
                                            GlobalNames.sBaggageClaimConstraintExcludedContainerType};
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = new Double[] {0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0.0,
                                    0,
                                    0,
                                    0,
                                    0,
                                    0};
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(String);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 6;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;


                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.vmDefaultVisualisationMode.AllowEditRow = false;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #endregion

                #region Airport Process
                aitTmp = ArboInputTable.getDirectory("Airport Process");
                aitInputData.AddChild(aitTmp);

                #region Pax Process Times
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Times_ProcessTableName;
                lpTmp.sFullName = "Pax Process Times";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_Items, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_Distrib_1, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_Param1_1, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_Param2_1, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_Param3_1, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_Distrib_2, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_Param1_2, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_Param2_2, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_Param3_2, typeof(Double)));
                // << Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_WaitingTimeReference, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_Distrib_3, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_Param1_3, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_Param2_3, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sProcessTable_Param3_3, typeof(Double)));
                // >> Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sProcessTable_Items);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = true;
                lpTmp.bSAirport_AnalyseColonne = false;
                lpTmp.iSAirport_TypeRecherche = 3;

                lpTmp.bAllocationTable = true;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, true, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row,
                    VisualisationMode.EditModeEnum.Row, new int[] { 0 }, null, null, null, null);

                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, null, new String[] { GlobalNames.sProcessTable_Distrib_1, GlobalNames.sProcessTable_Distrib_2 }, GlobalNames.OneofSpecificationTableName, 2, GlobalNames.sColumnNbBags, OverallTools.TableCheck.eTypeAnalyze.ProcessTable);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                // << Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
                #region Pax Process Capacities
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.capaProcessTableName;
                lpTmp.sFullName = GlobalNames.capaProcessTableFullName;
                lpTmp.bSaved = true;
                lpTmp.bActive = true;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sCapaQueue_Element, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.capaProcessTableProcessCapacityColumnName, typeof(Int32)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sCapaQueue_Element);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = 1;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = true;
                lpTmp.bSAirport_AnalyseColonne = false;
                lpTmp.iSAirport_TypeRecherche = 9;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, true, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion
                // >> Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)

                #region Pax Itinerary
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.ItineraryTableName;
                lpTmp.sFullName = "Pax Itinerary";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_Group, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_NextGroup, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_Weight, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_Distribution, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_Param1, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_Param2, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_Param3, typeof(Double)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sItineraryTable_Group);
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sItineraryTable_NextGroup);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = true;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, true, new int[] { 0, 1 }, new int[] { 0, 1 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row,
                    VisualisationMode.EditModeEnum.Row, new int[] { 0, 1 }, null, null, null, null);

                lpTmp.tcChecks = new OverallTools.TableCheck(lpTmp.sName, null, null, null, null, new String[] { GlobalNames.sItineraryTable_Distribution }, GlobalNames.OneofSpecificationTableName, 2, GlobalNames.sColumnNbBags, OverallTools.TableCheck.eTypeAnalyze.None);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Distribution Pax In
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.ProcessDistributionPaxInName;
                lpTmp.sFullName = GlobalNames.ProcessDistributionPaxInFullName;
#if(PAXINOUTUTILISATION)
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;
#else
                lpTmp.bSaved = false;
                lpTmp.bActive = false; lpTmp.bVisible = false;
#endif

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTo, typeof(String)));




                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTo);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = true;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 37;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row,
                    VisualisationMode.EditModeEnum.Row, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Distribution Pax Out
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.ProcessDistributionPaxOutName;
                lpTmp.sFullName = GlobalNames.ProcessDistributionPaxOutFullName;
#if(PAXINOUTUTILISATION)
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;
#else
                lpTmp.bSaved = false;
                lpTmp.bActive = false; lpTmp.bVisible = false;
#endif

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTo, typeof(String)));




                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTo);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = true;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 38;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row,
                    VisualisationMode.EditModeEnum.Row, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Process Schedule
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.ProcessScheduleName;
                lpTmp.sFullName = GlobalNames.sTableProcessScheduleLong;
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnBegin, typeof(DateTime)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnEnd, typeof(DateTime)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sTableProcessScheduleItinerary, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sTableProcessScheduleProcess, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sTableProcessScheduleGroupQueues, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sTableProcessScheduleProcessQueues, typeof(String)));
                // << Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sTableProcessScheduleProcessCapacity, typeof(String)));
                // >> Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)



                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnBegin);
                lpTmp.oDefaultFirstColumn = null;
                // << Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
                //added a new column for the CapaProcess table
                lpTmp.oDefaultLine = new Object[] { "", "", GlobalNames.ItineraryTableName,
                    GlobalNames.Times_ProcessTableName, GlobalNames.Group_QueuesName, GlobalNames.Capa_QueuesTableName,
                    GlobalNames.capaProcessTableName};
                // >> Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, true, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Parking (Shuttle)
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.ParkingTableName;
                lpTmp.sFullName = "Parking";
                lpTmp.bSaved = false;
                lpTmp.bActive = true; lpTmp.bVisible = false;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sParkingTable_Parking, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sParkingTable_Shuttle, typeof(Boolean)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sParkingTable_Parking);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row,
                    VisualisationMode.EditModeEnum.Row, new int[] { 0 }, null, null, null, null);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region From To Shuttle
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.FromToShuttleTableName;
                lpTmp.sFullName = "From - To - Shuttle";
                lpTmp.bSaved = false;
                lpTmp.bActive = true; lpTmp.bVisible = false;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnFromTo, typeof(String)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnFromTo);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 12;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Probability Profiles
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.OneofSpecificationTableName;
                lpTmp.sFullName = "Probability Profiles";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, null, null, false, false, Color.White, Color.Blue, Color.Black,
                                                        Color.White, VisualisationMode.SelectionModeEnum.Column, VisualisationMode.EditModeEnum.Column,
                                                        null, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #endregion

                #region Airport Area Capacity
                aitTmp = ArboInputTable.getDirectory("Airport Area Capacity");
                aitInputData.AddChild(aitTmp);

                #region Process Queues (Capacities)
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Capa_QueuesTableName;
                lpTmp.sFullName = "Process Queues (Capacities)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sCapaQueue_Element, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sCapaQueue_QueueCapacity, typeof(Int32)));




                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sCapaQueue_Element);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = 1;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = true;
                lpTmp.bSAirport_AnalyseColonne = false;
                lpTmp.iSAirport_TypeRecherche = 9;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, true, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Group Queues capacities
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Group_QueuesName;
                lpTmp.sFullName = "Group Queues (Capacities)";
                lpTmp.bSaved = true;
                lpTmp.bActive = true; lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sCapaQueue_Element, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sCapaQueue_QueueCapacity, typeof(Int32)));




                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sCapaQueue_Element);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = -1;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = true;
                lpTmp.bSAirport_AnalyseColonne = false;
                lpTmp.iSAirport_TypeRecherche = 30;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, true, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Animated Queues
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.Animated_QueuesName;
                lpTmp.sFullName = "Animated Queues";
                lpTmp.bSaved = PAX2SIM.bAnimatedQueues;
                lpTmp.bActive = true;
                lpTmp.bVisible = PAX2SIM.bAnimatedQueues;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sAnimatedQueue_Name, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sAnimatedQueue_Value, typeof(Int32)));




                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sAnimatedQueue_Name);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = 1;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(false, false, true, new int[] { 0 }, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;
                lpTmp.tcChecks = new OverallTools.TableCheck("Animated_Queues", OverallTools.TableCheck.eTypeAnalyze.AnimatedQueues);
                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #endregion

            }
            if (PAX2SIM.bPKG)
            {
                #region Pour la gestion des Parkings
                aitTmp = ArboInputTable.getDirectory("Parking");
                aitTmp.Visible = false;
                aitInputData.AddChild(aitTmp);

                #region Parking_General
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.sParkingGeneralName;
                lpTmp.sFullName = GlobalNames.sParkingGeneralFullName;
                lpTmp.bSaved = PAX2SIM.bPKG;
                lpTmp.bActive = PAX2SIM.bPKG;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHSGeneralTable_Data, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sParkingColumnTotal, typeof(Double)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sBHSGeneralTable_Data);
                lpTmp.oDefaultFirstColumn = new String[] {
                                    //GlobalNames.sParkingGeneralLine1,
                                    //GlobalNames.sParkingGeneralLine2,
                                    //GlobalNames.sParkingGeneralLine3,
                                    //GlobalNames.sParkingGeneralLine4,
                                    //GlobalNames.sParkingGeneralLine5,
                                    //GlobalNames.sParkingGeneralLine6,
                                    GlobalNames.sParkingGeneralLine7,
                                    //GlobalNames.sParkingGeneralLine8,
                                    //GlobalNames.sParkingGeneralLine9,
                                    //GlobalNames.sParkingGeneralLine10,
                                    //GlobalNames.sParkingGeneralLine11,
                                    GlobalNames.sParkingGeneralLine12,
                                    GlobalNames.sParkingGeneralLine16,
                                    GlobalNames.sParkingGeneralLine17,
                                    GlobalNames.sParkingGeneralLine18,
                                    GlobalNames.sParkingGeneralLine19,
                                    GlobalNames.sParkingGeneralLine20,
                                    GlobalNames.sParkingGeneralLine21
                                     };

                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = 0.0;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 35;     //Exception pour cette table.

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                ConditionnalFormatErrors cfeErrors = new ConditionnalFormatErrors();
                for (int i = 0; i < GestionDonneesHUB2SIM.NbTerminaux; i++)
                {
                    //cfeErrors.setFormat(i+2, 0, "", " Pax");
                    //cfeErrors.setFormat(i+2, 4, "", " Pax");
                    //cfeErrors.setFormat(i+2, 5, "", " Pax");
                    cfeErrors.setFormat(i + 2, 0, "", " Pax");
                    //cfeErrors.setFormat(i+2, 7, "", " Pax");
                    //cfeErrors.setFormat(i+2, 8, "", " Pax");
                    //cfeErrors.setFormat(i+2, 9, "", " Pax");
                    //cfeErrors.setFormat(i+2, 10, "", " Pax");
                    cfeErrors.setFormat(i + 2, 1, "", " %");
                    cfeErrors.setFormat(i + 2, 2, "", " Pax");
                    cfeErrors.setFormat(i + 2, 3, "", " Vehicles");
                    cfeErrors.setFormat(i + 2, 4, "", " %");
                    cfeErrors.setFormat(i + 2, 5, "", " Vehicles");
                    cfeErrors.setFormat(i + 2, 6, "", " Vehicles");
                    cfeErrors.setFormat(i + 2, 7, "", " Vehicles");

                }
                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true, Color.White,
                            Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell, VisualisationMode.EditModeEnum.Cell,
                            null, new int[] { 150, 60 }, null, null, new VisualisationMode.ConditionnalFormat[] { cfeErrors });

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Répartition modale
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.sParkingModaleDistribName;
                lpTmp.sFullName = GlobalNames.sParkingModaleDistribFullName;
                lpTmp.bSaved = PAX2SIM.bPKG;
                lpTmp.bActive = PAX2SIM.bPKG;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHSGeneralTable_Data, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sParkingColumnTotal, typeof(Double)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sBHSGeneralTable_Data);
                lpTmp.oDefaultFirstColumn = new String[] {
                                 GlobalNames.sParkingModalDistribLine1,
                                GlobalNames.sParkingModalDistribLine2,
                                GlobalNames.sParkingModalDistribLine3,
                                GlobalNames.sParkingModalDistribLine4 };

                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = 0.0;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 35;     //Exception pour cette table.

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                cfeErrors = new ConditionnalFormatErrors();
                for (int i = 0; i < GestionDonneesHUB2SIM.NbTerminaux; i++)
                {
                    cfeErrors.setFormat(i + 2, 0, "", " %");
                    cfeErrors.setFormat(i + 2, 1, "", " %");
                    cfeErrors.setFormat(i + 2, 2, "", " %");
                    cfeErrors.setFormat(i + 2, 3, "", " %");
                }
                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true, Color.White,
                            Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell, VisualisationMode.EditModeEnum.Cell,
                            null, new int[] { 150, 60 }, null, null, new VisualisationMode.ConditionnalFormat[] { cfeErrors });

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region DistribRushTime
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.sParkingDitributionTimeName;
                lpTmp.sFullName = GlobalNames.sParkingDitributionTimeFullName;
                lpTmp.bSaved = PAX2SIM.bPKG;
                lpTmp.bActive = PAX2SIM.bPKG;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHSGeneralTable_Data, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sParkingColumnTotal, typeof(Double)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sBHSGeneralTable_Data);
                lpTmp.oDefaultFirstColumn = null;

                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = 0.0;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = true;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 36;     //Exception pour cette table.

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;
                cfeErrors = new ConditionnalFormatErrors();
                for (int i = 0; i < GestionDonneesHUB2SIM.NbTerminaux; i++)
                {
                    for (int j = 0; j < GestionDonneesHUB2SIM.NbTerminaux; j++)
                    {
                        cfeErrors.setFormat(i + 2, j * 2, "", " Pax");
                        cfeErrors.setFormat(i + 2, j * 2 + 1, "", " %");
                    }
                }
                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true, Color.White,
                            Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell, VisualisationMode.EditModeEnum.Cell,
                            null, new int[] { 150, 60 }, null, null, new VisualisationMode.ConditionnalFormat[] { cfeErrors });

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Temps d'occupation
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.sParkingOccupationTimeName;
                lpTmp.sFullName = GlobalNames.sParkingOccupationTimeFullName;
                lpTmp.bSaved = PAX2SIM.bPKG;
                lpTmp.bActive = PAX2SIM.bPKG;
                lpTmp.bVisible = true;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                /*lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHSGeneralTable_Data, typeof(String)));*/

                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnBegin, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnEnd, typeof(Int32)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sParkingColumnTotal, typeof(Double)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnBegin);
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnEnd);
                lpTmp.oDefaultFirstColumn = null;/*new String[] {
                                 GlobalNames.sParkingOccupationTimeLine1,
                                GlobalNames.sParkingOccupationTimeLine2,
                                GlobalNames.sParkingOccupationTimeLine3,
                                GlobalNames.sParkingOccupationTimeLine4,
                                GlobalNames.sParkingOccupationTimeLine5,
                                GlobalNames.sParkingOccupationTimeLine6,
                                GlobalNames.sParkingOccupationTimeLine7,
                                GlobalNames.sParkingOccupationTimeLine8,
                                GlobalNames.sParkingOccupationTimeLine9,
                                GlobalNames.sParkingOccupationTimeLine10,
                                GlobalNames.sParkingOccupationTimeLine11,
                                GlobalNames.sParkingOccupationTimeLine12,
                                GlobalNames.sParkingOccupationTimeLine13,
                                GlobalNames.sParkingOccupationTimeLine14
                                 };*/

                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = 0.0;

                lpTmp.bAllowAddColumns = true;
                lpTmp.tTypeNewColumns = typeof(Double);
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = false;
                lpTmp.bSAirport_AnalyseColonne = true;
                lpTmp.iSAirport_TypeRecherche = 39;     //Exception pour cette table.

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = false;
                lpTmp.bIndexedOnTerminal = false;

                cfeErrors = new ConditionnalFormatErrors();
                for (int j = 0; j < 30; j++)
                {
                    cfeErrors.setFormat(2, j, "", " h");
                }
                for (int i = 0; i < GestionDonneesHUB2SIM.NbTerminaux; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        cfeErrors.setFormat(i + 3, j, "", " %");
                    }
                }
                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, true, false,/* new int[] { 0, 1 }*/null, new int[] { 0, 1 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, new int[] { 0, 1 }, null, null, null, new VisualisationMode.ConditionnalFormat[] { cfeErrors });


                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);

                #endregion

                #region  /*Temps d'occupation*/
                /*lpForDistributionTable = new LoadParameters();
                lpForDistributionTable.sName = GlobalNames.sParkingOccupationTimeName;
                lpForDistributionTable.sFullName = GlobalNames.sParkingOccupationTimeFullName;
                lpForDistributionTable.bSaved = PAX2SIM.bPKG;
                lpForDistributionTable.bActive = PAX2SIM.bPKG;
                lpForDistributionTable.bVisible = true;

                lpForDistributionTable.dstEntetes = new List<LoadParameters.Entete>();
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHSGeneralTable_Data, typeof(String)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sParkingColumnTotal, typeof(Double)));

                lpForDistributionTable.lsPrimaryKeys = new List<string>();
                lpForDistributionTable.lsPrimaryKeys.Add(GlobalNames.sBHSGeneralTable_Data);
                lpForDistributionTable.oDefaultFirstColumn = new String[] {
                                 GlobalNames.sParkingOccupationTimeLine1,
                                GlobalNames.sParkingOccupationTimeLine2,
                                GlobalNames.sParkingOccupationTimeLine3,
                                GlobalNames.sParkingOccupationTimeLine4,
                                GlobalNames.sParkingOccupationTimeLine5,
                                GlobalNames.sParkingOccupationTimeLine6,
                                GlobalNames.sParkingOccupationTimeLine7,
                                GlobalNames.sParkingOccupationTimeLine8,
                                GlobalNames.sParkingOccupationTimeLine9,
                                GlobalNames.sParkingOccupationTimeLine10,
                                GlobalNames.sParkingOccupationTimeLine11,
                                GlobalNames.sParkingOccupationTimeLine12,
                                GlobalNames.sParkingOccupationTimeLine13,
                                GlobalNames.sParkingOccupationTimeLine14
                                 };

                lpForDistributionTable.oDefaultLine = null;
                lpForDistributionTable.oDefaultColumn = null;
                lpForDistributionTable.oDefaultValue = 0.0;

                lpForDistributionTable.bAllowAddColumns = true;
                lpForDistributionTable.tTypeNewColumns = typeof(Double);
                lpForDistributionTable.bFlightCategoryTable = false;
                lpForDistributionTable.bShemaAirportTable = true;
                lpForDistributionTable.bSAirport_AnalyseLigne = false;
                lpForDistributionTable.bSAirport_AnalyseColonne = true;
                lpForDistributionTable.iSAirport_TypeRecherche = 35;     //Exception pour cette table.

                lpForDistributionTable.bAllocationTable = false;

                lpForDistributionTable.bBHS = false;
                lpForDistributionTable.bIndexedOnTerminal = false;

                cfeErrors = new ConditionnalFormatErrors();
                for (int i = 0; i < GestionDonneesHUB2SIM.NbTerminaux; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        cfeErrors.setFormat(i + 2, j, "", " %");
                    }
                }
                lpForDistributionTable.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true, Color.White,
                            Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell, VisualisationMode.EditModeEnum.Cell,
                            null, new int[] { 150, 60 }, null, null, new VisualisationMode.ConditionnalFormat[] { cfeErrors });

                dslpTableParameters.Add(lpForDistributionTable.sName, lpForDistributionTable);
                aitTmp.AddChild(lpForDistributionTable);*/
                #endregion


                #endregion
            }
            if (PAX2SIM.bBHS)
            {
                aitTmp = ArboInputTable.getDirectory(GestionDonneesHUB2SIM.sBHSAnalysis);
                aitInputData.AddChild(aitTmp);
                ///Par défaut le noeud BHS ne sera pas visible, (même s'il sera enregistré (dans le cas où
                ///la clef de protection autorise le mode BHS).
                aitTmp.Visible = false;
                #region BHS Global
                #region Process Queues (Capacities)
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.sBHS_Capa_Queues;
                lpTmp.sFullName = "BHS Process Queues (Capacities)";
                lpTmp.bSaved = false;
                lpTmp.bActive = PAX2SIM.bDebug;
                lpTmp.bVisible = PAX2SIM.bDebug;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sCapaQueue_Element, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sCapaQueue_QueueCapacity, typeof(Int32)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sCapaQueue_Element);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = 1;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = true;
                lpTmp.bSAirport_AnalyseColonne = false;
                lpTmp.iSAirport_TypeRecherche = 31;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = true;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region Group Queues capacities
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.sBHS_Group_Queues;
                lpTmp.sFullName = "BHS Group Queues (Capacities)";
                lpTmp.bSaved = false;
                lpTmp.bActive = PAX2SIM.bDebug;
                lpTmp.bVisible = PAX2SIM.bDebug;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sCapaQueue_Element, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sCapaQueue_QueueCapacity, typeof(Int32)));




                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sCapaQueue_Element);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = 1;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = true;
                lpTmp.bSAirport_AnalyseLigne = true;
                lpTmp.bSAirport_AnalyseColonne = false;
                lpTmp.iSAirport_TypeRecherche = 32;

                lpTmp.bAllocationTable = false;

                lpTmp.bBHS = true;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                    VisualisationMode.EditModeEnum.Cell, new int[] { 0 }, null, null, null, null);
                lpTmp.vmDefaultVisualisationMode.AllowEditColumn = true;

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #region BHS Itinerary
                lpTmp = new LoadParameters();
                lpTmp.sName = GlobalNames.sBHS_Itinerary;
                lpTmp.sFullName = "BHS Itinerary table";
                lpTmp.bSaved = false;
                lpTmp.bActive = PAX2SIM.bDebug;
                lpTmp.bVisible = PAX2SIM.bDebug;

                lpTmp.dstEntetes = new List<LoadParameters.Entete>();
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_Group, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_NextGroup, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_Succeed, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_Distribution, typeof(String)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_Param1, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_Param2, typeof(Double)));
                lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sItineraryTable_Param3, typeof(Double)));

                lpTmp.lsPrimaryKeys = new List<string>();
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sItineraryTable_Group);
                lpTmp.lsPrimaryKeys.Add(GlobalNames.sItineraryTable_NextGroup);
                lpTmp.oDefaultFirstColumn = null;
                lpTmp.oDefaultLine = null;
                lpTmp.oDefaultColumn = null;
                lpTmp.oDefaultValue = null;

                lpTmp.bAllowAddColumns = false;
                lpTmp.tTypeNewColumns = null;
                lpTmp.bFlightCategoryTable = false;
                lpTmp.bShemaAirportTable = false;
                lpTmp.bAllocationTable = true;

                lpTmp.bBHS = true;
                lpTmp.bIndexedOnTerminal = false;

                lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0, 1 }, new int[] { 0, 1 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row,
                    VisualisationMode.EditModeEnum.Row, new int[] { 0, 1 }, null, null, null, null);

                dslpTableParameters.Add(lpTmp.sName, lpTmp);
                aitTmp.AddChild(lpTmp);
                #endregion

                #endregion

            }
            aitTmp = ArboInputTable.getDirectory("User Data");
            aitInputData.AddChild(aitTmp);

            #region /**/
            /*

            #region DepartureParkin
            lpForDistributionTable = new LoadParameters();
            lpForDistributionTable.sName = GlobalNames.Alloc_BaggageClaimTableName;
            lpForDistributionTable.sFullName = "Baggage Claim (Allocation)";
            lpForDistributionTable.bSaved = false;
            lpForDistributionTable.bActive = true;lpForDistributionTable.bVisible = true;

            lpForDistributionTable.dstEntetes = new List<LoadParameters.Entete>();
            lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

            lpForDistributionTable.lsPrimaryKeys = new List<string>();
            lpForDistributionTable.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
            lpForDistributionTable.oDefaultFirstColumn = null;
            lpForDistributionTable.oDefaultLine = null;
            lpForDistributionTable.oDefaultColumn = null;
            lpForDistributionTable.oDefaultValue = null;

            lpForDistributionTable.bAllowAddColumns = true;
            lpForDistributionTable.tTypeNewColumns = typeof(String);
            lpForDistributionTable.bFlightCategoryTable = false;
            lpForDistributionTable.bShemaAirportTable = true;
            lpForDistributionTable.bAllocationTable = true;

            lpForDistributionTable.bBHS = false;
            lpForDistributionTable.bIndexedOnTerminal = false;

            lpForDistributionTable.vmDefaultVisualisationMode = new VisualisationMode(false, false, false, null, new int[] { 0 }, true, true,
                Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                VisualisationMode.EditModeEnum.None, new int[] { 0 }, null, null, null, null);

            cfcAllocation = new ConditionnalFormatCharacter(',', true);
            cfcAllocation.setCondition(1, Color.FromArgb(0, 255, 0));
            cfcAllocation.setCondition(2, Color.FromArgb(192, 255, 0));
            cfcAllocation.setCondition(3, Color.FromArgb(255, 255, 0));
            cfcAllocation.setCondition(4, Color.FromArgb(255, 128, 0));
            cfcAllocation.setCondition(5, Color.FromArgb(255, 0, 0));

            lpForDistributionTable.vmDefaultVisualisationMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfcAllocation };
            dslpTableParameters.Add(lpForDistributionTable.sName, lpForDistributionTable);
            #endregion


            #region ArrivalParking
            lpForDistributionTable = new LoadParameters();
            lpForDistributionTable.sName = GlobalNames.Alloc_BaggageClaimTableName;
            lpForDistributionTable.sFullName = "Baggage Claim (Allocation)";
            lpForDistributionTable.bSaved = false;
            lpForDistributionTable.bActive = true;lpForDistributionTable.bVisible = true;

            lpForDistributionTable.dstEntetes = new List<LoadParameters.Entete>();
            lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

            lpForDistributionTable.lsPrimaryKeys = new List<string>();
            lpForDistributionTable.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
            lpForDistributionTable.oDefaultFirstColumn = null;
            lpForDistributionTable.oDefaultLine = null;
            lpForDistributionTable.oDefaultColumn = null;
            lpForDistributionTable.oDefaultValue = null;

            lpForDistributionTable.bAllowAddColumns = true;
            lpForDistributionTable.tTypeNewColumns = typeof(String);
            lpForDistributionTable.bFlightCategoryTable = false;
            lpForDistributionTable.bShemaAirportTable = true;
            lpForDistributionTable.bAllocationTable = true;

            lpForDistributionTable.bBHS = false;
            lpForDistributionTable.bIndexedOnTerminal = false;

            lpForDistributionTable.vmDefaultVisualisationMode = new VisualisationMode(false, false, false, null, new int[] { 0 }, true, true,
                Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                VisualisationMode.EditModeEnum.None, new int[] { 0 }, null, null, null, null);

            cfcAllocation = new ConditionnalFormatCharacter(',', true);
            cfcAllocation.setCondition(1, Color.FromArgb(0, 255, 0));
            cfcAllocation.setCondition(2, Color.FromArgb(192, 255, 0));
            cfcAllocation.setCondition(3, Color.FromArgb(255, 255, 0));
            cfcAllocation.setCondition(4, Color.FromArgb(255, 128, 0));
            cfcAllocation.setCondition(5, Color.FromArgb(255, 0, 0));

            lpForDistributionTable.vmDefaultVisualisationMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfcAllocation };
            dslpTableParameters.Add(lpForDistributionTable.sName, lpForDistributionTable);
            #endregion

            #region MakeUp Airin
            lpForDistributionTable = new LoadParameters();
            lpForDistributionTable.sName = GlobalNames.Alloc_BaggageClaimTableName;
            lpForDistributionTable.sFullName = "Baggage Claim (Allocation)";
            lpForDistributionTable.bSaved = false;
            lpForDistributionTable.bActive = true;lpForDistributionTable.bVisible = true;

            lpForDistributionTable.dstEntetes = new List<LoadParameters.Entete>();
            lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

            lpForDistributionTable.lsPrimaryKeys = new List<string>();
            lpForDistributionTable.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
            lpForDistributionTable.oDefaultFirstColumn = null;
            lpForDistributionTable.oDefaultLine = null;
            lpForDistributionTable.oDefaultColumn = null;
            lpForDistributionTable.oDefaultValue = null;

            lpForDistributionTable.bAllowAddColumns = true;
            lpForDistributionTable.tTypeNewColumns = typeof(String);
            lpForDistributionTable.bFlightCategoryTable = false;
            lpForDistributionTable.bShemaAirportTable = true;
            lpForDistributionTable.bAllocationTable = true;

            lpForDistributionTable.bBHS = false;
            lpForDistributionTable.bIndexedOnTerminal = false;

            lpForDistributionTable.vmDefaultVisualisationMode = new VisualisationMode(false, false, false, null, new int[] { 0 }, true, true,
                Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                VisualisationMode.EditModeEnum.None, new int[] { 0 }, null, null, null, null);

            cfcAllocation = new ConditionnalFormatCharacter(',', true);
            cfcAllocation.setCondition(1, Color.FromArgb(0, 255, 0));
            cfcAllocation.setCondition(2, Color.FromArgb(192, 255, 0));
            cfcAllocation.setCondition(3, Color.FromArgb(255, 255, 0));
            cfcAllocation.setCondition(4, Color.FromArgb(255, 128, 0));
            cfcAllocation.setCondition(5, Color.FromArgb(255, 0, 0));

            lpForDistributionTable.vmDefaultVisualisationMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfcAllocation };
            dslpTableParameters.Add(lpForDistributionTable.sName, lpForDistributionTable);
            #endregion

            #region MakeUp Handlers
            lpForDistributionTable = new LoadParameters();
            lpForDistributionTable.sName = GlobalNames.Alloc_BaggageClaimTableName;
            lpForDistributionTable.sFullName = "Baggage Claim (Allocation)";
            lpForDistributionTable.bSaved = false;
            lpForDistributionTable.bActive = true;lpForDistributionTable.bVisible = true;

            lpForDistributionTable.dstEntetes = new List<LoadParameters.Entete>();
            lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

            lpForDistributionTable.lsPrimaryKeys = new List<string>();
            lpForDistributionTable.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
            lpForDistributionTable.oDefaultFirstColumn = null;
            lpForDistributionTable.oDefaultLine = null;
            lpForDistributionTable.oDefaultColumn = null;
            lpForDistributionTable.oDefaultValue = null;

            lpForDistributionTable.bAllowAddColumns = true;
            lpForDistributionTable.tTypeNewColumns = typeof(String);
            lpForDistributionTable.bFlightCategoryTable = false;
            lpForDistributionTable.bShemaAirportTable = true;
            lpForDistributionTable.bAllocationTable = true;

            lpForDistributionTable.bBHS = false;
            lpForDistributionTable.bIndexedOnTerminal = false;

            lpForDistributionTable.vmDefaultVisualisationMode = new VisualisationMode(false, false, false, null, new int[] { 0 }, true, true,
                Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                VisualisationMode.EditModeEnum.None, new int[] { 0 }, null, null, null, null);

            cfcAllocation = new ConditionnalFormatCharacter(',', true);
            cfcAllocation.setCondition(1, Color.FromArgb(0, 255, 0));
            cfcAllocation.setCondition(2, Color.FromArgb(192, 255, 0));
            cfcAllocation.setCondition(3, Color.FromArgb(255, 255, 0));
            cfcAllocation.setCondition(4, Color.FromArgb(255, 128, 0));
            cfcAllocation.setCondition(5, Color.FromArgb(255, 0, 0));

            lpForDistributionTable.vmDefaultVisualisationMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfcAllocation };
            dslpTableParameters.Add(lpForDistributionTable.sName, lpForDistributionTable);
            #endregion

            #region FPD AircraftMovements
            lpForDistributionTable = new LoadParameters();
            lpForDistributionTable.sName = GlobalNames.Alloc_BaggageClaimTableName;
            lpForDistributionTable.sFullName = "Baggage Claim (Allocation)";
            lpForDistributionTable.bSaved = false;
            lpForDistributionTable.bActive = true;lpForDistributionTable.bVisible = true;

            lpForDistributionTable.dstEntetes = new List<LoadParameters.Entete>();
            lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

            lpForDistributionTable.lsPrimaryKeys = new List<string>();
            lpForDistributionTable.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
            lpForDistributionTable.oDefaultFirstColumn = null;
            lpForDistributionTable.oDefaultLine = null;
            lpForDistributionTable.oDefaultColumn = null;
            lpForDistributionTable.oDefaultValue = null;

            lpForDistributionTable.bAllowAddColumns = true;
            lpForDistributionTable.tTypeNewColumns = typeof(String);
            lpForDistributionTable.bFlightCategoryTable = false;
            lpForDistributionTable.bShemaAirportTable = true;
            lpForDistributionTable.bAllocationTable = true;

            lpForDistributionTable.bBHS = false;
            lpForDistributionTable.bIndexedOnTerminal = false;

            lpForDistributionTable.vmDefaultVisualisationMode = new VisualisationMode(false, false, false, null, new int[] { 0 }, true, true,
                Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                VisualisationMode.EditModeEnum.None, new int[] { 0 }, null, null, null, null);

            cfcAllocation = new ConditionnalFormatCharacter(',', true);
            cfcAllocation.setCondition(1, Color.FromArgb(0, 255, 0));
            cfcAllocation.setCondition(2, Color.FromArgb(192, 255, 0));
            cfcAllocation.setCondition(3, Color.FromArgb(255, 255, 0));
            cfcAllocation.setCondition(4, Color.FromArgb(255, 128, 0));
            cfcAllocation.setCondition(5, Color.FromArgb(255, 0, 0));

            lpForDistributionTable.vmDefaultVisualisationMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfcAllocation };
            dslpTableParameters.Add(lpForDistributionTable.sName, lpForDistributionTable);
            #endregion

            #region FPA AircraftMovements
            lpForDistributionTable = new LoadParameters();
            lpForDistributionTable.sName = GlobalNames.Alloc_BaggageClaimTableName;
            lpForDistributionTable.sFullName = "Baggage Claim (Allocation)";
            lpForDistributionTable.bSaved = false;
            lpForDistributionTable.bActive = true;lpForDistributionTable.bVisible = true;

            lpForDistributionTable.dstEntetes = new List<LoadParameters.Entete>();
            lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

            lpForDistributionTable.lsPrimaryKeys = new List<string>();
            lpForDistributionTable.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
            lpForDistributionTable.oDefaultFirstColumn = null;
            lpForDistributionTable.oDefaultLine = null;
            lpForDistributionTable.oDefaultColumn = null;
            lpForDistributionTable.oDefaultValue = null;

            lpForDistributionTable.bAllowAddColumns = true;
            lpForDistributionTable.tTypeNewColumns = typeof(String);
            lpForDistributionTable.bFlightCategoryTable = false;
            lpForDistributionTable.bShemaAirportTable = true;
            lpForDistributionTable.bAllocationTable = true;

            lpForDistributionTable.bBHS = false;
            lpForDistributionTable.bIndexedOnTerminal = false;

            lpForDistributionTable.vmDefaultVisualisationMode = new VisualisationMode(false, false, false, null, new int[] { 0 }, true, true,
                Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                VisualisationMode.EditModeEnum.None, new int[] { 0 }, null, null, null, null);

            cfcAllocation = new ConditionnalFormatCharacter(',', true);
            cfcAllocation.setCondition(1, Color.FromArgb(0, 255, 0));
            cfcAllocation.setCondition(2, Color.FromArgb(192, 255, 0));
            cfcAllocation.setCondition(3, Color.FromArgb(255, 255, 0));
            cfcAllocation.setCondition(4, Color.FromArgb(255, 128, 0));
            cfcAllocation.setCondition(5, Color.FromArgb(255, 0, 0));

            lpForDistributionTable.vmDefaultVisualisationMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfcAllocation };
            dslpTableParameters.Add(lpForDistributionTable.sName, lpForDistributionTable);
            #endregion*/
            #endregion

            #region Pour ajouter un nouvel élément
            //Le commentaire suivant contient une région permettant de définir une nouvelle table et de l'ajouter au dossier actif.
            /* #region
            lpForDistributionTable = new LoadParameters();
            lpForDistributionTable.sName = "";
            lpForDistributionTable.sFullName = "";
            lpForDistributionTable.bSaved = true;
            lpForDistributionTable.bActive = true;lpForDistributionTable.bVisible = true;
            
            lpForDistributionTable.dstEntetes = new List<LoadParameters.Entete>();
            lpForDistributionTable.dstEntetes.Add( new LoadParameters.Entete("", typeof()));
            
            lpForDistributionTable.lsPrimaryKeys = new List<string>();
            lpForDistributionTable.lsPrimaryKeys .Add("");
            lpForDistributionTable.oDefaultFirstColumn = null;
            lpForDistributionTable.oDefaultLine = null;
            lpForDistributionTable.oDefaultColumn = null;
            lpForDistributionTable.oDefaultValue = null;
            
            lpForDistributionTable.bAllowAddColumns = false;
            lpForDistributionTable.tTypeNewColumns = null;
            lpForDistributionTable.bFlightCategoryTable = false;
            lpForDistributionTable.bShemaAirportTable = false;
            lpForDistributionTable.bAllocationTable = false;

            lpForDistributionTable.bBHS = false;
            lpForDistributionTable.bIndexedOnTerminal = false;

            lpForDistributionTable.vmDefaultVisualisationMode = null;

            dslpTableParameters.Add(lpForDistributionTable.sName, lpForDistributionTable);
            aitTmp.AddChild(lpForDistributionTable);
            #endregion*/
            #endregion
        }
        //<< Task #7405 - new Desk and extra information for Pax
        public static List<String> createUserAttributesTablesFromUserAttributesColumnNames(DataTable userAttributes)
        {
            List<String> tablesToBeDeleted = new List<String>();
            int indexColumnColumnName = userAttributes.Columns.IndexOf(GlobalNames.sUserAttributes_ColumnName);
            ArboInputTable passengerDirectory = aitInputData.GetChildNamed("Passenger");
            if (passengerDirectory == null) // PAX key flag disabled
            {
                return tablesToBeDeleted;
            }
            ArboInputTable userAttributesTablesDirectory = passengerDirectory.GetChildNamed(GlobalNames.sUserAttributes_DirectoryName);
            Dictionary<string, ArboInputTable> distributionTablesDictionary = userAttributesTablesDirectory.getChildrenDictionary();
            if (userAttributes.Rows.Count > 0)
            {
                if (distributionTablesDictionary == null)
                {
                    foreach (DataRow dr in userAttributes.Rows)
                    {
                        String distributionTableName = dr[indexColumnColumnName].ToString();

                        // >> Task #10764 Pax2Sim - new User attributes for Groups
                        if (GlobalNames.PAX_GROUP_USER_ATTRIBUTES_LIST.Contains(distributionTableName))
                        {
                            //we don't create user attribute table for the Pax_x_Group attributes
                            //as these are only used for the Planning and PaxPlan tables and receive values programmatically
                            continue;
                        }
                        // << Task #10764 Pax2Sim - new User attributes for Groups

                        //check if the table exists in the directory and if not call a function to add it
                        if (userAttributesTablesDirectory.GetChildNamed(distributionTableName) == null)
                        {
                            addDistributionTableToUserAttributesTableByName(distributionTableName, userAttributesTablesDirectory);
                        }
                    }
                }
                else
                {
                    List<String> realDistributionTableNames = new List<string>();
                    foreach (DataRow dr in userAttributes.Rows)
                    {
                        String distributionTableName = dr[indexColumnColumnName].ToString();

                        // >> Task #10764 Pax2Sim - new User attributes for Groups
                        if (GlobalNames.PAX_GROUP_USER_ATTRIBUTES_LIST.Contains(distributionTableName))
                        {
                            //we don't create user attribute table for the Pax_x_Group attributes
                            //as these are only used for the Planning and PaxPlan tables and receive values programmatically
                            continue;
                        }
                        // << Task #10764 Pax2Sim - new User attributes for Groups

                        if (!distributionTablesDictionary.ContainsKey(distributionTableName))
                            addDistributionTableToUserAttributesTableByName(distributionTableName, userAttributesTablesDirectory);
                        realDistributionTableNames.Add(distributionTableName);
                    }
                    foreach (KeyValuePair<string, ArboInputTable> pair in distributionTablesDictionary)
                    {
                        if (!realDistributionTableNames.Contains(pair.Key))
                        {
                            //delete the table pair.value
                            userAttributesTablesDirectory.RemoveChild(pair.Key);
                            tablesToBeDeleted.Add(pair.Key);
                        }
                    }
                    foreach (var name in tablesToBeDeleted)
                    {
                        userAttributesTablesDirectory.RemoveChild(name);
                        if (dsetpExceptionParameters != null && dsetpExceptionParameters.ContainsKey(name))
                            dsetpExceptionParameters.Remove(name);
                        if (dslpTableParameters != null && dslpTableParameters.ContainsKey(name))
                            dslpTableParameters.Remove(name);
                    }
                }
            }
            else
            {
                if (distributionTablesDictionary != null)
                {
                    List<String> realDistributionTableNames = new List<string>();
                    foreach (DataRow dr in userAttributes.Rows)
                    {
                        String distributionTableName = dr[indexColumnColumnName].ToString();

                        // >> Task #10764 Pax2Sim - new User attributes for Groups
                        if (GlobalNames.PAX_GROUP_USER_ATTRIBUTES_LIST.Contains(distributionTableName))
                        {
                            //we don't create user attribute table for the Pax_x_Group attributes
                            //as these are only used for the Planning and PaxPlan tables and receive values programmatically
                            continue;
                        }
                        // << Task #10764 Pax2Sim - new User attributes for Groups

                        if (!distributionTablesDictionary.ContainsKey(distributionTableName))
                            addDistributionTableToUserAttributesTableByName(distributionTableName, userAttributesTablesDirectory);
                        realDistributionTableNames.Add(distributionTableName);
                    }
                    foreach (KeyValuePair<string, ArboInputTable> pair in distributionTablesDictionary)
                    {
                        if (!realDistributionTableNames.Contains(pair.Key))
                        {
                            //delete the table pair.value
                            userAttributesTablesDirectory.RemoveChild(pair.Key);
                            tablesToBeDeleted.Add(pair.Key);
                        }
                    }
                    foreach (var name in tablesToBeDeleted)
                    {
                        userAttributesTablesDirectory.RemoveChild(name);
                        if (dsetpExceptionParameters != null && dsetpExceptionParameters.ContainsKey(name))
                            dsetpExceptionParameters.Remove(name);
                        if (dslpTableParameters != null && dslpTableParameters.ContainsKey(name))
                            dslpTableParameters.Remove(name);
                    }
                }
            }
            return tablesToBeDeleted;
        }

        private static void addDistributionTableToUserAttributesTableByName(String distributionTableName, ArboInputTable userAttributesTablesDirectory)
        {
            LoadParameters lpForDistributionTable = new LoadParameters();
            lpForDistributionTable.sName = distributionTableName;
            lpForDistributionTable.sFullName = distributionTableName;
            lpForDistributionTable.bSaved = true;
            lpForDistributionTable.bActive = true;
            lpForDistributionTable.bVisible = true;

            // >> Task #10069 Pax2Sim - no BNP development            
            if (!PAX2SIM.usaMode)
            {
                if (distributionTableName.Equals(GlobalNames.USA_STANDARD_PARAMETERS_TABLE_NAME))
                    lpForDistributionTable.bVisible = false;
            }
            // << Task #10069 Pax2Sim - no BNP development


            lpForDistributionTable.dstEntetes = new List<LoadParameters.Entete>();

            // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
            if (distributionTableName.Equals(GlobalNames.sUserAttributesBaggLoadingDelayTableName))
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sUserAttributesBaggLoadingDelayValueColumnName, typeof(String)));
            else if (distributionTableName.Equals(GlobalNames.sUserAttributesBaggLoadingRateTableName))
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sUserAttributesBaggLoadingRateValueColumnName, typeof(String)));
            else if (distributionTableName.Equals(GlobalNames.userAttributesReclaimLogTableName))
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.userAttributesReclaimLogValueColumnName, typeof(String)));
            else if (distributionTableName.Equals(GlobalNames.userAttributesEBSInputRateTableName))     // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.ebsRatesTableTerminalUnknownColumnName, typeof(String)));
            else if (distributionTableName.Equals(GlobalNames.userAttributesEBSOutputRateTableName))    // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.ebsRatesTableTerminalUnknownColumnName, typeof(String)));
            else if (distributionTableName.Equals(GlobalNames.flightSubcategoriesTableName))    // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.flightSubcategoryColumnName, typeof(String)));
            else if (distributionTableName.Equals(GlobalNames.numberOfPassengersTableName))
            {
                // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.originatingPaxDepartureColumnName, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.transferringPaxDepartureColumnName, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.terminatingPaxArrivalColumnName, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.transferringPaxArrivalColumnName, typeof(Double)));
                // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            }
            else if (distributionTableName.Equals(GlobalNames.numberOfBaggagesTableName))
            {
                // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.originatingBagDepartureColumnName, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.transferringBagDepartureColumnName, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.terminatingBagArrivalColumnName, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.transferringBagArrivalColumnName, typeof(Double)));
                // >> Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            }
            else if (distributionTableName.Equals(GlobalNames.USA_STANDARD_PARAMETERS_TABLE_NAME))
            {
                // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_OOG_PERCENT_PARAM_NAME, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_OS_PERCENT_PARAM_NAME, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_EDS_SCREENING_RATE_PER_MINUTE_PARAM_NAME, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_EDS_SCREENING_RATE_PER_HOUR_PARAM_NAME, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_FALSE_ALARM_EDS_DOM_PARAM_NAME, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_FALSE_ALARM_EDS_INT_PARAM_NAME, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_OSR_PROCESSING_RATE_BPH_PARAM_NAME, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_CLEAR_RATE_PARAM_NAME, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_ETD_SCREENING_RATE_DOM_WITH_IMAGE_PARAM_NAME, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_ETD_SCREENING_RATE_DOM_NO_IMAGE_PARAM_NAME, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_ETD_SCREENING_RATE_INT_WITH_IMAGE_PARAM_NAME, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_ETD_SCREENING_RATE_INT_NO_IMAGE_PARAM_NAME, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_RATE_LOST_TRACK_OS_PARAM_NAME, typeof(Double)));
                //lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_RATE_LOST_TRACK_OOG_PARAM_NAME, typeof(Double)));
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.USA_STANDARD_ETD_SCREENING_RATE_OS_BAGS_BPH_PARAM_NAME, typeof(Double)));
                // << Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
            }
            else
                lpForDistributionTable.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sUserAttributes_DistributionTableColumnName_Value, typeof(String)));
            // >> Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant

            lpForDistributionTable.lsPrimaryKeys = new List<string>();
            // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
            if (distributionTableName.Equals(GlobalNames.sUserAttributesBaggLoadingDelayTableName))
                lpForDistributionTable.lsPrimaryKeys.Add(GlobalNames.sUserAttributesBaggLoadingDelayValueColumnName);
            else if (distributionTableName.Equals(GlobalNames.sUserAttributesBaggLoadingRateTableName))
                lpForDistributionTable.lsPrimaryKeys.Add(GlobalNames.sUserAttributesBaggLoadingRateValueColumnName);
            else if (distributionTableName.Equals(GlobalNames.userAttributesReclaimLogTableName))
            { }
            else if (distributionTableName.Equals(GlobalNames.userAttributesEBSInputRateTableName))     // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
            { }//lpForDistributionTable.lsPrimaryKeys.Add(GlobalNames.userAttributesEBSInputRateValueColumnName);
            else if (distributionTableName.Equals(GlobalNames.userAttributesEBSOutputRateTableName))    // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rates
            { }//lpForDistributionTable.lsPrimaryKeys.Add(GlobalNames.userAttributesEBSOutputRateValueColumnName);
            else if (distributionTableName.Equals(GlobalNames.flightSubcategoriesTableName))    // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
            { }
            else if (distributionTableName.Equals(GlobalNames.numberOfPassengersTableName))    // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            { }
            else if (distributionTableName.Equals(GlobalNames.numberOfBaggagesTableName))    // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            { }
            else if (distributionTableName.Equals(GlobalNames.USA_STANDARD_PARAMETERS_TABLE_NAME))  // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
            { }
            else
                lpForDistributionTable.lsPrimaryKeys.Add(GlobalNames.sUserAttributes_DistributionTableColumnName_Value);
            // >> Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant 


            lpForDistributionTable.oDefaultFirstColumn = null;

            lpForDistributionTable.oDefaultLine = null;

            // << Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant
            if (distributionTableName.Equals(GlobalNames.sUserAttributesBaggLoadingDelayTableName))
                lpForDistributionTable.oDefaultColumn = new Double[] { 20, 100 };
            else if (distributionTableName.Equals(GlobalNames.sUserAttributesBaggLoadingRateTableName))
                lpForDistributionTable.oDefaultColumn = new Double[] { 12, 100 };
            else if (distributionTableName.Equals(GlobalNames.userAttributesReclaimLogTableName))
                lpForDistributionTable.oDefaultColumn = new Double[] { -15, 100 };
            else if (distributionTableName.Equals(GlobalNames.userAttributesEBSInputRateTableName))     // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rate
            { }
            else if (distributionTableName.Equals(GlobalNames.userAttributesEBSOutputRateTableName))    // << Task #9172 Pax2Sim - Static Analysis - EBS algorithm - Input/Output Rate
            { }
            else if (distributionTableName.Equals(GlobalNames.numberOfPassengersTableName))    // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            { }
            else if (distributionTableName.Equals(GlobalNames.numberOfBaggagesTableName))    // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
            { }
            else if (distributionTableName.Equals(GlobalNames.USA_STANDARD_PARAMETERS_TABLE_NAME))    // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
            { }
            else
                lpForDistributionTable.oDefaultColumn = new Double[] { 0, 100 };
            // >> Task #9066 Pax2Sim - Peak FLows - BHS Inbounds Smooth/Instant

            lpForDistributionTable.oDefaultValue = null;

            lpForDistributionTable.bAllowAddColumns = true;
            lpForDistributionTable.tTypeNewColumns = typeof(Double);

            if (GlobalNames.ebsRateTableNamesList.Contains(distributionTableName)
                || distributionTableName.Equals(GlobalNames.numberOfPassengersTableName)    // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
                || distributionTableName.Equals(GlobalNames.numberOfBaggagesTableName)
                || distributionTableName.Equals(GlobalNames.USA_STANDARD_PARAMETERS_TABLE_NAME))    // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
            {
                lpForDistributionTable.bFlightCategoryTable = false;
            }
            else
                lpForDistributionTable.bFlightCategoryTable = true;

            lpForDistributionTable.bShemaAirportTable = false;
            lpForDistributionTable.bAllocationTable = false;

            lpForDistributionTable.bBHS = false;
            lpForDistributionTable.bIndexedOnTerminal = false;

            // << Task #9260 Pax2Sim - Static Analysis - EBS algorithm - EBS per Terminal
            if (GlobalNames.ebsRateTableNamesList.Contains(distributionTableName)
                || distributionTableName.Equals(GlobalNames.numberOfPassengersTableName)    // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
                || distributionTableName.Equals(GlobalNames.numberOfBaggagesTableName)
                || distributionTableName.Equals(GlobalNames.USA_STANDARD_PARAMETERS_TABLE_NAME))    // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
            {
                lpForDistributionTable.vmDefaultVisualisationMode = new VisualisationMode(true, true, false, null, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row,
                    VisualisationMode.EditModeEnum.Row, new int[] { 0 }, null, null, null, null);
                lpForDistributionTable.vmDefaultVisualisationMode.AllowEditRow = true;
            }
            else
            {
                lpForDistributionTable.vmDefaultVisualisationMode = new VisualisationMode(true, true, false, null, new int[] { 0 }, false, false,
                    Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column,
                    VisualisationMode.EditModeEnum.Column, new int[] { 0 }, null, null, null, null);
                lpForDistributionTable.vmDefaultVisualisationMode.AllowEditColumn = true;
            }
            if (!GlobalNames.ebsRateTableNamesList.Contains(distributionTableName) // << Task #9260 Pax2Sim - Static Analysis - EBS algorithm - EBS per Terminal
                && !distributionTableName.Equals(GlobalNames.numberOfPassengersTableName)   // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
                && !distributionTableName.Equals(GlobalNames.numberOfBaggagesTableName)
                && !distributionTableName.Equals(GlobalNames.USA_STANDARD_PARAMETERS_TABLE_NAME))   // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
            {
                lpForDistributionTable.tcChecks = new OverallTools.TableCheck(lpForDistributionTable.sName, null, null, null, null, null, null, 1, null, OverallTools.TableCheck.eTypeAnalyze.None);
            }
            dslpTableParameters.Add(lpForDistributionTable.sName, lpForDistributionTable);
            userAttributesTablesDirectory.AddChild(lpForDistributionTable);
            if (!GlobalNames.ebsRateTableNamesList.Contains(distributionTableName)
                && !distributionTableName.Equals(GlobalNames.flightSubcategoriesTableName) // << Task #9504 Pax2Sim - flight subCategories - peak flow segregation
                && !distributionTableName.Equals(GlobalNames.numberOfPassengersTableName)   // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
                && !distributionTableName.Equals(GlobalNames.numberOfBaggagesTableName)    // << Task #9536 Pax2Sim - table to specify direct the nb of different types of pax(orig, transf...)
                && !distributionTableName.Equals(GlobalNames.USA_STANDARD_PARAMETERS_TABLE_NAME))   // >> Task #9967 Pax2Sim - BNP development - Peak Flows - USA Standard parameters table
            {
                dsetpExceptionParameters.Add(distributionTableName, ExceptionTable.ExceptionTableParameters.Flight |
                    ExceptionTable.ExceptionTableParameters.FirstAndBusiness |
                    ExceptionTable.ExceptionTableParameters.Airline);
            }
        }



        //>> Task #7405 - new Desk and extra information for Pax

        /// <summary>
        /// Fonction qui renvoie le \ref LoadParameters pour la table passée en parametre. Si la table n'a pas de \ref LoadParameters, alors la fonction renvoie NULL.
        /// </summary>
        /// <param name="sTableName">Nom de la table recherchée.</param>
        /// <returns>Paramétrage ou alors NULL.</returns>
        internal static LoadParameters getParameters(String sTableName)
        {
            ///Si \ref dslpTableParameters  est null, appel de la fonction \ref InitializeTableParameters.
            if (dslpTableParameters == null)
                InitializeTableParameters();
            ///Si \ref dslpTableParameters  est null,Renvoie de NULL.
            if (dslpTableParameters == null)
                return null;
            ///Si \ref dslpTableParameters ne contient pas de définition pour la table, alors la fonction renvoier NULL.
            if (!dslpTableParameters.ContainsKey(sTableName))
                return null;
            ///Renvoie des paramétrage pour la table demandée.
            return dslpTableParameters[sTableName];
        }

        /// <summary>
        /// Fonction qui permet de mettre à jour l'arborescence passée en parametre avec les informations sur les tables pour PAX2SIM.
        /// </summary>
        /// <param name="tnRoot">Noeud ou devront être ajoutée les tables et dossier pour PAX2SIM.</param>
        /// <param name="cmsTableContextMenu">Menu contextuel à ajouter aux tables.</param>
        internal static void UpdateTreeNode(TreeNode tnRoot, ContextMenuStrip cmsTableContextMenu)
        {
            ///Si aitInputData est null, appel de \ref InitializeTableParameters()
            if (aitInputData == null)
                InitializeTableParameters();
            ///Si aitInputData est null, la fonction s'arrete
            if (aitInputData == null)
                return;
            ///Si \ref tnRoot est null, la fonction s'arrete
            if (tnRoot == null)
                return;
            ///Appel de la fonction \ref ArboInputTable.UpdateTreeNodeWithChilds()
            aitInputData.UpdateTreeNodeWithChilds(tnRoot, cmsTableContextMenu);
        }

        /// <summary>
        /// Délégué qui permet de définir la fonction qui se charge de vérifier que la table est valide pour la simulation.
        /// </summary>
        /// <param name="tcParams">Paramétrage de vérification</param>
        /// <param name="sCurrentAnalysedTable">Table à analyser.</param>
        /// <param name="sortedColumn">Nom de la colonne sur laquelle effectuer le tri.</param>
        /// <param name="Order">Ordre de tri.</param>
        /// <returns>Liste des erreurs trouvées dans la table. Ou NULL si aucune erreur présente.</returns>
        internal delegate ArrayList CheckTableDelegate(OverallTools.TableCheck tcParams, NormalTable sCurrentAnalysedTable, String sortedColumn, SortOrder Order);

        /// <summary>
        /// Fonction qui converti le nom complet d'une table en son nom compressé.
        /// </summary>
        /// <param name="sFullName">Nom complet de la table.</param>
        /// <returns>Nom compressé de la table, ou alors le même nom que celui passé en paramètre, si aucune transformation n'a été trouvée.</returns>
        internal static String ConvertToShortName(String sFullName)
        {
            ///Si \ref dslpTableParameters est null, appel de la fonction \ref InitializeTableParameters().
            if (dslpTableParameters == null)
                InitializeTableParameters();

            ///Pour chaque table présente dans \ref dslpTableParameters
            foreach (String sName in dslpTableParameters.Keys)
            {
                ///__ Si \ref LoadParameters.sFullName de la table courante est égal à \ref sFullName, Renvoyer \ref LoadParameters.sName
                if (dslpTableParameters[sName].sFullName == sFullName)
                    return sName;
            }
            ///Renvoyer \ref sFullName
            return sFullName;
        }

        /// <summary>
        /// Fonction qui converti le nom compressé d'une table en son nom complet.
        /// </summary>
        /// <param name="sShortName">Nom compressé de la table.</param>
        /// <returns>Nom complet de la table, ou alors le même nom que celui passé en paramètre, si aucune transformation n'a été trouvée.</returns>
        internal static String ConvertToFullName(String sShortName)
        {
            ///Si \ref sShortName est null, alors la fonction renvoie null.
            if (sShortName == null)
                return null;
            ///Si \ref dslpTableParameters est null, appel de la fonction \ref InitializeTableParameters().
            if (dslpTableParameters == null)
                InitializeTableParameters();
            ///Si \ref dslpTableParameters ne contient pas comme clef \ref sShortName, alors Renvoyer \ref sShortName
            if (!dslpTableParameters.ContainsKey(sShortName))
                return sShortName;
            ///Sinon Renvoyer dslpTableParameters[sShortName].sFullName;
            return dslpTableParameters[sShortName].sFullName;
        }

        /// <summary>
        /// Fonction qui renvoie la liste des noms des tables qui ont des paramétrages.
        /// </summary>
        /// <returns>Liste des noms des tables qui ont des paramétrages.</returns>
        internal static List<String> getNames()
        {
            ///Si \ref dslpTableParameters est null, appel de la fonction \ref InitializeTableParameters().
            if (dslpTableParameters == null)
                InitializeTableParameters();
            List<String> lsNames = new List<string>();
            ///Pour chaque table présente dans \ref dslpTableParameters
            foreach (String sName in dslpTableParameters.Keys)
            {
                ///__ Si la table n'est pas visible, passer à la table suivante.
                LoadParameters lp = dslpTableParameters[sName];
                if (!lp.bVisible)
                    continue;
                ///__ Ajouter la table à la liste qui sera renvoyé 
                lsNames.Add(sName);
            }
            ///Renvoyer la liste des tables trouvées.
            return lsNames;
        }

        /// <summary>
        /// Fonction qui permet de changer de périmetre pour l'application \ref PAX2SIM.EnumPerimetre
        /// </summary>
        /// <param name="epPerimeter">Perimetre vers lequel l'application doit changer.</param>
        internal static void SwitchTo(PAX2SIM.EnumPerimetre epPerimeter)
        {
            ///Si \ref dslpTableParameters est null, appel de la fonction \ref InitializeTableParameters().
            if (dslpTableParameters == null)
                InitializeTableParameters();
            ///On essaie de récupérer le noeud représentant le noeud BHS (visible et accessible uniquement en mode \ref PAX2SIM.EnumPerimetre.BHS
            ///ou \ref PAX2SIM.EnumPerimetre.TMS.
            ArboInputTable aitBHS = aitInputData.GetChildNamed(GestionDonneesHUB2SIM.sBHSAnalysis);

            ArboInputTable aitTmp = aitInputData.GetChildNamed("Allocation & Planning");
            ///We set back to visible the 3 directories that are invisible in case of Parking mode in PAX2SIM.
            if (aitTmp != null)
                aitTmp.Visible = true;

            aitTmp = aitInputData.GetChildNamed("Airport Process");
            if (aitTmp != null)
                aitTmp.Visible = true;

            aitTmp = aitInputData.GetChildNamed("Airport Area Capacity");
            if (aitTmp != null)
                aitTmp.Visible = true;

            aitTmp = aitInputData.GetChildNamed("Parking");
            if (aitTmp != null)
                aitTmp.Visible = false;

            ///Si le périmètre est désormais \ref PAX2SIM.EnumPerimetre.PAX, changer les fonctions de vérifications vers les valeurs pour la simulation PAX.
            ///Sinon si le périmètre est désormais \ref PAX2SIM.EnumPerimetre.BHS, changer les fonctions de vérifications vers les valeurs pour la simulation Bagage.
            switch (epPerimeter)
            {
                case PAX2SIM.EnumPerimetre.PAX:
                    if (dslpTableParameters.ContainsKey(GlobalNames.FPDTableName))
                    {
                        LoadParameters lp = dslpTableParameters[GlobalNames.FPDTableName];
                        if (lp.tcChecks != null)
                        {
                            // << Task #9238 Pax2Sim - Validations - Validate flight plan Parking
                            lp.tcChecks.ObjectType = new String[][] { new String[]{"Check In"},
                                                    new String[]{"Check In"},
                                                    new String[]{"Boarding Gate"},
                                                    new String[]{GlobalNames.AIRCRAFT_PARKING_STAND_OBJECT_NAME}
                                                  };

                            lp.tcChecks.CanBeNull = new Boolean[] { false, false, false, true };
                            lp.tcChecks.ObjectColumns = new String[][] {
                                                    new String[]{GlobalNames.sFPD_Column_TerminalCI,"", GlobalNames.sFPD_Column_Eco_CI_Start, GlobalNames.sFPD_Column_Eco_CI_End},
                                                    new String[]{GlobalNames.sFPD_Column_TerminalCI,"", GlobalNames.sFPD_Column_FB_CI_Start, GlobalNames.sFPD_Column_FB_CI_End},
                                                    new String[]{GlobalNames.sFPD_A_Column_TerminalGate, "", GlobalNames.sFPD_Column_BoardingGate},
                                                    new String[]{GlobalNames.sFPD_A_Column_TerminalParking, "", GlobalNames.sFPD_A_Column_Parking}
                                                  };
                            /*                            
                                                        lp.tcChecks.ObjectType = new String[][] { new String[]{"Check In"},
                                                                                new String[]{"Check In"},
                                                                                new String[]{"Boarding Gate"}
                                                                              };

                                                        lp.tcChecks.CanBeNull = new Boolean[] { false, false, false };
                                                        lp.tcChecks.ObjectColumns = new String[][] { 
                                                                                new String[]{GlobalNames.sFPD_Column_TerminalCI,"", GlobalNames.sFPD_Column_Eco_CI_Start, GlobalNames.sFPD_Column_Eco_CI_End},
                                                                                new String[]{GlobalNames.sFPD_Column_TerminalCI,"", GlobalNames.sFPD_Column_FB_CI_Start, GlobalNames.sFPD_Column_FB_CI_End},
                                                                                new String[]{GlobalNames.sFPD_A_Column_TerminalGate, "", GlobalNames.sFPD_Column_BoardingGate}
                                                                              };
                            */
                            // >> Task #9238 Pax2Sim - Validations - Validate flight plan Parking
                        }
                    }
                    if (dslpTableParameters.ContainsKey(GlobalNames.FPATableName))
                    {
                        LoadParameters lp = dslpTableParameters[GlobalNames.FPATableName];
                        if (lp.tcChecks != null)
                        {
                            // << Task #9238 Pax2Sim - Validations - Validate flight plan Parking
                            lp.tcChecks.ObjectType = new String[][] { new String[]{"Arrival Gate"},
                                                                new String[]{"Baggage Claim"},
                                                                new String[]{GlobalNames.AIRCRAFT_PARKING_STAND_OBJECT_NAME}
                                                    };

                            lp.tcChecks.CanBeNull = new Boolean[] { false, false, true };
                            lp.tcChecks.ObjectColumns = new String[][] { new String[]{GlobalNames.sFPD_A_Column_TerminalGate, "", GlobalNames.sFPA_Column_ArrivalGate},
                                                new String[]{GlobalNames.sFPA_Column_TerminalReclaim, "", GlobalNames.sFPA_Column_ReclaimObject},
                                                new String[]{GlobalNames.sFPD_A_Column_TerminalParking, "", GlobalNames.sFPD_A_Column_Parking}
                                              };
                            /*                            
                                                        lp.tcChecks.ObjectType = new String[][] { new String[]{"Arrival Gate"},
                                                                                            new String[]{"Baggage Claim"}
                                                                                };

                                                        lp.tcChecks.CanBeNull = new Boolean[] { false, false };
                                                        lp.tcChecks.ObjectColumns = new String[][] { new String[]{GlobalNames.sFPD_A_Column_TerminalGate, "", GlobalNames.sFPA_Column_ArrivalGate},
                                                                            new String[]{GlobalNames.sFPA_Column_TerminalReclaim, "", GlobalNames.sFPA_Column_ReclaimObject}
                                                                          };
                            */
                            // >> Task #9238 Pax2Sim - Validations - Validate flight plan Parking
                        }
                    }

                    ///__ Si le noeud pour les paramétrages bagage est disponible, alors on le rend invisible.
                    if (aitBHS != null)
                        aitBHS.Visible = false;
                    break;
                case PAX2SIM.EnumPerimetre.BHS:
                case PAX2SIM.EnumPerimetre.TMS:
                    ///Cas où le périmetre de l'application est \ref PAX2SIM.EnumPerimetre.BHS ou \ref PAX2SIM.EnumPerimetre.TMS.
                    if (dslpTableParameters.ContainsKey(GlobalNames.FPDTableName))
                    {
                        LoadParameters lp = dslpTableParameters[GlobalNames.FPDTableName];
                        if (lp.tcChecks != null)
                        {
                            // << Task #9238 Pax2Sim - Validations - Validate flight plan Parking
                            lp.tcChecks.ObjectType = new String[][] { new String[]{"Check In"},
                                                    new String[]{"Check In"},
                                                    new String[]{GlobalNames.sBHS_MakeUpObject},
                                                    new String[]{GlobalNames.sBHS_MakeUpObject},
                                                    new String[]{GlobalNames.AIRCRAFT_PARKING_STAND_OBJECT_NAME}
                                                  };

                            lp.tcChecks.CanBeNull = new Boolean[] { false, false, false, true, true };
                            lp.tcChecks.ObjectColumns = new String[][] { new String[]{GlobalNames.sFPD_Column_TerminalCI,"", GlobalNames.sFPD_Column_Eco_CI_Start, GlobalNames.sFPD_Column_Eco_CI_End},
                                                    new String[]{GlobalNames.sFPD_Column_TerminalCI,"", GlobalNames.sFPD_Column_FB_CI_Start, GlobalNames.sFPD_Column_FB_CI_End},
                                                    new String[]{GlobalNames.sFPD_Column_TerminalMup,"",GlobalNames.sFPD_Column_Eco_Mup_Start,GlobalNames.sFPD_Column_Eco_Mup_End},
                                                    new String[]{GlobalNames.sFPD_Column_TerminalMup,"",GlobalNames.sFPD_Column_First_Mup_Start,GlobalNames.sFPD_Column_First_Mup_End},
                                                    new String[]{GlobalNames.sFPD_A_Column_TerminalParking, "", GlobalNames.sFPD_A_Column_Parking}
                                                  };
                            /*                            
                                                        lp.tcChecks.ObjectType = new String[][] { new String[]{"Check In"},
                                                                                new String[]{"Check In"},
                                                                                new String[]{GlobalNames.sBHS_MakeUpObject},
                                                                                new String[]{GlobalNames.sBHS_MakeUpObject}
                                                                              };

                                                        lp.tcChecks.CanBeNull = new Boolean[] { false, false, false, true };
                                                        lp.tcChecks.ObjectColumns = new String[][] { new String[]{GlobalNames.sFPD_Column_TerminalCI,"", GlobalNames.sFPD_Column_Eco_CI_Start, GlobalNames.sFPD_Column_Eco_CI_End},
                                                                                new String[]{GlobalNames.sFPD_Column_TerminalCI,"", GlobalNames.sFPD_Column_FB_CI_Start, GlobalNames.sFPD_Column_FB_CI_End},
                                                                                new String[]{GlobalNames.sFPD_Column_TerminalMup,"",GlobalNames.sFPD_Column_Eco_Mup_Start,GlobalNames.sFPD_Column_Eco_Mup_End},
                                                                                new String[]{GlobalNames.sFPD_Column_TerminalMup,"",GlobalNames.sFPD_Column_First_Mup_Start,GlobalNames.sFPD_Column_First_Mup_End},
                                                                              };
                            */
                            // >> Task #9238 Pax2Sim - Validations - Validate flight plan Parking
                        }
                    }
                    if (dslpTableParameters.ContainsKey(GlobalNames.FPATableName))
                    {
                        LoadParameters lp = dslpTableParameters[GlobalNames.FPATableName];
                        if (lp.tcChecks != null)
                        {
                            // << Task #9238 Pax2Sim - Validations - Validate flight plan Parking
                            lp.tcChecks.ObjectType = new String[][] {new String[]{"Baggage Claim"},
                                                                     new String[]{GlobalNames.AIRCRAFT_PARKING_STAND_OBJECT_NAME}
                                                                    };

                            lp.tcChecks.CanBeNull = new Boolean[] { false, true };
                            lp.tcChecks.ObjectColumns = new String[][] {
                                                new String[]{GlobalNames.sFPA_Column_TerminalReclaim, "", GlobalNames.sFPA_Column_ReclaimObject},
                                                new String[]{GlobalNames.sFPD_A_Column_TerminalParking, "", GlobalNames.sFPD_A_Column_Parking}
                                              };
                            /*                            
                                                        lp.tcChecks.ObjectType = new String[][] {new String[]{"Baggage Claim"}
                                                                                };

                                                        lp.tcChecks.CanBeNull = new Boolean[] { false };
                                                        lp.tcChecks.ObjectColumns = new String[][] { new String[]{GlobalNames.sFPA_Column_TerminalReclaim, "", GlobalNames.sFPA_Column_ReclaimObject}
                                                                          };
                            */
                            // >> Task #9238 Pax2Sim - Validations - Validate flight plan Parking
                        }
                    }
                    ///__ Si le noeud pour les paramétrages bagage est disponible, alors on le rend visible.
                    if (aitBHS != null)
                        aitBHS.Visible = true;
                    break;
                case PAX2SIM.EnumPerimetre.PKG:
                    ///We hide the 3 directories that are invisible in case of Parking mode in PAX2SIM.
                    aitTmp = aitInputData.GetChildNamed("Allocation & Planning");
                    if (aitTmp != null)
                        aitTmp.Visible = false;

                    aitTmp = aitInputData.GetChildNamed("Airport Process");
                    if (aitTmp != null)
                        aitTmp.Visible = false;

                    aitTmp = aitInputData.GetChildNamed("Airport Area Capacity");
                    if (aitTmp != null)
                        aitTmp.Visible = false;

                    aitTmp = aitInputData.GetChildNamed("Parking");
                    if (aitTmp != null)
                        aitTmp.Visible = true;


                    ///__ Si le noeud pour les paramétrages bagage est disponible, alors on le rend invisible.
                    if (aitBHS != null)
                        aitBHS.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// Fonction qui permet de générer les définitions des tables Bagages pour le terminal \ref iTerminal.
        /// </summary>
        /// <param name="iTerminal">Index du terminal à ajouter aux données BHS.</param>
        internal static void AddTerminalInputTables(int iTerminal)
        {
            ///On essaie de récupérer le noeud représentant le noeud BHS (visible et accessible uniquement en mode \ref PAX2SIM.EnumPerimetre.BHS
            ///ou \ref PAX2SIM.EnumPerimetre.TMS.
            ArboInputTable aitBHS = aitInputData.GetChildNamed(GestionDonneesHUB2SIM.sBHSAnalysis);

            ///Si le noeud BHS n'existe pas, alors la fonction s'arrète.
            if (aitBHS == null)
                return;

            String sPrefixeTerminal = GlobalNames.sBHS_PrefixeLong + iTerminal.ToString() + "_";
            ///La fonction essaie de récupérer le noeud qui représente le terminal qui doit être ajoutée. (Si ce noeud existe déjà, 
            ///alors la fonction ne fera rien).
            ArboInputTable aitTerminal = aitBHS.GetChildNamed(GlobalNames.BHS_Prefixe_Name + iTerminal.ToString());
            if (aitTerminal != null)
                return;
            ///Création d'un noeud pour le terminal courant.
            aitTerminal = aitTerminal = ArboInputTable.getDirectory(GlobalNames.BHS_Prefixe_Name + iTerminal.ToString());
            LoadParameters lpTmp;
            ConditionnalFormatCharacter cfcAllocation = null;
            ///Initialisation des tables BHS qui sont indexées sur le numéro de terminal.
            #region BHS Normal
            #region BHS_General
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_General;
            lpTmp.sFullName = "General BHS Information";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHSGeneralTable_Data, typeof(String)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHSGeneralTable_Value, typeof(Double)));

            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sBHSGeneralTable_Data);
            lpTmp.oDefaultFirstColumn = new String[] {
                         GlobalNames.sBHSGeneralTable_NumberArrivalInfeed ,
                         GlobalNames.sBHSGeneralTable_LastArrivalInfeed ,
                         GlobalNames.sBHSGeneralTable_NumberCI ,
                         GlobalNames.sBHSGeneralTable_LastCI ,
                         GlobalNames.sBHSGeneralTable_NumberTransferInfeed,
                         GlobalNames.sBHSGeneralTable_LastTransferInfeed ,
                         GlobalNames.sBHSGeneralTable_NumberHBS1 ,
                         GlobalNames.sBHSGeneralTable_LastHBS1,
                         GlobalNames.sBHSGeneralTable_NumberHBS3 ,
                         GlobalNames.sBHSGeneralTable_LastHBS3 ,
                         GlobalNames.sBHSGeneralTable_NumberME,
                         GlobalNames.sBHSGeneralTable_LastME ,
                         GlobalNames.sBHSGeneralTable_NumberMUP,
                         GlobalNames.sBHSGeneralTable_LastMUP ,
                         GlobalNames.sBHSGeneralTable_NumberCIColl,
                         GlobalNames.sBHSGeneralTable_LastCIColl ,
                         GlobalNames.sBHSGeneralTable_WindowColl ,
                         GlobalNames.sBHSGeneralTable_WindowSize};

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = 0.0;

            lpTmp.bAllowAddColumns = false;
            lpTmp.tTypeNewColumns = null;
            lpTmp.bFlightCategoryTable = false;
            lpTmp.bShemaAirportTable = true;
            lpTmp.bSAirport_AnalyseLigne = false;
            lpTmp.bSAirport_AnalyseColonne = false;
            lpTmp.iSAirport_TypeRecherche = -1;     //Exception pour cette table.

            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true, Color.White,
                        Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell, VisualisationMode.EditModeEnum.Cell,
                        null, new int[] { 150, 40 }, null, null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_ArrivalInfeed_Group
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_ArrivalInfeed_Groups;
            lpTmp.sFullName = "Arrival Infeed Groups";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_ArrivalInfeed_Group_ArrGroup, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_ArrivalInfeed_Group_ArrStart, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_ArrivalInfeed_Group_ArrEnd, typeof(Int32)));

            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sBHS_ArrivalInfeed_Group_ArrGroup);
            lpTmp.oDefaultFirstColumn = null;

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = false;
            lpTmp.tTypeNewColumns = null;
            lpTmp.bFlightCategoryTable = false;
            lpTmp.bShemaAirportTable = true;
            lpTmp.bSAirport_AnalyseLigne = true;
            lpTmp.bSAirport_AnalyseColonne = false;
            lpTmp.iSAirport_TypeRecherche = 23;

            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(false, false, true, null, new int[] { 0 }, false, false, Color.White,
                    Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell, VisualisationMode.EditModeEnum.None,
                    new int[] { 0 }, new int[] { 70, 70, 70 }, null, null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_CI_Groups
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_CI_Groups;
            lpTmp.sFullName = "Check-In Groups";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_Groups_CIGroup, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_Groups_CIStart, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_Groups_CIEnd, typeof(Int32)));

            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sBHS_CI_Groups_CIGroup);
            lpTmp.oDefaultFirstColumn = null;

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = false;
            lpTmp.tTypeNewColumns = null;
            lpTmp.bFlightCategoryTable = false;
            lpTmp.bShemaAirportTable = true;
            lpTmp.bSAirport_AnalyseLigne = true;
            lpTmp.bSAirport_AnalyseColonne = false;
            lpTmp.iSAirport_TypeRecherche = 17;

            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(false, false, true, null, new int[] { 0 }, false, false, Color.White,
                    Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell, VisualisationMode.EditModeEnum.None,
                    new int[] { 0 }, new int[] { 70, 70, 70 }, null, null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_TransferInfeed_Groups
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_TransferInfeed_Groups;
            lpTmp.sFullName = "Transfer Infeed Groups";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_TransferInfeed_Group_ArrGroup, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_TransferInfeed_Group_ArrStart, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_TransferInfeed_Group_ArrEnd, typeof(Int32)));

            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sBHS_TransferInfeed_Group_ArrGroup);
            lpTmp.oDefaultFirstColumn = null;

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = false;
            lpTmp.tTypeNewColumns = null;
            lpTmp.bFlightCategoryTable = false;
            lpTmp.bShemaAirportTable = true;
            lpTmp.bSAirport_AnalyseLigne = true;
            lpTmp.bSAirport_AnalyseColonne = false;
            lpTmp.iSAirport_TypeRecherche = 24;

            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(false, false, true, null, new int[] { 0 }, false, false, Color.White,
                    Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell, VisualisationMode.EditModeEnum.None,
                    new int[] { 0 }, new int[] { 70, 70, 70 }, null, null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_CI_Collectors
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_CI_Collectors;
            lpTmp.sFullName = "Check-In Collectors";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_Collectors_Collector, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_Collectors_CI_Desk_Start, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_Collectors_CI_Desk_End, typeof(Int32)));

            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sBHS_CI_Collectors_Collector);
            lpTmp.oDefaultFirstColumn = null;

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = false;
            lpTmp.tTypeNewColumns = null;
            lpTmp.bFlightCategoryTable = false;
            lpTmp.bShemaAirportTable = true;
            lpTmp.bSAirport_AnalyseLigne = true;
            lpTmp.bSAirport_AnalyseColonne = false;
            lpTmp.iSAirport_TypeRecherche = 15;

            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, true, new int[] { 0 }, new int[] { 0 }, true, true, Color.White,
                Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row, VisualisationMode.EditModeEnum.Row,
                new int[] { 0 }, new int[] { 70, 70, 70 }, null, null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_CI_Routing
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_CI_Routing;
            lpTmp.sFullName = "Check-In Routing";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_RoutingCI_Group, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_RoutingHBS1_Start, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_RoutingHBS1_End, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_RoutingHBS3_Start, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_RoutingHBS3_End, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_RoutingMES_Start, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_RoutingMES_End, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_RoutingMakeUp_Start, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_CI_RoutingMakeUp_End, typeof(Int32)));

            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sBHS_CI_RoutingCI_Group);
            lpTmp.oDefaultFirstColumn = null;

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = false;
            lpTmp.tTypeNewColumns = null;
            lpTmp.bFlightCategoryTable = false;
            lpTmp.bShemaAirportTable = true;
            lpTmp.bSAirport_AnalyseLigne = true;
            lpTmp.bSAirport_AnalyseColonne = false;
            lpTmp.iSAirport_TypeRecherche = 13;

            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, true, new int[] { 0 }, new int[] { 0 }, true, true, Color.White,
                        Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row, VisualisationMode.EditModeEnum.Row,
                        new int[] { 0 }, null, null, null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_HBS3_Routing
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_HBS3_Routing;
            lpTmp.sFullName = "HBS3 Routing";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_HBS3_Routing_HBS3, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_HBS3_RoutingMES_Start, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_HBS3_RoutingMES_End, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_HBS3_RoutingHBS1_Start, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_HBS3_RoutingHBS1_End, typeof(Int32)));

            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sBHS_HBS3_Routing_HBS3);
            lpTmp.oDefaultFirstColumn = null;

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = false;
            lpTmp.tTypeNewColumns = null;
            lpTmp.bFlightCategoryTable = false;
            lpTmp.bShemaAirportTable = true;
            lpTmp.bSAirport_AnalyseLigne = true;
            lpTmp.bSAirport_AnalyseColonne = false;
            lpTmp.iSAirport_TypeRecherche = 34;

            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, true, new int[] { 0 }, new int[] { 0 }, true, true, Color.White,
                        Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row, VisualisationMode.EditModeEnum.Row,
                        new int[] { 0 }, null, null, null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_Transfer_Routing
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_Transfer_Routing;
            lpTmp.sFullName = "Transfer Routing";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_Transfer_RoutingInfeed_Group, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_Transfer_RoutingHBS1_Start, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_Transfer_RoutingHBS1_End, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_Transfer_RoutingHBS3_Start, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_Transfer_RoutingHBS3_End, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_Transfer_RoutingMES_Start, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_Transfer_RoutingMES_End, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_Transfer_RoutingMakeUp_Start, typeof(Int32)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_Transfer_RoutingMakeUp_End, typeof(Int32)));

            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sBHS_Transfer_RoutingInfeed_Group);
            lpTmp.oDefaultFirstColumn = null;

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = false;
            lpTmp.tTypeNewColumns = null;
            lpTmp.bFlightCategoryTable = false;
            lpTmp.bShemaAirportTable = true;
            lpTmp.bSAirport_AnalyseLigne = true;
            lpTmp.bSAirport_AnalyseColonne = false;
            lpTmp.iSAirport_TypeRecherche = 14;

            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, true, new int[] { 0 }, new int[] { 0 }, true, true, Color.White,
                        Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Row, VisualisationMode.EditModeEnum.Row,
                        new int[] { 0 }, null, null, null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_Flow_Split
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_Flow_Split;
            lpTmp.sFullName = "Flow Split";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_Flow_Split_Flows, typeof(String)));


            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sBHS_Flow_Split_Flows);
            lpTmp.oDefaultFirstColumn = new String[]{GlobalNames.sBHS_Flow_Split_ToHBS2 ,
                                                GlobalNames.sBHS_Flow_Split_ToHBS3 ,
                                                GlobalNames.sBHS_Flow_Split_ToHBS4 ,
                                                GlobalNames.sBHS_Flow_Split_ToHBS5 ,
                                                GlobalNames.sBHS_Flow_Split_ToMESOri ,
                                                GlobalNames.sBHS_Flow_Split_ToMESTrans ,
                                                GlobalNames.sBHS_Flow_Split_InterlinkTrans,
                                                GlobalNames.sBHS_Flow_Split_InterlinkOrig};

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = true;
            lpTmp.tTypeNewColumns = typeof(Double);
            lpTmp.bFlightCategoryTable = true;
            lpTmp.bShemaAirportTable = false;
            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true, Color.White,
                Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column, VisualisationMode.EditModeEnum.Column,
                new int[] { 0 }, new int[] { 150, 30 }, null, null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_Process
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_Process;
            lpTmp.sFullName = "Process";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_Process_ProcessData, typeof(String)));
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sBHS_Process_Value, typeof(Double)));





            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sBHS_Process_ProcessData);
            lpTmp.oDefaultFirstColumn = new String[]{
                   GlobalNames.sBHS_Process_CIProcess ,
                   GlobalNames.sBHS_Process_MESShortRate,
                   GlobalNames.sBHS_Process_MESShortProcess,
                   GlobalNames.sBHS_Process_MESLongProcess ,

                   GlobalNames.sBHS_Process_HBS1Spac ,
                   GlobalNames.sBHS_Process_HBS1Velocity,

                   GlobalNames.sBHS_Process_HBS2Min ,
                   GlobalNames.sBHS_Process_HBS2Mode ,
                   GlobalNames.sBHS_Process_HBS2Max ,
                   GlobalNames.sBHS_Process_HBS2Timeout,
                   GlobalNames.sBHS_Process_HBS2Operators,

                   GlobalNames.sBHS_Process_HBS3Min ,
                   GlobalNames.sBHS_Process_HBS3Mode ,
                   GlobalNames.sBHS_Process_HBS3Max ,
                   GlobalNames.sBHS_Process_HBS3Spac ,
                   GlobalNames.sBHS_Process_HBS3Velocity,

                   GlobalNames.sBHS_Process_HBS4Min ,
                   GlobalNames.sBHS_Process_HBS4Mode,
                   GlobalNames.sBHS_Process_HBS4Max,
                   GlobalNames.sBHS_Process_HBS4Timeout ,
                   GlobalNames.sBHS_Process_HBS4Operators ,
                   GlobalNames.sBHS_Process_HBS4HoldInside ,

                   GlobalNames.sBHS_Process_HBS5Min ,
                   GlobalNames.sBHS_Process_HBS5Mode ,
                   GlobalNames.sBHS_Process_HBS5Max ,

                   GlobalNames.sBHS_Process_MupPickUp,
                   GlobalNames.sBHS_Process_Transf ,
                   GlobalNames.sBHS_Process_Arrival,
                   GlobalNames.sBHS_Process_NumberTray ,
                   GlobalNames.sBHS_Process_SorterVelocity ,
                   GlobalNames.sBHS_Process_SorterTrayLength ,
                   GlobalNames.sBHS_Process_SorterFillingLimit,
                   GlobalNames.sBHS_Process_SorterRecirLimit,
                   GlobalNames.sBHS_Process_SorterTiltInterval,
                   GlobalNames.sBHS_Process_SorterInductInterval,
                   GlobalNames.sBHS_Process_EBSOperating};

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = false;
            lpTmp.tTypeNewColumns = null;
            lpTmp.bFlightCategoryTable = false;
            lpTmp.bShemaAirportTable = false;
            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true, Color.White,
                    Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell, VisualisationMode.EditModeEnum.Cell,
                    null, new int[] { 150, 40 }, null, null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_Alloc_MakeUp
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_Alloc_MakeUp;
            lpTmp.sFullName = "Make-Up (Allocation)";
            lpTmp.bSaved = false;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
            lpTmp.oDefaultFirstColumn = null;
            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = true;
            lpTmp.tTypeNewColumns = typeof(String);
            lpTmp.bFlightCategoryTable = false;
            lpTmp.bShemaAirportTable = true;
            lpTmp.bSAirport_AnalyseLigne = false;
            lpTmp.bSAirport_AnalyseColonne = true;
            lpTmp.iSAirport_TypeRecherche = 16;

            lpTmp.bAllocationTable = true;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            cfcAllocation = new ConditionnalFormatCharacter(',', true);
            cfcAllocation.setCondition(1, Color.FromArgb(0, 255, 0));
            cfcAllocation.setCondition(2, Color.FromArgb(192, 255, 0));
            cfcAllocation.setCondition(3, Color.FromArgb(255, 128, 0));
            cfcAllocation.setCondition(4, Color.FromArgb(255, 0, 0));
            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(false, false, false, new int[] { 0 }, new int[] { 0 }, true, true, Color.White,
                    Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell, VisualisationMode.EditModeEnum.None,
                    new int[] { 0 }, null, null, null, new VisualisationMode.ConditionnalFormat[] { cfcAllocation });

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_Arrival_Containers
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_Arrival_Containers;
            lpTmp.sFullName = "Arrival Containers";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnSelectACategory, typeof(String)));

            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnSelectACategory);
            lpTmp.oDefaultFirstColumn = new String[]{
                   GlobalNames.sBHS_Arrival_Containers_FirstContainer,
                   GlobalNames.sBHS_Arrival_Containers_ContainerSize ,
                   GlobalNames.sBHS_Arrival_Containers_NumberLaterals};

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = new Double[] { 15.0, 40.0, 2.0 };
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = true;
            lpTmp.tTypeNewColumns = typeof(Double);
            lpTmp.bFlightCategoryTable = true;
            lpTmp.bShemaAirportTable = false;
            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true, Color.White, Color.Blue,
                            Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Column, VisualisationMode.EditModeEnum.Column, new int[] { 0 }, null, null,
                            null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_Mean_Flows_Arrival_Infeed
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_Mean_Flows_Arrival_Infeed;
            lpTmp.sFullName = "Mean Flows - Arrival Infeed";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
            lpTmp.oDefaultFirstColumn = null;

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = true;
            lpTmp.tTypeNewColumns = typeof(Double);
            lpTmp.bFlightCategoryTable = false;  // >> Bug #13558 Lyon BHS project
            lpTmp.bShemaAirportTable = true;
            lpTmp.bSAirport_AnalyseLigne = false;
            lpTmp.bSAirport_AnalyseColonne = true;
            lpTmp.iSAirport_TypeRecherche = 20;

            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true, Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                        VisualisationMode.EditModeEnum.Cell, null, null, null, null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_Mean_Flows_Check_In
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_Mean_Flows_Check_In;
            lpTmp.sFullName = "Mean Flows - Check In";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
            lpTmp.oDefaultFirstColumn = null;

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = true;
            lpTmp.tTypeNewColumns = typeof(Double);
            lpTmp.bFlightCategoryTable = false;  // >> Bug #13558 Lyon BHS project
            lpTmp.bShemaAirportTable = true;
            lpTmp.bSAirport_AnalyseLigne = false;
            lpTmp.bSAirport_AnalyseColonne = true;
            lpTmp.iSAirport_TypeRecherche = 21;

            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true, Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                        VisualisationMode.EditModeEnum.Cell, null, null, null, null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion

            #region sBHS_Mean_Flows_Transfer_Infeed
            lpTmp = new LoadParameters();
            lpTmp.sName = sPrefixeTerminal + GlobalNames.sBHS_Mean_Flows_Transfer_Infeed;
            lpTmp.sFullName = "Mean Flows - Transfer Infeed";
            lpTmp.bSaved = true;
            lpTmp.bActive = true;
            lpTmp.bVisible = true;

            lpTmp.dstEntetes = new List<LoadParameters.Entete>();
            lpTmp.dstEntetes.Add(new LoadParameters.Entete(GlobalNames.sColumnTime, typeof(DateTime)));

            lpTmp.lsPrimaryKeys = new List<string>();
            lpTmp.lsPrimaryKeys.Add(GlobalNames.sColumnTime);
            lpTmp.oDefaultFirstColumn = null;

            lpTmp.oDefaultLine = null;
            lpTmp.oDefaultColumn = null;
            lpTmp.oDefaultValue = null;

            lpTmp.bAllowAddColumns = true;
            lpTmp.tTypeNewColumns = typeof(Double);
            lpTmp.bFlightCategoryTable = false;  // >> Bug #13558 Lyon BHS project
            lpTmp.bShemaAirportTable = true;
            lpTmp.bSAirport_AnalyseLigne = false;
            lpTmp.bSAirport_AnalyseColonne = true;
            lpTmp.iSAirport_TypeRecherche = 22;

            lpTmp.bAllocationTable = false;

            lpTmp.bBHS = true;
            lpTmp.bIndexedOnTerminal = true;

            lpTmp.vmDefaultVisualisationMode = new VisualisationMode(true, false, false, new int[] { 0 }, new int[] { 0 }, true, true, Color.White, Color.Blue, Color.Black, Color.White, VisualisationMode.SelectionModeEnum.Cell,
                        VisualisationMode.EditModeEnum.Cell, null, null, null, null, null);

            dslpTableParameters.Add(lpTmp.sName, lpTmp);
            aitTerminal.AddChild(lpTmp);
            #endregion
            #endregion

            ///Ajout du nouveau dossier représentant le terminal aux dossiers contenant les informations bagages.
            aitBHS.AddChild(aitTerminal);

            ///Appel de la fonction de tri \ref ArboInputTable.Sort().
            aitBHS.Sort(false);
        }

        /// <summary>
        /// Fonction qui permet de savoir si un terminal donné à déjà été ajouté pour le paramétrage BHS
        /// </summary>
        /// <param name="iTerminal">Index du terminal à ajouter aux données BHS.</param>
        /// <returns>Booléen indiquant si le terminal est déjà représenté dans les paramétrage BHS ou non.</returns>
        internal static bool IsTerminalBHSAlreadyPresent(int iTerminal)
        {
            ///On essaie de récupérer le noeud représentant le noeud BHS (visible et accessible uniquement en mode \ref PAX2SIM.EnumPerimetre.BHS
            ///ou \ref PAX2SIM.EnumPerimetre.TMS.
            ArboInputTable aitBHS = aitInputData.GetChildNamed(GestionDonneesHUB2SIM.sBHSAnalysis);

            ///Si le noeud BHS n'existe pas, alors la fonction s'arrète.
            if (aitBHS == null)
                return false;

            String sPrefixeTerminal = GlobalNames.BHS_Prefixe_Name + iTerminal.ToString();
            ///La fonction essaie de récupérer le noeud qui représente le terminal qui doit être ajoutée. (Si ce noeud existe déjà, 
            ///alors la fonction ne fera rien).
            ArboInputTable aitTerminal = aitBHS.GetChildNamed(sPrefixeTerminal);
            if (aitTerminal != null)
                return true;
            return false;
        }

        /// <summary>
        /// Fonction qui s'occupe de supprimer de l'arborescence le terminal passé en paramètre.
        /// </summary>
        /// <param name="iTerminal">Index du terminal à supprimer des données BHS.</param>
        /// <returns>La liste des tables qui ont été supprimées à cause de la suppression de ce terminal.</returns>
        internal static List<String> RemoveTerminalInputTable(int iTerminal)
        {
            ///On essaie de récupérer le noeud représentant le noeud BHS (visible et accessible uniquement en mode \ref PAX2SIM.EnumPerimetre.BHS
            ///ou \ref PAX2SIM.EnumPerimetre.TMS.
            ArboInputTable aitBHS = aitInputData.GetChildNamed(GestionDonneesHUB2SIM.sBHSAnalysis);

            ///Si le noeud BHS n'existe pas, alors la fonction s'arrète.
            if (aitBHS == null)
                return new List<string>();

            String sPrefixeTerminal = GlobalNames.sBHS_PrefixeLong + iTerminal.ToString() + "_";

            ///La fonction essaie de récupérer le noeud qui représente le terminal qui doit être ajoutée. (Si ce noeud existe déjà, 
            ///alors la fonction ne fera rien).
            ArboInputTable aitTerminal = aitBHS.GetChildNamed(GlobalNames.BHS_Prefixe_Name + iTerminal.ToString());
            if (aitTerminal == null)
                return new List<string>();

            ///Appel de la fonction de suppression d'un objet enfant \ref ArboInputTable.RemoveChild().
            aitBHS.RemoveChild(GlobalNames.BHS_Prefixe_Name + iTerminal.ToString());

            List<string> lsReturn = new List<string>();

            ///Suppression de la variable \ref dslpTableParameters des définition des tables utilisées pour ce terminal.
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_General))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_General);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_ArrivalInfeed_Groups))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_ArrivalInfeed_Groups);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_CI_Groups))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_CI_Groups);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_TransferInfeed_Groups))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_TransferInfeed_Groups);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_CI_Collectors))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_CI_Collectors);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_CI_Routing))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_CI_Routing);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_HBS3_Routing))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_HBS3_Routing);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_Transfer_Routing))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_Transfer_Routing);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_Flow_Split))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_Flow_Split);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_Process))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_Process);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_Alloc_MakeUp))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_Alloc_MakeUp);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_Arrival_Containers))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_Arrival_Containers);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_Mean_Flows_Arrival_Infeed))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_Mean_Flows_Arrival_Infeed);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_Mean_Flows_Check_In))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_Mean_Flows_Check_In);
            if (dslpTableParameters.Remove(sPrefixeTerminal + GlobalNames.sBHS_Mean_Flows_Transfer_Infeed))
                lsReturn.Add(sPrefixeTerminal + GlobalNames.sBHS_Mean_Flows_Transfer_Infeed);
            return lsReturn;
        }

        /// <summary>
        /// Fonction qui permet de mettre les paramétrage en mode Trialversion, mode limité.
        /// </summary>
        internal static void SetTrialModeVersion()
        {
            ///Si \ref dslpTableParameters est null, appel de la fonction \ref InitializeTableParameters().
            if (dslpTableParameters == null)
                InitializeTableParameters();
            ///Un certain nombre de tables sont rendues invisibles.
            SetValues(GlobalNames.Transfer_TerminalDitributionTableName, false, false, false);
            SetValues(GlobalNames.Transfer_FlightCategoryDitributionTableName, false, false, false);
            SetValues(GlobalNames.OCT_ParkingTableName, false, false, false);
            SetValues(GlobalNames.Alloc_ParkingTableName, false, false, false);
            SetValues(GlobalNames.OCT_MakeUpTableName, false, false, false);
            SetValues(GlobalNames.SegregationName, false, false, false);
            SetValues(GlobalNames.TransferInfeedAllocationRulesTableName, false, false, false);
            SetValues(GlobalNames.Baggage_Claim_ConstraintName, false, false, false);
            SetValues(GlobalNames.Animated_QueuesName, false, false, false);
            SetValues(GlobalNames.ProcessScheduleName, false, false, false);
        }

        /// <summary>
        /// Fonction qui permet de changer le status d'une table aux paramètres passés.
        /// </summary>
        /// <param name="sName">Nom de la table pour laquelle on veut modifier les paramétrage.</param>
        /// <param name="bVisible">Paramétrage sur la visibilité</param>
        /// <param name="bActive">Paramétrage sur l'activation</param>
        /// <param name="bSaved">Paramétrage sur la sauvegarde</param>
        internal static void SetValues(String sName, bool bVisible, bool bActive, bool bSaved)
        {
            ///Si \ref dslpTableParameters est null, appel de la fonction \ref InitializeTableParameters().
            if (dslpTableParameters == null)
                InitializeTableParameters();
            if (dslpTableParameters.ContainsKey(sName))
            {
                ///Si la table contient une définition pour la table recherchée, alors les paramétrage pour cette table sont mis à jour.
                LoadParameters lpTmp = dslpTableParameters[sName];
                lpTmp.bVisible = bVisible;
                lpTmp.bActive = bActive;
                lpTmp.bSaved = bSaved;
            }
        }
        #endregion

        /// <summary>
        /// Fonction pour remetre par defaut (true) la variable qui pour chaque table definie sur elle
        /// doit tenir compte des exceptions.
        /// </summary>
        internal void ResetUseException()
        {
            if (dsntTables != null)
                foreach (NormalTable table in dsntTables.Values)
                {
                    ExceptionTable exTable = table as ExceptionTable;
                    if (exTable != null)
                        exTable.UseException = true;
                }

            // Segregation se traite differement
            NormalTable ntTable = GetTable(GlobalNames.SegregationName);
            if (ntTable != null)
            {
                ExceptionTable exTable = ntTable as ExceptionTable;
                if (exTable != null)
                    exTable.UseException = true;
            }
        }

        #endregion

        #region Variables
        /// <summary>
        /// Le dictionnaire contenant les tables UserData.(\ref UserDataTable)
        /// </summary>
        Dictionary<String, UserDataTable> dsudtUserData;

        /// <summary>
        /// Dictionnaire contenant les paramétrages d'allocation automatique.
        /// </summary>
        Dictionary<String, Classes.GenerateAllocationTool> dsgatAllocations;

        /// <summary>
        /// Délégué permettant de définir la fonction qui permet de récupérer la structure de l'aéroport.
        /// </summary>
        /// <returns>XmlNode représentant la structure de l'aéroport.</returns>
        internal delegate XmlNode GetStructureAirportDelegate();

        /// <summary>
        /// Fonction pour récupérer la structure de l'aéroport.
        /// </summary>
        private GetStructureAirportDelegate GetStructureAirportFunction;

        /// <summary>
        /// Délégué permettant de définir la fonction qui permet de récupérer l'information sur le fiat que le projet utilise les codes AlphaNumerique ou non pour les objets de l'aéroport.
        /// </summary>
        /// <returns>Booléen indiquant si les plans de vol utilise des données alphanumerique ou non.</returns>
        internal delegate bool GetUseAlphaNumericalFlightPlanDelegate();

        /// <summary>
        /// Fonction pour récupérer pour savoir si les plans de vol utilise des données alphanumerique ou non
        /// </summary>
        private GetUseAlphaNumericalFlightPlanDelegate GetUseAlphaNumericalFlightPlanFunction;

        /// <summary>
        /// Délégué permettant de déterminer le Perimetre actuellement utilisé dans le projet.
        /// </summary>
        /// <returns>Perimetre du projet.</returns>
        internal delegate PAX2SIM.EnumPerimetre GetPerimeterDelegate();

        /// <summary>
        /// Fonction pour déterminer le Perimetre actuellement utilisé dans le projet.
        /// </summary>
        private GetPerimeterDelegate GetPerimeterFunction;

        /// <summary>
        /// Délégué permettant de définir la fonction a appelée pour mettre à jour les tables suivant la structure de l'aéroport.
        /// </summary>
        /// <param name="sTableName">Nom de la table à mettre à jour.</param>
        internal delegate void UpdateTableFromAirportStructureDelegate(String sTableName, LoadParameters lpParameters);

        /// <summary>
        /// Fonction permettant de définir la fonction a appelée pour mettre à jour les tables suivant la structure de l'aéroport.
        /// </summary>
        private UpdateTableFromAirportStructureDelegate UpdateTableFromAirportStructure;

        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la classe qui initialise la totalité des variables. Elle appelle également le constructeur de la classes
        /// héritées \ref DataManaer(String Name)
        /// </summary>
        /// <param name="Name">Nom du nouveau système de donnée qui doit être créer.</param>
        internal DataManagerInput(String Name)
            : base(Name)
        {
            dsudtUserData = new Dictionary<string, UserDataTable>();
            dsgatAllocations = new Dictionary<string, Classes.GenerateAllocationTool>();
        }

        /// <summary>
        /// Constructeur de la classe qui initialise la totalité des variables. Elle appelle également le constructeur de la classes
        /// héritées \ref DataManaer(XmlNode Node). Elle charge également les tables à partir du noeud XML.
        /// </summary>
        /// <param name="Node"></param>
        internal DataManagerInput(XmlNode Node)
            : base(Node)
        {
            dsgatAllocations = new Dictionary<string, Classes.GenerateAllocationTool>();
            if (!OverallTools.FonctionUtiles.hasNamedChild(Node, "Allocations"))
                return;
            foreach (XmlNode xnChild in Node["Allocations"].ChildNodes)
            {
                Classes.GenerateAllocationTool gatTmp = new Classes.GenerateAllocationTool(xnChild, new Classes.GenerateAllocationTool.GetTable(GetTableDelegate), new Classes.GenerateAllocationTool.GetTableException(GetTableException));
                AddAllocation(gatTmp);
            }
        }

        /// <summary>
        /// Constructeur par copie pour le \ref DataManagerInput. Qui permet de créer une nouvelle instance de \ref DataManagerInput, copie conforme
        /// à celui passé en parametre.
        /// </summary>
        /// <param name="dmiOldDataManager">Système de donnée qui doit être dupliquer.</param>
        internal DataManagerInput(DataManagerInput dmiOldDataManager)
            : base(dmiOldDataManager)
        {
            ///Initialisation des différentes variables présentes dans le système de données.
            dsudtUserData = new Dictionary<string, UserDataTable>();
            dsgatAllocations = new Dictionary<string, Classes.GenerateAllocationTool>();
        }
        #endregion

        #region Pour les Tables
        /// <summary>
        /// Fonction qui se base sur la liste \ref lsListUsedNames pour déterminer si un nom est déjà utiliser dans le système de données
        /// ou non.
        /// </summary>
        /// <param name="Name">Nom recherché</param>
        /// <returns>Booléen indiquant si le nom est utilisé ou pas par le système de données.</returns>
        public override bool Exist(String sTableName)
        {
            ///Si la fonction \ref UserDataExists renvoie vrai, alors la fonction renvoie VRAI.
            if (UserDataExists(sTableName))
                return true;
            ///Sinon la fonction renvoie ce que retournera la fonction \ref DataManager.Exist().
            return base.Exist(sTableName);
        }

        /// <summary>
        /// Fonction permettant de récupérer le \ref NormalTable associé à la table passée en parametre.
        /// </summary>
        /// <param name="sTableName">Nom de la table recherchée</param>
        /// <returns>La table recherchée ou NULL si celle ci n'existe pas.</returns>
        internal override NormalTable GetTable(String sTableName)
        {
            ///Si la fonction \ref UserDataExists renvoie FAUX, alors la fonction renvoie ce que retournera la fonction \ref DataManager.GetTable().
            if (!UserDataExists(sTableName))
                return base.GetTable(sTableName);

            ///Sinon la fonction renvoie la table UserData.
            NormalTable ntReturn = null;
            foreach (String sKey in dsudtUserData.Keys)
            {
                ntReturn = dsudtUserData[sKey].GetTable(sTableName);
                if (ntReturn != null)
                    return ntReturn;
            }
            return null;
        }

        /// <summary>
        /// Fonction qui renvoie une table UserData au format enregistré sur disque. Si la table UserData n'est
        /// pas valide pour cette représentation, alors la fonction renvoie NULL. Une table valide pour cette
        /// représentation est une table qui comporte un nombre fini de colonnes qui sont toutes nommées avec 
        /// un nom distinct et unique. Ces noms de colonnes doivent être présent à la première ligne du fichier.
        /// </summary>
        /// <param name="sTableName">Le nom de la table qui doit être formatée au format Table.</param>
        /// <returns>Null, ou alors la table formatée.</returns>
        internal NormalTable GetFormatedUserDataTable(String sTableName)
        {
            ///Si la fonction \ref UserDataExists renvoie FAUX, alors la fonction renvoie ce que retournera la fonction \ref DataManager.GetTable().
            if (!UserDataExists(sTableName))
                return null;
            NormalTable ntReturn = null;
            foreach (String sKey in dsudtUserData.Keys)
            {
                ntReturn = dsudtUserData[sKey].GetFormatedTable(sTableName);
                if (ntReturn != null)
                    return ntReturn;
            }
            return null;
        }
        // >> Task #12393 Pax2Sim - File conversion for Athens
        internal NormalTable GetFormatedUserDataTableForFlightPlan(String sTableName)
        {
            ///Si la fonction \ref UserDataExists renvoie FAUX, alors la fonction renvoie ce que retournera la fonction \ref DataManager.GetTable().
            if (!UserDataExists(sTableName))
                return null;
            NormalTable ntReturn = null;
            foreach (String sKey in dsudtUserData.Keys)
            {
                ntReturn = dsudtUserData[sKey].GetFormatedFlightPlanTable(sTableName);
                if (ntReturn != null)
                    return ntReturn;
            }
            return null;
        }
        // << Task #12393 Pax2Sim - File conversion for Athens
        /// <summary>
        /// Fonction qui ajoute le \ref NormalTable passé en paramètre au dictionnaire \ref dsntTables des tables. Cette
        /// fonction a été redéfinie afin de permettre de transformer les \ref NormalTable en \ref ExceptionTable pour 
        /// les tables le nécessitant.
        /// On ajoute également la définition de la fonction a utilisé pour vérifier les tables. \ref DataManagerInput.CheckTableDelegate
        /// </summary>
        /// <param name="ntTable">Table à ajouter.</param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé ou non.</returns>
        internal override bool AddTable(NormalTable ntTable)
        {
            ///Si la table est null, alors la fonction renvoie FAUX. (Impossible d'ajouter une table vide)
            if (ntTable == null)
                return false;
            ///Récupération du nom de la table mère (s'il s'agit d'un filtre, il faut remonter jusqu'à la table racine.
            String sParent = ntTable.Name;
            if (ntTable.isFilter)
            {
                sParent = ((IFilterTable)ntTable).Root.Name;
            }

            ///Mis à jour dans la table de la fonction de vérification des tables \ref NormalTable.CheckTableFunction
            ntTable.CheckTableFunction = new DataManagerInput.CheckTableDelegate(CheckTable);

            ///Recherche des paramètres \ref ExceptionTable.ExceptionTableParameters pour la table mère. Si celle ci n'est
            ///pas sujette aux exceptions, alors appel de la fonction \ref DataManager.AddTable() et arret de la fonction.
            ExceptionTable.ExceptionTableParameters etpTmp = GetExceptionParameters(sParent);
            if (etpTmp == ExceptionTable.ExceptionTableParameters.None)
                return base.AddTable(ntTable);

            ///Conversion du \ref NormalTable en \ref ExceptionTable, afin que la représentation de la table puisse gérer les
            ///exceptions.
            ExceptionTable.ExceptionTableFormat etfTmp = GetExceptionFormats(sParent);
            ExceptionTable etTable = null;
            if (ntTable is ExceptionTable)
                etTable = (ExceptionTable)ntTable;
            else
                etTable = new ExceptionTable(ntTable);
            etTable.ExceptionType = etpTmp;
            etTable.ExceptionFormat = etfTmp;
            return base.AddTable(etTable);
        }

        /// <summary>
        /// Fonction qui permet de lancer l'initialisation des tables manquantes dans le système de données.
        /// </summary>
        internal void InitializeTables()
        {
            ///Si \ref dslpTableParameters est null, alors appel de la fonction \ref InitializeTableParameters
            if (dslpTableParameters == null)
                InitializeTableParameters();
            ///Pour chaque table présente dans \ref dslpTableParameters
            foreach (String sName in dslpTableParameters.Keys)
            {
                ///__ Récupération des paramètres \ref LoadParameters pour cette table.
                LoadParameters lpParam = dslpTableParameters[sName];
                ///__ Si la table n'est pas active, passage à la table suivante (\ref LoadParameters.bActive à FAUX)
                if (!lpParam.bActive)
                    continue;

                ///__ Si la table n'existe pas dans le \ref DataManagerInput
                if (!Exist(sName))
                {
                    ///____ Création de la table grace à la fonction \ref LoadParameters.InitializeTable()
                    DataTable dtNewTable = lpParam.InitializeTable();
                    ///____ Si la table a des lignes avec la première colonne fixe(\ref LoadParameters.oDefaultFirstColumn différent de NULL), alors 
                    ///initialisation de celles ci grace à \ref OverallTools.FonctionUtiles.initialiserLignesTable()
                    if (lpParam.oDefaultFirstColumn != null)
                    {
                        OverallTools.FonctionUtiles.initialiserLignesTable(dtNewTable, (String[])lpParam.oDefaultFirstColumn);
                    }
                    ///____ Ajout de la table nouvellement créée au système de données \ref AddTable().
                    AddTable(dtNewTable);
                }
                NormalTable ntTable = GetTable(sName);
                if ((ntTable != null) && (ntTable.TableParameters == null))
                    ntTable.TableParameters = lpParam;
                //<< Task #7405 - new Desk and extra information for Pax                
                if (ntTable is ExceptionTable)
                {
                    ExceptionTable etTable = (ExceptionTable)ntTable;
                    etTable.ExceptionType = GetExceptionParameters(ntTable.Name);
                    etTable.ExceptionFormat = GetExceptionFormats(ntTable.Name);
                }
                //>> Task #7405 - new Desk and extra information for Pax
                ///____ Si la table a des lignes avec la première colonne fixe(\ref LoadParameters.oDefaultFirstColumn différent de NULL), alors 
                ///vérification de la table pour être sur que cette table est conforme à ce qu'elle doit contenir.
                if (lpParam.oDefaultFirstColumn != null)
                {
                    if (ntTable == null)
                        continue;
                    DataTable dtNewTable = ntTable.Table;
                    if (dtNewTable == null)
                        continue;
                    dtNewTable = ConvertTables.ConvertLine(dtNewTable, (String[])lpParam.oDefaultFirstColumn, (Double[])lpParam.oDefaultColumn);
                    UpdateTable(dtNewTable);
                }
            }
        }

        /// <summary>
        /// Fonction qui retourne la liste des tables contenues dans le \ref DataManagerInput qui se basent sur 
        /// les catégories de vol.
        /// </summary>
        /// <returns></returns>
        internal List<String> getTablesForFlightCategoriesUpdate()
        {
            List<String> lsResults = new List<string>();
            if (dsntTables == null)
                return lsResults;
            foreach (String sTable in dsntTables.Keys)
            {
                NormalTable ntTable = GetTable(sTable);
                if (ntTable == null)
                    continue;
                if (ntTable.TableParameters == null)
                    continue;
                if (!ntTable.TableParameters.bFlightCategoryTable)
                    continue;
                lsResults.Add(sTable);
            }
            return lsResults;
        }
        #endregion

        #region Pour les Filtres
        /// <summary>
        /// Fonction qui permet de mettre à jour les filtres dans l'arborescence. Cela permet d'ajouter les filtres aux tables qui en
        /// possèdent.
        /// </summary>
        /// <param name="treeNode">Noeud racine des données contenus dans le système de données courant.</param>
        /// <param name="cmsMenuFilter">Menu contextuel qui doit être ajouté aux filtes ainsi ajoutés.</param>
        internal override void UpdateFilters(System.Windows.Forms.TreeNode treeNode, System.Windows.Forms.ContextMenuStrip cmsMenuFilter, ContextMenuStrip cmsMenuException)
        {
            ///Appel de la fonction de la classe de base \ref DataManager.UpdateFilters
            base.UpdateFilters(treeNode, cmsMenuFilter, cmsMenuException);
        }

        /// <summary>
        /// Fonction qui permet de récupérer les noms de tous les filtres de la table passée en paramètre (ainsi que le nom de la table passée en paramètres).
        /// </summary>
        /// <param name="sMainTableName">Nom de la table que l'on souhaite prendre comme racine pour récupérer les noms des filtres.</param>
        /// <returns>Liste de tous les noms des filtres et de la table racine. Si la table n'existe pas dans le système de données, alors la fonction renvoie une liste vide.</returns>
        internal List<String> getTableFilters(String sMainTableName)
        {
            List<String> lsResult = new List<string>();
            ///Si la table n'existe pas dans le système de données, alors la fonction renvoie une liste vide.
            if (!Exist(sMainTableName))
                return new List<string>();
            ///Ajout dans la liste qui sera retournée de \ref sMainTableName.
            lsResult.Add(sMainTableName);
            ///Ajout dans la liste du résultat de la fonction \ref NormalTable.GetAllFilters
            lsResult.AddRange(GetTable(sMainTableName).GetAllFilters());
            return lsResult;
        }



        #endregion

        #region Pour les tables d'exceptions et leurs paramétrages
        /// <summary>
        /// Fonction qui renvoie les parametres d'exception pour la table passée en parametres.
        /// </summary>
        /// <param name="sTableName">Nom de la table.</param>
        /// <returns>Parametres d'exception ou \ref ExceptionTable.ExceptionTableParameters.None si aucun parametres.</returns>
        internal ExceptionTable.ExceptionTableParameters GetExceptionParameter(String sTableName)
        {
            ///Si la table n'existe pas, la fonction renvoie \ref ExceptionTable.ExceptionTableParameters.None
            if (!Exist(sTableName))
                return ExceptionTable.ExceptionTableParameters.None;
            NormalTable ntTmp = GetTable(sTableName);
            ///Si la table n'est pas une table avec des exceptions, la fonction renvoie \ref ExceptionTable.ExceptionTableParameters.None
            if (!(ntTmp is ExceptionTable))
                return ExceptionTable.ExceptionTableParameters.None;
            ///Renvoyer le paramétrage des exceptions pour la table.
            return ((ExceptionTable)ntTmp).ExceptionType;
        }

        /// <summary>
        /// Fonction qui renvoie le format d'exception pour la table passée en parametres.
        /// </summary>
        /// <param name="sTableName">Nom de la table.</param>
        /// <returns>Format des exceptions</returns>
        internal ExceptionTable.ExceptionTableFormat GetExceptionFormat(String sTableName)
        {
            ///Si la table n'existe pas, la fonction renvoie \ref ExceptionTable.ExceptionTableFormat.Column
            if (!Exist(sTableName))
                return ExceptionTable.ExceptionTableFormat.Column;
            NormalTable ntTmp = GetTable(sTableName);
            ///Si la table n'est pas une table avec des exceptions, la fonction renvoie \ref ExceptionTable.ExceptionTableFormat.Column
            if (!(ntTmp is ExceptionTable))
                return ExceptionTable.ExceptionTableFormat.Column;
            ///Renvoyer le format des exceptions pour la table.
            return ((ExceptionTable)ntTmp).ExceptionFormat;
        }
        #endregion

        #region Pour les allocations
        /// <summary>
        /// Fonction qui permet d'ajouter des allocations au système de données courant.
        /// </summary>
        /// <param name="gatTmp">Paramétrage des allocations.</param>
        internal void AddAllocation(Classes.GenerateAllocationTool gatTmp)
        {
            ///Si le nom des allocation existe déjà la fonction ne fait rien.
            if (Exist(gatTmp.NomTable))
                return;
            ///Si des allocation avec le même nom existe déjà, alors on met à jour le paramétrage,
            if (dsgatAllocations.ContainsKey(gatTmp.NomTable))
                dsgatAllocations[gatTmp.NomTable] = gatTmp;
            ///Sinon on ajoute dans \ref dsgatAllocations le nouveau paramétrage.
            dsgatAllocations.Add(gatTmp.NomTable, gatTmp);
        }

        /// <summary>
        /// Permet de supprimer du système de données un paramétrage pour les allocations.
        /// </summary>
        /// <param name="Name">Nom du paramétrage à supprimer.</param>
        internal void RemoveAllocation(String Name)
        {
            ///On supprime de \ref dsgatAllocations le paramétrage pour ces allocations.
            if (dsgatAllocations.ContainsKey(Name))
                dsgatAllocations.Remove(Name);
        }

        /// <summary>
        /// Fonction qui permet de savoir si un nom fait partie des tables ayant des paramétrages d'allocation.
        /// </summary>
        /// <param name="Name">Nom de l'objet recherché</param>
        /// <returns>Booléen indiquant s'il existe ou non.</returns>
        internal bool IsAllocated(String Name)
        {
            ///Si \ref dsgatAllocations contient le \ref Name, alors la fonction renvoie VRAI, sinon elle renvoie FAUX.
            return dsgatAllocations.ContainsKey(Name);
        }

        /// <summary>
        /// Fonction qui permet de récupérer la définition d'une allocation.
        /// </summary>
        /// <param name="Name">Nom recherché.</param>
        /// <returns>Paramétrage d'allocation ou alors NULL.</returns>
        internal Classes.GenerateAllocationTool GetAllocation(String Name)
        {
            ///Si \ref IsAllocated renvoie FAUX, la fonction renvoie NULL.
            if (!IsAllocated(Name))
                return null;
            ///Sinon la fonction renvoie le paramétrage pour l'allocation demandé.
            return dsgatAllocations[Name];
        }

        /// <summary>
        /// Fonction déléguée permettant de récupérer une Table à partir de son nom (utilisé pour les allocations)
        /// </summary>
        /// <param name="Name">Nom de la table recherchée</param>
        /// <returns>DataTable ou null/returns>
        private DataTable GetTableDelegate(String Name)
        {

            if (!Exist(Name))
                return null;
            return GetTable(Name).Table;
        }

        /// <summary>
        /// Fonction permettant de récupérer le \ref ExceptionTables associé à la table passée en parametre (utilisé pour les allocations)
        /// </summary>
        /// <param name="sTableName">Nom de la table recherchée</param>
        /// <returns>La table recherchée ou NULL si celle ci n'existe pas.</returns>
        internal ExceptionTable GetTableException(String sTableName)
        {
            ///Si la table n'existe pas dans le système actuel de données. Renvoyer NULL.
            if (!Exist(sTableName))
                return null;
            ///Récupération de la table (\ref NormalTable) stockée dans la classe.
            NormalTable ntTmp = GetTable(sTableName);
            ///Si la table n'existe pas ou qu'elle n'est pas une table \ref ExceptionTable, alors la fonction renvoie NULL.
            if (ntTmp == null)
                return null;
            if (!(ntTmp is ExceptionTable))
                return null;
            ///Renvoyer la table demandée.
            return (ExceptionTable)ntTmp;

        }

        #endregion

        #region Pour les User data

        /// <summary>
        /// Fonction qui charge la totalité des userData présents dans le dossier passé en paramètre. Cette fonction descendra également 
        /// récursivement d'un niveau dans le système de fichier, créeant un UserData par dossier et incluant la totalité des fichiers
        /// présents dans ce sous dossier dans le UserData créer.
        /// </summary>
        /// <param name="Directory">Adresse du dossier contenant les UserData.</param>
        internal void LoadUserData(String Directory)
        {
            ///Si le dossier n'existe pas, alors la fonction s'arrète.
            if (!System.IO.Directory.Exists(Directory))
                return;
            ///Appe de la fonction \ref LoadUserData(String Directory, UserDataTable udtCurrentUserData) avec le dossier courant.
            LoadUserData(Directory, null);

            ///Récupération de la totalité des sous répertoires présent dans le dossier courant.
            String[] Directories = System.IO.Directory.GetDirectories(Directory);
            ///Pour chacun des sous répertoires présents dans le répertoire \ref Directory
            foreach (String sDirectory in Directories)
            {
                ///__ Création d'un UserData (\ref UserDataTable) avec le nom du sous répertoire.
                String sDirectoryName = sDirectory.Substring(sDirectory.LastIndexOf("\\") + 1);
                UserDataTable udtUserData = new UserDataTable(sDirectoryName);

                ///__ Ajout de ce UserData à la liste (\ref dsudtUserData) des UserData.
                dsudtUserData.Add(sDirectoryName, udtUserData);
                ///__ Appel de la fonction  LoadUserData(String Directory, UserDataTable udtCurrentUserData) avec le sous répertoire et le UserData créer.
                LoadUserData(sDirectory, udtUserData);
            }
        }

        /// <summary>
        /// Cette fonction s'occupe de générer les UserData à partir d'un dossier. Si le \ref udtCurrentUserData n'est pas NULL, alors chacun des
        /// fichiers trouvés dans le répertoire seront ajouter au UserData (\ref udtCurrentUserData).
        /// </summary>
        /// <param name="Directory">Dossier courant à observer.</param>
        /// <param name="udtCurrentUserData">UserData auquel ajouter les fichiers. (Peut être NULL, auquel cas chacun des fichiers générera un nouveau UserData).</param>
        private void LoadUserData(String Directory, UserDataTable udtCurrentUserData)
        {
            ///Si le dossier n'existe pas, alors la fonction s'arrète.
            if (!System.IO.Directory.Exists(Directory))
                return;
            ///Récupération de la totalité des fichiers présents dans le répertoire observé.
            String[] Files = System.IO.Directory.GetFiles(Directory);

            ///Pour chacun des fichiers :
            foreach (String File in Files)
            {
                ///__ Récupération du nom du fichier.
                String sNomTable = File;
                sNomTable = System.IO.Path.GetFileName(File);

                ///__ On ignore le fichier UserData.ud s'il existe et qu'il est situé à la racine de la classe. En effet ce fichier contient
                ///normalement la description et la liste des UserData que l'on est en train de lister.
                if ((sNomTable == "UserData.ud") && (udtCurrentUserData == null))
                    continue;

                ///__ Si le ficher à l'esxtention html et que le nom existe sans l'extension html
                ///__ alors il s'agi d'une note, donc on passe.
                if (sNomTable.EndsWith(".html") && System.IO.File.Exists(File.Substring(0, File.Length - 5)))
                    continue;

                ///__ Chargement de la table et génération d'un \ref NormalTable (\ref FilterTable).
                NormalTable ntTmp = LoadTableUserData(File);
                ///__ Si le chargement a échoué, alors on traite le fichier suivant.
                if (ntTmp == null)
                    continue;

                ///Si le UserData n'existe pas (\ref udtCurrentUserData) alors on en crée un nouveau avec le nom du fichier courante.
                UserDataTable udtTmp = udtCurrentUserData;
                if (udtTmp == null)
                {
                    udtTmp = new UserDataTable(sNomTable);
                    if (!dsudtUserData.ContainsKey(sNomTable))
                        dsudtUserData.Add(sNomTable, udtTmp);
                }

                ///Ajout de la définition de la table au UserData.
                udtTmp.AddTable(ntTmp);

                ///Si il y a un fichier de même nom avec l'extention html, il s'agi d'une note.
                if (System.IO.File.Exists(File + ".html"))
                {
                    if (udtTmp == null)
                        continue;
                    ntTmp.Note = System.IO.File.ReadAllText(File + ".html");
                }
            }
        }

        /// <summary>
        /// Fonction qui permet d'ajouter une table à un UserData existant. Si le UserData n'existe pas, alors celui ci est créer.
        /// </summary>
        /// <param name="file">Adresse du fichier de la table à ajouter</param>
        /// <param name="UserDataName">Nom du UserData auquel on souhaite ajouter le noeud.</param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé ou non.</returns>
        internal bool AddUserData(String file, String UserDataName)
        {
            ///Si \ref dsudtUserData est NULL, alors on initialize la variable
            if (dsudtUserData == null)
                dsudtUserData = new Dictionary<string, UserDataTable>();

            ///On charge la table à la table grace à la fonction \ref LoadTableUserData()
            NormalTable ntTable = LoadTableUserData(file);
            ///Si la table est NULL, alors on Retourne FAUX.
            if (ntTable == null)
                return false;
            ///Si le UserData n'existe pas, alors on crée un nouveau UserData avec le nom passé en parametre.
            if (!dsudtUserData.ContainsKey(UserDataName))
                dsudtUserData.Add(UserDataName, new UserDataTable(UserDataName));

            ///On ajoute la table au UserData sélectionné.
            dsudtUserData[UserDataName].AddTable(ntTable);

            ///On revoie VRAI.
            return true;
        }

        // >> Task #12393 Pax2Sim - File conversion for Athens
        internal void removeUserData(String userDataSubdirectoryName, String userDataTableName)
        {
            if (dsudtUserData != null && dsudtUserData.ContainsKey(userDataSubdirectoryName))
            {
                UserDataTable userDataSubdirectory = dsudtUserData[userDataSubdirectoryName];
                if (userDataSubdirectory != null)
                {
                    userDataSubdirectory.RemoveTable(userDataTableName);
                }
            }
        }
        // << Task #12393 Pax2Sim - File conversion for Athens

        /// <summary>
        /// Fonction qui permet de charger une table destinée à être affichée en tant que UserData.
        /// </summary>
        /// <param name="file">Nom du fichier à charger.</param>
        /// <returns>Table chargée ou NULL.</returns>
        private static NormalTable LoadTableUserData(String file)
        {
            DataTable tmp = new DataTable(System.IO.Path.GetFileName(file));
            ///Chargement de la table à l'aide de la fonction \ref OverallTools.FonctionUtiles.lectureUserData()
            ///Si le chargement se passe mal, alors la fonction renvoie NULL. sinon elle renvoie le nouveau 
            ///\ref NormalTable chargé.
            if (!OverallTools.FonctionUtiles.lectureUserData(tmp, file))
                return null;
            NormalTable ntTable = new NormalTable(tmp, VisualisationMode.ModifiableMode);
            return ntTable;
        }

        /// <summary>
        /// Fonction permettant de déterminer si un UserData existe ou non.
        /// </summary>
        /// <param name="sName">Nom du UserData recherché</param>
        /// <returns>Booléen indiquant si ce UserData existe ou non.</returns>
        internal bool UserDataExists(String sName)
        {
            ///Pour chaque UserData présent dans la liste \ref dsudtUserData
            foreach (String sKey in dsudtUserData.Keys)
            {
                ///__ Si le UserData contient une table avec le nom passé en paramètre, alors la fonction renvoie VRAI
                if (dsudtUserData[sKey].GetTable(sName) != null)
                    return true;
            }
            ///La fonction renvoie FAUX.
            return false;
        }

        /// <summary>
        /// Fonction qui permet de sauvegarder les UserData dans le dossier passé en paramètre.
        /// </summary>
        /// <param name="sDirectory">Dossier d'enregistrement.</param>
        internal void SaveUserData(String sDirectory)
        {
            ///Pour chaque UserData présent dans la liste \ref dsudtUserData
            foreach (String sUserDataName in dsudtUserData.Keys)
            {
                ///Appel de \ref UserDataTable.SaveUserData
                dsudtUserData[sUserDataName].SaveUserData(sDirectory);
            }
        }
        /// <summary>
        /// Fonction qui permet de sauvegarder les UserDAta pour une simulation (avec paramétrage).
        /// </summary>
        /// <param name="sDirectory">Dossier d'enregistrement.</param>
        /// <param name="dssParameters">Paramétrage d'enregistrement.</param>
        internal void SaveUserDataForSimulation(String sDirectory, Dictionary<String, String> dssParameters)
        {
            ///Pour chaque UserData présent dans la liste \ref dsudtUserData
            foreach (String sUserDataName in dsudtUserData.Keys)
            {
                ///__ Si \ref dssParameters contient une clef pour le UserData, appel de la fonction \ref UserDataTable.SaveUserDataForSimulation
                if (dssParameters.ContainsKey(sUserDataName))
                    dsudtUserData[sUserDataName].SaveUserDataForSimulation(sDirectory, dssParameters[sUserDataName]);
            }
        }

        /// <summary>
        /// Fonction pour supprimer du système de données 
        /// </summary>
        /// <param name="sNomUserData">Nom du UserData à supprimer</param>
        public void DeleteUserData(String sNomUserData)
        {
            ///Si \ref dsudtUserData est null, arréter la fonction.
            if (dsudtUserData == null)
                return;
            ///Si \ref dsudtUserData ne contient pas \ref sNomUserData, arréter la fonction.
            if (!dsudtUserData.ContainsKey(sNomUserData))
                return;
            ///Suppression du UserData.
            UserDataTable udtUserData = dsudtUserData[sNomUserData];
            List<String> Names = udtUserData.TablesNames;
            foreach (String sTableName in Names)
            {
                DeleteUserData(sNomUserData, sTableName);
            }
        }

        /// <summary>
        /// Fonction pour supprimer une table d'un UserData.
        /// </summary>
        /// <param name="sNomUserData">Nom du UserData d'où supprimer</param>
        /// <param name="sTableName">Nom de la table à supprimer</param>
        public void DeleteUserData(String sNomUserData, String sTableName)
        {
            ///Si \ref dsudtUserData est null, arréter la fonction.
            if (dsudtUserData == null)
                return;
            ///Si \ref dsudtUserData ne contient pas \ref sNomUserData, arréter la fonction.
            if (!dsudtUserData.ContainsKey(sNomUserData))
                return;
            ///Suppression de la table dans le UserData.
            UserDataTable udtUserData = dsudtUserData[sNomUserData];
            udtUserData.RemoveTable(sTableName);
            RemoveUsedName(sTableName);
            if (udtUserData.Tables == 0)
                dsudtUserData.Remove(sNomUserData);
        }

        /// <summary>
        /// Fonction permettant de mettre à jour l'arboresence du TreeView avec les définitions des UserData.
        /// </summary>
        /// <param name="tnUserDataNode">Noeud où doivent être ajouté les UserData</param>
        /// <param name="cmsUserDataMenu">Menu contextuels à ajouter aux UserData</param>
        internal void UpdateUserDataTree(TreeNode tnUserDataNode, ContextMenuStrip cmsUserDataMenu)
        {
            ///Si \ref dsudtUserData est null, arréter la fonction.
            if (dsudtUserData == null)
                return;
            ///Parcours des UserData (\ref dsudtUserData).
            foreach (String sKey in dsudtUserData.Keys)
            {
                UserDataTable udtTable = dsudtUserData[sKey];
                ///__ Recherche du noeud représentant le UserData, Si celui ci est null, alors il est créé et ajouté à \ref tnUserDataNode.
                TreeNode tnLeaf = OverallTools.TreeViewFunctions.RechercherNom(sKey, tnUserDataNode);
                if (tnLeaf == null)
                {
                    tnLeaf = OverallTools.TreeViewFunctions.CreateDirectory(sKey, cmsUserDataMenu);
                    OverallTools.TreeViewFunctions.AddSortedNode(tnUserDataNode, tnLeaf);
                }
                ///__ Appel de la fonction \ref UserDataTable.UpdateUserDataTree pour mettre à jour les tables.
                udtTable.UpdateUserDataTree(tnLeaf, cmsUserDataMenu);
            }
        }

        /// <summary>
        /// Fonction qui permet de récupérer la liste des UserData avec la liste des tables contenues dans chaque UserData.
        /// </summary>
        /// <returns>Dictionnaire de données contenant chacun des UserData avec ses tables associées.</returns>
        public Dictionary<String, List<String>> GetUserData()
        {
            ///Renvoyer le dictionnaire de données contenant chacun des UserData avec ses tables associées.
            Dictionary<String, List<String>> dslsResults = new Dictionary<string, List<string>>();
            foreach (String sKey in dsudtUserData.Keys)
            {
                dslsResults.Add(sKey, dsudtUserData[sKey].TablesNames);
            }
            return dslsResults;
        }
        #endregion

        #region Pour le chargement et la sauvegarde de ce DataManagerInput

        /// <summary>
        /// Fonction qui permet de chargement des données à partir d'un noeud XML.
        /// </summary>
        /// <param name="xmlElement">Noeud XML représentant le \ref DataManager</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (afin de gérer les modifications dans les informations stockées).</param>
        /// <param name="sRootDirectory">Le dossier où se trouvent le fichier .pax (dossier racine du projet).</param>
        /// <param name="chForm">La fenêtre de chargement pour pouvoir faire progresser l'état du chargement.</param>
        /// <returns>Booléen indiquant si l'ouverture s'est bien déroulée ou non.</returns>
        internal override bool LoadInputData(XmlElement xmlElement, VersionManager vmVersion, String sRootDirectory, Prompt.SIM_LoadingForm chForm)
        {
            ///Si le noeud XML ne contient pas de noeud DataManager, alors renvoyer FAUx (Erreur dans le fichier)
            if (!OverallTools.FonctionUtiles.hasNamedChild(xmlElement, "DataManager"))
                return false;
            ///Appeler la fonction \ref DataManager.LoadInputData
            return base.LoadInputData(xmlElement["DataManager"], vmVersion, sRootDirectory, chForm);
        }

        /// <summary>
        /// Fonction pour sauver le \ref DataManagerInput courant sous forme XML.
        /// </summary>
        /// <param name="projet"></param>
        /// <param name="chForm">La fenêtre de sauvegarde pour pouvoir faire progresser l'état de la sauvegarde.</param>
        /// <param name="xnNode">Noeud auquel ajouter les informations de la classe.</param>
        internal override void Save(XmlDocument projet, XmlElement xnNode, string sRootDirectory, string sOldRootDirectory, string sSavingDirectory, Prompt.SIM_LoadingForm chForm)
        {
            ///Appel de la fonction \ref save qui génère un noeud XML .
            XmlElement arbreFiltres = Save(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory, chForm);
            ///Ajout du noeud XML généré pour ce \ref DataManagerInput au noeud XML passé en entrée \ref xnNode.
            xnNode.AppendChild(arbreFiltres);
        }

        /// <summary>
        /// Fonction qui s'occupe de générer un noeud et de sauvegarder la totalité des tables présentes dans le système de données.
        /// </summary>
        /// <param name="projet">Projet XML auquel tous les noeuds XML doivent être liés.</param>
        /// <param name="chForm">La fenêtre de sauvegarde pour pouvoir faire progresser l'état de la sauvegarde.</param>
        /// <returns></returns>
        internal override XmlElement Save(XmlDocument projet, string sRootDirectory, string sOldRootDirectory, string sSavingDirectory, Prompt.SIM_LoadingForm chForm)
        {
            ///Création d'un noeud XML "DataManagerInput" qui stockera l'ensemble des informations de ce \ref DataManagerInput
            XmlElement arbreFiltres = projet.CreateElement("DataManagerInput");
            ///Ajout de l'attribut "Name" avec le \ref sName 
            arbreFiltres.SetAttribute("Name", sName);
            ///Appel de la fonction de base \ref DataManager.Save pour sauvegarder l'ensemble des tables qui sont stockées dans la parties \ref DataManager.
            XmlElement xnBase = base.Save(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory, chForm);
            ///Ajout du noeud représentant le \ref DataManager au noeud courant.
            if (xnBase != null)
                arbreFiltres.AppendChild(xnBase);
            ///Si des allocations existe dans le \ref DataManagerInput, alors elles sont sauvegarder sous le noeud "Allocations".
            if (dsgatAllocations.Count > 0)
            {
                XmlElement Allocation = projet.CreateElement("Allocations");
                foreach (String sKey in dsgatAllocations.Keys)
                    Allocation.AppendChild(dsgatAllocations[sKey].Save(projet));
            }
            ///Appel de la fonction \ref SaveUserData pour sauvegarder les UserData.
            SaveUserData(sRootDirectory + "\\Data\\UserData\\");
            ///Renvoie du Noeud XML représentant ce \ref DataManagerInput
            return arbreFiltres;
        }
        #endregion

        #region Gestion des fonctions lièes à l'interfaçage avec GestionDonneesHUB2SIM.
        /// <summary>
        /// Propriété qui permet d'initialiser la fonction à utiliser pour récupérer la structure de l'aéroport (\ref GetStructureAirportFunction).
        /// </summary>
        internal GetStructureAirportDelegate setGetStructureAirportFunction
        {
            set
            {
                GetStructureAirportFunction = value;
            }
        }

        /// <summary>
        /// Propriété qui permet d'initialiser la fonction à utiliser pour récupérer l'information sur l'utilisation de l'AlphaNumeric (\ref GetUseAlphaNumericalFlightPlanFunction).
        /// </summary>
        internal GetUseAlphaNumericalFlightPlanDelegate setGetUseAlphaNumericalFlightPlanFunction
        {
            set
            {
                GetUseAlphaNumericalFlightPlanFunction = value;
            }
        }

        /// <summary>
        /// Propriété permettant de définir la fonction a appelée pour mettre à jour les tables suivant la structure de l'aéroport (\ref UpdateTableFromAirportStructure)
        /// </summary>
        internal UpdateTableFromAirportStructureDelegate setUpdateTableFromAirportStructure
        {
            set
            {
                UpdateTableFromAirportStructure = value;
            }
        }

        /// <summary>
        /// Propriété qui permet d'initialiser la fonction à utiliser pour récupérer le Périmètre actuel de l'application (\ref GetPerimeterFunction).
        /// </summary>
        internal GetPerimeterDelegate setGetPerimeterFunction
        {
            set
            {
                GetPerimeterFunction = value;
            }
        }
        #endregion

        #region Fonction Copy et Dispose
        /// <summary>
        /// Fonction qui permet la copie du système de données. La fonction copie l'intégralité des tables et des filtes.
        /// </summary>
        /// <returns>Nouvelle instance qui est une copie de l'objet à copier.</returns>
        internal override Object Copy()
        {
            ///Appel du constructeur de copie \ref DataManagerInput
            return new DataManagerInput(this);
        }
        internal override void Dispose()
        {
            //foreach (GenerateAllocationTool gat in dsgatAllocations)
            //    gat.Dispose();
            dsgatAllocations.Clear();
            foreach (UserDataTable udt in dsudtUserData.Values)
                udt.Delete();
            dsudtUserData.Clear();
            GetPerimeterFunction = null;
            GetStructureAirportFunction = null;
            GetUseAlphaNumericalFlightPlanFunction = null;
            base.Dispose();
        }
        #endregion

        #region Fonctions de vérification des tables.
        /// <summary>
        /// Fonction qui permet la vérification de la table par rapport aux tables contenues dans ce système de données.
        /// cette fonction renverra le liste des erreurs trouvée lors de la vérification (ou NULL si aucune erreurs).
        /// </summary>
        /// <param name="tcParams">Paramétrage de l'outil de vérification de la table.</param>
        /// <param name="sCurrentAnalysedTable">Table courante devant être analysée.</param>
        /// <param name="sortedColumn">Nom de la colonne à utiliser pour trier la table (peut être NULL)</param>
        /// <param name="Order">Ordre de classement pour le tri</param>
        /// <returns>Liste des erreurs rencontrées lors de la vérification de la table.</returns>
        private ArrayList CheckTable(OverallTools.TableCheck tcParams, NormalTable sCurrentAnalysedTable, String sortedColumn, SortOrder Order)
        {
            ///Si les paramétrages \ref tcParams ou \ref sCurrentAnalysedTable sont null, alors la fonction renvoie NULL.
            if (tcParams == null)
                return null;
            if (sCurrentAnalysedTable == null)
                return null;
            ///Ajout de toutes les tables nécessaires à la vérification de la table dans une liste.
            Dictionary<String, DataTable> htTables = new Dictionary<String, DataTable>();
            String sParentName = sCurrentAnalysedTable.Name;
            if (sCurrentAnalysedTable.isFilter)
                sParentName = ((IFilterTable)sCurrentAnalysedTable).Root.Name;

            htTables.Add(sParentName, sCurrentAnalysedTable.Table);
            NormalTable ntTmp;
            if (tcParams.TableLinkedTable != null)
            {
                foreach (String TableName in tcParams.TableLinkedTable)
                {
                    ntTmp = GetTable(TableName);
                    if (ntTmp == null)
                        return null;
                    htTables.Add(TableName, ntTmp.Table);
                }
            }
            if (tcParams.OneOfTable != null)
            {
                ntTmp = GetTable(tcParams.OneOfTable);
                if (ntTmp == null)
                    return null;
                htTables.Add(GlobalNames.OneofSpecificationTableName, ntTmp.Table);
            }
            //<< Task #7405 - new Desk and extra information for Pax            
            if (tcParams.UserAttributeTableName != null)
            {
                ntTmp = GetTable(tcParams.UserAttributeTableName);
                if (ntTmp == null)
                    return null;
                htTables.Add(GlobalNames.sUserAttributesTableName, ntTmp.Table);
            }
            //>> Task #7405 - new Desk and extra information for Pax
            ///Récupération du périmètre de l'application (\ref GetPerimeterFunction).
            PAX2SIM.EnumPerimetre epPerimeter = PAX2SIM.EnumPerimetre.PAX;
            if (GetPerimeterFunction != null)
                epPerimeter = GetPerimeterFunction();
            ///Récupération de la structure de l'aéroport (\ref GetStructureAirportFunction).
            XmlNode structureAirport = null;
            if (GetStructureAirportFunction != null)
                structureAirport = GetStructureAirportFunction();
            ///Récupération du paramétrage des plans de vols concernant les affichage AlphaNumerique (\ref GetUseAlphaNumericalFlightPlanFunction).
            bool bUseAlphNumericForFlightInfo = false;
            if (GetUseAlphaNumericalFlightPlanFunction != null)
                bUseAlphNumericForFlightInfo = GetUseAlphaNumericalFlightPlanFunction();
            ///Appel de la fonction static \ref CheckTable avec le paramétrage adéquat.
            return CheckTable(tcParams, sCurrentAnalysedTable, htTables, structureAirport, bUseAlphNumericForFlightInfo, sortedColumn, Order, (epPerimeter == PAX2SIM.EnumPerimetre.PAX), (epPerimeter == PAX2SIM.EnumPerimetre.BHS));
        }

        /// <summary>
        /// Fonction qui permet la vérification de l'intégralité des tables rapport aux tables contenues dans ce système de données.
        /// cette fonction renverra le liste des erreurs trouvée lors de la vérification (ou NULL si aucune erreurs).
        /// </summary>
        /// <param name="sortedColumn">Nom de la colonne à utiliser pour trier la table (peut être NULL)</param>
        /// <param name="Order">Ordre de classement pour le tri</param>
        /// <returns>Liste des erreurs rencontrées lors de la vérification de la table.</returns>
        internal ArrayList CheckTables(String sortedColumn, SortOrder Order)
        {
            ///Récupération du périmètre de l'application (\ref GetPerimeterFunction).
            PAX2SIM.EnumPerimetre epPerimeter = PAX2SIM.EnumPerimetre.PAX;
            if (GetPerimeterFunction != null)
                epPerimeter = GetPerimeterFunction();
            ///Appel de la fonction \ref CheckTables avec comme premier parametre le résultat de \ref getNames().
            return CheckTables(getNames(),
                null,
                SortOrder.None,
                (epPerimeter == PAX2SIM.EnumPerimetre.PAX),
                (epPerimeter == PAX2SIM.EnumPerimetre.BHS));
        }

        /// <summary>
        /// Fonction qui permet la vérification de l'intégralité des tables contenues dans la liste \ref lsTablesToCheck.
        /// cette fonction renverra le liste des erreurs trouvée lors de la vérification (ou NULL si aucune erreurs).
        /// </summary>
        /// <param name="lsTablesToCheck">Liste des tables qui doivent être vérifiée pour une simulaton particulière.</param>
        /// <param name="sortedColumn">Nom de la colonne à utiliser pour trier la table (peut être NULL)</param>
        /// <param name="Order">Ordre de classement pour le tri</param>
        /// <param name="bCheckPaxSimulation">Booléen indiquant s'il s'agit d'une simulation Passager.</param>
        /// <param name="bCheckBagSimulation">Booléen indiquant s'il s'agit d'une simulation Bagage.</param>
        /// <returns>Liste des erreurs rencontrées lors de la vérification de la table.</returns>
        internal ArrayList CheckTables(List<String> lsTablesToCheck, String sortedColumn, SortOrder Order, bool bCheckPaxSimulation, bool bCheckBagSimulation)
        {
            ArrayList errorList = new ArrayList();
            ///Ajout de chacune des tables de la liste \ref lsTablesToCheck dans un dictionnaire avec comme clef le nom de la table racine.
            Dictionary<String, DataTable> dsdtTables = new Dictionary<string, DataTable>();
            foreach (String sName in lsTablesToCheck)
            {
                NormalTable ntTable = GetTable(sName);
                if (ntTable == null)
                    continue;
                String sParent = sName;
                if (ntTable.isFilter)
                    sParent = ((IFilterTable)ntTable).Root.Name;
                dsdtTables.Add(sParent, ntTable.Table);
            }


            ///Récupération de la structure de l'aéroport (\ref GetStructureAirportFunction).
            XmlNode structureAirport = null;
            if (GetStructureAirportFunction != null)
                structureAirport = GetStructureAirportFunction();
            ///Récupération du paramétrage des plans de vols concernant les affichage AlphaNumerique (\ref GetUseAlphaNumericalFlightPlanFunction).
            bool bUseAlphNumericForFlightInfo = false;
            if (GetUseAlphaNumericalFlightPlanFunction != null)
                bUseAlphNumericForFlightInfo = GetUseAlphaNumericalFlightPlanFunction();

            ///Pour chaque table de la liste \ref lsTablesToCheck
            foreach (String sName in lsTablesToCheck)
            {
                NormalTable ntTable = GetTable(sName);
                ///__ Récupération de la table racine.
                NormalTable ntParent = ntTable;
                if (ntTable.isFilter)
                    ntParent = ((IFilterTable)ntTable).Root;
                ///__ Si la table racine ne possède aucune défintion pour \ref NormalTable.TableParameters.tcChecks, alors passé à la table suivante de la liste.
                if (((ntParent.TableParameters) == null) || (ntParent.TableParameters.tcChecks == null))
                    continue;
                ///__ Vérification de la table à l'aide de la fonction statique \ref CheckTable appliqué sur la table passée en paramètres.
                /*                //<< Task #7405 - new Desk and extra information for Pax
                                if (sName == GlobalNames.Alloc_SecurityCheckTableName 
                                    || sName == GlobalNames.Alloc_TransferDeskTableName 
                                    || sName == GlobalNames.Alloc_PassportCheckTableName)
                                {                                
                                    if (ntParent.TableParameters.tcChecks.UserAttributeTableName != null)
                                    {
                                        NormalTable ntTmp = GetTable(ntParent.TableParameters.tcChecks.UserAttributeTableName);
                                        if (ntTmp == null)
                                            return null;
                                        if (!dsdtTables.ContainsKey(GlobalNames.sUserAttributesTableName))
                                            dsdtTables.Add(GlobalNames.sUserAttributesTableName, ntTmp.Table);
                                    }                    
                                }
                                //>> Task #7405 - new Desk and extra information for Pax
                 */
                ArrayList tmp = CheckTable(ntParent.TableParameters.tcChecks,
                    ntTable,
                    dsdtTables,
                    structureAirport,
                    bUseAlphNumericForFlightInfo,
                    sortedColumn,
                    Order,
                    bCheckPaxSimulation,
                    bCheckBagSimulation);

                if (tmp != null)
                    errorList.AddRange(tmp);
            }
            ///Si des erreurs ont été rencontrées lors de la vérification, alors celles ci sont renvoyées, sinon la fonction renvoie NULL.
            if (errorList.Count > 0)
                return errorList;
            return null;
        }

        /// <summary>
        /// Fonction qui permet la vérification de l'intégralité de la table \ref sCurrentAnalysedTable
        /// cette fonction renverra le liste des erreurs trouvée lors de la vérification (ou NULL si aucune erreurs).
        /// </summary>
        /// <param name="tcParams">Paramétrage de la table, les différentes informations et format que devra suivre la table.</param>
        /// <param name="sCurrentAnalysedTable">Table à analyser.</param>
        /// <param name="htTables">Tables qui sont associées à la table qui va être analysée.</param>
        /// <param name="xnStructureAirport">Structure de l'aéroport.</param>
        /// <param name="bUseAlphaNumerical">Booléen indiquant si les plans de vol ont un format AlphaNumérique ou non.</param>
        /// <param name="sortedColumn">Nom de la colonne à utiliser pour trier la table (peut être NULL)</param>
        /// <param name="Order">Ordre de classement pour le tri</param>
        /// <param name="bCheckPaxSimulation">Booléen indiquant s'il s'agit d'une simulation Passager.</param>
        /// <param name="bCheckBagSimulation">Booléen indiquant s'il s'agit d'une simulation Bagage.</param>
        /// <returns>Liste des erreurs rencontrées lors de la vérification de la table.</returns>
        internal static ArrayList CheckTable(OverallTools.TableCheck tcParams,
            NormalTable sCurrentAnalysedTable,
            Dictionary<String, DataTable> htTables,
            XmlNode xnStructureAirport,
            bool bUseAlphaNumerical,
            String sortedColumn,
            SortOrder Order,
            bool bCheckPaxSimulation,
            bool bCheckBagSimulation)
        {
            ArrayList errorList = new ArrayList();
            ArrayList warningList = new ArrayList();
            String SsortedColumn = sortedColumn;
            if (Order == SortOrder.Descending)
                SsortedColumn = sortedColumn + " DESC";
            if (Order == SortOrder.None)
                SsortedColumn = null;
            ConditionnalFormatErrors cfeErrors = new ConditionnalFormatErrors();

            if (!htTables.ContainsKey(sCurrentAnalysedTable.Name))
                return null;
            ///Vérification de la validité de la table pour le cas où l'utilisateur est en mode évaluation. \ref GestionDonneesHUB2SIM.IsValidShowUp()
            DataTable dtTable_ = (DataTable)htTables[sCurrentAnalysedTable.Name];
            if (!GestionDonneesHUB2SIM.IsValidShowUp(dtTable_))
            {
                ///Si la table n'est pas valide en mode évaluation, alors la vérification s'arrete et un message d'erreur est renvoyé à l'utilisateur.
                errorList.Add("The table " +
                    DataManagement.DataManagerInput.ConvertToFullName(dtTable_.TableName) + " is not valid. The maximum time can't be greater than " + GestionDonneesHUB2SIM.CI_ShowUpMax.ToString());
                if ((dtTable_.Columns.Count > 1) && (dtTable_.Rows.Count > 0))
                    cfeErrors.setCondition(1, dtTable_.Rows.Count - 1, errorList[errorList.Count - 1].ToString());
            }
            else
            {
                ///Sinon lancement de la vérification avec \ref OverallTools.TableCheck.CheckTable()
                if (tcParams.CheckTable(htTables, SsortedColumn, xnStructureAirport, errorList, warningList, cfeErrors, bUseAlphaNumerical, bCheckPaxSimulation, bCheckBagSimulation))
                    errorList = null;
            }
            ///Si la table possède un mode de visualisation (\ref NormalTable.Mode différent de null), alors les erreurs retournées sont ajoutées à celui ci.
            if (sCurrentAnalysedTable.Mode != null)
            {
                if ((cfeErrors.Errors != 0) || (cfeErrors.Warnings != 0))
                    sCurrentAnalysedTable.Mode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[] { cfeErrors };
                else
                    sCurrentAnalysedTable.Mode.ConditionnalFormatClass = null;
            }
            /// Renvoie des erreurs remontées par la vérification.
            return errorList;
        }

        /// <summary>
        /// Fonction qui compare les tables d'exceptions d'une table avec elle même et colore les differences
        /// </summary>
        /// 
        internal void CheckExDiff(String sTableName, String sExceptionType)
        {
            NormalTable ntExTable;
            ExceptionTable etRefTable;
            String sRefElement = null;
            String sBaseTable;
            bool bColumnException;

            // recuperation des tables
            etRefTable = this.GetTableException(sTableName);
            if (etRefTable == null) return;
            ntExTable = etRefTable.GetExceptionTable(sExceptionType);
            if (ntExTable == null)
                return;
            if (sExceptionType.EndsWith(GlobalNames.Airline)) // the exception check for the Airline exception doesn't make any sense  // >> Bug #14377 Tables Data check improvement
                return;

            bColumnException = etRefTable.ExceptionFormat == ExceptionTable.ExceptionTableFormat.Column;

            // Recherche du nom de la colonne dans la tables FP associé à l'exception dans la tables
            if (sExceptionType.StartsWith(GlobalNames.FirstAndBusiness))
            {
                sRefElement = null; // pas besoin dans ce cas
            }
            else
            {
                if (sExceptionType.EndsWith(GlobalNames.Flight))
                {
                    sRefElement = GlobalNames.sFPD_A_Column_ID;
                }
                else if (sExceptionType.EndsWith(GlobalNames.Airline))
                {
                    sRefElement = GlobalNames.sFPD_A_Column_AirlineCode;
                }
                else if (sExceptionType.EndsWith(GlobalNames.FlightCategory))
                {
                    sRefElement = GlobalNames.sFPD_A_Column_FlightCategory;
                }
            }

            // recuperation du nom de la table de base ()
            if (etRefTable.isFilter)
                sBaseTable = (etRefTable as IFilterTable).Root.Name;
            else
                sBaseTable = etRefTable.Name;

            // appel de la fonciton de verification
            ChechExDiff(ntExTable.Table, etRefTable.Table, sRefElement, sBaseTable, sExceptionType, ntExTable.Mode, bColumnException);
        }

        /// <summary>
        /// Fonction qui compare la table d'exception avec la table de reference passé en parametre et colore les differences
        /// </summary>
        /// <param name="dtExTable"></param>
        /// <param name="dtRefTable"></param>
        /// <param name="sRefElement">Nom dans les tables FP correspondant aux elements de la clef primaire de la table de reference</param>
        /// <param name="sExceptionType"></param>
        /// <param name="vmMode"></param>
        /// <param name="bColumn"></param>
        internal void ChechExDiff(
            DataTable dtExTable,
            DataTable dtRefTable,
            String sRefElement,
            String sBaseTable, // root table name
            String sExceptionType,
            VisualisationMode vmMode,
            bool bColumn)
        {
            // initialisation des variables
            ConditionnalFormatErrors cfe = new ConditionnalFormatErrors();

            // << Task #7949 Capacity Analysis - IST tables modification
            if (dtRefTable.Rows.Count != dtExTable.Rows.Count)
                return;
            // >> Task #7949 Capacity Analysis - IST tables modification

            if (bColumn)
            {
                for (int i = 1; i < dtExTable.Columns.Count; i++)
                {

                    // pour les first and business on peut comparer directement avec la table parente
                    if (sExceptionType.Equals(GlobalNames.FirstAndBusiness))
                    {
                        String ColumnName;
                        ColumnName = dtExTable.Columns[i].ColumnName;
                        int RefColumn = dtRefTable.Columns.IndexOf(ColumnName);
                        if (RefColumn == -1)
                            continue;

                        // on parcourt la colonne pour chercher les differences
                        for (int j = 0; j < dtRefTable.Rows.Count; j++)
                            if (dtRefTable.Rows[j][RefColumn].ToString() != dtExTable.Rows[j][i].ToString())
                                cfe.setExceptionDiffCondition(i, j, "Original value is " + dtRefTable.Rows[j][RefColumn]);
                        //cfe.setCondition(i, j, null, "Original value is " + dtRefTable.Rows[j][RefColumn]);
                    }

                    // pour les Flight on peux se baser sur les table FPA et FPD (une seul valeur possible)
                    else if (sExceptionType.EndsWith(GlobalNames.Flight))
                    {

                        //object valeur; // nom de l'entrée ou il faut recuperer les valeurs par defaut dans la table parente

                        // recherche du nom de la colonne ou chercher et pour retourner
                        //String outFPcolumn = GlobalNames.sFPD_A_Column_FlightCategory; // recherche du nom de la colonne d'où retourner
                        //String sSearch = dtTable.Columns[i].ColumnName; // element a chercher
                        String flightID = dtExTable.Columns[i].ColumnName; // element a chercher
                        DataTable dtFP;
                        if (flightID.StartsWith("A"))
                        {
                            NormalTable ntFPA = GetTable(GlobalNames.FPATableName);
                            if (ntFPA == null) return;
                            dtFP = ntFPA.Table;
                        }
                        else
                        {
                            NormalTable ntFPD = GetTable(GlobalNames.FPDTableName);
                            if (ntFPD == null) return;
                            dtFP = ntFPD.Table;
                        }
                        int indexLigne = OverallTools.DataFunctions.indexLigne(dtFP, GlobalNames.sFPD_A_Column_ID, flightID.Substring(2));
                        if (indexLigne == -1) continue;
                        object found = dtFP.Rows[indexLigne][GlobalNames.sFPD_A_Column_FlightCategory];
                        if (found == null) continue;

                        int RefColumn = dtRefTable.Columns.IndexOf(found.ToString());
                        if (RefColumn == -1)
                            continue;

                        // on parcourt la colonne pour chercher les differences
                        for (int j = 0; j < dtRefTable.Rows.Count; j++)
                            if (dtRefTable.Rows[j][RefColumn].ToString() != dtExTable.Rows[j][i].ToString())
                                cfe.setExceptionDiffCondition(i, j, "Reference value for " + found.ToString() + " is " + dtRefTable.Rows[j][RefColumn].ToString());
                        //cfe.setCondition(i, j, null, "Reference value for " + found.ToString() + " is " + dtRefTable.Rows[j][RefColumn].ToString());
                    }

                    // pour tout les autres cas on cherche si la valeur exist dans au moins un colonne
                    else
                    {
                        int iPk = dtRefTable.PrimaryKey.Length;
                        if (iPk == 0)
                            iPk = 1;

                        // on parcourt la colonne
                        for (int j = 0; j < dtRefTable.Rows.Count; j++)
                        {
                            // et pour chaque valeur on cherche si elle est presente dans la table parent
                            String valeur = dtExTable.Rows[j][i].ToString();
                            bool found = false;
                            for (int k = iPk - 1; k < dtRefTable.Columns.Count; k++)
                            {
                                String sRef = dtRefTable.Rows[j][k].ToString();
                                if (sRef == valeur)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                                cfe.setExceptionDiffCondition(i, j, "the value differs for all other case");
                            //cfe.setCondition(i, j, null, "the value differs for all other case");
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < dtExTable.Rows.Count; i++)
                {
                    // pour les exception en ligne (Pax pocess time et aircraft type) on peut ce baser directement sur la table parente
                    if (DataManagerInput.GetExceptionFormats(sBaseTable) == ExceptionTable.ExceptionTableFormat.Line)
                    {
                        int delta = 0; // decalage pour la colonne item
                        if (!sExceptionType.Equals(GlobalNames.FirstAndBusiness))
                            delta = 1;

                        String LineName = dtExTable.Rows[i][delta].ToString();
                        int Refline = OverallTools.DataFunctions.indexLigne(dtRefTable, 0, LineName);
                        if (Refline == -1)
                            continue;

                        // on parcourt la ligne pour chercher les differences
                        for (int j = 0; j < dtRefTable.Columns.Count; j++)
                            if (dtRefTable.Rows[Refline][j].ToString() != dtExTable.Rows[i][j + delta].ToString())
                                cfe.setExceptionDiffCondition(j + delta, i, "Original value is " + dtRefTable.Rows[Refline][j].ToString());
                        //cfe.setCondition(j + delta, i, null, "Original value is " + dtRefTable.Rows[Refline][j].ToString());

                    }
                }
            }
            if (cfe.exceptionDiffWarning > 0)   // >> Bug #14377 Tables Data check improvement
            {
                if (vmMode.ConditionnalFormatClass == null)
                    vmMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[1];
                vmMode.ConditionnalFormatClass[0] = cfe;
            }
        }

        #endregion

        #region Fonctions pour permettre de mettre à jour la structure de la table en fonction de la structure de l'aéroport.
        internal void UpdateTablesFromAirportStructure()
        {
            if (dsntTables == null)
                return;
            if (UpdateTableFromAirportStructure == null)
                return;
            foreach (String sTable in dsntTables.Keys)
            {
                NormalTable ntTable = GetTable(sTable);
                if (ntTable == null)
                    continue;
                if (ntTable.TableParameters == null)
                    continue;
                if (!ntTable.TableParameters.bShemaAirportTable)
                    continue;
                UpdateTableFromAirportStructure(sTable, ntTable.TableParameters);
                if (ntTable.Name == SIMCORE_TOOL.GlobalNames.Transfer_TerminalDitributionTableName &&
                    ntTable.Table != null)
                    OverallTools.DataFunctions.InitTranfertDistri(ntTable.Table);

            }
        }
        #endregion

    }
    #endregion

    #region public class DataManagerScenario : DataManager
    /// <summary>
    /// Classe qui contient toutes les informations concernant les données d'entrée d'un projet. Elle regroupe notamment
    /// les tables et les filtres.
    /// </summary>
    public class DataManagerScenario : DataManager
    {
        #region private class ArboNode
        /// <summary>
        /// Classe permettant de représenté un noeud de l'arbre visualiser dans le TreeView. Cette classe permet notamment de redéfinir rapidement
        /// cette arborescence en utilisant les fonctions prévues à cet effet.
        /// </summary>
        internal class ArboNode
        {
            /// <summary>
            /// Liste des noeuds fils du noeud courant.
            /// </summary>
            internal List<ArboNode> lanChilds;

            /// <summary>
            /// Tag permettant d'identifier le noeud et les informations relatives à ce noeud.
            /// </summary>
            internal TreeViewTag tvtType;

            /// <summary>
            /// Mode de visualisation de la table, si null, alors il n'y a pas de particularité, sinon
            /// les particularités sont définies dans cet élément.
            /// </summary>
            internal VisualisationMode vmMode;

            /// <summary>
            /// Constructeur d'un nouveau noeud \ref ArboNode.
            /// </summary>
            /// <param name="NodeType">Le tag qui doit être associé au nouveau noeud</param>
            internal ArboNode(TreeViewTag NodeType) : this(NodeType, null)
            {
            }

            /// <summary>
            /// Constructeur d'un nouveau noeud \ref ArboNode.
            /// </summary>
            /// <param name="NodeType">Le tag qui doit être associé au nouveau noeud.</param>
            /// <param name="Mode">Le mode de visualisation propre à la table.</param>
            internal ArboNode(TreeViewTag NodeType, VisualisationMode Mode)
            {
                ///Initialisation des variables de la classe.
                tvtType = NodeType;
                lanChilds = new List<ArboNode>();
                vmMode = Mode;
            }

            /// <summary>
            /// Fonction permet d'ajouter dans la liste des enfants du noeud courant un nouvel enfant.
            /// </summary>
            /// <param name="Child">L'enfant à ajouter.</param>
            internal void AddChild(ArboNode Child)
            {
                ///Ajout dans \ref lanChilds de l'enfant passé en paramètre.
                lanChilds.Add(Child);
            }

            /// <summary>
            /// Fonction pour pouvoir récupérer le nom du noeud courant (Nom qui représente le noeud plutôt qu'il ne l'identifie : ChartNode / TableNode...).
            /// </summary>
            /// <returns>Nom du noeud courant.</returns>
            private String getName()
            {
                ///Suivant le type stocké dans le tag( \ref tvtType), renvoie du type de noeud.
                if (tvtType.isChartNode)
                    return "ChartNode";
                if (tvtType.isTableNode)
                    return "TableNode";
                if (tvtType.isResultNode)
                    return "ResultNode";
                if (tvtType.isDirectoryNode)
                    return "DirectoryNode";
                if (tvtType.isFilterNode)
                    return "FilterNode";
                return "";
            }

            /// <summary>
            /// Fonction qui permet d'exporter l'arborescence sous forme XML pour pouvoir le sauvegarder.
            /// </summary>
            /// <param name="xdDocument"></param>
            /// <returns></returns>
            internal XmlElement ExportArbo(XmlDocument xdDocument)
            {
                ///Récupération du type du noeud courant. Si celui ci est null, alors la fonction renvoie NULL.
                String sNode = this.getName();
                if ((sNode == null) || (sNode == ""))
                    return null;
                ///Création du noeud XML représentant la classe avec comme nom le type du noeud courant.
                XmlElement xnResult = xdDocument.CreateElement(sNode);
                ///Ajout comme Attribut "Name" du nom du noeud courant.
                xnResult.SetAttribute("Name", tvtType.Name);
                ///Ajout comme Attribut "ScenarioName" du nom du Scénario courant.
                if ((tvtType.ScenarioName != "") && (tvtType.ScenarioName != null))
                    xnResult.SetAttribute("ScenarioName", tvtType.ScenarioName);

                ///Si le noeud est un filtre, alors les informations Bloqued et Copy sont également enregistrées.
                if (sNode == "FilterNode")
                {
                    if (tvtType.isCopyTable)
                        xnResult.SetAttribute("Copy", "True");
                    if (tvtType.isBloqued)
                        xnResult.SetAttribute("Bloqued", "True");
                }
                ///Parcours de l'ensemble des enfants du noeud courant.
                foreach (ArboNode anTmp in lanChilds)
                {
                    ///__ Utilisation de \ref ExportArbo pour exporter le noeud enfant et généré un noeud XML ajouté au noeud qui sera renvoyé.
                    XmlElement xnTmp = anTmp.ExportArbo(xdDocument);
                    if (xnTmp == null)
                        return null;
                    xnResult.AppendChild(xnTmp);
                }
                return xnResult;
            }


            internal static VisualisationMode getVisualisationMode(String sTableName, String sTableType)
            {
                VisualisationMode vmMode = null;
                switch (sTableType)
                {
                    case "TableNode":
                        if (sTableName.EndsWith("_Occupation"))
                        {
                        }
                        else if (sTableName.EndsWith("_IST"))
                        {
                        }
                        else if (sTableName.EndsWith("_Remaining"))
                        {
                        }
                        else if (sTableName.EndsWith("_Time"))
                        {
                        }
                        else if (sTableName.EndsWith("_Utilization"))
                        {
                        }
                        else if (sTableName.EndsWith(GlobalNames.SUMMARY_TABLE_NAME_SUFFIX))    // >> Task #10484 Pax2Sim - Pax analysis - Summary with distribution levels percent
                        {
                            ConditionnalFormatLine summaryFormat = new ConditionnalFormatLine();
                            for (int j = 0; j < 30; j++)    // >> Task #10484 Pax2Sim - Pax analysis - Summary with distribution levels percent
                            {
                                summaryFormat.setCondition(j * 2, Color.FromArgb(200, 255, 255));
                                summaryFormat.setCondition(j * 2 + 1, Color.FromArgb(255, 255, 180));
                            }

                            vmMode = new VisualisationMode(false, false, false, new int[2] { 0, 1 }, new int[1] { 0 });
                            vmMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[1];
                            vmMode.ConditionnalFormatClass[0] = (VisualisationMode.ConditionnalFormat)summaryFormat;
                        }
                        else if (sTableName.EndsWith(GlobalNames.BHS_STATS_TABLE_SUFFIX)) // >> Task #10985 Pax2Sim - BHS dynamic analysis - adapt statistics tables for the Dashboard
                        {
                            ConditionnalFormatLine bhsStatsFormat = new ConditionnalFormatLine();
                            for (int j = 0; j < 150; j++)    // >> Task #10484 Pax2Sim - Pax analysis - Summary with distribution levels percent
                            {
                                bhsStatsFormat.setCondition(j * 2, Color.FromArgb(200, 255, 255));
                                bhsStatsFormat.setCondition(j * 2 + 1, Color.FromArgb(255, 255, 180));
                            }
                            vmMode = new VisualisationMode(false, false, false, new int[2] { 0, 1 }, new int[1] { 0 });
                            vmMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[1];
                            vmMode.ConditionnalFormatClass[0] = (VisualisationMode.ConditionnalFormat)bhsStatsFormat;
                        }
                        break;
                    case "ResultNode":
                        ConditionnalFormatLine Format = new ConditionnalFormatLine();

                        /*  // >> Task #10615 Pax2Sim - Pax analysis - Summary - small changes
                        for (int j = 0; j < 30; j++)    // >> Task #10484 Pax2Sim - Pax analysis - Summary with distribution levels percent
                        {
                            Format.setCondition(j * 2, Color.FromArgb(200, 255, 255));
                            Format.setCondition(j * 2 + 1, Color.FromArgb(255, 255, 180));
                        }
                        */
                        for (int i = 0; i < 30; i++)
                        {
                            if (i < 6)
                            {
                                Format.setCondition(i, Color.FromArgb(255, 255, 180));
                            }
                            else if (i >= 6 && i < 14)
                            {
                                Format.setCondition(i, Color.FromArgb(200, 255, 255));
                            }
                            else if (i >= 14 && i < 21)
                            {
                                Format.setCondition(i, Color.FromArgb(255, 255, 180));
                            }
                            else
                            {
                                Format.setCondition(i, Color.FromArgb(200, 255, 255));
                            }
                        }
                        // << Task #10615 Pax2Sim - Pax analysis - Summary - small changes

                        vmMode = new VisualisationMode(false, false, false, new int[2] { 0, 1 }, new int[1] { 0 });
                        vmMode.ConditionnalFormatClass = new VisualisationMode.ConditionnalFormat[1];
                        vmMode.ConditionnalFormatClass[0] = (VisualisationMode.ConditionnalFormat)Format;
                        break;
                    case "FilterNode":
                        break;
                    default:
                        return null;
                }
                return vmMode;
            }

            /// <summary>
            /// Fonction qui permet de charger l'arborescence depuis un noeud XML.
            /// </summary>
            /// <param name="xnElement">Noeud XML représentant la racine de l'arborescence à charger.</param>
            /// <returns></returns>
            internal static ArboNode LoadArbo(XmlElement xnElement)
            {
                if (!OverallTools.FonctionUtiles.hasNamedAttribute(xnElement, "Name"))
                    return null;
                String sScenarioName = "";
                if (OverallTools.FonctionUtiles.hasNamedAttribute(xnElement, "ScenarioName"))
                    sScenarioName = xnElement.Attributes["ScenarioName"].Value;
                String sName = xnElement.Attributes["Name"].Value;
                TreeViewTag tvtTmp = null;
                VisualisationMode vmMode = null;
                ///Création d'un \ref TreeViewTag représentant le noeud sauvegardé.
                switch (xnElement.Name)
                {
                    case "TableNode":
                        tvtTmp = TreeViewTag.getTableNode(sScenarioName, sName);
                        break;
                    case "ResultNode":
                        tvtTmp = TreeViewTag.getResultNode(sScenarioName, sName);
                        vmMode = getVisualisationMode(sName, "ResultNode");
                        break;
                    case "ChartNode":
                        tvtTmp = TreeViewTag.getChartNode(sName);
                        break;
                    case "DirectoryNode":
                        tvtTmp = TreeViewTag.getDirectoryNode(sName);
                        break;
                    case "FilterNode":
                        bool bCopy = OverallTools.FonctionUtiles.hasNamedAttribute(xnElement, "Copy");
                        bool bBloqued = OverallTools.FonctionUtiles.hasNamedAttribute(xnElement, "Bloqued");
                        tvtTmp = TreeViewTag.getFilterNode(sScenarioName, sName, bBloqued, bCopy);
                        break;
                    default:
                        return null;
                }
                ///Création du noeud \ref ArboNode
                ArboNode anResults = new ArboNode(tvtTmp, vmMode);
                ///Pour chaque noeud enfant du noeud XML.
                foreach (XmlElement xeChild in xnElement.ChildNodes)
                {
                    ///__ Appeler la fonction \ref LoadArbo pour charger le noeud.
                    ArboNode anChild = LoadArbo(xeChild);
                    if (anChild == null)
                        return null;
                    anResults.AddChild(anChild);
                }
                return anResults;
            }

            /// <summary>
            /// Fonction qui permet de charger l'arborescence depuis un Arbre TreeNode.
            /// </summary>
            /// <param name="tnElement">Noeud du treeview représentant l'arborescence à copier.</param>
            /// <returns></returns>
            internal static ArboNode LoadArbo(TreeNode tnElement)
            {
                if (tnElement == null)
                    return null;
                if (tnElement.Tag == null)
                    return null;
                if (tnElement.Tag.GetType() != typeof(TreeViewTag))
                    return null;
                ///Récupération du Tag du noeud courant.
                TreeViewTag tvtTmp = (TreeViewTag)tnElement.Tag;

                ///Création du noeud \ref ArboNode
                ArboNode anResults = new ArboNode(tvtTmp);
                ///Pour chaque noeud enfant du noeud du treeview.
                foreach (TreeNode tnChild in tnElement.Nodes)
                {
                    ///__ Appeler la fonction \ref LoadArbo pour charger le noeud.
                    ArboNode anChild = LoadArbo(tnChild);
                    if (anChild == null)
                        return null;
                    anResults.AddChild(anChild);
                }
                return anResults;
            }

            internal void Dispose()
            {
                foreach (ArboNode an in lanChilds)
                    an.Dispose();
                lanChilds.Clear();
                lanChilds = null;

                tvtType.Dispose();
            }
        }
        #endregion

        #region Les différentes variables de la classe.

        /// <summary>
        /// Cette variable représente le noeud racine du scénario courant. Cela permet de générer l'arborescence facilement lorsqu'il 
        /// est nécessaire de regénérer le noeud du TreeView.
        /// </summary>
        internal ArboNode anRootNode;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Consctructeur permettant l'initialisation de  la classe représentant un Scénario.
        /// </summary>
        /// <param name="Name">Nom du scénario</param>
        protected DataManagerScenario(String Name)
            : base(Name)
        {
        }

        /// <summary>
        ///  Consctructeur permettant le chargement d'un scénario représenté par un noeud XML.
        /// </summary>
        /// <param name="Node">Noeud XML représentant le scénario.</param>
        protected DataManagerScenario(XmlNode Node)
            : base(Node)
        {
        }

        /// <summary>
        /// Constructeur par copie d'un nouveau \ref DataManagerScenario
        /// </summary>
        /// <param name="dmsOldScenario"></param>
        protected DataManagerScenario(DataManagerScenario dmsOldScenario)
            : base(dmsOldScenario)
        {
            anRootNode = ArboNode.LoadArbo(dmsOldScenario.anRootNode.ExportArbo(new XmlDocument()));
        }
        #endregion

        #region Fonctions Copy, Dispose et Name
        /// <summary>
        /// Nom du système de données courant.
        /// </summary>
        public override String Name
        {
            get
            {
                ///Renvoie le résultats de \ref base.Name
                return base.Name;
            }
            set
            {
                ///Met à jour le nom pour le \ref DataManager de base et aussi pour les informations telles que le \ref psScenario.Name.
                base.Name = value;
                ///Appel de \ref UpdateArbo pour pouvoir mettre à jour les TreeViewTag avec le nom du scénario.
                UpdateArbo(anRootNode);
                ///Mise à jour également du noeud racine \ref anRootNode du nom du scénario.
                if ((anRootNode != null) && (anRootNode.tvtType != null))
                    anRootNode.tvtType.Name = value;
            }
        }

        /// <summary>
        /// Permet de rendre la mémoire allouée pour la classe.
        /// </summary>
        internal override void Dispose()
        {
            base.Dispose();
            anRootNode = null;
        }

        /// <summary>
        /// Fonction permettant la copie du \ref DataManagerScenario courant. 
        /// </summary>
        /// <returns>Nouvelle instance de \ref DataManagerScenario</returns>
        internal override Object Copy()
        {
            ///Appel du constructeur par copie \ref DataManagerScenario
            return new DataManagerScenario(this);
        }
        #endregion

        #region Pour ouvrir et sauver les donnees de DataManagerScenario
        /// <summary>
        /// Fonction qui permet de chargement des données à partir d'un noeud XML.
        /// </summary>
        /// <param name="xmlElement">Noeud XML représentant le \ref DataManagerScenario</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (afin de gérer les modifications dans les informations stockées).</param>
        /// <param name="sRootDirectory">Le dossier où se trouvent le fichier .pax (dossier racine du projet).</param>
        /// <param name="chForm">La fenêtre de chargement pour pouvoir faire progresser l'état du chargement.</param>
        /// <returns>Booléen indiquant si l'ouverture s'est bien déroulée ou non.</returns>
        internal override bool LoadInputData(XmlElement xmlElement, VersionManager vmVersion, String sRootDirectory, Prompt.SIM_LoadingForm chForm)
        {
            ///On vérifie quelle est la version d'enregistrement qui est chargé. Si inférieur à 1.51, alors
            ///la fonction courante ne s'occupera de charger rien.
            if (vmVersion <= new VersionManager(1, 51))
            {
                ///La version est ancienne, donc il faut simplement servir d'intermédiaire entre \ref DataManagerPaxBHS
                ///et \ref DataManager.
                return base.LoadInputData(xmlElement, vmVersion, sRootDirectory, chForm);
            }
            ///Si le noeud XML ne contient pas de noeud DataManager, alors renvoyer FAUx (Erreur dans le fichier)
            if (!OverallTools.FonctionUtiles.hasNamedChild(xmlElement, "DataManager"))
                return false;

            ///Appeler la fonction \ref DataManager.LoadInputData
            if (!base.LoadInputData(xmlElement["DataManager"], vmVersion, sRootDirectory, chForm))
                return false;

            ///On essaye désormais de charger l'arborescence qui a été enregistrée dans le fichier. \ref anRootNode
            if (!OverallTools.FonctionUtiles.hasNamedChild(xmlElement, "Scenario"))
                return false;
            if (xmlElement["Scenario"].FirstChild != null)
                anRootNode = ArboNode.LoadArbo((XmlElement)xmlElement["Scenario"].FirstChild);
            else
                anRootNode = null;
            return true;
        }

        /// <summary>
        /// Fonction qui s'occupe de générer un noeud et de sauvegarder la totalité des tables présentes dans le système de données.
        /// </summary>
        /// <param name="chForm">La fenêtre de sauvegarde pour pouvoir faire progresser l'état de la sauvegarde.</param>
        /// <returns></returns>
        internal override XmlElement Save(XmlDocument projet, string sRootDirectory, string sOldRootDirectory, string sSavingDirectory, Prompt.SIM_LoadingForm chForm)
        {
            ///Création d'un noeud XML "DataManagerScenario" qui regroupera toutes les informations pour cette classe.
            XmlElement arbreFiltres = projet.CreateElement("DataManagerScenario");
            ///Ajout de l'attribut "Name" avec le \ref sName 
            arbreFiltres.SetAttribute("Name", sName);

            ///Appel de la fonction de base \ref DataManager.Save pour sauvegarder l'ensemble des tables qui sont stockées dans la parties \ref DataManager.
            XmlElement xnBase = base.Save(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory, chForm);
            if (xnBase != null)
                arbreFiltres.AppendChild(xnBase);
            ///Si \ref anRootNode est différent de null, alors il est également sauvegardé dans le fichier XML.
            if (anRootNode != null)
            {
                XmlNode xnArbo = anRootNode.ExportArbo(projet);
                XmlNode xnRacine = projet.CreateElement("Scenario");
                xnRacine.AppendChild(xnArbo);
                arbreFiltres.AppendChild(xnRacine);
            }
            ///Renvoie du Noeud XML représentant ce \ref DataManagerScenario
            return arbreFiltres;
        }

        #endregion

        #region Pour les Tables

        /// <summary>
        /// Fonction qui ajoute le \ref NormalTable passé en paramètre au dictionnaire \ref dsntTables des tables.
        /// </summary>
        /// <param name="ntTable">Table à ajouter.</param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé ou non.</returns>
        internal override bool AddTable(NormalTable ntTable)
        {
            ///Si la table passée en paramètre est null, alors la fonction renvoie NULL.
            if (ntTable == null)
                return false;
            NormalTable ntTmp = ntTable;
            ///Si la table passée en paramètre n'instensie pas \ref ResultsTable, alors il faut la convertir dans 
            ///ce format pour garantir que les autres fonctions de \ref DataManagerScenario seront appliquées sans
            ///problèmes.
            if (!(ntTmp is ResultsTable))
            {
                if (ntTable is FilterTable)
                {
                    ///Si la table est une table \ref FilterTable, alors il faut la convertir en \ref ResultsFilterTable.
                    ntTmp = new ResultsFilterTable(ntTable);
                }
                else if (ntTable is NormalTable)
                {
                    ///Si la table est une table \ref NormalTable, alors il faut la convertir en \ref ResultsTable.
                    ntTmp = new ResultsTable(ntTable);
                }
            }
            ///Appel de la fonction de base \ref NormalTable.AddTable().
            return base.AddTable(ntTmp);
        }
        #endregion

        #region Pour les Filtres

        /// <summary>
        /// Fonction qui permet de mettre à jour les filtres dans l'arborescence. Cela permet d'ajouter les filtres aux tables qui en
        /// possèdent.
        /// </summary>
        /// <param name="treeNode">Noeud racine des données contenus dans le système de données courant.</param>
        /// <param name="cmsMenuFilter">Menu contextuel qui doit être ajouté aux filtes ainsi ajoutés.</param>
        internal override void UpdateFilters(System.Windows.Forms.TreeNode treeNode, System.Windows.Forms.ContextMenuStrip cmsMenuFilter, ContextMenuStrip cmsMenuException)
        {
            ///Appel de la fonction de base \ref base.UpdateFilters().
            base.UpdateFilters(treeNode, cmsMenuFilter, cmsMenuException);
        }
        #endregion

        #region Pour la gestion de l'arborescence du scénario
        /// <summary>
        /// Fonction qui permet de mettre à jour le nom du scénario dans toute l'arborescence. 
        /// </summary>
        /// <param name="anCurrentNode">Noeud racine.</param>
        private void UpdateArbo(ArboNode anCurrentNode)
        {
            ///Si le noeud courant est différent de NULL et contient une informations \ref ArboNode.tvtType, alors le nom du scénario
            ///stocké dans \ref ArboNode.tvtType est mis à la valeur de \ref Name.
            if (anCurrentNode == null)
                return;
            if (anCurrentNode.tvtType != null)
                anCurrentNode.tvtType.ScenarioName = Name;

            ///Si le noeud courant a des enfants, alors pour chacun d'entre eux :
            if (anCurrentNode.lanChilds != null)
                foreach (ArboNode anChild in anCurrentNode.lanChilds)
                {
                    ///__ Appel de la fonction \ref UpdateArbo sur l'enfant.
                    UpdateArbo(anChild);
                }
        }

        /// <summary>
        /// Fonction qui permet de charger l'arborescence depuis un Arbre TreeNode.
        /// </summary>
        /// <param name="tnCurrentArbo">Noeud du treeview représentant l'arborescence à copier.</param>
        internal void UpdateAbro(TreeNode tnCurrentArbo)
        {
            ///Appel de la fonction \ref ArboNode.LoadArbo().
            anRootNode = ArboNode.LoadArbo(tnCurrentArbo);
        }

        /// <summary>
        /// Fonction qui permet de générer le TreeNode et tous ses enfants qui se chargera de représenter le scénario courant. Elle se base
        /// sur \ref anRootNode.
        /// </summary>
        /// <param name="cmsBranchTestMenu">Menu contextuel à allouer au noeud racine du scénario</param>
        /// <param name="cmsContextMenuInput">Menu contextuel à allouer aux noeuds Table.</param>
        /// <param name="cmsFilterMenu">Menu contextuel à allouer aux Filtres.</param>
        /// <param name="cmsAutomodMenu">Menu contextuel à allouer au dossier contenant les graphiques Automod.</param>
        /// <param name="cmsUserGraphics">Menu contextuel à allouer aux graphiques Automod</param>
        /// <returns>Noeud représentant ce scénario.</returns>
        internal TreeNode GenerateScenarioNode(
                                     ContextMenuStrip cmsBranchTestMenu,
                                     ContextMenuStrip cmsContextMenuInput,
                                     ContextMenuStrip cmsFilterMenu,
                                     ContextMenuStrip cmsAutomodMenu,
                                     ContextMenuStrip cmsUserGraphics)
        {
            ///Appel de la fonction \ref GenerateScenarioNode().
            TreeNode tnRoot = GenerateScenarioNode(anRootNode, cmsContextMenuInput, cmsFilterMenu, cmsAutomodMenu, cmsUserGraphics);
            ///Si le noeud générer n'est pas null, alors on lui alloue le menu contextuel 
            if (tnRoot != null)
                tnRoot.ContextMenuStrip = cmsBranchTestMenu;
            ///Renvoie du noeud nouvellement généré.
            return tnRoot;
        }

        /// <summary>
        /// Fonction qui permet de générer le TreeNode et tous ses enfant à partir du \ref ArboNode passé en parametre.
        /// </summary>
        /// <param name="anCurrentNode">Arborescence qui doit être générée.</param>
        /// <param name="cmsBranchTestMenu">Menu contextuel à allouer au noeud racine du scénario</param>
        /// <param name="cmsContextMenuInput">Menu contextuel à allouer aux noeuds Table.</param>
        /// <param name="cmsFilterMenu">Menu contextuel à allouer aux Filtres.</param>
        /// <param name="cmsAutomodMenu">Menu contextuel à allouer au dossier contenant les graphiques Automod.</param>
        /// <param name="cmsUserGraphics">Menu contextuel à allouer aux graphiques Automod</param>
        /// <returns></returns>
        private TreeNode GenerateScenarioNode(ArboNode anCurrentNode,
                                     ContextMenuStrip cmsContextMenuInput,
                                     ContextMenuStrip cmsFilterMenu,
                                     ContextMenuStrip cmsAutomodMenu,
                                     ContextMenuStrip cmsUserGraphics)
        {
            TreeNode tnResult = null;
            if (anCurrentNode == null)
                return null;
            ///Suivant le type de noeud représenté par le noeud \ref anCurrentNode, création d'un Noeud du treeview avec les propriétés adéquats.
            TreeViewTag tvt = anCurrentNode.tvtType;
            if (anCurrentNode.vmMode != null)
            {
                UpdateModeVisualisation(tvt.Name, anCurrentNode.vmMode);
            }
            if (tvt.isDirectoryNode)
            {
                tnResult = OverallTools.TreeViewFunctions.CreateDirectory(tvt.Name);
                if (tvt.Name == GestionDonneesHUB2SIM.sScenarioCustomGraphic)
                {
                    cmsContextMenuInput = cmsUserGraphics;
                    tnResult.ContextMenuStrip = cmsAutomodMenu;
                }
            }
            else
            {
                NormalTable ntTable = GetTable(tvt.Name);
                if ((ntTable != null) && (ntTable.Note != null) && (ntTable.Note != ""))
                {
                    tvt.HasNote = true;
                }
                if (tvt.isFilterNode)
                {
                    tnResult = OverallTools.TreeViewFunctions.createBranch(tvt.Name, tvt.Name, tvt, cmsFilterMenu);
                }
                else if (tvt.isResultNode)
                {
                    tnResult = OverallTools.TreeViewFunctions.createBranch(tvt.Name, tvt.Name, tvt, cmsContextMenuInput);
                }
                else if (tvt.isTableNode)
                {
                    tnResult = OverallTools.TreeViewFunctions.createBranch(tvt.Name, tvt.Name, tvt, cmsContextMenuInput);
                }
            }

            ///Si le noeud courant a des enfants, alors pour chacun d'entre eux :
            foreach (ArboNode anTmp in anCurrentNode.lanChilds)
                ///__ Appel de la fonction \ref GenerateScenarioNode sur l'enfant, et ajout du noeud généré aux enfants du noeud courant.
                tnResult.Nodes.Add(GenerateScenarioNode(anTmp, cmsContextMenuInput, cmsFilterMenu, cmsAutomodMenu, cmsUserGraphics));
            return tnResult;
        }

        // >> Task #10326 Pax2Sim - Copy Scenario - copy process description
        /// <summary>
        /// Fonction qui permet de générer le TreeNode et tous ses enfants qui se chargera de représenter le scénario courant. Elle se base
        /// sur \ref anRootNode.
        /// </summary>
        /// <param name="cmsBranchTestMenu">Menu contextuel à allouer au noeud racine du scénario</param>
        /// <param name="cmsContextMenuInput">Menu contextuel à allouer aux noeuds Table.</param>
        /// <param name="cmsFilterMenu">Menu contextuel à allouer aux Filtres.</param>
        /// <param name="cmsAutomodMenu">Menu contextuel à allouer au dossier contenant les graphiques Automod.</param>
        /// <param name="cmsUserGraphics">Menu contextuel à allouer aux graphiques Automod</param>
        /// <param name="processAndDescriptionDictionary">Result nodes from the scenario -> K: process name, V: process name and description</param>
        /// <returns>Noeud représentant ce scénario.</returns>
        internal TreeNode GenerateScenarioNode(
                                     ContextMenuStrip cmsBranchTestMenu,
                                     ContextMenuStrip cmsContextMenuInput,
                                     ContextMenuStrip cmsFilterMenu,
                                     ContextMenuStrip cmsAutomodMenu,
                                     ContextMenuStrip cmsUserGraphics,
                                     Dictionary<String, String> processAndDescriptionDictionary)
        {
            ///Appel de la fonction \ref GenerateScenarioNode().
            TreeNode tnRoot = GenerateScenarioNode(anRootNode, cmsContextMenuInput,
                cmsFilterMenu, cmsAutomodMenu, cmsUserGraphics, processAndDescriptionDictionary);
            ///Si le noeud générer n'est pas null, alors on lui alloue le menu contextuel 
            if (tnRoot != null)
                tnRoot.ContextMenuStrip = cmsBranchTestMenu;
            ///Renvoie du noeud nouvellement généré.
            return tnRoot;
        }

        /// <summary>
        /// Fonction qui permet de générer le TreeNode et tous ses enfant à partir du \ref ArboNode passé en parametre.
        /// </summary>
        /// <param name="anCurrentNode">Arborescence qui doit être générée.</param>
        /// <param name="cmsBranchTestMenu">Menu contextuel à allouer au noeud racine du scénario</param>
        /// <param name="cmsContextMenuInput">Menu contextuel à allouer aux noeuds Table.</param>
        /// <param name="cmsFilterMenu">Menu contextuel à allouer aux Filtres.</param>
        /// <param name="cmsAutomodMenu">Menu contextuel à allouer au dossier contenant les graphiques Automod.</param>
        /// <param name="cmsUserGraphics">Menu contextuel à allouer aux graphiques Automod</param>
        /// <param name="processAndDescriptionDictionary">Result nodes from the scenario -> K: process name, V: process name and description</param>
        /// <returns></returns>
        private TreeNode GenerateScenarioNode(ArboNode anCurrentNode,
                                     ContextMenuStrip cmsContextMenuInput,
                                     ContextMenuStrip cmsFilterMenu,
                                     ContextMenuStrip cmsAutomodMenu,
                                     ContextMenuStrip cmsUserGraphics,
                                     Dictionary<String, String> processAndDescriptionDictionary)
        {
            TreeNode tnResult = null;
            if (anCurrentNode == null)
                return null;
            ///Suivant le type de noeud représenté par le noeud \ref anCurrentNode, création d'un Noeud du treeview avec les propriétés adéquats.
            TreeViewTag tvt = anCurrentNode.tvtType;
            if (anCurrentNode.vmMode != null)
            {
                UpdateModeVisualisation(tvt.Name, anCurrentNode.vmMode);
            }
            if (tvt.isDirectoryNode)
            {
                tnResult = OverallTools.TreeViewFunctions.CreateDirectory(tvt.Name);
                if (tvt.Name == GestionDonneesHUB2SIM.sScenarioCustomGraphic)
                {
                    cmsContextMenuInput = cmsUserGraphics;
                    tnResult.ContextMenuStrip = cmsAutomodMenu;
                }
            }
            else
            {
                NormalTable ntTable = GetTable(tvt.Name);
                if ((ntTable != null) && (ntTable.Note != null) && (ntTable.Note != ""))
                {
                    tvt.HasNote = true;
                }
                if (tvt.isFilterNode)
                {
                    tnResult = OverallTools.TreeViewFunctions.createBranch(tvt.Name, tvt.Name, tvt, cmsFilterMenu);
                }
                else if (tvt.isResultNode)
                {
                    String processText = tvt.Name;

                    if (processAndDescriptionDictionary.ContainsKey(tvt.Name))
                        processAndDescriptionDictionary.TryGetValue(tvt.Name, out processText);

                    tnResult = OverallTools.TreeViewFunctions.createBranch(tvt.Name, processText, tvt, cmsContextMenuInput);
                }
                else if (tvt.isTableNode)
                {
                    tnResult = OverallTools.TreeViewFunctions.createBranch(tvt.Name, tvt.Name, tvt, cmsContextMenuInput);
                }
            }

            ///Si le noeud courant a des enfants, alors pour chacun d'entre eux :
            foreach (ArboNode anTmp in anCurrentNode.lanChilds)
                ///__ Appel de la fonction \ref GenerateScenarioNode sur l'enfant, et ajout du noeud généré aux enfants du noeud courant.
                tnResult.Nodes.Add(GenerateScenarioNode(anTmp, cmsContextMenuInput, cmsFilterMenu, cmsAutomodMenu, cmsUserGraphics,
                    processAndDescriptionDictionary));
            return tnResult;
        }
        // << Task #10326 Pax2Sim - Copy Scenario - copy process description
        #endregion
    }
    #endregion

    #region public class DataManagerParking : DataManagerScenario
    public class DataManagerParking : DataManagerScenario
    {
        #region Les différentes variables de la classe.
        /// <summary>
        /// The title that the user wants to give to the scenario.
        /// </summary>
        private String sTitle;

        /// <summary>
        /// The name of the general table for parking that had been used for the calcs.
        /// </summary>
        private String sGeneralTableName;

        /// <summary>
        /// The name of the distribution table for parking that had been used for the calcs.
        /// </summary>
        private String sDistributionTableName;

        /// <summary>
        /// The name of the modal distribution table for parking that had been used for the calcs.
        /// </summary>
        private String sModalDistributionTableName;

        /// <summary>
        /// The name of the occupation table for parking that had been used for the calcs.
        /// </summary>
        private String sOccupationTableName;

        /// <summary>
        /// The title that the user wants to give to the scenario.
        /// </summary>
        internal String Title
        {
            get
            {
                return sTitle;
            }
            set { sTitle = value; }
        }

        /// <summary>
        /// The name of the general table for parking that had been used for the calcs.
        /// </summary>
        internal String GeneralTableName
        {
            get
            {
                return sGeneralTableName;
            }
            set { sGeneralTableName = value; }
        }

        /// <summary>
        /// The name of the distribution table for parking that had been used for the calcs.
        /// </summary>
        internal String DistributionTableName
        {
            get
            {
                return sDistributionTableName;
            }
            set { sDistributionTableName = value; }
        }

        /// <summary>
        /// The name of the modal distribution table for parking that had been used for the calcs.
        /// </summary>
        internal String ModalDistributionTableName
        {
            get
            {
                return sModalDistributionTableName;
            }
            set { sModalDistributionTableName = value; }
        }

        /// <summary>
        /// The name of the occupation table for parking that had been used for the calcs.
        /// </summary>
        internal String OccupationTableName
        {
            get
            {
                return sOccupationTableName;
            }
            set { sOccupationTableName = value; }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Consctructeur permettant l'initialisation de  la classe représentant un Scénario parking.
        /// </summary>
        /// <param name="Name">Nom du scénario</param>
        public DataManagerParking(String Name)
            : base(Name)
        {
            sTitle = Name;
        }

        /// <summary>
        ///  Consctructeur permettant le chargement d'un scénario représenté par un noeud XML.
        /// </summary>
        /// <param name="Node">Noeud XML représentant le scénario.</param>
        public DataManagerParking(XmlNode Node)
            : base(Node)
        {
        }

        /// <summary>
        /// Constructeur par copie d'un nouveau \ref DataManagerParking
        /// </summary>
        /// <param name="dmsOldScenario"></param>
        protected DataManagerParking(DataManagerParking dmsOldScenario)
            : base(dmsOldScenario)
        {
            sTitle = dmsOldScenario.sTitle;
            sGeneralTableName = dmsOldScenario.sGeneralTableName;
            sDistributionTableName = dmsOldScenario.sDistributionTableName;
            sModalDistributionTableName = dmsOldScenario.sModalDistributionTableName;
            sOccupationTableName = dmsOldScenario.sOccupationTableName;
        }
        #endregion

        #region Fonctions Copy, Dispose et Name
        /// <summary>
        /// Nom du système de données courant.
        /// </summary>
        public override String Name
        {
            get
            {
                ///Renvoie le résultats de \ref base.Name
                return base.Name;
            }
            set
            {
                ///Met à jour le nom pour le \ref DataManagerScenario de base.
                base.Name = value;
            }
        }

        /// <summary>
        /// Permet de rendre la mémoire allouée pour la classe.
        /// </summary>
        internal override void Dispose()
        {
            base.Dispose();
        }

        /// <summary>
        /// Fonction permettant la copie du \ref DataManagerParking courant. 
        /// </summary>
        /// <returns>Nouvelle instance de \ref DataManagerParking</returns>
        internal override Object Copy()
        {
            ///Appel du constructeur par copie \ref DataManagerParking
            return new DataManagerParking(this);
        }
        #endregion

        #region Pour ouvrir et sauver les donnees de DataManagerScenario
        /// <summary>
        /// Fonction qui permet de chargement des données à partir d'un noeud XML.
        /// </summary>
        /// <param name="xmlElement">Noeud XML représentant le \ref DataManagerParking</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (afin de gérer les modifications dans les informations stockées).</param>
        /// <param name="sRootDirectory">Le dossier où se trouvent le fichier .pax (dossier racine du projet).</param>
        /// <param name="chForm">La fenêtre de chargement pour pouvoir faire progresser l'état du chargement.</param>
        /// <returns>Booléen indiquant si l'ouverture s'est bien déroulée ou non.</returns>
        internal override bool LoadInputData(XmlElement xmlElement, VersionManager vmVersion, String sRootDirectory, Prompt.SIM_LoadingForm chForm)
        {

            ///Si le noeud XML ne contient pas de noeud DataManager, alors renvoyer FAUx (Erreur dans le fichier)
            if (!OverallTools.FonctionUtiles.hasNamedChild(xmlElement, "DataManagerScenario"))
                return false;
            ///Appeler la fonction \ref DataManagerScenario.LoadInputData
            if (!base.LoadInputData(xmlElement["DataManagerScenario"], vmVersion, sRootDirectory, chForm))
                return false;
            if (OverallTools.FonctionUtiles.hasNamedAttribute(xmlElement, "Title"))
                sTitle = xmlElement.Attributes["Title"].Value;
            else
                sTitle = sName;
            ///Réouverture des noms des tables utilisés pour la génération des résultats.
            if (OverallTools.FonctionUtiles.hasNamedAttribute(xmlElement, "GeneralTable"))
                sGeneralTableName = xmlElement.Attributes["GeneralTable"].Value;
            if (OverallTools.FonctionUtiles.hasNamedAttribute(xmlElement, "Distribution"))
                sDistributionTableName = xmlElement.Attributes["Distribution"].Value;
            if (OverallTools.FonctionUtiles.hasNamedAttribute(xmlElement, "ModalDistribution"))
                sModalDistributionTableName = xmlElement.Attributes["ModalDistribution"].Value;
            if (OverallTools.FonctionUtiles.hasNamedAttribute(xmlElement, "Occupation"))
                sOccupationTableName = xmlElement.Attributes["Occupation"].Value;
            return true;
        }
        /// <summary>
        /// Fonction qui s'occupe de générer un noeud et de sauvegarder la totalité des tables présentes dans le système de données.
        /// </summary>
        /// <param name="projet">Projet XML auquel tous les noeuds XML doivent être liés.</param>
        /// <param name="chForm">La fenêtre de sauvegarde pour pouvoir faire progresser l'état de la sauvegarde.</param>
        /// <returns></returns>
        internal override XmlElement Save(XmlDocument projet, string sRootDirectory, string sOldRootDirectory, string sSavingDirectory, Prompt.SIM_LoadingForm chForm)
        {
            ///Création d'un noeud XML "DataManagerPaxBHS" qui regroupera toutes les informations pour cette classe.
            XmlElement arbreFiltres = projet.CreateElement("DataManagerParking");
            ///Ajout de l'attribut "Name" avec le \ref sName 
            arbreFiltres.SetAttribute("Name", sName);


            ///Ajout d'un attribut au noeud XML avec le titre du système de données courant.
            arbreFiltres.SetAttribute("Title", sTitle);
            arbreFiltres.SetAttribute("GeneralTable", sGeneralTableName);
            arbreFiltres.SetAttribute("Distribution", sDistributionTableName);
            arbreFiltres.SetAttribute("ModalDistribution", sModalDistributionTableName);
            arbreFiltres.SetAttribute("Occupation", sOccupationTableName);

            ///Appel de la fonction de base \ref DataManagerScenario.Save pour sauvegarder l'ensemble des tables qui sont stockées dans la parties \ref DataManagerScenario.
            XmlElement xnBase = base.Save(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory, chForm);
            if (xnBase != null)
                arbreFiltres.AppendChild(xnBase);
            ///Renvoie du Noeud XML représentant ce \ref DataManagerPaxBHS
            return arbreFiltres;
        }
        #endregion

        /// <summary>
        /// Function that will generate a review for the given class. That will allow the user to create
        /// a note with these informations to keep track of the informations use for the Scenario.
        /// </summary>
        /// <param name="bPrintPDF"></param>
        /// <returns></returns>
        internal List<string> getReview(bool bPrintPDF)
        {

            List<string> lsResult = new List<string>();
            /*Le nom et les statistics communes à tous les scénarios.*/
            lsResult.Add("Name" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + sName);
            lsResult.Add(" ");
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "Static parking calc" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1));
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "General Table" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + sGeneralTableName);
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "Distribution" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + sDistributionTableName);
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "Modal Distribution" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + sModalDistributionTableName);
            lsResult.Add(ParamScenario.PrintIndexation(bPrintPDF, false, true, 2) + "Occupation" + " :" + ParamScenario.PrintIndexation(bPrintPDF, false, false, 1) + sOccupationTableName);

            return lsResult;
        }
    }
    #endregion

    #region public class DataManagerPaxBHS : DataManagerScenario
    public class DataManagerPaxBHS : DataManagerScenario
    {
        #region Les différentes variables de la classe.
        /// <summary>
        /// Booléen indiquant si le scénario utilise la sauvegarde de la trace ou non. Cela implique que certaines tables seront
        /// recalculées à partir de la trace (Pax ou Bag) et ne seront donc pas stockées sur disque (Gain de temps à la sauvegarde).
        /// </summary>
        private Boolean bUseTraceSaving;

        /// <summary>
        /// Dans le cas où \ref bUseTraceSaving est à vrai, alors cette variable contiendra le chemin d'accès à la trace Bagage (si 
        /// celle ci existe).
        /// </summary>
        private String sBagTraceLocation;

        /// <summary>
        /// Dans le cas où \ref bUseTraceSaving est à vrai, alors cette variable contiendra le chemin d'accès à la trace Passager (si 
        /// celle ci existe).
        /// </summary>
        private String sPaxTraceLocation;

        /// <summary>
        /// Ce sont les paramètres du scénario. Cette classe contient toutes les informations concernant le paramétrage du scénario qui
        /// a généré les données stockées dans la classe.
        /// </summary>
        private Classes.ParamScenario psScenario;

        /// <summary>
        /// Délégué représentant la fonction qui doit être appelée pour recalculer les données à partir des traces. (lorsque \ref bUseTraceSaving
        /// est à VRAI).
        /// </summary>
        /// <param name="sScenarioName">Nom du scénario pour lequel il faut recalculer les tables.</param>
        /// <param name="sPaxTraceLocation">Chemin d'accès à la trace Passager</param>
        /// <param name="sBagTraceLocation">Chemin d'accès à la trace Bagage.</param>
        internal delegate void AnalyseTracesDelegate(String sScenarioName, String sPaxTraceLocation, String sBagTraceLocation);

        /// <summary>
        /// Dans le cas où \ref bUseTraceSaving est à vrai, cette variable contient la fonction qui doit être appelée pour recalculer
        /// les données pour le scénario à partir des traces.
        /// </summary>
        private AnalyseTracesDelegate atAnalyseTraces;

        // >> Task #13955 Pax2Sim -BHS trace loading issue
        internal bool useTraceSaving
        {
            get
            {
                return bUseTraceSaving;
            }
        }
        // << Task #13955 Pax2Sim -BHS trace loading issue

        /// <summary>
        /// Obtient ou définit le chemin d'accès à la trace Bagage (\ref sBagTraceLocation).
        /// </summary>
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

        /// <summary>
        /// Obtient ou définit le chemin d'accès à la trace Passager (\ref sPaxTraceLocation).
        /// </summary>
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

        /// <summary>
        /// Obtient ou définit les paramètres du scénario (\ref psScenario).
        /// </summary>
        internal Classes.ParamScenario Scenario
        {
            get
            {
                return psScenario;
            }
            set
            {
                psScenario = value;
                bUseTraceSaving = value.SaveTraceMode;
            }
        }

        /// <summary>
        /// Obtient ou définit la fonction pour analyser les traces (\ref atAnalyseTraces).
        /// </summary>
        internal AnalyseTracesDelegate AnalyseTracesFonctions
        {
            get
            {
                return atAnalyseTraces;
            }
            set
            {
                atAnalyseTraces = value;
            }
        }

        /// <summary>
        /// Délégué permettant de définir la fonction appelée pour recalculée les tables à partir des traces lorsque celle ci n'ont pas
        /// encore été recalculées.
        /// </summary>
        internal delegate void OpenTraceDelegate();

        /// <summary>
        /// Fonction qui est appelée pour lancer l'analyse de trace pour le recalcul des tables.
        /// </summary>
        internal void OpenTrace()
        {
            ///Appel de la fonction \ref atAnalyseTraces si celle ci n'est pas null.
            if (atAnalyseTraces != null)
                atAnalyseTraces(this.Name, PaxTraceLocation, BagTraceLocation);
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Consctructeur permettant l'initialisation de  la classe représentant un Scénario.
        /// </summary>
        /// <param name="Name">Nom du scénario</param>
        public DataManagerPaxBHS(String Name)
            : base(Name)
        {
            bUseTraceSaving = false;
            sBagTraceLocation = "";
            sPaxTraceLocation = "";
            atAnalyseTraces = null;
        }

        /// <summary>
        ///  Consctructeur permettant le chargement d'un scénario représenté par un noeud XML.
        /// </summary>
        /// <param name="Node">Noeud XML représentant le scénario.</param>
        public DataManagerPaxBHS(XmlNode Node)
            : base(Node)
        {
            //TODO : Mettre le code de chargement XML ici.
            bUseTraceSaving = false;
            sBagTraceLocation = "";
            sPaxTraceLocation = "";
            atAnalyseTraces = null;
        }

        /// <summary>
        /// Constructeur par copie d'un nouveau \ref DataManagerScenario
        /// </summary>
        /// <param name="dmsOldScenario"></param>

        /*        protected DataManagerPaxBHS(DataManagerPaxBHS dmsOldScenario)
                    : base(dmsOldScenario)
                {
                    bUseTraceSaving = dmsOldScenario.bUseTraceSaving;
                    if (dmsOldScenario.bUseTraceSaving)
                    {
                        if ((dmsOldScenario.sBagTraceLocation != null) && (dmsOldScenario.sBagTraceLocation != ""))
                        {
                            int iTmp = 1;
                            while (System.IO.File.Exists(dmsOldScenario.sBagTraceLocation + iTmp.ToString()))
                            {
                                iTmp++;
                            }
                            sBagTraceLocation = dmsOldScenario.sBagTraceLocation + iTmp.ToString();
                            OverallTools.ExternFunctions.CopyFile(sBagTraceLocation, dmsOldScenario.sBagTraceLocation, "", "Err00422 : Unable to copy the Bagtrace when attempting to copy " + dmsOldScenario.Name, null, null);
                        }
                        if ((dmsOldScenario.sPaxTraceLocation != null) && (dmsOldScenario.sPaxTraceLocation != ""))
                        {
                            int iTmp = 1;
                            while (System.IO.File.Exists(dmsOldScenario.sPaxTraceLocation + iTmp.ToString()))
                            {
                                iTmp++;
                            }
                            sPaxTraceLocation = dmsOldScenario.sPaxTraceLocation + iTmp.ToString();
                            OverallTools.ExternFunctions.CopyFile(sPaxTraceLocation, dmsOldScenario.sPaxTraceLocation, "", "Err00423 : Unable to copy the Paxtrace when attempting to copy " + dmsOldScenario.Name, null, null);
                        }
                    }

                    atAnalyseTraces = null;
                    psScenario = new Classes.ParamScenario(dmsOldScenario.psScenario.getParams(new XmlDocument()), new VersionManager(0, 0));
                }
                */
        protected DataManagerPaxBHS(DataManagerPaxBHS dmsOldScenario)   // >> Task #1954_bagTrace
            : base(dmsOldScenario)
        {
            bUseTraceSaving = dmsOldScenario.bUseTraceSaving;
            if (dmsOldScenario.bUseTraceSaving)
            {
                if ((dmsOldScenario.sBagTraceLocation != null) && (dmsOldScenario.sBagTraceLocation != ""))
                {
                    string newTraceName = dmsOldScenario.sBagTraceLocation;
                    if (newTraceName.Contains(dmsOldScenario.Name))
                        newTraceName = dmsOldScenario.sBagTraceLocation.Replace(dmsOldScenario.Name, dmsOldScenario.tempName);
                    if (!File.Exists(newTraceName))
                    {
                        sBagTraceLocation = newTraceName;
                        OverallTools.ExternFunctions.CopyFile(sBagTraceLocation, dmsOldScenario.sBagTraceLocation, "", "Err00422 : Unable to copy the Bagtrace when attempting to copy " + dmsOldScenario.Name, null, null);
                    }
                }
                if ((dmsOldScenario.sPaxTraceLocation != null) && (dmsOldScenario.sPaxTraceLocation != ""))
                {
                    string newTraceName = dmsOldScenario.sPaxTraceLocation;
                    if (newTraceName.Contains(dmsOldScenario.Name))
                        newTraceName = dmsOldScenario.sPaxTraceLocation.Replace(dmsOldScenario.Name, dmsOldScenario.tempName);
                    if (!File.Exists(newTraceName))
                    {
                        sPaxTraceLocation = newTraceName;
                        OverallTools.ExternFunctions.CopyFile(sPaxTraceLocation, dmsOldScenario.sPaxTraceLocation, "", "Err00423 : Unable to copy the Paxtrace when attempting to copy " + dmsOldScenario.Name, null, null);
                    }
                }
            }

            atAnalyseTraces = null;
            psScenario = new Classes.ParamScenario(dmsOldScenario.psScenario.getParams(new XmlDocument()), new VersionManager(0, 0));
        }
        #endregion

        #region Fonctions Copy, Dispose et Name
        /// <summary>
        /// Nom du système de données courant.
        /// </summary>
        public override String Name
        {
            get
            {
                ///Renvoie le résultats de \ref base.Name
                return base.Name;
            }
            set
            {
                ///Met à jour le nom pour le \ref DataManagerPaxBHS de base et aussi pour les informations telles que le \ref psScenario.Name.
                base.Name = value;
                psScenario.Name = value;
            }
        }

        /// <summary>
        /// Permet de rendre la mémoire allouée pour la classe.
        /// </summary>
        internal override void Dispose()
        {
            base.Dispose();
            if (PaxTraceLocation != null)
                ///Si les traces étaient dans le dossier Temporaire de PAX2SIM, alors elles sont supprimées.
                if (PaxTraceLocation.StartsWith(OverallTools.ExternFunctions.getUserDirectoryForPax2sim()))
                    OverallTools.ExternFunctions.DeleteFile(PaxTraceLocation);
            if (BagTraceLocation != null)
                if (BagTraceLocation.StartsWith(OverallTools.ExternFunctions.getUserDirectoryForPax2sim()))
                    OverallTools.ExternFunctions.DeleteFile(BagTraceLocation);
            atAnalyseTraces = null;
            if (psScenario != null)
            {
                psScenario.Dispose();
                psScenario = null;
            }
            sPaxTraceLocation = null;
            sBagTraceLocation = null;
        }

        /// <summary>
        /// Fonction permettant la copie du \ref DataManagerPaxBHS courant. 
        /// </summary>
        /// <returns>Nouvelle instance de \ref DataManagerPaxBHS</returns>
        internal override Object Copy()
        {
            ///Appel du constructeur par copie \ref DataManagerPaxBHS
            return new DataManagerPaxBHS(this);
        }
        #endregion

        #region Pour ouvrir et sauver les donnees de DataManagerScenario
        /// <summary>
        /// Fonction qui permet de chargement des données à partir d'un noeud XML.
        /// </summary>
        /// <param name="xmlElement">Noeud XML représentant le \ref DataManagerPaxBHS</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (afin de gérer les modifications dans les informations stockées).</param>
        /// <param name="sRootDirectory">Le dossier où se trouvent le fichier .pax (dossier racine du projet).</param>
        /// <param name="chForm">La fenêtre de chargement pour pouvoir faire progresser l'état du chargement.</param>
        /// <returns>Booléen indiquant si l'ouverture s'est bien déroulée ou non.</returns>
        internal override bool LoadInputData(XmlElement xmlElement, VersionManager vmVersion, String sRootDirectory, Prompt.SIM_LoadingForm chForm)
        {
            ///We check the version of the scenario that has to be loaded.
            bool bVersion_1_51 = (vmVersion <= new VersionManager(1, 51));

            ///Si le noeud XML ne contient pas de noeud DataManager, alors renvoyer FAUx (Erreur dans le fichier)
            if (((!OverallTools.FonctionUtiles.hasNamedChild(xmlElement, "DataManagerScenario")) && (!bVersion_1_51)) &&
                ((!OverallTools.FonctionUtiles.hasNamedChild(xmlElement, "DataManager")) && (bVersion_1_51)))
                return false;
            if (bVersion_1_51)
            {
                ///Appeler la fonction \ref DataManager.LoadInputData pour l'ancienne version.
                if (!base.LoadInputData(xmlElement["DataManager"], vmVersion, sRootDirectory, chForm))
                    return false;
            }
            else
            {
                ///Appeler la fonction \ref DataManager.LoadInputData
                if (!base.LoadInputData(xmlElement["DataManagerScenario"], vmVersion, sRootDirectory, chForm))
                    return false;
            }

            ///Recherche d'un noeud "ParamScenario" représentant les paramètres du scénario. S'il existe, alors appel du constructeur \ref  Classes.ParamScenario()
            if (OverallTools.FonctionUtiles.hasNamedChild(xmlElement, "ParamScenario"))
                psScenario = new Classes.ParamScenario(xmlElement["ParamScenario"], vmVersion);

            ///Recherche d'un noeud "PaxTrace" représentant le chemin d'accès à la trace passager.
            if (OverallTools.FonctionUtiles.hasNamedAttribute(xmlElement, "PaxTrace"))
            {
                ///__ Copie du fichier de trace passager dans le dossier temporaire pour l'application (\ref OverallTools.ExternFunctions.getTempDirectoryForPax2sim())
                String sOldFile = sRootDirectory + "\\" + xmlElement.Attributes["PaxTrace"].Value;
                if (System.IO.File.Exists(sOldFile))
                {
                    OverallTools.ExternFunctions.CheckCreateDirectory(OverallTools.ExternFunctions.getTempDirectoryForPax2sim());
                    PaxTraceLocation = OverallTools.ExternFunctions.getTempDirectoryForPax2sim() + sName + "_PaxTrace.tmp";
                    OverallTools.ExternFunctions.CopyFile(PaxTraceLocation, sOldFile, "", null, null, null);
                }
            }
            ///Recherche d'un noeud "PaxTrace" représentant le chemin d'accès à la trace Bagage.
            if (OverallTools.FonctionUtiles.hasNamedAttribute(xmlElement, "BagTrace"))
            {
                ///__ Copie du fichier de trace bagage dans le dossier temporaire pour l'application (\ref OverallTools.ExternFunctions.getTempDirectoryForPax2sim())
                String sOldFile = sRootDirectory + "\\" + xmlElement.Attributes["BagTrace"].Value;
                if (System.IO.File.Exists(sOldFile))
                {
                    OverallTools.ExternFunctions.CheckCreateDirectory(OverallTools.ExternFunctions.getTempDirectoryForPax2sim());
                    BagTraceLocation = OverallTools.ExternFunctions.getTempDirectoryForPax2sim() + sName + "_BagTrace.tmp";
                    OverallTools.ExternFunctions.CopyFile(BagTraceLocation, sOldFile, "", null, null, null);
                }
            }
            // >> Task #8958 Reclaim Synchronisation mode Gantt
            if (OverallTools.FonctionUtiles.hasNamedAttribute(xmlElement, "BagTrace")
                || OverallTools.FonctionUtiles.hasNamedAttribute(xmlElement, "PaxTrace"))
            {
                String oldFilePath = sRootDirectory + "\\" + "Output" + "\\" + psScenario.Name + "\\" + GlobalNames.RECLAIM_LOG_FILE_NAME + ".txt";
                if (System.IO.File.Exists(oldFilePath))
                {
                    OverallTools.ExternFunctions.CheckCreateDirectory(OverallTools.ExternFunctions.getTempDirectoryForPax2sim());
                    string newFilePath = OverallTools.ExternFunctions.getTempDirectoryForPax2sim() + sName + "_" + GlobalNames.RECLAIM_LOG_FILE_NAME + ".tmp";
                    OverallTools.ExternFunctions.CopyFile(newFilePath, oldFilePath, "", null, null, null);
                }
            }
            // << Task #8958 Reclaim Synchronisation mode Gantt
            if (bVersion_1_51)
            {
                ///On essaye désormais de charger l'arborescence qui a été enregistrée dans le fichier. \ref anRootNode
                if (!OverallTools.FonctionUtiles.hasNamedChild(xmlElement, "Scenario"))
                    return false;
                if (xmlElement["Scenario"].FirstChild != null)
                    anRootNode = ArboNode.LoadArbo((XmlElement)xmlElement["Scenario"].FirstChild);
                else
                    anRootNode = null;
            }
            return true;
        }
        /// <summary>
        /// Fonction qui s'occupe de générer un noeud et de sauvegarder la totalité des tables présentes dans le système de données.
        /// </summary>
        /// <param name="chForm">La fenêtre de sauvegarde pour pouvoir faire progresser l'état de la sauvegarde.</param>
        /// <returns></returns>
        internal override XmlElement Save(XmlDocument projet, string sRootDirectory, string sOldRootDirectory, string sSavingDirectory, Prompt.SIM_LoadingForm chForm)
        {
            ///Création d'un noeud XML "DataManagerPaxBHS" qui regroupera toutes les informations pour cette classe.
            XmlElement arbreFiltres = projet.CreateElement("DataManagerPaxBHS");
            ///Ajout de l'attribut "Name" avec le \ref sName 
            arbreFiltres.SetAttribute("Name", sName);
            ///Ajout d'un noeud ParamScenario qui contient \ref psScenario
            XmlNode xnScenario = psScenario.getParams(projet);
            if (xnScenario != null)
                arbreFiltres.AppendChild(xnScenario);

            ///Si le mode de sauvegarde est avec sauvegarde de trace.
            //if (psScenario.SaveTraceMode) // >> Task #1954_bagTrace
            //{
            ///__ Sauvegarde de la trace passager et bagage (suivant laquelle ou lesquelles existent).
            String sOriginingPath = OverallTools.ExternFunctions.getTempDirectoryForPax2sim();
            if (psScenario.PaxSimulation)
            {
                String sDestinationFile = sRootDirectory + sSavingDirectory + "PaxTrace.txt";
                if (PaxTraceLocation != null && PaxTraceLocation.StartsWith(sOriginingPath))
                {
                    //The PaxTrace is saved in the User Directory. So we have to copy it to the right directory.
                    OverallTools.ExternFunctions.CopyFile(sDestinationFile, PaxTraceLocation, "", null, null, null);
                    arbreFiltres.SetAttribute("PaxTrace", sSavingDirectory + "PaxTrace.txt");
                }
            }
            if (psScenario.BHSSimulation)
            {
                String sDestinationFile = sRootDirectory + sSavingDirectory + "BagTrace.txt";
                if (BagTraceLocation != null && BagTraceLocation.StartsWith(sOriginingPath))
                {
                    //The PaxTrace is saved in the User Directory. So we have to copy it to the right directory.
                    OverallTools.ExternFunctions.CopyFile(sDestinationFile, BagTraceLocation, "", null, null, null);
                    arbreFiltres.SetAttribute("BagTrace", sSavingDirectory + "BagTrace.txt");
                }
            }
            // >> Task #8958 Reclaim Synchronisation mode Gantt
            String tempDirectory = OverallTools.ExternFunctions.getTempDirectoryForPax2sim();
            //string originFullPath
            string originFullPath = tempDirectory + sName + "_" + GlobalNames.RECLAIM_LOG_FILE_NAME + ".tmp";
            string destinationFullPath = sRootDirectory + sSavingDirectory + GlobalNames.RECLAIM_LOG_FILE_NAME + ".txt";
            if (tempDirectory != "" && System.IO.File.Exists(originFullPath))
                OverallTools.ExternFunctions.CopyFile(destinationFullPath, originFullPath, "", null, null, null);
            // << Task #8958 Reclaim Synchronisation mode Gantt

            //string projectDirectoryPath = Data.getDossierEnregistrement();
            //string scenarioDirectory = projectDirectoryPath + "Output\\" + ScenarioName + "\\";
            //string reclaimLogFullPathInScenarioOutputDirectory = scenarioDirectory + GlobalNames.RECLAIM_LOG_FILE_NAME + ".txt";

            // >> Task #13240 Pax2Sim - Dynamic simulation - scenario note update
            /*
            DateTime lastModificationDate = DateTime.MinValue;
            if (PaxTraceLocation != null && File.Exists(PaxTraceLocation))
            {
                if (File.GetLastWriteTime(PaxTraceLocation) > lastModificationDate)
                    lastModificationDate = File.GetLastWriteTime(PaxTraceLocation);                    
            }
            if (BagTraceLocation != null && File.Exists(BagTraceLocation))
            {
                 if (File.GetLastWriteTime(BagTraceLocation) > lastModificationDate)
                    lastModificationDate = File.GetLastWriteTime(BagTraceLocation);                     
            }                
            psScenario.lastUpdateDate = lastModificationDate;
             */
            // << Task #13240 Pax2Sim - Dynamic simulation - scenario note update
            //}

            ///Appel de la fonction de base \ref DataManagerScenario.Save pour sauvegarder l'ensemble des tables qui sont stockées dans la parties \ref DataManagerScenario.
            XmlElement xnBase = base.Save(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory, chForm);
            if (xnBase != null)
                arbreFiltres.AppendChild(xnBase);
            ///Renvoie du Noeud XML représentant ce \ref DataManagerPaxBHS
            return arbreFiltres;
        }

        /// <summary>
        /// Fonction qui permet de convertir l'ancien format de sauvegarde en un \ref DataManagerPaxBHS.
        /// </summary>
        /// <param name="gdData">Ancien format de données.</param>
        internal override void ConvertGestionDonnees(GestionDonnees gdData)
        {
            ///Récupération de l'ensemble des noms des Tables.
            List<String> lsTables = gdData.TablesNames;

            ///Récupération de l'ensemble des noms des filtres.
            List<String> lsFilters = new List<string>();
            foreach (String sKey in gdData.ListFilters.Keys)
                lsFilters.Add(sKey);

            ///Pour toutes les tables présentes dans \ref gdData
            foreach (String sTable in lsTables)
            {
                ///__ S'il s'agit d'un filtre, alors passé à la table suivante.
                if (lsFilters.Contains(sTable))
                    continue;

                ///__ Si la table est ouverte (ie : Chargée en mémoire), alors appel du constructeur de \ref ResultsTable avec la table en parametre, sinon
                ///appel du constructeur avec le nom de la table en parametre.
                ResultsTable ntTmp = null;
                if (!gdData.isOpenned(sTable))
                {
                    ntTmp = new ResultsTable(sTable, gdData.getModeVisualisation(sTable), gdData.getGraphicFilter(sTable));
                }
                else
                {
                    ntTmp = new ResultsTable(gdData.GetTable(sTable), gdData.getModeVisualisation(sTable), gdData.getGraphicFilter(sTable));
                }

                ///__ Mise à jour de la propriété \ref ResultsTable.CalculatedFromTrace avec les données propre à la table :\ref GestionDonneesHUB2SIM.GestionDonnees.IsCalculatedFromTrace().
                ntTmp.CalculatedFromTrace = gdData.IsCalculatedFromTrace(sTable);
                if (ntTmp.CalculatedFromTrace)
                    ntTmp.OpenTrace = new OpenTraceDelegate(OpenTrace);
                if ((!ntTmp.CalculatedFromTrace) && (!gdData.isOpenned(sTable)))
                {
                    ntTmp.Path = gdData.PathPartialOpened(sTable);
                }
                ///__ Mise à jour de la note de la table
                ntTmp.Note = gdData.getNote(sTable);
                ///__ Ajout de la table au système de données avec \ref AddTable().
                AddTable(ntTmp);
            }

            String sFirstAdded = "";
            bool bHasProblem = false;
            for (int i = 0; i < lsFilters.Count; i++)
            {
                String sFilter = lsFilters[i];
                ///__ Récupération de la définition du filtre \ref Filter.
                Filter fFilter = gdData.getFilter(sFilter);
                ///__ Si la table parente de ce filtre n'est pas encore présente dans le système de données courant,
                ///alors cette table est mise en fin de liste, afin d'être traitée après tous les autres filtres.
                if (!Exist(fFilter.MotherTableName))
                {
                    //Si bHasProblem est vrai et que le filtre courant avait déjà été mis à la fin de la liste, cela signifie qu'il 
                    //y a un problème. Des tables manquent (ou des filtres) et ne permettent pas l'ajout de la totalité des filtres
                    //présents dans l'ancien système de données.
                    if ((bHasProblem) && (sFilter == sFirstAdded))
                        break;
                    //bHasProblem est mis à vrai pour indiquer que l'on vient de déplacer un élément.
                    bHasProblem = true;
                    //Si sFirstAdded est vide, cela veut dire que c'est la première fois qu'un filtre est déplacé, (ou alors que le premier
                    //filtre déplacé a finalement été ajouté). On met donc dans cette variable le nom du filtre courant.
                    if (sFirstAdded == "")
                        sFirstAdded = sFilter;
                    //On déplace le filtre et on passe au filtre suivant.
                    lsFilters.Add(sFilter);
                    lsFilters.RemoveAt(i);
                    i--;
                    continue;
                }
                //Si le filtre courant est également le premier filtre qui avait posé problème, alors on remet à "" la variable qui stockait son
                //nom.
                if (sFirstAdded == sFilter)
                    sFilter = "";
                //On remet à FAUX bHasProblem
                bHasProblem = false;
                ///__ Si le filtre est un filtre copie alors on ajoute également son DataTable
                DataTable dtValue = null;
                if (fFilter.copyTable)
                    dtValue = gdData.GetTable(sFilter);
                ///__ Appel de la fonction \ref AddFilter.
                AddFilter(fFilter, dtValue, gdData.getModeVisualisation(sFilter), gdData.getGraphicFilter(sFilter));
            }

            ///Mise à jour des chemin d'accès aux traces passagers et bagages.
            BagTraceLocation = gdData.BagTraceLocation;
            PaxTraceLocation = gdData.PaxTraceLocation;
        }
        #endregion

        #region Pour les graphiques Automod (et leurs tables associées)
        /// <summary>
        /// Permet d'ajouter une table liée à un graphique Automod. Afin d'être capable de visualiser les graphiques automod dans PAX2ISM. il 
        /// permet également de sauvegarder les informations liées et de visualiser sous la forme souhaitée.
        /// </summary>
        /// <param name="dtTable">La table contenant les données du graphique.</param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé.</returns>
        internal bool AddAutomodGraphic(DataTable dtTable)
        {
            ///Appel de la fonction \ref AddAutomodGraphic
            return AddAutomodGraphic(dtTable, GraphicFilter.getGraphicFilter(dtTable));
        }

        /// <summary>
        /// Permet d'ajouter une table liée à un graphique Automod. Afin d'être capable de visualiser les graphiques automod dans PAX2ISM. il 
        /// permet également de sauvegarder les informations liées et de visualiser sous la forme souhaitée.
        /// </summary>
        /// <param name="dtTable">La table contenant les données du graphique.</param>
        /// <param name="Defintion">Définition du graphique qui représente la table.</param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé.</returns>
        internal bool AddAutomodGraphic(DataTable dtTable, GraphicFilter Defintion)
        {
            ///Si la table existe déjà, alors la fonction renvoie FAUX
            if (Exist(dtTable.TableName))
                return false;
            ///Création d'une table du type \ref AutomodGraphicsTable avec les parametres qui avaient été passées à la fonction courante.
            AutomodGraphicsTable ntTable = new AutomodGraphicsTable(dtTable, Defintion);
            ///Appel de la fonction \ref AddTable().
            return AddTable(ntTable);
        }

        /// <summary>
        /// Fonction qui permet de mettre à jour une table Automod avec une nouvelle table.
        /// </summary>
        /// <param name="dtTable">Table contenant les modifications à apporter.</param>
        /// <returns>Booléen indiquant si la mise à jour s'est bien déroulée.</returns>
        internal bool UpdateAutomodGraphic(DataTable dtTable)
        {
            ///Si la table n'existe pas, alors la fonction renvoie FAUX.
            if (!Exist(dtTable.TableName))
                return false;
            ///Si la table qui doit être mise à jour n'est pas du type \ref AutomodGraphicsTable, alors la table renvoie FAUX, Impossible de mettre
            ///à jour cette table car elle n'est pas valide.
            NormalTable ntTmp = GetTable(dtTable.TableName);
            if (ntTmp.GetType() != typeof(AutomodGraphicsTable))
                return false;
            AutomodGraphicsTable agtTable = (AutomodGraphicsTable)ntTmp;
            ///Appel de la fonction \ref AutomodGraphicsTable.UpdateTable() pour mettre à jour la table.
            return agtTable.UpdateTable(dtTable);
        }

        /// <summary>
        /// Fonction qui permet de supprimer du système de données une table Automod.
        /// </summary>
        /// <param name="sTable">Nom de la table à supprimer.</param>
        /// <returns>Booléen indiquant si la suppression s'est bien déroulée.</returns>
        public bool DeleteAutomodGraphic(String sTable)
        {
            ///Si la table n'existe pas, alors la fonction renvoie FAUX.
            if (!Exist(sTable))
                return false;
            ///Si la table n'est pas de type \ref AutomodGraphicsTable, alors la fonction renvoie FAUX.
            NormalTable ntTmp = GetTable(sTable);
            if (ntTmp.GetType() != typeof(AutomodGraphicsTable))
                return false;
            return RemoveTable(sTable);
        }
        #endregion

        #region Pour les Tables

        /// <summary>
        /// Fonction qui permet de mettre à jour la table contenue dans le système de données avec les valeurs contenues dans la nouvelle
        /// table passée en paramètres.
        /// </summary>
        /// <param name="dtTable">Table contenant les mises à jour que l'on souhaite ajouter.</param>
        /// <param name="bCalculatedFromTrace">Booléen qui indique si la table est calculée depuis la trace ou non.</param>
        /// <returns>Booléen indiquant si la mise à jour s'est bien déroulée ou non.</returns>
        internal bool UpdateTable(DataTable dtTable, bool bCalculatedFromTrace)
        {
            ///Si la table hérite de \ref ResultsTable, alors on affecte à la table un booléen indiquant si elle est calculée
            ///à partir de la trace. Et si c'est la cas, on affecte également la fonction qui se chargera de recalculer la 
            ///trace lorsque cela sera nécessaire.
            NormalTable ntTmp = GetTable(dtTable.TableName);
            if (ntTmp is ResultsTable)
            {
                ResultsTable rtTmp = (ResultsTable)ntTmp;
                rtTmp.CalculatedFromTrace = bCalculatedFromTrace && bUseTraceSaving;
                if (bCalculatedFromTrace)
                    rtTmp.OpenTrace = new OpenTraceDelegate(OpenTrace);
                else
                    rtTmp.OpenTrace = null;
            }
            ///Appel de la fonction \ref NormalTable.UpdateTable pour mettre à jour la table.
            return ntTmp.UpdateTable(dtTable);
        }

        /// <summary>
        /// Fonction qui ajoute le \ref NormalTable passé en paramètre au dictionnaire \ref dsntTables des tables.
        /// </summary>
        /// <param name="ntTable">Table à ajouter.</param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé ou non.</returns>
        internal override bool AddTable(NormalTable ntTable)
        {
            ///Si la table passée en paramètre est null, alors la fonction renvoie NULL.
            if (ntTable == null)
                return false;
            ///Appel de la fonction de base \ref DataManagerScenario.AddTable() pour convertir et ajouter la table au scénario courant.
            bool bResult = base.AddTable(ntTable);
            ///Si un problème apparait lors de l'ajout de la table, alors la fonction se stoppe.
            if (!bResult)
                return false;
            NormalTable ntTmp = GetTable(ntTable.Name);
            ///Si la table a ajouter est du type \ref ResultsTable, alors il faut ajouter le délégué pour le calcul de la trace
            ///(uniquement si celle ci est calculée à partir de la trace).
            if (ntTmp is ResultsTable)
            {
                ResultsTable rtTmp = (ResultsTable)ntTmp;
                if (rtTmp.CalculatedFromTrace)
                    rtTmp.OpenTrace = new OpenTraceDelegate(OpenTrace);
            }
            ///Appel de la fonction de base \ref NormalTable.AddTable().
            return true;
        }
        #endregion

        #region Pour les Filtres

        /// <summary>
        /// Fonction qui permet de mettre à jour les filtres dans l'arborescence. Cela permet d'ajouter les filtres aux tables qui en
        /// possèdent.
        /// </summary>
        /// <param name="treeNode">Noeud racine des données contenus dans le système de données courant.</param>
        /// <param name="cmsMenuFilter">Menu contextuel qui doit être ajouté aux filtes ainsi ajoutés.</param>
        internal override void UpdateFilters(System.Windows.Forms.TreeNode treeNode, System.Windows.Forms.ContextMenuStrip cmsMenuFilter, ContextMenuStrip cmsMenuException)
        {
            ///Appel de la fonction de base \ref base.UpdateFilters().
            base.UpdateFilters(treeNode, cmsMenuFilter, cmsMenuException);
        }
        #endregion
    }
    #endregion

    #region public class DataManager_Allocation : DataManagerScenario

    public class DataManagerAllocation : DataManagerScenario
    {
        #region Les différentes variables de la classe.

        /// <summary>
        /// Dictionnaire contenant les paramétrages d'allocation automatique.
        /// </summary>
        Classes.GenerateAllocationTool gatAllocations;

        /// <summary>
        /// Function that allow the user to get the Tables from the input data.
        /// </summary>
        Classes.GenerateAllocationTool.GetTable fGetTableFunction;

        /// <summary>
        /// Function that allow the user to get the Exceptions Tables from the input data.
        /// </summary>
        Classes.GenerateAllocationTool.GetTableException fGetTableExceptionFunction;
        #endregion

        #region Accesseurs
        internal Classes.GenerateAllocationTool AllocationDefinition
        {
            get
            {
                return gatAllocations;
            }
            set
            {
                gatAllocations = value;
                Name = gatAllocations.NomTable;
            }
        }

        /// <summary>
        /// Function that allow the user to get the Tables from the input data.
        /// </summary>
        internal Classes.GenerateAllocationTool.GetTable GetTableFunction
        {
            set
            {
                fGetTableFunction = value;
            }
            get
            {
                return fGetTableFunction;
            }
        }

        /// <summary>
        /// Function that allow the user to get the Exceptions Tables from the input data.
        /// </summary>
        internal Classes.GenerateAllocationTool.GetTableException GetTableExceptionFunction
        {
            set
            {
                fGetTableExceptionFunction = value;
            }
            get
            {
                return fGetTableExceptionFunction;
            }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Consctructeur permettant l'initialisation de  la classe représentant un Scénario parking.
        /// </summary>
        /// <param name="Name">Nom du scénario</param>
        public DataManagerAllocation(String Name)
            : base(Name)
        {
            gatAllocations = null;
        }
        public DataManagerAllocation(Classes.GenerateAllocationTool gatAllocation)
            : base(gatAllocation.NomTable)
        {
            gatAllocations = gatAllocation;
        }

        /// <summary>
        ///  Consctructeur permettant le chargement d'un scénario représenté par un noeud XML.
        /// </summary>
        /// <param name="Node">Noeud XML représentant le scénario.</param>
        public DataManagerAllocation(XmlNode Node)
            : base(Node)
        {
        }

        /// <summary>
        /// Constructeur par copie d'un nouveau \ref DataManagerParking
        /// </summary>
        /// <param name="dmsOldScenario"></param>
        protected DataManagerAllocation(DataManagerAllocation dmsOldScenario)
            : base(dmsOldScenario)
        {
            fGetTableFunction = dmsOldScenario.fGetTableFunction;
            fGetTableExceptionFunction = dmsOldScenario.fGetTableExceptionFunction;

            gatAllocations = new Classes.GenerateAllocationTool(dmsOldScenario.gatAllocations.Save(new XmlDocument()),
                new Classes.GenerateAllocationTool.GetTable(GetTableDelegate),
                new Classes.GenerateAllocationTool.GetTableException(GetTableException));
        }
        #endregion

        #region Fonctions Copy, Dispose et Name
        /// <summary>
        /// Nom du système de données courant.
        /// </summary>
        public override String Name
        {
            get
            {
                ///Renvoie le résultats de \ref base.Name
                return base.Name;
            }
            set
            {
                ///Update the name used by the \ref gatAllocations
                gatAllocations.NomTable = value;
                ///Met à jour le nom pour le \ref DataManagerScenario de base.
                base.Name = value;
            }
        }

        /// <summary>
        /// Permet de rendre la mémoire allouée pour la classe.
        /// </summary>
        internal override void Dispose()
        {
            gatAllocations = null;
            base.Dispose();
        }

        /// <summary>
        /// Fonction permettant la copie du \ref DataManagerParking courant. 
        /// </summary>
        /// <returns>Nouvelle instance de \ref DataManagerParking</returns>
        internal override Object Copy()
        {
            ///Appel du constructeur par copie \ref DataManagerAllocation
            return new DataManagerAllocation(this);
        }
        #endregion

        #region Gestion des TABLES

        /// <summary>
        /// Fonction qui permet de mettre à jour la table contenue dans le système de données avec les valeurs contenues dans la nouvelle
        /// table passée en paramètres.
        /// </summary>
        /// <param name="sTableName">Table contenant les mises à jour que l'on souhaite ajouter.</param>
        /// <param name="bCalculatedFromAllocation">Booléen qui indique si la table est calculée depuis l'allocation.</param>
        /// <returns>Booléen indiquant si la mise à jour s'est bien déroulée ou non.</returns>
        internal bool UpdateTable(String sTableName, bool bCalculatedFromAllocation)
        {
            ///Si la table hérite de \ref ResultsTable, alors on affecte à la table un booléen indiquant si elle est calculée
            ///à partir de la trace. Et si c'est la cas, on affecte également la fonction qui se chargera de recalculer la 
            ///trace lorsque cela sera nécessaire.
            NormalTable ntTmp = GetTable(sTableName);
            if (ntTmp is ResultsTable)
            {
                ResultsTable rtTmp = (ResultsTable)ntTmp;
                rtTmp.CalculatedFromTrace = bCalculatedFromAllocation;
                if (bCalculatedFromAllocation)
                    rtTmp.OpenTrace = new DataManagerPaxBHS.OpenTraceDelegate(OpenTrace);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Fonction qui est appelée pour lancer la génération des allocations.
        /// </summary>
        internal void OpenTrace()
        {
            if (gatAllocations == null)
                return;
            DataTable[] dtResults = null;
            VisualisationMode[] vmResults = null;
            DataTable dtErrors = null;
            DataTable dtStats = null;
            DataTable dtFPI = null;
            List<string> alErrors = new List<string>();
            gatAllocations.Errors = alErrors;

            ///We set the \ref Classes.GenerateAllocationTool.AllocateFlightPlan to false to make sure that while loading and allocating the flight
            ///we will not erase informations already present in the flight plan.
            bool bOldValue = gatAllocations.AllocateFlightPlan;
            gatAllocations.AllocateFlightPlan = false;
            try
            {
                if (gatAllocations.AllocateDesks())
                {
                    dtResults = gatAllocations.TableResultat;
                    vmResults = gatAllocations.AllocationVisualisation;
                    dtErrors = gatAllocations.TableErrors;
                    dtStats = gatAllocations.Statistiques;
                    dtFPI = gatAllocations.FlightPlanInformation;
                }
                if (dtResults == null)
                {
                    String Error = "";
                    if (alErrors.Count > 0)
                        Error = alErrors[0].ToString();

                    OverallTools.ExternFunctions.PrintLogFile("Err00734 : An error occurred during the analysis of the flight plan table " + Error);
                    MessageBox.Show("Err00734 : An error occurred during the analysis of the flight plan table " + Error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gatAllocations.Errors = null;
                    return;
                }
            }
            catch (Exception e)
            {
                dtResults = null;
                vmResults = null;
                gatAllocations.Errors = null;
                MessageBox.Show("Err00733: (Some errors appear during the execution) " + this.GetType().ToString() + " throw an exception: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OverallTools.ExternFunctions.PrintLogFile("Err00733: (Some errors appear during the execution) " + this.GetType().ToString() + " throw an exception: " + e.Message);
                return;
            }
            gatAllocations.AllocateFlightPlan = bOldValue;
            NormalTable ntTmp;
            for (int i = 0; i < dtResults.Length; i++)
            {
                ntTmp = GetTable(dtResults[i].TableName);
                if (ntTmp != null)
                {
                    ntTmp.UpdateTable(dtResults[i]);
                    if (vmResults != null)
                    {
                        ntTmp.Mode = vmResults[i];
                    }
                }
            }
            if (dtErrors != null)
            {
                ntTmp = GetTable(dtErrors.TableName);
                if (ntTmp != null)
                {
                    ntTmp.UpdateTable(dtErrors);
                }
            }
            if (dtStats != null)
            {
                ntTmp = GetTable(dtStats.TableName);
                if (ntTmp != null)
                {
                    ntTmp.UpdateTable(dtStats);
                }
            }
            if (dtFPI != null)
            {
                ntTmp = GetTable(dtFPI.TableName);
                if (ntTmp != null)
                {
                    ntTmp.UpdateTable(dtFPI);
                }
            }
        }

        /// <summary>
        /// Fonction qui ajoute le \ref NormalTable passé en paramètre au dictionnaire \ref dsntTables des tables.
        /// </summary>
        /// <param name="ntTable">Table à ajouter.</param>
        /// <returns>Booléen indiquant si l'ajout s'est bien déroulé ou non.</returns>
        internal override bool AddTable(NormalTable ntTable)
        {
            ///Si la table passée en paramètre est null, alors la fonction renvoie NULL.
            if (ntTable == null)
                return false;
            ///Appel de la fonction de base \ref DataManagerScenario.AddTable() pour convertir et ajouter la table au scénario courant.
            bool bResult = base.AddTable(ntTable);
            ///Si un problème apparait lors de l'ajout de la table, alors la fonction se stoppe.
            if (!bResult)
                return false;
            NormalTable ntTmp = GetTable(ntTable.Name);
            ///Si la table a ajouter est du type \ref ResultsTable, alors il faut ajouter le délégué pour le calcul de la trace
            ///(uniquement si celle ci est calculée à partir de la trace).
            if (ntTmp is ResultsTable)
            {
                ResultsTable rtTmp = (ResultsTable)ntTmp;
                if (rtTmp.CalculatedFromTrace)
                    rtTmp.OpenTrace = new DataManagerPaxBHS.OpenTraceDelegate(OpenTrace);
            }
            ///Appel de la fonction de base \ref NormalTable.AddTable().
            return true;
        }

        #endregion

        #region Pour ouvrir et sauver les donnees de DataManagerScenario
        /// <summary>
        /// Fonction qui permet de chargement des données à partir d'un noeud XML.
        /// </summary>
        /// <param name="xmlElement">Noeud XML représentant le \ref DataManagerParking</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (afin de gérer les modifications dans les informations stockées).</param>
        /// <param name="sRootDirectory">Le dossier où se trouvent le fichier .pax (dossier racine du projet).</param>
        /// <param name="chForm">La fenêtre de chargement pour pouvoir faire progresser l'état du chargement.</param>
        /// <returns>Booléen indiquant si l'ouverture s'est bien déroulée ou non.</returns>
        internal override bool LoadInputData(XmlElement xmlElement, VersionManager vmVersion, String sRootDirectory, Prompt.SIM_LoadingForm chForm)
        {

            ///Si le noeud XML ne contient pas de noeud DataManager, alors renvoyer FAUx (Erreur dans le fichier)
            if (!OverallTools.FonctionUtiles.hasNamedChild(xmlElement, "DataManagerScenario"))
                return false;
            ///Appeler la fonction \ref DataManagerScenario.LoadInputData
            if (!base.LoadInputData(xmlElement["DataManagerScenario"], vmVersion, sRootDirectory, chForm))
                return false;

            if (!OverallTools.FonctionUtiles.hasNamedChild(xmlElement, "GenerateAllocationTool"))
                return false;
            gatAllocations = new Classes.GenerateAllocationTool(xmlElement["GenerateAllocationTool"],
                new Classes.GenerateAllocationTool.GetTable(GetTableDelegate),
                new Classes.GenerateAllocationTool.GetTableException(GetTableException));

            return true;
        }
        /// <summary>
        /// Fonction qui s'occupe de générer un noeud et de sauvegarder la totalité des tables présentes dans le système de données.
        /// </summary>
        /// <param name="projet">Projet XML auquel tous les noeuds XML doivent être liés.</param>
        /// <returns></returns>
        internal override XmlElement Save(XmlDocument projet, string sRootDirectory, string sOldRootDirectory, string sSavingDirectory, Prompt.SIM_LoadingForm chForm)
        {
            ///Création d'un noeud XML "DataManagerPaxBHS" qui regroupera toutes les informations pour cette classe.
            XmlElement arbreFiltres = projet.CreateElement("DataManagerAllocation");
            ///Ajout de l'attribut "Name" avec le \ref sName 
            arbreFiltres.SetAttribute("Name", sName);

            ///Appel de la fonction de base \ref DataManagerScenario.Save pour sauvegarder l'ensemble des tables qui sont stockées dans la parties \ref DataManagerScenario.
            XmlElement xnBase = base.Save(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory, chForm);
            if (xnBase != null)
                arbreFiltres.AppendChild(xnBase);
            if (gatAllocations != null)
            {
                XmlElement xeTmp = gatAllocations.Save(projet);
                if (xeTmp != null)
                    arbreFiltres.AppendChild(xeTmp);
            }
            ///Renvoie du Noeud XML représentant ce \ref DataManagerPaxBHS
            return arbreFiltres;
        }
        #endregion

        #region Pour les allocations

        /// <summary>
        /// Fonction déléguée permettant de récupérer une Table à partir de son nom (utilisé pour les allocations)
        /// </summary>
        /// <param name="Name">Nom de la table recherchée</param>
        /// <returns>DataTable ou null/returns>
        private DataTable GetTableDelegate(String sTableName)
        {
            if (GetTableFunction == null)
                return null;
            return GetTableFunction(sTableName);
        }

        /// <summary>
        /// Fonction permettant de récupérer le \ref ExceptionTables associé à la table passée en parametre (utilisé pour les allocations)
        /// </summary>
        /// <param name="sTableName">Nom de la table recherchée</param>
        /// <returns>La table recherchée ou NULL si celle ci n'existe pas.</returns>
        internal ExceptionTable GetTableException(String sTableName)
        {
            if (GetTableExceptionFunction == null)
                return null;
            return GetTableExceptionFunction(sTableName);
        }
        #endregion

        /// <summary>
        /// Function that will generate a review for the given class. That will allow the user to create
        /// a note with these informations to keep track of the informations use for the Scenario.
        /// </summary>
        /// <param name="bPrintPDF"></param>
        /// <returns></returns>
        internal List<string> getReview(bool bPrintPDF)
        {
            List<string> lsResult = new List<string>();
            try
            {
                lsResult.AddRange(gatAllocations.getReview(bPrintPDF));
            }
            catch (Exception e)
            {
                OverallTools.ExternFunctions.PrintLogFile("Execpt00332 : Unable to generate the review for Scenario Allocation " + Name + "." + e.Message);
                lsResult.Add("#### Problem while trying to generate the review");
            }
            return lsResult;
        }
    }
    #endregion

    #region public class DataManager_XXX : DataManagerScenario
    ///La classe suivante est la structure principale de toute nouvelle classe qui pourrait être créée en dessous
    ///de la classe DataManager (et plus particulièrement DataManagerScenario). 
    /*public class DataManager_XXX : DataManagerScenario
    {
        #region Les différentes variables de la classe.

        #endregion

        #region Constructeurs
        /// <summary>
        /// Consctructeur permettant l'initialisation de  la classe représentant un Scénario parking.
        /// </summary>
        /// <param name="Name">Nom du scénario</param>
        public DataManagerParking(String Name)
            : base(Name)
        {
        }

        /// <summary>
        ///  Consctructeur permettant le chargement d'un scénario représenté par un noeud XML.
        /// </summary>
        /// <param name="Node">Noeud XML représentant le scénario.</param>
        public DataManagerParking(XmlNode Node)
            : base(Node)
        {
        }

        /// <summary>
        /// Constructeur par copie d'un nouveau \ref DataManagerParking
        /// </summary>
        /// <param name="dmsOldScenario"></param>
        protected DataManagerParking(DataManagerParking dmsOldScenario)
            : base(dmsOldScenario)
        {
        }
        #endregion

        #region Fonctions Copy, Dispose et Name
        /// <summary>
        /// Nom du système de données courant.
        /// </summary>
        public override String Name
        {
            get
            {
                ///Renvoie le résultats de \ref base.Name
                return base.Name;
            }
            set
            {
                ///Met à jour le nom pour le \ref DataManagerScenario de base.
                base.Name = value;
            }
        }

        /// <summary>
        /// Permet de rendre la mémoire allouée pour la classe.
        /// </summary>
        internal override void Dispose()
        {
            base.Dispose();
        }

        /// <summary>
        /// Fonction permettant la copie du \ref DataManagerParking courant. 
        /// </summary>
        /// <returns>Nouvelle instance de \ref DataManagerParking</returns>
        internal override Object Copy()
        {
            ///Appel du constructeur par copie \ref DataManagerParking
            return new DataManagerParking(this);
        }
        #endregion

        #region Pour ouvrir et sauver les donnees de DataManagerScenario
        /// <summary>
        /// Fonction qui permet de chargement des données à partir d'un noeud XML.
        /// </summary>
        /// <param name="xmlElement">Noeud XML représentant le \ref DataManagerParking</param>
        /// <param name="vmVersion">Version d'enregistrement du projet (afin de gérer les modifications dans les informations stockées).</param>
        /// <param name="sRootDirectory">Le dossier où se trouvent le fichier .pax (dossier racine du projet).</param>
         /// <param name="chForm">La fenêtre de chargement pour pouvoir faire progresser l'état du chargement.</param>
        /// <returns>Booléen indiquant si l'ouverture s'est bien déroulée ou non.</returns>
        internal virtual bool LoadInputData(XmlElement xmlElement, VersionManager vmVersion, String sRootDirectory, Prompt.SIM_LoadingForm chForm)
        {

            ///Si le noeud XML ne contient pas de noeud DataManager, alors renvoyer FAUx (Erreur dans le fichier)
            if (!OverallTools.FonctionUtiles.hasNamedChild(xmlElement, "DataManagerParking"))
                return false;
            ///Appeler la fonction \ref DataManagerScenario.LoadInputData
            if (!base.LoadInputData(xmlElement["DataManagerParking"], vmVersion, sRootDirectory))
                return false;

            return true;
        }
        /// <summary>
        /// Fonction qui s'occupe de générer un noeud et de sauvegarder la totalité des tables présentes dans le système de données.
        /// </summary>
        /// <param name="projet">Projet XML auquel tous les noeuds XML doivent être liés.</param>
        /// <returns></returns>
        internal override XmlElement Save(XmlDocument projet, string sRootDirectory, string sOldRootDirectory, string sSavingDirectory)
        {
            ///Création d'un noeud XML "DataManagerPaxBHS" qui regroupera toutes les informations pour cette classe.
            XmlElement arbreFiltres = projet.CreateElement("DataManagerParking");
            ///Ajout de l'attribut "Name" avec le \ref sName 
            arbreFiltres.SetAttribute("Name", sName);

            ///Appel de la fonction de base \ref DataManagerScenario.Save pour sauvegarder l'ensemble des tables qui sont stockées dans la parties \ref DataManagerScenario.
            XmlElement xnBase = base.Save(projet, sRootDirectory, sOldRootDirectory, sSavingDirectory);
            if (xnBase != null)
                arbreFiltres.AppendChild(xnBase);
            ///Renvoie du Noeud XML représentant ce \ref DataManagerPaxBHS
            return arbreFiltres;
        }
        #endregion

    }*/
    #endregion
    #endregion
}
