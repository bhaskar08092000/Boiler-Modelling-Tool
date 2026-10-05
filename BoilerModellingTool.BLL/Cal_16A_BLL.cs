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
    public class Cal_16A_BLL
    {
        public DataSet GetInput_For_16A_Calculation(string BoilerID, string ProjectID, string BoilerLoad,int ObjectiveID)
        {
            Cal_16A_DAL mCal_16A_DAL = null;
            DataSet mDTable = null;

            mCal_16A_DAL = new Cal_16A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_16A_DAL.GetInput_For_16A_Calculation(BoilerID, ProjectID, BoilerLoad,ObjectiveID);
            return mDTable;

        }
        public void Insert_16A_Calculation(string BoilerID, string ProjectID, string BoilerLoad, int ObjectiveID, string Value, int pid)
        {
            Cal_16A_DAL mCal_16A_DAL = null;
            mCal_16A_DAL = new Cal_16A_DAL();
            mCal_16A_DAL.Insert_16A_Calculation(BoilerID, ProjectID, BoilerLoad, ObjectiveID, Value, pid);
        }

        public Double Get_2A_ReqEnthalpy(string BoilerID, string ProjectID, string InputVal)
        {
            Cal_16A_DAL mCal_16A_DAL = null;
            Double mDTable;

            mCal_16A_DAL = new Cal_16A_DAL();
            //mDTable = new DataSet();

            mDTable = mCal_16A_DAL.Get_2A_ReqEnthalpy(BoilerID, ProjectID, InputVal);
            return mDTable;

        }
        public Double Get_2A_ReqEnthalpy1(string BoilerID, string ProjectID, string InputVal)
        {
            Cal_16A_DAL mCal_16A_DAL = null;
            Double mDTable;

            mCal_16A_DAL = new Cal_16A_DAL();
            //mDTable = new DataSet();

            mDTable = mCal_16A_DAL.Get_2A_ReqEnthalpy1(BoilerID, ProjectID, InputVal);
            return mDTable;


        }

        public Double Get_2A_ReqEnthalpy2(string BoilerID, string ProjectID, string InputVal)
        {
            Cal_16A_DAL mCal_16A_DAL = null;
            Double mDTable;

            mCal_16A_DAL = new Cal_16A_DAL();
            //mDTable = new DataSet();

            mDTable = mCal_16A_DAL.Get_2A_ReqEnthalpy2(BoilerID, ProjectID, InputVal);
            return mDTable;


        }


    }
}