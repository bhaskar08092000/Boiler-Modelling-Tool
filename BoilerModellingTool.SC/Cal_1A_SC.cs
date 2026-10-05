using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.SC
{
    public class Cal_1A_SC
    {

        //public double Carbon { get; set; }
        //public double Hydrogen { get; set; }
        //public double Oxygen { get; set; }
        //public double Nitrogen { get; set; }
        //public double Sulfur { get; set; }
        //public double Ash { get; set; }
        //public double Moisture { get; set; }
        //public double Higher_heating_value { get; set; }
        //public double Volatile_matter_by_proximate_analysis { get; set; }
        //public double Fly_ash_fraction { get; set; }
        //public double Specific_humidity  { get; set; }
        //public double Specific_humidity_of_ambient_air { get; set; }

        //public double Design_fuel_consumption { get; set; }
        //public double Total_combustion_air { get; set; }
        //public double Design_flue_gas_flow { get; set; }
        //public double Excess_air { get; set; }
      
        //public double Design_CO2_in_flue_gas_at_eco_Outlet { get; set; }
        //public double Design_O2_in_flue_gas_at_eco_Outlet { get; set; }
 
        //public double Mass_flow_rate_of_Total_Combustion_air_ip { get; set; }

        // 1. Excess Air 
        public double Design_excess_air_ratio { get; set; } 

        //D32
        public double Correction_factor_for_elemental_composition { get; set; } 

        //D33
        public double Carbon_modified { get; set; } 

        //D34
        public double Oxygen_modified { get; set; } 

        //D35
        public double Theoretical_flue_gas_flow_rate { get; set; }

        //D36
        public double Excess_air_ratio_calc_based_on_given_total_combustion_air { get; set; } 

       
           // 2. HHV check
        public double Higher_heating_value_ByDulongsformula { get; set; }
        public double Deviation_in_given_HHV_value_from_calculated_value { get; set; }



           //LHV calculation
            //D43
        public double Lower_heating_value_on_as_received_basis { get; set; } 
            
            //D44
        public double Volatil_matter_on_dry_ash_free_basis { get; set; }  



           //------------------------------------------------------------------------------------------------------------------------------------

                // Coal-Combustion Product Volumes and Theoretical Air Volumes
            //F48
        public double Theoretical_volume_of_air { get; set; }   

            //F49
        public double Theoretical_volume_of_N2 { get; set; }  

            //F50
        public double Theoretical_volume_of_water_vapour { get; set; } 

            //F51
        public double Theoretical_volume_of_CO2 { get; set; }  

            //F52
        public double Theoretical_flue_gas_volume { get; set; } 

            //F53
        public double Fly_ash_concentration { get; set; } 


            

             

//-----------------------------------------------------------------------------------------------------------------------------------

            // Flue Gas Characteristics
            
            //F57  name changed due to ambiguity
        public double Design_fuel_consumption_ForFluegas { get; set; } 
            
            
            
            //F59
        public double Mass_flow_rate_of_Total_Combustion_air { get; set; } 
        
            //F58
        public double Theoretical_air_required { get; set; }  

            
            //F60
        public double Total_excess_air { get; set; }                        
            

              //-------------------------------------------------------------------------------------------
                ////61    //F61
        public double Excess_air_coefficient_of_flue_inlet_LowerFurnace { get; set; } 
               
                    //G61
        public double Excess_air_coefficient_of_flue_inlet_Upperfurnace { get; set; }   
                
                    //H61
        public double Excess_air_coefficient_of_flue_inlet_CrossDuct { get; set; }        

                    //I61
        public double Excess_air_coefficient_of_flue_inlet_ReverseChamber { get; set; } 

                    //J61
        public double Excess_air_coefficient_of_flue_inlet_BackPass { get; set; } 




              //-------------------------------------------------------------------------------------------

                //F62
        public double Excess_air_coefficient_of_flue_outlet_LowerFurnace { get; set; }
        public double Excess_air_coefficient_of_flue_outlet_UpperFurnace { get; set; }
        public double Excess_air_coefficient_of_flue_outlet_CrossDuct { get; set; }
        public double Excess_air_coefficient_of_flue_outlet_ReverseChamber { get; set; }
        public double Excess_air_coefficient_of_flue_outlet_BackPass { get; set; } 




             //-------------------------------------------------------------------------------------------

            //F63
        public double Average_excess_air_coefficient_of_flue_LowerFurnace { get; set; }
        public double Average_excess_air_coefficient_of_flue_UpperFurnace { get; set; }
        public double Average_excess_air_coefficient_of_flue_CrossDuct { get; set; }
        public double Average_excess_air_coefficient_of_flue_ReverseChamber { get; set; }
        public double Average_excess_air_coefficient_of_flue_BackPass { get; set; } 

            //-------------------------------------------------------------------------------------------


            //F64
        public double Excess_air_volume_LowerFurnace { get; set; }
        public double Excess_air_volume_UpperFurnace { get; set; }
        public double Excess_air_volume_CrossDuct { get; set; }
        public double Excess_air_volume_ReverseChamber { get; set; }
        public double Excess_air_volume_BackPass { get; set; } 

            //-------------------------------------------------------------------------------------------

            //F65
        public double Water_vapour_volume_LowerFurnace { get; set; }
        public double Water_vapour_volume_UpperFurnace { get; set; }
        public double Water_vapour_volume_CrossDuct { get; set; }
        public double Water_vapour_volume_ReverseChamber { get; set; }
        public double Water_vapour_volume_BackPass { get; set; }                           


            //-------------------------------------------------------------------------------------------



            //F66
        public double Flue_gas_total_volume_LowerFurnace { get; set; }
        public double Flue_gas_total_volume_UpperFurnace { get; set; }
        public double Flue_gas_total_volume_CrossDuct { get; set; }
        public double Flue_gas_total_volume_ReverseChamber { get; set; }
        public double Flue_gas_total_volume_BackPass { get; set; }  


            //-------------------------------------------------------------------------------------------
            //F67
        public double Total_flue_gas_volume_at_exit_LowerFurnace { get; set; }
        public double Total_flue_gas_volume_at_exit_UpperFurnace { get; set; }
        public double Total_flue_gas_volume_at_exit_CrossDuct { get; set; }
        public double Total_flue_gas_volume_at_exit_ReverseChamber { get; set; }
        public double Total_flue_gas_volume_at_exit_BackPass { get; set; } 


            //-------------------------------------------------------------------------------------------
            
            //F68
        public double Volume_fraction_of_RO2_LowerFurnace { get; set; }
        public double Volume_fraction_of_RO2_UpperFurnace { get; set; }
        public double Volume_fraction_of_RO2_CrossDuct { get; set; }
        public double Volume_fraction_of_RO2_ReverseChamber { get; set; }
        public double Volume_fraction_of_RO2_BackPass { get; set; } 

            //-------------------------------------------------------------------------------------------

            //F69
        public double Volume_fraction_of_water_vapour_LowerFurnace { get; set; }
        public double Volume_fraction_of_water_vapour_UpperFurnace { get; set; }
        public double Volume_fraction_of_water_vapour_CrossDuct { get; set; }
        public double Volume_fraction_of_water_vapour_ReverseChamber { get; set; }
        public double Volume_fraction_of_water_vapour_BackPass { get; set; } 

            //-------------------------------------------------------------------------------------------


            //F70
        public double Volume_fraction_of_triatomic_gas_LowerFurnace { get; set; }
        public double Volume_fraction_of_triatomic_gas_UpperFurnace { get; set; }
        public double Volume_fraction_of_triatomic_gas_CrossDuct { get; set; }
        public double Volume_fraction_of_triatomic_gas_ReverseChamber { get; set; }
        public double Volume_fraction_of_triatomic_gas_BackPass { get; set; } 


            //-------------------------------------------------------------------------------------------
            
            //F71
        public double Flue_gas_mass_of_1_kg_fuel_LowerFurnace { get; set; }
        public double Flue_gas_mass_of_1_kg_fuel_UpperFurnace { get; set; }
        public double Flue_gas_mass_of_1_kg_fuel_CrossDuct { get; set; }
        public double Flue_gas_mass_of_1_kg_fuel_ReverseChamber { get; set; }
        public double Flue_gas_mass_of_1_kg_fuel_BackPass { get; set; }        


            //-------------------------------------------------------------------------------------------
            
            //F72
        public double Flue_gas_density_LowerFurnace { get; set; }
        public double Flue_gas_density_UpperFurnace { get; set; }
        public double Flue_gas_density_CrossDuct { get; set; }
        public double Flue_gas_density_ReverseChamber { get; set; }
        public double Flue_gas_density_BackPass { get; set; } 



            //-------------------------------------------------------------------------------------------
            
            //F73
        public double Dimensionless_concentration_of_fly_ash_LowerFurnace { get; set; }
        public double Dimensionless_concentration_of_fly_ash_UpperFurnace { get; set; }
        public double Dimensionless_concentration_of_fly_ash_CrossDuct { get; set; }
        public double Dimensionless_concentration_of_fly_ash_ReverseChamber { get; set; }
        public double Dimensionless_concentration_of_fly_ash_BackPass { get; set; } 




           
    

                //Flue gas composition at economizer outlet (% vol. dry)

                //D76
        public double Total_flue_gas_volume_at_economizer_outlet { get; set; }
        public double Volume_of_CO2_at_economizer_outlet { get; set; }
        public double Volume_of_O2_at_economizer_outlet { get; set; }
        public double CO2_in_flue_gas_at_economizer_outlet { get; set; }
        public double O2_at_economizer_outlet_at_economizer_outlet { get; set; }
        public double Deviation_i_calculated_CO2_value_from_given_value { get; set; }
        public double Deviation_in_calculated_O2_value_from_given_value { get; set; } 

            
    
                //Flue gas flow rate check

                //D85
        public double Flue_gas_flow_cal { get; set; }
        public double Flue_gas_flow_design { get; set; }
        public double Deviation_in_calc_flue_gas_flow_rate_from_design_value { get; set; } 


                

     }

}
