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
    public class Cal_6B_BLL
    {
        public DataTable Get_PID_For_6B_Calculation()
        {
            Cal_6B_DAL mCal_6B_DAL = null;
            DataTable mDTable = null;

            mCal_6B_DAL = new Cal_6B_DAL();
            mDTable = new DataTable();

            mDTable = mCal_6B_DAL.Get_PID_For_6B_Calculation();
            return mDTable;

        }
        
        public DataSet Get_Input_For_6B_Calculation(string BoilerID, string ProjectID, string BoilerLoad, string SectionID,string LAst_5B_Sec,int obje)
        {
            Cal_6B_DAL mCal_6B_DAL = null;
            DataSet mDTable = null;

            mCal_6B_DAL = new Cal_6B_DAL();
            mDTable = new DataSet();

            mDTable = mCal_6B_DAL.GetInput_For_6B_Calculation(BoilerID, ProjectID, BoilerLoad, SectionID, LAst_5B_Sec,obje);
            return mDTable; 

        }

        public Double Get_2A_Platen_Exit_Temp_ReqTemp(string BoilerID, string ProjectID, string InputVal, string BoilerLoad, int ObjID)
        {
            Cal_6B_DAL mCal_6B_DAL = null;
            Double mDTable;

            mCal_6B_DAL = new Cal_6B_DAL();
            //mDTable = new DataSet();

            mDTable = mCal_6B_DAL.Get_2A_Platen_Exit_Temp_ReqTemp(BoilerID, ProjectID, InputVal,BoilerLoad,ObjID);
            return mDTable;
        }

        public DataSet Insert_6B_Calculation(int pid, string BoilerID, string ProjectID, int ObjectiveID, string BoilerLoad,string SectionID, double Value)
        {
            Cal_6B_DAL mCal_6B_DAL = null;
            DataSet mDTable;

            mCal_6B_DAL = new Cal_6B_DAL();
            //mDTable = new DataSet();

            mDTable = mCal_6B_DAL.Insert_6B_Calculation(pid,BoilerID,ProjectID,ObjectiveID,BoilerLoad, SectionID,Value);
            return mDTable;

        }
        
        public string GetLast_Element_of_HeatingSectionUpperFurnace(string BoilerID, string ProjectID)
        {
            Cal_6B_DAL mCal_6B_DAL = new Cal_6B_DAL();
            string mDts = null;

            mDts = mCal_6B_DAL.GetLast_Element_of_HeatingSectionUpperFurnace(BoilerID, ProjectID);

            return mDts;

        }
        
        public string Get_PreVious_HE_SectionID(string BoilerID, string ProjectID, string SectionID)
        {
            Cal_6B_DAL mCal_6B_DAL = new Cal_6B_DAL();
            string mDts = null;

            mDts = mCal_6B_DAL.Get_PreVious_HE_SectionID(BoilerID, ProjectID, SectionID);

            return mDts;
        }
        
        public DataSet Get_S_Parameter_Section_ID(string BoilerID, string ProjectID, string BoilerLoad, string SectionID,int ObjectiveID,int Iteration)
        {

            Cal_6B_DAL mCal_6B_DAL = new Cal_6B_DAL();
            DataSet mDts = null;
            mDts = new DataSet();

            mDts = mCal_6B_DAL.Get_S_Parameter_Section_ID(BoilerID, ProjectID, BoilerLoad, SectionID,ObjectiveID,Iteration);
            return mDts;
        }
        
        public void InsertUpadate_6B_S_ParameterValue(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID, double Input,
           double Output, int Iteration, string Previous_SectionID)
        {
            Cal_6B_DAL mCal_5B_DAL = new Cal_6B_DAL();
            DataSet mDts = null;
            mDts = new DataSet();

            mDts = mCal_5B_DAL.InsertUpadate_6B_S_ParameterValue(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Input, Output, Iteration,Previous_SectionID);
        }
    
    }
}
