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


namespace GenericClasses
{
    public class Class_16B_Calculation
    {
        public void Calculation_For_16B(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID)
        {

            Cal_16B_SC mCal_16B_SC = null;
            Cal_16B_BLL mCal_16B_BLL = null;
            DataSet mdataset = null;
            DataTable mDataTable = null;
            Stream_Macros mStream_Macros_BLL = null;






            mCal_16B_SC = new Cal_16B_SC();
            mCal_16B_BLL = new Cal_16B_BLL();
            mStream_Macros_BLL = new Stream_Macros();
            mdataset = new DataSet();

            System.Diagnostics.Debug.WriteLine("===== CALLING 16B INPUT SP =====");
            System.Diagnostics.Debug.WriteLine("BoilerID: " + BoilerID);
            System.Diagnostics.Debug.WriteLine("ProjectID: " + ProjectID);
            System.Diagnostics.Debug.WriteLine("BoilerLoad: " + BoilerLoad);
            System.Diagnostics.Debug.WriteLine("ObjectiveID: " + ObjectiveID);




            mdataset = mCal_16B_BLL.GetInput_For_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID);


            // ✅ CHECK DATASET
            if (mdataset == null)
            {
                System.Diagnostics.Debug.WriteLine("❌ 16B DATASET is NULL");
                return;
            }

            System.Diagnostics.Debug.WriteLine("✅ Tables Count: " + mdataset.Tables.Count);

            if (mdataset.Tables.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("❌ No tables returned");
                return;
            }


            System.Diagnostics.Debug.WriteLine("✅ Rows in Table[0]: " + mdataset.Tables[0].Rows.Count);

            if (mdataset.Tables[0].Rows.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("❌ No rows in Table[0]");
                return;
            }

            // ✅ PRINT VALUE
            for (int c = 0; c < mdataset.Tables[0].Columns.Count; c++)
            {
                string colName = mdataset.Tables[0].Columns[c].ColumnName;
                string valu = mdataset.Tables[0].Rows[0][c].ToString();

                System.Diagnostics.Debug.WriteLine(
                    "Column[" + c + "] = " + colName + " | VALUE = " + valu
                );
            }



            mCal_16B_SC.Flue_gas_flow_rate_at_primary_APH_inlet = Convert.ToDouble(mdataset.Tables[0].Rows[0][0].ToString()) / 3.6;

            mCal_16B_SC.Flue_gas_flow_rate_at_primary_APHoutlet = Convert.ToDouble(mdataset.Tables[1].Rows[0][0].ToString()) / 3.6;

            mCal_16B_SC.Flue_gas_flow_rate_at_secondary_APH_inlet = Convert.ToDouble(mdataset.Tables[2].Rows[0][0].ToString()) / 3.6;

            mCal_16B_SC.Flue_gas_flow_rate_at_secondary_APH_outlet = Convert.ToDouble(mdataset.Tables[3].Rows[0][0].ToString()) / 3.6;

            mCal_16B_SC.Primary_air_flow_rate_at_inlet = Convert.ToDouble(mdataset.Tables[4].Rows[0][0].ToString());


            if (mCal_16B_SC.Primary_air_flow_rate_at_inlet == null)
            {
                mCal_16B_SC.Primary_air_flow_rate_at_inlet = 0;
            }

            mCal_16B_SC.Secondary_air_flow_rate_at_inlet = Convert.ToDouble(mdataset.Tables[5].Rows[0][0].ToString());


            if (mCal_16B_SC.Secondary_air_flow_rate_at_inlet == null)
            {
                mCal_16B_SC.Secondary_air_flow_rate_at_inlet = 0;
            }

            mCal_16B_SC.Primary_air_flow_rate_at_outlet = Convert.ToDouble(mdataset.Tables[6].Rows[0][0].ToString());

            mCal_16B_SC.Secondary_air_flow_rate_at_outlet = Convert.ToDouble(mdataset.Tables[7].Rows[0][0].ToString());

            mCal_16B_SC.Primary_APH_Flue_gas_inlet_temp = Convert.ToDouble(mdataset.Tables[8].Rows[0][0].ToString());

