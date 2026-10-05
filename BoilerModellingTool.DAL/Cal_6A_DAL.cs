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
   public class Cal_6A_DAL
    {

        #region " Variables "

        private Database currentDatabase;

        #endregion
           

        public Cal_6A_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        
        public string GetLast_Element_of_HeatingSectionUpperFurnace(string BoilerID, string ProjectID)
        {
            String mStoredProcName = String.Empty;
            Cal_6A_SC mCal_6A_SC = null;
            DbCommand mDbCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;
            string Value = null;
            mDTable = new DataTable();
            mCal_6A_SC = new Cal_6A_SC();
            mStoredProcName = StoredProcedure.spr_GetLast_Element_of_HeatingSectionUpperFurnace;
            mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
            currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
            mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
            mDTable = mDSet.Tables[0];
            Value = mDSet.Tables[0].Rows[0]["SectionID"].ToString();
            return Value;
        }

        public DataSet GetInput_For_6A_Calculation(string BoilerID, string ProjectID, string BoilerLoad,string SectionID,string SectionType)
        {
            String mStoredProcName = String.Empty;

            Cal_6A_SC mCal_6A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_6A_SC = new Cal_6A_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_6A_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionType", DbType.String, SectionType);

                //currentDatabase.AddInParameter(mDBCommand, "@vSec_5B_ID", DbType.String, LAst_5B_Sec);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
            return mDSet;

        }

        public DataSet Insert_6A_Values_DAL(int pid, string BoilerID, string ProjectID, string BoilerLoad, string Value, string SectionID, int Objectiveid)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_6A_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.String, pid);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);

                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, Objectiveid);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Value);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);




                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
                return mDSet;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataSet Get_6A_parameter_dal()
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Get6A_parameter;
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
    }
}
