namespace ElectricSolution.Server.Api.Models.Enums
{
    public enum BatteryModels
    {
        TeslaPowerwall,
        LGCHEM,
        PanasonicEverVolt,
        SonnenEco,
        Simpliphi,
        Enphase,
        SamsungSDI,
        BYD_BBox,
        PylontechUS2000,
        PylontechUS3000,
        LGChemRESU,
        BYD_BatteryBoxPro,
        BlueNova,
        VictronEnergyMultiPlus,
        AlphaESS,
        EnerSysPowerSafe,
        TrojanSolarAGM,
        DiscoverAES,
        RollsSurrette,
        OutBackEnergyCell,
        CATLC48V,
        SchneiderElectric,
        BMZGroupESSX,
        SolaXPower,
        GeneracPWRcell
    }

    public static class BatteryModelsExtensions
    {
        public static string ConvertBatteryModelEnumToString(this BatteryModels model)
        {
            switch (model)
            {
                case BatteryModels.TeslaPowerwall:return "Tesla Powerwall";
                case BatteryModels.LGCHEM:return "LG Chem RESU";
                case BatteryModels.PanasonicEverVolt:return "Panasonic EverVolt";
                case BatteryModels.SonnenEco:return "Sonnen Eco";
                case BatteryModels.Simpliphi:return "Simpliphi PHI";
                case BatteryModels.Enphase:return "Enphase IQ Battery";
                case BatteryModels.SamsungSDI:return "Samsung SDI";
                case BatteryModels.BYD_BBox:return "BYD B-Box";
                case BatteryModels.PylontechUS2000:return "Pylontech US2000";
                case BatteryModels.PylontechUS3000:return "Pylontech US3000";
                case BatteryModels.LGChemRESU:return "LG Chem RESU 10H";
                case BatteryModels.BYD_BatteryBoxPro:return "BYD Battery-Box Pro";
                case BatteryModels.BlueNova:return "Blue Nova HP";
                case BatteryModels.VictronEnergyMultiPlus:return "Victron Energy MultiPlus";
                case BatteryModels.AlphaESS:return "Alpha ESS";
                case BatteryModels.EnerSysPowerSafe:return "EnerSys PowerSafe";
                case BatteryModels.TrojanSolarAGM:return "Trojan Solar AGM";
                case BatteryModels.DiscoverAES:return "Discover AES";
                case BatteryModels.RollsSurrette:return "Rolls Surrette S-550";
                case BatteryModels.OutBackEnergyCell:return "OutBack EnergyCell";
                case BatteryModels.CATLC48V:return "CATL C48V";
                case BatteryModels.SchneiderElectric:return "Schneider Electric Conext Battery";
                case BatteryModels.BMZGroupESSX:return "BMZ Group ESS X";
                case BatteryModels.SolaXPower:return "SolaX Power Station";
                case BatteryModels.GeneracPWRcell:return "Generac PWRcell";
                default:return "Unknown Model";
            }
        }
    }


}
