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
   public class Cal_15A_DAL
    {
        #region " Variables "

        private Database currentDatabase;

        #endregion

        #region " Constructor "

        public Cal_15A_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion

        public Cal_15A_SC GetInput_For_15A_Calculation(string BoilerID, string ProjectID, string SectionID)
        {
            String mStoredProcName = String.Empty;

            Cal_15A_SC mCal_15A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_15A_SC = new Cal_15A_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_15A_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                //currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                mCal_15A_SC.Tube_diameter = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Value"].ToString());
                mCal_15A_SC.Tube_thickness = Convert.ToDouble(mDSet.Tables[0].Rows[1]["Value"].ToString());
                mCal_15A_SC.Transverse_rows = Convert.ToDouble(mDSet.Tables[0].Rows[2]["Value"].ToString());
                mCal_15A_SC.Transverse_spacing = Convert.ToDouble(mDSet.Tables[0].Rows[3]["Value"].ToString());
                mCal_15A_SC.Longitudinal_tube_rows = Convert.ToDouble(mDSet.Tables[0].Rows[4]["Value"].ToString());
                mCal_15A_SC.Longitudinal_spacing = Convert.ToDouble(mDSet.Tables[0].Rows[5]["Value"].ToString());
                mCal_15A_SC.Number_of_head = Convert.ToDouble(mDSet.Tables[0].Rows[6]["Value"].ToString());
                mCal_15A_SC.Economizer_height = Convert.ToDouble(mDSet.Tables[0].Rows[7]["Value"].ToString());
                mCal_15A_SC.Relative_space_depth_prior_to_LTSH = Convert.ToDouble(mDSet.Tables[0].Rows[8]["Value"].ToString());
                
                mCal_15A_SC.Backpass_width = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Value"].ToString());
                mCal_15A_SC.Depth_of_flue_duct_at_LTSH_inlet = Convert.ToDouble(mDSet.Tables[1].Rows[1]["Value"].ToString());
                mCal_15A_SC.Depth_of_flue_duct_at_LTSH_outlet = Convert.ToDouble(mDSet.Tables[1].Rows[1]["Value"].ToString());

                mCal_15A_SC.Distance_from_Eco_to_front_and_rear_walls = 514;

                mCal_15A_SC.Heating_Section_Area = Convert.ToDouble(mDSet.Tables[2].Rows[0]["Value"].ToString());
            
            }
            catch
            {

            }
            return mCal_15A_SC;

        }

        public void Insert_15A_Calculation(string BoilerID, string ProjectID, int PID, string SectionID, string Value)
        {

            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_15A_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.String, PID);
                //currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                //currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, Boiler_Load);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Value);

                currentDatabase.ExecuteNonQuery(mDbCommand);
            }
            catch
            {

            }

        }

     }
}

