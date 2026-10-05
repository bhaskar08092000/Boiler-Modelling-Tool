using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.SC
{
    public class Cal_5B_SC
    {
        public double PSH_tube_diameter { get; set; }
        public double PSH_tube_thickness { get; set; }
        public double Configuration_factor_from_inlet_to_outlet_of_panel { get; set; }
        public double Inlet_radiation_area { get; set; }
        public double Outlet_radiation_area { get; set; }
        public double Steam_flow_area { get; set; }
        public double Gas_average_flow_area { get; set; }
        public double Correction_factor_for_tube_rows { get; set; }
        public double Relative_transverse_pitch { get; set; }
        public double Relative_vertical_pitch { get; set; }
        public double Configuration_factor_for_PSH { get; set; }
        public double Platen_superheater_average_longitudinal_spacing { get; set; }
        public double Platen_superheator_total_heating_area { get; set; }
        public double Side_Water_wall_heating_area_within_panel { get; set; }
        public double Heating_area_of_roof_tubes_in_Upper_furnace { get; set; }
        public double Heating_area_of_roof_tubes_in_PH_zone { get; set; }
        public double Effective_radiation_layer_thickness { get; set; }
        public double Temperature_of_flue_gas_into_panel_input { get; set; }
        public double Enthalpy_of_flue_gas_into_panel_input { get; set; }
        public double Heat_preservation_coefficient { get; set; }
        public double Partial_pressure_of_triatomic_gases { get; set; }
        public double Volume_fraction_of_water_vapor { get; set; }
        public double Gas_density { get; set; }
        public double Flue_gas_total_volume { get; set; }
        public double Mean_diameter_of_ash_particle { get; set; }
        public double Volume_fraction_of_triatomic_gases { get; set; }
        public double Dimensionless_concentration_of_fly_ash { get; set; }
        public double Furnace_pressure { get; set; }
        public double Radiation_flux_at_PSH_inlet { get; set; }
        public double Design_fuel_consumption { get; set; }

        public double LTSH_outlet_temperature { get; set; }
        public double LTSH_outlet_pressure { get; set; }
        public double PSH_inlet_steam_pressure { get; set; }
        public double Drum_pressure { get; set; }
        public double Furnace_roof_pressure_within_panel_zone_input { get; set; }
        public double Main_steam_flow_rate { get; set; }
        public double De_superheating_spray_stage_1 { get; set; }
        public double De_superheating_spray_stage_2 { get; set; }
        public double De_superheating_spray_enthalpy { get; set; }
        public double PSH_inlet_steam_temperature_design { get; set; }
        public double PSH_outlet_steam_temperature_design { get; set; }
        public double Final_SH_outlet_pressure { get; set; }



        public double PSH_effectiveness_coefficent { get; set; }
        public double Radiation_fuel_correction_coefficient_input { get; set; }
        public double Tube_wall_fouling_emmisivity { get; set; }




        //Calculations


        public double 	Temperature_of_flue_gas_into_panel	 { get; set; }
 public double 	Enthalpy_of_flue_gas_into_panel_	 { get; set; }
 public double 	Convection_heat_of_panel_zone_	 { get; set; }
 public double 	Radiation_heat_from_flue_gas_to_heating_surface_through_panel	 { get; set; }
 public double 	Absorbed_convection_heat_of_furnace_roof_at_panel_zone	 { get; set; }
 public double 	Absorbed_convection_heat_of_water_wall_at_panel_zone	 { get; set; }
 public double 	Enthalpy_of_flue_gas_out_of_panel	 { get; set; }
 public double 	Temperature_of_flue_gas_out_of_panel	 { get; set; }
 public double Average_flue_gas_temperature_C { get; set; }
 public double Average_flue_gas_temperature_K { get; set; }
 
 public double 	Absorbed_convection_heat_of_panel	 { get; set; }
 public double 	Product_of_pn_and_s	 { get; set; }
 public double 	Radiant_absorption_coefficient_of_gas	 { get; set; }
 public double 	Radiant_absorption_coefficient_of_fly_ash	 { get; set; }
 public double 	Radiant_absorption_coefficient_of_flue_gas_radiation	 { get; set; }
 public double 	Exponent_of_Eq_2_73	 { get; set; }
 public double 	Flue_gas_emissivity	 { get; set; }
 public double 	Coefficient_considering_reradiation	 { get; set; }
 public double 	Radiation_heat_flow_of_panel_zone	 { get; set; }
 public double 	Corrected_radiative_intensity_of_panel_zone	 { get; set; }
 public double 	Inlet_direct_radiation_from_the_furnace	 { get; set; }
 public double 	Furnace_radiation_heat_leaked_out_of_the_panel	 { get; set; }
 public double 	Radiation_heat_absorbed_by_panel	 { get; set; }
 public double 	Total_heat_absorbed_by_panel	 { get; set; }
 public double 	Radiation_fuel_correction_coefficient	 { get; set; }
 public double 	Radiation_heat_from_flue_gas_to_heating_surface_behind_panel	 { get; set; }
 public double 	Error_in_radiation_heat_transfer_from_flue_gas__to_downstream_elements	 { get; set; }
 public double 	Pressure_of_steam_into_panel	 { get; set; }
 public double 	Pressure_of_steam_out_of_panel	 { get; set; }
 public double 	Upstream_heating_section_steam_enthalpy	 { get; set; }
 public double 	Enthalpy_of_steam_into_panel	 { get; set; }
 public double 	Temperature_of_steam_into_panel_after_spray_attemperator	 { get; set; }
 public double 	Primary_desuperheating_water_flow_rate	 { get; set; }

 public double 	Secondary_desuperheating_spray_flow_rate	 { get; set; }

 public double 	Steam_flow_through_the_Heating_section	 { get; set; }
 public double 	Enthalpy_of_steam_out_of_the_panel	 { get; set; }
 public double 	Temperature_of_steam_out_of_panel	 { get; set; }
 public double 	Average_temperature_of_steam_in__panel	 { get; set; }
 public double 	Average_specific_volume_of_steam_in_the__panel	 { get; set; }
 public double 	Average_speed_of_steam_in_the_panel	 { get; set; }
 public double 	Correction_coefficient_of_tube_diameter	 { get; set; }
 public double 	Thermal_conductivity_of_steam	 { get; set; }
 public double 	Kinematic_viscosity_of_steam	 { get; set; }
 public double 	Prandtl_number_of_steam_Pr	 { get; set; }
 public double 	Heat_transfer_coefficient_from_tube_wall_to_steam	 { get; set; }
 public double 	Average_flue_gas_velocity_among_panel	 { get; set; }
 public double 	Thermal_conductivity_of_flue_gas	 { get; set; }
 public double 	Kinematic_viscosity_of_flue_gas	 { get; set; }
 public double 	Average_Prandtl_number_of_flue_gas	 { get; set; }
 public double 	Prandtl_number_of_flue_gas	 { get; set; }
 public double 	Correction_factor_of_tube_rows	 { get; set; }
 public double 	Flue_gas_compositon__and_temperature__correction_coefficient	 { get; set; }
 public double 	Correction_factor_for_geometric_arrangement	 { get; set; }
 public double 	Flue_gas_side_convection_coefficient	 { get; set; }
 public double 	Ash_deposition_coefficient	 { get; set; }
 public double 	Fouling_layer_temperature_of_tube_wall	 { get; set; }
 public double 	Radiation_heat_transfer_coefficient	 { get; set; }
 public double 	Utilization_coefficient_of_panel	 { get; set; }
 public double 	Flue_gas_side_heat_transfer_coefficient_	 { get; set; }
 public double 	Overall_heat_transfer_coefficient	 { get; set; }
 public double 	Large_temperature_difference	 { get; set; }
 public double 	Small_temperature_difference	 { get; set; }
 public double 	Logarithmic_mean_temperature_difference	 { get; set; }
 public double 	Convection_heat_of_panel	 { get; set; }
 public double 	Error_2	 { get; set; }
 public double 	Water_temperature_	 { get; set; }
 public double 	Average_temperature_difference_of_heat_transfer_1	 { get; set; }
 public double 	Absobed_convection__heat_by_both_side_water_walls_within_panel_zone	 { get; set; }
 public double 	Error_3	 { get; set; }
 public double 	Furnace_radiation_heat_flux_absorbed_by_furnace_roof_cover	 { get; set; }
 public double 	Furnace_radiation_heat_absorbed_by_furnace_roof_cover	 { get; set; }
 public double 	Enthalpy_increment_of_furnace_roof_cover	 { get; set; }
 public double 	Saturated_steam_temperature_of_drum_outlet	 { get; set; }
 public double 	Dry_saturated_steam_enthalpy_of_drum_outlet	 { get; set; }
 public double 	Furnace_roof_pressure_within_panel_zone_	 { get; set; }
 public double 	Steam_enthalpy_of_furnace_roof_inlet_within_panel_zone	 { get; set; }
 public double 	Steam_temperature_of_furnace_roof_inlet_within_panel_zone	 { get; set; }
 public double 	Steam_enthalpy_increment_of_furnace_roof_within_panel_zone	 { get; set; }
 public double 	Steam_enthalpy_of_furnace_roof_outlet_within_panel_zone	 { get; set; }
 public double 	Steam_temperature_of_furnace_roof_within_panel_zone_	 { get; set; }
 public double 	Average_steam_temperature_of_furnace_roof_within_panel_zone	 { get; set; }
 public double 	Average_temperature_difference_of_heat_transfer_2	 { get; set; }
 public double 	Absorbed_convection_heat_of_furnace_roof_within_panel_zone	 { get; set; }
 public double 	Error_4	 { get; set; }
 public double 	Absorbed_convection_heat_within_panel_zone	 { get; set; }
 public double 	Total_error	 { get; set; }

    }
}
