using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using SIMCORE_TOOL.Classes;
using SIMCORE_TOOL.DataManagement;

/*22/03/2012 : 
 SGE : Modifications linked to the fact that a BagPlan simulation needs files named FPA_Bags and FPD_Bags. The original
 Sodexi development weren't providing these 2 files. So a modification had been made to add these tables to the new 
 generated scenario. The modifications linked to that development would be marked with :
 //SGE-22/03/2012-Begin
 //SGE-22/03/2012-End
 */
namespace SIMCORE_TOOL.Prompt.Sodexi
{
    #region class AnalyseBagPlanClass
    class AnalyseBagPlanClass
    {
        #region Paramètres
        DataTable dtBagPlan_;
        DataTable dtFPATable_;
        DataTable dtFPDTable_;
        ExceptionTable dtMupOCT_;
        DateTime dtStartDate_;
        DateTime dtEnDate_;
        Int32 iPas_;
        Double dDwellingTime_;
        Double dDeltaTimePreviousDay_;

        int iCapaciteColisAD_ ;

        int iNbMaxColumnBefore = 4;
        #endregion

        #region Résultats
        DataTable dtStatistiquesIn;
        DataTable dtStatistiquesOut;
        ConditionnalFormatErrors cfeColors;


        #region //SGE-22/03/2012-Begin
        DataTable dtFPABags;
        DataTable dtFPDBags;
        #endregion //SGE-22/03/2012-End

        List<String> lsFiltres;
        List<String> lsErrors;
        #endregion

        #region Accesseurs
        internal DataTable StatistiquesIn
        {
            get
            {
                return dtStatistiquesIn;
            }
        }
        internal DataTable StatistiquesOut
        {
            get
            {
                return dtStatistiquesOut;
            }
        }
        internal ConditionnalFormatErrors FormatStatistiqueOut
        {
            get
            {
                return cfeColors;
            }
        }

        #region //SGE-22/03/2012-Begin
        internal DataTable FPABags
        {
            get
            {
                return dtFPABags;
            }
        }
        internal DataTable FPDBags
        {
            get
            {
                return dtFPDBags;
            }
        }
        #endregion //SGE-22/03/2012-End

        internal List<String> Filters
        {
            get
            {
                return lsFiltres;
            }
        }
        internal List<String> Errors
        {
            get
            {
                return lsErrors;
            }
        }
        #endregion

        #region Variables statiques
        static String sSuffixePoste = "Poste";
        static String sSuffixeFret = "Fret";
        static String sSuffixeAD = "AD";
        static String sSuffixeTotal = "Total";
        #endregion

        #region Constructeur Sodexi
        internal AnalyseBagPlanClass(DateTime dtStartDate, 
                        DateTime dtEnDate, 
                        Int32 iPas, 
                        Double dDwellingTime,
                        Double dDeltaTimePreviousDay,
                        Int32 iCapaciteColisAD,
                        DataTable dtBagPlan, 
                        DataTable dtFPATable, 
                        DataTable dtFPDTable,
                        NormalTable dtMupOCT)
        {
            dtBagPlan_ = dtBagPlan;
            dtStartDate_ = dtStartDate;
            dtEnDate_ = dtEnDate;
            iPas_ = iPas;
            dDwellingTime_ = dDwellingTime;
            dDeltaTimePreviousDay_ = dDeltaTimePreviousDay;
            iCapaciteColisAD_ = iCapaciteColisAD;
            dtFPATable_ = dtFPATable;
            dtFPDTable_ = dtFPDTable;

            if (dtMupOCT is DataManagement.ExceptionTable)
                dtMupOCT_ = (DataManagement.ExceptionTable)dtMupOCT;
            lsErrors = new List<string>();
            lsFiltres = new List<string>();
        }
        #endregion

        #region Class Statistiques
        class Statistiques
        {
            internal class StepStatistiques
            {
                int iFret;
                int iPoste;
                int iAD;
                internal StepStatistiques()
                {
                    iFret = 0;
                    iPoste = 0;
                    iAD = 0;
                }
                internal int Fret
                {
                    get
                    {
                        return iFret;
                    }
                }
                internal int Poste
                {
                    get
                    {
                        return iPoste;
                    }
                }
                internal int AD
                {
                    get
                    {
                        return iAD;
                    }
                }

                internal void IncFret()
                {
                    iFret++;
                }
                internal void IncPoste()
                {
                    iPoste++;
                }
                internal void IncAD()
                {
                    iAD++;
                }
            }

            Dictionary<int, StepStatistiques> dissStats;
            String ColumnName_;
            internal int[] iIndexLines;
            internal Statistiques()
            {
                dissStats = new Dictionary<int, StepStatistiques>();
            }

            #region Fonctions pour l'ajout de statistiques
            internal void intFret(int iPas)
            {
                if(!dissStats.ContainsKey(iPas))
                    dissStats.Add(iPas,new StepStatistiques());
                dissStats[iPas].IncFret();
            }
            internal void intPoste(int iPas)
            {
                if(!dissStats.ContainsKey(iPas))
                    dissStats.Add(iPas,new StepStatistiques());

                dissStats[iPas].IncPoste();
            }
            internal void intAD(int iPas)
            {
                if(!dissStats.ContainsKey(iPas))
                    dissStats.Add(iPas,new StepStatistiques());
                dissStats[iPas].IncAD();
            }
            #endregion

            internal int getFirstIndex
            {
                get
                {
                    if(dissStats == null)
                        return -1;
                    if(dissStats.Count == 0)
                        return -1;

                    int i = -1;
                    foreach (int iKey in dissStats.Keys)
                        if ((i > iKey)|| (i==-1)) 
                            i = iKey;
                    return i;
                }
            }
            internal int getLastIndex
            {
                get
                {
                    if (dissStats == null)
                        return -1;
                    if (dissStats.Count == 0)
                        return -1;

                    int i = -1;
                    foreach (int iKey in dissStats.Keys)
                        if ((i < iKey) )
                            i = iKey;
                    return i;
                }
            }
            internal StepStatistiques getStatistiques(int iPas)
            {
                if (dissStats == null)
                    return null;
                if (dissStats.ContainsKey(iPas))
                    return dissStats[iPas];
                return null;
            }

            public string ColumnName
            {
                get
                {
                    return ColumnName_;
                }
                set
                {
                    ColumnName_ = value;
                }
            }
        }
        #endregion

        #region internal bool AnalyseBagPlan()
        internal bool AnalyseBagPlan()
        {
            #region Vérifications des données d'entrée
            if (dtBagPlan_ == null)
            {
                return false;
            }
            if (dtFPATable_ == null)
                return false;
            if (dtFPDTable_ == null)
                return false;
            if (iPas_ == 0)
                return false;
            #endregion

            #region Vérification des tables.
            int iIndexTime = dtBagPlan_.Columns.IndexOf("Time(mn)");
            
            int iIndexFirstBag = dtBagPlan_.Columns.IndexOf("FirstBag");
            int iIndexID = dtBagPlan_.Columns.IndexOf("ID_PAX");
            int iIndexFPA_ID = dtBagPlan_.Columns.IndexOf("FPA_ID");
            int iIndexFPA_Class = dtBagPlan_.Columns.IndexOf("FPA_Class");
            int iIndexPaxAtReclaim = dtBagPlan_.Columns.IndexOf("PaxAtReclaim");
            int iIndexFPD_ID = dtBagPlan_.Columns.IndexOf("FPD_ID");
            ///La class permettra de connaître si le colis est du FRET ou de la POSTE
            ///1 : POSTE
            ///2 : FRET
            int iIndexFPD_Class = dtBagPlan_.Columns.IndexOf("FPD_Class");
            int iIndexSTD = dtBagPlan_.Columns.IndexOf("STD");
            int iIndexNbBags = dtBagPlan_.Columns.IndexOf("NbBags");
            /// OOG_Bag = 1 si le colis fait partie d'un colis AD qui a été 
            /// divisé. 
            int iIndexOOG_Bag = dtBagPlan_.Columns.IndexOf("OOG_Bag");
            int iIndexSegregation = dtBagPlan_.Columns.IndexOf("Segregation");
            int iIndexPassportLocal = dtBagPlan_.Columns.IndexOf("PassportLocal");
            int iIndexTransfer = dtBagPlan_.Columns.IndexOf("Transfer");
            int iIndexTermArr = dtBagPlan_.Columns.IndexOf("Term.Arr");
            int iIndexArrGate = dtBagPlan_.Columns.IndexOf("#Arr.Gate");
            int iIndexTermCI = dtBagPlan_.Columns.IndexOf("Term.CI");
            int iIndexCI = dtBagPlan_.Columns.IndexOf("#CI");

            if ((iIndexTime == -1) ||
                (iIndexID == -1) ||
                (iIndexFPA_ID == -1) ||
                (iIndexFPA_Class == -1) ||
                (iIndexPaxAtReclaim == -1) ||
                (iIndexFPD_ID == -1) ||
                (iIndexFPD_Class == -1) ||
                (iIndexSTD == -1) ||
                (iIndexNbBags == -1) ||
                (iIndexOOG_Bag == -1) ||
                (iIndexSegregation == -1) ||
                (iIndexPassportLocal == -1) ||
                (iIndexTransfer == -1) ||
                (iIndexTermArr == -1) ||
                (iIndexArrGate == -1) ||
                (iIndexTermCI == -1) ||
                (iIndexCI == -1))
            {
                return false;
            }


            Dictionary<Int32, OverallTools.TableInformations.FlightInformation> difiInformationFPD = OverallTools.TableInformations.getFlightsInformation(dtFPDTable_);
            Dictionary<Int32, OverallTools.TableInformations.FlightInformation> difiInformationFPA = OverallTools.TableInformations.getFlightsInformation(dtFPATable_);
            if(difiInformationFPD == null)
                return false;
            if(difiInformationFPA == null)
                return false;



            #region //SGE-22/03/2012-Begin
            dtFPABags = new DataTable("FPA_Bags");
            dtFPDBags = new DataTable("FPD_Bags");
            OverallTools.FonctionUtiles.initialiserTable(dtFPABags, OverallTools.StaticAnalysis.PaxPlanClass.FlightStatistics.getArrivalBagsDetailsColumns(), typeof(Int32));
            OverallTools.FonctionUtiles.initialiserTable(dtFPDBags, OverallTools.StaticAnalysis.PaxPlanClass.FlightStatistics.getDepartureBagsDetailsColumns(), typeof(Int32));
            Dictionary<int, OverallTools.StaticAnalysis.PaxPlanClass.FlightStatistics> difsArrivalStats = new Dictionary<int,OverallTools.StaticAnalysis.PaxPlanClass.FlightStatistics>();
            Dictionary<int, OverallTools.StaticAnalysis.PaxPlanClass.FlightStatistics> difsDepartureStats = new Dictionary<int, OverallTools.StaticAnalysis.PaxPlanClass.FlightStatistics>();
            #endregion //SGE-22/03/2012-End
            #endregion

            Dictionary<int, Statistiques> sStatistiquesIn = new Dictionary<int, Statistiques>();
            Dictionary<int, Statistiques> sStatistiquesOut = new Dictionary<int, Statistiques>();

            #region Boucle principale d'analyse du BagPlan
            foreach (DataRow drRow in dtBagPlan_.Rows)
            {
                Double dTime = FonctionsType.getDouble(drRow[iIndexTime]);
                int iCurrentStep = CalcStep(dTime);
                int iDepartureStep = CalcStep(dTime + dDwellingTime_);
                
                int FPA = FonctionsType.getInt(drRow[iIndexFPA_ID]);
                int FPD = FonctionsType.getInt(drRow[iIndexFPD_ID]);
                if (!sStatistiquesIn.ContainsKey(FPA))
                {
                    sStatistiquesIn.Add(FPA, new Statistiques());
                }
                if (!sStatistiquesOut.ContainsKey(FPD))
                {
                    sStatistiquesOut.Add(FPD, new Statistiques());
                }

                #region //SGE-22/03/2012-Begin

                if ((FPA != 0)&&(!difsArrivalStats.ContainsKey(FPA)))
                    difsArrivalStats.Add(FPA, new OverallTools.StaticAnalysis.PaxPlanClass.FlightStatistics(FPA,0));

                if ((FPD != 0) && (!difsDepartureStats.ContainsKey(FPD)))
                    difsDepartureStats.Add(FPD, new OverallTools.StaticAnalysis.PaxPlanClass.FlightStatistics(FPD,0));

                #endregion //SGE-22/03/2012-End

                if (FonctionsType.getInt(drRow[iIndexOOG_Bag]) > 0)
                {


                    /*SGE 17.01.2012 : Ignore le premier colis AD car il représente le colis contenant tous les colis AD*/
                    if (FonctionsType.getInt(drRow[iIndexFirstBag]) == 0)
                    {
                        //AD content (uniquement pour le out.
                        sStatistiquesOut[FPD].intAD(iDepartureStep);
                    }
                    else
                    {
                        //AD (fret de gros)
                        sStatistiquesIn[FPA].intFret(iCurrentStep);
                    }
                }
                else
                {
                    #region //SGE-22/03/2012-Begin
                    if ((FPA != 0) && (FPD != 0))
                    {
                        difsArrivalStats[FPA].AddTransferringPax(1);
                        difsDepartureStats[FPD].AddTransferringPax(1);
                    }
                    else if (FPA != 0)
                    {
                        difsArrivalStats[FPA].AddOriginatingTerminatingPax(1, 0);
                    }
                    else if (FPD != 0)
                    {
                        difsDepartureStats[FPD].AddOriginatingTerminatingPax(1, 0);
                    }
                    #endregion //SGE-22/03/2012-End

                    if (FonctionsType.getInt(drRow[iIndexFPD_Class]) == 1)
                        sStatistiquesOut[FPD].intPoste(iDepartureStep);
                    if (FonctionsType.getInt(drRow[iIndexFPA_Class]) == 1)
                        sStatistiquesIn[FPA].intPoste(iCurrentStep);
                    if (FonctionsType.getInt(drRow[iIndexFPD_Class]) == 2)
                        sStatistiquesOut[FPD].intFret(iDepartureStep);
                    if (FonctionsType.getInt(drRow[iIndexFPA_Class]) == 2)
                        sStatistiquesIn[FPA].intFret(iCurrentStep);
                    if((FonctionsType.getInt(drRow[iIndexFPD_Class]) == 0) &&
                        (FonctionsType.getInt(drRow[iIndexFPA_Class]) == 0))
                    {
                        lsErrors.Add("Unable to classify the bag " + FonctionsType.getInt(drRow[iIndexID]).ToString());
                    }
                }
            }
            #endregion

            ///Une fois arrivé ici, toutes les informations ont été tirées du BagPlan. On va donc
            ///désormais calculé la table.

            #region Vérification des liens entre id de vol et numéro de vol
            foreach (int iId in sStatistiquesIn.Keys)
            {
                if(!difiInformationFPA.ContainsKey(iId))
                {
                    lsErrors.Add("Flight ID " + iId.ToString() + " doesn't exist in the arrival flight plan");
                }
            }
            foreach (int iId in sStatistiquesOut.Keys)
            {
                if(!difiInformationFPD.ContainsKey(iId))
                {
                    lsErrors.Add("Flight ID " + iId.ToString() + " doesn't exist in the departure flight plan");
                }
            }
            #endregion

            #region Suppression des vols n'ayant pas de colis de la liste des vols (afin de ne garder que ceux intéressants).
            List<int> lsToDelete = new List<int>();
            foreach (int iId in difiInformationFPA.Keys)
            {
                if (!sStatistiquesIn.ContainsKey(iId))
                {
                    lsToDelete.Add(iId);
                }
            }
            while (lsToDelete.Count > 0)
            {
                difiInformationFPA.Remove(lsToDelete[0]);
                lsToDelete.RemoveAt(0);
            }
            foreach (int iId in difiInformationFPD.Keys)
            {
                if (!sStatistiquesOut.ContainsKey(iId))
                {
                    lsToDelete.Add(iId);
                }
            }
            while (lsToDelete.Count > 0)
            {
                difiInformationFPD.Remove(lsToDelete[0]);
                lsToDelete.RemoveAt(0);
            }
            #endregion


            #region //SGE-22/03/2012-Begin
            foreach (OverallTools.StaticAnalysis.PaxPlanClass.FlightStatistics fsTmp2 in difsDepartureStats.Values)
            {
                dtFPDBags.Rows.Add(fsTmp2.getDepartureBagsDetails());
            }
            foreach (OverallTools.StaticAnalysis.PaxPlanClass.FlightStatistics fsTmp2 in difsArrivalStats.Values)
            {
                dtFPABags.Rows.Add(fsTmp2.getArrivalBagsDetails());
            }

            dtFPDBags = OverallTools.DataFunctions.sortTable(dtFPDBags, "Flight ID");
            dtFPABags = OverallTools.DataFunctions.sortTable(dtFPABags, "Flight ID");
            #endregion //SGE-22/03/2012-End

            dtStatistiquesOut = CalculTableStatistique("StatistiquesOut", sStatistiquesOut, difiInformationFPD);
            dtStatistiquesIn = CalculTableStatistique("StatistiquesIn", sStatistiquesIn, difiInformationFPA);

            ///Il faut désormais calculé les couleurs pour l'ensemble de la table.
            cfeColors = GenerateColors(dtStatistiquesOut, dtMupOCT_, sStatistiquesOut, difiInformationFPD);
            return true;
        }
        #endregion

