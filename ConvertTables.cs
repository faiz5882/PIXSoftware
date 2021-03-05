using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using SIMCORE_TOOL.Classes;
using SIMCORE_TOOL.com.crispico.backwardsCompatibility;

namespace SIMCORE_TOOL
{
    class ConvertTables
    {
        public static ConvertTables Singleton = null;
        public static ConvertTables getConvertTable
        {
            get
            {
                if (Singleton == null)
                    Singleton = new ConvertTables();
                return Singleton;
            }
        }
        #region Class VersionMaster
        /// <summary>
        /// The VersionMaster class allowed to save quickly the changes for the saving version.
        /// It allows to define the different table that had changed between 2 versions.
        /// 
        /// </summary>
        private class VersionMaster
        {
            #region Variables for the VersionMaster class
            /// <summary>
            /// Version represented by the instance of the class.
            /// </summary>
            VersionManager vmCurrentVersion;
            /// <summary>
            /// List of the tables that have changed in the selected version.
            /// </summary>
            Dictionary<String, ModifiedTable> dTablesModification;
            #endregion

            #region Accesseurs
            internal VersionManager CurrentVersion
            {
                get
                {
                    return vmCurrentVersion;
                }
            }
            
            
            #endregion
            
            #region Class ModifiedTable
            /// <summary>
            /// The modifiedTable class permits to define the changes that would occures on a 
            /// table for a given Version. 
            /// The allowed changes are :
            ///  - Changing the name of the table.
            ///  - Changing the columns (number, order, names, content) of the table
            ///  - Changing the rows of the table (Default values, names, ...)
            /// </summary>
            internal class ModifiedTable
            {
                #region Variables for the ModifiedTable class
                private String sNewTableName;
                private String sOldTableName;
                internal GlobalNames.ConvertClass ConvertClass;

                #region Les modification des colonnes pour les tables données.
                private String[] tsOldColumns;
                private Type[] ttOldColumnsTypes;
                private Int32[] tiOldPrimaryKeys;

                private String[] tsNewColumns;
                private Type[] ttNewColumnsTypes;
                private Int32[] tiNewPrimaryKeys;

                private String[] tsLinksColumns;

                private Object[] toDefaultValuesColumns;

                private string[] tsNewRows;
                private string[] tsOldRows;
                private string[] tsLinksRows;
                private Object[] tdDefaultValueRows;
                #endregion
                #endregion

                #region Accesseurs
                internal String OldTableName
                {
                    get
                    {
                        return sOldTableName;
                    }
                }
                internal String NewTableName
                {
                    get
                    {
                        return sNewTableName;
                    }
                }

                internal String[] OldColumns
                {
                    get
                    {
                        return tsOldColumns;
                    }
                }
                internal Type[] OldColumnType
                {
                    get
                    {
                        return ttOldColumnsTypes;
                    }
                }
                internal Int32[] OldPrimaryKeys
                {
                    get
                    {
                        return tiOldPrimaryKeys;
                    }
                }

                internal String[] NewColumns
                {
                    get
                    {
                        return tsNewColumns;
                    }
                }
                internal Type[] NewColumnType
                {
                    get
                    {
                        return ttNewColumnsTypes;
                    }
                }
                internal Int32[] NewPrimaryKeys
                {
                    get
                    {
                        return tiNewPrimaryKeys;
                    }
                }

                internal String[] LinksColumns
                {
                    get
                    {
                        return tsLinksColumns;
                    }
                }
                internal Object[] DefaultValuesColumns
                {
                    get
                    {
                        return toDefaultValuesColumns;
                    }
                }

                internal String[] NewLines
                {
                    get
                    {
                        return tsNewRows;
                    }
                }
                internal String[] OldLines
                {
                    get
                    {
                        return tsOldRows;
                    }
                }
                internal string[] LinksRows
                {
                    get
                    {
                        return tsLinksRows;
                    }
                }
                internal Object[] DefaultValuesLines
                {
                    get
                    {
                        return tdDefaultValueRows;
                    }
                }

                internal Boolean isNameChanged
                {
                    get
                    {
                        return (sNewTableName != null);

                    }
                }
                internal Boolean isColumnsChanged
                {
                    get
                    {
                        return (tsNewColumns != null);

                    }
                }
                internal Boolean isRowsChanged
                {
                    get
                    {
                        return (tsNewRows != null);

                    }
                }
                internal Boolean isConverted
                {
                    get
                    {
                        return (ConvertClass != null);
                    }
                }
                internal GlobalNames.ConvertClass Convert
                {
                    get
                    {
                        return ConvertClass;
                    }
                }
                #endregion

                #region Constructeurs
                public ModifiedTable(String sTableName)
                {
                    sOldTableName = sTableName;
                    sNewTableName = null;

                    tsOldColumns = null;
                    ttOldColumnsTypes = null;
                    tiOldPrimaryKeys = null;

                    tsNewColumns = null;
                    ttNewColumnsTypes = null;
                    tiNewPrimaryKeys = null;

                    tsLinksColumns = null;

                    toDefaultValuesColumns = null;

                    tsNewRows = null;
                    tsOldRows = null;
                    tdDefaultValueRows = null;
                    ConvertClass = null;
                }
                #endregion

                #region Functions to add a new definition of change for a table
                public void setNewName(String NewName)
                {
                    sNewTableName = NewName;
                }
                public void setOldColumns(String[] Columns, Type[] Types, Int32[] PrimaryKeys)
                {
                    tsOldColumns = Columns;
                    ttOldColumnsTypes = Types;
                    tiOldPrimaryKeys = PrimaryKeys;
                }
                public void setNewColumns(String[] Columns, Type[] Types, Int32[] PrimaryKeys, String[] Links, Object[] DefaultValues)
                {
                    tsNewColumns = Columns;
                    ttNewColumnsTypes = Types;
                    tiNewPrimaryKeys = PrimaryKeys;

                    tsLinksColumns = Links;
                    toDefaultValuesColumns = DefaultValues;
                }
                public void setOldRows(String[] Rows)
                {
                    tsOldRows = Rows;
                }
                public void setNewRows(String[] Rows, String[] Links, Object[] DefaultValues)
                {
                    tsNewRows = Rows;
                    tdDefaultValueRows = DefaultValues;
                    tsLinksRows = Links;
                }
                internal void setTableConvertModifier(GlobalNames.ConvertClass Conversion)
                {
                    ConvertClass = Conversion;
                }
                #endregion
            }
            #endregion

            #region Constructeurs
            public VersionMaster(VersionManager vmVersion)
            {
                vmCurrentVersion = new VersionManager(vmVersion);
                dTablesModification = null;
            }
            #endregion

            #region Functions to check if the table has been modified in the current version.
            public Boolean isModifiedTable(String sTableName)
            {
                if (dTablesModification==null)
                    return false;
                return dTablesModification.ContainsKey(sTableName);
            }
            public Boolean isModifiedTableCheckNew(String sTableName)
            {
                return (getModifiedTableCheckNew(sTableName) != null);
            }
            internal ModifiedTable getModifiedTable(String sTableName)
            {
                if (!isModifiedTable(sTableName))
                    return null;
                return dTablesModification[sTableName];
            }
            internal ModifiedTable getModifiedTableCheckNew(String sTableName)
            {
                if (dTablesModification == null)
                    return null;
                foreach (ModifiedTable mtTables in dTablesModification.Values)
                {
                    if (mtTables.NewTableName == sTableName)
                        return mtTables;
                }
                return null;
            }
            #endregion

            #region Function to add a new definition of change for a table.
            private Boolean AddModifiedTable(ModifiedTable mtModification)
            {
                if (mtModification == null)
                    return false;
                if (dTablesModification == null)
                    dTablesModification = new Dictionary<string, ModifiedTable>();
                if (dTablesModification == null)
                    return false;
                if (dTablesModification.ContainsKey(mtModification.OldTableName))
                    return false;
                dTablesModification.Add(mtModification.OldTableName, mtModification);
                return true;
            }
            #endregion

            #region Functions to add new definition of change for a table.
            public Boolean setTableNameModified(String sTableName, String sNewTableName)
            {
                if ((sTableName == null) || (sNewTableName == null))
                    return false;
                ModifiedTable mtTable = getModifiedTable(sTableName);
                if (mtTable == null)
                {
                    mtTable = new ModifiedTable(sTableName);
                    if (!AddModifiedTable(mtTable))
                        return false;
                }
                mtTable.setNewName(sNewTableName);
                return true;
            }
            public Boolean setTableColumnsModified(String sTableName, String[] OldColumns, Type[] OldTypes, Int32[] OldPrimaryKeys,
                                                String[] NewColumns, Type[] NewTypes, Int32[] NewPrimaryKeys, String[] Links, Object[] DefaultValues)
            {
                if (sTableName == null)
                    return false;
                ModifiedTable mtTable = getModifiedTable(sTableName);
                if (mtTable == null)
                {
                    mtTable = new ModifiedTable(sTableName);
                    if (!AddModifiedTable(mtTable))
                        return false;
                }
                mtTable.setOldColumns(OldColumns, OldTypes, OldPrimaryKeys);
                mtTable.setNewColumns(NewColumns, NewTypes, NewPrimaryKeys, Links, DefaultValues);
                return true;
            }
            public Boolean setTableRowsModified(String sTableName, String[] OldRows, String[] NewRows, String[] Links, Object[] DefaultValues)
            {
                if (sTableName == null)
                    return false;
                ModifiedTable mtTable = getModifiedTable(sTableName);
                if (mtTable == null)
                {
                    mtTable = new ModifiedTable(sTableName);
                    if (!AddModifiedTable(mtTable))
                        return false;
                }
                mtTable.setOldRows(OldRows);
                mtTable.setNewRows(NewRows, Links, DefaultValues);
                return true;
            }

            internal Boolean setTableConvertModifier(String sTableName, GlobalNames.ConvertClass Conversion)
            {
                if (sTableName == null)
                    return false;
                ModifiedTable mtTable = getModifiedTable(sTableName);
                if (mtTable == null)
                {
                    mtTable = new ModifiedTable(sTableName);
                    if (!AddModifiedTable(mtTable))
                        return false;
                }
                mtTable.setTableConvertModifier(Conversion);
                return true;
            }
            #endregion
        }
        #endregion

        #region Variables for the ConvertTables class
        /// <summary>
        /// Contains the index of the first Version contains in the definition of the ConvertTables class
        /// </summary>
        VersionManager vmFirstVersion;
        /// <summary>
        /// Contains the index of the last Version contains in the definition of the ConvertTables class
        /// </summary>
        VersionManager vmLastVersion;
        /// <summary>
        /// Contains all the different changes in the saving versions.
        /// </summary>
        Dictionary<String, VersionMaster> dVersions;
        #endregion

        #region Constructeurs

        private ConvertTables()
        {
            dVersions = new Dictionary<String, VersionMaster>();
            VersionManager vmVersion = new VersionManager(1, 15);
            vmFirstVersion = new VersionManager(vmVersion);
            VersionMaster vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);

