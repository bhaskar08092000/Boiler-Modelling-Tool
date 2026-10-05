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
    public class Cal_Pressure_DAL
    {
        #region " Variables "

        private Database currentDatabase;

        #endregion
        #region " Constructor "

        public Cal_Pressure_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion



        public DataSet GetInput_For_Cal_Pressure_Calculation(string BoilerID, string ProjectID, string BoilerLoad, int ObjectiveID)
        {
            String mStoredProcName = String.Empty;
            String mStoredProcName1 = String.Empty;

            Cal_Pressure_SC mCal_Pressure_SC = null;
            DbCommand mDBCommand = null;
            DbCommand mDBCommand1 = null;

            DataSet mDSet = null;
            DataSet mDSet1 = null;

            mDSet1 = new DataSet();

            DataTable mDTable = null;


            //try
            //{
            mDTable = new DataTable();
            mCal_Pressure_SC = new Cal_Pressure_SC();
            mStoredProcName = StoredProcedure.spr_GetInput_For_Pressure_Calculation;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);


            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            //}
            //catch
            //{

            //}
            return mDSet;








        }
        public void Insert_Pressure_Calculation(string ProjectID, string BoilerID, string SectionID, string SectionType, Double Value, string BoilerLoad, int ObjectiveID, string Parameter)
        {





            DataSet mDSet3 = null;



            //DataSet mDSet3;
            String mStoredProcName3 = String.Empty;
            DbCommand mDbCommand3 = null;
            //try
            //{
            mDSet3 = new DataSet();
            mStoredProcName3 = StoredProcedure.spr_Insert_Pressure_Calculation;
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




        //public void Insert_Pressure_Calculation_FixedParameters(string ProjectID, string BoilerID, Double Value, string BoilerLoad, int ObjectiveID, string Parameter)
        //{





        //    DataSet mDSet4 = null;



        //    //DataSet mDSet3;
        //    String mStoredProcName4 = String.Empty;
        //    DbCommand mDbCommand4 = null;
        //    //try
        //    //{
        //    mDSet4 = new DataSet();
        //    mStoredProcName4 = StoredProcedure.spr_Insert_Pressure_Calculation_FixedParameters;
        //    mDbCommand4 = currentDatabase.GetStoredProcCommand(mStoredProcName4);
        //    currentDatabase.AddInParameter(mDbCommand4, "@vProjectID", DbType.String, ProjectID);
        //    currentDatabase.AddInParameter(mDbCommand4, "@vBoilerID", DbType.String, BoilerID);

        //    currentDatabase.AddInParameter(mDbCommand4, "@vParameter", DbType.String, Parameter);

        //    currentDatabase.AddInParameter(mDbCommand4, "@vBoilerLoad", DbType.String, BoilerLoad);
        //    currentDatabase.AddInParameter(mDbCommand4, "@vValue", DbType.String, Value.ToString());
        //    currentDatabase.AddInParameter(mDbCommand4, "@vObjectiveID", DbType.Int16, ObjectiveID);
        //    mDSet4 = currentDatabase.ExecuteDataSet(mDbCommand4);
        //    //}
        //    //catch
        //    //{

        //    //}

        //}
        public void Insert_Pressure_Calculation_FixedParameters(string ProjectID, string BoilerID, Double Value, string BoilerLoad, int ObjectiveID, string Parameter)
        {
            DbCommand mDbCommand4;

            string mStoredProcName4 = StoredProcedure.spr_Insert_Pressure_Calculation_FixedParameters;
            mDbCommand4 = currentDatabase.GetStoredProcCommand(mStoredProcName4);

            currentDatabase.AddInParameter(mDbCommand4, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDbCommand4, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand4, "@vParameter", DbType.String, Parameter);
            currentDatabase.AddInParameter(mDbCommand4, "@vBoilerLoad", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(mDbCommand4, "@vValue", DbType.Double, Value);
            currentDatabase.AddInParameter(mDbCommand4, "@vObjectiveID", DbType.Int16, ObjectiveID);

            // ✅ DEBUG
            System.Diagnostics.Debug.WriteLine($"INSERT → {Parameter} | {Value}");

            // ✅ CORRECT EXECUTION
            currentDatabase.ExecuteNonQuery(mDbCommand4);
        }


        public DataSet GetInput_For_TotalHeatingSections(string BoilerID, string ProjectID)
        {
            String mStoredProcName = String.Empty;

            Cal_Pressure_SC mCal_Pressure_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet1 = null;
            DataTable mDTable1 = null;

            //try
            //{
            mDTable1 = new DataTable();
            mCal_Pressure_SC = new Cal_Pressure_SC();
            mStoredProcName = StoredProcedure.spr_Get_CountOf_HeatingElements;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);


            mDSet1 = currentDatabase.ExecuteDataSet(mDBCommand);

            //}
            //catch
            //{

            //}
            return mDSet1;

        }

        public DataSet GetInput_For_TotalHeatingSectionsOfCrossDuct(string BoilerID, string ProjectID)
        {
            String mStoredProcName = String.Empty;

            Cal_Pressure_SC mCal_Pressure_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet2 = null;
            DataTable mDTable2 = null;

            //try
            //{
            mDTable2 = new DataTable();
            mCal_Pressure_SC = new Cal_Pressure_SC();
            mStoredProcName = StoredProcedure.spr_CountOf_HeatingElement_LocationOfCrossDuct;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);


            mDSet2 = currentDatabase.ExecuteDataSet(mDBCommand);

            //}
            //catch
            //{

            //}
            return mDSet2;

        }








    }

}






