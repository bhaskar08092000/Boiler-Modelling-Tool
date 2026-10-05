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
    public class Cal_5C_DAL
    {

        #region " Variables "

        private Database currentDatabase;

        #endregion
        #region " Constructor "

        public Cal_5C_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion

        public DataSet Get_Input_For_5C_Calculation(string BoilerID, string ProjectID, string BoilerLoad, String SectionID)
        {
            String mStoredProcName = String.Empty;

            Cal_5C_SC mCal_5C_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_5C_SC = new Cal_5C_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_Metal_Temp_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
            return mDSet;

        }
        public DataSet Get_Input_For_5C_Calculation_Of_Area(string BoilerID, string ProjectID, string SectionID, int ObjectiveID, int TypesTubes, int Tube_Position)
        {

            String mStoredProcName = String.Empty;

            Cal_5C_SC mCal_5C_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_5C_SC = new Cal_5C_SC();
                mStoredProcName = StoredProcedure.spr_Get_Input_For_5C_Calculation_Of_Area;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.String, ObjectiveID);
                currentDatabase.AddInParameter(mDBCommand, "@vTypesTubes", DbType.String, TypesTubes);
                currentDatabase.AddInParameter(mDBCommand, "@VTube_Position", DbType.String, Tube_Position);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
            return mDSet;

        }
        public DataSet Get_Thermal_Conductivity_For_5C_Calculation_(string BoilerID, string ProjectID, string SectionID, int ObjectiveID, int TypesTubes, int Tube_Position)
        {

            String mStoredProcName = String.Empty;

            Cal_5C_SC mCal_5C_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_5C_SC = new Cal_5C_SC();
                mStoredProcName = StoredProcedure.spr_Get_Thermal_Conductivity_For_5C_Calculation_;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.String, ObjectiveID);
                currentDatabase.AddInParameter(mDBCommand, "@vTypesTubes", DbType.String, TypesTubes);
                currentDatabase.AddInParameter(mDBCommand, "@VTube_Position", DbType.String, Tube_Position);


                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
            return mDSet;

        }
        public DataSet Get_Input_For_5C_Calculation_Vertical_Section(int TypesTubes_1, int TypesTubes_2)
        {
            String mStoredProcName = String.Empty;

            Cal_5C_SC mCal_5C_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_5C_SC = new Cal_5C_SC();
                mStoredProcName = StoredProcedure.spr_Get_Input_For_5C_Calculation_Vertical_Section;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDBCommand, "@vTypesTubes_1", DbType.String, TypesTubes_1);
                currentDatabase.AddInParameter(mDBCommand, "@vTypesTubes_2", DbType.String, TypesTubes_2);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
            return mDSet;


        }
        public DataSet Get_Input_For_5C_Ratio_DR_each_section(string BoilerID, string ProjectID, string SectionID, int ObjectiveID, int TypesTubes, int Tube_Position)
        {

            String mStoredProcName = String.Empty;

            Cal_5C_SC mCal_5C_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_5C_SC = new Cal_5C_SC();
                mStoredProcName = StoredProcedure.spr_Get_Input_For_5C_Ratio_DR_each_section;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.String, ObjectiveID);
                currentDatabase.AddInParameter(mDBCommand, "@vTypesTubes", DbType.String, TypesTubes);
                currentDatabase.AddInParameter(mDBCommand, "@VTube_Position", DbType.String, Tube_Position);


                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
            return mDSet;

        }
        public DataSet Get_MAterial_Section_For_5C_Calculation(string BoilerID, string ProjectID, string SectionID, int ObjectiveID, int TypesTubes, int Tube_Position, string Sec_NAme)
        {

            String mStoredProcName = String.Empty;

            Cal_5C_SC mCal_5C_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_5C_SC = new Cal_5C_SC();
                mStoredProcName = StoredProcedure.spr_Get_MAterial_Section_For_5C_Calculation_;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.String, ObjectiveID);
                currentDatabase.AddInParameter(mDBCommand, "@vTypesTubes", DbType.String, TypesTubes);
                currentDatabase.AddInParameter(mDBCommand, "@VTube_Position", DbType.String, Tube_Position);
                currentDatabase.AddInParameter(mDBCommand, "@VSection_NAme", DbType.String, Sec_NAme);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
            return mDSet;

        }
        public void Insert_Metal_Temperature_For_Heating_Element(string BoilerID, string ProjectID, string sectionID,
            string BoilerLoad, string ObjectiveID, string LoopID, string Steam_Temperature_last,
            string MAx_metal_temp, string Section_metal_temp,
            string Material_metal_temp, string metal_temp_last, string material_Metal_temp_last
            )
        {


            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_Metal_Temperature_For_Heating_Element;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);
                currentDatabase.AddInParameter(mDbCommand, "@VBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@VProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, sectionID);
                currentDatabase.AddInParameter(mDbCommand, "@VBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@VLoopID ", DbType.String, LoopID);
                currentDatabase.AddInParameter(mDbCommand, "@VOutlet_steam_temperature_of_the_loop", DbType.String, Steam_Temperature_last);
                currentDatabase.AddInParameter(mDbCommand, "@VMax_metal_temp", DbType.String, MAx_metal_temp);
                currentDatabase.AddInParameter(mDbCommand, "@VSection_of_Max_Metal_Temperature", DbType.String, Section_metal_temp);
                currentDatabase.AddInParameter(mDbCommand, "@VMaterial_in_Metal_Temperature", DbType.String, Material_metal_temp);
                currentDatabase.AddInParameter(mDbCommand, "@VMetal_Temperature_at_Outlet", DbType.String, metal_temp_last);
                currentDatabase.AddInParameter(mDbCommand, "@VMaterial_Metal_Temperature_at_Outlet", DbType.String, material_Metal_temp_last);


                currentDatabase.ExecuteNonQuery(mDbCommand);

            }
            catch
            {

            }

        }
    }
}
