using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;

namespace BoilerModellingTool.BLL
{
    public class Cal_Flow_BLL
    {
        public Cal_Flow_SC Get_Flow_CalculationInput(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,int TypeOfBoiler,int SHDesuperheatingSpray)
        {
            Cal_Flow_SC mCal_Flow_SC = new Cal_Flow_SC();
            Cal_Flow_DAL mCal_Flow_DAL = new Cal_Flow_DAL();
            mCal_Flow_SC = mCal_Flow_DAL.Get_InputFor_Flow_Calculations(ProjectID,BoilerID,BoilerLoad,ObjectiveID,TypeOfBoiler,SHDesuperheatingSpray);
            return mCal_Flow_SC;
        }

        public string Get_SectionID_From_MiscellaneousSH(string ProjectID, string BoilerID, int SectionNumber)
        {
            string SectionID;
            Cal_Flow_DAL mCal_Flow_DAL = new Cal_Flow_DAL();
            SectionID = mCal_Flow_DAL.Get_SectionID_From_MiscellaneousSH(ProjectID,BoilerID,SectionNumber);
            return SectionID;
        }

        public int Get_SectionNumber_Of_SectionID(string ProjectID,string BoilerID,string SectionID)
        {
            int a = 0;
            Cal_Flow_DAL mCal_Flow_DAL = new Cal_Flow_DAL();
            a = mCal_Flow_DAL.Get_SectionNumber_Of_SectionID(ProjectID,BoilerID,SectionID);
            return a;
        }

        public void Insert_FlowModule_Calculation_FixedParameters(string ProjectID,string BoilerID,string SectionType,Double Value,string BoilerLoad,int ObjectiveID)
        {
            Cal_Flow_DAL mCal_Flow_DAL = new Cal_Flow_DAL();
            mCal_Flow_DAL.Insert_Flow_FixedParameters(ProjectID, BoilerID, SectionType, Value, BoilerLoad, ObjectiveID);
        }
   
        public void Insert_FlowModule_Calculation_DynamicParameters(string ProjectID,string BoilerID,string SectionID,string SectionType,Double Value,string BoilerLoad,int ObjectiveID)
        {
            Cal_Flow_DAL mCal_Flow_DAL = new Cal_Flow_DAL();
            mCal_Flow_DAL.Insert_Flow_DynamicParameters(ProjectID, BoilerID, SectionID, SectionType, Value, BoilerLoad, ObjectiveID);
        }
        
    }
}
