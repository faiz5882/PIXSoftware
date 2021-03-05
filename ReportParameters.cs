using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.Windows.Forms;
using PDFjet.NET;
using SIMCORE_TOOL.Classes;
using SIMCORE_TOOL.com.crispico.gantt;
using System.Xml.Serialization;

namespace SIMCORE_TOOL
{
    public class ReportParameters
    {
        public enum PageFormats
        {
            A3_P = 0, A3_L = 1,
            A4_P = 2, A4_L = 3,
            A5_P = 4, A5_L = 5,
            B5_P = 6, B5_L = 7,
            Lettre_P = 8, Lettre_L = 8
        };
        internal enum ColumnSizePolicy { AdjustPage, AdjustContent, AdjustDefault };
        public static String[] PageFormatsNames = { "A3 Portrait", "A3 Landscape", "A4 Portrait", "A4 Landscape", "A5 Portrait", "A5 Landscape", "B5 Portrait", "B5 Landscape", "Letter Portrait", "Letter Landscape", "Legal Portrait", "Legal Landscape" };
        public static double[][] PageSizes = { A3.PORTRAIT, A3.LANDSCAPE, A4.PORTRAIT, A4.LANDSCAPE, A5.PORTRAIT, A5.LANDSCAPE, B5.PORTRAIT, B5.LANDSCAPE, Letter.PORTRAIT, Letter.LANDSCAPE, Legal.PORTRAIT, Legal.LANDSCAPE };
        public static String defaultTemplateString = "[Default]";

        #region Parameters and accessor
        // PDF Parameters
        private Boolean GeneratePDF_ = false;
        private int PageFormat_ = -1;

        // HTML Parameters
        private Boolean GenerateHTML_ = false;

        // Selected Tree
        private System.Windows.Forms.TreeView tv_PurgedTree_;
        private System.Windows.Forms.TreeView tv_CustomTree_;

        // General Parameters getters and setters
        private String titre;
        public String Titre
        {
            get { return titre; }
            set { titre = value; }
        }
        private String Name_;
        public String Name
        {
            get { return Name_; }
            set { Name_ = value; }
        }
        private String Author_;
        public String Author
        {
            get { return Author_; }
            set { Author_ = value; }
        }
        private String version;
        public String Version
        {
            get { return version; }
            set { version = value; }
        }
        private String date;
        public String Date
        {
            get { return date; }
            set { date = value; }
        }
        private String logoUser;
        public String UserLogo
        {
            get { return logoUser; }
            set { logoUser = value; }
        }
        private String template;
        public String Template
        {
            get { return template; }
            set { template = value; }
        }
        private bool exportNote;
        public bool ExportNote
        {
            get { return exportNote; }
            set { exportNote = value; }
        }
        private Boolean IncludeOnlyChecked_ = false;
        public Boolean IncludeOnlyChecked
        {
            get { return IncludeOnlyChecked_; }
            set { IncludeOnlyChecked_ = value; }
        }

        // Pured tree getter and setter
        public System.Windows.Forms.TreeView tv_PurgedTree
        {
            get { return tv_PurgedTree_; }
            set { tv_PurgedTree_ = value; }
        }
        public System.Windows.Forms.TreeView tv_CustomTree
        {
            get { return tv_CustomTree_; }
            set { tv_CustomTree_ = value; }
        }

        // PDF Parameters getter and setter
        public Boolean GeneratePDF
        {
            get { return GeneratePDF_; }
            set { GeneratePDF_ = value; }
        }
        public int PageFormat
        {
            get { return PageFormat_; }
            set { PageFormat_ = value; }
        }
        private int dividerLevel = 0;
        public int DividerLevel
        {
            get { return dividerLevel; }
            set { dividerLevel = value; }
        }

        // HTML Parameters getter and setter
        public Boolean GenerateHTML
        {
            get { return GenerateHTML_; }
            set { GenerateHTML_ = value; }
        }

        // >> Task #17969 PAX2SIM - Reports improvements
        public bool exportSourceTextFiles { get; set; }
        public bool exportXmlConfiguration { get; set; }
        public List<string> reportGroupsList = new List<string>();
        // << Task #17969 PAX2SIM - Reports improvements

        // Version overview
        private int[] versionColumnWidth;
        public int[] VersionColumnWidth
        {
            get { return versionColumnWidth; }
            set { versionColumnWidth = value; }
        }

        private List<String[]> versionList;
        public List<String[]> VersionList
        {
            get { return versionList; }
            set { versionList = value; }
        }

        // Document approval table
        private int[] approvalColumnWidth;
        public int[] ApprovalColumnWidth
        {
            get { return approvalColumnWidth; }
            set { approvalColumnWidth = value; }
        }

        private List<String[]> approvalList;
        public List<String[]> ApprovalList
        {
            get { return approvalList; }
            set { approvalList = value; }
        }

