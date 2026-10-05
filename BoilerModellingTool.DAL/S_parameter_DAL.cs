using System;
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
    public class S_parameter_DAL
    {
        #region " Variables "

        private Database currentDatabase;

        #endregion

        #region "Constuctor"

        public S_parameter_DAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }

        #endregion

        public void S_Parameter_data(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID)
        {
            String mStoredProcName = String.Empty;
            String mStoredProcName1 = String.Empty;
            String mStoredProcNameCount = String.Empty;
            DbCommand mDbCommand = null;
            DbCommand mDbCommand1 = null;
            DbCommand mDbCommandcount = null;

            DataSet mDSet = null;
            DataSet mDSet1 = null;
            DataSet mDsetCount = null;
            DataTable mDTable = null;
            string S_Value;

            try
            {
                mDTable = new DataTable();
                //Get data from database
                mStoredProcName = StoredProcedure.spr_Get_S_parameter;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
                for (int i = 0; i < mDSet.Tables[0].Rows.Count; i++)
                {
                    string SectionId =  mDSet.Tables[0].Rows[i][0].ToString();
                    string SectionType = mDSet.Tables[0].Rows[i][1].ToString();
                    string SectionNumber = mDSet.Tables[0].Rows[i][2].ToString();


                    //Getting values from 5B and PI

                    mStoredProcName = StoredProcedure.spr_Get_data_for_S_parameter;
                    mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                    currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                    currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                    //currentDatabase.AddInParameter(mDbCommand, "@vSectionID", DbType.String, SectionId);
                    currentDatabase.AddInParameter(mDbCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                    currentDatabase.AddInParameter(mDbCommand, "@vObjectiveID", DbType.Int32, ObjectiveID);
                    mDSet1 = currentDatabase.ExecuteDataSet(mDbCommand);




                    //For S parameter table count
                    mStoredProcNameCount = StoredProcedure.spr_Get_S_parameter_count;
                    mDbCommandcount = currentDatabase.GetStoredProcCommand(mStoredProcNameCount);

                 
                    currentDatabase.AddInParameter(mDbCommandcount, "@vProjectID", DbType.String, ProjectID);
                    currentDatabase.AddInParameter(mDbCommandcount, "@vBoilerID", DbType.String, BoilerID);
                    currentDatabase.AddInParameter(mDbCommandcount, "@vSectionID", DbType.String, SectionId);
                    currentDatabase.AddInParameter(mDbCommandcount, "@vSectionNumber", DbType.Int32,SectionNumber);
                    currentDatabase.AddInParameter(mDbCommandcount, "@vSectionType", DbType.String, SectionType);
                    currentDatabase.AddInParameter(mDbCommandcount, "@vIteration", DbType.Int32, 0);
                    currentDatabase.AddInParameter(mDbCommandcount, "@vBoilerLoad", DbType.String, BoilerLoad);
                    currentDatabase.AddInParameter(mDbCommandcount, "@vObjectiveID", DbType.Int32, ObjectiveID);
                    mDsetCount = currentDatabase.ExecuteDataSet(mDbCommandcount);

                    string count = mDsetCount.Tables[0].Rows[0][0].ToString();


                    if (count == "0")
                    {

                        //Insert into database for S parameter
                        
                        mStoredProcName1 = StoredProcedure.spr_Insert_S_parameter;
                        mDbCommand1 = currentDatabase.GetStoredProcCommand(mStoredProcName1);

                        currentDatabase.AddInParameter(mDbCommand1, "@vProjectID", DbType.String, ProjectID);
                        currentDatabase.AddInParameter(mDbCommand1, "@vBoilerID", DbType.String, BoilerID);
                        currentDatabase.AddInParameter(mDbCommand1, "@vSectionID", DbType.String, SectionId);

                        currentDatabase.AddInParameter(mDbCommand1, "@vSectionNumber", DbType.Int32, SectionNumber);
                        currentDatabase.AddInParameter(mDbCommand1, "@vSectionType", DbType.String, SectionType);
                        currentDatabase.AddInParameter(mDbCommand1, "@vIteration", DbType.Int32, 0);


                        if (SectionType == "Economizer")
                        {

                            S_Value = mDSet1.Tables[0].Rows[0][0].ToString();

                            if (S_Value == "")
                            {
                                currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, "");
                                currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, "");

                            }
                            else
                            {
                                currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, S_Value.ToString());
                                currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, S_Value.ToString());
                            }


                        }


                        else if (SectionType == "Superheater Elements" || SectionType == "Steam Screen Sections" || SectionType == "Water Screen Sections" || SectionType == "Reverse Chamber")
                        {
                            S_Value = mDSet1.Tables[1].Rows[0][0].ToString();

                            if (S_Value == "")
                            {
                                currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, "");
                                currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, "");


                            }
                            else
                            {
                                currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, S_Value.ToString());
                                currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, S_Value.ToString());

                            }


                        }

                        else if (SectionType == "Reheater Elements")
                        {
                            S_Value = mDSet1.Tables[2].Rows[0][0].ToString();

                            if (S_Value == "")
                            {

                                currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, "");
                                currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, "");
                            }
                            else
                            {
                                currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, S_Value.ToString());
                                currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, S_Value.ToString());

                            }

                        }



                        currentDatabase.AddInParameter(mDbCommand1, "@vBoilerLoad", DbType.String, BoilerLoad);
                        currentDatabase.AddInParameter(mDbCommand1, "@vObjectiveID", DbType.Int32, ObjectiveID);

                        currentDatabase.ExecuteNonQuery(mDbCommand1);


                    }
                    else
                    {

                        //Insert into database for S parameter
                       
                        mStoredProcName1 = StoredProcedure.spr_Update_S_parameter;
                        mDbCommand1 = currentDatabase.GetStoredProcCommand(mStoredProcName1);

                        currentDatabase.AddInParameter(mDbCommand1, "@vProjectID", DbType.String, ProjectID);
                        currentDatabase.AddInParameter(mDbCommand1, "@vBoilerID", DbType.String, BoilerID);
                        currentDatabase.AddInParameter(mDbCommand1, "@vSectionID", DbType.String, SectionId);

                        currentDatabase.AddInParameter(mDbCommand1, "@vSectionNumber", DbType.Int32, SectionNumber);
                        currentDatabase.AddInParameter(mDbCommand1, "@vSectionType", DbType.String, SectionType);
                        currentDatabase.AddInParameter(mDbCommand1, "@vIteration", DbType.Int32, 0);


                        if (SectionType == "Economizer")
                        {

                            S_Value = mDSet1.Tables[0].Rows[0][0].ToString();

                            if (S_Value == "")
                            {
                                currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, "");
                                currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, "");

                            }
                            else
                            {
                                currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, S_Value.ToString());
                                currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, S_Value.ToString());
                            }


                        }


                        else if (SectionType == "Superheater Elements" || SectionType == "Steam Screen Sections" || SectionType == "Reverse Chamber")
                        {
                            S_Value = mDSet1.Tables[1].Rows[0][0].ToString();

                            if (S_Value == "")
                            {
                                currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, "");
                                currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, "");


                            }
                            else
                            {
                                currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, S_Value.ToString());
                                currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, S_Value.ToString());

                            }


                        }

                        else if (SectionType == "Reheater Elements")
                        {
                            S_Value = mDSet1.Tables[2].Rows[0][0].ToString();

                            if (S_Value == "")
                            {

                                currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, "");
                                currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, "");
                            }
                            else
                            {
                                currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, S_Value.ToString());
                                currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, S_Value.ToString());

                            }

                        }





                        currentDatabase.AddInParameter(mDbCommand1, "@vBoilerLoad", DbType.String, BoilerLoad);
                        currentDatabase.AddInParameter(mDbCommand1, "@vObjectiveID", DbType.Int32, ObjectiveID);

                        currentDatabase.ExecuteNonQuery(mDbCommand1);


                    }







                }
            }



            catch(Exception e)
            {
                e.Message.ToString();

            }

          
        }

        public DataSet Get_S_parameter_Details(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID)
        {
            String mStoredProcName = String.Empty;

           
            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            //try
            //{
                mDTable = new DataTable();

                mStoredProcName = StoredProcedure.spr_get_S_parameter_Details_Iteration;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.String, ObjectiveID);
             
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            //}
            //catch
            //{

            //}
            return mDSet;

        }

        public DataSet Get_Iteration_of_Heating_element(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID,string SectionID)
        {
            String mStoredProcName = String.Empty;


            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            //try
            //{
            mDTable = new DataTable();

            mStoredProcName = StoredProcedure.spr_get_Iteration_of_Heating_element;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.String, ObjectiveID);
            currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, SectionID);


            

            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            //}
            //catch
            //{

            //}
            return mDSet;

        }

        public void delete_Iteration_of_Heating_element(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, int Itr_ID,string Sec_ID)
        {
            String mStoredProcName = String.Empty;


            DbCommand mDBCommand = null;
            DataSet mDSet = null;
            DataTable mDTable = null;

            //try
            //{
            mDTable = new DataTable();

            mStoredProcName = StoredProcedure.spr_delete_Iteration_ID;
            mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

            currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
            currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
            currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
            currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.String, ObjectiveID);
            currentDatabase.AddInParameter(mDBCommand, "@vIterationID", DbType.String, Itr_ID);
            currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, Sec_ID);

            mDSet = currentDatabase.ExecuteDataSet(mDBCommand);

            //}
            //catch
            //{

            //}
           // return mDSet;

        }

        public DataTable Get_Iteration_Data(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,int Iteration)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand = null;
            DataTable dt = null;
            DataSet mDSet = null;

            try
            {
                mDSet = new DataSet();
                dt = new DataTable();

                mStoredProcName = StoredProcedure.spr_Get_Iteration_Data;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDBCommand, "@vIteration", DbType.Int16, Iteration);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                dt = mDSet.Tables[0];
            }
           
            catch
            {

            }
            return dt;
        }

        public void Insert_S_Parameter_Next_Data(DataTable dt)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand = null;
            //DataTable dt = null;
            DataSet mDSet = null;

            try
            {
                mDSet = new DataSet();
                //dt = new DataTable();

                mStoredProcName = StoredProcedure.spr_Insert_S_Parameter_Values;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, dt.Rows[i]["BoilerID"]);
                    currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, dt.Rows[i]["ProjectID"]);
                    currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, dt.Rows[i]["BoilerLoad"]);
                    currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, dt.Rows[i]["ObjectiveID"]);
                    currentDatabase.AddInParameter(mDBCommand, "@vIteration", DbType.Int16, dt.Rows[i]["Iteration"]);
                    currentDatabase.AddInParameter(mDBCommand, "@vSectionID", DbType.String, dt.Rows[i]["SectionID"]);
                    currentDatabase.AddInParameter(mDBCommand, "@vSectionType", DbType.String, dt.Rows[i]["SectionType"]);
                    currentDatabase.AddInParameter(mDBCommand, "@vSectionNumber", DbType.Int16, dt.Rows[i]["SectionNumber"]);
                    currentDatabase.AddInParameter(mDBCommand, "@vOutput", DbType.String, dt.Rows[i]["Output"]);
                    currentDatabase.AddInParameter(mDBCommand, "@vInput", DbType.String, dt.Rows[i]["Input"]);
                    currentDatabase.ExecuteNonQuery(mDBCommand);
                    mDBCommand.Parameters.Clear();
                }      
            }

            catch
            {

            }
        }

        public DataTable Get_IterationWise_Data(string ProjectID, string BoilerID,string BoilerLoad,int ObjectiveID,int Iteration)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand = null;
            DataTable dt = null;
            DataSet mDSet = null;

            try
            {
                mDSet = new DataSet();
                dt = new DataTable();

                mStoredProcName = StoredProcedure.spr_Get_Iteration_Data;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDBCommand, "@vIteration", DbType.Int16, Iteration);

                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                dt = mDSet.Tables[0];
            }

            catch
            {

            }
            return dt;
        }

        public void Delete_Iteration_Data(string ProjectID,string BoilerID,string BoilerLoad,int ObjectiveID,int Iteration)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand = null;

            try
            {
                mStoredProcName = StoredProcedure.spr_Delete_Iteration_Data;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                currentDatabase.AddInParameter(mDBCommand, "@vIteration", DbType.Int16, Iteration);

                currentDatabase.ExecuteNonQuery(mDBCommand);
            }

            catch
            {

            }
        }

        public void Insert_S_Parameter_FirstData(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, int Iteration,double SH,double RH,double EC)
        {
            String mStoredProcName = String.Empty;
            String mStoredProcName1 = String.Empty;
            String mStoredProcNameCount = String.Empty;
            DbCommand mDbCommand = null;
            DbCommand mDbCommand1 = null;
            DbCommand mDbCommandcount = null;

            DataSet mDSet = null;
            DataSet mDSet1 = null;
            DataSet mDsetCount = null;
            DataTable mDTable = null;
            string S_Value;

            try
            {
                mDTable = new DataTable();
                //Get data from database
                mStoredProcName = StoredProcedure.spr_Get_S_parameter;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                int j = 0;

                for (int i = 0; i < mDSet.Tables[0].Rows.Count; i++)
                {
                    //int j = 0;
                    string SectionId = mDSet.Tables[0].Rows[i][0].ToString();
                    string SectionType = mDSet.Tables[0].Rows[i][1].ToString();
                    string SectionNumber = mDSet.Tables[0].Rows[i][2].ToString();

                    mStoredProcName1 = StoredProcedure.spr_Insert_S_Parameter_InitialValues;
                    mDbCommand1 = currentDatabase.GetStoredProcCommand(mStoredProcName1);

                    currentDatabase.AddInParameter(mDbCommand1, "@vProjectID", DbType.String, ProjectID);
                    currentDatabase.AddInParameter(mDbCommand1, "@vBoilerID", DbType.String, BoilerID);
                    currentDatabase.AddInParameter(mDbCommand1, "@vBoilerLoad", DbType.String, BoilerLoad);
                    currentDatabase.AddInParameter(mDbCommand1, "@vObjectiveID", DbType.Int32, ObjectiveID);
                    currentDatabase.AddInParameter(mDbCommand1, "@vIteration", DbType.Int32, Iteration);
                    currentDatabase.AddInParameter(mDbCommand1, "@vSectionNumber", DbType.Int32, SectionNumber);
                    currentDatabase.AddInParameter(mDbCommand1, "@vSectionID", DbType.String, SectionId);

                    //mDSet1 = currentDatabase.ExecuteDataSet(mDbCommand);                    
                    if (SectionType == "Economizer")
                    {
                        currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, EC);
                        currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, EC);                        
                        currentDatabase.AddInParameter(mDbCommand1, "@vSectionType", DbType.String, SectionType);
                        currentDatabase.ExecuteNonQuery(mDbCommand1);
                    }
                    else if (SectionType == "Superheater Elements" || SectionType == "Steam Screen Sections" || SectionType == "Reverse Chamber" || SectionType == "Water Screen Sections")
                    {
                        currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, SH);
                        currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, SH);                        
                        currentDatabase.AddInParameter(mDbCommand1, "@vSectionType", DbType.String, SectionType);
                        currentDatabase.ExecuteNonQuery(mDbCommand1);
                    }
                    else if (SectionType == "Reheater Elements")
                    {
                        currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, RH);
                        currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, RH);                        
                        currentDatabase.AddInParameter(mDbCommand1, "@vSectionType", DbType.String, SectionType);
                        currentDatabase.ExecuteNonQuery(mDbCommand1);
                    }


                    //if (j == 0)
                    //{
                    //    j++;
                        
                    //}
                    
                }

                mStoredProcName1 = StoredProcedure.spr_Insert_S_Parameter_InitialValues;
                mDbCommand1 = currentDatabase.GetStoredProcCommand(mStoredProcName1);

                currentDatabase.AddInParameter(mDbCommand1, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand1, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDbCommand1, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDbCommand1, "@vObjectiveID", DbType.Int32, ObjectiveID);
                currentDatabase.AddInParameter(mDbCommand1, "@vIteration", DbType.Int32, Iteration);
                currentDatabase.AddInParameter(mDbCommand1, "@vSectionNumber", DbType.Int32, 1);
                currentDatabase.AddInParameter(mDbCommand1, "@vSectionID", DbType.String,"1");

                currentDatabase.AddInParameter(mDbCommand1, "@vInput", DbType.String, SH);
                currentDatabase.AddInParameter(mDbCommand1, "@vOutput", DbType.String, SH);
                currentDatabase.AddInParameter(mDbCommand1, "@vSectionType", DbType.String, "Reverse Chamber");
                currentDatabase.ExecuteNonQuery(mDbCommand1);

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }

        }

        public DataSet GetInitialsValueOf_S_Parameter(string ProjectID, string BoilerID, string BoilerLoad, int ObjectiveID, int Iteration)
        {
            String mStoredProcName = String.Empty;
            DbCommand mDBCommand = null;
            DataTable dt = null;
            DataSet mDSet = null;            

            try
            {
                mDSet = new DataSet();
                dt = new DataTable();

                mStoredProcName = StoredProcedure.spr_Get_S_Parameter_InitialValue;
                mDBCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);

                currentDatabase.AddInParameter(mDBCommand, "@vBoilerID", DbType.String, BoilerID);
                currentDatabase.AddInParameter(mDBCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDBCommand, "@vBoilerLoad", DbType.String, BoilerLoad);
                currentDatabase.AddInParameter(mDBCommand, "@vObjectiveID", DbType.Int16, ObjectiveID);
                
                mDSet = currentDatabase.ExecuteDataSet(mDBCommand);
                
            }      
            

            catch
            {

            }
            return mDSet;
        }
       

    }
}
