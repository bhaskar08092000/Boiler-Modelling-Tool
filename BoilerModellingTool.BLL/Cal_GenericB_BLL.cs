using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;
using System.Data;
using System.Data.SqlClient;

namespace BoilerModellingTool.BLL
{
    public class Cal_GenericB_BLL
    {

        public Cal_GenericB_SC Get_InputFor_GenericB(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID, string SectionType, string PreviousSectionID, string Location, int CurrentLocationID, int PreviousLocationID, string GroupID1, string GroupID2, int CoolingMedium, int Iteration, string PreviousHeatingElementID, string PreviousSectionType)
        {
            Cal_GenericB_DAL mCal_GenericB_DAL = new Cal_GenericB_DAL();
            Cal_GenericB_SC mCal_GenericB_SC = new Cal_GenericB_SC();
            mCal_GenericB_SC = mCal_GenericB_DAL.Get_InputFor_GenericB(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, SectionType, PreviousSectionID, Location, CurrentLocationID, PreviousLocationID, GroupID1, GroupID2, CoolingMedium, Iteration, PreviousHeatingElementID,PreviousSectionType);
            return mCal_GenericB_SC;
        }
    
        public double Get_Required_Temperature(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,double Value)
        {
            double Result = 0;
            Cal_GenericB_DAL mCal_GenericB_DAL = new Cal_GenericB_DAL();
            Result=mCal_GenericB_DAL.Get_Temperature_From_Ethalpy(ProjectID, BoilerID, Value, BoilerLoad, ObjectiveID);
            return Result;
        }
    
        public DataTable Get_PID_For_GenericB_Calculation()
        {
            DataTable dt = new DataTable();
            Cal_GenericB_DAL mCal_GenericB_DAL = new Cal_GenericB_DAL();
            dt = mCal_GenericB_DAL.Get_PID_For_GenericB_Calculation();
            return dt;
        }

        public void Insert_GenericB_Calculation(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,string SectionID,string Location,int PID,double Value,string SectionType)
        {
            Cal_GenericB_DAL mCal_GenericB_DAL = new Cal_GenericB_DAL();
            mCal_GenericB_DAL.Insert_GenericB_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, PID, SectionID, Value, Location,SectionType);
        }
        
        public void Update_S_ParameterValue(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,string SectionID,double Input,double Output,int Iteration,string NextSectionID)
        {
            Cal_GenericB_DAL mCal_GenericB_DAL = new Cal_GenericB_DAL();
            mCal_GenericB_DAL.Update_S_ParameterValue(ProjectID, BoilerID, BoilerLoad, ObjectiveID, Input, Output, SectionID, Iteration,NextSectionID);
        }

        public string Get_SteamedCooledWall_SectionID(string ProjectID,string BoilerID)
        {
            string SectionID = "";
            Cal_GenericB_DAL mCal_GenericB_DAL = new Cal_GenericB_DAL();
            SectionID = mCal_GenericB_DAL.Get_SteamCooledWall_SectionID(ProjectID, BoilerID);
            return SectionID;
        }

    }
}
