export interface resetPassword {
  Email: string;
  OriginalPassword: string;
  ModifiedPassword: string;
  ConfirmedPassword: string;
  message: string;
}
export interface ToggleFields {
  passwordFieldType: boolean;
  currentPasswordFieldType: boolean;

}