        #region private ConditionnalFormatErrors GenerateColors()
        private ConditionnalFormatErrors GenerateColors(DataTable dtTable,
            ExceptionTable dtMupOCT,
            Dictionary<int, Statistiques> disStatistiques,
            Dictionary<Int32, OverallTools.TableInformations.FlightInformation> difiInformationFP)
        {
            ConditionnalFormatErrors cfeResult = new ConditionnalFormatErrors();
            foreach (int iKey in disStatistiques.Keys)
            {
                int iOpeningMup = -1;
                int iClosingMup = -1;
                int iLongStorageAreaLimit = -1;

                #region Récupération des heures d'ouverture et de fermeture des Mup
                if ((dtMupOCT != null) && (difiInformationFP.ContainsKey(iKey)))
                {
                    OverallTools.TableInformations.FlightInformation fiInformation = difiInformationFP[iKey];
                    Dictionary<String, String> dssOCT_Fret = dtMupOCT.GetInformationsColumns(0, "D_" + iKey.ToString(), fiInformation.AirlineCode, fiInformation.FlightCategory);
                    if (dssOCT_Fret != null)
                    {
                        String sOpening = dssOCT_Fret[GlobalNames.sOCT_MakeUpOpening];
                        String sClosing = dssOCT_Fret[GlobalNames.sOCT_MakeUpClosing];
                        int iOpening = FonctionsType.getInt(sOpening);
                        int iClosing = FonctionsType.getInt(sClosing);
                        DateTime heureOuvertureRessource = fiInformation.Time.AddMinutes(-iOpening);
                        DateTime heureFermetureRessource = fiInformation.Time.AddMinutes(-iClosing);


                        iOpeningMup = (int)((OverallTools.DataFunctions.MinuteDifference(dtStartDate_, heureOuvertureRessource)) / iPas_);
                        iClosingMup = (int)((OverallTools.DataFunctions.MinuteDifference(dtStartDate_, heureFermetureRessource) - 0.001) / iPas_);
                    }

                    /**/
                    DateTime dtTmp = fiInformation.Time.Date.AddHours(-dDeltaTimePreviousDay_);
                    Double dMinutes = OverallTools.DataFunctions.MinuteDifference(dtTmp, fiInformation.Time);
                    iLongStorageAreaLimit = CalcStep(dMinutes);
                    DateTime dtFlightSTD = fiInformation.Time;
                    int iFlightSTDIndex = (int)((OverallTools.DataFunctions.MinuteDifference(dtStartDate_, dtFlightSTD)) / iPas_);
                    if (iFlightSTDIndex < iLongStorageAreaLimit)
                        iLongStorageAreaLimit = -1;
                    else
                        iLongStorageAreaLimit = iFlightSTDIndex - iLongStorageAreaLimit;

                }
                #endregion

                if(iLongStorageAreaLimit !=-1)
                iLongStorageAreaLimit += iNbMaxColumnBefore;
                if(iOpeningMup !=-1)
                    iOpeningMup += iNbMaxColumnBefore;
                if (iOpeningMup != -1)
                    iClosingMup += iNbMaxColumnBefore;

                #region Calcul des index pour la mise à jour de toutes les valeurs dans la table
                /*int[] iIndexes = new int[4];
                iIndexes[0] = dtTable.Columns.IndexOf(disStatistiques[iKey].ColumnName + sSuffixePoste);
                iIndexes[1] = dtTable.Columns.IndexOf(disStatistiques[iKey].ColumnName + sSuffixeFret);
                iIndexes[2] = dtTable.Columns.IndexOf(disStatistiques[iKey].ColumnName + sSuffixeAD);
                iIndexes[3] = dtTable.Columns.IndexOf(disStatistiques[iKey].ColumnName + sSuffixeTotal);*/
                /*int[] iIndexes = new int[4];
                iIndexes[0] = //OverallTools.DataFunctions.indexLigne(dtTable,0,
                    dtTable.Columns.IndexOf(disStatistiques[iKey].ColumnName + sSuffixePoste);
                iIndexes[1] = dtTable.Columns.IndexOf(disStatistiques[iKey].ColumnName + sSuffixeFret);
                iIndexes[2] = dtTable.Columns.IndexOf(disStatistiques[iKey].ColumnName + sSuffixeAD);
                iIndexes[3] = dtTable.Columns.IndexOf(disStatistiques[iKey].ColumnName + sSuffixeTotal);*/
                #endregion

                #region Parcours de toute la table et mise à jour des informations et calcul des couleurs si possible.
                for (int i = iNbMaxColumnBefore; i < dtTable.Columns.Count; i++)
                {
                    if (disStatistiques[iKey].iIndexLines != null)
                    {
                    for (int j = 0; j < 4; j++)
                    {
                            bool bNull = false;
                            String sCurrentValue = dtTable.Rows[disStatistiques[iKey].iIndexLines[j]][i].ToString();
                            if (sCurrentValue == "")
                            {
                                bNull = true;
                                dtTable.Rows[disStatistiques[iKey].iIndexLines[j]][i] = 0;
                            }
                            if (sCurrentValue == "0")
                                bNull = true;
                            if (iOpeningMup != -1)
                            {
                                if ((i < iOpeningMup) && (!bNull))
                                {
                                    if((iLongStorageAreaLimit != -1) && (i<iLongStorageAreaLimit))
                                        cfeResult.setCondition(i,disStatistiques[iKey].iIndexLines[j], Color.LightBlue);
                                    else
                                        cfeResult.setCondition(i,disStatistiques[iKey].iIndexLines[j], Color.Orange);
                                }
                                else if ((i >= iOpeningMup) && (i <= iClosingMup))
                                    cfeResult.setCondition(i,disStatistiques[iKey].iIndexLines[j], Color.LightGreen);
                                else if (!bNull)
                                    cfeResult.setCondition(i,disStatistiques[iKey].iIndexLines[j], Color.Red);
                            }
                        }
                    }
                }
                #endregion
            }
            return cfeResult;
        }
        #endregion

        #region private DataTable CalculTableStatistique()
        private DataTable CalculTableStatistique(String sTableName, 
            Dictionary<int, Statistiques> disStatistiques, 
            Dictionary<Int32, OverallTools.TableInformations.FlightInformation> difiInformationFP)
        {

            #region Initialisation de la table de résultats.
            DataTable dtStatistiques = new DataTable(sTableName);
            dtStatistiques.Columns.Add("ID", typeof(Int32));
            dtStatistiques.Columns.Add("Time", typeof(DateTime));
            dtStatistiques.Columns.Add("Flight Number", typeof(String));
            dtStatistiques.Columns.Add("Area", typeof(String));

            OverallTools.DataFunctions.initialiserColumn(dtStatistiques, dtStartDate_, dtEnDate_, iPas_, typeof(Int32));

            int iMaxColumn = dtStatistiques.Columns.Count;

            List<String> lsOrder = new List<string>();
            Dictionary<String,int> dsiCorrespondance = new Dictionary<string,int>();
            foreach (int iKey in difiInformationFP.Keys)
            {
                lsOrder.Add(iKey.ToString() + " " + difiInformationFP[iKey].Time.ToShortDateString() + " " + difiInformationFP[iKey].FlightN);
                dsiCorrespondance.Add(lsOrder[lsOrder.Count-1], iKey);
            }

            lsOrder.Sort(new OverallTools.FonctionUtiles.ColumnsComparerList());
            /*          #region Initialisation de la table de résultats.
                      dtStatistiquesIn = new DataTable(sTableName);
                      dtStatistiquesIn.Columns.Add("Time");

                      OverallTools.DataFunctions.initialiserLignes(dtStatistiquesIn, dtStartDate_, dtEnDate_, iPas_);

                      int iMaxRow = dtStatistiquesIn.Rows.Count;

                      List<String> lsOrder = new List<string>();
                      Dictionary<String, int> dsiCorrespondance = new Dictionary<string, int>();
                      foreach (int iKey in difiInformationFP.Keys)
                      {
                          lsOrder.Add(iKey.ToString() + " " + difiInformationFP[iKey].Time.ToShortDateString() + " " + difiInformationFP[iKey].FlightN);
                          dsiCorrespondance.Add(lsOrder[lsOrder.Count - 1], iKey);
                      }

                      lsOrder.Sort(new OverallTools.FonctionUtiles.ColumnsComparerList());
                      #endregion
          */
            #endregion

            foreach (String sKey in lsOrder)
            {
                int iTotalAD = 0;
                #region Initialisation de la table de résultats.
                /*iPosteIndex = dtStatistiquesIn.Columns.Count;
                iFretIndex = iPosteIndex + 1;
                iADIndex = iPosteIndex + 2;
                iTotalIndex = iPosteIndex + 3;

                dtStatistiquesIn.Columns.Add(sKey + sSuffixePoste, typeof(Int32));
                dtStatistiquesIn.Columns.Add(sKey + sSuffixeFret, typeof(Int32));
                dtStatistiquesIn.Columns.Add(sKey + sSuffixeAD, typeof(Int32));
                dtStatistiquesIn.Columns.Add(sKey + sSuffixeTotal, typeof(Int32));*/
                #endregion

                #region Création des 4 nouvelles lignes pour le vol courant.
                DataRow drPoste = dtStatistiques.NewRow();
                DataRow drFret = dtStatistiques.NewRow();
                DataRow drAD = dtStatistiques.NewRow();
                DataRow drTotal = dtStatistiques.NewRow();
                drPoste[3] = sSuffixePoste;
                drFret[3] = sSuffixeFret;
                drAD[3] = sSuffixeAD;
                drTotal[3] = sSuffixeTotal;
                #endregion

                #region Récupération des statistiques pour le vol courant
                int iFlight = dsiCorrespondance[sKey];
                Statistiques stTmp = disStatistiques[iFlight];
                #region Initialisation des lignes

                drPoste[0] = iFlight;
                drFret[0] = iFlight;
                drAD[0] = iFlight;
                drTotal[0] = iFlight;


                drPoste[1] = difiInformationFP[iFlight].Time;
                drFret[1] = difiInformationFP[iFlight].Time;
                drAD[1] = difiInformationFP[iFlight].Time;
                drTotal[1] = difiInformationFP[iFlight].Time;

                drPoste[2] = difiInformationFP[iFlight].FlightN;
                drFret[2] = difiInformationFP[iFlight].FlightN;
                drAD[2] = difiInformationFP[iFlight].FlightN;
                drTotal[2] = difiInformationFP[iFlight].FlightN;
                #endregion

                stTmp.ColumnName = sKey;
                int iPasDebut = stTmp.getFirstIndex;
                int iPasFin = stTmp.getLastIndex;
                if ((iPasDebut == -1) || (iPasFin == -1))
                {
                    //lsErrors.Add("The data for Flight " + iFlight.ToString() + "(" + sKey + ") has a wrong format.");
                    continue;
                }
                iPasDebut += iNbMaxColumnBefore;
                iPasFin += iNbMaxColumnBefore;
                #endregion

                #region Partie pour faire le calcul du nombre de colis.
                for (int i = iPasDebut; i <= iPasFin; i++)
                {
                    if(i>= iMaxColumn)
                    {
                        lsErrors.Add("The data for Flight " + iFlight.ToString() + "(" + sKey + ") is out of range (information ignored).");
                        continue;
                    }
                    Statistiques.StepStatistiques ssStats = stTmp.getStatistiques(i - iNbMaxColumnBefore);
                    if(ssStats == null)
                        continue;
                    iTotalAD += ssStats.AD;
                    int iNombreAD = 0;
                    while (iTotalAD>= iCapaciteColisAD_)
                    {
                        iNombreAD ++;
                        iTotalAD-= iCapaciteColisAD_;
                    }
                    if ((i == iPasFin) && (iTotalAD > 0)){
                        iNombreAD++;
                        iTotalAD = 0;
                    }

                    drPoste[i] = ssStats.Poste;
                    drFret[i] = ssStats.Fret;
                    drAD[i] = ssStats.AD;
                    drTotal[i] = ssStats.Poste + ssStats.Fret + iNombreAD;
                    /*dtStatistiquesIn.Rows[i][iPosteIndex] = ssStats.Poste;
                    dtStatistiquesIn.Rows[i][iFretIndex] = ssStats.Fret;
                    dtStatistiquesIn.Rows[i][iADIndex] = ssStats.AD;
                    dtStatistiquesIn.Rows[i][iTotalIndex] = ssStats.Poste + ssStats.Fret + iNombreAD;*/
                }
                #endregion

                stTmp.iIndexLines = new int[4];
                stTmp.iIndexLines[0] = dtStatistiques.Rows.Count;
                dtStatistiques.Rows.Add(drPoste);
                stTmp.iIndexLines[1] = dtStatistiques.Rows.Count;
                dtStatistiques.Rows.Add(drFret);
                stTmp.iIndexLines[2] = dtStatistiques.Rows.Count;
                dtStatistiques.Rows.Add(drAD);
                stTmp.iIndexLines[3] = dtStatistiques.Rows.Count;
                dtStatistiques.Rows.Add(drTotal);
            }
            #region Création des 4 filtres sur la table

            lsFiltres.Add(CreateFilter(dtStatistiques.TableName + " " + sSuffixePoste, dtStatistiques.TableName, dtStatistiques, sSuffixePoste));
            lsFiltres.Add(CreateFilter(dtStatistiques.TableName + " " + sSuffixeFret, dtStatistiques.TableName, dtStatistiques, sSuffixeFret));
            lsFiltres.Add(CreateFilter(dtStatistiques.TableName + " " + sSuffixeAD, dtStatistiques.TableName, dtStatistiques, sSuffixeAD));
            lsFiltres.Add(CreateFilter(dtStatistiques.TableName + " " + sSuffixeTotal, dtStatistiques.TableName, dtStatistiques, sSuffixeTotal));

            /*lsFiltres.Add(CreateFilter(dtStatistiquesIn.TableName + sSuffixePoste, dtStatistiquesIn.TableName, lsOrder, sSuffixePoste));
            lsFiltres.Add(CreateFilter(dtStatistiquesIn.TableName + sSuffixeFret, dtStatistiquesIn.TableName, lsOrder, sSuffixeFret));
            lsFiltres.Add(CreateFilter(dtStatistiquesIn.TableName + sSuffixeAD, dtStatistiquesIn.TableName, lsOrder, sSuffixeAD));
            lsFiltres.Add(CreateFilter(dtStatistiquesIn.TableName + sSuffixeTotal, dtStatistiquesIn.TableName, lsOrder, sSuffixeTotal));
             */ 
            #endregion
            return dtStatistiques;
        }
        #endregion

        #region Autres fonctions
        private static String CreateFilter(String sName, String sParentName, List<String> Columns, String suffixeColumns)
        {
            String sResult = "<Filter Name=\"" + sName + "\" ParentTable=\"" + sParentName + "\" Copy=\"False\" Locked=\"False\" InheritedVisualisationMode=\"False\">";
            
            sResult += "<Columns> <Column Name=\"Time\" Formula=\"[Time]\" Condition=\"\" Display=\"True\" OperationType=\"Group\" />";
            foreach (String sColumn in Columns)
            {
                sResult += " <Column Name=\"" + sColumn + suffixeColumns + "\" Formula=\"[" + sColumn + suffixeColumns + "]\" Condition=\"\" Display=\"True\" OperationType=\"Group\" />";
            }
            sResult += "</Columns></Filter>";
            return sResult;
        }

        private static String CreateFilter(String sName, String sParentName, DataTable dtTable, String suffixeColumns)
        {
            String sResult = "<Filter Name=\"" + sName + "\" ParentTable=\"" + sParentName + "\" Copy=\"False\" Locked=\"False\" InheritedVisualisationMode=\"False\">";

            sResult += "<Columns>";
            foreach (DataColumn dcColumn in dtTable.Columns)
            {
                if (dcColumn.ColumnName == "Area")
                {
                    sResult += " <Column Name=\"" + dcColumn.ColumnName + "\" Formula=\"[" + dcColumn.ColumnName + "]\" Condition=\"=&quot;" + suffixeColumns + "&quot;\" Display=\"True\" OperationType=\"Group\" />";
                }
                else
                {
                    sResult += " <Column Name=\"" + dcColumn.ColumnName + "\" Formula=\"[" + dcColumn.ColumnName + "]\" Condition=\"\" Display=\"True\" OperationType=\"Group\" />";
                }
            }
            sResult += "</Columns></Filter>";
            return sResult;
        }

        private int CalcStep(Double dTime)
        {
            int iTime = FonctionsType.getInt(dTime, typeof(Double));
            int iCurrentStep = 0;
            if ((iTime % iPas_) == 0)
                iCurrentStep = (iTime / iPas_)/* + 1*/;
            else
                iCurrentStep = (iTime / iPas_);
            return iCurrentStep;
        }
        #endregion
    }
    #endregion

    #region static class SodexiNames
    static class SodexiNames
    {
        #region ArrivalFlightPlan
        internal static String sArrivalFlightPlan_Date = "Date seulement du vol";
        internal static String sArrivalFlightPlan_Time = "Heure seulement du vol";
        internal static String sArrivalFlightPlan_DateTime = "Date théorique (Plan Vol)";
        internal static String sArrivalFlightPlan_FlightN = "N° Vol (Plan Vol)";
        internal static String sArrivalFlightPlan_Airport = "Destination/Provenance (Plan Vol)";
        internal static String sArrivalFlightPlan_Direction = "Sens du Vol (Plan Vol)";
        internal static String sArrivalFlightPlan_Type = "Type de vol (Plan Vol)";
        internal static String sArrivalFlightPlan_Version = "Version Avion (Plan Vol)";

        internal static String[] ArrivalFlightPlan = new String[]
        {
            sArrivalFlightPlan_Date,
            sArrivalFlightPlan_Time,
            sArrivalFlightPlan_DateTime ,
            sArrivalFlightPlan_FlightN ,
            sArrivalFlightPlan_Airport ,
            sArrivalFlightPlan_Direction,
            sArrivalFlightPlan_Type,
            sArrivalFlightPlan_Version
        };

        #endregion

        #region DepartureFlightPlan
        internal static String sDepartureFlightPlan_Date = "Date seulement du vol";
        internal static String sDepartureFlightPlan_Time = "Heure seulement du vol";
        internal static String sDepartureFlightPlan_DateTime = "Date théorique (Plan Vol)";
        internal static String sDepartureFlightPlan_FlightN = "N° Vol (Plan Vol)";
        internal static String sDepartureFlightPlan_Airport = "Destination/Provenance (Plan Vol)";
        internal static String sDepartureFlightPlan_Direction = "Sens du Vol (Plan Vol)";
        internal static String sDepartureFlightPlan_Type = "Type de vol (Plan Vol)";
        internal static String sDepartureFlightPlan_Capacity = "Capacité Avion";
        internal static String sDepartureFlightPlan_Type2 = "Type Vol";
        internal static String sDepartureFlightPlan_NombreAntenne = "Nombre antenne par vol ";
    //<< Sodexi 14.03.2012
      //internal static String sDepartureFlightPlan_Version = "Version Avion (Plan Vol)";
        internal static String sDepartureFlightPlan_Version = "Vol AD";
    //>> Sodexi 14.03.2012
        internal static String sDepartureFlightPlan_CapaMaxItems = "Capacité Max (Item)";
        internal static String sDepartureFlightPlan_PoidMax = "Poids Max (Kg)";

        internal static String sDepartureFlightPlan_VolumeMax = "Volume Max (m3)";


        internal static String[] DepartureFlightPlan = new String[]
        {
            sDepartureFlightPlan_Date,
            sDepartureFlightPlan_Time,
            sDepartureFlightPlan_DateTime ,
            sDepartureFlightPlan_FlightN ,
            sDepartureFlightPlan_Airport ,
            sDepartureFlightPlan_Direction,
            sDepartureFlightPlan_Type ,
            sDepartureFlightPlan_Capacity ,
            sDepartureFlightPlan_Type2 ,
            sDepartureFlightPlan_NombreAntenne,
            sDepartureFlightPlan_Version ,
            sDepartureFlightPlan_CapaMaxItems,
            sDepartureFlightPlan_PoidMax,
            sDepartureFlightPlan_VolumeMax
        };
        #endregion

        #region Mix
        internal static String sMix_Destinations = "Destinations mixables";


        internal static String[] Mix = new String[]
        {
            sMix_Destinations
        };
        #endregion

        #region FretPoste
        internal static String sFretPoste_DateTime = "Date vol";
        internal static String sFretPoste_FlightN = "N° Vol d'apport";
        internal static String sFretPoste_Airport = "Provenance vol";
        internal static String sFretPoste_NLTA = "N° LTA";
        internal static String sFretPoste_Objet = "Objet";
        internal static String sFretPoste_NBObjets = "Nb Objets";
        internal static String sFretPoste_Poids = "Poids";
        internal static String sFretPoste_Destination = "Destination";
        internal static String sFretPoste_Destinataire = "Destinataire";
        internal static String sFretPoste_SPL1 = "SPL 1 (Préalerte)";
        internal static String sFretPoste_SPL2 = "SPL 2 (Préalerte)";
        internal static String sFretPoste_SPL3 = "SPL 3 (Préalerte)";
        internal static String sFretPoste_Antenne = "Antenne ou sortie";
        internal static String sFretPoste_CLR = "Objet Import CHR Poste";
        internal static String sFretPoste_CYM = "Objet Import CYM Poste";
        internal static String sFretPoste_NotCLRCYM = "Objet Import FR Poste pas CYM et pas CHR";

        internal static String sFretPoste_Longueur = "Longeur (m)";
        internal static String sFretPoste_Largeur = "Largeur (m)";
        internal static String sFretPoste_Hauteur = "Hauteur (m)";
        internal static String sFretPoste_Volume = "Volume (m3)";
        // << Sodexi Task#7495 Modification of Sodexi Project
        internal static String sFretPoste_StatusDouanier = "Status Douanier";
        // >> Sodexi Task#7495 Modification of Sodexi Project


        internal static String[] FretPoste = new String[]
        {
             sFretPoste_DateTime,
             sFretPoste_FlightN ,
             sFretPoste_Airport,
             sFretPoste_NLTA ,
             sFretPoste_Objet ,
             sFretPoste_NBObjets ,
             sFretPoste_Poids ,
             sFretPoste_Destination ,
             sFretPoste_Destinataire,
             sFretPoste_SPL1 ,
             sFretPoste_SPL2 ,
             sFretPoste_SPL3 ,
             sFretPoste_Antenne,
             sFretPoste_CLR,
             sFretPoste_CYM ,
             sFretPoste_NotCLRCYM,
             sFretPoste_Longueur,
             sFretPoste_Largeur ,
             sFretPoste_Hauteur ,
             sFretPoste_Volume
             // << Sodexi Task#7495 Modification of Sodexi Project
             , sFretPoste_StatusDouanier
             // >> Sodexi Task#7495 Modification of Sodexi Project             
        };
        #endregion

        #region Parameters
        internal static String sParameters_Name = "Name";
        internal static String sParameters_Value = "Value";


        internal static String[] Parameters = new String[]
        {
            sParameters_Name,
            sParameters_Value
        };
        #endregion

