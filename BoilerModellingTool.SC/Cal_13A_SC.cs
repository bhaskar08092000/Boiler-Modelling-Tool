using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.SC
{
    public class Cal_13A_SC
    {
        public double Tube_diameter { get; set; }
        public double Tube_thickness { get; set; }
        public double Reversing_chamber_height { get; set; }
        public double Reversing_chamber_depth { get; set; }
        public double Reversing_chamber_width { get; set; }
        public double Reversing_chamber_configuration_factor { get; set; }
        public double Total_number_of_eco_Hanger_tube_in_zone { get; set; }
        public double Eco_Hanger_diameter { get; set; }
        public double Eco_Hanger_tube_length_in_reverse_chamber_zone { get; set; }


        public double Total_side_wall_area {get;set;}
        public double Rear_wall_area { get; set; }
        public double Roof_area { get; set; }
        public double Total_heating_area { get; set; }
        public double Radiation_heating_area { get; set; }
        public double Reversing_chamber_peripheral_area { get; set; }
        public double Reversing_chamber_volume { get; set; }
        public double Effective_radiation_layer_thickness { get; set; }
        public double Eco_hanger_heating_area_in_zone { get; set; }
        public double Eco_Hanger_circumferential_area_in_zone { get; set; }

    }
}
