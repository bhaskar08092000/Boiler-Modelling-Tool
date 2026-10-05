using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BoilerModellingTool.BLL;
using System.IO;
using System.Text;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;
using System.Configuration;

namespace GenericClasses
{
    public class Class_16A_Calculation
    {
        public DataTable dtgrid = new DataTable();

        //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        public void Calculation_For_16A(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID)
        {

            Cal_16A_SC mCal_16A_SC = null;
            Cal_16A_BLL mCal_16A_BLL = null;
            DataSet mdataset = null;
            DataTable mDataTable = null;
            Stream_Macros mStream_Macros_BLL = null;

            mCal_16A_SC = new Cal_16A_SC();
            mCal_16A_BLL = new Cal_16A_BLL();
            mStream_Macros_BLL = new Stream_Macros();
            mdataset = new DataSet();


            mdataset = mCal_16A_BLL.GetInput_For_16A_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID);
            System.Diagnostics.Debug.WriteLine("===== 16A START =====");
            System.Diagnostics.Debug.WriteLine("ProjectID: " + ProjectID);
            System.Diagnostics.Debug.WriteLine("BoilerID: " + BoilerID);
            System.Diagnostics.Debug.WriteLine("BoilerLoad: " + BoilerLoad);
            System.Diagnostics.Debug.WriteLine("===== 16A DATASET DEBUG =====");

            for (int t = 0; t < mdataset.Tables.Count; t++)
            {
                DataTable table = mdataset.Tables[t];

                System.Diagnostics.Debug.WriteLine("Table[" + t + "] Rows: " + table.Rows.Count);

                for (int r = 0; r < table.Rows.Count; r++)
                {
                    string row = "";
                    foreach (DataColumn col in table.Columns)
                    {
                        row += col.ColumnName + "=" + table.Rows[r][col] + " | ";
                    }
                    System.Diagnostics.Debug.WriteLine(row);
                }
            }


            //Parameters


            mCal_16A_SC.Flue_gas_flow_rate_at_APH_inlet = Convert.ToDouble(mdataset.Tables[0].Rows[0][0].ToString()) / 3.6;

            mCal_16A_SC.Flue_gas_flow_rate_at_APH_outlet = Convert.ToDouble(mdataset.Tables[1].Rows[0][0].ToString()) / 3.6;

            mCal_16A_SC.Primary_air_flow_rate_at_inlet = Convert.ToDouble(mdataset.Tables[2].Rows[0][0].ToString());

            mCal_16A_SC.Secondary_air_flow_rate_at_inlet = Convert.ToDouble(mdataset.Tables[3].Rows[0][0].ToString());



            mCal_16A_SC.Primary_air_flow_rate_at_outlet = Convert.ToDouble(mdataset.Tables[4].Rows[0][0].ToString()) / 3.6;

            mCal_16A_SC.Secondary_air_flow_rate_at_outlet = Convert.ToDouble(mdataset.Tables[5].Rows[0][0].ToString()) / 3.6;

            mCal_16A_SC.Flue_gas_inlet_temp = Convert.ToDouble(mdataset.Tables[6].Rows[0][0].ToString());

            mCal_16A_SC.Flue_gas_outlet__temp_without_leakage_calculated = Convert.ToDouble(mdataset.Tables[7].Rows[0][0].ToString());


            mCal_16A_SC.Flue_gas_outlet_temp_with_leakage = Convert.ToDouble(mdataset.Tables[8].Rows[0][0].ToString());

            mCal_16A_SC.Primary_air_inlet_temp = Convert.ToDouble(mdataset.Tables[9].Rows[0][0].ToString());

            mCal_16A_SC.Secondary_air_inlet_temp = Convert.ToDouble(mdataset.Tables[10].Rows[0][0].ToString());

            mCal_16A_SC.Primary_air_outlet_temp = Convert.ToDouble(mdataset.Tables[11].Rows[0][0].ToString());


