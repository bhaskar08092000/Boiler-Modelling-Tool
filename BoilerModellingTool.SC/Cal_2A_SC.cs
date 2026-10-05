using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BoilerModellingTool.SC
{
   public class Cal_2A_SC
    {
        public string Theoretical_volume_of_CO2_and_SO2 { get; set; }
        public string Theoretical_volume_of_N2 { get; set; }	
        public string Theoretical_volume_of_H2O	{ get; set; }	
        public string Fly_ash_concentration     { get; set; }		
        public string Theoretical_volume_of_air	{ get; set; }	
        public string Excess_air_ratio { get; set; }		
        public string Fuel_firing_rate	{ get; set; }	
        public string Flue_gas_flow_rate { get; set; }	
        public string Theoretical_air_required	{ get; set; }

        public string EnthalpyID { get; set; }
        public string IRO2 { get; set; }	
        public string IN2 { get; set; }	
        public string IH2O { get; set; }	
        public string Ifa { get; set; }	
        public string Iog { get; set; }	
        public string Ioa { get; set; }

        //public string NewAlpha { get; set; }

        public string NewAlpha { get; set; }

        public string LowerFurnaceAlpha { get; set; }	
        public string UpperFurnaceAlpha { get; set; }	
        public string CrossDuctAlpha  { get; set; }	
        public string ReverseChamberAlpha { get; set; }
        public string BackpassAlpha { get; set; }	

        public string  APH_Flue_gas_Enthalpy { get; set; }	
        public string APH_Air_Enthalpy { get; set; }	
        public string Cp_flue_gas { get; set; }	
        public string Cp_air { get; set; }	


      
    }
}
