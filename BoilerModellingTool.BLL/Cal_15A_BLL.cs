using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;

namespace BoilerModellingTool.BLL
{
    public class Cal_15A_BLL
    {
        public Cal_15A_SC Get_Input_For_15A_Calculation(string BolierID, string ProjectID, string SectionID)
        {
            Cal_15A_DAL mCal_15A_DAL = null;
            Cal_15A_SC mCal_15A_SC = new Cal_15A_SC();
            DataSet mDTable = null;

            mCal_15A_DAL = new Cal_15A_DAL();
            mDTable = new DataSet();

            mCal_15A_SC = mCal_15A_DAL.GetInput_For_15A_Calculation(BolierID, ProjectID, SectionID);
            return mCal_15A_SC;
        }

        public void Insert_15A_Calculation(string BoilerID, string ProjectID, int PID, string SectionID, string Value)
        {
            Cal_15A_DAL mCal_15A_DAL = null;
            mCal_15A_DAL = new Cal_15A_DAL();
            mCal_15A_DAL.Insert_15A_Calculation(BoilerID, ProjectID, PID, SectionID, Value);
        }

    }
}
