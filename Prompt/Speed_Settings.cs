using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class Speed_Settings : Form
    {
        #region la classe TimeUnits
        private class TimeUnits
        {
            public String Name_;
            public String Initial_;

            public TimeUnits(String Name, String Initial)
            {
                Name_ = Name;
                Initial_ = Initial;
            }
            public override string ToString()
            {
                return Name_;
            }
        }
        #endregion

        #region Les construteurs de la classe et la fonction d'inialisation .

        private void Initialize()
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            txt_speed.BackColor = Color.White;
            txt_tolerance.BackColor = Color.White;
            txt_DistanceUnit.BackColor = Color.White;
            txt_TimeInitial.BackColor = Color.White;
            txt_DistanceInitial.BackColor = Color.White;
            txt_AllocationStep.BackColor = Color.White;
            cb_TimeUnit.Items.Add(new TimeUnits("Seconds","s"));
            cb_TimeUnit.Items.Add(new TimeUnits("Minutes","mn"));
            cb_TimeUnit.Items.Add(new TimeUnits("Hours","h"));
            cb_TimeUnit.SelectedIndex = 0;
        }
        public Speed_Settings(String DistanceUnit, String DistanceInitial, String TimeUnit, Double Speed, Double Tolerance, Double AllocationStep, Double[] tdLevels)
        {
            Initialize();
            txt_DistanceUnit.Text = DistanceUnit;
            txt_DistanceInitial.Text = DistanceInitial;
            txt_speed.Text = Speed.ToString();
            txt_tolerance.Text = Tolerance.ToString();
            txt_AllocationStep.Text= Math.Round(AllocationStep,2).ToString();
            foreach (TimeUnits unit in cb_TimeUnit.Items)
            {
                if (unit.ToString() == TimeUnit)
                {
                    cb_TimeUnit.SelectedItem = unit;
                    break;
                }
            }
            txt_FirstLevel.Text = tdLevels[0].ToString();
            txt_SecondLevel.Text = tdLevels[1].ToString();
            txt_ThirdLevel.Text = tdLevels[2].ToString();
        }
        #endregion

        #region Les fonctions appelées lors de changement d'initiale d'unité de mesure.
        private void txt_DistanceInitial_TextChanged(object sender, EventArgs e)
        {
            lbl_Speed.Text = txt_DistanceInitial.Text + " / " + txt_TimeInitial.Text;
        }


        private void cb_TimeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_TimeUnit.SelectedItem.GetType() != typeof(TimeUnits))
                return;
            txt_TimeInitial.Text = ((TimeUnits)cb_TimeUnit.SelectedItem).Initial_;
        }
        #endregion

        #region Fonction pour le bouton ok
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (txt_DistanceUnit.Text == "")
            {
                MessageBox.Show("Please choose an unit for the distances.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                txt_DistanceUnit.Focus();
                return;
            }
            if (txt_DistanceInitial.Text == "")
            {
                MessageBox.Show("Please choose an initial for the distances.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                txt_DistanceInitial.Focus();
                return;
            }
            Double dValue;
            if ((!Double.TryParse(txt_speed.Text, out dValue)) || (dValue <= 0))
            {
                MessageBox.Show("Please use a valid value for the speed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                txt_speed.Focus();
                return;
            }
            if (txt_tolerance.Text == "")
                txt_tolerance.Text = "0";
            if ((!Double.TryParse(txt_tolerance.Text, out dValue)) || (dValue < 0))
            {
                MessageBox.Show("Please use a valid value for the tolerance.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                txt_tolerance.Focus();
                return;
            }

            if ((!Double.TryParse(txt_AllocationStep.Text, out dValue)) || (dValue < 1))
            {
                MessageBox.Show("Please use a valid value for the allocation step.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                txt_AllocationStep.Focus();
                return;
            }


            if ((!Double.TryParse(txt_FirstLevel.Text, out dValue)) || (dValue < 1))
            {
                MessageBox.Show("Please use a valid value for the first level value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                txt_FirstLevel.Focus();
                return;
            }

            if ((!Double.TryParse(txt_SecondLevel.Text, out dValue)) || (dValue < 1))
            {
                MessageBox.Show("Please use a valid value for the second level value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                txt_SecondLevel.Focus();
                return;
            }
            if ((!Double.TryParse(txt_ThirdLevel.Text, out dValue)) || (dValue < 1))
            {
                MessageBox.Show("Please use a valid value for the third level value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                txt_ThirdLevel.Focus();
                return;
            }
        }
        #endregion

        #region Fonctions pour récupéré tous les paramètre renseignés dans cette fenêtre.
        public String getDistanceUnit()
        {
            return txt_DistanceUnit.Text;
        }
        public String getDistanceInitial()
        {
            return txt_DistanceInitial.Text;
        }
        public String getTimeUnit()
        {
            return cb_TimeUnit.Text;
        }
        public String getTimeInitial()
        {
            return txt_TimeInitial.Text;
        }
        public Double getSpeed()
        {
            Double dValue;
            if (!Double.TryParse(txt_speed.Text, out dValue))
            {
                return 0;
            }
            return dValue;
        }
        public Double getTolerance()
        {
            Double dValue;
            if (!Double.TryParse(txt_tolerance.Text, out dValue))
            {
                return 0;
            }
            return dValue;
        }
        public Double[] getLevels()
        {
            Double[] dValue = new double[3];
            if (!Double.TryParse(txt_FirstLevel.Text, out dValue[0]))
            {
                return null;
            }
            if (!Double.TryParse(txt_SecondLevel.Text, out dValue[1]))
            {
                return null;
            }
            if (!Double.TryParse(txt_ThirdLevel.Text, out dValue[2]))
            {
                return null;
            }
            return dValue;
        }

        public Double getAllocationStep()
        {
            Double dValue;
            if (!Double.TryParse(txt_AllocationStep.Text, out dValue))
            {
                return GlobalNames.PROJECT_DEFAULT_ALLOCATION_STEP; //15.0; // >> Task #13368 Project/Properties update
            }
            return dValue;
        }
        #endregion
    }
}