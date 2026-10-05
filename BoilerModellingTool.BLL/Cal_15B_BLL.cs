using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;

namespace BoilerModellingTool.BLL
{
    public class Cal_15B_BLL
    {

        public Cal_15B_SC Get_Input_For_15B_Calculation(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID, string PreviousSectionID, string PreviousLocation, string GroupID,string PreviousHeatingSectionID,int PendantRH)
        {
            Cal_15B_DAL mCal_15B_DAL = null;
            Cal_15B_SC mCal_15B_SC = new Cal_15B_SC();
            DataSet mDTable = null;

            mCal_15B_DAL = new Cal_15B_DAL();
            mDTable = new DataSet();

            mCal_15B_SC = mCal_15B_DAL.GetInput_For_15B_Calculation(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, PreviousSectionID, PreviousLocation, GroupID, PreviousHeatingSectionID, PendantRH);
            return mCal_15B_SC;

        }

        public Double Get_2A_PrimaryAir_ReqEnthalpy(string ProjectID, string BoilerID,string BoilerLoad, int ObjectiveID, string InputVal)
        {
            Cal_15B_DAL mCal_15B_DAL = null;
            Double ValueTemp;

            mCal_15B_DAL = new Cal_15B_DAL();
            //mDTable = new DataSet();

            ValueTemp = mCal_15B_DAL.Get_2A_PrimaryAir_ReqEnthalpy(ProjectID, BoilerID, BoilerLoad, ObjectiveID, InputVal);
            return ValueTemp;
        }

        public void Insert_15B_Calculation(string ProjectID, string BoilerID, string Boiler_Load, int ObjectiveID, int PID, string SectionID, double Value)
        {
            Cal_15B_DAL mCal_15B_DAL = null;
            mCal_15B_DAL = new Cal_15B_DAL();
            mCal_15B_DAL.Insert_15B_Calculation(ProjectID, BoilerID, Boiler_Load, ObjectiveID, PID, SectionID, Value);
        }
    
        public DataTable Get_PID_For_15B_Calculation()
        {
            DataTable dt = new DataTable();
            Cal_15B_DAL mCal_15B_DAL = new Cal_15B_DAL();
            dt = mCal_15B_DAL.Get_PID_For_15B_Calculation();
            return dt;
        }

        public void Update_S_ParameterValue(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID, double Input, double Output, int Iteration,string NextSectionID)
        {
            Cal_GenericB_DAL mCal_GenericB_DAL = new Cal_GenericB_DAL();
            mCal_GenericB_DAL.Update_S_ParameterValue(ProjectID, BoilerID, BoilerLoad, ObjectiveID, Input, Output, SectionID, Iteration,NextSectionID);
        }

    }
}
