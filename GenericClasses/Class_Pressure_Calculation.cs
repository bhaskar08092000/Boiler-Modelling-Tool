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
/// <summary>
/// Summary description for Class_Pressure_Calculation
/// </summary>
public class Class_Pressure_Calculation
{


    #region "Variables"

    private Database currentDatabase;

    #endregion

    #region "Constructor"

    public Class_Pressure_Calculation()
    {
        currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
    }

    #endregion

    String mStoredProcName = String.Empty;

    String mStoredProcName1 = String.Empty;

    String mStoredProcName2 = String.Empty;

    String mStoredProcName7 = String.Empty;

    String mStoredProcName8 = String.Empty;

    DbCommand mDbCommand = null;

    DbCommand mDbCommand1 = null;

    DbCommand mDbCommand2 = null;

    DbCommand mDbCommand7 = null;

    DbCommand mDbCommand8 = null;

    Cal_Pressure_SC mCal_Pressure_SC;

    Cal_Pressure_BLL mCal_Pressure_BLL;

    DataSet mDSet;

    DataSet mDSet1;

    DataSet mDSet2;

    DataSet mDSet7;

    DataSet mDSet8;


    string[] CrossDuctID, SuperHeaterID, ReheaterID, UpperFurnaceID,SteamScreenID;

    double[] InletPressureforCrossDuct;

    double[] OutletPressureforCrossDuct;

    double[] InletPressureForSuperheater;

    double[] OutletPressureForSuperheater;

    double[] InletPressureForReheater;

    double[] OutletPressureForReheater;

    double[] InletPressureForUpperFurnace;

    double[] OutletPressureForUpperFurnace;


    public void Calculation_For_PressureModule(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID)
    {
        mCal_Pressure_BLL = new Cal_Pressure_BLL();

        mCal_Pressure_SC = new Cal_Pressure_SC();



        InletPressureforCrossDuct = new double[10];

        OutletPressureforCrossDuct = new double[10];

        InletPressureForSuperheater = new double[10];

        OutletPressureForSuperheater = new double[10];

        InletPressureForReheater = new double[10];

        OutletPressureForReheater = new double[10];

        InletPressureForUpperFurnace = new double[10];

        OutletPressureForUpperFurnace = new double[10];


        CrossDuctID = new string[50];

        SuperHeaterID = new string[50];

        ReheaterID = new string[50];

        UpperFurnaceID = new string[50];

        SteamScreenID = new string[50];

        //SectionOfCD = new double[CountCD];

        //SectionIDOfCrossDuct
        mStoredProcName = StoredProcedure.spr_Get_SectionIDOf_CrossDuct;
        mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

        currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);

        mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

        for (int i = 0; i < mDSet.Tables[0].Rows.Count; i++)
        {
            string Value = mDSet.Tables[0].Rows[i]["SectionID"].ToString();
            CrossDuctID[i] = mDSet.Tables[0].Rows[i]["SectionID"].ToString();
        }


        //SectionIDOf Superheater

        mStoredProcName1 = StoredProcedure.spr_S_parameter_HeatinElement_Sequence;
        mDbCommand1 = currentDatabase.GetStoredProcCommand(mStoredProcName1);

        currentDatabase.AddInParameter(mDbCommand1, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand1, "@vBoilerID", DbType.String, BoilerID);

        mDSet1 = currentDatabase.ExecuteDataSet(mDbCommand1);

        for (int i = 0; i < mDSet1.Tables[1].Rows.Count; i++)
        {
            string Value = mDSet1.Tables[1].Rows[i]["SectionID"].ToString();
            SuperHeaterID[i] = mDSet1.Tables[1].Rows[i]["SectionID"].ToString();
        }


        //SectionID Reheater

        mDSet2 = currentDatabase.ExecuteDataSet(mDbCommand1);


        for (int i = 0; i < mDSet2.Tables[2].Rows.Count; i++)
        {
            string Value = mDSet2.Tables[2].Rows[i]["SectionID"].ToString();
            ReheaterID[i] = mDSet2.Tables[2].Rows[i]["SectionID"].ToString();
        }

        //SectionIDOfLastHeatingElementOfUpperFunace

        mStoredProcName7 = StoredProcedure.spr_GetLast_Element_of_HeatingSectionUpperFurnace;
        mDbCommand7 = currentDatabase.GetStoredProcCommand(mStoredProcName7);