            mCal_16A_SC.Secondary_air_outlet_temp = Convert.ToDouble(mdataset.Tables[12].Rows[0][0].ToString());

            mCal_16A_SC.O2_in_flue_gas_at_APH_inlet = Convert.ToDouble(mdataset.Tables[13].Rows[0][0].ToString());

            mCal_16A_SC.O2_in_flue_gas_at_APH_outlet_dry_basis = Convert.ToDouble(mdataset.Tables[14].Rows[0][0].ToString());

            mCal_16A_SC.APH_design_capcity_ratio = Convert.ToDouble(mdataset.Tables[15].Rows[0][0].ToString());

            mCal_16A_SC.Heat_balance_error_in_APH = Convert.ToDouble(mdataset.Tables[16].Rows[0][0].ToString());



            mCal_16A_SC.Inlet_primary_air_enthalpy = mCal_16A_BLL.Get_2A_ReqEnthalpy1(BoilerID, ProjectID, mCal_16A_SC.Primary_air_inlet_temp.ToString());


            mCal_16A_SC.Inlet_secondary_air_enthalpy = mCal_16A_BLL.Get_2A_ReqEnthalpy1(BoilerID, ProjectID, mCal_16A_SC.Secondary_air_inlet_temp.ToString());


            mCal_16A_SC.Inlet_fluegas_enthalpy = mCal_16A_BLL.Get_2A_ReqEnthalpy(BoilerID, ProjectID, mCal_16A_SC.Flue_gas_inlet_temp.ToString());

            mCal_16A_SC.Outlet_primary_air_enthalpy = mCal_16A_BLL.Get_2A_ReqEnthalpy1(BoilerID, ProjectID, mCal_16A_SC.Primary_air_outlet_temp.ToString());

            mCal_16A_SC.Outlet_secondary_air_enthalpy = mCal_16A_BLL.Get_2A_ReqEnthalpy1(BoilerID, ProjectID, mCal_16A_SC.Secondary_air_outlet_temp.ToString());

            mCal_16A_SC.Outlet_fluegas_enthalpy = mCal_16A_BLL.Get_2A_ReqEnthalpy(BoilerID, ProjectID, mCal_16A_SC.Flue_gas_outlet_temp_with_leakage.ToString());

            mCal_16A_SC.Leakage_air_enthalpy_at_flue_gas_outlet_temp = mCal_16A_BLL.Get_2A_ReqEnthalpy(BoilerID, ProjectID, mCal_16A_SC.Flue_gas_outlet_temp_with_leakage.ToString());

            mCal_16A_SC.Weighted_air_temperature_at_inlet_calculated = (mCal_16A_SC.Primary_air_inlet_temp + mCal_16A_SC.Secondary_air_inlet_temp) / 2;

            mCal_16A_SC.Leakage_air_enthalpy_at_air_inlet_temp = mCal_16A_BLL.Get_2A_ReqEnthalpy1(BoilerID, ProjectID, mCal_16A_SC.Weighted_air_temperature_at_inlet_calculated.ToString());

            mCal_16A_SC.Mean_specific_heat_of_fluegas_at_flue_gas_outlet_temp_kJ_kgK = mCal_16A_BLL.Get_2A_ReqEnthalpy2(BoilerID, ProjectID, mCal_16A_SC.Flue_gas_outlet_temp_with_leakage.ToString());

            //Calculation   

            //=(D18+D19)/2
            mCal_16A_SC.Weighted_air_temperature_at_inlet = (mCal_16A_SC.Primary_air_inlet_temp + mCal_16A_SC.Secondary_air_inlet_temp) / 2;

            //=(D12*D20+D13*D21)/(D12+D13)
            mCal_16A_SC.Weighted_air_temperature_at_outlet = (mCal_16A_SC.Primary_air_flow_rate_at_outlet * mCal_16A_SC.Primary_air_outlet_temp + mCal_16A_SC.Secondary_air_flow_rate_at_outlet * mCal_16A_SC.Secondary_air_outlet_temp) / (mCal_16A_SC.Primary_air_flow_rate_at_outlet + mCal_16A_SC.Secondary_air_flow_rate_at_outlet);

