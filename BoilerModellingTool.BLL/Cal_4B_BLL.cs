using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace BoilerModellingTool.BLL
{
    public class Cal_4B_BLL
    {
        public DataSet GetInput_For_4B_Calculation(string BoilerID, string ProjectID, string BoilerLoad)
        {
            Cal_4B_DAL mCal_4B_DAL = null;
            DataSet mDTable = null;

            mCal_4B_DAL = new Cal_4B_DAL();
            mDTable = new DataSet();

            mDTable = mCal_4B_DAL.GetInput_For_4B_Calculation(BoilerID, ProjectID, BoilerLoad);
            return mDTable;

        }
        public Double Get_2A_PrimaryAir_ReqEnthalpy(string BoilerID, string ProjectID, string InputVal)
        {
            Cal_4B_DAL mCal_4B_DAL = null;
            Double mDTable;

            mCal_4B_DAL = new Cal_4B_DAL();
            //mDTable = new DataSet();

            mDTable = mCal_4B_DAL.Get_2A_PrimaryAir_ReqEnthalpy(BoilerID, ProjectID, InputVal);
            return mDTable;

        }
        public Double Get_2A_TemperingAir_ReqEnthalpy(string BoilerID, string ProjectID, string InputVal)
        {
            Cal_4B_DAL mCal_4B_DAL = null;
            Double mDTable;

            mCal_4B_DAL = new Cal_4B_DAL();
            //mDTable = new DataSet();

            mDTable = mCal_4B_DAL.Get_2A_TemperingAir_ReqEnthalpy(BoilerID, ProjectID, InputVal);
            return mDTable;

        }
        public Double Get_2A_Recirculated_gas_temperature(string BoilerID, string ProjectID, string BoilerLoad)
        {
            Cal_4B_DAL mCal_4B_DAL = null;
            Double mDTable;

            mCal_4B_DAL = new Cal_4B_DAL();
            //mDTable = new DataSet();

            mDTable = mCal_4B_DAL.Get_2A_Recirculated_gas_enthalpy(BoilerID, ProjectID, BoilerLoad);
            return mDTable;

        }
        public DataSet Get_submodule_heating_section_in_upper_furnace(String BoilerID, String ProjectID)
        {
            Cal_4B_DAL mCal_4B_DAL = null;
            DataSet mDTable = null;

            mCal_4B_DAL = new Cal_4B_DAL();
            mDTable = new DataSet();

            mDTable = mCal_4B_DAL.Get_submodule_heating_section_in_upper_furnace(BoilerID, ProjectID);

            return mDTable;

        }
        public Double Get_2A_Recirculated_gas_enthalpy(string BoilerID, string ProjectID, string InputVal)
        {
            Cal_4B_DAL mCal_4B_DAL = null;
            Double mDTable;

            mCal_4B_DAL = new Cal_4B_DAL();
            //mDTable = new DataSet();

            mDTable = mCal_4B_DAL.Get_2A_Recirculated_gas_enthalpy(BoilerID, ProjectID, InputVal);
            return mDTable;

        }
        public DataSet Get_Design_Area_From_Section_ID_in_4B(String BoilerID, String ProjectID, String SectionID)
        {
            Cal_4B_DAL mCal_4B_DAL = null;
            DataSet mDTable = null;

            mCal_4B_DAL = new Cal_4B_DAL();
            mDTable = new DataSet();

            mDTable = mCal_4B_DAL.Get_Design_Area_From_Section_ID_in_4B(BoilerID, ProjectID, SectionID);
            return mDTable;
        }
        public string GetLast_Element_of_HeatingSectionUpperFurnace(string BoilerID, string ProjectID)
        {
            Cal_4B_DAL mCal_4B_DAL = null;
            string mDTable;

            mCal_4B_DAL = new Cal_4B_DAL();
            //mDTable = new DataSet();

            mDTable = mCal_4B_DAL.GetLast_Element_of_HeatingSectionUpperFurnace(BoilerID, ProjectID);
            return mDTable;
        }
        public Double Get_Tsat_p(Double P_Val)
        {
            Cal_4B_DAL mCal_4B_DAL = null;
            Double mDTable;

            mCal_4B_DAL = new Cal_4B_DAL();

            mDTable = mCal_4B_DAL.Get_Tsat_p(P_Val);
            return mDTable;

        }
        public Double Get_2A_FEGT_ReqEnthalpy(string BoilerID, string ProjectID, string InputVal)
        {
            Cal_4B_DAL mCal_4B_DAL = null;
            Double mDTable;

            mCal_4B_DAL = new Cal_4B_DAL();
            //mDTable = new DataSet();

            mDTable = mCal_4B_DAL.Get_2A_FEGT_ReqEnthalpy(BoilerID, ProjectID, InputVal);
            return mDTable;

        }
        public Double Get_avg_loc_vert_dir_4B_Cal(string BoilerID, string ProjectID, string SectionID,double water_wall_HF,double Nose_inlet_HF)
        {
            Cal_4B_DAL mCal_4B_DAL = null;
            Double mDTable;

            mCal_4B_DAL = new Cal_4B_DAL();

            mDTable = mCal_4B_DAL.Get_avg_loc_vert_dir_4B_Cal(BoilerID,ProjectID,SectionID,water_wall_HF,Nose_inlet_HF);
            return mDTable;

        }
        public void Insert_4B_Calculation(int pid, string BoilerID, string ProjectID, string BoilerLoad,int ObjeCtive, string Value)
        {

            Cal_4B_DAL mCal_4B_DAL = new Cal_4B_DAL();
            DataSet mDts = null;
            mDts = new DataSet();

            mDts = mCal_4B_DAL.Insert_4B_Calculation(pid, BoilerID, ProjectID, BoilerLoad,ObjeCtive, Value);
        }
        public DataTable Get_PID_For_4B_Calculation()
        {
            Cal_4B_DAL mCal_4B_DAL = null;
            DataTable mDTable = null;

            mCal_4B_DAL = new Cal_4B_DAL();
            mDTable = new DataTable();

            mDTable = mCal_4B_DAL.Get_PID_For_4B_Calculation();
            return mDTable;

        }

        public Double Get_2A_AdiabaticTemp_ReqTemp(string BoilerID, string ProjectID, string InputVal)
        {
            Cal_4B_DAL mCal_4B_DAL = null;
            Double mDTable;

            mCal_4B_DAL = new Cal_4B_DAL();
            //mDTable = new DataSet();

            mDTable = mCal_4B_DAL.Get_2A_AdiabaticTemp_ReqTemp(BoilerID, ProjectID, InputVal);
            return mDTable;

        }
       


    }
}
