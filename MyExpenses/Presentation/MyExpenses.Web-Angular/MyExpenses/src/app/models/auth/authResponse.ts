export interface AuthResponse{
  message:string;
  authCode:AuthCode;
  data:string;
}

export enum AuthCode{
  UserDoesNotExist,
AlreadyRegistered,
LoginFailed,
InvalidPassword,
UnknownError,
Failure,
Success
}
