using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant
{
    public partial class Oneof_AddFrequency_Assistant : Form
    {
        public Double Frequency_;
        public Double Value_;

        private void Initialize(Double Frequency, Double Value)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            Frequency_ = Frequency;
            Value_ = Value;
            TXT_Frequency.Text = Frequency.ToString();
            TXT_Value.Text = Value.ToString();
        }
        public Oneof_AddFrequency_Assistant()
        {
            Initialize(0, 0);
        }
        public Oneof_AddFrequency_Assistant(Double Frequency, Double Value)
        {
            Initialize(Frequency, Value);
        }

        private void TXT_Frequency_TextChanged(object sender, EventArgs e)
        {
            if(sender.GetType()!=typeof(TextBox))
                return;
            if ((!Double.TryParse(TXT_Frequency.Text, out Frequency_)) ||
                    (!Double.TryParse(TXT_Value.Text, out Value_)))
            {
                BTN_Ok.Enabled = false;
            }
            else
            {
                BTN_Ok.Enabled = true;
            }
        }

        private void BTN_Ok_Click(object sender, EventArgs e)
        {

            if ((!Double.TryParse(TXT_Frequency.Text, out Frequency_)) ||
                (!Double.TryParse(TXT_Value.Text, out Value_)))
            {
                MessageBox.Show("Invalide value in the fields. Please check the values.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.None;
            }else if ((Frequency_ <= 0))
            {
                MessageBox.Show("Invalide value in the fields. The frequency must be greater than 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.None;
            }
            else if ((Value_ < 0))
            {
                MessageBox.Show("Invalide value in the fields. The value must be greater or equal to 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.None;
            }
        }
    }
}