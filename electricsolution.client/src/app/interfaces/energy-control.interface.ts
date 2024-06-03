export interface EnergyControl {
  ID: number;
  date: Date;

  act_pw_o: number;
  ac_ao: number;
  ac_vo: number;

  consu_pw: number;

  dc1_a: number;
  dc1_pw: number;
  dc1_v: number;

  dc2_a: number;
  dc2_pw: number;
  dc2_v: number;

  dc_pw: number;

  ef: number;
  energ_exp: number;
  energ_imp: number;
  factpw_o: number;
  freq_o: number;
  grid_a: number;
  grid_actpw: number;
  grid_exp_pw: number;
  grid_factpw: number;
  grid_freq: number;
  grid_imp_pw: number;
  grid_reapw: number;
  grid_v: number;
  pe_pw: number;

  precio_exced: number;
  precio_pvpc: number;

  rea_pw_o: number;
  state_m: number;

  temp: number;
}
