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
    public class ProjectInfoBLL
    {
        public void InsertProjectInfoDetails(ProjectInfoSC vProjectInfoSC)
        {

            ProjectInfoDAL mProjectInfoDAL = new ProjectInfoDAL();
            DataSet mDts = null;
            mDts = new DataSet();

            mDts = mProjectInfoDAL.InsertProjectInfoDetails(vProjectInfoSC);

            vProjectInfoSC.ProjectID = Convert.ToString(mDts.Tables[0].Rows[0]["ProjectID"]);
        }

        public ProjectInfoSC GetProjectDetails(string vPID)
        {
            ProjectInfoSC mProjectInfoSC = new ProjectInfoSC();
            ProjectInfoDAL mProjectInfoDAL = new ProjectInfoDAL();
            mProjectInfoSC = mProjectInfoDAL.GetProjectInformationByProjectID(vPID);
            return mProjectInfoSC;
        }
    }
}
