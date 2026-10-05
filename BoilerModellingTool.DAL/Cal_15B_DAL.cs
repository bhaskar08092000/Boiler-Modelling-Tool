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
    public class Cal_15B_DAL
    {

        #region " Variables "

        private Database currentDatabase;

        #endregion

        #region " Constructor "
          public Cal_15B_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion

        public Cal_15B_SC GetInput_For_15B_Calculation(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID, string PreviousSectionID, string PreviousLocation, string GroupID, string PreviousHeatingSectionID, int PendantRH)
        {
            String mStoredProcName = String.Empty;

            Cal_15B_SC mCal_15B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_15B_SC = new Cal_15B_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_15B_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDBCommand, "@vPreviousSectionID", DbType.String, PreviousSectionID);
                currentDatabase.AddInParameter(mDBCommand, "@vPreviousLocation", DbType.String, PreviousLocation);
                currentDatabase.AddInParameter(mDBCommand, "@vGroupID", DbType.String, GroupID);
                currentDatabase.AddInParameter(mDBCommand, "@vSParameterSectionID", DbType.String, PreviousHeatingSectionID);
                currentDatabase.AddInParameter(mDBCommand, "@vPendantRH", DbType.Int16, PendantRH);
                //currentDatabase.AddInParameter(mDBCommand, "@vPreviousSectionID", DbType.String, PreviousSectionID);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                //GI
                mCal_15B_SC.Tube_diameter = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Value"].ToString());
                mCal_15B_SC.Tube_thickness = Convert.ToDouble(mDSet.Tables[0].Rows[1]["Value"].ToString());
                mCal_15B_SC.Heating_element_depth = Convert.ToDouble(mDSet.Tables[0].Rows[2]["Value"].ToString());
                mCal_15B_SC.Relative_space_depth_prior_to_economizer = Convert.ToDouble(mDSet.Tables[0].Rows[3]["Value"].ToString());

                //15A
                mCal_15B_SC.Relative_transverse_pitch = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Value"].ToString());
                mCal_15B_SC.Relative_vertical_pitch = Convert.ToDouble(mDSet.Tables[1].Rows[1]["Value"].ToString());
                mCal_15B_SC.Tube_total_heating_area = Convert.ToDouble(mDSet.Tables[1].Rows[2]["Value"].ToString());
                mCal_15B_SC.Heating_area_of_roof_tubes_in_zone = Convert.ToDouble(mDSet.Tables[1].Rows[3]["Value"].ToString());
                mCal_15B_SC.Side_Water_wall_heating_area__within_panel = Convert.ToDouble(mDSet.Tables[1].Rows[4]["Value"].ToString());
                mCal_15B_SC.Gas_average_flow_area = Convert.ToDouble(mDSet.Tables[1].Rows[5]["Value"].ToString());
                mCal_15B_SC.Steam_flow_area = Convert.ToDouble(mDSet.Tables[1].Rows[6]["Value"].ToString());
                mCal_15B_SC.Effective_radiation_layer_thickness = Convert.ToDouble(mDSet.Tables[1].Rows[7]["Value"].ToString());
                mCal_15B_SC.Correction_factor_for_tube_rows = Convert.ToDouble(mDSet.Tables[1].Rows[8]["Value"].ToString());

                //Flue Gas Properties
                mCal_15B_SC.Enthalpy_of_flue_gas_into_panel = Convert.ToDouble(mDSet.Tables[2].Rows[1]["Value"].ToString());
                mCal_15B_SC.Temperature_of_flue_gas_into_panel = Convert.ToDouble(mDSet.Tables[2].Rows[0]["Value"].ToString());

                //3A
                mCal_15B_SC.Heat_preservation_coefficient = Convert.ToDouble(mDSet.Tables[3].Rows[0]["Value"].ToString());

                //4B
                mCal_15B_SC.Partial_pressure_of_triatomic_gases = Convert.ToDouble(mDSet.Tables[4].Rows[0]["Value"].ToString());

                //1A
                mCal_15B_SC.Flue_gas_total_volume = Convert.ToDouble(mDSet.Tables[5].Rows[0]["Upper furnace"].ToString());
                mCal_15B_SC.Volume_fraction_of_water_vapor = Convert.ToDouble(mDSet.Tables[5].Rows[1]["Upper furnace"].ToString());
                mCal_15B_SC.Volume_fraction_of_triatomic_gases = Convert.ToDouble(mDSet.Tables[5].Rows[2]["Upper furnace"].ToString());
                mCal_15B_SC.Gas_density = Convert.ToDouble(mDSet.Tables[5].Rows[3]["Upper furnace"].ToString());
                mCal_15B_SC.Dimensionless_concentration_of_fly_ash = Convert.ToDouble(mDSet.Tables[5].Rows[4]["Upper furnace"].ToString());

                //1A and PI
                mCal_15B_SC.Design_fuel_consumption = (Convert.ToDouble(mDSet.Tables[6].Rows[0]["Value"].ToString())) / 3.6;

                //PI
                mCal_15B_SC.Mean_diameter_of_ash_particle = Convert.ToDouble(mDSet.Tables[7].Rows[0]["Value"].ToString());

                //HC
                mCal_15B_SC.Furnace_pressure = Convert.ToDouble(mDSet.Tables[8].Rows[0]["Value"].ToString());

                //Pressure
                mCal_15B_SC.Economizer_inlet_pressure = Convert.ToDouble(mDSet.Tables[9].Rows[0]["Value"].ToString());
                mCal_15B_SC.Economizer_outlet_pressure = Convert.ToDouble(mDSet.Tables[9].Rows[1]["Value"].ToString()); 

                //Flow
                mCal_15B_SC.Heating_section_flow_rate = (Convert.ToDouble(mDSet.Tables[10].Rows[0]["Value"].ToString())) * 1000;

                //PI
                mCal_15B_SC.Economizer_outlet_temperature_design = Convert.ToDouble(mDSet.Tables[11].Rows[0]["Value"].ToString());

                //S-parameter
                mCal_15B_SC.Economizer_inlet_temperature = Convert.ToDouble(mDSet.Tables[12].Rows[0]["Input"].ToString());

                //HC Input
                mCal_15B_SC.Fouling_uniformity_coefficient = Convert.ToDouble(mDSet.Tables[13].Rows[1]["Value"].ToString());

                mCal_15B_SC.Fuel_correction_coefficient = Convert.ToDouble(mDSet.Tables[13].Rows[0]["Value"].ToString());

                mCal_15B_SC.Ash_deposit_coefficient = Convert.ToDouble(mDSet.Tables[13].Rows[2]["Value"].ToString());

                mCal_15B_SC.Heating_section_effectiveness_coefficent = Convert.ToDouble(mDSet.Tables[14].Rows[0]["Value"].ToString());

                mCal_15B_SC.Flue_gas_flow_fraction_through_the_element = Convert.ToDouble(mDSet.Tables[15].Rows[0]["Value"].ToString());

            }
            catch
            {

            }
            return mCal_15B_SC;

        }

        //public Double Get_2A_PrimaryAir_ReqEnthalpy(string ProjectID, string BoilerID,string BoilerLoad,int ObjectiveID, string InputVal)
        //{
        //    String mStoredProcName = String.Empty;

        //    Cal_15B_SC mCal_15B_SC = null;
        //    DbCommand mDBCommand = null;
        //    DataSet mDSet = null;
        //    Double ValueTemp = 0.0;


        //    mCal_15B_SC = new Cal_15B_SC();
        //    mStoredProcName = StoredProcedure.spr_GenericB_Calculate_Temperature;
        //    mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

        //    currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
        //    currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
        //    currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
        //    currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
        //    currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
        //    mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

        //    Double Temp_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Temp(Deg C)"].ToString());

        //    Double Enthalpy_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["LowerFurnaceAlpha"].ToString());
        //    Double Temp_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
        //    Double Enthalpy_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["LowerFurnaceAlpha"].ToString());
        //    Double Input = Convert.ToDouble(InputVal);

        //    ValueTemp = (Temp_Max - Temp_Min) / (Enthalpy_Max - Enthalpy_Min) * (Input - Enthalpy_Min) + Temp_Min;
        //    //mDTable =  / (Temp_Max - Temp_Min) * (Input - Temp_Min) + Enthalpy_Min;
        //    return ValueTemp;

        //}
        //BB
        public Double Get_2A_PrimaryAir_ReqEnthalpy(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string InputVal)
        {
            string mStoredProcName = string.Empty;

            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            double ValueTemp = 0.0;

            // ✅ INPUT DEBUG
            System.Diagnostics.Debug.WriteLine("===== 15B INPUT PARAMETERS =====");
            System.Diagnostics.Debug.WriteLine("ProjectID: " + ProjectID);
            System.Diagnostics.Debug.WriteLine("BoilerID: " + BoilerID);
            System.Diagnostics.Debug.WriteLine("BoilerLoad: " + BoilerLoad);
            System.Diagnostics.Debug.WriteLine("ObjectiveID: " + ObjectiveID);
            System.Diagnostics.Debug.WriteLine("Raw InputVal: " + InputVal);

            // ✅ SAFE CONVERSION
            double Input = 0;

            if (!double.TryParse(InputVal, out Input))
            {
                System.Diagnostics.Debug.WriteLine("❌ INVALID INPUT STRING → default 0");
                Input = 0;
            }

            // ✅ ✅ CRITICAL FIX → CLAMP INPUT RANGE
            if (Input < 0)
            {
                System.Diagnostics.Debug.WriteLine("⚠️ Input below range → forcing 0");
                Input = 0;
            }

            if (Input > 2100)
            {
                System.Diagnostics.Debug.WriteLine("⚠️ Input above range → forcing 2100");
                Input = 2100;
            }

            System.Diagnostics.Debug.WriteLine("✅ FINAL SAFE INPUT: " + Input);

            // ✅ PREPARE SP CALL
            mStoredProcName = StoredProcedure.spr_GenericB_Calculate_Temperature;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);

            // ✅ IMPORTANT: SEND DECIMAL, NOT STRING
            currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.Decimal, Input);

            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            // ✅ DATASET DEBUG
            System.Diagnostics.Debug.WriteLine("===== 15B DATASET DEBUG =====");
            System.Diagnostics.Debug.WriteLine("Tables Count: " + mDSet.Tables.Count);

            for (int t = 0; t < mDSet.Tables.Count; t++)
            {
                DataTable table = mDSet.Tables[t];

                System.Diagnostics.Debug.WriteLine("---- TABLE INDEX: " + t);
                System.Diagnostics.Debug.WriteLine("Columns Count: " + table.Columns.Count);
                System.Diagnostics.Debug.WriteLine("Rows Count: " + table.Rows.Count);

                foreach (DataColumn col in table.Columns)
                {
                    System.Diagnostics.Debug.WriteLine("Column: " + col.ColumnName);
                }

                for (int r = 0; r < table.Rows.Count; r++)
                {
                    string rowData = "";
                    foreach (DataColumn col in table.Columns)
                    {
                        rowData += col.ColumnName + "=" + table.Rows[r][col] + " | ";
                    }
                    System.Diagnostics.Debug.WriteLine("Row[" + r + "] => " + rowData);
                }
            }

            // ✅ SAFE VARIABLE INIT
            double Temp_Min = 0, Temp_Max = 0, Enthalpy_Min = 0, Enthalpy_Max = 0;

            // ✅ HANDLE DATASET SHAPES
            if (mDSet.Tables.Count >= 2 &&
                mDSet.Tables[0].Rows.Count > 0 &&
                mDSet.Tables[1].Rows.Count > 0)
            {
                // ✅ NORMAL CASE
                double.TryParse(mDSet.Tables[0].Rows[0]["Temp(Deg C)"].ToString(), out Temp_Min);
                double.TryParse(mDSet.Tables[0].Rows[0]["LowerFurnaceAlpha"].ToString(), out Enthalpy_Min);

                double.TryParse(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString(), out Temp_Max);
                double.TryParse(mDSet.Tables[1].Rows[0]["LowerFurnaceAlpha"].ToString(), out Enthalpy_Max);
            }
            else if (mDSet.Tables.Count >= 2 && mDSet.Tables[1].Rows.Count > 0)
            {
                // ✅ SINGLE ROW CASE
                System.Diagnostics.Debug.WriteLine("⚠️ ONLY ONE DATA POINT → USING SAME VALUE");

                double.TryParse(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString(), out Temp_Min);
                double.TryParse(mDSet.Tables[1].Rows[0]["LowerFurnaceAlpha"].ToString(), out Enthalpy_Min);

                Temp_Max = Temp_Min;
                Enthalpy_Max = Enthalpy_Min;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("❌ NO VALID DATA → RETURN 0");
                return 0;
            }

            // ✅ DEBUG CALC INPUT
            System.Diagnostics.Debug.WriteLine("===== CALC INPUT =====");
            System.Diagnostics.Debug.WriteLine("Temp_Min: " + Temp_Min);
            System.Diagnostics.Debug.WriteLine("Temp_Max: " + Temp_Max);
            System.Diagnostics.Debug.WriteLine("Enthalpy_Min: " + Enthalpy_Min);
            System.Diagnostics.Debug.WriteLine("Enthalpy_Max: " + Enthalpy_Max);
            System.Diagnostics.Debug.WriteLine("Input: " + Input);

            // ✅ NaN PROTECTION
            if (double.IsNaN(Temp_Min) || double.IsNaN(Temp_Max) ||
                double.IsNaN(Enthalpy_Min) || double.IsNaN(Enthalpy_Max) ||
                double.IsNaN(Input))
            {
                System.Diagnostics.Debug.WriteLine("❌ NaN DETECTED → RETURN 0");
                return 0;
            }

            // ✅ DIVIDE-BY-ZERO PROTECTION
            if (Enthalpy_Max == Enthalpy_Min)
            {
                System.Diagnostics.Debug.WriteLine("⚠️ SAME ENTHALPY → RETURN Temp_Min");
                return Temp_Min;
            }

            // ✅ FINAL CALCULATION
            ValueTemp = (Temp_Max - Temp_Min) /
                        (Enthalpy_Max - Enthalpy_Min) *
                        (Input - Enthalpy_Min) + Temp_Min;

            // ✅ FINAL SAFETY
            if (double.IsNaN(ValueTemp) || double.IsInfinity(ValueTemp))
            {
                System.Diagnostics.Debug.WriteLine("❌ FINAL VALUE INVALID → RESET TO 0");
                ValueTemp = 0;
            }

            System.Diagnostics.Debug.WriteLine("✅ FINAL VALUE: " + ValueTemp);

            return ValueTemp;
        }


        //public void Insert_15B_Calculation(string ProjectID, string BoilerID, string Boiler_Load, int ObjectiveID, int PID, string SectionID, double Value)
        //{

        //    DbCommand mDbCommand = null;
        //    String mStoredProcedure = String.Empty;
        //    try
        //    {
        //        mStoredProcedure = StoredProcedure.spr_Insert_15B_Calculation;
        //        mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);
        //        currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
        //        currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
        //        currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.String, PID);
        //        currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.String, ObjectiveID);
        //        currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
        //        currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, Boiler_Load);
        //        currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Convert.ToString(Value));

        //        currentDatabase.ExecuteNonQuery(mDbCommand);
        //    }
        //    catch
        //    {

        //    }

        //}
        public void Insert_15B_Calculation(
    string ProjectID,
    string BoilerID,
    string Boiler_Load,
    int ObjectiveID,
    int PID,
    string SectionID,   // will be ignored by SP mapping
    double Value)
        {
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;

            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_15B_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                // ✅ CORRECT TYPES

                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);

                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.Int32, PID);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int32, ObjectiveID);

                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, Boiler_Load);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Value.ToString());


                System.Diagnostics.Debug.WriteLine(
                    $"✅ Insert 15B → PID:{PID}, SectionID:{SectionID}, Value:{Value}"
                );

                currentDatabase.ExecuteNonQuery(mDbCommand);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"❌ ERROR in Insert_15B_Calculation: {ex.Message}"
                );
            }
        }

        public DataTable Get_PID_For_15B_Calculation()
        {
            String mStoredProcName = String.Empty;

            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mStoredProcName = StoredProcedure.spr_Get_PID_For_15B_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                mDTable = mDSet.Tables[0];

            }
            catch
            {

            }
            return mDTable;
        }
    }
}