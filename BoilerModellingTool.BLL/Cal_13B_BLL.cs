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
    public class Cal_13B_BLL
    {
        public Cal_13B_SC Get_13B_CalculationInput(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID_F_Parameter, string SectionID_S_Parameter, int SteamCooledWall, int SteamScreenPresent, string SectionID_SteamCooledID, string SectionID_SteamScreenID, int Iteration)
        {
            Cal_13B_DAL mCal_13B_DAL = new Cal_13B_DAL();
            Cal_13B_SC mCal_13B_SC = new Cal_13B_SC();
            mCal_13B_SC = mCal_13B_DAL.Get_13B_Calculation_Values(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID_F_Parameter, SectionID_S_Parameter, SteamCooledWall, SteamScreenPresent, SectionID_SteamCooledID, SectionID_SteamScreenID, Iteration);
            return mCal_13B_SC;
        }

        public DataTable Get_PID_For_13B_Calculation()
        {
            DataTable dt = new DataTable();
            Cal_13B_DAL mCal_13B_DAL = new Cal_13B_DAL();
            dt=mCal_13B_DAL.Get_PID_For_13B_Calculation();
            return dt;
        }

        public void Insert_13B_Calculation(int PID,string ProjectID,string BoilerID,double Value,string BoilerLoad,int ObjectiveID)
        {
            Cal_13B_DAL mCal_13B_DAL = new Cal_13B_DAL();
            mCal_13B_DAL.Insert_13B_Calculation(PID, ProjectID, BoilerID, Value, BoilerLoad, ObjectiveID);
        }

        public double Get_Temperauter_From_Enthalpy(string ProjectID,string BoilerID,double Value,string BoilerLoad,int ObjectiveID)
        {
            Cal_13B_DAL mCal_13B_DAL = new Cal_13B_DAL();
            double Val = 0;
            Val = mCal_13B_DAL.Get_Temperature_From_Ethalpy(ProjectID, BoilerID, Value, BoilerLoad, ObjectiveID);
            return Val;
        }

        public double SteamTempFirstSuperheater(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID)
        {
            double p=0;
            Cal_13B_DAL mCal_13B_DAL = new Cal_13B_DAL();
            p=mCal_13B_DAL.SteamTempFirstSuperheater(ProjectID,BoilerID,BoilerLoad,ObjectiveID);
            return p;
        }

        public void Update_First_Superheater(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID, double Value1, double Value2, double Value3, int Iteration, string SteamScreenID)
        {
            Cal_13B_DAL mCal_13B_DAL = new Cal_13B_DAL();
            mCal_13B_DAL.Update_First_Superheater(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Value1, Value2, Value3, Iteration, SteamScreenID);
        }

    }
}
