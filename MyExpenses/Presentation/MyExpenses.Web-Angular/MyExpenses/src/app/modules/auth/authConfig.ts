import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {

  issuer: 'https://accounts.google.com',

  redirectUri: window.location.origin,

  clientId: '13612280549-t9t3daa1pkqu8uldfhai2ois973hlaai.apps.googleusercontent.com',

  scope: 'openid profile email',

  strictDiscoveryDocumentValidation: false,

};
