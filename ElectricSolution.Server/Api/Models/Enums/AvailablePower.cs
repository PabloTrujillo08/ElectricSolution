namespace ElectricSolution.Server.Api.Models.Enums
{
    public enum AvailablePower
    {
        Power3_3KW = 3300,
        Power4_4KW = 4400,
        Power5_5KW = 5500,
        Power6_6KW = 6600,
        Power7_7KW = 7700,
        Power8_8KW = 8800,
        Power9_9KW = 9900,
        Power10KW = 10000
    }

    public static class AvailablePowerExtensions
    {
        public static string ConvertPowerEnumToString(this AvailablePower power)
        {
            switch (power)
            {
                case AvailablePower.Power3_3KW: return "3.3 kW";
                case AvailablePower.Power4_4KW: return "4.4 kW";
                case AvailablePower.Power5_5KW: return "5.5 kW";
                case AvailablePower.Power6_6KW: return "6.6 kW";
                case AvailablePower.Power7_7KW: return "7.7 kW";
                case AvailablePower.Power8_8KW: return "8.8 kW";
                case AvailablePower.Power9_9KW: return "9.9 kW";
                case AvailablePower.Power10KW: return "10 kW";

                default: return "Unknown";
            }
        }
    }
}