        // << Sodexi Task#7129 Bagplan Update
        #region LocalAirports
        internal static String sLocalAirports_Country = "Country";
        internal static String sLocalAirports_Code = "Code";

        internal static String[] LocalAirports = new String[]
        {
            sLocalAirports_Country,
            sLocalAirports_Code
        };
        #endregion
        // >> Sodexi Task#7129 Bagplan Update

        #region Autre
        internal static String sDepartureFlightPlan_ = "";
        #endregion

        #region Mots clefs contenus dans colonnes
        internal static String sTypeVol_PetitVrac = "PETIT VRAC";
        internal static String sTypeVol_G02 = "G02";



        /// <summary>
        /// Not Used
        /// </summary>
        internal static String sAntenneSortie_G02FAP = "G02 FAP";
        internal static String sAntenneSortie_G02Import = "G02 IMPORT";
        /// <summary>
        /// Not Used
        /// </summary>
        internal static String sAntenneSortie_G02ImportNonMeca = "G02 IMPORT NON MECA"; 
        /// <summary>
        /// Not Used
        /// </summary>
        internal static String sAntenneSortie_G02TransitNonMeca = "G02 TRANSIT NON MECA";
        internal static String sAntenneSortie_PosteExport = "POSTE EXPORT";
        internal static String sAntenneSortie_PosteImport = "POSTE IMPORT";
        internal static String sAntenneSortie_PosteCHR = "POSTE IMPORT CHR";
        internal static String sAntenneSortie_PosteCYM = "POSTE IMPORT CYM";

        internal static String sAntenneSortie_AD = "AD";
        internal static String sAntenneSortie_ADContent = "AD CONTENT";
        internal static String sAntenneSortie_FRET = "FRET";



        internal static String sCDGImport = "CDG IMPORT";
        internal static String sPosteImport = "POSTE IMPORT";
        internal static String sPosteImportCHR = "POSTE IMPORT CHR";
        internal static String sPosteImportCYM = "POSTE IMPORT CYM";
        internal static String sADArea = "AD Area"; //only AD items

        internal static String[] tsAirportNames = new String[]
        {
            sCDGImport ,
            sPosteImport , 
            sPosteImportCHR,
            sPosteImportCYM,
            sADArea 
        };

        internal static String sUnknown = "Unknown";

        #endregion

        internal static String sFlightCategorie_Mix = "Mix";
        internal static String sFlightCategorie_NoMix = "NoMix";
        
        internal static Double[]oDefaultColumnMakeUp = new Double[] { 120, 30, 90, 1, 120, 3, 3, 40, 60, 3 };

        #region Vérification des tables
        public static Boolean IsValid(DataTable dtTable, String[] tsColumns)
        {
            if (dtTable == null)
                return false;
            if (tsColumns == null)
                return true;
            if (tsColumns.Length == 0)
                return true;
            List<String> lsColumn = new List<string>(tsColumns);
            foreach (String sColumnName in lsColumn)
            {
                if (!dtTable.Columns.Contains(sColumnName))
                    return false;
            }
            return true;
        }
        #endregion
    }
    #endregion

    #region class ConversionPlanDeVols
    class ConversionPlanDeVols
    {
        #region Paramètres
        DataTable dtArrivalFlightPlan_;
        DataTable dtDepartureFlightPlan_;
        DataTable dtMixable_;
        int iParamImportStart_;
        int iParamImportEnd_;

        int iParamImportPosteStart_;
        int iParamImportPosteEnd_;

        int iParamImportPosteCHRStart_;
        int iParamImportPosteCHREnd_;

        int iParamImportPosteCYMStart_;
        int iParamImportPosteCYMEnd_;

        int iParamADAreaStart_;
        int iParamADAreaEnd_;
        #endregion

        #region Résultats
        DataTable dtFPDTable;
        DataTable dtFPATable;
        DataTable dtAircraftException;
        DataTable dtMakeUpException;

        Dictionary<Int32,String> lsAircraftLooseForDeparture;

        Dictionary<Int32, Int32> diiMakeUpExceptionDeparture;

        internal DataTable FPDTable
        {
            get
            {
                return dtFPDTable;
            }
        }
        internal DataTable FPATable
        {
            get
            {
                return dtFPATable;
            }
        }
        
        internal DataTable AircraftException
        {
            get
            {
                return dtAircraftException;
            }
        }
        internal DataTable MakeUpException
        {
            get
            {
                return dtMakeUpException;
            }
        }
        
        #endregion

        #region Constructeurs
        internal ConversionPlanDeVols(DataTable dtArrivalFlightPlan, 
            DataTable dtDepartureFlightPlan, 
            DataTable dtMixable,
            int iParamImportStart,
            int iParamImportEnd,

            int iParamImportPosteStart,
            int iParamImportPosteEnd,
            int iParamImportPosteCHRStart,
            int iParamImportPosteCHREnd,
            int iParamImportPosteCYMStart,
            int iParamImportPosteCYMEnd,
            int iParamADAreaStart,
            int iParamADAreaEnd)
        {
            dtArrivalFlightPlan_ = dtArrivalFlightPlan;
            dtDepartureFlightPlan_ = dtDepartureFlightPlan;
            dtMixable_ = dtMixable;
            iParamImportStart_ = iParamImportStart;
            iParamImportEnd_ = iParamImportEnd;

            iParamImportPosteStart_ = iParamImportPosteStart;
            iParamImportPosteEnd_ = iParamImportPosteEnd;
            iParamImportPosteCHRStart_ = iParamImportPosteCHRStart;
            iParamImportPosteCHREnd_ = iParamImportPosteCHREnd;
            iParamImportPosteCYMStart_ = iParamImportPosteCYMStart;
            iParamImportPosteCYMEnd_ = iParamImportPosteCYMEnd;
            iParamADAreaStart_ = iParamADAreaStart;
            iParamADAreaEnd_ = iParamADAreaEnd;
        }
        #endregion

        #region Fonctions pour effectuer les conversions.
        internal void ConvertFlightPlan()
        {
            dtFPATable = ConvertArrivalFlightPlan();
            dtFPDTable = ConvertDepartureFlightPlan();
            if(diiMakeUpExceptionDeparture != null)
            {
                dtMakeUpException = GenerateMakeUpExceptions();
            }
            if(lsAircraftLooseForDeparture !=null)
            {
                dtAircraftException = GenerateAircraftExceptions();
            }
        }
        private DataTable ConvertArrivalFlightPlan()
        {
            #region Vérification de la table en entrée.
            if (dtArrivalFlightPlan_ == null)
                return null;
            if (dtArrivalFlightPlan_.Rows.Count == 0)
                return null;
            if (!SodexiNames.IsValid(dtArrivalFlightPlan_, SodexiNames.ArrivalFlightPlan))
                return null;

            int iIndex_Date = dtArrivalFlightPlan_.Columns.IndexOf(SodexiNames.sArrivalFlightPlan_Date);
            int iIndex_Time = dtArrivalFlightPlan_.Columns.IndexOf(SodexiNames.sArrivalFlightPlan_Time);
            int iIndex_DateTime = dtArrivalFlightPlan_.Columns.IndexOf(SodexiNames.sArrivalFlightPlan_DateTime);
            int iIndex_FlightN = dtArrivalFlightPlan_.Columns.IndexOf(SodexiNames.sArrivalFlightPlan_FlightN);
            int iIndex_Airport = dtArrivalFlightPlan_.Columns.IndexOf(SodexiNames.sArrivalFlightPlan_Airport);
            int iIndex_Direction = dtArrivalFlightPlan_.Columns.IndexOf(SodexiNames.sArrivalFlightPlan_Direction);
            int iIndex_Type = dtArrivalFlightPlan_.Columns.IndexOf(SodexiNames.sArrivalFlightPlan_Type);
            int iIndex_Version = dtArrivalFlightPlan_.Columns.IndexOf(SodexiNames.sArrivalFlightPlan_Version);

            #endregion

            DataManagerInput.LoadParameters lpParams = DataManagerInput.getParameters(GlobalNames.FPATableName);
            if (lpParams == null)
                return null;
            DataTable dtResult = lpParams.InitializeTable();
            dtResult.TableName = GlobalNames.FPATableName;

            int iId = 1;



            foreach (DataRow drRow in dtArrivalFlightPlan_.Rows)
            {
                DateTime dtDateTime = FonctionsType.getDate(drRow[iIndex_DateTime]);
                String sFlightN = FonctionsType.getString(drRow[iIndex_FlightN]);
                String sAirport = FonctionsType.getString(drRow[iIndex_Airport]);
                String sAircraft = FonctionsType.getString(drRow[iIndex_Type]);

                List<String> lsAirport = new List<string>();
                if (sAirport.Contains(","))
                {
                    lsAirport.AddRange(sAirport.Split(new char[] { ',' }));
                }
                else
                {
                    lsAirport.Add(sAirport);
                }

                foreach (String sAirportTmp in lsAirport)
                {

                    DataRow drNewRow = dtResult.NewRow();
                    drNewRow[GlobalNames.sFPD_A_Column_ID] = iId;

                    iId++;
                    drNewRow[GlobalNames.sFPD_A_Column_DATE] = dtDateTime.Date;
                    drNewRow[GlobalNames.sFPA_Column_STA] = dtDateTime.TimeOfDay;
                    if (sFlightN.Length >= 2)
                        drNewRow[GlobalNames.sFPD_A_Column_AirlineCode] = sFlightN.Substring(0, 2);
                    else
                        drNewRow[GlobalNames.sFPD_A_Column_AirlineCode] = SodexiNames.sUnknown;
                    drNewRow[GlobalNames.sFPD_A_Column_FlightN] = sFlightN;
                    drNewRow[GlobalNames.sFPD_A_Column_AirportCode] = sAirportTmp;
                    drNewRow[GlobalNames.sFPD_A_Column_FlightCategory] = "Arrival";
                    drNewRow[GlobalNames.sFPD_A_Column_AircraftType] = sAircraft;

                    drNewRow[GlobalNames.sFPA_Column_NoBSM] = false;
                    drNewRow[GlobalNames.sFPA_Column_CBP] = false;
                    drNewRow[GlobalNames.sFPD_A_Column_NbSeats] = 0;
                    drNewRow[GlobalNames.sFPD_A_Column_TerminalGate] = 1;
                    drNewRow[GlobalNames.sFPA_Column_ArrivalGate] = 1;
                    drNewRow[GlobalNames.sFPA_Column_TerminalReclaim] = 1;
                    drNewRow[GlobalNames.sFPA_Column_ReclaimObject] = 1;
                    drNewRow[GlobalNames.sFPA_Column_TerminalInfeedObject] = 1;
                    drNewRow[GlobalNames.sFPA_Column_StartArrivalInfeedObject] = 1;
                    drNewRow[GlobalNames.sFPA_Column_EndArrivalInfeedObject] = 1;
                    drNewRow[GlobalNames.sFPA_Column_TransferInfeedObject] = 1;
                    drNewRow[GlobalNames.sFPD_A_Column_TerminalParking] = 0;
                    drNewRow[GlobalNames.sFPD_A_Column_Parking] = 0;
                    drNewRow[GlobalNames.sFPD_A_Column_RunWay] = 0;
                    drNewRow[GlobalNames.sFPD_A_Column_User1] = "";
                    drNewRow[GlobalNames.sFPD_A_Column_User2] = "";
                    drNewRow[GlobalNames.sFPD_A_Column_User3] = "";
                    drNewRow[GlobalNames.sFPD_A_Column_User4] = "";
                    drNewRow[GlobalNames.sFPD_A_Column_User5] = "";
                    dtResult.Rows.Add(drNewRow);
                }
            }
            return dtResult;
        }
        
