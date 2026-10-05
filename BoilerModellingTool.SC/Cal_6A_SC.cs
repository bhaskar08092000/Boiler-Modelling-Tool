using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.SC
{
   public class Cal_6A_SC
    {
        //input
        public double Tube_diameter{ get;set; }
        public double Tube_thickness{ get;set; }
        public double Longitudinal_tube_rows{ get;set; }
        public double Number_of_head{ get;set; }
        public double Transverse_rows{ get;set; }
        public double Transverse_spacing{ get;set; }
        public double Longitudinal_spacing { get;set; }
        public double Reheater_depth{ get;set; }
        public double Furnace_width{ get;set; }
        public double Relative_space_depth_prior_to_Reheater{ get;set; }
        public double Height_of_flue_duct_at_Reheater_inlet{ get;set; }
        public double Height_of_flue_duct_at_Reheater_outlet{ get;set; }
        public double Distance_from_Reheater_to_WW_hanger_tube{ get;set; }
        public double Height_of_flue_duct_at_WW_hanger_inlet{ get;set; }
        public double Furnace_nose_up_dip_angle{ get;set; }        
   

        //Calculation
        public double Relative_transverse_spacing{ get;set; }
        public double Average_longitudinal_pitch{ get;set; }
        public double Relative_longitudinal_spacing{ get;set; }
        public double Average_tube_calculated_length{ get;set; }
        public double Heating_area_of_Reheater{ get;set; }
        public double Length_of_roof_tube_within_Reheater_zone{ get;set; }
        public double  Heating_area_of_roof_tubes{ get;set; }
        public double Heating_area_of_side_water_wall{ get;set; }
        public double Heating_area_of_bottom_water_wall{ get;set; }
        public double  Heating_area_of_water_wall{ get;set; }
        public double Inlet_flue_gas_flow_area{ get;set; }
        public double Gas_outlet_flow_area{ get;set; }
        public double Gas_average_flow_area{ get;set; }
        public double Steam_flow_area{ get;set; } 
        public double Effective_radiation_layer_thickness{ get;set; }
        public double Correction_factor_for_tube_rows{ get;set; }


    }
}
