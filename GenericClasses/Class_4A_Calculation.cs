using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using BoilerModellingTool.SC;
using BoilerModellingTool.BLL;

namespace GenericClasses
{
   public class Class_4A_Calculation
    {
        //string BoilerID = "F85C19BA-20C9-4B61-975D-636C8404F30A";
        //string ProjectID = "EBB9A3CD-916B-464A-9CC6-88EAD4850A91";
        //string BoilerLoad = "100%TMCR";
        //int ObjectiveID = 1;

         string BoilerID = null;
        string ProjectID = null;
        string SectionID = null;
        string BoilerLoad = null;
        int ObjectiveID = 0;


        public Class_4A_Calculation(string BoilerID, string ProjectID,
             string BoilerLoad ,int ObjectiveID)
        {
            
            this.BoilerID = BoilerID;
            this.ProjectID = ProjectID;          
            this.BoilerLoad = BoilerLoad;
            this.ObjectiveID = ObjectiveID;

        }


        public void Calculation_4A(string SectionID)
        {

        Cal_4A_SC mCal_4A_SC=null;
        Cal_4A_BLL mCal_4A_BLL=null;
        DataSet mdataset = null;
        DataTable mDataTable=null;


        try
        {
            mCal_4A_SC = new Cal_4A_SC();
            mCal_4A_BLL = new Cal_4A_BLL();
            mdataset = new DataSet();

            mdataset = mCal_4A_BLL.GetInput_For_4A_Calculation(BoilerID, ProjectID,BoilerLoad,ObjectiveID,SectionID);


            mCal_4A_SC.Furnace_width=Convert.ToDouble(mdataset.Tables[0].Rows[0]["Value"].ToString());

            mCal_4A_SC.Furnace_depth=Convert.ToDouble(mdataset.Tables[0].Rows[1]["Value"].ToString());

            mCal_4A_SC.Dry_bottom_hopper_inclination=Convert.ToDouble(mdataset.Tables[0].Rows[2]["Value"].ToString());

            mCal_4A_SC.Dry_bottom_hopper_outlet_depth=Convert.ToDouble(mdataset.Tables[0].Rows[3]["Value"].ToString());

            mCal_4A_SC.dry_bottom_hopper_height=Convert.ToDouble(mdataset.Tables[0].Rows[4]["Value"].ToString());

            mCal_4A_SC.Furnace_nose_length=Convert.ToDouble(mdataset.Tables[0].Rows[5]["Value"].ToString());

            mCal_4A_SC.Furnace_nose_up_dip_angle=Convert.ToDouble(mdataset.Tables[0].Rows[6]["Value"].ToString());

            mCal_4A_SC.Furnace_nose_elevation_angle=Convert.ToDouble(mdataset.Tables[0].Rows[7]["Value"].ToString());
          
            mCal_4A_SC.Platen_superheater_depth=Convert.ToDouble(mdataset.Tables[1].Rows[0]["Value"].ToString());

            mCal_4A_SC.Distance_from_platen_superheater_back_side_to_furnace_nose=Convert.ToDouble(mdataset.Tables[2].Rows[1]["Value"].ToString());
             
            mCal_4A_SC.Vertical_part_height_of_furnace_nose =Convert.ToDouble(mdataset.Tables[2].Rows[0]["Value"].ToString());

            mCal_4A_SC.Height_of_platen_superheater=Convert.ToDouble(mdataset.Tables[3].Rows[0]["Value"].ToString());
             
            mCal_4A_SC.Depth_of_empty_room_space_before_the_SH=Convert.ToDouble(mdataset.Tables[4].Rows[0]["Value"].ToString());
             
            mCal_4A_SC.Furnace_main_body_height =Convert.ToDouble(mdataset.Tables[4].Rows[1]["Value"].ToString());

            mCal_4A_SC.Tube_diameter_of_water_wall=Convert.ToDouble(mdataset.Tables[4].Rows[2]["Value"].ToString());

            mCal_4A_SC.Tube_spacing =Convert.ToDouble(mdataset.Tables[4].Rows[4]["Value"].ToString());
            
            mCal_4A_SC.Roof_tube_diameter= Convert.ToDouble(mdataset.Tables[4].Rows[5]["Value"].ToString());

            mCal_4A_SC.Roof_tube_thickness= Convert.ToDouble(mdataset.Tables[4].Rows[6]["Value"].ToString());
          
            mCal_4A_SC.Roof_tube_row_number = Convert.ToDouble(mdataset.Tables[4].Rows[7]["Value"].ToString());

            mCal_4A_SC.Design_Fuel_Consumption=Convert.ToDouble(mdataset.Tables[5].Rows[0]["Value"].ToString());;

            mCal_4A_SC.Lower_heating_value_on_as_received_basis = Convert.ToDouble(mdataset.Tables[6].Rows[0]["Value"].ToString()); ;

            
            mDataTable =mdataset.Tables[7];

           
            //Calculation formulas
        mCal_4A_SC.Furnace_top_volume1 = mCal_4A_SC.Depth_of_empty_room_space_before_the_SH*mCal_4A_SC.Height_of_platen_superheater*mCal_4A_SC.Furnace_width;

        mCal_4A_SC.Calculated_furnace_cross_section_area = mCal_4A_SC.Furnace_width*mCal_4A_SC.Furnace_depth; 

        mCal_4A_SC.Furnace_nose_downward_inclination_angleheight= mCal_4A_SC.Furnace_nose_length*Math.Tan(3.141592654/180*mCal_4A_SC.Furnace_nose_elevation_angle);  

        mCal_4A_SC.Furnace_top_volume2 =  (mCal_4A_SC.Furnace_depth-mCal_4A_SC.Furnace_nose_length)*mCal_4A_SC.Furnace_nose_downward_inclination_angleheight*mCal_4A_SC.Furnace_width;
               
        mCal_4A_SC.Furnace_top_volume=mCal_4A_SC.Furnace_top_volume1+  mCal_4A_SC.Furnace_top_volume2; 
 
        mCal_4A_SC.Main_body_volume =mCal_4A_SC.Furnace_main_body_height*mCal_4A_SC.Calculated_furnace_cross_section_area;

        mCal_4A_SC.Outlet_size_of_membrane_wall_at_dry_bottom_hoppermiddle= (mCal_4A_SC.Dry_bottom_hopper_outlet_depth+ mCal_4A_SC.Furnace_depth)/2;  

        mCal_4A_SC.Dry_bottom_hopper_volume =0.5*(mCal_4A_SC.Furnace_depth+ mCal_4A_SC.Outlet_size_of_membrane_wall_at_dry_bottom_hoppermiddle)*mCal_4A_SC.dry_bottom_hopper_height*mCal_4A_SC.Furnace_width;
   
        mCal_4A_SC.Calculated_furnace_volume=mCal_4A_SC.Dry_bottom_hopper_volume +  mCal_4A_SC.Main_body_volume+  mCal_4A_SC.Furnace_top_volume;

        mCal_4A_SC.Furnace_volume_thermal_load=(1000*mCal_4A_SC.Design_Fuel_Consumption*mCal_4A_SC.Lower_heating_value_on_as_received_basis/ mCal_4A_SC.Calculated_furnace_volume)/1000;
    
        mCal_4A_SC.Aspect_ratio_of_furnace_section   = mCal_4A_SC.Furnace_width/mCal_4A_SC.Furnace_depth;

        mCal_4A_SC.Furnace_sectional_thermal_load = ((1000 * mCal_4A_SC.Design_Fuel_Consumption * mCal_4A_SC.Lower_heating_value_on_as_received_basis) / mCal_4A_SC.Calculated_furnace_cross_section_area) / (double)Math.Pow(10, 6);

        mCal_4A_SC.Furnace_nose_outlet_height =  mCal_4A_SC.Height_of_platen_superheater-mCal_4A_SC.Vertical_part_height_of_furnace_nose;
          
        mCal_4A_SC.Height_furnace_top=mCal_4A_SC.Height_of_platen_superheater+ mCal_4A_SC.Furnace_nose_downward_inclination_angleheight;   

        mCal_4A_SC.Roof_tube_spacing = mCal_4A_SC.Furnace_width*1000/mCal_4A_SC.Roof_tube_row_number;  

        mCal_4A_SC.Heating_area_roof_tubes_Upperfurnace =mCal_4A_SC.Furnace_width*mCal_4A_SC.Depth_of_empty_room_space_before_the_SH;   

        mCal_4A_SC.Halfdry_bottom_hopperarea= (2* mCal_4A_SC.Furnace_width/Math.Sin(mCal_4A_SC.Dry_bottom_hopper_inclination*3.141592654/180)
        +2*(mCal_4A_SC.Furnace_depth+mCal_4A_SC.Outlet_size_of_membrane_wall_at_dry_bottom_hoppermiddle)/2) *mCal_4A_SC.dry_bottom_hopper_height; 

        mCal_4A_SC.Noseregion_side_wallarea=  2*mCal_4A_SC.Furnace_nose_downward_inclination_angleheight*(mCal_4A_SC.Furnace_depth-mCal_4A_SC.Furnace_nose_length);  

        mCal_4A_SC.Upperfurnace_side_wallarea=mCal_4A_SC.Height_of_platen_superheater*mCal_4A_SC.Depth_of_empty_room_space_before_the_SH*2;    

        mCal_4A_SC.Rearwall_area_noseregion =( mCal_4A_SC.Furnace_nose_downward_inclination_angleheight+mCal_4A_SC.Furnace_nose_length)*mCal_4A_SC.Furnace_width; 

        mCal_4A_SC.Exitwindowarea_lowerfurnace= (mCal_4A_SC.Furnace_depth-mCal_4A_SC.Furnace_nose_length)*mCal_4A_SC.Furnace_width;

        mCal_4A_SC.Lowerfurnace_enclosedarea= mCal_4A_SC.Halfdry_bottom_hopperarea+(2*(mCal_4A_SC.Furnace_width+mCal_4A_SC.Furnace_depth)
        *mCal_4A_SC.Furnace_main_body_height)+mCal_4A_SC.Noseregion_side_wallarea+
        (mCal_4A_SC.Furnace_width*mCal_4A_SC.Furnace_nose_downward_inclination_angleheight)+mCal_4A_SC.Rearwall_area_noseregion+mCal_4A_SC.Exitwindowarea_lowerfurnace;

        mCal_4A_SC.Lowerfurnace_EPRS_area=1.2*mCal_4A_SC.Exitwindowarea_lowerfurnace+0.9*(mCal_4A_SC.Lowerfurnace_enclosedarea-mCal_4A_SC.Exitwindowarea_lowerfurnace);

        mCal_4A_SC.Lowerfurnace_volume=mCal_4A_SC.Dry_bottom_hopper_volume+mCal_4A_SC.Main_body_volume+mCal_4A_SC.Furnace_top_volume2;    

        mCal_4A_SC.Thickness_effective_radiationlayer_lowerfurnace= 3.6*mCal_4A_SC.Lowerfurnace_volume/mCal_4A_SC.Lowerfurnace_enclosedarea;   

        mCal_4A_SC.Upperfurnace_absorbingarea   =mCal_4A_SC.Upperfurnace_side_wallarea+(mCal_4A_SC.Furnace_width*mCal_4A_SC.Height_of_platen_superheater)+mCal_4A_SC.Heating_area_roof_tubes_Upperfurnace;

        mCal_4A_SC.Upperfurnace_exitwindowarea  =(mCal_4A_SC.Height_of_platen_superheater+mCal_4A_SC.Platen_superheater_depth+mCal_4A_SC.Distance_from_platen_superheater_back_side_to_furnace_nose)*mCal_4A_SC.Furnace_width;

        mCal_4A_SC.Thickness_effective_radiation_upperfurnace= 3.6* mCal_4A_SC.Furnace_top_volume1/(mCal_4A_SC.Upperfurnace_absorbingarea+mCal_4A_SC.Upperfurnace_exitwindowarea);

         // ddl_boiler.SelectedItem.Value = BoilerID;
        // ddl_project.SelectedItem.Value = ProjectID; 




              for (int i = 0; i < mDataTable.Rows.Count; i++)
             {
                 int PID = Convert.ToInt16(mDataTable.Rows[i]["PID"].ToString());
               

                switch(PID)
            {

                case 1: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Furnace_volume_thermal_load.ToString(), PID,BoilerLoad,ObjectiveID);

                    break;

                case 2: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Calculated_furnace_volume.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;

                case 3: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Furnace_sectional_thermal_load.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 4: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Calculated_furnace_cross_section_area.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 5: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Aspect_ratio_of_furnace_section.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 6: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Outlet_size_of_membrane_wall_at_dry_bottom_hoppermiddle.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 7: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Dry_bottom_hopper_volume.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 8: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Furnace_nose_outlet_height.ToString(), PID, BoilerLoad, ObjectiveID);
                    break;
                case 9: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Furnace_nose_downward_inclination_angleheight.ToString(), PID, BoilerLoad, ObjectiveID);
                    break;
                case 10: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Height_furnace_top.ToString(), PID, BoilerLoad, ObjectiveID);
                    break;
                case 11: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Furnace_top_volume1.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 12: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Furnace_top_volume2.ToString(), PID, BoilerLoad, ObjectiveID);
                    break;
                case 13: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Furnace_top_volume.ToString(), PID, BoilerLoad, ObjectiveID);
                    break;
                case 14: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Main_body_volume.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 15: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Roof_tube_spacing.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 16: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Heating_area_roof_tubes_Upperfurnace.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 17: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Halfdry_bottom_hopperarea.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 18: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Noseregion_side_wallarea.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 19: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Upperfurnace_side_wallarea.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 20: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Rearwall_area_noseregion.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 21: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Exitwindowarea_lowerfurnace.ToString(), PID, BoilerLoad, ObjectiveID);
                    break;
                case 22: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Lowerfurnace_enclosedarea.ToString(), PID, BoilerLoad, ObjectiveID);
                    break;
                case 23: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Lowerfurnace_EPRS_area.ToString(), PID, BoilerLoad, ObjectiveID);
                    break;
                case 24: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Lowerfurnace_volume.ToString(), PID, BoilerLoad, ObjectiveID);
                    break;
                case 25: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Thickness_effective_radiationlayer_lowerfurnace.ToString(), PID, BoilerLoad, ObjectiveID);
                    break;
                case 26: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Upperfurnace_absorbingarea.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 27: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Upperfurnace_exitwindowarea.ToString(), PID, BoilerLoad, ObjectiveID);

                    break;
                case 28: mCal_4A_BLL.Insert_4A_Calculation(BoilerID, ProjectID, mCal_4A_SC.Thickness_effective_radiation_upperfurnace.ToString(), PID, BoilerLoad, ObjectiveID);
                    break;
                   
                default:
                    break;

            }



             }



        }
        catch
        {

        }
    }
        }


    }

