using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.SC
{
   public class Cal_4A_SC
    {

         //*Inputs*//

         public double  Furnace_width  { get; set; }
         public double Furnace_depth  { get; set; }
         public double Dry_bottom_hopper_inclination  { get; set; }
         public double Dry_bottom_hopper_outlet_depth  { get; set; }
         public double dry_bottom_hopper_height  { get; set; }
         public double Furnace_nose_length  { get; set; }
         public double Furnace_nose_up_dip_angle  { get; set; }
         public double Furnace_nose_elevation_angle  { get; set; }
         public double Platen_superheater_depth  { get; set; }
         public double Distance_from_platen_superheater_back_side_to_furnace_nose  { get; set; }
         public double Vertical_part_height_of_furnace_nose  { get; set; }
         public double Height_of_platen_superheater  { get; set; }
         public double Depth_of_empty_room_space_before_the_SH  { get; set; }
         public double Furnace_main_body_height  { get; set; }
         public double Tube_diameter_of_water_wall  { get; set; }
         public double Tube_thickness_of_waterwall { get; set; }
         public double Tube_spacing  { get; set; }
         public double Roof_tube_diameter  { get; set; }
         public double Roof_tube_thickness  { get; set; }
         public double Roof_tube_row_number { get; set; }
         public double Design_Fuel_Consumption { get; set; }
         public double Lower_heating_value_on_as_received_basis { get; set; }



       //*Calculations*//


        public double Furnace_volume_thermal_load{get;set;}
        public double Calculated_furnace_volume{get;set;}
        public double Furnace_sectional_thermal_load{get;set;}
        public double Calculated_furnace_cross_section_area{get;set;}
        public double Aspect_ratio_of_furnace_section{get;set;}
        public double Outlet_size_of_membrane_wall_at_dry_bottom_hoppermiddle{get;set;}
        public double Dry_bottom_hopper_volume{get;set;}
        public double Furnace_nose_outlet_height {get;set;}
        public double Furnace_nose_downward_inclination_angleheight{get;set;}
        public double Height_furnace_top{get;set;}
        public double Furnace_top_volume1{get;set;}
        public double Furnace_top_volume2{get;set;}
        public double Furnace_top_volume {get;set;}
        public double Main_body_volume {get;set;}
        public double Roof_tube_spacing{get;set;}
        public double Heating_area_roof_tubes_Upperfurnace{get;set;}
        public double Halfdry_bottom_hopperarea{get;set;}
        public double Noseregion_side_wallarea{get;set;}
        public double Upperfurnace_side_wallarea{get;set;}
        public double Rearwall_area_noseregion{get;set;}      
        public double Exitwindowarea_lowerfurnace{get;set;}
        public double Lowerfurnace_enclosedarea{get;set;}
        public double Lowerfurnace_EPRS_area{get;set;}
        public double Lowerfurnace_volume{get;set;}
        public double Thickness_effective_radiationlayer_lowerfurnace{get;set;}
        public double Upperfurnace_absorbingarea{get;set;}
        public double Upperfurnace_exitwindowarea{get;set;}
        public double Thickness_effective_radiation_upperfurnace { get; set; }


    }
}
