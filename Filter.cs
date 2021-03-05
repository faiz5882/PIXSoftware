using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Drawing;
using Microsoft.JScript;
using Microsoft.JScript.Vsa;    // << Bug #9439 Pax2Sim - Simcore version bug - order of operations


namespace SIMCORE_TOOL
{
    internal class Filter
    {

        #region Membres publiques statiques
        /// <summary>
        /// La liste des opérateurs qui peuvent être utilisés pour la création d'un filtre. Ces opérateurs
        /// ne sont pas sensibles à la casse (les minuscules ou majuscules ne différencient pas les 
        /// opérateurs). Une description de chacune de ces opérateurs est disponible dans l'aide en ligne. 
        /// </summary>
/*        public static String[] ListOperateurs ={ " = ", " <> ", " < ", " <= ", " > ", " >= ", " + ", " - ", 
                                                 " * ", " / "," and "," or "," xor ", " not( ", " If( ", 
                                                 " ToDate( ", " FullDateToString( ", " FullDateToTime( ",
                                                 " ToString( ", " ToTime( ", " ToInteger( ", " ToReal( ",
                                                 " True ", " False ", " Contains( "," IsEven( ", " IsOdd( ",
                                                 " Modulo( ", " Left( ", " Right( ", " SubString( ", " Length( ",
                                                 " LineNum ", " Value( ",
                                                 " SQRT( ", " Square( ",   // << Task #8986 Pax2Sim - Filters - Expression functions
                                                 " RoundCeiling( " , " " + GlobalNames.DATETIME_TO_DHMS + "( ", // >> Task #10010 Pax2Sim - Filters - Ceiling function
                                                 " " + GlobalNames.ROUND + "( "};
*/

        public static String[] ListOperateurs ={ " = ", " <> ", " < ", " <= ", " > ", " >= ", " + ", " - ", " * ", " / ",
                                                 " LineNum ", " True ", " False "," Value( ",
                                                 " If( ", " not( ", " and "," or "," xor ", " IsEven( ", " IsOdd( ", " Contains( ",
                                                 " ToString( ", " SubString( ", " Length( ", " Left( ", " Right( ", " FullDateToString( ",
                                                 " FullDateToTime( ", " StringToFullDate( ", " StringToTime( ", " " + GlobalNames.DATETIME_TO_DHMS + "( ",
                                                 " " + GlobalNames.ROUND + "( ", " RoundCeiling( " , " SQRT( ", " Square( ", " Modulo( ", " ToInteger( ", " ToReal( "
                                               };

        /// <summary>
        /// Ces "Opération"s ne sont pas disponible. Ils s'apparentent à des opérations de base de données,
        /// mais n'ont jamais été intégré ni développée.
        /// </summary>
        public static String[] ListOperation = { "Group", "Min", "Max", "Mean", "Sum", "Count", "Accumul" };
        #endregion

        #region Membres privés statiques
        /// <summary>
        /// Cette liste regroupera toutes les erreurs apparues lors de l'évaluation du filtre, ou lors du
        /// calcul de celui ci. Cette liste pourra alors être affichée à l'utilisateur afin qu'il effectue
        /// les changements nécessaires.
        /// </summary>
        private static ArrayList ListErreurs = new ArrayList();
        #endregion

        #region Les variables représentant le filtre
        /// <summary>
        /// \ref listColumnsNames contient le nom de toutes les colonnes qui seront présentes dans la table
        /// représentée par ce filtre. Cette liste est intimement liée aux listes \ref listFormules, \ref
        /// listDisplay, \ref listConditions, \ref listOperationType, \ref listFormulesTree. 
        /// Les informations contenues à un indice x de cette liste représentera les informations concernant
        /// la colonne x de la future table. 
        /// </summary>
        private ArrayList listColumnsNames;

        /// <summary>
        /// \ref listDisplay contient une liste de booléen permettant de savoir si la colonne sera visible
        /// dans la table calculée à partir du filtre ou non. Cela permet de masquer par exemple les 
        /// colonnes qui ne sont utilisée que pour trier la table mère. (cf \ref listColumnsNames pour plus
        /// d'informations sur le fonctionnement de la liste)
        /// </summary>
        private ArrayList listDisplay;

        /// <summary>
        /// \ref listFormules contient les formules à évaluer pour calculer le contenu des colonnes.
        /// (cf \ref listColumnsNames pour plus d'informations sur le fonctionnement de la liste)
        /// </summary>
        private ArrayList listFormules;

        /// <summary>
        /// \ref listConditions contient les formules à comparer aux formules \ref listFormules pour 
        /// savoir si une ligne de la table mère doit apparaître dans la table fille.
        /// (cf \ref listColumnsNames pour plus d'informations sur le fonctionnement de la liste)
        /// </summary>
        private ArrayList listConditions;

        /// <summary>
        /// Cette liste doit être renseignée, mais n'est pas utilisée. L'information qu'elle contient
        /// est en effet une provision à un futur fonctionnement.
        /// </summary>
        private ArrayList listOperationType;

        /// <summary>
        /// Cette liste est mise à jour lors de l'évaluation des filtres pour le calcul du filtre. Elle
        /// contient alors l'interprétation des listes \ref listFormules et \ref listConditions. Cela 
        /// permet ensuite de calculer et d'évaluer pour chaque ligne de la table mère si elle doit 
        /// apparaître dans la table du filtre et sous quel format.
        /// </summary>
        private List<ExpressionTree> listFormulesTree;

        /// <summary>
        /// Le nom donné au filtre courant. Ce nom sera unique au niveau d'un \ref DataManager.
        /// </summary>
        private String sName;

        /// <summary>
        /// La table à laquelle le filtre courant s'applique.
        /// </summary>
        private String sMotherTableName;

        /// <summary>
        /// Booléen indiquant si la table résultant du filtre sera modifiable et enregistrée au 
        /// même titre que n'importe quelle autre table, ou si le filtre sera réévalué à partir de la
        /// table mêre \ref sMotherTableName dès que l'on en aura besoin.
        /// </summary>
        private bool bCopyTable;

        /// <summary>
        /// Ce booléen indique si les données contenues dans le filtre sont modifiable ou non. En
        /// effet certain filtre ont des données modifiables qui sont alors remontées à la table 
        /// mère \ref sMotherTableName. Mais certaines conditions font que cela n'est pas possible.
        /// </summary>
        private bool bBlocked; 

        /// <summary>
        /// Booléen indiquant si le filtre doit hériter du \ref VisualisationMode de son parent ou pas.
        /// </summary>
        private bool bInheritedVisualisationMode;
        #endregion

        #region Les constructeurs de la classe.
        private void initilizeFilter()
        {
            listColumnsNames = new ArrayList();
            listFormules = new ArrayList();
            listDisplay = new ArrayList();
            listConditions = new ArrayList();
            listOperationType = new ArrayList();
            listFormulesTree = null;
            MotherTableName = "";
            Name = "";
            bCopyTable = false;
            bBlocked = true;
            bInheritedVisualisationMode = false;
        }
        public Filter(String Name_,
            String NomTableMere_,
            ArrayList listColumnsNames_,
            ArrayList listFormules_,
            ArrayList listDisplay_,
            ArrayList listConditions_,
            ArrayList listOperationType_,
            bool bCopyTable_,
            bool bInheritedVisualisationMode_)
        {
            listColumnsNames = listColumnsNames_;
            listFormules = listFormules_;
            listDisplay = listDisplay_;
            listConditions = listConditions_;
            listOperationType = listOperationType_;
            listFormulesTree = null;
            Name = Name_;
            MotherTableName = NomTableMere_;
            bCopyTable = bCopyTable_;

            bBlocked = true;
            bInheritedVisualisationMode = bInheritedVisualisationMode_;
        }

        public Filter(System.Xml.XmlNode Noeud)
        {
            initilizeFilter();
            if ((!OverallTools.FonctionUtiles.hasNamedAttribute(Noeud, "Name")))
            {

                ListeErreurs.Add("Err01348 : The xml definition for the filter is not valid");
            }
            if ((!OverallTools.FonctionUtiles.hasNamedAttribute(Noeud, "ParentTable")) ||
                (!OverallTools.FonctionUtiles.hasNamedAttribute(Noeud, "Copy")))
            {
                ListeErreurs.Add("Err01349 : The xml definition for the filter \"" + Noeud.Attributes["Name"].Value + "\" is not valid");
                return;
            }
            bInheritedVisualisationMode = OverallTools.FonctionUtiles.hasNamedAttribute(Noeud, "InheritedVisualisationMode");
            Name = Noeud.Attributes["Name"].Value;
            MotherTableName = Noeud.Attributes["ParentTable"].Value;
            bCopyTable = FonctionsType.getBoolean(Noeud.Attributes["Copy"].Value);
            bBlocked = true;
            System.Xml.XmlAttribute xaBlocked = Noeud.Attributes["Locked"];
            if(xaBlocked != null)
                bBlocked = FonctionsType.getBoolean(xaBlocked.Value);
            System.Xml.XmlElement colonnes = (System.Xml.XmlElement)Noeud.FirstChild;
            int i = 0;
            foreach (System.Xml.XmlElement colonne in colonnes.ChildNodes)
            {
                listColumnsNames.Add(colonne.Attributes["Name"].Value);
                listFormules.Add(colonne.Attributes["Formula"].Value);
                listConditions.Add(colonne.Attributes["Condition"].Value);
                listDisplay.Add(FonctionsType.getBoolean(colonne.Attributes["Display"].Value));
                listOperationType.Add(colonne.Attributes["OperationType"].Value);
                i++;
            }
        }
        #endregion

        #region La classe pour l'analyse des formules

        #region La classe héritée de toutes les classes qui analyse les expressions.
        private abstract class ExpressionNode
        {
            protected Type tType_;
            public abstract Object AppliquerExpression(DataRow ligne);
            public abstract bool verifierExpression(DataTable laTable);

            public abstract Object getValue(DataTable laTable, int iIndex);
            public Type tType
            {
                get
                {
                    return tType_;
                }
                set
                {
                    tType_ = value;
                }
            }
        }
        #endregion

        #region La classe Feuille qui représente chaque feuille de l'arbre de l'expression
        private class ExpressionLeaf : ExpressionNode
        {
            public enum typeOperande { Colonne, Value };
            private object oOperande;
            private String operande;
            private typeOperande type;
            public ExpressionLeaf(String sOperandeName)
            {
                operande = sOperandeName;
                if (sOperandeName == "linenum")
                {
                    tType = typeof(Int32);
                    type = typeOperande.Value;
                }
                else
                {
                    tType = null;
                    type = typeOperande.Colonne;
                }
            }
            public ExpressionLeaf(Object oValue, Type tType__)
            {
                oOperande = oValue;
                operande = "";
                tType = tType__;
                type = typeOperande.Value;
            }

            public override bool verifierExpression(DataTable laTable)
            {
                if (type == typeOperande.Value)
                {
                    return true;
                }
                if (!laTable.Columns.Contains(operande))
                {
                    ListErreurs.Add("Err01300 : The table does not contain a column named \"" + operande + "\".");
                    return false;
                }
                tType = laTable.Columns[operande].DataType;
                return true;
            }

            public bool verifierValidColumn(DataTable laTable) 
            {
                if (type == typeOperande.Value)
                    return false;
                return laTable.Columns.Contains(operande);
            }
            public override Object AppliquerExpression(DataRow ligne)
            {
                if (type == typeOperande.Colonne)
                {
                    return ligne.ItemArray[ligne.Table.Columns.IndexOf(operande)];
                }
                else if (operande == "linenum")
                {
                    return ligne.Table.Rows.IndexOf(ligne)+1;
                }
                else
                {
                    return oOperande;
                }
            }
            public override Object getValue(DataTable laTable, int iIndex)
            {
                if (type != typeOperande.Colonne)
                    return null;
                if(!laTable.Columns.Contains(operande))
                    return null;
                if(laTable.Rows.Count<iIndex)
                    return null;
                if (0 >= iIndex)
                    return null;
                return laTable.Rows[iIndex-1][operande];
            }
        }
        #endregion

        #region la Classe Branche qui représente chaque branche de l'arbre de l'expression
        private class ExpressionBranch : ExpressionNode
        {
            private ExpressionNode branche1, branche2, branche3;
            private String operateur;

            #region Les constructeurs de la classe de branche
            public ExpressionBranch(ExpressionNode branche1_, ExpressionNode branche2_, ExpressionNode branche3_, String operateur_)
            {
                branche1 = branche1_;
                branche2 = branche2_;
                branche3 = branche3_;
                operateur = operateur_;
            }

