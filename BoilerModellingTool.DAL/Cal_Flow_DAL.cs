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
    public class Cal_Flow_DAL
    {

        #region "Variables"

        private Database currentDatabase;

        #endregion

        #region "Constructor"

        public Cal_Flow_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }

        #endregion

        public Cal_Flow_SC Get_InputFor_Flow_Calculations(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, int TypeOfBoiler, int SHDesuperheatingSpray)
        {
            DataSet mDSet = null, mDSet1 = null,mDSet2=null;
            Cal_Flow_SC mCal_Flow_SC = null;
            String mStoredProcName = String.Empty;
            String mStoredProcName1 = String.Empty;
            String mStoredProfName2 = String.Empty;
            DbCommand mDbCommand = null, mDbCommand1 = null,mDbCommand2=null;
            mDSet = new DataSet();
            mDSet1 = new DataSet();
            mDSet2 = new DataSet();

            try
            {
                mCal_Flow_SC = new Cal_Flow_SC();

                //Procedure 1
                mStoredProcName = StoredProcedure.spr_GetInput_For_Flow_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                //Procedure 2
                mStoredProcName1 = StoredProcedure.spr_GetInput_For_Flow_BackpassRatioCalculation;
                mDbCommand1 = currentDatabase.GetStoredProcCommand(mStoredProcName1);

                currentDatabase.AddInParameter(mDbCommand1, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand1, "@vBoilerID", DbType.String, BoilerID);

                mDSet1 = currentDatabase.ExecuteDataSet(mDbCommand1);

                //Procedure 3
                mStoredProfName2 = StoredProcedure.spr_Flow_GetInput_For_DesuperheatingSprayEnthalpy;
                mDbCommand2 = currentDatabase.GetStoredProcCommand(mStoredProfName2);

                currentDatabase.AddInParameter(mDbCommand2, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand2, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand2, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand2, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand2, "@vType", DbType.Int16, SHDesuperheatingSpray);

                mDSet2 = currentDatabase.ExecuteDataSet(mDbCommand2);

                //Procedure 1 Parameters
                mCal_Flow_SC.Main_steam_flow_rate = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Value"].ToString());

                if (ObjectiveID == 1)
                {
                    mCal_Flow_SC.Hot_RH_flow_rate = Convert.ToDouble(mDSet.Tables[0].Rows[1]["Value"].ToString());
                }
                else
                {
                    mCal_Flow_SC.Hot_RH_flow_rate = 0.0;
                }

                mCal_Flow_SC.Economiser_feed_water_flow_rate = Convert.ToDouble(mDSet.Tables[0].Rows[2]["Value"].ToString());
                mCal_Flow_SC.SH_De_superheating_spray_stage_1 = Convert.ToDouble(mDSet.Tables[0].Rows[3]["Value"].ToString());

                if ((mDSet.Tables[0].Rows[4]["Value"].ToString() == "") || (mDSet.Tables[0].Rows[4]["Value"].ToString() == "-") || (mDSet.Tables[0].Rows[4]["Value"].ToString() == "0"))
                {
                    mCal_Flow_SC.SH_De_superheating_spray_stage_2_if_any = Convert.ToDouble(mDSet.Tables[0].Rows[4]["Value"].ToString());
                }
                else
                {
                    mCal_Flow_SC.SH_De_superheating_spray_stage_2_if_any = 0.0;
                }

                if (mDSet.Tables[0].Rows[5]["Value"].ToString() == "" || mDSet.Tables[0].Rows[5]["Value"].ToString() == "-" || mDSet.Tables[0].Rows[5]["Value"].ToString() == "0")
                {
                    mCal_Flow_SC.RH_De_superheating_spray = 0.0;
                }
                else
                {
                    mCal_Flow_SC.RH_De_superheating_spray = Convert.ToDouble(mDSet.Tables[0].Rows[5]["Value"].ToString());
                }

                if (TypeOfBoiler == 2)
                {
                    mCal_Flow_SC.Blow_down_percentage_if_it_is_sub_critical_boiler = 0.0;
                }
                else
                {
                    mCal_Flow_SC.Blow_down_percentage_if_it_is_sub_critical_boiler = Convert.ToDouble(mDSet.Tables[0].Rows[6]["Value"].ToString());
                }

                //Procedure 2 Parameters
                mCal_Flow_SC.Sidewall_front_portion_tube_diameter = Convert.ToDouble(mDSet1.Tables[0].Rows[0]["Value"].ToString());
                mCal_Flow_SC.Sidewall_front_portion_tube_numbers = Convert.ToDouble(mDSet1.Tables[0].Rows[1]["Value"].ToString());
                mCal_Flow_SC.Sidewall_rear_portion_tube_diameter = Convert.ToDouble(mDSet1.Tables[0].Rows[2]["Value"].ToString());
                mCal_Flow_SC.Sidewall_rear_portion_tube_numbers = Convert.ToDouble(mDSet1.Tables[0].Rows[3]["Value"].ToString());
                mCal_Flow_SC.Front_wall_tube_diameter = Convert.ToDouble(mDSet1.Tables[0].Rows[4]["Value"].ToString());
                mCal_Flow_SC.Front_wall_tube_numbers = Convert.ToDouble(mDSet1.Tables[0].Rows[5]["Value"].ToString());
                mCal_Flow_SC.Extended_steam_wall_header_tube_diameter = Convert.ToDouble(mDSet1.Tables[0].Rows[6]["Value"].ToString());
                mCal_Flow_SC.Extended_steam_wall_header_tube_numbers = Convert.ToDouble(mDSet1.Tables[0].Rows[7]["Value"].ToString());


                //Procedure 3 Parameters
                mCal_Flow_SC.Desuperheating_spray_temperature = Convert.ToDouble(mDSet2.Tables[0].Rows[0]["Value"].ToString());
                mCal_Flow_SC.Desuperheating_spray_pressure = Convert.ToDouble(mDSet2.Tables[1].Rows[0]["Value"].ToString());

            }
            catch (Exception e)
            {

            }

            return mCal_Flow_SC;
        }

        public string Get_SectionID_From_MiscellaneousSH(string ProjectID, string BoilerID, int SectionNumber)
        {
            string SectionID = null;
            DataSet mDSet;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            try
            {
                mDSet = new DataSet();
                mStoredProcName = StoredProcedure.spr_Flow_Get_SectionID_from_Miscellneous;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionNumber", DbType.Int16, SectionNumber);
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
                SectionID = mDSet.Tables[0].Rows[0]["SectionID"].ToString();
            }
            catch
            {

            }

            return SectionID;
        }

        public int Get_SectionNumber_Of_SectionID(string ProjectID, string BoilerID, string SectionID)
        {
            int SectionNumber = 0;
            DataSet mDSet;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            try
            {
                mDSet = new DataSet();
                mStoredProcName = StoredProcedure.spr_Flow_SectionNumber_Of_SectionID;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
                SectionNumber = Convert.ToInt16(mDSet.Tables[0].Rows[0]["SectionNumber"].ToString());
            }
            catch
            {

            }

            return SectionNumber;
        }

        public void Insert_Flow_FixedParameters(string ProjectID, string BoilerID, string SectionType, Double Value, string BoilerLoad, int ObjectiveID)
        {
            DataSet mDSet;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            try
            {
                mDSet = new DataSet();
                mStoredProcName = StoredProcedure.spr_Insert_Flow_Calculation_FixedParameters;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionType", DbType.String, SectionType);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Value.ToString());
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
            }
            catch
            {

            }

        }
    
        public void Insert_Flow_DynamicParameters(string ProjectID,string BoilerID,string SectionID,string SectionType,Double Value,string BoilerLoad,int ObjectiveID)
        {
            DataSet mDSet;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            try
            {
                mDSet = new DataSet();
                mStoredProcName = StoredProcedure.spr_Insert_Flow_Calculation_DynamicParamters;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionType", DbType.String, SectionType);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Value.ToString());
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
            }
            catch
            {

            }
        }
    }
}
