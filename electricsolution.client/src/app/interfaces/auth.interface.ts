export interface LoginResponse {
  isAuthenticated: boolean;
  userName?: string;
}

export interface AuthResponse {
  email: string;
  token: string;
  userId: string;
  username: string;
  message: string;
}
