using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.SC
{
    public class Cal_5A_SC
    {

        //Input
        public double Tube_diameter_of_plate_superheater { get; set; }
        public double Platen_superheater_tube_thickness { get; set; }
        public double Platen_superheater_transverse_rows { get; set; }
        public double Platen_superheater_longitudinal_rows { get; set; }
        public double Platen_superheater_height { get; set; }
        public double Platen_superheater_depth { get; set; }
        public double Platen_superheater_average_transverse_spacing { get; set; }
        public double Number_of_head { get; set; }
        public double Configuration_factor_for_PSH { get; set; }
        public double Distance_from_platen_superheater_back_side_to_furnace_nose { get; set; }
        public double Distance_from_reheater_to_platen_superheater { get; set; }


        public double Furnace_width { get; set; }
        public double Furnace_depth { get; set; }
        public double Calculated_height_of_flue_duct_of_reheater { get; set; }    
 

        //Calculations
        public double Platen_superheater_average_transverse_spacing_calc{ get; set; } 
        public double Platen_superheater_average_longitudinal_spacing{ get; set; } 
        public double Relative_transverse_pitch{ get; set; } 
        public double Relative_vertical_pitch{ get; set; } 
        public double Inlet_radiation_area{ get; set; } 
        public double Outlet_radiation_area{ get; set; } 
        public double Platen_superheator_total_heating_area{ get; set; } 
        public double Configuration_factor_from_inlet_to_outlet{ get; set; } 
        public double Side_Water_wall_heating_area_1_within_panel{ get; set; } 
        public double Side_Water_wall_heating_area_2_within_panel{ get; set; } 
        public double Side_Water_wall_heating_area_within_panel{ get; set; } 
        public double Roof_tube_length_within_platen_superheator_zone{ get; set; }  
        public double Heating_area_of_roof_tubes_in_PSH_zone{ get; set; } 
        public double Effective_radiation_layer_thickness{ get; set; } 
        public double Gas_inlet_flow_area{ get; set; } 
        public double Gas_outlet_flow_area{ get; set; } 
        public double Gas_average_flow_area{ get; set; } 
        public double Steam_flow_area{ get; set; } 
        public double Correction_factor_for_tube_rows{ get; set; } 
        public double PSH_convective_area{ get; set; } 
        public double Bottom_window_area { get; set; } 

    }
}
