using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using BoilerModellingTool.SC;
using BoilerModellingTool.BLL;
using BoilerModellingTool.DAL;

namespace GenericClasses
{
    public class Class_15B_Calculation
    {

        public void Class_15B_Calculation_Result(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,string SectionID,string PreviousSectionID,string PreviousLocation,string GroupID,int Flow,int Iteration, string PreviousHeatingSectionID,int PendantRH)
        {
            Cal_15B_SC mCal_15B_SC = null;
            Cal_15B_BLL mCal_15B_BLL = null;
            Stream_Macros mStream_Macros_BLL = null;
            DataSet mdataset = null;
            DataTable dt = new DataTable();



            mCal_15B_SC = new Cal_15B_SC();
            mCal_15B_BLL = new Cal_15B_BLL();
            mStream_Macros_BLL = new Stream_Macros();
            mdataset = new DataSet();
            //mDataTable = new DataTable();

            mCal_15B_SC = mCal_15B_BLL.Get_Input_For_15B_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, PreviousSectionID, PreviousLocation, GroupID, PreviousHeatingSectionID,PendantRH);         

            //F60
            mCal_15B_SC.Inlet_flue_gas_enthalpy = mCal_15B_SC.Enthalpy_of_flue_gas_into_panel;

            //F61
            mCal_15B_SC.Inlet_flue_gas_Temperature = mCal_15B_SC.Temperature_of_flue_gas_into_panel;

            //F62
            //.Convection_heat_in_Economizer_zone= 1153.78601588521; //Need to check 


            //F63
            mCal_15B_SC.Side_steam_wall_heat__absorption = 0;
            mCal_15B_SC.Convection_heat_in_Economizer_zone = 1169.5;


