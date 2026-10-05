using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using BoilerModellingTool.SC;

namespace BoilerModellingTool.DAL
{
    public class Cal_13A_DAL
    {
        #region "Variables"

        private Database currentDatabase;

        #endregion

        #region "Constructor"

        public Cal_13A_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }

        #endregion

        public Cal_13A_SC Get_13A_CalculationValues(string ProjectID,string BoilerID)
        {
            DataSet mDSet = null;
            Cal_13A_SC mCal_13A_SC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mStoredProcName = StoredProcedure.spr_GetInut_For_13A_Calculation_ReverseChamber;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                mCal_13A_SC = new Cal_13A_SC();

                mCal_13A_SC.Tube_diameter = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Value"].ToString());
                mCal_13A_SC.Tube_thickness = Convert.ToDouble(mDSet.Tables[0].Rows[1]["Value"].ToString());
                mCal_13A_SC.Reversing_chamber_height = Convert.ToDouble(mDSet.Tables[0].Rows[2]["Value"].ToString());
                mCal_13A_SC.Reversing_chamber_depth = Convert.ToDouble(mDSet.Tables[0].Rows[3]["Value"].ToString());
                mCal_13A_SC.Reversing_chamber_width = Convert.ToDouble(mDSet.Tables[0].Rows[4]["Value"].ToString());

                mCal_13A_SC.Reversing_chamber_configuration_factor = 1;

                mCal_13A_SC.Total_number_of_eco_Hanger_tube_in_zone=Convert.ToDouble(mDSet.Tables[1].Rows[0]["Value"].ToString());
                mCal_13A_SC.Eco_Hanger_diameter=Convert.ToDouble(mDSet.Tables[2].Rows[0]["Value"].ToString());
                mCal_13A_SC.Eco_Hanger_tube_length_in_reverse_chamber_zone = Convert.ToDouble(mDSet.Tables[3].Rows[0]["Value"].ToString());              

            }
            catch(Exception e)
            {

            }

            return mCal_13A_SC;
        }

        public DataTable Get_PID_For_13A_Calculation()
        {
            String mStoredProcName = String.Empty;

            Cal_13A_SC mCal_13A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_13A_SC = new Cal_13A_SC();
                mStoredProcName = StoredProcedure.spr_Get_PID_For_13A_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                mDTable = mDSet.Tables[0];
            }
            catch
            {

            }
            return mDTable;
        }

        public void Insert_13A_Calculation(int PID,string ProjectID,string BoilerID,double Value)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_13A_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.Int16, PID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Convert.ToString(Value));


                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
