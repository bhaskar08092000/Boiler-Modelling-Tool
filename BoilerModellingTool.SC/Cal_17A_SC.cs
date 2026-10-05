using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.SC
{
    public class Cal_17A_SC
    {
        //Input
        public double Heating_Element_outlet_temperature_in_SH_side {get;set;}
        public double Enthalpy_of_flue_gas_out_of_Heating_Element_in_SH_side {get;set;} 
        public double Heating_Element_outlet_temperature_in_RH_side {get;set;}
        public double Enthalpy_of_flue_gas_out_of_Heating_Element_in_RH_side {get;set;}
        public double APH_inlet_temperature {get;set;}
        public double Enthalpy_of_flue_gas_inlet_of_APH { get; set; }

        //Output
        public double Assumed_flow_fraction_through_LTSH_side{ get; set; }
        public double Calculated_enthalpy_of_flue_gas_at_APH_inlet{ get; set; }
        public double Deviation_Percentage_of_enthalpy_wrt_design_APH_inlet_enthalpy { get; set; }

        public double HC_flow_fraction_through_HeatingElement { get; set; }

    }
}
