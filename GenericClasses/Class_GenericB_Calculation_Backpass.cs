using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;
using BoilerModellingTool.BLL;
using System.Data;
using System.Data.SqlClient;


namespace GenericClasses
{
    public class Class_GenericB_Calculation_Backpass
    {
        DataTable dt;

        Cal_GenericB_SC Doc{get;set;}

        Cal_GenericB_BLL mCal_GenericB_BLL;

        Stream_Macros mStream_Macros;

        public void Calculation_For_GenericB(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,string SectionID,string SectionType,string Location,int LocationNumber,string PreviousSectionID,int PreviousLocationNumber,string GroupID1,string GroupID2,int Flow,int SteamCooledWallPresent,int Iteration,string PreviousHeatingElementID,string PreviousSectionType)
        {
            //Doc = new Cal_GenericB_SC();

            dt = new DataTable();

            string SteamCooledWallPresentSectionID = "";

            mCal_GenericB_BLL = new Cal_GenericB_BLL();

            mStream_Macros=new Stream_Macros();

            Doc = mCal_GenericB_BLL.Get_InputFor_GenericB(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, SectionType, PreviousSectionID, Location, LocationNumber, PreviousLocationNumber, GroupID1, GroupID2, SteamCooledWallPresent, Iteration, PreviousHeatingElementID, PreviousSectionType);

            SteamCooledWallPresentSectionID = mCal_GenericB_BLL.Get_SteamedCooledWall_SectionID(ProjectID, BoilerID);

            Doc.Inlet_flue_gas_temperature = Doc.Temperature_of_flue_gas_into_panel;

            Doc.Inlet_flue_gas_enthalpy = Doc.Enthalpy_of_flue_gas_into_panel;

            if (Location == "Backpass" || Location == "BackpassSH" || Location == "BackpassRH")
            {
                Doc.Convection_heat_of_heating_section_zone = 2500;

                Doc.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof = 0;

                Doc.Absorbed_heat_of_additional_heating_surfaces_of_side_wall = 0;
            }
            else
            {
                Doc.Convection_heat_of_heating_section_zone = 85;

                Doc.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof = 7.7;

                Doc.Absorbed_heat_of_additional_heating_surfaces_of_side_wall = 16;
            }
            

            //Doc.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof=7.7;

            //Doc.Absorbed_heat_of_additional_heating_surfaces_of_side_wall=19.2;

            //Doc.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof = 7.7;

            //Doc.Absorbed_heat_of_additional_heating_surfaces_of_side_wall = 16;

            //-------------------------Generic B Calculation-----------------------------

            do
            {

                //do
                //{

                    //do
                    //{

                        Doc.Outlet_flue_gas_enthalpy = Doc.Inlet_flue_gas_enthalpy - Doc.Convection_heat_of_heating_section_zone / Doc.Heat_preservation_coefficient;

                        Doc.Outlet_flue_gas_temperature = mCal_GenericB_BLL.Get_Required_Temperature(ProjectID, BoilerID, BoilerLoad, ObjectiveID, Doc.Outlet_flue_gas_enthalpy);
                        //Doc.Outlet_flue_gas_temperature = 891.4;

                        Doc.Flue_gas_average_temperature = (Doc.Inlet_flue_gas_temperature + Doc.Outlet_flue_gas_temperature) / 2;

                        Doc.Absorbed_convection_heat_of_heating_section = Doc.Convection_heat_of_heating_section_zone - Doc.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof - Doc.Absorbed_heat_of_additional_heating_surfaces_of_side_wall;

                        Doc.Product_of_pn_and_s = Doc.Partial_pressure_of_triatomic_gases * Doc.Effective_radiation_layer_thickness;

                        //10.2*((0.78+1.6*D28)/(10.2*F78)^0.5-0.1)*(1-0.37*(F76+273)/1000)
                        //double p0 = 0.78 + 1.6 * Doc.Volume_fraction_of_water_vapor;
                        //double p01 = Math.Pow((10.2 * Doc.Product_of_pn_and_s), 0.5)-0.1;
                        //double p02 = (1 - 0.37 * (Doc.Flue_gas_average_temperature + 273) / 1000);
                        double Power = ((0.78 + 1.6 * Doc.Volume_fraction_of_water_vapor) / (Math.Pow((10.2 * Doc.Product_of_pn_and_s), 0.5))) - 0.1;
                        double power1 = (1 - 0.37 * (Doc.Flue_gas_average_temperature + 273) / 1000);
                        double POWER3 = 10.2 * Power * power1;
                        Doc.Radiant_absorption_coefficient_of_gas = POWER3;

                        //43850*D29/((F76+273)^2*('4B'!D44)^2)^(1/3)
                        //double p0 = ((Math.Pow(Doc.Flue_gas_average_temperature + 273, 2) * Math.Pow(Doc.Mean_diameter_of_ash_particle, 2)));
                        //double p01 = Math.Pow(p0, 1 / 3);
                        double p01 = (Math.Pow(Math.Pow(Doc.Flue_gas_average_temperature + 273, 2) * Math.Pow(Doc.Mean_diameter_of_ash_particle, 2), 0.333333333));
                        Doc.Radiant_absorption_coefficient_of_fly_ash = 43850 * Doc.Gas_density / p01;

                        //F79*D32+F80*D33
                        Doc.Radiation_absorption_coefficient_of_flue_gas_radiation = Doc.Radiant_absorption_coefficient_of_gas * Doc.Volume_fraction_of_triatomic_gases + Doc.Radiant_absorption_coefficient_of_fly_ash * Doc.Dimensionless_concentration_of_fly_ash;

                        //F81*D34*D18
                        Doc.Exponent_of_Eq = Doc.Radiation_absorption_coefficient_of_flue_gas_radiation * Doc.Furnace_pressure * Doc.Effective_radiation_layer_thickness;

                        //1-EXP(-F82)
                        Doc.Flue_gas_emissivity = 1 - Math.Exp(-Doc.Exponent_of_Eq);

                        //D35
                        Doc.Radiation_heat_from_flue_gas_to_panel_back = Doc.Radiation_heat_from_flue_gas_in_upstream_zone;

                        //F77+F84
                        Doc.Total_heat_absorbed_by_heating_section = Doc.Absorbed_convection_heat_of_heating_section + Doc.Radiation_heat_from_flue_gas_to_panel_back;

                        //((D41)*0.980665+1.01325)/10
                        Doc.Inlet_steam_pressure_of_heating_section = ((Doc.Inlet_water_steam_pressure) * 0.980665 + 1.01325) / 10;

                        //((D42)*0.980665+1.01325)/10
                        Doc.Outlet_steam_pressure_of_heating_section = ((Doc.Outlet_water_steam_pressure) * 0.980665 + 1.01325) / 10;

                        //h_pt(D41*0.980665+1.01325,D40+0.01)
                        Doc.Upstream_heating_section_steam_enthalpy = mStream_Macros.h_pT(Doc.Inlet_water_steam_pressure * 0.980665 + 1.01325, Doc.Inlet_water_steam_temperature + 0.01);

                        //(F88*(D45-D46)+D52*D46)/D45
                        Doc.Heating_section_Inlet_steam_enthalpy = (Doc.Upstream_heating_section_steam_enthalpy * (Doc.Heating_section_flow_rate - Doc.De_superheating_spray) + Doc.De_superheating_spray_enthalpy * Doc.De_superheating_spray) / Doc.Heating_section_flow_rate;

                        //T_ph(D41*0.980665+1.01325,F89)
                        Doc.Inlet_steam_temperature = mStream_Macros.T_ph(Doc.Inlet_water_steam_pressure * 0.980665 + 1.01325, Doc.Heating_section_Inlet_steam_enthalpy);

                        //F89+D36*F85*D37/(D45/3600)
                        Doc.Outlet_steam_enthalpy = Doc.Heating_section_Inlet_steam_enthalpy + Doc.Design_fuel_consumption * Doc.Total_heat_absorbed_by_heating_section * Doc.Flue_gas_flow_fraction_through_the_element / (Doc.Heating_section_flow_rate / 3600);

                        if (SectionType == "Water Screen Sections")
                        {
                            Doc.Outlet_steam_temperature = Doc.Inlet_steam_temperature;
                        }
                        else
                        {
                            Doc.Outlet_steam_temperature = mStream_Macros.T_ph(10*Doc.Outlet_steam_pressure_of_heating_section,Doc.Outlet_steam_enthalpy);
                        }

                        //0.5*(F86+F87)
                        Doc.Average_steam_pressure_of_heating_section = 0.5 * (Doc.Inlet_steam_pressure_of_heating_section + Doc.Outlet_steam_pressure_of_heating_section);

                        //0.5*(F92+F90)
                        Doc.Average_steam_temperature_of_heating_section = 0.5 * (Doc.Outlet_steam_temperature + Doc.Inlet_steam_temperature);

                        //v_pT(F93*10,F94)
                        Doc.Average_specific_volume_of_steam_of_heating_section = mStream_Macros.v_pT(Doc.Average_steam_pressure_of_heating_section * 10, Doc.Average_steam_temperature_of_heating_section);

                        //D45*F95/(3600*D10)
                        Doc.Average_speed_of_steam_of_heating_section = Doc.Heating_section_flow_rate * Doc.Average_specific_volume_of_steam_of_heating_section / (3600 * Doc.Steam_flow_area);

                        //4.166666667E-11*(D8-2*D9)^6- 1.173076923177E-08*(D8-2*D9)^5 + 1.33733974377156E-06*(D8-2*D9)^4 - 0.0000800917832293105*(D8-2*D9)^3 + 0.00277223047821308*(D8-2*D9)^2 - 0.0603097028027454*(D8-2*D9) + 1.65375000006153
                        double p1 = 4.166666667E-11 * Math.Pow(Doc.Tube_diameter - 2 * Doc.Tube_thickness, 6);
                        double p2 = 1.173076923177E-08 * Math.Pow(Doc.Tube_diameter - 2 * Doc.Tube_thickness, 5);
                        double p3 = 1.33733974377156E-06 * Math.Pow(Doc.Tube_diameter - 2 * Doc.Tube_thickness, 4);
                        double p4 = 0.0000800917832293105 * Math.Pow(Doc.Tube_diameter - 2 * Doc.Tube_thickness, 3);
                        double p5 = 0.00277223047821308 * Math.Pow(Doc.Tube_diameter - 2 * Doc.Tube_thickness, 2);
                        double p6 = 0.0603097028027454 * (Doc.Tube_diameter - 2 * Doc.Tube_thickness);
                        Doc.Correction_factor_for_tube_diameter = p1 - p2 + p3 - p4 + p5 - p6 + 1.65375000006153;

                        //tc_pt(F93*10,F94)
                        Doc.Thermal_conductivity_of_steam_of_heating_section = mStream_Macros.tc_pT(Doc.Average_steam_pressure_of_heating_section * 10, Doc.Average_steam_temperature_of_heating_section);

                        //my_pT(F93*10,F94)*F95
                        double p02 = mStream_Macros.my_pT(Doc.Average_steam_pressure_of_heating_section * 10, Doc.Average_steam_temperature_of_heating_section);
                        Doc.Kinematic_viscosity_of_steam_of_heating_section = p02 * Doc.Average_specific_volume_of_steam_of_heating_section;

                        //pr_pT(F93*10,F94)
                        Doc.Prandtl_number_of_steam_of_heating_section = mStream_Macros.Pr_pT(Doc.Average_steam_pressure_of_heating_section * 10, Doc.Average_steam_temperature_of_heating_section);

                        //0.023*F98/(D8/1000-2*D9/1000)*(F96*(D8/1000-2*D9/1000)/F99)^0.8*F100^0.4*F97
                        //double p11 = (Doc.Tube_diameter / 1000 - 2 * Doc.Tube_thickness / 1000);
                        //double p12 = Math.Pow((Doc.Average_speed_of_steam_of_heating_section * (Doc.Tube_diameter / 1000 - 2 * Doc.Tube_thickness / 1000) / Doc.Kinematic_viscosity_of_steam_of_heating_section), 0.8);
                        //double p13 = Math.Pow(Doc.Prandtl_number_of_steam_of_heating_section, 0.4);
                        //Doc.Steam_side_heat_transfer_coefficient_of_heating_section = 0.023 * Doc.Thermal_conductivity_of_steam_of_heating_section / p11 * p12 * p13 * Doc.Correction_factor_for_tube_diameter;
                        Double a = ((Doc.Tube_diameter / 1000) - (2 * Doc.Tube_thickness / 1000));
                        Double c = 0.023 * Doc.Thermal_conductivity_of_steam_of_heating_section;
                        Double b = Math.Pow((Doc.Average_speed_of_steam_of_heating_section * a / Doc.Kinematic_viscosity_of_steam_of_heating_section), 0.8);
                        Double e = Math.Pow(Doc.Prandtl_number_of_steam_of_heating_section, 0.4);
                        Double f = Doc.Correction_factor_for_tube_diameter;
                        Doc.Steam_side_heat_transfer_coefficient_of_heating_section = c / a * b * e * f;


                        //D36*D30*D37*(F76+273)/(273*D11)
                        Doc.Flue_gas_velocity = Doc.Design_fuel_consumption * Doc.Flue_gas_total_volume * Doc.Flue_gas_flow_fraction_through_the_element * (Doc.Flue_gas_average_temperature + 273) / (273 * Doc.Gas_average_flow_area);

                        //0.0000882529644268775*F76+ 0.0215130434782609
                        Doc.Thermal_conductivity__of_flue_gas = 0.0000882529644268775 * Doc.Flue_gas_average_temperature + 0.0215130434782609;

                        //4.794381705E-11*F76^2 + 1.0990798983625E-07*F76+ 0.0000081852173913032
                        Doc.Kinematic_viscosity_of_flue_gas = 4.794381705E-11 * Math.Pow(Doc.Flue_gas_average_temperature, 2) + 1.0990798983625E-07 * Doc.Flue_gas_average_temperature + 0.0000081852173913032;

                        //0.67-0.0001*F76
                        Doc.Average_Prandtl_number_of_flue_gas = 0.67 - 0.0001 * Doc.Flue_gas_average_temperature;

                        //(0.94+0.56*D28)*F105
                        Doc.Prandtl_number_of_flue_gas = (0.94 + 0.56 * Doc.Volume_fraction_of_water_vapor) * Doc.Average_Prandtl_number_of_flue_gas;

                        Doc.Correction_factor_for_rows = Doc.Correction_factor_for_tube_rows;

                        //0.92+0.726*D28
                        Doc.Flue_gas_composition_and_temperature_correction_coefficient = 0.92 + 0.726 * Doc.Volume_fraction_of_water_vapor;

                        //IF(D13<=1.5,1,IF(D14>2,1,(1+((2*IF(D13>3,3,D13))-3)*(1-D14/2)^3)^-2))
                        if (Doc.Relative_transverse_pitch <= 1.5 || Doc.Relative_vertical_pitch >= 2)
                        {
                            Doc.Correction_factor_for_the_geometric_arrangement = 1;
                        }
                        else if (Doc.Relative_transverse_pitch > 1.5 && Doc.Relative_vertical_pitch < 2)
                        {
                            double x = 0;
                            if (1.5 < Doc.Relative_transverse_pitch && Doc.Relative_transverse_pitch <= 3)
                            {
                                x = Doc.Relative_transverse_pitch;
                            }
                            else if (Doc.Relative_transverse_pitch > 3)
                            {
                                x = 3;
                            }
                            double p101 = (2 * x - 3);
                            double p102 = Math.Pow((1 - Doc.Relative_vertical_pitch / 2), 3);
                            Doc.Correction_factor_for_the_geometric_arrangement = Math.Pow(1 + p101 * p102, -2);

                        }

                        //0.2*F103/(D8/1000)*(F102*(D8/1000)/F104)^0.65*F106^0.33*F107*F109*F108
                        double p21 = (Doc.Tube_diameter / 1000);
                        double p23 = Doc.Correction_factor_for_rows * Doc.Flue_gas_composition_and_temperature_correction_coefficient * Doc.Correction_factor_for_the_geometric_arrangement;
                        double p22 = Math.Pow((Doc.Flue_gas_velocity * (Doc.Tube_diameter / 1000) / Doc.Kinematic_viscosity_of_flue_gas), 0.65);
                        double p24 = Math.Pow(Doc.Prandtl_number_of_flue_gas, 0.33);
                        Doc.Flue_gas_side_convection_coefficient = 0.2 * Doc.Thermal_conductivity__of_flue_gas / p21 * p23 * p22 * p24;

                        Doc.Ash_deposit_coefficient_1 = Doc.Ash_deposit_coefficient;

                        //F94+1000*(F111+1/F101)*D36*(F77+F84)/D15
                        Doc.Fouling_layer_temperature_of_tube_wall_ = Doc.Average_steam_temperature_of_heating_section + 1000 * (Doc.Ash_deposit_coefficient_1 + 1 / Doc.Steam_side_heat_transfer_coefficient_of_heating_section) * Doc.Design_fuel_consumption * (Doc.Absorbed_convection_heat_of_heating_section + Doc.Radiation_heat_from_flue_gas_to_panel_back) / Doc.Total_heating_area;

                        //5.7*10^-8*(D62+1)/2*F83*(F76+273)^3*(1-((F112+273)/(F76+273))^4)/(1-((F112+273)/(F76+273)))
                        //double p31 = (Doc.Tube_wall_fouling_emmisivity + 1);
                        //double p32 = 2 * Doc.Flue_gas_emissivity * Math.Pow(Doc.Flue_gas_average_temperature + 273, 3);
                        //double p33 = (1-Math.Pow(((Doc.Fouling_layer_temperature_of_tube_wall_ + 273) / (Doc.Flue_gas_average_temperature + 273)), 4));
                        //double p34 = (1 - ((Doc.Fouling_layer_temperature_of_tube_wall_ + 273) / (Doc.Flue_gas_average_temperature + 273)));
                        //Doc.Radiation_heat_transfer_coefficient_of_heating_section = 5.7 * Math.Pow(10, -8)*p31 / p32 * p33 / p34;
                        Double Con = ((Doc.Fouling_layer_temperature_of_tube_wall_ + 273) / (Doc.Flue_gas_average_temperature + 273));
                        Double val1 = 5.7 * Math.Pow(10, -8) * (Doc.Tube_wall_fouling_emmisivity + 1) / 2 * Doc.Flue_gas_emissivity * Math.Pow(Doc.Flue_gas_average_temperature + 273, 3);
                        Double val2 = 1 - Math.Pow(Con, 4);
                        Double val3 = 1 - Con;
                        Doc.Radiation_heat_transfer_coefficient_of_heating_section = val1 * val2 / val3;

                        Doc.Fuel_correction_coefficient_1 = Doc.Fuel_correction_coefficient;

                        //F113*(1+F114*((F69+273)/1000)^0.25*(D19/D20)^0.007)
                        Doc.Radiation_heat_transfer_coefficient_correction = Doc.Radiation_heat_transfer_coefficient_of_heating_section * (1 + Doc.Fuel_correction_coefficient_1 * (Math.Pow(((Doc.Inlet_flue_gas_temperature + 273) / 1000), 0.25)) * Math.Pow(Doc.Relative_space_depth_prior_to_Heating_element / Doc.Heataing_element_depth, 0.007));

                        Doc.Fouling_uniformity_coefficient_1 = Doc.Fouling_uniformity_coefficient;

                        Doc.Effectiveness_coefficient = Doc.Heating_section_effectiveness_coefficent;

                        //F116*F110+F115
                        Doc.Flue_gas_side_heat_transfer_coefficient = Doc.Fouling_uniformity_coefficient * Doc.Flue_gas_side_convection_coefficient + Doc.Radiation_heat_transfer_coefficient_correction;

                        //F117/(1/F118+1/F101)
                        Doc.Overall_heat_transfer_coefficient = Doc.Effectiveness_coefficient / (1 / Doc.Flue_gas_side_heat_transfer_coefficient + 1 / Doc.Steam_side_heat_transfer_coefficient_of_heating_section);

                        if (Flow == 1)
                        {
                            //F75-F92
                            Doc.Small_temperature_difference = Doc.Outlet_flue_gas_temperature - Doc.Outlet_steam_temperature;
                            //F69-F90
                            Doc.Large_temperature_difference = Doc.Inlet_flue_gas_temperature - Doc.Inlet_steam_temperature;
                        }
                        else
                        {
                            Doc.Small_temperature_difference = Math.Abs(Doc.Outlet_flue_gas_temperature - Doc.Inlet_steam_temperature);

                            Doc.Large_temperature_difference = Doc.Inlet_flue_gas_temperature - Doc.Outlet_steam_temperature;
                        }

                        //(F121-F120)/LN(F121/F120)
                        double d1 = (Doc.Large_temperature_difference - Doc.Small_temperature_difference);
                        double d2 = Math.Log(Doc.Large_temperature_difference / Doc.Small_temperature_difference);
                        Doc.Logarithmic_mean_temperature_difference = d1 / d2;

                        //F119*D15*F122/(1000*D36)
                        Doc.Convection_heat_of_heating_section = Doc.Overall_heat_transfer_coefficient * Doc.Total_heating_area * Doc.Logarithmic_mean_temperature_difference / (1000 * Doc.Design_fuel_consumption);

                        //(F77-F123)/F77*100
                        Doc.Error_1 = (Doc.Absorbed_convection_heat_of_heating_section - Doc.Convection_heat_of_heating_section) / Doc.Absorbed_convection_heat_of_heating_section * 100;

                        //Doc.Absorbed_heat_of_additional_heating_surfaces_of_side_wall = Doc.Absorbed_convection_heat_of_water_wall;                  

                        //(F77-F123)/F77*100

                        Doc.Inlet_steam_water_temperature_at_sidewall_inlet = Doc.Inlet_steam_water_temperature_of_side_wall;

                        //((D44)*0.980665+1.01325)/10
                        Doc.Inlet_pressure_of_steam_water_at_sidewall_inlet = ((Doc.Inlet_steam_water_pressure_of_side_wall) * 0.980665 + 1.01325) / 10;

                        //h_pt(F126*10,F125+0.01)
                        Doc.Inlet_enthalpy_of_steamwater_at_sidewall_inlet = mStream_Macros.h_pT(Doc.Inlet_pressure_of_steam_water_at_sidewall_inlet * 10, Doc.Inlet_steam_water_temperature_at_sidewall_inlet + 0.01);

                        //F127+(F73*D36*D37*3600)/D48
                        Doc.Outlet_enthalpy_of_steam_water_at_sidewall_inlet = Doc.Inlet_enthalpy_of_steamwater_at_sidewall_inlet + (Doc.Absorbed_heat_of_additional_heating_surfaces_of_side_wall * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element * 3600) / Doc.Steam_water_flow_in_side_wall;

                        //T_ph((F126*10)-0.2,F128)
                        Doc.Outlet_steam_water_temperature_at_sidewall_inlet = mStream_Macros.T_ph((Doc.Inlet_pressure_of_steam_water_at_sidewall_inlet * 10) - 0.2, Doc.Outlet_enthalpy_of_steam_water_at_sidewall_inlet);

                        if (SteamCooledWallPresent == 2)
                        {
                            Doc.Working_medium_temperature_of_side_wall = Doc.Inlet_steam_water_temperature_of_side_wall;
                        }
                        else
                        {
                            if (SteamCooledWallPresentSectionID == SectionID)
                            {
                                Doc.Working_medium_temperature_of_side_wall = (Doc.Inlet_steam_water_temperature_at_sidewall_inlet + Doc.Outlet_steam_water_temperature_at_sidewall_inlet) / 2;
                            }
                            else
                            {
                                Doc.Working_medium_temperature_of_side_wall = Doc.Inlet_steam_water_temperature_of_side_wall;
                            }
                        }

                        //F76-F130
                        Doc.Average_temperature_difference_of_heat_transfer = Doc.Flue_gas_average_temperature - Doc.Working_medium_temperature_of_side_wall;

                        //F119*F131*D16/(1000*D36*D37)
                        Doc.Absorbed_convection_heat_of_water_wall = Doc.Overall_heat_transfer_coefficient * Doc.Average_temperature_difference_of_heat_transfer * Doc.Side_Water_wall_heating_area__within_panel / (1000 * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element);

                        //(F73-F132)
                        Doc.Error_2 = Doc.Absorbed_heat_of_additional_heating_surfaces_of_side_wall - Doc.Absorbed_convection_heat_of_water_wall;

                        Doc.Absorbed_heat_of_additional_heating_surfaces_of_side_wall = Doc.Absorbed_convection_heat_of_water_wall;

                    //} while (Math.Abs(Doc.Error_2) >= 0.01 || Doc.Error_2 < 0.0);    //while (Math.Abs(Doc.Error_2) >= 0.01);

                    Doc.Inlet_steam_temperature_of_superheater_at_furnace_roof_1 = Doc.Inlet_steam_temperature_of_superheater_at_furnace_roof;

                    //h_pt(D49*0.980665+1.01325,F134+0.01)
                    double i01 = Doc.Inlet_steam_pressure_of_HS_at_furnace_roof * 0.980665 + 1.01325;
                    double i02 = Doc.Inlet_steam_temperature_of_superheater_at_furnace_roof_1 + 0.01;
                    Doc.Inlet_steam_enthalpy_of_superheater_at_furnace_roof_1 = mStream_Macros.h_pT(i01,i02);

                    //3600*F72*D36/(D47)
                    Doc.Steam_enthalpy_increment_of_superheater_at_furnace_roof = 3600 * Doc.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof * Doc.Design_fuel_consumption / (Doc.Steam_flow_in_roof);

                    //((D51)*0.980665+1.01325)/10
                    Doc.Steam_pressure_of_furnace_roof_at_heating_section_zone = ((Doc.Furnace_roof_pressure_within_panel_zone) * 0.980665 + 1.01325) / 10;

                    //F134+F136
                    Doc.Outlet_steam_enthalpy_of_furnace_roof_superheater = Doc.Inlet_steam_enthalpy_of_superheater_at_furnace_roof_1 + Doc.Steam_enthalpy_increment_of_superheater_at_furnace_roof;

                    //T_ph(F137*10,F138)
                    Doc.Outlet_steam_temperature_of_furnace_roof_superheater = mStream_Macros.T_ph(Doc.Steam_pressure_of_furnace_roof_at_heating_section_zone * 10, Doc.Outlet_steam_enthalpy_of_furnace_roof_superheater);

                    //F76-0.5*(F135+F139)
                    Doc.Average_temperature_difference = Doc.Flue_gas_average_temperature - 0.5 * (Doc.Inlet_steam_temperature_of_superheater_at_furnace_roof_1 + Doc.Outlet_steam_temperature_of_furnace_roof_superheater);

                    //F119*F140*D17/(1000*D36*D37)
                    Doc.Absorbed_convection_heat_of_superheater_at_furnace_roof = Doc.Overall_heat_transfer_coefficient * Doc.Average_temperature_difference * Doc.Heating_area_of_roof_tubes_in_the_zone / (1000 * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element);

                    //(F72-F141)
                    Doc.Error_3 = Doc.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof - Doc.Absorbed_convection_heat_of_superheater_at_furnace_roof;

                    Doc.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof = Doc.Absorbed_convection_heat_of_superheater_at_furnace_roof;

                ///} while (Math.Abs(Doc.Error_3) >= 0.01 || Doc.Error_3 < 0.0);

                //F123+F132+F141
                Doc.Absorbed_convection_heat_within_heating_section_zone = Doc.Convection_heat_of_heating_section + Doc.Absorbed_convection_heat_of_water_wall + Doc.Absorbed_convection_heat_of_superheater_at_furnace_roof;

                //(F71-F143)
                Doc.Total_error = Doc.Convection_heat_of_heating_section_zone - Doc.Absorbed_convection_heat_within_heating_section_zone;

                //Doc.Convection_heat_of_heating_section_zone = Doc.Convection_heat_of_heating_section_zone+0.001;

                if(Doc.Total_error<1)
                {
                    if(Math.Abs(Doc.Total_error)>500)
                    {
                        Doc.Convection_heat_of_heating_section_zone = Doc.Convection_heat_of_heating_section_zone + 100;
                    }
                    else if(Math.Abs(Doc.Total_error)>200)
                    {
                        Doc.Convection_heat_of_heating_section_zone = Doc.Convection_heat_of_heating_section_zone + 50;
                    }
                    else if (Math.Abs(Doc.Total_error) > 100)
                    {
                        Doc.Convection_heat_of_heating_section_zone = Doc.Convection_heat_of_heating_section_zone + 25;
                    }
                    else
                    {
                        Doc.Convection_heat_of_heating_section_zone = Doc.Convection_heat_of_heating_section_zone + 0.001;
                    }
                }
                else
                {
                    if (Math.Abs(Doc.Total_error) > 500)
                    {
                        Doc.Convection_heat_of_heating_section_zone = Doc.Convection_heat_of_heating_section_zone - 100;
                    }
                    else if (Math.Abs(Doc.Total_error) > 200)
                    {
                        Doc.Convection_heat_of_heating_section_zone = Doc.Convection_heat_of_heating_section_zone - 50;
                    }
                    else if (Math.Abs(Doc.Total_error) > 100)
                    {
                        Doc.Convection_heat_of_heating_section_zone = Doc.Convection_heat_of_heating_section_zone - 25;
                    }
                    else if (Math.Abs(Doc.Total_error) > 50)
                    {
                        Doc.Convection_heat_of_heating_section_zone = Doc.Convection_heat_of_heating_section_zone - 10;
                    }
                    else if (Math.Abs(Doc.Total_error) > 25)
                    {
                        Doc.Convection_heat_of_heating_section_zone = Doc.Convection_heat_of_heating_section_zone - 5;
                    }
                    else
                    {
                        Doc.Convection_heat_of_heating_section_zone = Doc.Absorbed_convection_heat_within_heating_section_zone;
                    }
                }
                

                //Doc.Convection_heat_of_heating_section_zone = Doc.Absorbed_convection_heat_within_heating_section_zone;

            } while (Math.Abs(Doc.Total_error) >= 0.01 || Doc.Total_error < 0.0);


            //------------------------Fixed Calculation-------------------------

            //F77*D36*D37/1000
            Doc.Heat_absorption_in_heating_section = Doc.Absorbed_convection_heat_of_heating_section * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element / 1000;

            //F72*D36*D37/1000
            if (SteamCooledWallPresent == 1)
            {
                if (SteamCooledWallPresentSectionID == SectionID)
                {
                    Doc.Roof = (Doc.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof+Doc.Absorbed_heat_of_additional_heating_surfaces_of_side_wall) * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element / 1000;
                    Doc.Water_wall = 0;
                }
                else
                {
                    Doc.Roof = Doc.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element / 1000;
                    Doc.Water_wall = Doc.Absorbed_heat_of_additional_heating_surfaces_of_side_wall * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element / 1000;
                }
            }
            else
            {
                Doc.Roof = Doc.Absorbed_heat_of_additional_heating_surfaces_of_furnace_roof * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element / 1000;
                Doc.Water_wall = Doc.Absorbed_heat_of_additional_heating_surfaces_of_side_wall * Doc.Design_fuel_consumption * Doc.Flue_gas_flow_fraction_through_the_element / 1000;
            }

            //-------------------- Update S-Parameter Input/Output Values--------------------

            mCal_GenericB_BLL.Update_S_ParameterValue(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Math.Round(Doc.Inlet_water_steam_temperature, 2), Math.Round(Doc.Outlet_steam_temperature, 2), Iteration, PreviousHeatingElementID);

            
            //---------------Get PID for Generic B Calculation-------------------

           dt = mCal_GenericB_BLL.Get_PID_For_GenericB_Calculation();

            //---------------Insert Generic B Calculation------------------------

            foreach (DataRow row in dt.Rows)
            {
                int pid = Convert.ToInt16(row["PID"].ToString());

                switch (pid)
                {
                    case 1:
                        mCal_GenericB_BLL.Insert_GenericB_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Location, pid, Math.Round(Doc.Outlet_flue_gas_enthalpy,2),SectionType);
                        break;

                    case 2:
                        mCal_GenericB_BLL.Insert_GenericB_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Location, pid, Math.Round(Doc.Outlet_flue_gas_temperature,2), SectionType);
                        break;

                    case 3:
                        mCal_GenericB_BLL.Insert_GenericB_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Location, pid, Math.Round(Doc.Outlet_steam_enthalpy), SectionType);
                        break;

