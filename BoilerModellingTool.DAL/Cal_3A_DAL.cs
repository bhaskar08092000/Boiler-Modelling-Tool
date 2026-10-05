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
    public class Cal_3A_DAL
    {
        #region " Variables "

        private Database currentDatabase;

        #endregion

        #region " Constructor "

        public Cal_3A_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion

        public DataSet GetInput_For_3A_Calculation(string BoilerID, string ProjectID, string BoilerLoad, int Objective)
        {
            String mStoredProcName = String.Empty;

            Cal_3A_SC mCal_3A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_3A_SC = new Cal_3A_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_3A_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, Objective);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
            }
            catch
            {

            }
            return mDSet;
        }
       
        public void Insert_3A_Calculation(string BoilerID, string ProjectID,string Value,int PID,string BoilerLoad,int ObjectiveID)
        {
            
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_3A_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.String, PID);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Value);
                currentDatabase.AddInParameter(mDbCommand,"@vBoilerLoadID", DbType.String,BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, ObjectiveID);

                currentDatabase.ExecuteNonQuery(mDbCommand);
            }
            catch
            {

            }

        }

        public DataSet GetInputForRHFlow(string ProjectID,string BoilerID, int ObjectiveID, string BoilerLoad)
        {
            DataSet mDSet = null;
            Cal_3A_SC mCal_3A_SC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mCal_3A_SC = new Cal_3A_SC();
                mStoredProcName = StoredProcedure.spr_RHFlow_getInputParameters;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

            }
            catch
            {
                
            }

            return mDSet;
        }

        public DataSet EA_getUnburntCarbonLoss(string ProjectID, string BoilerID)
        {
            DataSet  mDSet = null;
            Cal_3A_SC mCal_3A_SC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mCal_3A_SC = new Cal_3A_SC();
                mStoredProcName = StoredProcedure.spr_EA_getUnburntCarbonLoss;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
               // currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, ObjectiveID);
               // currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

            }
            catch
            {

            }

            return mDSet;

        }

        public DataSet RHFlow_getHPHeatersCount(string ProjectID, string BoilerID, int ObjectiveID, string BoilerLoad)
        {
            DataSet mDSet = null;
            Cal_3A_SC mCal_3A_SC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mCal_3A_SC = new Cal_3A_SC();
                mStoredProcName = StoredProcedure.spr_RHFlow_getInputParameters;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

            }
            catch
            {

            }

            return mDSet;

        }

        public DataSet BindHPHeaters( string ProjectID, string BoilerID, int ObjectiveID, string BoilerLoad)
        {
            DataSet mDSet = null;
            Cal_3A_SC mCal_3A_SC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mCal_3A_SC = new Cal_3A_SC();
                mStoredProcName = StoredProcedure.spr_BindHPHeaters;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

            }
            catch
            {

            }

            return mDSet;

        }

        public DataSet RHFlow_getHPHeatersParameters(string ProjectID, string BoilerID, string SectionID)
        {
            DataSet mDSet = null;
            Cal_3A_SC mCal_3A_SC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mCal_3A_SC = new Cal_3A_SC();
                mStoredProcName = StoredProcedure.spr_RHFlow_getHPHeatersParameters;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
             //   currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

            }
            catch
            {

            }

            return mDSet;

        }
            
        
    }
}
