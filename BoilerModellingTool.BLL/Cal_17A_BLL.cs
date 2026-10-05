using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;
using System.Data;

namespace BoilerModellingTool.BLL
{
    public class Cal_17A_BLL
    {
        public Cal_17A_SC Get_17A_CalculationInput(string ProjecID,string BoilerID,string BoilerLoad,int ObjectiveID,string SHSectionID,string GroupIDSH,string RHSectionID,string GroupIDRH)
        {
            Cal_17A_SC mCal_17_SC = new Cal_17A_SC();
            Cal_17A_DAL mCal_17A_DAL = new Cal_17A_DAL();
            mCal_17_SC = mCal_17A_DAL.Get_InputFor_17A_Calculation(ProjecID, BoilerID, BoilerLoad, ObjectiveID, SHSectionID, GroupIDSH, RHSectionID, GroupIDRH);
            return mCal_17_SC;
        }

        public Cal_17A_SC Get_FlowFraction_From_HC(string BoilerLoad)
        {
            Cal_17A_SC mCal_17_SC = new Cal_17A_SC();
            Cal_17A_DAL mCal_17A_DAL = new Cal_17A_DAL();
            mCal_17_SC = mCal_17A_DAL.Get_FlowFraction_From_HC(BoilerLoad);
            return mCal_17_SC;
        }

        public Cal_17A_SC Calculate_Enthalpy(string ProjectID,string BoilerID,double a,double b,double c,string BoilerLoad,int ObjectiveID)
        {
            Cal_17A_SC mCal_17_SC = new Cal_17A_SC();
            Cal_17A_DAL mCal_17A_DAL = new Cal_17A_DAL();
            mCal_17_SC = mCal_17A_DAL.Calculate_Enthalpy(ProjectID, BoilerID, a, b, c, BoilerLoad,ObjectiveID);
            return mCal_17_SC;
        }

        public string[,] CalcuateNumberOfHeatingSectionAfterCrossDuct(string ProjectID,string BoilerID,int RHSteamTempControl)
        {
            string[,] SectionInfo=new string[10,2];
            Cal_17A_DAL mCal_17A_DAL = new Cal_17A_DAL();
            SectionInfo = mCal_17A_DAL.CalculateNumberOfHeatingSection(ProjectID, BoilerID, RHSteamTempControl);
            return SectionInfo;
        }

        public int NumberOfHeatingSection(string ProjectID,string BoilerID,int RHSteamTempControl)
        {
            int d;
            Cal_17A_DAL mCal_17A_DAL = new Cal_17A_DAL();
            d = mCal_17A_DAL.HeatingElementAfterCross(ProjectID, BoilerID, RHSteamTempControl);
            return d;
        }

        public void Insert_17A_Calcuation(string ProjectID,string BoilerID,string SectionType,string SectionID,Double Value,int ObjectiveID,string BoilerLoad)
        {
            Cal_17A_DAL mCal_17A_DAL = new Cal_17A_DAL();
            mCal_17A_DAL.Insert_17A_Calculation(ProjectID, BoilerID, SectionType, SectionID, Value, ObjectiveID, BoilerLoad);
        }
    }
}
