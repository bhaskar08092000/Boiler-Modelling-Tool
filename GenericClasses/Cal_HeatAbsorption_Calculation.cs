using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;
using BoilerModellingTool.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;


public class Cal_HeatAbsorption_Calculation
{
    //string str = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;


    #region "Variables"

    private Database currentDatabase;

    #endregion

    #region "Constructor"

    public Cal_HeatAbsorption_Calculation()
    {
        currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
    }

    #endregion

    String mStoredProcName = String.Empty;

    DbCommand mDbCommand = null;

    String mStoredProcName1 = String.Empty;

    DbCommand mDbCommand1 = null;

    String mStoredProcName2 = String.Empty;

    DbCommand mDbCommand2 = null;

    String mStoredProcName3 = String.Empty;

    DbCommand mDbCommand3 = null;

    String mStoredProcName5 = String.Empty;

    DbCommand mDbCommand5 = null;


    String mStoredProcName6 = String.Empty;

    DbCommand mDbCommand6 = null;


    String mStoredProcName20 = String.Empty;

    DbCommand mDbCommand20 = null;

    String mStoredProcName30 = String.Empty;

    DbCommand mDbCommand30 = null;

    String mStoredProcName40 = String.Empty;

    DbCommand mDbCommand40 = null;

    String mStoredProcName55 = String.Empty;

    DbCommand mDbCommand55 = null;




    String mStoredProcName50 = String.Empty;

    DbCommand mDbCommand50 = null;

    String mStoredProcName70 = String.Empty;

    DbCommand mDbCommand70 = null;

    String mStoredProcName65 = String.Empty;

    DbCommand mDbCommand65 = null;

    String mStoredProcName45 = String.Empty;

    DbCommand mDbCommand45 = null;

    String mStoredProcName46 = String.Empty;

    DbCommand mDbCommand46 = null;

    Cal_HeatAbsorption_BLL mCal_HeatAbsorption_BLL;

    Cal_HeatAbsorption_SC mCal_HeatAbsorption_SC;

    Stream_Macros mStream_Macros_Generic = null;



    DataSet mDSet;
    DataSet mDSet1;
    DataSet mDSet2;
    DataSet mDSet3;
    DataSet mDSet5;
    DataSet mDSet6;
    DataSet mDSet7;
    DataSet mDSet30;
    DataSet mDSet40;
    DataSet mDSet50;
    DataSet mDSet70;
    DataSet mDSet65;
    DataSet mDSet200;
    DataSet mDSet55;
    DataSet mDSet89;


    //double[] CrossDuctValue;


    public void Calculation_For_HeatAbsorptionModule(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID)
    {



        mCal_HeatAbsorption_BLL = new Cal_HeatAbsorption_BLL();

        mCal_HeatAbsorption_SC = new Cal_HeatAbsorption_SC();

        mStream_Macros_Generic = new Stream_Macros();

        List<double> CrossDuctValue = new List<double>();

        List<string> SuperheaterID = new List<string>();

        List<string> ReheaterID = new List<string>();

        List<string> WaterScreenID = new List<string>();

        List<string> SectionID = new List<string>();

        List<string> EconomizerID = new List<string>();




        //CrossDuctValue = new double[10];
        DataSet mdataset = null;
        DataTable mDataTable = null;
        mdataset = new DataSet();



        mdataset = mCal_HeatAbsorption_BLL.GetInput_For_Cal_HeatAbsorption_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID);


        mCal_HeatAbsorption_SC.Drum_separator_outlet_pressure = Convert.ToDouble(mdataset.Tables[0].Rows[0]["Value"].ToString());

        mCal_HeatAbsorption_SC.Design_fuel_consumption = Convert.ToDouble(mdataset.Tables[1].Rows[0]["Value"].ToString());

        mCal_HeatAbsorption_SC.Heat_preservation_coefficient = Convert.ToDouble(mdataset.Tables[2].Rows[0]["Value"].ToString());

        mCal_HeatAbsorption_SC.Flue_gas_fraction_in_left_side_backpass = Convert.ToDouble(mdataset.Tables[3].Rows[0]["Value"].ToString());

        mCal_HeatAbsorption_SC.Flue_gas_fraction_in_right_side_backpass = Convert.ToDouble(mdataset.Tables[4].Rows[0]["Value"].ToString());

        mCal_HeatAbsorption_SC.Economizer_bundle = Convert.ToDouble(mdataset.Tables[5].Rows[0]["Value"].ToString());//Uncomment Afterwards

        mCal_HeatAbsorption_SC.Higher_Heating_Value = Convert.ToDouble(mdataset.Tables[21].Rows[0]["Value"].ToString());

        mCal_HeatAbsorption_SC.Number_of_SH_heating_sections = Convert.ToDouble(mdataset.Tables[23].Rows[0]["Column1"].ToString());

        mCal_HeatAbsorption_SC.Number_of_RH_heating_sections = Convert.ToDouble(mdataset.Tables[24].Rows[0]["Column1"].ToString());

        mCal_HeatAbsorption_SC.Number_of_Economizer_HeatingSections = Convert.ToDouble(mdataset.Tables[25].Rows[0]["Column1"].ToString());

        DataSet mDSet20 = null;

        String mStoredProcName20 = String.Empty;
        DbCommand mDbCommand20 = null;
        mDSet20 = new DataSet();
        mStoredProcName20 = StoredProcedure.spr_GetValueForMedium;
        mDbCommand20 = currentDatabase.GetStoredProcCommand(mStoredProcName20);
        currentDatabase.AddInParameter(mDbCommand20, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand20, "@vBoilerID", DbType.String, BoilerID);
        mDSet20 = currentDatabase.ExecuteDataSet(mDbCommand20);


        int w = Convert.ToInt16(mDSet20.Tables[0].Rows[0]["FurnaceRoofCoolingMedium"]);

        DataSet mDSet21 = null;

        String mStoredProcName21 = String.Empty;
        DbCommand mDbCommand21 = null;
        mDSet21 = new DataSet();
        mStoredProcName21 = StoredProcedure.spr_GetValueForEconomizerHangerTubePresentUpstream;
        mDbCommand21 = currentDatabase.GetStoredProcCommand(mStoredProcName21);
        currentDatabase.AddInParameter(mDbCommand21, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand21, "@vBoilerID", DbType.String, BoilerID);
        mDSet21 = currentDatabase.ExecuteDataSet(mDbCommand21);


        int j = Convert.ToInt16(mDSet21.Tables[0].Rows[0]["EconomizerHangerTubePresentUpstream"]);




        if (w == 2 && j == 1)
        {
            mCal_HeatAbsorption_SC.Economizer_hanger_tube = Convert.ToDouble(mdataset.Tables[6].Rows[0]["Value"].ToString());
        }
        else
        {
            mCal_HeatAbsorption_SC.Economizer_hanger_tube = 0;
        }






