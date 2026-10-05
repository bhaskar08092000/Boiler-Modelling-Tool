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
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace GenericClasses
{
    public class Class_Flow_Calculation
    {

        #region "Variables"

        private Database currentDatabase;

        #endregion

        #region "Constructor"

        public Class_Flow_Calculation()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }

        #endregion

        String mStoredProcName = String.Empty;

        DbCommand mDbCommand = null;

        Cal_Flow_SC Doc {get;set;}

        Cal_Flow_BLL mCal_Flow_BLL;

        Class_3A_Calculation mClass_3A_Calculation;

        double[] SHSpray;

        int[] SectionNumberOfSH;

        double[] SectionOfEC;

        double[] SectionOfSH;

        double[] SectionOfRH;

        double[] SectionOfWS;

        double[] SectionOfSS;

        string[] SHDesuperheatingSprayStagesID;

        string[] EconomiserID, SuperHeaterID, ReheaterID, WaterScreenID, SteamScreenID;

        DataSet mDSet;

        Stream_Macros mStream_Macros;

        public void Calculation_For_FlowModule(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,int CountEC,int CountSH,int CountRH,int CountWS,int CountSS,int DesuperHeatingSprayTapOff,int SHDesuperheatingSpray,int TypeOfBoiler)
        {
            mStream_Macros = new Stream_Macros();

            mCal_Flow_BLL = new Cal_Flow_BLL();

            mClass_3A_Calculation = new Class_3A_Calculation(ProjectID,BoilerID,BoilerLoad,ObjectiveID);

            SHSpray = new double[CountSH];

            SHDesuperheatingSprayStagesID = new string[SHDesuperheatingSpray];

            SectionNumberOfSH = new int[CountSH];

            SectionOfEC = new double[CountEC];

            SectionOfSH = new double[CountSH];

            SectionOfRH = new double[CountRH];

            SectionOfWS = new double[CountWS];

            SectionOfSS = new double[CountSS];

            EconomiserID = new string[CountEC];

            SuperHeaterID = new string[CountSH];

            ReheaterID = new string[CountRH];

            WaterScreenID = new string[CountWS];

            SteamScreenID=new string[CountSS];

            mDSet = new DataSet();

            //Input for Flow Module
            Doc = mCal_Flow_BLL.Get_Flow_CalculationInput(ProjectID, BoilerID, BoilerLoad, ObjectiveID, TypeOfBoiler, DesuperHeatingSprayTapOff);

            //SectionID
            mStoredProcName = StoredProcedure.spr_Flow_GetSectionID_Of_HeatingElement;
            mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);

            mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

            for (int i = 0; i < CountEC;i++)
            {
                EconomiserID[i] = mDSet.Tables[0].Rows[i]["SectionID"].ToString();
            }

            for (int i = 0; i < CountSH;i++)
            {
                SuperHeaterID[i] = mDSet.Tables[1].Rows[i]["SectionID"].ToString();
            }

            for (int i = 0; i < CountRH;i++)
            {
                ReheaterID[i] = mDSet.Tables[2].Rows[i]["SectionID"].ToString();
            }

            for (int i = 0; i < CountWS;i++)
            {
                WaterScreenID[i] = mDSet.Tables[3].Rows[i]["SectionID"].ToString();
            }

            for (int i = 0; i < CountSS;i++)
            {
                SteamScreenID[i] = mDSet.Tables[4].Rows[i]["SectionID"].ToString();
            }

            //Calculation For Flow Module
            if(ObjectiveID==2)
            {
                Doc.Hot_RH_flow_rate = mClass_3A_Calculation.RHFlow(ProjectID,BoilerID,BoilerLoad,ObjectiveID);
            }

            Doc.Blow_down_flow = Doc.Main_steam_flow_rate * Doc.Blow_down_percentage_if_it_is_sub_critical_boiler / 100;

            if(DesuperHeatingSprayTapOff==3)
            {
                Doc.Desuperheater_tap_off_at_economizer_outlet = Doc.SH_De_superheating_spray_stage_1 + Doc.SH_De_superheating_spray_stage_2_if_any;
            }
            else
            {
                Doc.Desuperheater_tap_off_at_economizer_outlet = 0.0;
            }

            string SectionID;
            for (int i = 0; i < SHDesuperheatingSpray;i++)
            {
                SectionID = null;
                SHDesuperheatingSprayStagesID[i]=mCal_Flow_BLL.Get_SectionID_From_MiscellaneousSH(ProjectID,BoilerID,i+1);
                SectionID = SHDesuperheatingSprayStagesID[i];
                SectionNumberOfSH[i] = mCal_Flow_BLL.Get_SectionNumber_Of_SectionID(ProjectID, BoilerID, SectionID);
            }

            //if Sh desuperheating spray stage is 1
            if (SHDesuperheatingSpray == 1)
            {
                int k = 1, l = 0;

                for (int j = 0; j < CountSH; j++)
                {
                    if (k == SectionNumberOfSH[l])
                    {
                        SHSpray[j] = Doc.SH_De_superheating_spray_stage_1;
                        l++;
                    }
                    else
                    {
                        SHSpray[j] = 0;
                    }

                    k++;
                }
            }

            //if Sh desuperheating spray stage is 2
            else if (SHDesuperheatingSpray == 2)
            {
                {
                    for (int j = 0; j < CountSH; j++)
                    {
                        System.Diagnostics.Debug.WriteLine($"--- Iteration j = {j} (Section {j + 1}) ---");

                        SHSpray[j] = 0.0;

                        // Stage 1 → ONLY exact section
                        if (SectionNumberOfSH[0] != 0 && (j + 1) == SectionNumberOfSH[0])
                        {
                            System.Diagnostics.Debug.WriteLine(
                                $"Stage 1 condition met: SectionNumberOfSH[0] = {SectionNumberOfSH[0]}, current section = {j + 1}"
                            );

                            SHSpray[j] = Doc.SH_De_superheating_spray_stage_1;

                            System.Diagnostics.Debug.WriteLine(
                                $"Applied Stage 1 spray: SHSpray[{j}] = {SHSpray[j]}"
                            );
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(
                                $"Stage 1 condition NOT met: SectionNumberOfSH[0] = {SectionNumberOfSH[0]}, current section = {j + 1}"
                            );
                        }

                        // Stage 2 (same fix retained)
                        if (SectionNumberOfSH[1] != 0 &&
                            (j + 1) == SectionNumberOfSH[1] &&
                            Doc.SH_De_superheating_spray_stage_2_if_any > 0)
                        {
                            System.Diagnostics.Debug.WriteLine(
                                $"Stage 2 condition met: SectionNumberOfSH[1] = {SectionNumberOfSH[1]}, current section = {j + 1}"
                            );

                            SHSpray[j] = Doc.SH_De_superheating_spray_stage_2_if_any;

                            System.Diagnostics.Debug.WriteLine(
                                $"Applied Stage 2 spray: SHSpray[{j}] = {SHSpray[j]}"
                            );
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(
                                $"Stage 2 condition NOT met OR skipped"
                            );
                        }

                        System.Diagnostics.Debug.WriteLine($"Final SHSpray[{j}] = {SHSpray[j]}");
                    }


                    //for (int j = 0; j < CountSH; j++)
                    //{
                    //    int i = 1;
                    //    if (i == SectionNumberOfSH[j])
                    //    {
                    //        SHSpray[j] = Doc.SH_De_superheating_spray_stage_1;

                    //            while (j < CountSH)
                    //            {
                    //                j++;
                    //                i++;
                    //                if (i == SectionNumberOfSH[j])
                    //                {
                    //                    SHSpray[j] = Doc.SH_De_superheating_spray_stage_2_if_any;
                    //                }
                    //                else
                    //                {
                    //                    SHSpray[j] = 0.0;
                    //                }
                    //            }
                    //     }
                    // }

                }
                
            }


                //Economiser Section
                for (int i=0;i<CountEC;i++)
            {
                SectionOfEC[i] = Doc.Economiser_feed_water_flow_rate;
            }

            //Reheater Section
            for(int i=0;i<CountRH;i++)
            {
                SectionOfRH[i]=Doc.Hot_RH_flow_rate;
            }

            //Water Screen Section
            for(int i=0;i<CountWS;i++)
            {
                SectionOfWS[i]=Doc.Economiser_feed_water_flow_rate-Doc.Desuperheater_tap_off_at_economizer_outlet;
            }

            //Backpass Flow ratio calculation
            Doc.Fraction_of_total_flow_in_Sidewall_front=Math.Round((Math.Pow(Doc.Sidewall_front_portion_tube_diameter/1000,2)*Doc.Sidewall_front_portion_tube_numbers),2);

            Doc.Fraction_of_total_flow_in_Sidewall_rear=Math.Round((Math.Pow(Doc.Sidewall_rear_portion_tube_diameter/1000,2)*Doc.Sidewall_rear_portion_tube_numbers),2);

            Doc.Fraction_of_side_front_wall_flow_in_backpass_front_wall = Math.Round((Math.Pow(Doc.Front_wall_tube_diameter / 1000, 2) * Doc.Front_wall_tube_numbers),2);

            Doc.Fraction_of_side_front_wall_flow_in_extended_side_wall = Math.Round((Math.Pow(Doc.Extended_steam_wall_header_tube_diameter/1000, 2) * Doc.Extended_steam_wall_header_tube_numbers),2);

            Doc.Ratio_of_total_flow_in_Sidewall_front = Math.Round((Doc.Fraction_of_total_flow_in_Sidewall_front / (Doc.Fraction_of_total_flow_in_Sidewall_front + Doc.Fraction_of_total_flow_in_Sidewall_rear)),3);

            Doc.Ratio_of_total_flow_in_Sidewall_rear = Math.Round((Doc.Fraction_of_total_flow_in_Sidewall_rear / (Doc.Fraction_of_total_flow_in_Sidewall_front + Doc.Fraction_of_total_flow_in_Sidewall_rear)),2);

            Doc.Ratio_of_side_front_wall_flow_in_backpass_front_wall=Math.Round((Doc.Fraction_of_side_front_wall_flow_in_backpass_front_wall/(Doc.Fraction_of_side_front_wall_flow_in_backpass_front_wall+Doc.Fraction_of_side_front_wall_flow_in_extended_side_wall)),2);

            Doc.Ratio_of_side_front_wall_flow_in_extended_side_wall = Math.Round((Doc.Fraction_of_side_front_wall_flow_in_extended_side_wall / (Doc.Fraction_of_side_front_wall_flow_in_backpass_front_wall + Doc.Fraction_of_side_front_wall_flow_in_extended_side_wall)),2);

            //Steam Screen Section
            for(int i=0;i<CountSS;i++)
            {
                SectionOfSS[i] = (Doc.Economiser_feed_water_flow_rate - Doc.Desuperheater_tap_off_at_economizer_outlet) * Doc.Ratio_of_total_flow_in_Sidewall_front * Doc.Ratio_of_side_front_wall_flow_in_backpass_front_wall;
            }

            //Superheater Section
            for(int i=0;i<CountSH;i++)
            {
                if(i==0)
                {
                    SectionOfSH[0] = Doc.Economiser_feed_water_flow_rate - Doc.Desuperheater_tap_off_at_economizer_outlet - Doc.Blow_down_flow + SHSpray[0];
                }
                else
                {
                    SectionOfSH[i] = SHSpray[i] + SectionOfSH[i - 1];
                }
            }

            //Fixed Value Calculation

            Doc.Furnace=Doc.Economiser_feed_water_flow_rate-Doc.Desuperheater_tap_off_at_economizer_outlet;

            Doc.Reverse_chamber_side_wall=Doc.Economiser_feed_water_flow_rate-Doc.Desuperheater_tap_off_at_economizer_outlet-Doc.Blow_down_flow;

            Doc.Extended_side_wall = (Doc.Economiser_feed_water_flow_rate - Doc.Desuperheater_tap_off_at_economizer_outlet - Doc.Blow_down_flow) * Doc.Ratio_of_total_flow_in_Sidewall_front * Doc.Ratio_of_side_front_wall_flow_in_extended_side_wall;

            Doc.Furnace_roof = Doc.Economiser_feed_water_flow_rate - Doc.Desuperheater_tap_off_at_economizer_outlet;

            double parameter1 = Doc.Desuperheating_spray_pressure * 0.980665 + 1.01325;

            Doc.Desuperheaing_spray_enthalpy = mStream_Macros.h_pT(parameter1,Doc.Desuperheating_spray_temperature);

            //Insert Fixed Values
            mCal_Flow_BLL.Insert_FlowModule_Calculation_FixedParameters(ProjectID, BoilerID, "Furnace", Doc.Furnace, BoilerLoad, ObjectiveID);

            mCal_Flow_BLL.Insert_FlowModule_Calculation_FixedParameters(ProjectID, BoilerID, "Furnace roof", Doc.Furnace_roof, BoilerLoad, ObjectiveID);

            mCal_Flow_BLL.Insert_FlowModule_Calculation_FixedParameters(ProjectID, BoilerID, "Reverse chamber side wall", Doc.Reverse_chamber_side_wall, BoilerLoad, ObjectiveID);

            mCal_Flow_BLL.Insert_FlowModule_Calculation_FixedParameters(ProjectID, BoilerID, "Extended side wall", Doc.Extended_side_wall, BoilerLoad, ObjectiveID);

            mCal_Flow_BLL.Insert_FlowModule_Calculation_FixedParameters(ProjectID, BoilerID, "Desuperheaing spray enthalpy", Doc.Desuperheaing_spray_enthalpy, BoilerLoad, ObjectiveID);


            //Insert Dynamic Values
            for(int i=0;i<CountEC;i++)
            {
                mCal_Flow_BLL.Insert_FlowModule_Calculation_DynamicParameters(ProjectID, BoilerID, EconomiserID[i], "Economiser", SectionOfEC[i], BoilerLoad, ObjectiveID);
            }

            for (int i = 0; i < CountSH; i++)
            {
                mCal_Flow_BLL.Insert_FlowModule_Calculation_DynamicParameters(ProjectID, BoilerID, SuperHeaterID[i], "Superheater Elements", SectionOfSH[i], BoilerLoad, ObjectiveID);
            }

            for (int i = 0; i < CountRH; i++)
            {
                mCal_Flow_BLL.Insert_FlowModule_Calculation_DynamicParameters(ProjectID, BoilerID, ReheaterID[i], "Reheater Elements", SectionOfRH[i], BoilerLoad, ObjectiveID);
            }

            for (int i = 0; i < CountWS; i++)
            {
                mCal_Flow_BLL.Insert_FlowModule_Calculation_DynamicParameters(ProjectID, BoilerID, WaterScreenID[i], "Water Screen Sections", SectionOfWS[i], BoilerLoad, ObjectiveID);
            }

            for (int i = 0; i < CountEC; i++)
            {
                mCal_Flow_BLL.Insert_FlowModule_Calculation_DynamicParameters(ProjectID, BoilerID, SteamScreenID[i], "Steam Screen Sections", SectionOfSS[i], BoilerLoad, ObjectiveID);
            }

            for(int i=0;i<CountSH;i++)
            {
                mCal_Flow_BLL.Insert_FlowModule_Calculation_DynamicParameters(ProjectID, BoilerID, SuperHeaterID[i], "SH DESH spray", SHSpray[i], BoilerLoad, ObjectiveID);
            }
        }
    }
}
