using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.SC
{
   public class Cal_3A_SC
    {

       //Variable for 3A Calculation

            public double Higher_heating_value { get; set; }
            public double Lower_heating_value { get; set; }
            public double Unburnt_carbon_loss { get; set; }
            public double Heat_loss_due_to_furnace_wall_radiation_and_convection { get; set; }
            public double Dry_bottom_hopper_outlet_depth { get; set; }
            public double Furnace_depth { get; set; }
            public double Flow_of_superheated_steam_at_FSH_outlet { get; set; }
            public double Flow_of_reheated_steam_at_Final_RH_outlet { get; set; }
            public double Fuel_firing_rate { get; set; }
            public double Furnace_width { get; set; }

            public double Heat_loss_due_unburnt_carbon{ get; set; }
            public double Heat_loss_due_incomplete_combustion{ get; set; }
            public double Heat_loss_due_furnace_radiation_convection{ get; set; }
            public double Physical_heat_loss_of_ash{ get; set; }
            public double Heat_preservation_coefficient{ get; set; }
            public double Flowrate_superheatedsteam{ get; set; }
            public double Flowrate_reheatedsteam{ get; set; }
            public double Design_fuel_consumption{ get; set; }

            //Variable for RH Flow

            public double BFP_inlet_flow_rate { get; set; }
            public double RH_desuperheating_spray { get; set; }
            public double SH_Desuperheating_spray_stage_1 { get; set; }
            public double SH_Desuperheating_spray_stage_2 { get; set; }
            public double Feed_water_flow_rate_through_HP_heater { get; set; }
            public double Main_steam_flow_rate { get; set; }

    }
}
