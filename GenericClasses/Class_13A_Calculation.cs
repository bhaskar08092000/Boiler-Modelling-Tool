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
    public class Class_13A_Calculation
    {
        public Cal_13A_SC Doc { get; set; }

        public void Calculation_For_13A(string Project_ID,string Boiler_ID,int Objective_ID,string Boiler_Load,int RoofCoolingMedium,int EcoHangerTubePresent)
        {

            DataTable dt = new DataTable();

            Cal_13A_BLL mCal_13A_BLL = new Cal_13A_BLL();

            this.Doc = mCal_13A_BLL.Get_13A_Calculation_Input_ReverseChamber(Project_ID, Boiler_ID);

            //-------------------------13A Calculation------------------------------

            if (RoofCoolingMedium==2)
            {
                //F20
                Doc.Total_side_wall_area = 2 * Doc.Reversing_chamber_height * Doc.Reversing_chamber_depth;

                //F21
                Doc.Rear_wall_area = Doc.Reversing_chamber_width * Doc.Reversing_chamber_height;

                //F22
                Doc.Roof_area = Doc.Reversing_chamber_width * Doc.Reversing_chamber_depth;
            }
            else
            {
                //F20
                Doc.Total_side_wall_area = 0;

                //F21
                Doc.Rear_wall_area = 0;

                //F22
                Doc.Roof_area = 0;
            }
            

            //F23
            Doc.Total_heating_area = Doc.Total_side_wall_area + Doc.Rear_wall_area + Doc.Roof_area;

            //F24
            Doc.Radiation_heating_area = Doc.Reversing_chamber_configuration_factor * Doc.Total_heating_area;

            //F25
            Doc.Reversing_chamber_peripheral_area = Doc.Total_side_wall_area + 2 * Doc.Rear_wall_area + 2 * Doc.Roof_area;

            //F26
            Doc.Reversing_chamber_volume = 0.5 * Doc.Total_side_wall_area * Doc.Reversing_chamber_width;

            //F27
            Doc.Effective_radiation_layer_thickness = 3.6 * Doc.Reversing_chamber_volume / Doc.Reversing_chamber_peripheral_area;

            if (RoofCoolingMedium == 2 && EcoHangerTubePresent==1)
            {
                //F28
                Doc.Eco_hanger_heating_area_in_zone = 2 * Doc.Total_number_of_eco_Hanger_tube_in_zone * (Doc.Eco_Hanger_tube_length_in_reverse_chamber_zone) * Doc.Tube_diameter / 1000;

                //F29
                Doc.Eco_Hanger_circumferential_area_in_zone = Math.PI * Doc.Tube_diameter / 1000 * Doc.Eco_Hanger_tube_length_in_reverse_chamber_zone * Doc.Total_number_of_eco_Hanger_tube_in_zone;
            }
            else
            {
                Doc.Eco_hanger_heating_area_in_zone = 0;

                Doc.Eco_Hanger_circumferential_area_in_zone = 0;
            }


            //-------------Get PID for 13A Calculation-------------------

            dt = mCal_13A_BLL.Get_PID_For_13A_Calculation();


            //------------Insert 13A Calculation-------------------------

            foreach(DataRow row in dt.Rows)
            {
                int pid = Convert.ToInt16(row["PID"].ToString());

                switch (pid)
                {
                    case 1:
                        mCal_13A_BLL.Insert_13A_Calculation(pid, Project_ID, Boiler_ID, Doc.Total_side_wall_area);
                        break;

                    case 2:
                        mCal_13A_BLL.Insert_13A_Calculation(pid, Project_ID, Boiler_ID, Doc.Rear_wall_area);
                        break;

                    case 3:
                        mCal_13A_BLL.Insert_13A_Calculation(pid, Project_ID, Boiler_ID, Doc.Roof_area);
                        break;

                    case 4:
                        mCal_13A_BLL.Insert_13A_Calculation(pid, Project_ID, Boiler_ID, Doc.Total_heating_area);
                        break;

                    case 5:
                        mCal_13A_BLL.Insert_13A_Calculation(pid, Project_ID, Boiler_ID, Doc.Radiation_heating_area);
                        break;

                    case 6:
                        mCal_13A_BLL.Insert_13A_Calculation(pid, Project_ID, Boiler_ID, Doc.Reversing_chamber_peripheral_area);
                        break;

                    case 7:
                        mCal_13A_BLL.Insert_13A_Calculation(pid, Project_ID, Boiler_ID, Doc.Reversing_chamber_volume);
                        break;

                    case 8:
                        mCal_13A_BLL.Insert_13A_Calculation(pid, Project_ID, Boiler_ID, Doc.Effective_radiation_layer_thickness);
                        break;

                    case 9:
                        mCal_13A_BLL.Insert_13A_Calculation(pid, Project_ID, Boiler_ID, Doc.Eco_hanger_heating_area_in_zone);
                        break;

                    case 10:
                        mCal_13A_BLL.Insert_13A_Calculation(pid, Project_ID, Boiler_ID, Doc.Eco_Hanger_circumferential_area_in_zone);
                        break;

                }

            }

        }

    }
}