            mCal_16B_SC.Primary_APH_Flue_gas_outlet__temp_without_leakage = Convert.ToDouble(mdataset.Tables[9].Rows[0][0].ToString());


            if (mCal_16B_SC.Primary_APH_Flue_gas_outlet__temp_without_leakage == null)
            {
                mCal_16B_SC.Primary_APH_Flue_gas_outlet__temp_without_leakage = 0;
            }

            mCal_16B_SC.Primary_Flue_gas_outlet__temp_with_leakage = Convert.ToDouble(mdataset.Tables[10].Rows[0][0].ToString());

            mCal_16B_SC.Secondary_APH_Flue_gas_inlet_temp = Convert.ToDouble(mdataset.Tables[11].Rows[0][0].ToString());

            mCal_16B_SC.Secondary_APH_Flue_gas_outlet__temp_without_leakage = Convert.ToDouble(mdataset.Tables[12].Rows[0][0].ToString());

            if (mCal_16B_SC.Secondary_APH_Flue_gas_outlet__temp_without_leakage == null)
            {
                mCal_16B_SC.Secondary_APH_Flue_gas_outlet__temp_without_leakage = 0;
            }

            mCal_16B_SC.Secondary_Flue_gas_outlet__temp_with_leakage = Convert.ToDouble(mdataset.Tables[13].Rows[0][0].ToString());

            mCal_16B_SC.Primary_air_inlet_temp = Convert.ToDouble(mdataset.Tables[14].Rows[0][0].ToString());

            mCal_16B_SC.Secondary_air_inlet_temp = Convert.ToDouble(mdataset.Tables[15].Rows[0][0].ToString());

            mCal_16B_SC.Primary_air_outlet_temp = Convert.ToDouble(mdataset.Tables[16].Rows[0][0].ToString());

            mCal_16B_SC.Secondary_air_outlet_temp = Convert.ToDouble(mdataset.Tables[17].Rows[0][0].ToString());

            mCal_16B_SC.O2_in_flue_gas_at_primary_APH_inlet = Convert.ToDouble(mdataset.Tables[18].Rows[0][0].ToString());

            mCal_16B_SC.O2_in_flue_gas_at_Primary_APH_outlet_dry_basis = Convert.ToDouble(mdataset.Tables[19].Rows[0][0].ToString());

            mCal_16B_SC.O2_in_flue_gas_at_Secondary_APH_inlet = Convert.ToDouble(mdataset.Tables[20].Rows[0][0].ToString());

            mCal_16B_SC.O2_in_flue_gas_at_Secondary_APH_outlet_dry_basis = Convert.ToDouble(mdataset.Tables[21].Rows[0][0].ToString());

            mCal_16B_SC.APH_design_capcity_ratio = Convert.ToDouble(mdataset.Tables[22].Rows[0][0].ToString());

            mCal_16B_SC.Heat_balance_error_in_APH = Convert.ToDouble(mdataset.Tables[23].Rows[0][0].ToString());


            mCal_16B_SC.Inlet_primary_air_enthalpy = mCal_16B_BLL.Get_2A_ReqEnthalpy1(BoilerID, ProjectID, mCal_16B_SC.Primary_air_inlet_temp.ToString());

            mCal_16B_SC.Inlet_secondary_air_enthalpy = mCal_16B_BLL.Get_2A_ReqEnthalpy1(BoilerID, ProjectID, mCal_16B_SC.Secondary_air_inlet_temp.ToString());

            mCal_16B_SC.Inlet_fluegas_enthalpy_of_primary_APH = mCal_16B_BLL.Get_2A_ReqEnthalpy(BoilerID, ProjectID, mCal_16B_SC.Primary_APH_Flue_gas_inlet_temp.ToString());

            mCal_16B_SC.Inlet_fluegas_enthalpy_of_secondary_APH = mCal_16B_BLL.Get_2A_ReqEnthalpy(BoilerID, ProjectID, mCal_16B_SC.Secondary_APH_Flue_gas_inlet_temp.ToString());

