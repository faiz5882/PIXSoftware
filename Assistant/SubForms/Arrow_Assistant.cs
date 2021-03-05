using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant
{
    public partial class Arrow_Assistant : Form
    {
        SubForms.Distribution_SubForm d_subForm;
        private String Origin_;
        private bool bAEteModifie;
        private bool bEnabled_;


        #region Les constructeurs et les fonctions pour initialiser l'affichage.
        public bool AEteModifie
        {
            get
            {

                return bAEteModifie || d_subForm.getValeursOntChangees();
            }
        }
        public bool bEnabled
        {
            get
            {

                return bEnabled_;
            }
            set
            {
                bEnabled_ = value;
            }
        }
        public void Initialize(GestionDonneesHUB2SIM gdh_Datas__, String Distribution, Double param1, Double param2, Double param3)
        {
            InitializeComponent();
            d_subForm = new SubForms.Distribution_SubForm(gdh_Datas__, gdh_Datas__.getTable("Input", "OneofSpecificationTable"), Distribution, param1, param2, param3);
            d_subForm.BackColor = this.BackColor;
            d_subForm.TopLevel = false;
            d_subForm.Parent = p_Distribution;
            d_subForm.Show();
            btn_del.Enabled = !PAX2SIM.bRuntime;
            tt_ToolTip.SetToolTip(cb_Suceed, "If checked, that means the link is available only when the bag has succeeded in the previous process. If unchecked, that means the link is available only when the process has failed.");
        }

        public Arrow_Assistant(GestionDonneesHUB2SIM gdh_Datas__, 
            String Origin__,
            String OriginDescription,
            String Goal,
            String GoalDescription, 
            Boolean bSucceed, 
            String Distribution, 
            Double param1, 
            Double param2, 
            Double param3)
        {
            Initialize(gdh_Datas__, Distribution, param1, param2, param3);
            setDatas(Origin__, OriginDescription, Goal, GoalDescription, bSucceed, Distribution, param1, param2, param3);
        }

        public Arrow_Assistant(GestionDonneesHUB2SIM gdh_Datas__,
            String Origin__,
            String OriginDescription,
            String Goal,
            String GoalDescription, 
            Double Weight, 
            String Distribution, 
            Double param1, 
            Double param2, 
            Double param3)
        {
            Initialize(gdh_Datas__, Distribution, param1, param2, param3);
            setDatas(Origin__,OriginDescription, Goal, GoalDescription,Weight, Distribution, param1, param2, param3);
        }
        public void setDatas(String Origin__,
            String OriginDescription,
            String Goal,
            String GoalDescription, 
            Boolean bSucceed, 
            String Distribution, 
            Double param1, 
            Double param2, 
            Double param3)
        {
            lbl_Weight.Visible = false;
            txt_Weight.Visible = false;

            cb_Suceed.Visible = Origin__.Contains("HBS");

            lbl_Description.Text = GoalDescription;
            cb_Suceed.Checked = bSucceed;
            if (!cb_Suceed.Visible)
                cb_Suceed.Checked = true;
            txt_Weight.Text = "0";
            d_subForm.setValue(Distribution, param1, param2, param3);
            txt_GoalGroupe.Text = Goal;
            Origin_ = Origin__;
            chk_Distance.Checked = false;
            bAEteModifie = false;
        }
        public void setDatas(String Origin__,
            String OriginDescription,
            String Goal,
            String GoalDescription, 
            Double Weight, 
            String Distribution, 
            Double param1, 
            Double param2, 
            Double param3)
        {
            lbl_Weight.Visible = true;
            txt_Weight.Visible = true;
            cb_Suceed.Visible = false;
            txt_Weight.Text = Weight.ToString();
            lbl_Description.Text = GoalDescription;
            d_subForm.setValue(Distribution, param1, param2, param3);
            txt_GoalGroupe.Text = Goal;
            Origin_ = Origin__;
            chk_Distance.Checked = false;
            bAEteModifie = false;
        }
        #endregion

        #region Fonctions pour les différents clics possibles sur le formulaire
        private void btn_del_Click(object sender, EventArgs e)
        {
            if(Parent.Tag.GetType() == typeof(Ressource_Assistant))
                ((Ressource_Assistant)Parent.Tag).DeleteGoal(txt_GoalGroupe.Text);
            else if (Parent.Tag.GetType() == typeof(BHS_Itinerary))
                ((BHS_Itinerary)Parent.Tag).DeleteGoal(txt_GoalGroupe.Text);
        }
        #endregion

        #region Fonctions pour la gestion des trois textbox.

        private void txt_Distance_TextChanged(object sender, EventArgs e)
        {
            if (!txt_Distance.Enabled)
                return;
            Double tmp;
            if (!Double.TryParse(txt_Distance.Text, out tmp))
                tmp = 0;
            if (tmp == 0)
                d_subForm.setValue("Constant", 0, 0, 0);
            String distrib = "Constant";
            if (OverallTools.FonctionUtiles.Tolerance != 0)
            {
                distrib = "Uniform";
            }
            Double t = OverallTools.FonctionUtiles.ConvertToTime(tmp);

            d_subForm.setValue(distrib, t, t * OverallTools.FonctionUtiles.Tolerance / 100, 0);
        }

        private void txt_Weight_TextChanged(object sender, EventArgs e)
        {
            Double tmp;
            if (!Double.TryParse(txt_Weight.Text, out tmp))
            {
                MessageBox.Show("Please enter numerical values for the weight informations", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            bAEteModifie = true;
        }
        private void chk_Distance_CheckedChanged(object sender, EventArgs e)
        {
            txt_Distance.Enabled = chk_Distance.Checked;
            txt_Distance.Focus();
        }
        #endregion

        #region Fonction afin de pouvoir valider les changements depuis l'extérieur de la fenêtre.
        public bool estValide()
        {
            Double tmp;
            if (txt_Weight.Visible)
                if (!Double.TryParse(txt_Weight.Text, out tmp))
                    return false;
            return d_subForm.estValide();
        }
        #endregion

        private void p_Distribution_Paint(object sender, PaintEventArgs e)
        {
            if (d_subForm.getValeursOntChangees())
            {
                chk_Distance.Checked = false;
            }
        }

        private void txt_Weight_Validating(object sender, CancelEventArgs e)
        {
            Double tmp;
            e.Cancel = !Double.TryParse(txt_Weight.Text, out tmp);
        }

        #region Les setteurs et getteurs de la classe.
        public String Origin
        {
            get
            {
                return Origin_;
            }
            set
            {
                Origin_ = value;
            }
        }
        public String Goal
        {
            get
            {
                return txt_GoalGroupe.Text;
            }
            set
            {
                txt_GoalGroupe.Text = value;
            }
        }
        public Double Weight
        {
            get
            {
                Double tmp;
                if (!Double.TryParse(txt_Weight.Text, out tmp))
                    return -1;
                return tmp;
            }
            set
            {
                txt_Weight.Text = value.ToString();
            }
        }
        public Boolean Succeed
        {
            get
            {
                return cb_Suceed.Checked;
            }
        }
        public String Distribution
        {
            get
            {
                return d_subForm.getDistribution();
            }
        }
        public Double Param1
        {
            get
            {
                return d_subForm.getFirstParam();
            }
        }
        public Double Param2
        {
            get
            {
                return d_subForm.getSecondParam();
            }
        }
        public Double Param3
        {
            get
            {
                return d_subForm.getThirdParam();
            }
        }
        #endregion

        private void chk_Distance_Paint(object sender, PaintEventArgs e)
        {
            chk_Distance.Text = "Distance (" + OverallTools.FonctionUtiles.DistanceInitial+")";
        }

        private void txt_Distance_EnabledChanged(object sender, EventArgs e)
        {
            if (!((TextBox)sender).Enabled)
                ((TextBox)sender).Text = "0";
        }

        private void cb_Suceed_CheckedChanged(object sender, EventArgs e)
        {
            bAEteModifie = true;
        }
    }
}