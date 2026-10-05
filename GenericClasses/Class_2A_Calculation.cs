using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BoilerModellingTool.BLL;
using System.IO;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;

namespace GenericClasses
{
    public class Class_2A_Calculation
    {
        //string ProjectID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
        //string BoilerID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
        //string BoilerLoad = "100%TMCR";
        //int ObjectiveID = 1;


        string BoilerID = null;
        string ProjectID = null;
        string SectionID = null;
        string BoilerLoad = null;
        int ObjectiveID = 0;


        public Class_2A_Calculation(string BoilerID, string ProjectID,
             string BoilerLoad ,int ObjectiveID)
        {
            
            this.BoilerID = BoilerID;
            this.ProjectID = ProjectID;          
            this.BoilerLoad = BoilerLoad;
            this.ObjectiveID = ObjectiveID;

        }

        public void Calculation_2A_Class()
        {
            Cal_2A_SC mCal_2A_SC = null;
            Cal_2A_BLL mCal_2A_BLL = null;
            try
            {
                mCal_2A_SC = new Cal_2A_SC();
                mCal_2A_BLL = new Cal_2A_BLL();
                mCal_2A_SC = mCal_2A_BLL.GetInput_For_2A_Calculation(BoilerID, ProjectID, BoilerLoad,ObjectiveID);
                Double VRO2 = Convert.ToDouble(mCal_2A_SC.Theoretical_volume_of_CO2_and_SO2);
                Double VN2 = Convert.ToDouble(mCal_2A_SC.Theoretical_volume_of_N2);
                Double VH2O = Convert.ToDouble(mCal_2A_SC.Theoretical_volume_of_H2O);
                Double Gfa = Convert.ToDouble(mCal_2A_SC.Fly_ash_concentration);
                Double Alpha = Convert.ToDouble(mCal_2A_SC.Excess_air_ratio);
                Double Voa = Convert.ToDouble(mCal_2A_SC.Theoretical_volume_of_air);

                DataSet dt = null;

                Cal_2A_BLL mCal_2A_BLL1 = null;

                mCal_2A_BLL1 = new Cal_2A_BLL();
                dt = mCal_2A_BLL.GetCCO2FromEnthalpyTable();

                for (int i = 0; i <= dt.Tables[0].Rows.Count-1; i++)
                {
                    int EnthalpyTableID = Convert.ToInt32(dt.Tables[0].Rows[i]["EnthalpyTableID"].ToString());
                    Double CCO2 = Convert.ToDouble(dt.Tables[0].Rows[i]["CCO2θ(kJ/Nm3)"].ToString());
                    Double IRO2 = CCO2 * VRO2;
                    Double CN2 = Convert.ToDouble(dt.Tables[0].Rows[i]["CN2θ(kJ/Nm3)"].ToString());
                    Double IN2 = CN2 * VN2;
                    Double CH2O = Convert.ToDouble(dt.Tables[0].Rows[i]["CH2Oθ(kJ/Nm3)"].ToString());
                    Double IH2O = CH2O * VH2O;
                    Double Cfa = Convert.ToDouble(dt.Tables[0].Rows[i]["Cfaθ(kJ/kg ash)"].ToString());
                    Double Ifa = Cfa * Gfa;
                    Double Ca = Convert.ToDouble(dt.Tables[0].Rows[i]["Caθ(kJ/Nm3)"].ToString());
                    Double Ioa = Voa * Ca;
                    Double log = IRO2 + IN2 + IH2O + Ifa;//Refer it as Enthalpy
                    Double AlphaValue = log + (Alpha - 1) * Ioa;
                    mCal_2A_SC.EnthalpyID = EnthalpyTableID.ToString();
                    mCal_2A_SC.IRO2 = IRO2.ToString();
                    mCal_2A_SC.IN2 = IN2.ToString();
                    mCal_2A_SC.IH2O = IH2O.ToString();
                    mCal_2A_SC.Ifa = Ifa.ToString();
                    mCal_2A_SC.Ioa = Ioa.ToString();
                    mCal_2A_SC.Iog = log.ToString();
                    mCal_2A_SC.NewAlpha = AlphaValue.ToString();
                    mCal_2A_SC.LowerFurnaceAlpha = AlphaValue.ToString();
                    mCal_2A_SC.UpperFurnaceAlpha = AlphaValue.ToString();
                    mCal_2A_SC.CrossDuctAlpha = AlphaValue.ToString();
                    mCal_2A_SC.ReverseChamberAlpha = AlphaValue.ToString();
                    mCal_2A_SC.BackpassAlpha = AlphaValue.ToString();
                    mCal_2A_BLL1.Insert_EnthalpyTableForTempZeroCalculatedValue(mCal_2A_SC, BoilerID, ProjectID, BoilerLoad,ObjectiveID);

                   
                }
                btn_flue_gas_enthalpy();
            }
            catch
            {
                //ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", "alert('Error in 2A calculation!');", true);
            }
        }
       