            mCal_16B_SC.Outlet_primary_air_enthalpy = mCal_16B_BLL.Get_2A_ReqEnthalpy1(BoilerID, ProjectID, mCal_16B_SC.Primary_air_outlet_temp.ToString());

            mCal_16B_SC.Outlet_secondary_air_enthalpy = mCal_16B_BLL.Get_2A_ReqEnthalpy1(BoilerID, ProjectID, mCal_16B_SC.Secondary_air_outlet_temp.ToString());

            mCal_16B_SC.Primary_APH_Oulet_fluegas_enthalpy = mCal_16B_BLL.Get_2A_ReqEnthalpy(BoilerID, ProjectID, mCal_16B_SC.Primary_Flue_gas_outlet__temp_with_leakage.ToString());

            mCal_16B_SC.Secondary_APH_Oulet_fluegas_enthalpy = mCal_16B_BLL.Get_2A_ReqEnthalpy(BoilerID, ProjectID, mCal_16B_SC.Secondary_Flue_gas_outlet__temp_with_leakage.ToString());

            mCal_16B_SC.Primary_APH__Leakage_air_enthalpy_at_flue_gas_outlet_temp = mCal_16B_BLL.Get_2A_ReqEnthalpy(BoilerID, ProjectID, mCal_16B_SC.Primary_Flue_gas_outlet__temp_with_leakage.ToString());

            mCal_16B_SC.Primary_APH_Leakage_air_enthalpy_at_air_inlet_temp = mCal_16B_BLL.Get_2A_ReqEnthalpy1(BoilerID, ProjectID, mCal_16B_SC.Primary_air_inlet_temp.ToString());

            mCal_16B_SC.Secondary__APH_Leakage_air_enthalpy_at_flue_gas_outlet_temp = mCal_16B_BLL.Get_2A_ReqEnthalpy(BoilerID, ProjectID, mCal_16B_SC.Secondary_Flue_gas_outlet__temp_with_leakage.ToString());

            mCal_16B_SC.Seconday__APH_Leakage_air_enthalpy_at_air_inlet_temp = mCal_16B_BLL.Get_2A_ReqEnthalpy1(BoilerID, ProjectID, mCal_16B_SC.Secondary_air_inlet_temp.ToString());


            mCal_16B_SC.Primary_APH_Mean_specific_heat_of_fluegas_at_flue_gas_outlet_temp_kJ_kgK = mCal_16B_BLL.Get_2A_ReqEnthalpy2(BoilerID, ProjectID, mCal_16B_SC.Primary_Flue_gas_outlet__temp_with_leakage.ToString());

            mCal_16B_SC.Secondary_APH__Mean_specific_heat_of_fluegas_at_flue_gas_outlet_temp_kJ_kgK = mCal_16B_BLL.Get_2A_ReqEnthalpy2(BoilerID, ProjectID, mCal_16B_SC.Secondary_Flue_gas_outlet__temp_with_leakage.ToString());



            //=D23
            mCal_16B_SC.Air_temperature_at_inlet = mCal_16B_SC.Primary_air_inlet_temp;

            //=D25
            mCal_16B_SC.Air_temperature_at_outlet = mCal_16B_SC.Primary_air_outlet_temp;

            ////=(D37-D38)/(D19-F57)
            mCal_16B_SC.Mean_specific_heat_of_air_btw_air_inlet_and_flue_gas_outlet_temp_kJ_kgK = (mCal_16B_SC.Primary_APH__Leakage_air_enthalpy_at_flue_gas_outlet_temp - mCal_16B_SC.Primary_APH_Leakage_air_enthalpy_at_air_inlet_temp) / (mCal_16B_SC.Primary_Flue_gas_outlet__temp_with_leakage - mCal_16B_SC.Air_temperature_at_inlet);

            //=(D45-D44)/(21-D45)*0.9*100
            mCal_16B_SC.Air_leakage_percentage_in_operating_case = (mCal_16B_SC.O2_in_flue_gas_at_Primary_APH_outlet_dry_basis - mCal_16B_SC.O2_in_flue_gas_at_Secondary_APH_inlet) / (21 - mCal_16B_SC.O2_in_flue_gas_at_Secondary_APH_outlet_dry_basis) * 0.9 * 100;

