using MyExpenses.Domain.core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.Auth
{
    public class AuthResponse
    {
        public string message { get; set; }
        public AuthCode AutCode { get; set; }
        public string? Data { get; set; }

        public AuthResponse(string message, AuthCode authCode, string? data)
        {
            this.message = message;
            this.AutCode = authCode;
            this.Data = data;
        }
    }

    public static class AuthResponseHelper
    {
        public static AuthResponse Success()
        {
            return new("Success", AuthCode.Success, null);
        }
        public static AuthResponse SuccessWithToken(string token)
        {
            return new("Success", AuthCode.Success, token);
        }
        
        public static AuthResponse InValidPassword()
        {
            return new("Invalid email or password", AuthCode.InvalidPassword, null);
        }
        public static AuthResponse UserDoesNotExist()
        {
            return new("User does not exist", AuthCode.UserDoesNotExist, null);
        }
        public static AuthResponse AlreadyRegistered()
        {
            return new("User already registered", AuthCode.AlreadyRegistered, null);
        }
        
        public static AuthResponse UnKnownError(string message)
        {
            return new(message, AuthCode.UnknownError, null);
        }
        
        public static AuthResponse AuthenticationFailed(string message)
        {
            return new(message, AuthCode.Failure, null);
        }

        
    }
}
