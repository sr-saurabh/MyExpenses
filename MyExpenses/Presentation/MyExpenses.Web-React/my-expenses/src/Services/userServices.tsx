import { get, post } from './HttpProviders';

export interface registerUserData {
    firstName: string,
    lastName: string,
    avatar: string | null,
    phoneNumber: string | null,
}
var userProfile = {};

export const getCurrentUserProfile = (forceLoad = false) => {
    const profileData = localStorage.getItem('profileData');
    return get('AppUser/get-current-user');
    // if (!forceLoad &&(profileData != null || profileData !== undefined)) {
    //     userProfile = JSON.parse(profileData);
    //     return userProfile;
    // }
    // if(forceLoad || userProfile ==null || userProfile == undefined){
    //     debugger;
    //     get('AppUser/get-current-user').then((response) => {
    //         userProfile = response.data;
    //         localStorage.setItem('profileData', JSON.stringify(userProfile));
    //         return userProfile;
    //     }).catch((error) => {
    //         console.log(error);
    //     });
    // }
};

export const registerUser = (userData: registerUserData) => {
    return post('AppUser', userData);
}