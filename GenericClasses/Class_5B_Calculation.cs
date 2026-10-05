using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using BoilerModellingTool.SC;
using BoilerModellingTool.BLL;
//Changes done by Preeti --06--01-2021

namespace GenericClasses
{
    public class Class_5B_Calculation
    {
        string Boiler_ID = null;
        string Project_ID = null;
        string Section_ID = null;
        string Boiler_Load = null;
        int Objective_ID = 0;
        int iteration = 0;
        string Next_SectionID = null;

        //string Project_ID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
        //string Boiler_ID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
        //string Section_ID = "6F83D306-5D7A-4A9D-9D5F-218D4B2B1AD2";
        //int Objective_ID = 1;
        //int iteration = 0;
        //string Boiler_Load = "100%TMCR";
        string Prev_Section_ID = null;

        public Class_5B_Calculation(string BoilerID, string Project_ID,
             string BoilerLoad, int ObjectiveID, string SectionID, string PreviousSectionID, int Iteration)
        {
            this.Boiler_ID = BoilerID;
            this.Project_ID = Project_ID;
            this.Boiler_Load = BoilerLoad;
            this.Objective_ID = ObjectiveID;
            this.Section_ID = SectionID;
            this.Next_SectionID = PreviousSectionID;
            this.iteration = Iteration;
        }

