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
    public class Cal_HeatAbsorption_BLL
    {
        public DataSet GetInput_For_Cal_HeatAbsorption_Calculation(string BoilerID, string ProjectID, string BoilerLoad, int ObjectiveID)
        {


           Cal_HeatAbsorption_DAL mCal_HeatAbsorption_DAL = null;
            DataSet mDTable = null;

            mCal_HeatAbsorption_DAL = new Cal_HeatAbsorption_DAL();
            mDTable = new DataSet();

            mDTable = mCal_HeatAbsorption_DAL.GetInput_For_HeatAbsorption_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID);
            return mDTable;

        }

        public void Insert_HeatAbsorption_Calculation_Parameters(string ProjectID, string BoilerID, string SectionID, string SectionType, Double Value, string BoilerLoad, int ObjectiveID, string Parameter)
        {
            Cal_HeatAbsorption_DAL mCal_HeatAbsorption_DAL = new Cal_HeatAbsorption_DAL();
            mCal_HeatAbsorption_DAL.Insert_HeatAbsorption_Calculation(ProjectID, BoilerID, SectionID, SectionType, Value, BoilerLoad, ObjectiveID, Parameter);
        }



    }
}
