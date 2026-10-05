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
    public class Cal_5A_BLL
    {
        public DataSet GetInput_For_5A_Calculation(string BolierID, string ProjectID,string BoilerLoad)
        {
            Cal_5A_DAL mCal_5A_DAL = null;
            DataSet mDTable = null;

            mCal_5A_DAL = new Cal_5A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_5A_DAL.GetInput_For_5A_Calculation(BolierID, ProjectID,BoilerLoad);
            return mDTable;

        }
        public DataTable Get_PID_For_5A_Calculation()
        {
            Cal_5A_DAL mCal_5A_DAL = null;
            DataTable mDTable = null;

            mCal_5A_DAL = new Cal_5A_DAL();
            mDTable = new DataTable();

            mDTable = mCal_5A_DAL.Get_PID_For_5A_Calculation();
            return mDTable;

        }
        public void Insert_5A_Calculation(int pid, string BoilerID, string ProjectID,string BoilerLoad,string SectionID, int ObjectiveID, string Value)
        {

            Cal_5A_DAL mCal_5A_DAL = new Cal_5A_DAL();
            DataSet mDts = null;
            mDts = new DataSet();

            mDts = mCal_5A_DAL.Insert_5A_Calculation(pid, BoilerID, ProjectID, BoilerLoad, SectionID, ObjectiveID, Value);
        }
    }
}
