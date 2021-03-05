using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Simul8
{
    public partial class Simul8ControlPanel : Form
    {
        Simul8SubForm s8sf;
        HScrollBar hsbSpeed;

        PAX2SIM p2sMaster;
        SIMCORE_TOOL.Prompt.SIM_Scenarios_Assistant.ParamScenario psParams;

        GestionDonneesHUB2SIM Donnees;

        /// <summary>
        /// Gets or Set the end of the results collection period for the simulation. The time unit for the 
        /// EndSimulation value is in Minutes.
        /// </summary>
        internal float EndSimulation
        {
            get
            {
                /*return fEndSimulation;
                //*/
                return Simul8.Simul8SubForm.ConvertTimeInMinutes(s8sf.ResultsCollectionPeriod, s8sf.TimeUnit);
            }
            set
            {
                /*fEndSimulation = value;
                // Simul8.Simul8SubForm.ConvertMinutesInTime(value, s8sf.TimeUnit);
                //*/
                s8sf.ResultsCollectionPeriod = Simul8.Simul8SubForm.ConvertMinutesInTime(value, s8sf.TimeUnit);
            }
        }


        internal Boolean EnableSimulation
        {
            set
            {
                tsb_Reset.Enabled = value;
                tsb_StartStop.Enabled = value;
                tsb_Speed.Enabled = value;
                tsl_Speed.Enabled = value;
                tsb_Cancel.Visible = value;
                LinkMode.Enabled = !value;
                if ((!value) && (s8sf != null))
                {
                    if (s8sf.SimulationState == Simul8SubForm.SimulationStat.Running)
                    {
                        tsb_StartStop.Checked = false;
                        cb_StartStop_Click(null, null);
                    }
                }
            }
        }
        internal int SetSpeed
        {
            set
            {
                if (value < 20)
                    value = 20;
                if (value > tsb_Speed.Maximum)
                    value = tsb_Speed.Maximum;
                //tsb_Speed.Value = value;
                s8sf.SimulationSpeed = value;
                hsbSpeed.Value = value;
            }
        }

        internal SIMCORE_TOOL.Prompt.SIM_Scenarios_Assistant.ParamScenario Scenario
        {
            set
            {
                psParams = value;
            }
        }

        internal void Start()
        {
            tsb_StartStop.Checked = true;
            cb_StartStop_Click(null, null);
        }


        
        /// <summary>
        /// Function that permits to refresh the display of the Simul8 Area (For Windows 7 users )
        /// There were a problem with the invalidate function, so that function had been written to
        /// refresh the display. It only send the mouse down and up signals to Simul8.
        /// </summary>
        internal void RefreshSimul8()
        {
            if (s8sf != null)
                s8sf.RefreshSimul8();
        }
        private void EndRun()
        {
            tsb_Analyse.Visible = true;
            tsb_StartStop.Checked = false;

        }
        /// <summary>
        /// Gets and Sets the value for the start date for the simulation. By default it's 1/1/2000 at 0:00
        /// </summary>
        internal DateTime StartDate
        {
            get
            {
                if (s8sf != null)
                    return s8sf.StartDate;
                return new DateTime(1, 1, 1, 0, 0, 0);
            }
            set
            {
                if (s8sf != null)
                    s8sf.StartDate = value;
            }
        }

        internal bool OpenSimul8Project(String ProjectName)
        {
            return s8sf.OpenSimul8Project(ProjectName);
        }

        internal Simul8ControlPanel(SIMUL8.S8Simulation S8Application, 
            String sSubWindowName, 
            PAX2SIM p2sMaster_, 
            Simul8SubForm.Object_DoubleClick odcDoubleClickDelegate,
            GestionDonneesHUB2SIM Donnees_)
        {
            InitializeComponent();
            s8sf = new Simul8SubForm(S8Application, sSubWindowName, Donnees_.getNomProjet());
            s8sf.TopLevel = false;
            s8sf.Parent = this.panel1;
            s8sf.Dock = DockStyle.Fill;
            s8sf.Show();
            tscb_Zoom.Text = "100";
            s8sf.ZoomHandler = tscb_Zoom;
            //fEndSimulation = s8sf.ResultsCollectionPeriod;
            s8sf.ClockHandler = tsl_Time;
            s8sf.SimulationSpeed = 20;
            s8sf.EndRunDelegate = new Simul8SubForm.EndRun(EndRun);
            s8sf.Object_DroppedDelegate = new Simul8SubForm.Object_Dropped(Object_DroppedFunction);
            s8sf.GetFirstStationForGroupDelegate = new Simul8SubForm.GetFirstStationForGroup(getFirstStationInGroup);
            s8sf.GetLastStationForGroupDelegate = new Simul8SubForm.GetLastStationForGroup(getLastStationInGroup);
            s8sf.getGroupNameForStationDelegate = new Simul8SubForm.getGroupNameForStation(getGroupNameForStation);

            s8sf.Object_DoubleClickDelegate = odcDoubleClickDelegate;
            p2sMaster = p2sMaster_;
            psParams = null;

            //Rectangle rect = toolStripProgressBar1.;
            hsbSpeed = new HScrollBar();
            hsbSpeed.Parent = tsb_Speed.Control;
            hsbSpeed.Size = tsb_Speed.Size;
            hsbSpeed.Location = new Point(0, 0);
            //hsb.Parent = this;
            hsbSpeed.Maximum = 100;
            hsbSpeed.Minimum = 20;
            hsbSpeed.LargeChange = 1;
            hsbSpeed.SmallChange = 1;
            hsbSpeed.Show();
            hsbSpeed.Dock = DockStyle.Fill;
            hsbSpeed.Scroll += new ScrollEventHandler(hsbSpeed_Scroll);
            Donnees = Donnees_;
            LinkMode.Checked = s8sf.LinkMode;
        }

        private void cb_StartStop_Click(object sender, EventArgs e)
        {
            if (s8sf.SimulationState == Simul8SubForm.SimulationStat.Running)
            {
                s8sf.StopSimulation();
            }
            else
            {
                s8sf.RunSimulation(EndSimulation/**60.0/*fEndSimulation*/);
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            if (s8sf.SimulationState == Simul8SubForm.SimulationStat.Running)
            {
                this.tsb_StartStop.Checked = false;
                cb_StartStop_Click(null, null);

            }
            s8sf.ResetSimulation(0);
            tsb_Analyse.Visible = false;
        }

        private void cb_Showlinks_CheckedChanged(object sender, EventArgs e)
        {
            s8sf.ShowLinks = tsb_ShowHideLinks.Checked;
        }

        private void LinkMode_Click(object sender, EventArgs e)
        {
            if (sCurrentObject != "")
            {
                ToolStripButton tsbTmp = GroupIsNotHidden(sCurrentObject);
                if (tsbTmp != null)
                    tsbTmp.Visible = true;
            }
            s8sf.LinkMode = LinkMode.Checked;
        }
        private void cb_Zoom_Leave(object sender, EventArgs e)
        {
            Int32 iZoom;
            if ((!Int32.TryParse(tscb_Zoom.Text, out iZoom)) || (iZoom <= 0) || (iZoom > 500))
                iZoom = 100;
            s8sf.Zoom = iZoom;
        }

        private void tscb_Zoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cb_Zoom_Leave(sender, null);
            }
        }


        void hsbSpeed_Scroll(object sender, ScrollEventArgs e)
        {
            s8sf.SimulationSpeed = hsbSpeed.Value;
        }
        /*private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {
            Point pt = tsb_Speed.Control.PointToClient(MousePosition);
            if (tsb_Speed.Width > 0)
            {
                int iValue = (int)(((float)(pt.X) / tsb_Speed.Width) * 100);
                if (iValue > tsb_Speed.Maximum)
                    iValue = 100;
                if (iValue < 20)
                {
                    iValue = 20;
                    tsb_Speed.ToolTipText = "The minimum value for the speed is 20%.";
                }
                //int iValue = (int)(tsb_Zoom.Minimum+ ((float)((float)(pt.X) / tsb_Zoom.Width) * (tsb_Zoom.Maximum - tsb_Zoom.Minimum)));
                tsb_Speed.Value = iValue;
                s8sf.SimulationSpeed = iValue;
            }
        }*/

        private void tsb_Analyse_Click(object sender, EventArgs e)
        {
            tsb_Analyse.Visible = false;
            tsb_Cancel.Visible = false;
            EnableSimulation = false;
            if (p2sMaster != null)
                p2sMaster.AnalyseSimul8Results(psParams);
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            tsb_Cancel.Visible = false;
            tsb_Analyse.Visible = false;
            EnableSimulation = false;
            if (p2sMaster != null)
                p2sMaster.CancelSimul8Simulation(psParams);
        }

        private void tsb_Speed_Paint(object sender, PaintEventArgs e)
        {
            hsbSpeed.Invalidate();
        }

        /// <summary>
        /// Function that will update the right menu of the objects with the scenarios names.
        /// </summary>
        /// <param name="lsScenarios_">List of the available scenarios</param>
        /// <param name="ehContextMenuScenarioClick">Event to be sent when the user select a scenario.</param>
        internal void setScenarioMenu(List<String> lsScenarios_,List<String> lsGraphiques, EventHandler ehContextMenuScenarioClick)
        {
            s8sf.setScenarioMenu(lsScenarios_,lsGraphiques, ehContextMenuScenarioClick);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            s8sf.RefreshSimul8();
        }



        #region For the Objects that can be dropped on the simulation window
        List<ToolStripButton> ltsb_DroppedComponents;


        /// <summary>
        /// Function that updates the list and the Simul8 window with the groups names.
        /// The function will delete the groups that had disappeared in the Pax2sim
        /// structure and will add the new ones.
        /// </summary>
        /// <param name="alGroupsNames"></param>
        internal void setListGroup(List<String> alGroupsNames)
        {
            if (ltsb_DroppedComponents == null)
                ltsb_DroppedComponents = new List<ToolStripButton>();

            //If the current list is empty, then we have to delete all the component in 
            //the Simul8 window.
            if (alGroupsNames.Count == 0)
            {
                //We delete all the components that are not yet on the Simul8 window.
                ts_Element.Items.Clear();

                //We delete all the components that are on the Simul8 window.
                foreach (ToolStripButton tsbTmp in ltsb_DroppedComponents)
                {
                    s8sf.DeleteGroup(tsbTmp.Text);
                }
                ltsb_DroppedComponents.Clear();
                ts_Element.Visible = false;
                return;
            }

            //List of the element that have to be removed from the ltsb_DroppedComponents list.
            List<ToolStripButton> ltsb_ToRemove = new List<ToolStripButton>();

            //We are going through the objets already on the Simul8 window. If an element does not
            //appear in the list with the groups names, then we remove it from Simul8 window.
            foreach (ToolStripButton tsbTmp in ltsb_DroppedComponents)
            {
                if (!alGroupsNames.Contains(tsbTmp.Text))
                {
                    s8sf.DeleteGroup(tsbTmp.Text);
                    ltsb_ToRemove.Add(tsbTmp);
                }
                else
                {
                    alGroupsNames.Remove(tsbTmp.Text);
                }
            }
            foreach (ToolStripButton tsbTmp in ltsb_ToRemove)
            {
                ltsb_DroppedComponents.Remove(tsbTmp);
            }
            ltsb_ToRemove.Clear();


            //We remove from the toolstrip menu the element that are not in the list of current items.
            foreach (ToolStripButton tsbTmp in ts_Element.Items)
            {
                if (!alGroupsNames.Contains(tsbTmp.Text))
                {
                    ltsb_ToRemove.Add(tsbTmp);
                }
                else
                {
                    alGroupsNames.Remove(tsbTmp.Text);
                }
            }
            foreach (ToolStripButton tsbTmp in ltsb_ToRemove)
            {
                ltsb_DroppedComponents.Remove(tsbTmp);
            }
            ltsb_ToRemove.Clear();

            foreach (String sTmp in alGroupsNames)
            {
                AddToolStripButton(sTmp);
            }
            ts_Element.Visible = (ts_Element.Items.Count > 0);
        }

        internal bool ContainsGroup(String sName)
        {

            foreach (ToolStripButton tsbTmp in ltsb_DroppedComponents)
            {
                if (tsbTmp.Text == sName)
                {
                    return true;
                }
            }
            foreach (ToolStripButton tsbTmp in ts_Element.Items)
            {
                if (tsbTmp.Text == sName)
                {
                    return true;
                }
            }
            return false;
        }
        internal ToolStripButton GroupIsHidden(String sName)
        {
            foreach (ToolStripButton tsbTmp in ltsb_DroppedComponents)
            {
                if (tsbTmp.Text == sName)
                {
                    return tsbTmp;
                }
            }
            return null;
        }
        internal ToolStripButton GroupIsNotHidden(String sName)
        {
            foreach (ToolStripButton tsbTmp in ts_Element.Items)
            {
                if (tsbTmp.Text == sName)
                {
                    return tsbTmp;
                }
            }
            return null;
        }
        private void AddToolStripButton(String sButtonName)
        {
            if (ContainsGroup(sButtonName))
            {
                ToolStripButton tsbTmp = GroupIsHidden(sButtonName);
                if (tsbTmp == null)
                    return;
                ltsb_DroppedComponents.Remove(tsbTmp);
                ts_Element.Items.Add(tsbTmp);
            }
            else
            {
                int i;
                for (i = 0; i < imageList1.Images.Keys.Count; i++)
                {
                    if (sButtonName.Contains(imageList1.Images.Keys[i]))
                    {
                        break;
                    }
                }
                if (sButtonName.Length == 4)
                    i = 0;
                if (i >= imageList1.Images.Keys.Count)
                    return;
                ToolStripButton tsbTmp = new ToolStripButton(sButtonName, imageList1.Images[i], new EventHandler(tsbTmp_Click));
                tsbTmp.DisplayStyle = ToolStripItemDisplayStyle.Image;
                ts_Element.Items.Add(tsbTmp);
            }

        }

        void tsbTmp_Click(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(ToolStripButton))
                return;
            if (LinkMode.Checked)
            {
                LinkMode.Checked = false;
                s8sf.LinkMode = false;
            }
            ToolStripButton tsbTmp;
            if (sCurrentObject != "")
            {
                tsbTmp = GroupIsNotHidden(sCurrentObject);
                if (tsbTmp != null)
                    tsbTmp.Visible = true;
            }


            tsbTmp = (ToolStripButton)sender;
            tsbTmp.Visible = false;
            s8sf.DropComponent(tsbTmp.Text, /*"E:\\P2S_Pax_Generic.OS8",*/ tsbTmp.Image);
            sCurrentObject = tsbTmp.Text;
        }
        String sCurrentObject = "";

        private void Object_DroppedFunction(String sName)
        {
            ToolStripButton tsb_Component = null;
            if (sName == "")
            {

                tsb_Component = GroupIsNotHidden(sCurrentObject);
                if (tsb_Component == null)
                    return;
            }
            else
            {
                tsb_Component = GroupIsNotHidden(sName);
                //On recherche le groupe
                if (tsb_Component == null)
                    return;
                ts_Element.Items.Remove(tsb_Component);
                ltsb_DroppedComponents.Add(tsb_Component);
            }
            tsb_Component.Visible = true;
            sCurrentObject = "";
        }

        internal void ObjectDeletedFromItinerary(String sName)
        {
            ToolStripButton tsb = GroupIsHidden(sName);
            if (tsb == null)
                return;
            ltsb_DroppedComponents.Remove(tsb);
            ts_Element.Items.Add(tsb);

        }
        #endregion

        private int getFirstStationInGroup(int iTerminal, int iTypeGroupe, int iIndexGroupe)
        {
            return Donnees.getIndexFirstDesk(iTerminal, iTypeGroupe, iIndexGroupe);
        }
        private int getLastStationInGroup(int iTerminal, int iTypeGroupe, int iIndexGroupe)
        {
            return Donnees.getIndexLastDesk(iTerminal, iTypeGroupe, iIndexGroupe);
        }

        private String getGroupNameForStation(String sStationName)
        {
            return Donnees.getGroup(sStationName);
        }

        internal void CloseProject()
        {
            s8sf.CloseProject();
        }

        internal void initializeModel(TreeNode tnAirportStructure,
                                      List<String> alGroupsNames,
                                      DataTable dtItinerary)
        {
            if (tnAirportStructure == null)
                return;
            setListGroup(alGroupsNames);
            List<Simul8SubForm.Simul8Object> lsObjectsToAdd = new List<Simul8SubForm.Simul8Object>();
            foreach (String sGroupName in alGroupsNames)
            {
                TreeNode child = OverallTools.TreeViewFunctions.getAirportChild(tnAirportStructure, sGroupName);
                if (child == null)
                    continue;
                if (!((TreeViewTag)child.Tag).Visible)
                    continue;
                ToolStripButton tsbObject = GroupIsNotHidden(sGroupName);
                if(tsbObject == null)
                    continue;
                Object_DroppedFunction(sGroupName);
                lsObjectsToAdd.Add(new Simul8SubForm.Simul8Object(sGroupName, ((TreeViewTag)child.Tag).Name, sGroupName, tsbObject.Image, ((TreeViewTag)child.Tag).Location));
            }
            List<Assistant.Ressource_Assistant.ArrowParam> lapLinks = GenerateArrows(dtItinerary);
            s8sf.InitializeModel(lsObjectsToAdd, lapLinks);
        }
        private static List<Assistant.Ressource_Assistant.ArrowParam> GenerateArrows(DataTable dtItinerary)
        {
            if ((dtItinerary.Columns[2].DataType != typeof(Double)) &&
                   (dtItinerary.Columns[2].DataType != typeof(double)) &&
                   (dtItinerary.Columns[4].DataType != typeof(double)) &&
                   (dtItinerary.Columns[4].DataType != typeof(Double)) &&
                   (dtItinerary.Columns[5].DataType != typeof(double)) &&
                   (dtItinerary.Columns[5].DataType != typeof(Double)) &&
                   (dtItinerary.Columns[6].DataType != typeof(double)) &&
                   (dtItinerary.Columns[6].DataType != typeof(Double)))
                return null;
            List<Assistant.Ressource_Assistant.ArrowParam> lapResult = new List<SIMCORE_TOOL.Assistant.Ressource_Assistant.ArrowParam>();
            foreach (DataRow ligne in dtItinerary.Rows)
            {
                String from = ligne.ItemArray[0].ToString();
                String to = ligne.ItemArray[1].ToString();
                Double Weight = (Double)ligne.ItemArray[2];
                String distribution = ligne.ItemArray[3].ToString();
                Double param1, param2, param3;
                if ((!Double.TryParse(ligne.ItemArray[4].ToString(), out param1)) ||
                    (!Double.TryParse(ligne.ItemArray[5].ToString(), out param2)) ||
                    (!Double.TryParse(ligne.ItemArray[6].ToString(), out param3)))
                    continue;

                lapResult.Add(new Assistant.Ressource_Assistant.ArrowParam(from, "", to, "", Weight, distribution, param1, param2, param3));
            }

            return lapResult;
        }


        internal void SetModelReadyToSimulate(Prompt.SIM_Scenarios_Assistant.ParamScenario psParams)
        {

            //We generate the list used for initializing the capacity of the queues
            DataTable[] dtTables = new DataTable[2];
            dtTables[0] = Donnees.getTable("Input", psParams.CapaQueues/*  GlobalNames.Capa_QueuesTableName*/);
            dtTables[1] = Donnees.getTable("Input", psParams.GroupQueues /*GlobalNames.Group_QueuesName*/);
            s8sf.UpdateStationQueueCapacity(GestionDonneesHUB2SIM.generateCapaQueues(dtTables), psParams.FillQueue);
        }
    }
}