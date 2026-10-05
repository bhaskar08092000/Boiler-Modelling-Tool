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
    public class Cal_GenericB_DAL
    {
        #region " Variables "

        private Database currentDatabase;

        #endregion

        #region "Constuctor"

        public Cal_GenericB_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }

        #endregion

        public Cal_GenericB_SC Get_InputFor_GenericB(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID, string SectionType, string PreviousSectionID, string Location, int CurrentLocationID, int PreviousLocationID, string GroupID1, string GroupID2, int CoolingMedium, int Iteration, string PreviousHeatingElementID, string PreviousSectionType)
        {
            String mStoredProcName = String.Empty;
            String mStoredProcName1 = String.Empty;
            String mStoredProcName2 = String.Empty;
            String mStoredProcName3 = String.Empty;
            String mStoredProcName4 = String.Empty;
            Cal_GenericB_SC mCal_GenericB_SC = null;
            DbCommand mDbCommand = null, mDbCommand1 = null, mDbCommand2 = null, mDbCommand3 = null, mDbCommand4 = null;
            DataSet mDSet = null, mDSet1 = null, mDSet2 = null, mDSet3 = null, mDSet4 = null;
            //DataTable mDTable = null;
            //try
            {
                //------------------------------------------------------------------------------------------------------------------
                mDSet = new DataSet();
                mCal_GenericB_SC = new Cal_GenericB_SC();
                mStoredProcName = StoredProcedure.spr_GetInput_For_GenericB;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vPreviousSectionID", DbType.String, PreviousSectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionType", DbType.String, SectionType);
                currentDatabase.AddInParameter(mDbCommand, "@vLocation", DbType.String, Location);
                currentDatabase.AddInParameter(mDbCommand, "@vCurrentLocationID", DbType.Int16, CurrentLocationID);
                currentDatabase.AddInParameter(mDbCommand, "@vPreviousLocationID", DbType.Int16, PreviousLocationID);
                //currentDatabase.AddInParameter(mDbCommand, "@vPreviousSectionID", DbType.String, PreviousSectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vGroupID1", DbType.String, GroupID1);
                currentDatabase.AddInParameter(mDbCommand, "@vGroupID2", DbType.String, GroupID2);
                currentDatabase.AddInParameter(mDbCommand, "@vCoolingMedium", DbType.Int16, CoolingMedium);
                currentDatabase.AddInParameter(mDbCommand, "@vPreviousSectionType", DbType.String, PreviousSectionType);


                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                //GI
                mCal_GenericB_SC.Tube_diameter = Convert.ToDouble(mDSet.Tables[0].Rows[0]["Value"].ToString());
                mCal_GenericB_SC.Tube_thickness = Convert.ToDouble(mDSet.Tables[0].Rows[1]["Value"].ToString());
                mCal_GenericB_SC.Heataing_element_depth = Convert.ToDouble(mDSet.Tables[0].Rows[2]["Value"].ToString());
                mCal_GenericB_SC.Relative_space_depth_prior_to_Heating_element = Convert.ToDouble(mDSet.Tables[0].Rows[3]["Value"].ToString());

                //Geometric Input
                mCal_GenericB_SC.Relative_transverse_pitch = Convert.ToDouble(mDSet.Tables[1].Rows[0]["Value"].ToString());
                mCal_GenericB_SC.Relative_vertical_pitch = Convert.ToDouble(mDSet.Tables[1].Rows[1]["Value"].ToString());
                mCal_GenericB_SC.Total_heating_area = Convert.ToDouble(mDSet.Tables[1].Rows[2]["Value"].ToString());
                mCal_GenericB_SC.Heating_area_of_roof_tubes_in_the_zone = Convert.ToDouble(mDSet.Tables[1].Rows[3]["Value"].ToString());
                mCal_GenericB_SC.Side_Water_wall_heating_area__within_panel = Convert.ToDouble(mDSet.Tables[1].Rows[4]["Value"].ToString());
                mCal_GenericB_SC.Gas_average_flow_area = Convert.ToDouble(mDSet.Tables[1].Rows[5]["Value"].ToString());
                mCal_GenericB_SC.Steam_flow_area = Convert.ToDouble(mDSet.Tables[1].Rows[6]["Value"].ToString());
                mCal_GenericB_SC.Effective_radiation_layer_thickness = Convert.ToDouble(mDSet.Tables[1].Rows[7]["Value"].ToString());
                mCal_GenericB_SC.Correction_factor_for_tube_rows = Convert.ToDouble(mDSet.Tables[1].Rows[8]["Value"].ToString());

                //Flue Gas Properties
                mCal_GenericB_SC.Enthalpy_of_flue_gas_into_panel = Convert.ToDouble(mDSet.Tables[2].Rows[1]["Value"].ToString());
                mCal_GenericB_SC.Temperature_of_flue_gas_into_panel = Convert.ToDouble(mDSet.Tables[2].Rows[0]["Value"].ToString());
                
                //3A
                mCal_GenericB_SC.Heat_preservation_coefficient = Convert.ToDouble(mDSet.Tables[3].Rows[0]["Value"].ToString());

                //4B
                mCal_GenericB_SC.Partial_pressure_of_triatomic_gases = Convert.ToDouble(mDSet.Tables[4].Rows[0]["Value"].ToString());

                //1A
                mCal_GenericB_SC.Flue_gas_total_volume = Convert.ToDouble(mDSet.Tables[5].Rows[0]["Upper furnace"].ToString());
                mCal_GenericB_SC.Volume_fraction_of_water_vapor = Convert.ToDouble(mDSet.Tables[5].Rows[1]["Upper furnace"].ToString());
                mCal_GenericB_SC.Volume_fraction_of_triatomic_gases = Convert.ToDouble(mDSet.Tables[5].Rows[2]["Upper furnace"].ToString());
                mCal_GenericB_SC.Gas_density = Convert.ToDouble(mDSet.Tables[5].Rows[3]["Upper furnace"].ToString());
                mCal_GenericB_SC.Dimensionless_concentration_of_fly_ash = Convert.ToDouble(mDSet.Tables[5].Rows[4]["Upper furnace"].ToString());

                //1A and PI
                mCal_GenericB_SC.Design_fuel_consumption = (Convert.ToDouble(mDSet.Tables[6].Rows[0]["Value"].ToString())) / 3.6;

                //PI
                mCal_GenericB_SC.Mean_diameter_of_ash_particle = Convert.ToDouble(mDSet.Tables[7].Rows[0]["Value"].ToString());

                //HC
                mCal_GenericB_SC.Furnace_pressure = Convert.ToDouble(mDSet.Tables[8].Rows[0]["Value"].ToString());

                //5B or 13B
                //mCal_GenericB_SC.Inlet_steam_water_temperature_of_side_wall = Convert.ToDouble(mDSet.Tables[9].Rows[0]["Value"].ToString());

                //Flow
                mCal_GenericB_SC.Heating_section_flow_rate = (Convert.ToDouble(mDSet.Tables[9].Rows[0]["Value"].ToString()))*1000;

                mCal_GenericB_SC.Steam_flow_in_roof = (Convert.ToDouble(mDSet.Tables[10].Rows[0]["Value"].ToString()))*1000;

                mCal_GenericB_SC.Steam_water_flow_in_side_wall = (Convert.ToDouble(mDSet.Tables[11].Rows[0]["Value"].ToString()))*1000;

                mCal_GenericB_SC.De_superheating_spray_enthalpy = Convert.ToDouble(mDSet.Tables[12].Rows[0]["Value"].ToString());

                //Previous Module
                //mCal_GenericB_SC.Inlet_steam_enthalpy_of_superheater_at_furnace_roof = Convert.ToDouble(mDSet.Tables[14].Rows[0]["Value"].ToString());
                mCal_GenericB_SC.Inlet_steam_temperature_of_superheater_at_furnace_roof = Convert.ToDouble(mDSet.Tables[13].Rows[0]["Value"].ToString());

                //PI
                if (SectionType != "Steam Screen Sections")
                {               
                    mCal_GenericB_SC.Heating_section_inlet_steam_temperature_design = Convert.ToDouble(mDSet.Tables[14].Rows[0]["Value"].ToString());
 
                    mCal_GenericB_SC.Heating_section_inlet_steam_pressure_design = Convert.ToDouble(mDSet.Tables[15].Rows[0]["Value"].ToString());
                    
                    mCal_GenericB_SC.Heating_section_outlet_steam_temperature_design = Convert.ToDouble(mDSet.Tables[16].Rows[0]["Value"].ToString());
                                          
                }

                if(Location=="Backpass")
                {
                    if (SectionType == "Steam Screen Sections")
                    {
                        mCal_GenericB_SC.Flue_gas_flow_fraction_through_the_element = Convert.ToDouble(mDSet.Tables[14].Rows[0]["Value"].ToString());
                    }
                    else
                    {
                        mCal_GenericB_SC.Flue_gas_flow_fraction_through_the_element = Convert.ToDouble(mDSet.Tables[17].Rows[0]["Value"].ToString());
                    }

                    if(SectionType=="Superheater Elements")
                    {
                        mCal_GenericB_SC.De_superheating_spray = Convert.ToDouble(mDSet.Tables[18].Rows[0]["Value"].ToString());
                    }
                }
                else if (Location == "CrossDuct" || Location == "ReverseChamber")
                {
                    mCal_GenericB_SC.Flue_gas_flow_fraction_through_the_element = 1.0;

                    if (SectionType == "Superheater Elements")
                    {
                        mCal_GenericB_SC.De_superheating_spray = Convert.ToDouble(mDSet.Tables[17].Rows[0]["Value"].ToString());
                    }

                }

                //-------------------------------------------------------------------------------------------------------------------------

                mDSet1 = new DataSet();
                mStoredProcName1 = StoredProcedure.spr_GetInput_For_GenericB_PressureParameters;
                mDbCommand1 = currentDatabase.GetStoredProcCommand(mStoredProcName1);

                currentDatabase.AddInParameter(mDbCommand1, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand1, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand1, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand1, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand1, "@vSectionID", DbType.String, SectionID);            
                
                currentDatabase.AddInParameter(mDbCommand1, "@vSectionType", DbType.String, SectionType);
                               
                currentDatabase.AddInParameter(mDbCommand1, "@vLocation", DbType.String, Location);

                mDSet1 = currentDatabase.ExecuteDataSet(mDbCommand1);

                if (mDSet1 == null)
                {
                    System.Diagnostics.Debug.WriteLine("mDSet1 is null");
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine("mDSet1 Tables: " + mDSet1.Tables.Count);
                    //for (int i = 0; i < mDSet1.Tables.Count; i++)
                    //{
                    //    System.Diagnostics.Debug.WriteLine("Table " + i + " Rows=" + mDSet1.Tables[i].Rows.Count);
                    //    foreach (DataColumn col in mDSet1.Tables[i].Columns)
                    //    {
                    //        System.Diagnostics.Debug.WriteLine("  Col: " + col.ColumnName);
                    //    }
                    //}
                }
                if (SectionType == "Water Screen Sections")
                {
                    mCal_GenericB_SC.Inlet_water_steam_pressure = Convert.ToDouble(mDSet1.Tables[0].Rows[0]["Value"].ToString());
                    mCal_GenericB_SC.Outlet_water_steam_pressure = mCal_GenericB_SC.Inlet_water_steam_pressure + 0.1;
                }
                else
                {
                    mCal_GenericB_SC.Inlet_water_steam_pressure = Convert.ToDouble(mDSet1.Tables[0].Rows[0]["Value"].ToString());
                    mCal_GenericB_SC.Outlet_water_steam_pressure = Convert.ToDouble(mDSet1.Tables[0].Rows[1]["Value"].ToString()); 
                }

                if (Location == "CrossDuct" || Location=="ReverseChamber")
                {
                    mCal_GenericB_SC.Inlet_steam_pressure_of_HS_at_furnace_roof = Convert.ToDouble(mDSet1.Tables[1].Rows[0]["Value"].ToString());
                    mCal_GenericB_SC.Outlet_steam_pressure_of_HS_at_furnace_roof = Convert.ToDouble(mDSet1.Tables[1].Rows[1]["Value"].ToString());
                }
                else
                {
                    mCal_GenericB_SC.Inlet_steam_pressure_of_HS_at_furnace_roof = Convert.ToDouble(mDSet1.Tables[1].Rows[0]["Value"].ToString());
                    mCal_GenericB_SC.Outlet_steam_pressure_of_HS_at_furnace_roof = Convert.ToDouble(mDSet1.Tables[1].Rows[0]["Value"].ToString());
                }

                //-------------------------------------------------------------------------------------------------------------------------

                mDSet2 = new DataSet();
                mStoredProcName2 = StoredProcedure.spr_GetInput_For_GenericB_SideWallParameters;
                mDbCommand2 = currentDatabase.GetStoredProcCommand(mStoredProcName2);

                string SectionID1 = Get_SteamCooledWall_SectionID(ProjectID, BoilerID);
                int flag = 0;

                if(SectionID1==SectionID)
                {
                    flag = 1;
                }
                
                currentDatabase.AddInParameter(mDbCommand2, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand2, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand2, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand2, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand2, "@vLocation", DbType.String, Location);
                currentDatabase.AddInParameter(mDbCommand2, "@vCoolingMedium", DbType.Int16, CoolingMedium);
                currentDatabase.AddInParameter(mDbCommand2, "@vIteration", DbType.Int16,Iteration);
                currentDatabase.AddInParameter(mDbCommand2, "@vValidation", DbType.Int16, flag);

                mDSet2 = currentDatabase.ExecuteDataSet(mDbCommand2);

                mCal_GenericB_SC.Inlet_steam_water_temperature_of_side_wall = Convert.ToDouble(mDSet2.Tables[0].Rows[0]["Value"].ToString());

                mCal_GenericB_SC.Inlet_steam_water_pressure_of_side_wall = Convert.ToDouble(mDSet2.Tables[1].Rows[0]["Value"].ToString());

                //-------------------------------------------------------------------------------------------------------------------------

                mDSet3 = new DataSet();
                mStoredProcName3 = StoredProcedure.spr_GetInput_For_GenericB_S_Parameter;
                mDbCommand3 = currentDatabase.GetStoredProcCommand(mStoredProcName3);

                currentDatabase.AddInParameter(mDbCommand3, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand3, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand3, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand3, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand3, "@vIteration", DbType.Int16, Iteration);
                currentDatabase.AddInParameter(mDbCommand3, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDbCommand3, "@vSectionType", DbType.String, SectionType);

                mDSet3 = currentDatabase.ExecuteDataSet(mDbCommand3);

                if (SectionType == "Water Screen Sections")
                {
                    mCal_GenericB_SC.Inlet_water_steam_temperature = Convert.ToDouble(mDSet3.Tables[0].Rows[0]["Value"].ToString());
                }
               //else if (SectionType == "Superheater Elements")
               // {
               //     mCal_GenericB_SC.Inlet_water_steam_temperature = Convert.ToDouble(mDSet3.Tables[0].Rows[0]["Value"].ToString());
               // }
                else
                {
                    mCal_GenericB_SC.Inlet_water_steam_temperature = Convert.ToDouble(mDSet3.Tables[0].Rows[0]["Input"].ToString());
                }
                //-------------------------------------------------------------------------------------------------------------------------

                mDSet4 = new DataSet();
                mStoredProcName4 = StoredProcedure.spr_GetInput_For_GenericB_HC_Parameter;
                mDbCommand4 = currentDatabase.GetStoredProcCommand(mStoredProcName4);

                currentDatabase.AddInParameter(mDbCommand4, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand4, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand4, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand4, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand4, "@vSectionID", DbType.String, SectionID);

                mDSet4 = currentDatabase.ExecuteDataSet(mDbCommand4);

                mCal_GenericB_SC.Fouling_uniformity_coefficient = Convert.ToDouble(mDSet4.Tables[0].Rows[0]["Value"].ToString());

                mCal_GenericB_SC.Fuel_correction_coefficient = Convert.ToDouble(mDSet4.Tables[0].Rows[1]["Value"].ToString());

                mCal_GenericB_SC.Ash_deposit_coefficient = Convert.ToDouble(mDSet4.Tables[0].Rows[2]["Value"].ToString());

                mCal_GenericB_SC.Heating_section_effectiveness_coefficent = Convert.ToDouble(mDSet4.Tables[1].Rows[0]["Value"].ToString());

                if(SectionType=="Water Screen Sections")
                {
                    mCal_GenericB_SC.Tube_wall_fouling_emmisivity=0.68;
                }
                else
                {
                    mCal_GenericB_SC.Tube_wall_fouling_emmisivity=0.8;
                }

                //-------------------------------------------------------------------------------------------------------------------------
                mCal_GenericB_SC.Radiation_heat_from_flue_gas_in_upstream_zone = 0.0;

                mCal_GenericB_SC.Furnace_roof_pressure_within_panel_zone = 166.7;

            }
            //catch
            //{

            //}

            return mCal_GenericB_SC;
        }
    
        public double Get_Temperature_From_Ethalpy(string ProjectID,string BoilerID,double value,string BoilerLoad,int ObjectiveID)
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
                currentDatabase.AddInParameter(mDBCommand1, "@vBoilerLoad", DbType.String,BoilerLoad);
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
         
        public DataTable Get_PID_For_GenericB_Calculation()
        {
            String mStoredProcName = String.Empty;

            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            try
            {
                mDTable = new DataTable();
                mStoredProcName = StoredProcedure.spr_Get_PID_For_GenericB_Calculation;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                mDTable = mDSet.Tables[0];

            }
            catch
            {

            }

            return mDTable;
        }

        public void Insert_GenericB_Calculation(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, int PID, string SectionID, double Value, string Location, string SectionType)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Insert_GenericB_Calculation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vPID", DbType.Int16, PID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vValue", DbType.String, Convert.ToString(Value));
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vLocation", DbType.String, Location);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionType", DbType.String, SectionType);
                //currentDatabase.AddInParameter(mDbCommand, "@vHeatingElement", DbType.String, HeatingElement);

                currentDatabase.ExecuteNonQuery(mDbCommand);
                //mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Update_S_ParameterValue(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,double Input,double Output,string SectionID,int Iteration,string NextSectionID)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_Update_S_Parameter_Value;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vInput", DbType.String, Convert.ToString(Input));
                currentDatabase.AddInParameter(mDbCommand, "@vOutput", DbType.String, Convert.ToString(Output));
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionID);
                currentDatabase.AddInParameter(mDbCommand, "@vIteration", DbType.Int16, Iteration);
                currentDatabase.AddInParameter(mDbCommand, "@vNextSectionID", DbType.String, NextSectionID);

                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string Get_SteamCooledWall_SectionID(string ProjectID,string BoilerID)
        {
            String mStoredProcName = String.Empty;

            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;
            string SectionID = "";

            try
            {
                mDTable = new DataTable();
                mStoredProcName = StoredProcedure.spr_Get_SteamedCooledWall_SectionID;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand,"@vBoilerID",DbType.String,BoilerID);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                SectionID = mDSet.Tables[0].Rows[0]["HeatingSection"].ToString();

            }
            catch
            {

            }

            return SectionID;
        }

    }
}
