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
   public class Cal_5B_DAL
    { 
        #region " Variables "

    private Database currentDatabase;

    #endregion
        #region " Constructor "

    public Cal_5B_DAL()
    {
        currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
    }
        #endregion

        public DataSet Get_Input_For_5B_Calculation(string BoilerID, string ProjectID,string BoilerLoad,string SectionID,int ObjectiveID)
        {
            String mStoredProcName = String.Empty;

            Cal_5B_SC mCal_5B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_5B_SC = new Cal_5B_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_5B_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int32, ObjectiveID);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
                return mDSet;

        }

        public DataTable Get_PID_For_5B_Calculation()
        {
            String mStoredProcName = String.Empty;

            Cal_5B_SC mCal_5B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_5B_SC = new Cal_5B_SC();
                mStoredProcName = StoredProcedure.spr_Get_PID_For_5B_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);


                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                mDTable = mDSet.Tables[0];

            }
            catch
            {

            }
            return mDTable;

        }

        public Double Get_2A_Platen_Exit_Temp_ReqTemp(string BoilerID, string ProjectID, string InputVal, string BoilerLoad, int ObjID)
        {
            String mStoredProcName = String.Empty;

            Cal_5B_SC mCal_5B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            Double mDTable = 0.0;

            try
            {

                mCal_5B_SC = new Cal_5B_SC();
                mStoredProcName = StoredProcedure.spr_2A_Platen_Exit_Temp_ReqTemp;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, ObjID);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                Double Temp_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Temp(Deg C)"].ToString());
                Double Enthalpy_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["LowerFurnaceAlpha"].ToString());
                Double Temp_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
                Double Enthalpy_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["LowerFurnaceAlpha"].ToString());
                Double Input = Convert.ToDouble(InputVal);

                if(Convert.ToDouble(InputVal) <100)
                {
                    Temp_Min = 25;
                    Enthalpy_Min = 0;
                }


                //mDTable = (Enthalpy_Max - Enthalpy_Min) / (Temp_Max - Temp_Min) * (Input - Temp_Min) + Enthalpy_Min;
                mDTable = (Temp_Max - Temp_Min) / (Enthalpy_Max - Enthalpy_Min) * (Input - Enthalpy_Min) + Temp_Min;
                return mDTable;
            }
            catch
            {

            }
            return mDTable;

        }

        public DataSet Insert_5B_Calculation(int pid, string BoilerID, string ProjectID,string BoilerLoad,string Section_ID,int ObjectiveID, double Value)
        {
        DataSet mDSet = null;
        DbCommand mDbCommand = null;
        String mStoredProcedure = String.Empty;
        try
        {
            mStoredProcedure = StoredProcedure.spr_Insert_5B_Calculation;
            mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

            currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.String, pid);
            currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, Section_ID);
            currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Convert.ToString(Value));
            currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, ObjectiveID);


                

            mDSet = new DataSet();
            mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
            return mDSet;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

        //public Double Get_De_superheating_spray_Next_HE_Type(string BoilerID, string ProjectID,string SectionID)
        //{

        //    String mStoredProcName = String.Empty;

        //    Cal_5B_SC mCal_5B_SC = null;
        //    DbCommand mDbCommand = null;
        //    DataSet mDSet = null;
        //    DataTable mDTable = null;
        //        Double Value=0.0;
        //    //try
        //    //{
        //        mDTable = new DataTable();
        //        mCal_5B_SC = new Cal_5B_SC();
        //        mStoredProcName = StoredProcedure.spr_Get_De_superheating_spray_Next_HE_Type;
        //        mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
        //        currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
        //        currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);               
        //        currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
        //        mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
        //        mDTable = mDSet.Tables[1];
        //        Value = Convert.ToDouble(mDTable.Rows[0][0].ToString());


        //    //}
        //    //catch
        //    //{

        //    //}
        //    return Value;

        //}
        //BB
        public double Get_De_superheating_spray_Next_HE_Type(string BoilerID, string ProjectID, string SectionID)
        {
            double Value = 0.0;

            DbCommand mDbCommand = currentDatabase.GetStoredProcCommand(
                StoredProcedure.spr_Get_De_superheating_spray_Next_HE_Type
            );

            currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);

            System.Diagnostics.Debug.WriteLine("==== INPUT PARAMETERS ====");
            System.Diagnostics.Debug.WriteLine("ProjectID: " + ProjectID);
            System.Diagnostics.Debug.WriteLine("BoilerID: " + BoilerID);
            System.Diagnostics.Debug.WriteLine("SectionID: " + SectionID);

            DataSet mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

            System.Diagnostics.Debug.WriteLine("========== DEBUG NEXT HE TYPE ==========");
            System.Diagnostics.Debug.WriteLine("Tables Count: " + (mDSet?.Tables.Count ?? 0));

            bool valueFound = false;

            // ✅ SAFER VALUE SEARCH
            if (mDSet != null && mDSet.Tables.Count > 0)
            {
                for (int t = 0; t < mDSet.Tables.Count; t++)
                {
                    DataTable table = mDSet.Tables[t];

                    if (table != null && table.Rows.Count > 0 && table.Columns.Count > 0)
                    {
                        string columnName = table.Columns[0].ColumnName;
                        string rawValue = table.Rows[0][0].ToString();

                        System.Diagnostics.Debug.WriteLine($"Table[{t}] Column: {columnName}, Value: {rawValue}");

                        double temp;

                        // ✅ Only accept numeric values (ignore GUIDs etc.)
                        if (double.TryParse(rawValue, out temp))
                        {
                            Value = temp;
                            valueFound = true;

                            System.Diagnostics.Debug.WriteLine("✅ VALUE FOUND IN SP: " + Value);
                            break;
                        }
                    }
                }
            }

            // ✅ FALLBACK (SAFE GUARD)
            if (!valueFound)
            {
                System.Diagnostics.Debug.WriteLine("❌ SP did NOT return numeric VALUE → USING FALLBACK");

                try
                {
                    DbCommand fallbackCmd = currentDatabase.GetSqlStringCommand(
                        @"SELECT TOP 1 Value 
                  FROM dbo.[tbl_PIWater/SteamFlowsChild]
                  WHERE PID = 5
                    AND ProjectID = @vProjectID
                    AND BoilerID = @vBoilerID"
                    );

                    currentDatabase.AddInParameter(fallbackCmd, "@vProjectID", DbType.String, ProjectID);
                    currentDatabase.AddInParameter(fallbackCmd, "@vBoilerID", DbType.String, BoilerID);

                    object result = currentDatabase.ExecuteScalar(fallbackCmd);

                    if (result != null && result != DBNull.Value)
                    {
                        double.TryParse(result.ToString(), out Value);
                        System.Diagnostics.Debug.WriteLine("✅ FALLBACK VALUE = " + Value);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("❌ FALLBACK QUERY RETURNED NULL");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("❌ FALLBACK ERROR: " + ex.Message);
                }
            }

            return Value;
        }

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


            for (int i = 1; i < mDSet.Tables.Count ; i++)
            {
                mDSet.Tables[0].Merge(mDSet.Tables[i]);
            }

             mDTable =  mDSet.Tables[0];

            for(int i =0;i< mDTable.Rows.Count-1; i++)
            {
                if (SectionID == mDTable.Rows[i]["SectionID"].ToString())
               {
                    int Sec_number=Convert.ToInt16(mDTable.Rows[i]["SectionNumber"].ToString());
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

        public DataSet Get_S_Parameter_Section_ID(string BoilerID, string ProjectID,string BoilerLoad,int ObjectiveID, string SectionID,int Iteration)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Get_S_Parameter_Section_ID;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

              
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vIteration", DbType.Int16, Iteration);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
                return mDSet;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataSet InsertUpadate_5B_S_ParameterValue(string ProjectID, string BoilerID ,string BoilerLoad , int ObjectiveID ,string SectionID  ,double Input ,
	    double Output ,
	    int Iteration, string NextSectionID )
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
	            currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
	            currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
	            //currentDatabase.AddInParameter(mDbCommand, "@vSectionID_S_ParameterValue", DbType.String, SectionID_S_ParameterValue);
	            currentDatabase.AddInParameter(mDbCommand, "@vInput", DbType.String, Convert.ToString(Input));
	            currentDatabase.AddInParameter(mDbCommand, "@vOutput", DbType.String, Convert.ToString(Output));
                currentDatabase.AddInParameter(mDbCommand, "@vIteration", DbType.Int16, Iteration);
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
   
   }
}
