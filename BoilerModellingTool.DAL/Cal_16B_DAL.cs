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
using System.Configuration;


namespace BoilerModellingTool.DAL
{
    public class Cal_16B_DAL
    {
        #region " Variables "

        private Database currentDatabase;

        #endregion
        #region " Constructor "

        public Cal_16B_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion

        public DataSet GetInput_For_16B_Calculation(string BoilerID, string ProjectID, string BoilerLoad,int ObjectiveID)
        {
            String mStoredProcName = String.Empty;

            Cal_16B_SC mCal_12B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_12B_SC = new Cal_16B_SC();
                System.Diagnostics.Debug.WriteLine("===== INSIDE 16B BLL =====");
                System.Diagnostics.Debug.WriteLine("BoilerID: " + BoilerID);
                System.Diagnostics.Debug.WriteLine("ProjectID: " + ProjectID);
                System.Diagnostics.Debug.WriteLine("BoilerLoad: " + BoilerLoad);
                System.Diagnostics.Debug.WriteLine("ObjectiveID: " + ObjectiveID);
                mStoredProcName = StoredProcedure.spr_GetInput_For_16B_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int64, ObjectiveID);

                System.Diagnostics.Debug.WriteLine("===== CALLING 16B SP =====");
                System.Diagnostics.Debug.WriteLine("SP Name: spr_XXXX (your SP)");

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                // ✅ CHECK RESULT
                if (mDSet == null)
                {
                    System.Diagnostics.Debug.WriteLine("❌ SP RETURNED NULL");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("✅ SP returned tables: " + mDSet.Tables.Count);

                    if (mDSet.Tables.Count > 0)
                        System.Diagnostics.Debug.WriteLine("✅ Rows in table[0]: " + mDSet.Tables[0].Rows.Count);
                }





                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
            return mDSet;

        }