            //=(D8*F60)/100+D8
            mCal_16B_SC.Flue_gas_flow_rate_at_APH_outlet_calculated = (mCal_16B_SC.Flue_gas_flow_rate_at_primary_APH_inlet * mCal_16B_SC.Air_leakage_percentage_in_operating_case) / 100 + mCal_16B_SC.Flue_gas_flow_rate_at_primary_APH_inlet;

            //=F60/100*F59/D47*(D19-D23)+D19
            mCal_16B_SC.Flue_gas_outlet__temp_without_leakage_calculated = mCal_16B_SC.Air_leakage_percentage_in_operating_case / 100 * mCal_16B_SC.Mean_specific_heat_of_air_btw_air_inlet_and_flue_gas_outlet_temp_kJ_kgK / mCal_16B_SC.Secondary_APH__Mean_specific_heat_of_fluegas_at_flue_gas_outlet_temp_kJ_kgK * (mCal_16B_SC.Primary_Flue_gas_outlet__temp_with_leakage - mCal_16B_SC.Primary_air_inlet_temp) + mCal_16B_SC.Primary_Flue_gas_outlet__temp_with_leakage;

            //=D20-F82
            mCal_16B_SC.Gas_drop_without_leakage = mCal_16B_SC.Secondary_APH_Flue_gas_inlet_temp - mCal_16B_SC.Flue_gas_outlet__temp_without_leakage_calculated;

            //=F58-F57
            mCal_16B_SC.Air_side_temp_rise = mCal_16B_SC.Air_temperature_at_outlet - mCal_16B_SC.Air_temperature_at_inlet;

            //=D17-F57
            mCal_16B_SC.Temp_head = mCal_16B_SC.Primary_APH_Flue_gas_inlet_temp - mCal_16B_SC.Air_temperature_at_inlet;

            //=F63/F64
            mCal_16B_SC.Heat_capcity_ratio = mCal_16B_SC.Gas_drop_without_leakage / mCal_16B_SC.Air_side_temp_rise;

            //=F64/$F$65*100
            mCal_16B_SC.Air_side_efficiency = mCal_16B_SC.Air_side_temp_rise / mCal_16B_SC.Temp_head * 100;

            //=F63/$F$65*100
            mCal_16B_SC.Gas_side_efficiency = mCal_16B_SC.Gas_drop_without_leakage / mCal_16B_SC.Temp_head * 100;

            ////=(F68/F66+(24.5*(F66-$D$49)))*$D$49
            double val = (mCal_16B_SC.Gas_side_efficiency / mCal_16B_SC.Heat_capcity_ratio);
            double val1 = mCal_16B_SC.Heat_capcity_ratio - mCal_16B_SC.APH_design_capcity_ratio;
            double val2 = 24.5 * val1;
            double val3 = val + val2;
            mCal_16B_SC.Gas_side_efficiency_corrected_for_change_In_X_from_design = (val + (24.5 * val1)) * mCal_16B_SC.APH_design_capcity_ratio;

            //  { 91.58 + [24.5 * -0.07] } * 0.7
            //    {91.58 - 1.715}  * 0.7
            //   89.865 * 0.7
            //   62.90




            //=D9-D8
            mCal_16B_SC.Fluegas_side_leakage = mCal_16B_SC.Flue_gas_flow_rate_at_primary_APHoutlet - mCal_16B_SC.Flue_gas_flow_rate_at_primary_APH_inlet;

            //
            //mCal_16B_SC.Primary_air_side_leakage = 

            mCal_16B_SC.Air_temperature_at_inlet_2 = mCal_16B_SC.Secondary_air_inlet_temp;

            mCal_16B_SC.Air_temperature_at_outlet_2 = mCal_16B_SC.Secondary_air_outlet_temp;