        mCal_HeatAbsorption_SC.Economizer_circuit = mCal_HeatAbsorption_SC.Economizer_bundle + mCal_HeatAbsorption_SC.Economizer_hanger_tube;




        DataSet mDSet22 = null;

        String mStoredProcName22 = String.Empty;
        DbCommand mDbCommand22 = null;
        mDSet22 = new DataSet();
        mStoredProcName22 = StoredProcedure.spr_GetValueForTypeOfBoiler;
        mDbCommand22 = currentDatabase.GetStoredProcCommand(mStoredProcName22);
        currentDatabase.AddInParameter(mDbCommand22, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand22, "@vBoilerID", DbType.String, BoilerID);
        mDSet22 = currentDatabase.ExecuteDataSet(mDbCommand22);


        int sq = Convert.ToInt16(mDSet22.Tables[0].Rows[0]["TypeOfBoiler"]);

        if (sq == 1)
        {
            mCal_HeatAbsorption_SC.Drum_outlet_temperature = mStream_Macros_Generic.Tsat_p(mCal_HeatAbsorption_SC.Drum_separator_outlet_pressure * 0.980665 + 1.01325);
        }

        else
        {
            mCal_HeatAbsorption_SC.Drum_outlet_temperature = mCal_HeatAbsorption_SC.Separator_outlet_tempertaure;
        }







        mCal_HeatAbsorption_SC.Dry_saturated_steam_enthalpy_of_drum_outlet = mStream_Macros_Generic.h_pT(mCal_HeatAbsorption_SC.Drum_separator_outlet_pressure * 0.980665 + 1.01325, mCal_HeatAbsorption_SC.Drum_outlet_temperature + 0.001);

        mCal_HeatAbsorption_SC.Saturated_water_enthalpy_of_drum_outlet = mStream_Macros_Generic.h_pT(mCal_HeatAbsorption_SC.Drum_separator_outlet_pressure * 0.980665 + 1.01325, mCal_HeatAbsorption_SC.Drum_outlet_temperature - 0.5);


        if (w == 2 && j == 1)
        {
            mCal_HeatAbsorption_SC.Economizer_circuit_outlet_temperature = Convert.ToDouble(mdataset.Tables[7].Rows[0]["Value"].ToString());
        }
        else
        {
            mCal_HeatAbsorption_SC.Economizer_circuit_outlet_temperature = Convert.ToDouble(mdataset.Tables[22].Rows[0]["Value"].ToString());
        }




        //if (w == 2 && j == 1)
        //{
        //    mCal_HeatAbsorption_SC.Economizer_circuit_outlet_pressure = Convert.ToDouble(mdataset.Tables[8].Rows[0]["Value"].ToString());

        //}

        //else
        //{
        //    mCal_HeatAbsorption_SC.Economizer_circuit_outlet_pressure = Convert.ToDouble(mdataset.Tables[20].Rows[0]["Value"].ToString());

        //}
        //bb
        System.Diagnostics.Debug.WriteLine("===== FULL TABLE DEBUG START =====");

        int[] tablesToCheck = { 8, 20 };

