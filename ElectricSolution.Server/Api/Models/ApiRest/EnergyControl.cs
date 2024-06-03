using System.ComponentModel.DataAnnotations;

namespace ElectricSolution.Server.Api.Models.ApiRest
{
    public class EnergyControl
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }

        public float act_pw_o { get; set; }
        public float ac_ao { get; set; }
        public float ac_vo { get; set; }

        public int consu_pw { get; set; }

        public float dc1_a { get; set; }
        public float dc1_pw { get; set; }
        public float dc1_v { get; set; }

        public float dc2_a { get; set; }
        public float dc2_pw { get; set; }
        public float dc2_v { get; set; }

        public float dc_pw { get; set; }

        public float ef { get; set; }
        public float energ_exp { get; set; }
        public float energ_imp { get; set; }
        public float factpw_o { get; set; }
        public float freq_o { get; set; }
        public float grid_a { get; set; }
        public float grid_actpw { get; set; }
        public float grid_exp_pw { get; set; }
        public float grid_factpw { get; set; }
        public float grid_freq { get; set; }
        public float grid_imp_pw { get; set; }
        public float grid_reapw { get; set; }
        public float grid_v { get; set; }
        public float pe_pw { get; set; }

        public float precio_exced { get; set; }
        public float precio_pvpc { get; set; }

        public float rea_pw_o { get; set; }
        public float state_m { get; set; }

        public float temp { get; set; }


    }
}


public class EnergyControlData
{
    public string[] _now { get; } = {
        "ACT_PW_O",
        "AC_AO",
        "AC_VO",
        "CONSU_PW",
        "DC1_A",
        "DC1_PW",
        "DC1_V",
        "DC2_A",
        "DC2_PW",
        "DC2_V",
        "DC_PW",
        "EF",
        "ENERG_EXP",
        "ENERG_IMP",
        "FACTPW_O",
        "FREQ_O",
        "GRID_A",
        "GRID_ACTPW",
        "GRID_EXP_PW",
        "GRID_FACTPW",
        "GRID_FREQ",
        "GRID_IMP_PW",
        "GRID_REAPW",
        "GRID_V",
        "PE_PW",
        "PRECIO_EXCED",
        "PRECIO_PVPC",
        "REA_PW_O",
        "STATE_M",
        "TEMP"
    };
}



/*
ACT_PW_O     float
AC_AO        float
AC_VO        float
CONSU_PW     float
DC1_A        float
DC1_PW       float
DC1_V        float
DC2_A        float
DC2_PW       float
DC2_V        float
DC_PW        float
EF           float
ENERG_EXP    float
ENERG_IMP    float
FACTPW_O     float
FREQ_O       float
GRID_A       float
GRID_ACTPW   float
GRID_EXP_PW  float
GRID_FACTPW  float
GRID_FREQ    float
GRID_IMP_PW  float
GRID_REAPW   float
GRID_V       float
PE_PW        float
PRECIO_EXCED float
PRECIO_PVPC  float
REA_PW_O     float
STATE_M      float
TEMP         float


*/