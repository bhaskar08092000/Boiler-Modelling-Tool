using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.SC
{
    public class Cal_5C_SC
    {
        //5C-A
        public double No_of_loops_per_element { get; set; }
        public double Tube_diameter_of_platen_superheater { get; set; }
        public double Platen_superheater_tube_thickness { get; set; }
        public double Platen_superheater_height { get; set; }
        public double Platen_superheater_depth { get; set; }
        public double Platen_superheater_transverse_rows { get; set; }
        public double PSH_convective_area { get; set; }
        public double Relative_transverse_pitch { get; set; }

        public double Flow_rate_through_PSH { get; set; }
        public double Total_D_R_radiation_absorbed_in_SH { get; set; }
        public double convection_heat { get; set; }
        public double Design_fuel_consumption { get; set; }
        public double Enthalpy_of_steam_inlet_to_the_PSH { get; set; }
        public double Inlet_pressure_pf_steam { get; set; }
        public double Outlet_pressure { get; set; }
        public double Heat_transfer_coefficient_from_tube_wall_to_steam { get; set; }
        public double Steanm_outlet_temperature { get; set; }

        public double nose { get; set; }
        public double avg_location_vertical_direction { get; set; }
        public double Roof { get; set; }


        //5C-B

        //5C-C

        public double Fraction_of_flow_thorugh_outer__tube { get; set; }
        public double Factor_a_L_d5___Outer_loop { get; set; }
        public double Factor_b_L_d5___Inner_loop { get; set; }
        public double Error { get; set; }
        public double Flow_through_outer_loop { get; set; }
        public double Flow_through_each_inner__loop { get; set; }

        public double Pressure_drop_from_inlet_to_outlet { get; set; }
        public double Pressure_drop_per_unit_length_of_outer_tube { get; set; }
        public double Pressure_drop_per_unit_length_of_inner_tube { get; set; }


    }
}