        // Document control info
        private String projStep;
        public String ProjStep
        {
            get { return projStep; }
            set { projStep = value; }
        }
        private String partOf;
        public String PartOf
        {
            get { return partOf; }
            set { partOf = value; }
        }
        private String issuedDept;
        public String IssuedDept
        {
            get { return issuedDept; }
            set { issuedDept = value; }
        }
        private String intDocRef;
        public String IntDocRef
        {
            get { return intDocRef; }
            set { intDocRef = value; }
        }
        private String extDocRef;
        public String ExtDocRef
        {
            get { return extDocRef; }
            set { extDocRef = value; }
        }
        private String docStatus;
        public String DocStatus
        {
            get { return docStatus; }
            set { docStatus = value; }
        }
        private String appendices;
        public String Appendices
        {
            get { return appendices; }
            set { appendices = value; }
        }
        /// <summary>
        /// Liste des tables qui doivent être redimensionnées.
        /// Les tables sont referencées par une chaîne de caractere qui est 
        /// la concatenation du nom de la table, du nom du scenario et du type 
        /// d'exception si c'est une table d'exception.
        /// </summary>
        internal Dictionary<String, ColumnSizePolicy> dResizeTable;
        /// <summary>
        /// Fonction pour savoir si la table doit être redimensionnées.
        /// </summary>
        /// <param name="dataSet">Nom du scenario</param>
        /// <param name="name">Nom de la table</param>
        /// <param name="exceptionType">Type d'exception</param>
        /// <returns></returns>
        internal ColumnSizePolicy GetResizePolicy(String dataSet, String name, String exceptionType)
        {
            if (dResizeTable == null)
                return ColumnSizePolicy.AdjustDefault;
            String cle = dataSet + name + exceptionType;
            ColumnSizePolicy res;
            if(!dResizeTable.TryGetValue(cle, out res))
                res = ColumnSizePolicy.AdjustDefault;
            return res;
        }
        /// <summary>
        /// Fonction ajouter une table à la liste des tables a redimensionner.
        /// </summary>
        /// <param name="dataSet">Nom du scenario</param>
        /// <param name="name">Nom de la table</param>
        /// <param name="exceptionType">Type d'exception</param>
        internal void SetResizePolicy(String dataSet, String name, String exceptionType, ColumnSizePolicy policy)
        {
            if (dResizeTable == null)
                dResizeTable = new Dictionary<string, ColumnSizePolicy>();
            String key = dataSet + name + exceptionType;
            if (policy == ColumnSizePolicy.AdjustDefault)
                dResizeTable.Remove(key);
            else
                dResizeTable.Add(key, policy);
        }
        #endregion Parameters and accessor

