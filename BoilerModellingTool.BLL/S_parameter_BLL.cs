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
   public class S_parameter_BLL
    {

       public void S_Parameter(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID)
        {
            S_parameter_DAL mS_parameter_DAL = new S_parameter_DAL();

            mS_parameter_DAL.S_Parameter_data(ProjectID, BoilerID, BoilerLoad, ObjectiveID);
            
        }

       public DataSet Get_S_parameter_Details(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID)
        {
            DataSet mDSet = null;

            S_parameter_DAL mS_parameter_DAL = new S_parameter_DAL();

           mDSet= mS_parameter_DAL.Get_S_parameter_Details(ProjectID, BoilerID, BoilerLoad, ObjectiveID);

            return mDSet;

        }
        
       public DataSet Get_Iteration_of_Heating_element(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID,string SectionID)
        {
            DataSet mDSet = null;

            S_parameter_DAL mS_parameter_DAL = new S_parameter_DAL();

            mDSet = mS_parameter_DAL.Get_Iteration_of_Heating_element(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID);

            return mDSet;

        }
        
       public void delete_Iteration_of_Heating_element(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, int ITr_ID, string SectionID)
        {
            //DataSet mDSet = null;

            S_parameter_DAL mS_parameter_DAL = new S_parameter_DAL();

            mS_parameter_DAL.delete_Iteration_of_Heating_element(ProjectID, BoilerID, BoilerLoad, ObjectiveID, ITr_ID,SectionID);

            //return mDSet;

        }

       public DataTable Get_Iteration_Data(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,int Iteration)
        {
           S_parameter_DAL mS_parameter_DAL = new S_parameter_DAL();
           DataTable dt = new DataTable();
           dt = mS_parameter_DAL.Get_Iteration_Data(ProjectID,BoilerID,BoilerLoad,ObjectiveID,Iteration);
           return dt;
        }

       public void Insert_S_Parameter_Next_Data(DataTable dt)
       {
           S_parameter_DAL mS_parameter_DAL = new S_parameter_DAL();
           mS_parameter_DAL.Insert_S_Parameter_Next_Data(dt);
       }

       public DataTable Get_IterationWise_Data(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, int Iteration)
       {
           S_parameter_DAL mS_parameter_DAL = new S_parameter_DAL();
           DataTable dt = new DataTable();
           dt = mS_parameter_DAL.Get_IterationWise_Data(ProjectID, BoilerID, BoilerLoad, ObjectiveID, Iteration);
           return dt;
       }

       public void Delete_Iteration_Data(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,int Iteration)
       {
           S_parameter_DAL mS_parameter_DAL = new S_parameter_DAL();
           mS_parameter_DAL.Delete_Iteration_Data(ProjectID,BoilerID,BoilerLoad,ObjectiveID,Iteration);
       }

       public void Insert_S_Parameter_FirstIterationData(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, int Iteration,double SH,double RH,double EC)
       {
           S_parameter_DAL mS_parameter_DAL = new S_parameter_DAL();
           mS_parameter_DAL.Insert_S_Parameter_FirstData(ProjectID,BoilerID,BoilerLoad,ObjectiveID,Iteration,SH,RH,EC);
       }

       public DataSet GetInitialsValueOf_S_Parameter(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, int Iteration)
       {
           DataSet mDSet = null;

           S_parameter_DAL mS_parameter_DAL = new S_parameter_DAL();

           mDSet = mS_parameter_DAL.GetInitialsValueOf_S_Parameter(ProjectID, BoilerID, BoilerLoad, ObjectiveID, Iteration);

           return mDSet;
       }

   }
}