            do
            {
                //F64   F60-F62/D26
                mCal_15B_SC.Outlet_flue_gas_enthalpy = mCal_15B_SC.Inlet_flue_gas_enthalpy - mCal_15B_SC.Convection_heat_in_Economizer_zone / mCal_15B_SC.Heat_preservation_coefficient;

                //F65
                mCal_15B_SC.Outlet_flue_gas_temperature = mCal_15B_BLL.Get_2A_PrimaryAir_ReqEnthalpy(ProjectID, BoilerID, BoilerLoad, ObjectiveID, Convert.ToString(mCal_15B_SC.Outlet_flue_gas_enthalpy));

                //F66   F62-F63
                mCal_15B_SC.Absorbed_convection_heat_of_economizer = mCal_15B_SC.Convection_heat_in_Economizer_zone-mCal_15B_SC.Side_steam_wall_heat__absorption;

                //F67   (D43)/1000
                mCal_15B_SC.Economizer_water_flow_rate = mCal_15B_SC.Heating_section_flow_rate/1000;

                //F68   ((D41)*0.980665+1.01325)/10
                mCal_15B_SC.Inlet_water_pressure = ((mCal_15B_SC.Economizer_inlet_pressure) * 0.980665 + 1.01325) / 10;

                //F69
                mCal_15B_SC.Inlet_water_temperature = mCal_15B_SC.Economizer_inlet_temperature;

                //F70
                mCal_15B_SC.Inlet_water_enthalpy = mStream_Macros_BLL.h_pT(mCal_15B_SC.Inlet_water_pressure * 10, mCal_15B_SC.Inlet_water_temperature);

                //F71   F70+3.6*D36*D37*F66/F67
                mCal_15B_SC.Economizer_outlet_water_enthalpy  =mCal_15B_SC.Inlet_water_enthalpy+3.6*mCal_15B_SC.Design_fuel_consumption *mCal_15B_SC.Flue_gas_flow_fraction_through_the_element*mCal_15B_SC.Absorbed_convection_heat_of_economizer/mCal_15B_SC.Economizer_water_flow_rate;

                //F72   ((D42)*0.980665+1.01325)/10
                mCal_15B_SC.Outlet_water_pressure  =((mCal_15B_SC.Economizer_outlet_pressure)*0.980665+1.01325)/10;

                //F73   T_ph(F72*10,F71)
                mCal_15B_SC.Outlet_water_temperature  =mStream_Macros_BLL.T_ph(mCal_15B_SC.Outlet_water_pressure*10,mCal_15B_SC.Economizer_outlet_water_enthalpy);             
                
                //F74
                //mCal_15B_SC.Enthalpy_increment  =mCal_15B_SC.Economizer_outlet_water_enthalpy-mCal_15B_SC.Inlet_water_enthalpy;        
                
                //F75 F76
                if(Flow==1)
                {
                    mCal_15B_SC.Counterflow_large_temperature_difference = mCal_15B_SC.Inlet_flue_gas_Temperature - mCal_15B_SC.Inlet_water_temperature;

                    //F76
                    mCal_15B_SC.Counterflow_small_temperature_difference = mCal_15B_SC.Outlet_flue_gas_temperature - mCal_15B_SC.Outlet_water_temperature;
                
                }
                else
                {
                    //F75
                    mCal_15B_SC.Counterflow_large_temperature_difference = mCal_15B_SC.Inlet_flue_gas_Temperature - mCal_15B_SC.Outlet_water_temperature;

                    //F76
                    mCal_15B_SC.Counterflow_small_temperature_difference = mCal_15B_SC.Outlet_flue_gas_temperature - mCal_15B_SC.Inlet_water_temperature;
                
                }

                //F77   (F75-F76)/LN(F75/F76)
                mCal_15B_SC.Counterflow_logarithmic_mean_temperature_difference  =(mCal_15B_SC.Counterflow_large_temperature_difference -mCal_15B_SC.Counterflow_small_temperature_difference)/Math.Log(mCal_15B_SC.Counterflow_large_temperature_difference /mCal_15B_SC.Counterflow_small_temperature_difference);

                //F78   (F61+F65)/2
                mCal_15B_SC.Average_flue_gas_temperature  =(mCal_15B_SC.Inlet_flue_gas_Temperature+mCal_15B_SC.Outlet_flue_gas_temperature)/2;

                //F79   (F69+F73)/2
                mCal_15B_SC.Average_water_temperature  =(mCal_15B_SC.Inlet_water_temperature+mCal_15B_SC.Outlet_water_temperature)/2;

                //F80   D36*D37*D30*(F78+273)/(273*D11)
                mCal_15B_SC.Flue_gas_velocity  =mCal_15B_SC.Design_fuel_consumption *mCal_15B_SC.Flue_gas_flow_fraction_through_the_element*mCal_15B_SC.Flue_gas_total_volume *(mCal_15B_SC.Average_flue_gas_temperature+273)/(273*mCal_15B_SC.Gas_average_flow_area   );

                //F81   0.0000882529644268775*F78+ 0.0215130434782609
                mCal_15B_SC.Thermal_conductivity_of_flue_gas_  =0.0000882529644268775*mCal_15B_SC.Average_flue_gas_temperature+ 0.0215130434782609;

                //F82   4.794381705E-11*F78^2 + 1.0990798983625E-07*F78+ 0.0000081852173913032
                mCal_15B_SC.Kinematic_viscosity_of_flue_gas  = Math.Pow(4.794381705E-11*mCal_15B_SC.Average_flue_gas_temperature,2) + 1.0990798983625E-07*mCal_15B_SC.Average_flue_gas_temperature+ 0.0000081852173913032;

                //F83   0.71-0.0002*F78
                mCal_15B_SC.Average_Prandtl_number_of_flue_gas  =0.71-0.0002*mCal_15B_SC.Average_flue_gas_temperature;

                //F84   (0.94+0.56*D28)*F83
                mCal_15B_SC.Prandtl_number_of_flue_gas  =(0.94+0.56*mCal_15B_SC.Volume_fraction_of_water_vapor )*mCal_15B_SC.Average_Prandtl_number_of_flue_gas;

                //F85   D12
                mCal_15B_SC.Correction_factor_for_rows  =mCal_15B_SC.Correction_factor_for_tube_rows ;

                //F86   0.92+0.726*D28
                mCal_15B_SC.Flue_gas_composition_and_temperature_correction_coefficient  =0.92+0.726*mCal_15B_SC.Volume_fraction_of_water_vapor;

                //F87   IF(D13<=1.5,1,IF(D14>2,1,(1+((2*IF(D13>3,3,D13))-3)*(1-D14/2)^3)^-2))
                if (mCal_15B_SC.Relative_transverse_pitch <= 1.5 || mCal_15B_SC.Relative_vertical_pitch >= 2)
                {
                    mCal_15B_SC.Correction_factor_for_the_geometric_arrangement = 1;
                }
                else if (mCal_15B_SC.Relative_transverse_pitch > 1.5 && mCal_15B_SC.Relative_vertical_pitch < 2)
                {
                    double x = 0;
                    if (1.5 < mCal_15B_SC.Relative_transverse_pitch && mCal_15B_SC.Relative_transverse_pitch <= 3)
                    {
                        x = mCal_15B_SC.Relative_transverse_pitch;
                    }
                    else if (mCal_15B_SC.Relative_transverse_pitch > 3)
                    {
                        x = 3;
                    }
                    double p101 = (2 * x - 3);
                    double p102 = Math.Pow((1 - mCal_15B_SC.Relative_vertical_pitch / 2), 3);
                    mCal_15B_SC.Correction_factor_for_the_geometric_arrangement = Math.Pow(1 + p101 * p102, -2);

                }
                
                //mCal_15B_SC.Correction_factor_for_the_geometric_arrangement  =IF(mCal_15B_SC.Relative_transverse_pitch <=1.5,1,IF(mCal_15B_SC.Relative_vertical_pitch >2,1,(1+((2*IF(mCal_15B_SC.Relative_transverse_pitch >3,3,mCal_15B_SC.Relative_transverse_pitch ))-3)*(1-mCal_15B_SC.Relative_vertical_pitch /2)^3)^-2))
                //F88   0.2*(F81/(D8/1000))*(F80*(D8/1000)/F82)^0.65*F84^0.33*F85*F87*F86
                double p21 = (mCal_15B_SC.Tube_diameter / 1000);
                double p23 = mCal_15B_SC.Correction_factor_for_rows * mCal_15B_SC.Flue_gas_composition_and_temperature_correction_coefficient * mCal_15B_SC.Correction_factor_for_the_geometric_arrangement;
                double p22 = Math.Pow((mCal_15B_SC.Flue_gas_velocity * (mCal_15B_SC.Tube_diameter / 1000) / mCal_15B_SC.Kinematic_viscosity_of_flue_gas), 0.65);
                double p24 = Math.Pow(mCal_15B_SC.Prandtl_number_of_flue_gas, 0.33);
                mCal_15B_SC.Flue_gas_side_convection_coefficient = 0.2 * mCal_15B_SC.Thermal_conductivity_of_flue_gas_ / p21 * p23 * p22 * p24;

                //F89   D51
                mCal_15B_SC.Effective_coefficient = mCal_15B_SC.Heating_section_effectiveness_coefficent;

                //F90   F79+60
                mCal_15B_SC.Fouling_layer_temperature_of_tube_wall  =mCal_15B_SC.Average_water_temperature+60;

                //F91   D27*D18
                mCal_15B_SC.Product_of_pn_and_s  =mCal_15B_SC.Partial_pressure_of_triatomic_gases*mCal_15B_SC.Effective_radiation_layer_thickness  ;

                //F92   10.2*((0.78+1.6*D28)/(10.2*F91)^0.5-0.1)*(1-0.37*(F78+273)/1000)
                mCal_15B_SC.Radiant_absorption_coefficient_of_gas  =10.2*(Math.Pow(((0.78+1.6*mCal_15B_SC.Volume_fraction_of_water_vapor)/(10.2* mCal_15B_SC.Product_of_pn_and_s)),0.5)-0.1)*(1-0.37*(mCal_15B_SC.Average_flue_gas_temperature +273)/1000);

                //F93   43850*D29/((F78+273)^2*D31^2)^(1/3)
                //mCal_15B_SC.Radiant_absorption_coefficient_of_fly_ash  =43850*mCal_15B_SC.Gas_density /(Math.Pow((Math.Pow((mCal_15B_SC.Average_flue_gas_temperature +273),2))*Math.Pow(mCal_15B_SC.Mean_diameter_of_ash_particle,2),(1/3)));
                double p01 = (Math.Pow(Math.Pow(mCal_15B_SC.Average_flue_gas_temperature + 273, 2) * Math.Pow(mCal_15B_SC.Mean_diameter_of_ash_particle, 2), 0.333333333));
                mCal_15B_SC.Radiant_absorption_coefficient_of_fly_ash = 43850 * mCal_15B_SC.Gas_density / p01;

                //F94   F92*D32+F93*D33
                mCal_15B_SC.Radiant_absorption_coefficient_of_flue_gas_radiation = mCal_15B_SC.Radiant_absorption_coefficient_of_gas*mCal_15B_SC.Volume_fraction_of_triatomic_gases+mCal_15B_SC.Radiant_absorption_coefficient_of_fly_ash*mCal_15B_SC.Dimensionless_concentration_of_fly_ash;

                //F95   F94*D34*D18
                mCal_15B_SC.Exponent_of_Eq_2_73 = mCal_15B_SC.Radiant_absorption_coefficient_of_flue_gas_radiation*mCal_15B_SC.Furnace_pressure  *mCal_15B_SC.Effective_radiation_layer_thickness  ;

                //F96   1-EXP(-F95)
                mCal_15B_SC.Flue_gas_emissivity  =1-Math.Exp(-mCal_15B_SC.Exponent_of_Eq_2_73 );

                //F97   5.7*10^-8*(D52+1)/2*F96*(F78+273)^3*(1-((F90+273)/(F78+273))^4)/(1-((F90+273)/(F78+273)))
                Double Con = ((mCal_15B_SC.Fouling_layer_temperature_of_tube_wall + 273) / (mCal_15B_SC.Average_flue_gas_temperature + 273));
                Double val1 = 5.7 * Math.Pow(10, -8) * (mCal_15B_SC.Tube_wall_fouling_emmisivity + 1) / 2 * mCal_15B_SC.Flue_gas_emissivity * Math.Pow(mCal_15B_SC.Average_flue_gas_temperature + 273, 3);
                Double val2 = 1 - Math.Pow(Con, 4);
                Double val3 = 1 - Con;
                mCal_15B_SC.Radiation_heat_transfer_coefficient = val1 * val2 / val3;
                
                //F98   D53
                mCal_15B_SC.Fuel_correction_coefficient2 = mCal_15B_SC.Fuel_correction_coefficient;

                //F99   F97*(1+F98*((F61+273)/1000)^0.25*(D19/D20)^0.07)
                mCal_15B_SC.Radiation_heat_transfer_coefficient_correction = mCal_15B_SC.Radiation_heat_transfer_coefficient * (1 + mCal_15B_SC.Fuel_correction_coefficient2 * Math.Pow(((mCal_15B_SC.Inlet_flue_gas_Temperature + 273) / 1000), 0.25) * Math.Pow((mCal_15B_SC.Relative_space_depth_prior_to_economizer / mCal_15B_SC.Heating_element_depth), 0.07));

                //F100  D54*F88+F99
                mCal_15B_SC.Flue_gas_side_heat_transfer_coefficient  =mCal_15B_SC.Fouling_uniformity_coefficient *mCal_15B_SC.Flue_gas_side_convection_coefficient +mCal_15B_SC.Radiation_heat_transfer_coefficient_correction;

                //F101  F89/(1/F100)
                mCal_15B_SC.Overall_heat_transfer_coefficient  =mCal_15B_SC.Effective_coefficient/(1/mCal_15B_SC.Flue_gas_side_heat_transfer_coefficient);

                //F102  F101*F77*D15/(1000*D36*D37)
                mCal_15B_SC.Convection_heat  =mCal_15B_SC.Overall_heat_transfer_coefficient*mCal_15B_SC.Counterflow_logarithmic_mean_temperature_difference*mCal_15B_SC.Tube_total_heating_area /(1000*mCal_15B_SC.Design_fuel_consumption*mCal_15B_SC.Flue_gas_flow_fraction_through_the_element);

                //F103  (F66-F102)/F66*100
                mCal_15B_SC.Error  =(mCal_15B_SC.Absorbed_convection_heat_of_economizer-mCal_15B_SC.Convection_heat)/mCal_15B_SC.Absorbed_convection_heat_of_economizer*100;

                mCal_15B_SC.Convection_heat_in_Economizer_zone = mCal_15B_SC.Convection_heat;
                
            } while (Math.Abs(mCal_15B_SC.Error) >= 0.01 || mCal_15B_SC.Error<0);