            public ExpressionBranch(ExpressionNode branche1_, ExpressionNode branche2_, String operateur_)
            {
                branche1 = branche1_;
                branche2 = branche2_;
                branche3 = null;
                operateur = operateur_;
            }
            public ExpressionBranch(ExpressionNode branche1_, String operateur_)
            {
                //Il s'agit d'une branche où l'opérateur ne prend qu'un seul argument.
                branche1 = branche1_;
                branche2 = null;
                branche3 = null;
                operateur = operateur_;
            }
            #endregion

            public void changeBranch1(ExpressionNode Branch1)
            {
                if (Branch1 != null)
                    branche1 = Branch1;
            }

            #region public override bool verifierExpression(DataTable laTable)
            public override bool verifierExpression(DataTable laTable)
            {
                if (branche1 != null)
                {
                    if (!branche1.verifierExpression(laTable))
                    {
                        return false;
                    }
                }
                else
                {
                    ListErreurs.Add("Err01301 : The operator \"" + operateur + "\" needs a first operand.");
                    return false;
                }
                if (branche2 != null)
                {
                    if (!branche2.verifierExpression(laTable))
                    {
                        return false;
                    }
                }
                if (branche3 != null)
                {
                    if (!branche3.verifierExpression(laTable))
                    {
                        return false;
                    }
                }
                Type tTypeTemp1 = branche1.tType;
                Type tTypeTemp2;
                Type tTypeTemp3;
                #region Les fonctions avec uniquement un paramètre.
                if (branche2 == null)
                {
                    //Les fonctions avec uniquement un paramètre
                    switch (operateur)
                    {
                        case "-":
                            if (FonctionsType.isInt(tTypeTemp1) || FonctionsType.isDouble(tTypeTemp1))
                                tType = tTypeTemp1;
                            else
                            {
                                ListErreurs.Add("Err01354 : The operator \"-\" must be use with numerical values.");
                                return false;
                            }
                            break;
                        case "todate":  // >> Task #16728 PAX2SIM Improvements (Recurring) C#25
                        case "stringtofulldate":
                            if (!(FonctionsType.isString(tTypeTemp1) ||
                                    FonctionsType.isDate(tTypeTemp1)))
                            {
                                ListErreurs.Add("Err01304 : Please use the 'ToDate(' or 'StringToFullDate' function with string or date.");
                                return false;
                            }
                            tType = typeof(DateTime);
                            break;                        
                        case "fulldatetostring":
                            if (!(FonctionsType.isDate(tTypeTemp1)))
                            {
                                ListErreurs.Add("Err0610 : Please use the 'FullDateToString(' function with date.");
                                return false;
                            }
                            tType = typeof(String);
                            break;
                        case "fulldatetotime":
                            bool b = FonctionsType.isDate(tTypeTemp1);
                            b = FonctionsType.isString(tTypeTemp1);
                            if (!(FonctionsType.isDate(tTypeTemp1) ||
                                FonctionsType.isString(tTypeTemp1)))
                            {
                                ListErreurs.Add("Err0611 : Please use the 'FullDateToTime(' function with date or String.");
                                return false;
                            }
                            tType = typeof(TimeSpan);
                            break;
                        case "tostring":
                            tType = typeof(String);
                            break;
                        case "stringtotime":
                            if (!(FonctionsType.isString(tTypeTemp1) ||
                                    FonctionsType.isTime(tTypeTemp1)))
                            {
                                ListErreurs.Add("Err01305 : Please use the 'ToTime(' function with string with the format 'hh:mm:ss'.");
                                return false;
                            }
                            tType = typeof(TimeSpan);
                            break;
                        case "tointeger":
                            if (!((FonctionsType.isString(tTypeTemp1)) ||
                                 (FonctionsType.isInt(tTypeTemp1)) ||
                                  (FonctionsType.isDouble(tTypeTemp1))))
                            {
                                ListErreurs.Add("Err01361 : The 'ToInteger(' function accept type string, double or integer.");
                                return false;
                            }
                            tType = typeof(Int32);
                            break;
                        case "toreal":
                            if (!((FonctionsType.isString(tTypeTemp1)) ||
                                 (FonctionsType.isInt(tTypeTemp1)) ||
                                  (FonctionsType.isDouble(tTypeTemp1))))
                            {
                                ListErreurs.Add("Err01362 : The 'ToReal(' function accept type string, double or integer.");
                                return false;
                            }
                            tType = typeof(Double);
                            break;
                        case "not":
                            if (!FonctionsType.isBoolean(tTypeTemp1))
                            {
                                ListErreurs.Add("Err01306 : Please use the 'Not(' function with a boolean expression.");
                                return false;
                            }
                            tType = typeof(Boolean);
                            break;
                        case "iseven":
                            if (!((FonctionsType.isInt(tTypeTemp1)) ||
                                  (FonctionsType.isDouble(tTypeTemp1))))
                            {
                                ListErreurs.Add("Err01308 : The 'IsEven(' function accept type double or integer.");
                                return false;
                            }
                            if (FonctionsType.isDouble(tTypeTemp1))
                                ListErreurs.Add("Warn01364 : Warning, you are using the 'IsEven(' function with a double value, this value will be truncated to the smalelst integer.");
                            tType = typeof(Boolean);
                            break;
                        case "isodd":
                            if (!((FonctionsType.isInt(tTypeTemp1)) ||
                                  (FonctionsType.isDouble(tTypeTemp1))))
                            {
                                ListErreurs.Add("Err01363 :The 'IsOdd(' function accept type double or integer.");
                                return false;
                            }
                            if (FonctionsType.isDouble(tTypeTemp1))
                                ListErreurs.Add("Warn01365 : Warning, you are using the 'IsOdd(' function with a double value, this value will be truncated to the smalelst integer.");
                            tType = typeof(Boolean);
                            break;
                        case "length":
                            if (!FonctionsType.isString(tTypeTemp1))
                            {
                                ListErreurs.Add("Err01366 :The 'length(' function accept type String.");
                                return false;
                            }
                            tType = typeof(Int32);
                            break;
                        case "sqrt":
                            // << Task #8986 Pax2Sim - Filters - Expression functions                            
                            if (!((FonctionsType.isString(tTypeTemp1)) ||
                                 (FonctionsType.isInt(tTypeTemp1)) ||
                                  (FonctionsType.isDouble(tTypeTemp1))))
                            {
                                ListErreurs.Add("Err01361 : The 'Square Root' function accepts only the following types: string, double or integer.");
                                return false;
                            }
                            tType = typeof(Double);
                            break;
                            // >> Task #8986 Pax2Sim - Filters - Expression functions
                        case "square":
                            // << Task #8986 Pax2Sim - Filters - Expression functions                            
                            if (!((FonctionsType.isString(tTypeTemp1)) ||
                                 (FonctionsType.isInt(tTypeTemp1)) ||
                                  (FonctionsType.isDouble(tTypeTemp1))))
                            {
                                ListErreurs.Add("Err01361 : The 'Square' function accepts only the following types: string, double or integer.");
                                return false;
                            }
                            tType = typeof(Double);
                            break;
                            // >> Task #8986 Pax2Sim - Filters - Expression functions
                        case GlobalNames.CEILING_FUNCTION_IDENTIFIER:
                            // >> Task #10010 Pax2Sim - Filters - Ceiling function
                            if (!((FonctionsType.isString(tTypeTemp1)) ||
                                 (FonctionsType.isInt(tTypeTemp1)) ||
                                  (FonctionsType.isDouble(tTypeTemp1))))
                            {
                                ListErreurs.Add("Err01361 : The 'Ceiling' function accepts only the following types: string, double or integer.");
                                return false;
                            }
                            tType = typeof(Double);
                            break;
                            // << Task #10010 Pax2Sim - Filters - Ceiling function                        
                        default:
                            ListErreurs.Add("Err01302 : The operator \"" + operateur + "\" needs a second operand.");
                            return false;
                    }
                }
                #endregion
                #region Les fonctions avec deux paramètres.
                else if (branche3 == null)
                {
                    tTypeTemp2 = branche2.tType;
                    switch (operateur)
                    {
                        case ">":
                        case "<":
                        case ">=":
                        case "<=":
                            if (tTypeTemp1 != tTypeTemp2)
                            {
                                if (!((FonctionsType.isInt(tTypeTemp1) && FonctionsType.isDouble(tTypeTemp2)) ||
                                    (FonctionsType.isDouble(tTypeTemp1) && FonctionsType.isInt(tTypeTemp2)) ||
                                    (FonctionsType.isDouble(tTypeTemp1) && FonctionsType.isDouble(tTypeTemp2)) ||
                                    (FonctionsType.isInt(tTypeTemp1) && FonctionsType.isInt(tTypeTemp2)) ||
                                    (FonctionsType.isString(tTypeTemp1) && FonctionsType.isInt(tTypeTemp2)) ||
                                    (FonctionsType.isString(tTypeTemp1) && FonctionsType.isDouble(tTypeTemp2)) ||
                                    (FonctionsType.isInt(tTypeTemp1) && FonctionsType.isString(tTypeTemp2)) ||
                                    (FonctionsType.isDouble(tTypeTemp1) && FonctionsType.isString(tTypeTemp2))))
                                {
                                    ListErreurs.Add("Err01307 : Please use '" + operateur + "' between 2 values of the same type.");
                                    return false;
                                }
                            }
                            /*if (FonctionsType.isString(tTypeTemp1))
                            {
                                ListErreurs.Add("Err01308 : Cannot use '" + operateur + "' to compare 2 string.");
                                return false;
                            }*/
                            tType = typeof(Boolean);
                            break;
                        case "=":
                        case "<>":
                            if (tTypeTemp1 != tTypeTemp2)
                            {
                                if (!((FonctionsType.isInt(tTypeTemp1) && FonctionsType.isDouble(tTypeTemp2)) ||
                                    (FonctionsType.isDouble(tTypeTemp1) && FonctionsType.isInt(tTypeTemp1)) ||
                                    (FonctionsType.isDouble(tTypeTemp1) && FonctionsType.isDouble(tTypeTemp2)) ||
                                    (FonctionsType.isInt(tTypeTemp1) && FonctionsType.isInt(tTypeTemp2)) ||
                                    (FonctionsType.isString(tTypeTemp1) && FonctionsType.isInt(tTypeTemp2)) ||
                                    (FonctionsType.isString(tTypeTemp1) && FonctionsType.isDouble(tTypeTemp2)) ||
                                    (FonctionsType.isInt(tTypeTemp1) && FonctionsType.isString(tTypeTemp2)) ||
                                    (FonctionsType.isDouble(tTypeTemp1) && FonctionsType.isString(tTypeTemp2))))
                                {
                                    ListErreurs.Add("Err01303 : Please use '" + operateur + "' between 2 values of the same type.");
                                    return false;
                                }
                            }
                            tType = typeof(Boolean);
                            break;
                        case "+":
                            if (FonctionsType.isString(tTypeTemp1) || FonctionsType.isString(tTypeTemp2))
                            {
                                tType = typeof(String);
                                break;
                            }
                            if (FonctionsType.isDate(tTypeTemp1))
                            {
                                if (FonctionsType.isInt(tTypeTemp2) || FonctionsType.isDouble(tTypeTemp2))
                                {
                                    tType = typeof(DateTime);
                                }
                                else
                                {
                                    ListErreurs.Add("Err01309 : You cannot add a date with this type of value.");
                                    return false;
                                }
                            }
                            else if (FonctionsType.isTime(tTypeTemp1))
                            {
                                if (FonctionsType.isInt(tTypeTemp2) || FonctionsType.isDouble(tTypeTemp2) || FonctionsType.isTime(tTypeTemp2))
                                {
                                    if (FonctionsType.isDouble(tTypeTemp2))
                                    {
                                        //Add a warning message
                                        ListErreurs.Add("Err01371 : Warning, Adding Time format with double has limitation, the Double value will be truncated.");
                                    }
                                    tType = typeof(TimeSpan);
                                }
                                else
                                {
                                    ListErreurs.Add("Err01310 : You cannot add a time with this type of value.");
                                    return false;
                                }
                            }
                            else if (FonctionsType.isInt(tTypeTemp1) || FonctionsType.isDouble(tTypeTemp1))
                            {
                                if (FonctionsType.isDate(tTypeTemp2))
                                {
                                    branche3 = branche1;
                                    branche1 = branche2;
                                    branche2 = branche3;
                                    branche3 = null;
                                    tType = typeof(DateTime);
                                }
                                else if (FonctionsType.isTime(tTypeTemp2))
                                {
                                    branche3 = branche1;
                                    branche1 = branche2;
                                    branche2 = branche3;
                                    branche3 = null;
                                    tType = typeof(TimeSpan);
                                }
                                else if (FonctionsType.isInt(tTypeTemp2))
                                {
                                    if (FonctionsType.isInt(tTypeTemp1))
                                    {
                                        tType = typeof(Int32);
                                    }
                                    else
                                    {
                                        tType = typeof(Double);
                                    }
                                }
                                else if (FonctionsType.isDouble(tTypeTemp2))
                                {
                                    tType = typeof(Double);
                                }
                                else
                                {
                                    ListErreurs.Add("Err01347 : Cannot add this type of operande.");
                                    return false;
                                }
                            }
                            break;
                        case "-":
                            if (FonctionsType.isString(tTypeTemp1) || FonctionsType.isString(tTypeTemp2))
                            {
                                ListErreurs.Add("Err01311 : You cannot use substract operation on strings.");
                                return false;
                            }

                            if ((FonctionsType.isDate(tTypeTemp1)) && (FonctionsType.isDate(tTypeTemp2)))
                            {
                                tType = typeof(Int32);
                            }
                            else if (FonctionsType.isTime(tTypeTemp1) && FonctionsType.isTime(tTypeTemp2))
                            {
                                tType = typeof(Int32);
                            }
                            else if ((FonctionsType.isInt(tTypeTemp1) || FonctionsType.isDouble(tTypeTemp1))
                               && (FonctionsType.isInt(tTypeTemp2) || FonctionsType.isDouble(tTypeTemp2)))
                            {
                                if (FonctionsType.isInt(tTypeTemp1) && FonctionsType.isInt(tTypeTemp2))
                                    tType = tTypeTemp1;
                                else
                                    tType = typeof(Double);
                            }
                            else
                            {
                                ListErreurs.Add("Err01312 : You cannot substract this type of datas.");
                                return false;
                            }
                            break;
                        case "/":       //<< Task calculation of division in Filters
                            if ((FonctionsType.isInt(tTypeTemp1) || FonctionsType.isDouble(tTypeTemp1))
                                    && (FonctionsType.isInt(tTypeTemp2) || FonctionsType.isDouble(tTypeTemp2)))
                            {
                                tType = typeof(Double);
                            }
                            else
                            {
                                ListErreurs.Add("Err01313 : The '" + operateur + "' operation cannot be applied on date, time or string values.");
                                return false;
                            }
                            break;      //>> Task calculation of division in Filters
                        case "*":
                            if ((FonctionsType.isInt(tTypeTemp1) || FonctionsType.isDouble(tTypeTemp1))
                                && (FonctionsType.isInt(tTypeTemp2) || FonctionsType.isDouble(tTypeTemp2)))
                            {
                                tType = tTypeTemp1;
                            }
                            else
                            {
                                ListErreurs.Add("Err01313 : The '" + operateur + "' operation cannot be applied on date, time or string values.");
                                return false;
                            }
                            break;
                        case "and":
                        case "or":
                        case "xor":
                            if ((!FonctionsType.isBoolean(tTypeTemp1)) || (!FonctionsType.isBoolean(tTypeTemp2)))
                            {

                                ListErreurs.Add("Err01314 : The '" + operateur + "' operation has to be applied on boolean values.");
                                return false;
                            }
                            tType = typeof(Boolean);
                            break;
                        case "contains":
                            tType = typeof(Boolean);
                            break;
                        case "modulo":
                            if ((FonctionsType.isInt(tTypeTemp1) || FonctionsType.isDouble(tTypeTemp1))
                                && (FonctionsType.isInt(tTypeTemp2) || FonctionsType.isDouble(tTypeTemp2)))
                            {
                                tType = typeof(Int32);
                            }
                            else
                            {
                                ListErreurs.Add("Err01367 : The 'Modulo(' function accept type double or integer.");
                                return false;
                            }
                            if (FonctionsType.isDouble(tTypeTemp1))
                                ListErreurs.Add("Warn01368 : Warning, you are using the 'Modulo(' function with a double value, this value will be truncated to the smalelst integer.");

                            break;
                        case "value":
                            if (!FonctionsType.isInt(tTypeTemp2))
                            {
                                ListErreurs.Add("Err01299 : The 'Value(' function accept type integer as a second parameter."
                                    +" This parameter indicates the row number from which the data will be obtained.");
                                return false;
                            }
                            if((typeof(ExpressionLeaf).IsInstanceOfType(branche1))&&
                                (!((ExpressionLeaf)branche1).verifierValidColumn(laTable)))
                            {
                                ListErreurs.Add("Err01298 : The 'Value(' function accept type a column name as first parameter.");
                                return false;
                            }
                            tType = tTypeTemp1;
                            break;
                        case "left":
                        case "right":
                            if ((FonctionsType.isString(tTypeTemp1))
                                && (FonctionsType.isInt(tTypeTemp2) || FonctionsType.isDouble(tTypeTemp2)))
                            {
                                tType = typeof(String);
                            }
                            else
                            {
                                ListErreurs.Add("Err01369 : The 'Left('/'Right(' functions arguments are not valids : The first argument should be a string, and the second one should be an integer.");
                                return false;
                            }
                            break;
                        case GlobalNames.DATETIME_TO_DHMS_TO_LOWER:
                            {
                                if ((FonctionsType.isDate(tTypeTemp1) || FonctionsType.isTime(tTypeTemp1))
                                    && FonctionsType.isString(tTypeTemp2))
                                {
                                    tType = typeof(Double);
                                }
                                else
                                {
                                    ListErreurs.Add("Err01369 : The arguments of the 'DateTime to DHMS' function are not valid: The first argument should be "
                                        + "a date or time. The second one indicates the the result type: D = days, H = hours, M = minutes or S = seconds.");
                                    return false;
                                }
                                break;
                            }
                        case GlobalNames.ROUND_TO_LOWER:
                            {
                                if ((FonctionsType.isInt(tTypeTemp1) || FonctionsType.isDouble(tTypeTemp1))
                                    && FonctionsType.isInt(tTypeTemp2))
                                {
                                    tType = typeof(Decimal);
                                }
                                else
                                {
                                    ListErreurs.Add("Err01369 : The arguments of the 'Round' function are not valid:"
                                        + Environment.NewLine+ "The first argument should be an integer or a double, "
                                        + Environment.NewLine + " while the second argument, which indicates to how many decimals the rounding will be applied, "
                                        + "must be an integer.");
                                    return false;
                                }
                                break;
                            }
                        default:
                            ListErreurs.Add("Err01315 : The '" + operateur + "' operation does not exist with only 2 operands.");
                            return false;
                    }
                }
                #endregion
                #region Les fonctions avec trois paramètres.
                else
                {
                    tTypeTemp2 = branche2.tType;
                    tTypeTemp3 = branche3.tType;

                    switch (operateur)
                    {
                        case "if":
                            if (!FonctionsType.isBoolean(tTypeTemp1))
                            {

                                ListErreurs.Add("Err01316 : The first value on a 'if' operation must be a boolean.");
                                return false;
                            }
                            if (tTypeTemp2 != tTypeTemp3)
                            {
                                if ((FonctionsType.isInt(tTypeTemp2) || FonctionsType.isDouble(tTypeTemp2)) &&
                                    (FonctionsType.isInt(tTypeTemp3) || FonctionsType.isDouble(tTypeTemp3)))
                                {
                                    if (FonctionsType.isInt(tTypeTemp2) && FonctionsType.isInt(tTypeTemp3))
                                        tType = typeof(Int32);
                                    else
                                        tType = typeof(Double);
                                }
                                else if (FonctionsType.isString(tTypeTemp2) || FonctionsType.isString(tTypeTemp3))
                                    tType = typeof(String);
                                else
                                {
                                    ListErreurs.Add("Err01317 : The second and third operand must be of the same type.");
                                    return false;
                                }
                            }
                            else
                                tType = tTypeTemp2;
                            break;
                        case "substring":
                            if ((FonctionsType.isString(tTypeTemp1))
                                && (FonctionsType.isInt(tTypeTemp2) || FonctionsType.isDouble(tTypeTemp2))
                                && (FonctionsType.isInt(tTypeTemp3) || FonctionsType.isDouble(tTypeTemp3)))
                            {
                                tType = typeof(String);
                            }
                            else
                            {
                                ListErreurs.Add("Err01370 : The 'SubString(' functions arguments are not valid:"
                                    + Environment.NewLine + "The first parameter must be a string, the second and third must be integers."
                                    + Environment.NewLine + "The result is a substring with the starting index indicated by the second parameter"
                                    + Environment.NewLine + " and the length indicated by the third parameter."
                                    + Environment.NewLine);
                                return false;
                            }
                            break;
                        default:
                            ListErreurs.Add("Err01352 : The '" + operateur + "' operation does not exist with 3 operands.");
                            return false;
                    }
                }
                #endregion
                return true;

            }
            #endregion

