using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using BoilerModellingTool.DAL;
//using BoilerModellingTool.SC;

namespace BoilerModellingTool.BLL
{
    public class Cal_5C_BLL
    {
        public DataSet Get_Input_For_5C_Calculation(string BolierID, string ProjectID, string BoilerID, string SectionID)
        {
            Cal_5C_DAL mCal_5C_DAL = null;
            DataSet mDTable = null;

            mCal_5C_DAL = new Cal_5C_DAL();
            mDTable = new DataSet();

            mDTable = mCal_5C_DAL.Get_Input_For_5C_Calculation(BolierID, ProjectID, BoilerID, SectionID);
            return mDTable;

        }
        public DataSet Get_Thermal_Conductivity_For_5C_Calculation_(string BoilerID, string ProjectID, string SectionID, int ObjectiveID, int TypesTubes, int Tube_Position)
        {
            Cal_5C_DAL mCal_5C_DAL = null;
            DataSet mDTable = null;

            mCal_5C_DAL = new Cal_5C_DAL();
            mDTable = new DataSet();

            mDTable = mCal_5C_DAL.Get_Thermal_Conductivity_For_5C_Calculation_(BoilerID, ProjectID, SectionID, ObjectiveID, TypesTubes, Tube_Position);
            return mDTable;
        }
        public DataSet Get_MAterial_Section_For_5C_Calculation(string BoilerID, string ProjectID, string SectionID, int ObjectiveID, int TypesTubes, int Tube_Position, string Section_name)
        {
            Cal_5C_DAL mCal_5C_DAL = null;
            DataSet mDTable = null;

            mCal_5C_DAL = new Cal_5C_DAL();
            mDTable = new DataSet();

            mDTable = mCal_5C_DAL.Get_MAterial_Section_For_5C_Calculation(BoilerID, ProjectID, SectionID, ObjectiveID, TypesTubes, Tube_Position, Section_name);
            return mDTable;
        }
        public DataSet Get_Input_For_5C_Calculation_Of_Area(string BoilerID, string ProjectID, string SectionID, int ObjectiveID, int TypesTubes, int Tube_Position)
        {
            Cal_5C_DAL mCal_5C_DAL = null;
            DataSet mDTable = null;

            mCal_5C_DAL = new Cal_5C_DAL();
            mDTable = new DataSet();

            mDTable = mCal_5C_DAL.Get_Input_For_5C_Calculation_Of_Area(BoilerID, ProjectID, SectionID, ObjectiveID, TypesTubes, Tube_Position);
            return mDTable;
        }
        public DataSet Get_Input_For_5C_Calculation_Vertical_Section(int TypesTubes_1, int TypesTubes_2)
        {
            Cal_5C_DAL mCal_5C_DAL = null;
            DataSet mDTable = null;

            mCal_5C_DAL = new Cal_5C_DAL();
            mDTable = new DataSet();

            mDTable = mCal_5C_DAL.Get_Input_For_5C_Calculation_Vertical_Section(TypesTubes_1, TypesTubes_2);
            return mDTable;
        }
        public DataSet Get_Input_For_5C_Ratio_DR_each_section(string BoilerID, string ProjectID, string SectionID, int ObjectiveID, int TypesTubes, int Tube_Position)
        {
            Cal_5C_DAL mCal_5C_DAL = null;
            DataSet mDTable = null;

            mCal_5C_DAL = new Cal_5C_DAL();
            mDTable = new DataSet();

            mDTable = mCal_5C_DAL.Get_Input_For_5C_Ratio_DR_each_section(BoilerID, ProjectID, SectionID, ObjectiveID, TypesTubes, Tube_Position);
            return mDTable;
        }
        public void Insert_Metal_Temperature_For_Heating_Element(string BoilerID, string ProjectID, string sectionID,
        string BoilerLoad, string ObjectiveID, string LoopID, string Steam_Temperature_last,
        string MAx_metal_temp, string Section_metal_temp,
        string Material_metal_temp, string metal_temp_last, string material_Metal_temp_last
        )
        {
            Cal_5C_DAL mCal_5C_DAL = null;
            DataSet mDTable = null;

            mCal_5C_DAL = new Cal_5C_DAL();
            mDTable = new DataSet();

            mCal_5C_DAL.Insert_Metal_Temperature_For_Heating_Element(BoilerID, ProjectID, sectionID,
             BoilerLoad, ObjectiveID, LoopID, Steam_Temperature_last,
             MAx_metal_temp, Section_metal_temp,
             Material_metal_temp, metal_temp_last, material_Metal_temp_last
            );
        }
    }
}