            //----------------------Fixed Calculation---------------------------------------------

            mCal_15B_SC.Heat_absorption_in_heating_section=mCal_15B_SC.Absorbed_convection_heat_of_economizer* mCal_15B_SC.Design_fuel_consumption* mCal_15B_SC.Flue_gas_flow_fraction_through_the_element/1000;
            
            mCal_15B_SC.Roof=0;
            
            mCal_15B_SC.Water_wall = 0;

            //-------------------- Update S-Parameter Input/Output Values--------------------

            mCal_15B_BLL.Update_S_ParameterValue(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Math.Round(mCal_15B_SC.Economizer_inlet_temperature, 2), Math.Round(mCal_15B_SC.Outlet_water_temperature, 2), Iteration, PreviousHeatingSectionID);

            
            //---------------Get PID for Generic B Calculation-------------------

            dt = mCal_15B_BLL.Get_PID_For_15B_Calculation();

            //---------------Insert Generic B Calculation------------------------

            foreach (DataRow row in dt.Rows)
            {
                int pid = Convert.ToInt16(row["PID"].ToString());

                switch (pid)
                {
                    case 1:
                        mCal_15B_BLL.Insert_15B_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, pid, SectionID, Math.Round(mCal_15B_SC.Outlet_flue_gas_enthalpy, 2));
                        break;

                    case 2:
                        mCal_15B_BLL.Insert_15B_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, pid, SectionID, Math.Round(mCal_15B_SC.Outlet_flue_gas_temperature, 2));
                        break;

                    case 3:
                        mCal_15B_BLL.Insert_15B_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, pid, SectionID, Math.Round(mCal_15B_SC.Flue_gas_velocity, 2));
                        break;
                    
                    case 4:
                        mCal_15B_BLL.Insert_15B_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, pid, SectionID, Math.Round(mCal_15B_SC.Heat_absorption_in_heating_section, 2));
                        break;

                    case 5:
                        mCal_15B_BLL.Insert_15B_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, pid, SectionID, Math.Round(mCal_15B_SC.Roof, 2));
                        break;

                    case 6:
                        mCal_15B_BLL.Insert_15B_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, pid, SectionID, Math.Round(mCal_15B_SC.Water_wall, 2));
                        break;

                    case 7:
                        mCal_15B_BLL.Insert_15B_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, pid, SectionID, Math.Round(mCal_15B_SC.Outlet_water_temperature));
                        break;
                }

            }
            

        }
    }
}












































 