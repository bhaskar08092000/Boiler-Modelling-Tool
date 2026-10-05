using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using BoilerModellingTool.SC;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace BoilerModellingTool.DAL
{
   public class ProjectInfoDAL
    {

        #region " Variables "

        private Database currentDatabase;

        #endregion

        #region " Constructor "

        public ProjectInfoDAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion


        public DataSet InsertProjectInfoDetails(ProjectInfoSC vProjectInfoSC)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_ProjectInfo;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);

                currentDatabase.AddInParameter(mDbCommand, "@vProjectName", DbType.String, vProjectInfoSC.ProjectName);
                currentDatabase.AddInParameter(mDbCommand, "@vJobNumber", DbType.String, vProjectInfoSC.JobNumber);
                currentDatabase.AddInParameter(mDbCommand, "@vClient", DbType.String, vProjectInfoSC.Client);
                currentDatabase.AddInParameter(mDbCommand, "@vPlantType", DbType.String, vProjectInfoSC.PlantType);
                //currentDatabase.AddInParameter(mDbCommand, "@vDate", DbType.DateTime, vProjectInfoSC.Date);
                currentDatabase.AddInParameter(mDbCommand, "@vNumberOfBoiler", DbType.Int16, vProjectInfoSC.TotalNumberOfBoiler);
                currentDatabase.AddInParameter(mDbCommand, "@vTotalNumberOfBoiler", DbType.Int16, vProjectInfoSC.TotalNumberOfBoiler);
                currentDatabase.AddInParameter(mDbCommand, "@vIsEdit", DbType.String, vProjectInfoSC.IsEdit);
              
                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
                return mDSet;
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }

            return mDSet;
        }

        public ProjectInfoSC GetProjectInformationByProjectID(String vPID)
        {
            DataSet mDSet = null;
            ProjectInfoSC mProjectInfoSC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommnad = null;
            mDSet = new DataSet();

            try
            {
                mStoredProcName = StoredProcedure.spr_GetProjectInformation;
                mDbCommnad = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommnad, "@vProjectID", DbType.String, vPID);
                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommnad);

                mProjectInfoSC = new ProjectInfoSC();
                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mProjectInfoSC.ProjectID = mDSet.Tables[0].Rows[0]["ProjectID"].ToString();
                    mProjectInfoSC.TotalNumberOfBoiler = mDSet.Tables[0].Rows[0]["TotalNumberOfBoiler"].ToString();
                     
                    //for(int i=0;i<Convert.ToInt16(mProjectInfoSC.TotalNumberOfBoiler);i++)
                    //{
                    //    mProjectInfoSC.BoilerIDValues[i] = mDSet.Tables[0].Rows[i]["BoilerID"].ToString();
                    //}

                    mProjectInfoSC.BoilerValues=new string[Convert.ToInt16(mProjectInfoSC.TotalNumberOfBoiler)];

                    for(int i=0;i<Convert.ToInt16(mProjectInfoSC.TotalNumberOfBoiler);i++)
                    {
                        mProjectInfoSC.BoilerValues[i] = mDSet.Tables[0].Rows[i]["NameOfBoiler"].ToString(); 
                    }

                    mProjectInfoSC.ProjectName = mDSet.Tables[0].Rows[0]["ProjectName"].ToString();
                    mProjectInfoSC.JobNumber = mDSet.Tables[0].Rows[0]["JobNumber"].ToString(); 
                    mProjectInfoSC.Client = mDSet.Tables[0].Rows[0]["Client"].ToString();
                    mProjectInfoSC.Date = mDSet.Tables[0].Rows[0]["Date"].ToString();
                    //mProjectInfoSC.PlantType = mDSet.Tables[0].Rows[0]["ProjectID"].ToString(); 
                }
                return mProjectInfoSC;
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
   
}
