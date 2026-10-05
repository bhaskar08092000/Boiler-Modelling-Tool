using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;
using BoilerModellingTool.BLL;
using System.Collections;
using Utilities;

namespace GenericClasses
{
    public class Class_5C_Calculation
    {
        //string BoilerID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
        //string ProjectID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
        //string SectionID = "6F83D306-5D7A-4A9D-9D5F-218D4B2B1AD2";

        ////PSH	6F83D306-5D7A-4A9D-9D5F-218D4B2B1AD2
        ////Front RH	8922822D-1568-4A27-A4AE-77E7BCEDCA49
        ////Rear RH	54E4FC11-81D9-4C52-AC48-DA4FDB7628E2
        ////Front SH	28FAD033-0137-4916-8FA4-ECFFE9896622

        //string BoilerLoad = "100%TMCR";
        //int ObjectiveID = 1;
        //int Tube_Type_1 = 1;
        //int Tube_Type_2 = 2;
        //int Outer_Tube_Position = 1;
        //int Inner_Tube_Position = 2;

        string BoilerID = null;
        string ProjectID = null;
        string SectionID = null;
        string BoilerLoad = null;
        int ObjectiveID = 0;
        int Tube_Type_1 = 0;
        int Tube_Type_2 = 0;
        int Outer_Tube_Position = 0;
        int Inner_Tube_Position = 0;
      public  Class_5C_Calculation(string BoilerID, string ProjectID, string SectionID,
             string BoilerLoad ,int ObjectiveID ,int Tube_Type_1 ,int Tube_Type_2 , int Outer_Tube_Position,int Inner_Tube_Position)
        {
            //this.BoilerID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
            //this.ProjectID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
            //this.SectionID = "8922822D-1568-4A27-A4AE-77E7BCEDCA49";
            //this.BoilerLoad = "100%TMCR";
            //this.ObjectiveID = 1;
            //this.Tube_Type_1 = 4;
            //this.Tube_Type_2 = 4;
            //this.Outer_Tube_Position = 1;
            //this.Inner_Tube_Position = 2;


            this.BoilerID = BoilerID;
            this.ProjectID = ProjectID;
            this.SectionID = SectionID;
            this.BoilerLoad = BoilerLoad;
            this.ObjectiveID = ObjectiveID;
            this.Tube_Type_1 = Tube_Type_1;
            this.Tube_Type_2 = Tube_Type_2;
            this.Outer_Tube_Position = Outer_Tube_Position;
            this.Inner_Tube_Position = Inner_Tube_Position;



        }

        Cal_5C_BLL mCal_5C_BLL = new Cal_5C_BLL();
        Cal_5C_SC mCal_5C_SC = new Cal_5C_SC();
        List<Double> Out_In_Diameter = new List<Double>();
        List<Double> Out_Tube_Area = new List<Double>();
        List<Double> In_In_Diameter = new List<Double>();
        List<Double> In_Tube_Area = new List<Double>();
        List<Double> Average_of_flux_ratio_Tube_Out = new List<Double>();
        List<Double> Ratio_incedent_fluxes_Tube_Out = new List<Double>();
        List<Double> Average_of_flux_ratio_Tube_In = new List<Double>();
        List<Double> Ratio_incedent_fluxes_Tube_In = new List<Double>();
        List<int> V_Sec_List = new List<int>();
        List<Double> Flux_X_Area_Tube_Out = new List<Double>();
        List<Double> Flux_X_Area_Tube_In = new List<Double>();
        List<Double> Ratio_of_absorbed_flux_Out = new List<Double>();
        List<Double> Ratio_of_absorbed_flux_In = new List<Double>();
        List<Double> Absorbed_fraction = new List<Double>();
        List<Double> Absorbed_DR_MW = new List<Double>();
        Dictionary<string, List<int>> Pairs_Vertical_Section = new Dictionary<string, List<int>>();

        public List<Double> Input_List()
        {
            //List<Double> Input_5C_Module = new List<double>();
            List<Double> Input_List = new List<double>();
            Cal_5C_BLL mCal_5C_BLL = new Cal_5C_BLL();
            DataSet dt = new DataSet();
            dt = mCal_5C_BLL.Get_Input_For_5C_Calculation(BoilerID, ProjectID, BoilerLoad, SectionID);
            //D7
            mCal_5C_SC.No_of_loops_per_element = Convert.ToDouble(dt.Tables[0].Rows[0]["Value"].ToString());

            Input_List.Add(mCal_5C_SC.No_of_loops_per_element);
            //D8


            mCal_5C_SC.Tube_diameter_of_platen_superheater = Convert.ToDouble(dt.Tables[1].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.Tube_diameter_of_platen_superheater);
            //D9
            mCal_5C_SC.Platen_superheater_tube_thickness = Convert.ToDouble(dt.Tables[2].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.Platen_superheater_tube_thickness);
            //D10
            mCal_5C_SC.Platen_superheater_height = Convert.ToDouble(dt.Tables[3].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.Platen_superheater_height);
            //D11
            mCal_5C_SC.Platen_superheater_depth = Convert.ToDouble(dt.Tables[4].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.Platen_superheater_depth);
            //D12
            mCal_5C_SC.Platen_superheater_transverse_rows = Convert.ToDouble(dt.Tables[5].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.Platen_superheater_transverse_rows);
            //D13
            mCal_5C_SC.PSH_convective_area = Convert.ToDouble(dt.Tables[6].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.PSH_convective_area);
            //D14
            mCal_5C_SC.Relative_transverse_pitch = Convert.ToDouble(dt.Tables[7].Rows[0]["Value"].ToString());
           // mCal_5C_SC.Relative_transverse_pitch = 4.01053;
            Input_List.Add(mCal_5C_SC.Relative_transverse_pitch);


            //D18
            mCal_5C_SC.Flow_rate_through_PSH = Convert.ToDouble(dt.Tables[8].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.Flow_rate_through_PSH);
            //D19
            mCal_5C_SC.Total_D_R_radiation_absorbed_in_SH = Convert.ToDouble(dt.Tables[9].Rows[0]["Value"].ToString());
           // mCal_5C_SC.Total_D_R_radiation_absorbed_in_SH = 195.4;
            Input_List.Add(mCal_5C_SC.Total_D_R_radiation_absorbed_in_SH);
            //D20
            mCal_5C_SC.convection_heat = Convert.ToDouble(dt.Tables[10].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.convection_heat);
            //D21
            mCal_5C_SC.Design_fuel_consumption = Convert.ToDouble(dt.Tables[11].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.Design_fuel_consumption);

            //D22
            mCal_5C_SC.Enthalpy_of_steam_inlet_to_the_PSH = Convert.ToDouble(dt.Tables[12].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.Enthalpy_of_steam_inlet_to_the_PSH);
            //D23
            mCal_5C_SC.Inlet_pressure_pf_steam = Convert.ToDouble(dt.Tables[13].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.Inlet_pressure_pf_steam);
            //D24
            mCal_5C_SC.Outlet_pressure = Convert.ToDouble(dt.Tables[14].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.Outlet_pressure);
            //D25
            mCal_5C_SC.Heat_transfer_coefficient_from_tube_wall_to_steam = Convert.ToDouble(dt.Tables[15].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.Heat_transfer_coefficient_from_tube_wall_to_steam);
            //D26
            mCal_5C_SC.Steanm_outlet_temperature = Convert.ToDouble(dt.Tables[16].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.Steanm_outlet_temperature);

            //C31
            mCal_5C_SC.nose = Convert.ToDouble(dt.Tables[17].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.nose);
            //C32
            mCal_5C_SC.avg_location_vertical_direction = Convert.ToDouble(dt.Tables[18].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.avg_location_vertical_direction);
            //C33
            mCal_5C_SC.Roof = Convert.ToDouble(dt.Tables[19].Rows[0]["Value"].ToString());
            Input_List.Add(mCal_5C_SC.Roof);
            //Flue 
            Double Flue_gas_duct_declination_FSH_zone = Convert.ToDouble(dt.Tables[20].Rows[0]["Value"].ToString());

         

            

            //mCal_5C_SC.No_of_loops_per_element = 8;
            //mCal_5C_SC.Tube_diameter_of_platen_superheater = 47.63;
            //mCal_5C_SC.Platen_superheater_tube_thickness = 7.6;
            //mCal_5C_SC.Platen_superheater_height = 11.126;
            //mCal_5C_SC.Platen_superheater_depth = 1.653;
            //mCal_5C_SC.Platen_superheater_transverse_rows = 29;
            //mCal_5C_SC.PSH_convective_area = 1483.2;
            //mCal_5C_SC.Relative_transverse_pitch = 9.60;
            //mCal_5C_SC.Flow_rate_through_PSH = 736000;
            //mCal_5C_SC.Total_D_R_radiation_absorbed_in_SH = 1182.4;
            //mCal_5C_SC.convection_heat = 1796.5;
            //mCal_5C_SC.Design_fuel_consumption = 24.4722222222222;
            //mCal_5C_SC.Enthalpy_of_steam_inlet_to_the_PSH = 2977.4;
            //mCal_5C_SC.Inlet_pressure_pf_steam = 16.0;
            //mCal_5C_SC.Outlet_pressure = 15.4;
            //mCal_5C_SC.Heat_transfer_coefficient_from_tube_wall_to_steam = 4450.2;
            //mCal_5C_SC.Steanm_outlet_temperature = 509.8;
            //mCal_5C_SC.nose = 1.53816935395835;
            //mCal_5C_SC.avg_location_vertical_direction = 0.917739803305574;
            //mCal_5C_SC.Roof = 0.656913460294349;


            Input_List.Add(mCal_5C_SC.No_of_loops_per_element);
            Input_List.Add(mCal_5C_SC.Tube_diameter_of_platen_superheater);
            Input_List.Add(mCal_5C_SC.Platen_superheater_tube_thickness);
            Input_List.Add(mCal_5C_SC.Platen_superheater_height);
            Input_List.Add(mCal_5C_SC.Platen_superheater_depth);
            Input_List.Add(mCal_5C_SC.Platen_superheater_transverse_rows);
            Input_List.Add(mCal_5C_SC.PSH_convective_area);
            Input_List.Add(mCal_5C_SC.Flow_rate_through_PSH);
            Input_List.Add(mCal_5C_SC.Total_D_R_radiation_absorbed_in_SH);
            Input_List.Add(mCal_5C_SC.convection_heat);
            Input_List.Add(mCal_5C_SC.Design_fuel_consumption);
            Input_List.Add(mCal_5C_SC.Enthalpy_of_steam_inlet_to_the_PSH);
            Input_List.Add(mCal_5C_SC.Inlet_pressure_pf_steam);
            Input_List.Add(mCal_5C_SC.Outlet_pressure);
            Input_List.Add(mCal_5C_SC.Heat_transfer_coefficient_from_tube_wall_to_steam);
            Input_List.Add(mCal_5C_SC.Steanm_outlet_temperature);
            Input_List.Add(mCal_5C_SC.nose);
            Input_List.Add(mCal_5C_SC.avg_location_vertical_direction);
            Input_List.Add(mCal_5C_SC.Roof);
            Input_List.Add(mCal_5C_SC.Relative_transverse_pitch);

            //0	 Input_List.Add(mCal_5C_SC.No_of_loops_per_element);
            //1	        Input_List.Add(mCal_5C_SC.Tube_diameter_of_platen_superheater);
            //2	        Input_List.Add(mCal_5C_SC.Platen_superheater_tube_thickness);
            //3	        Input_List.Add(mCal_5C_SC.Platen_superheater_height);
            //4	        Input_List.Add(mCal_5C_SC.Platen_superheater_depth);
            //5	        Input_List.Add(mCal_5C_SC.Platen_superheater_transverse_rows);
            //6	        Input_List.Add(mCal_5C_SC.PSH_convective_area);
            //7	        Input_List.Add(mCal_5C_SC.Flow_rate_through_PSH);
            //8	        Input_List.Add(mCal_5C_SC.Total_D_R_radiation_absorbed_in_SH);
            //9	        Input_List.Add(mCal_5C_SC.convection_heat);
            //10	        Input_List.Add(mCal_5C_SC.Design_fuel_consumption);
            //11	        Input_List.Add(mCal_5C_SC.Enthalpy_of_steam_inlet_to_the_PSH);
            //12	        Input_List.Add(mCal_5C_SC.Inlet_pressure_pf_steam);
            //13	        Input_List.Add(mCal_5C_SC.Outlet_pressure);
            //14	        Input_List.Add(mCal_5C_SC.Heat_transfer_coefficient_from_tube_wall_to_steam);
            //15	        Input_List.Add(mCal_5C_SC.Steanm_outlet_temperature);
            //16	        Input_List.Add(mCal_5C_SC.nose);
            //17	        Input_List.Add(mCal_5C_SC.avg_location_vertical_direction);
            //18	        Input_List.Add(mCal_5C_SC.Roof);
            //19              Input_List.Add(mCal_5C_SC.Relative_transverse_pitch);
            return Input_List;
            //return Input_5C_Module;


        }
        //Retriving Value from DB
        public void Calculations_for_MI()
        {
            Dictionary<string, List<int>> Pairs_Vertical_Section = new Dictionary<string, List<int>>();
            List<int> V_Sec_List = new List<int>();
            Cal_5C_BLL mCal_5C_BLL = new Cal_5C_BLL();
            Stream_Macros stream_Macros_BLL = new Stream_Macros();
            DataSet dt_Sec = new DataSet();

            dt_Sec = mCal_5C_BLL.Get_Input_For_5C_Calculation_Vertical_Section(Tube_Type_1, Tube_Type_2);
            //int No_Of_Loop_Element = Convert.ToInt16(dt.Tables[0].Rows[0][0].ToString());
            List<Double> Input_var = Input_List();
            int No_Of_Loop_Element = Convert.ToInt16(Input_var.ElementAt(0));
            int Total_Vertical_Section;
            //number of vertical section in a Heating element 
            if (Tube_Type_1 == Tube_Type_2)
            {
                Total_Vertical_Section = (1 * (Convert.ToInt16(dt_Sec.Tables[0].Rows[0][0].ToString()))) + (No_Of_Loop_Element - 1) * (Convert.ToInt16(dt_Sec.Tables[0].Rows[0][0].ToString()));
            }
            else
            {

                Total_Vertical_Section = (1 * (Convert.ToInt16(dt_Sec.Tables[0].Rows[0][0].ToString()))) + (No_Of_Loop_Element - 1) * (Convert.ToInt16(dt_Sec.Tables[0].Rows[1][0].ToString()));
            }
            //Adding no of section into the List
            for (int i = 1; i <= Total_Vertical_Section; i++)
            {
                V_Sec_List.Add(i);

            }
            Pairs_Vertical_Section = Pairing_Tube_Section(Tube_Type_1, Tube_Type_2, No_Of_Loop_Element, V_Sec_List);
            List<Double> Input = Input_List();
            //Constant convection flux in the PSH-->=D20*D21/D13
            Double Constant_convection_flux = Input.ElementAt(9) * Input.ElementAt(10) / Input.ElementAt(6);

            //0	 Input_List.Add(mCal_5C_SC.No_of_loops_per_element);
            //1	        Input_List.Add(mCal_5C_SC.Tube_diameter_of_platen_superheater);
            //2	        Input_List.Add(mCal_5C_SC.Platen_superheater_tube_thickness);
            //3	        Input_List.Add(mCal_5C_SC.Platen_superheater_height);
            //4	        Input_List.Add(mCal_5C_SC.Platen_superheater_depth);
            //5	        Input_List.Add(mCal_5C_SC.Platen_superheater_transverse_rows);
            //6	        Input_List.Add(mCal_5C_SC.PSH_convective_area);
            //7	        Input_List.Add(mCal_5C_SC.Flow_rate_through_PSH);
            //8	        Input_List.Add(mCal_5C_SC.Total_D_R_radiation_absorbed_in_SH);
            //9	        Input_List.Add(mCal_5C_SC.convection_heat);
            //10	        Input_List.Add(mCal_5C_SC.Design_fuel_consumption);
            //11	        Input_List.Add(mCal_5C_SC.Enthalpy_of_steam_inlet_to_the_PSH);
            //12	        Input_List.Add(mCal_5C_SC.Inlet_pressure_pf_steam);
            //13	        Input_List.Add(mCal_5C_SC.Outlet_pressure);
            //14	        Input_List.Add(mCal_5C_SC.Heat_transfer_coefficient_from_tube_wall_to_steam);
            //15	        Input_List.Add(mCal_5C_SC.Steanm_outlet_temperature);
            //16	        Input_List.Add(mCal_5C_SC.nose);
            //17	        Input_List.Add(mCal_5C_SC.avg_location_vertical_direction);
            //18	        Input_List.Add(mCal_5C_SC.Roof);

            Steam_Tempreture_Calculation_Same_Tube_Type(Pairs_Vertical_Section, V_Sec_List, Constant_convection_flux, No_Of_Loop_Element, Input);
        }
        //Pairing of Section depend upon tube type
        public Dictionary<string, List<int>> Pairing_Tube_Section(int tube_type1, int tube_type2, int Total_loop, List<int> Total_Vertical_Sec)
        {
            Dictionary<string, List<int>> Pair_Sec = new Dictionary<string, List<int>>();
            for (int i = 1; i <= Total_loop; i++)
            {
                Pair_Sec.Add("Sec" + i.ToString(), new List<int>());

            }
            if (Tube_Type_1 == tube_type2)
            {
                int j = 0;

                int flag = 1;

                while (j <= Total_Vertical_Sec.Count - 1)
                {
                    List<int> Temp = new List<int>();
                    for (int i = 1; i <= Total_loop; i++)
                    {

                        Temp.Add(Total_Vertical_Sec.ElementAt(j));
                        j++;

                    }
                    if (flag % 2 == 0)
                    {
                        Temp.Reverse();
                    }

                    int p = 0;
                    for (int i = 1; i <= Total_loop; i++)
                    {

                        Pair_Sec["Sec" + i].Add(Temp.ElementAt(p));

                        p++;
                    }
                    flag = flag + 1;
                }

            }
            else
            {
                Pair_Sec["Sec" + 1].Add(Total_Vertical_Sec.ElementAt(0));
                Pair_Sec["Sec" + 1].Add(Total_Vertical_Sec.ElementAt(Total_Vertical_Sec.Count - 1));
                //Total_Vertical_Sec.RemoveAt(0);
                //Total_Vertical_Sec.RemoveAt(Total_Vertical_Sec.Count - 1);
                int j = 1;

                int flag = 1;

                while (j <= Total_Vertical_Sec.Count - 2)
                {
                    List<int> Temp = new List<int>();
                    for (int i = 1; i <= Total_loop - 1; i++)
                    {

                        Temp.Add(Total_Vertical_Sec.ElementAt(j));
                        j++;

                    }
                    if (flag % 2 == 0)
                    {
                        Temp.Reverse();
                    }
                    int p = 0;
                    for (int i = 2; i <= Total_loop; i++)
                    {

                        Pair_Sec["Sec" + i].Add(Temp.ElementAt(p));

                        p++;
                    }
                    flag = flag + 1;
                }
            }
            return Pair_Sec;

        }
        //Calculation of Area
        //public Tuple<List<Double>, List<Double>, List<Double>, List<Double>, List<Double>, Double, Double> Area_Calculation_Tube(int Inner_Tube_Position,int tube_Type)
        //{
        //    Double Correction_of_length_required = 0.00;
        //    Double Error_Correction_Area = 0.0;
        //    Double In_Sum_Length;
        //    Double In_Sum_Area;

