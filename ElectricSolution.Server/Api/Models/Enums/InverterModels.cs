namespace ElectricSolution.Server.Api.Models.Enums
{
    public enum InverterModels
    {
        SolarEdgeSE,
        SMA_SunnyBoy,
        FroniusPrimo,
        EnphaseMicro,
        VictronEnergy,
        HuaweiSun2000,
        OutBackPower,
        SchneiderElectricConext,
        ABB_UNO,
        DeltaSolivia,
        KACO_Blueplanet,
        GoodWe,
        Growatt,
        Sungrow,
        SolaredgeHDL,
        TMEIC,
        Omron,
        Tabuchi,
        ChintPower,
        Solis,
        Tigo,
        Refusol,
        Solectria,
        Yaskawa
    }

    public static class InverterModelsExtensions
    {
        public static string ConvertInverterModelEnumToString(this InverterModels model)
        {
            switch (model)
            {
                case InverterModels.SolarEdgeSE: return "SolarEdge SE";
                case InverterModels.SMA_SunnyBoy: return "SMA Sunny Boy";
                case InverterModels.FroniusPrimo: return "Fronius Primo";
                case InverterModels.EnphaseMicro: return "Enphase Microinverter";
                case InverterModels.VictronEnergy: return "Victron Energy";
                case InverterModels.HuaweiSun2000: return "Huawei Sun2000";
                case InverterModels.OutBackPower: return "OutBack Power Systems";
                case InverterModels.SchneiderElectricConext: return "Schneider Electric Conext";
                case InverterModels.ABB_UNO: return "ABB UNO";
                case InverterModels.DeltaSolivia: return "Delta Solivia";
                case InverterModels.KACO_Blueplanet: return "KACO Blueplanet";
                case InverterModels.GoodWe: return "GoodWe";
                case InverterModels.Growatt: return "Growatt";
                case InverterModels.Sungrow: return "Sungrow";
                case InverterModels.SolaredgeHDL: return "SolarEdge HD Wave";
                case InverterModels.TMEIC: return "TMEIC Solar Ware";
                case InverterModels.Omron: return "Omron Residential";
                case InverterModels.Tabuchi: return "Tabuchi Electric";
                case InverterModels.ChintPower: return "Chint Power";
                case InverterModels.Solis: return "Ginlong Solis";
                case InverterModels.Tigo: return "Tigo Energy Maximizer";
                case InverterModels.Refusol: return "REFUsol";
                case InverterModels.Solectria: return "Solectria Renewables";
                case InverterModels.Yaskawa: return "Yaskawa - Solectria Solar";
                default:
                    return "Unknown Model";
            }
        }

    }
}