            // =(D39-D40)/(D22-F77)
            mCal_16B_SC.Mean_specific_heat_of_air_btw_air_inlet_and_flue_gas_outlet_temp_kJ_kgK_2 = (mCal_16B_SC.Secondary__APH_Leakage_air_enthalpy_at_flue_gas_outlet_temp - mCal_16B_SC.Seconday__APH_Leakage_air_enthalpy_at_air_inlet_temp) / (mCal_16B_SC.Secondary_Flue_gas_outlet__temp_with_leakage - mCal_16B_SC.Air_temperature_at_inlet_2);

            //=(D45-D44)/(21-D45)*0.9*100
            mCal_16B_SC.Air_leakage_percentage_in_operating_case_2 = (mCal_16B_SC.O2_in_flue_gas_at_Secondary_APH_outlet_dry_basis - mCal_16B_SC.O2_in_flue_gas_at_Secondary_APH_inlet) / (21 - mCal_16B_SC.O2_in_flue_gas_at_Secondary_APH_outlet_dry_basis) * 0.9 * 100;

            //=(D10*F80)/100+D10
            mCal_16B_SC.Flue_gas_flow_rate_at_APH_outlet_calculated_2 = (mCal_16B_SC.Flue_gas_flow_rate_at_secondary_APH_inlet * mCal_16B_SC.Air_leakage_percentage_in_operating_case_2) / 100 + mCal_16B_SC.Flue_gas_flow_rate_at_secondary_APH_inlet;

            //=F80/100*F79/D47*(D22-D24)+D22
            double m = mCal_16B_SC.Secondary_Flue_gas_outlet__temp_with_leakage - mCal_16B_SC.Secondary_air_inlet_temp;
            mCal_16B_SC.Flue_gas_outlet__temp_without_leakage_calculated_2 = mCal_16B_SC.Air_leakage_percentage_in_operating_case_2 / 100 * mCal_16B_SC.Mean_specific_heat_of_air_btw_air_inlet_and_flue_gas_outlet_temp_kJ_kgK_2 / mCal_16B_SC.Secondary_APH__Mean_specific_heat_of_fluegas_at_flue_gas_outlet_temp_kJ_kgK * m + mCal_16B_SC.Secondary_Flue_gas_outlet__temp_with_leakage;

            //=D20-F82
            mCal_16B_SC.Gas_drop_without_leakage_2 = mCal_16B_SC.Secondary_APH_Flue_gas_inlet_temp - mCal_16B_SC.Flue_gas_outlet__temp_without_leakage_calculated_2;

            //=F78-F77
            mCal_16B_SC.Air_side_temp_rise_2 = mCal_16B_SC.Air_temperature_at_outlet_2 - mCal_16B_SC.Air_temperature_at_inlet_2;

            //=D20-F77
            mCal_16B_SC.Temp_head_2 = mCal_16B_SC.Secondary_APH_Flue_gas_inlet_temp - mCal_16B_SC.Air_temperature_at_inlet_2;

            //=F83/F84
            mCal_16B_SC.Heat_capcity_ratio_2 = mCal_16B_SC.Gas_drop_without_leakage_2 / mCal_16B_SC.Air_side_temp_rise_2;

            //=F84/$F$85*100
            mCal_16B_SC.Air_side_efficiency_2 = mCal_16B_SC.Air_side_temp_rise_2 / mCal_16B_SC.Temp_head_2 * 100;

            //=F83/$F$85*100
            mCal_16B_SC.Gas_side_efficiency_2 = mCal_16B_SC.Gas_drop_without_leakage_2 / mCal_16B_SC.Temp_head_2 * 100;

            //=(F88/F86+(24.5*(F86-$D$49)))*$D$49
            //mCal_16B_SC.Gas_side_efficiency_corrected_for_change_In_X_from_design_2 = (mCal_16B_SC.Gas_side_efficiency_2 / mCal_16B_SC.Heat_capcity_ratio_2 + (24.5 * (mCal_16B_SC.Heat_capcity_ratio_2 - mCal_16B_SC.APH_design_capcity_ratio))) * mCal_16B_SC.APH_design_capcity_ratio;

