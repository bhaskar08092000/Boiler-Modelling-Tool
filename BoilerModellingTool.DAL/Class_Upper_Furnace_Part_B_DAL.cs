using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using BoilerModellingTool.SC;

namespace BoilerModellingTool.DAL
{
    public class Class_Upper_Furnace_Part_B_DAL
    {
        #region " Variables "

    private Database currentDatabase;

    #endregion

        #region " Constructor "

    public Class_Upper_Furnace_Part_B_DAL()
    {
        currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
    }
        #endregion

        public string Get_PreVious_HE_SectionID(string BoilerID, string ProjectID, string SectionID)
        {
            String mStoredProcName = String.Empty;
            String mStoredProc_Name = String.Empty;

            Cal_5B_SC mCal_5B_SC = null;
            DbCommand mDbCommand = null;

            DataSet mDSet = null;
            DataTable mDTable = null;
            string Value = null;
            //try
            //{
            mDTable = new DataTable();
            mCal_5B_SC = new Cal_5B_SC();
            mStoredProcName = StoredProcedure.spr_S_parameter_HeatinElement_Sequence;
            mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
            currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
            mDSet = currentDatabase.ExecuteDataSet(mDbCommand);


            for (int i = 1; i < mDSet.Tables.Count; i++)
            {
                mDSet.Tables[0].Merge(mDSet.Tables[i]);
            }

            mDTable = mDSet.Tables[0];

            for (int i = 0; i < mDTable.Rows.Count - 1; i++)
            {
                if (SectionID == mDTable.Rows[i]["SectionID"].ToString())
                {
                    int Sec_number = Convert.ToInt16(mDTable.Rows[i]["SectionNumber"].ToString());
                    string Sec_Type = mDTable.Rows[i]["SectionType"].ToString();
                    if (Sec_number == 1 || Sec_Type == "Reheater Elements")
                    {
                        Value = SectionID;
                    }
                    else
                    {
                        Value = mDTable.Rows[i - 1]["SectionID"].ToString();
                    }
                }

            }









            
            return Value;
        }
        
        public DataSet Get_Design_Area_From_Section_ID_in_4B(string BoilerID, string ProjectID, string SectionID)
        {
            String mStoredProcName = String.Empty;

           
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
              
                mStoredProcName = StoredProcedure.spr_Get_Design_Area_From_Section_ID_in_4B;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
            return mDSet;

        }

        //public DataSet Get_S_Parameter_Section_ID(string BoilerID, string ProjectID, string BoilerLoad, string SectionID)
        //{
        //    DataSet mDSet = null;
        //    DbCommand mDbCommand = null;
        //    String mStoredProcedure = String.Empty;
        //    try
        //    {
        //        mStoredProcedure = StoredProcedure.spr_Get_S_Parameter_Section_ID;
        //        mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);


        //        currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
        //        currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
        //        currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
        //        currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);

        //        mDSet = new DataSet();
        //        mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
        //        return mDSet;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        //BB
        public DataSet Get_S_Parameter_Section_ID(
        string BoilerID,
        string ProjectID,
        string BoilerLoad,
        int ObjectiveID,
        string SectionID,
        int Iteration
            )
        {
            DbCommand cmd = currentDatabase.GetStoredProcCommand("spr_Get_S_Parameter_Section_ID");

            currentDatabase.AddInParameter(cmd, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(cmd, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(cmd, "@vBoilerLoadID", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(cmd, "@vObjectiveID", DbType.Int32, ObjectiveID);  // ✅ FIX
            currentDatabase.AddInParameter(cmd, "@vSectionID", DbType.String, SectionID);
            currentDatabase.AddInParameter(cmd, "@vIteration", DbType.Int32, Iteration);      // ✅ FIX

            return currentDatabase.ExecuteDataSet(cmd);
        }
        
        public Double Get_De_superheating_spray_Next_HE_Type(string BoilerID, string ProjectID, string SectionID)
        {

            String mStoredProcName = String.Empty;

           
            DbCommand mDbCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;
            Double Value = 0.0;
            //try
            //{
            mDTable = new DataTable();
          
            mStoredProcName = StoredProcedure.spr_Get_De_superheating_spray_Next_HE_Type;
            mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
            currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
            mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
            mDTable = mDSet.Tables[1];
            Value = Convert.ToDouble(mDTable.Rows[0][0].ToString());


            //}
            //catch
            //{

            //}
            return Value;

        }

        public DataSet Get_Input_For_Upper_Furnace_Part_B(string BoilerID, string ProjectID, string BoilerLoad, string SectionID)
        {
            String mStoredProcName = String.Empty;

         
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            //try
            //{
            mDTable = new DataTable();

            mStoredProcName = StoredProcedure.spr_Get_Input_For_Upper_Furnace_Part_B;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);

            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            //}
            //catch
            //{

            // }
            return mDSet;

        }
       
        public DataSet Get_submodule_heating_section_in_upper_furnace(String BoilerID, String ProjectID)
        {
            String mStoredProcName = String.Empty;

           // Cal_4B_SC mCal_4B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
               // mCal_4B_SC = new Cal_4B_SC();
                mStoredProcName = StoredProcedure.spr_Get_submodule_heating_section_in_upper_furnace;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                //currentDatabase.AddInParameter(mDBCommand,"@SectionID", DbType.String, SectionID);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
            return mDSet;

        }

        public DataSet InsertUpadate_5B_S_ParameterValue(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID, string Input,
  string Output,
  int Iteration,string NextSectionID)
        {

            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_InsertUpadate_5B_S_ParameterValue;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                //currentDatabase.AddInParameter(mDbCommand, "@vSectionID_S_ParameterValue", DbType.String, SectionID_S_ParameterValue);
                currentDatabase.AddInParameter(mDbCommand, "@vInput", DbType.String, Input);
                currentDatabase.AddInParameter(mDbCommand, "@vOutput", DbType.String, Output);
                currentDatabase.AddInParameter(mDbCommand, "@vIteration", DbType.String, Iteration);
                currentDatabase.AddInParameter(mDbCommand, "@vNextSectionID", DbType.String, NextSectionID);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
                return mDSet;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public DataSet Insert_4B_Calculation(int pid, string BoilerID, string ProjectID, string BoilerLoad, int Objective, string Value)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_4B_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.String, pid);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, Objective);


                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Value);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
                return mDSet;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