            vmMaster.setTableColumnsModified(GlobalNames.FPDTableName,
                GlobalNames.listeEnteteFPDTable_1_15, GlobalNames.listeTypeEnteteFPDTable_1_15, GlobalNames.listePrimaryKeyFPDTable_1_15,
                GlobalNames.ListeEntete_NewFPDTable_1_15, GlobalNames.ListeTypeEntete_NewFPDTable_1_15, GlobalNames.ListePrimaryKey_NewFPDTable_1_15, GlobalNames.listeEntete_LinksFPDTable_1_15, GlobalNames.ListeDefault_NewFPDTable_1_15);

            vmMaster.setTableColumnsModified(GlobalNames.FPATableName,
                GlobalNames.listeEnteteFPATable_1_15, GlobalNames.listeTypeEnteteFPATable_1_15, GlobalNames.listePrimaryKeyFPATable_1_15,
                GlobalNames.ListeEntete_NewFPATable_1_15, GlobalNames.ListeTypeEntete_NewFPATable_1_15, GlobalNames.ListePrimaryKey_NewFPATable_1_15, GlobalNames.ListeEntete_LinksFPATable_1_15, GlobalNames.ListeDefault_NewFPATable_1_15);


            vmVersion = new VersionManager(1, 16);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);
            vmMaster.setTableColumnsModified(GlobalNames.FP_AirlineCodesTableName,
                GlobalNames.ListeEntete_FP_AirlineCodesTable_1_16, GlobalNames.ListeTypeEntete_FP_AirlineCodesTable_1_16, GlobalNames.ListePrimaryKey_FP_AirlineCodesTable_1_16,
                GlobalNames.ListeEntete_NewFP_AirlineCodesTable_1_16, GlobalNames.ListeTypeEntete_NewFP_AirlineCodesTable_1_16, GlobalNames.ListePrimaryKey_NewFP_AirlineCodesTable_1_16, GlobalNames.ListeEntete_LinksFP_AirlineCodesTable_1_16, null);

            vmMaster.setTableNameModified(GlobalNames.OCT_MakeUpTableName_1_16, GlobalNames.OCT_MakeUpTableName);

            /*vmMaster.setTableRowsModified(GlobalNames.AllocationRulesTableName, GlobalNames.ListeOldLignesAllocationRulesTable_1_16,
                GlobalNames.ListeLignesAllocationRulesTable_1_16, GlobalNames.ListeLinksAllocationRulesTable_1_16, (object[])GlobalNames.Default_AllocationRulesTable_1_16);
            */
            vmMaster.setTableRowsModified(GlobalNames.OCT_MakeUpTableName, GlobalNames.ListeOldLignesAllocationRulesTable_1_16,
              GlobalNames.ListeLignesAllocationRulesTable_1_16, GlobalNames.ListeLinksAllocationRulesTable_1_16, (object[])GlobalNames.Default_AllocationRulesTable_1_16);
            int i;
            String sPrefixeTable;
            String sTableName;
            for (i = 1; i < GestionDonneesHUB2SIM.NbTerminaux; i++)
            {
                sPrefixeTable = GlobalNames.sBHS_PrefixeLong + i.ToString() + "_";
                sTableName = sPrefixeTable + GlobalNames.sBHS_Process;
                vmMaster.setTableRowsModified(sTableName, GlobalNames.Liste_Old_LignesProcess_BHS_1_16, GlobalNames.Liste_LignesProcess_BHS_1_16,
                    GlobalNames.ListeLinksLignesProcess_BHS_1_16, null);
            }

