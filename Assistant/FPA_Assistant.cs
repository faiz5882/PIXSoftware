using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant
{
    public partial class FPA_Assistant : Form
    {

        private class tagType
        {
            public String NomTable;
            public ComboBox cbTable;
            public int iIndexColonne;
            public tagType(String NomTable_, ComboBox cbTable_, int iIndexColonne_)
            {
                cbTable = cbTable_;
                NomTable = NomTable_;
                iIndexColonne = iIndexColonne_;
            }
        }
        
        GestionDonneesHUB2SIM donneesEnCours;
        int identifiant;

        #region Fonction pour définir le périmètre de l'application (pour savoir quels champs sont obligatoires)
        private void setPerimetre(PAX2SIM.EnumPerimetre Perimetre)
        {
            switch (Perimetre)
            {
                case PAX2SIM.EnumPerimetre.HUB:
                    break;
                case PAX2SIM.EnumPerimetre.AIR:
                    //Facultatif : Check in
                    lbl_TerminalReclaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    lbl_LevelReclaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    lbl_Reclaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    break;
                case PAX2SIM.EnumPerimetre.PAX:
                    //Facultatif : Runway, Parking, transferInfeed
                    lbl_Runway.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);

                    lbl_TerminalParking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    lbl_Parking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    tc_information.TabPages.Remove(tp_TransferInfeed);
                    break;

                case PAX2SIM.EnumPerimetre.BHS:
                case PAX2SIM.EnumPerimetre.TMS:
                    //Facultatif : Runway, arrival gate, parking
                    lbl_Runway.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);

                    lbl_TerminalGate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    lbl_LevelGate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    lbl_Gate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);

                    lbl_TerminalParking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    lbl_Parking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    break;
                default:
                    return;
            }
        }
        #endregion

        #region Les différentes fonctions pour initialiser le formulaire et les différentes listes.
        private void initialiserFormulaire()
        {

            txt_FlightN.BackColor = Color.White;
            txt_NbPax.BackColor = Color.White;
            txt_NbPax.BackColor = Color.White;
            tt_Tooltip.SetToolTip(txt_NbPax, "Overrides Aircraft Type and Loading Factor if entered.");
            tt_Tooltip.SetToolTip(lbl_NbPax, "Overrides Aircraft Type and Loading Factor if entered.");
            txt_NbPax.Text = "";
            txt_Id.BackColor = Color.FromArgb(230, 230, 230);

            DataTable table = donneesEnCours.getTable("Input", GlobalNames.FPATableName);
            if ((table != null) && (table.Rows.Count > 0))
            {
                dtp_DateTime.Value = (DateTime)table.Rows[table.Rows.Count - 1][GlobalNames.sFPD_A_Column_DATE];
            }
            else
            {
                dtp_DateTime.Value = DateTime.Now.Date;
            }
            btn_AddAirline.Tag = new tagType(GlobalNames.FP_AirlineCodesTableName, cb_Airline, 0);
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Airline, donneesEnCours.getTable("Input", GlobalNames.FP_AirlineCodesTableName), 0);
            btn_AddAirport.Tag = new tagType(GlobalNames.FP_AirportCodesTableName, cb_Airport, 0);
            OverallTools.FonctionUtiles.initialiserTextComboBox(cb_Airport, donneesEnCours.getTable("Input", GlobalNames.FP_AirportCodesTableName), 0); // >> Bug #11327 Pax2Sim - Flight Plan editor - airport code not loaded from table
            btn_Aircraft.Tag = new tagType(GlobalNames.FP_AircraftTypesTableName, cb_AircraftType, 0);
            OverallTools.FonctionUtiles.initialiserComboBox(cb_AircraftType, donneesEnCours.getTable("Input", GlobalNames.FP_AircraftTypesTableName), 0);
            btn_AddFC.Tag = new tagType(GlobalNames.FP_FlightCategoriesTableName, cb_FlightCategory, 0);
            OverallTools.FonctionUtiles.initialiserComboBox(cb_FlightCategory, donneesEnCours.getTable("Input", GlobalNames.FP_FlightCategoriesTableName), 0);

            btn_AddFC.Enabled = donneesEnCours.TrialCanAddRow("Input", GlobalNames.FP_FlightCategoriesTableName);

            OverallTools.FonctionUtiles.initialiserComboBox(cb_TerminalGate, donneesEnCours.getTerminal());
            OverallTools.FonctionUtiles.initialiserComboBox(cb_TerminalReclaim, donneesEnCours.getTerminal());


            OverallTools.FonctionUtiles.initialiserComboBox(cb_TerminalParking, donneesEnCours.getTerminal());
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Terminal_TransferInfeed, donneesEnCours.getTerminal());
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Runway, donneesEnCours.getRunWay());



            cb_LevelGate.Items.Clear();
            cb_LevelReclaim.Items.Clear();
            cb_Reclaim.Items.Clear();
            cb_Gate.Items.Clear();
            cb_Parking.Items.Clear();
            cb_TransferInfeed.Items.Clear();
            cb_ArrivalInfeedEnd.Items.Clear();
            cb_ArrivalInfeedStart.Items.Clear();


            txt_FlightN.Clear();
            cb_TSA.Checked = false;

            if (identifiant != -1)
            {
                if (donneesEnCours.getTable("Input", GlobalNames.FPATableName) == null)
                    return;
                btn_addNew.Visible = false;
                foreach (DataRow ligne in donneesEnCours.getTable("Input", GlobalNames.FPATableName).Rows)
                {
                    if (FonctionsType.getInt(ligne.ItemArray[0], ligne.ItemArray[0].GetType()) == identifiant)
                    {
                        initialiserFormulaire(ligne);
                    }
                }
            }
            OverallTools.FonctionUtiles.MajBackground(this);
            // >> Task #10175 Pax2Sim - Flight Plan table editor
            btnSearchAirline.Tag = GlobalNames.FP_ITEM_TYPE_AIRLINE;
            btnSearchAirport.Tag = GlobalNames.FP_ITEM_TYPE_AIRPORT;
            btnSearchAircraftType.Tag = GlobalNames.FP_ITEM_TYPE_AIRCRAFT;
            // << Task #10175 Pax2Sim - Flight Plan table editor
        }
        private void initialiserFormulaire(DataRow ligne)
        {
            txt_Id.Text = identifiant.ToString();
            dtp_DateTime.Value = OverallTools.DataFunctions.toDateTime(ligne[GlobalNames.sFPD_A_Column_DATE], ligne[GlobalNames.sFPA_Column_STA]);
            cb_Airline.Text = ligne[GlobalNames.sFPD_A_Column_AirlineCode].ToString();
            txt_FlightN.Text = ligne[GlobalNames.sFPD_A_Column_FlightN].ToString();
            cb_TSA.Checked = (Boolean)ligne[GlobalNames.sFPA_Column_NoBSM];
            cb_CBP.Checked = (Boolean)ligne[GlobalNames.sFPA_Column_CBP];
            cb_Airport.Text = ligne[GlobalNames.sFPD_A_Column_AirportCode].ToString();
            cb_FlightCategory.Text = ligne[GlobalNames.sFPD_A_Column_FlightCategory].ToString();
            cb_AircraftType.Text = ligne[GlobalNames.sFPD_A_Column_AircraftType].ToString();
            txt_NbPax.Text = ligne[GlobalNames.sFPD_A_Column_NbSeats].ToString();
            if (ligne[GlobalNames.sFPA_Column_ArrivalGate].ToString() != "0")
            {

                OverallTools.FormFunctions.afficherElement(cb_TerminalGate, ligne[GlobalNames.sFPD_A_Column_TerminalGate].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                //afficherElement(cb_LevelGate, ligne["Level Gate"].ToString());
                OverallTools.FormFunctions.afficherElement(cb_Gate, ligne[GlobalNames.sFPA_Column_ArrivalGate].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
            }
            else
            {
                cb_TerminalGate.Text = "";
                cb_LevelGate.Text = "";
                cb_Gate.Text = "";
            }

            if (ligne[GlobalNames.sFPA_Column_ReclaimObject].ToString() != "0")
            {
                OverallTools.FormFunctions.afficherElement(cb_TerminalReclaim, ligne[GlobalNames.sFPA_Column_TerminalReclaim].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                //afficherElement(cb_LevelReclaim, ligne["Level Reclaim"].ToString());
                OverallTools.FormFunctions.afficherElement(cb_Reclaim, ligne[GlobalNames.sFPA_Column_ReclaimObject].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
            }
            else
            {
                cb_TerminalReclaim.Text = "";
                cb_LevelReclaim.Text = "";
                cb_Reclaim.Text = "";
            }
            if (ligne[GlobalNames.sFPD_A_Column_TerminalParking].ToString() != "0")
            {
                OverallTools.FormFunctions.afficherElement(cb_TerminalParking, ligne[GlobalNames.sFPD_A_Column_TerminalParking].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_Parking, ligne[GlobalNames.sFPD_A_Column_Parking].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
            }
            else
            {
                cb_TerminalParking.Text = "";
                cb_Parking.Text = "";
            }
            if ((ligne[GlobalNames.sFPA_Column_TransferInfeedObject].ToString() != "0")||
                (ligne[GlobalNames.sFPA_Column_StartArrivalInfeedObject].ToString() != "0"))
            {
                OverallTools.FormFunctions.afficherElement(cb_Terminal_TransferInfeed, ligne[GlobalNames.sFPA_Column_TerminalInfeedObject].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_TransferInfeed, ligne[GlobalNames.sFPA_Column_TransferInfeedObject].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_ArrivalInfeedStart, ligne[GlobalNames.sFPA_Column_StartArrivalInfeedObject].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_ArrivalInfeedEnd, ligne[GlobalNames.sFPA_Column_EndArrivalInfeedObject].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
            }
            else
            {
                cb_Terminal_TransferInfeed.Text = "";
                cb_TransferInfeed.Text = "";
                cb_ArrivalInfeedEnd.Text = "";
                cb_ArrivalInfeedStart.Text = "";
            }

            if (ligne[GlobalNames.sFPD_A_Column_RunWay].ToString() != "0")
            {
                OverallTools.FormFunctions.afficherElement(cb_Runway, ligne[GlobalNames.sFPD_A_Column_RunWay].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
            }
            else
            {
                cb_Runway.Text = "";
            }
            txt_Comment1.Text = ligne[GlobalNames.sFPD_A_Column_User1].ToString();
            txt_Comment2.Text = ligne[GlobalNames.sFPD_A_Column_User2].ToString();
            txt_Comment3.Text = ligne[GlobalNames.sFPD_A_Column_User3].ToString();
            txt_Comment4.Text = ligne[GlobalNames.sFPD_A_Column_User4].ToString();
            txt_Comment5.Text = ligne[GlobalNames.sFPD_A_Column_User5].ToString();
        }

        #endregion

        #region Les constructeurs de la fenêtre.
        internal FPA_Assistant(GestionDonneesHUB2SIM donneesEnCours_, PAX2SIM.EnumPerimetre Perimetre)
        {
            InitializeComponent();
            this.dtp_DateTime.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + Application.CurrentCulture.DateTimeFormat.LongTimePattern;
            setPerimetre(Perimetre);
            donneesEnCours = donneesEnCours_;
            identifiant = -1;
            initialiserFormulaire();
        }
        internal FPA_Assistant(GestionDonneesHUB2SIM donneesEnCours_, int identifiant_, PAX2SIM.EnumPerimetre Perimetre)
        {
            
            InitializeComponent();
            this.dtp_DateTime.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + Application.CurrentCulture.DateTimeFormat.LongTimePattern;
            setPerimetre(Perimetre);
            donneesEnCours = donneesEnCours_;
            identifiant = identifiant_;
            initialiserFormulaire();
        }
        #endregion

        #region Les différentes fonctions qui remettent à jour l'affichage suivant les choix de l'utilisateur.

        private void cb_TerminalGate_SelectedIndexChanged(object sender, EventArgs e)
        {
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Gate, donneesEnCours.getArrivalGate(cb_TerminalGate.Text));
            cb_LevelGate.Text = "";
//            OverallTools.FonctionUtiles.initialiserComboBox(cb_LevelGate, donneesEnCours.getLevel(cb_TerminalGate.Text));
//            cb_Gate.SelectedIndex = -1;
//            cb_Gate.Items.Clear();
        }
        private void cb_TerminalReclaim_SelectedIndexChanged(object sender, EventArgs e)
        {
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Reclaim, donneesEnCours.getBaggageClaimBelt(cb_TerminalReclaim.Text/*, cb_LevelReclaim.Text*/));
            cb_LevelReclaim.Text = "";
            //OverallTools.FonctionUtiles.initialiserComboBox(cb_LevelReclaim, donneesEnCours.getLevel(cb_TerminalReclaim.Text));
            //cb_Reclaim.SelectedIndex = -1;
            //cb_Reclaim.Items.Clear();
        }

        private void cb_TerminalParking_SelectedIndexChanged(object sender, EventArgs e)
        {
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Parking, donneesEnCours.getParking(cb_TerminalParking.Text));
        }

        private void cb_Terminal_TransferInfeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            OverallTools.FonctionUtiles.initialiserComboBox(cb_TransferInfeed, donneesEnCours.getTransferInfeed(cb_Terminal_TransferInfeed.Text));
            OverallTools.FonctionUtiles.initialiserComboBox(cb_ArrivalInfeedStart, donneesEnCours.getArrivalInfeed(cb_Terminal_TransferInfeed.Text));
            OverallTools.FonctionUtiles.initialiserComboBox(cb_ArrivalInfeedEnd, donneesEnCours.getArrivalInfeed(cb_Terminal_TransferInfeed.Text));
        }

        private void btn_AddAirline_Click(object sender, EventArgs e)
        {
            //Si le tag du bouton appelant est null, alors on ne fait rien.
            if (((Button)sender).Tag == null)
                return;
            //On récupère le nom de la table.
            String nomTable = ((tagType)((Button)sender).Tag).NomTable;
            if (!donneesEnCours.tableEstPresente("Input", nomTable) )
                return;
            tagType typ = ((tagType)((Button)sender).Tag);
            DialogResult res;
            if (typ.NomTable.ToString() == "FP_AircraftTypesTable")
            {
                FP_AssistantAircraft AssisAircraft = new FP_AssistantAircraft(donneesEnCours.getTable("Input", nomTable), "Add an item in the \"" + nomTable + "\" table.");
                res = AssisAircraft.ShowDialog();
            }
            else
            {
                FP_Assistant Assis = new FP_Assistant(donneesEnCours.getTable("Input", nomTable), "Add an item in the \"" + nomTable + "\" table.");
                res = Assis.ShowDialog();
            }
            donneesEnCours.aEteModifiee("Input",typ.NomTable);
            String oldValue = typ.cbTable.Text;
            OverallTools.FonctionUtiles.initialiserComboBox(typ.cbTable, donneesEnCours.getTable("Input", typ.NomTable), typ.iIndexColonne);
            if (res == DialogResult.Cancel)
                typ.cbTable.SelectedText = oldValue;
            else
                typ.cbTable.SelectedIndex = typ.cbTable.Items.Count - 1;
            btn_AddFC.Enabled = donneesEnCours.TrialCanAddRow("Input", GlobalNames.FP_FlightCategoriesTableName);
        }
        #endregion

        #region Les différentes fonctions pour sauvegardé les nouvelles données entrées.
        private bool EstValide()
        {

            if (((cb_Airline.Text == "") && (lbl_Airline.Font.Bold)) ||
                 (txt_FlightN.Text == "") ||
                 ((cb_Airport.Text == "") && (lbl_Airport.Font.Bold)) ||
                 (cb_FlightCategory.Text == "") ||
                 (cb_AircraftType.Text == "") ||

                 ((cb_TerminalGate.Text == "") && (lbl_TerminalGate.Font.Bold)) ||
                 ((cb_LevelGate.Text == "") && (lbl_LevelGate.Font.Bold)) ||
                 ((cb_Gate.Text == "") && (lbl_Gate.Font.Bold)) ||

                 ((cb_TerminalReclaim.Text == "") && (lbl_TerminalReclaim.Font.Bold)) ||
                 ((cb_LevelReclaim.Text == "") && (lbl_LevelReclaim.Font.Bold)) ||
                 ((cb_Reclaim.Text == "") && (lbl_Reclaim.Font.Bold)) ||

                 ((cb_Runway.Text == "") && (lbl_Runway.Font.Bold)) ||

                 ((cb_TerminalParking.Text == "") && (lbl_TerminalParking.Font.Bold)) ||
                 ((cb_Parking.Text == "") && (lbl_Parking.Font.Bold)) ||

                 ((cb_Terminal_TransferInfeed.Text == "") && (lbl_Terminal_Transfer.Font.Bold) && (tc_information.TabPages.Contains(tp_TransferInfeed))) ||
                 ((cb_TransferInfeed.Text == "") && (lbl_Transfer.Font.Bold) && (tc_information.TabPages.Contains(tp_TransferInfeed))) ||
                 ((cb_ArrivalInfeedStart.Text == "") && (lbl_Transfer.Font.Bold) && (tc_information.TabPages.Contains(tp_TransferInfeed)))||
                 ((cb_ArrivalInfeedEnd.Text == "") && (lbl_Transfer.Font.Bold) && (tc_information.TabPages.Contains(tp_TransferInfeed)))
               )
            {
                MessageBox.Show("You must fill all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            int iNbPax;
            if (txt_NbPax.Text != "")
            {
                if ((!Int32.TryParse(txt_NbPax.Text, out iNbPax)) || iNbPax < 0)
                {
                    MessageBox.Show("Please enter a numerical value for the number of passenger.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_NbPax.Focus();
                    return false;
                }
            }

            DateTime dtDateMax = OverallTools.DataFunctions.getMaxDate(donneesEnCours.getTable("Input", GlobalNames.FPDTableName), 1);
            DateTime dtDateMax2 = OverallTools.DataFunctions.getMaxDate(donneesEnCours.getTable("Input", GlobalNames.FPATableName), 1);
            if (dtDateMax < dtDateMax2)
                dtDateMax = dtDateMax2;

            DateTime dtDateMin = OverallTools.DataFunctions.getMinDate(donneesEnCours.getTable("Input", GlobalNames.FPDTableName), 1);
            DateTime dtDateMin2 = OverallTools.DataFunctions.getMinDate(donneesEnCours.getTable("Input", GlobalNames.FPATableName), 1);
            if (dtDateMin < dtDateMin2)
                dtDateMin = dtDateMin2;

            DateTime dtDate = dtp_DateTime.Value.Date;
            if ((dtDateMin == DateTime.MinValue) || (dtDateMax == DateTime.MinValue))
                return true;
            
            // >> Task #13366 Error message correction            
            //if ((dtDate > dtDateMin.AddDays(7)) ||
            //    (dtDate < dtDateMax.AddDays(-7)))
            //{
            //    if (MessageBox.Show("The date range for the entire flight plan is greater than 7 days. The calculation time may be long. Do you want to continue ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            //        return true;
            //    return false;
            //}
            // << Task #13366 Error message correction
            return true;
        }
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (!save())
            {
                DialogResult = DialogResult.None;
            }
        }
        private bool save()
        {
            if (!EstValide())
            {
                return false;
            }
            if (txt_Id.Text == "")
            {
                //Nous sommes dans le cas d'un ajout d'une nouvelle ligne.
                int Id = (int)donneesEnCours.getMaxValue("Input", GlobalNames.FPATableName, 0) + 1;
                txt_Id.Text = Id.ToString();
                DataRow newligne = donneesEnCours.getTable("Input", GlobalNames.FPATableName).NewRow();
                newligne[GlobalNames.sFPD_A_Column_ID] = Id.ToString();
                donneesEnCours.getTable("Input", GlobalNames.FPATableName).Rows.Add(newligne);
            }
            DataRow ligne = donneesEnCours.getLine("Input", GlobalNames.FPATableName, 0, FonctionsType.getInt(txt_Id.Text, typeof(String)));
            if (ligne == null)
            {
                MessageBox.Show("An error appear when try to access the table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            ligne.BeginEdit();
            ligne[GlobalNames.sFPD_A_Column_DATE] = dtp_DateTime.Value.Date;

            ligne[GlobalNames.sFPA_Column_STA] = OverallTools.DataFunctions.EraseSeconds(dtp_DateTime.Value.TimeOfDay);

            ligne[GlobalNames.sFPD_A_Column_AirlineCode] = cb_Airline.Text;
            ligne[GlobalNames.sFPD_A_Column_FlightN] = txt_FlightN.Text;
            ligne[GlobalNames.sFPA_Column_NoBSM] = cb_TSA.Checked;
            ligne[GlobalNames.sFPA_Column_CBP] = cb_CBP.Checked;
            ligne[GlobalNames.sFPD_A_Column_AirportCode] = cb_Airport.Text;
            ligne[GlobalNames.sFPD_A_Column_FlightCategory] = cb_FlightCategory.Text;
            ligne[GlobalNames.sFPD_A_Column_AircraftType] = cb_AircraftType.Text;


            if (txt_NbPax.Text != "")
            {
                int iNbPax;
                if ((Int32.TryParse(txt_NbPax.Text, out iNbPax)) || iNbPax >= 0)
                {
                    ligne[GlobalNames.sFPD_A_Column_NbSeats] = iNbPax;
                }
                else
                {
                    ligne[GlobalNames.sFPD_A_Column_NbSeats] = 0;
                }
            }
            else
            {
                ligne[GlobalNames.sFPD_A_Column_NbSeats] = 0;
            }
            if (cb_Gate.SelectedIndex != -1)
            {
                ligne[GlobalNames.sFPD_A_Column_TerminalGate] = OverallTools.FonctionUtiles.Substring(cb_TerminalGate.Text);
                //ligne["Level Gate"] = OverallTools.FonctionUtiles.Substring(cb_LevelGate.Text);
                ligne[GlobalNames.sFPA_Column_ArrivalGate] = OverallTools.FonctionUtiles.Substring(cb_Gate.Text);
            }
            else
            {
                if (ligne[GlobalNames.sFPD_A_Column_TerminalGate].ToString() == "")
                {
                    // Si le contenu de la ligne était vide.
                    ligne[GlobalNames.sFPD_A_Column_TerminalGate] = 0;
                    //ligne["Level Gate"] = 0;
                    ligne[GlobalNames.sFPA_Column_ArrivalGate] = 0;
                }
            }
            if (cb_Reclaim.SelectedIndex != -1)
            {
                ligne[GlobalNames.sFPA_Column_TerminalReclaim] = OverallTools.FonctionUtiles.Substring(cb_TerminalReclaim.Text);
                //ligne["Level Reclaim"] = OverallTools.FonctionUtiles.Substring(cb_LevelReclaim.Text);
                ligne[GlobalNames.sFPA_Column_ReclaimObject] = OverallTools.FonctionUtiles.Substring(cb_Reclaim.Text);
            }
            else
            {
                if (ligne[GlobalNames.sFPA_Column_ReclaimObject].ToString() == "")
                {
                    // Si le contenu de la ligne était vide.
                    ligne[GlobalNames.sFPA_Column_TerminalReclaim] = 0;
                    //ligne["Level Reclaim"] = 0;
                    ligne[GlobalNames.sFPA_Column_ReclaimObject] = 0;
                }
            }
            
            if (tc_information.TabPages.Contains(tp_TransferInfeed))
            {
                ligne[GlobalNames.sFPA_Column_TerminalInfeedObject] = OverallTools.FonctionUtiles.Substring(cb_Terminal_TransferInfeed.Text);
                ligne[GlobalNames.sFPA_Column_TransferInfeedObject] = OverallTools.FonctionUtiles.Substring(cb_TransferInfeed.Text);
                ligne[GlobalNames.sFPA_Column_StartArrivalInfeedObject] = OverallTools.FonctionUtiles.Substring(cb_ArrivalInfeedStart.Text);
                ligne[GlobalNames.sFPA_Column_EndArrivalInfeedObject] = OverallTools.FonctionUtiles.Substring(cb_ArrivalInfeedEnd.Text);
            }
            else
            {
                if (ligne[GlobalNames.sFPA_Column_TransferInfeedObject].ToString() == "")
                {
                    ligne[GlobalNames.sFPA_Column_TransferInfeedObject] = 0;
                }

                if (ligne[GlobalNames.sFPA_Column_TerminalInfeedObject].ToString() == "")
                {
                    ligne[GlobalNames.sFPA_Column_TerminalInfeedObject] = 0;
                }
                if (ligne[GlobalNames.sFPA_Column_StartArrivalInfeedObject].ToString() == "")
                {
                    ligne[GlobalNames.sFPA_Column_StartArrivalInfeedObject] = 0;
                }
                if (ligne[GlobalNames.sFPA_Column_EndArrivalInfeedObject].ToString() == "")
                {
                    ligne[GlobalNames.sFPA_Column_EndArrivalInfeedObject] = 0;
                }
            }
            if (cb_Parking.SelectedIndex != -1)
            {
                ligne[GlobalNames.sFPD_A_Column_TerminalParking] = OverallTools.FonctionUtiles.Substring(cb_TerminalParking.Text);
                ligne[GlobalNames.sFPD_A_Column_Parking] = OverallTools.FonctionUtiles.Substring(cb_Parking.Text);
            }
            else
            {
                if (ligne[GlobalNames.sFPD_A_Column_TerminalParking].ToString() == "")
                {
                    ligne[GlobalNames.sFPD_A_Column_TerminalParking] = 0;
                    ligne[GlobalNames.sFPD_A_Column_Parking] = 0;
                }
            }
            
            if (cb_Runway.SelectedIndex != -1)
            {
                ligne[GlobalNames.sFPD_A_Column_RunWay] = OverallTools.FonctionUtiles.Substring(cb_Runway.Text);
            }
            else
            {
                if (ligne[GlobalNames.sFPD_A_Column_RunWay].ToString() == "")
                    ligne[GlobalNames.sFPD_A_Column_RunWay] = 0;
            }
            ligne[GlobalNames.sFPD_A_Column_User1] = txt_Comment1.Text;
            ligne[GlobalNames.sFPD_A_Column_User2] = txt_Comment2.Text;
            ligne[GlobalNames.sFPD_A_Column_User3] = txt_Comment3.Text;
            ligne[GlobalNames.sFPD_A_Column_User4] = txt_Comment4.Text;
            ligne[GlobalNames.sFPD_A_Column_User5] = txt_Comment5.Text;
            return true;
        }
        private void btn_addNew_Click(object sender, EventArgs e)
        {
            if (save())
            {
                txt_Id.Text = "";
                identifiant = -1;
                initialiserFormulaire();
            }
        }
        #endregion

        private void cb_Gate_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_LevelGate.Text = donneesEnCours.getArrivalGateLevel(cb_TerminalGate.Text, cb_Gate.Text);
        }

        private void cb_Reclaim_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_LevelReclaim.Text = donneesEnCours.getBaggageClaimLevel(cb_TerminalReclaim.Text, cb_Reclaim.Text);
        }

        // >> Task #10175 Pax2Sim - Flight Plan table editor
        #region Search buttons

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String searchItemType = "";
            DataTable codesTable = null;
            ComboBox codesComboBox = null;

            Button senderBtn = (Button)sender;
            if (senderBtn != null && senderBtn.Tag != null)
                searchItemType = senderBtn.Tag.ToString();

            if (searchItemType.Equals(GlobalNames.FP_ITEM_TYPE_AIRLINE))
            {
                if (!donneesEnCours.tableEstPresente("Input", GlobalNames.FP_AirlineCodesTableName))
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while selecting the Airline code. The Airline codes table was not found.");
                    return;
                }
                else
                {
                    codesTable = donneesEnCours.getTable("Input", GlobalNames.FP_AirlineCodesTableName);
                    codesComboBox = cb_Airline;
                }
            }
            else if (searchItemType.Equals(GlobalNames.FP_ITEM_TYPE_AIRPORT))
            {
                if (!donneesEnCours.tableEstPresente("Input", GlobalNames.FP_AirportCodesTableName))
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while selecting the Airport code. The Airport codes table was not found.");
                    return;
                }
                else
                {
                    codesTable = donneesEnCours.getTable("Input", GlobalNames.FP_AirportCodesTableName);
                    codesComboBox = cb_Airport;
                }
            }
            else if (searchItemType.Equals(GlobalNames.FP_ITEM_TYPE_AIRCRAFT))
            {
                if (!donneesEnCours.tableEstPresente("Input", GlobalNames.FP_AircraftTypesTableName))
                {
                    OverallTools.ExternFunctions.PrintLogFile("Error while selecting the Aircraft type code. The Aircraft type codes table was not found.");
                    return;
                }
                else
                {
                    codesTable = donneesEnCours.getTable("Input", GlobalNames.FP_AircraftTypesTableName);
                    codesComboBox = cb_AircraftType;
                }
            }

            if (codesComboBox != null && codesTable != null && codesTable.Rows.Count > 0)
            {
                String oldCode = "";
                if (codesComboBox.SelectedItem != null)
                    oldCode = codesComboBox.SelectedItem.ToString();

                FP_ItemSelection selectionAssistant = new FP_ItemSelection(codesTable,
                    searchItemType, oldCode);
                DialogResult result = selectionAssistant.ShowDialog();
                String selectedCode = selectionAssistant.selectedItemCode;
                selectionAssistant.Dispose();

                if (selectedCode != null)
                {
                    foreach (String codeInList in codesComboBox.Items)
                    {
                        if (codeInList.Trim().Equals(selectedCode))
                        {
                            codesComboBox.SelectedItem = codeInList;
                            break;
                        }
                    }
                }
                else
                {
                    if (selectedCode == null)
                    {
                        OverallTools.ExternFunctions
                            .PrintLogFile("Error while selecting the " + searchItemType + " code. The returned value is null.");
                    }
                    else if (!codesComboBox.Items.Contains(selectedCode))
                    {
                        OverallTools.ExternFunctions
                            .PrintLogFile("Error while selecting the " + searchItemType + " code. The returned code ("
                                    + selectedCode + " ) can't be found in the " + searchItemType + " codes list.");
                    }
                }
            }
        }
        #endregion
                
        // << Task #10175 Pax2Sim - Flight Plan table editor

    }
}