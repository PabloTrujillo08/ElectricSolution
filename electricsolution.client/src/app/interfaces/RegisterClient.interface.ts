export interface RegisterClient {
  email: string;
  password: string;
  name: string;
  lastName: string;
  userName: string;
  addresses: Address[];
  phoneNumber: string;
  vat: string;
  birthDate?: Date;
  creationDate?: Date;
}

export interface Address {
  street: string;
  doorNumber: string;
  city: string;
  zipCode: string;
  provinceId: number;
  isMainAddress: boolean;
}
