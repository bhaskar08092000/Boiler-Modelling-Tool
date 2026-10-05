using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;
using BoilerModellingTool.BLL;
using System.Data;

namespace GenericClasses
{
    public class Class_13B_Calculation
    {
        Cal_13B_SC Doc { get; set; }

        Stream_Macros mStream_Macros;

        public void Calculation_for_13B(string Project_ID, string Boiler_ID, string Boiler_Load, int Objective_ID, string SectionID_F_Parameter, string SectionID_S_Parameter, int SteamCooledWall, int SteamScreenPresent, string SectionID_SteamCooledID, string SectionID_SteamScreenID, int Iteration, string SectionID_FirstSuperheater,string SteamScreenID)
        {
            DataTable dt = new DataTable();

            Cal_13B_BLL mCal_13B_BLL = new Cal_13B_BLL();

            mStream_Macros = new Stream_Macros();

            Doc = mCal_13B_BLL.Get_13B_CalculationInput(Project_ID, Boiler_ID, Boiler_Load, Objective_ID, SectionID_F_Parameter, SectionID_S_Parameter, SteamCooledWall, SteamScreenPresent, SectionID_SteamCooledID, SectionID_SteamScreenID, Iteration);

            //-----------------Calculation---------------------

            //F56
            Doc.Side_wall_absorbed_heat = 85.2;

            //F57
            Doc.Rear_wall_absorbed_heat = 72.3;

            //F58
            Doc.Roof_absorbed_heat = 63.1;

            //F59
            Doc.Economiser_hanger_absorbed = 125.5;


            //Doc.Reverse_chamber_outlet_pressure = 166.3;
            
            //F53
            Doc.Inlet_flue_gas_temperature = Doc.Temperature_of_flue_gas_into_panel;
            //Doc.Inlet_flue_gas_temperature = Doc.Temperature_of_flue_gas_into_panel_input

            //F54
            Doc.Inlet_flue_gas_enthalpy = Doc.Enthalpy_of_flue_gas_into_panel;

            //F55
            //Doc.convection_heat = 357.3;

            
            do{

                do
                {

                    do
                    {

                        do
                        {
                            //Doc.Reverse_chamber_outlet_pressure = 166.3;

                            //F56+F57+F58
                            Doc.Total_absorbed_heat_by_reversing_chamber = Doc.Side_wall_absorbed_heat+Doc.Roof_absorbed_heat+Doc.Rear_wall_absorbed_heat;

                            //F54-(F60+F59)/D21
                            Doc.Outlet_flue_gas_enthalpy = Doc.Inlet_flue_gas_enthalpy -(Doc.Economiser_hanger_absorbed+Doc.Total_absorbed_heat_by_reversing_chamber) / Doc.Heat_preservation_coefficient;

                            //F62   Reversing Chamber Exit Temp. 1
                            Doc.Outlet_flue_gas_temperature = mCal_13B_BLL.Get_Temperauter_From_Enthalpy(Project_ID,Boiler_ID,Doc.Outlet_flue_gas_enthalpy,Boiler_Load,Objective_ID);

                            //F63
                            Doc.Average_flue_gas_temperature = 0.5 * (Doc.Inlet_flue_gas_temperature + Doc.Outlet_flue_gas_temperature);

                            //F64
                            Doc.Inlet_steam_temperature = Doc.Inlet_temperature_side_wall;

                            //F65 h_pt((D36)*0.980665+1.01325,D35)
                            double a1 = Doc.Inlet_steam_pressure * 0.980665+1.01325;
                            Doc.Inlet_steam_enthalpy_1 = mStream_Macros.h_pT(a1,Doc.Inlet_temperature_side_wall);

                            //F66 3600*F56*D31*D32/(D38)
                            Doc.Steam_enthalpy_increment = 3600 * Doc.Side_wall_absorbed_heat * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element / (Doc.Reverse_chamber_flow_rate);

                            //F67 ((Reverse chamber  outlet pressure)*0.980665+1.01325)/10
                            Doc.Reversing_chamber_outlet_pressure_1 = ((Doc.Reverse_chamber_outlet_pressure) * 0.980665 + 1.01325) / 10;

                            //F68 Steam enthalpy increment+Steam enthalpy increment
                            Doc.Reversing_chamber_outlet_steam_enthalpy = Doc.Inlet_steam_enthalpy_1 + Doc.Steam_enthalpy_increment;

                            //F69
                            double p1;
                            p1 = Doc.Reversing_chamber_outlet_pressure_1 * 10;
                            Doc.Reversing_chamber_outlet_steam_temperature = mStream_Macros.T_ph(p1, Doc.Reversing_chamber_outlet_steam_enthalpy);

                            //F70 0.5*(Inlet steam temperature+Reversing chamber outlet steam temperature)
                            Doc.Average_steam_temperature = 0.5 * (Doc.Inlet_steam_temperature + Doc.Reversing_chamber_outlet_steam_temperature);

                            //F71
                            Doc.Ash_deposition_coefficient = Doc.Ash_deposit_coefficient;

                            //F72 Average steam temperature+1000*Ash deposition coefficient*Design fuel consumption*Flue gas flow fraction through the element*Side wall absorbed heat/Side wall area
                            Doc.Fouling_layer_temperature_of_tube_wall = Doc.Average_steam_temperature + 1000 * Doc.Ash_deposition_coefficient * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element * Doc.Side_wall_absorbed_heat / Doc.Side_wall_area;

                            //F73 Partial pressure of triatomic gases*Effective radiation layer thickness
                            Doc.Product_of_pn_and_s = Doc.Partial_pressure_of_triatomic_gases * Doc.Effective_radiation_layer_thickness;

                            //F74 10.2*((0.78+1.6*Volume fraction of water vapor)/(10.2*Product of pn and s)^0.5-0.1)*(1-0.37*(Average flue gas temperature+273)/1000)
                            Doc.Radiant_absorption_coefficient_of_gas = 10.2 * ((0.78 + 1.6 * Doc.Volume_fraction_of_water_vapor) / Math.Pow(10.2 * Doc.Product_of_pn_and_s, 0.4) * (0.63 * (Doc.Average_flue_gas_temperature + 273) / 1000));

                            //F75  43850*'1A'!$I$72/((F63+273)^2*('4B'!D44)^2)^(1/3) PIE36 and 1A Flue Gas Density
                            Doc.Radiant_absorption_coefficient_of_fly_ash = Math.Round((43850 * Doc.Flue_gas_density / (Math.Pow(Math.Pow(Doc.Average_flue_gas_temperature + 273, 2) * Math.Pow(Doc.Mean_diameter_of_ash_particle, 2), 0.333333333))), 2, MidpointRounding.AwayFromZero);
                            //Doc.Radiant_absorption_coefficient_of_fly_ash = 43850 * (Doc.Flue_gas_density) / (Math.Pow(Math.Pow(Doc.Average_flue_gas_temperature + 273, 2) * Math.Pow(Doc.Mean_diameter_of_ash_particle, 2), 1/3));

                            //F76 Radiant absorption coefficient of gas*Volume fraction of triatomic gases+Radiant absorption coefficient of fly ash*Dimensionless concentration of fly ash
                            Doc.Radiant_absorption_coefficient_of_flue_gas_radiation = Doc.Radiant_absorption_coefficient_of_gas * Doc.Volume_fraction_of_triatomic_gases + Doc.Radiant_absorption_coefficient_of_fly_ash * Doc.Dimensionless_concentration_of_fly_ash;

                            //F77   Radiant absorption coefficient of flue gas radiation*Furnace pressure*Effective radiation layer thickness
                            Doc.Exponent_of_Eq_2_73 = Doc.Radiant_absorption_coefficient_of_flue_gas_radiation * Doc.Furnace_pressure * Doc.Effective_radiation_layer_thickness;

                            //F78   1-EXP(-Exponent of Eq 2.73)
                            Doc.Flue_gas_emissivity = 1 - Math.Exp(-Doc.Exponent_of_Eq_2_73);

                            //F79   5.7*10^-8*(Tube wall fouling emmisivity+1)/2*Flue gas emissivity*(Average flue gas temperature+273)^3*(1-((Fouling layer temperature of tube wall+273)/(Average flue gas temperature+273))^4)/(1-((Fouling layer temperature of tube wall+273)/(Average flue gas temperature+273)))
                            /*double p2 = 5.7 * Math.Pow(10, -8);
                            double p3 = (Doc.Tube_wall_fouling_emmisivity + 1) / 2;
                            double p4= Math.Pow((Doc.Average_flue_gas_temperature+273),3);
                            double p5 = (1 - Math.Pow(((Doc.Fouling_layer_temperature_of_tube_wall + 273) / (Doc.Average_flue_gas_temperature + 273)), 4));
                            double p6 = (1 - ((Doc.Fouling_layer_temperature_of_tube_wall + 273) / (Doc.Average_flue_gas_temperature + 273)));
                            Doc.Radiation_heat_transfer_coefficient = p2*p3*Doc.Flue_gas_emissivity*p4*p5/p6;
                            */
                            Double Con = ((Doc.Fouling_layer_temperature_of_tube_wall + 273) / (Doc.Average_flue_gas_temperature + 273));
                            Double val1 = 5.7 * Math.Pow(10, -8) * (Doc.Tube_wall_fouling_emmisivity + 1) / 2 * Doc.Flue_gas_emissivity * Math.Pow(Doc.Average_flue_gas_temperature + 273, 3);
                            Double val2 = 1 - Math.Pow(Con, 4);
                            Double val3 = 1 - Con;
                            Doc.Radiation_heat_transfer_coefficient = val1 * val2 / val3;

                            //D80   Average flue gas temperature-Fouling layer temperature of tube wall
                            Doc.Average_temperature_difference = Doc.Average_flue_gas_temperature - Doc.Fouling_layer_temperature_of_tube_wall;

                            //F81   Radiation heat transfer coefficient*Average temperature difference*Roof area/(1000*Design fuel consumption*Flue gas flow fraction through the element)
                            Doc.Absorbed_heat_of_roof_superheater = Doc.Radiation_heat_transfer_coefficient * Doc.Average_temperature_difference * Doc.Roof_area / (1000 * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element);

                            //F82   (Roof absorbed heat-Absorbed heat of roof superheater)/Roof absorbed heat*100
                            Doc.Error1 = (Doc.Roof_absorbed_heat - Doc.Absorbed_heat_of_roof_superheater) / Doc.Roof_absorbed_heat * 100;

                            Doc.Roof_absorbed_heat = Doc.Absorbed_heat_of_roof_superheater;

                        } /*while ((Math.Abs(Doc.Error1) >= 0.01));  */    while ((Math.Abs(Doc.Error1) >= 0.01) || Doc.Error1 < 0.0);

                        //F83   Radiation heat transfer coefficient*Average temperature difference*Rear wall area/(1000*Design fuel consumption*Flue gas flow fraction through the element)
                        Doc.Absorbed_heat_of_rear_wall_superheater = Doc.Radiation_heat_transfer_coefficient * Doc.Average_temperature_difference * Doc.Rear_wall_area / (1000 * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element);

                        //F84   (Rear wall absorbed heat-Absorbed heat of rear wall superheater)/Rear wall absorbed heat*100
                        Doc.Error2 = (Doc.Rear_wall_absorbed_heat - Doc.Absorbed_heat_of_rear_wall_superheater) / Doc.Rear_wall_absorbed_heat * 100;

                        Doc.Rear_wall_absorbed_heat = Doc.Absorbed_heat_of_rear_wall_superheater;

                    } /*while ((Math.Abs(Doc.Error2) >= 0.01)); */   while ((Math.Abs(Doc.Error2) >= 0.01) || Doc.Error2 < 0.0);


                    //F85   Radiation heat transfer coefficient*Average temperature difference*Side wall area/(1000*Design fuel consumption*Flue gas flow fraction through the element)
                    Doc.Absorbed_heat_of_side_wall_superheater = Doc.Radiation_heat_transfer_coefficient * Doc.Average_temperature_difference * Doc.Side_wall_area / (1000 * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element);

                    //F86   (Side wall absorbed heat-Absorbed heat of side wall superheater)/Side wall absorbed heat*100
                    Doc.Error3 = (Doc.Side_wall_absorbed_heat - Doc.Absorbed_heat_of_side_wall_superheater) / Doc.Side_wall_absorbed_heat * 100;

                    Doc.Side_wall_absorbed_heat = Doc.Absorbed_heat_of_side_wall_superheater;

                } while ((Math.Abs(Doc.Error3) >= 0.01) || Doc.Error3 < 0.0);  //while ((Math.Abs(Doc.Error3) >= 0.01) || Doc.Error3 < 0.0);



            //F87   Economiser hanger inlet temp
            Doc.Economiser_hanger_inlet_temp_1=Doc.Economiser_hanger_inlet_temp;

            //F88   Economiser hanger inlet enthalpy h_pt((D42)*0.980665+1.01325,D41)
            //Doc.Economiser_inlet_enthalpy=Doc.Economiser_hanger_inlet_enthalpy;
            Doc.Economiser_inlet_enthalpy = mStream_Macros.h_pT(Doc.Economiser_hanger_inlet_pressure * 0.980665 + 1.01325, Doc.Economiser_hanger_inlet_temp);

            //F89   Economiser inlet enthalpy+3600*Design fuel consumption*Flue gas flow fraction through the element*Economiser hanger absorbed/(Main steam flow rate-De-superheating spray (stage-1)-De-superheating spray (stage-2, if any))
            Doc.Economiser_outlet_enthalpy = Doc.Economiser_inlet_enthalpy + 3600 * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element * Doc.Economiser_hanger_absorbed / Doc.Economiser_hanger_flow_rate;

            //F90   ((Economiser hanger  outlet pressure)*0.980665+1.01325)/10
            Doc.Economiser_outlet_pressure=((Doc.Economiser_hanger_outlet_pressure)*0.980665+1.01325)/10;

            //F91   Economiser outlet temp
            double d = Doc.Economiser_outlet_pressure * 10;
            Doc.Economiser_outlet_temp=mStream_Macros.T_ph(d,Doc.Economiser_outlet_enthalpy);

            //F92   (F91+F87)/2
            Doc.Average_water_temp_of_hanger_tube=(Doc.Economiser_outlet_temp+Doc.Economiser_hanger_inlet_temp_1)/2;

            //F93   F63-(F92+1000*F71*D31*D32*(F59)/D14)
            Doc.Temp_difference=Doc.Average_flue_gas_temperature-(Doc.Average_water_temp_of_hanger_tube+1000*Doc.Ash_deposit_coefficient*Doc.Design_fuel_consumption*Doc.Flue_gas_flow_fraction_through_the_element*(Doc.Economiser_hanger_absorbed)/Doc.Eco_hanger_circumf_area);

            //F94   Radiation heat transfer coefficient*Temp difference*Eco. hanger heating area in zone/(1000*Design fuel consumption*Flue gas flow fraction through the element)
            Doc.Absorbed_convection_heat_of_hanger_tube=Doc.Radiation_heat_transfer_coefficient*Doc.Temp_difference*Doc.Eco_hanger_heating_area_in_zone/(1000*Doc.Design_fuel_consumption*Doc.Flue_gas_flow_fraction_through_the_element);

            //F95   Economiser hanger absorbed-Absorbed convection heat of hanger tube
            Doc.Error4=Doc.Economiser_hanger_absorbed-Doc.Absorbed_convection_heat_of_hanger_tube;

            Doc.Economiser_hanger_absorbed = Doc.Absorbed_convection_heat_of_hanger_tube;

            } while ((Math.Abs(Doc.Error4) >= 0.01) || Doc.Error4 < 0.0);



            //-------------------------Backpass Flow Ratio Calculation---------------------------

            Doc.Fraction_of_total_flow_in_Sidewall_front = Math.Round((Math.Pow(Doc.Sidewall_front_portion_tube_diameter / 1000, 2) * Doc.Sidewall_front_portion_tube_numbers), 2);

             Doc.Fraction_of_total_flow_in_Sidewall_rear = Math.Round((Math.Pow(Doc.Sidewall_rear_portion_tube_diameter / 1000, 2) * Doc.Sidewall_rear_portion_tube_numbers), 2);

            Doc.Fraction_of_side_front_wall_flow_in_backpass_front_wall = Math.Round((Math.Pow(Doc.Front_wall_tube_diameter / 1000, 2) * Doc.Front_wall_tube_numbers), 2);

            Doc.Fraction_of_side_front_wall_flow_in_extended_side_wall = Math.Round((Math.Pow(Doc.Extended_steam_wall_header_tube_diameter / 1000, 2) * Doc.Extended_steam_wall_header_tube_numbers), 2);

            Doc.Ratio_of_total_flow_in_Sidewall_front = Math.Round((Doc.Fraction_of_total_flow_in_Sidewall_front / (Doc.Fraction_of_total_flow_in_Sidewall_front + Doc.Fraction_of_total_flow_in_Sidewall_rear)), 2);

            Doc.Ratio_of_total_flow_in_Sidewall_rear = Math.Round((Doc.Fraction_of_total_flow_in_Sidewall_rear / (Doc.Fraction_of_total_flow_in_Sidewall_front + Doc.Fraction_of_total_flow_in_Sidewall_rear)), 2);

            Doc.Ratio_of_side_front_wall_flow_in_backpass_front_wall = Math.Round((Doc.Fraction_of_side_front_wall_flow_in_backpass_front_wall / (Doc.Fraction_of_side_front_wall_flow_in_backpass_front_wall + Doc.Fraction_of_side_front_wall_flow_in_extended_side_wall)), 2);

            Doc.Ratio_of_side_front_wall_flow_in_extended_side_wall = Math.Round((Doc.Fraction_of_side_front_wall_flow_in_extended_side_wall / (Doc.Fraction_of_side_front_wall_flow_in_backpass_front_wall + Doc.Fraction_of_side_front_wall_flow_in_extended_side_wall)), 2);


            //--------------------------Fixed Parameters Calculation-----------------------------

            //(F58+F56+F57)*D32*D31/1000
            Doc.Heat_absorption_in_heating_section = (Doc.Side_wall_absorbed_heat + Doc.Roof_absorbed_heat + Doc.Rear_wall_absorbed_heat) * Doc.Flue_gas_flow_fraction_through_the_element * Doc.Design_fuel_consumption / 1000;

            Doc.Roof = 0;

            Doc.Water_wall = 0;

            //F59*D32*D31/1000
            Doc.Economizer_hanger = Doc.Economiser_hanger_absorbed * Doc.Flue_gas_flow_fraction_through_the_element * Doc.Design_fuel_consumption / 1000;


            //--------------------------Calculations for first SH inlet steam temperature---------

            //h_pt((D37-0.1)*0.980665+1.01325,P63)
            double a0 = (Doc.Reverse_chamber_outlet_pressure - 0.1)*0.980665+1.01325;
            Doc.steam_Enthalpy_at_extended_side_wall = mStream_Macros.h_pT(a0, Doc.Steam_temp_at_extended_side_wall);

            //h_pt((D37-0.1)*0.980665+1.01325,P64)
            double p= mStream_Macros.h_pT(a0, Doc.Steam_screen_exit_steam_temp);
            Doc.Backpass_screen_exit_steam_enthalpy = p;

            //K63*L57+K64*L56
            if (SteamCooledWall == 2)
            {
                Doc.Enthalpy_at_reverse_chamber_roof_inlet = Doc.Reversing_chamber_outlet_steam_enthalpy;
            }
            else
            {
                Doc.Enthalpy_at_reverse_chamber_roof_inlet = Doc.steam_Enthalpy_at_extended_side_wall * Doc.Ratio_of_side_front_wall_flow_in_extended_side_wall + Doc.Backpass_screen_exit_steam_enthalpy * Doc.Ratio_of_side_front_wall_flow_in_backpass_front_wall;
            }

            //K65+((F58+F57)*D31/((D38/3600)*L53))
            Doc.Enthalpy_at_reverse_chamber_roof_outlet = Doc.Enthalpy_at_reverse_chamber_roof_inlet+((Doc.Roof_absorbed_heat+Doc.Rear_wall_absorbed_heat)*Doc.Design_fuel_consumption/((Doc.Reverse_chamber_flow_rate/3600)*Doc.Ratio_of_total_flow_in_Sidewall_front));

            //K66*L53+F68*L54
            Doc.steam_Enthalpy_at_first_SH_inlet=Doc.Enthalpy_at_reverse_chamber_roof_outlet*Doc.Ratio_of_total_flow_in_Sidewall_front+Doc.Reversing_chamber_outlet_steam_enthalpy*Doc.Ratio_of_total_flow_in_Sidewall_rear;

            //T_ph((D37-0.3)*0.980665+1.01325,K67)
            if(SteamScreenPresent==2)
            {
                Doc.Steam_temp_at_First_superheater=mCal_13B_BLL.SteamTempFirstSuperheater(Project_ID,Boiler_ID,Boiler_Load,Objective_ID);
            }
            else
            {
                Doc.Steam_temp_at_First_superheater = mStream_Macros.T_ph(a0, Doc.steam_Enthalpy_at_first_SH_inlet);
            }
            


            //--------------------------Get PID for 13B Calculation------------------------------

            dt = mCal_13B_BLL.Get_PID_For_13B_Calculation();


            //--------------------------Insert 13B Calculation-----------------------------------

            foreach (DataRow row in dt.Rows)
            {
                int pid = Convert.ToInt16(row["PID"].ToString());

                switch (pid)
                {
                    case 1:
                        mCal_13B_BLL.Insert_13B_Calculation(pid,Project_ID,Boiler_ID,Doc.Outlet_flue_gas_enthalpy,Boiler_Load,Objective_ID);
                        break;

                    case 2:
                        mCal_13B_BLL.Insert_13B_Calculation(pid, Project_ID, Boiler_ID, Doc.Outlet_flue_gas_temperature, Boiler_Load, Objective_ID);
                        break;

                    case 3:
                        mCal_13B_BLL.Insert_13B_Calculation(pid, Project_ID, Boiler_ID, Doc.Reversing_chamber_outlet_steam_enthalpy, Boiler_Load, Objective_ID);
                        break;

                    case 4:
                        mCal_13B_BLL.Insert_13B_Calculation(pid, Project_ID, Boiler_ID, Doc.Economiser_outlet_temp, Boiler_Load, Objective_ID);
                        break;

                    case 5:
                        mCal_13B_BLL.Insert_13B_Calculation(pid, Project_ID, Boiler_ID, Doc.Heat_absorption_in_heating_section, Boiler_Load, Objective_ID);
                        break;

                    case 6:
                        mCal_13B_BLL.Insert_13B_Calculation(pid, Project_ID, Boiler_ID, Doc.Roof, Boiler_Load, Objective_ID);
                        break;

                    case 7:
                        mCal_13B_BLL.Insert_13B_Calculation(pid, Project_ID, Boiler_ID, Doc.Water_wall, Boiler_Load, Objective_ID);
                        break;

                    case 8:
                        mCal_13B_BLL.Insert_13B_Calculation(pid, Project_ID, Boiler_ID, Doc.Economizer_hanger, Boiler_Load, Objective_ID);
                        break;

                    case 9:
                        mCal_13B_BLL.Insert_13B_Calculation(pid, Project_ID, Boiler_ID, Doc.Reversing_chamber_outlet_steam_temperature, Boiler_Load, Objective_ID);
                        break;
                }
            }

            //-------------------------------- Update S Parameter for First Superheater ---------------------------------------------

            mCal_13B_BLL.Update_First_Superheater(Project_ID, Boiler_ID, Boiler_Load, Objective_ID, SectionID_FirstSuperheater, Doc.Steam_temp_at_First_superheater, Doc.Inlet_temperature_side_wall, Doc.Reversing_chamber_outlet_steam_temperature, Iteration,SteamScreenID);

        }

    }
}