        #region Import/Export xml in .pax file
        private static string ELEMENT_NAME_EXPORT_TXT_FILES = "exportSourceTextFiles";
        private static string ELEMENT_NAME_EXPORT_XML_CONFIGURATION = "exportXmlConfiguration";
        private static string ELEMENT_NAME_REPORT_GROUPS = "assignedReportGroups";
        private static string ELEMENT_REPORT_GROUPS_SEPARATOR = "_RG_";
        /// <summary>
        /// Add the report parameters in the pax file.
        /// </summary>
        /// <param name="reportParametersList">List of report parameters</param>
        /// <param name="documentSource">XmlDocument of the pax file</param>
        /// <param name="SavePath"></param>
        /// <param name="chForm"></param>
        /// <returns></returns>
        public static XmlNode ExportParameters(
                    Dictionary<string, ReportParameters> reportParametersList,
                    XmlDocument documentSource,
                    String SavePath,
                    Prompt.SIM_LoadingForm chForm) ///\todo gestrion du \ref SIM_LoadingForm
        {
            if (reportParametersList == null)
                return null;

            XmlElement reportsArbre = documentSource.CreateElement("ReportList");
            foreach (String reportName in reportParametersList.Keys)
            {
                // create ReportParameters xml element
                ReportParameters rp = reportParametersList[reportName] ;
                XmlElement reportElement = documentSource.CreateElement("ReportParameters");
                reportElement.SetAttribute("Name", reportName);
                XmlElement tmp = null;
                // add the parameters
                if (rp.ExportNote)
                    reportElement.AppendChild(documentSource.CreateElement("ExportNote"));
                if (rp.GenerateHTML)
                    reportElement.AppendChild(documentSource.CreateElement("GenerateHTML"));
                if (rp.GeneratePDF)
                    reportElement.AppendChild(documentSource.CreateElement("GeneratePDF"));
                if (rp.IncludeOnlyChecked)
                    reportElement.AppendChild(documentSource.CreateElement("IncludeOnlyChecked"));
                // >> Task #17969 PAX2SIM - Reports improvements                
                if (rp.exportSourceTextFiles)
                {
                    reportElement.AppendChild(documentSource.CreateElement(ELEMENT_NAME_EXPORT_TXT_FILES));
                }
                if (rp.exportXmlConfiguration)
                {
                    reportElement.AppendChild(documentSource.CreateElement(ELEMENT_NAME_EXPORT_XML_CONFIGURATION));
                }
                if (rp.reportGroupsList.Count > 0)
                {
                    string reportGroupNames = "";
                    foreach (string reportGroup in rp.reportGroupsList)
                    {
                        reportGroupNames += reportGroup + ELEMENT_REPORT_GROUPS_SEPARATOR;
                    }
                    tmp = documentSource.CreateElement(ELEMENT_NAME_REPORT_GROUPS);
                    tmp.SetAttribute("value", reportGroupNames);
                    reportElement.AppendChild(tmp);
                }
                // << Task #17969 PAX2SIM - Reports improvements
                if (rp.Titre != null)
                {
                    tmp = documentSource.CreateElement("Title");
                    tmp.SetAttribute("value", rp.Titre);
                    reportElement.AppendChild(tmp);
                }
                if (rp.Date != null)
                {
                    tmp = documentSource.CreateElement("Date");
                    tmp.SetAttribute("value", rp.Date);
                    reportElement.AppendChild(tmp);
                }
                if (rp.Version != null)
                {
                    tmp = documentSource.CreateElement("Version");
                    tmp.SetAttribute("value", rp.Version);
                    reportElement.AppendChild(tmp);
                }
                tmp = documentSource.CreateElement("PageFormat");
                tmp.SetAttribute("value", rp.PageFormat.ToString());
                reportElement.AppendChild(tmp);
                if (rp.DividerLevel >= 0)
                {
                    tmp = documentSource.CreateElement("DividerLevel");
                    tmp.SetAttribute("value", rp.DividerLevel.ToString());
                    reportElement.AppendChild(tmp);
                }
                if (rp.Author != null)
                {
                    tmp = documentSource.CreateElement("Autor");
                    tmp.SetAttribute("value", rp.Author);
                    reportElement.AppendChild(tmp);
                }
                if (rp.UserLogo != null)
                {
                    tmp = documentSource.CreateElement("CustomLogo");
                    tmp.SetAttribute("value", rp.UserLogo);
                    reportElement.AppendChild(tmp);
                }
                if (rp.Template != null)
                {
                    tmp = documentSource.CreateElement("Template");
                    tmp.SetAttribute("value", rp.Template);
                    reportElement.AppendChild(tmp);
                }

                // Table adjustment
                if(rp.dResizeTable != null && rp.dResizeTable.Count > 0)
                {
                    tmp = documentSource.CreateElement("TableAdjustement");
                    foreach (String key in rp.dResizeTable.Keys)
                    {

                        XmlElement tmp2 = documentSource.CreateElement("Table");
                        tmp2.SetAttribute("Name", key);
                        tmp2.SetAttribute("Type", rp.dResizeTable[key].ToString());
                        tmp.AppendChild(tmp2);
                    }
                    reportElement.AppendChild(tmp);
                }

                // Versions & Document control
                if (rp.VersionList != null)
                {
                    tmp = documentSource.CreateElement("VersionOverview");
                    XmlElement xnWidth = documentSource.CreateElement("Width");
                    for (int i = 0; i < rp.VersionColumnWidth.Length; i++)
                    {
                        int width = rp.VersionColumnWidth[i];
                        xnWidth.SetAttribute("w"+i.ToString(), rp.VersionColumnWidth[i].ToString());
                    }
                    tmp.AppendChild(xnWidth);
                    for (int i=0 ; i<rp.VersionList.Count ; i++)
                    {
                        String[] version = rp.VersionList[i];
                        XmlElement row = documentSource.CreateElement("Row");
                        foreach (String str in version)
                        {
                            XmlElement cell = documentSource.CreateElement("Cell");
                            cell.InnerText = str;
                            row.AppendChild(cell);
                        }
                        tmp.AppendChild(row);
                    }
                    reportElement.AppendChild(tmp);
                }
                if (rp.ApprovalList != null)
                {
                    tmp = documentSource.CreateElement("DocApproval");
                    XmlElement xnWidth = documentSource.CreateElement("Width");
                    for (int i = 0; i < rp.ApprovalColumnWidth.Length; i++)
                    {
                        int width = rp.ApprovalColumnWidth[i];
                        xnWidth.SetAttribute("w" + i.ToString(), rp.ApprovalColumnWidth[i].ToString());
                    }
                    tmp.AppendChild(xnWidth);
                    for (int i=0 ; i<rp.ApprovalList.Count ; i++)
                    {
                        String[] approval = rp.ApprovalList[i];
                        XmlElement row = documentSource.CreateElement("Row");
                        foreach (String str in approval)
                        {
                            XmlElement cell = documentSource.CreateElement("Cell");
                            cell.InnerText = str;
                            row.AppendChild(cell);
                        }
                        tmp.AppendChild(row);
                    }
                    reportElement.AppendChild(tmp);
                }
                if (rp.ProjStep != null)
                {
                    tmp = documentSource.CreateElement("ProjStep");
                    tmp.SetAttribute("value", rp.ProjStep);
                    reportElement.AppendChild(tmp);
                }
                if (rp.PartOf != null)
                {
                    tmp = documentSource.CreateElement("PartOf");
                    tmp.SetAttribute("value", rp.PartOf);
                    reportElement.AppendChild(tmp);
                }
                if (rp.IssuedDept != null)
                {
                    tmp = documentSource.CreateElement("IssuedDept");
                    tmp.SetAttribute("value", rp.IssuedDept);
                    reportElement.AppendChild(tmp);
                }
                if (rp.IntDocRef != null)
                {
                    tmp = documentSource.CreateElement("IntDocRef");
                    tmp.SetAttribute("value", rp.IntDocRef);
                    reportElement.AppendChild(tmp);
                }
                if (rp.ExtDocRef != null)
                {
                    tmp = documentSource.CreateElement("ExtDocRef");
                    tmp.SetAttribute("value", rp.ExtDocRef);
                    reportElement.AppendChild(tmp);
                }
                if (rp.DocStatus != null)
                {
                    tmp = documentSource.CreateElement("DocStatus");
                    tmp.SetAttribute("value", rp.DocStatus);
                    reportElement.AppendChild(tmp);
                }
                if (rp.Appendices != null)
                {
                    tmp = documentSource.CreateElement("Appendices");
                    tmp.SetAttribute("value", rp.Appendices);
                    reportElement.AppendChild(tmp);
                }

                // treeview
                /* old version (before tree editor 11/04/2011)
                if (rp.tv_PurgedTree != null)
                {
                    tmp = documentSource.CreateElement("SelectedData");
                    foreach (TreeNode tn in rp.tv_PurgedTree.Nodes)
                    {
                        XmlElement subTmp = documentSource.CreateElement("Node");
                        subTmp.SetAttribute("Name", tn.Name);
                        subTmp.SetAttribute("Checked", tn.Checked.ToString());
                        AddTreeNode(subTmp, documentSource, tn);
                        tmp.AppendChild(subTmp);
                    }
                    reportElement.AppendChild(tmp);
                }*/
                if (rp.tv_CustomTree != null && rp.tv_CustomTree.Nodes.Count > 0)
                {
                    tmp = documentSource.CreateElement("SelectedData");
                    AddTreeNode(tmp, documentSource, rp.tv_CustomTree.Nodes);
                    reportElement.AppendChild(tmp);
                }
                reportsArbre.AppendChild(reportElement);
            }
            return reportsArbre;
        }

