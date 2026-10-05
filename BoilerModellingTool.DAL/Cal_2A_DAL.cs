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
    public class Cal_2A_DAL
    {

        #region " Variables "

        private Database currentDatabase;

        #endregion
        #region " Constructor "

        public Cal_2A_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion

        public Cal_2A_SC GetInput_For_2A_Calculation(string BoilerID, string ProjectID,string BoilerLoad,int objectiveID)
        {

            String mStoredProcName = String.Empty;
            Cal_2A_SC mCal_2A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_2A_SC = new Cal_2A_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_2A_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoileLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.String, objectiveID);
               

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                mDTable = mDSet.Tables[1];

                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mCal_2A_SC.Theoretical_volume_of_N2 = mDSet.Tables[0].Rows[2]["Value"].ToString();
                    mCal_2A_SC.Theoretical_volume_of_CO2_and_SO2 = mDSet.Tables[0].Rows[4]["Value"].ToString();
                   
                    mCal_2A_SC.Theoretical_volume_of_air = mDSet.Tables[0].Rows[1]["Value"].ToString();
                    mCal_2A_SC.Excess_air_ratio = mDSet.Tables[0].Rows[0]["Value"].ToString();
                    mCal_2A_SC.Fly_ash_concentration = mDSet.Tables[0].Rows[5]["Value"].ToString();
                    mCal_2A_SC.Flue_gas_flow_rate= mDSet.Tables[0].Rows[7]["Value"].ToString();
                    mCal_2A_SC.Fuel_firing_rate = mDTable.Rows[0][0].ToString();

                }

                mCal_2A_SC.Theoretical_volume_of_H2O = mDSet.Tables[3].Rows[0]["Lower_furnace"].ToString(); 
            }
            catch
            {

            }
            finally
            {

            }
            return mCal_2A_SC;
        }
        public DataSet GetInput_For_25_Enthalpy_Calculation(string BoilerID, string ProjectID,string BoilerLoad,int objectiveID)
        {
            String mStoredProcName = String.Empty;
            Cal_2A_SC mCal_2A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_2A_SC = new Cal_2A_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_25_Enthalpy_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.String, objectiveID);
	
                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
            }
            catch
            {}
            finally
            { }

            return mDSet;
        }

        public DataSet GetCCO2FromEnthalpyTable()
        {
            string mStoredProc = String.Empty;
            DbCommand mDBCommand = null;
            DataSet mDset = null;

            mDset = new DataSet();
            mStoredProc = StoredProcedure.spr_GetCCO2θ_For_2A_Calculation;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProc);
            mDset = currentDatabase.ExecuteDataSet(mDBCommand);
            return mDset;
        }
        public DataSet Insert_EnthalpyTableForTempZeroCalculatedValue(Cal_2A_SC mCal_2A_SC, string BoilerID, string ProjectID,string BoilerLoad,int objective)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            //BoilerLoad = "TT";

            //try
            //{
                mStoredProcedure = StoredProcedure.spr_Insert_EnthalpyTableForTempZeroCalculatedValue;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);
                currentDatabase.AddInParameter(mDbCommand, "@vEnthalpyTableID", DbType.String, mCal_2A_SC.EnthalpyID);
                currentDatabase.AddInParameter(mDbCommand, "@vIRO2", DbType.String, mCal_2A_SC.IRO2);
                currentDatabase.AddInParameter(mDbCommand, "@vIN2", DbType.String, mCal_2A_SC.IN2);
                currentDatabase.AddInParameter(mDbCommand, "@vIH2O", DbType.String, mCal_2A_SC.IH2O);
                currentDatabase.AddInParameter(mDbCommand, "@vIfa", DbType.String, mCal_2A_SC.Ifa);
                currentDatabase.AddInParameter(mDbCommand, "@vIog", DbType.String, mCal_2A_SC.Iog);
                currentDatabase.AddInParameter(mDbCommand, "@vIoa", DbType.String, mCal_2A_SC.Ioa);
                currentDatabase.AddInParameter(mDbCommand, "@vLowerFurnaceAlpha", DbType.String, mCal_2A_SC.LowerFurnaceAlpha);
                currentDatabase.AddInParameter(mDbCommand, "@vUpperFurnaceAlpha", DbType.String, mCal_2A_SC.UpperFurnaceAlpha);
                currentDatabase.AddInParameter(mDbCommand, "@vCrossDuctAlpha", DbType.String, mCal_2A_SC.CrossDuctAlpha);
                currentDatabase.AddInParameter(mDbCommand, "@vReverseChamberAlpha", DbType.String, mCal_2A_SC.ReverseChamberAlpha);
                currentDatabase.AddInParameter(mDbCommand, "@vBackpassAlpha", DbType.String, mCal_2A_SC.BackpassAlpha);
                currentDatabase.AddInParameter(mDbCommand, "@vNewAlpha", DbType.String, mCal_2A_SC.NewAlpha);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                 currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                 currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                 currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, objective);

                
                
                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);


            //}
            //catch
            //{

            //}
            return mDSet;
        }
        public void Insert_EnthalpyTableForTemp25CalculatedValue(Cal_2A_SC mCal_2A_SC, string BoilerID, string ProjectID,string BoilerLoad,int ObjectiveID)
        {
         DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
           // BoilerLoad = "TT";
            //try
            //{
                mStoredProcedure = StoredProcedure.spr_Insert_EnthalpyTableForTemp25CalculatedValue;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);
                currentDatabase.AddInParameter(mDbCommand, "@vIog", DbType.String, mCal_2A_SC.Iog);
                currentDatabase.AddInParameter(mDbCommand, "@vIoa", DbType.String, mCal_2A_SC.Ioa);
                currentDatabase.AddInParameter(mDbCommand, "@vNewAlpha", DbType.String, mCal_2A_SC.NewAlpha);

                currentDatabase.AddInParameter(mDbCommand, "@vLowerFurnaceAlpha", DbType.String, mCal_2A_SC.LowerFurnaceAlpha);
                currentDatabase.AddInParameter(mDbCommand, "@vUpperFurnaceAlpha", DbType.String, mCal_2A_SC.UpperFurnaceAlpha);
                currentDatabase.AddInParameter(mDbCommand, "@vCrossDuctAlpha", DbType.String, mCal_2A_SC.CrossDuctAlpha);
                currentDatabase.AddInParameter(mDbCommand, "@vReverseChamberAlpha", DbType.String, mCal_2A_SC.ReverseChamberAlpha);
                currentDatabase.AddInParameter(mDbCommand, "@vBackpassAlpha", DbType.String, mCal_2A_SC.BackpassAlpha);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
               
                currentDatabase.AddInParameter(mDbCommand, "@vAPH_Flue_gas_Enthalpy", DbType.String, mCal_2A_SC.APH_Flue_gas_Enthalpy);
                currentDatabase.AddInParameter(mDbCommand, "@vAPH_Air_Enthalpy", DbType.String, mCal_2A_SC.APH_Air_Enthalpy);
                currentDatabase.AddInParameter(mDbCommand, "@vEnthalpyTableID", DbType.Int16, mCal_2A_SC.EnthalpyID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
            //}
            //catch
            //{

            //}
           
        }
        public DataSet Get_Nearest_Max_Min_EnthalpyCalculation(string BoilerID, string ProjectID, Double Input,string BoilerLoad,int objectiveid)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Get_Nearest_Max_Min_EnthalpyCalculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);
                currentDatabase.AddInParameter(mDbCommand, "@vInputValue", DbType.Double, Input);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, objectiveid);

               mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
            }
            catch
            {

            }
            return mDSet;
        }
        public DataSet select_APH_Calculation(string BoilerID, string ProjectID,string BoilerLoad,int ObjectiveID)
        {
            String mStoredProcName = String.Empty;
            Cal_2A_SC mCal_2A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_2A_SC = new Cal_2A_SC();
                mStoredProcName = StoredProcedure.spr_select_APH_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@VObjectiveID", DbType.String, ObjectiveID);
                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
            }
            catch
            {}
            finally
            { }

            return mDSet;
        }
        //public void Update_CP_Flue_AND_Air_Gas(Cal_2A_SC mCal_2A_SC, string BoilerID, string ProjectID,string BoilerLoad,int objectiveID)
        //{
        //    DbCommand mDbCommand = null;
        //    String mStoredProcedure = String.Empty;
        //    BoilerLoad = "TT";
        //    try
        //    {
        //        mStoredProcedure = StoredProcedure.spr_Update_CP_Flue_AND_Air_Gas;
        //        mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);
        //        currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
        //        currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
        //        currentDatabase.AddInParameter(mDbCommand, "@vEnthalpyTableID", DbType.String, mCal_2A_SC.EnthalpyID);
        //        currentDatabase.AddInParameter(mDbCommand, "@vCp_Flue_Gas", DbType.String, mCal_2A_SC.Cp_flue_gas);
        //        currentDatabase.AddInParameter(mDbCommand, "@vCp_Air", DbType.String, mCal_2A_SC.Cp_air);
        //         currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
        //        currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, objectiveID);
        //         currentDatabase.ExecuteNonQuery(mDbCommand);
        //    }
        //    catch 
        //    {

        //    }
        //}
        //BB
        public void Update_CP_Flue_AND_Air_Gas(
    Cal_2A_SC mCal_2A_SC,
    string BoilerID,
    string ProjectID,
    string BoilerLoad,
    int objectiveID)
        {
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;

            //BoilerLoad = "TT";
            BoilerLoad = "100%TMCR";

            try
            {
                // ✅ DEBUG INPUT VALUES
                System.Diagnostics.Debug.WriteLine("==== CP UPDATE INPUT ====");
                System.Diagnostics.Debug.WriteLine("BoilerID: " + BoilerID);
                System.Diagnostics.Debug.WriteLine("ProjectID: " + ProjectID);
                System.Diagnostics.Debug.WriteLine("EnthalpyID: " + mCal_2A_SC.EnthalpyID);
                System.Diagnostics.Debug.WriteLine("Cp_flue_gas: " + mCal_2A_SC.Cp_flue_gas);
                System.Diagnostics.Debug.WriteLine("Cp_air: " + mCal_2A_SC.Cp_air);

                // ✅ UPDATE SP CALL
                mStoredProcedure = StoredProcedure.spr_Update_CP_Flue_AND_Air_Gas;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vEnthalpyTableID", DbType.String, mCal_2A_SC.EnthalpyID);

                // ✅ IMPORTANT FIX → use DOUBLE
                currentDatabase.AddInParameter(mDbCommand, "@vCp_Flue_Gas", DbType.Double, mCal_2A_SC.Cp_flue_gas);
                currentDatabase.AddInParameter(mDbCommand, "@vCp_Air", DbType.Double, mCal_2A_SC.Cp_air);

                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int32, objectiveID);

                currentDatabase.ExecuteNonQuery(mDbCommand);

                // ✅ ✅ STEP 2: VERIFY AFTER UPDATE
                string verifyQuery = @"
            SELECT 
                [Cp flue gas],
                [Cp air]
            FROM tbl_2A_EnthalpyTableForTemp25CalculatedValue
            WHERE BoilerID = @BoilerID
              AND ProjectID = @ProjectID
              AND EnthalpyTableID = @EnthalpyID";

                DbCommand verifyCommand = currentDatabase.GetSqlStringCommand(verifyQuery);

                currentDatabase.AddInParameter(verifyCommand, "@BoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(verifyCommand, "@ProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(verifyCommand, "@EnthalpyID", DbType.String, mCal_2A_SC.EnthalpyID);

                DataSet ds = currentDatabase.ExecuteDataSet(verifyCommand);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var row = ds.Tables[0].Rows[0];

                    System.Diagnostics.Debug.WriteLine("==== CP AFTER UPDATE ====");
                    System.Diagnostics.Debug.WriteLine("DB Cp_flue_gas: " + row[0]);
                    System.Diagnostics.Debug.WriteLine("DB Cp_air: " + row[1]);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("❌ VERIFY FAILED → NO DATA FOUND");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("❌ ERROR: " + ex.Message);
            }
        }
    }
}
