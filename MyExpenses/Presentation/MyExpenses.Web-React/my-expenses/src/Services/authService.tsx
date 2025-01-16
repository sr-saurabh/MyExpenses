import { LoginModel } from '../Models/LoginModel';
import { RegisterModel } from '../Models/RegisterModel';
import { post } from './HttpProviders';
// export interface LoginData {

export const login = (loginData: LoginModel) => {
    return post('Auth/login', loginData);
};

export const register = (registerData: RegisterModel) => {
    return post("Auth/register", registerData);
};

// for google login 
export const googleLogin = (credentials: any) => {
    return post('Auth/login-with-google', credentials);
};