        // >> Task #17969 PAX2SIM - Reports improvements
        internal static XmlDocument exportReportAsXmlByReportParameters(string reportName, ReportParameters reportParameters,
            Dictionary<string, GraphicFilter> graphicFilterDefinitions)
        {
            XmlDocument reportDocument = new XmlDocument();
            if (reportParameters == null)
            {
                OverallTools.ExternFunctions.PrintLogFile("Export report as xml: The report parameters are invalid.");
                return null;
            }
            XmlElement reportRootElement = reportDocument.CreateElement("ReportParameters");
            reportRootElement.SetAttribute("Name", reportName);

            XmlElement tmp = null;
            
            if (reportParameters.ExportNote)
                reportRootElement.AppendChild(reportDocument.CreateElement("ExportNote"));
            if (reportParameters.GenerateHTML)
                reportRootElement.AppendChild(reportDocument.CreateElement("GenerateHTML"));
            if (reportParameters.GeneratePDF)
                reportRootElement.AppendChild(reportDocument.CreateElement("GeneratePDF"));
            if (reportParameters.IncludeOnlyChecked)
                reportRootElement.AppendChild(reportDocument.CreateElement("IncludeOnlyChecked"));
            // >> Task #17969 PAX2SIM - Reports improvements                
            if (reportParameters.exportSourceTextFiles)
            {
                reportRootElement.AppendChild(reportDocument.CreateElement(ELEMENT_NAME_EXPORT_TXT_FILES));
            }
            if (reportParameters.exportXmlConfiguration)
            {
                reportRootElement.AppendChild(reportDocument.CreateElement(ELEMENT_NAME_EXPORT_XML_CONFIGURATION));
            }
            if (reportParameters.reportGroupsList.Count > 0)
            {
                string reportGroupNames = "";
                foreach (string reportGroup in reportParameters.reportGroupsList)
                {
                    reportGroupNames += reportGroup + ELEMENT_REPORT_GROUPS_SEPARATOR;
                }
                reportRootElement.AppendChild(reportDocument.CreateElement(ELEMENT_NAME_REPORT_GROUPS));
                reportRootElement.SetAttribute("value", reportGroupNames);
            }
            // << Task #17969 PAX2SIM - Reports improvements
            if (reportParameters.Titre != null)
            {
                tmp = reportDocument.CreateElement("Title");
                tmp.SetAttribute("value", reportParameters.Titre);
                reportRootElement.AppendChild(tmp);
            }
            if (reportParameters.Date != null)
            {
                tmp = reportDocument.CreateElement("Date");
                tmp.SetAttribute("value", reportParameters.Date);
                reportRootElement.AppendChild(tmp);
            }
            if (reportParameters.Version != null)
            {
                tmp = reportDocument.CreateElement("Version");
                tmp.SetAttribute("value", reportParameters.Version);
                reportRootElement.AppendChild(tmp);
            }
            tmp = reportDocument.CreateElement("PageFormat");
            tmp.SetAttribute("value", reportParameters.PageFormat.ToString());
            reportRootElement.AppendChild(tmp);
            if (reportParameters.DividerLevel >= 0)
            {
                tmp = reportDocument.CreateElement("DividerLevel");
                tmp.SetAttribute("value", reportParameters.DividerLevel.ToString());
                reportRootElement.AppendChild(tmp);
            }
            if (reportParameters.Author != null)
            {
                tmp = reportDocument.CreateElement("Autor");
                tmp.SetAttribute("value", reportParameters.Author);
                reportRootElement.AppendChild(tmp);
            }
            if (reportParameters.UserLogo != null)
            {
                tmp = reportDocument.CreateElement("CustomLogo");
                tmp.SetAttribute("value", reportParameters.UserLogo);
                reportRootElement.AppendChild(tmp);
            }
            if (reportParameters.Template != null)
            {
                tmp = reportDocument.CreateElement("Template");
                tmp.SetAttribute("value", reportParameters.Template);
                reportRootElement.AppendChild(tmp);
            }
            // Table adjustment
            if (reportParameters.dResizeTable != null && reportParameters.dResizeTable.Count > 0)
            {
                tmp = reportDocument.CreateElement("TableAdjustement");
                foreach (String key in reportParameters.dResizeTable.Keys)
                {
                    XmlElement tmp2 = reportDocument.CreateElement("Table");
                    tmp2.SetAttribute("Name", key);
                    tmp2.SetAttribute("Type", reportParameters.dResizeTable[key].ToString());
                    tmp.AppendChild(tmp2);
                }
                reportRootElement.AppendChild(tmp);
            }
            // Versions & Document control
            if (reportParameters.VersionList != null)
            {
                tmp = reportDocument.CreateElement("VersionOverview");
                XmlElement xnWidth = reportDocument.CreateElement("Width");
                for (int i = 0; i < reportParameters.VersionColumnWidth.Length; i++)
                {
                    int width = reportParameters.VersionColumnWidth[i];
                    xnWidth.SetAttribute("w" + i.ToString(), reportParameters.VersionColumnWidth[i].ToString());
                }
                tmp.AppendChild(xnWidth);
                for (int i = 0; i < reportParameters.VersionList.Count; i++)
                {
                    String[] version = reportParameters.VersionList[i];
                    XmlElement row = reportDocument.CreateElement("Row");
                    foreach (String str in version)
                    {
                        XmlElement cell = reportDocument.CreateElement("Cell");
                        cell.InnerText = str;
                        row.AppendChild(cell);
                    }
                    tmp.AppendChild(row);
                }
                reportRootElement.AppendChild(tmp);
            }
            if (reportParameters.ApprovalList != null)
            {
                tmp = reportDocument.CreateElement("DocApproval");
                XmlElement xnWidth = reportDocument.CreateElement("Width");
                for (int i = 0; i < reportParameters.ApprovalColumnWidth.Length; i++)
                {
                    int width = reportParameters.ApprovalColumnWidth[i];
                    xnWidth.SetAttribute("w" + i.ToString(), reportParameters.ApprovalColumnWidth[i].ToString());
                }
                tmp.AppendChild(xnWidth);
                for (int i = 0; i < reportParameters.ApprovalList.Count; i++)
                {
                    String[] approval = reportParameters.ApprovalList[i];
                    XmlElement row = reportDocument.CreateElement("Row");
                    foreach (String str in approval)
                    {
                        XmlElement cell = reportDocument.CreateElement("Cell");
                        cell.InnerText = str;
                        row.AppendChild(cell);
                    }
                    tmp.AppendChild(row);
                }
                reportRootElement.AppendChild(tmp);
            }
            if (reportParameters.ProjStep != null)
            {
                tmp = reportDocument.CreateElement("ProjStep");
                tmp.SetAttribute("value", reportParameters.ProjStep);
                reportRootElement.AppendChild(tmp);
            }
            if (reportParameters.PartOf != null)
            {
                tmp = reportDocument.CreateElement("PartOf");
                tmp.SetAttribute("value", reportParameters.PartOf);
                reportRootElement.AppendChild(tmp);
            }
            if (reportParameters.IssuedDept != null)
            {
                tmp = reportDocument.CreateElement("IssuedDept");
                tmp.SetAttribute("value", reportParameters.IssuedDept);
                reportRootElement.AppendChild(tmp);
            }
            if (reportParameters.IntDocRef != null)
            {
                tmp = reportDocument.CreateElement("IntDocRef");
                tmp.SetAttribute("value", reportParameters.IntDocRef);
                reportRootElement.AppendChild(tmp);
            }
            if (reportParameters.ExtDocRef != null)
            {
                tmp = reportDocument.CreateElement("ExtDocRef");
                tmp.SetAttribute("value", reportParameters.ExtDocRef);
                reportRootElement.AppendChild(tmp);
            }
            if (reportParameters.DocStatus != null)
            {
                tmp = reportDocument.CreateElement("DocStatus");
                tmp.SetAttribute("value", reportParameters.DocStatus);
                reportRootElement.AppendChild(tmp);
            }
            if (reportParameters.Appendices != null)
            {
                tmp = reportDocument.CreateElement("Appendices");
                tmp.SetAttribute("value", reportParameters.Appendices);
                reportRootElement.AppendChild(tmp);
            }                
            if (reportParameters.tv_CustomTree != null && reportParameters.tv_CustomTree.Nodes.Count > 0)
            {
                tmp = reportDocument.CreateElement("SelectedData");
                addTreeNodeWithGraphicFilterDefinition(tmp, reportDocument, reportParameters.tv_CustomTree.Nodes, graphicFilterDefinitions);
                reportRootElement.AppendChild(tmp);
            }
            reportDocument.AppendChild(reportRootElement);            
            return reportDocument;
        }

