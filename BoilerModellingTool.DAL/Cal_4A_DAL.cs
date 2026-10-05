using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using BoilerModellingTool.SC;
using System.Threading.Tasks;

namespace BoilerModellingTool.DAL
{
    public class Cal_4A_DAL
    {
         #region " Variables "

        private Database currentDatabase;

        #endregion
        #region " Constructor "

        public Cal_4A_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion

        public DataSet GetInput_For_4A_Calculation(string BoilerID, string ProjectID,string BoilerLoad,int ObjectiveID,string SectionID)
        {
            String mStoredProcName = String.Empty;

            Cal_4A_SC mCal_4A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;
           

            try
            {
                mDTable = new DataTable();
                mCal_4A_SC = new Cal_4A_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_4A_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String,BoilerLoad );
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.String, ObjectiveID);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
            return mDSet;

        }
        public void Insert_4A_Calculation(string BoilerID, string ProjectID, string Value, int PID,string BoilerLoad,int objectiveID)
        {
            //BoilerLoad = "TT";
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_4A_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.String, PID);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Value);
                 currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, objectiveID);
              
                currentDatabase.ExecuteNonQuery(mDbCommand);
            }
            catch
            {

            }

        }
    }
}
