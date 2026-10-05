using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
//using Microsoft.Practices.EnterpriseLibrary.Data;
using BoilerModellingTool.SC;
using BoilerModellingTool.BLL;
using BoilerModellingTool.DAL;
using System;


namespace GenericClasses
{
    public class S_parameter_generic
    {
       
        public void S_parameter_values(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID)
        {
            S_parameter_BLL mS_parameter_BLL = new S_parameter_BLL();
            mS_parameter_BLL.S_Parameter(ProjectID, BoilerID, BoilerLoad, ObjectiveID);

        }

        S_parameter_BLL mS_parameter_BLL;

        public void Get_S_parameter_values(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID)
        {
            mS_parameter_BLL = new S_parameter_BLL();
            mS_parameter_BLL.S_Parameter(ProjectID, BoilerID, BoilerLoad, ObjectiveID);
        }

        public DataSet Get_S_parameter_Details(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID)
        {
           DataSet mdset = new DataSet();
           mS_parameter_BLL = new S_parameter_BLL();
           mdset= mS_parameter_BLL.Get_S_parameter_Details(ProjectID, BoilerID, BoilerLoad, ObjectiveID);
           return mdset;
        }

        public DataSet Get_Iteration_of_Heating_element(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, string SectionID)
        {
            DataSet mDSet = null;

            S_parameter_DAL mS_parameter_DAL = new S_parameter_DAL();

            mDSet = mS_parameter_DAL.Get_Iteration_of_Heating_element(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SectionID);

            return mDSet;

        }
        
        public void delete_Iteration_of_Heating_element(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, int ITr_ID,string Sec_ID)
        {
            //DataSet mDSet = null;

            S_parameter_DAL mS_parameter_DAL = new S_parameter_DAL();

            mS_parameter_DAL.delete_Iteration_of_Heating_element(ProjectID, BoilerID, BoilerLoad, ObjectiveID, ITr_ID, Sec_ID);

            //return mDSet;

        }  

        public void Insert_Data_For_Next_Iteration(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,int Iteration)
        {
            S_parameter_BLL mS_parameter_BLL = new S_parameter_BLL();
            DataTable dt = new DataTable();
            dt = mS_parameter_BLL.Get_Iteration_Data(ProjectID,BoilerID,BoilerLoad,ObjectiveID,Iteration-1);

            for(int i=0;i<dt.Rows.Count;i++)
            {
                dt.Rows[i]["Iteration"]= Iteration;
            }

            mS_parameter_BLL.Insert_S_Parameter_Next_Data(dt);

        }

        //public int Comparison_S_Paramter_IterationWise(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,int LowestIteration,int SecondLowestIteration,int TotalCount)
        //{
        //    int flag=0;

        //    S_parameter_BLL mS_parameter_BLL = new S_parameter_BLL();

        //    string[,] S_Parameter_Values_Lowest = new string[TotalCount+1,2];
        //    string[,] S_Parameter_Values_SecondLowest = new string[TotalCount+1, 2];

        //    DataTable dt_Lowest = new DataTable();
        //    DataTable dt_SecondLowest = new DataTable();

        //    dt_Lowest = mS_parameter_BLL.Get_IterationWise_Data(ProjectID, BoilerID, BoilerLoad, ObjectiveID, LowestIteration);

        //    dt_SecondLowest = mS_parameter_BLL.Get_IterationWise_Data(ProjectID, BoilerID, BoilerLoad, ObjectiveID, SecondLowestIteration);

        //    int i = 0;

        //        for(int j=0;j<dt_SecondLowest.Rows.Count;j++)
        //        {
        //            if(dt_Lowest.Rows[i]["SectionID"].ToString()==dt_SecondLowest.Rows[j]["SectionID"].ToString())
        //            {
        //                double d1 = 0, d2 = 0, Result = 0;
        //                d1=Convert.ToDouble(dt_Lowest.Rows[j]["Output"]);
        //                d2=Convert.ToDouble(dt_SecondLowest.Rows[i]["Output"]);
        //                Result = d2 - d1;
        //                i++;
        //                if(Math.Round(Result)<=0.1)
        //                {
        //                    flag = 0;
        //                }
        //                else
        //                {
        //                    flag = 1;
        //                    mS_parameter_BLL.Delete_Iteration_Data(ProjectID,BoilerID,BoilerLoad,ObjectiveID,LowestIteration);
        //                    break;
        //                }