        private static void addTreeNodeWithGraphicFilterDefinition(XmlElement parentElement, XmlDocument documentSource, 
            TreeNodeCollection sourceTreeNode, Dictionary<string, GraphicFilter> graphicFilterDefinitions)
        {
            foreach (TreeNode child in sourceTreeNode)
            {
                XmlElement nodeXmlElement = documentSource.CreateElement("Node");
                nodeXmlElement.SetAttribute("Name", child.Name);
                nodeXmlElement.SetAttribute("Text", child.Text);
                nodeXmlElement.SetAttribute("Checked", child.Checked.ToString());
                if (child.Tag == null || child.Tag.GetType() != typeof(TreeViewTag))
                {
                    continue;
                }
                TreeViewTag tag = child.Tag as TreeViewTag;
                XmlNode graphicDefinitionNode = null;
                if (tag != null)
                {
                    if (tag.ScenarioName != null)
                    {
                        nodeXmlElement.SetAttribute("Scenario", tag.ScenarioName);
                    }
                    nodeXmlElement.SetAttribute("TagType", tag.TypeClass.ToString());
                    if (tag.IsExceptionNode && tag.TypeClass == TreeViewTag.TreeViewTypeTag.ChartNode)
                    {
                        nodeXmlElement.SetAttribute("Exception", true.ToString());
                    }
                    if (tag.IsExceptionNode)
                    {
                        nodeXmlElement.SetAttribute("TypeExcep", tag.ExceptionName);
                    }
                    nodeXmlElement.SetAttribute("TableName", tag.Name);
                    if (tag.TypeClass == TreeViewTag.TreeViewTypeTag.ChartNode)
                    {
                        string key = tag.ScenarioName + child.Name;
                        if (graphicFilterDefinitions.ContainsKey(key))
                        {
                            GraphicFilter graphicFilter = graphicFilterDefinitions[key];
                            graphicDefinitionNode = graphicFilter.creerArbreXml(documentSource);
                        }
                    }
                }
                addTreeNodeWithGraphicFilterDefinition(nodeXmlElement, documentSource, child.Nodes, graphicFilterDefinitions);
                parentElement.AppendChild(nodeXmlElement);
                if (graphicDefinitionNode != null)
                {
                    nodeXmlElement.AppendChild(graphicDefinitionNode);
                }
            }
        }
        // << Task #17969 PAX2SIM - Reports improvements
        
