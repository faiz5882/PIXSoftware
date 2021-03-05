using System.Windows.Forms;
namespace SIMCORE_TOOL
{
    partial class PAX2SIM
    { 
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PAX2SIM));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            Nevron.Chart.WinForm.NChartCommander nChartCommander1 = new Nevron.Chart.WinForm.NChartCommander();
            Nevron.Chart.WinForm.NChartToolbarCommandBuilder nChartToolbarCommandBuilder1 = new Nevron.Chart.WinForm.NChartToolbarCommandBuilder();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsl_WyStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.sc_Main_Tree = new System.Windows.Forms.SplitContainer();
            this.automaticUpdater1 = new wyDay.Controls.AutomaticUpdater();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.sc_Content_Help = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.TabViewTable = new System.Windows.Forms.TabPage();
            this.panel_Table = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsb_AddRow = new System.Windows.Forms.ToolStripButton();
            this.tsb_Edit = new System.Windows.Forms.ToolStripButton();
            this.tsb_DeleteRow = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ouvrirToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.enregistrerToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.PrintPreview = new System.Windows.Forms.ToolStripButton();
            this.imprimerToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.couperToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copierToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.collerToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.CollapseAllColumns = new System.Windows.Forms.ToolStripButton();
            this.ExpandAllColumns = new System.Windows.Forms.ToolStripButton();
            this.ResizeAllColumns = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.searchTypeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SearchToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.searchToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.prevToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.nextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tsl_NumberRows = new System.Windows.Forms.ToolStripLabel();
            this.tsb_Allocate_CheckIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tstb_Value = new System.Windows.Forms.ToolStripTextBox();
            this.tsb_valid = new System.Windows.Forms.ToolStripButton();
            this.tp_Summary = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.TabChart = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Graphique = new Nevron.Chart.WinForm.NChartControl();
            this.tp_BHS = new System.Windows.Forms.TabPage();
            this.tabSimul8 = new System.Windows.Forms.TabPage();
            this.tabGantt = new System.Windows.Forms.TabPage();
            this.axShockwaveFlash1 = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.ganttTitle = new System.Windows.Forms.Label();
            this.tabNewItinerary = new System.Windows.Forms.TabPage();
            this.shockwaveFlashForItinerary = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.axShockwaveFlash3 = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.axShockwaveFlash2 = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.testButton = new System.Windows.Forms.Button();
            this.tabPageDashboard = new System.Windows.Forms.TabPage();
            this.axShockwaveFlashForDashboard = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tssb_Perimeter = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmi_HUB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_PAX = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsi_BHS = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_TMS = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_AIR = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_PKG = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.AirportDef = new System.Windows.Forms.ToolStripButton();
            this.InputData = new System.Windows.Forms.ToolStripButton();
            this.StaticAnalysis = new System.Windows.Forms.ToolStripButton();
            this.DynamicAnalysis = new System.Windows.Forms.ToolStripButton();
            this.tsb_BHS = new System.Windows.Forms.ToolStripButton();
            this.tsb_Runway = new System.Windows.Forms.ToolStripButton();
            this.tsb_Chart = new System.Windows.Forms.ToolStripButton();
            this.Results = new System.Windows.Forms.ToolStripButton();
            this.tsbStartSimulation = new System.Windows.Forms.ToolStripButton();
            this.menuSIMCORE = new System.Windows.Forms.MenuStrip();
            this.lancementEtape2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitsAndSpeedsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listOfErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLatestErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLatestErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.allocateMakeUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allocateReclaimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allocateTransferInfeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allocateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateFlightPlansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estimateAirportOccupationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateFlightPlansAIAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDubaiFlightPlansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateFilesForDubaiAllocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateFlightPlanInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importLiegeFlightPlansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateLiegeAllocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importCDGDepartureFlightPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specificationDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appearenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.styleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTemporaryFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.userManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bHSTraceKeywordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.releaseNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemRequirementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tp_Itinerary = new System.Windows.Forms.TabPage();
            this.tabSimreporter = new System.Windows.Forms.TabPage();
            this.simreporterLaunchButton = new System.Windows.Forms.Button();
            this.simreporterWebBrowser = new System.Windows.Forms.WebBrowser();
            this.contextMenuAirport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.New = new System.Windows.Forms.ToolStripMenuItem();
            this.Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Properties = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromFPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuInput = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loadDefaultTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditTable = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportTable = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromExistingTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputContextMenuPasteFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyGraphicDefinitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteGraphicDefinitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToChartTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTargetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTargetTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewStatisticsToolStripRightClickMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCountingParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCountResultTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteISTCountedItemsTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigateToDeskFromTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyColorCodeOnGenericTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopyTableName = new System.Windows.Forms.ToolStripMenuItem();
            this.addToChartFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyPRJ4C = new System.Windows.Forms.NotifyIcon(this.components);
            this.cms_NotifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_Pax2SimBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuDataView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigateToDeskFromDatagridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.nChartCommandBarsManager1 = new Nevron.Chart.WinForm.NChartCommandBarsManager();
            this.ContextMenuFilter = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuFilterCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFilterEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFilterCut = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuFilterCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFilterPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFilterRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.copyGraphicDefinitionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteGraphicDefinitionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewFilterStatisticsToolStripRightClickMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigateToDeskFromFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateTextGanttFromFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyColorCodeOnFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTableNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IL_ImagesToolbar = new System.Windows.Forms.ImageList(this.components);
            this.fbd_SelectDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.cmsBranchTestMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeBHSResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsTextFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCharts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFilterToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteFilterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyChartDefinitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteChartDefinitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageChartLinksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsUserData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_AddUserData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_DeleteUserData = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_AnalyzeBHSResults = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmsi_analyzeBHSResults = new System.Windows.Forms.ToolStripMenuItem();
            this.launchMultipleScenariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_AllocationMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsAutomodMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addCustomGraphicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCustomGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_UserGraphics = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromExistingTableToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editColumnsNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFilterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyUserGraphicsChartDefinitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteUserGraphicsChartDefinitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateTextGanttFromTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyColorCodeOnTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsStatistics = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_ColumnHeader = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsExceptionTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditExceptionTable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteExceptionTable = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsReports = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.expandCollapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assignToReportGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateReportGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateReportGroupAsPdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateReportGroupAsHtmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pdfFormatPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.htmlFormatPreviewFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pdfFormatGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.htmlFormatGeneratorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigateToSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageLinksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshMainReportsNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsTextFilesFromReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportXMLReportConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyColorCodeFromReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportScenarioTablesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportAsTextFilesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cmsDocuments = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openDocumentsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFromProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addDocumentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllDocumenstMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDocument = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openDocumentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateDocumentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDocumentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.sc_Main_Tree.Panel1.SuspendLayout();
            this.sc_Main_Tree.Panel2.SuspendLayout();
            this.sc_Main_Tree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.automaticUpdater1)).BeginInit();
            this.sc_Content_Help.Panel1.SuspendLayout();
            this.sc_Content_Help.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.TabViewTable.SuspendLayout();
            this.panel_Table.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.tp_Summary.SuspendLayout();
            this.TabChart.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabGantt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).BeginInit();
            this.tabNewItinerary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shockwaveFlashForItinerary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash2)).BeginInit();
            this.tabPageDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlashForDashboard)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.menuSIMCORE.SuspendLayout();
            this.tabSimreporter.SuspendLayout();
            this.contextMenuAirport.SuspendLayout();
            this.contextMenuInput.SuspendLayout();
            this.cms_NotifyMenu.SuspendLayout();
            this.contextMenuDataView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nChartCommandBarsManager1)).BeginInit();
            this.ContextMenuFilter.SuspendLayout();
            this.cmsBranchTestMenu.SuspendLayout();
            this.cmsCharts.SuspendLayout();
            this.cmsUserData.SuspendLayout();
            this.cms_AnalyzeBHSResults.SuspendLayout();
            this.cms_AllocationMenu.SuspendLayout();
            this.cmsAutomodMenu.SuspendLayout();
            this.cms_UserGraphics.SuspendLayout();
            this.cmsStatistics.SuspendLayout();
            this.cmsExceptionTable.SuspendLayout();
            this.cmsReports.SuspendLayout();
            this.exportScenarioTablesContextMenuStrip.SuspendLayout();
            this.cmsDocuments.SuspendLayout();
            this.cmsDocument.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.sc_Main_Tree);
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.toolStrip1);
            resources.ApplyResources(this.toolStripContainer1.LeftToolStripPanel, "toolStripContainer1.LeftToolStripPanel");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuSIMCORE);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripStatusLabel1,
            this.tsl_WyStatus});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            resources.ApplyResources(this.toolStripStatusLabel, "toolStripStatusLabel");
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Spring = true;
            // 
            // tsl_WyStatus
            // 
            this.tsl_WyStatus.Name = "tsl_WyStatus";
            resources.ApplyResources(this.tsl_WyStatus, "tsl_WyStatus");
            // 
            // sc_Main_Tree
            // 
            this.sc_Main_Tree.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.sc_Main_Tree, "sc_Main_Tree");
            this.sc_Main_Tree.Name = "sc_Main_Tree";
            // 
            // sc_Main_Tree.Panel1
            // 
            this.sc_Main_Tree.Panel1.Controls.Add(this.automaticUpdater1);
            this.sc_Main_Tree.Panel1.Controls.Add(this.treeView1);
            // 
            // sc_Main_Tree.Panel2
            // 
            this.sc_Main_Tree.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.sc_Main_Tree.Panel2.Controls.Add(this.sc_Content_Help);
            // 
            // automaticUpdater1
            // 
            this.automaticUpdater1.ContainerForm = this;
            this.automaticUpdater1.KeepHidden = true;
            resources.ApplyResources(this.automaticUpdater1, "automaticUpdater1");
            this.automaticUpdater1.Name = "automaticUpdater1";
            this.automaticUpdater1.ToolStripItem = this.checkForUpdatesToolStripMenuItem;
            this.automaticUpdater1.UpdateType = wyDay.Controls.UpdateType.DoNothing;
            this.automaticUpdater1.wyUpdateCommandline = null;
            this.automaticUpdater1.BeforeChecking += new wyDay.Controls.BeforeHandler(this.automaticUpdater1_BeforeChecking);
            this.automaticUpdater1.BeforeDownloading += new wyDay.Controls.BeforeHandler(this.automaticUpdater1_BeforeDownloading);
            this.automaticUpdater1.CheckingFailed += new wyDay.Controls.FailHandler(this.automaticUpdater1_CheckingFailed);
            this.automaticUpdater1.DownloadingOrExtractingFailed += new wyDay.Controls.FailHandler(this.automaticUpdater1_DownloadingOrExtractingFailed);
            this.automaticUpdater1.UpdateAvailable += new System.EventHandler(this.automaticUpdater1_UpdateAvailable);
            this.automaticUpdater1.UpdateFailed += new wyDay.Controls.FailHandler(this.automaticUpdater1_UpdateFailed);
            this.automaticUpdater1.UpdateSuccessful += new wyDay.Controls.SuccessHandler(this.automaticUpdater1_UpdateSuccessful);
            this.automaticUpdater1.UpToDate += new wyDay.Controls.SuccessHandler(this.automaticUpdater1_UpToDate);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            resources.ApplyResources(this.checkForUpdatesToolStripMenuItem, "checkForUpdatesToolStripMenuItem");
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            resources.ApplyResources(this.treeView1, "treeView1");
            this.treeView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.treeView1.FullRowSelect = true;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Name = "treeView1";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeView1.Nodes"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeView1.Nodes1"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeView1.Nodes2")))});
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "icon_Treeview_Airport.gif");
            this.imageList1.Images.SetKeyName(2, "icon_Treeview_Terminal.gif");
            this.imageList1.Images.SetKeyName(3, "Icon_Treeview_Level.gif");
            this.imageList1.Images.SetKeyName(4, "icon_Treeview_Checkin.gif");
            this.imageList1.Images.SetKeyName(5, "icon_Treeview_Police.gif");
            this.imageList1.Images.SetKeyName(6, "icon_Treeview_Security.gif");
            this.imageList1.Images.SetKeyName(7, "icon_Treeview_ArrivalGate.gif");
            this.imageList1.Images.SetKeyName(8, "icon_Treeview_Baggage.gif");
            this.imageList1.Images.SetKeyName(9, "icon_Treeview_TransferDesk.gif");
            this.imageList1.Images.SetKeyName(10, "icon_Treeview_UserProcess.gif");
            this.imageList1.Images.SetKeyName(11, "Icon_Treeview_DepartureGate.gif");
            this.imageList1.Images.SetKeyName(12, "iconTreeview_IN.gif");
            this.imageList1.Images.SetKeyName(13, "iconTreeview_OUT.gif");
            this.imageList1.Images.SetKeyName(14, "icon_Treeview_Shopping_2.gif");
            this.imageList1.Images.SetKeyName(15, "");
            this.imageList1.Images.SetKeyName(16, "icon_Treeview_Coll CI.jpg");
            this.imageList1.Images.SetKeyName(17, "icon_Treeview_Transfer.gif");
            this.imageList1.Images.SetKeyName(18, "icon_Treeview_L1.gif");
            this.imageList1.Images.SetKeyName(19, "icon_Treeview_L3.gif");
            this.imageList1.Images.SetKeyName(20, "icon_Treeview_L5.gif");
            this.imageList1.Images.SetKeyName(21, "icon_Treeview_Manual Encoding.gif");
            this.imageList1.Images.SetKeyName(22, "icon_Treeview_EBS.gif");
            this.imageList1.Images.SetKeyName(23, "icon_Treeview_Make Up.gif");
            this.imageList1.Images.SetKeyName(24, "icon_Treeview_HBS.gif");
            this.imageList1.Images.SetKeyName(25, "icon_Treeview_ArrivalGate.gif");
            this.imageList1.Images.SetKeyName(26, "");
            this.imageList1.Images.SetKeyName(27, "Runway.bmp");
            this.imageList1.Images.SetKeyName(28, "Graph.bmp");
            this.imageList1.Images.SetKeyName(29, "edit.gif");
            this.imageList1.Images.SetKeyName(30, "note.gif");
            this.imageList1.Images.SetKeyName(31, "Table_Normal.bmp");
            this.imageList1.Images.SetKeyName(32, "Table_Exception.bmp");
            this.imageList1.Images.SetKeyName(33, "Table_Note.bmp");
            this.imageList1.Images.SetKeyName(34, "Table_Note_Exception.bmp");
            this.imageList1.Images.SetKeyName(35, "Table_Selected.bmp");
            this.imageList1.Images.SetKeyName(36, "Table_Selected_Exception.bmp");
            this.imageList1.Images.SetKeyName(37, "Table_Selected_Note.bmp");
            this.imageList1.Images.SetKeyName(38, "Table_Selected_Exception_Note.bmp");
            this.imageList1.Images.SetKeyName(39, "Filter_Normal.bmp");
            this.imageList1.Images.SetKeyName(40, "Filter_Exception.bmp");
            this.imageList1.Images.SetKeyName(41, "Filter_Note.bmp");
            this.imageList1.Images.SetKeyName(42, "Filter_Note_Exception.bmp");
            this.imageList1.Images.SetKeyName(43, "Filter_Selected.bmp");
            this.imageList1.Images.SetKeyName(44, "Filter_Selected_Exception.bmp");
            this.imageList1.Images.SetKeyName(45, "Filter_Selected_Note.bmp");
            this.imageList1.Images.SetKeyName(46, "Filter_Selected_Note_Exception.bmp");
            this.imageList1.Images.SetKeyName(47, "Filter_Blocked.bmp");
            this.imageList1.Images.SetKeyName(48, "Filter_Blocked_Exception.bmp");
            this.imageList1.Images.SetKeyName(49, "Filter_Blocked_Note.bmp");
            this.imageList1.Images.SetKeyName(50, "Filter_Blocked_Note_Exception.bmp");
            this.imageList1.Images.SetKeyName(51, "Filter_Blocked_Selected.bmp");
            this.imageList1.Images.SetKeyName(52, "Filter_Blocked_Selected_Exception.bmp");
            this.imageList1.Images.SetKeyName(53, "Filter_Blocked_Selected_Note.bmp");
            this.imageList1.Images.SetKeyName(54, "Filter_Blocked_Selected_Note_Exception.bmp");
            this.imageList1.Images.SetKeyName(55, "icon_ReportTreeview_Airport_Ltblue.gif");
            this.imageList1.Images.SetKeyName(56, "icon_ReportTreeview_Terminal_Ltblue.gif");
            this.imageList1.Images.SetKeyName(57, "Icon_ReportTreeview_Level_Ltblue.gif");
            this.imageList1.Images.SetKeyName(58, "icon_ReportTreeview_Checkin_Ltblue.gif");
            this.imageList1.Images.SetKeyName(59, "icon_ReportTreeview_Police_Ltblue.gif");
            this.imageList1.Images.SetKeyName(60, "icon_ReportTreeview_Security_LtBlue.gif");
            this.imageList1.Images.SetKeyName(61, "icon_ReportTreeview_ArrivalGate_Ltblue.gif");
            this.imageList1.Images.SetKeyName(62, "icon_ReportTreeview_Baggage_Ltblue.gif");
            this.imageList1.Images.SetKeyName(63, "icon_ReportTreeview_TransferDesk_Ltblue.gif");
            this.imageList1.Images.SetKeyName(64, "iconReportTreeview_UserProcess_Ltblue.gif");
            this.imageList1.Images.SetKeyName(65, "Icon_ReportTreeview_DepartureGate_Ltblue.gif");
            this.imageList1.Images.SetKeyName(66, "iconReportTreeview_IN_Ltblue.gif");
            this.imageList1.Images.SetKeyName(67, "iconReportTreeview_Out_Ltblue.gif");
            this.imageList1.Images.SetKeyName(68, "iconReportTreeview_Shopping2_Ltblue.gif");
            this.imageList1.Images.SetKeyName(69, "");
            this.imageList1.Images.SetKeyName(70, "pdf.png");
            this.imageList1.Images.SetKeyName(71, "dashboard.png");
            this.imageList1.Images.SetKeyName(72, "Report_Icon_16.png");
            // 
            // sc_Content_Help
            // 
            resources.ApplyResources(this.sc_Content_Help, "sc_Content_Help");
            this.sc_Content_Help.Name = "sc_Content_Help";
            // 
            // sc_Content_Help.Panel1
            // 
            this.sc_Content_Help.Panel1.Controls.Add(this.tabControl);
            this.sc_Content_Help.Panel2Collapsed = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.TabViewTable);
            this.tabControl.Controls.Add(this.tp_Summary);
            this.tabControl.Controls.Add(this.TabChart);
            this.tabControl.Controls.Add(this.tp_BHS);
            this.tabControl.Controls.Add(this.tabSimul8);
            this.tabControl.Controls.Add(this.tabGantt);
            this.tabControl.Controls.Add(this.tabNewItinerary);
            this.tabControl.Controls.Add(this.tabPageDashboard);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.HotTrack = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // TabViewTable
            // 
            this.TabViewTable.BackColor = System.Drawing.Color.SkyBlue;
            this.TabViewTable.Controls.Add(this.panel_Table);
            this.TabViewTable.Controls.Add(this.toolStrip2);
            resources.ApplyResources(this.TabViewTable, "TabViewTable");
            this.TabViewTable.Name = "TabViewTable";
            this.TabViewTable.UseVisualStyleBackColor = true;
            // 
            // panel_Table
            // 
            this.panel_Table.Controls.Add(this.dataGridView1);
            resources.ApplyResources(this.panel_Table, "panel_Table");
            this.panel_Table.Name = "panel_Table";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.dataGridView1_CellToolTipTextNeeded);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyUp);
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_AddRow,
            this.tsb_Edit,
            this.tsb_DeleteRow,
            this.toolStripSeparator6,
            this.ouvrirToolStripButton,
            this.enregistrerToolStripButton,
            this.tsbExcel,
            this.PrintPreview,
            this.imprimerToolStripButton,
            this.toolStripSeparator,
            this.couperToolStripButton,
            this.copierToolStripButton,
            this.collerToolStripButton,
            this.toolStripSeparator4,
            this.CollapseAllColumns,
            this.ExpandAllColumns,
            this.ResizeAllColumns,
            this.toolStripSeparator5,
            this.searchTypeToolStripButton,
            this.SearchToolStripTextBox,
            this.searchToolStripButton,
            this.prevToolStripButton,
            this.nextToolStripButton,
            this.tsl_NumberRows,
            this.tsb_Allocate_CheckIn,
            this.toolStripSeparator1,
            this.tstb_Value,
            this.tsb_valid});
            resources.ApplyResources(this.toolStrip2, "toolStrip2");
            this.toolStrip2.Name = "toolStrip2";
            // 
            // tsb_AddRow
            // 
            this.tsb_AddRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_AddRow.Image = global::SIMCORE_TOOL.Properties.Resources.Add;
            resources.ApplyResources(this.tsb_AddRow, "tsb_AddRow");
            this.tsb_AddRow.Name = "tsb_AddRow";
            this.tsb_AddRow.Click += new System.EventHandler(this.addNewRowToolStripMenuItem_Click);
            // 
            // tsb_Edit
            // 
            this.tsb_Edit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_Edit.Image = global::SIMCORE_TOOL.Properties.Resources.edit;
            resources.ApplyResources(this.tsb_Edit, "tsb_Edit");
            this.tsb_Edit.Name = "tsb_Edit";
            this.tsb_Edit.Click += new System.EventHandler(this.tsb_Edit_Click);
            // 
            // tsb_DeleteRow
            // 
            this.tsb_DeleteRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_DeleteRow.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            resources.ApplyResources(this.tsb_DeleteRow, "tsb_DeleteRow");
            this.tsb_DeleteRow.Name = "tsb_DeleteRow";
            this.tsb_DeleteRow.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // ouvrirToolStripButton
            // 
            this.ouvrirToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ouvrirToolStripButton.Image = global::SIMCORE_TOOL.Properties.Resources.Open;
            resources.ApplyResources(this.ouvrirToolStripButton, "ouvrirToolStripButton");
            this.ouvrirToolStripButton.Name = "ouvrirToolStripButton";
            this.ouvrirToolStripButton.Click += new System.EventHandler(this.ImportTable_Click);
            // 
            // enregistrerToolStripButton
            // 
            this.enregistrerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.enregistrerToolStripButton.Image = global::SIMCORE_TOOL.Properties.Resources.Save;
            resources.ApplyResources(this.enregistrerToolStripButton, "enregistrerToolStripButton");
            this.enregistrerToolStripButton.Name = "enregistrerToolStripButton";
            this.enregistrerToolStripButton.Click += new System.EventHandler(this.enregistrerToolStripButton_Click);
            // 
            // tsbExcel
            // 
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbExcel, "tsbExcel");
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Click += new System.EventHandler(this.tsbExcel_Click);
            // 
            // PrintPreview
            // 
            this.PrintPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintPreview.Image = global::SIMCORE_TOOL.Properties.Resources.Aperçu;
            resources.ApplyResources(this.PrintPreview, "PrintPreview");
            this.PrintPreview.Name = "PrintPreview";
            this.PrintPreview.Click += new System.EventHandler(this.PrintPreview_Click);
            // 
            // imprimerToolStripButton
            // 
            this.imprimerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.imprimerToolStripButton.Image = global::SIMCORE_TOOL.Properties.Resources.Print;
            resources.ApplyResources(this.imprimerToolStripButton, "imprimerToolStripButton");
            this.imprimerToolStripButton.Name = "imprimerToolStripButton";
            this.imprimerToolStripButton.Click += new System.EventHandler(this.imprimerToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
            // 
            // couperToolStripButton
            // 
            this.couperToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.couperToolStripButton.Image = global::SIMCORE_TOOL.Properties.Resources.Cut;
            resources.ApplyResources(this.couperToolStripButton, "couperToolStripButton");
            this.couperToolStripButton.Name = "couperToolStripButton";
            // 
            // copierToolStripButton
            // 
            this.copierToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copierToolStripButton.Image = global::SIMCORE_TOOL.Properties.Resources.copy;
            resources.ApplyResources(this.copierToolStripButton, "copierToolStripButton");
            this.copierToolStripButton.Name = "copierToolStripButton";
            // 
            // collerToolStripButton
            // 
            this.collerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.collerToolStripButton.Image = global::SIMCORE_TOOL.Properties.Resources.Paste;
            resources.ApplyResources(this.collerToolStripButton, "collerToolStripButton");
            this.collerToolStripButton.Name = "collerToolStripButton";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // CollapseAllColumns
            // 
            this.CollapseAllColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CollapseAllColumns.Image = global::SIMCORE_TOOL.Properties.Resources.Retrecir;
            resources.ApplyResources(this.CollapseAllColumns, "CollapseAllColumns");
            this.CollapseAllColumns.Name = "CollapseAllColumns";
            this.CollapseAllColumns.Click += new System.EventHandler(this.CollapseAllColumn_Click);
            // 
            // ExpandAllColumns
            // 
            this.ExpandAllColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExpandAllColumns.Image = global::SIMCORE_TOOL.Properties.Resources.Elargir;
            resources.ApplyResources(this.ExpandAllColumns, "ExpandAllColumns");
            this.ExpandAllColumns.Name = "ExpandAllColumns";
            this.ExpandAllColumns.Click += new System.EventHandler(this.ExpandAll_Click);
            // 
            // ResizeAllColumns
            // 
            this.ResizeAllColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ResizeAllColumns.Image = global::SIMCORE_TOOL.Properties.Resources.ElargirEntete;
            resources.ApplyResources(this.ResizeAllColumns, "ResizeAllColumns");
            this.ResizeAllColumns.Name = "ResizeAllColumns";
            this.ResizeAllColumns.Click += new System.EventHandler(this.ResizeAll_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // searchTypeToolStripButton
            // 
            this.searchTypeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.searchTypeToolStripButton.Image = global::SIMCORE_TOOL.Properties.Resources.CreateLink;
            resources.ApplyResources(this.searchTypeToolStripButton, "searchTypeToolStripButton");
            this.searchTypeToolStripButton.Name = "searchTypeToolStripButton";
            this.searchTypeToolStripButton.Click += new System.EventHandler(this.searchTypeToolStripButton_Click);
            // 
            // SearchToolStripTextBox
            // 
            resources.ApplyResources(this.SearchToolStripTextBox, "SearchToolStripTextBox");
            this.SearchToolStripTextBox.Name = "SearchToolStripTextBox";
            this.SearchToolStripTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchToolStripTextBox_KeyPress);
            this.SearchToolStripTextBox.TextChanged += new System.EventHandler(this.SearchToolStripTextBox_TextChanged);
            // 
            // searchToolStripButton
            // 
            this.searchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchToolStripButton.Image = global::SIMCORE_TOOL.Properties.Resources.toolbar_find;
            resources.ApplyResources(this.searchToolStripButton, "searchToolStripButton");
            this.searchToolStripButton.Name = "searchToolStripButton";
            this.searchToolStripButton.Click += new System.EventHandler(this.searchToolStripButton_Click);
            // 
            // prevToolStripButton
            // 
            this.prevToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.prevToolStripButton.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_left;
            resources.ApplyResources(this.prevToolStripButton, "prevToolStripButton");
            this.prevToolStripButton.Name = "prevToolStripButton";
            this.prevToolStripButton.Click += new System.EventHandler(this.prevToolStripButton_Click);
            // 
            // nextToolStripButton
            // 
            this.nextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextToolStripButton.Image = global::SIMCORE_TOOL.Properties.Resources.arrow_right;
            resources.ApplyResources(this.nextToolStripButton, "nextToolStripButton");
            this.nextToolStripButton.Name = "nextToolStripButton";
            this.nextToolStripButton.Click += new System.EventHandler(this.nextToolStripButton_Click);
            // 
            // tsl_NumberRows
            // 
            this.tsl_NumberRows.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsl_NumberRows.Name = "tsl_NumberRows";
            resources.ApplyResources(this.tsl_NumberRows, "tsl_NumberRows");
            this.tsl_NumberRows.Click += new System.EventHandler(this.tsl_NumberRows_Click);
            // 
            // tsb_Allocate_CheckIn
            // 
            this.tsb_Allocate_CheckIn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsb_Allocate_CheckIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_Allocate_CheckIn.Image = global::SIMCORE_TOOL.Properties.Resources.Table;
            resources.ApplyResources(this.tsb_Allocate_CheckIn, "tsb_Allocate_CheckIn");
            this.tsb_Allocate_CheckIn.Name = "tsb_Allocate_CheckIn";
            this.tsb_Allocate_CheckIn.Click += new System.EventHandler(this.tsb_Allocate_CheckIn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tstb_Value
            // 
            this.tstb_Value.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.tstb_Value, "tstb_Value");
            this.tstb_Value.Name = "tstb_Value";
            this.tstb_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tstb_Value_KeyDown);
            // 
            // tsb_valid
            // 
            this.tsb_valid.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsb_valid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.tsb_valid, "tsb_valid");
            this.tsb_valid.Name = "tsb_valid";
            this.tsb_valid.Click += new System.EventHandler(this.tsb_valid_Click);
            // 
            // tp_Summary
            // 
            this.tp_Summary.Controls.Add(this.richTextBox1);
            resources.ApplyResources(this.tp_Summary, "tp_Summary");
            this.tp_Summary.Name = "tp_Summary";
            this.tp_Summary.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.Name = "richTextBox1";
            // 
            // TabChart
            // 
            this.TabChart.Controls.Add(this.panel1);
            resources.ApplyResources(this.TabChart, "TabChart");
            this.TabChart.Name = "TabChart";
            this.TabChart.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Graphique);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // Graphique
            // 
            this.Graphique.AutoRefresh = false;
            this.Graphique.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.Graphique, "Graphique");
            this.Graphique.InputKeys = new System.Windows.Forms.Keys[0];
            this.Graphique.Name = "Graphique";
            this.Graphique.State = ((Nevron.Chart.WinForm.NState)(resources.GetObject("Graphique.State")));
            // 
            // tp_BHS
            // 
            resources.ApplyResources(this.tp_BHS, "tp_BHS");
            this.tp_BHS.Name = "tp_BHS";
            this.tp_BHS.UseVisualStyleBackColor = true;
            // 
            // tabSimul8
            // 
            resources.ApplyResources(this.tabSimul8, "tabSimul8");
            this.tabSimul8.Name = "tabSimul8";
            this.tabSimul8.UseVisualStyleBackColor = true;
            // 
            // tabGantt
            // 
            this.tabGantt.Controls.Add(this.axShockwaveFlash1);
            this.tabGantt.Controls.Add(this.ganttTitle);
            resources.ApplyResources(this.tabGantt, "tabGantt");
            this.tabGantt.Name = "tabGantt";
            this.tabGantt.UseVisualStyleBackColor = true;
            // 
            // axShockwaveFlash1
            // 
            resources.ApplyResources(this.axShockwaveFlash1, "axShockwaveFlash1");
            this.axShockwaveFlash1.Name = "axShockwaveFlash1";
            this.axShockwaveFlash1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash1.OcxState")));
            this.axShockwaveFlash1.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(this.player_FlashCall);
            // 
            // ganttTitle
            // 
            resources.ApplyResources(this.ganttTitle, "ganttTitle");
            this.ganttTitle.Name = "ganttTitle";
            // 
            // tabNewItinerary
            // 
            resources.ApplyResources(this.tabNewItinerary, "tabNewItinerary");
            this.tabNewItinerary.Controls.Add(this.shockwaveFlashForItinerary);
            this.tabNewItinerary.Controls.Add(this.axShockwaveFlash3);
            this.tabNewItinerary.Controls.Add(this.axShockwaveFlash2);
            this.tabNewItinerary.Controls.Add(this.testButton);
            this.tabNewItinerary.Name = "tabNewItinerary";
            this.tabNewItinerary.UseVisualStyleBackColor = true;
            // 
            // shockwaveFlashForItinerary
            // 
            resources.ApplyResources(this.shockwaveFlashForItinerary, "shockwaveFlashForItinerary");
            this.shockwaveFlashForItinerary.Name = "shockwaveFlashForItinerary";
            this.shockwaveFlashForItinerary.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("shockwaveFlashForItinerary.OcxState")));
            this.shockwaveFlashForItinerary.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(this.player_FlashCall_ProcessFlow);
            // 
            // axShockwaveFlash3
            // 
            resources.ApplyResources(this.axShockwaveFlash3, "axShockwaveFlash3");
            this.axShockwaveFlash3.Name = "axShockwaveFlash3";
            this.axShockwaveFlash3.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash3.OcxState")));
            // 
            // axShockwaveFlash2
            // 
            resources.ApplyResources(this.axShockwaveFlash2, "axShockwaveFlash2");
            this.axShockwaveFlash2.Name = "axShockwaveFlash2";
            this.axShockwaveFlash2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash2.OcxState")));
            // 
            // testButton
            // 
            resources.ApplyResources(this.testButton, "testButton");
            this.testButton.Name = "testButton";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButtonClick);
            // 
            // tabPageDashboard
            // 
            resources.ApplyResources(this.tabPageDashboard, "tabPageDashboard");
            this.tabPageDashboard.Controls.Add(this.axShockwaveFlashForDashboard);
            this.tabPageDashboard.Name = "tabPageDashboard";
            this.tabPageDashboard.UseVisualStyleBackColor = true;
            // 
            // axShockwaveFlashForDashboard
            // 
            resources.ApplyResources(this.axShockwaveFlashForDashboard, "axShockwaveFlashForDashboard");
            this.axShockwaveFlashForDashboard.Name = "axShockwaveFlashForDashboard";
            this.axShockwaveFlashForDashboard.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlashForDashboard.OcxState")));
            this.axShockwaveFlashForDashboard.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(this.axShockwaveFlashForDashboard_FlashCall);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssb_Perimeter,
            this.toolStripButton1,
            this.AirportDef,
            this.InputData,
            this.StaticAnalysis,
            this.DynamicAnalysis,
            this.tsb_BHS,
            this.tsb_Runway,
            this.tsb_Chart,
            this.Results,
            this.tsbStartSimulation});
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Stretch = true;
            // 
            // tssb_Perimeter
            // 
            this.tssb_Perimeter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tssb_Perimeter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_HUB,
            this.tsmi_PAX,
            this.tmsi_BHS,
            this.tsmi_TMS,
            this.tsmi_AIR,
            this.tsmi_PKG});
            resources.ApplyResources(this.tssb_Perimeter, "tssb_Perimeter");
            this.tssb_Perimeter.Name = "tssb_Perimeter";
            this.tssb_Perimeter.Click += new System.EventHandler(this.tssb_Perimeter_Click);
            // 
            // tsmi_HUB
            // 
            resources.ApplyResources(this.tsmi_HUB, "tsmi_HUB");
            this.tsmi_HUB.Name = "tsmi_HUB";
            this.tsmi_HUB.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmi_PAX
            // 
            resources.ApplyResources(this.tsmi_PAX, "tsmi_PAX");
            this.tsmi_PAX.Name = "tsmi_PAX";
            this.tsmi_PAX.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tmsi_BHS
            // 
            resources.ApplyResources(this.tmsi_BHS, "tmsi_BHS");
            this.tmsi_BHS.Name = "tmsi_BHS";
            this.tmsi_BHS.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmi_TMS
            // 
            resources.ApplyResources(this.tsmi_TMS, "tsmi_TMS");
            this.tsmi_TMS.Name = "tsmi_TMS";
            this.tsmi_TMS.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmi_AIR
            // 
            resources.ApplyResources(this.tsmi_AIR, "tsmi_AIR");
            this.tsmi_AIR.Name = "tsmi_AIR";
            this.tsmi_AIR.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmi_PKG
            // 
            resources.ApplyResources(this.tsmi_PKG, "tsmi_PKG");
            this.tsmi_PKG.Name = "tsmi_PKG";
            this.tsmi_PKG.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::SIMCORE_TOOL.Properties.Resources.PAX2SIM_BoutonProjetRouge_White1;
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.ToolBar1_OpenTool_click);
            // 
            // AirportDef
            // 
            this.AirportDef.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AirportDef.Image = global::SIMCORE_TOOL.Properties.Resources.PAX2SIM_BoutonAirportRouge_White1;
            resources.ApplyResources(this.AirportDef, "AirportDef");
            this.AirportDef.Name = "AirportDef";
            this.AirportDef.Click += new System.EventHandler(this.AirportDef_Click);
            // 
            // InputData
            // 
            this.InputData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InputData.Image = global::SIMCORE_TOOL.Properties.Resources.PAX2SIM_BoutonAirportDataRouge_White;
            resources.ApplyResources(this.InputData, "InputData");
            this.InputData.Name = "InputData";
            this.InputData.Click += new System.EventHandler(this.InputData_Click);
            // 
            // StaticAnalysis
            // 
            this.StaticAnalysis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StaticAnalysis.Image = global::SIMCORE_TOOL.Properties.Resources.PAX2SIM_BoutonStaticRouge_White;
            resources.ApplyResources(this.StaticAnalysis, "StaticAnalysis");
            this.StaticAnalysis.Name = "StaticAnalysis";
            this.StaticAnalysis.Click += new System.EventHandler(this.Analysis_Click);
            // 
            // DynamicAnalysis
            // 
            this.DynamicAnalysis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DynamicAnalysis.Image = global::SIMCORE_TOOL.Properties.Resources.PAX2SIM_BoutonDynamicRouge_White;
            resources.ApplyResources(this.DynamicAnalysis, "DynamicAnalysis");
            this.DynamicAnalysis.Name = "DynamicAnalysis";
            this.DynamicAnalysis.Click += new System.EventHandler(this.Allocate_Click);
            // 
            // tsb_BHS
            // 
            this.tsb_BHS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_BHS.Image = global::SIMCORE_TOOL.Properties.Resources.PAX2SIM_BoutonBHSJaune;
            resources.ApplyResources(this.tsb_BHS, "tsb_BHS");
            this.tsb_BHS.Name = "tsb_BHS";
            this.tsb_BHS.Click += new System.EventHandler(this.tsb_BHS_Click);
            // 
            // tsb_Runway
            // 
            this.tsb_Runway.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_Runway.Image = global::SIMCORE_TOOL.Properties.Resources.PAX2SIM_BoutonAirsideJaune;
            resources.ApplyResources(this.tsb_Runway, "tsb_Runway");
            this.tsb_Runway.Name = "tsb_Runway";
            this.tsb_Runway.Click += new System.EventHandler(this.tsb_Runway_Click);
            // 
            // tsb_Chart
            // 
            this.tsb_Chart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_Chart.Image = global::SIMCORE_TOOL.Properties.Resources.PAX2SIM_BoutonChartRouge_White;
            resources.ApplyResources(this.tsb_Chart, "tsb_Chart");
            this.tsb_Chart.Name = "tsb_Chart";
            this.tsb_Chart.Click += new System.EventHandler(this.ShowAssistant_Click);
            // 
            // Results
            // 
            this.Results.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Results.Image = global::SIMCORE_TOOL.Properties.Resources.PAX2SIM_BoutonReportRouge_White;
            resources.ApplyResources(this.Results, "Results");
            this.Results.Name = "Results";
            this.Results.Click += new System.EventHandler(this.Results_Click);
            // 
            // tsbStartSimulation
            // 
            this.tsbStartSimulation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStartSimulation.Image = global::SIMCORE_TOOL.Properties.Resources.PAX2SIM_BoutonAirsideJaune;
            resources.ApplyResources(this.tsbStartSimulation, "tsbStartSimulation");
            this.tsbStartSimulation.Name = "tsbStartSimulation";
            this.tsbStartSimulation.Click += new System.EventHandler(this.tsbStartSimulation_Click);
            // 
            // menuSIMCORE
            // 
            this.menuSIMCORE.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.menuSIMCORE, "menuSIMCORE");
            this.menuSIMCORE.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuSIMCORE.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuSIMCORE.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lancementEtape2ToolStripMenuItem,
            this.projetToolStripMenuItem,
            this.appearenceToolStripMenuItem,
            this.tsb_Help});
            this.menuSIMCORE.Name = "menuSIMCORE";
            // 
            // lancementEtape2ToolStripMenuItem
            // 
            this.lancementEtape2ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.importProjectToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator3,
            this.closeToolStripMenuItem,
            this.quitterToolStripMenuItem});
            this.lancementEtape2ToolStripMenuItem.Name = "lancementEtape2ToolStripMenuItem";
            resources.ApplyResources(this.lancementEtape2ToolStripMenuItem, "lancementEtape2ToolStripMenuItem");
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.New;
            resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Open;
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // importProjectToolStripMenuItem
            // 
            this.importProjectToolStripMenuItem.Name = "importProjectToolStripMenuItem";
            resources.ApplyResources(this.importProjectToolStripMenuItem, "importProjectToolStripMenuItem");
            this.importProjectToolStripMenuItem.Click += new System.EventHandler(this.importProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Save;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Save;
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            resources.ApplyResources(this.quitterToolStripMenuItem, "quitterToolStripMenuItem");
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // projetToolStripMenuItem
            // 
            this.projetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectNameToolStripMenuItem,
            this.unitsAndSpeedsToolStripMenuItem,
            this.openProjectFolderToolStripMenuItem,
            this.listOfErrorsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.allocateMakeUpToolStripMenuItem,
            this.allocateReclaimToolStripMenuItem,
            this.allocateTransferInfeedToolStripMenuItem,
            this.allocateToolStripMenuItem,
            this.generateFlightPlansToolStripMenuItem,
            this.estimateAirportOccupationToolStripMenuItem,
            this.generateFlightPlansAIAToolStripMenuItem,
            this.importDubaiFlightPlansToolStripMenuItem,
            this.generateFilesForDubaiAllocationToolStripMenuItem,
            this.generateFlightPlanInformationToolStripMenuItem,
            this.importLiegeFlightPlansToolStripMenuItem,
            this.generateLiegeAllocationToolStripMenuItem,
            this.importCDGDepartureFlightPlanToolStripMenuItem,
            this.specificationDocumentToolStripMenuItem});
            this.projetToolStripMenuItem.Name = "projetToolStripMenuItem";
            resources.ApplyResources(this.projetToolStripMenuItem, "projetToolStripMenuItem");
            // 
            // projectNameToolStripMenuItem
            // 
            this.projectNameToolStripMenuItem.Name = "projectNameToolStripMenuItem";
            resources.ApplyResources(this.projectNameToolStripMenuItem, "projectNameToolStripMenuItem");
            this.projectNameToolStripMenuItem.Click += new System.EventHandler(this.nomToolStripMenuItem_Click);
            // 
            // unitsAndSpeedsToolStripMenuItem
            // 
            this.unitsAndSpeedsToolStripMenuItem.Name = "unitsAndSpeedsToolStripMenuItem";
            resources.ApplyResources(this.unitsAndSpeedsToolStripMenuItem, "unitsAndSpeedsToolStripMenuItem");
            this.unitsAndSpeedsToolStripMenuItem.Click += new System.EventHandler(this.unitAndSpeedsToolStripMenuItem_Click);
            // 
            // openProjectFolderToolStripMenuItem
            // 
            this.openProjectFolderToolStripMenuItem.Name = "openProjectFolderToolStripMenuItem";
            resources.ApplyResources(this.openProjectFolderToolStripMenuItem, "openProjectFolderToolStripMenuItem");
            this.openProjectFolderToolStripMenuItem.Click += new System.EventHandler(this.openProjectFolderToolStripMenuItem_Click);
            // 
            // listOfErrorsToolStripMenuItem
            // 
            this.listOfErrorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLatestErrorsToolStripMenuItem,
            this.clearLatestErrorsToolStripMenuItem});
            this.listOfErrorsToolStripMenuItem.Name = "listOfErrorsToolStripMenuItem";
            resources.ApplyResources(this.listOfErrorsToolStripMenuItem, "listOfErrorsToolStripMenuItem");
            // 
            // showLatestErrorsToolStripMenuItem
            // 
            this.showLatestErrorsToolStripMenuItem.Name = "showLatestErrorsToolStripMenuItem";
            resources.ApplyResources(this.showLatestErrorsToolStripMenuItem, "showLatestErrorsToolStripMenuItem");
            this.showLatestErrorsToolStripMenuItem.Click += new System.EventHandler(this.showLatestErrorsToolStripMenuItem_Click);
            // 
            // clearLatestErrorsToolStripMenuItem
            // 
            this.clearLatestErrorsToolStripMenuItem.Name = "clearLatestErrorsToolStripMenuItem";
            resources.ApplyResources(this.clearLatestErrorsToolStripMenuItem, "clearLatestErrorsToolStripMenuItem");
            this.clearLatestErrorsToolStripMenuItem.Click += new System.EventHandler(this.clearLatestErrorsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // allocateMakeUpToolStripMenuItem
            // 
            this.allocateMakeUpToolStripMenuItem.Name = "allocateMakeUpToolStripMenuItem";
            resources.ApplyResources(this.allocateMakeUpToolStripMenuItem, "allocateMakeUpToolStripMenuItem");
            this.allocateMakeUpToolStripMenuItem.Click += new System.EventHandler(this.allocateMakeUpToolStripMenuItem_Click);
            // 
            // allocateReclaimToolStripMenuItem
            // 
            this.allocateReclaimToolStripMenuItem.Name = "allocateReclaimToolStripMenuItem";
            resources.ApplyResources(this.allocateReclaimToolStripMenuItem, "allocateReclaimToolStripMenuItem");
            this.allocateReclaimToolStripMenuItem.Click += new System.EventHandler(this.allocateMakeUpToolStripMenuItem_Click);
            // 
            // allocateTransferInfeedToolStripMenuItem
            // 
            this.allocateTransferInfeedToolStripMenuItem.Name = "allocateTransferInfeedToolStripMenuItem";
            resources.ApplyResources(this.allocateTransferInfeedToolStripMenuItem, "allocateTransferInfeedToolStripMenuItem");
            this.allocateTransferInfeedToolStripMenuItem.Click += new System.EventHandler(this.allocateMakeUpToolStripMenuItem_Click);
            // 
            // allocateToolStripMenuItem
            // 
            this.allocateToolStripMenuItem.Name = "allocateToolStripMenuItem";
            resources.ApplyResources(this.allocateToolStripMenuItem, "allocateToolStripMenuItem");
            this.allocateToolStripMenuItem.Click += new System.EventHandler(this.allocateMakeUpToolStripMenuItem_Click);
            // 
            // generateFlightPlansToolStripMenuItem
            // 
            this.generateFlightPlansToolStripMenuItem.Name = "generateFlightPlansToolStripMenuItem";
            resources.ApplyResources(this.generateFlightPlansToolStripMenuItem, "generateFlightPlansToolStripMenuItem");
            this.generateFlightPlansToolStripMenuItem.Click += new System.EventHandler(this.generateFlightPlansToolStripMenuItem_Click);
            // 
            // estimateAirportOccupationToolStripMenuItem
            // 
            this.estimateAirportOccupationToolStripMenuItem.Name = "estimateAirportOccupationToolStripMenuItem";
            resources.ApplyResources(this.estimateAirportOccupationToolStripMenuItem, "estimateAirportOccupationToolStripMenuItem");
            this.estimateAirportOccupationToolStripMenuItem.Click += new System.EventHandler(this.estimateAirportOccupationToolStripMenuItem_Click);
            // 
            // generateFlightPlansAIAToolStripMenuItem
            // 
            this.generateFlightPlansAIAToolStripMenuItem.Name = "generateFlightPlansAIAToolStripMenuItem";
            resources.ApplyResources(this.generateFlightPlansAIAToolStripMenuItem, "generateFlightPlansAIAToolStripMenuItem");
            this.generateFlightPlansAIAToolStripMenuItem.Click += new System.EventHandler(this.generateFlightPlansAIAToolStripMenuItem_Click);
            // 
            // importDubaiFlightPlansToolStripMenuItem
            // 
            this.importDubaiFlightPlansToolStripMenuItem.Name = "importDubaiFlightPlansToolStripMenuItem";
            resources.ApplyResources(this.importDubaiFlightPlansToolStripMenuItem, "importDubaiFlightPlansToolStripMenuItem");
            this.importDubaiFlightPlansToolStripMenuItem.Click += new System.EventHandler(this.importDubaiFlightPlansToolStripMenuItem_Click);
            // 
            // generateFilesForDubaiAllocationToolStripMenuItem
            // 
            this.generateFilesForDubaiAllocationToolStripMenuItem.Name = "generateFilesForDubaiAllocationToolStripMenuItem";
            resources.ApplyResources(this.generateFilesForDubaiAllocationToolStripMenuItem, "generateFilesForDubaiAllocationToolStripMenuItem");
            this.generateFilesForDubaiAllocationToolStripMenuItem.Click += new System.EventHandler(this.generateFilesForAllocationToolStripMenuItem_Click);
            // 
            // generateFlightPlanInformationToolStripMenuItem
            // 
            this.generateFlightPlanInformationToolStripMenuItem.Name = "generateFlightPlanInformationToolStripMenuItem";
            resources.ApplyResources(this.generateFlightPlanInformationToolStripMenuItem, "generateFlightPlanInformationToolStripMenuItem");
            this.generateFlightPlanInformationToolStripMenuItem.Click += new System.EventHandler(this.generateFlightPlanInformationToolStripMenuItem_Click);
            // 
            // importLiegeFlightPlansToolStripMenuItem
            // 
            this.importLiegeFlightPlansToolStripMenuItem.Name = "importLiegeFlightPlansToolStripMenuItem";
            resources.ApplyResources(this.importLiegeFlightPlansToolStripMenuItem, "importLiegeFlightPlansToolStripMenuItem");
            this.importLiegeFlightPlansToolStripMenuItem.Click += new System.EventHandler(this.importLiegeFlightPlansToolStripMenuItem_Click);
            // 
            // generateLiegeAllocationToolStripMenuItem
            // 
            this.generateLiegeAllocationToolStripMenuItem.Name = "generateLiegeAllocationToolStripMenuItem";
            resources.ApplyResources(this.generateLiegeAllocationToolStripMenuItem, "generateLiegeAllocationToolStripMenuItem");
            this.generateLiegeAllocationToolStripMenuItem.Click += new System.EventHandler(this.generateLiegeAllocationToolStripMenuItem_Click);
            // 
            // importCDGDepartureFlightPlanToolStripMenuItem
            // 
            this.importCDGDepartureFlightPlanToolStripMenuItem.Name = "importCDGDepartureFlightPlanToolStripMenuItem";
            resources.ApplyResources(this.importCDGDepartureFlightPlanToolStripMenuItem, "importCDGDepartureFlightPlanToolStripMenuItem");
            this.importCDGDepartureFlightPlanToolStripMenuItem.Click += new System.EventHandler(this.importCdgDepartureFlightPlansToolStripMenuItem_Click);
            // 
            // specificationDocumentToolStripMenuItem
            // 
            this.specificationDocumentToolStripMenuItem.Name = "specificationDocumentToolStripMenuItem";
            resources.ApplyResources(this.specificationDocumentToolStripMenuItem, "specificationDocumentToolStripMenuItem");
            this.specificationDocumentToolStripMenuItem.Click += new System.EventHandler(this.specificationDocumentToolStripMenuItem_Click);
            // 
            // appearenceToolStripMenuItem
            // 
            this.appearenceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.styleToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.openTemporaryFolderToolStripMenuItem,
            this.openLogToolStripMenuItem});
            this.appearenceToolStripMenuItem.Name = "appearenceToolStripMenuItem";
            resources.ApplyResources(this.appearenceToolStripMenuItem, "appearenceToolStripMenuItem");
            // 
            // styleToolStripMenuItem
            // 
            this.styleToolStripMenuItem.Name = "styleToolStripMenuItem";
            resources.ApplyResources(this.styleToolStripMenuItem, "styleToolStripMenuItem");
            this.styleToolStripMenuItem.Click += new System.EventHandler(this.styleToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frenchToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            // 
            // frenchToolStripMenuItem
            // 
            this.frenchToolStripMenuItem.Name = "frenchToolStripMenuItem";
            resources.ApplyResources(this.frenchToolStripMenuItem, "frenchToolStripMenuItem");
            this.frenchToolStripMenuItem.Click += new System.EventHandler(this.frenchToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // openTemporaryFolderToolStripMenuItem
            // 
            this.openTemporaryFolderToolStripMenuItem.Name = "openTemporaryFolderToolStripMenuItem";
            resources.ApplyResources(this.openTemporaryFolderToolStripMenuItem, "openTemporaryFolderToolStripMenuItem");
            this.openTemporaryFolderToolStripMenuItem.Click += new System.EventHandler(this.openTemporaryFolderToolStripMenuItem_Click);
            // 
            // openLogToolStripMenuItem
            // 
            this.openLogToolStripMenuItem.Name = "openLogToolStripMenuItem";
            resources.ApplyResources(this.openLogToolStripMenuItem, "openLogToolStripMenuItem");
            this.openLogToolStripMenuItem.Click += new System.EventHandler(this.openLogToolStripMenuItem_Click);
            // 
            // tsb_Help
            // 
            this.tsb_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.toolStripMenuItem1,
            this.releaseNotesToolStripMenuItem,
            this.systemRequirementsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem});
            this.tsb_Help.Name = "tsb_Help";
            resources.ApplyResources(this.tsb_Help, "tsb_Help");
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.userManualToolStripMenuItem,
            this.bHSTraceKeywordsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.CheckOnClick = true;
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            resources.ApplyResources(this.helpToolStripMenuItem1, "helpToolStripMenuItem1");
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // userManualToolStripMenuItem
            // 
            this.userManualToolStripMenuItem.Name = "userManualToolStripMenuItem";
            resources.ApplyResources(this.userManualToolStripMenuItem, "userManualToolStripMenuItem");
            this.userManualToolStripMenuItem.Click += new System.EventHandler(this.tsb_Help_Click);
            // 
            // bHSTraceKeywordsToolStripMenuItem
            // 
            this.bHSTraceKeywordsToolStripMenuItem.Name = "bHSTraceKeywordsToolStripMenuItem";
            resources.ApplyResources(this.bHSTraceKeywordsToolStripMenuItem, "bHSTraceKeywordsToolStripMenuItem");
            this.bHSTraceKeywordsToolStripMenuItem.Click += new System.EventHandler(this.bHSTraceKeywordsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // releaseNotesToolStripMenuItem
            // 
            this.releaseNotesToolStripMenuItem.CheckOnClick = true;
            this.releaseNotesToolStripMenuItem.Name = "releaseNotesToolStripMenuItem";
            resources.ApplyResources(this.releaseNotesToolStripMenuItem, "releaseNotesToolStripMenuItem");
            this.releaseNotesToolStripMenuItem.Click += new System.EventHandler(this.releaseNotesToolStripMenuItem_Click);
            // 
            // systemRequirementsToolStripMenuItem
            // 
            this.systemRequirementsToolStripMenuItem.Name = "systemRequirementsToolStripMenuItem";
            resources.ApplyResources(this.systemRequirementsToolStripMenuItem, "systemRequirementsToolStripMenuItem");
            this.systemRequirementsToolStripMenuItem.Click += new System.EventHandler(this.systemRequirementsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aProposToolStripMenuItem_Click);
            // 
            // tp_Itinerary
            // 
            resources.ApplyResources(this.tp_Itinerary, "tp_Itinerary");
            this.tp_Itinerary.Name = "tp_Itinerary";
            this.tp_Itinerary.UseVisualStyleBackColor = true;
            // 
            // tabSimreporter
            // 
            this.tabSimreporter.Controls.Add(this.simreporterLaunchButton);
            this.tabSimreporter.Controls.Add(this.simreporterWebBrowser);
            resources.ApplyResources(this.tabSimreporter, "tabSimreporter");
            this.tabSimreporter.Name = "tabSimreporter";
            this.tabSimreporter.UseVisualStyleBackColor = true;
            // 
            // simreporterLaunchButton
            // 
            resources.ApplyResources(this.simreporterLaunchButton, "simreporterLaunchButton");
            this.simreporterLaunchButton.Name = "simreporterLaunchButton";
            this.simreporterLaunchButton.UseVisualStyleBackColor = true;
            this.simreporterLaunchButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // simreporterWebBrowser
            // 
            resources.ApplyResources(this.simreporterWebBrowser, "simreporterWebBrowser");
            this.simreporterWebBrowser.Name = "simreporterWebBrowser";
            // 
            // contextMenuAirport
            // 
            this.contextMenuAirport.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuAirport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.New,
            this.Edit,
            this.Delete,
            this.tsmi_Properties,
            this.importFromFPToolStripMenuItem,
            this.showGraphToolStripMenuItem});
            this.contextMenuAirport.Name = "contextMenuAirport";
            resources.ApplyResources(this.contextMenuAirport, "contextMenuAirport");
            this.contextMenuAirport.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuAirport_Opening);
            // 
            // New
            // 
            resources.ApplyResources(this.New, "New");
            this.New.Name = "New";
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // Edit
            // 
            this.Edit.Image = global::SIMCORE_TOOL.Properties.Resources.edit;
            this.Edit.Name = "Edit";
            resources.ApplyResources(this.Edit, "Edit");
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // Delete
            // 
            resources.ApplyResources(this.Delete, "Delete");
            this.Delete.Name = "Delete";
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // tsmi_Properties
            // 
            this.tsmi_Properties.Name = "tsmi_Properties";
            resources.ApplyResources(this.tsmi_Properties, "tsmi_Properties");
            this.tsmi_Properties.Click += new System.EventHandler(this.tsmi_Properties_Click);
            // 
            // importFromFPToolStripMenuItem
            // 
            this.importFromFPToolStripMenuItem.Name = "importFromFPToolStripMenuItem";
            resources.ApplyResources(this.importFromFPToolStripMenuItem, "importFromFPToolStripMenuItem");
            // 
            // showGraphToolStripMenuItem
            // 
            this.showGraphToolStripMenuItem.Name = "showGraphToolStripMenuItem";
            resources.ApplyResources(this.showGraphToolStripMenuItem, "showGraphToolStripMenuItem");
            // 
            // contextMenuInput
            // 
            this.contextMenuInput.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDefaultTableToolStripMenuItem,
            this.EditTable,
            this.ImportTable,
            this.importFromExistingTableToolStripMenuItem,
            this.createFilterToolStripMenuItem,
            this.InputContextMenuPasteFilter,
            this.deleteTableToolStripMenuItem,
            this.copyGraphicDefinitionToolStripMenuItem,
            this.pasteGraphicDefinitionToolStripMenuItem,
            this.addToChartTableToolStripMenuItem,
            this.setTargetToolStripMenuItem,
            this.deleteTargetTableToolStripMenuItem,
            this.viewStatisticsToolStripRightClickMenuItem,
            this.setCountingParametersToolStripMenuItem,
            this.deleteCountResultTableToolStripMenuItem,
            this.deleteISTCountedItemsTableToolStripMenuItem,
            this.deleteStatisticsToolStripMenuItem,
            this.navigateToDeskFromTableToolStripMenuItem,
            this.applyColorCodeOnGenericTableToolStripMenuItem,
            this.toolStripMenuItemCopyTableName});
            this.contextMenuInput.Name = "contextMenuInput";
            resources.ApplyResources(this.contextMenuInput, "contextMenuInput");
            this.contextMenuInput.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuInput_Opening);
            // 
            // loadDefaultTableToolStripMenuItem
            // 
            this.loadDefaultTableToolStripMenuItem.Name = "loadDefaultTableToolStripMenuItem";
            resources.ApplyResources(this.loadDefaultTableToolStripMenuItem, "loadDefaultTableToolStripMenuItem");
            this.loadDefaultTableToolStripMenuItem.Click += new System.EventHandler(this.loadDefaultTableToolStripMenuItem_Click);
            // 
            // EditTable
            // 
            resources.ApplyResources(this.EditTable, "EditTable");
            this.EditTable.Image = global::SIMCORE_TOOL.Properties.Resources.edit;
            this.EditTable.Name = "EditTable";
            this.EditTable.Click += new System.EventHandler(this.EditTable_Click);
            // 
            // ImportTable
            // 
            this.ImportTable.Name = "ImportTable";
            resources.ApplyResources(this.ImportTable, "ImportTable");
            this.ImportTable.Click += new System.EventHandler(this.ImportTable_Click);
            // 
            // importFromExistingTableToolStripMenuItem
            // 
            this.importFromExistingTableToolStripMenuItem.Name = "importFromExistingTableToolStripMenuItem";
            resources.ApplyResources(this.importFromExistingTableToolStripMenuItem, "importFromExistingTableToolStripMenuItem");
            this.importFromExistingTableToolStripMenuItem.Click += new System.EventHandler(this.importFromExistingTableToolStripMenuItem_Click);
            // 
            // createFilterToolStripMenuItem
            // 
            resources.ApplyResources(this.createFilterToolStripMenuItem, "createFilterToolStripMenuItem");
            this.createFilterToolStripMenuItem.Name = "createFilterToolStripMenuItem";
            this.createFilterToolStripMenuItem.Click += new System.EventHandler(this.createFilterToolStripMenuItem_Click);
            // 
            // InputContextMenuPasteFilter
            // 
            resources.ApplyResources(this.InputContextMenuPasteFilter, "InputContextMenuPasteFilter");
            this.InputContextMenuPasteFilter.Name = "InputContextMenuPasteFilter";
            this.InputContextMenuPasteFilter.Click += new System.EventHandler(this.contextMenuFilterPaste_Click);
            // 
            // deleteTableToolStripMenuItem
            // 
            this.deleteTableToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.deleteTableToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            resources.ApplyResources(this.deleteTableToolStripMenuItem, "deleteTableToolStripMenuItem");
            this.deleteTableToolStripMenuItem.Name = "deleteTableToolStripMenuItem";
            this.deleteTableToolStripMenuItem.Click += new System.EventHandler(this.deleteTableToolStripMenuItem_Click);
            // 
            // copyGraphicDefinitionToolStripMenuItem
            // 
            this.copyGraphicDefinitionToolStripMenuItem.Name = "copyGraphicDefinitionToolStripMenuItem";
            resources.ApplyResources(this.copyGraphicDefinitionToolStripMenuItem, "copyGraphicDefinitionToolStripMenuItem");
            this.copyGraphicDefinitionToolStripMenuItem.Click += new System.EventHandler(this.copyGraphicDefinitionToolStripMenuItem_Click);
            // 
            // pasteGraphicDefinitionToolStripMenuItem
            // 
            this.pasteGraphicDefinitionToolStripMenuItem.Name = "pasteGraphicDefinitionToolStripMenuItem";
            resources.ApplyResources(this.pasteGraphicDefinitionToolStripMenuItem, "pasteGraphicDefinitionToolStripMenuItem");
            this.pasteGraphicDefinitionToolStripMenuItem.Click += new System.EventHandler(this.pasteGraphicDefinitionToolStripMenuItem_Click);
            // 
            // addToChartTableToolStripMenuItem
            // 
            this.addToChartTableToolStripMenuItem.Name = "addToChartTableToolStripMenuItem";
            resources.ApplyResources(this.addToChartTableToolStripMenuItem, "addToChartTableToolStripMenuItem");
            // 
            // setTargetToolStripMenuItem
            // 
            this.setTargetToolStripMenuItem.Name = "setTargetToolStripMenuItem";
            resources.ApplyResources(this.setTargetToolStripMenuItem, "setTargetToolStripMenuItem");
            this.setTargetToolStripMenuItem.Click += new System.EventHandler(this.setTargetToolStripMenuItem_Click);
            // 
            // deleteTargetTableToolStripMenuItem
            // 
            this.deleteTargetTableToolStripMenuItem.Name = "deleteTargetTableToolStripMenuItem";
            resources.ApplyResources(this.deleteTargetTableToolStripMenuItem, "deleteTargetTableToolStripMenuItem");
            this.deleteTargetTableToolStripMenuItem.Click += new System.EventHandler(this.deleteTargetTableToolStripMenuItem_Click);
            // 
            // viewStatisticsToolStripRightClickMenuItem
            // 
            this.viewStatisticsToolStripRightClickMenuItem.Name = "viewStatisticsToolStripRightClickMenuItem";
            resources.ApplyResources(this.viewStatisticsToolStripRightClickMenuItem, "viewStatisticsToolStripRightClickMenuItem");
            this.viewStatisticsToolStripRightClickMenuItem.Click += new System.EventHandler(this.viesStatisticsToolStripRightClickMenuItem_Click);
            // 
            // setCountingParametersToolStripMenuItem
            // 
            resources.ApplyResources(this.setCountingParametersToolStripMenuItem, "setCountingParametersToolStripMenuItem");
            this.setCountingParametersToolStripMenuItem.Name = "setCountingParametersToolStripMenuItem";
            this.setCountingParametersToolStripMenuItem.Click += new System.EventHandler(this.setCountingParametersToolStripMenuItem_Click);
            // 
            // deleteCountResultTableToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteCountResultTableToolStripMenuItem, "deleteCountResultTableToolStripMenuItem");
            this.deleteCountResultTableToolStripMenuItem.Name = "deleteCountResultTableToolStripMenuItem";
            this.deleteCountResultTableToolStripMenuItem.Click += new System.EventHandler(this.deleteCountResultTableToolStripMenuItem_Click);
            // 
            // deleteISTCountedItemsTableToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteISTCountedItemsTableToolStripMenuItem, "deleteISTCountedItemsTableToolStripMenuItem");
            this.deleteISTCountedItemsTableToolStripMenuItem.Name = "deleteISTCountedItemsTableToolStripMenuItem";
            this.deleteISTCountedItemsTableToolStripMenuItem.Click += new System.EventHandler(this.deleteISTCountedItemsTableToolStripMenuItem_Click);
            // 
            // deleteStatisticsToolStripMenuItem
            // 
            this.deleteStatisticsToolStripMenuItem.Name = "deleteStatisticsToolStripMenuItem";
            resources.ApplyResources(this.deleteStatisticsToolStripMenuItem, "deleteStatisticsToolStripMenuItem");
            this.deleteStatisticsToolStripMenuItem.Click += new System.EventHandler(this.deleteViewStatisticsToolStripMenuItem_Click);
            // 
            // navigateToDeskFromTableToolStripMenuItem
            // 
            this.navigateToDeskFromTableToolStripMenuItem.Name = "navigateToDeskFromTableToolStripMenuItem";
            resources.ApplyResources(this.navigateToDeskFromTableToolStripMenuItem, "navigateToDeskFromTableToolStripMenuItem");
            this.navigateToDeskFromTableToolStripMenuItem.Click += new System.EventHandler(this.navigateToDeskFromTreeNodeToolStripMenuItem_Click);
            // 
            // applyColorCodeOnGenericTableToolStripMenuItem
            // 
            this.applyColorCodeOnGenericTableToolStripMenuItem.Name = "applyColorCodeOnGenericTableToolStripMenuItem";
            resources.ApplyResources(this.applyColorCodeOnGenericTableToolStripMenuItem, "applyColorCodeOnGenericTableToolStripMenuItem");
            this.applyColorCodeOnGenericTableToolStripMenuItem.Click += new System.EventHandler(this.applyColorCodeOnGenericTableToolStripMenuItem_Click);
            // 
            // toolStripMenuItemCopyTableName
            // 
            this.toolStripMenuItemCopyTableName.Name = "toolStripMenuItemCopyTableName";
            resources.ApplyResources(this.toolStripMenuItemCopyTableName, "toolStripMenuItemCopyTableName");
            this.toolStripMenuItemCopyTableName.Click += new System.EventHandler(this.toolStripMenuItemCopyTableName_Click);
            // 
            // addToChartFilterToolStripMenuItem
            // 
            this.addToChartFilterToolStripMenuItem.Name = "addToChartFilterToolStripMenuItem";
            resources.ApplyResources(this.addToChartFilterToolStripMenuItem, "addToChartFilterToolStripMenuItem");
            // 
            // notifyPRJ4C
            // 
            resources.ApplyResources(this.notifyPRJ4C, "notifyPRJ4C");
            this.notifyPRJ4C.ContextMenuStrip = this.cms_NotifyMenu;
            this.notifyPRJ4C.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyPRJ4C_MouseClick);
            // 
            // cms_NotifyMenu
            // 
            this.cms_NotifyMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cms_NotifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Pax2SimBtn});
            this.cms_NotifyMenu.Name = "cms_NotifyMenu";
            resources.ApplyResources(this.cms_NotifyMenu, "cms_NotifyMenu");
            // 
            // tsmi_Pax2SimBtn
            // 
            resources.ApplyResources(this.tsmi_Pax2SimBtn, "tsmi_Pax2SimBtn");
            this.tsmi_Pax2SimBtn.Name = "tsmi_Pax2SimBtn";
            this.tsmi_Pax2SimBtn.Click += new System.EventHandler(this.tsmi_Pax2SimBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuDataView
            // 
            this.contextMenuDataView.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuDataView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewRowToolStripMenuItem,
            this.editCellToolStripMenuItem,
            this.editColumnToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteRowToolStripMenuItem,
            this.importTableToolStripMenuItem,
            this.navigateToDeskFromDatagridToolStripMenuItem});
            this.contextMenuDataView.Name = "contextMenuDataView";
            resources.ApplyResources(this.contextMenuDataView, "contextMenuDataView");
            this.contextMenuDataView.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuDataView_Opening);
            // 
            // addNewRowToolStripMenuItem
            // 
            this.addNewRowToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Add;
            resources.ApplyResources(this.addNewRowToolStripMenuItem, "addNewRowToolStripMenuItem");
            this.addNewRowToolStripMenuItem.Name = "addNewRowToolStripMenuItem";
            this.addNewRowToolStripMenuItem.Click += new System.EventHandler(this.addNewRowToolStripMenuItem_Click);
            // 
            // editCellToolStripMenuItem
            // 
            this.editCellToolStripMenuItem.Name = "editCellToolStripMenuItem";
            resources.ApplyResources(this.editCellToolStripMenuItem, "editCellToolStripMenuItem");
            this.editCellToolStripMenuItem.Click += new System.EventHandler(this.editCellToolStripMenuItem_Click);
            // 
            // editColumnToolStripMenuItem
            // 
            this.editColumnToolStripMenuItem.Name = "editColumnToolStripMenuItem";
            resources.ApplyResources(this.editColumnToolStripMenuItem, "editColumnToolStripMenuItem");
            this.editColumnToolStripMenuItem.Click += new System.EventHandler(this.editColumnToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteRowToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteRowToolStripMenuItem, "deleteRowToolStripMenuItem");
            this.deleteRowToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // importTableToolStripMenuItem
            // 
            this.importTableToolStripMenuItem.Name = "importTableToolStripMenuItem";
            resources.ApplyResources(this.importTableToolStripMenuItem, "importTableToolStripMenuItem");
            this.importTableToolStripMenuItem.Click += new System.EventHandler(this.ImportTable_Click);
            // 
            // navigateToDeskFromDatagridToolStripMenuItem
            // 
            this.navigateToDeskFromDatagridToolStripMenuItem.Name = "navigateToDeskFromDatagridToolStripMenuItem";
            resources.ApplyResources(this.navigateToDeskFromDatagridToolStripMenuItem, "navigateToDeskFromDatagridToolStripMenuItem");
            this.navigateToDeskFromDatagridToolStripMenuItem.Click += new System.EventHandler(this.navigateToDeskFromDatagridToolStripMenuItem_Click);
            // 
            // nChartCommandBarsManager1
            // 
            this.nChartCommandBarsManager1.AllowCustomize = false;
            this.nChartCommandBarsManager1.AutoRefresh = true;
            this.nChartCommandBarsManager1.ChartControl = this.Graphique;
            nChartCommander1.ChartControl = this.Graphique;
            nChartCommander1.ChartSizeStep = 5F;
            nChartCommander1.ElevationStep = 10F;
            nChartCommander1.HorizontalMoveStep = ((Nevron.GraphicsCore.NLength)(resources.GetObject("nChartCommander1.HorizontalMoveStep")));
            nChartCommander1.RotationStep = 10F;
            nChartCommander1.VerticalMoveStep = ((Nevron.GraphicsCore.NLength)(resources.GetObject("nChartCommander1.VerticalMoveStep")));
            nChartCommander1.ZoomStep = 5F;
            this.nChartCommandBarsManager1.Commander = nChartCommander1;
            this.nChartCommandBarsManager1.Palette.CheckedDark = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(221)))), ((int)(((byte)(242)))));
            this.nChartCommandBarsManager1.Palette.CheckedLight = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(221)))), ((int)(((byte)(242)))));
            this.nChartCommandBarsManager1.Palette.Control = System.Drawing.SystemColors.Control;
            this.nChartCommandBarsManager1.Palette.ControlBorder = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.nChartCommandBarsManager1.Palette.ControlDark = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(211)))), ((int)(((byte)(205)))));
            this.nChartCommandBarsManager1.Palette.ControlLight = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.nChartCommandBarsManager1.Palette.Highlight = System.Drawing.SystemColors.Highlight;
            this.nChartCommandBarsManager1.Palette.HighlightDark = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(196)))), ((int)(((byte)(232)))));
            this.nChartCommandBarsManager1.Palette.HighlightLight = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(196)))), ((int)(((byte)(232)))));
            this.nChartCommandBarsManager1.Palette.HighlightText = System.Drawing.SystemColors.HighlightText;
            this.nChartCommandBarsManager1.Palette.PressedDark = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(210)))), ((int)(((byte)(238)))));
            this.nChartCommandBarsManager1.Palette.PressedLight = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(210)))), ((int)(((byte)(238)))));
            this.nChartCommandBarsManager1.Palette.SelectedBorder = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(35)))), ((int)(((byte)(66)))));
            this.nChartCommandBarsManager1.Palette.Window = System.Drawing.SystemColors.Window;
            this.nChartCommandBarsManager1.ParentControl = this.TabChart;
            this.nChartCommandBarsManager1.RefreshInterval = 300;
            this.nChartCommandBarsManager1.ToolbarsBuilder = nChartToolbarCommandBuilder1;
            // 
            // ContextMenuFilter
            // 
            this.ContextMenuFilter.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ContextMenuFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuFilterCreate,
            this.ContextMenuFilterEdit,
            this.ContextMenuFilterCut,
            this.contextMenuFilterCopy,
            this.ContextMenuFilterPaste,
            this.ContextMenuFilterRemove,
            this.copyGraphicDefinitionToolStripMenuItem1,
            this.pasteGraphicDefinitionToolStripMenuItem1,
            this.addToChartFilterToolStripMenuItem,
            this.viewFilterStatisticsToolStripRightClickMenuItem,
            this.navigateToDeskFromFilterToolStripMenuItem,
            this.generateTextGanttFromFilterToolStripMenuItem,
            this.applyColorCodeOnFilterToolStripMenuItem,
            this.copyTableNameToolStripMenuItem});
            this.ContextMenuFilter.Name = "ContextMenuFilter";
            resources.ApplyResources(this.ContextMenuFilter, "ContextMenuFilter");
            this.ContextMenuFilter.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuFilter_Opening);
            // 
            // ContextMenuFilterCreate
            // 
            this.ContextMenuFilterCreate.Image = global::SIMCORE_TOOL.Properties.Resources.Design;
            resources.ApplyResources(this.ContextMenuFilterCreate, "ContextMenuFilterCreate");
            this.ContextMenuFilterCreate.Name = "ContextMenuFilterCreate";
            this.ContextMenuFilterCreate.Click += new System.EventHandler(this.createFilterToolStripMenuItem_Click);
            // 
            // ContextMenuFilterEdit
            // 
            this.ContextMenuFilterEdit.Image = global::SIMCORE_TOOL.Properties.Resources.edit;
            this.ContextMenuFilterEdit.Name = "ContextMenuFilterEdit";
            resources.ApplyResources(this.ContextMenuFilterEdit, "ContextMenuFilterEdit");
            this.ContextMenuFilterEdit.Click += new System.EventHandler(this.ContextMenuFilterEdit_Click);
            // 
            // ContextMenuFilterCut
            // 
            this.ContextMenuFilterCut.Image = global::SIMCORE_TOOL.Properties.Resources.Cut;
            resources.ApplyResources(this.ContextMenuFilterCut, "ContextMenuFilterCut");
            this.ContextMenuFilterCut.Name = "ContextMenuFilterCut";
            this.ContextMenuFilterCut.Click += new System.EventHandler(this.ContextMenuFilterCut_Click);
            // 
            // contextMenuFilterCopy
            // 
            this.contextMenuFilterCopy.Image = global::SIMCORE_TOOL.Properties.Resources.copy;
            resources.ApplyResources(this.contextMenuFilterCopy, "contextMenuFilterCopy");
            this.contextMenuFilterCopy.Name = "contextMenuFilterCopy";
            this.contextMenuFilterCopy.Click += new System.EventHandler(this.contextMenuFilterCopy_Click);
            // 
            // ContextMenuFilterPaste
            // 
            this.ContextMenuFilterPaste.Image = global::SIMCORE_TOOL.Properties.Resources.Paste;
            resources.ApplyResources(this.ContextMenuFilterPaste, "ContextMenuFilterPaste");
            this.ContextMenuFilterPaste.Name = "ContextMenuFilterPaste";
            this.ContextMenuFilterPaste.Click += new System.EventHandler(this.contextMenuFilterPaste_Click);
            // 
            // ContextMenuFilterRemove
            // 
            this.ContextMenuFilterRemove.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            resources.ApplyResources(this.ContextMenuFilterRemove, "ContextMenuFilterRemove");
            this.ContextMenuFilterRemove.Name = "ContextMenuFilterRemove";
            this.ContextMenuFilterRemove.Click += new System.EventHandler(this.ContextMenuFilterRemove_Click);
            // 
            // copyGraphicDefinitionToolStripMenuItem1
            // 
            this.copyGraphicDefinitionToolStripMenuItem1.Name = "copyGraphicDefinitionToolStripMenuItem1";
            resources.ApplyResources(this.copyGraphicDefinitionToolStripMenuItem1, "copyGraphicDefinitionToolStripMenuItem1");
            this.copyGraphicDefinitionToolStripMenuItem1.Click += new System.EventHandler(this.copyGraphicDefinitionToolStripMenuItem_Click);
            // 
            // pasteGraphicDefinitionToolStripMenuItem1
            // 
            this.pasteGraphicDefinitionToolStripMenuItem1.Name = "pasteGraphicDefinitionToolStripMenuItem1";
            resources.ApplyResources(this.pasteGraphicDefinitionToolStripMenuItem1, "pasteGraphicDefinitionToolStripMenuItem1");
            this.pasteGraphicDefinitionToolStripMenuItem1.Click += new System.EventHandler(this.pasteGraphicDefinitionToolStripMenuItem_Click);
            // 
            // viewFilterStatisticsToolStripRightClickMenuItem
            // 
            this.viewFilterStatisticsToolStripRightClickMenuItem.Name = "viewFilterStatisticsToolStripRightClickMenuItem";
            resources.ApplyResources(this.viewFilterStatisticsToolStripRightClickMenuItem, "viewFilterStatisticsToolStripRightClickMenuItem");
            this.viewFilterStatisticsToolStripRightClickMenuItem.Click += new System.EventHandler(this.viesStatisticsToolStripRightClickMenuItem_Click);
            // 
            // navigateToDeskFromFilterToolStripMenuItem
            // 
            this.navigateToDeskFromFilterToolStripMenuItem.Name = "navigateToDeskFromFilterToolStripMenuItem";
            resources.ApplyResources(this.navigateToDeskFromFilterToolStripMenuItem, "navigateToDeskFromFilterToolStripMenuItem");
            this.navigateToDeskFromFilterToolStripMenuItem.Click += new System.EventHandler(this.navigateToDeskFromTreeNodeToolStripMenuItem_Click);
            // 
            // generateTextGanttFromFilterToolStripMenuItem
            // 
            this.generateTextGanttFromFilterToolStripMenuItem.Name = "generateTextGanttFromFilterToolStripMenuItem";
            resources.ApplyResources(this.generateTextGanttFromFilterToolStripMenuItem, "generateTextGanttFromFilterToolStripMenuItem");
            this.generateTextGanttFromFilterToolStripMenuItem.Click += new System.EventHandler(this.generateTextGanttFromFilterToolStripMenuItem_Click);
            // 
            // applyColorCodeOnFilterToolStripMenuItem
            // 
            this.applyColorCodeOnFilterToolStripMenuItem.Name = "applyColorCodeOnFilterToolStripMenuItem";
            resources.ApplyResources(this.applyColorCodeOnFilterToolStripMenuItem, "applyColorCodeOnFilterToolStripMenuItem");
            this.applyColorCodeOnFilterToolStripMenuItem.Click += new System.EventHandler(this.applyColorCodeOnFilterToolStripMenuItem_Click);
            // 
            // copyTableNameToolStripMenuItem
            // 
            this.copyTableNameToolStripMenuItem.Name = "copyTableNameToolStripMenuItem";
            resources.ApplyResources(this.copyTableNameToolStripMenuItem, "copyTableNameToolStripMenuItem");
            this.copyTableNameToolStripMenuItem.Click += new System.EventHandler(this.copyTableNameToolStripMenuItem_Click);
            // 
            // IL_ImagesToolbar
            // 
            this.IL_ImagesToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IL_ImagesToolbar.ImageStream")));
            this.IL_ImagesToolbar.TransparentColor = System.Drawing.Color.White;
            this.IL_ImagesToolbar.Images.SetKeyName(0, "Baguette.bmp");
            this.IL_ImagesToolbar.Images.SetKeyName(1, "Mouse.bmp");
            this.IL_ImagesToolbar.Images.SetKeyName(2, "AxisManagement.bmp");
            this.IL_ImagesToolbar.Images.SetKeyName(3, "Police.bmp");
            this.IL_ImagesToolbar.Images.SetKeyName(4, "legend.bmp");
            this.IL_ImagesToolbar.Images.SetKeyName(5, "camembert.bmp");
            this.IL_ImagesToolbar.Images.SetKeyName(6, "Delete.bmp");
            this.IL_ImagesToolbar.Images.SetKeyName(7, "RoundedRectAnnotation.bmp");
            this.IL_ImagesToolbar.Images.SetKeyName(8, "suppressAnnotation.bmp");
            this.IL_ImagesToolbar.Images.SetKeyName(9, "Setpoint.bmp");
            // 
            // cmsBranchTestMenu
            // 
            this.cmsBranchTestMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsBranchTestMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiProperties,
            this.analyzeBHSResultsToolStripMenuItem,
            this.tsmiDelete,
            this.tsmiCopy,
            this.reloadToolStripMenuItem,
            this.exportAsTextFilesToolStripMenuItem});
            this.cmsBranchTestMenu.Name = "contextMenuStrip1";
            resources.ApplyResources(this.cmsBranchTestMenu, "cmsBranchTestMenu");
            this.cmsBranchTestMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsBranchTestMenu_Opening);
            // 
            // tsmiProperties
            // 
            this.tsmiProperties.Name = "tsmiProperties";
            resources.ApplyResources(this.tsmiProperties, "tsmiProperties");
            this.tsmiProperties.Click += new System.EventHandler(this.tsmiProperties_Click);
            // 
            // analyzeBHSResultsToolStripMenuItem
            // 
            this.analyzeBHSResultsToolStripMenuItem.Name = "analyzeBHSResultsToolStripMenuItem";
            resources.ApplyResources(this.analyzeBHSResultsToolStripMenuItem, "analyzeBHSResultsToolStripMenuItem");
            this.analyzeBHSResultsToolStripMenuItem.Click += new System.EventHandler(this.tmsi_analyseBHSResults_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            resources.ApplyResources(this.tsmiDelete, "tsmiDelete");
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Image = global::SIMCORE_TOOL.Properties.Resources.copy;
            resources.ApplyResources(this.tsmiCopy, "tsmiCopy");
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            resources.ApplyResources(this.reloadToolStripMenuItem, "reloadToolStripMenuItem");
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.tsmiReload_Click);
            // 
            // exportAsTextFilesToolStripMenuItem
            // 
            this.exportAsTextFilesToolStripMenuItem.Name = "exportAsTextFilesToolStripMenuItem";
            resources.ApplyResources(this.exportAsTextFilesToolStripMenuItem, "exportAsTextFilesToolStripMenuItem");
            this.exportAsTextFilesToolStripMenuItem.Click += new System.EventHandler(this.exportAsTextFilesToolStripMenuItem_Click);
            // 
            // cmsCharts
            // 
            this.cmsCharts.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsCharts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addChartToolStripMenuItem,
            this.editChartToolStripMenuItem,
            this.deleteChartToolStripMenuItem,
            this.createFilterToolStripMenuItem2,
            this.pasteFilterToolStripMenuItem1,
            this.copyChartDefinitionToolStripMenuItem,
            this.pasteChartDefinitionToolStripMenuItem,
            this.manageChartLinksToolStripMenuItem});
            this.cmsCharts.Name = "cmsCharts";
            resources.ApplyResources(this.cmsCharts, "cmsCharts");
            this.cmsCharts.Opening += new System.ComponentModel.CancelEventHandler(this.cmsCharts_Opening);
            // 
            // addChartToolStripMenuItem
            // 
            this.addChartToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Add;
            this.addChartToolStripMenuItem.Name = "addChartToolStripMenuItem";
            resources.ApplyResources(this.addChartToolStripMenuItem, "addChartToolStripMenuItem");
            this.addChartToolStripMenuItem.Click += new System.EventHandler(this.addChartToolStripMenuItem_Click);
            // 
            // editChartToolStripMenuItem
            // 
            this.editChartToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.edit;
            this.editChartToolStripMenuItem.Name = "editChartToolStripMenuItem";
            resources.ApplyResources(this.editChartToolStripMenuItem, "editChartToolStripMenuItem");
            this.editChartToolStripMenuItem.Click += new System.EventHandler(this.ShowAssistant_Click);
            // 
            // deleteChartToolStripMenuItem
            // 
            this.deleteChartToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            this.deleteChartToolStripMenuItem.Name = "deleteChartToolStripMenuItem";
            resources.ApplyResources(this.deleteChartToolStripMenuItem, "deleteChartToolStripMenuItem");
            this.deleteChartToolStripMenuItem.Click += new System.EventHandler(this.deleteChartToolStripMenuItem_Click);
            // 
            // createFilterToolStripMenuItem2
            // 
            this.createFilterToolStripMenuItem2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.createFilterToolStripMenuItem2.Image = global::SIMCORE_TOOL.Properties.Resources.Design;
            this.createFilterToolStripMenuItem2.Name = "createFilterToolStripMenuItem2";
            resources.ApplyResources(this.createFilterToolStripMenuItem2, "createFilterToolStripMenuItem2");
            this.createFilterToolStripMenuItem2.Click += new System.EventHandler(this.createFilterToolStripMenuItem2_Click);
            // 
            // pasteFilterToolStripMenuItem1
            // 
            this.pasteFilterToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pasteFilterToolStripMenuItem1.Image = global::SIMCORE_TOOL.Properties.Resources.Paste;
            this.pasteFilterToolStripMenuItem1.Name = "pasteFilterToolStripMenuItem1";
            resources.ApplyResources(this.pasteFilterToolStripMenuItem1, "pasteFilterToolStripMenuItem1");
            this.pasteFilterToolStripMenuItem1.Click += new System.EventHandler(this.pasteFilterToolStripMenuItem1_Click);
            // 
            // copyChartDefinitionToolStripMenuItem
            // 
            this.copyChartDefinitionToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.copy;
            this.copyChartDefinitionToolStripMenuItem.Name = "copyChartDefinitionToolStripMenuItem";
            resources.ApplyResources(this.copyChartDefinitionToolStripMenuItem, "copyChartDefinitionToolStripMenuItem");
            this.copyChartDefinitionToolStripMenuItem.Click += new System.EventHandler(this.copyGlobalChartDefinitionClick);
            // 
            // pasteChartDefinitionToolStripMenuItem
            // 
            this.pasteChartDefinitionToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Paste;
            this.pasteChartDefinitionToolStripMenuItem.Name = "pasteChartDefinitionToolStripMenuItem";
            resources.ApplyResources(this.pasteChartDefinitionToolStripMenuItem, "pasteChartDefinitionToolStripMenuItem");
            this.pasteChartDefinitionToolStripMenuItem.Click += new System.EventHandler(this.pasteGlobalChartDefinitionClick);
            // 
            // manageChartLinksToolStripMenuItem
            // 
            this.manageChartLinksToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.CreateLink3;
            this.manageChartLinksToolStripMenuItem.Name = "manageChartLinksToolStripMenuItem";
            resources.ApplyResources(this.manageChartLinksToolStripMenuItem, "manageChartLinksToolStripMenuItem");
            this.manageChartLinksToolStripMenuItem.Click += new System.EventHandler(this.manageChartLinksToolStripMenuItem_Click);
            // 
            // cmsUserData
            // 
            this.cmsUserData.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsUserData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_AddUserData,
            this.tsmi_DeleteUserData});
            this.cmsUserData.Name = "cmsUserData";
            resources.ApplyResources(this.cmsUserData, "cmsUserData");
            this.cmsUserData.Opening += new System.ComponentModel.CancelEventHandler(this.cmsUserData_Opening);
            // 
            // tsmi_AddUserData
            // 
            this.tsmi_AddUserData.Image = global::SIMCORE_TOOL.Properties.Resources.Add;
            resources.ApplyResources(this.tsmi_AddUserData, "tsmi_AddUserData");
            this.tsmi_AddUserData.Name = "tsmi_AddUserData";
            this.tsmi_AddUserData.Click += new System.EventHandler(this.tsmi_AddUserData_Click);
            // 
            // tsmi_DeleteUserData
            // 
            this.tsmi_DeleteUserData.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            resources.ApplyResources(this.tsmi_DeleteUserData, "tsmi_DeleteUserData");
            this.tsmi_DeleteUserData.Name = "tsmi_DeleteUserData";
            this.tsmi_DeleteUserData.Click += new System.EventHandler(this.tsmi_DeleteUserData_Click);
            // 
            // cms_AnalyzeBHSResults
            // 
            this.cms_AnalyzeBHSResults.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cms_AnalyzeBHSResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsi_analyzeBHSResults,
            this.launchMultipleScenariosToolStripMenuItem});
            this.cms_AnalyzeBHSResults.Name = "cms_AnalyzeBHSResults";
            resources.ApplyResources(this.cms_AnalyzeBHSResults, "cms_AnalyzeBHSResults");
            // 
            // tmsi_analyzeBHSResults
            // 
            this.tmsi_analyzeBHSResults.Name = "tmsi_analyzeBHSResults";
            resources.ApplyResources(this.tmsi_analyzeBHSResults, "tmsi_analyzeBHSResults");
            this.tmsi_analyzeBHSResults.Click += new System.EventHandler(this.tmsi_analyseBHSResults_Click);
            // 
            // launchMultipleScenariosToolStripMenuItem
            // 
            this.launchMultipleScenariosToolStripMenuItem.Name = "launchMultipleScenariosToolStripMenuItem";
            resources.ApplyResources(this.launchMultipleScenariosToolStripMenuItem, "launchMultipleScenariosToolStripMenuItem");
            this.launchMultipleScenariosToolStripMenuItem.Click += new System.EventHandler(this.launchMultipleScenariosToolStripMenuItem_Click);
            // 
            // cms_AllocationMenu
            // 
            this.cms_AllocationMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cms_AllocationMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.cms_AllocationMenu.Name = "cms_AllocationMenu";
            resources.ApplyResources(this.cms_AllocationMenu, "cms_AllocationMenu");
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // cmsAutomodMenu
            // 
            this.cmsAutomodMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsAutomodMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCustomGraphicToolStripMenuItem,
            this.addCustomGraphToolStripMenuItem});
            this.cmsAutomodMenu.Name = "cmsAutomodMenu";
            resources.ApplyResources(this.cmsAutomodMenu, "cmsAutomodMenu");
            // 
            // addCustomGraphicToolStripMenuItem
            // 
            this.addCustomGraphicToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Add;
            resources.ApplyResources(this.addCustomGraphicToolStripMenuItem, "addCustomGraphicToolStripMenuItem");
            this.addCustomGraphicToolStripMenuItem.Name = "addCustomGraphicToolStripMenuItem";
            this.addCustomGraphicToolStripMenuItem.Click += new System.EventHandler(this.addCustomGraphicToolStripMenuItem_Click);
            // 
            // addCustomGraphToolStripMenuItem
            // 
            this.addCustomGraphToolStripMenuItem.Name = "addCustomGraphToolStripMenuItem";
            resources.ApplyResources(this.addCustomGraphToolStripMenuItem, "addCustomGraphToolStripMenuItem");
            this.addCustomGraphToolStripMenuItem.Click += new System.EventHandler(this.addCustomGraphToolStripMenuItem_Click);
            // 
            // cms_UserGraphics
            // 
            this.cms_UserGraphics.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cms_UserGraphics.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshDataToolStripMenuItem,
            this.importFromExistingTableToolStripMenuItem1,
            this.editColumnsNameToolStripMenuItem,
            this.createFilterToolStripMenuItem1,
            this.pasteFilterToolStripMenuItem,
            this.deleteToolStripMenuItem1,
            this.copyUserGraphicsChartDefinitionToolStripMenuItem,
            this.pasteUserGraphicsChartDefinitionToolStripMenuItem,
            this.generateTextGanttFromTableToolStripMenuItem,
            this.applyColorCodeOnTableToolStripMenuItem});
            this.cms_UserGraphics.Name = "cms_UserGraphics";
            resources.ApplyResources(this.cms_UserGraphics, "cms_UserGraphics");
            this.cms_UserGraphics.Opening += new System.ComponentModel.CancelEventHandler(this.cms_UserGraphics_Opening);
            // 
            // refreshDataToolStripMenuItem
            // 
            this.refreshDataToolStripMenuItem.Name = "refreshDataToolStripMenuItem";
            resources.ApplyResources(this.refreshDataToolStripMenuItem, "refreshDataToolStripMenuItem");
            this.refreshDataToolStripMenuItem.Click += new System.EventHandler(this.refreshDataToolStripMenuItem_Click);
            // 
            // importFromExistingTableToolStripMenuItem1
            // 
            this.importFromExistingTableToolStripMenuItem1.Name = "importFromExistingTableToolStripMenuItem1";
            resources.ApplyResources(this.importFromExistingTableToolStripMenuItem1, "importFromExistingTableToolStripMenuItem1");
            // 
            // editColumnsNameToolStripMenuItem
            // 
            this.editColumnsNameToolStripMenuItem.Name = "editColumnsNameToolStripMenuItem";
            resources.ApplyResources(this.editColumnsNameToolStripMenuItem, "editColumnsNameToolStripMenuItem");
            this.editColumnsNameToolStripMenuItem.Click += new System.EventHandler(this.editColumnsNameToolStripMenuItem_Click);
            // 
            // createFilterToolStripMenuItem1
            // 
            this.createFilterToolStripMenuItem1.Image = global::SIMCORE_TOOL.Properties.Resources.Design;
            resources.ApplyResources(this.createFilterToolStripMenuItem1, "createFilterToolStripMenuItem1");
            this.createFilterToolStripMenuItem1.Name = "createFilterToolStripMenuItem1";
            this.createFilterToolStripMenuItem1.Click += new System.EventHandler(this.createFilterToolStripMenuItem_Click);
            // 
            // pasteFilterToolStripMenuItem
            // 
            this.pasteFilterToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Paste;
            resources.ApplyResources(this.pasteFilterToolStripMenuItem, "pasteFilterToolStripMenuItem");
            this.pasteFilterToolStripMenuItem.Name = "pasteFilterToolStripMenuItem";
            this.pasteFilterToolStripMenuItem.Click += new System.EventHandler(this.contextMenuFilterPaste_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            resources.ApplyResources(this.deleteToolStripMenuItem1, "deleteToolStripMenuItem1");
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // copyUserGraphicsChartDefinitionToolStripMenuItem
            // 
            this.copyUserGraphicsChartDefinitionToolStripMenuItem.Name = "copyUserGraphicsChartDefinitionToolStripMenuItem";
            resources.ApplyResources(this.copyUserGraphicsChartDefinitionToolStripMenuItem, "copyUserGraphicsChartDefinitionToolStripMenuItem");
            this.copyUserGraphicsChartDefinitionToolStripMenuItem.Click += new System.EventHandler(this.copyGraphicDefinitionToolStripMenuItem_Click);
            // 
            // pasteUserGraphicsChartDefinitionToolStripMenuItem
            // 
            this.pasteUserGraphicsChartDefinitionToolStripMenuItem.Name = "pasteUserGraphicsChartDefinitionToolStripMenuItem";
            resources.ApplyResources(this.pasteUserGraphicsChartDefinitionToolStripMenuItem, "pasteUserGraphicsChartDefinitionToolStripMenuItem");
            this.pasteUserGraphicsChartDefinitionToolStripMenuItem.Click += new System.EventHandler(this.pasteGraphicDefinitionToolStripMenuItem_Click);
            // 
            // generateTextGanttFromTableToolStripMenuItem
            // 
            this.generateTextGanttFromTableToolStripMenuItem.Name = "generateTextGanttFromTableToolStripMenuItem";
            resources.ApplyResources(this.generateTextGanttFromTableToolStripMenuItem, "generateTextGanttFromTableToolStripMenuItem");
            this.generateTextGanttFromTableToolStripMenuItem.Click += new System.EventHandler(this.generateTextGanttFromTableToolStripMenuItem_Click);
            // 
            // applyColorCodeOnTableToolStripMenuItem
            // 
            this.applyColorCodeOnTableToolStripMenuItem.Name = "applyColorCodeOnTableToolStripMenuItem";
            resources.ApplyResources(this.applyColorCodeOnTableToolStripMenuItem, "applyColorCodeOnTableToolStripMenuItem");
            this.applyColorCodeOnTableToolStripMenuItem.Click += new System.EventHandler(this.applyColorCodeOnTableToolStripMenuItem_Click);
            // 
            // cmsStatistics
            // 
            this.cmsStatistics.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsStatistics.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewStatisticsToolStripMenuItem});
            this.cmsStatistics.Name = "cmsStatistics";
            resources.ApplyResources(this.cmsStatistics, "cmsStatistics");
            // 
            // viewStatisticsToolStripMenuItem
            // 
            this.viewStatisticsToolStripMenuItem.Name = "viewStatisticsToolStripMenuItem";
            resources.ApplyResources(this.viewStatisticsToolStripMenuItem, "viewStatisticsToolStripMenuItem");
            this.viewStatisticsToolStripMenuItem.Click += new System.EventHandler(this.viewStatisticsToolStripMenuItem_Click);
            // 
            // cms_ColumnHeader
            // 
            this.cms_ColumnHeader.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cms_ColumnHeader.Name = "cms_UserColumns";
            resources.ApplyResources(this.cms_ColumnHeader, "cms_ColumnHeader");
            // 
            // cmsExceptionTable
            // 
            this.cmsExceptionTable.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsExceptionTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditExceptionTable,
            this.tsmiDeleteExceptionTable});
            this.cmsExceptionTable.Name = "cmsExceptionTable";
            resources.ApplyResources(this.cmsExceptionTable, "cmsExceptionTable");
            this.cmsExceptionTable.Opening += new System.ComponentModel.CancelEventHandler(this.cmsExceptionTable_Opening);
            // 
            // tsmiEditExceptionTable
            // 
            this.tsmiEditExceptionTable.Name = "tsmiEditExceptionTable";
            resources.ApplyResources(this.tsmiEditExceptionTable, "tsmiEditExceptionTable");
            this.tsmiEditExceptionTable.Click += new System.EventHandler(this.tsmiEditExceptionTable_Click);
            // 
            // tsmiDeleteExceptionTable
            // 
            this.tsmiDeleteExceptionTable.Name = "tsmiDeleteExceptionTable";
            resources.ApplyResources(this.tsmiDeleteExceptionTable, "tsmiDeleteExceptionTable");
            this.tsmiDeleteExceptionTable.Click += new System.EventHandler(this.tsmiDeleteExceptionTable_Click);
            // 
            // cmsReports
            // 
            this.cmsReports.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsReports.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expandCollapseToolStripMenuItem,
            this.addReportToolStripMenuItem,
            this.editReportToolStripMenuItem,
            this.assignToReportGroupsToolStripMenuItem,
            this.generateReportGroupToolStripMenuItem,
            this.deleteReportToolStripMenuItem,
            this.previewReportToolStripMenuItem,
            this.generateReportToolStripMenuItem,
            this.editNoteToolStripMenuItem,
            this.navigateToSourceToolStripMenuItem,
            this.manageLinksToolStripMenuItem,
            this.refreshMainReportsNodeToolStripMenuItem,
            this.exportAsTextFilesFromReportToolStripMenuItem,
            this.exportXMLReportConfigurationToolStripMenuItem,
            this.applyColorCodeFromReportToolStripMenuItem});
            this.cmsReports.Name = "cmsReports";
            resources.ApplyResources(this.cmsReports, "cmsReports");
            this.cmsReports.Opening += new System.ComponentModel.CancelEventHandler(this.cmsReports_Opening);
            // 
            // expandCollapseToolStripMenuItem
            // 
            this.expandCollapseToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.expandCollapse16x16;
            this.expandCollapseToolStripMenuItem.Name = "expandCollapseToolStripMenuItem";
            resources.ApplyResources(this.expandCollapseToolStripMenuItem, "expandCollapseToolStripMenuItem");
            this.expandCollapseToolStripMenuItem.Click += new System.EventHandler(this.expandCollapseToolStripMenuItem_Click);
            // 
            // addReportToolStripMenuItem
            // 
            this.addReportToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Add;
            this.addReportToolStripMenuItem.Name = "addReportToolStripMenuItem";
            resources.ApplyResources(this.addReportToolStripMenuItem, "addReportToolStripMenuItem");
            this.addReportToolStripMenuItem.Click += new System.EventHandler(this.addReportToolStripMenuItem_Click);
            // 
            // editReportToolStripMenuItem
            // 
            this.editReportToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Open;
            this.editReportToolStripMenuItem.Name = "editReportToolStripMenuItem";
            resources.ApplyResources(this.editReportToolStripMenuItem, "editReportToolStripMenuItem");
            this.editReportToolStripMenuItem.Click += new System.EventHandler(this.editReportToolStripMenuItem_Click);
            // 
            // assignToReportGroupsToolStripMenuItem
            // 
            resources.ApplyResources(this.assignToReportGroupsToolStripMenuItem, "assignToReportGroupsToolStripMenuItem");
            this.assignToReportGroupsToolStripMenuItem.Name = "assignToReportGroupsToolStripMenuItem";
            this.assignToReportGroupsToolStripMenuItem.Click += new System.EventHandler(this.assignToReportGroupsToolStripMenuItem_Click);
            // 
            // generateReportGroupToolStripMenuItem
            // 
            this.generateReportGroupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateReportGroupAsPdfToolStripMenuItem,
            this.generateReportGroupAsHtmlToolStripMenuItem});
            this.generateReportGroupToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.clipboard;
            this.generateReportGroupToolStripMenuItem.Name = "generateReportGroupToolStripMenuItem";
            resources.ApplyResources(this.generateReportGroupToolStripMenuItem, "generateReportGroupToolStripMenuItem");
            // 
            // generateReportGroupAsPdfToolStripMenuItem
            // 
            this.generateReportGroupAsPdfToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.pdf_icon;
            this.generateReportGroupAsPdfToolStripMenuItem.Name = "generateReportGroupAsPdfToolStripMenuItem";
            resources.ApplyResources(this.generateReportGroupAsPdfToolStripMenuItem, "generateReportGroupAsPdfToolStripMenuItem");
            // 
            // generateReportGroupAsHtmlToolStripMenuItem
            // 
            this.generateReportGroupAsHtmlToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.htmlFiletype;
            this.generateReportGroupAsHtmlToolStripMenuItem.Name = "generateReportGroupAsHtmlToolStripMenuItem";
            resources.ApplyResources(this.generateReportGroupAsHtmlToolStripMenuItem, "generateReportGroupAsHtmlToolStripMenuItem");
            // 
            // deleteReportToolStripMenuItem
            // 
            this.deleteReportToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            this.deleteReportToolStripMenuItem.Name = "deleteReportToolStripMenuItem";
            resources.ApplyResources(this.deleteReportToolStripMenuItem, "deleteReportToolStripMenuItem");
            this.deleteReportToolStripMenuItem.Click += new System.EventHandler(this.deleteReportToolStripMenuItem_Click);
            // 
            // previewReportToolStripMenuItem
            // 
            this.previewReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pdfFormatPreviewToolStripMenuItem,
            this.htmlFormatPreviewFormatToolStripMenuItem});
            this.previewReportToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Aperçu;
            this.previewReportToolStripMenuItem.Name = "previewReportToolStripMenuItem";
            resources.ApplyResources(this.previewReportToolStripMenuItem, "previewReportToolStripMenuItem");
            // 
            // pdfFormatPreviewToolStripMenuItem
            // 
            this.pdfFormatPreviewToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.pdf_icon;
            this.pdfFormatPreviewToolStripMenuItem.Name = "pdfFormatPreviewToolStripMenuItem";
            resources.ApplyResources(this.pdfFormatPreviewToolStripMenuItem, "pdfFormatPreviewToolStripMenuItem");
            this.pdfFormatPreviewToolStripMenuItem.Click += new System.EventHandler(this.pdfFormatPreviewToolStripMenuItem_Click);
            // 
            // htmlFormatPreviewFormatToolStripMenuItem
            // 
            this.htmlFormatPreviewFormatToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.htmlFiletype;
            this.htmlFormatPreviewFormatToolStripMenuItem.Name = "htmlFormatPreviewFormatToolStripMenuItem";
            resources.ApplyResources(this.htmlFormatPreviewFormatToolStripMenuItem, "htmlFormatPreviewFormatToolStripMenuItem");
            this.htmlFormatPreviewFormatToolStripMenuItem.Click += new System.EventHandler(this.htmlFormatPreviewFormatToolStripMenuItem_Click);
            // 
            // generateReportToolStripMenuItem
            // 
            this.generateReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pdfFormatGeneratorToolStripMenuItem,
            this.htmlFormatGeneratorToolStripMenuItem1});
            this.generateReportToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Chip;
            this.generateReportToolStripMenuItem.Name = "generateReportToolStripMenuItem";
            resources.ApplyResources(this.generateReportToolStripMenuItem, "generateReportToolStripMenuItem");
            // 
            // pdfFormatGeneratorToolStripMenuItem
            // 
            this.pdfFormatGeneratorToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.pdf_icon;
            this.pdfFormatGeneratorToolStripMenuItem.Name = "pdfFormatGeneratorToolStripMenuItem";
            resources.ApplyResources(this.pdfFormatGeneratorToolStripMenuItem, "pdfFormatGeneratorToolStripMenuItem");
            this.pdfFormatGeneratorToolStripMenuItem.Click += new System.EventHandler(this.pdfFormatGeneratorToolStripMenuItem_Click);
            // 
            // htmlFormatGeneratorToolStripMenuItem1
            // 
            this.htmlFormatGeneratorToolStripMenuItem1.Image = global::SIMCORE_TOOL.Properties.Resources.htmlFiletype;
            this.htmlFormatGeneratorToolStripMenuItem1.Name = "htmlFormatGeneratorToolStripMenuItem1";
            resources.ApplyResources(this.htmlFormatGeneratorToolStripMenuItem1, "htmlFormatGeneratorToolStripMenuItem1");
            this.htmlFormatGeneratorToolStripMenuItem1.Click += new System.EventHandler(this.htmlFormatGeneratorToolStripMenuItem1_Click);
            // 
            // editNoteToolStripMenuItem
            // 
            resources.ApplyResources(this.editNoteToolStripMenuItem, "editNoteToolStripMenuItem");
            this.editNoteToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.edit;
            this.editNoteToolStripMenuItem.Name = "editNoteToolStripMenuItem";
            this.editNoteToolStripMenuItem.Click += new System.EventHandler(this.editNoteToolStripMenuItem_Click);
            // 
            // navigateToSourceToolStripMenuItem
            // 
            this.navigateToSourceToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.left;
            this.navigateToSourceToolStripMenuItem.Name = "navigateToSourceToolStripMenuItem";
            resources.ApplyResources(this.navigateToSourceToolStripMenuItem, "navigateToSourceToolStripMenuItem");
            this.navigateToSourceToolStripMenuItem.Click += new System.EventHandler(this.navigateToSourceToolStripMenuItem_Click);
            // 
            // manageLinksToolStripMenuItem
            // 
            this.manageLinksToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.CreateLink3;
            this.manageLinksToolStripMenuItem.Name = "manageLinksToolStripMenuItem";
            resources.ApplyResources(this.manageLinksToolStripMenuItem, "manageLinksToolStripMenuItem");
            this.manageLinksToolStripMenuItem.Click += new System.EventHandler(this.manageLinksToolStripMenuItem_Click);
            // 
            // refreshMainReportsNodeToolStripMenuItem
            // 
            this.refreshMainReportsNodeToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.refresh;
            this.refreshMainReportsNodeToolStripMenuItem.Name = "refreshMainReportsNodeToolStripMenuItem";
            resources.ApplyResources(this.refreshMainReportsNodeToolStripMenuItem, "refreshMainReportsNodeToolStripMenuItem");
            this.refreshMainReportsNodeToolStripMenuItem.Click += new System.EventHandler(this.refreshMainReportsNodeToolStripMenuItem_Click);
            // 
            // exportAsTextFilesFromReportToolStripMenuItem
            // 
            this.exportAsTextFilesFromReportToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.exportAsTextFilesFromReportToolStripMenuItem, "exportAsTextFilesFromReportToolStripMenuItem");
            this.exportAsTextFilesFromReportToolStripMenuItem.Name = "exportAsTextFilesFromReportToolStripMenuItem";
            this.exportAsTextFilesFromReportToolStripMenuItem.Click += new System.EventHandler(this.exportAsTextFilesFromReportToolStripMenuItem_Click);
            // 
            // exportXMLReportConfigurationToolStripMenuItem
            // 
            resources.ApplyResources(this.exportXMLReportConfigurationToolStripMenuItem, "exportXMLReportConfigurationToolStripMenuItem");
            this.exportXMLReportConfigurationToolStripMenuItem.Name = "exportXMLReportConfigurationToolStripMenuItem";
            this.exportXMLReportConfigurationToolStripMenuItem.Click += new System.EventHandler(this.exportXMLReportConfigurationToolStripMenuItem_Click);
            // 
            // applyColorCodeFromReportToolStripMenuItem
            // 
            this.applyColorCodeFromReportToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.paint_brush_white_16;
            this.applyColorCodeFromReportToolStripMenuItem.Name = "applyColorCodeFromReportToolStripMenuItem";
            resources.ApplyResources(this.applyColorCodeFromReportToolStripMenuItem, "applyColorCodeFromReportToolStripMenuItem");
            this.applyColorCodeFromReportToolStripMenuItem.Click += new System.EventHandler(this.applyColorCodeFromReportToolStripMenuItem_Click);
            // 
            // exportScenarioTablesContextMenuStrip
            // 
            this.exportScenarioTablesContextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.exportScenarioTablesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAsTextFilesToolStripMenuItem1});
            this.exportScenarioTablesContextMenuStrip.Name = "exportScenarioTablesContextMenuStrip";
            resources.ApplyResources(this.exportScenarioTablesContextMenuStrip, "exportScenarioTablesContextMenuStrip");
            this.exportScenarioTablesContextMenuStrip.Click += new System.EventHandler(this.exportAsTextFilesToolStripMenuItem_Click);
            // 
            // exportAsTextFilesToolStripMenuItem1
            // 
            this.exportAsTextFilesToolStripMenuItem1.Name = "exportAsTextFilesToolStripMenuItem1";
            resources.ApplyResources(this.exportAsTextFilesToolStripMenuItem1, "exportAsTextFilesToolStripMenuItem1");
            // 
            // cmsDocuments
            // 
            this.cmsDocuments.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsDocuments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDocumentsFolderToolStripMenuItem,
            this.addDocumentMenuItem,
            this.removeAllDocumenstMenuItem});
            this.cmsDocuments.Name = "cmsDocuments";
            resources.ApplyResources(this.cmsDocuments, "cmsDocuments");
            // 
            // openDocumentsFolderToolStripMenuItem
            // 
            this.openDocumentsFolderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFromProjectToolStripMenuItem,
            this.openToolStripMenuItem1});
            this.openDocumentsFolderToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Open;
            this.openDocumentsFolderToolStripMenuItem.Name = "openDocumentsFolderToolStripMenuItem";
            resources.ApplyResources(this.openDocumentsFolderToolStripMenuItem, "openDocumentsFolderToolStripMenuItem");
            this.openDocumentsFolderToolStripMenuItem.Click += new System.EventHandler(this.openFromProjectToolStripMenuItem_Click);
            // 
            // openFromProjectToolStripMenuItem
            // 
            this.openFromProjectToolStripMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Open;
            this.openFromProjectToolStripMenuItem.Name = "openFromProjectToolStripMenuItem";
            resources.ApplyResources(this.openFromProjectToolStripMenuItem, "openFromProjectToolStripMenuItem");
            this.openFromProjectToolStripMenuItem.Click += new System.EventHandler(this.openFromProjectToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Image = global::SIMCORE_TOOL.Properties.Resources.Open;
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            resources.ApplyResources(this.openToolStripMenuItem1, "openToolStripMenuItem1");
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // addDocumentMenuItem
            // 
            this.addDocumentMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Add;
            this.addDocumentMenuItem.Name = "addDocumentMenuItem";
            resources.ApplyResources(this.addDocumentMenuItem, "addDocumentMenuItem");
            this.addDocumentMenuItem.Click += new System.EventHandler(this.addDocumentMenuItem_Click);
            // 
            // removeAllDocumenstMenuItem
            // 
            this.removeAllDocumenstMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            this.removeAllDocumenstMenuItem.Name = "removeAllDocumenstMenuItem";
            resources.ApplyResources(this.removeAllDocumenstMenuItem, "removeAllDocumenstMenuItem");
            this.removeAllDocumenstMenuItem.Click += new System.EventHandler(this.removeAllDocumentsMenuItem_Click);
            // 
            // cmsDocument
            // 
            this.cmsDocument.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsDocument.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDocumentMenuItem,
            this.updateDocumentMenuItem,
            this.removeDocumentMenuItem});
            this.cmsDocument.Name = "cmsDocument";
            resources.ApplyResources(this.cmsDocument, "cmsDocument");
            // 
            // openDocumentMenuItem
            // 
            this.openDocumentMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Open;
            this.openDocumentMenuItem.Name = "openDocumentMenuItem";
            resources.ApplyResources(this.openDocumentMenuItem, "openDocumentMenuItem");
            this.openDocumentMenuItem.Click += new System.EventHandler(this.openDocumentMenuItem_Click);
            // 
            // updateDocumentMenuItem
            // 
            this.updateDocumentMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.refresh;
            this.updateDocumentMenuItem.Name = "updateDocumentMenuItem";
            resources.ApplyResources(this.updateDocumentMenuItem, "updateDocumentMenuItem");
            this.updateDocumentMenuItem.Click += new System.EventHandler(this.updateDocumentMenuItem_Click);
            // 
            // removeDocumentMenuItem
            // 
            this.removeDocumentMenuItem.Image = global::SIMCORE_TOOL.Properties.Resources.Delete;
            this.removeDocumentMenuItem.Name = "removeDocumentMenuItem";
            resources.ApplyResources(this.removeDocumentMenuItem, "removeDocumentMenuItem");
            this.removeDocumentMenuItem.Click += new System.EventHandler(this.removeDocumentMenuItem_Click);
            // 
            // PAX2SIM
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuSIMCORE;
            this.Name = "PAX2SIM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PAX2SIM_FormClosing);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Pax2Sim_MouseWheel);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.sc_Main_Tree.Panel1.ResumeLayout(false);
            this.sc_Main_Tree.Panel2.ResumeLayout(false);
            this.sc_Main_Tree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.automaticUpdater1)).EndInit();
            this.sc_Content_Help.Panel1.ResumeLayout(false);
            this.sc_Content_Help.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.TabViewTable.ResumeLayout(false);
            this.TabViewTable.PerformLayout();
            this.panel_Table.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tp_Summary.ResumeLayout(false);
            this.TabChart.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabGantt.ResumeLayout(false);
            this.tabGantt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).EndInit();
            this.tabNewItinerary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.shockwaveFlashForItinerary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash2)).EndInit();
            this.tabPageDashboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlashForDashboard)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuSIMCORE.ResumeLayout(false);
            this.menuSIMCORE.PerformLayout();
            this.tabSimreporter.ResumeLayout(false);
            this.contextMenuAirport.ResumeLayout(false);
            this.contextMenuInput.ResumeLayout(false);
            this.cms_NotifyMenu.ResumeLayout(false);
            this.contextMenuDataView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nChartCommandBarsManager1)).EndInit();
            this.ContextMenuFilter.ResumeLayout(false);
            this.cmsBranchTestMenu.ResumeLayout(false);
            this.cmsCharts.ResumeLayout(false);
            this.cmsUserData.ResumeLayout(false);
            this.cms_AnalyzeBHSResults.ResumeLayout(false);
            this.cms_AllocationMenu.ResumeLayout(false);
            this.cmsAutomodMenu.ResumeLayout(false);
            this.cms_UserGraphics.ResumeLayout(false);
            this.cmsStatistics.ResumeLayout(false);
            this.cmsExceptionTable.ResumeLayout(false);
            this.cmsReports.ResumeLayout(false);
            this.exportScenarioTablesContextMenuStrip.ResumeLayout(false);
            this.cmsDocuments.ResumeLayout(false);
            this.cmsDocument.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuSIMCORE;
        private System.Windows.Forms.NotifyIcon notifyPRJ4C;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer sc_Main_Tree;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton AirportDef;
        private System.Windows.Forms.ToolStripButton InputData;
        private System.Windows.Forms.ToolStripButton StaticAnalysis;
        private System.Windows.Forms.ToolStripButton DynamicAnalysis;
        private System.Windows.Forms.ToolStripButton Results;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem lancementEtape2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuAirport;
        private System.Windows.Forms.ToolStripMenuItem New;
        private System.Windows.Forms.ToolStripMenuItem Edit;
        private System.Windows.Forms.ToolStripMenuItem Delete;
        private System.Windows.Forms.ContextMenuStrip contextMenuInput;
        private System.Windows.Forms.ToolStripMenuItem EditTable;
        private System.Windows.Forms.ToolStripMenuItem ImportTable;
        private System.Windows.Forms.ToolStripMenuItem importFromFPToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuDataView;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectNameToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage TabChart;
        private System.Windows.Forms.ToolStripMenuItem showGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createFilterToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private Nevron.Chart.WinForm.NChartControl Graphique;
        private Nevron.Chart.WinForm.NChartCommandBarsManager nChartCommandBarsManager1;
        private System.Windows.Forms.ContextMenuStrip ContextMenuFilter;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFilterEdit;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFilterRemove;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFilterCreate;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList IL_ImagesToolbar;
        private System.Windows.Forms.ToolStripMenuItem contextMenuFilterCopy;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFilterPaste;
        private System.Windows.Forms.ToolStripMenuItem InputContextMenuPasteFilter;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFilterCut;
        private System.Windows.Forms.ToolStripMenuItem addNewRowToolStripMenuItem;
        private ToolStripMenuItem appearenceToolStripMenuItem;
        private ToolStripMenuItem styleToolStripMenuItem;
        private ToolStripMenuItem editCellToolStripMenuItem;
        private FolderBrowserDialog fbd_SelectDirectory;
        private TabPage TabViewTable;
        private DataGridView dataGridView1;
        private ContextMenuStrip cmsBranchTestMenu;
        private ToolStripMenuItem tsmiProperties;
        private ToolStripMenuItem tsmiDelete;
        private ToolStripMenuItem tsmiCopy;
        private ToolStripButton tsbStartSimulation;
        private TabPage tp_Itinerary;
        private ToolStrip toolStrip2;
        private ToolStripButton ouvrirToolStripButton;
        private ToolStripButton enregistrerToolStripButton;
        private ToolStripButton imprimerToolStripButton;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripButton couperToolStripButton;
        private ToolStripButton copierToolStripButton;
        private ToolStripButton collerToolStripButton;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton CollapseAllColumns;
        private ToolStripButton ExpandAllColumns;
        private ToolStripButton ResizeAllColumns;
        private Panel panel_Table;
        private ToolStripButton PrintPreview;
        private ToolStripMenuItem editColumnToolStripMenuItem;
        private ContextMenuStrip cmsCharts;
        private ToolStripMenuItem addChartToolStripMenuItem;
        private ToolStripMenuItem deleteChartToolStripMenuItem;
        private ToolStripMenuItem editChartToolStripMenuItem;
        private ToolStripButton tsb_BHS;
        private ToolStripButton tsb_Runway;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripTextBox tstb_Value;
        private ToolStripButton tsb_valid;
        private ContextMenuStrip cms_NotifyMenu;
        private ToolStripMenuItem tsmi_Pax2SimBtn;
        private ToolStripButton tsb_Chart;
        private TabPage tp_Summary;
        private RichTextBox richTextBox1;
        private ToolStripButton tsb_AddRow;
        private ToolStripButton tsb_DeleteRow;
        private ToolStripButton tsb_Edit;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem tsb_Help;
        private ToolStripMenuItem importProjectToolStripMenuItem;
        private ToolStripMenuItem loadDefaultTableToolStripMenuItem;
        private ToolStripMenuItem importFromExistingTableToolStripMenuItem;
        private ToolStripSplitButton tssb_Perimeter;
        private ToolStripButton tsb_Allocate_CheckIn;
        private ToolStripMenuItem tsmi_Properties;
        private ToolStripMenuItem tsmi_HUB;
        private ToolStripMenuItem tsmi_PAX;
        private ToolStripMenuItem tmsi_BHS;
        private ToolStripMenuItem tsmi_AIR;
        private ToolStripMenuItem tsmi_TMS;
        private ToolStripMenuItem unitsAndSpeedsToolStripMenuItem;
        private ToolStripLabel tsl_NumberRows;
        private ContextMenuStrip cmsUserData;
        private ToolStripMenuItem tsmi_AddUserData;
        private ToolStripMenuItem tsmi_DeleteUserData;
        private ContextMenuStrip cms_AnalyzeBHSResults;
        private ToolStripMenuItem tmsi_analyzeBHSResults;
        private ToolStripMenuItem allocateMakeUpToolStripMenuItem;
        private ContextMenuStrip cms_AllocationMenu;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ContextMenuStrip cmsAutomodMenu;
        private ToolStripMenuItem addCustomGraphicToolStripMenuItem;
        private ToolStripButton tsbExcel;
        private ToolStripMenuItem allocateReclaimToolStripMenuItem;
        private ToolStripMenuItem allocateTransferInfeedToolStripMenuItem;
        private ToolStripMenuItem analyzeBHSResultsToolStripMenuItem;
        private ToolStripMenuItem deleteTableToolStripMenuItem;
        private TabPage tp_BHS;
        private ContextMenuStrip cms_UserGraphics;
        private ToolStripMenuItem refreshDataToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem1;
        private ToolStripMenuItem importFromExistingTableToolStripMenuItem1;
        private ToolStripMenuItem createFilterToolStripMenuItem1;
        private ToolStripMenuItem pasteFilterToolStripMenuItem;
        private ToolStripMenuItem launchMultipleScenariosToolStripMenuItem;
        private ToolStripMenuItem copyGraphicDefinitionToolStripMenuItem;
        private ToolStripMenuItem pasteGraphicDefinitionToolStripMenuItem;
        private ToolStripMenuItem copyGraphicDefinitionToolStripMenuItem1;
        private ToolStripMenuItem pasteGraphicDefinitionToolStripMenuItem1;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem userManualToolStripMenuItem;
        private ContextMenuStrip cmsStatistics;
        private ToolStripMenuItem viewStatisticsToolStripMenuItem;
        private ToolStripMenuItem languageToolStripMenuItem;
        private ToolStripMenuItem frenchToolStripMenuItem;
        private ToolStripMenuItem englishToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private SplitContainer sc_Content_Help;
        private ToolStripMenuItem helpToolStripMenuItem1;
        private ToolStripMenuItem releaseNotesToolStripMenuItem;
        private ContextMenuStrip cms_ColumnHeader;
        private TabPage tabSimul8;
        private ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel tsl_WyStatus;
        private wyDay.Controls.AutomaticUpdater automaticUpdater1; 
        private ToolStripMenuItem addToChartTableToolStripMenuItem;
        private ToolStripMenuItem addToChartFilterToolStripMenuItem;
        private ToolStripMenuItem systemRequirementsToolStripMenuItem;
        private ToolStripMenuItem createFilterToolStripMenuItem2;
        private ToolStripMenuItem pasteFilterToolStripMenuItem1;
        private ToolStripMenuItem allocateToolStripMenuItem;
        private ContextMenuStrip cmsExceptionTable;
        private ToolStripMenuItem tsmiEditExceptionTable;
        private ToolStripMenuItem tsmiDeleteExceptionTable;
        private ToolStripMenuItem addCustomGraphToolStripMenuItem;
        private ToolStripMenuItem editColumnsNameToolStripMenuItem;
        private ToolStripMenuItem tsmi_PKG;
        private ToolStripMenuItem estimateAirportOccupationToolStripMenuItem;
        private ToolStripMenuItem generateFlightPlansToolStripMenuItem;
        private TabPage tabGantt;
        private Label ganttTitle;
        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash1;
        private TabPage tabSimreporter;
        private Button simreporterLaunchButton;
        private WebBrowser simreporterWebBrowser;
        private TabPage tabNewItinerary;
        private Button testButton;
        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash2;
        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash3;
        private AxShockwaveFlashObjects.AxShockwaveFlash shockwaveFlashForItinerary;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem setTargetToolStripMenuItem;
        private ToolStripMenuItem viewStatisticsToolStripRightClickMenuItem;
        private ToolStripMenuItem viewFilterStatisticsToolStripRightClickMenuItem;
        private ToolStripMenuItem deleteTargetTableToolStripMenuItem;
        private ToolStripMenuItem setCountingParametersToolStripMenuItem;
        private ToolStripMenuItem deleteCountResultTableToolStripMenuItem;
        private ToolStripMenuItem deleteISTCountedItemsTableToolStripMenuItem;
        private TabPage tabPageDashboard;
        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlashForDashboard;
        private ToolStripMenuItem generateFlightPlansAIAToolStripMenuItem;
        private ToolStripMenuItem importDubaiFlightPlansToolStripMenuItem;
        private ToolStripMenuItem generateFilesForDubaiAllocationToolStripMenuItem;
        private ToolStripMenuItem generateFlightPlanInformationToolStripMenuItem;
        private ToolStripMenuItem generateLiegeAllocationToolStripMenuItem;
        private ToolStripMenuItem importLiegeFlightPlansToolStripMenuItem;
        private ToolStripMenuItem deleteStatisticsToolStripMenuItem;
        private ContextMenuStrip cmsReports;
        private ToolStripMenuItem addReportToolStripMenuItem;
        private ToolStripMenuItem editReportToolStripMenuItem;
        private ToolStripMenuItem deleteReportToolStripMenuItem;
        private ToolStripMenuItem previewReportToolStripMenuItem;
        private ToolStripMenuItem pdfFormatPreviewToolStripMenuItem;
        private ToolStripMenuItem htmlFormatPreviewFormatToolStripMenuItem;
        private ToolStripMenuItem generateReportToolStripMenuItem;
        private ToolStripMenuItem pdfFormatGeneratorToolStripMenuItem;
        private ToolStripMenuItem htmlFormatGeneratorToolStripMenuItem1;
        private ToolStripMenuItem editNoteToolStripMenuItem;
        private ToolStripMenuItem expandCollapseToolStripMenuItem;
        private ToolStripMenuItem navigateToSourceToolStripMenuItem;
        private ToolStripMenuItem manageLinksToolStripMenuItem;
        private ToolStripMenuItem exportAsTextFilesToolStripMenuItem;
        private ContextMenuStrip exportScenarioTablesContextMenuStrip;
        private ToolStripMenuItem exportAsTextFilesToolStripMenuItem1;
        private FolderBrowserDialog folderBrowserDialog1;
        private ToolStripMenuItem copyChartDefinitionToolStripMenuItem;
        private ToolStripMenuItem pasteChartDefinitionToolStripMenuItem;
        private ToolStripMenuItem copyUserGraphicsChartDefinitionToolStripMenuItem;
        private ToolStripMenuItem pasteUserGraphicsChartDefinitionToolStripMenuItem;
        private ToolStripMenuItem importCDGDepartureFlightPlanToolStripMenuItem;
        private ToolStripButton searchTypeToolStripButton;
        private ToolStripTextBox SearchToolStripTextBox;
        private ToolStripButton prevToolStripButton;
        private ToolStripButton nextToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton searchToolStripButton;
        private ToolStripMenuItem specificationDocumentToolStripMenuItem;
        private ContextMenuStrip cmsDocuments;
        private ToolStripMenuItem addDocumentMenuItem;
        private ToolStripMenuItem removeAllDocumenstMenuItem;
        private ContextMenuStrip cmsDocument;
        private ToolStripMenuItem updateDocumentMenuItem;
        private ToolStripMenuItem removeDocumentMenuItem;
        private ToolStripMenuItem openDocumentMenuItem;
        private ToolStripMenuItem openDocumentsFolderToolStripMenuItem;
        private ToolStripMenuItem openFromProjectToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem1;
        private ToolStripMenuItem openTemporaryFolderToolStripMenuItem;
        private ToolStripMenuItem openProjectFolderToolStripMenuItem;
        private ToolStripMenuItem refreshMainReportsNodeToolStripMenuItem;
        private ToolStripMenuItem manageChartLinksToolStripMenuItem;
        private ToolStripMenuItem navigateToDeskFromTableToolStripMenuItem;
        private ToolStripMenuItem navigateToDeskFromDatagridToolStripMenuItem;
        private ToolStripMenuItem navigateToDeskFromFilterToolStripMenuItem;
        private ToolStripMenuItem exportAsTextFilesFromReportToolStripMenuItem;
        private ToolStripMenuItem exportXMLReportConfigurationToolStripMenuItem;
        private ToolStripMenuItem assignToReportGroupsToolStripMenuItem;
        private ToolStripMenuItem generateReportGroupToolStripMenuItem;
        private ToolStripMenuItem generateReportGroupAsPdfToolStripMenuItem;
        private ToolStripMenuItem generateReportGroupAsHtmlToolStripMenuItem;
        private ToolStripMenuItem openLogToolStripMenuItem;
        private ToolStripMenuItem listOfErrorsToolStripMenuItem;
        private ToolStripMenuItem showLatestErrorsToolStripMenuItem;
        private ToolStripMenuItem clearLatestErrorsToolStripMenuItem;
        private ToolStripMenuItem generateTextGanttFromTableToolStripMenuItem;
        private ToolStripMenuItem generateTextGanttFromFilterToolStripMenuItem;
        private ToolStripMenuItem applyColorCodeOnFilterToolStripMenuItem;
        private ToolStripMenuItem applyColorCodeOnTableToolStripMenuItem;
        private ToolStripMenuItem applyColorCodeFromReportToolStripMenuItem;
        private ToolStripMenuItem applyColorCodeOnGenericTableToolStripMenuItem;
        private ToolStripMenuItem bHSTraceKeywordsToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemCopyTableName;
        private ToolStripMenuItem copyTableNameToolStripMenuItem;
    }
}