        private DataTable ConvertDepartureFlightPlan()
        {
            #region Vérification de la table en entrée.
            if (dtDepartureFlightPlan_ == null)
                return null;
            if (dtMixable_ == null)
                return null;
            if (dtDepartureFlightPlan_.Rows.Count == 0)
                return null;
            if (!SodexiNames.IsValid(dtDepartureFlightPlan_, SodexiNames.DepartureFlightPlan))
                return null;

            if (!SodexiNames.IsValid(dtMixable_, SodexiNames.Mix))
                return null;

            int iIndex_Mix = dtMixable_.Columns.IndexOf(SodexiNames.sMix_Destinations);
            List<String> lsMixable = new List<string>();
            foreach (DataRow drRow in dtMixable_.Rows)
            {
                lsMixable.Add(FonctionsType.getString(drRow[iIndex_Mix]));
            }


            int iIndex_Date = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_Date);
            int iIndex_Time = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_Time);
            int iIndex_DateTime = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_DateTime);
            int iIndex_FlightN = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_FlightN);
            int iIndex_Airport = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_Airport);
            int iIndex_Direction = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_Direction);
            int iIndex_Type = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_Type);
            int iIndex_Version = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_Version);
            int iIndex_Capacity = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_Capacity);
            int iIndex_Type2 = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_Type2);

            int iIndex_NombreAntenne = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_NombreAntenne);
            int iIndex_CapaMax = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_CapaMaxItems);
            int iIndex_PoidMax = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_PoidMax);
            int iIndex_VolumeMax = dtDepartureFlightPlan_.Columns.IndexOf(SodexiNames.sDepartureFlightPlan_VolumeMax);
            DataTable dtTmp = dtDepartureFlightPlan_.Clone();
            dtTmp.Columns [iIndex_DateTime].DataType = typeof(DateTime);
            foreach (DataRow drRow in dtDepartureFlightPlan_.Rows)
            {
                dtTmp.Rows.Add(drRow.ItemArray);
            }

            dtTmp = OverallTools.DataFunctions.sortTable(dtTmp, SodexiNames.sDepartureFlightPlan_DateTime);

            #endregion

            DataManagerInput.LoadParameters lpParams = DataManagerInput.getParameters(GlobalNames.FPDTableName);
            if (lpParams == null)
                return null;
            DataTable dtResult = lpParams.InitializeTable();
            dtResult.TableName = GlobalNames.FPDTableName;

            int iId = 1;

            DateTime dtMin = DateTime.MaxValue;
            DateTime dtMax = DateTime.MinValue;

            #region Parcours des vols et création d'un vol pour chaque.
            foreach (DataRow drRow in dtTmp.Rows)
            {
                DateTime dtDateTime = FonctionsType.getDate(drRow[iIndex_DateTime]);
                if ((dtMin != DateTime.MaxValue)&&(dtMin != dtDateTime.Date))
                {
                    DataRow []drRows = CreateRows(dtResult, iId, dtMin);
                    foreach (DataRow drTmp in drRows)
                    {
                        dtResult.Rows.Add(drTmp);
                    }
                    iId += drRows.Length;
                }
                dtMin = dtDateTime.Date;
                /*
                if (dtDateTime > dtMax)
                    dtMax = dtDateTime;
                if (dtDateTime < dtMin)
                    dtMin = dtDateTime;*/
                String sFlightN = FonctionsType.getString(drRow[iIndex_FlightN]);
                String sAirport = FonctionsType.getString(drRow[iIndex_Airport]);
                String sAircraft = FonctionsType.getString(drRow[iIndex_Type]);

                List<String> lsAirport = new List<string>();
                if (sAirport.Contains(","))
                {
                    lsAirport.AddRange(sAirport.Split(new char[] { ',' }));
                }
                else
                {
                    lsAirport.Add(sAirport);
                }

                foreach (String sAirportTmp in lsAirport)
                {

                    String sType2 = FonctionsType.getString(drRow[iIndex_Type2]);
                    DataRow drNewRow = dtResult.NewRow();
                    bool bMakeUpAllocated = false;
                    if(sType2 == SodexiNames.sTypeVol_PetitVrac)
                    {
                        if (lsAircraftLooseForDeparture == null)
                            lsAircraftLooseForDeparture = new Dictionary<int, string>();
                        lsAircraftLooseForDeparture.Add(iId,sAircraft);
                    }
                    else if( sType2 == SodexiNames.sTypeVol_G02)
                    {
                        bMakeUpAllocated = true;
                        drNewRow[GlobalNames.sFPD_Column_Eco_Mup_Start] = iParamImportStart_;
                        drNewRow[GlobalNames.sFPD_Column_Eco_Mup_End] = iParamImportEnd_;
                        drNewRow[GlobalNames.sFPD_Column_First_Mup_Start] = iParamImportStart_;
                        drNewRow[GlobalNames.sFPD_Column_First_Mup_End] = iParamImportEnd_;

                        if (diiMakeUpExceptionDeparture == null)
                            diiMakeUpExceptionDeparture = new Dictionary<int, int>();
                        diiMakeUpExceptionDeparture.Add(iId, FonctionsType.getInt(drRow[iIndex_NombreAntenne])); 
                    }else if (FonctionsType.getInt(drRow[iIndex_NombreAntenne]) > 0)
                    {
                        if (diiMakeUpExceptionDeparture == null)
                            diiMakeUpExceptionDeparture = new Dictionary<int, int>();
                        diiMakeUpExceptionDeparture.Add(iId, FonctionsType.getInt(drRow[iIndex_NombreAntenne])); 
                    }

                    drNewRow[GlobalNames.sFPD_A_Column_ID] = iId;
                    iId++;
                    drNewRow[GlobalNames.sFPD_A_Column_DATE] = dtDateTime.Date;
                    drNewRow[GlobalNames.sFPD_Column_STD] = dtDateTime.TimeOfDay;
                    if (sFlightN.Length >= 2)
                        drNewRow[GlobalNames.sFPD_A_Column_AirlineCode] = sFlightN.Substring(0, 2);
                    else
                        drNewRow[GlobalNames.sFPD_A_Column_AirlineCode] = SodexiNames.sUnknown;
                    drNewRow[GlobalNames.sFPD_A_Column_FlightN] = sFlightN;
                    drNewRow[GlobalNames.sFPD_A_Column_AirportCode] = sAirportTmp;

                    
                    if(lsMixable.Contains(sAirportTmp))
                        drNewRow[GlobalNames.sFPD_A_Column_FlightCategory] = SodexiNames.sFlightCategorie_Mix;
                    else
                        drNewRow[GlobalNames.sFPD_A_Column_FlightCategory] = SodexiNames.sFlightCategorie_NoMix;



                    drNewRow[GlobalNames.sFPD_A_Column_AircraftType] = sAircraft;

                    drNewRow[GlobalNames.sFPD_Column_TSA] = false;
                    drNewRow[GlobalNames.sFPD_A_Column_NbSeats] = 0;

                    drNewRow[GlobalNames.sFPD_Column_TerminalCI] = 1;
                    drNewRow[GlobalNames.sFPD_Column_Eco_CI_Start] = 1;
                    drNewRow[GlobalNames.sFPD_Column_Eco_CI_End] = 1;
                    drNewRow[GlobalNames.sFPD_Column_FB_CI_Start] = 1;
                    drNewRow[GlobalNames.sFPD_Column_FB_CI_End] = 1;

                    drNewRow[GlobalNames.sFPD_Column_Eco_Drop_Start] = 1;
                    drNewRow[GlobalNames.sFPD_Column_Eco_Drop_End] = 1;
                    drNewRow[GlobalNames.sFPD_Column_FB_Drop_Start] = 1;
                    drNewRow[GlobalNames.sFPD_Column_FB_Drop_End] = 1;


                    drNewRow[GlobalNames.sFPD_A_Column_TerminalGate] = 1;
                    drNewRow[GlobalNames.sFPD_Column_BoardingGate] = 1;


                    drNewRow[GlobalNames.sFPD_Column_TerminalMup] = 1;
                    if (!bMakeUpAllocated)
                    {
                        drNewRow[GlobalNames.sFPD_Column_Eco_Mup_Start] = 0;
                        drNewRow[GlobalNames.sFPD_Column_Eco_Mup_End] = 0;
                        drNewRow[GlobalNames.sFPD_Column_First_Mup_Start] = 0;
                        drNewRow[GlobalNames.sFPD_Column_First_Mup_End] = 0;
                    }

                    drNewRow[GlobalNames.sFPD_A_Column_TerminalParking] = 0;
                    drNewRow[GlobalNames.sFPD_A_Column_Parking] = 0;
                    drNewRow[GlobalNames.sFPD_A_Column_RunWay] = 0;
                    drNewRow[GlobalNames.sFPD_A_Column_User1] = drRow[iIndex_CapaMax];
                    drNewRow[GlobalNames.sFPD_A_Column_User2] = drRow[iIndex_PoidMax];
                    drNewRow[GlobalNames.sFPD_A_Column_User3] = drRow[iIndex_VolumeMax];
                  //<< Sodexi 14.03.2012
                    drNewRow[GlobalNames.sFPD_A_Column_User4] = drRow[iIndex_Version];
                  //>> Sodexi 14.03.2012
                  //<< Sodexi 27.03.2012 Add "Type Vol" from Sodexi 
                  // into the column User5 from FPD
                    drNewRow[GlobalNames.sFPD_A_Column_User5] = drRow[iIndex_Type2];
                  //>> Sodexi 27.03.2012
                    dtResult.Rows.Add(drNewRow);
                }
            }
            #endregion

            #region Ajout d'un vol par jour pour les colis en import
            if (dtMin != DateTime.MaxValue)
            {
                DataRow[] drRows = CreateRows(dtResult, iId, dtMin);
                foreach (DataRow drTmp in drRows)
                {
                    dtResult.Rows.Add(drTmp);
                }
                iId += 4;
            }

            /*dtMin = dtMin.Date;
            dtMax = dtMax.Date;
            while (dtMin <= dtMax)
            {
                for (int i = 0; i < 4; i++)
                {
                    DataRow drNewRow = dtResult.NewRow();
                    drNewRow[GlobalNames.sFPD_A_Column_ID] = iId;
                    iId++;
                    drNewRow[GlobalNames.sFPD_A_Column_DATE] = dtMin ;
                    drNewRow[GlobalNames.sFPD_Column_STD] = new TimeSpan(23,59,00);
                    drNewRow[GlobalNames.sFPD_A_Column_AirlineCode] = SodexiNames.sUnknown;
                    drNewRow[GlobalNames.sFPD_A_Column_FlightN] = SodexiNames.sUnknown;
                    drNewRow[GlobalNames.sFPD_A_Column_AirportCode] = SodexiNames.tsAirportNames[i];


                    drNewRow[GlobalNames.sFPD_A_Column_FlightCategory] = SodexiNames.sUnknown;
                    


                    drNewRow[GlobalNames.sFPD_A_Column_AircraftType] = SodexiNames.sUnknown;

                    drNewRow[GlobalNames.sFPD_Column_TSA] = false;
                    drNewRow[GlobalNames.sFPD_A_Column_NbSeats] = 0;

                    drNewRow[GlobalNames.sFPD_Column_TerminalCI] = 1;
                    drNewRow[GlobalNames.sFPD_Column_Eco_CI_Start] = 1;
                    drNewRow[GlobalNames.sFPD_Column_Eco_CI_End] = 1;
                    drNewRow[GlobalNames.sFPD_Column_FB_CI_Start] = 1;
                    drNewRow[GlobalNames.sFPD_Column_FB_CI_End] = 1;

                    drNewRow[GlobalNames.sFPD_Column_Eco_Drop_Start] = 1;
                    drNewRow[GlobalNames.sFPD_Column_Eco_Drop_End] = 1;
                    drNewRow[GlobalNames.sFPD_Column_FB_Drop_Start] = 1;
                    drNewRow[GlobalNames.sFPD_Column_FB_Drop_End] = 1;


                    drNewRow[GlobalNames.sFPD_A_Column_TerminalGate] = 1;
                    drNewRow[GlobalNames.sFPD_Column_BoardingGate] = 1;


                    drNewRow[GlobalNames.sFPD_Column_TerminalMup] = 1;
                    drNewRow[GlobalNames.sFPD_Column_Eco_Mup_Start] = 0;
                    drNewRow[GlobalNames.sFPD_Column_Eco_Mup_End] = 0;
                    drNewRow[GlobalNames.sFPD_Column_First_Mup_Start] = 0;
                    drNewRow[GlobalNames.sFPD_Column_First_Mup_End] = 0;

                    drNewRow[GlobalNames.sFPD_A_Column_Parking] = 0;
                    drNewRow[GlobalNames.sFPD_A_Column_RunWay] = 0;
                    drNewRow[GlobalNames.sFPD_A_Column_User1] = "300000000";
                    drNewRow[GlobalNames.sFPD_A_Column_User2] = "300000000";
                    drNewRow[GlobalNames.sFPD_A_Column_User3] = "300000000";
                    drNewRow[GlobalNames.sFPD_A_Column_User4] = "";
                    drNewRow[GlobalNames.sFPD_A_Column_User5] = "";
                    dtResult.Rows.Add(drNewRow);
                }
                dtMin = dtMin.AddDays(1);
            }*/
            #endregion
            return dtResult;
        }

        private DataRow[] CreateRows(DataTable dtTmp, int iId, DateTime dtDate)
        {
            DataRow[] drNewRows = new DataRow[SodexiNames.tsAirportNames.Length];
            for (int i = 0; i < SodexiNames.tsAirportNames.Length; i++)
            {
                drNewRows[i] = dtTmp.NewRow();
                drNewRows[i][GlobalNames.sFPD_A_Column_ID] = iId;
                iId++;
                drNewRows[i][GlobalNames.sFPD_A_Column_DATE] = dtDate;
                drNewRows[i][GlobalNames.sFPD_Column_STD] = new TimeSpan(23, 59, 00);
                drNewRows[i][GlobalNames.sFPD_A_Column_AirlineCode] = SodexiNames.tsAirportNames[i];
                drNewRows[i][GlobalNames.sFPD_A_Column_FlightN] = SodexiNames.tsAirportNames[i];
                drNewRows[i][GlobalNames.sFPD_A_Column_AirportCode] = SodexiNames.tsAirportNames[i];

                drNewRows[i][GlobalNames.sFPD_A_Column_FlightCategory] = SodexiNames.tsAirportNames[i];

                drNewRows[i][GlobalNames.sFPD_A_Column_AircraftType] = SodexiNames.sUnknown;

                drNewRows[i][GlobalNames.sFPD_Column_TSA] = false;
                drNewRows[i][GlobalNames.sFPD_A_Column_NbSeats] = 0;

                drNewRows[i][GlobalNames.sFPD_Column_TerminalCI] = 1;
                drNewRows[i][GlobalNames.sFPD_Column_Eco_CI_Start] = 1;
                drNewRows[i][GlobalNames.sFPD_Column_Eco_CI_End] = 1;
                drNewRows[i][GlobalNames.sFPD_Column_FB_CI_Start] = 1;
                drNewRows[i][GlobalNames.sFPD_Column_FB_CI_End] = 1;

                drNewRows[i][GlobalNames.sFPD_Column_Eco_Drop_Start] = 1;
                drNewRows[i][GlobalNames.sFPD_Column_Eco_Drop_End] = 1;
                drNewRows[i][GlobalNames.sFPD_Column_FB_Drop_Start] = 1;
                drNewRows[i][GlobalNames.sFPD_Column_FB_Drop_End] = 1;


                drNewRows[i][GlobalNames.sFPD_A_Column_TerminalGate] = 1;
                drNewRows[i][GlobalNames.sFPD_Column_BoardingGate] = 1;


                drNewRows[i][GlobalNames.sFPD_Column_TerminalMup] = 1;
                int iStart = 0;
                int iEnd = 0;
                if(i==0)
                {
                    iStart = iParamImportStart_;
                    iEnd = iParamImportEnd_;
                }
                else if (i == 1)
                {
                    iStart = iParamImportPosteStart_;
                    iEnd = iParamImportPosteEnd_;
                }
                else if (i == 2)
                {
                    iStart = iParamImportPosteCHRStart_;
                    iEnd = iParamImportPosteCHREnd_;
                }
                else if (i == 3)
                {
                    iStart = iParamImportPosteCYMStart_;
                    iEnd = iParamImportPosteCYMEnd_;
                }
                else if (i == 4)
                {
                    iStart = iParamADAreaStart_;
                    iEnd = iParamADAreaEnd_;
                }
                drNewRows[i][GlobalNames.sFPD_Column_Eco_Mup_Start] = iStart;
                drNewRows[i][GlobalNames.sFPD_Column_Eco_Mup_End] = iEnd;
                drNewRows[i][GlobalNames.sFPD_Column_First_Mup_Start] = iStart;
                drNewRows[i][GlobalNames.sFPD_Column_First_Mup_End] = iEnd;

                drNewRows[i][GlobalNames.sFPD_A_Column_TerminalParking] = 0;
                drNewRows[i][GlobalNames.sFPD_A_Column_Parking] = 0;
                drNewRows[i][GlobalNames.sFPD_A_Column_RunWay] = 0;
                drNewRows[i][GlobalNames.sFPD_A_Column_User1] = "300000000";
                drNewRows[i][GlobalNames.sFPD_A_Column_User2] = "300000000";
                drNewRows[i][GlobalNames.sFPD_A_Column_User3] = "300000000";
                drNewRows[i][GlobalNames.sFPD_A_Column_User4] = "";
                drNewRows[i][GlobalNames.sFPD_A_Column_User5] = "";
            }
            return drNewRows;
        }

        private DataTable GenerateAircraftExceptions()
        {
            DataTable dtResult = new DataTable(GlobalNames.Flight);
            
            dtResult.Columns.Add(GlobalNames.Flight, typeof(String));
            dtResult.Columns.Add(GlobalNames.sFPAircraft_AircraftTypes, typeof(String));
            dtResult.Columns.Add(GlobalNames.sFPAircraft_Description, typeof(String));
            dtResult.Columns.Add(GlobalNames.sFPAircraft_Wake, typeof(String));
            dtResult.Columns.Add(GlobalNames.sFPAircraft_Body, typeof(String));
            dtResult.Columns.Add(GlobalNames.sFPAircraft_NumberSeats, typeof(Int32));
            dtResult.Columns.Add(GlobalNames.sTableColumn_ULDLoose, typeof(String));

            dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns[0], dtResult.Columns[1] };

            foreach (int ikey in lsAircraftLooseForDeparture.Keys)
            {
                DataRow drNewRow = dtResult.NewRow();
                drNewRow[0] = "D_" + ikey.ToString();
                drNewRow[1] = lsAircraftLooseForDeparture[ikey];
                drNewRow[2] = "";
                drNewRow[3] = "";
                drNewRow[4] = "";
                drNewRow[5] = 0;
                drNewRow[6] = GlobalNames.sTableContent_Loose;
                dtResult.Rows.Add(drNewRow);
            }
            return dtResult;
        }

        private DataTable GenerateMakeUpExceptions()
        {
            DataManagerInput.LoadParameters lpMakeUp = DataManagement.DataManagerInput.getParameters(GlobalNames.OCT_MakeUpTableName);

            DataTable dtResult = lpMakeUp.InitializeTable();
            if (lpMakeUp.oDefaultFirstColumn != null)
            {
                OverallTools.FonctionUtiles.initialiserLignesTable(dtResult, (String[])lpMakeUp.oDefaultFirstColumn);
            }
            int j=1;
            foreach (int ikey in diiMakeUpExceptionDeparture.Keys)
            {
                dtResult.Columns.Add("D_" + ikey.ToString(), typeof(Double));
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    dtResult.Rows[i][j] = SodexiNames.oDefaultColumnMakeUp[i];
                }
                dtResult.Rows[3][j] = FonctionsType.getDouble(diiMakeUpExceptionDeparture[ikey]);
                j++;
            }
            return dtResult;
        }
        #endregion

        internal static ParamScenario GenerationScenario(String sName, 
            DateTime tsStartDate, 
            DateTime tsEndDate,
            Double dSamplingStep,
            Double dAnalysisRange,
            Dictionary<String, String> htUserData)
        {
            ParamScenario psScenario = new ParamScenario(sName);
            psScenario.DateDebut = tsStartDate;
            psScenario.DateFin = tsEndDate;
            psScenario.SamplingStep = dSamplingStep;
            psScenario.AnalysisRange = dAnalysisRange;
            psScenario.StatisticsStep = 0;
            psScenario.StatisticsStepMode = "";


            psScenario.DeparturePeak = false;
            psScenario.ArrivalPeak = false;
            psScenario.PaxPlan = true;
            psScenario.BHSSimulation = false;
            psScenario.TMSSimulation = false;
            psScenario.BagPlan = false;

            psScenario.FPD = GlobalNames.FPDTableName;
            psScenario.OCT_CI_Table = GlobalNames.OCT_CITableName;
            psScenario.OCT_BG = GlobalNames.OCT_BoardGateTableName;
            psScenario.OCT_MakeUp = GlobalNames.OCT_MakeUpTableName;
            psScenario.DepartureLoadFactors = GlobalNames.FPD_LoadFactorsTableName;
            psScenario.CI_ShowUpTable = GlobalNames.CI_ShowUpTableName;


            psScenario.FPA = GlobalNames.FPATableName;
            psScenario.OCT_BC = GlobalNames.OCT_BaggageClaimTableName;
            psScenario.ArrivalLoadFactors = GlobalNames.FPA_LoadFactorsTableName;
            psScenario.ICT_Table = GlobalNames.Transfer_ICTTableName;


            psScenario.Airline = GlobalNames.FP_AirlineCodesTableName;


            psScenario.FlightCategories = GlobalNames.FP_FlightCategoriesTableName;
            psScenario.AircraftType = GlobalNames.FP_AircraftTypesTableName;
            psScenario.NbBags = GlobalNames.NbBagsTableName;
            psScenario.NbTrolley = GlobalNames.NbTrolleyTableName;
            psScenario.NbVisitors = GlobalNames.NbVisitorsTableName;

            psScenario.AircraftLinksTable = GlobalNames.FPLinksTableName;


            psScenario.TransfTerminalDistribution = "";
            psScenario.TransfFligthCategoryDistribution = "";
            psScenario.UseSeed = false;
            psScenario.Seed = 1;


            psScenario.GenerateAll = false;
            psScenario.GenerateFlightsAtEnd = false;
            psScenario.TransferArrivalGeneration = true;
            psScenario.FillTransfer = false;
            psScenario.FillQueue = false;
            psScenario.Opening_CITable = "";
            psScenario.PaxSimulation = true;


            psScenario.UseProcessSchedule = false;
            psScenario.ProcessSchedule = "";
            psScenario.ProcessTimes = GlobalNames.Times_ProcessTableName;
            psScenario.Itinerary = GlobalNames.ItineraryTableName;
            psScenario.CapaQueues =GlobalNames.Capa_QueuesTableName;
            psScenario.GroupQueues = GlobalNames.Group_QueuesName;
            psScenario.AnimatedQueues = "";

            psScenario.Security = GlobalNames.Alloc_SecurityCheckTableName;
            psScenario.Transfer = GlobalNames.Alloc_TransferDeskTableName;
            psScenario.Passport = GlobalNames.Alloc_PassportCheckTableName;
            psScenario.Saturation = GlobalNames.Alloc_SaturationTableName;
            psScenario.Oneof = GlobalNames.OneofSpecificationTableName;

            psScenario.UserData = null;

            psScenario.WarmUp = 0;


            psScenario.SaveTraceMode = false;
            psScenario.ModelName = "";
            psScenario.DisplayModel = false;
            psScenario.RegeneratePaxplan = true;
            psScenario.ExportUserData = true;


            // Utilisation des tables d'exceptions
            psScenario.UseException(GlobalNames.OCT_CITableName ,false );
            psScenario.UseException(GlobalNames.OCT_BoardGateTableName ,false );
            psScenario.UseException(GlobalNames.OCT_MakeUpTableName ,false );
            psScenario.UseException(GlobalNames.FPD_LoadFactorsTableName ,false );
            psScenario.UseException(GlobalNames.CI_ShowUpTableName ,false );
            psScenario.UseException(GlobalNames.OCT_BaggageClaimTableName ,false );
            psScenario.UseException(GlobalNames.FPA_LoadFactorsTableName ,false );
            psScenario.UseException(GlobalNames.Transfer_ICTTableName ,false );
            psScenario.UseException(GlobalNames.FP_AircraftTypesTableName ,false );
            psScenario.UseException(GlobalNames.NbBagsTableName ,false );
            psScenario.UseException(GlobalNames.NbTrolleyTableName ,false );
            psScenario.UseException(GlobalNames.NbVisitorsTableName ,false );
            psScenario.UseException(GlobalNames.Times_ProcessTableName ,false );
            psScenario.UseExSegregation = false;

            psScenario.UserData = new ParamUserData(htUserData);

            return psScenario;
        }
    }
    #endregion
    
    #region class GenerateBagPlanFromSodexi
    class GenerateBagPlanFromSodexi
    {
        #region Paramètres
        DataTable dtFPATable_;
        DataTable dtFPDTable_;
        DataTable dtPosteFret_;
        DataTable dtLocalAirports_; // Sodexi Task#7129 Bagplan Update
        DateTime dtStartTime_;
        DateTime dtEndTime_;
        Double dMinTimeBetweenFlights_;

        Double dPoidADBoxes_;
        Double dVolumeADBoxes_;
        int iCapaciteADBoxes_;

        int iCapaciteContainer_;
        Double dPoidContainer_;
        Double dVolumeContainer_;

        #endregion

        #region Variables

        int iCurrentID;
        Dictionary<String, List<ADContent>> dsadContent;
        Dictionary<String, FPFlight> dsffFPDFlights;
        Dictionary<String, FPFlight> dsffFPAFlights;
        Dictionary<String, String> localAirportsCodesDictionary;    // Sodexi Task#7129 Bagplan Update

        int iIndex_sFretPoste_DateTime=-1;
        int iIndex_sFretPoste_FlightN =-1;
        int iIndex_sFretPoste_Airport=-1;
        int iIndex_sFretPoste_NLTA =-1;
        int iIndex_sFretPoste_Objet =-1;
        int iIndex_sFretPoste_NBObjets =-1;
        int iIndex_sFretPoste_Poids =-1;
        int iIndex_sFretPoste_Destination =-1;
        int iIndex_sFretPoste_Destinataire=-1;
        int iIndex_sFretPoste_SPL1 =-1;
        int iIndex_sFretPoste_SPL2 =-1;
        int iIndex_sFretPoste_SPL3 =-1;
        int iIndex_sFretPoste_Antenne=-1;
        int iIndex_sFretPoste_CLR=-1;
        int iIndex_sFretPoste_CYM =-1;
        int iIndex_sFretPoste_NotCLRCYM=-1;
        int iIndex_sFretPoste_Length=-1;
        int iIndex_sFretPoste_Width =-1;
        int iIndex_sFretPoste_Height =-1;
        int iIndex_sFretPoste_Volume = -1;
        // << Sodexi Task#7495 Modification of Sodexi Project
        int iIndex_sFretPoste_StatusDouanier = -1;
        // >> Sodexi Task#7495 Modification of Sodexi Project


        int iIndexTime=-1;        
        int iIndexFirstBag=-1;
        int iIndexID=-1;
        int iIndexFPA_ID=-1;
        int iIndexFPA_Class=-1;
        int iIndexPaxAtReclaim =-1;
        int iIndexFPD_ID =-1;
        int iIndexFPD_Class =-1;
        int iIndexSTD =-1;
        int iIndexNbBags =-1;
        int iIndexOOG_Bag =-1;
        int iIndexSegregation =-1;
        int iIndexPassportLocal=-1;
        int iIndexTransfer =-1;
        int iIndexTermArr =-1;
        int iIndexArrGate =-1;
        int iIndexTermCI =-1;
        int iIndexCI = -1;

        // << Sodexi Task#7129 Bagplan Update
        int iIndexLocalAirports_Country = -1;
        int iIndexLocalAirports_Code = -1;

        int iIndexBP2Time = -1;
        int iIndexBP2FirstBag = -1;
        int iIndexBP2BagId = -1;
        int iIndexBP2PaxID = -1;
        int iIndexBP2FPA_ID = -1;
        int iIndexBP2FPA_Class = -1;
        int iIndexBP2PaxAtReclaim = -1;
        int iIndexBP2FPD_ID = -1;
        int iIndexBP2FPD_Class = -1;
        int iIndexBP2STD = -1;
        int iIndexBP2NbBags = -1;
        int iIndexBP2OOG_Bag = -1;
        int iIndexBP2Segregation = -1;
        int iIndexBP2PassportLocal = -1;
        int iIndexBP2Transfer = -1;
        int iIndexBP2TermArr = -1;
        int iIndexBP2ArrGate = -1;
        int iIndexBP2TermCI = -1;
        int iIndexBP2CI = -1;
        int iIndexBP2Weight = -1;
        int iIndexBP2Length = -1;
        int iIndexBP2Width = -1;
        int iIndexBP2Height = -1;
        int iIndexBP2Volume = -1;
        int iIndexBP2LocalOrig = -1;
        int iIndexBP2LocalDest = -1;
        // << Sodexi Task#7495 Modification of Sodexi Project
        int iIndexBP2CustomStatus = -1;
        // >> Sodexi Task#7495 Modification of Sodexi Project
        int iIndexBP2User1 = -1;
        int iIndexBP2User2 = -1;
        int iIndexBP2User3 = -1;
        int iIndexBP2User4 = -1;
        int iIndexBP2User5 = -1;

        int iIndexBPIBagId = -1;
        int iIndexBPIPaxId = -1;
        int iIndexBPIArrivalFlight = -1;
        int iIndexBPIDepartureFlight = -1;
        int iIndexBPILTA = -1;
        int iIndexBPIAntenne = -1;
        int iIndexBPIWeight = -1;
        int iIndexBPILength = -1;
        int iIndexBPIWidth = -1;
        int iIndexBPIHeight = -1;
        int iIndexBPIVolume = -1;
        int iIndexBPIOrigLocal = -1;
        int iIndexBPIDestLocal = -1;
        // << Sodexi Task#7495 Modification of Sodexi Project
        int iIndexBPICustomStatus = -1;
        // >> Sodexi Task#7495 Modification of Sodexi Project
        int iIndexBPIUser1 = -1;
        int iIndexBPIUser2 = -1;
        int iIndexBPIUser3 = -1;
        int iIndexBPIUser4 = -1;
        int iIndexBPIUser5 = -1;
        // >> Sodexi Task#7129 Bagplan Update
        #endregion 

        #region Résultats
        DataTable dtBagPlan;
        DataTable dtBagPlan2;   // Sodexi Task#7129 Bagplan Update
        DataTable dtBagPlanInformation;
        DataTable dtBagPlanConversionErrors;

        DataTable dtIgnoredLines;
        DataTable dtDepartureStatistiques;
        DataTable dtFPDStatisticsExtended;  //Sodexi 05.06.2012 FPD+item/weight/volume columns


        internal DataTable BagPlan
        {
            get
            {
                return dtBagPlan;
            }
        }
        // Sodexi Task#7129 Bagplan Update
        internal DataTable BagPlan2
        {
            get
            {
                return dtBagPlan2;
            }
        }

        internal DataTable BagPlanInformation
        {
            get
            {
                return dtBagPlanInformation;
            }
        }

        internal DataTable BagPlanConversionErrors
        {
            get
            {
                return dtBagPlanConversionErrors;
            }
        }

        internal DataTable IgnoredLines
        {
            get
            {
                return dtIgnoredLines;
            }
        }
        internal DataTable DepartureStatistiques
        {
            get
            {
                return dtDepartureStatistiques;
            }
        }
        //Sodexi 05.06.2012 FPD+item/weight/volume columns
        internal DataTable FPDStatisticsExtended
        {
            get
            {
                return dtFPDStatisticsExtended;
            }
        }
        #endregion

        #region Constructeurs
        internal GenerateBagPlanFromSodexi(DataTable dtFPDTable,
            DataTable dtFPATable,
            DataTable dtPosteFret,
            DataTable dtLocalAirports,  // Sodexi Task#7129 Bagplan Update
            DateTime dtStartTime,
            DateTime dtEndTime,
            Double dMinTimeBetweenFlights,
             int iCapaciteADBoxes,
            Double dPoidADBoxes,
             Double dVolumeADBoxes,
            int iCapaciteContainer,
            double dPoidContainer,
            Double dVolumeContainer
            
            )
        {
            iCurrentID = 0;
            dtFPATable_ = dtFPATable;
            dtFPDTable_ = dtFPDTable;
            dtPosteFret_ = dtPosteFret;
            dtLocalAirports_ = dtLocalAirports;     // Sodexi Task#7129 Bagplan Update
            dtStartTime_ = dtStartTime;
            dtEndTime_ = dtEndTime;
            dMinTimeBetweenFlights_ = dMinTimeBetweenFlights;

            FPFlight.iCapacityBox = iCapaciteADBoxes;
            FPFlight.dPoidBox = dPoidADBoxes;
            FPFlight.dVolumeBox = dVolumeADBoxes;

            FPFlight.iCapacityContainer = iCapaciteContainer;

            FPFlight.dPoidContainer = dPoidContainer;
            FPFlight.dVolumeContainer = dVolumeContainer;

            dPoidADBoxes_ = dPoidADBoxes;
            dVolumeADBoxes_ = dVolumeADBoxes;
            iCapaciteADBoxes_ = iCapaciteADBoxes;
            iCapaciteContainer_ = iCapaciteContainer;
            dPoidContainer_ = dPoidContainer;
            dVolumeContainer_ = dVolumeContainer;

            dsadContent = new Dictionary<string, List<ADContent>>();
            dsffFPDFlights = new Dictionary<string, FPFlight>();
            dsffFPAFlights = new Dictionary<string, FPFlight>();
            localAirportsCodesDictionary = new Dictionary<String, String>();       // Sodexi Task#7129 Bagplan Update
        }
        #endregion

        #region Vérification des tables et des paramètres
        private bool CheckTable()
        {
            if (dtFPATable_ == null)
                return false;
            if (dtFPDTable_ == null)
                return false;
            if (dtPosteFret_ == null)
                return false;
            if (!SodexiNames.IsValid(dtPosteFret_, SodexiNames.FretPoste))
                return false;
            // << Sodexi Task#7129 Bagplan Update
            if (dtLocalAirports_ == null)
                return false;
            if (!SodexiNames.IsValid(dtLocalAirports_, SodexiNames.LocalAirports))
                return false;
            iIndexLocalAirports_Country = dtLocalAirports_.Columns.IndexOf(SodexiNames.sLocalAirports_Country);
            iIndexLocalAirports_Code = dtLocalAirports_.Columns.IndexOf(SodexiNames.sLocalAirports_Code);
            // >> Sodexi Task#7129 Bagplan Update
            iIndex_sFretPoste_DateTime = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_DateTime);
            iIndex_sFretPoste_FlightN = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_FlightN);
            iIndex_sFretPoste_Airport = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_Airport);
            iIndex_sFretPoste_NLTA = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_NLTA);
            iIndex_sFretPoste_Objet = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_Objet);
            iIndex_sFretPoste_NBObjets = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_NBObjets);
            iIndex_sFretPoste_Poids = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_Poids);
            iIndex_sFretPoste_Destination = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_Destination);
            iIndex_sFretPoste_Destinataire = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_Destinataire);
            iIndex_sFretPoste_SPL1 = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_SPL1);
            iIndex_sFretPoste_SPL2 = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_SPL2);
            iIndex_sFretPoste_SPL3 = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_SPL3);
            iIndex_sFretPoste_Antenne = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_Antenne);
            iIndex_sFretPoste_CLR = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_CLR);
            iIndex_sFretPoste_CYM = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_CYM);
            iIndex_sFretPoste_NotCLRCYM = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_NotCLRCYM);
            iIndex_sFretPoste_Length = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_Longueur);
            iIndex_sFretPoste_Width = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_Largeur);
            iIndex_sFretPoste_Height = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_Hauteur);
            iIndex_sFretPoste_Volume = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_Volume);
            // << Sodexi Task#7495 Modification of Sodexi Project
            iIndex_sFretPoste_StatusDouanier = dtPosteFret_.Columns.IndexOf(SodexiNames.sFretPoste_StatusDouanier);
            // >> Sodexi Task#7495 Modification of Sodexi Project

            dtPosteFret_ = OverallTools.DataFunctions.sortTable(dtPosteFret_, SodexiNames.sFretPoste_DateTime);

            int iIndex_FPD_Date = dtFPDTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
            int iIndex_FPD_STD = dtFPDTable_.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
            int iIndex_FPD_Airport = dtFPDTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
            int iIndex_FPD_FlightN = dtFPDTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int iIndex_FPD_id = dtFPDTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            int iIndex_FPD_FC = dtFPDTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            int iIndex_FPD_User1 = dtFPDTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User1);
            int iIndex_FPD_User2 = dtFPDTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User2);
            int iIndex_FPD_User3 = dtFPDTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User3);
            int iIndex_FPD_Terminal = dtFPDTable_.Columns.IndexOf(GlobalNames.sFPD_Column_TerminalCI);
            int iIndex_FPD_Desk = dtFPDTable_.Columns.IndexOf(GlobalNames.sFPD_Column_Eco_CI_Start);
          //<< Sodexi 14.03.2011
            int iIndex_FPD_AD = dtFPDTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_User4);
          //>> Sodexi 14.03.2011
            
            int iIndex_FPA_Date = dtFPATable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
            int iIndex_FPA_STA = dtFPATable_.Columns.IndexOf(GlobalNames.sFPA_Column_STA);
            int iIndex_FPA_Airport = dtFPATable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirportCode);
            int iIndex_FPA_id = dtFPATable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            int iIndex_FPA_FlightN = dtFPATable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int iIndex_FPA_FC = dtFPATable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightCategory);
            int iIndex_FPA_Terminal = dtFPATable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_TerminalGate);
            int iIndex_FPA_Desk = dtFPATable_.Columns.IndexOf(GlobalNames.sFPA_Column_ArrivalGate);

            foreach(DataRow drRow in dtFPDTable_.Rows)
            { 
              //<< Sodexi 14.03.2011
                //Check the value in the User4 column from the FPD and 
                //set the ADVol to true if the value is 1 and folse otherwise
                bool ADVol = false;
                if (drRow[iIndex_FPD_AD].Equals("1"))
                    ADVol = true;
                if (drRow[iIndex_FPD_AD].Equals("0"))
                    ADVol = false;
              //>> Sodexi 14.03.2011

                FPFlight ffTmp = new FPFlight(FonctionsType.getInt(drRow[iIndex_FPD_id]),
                    FonctionsType.getDate(drRow[iIndex_FPD_Date]),
                    FonctionsType.getTime(drRow[iIndex_FPD_STD]),
                    FonctionsType.getString(drRow[iIndex_FPD_Airport]),
                    FonctionsType.getString(drRow[iIndex_FPD_FlightN]),
                        FonctionsType.getString(drRow[iIndex_FPD_FC]),
                    FonctionsType.getInt(drRow[iIndex_FPD_User1]),
                    FonctionsType.getDouble(drRow[iIndex_FPD_User2]),
                    FonctionsType.getDouble(drRow[iIndex_FPD_User3]),
                    FonctionsType.getInt(drRow[iIndex_FPD_Terminal]),
                    FonctionsType.getInt(drRow[iIndex_FPD_Desk]),
                  //<< Sodexi 14.03.2011
                    ADVol);
                    //FonctionsType.getBoolean(drRow[iIndex_FPD_AD]));
                  //>> Sodexi 14.03.2011
                dsffFPDFlights.Add(ffTmp.iID_.ToString(), ffTmp);
            }

            foreach (DataRow drRow in dtFPATable_.Rows)
            {
                FPFlight ffTmp = new FPFlight(FonctionsType.getInt(drRow[iIndex_FPA_id]),
                    FonctionsType.getDate(drRow[iIndex_FPA_Date]),
                    FonctionsType.getTime(drRow[iIndex_FPA_STA]),
                    FonctionsType.getString(drRow[iIndex_FPA_Airport]),
                    FonctionsType.getString(drRow[iIndex_FPA_FlightN]),
                    FonctionsType.getString(drRow[iIndex_FPA_FC]),
                    0,0,0,
                    FonctionsType.getInt(drRow[iIndex_FPA_Terminal]),
                    FonctionsType.getInt(drRow[iIndex_FPA_Desk]),
                  //<< Sodexi 14.03.2011
                    false);
                  //>> Sodexi 14.03.2011
                dsffFPAFlights.Add(ffTmp.iID_.ToString(), ffTmp);
            }

            // << Sodexi Task#7129 Bagplan Update
            if (iIndexLocalAirports_Country != -1 && iIndexLocalAirports_Code != -1)
            {
                foreach (DataRow drRow in dtLocalAirports_.Rows)
                {
                    String country = FonctionsType.getString(drRow[iIndexLocalAirports_Country]);
                    String code = FonctionsType.getString(drRow[iIndexLocalAirports_Code]);
                    localAirportsCodesDictionary.Add(code, country);
                }
            }
            // >> Sodexi Task#7129 Bagplan Update
            return true;
        }
        #endregion

        #region Gestion des AD Content
        private class ADContent
        {
            internal String sObject_;
            internal int NbOjects_;
            internal Double dPoids_;
            internal String sDestination_;

            internal Double dLongueur_;
            internal Double dLargeur_;
            internal Double dHauteur_;

            internal Double dVolume_;


            internal ADContent(String sObject,
                int NbOjects,
                Double dPoids,
                String sDestination,
                Double dLongueur,
                Double dLargeur,
                Double dHauteur,
                Double dVolume)
            {
                sObject_ = sObject;
                NbOjects_ = NbOjects;
                dPoids_ = dPoids;
                dLongueur_ = dLongueur;
                dLargeur_ = dLargeur;
                dHauteur_ = dHauteur;
                sDestination_ = sDestination;
                dVolume_ = dVolume;
            }
        }

        private void GenerateADContent()
        {
            if (dtPosteFret_ == null)
                return;

            foreach(DataRow drRow in dtPosteFret_.Rows)
            {
                if (FonctionsType.getString(drRow[iIndex_sFretPoste_Antenne]) == SodexiNames.sAntenneSortie_ADContent)
                {
                    String sObject = FonctionsType.getString(drRow[iIndex_sFretPoste_Objet]);
                    if(!dsadContent.ContainsKey(sObject))
                        dsadContent.Add(sObject,new List<ADContent>());

                    dsadContent[sObject].Add(new ADContent(sObject,
                        FonctionsType.getInt(drRow[iIndex_sFretPoste_NBObjets]),
                        FonctionsType.getDouble(drRow[iIndex_sFretPoste_Poids]),
                        FonctionsType.getString(drRow[iIndex_sFretPoste_Destination]),
                        FonctionsType.getDouble(drRow[iIndex_sFretPoste_Length]),
                        FonctionsType.getDouble(drRow[iIndex_sFretPoste_Width]),
                        FonctionsType.getDouble(drRow[iIndex_sFretPoste_Height]),
                        FonctionsType.getDouble(drRow[iIndex_sFretPoste_Volume])));
                }
            }
        }
        #endregion

        #region Fonctions pour la génération des BagPlan
        internal void GenerateBagPlan()
        {
            if (!CheckTable())
                return;
            GenerateADContent();
            GenerateBagPlans();
            dtDepartureStatistiques = GenerateDepartureStatistics();
            dtFPDStatisticsExtended = GenerateFPDStatisticsExtended();
        }

        private void GenerateBagPlans()
        {
            #region Création du BagPlan
            dtBagPlan = new DataTable(GlobalNames.BagPlanName);

            dtBagPlan.Columns.Add("Time(mn)", typeof(Double));

            dtBagPlan.Columns.Add("FirstBag", typeof(Int32));
            dtBagPlan.Columns.Add("ID_PAX", typeof(Int32));
            dtBagPlan.Columns.Add("FPA_ID", typeof(Int32));
            dtBagPlan.Columns.Add("FPA_Class", typeof(Int32));
            dtBagPlan.Columns.Add("PaxAtReclaim", typeof(Double));
            dtBagPlan.Columns.Add("FPD_ID", typeof(Int32));



            dtBagPlan.Columns.Add("FPD_Class", typeof(Int32));
            dtBagPlan.Columns.Add("STD", typeof(Int32));
            dtBagPlan.Columns.Add("NbBags", typeof(Int32));


            dtBagPlan.Columns.Add("OOG_Bag", typeof(Int32));
            dtBagPlan.Columns.Add("Segregation", typeof(Int32));
            dtBagPlan.Columns.Add("PassportLocal", typeof(Int32));
            dtBagPlan.Columns.Add("Transfer", typeof(Int32));
            dtBagPlan.Columns.Add("Term.Arr", typeof(Int32));
            dtBagPlan.Columns.Add("#Arr.Gate", typeof(Int32));
            dtBagPlan.Columns.Add("Term.CI", typeof(Int32));
            dtBagPlan.Columns.Add("#CI", typeof(Int32));


            iIndexTime = dtBagPlan.Columns.IndexOf("Time(mn)");

            iIndexFirstBag = dtBagPlan.Columns.IndexOf("FirstBag");
            iIndexID = dtBagPlan.Columns.IndexOf("ID_PAX");
            iIndexFPA_ID = dtBagPlan.Columns.IndexOf("FPA_ID");
            iIndexFPA_Class = dtBagPlan.Columns.IndexOf("FPA_Class");
            iIndexPaxAtReclaim = dtBagPlan.Columns.IndexOf("PaxAtReclaim");
            iIndexFPD_ID = dtBagPlan.Columns.IndexOf("FPD_ID");
            ///La class permettra de connaître si le colis est du FRET ou de la POSTE
            ///1 : POSTE
            ///2 : FRET
            iIndexFPD_Class = dtBagPlan.Columns.IndexOf("FPD_Class");
            iIndexSTD = dtBagPlan.Columns.IndexOf("STD");
            iIndexNbBags = dtBagPlan.Columns.IndexOf("NbBags");
            /// OOG_Bag = 1 si le colis fait partie d'un colis AD qui a été 
            /// divisé. 
            iIndexOOG_Bag = dtBagPlan.Columns.IndexOf("OOG_Bag");
            iIndexSegregation = dtBagPlan.Columns.IndexOf("Segregation");
            iIndexPassportLocal = dtBagPlan.Columns.IndexOf("PassportLocal");
            iIndexTransfer = dtBagPlan.Columns.IndexOf("Transfer");
            iIndexTermArr = dtBagPlan.Columns.IndexOf("Term.Arr");
            iIndexArrGate = dtBagPlan.Columns.IndexOf("#Arr.Gate");
            iIndexTermCI = dtBagPlan.Columns.IndexOf("Term.CI");
            iIndexCI = dtBagPlan.Columns.IndexOf("#CI");

            if ((iIndexTime == -1) ||
                (iIndexID == -1) ||
                (iIndexFPA_ID == -1) ||
                (iIndexFPA_Class == -1) ||
                (iIndexPaxAtReclaim == -1) ||
                (iIndexFPD_ID == -1) ||
                (iIndexFPD_Class == -1) ||
                (iIndexSTD == -1) ||
                (iIndexNbBags == -1) ||
                (iIndexOOG_Bag == -1) ||
                (iIndexSegregation == -1) ||
                (iIndexPassportLocal == -1) ||
                (iIndexTransfer == -1) ||
                (iIndexTermArr == -1) ||
                (iIndexArrGate == -1) ||
                (iIndexTermCI == -1) ||
                (iIndexCI == -1))
            {
                return;
            }

            // << Sodexi Task#7129 Bagplan Update
            #region BagaPlan_2
            dtBagPlan2 = new DataTable(GlobalNames.BagPlan2Name);

            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_Time, typeof(Double));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_FirstBag, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_BagId, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_PaxId, typeof(Int32));

            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_FPAId, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_FPAClass, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_PaxAtReclaim, typeof(Double));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_FPDId, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_FPDClass, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_STD, typeof(Int32));

            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_NbBags, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_OOGBag, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_Segregation, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_PassportLocal, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_Transfer, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_ArrivalTerminal, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_ArrivalGateNb, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_CheckInTerminal, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_CheckInNb, typeof(Int32));

            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_Weight, typeof(Double));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_Length, typeof(Double));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_Width, typeof(Double));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_Height, typeof(Double));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_Volume, typeof(Double));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_LocalOrigin, typeof(Int32));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_LocalDestination, typeof(Int32));
            // << Sodexi Task#7495 Modification of Sodexi Project
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_CustomStatus, typeof(String));
            // >> Sodexi Task#7495 Modification of Sodexi Project
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_User1, typeof(String));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_User2, typeof(String));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_User3, typeof(String));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_User4, typeof(String));
            dtBagPlan2.Columns.Add(GlobalNames.sBagPlan2_Column_User5, typeof(String));

            iIndexBP2Time = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_Time);
            iIndexBP2FirstBag = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_FirstBag);
            iIndexBP2BagId = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_BagId);
            iIndexBP2PaxID = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_PaxId);
            iIndexBP2FPA_ID = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_FPAId);
            iIndexBP2FPA_Class = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_FPAClass);
            iIndexBP2PaxAtReclaim = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_PaxAtReclaim);
            iIndexBP2FPD_ID = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_FPDId);
            /// The class shows if the package is from Poste or Fret: 1 = POSTE and 2 = FRET
            iIndexBP2FPD_Class = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_FPDClass);
            iIndexBP2STD = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_STD);
            iIndexBP2NbBags = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_NbBags);
            /// OOG_Bag = 1 if the package is part of a AD package that has been divised
            iIndexBP2OOG_Bag = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_OOGBag);
            iIndexBP2Segregation = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_Segregation);
            iIndexBP2PassportLocal = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_PassportLocal);
            iIndexBP2Transfer = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_Transfer);
            iIndexBP2TermArr = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_ArrivalTerminal);
            iIndexBP2ArrGate = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_ArrivalGateNb);
            iIndexBP2TermCI = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_CheckInTerminal);
            iIndexBP2CI = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_CheckInNb);
            iIndexBP2Weight = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_Weight);
            iIndexBP2Length = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_Length);
            iIndexBP2Width = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_Width);
            iIndexBP2Height = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_Height);
            iIndexBP2Volume = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_Volume);
            iIndexBP2LocalOrig = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_LocalOrigin);
            iIndexBP2LocalDest = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_LocalDestination);
            // << Sodexi Task#7495 Modification of Sodexi Project            
            iIndexBP2CustomStatus = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_CustomStatus);
            // >> Sodexi Task#7495 Modification of Sodexi Project
            iIndexBP2User1 = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_User1);
            iIndexBP2User2 = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_User2);
            iIndexBP2User3 = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_User3);
            iIndexBP2User4 = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_User4);
            iIndexBP2User5 = dtBagPlan2.Columns.IndexOf(GlobalNames.sBagPlan2_Column_User5);

            if ((iIndexBP2Time == -1) ||
                (iIndexBP2FirstBag == -1) ||
                (iIndexBP2BagId == -1) ||
                (iIndexBP2PaxID == -1) ||                
                (iIndexBP2FPA_ID == -1) ||
                (iIndexBP2FPA_Class == -1) ||
                (iIndexBP2PaxAtReclaim == -1) ||
                (iIndexBP2FPD_ID == -1) ||
                (iIndexBP2FPD_Class == -1) ||
                (iIndexBP2STD == -1) ||
                (iIndexBP2NbBags == -1) ||
                (iIndexBP2OOG_Bag == -1) ||
                (iIndexBP2Segregation == -1) ||
                (iIndexBP2PassportLocal == -1) ||
                (iIndexBP2Transfer == -1) ||
                (iIndexBP2TermArr == -1) ||
                (iIndexBP2ArrGate == -1) ||
                (iIndexBP2TermCI == -1) ||
                (iIndexBP2CI == -1) ||
                (iIndexBP2Weight == -1) ||
                (iIndexBP2Length == -1) ||
                (iIndexBP2Width == -1) ||
                (iIndexBP2Height == -1) ||
                (iIndexBP2Volume == -1) ||
                (iIndexBP2LocalOrig == -1) ||
                (iIndexBP2LocalDest == -1) ||
                // << Sodexi Task#7495 Modification of Sodexi Project
                (iIndexBP2CustomStatus == -1) ||
                // >> Sodexi Task#7495 Modification of Sodexi Project
                (iIndexBP2User1 == -1) ||
                (iIndexBP2User2 == -1) ||
                (iIndexBP2User3 == -1) ||
                (iIndexBP2User4 == -1) ||
                (iIndexBP2User5 == -1)) 
            {
                return;
            }
            #endregion
            // >> Sodexi Task#7129 Bagplan Update

            #region Bag Plan Information
            dtBagPlanInformation = new DataTable("BagPlan Information");
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_ID_BAG, typeof(Int32));    // Sodexi Task#7129 Bagplan Update
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_ID_PAX, typeof(Int32));
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_ArrivalFlight, typeof(String));
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_DepartureFlight, typeof(String));
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_LTA, typeof(String));
            //<< Sodexi 14.03.2011
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_Antenne, typeof(String));
            //>> Sodexi 14.03.2011  
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_Weight, typeof(Double));
            // << Sodexi Task#7129 Bagplan Update
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_Length, typeof(Double));
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_Width, typeof(Double));
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_Height, typeof(Double));
            // >> Sodexi Task#7129 BagPlan Update
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_Volume, typeof(Double));
            // << Sodexi Task#7129 BagPlan Update
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_OriginLocal, typeof(Int32));
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_DestinationLocal, typeof(Int32));
            // << Sodexi Task#7495 Modification of Sodexi Project
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_CustomStatus, typeof(String));
            // >> Sodexi Task#7495 Modification of Sodexi Project
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_User1, typeof(String));
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_User2, typeof(String));
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_User3, typeof(String));
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_User4, typeof(String));
            dtBagPlanInformation.Columns.Add(GlobalNames.sBagPlanInformation_User5, typeof(String));
            
            iIndexBPIBagId = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_ID_BAG);
            iIndexBPIPaxId = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_ID_PAX);
            iIndexBPIArrivalFlight = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_ArrivalFlight);
            iIndexBPIDepartureFlight = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_DepartureFlight);
            iIndexBPILTA = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_LTA);
            iIndexBPIAntenne = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_Antenne);
            iIndexBPIWeight = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_Weight);
            iIndexBPILength = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_Length);
            iIndexBPIWidth = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_Width);
            iIndexBPIOrigLocal = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_OriginLocal);
            iIndexBPIDestLocal = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_DestinationLocal);
            iIndexBPIHeight = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_Height);
            iIndexBPIVolume = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_Volume);
            // << Sodexi Task#7495 Modification of Sodexi Project
            iIndexBPICustomStatus = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_CustomStatus);
            // >> Sodexi Task#7495 Modification of Sodexi Project
            iIndexBPIUser1 = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_User1);
            iIndexBPIUser2 = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_User2);
            iIndexBPIUser3 = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_User3);
            iIndexBPIUser4 = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_User4);
            iIndexBPIUser5 = dtBagPlanInformation.Columns.IndexOf(GlobalNames.sBagPlanInformation_User5);
            // >> Sodexi Task#7129 Bagplan Update
            #endregion

            #endregion

            dtIgnoredLines = dtPosteFret_.Clone();
            dtIgnoredLines.TableName ="Ignored Lines";

            dtBagPlanConversionErrors = dtPosteFret_.Clone();
            dtBagPlanConversionErrors.TableName = "BagPlan Errors";
            dtBagPlanConversionErrors.Columns.Add("Status", typeof(String));
          //<< Sodexi 14.03.2011
            #region AD items
            foreach (DataRow drRow in dtPosteFret_.Rows)
            {
                String sAntenne = FonctionsType.getString(drRow[iIndex_sFretPoste_Antenne]);
                if ((sAntenne == SodexiNames.sAntenneSortie_AD) || (sAntenne == SodexiNames.sAntenneSortie_ADContent))
                {

                    #region Partie ou l'on ignore les lignes qui ne sont pas a traiter
                    //include ADContent
                    //if ( sAntenne == SodexiNames.sAntenneSortie_ADContent)
                    //  continue;

                    if (sAntenne == SodexiNames.sAntenneSortie_G02FAP)
                    {
                        dtIgnoredLines.Rows.Add(drRow.ItemArray);
                        ///TODO Mettre une ligne pour dire que cette ligne a été ignorée.
                        continue;
                    }

                    if (sAntenne == SodexiNames.sAntenneSortie_G02ImportNonMeca)
                    {
                        dtIgnoredLines.Rows.Add(drRow.ItemArray);
                        ///TODO Mettre une ligne pour dire que cette ligne a été ignorée.
                        continue;
                    }

                    if (sAntenne == SodexiNames.sAntenneSortie_G02TransitNonMeca)
                    {
                        dtIgnoredLines.Rows.Add(drRow.ItemArray);
                        ///TODO Mettre une ligne pour dire que cette ligne a été ignorée.
                        continue;
                    }
                    #endregion

                    String sDestination = FonctionsType.getString(drRow[iIndex_sFretPoste_Destination]);
                    DateTime dtTime = FonctionsType.getDate(drRow[iIndex_sFretPoste_DateTime]);
                    if (dtTime < dtStartTime_)
                        continue;
                    if (dtTime > dtEndTime_)
                        continue;

                    FPFlight ffArrival = getFlight(dsffFPAFlights, FonctionsType.getDate(drRow[iIndex_sFretPoste_DateTime]), FonctionsType.getString(drRow[iIndex_sFretPoste_FlightN]));
                    if (ffArrival == null)
                    {
                        Object[] tmp = new Object[drRow.ItemArray.Length + 1];
                        drRow.ItemArray.CopyTo(tmp, 0);
                        tmp[tmp.Length - 1] = "Unable to find the arrival flight for item";
                        //Error : Le vol arrivée n'existe pas.
                        dtBagPlanConversionErrors.Rows.Add(tmp);
                        continue;
                    }
                    #region Gestion des AD et ADContent
                    if (sAntenne == SodexiNames.sAntenneSortie_AD)
                    {
                        AddRows(drRow, ffArrival, true, 2, SodexiNames.sADArea, sAntenne);
                        continue;
                    }
                    if (sAntenne == SodexiNames.sAntenneSortie_ADContent)
                    {
                        AddRows(drRow, ffArrival, false, 2, sDestination, true, sAntenne);
                        continue;
                    }
                    #endregion

                    #region Gestion de la poste
                    if (sAntenne == SodexiNames.sAntenneSortie_PosteExport)
                    {
                        AddRows(drRow, ffArrival, false, 1, sDestination, sAntenne);
                        continue;
                    }
                    else if (sAntenne == SodexiNames.sAntenneSortie_PosteImport)
                    {
                        //sPosteImport
                        AddRows(drRow, ffArrival, false, 1, SodexiNames.sPosteImport, sAntenne);
                        continue;
                    }
                    else if (sAntenne == SodexiNames.sAntenneSortie_PosteCHR)
                    {
                        AddRows(drRow, ffArrival, false, 1, SodexiNames.sPosteImportCHR, sAntenne);
                        //sPosteImportCHR
                        continue;
                    }
                    else if (sAntenne == SodexiNames.sAntenneSortie_PosteCYM)
                    {
                        //sPosteImportCYM
                        AddRows(drRow, ffArrival, false, 1, SodexiNames.sPosteImportCYM, sAntenne);
                        continue;
                    }
                    #endregion

                    #region Gestion du FRET mécanisable
                    if (sAntenne == SodexiNames.sAntenneSortie_FRET)
                    {
                        AddRows(drRow, ffArrival, false, 2, sDestination, sAntenne);
                        continue;
                    }
                    else if (sAntenne == SodexiNames.sAntenneSortie_G02Import)
                    {
                        //sCDGImport
                        AddRows(drRow, ffArrival, false, 2, SodexiNames.sCDGImport, sAntenne);
                        continue;
                    }
                    #endregion

                    Object[] tmp2 = new Object[drRow.ItemArray.Length + 1];
                    drRow.ItemArray.CopyTo(tmp2, 0);
                    tmp2[tmp2.Length - 1] = "Unable to process item : \"Antenne\" doesn't contains a valid value.";
                    //Error : Le vol arrivée n'existe pas.
                    dtBagPlanConversionErrors.Rows.Add(tmp2);
                }
            }
            #endregion

            #region items that are not AD
            foreach (DataRow drRow in dtPosteFret_.Rows)
            {
                String sAntenne = FonctionsType.getString(drRow[iIndex_sFretPoste_Antenne]);
                if ((sAntenne != SodexiNames.sAntenneSortie_AD) && (sAntenne != SodexiNames.sAntenneSortie_ADContent))
                {
                    #region Partie ou l'on ignore les lignes qui ne sont pas a traiter
                    //include ADContent
                    //if ( sAntenne == SodexiNames.sAntenneSortie_ADContent)
                    //  continue;

                    if (sAntenne == SodexiNames.sAntenneSortie_G02FAP)
                    {
                        dtIgnoredLines.Rows.Add(drRow.ItemArray);
                        ///TODO Mettre une ligne pour dire que cette ligne a été ignorée.
                        continue;
                    }

                    if (sAntenne == SodexiNames.sAntenneSortie_G02ImportNonMeca)
                    {
                        dtIgnoredLines.Rows.Add(drRow.ItemArray);
                        ///TODO Mettre une ligne pour dire que cette ligne a été ignorée.
                        continue;
                    }

                    if (sAntenne == SodexiNames.sAntenneSortie_G02TransitNonMeca)
                    {
                        dtIgnoredLines.Rows.Add(drRow.ItemArray);
                        ///TODO Mettre une ligne pour dire que cette ligne a été ignorée.
                        continue;
                    }
                    #endregion

                    String sDestination = FonctionsType.getString(drRow[iIndex_sFretPoste_Destination]);
                    DateTime dtTime = FonctionsType.getDate(drRow[iIndex_sFretPoste_DateTime]);
                    if (dtTime < dtStartTime_)
                        continue;
                    if (dtTime > dtEndTime_)
                        continue;

                    FPFlight ffArrival = getFlight(dsffFPAFlights, FonctionsType.getDate(drRow[iIndex_sFretPoste_DateTime]), FonctionsType.getString(drRow[iIndex_sFretPoste_FlightN]));
                    if (ffArrival == null)
                    {
                        Object[] tmp = new Object[drRow.ItemArray.Length + 1];
                        drRow.ItemArray.CopyTo(tmp, 0);
                        tmp[tmp.Length - 1] = "Unable to find the arrival flight for item";
                        //Error : Le vol arrivée n'existe pas.
                        dtBagPlanConversionErrors.Rows.Add(tmp);
                        continue;
                    }
                    #region Gestion des AD
                    if (sAntenne == SodexiNames.sAntenneSortie_AD)
                    {
                        AddRows(drRow, ffArrival, true, 2, SodexiNames.sADArea, sAntenne);
                        continue;
                    }
                    #endregion

                    #region Gestion de la poste
                    if (sAntenne == SodexiNames.sAntenneSortie_PosteExport)
                    {
                        AddRows(drRow, ffArrival, false, 1, sDestination, sAntenne);
                        continue;
                    }
                    else if (sAntenne == SodexiNames.sAntenneSortie_PosteImport)
                    {
                        //sPosteImport
                        AddRows(drRow, ffArrival, false, 1, SodexiNames.sPosteImport, sAntenne);
                        continue;
                    }
                    else if (sAntenne == SodexiNames.sAntenneSortie_PosteCHR)
                    {
                        AddRows(drRow, ffArrival, false, 1, SodexiNames.sPosteImportCHR, sAntenne);
                        //sPosteImportCHR
                        continue;
                    }
                    else if (sAntenne == SodexiNames.sAntenneSortie_PosteCYM)
                    {
                        //sPosteImportCYM
                        AddRows(drRow, ffArrival, false, 1, SodexiNames.sPosteImportCYM, sAntenne);
                        continue;
                    }
                    #endregion

                    #region Gestion du FRET mécanisable
                    if (sAntenne == SodexiNames.sAntenneSortie_FRET)
                    {
                        AddRows(drRow, ffArrival, false, 2, sDestination, sAntenne);
                        continue;
                    }
                    else if (sAntenne == SodexiNames.sAntenneSortie_G02Import)
                    {
                        //sCDGImport
                        AddRows(drRow, ffArrival, false, 2, SodexiNames.sCDGImport, sAntenne);
                        continue;
                    }
                    #endregion

                    Object[] tmp2 = new Object[drRow.ItemArray.Length + 1];
                    drRow.ItemArray.CopyTo(tmp2, 0);
                    tmp2[tmp2.Length - 1] = "Unable to process item : \"Antenne\" doesn't contains a valid value.";
                    //Error : Le vol arrivée n'existe pas.
                    dtBagPlanConversionErrors.Rows.Add(tmp2);
                }
            }
            #endregion
          //>> Sodexi 14.03.2011
        }
        #endregion

        #region Fonction AddRows / AddRow
      //<< Sodexi 14.03.2011
        private void AddRows(DataRow drRow, FPFlight ffArrival, bool bAD, int iClasse/*1 = F&B 2 = Eco*/, String sDestination, String sAntenne)
        {
            AddRows(drRow, ffArrival, bAD, iClasse/*1 = F&B 2 = Eco*/, sDestination, false, sAntenne);
        }
      //>> Sodexi 14.03.2011

      //<< Sodexi 14.03.2011
        private void AddRows(DataRow drRow, FPFlight ffArrival, bool bAD, int iClasse/*1 = F&B 2 = Eco*/, String sDestination,
                             bool bADContent, String sAntenne)  //<< Sodexi 14.03.2011 add sAntenne parameter
        {
            //bAD ==> OOG a 1 + generation de 1 ligne  (first bag) et x lignes (non first bags)
            //Fret ==> Eco 
            //POSTE : F&B
            DateTime dtTime = /*FonctionsType.getDate(drRow[iIndex_sFretPoste_DateTime])*/ffArrival.Date_.AddMinutes(dMinTimeBetweenFlights_);
            int iNbObjets = FonctionsType.getInt(drRow[iIndex_sFretPoste_NBObjets]);

            Double dPoids = FonctionsType.getDouble(drRow[iIndex_sFretPoste_Poids]);
            Double dVolume = FonctionsType.getDouble(drRow[iIndex_sFretPoste_Volume]);
            // << Sodexi Task#7129 Bagplan Update
            Double dLength = FonctionsType.getDouble(drRow[iIndex_sFretPoste_Length]);
            Double dWidth = FonctionsType.getDouble(drRow[iIndex_sFretPoste_Width]);
            Double dHeight = FonctionsType.getDouble(drRow[iIndex_sFretPoste_Height]);
            // >> Sodexi Task#7129 Bagplan Update

            if (iNbObjets <= 0)
            {
                Object[] tmp2 = new Object[drRow.ItemArray.Length + 1];
                drRow.ItemArray.CopyTo(tmp2, 0);
                tmp2[tmp2.Length - 1] = "Unable to process item : Invalid number of objet for item.";
                //Error : Le vol arrivée n'existe pas.
                dtBagPlanConversionErrors.Rows.Add(tmp2);
                return;
            }
            Double dPoidUnitaire = dPoids / iNbObjets;
            Double dVolumeUnitaire = dVolume / iNbObjets;
            // << Sodexi Task#7129 Bagplan Update
            Double dLengthPerObject = 0.0;
            Double dWidthPerObject = 0.0;
            Double dHeightPerObject = 0.0;

            if (iNbObjets > 1)
            {
                if (dLength >= dWidth && dLength >= dHeight)
                {
                    dLengthPerObject = dLength / iNbObjets;
                    dWidthPerObject = dWidth;
                    dHeightPerObject = dHeight;
                }
                if (dWidth >= dLength && dWidth >= dHeight)
                {
                    dWidthPerObject = dWidth / iNbObjets;
                    dLengthPerObject = dLength;
                    dHeightPerObject = dHeight;
                }
                if (dHeight >= dLength && dHeight >= dWidth)
                {
                    dHeightPerObject = dHeight / iNbObjets;
                    dLengthPerObject = dLength;
                    dWidthPerObject = dWidth;
                }
            }
            else
            {
                dLengthPerObject = dLength;
                dWidthPerObject = dWidth;
                dHeightPerObject = dHeight;
            }
            // >> Sodexi Task#7129 Bagplan Update

            for (int i = 1; i <= iNbObjets; i++)
            {
                iCurrentID++;
                bool bAdded = false;
                if (sDestination != SodexiNames.sADArea)
                {
                    if (bADContent)
                        bAdded = AddRow(iCurrentID, drRow, dtTime, ffArrival, iClasse, sDestination,
                                        dPoidUnitaire, dVolumeUnitaire, dWidthPerObject, dLengthPerObject, dHeightPerObject,
                                        true, true, sAntenne);  // Sodexi Task#7129 Bagplan Update
                     else
                        bAdded = AddRow(iCurrentID, drRow, dtTime, ffArrival, iClasse, sDestination,
                                        dPoidUnitaire, dVolumeUnitaire, dWidthPerObject, dLengthPerObject, dHeightPerObject,
                                        false, false, sAntenne);    // Sodexi Task#7129 Bagplan Update
                 }
                 else
                    bAdded = AddRow(iCurrentID, drRow, dtTime, ffArrival, iClasse, SodexiNames.sADArea,
                                    dPoidUnitaire, dVolumeUnitaire, dWidthPerObject, dLengthPerObject, dHeightPerObject,
                                    true, false, sAntenne); // Sodexi Task#7129 Bagplan Update
                #region old_comments
                /*             if (bAD)    //we are managing the AD item
                    bAdded = AddRow(iCurrentID, drRow, dtTime, ffArrival, iClasse, SodexiNames.sADArea, dPoidUnitaire, dVolumeUnitaire, true, false);
                else
                {
                    if (bADContent)  // manage the ADContent
                        bAdded = AddRow(iCurrentID, drRow, dtTime, ffArrival, iClasse, sDestination, dPoidUnitaire, dVolumeUnitaire, false, true);
                    else   // any other object
                        bAdded = AddRow(iCurrentID, drRow, dtTime, ffArrival, iClasse, sDestination, dPoidUnitaire, dVolumeUnitaire, false, false);
                }*/
                #endregion

                if (!bAdded)
                 {
                     //Tous les objets ne peuvent trouver une destination. Donc on doit écrire un message pour l'indiquer
                     //TODO : indiquer qu'un problème pour une destination est apparue.

                     Object[] tmp2 = new Object[drRow.ItemArray.Length + 1];
                     drRow.ItemArray.CopyTo(tmp2, 0);
                     tmp2[tmp2.Length - 1] = "Unable to process totally item : " + (iNbObjets - i + 1).ToString() + " of the item doesn't have a departure flight.";
                     //Error : Le vol arrivée n'existe pas.
                     dtBagPlanConversionErrors.Rows.Add(tmp2);
                     break;
                    }

              }
        
        }
        
        #region old_comments2
        /*    else
        //    {
                #region Vérification de la présence du contenu d'un AD content
                //Cas des vols AD
              String sKey = FonctionsType.getString(drRow[iIndex_sFretPoste_NLTA]);
                if (!dsadContent.ContainsKey(sKey))
                {
                    Object[] tmp2 = new Object[drRow.ItemArray.Length + 1];
                    drRow.ItemArray.CopyTo(tmp2, 0);
                    tmp2[tmp2.Length - 1] = "Unable to process item : Couldn't find AD content.";
                    //Error : Le vol arrivée n'existe pas.
                    dtBagPlanConversionErrors.Rows.Add(tmp2);
                    return;
                }
                    int iNumber = 0;
                    Double dPoidAD = 0;
                    Double dVolumeAD = 0;
                    foreach (ADContent adContent in dsadContent[sKey])
                {
                    iNumber += adContent.NbOjects_;
                    dPoidAD += adContent.dPoids_;
                    dVolumeAD += adContent.dVolume_;
           
                }
                
                if ((iNumber != iNbObjets) || (Math.Round(dPoidAD, 2) != Math.Round(dPoids, 2)) || (Math.Round(dVolumeAD, 2) != Math.Round(dVolume, 2)))
                {
                    Object[] tmp2 = new Object[drRow.ItemArray.Length + 1];
                    drRow.ItemArray.CopyTo(tmp2, 0);
                    tmp2[tmp2.Length - 1] = "Problem with the AD Content, Number of item or weight or volume are not matching.";
                    //Error : Le vol arrivée n'existe pas.
                    dtBagPlanConversionErrors.Rows.Add(tmp2);
                    return;
                }
                */

                #region Ajout du colis (firstBag) qui représentera l'arrivée du colis
       /*         iCurrentID++;
                bool bAdded = AddRow(iCurrentID, drRow, dtTime, ffArrival, iClasse, SodexiNames.sADArea, dPoids,dVolume, true, false);
                if (!bAdded)
                {
                    Object[] tmp2 = new Object[drRow.ItemArray.Length + 1];
                    drRow.ItemArray.CopyTo(tmp2, 0);
                    tmp2[tmp2.Length - 1] = "Unable find departure flight for AD ";
                    //Error : Le vol arrivée n'existe pas.
                    dtBagPlanConversionErrors.Rows.Add(tmp2);
                    return;
                }
            */
                /*AddRow(iCurrentID,
                    OverallTools.DataFunctions.MinuteDifference(dtStartTime_, ffArrival.Date_),
                    true,
                    false,
                    ffArrival.iID_,
                    iClasse,
                    0,
                    0,
                    FonctionsType.getInt(drRow[iIndex_sFretPoste_NBObjets]),
                    ffArrival.iTerminal_, ffArrival.iDeskIndex_,
                    0, 0
                    );*/


                /*DataRow newRow = dtBagPlanInformation.NewRow();
                newRow[0] = iCurrentID;
                newRow[1] = ffArrival.sFlightNumber_;
                newRow[2] = ;

                newRow[3] = drRow[iIndex_sFretPoste_NLTA];
                newRow[4] = FonctionsType.getDouble(drRow[iIndex_sFretPoste_Poids]);
                newRow[5] = FonctionsType.getDouble(drRow[iIndex_sFretPoste_Volume]);
                dtBagPlanInformation.Rows.Add(newRow);
                */
                #endregion
                //TODO : Parcours des AD content pour générer une ligne par objet.

