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
namespace GenericClasses
{
    public class Class_6B_Calculation
    {
        //string str = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        string Boiler_ID = null;
        string Project_ID = null;
        //string Section_ID = null;
        string Boiler_Load = null;
        int Objective_ID = 0;
        string Section_ID = null;
        string Previous_SectionID = null;
        int iteration = 0;

        public Class_6B_Calculation(string BoilerID, string Project_ID, string BoilerLoad ,int ObjectiveID,string SectionID,string PreviousSectionID,int Iteration)
        { 
            this.Boiler_ID = BoilerID;
            this.Project_ID = Project_ID;          
            this.Boiler_Load = BoilerLoad;
            this.Objective_ID = ObjectiveID;
            this.Section_ID = SectionID;
            this.Previous_SectionID = PreviousSectionID;
            this.iteration = Iteration;
        }


        public void Class_6B_Calculation_Result()
        {
            //string Project_ID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
            //string Boiler_ID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
            
            //int Objective_ID = 1;
            string Prev_Section_ID = null;
            string Last_5B_Sec_ID = null;
            //string Boiler_Load = "100%TMCR";
            //int iteration = 0;

            Cal_6B_SC mCal_6B_SC = null;
            Cal_6B_BLL mCal_6B_BLL = null;
            Stream_Macros mStream_Macros_BLL = null;
            DataSet mdataset = null;
            mCal_6B_SC = new Cal_6B_SC();
            mCal_6B_BLL = new Cal_6B_BLL();
            mStream_Macros_BLL = new Stream_Macros();
            mdataset = new DataSet();

            Prev_Section_ID = mCal_6B_BLL.Get_PreVious_HE_SectionID(Boiler_ID, Project_ID, Section_ID);
            Last_5B_Sec_ID = mCal_6B_BLL.GetLast_Element_of_HeatingSectionUpperFurnace(Boiler_ID, Project_ID);
           // Last_5B_Sec_ID = "6F83D306-5D7A-4A9D-9D5F-218D4B2B1AD2";

            mdataset = mCal_6B_BLL.Get_Input_For_6B_Calculation(Boiler_ID, Project_ID, Boiler_Load, Section_ID, Last_5B_Sec_ID, Objective_ID);
            //D_8
            mCal_6B_SC.Front_RH_tube_diameter_input = Convert.ToDouble(mdataset.Tables[0].Rows[0]["Value"].ToString());
            //D_9
            mCal_6B_SC.Front_RH_tube_thickness_input = Convert.ToDouble(mdataset.Tables[1].Rows[0]["Value"].ToString());
            //D_10
            mCal_6B_SC.Steam_flow_area_input = Convert.ToDouble(mdataset.Tables[2].Rows[0]["Value"].ToString());
            //D_11
            mCal_6B_SC.Gas_average_flow_area_input = Convert.ToDouble(mdataset.Tables[3].Rows[0]["Value"].ToString());
            //D_12
            mCal_6B_SC.Correction_factor_for_tube_rows_input = Convert.ToDouble(mdataset.Tables[4].Rows[0]["Value"].ToString());
            //D_13
            mCal_6B_SC.Relative_transverse_pitch_input = Convert.ToDouble(mdataset.Tables[5].Rows[0]["Value"].ToString());
            //D_14
            mCal_6B_SC.Relative_vertical_pitch_input = Convert.ToDouble(mdataset.Tables[6].Rows[0]["Value"].ToString());
            //D_15
            mCal_6B_SC.Front_RH_total_heating_area_input = Convert.ToDouble(mdataset.Tables[7].Rows[0]["Value"].ToString());
            //D_16
            mCal_6B_SC.Side_Water_wall_heating_area__within_panel_input = Convert.ToDouble(mdataset.Tables[8].Rows[0]["Value"].ToString());
            //D_17
            mCal_6B_SC.Heating_area_of_roof_tubes_in_PH_zone_input = Convert.ToDouble(mdataset.Tables[9].Rows[0]["Value"].ToString());
            //D_18
            mCal_6B_SC.Effective_radiation_layer_thickness_input = Convert.ToDouble(mdataset.Tables[10].Rows[0]["Value"].ToString());
            //D_22
            mCal_6B_SC.Temperature_of_flue_gas_into_panel_input = Convert.ToDouble(mdataset.Tables[11].Rows[0]["Value"].ToString());
            //D_23
            mCal_6B_SC.Enthalpy_of_flue_gas_into_panel__input = Convert.ToDouble(mdataset.Tables[12].Rows[0]["Value"].ToString());
            //D_24
            mCal_6B_SC.Heat_preservation_coefficient_input = Convert.ToDouble(mdataset.Tables[13].Rows[0]["Value"].ToString());
            //D_25
            mCal_6B_SC.Partial_pressure_of_triatomic_gases_input = Convert.ToDouble(mdataset.Tables[14].Rows[0]["Value"].ToString());
            //D_26
            mCal_6B_SC.Volume_fraction_of_water_vapor_input = Convert.ToDouble(mdataset.Tables[15].Rows[0]["Lower furnace"].ToString());
            //D_27
            mCal_6B_SC.Gas_density_input = Convert.ToDouble(mdataset.Tables[16].Rows[0]["Lower furnace"].ToString());
            //D_28
            mCal_6B_SC.Flue_gas_total_volume_input = Convert.ToDouble(mdataset.Tables[17].Rows[0]["Lower furnace"].ToString());
            //D_29
            mCal_6B_SC.Mean_diameter_of_ash_particle_input = Convert.ToDouble(mdataset.Tables[18].Rows[0]["Value"].ToString());
            //D_30
            mCal_6B_SC.Volume_fraction_of_triatomic_gases_input = Convert.ToDouble(mdataset.Tables[19].Rows[0]["Lower furnace"].ToString());
            //D_31
            mCal_6B_SC.Dimensionless_concentration_of_fly_ash_input = Convert.ToDouble(mdataset.Tables[20].Rows[0]["Lower furnace"].ToString());
            //D_32
            mCal_6B_SC.Furnace_pressure_input = Convert.ToDouble(mdataset.Tables[21].Rows[0]["Value"].ToString());
            //D_33
            mCal_6B_SC.Radiation_heat_from_flue_gas_in_PSH_zone_input = Convert.ToDouble(mdataset.Tables[22].Rows[0]["Value"].ToString());
            //D_34
            mCal_6B_SC.Radiation_heat_from_leakage_in_PSH_zone_input = Convert.ToDouble(mdataset.Tables[23].Rows[0]["Value"].ToString());
            //D_35
            mCal_6B_SC.Design_fuel_consumption_input = Convert.ToDouble(mdataset.Tables[24].Rows[0]["Value"].ToString()) / 3.6;

            DataSet S_Para_Dataset = new DataSet();
            
            //S parameter SectionID
            S_Para_Dataset = mCal_6B_BLL.Get_S_Parameter_Section_ID(Boiler_ID, Project_ID, Boiler_Load, Section_ID,Objective_ID,iteration);

            //D_39
            mCal_6B_SC.Front_RH_inlet_temperature_input = Convert.ToDouble(S_Para_Dataset.Tables[0].Rows[0]["Inlet_temperature"].ToString());
            //D_40
            mCal_6B_SC.Front_RH_inlet_pressure_input = Convert.ToDouble(S_Para_Dataset.Tables[1].Rows[0]["Inlet_Pressure"].ToString());
            //D_41
            mCal_6B_SC.Front_RH_outlet_pressure_input = Convert.ToDouble(S_Para_Dataset.Tables[2].Rows[0]["Outlet_Pressure"].ToString());
            //D_42
            mCal_6B_SC.Working_medium_temperature_of_water_wall_input = Convert.ToDouble(mdataset.Tables[25].Rows[0]["Value"].ToString());
            //D_43
            mCal_6B_SC.Reheat_steam_flow_rate_input = Convert.ToDouble(mdataset.Tables[26].Rows[0]["Value"].ToString()) * 1000;
            //D_44
            if (mdataset.Tables[39].Rows[0]["SectionType"].ToString() == "Reheater Elements")
            {
                mCal_6B_SC.Main_steam_flow_rate_input = 0;
            }
            else
            {
                mCal_6B_SC.Main_steam_flow_rate_input = Convert.ToDouble(mdataset.Tables[27].Rows[0]["Value"].ToString()) * 1000;
            }
            //D_45
            mCal_6B_SC.De_superheating_spray_stage_1_input = Convert.ToDouble(mdataset.Tables[28].Rows[0]["Value"].ToString());
            //D_46
            mCal_6B_SC.De_superheating_spray_stage_2_if_any_input = Convert.ToDouble(mdataset.Tables[29].Rows[0]["Value"].ToString()) * 1000;
            //D_46
            mCal_6B_SC.Inlet_steam_enthalpy_of_superheater_at_furnace_roof_input = Convert.ToDouble(mdataset.Tables[30].Rows[0]["Value"].ToString());
            //D_47
            mCal_6B_SC.Inlet_steam_temperature_of_superheater_at_furnace_roof_input = Convert.ToDouble(mdataset.Tables[31].Rows[0]["Value"].ToString());
            //D_48
            mCal_6B_SC.Furnace_roof_pressure_within_panel_zone__input = Convert.ToDouble(mdataset.Tables[32].Rows[0]["Value"].ToString());
            //D_49
            mCal_6B_SC.Front_RH_inlet_steam_temperature_design_input = Convert.ToDouble(S_Para_Dataset.Tables[0].Rows[0]["Inlet_temperature"].ToString());
            //D_54
            mCal_6B_SC.Front_RH_effectiveness_coefficent_input = Convert.ToDouble(mdataset.Tables[33].Rows[0]["Value"].ToString());
            //D_55
            mCal_6B_SC.Ash_deposit_coefficient_input = Convert.ToDouble(mdataset.Tables[34].Rows[0]["Value"].ToString());
            //D_56
            mCal_6B_SC.Tube_wall_fouling_emmisivity_input = Convert.ToDouble(mdataset.Tables[35].Rows[0]["Value"].ToString());
            //D_57
            mCal_6B_SC.Fouling_uniformity_coefficient_input = Convert.ToDouble(mdataset.Tables[36].Rows[0]["Value"].ToString());

            //6B Calculation started----------------->
            //F_62
            mCal_6B_SC.Inlet_flue_gas_temperature = mCal_6B_SC.Temperature_of_flue_gas_into_panel_input;

            //F_63
            mCal_6B_SC.Inlet_flue_gas_enthalpy = mCal_6B_SC.Enthalpy_of_flue_gas_into_panel__input;


            Double Upstream_HEatingSection_steam_enthalpy = 0.0;
            //Assummed 

            //F_64
            mCal_6B_SC.Convection_heat_of_Front_reheater_zone = 1000;

            //F_65
            mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof = 200;

            //F_66
            mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_water_wall = 200;

            do
            {
                do
                {

                    do
                    {

                        //F_67
                        mCal_6B_SC.Outlet_flue_gas_enthalpy = (mCal_6B_SC.Inlet_flue_gas_enthalpy - mCal_6B_SC.Convection_heat_of_Front_reheater_zone) / mCal_6B_SC.Heat_preservation_coefficient_input;

                        string input_value = Convert.ToString(mCal_6B_SC.Outlet_flue_gas_enthalpy);
                        //F_68
                        mCal_6B_SC.Outlet_flue_gas_temperature = mCal_6B_BLL.Get_2A_Platen_Exit_Temp_ReqTemp(Boiler_ID, Project_ID, input_value, Boiler_Load, Objective_ID);

                        //F_69
                        mCal_6B_SC.Flue_gas_average_temperature = (mCal_6B_SC.Inlet_flue_gas_temperature + mCal_6B_SC.Outlet_flue_gas_temperature) / 2;

                        //F_70
                        mCal_6B_SC.Absorbed_convection_heat_of_reheated_steam = (mCal_6B_SC.Convection_heat_of_Front_reheater_zone - mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof - mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_water_wall);

                        //F_71
                        mCal_6B_SC.Product_of_pn_and_s = mCal_6B_SC.Partial_pressure_of_triatomic_gases_input * mCal_6B_SC.Effective_radiation_layer_thickness_input;

                        //F_72

                        Double Power = ((0.78 + 1.6 * mCal_6B_SC.Volume_fraction_of_water_vapor_input) / (Math.Pow((10.2 * mCal_6B_SC.Product_of_pn_and_s), 0.5))) - 0.1;
                        Double power1 = (1 - 0.37 * (mCal_6B_SC.Flue_gas_average_temperature + 273) / 1000);
                        Double POWER3 = 10.2 * Power * power1;
                        mCal_6B_SC.Radiant_absorption_coefficient_of_gas = Math.Round(POWER3, 2, MidpointRounding.AwayFromZero);

                        //F_73
                        //=43850*D27/((F69+273)^2*(D29)^2)^(1/3)
                        mCal_6B_SC.Radiant_absorption_coefficient_of_fly_ash =
                            Math.Round((43850 * mCal_6B_SC.Gas_density_input / (Math.Pow(Math.Pow(mCal_6B_SC.Flue_gas_average_temperature + 273, 2) * Math.Pow(mCal_6B_SC.Mean_diameter_of_ash_particle_input, 2), 0.333333333))), 2, MidpointRounding.AwayFromZero);

                        //F_74
                        mCal_6B_SC.Radiation_absorption_coefficient_of_flue_gas_radiation = mCal_6B_SC.Radiant_absorption_coefficient_of_gas * mCal_6B_SC.Volume_fraction_of_triatomic_gases_input + mCal_6B_SC.Radiant_absorption_coefficient_of_fly_ash * mCal_6B_SC.Dimensionless_concentration_of_fly_ash_input;

                        //F_75   =F74*D32*D18
                        mCal_6B_SC.Exponent_of_Eq_2_73 = mCal_6B_SC.Radiation_absorption_coefficient_of_flue_gas_radiation * mCal_6B_SC.Furnace_pressure_input * mCal_6B_SC.Effective_radiation_layer_thickness_input;

                        //F_76 
                        mCal_6B_SC.Flue_gas_emissivity = 1 - (Math.Exp(-(mCal_6B_SC.Exponent_of_Eq_2_73)));

                        //F_77
                        mCal_6B_SC.Radiation_heat_from_PSH_zone = mCal_6B_SC.Radiation_heat_from_flue_gas_in_PSH_zone_input + mCal_6B_SC.Radiation_heat_from_leakage_in_PSH_zone_input;
                        //F_78
                        mCal_6B_SC.Total_heat_absorbed_by_Front_Reheater = mCal_6B_SC.Absorbed_convection_heat_of_reheated_steam + mCal_6B_SC.Radiation_heat_from_PSH_zone;

                        //F_79
                        mCal_6B_SC.Inlet_steam_pressure_of_FRH = ((mCal_6B_SC.Front_RH_inlet_pressure_input) * 0.980665 + 1.01325) / 10;

                        //F_80
                        mCal_6B_SC.Outlet_steam_pressure_of_FRH = ((mCal_6B_SC.Front_RH_outlet_pressure_input) * 0.980665 + 1.01325) / 10;

                        //F_81 =h_pt(D40*0.980665+1.01325,D39+0.01)
                        Upstream_HEatingSection_steam_enthalpy = mStream_Macros_BLL.h_pT(mCal_6B_SC.Front_RH_inlet_pressure_input * 0.980665 + 1.01325, mCal_6B_SC.Front_RH_inlet_temperature_input + 0.01);

                        //F_82 =(F81*(D43-D44)+D45*D44)/D43
                        mCal_6B_SC.Inlet_steam_enthalpy = (Upstream_HEatingSection_steam_enthalpy * (mCal_6B_SC.Reheat_steam_flow_rate_input - mCal_6B_SC.Main_steam_flow_rate_input) + mCal_6B_SC.De_superheating_spray_stage_1_input * mCal_6B_SC.Main_steam_flow_rate_input) / mCal_6B_SC.Reheat_steam_flow_rate_input;


                        //F_83 =T_ph(10*F79,F82)
                        mCal_6B_SC.Inlet_steam_temperature = mStream_Macros_BLL.T_ph(10 * mCal_6B_SC.Inlet_steam_pressure_of_FRH, mCal_6B_SC.Inlet_steam_enthalpy);


                        //F_84 =F82+D35*F78/(D43/3600)
                        mCal_6B_SC.Outlet_steam_enthalpy = mCal_6B_SC.Inlet_steam_enthalpy + mCal_6B_SC.Design_fuel_consumption_input * mCal_6B_SC.Total_heat_absorbed_by_Front_Reheater / (mCal_6B_SC.Reheat_steam_flow_rate_input / 3600);
                        //F_85 =T_ph(10*F80,F84)
                        mCal_6B_SC.Outlet_steam_temperature = mStream_Macros_BLL.T_ph(10 * mCal_6B_SC.Outlet_steam_pressure_of_FRH, mCal_6B_SC.Outlet_steam_enthalpy);

                        //F_86
                        mCal_6B_SC.Average_steam_pressure_of_Reheater = 0.5 * (mCal_6B_SC.Inlet_steam_pressure_of_FRH + mCal_6B_SC.Outlet_steam_pressure_of_FRH);
                        //F_87
                        mCal_6B_SC.Average_steam_temperature_of_Reheater = 0.5 * (mCal_6B_SC.Outlet_steam_temperature + mCal_6B_SC.Inlet_steam_temperature);
                        //F_88
                        mCal_6B_SC.Average_specific_volume_of_steam_of_Reheater = mStream_Macros_BLL.v_pT(mCal_6B_SC.Average_steam_pressure_of_Reheater * 10, mCal_6B_SC.Average_steam_temperature_of_Reheater);
                        //F_88 =D43*F88/(3600*D10)
                        mCal_6B_SC.Average_speed_of_steam_of_Reheater = mCal_6B_SC.Reheat_steam_flow_rate_input * mCal_6B_SC.Average_specific_volume_of_steam_of_Reheater / (3600 * mCal_6B_SC.Steam_flow_area_input);
                        //F_89 
                        Double Con_Val = (mCal_6B_SC.Front_RH_tube_diameter_input - 2 * mCal_6B_SC.Front_RH_tube_thickness_input);
                        Double v1 = 4.167 * Math.Pow(10, -11) * Math.Pow(Con_Val, 6);
                        Double v2 = 1.173 * Math.Pow(10, -8) * Math.Pow(Con_Val, 5);
                        Double v3 = 1.337 * Math.Pow(10, -6) * Math.Pow(Con_Val, 4);
                        Double v4 = 8.01 * Math.Pow(10, -5) * Math.Pow(Con_Val, 3);
                        Double v5 = 2.77 * Math.Pow(10, -3) * Math.Pow(Con_Val, 2);
                        Double v6 = 0.06 * Con_Val;
                        Double v7 = 1.654;
                        mCal_6B_SC.Correction_factor_for_tube_diameter = v1 - v2 + v3 - v4 + v5 - v6 + v7;

                        //F_90
                        mCal_6B_SC.Thermal_conductivity_of_steam_of_Reheater_ = mStream_Macros_BLL.tc_pT(mCal_6B_SC.Average_steam_pressure_of_Reheater * 10, mCal_6B_SC.Average_steam_temperature_of_Reheater);
                        //F_91
                        mCal_6B_SC.Kinematic_viscosity_of_steam_of_Reheater = mStream_Macros_BLL.my_pT(mCal_6B_SC.Average_steam_pressure_of_Reheater * 10, mCal_6B_SC.Average_steam_temperature_of_Reheater) * mCal_6B_SC.Average_specific_volume_of_steam_of_Reheater;
                        //F_92
                        mCal_6B_SC.Prandtl_number_of_steam_of_Reheater = mStream_Macros_BLL.Pr_pT(mCal_6B_SC.Average_steam_pressure_of_Reheater * 10, mCal_6B_SC.Average_steam_temperature_of_Reheater);
                        //F_93  =0.023*F91/(D8/1000-2*D9/1000)*(F89*(D8/1000-2*D9/1000)/F92)^0.8*F93^0.4*F90

                        Double Const_Val = ((mCal_6B_SC.Front_RH_tube_diameter_input / 1000) - 2 * (mCal_6B_SC.Front_RH_tube_thickness_input / 1000));
                        Double f1 = 0.023 * mCal_6B_SC.Thermal_conductivity_of_steam_of_Reheater_ / Const_Val;
                        Double f2 = Math.Pow(((mCal_6B_SC.Average_speed_of_steam_of_Reheater * Const_Val) / mCal_6B_SC.Kinematic_viscosity_of_steam_of_Reheater), 0.8);
                        Double f3 = Math.Pow(mCal_6B_SC.Prandtl_number_of_steam_of_Reheater, 0.4);
                        Double f4 = mCal_6B_SC.Correction_factor_for_tube_diameter;
                        mCal_6B_SC.Steam_side_heat_transfer_coefficient_of_Reheater = f1 * f2 * f3 * f4;

                        //F_94
                        mCal_6B_SC.Flue_gas_velocity = mCal_6B_SC.Design_fuel_consumption_input * mCal_6B_SC.Flue_gas_total_volume_input * (mCal_6B_SC.Flue_gas_average_temperature + 273) / (273 * mCal_6B_SC.Gas_average_flow_area_input);
                        //F_95
                        mCal_6B_SC.Thermal_conductivity__of_flue_gas = 0.0000882529644268775 * mCal_6B_SC.Flue_gas_average_temperature + 0.0215130434782609;
                        //F_96
                        Double Val_A = 4.794 * Math.Pow(mCal_6B_SC.Flue_gas_average_temperature, 2) * Math.Pow(10, -11);
                        Double Val_B = 1.099 * Math.Pow(10, -7) * mCal_6B_SC.Flue_gas_average_temperature;
                        Double Val_C = 8.185 * Math.Pow(10, -6);
                        mCal_6B_SC.Kinematic_viscosity_of_flue_gas = Val_A + Val_B + Val_C;

                        //F_97
                        mCal_6B_SC.Average_Prandtl_number_of_flue_gas = 0.67 - 0.0001 * mCal_6B_SC.Flue_gas_average_temperature;

                        //F_98 =(0.94+0.56*D26)*F98
                        mCal_6B_SC.Prandtl_number_of_flue_gas = (0.94 + 0.56 * mCal_6B_SC.Volume_fraction_of_water_vapor_input) * mCal_6B_SC.Average_Prandtl_number_of_flue_gas;

                        //F_99
                        mCal_6B_SC.Correction_factor_for_rows = mCal_6B_SC.Correction_factor_for_tube_rows_input;
                        //F_100
                        mCal_6B_SC.Flue_gas_composition_and_temperature_correction_coefficient = 0.92 + 0.726 * mCal_6B_SC.Volume_fraction_of_water_vapor_input;
                        //F_101
                        Double Val_D16;

                        if (mCal_6B_SC.Relative_transverse_pitch_input <= 1.5)
                        {
                            mCal_6B_SC.Correction_factor_for_the_geometric_arrangement = 1;
                        }
                        else
                        {
                            if (mCal_6B_SC.Relative_vertical_pitch_input > 2)
                            {
                                mCal_6B_SC.Correction_factor_for_the_geometric_arrangement = 1;
                            }
                            else
                            {
                                if (mCal_6B_SC.Relative_transverse_pitch_input > 3)
                                {
                                    Val_D16 = 3;
                                    mCal_6B_SC.Correction_factor_for_the_geometric_arrangement = Math.Pow(1 + ((2 * Val_D16) - 3) * Math.Pow((1 - (mCal_6B_SC.Relative_vertical_pitch_input / 2)), 3), -2);
                                }
                                else
                                {
                                    Val_D16 = mCal_6B_SC.Relative_transverse_pitch_input;
                                    mCal_6B_SC.Correction_factor_for_the_geometric_arrangement = Math.Pow(1 + ((2 * Val_D16) - 3) * Math.Pow((1 - (mCal_6B_SC.Relative_vertical_pitch_input / 2)), 3), -2);
                                }
                            }
                        }




                        //F_102
                        mCal_6B_SC.Flue_gas_side_convection_coefficient = 0.2 * mCal_6B_SC.Thermal_conductivity__of_flue_gas / (mCal_6B_SC.Front_RH_tube_diameter_input / 1000) * Math.Pow((mCal_6B_SC.Flue_gas_velocity * (mCal_6B_SC.Front_RH_tube_diameter_input / 1000) / mCal_6B_SC.Kinematic_viscosity_of_flue_gas), 0.65) * Math.Pow(mCal_6B_SC.Prandtl_number_of_flue_gas, 0.33) * mCal_6B_SC.Correction_factor_for_rows * mCal_6B_SC.Correction_factor_for_the_geometric_arrangement * mCal_6B_SC.Flue_gas_composition_and_temperature_correction_coefficient;
                        // mCal_6B_SC.Flue_gas_side_convection_coefficient = 1;
                        //F_103
                        mCal_6B_SC.Ash_deposit_coefficient = mCal_6B_SC.Ash_deposit_coefficient_input;

                        //F_104
                        mCal_6B_SC.Fouling_layer_temperature_of_tube_wall_ = mCal_6B_SC.Average_steam_temperature_of_Reheater + 1000 * (mCal_6B_SC.Ash_deposit_coefficient + 1 / mCal_6B_SC.Steam_side_heat_transfer_coefficient_of_Reheater) * mCal_6B_SC.Design_fuel_consumption_input * (mCal_6B_SC.Absorbed_convection_heat_of_reheated_steam + mCal_6B_SC.Radiation_heat_from_PSH_zone) / mCal_6B_SC.Front_RH_total_heating_area_input;

                        //F_105
                        Double Con = ((mCal_6B_SC.Fouling_layer_temperature_of_tube_wall_ + 273) / (mCal_6B_SC.Flue_gas_average_temperature + 273));
                        Double val1 = 5.7 * Math.Pow(10, -8) * (mCal_6B_SC.Tube_wall_fouling_emmisivity_input + 1) / 2 * mCal_6B_SC.Flue_gas_emissivity * Math.Pow(mCal_6B_SC.Flue_gas_average_temperature + 273, 3);
                        Double val2 = 1 - Math.Pow(Con, 4);
                        Double val3 = 1 - Con;

                        mCal_6B_SC.Radiation_heat_transfer_coefficient_of_Reheater = val1 * val2 / val3;

                        //F_106
                        mCal_6B_SC.Fuel_correction_coefficient = 0.4;

                        //F_107  =F105*(1+F106*((F62+273)/1000)^0.25*('6A'!D16/'6A'!D14)^0.007)
                        Double Cal_6A_D16 = Convert.ToDouble(mdataset.Tables[38].Rows[0]["Value"].ToString());
                        Double Cal_6A_D14 = Convert.ToDouble(mdataset.Tables[37].Rows[0]["Value"].ToString());


                        mCal_6B_SC.Radiation_heat_transfer_coefficient_correction = mCal_6B_SC.Radiation_heat_transfer_coefficient_of_Reheater * (1 + mCal_6B_SC.Fuel_correction_coefficient * Math.Pow(((mCal_6B_SC.Inlet_flue_gas_temperature + 273) / 1000), 0.25) * Math.Pow((Cal_6A_D16 / Cal_6A_D14), 0.007));

                        //F_108
                        mCal_6B_SC.Fouling_uniformity_coefficient = mCal_6B_SC.Fouling_uniformity_coefficient_input;

                        //F_109
                        mCal_6B_SC.Effectiveness_coefficient = Convert.ToDouble(mdataset.Tables[33].Rows[0]["Value"].ToString());
                        //F_110
                        mCal_6B_SC.Flue_gas_side_heat_transfer_coefficient = mCal_6B_SC.Fouling_uniformity_coefficient * mCal_6B_SC.Flue_gas_side_convection_coefficient + mCal_6B_SC.Radiation_heat_transfer_coefficient_of_Reheater;
                        //F_111
                        mCal_6B_SC.Overall_heat_transfer_coefficient = mCal_6B_SC.Effectiveness_coefficient / (1 / mCal_6B_SC.Flue_gas_side_heat_transfer_coefficient + 1 / mCal_6B_SC.Steam_side_heat_transfer_coefficient_of_Reheater);


                        if (mdataset.Tables[40].Rows[0][0].ToString() == "1")
                        //F_128
                        {
                            //F_113 F113=F68-F85
                            mCal_6B_SC.Small_temperature_difference = mCal_6B_SC.Outlet_flue_gas_temperature - mCal_6B_SC.Outlet_steam_temperature;
                            //F_114 F114=F62-F83
                            mCal_6B_SC.Large_temperature_difference = mCal_6B_SC.Inlet_flue_gas_temperature - mCal_6B_SC.Inlet_steam_temperature;

                        }
                        else
                        {
                            //F_113 F113=F68-F83
                            mCal_6B_SC.Small_temperature_difference = mCal_6B_SC.Outlet_flue_gas_temperature - mCal_6B_SC.Outlet_steam_temperature;
                            //F_114 F114=F62-F85
                            mCal_6B_SC.Large_temperature_difference = mCal_6B_SC.Inlet_flue_gas_temperature - mCal_6B_SC.Inlet_steam_temperature;
                        }

                        //F_114 =(F113-F112)/LN(F113/F112)

                        mCal_6B_SC.Logarithmic_mean_temperature_difference = (mCal_6B_SC.Large_temperature_difference - mCal_6B_SC.Small_temperature_difference) / Math.Log(mCal_6B_SC.Large_temperature_difference / mCal_6B_SC.Small_temperature_difference);

                        //F_115
                        mCal_6B_SC.Convection_heat_of_front_Reheater = (mCal_6B_SC.Overall_heat_transfer_coefficient * mCal_6B_SC.Front_RH_total_heating_area_input * mCal_6B_SC.Logarithmic_mean_temperature_difference) / (1000 * mCal_6B_SC.Design_fuel_consumption_input);
                        //F_116
                        mCal_6B_SC.Error = (mCal_6B_SC.Absorbed_convection_heat_of_reheated_steam - mCal_6B_SC.Convection_heat_of_front_Reheater) / mCal_6B_SC.Absorbed_convection_heat_of_reheated_steam * 100;

                        //F_117
                        mCal_6B_SC.Working_medium_temperature_of_water_wall_Calc = mCal_6B_SC.Working_medium_temperature_of_water_wall_input;

                        //F_118
                        mCal_6B_SC.Average_temperature_difference_of_heat_transfer = mCal_6B_SC.Flue_gas_average_temperature - mCal_6B_SC.Working_medium_temperature_of_water_wall_Calc;
                        //F_119/
                        mCal_6B_SC.Absorbed_convection_heat_of_water_wall = mCal_6B_SC.Overall_heat_transfer_coefficient * mCal_6B_SC.Average_temperature_difference_of_heat_transfer * mCal_6B_SC.Side_Water_wall_heating_area__within_panel_input / (1000 * mCal_6B_SC.Design_fuel_consumption_input);
                        //F_120 =(F66-F120)
                        mCal_6B_SC.Error_Value_1 = mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_water_wall - mCal_6B_SC.Absorbed_convection_heat_of_water_wall;

                        mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_water_wall = mCal_6B_SC.Absorbed_convection_heat_of_water_wall;

                    } while (Math.Abs(mCal_6B_SC.Error_Value_1) >= 0.001 );

                    //F_121
                    mCal_6B_SC.Inlet_steam_enthalpy_of_superheater_at_furnace_roof = mCal_6B_SC.Inlet_steam_enthalpy_of_superheater_at_furnace_roof_input;
                    //F_122
                    mCal_6B_SC.Inlet_steam_temperature_of_superheater_at_furnace_roof = mCal_6B_SC.Inlet_steam_temperature_of_superheater_at_furnace_roof_input;
                    //F_123 =3600*F65*D35/(D46)
                    mCal_6B_SC.Steam_enthalpy_increment_of_superheater_at_furnace_roof = 3600 * mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof * mCal_6B_SC.Design_fuel_consumption_input / (mCal_6B_SC.De_superheating_spray_stage_2_if_any_input);

                    //F_124
                    mCal_6B_SC.Steam_pressure_of_furnace_roof_at_reheater_zone = ((mCal_6B_SC.Furnace_roof_pressure_within_panel_zone__input) * 0.980665 + 1.01325) / 10;
                    //F_125
                    mCal_6B_SC.Outlet_steam_enthalpy_of_furnace_roof_superheater = mCal_6B_SC.Inlet_steam_enthalpy_of_superheater_at_furnace_roof + mCal_6B_SC.Steam_enthalpy_increment_of_superheater_at_furnace_roof;

                    //F_126
                    mCal_6B_SC.Outlet_steam_temperature_of_furnace_roof_superheater = mStream_Macros_BLL.T_ph(mCal_6B_SC.Steam_pressure_of_furnace_roof_at_reheater_zone * 10, mCal_6B_SC.Outlet_steam_enthalpy_of_furnace_roof_superheater);

                    //F_127
                    mCal_6B_SC.Average_temperature_difference = mCal_6B_SC.Flue_gas_average_temperature - 0.5 * (mCal_6B_SC.Inlet_steam_temperature_of_superheater_at_furnace_roof + mCal_6B_SC.Outlet_steam_temperature_of_furnace_roof_superheater);

                    //F_128
                    mCal_6B_SC.Absorbed_convection_heat_of_superheater_at_furnace_roof = mCal_6B_SC.Overall_heat_transfer_coefficient * mCal_6B_SC.Average_temperature_difference * mCal_6B_SC.Heating_area_of_roof_tubes_in_PH_zone_input / (1000 * mCal_6B_SC.Design_fuel_consumption_input);
                    //F_129 =(F65-F129)
                    mCal_6B_SC.Error_Value_2 = (mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof - mCal_6B_SC.Absorbed_convection_heat_of_superheater_at_furnace_roof);

                    mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof = mCal_6B_SC.Absorbed_convection_heat_of_superheater_at_furnace_roof;

                    if (mCal_6B_SC.Error_Value_2 > 0)
                    {
                        mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof = mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof + 0.0001;
                    }
                    else
                    {
                        mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof = mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof - 0.0001;
                    }
                    // mCal_6B_SC.Convection_heat_of_Front_reheater_zone = mCal_6B_SC.Absorbed_convection_heat_within_Reheater_zone;

                }

                while (Math.Abs(mCal_6B_SC.Error_Value_2) >= 0.001);


                //} while (Math.Abs(mCal_6B_SC.Error_Value_2) >= 0.001);
                //F_130
                mCal_6B_SC.Absorbed_convection_heat_within_Reheater_zone = mCal_6B_SC.Convection_heat_of_front_Reheater + mCal_6B_SC.Absorbed_convection_heat_of_water_wall + mCal_6B_SC.Absorbed_convection_heat_of_superheater_at_furnace_roof;
                //F_131  =(F64-F131)
                mCal_6B_SC.Total_error = (mCal_6B_SC.Convection_heat_of_Front_reheater_zone - mCal_6B_SC.Absorbed_convection_heat_within_Reheater_zone);

                mCal_6B_SC.Convection_heat_of_Front_reheater_zone = mCal_6B_SC.Absorbed_convection_heat_within_Reheater_zone;



                //if (mCal_6B_SC.Total_error > 0)
                //{
                //    mCal_6B_SC.Absorbed_convection_heat_within_Reheater_zone = mCal_6B_SC.Absorbed_convection_heat_within_Reheater_zone + 0.0001;
                //}
                //else
                //{
                //    mCal_6B_SC.Absorbed_convection_heat_within_Reheater_zone = mCal_6B_SC.Absorbed_convection_heat_within_Reheater_zone - 0.0001;
                //}
                // mCal_6B_SC.Convection_heat_of_Front_reheater_zone = mCal_6B_SC.Absorbed_convection_heat_within_Reheater_zone;

                //}

                //while (mCal_6B_SC.Total_error <= -0.001);

            } while (Math.Abs(mCal_6B_SC.Total_error) >= 0.001);

            Double var1 = mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof;
            Double var2 = mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_water_wall;
            Double var3 = mCal_6B_SC.Convection_heat_of_Front_reheater_zone;

            //=F78*D35/1000
            Double Heat_absorption_heating_section = mCal_6B_SC.Total_heat_absorbed_by_Front_Reheater * mCal_6B_SC.Design_fuel_consumption_input / 1000;
            //=F65*D35/1000
            Double Roof = mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof * mCal_6B_SC.Design_fuel_consumption_input / 1000;
            //=F66*D35/1000
            Double Water_wall = mCal_6B_SC.Absorbed_heat_of_additional_heating_surfaces_of_water_wall * mCal_6B_SC.Design_fuel_consumption_input / 1000;

            mCal_6B_BLL.InsertUpadate_6B_S_ParameterValue(Project_ID, Boiler_ID, Boiler_Load, Objective_ID, Section_ID, Math.Round(mCal_6B_SC.Front_RH_inlet_temperature_input,2), Math.Round(mCal_6B_SC.Outlet_steam_temperature,2), iteration,Previous_SectionID);


            DataTable dt1 = new DataTable();
            dt1 = mCal_6B_BLL.Get_PID_For_6B_Calculation();

            foreach (DataRow row in dt1.Rows)
            {
                int pid = Convert.ToInt16(row["PID"].ToString());
              //  Boiler_Load = "100%PP";
                switch (pid)
                {


                    case 1:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Front_RH_inlet_temperature_input,2));

                        break;
                    case 2:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Outlet_steam_temperature,2));