            //=D11-D10
            mCal_16B_SC.Fluegas_side_leakage_2 = mCal_16B_SC.Flue_gas_flow_rate_at_secondary_APH_outlet - mCal_16B_SC.Flue_gas_flow_rate_at_secondary_APH_inlet;


            //SqlConnection con1 = new SqlConnection(constr);
            //SqlCommand cmd1 = new SqlCommand("[dbo].[spr_Get_16B_Parameters]");
            //cmd1.CommandType = CommandType.StoredProcedure;
            //cmd1.Connection = con1;
            //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            //DataTable dt1 = new DataTable();
            //sda1.Fill(dt1);


            for (int i = 1; i < 30;i++)
            {
                int pid = i;

                switch (pid)
                {
                    case 1:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Air_temperature_at_inlet.ToString(), pid);

                        break;


                    case 2:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Air_temperature_at_outlet.ToString(), pid);

                        break;


                    case 3:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Mean_specific_heat_of_air_btw_air_inlet_and_flue_gas_outlet_temp_kJ_kgK.ToString(), pid);

                        break;

                    case 4:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Air_leakage_percentage_in_operating_case.ToString(), pid);

                        break;

                    case 5:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Flue_gas_flow_rate_at_APH_outlet_calculated.ToString(), pid);

                        break;

                    case 6:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Flue_gas_outlet__temp_without_leakage_calculated.ToString(), pid);

                        break;


                    case 7:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Gas_drop_without_leakage.ToString(), pid);

                        break;

                    case 8:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Air_side_temp_rise.ToString(), pid);

                        break;

                    case 9:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Temp_head.ToString(), pid);

                        break;

                    case 10:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Heat_capcity_ratio.ToString(), pid);

                        break;

                    case 11:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Air_side_efficiency.ToString(), pid);

                        break;

                    case 12:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Gas_side_efficiency.ToString().ToString(), pid);

                        break;


                    case 13:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Gas_side_efficiency_corrected_for_change_In_X_from_design.ToString(), pid);

                        break;


                    case 14:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Fluegas_side_leakage.ToString(), pid);

                        break;

                    case 15:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Primary_air_side_leakage.ToString(), pid);

                        break;

                    case 16:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID,  BoilerLoad, ObjectiveID, mCal_16B_SC.Air_temperature_at_inlet_2.ToString(), pid);

                        break;

                    case 17:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Air_temperature_at_outlet_2.ToString(), pid);

                        break;

                    case 18:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Mean_specific_heat_of_air_btw_air_inlet_and_flue_gas_outlet_temp_kJ_kgK_2.ToString(), pid);



                        break;

                    case 19:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Air_leakage_percentage_in_operating_case_2.ToString(), pid);

                        break;

                    case 20:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Flue_gas_flow_rate_at_APH_outlet_calculated_2.ToString(), pid);

                        break;

                    case 21:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID,  BoilerLoad, ObjectiveID, mCal_16B_SC.Flue_gas_outlet__temp_without_leakage_calculated_2.ToString(), pid);

                        break;

                    case 22:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Gas_drop_without_leakage_2.ToString(), pid);

                        break;

                    case 23:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Air_side_temp_rise_2.ToString(), pid);

                        break;

                    case 24:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Temp_head_2.ToString(), pid);

                        break;

                    case 25:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID,  BoilerLoad, ObjectiveID, mCal_16B_SC.Heat_capcity_ratio_2.ToString(), pid);

                        break;



                    case 26:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID,  BoilerLoad, ObjectiveID, mCal_16B_SC.Air_side_efficiency_2.ToString(), pid);

                        break;

                    case 27:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Gas_side_efficiency_2.ToString(), pid);

                        break;

                    case 28:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, mCal_16B_SC.Gas_side_efficiency_corrected_for_change_In_X_from_design_2.ToString(), pid);

                        break;

                    case 29:

                        mCal_16B_BLL.Insert_16B_Calculation(BoilerID, ProjectID,BoilerLoad, ObjectiveID, mCal_16B_SC.Fluegas_side_leakage_2.ToString(), pid);

                        break;

                }

            }
        }
    }
}