            //=(D30-D31)/(D17-F43)
            mCal_16A_SC.Mean_specific_heat_of_air_btw_air_inlet_and_flue_gas_outlet_temp_kJ_kgK = (mCal_16A_SC.Leakage_air_enthalpy_at_flue_gas_outlet_temp - mCal_16A_SC.Leakage_air_enthalpy_at_air_inlet_temp) / (mCal_16A_SC.Flue_gas_outlet_temp_with_leakage - mCal_16A_SC.Weighted_air_temperature_at_inlet);

            //=(D34-D33)/(21-D34)*0.9*100
            mCal_16A_SC.Air_leakage_percentage_in_operating_case = (mCal_16A_SC.O2_in_flue_gas_at_APH_outlet_dry_basis - mCal_16A_SC.O2_in_flue_gas_at_APH_inlet) / (21 - mCal_16A_SC.O2_in_flue_gas_at_APH_outlet_dry_basis) * 0.9 * 100;

            //=(D8*F47)/100+D8
            mCal_16A_SC.Flue_gas_flow_rate_at_APH_outlet_calculated = (mCal_16A_SC.Flue_gas_flow_rate_at_APH_inlet * mCal_16A_SC.Air_leakage_percentage_in_operating_case) / 100 + mCal_16A_SC.Flue_gas_flow_rate_at_APH_inlet;


            // =F47/100*F45/D35*(D17-D18)+D17
            mCal_16A_SC.Flue_gas_outlet__temp_without_leakage_calculated = mCal_16A_SC.Air_leakage_percentage_in_operating_case / 100 * mCal_16A_SC.Mean_specific_heat_of_air_btw_air_inlet_and_flue_gas_outlet_temp_kJ_kgK / mCal_16A_SC.Mean_specific_heat_of_fluegas_at_flue_gas_outlet_temp_kJ_kgK * (mCal_16A_SC.Flue_gas_outlet_temp_with_leakage - mCal_16A_SC.Primary_air_inlet_temp) + mCal_16A_SC.Flue_gas_outlet_temp_with_leakage;

            //=D15-F49
            mCal_16A_SC.Gas_drop_without_leakage = mCal_16A_SC.Flue_gas_inlet_temp - mCal_16A_SC.Flue_gas_outlet__temp_without_leakage_calculated;

            //=F44-F43
            mCal_16A_SC.Air_side_temp_rise = mCal_16A_SC.Weighted_air_temperature_at_outlet - mCal_16A_SC.Weighted_air_temperature_at_inlet;

            //=D15-F43
            mCal_16A_SC.Temp_head = mCal_16A_SC.Flue_gas_inlet_temp - mCal_16A_SC.Weighted_air_temperature_at_inlet;




            //=F50/F51
            mCal_16A_SC.Heat_capcity_ratio = mCal_16A_SC.Gas_drop_without_leakage / mCal_16A_SC.Air_side_temp_rise;

            //=F51/$F$52*100
            mCal_16A_SC.Air_side_efficiency = mCal_16A_SC.Air_side_temp_rise / mCal_16A_SC.Temp_head * 100;

            //=F50/$F$52*100
            mCal_16A_SC.Gas_side_efficiency = mCal_16A_SC.Gas_drop_without_leakage / mCal_16A_SC.Temp_head * 100;

            //=(F55/F53+(24.5*(F53-$D$37)))*$D$37
            mCal_16A_SC.Gas_side_efficiency_corrected_for_change_In_X_from_design = (mCal_16A_SC.Gas_side_efficiency / mCal_16A_SC.Heat_capcity_ratio + 24.5 * (mCal_16A_SC.Heat_capcity_ratio - mCal_16A_SC.APH_design_capcity_ratio)) * mCal_16A_SC.Heat_capcity_ratio;