            vmVersion = new VersionManager(1, 17);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);
            vmMaster.setTableColumnsModified(GlobalNames.FPDTableName,
                GlobalNames.ListeEntete_NewFPDTable_1_15, GlobalNames.ListeTypeEntete_NewFPDTable_1_15, GlobalNames.ListePrimaryKey_NewFPDTable_1_15,
                GlobalNames.ListeEntete_NewFPDTable_1_17, GlobalNames.ListeTypeEntete_NewFPDTable_1_17, GlobalNames.ListePrimaryKey_NewFPDTable_1_17, GlobalNames.ListeEntete_LinksFPDTable_1_17, null);
            vmMaster.setTableColumnsModified(GlobalNames.FPATableName,
                GlobalNames.ListeEntete_NewFPATable_1_15, GlobalNames.ListeTypeEntete_NewFPATable_1_15, GlobalNames.ListePrimaryKey_NewFPATable_1_15,
                GlobalNames.ListeEntete_NewFPATable_1_17, GlobalNames.ListeTypeEntete_NewFPATable_1_17, GlobalNames.ListePrimaryKey_NewFPATable_1_17, GlobalNames.ListeEntete_LinksFPATable_1_17, null);


            vmVersion = new VersionManager(1, 18);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);
            vmMaster.setTableColumnsModified(GlobalNames.FP_AircraftTypesTableName,
                GlobalNames.ListeEntete_FP_AircraftTypesTable_1_18, GlobalNames.ListeTypeEntete_FP_AircraftTypesTable_1_18, GlobalNames.ListePrimaryKey_FP_AircraftTypesTable_1_18,
                GlobalNames.ListeEntete_NewFP_AircraftTypesTable_1_18, GlobalNames.ListeTypeEntete_NewFP_AircraftTypesTable_1_18, GlobalNames.ListePrimaryKey_NewFP_AircraftTypesTable_1_18, GlobalNames.ListeEntete_LinksFP_AircraftTypesTable_1_18, GlobalNames.ListeDefault_NewFP_AircraftTypesTable_1_18);

            vmVersion = new VersionManager(1, 19);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);

            vmVersion = new VersionManager(1, 20);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);
            for (i = 1; i < GestionDonneesHUB2SIM.NbTerminaux; i++)
            {
                sPrefixeTable = GlobalNames.sBHS_PrefixeLong + i.ToString() + "_";
                sTableName = sPrefixeTable + GlobalNames.sBHS_Process;
                if (PAX2SIM.bDelhi)
                {
                    vmMaster.setTableRowsModified(sTableName, GlobalNames.ListeLignesProcess_BHS_Old_Delhi_1_20, GlobalNames.ListeLignesProcess_BHS_New_Delhi_1_20,
                        GlobalNames.ListeLinksLignesProcess_BHS_Delhi_1_20, GlobalNames.ListeValuesProcess_BHS_Delhi_1_20);
                }
                else
                {
                    vmMaster.setTableRowsModified(sTableName, GlobalNames.Liste_LignesProcess_BHS_1_16, GlobalNames.ListeLignesProcess_BHS_New_1_20,
                        GlobalNames.ListeLinksLignesProcess_BHS_1_20, GlobalNames.ListeValuesProcess_BHS_1_20);
                }
            }


            vmVersion = new VersionManager(1, 21);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);

            vmMaster.setTableRowsModified(GlobalNames.OCT_BaggageClaimTableName, GlobalNames.ListeLignesOCT_BaggageClaim_1_21, GlobalNames.ListeLignesOCT_BaggageClaim_New_1_21,
                GlobalNames.ListeLinksLignesOCT_BaggageClaim_1_21, GlobalNames.ListeValuesOCT_BaggageClaim_1_21);


            vmVersion = new VersionManager(1, 22);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);
            vmMaster.setTableColumnsModified(GlobalNames.FPDTableName,
               GlobalNames.ListeEntete_NewFPDTable_1_17, GlobalNames.ListeTypeEntete_NewFPDTable_1_17, GlobalNames.ListePrimaryKey_NewFPDTable_1_17,
               GlobalNames.ListeEntete_NewFPDTable_1_22, GlobalNames.ListeTypeEntete_NewFPDTable_1_22, GlobalNames.ListePrimaryKey_NewFPDTable_1_22, GlobalNames.listeEntete_LinksFPDTable_1_22, null);
            for (i = 1; i < GestionDonneesHUB2SIM.NbTerminaux; i++)
            {
                sPrefixeTable = GlobalNames.sBHS_PrefixeLong + i.ToString() + "_";
                sTableName = sPrefixeTable + GlobalNames.sBHS_Process;

                vmMaster.setTableRowsModified(sTableName, GlobalNames.ListeLignesProcess_BHS_New_1_20, GlobalNames.ListeLignesProcess_BHS_New_1_22,
                    GlobalNames.ListeLignesProcess_BHS_1_22, GlobalNames.ListeValuesProcess_BHS_1_22);
            }
            vmVersion = new VersionManager(1, 23);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);
            vmMaster.setTableColumnsModified(GlobalNames.Animated_QueuesName,
               GlobalNames.ListeEntete_Old_Animated_QueuesTable_1_23, GlobalNames.ListeTypeEntete_Old_Animated_QueuesTable_1_23, GlobalNames.ListePrimaryKey_Old_Animated_QueuesTable_1_23,
               GlobalNames.ListeEntete_New_Animated_QueuesTable_1_23, GlobalNames.ListeTypeEntete_New_Animated_QueuesTable_1_23, GlobalNames.ListePrimaryKey_New_Animated_QueuesTable_1_23, GlobalNames.listeEntete_LinksAnimated_QueuesTable_1_23, GlobalNames.ListeDefault_NewFPDTable_1_23);


            vmVersion = new VersionManager(1, 24);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);
            vmMaster.setTableColumnsModified(GlobalNames.FPDTableName,
               GlobalNames.ListeEntete_NewFPDTable_1_22, GlobalNames.ListeTypeEntete_NewFPDTable_1_22, GlobalNames.ListePrimaryKey_NewFPDTable_1_22,
               GlobalNames.ListeEntete_New_FPDTable_1_24, GlobalNames.ListeTypeEntete_New_FPDTable_1_24, GlobalNames.ListePrimaryKey_New_FPDTable_1_24, GlobalNames.listeEntete_LinksFPDTable_1_24, GlobalNames.ListeDefault_New_FPDTable_1_24);
            vmMaster.setTableColumnsModified(GlobalNames.FPATableName,
                GlobalNames.ListeEntete_NewFPATable_1_17, GlobalNames.ListeTypeEntete_NewFPATable_1_17, GlobalNames.ListePrimaryKey_NewFPATable_1_17,
                GlobalNames.ListeEntete_New_FPATable_1_24, GlobalNames.ListeTypeEntete_New_FPATable_1_24, GlobalNames.ListePrimaryKey_New_FPATable_1_24, GlobalNames.listeEntete_LinksFPATable_1_24, GlobalNames.ListeDefault_New_FPATable_1_24);
            vmMaster.setTableColumnsModified(GlobalNames.BagPlanName,
                           GlobalNames.ListeEntete_Old_BagPlan_1_24, GlobalNames.ListeTypeEntete_Old_BagPlan_1_24, GlobalNames.ListePrimaryKey_Old_BagPlan_1_24,
                           GlobalNames.ListeEntete_New_BagPlan_1_24, GlobalNames.ListeTypeEntete_New_BagPlan_1_24, GlobalNames.ListePrimaryKey_New_BagPlan_1_24, GlobalNames.listeEntete_LinksBagPlan_1_24, GlobalNames.ListeDefault_New_BagPlan_1_24);
            vmMaster.setTableRowsModified(GlobalNames.Baggage_Claim_ConstraintName, GlobalNames.ListeLignesBaggage_Claim_Constraint_1_24, GlobalNames.ListeLignesBaggage_Claim_Constraint_New_1_24,
                GlobalNames.ListeLignesBaggage_Claim_Constraint_1_24, GlobalNames.ListeValuesBaggage_Claim_Constraint_1_24);

            vmVersion = new VersionManager(1, 25);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);
            vmMaster.setTableColumnsModified(GlobalNames.FPDTableName,
                GlobalNames.ListeEntete_New_FPDTable_1_24, GlobalNames.ListeTypeEntete_New_FPDTable_1_24, GlobalNames.ListePrimaryKey_New_FPDTable_1_24,
                GlobalNames.ListeEntete_New_FPDTable_1_25, GlobalNames.ListeTypeEntete_New_FPDTable_1_25, GlobalNames.ListePrimaryKey_New_FPDTable_1_25, GlobalNames.listeEntete_LinksFPDTable_1_25, GlobalNames.ListeDefault_New_FPDTable_1_25);

            vmMaster.setTableColumnsModified(GlobalNames.FPATableName,
                GlobalNames.ListeEntete_New_FPATable_1_24, GlobalNames.ListeTypeEntete_New_FPATable_1_24, GlobalNames.ListePrimaryKey_New_FPATable_1_24,
                GlobalNames.ListeEntete_New_FPATable_1_25, GlobalNames.ListeTypeEntete_New_FPATable_1_25, GlobalNames.ListePrimaryKey_New_FPATable_1_25, GlobalNames.listeEntete_LinksFPATable_1_25, GlobalNames.ListeDefault_New_FPATable_1_25);



            vmMaster.setTableColumnsModified(GlobalNames.Pax_GenTransferLog,
               GlobalNames.ListeEntete_PaxGenTransferLog_Old_1_25, GlobalNames.ListeTypeEntete_PaxGenTransferLog_Old_1_25, null,
               GlobalNames.ListeEntete_PaxGenTransferLog_New_1_25, GlobalNames.ListeTypeEntete_PaxGenTransferLog_New_1_25, null, GlobalNames.ListeLinks_PaxGenTransferLog_New_1_25, null);

            vmMaster.setTableColumnsModified(GlobalNames.NbTrolleyTableName,
                           GlobalNames.ListeEntete_NbTrolleyTable_Old_1_25, GlobalNames.ListeTypeEntete_NbTrolleyTable_Old_1_25, GlobalNames.ListePrimaryKey_NbTrolleyTable_Old_1_25,
                           GlobalNames.ListeEntete_NbTrolleyTable_New_1_25, GlobalNames.ListeTypeEntete_NbTrolleyTable_New_1_25, GlobalNames.ListePrimaryKey_NbTrolleyTable_New_1_25, GlobalNames.ListeLinksEntete_NbTrolleyTable_1_25, null);

            vmMaster.setTableRowsModified(GlobalNames.TransferInfeedAllocationRulesTableName, GlobalNames.ListeLignesTransferInfeedAllocationRulesTable_Old_1_25, GlobalNames.ListeLignesTransferInfeedAllocationRulesTable_New_1_25,
                GlobalNames.ListeLinksTransferInfeedAllocationRulesTable_1_25, GlobalNames.Default_TransferInfeedAllocationRulesTable_1_25);

            vmMaster.setTableRowsModified(GlobalNames.OCT_MakeUpTableName, GlobalNames.ListeLignesAllocationRulesTable_1_16, GlobalNames.ListeLignesAllocationRulesTable_1_25,
               GlobalNames.ListeLinksAllocationRulesTable_1_25, GlobalNames.Default_AllocationRulesTable_1_25);


            vmMaster.setTableRowsModified(GlobalNames.FPD_LoadFactorsTableName, GlobalNames.ListeLignesFPD_LF_1_25, GlobalNames.ListeLignesFPD_LF_New_1_25,
             GlobalNames.ListeLinksLignesFPD_LF_1_25, GlobalNames.ListeDefaultFPD_LF_1_25);

            vmMaster.setTableRowsModified(GlobalNames.FPA_LoadFactorsTableName, GlobalNames.ListeLignesFPA_LF_1_25, GlobalNames.ListeLignesFPA_LF_New_1_25,
              GlobalNames.ListeLinksLignesFPA_LF_1_25, GlobalNames.ListeDefaultFPA_LF_1_25);

            vmMaster.setTableColumnsModified(GlobalNames.PaxPlanName, GlobalNames.ListeEntete_Old_PaxPlan_1_25, GlobalNames.ListeTypeEntete_Old_PaxPlan_1_25, GlobalNames.ListePrimaryKey_New_PaxPlan_1_25,
                GlobalNames.ListeEntete_New_PaxPlan_1_25, GlobalNames.ListeTypeEntete_New_PaxPlan_1_25, GlobalNames.ListePrimaryKey_New_PaxPlan_1_25, GlobalNames.ListeEntete_LinksPaxPlan_1_25, GlobalNames.ListeDefault_New_PaxPlan_1_25);

            vmMaster.setTableColumnsModified(GlobalNames.BagPlanName, GlobalNames.ListeEntete_New_BagPlan_1_24, GlobalNames.ListeTypeEntete_New_BagPlan_1_24, GlobalNames.ListePrimaryKey_New_BagPlan_1_24,
                GlobalNames.ListeEntete_New_PaxPlan_1_25, GlobalNames.ListeTypeEntete_New_BagPlan_1_25, GlobalNames.ListePrimaryKey_New_BagPlan_1_25, GlobalNames.listeEntete_LinksBagPlan_1_25, GlobalNames.ListeDefault_New_BagPlan_1_25);

            /**/

            vmVersion = new VersionManager(1, 26);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);
            vmMaster.setTableColumnsModified(GlobalNames.FPDTableName,
                GlobalNames.ListeEntete_New_FPDTable_1_25, GlobalNames.ListeTypeEntete_New_FPDTable_1_25, GlobalNames.ListePrimaryKey_New_FPDTable_1_25,
                GlobalNames.ListeEntete_New_FPDTable_1_26, GlobalNames.ListeTypeEntete_New_FPDTable_1_26, GlobalNames.ListePrimaryKey_New_FPDTable_1_26, GlobalNames.listeEntete_LinksFPDTable_1_26, GlobalNames.ListeDefault_New_FPDTable_1_26);

            vmVersion = new VersionManager(1, 27);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);


            vmVersion = new VersionManager(1, 28);
            vmMaster = new VersionMaster(vmVersion);
            for (i = 1; i < GestionDonneesHUB2SIM.NbTerminaux; i++)
            {
                sPrefixeTable = GlobalNames.sBHS_PrefixeLong + i.ToString() + "_";
                sTableName = sPrefixeTable + GlobalNames.sBHS_Process;

                vmMaster.setTableRowsModified(sTableName, GlobalNames.ListeLignesProcess_BHS_New_1_22, GlobalNames.Liste_LignesProcess_BHS_1_28,
                    GlobalNames.ListeLinksLignesProcess_BHS_1_28, GlobalNames.ListeValuesProcess_BHS_1_22);
                vmMaster.setTableColumnsModified(sTableName,
                GlobalNames.ListeEntete_Old_BHS_Process_1_28, GlobalNames.ListeTypeEntete_Old_BHS_Process_1_28, GlobalNames.ListePrimaryKey_Old_BHS_Process_1_28,
                GlobalNames.ListeEntete_New_BHS_Process_1_28, GlobalNames.ListeTypeEntete_New_BHS_Process_1_28, GlobalNames.ListePrimaryKey_New_BHS_Process_1_28, GlobalNames.ListeEntete_Links_BHS_Process_1_28, null);

            }
            vmMaster.setTableConvertModifier(GlobalNames.Alloc_SecurityCheckTableName, new GlobalNames.ConvertAllocationSecurity_1_28());
            vmMaster.setTableColumnsModified(GlobalNames.FP_AircraftTypesTableName,GlobalNames.ListeEntete_Old_FP_AircraftTypesTable_1_28,GlobalNames.ListeTypeEntete_Old_FP_AircraftTypesTable_1_28,
                GlobalNames.ListePrimaryKey_FP_AircraftTypesTable_1_18,GlobalNames.ListeEntete_NewFP_AircraftTypesTable_1_28,GlobalNames.ListeTypeEntete_NewFP_AircraftTypesTable_1_28,
                GlobalNames.ListePrimaryKey_NewFP_AircraftTypesTable_1_28,GlobalNames.ListeEntete_LinksFP_AircraftTypesTable_1_28,GlobalNames.ListeDefault_FP_AircraftTypesTable_1_28 );
            dVersions.Add(vmVersion.ToString(), vmMaster);

            /*/**/

