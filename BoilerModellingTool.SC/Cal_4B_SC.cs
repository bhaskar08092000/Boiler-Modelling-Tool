using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.SC
{
   public class Cal_4B_SC
    {

        //Input------

        public Double Lower_furnace_EPRS_area { get; set; }
        public Double Lower_furnace_enclosed_area { get; set; }
        public Double Exit_window_area_of_lower_furnace { get; set; }
        public Double Thickness_of_effective_radiation_layer_lower_furnace { get; set; }
        public Double Upper_furnace_absorbing_area { get; set; }
        public Double Upper_furnace_exit_window_area { get; set; }
        public Double Thickness_of_effective_radiation_layer_upper_furnace { get; set; }
        public Double half_dry_bottom_hopper_height { get; set; }
        public Double Furnace_main_body_height_ { get; set; }
        public Double Furnace_nose_downward_inclination__angle_height { get; set; }
        public Double Operating_burners_middle_elevation { get; set; }
        public Double Hopper_Middle_elevation { get; set; }
        public Double Primary_air_temperature_at_APH_outlet { get; set; }
        public Double Secondary_air_temperature_at_APH_outlet { get; set; }
        public Double Temp_of_leakage_air_into_furnace { get; set; }
        public Double Total_combustion_air_incl_furnace_leakage { get; set; }
        public Double Primary_air_flow_rate_at_APH_oulet { get; set; }
        public Double Secondary_air_flow_rate_at_APH_outlet { get; set; }
        public Double Tempering_air_flow_rate { get; set; }
        public Double Excess_air_ratio { get; set; }
        public Double Lower_heating_value { get; set; }
        public Double Unburnt_carbon_loss { get; set; }
        public Double Heat_loss_due_to_incomplete_combustion { get; set; }
        public Double Physical_heat_loss_of_ash { get; set; }
        public Double Buner_tilt { get; set; }
        public Double Drum_pressure { get; set; }
        public Double Economizer_hanger_outlet_steam_enthalpy { get; set; }
        public Double Main_steam_flow_rate { get; set; }
        public Double Desuperheating_spray_stage1 { get; set; }
        public Double Desuperheating_spray_stage2_if_any { get; set; }
        public Double Mean_diameter_of_ash_particle { get; set; }
        public Double Volume_fraction_of_water_vapor { get; set; }
        public Double Volume_fraction_of_triatomic_gases { get; set; }
        public Double Gas_density { get; set; }
        public Double Furnace_pressure { get; set; }
        public Double Dimensionless_concentration_of_fly_ash { get; set; }
        public Double Heat_preservation_coefficient { get; set; }
        public Double Design_fuel_consumption { get; set; }
        public Double Radiation_absorption_coefficient_of_coke_particles { get; set; }
        public Double Recirculated_gas_PC_of_total_gas_flow { get; set; }
        public Double Recirculated_gas_temperature { get; set; }
        public Double Recirculated_gas_enthalpy { get; set; }
        public Double Radiation_absorption_coefficint_of_coke_particles { get; set; }
        public Double Dimensionless_number_of_coke_particle_realted_to_coal_type { get; set; }
        public Double Dimensionless_number_of_coke_particle_realted_to_combustion_mode { get; set; }
        public Double Water_wall_fouling_factor { get; set; }



        public Double Tempering_air_temperature { get; set; }
        public Double Primary_air_enthalpy { get; set; }
        public Double Secondary_air_enthalpy { get; set; }
        public Double Tempering_air_enthalpy { get; set; }
        public Double Leakage_air_enthalpy { get; set; }
        public Double Leakage_air_flow_rate { get; set; }
        public Double Heat_input_by_air_sensible_ { get; set; }
        public Double Heat_input_by_gas_recirculation { get; set; }
        public Double Heat_input_by_1_kg_fuel { get; set; }
        public Double Theoretical_combustion_temparature_C { get; set; }

        public Double Theoretical_combustion_temparature_K { get; set; }

        public Double Furnace_outlet_gas_temperature { get; set; }
        public Double Furnace_outlet_gas_enthalpy { get; set; }
        public Double Partial_pressure_of_triatomic_gases { get; set; }
        public Double Average__gas_temp_in_lower_furnace_ { get; set; }
        public Double Product_of_pn_and_s { get; set; }
        public Double Radiation_absorption_coefficient_of_gas { get; set; }
        public Double Radiation_absorption_coefficient_of_fly_ash { get; set; }
        public Double Radiation_absorption_coefficient_of_flame_radiation { get; set; }
        public Double Exponent_of_Eq_2_73{get;set;}
        public Double Furnace_flame_emissivity { get; set; }
        public Double Average_thermal_efficiency_coefficient { get; set; }
        public Double Furnace_emissivity { get; set; }
        public Double Burner_height { get; set; }
        public Double Height_from_dry_bottom_hopper_to_center_of_furnace_exit { get; set; }
        public Double Burner_relative_height { get; set; }
        public Double Parameter { get; set; }
        public Double Relative_height_of_flame_center { get; set; }
        public Double Flame_center_modification_facor { get; set; }
       // public Double Furnace_outlet_gas_temperature { get; set; }
      //  public Double Furnace_outlet_gas_enthalpy { get; set; }
        public Double Radiative_absorbed_heat_of_furnace { get; set; }
        public Double Furnace_outlet_gas_temperature_error { get; set; }


        public Double Drum_saturation_temperature { get; set; }
        public Double Dry_saturated_steam_enthalpy_of_drum_outlet { get; set; }



        //Extra Modules

        public Double furnace_Pns { get; set; }
        public Double furnace_Temp_K { get; set; }
        public Double furnace_Kg { get; set; }
        public Double furnace_Kfa { get; set; }
        public Double furnace_K { get; set; }
        public Double furnace_flame_emissivity_af {get;set;}
           
            public Double furnace_Zeta_based_on_Tg { get; set; }
            public Double furnace_Furnace_emisisvity_based_on_zeta { get; set; }
            public Double calc_Lower_furnace_exit_temp { get; set; }
            public Double calc_Lower_furnace_exit_enthalpy { get; set; }
            public Double calc_Heat_available_lower_furnace { get; set; }
            public Double calc_Direct_radiation_from_lower_furnace { get; set; }
            public Double calc_zeta_ww_upper_furance { get; set; }
            public Double calc_Tg_Tpsh_in_ratio {get;set;}
        public Double calc_Average_temperature_in_upper_furnace_Tg_DegC {get;set;}

        public Double calc_Average_temperature_in_upper_furnace_Tg_K { get; set; }
        public Double calc_WW_exit_radiation { get; set; }
        public Double calc_Downstream_element_inlet_fluegas_temperature_Tpsh_in {get;set;}
        public Double calc_Downstream__element_Inlet_flue_gas__enthalpy { get; set; }
        public Double calc_Available_heat { get; set; }
        public Double calc_Error { get; set; }
        public Double calc_Radiation_flux_to_downstream__heating_element { get; set; }
        public Double calculation_Nose_inlet_Temp { get; set; }

        public Double calculation_Nose_inlet_Heat_Flux{ get; set; }
        public Double calculation_water_wall_Temp { get; set; }

        public Double calculation_water_wall_Heat_Flux{ get; set; }
        public Double calculation_Roof_geometric_mean_Temp { get; set; }

        public Double calculation_Roof_geometric_mean_Heat_Flux { get; set; }

        public Double Nose_Heat_Flux { get; set; }

        public Double Nose_Heat_Flux_Ratio { get; set; }
   
        public Double calculation_avg_location_vertical_direction_Heat_Flux { get; set; }

        public Double calculation_avg_location_vertical_direction_Heat_Flux_Ratio{ get; set; }

        public Double calculation_Roof_Heat_Flux { get; set; }

        public Double calculation_Roof_Heat_Flux_ratio{ get; set; }

        public Double Direct_radiation_from_lower_furnace_MW { get; set; }

        //----New changes in 4B module 

        public Double Upper_furnace_water_wall_n_roof_area { get; set; }
        public Double Area_factor_for_water_walls_n_roof { get; set; }
        public Double Effective_area_of_upper_furnace { get; set; }


    }
}
