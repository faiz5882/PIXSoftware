using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SIMCORE_TOOL.Prompt
{
    public partial class SIM_ParametersIhm : Form
    {

        DataTable dt;
        DataTable dtProcess;

        /// <summary>
        /// Constructeur de la classe, on passe les deux tables Input et dtProcess
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtProcess"></param>
        public SIM_ParametersIhm(DataTable dt,DataTable dtProcess)
        {
            InitializeComponent();
            this.dt = dt;
            this.dtProcess = dtProcess;
        }

        /// <summary>
        /// Remplit les champs de l'assistant de parametrage Input
        /// </summary>
        internal void fillFields()
        {
            ///On remplit tous les champs avec les données contenues dans la table Input et process
            tb1.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt,0, "Extra_MES_North_Active_(1/0)")][1].ToString();
            tb2.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt,0, "Extra_MES_South_Active_(1/0)")][1].ToString();
            tb3.Text = dtProcess.Rows[OverallTools.DataFunctions.indexLigne(dtProcess, 0, "MES Short process time (sec)")][1].ToString();
            tb4.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "MES_NRO_Sorter_rate")][1].ToString();
            tb5.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "MES_NRT_Sorter_rate")][1].ToString();
            tb6.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_1")][1].ToString();
            tb7.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_2")][1].ToString();
            tb8.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_3")][1].ToString();
            tb9.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_4")][1].ToString();
            tb10.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_5")][1].ToString();

            tb10b.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_6")][1].ToString();
            tb10c.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_7")][1].ToString();
            tb10d.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_8")][1].ToString();
            tb10e.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_9")][1].ToString();
            tb10f.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_10")][1].ToString();
            tb10g.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_11")][1].ToString();

            tb11.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Delay_between_bag_suspected_at_Level_1_and_displayed_on_screen_at_Level_2_in_sec")][1].ToString();
            tb12.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_Level_3_redundancy_chute_to_the_level_3_station_:")][1].ToString();
            tb13.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_a_Level_3_station_to_the_other:")][1].ToString();
            tb14.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_a_Level_3_station_to_the_bag's_chute:")][1].ToString();
            tb15.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Machines_Availability_1_south:")][1].ToString();
            tb16.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Machines_Availability_1_north:")][1].ToString();
            tb17.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Machines_Availability_2_south:")][1].ToString();
            tb18.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Machines_Availability_2_north:")][1].ToString();
            tb19.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Reinduction_rule_1/X_bags_south:")][1].ToString();
            tb20.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Reinduction_rule_1/X_bags_north:")][1].ToString();

            tb21.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Extra_Transfer_South_Active_(1/0)")][1].ToString();
            tb22.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Extra_Transfer_North_Active_(1/0)")][1].ToString();
            tb23.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Transfer_processing_time_in_sec/bag(Mean)")][1].ToString();
            tb24.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Transfer_processing_time_in_sec/bag(Std)")][1].ToString();
            tb25.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Max_Nbr_of_Bags_Waiting_In_Front_Of_The_Regular_In-Feed_Line_To_Send_The_Next_Tug_To_The_Extra_In-Feed_Line_When_Activated_")][1].ToString();
            tb26.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Tug_travel_time_from_transfer_in-feed_in_one_hall_to_transfer_in-feed_in_the_other_hall:")][1].ToString();
            tb27.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "TimeSlot_for_the_South_EBS")][1].ToString();
            tb28.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "TimeSlot_for_the_North_EBS")][1].ToString();
            tb29.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Flight_Make-Up_Chute_Capacity")][1].ToString();
            tb30.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Delay_for_sniffing_dogs_procedure_at_transfer_in-feed_or_reclaim_belt")][1].ToString();
            tb31.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Delay_to_send_an_operator_to_deploy_a_diverter_in_case_of_a_failure_on_a_check-in_induction(sec)")][1].ToString();
            tb32.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Time_for_the_equipment_to_dock_A/C_(min):")][1].ToString();
            tb33.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Bag_unloading_from_plane_to_carts(bag/min)")][1].ToString();
            tb34.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Unloading_time_per_ULD(min):")][1].ToString();
            tb35.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_Time_from_plane_to_baggage_hall(min):")][1].ToString();
            tb36.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "TransferNorth_MES_%")][1].ToString();
            tb37.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "TransferNorth_MES_ElevatorProcessing_Time")][1].ToString();

            cb1.Checked = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "MES_Line_Open_(1/0)")][1].ToString() == "1";
            cb2.Checked = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "EBS_Line_Open_(1/0)")][1].ToString() == "1";

            tb38.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_departing_check-in_desks_to_the_OOG_check-in_desks:")][1].ToString();
            tb39.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_check-in_desk_to_OOG_Elevator:")][1].ToString();
            tb40.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_Elevator_to_bag's_chute:")][1].ToString();
            tb41.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_Baggage_Hall_Entry_to_OOG_X-ray_machine_at_ground_level:")][1].ToString();
            tb42.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_X-ray_machine_at_ground_level_to_bag's_chute:")][1].ToString();
            tb43.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_back_side_to_passenger_side:")][1].ToString();
            tb44.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_X-ray_processing_time_at_ground_level_for_transfering_bags:")][1].ToString();
            tb45.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_elevator_time_from_one_floor_to_the_other:")][1].ToString();
            tb46.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_1")][1].ToString();
            tb47.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_2")][1].ToString();
            tb48.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_3")][1].ToString();
            tb49.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_4")][1].ToString();
            tb50.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_5")][1].ToString();
            tb51.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_6")][1].ToString();
            tb52.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_7")][1].ToString();
            tb53.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_8")][1].ToString();
            tb54.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_9")][1].ToString();
            tb55.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_10")][1].ToString();
            tb56.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_11")][1].ToString();
            tb57.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "South_purcent_using_the_Xray3")][1].ToString();
            tb58.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "South_purcent_using_the_lift")][1].ToString();
            tb59.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "South_nb_Oper")][1].ToString();
            tb60.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_check-in_processing_time_/_bag:")][1].ToString();
            tb61.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "North_nb_Oper")][1].ToString();
            tb62.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_XRay_Processing_Time:")][1].ToString();
            tb63.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "%_OOG_bags_to_level_4:")][1].ToString();
            tb64.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Level_4_Processing_Time:")][1].ToString();
            tb65.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_Xray_to_other")][1].ToString();
            tb66.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "North_purcent_using_the_lift")][1].ToString();
            tb67.Text = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "North_purcent_using_the_Xray4")][1].ToString();

            cb3.Checked = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_South_Active_(1/0)")][1].ToString() == "1";
            cb4.Checked = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "XRAY3_Active_(1/0)")][1].ToString() == "1";
            cb5.Checked = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "XRAY1_Active_(1/0)")][1].ToString() == "1";
            cb6.Checked = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_North_Active_(1/0)")][1].ToString() == "1";
            cb7.Checked = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "XRAY4_Active_(1/0)")][1].ToString() == "1";
            cb8.Checked = dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "XRAY2_Active_(1/0)")][1].ToString() == "1";
        
        }

        /// <summary>
        /// Sauvegarde les données de l'assistant Input
        /// </summary>
        private void saveData()
        {
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Extra_MES_North_Active_(1/0)")][1] = tb1.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Extra_MES_South_Active_(1/0)")][1] = tb2.Text;
            dtProcess.Rows[OverallTools.DataFunctions.indexLigne(dtProcess, 0, "MES Short process time (sec)")][1] = tb3.Text; ;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "MES_NRO_Sorter_rate")][1]=tb4.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "MES_NRT_Sorter_rate")][1]=tb5.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_1")][1]=tb6.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_2")][1]=tb7.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_3")][1]=tb8.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_4")][1]=tb9.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_5")][1] = tb10.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_6")][1] = tb10b.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_7")][1] = tb10c.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_8")][1] = tb10d.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_9")][1] = tb10e.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_10")][1] = tb10f.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_reclaim_to_re-check_desk_11")][1] = tb10g.Text;

            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Delay_between_bag_suspected_at_Level_1_and_displayed_on_screen_at_Level_2_in_sec")][1]=tb11.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_Level_3_redundancy_chute_to_the_level_3_station_:")][1]=tb12.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_a_Level_3_station_to_the_other:")][1]=tb13.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_a_Level_3_station_to_the_bag's_chute:")][1]=tb14.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Machines_Availability_1_south:")][1]=tb15.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Machines_Availability_1_north:")][1]=tb16.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Machines_Availability_2_south:")][1]=tb17.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Machines_Availability_2_north:")][1]=tb18.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Reinduction_rule_1/X_bags_south:")][1]=tb19.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Reinduction_rule_1/X_bags_north:")][1]=tb20.Text;

            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Extra_Transfer_South_Active_(1/0)")][1]=tb21.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Extra_Transfer_North_Active_(1/0)")][1]=tb22.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Transfer_processing_time_in_sec/bag(Mean)")][1]=tb23.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Transfer_processing_time_in_sec/bag(Std)")][1]=tb24.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Max_Nbr_of_Bags_Waiting_In_Front_Of_The_Regular_In-Feed_Line_To_Send_The_Next_Tug_To_The_Extra_In-Feed_Line_When_Activated_")][1]=tb25.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Tug_travel_time_from_transfer_in-feed_in_one_hall_to_transfer_in-feed_in_the_other_hall:")][1]=tb26.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "TimeSlot_for_the_South_EBS")][1]=tb27.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "TimeSlot_for_the_North_EBS")][1]=tb28.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Flight_Make-Up_Chute_Capacity")][1]=tb29.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Delay_for_sniffing_dogs_procedure_at_transfer_in-feed_or_reclaim_belt")][1]=tb30.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Delay_to_send_an_operator_to_deploy_a_diverter_in_case_of_a_failure_on_a_check-in_induction(sec)")][1]=tb31.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Time_for_the_equipment_to_dock_A/C_(min):")][1]=tb32.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Bag_unloading_from_plane_to_carts(bag/min)")][1]=tb33.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Unloading_time_per_ULD(min):")][1]=tb34.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_Time_from_plane_to_baggage_hall(min):")][1]=tb35.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "TransferNorth_MES_%")][1]=tb36.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "TransferNorth_MES_ElevatorProcessing_Time")][1]=tb37.Text;



            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_departing_check-in_desks_to_the_OOG_check-in_desks:")][1]=tb38.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_check-in_desk_to_OOG_Elevator:")][1]=tb39.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_Elevator_to_bag's_chute:")][1]=tb40.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_Baggage_Hall_Entry_to_OOG_X-ray_machine_at_ground_level:")][1]=tb41.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_X-ray_machine_at_ground_level_to_bag's_chute:")][1]=tb42.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_back_side_to_passenger_side:")][1]=tb43.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_X-ray_processing_time_at_ground_level_for_transfering_bags:")][1]=tb44.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_elevator_time_from_one_floor_to_the_other:")][1]=tb45.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_1")][1]=tb46.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_2")][1]=tb47.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_3")][1]=tb48.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_4")][1]=tb49.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_5")][1]=tb50.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_6")][1]=tb51.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_7")][1]=tb52.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_8")][1]=tb53.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_9")][1]=tb54.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_10")][1]=tb55.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_11")][1]=tb56.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "South_purcent_using_the_Xray3")][1]=tb57.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "South_purcent_using_the_lift")][1]=tb58.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "South_nb_Oper")][1]=tb59.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_check-in_processing_time_/_bag:")][1]=tb60.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "North_nb_Oper")][1]=tb61.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_XRay_Processing_Time:")][1]=tb62.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "%_OOG_bags_to_level_4:")][1]=tb63.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Level_4_Processing_Time:")][1]=tb64.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Travel_time_from_Xray_to_other")][1]=tb65.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "North_purcent_using_the_lift")][1]=tb66.Text;
            dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "North_purcent_using_the_Xray4")][1]=tb67.Text;

            if(cb1.Checked)
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "MES_Line_Open_(1/0)")][1] = "1";
            else
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "MES_Line_Open_(1/0)")][1] = "0";
            if(cb2.Checked)
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "EBS_Line_Open_(1/0)")][1] ="1";
            else
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "EBS_Line_Open_(1/0)")][1] ="0";
            if(cb3.Checked)
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_South_Active_(1/0)")][1]="1";
            else
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_South_Active_(1/0)")][1]="0";
            if(cb4.Checked)
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "XRAY3_Active_(1/0)")][1]="1";
            else
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "XRAY3_Active_(1/0)")][1]="0";
            if(cb5.Checked)
               dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "XRAY1_Active_(1/0)")][1]="1";
            else
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "XRAY1_Active_(1/0)")][1]="0";
            if(cb6.Checked)
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_North_Active_(1/0)")][1]="1";
            else
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "OOG_North_Active_(1/0)")][1]="0";
            if(cb7.Checked)
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "XRAY4_Active_(1/0)")][1]="1";
            else
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "XRAY4_Active_(1/0)")][1]="0";
            if(cb8.Checked )
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "XRAY2_Active_(1/0)")][1]="1";
            else
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "XRAY2_Active_(1/0)")][1] = "0";
        }



        private void btn_Ok_Click(object sender, EventArgs e)
        {
            ///Si les données entrées sont correct, on les sauvegardes dans la table Input et process
            if (OverallTools.FormFunctions.VerifData(this))
            {
                saveData();
                this.DialogResult = DialogResult.OK;
            }
            /// Sinon on affiche un message d'erreur et on reste sur la page actuelle
            else
            {
                MessageBox.Show("Some fields are not properly filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Lors du changement de valeur d'un champ, on verrifie la données entrée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_TextChanged(object sender, EventArgs e)
        {
            String sValue = ((TextBox)sender).Text;
            Double dValue;
            ///Si aucune valeur n'a été entrée, on met 0 comme valeur par défaut
            if (sValue == "")
            {
                ((TextBox)sender).Text = "0";
            }
            ///Si la valeur n'est pas de type double, on colore le champ en rouge pour dire que la valeur est incorrecte
            else if (!Double.TryParse(sValue, out dValue))
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
            ///Si la valeur est inferieur à 0 on colore le champ en rouge
            else if (dValue < 0)
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
            ///Sinon la valeur est correcte
            ((TextBox)sender).BackColor = Color.White;
        }

        /// <summary>
        /// Lors d'un changement de selection d'ongle, on met le focus sur la première textbox contenue dans cet onglet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabPage_Click(object sender, EventArgs e)
        {
            if (((TabControl)sender).SelectedTab.Name == "tabCheckIn")
                tb1.Focus();
            else if (((TabControl)sender).SelectedTab.Name == "tabXRayParameters")
                tb11.Focus();
            else if (((TabControl)sender).SelectedTab.Name == "tabTransfertInFeed")
                tb21.Focus();
            else if (((TabControl)sender).SelectedTab.Name == "tabOGGFlow")
                tb38.Focus();
        }

        /// <summary>
        /// Vérification de la validitée des données
        /// </summary>
        /// <returns></returns>
        internal Boolean DataVerification()
        {
            ///On crée 4 listes correspondantes aux 4 onglets de l'assistant de parametrage. 
            ///Chaque liste contient le nom des differents parametres que l'ongle contient
            /*"MES Short process time (sec)",*/
            ArrayList checkInParam = new ArrayList (new String[]{"Extra_MES_North_Active_(1/0)", "Extra_MES_South_Active_(1/0)", "MES_NRO_Sorter_rate", "MES_NRT_Sorter_rate", "Travel_time_from_reclaim_to_re-check_desk_1", "Travel_time_from_reclaim_to_re-check_desk_2",
            "Travel_time_from_reclaim_to_re-check_desk_3","Travel_time_from_reclaim_to_re-check_desk_4","Travel_time_from_reclaim_to_re-check_desk_5","Travel_time_from_reclaim_to_re-check_desk_6","Travel_time_from_reclaim_to_re-check_desk_7","Travel_time_from_reclaim_to_re-check_desk_8",
            "Travel_time_from_reclaim_to_re-check_desk_9", "Travel_time_from_reclaim_to_re-check_desk_10","Travel_time_from_reclaim_to_re-check_desk_11","MES_Line_Open_(1/0)", "EBS_Line_Open_(1/0)" });

            ArrayList XRaysParam = new ArrayList (new String[]{"Delay_between_bag_suspected_at_Level_1_and_displayed_on_screen_at_Level_2_in_sec","Travel_time_from_Level_3_redundancy_chute_to_the_level_3_station_:",
            "Travel_time_from_a_Level_3_station_to_the_other:","Travel_time_from_a_Level_3_station_to_the_bag's_chute:","Machines_Availability_1_south:",
            "Machines_Availability_1_north:","Machines_Availability_2_south:","Machines_Availability_2_north:", "Reinduction_rule_1/X_bags_south:","Reinduction_rule_1/X_bags_north:"});

            ArrayList TransferInFeed = new ArrayList (new String[]{"Extra_Transfer_South_Active_(1/0)", "Extra_Transfer_North_Active_(1/0)", "Transfer_processing_time_in_sec/bag(Mean)", "Transfer_processing_time_in_sec/bag(Std)", 
            "Max_Nbr_of_Bags_Waiting_In_Front_Of_The_Regular_In-Feed_Line_To_Send_The_Next_Tug_To_The_Extra_In-Feed_Line_When_Activated_", "Tug_travel_time_from_transfer_in-feed_in_one_hall_to_transfer_in-feed_in_the_other_hall:",
            "TimeSlot_for_the_South_EBS", "TimeSlot_for_the_North_EBS", "Flight_Make-Up_Chute_Capacity", "Delay_for_sniffing_dogs_procedure_at_transfer_in-feed_or_reclaim_belt", "Delay_to_send_an_operator_to_deploy_a_diverter_in_case_of_a_failure_on_a_check-in_induction(sec)",
            "Time_for_the_equipment_to_dock_A/C_(min):", "Bag_unloading_from_plane_to_carts(bag/min)", "Unloading_time_per_ULD(min):", "Travel_Time_from_plane_to_baggage_hall(min):", "TransferNorth_MES_%", "TransferNorth_MES_ElevatorProcessing_Time"});

            ArrayList OOGFlowParam = new ArrayList (new String[]{"Travel_time_from_departing_check-in_desks_to_the_OOG_check-in_desks:", "Travel_time_from_OOG_check-in_desk_to_OOG_Elevator:",
            "Travel_time_from_OOG_Elevator_to_bag's_chute:", "Travel_time_from_Baggage_Hall_Entry_to_OOG_X-ray_machine_at_ground_level:", "Travel_time_from_OOG_X-ray_machine_at_ground_level_to_bag's_chute:",
            "Travel_time_from_OOG_back_side_to_passenger_side:", "OOG_X-ray_processing_time_at_ground_level_for_transfering_bags:", "OOG_elevator_time_from_one_floor_to_the_other:", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_1",
            "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_2", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_3", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_4",
            "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_5", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_6", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_7",
            "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_8", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_9", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_10",
            "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_11", "South_purcent_using_the_Xray3", "South_purcent_using_the_lift", "South_nb_Oper", "OOG_check-in_processing_time_/_bag:", 
            "North_nb_Oper", "OOG_XRay_Processing_Time:", "%_OOG_bags_to_level_4:", "Level_4_Processing_Time:", "Travel_time_from_Xray_to_other", "North_purcent_using_the_lift", "North_purcent_using_the_Xray4",
            "OOG_South_Active_(1/0)", "XRAY3_Active_(1/0)", "XRAY1_Active_(1/0)", "OOG_North_Active_(1/0)", "XRAY4_Active_(1/0)", "XRAY2_Active_(1/0)"});


            ///Si aucune colonne de type n'est présente, on le fait savoir et on en créer une
            if (dt.Columns.Count < 3)
            {
                MessageBox.Show("Type column is missing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                dt.Columns.Add("C");
                dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, "Description")][2] = "Type";

               

                for (int i = 0; i < checkInParam.Count; i++)
                    dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, checkInParam[i].ToString())][2] = "Check In";
                for (int i = 0; i < XRaysParam.Count; i++)
                    dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, XRaysParam[i].ToString())][2] = "X-Rays Parameters";
                for (int i = 0; i < TransferInFeed.Count; i++)
                    dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, TransferInFeed[i].ToString())][2] = "Transfer In-Feed";
                for (int i = 0; i < OOGFlowParam.Count; i++)
                    dt.Rows[OverallTools.DataFunctions.indexLigne(dt, 0, OOGFlowParam[i].ToString())][2] = "OOG Flow";
            }

            ///Verification de bonne validitée des valeurs des differentes listes
            String ErrorMessage = "";
            if ((ErrorMessage = isParameterPresent(checkInParam, dt, dtProcess)) != "")
            {
                MessageBox.Show("Cannot Open Input assistant. " + ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if ((ErrorMessage = isParameterPresent(XRaysParam, dt, dtProcess)) != "")
            {
                MessageBox.Show("Cannot Open Input assistant. " + ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if ((ErrorMessage = isParameterPresent(TransferInFeed, dt, dtProcess)) != "")
            {
                MessageBox.Show("Cannot Open Input assistant. " + ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if ((ErrorMessage = isParameterPresent(OOGFlowParam, dt, dtProcess)) != "")
            {
                MessageBox.Show("Cannot Open Input assistant. " + ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else return true;
        }

        internal static Boolean DataVerification(DataTable dtInput)
        {
            ///On crée 4 listes correspondantes aux 4 onglets de l'assistant de parametrage. 
            ///Chaque liste contient le nom des differents parametres que l'ongle contient
            ArrayList checkInParam = new ArrayList (new String[]{ "Extra_MES_North_Active_(1/0)", "Extra_MES_South_Active_(1/0)","MES Short process time (sec)", "MES_NRO_Sorter_rate",
            "MES_NRT_Sorter_rate", "Travel_time_from_reclaim_to_re-check_desk_1", "Travel_time_from_reclaim_to_re-check_desk_2",
            "Travel_time_from_reclaim_to_re-check_desk_3","Travel_time_from_reclaim_to_re-check_desk_4","Travel_time_from_reclaim_to_re-check_desk_5", "Travel_time_from_reclaim_to_re-check_desk_6","Travel_time_from_reclaim_to_re-check_desk_7","Travel_time_from_reclaim_to_re-check_desk_8",
            "Travel_time_from_reclaim_to_re-check_desk_9", "Travel_time_from_reclaim_to_re-check_desk_10","Travel_time_from_reclaim_to_re-check_desk_11","MES_Line_Open_(1/0)", "EBS_Line_Open_(1/0)" });

            ArrayList XRaysParam = new ArrayList (new String[]{"Delay_between_bag_suspected_at_Level_1_and_displayed_on_screen_at_Level_2_in_sec","Travel_time_from_Level_3_redundancy_chute_to_the_level_3_station_:",
            "Travel_time_from_a_Level_3_station_to_the_other:","Travel_time_from_a_Level_3_station_to_the_bag's_chute:","Machines_Availability_1_south:",
            "Machines_Availability_1_north:","Machines_Availability_2_south:","Machines_Availability_2_north:", "Reinduction_rule_1/X_bags_south:","Reinduction_rule_1/X_bags_north:"});

            ArrayList TransferInFeed = new ArrayList (new String[]{"Extra_Transfer_South_Active_(1/0)", "Extra_Transfer_North_Active_(1/0)", "Transfer_processing_time_in_sec/bag(Mean)", "Transfer_processing_time_in_sec/bag(Std)", 
            "Max_Nbr_of_Bags_Waiting_In_Front_Of_The_Regular_In-Feed_Line_To_Send_The_Next_Tug_To_The_Extra_In-Feed_Line_When_Activated_", "Tug_travel_time_from_transfer_in-feed_in_one_hall_to_transfer_in-feed_in_the_other_hall:",
            "TimeSlot_for_the_South_EBS", "TimeSlot_for_the_North_EBS", "Flight_Make-Up_Chute_Capacity", "Delay_for_sniffing_dogs_procedure_at_transfer_in-feed_or_reclaim_belt", "Delay_to_send_an_operator_to_deploy_a_diverter_in_case_of_a_failure_on_a_check-in_induction(sec)",
            "Time_for_the_equipment_to_dock_A/C_(min):", "Bag_unloading_from_plane_to_carts(bag/min)", "Unloading_time_per_ULD(min):", "Travel_Time_from_plane_to_baggage_hall(min):", "TransferNorth_MES_%", "TransferNorth_MES_ElevatorProcessing_Time"});

            ArrayList OOGFlowParam = new ArrayList (new String[]{"Travel_time_from_departing_check-in_desks_to_the_OOG_check-in_desks:", "Travel_time_from_OOG_check-in_desk_to_OOG_Elevator:",
            "Travel_time_from_OOG_Elevator_to_bag's_chute:", "Travel_time_from_Baggage_Hall_Entry_to_OOG_X-ray_machine_at_ground_level:", "Travel_time_from_OOG_X-ray_machine_at_ground_level_to_bag's_chute:",
            "Travel_time_from_OOG_back_side_to_passenger_side:", "OOG_X-ray_processing_time_at_ground_level_for_transfering_bags:", "OOG_elevator_time_from_one_floor_to_the_other:", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_1",
            "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_2", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_3", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_4",
            "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_5", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_6", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_7",
            "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_8", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_9", "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_10",
            "Travel_time_from_OOG_double_doors_to_the_specific_reclaim_11", "South_purcent_using_the_Xray3", "South_purcent_using_the_lift", "South_nb_Oper", "OOG_check-in_processing_time_/_bag:", 
            "North_nb_Oper", "OOG_XRay_Processing_Time:", "%_OOG_bags_to_level_4:", "Level_4_Processing_Time:", "Travel_time_from_Xray_to_other", "North_purcent_using_the_lift", "North_purcent_using_the_Xray4",
            "OOG_South_Active_(1/0)", "XRAY3_Active_(1/0)", "XRAY1_Active_(1/0)", "OOG_North_Active_(1/0)", "XRAY4_Active_(1/0)", "XRAY2_Active_(1/0)"});


            ///Si aucune colonne de type n'est présente, on le fait savoir et on en créer une
            if (dtInput.Columns.Count < 3)
            {
                MessageBox.Show("Type column is missing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                dtInput.Columns.Add("C");
                dtInput.Rows[OverallTools.DataFunctions.indexLigne(dtInput, 0, "Description")][2] = "Type";

                for (int i = 0; i < checkInParam.Count; i++)
                    dtInput.Rows[OverallTools.DataFunctions.indexLigne(dtInput, 0, checkInParam[i].ToString())][2] = "Check In";
                for (int i = 0; i < XRaysParam.Count; i++)
                    dtInput.Rows[OverallTools.DataFunctions.indexLigne(dtInput, 0, XRaysParam[i].ToString())][2] = "X-Rays Parameters";
                for (int i = 0; i < TransferInFeed.Count; i++)
                    dtInput.Rows[OverallTools.DataFunctions.indexLigne(dtInput, 0, TransferInFeed[i].ToString())][2] = "Transfer In-Feed";
                for (int i = 0; i < OOGFlowParam.Count; i++)
                    dtInput.Rows[OverallTools.DataFunctions.indexLigne(dtInput, 0, OOGFlowParam[i].ToString())][2] = "OOG Flow";
            }

            ///Verification de bonne validitée des valeurs des differentes listes
            String ErrorMessage = "";
            if ((ErrorMessage = isParameterPresent(checkInParam, dtInput, null)) != "")
            {
                MessageBox.Show("Cannot Open Input assistant. " + ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if ((ErrorMessage = isParameterPresent(XRaysParam, dtInput, null)) != "")
            {
                MessageBox.Show("Cannot Open Input assistant. " + ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if ((ErrorMessage = isParameterPresent(TransferInFeed, dtInput, null)) != "")
            {
                MessageBox.Show("Cannot Open Input assistant. " + ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if ((ErrorMessage = isParameterPresent(OOGFlowParam, dtInput, null)) != "")
            {
                MessageBox.Show("Cannot Open Input assistant. " + ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else return true;
        }

        /// <summary>
        /// Fonction permettant de regarder si chaque valeur est correcte, si les champs et les types sont présents dans la table.
        /// </summary>
        /// <param name="paramType"></param>
        /// <param name="dt">table principale contenant les parametres d'Input</param>
        /// <param name="dtProcess"> table contenant le champ MES Short process time (sec)</param>
        /// <returns></returns>
        internal static String isParameterPresent(ArrayList paramType, DataTable dt, DataTable dtProcess)
        {
            DataTable dttest = dt;
            Boolean isPresent;
            for (int i = 0; i < paramType.Count; i++)
            {
                ///Si le parametre à traiter est "MES Short process time (sec)", alors on utilise la table process
                if (paramType[i].ToString() == "MES Short process time (sec)")
                    continue;
                ///Sinon utilisation de la table Input
                else
                    dttest = dt;
                isPresent = false;
                for (int j = 0; j < dttest.Rows.Count; j++)
                {
                    ///Comparaison du parametre contenue dans la liste et du parametre de la table pour pouvoir récupérer le numéro de ligne dans la table
                    if (paramType[i].ToString() == dttest.Rows[j][0].ToString())
                    {
                        isPresent = true;
                        double value;
                        ///Si la valeur contenue dans la table n'est pas null, qu'il est bien de type double et que sa valeur est bien superieur à 0
                        if (dttest.Rows[j][1].ToString() != "" && double.TryParse(dttest.Rows[j][1].ToString(), out value) && value >= 0)
                        {
                            ///Si on cherche la valeur du parametre MES Short process time (sec), on ne regarde pas si la table contient une colonne de type donc on sort
                            if (paramType[i].ToString() == "MES Short process time (sec)")
                                continue;
                            String type = dttest.Rows[j][2].ToString();
                            ///Si le parametre n'est pas MES Short process time (sec) que son type est Check In ou X-Rays Parameters ou Transfer In-Feed ou OOG Flow alors c'est bon
                            if (type == "Check In" || type == "X-Rays Parameters" || type == "Transfer In-Feed" || type == "OOG Flow")
                            {
                                continue;
                            }
                            ///Sinon on renvoit une erreur
                            else return "No type provided for parameter " + paramType[i];
                        }
                            ///Sinon on renvoit une erreur
                        else return "Wrong value for parameter " + paramType[i];
                    }
                }
                ///Si le nom du parametre n'est pas contenue dans la table on renvoit une erreur
                if (!isPresent)
                    return "Parameter " + paramType[i] + " is not present in Input table";
            }
            return "";
        }

    }
}
