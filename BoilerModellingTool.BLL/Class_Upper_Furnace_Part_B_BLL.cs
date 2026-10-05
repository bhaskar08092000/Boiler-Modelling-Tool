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
    public class Class_Upper_Furnace_Part_B_BLL
    {
        public string Get_PreVious_HE_SectionID(string BoilerID, string ProjectID, string SectionID)
        {
            Class_Upper_Furnace_Part_B_DAL mClass_Upper_Furnace_Part_B_DAL = new Class_Upper_Furnace_Part_B_DAL();
            string mDts = null;

            mDts = mClass_Upper_Furnace_Part_B_DAL.Get_PreVious_HE_SectionID(BoilerID, ProjectID, SectionID);

            return mDts;
        }

        public DataSet Get_Design_Area_From_Section_ID_in_4B(String BoilerID, String ProjectID, String SectionID)
        {
            Class_Upper_Furnace_Part_B_DAL mCal_4B_DAL = null;
            DataSet mDTable = null;

            mCal_4B_DAL = new Class_Upper_Furnace_Part_B_DAL();
            mDTable = new DataSet();

            mDTable = mCal_4B_DAL.Get_Design_Area_From_Section_ID_in_4B(BoilerID, ProjectID, SectionID);
            return mDTable;
        }

        //public DataSet Get_S_Parameter_Section_ID(string BoilerID, string ProjectID, string BoilerLoad, string SectionID)
        //{

        //    Class_Upper_Furnace_Part_B_DAL mClass_Upper_Furnace_Part_B_DAL = new Class_Upper_Furnace_Part_B_DAL();
        //    DataSet mDts = null;
        //    mDts = new DataSet();

        //    mDts = mClass_Upper_Furnace_Part_B_DAL.Get_S_Parameter_Section_ID(BoilerID, ProjectID, BoilerLoad, SectionID);
        //    return mDts;
        //}
        //BB
        public DataSet Get_S_Parameter_Section_ID(
                string BoilerID,
                string ProjectID,
                string BoilerLoad,
                int SectionID,
                string ObjectiveID,     // ✅ ADD
                int Iteration        // ✅ ADD
            )
        {
            Class_Upper_Furnace_Part_B_DAL mClass_Upper_Furnace_Part_B_DAL =
                new Class_Upper_Furnace_Part_B_DAL();

            DataSet mDts = new DataSet();

            mDts = mClass_Upper_Furnace_Part_B_DAL.Get_S_Parameter_Section_ID(
                BoilerID,
                ProjectID,
                BoilerLoad,     // ✅ PASS
                SectionID,
                ObjectiveID,
                Iteration        // ✅ PASS
            );

            return mDts;
        }

        public Double Get_De_superheating_spray_Next_HE_Type(string BoilerID, string ProjectID, string SectionID)
        {
            Class_Upper_Furnace_Part_B_DAL mClass_Upper_Furnace_Part_B_DAL = new Class_Upper_Furnace_Part_B_DAL();
            Double mDts = 0.0;

            mDts = mClass_Upper_Furnace_Part_B_DAL.Get_De_superheating_spray_Next_HE_Type(BoilerID, ProjectID, SectionID);

            return mDts;
        }
        
        public DataSet Get_Input_For_Upper_Furnace_Part_B(string BolierID, string ProjectID, string BoilerID, string SectionID)
        {
            Class_Upper_Furnace_Part_B_DAL mCal_5B_DAL = null;
            DataSet mDTable = null;

            mCal_5B_DAL = new Class_Upper_Furnace_Part_B_DAL();
            mDTable = new DataSet();

            mDTable = mCal_5B_DAL.Get_Input_For_Upper_Furnace_Part_B(BolierID, ProjectID, BoilerID, SectionID);
            return mDTable;

        }

        //public DataSet Get_submodule_heating_section_in_upper_furnace(String BoilerID, String ProjectID)
        //{
        //    Class_Upper_Furnace_Part_B_DAL mCal_5B_DAL = null;
        //    DataSet mDTable = null;

        //    mCal_5B_DAL = new Class_Upper_Furnace_Part_B_DAL();
        //    mDTable = new DataSet();

        //    mDTable = mCal_5B_DAL.Get_submodule_heating_section_in_upper_furnace(BoilerID, ProjectID);
        //    return mDTable;
        //}
        public DataSet Get_submodule_heating_section_in_upper_furnace(String BoilerID, String ProjectID)
        {
            Class_Upper_Furnace_Part_B_DAL mCal_5B_DAL = null;
            DataSet mDTable = null;

            try
            {
                // Debug input parameters
                System.Diagnostics.Debug.WriteLine("DEBUG: BoilerID = " + BoilerID);
                System.Diagnostics.Debug.WriteLine("DEBUG: ProjectID = " + ProjectID);

                mCal_5B_DAL = new Class_Upper_Furnace_Part_B_DAL();
                mDTable = new DataSet();

                mDTable = mCal_5B_DAL.Get_submodule_heating_section_in_upper_furnace(BoilerID, ProjectID);

                // Debug dataset result
                if (mDTable == null)
                {
                    System.Diagnostics.Debug.WriteLine("DEBUG: DataSet is NULL");
                }
                else if (mDTable.Tables.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("DEBUG: No tables returned");
                }
                else if (mDTable.Tables[0].Rows.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("DEBUG: No rows returned");
                }
                else
                {
                    foreach (DataRow row in mDTable.Tables[0].Rows)
                    {
                        if (mDTable.Tables[0].Columns.Contains("SectionID"))
                        {
                            System.Diagnostics.Debug.WriteLine("DEBUG: SectionID = " + row["SectionID"].ToString());
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("DEBUG: SectionID column not found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }

            return mDTable;
        }

        public void InsertUpadate_5B_S_ParameterValue(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID, string Input,
           string Output, int Iteration,string NextSectionID)
        {
            Class_Upper_Furnace_Part_B_DAL mCal_5B_DAL = new Class_Upper_Furnace_Part_B_DAL();
            DataSet mDts = null;
            mDts = new DataSet();
            mDts = mCal_5B_DAL.InsertUpadate_5B_S_ParameterValue(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID, Input, Output, Iteration, NextSectionID);
        }

        public void Insert_4B_Calculation(int pid, string BoilerID, string ProjectID, string BoilerLoad, int ObjeCtive, string Value)
        {

            Cal_4B_DAL mCal_4B_DAL = new Cal_4B_DAL();
            DataSet mDts = null;
            mDts = new DataSet();

            mDts = mCal_4B_DAL.Insert_4B_Calculation(pid, BoilerID, ProjectID, BoilerLoad, ObjeCtive, Value);
        }
    
    }
}