/*
                if (bADContent)
                {
                    iNbObjets = adContent.NbOjects_;

                    dPoids = adContent.dPoids_;
                    dVolume = adContent.dVolume_;

                    if (iNbObjets <= 0)
                    {

                        Object[] tmp2 = new Object[drRow.ItemArray.Length + 1];
                        drRow.ItemArray.CopyTo(tmp2, 0);
                        tmp2[tmp2.Length - 1] = "Unable to process Ad Content item with destination (invalid number of objets) :" + adContent.sDestination_;
                        //Error : Le vol arrivée n'existe pas.
                        dtBagPlanConversionErrors.Rows.Add(tmp2);
                        continue;
                    }
                    Double dPoidUnitaire = dPoids / iNbObjets;
                    Double dVolumeUnitaire = dVolume / iNbObjets;
                    for (int i = 0; i < iNbObjets; i++)
                    {
                        //Pour chaque content de ce AD content, on génère une nouvelle ligne.
                        bAdded = AddRow(iCurrentID, drRow, dtTime, ffArrival, iClasse, adContent.sDestination_, dPoidUnitaire, dVolumeUnitaire, true, true);
                        if (!bAdded)
                        {
                            //Tous les objets ne peuvent trouver une destination. Donc on doit écrire un message pour l'indiquer
                            Object[] tmp2 = new Object[drRow.ItemArray.Length + 1];
                            drRow.ItemArray.CopyTo(tmp2, 0);
                            tmp2[tmp2.Length - 1] = "Unable to process Ad Content item with destination :" + adContent.sDestination_;
                            //Error : Le vol arrivée n'existe pas.
                            dtBagPlanConversionErrors.Rows.Add(tmp2);
                            break;
                        }
                    }
                } */
        //   }
                #endregion

      //<< Sodexi 14.03.2011
        //<< Sodexi 14.03.2011 added sAntenne parameter        
        // Sodexi Task#7129 Bagplan Update added dWidth, dLength, dHeight
        private bool AddRow(int i_ID,DataRow drRow, DateTime dtTime, FPFlight ffArrival, int iClasse, String sDestination,
                            Double dPoid, Double dVolume, Double dWidth, Double dLength, Double dHeight,
                            bool bAD, bool bAdContent, String sAntenne)
        {

            int iFPD = getDepartureFlight(sDestination, dtTime, iClasse, dPoid, dVolume, bAdContent);
            if (iFPD == -1)
            {
                return false;
            }
            FPFlight ffDeparture = dsffFPDFlights[iFPD.ToString()];

            /*Poste : un colis par ligne dans Fret/Poste*/
            AddRow(i_ID,
                OverallTools.DataFunctions.MinuteDifference(dtStartTime_, ffArrival.Date_),
                bAD,
                bAdContent,
                ffArrival.iID_,
                iClasse,
                ffDeparture.iID_,
                OverallTools.DataFunctions.MinuteDifference(dtStartTime_, ffDeparture.Date_),
                1,
                ffArrival.iTerminal_, ffArrival.iDeskIndex_,
                ffDeparture.iTerminal_, ffDeparture.iDeskIndex_
                );

            // << Sodexi Task#7129 Bagplan Update
            String originAirportCode = drRow[iIndex_sFretPoste_Airport].ToString();
            String destinationAirportCode = drRow[iIndex_sFretPoste_Destination].ToString();
            // << Sodexi Task#7495 Modification of Sodexi Project
            String customStatus = drRow[iIndex_sFretPoste_StatusDouanier].ToString();
            // >> Sodexi Task#7495 Modification of Sodexi Project
            addRowForBagPlan2(i_ID, OverallTools.DataFunctions.MinuteDifference(dtStartTime_, ffArrival.Date_),
                              bAD, bAdContent, ffArrival.iID_, iClasse, ffDeparture.iID_,
                              OverallTools.DataFunctions.MinuteDifference(dtStartTime_, ffDeparture.Date_),
                              1, ffArrival.iTerminal_, ffArrival.iDeskIndex_,
                              ffDeparture.iTerminal_, ffDeparture.iDeskIndex_,
                              dPoid, dLength, dWidth, dHeight, dVolume, originAirportCode, destinationAirportCode
                              , customStatus);     // << Sodexi Task#7495 Modification of Sodexi Project       
            addRowForBagPlanInformation(0, i_ID, ffArrival.sFlightNumber_, ffDeparture.sFlightNumber_, drRow[iIndex_sFretPoste_NLTA].ToString(),
                                        sAntenne, dPoid, dLength, dWidth, dHeight, dVolume, originAirportCode, destinationAirportCode
                                        , customStatus);   // << Sodexi Task#7495 Modification of Sodexi Project
            // >> Sodexi Task#7129 Bagplan Update
            return true;
        }

        private void AddRow(int iId,
            Double dTime,
            bool bAD,
            bool bADContent,
            int iFPA_ID,
            int iFretPoste,
            int iFPD_ID,
            double dSTD,
            int iNbBags,
            int iTerminalArrival,
            int iArrivalGate,
            int iTerminalCI,
            int iCheckIn)
        {
            DataRow drRow = dtBagPlan.NewRow();
            drRow[iIndexTime] = dTime;
            drRow[iIndexID] = iId;
            if ((!bAD) || (bAD && !bADContent))
                drRow[iIndexFirstBag] = 1;
            else
                drRow[iIndexFirstBag] = 0;
           /* if (bADContent_)
                drRow[iIndexFirstBag] = 0;
            else
                drRow[iIndexFirstBag] = 1;
            */
            drRow[iIndexFPA_ID] = iFPA_ID;
            drRow[iIndexFPA_Class] = iFretPoste;
            drRow[iIndexPaxAtReclaim] = dTime;
            drRow[iIndexFPD_ID] = iFPD_ID;
            drRow[iIndexFPD_Class] = iFretPoste;
            drRow[iIndexSTD] = dSTD;
            drRow[iIndexNbBags] = iNbBags;
            if (bAD)
                drRow[iIndexOOG_Bag] = 1;
            else
                drRow[iIndexOOG_Bag] = 0;

            drRow[iIndexSegregation] = 0;
            drRow[iIndexPassportLocal] = 1;
            drRow[iIndexTransfer] = 1;
            drRow[iIndexTermArr] = iTerminalArrival;
            drRow[iIndexArrGate] = iArrivalGate;
            drRow[iIndexTermCI] = iTerminalCI;
            drRow[iIndexCI] = iCheckIn;
            dtBagPlan.Rows.Add(drRow);
        }
        
        // << Sodexi Task#7129 Bagplan Update
        private void addRowForBagPlan2( int iId, Double dTime, bool bAD, bool bADContent, int iFPA_ID,
                                        int iFretPoste, int iFPD_ID, double dSTD, int iNbBags, int iTerminalArrival,
                                        int iArrivalGate, int iTerminalCI, int iCheckIn, Double dWeight, Double dLenght,
                                        Double dWidth, Double dHeight, Double dVolume,
                                        String sOriginAirportCode, String sDestinationAirportCode
                                        , String customStatus)  // Sodexi Task#7495 Modification of Sodexi Project
        {
            DataRow newRow = dtBagPlan2.NewRow();
            newRow[iIndexBP2Time] = dTime;
            if ((!bAD) || (bAD && !bADContent))
                newRow[iIndexBP2FirstBag] = 1;
            else
                newRow[iIndexBP2FirstBag] = 0;
            newRow[iIndexBP2BagId] = 0;
            newRow[iIndexBP2PaxID] = iId;
            newRow[iIndexBP2FPA_ID] = iFPA_ID;
            newRow[iIndexBP2FPA_Class] = iFretPoste;
            newRow[iIndexBP2PaxAtReclaim] = dTime;
            newRow[iIndexBP2FPD_ID] = iFPD_ID;
            newRow[iIndexBP2FPD_Class] = iFretPoste;
            newRow[iIndexBP2STD] = dSTD;
            newRow[iIndexBP2NbBags] = iNbBags;
            if (bAD)
                newRow[iIndexBP2OOG_Bag] = 1;
            else
                newRow[iIndexBP2OOG_Bag] = 0;
            newRow[iIndexBP2Segregation] = 0;
            newRow[iIndexBP2PassportLocal] = 1;
            newRow[iIndexBP2Transfer] = 1;
            newRow[iIndexBP2TermArr] = iTerminalArrival;
            newRow[iIndexBP2ArrGate] = iArrivalGate;
            newRow[iIndexBP2TermCI] = iTerminalCI;
            newRow[iIndexBP2CI] = iCheckIn;
            newRow[iIndexBP2Weight] = Math.Round(dWeight, 2, MidpointRounding.AwayFromZero);
            newRow[iIndexBP2Length] = Math.Round(dLenght, 2, MidpointRounding.AwayFromZero);
            newRow[iIndexBP2Width] = Math.Round(dWidth, 2, MidpointRounding.AwayFromZero);
            newRow[iIndexBP2Height] = Math.Round(dHeight, 2, MidpointRounding.AwayFromZero);
            newRow[iIndexBP2Volume] = Math.Round(dVolume, 2, MidpointRounding.AwayFromZero);
            if (localAirportsCodesDictionary.ContainsKey(sOriginAirportCode))
                newRow[iIndexBP2LocalOrig] = 1;
            else
                newRow[iIndexBP2LocalOrig] = 0;
            if (localAirportsCodesDictionary.ContainsKey(sDestinationAirportCode))
                newRow[iIndexBP2LocalDest] = 1;
            else
                newRow[iIndexBP2LocalDest] = 0;
            // << Sodexi Task#7495 Modification of Sodexi Project
            newRow[iIndexBP2CustomStatus] = customStatus;
            // >> Sodexi Task#7495 Modification of Sodexi Project
            String countryCode = "";
            newRow[iIndexBP2User1] = sOriginAirportCode;            
            if (localAirportsCodesDictionary.TryGetValue(sOriginAirportCode, out countryCode))
                newRow[iIndexBP2User2] = countryCode;            
            newRow[iIndexBP2User3] = sDestinationAirportCode; 
            if (localAirportsCodesDictionary.TryGetValue(sDestinationAirportCode, out countryCode))
                newRow[iIndexBP2User4] = countryCode;            
            //newRow[iIndexBP2User5] = ;
            dtBagPlan2.Rows.Add(newRow);
        }

        private void addRowForBagPlanInformation(int bagId, int paxId, String arrivalFlightNumber, String departureFlightNumber, String LTA,
                                                 String antenne, Double weight, Double length, Double width, Double height, Double volume,
                                                 String originAirportCode, String destinationAirportCode
                                                 , String customStatus)
        {
            DataRow newRow = dtBagPlanInformation.NewRow();
            newRow[iIndexBPIBagId] = bagId;
            newRow[iIndexBPIPaxId] = paxId;
            newRow[iIndexBPIArrivalFlight] = arrivalFlightNumber;
            newRow[iIndexBPIDepartureFlight] = departureFlightNumber;
            newRow[iIndexBPILTA] = LTA;
            //<< Sodexi 14.03.2011
            newRow[iIndexBPIAntenne] = antenne;
            //>> Sodexi 14.03.2011
            newRow[iIndexBPIWeight] = Math.Round(weight, 2, MidpointRounding.AwayFromZero);
            newRow[iIndexBPILength] = Math.Round(length, 2, MidpointRounding.AwayFromZero);
            newRow[iIndexBPIWidth] = Math.Round(width, 2, MidpointRounding.AwayFromZero);
            newRow[iIndexBPIHeight] = Math.Round(height, 2, MidpointRounding.AwayFromZero);
            newRow[iIndexBPIVolume] = Math.Round(volume, 2, MidpointRounding.AwayFromZero);
            if (localAirportsCodesDictionary.ContainsKey(originAirportCode))
                newRow[iIndexBPIOrigLocal] = 1;
            else
                newRow[iIndexBPIOrigLocal] = 0;
            if (localAirportsCodesDictionary.ContainsKey(destinationAirportCode))
                newRow[iIndexBPIDestLocal] = 1;
            else
                newRow[iIndexBPIDestLocal] = 0;
            // << Sodexi Task#7495 Modification of Sodexi Project
            newRow[iIndexBPICustomStatus] = customStatus;
            // >> Sodexi Task#7495 Modification of Sodexi Project
            newRow[iIndexBPIUser1] = originAirportCode;
            String countryCode = "";
            if (localAirportsCodesDictionary.TryGetValue(originAirportCode, out countryCode))
                newRow[iIndexBPIUser2] = countryCode;
            newRow[iIndexBPIUser3] = destinationAirportCode;
            if (localAirportsCodesDictionary.TryGetValue(destinationAirportCode, out countryCode))
                newRow[iIndexBPIUser4] = countryCode;
            dtBagPlanInformation.Rows.Add(newRow);
        }
        // >> Sodexi Task#7129 Bagplan Update
        #endregion

        #region Gestion des vols départs
        private class FPFlight
        {
            #region Partie concernant les informations sur le vol
            internal int iID_;
            internal DateTime Date_;
            internal String sAirport_;

            internal String sFlightNumber_;
            internal String sFlightCategory_;
            internal int iTerminal_;
            internal int iDeskIndex_;

            internal int iNumberFret;
            internal int iNumberPoste;
            internal int iNumberAD;

            internal Double dPoidFret;
            internal Double dPoidPoste;
            internal Double dPoidAD;

            internal Double dVolumeFret;
            internal Double dVolumePoste;
            internal Double dVolumeAD;
          //<< Sodexi 14.03.2011
            //boolean to store the AD Vol information
            // FPD.user4 = 1 => bADContent_ = true => the flight is selected to carry the AD item            
            internal Boolean bADContent_;
          //<< Sodexi 14.03.2011
            #endregion



            #region Partie concernant la capacité du vol.
            internal int iCapacity_;
            internal Double dVolume_;
            internal Double dPoids_;



            /// <summary>
            /// capacity left in the AD box that is currently feeling
            /// </summary>
            internal int iADBox;
            /// <summary>
            /// Poids restant dans l'ADbox qui est en cours de remplissage.
            /// </summary>
            internal Double dADPoids;
            /// <summary>
            /// Volume restant dans l'ADBox qui est en cours de remplissage.
            /// </summary>
            internal Double dADVolume;


            internal int iPosteBox;
            internal Double dPostePoids;
            internal Double dPosteVolume;


            internal int iFretBox;
            internal Double dFretPoids;
            internal Double dFretVolume;

            internal bool bMix;
            #endregion

            #region Partie concernant les paramétrages des container et des boites AD
            internal static int iCapacityContainer=60;
            internal static Double dPoidContainer=2000;
            internal static Double dVolumeContainer=2;
            internal static int iCapacityBox=40;
            internal static Double dPoidBox=15;
            internal static Double dVolumeBox=0.072;
            #endregion

            #region Constructeurs
            internal FPFlight(
                int iID,
                DateTime Date,
                TimeSpan STD,
                String sAirport,
                String sFlightNumber,
                String sFlightCategory,
                int iCapacity,
                Double dPoids,
                Double dVolume,
                int iTerminal,
                int iDeskIndex,
              //<< Sodexi 14.03.2011
                bool bADContent
              //>> Sodexi 14.03.2011
                )
            {
                iID_=iID;
                Date_ = Date.Add(STD);
                sAirport_=sAirport;
                dVolume_=dVolume;
                dPoids_=dPoids;
                iCapacity_ = iCapacity;
                sFlightNumber_ = sFlightNumber;
                sFlightCategory_ = sFlightCategory;
                iTerminal_=iTerminal;
                iDeskIndex_ = iDeskIndex;
                iADBox = 0;
                dADPoids = 0;
                dADVolume = 0;

                iPosteBox = 0;
                dPostePoids = 0;
                dPosteVolume = 0;

                iFretBox = 0;
                dFretPoids = 0;
                dFretVolume = 0;
                bMix = (sFlightCategory != SodexiNames.sFlightCategorie_NoMix);
              //<< Sodexi 14.03.2011
                bADContent_ = bADContent;
              //>> Sodexi 14.03.2011
            }
            #endregion

            #region Fonctions pour vérifier s'il reste de la place pour une nouvelle box.
            internal bool HasPlace(int iClasse, Double dPoid, Double dVolume, bool bAD)
            {
             //   if ((!bADContent_) && (bAD))    //if the flag is 1 for AD flight and item is AD we check the capacity. Otherwise we exit.
               //     return false;
                return HasPlace(1, dPoid, dVolume, iCapacity_, dPoids_, dVolume_); //search if flight got capacity for items
                #region 23/01/2012
                /*if (bAD)
                {
                    if (HasPlace(1, 0, 0, iADBox, dADPoids, dADVolume))
                        return true;
                }
                return HasPlace(1, 0, 0, iCapacity_, dPoids_, dVolume_);*/
                #endregion
                #region <23/01/2012
                /*if (bAD)
                {
                    if (HasPlace(1, dPoid, dVolume, iADBox, dADPoids, dADVolume))
                        return true;
                    if (bMix)
                    {
                        return HasPlace(iCapacityBox ??, dPoidBox, dVolumeBox, iCapacity_, dPoids_, dVolume_); 
                    }
                    else
                    {
                        if (HasPlace(iCapacityBox ??, dPoidBox, dVolumeBox, iFretBox, dFretPoids, dFretVolume))
                            return true;
                        return HasPlace(iCapacityContainer ??, dPoidContainer, dVolumeContainer, iCapacity_, dPoids_, dVolume_); 
                    }
                }
                else
                {
                    if (bMix)
                    {
                        return HasPlace(1,dPoid, dVolume, iCapacity_, dPoids_, dVolume_);
                    }
                    else
                    {
                        if (iClasse == 1)
                        {
                            if(!HasPlace(1,dPoid, dVolume, iPosteBox, dPostePoids, dPosteVolume))
                                return true;
                            }
                        else
                        {
                            if(!HasPlace(1,dPoid, dVolume, iFretBox, dFretPoids, dFretVolume))
                                return true;
                        }
                        return HasPlace(iCapacityContainer, dPoidContainer, dVolumeContainer, iCapacity_, dPoids_, dVolume_);
                    }
                }*/
                #endregion
            }
            internal static bool HasPlace(int iSpace, Double dPoid, Double dVolume, int iSpaceLeft, Double PoidLeft, Double dVolumeLeft)
            {
                if (iSpace > iSpaceLeft)    //capacity is reached for the nb of items
                    return false;
                if (dPoid > PoidLeft)       //capacity is reached for weight
                    return false;
                if (dVolume > dVolumeLeft)  //capacity is reached for volume
                    return false;
                return true;
            }
            #endregion

            #region Fonction pour ajouter un élément à ce vol départ.
            internal void AddElement(int iClasse,Double dPoid,  Double dVolume, bool bAD)
            {
                AddElement(iClasse, 1, dPoid, dVolume, bAD);
            }

            private void AddElement(int iClasse,int iNumber, Double dPoid, Double dVolume, bool bAD)
            {

                iCapacity_-=iNumber;
                dPoids_ -= dPoid;
                dVolume_ -= dVolume;
                if(bAD)
                {
                    iNumberAD+= iNumber;
                    dPoidAD+= dPoid;
                    dVolumeAD+= dVolume;
                }
                else if (iClasse == 1)
                {
                    iNumberPoste += iNumber;
                    dPoidPoste += dPoid;
                    dVolumePoste += dVolume;
                }
                else
                {
                    iNumberFret += iNumber;
                    dPoidFret += dPoid;
                    dVolumeFret += dVolume;
                }

                #region 23/01/2012
                

                /*if (bAD)
                {
                    if (iADBox == 0)
                    {
                        AddElement(iClasse, iCapacityBox, 0, 0, false);
                        iADBox = iCapacityBox;
                        dADPoids = 1000;
                        dADVolume = 1000;
                    }
                    iADBox--;
                    return;
                }
                else
                {
                    iCapacity_--;
                }*/
                #endregion

                #region <23/01/2012
                /*if (bAD)
                {
                    if (iADBox == 0)
                    {
                        AddElement(iClasse, iCapacityBox, dPoidBox, dVolume, false);
                        iADBox = iCapacityBox;
                        dADPoids = dPoidBox;
                        dADVolume = dVolumeBox;
                    }
                    iADBox--;
                    dADPoids -= dPoid;
                    dADVolume -= dVolume;
                    return;
                }
                else
                {
                    if (bMix)
                    {
                        iCapacity_--;
                        dPoids_ -= dPoid;
                        dVolume_ -= dVolume;
                    }
                    else
                    {
                        if (iClasse == 1)
                        {
                            if (iPosteBox == 0)
                            {
                                iCapacity_-=iCapacityContainer;
                                dPoids_ -= dPoidContainer;
                                dVolume_ -= dVolumeContainer;
                                iPosteBox = iCapacityContainer;
                                dPostePoids = dPoidContainer;
                                dPosteVolume = dVolumeContainer;
                            }
                            iPosteBox--;
                            dPostePoids -= dPoid;
                            dPosteVolume -= dVolume;
                        }
                        else
                        {
                            if (iFretBox == 0)
                            {
                                iCapacity_ -= iCapacityContainer;
                                dPoids_ -= dPoidContainer;
                                dVolume_ -= dVolumeContainer;
                                iFretBox = iCapacityContainer;
                                dFretPoids = dPoidContainer;
                                dFretVolume = dVolumeContainer;
                            }
                            iFretBox--;
                            dFretPoids -= dPoid;
                            dFretVolume -= dVolume;
                        }
                    }
                }*/
                #endregion
            }
            #endregion
        }

        private int getDepartureFlight(String sDestination, DateTime dtLimit, int iClasse /*1= Poste 2 = Fret*/,Double dPoid, Double dVolume, bool bAD)
        {
            DateTime dtDeparture = DateTime.MaxValue;
            int iIndex= -1;
            foreach (String sKey in dsffFPDFlights.Keys)
            {
                if (dsffFPDFlights[sKey].sAirport_ != sDestination)
                    continue;
                if (dsffFPDFlights[sKey].Date_ < dtLimit)
                    continue;
                if (dsffFPDFlights[sKey].Date_ > dtDeparture)
                    continue;
              //<< Sodexi 14.03.2011 
                if (!dsffFPDFlights[sKey].bADContent_ && bAD)
                    continue;
              //>> Sodexi 14.03.2011
                
                if (!dsffFPDFlights[sKey].HasPlace(iClasse, dPoid, dVolume, bAD))
                    continue;                
                                   
                iIndex = dsffFPDFlights[sKey].iID_;
                dtDeparture = dsffFPDFlights[sKey].Date_;
            }

            if (iIndex != -1)
            {
                String sKeyTmp = iIndex.ToString();
                dsffFPDFlights[sKeyTmp].AddElement(iClasse, dPoid, dVolume, bAD);
            }

            return iIndex;
        }

        private FPFlight getFlight(Dictionary<String, FPFlight> dsffListe,DateTime dtTime, String sFlightNumber)
        {
            foreach (String sKey in dsffListe.Keys)
            {
                if (dsffListe[sKey].Date_.Date != dtTime.Date)
                    continue;
                if (dsffListe[sKey].sFlightNumber_ != sFlightNumber)
                    continue;
                return dsffListe[sKey];
            }
            return null;
        }

        private DataTable GenerateDepartureStatistics()
        {
            DataTable dttable = new DataTable("Depature Statistics");
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_ID, typeof(Int32));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_FlightN, typeof(String));
            dttable.Columns.Add("#Fret", typeof(Int32));
            dttable.Columns.Add("#Poste", typeof(Int32));
            dttable.Columns.Add("#AD", typeof(Int32));
            dttable.Columns.Add("#Total", typeof(Int32));
            dttable.Columns.Add("Weight Fret", typeof(Double));
            dttable.Columns.Add("Weight Poste", typeof(Double));
            dttable.Columns.Add("Weight AD", typeof(Double));
            dttable.Columns.Add("Weight Total", typeof(Double));
            dttable.Columns.Add("Volume Fret", typeof(Double));
            dttable.Columns.Add("Volume Poste", typeof(Double));
            dttable.Columns.Add("Volume AD", typeof(Double));
            dttable.Columns.Add("Volume Total", typeof(Double));

            foreach (String sKey in dsffFPDFlights.Keys)
            {
                FPFlight fftmp = dsffFPDFlights[sKey];
                DataRow drRow = dttable.NewRow();
                drRow[0] = fftmp.iID_;
                drRow[1] = fftmp.sFlightNumber_;

                drRow[2] = fftmp.iNumberFret;
                drRow[3] = fftmp.iNumberPoste;
                drRow[4] = fftmp.iNumberAD;
                drRow[5] = fftmp.iNumberFret + fftmp.iNumberPoste + fftmp.iNumberAD;

                drRow[6] = fftmp.dPoidFret;
                drRow[7] = fftmp.dPoidPoste;
                drRow[8] = fftmp.dPoidAD;
                drRow[9] = fftmp.dPoidAD + fftmp.dPoidPoste + fftmp.dPoidFret;

                drRow[10] = fftmp.dVolumeFret;
                drRow[11] = fftmp.dVolumePoste;
                drRow[12] = fftmp.dVolumeAD;
                drRow[13] = fftmp.dVolumeAD + fftmp.dPoidFret + fftmp.dVolumeFret;
                dttable.Rows.Add(drRow);
            }
            return dttable;
        }

        private DataTable GenerateFPDStatisticsExtended()
        {
            int indexColumnDepartureFlightID = dtFPDTable_.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);

            DataTable dttable = new DataTable(GlobalNames.FPDStatisticsExtendedName);

            #region FPDStatisticsExtended columns
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_ID, typeof(Int32));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_DATE, System.Type.GetType("System.DateTime"));
            dttable.Columns.Add(GlobalNames.sFPD_Column_STD, System.Type.GetType("System.TimeSpan"));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_AirlineCode, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_FlightN, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_AirportCode, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_FlightCategory, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_AircraftType, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_TSA, System.Type.GetType("System.Boolean"));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_NbSeats, System.Type.GetType("System.Int32"));

            dttable.Columns.Add(GlobalNames.sFPD_Column_TerminalCI, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_Eco_CI_Start, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_Eco_CI_End, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_FB_CI_Start, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_FB_CI_End, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_Eco_Drop_Start, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_Eco_Drop_End, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_FB_Drop_Start, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_FB_Drop_End, typeof(String));

            dttable.Columns.Add(GlobalNames.sFPD_A_Column_TerminalGate, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_BoardingGate, typeof(String));

            dttable.Columns.Add(GlobalNames.sFPD_Column_TerminalMup, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_Eco_Mup_Start, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_Eco_Mup_End, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_First_Mup_Start, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_Column_First_Mup_End, typeof(String));

            dttable.Columns.Add(GlobalNames.sFPD_A_Column_TerminalParking, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_Parking, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_RunWay, typeof(String));

            dttable.Columns.Add(GlobalNames.sFPD_A_Column_User1, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_User2, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_User3, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_User4, typeof(String));
            dttable.Columns.Add(GlobalNames.sFPD_A_Column_User5, typeof(String));

            dttable.Columns.Add("#Fret", typeof(Int32));
            dttable.Columns.Add("#Poste", typeof(Int32));
            dttable.Columns.Add("#AD", typeof(Int32));
            dttable.Columns.Add("#Total", typeof(Int32));
            dttable.Columns.Add("Weight Fret", typeof(Double));
            dttable.Columns.Add("Weight Poste", typeof(Double));
            dttable.Columns.Add("Weight AD", typeof(Double));
            dttable.Columns.Add("Weight Total", typeof(Double));
            dttable.Columns.Add("Volume Fret", typeof(Double));
            dttable.Columns.Add("Volume Poste", typeof(Double));
            dttable.Columns.Add("Volume AD", typeof(Double));
            dttable.Columns.Add("Volume Total", typeof(Double));
