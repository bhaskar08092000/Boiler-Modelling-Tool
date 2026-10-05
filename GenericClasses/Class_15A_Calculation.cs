using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using BoilerModellingTool.SC;
using BoilerModellingTool.BLL;
using BoilerModellingTool.DAL;

namespace GenericClasses
{
    public class Class_15A_Calculation
    {

        public void Class_15A_Calculation_Result(string ProjectID, string BoilerID, string SectionID)
        {
            //string Project_ID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
            //string Boiler_ID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
            //string Boiler_Load = "TMCR";
            //string SectionID = "105D1268-A5C7-43B0-9461-76689569D0FE";
            //int ObjectiveID = 1;


            Cal_15A_SC mCal_15A_SC = null;
            Cal_15A_BLL mCal_15A_BLL = null;
            Stream_Macros mStream_Macros_BLL = null;
            DataSet mdataset = null;
            DataTable mDataTable = null;

            mCal_15A_SC = new Cal_15A_SC();
            mCal_15A_BLL = new Cal_15A_BLL();
            mStream_Macros_BLL = new Stream_Macros();
            mdataset = new DataSet();
            mDataTable = new DataTable();



            mCal_15A_SC = mCal_15A_BLL.Get_Input_For_15A_Calculation(BoilerID, ProjectID, SectionID);



            //CALCULATION

            //F24
            mCal_15A_SC.Relative_transverse_spacing = mCal_15A_SC.Transverse_spacing / mCal_15A_SC.Tube_diameter;

            //F25
            mCal_15A_SC.Average_longitudinal_pitch = mCal_15A_SC.Economizer_height * 1000 / (mCal_15A_SC.Longitudinal_tube_rows - 1);

            //F26
            mCal_15A_SC.Relative_longitudinal_spacing = mCal_15A_SC.Average_longitudinal_pitch / mCal_15A_SC.Tube_diameter;

            //F27
            if (mCal_15A_SC.Heating_Section_Area != 0)
            {
                mCal_15A_SC.Heating_area_of_economizer = mCal_15A_SC.Heating_Section_Area;
            }
            else
            {
                mCal_15A_SC.Heating_area_of_economizer = Math.PI * mCal_15A_SC.Tube_diameter / 1000 * (mCal_15A_SC.Depth_of_flue_duct_at_LTSH_inlet - 2 * mCal_15A_SC.Distance_from_Eco_to_front_and_rear_walls / 1000) * mCal_15A_SC.Longitudinal_tube_rows * mCal_15A_SC.Transverse_rows;
            }

            //F28
            mCal_15A_SC.Length_of_roof_tube_within_FSH_zone = 0;

            //F29
            mCal_15A_SC.Heating_area_of_roof_tubes = mCal_15A_SC.Backpass_width * mCal_15A_SC.Length_of_roof_tube_within_FSH_zone;

            //F30
            mCal_15A_SC.Heating_area_of_side_steam_wall = 0;

            //F31
            mCal_15A_SC.Heating_area_of_bottom_steam_wall = 0;

            //F32
            mCal_15A_SC.Heating_area_of_steam_wall = mCal_15A_SC.Heating_area_of_side_steam_wall + mCal_15A_SC.Heating_area_of_bottom_steam_wall;

            //F33
            mCal_15A_SC.Inlet_flue_gas_flow_area = mCal_15A_SC.Depth_of_flue_duct_at_LTSH_inlet * (mCal_15A_SC.Backpass_width - mCal_15A_SC.Transverse_rows * mCal_15A_SC.Tube_diameter / 1000);

            //F34
            mCal_15A_SC.Gas_outlet_flow_area = mCal_15A_SC.Depth_of_flue_duct_at_LTSH_outlet * (mCal_15A_SC.Backpass_width - mCal_15A_SC.Transverse_rows * mCal_15A_SC.Tube_diameter / 1000);

            //F35
            mCal_15A_SC.Gas_average_flow_area = 2 * mCal_15A_SC.Inlet_flue_gas_flow_area * mCal_15A_SC.Gas_outlet_flow_area / (mCal_15A_SC.Inlet_flue_gas_flow_area + mCal_15A_SC.Gas_outlet_flow_area);

            //F36
            //mCal_15A_SC.Steam_flow_area_ = mCal_15A_SC.Number_of_head * mCal_15A_SC.Transverse_rows * Math.PI * Math.Pow((mCal_15A_SC.Tube_diameter / 1000 - 2 * mCal_15A_SC.Tube_thickness / 1000), 2 / 4);

            //F37
            mCal_15A_SC.Effective_radiation_layer_thickness = 0.9 * mCal_15A_SC.Tube_diameter / 1000 * (4 * mCal_15A_SC.Relative_transverse_spacing * mCal_15A_SC.Relative_longitudinal_spacing / Math.PI - 1);

            mCal_15A_SC.Steam_flow_area_ =
                mCal_15A_SC.Number_of_head *
                mCal_15A_SC.Transverse_rows *
                Math.PI *
                Math.Pow(
                    (mCal_15A_SC.Tube_diameter / 1000 -
                     2 * mCal_15A_SC.Tube_thickness / 1000),
                    2
                ) / 4;

            //F38
            //mCal_15A_SC.Correction_factor_for_tube_rows= IF((0.91+0.0125*(mCal_15A_SC.Longitudinal_tube_rows-2))>1,1,(0.91+0.0125*(mCal_15A_SC.Longitudinal_tube_rows-2)))

            if ((0.91 + 0.0125 * (mCal_15A_SC.Longitudinal_tube_rows - 2) > 1))
            {
                mCal_15A_SC.Correction_factor_for_tube_rows = 1;
            }
            else
            {
                mCal_15A_SC.Correction_factor_for_tube_rows = (0.91 + 0.0125 * (mCal_15A_SC.Longitudinal_tube_rows - 2));
            }

            //insert

            for (int i = 1; i < 16; i++)
            {
                int PID = i;

                switch (PID)
                {
                    case 1:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Relative_transverse_spacing.ToString());
                        break;
                    case 2:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Average_longitudinal_pitch.ToString());
                        break;
                    case 3:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Relative_longitudinal_spacing.ToString());
                        break;
                    case 4:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Heating_area_of_economizer.ToString());
                        break;
                    case 5:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Length_of_roof_tube_within_FSH_zone.ToString());
                        break;
                    case 6:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Heating_area_of_roof_tubes.ToString());
                        break;
                    case 7:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Heating_area_of_side_steam_wall.ToString());
                        break;
                    case 8:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Heating_area_of_bottom_steam_wall.ToString());
                        break;
                    case 9:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Heating_area_of_steam_wall.ToString());
                        break;
                    case 10:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Inlet_flue_gas_flow_area.ToString());
                        break;
                    case 11:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Gas_outlet_flow_area.ToString());
                        break;
                    case 12:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Gas_average_flow_area.ToString());
                        break;
                    case 13:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Steam_flow_area_.ToString());
                        break;
                    case 14:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Effective_radiation_layer_thickness.ToString());
                        break;
                    case 15:
                        mCal_15A_BLL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, mCal_15A_SC.Correction_factor_for_tube_rows.ToString());
                        break;


                }
            }
        }
    }
}