        currentDatabase.AddInParameter(mDbCommand7, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand7, "@vBoilerID", DbType.String, BoilerID);

        mDSet7 = currentDatabase.ExecuteDataSet(mDbCommand7);

        for (int i = 0; i < mDSet7.Tables[0].Rows.Count; i++)
        {
            string Value = mDSet7.Tables[0].Rows[i]["SectionID"].ToString();
            UpperFurnaceID[i] = mDSet7.Tables[0].Rows[i]["SectionID"].ToString();
        }


        //SteamScreen
        mStoredProcName8 = StoredProcedure.Get_CountOfSteamScreenSection;
        mDbCommand8 = currentDatabase.GetStoredProcCommand(mStoredProcName8);

        currentDatabase.AddInParameter(mDbCommand8, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand8, "@vBoilerID", DbType.String, BoilerID);

        mDSet8 = currentDatabase.ExecuteDataSet(mDbCommand8);

        for (int i = 0; i < mDSet8.Tables[0].Rows.Count; i++)
        {
            string Value = mDSet8.Tables[0].Rows[i]["SectionID"].ToString();
            SteamScreenID[i] = mDSet8.Tables[0].Rows[i]["SectionID"].ToString();
        }



        //Calculations

        DataSet mdataset = null;
        DataTable mDataTable = null;
        mdataset = new DataSet();



        DataSet mdataset1 = null;
        DataTable mDataTable1 = null;
        mdataset1 = new DataSet();

        DataSet mdataset2 = null;
        DataTable mDataTable2 = null;
        mdataset2 = new DataSet();



