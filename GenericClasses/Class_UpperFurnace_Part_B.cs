using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using BoilerModellingTool.SC;
using BoilerModellingTool.BLL;

//Changes done by Preeti on 04/12/2020
public class Class_UpperFurnace_Part_B
{
	
    public void Calculation_Class_4B_UpperFurnace(string Project_ID,string Boiler_ID,string Boiler_Load,int Objective_ID,string SectionID,int iteration,string PreviousElement,int Location)
    {
        //string Project_ID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
        //string Boiler_ID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
        //string Section_ID = "Upper-Furnace-B-part";
        //int Objective_ID = 1;
        //string Boiler_Load = "100%TMCR";
        //int iteration = 1;
        //string Prev_Section_ID = null;


        DataSet DS = new DataSet();
        DataTable Section_IDS = new DataTable();
        int Count_Element =0;
        Class_Upper_Furnace_Part_B_BLL mClass_Upper_Furnace= new Class_Upper_Furnace_Part_B_BLL();

        //taking All the element in Upper Furnace 
        //DS = mClass_Upper_Furnace.Get_submodule_heating_section_in_upper_furnace(Boiler_ID, Project_ID);

        //Section_IDS = DS.Tables[1];
        //Count Section Id in Upper furnace
        
        //Count_Element=Convert.ToInt16(DS.Tables[0].Rows[0][0].ToString());

       //list of Section Id in Upper furnace

        //List<string> Section_List = Section_IDS.AsEnumerable()
         //                  .Select(r=> r.Field<string>("SectionID"))
         //                  .ToList();

        

        //for(int i=0;i<Count_Element-1;i++)
        //{
            //Calculation for particular Sec ID except last element
            //string Curr_Section_ID = Section_List.ElementAt(i);

            string Curr_Section_ID = SectionID;

            Class_Upper_Furnace_Part_B_BLL mClass_Upper_Furnace_Part_B_BLL = new Class_Upper_Furnace_Part_B_BLL();

            string Prev_Sec_ID = mClass_Upper_Furnace_Part_B_BLL.Get_PreVious_HE_SectionID(Boiler_ID, Project_ID, Curr_Section_ID);

            DataSet Input = new DataSet();

            Input = mClass_Upper_Furnace_Part_B_BLL.Get_Input_For_Upper_Furnace_Part_B(Boiler_ID, Project_ID, Boiler_Load, Curr_Section_ID);

            //Input
            Stream_Macros stream_Macros_BLL = new Stream_Macros();

            //Calculating Area --instead of creating new Upper furnace A
            DataSet Dt = mClass_Upper_Furnace_Part_B_BLL.Get_Design_Area_From_Section_ID_in_4B(Boiler_ID, Project_ID, Curr_Section_ID);
        //System.Diagnostics.Debug.WriteLine("===== FULL VALUE CHECK (DESIGN AREA) =====");

        //if (Dt == null)
        //{
        //    System.Diagnostics.Debug.WriteLine("❌ Dt is NULL");
        //}
        //else if (Dt.Tables.Count == 0)
        //{
        //    System.Diagnostics.Debug.WriteLine("❌ No tables in Dt");
        //}
        //else if (Dt.Tables[0].Rows.Count == 0)
        //{
        //    System.Diagnostics.Debug.WriteLine("❌ No rows in Dt.Tables[0]");
        //}
        //else
        //{
        //    var row = Dt.Tables[0].Rows[0];

        //    System.Diagnostics.Debug.WriteLine("✅ Columns count: " + Dt.Tables[0].Columns.Count);

        //    // ✅ Show all columns and values
        //    for (int c = 0; c < Dt.Tables[0].Columns.Count; c++)
        //    {
        //        string colName = Dt.Tables[0].Columns[c].ColumnName;
        //        string value = row[c] == null ? "NULL" : row[c].ToString();

        //        System.Diagnostics.Debug.WriteLine(
        //            "Column[" + c + "] = " + colName +
        //            "  | VALUE = [" + value + "]");
        //    }

        //    // ✅ Show what your code is currently using
        //    System.Diagnostics.Debug.WriteLine("❌ CURRENTLY ACCESSING: Column[0] VALUE = [" + row[0] + "]");

        //    // ✅ Try to identify correct numeric value
        //    double testVal;
        //    for (int c = 0; c < Dt.Tables[0].Columns.Count; c++)
        //    {
        //        string val = row[c].ToString();

        //        if (double.TryParse(val, out testVal))
        //        {
        //            System.Diagnostics.Debug.WriteLine("✅ NUMERIC VALUE FOUND at Column[" + c + "] = " + val);
        //        }
        //        else
        //        {
        //            System.Diagnostics.Debug.WriteLine("❌ NOT NUMERIC Column[" + c + "] = " + val);
        //        }
        //    }
        //}

        //System.Diagnostics.Debug.WriteLine("===== FULL VALUE CHECK (DESIGN AREA) =====");

        //double finalValue = 0;   // ✅ final usable value

        //if (Dt == null)
        //{
        //    System.Diagnostics.Debug.WriteLine("❌ Dt is NULL");
        //}
        //else if (Dt.Tables.Count == 0)
        //{
        //    System.Diagnostics.Debug.WriteLine("❌ No tables in Dt");
        //}
        //else if (Dt.Tables[0].Rows.Count == 0)
        //{
        //    System.Diagnostics.Debug.WriteLine("❌ No rows in Dt.Tables[0]");
        //}
        //else
        //{
        //    var table = Dt.Tables[0];
        //    var row = table.Rows[0];

        //    System.Diagnostics.Debug.WriteLine("✅ Columns count: " + table.Columns.Count);

        //    // ✅ Show all columns and values
        //    for (int c = 0; c < table.Columns.Count; c++)
        //    {
        //        string colName = table.Columns[c].ColumnName;
        //        string value = row[c] == null ? "NULL" : row[c].ToString();

        //        System.Diagnostics.Debug.WriteLine(
        //            "Column[" + c + "] = " + colName +
        //            "  | VALUE = [" + value + "]");
        //    }

        //    // ✅ TRY TO FIND ACTUAL NUMERIC VALUE
        //    double testVal;
        //    bool found = false;

        //    for (int c = 0; c < table.Columns.Count; c++)
        //    {
        //        string val = Convert.ToString(row[c]);

        //        if (double.TryParse(val, out testVal))
        //        {
        //            finalValue = testVal;
        //            found = true;

        //            System.Diagnostics.Debug.WriteLine(
        //                "✅ SELECTED NUMERIC VALUE → Column[" + c + "] = " + finalValue
        //            );

        //            break; // ✅ stop once found
        //        }
        //        else
        //        {
        //            System.Diagnostics.Debug.WriteLine("❌ NOT NUMERIC Column[" + c + "] = " + val);
        //        }
        //    }

        //    if (!found)
        //    {
        //        System.Diagnostics.Debug.WriteLine("❌ NO NUMERIC VALUE FOUND → DEFAULT 0");
        //        finalValue = 0;
        //    }

        //    // ✅ Final value being used
        //    System.Diagnostics.Debug.WriteLine("✅ FINAL VALUE USED = " + finalValue);
        //}
        System.Diagnostics.Debug.WriteLine("===== FULL VALUE CHECK (DESIGN AREA) =====");

        string nextSectionID = "";
        double finalValue = 0;

        // ✅ SAFETY CHECKS
        if (Dt == null)
        {
            System.Diagnostics.Debug.WriteLine("❌ Dt is NULL");
        }
        else if (Dt.Tables.Count == 0)
        {
            System.Diagnostics.Debug.WriteLine("❌ No tables in Dt");
        }
        else
        {
            // ✅ ===== READ SECTIONID (TABLE[0]) =====
            if (Dt.Tables.Count > 0 && Dt.Tables[0].Rows.Count > 0)
            {
                nextSectionID = Dt.Tables[0].Rows[0][0].ToString();

                System.Diagnostics.Debug.WriteLine("✅ SectionID (Table[0]) = " + nextSectionID);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("❌ No SectionID found in Table[0]");
            }

            // ✅ ===== READ VALUE (TABLE[1]) ===== ⭐ IMPORTANT FIX
            if (Dt.Tables.Count > 1 && Dt.Tables[1].Rows.Count > 0)
            {
                string rawVal = Dt.Tables[1].Rows[0][0].ToString();

                System.Diagnostics.Debug.WriteLine("Raw Value from Table[1] = " + rawVal);

                if (!double.TryParse(rawVal, out finalValue))
                {
                    System.Diagnostics.Debug.WriteLine("❌ Failed to parse numeric value, defaulting to 0");
                    finalValue = 0;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("✅ Parsed Value (Table[1]) = " + finalValue);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("❌ No Value found in Table[1]");
            }
        }

        // ✅ FINAL OUTPUT
        System.Diagnostics.Debug.WriteLine("✅ FINAL VALUE USED = " + finalValue);

        // ✅ ASSIGN TO YOUR VARIABLE
        double HS_total_heating_area = finalValue;



        //D8  Design area of Current heating Section
        //Double HS_total_heating_area = Convert.ToDouble(Dt.Tables[0].Rows[0][0].ToString());
        // Double HS_total_heating_area = Dt.Tables.Cast<DataTable>().SelectMany(t => t.AsEnumerable()).SelectMany(r => r.ItemArray).Where(v => double.TryParse(v?.ToString(), out _)).Select(v => Convert.ToDouble(v)).FirstOrDefault();


        //D12
        Double Radiation_heat_from_flue_gas_in_upper_furnace_zone = 0;
            if (Location == 0)
            {
                Radiation_heat_from_flue_gas_in_upper_furnace_zone = Convert.ToDouble(Input.Tables[3].Rows[0]["Value"].ToString());
            }

            else
            {
                Radiation_heat_from_flue_gas_in_upper_furnace_zone = Convert.ToDouble(Input.Tables[4].Rows[0]["Value"].ToString());
            }
       
            DataSet S_Para_Dataset = new DataSet();
        //BB
        //S_Para_Dataset = mClass_Upper_Furnace_Part_B_BLL.Get_S_Parameter_Section_ID(Boiler_ID, Project_ID, Boiler_Load, Curr_Section_ID);

          S_Para_Dataset = mClass_Upper_Furnace_Part_B_BLL.Get_S_Parameter_Section_ID(Boiler_ID, Project_ID, Boiler_Load, Objective_ID, Curr_Section_ID, iteration);

        //D16
            Double Inlet_temperature = Convert.ToDouble(S_Para_Dataset.Tables[0].Rows[0]["Inlet_temperature"].ToString());
           //D17
            Double Inlet_pressure=Convert.ToDouble(S_Para_Dataset.Tables[1].Rows[0]["Inlet_Pressure"].ToString());
            //D18
            Double HS_outlet_pressure = Convert.ToDouble(S_Para_Dataset.Tables[2].Rows[0]["Outlet_Pressure"].ToString());

       
            //D19
            Double Heating_section_steam_flow_rate1 = Convert.ToDouble(Input.Tables[0].Rows[0]["Value"].ToString());
        Double Heating_section_steam_flow_rate = Heating_section_steam_flow_rate1 * 1000;
            //D20
            Double De_superheating_spray_ = Convert.ToDouble(Input.Tables[3].Rows[0]["Value"].ToString());
        //Double De_superheating_spray_ = Convert.ToDouble(Input.Tables[1].Rows[0]["Value"].ToString());
        //D21
        Double De_superheating_spray_enthalpy=Convert.ToDouble(Input.Tables[2].Rows[0]["Value"].ToString());



            //Calculations
            //F25=D12
            Double	Total_heat_absorbed_by__HS=Radiation_heat_from_flue_gas_in_upper_furnace_zone;
            //F26=((D17)*0.980665+1.01325)/10
            Double	Inlet_steam_pressure_of_HS=((Inlet_pressure)*0.980665+1.01325)/10;
            //F27=((D18)*0.980665+1.01325)/10
            Double	Outlet_steam_pressure_of_HS=((HS_outlet_pressure)*0.980665+1.01325)/10;
            //F28=h_pt(D17*0.980665+1.01325,D16+0.01)
            Double	Upstream_heating_section_steam_enthalpy=stream_Macros_BLL.h_pT(Inlet_pressure*0.980665+1.01325,Inlet_temperature+0.01);
            //F29=(F28*(D19-D20)+D21*D20)/D19
            Double	Heating_section_Inlet_steam_enthalpy=(Upstream_heating_section_steam_enthalpy*(Heating_section_steam_flow_rate-De_superheating_spray_)+De_superheating_spray_enthalpy*De_superheating_spray_)/Heating_section_steam_flow_rate;
            //F30=T_ph(10*F26,F29)
            Double	Inlet_steam_temperature=stream_Macros_BLL.T_ph(10*Inlet_steam_pressure_of_HS,Heating_section_Inlet_steam_enthalpy);
            //F31=F29+F25*1000/(D19/3600)
            Double	Outlet_steam_enthalpy=Heating_section_Inlet_steam_enthalpy+Total_heat_absorbed_by__HS*1000/(Heating_section_steam_flow_rate/3600);
            //F32=T_ph(10*F27,F31)
            Double	Outlet_steam_temperature=stream_Macros_BLL.T_ph(10*Outlet_steam_pressure_of_HS,Outlet_steam_enthalpy);


            //=F25      
            Double Heat_absorption_in_heating_section = Total_heat_absorbed_by__HS;
            //=(F138+F70)*D40/1000
            Double Roof = 0;
            //=F71*D40/1000
            Double Water_wall = 0;

            if (Location == 0)
                {
                     Class_Upper_Furnace_Part_B_BLL Heat_absorbed = new Class_Upper_Furnace_Part_B_BLL();

                    Heat_absorbed.Insert_4B_Calculation(13,Boiler_ID,Project_ID,Boiler_Load,Objective_ID,Heat_absorption_in_heating_section.ToString());
                }
                else
                {
                     Class_Upper_Furnace_Part_B_BLL Heat_absorbed = new Class_Upper_Furnace_Part_B_BLL();

                    Heat_absorbed.Insert_4B_Calculation(14,Boiler_ID,Project_ID,Boiler_Load,Objective_ID,Heat_absorption_in_heating_section.ToString());


                }

                mClass_Upper_Furnace_Part_B_BLL.InsertUpadate_5B_S_ParameterValue(Project_ID, Boiler_ID, Boiler_Load, Objective_ID, Curr_Section_ID, Inlet_temperature.ToString(), Outlet_steam_temperature.ToString(), iteration, PreviousElement);

        

    }

}