/*
            dttable.Columns.Add(GlobalNames.FPDStatisticsExtended_ItemTotal, typeof(Int32));
            dttable.Columns.Add(GlobalNames.FPDStatisticsExtended_WeightTotal, typeof(Double));
            dttable.Columns.Add(GlobalNames.FPDStatisticsExtended_VolumeTotal, typeof(Double));
*/
            #endregion

            foreach (DataRow dr in dtFPDTable_.Rows)
            {
                String key = dr[indexColumnDepartureFlightID].ToString();
                if (dsffFPDFlights.ContainsKey(key))
                {
                    DataRow drRow = dttable.NewRow();

                    #region copy FPD data
                    drRow[GlobalNames.sFPD_A_Column_ID] = dr[GlobalNames.sFPD_A_Column_ID];
                    drRow[GlobalNames.sFPD_A_Column_DATE] = dr[GlobalNames.sFPD_A_Column_DATE];
                    drRow[GlobalNames.sFPD_Column_STD] = dr[GlobalNames.sFPD_Column_STD];
                    drRow[GlobalNames.sFPD_A_Column_AirlineCode] = dr[GlobalNames.sFPD_A_Column_AirlineCode];
                    drRow[GlobalNames.sFPD_A_Column_FlightN] = dr[GlobalNames.sFPD_A_Column_FlightN];
                    drRow[GlobalNames.sFPD_A_Column_AirportCode] = dr[GlobalNames.sFPD_A_Column_AirportCode];
                    drRow[GlobalNames.sFPD_A_Column_FlightCategory] = dr[GlobalNames.sFPD_A_Column_FlightCategory];
                    drRow[GlobalNames.sFPD_A_Column_AircraftType] = dr[GlobalNames.sFPD_A_Column_AircraftType];
                    drRow[GlobalNames.sFPD_Column_TSA] = dr[GlobalNames.sFPD_Column_TSA];
                    drRow[GlobalNames.sFPD_A_Column_NbSeats] = dr[GlobalNames.sFPD_A_Column_NbSeats];

                    drRow[GlobalNames.sFPD_Column_TerminalCI] = dr[GlobalNames.sFPD_Column_TerminalCI];
                    drRow[GlobalNames.sFPD_Column_Eco_CI_Start] = dr[GlobalNames.sFPD_Column_Eco_CI_Start];
                    drRow[GlobalNames.sFPD_Column_Eco_CI_End] = dr[GlobalNames.sFPD_Column_Eco_CI_End];
                    drRow[GlobalNames.sFPD_Column_FB_CI_Start] = dr[GlobalNames.sFPD_Column_FB_CI_Start];
                    drRow[GlobalNames.sFPD_Column_FB_CI_End] = dr[GlobalNames.sFPD_Column_FB_CI_End];
                    drRow[GlobalNames.sFPD_Column_Eco_Drop_Start] = dr[GlobalNames.sFPD_Column_Eco_Drop_Start];
                    drRow[GlobalNames.sFPD_Column_Eco_Drop_End] = dr[GlobalNames.sFPD_Column_Eco_Drop_End];
                    drRow[GlobalNames.sFPD_Column_FB_Drop_Start] = dr[GlobalNames.sFPD_Column_FB_Drop_Start];
                    drRow[GlobalNames.sFPD_Column_FB_Drop_End] = dr[GlobalNames.sFPD_Column_FB_Drop_End];

                    drRow[GlobalNames.sFPD_A_Column_TerminalGate] = dr[GlobalNames.sFPD_A_Column_TerminalGate];
                    drRow[GlobalNames.sFPD_Column_BoardingGate] = dr[GlobalNames.sFPD_Column_BoardingGate];

                    drRow[GlobalNames.sFPD_Column_TerminalMup] = dr[GlobalNames.sFPD_Column_TerminalMup];
                    drRow[GlobalNames.sFPD_Column_Eco_Mup_Start] = dr[GlobalNames.sFPD_Column_Eco_Mup_Start];
                    drRow[GlobalNames.sFPD_Column_Eco_Mup_End] = dr[GlobalNames.sFPD_Column_Eco_Mup_End];
                    drRow[GlobalNames.sFPD_Column_First_Mup_Start] = dr[GlobalNames.sFPD_Column_First_Mup_Start];
                    drRow[GlobalNames.sFPD_Column_First_Mup_End] = dr[GlobalNames.sFPD_Column_First_Mup_End];

                    drRow[GlobalNames.sFPD_A_Column_TerminalParking] = dr[GlobalNames.sFPD_A_Column_TerminalParking];
                    drRow[GlobalNames.sFPD_A_Column_Parking] = dr[GlobalNames.sFPD_A_Column_Parking];
                    drRow[GlobalNames.sFPD_A_Column_RunWay] = dr[GlobalNames.sFPD_A_Column_RunWay];

                    drRow[GlobalNames.sFPD_A_Column_User1] = dr[GlobalNames.sFPD_A_Column_User1];
                    drRow[GlobalNames.sFPD_A_Column_User2] = dr[GlobalNames.sFPD_A_Column_User2];
                    drRow[GlobalNames.sFPD_A_Column_User3] = dr[GlobalNames.sFPD_A_Column_User3];
                    drRow[GlobalNames.sFPD_A_Column_User4] = dr[GlobalNames.sFPD_A_Column_User4];
                    drRow[GlobalNames.sFPD_A_Column_User5] = dr[GlobalNames.sFPD_A_Column_User5];
                    #endregion

                    // retrieve the flight based on the ID                
                    FPFlight fftmp = dsffFPDFlights[key];
                    /*
                    drRow[GlobalNames.FPDStatisticsExtended_ItemTotal] = fftmp.iNumberFret + fftmp.iNumberPoste + fftmp.iNumberAD;
                    drRow[GlobalNames.FPDStatisticsExtended_WeightTotal] = fftmp.dPoidAD + fftmp.dPoidPoste + fftmp.dPoidFret;
                    drRow[GlobalNames.FPDStatisticsExtended_VolumeTotal] = fftmp.dVolumeAD + fftmp.dPoidFret + fftmp.dVolumeFret;
                    */
                    drRow["#Fret"] = fftmp.iNumberFret;
                    drRow["#Poste"] = fftmp.iNumberPoste;
                    drRow["#AD"] = fftmp.iNumberAD;
                    drRow["#Total"] = fftmp.iNumberFret + fftmp.iNumberPoste + fftmp.iNumberAD;

                    drRow["Weight Fret"] = fftmp.dPoidFret;
                    drRow["Weight Poste"] = fftmp.dPoidPoste;
                    drRow["Weight AD"] = fftmp.dPoidAD;
                    drRow["Weight Total"] = fftmp.dPoidAD + fftmp.dPoidPoste + fftmp.dPoidFret;

                    drRow["Volume Fret"] = fftmp.dVolumeFret;
                    drRow["Volume Poste"] = fftmp.dVolumePoste;
                    drRow["Volume AD"] = fftmp.dVolumeAD;
                    drRow["Volume Total"] = fftmp.dVolumeAD + fftmp.dPoidFret + fftmp.dVolumeFret;                    
                               
                    dttable.Rows.Add(drRow);
                }
            }
            return dttable;
        }
        #endregion
    }
    #endregion
}
