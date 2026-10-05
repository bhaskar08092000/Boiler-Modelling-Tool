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
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;
using System.Net.NetworkInformation;

namespace BoilerModellingTool.DAL
{

    public class Cal_4B_DAL
    {
        #region " Variables "

        private Database currentDatabase;

        #endregion
        #region " Constructor "

        public Cal_4B_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion

        public DataSet GetInput_For_4B_Calculation(string BoilerID, string ProjectID, string BoilerLoad)
        {
            String mStoredProcName = String.Empty;

            Cal_4B_SC mCal_4B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_4B_SC = new Cal_4B_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_4B_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);



                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                System.Diagnostics.Debug.WriteLine("---- 4B INPUT DATA DEBUG ----");

                if (mDSet == null)
                {
                    System.Diagnostics.Debug.WriteLine("❌ Dataset is NULL");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Tables count: " + mDSet.Tables.Count);

                    for (int t = 0; t < mDSet.Tables.Count; t++)
                    {
                        System.Diagnostics.Debug.WriteLine("Table " + t + " Rows: " + mDSet.Tables[t].Rows.Count);

                        for (int r = 0; r < mDSet.Tables[t].Rows.Count; r++)
                        {
                            foreach (DataColumn col in mDSet.Tables[t].Columns)
                            {
                                System.Diagnostics.Debug.Write(col.ColumnName + "=" + mDSet.Tables[t].Rows[r][col] + " | ");
                            }
                            System.Diagnostics.Debug.WriteLine("");
                        }
                    }
                }


            }
            catch
            {

            }
            return mDSet;

        }

        public Double Get_2A_TemperingAir_ReqEnthalpy(string BoilerID, string ProjectID, string InputVal)
        {
            String mStoredProcName = String.Empty;

            Cal_4B_SC mCal_4B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            Double mDTable = 0.0;

            try
            {

                mCal_4B_SC = new Cal_4B_SC();
                mStoredProcName = StoredProcedure.spr_2A_TemperingAir_ReqEnthalpy;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                Double Temp_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Temp(Deg C)"].ToString());
              

                Double Enthalpy_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Ioa(kJ/kg)"].ToString());
                Double Temp_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
                Double Enthalpy_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Ioa(kJ/kg)"].ToString());
                Double Input = Convert.ToDouble(InputVal);
                if (Convert.ToDouble(InputVal) < 100)
                {
                    Temp_Min = 25;
                    Enthalpy_Min = 0;
                }



                mDTable = (Enthalpy_Max - Enthalpy_Min) / (Temp_Max - Temp_Min) * (Input - Temp_Min) + Enthalpy_Min;
                return mDTable;
            }
            catch
            {

            }
            return mDTable;

        }

        public Double Get_2A_PrimaryAir_ReqEnthalpy(string BoilerID, string ProjectID, string InputVal)
        {
            String mStoredProcName = String.Empty;

            Cal_4B_SC mCal_4B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            Double mDTable = 0.0;

            try
            {

                mCal_4B_SC = new Cal_4B_SC();
                mStoredProcName = StoredProcedure.spr_2A_PrimaryAir_ReqEnthalpy;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                Double Temp_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Temp(Deg C)"].ToString());

                Double Enthalpy_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Ioa(kJ/kg)"].ToString());
                Double Temp_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
                Double Enthalpy_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Ioa(kJ/kg)"].ToString());
                Double Input = Convert.ToDouble(InputVal);
                if (Convert.ToDouble(InputVal) < 100)
                {
                    Temp_Min = 25;
                    Enthalpy_Min = 0;
                }



                mDTable = (Enthalpy_Max - Enthalpy_Min) / (Temp_Max - Temp_Min) * (Input - Temp_Min) + Enthalpy_Min;
                return mDTable;
            }
            catch
            {

            }
            return mDTable;

        }

        public Double Get_2A_Recirculated_gas_temperature(string BoilerID, string ProjectID, string BoilerLoad)
        {
            String mStoredProcName = String.Empty;

            Cal_4B_SC mCal_4B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            Double mDTable = 0.0;

            try
            {

                mCal_4B_SC = new Cal_4B_SC();
                mStoredProcName = StoredProcedure.spr_Get_Recirculated_gas_temperature;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                return mDTable;
            }
            catch
            {

            }
            return mDTable;

        }
        
        public Double Get_2A_Recirculated_gas_enthalpy(string BoilerID, string ProjectID, string InputVal)
        {
            String mStoredProcName = String.Empty;

            Cal_4B_SC mCal_4B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            Double mDTable = 0.0;

            try
            {

                mCal_4B_SC = new Cal_4B_SC();
                mStoredProcName = StoredProcedure.spr_2A_PrimaryAir_ReqEnthalpy;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                Double Temp_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Temp(Deg C)"].ToString());
                Double Enthalpy_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Ioa(kJ/kg)"].ToString());
                Double Temp_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
                Double Enthalpy_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Ioa(kJ/kg)"].ToString());
                Double Input = Convert.ToDouble(InputVal);


                mDTable = (Enthalpy_Max - Enthalpy_Min) / (Temp_Max - Temp_Min) * (Input - Temp_Min) + Enthalpy_Min;
                return mDTable;
            }
            catch
            {

            }
            return mDTable;

        }
   
       
        public Double Get_2A_FEGT_ReqEnthalpy(string BoilerID, string ProjectID, string InputVal)
        {
            
            String mStoredProcName = String.Empty;

            Cal_4B_SC mCal_4B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            Double mDTable = 0.0;

            try
            {

                mCal_4B_SC = new Cal_4B_SC();
                mStoredProcName = StoredProcedure.spr_2A_FEGT_ReqEnthalpy;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                Double Temp_Min =Math.Round(Convert.ToDouble(mDSet.Tables[0].Rows[0]["Temp(Deg C)"].ToString()),0,MidpointRounding.AwayFromZero);
                Double Enthalpy_Min = Math.Round(Convert.ToDouble(mDSet.Tables[0].Rows[0]["LowerFurnaceAlpha"].ToString()), 0, MidpointRounding.AwayFromZero);
                Double Temp_Max = Math.Round(Convert.ToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString()), 0, MidpointRounding.AwayFromZero);
                Double Enthalpy_Max = Math.Round(Convert.ToDouble(mDSet.Tables[1].Rows[0]["LowerFurnaceAlpha"].ToString()), 0, MidpointRounding.AwayFromZero);
                Double Input =Math.Round( Convert.ToDouble(InputVal),2);

                if (Convert.ToDouble(InputVal) < 100)
                {
                    Temp_Min = 25;
                    Enthalpy_Min = 0;
                }


                //=(J149-J148)/(J147-J146)*(L146-J146)+J148
                mDTable = (Enthalpy_Max - Enthalpy_Min) / (Temp_Max - Temp_Min) * (Input - Temp_Min) + Enthalpy_Min;
                return mDTable;

                	

            }
            catch
            {

            }
            return mDTable;

        }
        
        public Double Get_2A_AdiabaticTemp_ReqTemp(string BoilerID, string ProjectID, string InputVal)
        {
            String mStoredProcName = String.Empty;

            Cal_4B_SC mCal_4B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            Double mDTable = 0.0;

            try
            {

                mCal_4B_SC = new Cal_4B_SC();
                mStoredProcName = StoredProcedure.spr_2A_AdiabaticTemp_ReqTemp;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                Double Temp_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Temp(Deg C)"].ToString());
                Double Enthalpy_Min = Convert.ToDouble(mDSet.Tables[0].Rows[0]["LowerFurnaceAlpha"].ToString());
                Double Temp_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
                Double Enthalpy_Max = Convert.ToDouble(mDSet.Tables[1].Rows[0]["LowerFurnaceAlpha"].ToString());
                Double Input = Convert.ToDouble(InputVal);

                if (Convert.ToDouble(InputVal) < 100)
                {
                    Temp_Min = 25;
                    Enthalpy_Min = 0;
                }



                //mDTable = (Enthalpy_Max - Enthalpy_Min) / (Temp_Max - Temp_Min) * (Input - Temp_Min) + Enthalpy_Min;
                mDTable = (Temp_Max - Temp_Min) / (Enthalpy_Max - Enthalpy_Min) * (Input - Enthalpy_Min) + Temp_Min;
                return mDTable;
            }
            catch
            {

            }
            return mDTable;

        }

        //---New changes 19-08-2020 by Preeti ubale 
        public DataSet Get_submodule_heating_section_in_upper_furnace(String BoilerID,String ProjectID)
        {
            String mStoredProcName = String.Empty;

            Cal_4B_SC mCal_4B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_4B_SC = new Cal_4B_SC();
                mStoredProcName = StoredProcedure.spr_Get_submodule_heating_section_in_upper_furnace;
                Debug.WriteLine("SP Name: " + mStoredProcName);
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                //currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                System.Diagnostics.Debug.WriteLine("Project_ID: " + ProjectID);
                System.Diagnostics.Debug.WriteLine("Boiler_ID: " + BoilerID);



                // ✅ ✅ DEBUG START HERE
                System.Diagnostics.Debug.WriteLine("---- SUBMODULE DATA DEBUG ----");

                if (mDSet == null)
                {
                    System.Diagnostics.Debug.WriteLine("❌ DataSet is NULL");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Tables count: " + mDSet.Tables.Count);

                    for (int t = 0; t < mDSet.Tables.Count; t++)
                    {
                        System.Diagnostics.Debug.WriteLine("Table " + t + " Rows: " + mDSet.Tables[t].Rows.Count);

                        // print actual data
                        for (int r = 0; r < mDSet.Tables[t].Rows.Count; r++)
                        {
                            foreach (DataColumn col in mDSet.Tables[t].Columns)
                            {
                                var val = mDSet.Tables[t].Rows[r][col] == DBNull.Value ? "NULL" : mDSet.Tables[t].Rows[r][col].ToString();
                                System.Diagnostics.Debug.Write(col.ColumnName + "=" + val + " | ");
                            }
                            System.Diagnostics.Debug.WriteLine("");
                        }
                    }
                }
                // ✅ ✅ DEBUG END


            }
            catch
            {

            }
            return mDSet;

        }

        //public DataSet Get_Design_Area_From_Section_ID_in_4B(string BoilerID, string ProjectID, string SectionID)
        //{
        //    String mStoredProcName = String.Empty;

        //    Cal_4B_SC mCal_4B_SC = null;
        //    DbCommand mDBCommand = null;
        //    DataSet mDSet = null;
        //    DataTable mDTable = null;

        //    try
        //    {
        //        mDTable = new DataTable();
        //        mCal_4B_SC = new Cal_4B_SC();
        //        mStoredProcName = StoredProcedure.spr_Get_Design_Area_From_Section_ID_in_4B;
        //        mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
        //        currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
        //        currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
        //        currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);
        //        mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

        //    }
        //    catch
        //    {

        //    }
        //    return mDSet;

        //}
        public DataSet Get_Design_Area_From_Section_ID_in_4B(string BoilerID, string ProjectID, string SectionID)
        {
            try
            {
                DbCommand mDBCommand = currentDatabase.GetStoredProcCommand(
                    StoredProcedure.spr_Get_Design_Area_From_Section_ID_in_4B);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);

                DataSet mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                // ✅ Debug what SP returns
                System.Diagnostics.Debug.WriteLine("✅ DAL SUCCESS");
                System.Diagnostics.Debug.WriteLine("Tables Count: " + (mDSet?.Tables.Count ?? 0));

                if (mDSet != null)
                {
                    for (int i = 0; i < mDSet.Tables.Count; i++)
                    {
                        System.Diagnostics.Debug.WriteLine($"Table[{i}] Rows: {mDSet.Tables[i].Rows.Count}");
                    }
                }

                return mDSet ?? new DataSet(); // ✅ never return null
            }
            catch (Exception ex)
            {
                // ✅ THIS IS THE KEY FIX
                System.Diagnostics.Debug.WriteLine("💥 DAL ERROR:");
                System.Diagnostics.Debug.WriteLine(ex.ToString());

                throw; // 🔴 MUST KEEP THIS
            }
        }


        public Double Get_Tsat_p(Double P_Val)
        {
            String mStoredProcName = String.Empty;

            Cal_4B_SC mCal_4B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            Double mDTable = 0.0;

            try
            {

                mCal_4B_SC = new Cal_4B_SC();
                mStoredProcName = StoredProcedure.spr_Tsat_p;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@p", DbType.String, P_Val);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                mDTable = Convert.ToDouble(mDSet.Tables[1].Rows[0]["fromSIunit_T"].ToString());

            }
            catch
            {

            }
            return mDTable;

        }
        
        public DataTable Get_PID_For_4B_Calculation()
        {
            String mStoredProcName = String.Empty;

            Cal_4B_SC mCal_4B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_4B_SC = new Cal_4B_SC();
                mStoredProcName = StoredProcedure.spr_Get_PID_For_4B_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);


                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                mDTable = mDSet.Tables[0];

            }
            catch
            {

            }
            return mDTable;

        }

        public string GetLast_Element_of_HeatingSectionUpperFurnace(string BoilerID, string ProjectID)
        {
            String mStoredProcName = String.Empty;
            String mStoredProc_Name = String.Empty;

            Cal_4B_SC mCal_4B_SC = null;
            DbCommand mDbCommand = null;

            DataSet mDSet = null;
            DataTable mDTable = null;
            string Value = null;
            //try
            //{
            mDTable = new DataTable();
            mCal_4B_SC = new Cal_4B_SC();
            mStoredProcName = StoredProcedure.spr_GetLast_Element_of_HeatingSectionUpperFurnace;
            mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
            currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
            mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
            Value = mDSet.Tables[0].Rows[0]["SectionID"].ToString();
            return Value; 
        }

        public Double Get_avg_loc_vert_dir_4B_Cal(string BoilerID, string ProjectID, string SectionID,double water_wall_HF,double Nose_inlet_HF)
        {
            String mStoredProcName = String.Empty;

            Cal_4B_SC mCal_4B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            Double mDTable = 0.0;

            try
            {

                mCal_4B_SC = new Cal_4B_SC();
                mStoredProcName = StoredProcedure.spr_Get_avg_loc_vert_dir_4B_Cal;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDBCommand, "@vWaterWall_Heat_Flux", DbType.String, water_wall_HF);
                currentDatabase.AddInParameter(mDBCommand, "@vNose_inlet_Heat_Flux", DbType.String, Nose_inlet_HF);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                mDTable = Convert.ToDouble(mDSet.Tables[0].Rows[0]["avg_location_vertical_direction"].ToString());

            }
            catch
            {

            }
            return mDTable;
        }

        public DataSet Insert_4B_Calculation(int pid, string BoilerID, string ProjectID, string BoilerLoad,int Objective, string Value)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_4B_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.String, pid);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, Objective);


                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Value);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
                return mDSet;
            }
            catch (Exception ex)
            {
                throw;
            }
      
        
        }
    }

}
