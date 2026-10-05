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
using System.Web;
using System.Web.UI.WebControls;
using System.Windows;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;

namespace GenericClasses
{
    public class Class_1A_Calculation
    {
        #region " Properties "

        public Cal_1A_SC Doc { get; set; }
        public Cal_1A_BLL BLL { get; set; }
        //public Cal_5B_SC Doc1 { get; set; }

        #endregion

        public DataTable dtgrid = new DataTable();

        //string ProjectID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
        //string BoilerID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
        //string BoilerLoad = "100%TMCR";
        //int ObjectiveID = 1;


        public double FC, VM, Ash, Moisture, Sulphur;
        public double FC1, VM1, C1, C, H, N, O;
        public double Ambient_temperature, Relative_humidity;

        string BoilerID = null;
        string ProjectID = null;
        string SectionID = null;
        string BoilerLoad = null;
        int ObjectiveID = 0;
      
       
        public Class_1A_Calculation(string BoilerID, string ProjectID,
             string BoilerLoad ,int ObjectiveID)
        {
            
            this.BoilerID = BoilerID;
            this.ProjectID = ProjectID;          
            this.BoilerLoad = BoilerLoad;
            this.ObjectiveID = ObjectiveID;

        }             

     
        public void Calculation_1A_Class()
        {
            try
            {
                UltimateAnalysisConversion();
                SpecificHumidityCalculation();
            }
            catch(Exception e)
            {
                //ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", "alert('Error in 1A calculation!');", true);
            }

        }
       
        public void UltimateAnalysisConversion()
        {
            Cal_1A_BLL mCal_1A_BLL = new Cal_1A_BLL();
            Cal_1A_SC cal_1A_SC = new Cal_1A_SC();
            DataSet dt = new DataSet();
            dt = mCal_1A_BLL.GetFuelParameterForUltimate(ProjectID, BoilerID, BoilerLoad, ObjectiveID);

         
            FC = Convert.ToDouble(dt.Tables[0].Rows[0][0].ToString());       //Fixed_Carbon
            VM = Convert.ToDouble(dt.Tables[0].Rows[1][0].ToString());     //Volatile_Matter
            Ash = Convert.ToDouble(dt.Tables[0].Rows[2][0].ToString());
            Moisture = Convert.ToDouble(dt.Tables[0].Rows[3][0].ToString());
            Sulphur = Convert.ToDouble(dt.Tables[0].Rows[4][0].ToString());

            FC1 = Math.Round(FC / (FC + VM) * 100, 2, MidpointRounding.ToEven);    //FixedCarbon_Modified1        //=K8/(K8+K9)*100

            VM1 = Math.Round(VM / (FC + VM) * 100, 2, MidpointRounding.ToEven);    //Volatile_Matter_Modified1    //=K9/(K8+K9)*100


            C1 = Math.Round((FC1 + 0.9 * (VM1 - 14)) * (FC + VM) / 100, 2, MidpointRounding.ToEven);       //=(K14+0.9*(K15-14))*(K8+K9)/100

            C = Math.Round(C1 - Sulphur, 2, MidpointRounding.ToEven);     //=K17-K12

            H = Math.Round((VM1 * (7.35 / (VM1 + 10) - 0.013)) * (FC + VM) / 100, 2, MidpointRounding.ToEven);   //=(K15*(7.35/(K15+10)-0.013))*(K8+K9)/100

            N = Math.Round((2.1 - 0.012 * VM1) * (FC + VM) / 100, 2, MidpointRounding.ToEven);       //=(2.1-0.012*K15)*(K8+K9)/100

            O = Math.Round(100 - (C + H + N + Sulphur + Ash + Moisture), 2, MidpointRounding.ToEven);      //=100-(K18+K20+K22+K12+K10+K11)

        }

        public void SpecificHumidityCalculation()       //K18
        {
           
            DataSet dt = new DataSet();
            Cal_1A_BLL BLL = new Cal_1A_BLL();
            dt = BLL.GetAirTempParameterForSpecificHumidity(ProjectID, BoilerID, BoilerLoad, ObjectiveID);

            Ambient_temperature = Convert.ToDouble(dt.Tables[0].Rows[0][1].ToString());      //K28
            Relative_humidity = Convert.ToDouble(dt.Tables[0].Rows[1][1].ToString());       //K29

            double C1 = -5800.2206;     //K31
            double C2 = 1.3914993;      //K32
            double C3 = -0.048640239;       //K33           
            double C4 = 4.17648E-05;        //K34
            double C5 = -1.44521E-08;       //K35
            double C6 = 6.5459673;      //K36

            // Saturation pressure, Ps=EXP(($K$31/(K28+273.15))+$K$32+($K$33*(K28+273.15))+($K$34*(K28+273.15)^2)+($K$35*(K28+273.15)^3)+($K$36*LN(K28+273.15)))
            double Ps = Math.Exp((C1 / (Ambient_temperature + 273.15)) + C2 + (C3 * (Ambient_temperature + 273.15)) + (C4 * Math.Pow((Ambient_temperature + 273.15), 2)) + (C5 * Math.Pow((Ambient_temperature + 273.15), 3)) + (C6 * Math.Log(Ambient_temperature + 273.15)));
            double Saturation_pressure = Math.Round(Ps, 2, MidpointRounding.ToEven);      //K38

            // Water vapor pressure =K38*K29/100
            double Water_vapor_pressure = Math.Round(Saturation_pressure * Relative_humidity / 100, 2, MidpointRounding.ToEven);        //K40

            //Specific humidity=0.621945*K40/(101325-K40)
            double Specific_humidity = 0.621945 * Water_vapor_pressure / (101325 - Water_vapor_pressure); //D18

            //--------------------------------

            //Specific humidity of ambient air
            double Specific_humidity_of_ambient_air = Specific_humidity * 1.608;    //D29       
            Excess_air_check(Specific_humidity, Specific_humidity_of_ambient_air);

        }