        /// <summary>
        /// Add the tree node and his child to the xml document
        /// (used in versions before TreeEditor 11/04/2011)
        /// </summary>
        /// <param name="parentElt">Xml parent element</param>
        /// <param name="documentSource"></param>
        /// <param name="tn">node to export</param>
        private static void AddTreeNode_old(XmlElement parentElt, XmlDocument documentSource, TreeNode tn)
        {
            foreach (TreeNode child in tn.Nodes)
            {
                XmlElement tmp = documentSource.CreateElement("Node");
                tmp.SetAttribute("Name", child.Name);
                tmp.SetAttribute("Checked", child.Checked.ToString()); // pas touche ! ( bool.ToString() met une majuscule au debut :/ )
                AddTreeNode_old(tmp, documentSource, child);
                parentElt.AppendChild(tmp);
            }
        }
        /// <summary>
        /// Add the tree node and his child to the xml document
        /// </summary>
        /// <param name="parentElt">Xml parent element</param>
        /// <param name="documentSource"></param>
        /// <param name="tn">node to export</param>
        private static void AddTreeNode(XmlElement parentElt, XmlDocument documentSource, TreeNodeCollection tnc)
        {
            foreach (TreeNode child in tnc)
            {
                XmlElement tmp = documentSource.CreateElement("Node");
                tmp.SetAttribute("Name", child.Name);
                tmp.SetAttribute("Text", child.Text);
                tmp.SetAttribute("Checked", child.Checked.ToString());
                TreeViewTag tvt = child.Tag as TreeViewTag;
                if (tvt != null)
                {
                    if (tvt.ScenarioName != null)
                        tmp.SetAttribute("Scenario", tvt.ScenarioName);
                    tmp.SetAttribute("TagType", tvt.TypeClass.ToString());
                    if (tvt.IsExceptionNode && tvt.TypeClass == TreeViewTag.TreeViewTypeTag.ChartNode)
                        tmp.SetAttribute("Exception", true.ToString());
                    if (tvt.IsExceptionNode)
                        tmp.SetAttribute("TypeExcep", tvt.ExceptionName);
                    tmp.SetAttribute("TableName", tvt.Name);
                    //if (OverallTools.FonctionUtiles.hasNamedAttribute(xnTreeNode, "TableName"))
                    //    sTableName = xnTreeNode.Attributes["TableName"].InnerText;

                }
                AddTreeNode(tmp, documentSource, child.Nodes);
                parentElt.AppendChild(tmp);
            }
        }

