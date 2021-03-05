using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Prompt
{
    public partial class Appearence : Form
    {

        #region Constructeurs et fonctions pour initialiser la classe
        private void initialize()
        {
            nud_Angle.BackColor = Color.White;
            /*cb_theme.Items.Add(PredefinedFrame.Inspirat);
            cb_theme.Items.Add(PredefinedFrame.None);
            cb_theme.Items.Add(PredefinedFrame.Office2007Aqua);
            cb_theme.Items.Add(PredefinedFrame.Office2007Blue);
            cb_theme.Items.Add(PredefinedFrame.OpusAlpha);
            cb_theme.Items.Add(PredefinedFrame.Simple);
            cb_theme.Items.Add(PredefinedFrame.VistaPlus.ToString());
            cb_theme.Items.Add(PredefinedFrame.VistaRoyal);
            cb_theme.Items.Add(PredefinedFrame.VistaSlate);
            if (NUIManager.FrameAppearance != null)
            {
                cb_theme.SelectedItem = NUIManager.FrameAppearance.Name;
            }
            else
            {
                cb_theme.SelectedItem = PredefinedFrame.None;
            }*/
            initializeColor(btn_FirstColor, PAX2SIM.Couleur1);
            initializeColor(btn_SecondColor, PAX2SIM.Couleur2);
            nud_Angle.Value = PAX2SIM.Angle;
            OverallTools.FonctionUtiles.MajBackground(this);
        }
        private void initializeColor(Button btn, Color couleur)
        {
            btn.Image = OverallTools.FonctionUtiles.dessinerFondEcran(btn.Width, btn.Height, couleur, couleur, 90);
            btn.Tag = couleur;
        }
        public Appearence()
        {
            InitializeComponent();
            initialize();
        }
        #endregion

        #region Gestion des boutons et de la liste déroulante
        private void btn_ok_Click(object sender, EventArgs e)
        {
            PAX2SIM.Couleur1 = (Color)btn_FirstColor.Tag;
            PAX2SIM.Couleur2 = (Color)btn_SecondColor.Tag;
            PAX2SIM.Angle = (int)nud_Angle.Value;
        }
        #endregion

        private void btn_Color_Click(object sender, EventArgs e)
        {
            cd_ChooseColor.Color = (Color)(((Button)sender).Tag);
            if (cd_ChooseColor.ShowDialog() == DialogResult.OK)
            {
                initializeColor((Button)sender,cd_ChooseColor.Color);
                this.BackgroundImage = OverallTools.FonctionUtiles.dessinerFondEcran(this.Width, this.Height, (Color)btn_FirstColor.Tag, (Color)btn_SecondColor.Tag, (float)nud_Angle.Value);
            }
        }

        private void nud_Angle_ValueChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = OverallTools.FonctionUtiles.dessinerFondEcran(this.Width, this.Height, (Color)btn_FirstColor.Tag, (Color)btn_SecondColor.Tag, (float)nud_Angle.Value);
        }
    }
}