        foreach (int t in tablesToCheck)
        {
            System.Diagnostics.Debug.WriteLine("---- Checking Table[" + t + "] ----");

            if (mdataset.Tables.Count <= t)
            {
                System.Diagnostics.Debug.WriteLine("❌ Table[" + t + "] does NOT exist");
                continue;
            }

            // ✅ Show Columns
            System.Diagnostics.Debug.WriteLine("Columns:");
            foreach (DataColumn col in mdataset.Tables[t].Columns)
            {
                System.Diagnostics.Debug.WriteLine("   " + col.ColumnName);
            }

            // ✅ Show Rows + Values
            if (mdataset.Tables[t].Rows.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("✅ Row exists");

                for (int c = 0; c < mdataset.Tables[t].Columns.Count; c++)
                {
                    string colName = mdataset.Tables[t].Columns[c].ColumnName;
                    string valu = mdataset.Tables[t].Rows[0][c]?.ToString();

                    System.Diagnostics.Debug.WriteLine(
                        "   VALUE [" + colName + "] = " + valu
                    );
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("❌ No Rows in Table[" + t + "]");
            }
        }
        double val = 0;

        if (w == 2 && j == 1)
        {
            if (mdataset.Tables.Count > 8 && mdataset.Tables[8].Rows.Count > 0)
            {
                double.TryParse(
                    mdataset.Tables[8].Rows[0][0]?.ToString(),
                    out val
                );
            }
        }
        else
        {
            if (mdataset.Tables.Count > 20 && mdataset.Tables[20].Rows.Count > 0)
            {
                double.TryParse(
                    mdataset.Tables[20].Rows[0][0]?.ToString(),
                    out val
                );
            }
        }

        mCal_HeatAbsorption_SC.Economizer_circuit_outlet_pressure = val;



        DataSet mDSet45 = null;

        String mStoredProcName45 = String.Empty;
        DbCommand mDbCommand45 = null;
        mDSet45 = new DataSet();
        mStoredProcName45 = StoredProcedure.spr_GetValueForMedium;
        mDbCommand45 = currentDatabase.GetStoredProcCommand(mStoredProcName45);
        currentDatabase.AddInParameter(mDbCommand45, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand45, "@vBoilerID", DbType.String, BoilerID);
        mDSet45 = currentDatabase.ExecuteDataSet(mDbCommand45);


        DataSet mDSet46 = null;

        String mStoredProcName46 = String.Empty;
        DbCommand mDbCommand46 = null;
        mDSet46 = new DataSet();
        mStoredProcName46 = StoredProcedure.spr_GetValueFor_EconomizerHangerTubePresentUpstream;
        mDbCommand46 = currentDatabase.GetStoredProcCommand(mStoredProcName46);
        currentDatabase.AddInParameter(mDbCommand46, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand46, "@vBoilerID", DbType.String, BoilerID);
        mDSet46 = currentDatabase.ExecuteDataSet(mDbCommand46);


        int uy = Convert.ToInt16(mDSet46.Tables[0].Rows[0]["EconomizerHangerTubePresentUpstream"]);

        int qd = Convert.ToInt16(mDSet45.Tables[0].Rows[0]["FurnaceRoofCoolingMedium"]);


        mCal_HeatAbsorption_SC.Economizer_circuit_outlet_enthalpy = mStream_Macros_Generic.h_pT(mCal_HeatAbsorption_SC.Economizer_circuit_outlet_pressure * 0.980665 + 1.01325, mCal_HeatAbsorption_SC.Economizer_circuit_outlet_temperature);

        mCal_HeatAbsorption_SC.Steam_flow_rate_in_furnace = Convert.ToDouble(mdataset.Tables[9].Rows[0]["Value"].ToString());

        //mCal_HeatAbsorption_SC.Blow_down_flow = Convert.ToDouble(mdataset.Tables[10].Rows[0]["Value"].ToString());
        //BB
        double blowDownFlow = 0;

        if (mdataset.Tables.Count > 10 && mdataset.Tables[10].Rows.Count > 0)
        {
            double.TryParse(
                mdataset.Tables[10].Rows[0][0]?.ToString(),
                out blowDownFlow
            );
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("❌ Blow Down Flow missing in Table[10] → default 0");
        }

        mCal_HeatAbsorption_SC.Blow_down_flow = blowDownFlow;


        mCal_HeatAbsorption_SC.Blowdown_Heat_corresponding_to_the_drum_outlet_and_eco_Outlet_enthalpies = (mCal_HeatAbsorption_SC.Saturated_water_enthalpy_of_drum_outlet - mCal_HeatAbsorption_SC.Economizer_circuit_outlet_enthalpy) * mCal_HeatAbsorption_SC.Blow_down_flow / (3.6 * 1000);


        //(E26-E30)*(E31-E32)/(3.6*1000)
        mCal_HeatAbsorption_SC.Heat_corresponding_to_the_drum_outlet_and_eco_Outlet_enthalpies = (mCal_HeatAbsorption_SC.Dry_saturated_steam_enthalpy_of_drum_outlet - mCal_HeatAbsorption_SC.Economizer_circuit_outlet_enthalpy) * (mCal_HeatAbsorption_SC.Steam_flow_rate_in_furnace - mCal_HeatAbsorption_SC.Blow_down_flow) / (3.6 * 1000);

        mCal_HeatAbsorption_SC.Total_furnace_absorption_Design = mCal_HeatAbsorption_SC.Blowdown_Heat_corresponding_to_the_drum_outlet_and_eco_Outlet_enthalpies + mCal_HeatAbsorption_SC.Heat_corresponding_to_the_drum_outlet_and_eco_Outlet_enthalpies;



        ////
        mCal_HeatAbsorption_SC.Heat_Input_By_1Kg_fuel_4B = Convert.ToDouble(mdataset.Tables[11].Rows[0]["Value"].ToString());

        mCal_HeatAbsorption_SC.Downstream_element_Inlet_flue_gas_enthalpy_4B = Convert.ToDouble(mdataset.Tables[12].Rows[0]["Value"].ToString());

        mCal_HeatAbsorption_SC.Inlet_direct_radiation_from_the_furnace_5B = Convert.ToDouble(mdataset.Tables[13].Rows[0]["Value"].ToString());

        mCal_HeatAbsorption_SC.Furnace_radiation_heat_absorbed_by_furnace_roof_cover_5B = Convert.ToDouble(mdataset.Tables[14].Rows[0]["Value"].ToString());
        ////



        //=((D10*('4B'!F74-'4B'!M91))-'5B'!F86-'5B'!F138)*D9/1000
        mCal_HeatAbsorption_SC.Heat_absorbed_by_water_walls_in_upper_and_lower_furnace = ((mCal_HeatAbsorption_SC.Heat_preservation_coefficient * (mCal_HeatAbsorption_SC.Heat_Input_By_1Kg_fuel_4B - mCal_HeatAbsorption_SC.Downstream_element_Inlet_flue_gas_enthalpy_4B)) - mCal_HeatAbsorption_SC.Inlet_direct_radiation_from_the_furnace_5B - mCal_HeatAbsorption_SC.Furnace_radiation_heat_absorbed_by_furnace_roof_cover_5B) * mCal_HeatAbsorption_SC.Design_fuel_consumption / 1000;

        mCal_HeatAbsorption_SC.WaterWall_Absorption_for_5B = Convert.ToDouble(mdataset.Tables[15].Rows[0]["Value"].ToString());

        mCal_HeatAbsorption_SC.WaterWall_Absorption_for_6B = Convert.ToDouble(mdataset.Tables[16].Rows[0]["Value"].ToString());


        mStoredProcName1 = StoredProcedure.spr_GetValueOfCrossDuct;
        mDbCommand1 = currentDatabase.GetStoredProcCommand(mStoredProcName1);

        currentDatabase.AddInParameter(mDbCommand1, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand1, "@vBoilerID", DbType.String, BoilerID);

        mDSet1 = currentDatabase.ExecuteDataSet(mDbCommand1);

        for (int i = 0; i < mDSet1.Tables[0].Rows.Count; i++)
        {
            CrossDuctValue.Add(Convert.ToDouble(mDSet1.Tables[0].Rows[i][0].ToString()));

            // mCal_HeatAbsorption_SC.Heat_absorbed_by_water_walls_in_cross_duct_and_5B_location = CrossDuctValue[i];
        }

        // mCal_HeatAbsorption_SC.Heat_absorbed_by_water_walls_in_cross_duct_and_5B_location = mCal_HeatAbsorption_SC.WaterWall_Absorption_for_5B + mCal_HeatAbsorption_SC.WaterWall_Absorption_for_6B + CrossDuctValue.AsQueryable().Sum();

        mCal_HeatAbsorption_SC.Heat_absorbed_by_water_walls_in_cross_duct_and_5B_location = mCal_HeatAbsorption_SC.WaterWall_Absorption_for_5B + mCal_HeatAbsorption_SC.WaterWall_Absorption_for_6B + CrossDuctValue.ElementAt(0) + CrossDuctValue.ElementAt(1) + CrossDuctValue.ElementAt(2);



        mCal_HeatAbsorption_SC.Reverse_chamber = Convert.ToDouble(mdataset.Tables[17].Rows[0]["Value"].ToString());

        mCal_HeatAbsorption_SC.Steam_screen = Convert.ToDouble(mdataset.Tables[18].Rows[0]["Value"].ToString());

        // mCal_HeatAbsorption_SC.Separator_outlet_tempertaure = Convert.ToDouble(mdataset.Tables[19].Rows[0]["Value"].ToString());

        // DataSet mDSet20 = null;

        // String mStoredProcName20 = String.Empty;
        // DbCommand mDbCommand20 = null;
        // mDSet20 = new DataSet();
        // mStoredProcName20 = StoredProcedure.spr_GetValueOfPresenceOfCooledWater;
        // mDbCommand20 = currentDatabase.GetStoredProcCommand(mStoredProcName20);
        // currentDatabase.AddInParameter(mDbCommand20, "@vProjectID", DbType.String, ProjectID);
        // currentDatabase.AddInParameter(mDbCommand20, "@vBoilerID", DbType.String, BoilerID);
        // mDSet20 = currentDatabase.ExecuteDataSet(mDbCommand20);

        // int a = Convert.ToInt16(mDSet20.Tables[0].Rows[0]["PresenceOfCooledWater"]);

        //if (a==1)
        //{
        //    mCal_HeatAbsorption_SC.Steam_side_wall = Convert.ToDouble(mdataset.Tables[19].Rows[0]["Value"].ToString());
        //}
        //else
        //{
        mCal_HeatAbsorption_SC.Steam_side_wall = 0;

        mStoredProcName5 = StoredProcedure.spr_GenericB_Get_ValueForRHSteamTempControl;
        mDbCommand5 = currentDatabase.GetStoredProcCommand(mStoredProcName5);

        currentDatabase.AddInParameter(mDbCommand5, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand5, "@vBoilerID", DbType.String, BoilerID);

        mDSet5 = currentDatabase.ExecuteDataSet(mDbCommand5);

        int b = Convert.ToInt16(mDSet5.Tables[0].Rows[0]["RHSteamTempControl"]);






        mStoredProcName6 = StoredProcedure.spr_Get_CountOf_HeatingElements;
        mDbCommand6 = currentDatabase.GetStoredProcCommand(mStoredProcName6);

        currentDatabase.AddInParameter(mDbCommand6, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand6, "@vBoilerID", DbType.String, BoilerID);

        mDSet6 = currentDatabase.ExecuteDataSet(mDbCommand6);

        int z = Convert.ToInt16(mDSet6.Tables[1].Rows[0]["CountSH"]);

        int g = Convert.ToInt16(mDSet6.Tables[2].Rows[0]["CountRH"]);

        int o = Convert.ToInt16(mDSet6.Tables[3].Rows[0]["CountWS"]);

        int n = Convert.ToInt16(mDSet6.Tables[0].Rows[0]["CountEC"]);




        mStoredProcName2 = StoredProcedure.spr_Get_SectionIDOf_HeatingElements;
        mDbCommand2 = currentDatabase.GetStoredProcCommand(mStoredProcName2);

        currentDatabase.AddInParameter(mDbCommand2, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand2, "@vBoilerID", DbType.String, BoilerID);

        mDSet2 = currentDatabase.ExecuteDataSet(mDbCommand2);

        //Economizer

        for (int i = 0; i < n; i++)
        {
            //WaterScreenID.Add(Convert.ToString(mDSet2.Tables[3].Rows[i][0].ToString()));
            EconomizerID.Add(Convert.ToString(mDSet2.Tables[0].Rows[i]["SectionID"].ToString()));
        }




        //WaterScreen



        for (int i = 0; i < o; i++)
        {
            //WaterScreenID.Add(Convert.ToString(mDSet2.Tables[3].Rows[i][0].ToString()));
            WaterScreenID.Add(Convert.ToString(mDSet2.Tables[3].Rows[i]["SectionID"].ToString()));
        }


        List<Double> WaterScreen_Value = new List<Double>();
        for (int rj = 0; rj < WaterScreenID.Count; rj++)
        {

            mStoredProcName55 = StoredProcedure.spr_GenericB_Get_ValueForSuperheater_demo;
            mDbCommand55 = currentDatabase.GetStoredProcCommand(mStoredProcName55);

            currentDatabase.AddInParameter(mDbCommand55, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDbCommand55, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand55, "@vSectionID", DbType.String, WaterScreenID.ElementAt(rj));
            currentDatabase.AddInParameter(mDbCommand55, "@vObjectiveID", DbType.Int16, ObjectiveID);
            currentDatabase.AddInParameter(mDbCommand55, "@vBoilerLoad", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(mDbCommand55, "@vRHSteamTempControl", DbType.String, b);

            mDSet55 = currentDatabase.ExecuteDataSet(mDbCommand55);




            for (int i = 0; i < mDSet55.Tables.Count; i++)
            {


                WaterScreen_Value.Add(Convert.ToDouble(mDSet55.Tables[i].Rows[0][0].ToString()));

            }




        }


        double WaterScreenSummation = WaterScreen_Value.Sum();

        mCal_HeatAbsorption_SC.Heat_absorbed_by_water_screens = WaterScreenSummation;

        mCal_HeatAbsorption_SC.Total_furnace_absorption = mCal_HeatAbsorption_SC.Heat_absorbed_by_water_walls_in_upper_and_lower_furnace + mCal_HeatAbsorption_SC.Heat_absorbed_by_water_walls_in_cross_duct_and_5B_location + mCal_HeatAbsorption_SC.Heat_absorbed_by_water_screens;

        mCal_HeatAbsorption_SC.Furnace_outlet_steam_enthalpy = ((mCal_HeatAbsorption_SC.Total_furnace_absorption - mCal_HeatAbsorption_SC.Blowdown_Heat_corresponding_to_the_drum_outlet_and_eco_Outlet_enthalpies) * 3.6 * 1000 / (mCal_HeatAbsorption_SC.Steam_flow_rate_in_furnace - mCal_HeatAbsorption_SC.Blow_down_flow)) + mCal_HeatAbsorption_SC.Economizer_circuit_outlet_enthalpy;

        mCal_HeatAbsorption_SC.Error_in_heat_absorption = (mCal_HeatAbsorption_SC.Furnace_outlet_steam_enthalpy - mCal_HeatAbsorption_SC.Dry_saturated_steam_enthalpy_of_drum_outlet) / mCal_HeatAbsorption_SC.Dry_saturated_steam_enthalpy_of_drum_outlet * 100;

        ///SuperHeater

        //for (int i = 0; i < z; i++)
        //{

        //    SuperheaterID.Add(Convert.ToString(mDSet2.Tables[1].Rows[i]["SectionID"].ToString()));

        //}

        //List<Double> SuperHeater_Value = new List<Double>();

        //for (int i = 0; i < SuperheaterID.Count; i++)
        //{

        //    mStoredProcName3 = StoredProcedure.spr_GenericB_Get_ValueForSuperheater_demo;
        //    mDbCommand3 = currentDatabase.GetStoredProcCommand(mStoredProcName3);

        //    currentDatabase.AddInParameter(mDbCommand3, "@vProjectID", DbType.String, ProjectID);
        //    currentDatabase.AddInParameter(mDbCommand3, "@vBoilerID", DbType.String, BoilerID);
        //    currentDatabase.AddInParameter(mDbCommand3, "@vSectionID", DbType.String, SuperheaterID.ElementAt(i));
        //    currentDatabase.AddInParameter(mDbCommand3, "@vObjectiveID", DbType.Int16, ObjectiveID);
        //    currentDatabase.AddInParameter(mDbCommand3, "@vBoilerLoad", DbType.String, BoilerLoad);
        //    currentDatabase.AddInParameter(mDbCommand3, "@vRHSteamTempControl", DbType.String, b);

        //    mDSet3 = currentDatabase.ExecuteDataSet(mDbCommand3);

        //    for (int kk = 0; kk < mDSet3.Tables.Count; kk++)
        //    {

        //        SuperHeater_Value.Add(Convert.ToDouble(mDSet3.Tables[kk].Rows[0][0].ToString()));

        //    }



        //}
        // List<double> SuperHeater_Value = new List<double>();

        // for (int i = 0; i < SuperheaterID.Count; i++)
        // {
        //     mStoredProcName3 = StoredProcedure.spr_GenericB_Get_ValueForSuperheater_demo;
        //     mDbCommand3 = currentDatabase.GetStoredProcCommand(mStoredProcName3);

        //     currentDatabase.AddInParameter(mDbCommand3, "@vProjectID", DbType.String, ProjectID);
        //     currentDatabase.AddInParameter(mDbCommand3, "@vBoilerID", DbType.String, BoilerID);
        //     currentDatabase.AddInParameter(mDbCommand3, "@vSectionID", DbType.String, SuperheaterID.ElementAt(i));
        //     currentDatabase.AddInParameter(mDbCommand3, "@vObjectiveID", DbType.Int16, ObjectiveID);
        //     currentDatabase.AddInParameter(mDbCommand3, "@vBoilerLoad", DbType.String, BoilerLoad);
        //     currentDatabase.AddInParameter(mDbCommand3, "@vRHSteamTempControl", DbType.String, b);

        //     mDSet3 = currentDatabase.ExecuteDataSet(mDbCommand3);

        //     for (int kk = 0; kk < mDSet3.Tables.Count; kk++)
        //     {
        //         if (mDSet3.Tables[kk].Rows.Count > 0)
        //         {
        //             double valu = 0;

        //             double.TryParse(
        //                 mDSet3.Tables[kk].Rows[0][0]?.ToString(),
        //                 out valu
        //             );

        //             SuperHeater_Value.Add(valu);
        //         }
        //         else
        //         {
        //             System.Diagnostics.Debug.WriteLine(
        //                 $"❌ Empty table in SuperHeater SP, Table Index: {kk}"
        //             );

        //             SuperHeater_Value.Add(0);
        //         }
        //     }
        // }

        // double LTSH = SuperHeater_Value.ElementAt(0);

        // double Pendant_LTSH = SuperHeater_Value.ElementAt(1);

        // double Platen_SH = SuperHeater_Value.ElementAt(2);

        // double Final_SH = SuperHeater_Value.ElementAt(3);

        // mCal_HeatAbsorption_SC.LTSH = LTSH;

        // mCal_HeatAbsorption_SC.Pendant_LTSH = Pendant_LTSH;

        // mCal_HeatAbsorption_SC.Platen_SH = Platen_SH;

        // mCal_HeatAbsorption_SC.Final_SH = Final_SH;

        //// mCal_HeatAbsorption_SC.Superheater_circuit_total = mCal_HeatAbsorption_SC.LTSH + mCal_HeatAbsorption_SC.Pendant_LTSH + mCal_HeatAbsorption_SC.Platen_SH + mCal_HeatAbsorption_SC.Final_SH;

        // mCal_HeatAbsorption_SC.Superheater_circuit_total = SuperHeater_Value.Sum();
        List<double> SuperHeater_Value = new List<double>();

        System.Diagnostics.Debug.WriteLine("🚀 ===== Superheater Calculation START =====");

        // ================= SUPERHEATER =================

        for (int i = 0; i < z; i++)
        {
            if (mDSet2.Tables.Count > 1 &&
                mDSet2.Tables[1].Rows.Count > i)
            {
                string sectionId =
                    Convert.ToString(
                        mDSet2.Tables[1].Rows[i]["SectionID"]
                    );

                SuperheaterID.Add(sectionId);

                System.Diagnostics.Debug.WriteLine(
                    $"✅ Added SuperheaterID = {sectionId}"
                );
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(
                    $"❌ Missing Superheater SectionID at index {i}"
                );
            }
        }

        // ADD HERE
        System.Diagnostics.Debug.WriteLine(
            $"✅ SuperheaterID Count = {SuperheaterID.Count}"
        );

        foreach (var id in SuperheaterID)
        {
            System.Diagnostics.Debug.WriteLine(
                $"✅ SuperheaterID = {id}"
            );
        }


        for (int i = 0; i < SuperheaterID.Count; i++)
        {
            string currentSectionID = SuperheaterID.ElementAt(i);

            System.Diagnostics.Debug.WriteLine(
                $"🔹 Iteration {i} | Calling SP for SectionID: {currentSectionID}"
            );

            mStoredProcName3 = StoredProcedure.spr_GenericB_Get_ValueForSuperheater_demo;
            mDbCommand3 = currentDatabase.GetStoredProcCommand(mStoredProcName3);

            currentDatabase.AddInParameter(mDbCommand3, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDbCommand3, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand3, "@vSectionID", DbType.String, currentSectionID);
            currentDatabase.AddInParameter(mDbCommand3, "@vObjectiveID", DbType.Int16, ObjectiveID);
            currentDatabase.AddInParameter(mDbCommand3, "@vBoilerLoad", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(mDbCommand3, "@vRHSteamTempControl", DbType.Int32, Convert.ToInt32(b));

            mDSet3 = currentDatabase.ExecuteDataSet(mDbCommand3);


            System.Diagnostics.Debug.WriteLine(
                $"✅ SP Executed for SectionID = {currentSectionID}"
            );

            if (mDSet3 != null)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"✅ Dataset Tables Count = {mDSet3.Tables.Count}"
                );
            }


            // ✅ CHECK DATASET
            if (mDSet3 == null)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"❌ Dataset is NULL for SectionID: {currentSectionID}"
                );
                continue;
            }

