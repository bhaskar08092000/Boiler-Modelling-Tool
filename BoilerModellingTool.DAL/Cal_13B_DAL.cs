using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.SC;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace BoilerModellingTool.DAL
{
    public class Cal_13B_DAL
    {
        #region "Variables"

        private Database currentDatabase;

        #endregion

        #region "Constructor"

        public Cal_13B_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }

        #endregion


        public Cal_13B_SC Get_13B_Calculation_Values(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID_F_Paramter, string SectionID_S_Parameter, int FurnaceCoolingMedium, int SteamCooledWallPresent, string SectionID_SteamCooledID, string SectionID_SteamScreenID, int Iteration)
        {
            DataSet mDSet = null, mDSet1 = null, mDSet2 = null, mDSet3 = null, mDSet4 = null;
            Cal_13B_SC mCal_13B_SC = null;
            String mStoredProcName = String.Empty;
            String mStoredProcName1 = String.Empty;
            String mStoredProcName2 = String.Empty;
            String mStoredProcName3 = String.Empty;
            String mStoredProcName4 = String.Empty;
            DbCommand mDbCommand = null, mDbCommand1 = null, mDbCommand2 = null, mDbCommand3 = null, mDbCommand4 = null;
            mDSet = new DataSet();
            mDSet1 = new DataSet();
            mDSet2 = new DataSet();
            mDSet3 = new DataSet();
            mDSet4 = new DataSet();

            try
            {

                mCal_13B_SC = new Cal_13B_SC();

                //--------------------------------------1st Input-------------------------------------------------------
                mStoredProcName = StoredProcedure.spr_GetInput_For_13B_Calculation_ReverseChamber;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vIteration", DbType.Int16, Iteration);
                
                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);   

                //GI
                mCal_13B_SC.Tube_diameter = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Value"].ToString());
                mCal_13B_SC.Tube_thickness= Convert.ToDouble(mDSet.Tables[0].Rows[1]["Value"].ToString());

                //3A
                mCal_13B_SC.Side_wall_area= Convert.ToDouble(mDSet.Tables[1].Rows[0]["Value"].ToString());
                mCal_13B_SC.Roof_area= Convert.ToDouble(mDSet.Tables[1].Rows[2]["Value"].ToString());
                mCal_13B_SC.Rear_wall_area= Convert.ToDouble(mDSet.Tables[1].Rows[1]["Value"].ToString());
                mCal_13B_SC.Eco_hanger_heating_area_in_zone = Convert.ToDouble(mDSet.Tables[1].Rows[4]["Value"].ToString());
                mCal_13B_SC.Eco_hanger_circumf_area = Convert.ToDouble(mDSet.Tables[1].Rows[5]["Value"].ToString());
                mCal_13B_SC.Effective_radiation_layer_thickness = Convert.ToDouble(mDSet.Tables[1].Rows[3]["Value"].ToString());

                //12B
                //mCal_13B_SC.Temperature_of_flue_gas_into_panel = Convert.ToDouble(mDSet.Tables[2].Rows[1]["Value"].ToString());
                //mCal_13B_SC.Enthalpy_of_flue_gas_into_panel = Convert.ToDouble(mDSet.Tables[2].Rows[0]["Value"].ToString());

                //3A
                mCal_13B_SC.Heat_preservation_coefficient = Convert.ToDouble(mDSet.Tables[2].Rows[0]["Value"].ToString());

                //4B
                mCal_13B_SC.Partial_pressure_of_triatomic_gases = Convert.ToDouble(mDSet.Tables[3].Rows[0]["Value"].ToString());

                //1A
                mCal_13B_SC.Volume_fraction_of_water_vapor = Convert.ToDouble(mDSet.Tables[4].Rows[0]["Lower furnace"].ToString());
                mCal_13B_SC.Gas_density = Convert.ToDouble(mDSet.Tables[4].Rows[1]["Lower furnace"].ToString());

                //1A
                double a = Convert.ToDouble(mDSet.Tables[5].Rows[0]["Value"].ToString());
                double b = Convert.ToDouble(mDSet.Tables[6].Rows[0]["Lower furnace"].ToString());
                mCal_13B_SC.Flue_gas_total_volume=b*(1+a/100);

                //PI
                mCal_13B_SC.Mean_diameter_of_ash_particle = Convert.ToDouble(mDSet.Tables[7].Rows[0]["Value"].ToString()); 

                //1A
                mCal_13B_SC.Volume_fraction_of_triatomic_gases = Convert.ToDouble(mDSet.Tables[8].Rows[0]["Lower furnace"].ToString());
                mCal_13B_SC.Dimensionless_concentration_of_fly_ash = Convert.ToDouble(mDSet.Tables[8].Rows[1]["Lower furnace"].ToString());
                
                //HC
                mCal_13B_SC.Furnace_pressure = Convert.ToDouble(mDSet.Tables[9].Rows[0]["Value"].ToString());

                mCal_13B_SC.Radiation_heat_from_flue_gas_in_upstream_zone = 0.0;
                
                //1A
                double c = Convert.ToDouble(mDSet.Tables[10].Rows[0]["Value"].ToString());
                mCal_13B_SC.Design_fuel_consumption=c/3.6;

                //17A
                
                
                //10B
                //mCal_13B_SC.Inlet_temperature_side_wall = Convert.ToDouble(mDSet.Tables[12].Rows[1]["Value"].ToString());
                //mCal_13B_SC.Inlet_steam_enthalpy = Convert.ToDouble(mDSet.Tables[12].Rows[0]["Value"].ToString());

                //
                //mCal_13B_SC.Reverse_chamber_outlet_pressure = 166.3;

                //PI
                //mCal_13B_SC.Main_steam_flow_rate = Convert.ToDouble(mDSet.Tables[13].Rows[0]["Value"].ToString())*1000;
                //mCal_13B_SC.Desuperheating_spray_stage_1 = Convert.ToDouble(mDSet.Tables[13].Rows[1]["Value"].ToString())*1000;
                //mCal_13B_SC.Desuperheating_spray_stage_2_if_any = Convert.ToDouble(mDSet.Tables[13].Rows[2]["Value"].ToString())*1000;

                //15B
                mCal_13B_SC.Economiser_hanger_inlet_temp = 276;
                //mCal_13B_SC.Economiser_hanger_inlet_enthalpy = 1213; 

                //
                //mCal_13B_SC.Economiser_hanger_outlet_pressure = 167;

                //HC
                mCal_13B_SC.Ash_deposit_coefficient = Convert.ToDouble(mDSet.Tables[11].Rows[0]["Value"].ToString());
                mCal_13B_SC.Tube_wall_fouling_emmisivity = Convert.ToDouble(mDSet.Tables[12].Rows[0]["Value"].ToString());

                //Pressure Module
                mCal_13B_SC.Economiser_hanger_inlet_pressure = Convert.ToDouble(mDSet.Tables[13].Rows[0]["Value"].ToString());
                mCal_13B_SC.Economiser_hanger_outlet_pressure = Convert.ToDouble(mDSet.Tables[13].Rows[1]["Value"].ToString());
                mCal_13B_SC.Inlet_steam_pressure = Convert.ToDouble(mDSet.Tables[13].Rows[2]["Value"].ToString());
                mCal_13B_SC.Reverse_chamber_outlet_pressure = Convert.ToDouble(mDSet.Tables[13].Rows[3]["Value"].ToString());

                //Flow Module
                mCal_13B_SC.Reverse_chamber_flow_rate = Convert.ToDouble(mDSet.Tables[14].Rows[0]["Value"].ToString())*1000;
                mCal_13B_SC.Economiser_hanger_flow_rate = Convert.ToDouble(mDSet.Tables[14].Rows[1]["Value"].ToString())*1000;

                //15B
                mCal_13B_SC.Economiser_hanger_inlet_temp = Convert.ToDouble(mDSet.Tables[15].Rows[0]["Value"].ToString());

                //1A Flue gas density
                mCal_13B_SC.Flue_gas_density = Convert.ToDouble(mDSet.Tables[16].Rows[0]["Lower furnace"].ToString());

                //17A Calculation
                mCal_13B_SC.Flue_gas_flow_fraction_through_the_element = Convert.ToDouble(mDSet.Tables[17].Rows[0]["Value"].ToString()); ;

                //--------------------------------------------------2nd Input F parameter--------------------------------------

                mStoredProcName1 = StoredProcedure.spr_GetInput_For_13B_Calculation_F_Parameter;
                mDbCommand1 = currentDatabase.GetStoredProcCommand(mStoredProcName1);
                currentDatabase.AddInParameter(mDbCommand1, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand1, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand1, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand1, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand1, "@vSectionID", DbType.String, SectionID_F_Paramter);
                currentDatabase.AddInParameter(mDbCommand1, "@vFurnaceRoofCoolingMedium", DbType.Int16, FurnaceCoolingMedium);

                mDSet1 = currentDatabase.ExecuteDataSet(mDbCommand1);
                //12B

                mCal_13B_SC. Enthalpy_of_flue_gas_into_panel = Convert.ToDouble(mDSet1.Tables[0].Rows[0]["Value"].ToString());
                mCal_13B_SC.Temperature_of_flue_gas_into_panel = Convert.ToDouble(mDSet1.Tables[0].Rows[1]["Value"].ToString());

                //-------------------------------------------------3rd Input S parameter----------------------------------------

                mStoredProcName2 = StoredProcedure.spr_GetInput_For_13B_Calculation_S_Parameter;
                mDbCommand2 = currentDatabase.GetStoredProcCommand(mStoredProcName2);
                currentDatabase.AddInParameter(mDbCommand2, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand2, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand2, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand2, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand2, "@vSectionID", DbType.String, SectionID_S_Parameter);
                currentDatabase.AddInParameter(mDbCommand2, "@vIteration", DbType.Int16, Iteration);

                mDSet2 = currentDatabase.ExecuteDataSet(mDbCommand2);

                mCal_13B_SC.Inlet_temperature_side_wall = Convert.ToDouble(mDSet2.Tables[0].Rows[0]["Value"].ToString());

                //------------------------------------------------4Th Flow Fraction Parameters------------------------------------

                mStoredProcName3 = StoredProcedure.spr_GetInput_For_Flow_BackpassRatioCalculation;
                mDbCommand3 = currentDatabase.GetStoredProcCommand(mStoredProcName3);

                currentDatabase.AddInParameter(mDbCommand3, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand3, "@vBoilerID", DbType.String, BoilerID);

                mDSet3 = currentDatabase.ExecuteDataSet(mDbCommand3);

                mCal_13B_SC.Sidewall_front_portion_tube_diameter = Convert.ToDouble(mDSet3.Tables[0].Rows[0]["Value"].ToString());
                mCal_13B_SC.Sidewall_front_portion_tube_numbers = Convert.ToDouble(mDSet3.Tables[0].Rows[1]["Value"].ToString());
                mCal_13B_SC.Sidewall_rear_portion_tube_diameter = Convert.ToDouble(mDSet3.Tables[0].Rows[2]["Value"].ToString());
                mCal_13B_SC.Sidewall_rear_portion_tube_numbers = Convert.ToDouble(mDSet3.Tables[0].Rows[3]["Value"].ToString());
                mCal_13B_SC.Front_wall_tube_diameter = Convert.ToDouble(mDSet3.Tables[0].Rows[4]["Value"].ToString());
                mCal_13B_SC.Front_wall_tube_numbers = Convert.ToDouble(mDSet3.Tables[0].Rows[5]["Value"].ToString());
                mCal_13B_SC.Extended_steam_wall_header_tube_diameter = Convert.ToDouble(mDSet3.Tables[0].Rows[6]["Value"].ToString());
                mCal_13B_SC.Extended_steam_wall_header_tube_numbers = Convert.ToDouble(mDSet3.Tables[0].Rows[7]["Value"].ToString());

                //-----------------------------------------------5th First Superheater S Parameter--------------------------------

                mStoredProcName4 = StoredProcedure.spr_GetInput_For_13B_Calculation_InletSteam;
                mDbCommand4 = currentDatabase.GetStoredProcCommand(mStoredProcName4);

                currentDatabase.AddInParameter(mDbCommand4, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand4, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand4, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand4, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand4, "@vSteamCooledWall", DbType.Int16, SteamCooledWallPresent);
                currentDatabase.AddInParameter(mDbCommand4, "@vSteamScreenPresent", DbType.Int16, 1);
                currentDatabase.AddInParameter(mDbCommand4, "@vSectionID_SteamCooledID", DbType.String, SectionID_SteamCooledID);
                currentDatabase.AddInParameter(mDbCommand4, "@vSectionID_SteamScreenID", DbType.String, SectionID_SteamScreenID);

                mDSet4 = currentDatabase.ExecuteDataSet(mDbCommand4);

                mCal_13B_SC.Steam_temp_at_extended_side_wall = Convert.ToDouble(mDSet4.Tables[0].Rows[0]["Value"].ToString());
                mCal_13B_SC.Steam_screen_exit_steam_temp = Convert.ToDouble(mDSet4.Tables[1].Rows[0]["Value"].ToString());
               
            }

            catch
            {

            }

            return mCal_13B_SC;

        }

        public DataTable Get_PID_For_13B_Calculation()
        {
            String mStoredProcName = String.Empty;

            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mStoredProcName = StoredProcedure.spr_Get_PID_For_13B_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                mDTable = mDSet.Tables[0];

            }
            catch
            {

            }

            return mDTable;
        }

        public void Insert_13B_Calculation(int PID,string ProjectID,string BoilerID,double Value,string BoilerLoad,int ObjectiveID)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_13B_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.Int16, PID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Convert.ToString(Value));
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                //currentDatabase.AddInParameter(mDbCommand, "@vHeatingElement", DbType.String, HeatingElement);

                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public double Get_Temperature_From_Ethalpy(string ProjectID, string BoilerID, double value, string BoilerLoad, int ObjectiveID)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand1 = null;
            DataSet mDSet1 = null;
            double Temp_Min1, Enthalpy_Min1, Temp_Max1, Enthalpy_Max1, Input1;
            double Result = 0.0;

            try
            {
                mStoredProcName = StoredProcedure.spr_GenericB_Calculate_Temperature;
                mDBCommand1 = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand1, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand1, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand1, "@vInputValue", DbType.String, value);
                currentDatabase.AddInParameter(mDBCommand1, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand1, "@vObjectiveID", DbType.Int16, ObjectiveID);

                mDSet1 = currentDatabase.ExecuteDataSet(mDBCommand1);

                Temp_Min1 = Convert.ToDouble(mDSet1.Tables[0].Rows[0]["Temp(Deg C)"].ToString());
                Enthalpy_Min1 = Convert.ToDouble(mDSet1.Tables[0].Rows[0]["LowerFurnaceAlpha"].ToString());
                Temp_Max1 = Convert.ToDouble(mDSet1.Tables[1].Rows[0]["Temp(Deg C)"].ToString());
                Enthalpy_Max1 = Convert.ToDouble(mDSet1.Tables[1].Rows[0]["LowerFurnaceAlpha"].ToString());

                Result = (Temp_Max1 - Temp_Min1) / (Enthalpy_Max1 - Enthalpy_Min1) * (value - Enthalpy_Min1) + Temp_Min1;

            }
            catch
            {

            }

            return Result;
        }

        public double SteamTempFirstSuperheater(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            double p1 = 0;

            try
            {
                mStoredProcName = StoredProcedure.spr_GetInput_For_First_Superheater;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                p1 = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Value"].ToString());

            }
            catch
            {

            }
            return p1;
        }

        public void Update_First_Superheater(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID,string SectionID,double Value1,double Value2,double Value3,int Iteration,string SteamScreenID)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Update_S_Paramter_First_Superheater;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID_S_ParameterValue", DbType.String, Convert.ToString(Value1));
                currentDatabase.AddInParameter(mDbCommand, "@vInput", DbType.String, Convert.ToString(Value2));
                currentDatabase.AddInParameter(mDbCommand, "@vOutput", DbType.String, Convert.ToString(Value3));
                currentDatabase.AddInParameter(mDbCommand, "@vIteration", DbType.Int16,Iteration);
                currentDatabase.AddInParameter(mDbCommand, "@vSteamScreenID", DbType.String, SteamScreenID);
                //currentDatabase.AddInParameter(mDbCommand, "@vHeatingElement", DbType.String, HeatingElement);

                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
