using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;
using BoilerModellingTool.BLL;
using System.Data;
using System.Data.SqlClient;

namespace GenericClasses
{
    public class Class_17A_Calculation
    {
        public Cal_17A_SC Doc { get; set; }

        SqlConnection con;

        int NumberOfHeatingElemntsAfterCrossDuct=0;

        public void Calculation_For_17A(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,string SHSectionID,string GroupIDSH,string RHSectionID,string GroupIDRH,int RHSteamTempControl)
        {

            Cal_17A_BLL mCal_17A_BLL = new Cal_17A_BLL();

            string[,] SectionInfo;

            int d;

            if (RHSteamTempControl == 2)
            {

                this.Doc = mCal_17A_BLL.Get_17A_CalculationInput(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SHSectionID, GroupIDSH, RHSectionID, GroupIDRH);

                if (Doc.Heating_Element_outlet_temperature_in_SH_side == 0.0 || Doc.Heating_Element_outlet_temperature_in_RH_side == 0.0 || Doc.APH_inlet_temperature == 0.0)
                {
                    this.Doc = mCal_17A_BLL.Get_FlowFraction_From_HC(BoilerLoad);

                    d = mCal_17A_BLL.NumberOfHeatingSection(ProjectID, BoilerID, RHSteamTempControl);

                    SectionInfo = new string[d, 2];

                    SectionInfo = mCal_17A_BLL.CalcuateNumberOfHeatingSectionAfterCrossDuct(ProjectID, BoilerID, RHSteamTempControl);

                    for (int i = 0; i < d; i++)
                    {
                        mCal_17A_BLL.Insert_17A_Calcuation(ProjectID, BoilerID, SectionInfo[i, 0], SectionInfo[i, 1], Doc.Assumed_flow_fraction_through_LTSH_side, ObjectiveID, BoilerLoad);
                    }

                }
                else
                {
                    this.Doc = mCal_17A_BLL.Calculate_Enthalpy(ProjectID, BoilerID, Doc.Heating_Element_outlet_temperature_in_SH_side, Doc.Heating_Element_outlet_temperature_in_RH_side, Doc.APH_inlet_temperature,BoilerLoad,ObjectiveID);
                    
                    //Calculation
                    Doc.Assumed_flow_fraction_through_LTSH_side = 1.00;

                    do
                    {
                        Doc.Calculated_enthalpy_of_flue_gas_at_APH_inlet = Doc.Assumed_flow_fraction_through_LTSH_side * Doc.Enthalpy_of_flue_gas_out_of_Heating_Element_in_SH_side + (1 - Doc.Assumed_flow_fraction_through_LTSH_side) * Doc.Enthalpy_of_flue_gas_out_of_Heating_Element_in_RH_side;

                        Doc.Deviation_Percentage_of_enthalpy_wrt_design_APH_inlet_enthalpy = (Doc.Calculated_enthalpy_of_flue_gas_at_APH_inlet - Doc.Enthalpy_of_flue_gas_inlet_of_APH) * 100 / Doc.Enthalpy_of_flue_gas_inlet_of_APH;

                        Doc.Assumed_flow_fraction_through_LTSH_side = Doc.Assumed_flow_fraction_through_LTSH_side - 0.001;

                    } while (Math.Abs(Doc.Deviation_Percentage_of_enthalpy_wrt_design_APH_inlet_enthalpy) >= 0.05 || Doc.Deviation_Percentage_of_enthalpy_wrt_design_APH_inlet_enthalpy<0);

                    d = mCal_17A_BLL.NumberOfHeatingSection(ProjectID, BoilerID, RHSteamTempControl);

                    SectionInfo = new string[d, 2];

                    SectionInfo = mCal_17A_BLL.CalcuateNumberOfHeatingSectionAfterCrossDuct(ProjectID, BoilerID, RHSteamTempControl);
                
                    for(int i=0;i<d;i++)
                    {
                        if(SectionInfo[i,0]=="BackpassRH")
                        {
                            Double Val=1-Doc.Assumed_flow_fraction_through_LTSH_side;
                            mCal_17A_BLL.Insert_17A_Calcuation(ProjectID,BoilerID,SectionInfo[i,0],SectionInfo[i,1],Val,ObjectiveID,BoilerLoad);
                        }
                        else
                        {
                            mCal_17A_BLL.Insert_17A_Calcuation(ProjectID, BoilerID, SectionInfo[i, 0], SectionInfo[i, 1], Doc.Assumed_flow_fraction_through_LTSH_side, ObjectiveID, BoilerLoad);
                        }
                    }
                
                }
            }
            else
            {
                //this.Doc.Assumed_flow_fraction_through_LTSH_side = 1.0;

                d = mCal_17A_BLL.NumberOfHeatingSection(ProjectID, BoilerID, RHSteamTempControl);

                SectionInfo = new string[d, 2];

                SectionInfo = mCal_17A_BLL.CalcuateNumberOfHeatingSectionAfterCrossDuct(ProjectID, BoilerID, RHSteamTempControl);

                for(int i=0;i<d;i++)
                {
                    mCal_17A_BLL.Insert_17A_Calcuation(ProjectID, BoilerID, SectionInfo[i, 0], SectionInfo[i, 1], 1, ObjectiveID, BoilerLoad);
                }

            }

        }
    
    }
}
