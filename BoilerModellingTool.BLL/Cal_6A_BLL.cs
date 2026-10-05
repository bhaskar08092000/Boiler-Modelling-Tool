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
    public class Cal_6A_BLL
    {

        public void Insert_6A_Value(int pid, string BoilerID, string ProjectID, string BoilerLoad, string Value, string SectionID, int Objectiveid)
        {

            Cal_6A_DAL mCal_6A_DAL = new Cal_6A_DAL();
            DataSet mDts = null;
            mDts = new DataSet();

            mDts = mCal_6A_DAL.Insert_6A_Values_DAL(pid, BoilerID, ProjectID, BoilerLoad, Value, SectionID, Objectiveid);
        }

        public string GetLast_Element_of_HeatingSectionUpperFurnace(string BoilerID, string ProjectID)
        {
            Cal_6A_DAL mCal_6A_DAL = new Cal_6A_DAL();
            string mDts = null;

            mDts = mCal_6A_DAL.GetLast_Element_of_HeatingSectionUpperFurnace(BoilerID, ProjectID);

            return mDts;

        }
        public DataSet GetInput_For_6A_Calculation(string BoilerID, string ProjectID, string BoilerLoad,string SectionID,string SectionType)
        {
            Cal_6A_DAL mCal_6A_DAL = new Cal_6A_DAL();
            DataSet mDts = null;

            mDts = mCal_6A_DAL.GetInput_For_6A_Calculation(BoilerID, ProjectID, BoilerLoad,SectionID,SectionType);

            return mDts;
        }

        public DataSet Get_6A_parameters()
        {
            Cal_6A_DAL mCal_6A_DAL = new Cal_6A_DAL();
            DataSet mDts = null;

            mDts = mCal_6A_DAL.Get_6A_parameter_dal();

            return mDts;
        }
    }
}
