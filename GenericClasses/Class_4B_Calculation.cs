using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using BoilerModellingTool.SC;
using BoilerModellingTool.BLL;
//Changes done by Preeti --09--01-2021 


namespace GenericClasses
{


    public class Class_4B_Calculation
    {
        private readonly double areaFactor;

        // string str = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //string Project_ID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
        //string Boiler_ID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
        //string Boiler_Load = "100%TMCR";
        string Last_Element_UF = "";
        //Sub module in Upper Furnace---used different Boiler ID for project 
        //string P = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
        //string B = "638432A6-FF38-48FE-B901-0BD85A4033BC";

        string Boiler_ID = null;
        string Project_ID = null;
        string SectionID = null;
        string Boiler_Load = null;
        int ObjectiveID = 0;


        public Class_4B_Calculation(string BoilerID, string Project_ID,
             string BoilerLoad, int ObjectiveID)
        {

            this.Boiler_ID = BoilerID;
            this.Project_ID = Project_ID;
            this.Boiler_Load = BoilerLoad;
            this.ObjectiveID = ObjectiveID;

        }



        public void Calculation_4B_REsult()
        {

            Cal_4B_SC mCal_4B_SC = null;
            Cal_4B_BLL mCal_4B_BLL = null;
            Stream_Macros mStream_Macros_BLL = null;
            DataSet mdataset = null;
            DataTable mDataTable = null;

            try
            {
                mCal_4B_SC = new Cal_4B_SC();
            mCal_4B_BLL = new Cal_4B_BLL();
            mStream_Macros_BLL = new Stream_Macros();
            mdataset = new DataSet();

            mdataset = mCal_4B_BLL.GetInput_For_4B_Calculation(Boiler_ID, Project_ID, Boiler_Load);
            Double Lower_furnace_EPRS_area = Convert.ToDouble(mdataset.Tables[0].Rows[0]["Value"].ToString());
            Double Lower_furnace_enclosed_area = Convert.ToDouble(mdataset.Tables[1].Rows[0]["Value"].ToString());
            Double Exit_window_area_of_lower_furnace = Convert.ToDouble(mdataset.Tables[2].Rows[0]["Value"].ToString());
            Double Thickness_of_effective_radiation_layer_lower_furnace = Convert.ToDouble(mdataset.Tables[3].Rows[0]["Value"].ToString());
            Double Upper_furnace_absorbing_area = Convert.ToDouble(mdataset.Tables[4].Rows[0]["Value"].ToString());
            Double Upper_furnace_exit_window_area = Convert.ToDouble(mdataset.Tables[5].Rows[0]["Value"].ToString());
            Double Thickness_of_effective_radiation_layer_upper_furnace = Convert.ToDouble(mdataset.Tables[6].Rows[0]["Value"].ToString());
            Double half_dry_bottom_hopper_height = Convert.ToDouble(mdataset.Tables[7].Rows[0]["Value"].ToString());
            Double Furnace_main_body_height_ = Convert.ToDouble(mdataset.Tables[8].Rows[0]["Value"].ToString());
            Double Furnace_nose_downward_inclination__angle_height = Convert.ToDouble(mdataset.Tables[9].Rows[0]["Value"].ToString());
            Double Operating_burners_middle_elevation = Convert.ToDouble(mdataset.Tables[10].Rows[0]["Value"].ToString());
            Double Hopper_Middle_elevation = Convert.ToDouble(mdataset.Tables[11].Rows[0]["Value"].ToString());
            Double Primary_air_temperature_at_APH_outlet = Convert.ToDouble(mdataset.Tables[12].Rows[0]["Value"].ToString());
            Double Secondary_air_temperature_at_APH_outlet = Convert.ToDouble(mdataset.Tables[13].Rows[0]["Value"].ToString());
            Double Temp_of_leakage_air_into_furnace = Convert.ToDouble(mdataset.Tables[14].Rows[0]["Value"].ToString());

            Double Total_combustion_air_incl_furnace_leakage = Convert.ToDouble(mdataset.Tables[15].Rows[0]["Value"].ToString()) / 3.6;
            Double Primary_air_flow_rate_at_APH_oulet = Convert.ToDouble(mdataset.Tables[16].Rows[0]["Value"].ToString()) / 3.6;
            Double Secondary_air_flow_rate_at_APH_outlet = Convert.ToDouble(mdataset.Tables[17].Rows[0]["Value"].ToString()) / 3.6;
            Double Tempering_air_flow_rate = Convert.ToDouble(mdataset.Tables[18].Rows[0]["Value"].ToString()) / 3.6;

            Double Excess_air_ratio = Convert.ToDouble(mdataset.Tables[19].Rows[0]["Lower furnace"].ToString());
            Double Lower_heating_value = Convert.ToDouble(mdataset.Tables[20].Rows[0]["Value"].ToString());
            Double Unburnt_carbon_loss = Convert.ToDouble(mdataset.Tables[21].Rows[0]["Value"].ToString());
            Double Heat_loss_due_to_incomplete_combustion = Convert.ToDouble(mdataset.Tables[22].Rows[0]["Value"].ToString());
            Double Physical_heat_loss_of_ash = Convert.ToDouble(mdataset.Tables[23].Rows[0]["Value"].ToString());
            Double Buner_tilt = Convert.ToDouble(mdataset.Tables[24].Rows[0]["Value"].ToString());
            Double Drum_pressure = Convert.ToDouble(mdataset.Tables[25].Rows[0]["Value"].ToString());
            //Double Drum_pressure = 167.0;
            //  Double Economizer_hanger_outlet_steam_enthalpy = 1228.9; //Convert.ToDouble(mdataset.Tables[26].Rows[0]["Value"].ToString());//Input from 13B 


            Double Main_steam_flow_rate = Convert.ToDouble(mdataset.Tables[27].Rows[0]["Value"].ToString()) * 1000;
            Double Desuperheating_spray_stage1 = Convert.ToDouble(mdataset.Tables[28].Rows[0]["Value"].ToString()) * 1000;
            Double Desuperheating_spray_stage2_if_any = Convert.ToDouble(mdataset.Tables[29].Rows[0]["Value"].ToString()) * 1000;

            Double Mean_diameter_of_ash_particle = Convert.ToDouble(mdataset.Tables[30].Rows[0]["Value"].ToString());
            Double Volume_fraction_of_water_vapor = Convert.ToDouble(mdataset.Tables[32].Rows[0]["Lower furnace"].ToString());
            Double Volume_fraction_of_triatomic_gases = Convert.ToDouble(mdataset.Tables[33].Rows[0]["Lower furnace"].ToString());
            Double Gas_density = Convert.ToDouble(mdataset.Tables[34].Rows[0]["Lower furnace"].ToString());
            Double Furnace_pressure = Convert.ToDouble(mdataset.Tables[35].Rows[0]["Value"].ToString());
            Double Dimensionless_concentration_of_fly_ash = Convert.ToDouble(mdataset.Tables[36].Rows[0]["Lower furnace"].ToString());
            Double Heat_preservation_coefficient = Convert.ToDouble(mdataset.Tables[37].Rows[0]["Value"].ToString());
            Double Design_fuel_consumption = Convert.ToDouble(mdataset.Tables[38].Rows[0]["Value"].ToString()) / 3.6;
            Double Radiation_absorption_coefficient_of_coke_particles = Convert.ToDouble(mdataset.Tables[40].Rows[0]["Value"].ToString());
            Double Recirculated_gas_PC_of_total_gas_flow = Convert.ToDouble(mdataset.Tables[39].Rows[0]["Value"].ToString());
            Double Recirculated_gas_temperature = mCal_4B_BLL.Get_2A_Recirculated_gas_temperature(Boiler_ID, Project_ID, Boiler_Load);
            Double Recirculated_gas_enthalpy = mCal_4B_BLL.Get_2A_Recirculated_gas_enthalpy(Boiler_ID, Project_ID, Recirculated_gas_PC_of_total_gas_flow.ToString());

            //Double Radiation_absorption_coefficint_of_coke_particles = Convert.ToDouble(mdataset.Tables[39].Rows[0]["Value"].ToString());


            Double Dimensionless_number_of_coke_particle_realted_to_coal_type = Convert.ToDouble(mdataset.Tables[41].Rows[0]["Value"].ToString());
            Double Dimensionless_number_of_coke_particle_realted_to_combustion_mode = Convert.ToDouble(mdataset.Tables[42].Rows[0]["Value"].ToString());
            Double Water_wall_fouling_factor = Convert.ToDouble(mdataset.Tables[43].Rows[0]["Value"].ToString());



            //CALCULATIONS

            //f_66
            Double Tempering_air_temperature = Temp_of_leakage_air_into_furnace + 5;

            mCal_4B_SC.Tempering_air_temperature = Tempering_air_temperature;
            //f_67
            Double Primary_air_enthalpy = mCal_4B_BLL.Get_2A_PrimaryAir_ReqEnthalpy(Boiler_ID, Project_ID, Primary_air_temperature_at_APH_outlet.ToString());

            mCal_4B_SC.Primary_air_enthalpy = Primary_air_enthalpy;
            //f_68
            Double Secondary_air_enthalpy = mCal_4B_BLL.Get_2A_PrimaryAir_ReqEnthalpy(Boiler_ID, Project_ID, Secondary_air_temperature_at_APH_outlet.ToString());
            mCal_4B_SC.Secondary_air_enthalpy = Secondary_air_enthalpy;

            //f_69
            Double Tempering_air_enthalpy = mCal_4B_BLL.Get_2A_PrimaryAir_ReqEnthalpy(Boiler_ID, Project_ID, Tempering_air_temperature.ToString());
            mCal_4B_SC.Tempering_air_enthalpy = Tempering_air_enthalpy;
            //f_70
            Double Leakage_air_enthalpy = mCal_4B_BLL.Get_2A_PrimaryAir_ReqEnthalpy(Boiler_ID, Project_ID, Temp_of_leakage_air_into_furnace.ToString());

            mCal_4B_SC.Leakage_air_enthalpy = Leakage_air_enthalpy;
            //f_71
            Double Leakage_air_flow_rate = Total_combustion_air_incl_furnace_leakage - (Primary_air_flow_rate_at_APH_oulet + Secondary_air_flow_rate_at_APH_outlet + Tempering_air_flow_rate);

            mCal_4B_SC.Leakage_air_flow_rate = Leakage_air_flow_rate;
            //F_72
            Double Heat_input_by_air_sensible = ((Primary_air_flow_rate_at_APH_oulet * Primary_air_enthalpy + Secondary_air_flow_rate_at_APH_outlet * Secondary_air_enthalpy + Tempering_air_flow_rate * Tempering_air_enthalpy + Leakage_air_flow_rate * Leakage_air_enthalpy) * Excess_air_ratio) / (Total_combustion_air_incl_furnace_leakage);
            mCal_4B_SC.Heat_input_by_air_sensible_ = Heat_input_by_air_sensible;

            //F_73
            Double Heat_input_by_gas_recirculation = Recirculated_gas_enthalpy * Recirculated_gas_PC_of_total_gas_flow / (100 + Recirculated_gas_PC_of_total_gas_flow);
            mCal_4B_SC.Heat_input_by_gas_recirculation = Heat_input_by_gas_recirculation;

            //f_74
            Double Heat_input_by_1_kg_fuel = (Lower_heating_value * (100 - Unburnt_carbon_loss - Heat_loss_due_to_incomplete_combustion - Physical_heat_loss_of_ash)) / (100) + Heat_input_by_air_sensible;
            mCal_4B_SC.Heat_input_by_1_kg_fuel = Heat_input_by_1_kg_fuel;

            //f_75
            Double Theoretical_combustion_temparature_C = mCal_4B_BLL.Get_2A_AdiabaticTemp_ReqTemp(Boiler_ID, Project_ID, Heat_input_by_1_kg_fuel.ToString());
            mCal_4B_SC.Theoretical_combustion_temparature_C = Theoretical_combustion_temparature_C;

            //f_76
            Double Theoretical_combustion_temparature_k = Theoretical_combustion_temparature_C + 273;
            mCal_4B_SC.Theoretical_combustion_temparature_K = Theoretical_combustion_temparature_k;

            //_________________Not updating Values________________________
            //f_88
            Double Average_thermal_efficiency_coefficient = Water_wall_fouling_factor * 1;
            mCal_4B_SC.Average_thermal_efficiency_coefficient = Average_thermal_efficiency_coefficient;
            //f_90
            Double Burner_height = Operating_burners_middle_elevation - Hopper_Middle_elevation;
            mCal_4B_SC.Burner_height = Burner_height;
            //f_91
            Double Height_from_dry_bottom_hopper_to_center_of_furnace_exit = half_dry_bottom_hopper_height + Furnace_main_body_height_ + Furnace_nose_downward_inclination__angle_height;
            mCal_4B_SC.Height_from_dry_bottom_hopper_to_center_of_furnace_exit = Height_from_dry_bottom_hopper_to_center_of_furnace_exit;
            //f_92
            Double Burner_relative_height = Burner_height / Height_from_dry_bottom_hopper_to_center_of_furnace_exit;
            mCal_4B_SC.Burner_relative_height = Burner_relative_height;
            //f_93
            Double Parameter = 0.1 / 20 * Buner_tilt;
            mCal_4B_SC.Parameter = Parameter;
            //f_94
            Double Relative_height_of_flame_center = Burner_relative_height + Parameter;
            mCal_4B_SC.Relative_height_of_flame_center = Relative_height_of_flame_center;
            //F95  
            Double Coefficient_for_flame_centre_modification = Convert.ToDouble(mdataset.Tables[46].Rows[0]["Value"].ToString());
            //F96   
            Double Coefficient_of_recirculation = Recirculated_gas_PC_of_total_gas_flow / 100;
            //F97  ='1A'!F67
            Double Gas_volume_leaving_the_furnace = Convert.ToDouble(mdataset.Tables[47].Rows[0]["Lower Furnace"].ToString());
            //F98    =('1A'!F48*0.79)*('1A'!D31-1)+'1A'!F49
            Double Val = Convert.ToDouble(mdataset.Tables[48].Rows[0]["Value"].ToString()) * 0.79;
            Double Val1 = Convert.ToDouble(mdataset.Tables[51].Rows[0]["Value"].ToString()) - 1;
            Double Val2 = Convert.ToDouble(mdataset.Tables[49].Rows[0]["Value"].ToString());
            Double Volume_of_N2_at_furnace_exit = Val * Val1 + Val2;
            //F99    ='1A'!F48*0.21*('1A'!D36-1)
            Double Val3 = Convert.ToDouble(mdataset.Tables[48].Rows[0]["Value"].ToString()) * 0.21;
            Double Val4 = Convert.ToDouble(mdataset.Tables[52].Rows[0]["Value"].ToString()) - 1;
            Double Volume_of_oxygen = Val3 * Val4;

            //F100   
            Double Volume_of_triatomic_gases = Convert.ToDouble(mdataset.Tables[50].Rows[0]["Value"].ToString());
            //F101  =F97*(1+F96)/(F98+F99+F100) 
            Double Gas_fraction = Gas_volume_leaving_the_furnace * (1 + Coefficient_of_recirculation) / (Volume_of_N2_at_furnace_exit + Volume_of_oxygen + Volume_of_triatomic_gases);

            //f_80
            Double Partial_pressure_of_triatomic_gases = Furnace_pressure * Volume_fraction_of_triatomic_gases;
            mCal_4B_SC.Partial_pressure_of_triatomic_gases = Partial_pressure_of_triatomic_gases;


            //---------------Assigned Value--------------

            //f108
            Double Furnace_outlet_gas_temperature_error = 0;
            mCal_4B_SC.Furnace_outlet_gas_temperature_error = Furnace_outlet_gas_temperature_error;
            //F 77
            // Double Furnace_outlet_gas_temperature = 1355.9;
            Double Furnace_outlet_gas_temperature = 1;
            mCal_4B_SC.Furnace_outlet_gas_temperature = Furnace_outlet_gas_temperature;
            //f82
            Double Average__gas_temp_in_lower_furnace = 0;
            mCal_4B_SC.Average__gas_temp_in_lower_furnace_ = Average__gas_temp_in_lower_furnace;
            //f78
            Double Furnace_outlet_gas_enthalpy = 0;
            mCal_4B_SC.Furnace_outlet_gas_enthalpy = Furnace_outlet_gas_enthalpy;

            Double Mean_overall_heat_capacity_combustion_products = 0.0;
            Double Exponent_of_Eq_273 = 0.0;
            do
            {

                //f78
                Furnace_outlet_gas_enthalpy = mCal_4B_BLL.Get_2A_FEGT_ReqEnthalpy(Boiler_ID, Project_ID, Furnace_outlet_gas_temperature.ToString());
                mCal_4B_SC.Furnace_outlet_gas_enthalpy = Furnace_outlet_gas_enthalpy;
                //F79
                Mean_overall_heat_capacity_combustion_products = (Heat_input_by_1_kg_fuel - Furnace_outlet_gas_enthalpy) / (Theoretical_combustion_temparature_C - Furnace_outlet_gas_temperature);


                //F 104 =F95*(1-(0.4*F94))*F101^(1/3)
                Double Flame_center_modification_facor = Coefficient_for_flame_centre_modification * (1 - (0.4 * Relative_height_of_flame_center)) * Math.Pow(Gas_fraction, (0.3333333));
                mCal_4B_SC.Flame_center_modification_facor = Flame_center_modification_facor;

                //f_107
                Double Radiative_absorbed_heat_of_furnace = Heat_preservation_coefficient * (Heat_input_by_1_kg_fuel - Furnace_outlet_gas_enthalpy);
                mCal_4B_SC.Radiative_absorbed_heat_of_furnace = Radiative_absorbed_heat_of_furnace;

                Flame_center_modification_facor = 0.3743;

                //=((F107*(F76^2))/((5.67*10^-11)*10800*F74)*((1-(F77+273)/F76)/F104)^(-5/3))^(1/4)
                Double a = (Radiative_absorbed_heat_of_furnace * Math.Pow(Theoretical_combustion_temparature_k, 2)) / (5.67 * Math.Pow(10, -11) * 10800 * Heat_input_by_1_kg_fuel);
                //2832764236338.8115
                Double b = (1 - ((Furnace_outlet_gas_temperature + 273) / Theoretical_combustion_temparature_k));
                //0.27207696494691591
                Double c = Math.Pow((b / Flame_center_modification_facor), (-1.66666));
                //1.2010784463104316
                Double d = a * c;
                //f_81
                Average__gas_temp_in_lower_furnace = Math.Pow(d, 0.25);
                mCal_4B_SC.Average__gas_temp_in_lower_furnace_ = Average__gas_temp_in_lower_furnace;
                //f_82
                Double Product_of_pn_and_s = Partial_pressure_of_triatomic_gases * Thickness_of_effective_radiation_layer_lower_furnace;
                mCal_4B_SC.Product_of_pn_and_s = Product_of_pn_and_s;
                //f83 =10.2*((0.78+1.6*D45)/(10.2*F82)^0.5-0.1)*(1-0.37*(F81)/1000)
                Double Power = ((0.78 + 1.6 * Volume_fraction_of_water_vapor) / (Math.Pow((10.2 * Product_of_pn_and_s), 0.5))) - 0.1;
                Double power1 = (1 - 0.37 * Average__gas_temp_in_lower_furnace / 1000);
                Double POWER3 = 10.2 * Power * power1;

                Double Radiation_absorption_coefficient_of_gas = POWER3;
                //F84

                Double Radiation_absorption_coefficient_of_fly_ash = (43850 * Gas_density / (Math.Pow(Math.Pow(Average__gas_temp_in_lower_furnace, 2) * Math.Pow(Mean_diameter_of_ash_particle, 2), 0.333333333)));
                mCal_4B_SC.Radiation_absorption_coefficient_of_fly_ash = Radiation_absorption_coefficient_of_fly_ash;
                //f85
                Double Radiation_absorption_coefficient_of_flame_radiation = (Radiation_absorption_coefficient_of_gas * Volume_fraction_of_triatomic_gases) + (Radiation_absorption_coefficient_of_fly_ash * Dimensionless_concentration_of_fly_ash) + (Radiation_absorption_coefficient_of_coke_particles * Dimensionless_number_of_coke_particle_realted_to_coal_type * Dimensionless_number_of_coke_particle_realted_to_combustion_mode);
                mCal_4B_SC.Radiation_absorption_coefficient_of_flame_radiation = Radiation_absorption_coefficient_of_flame_radiation;

                //f_86
                Exponent_of_Eq_273 = Radiation_absorption_coefficient_of_flame_radiation * Furnace_pressure * Thickness_of_effective_radiation_layer_lower_furnace;
                mCal_4B_SC.Exponent_of_Eq_2_73 = Exponent_of_Eq_273;



                //F102  
                Double Buger_criteria = Exponent_of_Eq_273;
                //F103   =1.6*LN((1.4*F102^2+F102+2)/(1.4*F102^2-F102+2))
                Double Const1 = 1.4 * Math.Pow(Buger_criteria, 2) + Buger_criteria + 2;
                Double const2 = 1.4 * Math.Pow(Buger_criteria, 2) - Buger_criteria + 2;
                Double Effective_Buger_criteria = 1.6 * Math.Log((Const1 / const2));

                //f106
                Double Furnace_outlet_gas_enthalpy2 = mCal_4B_BLL.Get_2A_FEGT_ReqEnthalpy(Boiler_ID, Project_ID, Furnace_outlet_gas_temperature.ToString());
                mCal_4B_SC.Furnace_outlet_gas_enthalpy = Furnace_outlet_gas_enthalpy2;



                //f_87

                Double Furnace_flame_emissivity = 1 - (Math.Exp(-(Exponent_of_Eq_273)));
                mCal_4B_SC.Furnace_flame_emissivity = Furnace_flame_emissivity;

                //f89
                Double Furnace_emissivity = Furnace_flame_emissivity / (Average_thermal_efficiency_coefficient * (1 - Furnace_flame_emissivity) + Furnace_flame_emissivity);
                mCal_4B_SC.Furnace_emissivity = Furnace_emissivity;
                //f105 =(F76/(1+F104*(F103)^0.3*(((5.67*10^-11)*F88*D9*(F76^3))/(D50*D51*F79))^0.6))-273
                //Double one = Theoretical_combustion_temparature_k;
                //Double two = 1 + Flame_center_modification_facor * Math.Pow((Effective_Buger_criteria), 0.3);
                //Double three = ((5.67 * Math.Pow(10, -11)) * Average_thermal_efficiency_coefficient * Lower_furnace_enclosed_area * Math.Pow(Theoretical_combustion_temparature_k, 3));
                //Double Four = (Heat_preservation_coefficient * Design_fuel_consumption * Mean_overall_heat_capacity_combustion_products);
                //Double Five = Math.Pow(three / Four, 0.6);
                //Double Furnace_outlet_gas_temperature_New = (one / (two * Five)) - 273;

                Double one = Theoretical_combustion_temparature_k;
                Double two = Flame_center_modification_facor * Math.Pow((Effective_Buger_criteria), 0.3);
                Double three = ((5.67 * Math.Pow(10, -11)) * Average_thermal_efficiency_coefficient * Lower_furnace_enclosed_area * Math.Pow(Theoretical_combustion_temparature_k, 3));
                Double Four = (Heat_preservation_coefficient * Design_fuel_consumption * Mean_overall_heat_capacity_combustion_products);
                Double Five = Math.Pow(three / Four, 0.6);
                Double Furnace_outlet_gas_temperature_New = one / (1 + (two * Five)) - 273;


                Furnace_outlet_gas_temperature_error = Furnace_outlet_gas_temperature - Furnace_outlet_gas_temperature_New;

                Furnace_outlet_gas_temperature = Furnace_outlet_gas_temperature_New;

            } while (Math.Abs(Furnace_outlet_gas_temperature_error) >= 0.01);

            //Furnace_outlet_gas_enthalpy = mCal_4B_BLL.Get_2A_FEGT_ReqEnthalpy(Boiler_ID, Project_ID, Furnace_outlet_gas_temperature.ToString());
            //mCal_4B_SC.Furnace_outlet_gas_enthalpy = Furnace_outlet_gas_enthalpy;

            //f_107 =D50*(F74-F106)
            Double Radiative_absorbed_heat_of_furnace_ = Heat_preservation_coefficient * (Heat_input_by_1_kg_fuel - Furnace_outlet_gas_enthalpy);

            //Random Generator Logic

            //Double SB_Mod_for_upper_furnace_calc_Error = 0;
            //Double SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnaceTg_k = 0;
            //Double SB_Mod_for_Emissivity_calc_Pns = 0;
            //Double SB_Mod_for_Emissivity_calc_for_upper_furnaceTemp_K = 0;
            //Double SB_Mod_for_Emissivity_calc_for_upper_furnaceKg = 0;
            //Double SB_Mod_for_Emissivity_calc_for_upper_furnaceKfa = 0;
            //Double SB_Mod_for_Emissivity_calc_for_upper_furnaceK = 0;
            //Double SB_Mod_for_Emissivity_calc_for_upper_furnaceflame_emissivity_af = 0;
            //Double SB_Mod_for_Emissivity_calc_for_upper_furnaceZeta_based_on_Tg = 0;
            //Double SB_Mod_for_Emissivity_calc_for_upper_furnaceFurnace_emisisvity_based_on_zeta = 0;
            //Double SB_Mod_for_upper_furnace_calc_WW_exit_radiation = 0;
            Double SB_Mod_for_upper_furnace_calc_Downstream_element_inlet_fluegas_temperature_Tpsh_in = 0;
            //Double SB_Mod_for_upper_furnace_calc_Downstream__element_Inlet_flue_gas__enthalpy = 0;
            //Double SB_Mod_for_upper_furnace_calc_Available_heat = 0;
            Double SB_Mod_for_upper_furnace_calc_Radiation_flux_to_downstream__heating_element = 0;
            //Double SB_Mod_for_Upper_furnace_flux_calculation_Nose_inlet_Temp = 0;
            //Double SB_Mod_for_Upper_furnace_flux_calculation_water_wall_Temp = 0;
            //Double SB_Mod_for_Upper_furnace_flux_calculation_Roof_geometric_mean_Temp = 0;
            //Double SB_Mod_for_Upper_furnace_flux_calculation_Nose_inlet_Heat = 0;
            //Double SB_Mod_for_Upper_furnace_flux_calculation_water_wall_Heat = 0;
            //Double SB_Mod_for_Upper_furnace_flux_calculation_Roof_geometric_mean_Heat = 0;

            //* submodule for upper furnace calc
            //M 78
            Double SB_Mod_for_upper_furnace_calc_Lower_furnace_exit_temp = Furnace_outlet_gas_temperature;
            //M 79
            Double SB_Mod_for_upper_furnace_calc_Lower_furnace_exit_enthalpy = Furnace_outlet_gas_enthalpy;
            mCal_4B_SC.calc_Lower_furnace_exit_temp = SB_Mod_for_upper_furnace_calc_Lower_furnace_exit_temp;
            mCal_4B_SC.calc_Lower_furnace_exit_enthalpy = SB_Mod_for_upper_furnace_calc_Lower_furnace_exit_enthalpy;
            //M 80
            Double SB_Mod_for_upper_furnace_calc_Heat_available_lower_furnace = Heat_input_by_1_kg_fuel - SB_Mod_for_upper_furnace_calc_Lower_furnace_exit_enthalpy;
            mCal_4B_SC.calc_Heat_available_lower_furnace = SB_Mod_for_upper_furnace_calc_Heat_available_lower_furnace;
            //M 81
            Double SB_Mod_for_upper_furnace_calc_Direct_radiation_from_lower_furnace = SB_Mod_for_upper_furnace_calc_Heat_available_lower_furnace / (Lower_furnace_enclosed_area - Exit_window_area_of_lower_furnace) * Exit_window_area_of_lower_furnace;

            mCal_4B_SC.calc_Direct_radiation_from_lower_furnace = SB_Mod_for_upper_furnace_calc_Direct_radiation_from_lower_furnace;
            //M 82
            mCal_4B_SC.Direct_radiation_from_lower_furnace_MW = SB_Mod_for_upper_furnace_calc_Direct_radiation_from_lower_furnace * Design_fuel_consumption / 1000;
            //M 83
            Double SB_Mod_for_upper_furnace_calc_zeta_ww_upper_furance = Convert.ToDouble(mdataset.Tables[45].Rows[0]["Value"]);
            //M 84
            Double SB_Mod_for_upper_furnace_calc_Tg_Tpsh_in_ratio = Convert.ToDouble(mdataset.Tables[44].Rows[0]["TP_TSH"]);
            //M 85
            mCal_4B_SC.calc_Tg_Tpsh_in_ratio = SB_Mod_for_upper_furnace_calc_Tg_Tpsh_in_ratio;

            //-----New Changes Updated 19-08-2020 by Preeti Ubale 

            string P = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
            P = Project_ID;
            string B = "638432A6-FF38-48FE-B901-0BD85A4033BC";
            B = Boiler_ID;
            //string P = "542A72B7-C79F-4549-B7CC-B7838ED04D43";
            //P = Project_ID;
            //string B = "A304B6AD-2B90-4A34-A559-9E1F799C493D";
            //B = Boiler_ID;
            //string P = Project_ID;
            //string B = Boiler_ID;
            //String S = SectionID;
            //String S = "65280810-EDC4-448D-AF1D-BD502C761FA1";

            //DataSet MSubmodule_Dataset = null;
            //DataSet Design_Area = null;


            //MSubmodule_Dataset = mCal_4B_BLL.Get_submodule_heating_section_in_upper_furnace(B, P);

            //System.Diagnostics.Debug.WriteLine("Before assignment P: " + P);
            //P = Project_ID;
            //System.Diagnostics.Debug.WriteLine("After assignment P: " + P);

            //int No_OF_Heating_Ele = Convert.ToInt16(MSubmodule_Dataset.Tables[0].Rows[0][0].ToString());

            //List<Double> Zeta_val = new List<Double>();
            //List<string> Section_val = new List<string>();
            //List<Double> Design_Area_val = new List<Double>();
            //List<Double> Heat_Section_Absorbed = new List<Double>();

            //if (No_OF_Heating_Ele > 1)
            //{
            //    System.Diagnostics.Debug.WriteLine("No_OF_Heating_Ele: " + No_OF_Heating_Ele);

            //    System.Diagnostics.Debug.WriteLine("Tables count: " + MSubmodule_Dataset.Tables.Count);

            //    if (MSubmodule_Dataset.Tables.Count > 1)
            //    {
            //        System.Diagnostics.Debug.WriteLine("Table[1] Rows: " + MSubmodule_Dataset.Tables[1].Rows.Count);
            //    }

            //    //for (int i = 0; i <= No_OF_Heating_Ele - 1; i++)

            //    //int rowCount = MSubmodule_Dataset.Tables[1].Rows.Count;

            //    //for (int i = 0; i < rowCount; i++)

            //    //{

            //    //    System.Diagnostics.Debug.WriteLine("Loop i: " + i);

            //    //    if (i >= MSubmodule_Dataset.Tables[1].Rows.Count)
            //    //    {
            //    //        System.Diagnostics.Debug.WriteLine("❌ ERROR: Table[1] row missing at index " + i);
            //    //        break;
            //    //    }


            //    //    Zeta_val.Add(Convert.ToDouble(MSubmodule_Dataset.Tables[1].Rows[i][0].ToString()));
            //    //    Section_val.Add(MSubmodule_Dataset.Tables[1].Rows[i][1].ToString());
            //    //}
            //    //int rowCount = MSubmodule_Dataset.Tables[1].Rows.Count;

            //    int rowCount = MSubmodule_Dataset.Tables[1].Rows.Count;

            //    for (int i = 0; i < rowCount; i++)
            //    {
            //        var row = MSubmodule_Dataset.Tables[1].Rows[i];

            //        double zeta = Convert.ToDouble(row["Zeta_Value"]);
            //        string sectionId = row["SectionID"].ToString();

            //        Zeta_val.Add(zeta);
            //        Section_val.Add(sectionId);
            //    }

            //    for (int i = 0; i <= Section_val.Count - 1; i++)
            //    {
            //        Design_Area = mCal_4B_BLL.Get_Design_Area_From_Section_ID_in_4B(B, P, Section_val.ElementAt(i).ToString());
            //        if (Design_Area.Tables[1].Rows[0][0].ToString() == "")
            //        {
            //            Design_Area_val.Add(0.0);
            //        }
            //        else
            //        {
            //            Design_Area_val.Add(Convert.ToDouble(Design_Area.Tables[1].Rows[0][0].ToString()));
            //        }
            //    }
            //    mCal_4B_SC.Upper_furnace_water_wall_n_roof_area = Upper_furnace_absorbing_area;
            //    mCal_4B_SC.Area_factor_for_water_walls_n_roof = Convert.ToDouble(MSubmodule_Dataset.Tables[2].Rows[0][0].ToString());

            //    mCal_4B_SC.Effective_area_of_upper_furnace = Zeta_val.Sum(xy => Convert.ToDouble(xy)) + Design_Area_val.Sum((xy => Convert.ToDouble(xy))) + mCal_4B_SC.Upper_furnace_water_wall_n_roof_area + mCal_4B_SC.Area_factor_for_water_walls_n_roof;

            //}
            //else
            //{

            //    mCal_4B_SC.Upper_furnace_water_wall_n_roof_area = Upper_furnace_absorbing_area;

            //    mCal_4B_SC.Area_factor_for_water_walls_n_roof = Convert.ToDouble(MSubmodule_Dataset.Tables[0].Rows[0][0].ToString());

            //    mCal_4B_SC.Effective_area_of_upper_furnace = Zeta_val.Sum((xy => Convert.ToDouble(xy))) + Design_Area_val.Sum((xy => Convert.ToDouble(xy))) + mCal_4B_SC.Upper_furnace_water_wall_n_roof_area + mCal_4B_SC.Area_factor_for_water_walls_n_roof;


            //}
            DataSet MSubmodule_Dataset = null;
            DataSet Design_Area = null;

            MSubmodule_Dataset = mCal_4B_BLL.Get_submodule_heating_section_in_upper_furnace(B, P);

            System.Diagnostics.Debug.WriteLine("===== FULL DATASET DEBUG (MSubmodule_Dataset) =====");

            if (MSubmodule_Dataset == null)
            {
                System.Diagnostics.Debug.WriteLine("❌ MSubmodule_Dataset is NULL");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Tables count: " + MSubmodule_Dataset.Tables.Count);

                for (int t = 0; t < MSubmodule_Dataset.Tables.Count; t++)
                {
                    System.Diagnostics.Debug.WriteLine("---- TABLE INDEX: " + t + " ----");

                    DataTable table = MSubmodule_Dataset.Tables[t];

                    // Columns
                    for (int c = 0; c < table.Columns.Count; c++)
                    {
                        System.Diagnostics.Debug.WriteLine("Column[" + c + "] = " + table.Columns[c].ColumnName);
                    }

                    // Rows
                    for (int r1 = 0; r1 < table.Rows.Count; r1++)
                    {
                        string rowData = "";
                        for (int c = 0; c < table.Columns.Count; c++)
                        {
                            rowData += table.Columns[c].ColumnName + "=" + table.Rows[r1][c] + " | ";
                        }
                        System.Diagnostics.Debug.WriteLine("Row[" + r1 + "] => " + rowData);
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine("Before assignment P: " + P);
            P = Project_ID;
            System.Diagnostics.Debug.WriteLine("After assignment P: " + P);

            int No_OF_Heating_Ele = Convert.ToInt16(MSubmodule_Dataset.Tables[0].Rows[0][0].ToString());

            List<double> Zeta_val = new List<double>();
            List<string> Section_val = new List<string>();
            List<double> Design_Area_val = new List<double>();
            List<double> Heat_Section_Absorbed = new List<double>();

          
            try
            {
                System.Diagnostics.Debug.WriteLine("✅ START MAIN BLOCK");

                if (No_OF_Heating_Ele > 1)
                {
                    System.Diagnostics.Debug.WriteLine("No_OF_Heating_Ele: " + No_OF_Heating_Ele);

                    // ✅ DATASET STRUCTURE DEBUG
                    if (MSubmodule_Dataset != null)
                    {
                        System.Diagnostics.Debug.WriteLine("Tables Count: " + MSubmodule_Dataset.Tables.Count);

                        for (int t = 0; t < MSubmodule_Dataset.Tables.Count; t++)
                        {
                            System.Diagnostics.Debug.WriteLine($"Table[{t}] Rows: {MSubmodule_Dataset.Tables[t].Rows.Count}");
                            System.Diagnostics.Debug.WriteLine($"Table[{t}] Columns: {MSubmodule_Dataset.Tables[t].Columns.Count}");
                        }
                    }

                    // ✅ PROCESS ZETA + SECTION
                    int rowCount = MSubmodule_Dataset.Tables[1].Rows.Count;
                    System.Diagnostics.Debug.WriteLine("RowCount Table[1]: " + rowCount);

                    for (int i = 0; i < rowCount; i++)
                    {
                        try
                        {
                            var row = MSubmodule_Dataset.Tables[1].Rows[i];

                            System.Diagnostics.Debug.WriteLine("---- LOOP i = " + i);

                            double zeta = Convert.ToDouble(row["Zeta_Value"]);
                            string sectionId = row["SectionID"].ToString();

                            System.Diagnostics.Debug.WriteLine("Zeta: " + zeta);
                            System.Diagnostics.Debug.WriteLine("SectionID: " + sectionId);

                            Zeta_val.Add(zeta);
                            Section_val.Add(sectionId);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("💥 ERROR in ZETA LOOP: " + ex.ToString());
                            throw;
                        }
                    }

                    // ✅ PROCESS DESIGN AREA
                    for (int i = 0; i < Section_val.Count; i++)
                    {
                        try
                        {
                            System.Diagnostics.Debug.WriteLine("===== DESIGN AREA CALL =====");
                            System.Diagnostics.Debug.WriteLine("SectionID Used: " + Section_val[i]);

                            Design_Area = mCal_4B_BLL.Get_Design_Area_From_Section_ID_in_4B(B, P, Section_val[i]);

                            double totalDesignArea = 0.0;

                            if (Design_Area != null)
                            {
                                System.Diagnostics.Debug.WriteLine("Design_Area Tables: " + Design_Area.Tables.Count);

                                for (int t = 0; t < Design_Area.Tables.Count; t++)
                                {
                                    System.Diagnostics.Debug.WriteLine($"Design_Area Table[{t}] Rows: {Design_Area.Tables[t].Rows.Count}");
                                }
                            }

                            if (Design_Area != null &&
                                Design_Area.Tables.Count > 1 &&
                                Design_Area.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataRow dr in Design_Area.Tables[1].Rows)
                                {
                                    if (dr[0] != null && dr[0].ToString().Trim() != "")
                                    {
                                        double val = Convert.ToDouble(dr[0]);
                                        totalDesignArea += val;

                                        System.Diagnostics.Debug.WriteLine("Adding value: " + val);
                                    }
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("❌ Design_Area invalid or empty");
                            }

                            Design_Area_val.Add(totalDesignArea);
                            System.Diagnostics.Debug.WriteLine("✅ Total Design Area: " + totalDesignArea);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("💥 ERROR IN DESIGN AREA LOOP: " + ex.ToString());
                            throw;
                        }
                    }

                    // ✅ AREA FACTOR
                    double areaFactor = 0.0;

                    try
                    {
                        System.Diagnostics.Debug.WriteLine("===== AREA FACTOR DEBUG =====");

                        if (MSubmodule_Dataset != null &&
                            MSubmodule_Dataset.Tables.Count > 2 &&
                            MSubmodule_Dataset.Tables[2].Rows.Count > 0 &&
                            MSubmodule_Dataset.Tables[2].Columns.Count > 0)
                        {
                            var raw = MSubmodule_Dataset.Tables[2].Rows[0][0];

                            System.Diagnostics.Debug.WriteLine("Area Factor Raw: " + raw);

                            if (raw != null && raw.ToString().Trim() != "")
                            {
                                areaFactor = Convert.ToDouble(raw);
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("❌ AREA FACTOR DATA INVALID");
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("💥 ERROR IN AREA FACTOR: " + ex.ToString());
                        throw;
                    }

                    // ✅ FINAL CALCULATION
                    try
                    {
                        System.Diagnostics.Debug.WriteLine("✅ BEFORE FINAL CALCULATION");

                        mCal_4B_SC.Upper_furnace_water_wall_n_roof_area = Upper_furnace_absorbing_area;
                        mCal_4B_SC.Area_factor_for_water_walls_n_roof = areaFactor;

                        System.Diagnostics.Debug.WriteLine("Zeta Sum: " + Zeta_val.Sum());
                        System.Diagnostics.Debug.WriteLine("Design Area Sum: " + Design_Area_val.Sum());
                        System.Diagnostics.Debug.WriteLine("Wall Area: " + Upper_furnace_absorbing_area);
                        System.Diagnostics.Debug.WriteLine("Area Factor: " + areaFactor);

                            //mCal_4B_SC.Effective_area_of_upper_furnace =
                            //    Zeta_val.Sum()
                            //    + Design_Area_val.Sum()
                            //    + Upper_furnace_absorbing_area
                            //    + areaFactor;

                            //System.Diagnostics.Debug.WriteLine("✅ FINAL CALCULATION DONE");
                            double Q80 = 0.0;

                            //// ✅ Heating section contribution
                            //for (int i = 0; i < Design_Area_val.Count; i++)
                            //{
                            //    Q80 += Zeta_val[i] * Design_Area_val[i];
                            //}

                            // ✅ No second section
                            double Q82 = 0.0;

                            // ✅ Wall area
                            double Q83 = Upper_furnace_absorbing_area;

                            // ✅ Area factor
                            //double Q84 = areaFactor;
                            double Q84 = 1; // Adding the area factor as 1 for testing default should be 1

                            // ✅ FINAL formula (exact Excel)
                            double effectiveArea = Q80 + Q82 + (Q83 * Q84);

                            mCal_4B_SC.Effective_area_of_upper_furnace = effectiveArea;

                            // ✅ DEBUG
                            System.Diagnostics.Debug.WriteLine($"Q80: {Q80}");
                            System.Diagnostics.Debug.WriteLine($"Q82: {Q82}");
                            System.Diagnostics.Debug.WriteLine($"Q83: {Q83}");
                            System.Diagnostics.Debug.WriteLine($"Q84: {Q84}");
                            System.Diagnostics.Debug.WriteLine($"✅ Effective Area: {effectiveArea}");
                        }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("💥 ERROR IN FINAL CALCULATION: " + ex.ToString());
                        throw;
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("===== ELSE BLOCK EXECUTED =====");

                    double areaFactorElse = 0.0;

                    try
                    {
                        if (MSubmodule_Dataset != null &&
                            MSubmodule_Dataset.Tables.Count > 0 &&
                            MSubmodule_Dataset.Tables[0].Rows.Count > 0 &&
                            MSubmodule_Dataset.Tables[0].Columns.Count > 0)
                        {
                            var raw = MSubmodule_Dataset.Tables[0].Rows[0][0];

                            System.Diagnostics.Debug.WriteLine("Else Area Factor Raw: " + raw);

                            if (raw != null && raw.ToString().Trim() != "")
                            {
                                areaFactorElse = Convert.ToDouble(raw);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("💥 ERROR IN ELSE AREA FACTOR: " + ex.ToString());
                        throw;
                    }

                    mCal_4B_SC.Upper_furnace_water_wall_n_roof_area = Upper_furnace_absorbing_area;
                    mCal_4B_SC.Area_factor_for_water_walls_n_roof = areaFactorElse;

                        //mCal_4B_SC.Effective_area_of_upper_furnace =
                        //    Zeta_val.Sum()
                        //    + Design_Area_val.Sum()
                        //    + Upper_furnace_absorbing_area
                        //    + areaFactorElse;

                        double Q80 = 0.0;

                        //// ✅ Heating section contribution
                        for (int i = 0; i < Design_Area_val.Count; i++)
                        {
                            Q80 += Zeta_val[i] * Design_Area_val[i];
                        }

                        // ✅ No second section
                        double Q82 = 0.0;

                        // ✅ Wall area
                        double Q83 = Upper_furnace_absorbing_area;

                        // ✅ Area factor
                        //double Q84 = areaFactor;
                        double Q84 = 1;

                        // ✅ FINAL formula (exact Excel)
                        double effectiveArea = Q80 + Q82 + (Q83 * Q84);

                        mCal_4B_SC.Effective_area_of_upper_furnace = effectiveArea;

                        // ✅ DEBUG
                        System.Diagnostics.Debug.WriteLine($"Q80: {Q80}");
                        System.Diagnostics.Debug.WriteLine($"Q82: {Q82}");
                        System.Diagnostics.Debug.WriteLine($"Q83: {Q83}");
                        System.Diagnostics.Debug.WriteLine($"Q84: {Q84}");
                        System.Diagnostics.Debug.WriteLine($"✅ Effective Area: {effectiveArea}");
                    }

                System.Diagnostics.Debug.WriteLine("✅ END MAIN BLOCK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("💥 FINAL EXCEPTION CAUGHT:");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw; // 🔴 important
            }
                // M 87
                Double SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C = 1189.2;
                Double SB_Mod_for_upper_furnace_calc_Error = 0;
                Double SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnaceTg_k = 0;
                Double SB_Mod_for_Emissivity_calc_for_upper_furnaceFurnace_emisisvity_based_on_zeta = 0;

                int iter = 0;
                int maxIter = 5000;

                do
                {
                    iter++;

                    // M88
                    SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnaceTg_k =
                        SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C + 273;

                    // M90
                    SB_Mod_for_upper_furnace_calc_Downstream_element_inlet_fluegas_temperature_Tpsh_in =
                        SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnaceTg_k /
                        SB_Mod_for_upper_furnace_calc_Tg_Tpsh_in_ratio - 273;

                    // DEBUG
                    System.Diagnostics.Debug.WriteLine("--------------------------------------------------");
                    System.Diagnostics.Debug.WriteLine($"Iteration: {iter}");
                    System.Diagnostics.Debug.WriteLine($"Tg (degC): {SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C}");
                    System.Diagnostics.Debug.WriteLine($"Tg (K): {SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnaceTg_k}");
                    System.Diagnostics.Debug.WriteLine($"Tpsh_in: {SB_Mod_for_upper_furnace_calc_Downstream_element_inlet_fluegas_temperature_Tpsh_in}");

                    // M66
                    Double SB_Mod_for_Emissivity_calc_Pns =
                        Partial_pressure_of_triatomic_gases * Thickness_of_effective_radiation_layer_upper_furnace;

                    // M68
                    Double SB_Mod_for_Emissivity_calc_for_upper_furnaceTemp_K =
                        SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C + 273;

                    // M69
                    Double P1 = ((0.78 + 1.6 * Volume_fraction_of_water_vapor) /
                                Math.Sqrt(10.2 * SB_Mod_for_Emissivity_calc_Pns)) - 0.1;

                    Double P2 = (1 - 0.37 * SB_Mod_for_Emissivity_calc_for_upper_furnaceTemp_K / 1000);

                    Double SB_Mod_for_Emissivity_calc_for_upper_furnaceKg = 10.2 * P1 * P2;

                    // DEBUG
                    System.Diagnostics.Debug.WriteLine($"P1: {P1}");
                    System.Diagnostics.Debug.WriteLine($"P2: {P2}");
                    System.Diagnostics.Debug.WriteLine($"Kg: {SB_Mod_for_Emissivity_calc_for_upper_furnaceKg}");

                    // M70
                    Double b4 =
                        Math.Pow(SB_Mod_for_Emissivity_calc_for_upper_furnaceTemp_K, 2) *
                        Math.Pow(Mean_diameter_of_ash_particle, 2);

                    Double a4 = Math.Pow(b4, 1.0 / 3.0);
                    if (a4 == 0) a4 = 1e-6;

                    Double SB_Mod_for_Emissivity_calc_for_upper_furnaceKfa =
                        (43850 * Gas_density) / a4;

                    // M71
                    Double SB_Mod_for_Emissivity_calc_for_upper_furnaceK =
                        SB_Mod_for_Emissivity_calc_for_upper_furnaceKg * Volume_fraction_of_triatomic_gases +
                        SB_Mod_for_Emissivity_calc_for_upper_furnaceKfa * Dimensionless_concentration_of_fly_ash +
                        Radiation_absorption_coefficient_of_coke_particles *
                        Dimensionless_number_of_coke_particle_realted_to_coal_type *
                        Dimensionless_number_of_coke_particle_realted_to_combustion_mode;
                    mCal_4B_SC.furnace_K = SB_Mod_for_Emissivity_calc_for_upper_furnaceK;

                    // M72
                    Double SB_Mod_for_Emissivity_calc_for_upper_furnaceflame_emissivity_af =
                        1 - Math.Exp(-SB_Mod_for_Emissivity_calc_for_upper_furnaceK *
                                     Furnace_pressure *
                                     Thickness_of_effective_radiation_layer_upper_furnace);
                    mCal_4B_SC.furnace_flame_emissivity_af = SB_Mod_for_Emissivity_calc_for_upper_furnaceflame_emissivity_af;

                    // M75
                    SB_Mod_for_Emissivity_calc_for_upper_furnaceFurnace_emisisvity_based_on_zeta =
                        SB_Mod_for_Emissivity_calc_for_upper_furnaceflame_emissivity_af /
                        (SB_Mod_for_upper_furnace_calc_zeta_ww_upper_furance *
                        (1 - SB_Mod_for_Emissivity_calc_for_upper_furnaceflame_emissivity_af) + SB_Mod_for_Emissivity_calc_for_upper_furnaceflame_emissivity_af);

                    // DEBUG
                    System.Diagnostics.Debug.WriteLine($"K: {SB_Mod_for_Emissivity_calc_for_upper_furnaceK}");
                    System.Diagnostics.Debug.WriteLine($"Flame emissivity: {SB_Mod_for_Emissivity_calc_for_upper_furnaceflame_emissivity_af}");
                    System.Diagnostics.Debug.WriteLine($"Furnace emissivity: {SB_Mod_for_Emissivity_calc_for_upper_furnaceFurnace_emisisvity_based_on_zeta}");

                    // M76
                    Double x = 5.67e-14;
                    Double y = Math.Pow(SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnaceTg_k, 4);
                    Double z = SB_Mod_for_Emissivity_calc_for_upper_furnaceFurnace_emisisvity_based_on_zeta;

                    Double SB_Mod_for_upper_furnace_calc_WW_exit_radiation =
                        x * y * z * mCal_4B_SC.Effective_area_of_upper_furnace;

                    System.Diagnostics.Debug.WriteLine($"Radiation (MW): {SB_Mod_for_upper_furnace_calc_WW_exit_radiation}");

                    // M91
                    Double SB_Mod_for_upper_furnace_calc_Downstream__element_Inlet_flue_gas__enthalpy =
                        mCal_4B_BLL.Get_2A_FEGT_ReqEnthalpy(
                            Boiler_ID,
                            Project_ID,
                            SB_Mod_for_upper_furnace_calc_Downstream_element_inlet_fluegas_temperature_Tpsh_in
                            .ToString(System.Globalization.CultureInfo.InvariantCulture));
                    mCal_4B_SC.calc_Downstream__element_Inlet_flue_gas__enthalpy = SB_Mod_for_upper_furnace_calc_Downstream__element_Inlet_flue_gas__enthalpy;

                    System.Diagnostics.Debug.WriteLine($"Enthalpy: {SB_Mod_for_upper_furnace_calc_Downstream__element_Inlet_flue_gas__enthalpy}");

                    // M92
                    Double SB_Mod_for_upper_furnace_calc_Available_heat =
                        Heat_preservation_coefficient * Design_fuel_consumption *
                        (SB_Mod_for_upper_furnace_calc_Lower_furnace_exit_enthalpy - SB_Mod_for_upper_furnace_calc_Downstream__element_Inlet_flue_gas__enthalpy) / 1000
                        + mCal_4B_SC.Direct_radiation_from_lower_furnace_MW;

                    System.Diagnostics.Debug.WriteLine($"Available Heat (MW): {SB_Mod_for_upper_furnace_calc_Available_heat}");
                    mCal_4B_SC.calc_Available_heat = SB_Mod_for_upper_furnace_calc_Available_heat;

                    // M93
                    SB_Mod_for_upper_furnace_calc_Error =
                        (SB_Mod_for_upper_furnace_calc_WW_exit_radiation - SB_Mod_for_upper_furnace_calc_Available_heat) /
                        Math.Max(Math.Abs(SB_Mod_for_upper_furnace_calc_Available_heat), 1e-6) * 100;

                    System.Diagnostics.Debug.WriteLine($"Error (%): {SB_Mod_for_upper_furnace_calc_Error}");
                    mCal_4B_SC.calc_Error = SB_Mod_for_upper_furnace_calc_Error;
                    SB_Mod_for_upper_furnace_calc_Radiation_flux_to_downstream__heating_element = 5.67 * Math.Pow(10, -11) * SB_Mod_for_Emissivity_calc_for_upper_furnaceFurnace_emisisvity_based_on_zeta *
                       Math.Pow(SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnaceTg_k, 4);


                    //SB_Mod_for_upper_furnace_calc_WW_exit_radiation = SB_Mod_for_upper_furnace_calc_Available_heat;

                    // ✅ Step logic
                    double errorAbs = Math.Abs(SB_Mod_for_upper_furnace_calc_Error);
                    double step;

                    if (errorAbs > 20) step = 20;
                    else if (errorAbs > 10) step = 10;
                    else if (errorAbs > 5) step = 5;
                    else if (errorAbs > 1) step = 1;
                    else step = 0.1;

                    if (SB_Mod_for_upper_furnace_calc_Error > 0)
                        SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C -= step;
                    else
                        SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C += step;

                    // ✅ CRITICAL FIX → restrict to correct solution zone
                    if (SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C < 1000)
                        SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C = 1000;

                    if (SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C > 1400)
                        SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C = 1400;

                    if (iter > maxIter)
                        break;

                } while (Math.Abs(SB_Mod_for_upper_furnace_calc_Error) > 0.1);



                ////M 87
                //Double SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C = 750;
                //Double SB_Mod_for_upper_furnace_calc_Error = 0;
                //Double SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnaceTg_k = 0;
                //Double SB_Mod_for_Emissivity_calc_for_upper_furnaceFurnace_emisisvity_based_on_zeta = 0;



                //do
                //{

                //    //M88

                //    SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnaceTg_k = SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C + 273;

                //    //M 90
                //    SB_Mod_for_upper_furnace_calc_Downstream_element_inlet_fluegas_temperature_Tpsh_in = SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnaceTg_k / SB_Mod_for_upper_furnace_calc_Tg_Tpsh_in_ratio - 273;

                //    //M66
                //    Double SB_Mod_for_Emissivity_calc_Pns = Partial_pressure_of_triatomic_gases * Thickness_of_effective_radiation_layer_upper_furnace;
                //    //M68
                //    Double SB_Mod_for_Emissivity_calc_for_upper_furnaceTemp_K = SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C + 273;

                //    //M69
                //    Double P1 = ((0.78 + 1.6 * Volume_fraction_of_water_vapor) / (Math.Pow((10.2 * SB_Mod_for_Emissivity_calc_Pns), 0.5))) - 0.1;
                //    Double P2 = (1 - 0.37 * SB_Mod_for_Emissivity_calc_for_upper_furnaceTemp_K / 1000);
                //    Double SB_Mod_for_Emissivity_calc_for_upper_furnaceKg = 10.2 * P1 * P2;


                //    //  SB_Mod_for_Emissivity_calc_for_upper_furnaceKg = 10.2 * b3 * c3;

                //    //SB_Mod_for_Emissivity_calc_for_upper_furnaceKg = Math.Round(SB_Mod_for_Emissivity_calc_for_upper_furnaceKg, 3);
                //    SB_Mod_for_Emissivity_calc_for_upper_furnaceKg = (SB_Mod_for_Emissivity_calc_for_upper_furnaceKg);



                //    Double Average__gas_temp_in_lower_furnace_POWER1 = Math.Pow(SB_Mod_for_Emissivity_calc_for_upper_furnaceTemp_K, 2);

                //    Double Mean_diameter_of_ash_particle_POWER1 = Math.Pow(Mean_diameter_of_ash_particle, 2);
                //    Double b4 = Average__gas_temp_in_lower_furnace_POWER1 * Mean_diameter_of_ash_particle_POWER1;

                //    Double a4 = Math.Pow(b4, (0.333333333));
                //    //M70
                //    Double SB_Mod_for_Emissivity_calc_for_upper_furnaceKfa = (43850 * Gas_density) / a4;

                //    //M71
                //    Double SB_Mod_for_Emissivity_calc_for_upper_furnaceK = SB_Mod_for_Emissivity_calc_for_upper_furnaceKg * Volume_fraction_of_triatomic_gases + SB_Mod_for_Emissivity_calc_for_upper_furnaceKfa
                //        * Dimensionless_concentration_of_fly_ash + Radiation_absorption_coefficient_of_coke_particles * Dimensionless_number_of_coke_particle_realted_to_coal_type * Dimensionless_number_of_coke_particle_realted_to_combustion_mode;
                //    mCal_4B_SC.furnace_K = SB_Mod_for_Emissivity_calc_for_upper_furnaceK;
                //    // M72
                //    Double SB_Mod_for_Emissivity_calc_for_upper_furnaceflame_emissivity_af = 1 - Math.Exp(-SB_Mod_for_Emissivity_calc_for_upper_furnaceK * Furnace_pressure * Thickness_of_effective_radiation_layer_upper_furnace);
                //    mCal_4B_SC.furnace_flame_emissivity_af = SB_Mod_for_Emissivity_calc_for_upper_furnaceflame_emissivity_af;
                //    //M 74  
                //    Double SB_Mod_for_Emissivity_calc_for_upper_furnaceZeta_based_on_Tg = SB_Mod_for_upper_furnace_calc_zeta_ww_upper_furance;
                //    mCal_4B_SC.furnace_Zeta_based_on_Tg = SB_Mod_for_Emissivity_calc_for_upper_furnaceZeta_based_on_Tg;
                //    //M 75  
                //    SB_Mod_for_Emissivity_calc_for_upper_furnaceFurnace_emisisvity_based_on_zeta = SB_Mod_for_Emissivity_calc_for_upper_furnaceflame_emissivity_af / (SB_Mod_for_Emissivity_calc_for_upper_furnaceZeta_based_on_Tg *
                //          (1 - SB_Mod_for_Emissivity_calc_for_upper_furnaceflame_emissivity_af) + SB_Mod_for_Emissivity_calc_for_upper_furnaceflame_emissivity_af);

                //    mCal_4B_SC.furnace_Furnace_emisisvity_based_on_zeta = SB_Mod_for_Emissivity_calc_for_upper_furnaceFurnace_emisisvity_based_on_zeta;




                //    Double x = (5.67 * Math.Pow(10, -14));
                //    // Double x = 5.67e-8;
                //    Double y = Math.Pow(SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnaceTg_k, 4);
                //    Double z = SB_Mod_for_Emissivity_calc_for_upper_furnaceFurnace_emisisvity_based_on_zeta;
                //    //Double k = (Upper_furnace_exit_window_area + Upper_furnace_absorbing_area * SB_Mod_for_upper_furnace_calc_zeta_ww_upper_furance);
                //    //M 76  
                //    Double SB_Mod_for_upper_furnace_calc_WW_exit_radiation = x * y * z * mCal_4B_SC.Effective_area_of_upper_furnace;


                //    //    *SB_Mod_for_Emissivity_calc_for_upper_furnaceFurnace_emisisvity_based_on_zeta*();
                //    //M 91

                //    Double SB_Mod_for_upper_furnace_calc_Downstream__element_Inlet_flue_gas__enthalpy = mCal_4B_BLL.Get_2A_FEGT_ReqEnthalpy(Boiler_ID, Project_ID,
                //        SB_Mod_for_upper_furnace_calc_Downstream_element_inlet_fluegas_temperature_Tpsh_in.ToString());
                //    mCal_4B_SC.calc_Downstream__element_Inlet_flue_gas__enthalpy = SB_Mod_for_upper_furnace_calc_Downstream__element_Inlet_flue_gas__enthalpy;

                //    //M 92
                //    Double SB_Mod_for_upper_furnace_calc_Available_heat = Heat_preservation_coefficient * Design_fuel_consumption * (SB_Mod_for_upper_furnace_calc_Lower_furnace_exit_enthalpy -
                //              SB_Mod_for_upper_furnace_calc_Downstream__element_Inlet_flue_gas__enthalpy) / 1000 + mCal_4B_SC.Direct_radiation_from_lower_furnace_MW;

                //    mCal_4B_SC.calc_Available_heat = SB_Mod_for_upper_furnace_calc_Available_heat;
                //    //M 93
                //    //SB_Mod_for_upper_furnace_calc_Error = ((SB_Mod_for_upper_furnace_calc_Available_heat - SB_Mod_for_upper_furnace_calc_WW_exit_radiation) / SB_Mod_for_upper_furnace_calc_Available_heat) * 100;
                //    SB_Mod_for_upper_furnace_calc_Error = ((SB_Mod_for_upper_furnace_calc_WW_exit_radiation - SB_Mod_for_upper_furnace_calc_Available_heat) / SB_Mod_for_upper_furnace_calc_Available_heat) * 100;
                //    mCal_4B_SC.calc_Error = SB_Mod_for_upper_furnace_calc_Error;
                //    //M 94
                //    SB_Mod_for_upper_furnace_calc_Radiation_flux_to_downstream__heating_element = 5.67 * Math.Pow(10, -11) * SB_Mod_for_Emissivity_calc_for_upper_furnaceFurnace_emisisvity_based_on_zeta *
                //       Math.Pow(SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnaceTg_k, 4);


                //    //SB_Mod_for_upper_furnace_calc_WW_exit_radiation = SB_Mod_for_upper_furnace_calc_Available_heat;

                //    // mCal_4B_SC.calc_zeta_ww_upper_furance = SB_Mod_for_upper_furnace_calc_WW_exit_radiation;
                //    // SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C = SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C;
                //    //SB_Mod_for_upper_furnace_calc_Downstream_element_inlet_fluegas_temperature_Tpsh_in = SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C;
                //    //-------------------------------------------------------------------------------------------

                //    //SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C = SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C + 0.1;



                //    if (SB_Mod_for_upper_furnace_calc_Error > 0)
                //        SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C += 0.1;
                //    else
                //        SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C -= 0.1;

                //} while (Math.Abs(SB_Mod_for_upper_furnace_calc_Error) > 0.1);

                for (int i = 0; i < 2; i++)
                    {
                        Heat_Section_Absorbed.Add(0);
                    }

                    for (int i = 0; i <= No_OF_Heating_Ele - 1; i++)
                    {
                        for (int c = 0; c <= Zeta_val.Count - 1; c++)
                        {
                            Double Q80 = Convert.ToDouble(Design_Area_val.ElementAt(c).ToString());
                            Double Q79 = Convert.ToDouble(Zeta_val.ElementAt(c).ToString());
                            //=(5.67*10^-14)*(M88^4)*Q80*Q79
                            Double ab = (5.67 * Math.Pow(10, -14));
                            Double ac = Math.Pow(SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnaceTg_k, 4);
                            Double ad = Q80 * Q79;
                            //Double k = (Upper_furnace_exit_window_area + Upper_furnace_absorbing_area * SB_Mod_for_upper_furnace_calc_zeta_ww_upper_furance);
                            Double Heat_Absorbed = Math.Round(ab * ac * ad, 3);
                            Heat_Section_Absorbed.Add(Heat_Absorbed);
                        }
                    }


                    //  mCal_4B_SC.calc_Radiation_flux_to_downstream__heating_element = SB_Mod_for_upper_furnace_calc_Radiation_flux_to_downstream__heating_element;
                    //M98
                    Double SB_Mod_for_Upper_furnace_flux_calculation_Nose_inlet_Temp = Furnace_outlet_gas_temperature;
                    mCal_4B_SC.calculation_Nose_inlet_Temp = SB_Mod_for_Upper_furnace_flux_calculation_Nose_inlet_Temp;
                    //M100
                    Double SB_Mod_for_Upper_furnace_flux_calculation_water_wall_Temp = SB_Mod_for_upper_furnace_calc_Average_temperature_in_upper_furnace_Tg_deg_C;
                    mCal_4B_SC.calculation_water_wall_Temp = SB_Mod_for_Upper_furnace_flux_calculation_water_wall_Temp;
                    //M101
                    Double SB_Mod_for_Upper_furnace_flux_calculation_Roof_geometric_mean_Temp = Math.Pow(SB_Mod_for_Upper_furnace_flux_calculation_water_wall_Temp, 2) / SB_Mod_for_Upper_furnace_flux_calculation_Nose_inlet_Temp;
                    mCal_4B_SC.calculation_Roof_geometric_mean_Temp = SB_Mod_for_Upper_furnace_flux_calculation_Roof_geometric_mean_Temp;
                    //N98
                    Double p = 5.67 * Math.Pow(10, -11) * SB_Mod_for_upper_furnace_calc_zeta_ww_upper_furance * SB_Mod_for_Emissivity_calc_for_upper_furnaceFurnace_emisisvity_based_on_zeta;
                    Double q = Math.Pow((SB_Mod_for_Upper_furnace_flux_calculation_Nose_inlet_Temp + 273), 4);
                    Double SB_Mod_for_Upper_furnace_flux_calculation_Nose_inlet_Heat = p * q;
                    mCal_4B_SC.calculation_Nose_inlet_Heat_Flux = SB_Mod_for_Upper_furnace_flux_calculation_Nose_inlet_Heat;

                    Double r = Math.Pow((SB_Mod_for_Upper_furnace_flux_calculation_water_wall_Temp + 273), 4);
                    //N100
                    Double SB_Mod_for_Upper_furnace_flux_calculation_water_wall_Heat = p * r;

                    mCal_4B_SC.calculation_water_wall_Heat_Flux = SB_Mod_for_Upper_furnace_flux_calculation_water_wall_Heat;
                    //N101
                    Double s = Math.Pow((SB_Mod_for_Upper_furnace_flux_calculation_Roof_geometric_mean_Temp + 273), 4);

                    Double SB_Mod_for_Upper_furnace_flux_calculation_Roof_geometric_mean_Heat = p * s;

                    mCal_4B_SC.calculation_Roof_geometric_mean_Heat_Flux = SB_Mod_for_Upper_furnace_flux_calculation_Roof_geometric_mean_Heat;
                    //M104
                    mCal_4B_SC.Nose_Heat_Flux = SB_Mod_for_Upper_furnace_flux_calculation_Nose_inlet_Heat;
                    //N104
                    mCal_4B_SC.Nose_Heat_Flux_Ratio = mCal_4B_SC.Nose_Heat_Flux / mCal_4B_SC.calculation_water_wall_Heat_Flux;
                    //need to take from 5A modules
                    //  mCal_4B_SC.calculation_avg_location_vertical_direction_Heat_Flux = mCal_4B_SC.calculation_water_wall_Heat_Flux*

                    mCal_4B_SC.calculation_Roof_Heat_Flux = mCal_4B_SC.calculation_Roof_geometric_mean_Heat_Flux;
                    mCal_4B_SC.calculation_Roof_Heat_Flux_ratio = mCal_4B_SC.calculation_Roof_Heat_Flux / SB_Mod_for_Upper_furnace_flux_calculation_water_wall_Heat;

                    Last_Element_UF = mCal_4B_BLL.GetLast_Element_of_HeatingSectionUpperFurnace(Boiler_ID, Project_ID);

                    Double avg_location_vertical_direction_HF = mCal_4B_BLL.Get_avg_loc_vert_dir_4B_Cal(Boiler_ID, Project_ID, Last_Element_UF, SB_Mod_for_Upper_furnace_flux_calculation_water_wall_Heat, SB_Mod_for_Upper_furnace_flux_calculation_Nose_inlet_Heat);

                    Double avg_location_vertical_direction_HF_Ratio = avg_location_vertical_direction_HF / SB_Mod_for_Upper_furnace_flux_calculation_water_wall_Heat;



                    DataTable dt1 = new DataTable();
                    dt1 = mCal_4B_BLL.Get_PID_For_4B_Calculation();

                    foreach (DataRow row in dt1.Rows)
                    {
                        int pid = Convert.ToInt16(row["PID"].ToString());
                        //int cgid = Convert.ToInt16(row["CalculationGrpID"].ToString());
                        //double Specific_humidity_of_ambient_air = Specific_humidity_of_ambient_airV;

                        //string BoilerID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
                        //string ProjectID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
                        //string BoilerLoadID = "100";
                        //int ObjectiveID = 1;


                        switch (pid)
                        {
                            case 1:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, Theoretical_combustion_temparature_C.ToString());
                                break;

                            case 2:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, Furnace_outlet_gas_temperature.ToString());
                                break;
                            case 3:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, SB_Mod_for_upper_furnace_calc_Lower_furnace_exit_temp.ToString());
                                break;
                            case 4:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, SB_Mod_for_upper_furnace_calc_Downstream_element_inlet_fluegas_temperature_Tpsh_in.ToString());
                                break;
                            case 5:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, SB_Mod_for_upper_furnace_calc_Radiation_flux_to_downstream__heating_element.ToString());
                                break;
                            case 6:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, SB_Mod_for_Upper_furnace_flux_calculation_Nose_inlet_Heat.ToString());
                                break;
                            case 7:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, SB_Mod_for_Upper_furnace_flux_calculation_water_wall_Heat.ToString());
                                break;
                            case 8:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, SB_Mod_for_Upper_furnace_flux_calculation_Roof_geometric_mean_Heat.ToString());
                                break;
                            case 9:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, mCal_4B_SC.Nose_Heat_Flux_Ratio.ToString());
                                break;
                            case 10:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, avg_location_vertical_direction_HF_Ratio.ToString());
                                break;
                            case 11:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, mCal_4B_SC.calculation_Roof_Heat_Flux_ratio.ToString());
                                break;
                            case 12:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, mCal_4B_SC.Partial_pressure_of_triatomic_gases.ToString());
                                break;
                            case 13:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, Heat_Section_Absorbed.ElementAt(0).ToString());
                                break;
                            case 14:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, Heat_Section_Absorbed.ElementAt(1).ToString());
                                break;
                            case 15:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, mCal_4B_SC.calc_Downstream__element_Inlet_flue_gas__enthalpy.ToString());
                                break;
                            case 16:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, mCal_4B_SC.Heat_input_by_1_kg_fuel.ToString());
                                break;
                            case 17:
                                mCal_4B_BLL.Insert_4B_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, ObjectiveID, mCal_4B_SC.Theoretical_combustion_temparature_K.ToString());
                                break;



                        }
                    }
            }

            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("💥 EXCEPTION AT CALLER:");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw;

            }

        }
            }
        }
