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



//Changes Done by Preeti on 9-10-2020
//Changes done by Shubhangi 18-11-2020

public class Class_6A_Calculation
{
    #region " Properties "

    public Cal_6A_SC Doc { get; set; }




    #endregion

    public DataTable dtgrid = new DataTable();

    //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;


    //string Project_ID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
    //string Boiler_ID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
    //string Section_ID = "54E4FC11-81D9-4C52-AC48-DA4FDB7628E2";
    //int Objective_ID = 1;
        string Prev_Section_ID = null;
        string Last_5B_Sec_ID = null;
        //string Boiler_Load = "100%TMCR";
        Cal_6A_BLL mCal_6A_BLL = new Cal_6A_BLL();

        string Boiler_ID = null;
        string Project_ID = null;
        string Section_ID = null;
        string Boiler_Load = null;
        int Objective_ID = 0;
        string SectionType = null;


        public Class_6A_Calculation(string BoilerID, string Project_ID,
             string BoilerLoad ,int ObjectiveID,string Section_ID,string SectionType)
        {

            this.Boiler_ID = BoilerID;
            this.Project_ID = Project_ID;          
            this.Boiler_Load = BoilerLoad;
            this.Objective_ID = ObjectiveID;
            this.Section_ID = Section_ID;
            this.SectionType = SectionType;

        }
   
        public void calculation_For_6A()
    {


        DataSet dt = new DataSet();
        //Last_5B_Sec_ID = mCal_6A_BLL.GetLast_Element_of_HeatingSectionUpperFurnace(Boiler_ID, Project_ID);
        dt = mCal_6A_BLL.GetInput_For_6A_Calculation(Boiler_ID, Project_ID, Boiler_Load,Section_ID,SectionType);



        //D7 
        Double Tube_diameter = Convert.ToDouble(dt.Tables[0].Rows[0]["Value"].ToString());

        //D8
        Double Tube_thickness = Convert.ToDouble(dt.Tables[0].Rows[1]["Value"].ToString());

        //D9 
        Double Longitudinal_tube_rows = Convert.ToDouble(dt.Tables[0].Rows[4]["Value"].ToString());

        //D10 
        Double Number_of_head = Convert.ToDouble(dt.Tables[0].Rows[6]["Value"].ToString());

        //D11
        Double Transverse_rows = Convert.ToDouble(dt.Tables[0].Rows[2]["Value"].ToString());

        //D12
        Double Transverse_spacing = Convert.ToDouble(dt.Tables[0].Rows[3]["Value"].ToString());

        //D13 ------
        Double Longitudinal_spacing = Convert.ToDouble(dt.Tables[0].Rows[5]["Value"].ToString());

        //D14
        Double Reheater_depth = Convert.ToDouble(dt.Tables[0].Rows[7]["Value"].ToString());


        //D15 
        Double Furnace_width = Convert.ToDouble(dt.Tables[2].Rows[0][0].ToString());

        //D16
        Double Relative_space_depth_prior_to_Reheater = Convert.ToDouble(dt.Tables[0].Rows[8]["Value"].ToString());

        //D17
        Double Height_of_flue_duct_at_Reheater_inlet = Convert.ToDouble(dt.Tables[0].Rows[9]["Value"].ToString());

        //D18
        Double Height_of_flue_duct_at_Reheater_outlet = Convert.ToDouble(dt.Tables[0].Rows[10]["Value"].ToString());

        //D19
        Double Distance_from_Reheater_to_WW_hanger_tube = Convert.ToDouble(dt.Tables[0].Rows[11]["Value"].ToString());

        //D20 
        Double Height_of_flue_duct_at_WW_hanger_inlet = Convert.ToDouble(dt.Tables[0].Rows[12]["Value"].ToString());

        //D21 
        Double Furnace_nose_up_dip_angle = Convert.ToDouble(dt.Tables[1].Rows[0][0].ToString());

        //D22 
        Double Heating_Sec_Area = Convert.ToDouble(dt.Tables[0].Rows[13]["Value"].ToString());

        int Roof_Cooling_MEdium = Convert.ToInt16(dt.Tables[3].Rows[0][0].ToString());

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        ////Calculation

        //F26
        Double Relative_transverse_spacing = Transverse_spacing / Tube_diameter;    //=D12/D7

        //F27
        Double Average_longitudinal_pitch = Reheater_depth * 1000 / (Longitudinal_tube_rows - 1);     //=D14*1000/(D9-1)

        //F28
        Double Relative_longitudinal_spacing = Average_longitudinal_pitch / Tube_diameter;       //=F27/D7

        //F29 
        Double Average_tube_calculated_length = 7.766;    //7.766

        //F30 
        Double Heating_area_of_Reheater = 0;
        if (Heating_Sec_Area == 0)
        {
            //=D9*D11*F29*PI()*D7/1000
            Heating_area_of_Reheater = Longitudinal_tube_rows * Transverse_rows *Average_tube_calculated_length* 3.141592654 * Tube_diameter / 1000 ;     //=14*D11*PI()*D7/1000*F29
        }
        else
        {
            Heating_area_of_Reheater = Heating_Sec_Area;
        }
        //F31 
        Double Length_of_roof_tube_within_Reheater_zone = Distance_from_Reheater_to_WW_hanger_tube + Reheater_depth;     //=D19+D14

        //F32 
        Double Heating_area_of_roof_tubes = 0;
        if (Roof_Cooling_MEdium == 1)//if water 
        {
            Heating_area_of_roof_tubes = 0;
        }
        else//Steam 
        {
            Heating_area_of_roof_tubes = Furnace_width * Length_of_roof_tube_within_Reheater_zone;      //=D15*F31
        }


        //F33 
        Double Heating_area_of_side_water_wall = 2 * 0.5 * Length_of_roof_tube_within_Reheater_zone * (Height_of_flue_duct_at_Reheater_inlet + Height_of_flue_duct_at_WW_hanger_inlet);      //=2*0.5*F31*(D17+D20)

        //F34 
        Double Heating_area_of_bottom_water_wall = Furnace_width * Length_of_roof_tube_within_Reheater_zone / Math.Cos(3.141592654 / 180 * Furnace_nose_up_dip_angle);        //=D15*F31/COS(PI()/180*D21)

        //F35
        Double Heating_area_of_water_wall = 0;
        if (Roof_Cooling_MEdium == 1)//if water 
        {
            Heating_area_of_water_wall = Heating_area_of_side_water_wall + Heating_area_of_bottom_water_wall + Furnace_width * Length_of_roof_tube_within_Reheater_zone;
        }
        else//Steam
        {
            Heating_area_of_water_wall = Heating_area_of_side_water_wall + Heating_area_of_bottom_water_wall;
        }

        //F36 
        Double Inlet_flue_gas_flow_area = Height_of_flue_duct_at_Reheater_inlet * (Furnace_width - Tube_diameter / 1000 * Transverse_rows);     //=D17*(D15-D7/1000*D11)

        //F37
        Double Gas_outlet_flow_area = Height_of_flue_duct_at_Reheater_outlet * (Furnace_width - Tube_diameter / 1000 * Transverse_rows);     //=D18*(D15-D7/1000*D11)

        //F38 
        Double Gas_average_flow_area = 2 * Inlet_flue_gas_flow_area * Gas_outlet_flow_area / (Inlet_flue_gas_flow_area + Gas_outlet_flow_area);        //=2*F36*F37/(F36+F37)

        //F39
        Double Steam_flow_area = 3.141592654 / 4 * Number_of_head * Transverse_rows * Math.Pow((Tube_diameter / 1000 - 2 * Tube_thickness / 1000), 2);              //=PI()/4*D10*D11*(D7/1000-2*D8/1000)^2

        //F40 
        Double Effective_radiation_layer_thickness = 0.9 * Tube_diameter / 1000 * (4 * Relative_transverse_spacing * Relative_longitudinal_spacing / 3.141592654 - 1);      //=0.9*D7/1000*(4*F26*F28/PI()-1)



        //F41
        Double Correction_factor_for_tube_rows = 0;

        //// = IF((0.91+0.0125*(D9-2))>1,1,(0.91+0.0125*(D9-2)))
        if ((0.91 + 0.0125 * (Longitudinal_tube_rows - 2)) > 1)
        {
            Correction_factor_for_tube_rows = 1;
        }
        else
        {
            Correction_factor_for_tube_rows = (0.91 + 0.0125 * (Longitudinal_tube_rows - 2));
        }




        //SqlConnection con1 = new SqlConnection(constr);
        //SqlCommand cmd1 = new SqlCommand("[dbo].[spr_Get_6A_Parameters]");
        //cmd1.CommandType = CommandType.StoredProcedure;
        //cmd1.Connection = con1;
        //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
        //DataTable dt1 = new DataTable();
        //sda1.Fill(dt1);

        DataSet dt1 = new DataSet();
        dt1 = mCal_6A_BLL.Get_6A_parameters();

        foreach (DataRow row in dt1.Tables[0].Rows)
        {
            int pid = Convert.ToInt16(row["PID"].ToString());
            //int cgid = Convert.ToInt16(row["CalculationGrpID"].ToString());
            //double Specific_humidity_of_ambient_air = Specific_humidity_of_ambient_airV;

            //string Boiler_ID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
            //string Project_ID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
            //string Boiler_Load = "100%T";
            //int Objective_ID = 1;






            switch (pid)
            {
                case 1:

                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Relative_transverse_spacing.ToString(), Section_ID, Objective_ID);

                    break;

                case 2:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Average_longitudinal_pitch.ToString(), Section_ID, Objective_ID);

                    break;

                case 3:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Relative_longitudinal_spacing.ToString(), Section_ID, Objective_ID);