#if(NEWALLOCATIONSECU)
            for (int iVersion = 29; iVersion <= 51; iVersion++)
            {
                vmVersion = new VersionManager(1, iVersion);
                vmMaster = new VersionMaster(vmVersion);
                dVersions.Add(vmVersion.ToString(), vmMaster);
            }
            /*
            vmVersion = new VersionManager(1, 30);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);

            vmVersion = new VersionManager(1, 31);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);

            vmVersion = new VersionManager(1, 50);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);

            vmVersion = new VersionManager(1, 51);
            vmMaster = new VersionMaster(vmVersion);
            dVersions.Add(vmVersion.ToString(), vmMaster);*/

            vmVersion = new VersionManager(1, 52);
            vmMaster = new VersionMaster(vmVersion);

            vmMaster.setTableConvertModifier(GlobalNames.Alloc_SecurityCheckTableName, new GlobalNames.ConvertAllocationSecurity_1_52());
            vmMaster.setTableConvertModifier(GlobalNames.Alloc_PassportCheckTableName, new GlobalNames.ConvertAllocationSecurity_1_52());
            vmMaster.setTableConvertModifier(GlobalNames.Alloc_TransferDeskTableName, new GlobalNames.ConvertAllocationSecurity_1_52());


            vmMaster.setTableRowsModified(GlobalNames.OCT_CITableName, GlobalNames.ListeOldLignesOCT_CITable_1_52, GlobalNames.ListeLignesOCT_CITable_1_52,
               GlobalNames.ListeLinksOCT_CITable_1_52, GlobalNames.Default_OCT_CITable_1_52);

            //>> AndreiZaharia / Mulhouse project
            vmMaster.setTableRowsModified(GlobalNames.FPA_LoadFactorsTableName, GlobalNames.ListeLignesFPA_LF_1_25, GlobalNames.ListeLignesFPA_LF_New_1_52,
               GlobalNames.ListeLinksLignesFPA_LF_1_52, GlobalNames.ListeDefaultFPA_LF_1_52);

            vmMaster.setTableRowsModified(GlobalNames.FPD_LoadFactorsTableName, GlobalNames.ListeLignesFPD_LF_1_25, GlobalNames.ListeLignesFPD_LF_New_1_52,
               GlobalNames.ListeLinksLignesFPD_LF_1_52, GlobalNames.ListeDefaultFPD_LF_1_52);
            //<< AndreiZaharia / Mulhouse project
            dVersions.Add(vmVersion.ToString(), vmMaster);

            vmVersion = new VersionManager(1, 53);
            vmMaster = new VersionMaster(vmVersion);
            // << Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
            vmMaster.setTableColumnsModified(GlobalNames.ProcessScheduleName, GlobalNames.listeEnteteProcessScheduleTable_1_52,
                GlobalNames.listeTypeEnteteProcessScheduleTable_1_52,
                GlobalNames.listePrimaryKeyProcessScheduleTable_1_52, GlobalNames.ListeEntete_NewProcessScheduleTable_1_53,
                GlobalNames.ListeTypeEntete_NewProcessScheduleTable_1_53, GlobalNames.ListePrimaryKey_NewProcessScheduleTable_1_53,
                GlobalNames.listeEntete_LinksProcessScheduleTable_1_53, GlobalNames.ListeDefault_NewProcessScheduleTable_1_53);

            vmMaster.setTableColumnsModified(GlobalNames.Times_ProcessTableName, GlobalNames.listeEntetePaxProcessTimesTable_1_53,
                GlobalNames.listeTypeEntetePaxProcessTimesTable_1_53, GlobalNames.listePrimaryKeyPaxProcessTimesTable_1_53,
                GlobalNames.ListeEntete_NewPaxProcessTimesTable_1_54, GlobalNames.ListeTypeEntete_NewPaxProcessTimesTable_1_54,
                GlobalNames.ListePrimaryKey_NewPaxProcessTimesTable_1_54, GlobalNames.listeEntete_LinksPaxProcessTimesTable_1_54,
                GlobalNames.ListeDefault_NewPaxProcessTimesTable_1_54);
            // >> Task #8647 Pax2Sim - Adapt to PaxTrace modification (a new delay(waiting) time is added for each Group)
            dVersions.Add(vmVersion.ToString(), vmMaster);

            vmVersion = new VersionManager(1, 54);
            vmMaster = new VersionMaster(vmVersion);
            vmMaster.setTableRowsModified(GlobalNames.OCT_CITableName, GlobalNames.ListeOldLignesOCT_CITable_1_53, GlobalNames.ListeLignesOCT_CITable_1_54,
               GlobalNames.ListeLinksOCT_CITable_1_54, GlobalNames.Default_OCT_CITable_1_54);   // >> Bug #13367 Liege allocation

            dVersions.Add(vmVersion.ToString(), vmMaster);
/*
            // >> Task #11326 Pax2Sim - Allocation - add Parking allocation option            
            vmVersion = new VersionManager(1, 54);
            vmMaster = new VersionMaster(vmVersion);
            vmMaster.setTableColumnsModified(GlobalNames.FPDTableName,
                GlobalNames.ListeEntete_New_FPDTable_1_26, GlobalNames.ListeTypeEntete_New_FPDTable_1_26, GlobalNames.ListePrimaryKey_New_FPDTable_1_26,
                GlobalNames.ListeEntete_New_FPDTable_1_54, GlobalNames.ListeTypeEntete_New_FPDTable_1_54, GlobalNames.ListePrimaryKey_New_FPDTable_1_54,
                GlobalNames.listeEntete_LinksFPDTable_1_54, GlobalNames.ListeDefault_New_FPDTable_1_54);
            dVersions.Add(vmVersion.ToString(), vmMaster);
            // << Task #11326 Pax2Sim - Allocation - add Parking allocation option
*/
            // >> Task #15867 Transfer Distribution Tables/Charts improvement
            vmVersion = new VersionManager(1, 55);
            vmMaster = new VersionMaster(vmVersion);
            vmMaster.setTableColumnsModified(GlobalNames.Transfer_FlightCategoryDitributionTableName, CompatibilityConstants.transferFlightCategoryDistributionColumnNames_1_54,
                CompatibilityConstants.transferFlightCategoryDistributionColumnDataTypes_1_54, CompatibilityConstants.transferFlightCategoryDistributionPrimaryKey_1_54,
                CompatibilityConstants.transferFlightCategoryDistributionColumnNames_1_55, CompatibilityConstants.transferFlightCategoryDistributionColumnDataTypes_1_55,
                CompatibilityConstants.transferFlightCategoryDistributionPrimaryKey_1_55, CompatibilityConstants.transferFlightCategoryDistributionLinks_1_54_1_55,
                CompatibilityConstants.transferFlightCategoryDistributionDefaultValues_1_55);
            vmMaster.setTableColumnsModified(GlobalNames.Transfer_TerminalDitributionTableName, CompatibilityConstants.transferTerminalDistributionColumnNames_1_54,
                CompatibilityConstants.transferTerminalDistributionColumnDataTypes_1_54, CompatibilityConstants.transferTerminalDistributionPrimaryKey_1_54,
                CompatibilityConstants.transferTerminalDistributionColumnNames_1_55, CompatibilityConstants.transferTerminalDistributionColumnDataTypes_1_55,
                CompatibilityConstants.transferTerminalDistributionPrimaryKey_1_55, CompatibilityConstants.transferTerminalDistributionLinks_1_54_1_55,
                CompatibilityConstants.transferTerminalDistributionDefaultValues_1_55);            
            dVersions.Add(vmVersion.ToString(), vmMaster);
            // << Task #15867 Transfer Distribution Tables/Charts improvement

            vmLastVersion = new VersionManager(vmVersion);
