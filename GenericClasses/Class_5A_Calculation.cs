using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using BoilerModellingTool.SC;
using BoilerModellingTool.BLL;
//Changes done by Preeti --12--01-2021

namespace GenericClasses
{

    public class Class_5A_Calculation
    {
        string Boiler_ID = null;
        string Project_ID = null;
        string Section_ID = null;
        string Boiler_Load = null;
        int Objective_ID = 0;
        string SectionID;



        public Class_5A_Calculation(string BoilerID, string Project_ID,
             string BoilerLoad, int ObjectiveID)
        {

            this.Boiler_ID = BoilerID;
            this.Project_ID = Project_ID;
            this.Boiler_Load = BoilerLoad;
            this.Objective_ID = ObjectiveID;
            //this.Section_ID = SectionID;

        }



        public void Calculation_For_5A()
        {
            //string Project_ID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
            //string Boiler_ID = "F85C19BA-20C9-4B61-975D-636C8404F30A";

            //string Objective_ID = "1";
            //string Boiler_Load = "100%TMCR";

            Cal_5A_BLL mCal_5A_BLL = new Cal_5A_BLL();
            Cal_6A_BLL mCal_6A_BLL = new Cal_6A_BLL();
            Cal_5A_SC cal_5A_SC = new Cal_5A_SC();
            DataSet dt = new DataSet();

            Section_ID = mCal_6A_BLL.GetLast_Element_of_HeatingSectionUpperFurnace(Boiler_ID, Project_ID);

            dt = mCal_5A_BLL.GetInput_For_5A_Calculation(Boiler_ID, Project_ID, Section_ID);

            //Inputs ....
            //D7
            cal_5A_SC.Tube_diameter_of_plate_superheater = Convert.ToDouble(dt.Tables[0].Rows[0][0].ToString());

            //D8
            cal_5A_SC.Platen_superheater_tube_thickness = Convert.ToDouble(dt.Tables[1].Rows[0][0].ToString());

            //D9
            cal_5A_SC.Platen_superheater_transverse_rows = Convert.ToDouble(dt.Tables[2].Rows[0][0].ToString());

            //D10
            cal_5A_SC.Platen_superheater_longitudinal_rows = Convert.ToDouble(dt.Tables[3].Rows[0][0].ToString());

            //D11
            cal_5A_SC.Platen_superheater_height = Convert.ToDouble(dt.Tables[4].Rows[0][0].ToString());

            //D12
            cal_5A_SC.Platen_superheater_depth = Convert.ToDouble(dt.Tables[5].Rows[0][0].ToString());
            //cal_5A_SC.Platen_superheater_depth = 1.653;
            //D13
            cal_5A_SC.Platen_superheater_average_transverse_spacing = Convert.ToDouble(dt.Tables[6].Rows[0][0].ToString());

            //D14
            cal_5A_SC.Number_of_head = Convert.ToDouble(dt.Tables[7].Rows[0][0].ToString());

            //D15
            cal_5A_SC.Furnace_width = Convert.ToDouble(dt.Tables[8].Rows[0][0].ToString());
            //cal_5A_SC.Furnace_width = 13.87;

            //D16
            cal_5A_SC.Furnace_depth = Convert.ToDouble(dt.Tables[9].Rows[0][0].ToString());
            // cal_5A_SC.Furnace_depth = 10.59;

            //D17
            cal_5A_SC.Configuration_factor_for_PSH = Convert.ToDouble(dt.Tables[10].Rows[0][0].ToString());

            //D18
            cal_5A_SC.Distance_from_platen_superheater_back_side_to_furnace_nose = Convert.ToDouble(dt.Tables[11].Rows[0][0].ToString());

            //D19
            cal_5A_SC.Distance_from_reheater_to_platen_superheater = Convert.ToDouble(dt.Tables[12].Rows[0][0].ToString());

            //D20
            cal_5A_SC.Calculated_height_of_flue_duct_of_reheater = Convert.ToDouble(dt.Tables[13].Rows[0][0].ToString());
            // cal_5A_SC.Calculated_height_of_flue_duct_of_reheater = 10.38;

            //Calculations.....................
            //F26
            cal_5A_SC.Platen_superheater_average_transverse_spacing_calc = cal_5A_SC.Furnace_width * 1000 / (cal_5A_SC.Platen_superheater_transverse_rows + 1);       //=D15*1000/(D9+1)

            //F27
            cal_5A_SC.Platen_superheater_average_longitudinal_spacing = (cal_5A_SC.Platen_superheater_depth - cal_5A_SC.Tube_diameter_of_plate_superheater / 1000) * 1000 / (cal_5A_SC.Platen_superheater_longitudinal_rows - 1);        //=(D12-D7/1000)*1000/(D10-1)

            //F28
            cal_5A_SC.Relative_transverse_pitch = cal_5A_SC.Platen_superheater_average_transverse_spacing / cal_5A_SC.Tube_diameter_of_plate_superheater;      //=D13/D7

            //F29
            cal_5A_SC.Relative_vertical_pitch = cal_5A_SC.Platen_superheater_average_longitudinal_spacing / cal_5A_SC.Tube_diameter_of_plate_superheater;    //=F27/D7

            //F30
            cal_5A_SC.Inlet_radiation_area = (cal_5A_SC.Platen_superheater_height + cal_5A_SC.Platen_superheater_depth + cal_5A_SC.Tube_diameter_of_plate_superheater / 1000) * cal_5A_SC.Furnace_width * cal_5A_SC.Platen_superheater_transverse_rows / (cal_5A_SC.Platen_superheater_transverse_rows + 1);       //=(D11+D12+D7/1000)*D15*D9/(D9+1)

            //F31
            cal_5A_SC.Outlet_radiation_area = cal_5A_SC.Platen_superheater_height * cal_5A_SC.Furnace_width * cal_5A_SC.Platen_superheater_transverse_rows / (cal_5A_SC.Platen_superheater_transverse_rows + 1);      //=D11*D15*D9/(D9+1)

            //F32
            cal_5A_SC.Platen_superheator_total_heating_area = 2 * cal_5A_SC.Platen_superheater_height * (cal_5A_SC.Platen_superheater_depth + cal_5A_SC.Tube_diameter_of_plate_superheater / 1000) * cal_5A_SC.Platen_superheater_transverse_rows * cal_5A_SC.Configuration_factor_for_PSH;  //=2*D11*(D12+D7/1000)*D9*D17

            //F33      
            cal_5A_SC.Configuration_factor_from_inlet_to_outlet = Math.Pow((Math.Pow((1000 * cal_5A_SC.Platen_superheater_depth / cal_5A_SC.Platen_superheater_average_transverse_spacing), 2) + 1), 0.5) - (1000 * cal_5A_SC.Platen_superheater_depth / cal_5A_SC.Platen_superheater_average_transverse_spacing);      //=((1000*D12/D13)^2+1)^0.5-(1000*D12/D13)

            //F34
            cal_5A_SC.Side_Water_wall_heating_area_1_within_panel = 2 * (cal_5A_SC.Platen_superheater_height * (cal_5A_SC.Platen_superheater_depth + cal_5A_SC.Distance_from_platen_superheater_back_side_to_furnace_nose));    //=2*(D11*(D12+D18))

            //F35
            cal_5A_SC.Side_Water_wall_heating_area_2_within_panel = 2 * (0.5 * (cal_5A_SC.Distance_from_reheater_to_platen_superheater - cal_5A_SC.Distance_from_platen_superheater_back_side_to_furnace_nose) * (cal_5A_SC.Platen_superheater_height + cal_5A_SC.Calculated_height_of_flue_duct_of_reheater));    //=2*(0.5*(D19-D18)*(D11+D20))

            //F37
            cal_5A_SC.Roof_tube_length_within_platen_superheator_zone = (cal_5A_SC.Platen_superheater_depth + cal_5A_SC.Distance_from_reheater_to_platen_superheater);    //=(D12+D19)



            int Furnace_roof_cooling_medium = Convert.ToInt16(dt.Tables[14].Rows[0][0].ToString());
            if (Furnace_roof_cooling_medium == 1)//if Water  take F36= F34+ F35+(D15*F37 ) 
            {
                //F36
                cal_5A_SC.Side_Water_wall_heating_area_within_panel = cal_5A_SC.Side_Water_wall_heating_area_1_within_panel + cal_5A_SC.Side_Water_wall_heating_area_2_within_panel + (cal_5A_SC.Furnace_width * cal_5A_SC.Roof_tube_length_within_platen_superheator_zone);     //=F34+F35
                //F38
                cal_5A_SC.Heating_area_of_roof_tubes_in_PSH_zone = 0;

            }
            else//if steam  take F36= F34+F35
            {
                //F36
                cal_5A_SC.Side_Water_wall_heating_area_within_panel = cal_5A_SC.Side_Water_wall_heating_area_1_within_panel + cal_5A_SC.Side_Water_wall_heating_area_2_within_panel;     //=F34+F35
                //F38
                cal_5A_SC.Heating_area_of_roof_tubes_in_PSH_zone = cal_5A_SC.Furnace_width * cal_5A_SC.Roof_tube_length_within_platen_superheator_zone;         //=D15*F37

            }




            //F39
            cal_5A_SC.Effective_radiation_layer_thickness = 1.8 / (1 / cal_5A_SC.Platen_superheater_depth + 1 / cal_5A_SC.Platen_superheater_height + 1 / (cal_5A_SC.Platen_superheater_average_transverse_spacing / 1000));           //=1.8/(1/D12+1/D11+1/(D13/1000))

            //F40
            cal_5A_SC.Gas_inlet_flow_area = (cal_5A_SC.Platen_superheater_height + cal_5A_SC.Platen_superheater_depth) * (cal_5A_SC.Furnace_width - cal_5A_SC.Tube_diameter_of_plate_superheater / 1000 * cal_5A_SC.Platen_superheater_transverse_rows);               //=(D11+D12)*(D15-D7/1000*D9)

            //F41
            cal_5A_SC.Gas_outlet_flow_area = cal_5A_SC.Platen_superheater_height * (cal_5A_SC.Furnace_width - cal_5A_SC.Tube_diameter_of_plate_superheater / 1000 * cal_5A_SC.Platen_superheater_transverse_rows);               //=D11*(D15-D7/1000*D9)

            //F42
            cal_5A_SC.Gas_average_flow_area = 2 * cal_5A_SC.Gas_inlet_flow_area * cal_5A_SC.Gas_outlet_flow_area / (cal_5A_SC.Gas_inlet_flow_area + cal_5A_SC.Gas_outlet_flow_area);             //=2*F40*F41/(F40+F41)

            //F43
            cal_5A_SC.Steam_flow_area = 3.141592654 / 4 * cal_5A_SC.Number_of_head * cal_5A_SC.Platen_superheater_transverse_rows * Math.Pow((cal_5A_SC.Tube_diameter_of_plate_superheater / 1000 - 2 * cal_5A_SC.Platen_superheater_tube_thickness / 1000), 2);                 //=PI()/4*D14*D9*(D7/1000-2*D8/1000)^2    PI = 3.141592654

            //F44               cal_5A_SC.Correction_factor_for_tube_rows =;    //= IF((0.91+0.0125*(D10-2))>1,1,(0.91+0.0125*(D10-2)))

            if ((0.91 + 0.0125 * (cal_5A_SC.Platen_superheater_longitudinal_rows - 2)) > 1)
            {
                cal_5A_SC.Correction_factor_for_tube_rows = 1;
            }
            else
            {
                cal_5A_SC.Correction_factor_for_tube_rows = (0.91 + 0.0125 * (cal_5A_SC.Platen_superheater_longitudinal_rows - 2));
            }

            //F45
            cal_5A_SC.PSH_convective_area = 3.141592654 * cal_5A_SC.Tube_diameter_of_plate_superheater * cal_5A_SC.Platen_superheator_total_heating_area / (2 * cal_5A_SC.Platen_superheater_average_longitudinal_spacing);               //=PI()*D7*F32/(2*F27)

            //F46
            cal_5A_SC.Bottom_window_area = (cal_5A_SC.Platen_superheater_depth + cal_5A_SC.Tube_diameter_of_plate_superheater / 1000) * cal_5A_SC.Furnace_width * cal_5A_SC.Platen_superheater_transverse_rows / (cal_5A_SC.Platen_superheater_transverse_rows + 1);                //=(D12+D7/1000)*'4A'!D7*D9/(D9+1) 


            DataTable dt1 = new DataTable();
            dt1 = mCal_5A_BLL.Get_PID_For_5A_Calculation();

            foreach (DataRow row in dt1.Rows)
            {
                int pid = Convert.ToInt16(row["PID"].ToString());

                switch (pid)
                {
                    case 1:

                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Platen_superheater_average_transverse_spacing_calc.ToString());

                        break;

                    case 2:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Platen_superheater_average_longitudinal_spacing.ToString());
                        break;
                    case 3:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Relative_transverse_pitch.ToString());
                        break;
                    case 4:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Relative_vertical_pitch.ToString());
                        break;
                    case 5:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Inlet_radiation_area.ToString());
                        break;
                    case 6:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Outlet_radiation_area.ToString());
                        break;
                    case 7:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Platen_superheator_total_heating_area.ToString());
                        break;
                    case 8:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Configuration_factor_from_inlet_to_outlet.ToString());
                        break;
                    case 9:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Side_Water_wall_heating_area_1_within_panel.ToString());
                        break;
                    case 10:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Side_Water_wall_heating_area_2_within_panel.ToString());
                        break;
                    case 11:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Side_Water_wall_heating_area_within_panel.ToString());
                        break;
                    case 12:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Roof_tube_length_within_platen_superheator_zone.ToString());
                        break;
                    case 13:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Heating_area_of_roof_tubes_in_PSH_zone.ToString());
                        break;
                    case 14:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Effective_radiation_layer_thickness.ToString());
                        break;
                    case 15:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Gas_inlet_flow_area.ToString());
                        break;
                    case 16:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Gas_outlet_flow_area.ToString());

                        break;


                    case 17:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Gas_average_flow_area.ToString());
                        break;
                    case 18:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Steam_flow_area.ToString());
                        break;

                    case 19:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Correction_factor_for_tube_rows.ToString());
                        break;

                    case 20:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.PSH_convective_area.ToString());
                        break;

                    case 21:
                        mCal_5A_BLL.Insert_5A_Calculation(pid, Boiler_ID, Project_ID, Boiler_Load, Section_ID, Objective_ID, cal_5A_SC.Bottom_window_area.ToString());
                        break;
                }


            }
        }
    }
}