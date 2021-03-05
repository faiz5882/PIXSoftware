using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nevron.GraphicsCore;
using Nevron.Editors;
using System.Collections;
using SIMCORE_TOOL.com.crispico.charts;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_Setpoint : Form
    {
        private List<double> Value_;   // List of the setpoints first value
        private List<double> Value2_;  // List of the setpoints second values
        private List<String> axis_;     // List of the setpoints axis
        private ArrayList listSetPointStrokeColor_; // List of the setpoints Stroke color
        private ArrayList listSetPointFillColor_;   // List of the setpoints Fill color when area
        private NColorFillStyle lesCouleursRemplissage;
        private Boolean editSetpoint;   // Usefull when we click on Edit Button. The first click edits the setpoint settings , and the second click affects the changes. 
        private double setpointOutNum2;
        private List<Boolean> isArea_;
        private NStrokeStyle StrokeStyle;
        private NStrokeStyle FillStyle;
        private Image defaultImage;
        private Boolean FillColorChanged;   // checks whether the fill color of the area is changed  while editing
        private Boolean DTimeAbscissa; // Indicate whether the abscissa type is a dateTime or not
        private List<DateTime> beginDateTime_;
        private List<DateTime> endDateTime_;
        private List<Boolean> isActivated_;
        private DateTime begindtime;
        private DateTime enddtime;


        #region Setters and getters
        public ArrayList listSetPointStrokeColor
        {
            get
            {
                return listSetPointStrokeColor_;
            }
            set
            {
                listSetPointStrokeColor_ = value;
            }
        }
        public ArrayList listSetPointFillColor
        {
            get
            {
                return listSetPointFillColor_;
            }
            set
            {
                listSetPointFillColor_ = value;
            }
        }
        public List<double> Value
        {
            get
            {
                return Value_;
            }
            set
            {
                Value_ = value;
            }
        }
        public List<double> Value2
        {
            get
            {
                return Value2_;
            }
            set
            {
                Value2_ = value;
            }
        }

        public List<DateTime> beginDateTime
        {
            get
            {
                return beginDateTime_;
            }
            set
            {
                beginDateTime_ = value;
            }
        }
        public List<DateTime> endDateTime
        {
            get
            {
                return endDateTime_;
            }
            set
            {
                endDateTime_ = value;
            }
        }

        public List<String> axis
        {
            get
            {
                return axis_;
            }
            set
            {
                axis_ = value;
            }
        }
        public List<Boolean> isArea
        {
            get
            {
                return isArea_;
            }
            set
            {
                isArea_ = value;
            }
        }

        public Boolean DateTimeAbscissa
        {
            get
            {
                return DTimeAbscissa;
            }
            set
            {
                DTimeAbscissa = value;
            }
        }

        public List<Boolean> isActivated
        {
            get
            {
                return isActivated_;
            }
            set
            {
                isActivated_ = value;
            }
        }

        public List<ChartUtils.CHART_POSITIONS> chartPositions = new List<ChartUtils.CHART_POSITIONS>();    // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)

        public List<SetPoint> setPoints = new List<SetPoint>(); // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)

        #endregion


        public SIM_Setpoint()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            Value_ = new List<double>();
            Value2_ = new List<double>();
            beginDateTime_ = new List<DateTime>();
            endDateTime_ = new List<DateTime>();
            axis_ = new List<string>();
            listSetPointStrokeColor_ = new ArrayList();
            listSetPointFillColor_ = new ArrayList();
            FillColorChanged = false;
            defaultImage = (new NColorFillStyle(Color.Black)).CreatePreview(new NSize(33, 13), new NStrokeStyle(Color.Black));
            editSetpoint = false;
            secondValueBox.Enabled = FillColor.Enabled = endDTime.Enabled = false;
            setpointOutNum2 = -1;
            rbtn_Line.Checked = true;
            isArea_ = new List<bool>();
            isActivated_ = new List<bool>();
            for (int i = 0; i < isArea_.Count; i++)
            {
                isArea_[i] = false;
                isActivated_[i] = true;
            }

            DTimeAbscissa = false;
            beginDTime.Visible = false;
            endDTime.Visible = false;

            chartPositionComboBox.Items.Clear();
            chartPositionComboBox.Items.Add(ChartUtils.CHART_POSITIONS.TopLeft);
            chartPositionComboBox.Items.Add(ChartUtils.CHART_POSITIONS.TopRight);
            chartPositionComboBox.Items.Add(ChartUtils.CHART_POSITIONS.BottomLeft);
            chartPositionComboBox.Items.Add(ChartUtils.CHART_POSITIONS.BottomRight);
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Click on add butoon
        private void btn_Add_Click(object sender, EventArgs e)
        {
            double setpointOutNum = -1;
            DateTime beginDT = new DateTime(1,1,1);
            DateTime endDT = new DateTime(1,1,1);

            if (beginDTime.Visible)
            {
                //If the argument passed is not an int,warn the user 
                if (!DateTime.TryParse(beginDTime.Text, out beginDT))
                {
                    DialogResult = DialogResult.None;
                    MessageBox.Show("Please provide a correct Setpoint Value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (beginDT < begindtime || beginDT > enddtime)
                {
                    DialogResult = DialogResult.None;
                    MessageBox.Show("Setpoint value out of range, Min: " + begindtime + ", Max: " + enddtime, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //If the area box is checked and that the argument passed is not an int,warn the user 
                if (rbtn_Area.Checked && !DateTime.TryParse(endDTime.Text, out endDT))
                {
                    DialogResult = DialogResult.None;
                    MessageBox.Show("Please provide a correct second Setpoint Value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (rbtn_Area.Checked && endDT < begindtime || rbtn_Area.Checked && endDT > enddtime)
                {
                    DialogResult = DialogResult.None;
                    MessageBox.Show("Setpoint value out of range, Min: " + begindtime + ", Max: " + enddtime, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
            {
                //If the argument passed is not an int,warn the user 
                if (!double.TryParse(valueBox.Text, out setpointOutNum))
                {
                    DialogResult = DialogResult.None;
                    MessageBox.Show("Please provide a correct Setpoint Value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //If the area box is checked and that the argument passed is not an int,warn the user 
                if (rbtn_Area.Checked && !double.TryParse(secondValueBox.Text, out setpointOutNum2))
                {
                    DialogResult = DialogResult.None;
                    MessageBox.Show("Please provide a correct second Setpoint Value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            if (AxisCBox.Text == "")
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Please provide an axis for the Setpoint", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (chartPositionComboBox.Text == "")   // >> Bug #15147 Charts Setpoints only work for first series chart location (frame) C#8
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Please provide the chart postion for the Setpoint", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //If a color is not choosed for the setpoint,warn the user  
            if (setpointColor.Image == defaultImage)
            {
                ///On définit un style noir par defaut pour le Setpoint 
                NStrokeStyle style = new NStrokeStyle(Color.Black);
                //listSetPointStrokeColor.Add(style);
                StrokeStyle = style;
                setpointColor.Image = style.CreatePreview(new NSize(33, 13), new NStrokeStyle(Color.Black));
            }
            //If a fill color is not choosed when the area box is checked,warn the user
            if (rbtn_Area.Checked && FillColor.Image == defaultImage)
            {
                NStrokeStyle style = new NStrokeStyle(Color.Black);
                NFillStyle fillStyle = new NColorFillStyle(Color.Black);
                FillStyle = style;
                FillColor.Image = fillStyle.CreatePreview(new NSize(33, 13), style);
                //listSetPointFillColor.Add(FillStyle);
            }
            //If editSetpoint is true, it means that the user affect a new value or new color to the existing setpoint 
            if (editSetpoint)
            {
                //If it's an area, affect the value and the color
                if (rbtn_Area.Checked)
                {
                    if (beginDTime.Visible)
                    {
                        endDateTime_[SetPointList.SelectedIndex] = endDT;
                        Value2_[SetPointList.SelectedIndex] = -1;
                    }
                    else
                    {
                        Value2_[SetPointList.SelectedIndex] = setpointOutNum2;
                        endDateTime_[SetPointList.SelectedIndex] = new DateTime(1, 1, 1);
                    }
                    isArea_[SetPointList.SelectedIndex] = true;
                    if (FillColorChanged) // if the fillColor is changed while editing a setpoint area, fix the new color
                    {
                        listSetPointFillColor_[SetPointList.SelectedIndex] = FillStyle;
                        FillColorChanged = false;
                    }
                }
                //Else set the corresponding value and color.
                else
                {
                    endDateTime_[SetPointList.SelectedIndex] = new DateTime(1, 1, 1);
                    Value2_[SetPointList.SelectedIndex] = -1;
                    listSetPointFillColor_[SetPointList.SelectedIndex] = null;
                    isArea_[SetPointList.SelectedIndex] = false;
                }

                if (beginDTime.Visible)
                {
                    beginDateTime_[SetPointList.SelectedIndex] = beginDT;
                    Value_[SetPointList.SelectedIndex] = -1;
                }
                else
                {
                    Value_[SetPointList.SelectedIndex] = setpointOutNum;
                    beginDateTime_[SetPointList.SelectedIndex] = new DateTime(1, 1, 1);
                }
                axis_[SetPointList.SelectedIndex] = AxisCBox.Text;
                chartPositions[SetPointList.SelectedIndex] = (ChartUtils.CHART_POSITIONS)chartPositionComboBox.SelectedItem;    // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)

                //set up to date the string dysplayed in the list box
                SetPointList.Items[SetPointList.SelectedIndex] = this.toString(SetPointList.SelectedIndex);
                editSetpoint = false;                
            }
            // Else it means that the user just adds a new setpoint
            else
            {
                //If the setpoint is an area, add its corresponding values
                if (rbtn_Area.Checked)
                {
                    if (beginDTime.Visible)
                    {
                        endDateTime_.Add(endDT);
                        Value2_.Add(-1);
                        beginDTime.Text = begindtime.ToString();
                        endDTime.Text = enddtime.ToString();
                    }
                    else
                    {
                        Value2_.Add(setpointOutNum2);
                        endDateTime_.Add(new DateTime(1, 1, 1));
                    }
                    listSetPointFillColor_.Add(FillStyle);
                }
                //otherwise put default values
                else
                {
                    Value2_.Add(-1);
                    endDateTime_.Add(new DateTime(1, 1, 1));
                    listSetPointFillColor_.Add(null);
                }
                if (beginDTime.Visible)
                {
                    Value_.Add(-1);
                    beginDateTime_.Add(beginDT);
                    beginDTime.Text = begindtime.ToString();
                }
                else
                {
                    Value_.Add(setpointOutNum);
                    beginDateTime_.Add(new DateTime(1, 1, 1));
                }
                isActivated_.Add(true);
                if(axis_ == null)
                    axis = new List<string>();
                axis_.Add(AxisCBox.Text);

                if (chartPositions == null) // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)
                    chartPositions = new List<ChartUtils.CHART_POSITIONS>();
                chartPositions.Add((ChartUtils.CHART_POSITIONS)chartPositionComboBox.SelectedItem);

                listSetPointStrokeColor_.Add(StrokeStyle);
                if (rbtn_Area.Checked)
                    isArea_.Add(true);
                else isArea_.Add(false);
                //Add this setpoint to the setpoint list
                SetPointList.Items.Add(this.toString(SetPointList.Items.Count));
            }
            valueBox.Text = "";
            secondValueBox.Text = "";


            FillColor.Image = defaultImage;
            setpointColor.Image = defaultImage;
        }

        // A setpoint is dysplayed in the list as "Name:Value1-Value2(if exist):Axis"
        private String toString(int i)
        {
            String SetpointActivated;
            if (isActivated_[i])
                SetpointActivated = "Activated";
            else
                SetpointActivated = "Desactivated";

            if (isArea_[i])
            {
                if (Value_[i] != -1)
                {
                    return "Setpoint" + i + " : " + SetpointActivated + " : " + Value_[i] + " - " + Value2_[i] + " : " + axis_[i] + " : " + chartPositions[i];
                }
                else
                {
                    return "Setpoint" + i + " : " + SetpointActivated + " : [" + beginDateTime_[i] + "] - [" + endDateTime_[i] + "] : " + axis_[i] + " : " + chartPositions[i]; 
                }
            }
            else
            {
                if (Value_[i] != -1)
                {
                    return "Setpoint" + i + " : " + SetpointActivated + " : " + Value_[i] + " : " + axis_[i] + " : " + chartPositions[i]; 
                }
                else
                {
                    return "Setpoint" + i + " : " + SetpointActivated + " : [" + beginDateTime_[i] + "] : " + axis_[i] + " : " + chartPositions[i];
                }
            }
        }

        //Remove all the values associated to the removed setpoint
        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (SetPointList.SelectedItem != null)
            {
                Value_.RemoveAt(SetPointList.SelectedIndex);
                Value2_.RemoveAt(SetPointList.SelectedIndex);
                axis_.RemoveAt(SetPointList.SelectedIndex);
                listSetPointStrokeColor_.RemoveAt(SetPointList.SelectedIndex);
                listSetPointFillColor_.RemoveAt(SetPointList.SelectedIndex);
                isArea_.RemoveAt(SetPointList.SelectedIndex);
                beginDateTime_.RemoveAt(SetPointList.SelectedIndex);
                endDateTime_.RemoveAt(SetPointList.SelectedIndex);
                isActivated_.RemoveAt(SetPointList.SelectedIndex);
                chartPositions.RemoveAt(SetPointList.SelectedIndex);    // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)

                SetPointList.Items.RemoveAt(SetPointList.SelectedIndex);
                btn_Activate.Enabled = false;
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            double setpointOutNum;
            DateTime beginDT = new DateTime(1, 1, 1);
            DateTime endDT = new DateTime(1, 1, 1);

            //First click on edit button for a selected setpoint
            if (!editSetpoint)
            {
                if (SetPointList.SelectedItem != null)
                {
                    chartPositionComboBox.SelectedItem = chartPositions[SetPointList.SelectedIndex];    // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)
                    AxisCBox.Text = axis_[SetPointList.SelectedIndex].ToString();
                    AdaptAxisType();
                    //If the setpoint edited is an area, display its corresponding fields
                    if (isArea_[SetPointList.SelectedIndex])
                    {
                        if (beginDTime.Visible)
                            endDTime.Text = endDateTime_[SetPointList.SelectedIndex].ToString();
                        else
                            secondValueBox.Text = Value2_[SetPointList.SelectedIndex].ToString();
                        lesCouleursRemplissage = new NColorFillStyle(((NStrokeStyle)listSetPointFillColor_[SetPointList.SelectedIndex]).Color);
                        FillColor.Image = ((NFillStyle)lesCouleursRemplissage).CreatePreview(new NSize(33, 13), (NStrokeStyle)listSetPointFillColor_[SetPointList.SelectedIndex]);
                        rbtn_Area.Checked = true;
                        rbtn_Line.Checked = false;
                        
                    }
                    else
                    {
                        rbtn_Line.Checked = true;
                        rbtn_Area.Checked = false;
                    }
                    if (beginDTime.Visible)
                        beginDTime.Text = beginDateTime_[SetPointList.SelectedIndex].ToString();
                    else
                        valueBox.Text = Value_[SetPointList.SelectedIndex].ToString();
                    
                    setpointColor.Image = ((NStrokeStyle)listSetPointStrokeColor_[SetPointList.SelectedIndex]).CreatePreview(new NSize(33, 13), (NStrokeStyle)listSetPointStrokeColor_[SetPointList.SelectedIndex]);
                    editSetpoint = true;

                    btn_Add.Enabled = btn_Remove.Enabled = SetPointList.Enabled = false;
                }
                btn_Edit.Text = "Validate";
            }
            //Second click on edit button for a selected setpoint
            else
            {
                if (beginDTime.Visible)
                {
                    //If the argument passed is not an int,warn the user 
                    if (!DateTime.TryParse(beginDTime.Text, out beginDT))
                    {
                        DialogResult = DialogResult.None;
                        MessageBox.Show("Please provide a correct Setpoint Value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (beginDT < begindtime || beginDT > enddtime)
                    {
                        DialogResult = DialogResult.None;
                        MessageBox.Show("Setpoint value out of range, Min: " + begindtime + ", Max: " + enddtime, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //If the area box is checked and that the argument passed is not an int,warn the user 
                    if (rbtn_Area.Checked && !DateTime.TryParse(endDTime.Text, out endDT))
                    {
                        DialogResult = DialogResult.None;
                        MessageBox.Show("Please provide a correct second Setpoint Value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (rbtn_Area.Checked && endDT < begindtime || rbtn_Area.Checked && endDT > enddtime)
                    {
                        DialogResult = DialogResult.None;
                        MessageBox.Show("Setpoint value out of range, Min: " + begindtime + ", Max: " + enddtime, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    if (!double.TryParse(valueBox.Text, out setpointOutNum))
                    {
                        DialogResult = DialogResult.None;
                        MessageBox.Show("Please provide a correct Setpoint Value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (rbtn_Area.Checked && !double.TryParse(secondValueBox.Text, out setpointOutNum2))
                    {
                        DialogResult = DialogResult.None;
                        MessageBox.Show("Please provide a correct second Setpoint Value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                if (AxisCBox.Text == "")
                {
                    DialogResult = DialogResult.None;
                    MessageBox.Show("Please provide an axis for the Setpoint", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (chartPositionComboBox.Text == "")   // >> Bug #15147 Charts Setpoints only work for first series chart location (frame) C#8
                {
                    DialogResult = DialogResult.None;
                    MessageBox.Show("Please provide the chart postion for the Setpoint", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //Carry out the associated actions in the add method
                btn_Add_Click(sender, e);
                btn_Add.Enabled = btn_Remove.Enabled = SetPointList.Enabled = true;

                btn_Edit.Text = "Edit";
            }
        }

        // Setpoint constant line color edition, or setpoint area stroke color edition
        private void setpointColor_Click(object sender, EventArgs e)
        {
            StrokeStyle = new NStrokeStyle(Color.Black);
            if (NStrokeStyleTypeEditor.Edit(StrokeStyle, out StrokeStyle))
            {
                setpointColor.Image = StrokeStyle.CreatePreview(new NSize(33, 13), StrokeStyle);
            }
            if (editSetpoint)
                listSetPointStrokeColor_[SetPointList.SelectedIndex] = StrokeStyle;
        }

        private void rbtn_Line_CheckedChanged(object sender, EventArgs e)
        {
            secondValueBox.Enabled = endDTime.Enabled = FillColor.Enabled = endDTime.Enabled = false;
        }

        private void rbtn_Area_CheckedChanged(object sender, EventArgs e)
        {
            secondValueBox.Enabled = endDTime.Enabled = FillColor.Enabled = endDTime.Enabled = true;
        }

        // Setpoint area fill color edition
        private void FillColor_Click(object sender, EventArgs e)
        {
            FillStyle = new NStrokeStyle(Color.Black);
            if (NStrokeStyleTypeEditor.Edit(FillStyle, out FillStyle))
            {
                lesCouleursRemplissage = new NColorFillStyle(FillStyle.Color);
                FillColor.Image = ((NFillStyle)lesCouleursRemplissage).CreatePreview(new NSize(33, 13), FillStyle);
            }
            // If we change the fill color during a setpoint edition, and the fill color has not been previously chosen, indicate that it's the first affectation
            if (editSetpoint)
            {
                if (listSetPointFillColor_[SetPointList.SelectedIndex] != null)
                    // FillColorChanged is usefull here because when the user didn't change the fill color, the global FillStyle variable will be affected to null,
                    //so we'll affect the new setpoint fill color to null,what shouldn't arrive
                    FillColorChanged = true;
                listSetPointFillColor_[SetPointList.SelectedIndex] = FillStyle;
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Today;
            if ( valueBox.Text != "" || secondValueBox.Text != "")
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to quit? The values will be lost", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    btn_Add.Enabled = btn_Remove.Enabled = SetPointList.Enabled = true;
                    editSetpoint = false;
                    retrieveSetPoints();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else{}
            }
            else{
                retrieveSetPoints();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        //used to display all the setpoints available when openning the setpoint window
        internal void DisplaySetpointList()
        {
            // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)            
            if (setPoints.Count > 0)
            {
                for (int i = 0; i < Value_.Count; i++)
                    chartPositions.Add(setPoints[i].chartPosition);
            }
            else
            {
                for (int i = 0; i < Value_.Count; i++)
                    chartPositions.Add(ChartUtils.CHART_POSITIONS.TopLeft);
            }

            SetPointList.Items.Clear();
            AdaptAxisType();
            valueBox.Text = "";
            secondValueBox.Text = "";
            btn_Activate.Enabled = false;
            if (SetPointList.Items.Count == 0)
            {
                for (int i = 0; i < Value_.Count; i++)
                {
                    SetPointList.Items.Add(this.toString(i));
                }
            }

            FillColor.Image = defaultImage;
            setpointColor.Image = defaultImage;
            retrieveSetPoints();
        }

        private void AxisCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdaptAxisType();
        }

        private void AdaptAxisType()
        {
            if (AxisCBox.SelectedIndex == 0)
            {
                if (DTimeAbscissa)
                {
                    beginDTime.Visible = true;
                    endDTime.Visible = true;
                    valueBox.Visible = false;
                    secondValueBox.Visible = false;
                }
            }
            else
            {
                beginDTime.Visible = false;
                endDTime.Visible = false;
                valueBox.Visible = true;
                secondValueBox.Visible = true;
            }
        }
        //Not used, but Could be useful for the cancel button to remove from the list all the items that have been added since we have opened the setpoint window
        internal void removeAllLists()
        {
            for (int i = 0; i < Value_.Count; i++)
            {
                Value_.RemoveAt(i);
                Value2_.RemoveAt(i);
                axis_.RemoveAt(i);
                listSetPointStrokeColor_.RemoveAt(i);
                listSetPointFillColor_.RemoveAt(i);
                isArea_.RemoveAt(i);
                beginDateTime_.RemoveAt(i);
                endDateTime_.RemoveAt(i);
                isActivated_.RemoveAt(i);
                SetPointList.Items.RemoveAt(i);
            }
        }

        private void SetPointList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SetPointList.SelectedIndex != -1)
            {
                btn_Activate.Enabled = true;
                if (isActivated_[SetPointList.SelectedIndex])
                    btn_Activate.Text = "Desactivate";
                else
                    btn_Activate.Text = "Activate";
            }
        }

        private void btn_Activate_Click(object sender, EventArgs e)
        {
            isActivated_[SetPointList.SelectedIndex] = !isActivated_[SetPointList.SelectedIndex];

            if (isActivated_[SetPointList.SelectedIndex])
                btn_Activate.Text = "Desactivate";
            else
                btn_Activate.Text = "Activate";
            SetPointList.Items[SetPointList.SelectedIndex] = this.toString(SetPointList.SelectedIndex);
        }

        internal void SetAbscissaDateLimits(DateTime beginDate,DateTime EndDate)
        {
            beginDTime.Value = begindtime = beginDate;
            endDTime.Value = enddtime = EndDate;
        }

        private void retrieveSetPoints()    // >> Bug #15147 Charts Setpoints only work for first series chart location (frame)
        {
            setPoints = ChartUtils.retrieveSetPoints(Value_, Value2_, beginDateTime_, endDateTime_, isArea_, isActivated_, axis_,
                                                        chartPositions, listSetPointStrokeColor_, listSetPointFillColor_);
        }

    }
}
