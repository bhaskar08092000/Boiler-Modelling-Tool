using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerModellingTool.SC
{
    public class BoilerInformationSC
    {
        public string BoilerID { get; set; }
        public string ProjectID { get; set; }
        //public string SampleDate { get; set; }
        public string TypeOfBoiler { get; set; }
        public string BoilerName { get; set; }
        public string BoilerType { get; set; }
        public string DrumArrangements { get; set; }
        public string Objective { get; set; }
        public string BoilerManufacturer { get; set; }
        public string Capacity { get; set; }
        public string CapacityUnit { get; set; }
        public string TypeOfAirPreheater { get; set; }
        public string NumberOfAPH { get; set; }
        public string NumberOfPrimaryAPH { get; set; }
        public string NumberOfSecondaryAPH { get; set; }
        public string PrimaryFuel { get; set; }
        public string BurnerPosition { get; set; }
        public string TypeOfCornerFiring { get; set; }
        public string GasRecirculation { get; set; }
        public string TypeOfGas { get; set; }
        public string SHSteamTempControl { get; set; }
        public string RHSteamTempControl { get; set; }
        public string NumberOfSuperheaterElements { get; set; }
        public string NumberOfReheaterElements { get; set; }
        public string NumberOfWaterScreenSections { get; set; }
        public string NumberOfSteamScreenSections { get; set; }
        public string NumberOfSHDesuperheatingSprayStages { get; set; }
        public string NumberOfRHDesuperheatingSprayStages { get; set; }
        public string NumberOfEconomizerSections { get; set; }
        public string SecondaryFuel { get; set; }
        public string IsEdit { get; set; }

        public string HeatingSectionUpperFurnace { get; set; }

        public string HeatingSectionCrossDuct { get; set; }

        public string EconomizerHangerTubePresentUpstream { get; set; }
        public string PendantSHSectionPresentUpstream { get; set; }
        public string PendantSHName { get; set; }
        public string PendantRHSectionPresentUpstream { get; set; }

        public string HeatingSectionBackpass { get; set; }
        public string HeatingSectionBackpassRH { get; set; }
        public string HeatingSectionBackpassSH { get; set; }
        public string BackpassConfiguration { get; set; }

        public string PreceedingHeatingSectionRH { get; set; }
        public string SucceedingHeatingSectionRH { get; set; }
        public string PreceedingHeatingSectionSH { get; set; }
        public string SucceedingHeatingSectionSH { get; set; }

        public string LocationSHDesuperheatingSpray { get; set; }
        public string LocationRHDesuperheatingSpray { get; set; }

        public string HeatingSectionDownstreamSH { get; set; }
        public string HeatingSectionDownstreamRH { get; set; }

        public string FurnaceRoofCoolingMedium { get; set; }


        //public string HeatingSectionDownstreamSH { get; set; }
        //public string HeatingSectionDownstreamRH { get; set; }



        public int SteamCooled { get; set; }
        public string HeatingSection { get; set; }



        
    }
}