        mdataset = mCal_Pressure_BLL.GetInput_For_Cal_Pressure_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID);

        mdataset1 = mCal_Pressure_BLL.GetInput_For_TotalHeatingSections(BoilerID, ProjectID);

        mdataset2 = mCal_Pressure_BLL.GetInput_For_TotalHeatingSectionsOfCrossDuct(BoilerID, ProjectID);
        


        mCal_Pressure_SC.Feed_water_pressure_at_eco_Inlet = Convert.ToDouble(mdataset.Tables[0].Rows[0]["Value"].ToString());

        mCal_Pressure_SC.Drum_separator_outlet__pressure = Convert.ToDouble(mdataset.Tables[1].Rows[0]["Value"].ToString());

        mCal_Pressure_SC.CRH_inlet_pressure = Convert.ToDouble(mdataset.Tables[2].Rows[0]["Value"].ToString());

        mCal_Pressure_SC.CRH_outlet_pressure = Convert.ToDouble(mdataset.Tables[3].Rows[0]["Value"].ToString());

        mCal_Pressure_SC.SH_outlet_pressure = Convert.ToDouble(mdataset.Tables[7].Rows[0]["Value"].ToString());



        mCal_Pressure_SC.Number_of_RH_heating_sections = Convert.ToDouble(mdataset1.Tables[2].Rows[0]["CountRH"].ToString());

        mCal_Pressure_SC.Number_of_SH_heating_sections = Convert.ToDouble(mdataset1.Tables[1].Rows[0]["CountSH"].ToString());

        mCal_Pressure_SC.Number_of_heating_sections_in_cross_duct = Convert.ToDouble(mdataset2.Tables[0].Rows[0]["CountCrossDuct"].ToString());

        // Calculations

        mCal_Pressure_SC.Economzier_bundle_inlet_pressure = mCal_Pressure_SC.Feed_water_pressure_at_eco_Inlet;

        mCal_Pressure_SC.Economizer_bundle_outlet_pressure = mCal_Pressure_SC.Feed_water_pressure_at_eco_Inlet - (mCal_Pressure_SC.Economzier_bundle_inlet_pressure - mCal_Pressure_SC.Drum_separator_outlet__pressure) * 0.5;

        mCal_Pressure_SC.Economzer_hanger_inlet_pressure = mCal_Pressure_SC.Economizer_bundle_outlet_pressure;

        mCal_Pressure_SC.Economzer_hanger_outlet_pressure = mCal_Pressure_SC.Economzer_hanger_inlet_pressure - (mCal_Pressure_SC.Economzier_bundle_inlet_pressure - mCal_Pressure_SC.Drum_separator_outlet__pressure) * 0.3;

        mCal_Pressure_SC.Furnace_roof_total_pressure_drop = 2;

        mCal_Pressure_SC.Furnace_roof_pressure_drop_per_HS = mCal_Pressure_SC.Furnace_roof_total_pressure_drop / (mCal_Pressure_SC.Number_of_heating_sections_in_cross_duct + 1);

        mCal_Pressure_SC.Inlet_pressure_for_5B = mCal_Pressure_SC.Drum_separator_outlet__pressure;

        mCal_Pressure_SC.Outlet_pressure_for_5B = mCal_Pressure_SC.Inlet_pressure_for_5B - mCal_Pressure_SC.Furnace_roof_pressure_drop_per_HS;

        mCal_Pressure_SC.Inlet_pressure_for_6B = mCal_Pressure_SC.Outlet_pressure_for_5B;

        mCal_Pressure_SC.Outlet_pressure_for_6B = mCal_Pressure_SC.Inlet_pressure_for_6B - mCal_Pressure_SC.Furnace_roof_pressure_drop_per_HS;



        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Feed_water_pressure_at_eco_Inlet, BoilerLoad, ObjectiveID, "Feed water pressure at eco. Inlet");

        //mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Drum_separator_outlet__pressure, BoilerLoad, ObjectiveID, "Drum / separator outlet  pressure ");

        //mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.SH_outlet_pressure, BoilerLoad, ObjectiveID, "SH outlet pressure");

        //mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.CRH_inlet_pressure, BoilerLoad, ObjectiveID, "CRH Inlet pressure");

        //mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.CRH_outlet_pressure, BoilerLoad, ObjectiveID, "CRH Outlet pressure");

        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Economzier_bundle_inlet_pressure, BoilerLoad, ObjectiveID, "Economizer Bundle Inlet Pressure");

        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Economizer_bundle_outlet_pressure, BoilerLoad, ObjectiveID, "Economizer Bundle Outlet Pressure");

        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Economzer_hanger_inlet_pressure, BoilerLoad, ObjectiveID, "Economizer Hanger Inlet Pressure");

        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Economzer_hanger_outlet_pressure, BoilerLoad, ObjectiveID, "Economizer Hanger Outlet Pressure");

        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Furnace_roof_total_pressure_drop, BoilerLoad, ObjectiveID, "Furnace Roof Total Pressure Drop ");

        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Furnace_roof_pressure_drop_per_HS, BoilerLoad, ObjectiveID, "Furnace Roof Pressure Drop per HS");






        //Cross Duct



        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters(ProjectID, BoilerID, UpperFurnaceID[0], "Upper Furnace", mCal_Pressure_SC.Inlet_pressure_for_5B, BoilerLoad, ObjectiveID, "Inlet Pressure");
        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters(ProjectID, BoilerID, UpperFurnaceID[0], "Upper Furnace", mCal_Pressure_SC.Outlet_pressure_for_5B, BoilerLoad, ObjectiveID, "Outlet Pressure");




        DataSet mDSet10 = null;

        String mStoredProcName10 = String.Empty;
        DbCommand mDbCommand10 = null;
        mDSet10 = new DataSet();
        mStoredProcName10 = StoredProcedure.Get_OutputForFurnaceRoofCoolingMedium;
        mDbCommand10 = currentDatabase.GetStoredProcCommand(mStoredProcName10);
        currentDatabase.AddInParameter(mDbCommand10, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand10, "@vBoilerID", DbType.String, BoilerID);
        mDSet10 = currentDatabase.ExecuteDataSet(mDbCommand10);


        int a = Convert.ToInt16(mDSet10.Tables[0].Rows[0]["FurnaceRoofCoolingMedium"]);

        for (int i = 0; i < mCal_Pressure_SC.Number_of_heating_sections_in_cross_duct; i++)


            if (a == 1)
            {

                InletPressureforCrossDuct[i] = mCal_Pressure_SC.Drum_separator_outlet__pressure;

                OutletPressureforCrossDuct[i] = mCal_Pressure_SC.Drum_separator_outlet__pressure;

            }

            else
            {

                if (i == 0)
                {
                    InletPressureforCrossDuct[i] = mCal_Pressure_SC.Inlet_pressure_for_6B;

                    OutletPressureforCrossDuct[i] = InletPressureforCrossDuct[i] - mCal_Pressure_SC.Furnace_roof_pressure_drop_per_HS;
                }

                else
                {
                    InletPressureforCrossDuct[i] = OutletPressureforCrossDuct[i - 1];

                    OutletPressureforCrossDuct[i] = InletPressureforCrossDuct[i] - mCal_Pressure_SC.Furnace_roof_pressure_drop_per_HS;
                }

            }




        for (int i = 0; i < mCal_Pressure_SC.Number_of_heating_sections_in_cross_duct; i++)
        {
            mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters(ProjectID, BoilerID, CrossDuctID[i], "CrossDuct", InletPressureforCrossDuct[i], BoilerLoad, ObjectiveID, "Inlet Pressure");
            mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters(ProjectID, BoilerID, CrossDuctID[i], "CrossDuct", OutletPressureforCrossDuct[i], BoilerLoad, ObjectiveID, "Outlet Pressure");
        }




        //Reverse Chamber

        mCal_Pressure_SC.Reverse_chamber_inlet = OutletPressureforCrossDuct[Convert.ToInt16(mCal_Pressure_SC.Number_of_heating_sections_in_cross_duct - 1)];


        mCal_Pressure_SC.Reverse_chamber_outlet = mCal_Pressure_SC.Reverse_chamber_inlet - mCal_Pressure_SC.Furnace_roof_pressure_drop_per_HS;


        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Reverse_chamber_inlet, BoilerLoad, ObjectiveID, "Reverse Chamber Inlet");

        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Reverse_chamber_outlet, BoilerLoad, ObjectiveID, "Reverse Chamber Outlet");



        //Steam Screen

        mCal_Pressure_SC.Steam_screen_inlet = mCal_Pressure_SC.Reverse_chamber_outlet;

        mCal_Pressure_SC.Steam_screen_outlet = mCal_Pressure_SC.Steam_screen_inlet - mCal_Pressure_SC.Furnace_roof_pressure_drop_per_HS;

        //mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Steam_screen_inlet, BoilerLoad, ObjectiveID, "Steam Screen Inlet");

        //mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Steam_screen_outlet, BoilerLoad, ObjectiveID, "Steam Screen Outlet");

        for (int i = 0; i < mDSet8.Tables[0].Rows.Count; i++)
        {
            mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters(ProjectID, BoilerID, SteamScreenID[i], "Steam Screen Sections", mCal_Pressure_SC.Steam_screen_inlet, BoilerLoad, ObjectiveID, "Inlet Pressure");
            mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters(ProjectID, BoilerID, SteamScreenID[i], "Steam Screen Sections", mCal_Pressure_SC.Steam_screen_outlet, BoilerLoad, ObjectiveID, "Outlet Pressure");
        }


        //Side Wall

        mCal_Pressure_SC.Extended_side_wall_inlet = mCal_Pressure_SC.Reverse_chamber_outlet;

        mCal_Pressure_SC.Extended_side_wall_outlet = mCal_Pressure_SC.Extended_side_wall_inlet - mCal_Pressure_SC.Furnace_roof_pressure_drop_per_HS;

        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Extended_side_wall_inlet, BoilerLoad, ObjectiveID, "Extended Side Wall Inlet");

        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Extended_side_wall_outlet, BoilerLoad, ObjectiveID, "Extended Side Wall Outlet");



        //Superheater Sections

        mCal_Pressure_SC.SH_section_inlet_pressure = mCal_Pressure_SC.Extended_side_wall_inlet;

        mCal_Pressure_SC.Pressure_drop_per_SH_section = (mCal_Pressure_SC.SH_section_inlet_pressure - mCal_Pressure_SC.SH_outlet_pressure) / mCal_Pressure_SC.Number_of_SH_heating_sections;

        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.SH_section_inlet_pressure, BoilerLoad, ObjectiveID, "SH section inlet pressure");

        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Pressure_drop_per_SH_section, BoilerLoad, ObjectiveID, "Pressure drop per SH section");


        DataSet mDSet11 = null;

        mDSet11 = new DataSet();
        mDSet11 = currentDatabase.ExecuteDataSet(mDbCommand10);


        int b = Convert.ToInt16(mDSet11.Tables[0].Rows[0]["FurnaceRoofCoolingMedium"]);


        for (int i = 0; i < mCal_Pressure_SC.Number_of_SH_heating_sections; i++)
        {


            if (b == 1)
            {
                {
                    if (i == 0)
                    {
                        InletPressureForSuperheater[i] = mCal_Pressure_SC.Drum_separator_outlet__pressure;

                        OutletPressureForSuperheater[i] = InletPressureForSuperheater[i] - mCal_Pressure_SC.Pressure_drop_per_SH_section;
                    }
                    else
                    {
                        InletPressureForSuperheater[i] = OutletPressureForSuperheater[i - 1];

                        OutletPressureForSuperheater[i] = InletPressureForSuperheater[i] - mCal_Pressure_SC.Pressure_drop_per_SH_section;

                    }

                }

            }

            else
            {
                if (i == 0)
                {
                    InletPressureForSuperheater[i] = mCal_Pressure_SC.SH_section_inlet_pressure;

                    OutletPressureForSuperheater[i] = InletPressureForSuperheater[i] - mCal_Pressure_SC.Pressure_drop_per_SH_section;
                }
                else
                {
                    InletPressureForSuperheater[i] = OutletPressureForSuperheater[i - 1];

                    OutletPressureForSuperheater[i] = InletPressureForSuperheater[i] - mCal_Pressure_SC.Pressure_drop_per_SH_section;

                }

            }
        }




        for (int i = 0; i < mCal_Pressure_SC.Number_of_SH_heating_sections; i++)
        {
            mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters(ProjectID, BoilerID, SuperHeaterID[i], "Superheater Elements", InletPressureForSuperheater[i], BoilerLoad, ObjectiveID, "Inlet Pressure");
            mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters(ProjectID, BoilerID, SuperHeaterID[i], "Superheater Elements", OutletPressureForSuperheater[i], BoilerLoad, ObjectiveID, "Outlet Pressure");
        }






        // Reheater Sections


        mCal_Pressure_SC.RH_section_inlet_pressure = mCal_Pressure_SC.CRH_inlet_pressure;
        
        mCal_Pressure_SC.Pressure_drop_per_RH_section = (mCal_Pressure_SC.RH_section_inlet_pressure - mCal_Pressure_SC.CRH_outlet_pressure) / mCal_Pressure_SC.Number_of_RH_heating_sections;


        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.RH_section_inlet_pressure, BoilerLoad, ObjectiveID, "RH section inlet pressure");

        mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters_FixedParameters(ProjectID, BoilerID, mCal_Pressure_SC.Pressure_drop_per_RH_section, BoilerLoad, ObjectiveID, "Pressure drop per RH section");


        DataSet mDSet20 = null;

        String mStoredProcName20 = String.Empty;
        DbCommand mDbCommand20 = null;
        mDSet20 = new DataSet();
        mStoredProcName20 = StoredProcedure.Get_OutputForReheaterType;
        mDbCommand20 = currentDatabase.GetStoredProcCommand(mStoredProcName20);
        currentDatabase.AddInParameter(mDbCommand20, "@vProjectID", DbType.String, ProjectID);
        currentDatabase.AddInParameter(mDbCommand20, "@vBoilerID", DbType.String, BoilerID);
        mDSet20 = currentDatabase.ExecuteDataSet(mDbCommand20);


        int z = Convert.ToInt16(mDSet20.Tables[0].Rows[0]["BoilerType"]);

        if (z == 1)
        {
            for (int i = 0; i < mCal_Pressure_SC.Number_of_RH_heating_sections; i++)
            {
                if (i == 0)
                {
                    InletPressureForReheater[i] = mCal_Pressure_SC.RH_section_inlet_pressure;

                    OutletPressureForReheater[i] = InletPressureForReheater[i] - mCal_Pressure_SC.Pressure_drop_per_RH_section;
                }
                else
                {
                    InletPressureForReheater[i] = OutletPressureForReheater[i - 1];

                    OutletPressureForReheater[i] = InletPressureForReheater[i] - mCal_Pressure_SC.Pressure_drop_per_RH_section;

                }
            }
        }






        for (int i = 0; i < mCal_Pressure_SC.Number_of_RH_heating_sections; i++)
        {
            mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters(ProjectID, BoilerID, ReheaterID[i], "Reheater Elements", InletPressureForReheater[i], BoilerLoad, ObjectiveID, "Inlet Pressure");
            mCal_Pressure_BLL.Insert_Pressure_Calculation_Parameters(ProjectID, BoilerID, ReheaterID[i], "Reheater Elements", OutletPressureForReheater[i], BoilerLoad, ObjectiveID, "Outlet Pressure");
        }
        
    }







}