        //    DataSet dt_in = new DataSet();
        //    dt_in = mCal_5C_BLL.Get_Input_For_5C_Calculation_Of_Area(BoilerID, ProjectID, SectionID, ObjectiveID, tube_Type, Inner_Tube_Position);
        //    List<Double> In_Diameter = new List<Double>();
        //    List<Double> In_Thickness = new List<Double>();
        //    List<Double> In_Length = new List<Double>();
        //    List<String> In_Material = new List<String>();
        //    List<Double> Out_Diameter = new List<Double>();
        //    List<Double> In_In_Diameter = new List<Double>();
        //    List<Double> Thermal_Con_Value = new List<Double>();
        //    List<Double> In_Tube_Area = new List<Double>();
        //    DataSet Thermal_Conductivity = new DataSet();
        //    Thermal_Conductivity = mCal_5C_BLL.Get_Thermal_Conductivity_For_5C_Calculation_(BoilerID, ProjectID, SectionID, ObjectiveID, tube_Type, Inner_Tube_Position);
        //    for (int i = 0; i <= Thermal_Conductivity.Tables[0].Rows.Count - 1; i++)
        //    {
        //        Thermal_Con_Value.Add(Convert.ToDouble(Thermal_Conductivity.Tables[0].Rows[i]["ThermalConductivity"].ToString()));
        //    }
        //    //Input 5C_Ar
        //    DataSet dt = new DataSet();
        //    dt = mCal_5C_BLL.Get_Input_For_5C_Calculation(BoilerID, ProjectID, BoilerLoad, SectionID);


        //    List<Double> Input = Input_List();
        //    //D7
        //    mCal_5C_SC.No_of_loops_per_element = Input.ElementAt(0);
        //    //D13
        //    mCal_5C_SC.PSH_convective_area = Input.ElementAt(6);
        //    //D15
        //    Double Nose_up_Dip = Input.ElementAt(16);
        //    //D12
        //    Double Platen_SuperHeater_Transevers = Input.ElementAt(5);
        //    do
        //    {
        //        In_Diameter.Clear();
        //        In_Thickness.Clear();
        //        In_Length.Clear();
        //        In_Material.Clear();
        //        In_In_Diameter.Clear();
        //        In_Tube_Area.Clear();
        //        for (int i = 0; i <= dt_in.Tables[0].Rows.Count - 1; i++)
        //        {
        //            int PID = Convert.ToInt16(dt_in.Tables[0].Rows[i]["PID"].ToString());
        //            string Tube_Section = dt_in.Tables[0].Rows[i]["Tube_SectionID"].ToString();
        //            if (PID == 1)
        //            {
        //                switch (tube_Type)
        //                {
        //                    case 1:

        //                        In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()));

        //                        break;
        //                    case 2:
        //                        if (Tube_Section != "Sec 3" && Tube_Section != "Sec 8")
        //                        {
        //                            In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()) + Correction_of_length_required);
        //                        }
        //                        else if (Tube_Section == "Sec 3")
        //                        {

        //                            Double Value = Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()) / Math.Cos((Math.PI / 180) * Input.ElementAt(5));
        //                            In_Length.Add(Value);
        //                            ///COS(PI()/180*D15)

        //                        }
        //                        else if (Tube_Section == "Sec 8")
        //                        {
        //                            In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()));
        //                        }
        //                        break;
        //                    case 3:
        //                        if (Tube_Section != "Sec 3" && Tube_Section != "Sec 8" && Tube_Section != "Sec 13")
        //                        {
        //                            In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()) + Correction_of_length_required);
        //                        }
        //                        else
        //                        {
        //                            In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()));
        //                        }
        //                        break;
        //                    case 4:
        //                        if (Tube_Section != "Sec 3")
        //                        {
        //                            In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()) + Correction_of_length_required);
        //                        }
        //                        else
        //                        {
        //                            //R48/COS(PI()/180*D15)//Need to check with other
        //                            Double Value = Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()) / Math.Cos((Math.PI / 180) * Nose_up_Dip);
        //                            In_Length.Add(Value);
        //                        }
        //                        break;
        //                    case 5:
        //                        if (Tube_Section != "Sec 1" && Tube_Section != "Sec 6")
        //                        {
        //                            In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()) + Correction_of_length_required);
        //                        }
        //                        else
        //                        {
        //                            In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()));
        //                        }
        //                        break;
        //                }
        //            }
        //            else if (PID == 2)
        //            {
        //                In_Diameter.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()));
        //            }
        //            else if (PID == 3)
        //            {
        //                In_Thickness.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()));
        //            }
        //            else
        //            {
        //                In_Material.Add(dt_in.Tables[0].Rows[i]["Value"].ToString());
        //            }
        //        }
        //        for (int c = 0; c <= In_Diameter.Count - 1; c++)
        //        {
        //            In_In_Diameter.Add(In_Diameter.ElementAt(c) - 2 * In_Thickness.ElementAt(c));
        //            In_Tube_Area.Add(Math.PI * (In_Diameter.ElementAt(c) / 1000) * In_Length.ElementAt(c) * Platen_SuperHeater_Transevers);
        //        }
        //        In_Sum_Length = In_Length.Sum();
        //        In_Sum_Area = In_Tube_Area.Sum();
        //        Double Total_Area_Calc = In_Sum_Area + In_Sum_Area * (mCal_5C_SC.No_of_loops_per_element - 1);

        //        mCal_5C_SC.PSH_convective_area = Input.ElementAt(6);
        //        Error_Correction_Area = mCal_5C_SC.PSH_convective_area - Total_Area_Calc;
        //        if (Correction_of_length_required > 0.77)
        //        {

        //        }

        //        if (Error_Correction_Area > 0)
        //        {
        //            Correction_of_length_required = Correction_of_length_required + 0.0001;
        //        }
        //        else
        //        {
        //            Correction_of_length_required = Correction_of_length_required - 0.0001;
        //        }

        //    }

        //    while (Error_Correction_Area <= -0.001);


        //    return Tuple.Create<List<Double>, List<Double>, List<Double>, List<Double>, List<Double>, Double, Double>