                    break;

                case 4:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Average_tube_calculated_length.ToString(), Section_ID, Objective_ID);

                    break;

                case 5:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Heating_area_of_Reheater.ToString(), Section_ID, Objective_ID);

                    break;

                case 6:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Length_of_roof_tube_within_Reheater_zone.ToString(), Section_ID, Objective_ID);

                    break;

                case 7:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Heating_area_of_roof_tubes.ToString(), Section_ID, Objective_ID);

                    break;

                case 8:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Heating_area_of_side_water_wall.ToString(), Section_ID, Objective_ID);

                    break;

                case 9:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Heating_area_of_bottom_water_wall.ToString(), Section_ID, Objective_ID);

                    break;

                case 10:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Heating_area_of_water_wall.ToString(), Section_ID, Objective_ID);

                    break;

                case 11:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Inlet_flue_gas_flow_area.ToString(), Section_ID, Objective_ID);

                    break;

                case 12:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Gas_outlet_flow_area.ToString(), Section_ID, Objective_ID);

                    break;

                case 13:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Gas_average_flow_area.ToString(), Section_ID, Objective_ID);

                    break;

                case 14:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Steam_flow_area.ToString(), Section_ID, Objective_ID);

                    break;

                case 15:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Effective_radiation_layer_thickness.ToString(), Section_ID, Objective_ID);

                    break;

                case 16:
                    mCal_6A_BLL.Insert_6A_Value(pid, Boiler_ID, Project_ID, Boiler_Load, Correction_factor_for_tube_rows.ToString(), Section_ID, Objective_ID);

                    break;

            }
        }

    }
}