        public void btn_flue_gas_enthalpy()
        {
            Cal_2A_SC mCal_2A_SC = null;
            Cal_2A_SC mCal_2A_SC1 = null;
            Cal_2A_BLL mCal_2A_BLL = null;
            Cal_2A_BLL mCal_2A_BLL1 = null;
            Double Reqd_Temp = 25.0;

            try
            {
                mCal_2A_SC = new Cal_2A_SC();
                mCal_2A_BLL = new Cal_2A_BLL();
                DataSet dt = null;
                dt = mCal_2A_BLL.Get_Nearest_Max_Min_EnthalpyCalculation(BoilerID, ProjectID, Reqd_Temp, BoilerLoad,ObjectiveID);

                Double Min_Temp = Convert.ToDouble(dt.Tables[0].Rows[0]["Temp(Deg C)"].ToString());
                Double Min_Enthalpy_FlueGas = Convert.ToDouble(dt.Tables[0].Rows[0]["Iog(kJ/kg)"].ToString());
                Double Min_Enthalpy_Air = Convert.ToDouble(dt.Tables[0].Rows[0]["Ioa(kJ/kg)"].ToString());

                DataTable mDTable = null;
                mDTable = dt.Tables[1];

                Double Max_Temp = Convert.ToDouble(mDTable.Rows[0]["Temp(Deg C)"].ToString());
                Double Max_Enthalpy_FlueGas = Convert.ToDouble(mDTable.Rows[0]["Iog(kJ/kg)"].ToString());
                Double Max_Enthalpy_Air = Convert.ToDouble(mDTable.Rows[0]["Ioa(kJ/kg)"].ToString());

                Double Reference_temp_Theoretical_Flue_gas = (Max_Enthalpy_FlueGas - Min_Enthalpy_FlueGas) / (Max_Temp - Min_Temp) * (Reqd_Temp - Min_Temp) + Min_Enthalpy_FlueGas;
                Double Reference_temp_Air = (Max_Enthalpy_Air - Min_Enthalpy_Air) / (Max_Temp - Min_Temp) * (Reqd_Temp - Min_Temp) + Min_Enthalpy_Air;

                DataSet dataset25Enthalpy = null;
                DataTable Excess_air_ratio = null;
                DataTable Fuel_firing_rate = null;
                DataTable data_IOA_IOG = null;
                DataTable Flue_gas_flowrate = null;
                DataTable Theoritical_Air_Req = null;
                DataTable Ricirculated_gas = null;

                dataset25Enthalpy = mCal_2A_BLL.GetInput_For_25_Enthalpy_Calculation(BoilerID, ProjectID, BoilerLoad,ObjectiveID);

                data_IOA_IOG = dataset25Enthalpy.Tables[0];
                Excess_air_ratio = dataset25Enthalpy.Tables[3];
                Fuel_firing_rate = dataset25Enthalpy.Tables[1];
                Flue_gas_flowrate = dataset25Enthalpy.Tables[4];//270
                Theoritical_Air_Req = dataset25Enthalpy.Tables[6];
                Ricirculated_gas = dataset25Enthalpy.Tables[5];

                Double Input_1A_Excess_Air_Ratio = Convert.ToDouble(Excess_air_ratio.Rows[0]["Value"].ToString());
                Double Reciculated= Convert.ToDouble(Ricirculated_gas.Rows[0]["PIINPUT"].ToString());

                for (int i = 0; i < data_IOA_IOG.Rows.Count; i++)
                {
                    Double IOG_25_Enthalpy = Convert.ToDouble(data_IOA_IOG.Rows[i]["Iog(kJ/kg)"].ToString()) - Reference_temp_Theoretical_Flue_gas;
                    Double IOA_25_Enthalpy = Convert.ToDouble(data_IOA_IOG.Rows[i]["Ioa(kJ/kg)"].ToString()) - Reference_temp_Air;
                    Double Lower_F_Alpha = IOG_25_Enthalpy + (Input_1A_Excess_Air_Ratio - 1) * IOA_25_Enthalpy * (1 + Reciculated / 100);
                   // =(B63+($D$61-1)*C63)*$F$13/$F$14
                    Double APH_Flue_gas_Enthalpy = (IOG_25_Enthalpy + (Input_1A_Excess_Air_Ratio - 1) * IOA_25_Enthalpy) * (Convert.ToDouble(Fuel_firing_rate.Rows[0]["Value"].ToString())/3.6) / (Convert.ToDouble(Flue_gas_flowrate.Rows[0]["Value"].ToString()));
                    //=C63*$F$13/$F$15
                    Double APH_Air_Enthalpy = IOA_25_Enthalpy * (Convert.ToDouble(Fuel_firing_rate.Rows[0]["Value"].ToString())/3.6) / (Convert.ToDouble(Theoritical_Air_Req.Rows[0]["Value"].ToString()));
                    int EnthalpyID = Convert.ToInt16(data_IOA_IOG.Rows[i]["EnthalpyTableID"].ToString());

                    mCal_2A_SC.Iog = IOG_25_Enthalpy.ToString();
                    mCal_2A_SC.Ioa = IOA_25_Enthalpy.ToString();
                    mCal_2A_SC.LowerFurnaceAlpha = Lower_F_Alpha.ToString();
                    mCal_2A_SC.UpperFurnaceAlpha = Lower_F_Alpha.ToString();
                    mCal_2A_SC.CrossDuctAlpha = Lower_F_Alpha.ToString();
                    mCal_2A_SC.NewAlpha = Lower_F_Alpha.ToString();
                    mCal_2A_SC.ReverseChamberAlpha = Lower_F_Alpha.ToString();
                    mCal_2A_SC.BackpassAlpha = Lower_F_Alpha.ToString();
                    mCal_2A_SC.APH_Air_Enthalpy = APH_Air_Enthalpy.ToString();
                    mCal_2A_SC.APH_Flue_gas_Enthalpy = APH_Flue_gas_Enthalpy.ToString();
                    mCal_2A_SC.EnthalpyID = EnthalpyID.ToString();

                    mCal_2A_BLL1 = new Cal_2A_BLL();
                    mCal_2A_BLL1.Insert_EnthalpyTableForTemp25CalculatedValue(mCal_2A_SC, BoilerID, ProjectID, BoilerLoad,ObjectiveID);

                }

                mCal_2A_SC1 = new Cal_2A_SC();

                DataSet ds_APH_Cal = null;
                ds_APH_Cal = mCal_2A_BLL.select_APH_Calculation(BoilerID, ProjectID, BoilerLoad,ObjectiveID);

                for (int i = 0; i < ds_APH_Cal.Tables[0].Rows.Count; i++)
                {

                    if (i == 0)
                    {
                        Double APH_Flue_gas = Convert.ToDouble(ds_APH_Cal.Tables[0].Rows[i]["APH_Flue_gas_Enthalpy"].ToString());
                        Double APH_Air = Convert.ToDouble(ds_APH_Cal.Tables[0].Rows[i]["APH_Air_Enthalpy"].ToString());
                        Double Temp_deg = Convert.ToDouble(ds_APH_Cal.Tables[0].Rows[i]["Temp(Deg C)"].ToString());
                        int EnthalyT_ID = Convert.ToInt16(ds_APH_Cal.Tables[0].Rows[i]["EnthalpyTableID"].ToString());
                        Double Cp_flue_gas = (APH_Flue_gas - 0) / (Temp_deg - Reqd_Temp);
                        Double Cp_air = (APH_Air - 0) / (Temp_deg - Reqd_Temp);
                        mCal_2A_SC1.Cp_flue_gas = Cp_flue_gas.ToString();
                        mCal_2A_SC1.Cp_air = Cp_air.ToString();
                        mCal_2A_SC1.EnthalpyID = EnthalyT_ID.ToString();
                        mCal_2A_BLL.Update_CP_Flue_AND_Air_Gas(mCal_2A_SC1, BoilerID, ProjectID, BoilerLoad,ObjectiveID);

                    }
                    else
                    {
                        for (int j = i; j < ds_APH_Cal.Tables[0].Rows.Count; j++)
                        {

                            Double APH_Flue_gas = Convert.ToDouble(ds_APH_Cal.Tables[0].Rows[i - 1]["APH_Flue_gas_Enthalpy"].ToString());
                            Double APH_Air = Convert.ToDouble(ds_APH_Cal.Tables[0].Rows[i - 1]["APH_Air_Enthalpy"].ToString());
                            Double Temp_deg = Convert.ToDouble(ds_APH_Cal.Tables[0].Rows[i - 1]["Temp(Deg C)"].ToString());
                            Double APH_Flue_gas1 = Convert.ToDouble(ds_APH_Cal.Tables[0].Rows[j]["APH_Flue_gas_Enthalpy"].ToString());
                            Double APH_Air1 = Convert.ToDouble(ds_APH_Cal.Tables[0].Rows[j]["APH_Air_Enthalpy"].ToString());
                            Double Temp_deg1 = Convert.ToDouble(ds_APH_Cal.Tables[0].Rows[j]["Temp(Deg C)"].ToString());
                            int EnthalyT_ID = Convert.ToInt16(ds_APH_Cal.Tables[0].Rows[j]["EnthalpyTableID"].ToString());
                            Double Cp_flue_gas = (APH_Flue_gas1 - APH_Flue_gas) / (Temp_deg1 - Temp_deg);
                            Double Cp_air = (APH_Air1 - APH_Air) / (Temp_deg1 - Temp_deg);
                            mCal_2A_SC1.Cp_flue_gas = Cp_flue_gas.ToString();
                            mCal_2A_SC1.Cp_air = Cp_air.ToString();
                            mCal_2A_SC1.EnthalpyID = EnthalyT_ID.ToString();
                            mCal_2A_BLL.Update_CP_Flue_AND_Air_Gas(mCal_2A_SC1, BoilerID, ProjectID, BoilerLoad,ObjectiveID);
                        }
                    }

                }
            }
            catch
            {

            }
        }
        
    }
}
