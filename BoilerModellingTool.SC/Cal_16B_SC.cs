using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.SC
{
    public class Cal_16B_SC
    {


public double Flue_gas_flow_rate_at_primary_APH_inlet {get;set;}
public double Flue_gas_flow_rate_at_primary_APHoutlet {get;set;}
public double Flue_gas_flow_rate_at_secondary_APH_inlet {get;set;}
public double Flue_gas_flow_rate_at_secondary_APH_outlet {get;set;}
public double Primary_air_flow_rate_at_inlet {get;set;}
public double Secondary_air_flow_rate_at_inlet {get;set;}
public double Primary_air_flow_rate_at_outlet {get;set;}
public double Secondary_air_flow_rate_at_outlet {get;set;}
public double Primary_APH_Flue_gas_inlet_temp {get;set;}
public double Primary_APH_Flue_gas_outlet__temp_without_leakage {get;set;}
public double Primary_Flue_gas_outlet__temp_with_leakage {get;set;}
public double Secondary_APH_Flue_gas_inlet_temp {get;set;}
public double Secondary_APH_Flue_gas_outlet__temp_without_leakage {get;set;}
public double Secondary_Flue_gas_outlet__temp_with_leakage {get;set;}
public double Primary_air_inlet_temp {get;set;}
public double Secondary_air_inlet_temp {get;set;}
public double Primary_air_outlet_temp {get;set;}
public double Secondary_air_outlet_temp { get; set; }
public double Inlet_primary_air_enthalpy { get; set; }
public double Inlet_secondary_air_enthalpy { get; set; }
public double Inlet_fluegas_enthalpy_of_primary_APH { get; set; }
public double Inlet_fluegas_enthalpy_of_secondary_APH { get; set; }
public double Outlet_primary_air_enthalpy { get; set; }
public double Outlet_secondary_air_enthalpy { get; set; }
public double Primary_APH_Oulet_fluegas_enthalpy { get; set; }
public double Secondary_APH_Oulet_fluegas_enthalpy { get; set; }
public double Primary_APH__Leakage_air_enthalpy_at_flue_gas_outlet_temp { get; set; }
public double Primary_APH_Leakage_air_enthalpy_at_air_inlet_temp { get; set; }
public double Secondary__APH_Leakage_air_enthalpy_at_flue_gas_outlet_temp { get; set; }
public double Seconday__APH_Leakage_air_enthalpy_at_air_inlet_temp { get; set; }
public double O2_in_flue_gas_at_primary_APH_inlet { get; set; }
public double O2_in_flue_gas_at_Primary_APH_outlet_dry_basis { get; set; }
public double O2_in_flue_gas_at_Secondary_APH_inlet { get; set; }
public double O2_in_flue_gas_at_Secondary_APH_outlet_dry_basis { get; set; }
public double Primary_APH_Mean_specific_heat_of_fluegas_at_flue_gas_outlet_temp_kJ_kgK { get; set; }
public double Secondary_APH__Mean_specific_heat_of_fluegas_at_flue_gas_outlet_temp_kJ_kgK { get; set; }
public double APH_design_capcity_ratio { get; set; }
public double Heat_balance_error_in_APH { get; set; }


public double Mean_specific_heat_of_air_btw_air_inlet_and_flue_gas_outlet_temp_kJ_kgK { get; set; }
public double Mean_specific_heat_of_air_btw_air_inlet_and_flue_gas_outlet_temp_kJ_kgK_2 { get; set; }

public double Flue_gas_outlet__temp_without_leakage_calculated { get; set; }
public double Gas_drop_without_leakage { get; set; }
public double Gas_drop_without_leakage_2 { get; set; }
public double Heat_capcity_ratio { get; set; }
public double Heat_capcity_ratio_2 { get; set; }
public double Flue_gas_outlet__temp_without_leakage_calculated_2 { get; set; }

public double Fluegas_side_leakage { get; set; }
public double Fluegas_side_leakage_2 { get; set; }
public double Primary_air_side_leakage { get; set; }
public double Air_temperature_at_inlet { get; set; }
public double Air_temperature_at_outlet { get; set; }

public double Air_leakage_percentage_in_operating_case { get; set; }
public double Flue_gas_flow_rate_at_APH_outlet_calculated { get; set; }
public double Flue_gas_flow_rate_at_APH_outlet_calculated_2 { get; set; }



public double Leakage_air_enthalpy_at_flue_gas_outlet_temp { get; set; }
public double Leakage_air_enthalpy_at_air_inlet_temp { get; set; }
public double Air_side_temp_rise { get; set; }
public double Air_side_temp_rise_2 { get; set; }
public double Temp_head { get; set; }
public double Temp_head_2 { get; set; }

public double Air_side_efficiency { get; set; }
public double Air_side_efficiency_2 { get; set; }
public double Gas_side_efficiency { get; set; }
public double Gas_side_efficiency_2 { get; set; }
public double Gas_side_efficiency_corrected_for_change_In_X_from_design { get; set; }
public double Gas_side_efficiency_corrected_for_change_In_X_from_design_2 { get; set; }

public double Secondary_air_side_leakage { get; set; }


public double Air_temperature_at_inlet_2 { get; set; }
public double Air_temperature_at_outlet_2 { get; set; }

public double Air_leakage_percentage_in_operating_case_2 { get; set; }



    }
}
