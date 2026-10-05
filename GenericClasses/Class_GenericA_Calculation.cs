using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;
using BoilerModellingTool.BLL;
using System.Data;

namespace GenericClasses
{
    public class Class_GenericA_Calculation
    {
        public Cal_GenericA_SC Doc { get; set; }

        public void Calculation_For_GenericA(string ProjectID,string BoilerID,string SectionID,string HeatingElement,string Location,int FurnaceRoofCoolingMedium)
        {
            DataTable dt = new DataTable();

            Cal_GenericA_BLL mCal_GenericA_BLL = new Cal_GenericA_BLL();

            this.Doc = mCal_GenericA_BLL.Get_InputFor_GenericA(ProjectID, BoilerID, SectionID, HeatingElement);

            //----------Calculations-------------

            //F29
            if (Doc.Heating_section_average_transverse_spacing != 0.0)
            {
                Doc.Transverse_spacing = Doc.Heating_section_average_transverse_spacing;
            }
            else
            {
                Doc.Transverse_spacing = Doc.Furnace_width / (Doc.Heating_section_transverse_rows + 1) * 1000;
            }

            //F30
            if (Doc.Heating_section_average_longitudinal_spacing != 0.0)
            {
                Doc.Longitudinal_spacing = Doc.Heating_section_average_longitudinal_spacing;
            }
            else if (Doc.Heating_section_average_longitudinal_spacing == 1)
            {
                Doc.Longitudinal_spacing = Doc.Tube_diameter_of_heating_section;
            }
            else
            {
                Doc.Longitudinal_spacing = Doc.Heating_section_depth / (Doc.Heating_section_longitudinal_rows - 1) * 1000;
            }

            //Doc.Longitudinal_spacing = Doc.Heating_section_average_longitudinal_spacing;

            //F31
            Doc.Relative_transverse_spacing = Doc.Transverse_spacing / Doc.Tube_diameter_of_heating_section;

            //F32
            Doc.Relative_longitudinal_spacing = Doc.Longitudinal_spacing / Doc.Tube_diameter_of_heating_section;

            //F33
            Doc.Average_tube_calculated_length = Doc.Height_of_flue_duct_at_heating_section_inlet;

            //F34
            if (Doc.Heating_section_area != 0.0)
            {
                Doc.Heating_area_of_heating_section = Doc.Heating_section_area;
            }
            else
            {
                Doc.Heating_area_of_heating_section = Doc.Heating_section_longitudinal_rows * Doc.Heating_section_transverse_rows * 3.14 * Doc.Tube_diameter_of_heating_section / 1000 * Doc.Average_tube_calculated_length;
            }

            //Doc.Heating_area_of_heating_section = Doc.Heating_section_longitudinal_rows * Doc.Heating_section_transverse_rows * 3.14 * Doc.Tube_diameter_of_heating_section / 1000 * Doc.Average_tube_calculated_length;

            //F35
            Doc.Length_of_roof_tube_within_heating_section_zone = Doc.Heating_section_depth + Doc.Distance_from_heating_section_to_downstream_heating_section;

            //F36
            if (FurnaceRoofCoolingMedium == 1 || Location == "Backpass" || Location == "ReverseChamber")
            {
                Doc.Heating_area_of_roof_tubes = 0.0;
            }
            else
            {
                Doc.Heating_area_of_roof_tubes = Doc.Furnace_width * Doc.Length_of_roof_tube_within_heating_section_zone;
            }

            //F37
            if (Location == "CrossDuct" || Location == "ReverseChamber")
            {
                Doc.Heating_area_of_side_water_wall = 2 * 0.5 * Doc.Length_of_roof_tube_within_heating_section_zone * (Doc.Height_of_flue_duct_at_heating_section_inlet + Doc.Height_of_flue_duct_at_inlet_of_downstream_heating_element);
            }
            else
            {
                Doc.Heating_area_of_side_water_wall = 0.0;
            }

            //F38
            if (Location == "CrossDuct" || Location == "ReverseChamber")
            {
                Doc.Heating_area_of_bottom_water_wall = Doc.Furnace_width * Doc.Length_of_roof_tube_within_heating_section_zone / Math.Cos(Math.PI / 180 * Doc.Furnace_nose_up_dip_angle);
            }
            else
            {
                Doc.Heating_area_of_bottom_water_wall = 0.0;
            }

            //F39
            if (HeatingElement != "Steam Screen Sections")
            {
                if (Location == "CrossDuct")
                {
                    if (FurnaceRoofCoolingMedium == 1)
                    {
                        Doc.Heating_area_of_water_wall = Doc.Heating_area_of_side_water_wall + Doc.Heating_area_of_bottom_water_wall + (Doc.Furnace_width * Doc.Length_of_roof_tube_within_heating_section_zone);
                    }
                    else
                    {
                        Doc.Heating_area_of_water_wall = Doc.Heating_area_of_side_water_wall + Doc.Heating_area_of_bottom_water_wall;
                    }
                }
                else
                {
                    Doc.Heating_area_of_water_wall = 0.0;
                }
            }
            else
            {
                Doc.Heating_area_of_water_wall = 0.0;
            }

            //F40
            Doc.Inlet_flue_gas_flow_area = Doc.Height_of_flue_duct_at_heating_section_inlet * (Doc.Furnace_width - Doc.Tube_diameter_of_heating_section / 1000 * Doc.Heating_section_transverse_rows);

            //F41
            Doc.Gas_outlet_flow_area = Doc.Height_of_flue_duct_at_heating_section_outlet * (Doc.Furnace_width - Doc.Tube_diameter_of_heating_section / 1000 * Doc.Heating_section_transverse_rows);

            //F42
            Doc.Gas_average_flow_area = 2 * Doc.Inlet_flue_gas_flow_area * Doc.Gas_outlet_flow_area / (Doc.Inlet_flue_gas_flow_area + Doc.Gas_outlet_flow_area);

            //F43
            Doc.Steam_flow_area = Math.PI / 4 * Doc.Number_of_head * Doc.Heating_section_transverse_rows * Math.Pow((Doc.Tube_diameter_of_heating_section / 1000 - 2 * Doc.Tube_thickness / 1000), 2);

            //F44
            Doc.Effective_radiation_layer_thickness = 0.9 * Doc.Tube_diameter_of_heating_section / 1000 * (4 * Doc.Relative_transverse_spacing * Doc.Relative_longitudinal_spacing / Math.PI - 1);

            //F45
            if(Doc.Heating_section_longitudinal_rows==1)
            {
                Doc.Correction_factor_for_tube_rows = 0.91;
            }
            else if((0.91+0.0125*(Doc.Heating_section_longitudinal_rows-2))>1)
            {
                Doc.Correction_factor_for_tube_rows = 1;
            }
            else
            {
                Doc.Correction_factor_for_tube_rows = (0.91 + 0.0125 * (Doc.Heating_section_longitudinal_rows - 2));
            }


            //----------------Get PID for Generic A----------------

            dt = mCal_GenericA_BLL.Get_PID_For_GenericA_Calculations();


            //----------------Insert Generic A Calculation Values---------------

            foreach (DataRow row in dt.Rows)
            {
                int pid = Convert.ToInt16(row["PID"].ToString());

                switch (pid)
                {
                    case 1:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Transverse_spacing,2), HeatingElement, Location);
                        break;

                    case 2:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Longitudinal_spacing,2), HeatingElement, Location);
                        break;

