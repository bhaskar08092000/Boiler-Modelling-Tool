using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.DAL;
using BoilerModellingTool.SC;
using System.Data;


namespace BoilerModellingTool.BLL
{
    public class Cal_Pressure_BLL
    {

        public DataSet GetInput_For_Cal_Pressure_Calculation(string BoilerID, string ProjectID,string BoilerLoad,int ObjectiveID)
        {
            Cal_Pressure_DAL mCal_Pressure_DAL = null;
            DataSet mDTable = null;

            mCal_Pressure_DAL = new Cal_Pressure_DAL();
            mDTable = new DataSet();

            mDTable = mCal_Pressure_DAL.GetInput_For_Cal_Pressure_Calculation(BoilerID,ProjectID,BoilerLoad,ObjectiveID);
            return mDTable;

        }

       public void Insert_Pressure_Calculation_Parameters(string ProjectID,string BoilerID,string SectionID,string SectionType,Double Value,string BoilerLoad,int ObjectiveID,string Parameter)
        {
            Cal_Pressure_DAL mCal_Pressure_DAL = new Cal_Pressure_DAL();
            mCal_Pressure_DAL.Insert_Pressure_Calculation(ProjectID, BoilerID,SectionID, SectionType, Value, BoilerLoad, ObjectiveID,Parameter);
        }

       public void Insert_Pressure_Calculation_Parameters_FixedParameters(string ProjectID, string BoilerID,  Double Value, string BoilerLoad, int ObjectiveID, string Parameter)
       {
           Cal_Pressure_DAL mCal_Pressure_DAL = new Cal_Pressure_DAL();
           mCal_Pressure_DAL.Insert_Pressure_Calculation_FixedParameters(ProjectID, BoilerID, Value, BoilerLoad, ObjectiveID, Parameter);
       }
        





        public DataSet GetInput_For_TotalHeatingSections(string BoilerID, string ProjectID)
        {
            Cal_Pressure_DAL mCal_Pressure_DAL = null;
            DataSet mDTable1 = null;

            mCal_Pressure_DAL = new Cal_Pressure_DAL();
            mDTable1 = new DataSet();

            mDTable1 = mCal_Pressure_DAL.GetInput_For_TotalHeatingSections(BoilerID,ProjectID);
            return mDTable1;

        }


        public DataSet GetInput_For_TotalHeatingSectionsOfCrossDuct(string BoilerID, string ProjectID)
        {
            Cal_Pressure_DAL mCal_Pressure_DAL = null;
            DataSet mDTable2 = null;

            mCal_Pressure_DAL = new Cal_Pressure_DAL();
            mDTable2 = new DataSet();

            mDTable2 = mCal_Pressure_DAL.GetInput_For_TotalHeatingSectionsOfCrossDuct(BoilerID, ProjectID);
            return mDTable2;

        }





    }
}
