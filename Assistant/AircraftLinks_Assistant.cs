using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant
{
    public partial class AircraftLinks_Assistant : Form
    {
        DataTable FPATable;
        DataTable FPDTable;
        DataTable FPLinkTable;
        List<FPFlight> lFPA;
        List<FPFlight> lFPD;
        List<Line> llLines;
        private void Initialize(DataTable dtFPATable, DataTable dtFPDTable, DataTable dtFPAircraftLinksTable)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            FPATable = dtFPATable;
            FPDTable = dtFPDTable;
            FPLinkTable = dtFPAircraftLinksTable;
            lFPA = getFlights(dtFPATable);
            lFPD = getFlights(dtFPDTable);
            llLines = new List<Line>();
            int i = 0;
            Line lNewLine = null;
            bool bMismatch = false;
            foreach (FPFlight FPA in lFPA)
            {
                lNewLine = new Line(i, panel1, toolTip1);
                llLines.Add(lNewLine);
                lNewLine.setFPAInformations(FPA);
                lNewLine.setFPDInformations(getValidDepartureFlights(FPA, false, true));
                i++;

                String sValue = OverallTools.DataFunctions.getValue(dtFPAircraftLinksTable, FPA.getID().ToString(), 0, 3);
                if (sValue == "")
                    continue;
                FPFlight fp = getFlight(lFPD, sValue);
                if (fp == null)
                    continue;
                lNewLine.setSelectedFPD(sValue);
                bMismatch = bMismatch || lNewLine.isMismatched;
            }
            if (bMismatch)
                cb_AllowMismatch.Checked = true;

            if (lNewLine != null)
                lNewLine.Show(0);
        }

        private List<FPFlight> getFlights(DataTable FPTable)
        {
            if (FPTable == null)
                return null;
            int iIndexID = FPTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_ID);
            int iIndexN= FPTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_FlightN);
            int iIndexDate = FPTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_DATE);
            int iIndexSTD = FPTable.Columns.IndexOf(GlobalNames.sFPD_Column_STD);
            if (iIndexSTD == -1)
                iIndexSTD = FPTable.Columns.IndexOf(GlobalNames.sFPA_Column_STA);

            int iIndexAircraft= FPTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AircraftType);
            int iIndexAirline = FPTable.Columns.IndexOf(GlobalNames.sFPD_A_Column_AirlineCode);
            if ((iIndexID == -1) ||
                (iIndexDate == -1) ||
                (iIndexSTD == -1) ||
                (iIndexAircraft == -1) ||
                (iIndexAirline == -1)||
                (iIndexN==-1))
            {
                return null;
            }
            List<FPFlight> lfList = new List<FPFlight>();

            foreach (DataRow drRow in FPTable.Rows)
            {
                lfList.Add(new FPFlight((int)drRow[iIndexID], ((DateTime)drRow[iIndexDate]).Add((TimeSpan)drRow[iIndexSTD]), drRow[iIndexN].ToString(), drRow[iIndexAirline].ToString(), drRow[iIndexAircraft].ToString()));
            }
            return lfList;
        }
        public AircraftLinks_Assistant(DataTable dtFPATable, DataTable dtFPDTable, DataTable dtFPAircraftLinksTable)
        {
            Initialize(dtFPATable, dtFPDTable, dtFPAircraftLinksTable);

        }

        internal List<FPFlight> getValidDepartureFlights(FPFlight fpFlight, bool bIgnoreAllocatedFlights, bool bShowMismatchFlights)
        {
            List<FPFlight> lResults = new List<FPFlight>();
            foreach (FPFlight fpTmp in lFPD)
            {
                if ((bIgnoreAllocatedFlights) && (fpTmp.Allocated))
                    continue;
                if (fpTmp.FlightTime() < fpFlight.FlightTime())
                    continue;
                if (((fpTmp.Aircraft != fpFlight.Aircraft) ||
                    (fpTmp.Airline != fpFlight.Airline)) && (!bShowMismatchFlights))
                    continue;
                lResults.Add(fpTmp);
            }
            return lResults;
        }

        internal FPFlight getFlight(List<FPFlight> lfp, string sID)
        {
            if (lfp == null)
                return null;
            foreach (FPFlight fpTmp in lfp)
            {
                if (fpTmp.getID() == sID)
                    return fpTmp;
            }
            return null;
        }


        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateRegion(panel1.Size, panel1.VerticalScroll.Value);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            UpdateRegion(panel1.Size, panel1.VerticalScroll.Value);
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            UpdateRegion(panel1.Size, panel1.VerticalScroll.Value);
        }

        private void cb_AllowMismatch_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
            //UpdateRegion(panel1.Size, panel1.VerticalScroll.Value, true);
        }

        private void cb_Reaffect_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
            //UpdateRegion(panel1.Size, panel1.VerticalScroll.Value, true);
        }

        private void UpdateRegion(Size sCurrentSize, int iCurrentScroll/*, bool bUpdateContent*/)
        {
            int iNumber = sCurrentSize.Height / 30+2;
            int iFirst = iCurrentScroll / 30 -1;
            if (iFirst < 0)
                iFirst = 0;
            for (int i = iFirst; (i < (iFirst + iNumber)) && (i < llLines.Count); i++)
            {
                llLines[i].Show(iCurrentScroll);
                /*if (bUpdateContent)
                    llLines[i].setFPDInformations(getValidDepartureFlights(llLines[i].FPA, cb_Reaffect.Checked, cb_AllowMismatch.Checked));
                llLines[i].Mismatch = cb_AllowMismatch.Checked;
                llLines[i].HideAlreadyAllocated = cb_Reaffect.Checked;*/
            }
        }

        private void UpdateView()
        {
            foreach (Line lTmp in llLines)
            //for (int i = 0; (i < (iFirst + iNumber)) && (i < llLines.Count); i++)
            {
                lTmp.Mismatch = cb_AllowMismatch.Checked;
                lTmp.HideAlreadyAllocated = cb_Reaffect.Checked;
            }
        }

        internal ToolTip Tooltip
        {
            get { return this.toolTip1; }
        }

        internal class FPFlight
        {
            int FPID;
            DateTime Time;
            string FPName;
            String sAirline_;
            String sAircraft_;

            Boolean bAllocated;
            Line lOldAllocation;

            internal Line CurrentAllocated
            {
                get
                {
                    return lOldAllocation;
                }
                set
                {
                    lOldAllocation = value;
                }
            }

            internal Boolean Allocated
            {
                get
                {
                    return bAllocated;
                }
                set
                {
                    bAllocated = value;
                }
            }
            internal String Airline
            {
                get
                {
                    return sAirline_;
                }
                set
                {
                    sAirline_ = value;
                }
            }
            internal String Aircraft
            {
                get
                {
                    return sAircraft_;
                }
                set
                {
                    sAircraft_ = value;
                }
            }

            internal int ID
            {
                get
                {
                    return FPID;
                }
            }

            internal FPFlight(int sFPID, DateTime dtTime, string sFPName, String sAirline, String sAircraft)
            {
                FPID = sFPID;
                Time = dtTime;
                FPName = sFPName;
                bAllocated = false;
                sAirline_ = sAirline;
                sAircraft_ = sAircraft;
            }

            internal string getNames()
            {
                return FPName;
            }
            internal string getID()
            {
                return FPID.ToString();
            }
            internal DateTime FlightTime()
            {
                return Time;
            }
            public override string ToString()
            {
                return getID();
            }
        }
        internal class Line
        {
            Label lbl_FPAID;
            Label lbl_STA;
            Label lbl_FPAN;
            ComboBox cb_FPDID;
            Label lbl_STD;
            ComboBox cb_FPDN;
            Label lbl_RotationDuration;
            Label lbl_AircraftType;
            Label lbl_Company;
            
            FPFlight oldSelected;
            FPFlight FPAFlight;

            PictureBox img_warning;

            List<FPFlight> lfAvailableflights;

            bool bMismatch;
            bool bHideAlreadyAllocated;

            int iIndexLine_;
            bool bAdded = false;
            Control cParent_;
            ToolTip toolTip;

            internal FPFlight FPA
            {
                get
                {
                    return FPAFlight;
                }
            }


            internal bool Mismatch
            {
                set
                {
                    bMismatch = value;
                }
            }
            internal bool HideAlreadyAllocated
            {
                set
                {
                    bHideAlreadyAllocated = value;
                }
            }

            internal bool isMismatched
            {
                get 
                {
                    if(oldSelected == null)
                        return false;
                    return ((oldSelected.Aircraft != FPA.Aircraft) ||
                            (oldSelected.Airline != FPA.Airline));
                }
            }

            internal Line(int iIndexLine, Control parent, ToolTip tooltip)
            {
                lbl_FPAID = new Label();
                lbl_STA = new Label();
                lbl_FPAN = new Label();
                cb_FPDID = new ComboBox();
                lbl_STD = new Label();
                cb_FPDN = new ComboBox();
                lbl_RotationDuration = new Label();
                lbl_AircraftType = new Label();
                lbl_Company = new Label();

                img_warning = new PictureBox();
                OverallTools.ImageFunctions.SetIconPicture(
                    img_warning,
                    OverallTools.ImageFunctions.EnumIcons.Warning,
                    0.15);
                /*img_warning.Image = SystemIcons.Exclamation.ToBitmap();
                img_warning.SizeMode = PictureBoxSizeMode.StretchImage;
                img_warning.Size = new System.Drawing.Size(img_warning.Size.Width / 8, img_warning.Size.Height / 4); //Scale(0.3f);*/
                img_warning.Visible = false;
                tooltip.SetToolTip(img_warning, "");

                lbl_FPAID.AutoSize = true;
                lbl_STA .AutoSize = true;
                lbl_FPAN .AutoSize = true;
                lbl_STD .AutoSize = true;
                lbl_RotationDuration.AutoSize = true;
                lbl_RotationDuration.AutoSize = true;
                lbl_AircraftType.AutoSize = true;

                cb_FPDID.TabIndex = (iIndexLine+1)*2;
                cb_FPDN.TabIndex = (iIndexLine+1) * 2+1;
                iIndexLine_ = iIndexLine;

                cb_FPDID.Size = new System.Drawing.Size(87, 21);
                cb_FPDN.Size = new System.Drawing.Size(95, 21);

                cb_FPDID.SelectedIndexChanged += new EventHandler(cb_FPDID_SelectedIndexChanged);
                cb_FPDN.SelectedIndexChanged += new EventHandler(cb_FPDN_SelectedIndexChanged);

                cb_FPDID.Enter += new EventHandler(cb_FPDID_Enter);
                cb_FPDN.Enter += new EventHandler(cb_FPDID_Enter);

                cb_FPDID.DropDownStyle = ComboBoxStyle.DropDownList;
                cb_FPDN.DropDownStyle = ComboBoxStyle.DropDownList;
                cParent_ = parent;
                this.toolTip = tooltip;
                oldSelected = null;
                HideAlreadyAllocated = true;
                Mismatch = false;
                lfAvailableflights = null;
            }

            void cb_FPDID_Enter(object sender, EventArgs e)
            {
                //We recalculate the content of the list and we remove the ones that are already allocated.
                UpdateFPDInformations();
            }

            internal void Show(int iScroll)
            {
                if (bAdded)
                    return;
                bAdded = true;
                
                lbl_FPAID.Location = new System.Drawing.Point(25, 14 + iIndexLine_ * 30-iScroll);
                lbl_STA.Location = new System.Drawing.Point(105, 14 + iIndexLine_ * 30 - iScroll);
                lbl_FPAN.Location = new System.Drawing.Point(238, 14 + iIndexLine_ * 30 - iScroll);
                lbl_AircraftType.Location = new System.Drawing.Point(328, 14 + iIndexLine_ * 30 - iScroll);
                lbl_Company.Location = new System.Drawing.Point(413, 14 + iIndexLine_ * 30 - iScroll);
                cb_FPDID.Location = new System.Drawing.Point(544, 11 + iIndexLine_ * 30 - iScroll);
                lbl_STD.Location = new System.Drawing.Point(687, 14 + iIndexLine_ * 30 - iScroll);
                cb_FPDN.Location = new System.Drawing.Point(815, 11 + iIndexLine_ * 30 - iScroll);
                lbl_RotationDuration.Location = new System.Drawing.Point(930, 14 + iIndexLine_ * 30 - iScroll);
                img_warning.Location = new System.Drawing.Point(8, 14 + iIndexLine_ * 30 - iScroll);

                cParent_.Controls.AddRange(new Control[]{
                            img_warning,
                            lbl_FPAID,
                            lbl_STA ,
                            lbl_FPAN , 
                            lbl_AircraftType ,
                            lbl_Company ,
                            cb_FPDID ,
                            lbl_STD ,
                            cb_FPDN ,
                            lbl_RotationDuration});
            }
            /// <summary>
            /// Utilisé pour bloquer le changement de valeur des ComboBox lorsque l'autre ComboBox est edité.
            /// Cela évite notament l'appel infini de cb_FPDN_SelectedIndexChanged
            /// </summary>
            bool bSecure = false;
            void cb_FPDN_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (cb_FPDN.SelectedIndex == -1)
                {
                    lbl_STD.Text = "";
                    cb_FPDN.Text = "";
                    oldSelected = null;
                }
                if (!bSecure)
                {
                    bSecure = true;
                    cb_FPDID.SelectedIndex = cb_FPDN.SelectedIndex;
                    bSecure = false;
                }
            }


            void cb_FPDID_SelectedIndexChanged(object sender, EventArgs e)
            {
                toolTip.SetToolTip(img_warning, "");
                img_warning.Visible = false;

                // Si aucun vol n'est selectionné, on reset info
                if (cb_FPDID.SelectedIndex == -1)
                {
                    lbl_STD.Text = "";
                    cb_FPDN.Text = "";
                    oldSelected = null;
                    lbl_RotationDuration.Text ="";
                }

                // 
                if (oldSelected != null)
                {
                    oldSelected.Allocated = false;
                    oldSelected.CurrentAllocated = null;
                }
                bSecure = true;
                oldSelected = getSelectedFPD();
                if (oldSelected != null)
                {
                    if (oldSelected.Allocated)
                    {
                        oldSelected.CurrentAllocated.cb_FPDID.SelectedIndex = -1;
                    }
                    oldSelected.CurrentAllocated = this;
                    oldSelected.Allocated = true;
                    lbl_STD.Text = oldSelected.FlightTime().ToString();
                    if (this.FPA.Aircraft != oldSelected.Aircraft)
                    {
                        toolTip.SetToolTip(img_warning, "The departure aircraft selected is different than arrival aircraft !");
                        img_warning.Visible = true;
                    }
                    else if (this.FPA.Airline != oldSelected.Airline)
                    {
                        toolTip.SetToolTip(img_warning, "The airline of the selected departure flight is different than the arrival one !");
                        img_warning.Visible = true;
                    }
                }
                else
                {
                    lbl_STD.Text = "";
                }
                cb_FPDN.SelectedIndex = cb_FPDID.SelectedIndex;
                bSecure = false;
                if (oldSelected != null)
                {
                    lbl_RotationDuration.Text = Math.Round( OverallTools.DataFunctions.MinuteDifference(FPA.FlightTime(), oldSelected.FlightTime()),2).ToString();
                }
            }

            /// <summary>
            /// Fonction pour mettre le point d'exclamation sur la ligne
            /// </summary>
            internal void SetExclamation()
            {
                PictureBox img_warning = new PictureBox();
                img_warning.Image = SystemIcons.Exclamation.ToBitmap();
            }

            /// <summary>
            /// Fonction pour retirer le point d'exclamation de la ligne
            /// </summary>
            internal void RemoveExclamation()
            {
                img_warning.Visible = false;
            }

            /// <summary>
            /// Fonction pour initialiser les informations du vol en arrivé.
            /// </summary>
            /// <param name="FPA">Informations sur le vol</param>
            internal void setFPAInformations(FPFlight FPA) 
            {
                lbl_FPAID.Text = FPA.getID();
                lbl_STA.Text = FPA.FlightTime().ToString();
                lbl_FPAN.Text = FPA.getNames();
                lbl_AircraftType.Text = FPA.Aircraft;
                lbl_Company.Text = FPA.Airline;
                FPAFlight = FPA;
            }

            /// <summary>
            /// Fonction pour initialiser les informations du vol en depart.
            /// </summary>
            /// <param name="lsFlights">List des vols pouvant être associé</param>
            internal void setFPDInformations(List<FPFlight> lsFlights)
            {
                lfAvailableflights = lsFlights;
                UpdateFPDInformations();
            }

            /// <summary>
            /// Fonction pour mettre à jour la list des choix.
            /// </summary>
            internal void UpdateFPDInformations()
            {
                // Sauvegarde du vol selectionné
                String sSelected = cb_FPDID.Text;
                int iSelected = -1;

                // On vide les listes
                cb_FPDID.Items.Clear();
                cb_FPDID.Text = "";
                cb_FPDN.Items.Clear();
                cb_FPDN.Text = "";
                lbl_RotationDuration.Text = "";
                lbl_STD.Text = "";

                // Puis on y remet les info en fonction de ce qui est disponible.
                for (int i = 0; i < lfAvailableflights.Count; i++)
                {
                    if (lfAvailableflights[i].getID() == sSelected)
                    {
                        iSelected = cb_FPDID.Items.Count;
                    }
                    else
                    {
                        if ((lfAvailableflights[i].Allocated) && (bHideAlreadyAllocated))
                            continue;
                        if (((lfAvailableflights[i].Aircraft != FPA.Aircraft) ||
                            ((lfAvailableflights[i].Airline != FPA.Airline))) && (!bMismatch))
                            continue;
                    }
                    cb_FPDID.Items.Add(lfAvailableflights[i]);
                    cb_FPDN.Items.Add(lfAvailableflights[i].getNames());
                }

                // On replace le ComboBox sur la valeur initial
                cb_FPDID.SelectedIndex = iSelected;
            }

            /// <summary>
            /// Fonction utilisé pour retablir le lien à l'ouverture de l'éditeur.
            /// </summary>
            /// <param name="sID">flight ID</param>
            internal void setSelectedFPD(String sID)
            {
                for (int i = 0; i < cb_FPDID.Items.Count; i++)
                {
                    if (((FPFlight)cb_FPDID.Items[i]).getID() == sID)
                    {
                        cb_FPDID.SelectedIndex = i;
                        return;
                    }
                }
                if (!bMismatch)
                {
                    Mismatch = true;
                    UpdateFPDInformations();
                    setSelectedFPD(sID);
                    if (cb_FPDID.SelectedIndex == -1)
                    {
                        Mismatch = false;
                        UpdateFPDInformations();
                    }
                }
                else
                {
                    cb_FPDID.SelectedIndex = -1;
                }
            }

            internal FPFlight getSelectedFPD()
            {
                if (cb_FPDID.SelectedIndex == -1)
                    return null;
                return ((FPFlight)cb_FPDID.SelectedItem);
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            FPLinkTable.Rows.Clear();
            foreach (Line lLine in llLines)
            {
                DataRow drNewRow = FPLinkTable.NewRow();
                drNewRow[0] = lLine.FPA.ID;
                drNewRow[1] = lLine.FPA.FlightTime();
                drNewRow[2] = lLine.FPA.getNames();

                FPFlight FPD = lLine.getSelectedFPD();
                if (FPD == null)
                {
                    drNewRow[3] = "";
                    drNewRow[4] = "";
                    drNewRow[5] = "";
                    drNewRow[6] = "";
                }
                else
                {
                    drNewRow[3] = FPD.ID.ToString();
                    drNewRow[4] = FPD.FlightTime().ToString();
                    drNewRow[5] = FPD.getNames();
                    drNewRow[6] = Math.Round(OverallTools.DataFunctions.MinuteDifference(lLine.FPA.FlightTime(), FPD.FlightTime()), 2);
                }
                FPLinkTable.Rows.Add(drNewRow);

            }
        }


        /*internal class ManageList
        {
            class Item
            {
                internal String ItemName;
                internal Type TypeItem;
                internal String TextItem;
                internal Point LocationItem;
                internal Size SizeItem;
                internal Object oItem;
                internal Item()
                {
                    oItem = null;
                }

                internal void setValue(String value)
                {
                    TextItem = value;
                    update(null);
                }
                internal void update(Control parent)
                {
                    Control ctrl = null;
                    if (TypeItem == typeof(ComboBox))
                    {
                        ctrl = (ComboBox)oItem;
                    }
                    else if (TypeItem == typeof(Label))
                    {
                        ctrl = (Label)oItem;
                    }
                    else if (TypeItem == typeof(CheckBox))
                    {
                        ctrl = (CheckBox)oItem;
                    }
                    else if (TypeItem == typeof(Button))
                    {
                        ctrl = (Button)oItem;
                    }
                    if (ctrl == null)
                        return;
                    ctrl.Text = TextItem;
                    ctrl.Location = LocationItem;
                    ctrl.Size = SizeItem;
                    if (parent != null)
                        ctrl.Parent = parent;
                }
            }
            class Line
            {
                List<Item> liItems;
                Line(List<Item> ltNewItems, Control parent)
                {
                    liItems = ltNewItems;
                    foreach (Item iItem in liItems)
                    {
                        iItem.oItem = iItem.TypeItem.GetConstructor(Type.EmptyTypes).Invoke(null);
                        iItem.update(parent);
                    }
                }
                Item getItem(String Name)
                {
                    foreach (Item iItem in liItems)
                        if (iItem.ItemName == Name)
                            return iItem;
                    return null;
                }
                internal void setValue(String sName, String value)
                {
                    Item iItem = getItem(sName);
                    iItem.setValue(value);
                }
                internal void setList(String sName, String value)
                {
                    Item iItem = getItem(sName);
                    iItem.setValue(value);
                }
            }
            List<Line> llContent;
            internal ManageList(int iSeparationPixel, List<Point>lpFirstLocation, List<Size> lsSizes)
            {
                llContent = new List<Line>();
            }
        }*/
    }
}