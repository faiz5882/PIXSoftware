using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace SIMCORE_TOOL.Assistant
{
    internal partial class FPD_Assistant : Form
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
                    lbl_TerminalCI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    lbl_LevelCI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    lbl_CIEndEco.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    lbl_CIStartEco.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    break;

                case PAX2SIM.EnumPerimetre.PAX:
                    //Facultatif : Runway, Parking, Make-Up
                    lbl_Runway.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);

                    lbl_Terminal_Parking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    lbl_Parking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);

                    tc_Information.TabPages.Remove(tp_MakeUp);
                    break;

                case PAX2SIM.EnumPerimetre.BHS:
                case PAX2SIM.EnumPerimetre.TMS:
                    //Facultatif : Runway, Boarding gate.
                    lbl_Runway.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);

                    lbl_Terminal_BoardingGate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    lbl_Level_BoardingGate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    lbl_BoardingGate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    break;
                default :
                    return;
            }
        }
        #endregion

        #region Les différentes fonctions pour initialiser le formulaire et les différentes listes.
        private void initialiserFormulaire()
        {
            txt_FlightN.BackColor = Color.White;
            txt_NbPax.BackColor = Color.White;
            txt_Id.BackColor = Color.FromArgb(230,230,230);

            DataTable table = donneesEnCours.getTable("Input", GlobalNames.FPDTableName);
            if ((table != null) && (table.Rows.Count > 0))
            {
                dtp_DateTime.Value = (DateTime)table.Rows[table.Rows.Count - 1][GlobalNames.sFPD_A_Column_DATE];
            }
            else
            {
                dtp_DateTime.Value = DateTime.Now.Date;
            }
            //mtxt_Date.Text = DateTime.Now.Date.ToShortDateString();
            //mtxt_Time.Text = DateTime.Now.TimeOfDay.ToString();
            btn_AddAirline.Tag = new tagType(GlobalNames.FP_AirlineCodesTableName, cb_Airline, 0);
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Airline, donneesEnCours.getTable("Input", GlobalNames.FP_AirlineCodesTableName), 0);
            btn_AddAirport.Tag = new tagType(GlobalNames.FP_AirportCodesTableName, cb_Airport, 0);
            OverallTools.FonctionUtiles.initialiserTextComboBox(cb_Airport, donneesEnCours.getTable("Input", GlobalNames.FP_AirportCodesTableName), 0); // >> Bug #11327 Pax2Sim - Flight Plan editor - airport code not loaded from table
            btn_Aircraft.Tag = new tagType(GlobalNames.FP_AircraftTypesTableName, cb_AircraftType, 0);
            OverallTools.FonctionUtiles.initialiserComboBox(cb_AircraftType, donneesEnCours.getTable("Input", GlobalNames.FP_AircraftTypesTableName), 0);
            btn_AddFC.Tag = new tagType(GlobalNames.FP_FlightCategoriesTableName, cb_FlightCategory, 0);
            OverallTools.FonctionUtiles.initialiserComboBox(cb_FlightCategory, donneesEnCours.getTable("Input", GlobalNames.FP_FlightCategoriesTableName), 0);
            btn_AddFC.Enabled = donneesEnCours.TrialCanAddRow("Input", GlobalNames.FP_FlightCategoriesTableName);

            OverallTools.FonctionUtiles.initialiserComboBox(cb_TerminalCI, donneesEnCours.getTerminal());
            OverallTools.FonctionUtiles.initialiserComboBox(cb_TerminalGate, donneesEnCours.getTerminal());
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Terminal_Parking, donneesEnCours.getTerminal());
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Terminal_MakeUp, donneesEnCours.getTerminal());
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Runway, donneesEnCours.getRunWay());
            
            cb_LevelCI.Items.Clear();
            cb_LevelGate.Items.Clear();
            cb_CIStartEco.Items.Clear();
            cb_CIStartFirst.Items.Clear();
            cb_CIEndEco.Items.Clear();
            cb_CIEndFirst.Items.Clear();
            cb_EcoBagDropEnd.Items.Clear();
            cb_EcoBagDropStart.Items.Clear();
            cb_FirstBagDropEnd.Items.Clear();
            cb_FirstBagDropStart.Items.Clear();
            cb_Gate.Items.Clear();
            txt_FlightN.Clear();
            cb_TSA.Checked = false;
            if (identifiant != -1)
            {
                if (donneesEnCours.getTable("Input", GlobalNames.FPDTableName) == null)
                    return;
                btn_addNew.Visible = false;
                foreach (DataRow ligne in donneesEnCours.getTable("Input", GlobalNames.FPDTableName).Rows)
                {
                    if (FonctionsType.getInt(ligne.ItemArray[0]) == identifiant)
                    {
                        initialiserFormulaire(ligne);
                        break;
                    }
                }
            }
            OverallTools.FonctionUtiles.MajBackground(this);
            // >> Task #10175 Pax2Sim - Flight Plan table editor
            searchAirlineBtn.Tag = GlobalNames.FP_ITEM_TYPE_AIRLINE;
            searchAirportBtn.Tag = GlobalNames.FP_ITEM_TYPE_AIRPORT;
            searchAircraftTypeBtn.Tag = GlobalNames.FP_ITEM_TYPE_AIRCRAFT;
            // << Task #10175 Pax2Sim - Flight Plan table editor
        }
        private void initialiserFormulaire(DataRow ligne)
        {
            txt_Id.Text = identifiant.ToString();
            dtp_DateTime.Value = OverallTools.DataFunctions.toDateTime(ligne[GlobalNames.sFPD_A_Column_DATE], ligne[GlobalNames.sFPD_Column_STD]);
            cb_Airline.Text = ligne[GlobalNames.sFPD_A_Column_AirlineCode].ToString();
            txt_FlightN.Text = ligne[GlobalNames.sFPD_A_Column_FlightN].ToString();
            cb_Airport.Text = ligne[GlobalNames.sFPD_A_Column_AirportCode].ToString();
            cb_FlightCategory.Text = ligne[GlobalNames.sFPD_A_Column_FlightCategory].ToString();
            cb_AircraftType.Text = ligne[GlobalNames.sFPD_A_Column_AircraftType].ToString();
            txt_NbPax.Text = ligne[GlobalNames.sFPD_A_Column_NbSeats].ToString();
            cb_TSA.Checked = (Boolean)ligne[GlobalNames.sFPD_Column_TSA];
            String sValue = ligne[GlobalNames.sFPD_Column_Eco_CI_Start].ToString();
            if ((sValue != null)&&(sValue.Length != 0) && (sValue.Trim() != "0"))
            {

                OverallTools.FormFunctions.afficherElement(cb_TerminalCI, ligne[GlobalNames.sFPD_Column_TerminalCI].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                //afficherElement(cb_LevelCI, ligne["Level CI"].ToString());
                OverallTools.FormFunctions.afficherElement(cb_CIStartEco, ligne[GlobalNames.sFPD_Column_Eco_CI_Start].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_CIEndEco, ligne[GlobalNames.sFPD_Column_Eco_CI_End].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_CIStartFirst, ligne[GlobalNames.sFPD_Column_FB_CI_Start].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_CIEndFirst, ligne[GlobalNames.sFPD_Column_FB_CI_End].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_EcoBagDropStart, ligne[GlobalNames.sFPD_Column_Eco_Drop_Start].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_EcoBagDropEnd, ligne[GlobalNames.sFPD_Column_Eco_Drop_End].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_FirstBagDropStart, ligne[GlobalNames.sFPD_Column_FB_Drop_Start].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_FirstBagDropEnd, ligne[GlobalNames.sFPD_Column_FB_Drop_End].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
            }
            else
            {
                cb_TerminalCI.Text = "";
                cb_LevelCI.Text = "";
                cb_CIStartFirst.Text = "";
                cb_CIEndFirst.Text = "";
                cb_CIStartEco.Text = "";
                cb_CIEndEco.Text = "";
                cb_EcoBagDropStart.Text = "";
                cb_EcoBagDropEnd.Text = "";
                cb_FirstBagDropStart.Text = "";
                cb_FirstBagDropEnd.Text = "";
            }
            sValue = ligne[GlobalNames.sFPD_Column_BoardingGate].ToString();
            if ((sValue != null) && (sValue.Length != 0) && (sValue.Trim() != "0"))
            {
                OverallTools.FormFunctions.afficherElement(cb_TerminalGate, ligne[GlobalNames.sFPD_A_Column_TerminalGate].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                //afficherElement(cb_LevelGate, ligne["Level Gate"].ToString());
                OverallTools.FormFunctions.afficherElement(cb_Gate, ligne[GlobalNames.sFPD_Column_BoardingGate].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
            }
            else
            {
                cb_TerminalGate.Text = "";
                cb_LevelGate.Text = "";
                cb_Gate.Text = "";
            }
            sValue = ligne[GlobalNames.sFPD_A_Column_Parking].ToString();
            if ((sValue != null) && (sValue.Length != 0) && (sValue.Trim() != "0"))
            {
                OverallTools.FormFunctions.afficherElement(cb_Terminal_Parking, ligne[GlobalNames.sFPD_A_Column_TerminalParking].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_Parking, ligne[GlobalNames.sFPD_A_Column_Parking].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
            }
            else
            {
                cb_Terminal_Parking.Text = "";
                cb_Parking.Text = "";
            }

            sValue = ligne[GlobalNames.sFPD_Column_Eco_Mup_Start].ToString();
            if ((sValue != null) && (sValue.Length != 0) && (sValue.Trim() != "0"))
            {
                OverallTools.FormFunctions.afficherElement(cb_Terminal_MakeUp, ligne[GlobalNames.sFPD_Column_TerminalMup].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_Eco_Class_MakeUp, ligne[GlobalNames.sFPD_Column_Eco_Mup_Start].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_First_Class_MakeUp, ligne[GlobalNames.sFPD_Column_Eco_Mup_End].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_Start_FB_Mup, ligne[GlobalNames.sFPD_Column_First_Mup_Start].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
                OverallTools.FormFunctions.afficherElement(cb_End_FB_MUP, ligne[GlobalNames.sFPD_Column_First_Mup_End].ToString(), donneesEnCours.UseAlphaNumericForFlightInfo);
            }
            else
            {
                cb_Terminal_MakeUp.Text = "";
                cb_Eco_Class_MakeUp.Text = "";
                cb_First_Class_MakeUp.Text = "";
                cb_Start_FB_Mup.Text = "";
                cb_End_FB_MUP.Text = "";
            }

            sValue = ligne[GlobalNames.sFPD_A_Column_RunWay].ToString();
            if ((sValue != null) && (sValue.Length != 0) && (sValue.Trim() != "0"))
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

        private void initialiserComboBox(ComboBox cb, ComboBox start)
        {
            String AncienneValeur = cb.Text;
            cb.Items.Clear();
            OverallTools.FonctionUtiles.initialiserComboBox(cb, EpurerListe(start.Text, donneesEnCours.getCheckIn(cb_TerminalCI.Text/*, cb_LevelCI.Text*/,/*OverallTools.FonctionUtiles.Substring(*/start.Text/*)*/)));
            if (cb.Items.IndexOf(AncienneValeur) != -1)
            {
                cb.SelectedItem = AncienneValeur;
            }
        }

        private void initialiserCombo(ComboBox cb, ComboBox start)
        {
            String AncienneValeur = cb.Text;
            cb.Items.Clear();
            bool bAdd = false;
            for(int i=0;i<start.Items.Count;i++){
                if(start.Items[i].ToString() == start.Text)
                    bAdd = true;
                if (bAdd)
                    cb.Items.Add(start.Items[i]);
            }

            //OverallTools.FonctionUtiles.initialiserComboBox(cb, EpurerListe(start.Text, donneesEnCours.getCheckIn(cb_TerminalCI.Text/*, cb_LevelCI.Text*/,/*OverallTools.FonctionUtiles.Substring(*/start.Text/*)*/)));
            if (cb.Items.IndexOf(AncienneValeur) != -1)
            {
                cb.SelectedItem = AncienneValeur;
            }
        }

        private String[] EpurerListe(String text, String[] liste)
        {
            if (liste == null)
                return null;
            String[] resultats = new String[1];
            int debut = -1;
            for (int i = 0; i < liste.Length; i++)
            {
                if (liste[i] == text)
                {
                    resultats = new String[liste.Length - i];
                    debut = 0;
                }
                if (debut != -1)
                {
                    resultats[debut] = liste[i];
                    debut++;
                }
            }
            return resultats;
        }
        #endregion

        #region Les constructeurs de la fenêtre.
        internal FPD_Assistant(GestionDonneesHUB2SIM donneesEnCours_, PAX2SIM.EnumPerimetre Perimetre)
        {
            InitializeComponent();
            this.dtp_DateTime.CustomFormat = Application.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + Application.CurrentCulture.DateTimeFormat.LongTimePattern;
            setPerimetre(Perimetre);
            donneesEnCours = donneesEnCours_;
            identifiant = -1;
            initialiserFormulaire();
        }
        internal FPD_Assistant(GestionDonneesHUB2SIM donneesEnCours_, int identifiant_, PAX2SIM.EnumPerimetre Perimetre)
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
        private void cb_TerminalCI_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_CIStartFirst.Items.Clear();
            cb_CIStartEco.Items.Clear();
            cb_CIEndFirst.Items.Clear();
            cb_CIEndEco.Items.Clear();
            cb_EcoBagDropStart.Items.Clear();
            cb_EcoBagDropEnd.Items.Clear();
            cb_FirstBagDropStart.Items.Clear();
            cb_FirstBagDropEnd.Items.Clear();
            cb_LevelCI.Text = "";
            OverallTools.FonctionUtiles.initialiserComboBox(cb_CIStartFirst, donneesEnCours.getCheckIn(cb_TerminalCI.Text));
            OverallTools.FonctionUtiles.initialiserComboBox(cb_CIStartEco, donneesEnCours.getCheckIn(cb_TerminalCI.Text));
            OverallTools.FonctionUtiles.initialiserComboBox(cb_EcoBagDropStart, donneesEnCours.getCheckIn(cb_TerminalCI.Text));
            OverallTools.FonctionUtiles.initialiserComboBox(cb_FirstBagDropStart, donneesEnCours.getCheckIn(cb_TerminalCI.Text));
//            OverallTools.FonctionUtiles.initialiserComboBox(cb_LevelCI, donneesEnCours.getLevel(cb_TerminalCI.Text));
        }

        private void cb_TerminalGate_SelectedIndexChanged(object sender, EventArgs e)
        {
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Gate, donneesEnCours.getBoardingGate(cb_TerminalGate.Text));
            cb_LevelGate.Text = "";
//            cb_Gate.Items.Clear();
//            OverallTools.FonctionUtiles.initialiserComboBox(cb_LevelGate, donneesEnCours.getLevel(cb_TerminalGate.Text));
        }
        private void cb_CIStartFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            initialiserComboBox(cb_CIEndFirst, cb_CIStartFirst);
        }

        private void cb_CIStartEco_SelectedIndexChanged(object sender, EventArgs e)
        {
            initialiserComboBox(cb_CIEndEco, cb_CIStartEco);
            cb_LevelCI.Text = donneesEnCours.getCheckInLevel(cb_TerminalCI.Text, cb_CIStartEco.Text);
            if (cb_CIStartFirst.Text == "")
            {
                cb_CIStartFirst.Text = cb_CIStartEco.Text;
            }
        }
        private void cb_BagDropStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            initialiserComboBox(cb_EcoBagDropEnd, cb_EcoBagDropStart);
            if (cb_FirstBagDropStart.Text == "")
            {
                cb_FirstBagDropStart.Text = cb_EcoBagDropStart.Text;
            }
        }
        private void cb_FirstBagDropStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            initialiserComboBox(cb_FirstBagDropEnd, cb_FirstBagDropStart);
        }
        private void cb_CIEndEco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_CIEndFirst.Text == "")
            {
                cb_CIEndFirst.Text = cb_CIEndEco.Text;
            }
        }

        private void cb_EcoBagDropEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_FirstBagDropEnd.Text == "")
            {
                cb_FirstBagDropEnd.Text = cb_EcoBagDropEnd.Text;
            }

        }

        private void cb_Terminal_Parking_SelectedIndexChanged(object sender, EventArgs e)
        {
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Parking, donneesEnCours.getParking(cb_Terminal_Parking.Text));
        }
        private void cb_Terminal_MakeUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] MakeUp = donneesEnCours.getMakeUp(cb_Terminal_MakeUp.Text);
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Eco_Class_MakeUp, MakeUp);
            OverallTools.FonctionUtiles.initialiserComboBox(cb_First_Class_MakeUp, MakeUp);
            OverallTools.FonctionUtiles.initialiserComboBox(cb_Start_FB_Mup, MakeUp);
            OverallTools.FonctionUtiles.initialiserComboBox(cb_End_FB_MUP, MakeUp);
        }

        private void cb_MUPStartEco_SelectedIndexChanged(object sender, EventArgs e)
        {
            initialiserCombo(cb_First_Class_MakeUp, cb_Eco_Class_MakeUp);
            /*if (cb_Start_FB_Mup.Text == "")
            {
                cb_Start_FB_Mup.Text = cb_Eco_Class_MakeUp.Text;
            }*/
        }
        private void cb_MUPStartFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            initialiserCombo(cb_End_FB_MUP, cb_Start_FB_Mup);
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
            String oldValue = typ.cbTable.SelectedText;
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

                  ((cb_TerminalCI.Text == "") && (lbl_TerminalCI.Font.Bold)) ||
                  ((cb_LevelCI.Text == "") && (lbl_LevelCI.Font.Bold)) ||
                  ((cb_CIStartEco.Text == "") && (lbl_CIStartEco.Font.Bold)) ||
                  ((cb_CIEndEco.Text == "") && (lbl_CIEndEco.Font.Bold)) ||

                  ((cb_TerminalGate.Text == "") && (lbl_Terminal_BoardingGate.Font.Bold)) ||
                  ((cb_LevelGate.Text == "") && (lbl_Level_BoardingGate.Font.Bold)) ||
                  ((cb_Gate.Text == "") && (lbl_BoardingGate.Font.Bold)) ||

                  ((cb_Runway.Text == "") && (lbl_Runway.Font.Bold)) ||

                  ((cb_Terminal_Parking.Text == "") && (lbl_Terminal_Parking.Font.Bold)) ||
                  ((cb_Parking.Text == "") && (lbl_Parking.Font.Bold)) ||

                  ((cb_Terminal_MakeUp.Text == "") && (lbl_Terminal_MakeUp.Font.Bold) && (tc_Information.TabPages.Contains(tp_MakeUp))) ||
                  ((cb_Eco_Class_MakeUp.Text == "") && (lbl_Eco_Class_MakeUp.Font.Bold) && (tc_Information.TabPages.Contains(tp_MakeUp))) ||
                  ((cb_First_Class_MakeUp.Text == "") && (lbl_First_Class_MakeUp.Font.Bold) && (tc_Information.TabPages.Contains(tp_MakeUp)))
                )
            {
                MessageBox.Show("You must fill all the required fields.","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
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
            //    if (MessageBox.Show("The date range of your flight plan is greater than 7 days. The calculation could take more time. Do you want to continue ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
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
                int Id = (int)donneesEnCours.getMaxValue("Input", GlobalNames.FPDTableName, 0) + 1;
                txt_Id.Text = Id.ToString();
                DataRow newligne = donneesEnCours.getTable("Input", GlobalNames.FPDTableName).NewRow();
                newligne[GlobalNames.sFPD_A_Column_ID] = Id;
                donneesEnCours.getTable("Input", GlobalNames.FPDTableName).Rows.Add(newligne);
            }
            DataRow ligne = donneesEnCours.getLine("Input", GlobalNames.FPDTableName, 0, FonctionsType.getInt(txt_Id.Text));
            if (ligne == null)
            {
                MessageBox.Show("An error appear when try to access table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            ligne.BeginEdit();
            ligne[GlobalNames.sFPD_A_Column_DATE] = dtp_DateTime.Value.Date;

            ligne[GlobalNames.sFPD_Column_STD] = OverallTools.DataFunctions.EraseSeconds(dtp_DateTime.Value.TimeOfDay);

            ligne[GlobalNames.sFPD_A_Column_AirlineCode] = cb_Airline.Text;
            ligne[GlobalNames.sFPD_A_Column_FlightN] = txt_FlightN.Text;
            ligne[GlobalNames.sFPD_A_Column_AirportCode] = cb_Airport.Text;
            ligne[GlobalNames.sFPD_A_Column_FlightCategory] = cb_FlightCategory.Text;
            ligne[GlobalNames.sFPD_A_Column_AircraftType] = cb_AircraftType.Text;
            ligne[GlobalNames.sFPD_Column_TSA] = cb_TSA.Checked;

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
            if (cb_CIStartEco.SelectedIndex != -1)
            {
                ligne[GlobalNames.sFPD_Column_TerminalCI] = OverallTools.FonctionUtiles.Substring(cb_TerminalCI.Text);
                //ligne["Level CI"] = OverallTools.FonctionUtiles.Substring(cb_LevelCI.Text);
                ligne[GlobalNames.sFPD_Column_Eco_CI_Start] = OverallTools.FonctionUtiles.Substring(cb_CIStartEco.Text);
                ligne[GlobalNames.sFPD_Column_Eco_CI_End] = OverallTools.FonctionUtiles.Substring(cb_CIEndEco.Text);
                if (cb_CIEndFirst.SelectedIndex != -1)
                {
                    ligne[GlobalNames.sFPD_Column_FB_CI_Start] = OverallTools.FonctionUtiles.Substring(cb_CIStartFirst.Text);
                    ligne[GlobalNames.sFPD_Column_FB_CI_End] = OverallTools.FonctionUtiles.Substring(cb_CIEndFirst.Text);
                }
                else
                {
                    ligne[GlobalNames.sFPD_Column_FB_CI_Start] = OverallTools.FonctionUtiles.Substring(cb_CIStartEco.Text);
                    ligne[GlobalNames.sFPD_Column_FB_CI_End] = OverallTools.FonctionUtiles.Substring(cb_CIEndEco.Text);
                }
                if (cb_EcoBagDropEnd.SelectedIndex != -1)
                {
                    ligne[GlobalNames.sFPD_Column_Eco_Drop_Start] = OverallTools.FonctionUtiles.Substring(cb_EcoBagDropStart.Text);
                    ligne[GlobalNames.sFPD_Column_Eco_Drop_End] = OverallTools.FonctionUtiles.Substring(cb_EcoBagDropEnd.Text);
                }
                else
                {
                    ligne[GlobalNames.sFPD_Column_Eco_Drop_Start] = 0;
                    ligne[GlobalNames.sFPD_Column_Eco_Drop_End] = 0;
                }
                if (cb_FirstBagDropEnd.SelectedIndex != -1)
                {
                    ligne[GlobalNames.sFPD_Column_FB_Drop_Start] = OverallTools.FonctionUtiles.Substring(cb_FirstBagDropStart.Text);
                    ligne[GlobalNames.sFPD_Column_FB_Drop_End] = OverallTools.FonctionUtiles.Substring(cb_FirstBagDropEnd.Text);
                }
                else
                {
                    ligne[GlobalNames.sFPD_Column_FB_Drop_Start] = 0;
                    ligne[GlobalNames.sFPD_Column_FB_Drop_End] = 0;
                }
            }
            else
            {
                if (ligne[GlobalNames.sFPD_Column_TerminalCI].ToString() == "")
                {
                    ligne[GlobalNames.sFPD_Column_TerminalCI] = 0;
                    ligne[GlobalNames.sFPD_Column_Eco_CI_Start] = 0;
                    ligne[GlobalNames.sFPD_Column_Eco_CI_End] = 0;
                    ligne[GlobalNames.sFPD_Column_FB_CI_Start] = 0;
                    ligne[GlobalNames.sFPD_Column_FB_CI_End] = 0;
                    ligne[GlobalNames.sFPD_Column_Eco_Drop_Start] = 0;
                    ligne[GlobalNames.sFPD_Column_Eco_Drop_End] = 0;
                    ligne[GlobalNames.sFPD_Column_FB_Drop_Start] = 0;
                    ligne[GlobalNames.sFPD_Column_FB_Drop_End] = 0;
                }
            }
            if (cb_Gate.SelectedIndex != -1)
            {
                ligne[GlobalNames.sFPD_A_Column_TerminalGate] = OverallTools.FonctionUtiles.Substring(cb_TerminalGate.Text);
                //ligne["Level Gate"] = OverallTools.FonctionUtiles.Substring(cb_LevelGate.Text);
                ligne[GlobalNames.sFPD_Column_BoardingGate] = OverallTools.FonctionUtiles.Substring(cb_Gate.Text);
            }
            else
            {
                if (ligne[GlobalNames.sFPD_A_Column_TerminalGate].ToString() == "")
                {
                    ligne[GlobalNames.sFPD_A_Column_TerminalGate] = 0;
                    //ligne["Level Gate"] = 0;
                    ligne[GlobalNames.sFPD_Column_BoardingGate] = 0;
                }
            }
            if (cb_Parking.SelectedIndex != -1)
            {
                ligne[GlobalNames.sFPD_A_Column_TerminalParking] = OverallTools.FonctionUtiles.Substring(cb_Terminal_Parking.Text);
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
            if (tc_Information.TabPages.Contains(tp_MakeUp))
            {
                ligne[GlobalNames.sFPD_Column_TerminalMup] = OverallTools.FonctionUtiles.Substring(cb_Terminal_MakeUp.Text);
                ligne[GlobalNames.sFPD_Column_Eco_Mup_Start] = OverallTools.FonctionUtiles.Substring(cb_Eco_Class_MakeUp.Text);
                if (cb_First_Class_MakeUp.SelectedIndex == -1)
                {
                    ligne[GlobalNames.sFPD_Column_Eco_Mup_End] = ligne[GlobalNames.sFPD_Column_Eco_Mup_Start];
                }else{
                    ligne[GlobalNames.sFPD_Column_Eco_Mup_End] = OverallTools.FonctionUtiles.Substring(cb_First_Class_MakeUp.Text);
                }
                if (cb_Start_FB_Mup.SelectedIndex == -1)
                {
                    ligne[GlobalNames.sFPD_Column_First_Mup_Start] = ligne[GlobalNames.sFPD_Column_Eco_Mup_Start];
                }
                else
                {
                    ligne[GlobalNames.sFPD_Column_First_Mup_Start] = OverallTools.FonctionUtiles.Substring(cb_Start_FB_Mup.Text);
                    if (cb_End_FB_MUP.SelectedIndex == -1)
                    {
                        ligne[GlobalNames.sFPD_Column_First_Mup_End] = ligne[GlobalNames.sFPD_Column_First_Mup_Start];
                    }
                    else
                    {
                        ligne[GlobalNames.sFPD_Column_First_Mup_End] = OverallTools.FonctionUtiles.Substring(cb_End_FB_MUP.Text);
                    }
                }
            }
            else
            {
                if (ligne[GlobalNames.sFPD_Column_TerminalMup].ToString() == "")
                {
                    ligne[GlobalNames.sFPD_Column_TerminalMup] = 0;
                    ligne[GlobalNames.sFPD_Column_Eco_Mup_Start] = 0;
                    ligne[GlobalNames.sFPD_Column_Eco_Mup_End] = 0;
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
            cb_LevelGate.Text = donneesEnCours.getBoardingGateLevel(cb_TerminalGate.Text, cb_Gate.Text);
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