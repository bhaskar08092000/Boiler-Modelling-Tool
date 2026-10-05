using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using BoilerModellingTool.SC;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace BoilerModellingTool.DAL
{
    public class Cal_1A_DAL
    {       
          #region " Variables "

        private Database currentDatabase;

        #endregion
        #region " Constructor "

        public Cal_1A_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion

    public DataSet GetData(Cal_1A_SC vCal_1A_SC)
{
    DataSet mDSet = null;
    DbCommand mDbCommand = null;
    String mStoredProcedure = String.Empty;
    try
    {
        mStoredProcedure = StoredProcedure.spr_Get_PI_for_1A;
        mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

        mDSet = new DataSet();
        mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
        return mDSet;
    }
    catch (Exception ex)
    {
        throw;
    }
}
    public DataSet GetFuelParameterForUltimate(string ProjectID,string BoilerID,string BoilerLoad,int objeCtive_ID)
    {
            String mStoredProcName = String.Empty;
            Cal_2A_SC mCal_2A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            try
            {              
                mCal_2A_SC = new Cal_2A_SC();
                mStoredProcName = StoredProcedure.spr_GetFuelParameterForUltimate;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, objeCtive_ID);  
	
                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                
            }
            catch
            {

            }
            return mDSet;
           
    }
    public DataSet Insert1A_Value_with_5_Parameters(int pid, int cgid, string BoilerID, string ProjectID, string BoilerLoadID, int ObjectiveID, double Upperfurnace, double LowerFurnace, double Crossduct, double ReverseChamber, double Backpass)
    {
        String mStoredProcName = String.Empty;
            Cal_2A_SC mCal_2A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            //BoilerLoadID = "TT";


            try
            {
                
                mCal_2A_SC = new Cal_2A_SC();
                mStoredProcName = StoredProcedure.spr_Insert1AValues_5_parameter;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vPID ", DbType.Int16, pid);
                currentDatabase.AddInParameter(mDBCommand, "@vCalculationGrpID", DbType.Int16, cgid);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoadID);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDBCommand, "@vLower_furnace", DbType.String, LowerFurnace);
                currentDatabase.AddInParameter(mDBCommand, "@vUpper_furnace", DbType.String, Upperfurnace);
                currentDatabase.AddInParameter(mDBCommand, "@vCross_duct", DbType.String, Crossduct);
                currentDatabase.AddInParameter(mDBCommand, "@vReverse_Chamber", DbType.String, ReverseChamber);
                currentDatabase.AddInParameter(mDBCommand, "@vBackpass", DbType.String, Backpass);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                return mDSet;
            }
        catch
            {

            }
            return mDSet;
    } 
    public DataSet Insert1A_Value(int pid, int cgid, string BoilerID, string ProjectID, string BoilerLoadID, int ObjectiveID, double Value)
    {
        String mStoredProcName = String.Empty;
        Cal_2A_SC mCal_2A_SC = null;
        DbCommand mDBCommand = null;
        DataSet mDSet = null;

        //BoilerLoadID = "TT";


        try
        {

            mCal_2A_SC = new Cal_2A_SC();
            mStoredProcName = StoredProcedure.spr_Insert1AValues;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
            currentDatabase.AddInParameter(mDBCommand, "@vPID ", DbType.Int16, pid);
            currentDatabase.AddInParameter(mDBCommand, "@vCalculationGrpID", DbType.Int16, cgid);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoadID);
            currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
            currentDatabase.AddInParameter(mDBCommand, "@vValue", DbType.String, Value);
            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
            return mDSet;
        }
        catch
        {

        }
        return mDSet;
    }
    public DataSet Get_1A_Parameters()
    {
        String mStoredProcName = String.Empty;
        Cal_2A_SC mCal_2A_SC = null;
        DbCommand mDBCommand = null;
        DataSet mDSet = null;
        try
        {
            mCal_2A_SC = new Cal_2A_SC();
            mStoredProcName = StoredProcedure.spr_Get_1A_Parameters;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
            mDSet = new DataSet();
            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

        }
        catch
        {

        }
        return mDSet;

    }
    public DataSet Get1A_EA_Parameters(string ProjectID,string BoilerID)
    {
        String mStoredProcName = String.Empty;
        Cal_2A_SC mCal_2A_SC = null;
        DbCommand mDBCommand = null;
        DataSet mDSet = null;


        try
        {

            mCal_2A_SC = new Cal_2A_SC();
            mStoredProcName = StoredProcedure.spr_Get1A_EA_Parameters;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
            
            currentDatabase.AddInParameter(mDBCommand,"@vProjectID", DbType.String,ProjectID);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
      
            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
            return mDSet;
        }
        catch
        {

        }
        return mDSet;
    }
    public DataSet GetExcessAirCheck(string ProjectID, string BoilerID,string BoileLoad,int ObjectiveID)
    {
        String mStoredProcName = String.Empty;
        Cal_2A_SC mCal_2A_SC = null;
        DbCommand mDBCommand = null;
        DataSet mDSet = null;


        try
        {

            mCal_2A_SC = new Cal_2A_SC();
            mStoredProcName = StoredProcedure.spr_GetExcessAirCheck;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vBoileLoad", DbType.String, BoileLoad);
            currentDatabase.AddInParameter(mDBCommand, "@vObjective", DbType.Int16, ObjectiveID);
         

            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
            return mDSet;
        }
        catch
        {

        }
        return mDSet;
    }
    public DataSet Get_Objective(string ProjectID, string BoilerID)
    {
        String mStoredProcName = String.Empty;
        Cal_2A_SC mCal_2A_SC = null;
        DbCommand mDBCommand = null;
        DataSet mDSet = null;


        try
        {

            mCal_2A_SC = new Cal_2A_SC();
            mStoredProcName = StoredProcedure.spr_Get_Objective;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);

            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
            return mDSet;
        }
        catch
        {

        }
        return mDSet;
    }
    public DataSet GetAirTempParameterForSpecificHumidity(string ProjectID,string BoilerID,string BoileLoad,int objectiveID)
    {
        String mStoredProcName = String.Empty;
        Cal_2A_SC mCal_2A_SC = null;
        DbCommand mDBCommand = null;
        DataSet mDSet = null;
        try
        {
            mCal_2A_SC = new Cal_2A_SC();
            mStoredProcName = StoredProcedure.spr_GetAirTempParameterForSpecificHumidity;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoileLoad);
            currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.String, objectiveID);

            mDSet = new DataSet();
            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

        }
        catch
        {

        }
        return mDSet;

    }
    
    }

}