            #region public override Object AppliquerExpression(DataRow ligne)
            public override Object AppliquerExpression(DataRow ligne)
            {
                object oBranche1, oBranche2 = null, oBranche3 = null;
                if(operateur == "value")
                {
                    //On récupère la valeur pour la branche 2
                    oBranche2 = branche2.AppliquerExpression(ligne);
                    return branche1.getValue(ligne.Table,FonctionsType.getInt(oBranche2, branche2.tType));
                }
                oBranche1 = branche1.AppliquerExpression(ligne);
                if (oBranche1 == null)
                {
                    return null;
                }
                if (branche2 != null)
                {
                    oBranche2 = branche2.AppliquerExpression(ligne);
                    if (oBranche2 == null)
                    {
                        return null;
                    }
                }
                if (branche3 != null)
                {
                    oBranche3 = branche3.AppliquerExpression(ligne);
                    if (oBranche3 == null)
                    {
                        return null;
                    }
                }
                Double dBranche2;
                String sResultat;
                switch (operateur)
                {
                    case "<":
                        #region operation <
                        if (FonctionsType.isDate(branche1.tType))
                        {
                            return FonctionsType.getDate(oBranche1, branche1.tType) < FonctionsType.getDate(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isTime(branche1.tType))
                        {
                            return FonctionsType.getTime(oBranche1, branche1.tType) < FonctionsType.getTime(oBranche2, branche2.tType);
                        }
                        else if ((FonctionsType.isDouble(branche1.tType)) || (FonctionsType.isDouble(branche2.tType)) ||
                                (FonctionsType.isString(branche1.tType)) || (FonctionsType.isString(branche2.tType)))
                        {
                            return FonctionsType.getDouble(oBranche1, branche1.tType) < FonctionsType.getDouble(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isInt(branche1.tType))
                        {
                            return FonctionsType.getInt(oBranche1, branche1.tType) < FonctionsType.getInt(oBranche2, branche2.tType);
                        }
                        else
                        {

                            ListErreurs.Add("Err01318 : Impossible to compare this type of datas.");
                            return null;
                        }
                        #endregion
                    case ">":
                        #region operation >
                        if (FonctionsType.isDate(branche1.tType))
                        {
                            return FonctionsType.getDate(oBranche1, branche1.tType) > FonctionsType.getDate(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isTime(branche1.tType))
                        {
                            return FonctionsType.getTime(oBranche1, branche1.tType) > FonctionsType.getTime(oBranche2, branche2.tType);
                        }
                        else if ((FonctionsType.isDouble(branche1.tType)) || (FonctionsType.isDouble(branche2.tType)) ||
                           (FonctionsType.isString(branche1.tType)) || (FonctionsType.isString(branche2.tType)))
                        {
                            return FonctionsType.getDouble(oBranche1, branche1.tType) > FonctionsType.getDouble(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isInt(branche1.tType))
                        {
                            return FonctionsType.getInt(oBranche1, branche1.tType) > FonctionsType.getInt(oBranche2, branche2.tType);
                        }
                        else
                        {
                            ListErreurs.Add("Err01319 : Impossible to compare this type of datas.");
                            return null;
                        }
                        #endregion
                    case "<=":
                        #region operation <=
                        if (FonctionsType.isDate(branche1.tType))
                        {
                            return FonctionsType.getDate(oBranche1, branche1.tType) <= FonctionsType.getDate(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isTime(branche1.tType))
                        {
                            return FonctionsType.getTime(oBranche1, branche1.tType) <= FonctionsType.getTime(oBranche2, branche2.tType);
                        }
                        else if ((FonctionsType.isDouble(branche1.tType)) || (FonctionsType.isDouble(branche2.tType)) ||
                           (FonctionsType.isString(branche1.tType)) || (FonctionsType.isString(branche2.tType)))
                        {
                            return FonctionsType.getDouble(oBranche1, branche1.tType) <= FonctionsType.getDouble(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isInt(branche1.tType))
                        {
                            return FonctionsType.getInt(oBranche1, branche1.tType) <= FonctionsType.getInt(oBranche2, branche2.tType);
                        }
                        else
                        {
                            ListErreurs.Add("Err01320 : Impossible to compare this type of datas.");
                            return null;
                        }
                        #endregion
                    case ">=":
                        #region operation >=
                        if (FonctionsType.isDate(branche1.tType))
                        {
                            return FonctionsType.getDate(oBranche1, branche1.tType) >= FonctionsType.getDate(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isTime(branche1.tType))
                        {
                            return FonctionsType.getTime(oBranche1, branche1.tType) >= FonctionsType.getTime(oBranche2, branche2.tType);
                        }
                        else if ((FonctionsType.isDouble(branche1.tType)) || (FonctionsType.isDouble(branche2.tType)) ||
                           (FonctionsType.isString(branche1.tType)) || (FonctionsType.isString(branche2.tType)))
                        {
                            return FonctionsType.getDouble(oBranche1, branche1.tType) >= FonctionsType.getDouble(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isInt(branche1.tType))
                        {
                            return FonctionsType.getInt(oBranche1, branche1.tType) >= FonctionsType.getInt(oBranche2, branche2.tType);
                        }
                        else
                        {
                            ListErreurs.Add("Err01321 : Impossible to compare this type of datas.");
                            return null;
                        }
                        #endregion
                    case "=":
                        #region operation =
                        if (FonctionsType.isDate(branche1.tType))
                        {
                            return FonctionsType.getDate(oBranche1, branche1.tType) == FonctionsType.getDate(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isTime(branche1.tType))
                        {
                            return FonctionsType.getTime(oBranche1, branche1.tType) == FonctionsType.getTime(oBranche2, branche2.tType);
                        }
                        else if ((FonctionsType.isDouble(branche1.tType)) || (FonctionsType.isDouble(branche2.tType)))
                        {
                            return FonctionsType.getDouble(oBranche1, branche1.tType) == FonctionsType.getDouble(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isInt(branche1.tType))
                        {
                            return FonctionsType.getInt(oBranche1, branche1.tType) == FonctionsType.getInt(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isString(branche1.tType))
                        {
                            return FonctionsType.getString(oBranche1, branche1.tType) == FonctionsType.getString(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isBoolean(branche1.tType))
                        {
                            return FonctionsType.getBoolean(oBranche1, branche1.tType) == FonctionsType.getBoolean(oBranche2, branche2.tType);
                        }
                        else
                        {
                            ListErreurs.Add("Err01322 : Impossible to compare this type of datas.");
                            return null;
                        }
                        #endregion
                    case "<>":
                        #region operation <>

                        if (FonctionsType.isDate(branche1.tType))
                        {
                            return FonctionsType.getDate(oBranche1, branche1.tType) != FonctionsType.getDate(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isTime(branche1.tType))
                        {
                            return FonctionsType.getTime(oBranche1, branche1.tType) != FonctionsType.getTime(oBranche2, branche2.tType);
                        }
                        else if ((FonctionsType.isDouble(branche1.tType)) || (FonctionsType.isDouble(branche2.tType)))
                        {
                            return FonctionsType.getDouble(oBranche1, branche1.tType) != FonctionsType.getDouble(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isInt(branche1.tType))
                        {
                            return FonctionsType.getInt(oBranche1, branche1.tType) != FonctionsType.getInt(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isString(branche1.tType))
                        {
                            return FonctionsType.getString(oBranche1, branche1.tType) != FonctionsType.getString(oBranche2, branche2.tType);
                        }
                        else
                        {
                            ListErreurs.Add("Err01323 : Impossible to compare this type of datas.");
                            return null;
                        }
                        #endregion
                    case "and":
                        #region operation and
                        return FonctionsType.getBoolean(oBranche1, branche1.tType) && FonctionsType.getBoolean(oBranche2, branche2.tType);
                        #endregion
                    case "or":
                        #region operation or
                        return FonctionsType.getBoolean(oBranche1, branche1.tType) || FonctionsType.getBoolean(oBranche2, branche2.tType);
                        #endregion
                    case "not":
                        #region operation not
                        return !FonctionsType.getBoolean(oBranche1, branche1.tType);
                        #endregion
                    case "xor":
                        #region operation xor
                        bool val1 = FonctionsType.getBoolean(oBranche1, branche1.tType);
                        bool val2 = FonctionsType.getBoolean(oBranche2, branche2.tType);
                        return ((val1 && (!val2)) || ((!val1) && val2));
                        #endregion
                    case "/":
                        #region operation /
                        dBranche2 = FonctionsType.getDouble(oBranche2, branche2.tType); ;

                        if (dBranche2 == 0)
                        {
                            ListErreurs.Add("Err01324 : Division y 0.");
                            return null;
                        }   //<< Task calculation of division in Filters
                        //if (FonctionsType.isDouble(branche1.tType))
                        //{
                        return FonctionsType.getDouble(oBranche1, branche1.tType) / dBranche2;
                        //}
                        //else
                        //{
                        //    return (int)(FonctionsType.getDouble(oBranche1, branche1.tType) / dBranche2);
                        //}     //>> Task calculation of division in Filters
                        #endregion
                    case "*":
                        #region operation *
                        if (FonctionsType.isInt(tType))
                            return (int)FonctionsType.getDouble(oBranche1, branche1.tType) * FonctionsType.getDouble(oBranche2, branche2.tType);
                        return FonctionsType.getDouble(oBranche1, branche1.tType) * FonctionsType.getDouble(oBranche2, branche2.tType);
                        #endregion
                    case "todate":  // >> Task #16728 PAX2SIM Improvements (Recurring) C#25
                    case "stringtofulldate":
                        #region operation todate
                        DateTime dtResultat;
                        sResultat = oBranche1.ToString();
                        if (!DateTime.TryParse(sResultat, out dtResultat))
                        {
                            ListErreurs.Add("Err01325 : '" + sResultat + "' is not a valid format for a date.");
                            return null;
                        }
                        return dtResultat;
                        #endregion
                    case "stringtotime":
                        #region operation totime
                        TimeSpan tsResultat;
                        sResultat = oBranche1.ToString();
                        if (!TimeSpan.TryParse(sResultat, out tsResultat))
                        {
                            ListErreurs.Add("Err01326 : '" + sResultat + "' is not a valid format for a time. The format should be 'hh:mm:ss'.");
                            return null;
                        }
                        return tsResultat;
                        #endregion                    
                    case "fulldatetostring":
                        #region operation FullDateToString
                        DateTime dt = (DateTime)oBranche1;
                        return dt.ToString();
                        #endregion

                    case "fulldatetotime":
                        #region operation FullDateToTime
                        if (oBranche1.GetType() == typeof(DateTime))
                            dt = (DateTime)oBranche1;
                        else
                        {
                            String str = oBranche1.ToString();
                            if (!DateTime.TryParse(str, out dt))
                            {
                                ListErreurs.Add("Err0613 : 'in function FullDateToTime, " + oBranche1.ToString() + "' is not a valid format for a DateTime.");
                                return null;
                            }
                        }
                        return dt.ToLongTimeString();
                        #endregion

                    case "tointeger":
                        #region operation tointeger
                        return FonctionsType.getInt(oBranche1, branche1.tType);
                        #endregion

                    case "toreal":
                        #region operation toreal
                        return FonctionsType.getDouble(oBranche1, branche1.tType);
                        #endregion
                    case "tostring":
                        #region operation tostring
                        return FonctionsType.getString(oBranche1, branche1.tType);
                        #endregion
                    case "-":
                        #region operation -
                        if (branche2 == null)
                        {
                            if (FonctionsType.isInt(branche1.tType))
                                return -FonctionsType.getInt(oBranche1, branche1.tType);
                            return -FonctionsType.getDouble(oBranche1, branche1.tType);
                        }
                        else if (FonctionsType.isDate(branche1.tType) && FonctionsType.isDate(branche2.tType))
                        {
                            return FonctionsType.getDate(oBranche1, branche1.tType) - FonctionsType.getDate(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isTime(branche1.tType) && FonctionsType.isTime(branche2.tType))
                        {
                            return FonctionsType.getTime(oBranche1, branche1.tType) - FonctionsType.getTime(oBranche2, branche2.tType);
                        }
                        else if (FonctionsType.isInt(branche1.tType) || FonctionsType.isDouble(branche1.tType))
                        {
                            if (FonctionsType.isInt(branche1.tType))
                                return FonctionsType.getInt(oBranche1, branche1.tType) - FonctionsType.getInt(oBranche2, branche2.tType);
                            return FonctionsType.getDouble(oBranche1, branche1.tType) - FonctionsType.getDouble(oBranche2, branche2.tType);
                        }
                        else
                        {
                            if (FonctionsType.isDate(branche1.tType))
                            {
                                DateTime dtBranche1 = FonctionsType.getDate(oBranche1, branche1.tType);
                                return dtBranche1.AddDays(-FonctionsType.getDouble(oBranche2, branche2.tType));
                            }
                            else if (FonctionsType.isTime(branche1.tType))
                            {
                                TimeSpan tsBranche1 = FonctionsType.getTime(oBranche1, branche1.tType);
                                return tsBranche1.Subtract(new TimeSpan(0, FonctionsType.getInt(oBranche2, branche2.tType), 0));
                            }
                            else
                            {
                                ListErreurs.Add("Err01327 : We cannot apply the substract operation between these types of datas.");
                                return null;
                            }
                        }
                        #endregion
                    case "+":
                        #region operation +
                        if (FonctionsType.isString(tType))
                        {
                            return FonctionsType.getString(oBranche1, branche1.tType) + FonctionsType.getString(oBranche2, branche2.tType);
                        }
                        if (FonctionsType.isDate(branche1.tType))
                        {
                            return FonctionsType.getDate(oBranche1, branche1.tType).AddDays(FonctionsType.getDouble(oBranche2, branche2.tType));
                        }
                        else if (branche1.tType == typeof(TimeSpan))
                        {
                            if (branche2.tType == typeof(TimeSpan))
                            {
                                return FonctionsType.getTime(oBranche1, branche1.tType).Add(FonctionsType.getTime(oBranche2, branche2.tType));
                            }
                            else
                            {
                                //FonctionsType.getTime(oBranche1, branche1.tType).
                                return FonctionsType.getTime(oBranche1, branche1.tType).Add(new TimeSpan(0, 0, FonctionsType.getInt(oBranche2, branche2.tType)));
                            }
                        }
                        else if (FonctionsType.isInt(branche1.tType) || FonctionsType.isDouble(branche1.tType))
                        {

                            if (FonctionsType.isInt(branche1.tType))
                            {
                                return FonctionsType.getInt(oBranche1, branche1.tType) + FonctionsType.getInt(oBranche2, branche2.tType);
                            }
                            else
                            {
                                return FonctionsType.getDouble(oBranche1, branche1.tType) + FonctionsType.getDouble(oBranche2, branche2.tType);
                            }
                        }
                        else
                        {
                            ListErreurs.Add("Err01328 : We cannot add a Time with this type of datas.");
                            return null;
                        }
                        #endregion
                    case "if":
                        #region operation if
                        bool condition = (bool)oBranche1;
                        if (condition)
                        {
                            return oBranche2;
                        }
                        else
                        {
                            return oBranche3;
                        }
                        #endregion
                    case "contains":
                        #region operation Contains
                        String sValue = oBranche1.ToString().ToLower();
                        String sCompare = oBranche2.ToString().ToLower();
                        if ((sCompare == null) || (sCompare == ""))
                            return false;
                        return sValue.Contains(sCompare);
                        #endregion
                    case "iseven":
                        #region operation IsEven
                        return ((FonctionsType.getInt(oBranche1, branche1.tType) % 2) == 0);
                        #endregion
                    case "isodd":
                        #region operation IsOdd
                        return ((FonctionsType.getInt(oBranche1, branche1.tType) % 2) == 1);
                        #endregion
                    case "modulo":
                        #region operation Modulo
                        return FonctionsType.getInt(oBranche1, branche1.tType) % FonctionsType.getInt(oBranche2, branche2.tType);
                        #endregion
                    case "left":
                        #region operation Left
                        int iLongueur = FonctionsType.getInt(oBranche2, branche2.tType);
                        if (iLongueur <= 0)
                            return "";
                        String sChaine = FonctionsType.getString(oBranche1, branche1.tType);
                        if(iLongueur> sChaine.Length)
                            return sChaine;
                        return sChaine.Substring(0, iLongueur);
                        #endregion
                    case "right":
                        #region operation Right
                        if (FonctionsType.getInt(oBranche2, branche2.tType) <= 0)
                            return "";
                        if (FonctionsType.getInt(oBranche2, branche2.tType) >= FonctionsType.getString(oBranche1, branche1.tType).Length)
                        {
                            return FonctionsType.getString(oBranche1, branche1.tType);
                        }
                        return FonctionsType.getString(oBranche1, branche1.tType).Substring(FonctionsType.getString(oBranche1, branche1.tType).Length - FonctionsType.getInt(oBranche2, branche2.tType));
                        #endregion
                    case "substring":
                        #region operation SubString

                        if (FonctionsType.getInt(oBranche2, branche2.tType) <= 0)
                            return "";
                        if (FonctionsType.getInt(oBranche3, branche3.tType) <= 0)
                            return "";
                        if ((FonctionsType.getInt(oBranche2, branche2.tType) >= FonctionsType.getString(oBranche1, branche1.tType).Length) ||
                            (FonctionsType.getInt(oBranche2, branche2.tType)<0))
                        {
                            return "";
                        }
                        if ((FonctionsType.getInt(oBranche2, branche2.tType) + FonctionsType.getInt(oBranche3, branche3.tType)) > FonctionsType.getString(oBranche1, branche1.tType).Length)
                        {
                            return FonctionsType.getString(oBranche1, branche1.tType).Substring(FonctionsType.getInt(oBranche2, branche2.tType));
                        }
                        return FonctionsType.getString(oBranche1, branche1.tType).Substring(FonctionsType.getInt(oBranche2, branche2.tType), FonctionsType.getInt(oBranche3, branche3.tType));
                        #endregion
                    case "length":
                        #region operation Length
                        return FonctionsType.getString(oBranche1, branche1.tType).Length;
                        #endregion
                    case "sqrt":
                        // << Task #8986 Pax2Sim - Filters - Expression functions                        
                        #region operation Square Root
                        return FonctionsType.getSQRT(oBranche1, branche1.tType);
                        #endregion
                        // >> Task #8986 Pax2Sim - Filters - Expression functions
                    case "square":
                        // << Task #8986 Pax2Sim - Filters - Expression functions                        
                        #region operation Square
                        return FonctionsType.getSquare(oBranche1, branche1.tType);
                        #endregion
                        // >> Task #8986 Pax2Sim - Filters - Expression functions
                    case GlobalNames.CEILING_FUNCTION_IDENTIFIER:
                        // >> Task #10010 Pax2Sim - Filters - Ceiling function
                        #region operation Ceiling
                        return FonctionsType.getCeiling(oBranche1, branche1.tType);
                        #endregion
                        // << Task #10010 Pax2Sim - Filters - Ceiling function
                    case GlobalNames.DATETIME_TO_DHMS_TO_LOWER:
                        {
                            if (!FonctionsType.isDate(branche1.tType))
                            {
                                ListErreurs.Add("Err1701: The first parameter must be a Date.");
                                return null;  
                            }
                            
                            if (!FonctionsType.isString(branche2.tType))
                            {
                                ListErreurs.Add("Err1702: The second parameter, indicating the result time unit, "
                                    + "must be one of the strings: d = day, h = hour, m = minutes, s = seconds. Please add using \' \" \'.");
                                return null;
                            }
                            DateTime referenceDate = new DateTime(2000, 1, 1);
                            DateTime givenDate = FonctionsType.getDate(oBranche1, branche1.tType);
                            
                            string resultQualifier = FonctionsType.getString(oBranche2, branche2.tType).ToLower();
                            if (resultQualifier.ToLower() != "d" && resultQualifier.ToLower() != "h" 
                                && resultQualifier.ToLower() != "m" && resultQualifier.ToLower() != "s")
                            {
                                ListErreurs.Add("Err1702: The second parameter, indicating the result time unit, "
                                    + "must be one of the strings: d = day, h = hour, m = minutes, s = seconds. Please add using \' \" \'.");
                                return null;
                            }

                            double result = 0;
                            if (resultQualifier == "d")
                                result =  (givenDate - referenceDate).TotalDays;
                            else if (resultQualifier == "h")
                                result = (givenDate - referenceDate).TotalHours;
                            else if (resultQualifier == "m")
                                result = (givenDate - referenceDate).TotalMinutes;
                            else if (resultQualifier == "s")
                                result = (givenDate - referenceDate).TotalSeconds;
                            return result;                                                     
                        }
                    case GlobalNames.ROUND_TO_LOWER:
                        {
                            if ((FonctionsType.isInt(branche1.tType) || FonctionsType.isDouble(branche1.tType))
                                && FonctionsType.isInt(branche2.tType))
                            {
                                double givenValue = -1;
                                int nbOfDecimals = FonctionsType.getInt(oBranche2, branche2.tType);

                                if (FonctionsType.isInt(branche1.tType))
                                    givenValue = FonctionsType.getInt(oBranche1, branche1.tType);
                                else if (FonctionsType.isDouble(branche1.tType))
                                    givenValue = FonctionsType.getDouble(oBranche1, branche1.tType);

                                return Math.Round(givenValue, nbOfDecimals);
                            }
                            return -1;
                        }
                    /*
                    case "":
                        #region operation
                        #endregion
                        break;*/
                    default:
                        break;
                }
                return null;
            }
            #endregion

            public override Object getValue(DataTable laTable, int iIndex)
            {
                return iIndex;
            }
        }
        #endregion

        #region La Class Tree qui est la représentation de la formule sous la forme d'un arbre.

        private class ExpressionTree : ExpressionNode
        {
            private ExpressionNode Racine;
            private ExpressionNode Condition;
            private Object resultatRacine;            
            DataRow ligneTestee;
            public ExpressionTree(ExpressionNode Racine_)
            {
                Racine = Racine_;
                Condition = null;
                resultatRacine = null;
                ligneTestee = null;
            }
            public ExpressionTree(ExpressionNode Racine_, ExpressionNode Condition_)
            {
                Racine = Racine_;
                Condition = Condition_;
                resultatRacine = null;
                ligneTestee = null;                                
            }
            public override bool verifierExpression(DataTable laTable)
            {
                if (Racine.verifierExpression(laTable))
                {
                    tType = Racine.tType;
                    if (Condition != null)
                    {
                        ((ExpressionBranch)Condition).changeBranch1(new ExpressionLeaf("", tType));
                        return Condition.verifierExpression(laTable);
                    }
                    return true;
                }
                return false;
            }
            public override Object AppliquerExpression(DataRow ligne)
            {
                if (ligne != ligneTestee)
                {
                    resultatRacine = Racine.AppliquerExpression(ligne);
                    ligneTestee = ligne;
                }
                return resultatRacine;//ToString(Racine, resultatRacine);
            }
            public bool testerCondition(DataRow ligne)
            {
                resultatRacine = Racine.AppliquerExpression(ligne);
                if (resultatRacine == null)
                    return false;
                ligneTestee = ligne;
                if (Condition == null)
                {
                    return true;
                }
                ExpressionLeaf Value = new ExpressionLeaf(resultatRacine, Racine.tType);
                ((ExpressionBranch)Condition).changeBranch1(Value);
                Object objResult = Condition.AppliquerExpression(ligne);
                if (objResult == null)
                    return false;
                return (bool)objResult;
            }
            public override Object getValue(DataTable laTable, int iIndex)
            {
                return null;
            }
        }
        #endregion

        #region Les fonctions qui génèrent l'arbre à partir d'une formule.
        
        #region private static ExpressionNode parseExpression(String formule, ref int iCurseur)
        private static ExpressionNode parseExpression(String formule, ref int iCurseur)
        {
            String formul = formule.Substring(iCurseur);
            int iCaractere = OverallTools.FonctionUtiles.nextCaractere(formul);
            if (iCaractere == -1)
            {
                //Il n'y avait plus de formule après.
                ListErreurs.Add("Err01329 : Found end of expression.");
                return null;
            }
            ExpressionNode branche1 = null;
            switch (formul[iCaractere])
            {
                case '[':
                    //Nous sommes dans le cas où le caractère suivant est un nom de colonne.
                    String nomColonne = OverallTools.FonctionUtiles.extraireDonnees(formul.Substring(iCaractere + 1), ']');
                    if (nomColonne == null)
                    {
                        ListErreurs.Add("Err01330 : The column name has an error.");
                        //Le nom de la colonne est erronée
                        return null;
                    }
                    iCurseur += formul.IndexOf(']') + 1;
                    ExpressionLeaf feuille = new ExpressionLeaf(nomColonne);
                    branche1 = feuille;
                    break;
                case '(':
                    iCurseur = iCurseur + iCaractere + 1;
                    branche1 = parseExpression(formule, ref iCurseur);
                    if (branche1 == null)
                    {
                        return null;
                    }
                    iCaractere = OverallTools.FonctionUtiles.nextCaractere(formule.Substring(iCurseur));
                    if (formule[iCurseur + iCaractere] == ')')
                    {
                        //On ignore la parenthese fermante et on passe à l'analyse du reste de l'expression.
                        iCurseur += iCaractere + 1;
                    }
                    else
                    {
                        ListErreurs.Add("Err01331 : Waiting for right parentheses (missing a closing parenthese).");
                        //La parenthese ouvrante ne se ferme pas.
                        return null;
                    }
                    break;
                case '"':
                    String Chaine = OverallTools.FonctionUtiles.extraireDonnees(formul.Substring(iCaractere + 1), '"');
                    if (Chaine == null)
                    {
                        ListErreurs.Add("Err01332 : Unable to find the token '\"' for the current string.");
                        //La chaine est commencée mais pas finie.
                        return null;
                    }
                    iCurseur += formul.Substring(iCaractere + 1).IndexOf('"') + 2 + iCaractere;
                    branche1 = new ExpressionLeaf(Chaine, typeof(String));
                    break;
                default:
                    {
                        String operateur = OverallTools.FonctionUtiles.nextOperateur(formul, ref iCaractere);
                        if (operateur == "")
                        {
                            //We didn't find an operator, that means that the next value is a value or an error.
                            if ((formul.Length > 0) && (formul[iCaractere] >= '0') && (formul[iCaractere] <= '9'))
                            {
                                Double dValeur;
                                int iValeur;
                                operateur = OverallTools.FonctionUtiles.nextDoubleDate(formul, ref iCaractere);
                                if (!Double.TryParse(operateur, out dValeur))
                                {
                                    ListErreurs.Add("Err01333 : Please use '\"' to define strings, time or date.");
                                    return null;
                                }

                                iCurseur += iCaractere;
                                if (!Int32.TryParse(operateur, out iValeur))
                                {
                                    branche1 = new ExpressionLeaf(dValeur, typeof(Double));
                                }
                                else
                                {
                                    branche1 = new ExpressionLeaf(iValeur, typeof(Int32));
                                }
                            }
                            else
                            {
                                //Le premier caractère de l'expression n'est pas reconnu, ou tout simplement 
                                //il n'y a pas de caractere suivant.
                                ListErreurs.Add("Err01334 : Please use '\"' to define strings, time or date.");
                                return null;
                            }
                        }
                        else
                        {
                            //We found an operator here. So we have to manage the right behaviour.
                            operateur = operateur.ToLower();
                            ExpressionNode tmp = null;
                            switch (operateur)
                            {
                                case "-":
                                    #region case "-"
                                    iCurseur = iCurseur + iCaractere;
                                    branche1 = parseExpression(formule, ref iCurseur);
                                    if (branche1 != null)
                                    {
                                        branche1 = new ExpressionBranch(branche1, operateur);
                                    }
                                    else
                                    {
                                        ListErreurs.Add("Err01353 : Unvalid use of function : " + operateur);
                                        return null; // Caractère manquant.
                                    }
                                    break;
                                    #endregion
                                case "contains":
                                case "left":
                                case "right":
                                case "modulo":
                                case "value":
                                case GlobalNames.DATETIME_TO_DHMS_TO_LOWER:
                                case GlobalNames.ROUND_TO_LOWER:
                                    #region case "contains, datetimetoDHMS, Round":
                                    iCurseur = iCurseur + iCaractere;
                                    iCaractere = OverallTools.FonctionUtiles.nextCaractere(formule.Substring(iCurseur));
                                    if (iCaractere == -1)
                                    {
                                        ListErreurs.Add("Err01355 : Invalid use of function : " + operateur + ". The opening parentheses might be missing.");
                                        return null; // Caractère manquant.
                                    }
                                    if (formule[iCurseur + iCaractere] != '(')
                                    {
                                        ListErreurs.Add("Err01356 : Waiting for right parentheses for the '" + operateur + "' function");
                                        return null; // Ouverture de parenthese manquant.
                                    }
                                    iCurseur += iCaractere + 1;
                                    branche1 = parseExpression(formule, ref iCurseur);

                                    if (branche1 == null)
                                    {
                                        if (operateur == GlobalNames.DATETIME_TO_DHMS_TO_LOWER)
                                        {
                                            ListErreurs.Add("Err01357 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " - the first parameter must be a date. It can be a column having DateTime as data type."
                                                + Environment.NewLine + " - the second parameter, indicating the result time unit, "
                                                + "must be one of the strings: d = day, h = hour, m = minutes, s = seconds. Please add by using \' \" \'."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == GlobalNames.ROUND_TO_LOWER)
                                        {
                                            ListErreurs.Add("Err01357 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " - the first parameter must be a number. It can be a column having Number as data type."
                                                + Environment.NewLine + " - the second parameter must be an integer indicating on on how many decimals the rounding"
                                                + " should be applied "
                                                + Environment.NewLine);

                                        }
                                        else if (operateur == "value")
                                        {
                                            ListErreurs.Add("Err01357 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " - the first parameter must be a column"
                                                + Environment.NewLine + " - the second parameter must be an integer indicating which row belonging to the"
                                                + " specified column we should obatin the value from."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == "modulo")
                                        {
                                            ListErreurs.Add("Err01357 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " - Both parameters must be Integers or columns having Integer or Double as data type."
                                                + " Doubles are accepted but will be truncated to the integer values."
                                                + Environment.NewLine + " - The result is the remainder after the division of the first parameter by the second parameter."                                                
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == "right")
                                        {
                                            ListErreurs.Add("Err01357 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " - The first parameter must be a String."
                                                + Environment.NewLine + " - The second parameter must be an Integer indicating how many characters from "
                                                + "the String will be shown starting from the end of the String."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == "left")
                                        {
                                            ListErreurs.Add("Err01357 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " - The first parameter must be a String."
                                                + Environment.NewLine + " - The second parameter must be an Integer indicating how many characters from "
                                                + "the String will be shown starting from the first character of the String."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == "contains")
                                        {
                                            ListErreurs.Add("Err01357 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " - Both parameters must be Strings."
                                                + Environment.NewLine + " - The function returns true if the first string contains the second string and false otherwise."
                                                + Environment.NewLine);
                                        }
                                        else
                                            ListErreurs.Add("Err01357 : The  '" + operateur + "' function must be applied on valid data. The first parameter is not valid.");
                                        return null;
                                    }
                                    //On recherche la virgule de séparation
                                    iCaractere = OverallTools.FonctionUtiles.nextCaractere(formule.Substring(iCurseur));
                                    if ((formule[iCurseur + iCaractere] == ',') || (formule[iCurseur + iCaractere] == ';'))
                                        iCurseur += iCaractere + 1;
                                    else
                                    {
                                        //if (operateur == GlobalNames.DATETIME_TO_DHMS_TO_LOWER)
                                        //    ListErreurs.Add("Err01358 : Invalid syntax for the 'DateTimeToDHMS' function. Expecting ',' for separating the different arguments.");
                                        //else if (operateur == GlobalNames.ROUND_TO_LOWER)
                                        //    ListErreurs.Add("Err01358 : Invalid syntax for the 'Round' function. Expecting ',' for separating the different arguments.");
                                        //else if (operateur == "value")
                                        //    ListErreurs.Add("Err01358 : Invalid syntax for the 'Value' function. Expecting ',' for separating the different arguments.");
                                        //else
                                            ListErreurs.Add("Err01358 : Invalid syntax for the '" + operateur + "' function. Expecting ',' for separating the different arguments.");
                                        return null;
                                    }

                                    ExpressionNode branche2_Tmp = parseExpression(formule, ref iCurseur);
                                    if (branche2_Tmp == null)
                                    {
                                        if (operateur == GlobalNames.DATETIME_TO_DHMS_TO_LOWER)
                                        {
                                            ListErreurs.Add("Err1702: The second argument, indicating the result time unit, "
                                                + "must be one of the strings: d = day, h = hour, m = minutes, s = seconds. Please add by using \' \" \'.");
                                        }
                                        else if (operateur == GlobalNames.ROUND_TO_LOWER)
                                        {
                                            ListErreurs.Add("Err01359 : The second argument for the 'Round' function indicating on how many decimals "
                                            + "the rounding should be applied is not valid. It must be an integer.");
                                        }
                                        else if (operateur == "value")
                                        {
                                            ListErreurs.Add("Err01359 : The second argument for the 'Value' function should be an Integer,"
                                                + " indicating the row number from which the data will be obtained.");
                                        }
                                        else
                                            ListErreurs.Add("Err01359 : The second argument for the '" + operateur + "' function is not valid.");
                                        return null;
                                    }

                                    tmp = new ExpressionBranch(branche1, branche2_Tmp, operateur);
                                    branche1 = tmp;
                                    //On recherche la parenthese fermante
                                    iCaractere = OverallTools.FonctionUtiles.nextCaractere(formule.Substring(iCurseur));
                                    if (formule[iCurseur + iCaractere] == ')')
                                        iCurseur += iCaractere + 1;
                                    else
                                    {
                                        ListErreurs.Add("Err01360 : Waiting for right parentheses for the '" + operateur + "' function.");
                                        return null;
                                    }
                                    break;
                                    #endregion
                                case "not":
                                case "tostring":
                                case "todate":  // >> Task #16728 PAX2SIM Improvements (Recurring) C#25
                                case "stringtofulldate":
                                case "stringtotime":
                                case "tointeger":
                                case "toreal":
                                case "iseven":
                                case "isodd":                                
                                case "fulldatetostring":
                                case "fulldatetotime":
                                case "length":
                                case "sqrt":        // << Task #8986 Pax2Sim - Filters - Expression functions
                                case "square":      // << Task #8986 Pax2Sim - Filters - Expression functions
                                case GlobalNames.CEILING_FUNCTION_IDENTIFIER:     // >> Task #10010 Pax2Sim - Filters - Ceiling function
                                    #region case "Operateur unaire"
                                    iCurseur = iCurseur + iCaractere;
                                    iCaractere = OverallTools.FonctionUtiles.nextCaractere(formule.Substring(iCurseur));
                                    if (iCaractere == -1)
                                    {
                                        ListErreurs.Add("Err01335 : Invalid use of function : " + operateur + " The opening parentheses might be missing.");
                                        return null; // Caractère manquant.
                                    }
                                    if (formule[iCurseur + iCaractere] != '(')
                                    {
                                        ListErreurs.Add("Err01336 : Waiting for right parentheses for the '" + operateur + "' function");
                                        return null; // Ouverture de parenthese manquant.
                                    }
                                    iCurseur += iCaractere + 1;
                                    branche1 = parseExpression(formule, ref iCurseur);

                                    if (branche1 != null)
                                    {
                                        branche1 = new ExpressionBranch(branche1, operateur);
                                    }
                                    else
                                    {
                                        if (operateur == "stringtotime")
                                        {
                                            ListErreurs.Add("Err01337 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " The parameter must be a string with a Time format. Ex.: 'hh:mm:ss'."
                                                + Environment.NewLine + " The string format follows the general formating rules for Time."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == "stringtofulldate" || operateur == "todate")  // >> Task #16728 PAX2SIM Improvements (Recurring) C#25
                                        {
                                            ListErreurs.Add("Err01337 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " The parameter must be a string with a Date and Time format. Ex.: 'dd/MM/yyyy HH:mm:ss'."
                                                + Environment.NewLine + " The string format follows the general formating rules for Date and Time."
                                                + Environment.NewLine + " The result is a Full Date with Time value."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == "fulldatetostring")
                                        {
                                            ListErreurs.Add("Err01337 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " The parameter must be a Date and Time type value."                                                
                                                + Environment.NewLine + " The result is the string representation of the Full Date with Time value."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == "fulldatetotime")
                                        {
                                            ListErreurs.Add("Err01337 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " The parameter must be a Date and Time type value or a string that can be converted into"
                                                                      + " a Date and Time."
                                                + Environment.NewLine + " The result is the string representation of the Time value of the parameter."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == "iseven")
                                        {
                                            ListErreurs.Add("Err01337 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " The parameter must be an Integer or Double value."
                                                + Environment.NewLine + " The result is a boolean indicating if the value is an even number."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == "isodd")
                                        {
                                            ListErreurs.Add("Err01337 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " The parameter must be an Integer or Double value."
                                                + Environment.NewLine + " The result is a boolean indicating if the value is an odd number."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == "not")
                                        {
                                            ListErreurs.Add("Err01337 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " The parameter must be a boolean."
                                                + Environment.NewLine + " The result is a boolean that negates the parameter's value."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == "length")
                                        {
                                            ListErreurs.Add("Err01337 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " The parameter must be a string."
                                                + Environment.NewLine + " The result is an integer indicating how many character the string has."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == "sqrt")
                                        {
                                            ListErreurs.Add("Err01337 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " The parameter must be an Integer, Double or a String that can be parse into a Number."
                                                + Environment.NewLine + " The result is a Double value representing the Square Root of the parameter."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == "square")
                                        {
                                            ListErreurs.Add("Err01337 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " The parameter must be an Integer, Double or a String that can be parse into a Number."
                                                + Environment.NewLine + " The result is a Double representing the value obtained by raising the parameter to the power 2."
                                                + Environment.NewLine);
                                        }
                                        else if (operateur == GlobalNames.CEILING_FUNCTION_IDENTIFIER)
                                        {
                                            ListErreurs.Add("Err01337 : The  '" + operateur + "' function must be applied on valid data:"
                                                + Environment.NewLine + " The parameter must be an Integer, Double or a String that can be parse into a Number."
                                                + Environment.NewLine + " The result is a value representing the smallest integer greater than or equal to the specified"
                                                                      + " parameter."
                                                + Environment.NewLine);
                                        }
                                        else
                                        {
                                            ListErreurs.Add("Err01337 : The  '" + operateur + "' function must be applied on valid data.");
                                        }
                                        return null;
                                    }
                                    //On recherche la parenthese fermante.
                                    iCaractere = OverallTools.FonctionUtiles.nextCaractere(formule.Substring(iCurseur));
                                    if (formule[iCurseur + iCaractere] == ')')
                                    {
                                        //On ignore la parenthese fermante et on passe à l'analyse du reste de l'expression.
                                        iCurseur += iCaractere + 1;
                                    }
                                    else
                                    {
                                        ListErreurs.Add("Err01338 : Waiting for right parentheses for the '" + operateur + "' function.");
                                        //La parenthese ouvrante ne se ferme pas.
                                        return null;
                                    }
                                    break;
                                    #endregion
                                case "if":
                                case "substring":
                                    #region case "if":
                                    iCurseur = iCurseur + iCaractere;
                                    iCaractere = OverallTools.FonctionUtiles.nextCaractere(formule.Substring(iCurseur));
                                    if (iCaractere == -1)
                                    {
                                        ListErreurs.Add("Err01339 : Invalid syntax for the '" + operateur + "' function.");
                                        return null; // Caractère manquant.
                                    }
                                    if (formule[iCurseur + iCaractere] != '(')
                                    {
                                        ListErreurs.Add("Err01340 : Waiting for right parentheses for the '" + operateur + "' function.");
                                        return null; // Ouverture de parenthese manquant.
                                    }
                                    iCurseur += iCaractere + 1;
                                    branche1 = parseExpression(formule, ref iCurseur);

                                    if (branche1 == null)
                                    {
                                        ListErreurs.Add("Err01341 : Invalid boolean expression. The first parameter must be a boolean or a boolean expression."
                                            + Environment.NewLine + "The function needs 3 parameters: the first must be a boolean and the second and third must have the same type."
                                            + Environment.NewLine + "The result is the second parameter if the boolean is true or the third parameter otherwise."
                                            + Environment.NewLine);
                                        return null;
                                    }
                                    //On recherche la virgule de séparation
                                    iCaractere = OverallTools.FonctionUtiles.nextCaractere(formule.Substring(iCurseur));
                                    if ((formule[iCurseur + iCaractere] == ',') || (formule[iCurseur + iCaractere] == ';'))
                                        iCurseur += iCaractere + 1;
                                    else
                                    {
                                        ListErreurs.Add("Err01342 : Invalid syntax for the '" + operateur + "' function. Please separate the parameters by using ','.");
                                        return null;
                                    }
                                    ExpressionNode branche2 = parseExpression(formule, ref iCurseur);
                                    if (branche2 == null)
                                    {
                                        ListErreurs.Add("Err01343 : Invalid expression in the '" + operateur + "' function."
                                            + Environment.NewLine
                                            + " The second and third parameters must be of the same type.");
                                        return null;
                                    }
                                    //On recherche la virgule de séparation
                                    iCaractere = OverallTools.FonctionUtiles.nextCaractere(formule.Substring(iCurseur));
                                    if ((formule[iCurseur + iCaractere] == ',') || (formule[iCurseur + iCaractere] == ';'))
                                        iCurseur += iCaractere + 1;
                                    else
                                    {
                                        ListErreurs.Add("Err01344 : Invalid syntax for the '" + operateur + "' function. Please separate the parameters by using ','.");
                                        return null;
                                    }
                                    ExpressionNode branche3 = parseExpression(formule, ref iCurseur);
                                    if (branche3 == null)
                                    {
                                        ListErreurs.Add("Err01345 : Invalid expression in the '" + operateur + "' function."
                                            + Environment.NewLine
                                            + " The second and third parameters must be of the same type.");
                                        return null;
                                    }
                                    tmp = new ExpressionBranch(branche1, branche2, branche3, operateur);
                                    branche1 = tmp;
                                    //On recherche la parenthese fermante
                                    iCaractere = OverallTools.FonctionUtiles.nextCaractere(formule.Substring(iCurseur));
                                    if (formule[iCurseur + iCaractere] == ')')
                                        iCurseur += iCaractere + 1;
                                    else
                                    {
                                        ListErreurs.Add("Err01346 : Waiting for right parentheses for the '" + operateur + "' function.");
                                        return null;
                                    }
                                    break;
                                    #endregion
                                case "true":
                                case "false":
                                    #region case boolean
                                    Boolean value = (operateur == "true");
                                    iCurseur = iCurseur + iCaractere;
                                    branche1 = new ExpressionLeaf(value, typeof(Boolean));
                                    break;
                                    #endregion
                                case "linenum":
                                    #region case numligne
                                    iCurseur = iCurseur + iCaractere;
                                    branche1 = new ExpressionLeaf(operateur);
                                    break;
                                    #endregion
                                default:
                                    return null;
                            }

                        }
                    }
                    break;
            }
            return parseExpression(formule, ref iCurseur, branche1);
        }
        #endregion
        #region private static ExpressionNode parseExpression(String formule, ref int iCurseur, ExpressionNode branche1)
        private static ExpressionNode parseExpression(String formule, ref int iCurseur, ExpressionNode branche1)
        {
            String formul = formule.Substring(iCurseur);
            int iCaractere = OverallTools.FonctionUtiles.nextCaractere(formul);
            if (iCaractere == -1)
            {
                //Il n'y a plus de formule après.
                return branche1;
            }
            if ((formule[iCurseur + iCaractere] == ')') || (formule[iCurseur + iCaractere] == ',') || (formule[iCurseur + iCaractere] == ';'))
                return branche1;
            ExpressionNode branche2 = null;
            ExpressionBranch res = null;

            String op = OverallTools.FonctionUtiles.nextOperateur(formul, ref iCaractere);
            op = op.ToLower();
            switch (op)
            {
                case "+":
                case "-":
                case "/":
                case "*":
                case "and":
                case "or":
                case "xor":
                case ">":
                case "<":
                case ">=":
                case "<=":
                case "=":
                case "<>":
                case "contains":
                case "modulo":
                case "value":
                case "left":
                case "right":
                    iCurseur = iCurseur + iCaractere;
                    branche2 = parseExpression(formule, ref iCurseur);
                    if (branche2 != null)
                    {
                        res = new ExpressionBranch(branche1, branche2, op);
                    }
                    break;
                default:
                    break;
            }
            return res;
        }
        #endregion
        #endregion

        #endregion

        #region La liste des fonctions get et set pour la classe.

        public static ArrayList ListeErreurs
        {
            get
            {
                ArrayList tmp = (ArrayList)ListErreurs.Clone();
                ListErreurs.Clear();
                return tmp;
            }
        }

        public bool copyTable
        {
            get
            {
                return bCopyTable;
            }
        }
        public bool Blocked
        {
            get
            {
                return bBlocked;
            }
            set
            {
                bBlocked = value;
            }
        }
        internal bool InheritedVisualisationMode
        {
            get
            {
                return bInheritedVisualisationMode;
            }
            set
            {
                bInheritedVisualisationMode = value;
            }
        }
        public ArrayList OperationType
        {
            get
            {
                return listOperationType;
            }
        }
        public ArrayList Conditions
        {
            get
            {
                return listConditions;
            }
        }
        public ArrayList ColumnsNames
        {
            get
            {
                return listColumnsNames;
            }
        }
        public ArrayList Formules
        {
            get
            {
                return listFormules;
            }
        }
        public ArrayList Display
        {
            get
            {
                return listDisplay;
            }
        }
        public String Name
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
        public String MotherTableName
        {
            get
            {
                return sMotherTableName;
            }
            set
            {
                sMotherTableName = value;
            }
        }
        #endregion

        #region Fonctions statiques de la classe.
        /// <summary>
        /// Fonction qui vérifie que la syntaxe de la formule est valide.
        /// </summary>
        /// <param name="formule">La chaine de caractère représentant la formule</param>
        /// <param name="condition">La condition qui doit être respecté par chaque ligne pour être garder dans le filtre</param>
        /// <param name="table">La table sur laquelle on veut appliquer le filtre.</param>
        /// <returns>Booléen indiquant si la formule est valide ou non.</returns>
        public static bool estFormuleValide(String formule, String condition, DataTable table)
        {
            int iCurseur = 0;
            ExpressionNode racine = parseExpression(formule, ref iCurseur);
            iCurseur = 0;
            ExpressionNode enCondition = parseExpression(condition, ref iCurseur, null);
            if ((racine == null) || ((condition.Length != 0) && (enCondition == null)))
            {
                return false;
            }

            ExpressionTree Arbre = new ExpressionTree(racine, enCondition);
            //Une fois que l'arbre est défini il faut vérifier ses branches.
            return Arbre.verifierExpression(table);
        }

        // << Bug #9439 Pax2Sim - Simcore version bug - order of operations
        private static List<String> simpleOperatorsList = new List<String> { "+", "-", "/", "*" };
        private static List<String> digitSeparatorList = new List<String> { "." };
        private static List<String> paranthesesList = new List<String> { "(", ")" };
        private static List<String> functionsList = new List<String> { "square", "sqrt", "toreal"
            , GlobalNames.CEILING_FUNCTION_IDENTIFIER }; // >> Task #10010 Pax2Sim - Filters - Ceiling function

        public static Object evaluateSimpleExpression(String expression, DataRow dataRow)
        {
            String expresionWithRealValues = "";
            int i = 0;
            bool isParsed = true;

            for (; i < expression.Length; )
            {
                String currentCharacter = expression[i].ToString();

                if (currentCharacter.Contains(" "))
                {
                    i++;
                    continue;
                }

                if (currentCharacter.Equals("["))
                {
                    //the term is a column name: we need to get its value
                    String columnName = OverallTools.FonctionUtiles.extraireDonnees(expression.Substring(i + 1), ']');
                    if (columnName != null && dataRow[columnName] != null)
                    {                        
                        double value = 0;
                        if (Double.TryParse(dataRow[columnName].ToString(), out value))
                        {
                            expresionWithRealValues += dataRow[columnName];                            
                        }
                        else
                            isParsed = false;
                    }
                    i += columnName.Length + 2;
                    if (i >= expression.Length)
                        break;
                }
                else if (simpleOperatorsList.Contains(currentCharacter)
                         || paranthesesList.Contains(currentCharacter)
                         || digitSeparatorList.Contains(currentCharacter))
                {
                    //operator, parantheses or digit
                    expresionWithRealValues += currentCharacter;
                    i++;
                    if (i >= expression.Length)
                        break;
                }
                else if (currentCharacter.Equals("]"))
                {
                    i++;
                    if (i >= expression.Length)
                        break;
                }
                else
                {
                    String expressionTerm = "";
                    int j = 0;
                    for (j = i; j < expression.Length; j++)
                    {                        
                        if (!simpleOperatorsList.Contains(expression[j].ToString())
                            && !paranthesesList.Contains(expression[j].ToString()))
                        {
                            expressionTerm += expression[j];
                        }
                        else
                            break;                    
                    }
                    
                    if (functionsList.Contains(expressionTerm.ToLower()))
                    {
                        //the term is a function: square(term), sqrt(term)
                        int endOfFunctionIndex = 0;
                        bool retrievedOk = true;
                        string functionTerm = getFunctionStringTermFromExpression(expression, dataRow, j + 1,
                                                                                  out endOfFunctionIndex, out retrievedOk);
                        double termValue = 0;
                        if (retrievedOk && getValueFromStringTerm(functionTerm, out termValue))
                        {
                            if (expressionTerm.ToLower().Equals("square"))
                            {
                                double functionValue = termValue * termValue;
                                expresionWithRealValues += functionValue;
                            }
                            else if (expressionTerm.ToLower().Equals("sqrt"))
                            {
                                double functionValue = Math.Sqrt(termValue);
                                expresionWithRealValues += functionValue;
                            }
                            else if (expressionTerm.ToLower().Equals("toreal"))
                            {                                
                                expresionWithRealValues += termValue;
                            }
                            else if (expressionTerm.ToLower().Equals(GlobalNames.CEILING_FUNCTION_IDENTIFIER))       // >> Task #10010 Pax2Sim - Filters - Ceiling function
                            {                                
                                double functionValue = Math.Ceiling(termValue);
                                expresionWithRealValues += functionValue;
                            }
                        }
                        else
                        {
                            isParsed = false;
                            break;
                        }
                        //jump in the expression at the end of the function parsed 
                        i = endOfFunctionIndex;
                    }
                    else
                    {                       
                        double value = 0;
                        if (Double.TryParse(expressionTerm, out value))
                        {
                            //the term is a number as string
                            expresionWithRealValues += value;
                            i += expressionTerm.Length;
                            if (i >= expression.Length)
                                break;
                        }
                        else
                        {
                            //the term is something else => the expression will not be evaluated this way
                            isParsed = false;
                            i += expressionTerm.Length;
                            if (i >= expression.Length)
                                break;
                        }
                    }
                }
            }

            Object simpleExpressionResult = null;
            if (isParsed)
            {                
                try
                {
                    var engine = VsaEngine.CreateEngine();
                    simpleExpressionResult = Eval.JScriptEvaluate(expresionWithRealValues, engine);
                }
                catch (Exception e)
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while trying to evaluate an expression ("
                        + expression.ToString() + ") . "
                        + e.Message);
                }
            }
            return simpleExpressionResult;
        }

        private static string getFunctionStringTermFromExpression(String expression, DataRow dataRow, int functionTermStartIndex,
            out int endOfFunctionIndex, out bool retrievedOk)
        {
            string functionTerm = "";
            int k = 0;
            int nbParanthesesToMatch = 0;
            retrievedOk = true;

            for (k = functionTermStartIndex; k < expression.Length; )
            {
                string currentCharacter = expression[k].ToString();

                if (currentCharacter.Equals(" "))
                {
                    k++;
                    continue;
                }

                if (currentCharacter.Equals(")"))
                {
                    nbParanthesesToMatch--;
                    if (nbParanthesesToMatch == -1)
                    {
                        k++;
                        break;
                    }
                    else
                    {
                        functionTerm += currentCharacter;
                        k++;
                    }
                }
                else if (currentCharacter.Equals("("))
                {
                    nbParanthesesToMatch++;
                    functionTerm += currentCharacter;
                    k++;
                }
                else if (currentCharacter.Equals("["))
                {
                    //the term is a column name: we need to get its value
                    String columnName = OverallTools.FonctionUtiles.extraireDonnees(expression.Substring(k + 1), ']');
                    if (columnName != null && dataRow[columnName] != null)
                    {
                        double value = 0;
                        if (Double.TryParse(dataRow[columnName].ToString(), out value))
                        {
                            functionTerm += dataRow[columnName];
                        }
                    }
                    k += columnName.Length + 2;
                    if (k >= expression.Length)
                        break;
                }
                else if (currentCharacter.Equals("]"))
                {
                    k++;
                    if (k >= expression.Length)
                        break;
                }
                else if (simpleOperatorsList.Contains(currentCharacter)
                         || digitSeparatorList.Contains(currentCharacter))
                {
                    //operator or digit
                    functionTerm += currentCharacter;
                    k++;
                    if (k >= expression.Length)
                        break;
                }
                else
                {
                    double value = 0;
                    if (Double.TryParse(currentCharacter, out value))
                    {
                        //the term is a number as string
                        functionTerm += value;
                        k += currentCharacter.Length;
                        if (k >= expression.Length)
                            break;
                    }
                    else
                    {
                        //the term is something else => the expression will not be evaluated this way  
                        retrievedOk = false;
                        break;
                        //k++;
                        //if (k >= expression.Length)
                        //    break;
                    }
                }

            }
            endOfFunctionIndex = k;
            return functionTerm;
        }

        private static bool getValueFromStringTerm(String term, out double termValue)
        {
            bool ok = true;
            Object simpleExpressionResult = null;
            try
            {
                var engine = VsaEngine.CreateEngine();
                simpleExpressionResult = Eval.JScriptEvaluate(term, engine);
                if (simpleExpressionResult != null)
                {
                    if (!Double.TryParse(simpleExpressionResult.ToString(), out termValue))
                        return false;
                }
                else
                {
                    termValue = -1;
                    return false;
                }
            }
            catch (Exception e)
            {
                OverallTools.ExternFunctions.PrintLogFile("Error while trying to evaluate an expression ("
                    + term.ToString() + ") . "
                    + e.Message);
                termValue = -1;
                return false;
            }
            return ok;
        }
        // >> Bug #9439 Pax2Sim - Simcore version bug - order of operations
        #endregion

        #region Les différentes fonction utiles pour un filtre.

        internal bool AddColumn(String ColumnName, String Formule, bool Display, String Condition, String OperationType)
        {
            return InsertColumn(-1, ColumnName, Formule, Display, Condition, OperationType);
        }
        internal bool InsertColumn(String ColumnToMove, String ColumnName, String Formule, bool Display, String Condition, String OperationType)
        {
            return InsertColumn(listColumnsNames.IndexOf(ColumnToMove), ColumnName, Formule, Display, Condition, OperationType);
        }
        internal bool InsertColumn(int Position, String ColumnName, String Formule, bool Display, String Condition, String OperationType)
        {
            if ((Position == -1) || (Position > listColumnsNames.Count))
            {
                Position = listColumnsNames.Count;
            }
            listColumnsNames.Insert(Position, ColumnName);
            listFormules.Insert(Position, Formule);
            listDisplay.Insert(Position, Display);
            listConditions.Insert(Position, Condition);
            listOperationType.Insert(Position, OperationType);
            //listFormulesTree.Insert(Position, null);
            return true;
        }
        internal void ConvertColumnsNames(String sOldName, String sNewName)
        {
            ConvertColumnsNames(sOldName, sNewName, null);
        }
        internal void ConvertColumnsNames(String sOldName, String sNewName, ArrayList alIgnoredColumns)
        {
            String sOldName_ = "[" + sOldName + "]";
            String sNewName_ = "[" + sNewName + "]";
            for (int i = 0; i < listFormules.Count; i++)
            {
                if ((alIgnoredColumns != null) && (alIgnoredColumns.Contains(listColumnsNames[i])))
                    continue;
                if (listColumnsNames[i].ToString() == sOldName)
                    listColumnsNames[i] = sNewName;
                listFormules[i] = ((string)listFormules[i]).Replace(sOldName_, sNewName_);
                listConditions[i] = ((string)listConditions[i]).Replace(sOldName_, sNewName_);
            }
        }
        #region Fonction pour déterminer si le filtre peut être modifié ou non.
        /// <summary>
        /// Fonction qui indique si le filtre accepte l'ajout de données ou non.
        /// </summary>
        /// <param name="table">Table suivant laquelle on souhaite tester le filtre</param>
        /// <param name="affect">Definie si la variable \ref bBlocked doit etre affecté</param>
        /// <returns>Renvoie un booléen indiquant si le filtre peut ou non servir à ajouter des données.</returns>
        public bool isBlocked(DataTable table, Filter parent, bool affect)
        {
            if (parent != null)
            {
                if ((!parent.copyTable) /*&& (parent.Blocked)*/)
                {
                    if (affect) bBlocked = true;
                    return true;
                }
            }
            foreach (String operation in listOperationType)
            {
                if (operation != ListOperation[0])//Une opération est différente de l'opération de regroupement.
                {
                    if (affect) bBlocked = true;
                    return true;
                }
            }
            int[] indexAffichees;
            int nombreAffichees = NombreAffichees(out indexAffichees);
            for (int i = 0; i < indexAffichees.Length; i++)
            {
                if (indexAffichees[i] != -1)
                {
                    if (table.Columns.IndexOf(listColumnsNames[indexAffichees[i]].ToString()) == -1)
                    {
                        if (affect) bBlocked = true;
                        return true;
                    }
                    if (listFormules[indexAffichees[i]].ToString() != "[" + listColumnsNames[indexAffichees[i]].ToString() + "]")
                    {
                        if (affect) bBlocked = true;
                        return true;
                    }
                }
            }
            if ((table.PrimaryKey != null) && (table.PrimaryKey.Length != 0))
            {
                foreach (DataColumn PrimaryKey in table.PrimaryKey)
                {
                    bool find = false;
                    for (int i = 0; i < indexAffichees.Length; i++)
                    {
                        if (indexAffichees[i] != -1)
                        {
                            if (PrimaryKey.ColumnName == listColumnsNames[indexAffichees[i]].ToString())
                            {
                                find = true;
                                break;
                            }
                        }
                    }
                    if (!find)
                    {
                        if (affect) bBlocked = true;
                        return true;
                    }
                }
            }
            if (affect) bBlocked = false;
            return false;
        }
        #endregion

        #region Fonction pour parser le filtre courant
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool parseFilter(DataTable table, Filter parent)
        {
            if (table == null)
            {
                ListeErreurs.Add("Err01350 : Unable to parse the filter. The table is not valid.");
                return false;
            }
            listFormulesTree = new List<ExpressionTree>();
            //String[,] formatTable = analyseStructureTable(table);
            int iCurseur;
            for (int i = 0; i < listColumnsNames.Count; i++)
            {
                iCurseur = 0;
                ExpressionNode branche1 = parseExpression(listFormules[i].ToString(), ref iCurseur);
                iCurseur = 0;
                ExpressionNode condition = parseExpression(listConditions[i].ToString(), ref iCurseur, null);
                if (branche1 == null)
                {
                    listFormulesTree = null;
                    return false;
                }

                ExpressionTree Arbre = new ExpressionTree(branche1, condition);

                if (!Arbre.verifierExpression(table))
                {
                    listFormulesTree = null;
                    return false;
                }
                listFormulesTree.Add(Arbre);
            }
            isBlocked(table, parent, true);
            return true;
        }
        #endregion

        #region Fonction qui détermine le nombre de colonnes qui seront affichées dans la table finale.
        private int NombreAffichees(out int[] indexColonnes)
        {
            int Resultat = 0;
            indexColonnes = new int[listFormules.Count];
            for (int i = 0; i < indexColonnes.Length; i++)
            {
                indexColonnes[i] = -1;
                if ((bool)listDisplay[i])
                {
                    indexColonnes[i] = Resultat;
                    Resultat++;
                }
            }
            return Resultat;
        }
        #endregion

        #region Fonction qui applique le filtre sur la table donnée.
        //Ici pb de type et pb avec les clefs primaire.
        public DataTable applyFilter(DataTable tableEntree, Filter filtreTableEntree)
        {
            if (!parseFilter(tableEntree, filtreTableEntree))
            {
                return null;
            }
            DataTable resultats = new DataTable(Name);
            int i;
            int[] indexColonnes = new int[listFormules.Count];
            int NombreAffiches = NombreAffichees(out indexColonnes);
            if (NombreAffiches == 0)
            {
                ListeErreurs.Add("Err01351 : You have to define one or more columns to be showned in the destination table.");
                return null;
            }
            String[] PrimaryKeyNames = null;
            DataColumn[] drPrimaryKeys = null;
            if ((!bBlocked) && (tableEntree.PrimaryKey.Length != 0))
            {
                PrimaryKeyNames = new String[tableEntree.PrimaryKey.Length];
                drPrimaryKeys = new DataColumn[tableEntree.PrimaryKey.Length];
                for (i = 0; i < tableEntree.PrimaryKey.Length; i++)
                {
                    PrimaryKeyNames[i] = tableEntree.PrimaryKey[i].ColumnName;
                }
            }
            int iPrimaryKeyFound = 0;

            for (i = 0; i < indexColonnes.Length; i++)
            {
                if (indexColonnes[i] != -1)
                {
                    resultats.Columns.Add(listColumnsNames[i].ToString(), ((ExpressionTree)listFormulesTree[i]).tType);
                    if (PrimaryKeyNames != null)
                    {
                        foreach (String value in PrimaryKeyNames)
                        {
                            if (listColumnsNames[i].ToString() == value)
                            {
                                drPrimaryKeys[iPrimaryKeyFound] = resultats.Columns[resultats.Columns.Count - 1];
                                iPrimaryKeyFound++;
                                break;
                            }
                        }
                    }
                }
            }
            if ((PrimaryKeyNames != null) && (iPrimaryKeyFound == PrimaryKeyNames.Length))
            {
                resultats.PrimaryKey = drPrimaryKeys;
            }
            //String[] NouvelleLigne = new String[NombreAffiches];
            DataRow NouvelleLigne;
            object tmp;
            bool condition;
            foreach (DataRow ligne in tableEntree.Rows)
            {
                condition = true;
                NouvelleLigne = resultats.NewRow();
                for (i = 0; i < listColumnsNames.Count; i++)
                {
                    if ((bool)((ExpressionTree)listFormulesTree[i]).testerCondition(ligne))
                    {
                        if (indexColonnes[i] != -1)
                        {
                            tmp = ((ExpressionTree)listFormulesTree[i]).AppliquerExpression(ligne);
                            if (tmp == null)
                            {
                                tmp = "#Error";
                                if (resultats.Columns[indexColonnes[i]].DataType == typeof(TimeSpan))
                                    tmp = TimeSpan.MinValue;
                                else if (resultats.Columns[indexColonnes[i]].DataType == typeof(DateTime))
                                    tmp = DateTime.MinValue;
                                else if (resultats.Columns[indexColonnes[i]].DataType != typeof(String))
                                    tmp = 0;
                            }

                            // << Bug #9439 Pax2Sim - Simcore version bug - order of operations
                            
                            Object simpleExpressionResult = evaluateSimpleExpression(listFormules[i].ToString(), ligne);
                            if (simpleExpressionResult != null)
                            {
                                double value = 0;
                                if (Double.TryParse(simpleExpressionResult.ToString(), out value))
                                    tmp = Math.Round(value, 3);
                                else
                                    tmp = simpleExpressionResult;
                            }
                            
                            // >> Bug #9439 Pax2Sim - Simcore version bug - order of operations

                            NouvelleLigne[indexColonnes[i]] = tmp;
                        }
                    }
                    else
                    {
                        condition = false;
                        NouvelleLigne = null;
                        break;
                    }
                }
                if (!condition)
                    continue;
                resultats.Rows.Add(NouvelleLigne);
            }
            resultats.AcceptChanges();
            return resultats;
        }
        #endregion

        public System.Xml.XmlNode creerArbreXml(System.Xml.XmlDocument document)
        {

            System.Xml.XmlElement filtre = document.CreateElement("Filter");
            filtre.SetAttribute("Name", Name);
            filtre.SetAttribute("ParentTable", MotherTableName);
            filtre.SetAttribute("Copy", bCopyTable.ToString());
            filtre.SetAttribute("Locked", bBlocked.ToString());
            if (bInheritedVisualisationMode)
                filtre.SetAttribute("InheritedVisualisationMode", bInheritedVisualisationMode.ToString());

            System.Xml.XmlElement Colonnes = document.CreateElement("Columns");
            System.Xml.XmlElement Colonne;
            for (int i = 0; i < listColumnsNames.Count; i++)
            {
                Colonne = document.CreateElement("Column");
                Colonne.SetAttribute("Name", listColumnsNames[i].ToString());
                Colonne.SetAttribute("Formula", listFormules[i].ToString());
                Colonne.SetAttribute("Condition", listConditions[i].ToString());
                Colonne.SetAttribute("Display", listDisplay[i].ToString());
                Colonne.SetAttribute("OperationType", listOperationType[i].ToString());
                Colonnes.AppendChild(Colonne);
            }
            filtre.AppendChild(Colonnes);
            return filtre;
        }
        public bool isValid(DataTable table)
        {
            for (int i = 0; i < listFormules.Count; i++)
            {
                if (!estFormuleValide(listFormules[i].ToString(), listConditions[i].ToString(), table))
                {
                    return false;
                }
            }
            return true;
        }
        public override string ToString()
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            return creerArbreXml(doc).OuterXml;
        }

        public string ToString(GraphicFilter gfFilter)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlNode xnResult = creerArbreXml(doc);
            if (gfFilter != null)
                xnResult.AppendChild(gfFilter.creerArbreXml(doc));
            return xnResult.OuterXml;
        }
        #endregion
    }
}
