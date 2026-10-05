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
   public class Cal_3A_BLL
    {
       public DataSet GetInput_For_3A_Calculation(string BolierID, string ProjectID,string BoilerLoad,int Objective)
        {
            Cal_3A_DAL mCal_3A_DAL = null;
            DataSet mDTable = null;

            mCal_3A_DAL = new Cal_3A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_3A_DAL.GetInput_For_3A_Calculation(BolierID, ProjectID, BoilerLoad, Objective);
            return mDTable;

        }
       public void Insert_3A_Calculation(string BoilerID, string ProjectID, string Value, int PID, string BoilerLoad,int objectiveID)
        {
            Cal_3A_DAL mCal_3A_DAL = null;
            mCal_3A_DAL = new Cal_3A_DAL();
            mCal_3A_DAL.Insert_3A_Calculation(BoilerID, ProjectID, Value, PID, BoilerLoad, objectiveID);
        }
       public DataSet GetInput_For_RHFlow(string ProjectID,string BoilerID,int ObjectiveID, string BoilerLoad)
        {
           Cal_3A_DAL mCal_3A_DAL = null;
            DataSet mDTable = null;

            mCal_3A_DAL = new Cal_3A_DAL();
            mDTable = new DataSet();

            mDTable=mCal_3A_DAL.GetInputForRHFlow(ProjectID,BoilerID,ObjectiveID,BoilerLoad);

           return mDTable;
        }
       public DataSet EA_getUnburntCarbonLoss(string ProjectID, string BoilerID)
       {
           Cal_3A_DAL mCal_3A_DAL = null;
           DataSet mDTable = null;

           mCal_3A_DAL = new Cal_3A_DAL();
           mDTable = new DataSet();

           mDTable = mCal_3A_DAL.EA_getUnburntCarbonLoss(ProjectID, BoilerID);
           return mDTable;

       }
       public DataSet RHFlow_getHPHeatersCount(string ProjectID, string BoilerID, int ObjectiveID, string BoilerLoad)
       {
           Cal_3A_DAL mCal_3A_DAL = null;
           DataSet mDTable = null;

           mCal_3A_DAL = new Cal_3A_DAL();
           mDTable = new DataSet();

           mDTable = mCal_3A_DAL.RHFlow_getHPHeatersCount(ProjectID, BoilerID, ObjectiveID,  BoilerLoad);
           return mDTable;

       }
       public DataSet BindHPHeaters(string ProjectID, string BoilerID, int ObjectiveID, string BoilerLoad)
       {
           Cal_3A_DAL mCal_3A_DAL = null;
           DataSet mDTable = null;

           mCal_3A_DAL = new Cal_3A_DAL();
           mDTable = new DataSet();

           mDTable = mCal_3A_DAL.BindHPHeaters(ProjectID, BoilerID, ObjectiveID, BoilerLoad);
           return mDTable;

       }
       public DataSet RHFlow_getHPHeatersParameters(string ProjectID, string BoilerID, string SectionID)
       {
           Cal_3A_DAL mCal_3A_DAL = null;
           DataSet mDTable = null;

           mCal_3A_DAL = new Cal_3A_DAL();
           mDTable = new DataSet();

           mDTable = mCal_3A_DAL.RHFlow_getHPHeatersParameters(ProjectID, BoilerID, SectionID);
           return mDTable;

       }
    }
}