                    case 3:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Relative_transverse_spacing,2), HeatingElement, Location);
                        break;

                    case 4:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Relative_longitudinal_spacing,2), HeatingElement, Location);
                        break;

                    case 5:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Average_tube_calculated_length,2), HeatingElement, Location);
                        break;

                    case 6:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Heating_area_of_heating_section,2), HeatingElement, Location);
                        break;

                    case 7:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Length_of_roof_tube_within_heating_section_zone,2), HeatingElement, Location);
                        break;

                    case 8:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Heating_area_of_roof_tubes,2), HeatingElement, Location);
                        break;

                    case 9:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Heating_area_of_side_water_wall,2), HeatingElement, Location);
                        break;

                    case 10:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Heating_area_of_bottom_water_wall,2), HeatingElement, Location);
                        break;

                    case 11:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Heating_area_of_water_wall,2), HeatingElement, Location);
                        break;

                    case 12:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Inlet_flue_gas_flow_area,2), HeatingElement, Location);
                        break;

                    case 13:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Gas_outlet_flow_area,2), HeatingElement, Location);
                        break;

                    case 14:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Gas_average_flow_area,2), HeatingElement, Location);
                        break;

                    case 15:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Steam_flow_area,2), HeatingElement, Location);
                        break;

                    case 16:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Effective_radiation_layer_thickness,2), HeatingElement, Location);
                        break;

                    case 17:
                        mCal_GenericA_BLL.Insert_GenericA_Calculation(pid, ProjectID, BoilerID, SectionID, Math.Round(Doc.Correction_factor_for_tube_rows, 2), HeatingElement, Location);
                        break;
                }
            }

        }

    }
}