#endif
        }
        #endregion

        #region Functions to detect the next change after a given Version for a table in the list of Version
        /// <summary>
        /// Returns the next version that the table will change. Starts at vmActualVersion +1 and stops 
        /// when it can't find more version. It returns null if there is no more changed for the table.
        /// </summary>
        /// <param name="sTableName">Name of the table.</param>
        /// <param name="vmActualVersion"></param>
        /// <returns></returns>
        private VersionMaster HasChanged(String sTableName, VersionManager vmActualVersion)
        {
            VersionManager vmObservedVersionName = vmActualVersion;
            if (vmObservedVersionName == null)
                return null;
            vmObservedVersionName.IncrecementMinorVersion();
            if (!dVersions.ContainsKey(vmObservedVersionName.ToString()))
                vmObservedVersionName.IncrecementMajorVersion();

            if ((dVersions == null) || (vmObservedVersionName == null)
                || (!dVersions.ContainsKey(vmObservedVersionName.ToString())))
                return null;
            VersionMaster vmObservedVersion = dVersions[vmObservedVersionName.ToString()];
            while (vmObservedVersion != null)
            {/*
                String sTableName = vmObservedVersion.isModifiedTable(sTableName);
                if (sTableName != null)
                    return vmObservedVersion;*/
                if (vmObservedVersion.isModifiedTable(sTableName))
                    return vmObservedVersion;

                vmObservedVersionName.IncrecementMinorVersion();
                if (!dVersions.ContainsKey(vmObservedVersionName.ToString()))
                {
                    vmObservedVersionName.IncrecementMajorVersion();
                    if (!dVersions.ContainsKey(vmObservedVersionName.ToString()))
                        return null;
                }
                vmObservedVersion = dVersions[vmObservedVersionName.ToString()];
            }
            return null;
        }

        private VersionMaster HasChanged(String sTableName)
        {
            return HasChanged(sTableName, vmFirstVersion);
        }
        #endregion

        #region Functions that returns the oldest changes on the observed table before the observed version (can be the observed version)
        /// <summary>
        /// Find the first time that the table has been changed.
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="vmActualVersion"></param>
        /// <param name="vmLimitVersion"></param>
        /// <returns></returns>
        private VersionMaster HasChanged_Rew(String sTableName, VersionManager vmActualVersion, VersionManager vmLimitVersion)
        {
            VersionManager vmObservedVersionName = new VersionManager(vmActualVersion);
            if (vmObservedVersionName == null)
                return null;
            if ((dVersions == null) || (vmObservedVersionName == null)
                || (!dVersions.ContainsKey(vmObservedVersionName.ToString())) ||
                (vmObservedVersionName < vmLimitVersion))
            {
                if (vmActualVersion.isVersion(0, 0))
                    return null;
                vmObservedVersionName.DecrecementMinorVersion();
                return  HasChanged_Rew(sTableName, vmObservedVersionName, vmLimitVersion);
            }
            VersionMaster vmObservedVersion = dVersions[vmObservedVersionName.ToString()];
            VersionMaster.ModifiedTable vtTable = null;
            if (vmObservedVersion.isModifiedTable(sTableName))
            {
                vtTable = vmObservedVersion.getModifiedTable(sTableName);
                if (vtTable.isNameChanged)
                    vtTable = null;
            }
            if (vmObservedVersion.isModifiedTableCheckNew(sTableName))
            {
                vtTable = vmObservedVersion.getModifiedTableCheckNew(sTableName);
                sTableName = vtTable.OldTableName;
            }
            vmObservedVersionName.DecrecementMinorVersion();
            VersionMaster vmResult = HasChanged_Rew(sTableName, vmObservedVersionName, vmLimitVersion);
            if (vmResult != null)
                return vmResult;
            if ((vtTable != null) && (vtTable.isColumnsChanged))
                return vmObservedVersion;
            return null;
        }
        private VersionMaster HasChanged_Rew(String sTableName, VersionManager vmLimitVersion)
        {
            return HasChanged_Rew(sTableName, vmLastVersion, vmLimitVersion);
        }
        #endregion

        #region returns the next changed on the observed table name (starting on the given version and going dec.).
        private VersionMaster NameHasChanged_Rew(String sTableName, VersionManager vmActualVersion, VersionManager vmLimitVersion)
        {
            VersionManager vmObservedVersionName = new VersionManager(vmActualVersion);
            if (vmObservedVersionName == null)
                return null;

            if ((dVersions == null) || (vmObservedVersionName == null)
                || (!dVersions.ContainsKey(vmObservedVersionName.ToString())) ||
                (vmObservedVersionName < vmLimitVersion))
                return null;
            VersionMaster vmObservedVersion = dVersions[vmObservedVersionName.ToString()];
            while (vmObservedVersion != null)
            {
                if (vmObservedVersion.isModifiedTableCheckNew(sTableName))
                {
                    return vmObservedVersion;
                }

                vmObservedVersionName.DecrecementMinorVersion();
                if ((!dVersions.ContainsKey(vmObservedVersionName.ToString())) ||
                    (vmObservedVersionName < vmLimitVersion))
                    return null;
                vmObservedVersion = dVersions[vmObservedVersionName.ToString()];
            }
            return null;
        }
        private VersionMaster NameHasChanged_Rew(String sTableName, VersionManager vmLimitVersion)
        {
            return NameHasChanged_Rew(sTableName, vmLastVersion, vmLimitVersion);
        }
        #endregion

        #region Returns the name of the observed table at the observed version.
        private String OriginalName(String sTableName, VersionManager vmLimitVersion)
        {
            VersionMaster vmPreviousChange = NameHasChanged_Rew(sTableName, vmLimitVersion);
            if (vmPreviousChange == null)
                return sTableName;
            VersionManager vmCurrentVersion = null;
            String sTableNameChanged = vmPreviousChange.getModifiedTableCheckNew(sTableName).OldTableName;

            while(vmPreviousChange.CurrentVersion != vmLimitVersion)
            {
                vmCurrentVersion = new VersionManager(vmPreviousChange.CurrentVersion);
                vmCurrentVersion.DecrecementMinorVersion();
                vmPreviousChange = NameHasChanged_Rew(sTableName, vmCurrentVersion, vmLimitVersion);
                if (vmPreviousChange == null)
                    return sTableNameChanged;
                sTableNameChanged = vmPreviousChange.getModifiedTableCheckNew(sTableName).OldTableName;
            }
            return sTableNameChanged;
        }
        #endregion

        #region LectureFichier(
        /// <summary>
        /// Fonction qui s'occupe de lire les tables définies par défault dans Pax2sim.
        /// </summary>
        /// <param name="sNomTable"></param>
        /// <param name="tsColumnsName"></param>
        /// <param name="ttColumnsType"></param>
        /// <param name="tiPrimaryKey"></param>
        /// <param name="sNomFichier"></param>
        /// <param name="sSeparateur"></param>
        /// <param name="bAllowAddDoubleColumns"></param>
        /// <param name="bAllowAddStringColumns"></param>
        /// <param name="bAllowAddBoolColumns"></param>
        /// <param name="bRessources"></param>
        /// <param name="bIgnored"></param>
        /// <param name="alListeErrors"></param>
        /// <param name="Version"></param>
        /// <returns></returns>
        public DataTable LectureFichier(String sNomTable,
                                        String sParentName,
                                          String[] tsColumnsName,
                                          Type[] ttColumnsType,
                                          int[] tiPrimaryKey,
                                          String sNomFichier,
                                          String sSeparateur,
                                          bool bAllowAddDoubleColumns,
                                          bool bAllowAddStringColumns,
                                          bool bAllowAddBoolColumns,
                                          bool bRessources,
                                          bool bIgnored,
                                          ArrayList alListeErrors,
                                          VersionManager Version)
        {
            DataTable dtResult = null;
            bool bResult;
            dtResult = new DataTable(sNomTable);

            #region There is no version, or the current version does not appears in the list. (means that the table does not need any change)            
            if ((dVersions == null) ||(Version == null) ||(!dVersions.ContainsKey(Version.ToString())))
            {
                /*The version is not on the ones that requires a specific opening (or the tables is not initialized properly.*/
                OverallTools.FonctionUtiles.initialiserTable(dtResult,
                                                             tsColumnsName,
                                                             ttColumnsType,
                                                             tiPrimaryKey);

                bResult = OverallTools.FonctionUtiles.LectureFichier(dtResult,
                                              tsColumnsName,
                                              ttColumnsType,
                                              sNomFichier,
                                              sSeparateur,
                                              bAllowAddDoubleColumns,
                                              bAllowAddStringColumns,
                                              bAllowAddBoolColumns,
                                              bRessources,
                                              bIgnored,
                                              alListeErrors);
                if (!bResult)
                    return null;
                return dtResult;
            }
            #endregion

            //We take the current version.
            VersionMaster dmVersions = dVersions[Version.ToString()];
            
            //We find the original table name for the project version.
            String sOriginalName = OriginalName(sNomTable, Version);
            //If the parent name exists. We check what was the original parent name instead.
            if(sParentName != null)
                sOriginalName = OriginalName(sParentName, Version);

            //We check if the table has been modified in the loaded version. 
            bool bIsModified = dmVersions.isModifiedTable(sOriginalName);



            VersionMaster.ModifiedTable mtTableDefinition = null;
            bool bIsColumnsModified = false;
            ///Si la table avait été modifié dans la version \ref Version, alors on récupère les informations liées aux modification
            ///de cette table dans cette version.
            if (bIsModified)
            {
                mtTableDefinition = dmVersions.getModifiedTable(sOriginalName);
                bIsColumnsModified = mtTableDefinition.isColumnsChanged;
            }
            if (!bIsColumnsModified)
            {
                dmVersions = HasChanged_Rew(sNomTable, Version);
                if (sParentName != null)
                    dmVersions = HasChanged_Rew(sParentName, Version);
                bIsModified = (dmVersions != null);
                if (bIsModified)
                {
                    mtTableDefinition = dmVersions.getModifiedTable(sOriginalName);
                    bIsColumnsModified = mtTableDefinition.isColumnsChanged;
                }
            }



            String[] tsEnteteOld = tsColumnsName;
            Type[] tsTypeEnteteOld = ttColumnsType;
            Int32[] tsPrimaryKeyOld = tiPrimaryKey;

            if (bIsColumnsModified)
            {
                tsEnteteOld = mtTableDefinition.OldColumns;
                tsTypeEnteteOld = mtTableDefinition.OldColumnType;
                tsPrimaryKeyOld = mtTableDefinition.OldPrimaryKeys;
            }

            OverallTools.FonctionUtiles.initialiserTable(dtResult,
                                                         tsEnteteOld,
                                                         tsTypeEnteteOld,
                                                         tsPrimaryKeyOld);
            bResult = OverallTools.FonctionUtiles.LectureFichier(dtResult,
                                          tsEnteteOld,
                                          tsTypeEnteteOld,
                                          sNomFichier,
                                          sSeparateur,
                                          bAllowAddDoubleColumns,
                                          bAllowAddStringColumns,
                                          bAllowAddBoolColumns,
                                          bRessources,
                                          bIgnored,
                                          alListeErrors);
            if (!bResult)
                return null;


            //We take the current version.
            dmVersions = dVersions[Version.ToString()];

            //To make sure that we dont change the name of the table in case we are using the sParentName function.
            if (sParentName == null)
                sOriginalName = null;
            return ConvertTable(dtResult, Version, sOriginalName);
        }
        public DataTable LectureFichier(String sNomTable,
                                        String sParentName,
                                          List<DataManagement.DataManagerInput.LoadParameters.Entete> leEntete,
                                          List<String> lsPrimaryKey,
                                          String sNomFichier,
                                          String sSeparateur,
                                          Type tAjoutColumnType,
                                          bool bRessources,
                                          bool bIgnored,
                                          ArrayList alListeErrors,
                                          VersionManager Version) 
        {
            Int32[] iPrimary = new Int32[lsPrimaryKey.Count];
            String[] tsEntete = new String[leEntete.Count];
            Type[] ttEntete = new Type[leEntete.Count];
            for (int i = 0; i < leEntete.Count; i++)
            {
                tsEntete[i] = leEntete[i].sName;
                ttEntete[i] = leEntete[i].tType;
            }
            for (int i = 0; i < lsPrimaryKey.Count; i++)
            {
                iPrimary[i] = OverallTools.FonctionUtiles.indexDansListe(lsPrimaryKey[i], tsEntete);
            }
            return LectureFichier(sNomTable,sParentName, tsEntete, ttEntete, iPrimary,
                sNomFichier, sSeparateur, tAjoutColumnType == typeof(Double), tAjoutColumnType == typeof(String), tAjoutColumnType == typeof(Boolean),
                bRessources, bIgnored, alListeErrors, Version);
        }

        #endregion

        public string ConvertName(String Name,
                                         VersionManager sVersion)
        {
            VersionManager vmCurrent = new VersionManager(sVersion);
            if (!dVersions.ContainsKey(vmCurrent.ToString()))
                return Name;
            if (vmCurrent.isVersion(0, 0))
                return Name;
            String sNewName = Name;
            VersionMaster vmCurrentMaster = dVersions[vmCurrent.ToString()];
            VersionMaster.ModifiedTable mtTable = null;
            while (vmCurrentMaster != null)
            {
                mtTable = null;
                if (vmCurrentMaster.isModifiedTable(sNewName))
                {
                    mtTable = vmCurrentMaster.getModifiedTable(sNewName);
                    if (mtTable.isNameChanged)
                    {
                        sNewName = mtTable.NewTableName;
                    }
                }
                vmCurrent.IncrecementMinorVersion();
                if (!dVersions.ContainsKey(vmCurrent.ToString()))
                    vmCurrent.IncrecementMajorVersion();
                if (!dVersions.ContainsKey(vmCurrent.ToString()))
                    break;
                vmCurrentMaster = dVersions[vmCurrent.ToString()];
            }
            #region Conversion du nom des mean flows vers un nom plus générique.
            if (sNewName.EndsWith("Arrival_Infeed_Mean_Flows"))
                sNewName = sNewName.Replace("Arrival_Infeed_Mean_Flows", "Mean_Flows_Arrival_Infeed");
            if (sNewName.EndsWith("Check_In_Mean_Flows"))
                sNewName = sNewName.Replace("Check_In_Mean_Flows", "Mean_Flows_Check_In");
            if (sNewName.EndsWith("Transfer_Infeed_Mean_Flows"))
                sNewName = sNewName.Replace("Transfer_Infeed_Mean_Flows", "Mean_Flows_Transfer_Infeed");
            #endregion
            return sNewName;
        }


        #region ConvertTableFilter(DataTable dtTable, String sMotherTableName, String[] columnsName,
        internal void ConvertFilter(DataTable dtTable,
                                  Filter fFilter,
                                  String sMotherTableName,
                                  VersionManager vmVersion)
        {
            if (fFilter == null)
                return;
            if (!dVersions.ContainsKey(vmVersion.ToString()))
                return;
            VersionMaster.ModifiedTable mtChanges=null;
            VersionMaster vmMaster = dVersions[vmVersion.ToString()];
            VersionManager vmManager = new VersionManager(vmVersion);
            String sOldMotherTableName = OriginalName(sMotherTableName, vmVersion);
            while (vmMaster != null)
            {
                if (vmMaster.isModifiedTable(sOldMotherTableName))
                {
                    mtChanges = vmMaster.getModifiedTable(sOldMotherTableName);
                    if (mtChanges.isNameChanged)
                    {
                        sOldMotherTableName = mtChanges.NewTableName;
                    }
                    if (mtChanges.isColumnsChanged)
                    {
                        DataTable dtTmp = null;
                        if (dtTable != null)
                        {
                            dtTmp = dtTable.Copy();
                            dtTable.Rows.Clear();
                            dtTable.PrimaryKey = null;
                            dtTable.Columns.Clear();
                        }
                        ConvertFilterColumns(dtTmp, dtTable, fFilter, mtChanges);
                    }
                    if ((mtChanges.isRowsChanged)&& (dtTable != null))
                    {
                        bool bAllPresent = true;
                        foreach (String sTmp in mtChanges.OldLines)
                        {
                            if (OverallTools.DataFunctions.indexLigne(dtTable, 0, sTmp) == -1)
                            {
                                bAllPresent = false;
                                break;
                            }
                        }
                        if ((bAllPresent) && (dtTable.Rows.Count == mtChanges.OldLines.Length))
                        {
                            String[] tsListeRows = mtChanges.NewLines;
                            String[] tsLinksRows = mtChanges.LinksRows;
                            Object[] tsDefaultRows = mtChanges.DefaultValuesLines;

                            DataTable dtTableTmp = ConvertLine(dtTable, tsLinksRows, tsListeRows, tsDefaultRows);
                            if (dtTableTmp != null)
                            {
                                dtTable.Rows.Clear();
                                dtTable.Columns.Clear();
                                foreach (DataColumn dcColumn in dtTableTmp.Columns)
                                {
                                    dtTable.Columns.Add(dcColumn.ColumnName, dcColumn.DataType);
                                }
                                foreach (DataRow drRow in dtTableTmp.Rows)
                                {
                                    dtTable.Rows.Add(drRow.ItemArray);
                                }
                                dtTableTmp.Dispose();
                            }
                        }
                    }
                }
                vmMaster = HasChanged(sOldMotherTableName, vmManager);
            }
        }
        private static void ConvertFilterColumns(DataTable dtOrigin,
                                                   DataTable dtDestination,
                                                   Filter fFilter,
                                                   VersionMaster.ModifiedTable mtChanges)
        {
            if(!mtChanges.isColumnsChanged)
                return;
            Int32[] iOldColumnsPosition = new Int32[mtChanges.OldColumns.Length];
            int i;
            bool bFindAll = true;
            bool bRightOrder = true;
            int iLastFound = 0;
            int iNumberFound = 0;
            for (i = 0; i < mtChanges.OldColumns.Length; i++)
            {
                iOldColumnsPosition[i] = fFilter.ColumnsNames.IndexOf(mtChanges.OldColumns[i]);
                //iOldColumnsPosition[i] = dtOrigin.Columns.IndexOf(mtChanges.OldColumns[i]);
                if (iOldColumnsPosition[i] == -1)
                    bFindAll = false;
                else
                {
                    iNumberFound++;
                    if (iLastFound > iOldColumnsPosition[i])
                        bRightOrder = false;
                    iLastFound = iOldColumnsPosition[i];
                }
            }
            ArrayList alNewColumns = new ArrayList();
            if (bFindAll && fFilter.ColumnsNames.Count == iOldColumnsPosition.Length)
            {
                if (dtDestination != null)
                {
                    OverallTools.FonctionUtiles.initialiserTable(dtDestination, mtChanges.NewColumns, mtChanges.NewColumnType, mtChanges.NewPrimaryKeys);
                }
            }
            else if (!bFindAll && (iNumberFound == 0))
                return;



            if (bRightOrder)
            {
                int iIndexTable = 0;
                int iIndexLinks = 0;
                int iFilterPosition = 0;
                while ((iFilterPosition < fFilter.ColumnsNames.Count) 
                    || (iIndexLinks < mtChanges.LinksColumns.Length))
                {
                    String sLinkName = null;
                    String sNewName = null;
                    String sColumnName = null;
                    if (iFilterPosition < fFilter.ColumnsNames.Count)
                    {
                        sColumnName = (String)fFilter.ColumnsNames[iFilterPosition];
                        if (!(bool)fFilter.Display[iFilterPosition])
                        {
                            iFilterPosition++;
                            continue;
                        }
                    }
                    if (iIndexLinks < mtChanges.LinksColumns.Length)
                    {
                        sLinkName = mtChanges.LinksColumns[iIndexLinks];
                        sNewName = mtChanges.NewColumns[iIndexLinks];
                    }
                    if (sColumnName != sLinkName)
                    {
                        int iIndexLink = -1;
                        if((sColumnName != null)&& (sColumnName != ""))
                            iIndexLink = OverallTools.FonctionUtiles.indexDansListe(sColumnName, mtChanges.LinksColumns);
                        if (sLinkName == "")
                        {
                            //Nouvelle colonne.
                            if ((dtDestination != null) && (!dtDestination.Columns.Contains(sNewName)))
                                dtDestination.Columns.Add(sNewName, mtChanges.NewColumnType[iIndexLinks]);
                            alNewColumns.Add(sNewName);
                            fFilter.InsertColumn(iFilterPosition, sNewName, "[" + sNewName + "]", true, "", "Group");
                            iIndexLinks++;
                            iFilterPosition++;
                        }
                        else if ((iIndexLink == -1) && (sColumnName != null) && (sColumnName != ""))
                        {
                            //Colonne présente dans l'ancienne table.
                            if ((dtDestination != null) && (!dtDestination.Columns.Contains(sColumnName)))
                                dtDestination.Columns.Add(sColumnName, dtOrigin.Columns[iIndexTable].DataType);
                            iIndexTable++;
                            iFilterPosition++;
                        }
                        else
                        {
                            if (iIndexLink == -1)
                                iIndexLink = mtChanges.LinksColumns.Length-1;
                            //Colonne du filtre est en fait une colonne présente plus loin dans la table.
                            for (i = iIndexLinks; i <= iIndexLink; i++)
                            {
                                if ((mtChanges.LinksColumns[i] != "") && ((mtChanges.LinksColumns[i] != sColumnName)))
                                    continue;
                                if ((dtDestination != null) && (!dtDestination.Columns.Contains(mtChanges.NewColumns[i])))
                                    dtDestination.Columns.Add(mtChanges.NewColumns[i], mtChanges.NewColumnType[i]);
                                if (mtChanges.LinksColumns[i] != sColumnName)
                                {
                                    alNewColumns.Add(mtChanges.NewColumns[i]);
                                    fFilter.InsertColumn(iFilterPosition, mtChanges.NewColumns[i], "[" + mtChanges.NewColumns[i] + "]", true, "", "Group");
                                }
                                iFilterPosition++;
                            }
                            iIndexLinks = iIndexLink + 1;
                            iIndexTable++;
                        }
                        /*
                        if ((iIndexLink == -1) && (sLinkName != "") && (sLinkName != null))
                        {
                            if ((dtDestination != null) && (!dtDestination.Columns.Contains(sColumnName)))
                                dtDestination.Columns.Add(sColumnName, dtOrigin.Columns[iIndexTable].DataType);
                            iIndexTable++;
                            iFilterPosition++;
                        }
                        else
                        {
                            if (iIndexLink == -1)
                                iIndexLink = mtChanges.LinksColumns.Length - 1;
                            for (i = iIndexLinks; i <= iIndexLink; i++)
                            {
                                if ((mtChanges.LinksColumns[i] != "") && ((mtChanges.LinksColumns[i] !=sColumnName)))
                                    continue;
                                if ((dtDestination != null) && (!dtDestination.Columns.Contains(mtChanges.NewColumns[i])))
                                    dtDestination.Columns.Add(mtChanges.NewColumns[i], mtChanges.NewColumnType[i]);
                                if (mtChanges.LinksColumns[i] != sColumnName)
                                {
                                    alNewColumns.Add(mtChanges.NewColumns[i]);
                                    fFilter.InsertColumn(iFilterPosition, mtChanges.NewColumns[i], "[" + mtChanges.NewColumns[i] + "]", true, "", "Group");
                                }
                                iFilterPosition++;
                            }
                            iIndexLinks = iIndexLink + 1;
                            iIndexTable++;
                        }*/
                    }
                    else
                    {
                        // (sColumnName == sLinkName)
                        if ((dtDestination != null)&&(!dtDestination.Columns.Contains(sNewName)))
                            dtDestination.Columns.Add(sNewName, mtChanges.NewColumnType[iIndexLinks]);
                        iIndexLinks++;
                        iFilterPosition++;
                        iIndexTable++;
                    }
                }
            }
            else
            {
                if (dtDestination != null)
                {
                    for (i = 0; i < dtOrigin.Columns.Count; i++)
                    {
                        if (!dtDestination.Columns.Contains(dtOrigin.Columns[i].ColumnName))
                            dtDestination.Columns.Add(dtOrigin.Columns[i].ColumnName, dtOrigin.Columns[i].DataType);
                    }
                }
                for (i = 0; i < mtChanges.LinksColumns.Length; i++)
                {
                    if (mtChanges.LinksColumns[i] != "")
                        continue;

                    if ((dtDestination != null) && (!dtDestination.Columns.Contains(mtChanges.NewColumns[i])))
                        dtDestination.Columns.Add(mtChanges.NewColumns[i], mtChanges.NewColumnType[i]);
                    alNewColumns.Add(mtChanges.NewColumns[i]);
                    fFilter.AddColumn(mtChanges.NewColumns[i], "[" + mtChanges.NewColumns[i] + "]", true, "", "Group");
                }
            }
            for (i = 0; i < mtChanges.LinksColumns.Length; i++)
            {
                if ((mtChanges.LinksColumns[i] == "") || (mtChanges.LinksColumns[i] == mtChanges.NewColumns[i]))
                    continue;
                fFilter.ConvertColumnsNames(mtChanges.LinksColumns[i], mtChanges.NewColumns[i], alNewColumns);
            }

            if (dtDestination != null)
                CopyData(dtDestination, dtOrigin, mtChanges.LinksColumns, mtChanges.NewColumns);
        }
        private static void CopyData(DataTable dtDestination, DataTable dtorigin, String[] tsLinks, String[] tsNewColumns)
        {
            Int32[] tiIndex = new Int32[dtDestination.Columns.Count];
            Type[] ttTypes = new Type[dtDestination.Columns.Count];
            int i;
            for (i = 0; i < dtDestination.Columns.Count; i++)
            {
                tiIndex[i]  = OverallTools.FonctionUtiles.indexDansListe(dtDestination.Columns[i].ColumnName, tsNewColumns);
                ttTypes[i] = dtDestination.Columns[i].DataType;
                if (tiIndex[i] == -1)
                {
                    tiIndex[i] = dtorigin.Columns.IndexOf(dtDestination.Columns[i].ColumnName);
                }
                else
                {
                      tiIndex[i]= dtorigin.Columns.IndexOf(tsLinks[ tiIndex[i]]);
                }
            }
            for (i = 0; i < dtorigin.Rows.Count; i++)
            {
                DataRow drRow = dtDestination.NewRow();
                for (int j = 0; j < tiIndex.Length; j++)
                {
                    if(tiIndex[j] == -1)
                    {
                         /*drRow[j]*/
                        if (ttTypes[j] == typeof(String))
                        {
                            drRow[j] = "";
                        }
                        else if (ttTypes[j] == typeof(Boolean))
                        {
                            drRow[j] =false;
                        }
                        else if (ttTypes[j] == typeof(DateTime))
                        {
                            drRow[j] = DateTime.Now;
                        }
                        else if (ttTypes[j] == typeof(TimeSpan))
                        {
                            drRow[j] = new TimeSpan(0,0,0);
                        }
                        drRow[j] = OverallTools.FonctionUtiles.ConvertObject(0, ttTypes[j]);
                    }
                    else
                    {
                        drRow[j] = OverallTools.FonctionUtiles.ConvertObject( dtorigin.Rows[i][tiIndex[j]],ttTypes[j]);
                    }
                }
                dtDestination.Rows.Add(drRow);
            }
            dtDestination.AcceptChanges();
        }
        #endregion


        #region internal DataTable ConvertTable(DataTable dtTable, VersionManager vmVersion)
        internal DataTable ConvertTable(DataTable dtTable, VersionManager vmVersion, String sParentName)
        {
            if (!dVersions.ContainsKey(vmVersion.ToString()))
                return dtTable;
            if (vmVersion.isVersion(0, 0))
                return dtTable;
            VersionManager vmCurrentVersion = new VersionManager(vmVersion);
            VersionMaster vmCurrentMaster = dVersions[vmCurrentVersion.ToString()];
            VersionMaster.ModifiedTable mtTable = null;
            DataTable dtResult = dtTable.Copy();
            DataTable dtTmp = null;

            String[] listeEntete;
            Type[] listeTypeEntete;
            int[] listePrimaryKey;
            String[] tsLinks;
            Object[] tsNewData;

            String sCurrentName = dtTable.TableName;
            if (sParentName != null)
                sCurrentName = sParentName;

            while (vmCurrentMaster != null)
            {
                mtTable = null;

                if (vmCurrentMaster.isModifiedTable(sCurrentName))
                {
                    mtTable = vmCurrentMaster.getModifiedTable(sCurrentName);
                }
                if (mtTable != null)
                {
                    if ((mtTable.isNameChanged) && (sParentName == null))
                    {
                        dtResult.TableName = mtTable.NewTableName;
                        sCurrentName = mtTable.NewTableName;
                    }
                    if (mtTable.isColumnsChanged)
                    {
                        listeEntete = mtTable.NewColumns;
                        listeTypeEntete = mtTable.NewColumnType;
                        listePrimaryKey = mtTable.NewPrimaryKeys;
                        tsLinks = mtTable.LinksColumns;
                        tsNewData = mtTable.DefaultValuesColumns;

                        dtTmp = new DataTable(dtResult.TableName);

                        OverallTools.FonctionUtiles.initialiserTable(dtTmp,
                                                                     listeEntete,
                                                                     listeTypeEntete,
                                                                     listePrimaryKey);
                        ConvertTables.ConvertTable(dtResult, dtTmp, tsLinks, tsNewData);
                        dtResult.Dispose();
                        dtResult = dtTmp;
                    }
                    if (mtTable.isRowsChanged)
                    {
                        String[] tsListeRows = mtTable.NewLines;
                        String[] tsLinksRows = mtTable.LinksRows;
                        Object[] tsDefaultRows = mtTable.DefaultValuesLines;

                        DataTable dtTableTmp = ConvertLine(dtResult, tsLinksRows, tsListeRows, tsDefaultRows);
                        if (dtTableTmp != null)
                            dtResult = dtTableTmp;
                    }
                    if (mtTable.isConverted)
                    {
                        try
                        {
                            DataTable dtTableTmp = mtTable.Convert.Convert(dtResult);
                            if (dtTableTmp != null)
                                dtResult = dtTableTmp;
                        }
                        catch (Exception exc)
                        {
                            OverallTools.ExternFunctions.PrintLogFile("Except02010: " + this.GetType().ToString() + " throw an exception: " + exc.Message);
                        }
                    }
                }
                vmCurrentVersion.IncrecementMinorVersion();
                if (!dVersions.ContainsKey(vmCurrentVersion.ToString()))
                    vmCurrentVersion.IncrecementMajorVersion();
                if (!dVersions.ContainsKey(vmCurrentVersion.ToString()))
                    break;
                vmCurrentMaster = dVersions[vmCurrentVersion.ToString()];
            }
            return dtResult;
        }
        #endregion

        #region ConvertTable(DataTable dtOriginTable,DataTable dtDestinationTable, String[] tsLinks)
        /// <summary>
        /// Fonction qui va remplir la table \ref dtDestinationTable avec le contenu de la table \ref dtOriginTable
        /// en suivant les indications présentes dans \ref tsLinks et en initialisant les nouvelles colonnes avec les 
        /// valeurs présentes dans \ref toNewData.
        /// </summary>
        /// <param name="dtOriginTable"></param>
        /// <param name="dtDestinationTable"></param>
        /// <param name="tsLinks"></param>
        /// <param name="toNewData"></param>
        /// <returns></returns>
        private static bool ConvertTable(DataTable dtOriginTable,
                                  DataTable dtDestinationTable,
                                  String[] tsLinks,
                                  Object[] toNewData)
        {
            int[] iIndex = new int[dtDestinationTable.Columns.Count];
            int i;
            if ((toNewData != null) && (toNewData.Length != dtDestinationTable.Columns.Count))
                toNewData = null;
            for (i = 0; i < dtDestinationTable.Columns.Count; i++)
            {
                iIndex[i] = dtOriginTable.Columns.IndexOf(tsLinks[i]);
            }
            foreach (DataRow line in dtOriginTable.Rows)
            {
                DataRow newRow = dtDestinationTable.NewRow();
                for (i = 0; i < dtDestinationTable.Columns.Count; i++)
                {
                    if (iIndex[i] != -1)
                        newRow[i] = line[iIndex[i]];
                    else
                    {
                        if (toNewData != null)
                        {
                            if (toNewData[i].GetType() == dtDestinationTable.Columns[i].DataType)
                            {
                                newRow[i] = toNewData[i];
                                continue;
                            }
                        }
                        if ((dtDestinationTable.Columns[i].DataType == typeof(Double)) ||
                            (dtDestinationTable.Columns[i].DataType == typeof(Int32)) ||
                            (dtDestinationTable.Columns[i].DataType == typeof(Int16)) ||
                            (dtDestinationTable.Columns[i].DataType == typeof(Int64)))
                            newRow[i] = 0;
                        else if (dtDestinationTable.Columns[i].DataType == typeof(String))
                            newRow[i] = "";
                        else if (dtDestinationTable.Columns[i].DataType == typeof(Boolean))
                            newRow[i] = false;
                    }
                }
                dtDestinationTable.Rows.Add(newRow);
            }
            dtDestinationTable.AcceptChanges();
            return true;
        }
        #endregion


        internal void ConvertFilters(Hashtable ListFilters,
                                          VersionManager sVersion)
        {
            if (ListFilters.Count == 0)
                return;
            foreach (Filter fFiltre in ListFilters.Values)
            {
                if (fFiltre.copyTable)
                    continue;
                String sMother = GestionDonneesHUB2SIM.motherTable(fFiltre.Name, ListFilters);
                int iIndex = OverallTools.FonctionUtiles.indexDansListe(sMother, GestionDonneesHUB2SIM.ListeNomTablePAX);
                if (iIndex == -1)
                    continue;
                ConvertFilter(null, fFiltre, sMother, sVersion);
            }
        }









        #region Version 1.15 ==> mean flows.
        public static class Version_1_15
        {
            #region Mean_Flows
            public static bool ConvertMeanFlows_To_1_16(GestionDonnees gdData)
            {

                if (PAX2SIM.bDelhi)
                    return true;
                int j;
                for (int i = 1; i < GestionDonneesHUB2SIM.NbTerminaux; i++)
                {
                    //PAX2SIM.sTerminalName
                    String sPrefixeTable = GlobalNames.sBHS_PrefixeLong + i.ToString() + "_";
                    String sTableName = sPrefixeTable + "Mean_Flows";
                    if (!gdData.estPresent(sTableName))
                    {
                        continue;
                    }
                    DataTable dtTable = gdData.GetTable(sTableName);
                    #region Create the news table
                    DataTable dtArrival_MeanFlows = new DataTable(sPrefixeTable + "Mean_Flows_Arrival_Infeed");
                    DataTable dtCheckIn_MeanFlows = new DataTable(sPrefixeTable + "Mean_Flows_Check_In");
                    DataTable dtTransfer_MeanFlows = new DataTable(sPrefixeTable + "Mean_Flows_Transfer_Infeed");
                    dtArrival_MeanFlows.Columns.Add("Time", typeof(DateTime));
                    dtCheckIn_MeanFlows.Columns.Add("Time", typeof(DateTime));
                    dtTransfer_MeanFlows.Columns.Add("Time", typeof(DateTime));
                    #endregion
                    #region Determine the destination columns
                    int[] tiArrivalColumns = new int[dtTable.Columns.Count];
                    int[] tiCheckInColumns = new int[dtTable.Columns.Count];
                    int[] tiTransferColumns = new int[dtTable.Columns.Count];
                    tiArrivalColumns[0] = 0;
                    tiCheckInColumns[0] = 0;
                    tiTransferColumns[0] = 0;
                    for (j = 1; j < dtTable.Columns.Count; j++)
                    {
                        String sColumnName = dtTable.Columns[j].ColumnName;
                        tiArrivalColumns[j] = -1;
                        tiCheckInColumns[j] = -1;
                        tiTransferColumns[j] = -1;
                        if (sColumnName.Contains("Arrival Infeed Group"))
                        {
                            tiArrivalColumns[j] = dtArrival_MeanFlows.Columns.Count;
                            dtArrival_MeanFlows.Columns.Add(sColumnName, typeof(Double));
                        }
                        else if (sColumnName.Contains(PAX2SIM.sCheckInGroup))
                        {
                            tiCheckInColumns[j] = dtCheckIn_MeanFlows.Columns.Count;
                            dtCheckIn_MeanFlows.Columns.Add(sColumnName, typeof(Double));
                        }
                        else if (sColumnName.Contains("Transfer Infeed Group"))
                        {
                            tiTransferColumns[j] = dtTransfer_MeanFlows.Columns.Count;
                            dtTransfer_MeanFlows.Columns.Add(sColumnName, typeof(Double));
                        }
                    }
                    #endregion
                    for (j = 0; j < dtTable.Rows.Count; j++)
                    {
                        DataRow drArrival = dtArrival_MeanFlows.NewRow();
                        DataRow drCheckIn = dtCheckIn_MeanFlows.NewRow();
                        DataRow drTransfer = dtTransfer_MeanFlows.NewRow();
                        for (int k = 0; k < dtTable.Columns.Count; k++)
                        {
                            if (tiArrivalColumns[k] != -1)
                                drArrival[tiArrivalColumns[k]] = dtTable.Rows[j][k];
                            if (tiCheckInColumns[k] != -1)
                                drCheckIn[tiCheckInColumns[k]] = dtTable.Rows[j][k];
                            if (tiTransferColumns[k] != -1)
                                drTransfer[tiTransferColumns[k]] = dtTable.Rows[j][k];
                        }
                        dtArrival_MeanFlows.Rows.Add(drArrival);
                        dtCheckIn_MeanFlows.Rows.Add(drCheckIn);
                        dtTransfer_MeanFlows.Rows.Add(drTransfer);
                    }
                    gdData.AddReplaceTable(dtArrival_MeanFlows);
                    if (GestionDonneesHUB2SIM.modeVisualisation.ContainsKey(dtArrival_MeanFlows.TableName))
                        gdData.AddReplaceModeVisualisation(dtArrival_MeanFlows.TableName, (VisualisationMode)GestionDonneesHUB2SIM.modeVisualisation[dtArrival_MeanFlows.TableName]);
                    gdData.AddReplaceTable(dtCheckIn_MeanFlows);
                    if (GestionDonneesHUB2SIM.modeVisualisation.ContainsKey(dtCheckIn_MeanFlows.TableName))
                        gdData.AddReplaceModeVisualisation(dtCheckIn_MeanFlows.TableName, (VisualisationMode)GestionDonneesHUB2SIM.modeVisualisation[dtCheckIn_MeanFlows.TableName]);
                    gdData.AddReplaceTable(dtTransfer_MeanFlows);
                    if (GestionDonneesHUB2SIM.modeVisualisation.ContainsKey(dtTransfer_MeanFlows.TableName))
                        gdData.AddReplaceModeVisualisation(dtTransfer_MeanFlows.TableName, (VisualisationMode)GestionDonneesHUB2SIM.modeVisualisation[dtTransfer_MeanFlows.TableName]);
                    gdData.removeTable(dtTable.TableName);
                }
                return true;
            }
            #endregion
        }
        #endregion



        #region  ConvertLine(DataTable dtTable, String[] OldLines, String[] NewLines)
        /// <summary>
        /// Function which create a new table like the one passed in parameters,
        /// with the new lines. Each oldline is copied with the new name.
        /// OldLines and NewLines has to have the same length.
        /// </summary>
        /// <param name="dtTable">Old table</param>
        /// <param name="OldLines">Old lines to copy ("" for creating a new line)</param>
        /// <param name="NewLines">New lines to put in the table ("" to remove the old line)</param>
        /// <returns></returns>
        public static DataTable ConvertLine(DataTable dtTable, String[] NewLines, Double[] oValues)
        {
            String[] OldLines = new string[NewLines.Length];
            int i;
            for (i = 0; i < NewLines.Length; i++)
                OldLines[i] = "";
            ArrayList alList = new ArrayList(NewLines);
            for (i = 0; i < dtTable.Rows.Count; i++)
            {
                int iIndex = alList.IndexOf(dtTable.Rows[i][0].ToString());
                if (iIndex != -1)
                    OldLines[iIndex] = dtTable.Rows[i][0].ToString();
            }



            DataTable dtResultTable = ConvertLine(dtTable, OldLines, NewLines, oValues);
            dtResultTable.AcceptChanges();
            return dtResultTable;
        }
        /// <summary>
        /// Function which create a new table like the one passed in parameters,
        /// with the new lines. Each oldline is copied with the new name.
        /// OldLines et NewLines doivent être de la même longueur.
        /// </summary>
        /// <param name="dtTable">Old table</param>
        /// <param name="OldLines">Old lines to copy ("" for creating a new line)</param>
        /// <param name="NewLines">New lines to put in the table ("" to remove the old line)</param>
        /// <returns></returns>
        private static DataTable ConvertLine(DataTable dtTable, String[] OldLines, String[] NewLines, Double[] dValue)
        {
            int j;
            if ((dValue != null) && (dValue.Length != NewLines.Length))
                dValue = null;
            //On parcours la table, et l'on change les valeurs qui ont besoin d'être modifié.
            for (j = 0; j < dtTable.Rows.Count; j++)
            {
                String sValue = dtTable.Rows[j][0].ToString();
                if (sValue.Length == 0)
                    continue;
                int iIndexLigne = OverallTools.FonctionUtiles.indexDansListe(sValue, OldLines);
                if (iIndexLigne == -1)
                    continue;
                if (NewLines[iIndexLigne] != sValue)
                    dtTable.Rows[j][0] = NewLines[iIndexLigne];
            }

            DataTable dtTableTmp = dtTable.Copy();
            dtTableTmp.Rows.Clear();
            for (j = 0; j < NewLines.Length; j++)
            {
                if (NewLines[j] == "")
                    continue;
                DataRow newRow = dtTableTmp.NewRow();
                newRow[0] = NewLines[j];
                int iIndexLigne = OverallTools.DataFunctions.indexLigne(dtTable, 0, NewLines[j]);
                if (iIndexLigne == -1)
                {
                    for (int k = 1; k < dtTableTmp.Columns.Count; k++)
                    {
                        if (dtTableTmp.Columns[k].DataType == typeof(Double))
                        {
                            if (dValue != null)
                                newRow[k] = dValue[j];
                            else
                                newRow[k] = 0;
                        }
                        else if (dtTableTmp.Columns[k].DataType == typeof(Int32))
                        {
                            if (dValue != null)
                                newRow[k] = (int)dValue[j];
                            else
                                newRow[k] = 0;
                        }
                        else if (dtTableTmp.Columns[k].DataType == typeof(String))
                            newRow[k] = "";
                    }
                }
                else
                {
                    for (int k = 1; k < dtTable.Columns.Count; k++)
                    {
                        newRow[k] = dtTable.Rows[iIndexLigne][k];
                    }
                }
                dtTableTmp.Rows.Add(newRow);
            }
            dtTableTmp.AcceptChanges();
            return dtTableTmp;
        }
        /// <summary>
        /// Function which create a new table like the one passed in parameters,
        /// with the new lines. Each oldline is copied with the new name.
        /// OldLines et NewLines doivent être de la même longueur.
        /// </summary>
        /// <param name="dtTable">Old table</param>
        /// <param name="OldLines">Old lines to copy ("" for creating a new line)</param>
        /// <param name="NewLines">New lines to put in the table ("" to remove the old line)</param>
        /// <returns></returns>
        private static DataTable ConvertLine(DataTable dtTable, String[] OldLines, String[] NewLines, Object[] toValue)
        {
            int j;
            if ((toValue != null) && (toValue.Length != NewLines.Length))
                toValue = null;
            //On parcours la table, et l'on change les valeurs qui ont besoin d'être modifié.
            for (j = 0; j < dtTable.Rows.Count; j++)
            {
                String sValue = dtTable.Rows[j][0].ToString();
                if (sValue.Length == 0)
                    continue;
                int iIndexLigne = OverallTools.FonctionUtiles.indexDansListe(sValue, OldLines);
                if (iIndexLigne == -1)
                    continue;
                if (NewLines[iIndexLigne] != sValue)
                    dtTable.Rows[j][0] = NewLines[iIndexLigne];
            }

            DataTable dtTableTmp = dtTable.Copy();
            dtTableTmp.Rows.Clear();
            for (j = 0; j < NewLines.Length; j++)
            {
                if (NewLines[j] == "")
                    continue;
                DataRow newRow = dtTableTmp.NewRow();
                newRow[0] = NewLines[j];
                int iIndexLigne = OverallTools.DataFunctions.indexLigne(dtTable, 0, NewLines[j]);
                if (iIndexLigne == -1)
                {
                    for (int k = 1; k < dtTableTmp.Columns.Count; k++)
                    {
                        if (dtTableTmp.Columns[k].DataType == typeof(Double))
                        {
                            if (toValue != null)
                                newRow[k] = toValue[j];
                            else
                                newRow[k] = 0;
                        }
                        else if (dtTableTmp.Columns[k].DataType == typeof(Int32))
                        {
                            if (toValue != null)
                                newRow[k] = (int)toValue[j];
                            else
                                newRow[k] = 0;
                        }
                        else if (dtTableTmp.Columns[k].DataType == typeof(String))
                            newRow[k] = "";
                    }
                }
                else
                {
                    for (int k = 1; k < dtTable.Columns.Count; k++)
                    {
                        newRow[k] = dtTable.Rows[iIndexLigne][k];
                    }
                }
                dtTableTmp.Rows.Add(newRow);
            }
            dtTableTmp.AcceptChanges();
            return dtTableTmp;
        }
        #endregion
        
        #region ConvertFilters(Filter fFilter, String[]sExpectedDisplayedColumn,String[]sNewColumns)
        public static bool ConvertFilter(Filter fFilter,
                                          String[] sExpectedDisplayedColumns,
                                          String[] sNewColumns)
        {
            int iDisplayedColumns = 0;
            foreach (Boolean bValue in fFilter.Display)
            {
                if (bValue)
                    iDisplayedColumns++;
            }
            foreach (String sColumnName in sExpectedDisplayedColumns)
            {
                int iIndex = fFilter.ColumnsNames.IndexOf(sColumnName);
                if (iIndex == -1)
                    return false;
                if (!(Boolean)fFilter.Display[iIndex])
                    return false;
                iDisplayedColumns--;
            }
            if (iDisplayedColumns != 0)
                return false;
            String sLastColumnName = "";
            foreach (String sColumnName in sNewColumns)
            {
                if (fFilter.ColumnsNames.Contains(sColumnName))
                {
                    sLastColumnName = sColumnName;
                    continue;
                }
                int iIndex = 0;
                if (sLastColumnName != "")
                {
                    iIndex = fFilter.ColumnsNames.IndexOf(sLastColumnName) + 1;
                }
                fFilter.ColumnsNames.Insert(iIndex, sColumnName);
                fFilter.Conditions.Insert(iIndex, "");
                fFilter.Display.Insert(iIndex, true);
                fFilter.Formules.Insert(iIndex, "[" + sColumnName + "]");
                fFilter.OperationType.Insert(iIndex, "Group");
            }
            for (int i = 0; i < fFilter.ColumnsNames.Count; i++)
            {
                if (!(bool)fFilter.Display[i])
                    continue;
                if (!OverallTools.FonctionUtiles.estPresentDansListe((String)fFilter.ColumnsNames[i], sNewColumns))
                {
                    fFilter.ColumnsNames.RemoveAt(i);
                    fFilter.Conditions.RemoveAt(i);
                    fFilter.Display.RemoveAt(i);
                    fFilter.Formules.RemoveAt(i);
                    fFilter.OperationType.RemoveAt(i);
                    i--;
                }
            }
            return true;
        }
        #endregion


        internal class ConvertColumnOutputTables
        {
            internal string ColumnName;
            internal Type ReadingType;
            internal Type GoalType;
            internal string ReadingUnit;
            internal ConvertColumnOutputTables(String ColumnName_,
                Type ReadingType_,
                Type GoalType_,
                String ReadingUnit_)
            {
                ColumnName = ColumnName_;
                ReadingType = ReadingType_;
                GoalType = GoalType_;
                ReadingUnit = ReadingUnit_;
            }
            internal Object Convert(Object oValue)
            {
                //We convert the loaded value into seconds.
                Double dConvert = 1;
                switch (ReadingUnit)
                {
                    case "Day":
                        dConvert = 60 * 60 * 24;
                        break;
                    case "Hour":
                        dConvert = 60 * 60;
                        break;
                    case "Minute":
                        dConvert = 60;
                        break;
                    case "Second":
                        dConvert =1;
                        break;
                    default :
                        break;
                }
                Double dValue=0;
                if ((oValue.GetType() == typeof(Double)) ||
                    (oValue.GetType() == typeof(Double)))
                    dValue = (Double)oValue;
                else if ((oValue.GetType() == typeof(Int32)) ||
                        (oValue.GetType() == typeof(int)))
                    dValue = (Double)((int)dValue);
                else
                    if (!Double.TryParse(oValue.ToString(), out dValue))
                        return null;

                dValue *= dConvert;
                if (GoalType == typeof(TimeSpan))
                {
                    TimeSpan tsResult = new TimeSpan(0, 0, (int)dValue);
                    return tsResult;
                }
                return oValue;
            }
        }

    }
}

