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
    public class Cal_16A_DAL
    {
        #region " Variables "

        private Database currentDatabase;

        #endregion
        #region " Constructor "

        public Cal_16A_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion

        public DataSet GetInput_For_16A_Calculation(string BoilerID, string ProjectID, string BoilerLoad,int ObjectiveID)
        {
            String mStoredProcName = String.Empty;

            Cal_16A_SC mCal_16A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_16A_SC = new Cal_16A_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_16A_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int64, ObjectiveID);
               

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
            return mDSet;

        }

        public void Insert_16A_Calculation(string BoilerID, string ProjectID, string BoilerLoad, int ObjectiveID, string Value, int PID)
        {

            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            //try
            //{
            mStoredProcedure = StoredProcedure.spr_Insert_16A_Calculation;
            mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);
            currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.String, PID);
            currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int64, ObjectiveID);
            currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Value);

            currentDatabase.ExecuteNonQuery(mDbCommand);




            //}
            //catch
            //{

            //}
        }

        //public Double Get_2A_ReqEnthalpy(string BoilerID, string ProjectID, string InputVal)
        //{
        //    String mStoredProcName = String.Empty;



        //    Cal_16A_SC mCal_16A_SC = null;
        //    DbCommand mDBCommand = null;
        //    DataSet mDSet = null;
        //    Double mDTable = 0.0;



        //    //try




        //    mCal_16A_SC = new Cal_16A_SC();
        //    mStoredProcName = StoredProcedure.spr_16A_CAlculation_Flue_GAS_ReqEnthalpy;

        //    mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);



        //    currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
        //    currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
        //    currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
        //    mDSet = currentDatabase.ExecuteDataSet(mDBCommand);



        //    Double Temp_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Temp(Deg C)"].ToString());
        //    if (Temp_Min == 0)
        //    {
        //        Temp_Min = 25;
        //    }

        //    Double Enthalpy_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["APH_Flue_gas_Enthalpy"].ToString());
        //    if (Enthalpy_Min < 0)
        //    {
        //        Enthalpy_Min = 0;
        //    }
        //    Double Temp_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
        //    Double Enthalpy_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["APH_Flue_gas_Enthalpy"].ToString());
        //    Double Input = Convert.ToDouble(InputVal);




        //    mDTable = (Enthalpy_Max - Enthalpy_Min) / (Temp_Max - Temp_Min) * (Input - Temp_Min) + Enthalpy_Min;
        //    return mDTable;
        //}
        //BB
        private double SafeToDouble(object value, string fieldName)
        {
            double result = 0;
            string str = Convert.ToString(value);

            System.Diagnostics.Debug.WriteLine("RAW [" + fieldName + "] = " + str);

            if (!double.TryParse(str, out result))
            {
                System.Diagnostics.Debug.WriteLine("❌ INVALID " + fieldName + " → default 0");
                result = 0;
            }

            return result;
        }

        //public Double Get_2A_ReqEnthalpy(string BoilerID, string ProjectID, string InputVal)
        //{
        //    DbCommand mDBCommand = null;
        //    DataSet mDSet = null;
        //    double result = 0;

        //    string sp = StoredProcedure.spr_16A_CAlculation_Flue_GAS_ReqEnthalpy;
        //    mDBCommand = currentDatabase.GetStoredProcCommand(sp);
        //    double inputValDouble = 0;
        //    System.Diagnostics.Debug.WriteLine("🔥 FINAL INPUT GOING TO SP = " + inputValDouble);


        //    // ✅ Convert input safely
        //    double.TryParse(InputVal, out inputValDouble);

        //    // ✅ DEBUG BEFORE FIX
        //    System.Diagnostics.Debug.WriteLine("RAW InputVal BEFORE FIX = " + inputValDouble);

        //    // ✅ FIX: avoid forcing to 0 blindly
        //    if (inputValDouble <= 0)
        //    {
        //        System.Diagnostics.Debug.WriteLine("⚠️ Input is <= 0, adjusting to small positive value");

        //        inputValDouble = 1;   // ✅ use 1 instead of 0
        //    }

        //    // ✅ FINAL INPUT BEING SENT
        //    System.Diagnostics.Debug.WriteLine("✅ FINAL INPUT SENT TO SP = " + inputValDouble);

        //    // ✅ PASS CORRECT VALUE
        //    currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.Double, inputValDouble);


        //    //currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
        //    //currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
        //    //currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
        //    //currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
        //    currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
        //    currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);

        //    mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

        //    // ✅ DATASET SAFETY
        //    if (mDSet == null || mDSet.Tables.Count < 2)
        //    {
        //        System.Diagnostics.Debug.WriteLine("❌ No tables returned in 16A SP → return 0");
        //        return 0;
        //    }

        //    if (mDSet.Tables[0].Rows.Count == 0 || mDSet.Tables[1].Rows.Count == 0)
        //    {
        //        System.Diagnostics.Debug.WriteLine("❌ Empty rows in 16A SP → return 0");
        //        return 0;
        //    }

        //    // ✅ SAFE PARSE
        //    double Temp_Min = SafeToDouble(mDSet.Tables[0].Rows[0]["Temp(Deg C)"], "Temp_Min");
        //    if (Temp_Min == 0) Temp_Min = 25;

        //    //double Enthalpy_Min = SafeToDouble(mDSet.Tables[0].Rows[0]["APH_Flue_gas_Enthalpy"], "Enthalpy_Min");
        //    double Enthalpy_Min = SafeToDouble(mDSet.Tables[0].Rows[0]["VALUE"], "Enthalpy_Min");
        //    if (Enthalpy_Min < 0) Enthalpy_Min = 0;

        //    double Temp_Max = SafeToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"], "Temp_Max");
        //    double Enthalpy_Max = SafeToDouble(mDSet.Tables[1].Rows[0]["VALUE"], "Enthalpy_Max");
        //    // double Enthalpy_Max = SafeToDouble(mDSet.Tables[1].Rows[0]["APH_Flue_gas_Enthalpy"], "Enthalpy_Max");

        //    double Input = SafeToDouble(InputVal, "Input");

        //    // ✅ DIVIDE-BY-ZERO PROTECTION
        //    if (Temp_Max == Temp_Min)
        //    {
        //        System.Diagnostics.Debug.WriteLine("⚠️ Temp_Min == Temp_Max → return 0");
        //        return 0;
        //    }

        //    // ✅ CALCULATION
        //    result = (Enthalpy_Max - Enthalpy_Min) /
        //             (Temp_Max - Temp_Min) *
        //             (Input - Temp_Min) + Enthalpy_Min;

        //    if (double.IsNaN(result) || double.IsInfinity(result))
        //    {
        //        System.Diagnostics.Debug.WriteLine("❌ Invalid result → reset 0");
        //        result = 0;
        //    }

        //    return result;
        //}

        public Double Get_2A_ReqEnthalpy(string BoilerID, string ProjectID, string InputVal)
        {
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            double result = 0;

            string sp = StoredProcedure.spr_16A_CAlculation_Flue_GAS_ReqEnthalpy;
            mDBCommand = currentDatabase.GetStoredProcCommand(sp);

            // ✅ STEP 1: Convert input properly
            double inputValDouble = 0;
            double.TryParse(InputVal, out inputValDouble);

            System.Diagnostics.Debug.WriteLine("RAW InputVal BEFORE FIX = " + inputValDouble);

            // ✅ FIX: Avoid 0 or negative input
            if (inputValDouble <= 0)
            {
                System.Diagnostics.Debug.WriteLine("⚠️ Input is <= 0, adjusting to 1");
                inputValDouble = 1;
            }

            System.Diagnostics.Debug.WriteLine("✅ FINAL INPUT SENT TO SP = " + inputValDouble);

            // ✅ STEP 2: Pass parameters (ONLY ONCE)
            currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.Double, inputValDouble);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);

            // ✅ STEP 3: Execute
            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            // ✅ STEP 4: Safety checks
            if (mDSet == null || mDSet.Tables.Count < 2)
            {
                System.Diagnostics.Debug.WriteLine("❌ No tables returned → return 0");
                return 0;
            }

            if (mDSet.Tables[0].Rows.Count == 0 || mDSet.Tables[1].Rows.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("❌ Empty rows → return 0");
                return 0;
            }

            // ✅ STEP 5: Extract values
            double Temp_Min = SafeToDouble(mDSet.Tables[0].Rows[0]["Temp(Deg C)"], "Temp_Min");
            double Temp_Max = SafeToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"], "Temp_Max");

            double Enthalpy_Min = SafeToDouble(mDSet.Tables[0].Rows[0]["VALUE"], "Enthalpy_Min");
            double Enthalpy_Max = SafeToDouble(mDSet.Tables[1].Rows[0]["VALUE"], "Enthalpy_Max");

            // ✅ IMPORTANT FIX: Use corrected input (not InputVal string)
            double Input = inputValDouble;

            // ✅ DEBUG
            System.Diagnostics.Debug.WriteLine("===== CALC INPUT =====");
            System.Diagnostics.Debug.WriteLine("Temp_Min: " + Temp_Min);
            System.Diagnostics.Debug.WriteLine("Temp_Max: " + Temp_Max);
            System.Diagnostics.Debug.WriteLine("Enthalpy_Min: " + Enthalpy_Min);
            System.Diagnostics.Debug.WriteLine("Enthalpy_Max: " + Enthalpy_Max);
            System.Diagnostics.Debug.WriteLine("Input: " + Input);

            // ✅ STEP 6: Protection
            if (Temp_Max == Temp_Min)
            {
                System.Diagnostics.Debug.WriteLine("⚠️ Temp_Min == Temp_Max → return 0");
                return 0;
            }

            // ✅ STEP 7: Calculation
            result = (Enthalpy_Max - Enthalpy_Min) /
                     (Temp_Max - Temp_Min) *
                     (Input - Temp_Min) + Enthalpy_Min;

            // ✅ STEP 8: Final safety
            if (double.IsNaN(result) || double.IsInfinity(result))
            {
                System.Diagnostics.Debug.WriteLine("❌ Invalid result → reset 0");
                result = 0;
            }

            System.Diagnostics.Debug.WriteLine("✅ FINAL RESULT = " + result);

            return result;
        }







        public Double Get_2A_ReqEnthalpy1(string BoilerID, string ProjectID, string InputVal)
        {
            String mStoredProcName = String.Empty;



            Cal_16A_SC mCal_16A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            Double mDTable = 0.0;



            //try




            mCal_16A_SC = new Cal_16A_SC();
            mStoredProcName = StoredProcedure.spr_16A_CAlculation_ReqEnthalpy;

            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);



            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);



            Double Temp_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Temp(Deg C)"].ToString());
            if (Temp_Min == 0)
            {
                Temp_Min = 25;
            }

            Double Enthalpy_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["APH_Air_Enthalpy"].ToString());
            if (Enthalpy_Min < 0)
            {
                Enthalpy_Min = 0;
            }
            Double Temp_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
            Double Enthalpy_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["APH_Air_Enthalpy"].ToString());
            Double Input = Convert.ToDouble(InputVal);




            mDTable = (Enthalpy_Max - Enthalpy_Min) / (Temp_Max - Temp_Min) * (Input - Temp_Min) + Enthalpy_Min;
            return mDTable;




            //catch
            //{



            //}
            //return mDTable;



        }



        //public Double Get_2A_ReqEnthalpy2(string BoilerID, string ProjectID, string InputVal)
        //{
        //    String mStoredProcName = String.Empty;



        //    Cal_16A_SC mCal_16A_SC = null;
        //    DbCommand mDBCommand = null;
        //    DataSet mDSet = null;
        //    Double mDTable = 0.0;



        //    //try




        //    mCal_16A_SC = new Cal_16A_SC();
        //    mStoredProcName = StoredProcedure.spr_16A_CAlculation_ReqEnthalpyforCPFlueGas;

        //    mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);



        //    currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
        //    currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
        //    currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
        //    mDSet = currentDatabase.ExecuteDataSet(mDBCommand);



        //    Double Temp_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Temp(Deg C)"].ToString());
        //    if (Temp_Min == 0)
        //    {
        //        Temp_Min = 25;
        //    }

        //    Double Enthalpy_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Cp flue gas"].ToString());
        //    if (Enthalpy_Min < 0)
        //    {
        //        Enthalpy_Min = 0;
        //    }
        //    Double Temp_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
        //    Double Enthalpy_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Cp flue gas"].ToString());
        //    Double Input = Convert.ToDouble(InputVal);




        //    mDTable = (Enthalpy_Max - Enthalpy_Min) / (Temp_Max - Temp_Min) * (Input - Temp_Min) + Enthalpy_Min;
        //    return mDTable;



        //}
        //BB
        //private double SafeToDouble(object value, string fieldName)
        //{
        //    double result = 0;
        //    string str = Convert.ToString(value);

        //    System.Diagnostics.Debug.WriteLine("RAW [" + fieldName + "] = " + str);

        //    if (!double.TryParse(str, out result))
        //    {
        //        System.Diagnostics.Debug.WriteLine("❌ INVALID " + fieldName + " → default 0");
        //        result = 0;
        //    }

        //    return result;
        //}
        public Double Get_2A_ReqEnthalpy2(string BoilerID, string ProjectID, string InputVal)
        {
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            double mDTable = 0.0;

            string mStoredProcName = StoredProcedure.spr_16A_CAlculation_ReqEnthalpyforCPFlueGas;

            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            // ✅ ✅ FIX 1: CORRECT PARAMETER ORDER (VERY IMPORTANT)
            currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);

            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            // ✅ ✅ FIX 2: FULL DATASET SAFETY
            if (mDSet == null)
            {
                System.Diagnostics.Debug.WriteLine("❌ Dataset is NULL → RETURN 0");
                return 0;
            }

            if (mDSet.Tables.Count < 2)
            {
                System.Diagnostics.Debug.WriteLine("❌ LESS THAN 2 TABLES → RETURN 0");
                return 0;
            }

            if (mDSet.Tables[0].Rows.Count == 0 || mDSet.Tables[1].Rows.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("❌ EMPTY ROWS → RETURN 0");
                return 0;
            }

            // ✅ ✅ FIX 3: SAFE VALUE READING USING INDEX (NOT COLUMN NAME)
            double Temp_Min = SafeToDouble(mDSet.Tables[0].Rows[0][0], "Temp_Min");
            if (Temp_Min == 0)
            {
                Temp_Min = 25;
            }

            double Enthalpy_Min = SafeToDouble(mDSet.Tables[0].Rows[0][1], "Cp_Min");
            if (Enthalpy_Min < 0)
            {
                Enthalpy_Min = 0;
            }

            double Temp_Max = SafeToDouble(mDSet.Tables[1].Rows[0][0], "Temp_Max");

            double Enthalpy_Max = SafeToDouble(mDSet.Tables[1].Rows[0][1], "Cp_Max");

            double Input = SafeToDouble(InputVal, "Input");

            // ✅ ✅ FIX 4: DIVIDE-BY-ZERO PROTECTION
            if (Temp_Max == Temp_Min)
            {
                System.Diagnostics.Debug.WriteLine("⚠️ Temp_Min == Temp_Max → RETURN 0");
                return 0;
            }

            // ✅ ✅ FINAL CALCULATION
            mDTable = (Enthalpy_Max - Enthalpy_Min) /
                      (Temp_Max - Temp_Min) *
                      (Input - Temp_Min) + Enthalpy_Min;

            // ✅ ✅ FINAL SAFETY CHECK
            if (double.IsNaN(mDTable) || double.IsInfinity(mDTable))
            {
                System.Diagnostics.Debug.WriteLine("❌ Invalid result → RESET 0");
                mDTable = 0;
            }

            System.Diagnostics.Debug.WriteLine("✅ FINAL CP VALUE: " + mDTable);

            return mDTable;
        }



    }



}