        public void Insert_16B_Calculation(string BoilerID, string ProjectID, string BoilerLoad, int ObjectiveID, string Value, int PID)
        {
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_16B_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int64, ObjectiveID);

                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.String, PID);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Value);

                currentDatabase.ExecuteNonQuery(mDbCommand);



            }
            catch
            {

            }
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

        //    //Double Enthalpy_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["APH_Flue_gas_Enthalpy"].ToString());

        //    Double Enthalpy_Min = Convert.ToDouble(
        //        mDSet.Tables[0].Rows[0]["VALUE"].ToString()
        //    );

        //    if (Enthalpy_Min < 0)
        //    {
        //        Enthalpy_Min = 0;
        //    }
        //    Double Temp_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
        //    //Double Enthalpy_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["APH_Flue_gas_Enthalpy"].ToString());
        //    Double Enthalpy_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["APH_Air_Enthalpy"].ToString());
        //    Double Input = Convert.ToDouble(InputVal);




        //    mDTable = (Enthalpy_Max - Enthalpy_Min) / (Temp_Max - Temp_Min) * (Input - Temp_Min) + Enthalpy_Min;
        //    return mDTable;
        //}
        //BB
        public Double Get_2A_ReqEnthalpy(string BoilerID, string ProjectID, string InputVal)
        {
            string mStoredProcName = "";
            DbCommand mDBCommand = null;
            DataSet mDSet = null;

            double mDTable = 0.0;

            mStoredProcName = StoredProcedure.spr_16A_CAlculation_Flue_GAS_ReqEnthalpy;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);

            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            // ✅ DEBUG START
            System.Diagnostics.Debug.WriteLine("===== DEBUG 16A DATASET =====");
            for (int t = 0; t < mDSet.Tables.Count; t++)
            {
                System.Diagnostics.Debug.WriteLine("Table: " + t);

                foreach (DataColumn col in mDSet.Tables[t].Columns)
                {
                    System.Diagnostics.Debug.WriteLine("   Column: " + col.ColumnName);
                }

                if (mDSet.Tables[t].Rows.Count > 0)
                {
                    foreach (DataColumn col in mDSet.Tables[t].Columns)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            "   VALUE [" + col.ColumnName + "] = " +
                            mDSet.Tables[t].Rows[0][col.ColumnName]
                        );
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("   ❌ No Rows");
                }
            }
            System.Diagnostics.Debug.WriteLine("===== DEBUG END =====");
            // ✅ DEBUG END

            double Temp_Min = 0;
            double Temp_Max = 0;
            double Enthalpy_Min = 0;
            double Enthalpy_Max = 0;

            // ✅ SAFE READ TABLE 0
            if (mDSet.Tables.Count > 0 && mDSet.Tables[0].Rows.Count > 0)
            {
                double.TryParse(mDSet.Tables[0].Rows[0]["Temp(Deg C)"]?.ToString(), out Temp_Min);
                double.TryParse(mDSet.Tables[0].Rows[0]["VALUE"]?.ToString(), out Enthalpy_Min);
            }

            // ✅ SAFE READ TABLE 1
            if (mDSet.Tables.Count > 1 && mDSet.Tables[1].Rows.Count > 0)
            {
                double.TryParse(mDSet.Tables[1].Rows[0]["Temp(Deg C)"]?.ToString(), out Temp_Max);
                double.TryParse(mDSet.Tables[1].Rows[0]["VALUE"]?.ToString(), out Enthalpy_Max);
            }

            if (Temp_Min == 0)
            {
                Temp_Min = 25;
            }

            if (Enthalpy_Min < 0)
            {
                Enthalpy_Min = 0;
            }

            double Input = 0;
            double.TryParse(InputVal, out Input);

            // ✅ FINAL CALCULATION
            if ((Temp_Max - Temp_Min) != 0)
            {
                mDTable = (Enthalpy_Max - Enthalpy_Min)
                        / (Temp_Max - Temp_Min)
                        * (Input - Temp_Min)
                        + Enthalpy_Min;
            }

            return mDTable;
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

            System.Diagnostics.Debug.WriteLine("===== DEBUG DATASET START =====");

            for (int t = 0; t < mDSet.Tables.Count; t++)
            {
                System.Diagnostics.Debug.WriteLine("Table Index: " + t);

                if (mDSet.Tables[t].Columns.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("❌ No columns");
                    continue;
                }

                foreach (DataColumn col in mDSet.Tables[t].Columns)
                {
                    System.Diagnostics.Debug.WriteLine("   Column: " + col.ColumnName);
                }

                if (mDSet.Tables[t].Rows.Count > 0)
                {
                    for (int c = 0; c < mDSet.Tables[t].Columns.Count; c++)
                    {
                        string colName = mDSet.Tables[t].Columns[c].ColumnName;
                        string val = mDSet.Tables[t].Rows[0][c]?.ToString();

                        System.Diagnostics.Debug.WriteLine(
                            "   VALUE [" + colName + "] = " + val
                        );
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("   ❌ No rows");
                }
            }

            System.Diagnostics.Debug.WriteLine("===== DEBUG DATASET END =====");



            Double Temp_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Temp(Deg C)"].ToString());
            if (Temp_Min == 0)
            {
                Temp_Min = 25;
            }

            //Double Enthalpy_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["APH_Air_Enthalpy"].ToString());

            Double Enthalpy_Min = Convert.ToDouble(
                mDSet.Tables[0].Rows[0]["APH_Air_Enthalpy"].ToString()
            );


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

        public Double Get_2A_ReqEnthalpy2(string BoilerID, string ProjectID, string InputVal)
        {
            String mStoredProcName = String.Empty;



            Cal_16A_SC mCal_16A_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            Double mDTable = 0.0;



            //try




            mCal_16A_SC = new Cal_16A_SC();
            mStoredProcName = StoredProcedure.spr_16A_CAlculation_ReqEnthalpyforCPFlueGas;

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

            Double Enthalpy_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Cp flue gas"].ToString());
            if (Enthalpy_Min < 0)
            {
                Enthalpy_Min = 0;
            }
            Double Temp_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
            Double Enthalpy_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Cp flue gas"].ToString());
            Double Input = Convert.ToDouble(InputVal);




            mDTable = (Enthalpy_Max - Enthalpy_Min) / (Temp_Max - Temp_Min) * (Input - Temp_Min) + Enthalpy_Min;
            return mDTable;



        }

    }



}

        
    

