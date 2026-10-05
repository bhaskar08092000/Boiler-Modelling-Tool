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
    public class Cal_1A_BLL
    {

        public DataSet GetFuelParameterForUltimate(string ProjectID, string BoilerID, string BoilerLoad, int objeCtive_ID)
        {
            Cal_1A_DAL mCal_1A_DAL = null;
            DataSet mDTable = null;

            mCal_1A_DAL = new Cal_1A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_1A_DAL.GetFuelParameterForUltimate(ProjectID, BoilerID,BoilerLoad,objeCtive_ID);
            return mDTable;
        }
        public DataSet Get1A_EA_Parameters(string ProjectID, string BoilerID)
        {
            Cal_1A_DAL mCal_1A_DAL = null;
            DataSet mDTable = null;

            mCal_1A_DAL = new Cal_1A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_1A_DAL.Get1A_EA_Parameters(ProjectID, BoilerID);
            return mDTable;
        }
        public DataSet Get_1A_Parameters()
        {
            Cal_1A_DAL mCal_1A_DAL = null;
            DataSet mDTable = null;

            mCal_1A_DAL = new Cal_1A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_1A_DAL.Get_1A_Parameters();
            return mDTable;
        }
        public DataSet Insert1A_Value_with_5_Parameters(int pid, int cgid, string BoilerID, string ProjectID, string BoilerLoadID, int ObjectiveID, double Upperfurnace, double LowerFurnace, double Crossduct, double ReverseChamber, double Backpass)
        {
            Cal_1A_DAL mCal_1A_DAL = null;
            DataSet mDTable = null;

            mCal_1A_DAL = new Cal_1A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_1A_DAL.Insert1A_Value_with_5_Parameters( pid,  cgid,  BoilerID,  ProjectID,  BoilerLoadID,  ObjectiveID,  Upperfurnace,  LowerFurnace,  Crossduct,  ReverseChamber,  Backpass);
            return mDTable;
        }
        public DataSet Insert1A_Value(int pid, int cgid, string BoilerID, string ProjectID, string BoilerLoadID, int ObjectiveID, double Value)
        {
            Cal_1A_DAL mCal_1A_DAL = null;
            DataSet mDTable = null;

            mCal_1A_DAL = new Cal_1A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_1A_DAL.Insert1A_Value( pid,  cgid,  BoilerID,  ProjectID,  BoilerLoadID,  ObjectiveID,  Value);
            return mDTable;
        }
        public DataSet GetExcessAirCheck(string ProjectID, string BoilerID,string BoileLoad,int Objectiveid)
        {
            Cal_1A_DAL mCal_1A_DAL = null;
            DataSet mDTable = null;

            mCal_1A_DAL = new Cal_1A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_1A_DAL.GetExcessAirCheck(ProjectID, BoilerID,BoileLoad,Objectiveid);
            return mDTable;
        }
        public DataSet Get_Objective(string ProjectID, string BoilerID)
        {
            Cal_1A_DAL mCal_1A_DAL = null;
            DataSet mDTable = null;

            mCal_1A_DAL = new Cal_1A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_1A_DAL.Get_Objective(ProjectID, BoilerID);
            return mDTable;
        }
        public DataSet GetAirTempParameterForSpecificHumidity(string ProjectID, string BoilerID, string BoileLoad, int objectiveID)
        {
            Cal_1A_DAL mCal_1A_DAL = null;
            DataSet mDTable = null;

            mCal_1A_DAL = new Cal_1A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_1A_DAL.GetAirTempParameterForSpecificHumidity(ProjectID,BoilerID,BoileLoad,objectiveID);
            return mDTable;
        }

    }
    
}
