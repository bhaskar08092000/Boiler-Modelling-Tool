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
    public class Cal_5B_BLL
    {
        public DataTable Get_PID_For_5B_Calculation()
        {
            Cal_5B_DAL mCal_5B_DAL = null;
            DataTable mDTable = null;

            mCal_5B_DAL = new Cal_5B_DAL();
            mDTable = new DataTable();

            mDTable = mCal_5B_DAL.Get_PID_For_5B_Calculation();
            return mDTable;

        }

        public DataSet Get_Input_For_5B_Calculation(string BolierID, string ProjectID,string BoilerLoad,string SectionID,int ObjectiveID)
        {
            Cal_5B_DAL mCal_5B_DAL = null;
            DataSet mDTable = null;

            mCal_5B_DAL = new Cal_5B_DAL();
            mDTable = new DataSet();

            mDTable = mCal_5B_DAL.Get_Input_For_5B_Calculation(BolierID, ProjectID, BoilerLoad, SectionID, ObjectiveID);
            return mDTable;

        }
        
        public void Insert_5B_Calculation(int pid, string BoilerID, string ProjectID ,string BoilerLoad,string Section_ID,int obje ,double Value)
        {

            Cal_5B_DAL mCal_5B_DAL = new Cal_5B_DAL();
            DataSet mDts = null;
            mDts = new DataSet();

            mDts = mCal_5B_DAL.Insert_5B_Calculation(pid, BoilerID, ProjectID, BoilerLoad,Section_ID,obje, Value);
        }
        
        public Double Get_De_superheating_spray_Next_HE_Type(string BoilerID, string ProjectID, string SectionID)
        {
            Cal_5B_DAL mCal_5B_DAL = new Cal_5B_DAL();
            Double mDts = 0.0;

            mDts = mCal_5B_DAL.Get_De_superheating_spray_Next_HE_Type(BoilerID, ProjectID, SectionID);

            return mDts;
        }
        
        public string Get_PreVious_HE_SectionID(string BoilerID, string ProjectID, string SectionID)
        {
            Cal_5B_DAL mCal_5B_DAL = new Cal_5B_DAL();
            string mDts = null;

            mDts = mCal_5B_DAL.Get_PreVious_HE_SectionID(BoilerID, ProjectID, SectionID);

            return mDts;
        }
        
        public DataSet Get_S_Parameter_Section_ID(string BoilerID, string ProjectID, string BoilerLoad,int ObjectiveID, string SectionID,int iteration)
        {

            Cal_5B_DAL mCal_5B_DAL = new Cal_5B_DAL();
            DataSet mDts = null;
            mDts = new DataSet();

            mDts = mCal_5B_DAL.Get_S_Parameter_Section_ID( BoilerID, ProjectID, BoilerLoad,ObjectiveID, SectionID, iteration);
            return mDts;
        }
        
        public double Get_2A_Platen_Exit_Temp_ReqTemp(string Boiler_ID, string Project_ID, string input_val, string Boiler_Load, int Objective_ID)
        {
            Cal_5B_DAL mCal_5B_DAL = null;
            Double mDTable;

            mCal_5B_DAL = new Cal_5B_DAL();
            //mDTable = new DataSet();

            mDTable = mCal_5B_DAL.Get_2A_Platen_Exit_Temp_ReqTemp(Boiler_ID, Project_ID, input_val, Boiler_Load, Objective_ID);
            return mDTable;
        }
        
        public void InsertUpadate_5B_S_ParameterValue(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID, double Input,
            double Output,int Iteration,string NextSectionID)
        {
            Cal_5B_DAL mCal_5B_DAL = new Cal_5B_DAL();
            DataSet mDts = null;
            mDts = new DataSet();

            mDts = mCal_5B_DAL.InsertUpadate_5B_S_ParameterValue(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Input, Output, Iteration, NextSectionID);
        }
    
    }
}

