using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoilerModellingTool.SC;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;

namespace BoilerModellingTool.DAL
{
    public class BoilerInformationDAL
    {
        #region " Variables "

        private Database currentDatabase;

        #endregion

        #region " Constructor "

        public BoilerInformationDAL()
        {
            currentDatabase = DatabaseFactory.CreateDatabase(StoredProcedure.DBName);
        }
        #endregion

        public DataSet InsertUpdateBoilerInformation(BoilerInformationSC vBoilerInformationSC)
        {
            DataSet mDSet = null;
            DbCommand mDbCommand = null;
            String mStoredProcedure = String.Empty;
            try
            {
                mStoredProcedure = StoredProcedure.spr_InsertUpdateBoilerInformation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcedure);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, vBoilerInformationSC.BoilerID);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, vBoilerInformationSC.ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerManufacturer", DbType.String, vBoilerInformationSC.BoilerManufacturer);
                currentDatabase.AddInParameter(mDbCommand, "@vCapacity", DbType.String, vBoilerInformationSC.Capacity);
                currentDatabase.AddInParameter(mDbCommand, "@vCapacityUnit", DbType.String, vBoilerInformationSC.CapacityUnit);
                currentDatabase.AddInParameter(mDbCommand, "@vTypeOfBoiler", DbType.String, vBoilerInformationSC.TypeOfBoiler);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerName", DbType.String, vBoilerInformationSC.BoilerName);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerType", DbType.String, vBoilerInformationSC.BoilerType);
                currentDatabase.AddInParameter(mDbCommand, "@vDrumArrangements", DbType.String, vBoilerInformationSC.DrumArrangements);
                currentDatabase.AddInParameter(mDbCommand, "@vTypeOfAirPreheater", DbType.String, vBoilerInformationSC.TypeOfAirPreheater);
                currentDatabase.AddInParameter(mDbCommand, "@vNumberOfAPH", DbType.String, vBoilerInformationSC.NumberOfAPH);
                currentDatabase.AddInParameter(mDbCommand, "@vNumberOfPrimaryAPH", DbType.String, vBoilerInformationSC.NumberOfPrimaryAPH);
                currentDatabase.AddInParameter(mDbCommand, "@vNumberOfSecondaryAPH", DbType.String, vBoilerInformationSC.NumberOfSecondaryAPH);
                currentDatabase.AddInParameter(mDbCommand, "@vPrimaryFuel", DbType.String, vBoilerInformationSC.PrimaryFuel);
                currentDatabase.AddInParameter(mDbCommand, "@vBurnerPosition", DbType.String, vBoilerInformationSC.BurnerPosition);
                currentDatabase.AddInParameter(mDbCommand, "@vTypeOfCornerFiring", DbType.String, vBoilerInformationSC.TypeOfCornerFiring);
                currentDatabase.AddInParameter(mDbCommand, "@vGasRecirculation", DbType.String, vBoilerInformationSC.GasRecirculation);
                currentDatabase.AddInParameter(mDbCommand, "@vTypeOfGas", DbType.String, vBoilerInformationSC.TypeOfGas);
                currentDatabase.AddInParameter(mDbCommand, "@vSHSteamTempControl", DbType.String, vBoilerInformationSC.SHSteamTempControl);
                currentDatabase.AddInParameter(mDbCommand, "@vRHSteamTempControl", DbType.String, vBoilerInformationSC.RHSteamTempControl);
                currentDatabase.AddInParameter(mDbCommand, "@vNumberOfSHDesuperheatingSprayStages", DbType.String, vBoilerInformationSC.NumberOfSHDesuperheatingSprayStages);
                currentDatabase.AddInParameter(mDbCommand, "@vNumberOfRHDesuperheatingSprayStages", DbType.String, vBoilerInformationSC.NumberOfRHDesuperheatingSprayStages);
                currentDatabase.AddInParameter(mDbCommand, "@vSecondaryFuel", DbType.String, vBoilerInformationSC.SecondaryFuel);
                //currentDatabase.AddInParameter(mDbCommand, "@vNumberOfEconomizerSections", DbType.String, vBoilerInformationSC.NumberOfEconomizerSections);
                //currentDatabase.AddInParameter(mDbCommand, "@vNumberOfSuperheaterElements", DbType.String, vBoilerInformationSC.NumberOfSuperheaterElements);
                //currentDatabase.AddInParameter(mDbCommand, "@vNumberOfReheaterElements", DbType.String, vBoilerInformationSC.NumberOfReheaterElements);
                //currentDatabase.AddInParameter(mDbCommand, "@vNumberOfWaterScreenSections", DbType.String, vBoilerInformationSC.NumberOfWaterScreenSections);
                //currentDatabase.AddInParameter(mDbCommand, "@vNumberOfSteamScreenSections", DbType.String, vBoilerInformationSC.NumberOfSteamScreenSections);              
                currentDatabase.AddInParameter(mDbCommand, "@vIsEdit", DbType.String, vBoilerInformationSC.IsEdit);
                currentDatabase.AddInParameter(mDbCommand, "@vFurnaceRoofCoolingMedium", DbType.String, vBoilerInformationSC.FurnaceRoofCoolingMedium);
                //currentDatabase.AddInParameter(mDbCommand, "@vPresenceOfCooledWater", DbType.String, vBoilerInformationSC.SteamCooled);
                //currentDatabase.AddInParameter(mDbCommand, "@vHeatingSection", DbType.String, vBoilerInformationSC.HeatingSection);



                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);
            }
            catch(Exception)
            {
                throw;
            }
            return mDSet;
        }   //Insert

        public BoilerInformationSC GetBoilerInformationByProjectID(string ProjectID,String BoilerID)
        {
            DataSet mDSet = null;
            BoilerInformationSC mBoilerInformationSC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mStoredProcName = StoredProcedure.spr_GetBoilerInformation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                mBoilerInformationSC = new BoilerInformationSC();
                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mBoilerInformationSC.ProjectID = mDSet.Tables[0].Rows[0]["ProjectID"].ToString();
                    mBoilerInformationSC.BoilerID = mDSet.Tables[0].Rows[0]["BoilerID"].ToString();
                    mBoilerInformationSC.BoilerManufacturer = mDSet.Tables[0].Rows[0]["BoilerManufacturer"].ToString();
                    mBoilerInformationSC.Capacity = mDSet.Tables[0].Rows[0]["Capacity"].ToString();
                    mBoilerInformationSC.CapacityUnit = mDSet.Tables[0].Rows[0]["CapacityUnit"].ToString();
                    mBoilerInformationSC.TypeOfBoiler = mDSet.Tables[0].Rows[0]["TypeOfBoiler"].ToString();
                    mBoilerInformationSC.BoilerType = mDSet.Tables[0].Rows[0]["BoilerType"].ToString();
                    mBoilerInformationSC.DrumArrangements = mDSet.Tables[0].Rows[0]["DrumArrangements"].ToString();
                    mBoilerInformationSC.TypeOfAirPreheater = mDSet.Tables[0].Rows[0]["TypeOfAirPreheater"].ToString();
                    mBoilerInformationSC.NumberOfAPH = mDSet.Tables[0].Rows[0]["NumberOfAPH"].ToString();
                    mBoilerInformationSC.NumberOfPrimaryAPH = mDSet.Tables[0].Rows[0]["NumberOfPrimaryAPH"].ToString();
                    mBoilerInformationSC.NumberOfSecondaryAPH = mDSet.Tables[0].Rows[0]["NumberOfSecondaryAPH"].ToString();
                    mBoilerInformationSC.PrimaryFuel = mDSet.Tables[0].Rows[0]["PrimaryFuel"].ToString();
                    mBoilerInformationSC.BurnerPosition = mDSet.Tables[0].Rows[0]["BurnerPosition"].ToString();
                    mBoilerInformationSC.TypeOfCornerFiring = mDSet.Tables[0].Rows[0]["TypeOfCornerFiring"].ToString();
                    mBoilerInformationSC.GasRecirculation = mDSet.Tables[0].Rows[0]["GasRecirculation"].ToString();
                    mBoilerInformationSC.TypeOfGas = mDSet.Tables[0].Rows[0]["TypeOfGas"].ToString();
                    mBoilerInformationSC.SHSteamTempControl = mDSet.Tables[0].Rows[0]["SHSteamTempControl"].ToString();
                    mBoilerInformationSC.RHSteamTempControl = mDSet.Tables[0].Rows[0]["RHSteamTempControl"].ToString();
                    mBoilerInformationSC.NumberOfSHDesuperheatingSprayStages = mDSet.Tables[0].Rows[0]["NumberOfSHDesuperheatingSprayStages"].ToString();
                    mBoilerInformationSC.NumberOfRHDesuperheatingSprayStages = mDSet.Tables[0].Rows[0]["NumberOfRHDesuperheatingSprayStages"].ToString();
                    mBoilerInformationSC.NumberOfEconomizerSections = mDSet.Tables[0].Rows[0]["NumberOfEconomizerSections"].ToString();
                    mBoilerInformationSC.NumberOfSuperheaterElements = mDSet.Tables[0].Rows[0]["NumberOfSuperheaterElements"].ToString();
                    mBoilerInformationSC.NumberOfReheaterElements = mDSet.Tables[0].Rows[0]["NumberOfReheaterElements"].ToString();
                    mBoilerInformationSC.NumberOfWaterScreenSections = mDSet.Tables[0].Rows[0]["NumberOfWaterScreenSections"].ToString();
                    mBoilerInformationSC.NumberOfSteamScreenSections = mDSet.Tables[0].Rows[0]["NumberOfSteamScreenSections"].ToString();
                    mBoilerInformationSC.SecondaryFuel = mDSet.Tables[0].Rows[0]["SecondaryFuel"].ToString();
                    mBoilerInformationSC.FurnaceRoofCoolingMedium = mDSet.Tables[0].Rows[0]["FurnaceRoofCoolingMedium"].ToString();
                    

                    
                }
                
            }
            catch(Exception ex)
            {

            }
            return mBoilerInformationSC;
        }   //Get Boiler Information

        public BoilerInformationSC GetUpperFurnaceInformation(string ProjectID,string BoilerID)
        {
            DataSet mDSet = null;
            BoilerInformationSC mBoilerInformationSC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mStoredProcName = StoredProcedure.spr_GetUpperFurnaceInformation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                mBoilerInformationSC = new BoilerInformationSC();
                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mBoilerInformationSC.HeatingSectionUpperFurnace = mDSet.Tables[0].Rows[0]["TotalSection"].ToString();
                }

            }
            catch(Exception ex)
            {

            }

            return mBoilerInformationSC;
        }   //Upper Furnace

        public BoilerInformationSC GetCrossDuctInformation(string ProjectID, string BoilerID)
        {
            DataSet mDSet = null;
            BoilerInformationSC mBoilerInformationSC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mStoredProcName = StoredProcedure.spr_GetCrossDuctInformation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                mBoilerInformationSC = new BoilerInformationSC();
                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mBoilerInformationSC.HeatingSectionCrossDuct = mDSet.Tables[0].Rows[0]["TotalSection"].ToString();
                }

            }
            catch (Exception ex)
            {

            }

            return mBoilerInformationSC;
        }   //Cross Duct

        public BoilerInformationSC GetReverseChamberInformation(string ProjectID, string BoilerID)
        {
            DataSet mDSet = null;
            BoilerInformationSC mBoilerInformationSC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mStoredProcName = StoredProcedure.spr_GetReverseChamberInformation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                mBoilerInformationSC = new BoilerInformationSC();
                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mBoilerInformationSC.EconomizerHangerTubePresentUpstream = mDSet.Tables[0].Rows[0]["EconomizerHangerTubePresentUpstream"].ToString();
                    mBoilerInformationSC.PendantSHSectionPresentUpstream = mDSet.Tables[0].Rows[0]["PendantSHPresentUpstream"].ToString();
                    mBoilerInformationSC.PendantSHName = mDSet.Tables[0].Rows[0]["PendantSHName"].ToString();
                    mBoilerInformationSC.PendantRHSectionPresentUpstream = mDSet.Tables[0].Rows[0]["PendantRHPresentUpstream"].ToString();
                }

            }
            catch (Exception ex)
            {

            }

            return mBoilerInformationSC;
        }   //Reverse Chamber

        public BoilerInformationSC GetBackpassInformation(string ProjectID, string BoilerID)
        {
            DataSet mDSet = null;
            BoilerInformationSC mBoilerInformationSC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mStoredProcName = StoredProcedure.spr_GetBackpassInformation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                mBoilerInformationSC = new BoilerInformationSC();
                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mBoilerInformationSC.HeatingSectionBackpass = mDSet.Tables[0].Rows[0]["TotalSection"].ToString();
                }

            }
            catch (Exception ex)
            {

            }

            return mBoilerInformationSC;
        }   //Backpass

        public BoilerInformationSC GetBackpassSHInformation(string ProjectID, string BoilerID)
        {
            DataSet mDSet = null;
            BoilerInformationSC mBoilerInformationSC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mStoredProcName = StoredProcedure.spr_GetBackpassInformationSH;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                mBoilerInformationSC = new BoilerInformationSC();
                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mBoilerInformationSC.HeatingSectionBackpassSH = mDSet.Tables[0].Rows[0]["TotalSection"].ToString();
                }

            }
            catch (Exception ex)
            {

            }

            return mBoilerInformationSC;
        }   //Backpass SH

        public BoilerInformationSC GetBackpassRHInformation(string ProjectID, string BoilerID)
        {
            DataSet mDSet = null;
            BoilerInformationSC mBoilerInformationSC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mStoredProcName = StoredProcedure.spr_GetBackpassInformationRH;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                mBoilerInformationSC = new BoilerInformationSC();
                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mBoilerInformationSC.HeatingSectionBackpassRH = mDSet.Tables[0].Rows[0]["TotalSection"].ToString();
                }

            }
            catch (Exception ex)
            {

            }

            return mBoilerInformationSC;
        }   //Backpass RH

        public BoilerInformationSC GetMiscellaneousInformation(string ProjectID, string BoilerID)
        {
            DataSet mDSet = null;
            BoilerInformationSC mBoilerInformationSC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mStoredProcName = StoredProcedure.spr_GetMiscellaneousInformation;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                mBoilerInformationSC = new BoilerInformationSC();
                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mBoilerInformationSC.LocationSHDesuperheatingSpray = mDSet.Tables[0].Rows[0]["LocationSHDesuperheatingSpray"].ToString();
                    mBoilerInformationSC.LocationRHDesuperheatingSpray = mDSet.Tables[0].Rows[0]["LocationRHDesuperheatingSpray"].ToString();
                    //mBoilerInformationSC.HeatingSectionDownstreamSH = mDSet.Tables[0].Rows[0]["HeatingSectionDownstreamSH"].ToString();
                    //mBoilerInformationSC.HeatingSectionDownstreamRH = mDSet.Tables[0].Rows[0]["HeatingSectionDownstreamRH"].ToString();
                    mBoilerInformationSC.NumberOfSHDesuperheatingSprayStages = mDSet.Tables[0].Rows[0]["SHDesuperheatingSprayStages"].ToString();
                    mBoilerInformationSC.NumberOfRHDesuperheatingSprayStages = mDSet.Tables[0].Rows[0]["RHDesuperheatingSprayStages"].ToString();
                    mBoilerInformationSC.SteamCooled = Convert.ToInt16(mDSet.Tables[0].Rows[0]["PresenceOfCooledWater"]);
                    mBoilerInformationSC.HeatingSection = mDSet.Tables[0].Rows[0]["HeatingSection"].ToString();
                }

            }
            catch (Exception ex)
            {

            }

            return mBoilerInformationSC;
        }   //Miscellaneous

        public BoilerInformationSC GetMiscellaneousSHInfo(string ProjectID,string BoilerID)
        {
            DataSet mDSet = null;
            BoilerInformationSC mBoilerInformationSC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mStoredProcName = StoredProcedure.spr_GetMiscellaneousInformationSH;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                mBoilerInformationSC = new BoilerInformationSC();
                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mBoilerInformationSC.NumberOfSHDesuperheatingSprayStages = mDSet.Tables[0].Rows[0]["TotalSection"].ToString();
                }

            }
            catch (Exception ex)
            {

            }

            return mBoilerInformationSC;
        }

        public BoilerInformationSC GetMiscellaneousRHInfo(string ProjectID, string BoilerID)
        {
            DataSet mDSet = null;
            BoilerInformationSC mBoilerInformationSC = null;
            String mStoredProcName = String.Empty;
            DbCommand mDbCommand = null;
            mDSet = new DataSet();

            try
            {
                mStoredProcName = StoredProcedure.spr_GetMiscellaneousInformationRH;
                mDbCommand = currentDatabase.GetStoredProcCommand(mStoredProcName);
                currentDatabase.AddInParameter(mDbCommand, "@vProjectID", DbType.String, ProjectID);
                currentDatabase.AddInParameter(mDbCommand, "@vBoilerID", DbType.String, BoilerID);

                mDSet = new DataSet();
                mDSet = currentDatabase.ExecuteDataSet(mDbCommand);

                mBoilerInformationSC = new BoilerInformationSC();
                if (mDSet.Tables[0].Rows.Count > 0)
                {
                    mBoilerInformationSC.NumberOfRHDesuperheatingSprayStages = mDSet.Tables[0].Rows[0]["TotalSection"].ToString();
                }

            }
            catch (Exception ex)
            {

            }

            return mBoilerInformationSC;
        }

    }
}
