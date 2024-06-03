export interface ServerResponse<T> {
  message: string;
  success: boolean;
  data?: T;
}