            //=D9-D8
            mCal_16A_SC.Fluegas_side_leakage = mCal_16A_SC.Flue_gas_flow_rate_at_APH_outlet - mCal_16A_SC.Flue_gas_flow_rate_at_APH_inlet;

            //=D10-D12
            mCal_16A_SC.Primary_air_side_leakage = mCal_16A_SC.Primary_air_flow_rate_at_inlet - mCal_16A_SC.Primary_air_flow_rate_at_outlet;

            if (mCal_16A_SC.Primary_air_flow_rate_at_inlet == null)
            {
                mCal_16A_SC.Primary_air_flow_rate_at_inlet = 0;
            }

            //SqlConnection con1 = new SqlConnection(constr);
            //SqlCommand cmd1 = new SqlCommand("[dbo].[spr_Get_16A_Parameters]");
            //cmd1.CommandType = CommandType.StoredProcedure;
            //cmd1.Connection = con1;
            //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            //DataTable dt1 = new DataTable();
            //sda1.Fill(dt1);


            for (int i=1;i<17;i++)
            {
                int pid = i;

                switch (pid)
                {
                    case 1:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16A_SC.Weighted_air_temperature_at_inlet.ToString(), pid);

                        break;

                    case 2:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID, BoilerLoad, ObjectiveID, mCal_16A_SC.Weighted_air_temperature_at_outlet.ToString(), pid);

                        break;


                    case 3:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID, BoilerLoad, ObjectiveID, mCal_16A_SC.Mean_specific_heat_of_air_btw_air_inlet_and_flue_gas_outlet_temp_kJ_kgK.ToString(), pid);

                        break;


                    case 4:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID,BoilerLoad, ObjectiveID, mCal_16A_SC.Air_leakage_percentage_in_operating_case.ToString(), pid);

                        break;



                    case 5:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID, BoilerLoad, ObjectiveID, mCal_16A_SC.Flue_gas_flow_rate_at_APH_outlet_calculated.ToString(), pid);

                        break;


                    case 6:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID, BoilerLoad, ObjectiveID, mCal_16A_SC.Flue_gas_outlet__temp_without_leakage_calculated.ToString(), pid);

                        break;


                    case 7:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID,  BoilerLoad, ObjectiveID, mCal_16A_SC.Gas_drop_without_leakage.ToString(), pid);

                        break;

                    case 8:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID,  BoilerLoad, ObjectiveID, mCal_16A_SC.Air_side_temp_rise.ToString(), pid);

                        break;




                    case 9:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID, BoilerLoad, ObjectiveID, mCal_16A_SC.Temp_head.ToString(), pid);

                        break;


                    case 10:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID, BoilerLoad, ObjectiveID, mCal_16A_SC.Heat_capcity_ratio.ToString(), pid);

                        break;

                    case 11:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID,BoilerLoad, ObjectiveID, mCal_16A_SC.Air_side_efficiency.ToString(), pid);

                        break;

                    case 12:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID, BoilerLoad, ObjectiveID, mCal_16A_SC.Gas_side_efficiency.ToString(), pid);

                        break;

                    case 13:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID, BoilerLoad, ObjectiveID, mCal_16A_SC.Gas_side_efficiency_corrected_for_change_In_X_from_design.ToString(), pid);

                        break;

                    case 14:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID,  BoilerLoad, ObjectiveID, mCal_16A_SC.Fluegas_side_leakage.ToString(), pid);

                        break;

                    case 15:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID,  BoilerLoad, ObjectiveID, mCal_16A_SC.Primary_air_side_leakage.ToString(), pid);

                        break;

                    case 16:

                        mCal_16A_BLL.Insert_16A_Calculation(BoilerLoad, ProjectID, BoilerLoad, ObjectiveID, mCal_16A_SC.Secondary_air_side_leakage.ToString(), pid);

                        break;




                }
            }

        } 


    }
}
