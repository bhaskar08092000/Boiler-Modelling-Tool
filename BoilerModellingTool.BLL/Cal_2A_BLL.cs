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
    public class Cal_2A_BLL
    {
        public Cal_2A_SC GetInput_For_2A_Calculation(string BoilerID, string ProjectID,string BoilerLoad,int Objective)
        {
            Cal_2A_SC mCal_2A_SC = new Cal_2A_SC();
            Cal_2A_DAL mCal_2A_DAL = new Cal_2A_DAL();

            mCal_2A_SC = new Cal_2A_SC();
            mCal_2A_DAL = new Cal_2A_DAL();

            mCal_2A_SC = mCal_2A_DAL.GetInput_For_2A_Calculation(BoilerID, ProjectID, BoilerLoad, Objective);

            return mCal_2A_SC;

        }
        public DataSet GetCCO2FromEnthalpyTable()
        {
            Cal_2A_DAL mCal_2A_DAL = null;
            DataSet mDTable = null;

            mCal_2A_DAL = new Cal_2A_DAL();
            mDTable = new DataSet();
            mDTable = mCal_2A_DAL.GetCCO2FromEnthalpyTable();
            return mDTable;
        }
        public DataSet Insert_EnthalpyTableForTempZeroCalculatedValue(Cal_2A_SC mCal_2A_SC, string BoilerID, string ProjectID, string BoilerLoad,int objective)
        {
            Cal_2A_DAL mCal_2A_DAL = null;
            DataSet mDTable = null;

            mCal_2A_DAL = new Cal_2A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_2A_DAL.Insert_EnthalpyTableForTempZeroCalculatedValue(mCal_2A_SC, BoilerID, ProjectID, BoilerLoad, objective);
            return mDTable;
        }
        public void Insert_EnthalpyTableForTemp25CalculatedValue(Cal_2A_SC mCal_2A_SC, string BoilerID, string ProjectID, string BoilerLoad,int objectiveID)
        {
            Cal_2A_DAL mCal_2A_DAL = null;
            DataSet mDTable = null;

            mCal_2A_DAL = new Cal_2A_DAL();
            mDTable = new DataSet();

            mCal_2A_DAL.Insert_EnthalpyTableForTemp25CalculatedValue(mCal_2A_SC, BoilerID, ProjectID, BoilerLoad, objectiveID);
            //return mDTable;
        }
        public DataSet Get_Nearest_Max_Min_EnthalpyCalculation(string BoilerID, string ProjectID, Double Input, string BoilerLoad,int objectiveID)
        {
             Cal_2A_DAL mCal_2A_DAL = null;
            DataSet mDTable = null;

            mCal_2A_DAL = new Cal_2A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_2A_DAL.Get_Nearest_Max_Min_EnthalpyCalculation(BoilerID, ProjectID, Input, BoilerLoad, objectiveID);
            return mDTable;
        }
        public DataSet GetInput_For_25_Enthalpy_Calculation(string BoilerID, string ProjectID, string BoilerLoad,int objectiveID)
        {
            Cal_2A_DAL mCal_2A_DAL = null;
            DataSet mDTable = null;

            mCal_2A_DAL = new Cal_2A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_2A_DAL.GetInput_For_25_Enthalpy_Calculation(BoilerID, ProjectID,BoilerLoad,objectiveID);

            return mDTable;
            
        }
        public DataSet select_APH_Calculation(string BoilerID, string ProjectID, string BoilerLoad,int objectiveID)
        {
            Cal_2A_DAL mCal_2A_DAL = null;
            DataSet mDTable = null;

            mCal_2A_DAL = new Cal_2A_DAL();
            mDTable = new DataSet();

            mDTable = mCal_2A_DAL.select_APH_Calculation(BoilerID, ProjectID, BoilerLoad, objectiveID);
            return mDTable;
        }
        public void Update_CP_Flue_AND_Air_Gas(Cal_2A_SC mCal_2A_SC, string BoilerID, string ProjectID, string BoilerLoad,int objectiveID)
        {
            Cal_2A_DAL mCal_2A_DAL = null;
            DataSet mDTable = null;

            mCal_2A_DAL = new Cal_2A_DAL();
            mDTable = new DataSet();
            mCal_2A_DAL.Update_CP_Flue_AND_Air_Gas(mCal_2A_SC, BoilerID, ProjectID, BoilerLoad, objectiveID);
           
        }
    }
}
