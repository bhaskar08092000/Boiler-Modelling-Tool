using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;
using BoilerModellingTool.BLL;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;

namespace GenericClasses
{
   public class Class_3A_Calculation
    {
        Stream_Macros obj_steam_macro;

        Cal_3A_SC mCal_3A_SC = null;

        Cal_3A_BLL mCal_3A_BLL = null;

        //string str = "server=172.16.10.35;database=BoilerDesign;uid=sa;password=crm12@tce;";
        //string ProjectID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
        //string BoilerID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
        //string BoilerLoad,ObjectiveID = "100%TMCR";
        //int ObjectiveID = 1;

        string BoilerID = null;
        string ProjectID = null;
       // string SectionID = null;
        string BoilerLoad = null;
        int ObjectiveID = 0;


        public Class_3A_Calculation(string BoilerID, string ProjectID,
             string BoilerLoad ,int ObjectiveID)
        {
            
            this.BoilerID = BoilerID;
            this.ProjectID = ProjectID;          
            this.BoilerLoad = BoilerLoad;
            this.ObjectiveID = ObjectiveID;

        }

        public void Calculation_For_3A()
        {
            obj_steam_macro = new Stream_Macros();

            DataSet mdataset = null;

            DataTable mDataTable = null;

            mCal_3A_SC = new Cal_3A_SC();

            mCal_3A_BLL = new Cal_3A_BLL();

            mdataset = new DataSet();

            mdataset = mCal_3A_BLL.GetInput_For_3A_Calculation(BoilerID, ProjectID, BoilerLoad,ObjectiveID);
            
            mCal_3A_SC.Higher_heating_value = Convert.ToDouble(mdataset.Tables[0].Rows[0]["Value"].ToString());

            mCal_3A_SC.Lower_heating_value= Convert.ToDouble( mdataset.Tables[1].Rows[0]["Value"].ToString());          

            mCal_3A_SC.Heat_loss_due_to_furnace_wall_radiation_and_convection = Convert.ToDouble(mdataset.Tables[3].Rows[0]["Value"].ToString());

            mCal_3A_SC.Dry_bottom_hopper_outlet_depth = Convert.ToDouble(mdataset.Tables[7].Rows[0]["Value"].ToString());

            mCal_3A_SC.Furnace_depth = Convert.ToDouble(mdataset.Tables[8].Rows[0]["Value"].ToString());

            mCal_3A_SC.Flow_of_superheated_steam_at_FSH_outlet = Convert.ToDouble(mdataset.Tables[4].Rows[0]["Value"].ToString());          

            mCal_3A_SC.Fuel_firing_rate = Convert.ToDouble(mdataset.Tables[6].Rows[0]["Value"].ToString());

            mCal_3A_SC.Furnace_width = Convert.ToDouble(mdataset.Tables[8].Rows[0]["Value"].ToString());

            if (ObjectiveID == 1)
            {

                mCal_3A_SC.Unburnt_carbon_loss = Convert.ToDouble(mdataset.Tables[2].Rows[0]["Value"].ToString());

                mCal_3A_SC.Flow_of_reheated_steam_at_Final_RH_outlet = Convert.ToDouble(mdataset.Tables[5].Rows[0]["Value"].ToString());

            }
            
            else
            {
                double result=0;
                result=RHFlow(ProjectID,BoilerID,BoilerLoad,ObjectiveID);
                mCal_3A_SC.Flow_of_reheated_steam_at_Final_RH_outlet=result;

                DataSet ds = new DataSet();

               ds= mCal_3A_BLL.EA_getUnburntCarbonLoss(ProjectID, BoilerID);

               DataTable dt = ds.Tables[0];
                //SqlConnection con = new SqlConnection(str);
                //SqlCommand cmd = new SqlCommand("spr_EA_getUnburntCarbonLoss");
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@vProjectID", DbType.Int32).Value = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
                //cmd.Parameters.AddWithValue("@vBoilerID", DbType.Int16).Value = "F85C19BA-20C9-4B61-975D-636C8404F30A";
                //cmd.Connection = con;
                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
               
                double Count = Convert.ToDouble(dt.Rows[0]["Value"].ToString());

                mCal_3A_SC.Unburnt_carbon_loss = Count;

            }


            mCal_3A_SC.Heat_loss_due_unburnt_carbon = (mCal_3A_SC.Unburnt_carbon_loss * mCal_3A_SC.Higher_heating_value) / mCal_3A_SC.Lower_heating_value;

            mCal_3A_SC.Heat_loss_due_incomplete_combustion = 0.00;

            mCal_3A_SC.Heat_loss_due_to_furnace_wall_radiation_and_convection = mCal_3A_SC.Heat_loss_due_to_furnace_wall_radiation_and_convection * mCal_3A_SC.Higher_heating_value / mCal_3A_SC.Lower_heating_value;

            mCal_3A_SC.Design_fuel_consumption = mCal_3A_SC.Fuel_firing_rate;

            mCal_3A_SC.Physical_heat_loss_of_ash = (31.5 * (mCal_3A_SC.Dry_bottom_hopper_outlet_depth * mCal_3A_SC.Furnace_width) / 1000) / (mCal_3A_SC.Lower_heating_value * mCal_3A_SC.Design_fuel_consumption / 1000) * 100;

            mCal_3A_SC.Heat_preservation_coefficient = 1 - mCal_3A_SC.Heat_loss_due_to_furnace_wall_radiation_and_convection / 100;

            mCal_3A_SC.Flowrate_superheatedsteam = mCal_3A_SC.Flow_of_superheated_steam_at_FSH_outlet * 1000;                                                                               

            mCal_3A_SC.Flowrate_reheatedsteam = mCal_3A_SC.Flow_of_reheated_steam_at_Final_RH_outlet * 1000;


            mDataTable = new DataTable();
            mDataTable = mdataset.Tables[10];
            

      
           for (int i = 0; i < mDataTable.Rows.Count; i++)
             {
                 int PID = Convert.ToInt16(mDataTable.Rows[i]["PID"].ToString());                                     
               

                switch(PID)
            {



                case 1: mCal_3A_BLL.Insert_3A_Calculation(BoilerID, ProjectID, mCal_3A_SC.Heat_loss_due_unburnt_carbon.ToString(), PID, BoilerLoad,ObjectiveID);
                    // mCal_3A_SC.Heat_loss_due_unburnt_carbon = Heat_loss_due_to_unburnt_carbon.ToString();

                    break;

                //Heat loss due to incomplete combustion

                case 2: mCal_3A_BLL.Insert_3A_Calculation(BoilerID, ProjectID, mCal_3A_SC.Heat_loss_due_incomplete_combustion.ToString(), PID, BoilerLoad,ObjectiveID);
                    //mCal_3A_SC.Heat_loss_due_incomplete_combustion = Heat_loss_due_to_incomplete_combustion.ToString();

                    break;
                //Heat loss due to furnace wall radiation and convection

                case 3: mCal_3A_BLL.Insert_3A_Calculation(BoilerID, ProjectID, mCal_3A_SC.Heat_loss_due_to_furnace_wall_radiation_and_convection.ToString(), PID, BoilerLoad,ObjectiveID);
                    // mCal_3A_SC.Heat_loss_due_furnace_radiation_convection = Heat_loss_furnace_radiation_convection.ToString();

                    break;
                //Design fuel consumption

                case 4: mCal_3A_BLL.Insert_3A_Calculation(BoilerID, ProjectID, mCal_3A_SC.Physical_heat_loss_of_ash.ToString(), PID, BoilerLoad,ObjectiveID);
                    //mCal_3A_SC.Design_fuel_consumption = Design_fuel_consumption.ToString();

                    break;
                //Physical heat loss of ash

                case 5: mCal_3A_BLL.Insert_3A_Calculation(BoilerID, ProjectID, mCal_3A_SC.Heat_preservation_coefficient.ToString(), PID, BoilerLoad,ObjectiveID);
                    // mCal_3A_SC.Physical_heat_loss_of_ash = Physical_heat_loss_ash.ToString();

                    break;

                //Heat preservation coefficient

                case 6: mCal_3A_BLL.Insert_3A_Calculation(BoilerID, ProjectID, mCal_3A_SC.Flowrate_superheatedsteam.ToString(), PID, BoilerLoad,ObjectiveID);
                    // mCal_3A_SC.Heat_preservation_coefficient = Heat_preservation_coefficient.ToString();

                    break;
                //Flow rate of superheated steam

                case 7: mCal_3A_BLL.Insert_3A_Calculation(BoilerID, ProjectID, mCal_3A_SC.Flowrate_reheatedsteam.ToString(), PID, BoilerLoad,ObjectiveID);
                    // mCal_3A_SC.Flowrate_superheatedsteam = Flow_rate_superheated_steam.ToString();

                    break;
                //Flow rate of reheated steam

                case 8: mCal_3A_BLL.Insert_3A_Calculation(BoilerID, ProjectID, mCal_3A_SC.Design_fuel_consumption.ToString(), PID, BoilerLoad,ObjectiveID);
                    // mCal_3A_SC.Flowrate_reheatedsteam = Flow_rate_reheated_steam.ToString();
                    break;

                default:
                    break;
                  
           
                }

           

        }
      
    
            //*catch
        {

        } 
       

    }