            if (mDSet3.Tables.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"❌ No tables returned for SectionID: {currentSectionID}"
                );
                continue;
            }

            var table = mDSet3.Tables[0];

            System.Diagnostics.Debug.WriteLine(
                $"✅ Tables Count: {mDSet3.Tables.Count} | Rows in Table[0]: {table.Rows.Count}"
            );

            // ✅ READ ROWS
            if (table.Rows.Count > 0)
            {
                int rowIndex = 0;

                foreach (DataRow row in table.Rows)
                {
                    string rawValue = row[0]?.ToString();

                    double valu = 0;
                    bool parsed = double.TryParse(rawValue, out valu);

                    System.Diagnostics.Debug.WriteLine(
                        $"   ➤ Row {rowIndex} | Raw: {rawValue} | Parsed: {valu} | Success: {parsed}"
                    );

                    System.Diagnostics.Debug.WriteLine(
                        $"SectionID = {currentSectionID}, Value = {valu}"
                    );


                    SuperHeater_Value.Add(valu);
                    rowIndex++;
                }

                System.Diagnostics.Debug.WriteLine(
                    $"✅ Data found for SectionID: {currentSectionID}, breaking loop"
                );

                // ✅ Break only after data found
                //break;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(
                    $"❌ Table has ZERO rows for SectionID: {currentSectionID}"
                );
                SuperHeater_Value.Add(0);
            }
        }

        System.Diagnostics.Debug.WriteLine(
            $"🔸 Final SuperHeater_Value Count = {SuperHeater_Value.Count}"
        );


        // ✅ SAFE ACCESS
        double GetSafeValue(List<double> list, int index)
        {
            if (index >= 0 && index < list.Count)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"✅ Accessing index {index}, value = {list[index]}"
                );
                return list[index];
            }

            System.Diagnostics.Debug.WriteLine(
                $"❌ Index {index} out of range (Count={list.Count})"
            );

            return 0;
        }
        System.Diagnostics.Debug.WriteLine(
       "===== FINAL SUPERHEATER VALUES LIST ====="
       );

        for (int x = 0; x < SuperHeater_Value.Count; x++)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Index {x} = {SuperHeater_Value[x]}"
            );
        }


        // ✅ ASSIGN VALUES
        double LTSH = GetSafeValue(SuperHeater_Value, 0);
        double Pendant_LTSH = GetSafeValue(SuperHeater_Value, 1);
        double Platen_SH = GetSafeValue(SuperHeater_Value, 2);
        double Final_SH = GetSafeValue(SuperHeater_Value, 3);

        //// ✅ ASSIGN VALUES
        //double LTSH = GetSafeValue(SuperHeater_Value, 0);
        //double Platen_SH = GetSafeValue(SuperHeater_Value, 1);      // swapped
        //double Pendant_LTSH = GetSafeValue(SuperHeater_Value, 2);   // swapped
        //double Final_SH = GetSafeValue(SuperHeater_Value, 3);



        // ✅ PRINT FINAL VALUES
        System.Diagnostics.Debug.WriteLine($"🔥 LTSH = {LTSH}");
        System.Diagnostics.Debug.WriteLine($"🔥 Pendant_LTSH = {Pendant_LTSH}");
        System.Diagnostics.Debug.WriteLine($"🔥 Platen_SH = {Platen_SH}");
        System.Diagnostics.Debug.WriteLine($"🔥 Final_SH = {Final_SH}");




        // ✅ SET VALUES
        mCal_HeatAbsorption_SC.LTSH = LTSH;
        mCal_HeatAbsorption_SC.Pendant_LTSH = Pendant_LTSH;
        mCal_HeatAbsorption_SC.Platen_SH = Platen_SH;
        mCal_HeatAbsorption_SC.Final_SH = Final_SH;


        // ✅ TOTAL
        mCal_HeatAbsorption_SC.Superheater_circuit_total = SuperHeater_Value.Sum() + mCal_HeatAbsorption_SC.Reverse_chamber;

        System.Diagnostics.Debug.WriteLine(
            $"✅ Total Superheater Circuit = {mCal_HeatAbsorption_SC.Superheater_circuit_total}"
        );

        System.Diagnostics.Debug.WriteLine("✅ ===== Superheater Calculation END =====");


        //Reheater

        //for (int i = 0; i < g; i++)
        //{


        //    ReheaterID.Add(Convert.ToString(mDSet2.Tables[2].Rows[i]["SectionID"].ToString()));
        //}

        //List<Double> Reheater_Value = new List<Double>();

        //for (int kl = 0; kl < ReheaterID.Count; kl++)
        //{

        //    mStoredProcName3 = StoredProcedure.spr_GenericB_Get_ValueForSuperheater_demo;
        //    mDbCommand3 = currentDatabase.GetStoredProcCommand(mStoredProcName3);

        //    currentDatabase.AddInParameter(mDbCommand3, "@vProjectID", DbType.String, ProjectID);
        //    currentDatabase.AddInParameter(mDbCommand3, "@vBoilerID", DbType.String, BoilerID);
        //    currentDatabase.AddInParameter(mDbCommand3, "@vSectionID", DbType.String, ReheaterID.ElementAt(kl));
        //    currentDatabase.AddInParameter(mDbCommand3, "@vObjectiveID", DbType.Int16, ObjectiveID);
        //    currentDatabase.AddInParameter(mDbCommand3, "@vBoilerLoad", DbType.String, BoilerLoad);
        //    currentDatabase.AddInParameter(mDbCommand3, "@vRHSteamTempControl", DbType.String, b);

        //    mDSet89 = currentDatabase.ExecuteDataSet(mDbCommand3);

        //    for (int kk = 0; kk < mDSet3.Tables.Count; kk++)
        //    {

        //        // Reheater_Value.Add(Convert.ToDouble(mDSet3.Tables[kk].Rows[0][0].ToString()));
        //        Reheater_Value.Add(Convert.ToDouble(mDSet89.Tables[kk].Rows[0][0].ToString()));

        //    }



        //}


        //double Front_RH = Reheater_Value.ElementAt(0);

        //double Rear_RH = Reheater_Value.ElementAt(1);

        //mCal_HeatAbsorption_SC.Front_RH = Front_RH;

        //mCal_HeatAbsorption_SC.Rear_RH = Rear_RH;


        //double Sum1 = Reheater_Value.Sum();

        ////mCal_HeatAbsorption_SC.RH_circuit_total = Sum1;

        //mCal_HeatAbsorption_SC.RH_circuit_total = Reheater_Value.Sum();

        // ================= REHEATER =================

        // ✅ SAFE FETCH IDs
        for (int i = 0; i < g; i++)
        {
            if (mDSet2.Tables.Count > 2 && mDSet2.Tables[2].Rows.Count > i)
            {
                ReheaterID.Add(
                    Convert.ToString(mDSet2.Tables[2].Rows[i]["SectionID"])
                );
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(
                    $"❌ Missing Reheater SectionID at index {i}"
                );
            }
        }

        List<double> Reheater_Value = new List<double>();

        for (int kl = 0; kl < ReheaterID.Count; kl++)
        {
            mStoredProcName3 = StoredProcedure.spr_GenericB_Get_ValueForSuperheater_demo;
            mDbCommand3 = currentDatabase.GetStoredProcCommand(mStoredProcName3);

            currentDatabase.AddInParameter(mDbCommand3, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDbCommand3, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand3, "@vSectionID", DbType.String, ReheaterID.ElementAt(kl));
            currentDatabase.AddInParameter(mDbCommand3, "@vObjectiveID", DbType.Int16, ObjectiveID);
            currentDatabase.AddInParameter(mDbCommand3, "@vBoilerLoad", DbType.String, BoilerLoad);
            //currentDatabase.AddInParameter(mDbCommand3, "@vRHSteamTempControl", DbType.String, b);
            currentDatabase.AddInParameter(mDbCommand3, "@vRHSteamTempControl", DbType.Int32, Convert.ToInt32(b));


            mDSet89 = currentDatabase.ExecuteDataSet(mDbCommand3);

            // ✅ CHECK DATASET
            if (mDSet89 == null || mDSet89.Tables.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"❌ No result for Reheater SectionID: {ReheaterID[kl]}"
                );

                Reheater_Value.Add(0);
                continue;
            }

            for (int kk = 0; kk < mDSet89.Tables.Count; kk++)   // ✅ FIXED dataset
            {
                if (mDSet89.Tables[kk].Rows.Count > 0)
                {
                    double valu = 0;

                    double.TryParse(
                        mDSet89.Tables[kk].Rows[0][0]?.ToString(),
                        out valu
                    );

                    Reheater_Value.Add(valu);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"❌ Empty Reheater table | SectionID: {ReheaterID[kl]} | Table: {kk}"
                    );

                    Reheater_Value.Add(0);
                }
            }
        }

        // ✅ DEBUG
        System.Diagnostics.Debug.WriteLine(
            $"Reheater_Value Count = {Reheater_Value.Count}"
        );


        // ✅ SAFE FUNCTION
        //double GetSafeValue(List<double> list, int index)
        //{
        //    if (index >= 0 && index < list.Count)
        //        return list[index];

        //    System.Diagnostics.Debug.WriteLine(
        //        $"❌ Reheater Index {index} out of range (Count={list.Count})"
        //    );

        //    return 0;
        //}


        // ✅ SAFE ASSIGNMENT
        double Front_RH = GetSafeValue(Reheater_Value, 0);
        double Rear_RH = GetSafeValue(Reheater_Value, 1);


        // ✅ SET VALUES
        mCal_HeatAbsorption_SC.Front_RH = Front_RH;
        mCal_HeatAbsorption_SC.Rear_RH = Rear_RH;


        // ✅ TOTAL
        mCal_HeatAbsorption_SC.RH_circuit_total = Reheater_Value.Sum();







        mStoredProcName40 = StoredProcedure.spr_GenericB_LocationWise_HeatingElement;
        mDbCommand40 = currentDatabase.GetStoredProcCommand(mStoredProcName40);

        currentDatabase.AddInParameter(mDbCommand40, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand40, "@vBoilerID", DbType.String, BoilerID);
        currentDatabase.AddInParameter(mDbCommand40, "@vRHSteamTempConrol", DbType.Int32, Convert.ToInt32(b));
        //currentDatabase.AddInParameter(mDbCommand40, "@vRHSteamTempConrol", DbType.String, b);


        mDSet40 = currentDatabase.ExecuteDataSet(mDbCommand40);

        int ds = mDSet40.Tables.Count;

        for (int i = 0; i < ds - 1; i++)
        {
            for (int r = 0; r < mDSet40.Tables[i].Rows.Count; r++)
            {

                SectionID.Add(mDSet40.Tables[i].Rows[r][0].ToString());
            }
        }
        List<Double> Value_Arra = new List<Double>();
        for (int i = 0; i < SectionID.Count - 1; i++)
        {


            mStoredProcName50 = StoredProcedure.spr_GetInputForUpperFurnace;
            mDbCommand50 = currentDatabase.GetStoredProcCommand(mStoredProcName50);

            currentDatabase.AddInParameter(mDbCommand50, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDbCommand50, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDbCommand50, "@vSectionID", DbType.String, SectionID.ElementAt(i));
            currentDatabase.AddInParameter(mDbCommand50, "@vObjectiveID", DbType.Int16, ObjectiveID);
            currentDatabase.AddInParameter(mDbCommand50, "@vBoilerLoad", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(mDbCommand50, "@vRHSteamTempControl", DbType.Int32, Convert.ToInt32(b));
            // currentDatabase.AddInParameter(mDbCommand50, "@vRHSteamTempControl", DbType.String, b);



            mDSet50 = currentDatabase.ExecuteDataSet(mDbCommand50);
            for (int fg = 0; fg < mDSet50.Tables.Count; fg++)
            {
                if (mDSet50.Tables[fg].Rows.Count == 0)
                {
                    Value_Arra.Add(0);
                }
                else
                {
                    object value = mDSet50.Tables[fg].Rows[0][0];

                    if (value == null || value == DBNull.Value)
                    {
                        Value_Arra.Add(0);
                    }
                    else
                    {
                        double dValue = Convert.ToDouble(value);

                        if (double.IsNaN(dValue))
                        {
                            Value_Arra.Add(0);
                        }
                        else
                        {
                            Value_Arra.Add(dValue);
                        }
                    }
                }
            }


            //for (int fg = 0; fg < mDSet50.Tables.Count-1; fg++)
            //{

            //    if (mDSet50.Tables[fg].Rows.Count == 0)
            //    {

            //    }
            //    else
            //    {
            //        Value_Arra.Add(Convert.ToDouble(mDSet50.Tables[fg].Rows[0][0].ToString()));


            //    }
            //}

        }



        double Sum = Value_Arra.Sum();

        mStoredProcName70 = StoredProcedure.spr_GetInputForReverseChamberForHeatAbsorption;
        mDbCommand70 = currentDatabase.GetStoredProcCommand(mStoredProcName70);

        currentDatabase.AddInParameter(mDbCommand70, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand70, "@vBoilerID", DbType.String, BoilerID);
        currentDatabase.AddInParameter(mDbCommand70, "@vObjectiveID", DbType.Int16, ObjectiveID);
        currentDatabase.AddInParameter(mDbCommand70, "@vBoilerLoad", DbType.String, BoilerLoad);


        mDSet70 = currentDatabase.ExecuteDataSet(mDbCommand70);

        int rr = Convert.ToInt16(mDSet70.Tables[0].Rows[0]["VALUE"]);

        int qq = Convert.ToInt16(mDSet70.Tables[1].Rows[0]["VALUE"]);

        mCal_HeatAbsorption_SC.Furnace_roof = Sum + rr + qq;

        mCal_HeatAbsorption_SC.Superheater_circuit_total = SuperHeater_Value.Sum() + mCal_HeatAbsorption_SC.Reverse_chamber+ mCal_HeatAbsorption_SC.Furnace_roof;



        // mDSet30 = currentDatabase.ExecuteDataSet(mDbCommand30);





        // =((E22+E39+E57+E63)/(D9*'1A'!D15/1000))*100
        mCal_HeatAbsorption_SC.Boiler_Efficiency = ((mCal_HeatAbsorption_SC.Economizer_circuit + mCal_HeatAbsorption_SC.Total_furnace_absorption + mCal_HeatAbsorption_SC.Superheater_circuit_total + mCal_HeatAbsorption_SC.RH_circuit_total) / (mCal_HeatAbsorption_SC.Design_fuel_consumption * mCal_HeatAbsorption_SC.Higher_Heating_Value / 1000)) * 100;

        String mStoredProcName = String.Empty;

        String mStoredProcName500 = String.Empty;

        String mStoredProcName700 = String.Empty;



        DbCommand mDbCommand100 = null;

        DbCommand mDbCommand500 = null;

        DbCommand mDbCommand700 = null;





        DataSet mDSet100;

        DataSet mDSet200;

        DataSet mDSet300;

        DataSet mDSet500;

        DataSet mDSet600;

        DataSet mDSet700;

        string[] Economizer, Reheater, Superheater, EconomizerID1, SuperHeaterID1, ReheaterID1;

        EconomizerID1 = new string[50];

        Economizer = new string[50];

        Reheater = new string[50];

        Superheater = new string[50];

        SuperHeaterID1 = new string[50];

        ReheaterID1 = new string[50];

        mStoredProcName500 = StoredProcedure.spr_Get_SectionValueOf_HeatingElements;
        mDbCommand500 = currentDatabase.GetStoredProcCommand(mStoredProcName500);

        currentDatabase.AddInParameter(mDbCommand500, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand500, "@vBoilerID", DbType.String, BoilerID);

        mDSet500 = currentDatabase.ExecuteDataSet(mDbCommand500);

        for (int i = 0; i < mDSet500.Tables[0].Rows.Count; i++)
        {
            string Value = mDSet500.Tables[0].Rows[i]["SectionValue"].ToString();
            Economizer[i] = mDSet500.Tables[0].Rows[i]["SectionValue"].ToString();
        }

        mDSet600 = currentDatabase.ExecuteDataSet(mDbCommand500);

        for (int i = 0; i < mDSet600.Tables[1].Rows.Count; i++)
        {
            string Value = mDSet600.Tables[1].Rows[i]["SectionValue"].ToString();
            Superheater[i] = mDSet600.Tables[1].Rows[i]["SectionValue"].ToString();
        }

        mDSet700 = currentDatabase.ExecuteDataSet(mDbCommand500);

        for (int i = 0; i < mDSet700.Tables[2].Rows.Count; i++)
        {
            string Value = mDSet700.Tables[2].Rows[i]["SectionValue"].ToString();
            Reheater[i] = mDSet700.Tables[2].Rows[i]["SectionValue"].ToString();
        }


        mStoredProcName700 = StoredProcedure.spr_S_parameter_HeatinElement_Sequence;
        mDbCommand700 = currentDatabase.GetStoredProcCommand(mStoredProcName700);

        currentDatabase.AddInParameter(mDbCommand700, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand700, "@vBoilerID", DbType.String, BoilerID);

        mDSet700 = currentDatabase.ExecuteDataSet(mDbCommand700);

        for (int i = 0; i < mDSet700.Tables[1].Rows.Count; i++)
        {
            string Value = mDSet700.Tables[1].Rows[i]["SectionID"].ToString();
            SuperHeaterID1[i] = mDSet700.Tables[1].Rows[i]["SectionID"].ToString();
        }

        //SectionID Reheater


        mDSet200 = currentDatabase.ExecuteDataSet(mDbCommand700);


        for (int i = 0; i < mDSet200.Tables[2].Rows.Count; i++)
        {
            string Value = mDSet200.Tables[2].Rows[i]["SectionID"].ToString();
            ReheaterID1[i] = mDSet200.Tables[2].Rows[i]["SectionID"].ToString();
        }

        //SectionID Economizer

        mDSet300 = currentDatabase.ExecuteDataSet(mDbCommand700);


        for (int i = 0; i < mDSet300.Tables[0].Rows.Count; i++)
        {
            string Value = mDSet300.Tables[0].Rows[i]["SectionID"].ToString();
            EconomizerID1[i] = mDSet300.Tables[0].Rows[i]["SectionID"].ToString();
        }

        mCal_HeatAbsorption_BLL.Insert_HeatAbsorption_Calculation_Parameters(
            ProjectID,
            BoilerID,
            null,   // ✅ no SectionID
            null,   // ✅ no element type
            mCal_HeatAbsorption_SC.Boiler_Efficiency,
            BoilerLoad,
            ObjectiveID,
            "Boiler Efficiency"
            );




        for (int i = 0; i < mCal_HeatAbsorption_SC.Number_of_Economizer_HeatingSections; i++)
        {
            mCal_HeatAbsorption_BLL.Insert_HeatAbsorption_Calculation_Parameters(ProjectID, BoilerID, EconomizerID1[i], "Economizer", mCal_HeatAbsorption_SC.Economizer_bundle, BoilerLoad, ObjectiveID, Economizer[i]);

        }

        //for (int i = 0; i < mCal_HeatAbsorption_SC.Number_of_RH_heating_sections; i++)
        //{
        //    mCal_HeatAbsorption_BLL.Insert_HeatAbsorption_Calculation_Parameters(ProjectID, BoilerID, ReheaterID1[i], "Reheater Elements", Reheater_Value[i], BoilerLoad, ObjectiveID, Reheater[i]);

        //}
        for (int i = 0; i < mCal_HeatAbsorption_SC.Number_of_RH_heating_sections; i++)
        {
            mCal_HeatAbsorption_BLL.Insert_HeatAbsorption_Calculation_Parameters(
                ProjectID,
                BoilerID,
                ReheaterID1[i],
                "Reheater Elements",
                (i < Reheater_Value.Count ? Reheater_Value[i] : 0),  // ✅ FIX
                BoilerLoad,
                ObjectiveID,
                Reheater[i]
            );
        }



        //for (int i = 0; i < mCal_HeatAbsorption_SC.Number_of_SH_heating_sections; i++)
        //{
        //    mCal_HeatAbsorption_BLL.Insert_HeatAbsorption_Calculation_Parameters(ProjectID, BoilerID, SuperHeaterID1[i], "Superheater Elements", SuperHeater_Value[i], BoilerLoad, ObjectiveID, Superheater[i]);

        //}
        for (int i = 0; i < mCal_HeatAbsorption_SC.Number_of_SH_heating_sections; i++)
        {

            System.Diagnostics.Debug.WriteLine(
                $"Insert: SectionID={SuperHeaterID1[i]} | Value={(i < SuperHeater_Value.Count ? SuperHeater_Value[i] : 0)}"
            );

            mCal_HeatAbsorption_BLL.Insert_HeatAbsorption_Calculation_Parameters(
                ProjectID,
                BoilerID,
                SuperHeaterID1[i],
                "Superheater Elements",
                (i < SuperHeater_Value.Count ? SuperHeater_Value[i] : 0),  // ✅ FIX
                BoilerLoad,
                ObjectiveID,
                Superheater[i]
            );


        }

        mCal_HeatAbsorption_BLL.Insert_HeatAbsorption_Calculation_Parameters(ProjectID, BoilerID, "NULL", "NULL", mCal_HeatAbsorption_SC.Total_furnace_absorption_Design, BoilerLoad, ObjectiveID, "Total furnace absorption-Design");




    }





}















