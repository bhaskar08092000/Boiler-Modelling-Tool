using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.SC
{
    public class Cal_13B_SC
    {
        //Input

        public double Tube_diameter {get;set;}
        public double Tube_thickness { get; set; }
        public double Side_wall_area { get; set; }
        public double Roof_area { get; set; }
        public double Rear_wall_area { get; set; }
        public double Eco_hanger_heating_area_in_zone { get; set; }
        public double Eco_hanger_circumf_area { get; set; }
        public double Effective_radiation_layer_thickness { get; set; }
        public double Temperature_of_flue_gas_into_panel { get; set; }
        public double Enthalpy_of_flue_gas_into_panel { get; set; }
        public double Heat_preservation_coefficient { get; set; }
        public double Partial_pressure_of_triatomic_gases { get; set; }
        public double Volume_fraction_of_water_vapor { get; set; }
        public double Gas_density { get; set; }
        public double Flue_gas_total_volume { get; set; }
        public double Mean_diameter_of_ash_particle { get; set; }
        public double Volume_fraction_of_triatomic_gases { get; set; }
        public double Dimensionless_concentration_of_fly_ash { get; set; }
        public double Furnace_pressure { get; set; }
        public double Radiation_heat_from_flue_gas_in_upstream_zone { get; set; }
        public double Design_fuel_consumption { get; set; }
        public double Flue_gas_flow_fraction_through_the_element { get; set; }
        public double Inlet_temperature_side_wall { get; set; }
        //public double Inlet_steam_enthalpy { get; set; }
        //public double Reverse_chamber_outlet_pressure { get; set; }
        //public double Main_steam_flow_rate { get; set; }
        //public double Desuperheating_spray_stage_1 { get; set; }
        //public double Desuperheating_spray_stage_2_if_any { get; set; }
        //public double Inlet_temperature_side_wall { get; set; }
        public double Inlet_steam_pressure { get; set; }
        public double Reverse_chamber_outlet_pressure { get; set; }
        public double Reverse_chamber_flow_rate { get; set; }

        public double Economiser_hanger_inlet_temp { get; set; }
        public double Economiser_hanger_inlet_pressure { get; set; }
        public double Economiser_hanger_inlet_enthalpy { get; set; }
        public double Economiser_hanger_outlet_pressure { get; set; }
        public double Economiser_hanger_flow_rate { get; set; }
        public double Ash_deposit_coefficient { get; set; }
        public double Tube_wall_fouling_emmisivity { get; set; }

        public double Reversing_Chamber_Exit_Temp_1 { get; set; }
        //public double Mean_diameter_of_ash_particle { get; set; }   //pi
        public double Flue_gas_density { get; set; }    //1A


        //Output

        public double Inlet_flue_gas_temperature { get; set; }
        public double Inlet_flue_gas_enthalpy { get; set; }
        public double convection_heat { get; set; }
        public double Side_wall_absorbed_heat { get; set; }
        public double Rear_wall_absorbed_heat { get; set; }
        public double Roof_absorbed_heat { get; set; }
        public double Economiser_hanger_absorbed { get; set; }
        public double Total_absorbed_heat_by_reversing_chamber { get; set; }
        public double Outlet_flue_gas_enthalpy { get; set; }
        public double Outlet_flue_gas_temperature { get; set; }
        public double Average_flue_gas_temperature { get; set; }
        public double Inlet_steam_temperature { get; set; }
        public double Inlet_steam_enthalpy_1 { get; set; }
        public double Steam_enthalpy_increment { get; set; }
        public double Reversing_chamber_outlet_pressure_1 { get; set; }
        public double Reversing_chamber_outlet_steam_enthalpy { get; set; }
        public double Reversing_chamber_outlet_steam_temperature { get; set; }
        public double Average_steam_temperature { get; set; }
        public double Ash_deposition_coefficient { get; set; }
        public double Fouling_layer_temperature_of_tube_wall { get; set; }
        public double Product_of_pn_and_s { get; set; }
        public double Radiant_absorption_coefficient_of_gas { get; set; }
        public double Radiant_absorption_coefficient_of_fly_ash { get; set; }
        public double Radiant_absorption_coefficient_of_flue_gas_radiation { get; set; }
        public double Exponent_of_Eq_2_73 { get; set; }
        public double Flue_gas_emissivity { get; set; }
        public double Radiation_heat_transfer_coefficient { get; set; }
        public double Average_temperature_difference { get; set; }
        public double Absorbed_heat_of_roof_superheater { get; set; }
        public double Error1 { get; set; }
        public double Absorbed_heat_of_rear_wall_superheater { get; set; }
        public double Error2 { get; set; }
        public double Absorbed_heat_of_side_wall_superheater { get; set; }
        public double Error3 { get; set; }
        public double Economiser_hanger_inlet_temp_1 { get; set; }
        public double Economiser_inlet_enthalpy { get; set; }
        public double Economiser_outlet_enthalpy { get; set; }
        public double Economiser_outlet_pressure { get; set; }
        public double Economiser_outlet_temp { get; set; }
        public double Average_water_temp_of_hanger_tube { get; set; }
        public double Temp_difference { get; set; }
        public double Absorbed_convection_heat_of_hanger_tube { get; set; }
        public double Error4 { get; set; }

        public double Heat_absorption_in_heating_section { get; set; }
        public double Roof { get; set; }
        public double Water_wall { get; set; }
        public double Economizer_hanger { get; set; }

        public double Sidewall_front_portion_tube_diameter { get; set; }
        public double Sidewall_front_portion_tube_numbers { get; set; }
        public double Sidewall_rear_portion_tube_diameter { get; set; }
        public double Sidewall_rear_portion_tube_numbers { get; set; }
        public double Front_wall_tube_diameter { get; set; }
        public double Front_wall_tube_numbers { get; set; }
        public double Extended_steam_wall_header_tube_diameter { get; set; }
        public double Extended_steam_wall_header_tube_numbers { get; set; }

        public double Fraction_of_total_flow_in_Sidewall_front { get; set; }
        public double Fraction_of_total_flow_in_Sidewall_rear { get; set; }
        public double Fraction_of_side_front_wall_flow_in_backpass_front_wall { get; set; }
        public double Fraction_of_side_front_wall_flow_in_extended_side_wall { get; set; }

        public double Ratio_of_total_flow_in_Sidewall_front { get; set; }
        public double Ratio_of_total_flow_in_Sidewall_rear { get; set; }
        public double Ratio_of_side_front_wall_flow_in_backpass_front_wall { get; set; }
        public double Ratio_of_side_front_wall_flow_in_extended_side_wall { get; set; }

        public double steam_Enthalpy_at_extended_side_wall { get; set; }
        public double Backpass_screen_exit_steam_enthalpy { get; set; }
        public double Enthalpy_at_reverse_chamber_roof_inlet { get; set; }
        public double Enthalpy_at_reverse_chamber_roof_outlet { get; set; }
        public double steam_Enthalpy_at_first_SH_inlet { get; set; }
        public double Steam_temp_at_First_superheater { get; set; }

        public double Steam_temp_at_extended_side_wall { get; set; }
        public double Steam_screen_exit_steam_temp { get; set; }



    }
}