                    case 4:
                        mCal_GenericB_BLL.Insert_GenericB_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Location, pid, Math.Round(Doc.Outlet_steam_temperature,2), SectionType);
                        break;

                    case 5:
                        mCal_GenericB_BLL.Insert_GenericB_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Location, pid, Math.Round(Doc.Outlet_steam_water_temperature_at_sidewall_inlet,2), SectionType);
                        break;

                    case 6:
                        mCal_GenericB_BLL.Insert_GenericB_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Location, pid, Math.Round(Doc.Outlet_steam_temperature_of_furnace_roof_superheater,2), SectionType);
                        break;

                    case 7:
                        mCal_GenericB_BLL.Insert_GenericB_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Location, pid, Math.Round(Doc.Heat_absorption_in_heating_section,2), SectionType);
                        break;

                    case 8:
                        mCal_GenericB_BLL.Insert_GenericB_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Location, pid, Math.Round(Doc.Roof,2), SectionType);
                        break;

                    case 9:
                        mCal_GenericB_BLL.Insert_GenericB_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Location, pid, Math.Round(Doc.Water_wall,2), SectionType);
                        break;

                    case 10:
                        mCal_GenericB_BLL.Insert_GenericB_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Location, pid, Math.Round(Doc.Flue_gas_velocity), SectionType);
                        break;
                }

            }


            
        }
    
    }
}
