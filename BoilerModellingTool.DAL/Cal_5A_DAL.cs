using System;
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

    public class Cal_5A_DAL
    {

          #region " Variables "

        private Database currentDatabase;

        #endregion
        #region " Constructor "

        public Cal_5A_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion
        public DataSet GetInput_For_5A_Calculation(string BoilerID, string ProjectID, string SectionID)
        {
            String mStoredProcName = String.Empty;
            Cal_5A_SC mCal_5A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;
            try
            {
                mDTable = new DataTable();
                mCal_5A_SC = new Cal_5A_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_5A_Calculation;
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
    
        public DataTable Get_PID_For_5A_Calculation()
        {
            String mStoredProcName = String.Empty;

            Cal_5A_SC mCal_5A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_5A_SC = new Cal_5A_SC();
                mStoredProcName = StoredProcedure.spr_Get_PID_For_5A_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                mDTable = mDSet.Tables[0];

            }
            catch
            {

            }
            return mDTable;

        }


        public DataSet Insert_5A_Calculation(int pid, string BoilerID, string ProjectID,string BoilerLoad,string SectionID, int ObjectiveID, string Value)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_5A_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.String, pid);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, ObjectiveID);
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
