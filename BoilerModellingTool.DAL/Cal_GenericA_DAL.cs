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
    public class Cal_GenericA_DAL
    {
        #region " Variables "

        private Database currentDatabase;

        #endregion

        #region "Constuctor"

        public Cal_GenericA_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }

        #endregion

        public Cal_GenericA_SC Get_InputFor_GenericA(string ProjectID,string BoilerID,string SectionID,string HeatingElement)
        {
            String mStoredProcName = String.Empty;
            Cal_GenericA_SC mCal_GenericA_SC = null;
            DbCommand mDbCommand = null;  
            DataSet mDSet = null;
            DataTable mDTable = null;
            
            try
            {
                mDTable = new DataTable();
                mCal_GenericA_SC = new Cal_GenericA_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_GenericA;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vHeatingElement", DbType.String, HeatingElement);

                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                //Furnace
                mCal_GenericA_SC.Furnace_width = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Value"].ToString());
                mCal_GenericA_SC.Furnace_nose_up_dip_angle = Convert.ToDouble(mDSet.Tables[0].Rows[1]["Value"].ToString());
                mCal_GenericA_SC.Flue_gas_duct_declination_angle_in_FSH_zone = Convert.ToDouble(mDSet.Tables[0].Rows[2]["Value"].ToString());

                //Other Heating Section
                mCal_GenericA_SC.Tube_diameter_of_heating_section = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Value"].ToString());
                mCal_GenericA_SC.Tube_thickness = Convert.ToDouble(mDSet.Tables[1].Rows[1]["Value"].ToString());
                mCal_GenericA_SC.Heating_section_transverse_rows = Convert.ToDouble(mDSet.Tables[1].Rows[2]["Value"].ToString());

                if ((mDSet.Tables[1].Rows[3]["Value"].ToString() == "") || (mDSet.Tables[1].Rows[3]["Value"].ToString() == "-"))
                {
                    mCal_GenericA_SC.Heating_section_average_transverse_spacing = 0.0;
                }
                else
                {
                    mCal_GenericA_SC.Heating_section_average_transverse_spacing = Convert.ToDouble(mDSet.Tables[1].Rows[3]["Value"].ToString());
                }
                //mCal_GenericA_SC.Heating_section_longitudinal_rows = Convert.ToDouble(mDSet.Tables[1].Rows[3]["Value"].ToString());
                mCal_GenericA_SC.Heating_section_longitudinal_rows = Convert.ToDouble(mDSet.Tables[1].Rows[4]["Value"].ToString());


                if ((mDSet.Tables[1].Rows[5]["Value"].ToString() == "") || (mDSet.Tables[1].Rows[5]["Value"].ToString() == "-"))
                {
                    mCal_GenericA_SC.Heating_section_average_longitudinal_spacing = 0.0;
                }
                else
                {
                    mCal_GenericA_SC.Heating_section_average_longitudinal_spacing = Convert.ToDouble(mDSet.Tables[1].Rows[5]["Value"].ToString());
                }
                //mCal_GenericA_SC.Heating_section_depth = Convert.ToDouble(mDSet.Tables[1].Rows[5]["Value"].ToString());
                mCal_GenericA_SC.Number_of_head = Convert.ToDouble(mDSet.Tables[1].Rows[6]["Value"].ToString());
                mCal_GenericA_SC.Heating_section_depth = Convert.ToDouble(mDSet.Tables[1].Rows[7]["Value"].ToString());
                mCal_GenericA_SC.Relative_space_depth_prior_to_heating_element = Convert.ToDouble(mDSet.Tables[1].Rows[8]["Value"].ToString());
                mCal_GenericA_SC.Height_of_flue_duct_at_heating_section_inlet = Convert.ToDouble(mDSet.Tables[1].Rows[9]["Value"].ToString());
                mCal_GenericA_SC.Height_of_flue_duct_at_heating_section_outlet = Convert.ToDouble(mDSet.Tables[1].Rows[10]["Value"].ToString());
                mCal_GenericA_SC.Distance_from_heating_section_to_downstream_heating_section = Convert.ToDouble(mDSet.Tables[1].Rows[11]["Value"].ToString());
                mCal_GenericA_SC.Height_of_flue_duct_at_inlet_of_downstream_heating_element = Convert.ToDouble(mDSet.Tables[1].Rows[12]["Value"].ToString());

                if ((mDSet.Tables[1].Rows[13]["Value"].ToString() == "") || (mDSet.Tables[1].Rows[5]["Value"].ToString() == "-"))
                {
                    mCal_GenericA_SC.Heating_section_area = 0.0;
                }
                else
                {
                    mCal_GenericA_SC.Heating_section_area = Convert.ToDouble(mDSet.Tables[1].Rows[13]["Value"].ToString());
                }
                //mCal_GenericA_SC.Heating_section_area = Convert.ToDouble(mDSet.Tables[1].Rows[13]["Value"].ToString());

            }
            catch
            {

            }

            return mCal_GenericA_SC;
        }

        public DataTable Get_PID_For_GenericA_Calculation()
        {
            String mStoredProcName = String.Empty;

            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mStoredProcName = StoredProcedure.spr_Get_PID_For_GenericA;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                mDTable = mDSet.Tables[0];

            }
            catch
            {

            }

            return mDTable;

        }

        public void Insert_GenericA_Calculation(int PID,string ProjectID,string BoilerID,string SectionID,double Value,string HeatingElement,string Location)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_GenericA_Calculatioin;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.Int16, PID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Convert.ToString(Value));
                currentDatabase.AddInParameter(mDbCommand, "@vHeatingElement", DbType.String, HeatingElement);
                currentDatabase.AddInParameter(mDbCommand, "@vLocation", DbType.String, Location);

                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
