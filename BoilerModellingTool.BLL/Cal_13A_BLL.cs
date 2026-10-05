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
    public class Cal_13A_BLL
    {
        public Cal_13A_SC Get_13A_Calculation_Input_ReverseChamber(string ProjectID,string BoilerID)
        {
            Cal_13A_SC mCal_13A_SC = new Cal_13A_SC();
            Cal_13A_DAL mCal_13A_DAL = new Cal_13A_DAL();
            mCal_13A_SC = mCal_13A_DAL.Get_13A_CalculationValues(ProjectID, BoilerID);
            return mCal_13A_SC;
        }

        public DataTable Get_PID_For_13A_Calculation()
        {
            Cal_13A_DAL mCal_13A_Dal = new Cal_13A_DAL();
            DataTable dt = new DataTable();
            dt=mCal_13A_Dal.Get_PID_For_13A_Calculation();
            return dt;
        }

        public void Insert_13A_Calculation(int PID, string ProjectID, string BoilerID, double Value)
        {
            Cal_13A_DAL mCal_13A_DAL = new Cal_13A_DAL();
            mCal_13A_DAL.Insert_13A_Calculation(PID,ProjectID, BoilerID, Value);
        }

    }
}