        //       (In_In_Diameter, In_Tube_Area, In_Diameter, In_Length, Thermal_Con_Value, In_Sum_Area, In_Sum_Length
        //       );
        //}
        //
        public Tuple<List<Double>, List<Double>, List<Double>, List<Double>, List<Double>, Double, Double> Calculation_Area_5C_Outer_Tube()
        {
            //D12
            int Platen_SuperHeater_Transevers = 29;
            DataSet dt = new DataSet();
            dt = mCal_5C_BLL.Get_Input_For_5C_Calculation_Of_Area(BoilerID, ProjectID, SectionID, ObjectiveID, Tube_Type_1, Outer_Tube_Position);
            DataSet Thermal_Conductivity = new DataSet();
            Thermal_Conductivity = mCal_5C_BLL.Get_Thermal_Conductivity_For_5C_Calculation_(BoilerID, ProjectID, SectionID, ObjectiveID, Tube_Type_1, Outer_Tube_Position);
            List<Double> Out_Diameter = new List<Double>();
            List<Double> Out_Thickness = new List<Double>();
            List<Double> Out_Length = new List<Double>();
            List<String> Out_Material = new List<String>();
            List<Double> Thermal_Con_Value = new List<Double>();

            Out_In_Diameter.Clear();
            Out_Tube_Area.Clear();
            for (int i = 0; i <= Thermal_Conductivity.Tables[0].Rows.Count - 1; i++)
            {
                Thermal_Con_Value.Add(Convert.ToDouble(Thermal_Conductivity.Tables[0].Rows[i]["ThermalConductivity"].ToString()));

            }
            if (Tube_Type_1 == 1)
            {
                for (int i = 0; i <= dt.Tables[0].Rows.Count - 1; i++)
                {
                    int PID = Convert.ToInt16(dt.Tables[0].Rows[i]["PID"].ToString());

                    if (PID == 1)
                    {
                        Out_Length.Add(Convert.ToDouble(dt.Tables[0].Rows[i]["Value"].ToString()));
                    }
                    else if (PID == 2)
                    {
                        Out_Diameter.Add(Convert.ToDouble(dt.Tables[0].Rows[i]["Value"].ToString()));
                    }
                    else if (PID == 3)
                    {
                        Out_Thickness.Add(Convert.ToDouble(dt.Tables[0].Rows[i]["Value"].ToString()));
                    }
                    else
                    {
                        Out_Material.Add(dt.Tables[0].Rows[i]["Value"].ToString());
                    }
                }
            }
            if (Tube_Type_1 == 4)
            {
                for (int i = 0; i <= dt.Tables[0].Rows.Count - 1; i++)
                {
                    int PID = Convert.ToInt16(dt.Tables[0].Rows[i]["PID"].ToString());

                    if (PID == 1)
                    {
                        Out_Length.Add(Convert.ToDouble(dt.Tables[0].Rows[i]["Value"].ToString()));
                    }
                    else if (PID == 2)
                    {
                        Out_Diameter.Add(Convert.ToDouble(dt.Tables[0].Rows[i]["Value"].ToString()));
                    }
                    else if (PID == 3)
                    {
                        Out_Thickness.Add(Convert.ToDouble(dt.Tables[0].Rows[i]["Value"].ToString()));
                    }
                    else
                    {
                        Out_Material.Add(dt.Tables[0].Rows[i]["Value"].ToString());
                    }
                }
            }
            for (int c = 0; c <= Out_Diameter.Count - 1; c++)
            {
                Out_In_Diameter.Add(Out_Diameter.ElementAt(c) - 2 * Out_Thickness.ElementAt(c));
                Out_Tube_Area.Add(Math.PI * (Out_Diameter.ElementAt(c) / 1000) * Out_Length.ElementAt(c) * Platen_SuperHeater_Transevers);
            }
            Double Out_Sum_Length = Out_Length.Sum();
            Double Out_Sum_Area = Out_Tube_Area.Sum();
            return Tuple.Create<List<Double>, List<Double>, List<Double>, List<Double>, List<Double>, Double, Double>(Out_In_Diameter, Out_Tube_Area, Out_Diameter, Out_Length, Thermal_Con_Value, Out_Sum_Area, Out_Sum_Length);
        }
        public Tuple<List<Double>, List<Double>, List<Double>, List<Double>, List<Double>, Double, Double> Calculation_Area_5C_Inner_Tube()
        {
            //Input     
            Double Correction_of_length_required = 0.00;
            Double Error_Correction_Area;
            Double In_Sum_Length;
            Double In_Sum_Area;
           // Correction_of_length_required = 0.7815;
            //D12
          //  int Platen_SuperHeater_Transevers = 29;
            DataSet dt_in = new DataSet();
            dt_in = mCal_5C_BLL.Get_Input_For_5C_Calculation_Of_Area(BoilerID, ProjectID, SectionID, ObjectiveID, Tube_Type_2, Inner_Tube_Position);
            List<Double> In_Diameter = new List<Double>();
            List<Double> In_Thickness = new List<Double>();
            List<Double> In_Length = new List<Double>();
            List<String> In_Material = new List<String>();
            List<Double> Out_Diameter = new List<Double>();
            List<Double> Thermal_Con_Value = new List<Double>();
            DataSet Thermal_Conductivity = new DataSet();
            Thermal_Conductivity = mCal_5C_BLL.Get_Thermal_Conductivity_For_5C_Calculation_(BoilerID, ProjectID, SectionID, ObjectiveID, Tube_Type_2, Inner_Tube_Position);
            for (int i = 0; i <= Thermal_Conductivity.Tables[0].Rows.Count - 1; i++)
            {
                Thermal_Con_Value.Add(Convert.ToDouble(Thermal_Conductivity.Tables[0].Rows[i]["ThermalConductivity"].ToString()));
            }
            Tuple<List<Double>, List<Double>, List<Double>, List<Double>, List<Double>, Double, Double> Tube = Calculation_Area_5C_Outer_Tube();
            //(Out_In_Diameter, Out_Tube_Area, Out_Diameter,Out_Length,Thermal_Con_Value, Out_Sum_Area, Out_Sum_Length);

            //Input 5C_Ar


            List<Double> Input = Input_List();
            //D7
            mCal_5C_SC.No_of_loops_per_element = Input.ElementAt(0);
            //D13
            mCal_5C_SC.PSH_convective_area = Input.ElementAt(6);
            //D15
            Double Nose_up_Dip = Input.ElementAt(16);
            //D12
            Double Platen_SuperHeater_Transevers = Input.ElementAt(5);
            //DataSet dt = new DataSet();
            //dt = mCal_5C_BLL.Get_Input_For_5C_Calculation(BoilerID, ProjectID, BoilerLoad, SectionID);
            ////D7
            //mCal_5C_SC.No_of_loops_per_element = Convert.ToDouble(dt.Tables[0].Rows[0]["Value"].ToString());
            ////D13
            //mCal_5C_SC.PSH_convective_area = Convert.ToDouble(dt.Tables[6].Rows[0]["Value"].ToString());
            do
            {
                In_Diameter.Clear();
                In_Thickness.Clear();
                In_Length.Clear();
                In_Material.Clear();
                In_In_Diameter.Clear();
                In_Tube_Area.Clear();
                for (int i = 0; i <= dt_in.Tables[0].Rows.Count - 1; i++)
                {
                    int PID = Convert.ToInt16(dt_in.Tables[0].Rows[i]["PID"].ToString());
                    string Tube_Section = dt_in.Tables[0].Rows[i]["Tube_SectionID"].ToString();
                    if (PID == 1)
                    {
                        switch (Tube_Type_2)
                        {
                            case 1:

                                In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()));

                                break;
                            case 2:
                                if (Tube_Section != "Sec 3" && Tube_Section != "Sec 8")
                                {
                                    In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()) + Correction_of_length_required);
                                }
                                else if (Tube_Section == "Sec 3")
                                {
                                    In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()));
                                    //Double Value = Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()) / Math.Cos((Math.PI / 180) * Input.ElementAt(5));
                                    //In_Length.Add(Value);
                                    ///COS(PI()/180*D15)

                                }
                                else if (Tube_Section == "Sec 8")
                                {
                                    In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()));
                                }
                                break;
                            case 3:
                                if (Tube_Section != "Sec 3" && Tube_Section != "Sec 8" && Tube_Section != "Sec 13")
                                {
                                    In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()) + Correction_of_length_required);
                                }
                                else
                                {
                                    In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()));
                                }
                                break;
                            case 4:
                                if (Tube_Section != "Sec 3")
                                {
                                    In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()) + Correction_of_length_required);
                                }
                                else
                                {
                                    //R48/COS(PI()/180*D15)//Need to check with other
                                    Double Value = Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()) / Math.Cos((Math.PI / 180) * Nose_up_Dip);
                                    In_Length.Add(Value);
                                }
                                break;
                            case 5:
                                if (Tube_Section != "Sec 1" && Tube_Section != "Sec 6")
                                {
                                    In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()) + Correction_of_length_required);
                                }
                                else
                                {
                                    In_Length.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()));
                                }
                                break;
                        }
                    }
                    else if (PID == 2)
                    {
                        In_Diameter.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()));
                    }
                    else if (PID == 3)
                    {
                        In_Thickness.Add(Convert.ToDouble(dt_in.Tables[0].Rows[i]["Value"].ToString()));
                    }
                    else
                    {
                        In_Material.Add(dt_in.Tables[0].Rows[i]["Value"].ToString());
                    }
                }

                for (int c = 0; c <= In_Diameter.Count - 1; c++)
                {
                    In_In_Diameter.Add(In_Diameter.ElementAt(c) - 2 * In_Thickness.ElementAt(c));
                    In_Tube_Area.Add(Math.PI * (In_Diameter.ElementAt(c) / 1000) * In_Length.ElementAt(c) * Platen_SuperHeater_Transevers);
                }
                In_Sum_Length = In_Length.Sum();
                In_Sum_Area = In_Tube_Area.Sum();
                Double Total_Area_Calc = Tube.Item6 + In_Sum_Area * (mCal_5C_SC.No_of_loops_per_element - 1);
                Out_In_Diameter = Tube.Item1;
                Out_Tube_Area = Tube.Item2;
                Out_Diameter = Tube.Item3;
                //D 13

                Error_Correction_Area = mCal_5C_SC.PSH_convective_area - Total_Area_Calc;
               
                if (Correction_of_length_required > 0.77)
                {

                }

                if (Error_Correction_Area > 0)
                {
                    //Correction_of_length_required = Math.Round(Correction_of_length_required + 0.0001, 5);
                    Correction_of_length_required = Correction_of_length_required + 0.01;
                }
                else
                {
                    //Correction_of_length_required = Math.Round(Correction_of_length_required - 0.0001, 5);
                    Correction_of_length_required = Correction_of_length_required - 0.01;
                }

            }

            while (Error_Correction_Area >= -0.01);

            //(Out_In_Diameter, Out_Tube_Area, Out_Diameter,Out_Length,Thermal_Con_Value, Out_Sum_Area, Out_Sum_Length);


            return Tuple.Create<List<Double>, List<Double>, List<Double>, List<Double>, List<Double>, Double, Double>

                (In_In_Diameter, In_Tube_Area, In_Diameter, In_Length, Thermal_Con_Value, In_Sum_Area, In_Sum_Length
                );

        }
    

        public Tuple<List<Double>, List<Double>> Calculation_DR_Heat_Flux(Double Relative_Transvere_Spacing, Double Total_DR_radiation_absorbed_in_SH, Double Design_fuel_consumption, List<int> V_Sec_List)
        {
            List<Double> Absorbed_fraction = new List<Double>();
            List<Double> Absorbed_DR_MW = new List<Double>();
            //D14
            // Relative_Transvere_Spacing = 3.60;
            Double d_s_ratio = 1 / Relative_Transvere_Spacing;
            Double Fraction = 1.57 * d_s_ratio - d_s_ratio * (Math.Asin(d_s_ratio) - (1 / d_s_ratio) + Math.Sqrt(Math.Pow(1 / d_s_ratio, 2) - 1));
            //P 81
            Double Sum_Absorbed_fraction = 0.0;
            for (int c = 0; c <= V_Sec_List.Count - 1; c++)
            {
                Absorbed_fraction.Add(Fraction * Math.Pow((1 - Fraction), (V_Sec_List.ElementAt(c) - 1)));
            }

            Sum_Absorbed_fraction = Absorbed_fraction.Sum();
            //=D19*D21/(1000*P81) 
            Double Total_direct_radiation_absorbed_PSH = Total_DR_radiation_absorbed_in_SH * Design_fuel_consumption / (1000 * Sum_Absorbed_fraction);
            // Double Total_direct_radiation_absorbed_PSH = 0; //For 8C
            for (int c = 0; c <= V_Sec_List.Count - 1; c++)
            {
                Absorbed_DR_MW.Add(Total_direct_radiation_absorbed_PSH * Absorbed_fraction.ElementAt(c));
            }

            return Tuple.Create<List<Double>, List<Double>>(Absorbed_fraction, Absorbed_DR_MW);

        }
        //
        public List<Double> Calc_Ratio_DR_each_section(int Type_OF_Tube, int position, double nose, double avg_location_vertical_direction, double Roof)
        {
            List<Double> Average_of_flux_ratio = new List<Double>();
            List<String> Section_Tubes = new List<String>();
            DataSet dt_in = new DataSet();

            Double avg_Nose = nose;
            Double avg_Nose_Vertical = (nose + avg_location_vertical_direction) / 2;
            Double avg_Roof_Vertical = (Roof + avg_location_vertical_direction) / 2;

            dt_in = mCal_5C_BLL.Get_Input_For_5C_Ratio_DR_each_section(BoilerID, ProjectID, SectionID, ObjectiveID, Type_OF_Tube, position);

            foreach (DataRow row in dt_in.Tables[0].Rows)
            {
                Section_Tubes.Add(row["Tube_SectionID"].ToString());
            }

            string[] strSecID = Section_Tubes.ToArray();
            Array.Sort(strSecID, new AlphanumComparatorFast());
            Section_Tubes = strSecID.ToList();

            if (Type_OF_Tube == 1)
            {
                for (int i = 0; i <= Section_Tubes.Count - 1; i++)
                {
                    string Tube_Section = Section_Tubes.ElementAt(i).ToString();
                    if (Tube_Section == "Sec 3")
                    {
                        Average_of_flux_ratio.Add(avg_Nose);
                    }
                    else if (Tube_Section == "Sec 2" || Tube_Section == "Sec 4")
                    {
                        Average_of_flux_ratio.Add(avg_Nose_Vertical);
                    }
                    else if (Tube_Section == "Sec 1" || Tube_Section == "Sec 5")
                    {
                        Average_of_flux_ratio.Add(avg_Roof_Vertical);
                    }
                }

            }
            else if (Type_OF_Tube == 2)
            {
                for (int i = 0; i <= Section_Tubes.Count - 1; i++)
                {
                    string Tube_Section = Section_Tubes.ElementAt(i).ToString();
                    if (Tube_Section == "Sec 3" || Tube_Section == "Sec 8")
                    {
                        Average_of_flux_ratio.Add(avg_Nose);
                    }
                    else if (Tube_Section == "Sec 2" || Tube_Section == "Sec 4" || Tube_Section == "Sec 7" || Tube_Section == "Sec 9")
                    {
                        Average_of_flux_ratio.Add(avg_Nose_Vertical);
                    }
                    else if (Tube_Section == "Sec 1" || Tube_Section == "Sec 5" || Tube_Section == "Sec 6" || Tube_Section == "Sec 10")
                    {
                        Average_of_flux_ratio.Add(avg_Roof_Vertical);
                    }
                }
            }
            else if (Type_OF_Tube == 3)
            {
                for (int i = 0; i <= Section_Tubes.Count - 1; i++)
                {
                    string Tube_Section = Section_Tubes.ElementAt(i).ToString();
                    if (Tube_Section == "Sec 3" || Tube_Section == "Sec 8" || Tube_Section == "Sec 13")
                    {
                        Average_of_flux_ratio.Add(avg_Nose);
                    }
                    else if (Tube_Section == "Sec 2" || Tube_Section == "Sec 4" || Tube_Section == "Sec 7" || Tube_Section == "Sec 9" || Tube_Section == "Sec 12" || Tube_Section == "Sec 14")
                    {
                        Average_of_flux_ratio.Add(avg_Nose_Vertical);
                    }
                    else if (Tube_Section == "Sec 1" || Tube_Section == "Sec 5" || Tube_Section == "Sec 6" || Tube_Section == "Sec 10" || Tube_Section == "Sec 11" || Tube_Section == "Sec 15")
                    {
                        Average_of_flux_ratio.Add(avg_Roof_Vertical);
                    }
                }
            }
            else if (Type_OF_Tube == 4)
            {
                for (int i = 0; i <= Section_Tubes.Count - 1; i++)
                {
                    string Tube_Section = Section_Tubes.ElementAt(i).ToString();
                    if (Tube_Section == "Sec 3")
                    {
                        Average_of_flux_ratio.Add(avg_Nose);
                    }
                    else if (Tube_Section == "Sec 2" || Tube_Section == "Sec 4" || Tube_Section == "Sec 7")
                    {
                        Average_of_flux_ratio.Add(avg_Nose_Vertical);
                    }
                    else if (Tube_Section == "Sec 1" || Tube_Section == "Sec 5" || Tube_Section == "Sec 6")
                    {
                        Average_of_flux_ratio.Add(avg_Roof_Vertical);
                    }
                }
            }
            else if (Type_OF_Tube == 5)
            {
                for (int i = 0; i <= Section_Tubes.Count - 1; i++)
                {
                    string Tube_Section = Section_Tubes.ElementAt(i).ToString();
                    if (Tube_Section == "Sec 1" || Tube_Section == "Sec 6")
                    {
                        Average_of_flux_ratio.Add(avg_Nose);
                    }
                    else if (Tube_Section == "Sec 2" || Tube_Section == "Sec 5" || Tube_Section == "Sec 7")
                    {
                        Average_of_flux_ratio.Add(avg_Nose_Vertical);
                    }
                    else if (Tube_Section == "Sec 3" || Tube_Section == "Sec 4" || Tube_Section == "Sec 8")
                    {
                        Average_of_flux_ratio.Add(avg_Roof_Vertical);
                    }
                }
            }

            return Average_of_flux_ratio;
        }
        //
        public List<Double> Calc_Ratio_incedent_fluxes(int Type_OF_Tube, int position, double nose, double avg_location_vertical_direction, double Roof)
        {
            List<Double> Average_of_flux_ratio = new List<Double>();
            List<Double> Ratio_incedent_fluxes = new List<Double>();

            Average_of_flux_ratio = Calc_Ratio_DR_each_section(Type_OF_Tube, position, nose, avg_location_vertical_direction, Roof);
            if (Type_OF_Tube == 1)
            {
                Double Avg = Average_of_flux_ratio.ElementAt(0) + Average_of_flux_ratio.ElementAt(1) + Average_of_flux_ratio.ElementAt(2);
                Double Sec1 = Average_of_flux_ratio.ElementAt(0) / Avg;
                Ratio_incedent_fluxes.Add(Sec1);
                Double Sec2 = Average_of_flux_ratio.ElementAt(1) / Avg;
                Ratio_incedent_fluxes.Add(Sec2);
                Double Sec3 = Average_of_flux_ratio.ElementAt(2) / Avg;
                Ratio_incedent_fluxes.Add(Sec3);
                Double Sec4 = Average_of_flux_ratio.ElementAt(3) / (Average_of_flux_ratio.ElementAt(3) + Average_of_flux_ratio.ElementAt(4));
                Ratio_incedent_fluxes.Add(Sec4);
                Double Sec5 = Average_of_flux_ratio.ElementAt(4) / (Average_of_flux_ratio.ElementAt(3) + Average_of_flux_ratio.ElementAt(4));
                Ratio_incedent_fluxes.Add(Sec5);
            }
            else if (Type_OF_Tube == 2)
            {
                Double Avg = (Average_of_flux_ratio.ElementAt(1) + Average_of_flux_ratio.ElementAt(2) + Average_of_flux_ratio.ElementAt(7) + Average_of_flux_ratio.ElementAt(0));
                Double Sec1 = Average_of_flux_ratio.ElementAt(0) / Avg;
                Ratio_incedent_fluxes.Add(Sec1);
                Double Sec2 = Average_of_flux_ratio.ElementAt(1) / Avg;
                Ratio_incedent_fluxes.Add(Sec2);
                Double Sec3 = Average_of_flux_ratio.ElementAt(2) / Avg;
                Ratio_incedent_fluxes.Add(Sec3);
                Double Sec4 = Average_of_flux_ratio.ElementAt(3) / (Average_of_flux_ratio.ElementAt(3) + Average_of_flux_ratio.ElementAt(4));
                Ratio_incedent_fluxes.Add(Sec4);
                Double Sec5 = Average_of_flux_ratio.ElementAt(4) / (Average_of_flux_ratio.ElementAt(3) + Average_of_flux_ratio.ElementAt(4));
                Ratio_incedent_fluxes.Add(Sec5);
                Double Sec6 = Average_of_flux_ratio.ElementAt(5) / (Average_of_flux_ratio.ElementAt(5) + Average_of_flux_ratio.ElementAt(6));
                Ratio_incedent_fluxes.Add(Sec6);
                Double Sec7 = Average_of_flux_ratio.ElementAt(6) / (Average_of_flux_ratio.ElementAt(5) + Average_of_flux_ratio.ElementAt(6));
                Ratio_incedent_fluxes.Add(Sec7);
                Double Sec8 = Average_of_flux_ratio.ElementAt(7) / Avg;
                Ratio_incedent_fluxes.Add(Sec8);
                Double Sec9 = Average_of_flux_ratio.ElementAt(8) / (Average_of_flux_ratio.ElementAt(8) + Average_of_flux_ratio.ElementAt(9));
                Ratio_incedent_fluxes.Add(Sec9);
                Double Sec10 = Average_of_flux_ratio.ElementAt(9) / (Average_of_flux_ratio.ElementAt(8) + Average_of_flux_ratio.ElementAt(9));
                Ratio_incedent_fluxes.Add(Sec10);
            }
            else if (Type_OF_Tube == 3)
            {
                Double Avg = Average_of_flux_ratio.ElementAt(0) + Average_of_flux_ratio.ElementAt(1) + Average_of_flux_ratio.ElementAt(2) + Average_of_flux_ratio.ElementAt(7) + Average_of_flux_ratio.ElementAt(12);
                Double Sec1 = Average_of_flux_ratio.ElementAt(0) / Avg;
                Ratio_incedent_fluxes.Add(Sec1);
                Double Sec2 = Average_of_flux_ratio.ElementAt(1) / Avg;
                Ratio_incedent_fluxes.Add(Sec2);
                Double Sec3 = Average_of_flux_ratio.ElementAt(2) / Avg;
                Ratio_incedent_fluxes.Add(Sec3);
                Double Sec4 = Average_of_flux_ratio.ElementAt(3) / Average_of_flux_ratio.ElementAt(3) + Average_of_flux_ratio.ElementAt(4);
                Ratio_incedent_fluxes.Add(Sec4);
                Double Sec5 = Average_of_flux_ratio.ElementAt(4) / Average_of_flux_ratio.ElementAt(3) + Average_of_flux_ratio.ElementAt(4);
                Ratio_incedent_fluxes.Add(Sec5);
                Double Sec6 = Average_of_flux_ratio.ElementAt(5) / Average_of_flux_ratio.ElementAt(5) + Average_of_flux_ratio.ElementAt(6);
                Ratio_incedent_fluxes.Add(Sec6);
                Double Sec7 = Average_of_flux_ratio.ElementAt(6) / Average_of_flux_ratio.ElementAt(5) + Average_of_flux_ratio.ElementAt(6);
                Ratio_incedent_fluxes.Add(Sec7);
                Double Sec8 = Average_of_flux_ratio.ElementAt(7) / Avg;
                Ratio_incedent_fluxes.Add(Sec8);
                Double Sec9 = Average_of_flux_ratio.ElementAt(8) / Average_of_flux_ratio.ElementAt(8) + Average_of_flux_ratio.ElementAt(9);
                Ratio_incedent_fluxes.Add(Sec9);
                Double Sec10 = Average_of_flux_ratio.ElementAt(9) / Average_of_flux_ratio.ElementAt(8) + Average_of_flux_ratio.ElementAt(9);
                Ratio_incedent_fluxes.Add(Sec10);
                Double Sec11 = Average_of_flux_ratio.ElementAt(10) / Average_of_flux_ratio.ElementAt(10) + Average_of_flux_ratio.ElementAt(11);
                Ratio_incedent_fluxes.Add(Sec11);
                Double Sec12 = Average_of_flux_ratio.ElementAt(11) / Average_of_flux_ratio.ElementAt(10) + Average_of_flux_ratio.ElementAt(11);
                Ratio_incedent_fluxes.Add(Sec12);
                Double Sec13 = Average_of_flux_ratio.ElementAt(12) / Avg;
                Ratio_incedent_fluxes.Add(Sec13);
                Double Sec14 = Average_of_flux_ratio.ElementAt(13) / Average_of_flux_ratio.ElementAt(13) + Average_of_flux_ratio.ElementAt(14);
                Ratio_incedent_fluxes.Add(Sec14);
                Double Sec15 = Average_of_flux_ratio.ElementAt(14) / Average_of_flux_ratio.ElementAt(13) + Average_of_flux_ratio.ElementAt(14);
                Ratio_incedent_fluxes.Add(Sec15);
            }
            else if (Type_OF_Tube == 4)
            {
                Double Avg = Average_of_flux_ratio.ElementAt(0) + Average_of_flux_ratio.ElementAt(1) + Average_of_flux_ratio.ElementAt(2);
                Double Sec1 = Average_of_flux_ratio.ElementAt(0) / Avg;
                Ratio_incedent_fluxes.Add(Sec1);
                Double Sec2 = Average_of_flux_ratio.ElementAt(1) / Avg;
                Ratio_incedent_fluxes.Add(Sec2);
                Double Sec3 = Average_of_flux_ratio.ElementAt(2) / Avg;
                Ratio_incedent_fluxes.Add(Sec3);
                Double Avg2 = Average_of_flux_ratio.ElementAt(3) + Average_of_flux_ratio.ElementAt(4);
                Double Sec4 = Average_of_flux_ratio.ElementAt(3) / Avg2;
                Ratio_incedent_fluxes.Add(Sec4);
                Double Sec5 = Average_of_flux_ratio.ElementAt(4) / Avg2;
                Ratio_incedent_fluxes.Add(Sec5);
                Double avg3 = Average_of_flux_ratio.ElementAt(5) + Average_of_flux_ratio.ElementAt(6);
                Double Sec6 = Average_of_flux_ratio.ElementAt(5) / avg3;
                Ratio_incedent_fluxes.Add(Sec6);
                Double Sec7 = Average_of_flux_ratio.ElementAt(6) / avg3;
                Ratio_incedent_fluxes.Add(Sec7);

            }
            else if (Type_OF_Tube == 5)
            {
                Double Sec1 = Average_of_flux_ratio.ElementAt(0) / (Average_of_flux_ratio.ElementAt(0) + Average_of_flux_ratio.ElementAt(5));
                Ratio_incedent_fluxes.Add(Sec1);
                Double Sec2 = Average_of_flux_ratio.ElementAt(1) / (Average_of_flux_ratio.ElementAt(1) + Average_of_flux_ratio.ElementAt(2));
                Ratio_incedent_fluxes.Add(Sec2);
                Double Sec3 = Average_of_flux_ratio.ElementAt(2) / (Average_of_flux_ratio.ElementAt(1) + Average_of_flux_ratio.ElementAt(2));
                Ratio_incedent_fluxes.Add(Sec3);
                Double Sec4 = Average_of_flux_ratio.ElementAt(3) / (Average_of_flux_ratio.ElementAt(3) + Average_of_flux_ratio.ElementAt(4));
                Ratio_incedent_fluxes.Add(Sec4);
                Double Sec5 = Average_of_flux_ratio.ElementAt(4) / (Average_of_flux_ratio.ElementAt(3) + Average_of_flux_ratio.ElementAt(4));
                Ratio_incedent_fluxes.Add(Sec5);
                Double Sec6 = Average_of_flux_ratio.ElementAt(5) / (Average_of_flux_ratio.ElementAt(0) + Average_of_flux_ratio.ElementAt(5));
                Ratio_incedent_fluxes.Add(Sec6);
                Double Sec7 = Average_of_flux_ratio.ElementAt(6) / (Average_of_flux_ratio.ElementAt(6) + Average_of_flux_ratio.ElementAt(7));
                Ratio_incedent_fluxes.Add(Sec7);
                Double Sec8 = Average_of_flux_ratio.ElementAt(7) / (Average_of_flux_ratio.ElementAt(6) + Average_of_flux_ratio.ElementAt(7));
                Ratio_incedent_fluxes.Add(Sec8);

            }


            return Ratio_incedent_fluxes;
        }
        //
        public Tuple<List<Double>, List<Double>> Calc_Flux_X_Area(int Type_OF_Tube, int position, List<Double> Input_List)
        {
            List<Double> C_Table_Output = new List<double>();
            DataSet dt = new DataSet();

            Stream_Macros stream_Macros_BLL = new Stream_Macros();
            Tuple<List<Double>, List<Double>, List<Double>, List<Double>, List<Double>, Double, Double> Tube_1 = Calculation_Area_5C_Outer_Tube();
            List<Double> Out_In_Diameter = Tube_1.Item1;
            List<Double> Out_Tube_Area = Tube_1.Item2;

            Tuple<List<Double>, List<Double>, List<Double>, List<Double>, List<Double>, Double, Double> Tube_2 = Calculation_Area_5C_Inner_Tube();
            List<Double> In_In_Diameter = Tube_2.Item1;
            List<Double> In_Tube_Area = Tube_2.Item2;


            //Input 5C_A
            //dt = mCal_5C_BLL.Get_Input_For_5C_Calculation(BoilerID, ProjectID, BoilerLoad, SectionID);
            //D7
            mCal_5C_SC.No_of_loops_per_element = Input_List.ElementAt(0);
            //D18
            mCal_5C_SC.Flow_rate_through_PSH = Input_List.ElementAt(7);
            //D22
            mCal_5C_SC.Enthalpy_of_steam_inlet_to_the_PSH = Input_List.ElementAt(11);
            //D23
            mCal_5C_SC.Inlet_pressure_pf_steam = Input_List.ElementAt(12);
            //D24
            mCal_5C_SC.Outlet_pressure = Input_List.ElementAt(13);
            //D25
            mCal_5C_SC.Heat_transfer_coefficient_from_tube_wall_to_steam = Input_List.ElementAt(14);
            //D26
            mCal_5C_SC.Steanm_outlet_temperature = Input_List.ElementAt(15);
            //C42
            mCal_5C_SC.Fraction_of_flow_thorugh_outer__tube = 0.000000;
            //0.164511961902175
            double Assumed = 0.000000;
            do
            {
                //Out_In_Diameter, Out_Tube_Area, Out_Diameter,Out_Length,Thermal_Con_Value, Out_Sum_Area, Out_Sum_Length
                //C43=C42^2*M18/L8^5
                // mCal_5C_SC.Factor_a_L_d5___Outer_loop = Math.Pow(mCal_5C_SC.Fraction_of_flow_thorugh_outer__tube, 2) * (Tube_1.Item7 / Math.Pow(Out_In_Diameter.ElementAt(0), 5));
                mCal_5C_SC.Factor_a_L_d5___Outer_loop = Math.Pow(Assumed, 2) * (Tube_1.Item7 / Math.Pow(Out_In_Diameter.ElementAt(0), 5));
                //C44=((1-C42)/(D7-1))^2*T18/S8^5
                //mCal_5C_SC.Factor_b_L_d5___Inner_loop = Math.Pow(((1 - mCal_5C_SC.Fraction_of_flow_thorugh_outer__tube) / (mCal_5C_SC.No_of_loops_per_element - 1)), 2) * (Tube_2.Item7 / Math.Pow((In_In_Diameter.ElementAt(0)), 5));
                mCal_5C_SC.Factor_b_L_d5___Inner_loop = Math.Pow(((1 - Assumed) / (mCal_5C_SC.No_of_loops_per_element - 1)), 2) * (Tube_2.Item7 / Math.Pow((In_In_Diameter.ElementAt(0)), 5));
                //C45=(C43-C44)/C43*100)
                mCal_5C_SC.Error = (mCal_5C_SC.Factor_a_L_d5___Outer_loop - mCal_5C_SC.Factor_b_L_d5___Inner_loop) / (mCal_5C_SC.Factor_a_L_d5___Outer_loop * 100);

                //mCal_5C_SC.Fraction_of_flow_thorugh_outer__tube = mCal_5C_SC.Fraction_of_flow_thorugh_outer__tube + 0.001;                   
                // mCal_5C_SC.Fraction_of_flow_thorugh_outer__tube = mCal_5C_SC.Fraction_of_flow_thorugh_outer__tube + 0.0001;
                Assumed = Assumed + 0.0001;
                // Correction_of_length_required = Math.Round(Correction_of_length_required + 0.0001, 5);
                Double maths = Math.Abs(mCal_5C_SC.Error);

                mCal_5C_SC.Fraction_of_flow_thorugh_outer__tube = Assumed;
            }

            while (Math.Abs(mCal_5C_SC.Error) >= 0.00001);

            //C46=C42*D18
            mCal_5C_SC.Flow_through_outer_loop = mCal_5C_SC.Fraction_of_flow_thorugh_outer__tube * mCal_5C_SC.Flow_rate_through_PSH;
            //C47=(1-C42)/(D7-1)*D18
            mCal_5C_SC.Flow_through_each_inner__loop = (1 - mCal_5C_SC.Fraction_of_flow_thorugh_outer__tube) / (mCal_5C_SC.No_of_loops_per_element - 1) * mCal_5C_SC.Flow_rate_through_PSH;

            //C52=D23-D24
            mCal_5C_SC.Pressure_drop_from_inlet_to_outlet = mCal_5C_SC.Inlet_pressure_pf_steam - mCal_5C_SC.Outlet_pressure;
            //C53=C52/M18
            mCal_5C_SC.Pressure_drop_per_unit_length_of_outer_tube = mCal_5C_SC.Pressure_drop_from_inlet_to_outlet / Tube_1.Item7;
            //C54=C52/T18
            mCal_5C_SC.Pressure_drop_per_unit_length_of_inner_tube = mCal_5C_SC.Pressure_drop_from_inlet_to_outlet / Tube_2.Item7;


            List<Double> Average_of_flux_ratio = Calc_Ratio_incedent_fluxes(Type_OF_Tube, position, Input_List.ElementAt(16), Input_List.ElementAt(17), Input_List.ElementAt(18));
            //Ratio_incedent_fluxes = Calc_Ratio_incedent_fluxes(Type_OF_Tube, nose, avg_location_vertical_direction, Roof);
            List<Double> Flux_X_Area = new List<double>();
            for (int c = 0; c <= Average_of_flux_ratio.Count - 1; c++)
            {
                if (position == 1)
                {
                    Flux_X_Area.Add(Out_Tube_Area.ElementAt(c) * Average_of_flux_ratio.ElementAt(c));
                }
                else
                {
                    Flux_X_Area.Add(In_Tube_Area.ElementAt(c) * Average_of_flux_ratio.ElementAt(c));
                }
            }



            C_Table_Output.Add(mCal_5C_SC.Flow_through_outer_loop);
            C_Table_Output.Add(mCal_5C_SC.Flow_through_each_inner__loop);
            C_Table_Output.Add(mCal_5C_SC.Pressure_drop_per_unit_length_of_outer_tube);
            C_Table_Output.Add(mCal_5C_SC.Pressure_drop_per_unit_length_of_inner_tube);
            C_Table_Output.Add(mCal_5C_SC.Inlet_pressure_pf_steam);
            C_Table_Output.Add(mCal_5C_SC.Enthalpy_of_steam_inlet_to_the_PSH);
            C_Table_Output.Add(mCal_5C_SC.Heat_transfer_coefficient_from_tube_wall_to_steam);
            C_Table_Output.Add(mCal_5C_SC.Flow_rate_through_PSH);
            C_Table_Output.Add(mCal_5C_SC.Outlet_pressure);
            C_Table_Output.Add(mCal_5C_SC.Steanm_outlet_temperature);



            return Tuple.Create<List<Double>, List<Double>>(Flux_X_Area, C_Table_Output);
        }
        //
        public List<Double> Calc_Ratio_of_absorbed_flux(int Type_OF_Tube, int position, List<Double> Input_List)
        {
            Tuple<List<Double>, List<Double>> C_table_OP = Calc_Flux_X_Area(Type_OF_Tube, position, Input_List);
            List<Double> Flux_X_Area = new List<Double>();
            List<Double> Ratio_of_absorbed_flux = new List<Double>();

            Flux_X_Area = C_table_OP.Item1;
            if (Type_OF_Tube == 1)
            {
                Double Avg = Flux_X_Area.ElementAt(0) + Flux_X_Area.ElementAt(1) + Flux_X_Area.ElementAt(2);
                Double Sec1 = Flux_X_Area.ElementAt(0) / Avg;
                Ratio_of_absorbed_flux.Add(Sec1);
                Double Sec2 = Flux_X_Area.ElementAt(1) / Avg;
                Ratio_of_absorbed_flux.Add(Sec2);
                Double Sec3 = Flux_X_Area.ElementAt(2) / Avg;
                Ratio_of_absorbed_flux.Add(Sec3);
                Double Sec4 = Flux_X_Area.ElementAt(3) / (Flux_X_Area.ElementAt(3) + Flux_X_Area.ElementAt(4));
                Ratio_of_absorbed_flux.Add(Sec4);
                Double Sec5 = Flux_X_Area.ElementAt(4) / (Flux_X_Area.ElementAt(3) + Flux_X_Area.ElementAt(4));
                Ratio_of_absorbed_flux.Add(Sec5);
            }
            else if (Type_OF_Tube == 2)
            {
                Double Avg = (Flux_X_Area.ElementAt(1) + Flux_X_Area.ElementAt(2) + Flux_X_Area.ElementAt(7) + Flux_X_Area.ElementAt(0));
                Double Sec1 = Flux_X_Area.ElementAt(0) / Avg;
                Ratio_of_absorbed_flux.Add(Sec1);
                Double Sec2 = Flux_X_Area.ElementAt(1) / Avg;
                Ratio_of_absorbed_flux.Add(Sec2);
                Double Sec3 = Flux_X_Area.ElementAt(2) / Avg;
                Ratio_of_absorbed_flux.Add(Sec3);
                Double Sec4 = Flux_X_Area.ElementAt(3) / (Flux_X_Area.ElementAt(3) + Flux_X_Area.ElementAt(4));
                Ratio_of_absorbed_flux.Add(Sec4);
                Double Sec5 = Flux_X_Area.ElementAt(4) / (Flux_X_Area.ElementAt(3) + Flux_X_Area.ElementAt(4));
                Ratio_of_absorbed_flux.Add(Sec5);
                Double Sec6 = Flux_X_Area.ElementAt(5) / (Flux_X_Area.ElementAt(5) + Flux_X_Area.ElementAt(6));
                Ratio_of_absorbed_flux.Add(Sec6);
                Double Sec7 = Flux_X_Area.ElementAt(6) / (Flux_X_Area.ElementAt(5) + Flux_X_Area.ElementAt(6));
                Ratio_of_absorbed_flux.Add(Sec7);
                Double Sec8 = Flux_X_Area.ElementAt(7) / Avg;
                Ratio_of_absorbed_flux.Add(Sec8);
                Double Sec9 = Flux_X_Area.ElementAt(8) / (Flux_X_Area.ElementAt(8) + Flux_X_Area.ElementAt(9));
                Ratio_of_absorbed_flux.Add(Sec9);
                Double Sec10 = Flux_X_Area.ElementAt(9) / (Flux_X_Area.ElementAt(8) + Flux_X_Area.ElementAt(9));
                Ratio_of_absorbed_flux.Add(Sec10);
            }
            else if (Type_OF_Tube == 3)
            {
                Double Avg = (Flux_X_Area.ElementAt(0) + Flux_X_Area.ElementAt(1) + Flux_X_Area.ElementAt(2) + Flux_X_Area.ElementAt(7) + Flux_X_Area.ElementAt(12));
                Double Sec1 = Flux_X_Area.ElementAt(0) / Avg;
                Ratio_of_absorbed_flux.Add(Sec1);
                Double Sec2 = Flux_X_Area.ElementAt(1) / Avg;
                Ratio_of_absorbed_flux.Add(Sec2);
                Double Sec3 = Flux_X_Area.ElementAt(2) / Avg;
                Ratio_of_absorbed_flux.Add(Sec3);
                Double Sec4 = Flux_X_Area.ElementAt(3) / (Flux_X_Area.ElementAt(3) + Flux_X_Area.ElementAt(4));
                Ratio_of_absorbed_flux.Add(Sec4);
                Double Sec5 = Flux_X_Area.ElementAt(4) / (Flux_X_Area.ElementAt(3) + Flux_X_Area.ElementAt(4));
                Ratio_of_absorbed_flux.Add(Sec5);
                Double Sec6 = Flux_X_Area.ElementAt(5) / (Flux_X_Area.ElementAt(5) + Flux_X_Area.ElementAt(6));
                Ratio_of_absorbed_flux.Add(Sec6);
                Double Sec7 = Flux_X_Area.ElementAt(6) / (Flux_X_Area.ElementAt(5) + Flux_X_Area.ElementAt(6));
                Ratio_of_absorbed_flux.Add(Sec7);
                Double Sec8 = Flux_X_Area.ElementAt(7) / Avg;
                Ratio_of_absorbed_flux.Add(Sec8);
                Double Sec9 = Flux_X_Area.ElementAt(8) / (Flux_X_Area.ElementAt(8) + Flux_X_Area.ElementAt(9));
                Ratio_of_absorbed_flux.Add(Sec9);
                Double Sec10 = Flux_X_Area.ElementAt(9) / (Flux_X_Area.ElementAt(8) + Flux_X_Area.ElementAt(9));
                Ratio_of_absorbed_flux.Add(Sec10);
                Double Sec11 = Flux_X_Area.ElementAt(10) / (Flux_X_Area.ElementAt(10) + Flux_X_Area.ElementAt(11));
                Ratio_of_absorbed_flux.Add(Sec11);
                Double Sec12 = Flux_X_Area.ElementAt(11) / (Flux_X_Area.ElementAt(10) + Flux_X_Area.ElementAt(11));
                Ratio_of_absorbed_flux.Add(Sec12);
                Double Sec13 = Flux_X_Area.ElementAt(12) / Avg;
                Ratio_of_absorbed_flux.Add(Sec13);
                Double Sec14 = Flux_X_Area.ElementAt(13) / (Flux_X_Area.ElementAt(13) + Flux_X_Area.ElementAt(14));
                Ratio_of_absorbed_flux.Add(Sec14);
                Double Sec15 = Flux_X_Area.ElementAt(14) / (Flux_X_Area.ElementAt(13) + Flux_X_Area.ElementAt(14));
                Ratio_of_absorbed_flux.Add(Sec15);
            }
            else if (Type_OF_Tube == 4)
            {
                Double Avg = (Flux_X_Area.ElementAt(0) + Flux_X_Area.ElementAt(1) + Flux_X_Area.ElementAt(2));
                Double Sec1 = Flux_X_Area.ElementAt(0) / Avg;
                Ratio_of_absorbed_flux.Add(Sec1);
                Double Sec2 = Flux_X_Area.ElementAt(1) / Avg;
                Ratio_of_absorbed_flux.Add(Sec2);
                Double Sec3 = Flux_X_Area.ElementAt(2) / Avg;
                Ratio_of_absorbed_flux.Add(Sec3);
                Double Avg2 = Flux_X_Area.ElementAt(3) + Flux_X_Area.ElementAt(4);
                Double Sec4 = Flux_X_Area.ElementAt(3) / Avg2;
                Ratio_of_absorbed_flux.Add(Sec4);
                Double Sec5 = Flux_X_Area.ElementAt(4) / Avg2;
                Double avg3 = Flux_X_Area.ElementAt(5) + Flux_X_Area.ElementAt(6);
                Ratio_of_absorbed_flux.Add(Sec5);
                Double Sec6 = Flux_X_Area.ElementAt(5) / avg3;
                Ratio_of_absorbed_flux.Add(Sec6);
                Double Sec7 = Flux_X_Area.ElementAt(6) / avg3;
                Ratio_of_absorbed_flux.Add(Sec7);



            }
            else if (Type_OF_Tube == 5)
            {
                Double Sec1 = Flux_X_Area.ElementAt(0) / (Flux_X_Area.ElementAt(0) + Flux_X_Area.ElementAt(5));
                Ratio_of_absorbed_flux.Add(Sec1);
                Double Sec2 = Flux_X_Area.ElementAt(1) / (Flux_X_Area.ElementAt(1) + Flux_X_Area.ElementAt(2));
                Ratio_of_absorbed_flux.Add(Sec2);
                Double Sec3 = Flux_X_Area.ElementAt(2) / (Flux_X_Area.ElementAt(1) + Flux_X_Area.ElementAt(2));
                Ratio_of_absorbed_flux.Add(Sec3);
                Double Sec4 = Flux_X_Area.ElementAt(3) / (Flux_X_Area.ElementAt(3) + Flux_X_Area.ElementAt(4));
                Ratio_of_absorbed_flux.Add(Sec4);
                Double Sec5 = Flux_X_Area.ElementAt(4) / (Flux_X_Area.ElementAt(3) + Flux_X_Area.ElementAt(4));
                Ratio_of_absorbed_flux.Add(Sec5);
                Double Sec6 = Flux_X_Area.ElementAt(5) / (Flux_X_Area.ElementAt(0) + Flux_X_Area.ElementAt(5));
                Ratio_of_absorbed_flux.Add(Sec6);
                Double Sec7 = Flux_X_Area.ElementAt(6) / (Flux_X_Area.ElementAt(6) + Flux_X_Area.ElementAt(7));
                Ratio_of_absorbed_flux.Add(Sec7);
                Double Sec8 = Flux_X_Area.ElementAt(7) / (Flux_X_Area.ElementAt(6) + Flux_X_Area.ElementAt(7));
                Ratio_of_absorbed_flux.Add(Sec8);

            }
            return Ratio_of_absorbed_flux;
        }
        //
        public List<Double> Calculation_AVG_DR_FLUX(Dictionary<string, List<int>> Pairs_Vertical_Section, List<int> V_Sec_List, int Sec, int position, int Tube_Type)
        {
            Tuple<List<Double>, List<Double>, List<Double>, List<Double>, List<Double>, Double, Double> Cal_Area_1 = Calculation_Area_5C_Outer_Tube();
            // //(Out_In_Diameter, Out_Tube_Area, Out_Diameter,Out_Length,Thermal_Con_Value, Out_Sum_Area, Out_Sum_Length);

            List<Double> Out_OD = new List<Double>();
            List<Double> Out_Tube_Area = new List<Double>();
            Out_OD = Cal_Area_1.Item3;
            Out_Tube_Area = Cal_Area_1.Item2;

            Tuple<List<Double>, List<Double>, List<Double>, List<Double>, List<Double>, Double, Double> Cal_Area2 = Calculation_Area_5C_Inner_Tube();
            List<Double> In_OD = new List<Double>();
            List<Double> In_Tube_Area = new List<Double>();
            In_OD = Cal_Area2.Item3;
            In_Tube_Area = Cal_Area2.Item2;

            List<Double> Input_List_var = Input_List();
            mCal_5C_SC.nose = Input_List_var.ElementAt(16);
            mCal_5C_SC.avg_location_vertical_direction = Input_List_var.ElementAt(17);
            mCal_5C_SC.Roof = Input_List_var.ElementAt(18);

            List<Double> Ratio_of_absorbed_flux_Out = Calc_Ratio_of_absorbed_flux(Tube_Type_1, Outer_Tube_Position, Input_List_var);
            List<Double> Ratio_of_absorbed_flux_In = Calc_Ratio_of_absorbed_flux(Tube_Type_2, Inner_Tube_Position, Input_List_var);


            List<int> Pair = new List<int>();
            List<Double> avg_DR_flux_Kw_m2 = new List<Double>();

            //E----------------->avg DR flux (Kw/m2) 


            Tuple<List<Double>, List<Double>> Heat_Flux = Calculation_DR_Heat_Flux(mCal_5C_SC.Relative_transverse_pitch, mCal_5C_SC.Total_D_R_radiation_absorbed_in_SH, mCal_5C_SC.Design_fuel_consumption, V_Sec_List);
            List<Double> Absorbed_fraction = Heat_Flux.Item1;
            List<Double> Absorbed_DR_MW = Heat_Flux.Item2;


            int No_Loop = Pairs_Vertical_Section.Count;
            int Loop_Count = 1;
            if (position == 1)
            {
                Loop_Count = 1;
                No_Loop = 1;

            }
            else
            {
                Loop_Count = 2;

            }
            for (int i = Loop_Count; i <= No_Loop; i++)
            {
                Pair = Pairs_Vertical_Section["Sec" + i];
                if (Tube_Type == 1)
                {
                    for (int j = 0; j <= Sec - 1; j++)
                    {

                        if (j == 0 || j == 1 || j == 2)
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(0) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(0) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }
                        }
                        else
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(1) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(1) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }

                        }
                    }
                    break;
                }
                else if (Tube_Type == 2)
                {
                    for (int j = 0; j <= Sec - 1; j++)
                    {

                        if (j == 0 || j == 1 || j == 2 || j == 7)
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(0) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(0) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }

                        }
                        else if (j == 3 || j == 4)
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(1) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(1) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }

                        }
                        else if (j == 5 || j == 6)
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(2) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(2) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }
                        }
                        else
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(3) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(3) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }
                        }
                    }
                }
                else if (Tube_Type == 3)
                {
                    for (int j = 0; j <= Sec - 1; j++)
                    {

                        if (j == 0 || j == 1 || j == 2 || j == 7 || j == 12)
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(0) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(0) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }

                        }
                        else if (j == 3 || j == 4)
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(1) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(1) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }


                        }
                        else if (j == 5 || j == 6)
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(2) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(2) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }

                        }
                        else if (j == 8 || j == 9)
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(3) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(3) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }
                        }
                        else if (j == 10 || j == 11)
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(4) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(4) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }

                        }
                        else
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(5) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(5) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }

                        }
                    }

                }
                else if (Tube_Type == 4)
                {
                    for (int j = 0; j <= Sec - 1; j++)
                    {

                        if (j == 0 || j == 1 || j == 2)
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(0) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));

                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(0) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }
                        }
                        else if (j == 3 || j == 4)
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(1) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));

                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(1) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }
                        }
                        else
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(2) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));

                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(2) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }
                        }


                    }
                }
                else if (Tube_Type == 5)
                {
                    for (int j = 0; j <= Sec - 1; j++)
                    {

                        if (j == 0 || j == 1 || j == 2 || j == 5)
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(0) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(0) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }
                        }
                        else if (j == 3 || j == 4)
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(1) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(2) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }

                        }

                        else
                        {
                            if (position == 1)
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(2) - 1) * 1000 * Ratio_of_absorbed_flux_Out.ElementAt(j)) / Out_Tube_Area.ElementAt(j));
                            }
                            else
                            {
                                avg_DR_flux_Kw_m2.Add((Absorbed_DR_MW.ElementAt(Pair.ElementAt(2) - 1) * 1000 * Ratio_of_absorbed_flux_In.ElementAt(j)) / In_Tube_Area.ElementAt(j));
                            }

                        }
                    }

                }

            }
            return avg_DR_flux_Kw_m2;
        }
        //
        public List<Double> Calculation_total_flux(List<Double> avg_DR_flux_Kw_m2_Out, Double Constant_convection_flux)
        {
            List<Double> total_flux_Kw_m = new List<Double>();
            for (int i = 0; i <= avg_DR_flux_Kw_m2_Out.Count - 1; i++)
            {
                total_flux_Kw_m.Add(avg_DR_flux_Kw_m2_Out.ElementAt(i) + Constant_convection_flux);
            }

            return total_flux_Kw_m;
        }
        //
        public List<Double> energy_absorbed_kW_(List<Double> avg_DR_flux_Kw_m2, List<Double> Tube_Area)
        {
            List<Double> energy_absorbed_kW = new List<Double>();
            int i = 0;
            while (i <= avg_DR_flux_Kw_m2.Count - 1)
            {
                for (int j = 0; j <= Tube_Area.Count - 1; j++)
                {

                    energy_absorbed_kW.Add(avg_DR_flux_Kw_m2.ElementAt(i) * Tube_Area.ElementAt(j));
                    i++;
                }
            }

            return energy_absorbed_kW;
        }
        //
        public List<Double> enthalpy_increment(List<Double> energy_absorbed_kW, List<Double> C_table_Output, int pos)
        {
            List<Double> enthalpy_increment = new List<Double>();
            if (pos == 1)
            {
                for (int i = 0; i <= energy_absorbed_kW.Count - 1; i++)
                {
                    //=G125*3600/$C$46
                    enthalpy_increment.Add(energy_absorbed_kW.ElementAt(i) * 3600 / C_table_Output.ElementAt(0));
                }
            }
            else
            {
                for (int i = 0; i <= energy_absorbed_kW.Count - 1; i++)
                {
                    //=G125*3600/$C$46
                    enthalpy_increment.Add(energy_absorbed_kW.ElementAt(i) * 3600 / C_table_Output.ElementAt(1));
                }

            }
            return enthalpy_increment;
        }
        //
        public Tuple<List<Double>, List<Double>> outlet_enthalpy_Outlet_Pressure(List<Double> enthalpy_increment_KJ_Kg, List<Double> Diameter, List<Double> Pressure_Out, int position, int Tube_Type)
        {
            List<Double> outlet_enthalpy_Out = new List<Double>();
            List<Double> outlet_enthalpy_IN = new List<Double>();
            List<Double> Outlet_Pressure_Out = new List<Double>();
            List<Double> Outlet_Pressure_IN = new List<Double>();
            if (position == 1)
            {

                if (Tube_Type_1 == 2)
                {

                    outlet_enthalpy_Out.Add(enthalpy_increment_KJ_Kg.ElementAt(0) + Pressure_Out.ElementAt(5));

                    //outlet_enthalpy_Out.Add(enthalpy_increment_KJ_Kg.ElementAt(0) + 3366.1);

                    for (int i = 1; i <= enthalpy_increment_KJ_Kg.Count - 1; i++)
                    {
                        outlet_enthalpy_Out.Add(outlet_enthalpy_Out.ElementAt(i - 1) + enthalpy_increment_KJ_Kg.ElementAt(i));
                    }
                    int k = 0;
                    while (k <= enthalpy_increment_KJ_Kg.Count - 1)
                    {
                        if (k == 0 || k == 5 || k == 7 || k == 9)
                        {
                            Outlet_Pressure_Out.Add(Pressure_Out.ElementAt(4) - Pressure_Out.ElementAt(2) * Diameter.ElementAt(0));
                        }
                        //Outlet_Pressure_Out.Add(3.7 - Pressure_Out.ElementAt(2) * Diameter.ElementAt(0));

                        else
                        {
                            Outlet_Pressure_Out.Add(Outlet_Pressure_Out.ElementAt(k - 1) - Pressure_Out.ElementAt(2) * Diameter.ElementAt(k));
                        }
                        k++;
                    }

                }

                else
                {
                    //=$D$22+H125
                    //=$D$23-$C$53*C125
                    outlet_enthalpy_Out.Add(enthalpy_increment_KJ_Kg.ElementAt(0) + Pressure_Out.ElementAt(5));

                    //outlet_enthalpy_Out.Add(enthalpy_increment_KJ_Kg.ElementAt(0) + 3366.1);

                    for (int i = 1; i <= enthalpy_increment_KJ_Kg.Count - 1; i++)
                    {
                        outlet_enthalpy_Out.Add(outlet_enthalpy_Out.ElementAt(i - 1) + enthalpy_increment_KJ_Kg.ElementAt(i));
                    }
                    Outlet_Pressure_Out.Add(Pressure_Out.ElementAt(4) - Pressure_Out.ElementAt(2) * Diameter.ElementAt(0));
                    //Outlet_Pressure_Out.Add(3.7 - Pressure_Out.ElementAt(2) * Diameter.ElementAt(0));

                    for (int i = 1; i <= Diameter.Count - 1; i++)
                    {
                        Outlet_Pressure_Out.Add(Outlet_Pressure_Out.ElementAt(i - 1) - Pressure_Out.ElementAt(2) * Diameter.ElementAt(i));
                    }
                }

                return Tuple.Create<List<Double>, List<Double>>(outlet_enthalpy_Out, Outlet_Pressure_Out);


            }
            else
            {
                int p = 0;
                while (p <= enthalpy_increment_KJ_Kg.Count - 1)
                {
                    for (int i = 0; i <= Diameter.Count - 1; i++)
                    {
                        if (i == 0)
                        {
                            outlet_enthalpy_IN.Add(enthalpy_increment_KJ_Kg.ElementAt(p) + Pressure_Out.ElementAt(5));
                            //outlet_enthalpy_IN.Add(enthalpy_increment_KJ_Kg.ElementAt(p) + 3366.1);
                            p++;
                        }
                        else
                        {
                            outlet_enthalpy_IN.Add(outlet_enthalpy_IN.ElementAt(p - 1) + enthalpy_increment_KJ_Kg.ElementAt(p));
                            p++;
                        }
                    }
                }
                int k = 0;
                while (k <= enthalpy_increment_KJ_Kg.Count - 1)
                {
                    for (int i = 0; i <= Diameter.Count - 1; i++)
                    {
                        if (i == 0)
                        {
                            Outlet_Pressure_IN.Add(Pressure_Out.ElementAt(4) - Pressure_Out.ElementAt(3) * Diameter.ElementAt(0));
                            // Outlet_Pressure_Out.Add(Pressure_Out.ElementAt(4) - Pressure_Out.ElementAt(2) * Diameter.ElementAt(0));
                            // Outlet_Pressure_IN.Add(3.7 - Pressure_Out.ElementAt(3) * Diameter.ElementAt(0));

                            k++;
                        }
                        else
                        {
                            Outlet_Pressure_IN.Add(Outlet_Pressure_IN.ElementAt(i - 1) - Pressure_Out.ElementAt(3) * Diameter.ElementAt(i));
                            k++;
                        }
                    }

                }
                return Tuple.Create<List<Double>, List<Double>>(outlet_enthalpy_IN, Outlet_Pressure_IN);
            }

        }
        //
        public List<Double> steam_temp_temperature(List<Double> outlet_enthalpy, List<Double> Outlet_Pressure)
        {
            //=T_ph(J125*10,I125)
            List<Double> steam_temp = new List<Double>();
            Stream_Macros stream_Macros_BLL = new Stream_Macros();
            for (int i = 0; i <= outlet_enthalpy.Count - 1; i++)
            {
                steam_temp.Add(stream_Macros_BLL.T_ph(Outlet_Pressure.ElementAt(i) * 10, outlet_enthalpy.ElementAt(i)));
            }
            return steam_temp;

        }
        //
        public List<Double> inside_film_drop(List<double> Heat_Flux, List<double> Flux_X_Area, List<double> OD, List<double> ID)
        {
            //=(1/$D$25)*F125*1000*(J8/L8)
            List<Double> inside_film_Tube = new List<double>();
            int j = 0;
            while (j <= Flux_X_Area.Count - 1)
            {

                for (int i = 0; i <= OD.Count - 1; i++)
                {

                    inside_film_Tube.Add((1 / Heat_Flux.ElementAt(6)) * Flux_X_Area.ElementAt(j) * 1000 * (OD.ElementAt(i) / ID.ElementAt(i)));
                    j++;
                }
            }
            return inside_film_Tube;
        }
        //
        public List<Double> Tube_wall_drop(List<double> Flux_X_Area, List<double> OD, List<double> ID, List<double> Thermal)
        {
            //J8*LN(J8/L8)*10^-3/(2*P8)*F125*1000
            List<Double> Tube_wall_Tube = new List<double>();
            int j = 0;
            while (j <= Flux_X_Area.Count - 1)
            {

                for (int i = 0; i <= OD.Count - 1; i++)
                {
                    Tube_wall_Tube.Add(OD.ElementAt(i) * Math.Log(OD.ElementAt(i) / ID.ElementAt(i)) * Math.Pow(10, -3) / (2 * Thermal.ElementAt(i)) * Flux_X_Area.ElementAt(j) * 1000);
                    j++;
                }
            }
            return Tube_wall_Tube;
        }
        //
        public List<Double> Total_Drop(List<double> Inside_Film_Drop, List<double> Tube_Wall_Drop)
        {
            //=L125+M125
            List<double> Total_Drop_Tube = new List<double>();
            for (int i = 0; i <= Inside_Film_Drop.Count - 1; i++)
            {
                Total_Drop_Tube.Add(Inside_Film_Drop.ElementAt(i) + Tube_Wall_Drop.ElementAt(i));
            }

            return Total_Drop_Tube;
        }
        //
        public List<Double> Metal_temp(List<double> Total_Drop, List<double> Steam_Temp)
        {
            //=K125+N125
            List<double> Metal_temp = new List<double>();
            for (int i = 0; i <= Steam_Temp.Count - 1; i++)
            {
                Metal_temp.Add(Total_Drop.ElementAt(i) + Steam_Temp.ElementAt(i));
            }

            return Metal_temp;
        }

        //All Function Loop 
        public void Steam_Tempreture_Calculation_Same_Tube_Type(Dictionary<string, List<int>> Pairs_Vertical_Section, List<int> V_Sec_List, Double Constant_convection_flux, int No_Of_Loop_Element, List<Double> Input_cal)
        {

            //Area_Outer_Tube 

            Tuple<List<Double>, List<Double>, List<Double>, List<Double>, List<Double>, Double, Double> Cal_Area_1 = Calculation_Area_5C_Outer_Tube();
            // //(Out_In_Diameter, Out_Tube_Area, Out_Diameter,Out_Length,Thermal_Con_Value, Out_Sum_Area, Out_Sum_Length);

            List<Double> Out_OD = Cal_Area_1.Item3;
            List<Double> Out_Tube_Area = Cal_Area_1.Item2;
            List<Double> Out_ID = Cal_Area_1.Item1;
            List<Double> Out_Thermal = Cal_Area_1.Item5;
            List<Double> Out_Length = Cal_Area_1.Item4;


            //Area_inner Tube

            Tuple<List<Double>, List<Double>, List<Double>, List<Double>, List<Double>, Double, Double> Cal_Area = Calculation_Area_5C_Inner_Tube();
            List<Double> In_OD = Cal_Area.Item3;
            List<Double> In_Tube_Area = Cal_Area.Item2;
            List<Double> In_ID = Cal_Area.Item1;
            List<Double> In_Thermal = Cal_Area.Item5;
            List<Double> In_Length = Cal_Area.Item4;

            List<Double> Average_of_flux_ratio_Tube_Out = Calc_Ratio_DR_each_section(Tube_Type_1, Outer_Tube_Position, mCal_5C_SC.nose, mCal_5C_SC.avg_location_vertical_direction, mCal_5C_SC.Roof);
            List<Double> Average_of_flux_ratio_Tube_In = Calc_Ratio_DR_each_section(Tube_Type_2, Inner_Tube_Position, mCal_5C_SC.nose, mCal_5C_SC.avg_location_vertical_direction, mCal_5C_SC.Roof);
            List<Double> Ratio_incedent_fluxes_Tube_Out = Calc_Ratio_incedent_fluxes(Tube_Type_1, Outer_Tube_Position, mCal_5C_SC.nose, mCal_5C_SC.avg_location_vertical_direction, mCal_5C_SC.Roof);
            List<Double> Ratio_incedent_fluxes_Tube_In = Calc_Ratio_incedent_fluxes(Tube_Type_2, Inner_Tube_Position, mCal_5C_SC.nose, mCal_5C_SC.avg_location_vertical_direction, mCal_5C_SC.Roof);


            List<Double> Flux_X_Area_Tube_Out = new List<Double>();
            List<Double> Flux_X_Area_Tube_In = new List<Double>();


            Tuple<List<Double>, List<Double>> Flux_Area = Calc_Flux_X_Area(Tube_Type_1, Outer_Tube_Position, Input_cal);
            List<Double> C_Table_output = Flux_Area.Item2;

            DataSet dt_Sec = new DataSet();
            dt_Sec = mCal_5C_BLL.Get_Input_For_5C_Calculation_Vertical_Section(Tube_Type_1, Tube_Type_2);
            int Sec_1 = Convert.ToInt16(dt_Sec.Tables[0].Rows[0][1].ToString());
            int Sec_2 = 0;
            if (Tube_Type_1 == Tube_Type_2)
            {
                Sec_2 = Convert.ToInt16(dt_Sec.Tables[0].Rows[0][1].ToString());
            }
            else
            {
                Sec_2 = Convert.ToInt16(dt_Sec.Tables[0].Rows[1][1].ToString());
            }

            //Possition for inner or outer

            //E avg DR flux (Kw/m2)
            List<Double> avg_DR_flux_Kw_m2_Out = Calculation_AVG_DR_FLUX(Pairs_Vertical_Section, V_Sec_List, Sec_1, 1, Tube_Type_1);
            List<Double> avg_DR_flux_Kw_m2_IN = Calculation_AVG_DR_FLUX(Pairs_Vertical_Section, V_Sec_List, Sec_2, 2, Tube_Type_2);

            //F  total flux (Kw/m2)
            List<Double> total_flux_Kw_m2_Out = Calculation_total_flux(avg_DR_flux_Kw_m2_Out, Constant_convection_flux);

            List<Double> total_flux_Kw_m2_IN = Calculation_total_flux(avg_DR_flux_Kw_m2_IN, Constant_convection_flux);



            //G  energy_absorbed_kW_

            List<Double> energy_absorbed_kW__Out = energy_absorbed_kW_(total_flux_Kw_m2_Out, Out_Tube_Area);
            List<Double> energy_absorbed_kW__IN = energy_absorbed_kW_(total_flux_Kw_m2_IN, In_Tube_Area);


            //H enthalpy increment(KJ/Kg)
            List<Double> Ratio_of_absorbed_flux_Out = Calc_Ratio_of_absorbed_flux(Tube_Type_1, Outer_Tube_Position, Input_cal);
            List<Double> Ratio_of_absorbed_flux_in = Calc_Ratio_of_absorbed_flux(Tube_Type_2, Inner_Tube_Position, Input_cal);





            List<Double> enthalpy_increment_KJ_Kg_Out = enthalpy_increment(energy_absorbed_kW__Out, C_Table_output, 1);
            List<Double> enthalpy_increment_KJ_Kg_IN = enthalpy_increment(energy_absorbed_kW__IN, C_Table_output, 2);

            //outlet enthalpy (kJ/kg)	Outlet Pressure(Mpa)

            Tuple<List<Double>, List<Double>> Enthalpy_Pressure_1 = outlet_enthalpy_Outlet_Pressure(enthalpy_increment_KJ_Kg_Out, Out_Length, C_Table_output, 1, Tube_Type_1);
            Tuple<List<Double>, List<Double>> Enthalpy_Pressure_2 = outlet_enthalpy_Outlet_Pressure(enthalpy_increment_KJ_Kg_IN, In_Length, C_Table_output, 2, Tube_Type_1);

            List<Double> outlet_enthalpy_Out = Enthalpy_Pressure_1.Item1;
            List<Double> Outlet_Pressure_Out = Enthalpy_Pressure_1.Item2;
            List<Double> outlet_enthalpy_IN = Enthalpy_Pressure_2.Item1;
            List<Double> Outlet_Pressure_IN = Enthalpy_Pressure_2.Item2;



            //steam temp temperature outlet of section
            List<Double> steam_temp_Out = steam_temp_temperature(outlet_enthalpy_Out, Outlet_Pressure_Out);
            List<Double> steam_temp_IN = steam_temp_temperature(outlet_enthalpy_IN, Outlet_Pressure_IN);



            //inside_film_Drop
            List<Double> inside_film_Out = inside_film_drop(C_Table_output, total_flux_Kw_m2_Out, Out_OD, Out_ID);
            List<Double> inside_film_IN = inside_film_drop(C_Table_output, total_flux_Kw_m2_IN, In_OD, In_ID);


            //Tube_wall_drop
            // =J8*LN(J8/L8)*10^-3/(2*P8)*F125*1000
            List<Double> Tube_wall_drop_Out = Tube_wall_drop(total_flux_Kw_m2_Out, Out_OD, Out_ID, Out_Thermal);
            List<Double> Tube_wall_drop_IN = Tube_wall_drop(total_flux_Kw_m2_IN, In_OD, In_ID, In_Thermal);



            //Total_Drop


            List<Double> Total_Drop_Out = Total_Drop(inside_film_Out, Tube_wall_drop_Out);
            List<Double> Total_Drop_IN = Total_Drop(inside_film_IN, Tube_wall_drop_IN);

            //Metal_Temp

            List<Double> Metal_temp_Out = Metal_temp(Total_Drop_Out, steam_temp_Out);
            List<Double> Metal_temp_IN = Metal_temp(Total_Drop_IN, steam_temp_IN);


            Over_All_Loop(Outlet_Pressure_IN, steam_temp_IN, Metal_temp_IN, Sec_2, No_Of_Loop_Element,
                                 Outlet_Pressure_Out, steam_temp_Out, Metal_temp_Out, Sec_1, C_Table_output);


            //C_Table_Output.Add(mCal_5C_SC.Flow_through_outer_loop);  C_46
            //C_Table_Output.Add(mCal_5C_SC.Flow_through_each_inner__loop);C_47
            //C_Table_Output.Add(mCal_5C_SC.Pressure_drop_per_unit_length_of_outer_tube);C_53
            //C_Table_Output.Add(mCal_5C_SC.Pressure_drop_per_unit_length_of_inner_tube);C_54
            //C_Table_Output.Add(mCal_5C_SC.Inlet_pressure_pf_steam); D23
            //C_Table_Output.Add(mCal_5C_SC.Enthalpy_of_steam_inlet_to_the_PSH);D22

        }
        //Final Result Set
        public void Over_All_Loop(List<Double> Outlet_Pressure_IN, List<Double> steam_temp_IN, List<Double> Metal_temp_IN, int Total_Sec, int No_Of_Loop_Element
            , List<Double> Outlet_Pressure_Out, List<Double> steam_temp_Out, List<Double> Metal_temp_Out, int Sec_1, List<Double> C_Table_output)
        {
            Dictionary<string, List<Double>> Tube2_Sec_Outlet_Pressure = new Dictionary<string, List<Double>>();
            Dictionary<string, List<Double>> Tube2_Sec_steam_temp_IN = new Dictionary<string, List<Double>>();
            Dictionary<string, List<Double>> Tube2_Sec_Metal_temp_IN = new Dictionary<string, List<Double>>();
            Dictionary<string, Double> Tube2_Sec_Max_Metal_Temp = new Dictionary<string, Double>();
            Dictionary<string, string> Tube2_Max_Metal_Temp_Section = new Dictionary<string, string>();
            Dictionary<string, string> Tube2_Max_Metal_Temp_MAterial = new Dictionary<string, string>();
            Dictionary<string, Double> Tube2_Sec_LAst_Metal_Temp = new Dictionary<string, Double>();
            Dictionary<string, string> Tube2_Sec_LAst_Metal_Temp_Section = new Dictionary<string, string>();
            Dictionary<string, string> Tube2_Sec_LAst_Metal_Temp_MAterial = new Dictionary<string, string>();
            Dictionary<string, Double> Tube2_Sec_Steam_Enthalpy = new Dictionary<string, Double>();
            Dictionary<string, Double> Tube2_Sec_LAst_Steam_Temp = new Dictionary<string, Double>();
            Stream_Macros stream_Macros_BLL = new Stream_Macros();
            for (int i = 1; i <= No_Of_Loop_Element; i++)
            {

                Tube2_Sec_Outlet_Pressure.Add("Loop" + i.ToString(), new List<Double>());
                Tube2_Sec_steam_temp_IN.Add("Loop" + i.ToString(), new List<Double>());
                Tube2_Sec_Metal_temp_IN.Add("Loop" + i.ToString(), new List<Double>());

            }


            Tube2_Sec_Outlet_Pressure["Loop" + 1] = Outlet_Pressure_Out;
            Tube2_Sec_steam_temp_IN["Loop" + 1] = steam_temp_Out;
            Tube2_Sec_Metal_temp_IN["Loop" + 1] = Metal_temp_Out;


            int k = 0;

            while (k <= Outlet_Pressure_IN.Count - 1)
            {
                for (int p = 2; p <= No_Of_Loop_Element; p++)
                {
                    int q = 0;
                    while (q != Total_Sec)
                    {

                        Tube2_Sec_Outlet_Pressure["Loop" + p].Add(Outlet_Pressure_IN.ElementAt(k));
                        Tube2_Sec_steam_temp_IN["Loop" + p].Add(steam_temp_IN.ElementAt(k));
                        Tube2_Sec_Metal_temp_IN["Loop" + p].Add(Metal_temp_IN.ElementAt(k));
                        k++;
                        q++;
                    }
                }
            }
            //MAximum of metal Temperature
            for (int i = 1; i <= No_Of_Loop_Element; i++)
            {
                List<double> Metal = Tube2_Sec_Metal_temp_IN["Loop" + i];
                Tube2_Sec_Max_Metal_Temp.Add("Loop" + i.ToString(), Metal.Max());
                int maxIndex = Metal.ToList().IndexOf(Metal.Max()) + 1;
                Tube2_Max_Metal_Temp_Section.Add("Loop" + i.ToString(), "Section" + maxIndex.ToString());

                DataSet Thermal_Conductivity = new DataSet();
                if (i == 1)
                {
                    Thermal_Conductivity = mCal_5C_BLL.Get_MAterial_Section_For_5C_Calculation(BoilerID, ProjectID, SectionID, ObjectiveID, Tube_Type_1, Outer_Tube_Position, "Sec " + maxIndex.ToString());

                }
                else
                {
                    Thermal_Conductivity = mCal_5C_BLL.Get_MAterial_Section_For_5C_Calculation(BoilerID, ProjectID, SectionID, ObjectiveID, Tube_Type_2, Inner_Tube_Position, "Sec " + maxIndex.ToString());
                }
                Tube2_Max_Metal_Temp_MAterial.Add("Loop" + i.ToString(), Thermal_Conductivity.Tables[0].Rows[0]["Material"].ToString());

            }
            //Last of Metal Temperature
            for (int i = 1; i <= No_Of_Loop_Element; i++)
            {
                List<double> Metal = Tube2_Sec_Metal_temp_IN["Loop" + i];
                Tube2_Sec_LAst_Metal_Temp.Add("Loop" + i.ToString(), Metal.Last());
                int maxIndex = Metal.ToList().IndexOf(Metal.Last()) + 1;
                Tube2_Sec_LAst_Metal_Temp_Section.Add("Loop" + i.ToString(), "Section" + maxIndex.ToString());
                //MAterial USed for that Section

                DataSet Thermal_Conductivity = new DataSet();
                if (i == 1)
                {
                    Thermal_Conductivity = mCal_5C_BLL.Get_MAterial_Section_For_5C_Calculation(BoilerID, ProjectID, SectionID, ObjectiveID, Tube_Type_1, Outer_Tube_Position, "Sec " + maxIndex.ToString());
                }
                else
                {
                    Thermal_Conductivity = mCal_5C_BLL.Get_MAterial_Section_For_5C_Calculation(BoilerID, ProjectID, SectionID, ObjectiveID, Tube_Type_2, Inner_Tube_Position, "Sec " + maxIndex.ToString());
                }
                Tube2_Sec_LAst_Metal_Temp_MAterial.Add("Loop" + i.ToString(), Thermal_Conductivity.Tables[0].Rows[0]["Material"].ToString());

            }
            //LAst of Steam Temperature
            for (int i = 1; i <= No_Of_Loop_Element; i++)
            {
                List<double> Metal = Tube2_Sec_steam_temp_IN["Loop" + i];
                Tube2_Sec_LAst_Steam_Temp.Add("Loop" + i.ToString(), Metal.Last());

            }

            //Steam Enthalpy of each loop
            for (int i = 1; i <= No_Of_Loop_Element; i++)
            {
                //=h_pt(J216*10,K216)
                List<Double> steam = Tube2_Sec_steam_temp_IN["Loop" + i];
                List<Double> Pressure = Tube2_Sec_Outlet_Pressure["Loop" + i];
                Double Steam_enthalpy = stream_Macros_BLL.h_pT(Pressure.Last() * 10, steam.Last());
                Tube2_Sec_Steam_Enthalpy.Add("Loop" + i.ToString(), Steam_enthalpy);
            }

            //PSH Enthalpy after mixing	=(C46*K132+C47*(K150+K167+K184+K201+K218+K235+K252))/D18
            List<Double> Enthalpy = new List<double>();
            foreach (KeyValuePair<string, double> kvp in Tube2_Sec_Steam_Enthalpy)
            {
                Enthalpy.Add(kvp.Value);
            }
            //=(C46*K132+C47*(K150+K167+K184+K201+K218+K235+K252))/D18

            //Validation of Result
            Double PSH_Enthalpy_after_mixing = (C_Table_output.ElementAt(0) * Enthalpy.ElementAt(0) + C_Table_output.ElementAt(1) *
              (Enthalpy.Skip(1).Take(Enthalpy.Count - 1).Sum())) / C_Table_output.ElementAt(7);

            Double PSH_outlet_pressure = C_Table_output.ElementAt(8);
            Double PSH_outlet_temperature_mixing_calculated = stream_Macros_BLL.T_ph(PSH_outlet_pressure * 10, PSH_Enthalpy_after_mixing);
            Double PSH_outlet_temperature_mixing_Design = C_Table_output.ElementAt(9);

            for (int i = 1; i <= No_Of_Loop_Element; i++)
            {
                List<double> Metal = new List<double>();
                foreach (KeyValuePair<string, double> kvp in Tube2_Sec_Max_Metal_Temp)
                {
                    Metal.Add(kvp.Value);
                }
                int pos = Metal.ToList().IndexOf(Metal.Max());

            }

            for (int Fi = 1; Fi <= No_Of_Loop_Element; Fi++)
            {
                string LoopID = "Loop " + Fi;
                string Steam_Last = Convert.ToString(Tube2_Sec_LAst_Steam_Temp["Loop" + Fi]);
                string Max_Metal_Temp = Convert.ToString(Tube2_Sec_Max_Metal_Temp["Loop" + Fi]);
                string Max_Metal_Temp_Sec = Tube2_Max_Metal_Temp_Section["Loop" + Fi];
                string Max_Metal_Temp_Material = Tube2_Max_Metal_Temp_MAterial["Loop" + Fi];
                string Last_Metal_Temp = Convert.ToString(Tube2_Sec_LAst_Metal_Temp["Loop" + Fi]);
                string Last_Metal_Temp_MAterial = Tube2_Sec_LAst_Metal_Temp_MAterial["Loop" + Fi];
                mCal_5C_BLL.Insert_Metal_Temperature_For_Heating_Element(BoilerID, ProjectID, SectionID, BoilerLoad, ObjectiveID.ToString(), LoopID
                    , Steam_Last, Max_Metal_Temp, Max_Metal_Temp_Sec, Max_Metal_Temp_Material, Last_Metal_Temp, Last_Metal_Temp_MAterial);
            }



        }

    }
}


