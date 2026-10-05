using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerModellingTool.SC;
using BoilerModellingTool.DAL;

namespace BoilerModellingTool.BLL
{
    public class BoilerInformationBLL
    {
        public void InsertUpdateBoilerInformation(BoilerInformationSC vBoilerInformationSC)
        {
            BoilerInformationDAL mBoilerInformationDAL = new BoilerInformationDAL();
            mBoilerInformationDAL.InsertUpdateBoilerInformation(vBoilerInformationSC);
        }   //Insert

        public BoilerInformationSC getBoilerInformation(string ProjectID, string BoilerID)
        {
            BoilerInformationSC mBoilerInformationSC = new BoilerInformationSC();
            BoilerInformationDAL mBoilerInformationDAL = new BoilerInformationDAL();
            mBoilerInformationSC=mBoilerInformationDAL.GetBoilerInformationByProjectID(ProjectID, BoilerID);
            return mBoilerInformationSC;
        }   //Get Boiler Information

        public BoilerInformationSC GetUpperFurnaceInformation(string ProjectID,string BoilerID)
        {
            BoilerInformationSC mBoilerInformationSC = new BoilerInformationSC();
            BoilerInformationDAL mBoilerInformationDAL = new BoilerInformationDAL();
            mBoilerInformationSC = mBoilerInformationDAL.GetUpperFurnaceInformation(ProjectID, BoilerID);
            return mBoilerInformationSC;
        }   //Upper Furnace

        public BoilerInformationSC GetCrossDuctInformation(string ProjectID,string BoilerID)
        {
            BoilerInformationSC mBoilerInformationSC = new BoilerInformationSC();
            BoilerInformationDAL mBoilerInformationDAL = new BoilerInformationDAL();
            mBoilerInformationSC = mBoilerInformationDAL.GetCrossDuctInformation(ProjectID, BoilerID);
            return mBoilerInformationSC;
        }       //Cross Duct

        public BoilerInformationSC GetReverseChamberInformation(string ProjectID,string BoilerID)
        {
            BoilerInformationSC mBoilerInformationSC = new BoilerInformationSC();
            BoilerInformationDAL mBoilerInformationDAL = new BoilerInformationDAL();
            mBoilerInformationSC = mBoilerInformationDAL.GetReverseChamberInformation(ProjectID, BoilerID);
            return mBoilerInformationSC;
        }   //Reverse Chamber

        public BoilerInformationSC GetBackpassInformation(string ProjectID, string BoilerID)
        {
            BoilerInformationSC mBoilerInformationSC = new BoilerInformationSC();
            BoilerInformationDAL mBoilerInformationDAL = new BoilerInformationDAL();
            mBoilerInformationSC = mBoilerInformationDAL.GetBackpassInformation(ProjectID, BoilerID);
            return mBoilerInformationSC;
        }   //Backpass

        public BoilerInformationSC GetBackpassSHInformation(string ProjectID, string BoilerID)
        {
            BoilerInformationSC mBoilerInformationSC = new BoilerInformationSC();
            BoilerInformationDAL mBoilerInformationDAL = new BoilerInformationDAL();
            mBoilerInformationSC = mBoilerInformationDAL.GetBackpassSHInformation(ProjectID, BoilerID);
            return mBoilerInformationSC;
        }   //Backpass SH

        public BoilerInformationSC GetBackpassRHInformation(string ProjectID, string BoilerID)
        {
            BoilerInformationSC mBoilerInformationSC = new BoilerInformationSC();
            BoilerInformationDAL mBoilerInformationDAL = new BoilerInformationDAL();
            mBoilerInformationSC = mBoilerInformationDAL.GetBackpassRHInformation(ProjectID, BoilerID);
            return mBoilerInformationSC;
        }   //Backpass RH

        public BoilerInformationSC GetMiscellaneousInformation(string ProjectID, string BoilerID)
        {
            BoilerInformationSC mBoilerInformationSC = new BoilerInformationSC();
            BoilerInformationDAL mBoilerInformationDAL = new BoilerInformationDAL();
            mBoilerInformationSC = mBoilerInformationDAL.GetMiscellaneousInformation(ProjectID, BoilerID);
            return mBoilerInformationSC;
        }       //Miscellaneous

        public BoilerInformationSC GetMiscellaneousSHInfo(string ProjectID, string BoilerID)
        {
            BoilerInformationSC mBoilerInformationSC = new BoilerInformationSC();
            BoilerInformationDAL mBoilerInformationDAL = new BoilerInformationDAL();
            mBoilerInformationSC = mBoilerInformationDAL.GetMiscellaneousSHInfo(ProjectID, BoilerID);
            return mBoilerInformationSC;
        }

        public BoilerInformationSC GetMiscellaneousRHInfo(string ProjectID, string BoilerID)
        {
            BoilerInformationSC mBoilerInformationSC = new BoilerInformationSC();
            BoilerInformationDAL mBoilerInformationDAL = new BoilerInformationDAL();
            mBoilerInformationSC = mBoilerInformationDAL.GetMiscellaneousRHInfo(ProjectID, BoilerID);
            return mBoilerInformationSC;
        }

    }
}