        public void Excess_air_check(double Specific_humidity, double Specific_humidity_of_ambient_air)
        {

            //1. Excess Air Check

            //input data
           
            DataSet dt = new DataSet();
            Cal_1A_BLL BLL = new Cal_1A_BLL();
            dt = BLL.GetExcessAirCheck(ProjectID, BoilerID,BoilerLoad,ObjectiveID);
            double Carbon = Convert.ToDouble(dt.Tables[2].Rows[0][1].ToString());       //D8 
            double Hydrogen = Convert.ToDouble(dt.Tables[2].Rows[1][1].ToString());     //D9
            double Oxygen = Convert.ToDouble(dt.Tables[2].Rows[2][1].ToString());       //D10   
            double Nitrogen = Convert.ToDouble(dt.Tables[2].Rows[3][1].ToString());     //D11
            double Sulfur = Convert.ToDouble(dt.Tables[2].Rows[6][1].ToString());       //D12 
            double Ash = Convert.ToDouble(dt.Tables[2].Rows[4][1].ToString());          //D13
            double Moisture = Convert.ToDouble(dt.Tables[2].Rows[5][1].ToString());     //D14 
            double Higher_heating_value = Convert.ToDouble(dt.Tables[2].Rows[7][1].ToString());     //D15 
            double Fly_ash_fraction = Convert.ToDouble(dt.Tables[2].Rows[8][1].ToString());     //D17 
            double Volatile_matter_by_proximate_analysis = Convert.ToDouble(dt.Tables[2].Rows[9][1].ToString());        //D16 
            //double Mean_diameter_of_ash_particle = Convert.ToDouble(dt.Tables[2].Rows[10][1].ToString());
      



            double Design_fuel_consumption = Convert.ToDouble(dt.Tables[0].Rows[0][1].ToString()) / 3.6;     //D21  = F57    
            double Total_combustion_air = Convert.ToDouble(dt.Tables[0].Rows[1][1].ToString()) / 3.6;         // D22  
            double Design_flue_gas_flow = Convert.ToDouble(dt.Tables[0].Rows[3][1].ToString());         //D24

            
            //DataSet dt2 = new DataSet();
            //dt2 = BLL.Get_Objective(ProjectID, BoilerID);


            int flag = ObjectiveID;
            double Excess_air;

            if (flag == 2)
            {
                Excess_air = EA_Calculation(Specific_humidity_of_ambient_air);
            }
            else
            {
                Excess_air = Convert.ToDouble(dt.Tables[3].Rows[0][1].ToString());                   //D23 
            }

            //Trial purpose
            //Excess_air = 20.0;


            double Design_CO2_in_flue_gas_at_eco_Outlet = Convert.ToDouble(dt.Tables[1].Rows[0][1].ToString());     //D25    
            double Design_O2_in_flue_gas_at_eco_Outlet = Convert.ToDouble(dt.Tables[1].Rows[1][1].ToString());     //D26
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            /* Step 1  Get Paramter List
             * Step2 loop parameter list
             * Step 3  swithc case
             * step 4  switch case formula*/

            //Calculations 
            this.Doc = new Cal_1A_SC();

            //Working for D32
            //double rDouble = 0.0;
            //do
            //{

            //    //D31 
            //    this.Doc.Design_excess_air_ratio = Math.Round((Excess_air / 100 + 1), 2);     //=D23/100+1

            //    //D32
            //    this.Doc.Correction_factor_for_elemental_composition = Math.Round(rDouble, 2);

            //    //D33
            //    this.Doc.Carbon_modified = (Carbon - this.Doc.Correction_factor_for_elemental_composition) / (100 - Hydrogen - Nitrogen - Sulfur - Ash - Moisture - this.Doc.Correction_factor_for_elemental_composition) * (100 - Hydrogen - Nitrogen - Sulfur - Ash - Moisture);       //=(D8-D32)/(100-D9-D11-D12-D13-D14-D32)*(100-D9-D11-D12-D13-D14)

            //    //D34
            //    this.Doc.Oxygen_modified = (Oxygen) / (100 - Hydrogen - Nitrogen - Sulfur - Ash - Moisture - this.Doc.Correction_factor_for_elemental_composition) * (100 - Hydrogen - Nitrogen - Sulfur - Ash - Moisture);       //=(D10)/(100-D9-D11-D12-D13-D14-D32)*(100-D9-D11-D12-D13-D14)

            //    //D35
            //    this.Doc.Theoretical_flue_gas_flow_rate = 4.32 * ((this.Doc.Carbon_modified / 100) * 2.667 + ((Hydrogen / 100) * 8 - (this.Doc.Oxygen_modified / 100)) + (Sulfur / 100)) * Design_fuel_consumption * (1 + Specific_humidity);        //=4.32*((D33/100)*2.667+((D9/100)*8-(D34/100))+(D12/100))*F57*(1+D18)

            //    //D36
            //    this.Doc.Excess_air_ratio_calc_based_on_given_total_combustion_air = Total_combustion_air / this.Doc.Theoretical_flue_gas_flow_rate;     //=D22/D35



            //    if (this.Doc.Design_excess_air_ratio == this.Doc.Excess_air_ratio_calc_based_on_given_total_combustion_air)
            //    {
            //        this.Doc.Correction_factor_for_elemental_composition = Math.Round(rDouble, 2);
            //        break;

            //    }

            //    rDouble = rDouble + 0.1;


            //} while (rDouble <= 20.0);



            double rDouble = 18.7;
            do
            {

                //D31 
                this.Doc.Design_excess_air_ratio = (Excess_air / 100 + 1);     //=D23/100+1

                //D32
                this.Doc.Correction_factor_for_elemental_composition = Math.Round(rDouble, 2);

                //D33
                this.Doc.Carbon_modified = (Carbon - this.Doc.Correction_factor_for_elemental_composition) / (100 - Hydrogen - Nitrogen - Sulfur - Ash - Moisture - this.Doc.Correction_factor_for_elemental_composition) * (100 - Hydrogen - Nitrogen - Sulfur - Ash - Moisture);       //=(D8-D32)/(100-D9-D11-D12-D13-D14-D32)*(100-D9-D11-D12-D13-D14)

                //D34
                this.Doc.Oxygen_modified = (Oxygen) / (100 - Hydrogen - Nitrogen - Sulfur - Ash - Moisture - this.Doc.Correction_factor_for_elemental_composition) * (100 - Hydrogen - Nitrogen - Sulfur - Ash - Moisture);       //=(D10)/(100-D9-D11-D12-D13-D14-D32)*(100-D9-D11-D12-D13-D14)

                //D35
                this.Doc.Theoretical_flue_gas_flow_rate = 4.32 * ((this.Doc.Carbon_modified / 100) * 2.667 + ((Hydrogen / 100) * 8 - (this.Doc.Oxygen_modified / 100)) + (Sulfur / 100)) * Design_fuel_consumption * (1 + Specific_humidity);        //=4.32*((D33/100)*2.667+((D9/100)*8-(D34/100))+(D12/100))*F57*(1+D18)

                //D36
                this.Doc.Excess_air_ratio_calc_based_on_given_total_combustion_air = Total_combustion_air / this.Doc.Theoretical_flue_gas_flow_rate;     //=D22/D35


                rDouble = rDouble + 0.1;


            } while (Math.Round(Doc.Design_excess_air_ratio,2)!=Math.Round(Doc.Excess_air_ratio_calc_based_on_given_total_combustion_air,2));




            //----------------------------------------------------------------------------------------------------------------------------------


            // 2. HHV check

            //D39
            this.Doc.Higher_heating_value_ByDulongsformula = ((337 * this.Doc.Carbon_modified) + (1442 * (Hydrogen - (this.Doc.Oxygen_modified / 8))) + (93 * Sulfur));      // =((337*D33)+(1442*(D9-(D34/8)))+(93*D12));

            //D40
            this.Doc.Deviation_in_given_HHV_value_from_calculated_value = (Higher_heating_value - this.Doc.Higher_heating_value_ByDulongsformula) / this.Doc.Higher_heating_value_ByDulongsformula * 100;      //=(D15-D39)/D39*100;



            //-----------------------------------------------------------------------------------------------------------------------------------


            //LHV calculation
            //D43
            this.Doc.Lower_heating_value_on_as_received_basis = Higher_heating_value - (212 * Hydrogen + 24.5 * Moisture + 0.8 * this.Doc.Oxygen_modified);        //=D15-(212*D9+24.5*D14+0.8*D34)

            //D44
            this.Doc.Volatil_matter_on_dry_ash_free_basis = (Volatile_matter_by_proximate_analysis * 100) / (100 - Ash - Moisture);            //=(D16*100)/(100-D13-D14)



            //------------------------------------------------------------------------------------------------------------------------------------

            // Coal-Combustion Product Volumes and Theoretical Air Volumes
            //F48
            this.Doc.Theoretical_volume_of_air = 0.0889 * (this.Doc.Carbon_modified + 0.375 * Sulfur) + 0.265 * Hydrogen - 0.0333 * this.Doc.Oxygen_modified;       //=0.0889*(D33+0.375*D12)+0.265*D9-0.0333*D34

            //F49
            this.Doc.Theoretical_volume_of_N2 = 0.79 * this.Doc.Theoretical_volume_of_air + 0.8 * Nitrogen / 100;        //=0.79*F48+0.8*D11/100

            //F50
            this.Doc.Theoretical_volume_of_water_vapour = 0.111 * Hydrogen + 0.0124 * Moisture + Specific_humidity_of_ambient_air * this.Doc.Theoretical_volume_of_air;      //=0.111*D9+0.0124*D14+D29*F48

            //F51
            this.Doc.Theoretical_volume_of_CO2 = 1.866 * (this.Doc.Carbon_modified + 0.375 * Sulfur) / 100;       //=1.866*(D33+0.375*D12)/100

            //F52
            this.Doc.Theoretical_flue_gas_volume = this.Doc.Theoretical_volume_of_CO2 + this.Doc.Theoretical_volume_of_N2 + this.Doc.Theoretical_volume_of_water_vapour;     //=F51+F49+F50

            //F53
            this.Doc.Fly_ash_concentration = Ash * Fly_ash_fraction / 100;       //=D13*D17/100






            //-----------------------------------------------------------------------------------------------------------------------------------

            // Flue Gas Characteristics

            //F57
            this.Doc.Design_fuel_consumption_ForFluegas = Design_fuel_consumption;     //=D21


            //F59
            this.Doc.Mass_flow_rate_of_Total_Combustion_air = Total_combustion_air;      //=D22


            //61    //F61
            this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace = 1 + (Excess_air / 100);        //=1+(D23/100) apply this same value to Furnace,cross duct.... all 5 boiler parts 

            //F58
            this.Doc.Theoretical_air_required = this.Doc.Mass_flow_rate_of_Total_Combustion_air / this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace;    //=F59/F61


            //F60
            this.Doc.Total_excess_air = (this.Doc.Mass_flow_rate_of_Total_Combustion_air - this.Doc.Theoretical_air_required);                            //=(F59-F58)


            //-------------------------------------------------------------------------------------------
            ////61    //F61
            //double this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace = 1 + (Excess_air / 100);        //=1+(D23/100) apply this same value to Furnace,cross duct.... all 5 boiler parts 

            //G61
            this.Doc.Excess_air_coefficient_of_flue_inlet_Upperfurnace = this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace;   //F61

            //H61
            this.Doc.Excess_air_coefficient_of_flue_inlet_CrossDuct = this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace;       //F61

            //I61
            this.Doc.Excess_air_coefficient_of_flue_inlet_ReverseChamber = this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace;     //F61 

            //J61
            this.Doc.Excess_air_coefficient_of_flue_inlet_BackPass = this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace;           //F61




            //-------------------------------------------------------------------------------------------

            //F62
            this.Doc.Excess_air_coefficient_of_flue_outlet_LowerFurnace = this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace;       //F61 
            this.Doc.Excess_air_coefficient_of_flue_outlet_UpperFurnace = this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace;
            this.Doc.Excess_air_coefficient_of_flue_outlet_CrossDuct = this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace;
            this.Doc.Excess_air_coefficient_of_flue_outlet_ReverseChamber = this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace;
            this.Doc.Excess_air_coefficient_of_flue_outlet_BackPass = this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace;




            //-------------------------------------------------------------------------------------------

            //F63
            this.Doc.Average_excess_air_coefficient_of_flue_LowerFurnace = (this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace + this.Doc.Excess_air_coefficient_of_flue_outlet_LowerFurnace) / 2;      //=(F61+F62)/2    to all 5 
            this.Doc.Average_excess_air_coefficient_of_flue_UpperFurnace = (this.Doc.Excess_air_coefficient_of_flue_inlet_Upperfurnace + this.Doc.Excess_air_coefficient_of_flue_outlet_UpperFurnace) / 2;     //=(G61+G62)/2
            this.Doc.Average_excess_air_coefficient_of_flue_CrossDuct = (this.Doc.Excess_air_coefficient_of_flue_inlet_CrossDuct + this.Doc.Excess_air_coefficient_of_flue_outlet_CrossDuct) / 2;        //=(H61+H62)/2
            this.Doc.Average_excess_air_coefficient_of_flue_ReverseChamber = (this.Doc.Excess_air_coefficient_of_flue_inlet_ReverseChamber + this.Doc.Excess_air_coefficient_of_flue_outlet_ReverseChamber) / 2;   //=(I61+I62)/2
            this.Doc.Average_excess_air_coefficient_of_flue_BackPass = (this.Doc.Excess_air_coefficient_of_flue_inlet_BackPass + this.Doc.Excess_air_coefficient_of_flue_outlet_BackPass) / 2;         //=(J61+J62)/2

            //-------------------------------------------------------------------------------------------


            //F64
            this.Doc.Excess_air_volume_LowerFurnace = (this.Doc.Average_excess_air_coefficient_of_flue_LowerFurnace - 1) * this.Doc.Theoretical_volume_of_air;                           // =(F63-1)*F48 
            this.Doc.Excess_air_volume_UpperFurnace = (this.Doc.Average_excess_air_coefficient_of_flue_UpperFurnace - 1) * this.Doc.Theoretical_volume_of_air;                           //=(G63-1)*F48
            this.Doc.Excess_air_volume_CrossDuct = (this.Doc.Average_excess_air_coefficient_of_flue_CrossDuct - 1) * this.Doc.Theoretical_volume_of_air;                           //=(H63-1)*F48
            this.Doc.Excess_air_volume_ReverseChamber = (this.Doc.Average_excess_air_coefficient_of_flue_ReverseChamber - 1) * this.Doc.Theoretical_volume_of_air;                          //=(I63-1)*F48
            this.Doc.Excess_air_volume_BackPass = (this.Doc.Average_excess_air_coefficient_of_flue_BackPass - 1) * this.Doc.Theoretical_volume_of_air;                         //=(J63-1)*F48

            //-------------------------------------------------------------------------------------------

            //F65
            this.Doc.Water_vapour_volume_LowerFurnace = this.Doc.Theoretical_volume_of_water_vapour + Specific_humidity_of_ambient_air * (this.Doc.Average_excess_air_coefficient_of_flue_LowerFurnace - 1) * this.Doc.Theoretical_volume_of_air;                         //=$F$50+$D$29*(F63-1)*$F$48
            this.Doc.Water_vapour_volume_UpperFurnace = this.Doc.Theoretical_volume_of_water_vapour + Specific_humidity_of_ambient_air * (this.Doc.Average_excess_air_coefficient_of_flue_UpperFurnace - 1) * this.Doc.Theoretical_volume_of_air;                         //=$F$50+$D$29*(G63-1)*$F$48
            this.Doc.Water_vapour_volume_CrossDuct = this.Doc.Theoretical_volume_of_water_vapour + Specific_humidity_of_ambient_air * (this.Doc.Average_excess_air_coefficient_of_flue_CrossDuct - 1) * this.Doc.Theoretical_volume_of_air;                            //=$F$50+$D$29*(H63-1)*$F$48
            this.Doc.Water_vapour_volume_ReverseChamber = this.Doc.Theoretical_volume_of_water_vapour + Specific_humidity_of_ambient_air * (this.Doc.Average_excess_air_coefficient_of_flue_ReverseChamber - 1) * this.Doc.Theoretical_volume_of_air;                       //=$F$50+$D$29*(I63-1)*$F$48
            this.Doc.Water_vapour_volume_BackPass = this.Doc.Theoretical_volume_of_water_vapour + Specific_humidity_of_ambient_air * (this.Doc.Average_excess_air_coefficient_of_flue_BackPass - 1) * this.Doc.Theoretical_volume_of_air;                             //=$F$50+$D$29*(J63-1)*$F$48


            //-------------------------------------------------------------------------------------------



            //F66
            this.Doc.Flue_gas_total_volume_LowerFurnace = this.Doc.Theoretical_flue_gas_volume + (this.Doc.Average_excess_air_coefficient_of_flue_LowerFurnace - 1) * this.Doc.Theoretical_volume_of_air + Specific_humidity_of_ambient_air * (this.Doc.Average_excess_air_coefficient_of_flue_LowerFurnace - 1) * this.Doc.Theoretical_volume_of_air;               //=$F$52+(F63-1)*$F$48+$D$29*(F63-1)*$F$48
            this.Doc.Flue_gas_total_volume_UpperFurnace = this.Doc.Theoretical_flue_gas_volume + (this.Doc.Average_excess_air_coefficient_of_flue_UpperFurnace - 1) * this.Doc.Theoretical_volume_of_air + Specific_humidity_of_ambient_air * (this.Doc.Average_excess_air_coefficient_of_flue_UpperFurnace - 1) * this.Doc.Theoretical_volume_of_air;               //=$F$52+(G63-1)*$F$48+$D$29*(G63-1)*$F$48
            this.Doc.Flue_gas_total_volume_CrossDuct = this.Doc.Theoretical_flue_gas_volume + (this.Doc.Average_excess_air_coefficient_of_flue_CrossDuct - 1) * this.Doc.Theoretical_volume_of_air + Specific_humidity_of_ambient_air * (this.Doc.Average_excess_air_coefficient_of_flue_CrossDuct - 1) * this.Doc.Theoretical_volume_of_air;                        //=$F$52+(H63-1)*$F$48+$D$29*(H63-1)*$F$48
            this.Doc.Flue_gas_total_volume_ReverseChamber = this.Doc.Theoretical_flue_gas_volume + (this.Doc.Average_excess_air_coefficient_of_flue_ReverseChamber - 1) * this.Doc.Theoretical_volume_of_air + Specific_humidity_of_ambient_air * (this.Doc.Average_excess_air_coefficient_of_flue_ReverseChamber - 1) * this.Doc.Theoretical_volume_of_air;         //=$F$52+(I63-1)*$F$48+$D$29*(I63-1)*$F$48
            this.Doc.Flue_gas_total_volume_BackPass = this.Doc.Theoretical_flue_gas_volume + (this.Doc.Average_excess_air_coefficient_of_flue_BackPass - 1) * this.Doc.Theoretical_volume_of_air + Specific_humidity_of_ambient_air * (this.Doc.Average_excess_air_coefficient_of_flue_BackPass - 1) * this.Doc.Theoretical_volume_of_air;             //=$F$52+(J63-1)*$F$48+$D$29*(J63-1)*$F$48


            //-------------------------------------------------------------------------------------------
            //F67
            this.Doc.Total_flue_gas_volume_at_exit_LowerFurnace = this.Doc.Theoretical_flue_gas_volume + (this.Doc.Excess_air_coefficient_of_flue_outlet_LowerFurnace - 1) * this.Doc.Theoretical_volume_of_air + Specific_humidity_of_ambient_air * (this.Doc.Excess_air_coefficient_of_flue_outlet_LowerFurnace - 1) * this.Doc.Theoretical_volume_of_air;       //=$F$52+(F62-1)*$F$48+$D$29*(F62-1)*$F$48
            this.Doc.Total_flue_gas_volume_at_exit_UpperFurnace = this.Doc.Theoretical_flue_gas_volume + (this.Doc.Excess_air_coefficient_of_flue_outlet_UpperFurnace - 1) * this.Doc.Theoretical_volume_of_air + Specific_humidity_of_ambient_air * (this.Doc.Excess_air_coefficient_of_flue_outlet_UpperFurnace - 1) * this.Doc.Theoretical_volume_of_air;                     //=$F$52+(G62-1)*$F$48+$D$29*(G62-1)*$F$48
            this.Doc.Total_flue_gas_volume_at_exit_CrossDuct = this.Doc.Theoretical_flue_gas_volume + (this.Doc.Excess_air_coefficient_of_flue_outlet_CrossDuct - 1) * this.Doc.Theoretical_volume_of_air + Specific_humidity_of_ambient_air * (this.Doc.Excess_air_coefficient_of_flue_outlet_CrossDuct - 1) * this.Doc.Theoretical_volume_of_air;                             //=$F$52+(H62-1)*$F$48+$D$29*(H62-1)*$F$48
            this.Doc.Total_flue_gas_volume_at_exit_ReverseChamber = this.Doc.Theoretical_flue_gas_volume + (this.Doc.Excess_air_coefficient_of_flue_outlet_ReverseChamber - 1) * this.Doc.Theoretical_volume_of_air + Specific_humidity_of_ambient_air * (this.Doc.Excess_air_coefficient_of_flue_outlet_ReverseChamber - 1) * this.Doc.Theoretical_volume_of_air;               //=$F$52+(I62-1)*$F$48+$D$29*(I62-1)*$F$48
            this.Doc.Total_flue_gas_volume_at_exit_BackPass = this.Doc.Theoretical_flue_gas_volume + (this.Doc.Excess_air_coefficient_of_flue_outlet_BackPass - 1) * this.Doc.Theoretical_volume_of_air + Specific_humidity_of_ambient_air * (this.Doc.Excess_air_coefficient_of_flue_outlet_BackPass - 1) * this.Doc.Theoretical_volume_of_air;                   //=$F$52+(J62-1)*$F$48+$D$29*(J62-1)*$F$48


            //-------------------------------------------------------------------------------------------

            //F68
            this.Doc.Volume_fraction_of_RO2_LowerFurnace = this.Doc.Theoretical_volume_of_CO2 / this.Doc.Flue_gas_total_volume_LowerFurnace;     //=F51/F66
            this.Doc.Volume_fraction_of_RO2_UpperFurnace = this.Doc.Theoretical_volume_of_CO2 / this.Doc.Flue_gas_total_volume_UpperFurnace;     //=F51/G66
            this.Doc.Volume_fraction_of_RO2_CrossDuct = this.Doc.Theoretical_volume_of_CO2 / this.Doc.Flue_gas_total_volume_CrossDuct;            //=F51/H66
            this.Doc.Volume_fraction_of_RO2_ReverseChamber = this.Doc.Theoretical_volume_of_CO2 / this.Doc.Flue_gas_total_volume_ReverseChamber;       //=F51/I66
            this.Doc.Volume_fraction_of_RO2_BackPass = this.Doc.Theoretical_volume_of_CO2 / this.Doc.Flue_gas_total_volume_BackPass;             //=F51/J66

            //-------------------------------------------------------------------------------------------

            //F69
            this.Doc.Volume_fraction_of_water_vapour_LowerFurnace = this.Doc.Water_vapour_volume_LowerFurnace / this.Doc.Flue_gas_total_volume_LowerFurnace;        //=F65/F66
            this.Doc.Volume_fraction_of_water_vapour_UpperFurnace = this.Doc.Water_vapour_volume_UpperFurnace / this.Doc.Flue_gas_total_volume_UpperFurnace;        //=G65/G66
            this.Doc.Volume_fraction_of_water_vapour_CrossDuct = this.Doc.Water_vapour_volume_CrossDuct / this.Doc.Flue_gas_total_volume_CrossDuct;           //=H65/H66
            this.Doc.Volume_fraction_of_water_vapour_ReverseChamber = this.Doc.Water_vapour_volume_ReverseChamber / this.Doc.Flue_gas_total_volume_ReverseChamber;      //=I65/I66
            this.Doc.Volume_fraction_of_water_vapour_BackPass = this.Doc.Water_vapour_volume_BackPass / this.Doc.Flue_gas_total_volume_BackPass;            //=J65/J66

            //-------------------------------------------------------------------------------------------


            //F70
            this.Doc.Volume_fraction_of_triatomic_gas_LowerFurnace = (this.Doc.Water_vapour_volume_LowerFurnace + this.Doc.Theoretical_volume_of_CO2) / this.Doc.Flue_gas_total_volume_LowerFurnace;       //=(F65+F51)/F66
            this.Doc.Volume_fraction_of_triatomic_gas_UpperFurnace = (this.Doc.Water_vapour_volume_UpperFurnace + this.Doc.Theoretical_volume_of_CO2) / this.Doc.Flue_gas_total_volume_UpperFurnace;       //=(G65+F51)/G66
            this.Doc.Volume_fraction_of_triatomic_gas_CrossDuct = (this.Doc.Water_vapour_volume_CrossDuct + this.Doc.Theoretical_volume_of_CO2) / this.Doc.Flue_gas_total_volume_CrossDuct;          //=(H65+F51)/H66
            this.Doc.Volume_fraction_of_triatomic_gas_ReverseChamber = (this.Doc.Water_vapour_volume_ReverseChamber + this.Doc.Theoretical_volume_of_CO2) / this.Doc.Flue_gas_total_volume_ReverseChamber;     //=(I65+F51)/I66
            this.Doc.Volume_fraction_of_triatomic_gas_BackPass = (this.Doc.Water_vapour_volume_BackPass + this.Doc.Theoretical_volume_of_CO2) / this.Doc.Flue_gas_total_volume_BackPass;           //=(J65+F51)/J66


            //-------------------------------------------------------------------------------------------

            //F71
            this.Doc.Flue_gas_mass_of_1_kg_fuel_LowerFurnace = 1 - Ash / 100 + (1.293 + (Specific_humidity_of_ambient_air * 0.804)) * this.Doc.Average_excess_air_coefficient_of_flue_LowerFurnace * this.Doc.Theoretical_volume_of_air;     //=1-$D$13/100+(1.293+($D$29*0.804))*F63*$F$48
            this.Doc.Flue_gas_mass_of_1_kg_fuel_UpperFurnace = 1 - Ash / 100 + (1.293 + (Specific_humidity_of_ambient_air * 0.804)) * this.Doc.Average_excess_air_coefficient_of_flue_UpperFurnace * this.Doc.Theoretical_volume_of_air;     //=1-$D$13/100+(1.293+($D$29*0.804))*G63*$F$48
            this.Doc.Flue_gas_mass_of_1_kg_fuel_CrossDuct = 1 - Ash / 100 + (1.293 + (Specific_humidity_of_ambient_air * 0.804)) * this.Doc.Average_excess_air_coefficient_of_flue_CrossDuct * this.Doc.Theoretical_volume_of_air;        //=1-$D$13/100+(1.293+($D$29*0.804))*H63*$F$48
            this.Doc.Flue_gas_mass_of_1_kg_fuel_ReverseChamber = 1 - Ash / 100 + (1.293 + (Specific_humidity_of_ambient_air * 0.804)) * this.Doc.Average_excess_air_coefficient_of_flue_ReverseChamber * this.Doc.Theoretical_volume_of_air;       //=1-$D$13/100+(1.293+($D$29*0.804))*I63*$F$48
            this.Doc.Flue_gas_mass_of_1_kg_fuel_BackPass = 1 - Ash / 100 + (1.293 + (Specific_humidity_of_ambient_air * 0.804)) * this.Doc.Average_excess_air_coefficient_of_flue_BackPass * this.Doc.Theoretical_volume_of_air;         //=1-$D$13/100+(1.293+($D$29*0.804))*J63*$F$48


            //-------------------------------------------------------------------------------------------

            //F72
            this.Doc.Flue_gas_density_LowerFurnace = this.Doc.Flue_gas_mass_of_1_kg_fuel_LowerFurnace / this.Doc.Flue_gas_total_volume_LowerFurnace;       //=F71/F66
            this.Doc.Flue_gas_density_UpperFurnace = this.Doc.Flue_gas_mass_of_1_kg_fuel_UpperFurnace / this.Doc.Flue_gas_total_volume_UpperFurnace;       //=G71/G66
            this.Doc.Flue_gas_density_CrossDuct = this.Doc.Flue_gas_mass_of_1_kg_fuel_CrossDuct / this.Doc.Flue_gas_total_volume_CrossDuct;          //=H71/H66
            this.Doc.Flue_gas_density_ReverseChamber = this.Doc.Flue_gas_mass_of_1_kg_fuel_ReverseChamber / this.Doc.Flue_gas_total_volume_ReverseChamber;     //=I71/I66
            this.Doc.Flue_gas_density_BackPass = this.Doc.Flue_gas_mass_of_1_kg_fuel_BackPass / this.Doc.Flue_gas_total_volume_BackPass;           //=J71/J66



            //-------------------------------------------------------------------------------------------
            //=D13*D17/100/F71;
            //F73
            this.Doc.Dimensionless_concentration_of_fly_ash_LowerFurnace = Ash * Fly_ash_fraction / 100 / this.Doc.Flue_gas_mass_of_1_kg_fuel_LowerFurnace;         //=D13*D17/100/F71
            this.Doc.Dimensionless_concentration_of_fly_ash_UpperFurnace = Ash * Fly_ash_fraction / 100 / this.Doc.Flue_gas_mass_of_1_kg_fuel_UpperFurnace;         //=D13*D17/100/G71
            this.Doc.Dimensionless_concentration_of_fly_ash_CrossDuct = Ash * Fly_ash_fraction / 100 / this.Doc.Flue_gas_mass_of_1_kg_fuel_CrossDuct;            //=D13*D17/100/H71
            this.Doc.Dimensionless_concentration_of_fly_ash_ReverseChamber = Ash * Fly_ash_fraction / 100 / this.Doc.Flue_gas_mass_of_1_kg_fuel_ReverseChamber;       //=D13*D17/100/I71
            this.Doc.Dimensionless_concentration_of_fly_ash_BackPass = Ash * Fly_ash_fraction / 100 / this.Doc.Flue_gas_mass_of_1_kg_fuel_BackPass;             //=D13*D17/100/J71



            //Flue gas composition at economizer outlet (% vol. dry)

            //D76

            this.Doc.Total_flue_gas_volume_at_economizer_outlet = this.Doc.Theoretical_volume_of_N2 + this.Doc.Theoretical_volume_of_CO2 + (this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace - 1) * this.Doc.Theoretical_volume_of_air;          //=F49+F51+(F61-1)*F48
            this.Doc.Volume_of_CO2_at_economizer_outlet = 1.866 * (this.Doc.Carbon_modified) / 100;                  //=1.866*(D33)/100
            this.Doc.Volume_of_O2_at_economizer_outlet = (this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace - 1) * this.Doc.Theoretical_volume_of_air * 0.21;                   //=(F61-1)*F48*0.21 
            this.Doc.CO2_in_flue_gas_at_economizer_outlet = this.Doc.Volume_of_CO2_at_economizer_outlet / this.Doc.Total_flue_gas_volume_at_economizer_outlet * 100;                //=D77/$D$76*100
            this.Doc.O2_at_economizer_outlet_at_economizer_outlet = this.Doc.Volume_of_O2_at_economizer_outlet / this.Doc.Total_flue_gas_volume_at_economizer_outlet * 100;        //=D78/$D$76*100       
            this.Doc.Deviation_i_calculated_CO2_value_from_given_value = (this.Doc.CO2_in_flue_gas_at_economizer_outlet - Design_CO2_in_flue_gas_at_eco_Outlet) / Design_CO2_in_flue_gas_at_eco_Outlet * 100;   //=(D79-D25)/D25*100
            this.Doc.Deviation_in_calculated_O2_value_from_given_value = (this.Doc.O2_at_economizer_outlet_at_economizer_outlet - Design_O2_in_flue_gas_at_eco_Outlet) / Design_O2_in_flue_gas_at_eco_Outlet * 100;   //=(D80-D26)/D26*100


            //Flue gas flow rate check

            //D85
            this.Doc.Flue_gas_flow_cal = this.Doc.Flue_gas_mass_of_1_kg_fuel_LowerFurnace * this.Doc.Design_fuel_consumption_ForFluegas;           //=F71*F57
            this.Doc.Flue_gas_flow_design = Design_flue_gas_flow/3.6;       //=D24

            //=(D85-D86)/D86*100

            this.Doc.Deviation_in_calc_flue_gas_flow_rate_from_design_value = (this.Doc.Flue_gas_flow_cal - this.Doc.Flue_gas_flow_design) / this.Doc.Flue_gas_flow_design * 100;      //=(D85-D86)/D86*100


            DataSet dt1 = new DataSet();
            dt1 = BLL.Get_1A_Parameters();

            foreach (DataRow row in dt1.Tables[0].Rows)
            {
                int pid = Convert.ToInt16(row["PID"].ToString());
                int cgid = Convert.ToInt16(row["CalculationGrpID"].ToString());
                //double Specific_humidity_of_ambient_air = Specific_humidity_of_ambient_airV;

               
               // int BoilerLoad = 1;
               // int ObjectiveID = 1;

                switch (pid)
                {
                    case 1:
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Design_excess_air_ratio);
                        break;


                    case 2:    //D32

                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Correction_factor_for_elemental_composition);

                        break;

                    case 3:      //D33

                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Carbon_modified);