        public double RHFlow(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID)
        {
            Cal_3A_SC mCal_3A_SC = null;
            DataSet mdataset = null;
            Cal_3A_BLL mCal_3A_BLL = null;

            string[] HPHeaters = new string[5];

            DataSet ds1 = new DataSet();
            ds1 = mCal_3A_BLL.RHFlow_getHPHeatersCount(ProjectID, BoilerID, ObjectiveID, BoilerLoad);

            DataTable dt1 = ds1.Tables[0];

            //SqlConnection con = new SqlConnection(str);
            //SqlCommand cmd = new SqlCommand("spr_RHFlow_getHPHeatersCount");
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@vProjectID", DbType.Int32).Value = ProjectID;
            //cmd.Parameters.AddWithValue("@vBoilerID", DbType.Int16).Value = BoilerID;
            //cmd.Parameters.AddWithValue("@vBoilerLoad", DbType.Int32).Value = BoilerLoad;
            //cmd.Parameters.AddWithValue("@vObjective", DbType.Int16).Value = ObjectiveID;
            //cmd.Connection = con;
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            int Count = Convert.ToInt16(dt1.Rows[0]["Total"].ToString());


            DataSet dset = new DataSet();

            dset = mCal_3A_BLL.BindHPHeaters(ProjectID, BoilerID, ObjectiveID, BoilerLoad);

            //SqlCommand cmd7 = new SqlCommand();
            //cmd7.CommandType = CommandType.StoredProcedure;
            //cmd7.CommandText = "dbo.spr_BindHPHeaters";
            //cmd7.Connection = con;
            //cmd7.Parameters.AddWithValue("@vProjectID", DbType.String).Value = ProjectID;
            //cmd7.Parameters.AddWithValue("@vBoilerID", DbType.String).Value = BoilerID;
            //cmd7.Parameters.AddWithValue("@vBoilerLoad", DbType.Int32).Value = BoilerLoad;
            //cmd7.Parameters.AddWithValue("@vObjective", DbType.Int16).Value = ObjectiveID;
            //con.Open();
            //SqlDataAdapter sda2 = new SqlDataAdapter(cmd7);
            //DataTable dt2 = new DataTable();
            //sda2.Fill(dt2);
           // SqlDataReader dr = cmd7.ExecuteReader();

            for (int i = 0; i < dset.Tables[0].Rows.Count;i++)

            {

                HPHeaters[i] = dset.Tables[0].Rows[i][0].ToString();
                       
                }
            //dr.Close();

            //for (int i=0;i<=Count;i++)
            //{
            //    HPHeaters[i] = dt2.Rows[i]["SectionID"].ToString();
            //}

            //Input
            double[] Extraction_steam_inlet_pressure = new double[5];
            double[] Extraction_steam_inlet_temperature = new double[5];
            double[] Extraction_steam_outlet_temperature = new double[5];
            double[] Feed_water_inlet_pressure = new double[5];
            double[] Feed_water_inlet_temperature = new double[5];
            double[] Feed_water_outlet_pressure = new double[5];
            double[] Feed_water_outlet_temperature = new double[5];

            //Output
            double[] Extraction_steam_inlet_enthalpy = new double[5];
            double[] Extraction_steam_outlet_pressure = new double[5];
            double[] Extraction_steam_outlet_enthalpy = new double[5];
            double[] Feed_water_inlet_enthapy = new double[5];
            double[] Feed_water_outlet_enthapy = new double[5];
            double[] Heat_gain_by_the_feed_water_in_HPH = new double[5];
            double[] Extracted_steam_flow_to_HPH = new double[5];

            double[] Total_drains_from_previous_HPH_to_this_HP_heater = new double[5];

            double[] Heat_given_by_the_drain_of_pervious_HPH = new double[5];
            //double Extracted_steam_flow_to_HPH;
            double Total_extracted_steam_from_HP_heater = 0;
            double Total_RH_flow_rate = 0;

            double result = 0;

            try
            {
                mCal_3A_SC = new Cal_3A_SC();
                mdataset = new DataSet();
                mCal_3A_BLL = new Cal_3A_BLL();

                mdataset = mCal_3A_BLL.GetInput_For_RHFlow(ProjectID, BoilerID, ObjectiveID, BoilerLoad);

                mCal_3A_SC.BFP_inlet_flow_rate = Convert.ToDouble(mdataset.Tables[0].Rows[0]["Value"].ToString());

                mCal_3A_SC.RH_desuperheating_spray = Convert.ToDouble(mdataset.Tables[1].Rows[0]["Value"].ToString());

                mCal_3A_SC.SH_Desuperheating_spray_stage_1 = Convert.ToDouble(mdataset.Tables[2].Rows[0]["Value"].ToString());

                mCal_3A_SC.SH_Desuperheating_spray_stage_2 = Convert.ToDouble(mdataset.Tables[3].Rows[0]["Value"].ToString());

                mCal_3A_SC.Main_steam_flow_rate = Convert.ToDouble(mdataset.Tables[4].Rows[0]["Value"].ToString());

                mCal_3A_SC.Feed_water_flow_rate_through_HP_heater = Convert.ToDouble(mdataset.Tables[5].Rows[0]["Value"].ToString());

                for (int j = 0; j < Count; j++)
                {
                    DataSet dseet = new DataSet();

                    dseet = mCal_3A_BLL.RHFlow_getHPHeatersParameters(ProjectID, BoilerID, HPHeaters[j]);


                    //SqlCommand cmd1 = new SqlCommand("spr_RHFlow_getHPHeatersParameters");
                    //cmd1.CommandType = CommandType.StoredProcedure;
                    //cmd1.Parameters.AddWithValue("@vProjectID", DbType.String).Value = ProjectID;
                    //cmd1.Parameters.AddWithValue("@vBoilerID", DbType.String).Value = BoilerID;
                    //cmd1.Parameters.AddWithValue("@vSectionID", DbType.String).Value = HPHeaters[j];
                    //cmd1.Connection = con;
                    //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    //DataTable dt1 = new DataTable();
                    //sda1.Fill(dt1);
                    Extraction_steam_inlet_pressure[j] = Convert.ToDouble(dseet.Tables[0].Rows[0]["Value"].ToString());
                    Extraction_steam_inlet_temperature[j] = Convert.ToDouble(dseet.Tables[0].Rows[1]["Value"].ToString());
                    Extraction_steam_outlet_temperature[j] = Convert.ToDouble(dseet.Tables[0].Rows[2]["Value"].ToString());
                    Feed_water_inlet_pressure[j] = Convert.ToDouble(dseet.Tables[0].Rows[3]["Value"].ToString());
                    Feed_water_inlet_temperature[j] = Convert.ToDouble(dseet.Tables[0].Rows[4]["Value"].ToString());
                    Feed_water_outlet_pressure[j] = Convert.ToDouble(dseet.Tables[0].Rows[5]["Value"].ToString());
                    Feed_water_outlet_temperature[j] = Convert.ToDouble(dseet.Tables[0].Rows[6]["Value"].ToString());
                    //cmd1.Parameters.Clear();
                    //dt1.Clear();
                }

                int k = 0;

                for (k = 0; k < 1; k++)
                {
                    Extraction_steam_inlet_enthalpy[k] = obj_steam_macro.h_pT(Extraction_steam_inlet_pressure[k], Extraction_steam_inlet_temperature[k]);
                    Extraction_steam_outlet_pressure[k] = Extraction_steam_inlet_pressure[k] - 0.5;
                    Extraction_steam_outlet_enthalpy[k] = obj_steam_macro.h_pT(Extraction_steam_outlet_pressure[k], Extraction_steam_outlet_temperature[k]);
                    Feed_water_inlet_enthapy[k] = obj_steam_macro.h_pT(Feed_water_inlet_pressure[k], Feed_water_inlet_temperature[k]);
                    Feed_water_outlet_enthapy[k] = obj_steam_macro.h_pT(Feed_water_outlet_pressure[k], Feed_water_outlet_temperature[k]);
                    Heat_gain_by_the_feed_water_in_HPH[k] = mCal_3A_SC.Feed_water_flow_rate_through_HP_heater / 3.6 * (Feed_water_outlet_enthapy[k] - Feed_water_inlet_enthapy[k]);
                    Extracted_steam_flow_to_HPH[k] = Heat_gain_by_the_feed_water_in_HPH[k] / (Extraction_steam_inlet_enthalpy[k] - Extraction_steam_outlet_enthalpy[k]) * 3.6;
                }

                for (int l = 1; l < Count; l++)
                {
                    Extraction_steam_inlet_enthalpy[l] = obj_steam_macro.h_pT(Extraction_steam_inlet_pressure[l], Extraction_steam_inlet_temperature[l]);
                    Extraction_steam_outlet_pressure[l] = Extraction_steam_inlet_pressure[l] - 0.5;
                    Extraction_steam_outlet_enthalpy[l] = obj_steam_macro.h_pT(Extraction_steam_outlet_pressure[k], Extraction_steam_outlet_temperature[k]);
                    Feed_water_inlet_enthapy[l] = obj_steam_macro.h_pT(Feed_water_inlet_pressure[l], Feed_water_inlet_temperature[l]);
                    Feed_water_outlet_enthapy[l] = obj_steam_macro.h_pT(Feed_water_outlet_pressure[l], Feed_water_outlet_temperature[l]);
                    Heat_gain_by_the_feed_water_in_HPH[l] = mCal_3A_SC.Feed_water_flow_rate_through_HP_heater / 3.6 * (Feed_water_outlet_enthapy[k] - Feed_water_inlet_enthapy[l]);
                    Total_drains_from_previous_HPH_to_this_HP_heater[l] = Extracted_steam_flow_to_HPH[l - 1];
                    Heat_given_by_the_drain_of_pervious_HPH[l] = Total_drains_from_previous_HPH_to_this_HP_heater[l] / 3.6 * (Extraction_steam_outlet_enthalpy[l - 1] - Extraction_steam_outlet_enthalpy[l]);
                    Extracted_steam_flow_to_HPH[l] = (Heat_gain_by_the_feed_water_in_HPH[l] - Heat_given_by_the_drain_of_pervious_HPH[l]) * 3.6 / (Extraction_steam_inlet_enthalpy[l] - Extraction_steam_outlet_enthalpy[l]);
                }


                int m = 0;
                while (m != Count)
                {
                    double p1 = Extracted_steam_flow_to_HPH[m];
                    Total_extracted_steam_from_HP_heater += p1;
                    m++;
                }

                Total_RH_flow_rate = mCal_3A_SC.Main_steam_flow_rate - Total_extracted_steam_from_HP_heater + mCal_3A_SC.RH_desuperheating_spray;

                result = Total_RH_flow_rate;
            }
            catch
            {

            }

            return result;
        }
    }
}
