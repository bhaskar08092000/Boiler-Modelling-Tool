using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.DAL
{
    public class StoredProcedure
    {
        public const string DBName = "constr";

        public const string spr_SignUp = "[dbo].[SP_InsertSignUp]";

        public const string spr_LogIN = "[dbo].[spr_LogIn]";

        public const string spr_Insert_EnthalpyTableForTempZeroCalculatedValue = "[dbo].[spr_Insert_EnthalpyTableForTempZeroCalculatedValue]";

        public const string spr_Get_Nearest_Max_Min_EnthalpyCalculation = "[dbo].[spr_Get_Nearest_Max_Min_EnthalpyCalculation]";

        public const string spr_GetInput_For_25_Enthalpy_Calculation = "[dbo].[spr_GetInput_For_25_Enthalpy_Calculation]";

        public const string spr_Insert_EnthalpyTableForTemp25CalculatedValue = "[dbo].[spr_Insert_EnthalpyTableForTemp25CalculatedValue]";

        public const string spr_Update_CP_Flue_AND_Air_Gas = "[dbo].[spr_Update_CP_Flue_AND_Air_Gas]";

        public const string spr_select_APH_Calculation = "[dbo].[spr_select_APH_Calculation]";


        //Project and Boiler Information

        public const string spr_InsertUpdateBoilerInformation = "dbo.spr_InsertUpdateBoilerInformation";

        public const string spr_GetBoilerNumber = "dbo.spr_GetBoilerNumber";

        public const string spr_ProjectInfo = "[dbo].[spr_InsertUpdateProjectInformation]";

        public const string spr_GetProjectInformation = "dbo.spr_GetProjectInformation";

        public const string spr_GetBoilerInformation = "dbo.spr_GetBoilerInformation";

        public const string spr_GetUpperFurnaceInformation = "dbo.spr_GetUpperFurnaceInformation";

        public const string spr_GetCrossDuctInformation = "dbo.spr_GetCrossDuctInformation";

        public const string spr_GetReverseChamberInformation = "dbo.spr_GetReverseChamberInformation";

        public const string spr_GetBackpassInformation = "dbo.spr_GetBackpassInformation";

        public const string spr_GetBackpassInformationSH = "dbo.spr_GetBackpassInformationSH";

        public const string spr_GetBackpassInformationRH = "dbo.spr_GetBackpassInformationRH";

        public const string spr_GetMiscellaneousInformation = "dbo.spr_GetMiscellaneousInformationData";

        public const string spr_GetMiscellaneousInformationRH = "spr_GetMiscellaneousInformationRH";

        public const string spr_GetMiscellaneousInformationSH = "spr_GetMiscellaneousInformationSH";
                

        public const string spr_Tsat_p = "[dbo].[Tsat_p]";

        
        //1A Calculations
        public const string spr_GetFuelParameterForUltimate = "[dbo].[spr_GetFuelParameterForUltimate]";

        public const string spr_GetAirTempParameterForSpecificHumidity = "[dbo].[spr_GetAirTempParameterForSpecificHumidity]";

        public const string spr_Get_PI_for_1A = "[dbo].[spr_Get_PI_for_1A]";

        public const string spr_Insert1AValues = "[dbo].[spr_Insert1AValues]";

        public const string spr_Get_1A_Parameters = "[dbo].[spr_Get_1A_Parameters]";

        public const string spr_Get1A_EA_Parameters = "[dbo].[spr_Get1A_EA_Parameters]";

        public const string spr_GetExcessAirCheck = "[dbo].[spr_GetExcessAirCheck]";

        public const string spr_Get_Objective = "[dbo].[ spr_Get_Objective]";

        public const string spr_Insert1AValues_5_parameter = "[dbo].[spr_Insert1AValues_5_parameter]";


        //2A Calculation

        public const string spr_2A_PrimaryAir_ReqEnthalpy = "[dbo].[spr_2A_SecondaryAir_ReqEnthalpy]";

        public const string spr_2A_AdiabaticTemp_ReqTemp = "[dbo].[spr_2A_AdiabaticTemp_ReqTemp]";

        public const string spr_2A_FEGT_ReqEnthalpy = "[dbo].[spr_2A_FEGT_ReqEnthalpy]";

        public const string spr_2A_Platen_Exit_Temp_ReqTemp = "[dbo].[spr_2A_Platen_Exit_Temp_ReqTemp]";

        public const string spr_GetInput_For_2A_Calculation = "[dbo].[spr_GetInput_For_2A_Calculation]";

        public const string spr_GetCCO2θ_For_2A_Calculation = "[dbo].[spr_GetCCO2θ_For_2A_Calculation]";

        public const string spr_2A_Front_RH_Exit_ReqTemp = "[dbo].[spr_2A_Front_RH_Exit_ReqTemp]";

        public const string spr_2A_TemperingAir_ReqEnthalpy = "spr_2A_Slag_screen_exit_DESIGN_ReqEnthalpy";



        //3A and RH flow Calculation

        public const string spr_GetInput_For_3A_Calculation = "[dbo].[spr_GetInput_For_3A_Calculation]";

        public const string spr_Insert_3A_Calculation = "[dbo].[spr_Insert_3A_Calculation]";

        public const string spr_RHFlow_getInputParameters = "dbo.spr_RHFlow_getInputParameters";

        public const string spr_EA_getUnburntCarbonLoss = "[dbo].[spr_EA_getUnburntCarbonLoss]";

        public const string spr_RHFlow_getHPHeatersCount = "[dbo].[spr_RHFlow_getHPHeatersCount]";

        public const string spr_BindHPHeaters = "[dbo].[spr_BindHPHeaters]";

        public const string spr_RHFlow_getHPHeatersParameters = "[dbo].[spr_RHFlow_getHPHeatersParameters]";



        //4A Calculation

        public const string spr_GetInput_For_4A_Calculation = "[dbo].[spr_GetInput_For_4A_Calculation]";

        public const string spr_Insert_4A_Calculation = "[dbo].[spr_Insert_4A_Calculation]";

        //4B Calculation

        public const string spr_GetInput_For_4B_Calculation = "[dbo].[spr_GetInput_For_4B_Calculation]";

        public const string spr_Get_avg_loc_vert_dir_4B_Cal = "[dbo].[spr_Get_avg_loc_vert_dir_4B_Cal]";

        public const string spr_Get_PID_For_4B_Calculation = "[dbo].[spr_Get_4B_Parameters]";

        public const string spr_Get_submodule_heating_section_in_upper_furnace = "[dbo].[spr_Get_submodule_heating_section_in_upper_furnace]";

        public const string spr_Get_Design_Area_From_Section_ID_in_4B = "[dbo].[spr_Get_Design_Area_From_Section_ID_in_4B]";

        public const string spr_Get_Recirculated_gas_temperature = "[dbo].[spr_Get_Recirculated_gas_temperature]";

        public const string spr_Insert_4B_Calculation = "[dbo].[spr_Insert_4B_Calculation]";


        //5A Calculations
        public const string spr_Insert_5A_Calculation = "[dbo].[spr_Insert_5A_Calculation]";

        public const string spr_Get_PID_For_5A_Calculation = "[dbo].[spr_Get_5A_Parameters]";

        public const string spr_GetInput_For_5A_Calculation = "[dbo].[spr_GetInput_For_5A_Calculation]";

        //5B Calculations
        public const string spr_Insert_5B_Calculation = "[dbo].[spr_Insert_5B_Calculation]";

        public const string spr_S_parameter_HeatinElement_Sequence = "[dbo].[spr_S_parameter_HeatinElement_Sequence]";

        public const string spr_Get_S_Parameter_Section_ID = "[dbo].[spr_Get_S_Parameter_Section_ID]";

        public const string spr_InsertUpadate_5B_S_ParameterValue = "[dbo].[spr_InsertUpadate_5B_S_ParameterValue]";

        public const string spr_Get_De_superheating_spray_Next_HE_Type = "[dbo].[spr_Get_De-superheating spray _Next_HE_Type]";

        public const string spr_GetInput_For_5B_Calculation = "[dbo].[spr_Get_Input_for_5B]";

        public const string spr_Get_PID_For_5B_Calculation = "[dbo].[spr_Get_5B_Parameters]";

        //S parameter     
        public const string spr_Get_S_parameter = "[dbo].[get_S_parameterData]";

        public const string spr_Get_data_for_S_parameter = "[dbo].[spr_Get_for_S_parameter]";

        public const string spr_Insert_S_Parameter_InitialValues = "spr_InsertUpdateSParameter_InitialValues";

        public const string spr_Get_S_Parameter_InitialValue = "spr_Get_Initial_S_ParameterDataValues";

        public const string spr_Insert_S_parameter = "[dbo].[spr_Insert_S_parameter_values]";

        public const string spr_Get_S_parameter_count = "[dbo].[spr_Get_S_para_Count]";

        public const string spr_Update_S_parameter = "[dbo].[spr_Update_S_parameter_values]";

        public const string spr_Get_Iteration_Data = "spr_Get_Iteration_Data";

        public const string spr_Insert_S_Parameter_Values = "spr_S_Parameter_Next_Iteration_Data";

        public const string spr_Get_IterationWiseData="spr_Get_Iteration_Wise_Data";

        public const string spr_Delete_Iteration_Data = "spr_Delete_Iteration_Data";

        //Default PAge For Looping 

        public const string spr_get_S_parameter_Details_Iteration = "[dbo].[get_S_parameter_Details_Iteration]";

        public const string spr_get_Iteration_of_Heating_element = "[dbo].[get_Iteration_of_Heating_element]";

        public const string spr_delete_Iteration_ID = "[dbo].[delete_Iteration_ID]";


        //6A Calculations
        public const string spr_Insert_6A_Calculation = "[dbo].[spr_Insert_6A_Calculation]";

        //public const string spr_Insert_6A_Calculation = "[dbo].[spr_Insert_6A_Calculation]";
        public const string spr_GetInput_For_6A_Calculation = "[dbo].[spr_GetInput_For_6A_CalculationNew]";

        public const string spr_Get6A_parameter = "[dbo].[spr_Get_6A_Parameters]";

        //6B Calculations
        public const string spr_Insert_6B_Calculation = "[dbo].[spr_Insert_6B_Calculation]";

        public const string spr_Get_PID_For_6B_Calculation = "[dbo].[spr_Get_6B_Parameters]";

        public const string spr_GetInput_For_6B_Calculation = "[dbo].[spr_GetInput_For_6B_Calculation]";



        //7A Calculation

        public const string spr_GetInput_For_7A_Calculation = "[dbo].[spr_GetInput_For_7A_Calculation]";

        public const string spr_Insert_7A_Calculation = "[dbo].[spr_Insert_7A_Calculation]";


        //8A Calculations
        public const string spr_GetInput_For_8A_Calculation = "[dbo].[spr_GetInput_For_8A_Calculation]";

        public const string spr_Insert_8A_Calculation = "[dbo].[spr_Insert_8A_Calculation]";



        //9A Calculation
        public const string spr_GetInput_For_9A_Calculation = "[dbo].[spr_GetInput_For_9A_Calculation]";

        public const string spr_Insert_9A_Calculation = "[dbo].[spr_Insert_9A_Calculation]";


        //10A Calculation
        public const string spr_GetInput_For_10A_Calculation = "[dbo].[spr_GetInput_For_10A_Calculation]";

        public const string spr_Insert_10A_Calculation = "[dbo].[spr_Insert_10A_Calculation]";


        //13A Calculation
        public const string spr_GetInut_For_13A_Calculation_ReverseChamber = "spr_GetInput_For_13A_Calculation_ReverseChamber";

        public const string spr_Insert_13A_Calculation = "dbo.spr_Insert_13A_Calculation";

        public const string spr_Get_PID_For_13A_Calculation = "spr_Get_13A_Parameters";

        //13B Calculation

        public const string spr_GetInput_For_13B_Calculation_ReverseChamber = "spr_GetInput_For_13B_Calculation";

        public const string spr_Insert_13B_Calculation = "spr_Insert_13B_Calculation";

        public const string spr_Get_PID_For_13B_Calculation = "spr_Get_13B_Parameters";

        public const string spr_GetInput_For_13B_Calculation_F_Parameter = "spr_GetInput_For_13B_Calculation_F_parameter";

        public const string spr_GetInput_For_13B_Calculation_S_Parameter = "spr_GetInput_For_13B_Calculation_S_parameter";

        public const string spr_GetInput_For_13B_Calculation_InletSteam = "spr_GetInput_For_13B_Calculation_InletSteamTemp";

        public const string spr_GetInput_For_First_Superheater = "spr_Get_13B_FirstSupheater_S_Parameter";

        public const string spr_Update_S_Paramter_First_Superheater = "spr_InsertUpadate_13B_S_ParameterValue";


        //Generic A Calculation

        public const string spr_GetInput_For_GenericA = "spr_GenericA_GetGeometricInputParameters";

        public const string spr_Get_PID_For_GenericA = "spr_Get_GenericA_Parameters";

        public const string spr_Insert_GenericA_Calculatioin = "spr_Insert_GenericA_Calculation";

        //17A Calculation

        public const string spr_GetInput_For_17A_Calculation = "spr_GetInput_For_17A_Calculation";

        public const string spr_GetInput_For_17A_CalculationHC = "spr_GetInput_For_17A_CalculationHC";

        public const string spr_17A_NumberOfHeatingElementAfterCrossDuct = "spr_GetCountOfHeatingElementsAfterCrossDuct";

        public const string spr_17A_HeatingElementAfterCrossDuct = "spr_GetHeatingElementsAfterCrossDuct";

        public const string spr_Insert_17A_Calculation = "spr_Insert_17A_Calculation";

        //Flow Module Calculation

        public const string spr_GetInput_For_Flow_Calculation = "spr_GetInput_For_Flow_Calculation";

        public const string spr_GetInput_For_Flow_BackpassRatioCalculation = "spr_GetInput_For_Flow_BackpassRationCalculation";

        public const string spr_Flow_Get_SectionID_from_Miscellneous = "spr_GetElementFromMiscellaneousSH";

        public const string spr_Flow_SectionNumber_Of_SectionID = "spr_Get_NumberOfSectionIDMiscellaneous";

        public const string spr_Flow_GetSectionID_Of_HeatingElement = "spr_Get_SectionIDOf_HeatingElements";

        public const string spr_Insert_Flow_Calculation_FixedParameters = "spr_Insert_Flow_Calculation_Fixed";

        public const string spr_Insert_Flow_Calculation_DynamicParamters = "spr_Insert_Flow_Calculation_Dynamic";

        public const string spr_Flow_GetInput_For_DesuperheatingSprayEnthalpy = "spr_Flow_Get_DesuperheatingSprayEnthalpy_Input";

        //Pressure Module Calculation

        public const string spr_GetInput_For_Pressure_Calculation = "[dbo].[spr_GetInput_For_Pressure_Calculation]";

        public const string spr_Insert_Pressure_Calculation = "[dbo].[spr_Insert_Pressure_Calculation]";

        public const string spr_Insert_Pressure_Calculation_FixedParameters = "[dbo].[spr_Insert_Pressure_Calculation_FixedParameters]";

        public const string spr_Get_CountOf_HeatingElements = "[dbo].[spr_Get_CountOf_HeatingElements]";

        public const string spr_CountOf_HeatingElement_LocationOfCrossDuct = "[dbo].[spr_CountOf_HeatingElement_LocationOfCrossDuct]";

        public const string spr_Get_SectionIDOf_CrossDuct = "[dbo].[spr_Get_SectionIDOf_CrossDuct]";

        public const string Get_OutputForFurnaceRoofCoolingMedium = "[dbo].[Get_OutputForFurnaceRoofCoolingMedium]";

        public const string Get_OutputForReheaterType = "[dbo].[Get_OutputForReheaterType]";

        public const string spr_GetLast_Element_of_HeatingSectionUpperFurnace = "[dbo].[spr_GetLast_Element_of_HeatingSectionUpperFurnace]";

        public const string Get_CountOfSteamScreenSection = "spr_GetSteamScreenSections";

        //public const string spr_S_parameter_HeatinElement_Sequence = "[dbo].[spr_S_parameter_HeatinElement_Sequence]";


        //Generic B Calculation

        public const string spr_GetInput_For_GenericB = "spr_GenericB_Get_Input";
       
        public const string spr_GetInput_For_GenericB_PressureParameters = "[dbo].[spr_GenericB_Get_Input_PessureParameters]";

        public const string spr_GetInput_For_GenericB_SideWallParameters = "spr_GenericB_Get_Input_SideWallParameters_New";

        public const string spr_GetInput_For_GenericB_S_Parameter = "spr_Get_S_ParameterValue";

        public const string spr_GetInput_For_GenericB_HC_Parameter = "spr_GenericB_Get_Input_HCParameter";

        public const string spr_GenericB_Calculate_Temperature = "spr_2A_Platen_Exit_Temp_ReqTemp";

        public const string spr_Get_PID_For_GenericB_Calculation = "spr_Get_GenericB_Parameters";

        public const string spr_Insert_GenericB_Calculation = "[dbo].[spr_Insert_GenericB_Calculation]";

        public const string spr_Update_S_Parameter_Value = "spr_S_Parameter_Update";

        public const string spr_Get_SteamedCooledWall_SectionID = "spr_Get_SectionID_ofSteamCooled_HeatingElement";


        //15a calculation
        public const string spr_GetInput_For_15A_Calculation = "[dbo].[spr_GetInput_For_15A_Calculation]";

        public const string spr_Insert_15A_Calculation = "[dbo].[spr_Insert_15A_Calculation]";
        

        //15B calculation
        public const string spr_GetInput_For_15B_Calculation = "[dbo].[spr_GetInput_For_15B_Calculation]";

        public const string spr_Insert_15B_Calculation = "[dbo].[spr_Insert_15B_Calculation]";

        public const string spr_Get_PID_For_15B_Calculation = "spr_Get_15B_Parameters";

        //Upper Furnace PArt B

        public const string spr_Get_Input_For_Upper_Furnace_Part_B = "[dbo].[spr_Get_Input_for_UpperFurnace_Part_B]";

        //16A
        public const string spr_GetInput_For_16A_Calculation = "[dbo].[spr_GetInput_For_16A_Calculation]";



        public const string spr_Insert_16A_Calculation = "[dbo].[spr_Insert_16A_Calculation]";



        public const string spr_16A_CAlculation_Flue_GAS_ReqEnthalpy = "[dbo].[spr_16A_CAlculation_Flue_GAS_ReqEnthalpy]";



        public const string spr_16A_CAlculation_ReqEnthalpy = "[dbo].[spr_16A_CAlculation_ReqEnthalpy]";



        public const string spr_16A_CAlculation_ReqEnthalpyforCPFlueGas = "[dbo].[spr_16A_CAlculation_ReqEnthalpyforCPFlueGas]";


        //16B

        public const string spr_GetInput_For_16B_Calculation = "[dbo].[spr_GetInput_For_16B_Calculation]";

        public const string spr_Insert_16B_Calculation = "[dbo].[spr_Insert_16B_Calculation]";

        public const string spr_Get_16B_Parameters = "[dbo].[spr_Get_16B_Parameters]";


        //Heat Absorption

        public const string spr_GetInput_For_HeatAbsorption_Calculation = "[dbo].[spr_GetInput_For_HeatAbsorption_Calculation]";

        public const string spr_GetValueOfCrossDuct = "[dbo].[spr_GetValueOfCrossDuct]";

        public const string spr_GetValueOfPresenceOfCooledWater = "[dbo].[spr_GetValueOfPresenceOfCooledWater]";

        public const string spr_Get_SectionIDOf_HeatingElements = "[dbo].[spr_Get_SectionIDOf_HeatingElements]";

        public const string spr_GenericB_Get_Value_For_Heater = "[dbo].[spr_GenericB_Get_ValueForSuperheater]";

        public const string spr_GenericB_Get_ValueForRHSteamTempControl = "[dbo].[spr_GenericB_Get_ValueForRHSteamTempControl]";

        public const string spr_GetValueForMedium = "[dbo].[spr_GetValueForMedium]";

        public const string spr_GetValueForEconomizerHangerTubePresentUpstream = "[dbo].[spr_GetValueForEconomizerHangerTubePresentUpstream]";

        public const string spr_GetValueForTypeOfBoiler = "[dbo].[spr_GetValueForTypeOfBoiler]";

        public const string spr_GetInputForUpperFurnace = "[dbo].[spr_GetInputForUpperFurnace]";

        public const string spr_GenericB_LocationWise_HeatingElement = "[dbo].[spr_GenericB_LocationWise_HeatingElement]";

        public const string spr_GetInputForReverseChamberForHeatAbsorption = "[dbo].[spr_GetInputForReverseChamberForHeatAbsorption]";

        public const string spr_GetValueFor_EconomizerHangerTubePresentUpstream = "[dbo].[spr_GetValueFor_EconomizerHangerTubePresentUpstream]";

        public const string spr_GenericB_Get_ValueForSuperheater_demo = "[dbo].[spr_GenericB_Get_ValueForSuperheater_demo]";

        public const string spr_Insert_HeatAbsorption_Calculation = "[dbo].[spr_Insert_HeatAbsorption_Calculation]";

        public const string spr_Get_SectionValueOf_HeatingElements = "[dbo].[spr_Get_SectionValueOf_HeatingElements]";


        //GI

        public const string spr_GetBackpassInformationForGI = "spr_GetBackpassInformationForGI";

        //5C Calculation 
        public const string spr_Get_Input_For_5C_Calculation_Of_Area = "[dbo].[spr_Get_Input_For_5C_Calculation_Of_Area]";
        public const string spr_Get_Input_For_5C_Calculation_Vertical_Section = "[dbo].[spr_Get_Input_For_5C_Calculation_Vertical_Section]";
        public const string spr_GetInput_For_5C_Calculation = "[dbo].[spr_GetInput_For_5C_A_Calculation]";
        public const string spr_Get_Input_For_5C_Ratio_DR_each_section = "[dbo].[spr_Get_Input_For_5C_Ratio_DR_each_section]";
        public const string spr_Get_Thermal_Conductivity_For_5C_Calculation_ = "[dbo].[spr_Get_Thermal_Conductivity_For_5C_Calculation_]";
        public const string spr_Get_MAterial_Section_For_5C_Calculation_ = "[dbo].[spr_Get_MAterial_Section_For_5C_Calculation_]";
        public const string spr_Insert_Metal_Temperature_For_Heating_Element = "[dbo].[spr_Insert_Metal_Temperature_For_Heating_Element]";
        public const string spr_Reaheter = "[dbo].[spr_GetInput_For_5C_Reheater]";
        public const string spr_GetInput_For_Metal_Temp_Calculation = "[dbo].[spr_GetInput_For_Metal_Temp_Calculation]";


    }
}
