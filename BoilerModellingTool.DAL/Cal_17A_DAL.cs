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
    public class Cal_17A_DAL
    {
        #region "Variables"

        private Database currentDatabase;

        #endregion

        #region "Constructor"

        public Cal_17A_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }

        #endregion

        public Cal_17A_SC Get_InputFor_17A_Calculation(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SHSectionID,string GroupIDSH,string RHSectionID,string GroupIDRH)
        {
            DataSet mDSet = null;
            Cal_17A_SC mCal_17A_SC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mCal_17A_SC = new Cal_17A_SC();

                mStoredProcName = StoredProcedure.spr_GetInput_For_17A_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vSHSectionID", DbType.String,SHSectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vGroupIDSH", DbType.String, GroupIDSH);
                currentDatabase.AddInParameter(mDbCommand, "@vRHSectionID", DbType.String,RHSectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vGroupIDRH", DbType.String, GroupIDRH);
                //currentDatabase.AddInParameter(mDbCommand, "@vRHSteamperatureControl", DbType.Int16, RHSteamTemperatureControl);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                if (mDSet.Tables[0].Rows[0]["Value"].ToString() == "" || mDSet.Tables[0].Rows[0]["Value"].ToString() == "-" || mDSet.Tables[0].Rows[0]["Value"].ToString() == "0")
                {
                    mCal_17A_SC.Heating_Element_outlet_temperature_in_SH_side = 0.0;
                }
                else
                {
                    mCal_17A_SC.Heating_Element_outlet_temperature_in_SH_side = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Value"].ToString());
                }

                if (mDSet.Tables[1].Rows[0]["Value"].ToString() == "" || mDSet.Tables[1].Rows[0]["Value"].ToString() == "-" || mDSet.Tables[0].Rows[0]["Value"].ToString() == "0")
                {
                    mCal_17A_SC.Heating_Element_outlet_temperature_in_RH_side = 0.0;
                }
                else
                {
                    mCal_17A_SC.Heating_Element_outlet_temperature_in_RH_side = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Value"].ToString());
                }

                if (mDSet.Tables[2].Rows[0]["Value"].ToString() == "" || mDSet.Tables[2].Rows[0]["Value"].ToString() == "-" || mDSet.Tables[0].Rows[0]["Value"].ToString() == "0")
                {
                    mCal_17A_SC.APH_inlet_temperature = 0.0;
                }
                else
                {
                    mCal_17A_SC.APH_inlet_temperature = Convert.ToDouble(mDSet.Tables[2].Rows[0]["Value"].ToString());
                }        
                
            }
            catch(Exception e)
            {

            }

            return mCal_17A_SC;

        }
    
        public Cal_17A_SC Get_FlowFraction_From_HC(string BoilerLoad)
        {
            DataSet mDSet = null;
            Cal_17A_SC mCal_17A_SC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mCal_17A_SC = new Cal_17A_SC();

                mStoredProcName = StoredProcedure.spr_GetInput_For_17A_CalculationHC;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                mCal_17A_SC.Assumed_flow_fraction_through_LTSH_side = Convert.ToDouble(mDSet.Tables[0].Rows[0]["ValueFlue Gas flow fraction through LTSH side"].ToString());
                
            }
            catch(Exception e)
            {

            }
            return mCal_17A_SC;
        }

        public Cal_17A_SC Calculate_Enthalpy(string ProjectID,string BoilerID,Double a,Double b,Double c,string BoilerLoad,int ObjectiveID)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand1 = null, mDBCommand2 = null, mDBCommand3=null;
            DataSet mDSet1 = null, mDSet2 = null, mDSet3=null;
            Cal_17A_SC mCal_17A_SC = null;
            Double Temp_Min1, Enthalpy_Min1, Temp_Max1, Enthalpy_Max1, Input1;
            Double Temp_Min2, Enthalpy_Min2, Temp_Max2, Enthalpy_Max2, Input2;
            Double Temp_Min3, Enthalpy_Min3, Temp_Max3, Enthalpy_Max3, Input3;

            try
            {
                mCal_17A_SC = new Cal_17A_SC();

                mStoredProcName = StoredProcedure.spr_2A_TemperingAir_ReqEnthalpy;
                mDBCommand1 = currentDatabase.GetStoredProcCommand(mStoredProcName);
                mDBCommand2 = currentDatabase.GetStoredProcCommand(mStoredProcName);
                mDBCommand3 = currentDatabase.GetStoredProcCommand(mStoredProcName);


                //Enthalpy of SH side
                currentDatabase.AddInParameter(mDBCommand1, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand1, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand1, "@vInputValue", DbType.String, a.ToString());
                currentDatabase.AddInParameter(mDBCommand1, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand1, "@vObjectiveID", DbType.Int16, ObjectiveID);


                mDSet1 = currentDatabase.ExecuteDataSet(mDBCommand1);

                Temp_Min1 = Convert.ToDouble(mDSet1.Tables[0].Rows[0]["Temp(Deg C)"].ToString());
                Enthalpy_Min1 = Convert.ToDouble(mDSet1.Tables[0].Rows[0]["LowerFurnaceAlpha"].ToString());
                Temp_Max1 = Convert.ToDouble(mDSet1.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
                Enthalpy_Max1 = Convert.ToDouble(mDSet1.Tables[1].Rows[0]["LowerFurnaceAlpha"].ToString());

                Input1 = a;

                mCal_17A_SC.Enthalpy_of_flue_gas_out_of_Heating_Element_in_SH_side = (Enthalpy_Max1 - Enthalpy_Min1) / (Temp_Max1 - Temp_Min1) * (Input1 - Temp_Min1) + Enthalpy_Min1;


                //Enthalpy of RH side
                currentDatabase.AddInParameter(mDBCommand2, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand2, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand2, "@vInputValue", DbType.String, b.ToString());
                currentDatabase.AddInParameter(mDBCommand1, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand1, "@vObjectiveID", DbType.Int16, ObjectiveID);

                mDSet2 = currentDatabase.ExecuteDataSet(mDBCommand2);

                Temp_Min2 = Convert.ToDouble(mDSet2.Tables[0].Rows[0]["Temp(Deg C)"].ToString());
                Enthalpy_Min2 = Convert.ToDouble(mDSet2.Tables[0].Rows[0]["LowerFurnaceAlpha"].ToString());
                Temp_Max2 = Convert.ToDouble(mDSet2.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
                Enthalpy_Max2 = Convert.ToDouble(mDSet2.Tables[1].Rows[0]["LowerFurnaceAlpha"].ToString());

                Input2 = b;

                mCal_17A_SC.Enthalpy_of_flue_gas_out_of_Heating_Element_in_RH_side = (Enthalpy_Max2 - Enthalpy_Min2) / (Temp_Max2 - Temp_Min2) * (Input2 - Temp_Min2) + Enthalpy_Min2;


                //Enthalpy of APH side

                currentDatabase.AddInParameter(mDBCommand3, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand3, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand3, "@vInputValue", DbType.String, c.ToString());
                currentDatabase.AddInParameter(mDBCommand1, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand1, "@vObjectiveID", DbType.Int16, ObjectiveID);

                mDSet3 = currentDatabase.ExecuteDataSet(mDBCommand3);

                Temp_Min3 = Convert.ToDouble(mDSet3.Tables[0].Rows[0]["Temp(Deg C)"].ToString());
                Enthalpy_Min3 = Convert.ToDouble(mDSet3.Tables[0].Rows[0]["LowerFurnaceAlpha"].ToString());
                Temp_Max3 = Convert.ToDouble(mDSet3.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
                Enthalpy_Max3 = Convert.ToDouble(mDSet3.Tables[1].Rows[0]["LowerFurnaceAlpha"].ToString());

                Input3 = c;

                mCal_17A_SC.Enthalpy_of_flue_gas_inlet_of_APH = (Enthalpy_Max3 - Enthalpy_Min3) / (Temp_Max3 - Temp_Min3) * (Input3 - Temp_Min3) + Enthalpy_Min3;

            }

            catch(Exception e)
            {

            }

            return mCal_17A_SC;
        }

        public string[,] CalculateNumberOfHeatingSection(string ProjectID,string BoilerID,int RHSteamTempControl)
        {
            String mStoredProcName = String.Empty;
            String mStoredProcName1 = String.Empty;
            DbCommand mDBCommand = null, mDBCommand1=null;
            DataSet mDSet = null,mDSet1=null;
            int a=0, b=0, c=0, d=0;
            string[,] SectionInfo={};
            try
            {
                mStoredProcName = StoredProcedure.spr_17A_NumberOfHeatingElementAfterCrossDuct;
                mStoredProcName1=StoredProcedure.spr_17A_HeatingElementAfterCrossDuct;

                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                mDBCommand1 = currentDatabase.GetStoredProcCommand(mStoredProcName1);

                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vRHSteamTempControl", DbType.Int16, RHSteamTempControl);

                currentDatabase.AddInParameter(mDBCommand1, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand1, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand1, "@vRHSteamTempControl", DbType.Int16, RHSteamTempControl);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                mDSet1 = currentDatabase.ExecuteDataSet(mDBCommand1);

                if(RHSteamTempControl==2)
                {
                    int i=0;
                    a = Convert.ToInt16(mDSet.Tables[0].Rows[0]["Column1"].ToString());
                    b = Convert.ToInt16(mDSet.Tables[1].Rows[0]["Column1"].ToString());
                    c = Convert.ToInt16(mDSet.Tables[2].Rows[0]["Column1"].ToString());
                    d = a + b + c;
                    SectionInfo = new string[d, 2];

                    if (a != 0)
                    {
                        //Reverse Chamber
                        SectionInfo[0, 0] = mDSet1.Tables[0].Rows[0]["SectionType"].ToString();
                        SectionInfo[0, 1] = mDSet1.Tables[0].Rows[0]["PendantSHName"].ToString();

                        //SH Section Backpass
                        for (i = 1; i <= b; i++)
                        {
                            SectionInfo[i, 0] = mDSet1.Tables[1].Rows[i - 1]["SectionType"].ToString();
                            SectionInfo[i, 1] = mDSet1.Tables[1].Rows[i - 1]["SectionID"].ToString();
                        }

                        //RH Section Backpass
                        int l = 0;
                        for (int j = i; j < d; j++)
                        {
                            SectionInfo[j, 0] = mDSet1.Tables[2].Rows[l]["SectionType"].ToString();
                            SectionInfo[j, 1] = mDSet1.Tables[2].Rows[l]["SectionID"].ToString();
                            l++;
                        }
                    }
                    else
                    {
                        //SH Section Backpass
                        for (i = 0; i < b; i++)
                        {
                            SectionInfo[i, 0] = mDSet1.Tables[1].Rows[i - 1]["SectionType"].ToString();
                            SectionInfo[i, 1] = mDSet1.Tables[1].Rows[i - 1]["SectionID"].ToString();
                        }

                        //RH Section Backpass
                        int l = 0;
                        for (int j = i; j < d; j++)
                        {
                            SectionInfo[j, 0] = mDSet1.Tables[2].Rows[l]["SectionType"].ToString();
                            SectionInfo[j, 1] = mDSet1.Tables[2].Rows[l]["SectionID"].ToString();
                            l++;
                        }
                    }


                }
                else
                {
                    int i;
                    a = Convert.ToInt16(mDSet.Tables[0].Rows[0]["Column1"].ToString());
                    b = Convert.ToInt16(mDSet.Tables[1].Rows[0]["Column1"].ToString());
                    d = a + b;
                    SectionInfo = new string[d, 2];

                    if (a != 0)
                    {
                        //Reverse Chamber
                        SectionInfo[0, 0] = mDSet1.Tables[0].Rows[0]["SectionType"].ToString();
                        SectionInfo[0, 1] = mDSet1.Tables[0].Rows[0]["PendantSHName"].ToString();

                        //Bakpass Section
                        for (i = 1; i <= b; i++)
                        {
                            SectionInfo[i, 0] = mDSet1.Tables[1].Rows[i - 1]["SectionType"].ToString();
                            SectionInfo[i, 1] = mDSet1.Tables[1].Rows[i - 1]["SectionID"].ToString();
                        }
                    }
                    else
                    {
                        //Bakpass Section
                        for (i = 0; i < b; i++)
                        {
                            SectionInfo[i, 0] = mDSet1.Tables[1].Rows[i - 1]["SectionType"].ToString();
                            SectionInfo[i, 1] = mDSet1.Tables[1].Rows[i - 1]["SectionID"].ToString();
                        }
                    }

                }

            }
            catch(Exception e)
            {

            }

            return SectionInfo;
        }

        public int HeatingElementAfterCross(string ProjectID,string BoilerID,int RHSteamTempControl)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            int a=0, b=0, c=0, d = 0;

            try
            {
                mStoredProcName = StoredProcedure.spr_17A_NumberOfHeatingElementAfterCrossDuct;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
              
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vRHSteamTempControl", DbType.Int16, RHSteamTempControl);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

                if (RHSteamTempControl == 2)
                {
                    a = Convert.ToInt32(mDSet.Tables[0].Rows[0]["Column1"].ToString());
                    b = Convert.ToInt32(mDSet.Tables[1].Rows[0]["Column1"].ToString());
                    c = Convert.ToInt32(mDSet.Tables[2].Rows[0]["Column1"].ToString());
                    d = a + b + c;
                }
                else
                {
                    a = Convert.ToInt16(mDSet.Tables[0].Rows[0]["Column1"].ToString());
                    b = Convert.ToInt16(mDSet.Tables[1].Rows[0]["Column1"].ToString());
                    d = a + b;
                }

            }
            catch(Exception e)
            {

            }

            return d;
        }

        public void Insert_17A_Calculation(string ProjectID,string BoilerID,string SectionType,string SectionID,Double Value,int ObjectiveID,string BoilerLoad)
        {
            //DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_17A_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionType", DbType.String, SectionType);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Convert.ToString(Value));
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
   
                currentDatabase.ExecuteNonQuery(mDbCommand);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
