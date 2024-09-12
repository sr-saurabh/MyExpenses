import { post } from '../Services/HttpProviders';

export const login = (loginData) => {
    return post('Auth/login', loginData);
};

export const register = (registerData) => {
    return post("Auth/register", registerData);
};

// for google login 
export const googleLogin = (credentials) => {
    return post('Auth/login-with-google', credentials);
};