        // load the parameters *
        /// <summary>
        /// Build all ReportParameters from the .pax xml file.
        /// </summary>
        /// <param name="xnReportList">Xml node containing the report list.</param>
        /// <param name="Data"></param>
        /// <returns>List of ReportParameters</returns>
        public static Dictionary<string, ReportParameters> ImportParameters(XmlNode xnReportList, VersionManager Version, TreeNodeCollection MainTree)
        {
            //if (Version <= new VersionManager(1, 29))
            //{
                // Old chargement....
                //return null;
            //}
            if (xnReportList == null)
                return null;
            Dictionary<string, ReportParameters> htReportList = new Dictionary<string, ReportParameters>();
            foreach (XmlNode xnChild in xnReportList.ChildNodes)
            {
                if (xnChild.Name == "ReportParameters")
                {
                    htReportList.Add(
                        xnChild.Attributes["Name"].InnerText,
                        new ReportParameters(xnChild, Version, MainTree));
                }
            }
            return htReportList;
        }
        /// <summary>
        /// Build a ReportParameters from the .pax xml file.
        /// </summary>
        /// <param name="xnParameters">Xml node containing the report parameters.</param>
        /// <param name="Data"></param>
        public ReportParameters(XmlNode xnParameters, VersionManager vmVersion, TreeNodeCollection MainTree)
        {
            foreach (XmlNode xnParam in xnParameters.ChildNodes)
            {
                if (xnParam.Name == "ExportNote")
                    ExportNote = true;
                if (xnParam.Name == "GeneratePDF")
                    GeneratePDF = true;
                if (xnParam.Name == "GenerateHTML")
                    GenerateHTML = true;
                if (xnParam.Name == "IncludeOnlyChecked")
                    IncludeOnlyChecked = true;
                // >> Task #17969 PAX2SIM - Reports improvements                
                if (xnParam.Name == ELEMENT_NAME_EXPORT_TXT_FILES)
                {
                    exportSourceTextFiles = true;
                }
                if (xnParam.Name == ELEMENT_NAME_EXPORT_XML_CONFIGURATION)
                {
                    exportXmlConfiguration = true;
                }                
                if (xnParam.Name == ELEMENT_NAME_REPORT_GROUPS)
                {
                    string reportGroupsAsString = xnParam.Attributes["value"].InnerText;
                    if (reportGroupsAsString.Contains(ELEMENT_REPORT_GROUPS_SEPARATOR))
                    {
                        string[] groups = reportGroupsAsString.Split(new string[] { ELEMENT_REPORT_GROUPS_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string reportGroup in groups)
                        {
                            reportGroupsList.Add(reportGroup);
                        }
                    }
                }
                // << Task #17969 PAX2SIM - Reports improvements
                int iValue;
                if (xnParam.Name == "PageFormat")
                {
                    Int32.TryParse(xnParam.Attributes["value"].InnerText, out iValue);
                    PageFormat = iValue;
                }
                if (xnParam.Name == "DividerLevel")
                    DividerLevel = FonctionsType.getInt(xnParam.Attributes["value"].InnerText);
                //else  // Bug Divider level
                //    DividerLevel = -1;
                if (xnParam.Name == "Autor")
                    Author = xnParam.Attributes["value"].InnerText;
                if (xnParam.Name == "CustomLogo")
                    UserLogo = xnParam.Attributes["value"].InnerText;
                if (xnParam.Name == "Template")
                    Template = xnParam.Attributes["value"].InnerText;
                if (xnParam.Name == "Title")
                    Titre = xnParam.Attributes["value"].InnerText;
                if (xnParam.Name == "Version")
                    Version = xnParam.Attributes["value"].InnerText;
                if (xnParam.Name == "Date")
                    Date = xnParam.Attributes["value"].InnerText;
                if (xnParam.Name == "TableAdjustement")
                {
                    dResizeTable = new Dictionary<string, ColumnSizePolicy>();
                    foreach (XmlNode xnAdj in xnParam.ChildNodes)
                    {
                        String name = xnAdj.Attributes["Name"].Value;
                        String adj = xnAdj.Attributes["Type"].Value;
                        if (adj == ColumnSizePolicy.AdjustContent.ToString())
                        {
                            dResizeTable.Add(name, ColumnSizePolicy.AdjustContent);
                        }
                        else if (adj == ColumnSizePolicy.AdjustPage.ToString())
                        {
                            dResizeTable.Add(name, ColumnSizePolicy.AdjustPage);
                        }
                        else
                            dResizeTable.Add(name, ColumnSizePolicy.AdjustDefault);
                    }
                }
                if (xnParam.Name == "VersionOverview")
                {
                    VersionList = new List<string[]>();
                    VersionColumnWidth = new int[5];
                    int j = 0;
                    foreach (XmlNode xnRow in xnParam.ChildNodes)
                    {
                        if (xnRow.Name == "Width")
                            for (int n = 0; n < xnRow.Attributes.Count; n++)
                            {
                                int width;
                                if (Int32.TryParse(xnRow.Attributes[n].InnerText, out width))
                                    VersionColumnWidth[n] = width;
                                else VersionColumnWidth[n] = 100;
                            }
                        else
                        {
                            String[] row = new String[5];
                            int i = 0;
                            foreach (XmlNode xnCell in xnRow.ChildNodes)
                            {
                                row[i] = xnCell.InnerText;
                                i++;
                            }
                            VersionList.Add(row);
                            j++;
                        }
                    }
                }
                if (xnParam.Name == "DocApproval")
                {
                    ApprovalList = new List<string[]>();
                    ApprovalColumnWidth = new int[5];
                    int j = 0;
                    foreach (XmlNode xnRow in xnParam.ChildNodes)
                    {
                        if (xnRow.Name == "Width")
                            for (int n = 0; n < xnRow.Attributes.Count; n++)
                            {
                                int width;
                                if (Int32.TryParse(xnRow.Attributes[n].InnerText, out width))
                                    ApprovalColumnWidth[n] = width;
                                else ApprovalColumnWidth[n] = 100;
                            }
                        else
                        {
                            String[] row = new String[5];
                            int i = 0;
                            foreach (XmlNode xnCell in xnRow.ChildNodes)
                            {
                                row[i] = xnCell.InnerText;
                                i++;
                            }
                            ApprovalList.Add(row);
                            j++;
                        }
                    }
                }
                if (xnParam.Name == "ProjStep")
                    ProjStep = xnParam.Attributes["value"].InnerText;
                if (xnParam.Name == "PartOf")
                    PartOf = xnParam.Attributes["value"].InnerText;
                if (xnParam.Name == "IssuedDept")
                    IssuedDept = xnParam.Attributes["value"].InnerText;
                if (xnParam.Name == "IntDocRef")
                    IntDocRef = xnParam.Attributes["value"].InnerText;
                if (xnParam.Name == "ExtDocRef")
                    ExtDocRef = xnParam.Attributes["value"].InnerText;
                if (xnParam.Name == "DocStatus")
                    DocStatus = xnParam.Attributes["value"].InnerText;
                if (xnParam.Name == "Appendices")
                    Appendices = xnParam.Attributes["value"].InnerText;
                if (xnParam.Name == "SelectedData")
                {
                    if (vmVersion <= new VersionManager(1, 29))
                    {

                        // Load old version with checked Tree
                        this.tv_PurgedTree = new TreeView();
                        foreach (XmlNode xnTreeNode in xnParam.ChildNodes)
                        {
                            TreeNode tn = new TreeNode();
                            tn.Name = xnTreeNode.Attributes["Name"].InnerText;
                            bool bValue;
                            Boolean.TryParse(xnTreeNode.Attributes["Checked"].InnerText, out bValue);
                            tn.Checked = bValue;
                            //tn.Checked =  Boolean.Parse( xnTreeNode.Attributes["Checked"].InnerText);
                            tv_PurgedTree.Nodes.Add(tn);
                            BuildPurgedTree(tn, xnTreeNode);
                        }

                        // convert old checked tree (before 1.29) to custom tree
                        this.tv_CustomTree = new TreeView();
                        ConvertCheckedToCustomTree(MainTree, tv_PurgedTree_.Nodes, tv_CustomTree.Nodes);
                        this.tv_PurgedTree = null;
                    }
                    else
                    {
                        // Load custom tree
                        this.tv_CustomTree = new TreeView();
                        BuildTree(tv_CustomTree.Nodes, xnParam.ChildNodes);
                        this.tv_PurgedTree = null;
                    }
                }
            }
        }

        /// <summary>
        /// Convert the old Checked TreeView in new Custom TreeView by keeping only the checked TreeNode.
        /// a partir de la version 1.29 le 15.04.2011
        /// </summary>
        /// <param name="OriginalNodes"></param>
        /// <param name="CheckedNodes"></param>
        /// <param name="CustomNodes"></param>
        private void ConvertCheckedToCustomTree(TreeNodeCollection OriginalNodes, TreeNodeCollection CheckedNodes, TreeNodeCollection CustomNodes)
        {
            foreach (TreeNode tn_Purged in CheckedNodes)
            {
                TreeNode originalNode = OriginalNodes[tn_Purged.Name];
                if (originalNode != null)
                {
                    TreeViewTag Tag = (originalNode.Tag as TreeViewTag);
                    TreeNode tn = OverallTools.TreeViewFunctions.createBranch(originalNode.Name, originalNode.Text, Tag, null, false, true);
                    CustomNodes.Add(tn);
                    ConvertCheckedToCustomTree(originalNode.Nodes, tn_Purged.Nodes, tn.Nodes);
                }
            }
        }

        /// <summary>
        /// Build TreeView according to the report elements.
        /// </summary>
        /// <param name="nodes">Collection where to add the created nodes</param>
        /// <param name="xnNodes">List where to read the nodes definition</param>
        private void BuildTree(TreeNodeCollection nodes, XmlNodeList xnNodes)
        {
            foreach (XmlNode xnTreeNode in xnNodes)
            {
                TreeNode tn = new TreeNode();
                tn.Name = xnTreeNode.Attributes["Name"].InnerText;
                if (OverallTools.FonctionUtiles.hasNamedAttribute(xnTreeNode, "Text"))
                    tn.Text = xnTreeNode.Attributes["Text"].InnerText;
                else
                    tn.Text = tn.Name;
                bool bValue;
                Boolean.TryParse(xnTreeNode.Attributes["Checked"].InnerText, out bValue);
                tn.Checked = bValue;
                XmlAttribute xattr = xnTreeNode.Attributes["Scenario"];
                TreeViewTag tnt;
                String sScenarioName = null;
                if (xattr != null) // get scenario name
                    sScenarioName = xattr.InnerText;

                TreeViewTag.TreeViewTypeTag.ParagraphNode.ToString();

                String sTableName = tn.Name;
                String sExceptName = "";
                bool bExcept = false;
                if (OverallTools.FonctionUtiles.hasNamedAttribute(xnTreeNode, "TypeExcep"))
                {
                    sExceptName = xnTreeNode.Attributes["TypeExcep"].InnerText;
                }
                if (OverallTools.FonctionUtiles.hasNamedAttribute(xnTreeNode, "Exception"))
                {
                    Boolean.TryParse(xnTreeNode.Attributes["Exception"].InnerText, out bExcept);
                }
                if (OverallTools.FonctionUtiles.hasNamedAttribute(xnTreeNode, "TableName"))
                    sTableName = xnTreeNode.Attributes["TableName"].InnerText;
                String tagType="";
                if (OverallTools.FonctionUtiles.hasNamedAttribute(xnTreeNode, "TagType"))
                    // create the tag according to the TagType
                    tagType= xnTreeNode.Attributes["TagType"].InnerText;
                if (tagType.Equals(TreeViewTag.TreeViewTypeTag.DirectoryNode.ToString()))
                    tnt = TreeViewTag.getDirectoryNode(sTableName);
                else if (tagType.Equals(TreeViewTag.TreeViewTypeTag.ChartNode.ToString()))
                    tnt = TreeViewTag.getChartNode(sTableName);
                else if (tagType.Equals(TreeViewTag.TreeViewTypeTag.FilterNode.ToString()))
                    tnt = TreeViewTag.getFilterNode(sScenarioName, sTableName);
                else if (tagType.Equals(TreeViewTag.TreeViewTypeTag.ParagraphNode.ToString()))
                {
                    //>>GanttNote for Report
                    // check if the node contains a Note or a Gantt Note; because the only attribute present 
                    // that can separete the Note from the Gantt Note is the Text of the Node
                    // we check if the text has the 'Gantt' word 
                    if (tn.Text.Contains(Model.GANTT_PREFIX))
                        tnt = TreeViewTag.getGanttParagraphNode(sTableName);
                    else
                        tnt = TreeViewTag.getParagraphNode(sTableName);
                    if(sExceptName!= "")
                    {
                        tnt.IsUnleashed = true;
                        tnt.IsExceptionNode = true;
                        tnt.ScenarioName = sScenarioName;
                        tnt.ExceptionName = sExceptName;
                    }
                }
                else if (tagType.Equals(TreeViewTag.TreeViewTypeTag.TableNode.ToString()))
                    tnt = TreeViewTag.getTableNode(sScenarioName, sTableName);
                else if (tagType.Equals(TreeViewTag.TreeViewTypeTag.ResultNode.ToString()))
                    tnt = TreeViewTag.getResultNode(sScenarioName, sTableName);
                else if (tagType.Equals(TreeViewTag.TreeViewTypeTag.ExceptionTable.ToString()))
                    tnt = TreeViewTag.getExceptionTableNode(sScenarioName, sTableName, sExceptName);
                else if (tagType.Equals(TreeViewTag.TreeViewTypeTag.ExceptionFilter.ToString()))
                    tnt = TreeViewTag.getExceptionFilterNode(sScenarioName, sTableName, sExceptName, false, true);
                else
                {
                    // undefined node
                    tnt = new TreeViewTag();
                    tnt.Name = sTableName;
                    tnt.ScenarioName = sScenarioName;
                }
                tnt.IsUnleashed = true;
                tnt.ScenarioName = sScenarioName;
                if (bExcept)
                {
                    tnt.IsExceptionNode = true;
                    tnt.ExceptionName = sExceptName;
                }
                tn.ImageIndex = tnt.ImageIndex;
                tn.SelectedImageIndex = tnt.SelectedImageIndex;
                tn.Tag = tnt;
                nodes.Add(tn);

                // Add children node
                BuildTree(tn.Nodes, xnTreeNode.ChildNodes);
            }
        }

        /// <summary>
        /// Build a simple TreeView according to the checked elements of the xml selected tree.
        /// (version before tree editor 11/04/2011)
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="xnParentNode"></param>
        private void BuildPurgedTree(TreeNode treeNode, XmlNode xnParentNode)
        {
            foreach (XmlNode xnNode in xnParentNode.ChildNodes)
            {
                TreeNode tn = new TreeNode();
                tn.Name = xnNode.Attributes["Name"].InnerText;
                tn.Checked = xnNode.Attributes["Checked"].InnerText.Equals("true");
                treeNode.Nodes.Add(tn);
                BuildPurgedTree(tn, xnNode);
            }
        }

        /// <summary>
        /// Function to create a simple tree with juste the node name 
        /// and the Checked value.
        /// (used in versions before TreeEditor 11/04/2011)
        /// </summary>
        /// <param name="document">Pax xml document</param>
        /// <param name="node"></param>
        /// <returns></returns>
        private XmlElement generateXmlSelectedTree(XmlDocument document, TreeNode node)
        {
            XmlElement result = document.CreateElement(node.Name);
            result.SetAttribute("Value", node.Checked.ToString());
            int i = 0;
            if (node.Nodes.Count != 0)
            {
                if (node.Nodes[0].Name == "Chart")
                {
                    i++;
                    result.SetAttribute("Value", node.Nodes[0].Checked.ToString());
                }
            }
            for (; i < node.Nodes.Count; i++)
            {
                XmlElement child = generateXmlSelectedTree(document, node.Nodes[i]);
                if (child != null)
                {
                    result.AppendChild(child);
                }
            }
            return result;
        }
        #endregion Import/Export xml in .pax file

        public ReportParameters()
        {   
        }
    }
}