                        break;

                    case 4:     //D34

                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Oxygen_modified);
                        break;


                    case 5:      //D35

                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Theoretical_flue_gas_flow_rate);
                        break;

                    case 6:     //D36

                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Excess_air_ratio_calc_based_on_given_total_combustion_air);
                        break;


                    case 7:      // 2. HHV check

                        //D39

                        //BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Higher_heating_value_ByDulongsformula); //changed as per excel 

                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, Higher_heating_value);
                        break;

                    case 8:      //D40
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Deviation_in_given_HHV_value_from_calculated_value);
                        break;


                    //LHV calculation
                    case 9:
                        //D43
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Lower_heating_value_on_as_received_basis);
                        break;

                    case 10:     //D44
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Volatil_matter_on_dry_ash_free_basis);
                        break;


                    // Coal-Combustion Product Volumes and Theoretical Air Volumes



                    case 11:     //F48
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Theoretical_volume_of_air);
                        break;

                    case 12:    //F49
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Theoretical_volume_of_N2);
                        break;

                    case 13:      //F50
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Theoretical_volume_of_water_vapour);
                        break;

                    case 14:
                        //F51
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Theoretical_volume_of_CO2);
                        break;

                    case 15:     //F52
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Theoretical_flue_gas_volume);
                        break;

                    case 16:
                        //F53
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Fly_ash_concentration);
                        break;


                    // Flue Gas Characteristics
                    case 17:      //F57
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Design_fuel_consumption_ForFluegas);
                        break;

                    case 19:      //F59
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Mass_flow_rate_of_Total_Combustion_air);

                        break;

                    case 21:      //61    //F61

                        BLL.Insert1A_Value_with_5_Parameters(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Excess_air_coefficient_of_flue_inlet_LowerFurnace, this.Doc.Excess_air_coefficient_of_flue_inlet_Upperfurnace, this.Doc.Excess_air_coefficient_of_flue_inlet_CrossDuct, this.Doc.Excess_air_coefficient_of_flue_inlet_ReverseChamber, this.Doc.Excess_air_coefficient_of_flue_inlet_BackPass);
                        break;


                    case 18:      //F58
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Theoretical_air_required);
                        break;

                    case 20:     //F60
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Total_excess_air);
                        break;






                    case 22:     //F62

                        BLL.Insert1A_Value_with_5_Parameters(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Excess_air_coefficient_of_flue_outlet_LowerFurnace, this.Doc.Excess_air_coefficient_of_flue_outlet_UpperFurnace, this.Doc.Excess_air_coefficient_of_flue_outlet_CrossDuct, this.Doc.Excess_air_coefficient_of_flue_outlet_ReverseChamber, this.Doc.Excess_air_coefficient_of_flue_outlet_BackPass);

                        break;

                    case 23:      //F63

                        BLL.Insert1A_Value_with_5_Parameters(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Average_excess_air_coefficient_of_flue_LowerFurnace, this.Doc.Average_excess_air_coefficient_of_flue_UpperFurnace, this.Doc.Average_excess_air_coefficient_of_flue_CrossDuct, this.Doc.Average_excess_air_coefficient_of_flue_ReverseChamber, this.Doc.Average_excess_air_coefficient_of_flue_BackPass);

                        break;

                    case 24:
                        //F64

                        BLL.Insert1A_Value_with_5_Parameters(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Excess_air_volume_LowerFurnace, this.Doc.Excess_air_volume_UpperFurnace, this.Doc.Excess_air_volume_CrossDuct,
                            this.Doc.Excess_air_volume_ReverseChamber, this.Doc.Excess_air_volume_BackPass);
                        break;

                    case 25:      //F65

                        BLL.Insert1A_Value_with_5_Parameters(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Water_vapour_volume_LowerFurnace, this.Doc.Water_vapour_volume_UpperFurnace, this.Doc.Water_vapour_volume_CrossDuct,
                            this.Doc.Water_vapour_volume_ReverseChamber, this.Doc.Water_vapour_volume_BackPass);
                        break;

                    case 26:        //F66

                        BLL.Insert1A_Value_with_5_Parameters(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Flue_gas_total_volume_LowerFurnace, this.Doc.Flue_gas_total_volume_UpperFurnace, this.Doc.Flue_gas_total_volume_CrossDuct,
                            this.Doc.Flue_gas_total_volume_ReverseChamber, this.Doc.Flue_gas_total_volume_BackPass);
                        break;

                    case 27:        //F67

                        BLL.Insert1A_Value_with_5_Parameters(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Total_flue_gas_volume_at_exit_LowerFurnace, this.Doc.Total_flue_gas_volume_at_exit_UpperFurnace,
                            this.Doc.Total_flue_gas_volume_at_exit_CrossDuct, this.Doc.Total_flue_gas_volume_at_exit_ReverseChamber, this.Doc.Total_flue_gas_volume_at_exit_BackPass);
                        break;

                    case 28:     //F68


                        BLL.Insert1A_Value_with_5_Parameters(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Volume_fraction_of_RO2_LowerFurnace, this.Doc.Volume_fraction_of_RO2_UpperFurnace,
                           this.Doc.Volume_fraction_of_RO2_CrossDuct, this.Doc.Volume_fraction_of_RO2_ReverseChamber, this.Doc.Volume_fraction_of_RO2_BackPass);
                        break;

                    case 29:     //F69


                        BLL.Insert1A_Value_with_5_Parameters(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Volume_fraction_of_water_vapour_LowerFurnace, this.Doc.Volume_fraction_of_water_vapour_UpperFurnace,
                            this.Doc.Volume_fraction_of_water_vapour_CrossDuct, this.Doc.Volume_fraction_of_water_vapour_ReverseChamber, this.Doc.Volume_fraction_of_water_vapour_BackPass);
                        break;

                    case 30:     //F70


                        BLL.Insert1A_Value_with_5_Parameters(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Volume_fraction_of_triatomic_gas_LowerFurnace, this.Doc.Volume_fraction_of_triatomic_gas_UpperFurnace,
                           this.Doc.Volume_fraction_of_triatomic_gas_CrossDuct, this.Doc.Volume_fraction_of_triatomic_gas_ReverseChamber, this.Doc.Volume_fraction_of_triatomic_gas_BackPass);
                        break;

                    case 31:
                        //F71

                        BLL.Insert1A_Value_with_5_Parameters(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Flue_gas_mass_of_1_kg_fuel_LowerFurnace, this.Doc.Flue_gas_mass_of_1_kg_fuel_UpperFurnace,
                            this.Doc.Flue_gas_mass_of_1_kg_fuel_CrossDuct, this.Doc.Flue_gas_mass_of_1_kg_fuel_ReverseChamber, this.Doc.Flue_gas_mass_of_1_kg_fuel_BackPass);

                        break;

                    case 32:      //F72

                        BLL.Insert1A_Value_with_5_Parameters(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Flue_gas_density_LowerFurnace, this.Doc.Flue_gas_density_UpperFurnace,
                            this.Doc.Flue_gas_density_CrossDuct, this.Doc.Flue_gas_density_ReverseChamber, this.Doc.Flue_gas_density_BackPass);

                        break;

                    case 33:
                        //F73

                        BLL.Insert1A_Value_with_5_Parameters(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Dimensionless_concentration_of_fly_ash_LowerFurnace, this.Doc.Dimensionless_concentration_of_fly_ash_UpperFurnace,
                            this.Doc.Dimensionless_concentration_of_fly_ash_CrossDuct, this.Doc.Dimensionless_concentration_of_fly_ash_ReverseChamber, this.Doc.Dimensionless_concentration_of_fly_ash_BackPass);
                        break;

                    //Flue gas composition at economizer outlet (% vol. dry)
                    case 34:

                        //D76
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Total_flue_gas_volume_at_economizer_outlet);

                        break;


                    case 35:
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Volume_of_CO2_at_economizer_outlet);
                        break;


                    case 36:
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Volume_of_O2_at_economizer_outlet);
                        break;


                    case 37:
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.CO2_in_flue_gas_at_economizer_outlet);
                        break;


                    case 38:
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.O2_at_economizer_outlet_at_economizer_outlet);
                        break;


                    case 39:
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Deviation_i_calculated_CO2_value_from_given_value);
                        break;


                    case 40:
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Deviation_in_calculated_O2_value_from_given_value);
                        break;



                    //Flue gas flow rate check
                    case 41:

                        //D85

                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Flue_gas_flow_cal);
                        break;

                    case 42:
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Flue_gas_flow_design);
                        break;

                    case 43:
                        BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, this.Doc.Deviation_in_calc_flue_gas_flow_rate_from_design_value);
                        break;

                    case 44: BLL.Insert1A_Value(pid, cgid, BoilerID, ProjectID, BoilerLoad, ObjectiveID, Specific_humidity_of_ambient_air);
                        //Insert statement for Specific_humidity_of_ambient_air;
                        break;




                }
            }




            //Calculations


        } 

        //Excess Air check Ends
        public double EA_Calculation(double Specific_Humidity_Of_Ambient_Air_Parameter)
        {
            double UC_carbon_in_Bottom_ash_content_per_100_kg_of_fuel_fired;
            double UC_carbon_in_Fly_ash_content_per_100_kg_of_fuel_fired;
            double Total_unburnt_carbon_per_100_kg_of_fuel_fired;
            double Heat_loss_due_to_the_unburnt_carbon;
            double Combustible_carbon_in_fuel;
            double Theoretical_volume_of_air;
            double Specific_humidity_of_air;

            double Assumed_excess_air_in_the_operating_case = 9.99;

            double CO2;
            double CO2_VolDry;
            double CO2_VolWet;

            double O2;
            double O2_VolDry;
            double O2_VolWet;

            double SO2;
            double SO2_VolDry;
            double SO2_VolWet;

            double N2;
            double N2_VolDry;
            double N2_VolWet;

            double Total_dry_combustion_products;
            double H2O_combustion;
            double H2O_fuel;
            double H2O_air;
            double Total_H2O_air;
            double Total_H2O_air_VolWet;
            double Total_wet_combustion_products;
            double Dry_air;
            double H2O_in_air;
            double Total_wet_air;

            double Carbon;
            double Hydrogen;
            double Oxygen;
            double Nitrogen;
            double Sulfur;
            double Ash;
            double Moisture;
            double Higher_heating_value;
            double Specific_humidity_of_ambient_air;
            double Unburnt_carbon_in_bottom_ash;
            double Unburnt_carbon_in_fly_ash;
            double Fly_ash_fraction_of_total_ash;
            double O2_in_flue_gas_at_eco_Outlet_measured;
            double O2_in_flue_gas_at_APH_Outlet_measured;

            
            DataSet dt = new DataSet();
            dt = BLL.Get1A_EA_Parameters(ProjectID, BoilerID);

            Carbon = Convert.ToDouble(dt.Tables[0].Rows[0][1].ToString());       //D8 
            Hydrogen = Convert.ToDouble(dt.Tables[0].Rows[1][1].ToString());     //D9
            Oxygen = Convert.ToDouble(dt.Tables[0].Rows[2][1].ToString());       //D10   
            Nitrogen = Convert.ToDouble(dt.Tables[0].Rows[3][1].ToString());     //D11
            Sulfur = Convert.ToDouble(dt.Tables[0].Rows[4][1].ToString());       //D12 
            Ash = Convert.ToDouble(dt.Tables[0].Rows[5][1].ToString());          //D13
            Moisture = Convert.ToDouble(dt.Tables[0].Rows[6][1].ToString());     //D14 
            Higher_heating_value = Convert.ToDouble(dt.Tables[0].Rows[7][1].ToString());     //D15 

            Specific_humidity_of_ambient_air = Specific_Humidity_Of_Ambient_Air_Parameter;    //D16

            Unburnt_carbon_in_bottom_ash = Convert.ToDouble(dt.Tables[1].Rows[0][1].ToString());    //D17
            Unburnt_carbon_in_fly_ash = Convert.ToDouble(dt.Tables[1].Rows[0][2].ToString());    //D18

            Fly_ash_fraction_of_total_ash = Convert.ToDouble(dt.Tables[2].Rows[0][1].ToString());   //D19

            O2_in_flue_gas_at_eco_Outlet_measured = Convert.ToDouble(dt.Tables[3].Rows[0][1].ToString()); //D20
            O2_in_flue_gas_at_APH_Outlet_measured = Convert.ToDouble(dt.Tables[3].Rows[0][2].ToString());    //D21

            //F27
            UC_carbon_in_Bottom_ash_content_per_100_kg_of_fuel_fired = Unburnt_carbon_in_bottom_ash * (1 - Fly_ash_fraction_of_total_ash) * Ash / (106 - Unburnt_carbon_in_bottom_ash);

            //F28
            UC_carbon_in_Fly_ash_content_per_100_kg_of_fuel_fired = Unburnt_carbon_in_fly_ash * Fly_ash_fraction_of_total_ash * Ash / (100 - Unburnt_carbon_in_fly_ash);

            //F29
            Total_unburnt_carbon_per_100_kg_of_fuel_fired = UC_carbon_in_Fly_ash_content_per_100_kg_of_fuel_fired + UC_carbon_in_Bottom_ash_content_per_100_kg_of_fuel_fired;

            //F30
            Heat_loss_due_to_the_unburnt_carbon = Total_unburnt_carbon_per_100_kg_of_fuel_fired * 8060 * 4.18 / Higher_heating_value;

            //F31
            Combustible_carbon_in_fuel = Carbon - Total_unburnt_carbon_per_100_kg_of_fuel_fired;

            //F32
            Theoretical_volume_of_air = 0.0889 * (Carbon + 0.375 * Sulfur) + 0.265 * Hydrogen - 0.033 * Oxygen;

            //F33
            Specific_humidity_of_air = 1.608 * Specific_humidity_of_ambient_air;

            do
            {
                Assumed_excess_air_in_the_operating_case = Assumed_excess_air_in_the_operating_case + 0.01;

                //C38
                CO2 = 1.866 * (Combustible_carbon_in_fuel / 100);

                //C39
                O2 = (Assumed_excess_air_in_the_operating_case / 100) * Theoretical_volume_of_air * 0.2095;

                //C40
                SO2 = 1.866 * 0.375 * Sulfur / 100;

                //C41
                N2 = 0.79 * Theoretical_volume_of_air * (1 + Assumed_excess_air_in_the_operating_case / 100) + 0.8 * Nitrogen / 100;

                //C42
                Total_dry_combustion_products = CO2 + O2 + SO2 + N2;

                //C43
                H2O_combustion = 0.111 * Hydrogen;

                //C44
                H2O_fuel = 0.0124 * Moisture;

                //C45
                H2O_air = Specific_humidity_of_air * Theoretical_volume_of_air;

                //C46
                Total_H2O_air = H2O_combustion + H2O_fuel + H2O_air;

                //C47
                Total_wet_combustion_products = Total_dry_combustion_products + H2O_combustion + H2O_fuel + H2O_air + Total_H2O_air;

                //C48
                Dry_air = Theoretical_volume_of_air * (1 + Assumed_excess_air_in_the_operating_case / 100);

                //C49
                H2O_in_air = H2O_air;

                //C50
                Total_wet_air = Dry_air + H2O_in_air;

                //D38
                CO2_VolDry = CO2 * 100 / Total_dry_combustion_products;

                //D39
                O2_VolDry = O2 * 100 / Total_dry_combustion_products;

                //D40
                SO2_VolDry = SO2 * 100 / Total_dry_combustion_products;

                //D41
                N2_VolDry = N2 * 100 / Total_dry_combustion_products;

                //E38
                CO2_VolWet = CO2 * 100 / Total_wet_combustion_products;

                //E39
                O2_VolWet = O2 * 100 / Total_wet_combustion_products;

                //E40
                SO2_VolWet = SO2 * 100 / Total_wet_combustion_products;

                //E41
                N2_VolWet = N2 * 100 / Total_wet_combustion_products;

                //E46
                Total_H2O_air_VolWet = Total_H2O_air * 100 / Total_wet_combustion_products;

            } while (Total_H2O_air_VolWet != O2_in_flue_gas_at_eco_Outlet_measured);

            return Assumed_excess_air_in_the_operating_case;

        }

    }

}
