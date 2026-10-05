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
    public class Cal_6B_DAL
    {
        #region " Variables "

        private Database currentDatabase;

        #endregion
        #region " Constructor "

        public Cal_6B_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion

        public DataSet GetInput_For_6B_Calculation(string BoilerID, string ProjectID, string BoilerLoad, string SectionID, string LAst_5B_Sec,int objective)
        {
            String mStoredProcName = String.Empty;

            Cal_6B_SC mCal_6B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_6B_SC = new Cal_6B_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_6B_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDBCommand, "@vSec_5B_ID", DbType.String, LAst_5B_Sec);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.String, objective);
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            }
            catch
            {

            }
            return mDSet;

        }
        
        public DataTable Get_PID_For_6B_Calculation()
        {
            String mStoredProcName = String.Empty;

            Cal_6B_SC mCal_6B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mCal_6B_SC = new Cal_6B_SC();
                mStoredProcName = StoredProcedure.spr_Get_PID_For_6B_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);


                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                mDTable = mDSet.Tables[0];

            }
            catch
            {

            }
            return mDTable;

        }
        
        public DataSet Insert_6B_Calculation(int pid, string BoilerID, string ProjectID, int ObjectiveID ,string BoilerLoad, string SectionID, double Value)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            //try
            //{
                mStoredProcedure = StoredProcedure.spr_Insert_6B_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.String, pid);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);                
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Convert.ToString(Value));
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
                return mDSet;
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }
        
        public Double Get_2A_Platen_Exit_Temp_ReqTemp(string BoilerID, string ProjectID, string InputVal,string BoilerLoad,int ObjID)
        {
            String mStoredProcName = String.Empty;

           // Cal_5B_SC mCal_5B_SC = null;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            Double mDTable = 0.0;

            try
            {

               // mCal_5B_SC = new Cal_5B_SC();
                mStoredProcName = StoredProcedure.spr_2A_Platen_Exit_Temp_ReqTemp;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vInputValue", DbType.String, InputVal);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, ObjID);
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
        
        public string Get_PreVious_HE_SectionID(string BoilerID, string ProjectID, string SectionID)
        {
            String mStoredProcName = String.Empty;
            Cal_6B_SC mCal_6B_SC = null;
            DbCommand mDbCommand = null;

            DataSet mDSet = null;
            DataTable mDTable = null;
            string Value = null;
            //try
            //{
            mDTable = new DataTable();
            mCal_6B_SC = new Cal_6B_SC();
            mStoredProcName = StoredProcedure.spr_S_parameter_HeatinElement_Sequence;
            mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
            currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
            mDSet = currentDatabase.ExecuteDataSet(mDbCommand);


            for (int i = 1; i < mDSet.Tables.Count; i++)
            {
                mDSet.Tables[0].Merge(mDSet.Tables[i]);
            }

            mDTable = mDSet.Tables[0];

            for (int i = 0; i < mDTable.Rows.Count - 1; i++)
            {
                if (SectionID == mDTable.Rows[i]["SectionID"].ToString())
                {
                    int Sec_number = Convert.ToInt16(mDTable.Rows[i]["SectionNumber"].ToString());
                    string Sec_Type = mDTable.Rows[i]["SectionType"].ToString();
                    if (Sec_number == 1 )
                    {
                        Value = SectionID;
                    }
                    else
                    {
                        Value = mDTable.Rows[i - 1]["SectionID"].ToString();
                    }
                }

            }


            return Value;
        }
        
        public string GetLast_Element_of_HeatingSectionUpperFurnace(string BoilerID, string ProjectID)
        {
            String mStoredProcName = String.Empty;
            Cal_6B_SC mCal_6B_SC = null;
            DbCommand mDbCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;
            string Value = null;          
            mDTable = new DataTable();
            mCal_6B_SC = new Cal_6B_SC();
            mStoredProcName = StoredProcedure.spr_GetLast_Element_of_HeatingSectionUpperFurnace;
            mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
            currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
            mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
            mDTable = mDSet.Tables[0];
            Value = mDSet.Tables[0].Rows[0]["SectionID"].ToString();
            return Value;
        }
        
        public DataSet Get_S_Parameter_Section_ID(string BoilerID, string ProjectID, string BoilerLoad, string SectionID,int ObjectiveID,int Iteration)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Get_S_Parameter_Section_ID;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);


                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoadID", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                //currentDatabase.AddInParameter(mDbCommand, "@vS_Parameter_SectionID", DbType.String, PreviousHeatingSectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vIteration", DbType.String, Iteration);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
                return mDSet;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public DataSet InsertUpadate_6B_S_ParameterValue(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID, double Input,double Output,int Iteration,string NextSectionID)
        {

            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_InsertUpadate_5B_S_ParameterValue;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                //currentDatabase.AddInParameter(mDbCommand, "@vSectionID_S_ParameterValue", DbType.String, SectionID_S_ParameterValue);
                currentDatabase.AddInParameter(mDbCommand, "@vInput", DbType.String, Convert.ToString(Input));
                currentDatabase.AddInParameter(mDbCommand, "@vOutput", DbType.String,Convert.ToString(Output));
                currentDatabase.AddInParameter(mDbCommand, "@vIteration", DbType.Int16, Iteration);
                currentDatabase.AddInParameter(mDbCommand, "@vNextSectionID", DbType.String, NextSectionID);

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
