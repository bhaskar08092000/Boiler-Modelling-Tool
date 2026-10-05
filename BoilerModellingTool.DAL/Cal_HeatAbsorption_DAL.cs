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
    public class Cal_HeatAbsorption_DAL
    {
        #region " Variables "

        private Database currentDatabase;

        #endregion
        #region " Constructor "
        public Cal_HeatAbsorption_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion



        public DataSet GetInput_For_HeatAbsorption_Calculation(string BoilerID, string ProjectID, string BoilerLoad, int ObjectiveID)
        {
            String mStoredProcName = String.Empty;


            Cal_HeatAbsorption_SC mCal_HeatAbsorption_SC = null;
            DbCommand mDBCommand = null;


            DataSet mDSet = null;


            DataTable mDTable = null;


            //try
            //{
            mDTable = new DataTable();
            mCal_HeatAbsorption_SC = new Cal_HeatAbsorption_SC();
            mStoredProcName = StoredProcedure.spr_GetInput_For_HeatAbsorption_Calculation;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);


            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);


            return mDSet;








        }


        public void Insert_HeatAbsorption_Calculation(string ProjectID, string BoilerID, string SectionID, string SectionType, Double Value, string BoilerLoad, int ObjectiveID, string Parameter)
        {





            DataSet mDSet3 = null;



            //DataSet mDSet3;
            String mStoredProcName3 = String.Empty;
            DbCommand mDbCommand3 = null;
            //try
            //{
            mDSet3 = new DataSet();
            mStoredProcName3 = StoredProcedure.spr_Insert_HeatAbsorption_Calculation;
            mDbCommand3 = currentDatabase.GetStoredProcCommand(mStoredProcName3);
            currentDatabase.AddInParameter(mDbCommand3, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDbCommand3, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand3, "@vSectionID", DbType.String, SectionID);
            currentDatabase.AddInParameter(mDbCommand3, "@vParameter", DbType.String, Parameter);
            currentDatabase.AddInParameter(mDbCommand3, "@vSectionType", DbType.String, SectionType);
            currentDatabase.AddInParameter(mDbCommand3, "@vBoilerLoad", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(mDbCommand3, "@vValue", DbType.String, Value.ToString());
            currentDatabase.AddInParameter(mDbCommand3, "@vObjectiveID", DbType.Int16, ObjectiveID);
            mDSet3 = currentDatabase.ExecuteDataSet(mDbCommand3);
            //}
            //catch
            //{

            //}

        }



    }



}