                        break;
                    case 3:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID,Math.Round(mCal_6B_SC.Temperature_of_flue_gas_into_panel_input,2));
                        break;

                    case 4:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Outlet_flue_gas_temperature,2));

                        break;
                    case 5:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Inlet_steam_temperature,2));

                        break;

                    case 6:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Inlet_steam_temperature_of_superheater_at_furnace_roof,2));

                        break;
                    case 7:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Outlet_steam_temperature_of_furnace_roof_superheater,2));

                        break;
                    case 8:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Working_medium_temperature_of_water_wall_Calc,2));

                        break;
                    case 9:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(Heat_absorption_heating_section,2));

                        break;
                    case 10:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(Roof,2));

                        break;
                    case 11:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID,Math.Round( Water_wall,2));

                        break;
                    case 12:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Outlet_flue_gas_enthalpy,2));

                        break;
                    case 13:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Flue_gas_velocity,2));

                        break;
                    case 14:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Radiation_heat_from_PSH_zone,2));

                        break;
                    case 15:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Absorbed_convection_heat_of_reheated_steam,2));

                        break;
                    case 16:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Inlet_steam_enthalpy,2));

                        break;
                    case 17:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Inlet_steam_pressure_of_FRH,2));

                        break;
                    case 18:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Outlet_steam_pressure_of_FRH,2));

                        break;
                    case 19:
                        mCal_6B_BLL.Insert_6B_Calculation(pid, Boiler_ID, Project_ID, Objective_ID, Boiler_Load, Section_ID, Math.Round(mCal_6B_SC.Steam_side_heat_transfer_coefficient_of_Reheater,2));

                        break;

                }

            }
        }
    }
}