        public void Calculations_for_5B()
        {
            
            Cal_5B_SC mcal_5B_SC = new Cal_5B_SC();
            Cal_5B_BLL mcal_5B_BLL = new Cal_5B_BLL();
            DataSet dt = new DataSet();

            dt = mcal_5B_BLL.Get_Input_For_5B_Calculation(Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID);

            Prev_Section_ID = mcal_5B_BLL.Get_PreVious_HE_SectionID(Boiler_ID, Project_ID, Section_ID);

            //Input
            Stream_Macros stream_Macros_BLL = new Stream_Macros();

            //D8 
            mcal_5B_SC.PSH_tube_diameter = Convert.ToDouble(dt.Tables[0].Rows[0][0].ToString());

            //D9
            mcal_5B_SC.PSH_tube_thickness = Convert.ToDouble(dt.Tables[1].Rows[0][0].ToString());

            //D10
            mcal_5B_SC.Configuration_factor_from_inlet_to_outlet_of_panel = Convert.ToDouble(dt.Tables[2].Rows[0][0].ToString());

            //D11
            mcal_5B_SC.Inlet_radiation_area = Convert.ToDouble(dt.Tables[3].Rows[0][0].ToString());

            //D12       
            mcal_5B_SC.Outlet_radiation_area = Convert.ToDouble(dt.Tables[4].Rows[0][0].ToString());

            //D13
            mcal_5B_SC.Steam_flow_area = Convert.ToDouble(dt.Tables[5].Rows[0][0].ToString());

            //D14
            mcal_5B_SC.Gas_average_flow_area = Convert.ToDouble(dt.Tables[6].Rows[0][0].ToString());

            //D15 
            mcal_5B_SC.Correction_factor_for_tube_rows = Convert.ToDouble(dt.Tables[7].Rows[0][0].ToString());
            //D16
            mcal_5B_SC.Relative_transverse_pitch = Convert.ToDouble(dt.Tables[8].Rows[0][0].ToString());

            //D17
            mcal_5B_SC.Relative_vertical_pitch = Convert.ToDouble(dt.Tables[9].Rows[0][0].ToString());

            //D18
            mcal_5B_SC.Configuration_factor_for_PSH = Convert.ToDouble(dt.Tables[10].Rows[0][0].ToString());

            //D19
            mcal_5B_SC.Platen_superheater_average_longitudinal_spacing = Convert.ToDouble(dt.Tables[11].Rows[0][0].ToString());

            //D20
            mcal_5B_SC.Platen_superheator_total_heating_area = Convert.ToDouble(dt.Tables[12].Rows[0][0].ToString());

            //D21  
            mcal_5B_SC.Side_Water_wall_heating_area_within_panel = Convert.ToDouble(dt.Tables[13].Rows[0][0].ToString());

            //D22
            mcal_5B_SC.Heating_area_of_roof_tubes_in_Upper_furnace = Convert.ToDouble(dt.Tables[14].Rows[0][0].ToString());

            //D23
            mcal_5B_SC.Heating_area_of_roof_tubes_in_PH_zone = Convert.ToDouble(dt.Tables[15].Rows[0][0].ToString());

            //D24
            mcal_5B_SC.Effective_radiation_layer_thickness = Math.Round((Convert.ToDouble(dt.Tables[16].Rows[0][0].ToString())), 2, MidpointRounding.AwayFromZero);

            //D28 need to take from 4B 
            mcal_5B_SC.Temperature_of_flue_gas_into_panel_input = Convert.ToDouble(dt.Tables[17].Rows[0][0].ToString());

            //D29 need to take from 4B 
            mcal_5B_SC.Enthalpy_of_flue_gas_into_panel_input = Convert.ToDouble(dt.Tables[18].Rows[0][0].ToString());

            //D30
            mcal_5B_SC.Heat_preservation_coefficient = Convert.ToDouble(dt.Tables[19].Rows[0][0].ToString());

            //D31 need to take from 4B
            mcal_5B_SC.Partial_pressure_of_triatomic_gases = Convert.ToDouble(dt.Tables[20].Rows[0][0].ToString());

            //D32
            mcal_5B_SC.Volume_fraction_of_water_vapor = Convert.ToDouble(dt.Tables[21].Rows[0][0].ToString());

            //D33
            mcal_5B_SC.Gas_density = Convert.ToDouble(dt.Tables[22].Rows[0][0].ToString());

            //D34
            mcal_5B_SC.Flue_gas_total_volume = Convert.ToDouble(dt.Tables[23].Rows[0][0].ToString());

            //D35
            mcal_5B_SC.Mean_diameter_of_ash_particle = Convert.ToDouble(dt.Tables[24].Rows[0][0].ToString());

            //D36
            mcal_5B_SC.Volume_fraction_of_triatomic_gases = Convert.ToDouble(dt.Tables[25].Rows[0][0].ToString());

            //D37
            mcal_5B_SC.Dimensionless_concentration_of_fly_ash = Convert.ToDouble(dt.Tables[26].Rows[0][0].ToString());

            //D38
            mcal_5B_SC.Furnace_pressure = Convert.ToDouble(dt.Tables[27].Rows[0][0].ToString());

            //D39  need to take from 4B    
            mcal_5B_SC.Radiation_flux_at_PSH_inlet = Convert.ToDouble(dt.Tables[28].Rows[0][0].ToString());

            //D47 need to take data
            mcal_5B_SC.Drum_pressure = Convert.ToDouble(dt.Tables[30].Rows[0][0].ToString());

            //D40
            mcal_5B_SC.Design_fuel_consumption = Convert.ToDouble(dt.Tables[29].Rows[0][0].ToString()) / 3.6;

            DataSet S_Para_Dataset = new DataSet();

            S_Para_Dataset = mcal_5B_BLL.Get_S_Parameter_Section_ID(Boiler_ID, Project_ID, Boiler_Load, Objective_ID, Prev_Section_ID, iteration);

            //D44  need to take from 12B

            //if (iteration != 0)
            //{
            //    mcal_5B_SC.LTSH_outlet_temperature = Convert.ToDouble(S_Para_Dataset.Tables[0].Rows[0]["Inlet_temperature"].ToString());
            //}
            //else
            //{
            //    mcal_5B_SC.LTSH_outlet_temperature = (stream_Macros_BLL.Tsat_p(mcal_5B_SC.Drum_pressure * 0.980665 + 1.01325));
            //    //mcal_5B_SC.LTSH_outlet_temperature = 430.7;
            //}
            mcal_5B_SC.LTSH_outlet_temperature = 430.66;

            //D45  need to take from 12B
            //mcal_5B_SC.LTSH_outlet_pressure = Convert.ToDouble(S_Para_Dataset.Tables[1].Rows[0]["Inlet_Pressure"].ToString());
            mcal_5B_SC.LTSH_outlet_pressure = 162.7;

            //D46 need to take data
            mcal_5B_SC.PSH_inlet_steam_pressure = mcal_5B_SC.LTSH_outlet_pressure - 0.5;

            

            //D48 need to take data
            mcal_5B_SC.Furnace_roof_pressure_within_panel_zone_input = mcal_5B_SC.Drum_pressure - 0.1;

            //D49
            mcal_5B_SC.Main_steam_flow_rate = Convert.ToDouble(dt.Tables[31].Rows[0][0].ToString()) * 1000;

            //D50
            mcal_5B_SC.De_superheating_spray_stage_1 = mcal_5B_BLL.Get_De_superheating_spray_Next_HE_Type(Boiler_ID, Project_ID, Section_ID) * 1000;

            //D51
            mcal_5B_SC.De_superheating_spray_stage_2 = Convert.ToDouble(dt.Tables[32].Rows[0][0].ToString()) * 1000;


            mcal_5B_SC.De_superheating_spray_enthalpy = Convert.ToDouble(dt.Tables[33].Rows[0][0].ToString());

            //D53 need to take data
            mcal_5B_SC.PSH_inlet_steam_temperature_design = Convert.ToDouble(dt.Tables[34].Rows[0][0].ToString());

            //D54 need to take data
            mcal_5B_SC.PSH_outlet_steam_temperature_design = Convert.ToDouble(dt.Tables[35].Rows[0][0].ToString());

            //D55 need to take data
            mcal_5B_SC.Final_SH_outlet_pressure = 155.8;

            //D58
            mcal_5B_SC.PSH_effectiveness_coefficent = Convert.ToDouble(dt.Tables[36].Rows[0][0].ToString());

            //D59
            mcal_5B_SC.Radiation_fuel_correction_coefficient = Convert.ToDouble(dt.Tables[37].Rows[0][0].ToString());

            //D60
            mcal_5B_SC.Tube_wall_fouling_emmisivity = Convert.ToDouble(dt.Tables[38].Rows[0][0].ToString());

            //----------------------------------------------------------------------------------------------
            //Calculations

            // Double Error_in_radiation_heat_transfer_from_flue_gas__to_downstream_elements;
            //* Pressure drop calculation sub module	

            //Double I1 = 44.5;
            //Double A1 = 0.192;
            //Double I2 = 31.08;
            //Double A2 = 0.2924;
            //Double I1_A1_I2_A2 = 3.3;
            //Double Pr_Drop_in_SH_sys = 7.7;
            //Double Pr_Drop_in_Platen = 5.9;


            ////Assumed Value for Calculation 
            ////F68
            //mcal_5B_SC.Convection_heat_of_panel_zone_ = 1000;
            //F69
            //mcal_5B_SC.Radiation_heat_from_flue_gas_to_heating_surface_through_panel = 170;
            ////F70
            //mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_at_panel_zone = 70;
            ////F71
            //mcal_5B_SC.Absorbed_convection_heat_of_water_wall_at_panel_zone = 150;
            ////From 2A --> 1044.2;
            //Double E = 0.00314;
            //Assumed Value for Calculation 
            //F68
            mcal_5B_SC.Convection_heat_of_panel_zone_ = 1957.87;
            //F69
            mcal_5B_SC.Radiation_heat_from_flue_gas_to_heating_surface_through_panel = 117.08;
            //F70
            mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_at_panel_zone = 63.86;
            //F71
            mcal_5B_SC.Absorbed_convection_heat_of_water_wall_at_panel_zone = 102.71;
            //From 2A --> 1044.2;
            Double E = 0.00314;


            //Loop Calculation 
            do
            {

                do
                {
                    do
                    {

                        do
                        {
                            //F_72 =F67-(F68+F69)/D30
                            mcal_5B_SC.Enthalpy_of_flue_gas_out_of_panel = Math.Round((mcal_5B_SC.Enthalpy_of_flue_gas_into_panel_input - (mcal_5B_SC.Convection_heat_of_panel_zone_ + mcal_5B_SC.Radiation_heat_from_flue_gas_to_heating_surface_through_panel) / mcal_5B_SC.Heat_preservation_coefficient), 2, MidpointRounding.AwayFromZero);

                            //F_73 ='2A'!F142
                            string input_val = mcal_5B_SC.Enthalpy_of_flue_gas_out_of_panel.ToString();

                            mcal_5B_SC.Temperature_of_flue_gas_out_of_panel = mcal_5B_BLL.Get_2A_Platen_Exit_Temp_ReqTemp(Boiler_ID, Project_ID, input_val, Boiler_Load, Objective_ID);

                            //F_74 =(F66+F73)/2
                            mcal_5B_SC.Average_flue_gas_temperature_C = Math.Round(((mcal_5B_SC.Temperature_of_flue_gas_into_panel_input + mcal_5B_SC.Temperature_of_flue_gas_out_of_panel) / 2), 2, MidpointRounding.AwayFromZero);
                            //F_75 =F74+273
                            mcal_5B_SC.Average_flue_gas_temperature_K = mcal_5B_SC.Average_flue_gas_temperature_C + 273;

                            //F_76 =F68-F70-F71
                            mcal_5B_SC.Absorbed_convection_heat_of_panel = mcal_5B_SC.Convection_heat_of_panel_zone_ - mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_at_panel_zone - mcal_5B_SC.Absorbed_convection_heat_of_water_wall_at_panel_zone;
                            //F_77 =D31*D24
                            mcal_5B_SC.Product_of_pn_and_s = Math.Round((mcal_5B_SC.Partial_pressure_of_triatomic_gases * mcal_5B_SC.Effective_radiation_layer_thickness), 4, MidpointRounding.AwayFromZero);
                            //F_78--->=10.2*((0.78+1.6*D32)/(10.2*F77)^0.5-0.1)*1-0.37*F75/1000)
                            Double Power = ((0.78 + 1.6 * mcal_5B_SC.Volume_fraction_of_water_vapor) / (Math.Pow((10.2 * mcal_5B_SC.Product_of_pn_and_s), 0.5))) - 0.1;
                            Double power1 = (1 - 0.37 * mcal_5B_SC.Average_flue_gas_temperature_K / 1000);
                            Double POWER3 = 10.2 * Power * power1;
                            mcal_5B_SC.Radiant_absorption_coefficient_of_gas = Math.Round(POWER3, 2, MidpointRounding.AwayFromZero);

                            //F_79 =43850*D33/((F75)^2*(D35)^2)^(1/3)
                            mcal_5B_SC.Radiant_absorption_coefficient_of_fly_ash = Math.Round((43850 * mcal_5B_SC.Gas_density / (Math.Pow(Math.Pow(mcal_5B_SC.Average_flue_gas_temperature_K, 2) * Math.Pow(mcal_5B_SC.Mean_diameter_of_ash_particle, 2), 0.333333333))), 2, MidpointRounding.AwayFromZero);
                            //F_80 =F78*D36+F79*D37
                            mcal_5B_SC.Radiant_absorption_coefficient_of_flue_gas_radiation = Math.Round(((mcal_5B_SC.Radiant_absorption_coefficient_of_gas * mcal_5B_SC.Volume_fraction_of_triatomic_gases) + (mcal_5B_SC.Radiant_absorption_coefficient_of_fly_ash * mcal_5B_SC.Dimensionless_concentration_of_fly_ash)), 3, MidpointRounding.AwayFromZero);
                            //F_81 =F80*D38*D24
                            mcal_5B_SC.Exponent_of_Eq_2_73 = (mcal_5B_SC.Radiant_absorption_coefficient_of_flue_gas_radiation * mcal_5B_SC.Furnace_pressure * mcal_5B_SC.Effective_radiation_layer_thickness);
                            //F_82
                            //Exponent_of_Eq_2_73=0.19;

                            mcal_5B_SC.Flue_gas_emissivity = 1 - (Math.Exp(-(mcal_5B_SC.Exponent_of_Eq_2_73)));
                            //F_83
                            mcal_5B_SC.Coefficient_considering_reradiation = 16.006 * Math.Pow(mcal_5B_SC.Temperature_of_flue_gas_into_panel_input / 1400, 4) - 57.166667 * Math.Pow(mcal_5B_SC.Temperature_of_flue_gas_into_panel_input / 1400, 3) + 71.458 * Math.Pow(mcal_5B_SC.Temperature_of_flue_gas_into_panel_input / 1400, 2) - 37.998333 * (mcal_5B_SC.Temperature_of_flue_gas_into_panel_input / 1400) + 8.35;
                            //F_84
                            mcal_5B_SC.Radiation_heat_flow_of_panel_zone = mcal_5B_SC.Radiation_flux_at_PSH_inlet;
                            //F_85
                            mcal_5B_SC.Corrected_radiative_intensity_of_panel_zone = mcal_5B_SC.Coefficient_considering_reradiation * mcal_5B_SC.Radiation_heat_flow_of_panel_zone;
                            //F_86 =(F85*D11/D40)
                            mcal_5B_SC.Inlet_direct_radiation_from_the_furnace = mcal_5B_SC.Corrected_radiative_intensity_of_panel_zone * mcal_5B_SC.Inlet_radiation_area / mcal_5B_SC.Design_fuel_consumption;
                            //Flue_gas_emissivity = 0.177;

                            //F_87
                            mcal_5B_SC.Furnace_radiation_heat_leaked_out_of_the_panel = (mcal_5B_SC.Inlet_direct_radiation_from_the_furnace * (1 - mcal_5B_SC.Flue_gas_emissivity) * mcal_5B_SC.Configuration_factor_from_inlet_to_outlet_of_panel) / mcal_5B_SC.Coefficient_considering_reradiation;
                            //F_88
                            mcal_5B_SC.Radiation_heat_absorbed_by_panel = mcal_5B_SC.Inlet_direct_radiation_from_the_furnace - mcal_5B_SC.Furnace_radiation_heat_leaked_out_of_the_panel;
                            //F_89 =F76+F88
                            mcal_5B_SC.Total_heat_absorbed_by_panel = mcal_5B_SC.Absorbed_convection_heat_of_panel + mcal_5B_SC.Radiation_heat_absorbed_by_panel;
                            //F_91----->120 coming 117.14 is there 
                            double e1 = (5.7 * Math.Pow(10, -11));

                            mcal_5B_SC.Radiation_heat_from_flue_gas_to_heating_surface_behind_panel = ((e1 * mcal_5B_SC.Flue_gas_emissivity * mcal_5B_SC.Outlet_radiation_area * Math.Pow(mcal_5B_SC.Average_flue_gas_temperature_K, 4) * mcal_5B_SC.Radiation_fuel_correction_coefficient)) / mcal_5B_SC.Design_fuel_consumption;
                            //F_92 =(F69-F91)
                            mcal_5B_SC.Error_in_radiation_heat_transfer_from_flue_gas__to_downstream_elements = mcal_5B_SC.Radiation_heat_from_flue_gas_to_heating_surface_through_panel - mcal_5B_SC.Radiation_heat_from_flue_gas_to_heating_surface_behind_panel;
                            //F_69
                            mcal_5B_SC.Radiation_heat_from_flue_gas_to_heating_surface_through_panel = mcal_5B_SC.Radiation_heat_from_flue_gas_to_heating_surface_behind_panel;

                            mcal_5B_SC.Error_in_radiation_heat_transfer_from_flue_gas__to_downstream_elements = Math.Round(mcal_5B_SC.Error_in_radiation_heat_transfer_from_flue_gas__to_downstream_elements, 3);
                        }
                        //while (Math.Abs(mcal_5B_SC.Error_in_radiation_heat_transfer_from_flue_gas__to_downstream_elements) >= 0.01);

                        while ((Math.Abs(mcal_5B_SC.Error_in_radiation_heat_transfer_from_flue_gas__to_downstream_elements) >= 0.01) || mcal_5B_SC.Error_in_radiation_heat_transfer_from_flue_gas__to_downstream_elements<0);



                        //F_93
                        mcal_5B_SC.Pressure_of_steam_into_panel = (mcal_5B_SC.PSH_inlet_steam_pressure * 0.980665 + 1.01325) / 10;
                        //F_94                                                                                  =((D55)*0.980665+1.01325)/10
                        mcal_5B_SC.Pressure_of_steam_out_of_panel = ((mcal_5B_SC.Final_SH_outlet_pressure) * 0.980665 + 1.01325) / 10;
                        //f_97
                        //mcal_5B_SC.Primary_desuperheating_water_flow_rate_kg_h = mcal_5B_SC.De_superheating_spray_stage_1;
                        ////f_98
                        //mcal_5B_SC.Primary_desuperheating_water_flow_rate_t_h = mcal_5B_SC.Primary_desuperheating_water_flow_rate_kg_h / 1000;
                        ////f_99
                        //mcal_5B_SC.Secondary_desuperheating_spray_flow_rate_kg_h = mcal_5B_SC.De_superheating_spray_stage_2;
                        ////f_100
                        //mcal_5B_SC.Secondary_desuperheating_spray_flow_rate_t_h = mcal_5B_SC.Secondary_desuperheating_spray_flow_rate_kg_h / 1000;
                        ////G_95
                        //Double Temperature_of_steam_into_panel_after_spray_attemperator_G_95 = ((((mcal_5B_SC.Main_steam_flow_rate - mcal_5B_SC.Primary_desuperheating_water_flow_rate_kg_h - mcal_5B_SC.Secondary_desuperheating_spray_flow_rate_kg_h) * stream_Macros_BLL.h_pT(mcal_5B_SC.LTSH_outlet_pressure * 10, mcal_5B_SC.LTSH_outlet_temperature) + (mcal_5B_SC.Primary_desuperheating_water_flow_rate_kg_h * mcal_5B_SC.De_superheating_spray_enthalpy))) / (mcal_5B_SC.Main_steam_flow_rate - mcal_5B_SC.Secondary_desuperheating_spray_flow_rate_kg_h));
                        ////G_96
                        //Double Temperature_of_steam_into_panel_after_spray_attemperator_G_96 = stream_Macros_BLL.T_ph(mcal_5B_SC.Pressure_of_steam_into_panel * 10, Temperature_of_steam_into_panel_after_spray_attemperator_G_95);

                        //F_95  =h_pt(D45*0.980665+1.01325,D44+0.01)
                        mcal_5B_SC.Upstream_heating_section_steam_enthalpy = stream_Macros_BLL.h_pT(mcal_5B_SC.LTSH_outlet_pressure * 0.980665 + 1.01325, mcal_5B_SC.LTSH_outlet_temperature + 0.01);


                        //F_96 =(F95*(D49-D50)+D52*D50)/D49

                        mcal_5B_SC.Enthalpy_of_steam_into_panel = (mcal_5B_SC.Upstream_heating_section_steam_enthalpy * (mcal_5B_SC.Main_steam_flow_rate - mcal_5B_SC.De_superheating_spray_stage_1) + mcal_5B_SC.De_superheating_spray_enthalpy * mcal_5B_SC.De_superheating_spray_stage_1) / mcal_5B_SC.Main_steam_flow_rate;
                        //F_97 =T_ph(D46*0.980665+1.01325,F96)

                        mcal_5B_SC.Temperature_of_steam_into_panel_after_spray_attemperator = stream_Macros_BLL.T_ph(mcal_5B_SC.PSH_inlet_steam_pressure * 0.980665 + 1.01325, mcal_5B_SC.Enthalpy_of_steam_into_panel);

                        //F_101
                        //mcal_5B_SC. = mcal_5B_SC.Main_steam_flow_rate - mcal_5B_SC.Secondary_desuperheating_spray_flow_rate_kg_h;

                        ////F_102
                        //mcal_5B_SC.Enthalpy_of_steam_out_of_the_panel = mcal_5B_SC.Enthalpy_of_steam_into_panel + mcal_5B_SC.Design_fuel_consumption * mcal_5B_SC.Total_heat_absorbed_by_panel / ((mcal_5B_SC.Main_steam_flow_rate - mcal_5B_SC.Secondary_desuperheating_spray_flow_rate_kg_h) / 3600);
                        ////F_103 =F96+D40*F89/(D49/3600)
                        mcal_5B_SC.Enthalpy_of_steam_out_of_the_panel = mcal_5B_SC.Enthalpy_of_steam_into_panel + mcal_5B_SC.Design_fuel_consumption * mcal_5B_SC.Total_heat_absorbed_by_panel / (mcal_5B_SC.Main_steam_flow_rate / 3600);

                        //F_104 =T_ph(10*F94,F103)
                        mcal_5B_SC.Temperature_of_steam_out_of_panel = stream_Macros_BLL.T_ph(10 * mcal_5B_SC.Pressure_of_steam_out_of_panel, mcal_5B_SC.Enthalpy_of_steam_out_of_the_panel);

                        //F_105 =(F97+F104)/2
                        mcal_5B_SC.Average_temperature_of_steam_in__panel = (mcal_5B_SC.Temperature_of_steam_into_panel_after_spray_attemperator + mcal_5B_SC.Temperature_of_steam_out_of_panel) / 2;

                        //F_106 =v_pT(10*(F93+F94)/2,F105)
                        mcal_5B_SC.Average_specific_volume_of_steam_in_the__panel = stream_Macros_BLL.v_pT(10 * (mcal_5B_SC.Pressure_of_steam_into_panel + mcal_5B_SC.Pressure_of_steam_out_of_panel) / 2, mcal_5B_SC.Average_temperature_of_steam_in__panel);

                        //F_107  =D49*F106/(3600*D13)
                        mcal_5B_SC.Average_speed_of_steam_in_the_panel = mcal_5B_SC.Main_steam_flow_rate * mcal_5B_SC.Average_specific_volume_of_steam_in_the__panel / (3600 * mcal_5B_SC.Steam_flow_area);

                        ////F_108
                        //f_108
                        //f_108 - Calculate inner diameter (in mm) for correction coefficient
                        Double Con_Val_mm = mcal_5B_SC.PSH_tube_diameter - 2 * mcal_5B_SC.PSH_tube_thickness;  // Inner diameter in mm
                        Double Con_Val_m = Con_Val_mm / 1000.0;  // Convert to meters for heat transfer calc

                        // Calculate correction coefficient using EXACT Excel coefficients (mm-based)
                        Double v1 = 4.166666667e-11 * Math.Pow(Con_Val_mm, 6);
                        Double v2 = 1.173076923177e-08 * Math.Pow(Con_Val_mm, 5);
                        Double v3 = 1.33733974377156e-06 * Math.Pow(Con_Val_mm, 4);
                        Double v4 = 0.0000800917832293105 * Math.Pow(Con_Val_mm, 3);
                        Double v5 = 0.00277223047821308 * Math.Pow(Con_Val_mm, 2);
                        Double v6 = 0.0603097028027454 * Con_Val_mm;
                        Double v7 = 1.65375000006153;

                        mcal_5B_SC.Correction_coefficient_of_tube_diameter = v1 - v2 + v3 - v4 + v5 - v6 + v7;

                        //f_109
                        mcal_5B_SC.Thermal_conductivity_of_steam = stream_Macros_BLL.tc_pT(10 * (mcal_5B_SC.Pressure_of_steam_into_panel + mcal_5B_SC.Pressure_of_steam_out_of_panel) / 2, mcal_5B_SC.Average_temperature_of_steam_in__panel);

                        //f_110
                        mcal_5B_SC.Kinematic_viscosity_of_steam = stream_Macros_BLL.my_pT(10 * (mcal_5B_SC.Pressure_of_steam_into_panel + mcal_5B_SC.Pressure_of_steam_out_of_panel) / 2, mcal_5B_SC.Average_temperature_of_steam_in__panel) * mcal_5B_SC.Average_specific_volume_of_steam_in_the__panel;

                        //f_111
                        mcal_5B_SC.Prandtl_number_of_steam_Pr = stream_Macros_BLL.Pr_pT(10 * (mcal_5B_SC.Pressure_of_steam_into_panel + mcal_5B_SC.Pressure_of_steam_out_of_panel) / 2, mcal_5B_SC.Average_temperature_of_steam_in__panel);

                        //f_112 - Heat transfer coefficient from tube wall to steam (Dittus-Boelert equation)
                        // Formula: =0.023*F109/(D8/1000-2*D9/1000)*(F107*(D8/1000-2*D9/1000)/F110)^0.8*F111^0.4*F108
                        Double Re = (mcal_5B_SC.Average_speed_of_steam_in_the_panel * Con_Val_m) / mcal_5B_SC.Kinematic_viscosity_of_steam;
                        Double Pr = mcal_5B_SC.Prandtl_number_of_steam_Pr;
                        Double Nu = 0.023 * Math.Pow(Re, 0.8) * Math.Pow(Pr, 0.4);

                        mcal_5B_SC.Heat_transfer_coefficient_from_tube_wall_to_steam =
                            (Nu * mcal_5B_SC.Thermal_conductivity_of_steam / Con_Val_m) * mcal_5B_SC.Correction_coefficient_of_tube_diameter;

                        System.Diagnostics.Debug.WriteLine($"[5B] Con_Val_mm={Con_Val_mm}, Con_Val_m={Con_Val_m}");
                        System.Diagnostics.Debug.WriteLine($"[5B] Correction_coefficient={mcal_5B_SC.Correction_coefficient_of_tube_diameter}");
                        System.Diagnostics.Debug.WriteLine($"[5B] Re={Re}, Pr={Pr}, Nu={Nu}");
                        System.Diagnostics.Debug.WriteLine($"[5B] Final Heat_transfer_coefficient={mcal_5B_SC.Heat_transfer_coefficient_from_tube_wall_to_steam}");

                        //Double Con_Val = (mcal_5B_SC.PSH_tube_diameter - 2 * mcal_5B_SC.PSH_tube_thickness);
                        //Double v1 = 4.167 * Math.Pow(10, -11) * Math.Pow(Con_Val, 6);
                        //Double v2 = 1.173 * Math.Pow(10, -8) * Math.Pow(Con_Val, 5);
                        //Double v3 = 1.337 * Math.Pow(10, -6) * Math.Pow(Con_Val, 4);
                        //Double v4 = 8.01 * Math.Pow(10, -5) * Math.Pow(Con_Val, 3);
                        //Double v5 = 2.77 * Math.Pow(10, -3) * Math.Pow(Con_Val, 2);
                        //Double v6 = 0.06 * Con_Val;
                        //Double v7 = 1.654;

                        //mcal_5B_SC.Correction_coefficient_of_tube_diameter = v1 - v2 + v3 - v4 + v5 - v6 + v7;
                        //// Double Correction_coefficient_of_tube_diameter =Math.Pow(4.16*10,-11) * Math.Pow(, 6) -Math.Pow(1.17*10,-08) * Math.Pow((PSH_tube_diameter - 2 * PSH_tube_thickness), 5) +Math.Pow(1.33*10,-06) * Math.Pow((PSH_tube_diameter - 2 * PSH_tube_thickness), 4) - 0.0000800917832293105 * Math.Pow((PSH_tube_diameter - 2 * PSH_tube_thickness), 3) + 0.00277223047821308 * Math.Pow((PSH_tube_diameter - 2 * PSH_tube_thickness), 2) - 0.0603097028027454 * (PSH_tube_diameter - 2 * PSH_tube_thickness) + 1.65375000006153;

                        ////f_109
                        //mcal_5B_SC.Thermal_conductivity_of_steam = stream_Macros_BLL.tc_pT(10 * (mcal_5B_SC.Pressure_of_steam_into_panel + mcal_5B_SC.Pressure_of_steam_out_of_panel) / 2, mcal_5B_SC.Average_temperature_of_steam_in__panel);

                        ////f_110
                        //mcal_5B_SC.Kinematic_viscosity_of_steam = stream_Macros_BLL.my_pT(10 * (mcal_5B_SC.Pressure_of_steam_into_panel + mcal_5B_SC.Pressure_of_steam_out_of_panel) / 2, mcal_5B_SC.Average_temperature_of_steam_in__panel) * mcal_5B_SC.Average_specific_volume_of_steam_in_the__panel;

                        ////f_111
                        //mcal_5B_SC.Prandtl_number_of_steam_Pr = stream_Macros_BLL.Pr_pT(10 * (mcal_5B_SC.Pressure_of_steam_into_panel + mcal_5B_SC.Pressure_of_steam_out_of_panel) / 2, mcal_5B_SC.Average_temperature_of_steam_in__panel);

                        ////f_112----->4524 val 
                        //Double Const_Val = ((mcal_5B_SC.PSH_tube_diameter / 1000) - 2 * (mcal_5B_SC.PSH_tube_thickness / 1000));
                        //Double f1 = 0.023 * mcal_5B_SC.Thermal_conductivity_of_steam / Const_Val;
                        //Double f2 = Math.Pow(((mcal_5B_SC.Average_speed_of_steam_in_the_panel * Const_Val) / mcal_5B_SC.Kinematic_viscosity_of_steam), 0.8);
                        //Double f3 = Math.Pow(mcal_5B_SC.Prandtl_number_of_steam_Pr, 0.4);
                        //Double f4 = mcal_5B_SC.Correction_coefficient_of_tube_diameter;
                        ////Double f5 = ;
                        //mcal_5B_SC.Heat_transfer_coefficient_from_tube_wall_to_steam = f1 * f2 * f3 * f4;

                        ////f_112 - Heat transfer coefficient from tube wall to steam (Dittus-Boelert equation)

                        //f_113
                        mcal_5B_SC.Average_flue_gas_velocity_among_panel = mcal_5B_SC.Flue_gas_total_volume * mcal_5B_SC.Design_fuel_consumption * (mcal_5B_SC.Average_flue_gas_temperature_C + 273) / (273 * mcal_5B_SC.Gas_average_flow_area);

                        //f_114
                        mcal_5B_SC.Thermal_conductivity_of_flue_gas = 0.0000882529644268775 * mcal_5B_SC.Average_flue_gas_temperature_C + 0.0215130434782609;

                        //f_115
                        Double Val_A = 4.794 * Math.Pow(mcal_5B_SC.Average_flue_gas_temperature_C, 2) * Math.Pow(10, -11);
                        Double Val_B = 1.099 * Math.Pow(10, -7) * mcal_5B_SC.Average_flue_gas_temperature_C;
                        Double Val_C = 8.185 * Math.Pow(10, -6);
                        mcal_5B_SC.Kinematic_viscosity_of_flue_gas = Val_A + Val_B + Val_C;

                        //Double Kinematic_viscosity_of_flue_gas = 4.794381705 * Math.Pow(Average_flue_gas_temperature_C, 2) + 1.0990798983625 * Average_flue_gas_temperature_C + 0.0000081852173913032;

                        //f_116
                        mcal_5B_SC.Average_Prandtl_number_of_flue_gas = (0.68 - 0.0001 * mcal_5B_SC.Average_flue_gas_temperature_C);

                        //f_117
                        mcal_5B_SC.Prandtl_number_of_flue_gas = (0.94 + 0.56 * mcal_5B_SC.Volume_fraction_of_water_vapor) * mcal_5B_SC.Average_Prandtl_number_of_flue_gas;

                        //F_118
                        mcal_5B_SC.Correction_factor_of_tube_rows = mcal_5B_SC.Correction_factor_for_tube_rows;

                        //F_119
                        mcal_5B_SC.Flue_gas_compositon__and_temperature__correction_coefficient = 0.92 + 0.726 * mcal_5B_SC.Volume_fraction_of_water_vapor;

                        //f_120
                        //Double Correction_factor_for_geometric_arrangement;
                        Double Val_D16;

                        if (mcal_5B_SC.Relative_transverse_pitch <= 1.5)
                        {
                            mcal_5B_SC.Correction_factor_for_geometric_arrangement = 1;
                        }
                        else
                        {
                            if (mcal_5B_SC.Relative_vertical_pitch > 2)
                            {
                                mcal_5B_SC.Correction_factor_for_geometric_arrangement = 1;
                            }
                            else
                            {
                                if (mcal_5B_SC.Relative_transverse_pitch > 3)
                                {
                                    Val_D16 = 3;
                                    mcal_5B_SC.Correction_factor_for_geometric_arrangement = Math.Pow(1 + ((2 * Val_D16) - 3) * Math.Pow((1 - (mcal_5B_SC.Relative_vertical_pitch / 2)), 3), -2);
                                }
                                else
                                {
                                    Val_D16 = mcal_5B_SC.Relative_transverse_pitch;
                                    mcal_5B_SC.Correction_factor_for_geometric_arrangement = Math.Pow(1 + ((2 * Val_D16) - 3) * Math.Pow((1 - (mcal_5B_SC.Relative_vertical_pitch / 2)), 3), -2);
                                }
                            }
                        }

                        //f_121
                        mcal_5B_SC.Flue_gas_side_convection_coefficient = 0.2 * mcal_5B_SC.Thermal_conductivity_of_flue_gas / (mcal_5B_SC.PSH_tube_diameter / 1000) * Math.Pow((mcal_5B_SC.Average_flue_gas_velocity_among_panel * (mcal_5B_SC.PSH_tube_diameter / 1000) / mcal_5B_SC.Kinematic_viscosity_of_flue_gas), 0.65) * Math.Pow(mcal_5B_SC.Prandtl_number_of_flue_gas, 0.33) * mcal_5B_SC.Correction_factor_of_tube_rows * mcal_5B_SC.Correction_factor_for_geometric_arrangement * mcal_5B_SC.Flue_gas_compositon__and_temperature__correction_coefficient;
                        //Ash Resistence Coefficient claculation 
                        //J_113
                        //mcal_5B_SC.reqd_J113 = mcal_5B_SC.PSH_effectiveness_coefficent;

                        //f_125
                        if (mcal_5B_SC.Average_flue_gas_velocity_among_panel <= 4)
                        {
                            mcal_5B_SC.Utilization_coefficient_of_panel = 0.0024 * Math.Pow(mcal_5B_SC.Average_flue_gas_velocity_among_panel, 5) - 0.0314 * Math.Pow(mcal_5B_SC.Average_flue_gas_velocity_among_panel, 4) + 0.1511 * Math.Pow(mcal_5B_SC.Average_flue_gas_velocity_among_panel, 3) - 0.3412 * Math.Pow(mcal_5B_SC.Average_flue_gas_velocity_among_panel, 2) + 0.5189 * mcal_5B_SC.Average_flue_gas_velocity_among_panel + 0.2108;

                        }
                        else
                        {
                            mcal_5B_SC.Utilization_coefficient_of_panel = 0.85;
                        }

                        //J_114
                        //Double h1 = Flue_gas_side_heat_transfer_coefficient;
                        //J_115
                        // Double h2 = Heat_transfer_coefficient_from_tube_wall_to_steam;
                        //J_116
                        //Double one_h1_one_h2 = (1 / Flue_gas_side_heat_transfer_coefficient) + (1 / Heat_transfer_coefficient_from_tube_wall_to_steam);
                        //J_117
                        //Double E = (1 / reqd_J113) * one_h1_one_h2;

                        //F_122
                        mcal_5B_SC.Ash_deposition_coefficient = E;

                        //F_123
                        mcal_5B_SC.Fouling_layer_temperature_of_tube_wall = mcal_5B_SC.Average_temperature_of_steam_in__panel + 1000 * (mcal_5B_SC.Ash_deposition_coefficient + 1 / mcal_5B_SC.Heat_transfer_coefficient_from_tube_wall_to_steam) * mcal_5B_SC.Design_fuel_consumption * (mcal_5B_SC.Absorbed_convection_heat_of_panel + mcal_5B_SC.Radiation_heat_absorbed_by_panel) / mcal_5B_SC.Platen_superheator_total_heating_area;

                        //F_124----> 63.44
                        Double Con = ((mcal_5B_SC.Fouling_layer_temperature_of_tube_wall + 273) / (mcal_5B_SC.Average_flue_gas_temperature_K));
                        Double val1 = 5.7 * Math.Pow(10, -8) * (mcal_5B_SC.Tube_wall_fouling_emmisivity + 1) / 2 * mcal_5B_SC.Flue_gas_emissivity * Math.Pow(mcal_5B_SC.Average_flue_gas_temperature_K, 3);
                        Double val2 = 1 - Math.Pow(Con, 4);
                        Double val3 = 1 - Con;

                        mcal_5B_SC.Radiation_heat_transfer_coefficient = val1 * val2 / val3;

                        //f_126---->94.30
                        mcal_5B_SC.Flue_gas_side_heat_transfer_coefficient_ = mcal_5B_SC.Utilization_coefficient_of_panel * (((3.14 * mcal_5B_SC.PSH_tube_diameter) / (2 * mcal_5B_SC.Configuration_factor_for_PSH * mcal_5B_SC.Platen_superheater_average_longitudinal_spacing) * (mcal_5B_SC.Flue_gas_side_convection_coefficient)) + mcal_5B_SC.Radiation_heat_transfer_coefficient);

                        //F_127
                        mcal_5B_SC.Overall_heat_transfer_coefficient = mcal_5B_SC.Flue_gas_side_heat_transfer_coefficient_ / (1 + (1 + mcal_5B_SC.Radiation_heat_absorbed_by_panel / mcal_5B_SC.Absorbed_convection_heat_of_panel) * (mcal_5B_SC.Ash_deposition_coefficient + 1 / mcal_5B_SC.Heat_transfer_coefficient_from_tube_wall_to_steam) * mcal_5B_SC.Flue_gas_side_heat_transfer_coefficient_);

                        if (dt.Tables[39].Rows[0][0].ToString() == "1")
                        //F_128
                        {
                            // F128=F66-F97 parallel Flow
                            mcal_5B_SC.Large_temperature_difference = mcal_5B_SC.Temperature_of_flue_gas_into_panel_input - mcal_5B_SC.Temperature_of_steam_into_panel_after_spray_attemperator;
                            //F129=F73-F104
                            mcal_5B_SC.Small_temperature_difference = mcal_5B_SC.Temperature_of_flue_gas_out_of_panel - mcal_5B_SC.Temperature_of_steam_out_of_panel;

                        }
                        else
                        {
                            //F128 =F66-F92 Counter Flow
                            mcal_5B_SC.Large_temperature_difference = mcal_5B_SC.Temperature_of_flue_gas_into_panel_input - mcal_5B_SC.Error_in_radiation_heat_transfer_from_flue_gas__to_downstream_elements;
                            //F129=F73-F97
                            mcal_5B_SC.Small_temperature_difference = mcal_5B_SC.Temperature_of_flue_gas_out_of_panel - mcal_5B_SC.Temperature_of_steam_into_panel_after_spray_attemperator;
                        }
                        //F_129


                        //F_130 =(F128-F129)/(LN(F128/F129))
                        mcal_5B_SC.Logarithmic_mean_temperature_difference = (mcal_5B_SC.Large_temperature_difference - mcal_5B_SC.Small_temperature_difference) / Math.Log(mcal_5B_SC.Large_temperature_difference / mcal_5B_SC.Small_temperature_difference);

                        //F_131--->=F127*F130*D20/(1000*D40)

                        mcal_5B_SC.Convection_heat_of_panel = mcal_5B_SC.Overall_heat_transfer_coefficient * mcal_5B_SC.Logarithmic_mean_temperature_difference * mcal_5B_SC.Platen_superheator_total_heating_area / (1000 * mcal_5B_SC.Design_fuel_consumption);

                        //F_132 =100*(F76-F131)/F76
                        mcal_5B_SC.Error_2 = 100 * (mcal_5B_SC.Absorbed_convection_heat_of_panel - mcal_5B_SC.Convection_heat_of_panel) / mcal_5B_SC.Absorbed_convection_heat_of_panel;



                        //F_133 =Tsat_p(D47*0.980665+1.01325)

                        mcal_5B_SC.Water_temperature_ = stream_Macros_BLL.Tsat_p(mcal_5B_SC.Drum_pressure * 0.980665 + 1.01325);
                        //f_134
                        mcal_5B_SC.Average_temperature_difference_of_heat_transfer_1 = mcal_5B_SC.Average_flue_gas_temperature_C - mcal_5B_SC.Water_temperature_;

                        //F_135
                        mcal_5B_SC.Absobed_convection__heat_by_both_side_water_walls_within_panel_zone = mcal_5B_SC.Overall_heat_transfer_coefficient * mcal_5B_SC.Average_temperature_difference_of_heat_transfer_1 * mcal_5B_SC.Side_Water_wall_heating_area_within_panel / (1000 * mcal_5B_SC.Design_fuel_consumption);

                        //F_136=F71-F135
                        mcal_5B_SC.Absorbed_convection_heat_of_water_wall_at_panel_zone = mcal_5B_SC.Absobed_convection__heat_by_both_side_water_walls_within_panel_zone;
                        mcal_5B_SC.Error_3 = mcal_5B_SC.Absorbed_convection_heat_of_water_wall_at_panel_zone - mcal_5B_SC.Absobed_convection__heat_by_both_side_water_walls_within_panel_zone;
                        //mcal_5B_SC.Absorbed_convection_heat_of_water_wall_at_panel_zone = mcal_5B_SC.Absobed_convection__heat_by_both_side_water_walls_within_panel_zone;


                    } //while ((Math.Abs(mcal_5B_SC.Error_3) >= 0.01));
                    while ((Math.Abs(mcal_5B_SC.Error_3) >= 0.01) || mcal_5B_SC.Error_3<0.0);

                    //F_137 from 4B Modules
                    mcal_5B_SC.Furnace_radiation_heat_flux_absorbed_by_furnace_roof_cover = 68.93;
                    //F_138 =F83*F137*D22/D40
                    mcal_5B_SC.Furnace_radiation_heat_absorbed_by_furnace_roof_cover = mcal_5B_SC.Coefficient_considering_reradiation * mcal_5B_SC.Furnace_radiation_heat_flux_absorbed_by_furnace_roof_cover * mcal_5B_SC.Heating_area_of_roof_tubes_in_Upper_furnace / mcal_5B_SC.Design_fuel_consumption;
                    //F_139 =3600*D40*F138/(D51)
                    mcal_5B_SC.Enthalpy_increment_of_furnace_roof_cover = 3600 * mcal_5B_SC.Design_fuel_consumption * mcal_5B_SC.Furnace_radiation_heat_absorbed_by_furnace_roof_cover / (mcal_5B_SC.De_superheating_spray_stage_2);
                    //F_140.
                    mcal_5B_SC.Saturated_steam_temperature_of_drum_outlet = stream_Macros_BLL.Tsat_p(mcal_5B_SC.Drum_pressure * 0.980665 + 1.01325);
                    //F_141
                    mcal_5B_SC.Dry_saturated_steam_enthalpy_of_drum_outlet = stream_Macros_BLL.h_pT((mcal_5B_SC.Drum_pressure * 0.980665 + 1.01325), (mcal_5B_SC.Saturated_steam_temperature_of_drum_outlet + 0.001));
                    //F_142
                    mcal_5B_SC.Furnace_roof_pressure_within_panel_zone_ = ((mcal_5B_SC.Furnace_roof_pressure_within_panel_zone_input) * 0.980665 + 1.01325) / 10;
                    //F_143
                    mcal_5B_SC.Steam_enthalpy_of_furnace_roof_inlet_within_panel_zone = mcal_5B_SC.Dry_saturated_steam_enthalpy_of_drum_outlet + mcal_5B_SC.Enthalpy_increment_of_furnace_roof_cover;
                    //F_144
                    mcal_5B_SC.Steam_temperature_of_furnace_roof_inlet_within_panel_zone = stream_Macros_BLL.T_ph(10 * mcal_5B_SC.Furnace_roof_pressure_within_panel_zone_, mcal_5B_SC.Steam_enthalpy_of_furnace_roof_inlet_within_panel_zone);
                    //F_145 =3600*D40*F70/(D51)
                    mcal_5B_SC.Steam_enthalpy_increment_of_furnace_roof_within_panel_zone = (3600 * mcal_5B_SC.Design_fuel_consumption * mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_at_panel_zone) / mcal_5B_SC.De_superheating_spray_stage_2;
                    //F_146
                    mcal_5B_SC.Steam_enthalpy_of_furnace_roof_outlet_within_panel_zone = mcal_5B_SC.Steam_enthalpy_of_furnace_roof_inlet_within_panel_zone + mcal_5B_SC.Steam_enthalpy_increment_of_furnace_roof_within_panel_zone;
                    //F_147
                    mcal_5B_SC.Steam_temperature_of_furnace_roof_within_panel_zone_ = stream_Macros_BLL.T_ph(10 * mcal_5B_SC.Furnace_roof_pressure_within_panel_zone_, mcal_5B_SC.Steam_enthalpy_of_furnace_roof_outlet_within_panel_zone);
                    //F_148
                    mcal_5B_SC.Average_steam_temperature_of_furnace_roof_within_panel_zone = 0.5 * (mcal_5B_SC.Steam_temperature_of_furnace_roof_inlet_within_panel_zone + mcal_5B_SC.Steam_temperature_of_furnace_roof_within_panel_zone_);
                    //F_149
                    mcal_5B_SC.Average_temperature_difference_of_heat_transfer_1 = mcal_5B_SC.Average_flue_gas_temperature_C - mcal_5B_SC.Average_steam_temperature_of_furnace_roof_within_panel_zone;
                    //F_150
                    mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_within_panel_zone = mcal_5B_SC.Overall_heat_transfer_coefficient * mcal_5B_SC.Average_temperature_difference_of_heat_transfer_1 * mcal_5B_SC.Heating_area_of_roof_tubes_in_PH_zone / 1000 / mcal_5B_SC.Design_fuel_consumption;
                    mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_at_panel_zone = mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_within_panel_zone;
                    //F_151 =F70-F150
                    mcal_5B_SC.Error_4 = mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_at_panel_zone - mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_within_panel_zone;

                    double abs_val = Math.Abs(mcal_5B_SC.Error_4);
                    //mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_at_panel_zone = mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_within_panel_zone;



                } while ((Math.Abs(mcal_5B_SC.Error_4) >= 0.01) || mcal_5B_SC.Error_4<0.0);


                //F_152 =F131+F135+F150

                mcal_5B_SC.Absorbed_convection_heat_within_panel_zone = mcal_5B_SC.Convection_heat_of_panel + mcal_5B_SC.Absobed_convection__heat_by_both_side_water_walls_within_panel_zone + mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_within_panel_zone;
                //F_153 =F68-F152
                mcal_5B_SC.Total_error = mcal_5B_SC.Convection_heat_of_panel_zone_ - mcal_5B_SC.Absorbed_convection_heat_within_panel_zone;
                System.Diagnostics.Debug.WriteLine(
                    $"Temp_out = {mcal_5B_SC.Temperature_of_flue_gas_out_of_panel} | " +
                    $"F68 = {mcal_5B_SC.Convection_heat_of_panel_zone_} | " +
                    $"F69 = {mcal_5B_SC.Radiation_heat_from_flue_gas_to_heating_surface_through_panel} | " +
                    $"F70 = {mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_at_panel_zone} | " +
                    $"F71 = {mcal_5B_SC.Absorbed_convection_heat_of_water_wall_at_panel_zone} | " +
                    $"Error = {mcal_5B_SC.Total_error}");



                Double d = Math.Abs(mcal_5B_SC.Total_error);

                if (Math.Abs(mcal_5B_SC.Total_error) > 0)
                {
                    mcal_5B_SC.Absorbed_convection_heat_within_panel_zone = mcal_5B_SC.Absorbed_convection_heat_within_panel_zone + 0.1;
                }
                else
                {
                    mcal_5B_SC.Absorbed_convection_heat_within_panel_zone = mcal_5B_SC.Absorbed_convection_heat_within_panel_zone - 0.1;
                }
                mcal_5B_SC.Convection_heat_of_panel_zone_ = mcal_5B_SC.Absorbed_convection_heat_within_panel_zone;
                //double relax = 0.3;

                //mcal_5B_SC.Convection_heat_of_panel_zone_ =
                //    mcal_5B_SC.Convection_heat_of_panel_zone_
                //    - relax * mcal_5B_SC.Total_error;

            } while (Math.Abs(mcal_5B_SC.Total_error) >= 0.1);
            // while (Math.Abs(mcal_5B_SC.Total_error) >= 0.1 || mcal_5B_SC.Total_error<0.0);


            //} while (mcal_5B_SC.Total_error >= 0.001);
            //=F89*D40/1000
            Double Heat_absorption_heating_section = mcal_5B_SC.Total_heat_absorbed_by_panel * mcal_5B_SC.Design_fuel_consumption / 1000;
            //=(F138+F70)*D40/1000
            Double Roof = (mcal_5B_SC.Furnace_radiation_heat_absorbed_by_furnace_roof_cover + mcal_5B_SC.Absorbed_convection_heat_of_furnace_roof_at_panel_zone) * mcal_5B_SC.Design_fuel_consumption / 1000;
            //=F71*D40/1000
            Double Water_wall = mcal_5B_SC.Absorbed_convection_heat_of_water_wall_at_panel_zone * mcal_5B_SC.Design_fuel_consumption / 1000;


            mcal_5B_BLL.InsertUpadate_5B_S_ParameterValue(Project_ID, Boiler_ID, Boiler_Load, Objective_ID, Section_ID, Math.Round(mcal_5B_SC.LTSH_outlet_temperature, 2), Math.Round(mcal_5B_SC.Temperature_of_steam_out_of_panel, 2), iteration, Next_SectionID);

            DataTable dt1 = new DataTable();
            dt1 = mcal_5B_BLL.Get_PID_For_5B_Calculation();

            foreach (DataRow row in dt1.Rows)
            {
                int pid = Convert.ToInt16(row["PID"].ToString());



                Cal_5B_BLL mCal_5B_BLL = new Cal_5B_BLL();

                switch (pid)
                {
                        


                    case 1: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.LTSH_outlet_temperature,2)); break;
                    case 2: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Temperature_of_steam_out_of_panel,2)); break;
                    case 3: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Temperature_of_flue_gas_into_panel_input,2)); break;
                    case 4: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Temperature_of_flue_gas_out_of_panel,2)); break;
                    case 5: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Temperature_of_steam_into_panel_after_spray_attemperator,2)); break;
                    case 6: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Steam_temperature_of_furnace_roof_inlet_within_panel_zone,2)); break;
                    case 7: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Steam_temperature_of_furnace_roof_within_panel_zone_,2)); break;
                    case 8: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Water_temperature_,2)); break;
                    case 9: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(Heat_absorption_heating_section,2)); break;
                    case 10: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(Roof,2)); break;
                    case 11: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(Water_wall,2)); break;
                    case 12: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Enthalpy_of_flue_gas_out_of_panel,2)); break;
                    case 13: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Radiation_heat_from_flue_gas_to_heating_surface_behind_panel,2)); break;
                    case 14: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Inlet_direct_radiation_from_the_furnace,2)); break;
                    case 15: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Saturated_steam_temperature_of_drum_outlet,2)); break;
                    case 16: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Steam_enthalpy_of_furnace_roof_outlet_within_panel_zone,2)); break;
                    case 17: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Radiation_heat_absorbed_by_panel,2)); break;
                    case 18: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Enthalpy_increment_of_furnace_roof_cover,2)); break;
                    case 19: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Furnace_radiation_heat_absorbed_by_furnace_roof_cover,2)); break;
                    case 20: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Average_flue_gas_velocity_among_panel,2)); break;
                    case 21: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Absorbed_convection_heat_of_panel,2)); break;
                    case 22: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Enthalpy_of_steam_into_panel,2)); break;
                    case 23: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Pressure_of_steam_into_panel,2)); break;
                    case 24: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Pressure_of_steam_out_of_panel,2)); break;
                    case 25: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID, Math.Round(mcal_5B_SC.Heat_transfer_coefficient_from_tube_wall_to_steam,2)); break;
                    case 26: mCal_5B_BLL.Insert_5B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, Math.Round(mcal_5B_SC.Furnace_radiation_heat_leaked_out_of_the_panel,2)); break;


        
                } //Switch Close
            }   //For close

        }

    }
}