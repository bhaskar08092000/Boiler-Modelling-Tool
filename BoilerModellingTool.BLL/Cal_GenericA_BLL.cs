using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;
using System.Data;

namespace BoilerModellingTool.BLL
{
    public class Cal_GenericA_BLL
    {
        public Cal_GenericA_SC Get_InputFor_GenericA(string ProjectID,string BoilerID,string SectionID,string HeatingElement)
        {
            Cal_GenericA_SC mCal_GenericA_SC = null;
            Cal_GenericA_DAL mCal_GenericA_DAL = null;
            mCal_GenericA_DAL = new Cal_GenericA_DAL();
            mCal_GenericA_SC = mCal_GenericA_DAL.Get_InputFor_GenericA(ProjectID, BoilerID, SectionID, HeatingElement);
            return mCal_GenericA_SC;
        }

        public DataTable Get_PID_For_GenericA_Calculations()
        {
            Cal_GenericA_DAL mCal_GenericA_DAL = new Cal_GenericA_DAL();
            DataTable dt = new DataTable();
            dt = mCal_GenericA_DAL.Get_PID_For_GenericA_Calculation();
            return dt;
        }

        public void Insert_GenericA_Calculation(int PID,string ProjectID,string BoilerID,string SectionID,double Value,string HeatingElement,string Location)
        {
            Cal_GenericA_DAL mCal_GenericA_DAL = new Cal_GenericA_DAL();
            mCal_GenericA_DAL.Insert_GenericA_Calculation(PID, ProjectID, BoilerID, SectionID, Value, HeatingElement, Location);
        }

    }
}
