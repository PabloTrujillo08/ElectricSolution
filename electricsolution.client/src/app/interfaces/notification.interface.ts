export interface notification {
  id: string;
  message: string;
  read: boolean;
  sentDate: Date,
  reason: number,
  clientId: string,
  contractId?: number,
}