        //            }
        //        }

        //    return flag;
        //}
        //BB
        private double SafeToDouble(object value, string name)
        {
            double result = 0;
            string str = Convert.ToString(value);

            System.Diagnostics.Debug.WriteLine("RAW [" + name + "] = " + str);

            if (!double.TryParse(str, out result))
            {
                System.Diagnostics.Debug.WriteLine("❌ INVALID " + name + " → default 0");
                result = 0;
            }

            return result;
        }
        public int Comparison_S_Paramter_IterationWise(
    string ProjectID, string BoilerID, string BoilerLoad,
    int ObjectiveID, int LowestIteration, int SecondLowestIteration, int TotalCount)
        {
            int flag = 0;

            S_parameter_BLL mS_parameter_BLL = new S_parameter_BLL();

            DataTable dt_Lowest = mS_parameter_BLL.Get_IterationWise_Data(
                ProjectID, BoilerID, BoilerLoad, ObjectiveID, LowestIteration);

            DataTable dt_SecondLowest = mS_parameter_BLL.Get_IterationWise_Data(
                ProjectID, BoilerID, BoilerLoad, ObjectiveID, SecondLowestIteration);

            // ✅ DEBUG START
            System.Diagnostics.Debug.WriteLine("===== S_PARAMETER DEBUG =====");
            System.Diagnostics.Debug.WriteLine("Lowest Rows: " + (dt_Lowest?.Rows.Count ?? 0));
            System.Diagnostics.Debug.WriteLine("SecondLowest Rows: " + (dt_SecondLowest?.Rows.Count ?? 0));

            // ✅ SAFETY CHECK
            if (dt_Lowest == null || dt_SecondLowest == null ||
                dt_Lowest.Rows.Count == 0 || dt_SecondLowest.Rows.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("❌ EMPTY DATASET → RETURN 0");
                return 0;
            }

            // ✅ LOOP SAFELY
            for (int i = 0; i < dt_Lowest.Rows.Count; i++)
            {
                for (int j = 0; j < dt_SecondLowest.Rows.Count; j++)
                {
                    string sec1 = Convert.ToString(dt_Lowest.Rows[i]["SectionID"]);
                    string sec2 = Convert.ToString(dt_SecondLowest.Rows[j]["SectionID"]);

                    System.Diagnostics.Debug.WriteLine($"Comparing Section: {sec1} vs {sec2}");

                    if (sec1 == sec2)
                    {
                        double d1 = SafeToDouble(dt_Lowest.Rows[i]["Output"], "Lowest_Output");
                        double d2 = SafeToDouble(dt_SecondLowest.Rows[j]["Output"], "SecondLowest_Output");

                        double Result = d2 - d1;

                        System.Diagnostics.Debug.WriteLine($"d1={d1}, d2={d2}, Result={Result}");

                        if (Math.Abs(Result) <= 0.1)
                        {
                            flag = 0;
                        }
                        else
                        {
                            flag = 1;
                            System.Diagnostics.Debug.WriteLine("⚠️ Difference > 0.1 → Deleting Iteration");

                            mS_parameter_BLL.Delete_Iteration_Data(
                                ProjectID, BoilerID, BoilerLoad, ObjectiveID, LowestIteration);

                            return flag;
                        }
                    }
                }
            }

            return flag;
        }


        public void Insert_S_Parameter_ForFirstTime(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID,int Iteration)
        {
            double SH, RH, EC;
            Stream_Macros mSteam_macro = new Stream_Macros();
            DataSet ds = new DataSet();
            S_parameter_BLL mS_parameter_BLL = new S_parameter_BLL();
            ds = mS_parameter_BLL.GetInitialsValueOf_S_Parameter(ProjectID, BoilerID, BoilerLoad, ObjectiveID, Iteration);

            EC = Convert.ToDouble(ds.Tables[0].Rows[0]["Value"].ToString());
            RH = Convert.ToDouble(ds.Tables[1].Rows[0]["Value"].ToString());
            SH = Convert.ToDouble(ds.Tables[2].Rows[0]["Value"].ToString());

            SH = mSteam_macro.Tsat_p(SH*0.980665 + 1.01325);
            //SH = 505.8;

            mS_parameter_BLL.Insert_S_Parameter_FirstIterationData(ProjectID, BoilerID, BoilerLoad, ObjectiveID, Iteration,SH,RH,EC);
            
        }

    }

}
