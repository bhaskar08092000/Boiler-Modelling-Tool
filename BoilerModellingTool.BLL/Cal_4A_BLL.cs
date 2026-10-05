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
    public class Cal_4A_BLL
    {
        public DataSet GetInput_For_4A_Calculation(string BolierID, string ProjectID,string BoilerLoad,int ObjectiveID,string SectionID)
        {
            Cal_4A_DAL mCal_4A_DAL = null;
            DataSet mDTable = null;

            mCal_4A_DAL = new Cal_4A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_4A_DAL.GetInput_For_4A_Calculation(BolierID, ProjectID, BoilerLoad, ObjectiveID,SectionID);
            return mDTable;

        }
        public void Insert_4A_Calculation(string BoilerID, string ProjectID, string Value, int PID,string BoilerLoad,int ObjectiveID)
        {
            Cal_4A_DAL mCal_4A_DAL = null;
            mCal_4A_DAL = new Cal_4A_DAL();
            mCal_4A_DAL.Insert_4A_Calculation(BoilerID, ProjectID, Value, PID,BoilerLoad,ObjectiveID);
        }
    }
}
