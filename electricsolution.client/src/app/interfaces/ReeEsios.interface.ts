export interface ReeEsiosResponse {
  indicator: Indicator;
}

export interface Indicator {
  name: string;
  short_name: string;
  id: number;
  composited: boolean;
  step_type: string;
  disaggregated: boolean;
  magnitud: Magnitud[];
  tiempo: Tiempo[];
  geos: Geo[];
  values_updated_at: string;
  values: Value[];
  value: number;
  datetime: string;
  datetime_utc: string;
  tz_time: string;
  geo_id: number;
  geo_name: string;
}

export interface Magnitud {
  name: string;
  id: number;
}

export interface Tiempo {
  name: string;
  id: number;
}

export interface Geo {
  geo_id: number;
  geo_name: string;
}

export interface Value {
  value: number;
  datetime: string;
  datetime_utc: string;
  tz_time: string;
  geo_id: number;
  geo_name: string;
}
export interface ReeEsiosSalesResponse {
  indicator: Indicator;
}

