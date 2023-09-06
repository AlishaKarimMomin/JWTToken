using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using Utility.Configuration;
using Utility.Security;
using Utility.Configuration;

namespace Utility.Security
{
    public class JWTBuilder
    {
        #region JWT Token Genration
        //public static Tuple<string, DateTime?> Generation(ObjectId userId, string username, int userTypeId, ObjectId companyId, string email, string password, string appURL,string CompanyType, int? duration = null)
        public static Tuple<string, DateTime?> Generation(int userId, string username, int userTypeId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(EncryptorDecryptorEngine.password);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("USERID",  Convert.ToString(userId)),
                    new Claim("USERNAME", username),
                    new Claim("USERTYPEID", Convert.ToString(userTypeId)),                    
                }),
                Expires = AppSettings.JWT_TokenExpirationInSeconds(),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return new Tuple<string, DateTime?>(tokenString, tokenDescriptor.Expires);
        }

        #endregion


        public static List<Claim> IsTokenValid(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();
            SecurityToken validatedToken;
            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                return principal.Claims.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Sentry.SentrySdk.CaptureException(ex);
                return null;
            }
        }

        public static bool IsTokenExpired(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();
            SecurityToken validatedToken;
            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                var expire = Convert.ToInt32(principal.Claims.ToList().FirstOrDefault(x => x.Type == "exp").Value);
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(expire);
                if (dateTimeOffset.LocalDateTime < DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Sentry.SentrySdk.CaptureException(ex);
                return false;
            }
        }

        public static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // validate expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EncryptorDecryptorEngine.password)) // The same key as the one that generate the token
            };
        }
    }
}
