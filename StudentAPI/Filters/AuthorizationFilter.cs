using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Utility.Security;

namespace Students.API.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        const string tokenKey = "accesstoken";

        bool requireHttps = false;

        private enum TokenAuthorization : int
        {
            Authorized = 0,
            InvalidToken = 1,
            MissingToken = 2,
            MissingAPIKey = 3,
            InvalidAPIKey = 4,
            UnAuthorized = 5,
            TokenExpire = 6
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            switch (AuthorizeToken(context))
            {
                case TokenAuthorization.Authorized:
                    //DO NOTHING
                    break;
                case TokenAuthorization.InvalidToken:
                    throw new UnauthorizedAccessException("Invalid Token");
                //break;
                //context.Result = SendResponse(new APIErrorResponse("Invalid Token", HttpStatusCode.Unauthorized)
                case TokenAuthorization.InvalidAPIKey:
                    //context.Result = SendResponse(new APIErrorResponse("Invalid API Key", HttpStatusCode.Unauthorized));
                    throw new UnauthorizedAccessException("Invalid API Key");
                //break;
                case TokenAuthorization.MissingToken:
                    //context.Result = SendResponse(new APIErrorResponse("Missing Token", HttpStatusCode.Unauthorized));
                    throw new UnauthorizedAccessException("Missing Token");
                //break;
                case TokenAuthorization.MissingAPIKey:
                    //context.Result = SendResponse(new APIErrorResponse("Missing API Key", HttpStatusCode.Unauthorized));
                    throw new UnauthorizedAccessException("Missing API Key");
                //break;
                case TokenAuthorization.UnAuthorized:
                    //context.Result = SendResponse(new APIErrorResponse("Unauthorized", HttpStatusCode.Unauthorized));
                    throw new UnauthorizedAccessException("Unauthorized");
                //break;
                case TokenAuthorization.TokenExpire:
                    //context.Result = SendResponse(new APIErrorResponse("Token has been expired", HttpStatusCode.Unauthorized));
                    throw new UnauthorizedAccessException("Token has been expired");
                //break;
                default:
                    break;
            }
        }
        private TokenAuthorization AuthorizeToken(AuthorizationFilterContext actionContext)
        {
            if (!actionContext.HttpContext.Request.Headers.Any(x => x.Key == tokenKey))
            {
                return TokenAuthorization.MissingToken;
            }
            else
            {
                string token = actionContext.HttpContext.Request.Headers[tokenKey][0];
                List<Claim> claims = JWTBuilder.IsTokenValid(token);
                if (claims == null)
                {
                    if (JWTBuilder.IsTokenExpired(token))
                        return TokenAuthorization.TokenExpire;
                    else
                        return TokenAuthorization.InvalidToken;
                }
                else
                {
                    
                    if (int.TryParse(claims[1].Value, out _) && Int32.Parse(claims[1].Value) == 1)
                    {
                        actionContext.HttpContext.Items.Add(new KeyValuePair<object, object>("USERNAME", claims.FirstOrDefault(x => x.Type == "USERNAME").Value));
                        actionContext.HttpContext.Items.Add(new KeyValuePair<object, object>("USERID", claims.FirstOrDefault(x => x.Type == "USERID").Value));
                        actionContext.HttpContext.Items.Add(new KeyValuePair<object, object>("USERTYPEID", claims.FirstOrDefault(x => x.Type == "USERTYPEID").Value));
                    }
                    else
                    {
                        actionContext.HttpContext.Items.Add(new KeyValuePair<object, object>("USERNAME", claims.FirstOrDefault(x => x.Type == "USERNAME").Value));
                        actionContext.HttpContext.Items.Add(new KeyValuePair<object, object>("USERID", claims.FirstOrDefault(x => x.Type == "USERID").Value));
                        actionContext.HttpContext.Items.Add(new KeyValuePair<object, object>("USERTYPEID", claims.FirstOrDefault(x => x.Type == "USERTYPEID").Value));
                    }
                    if (int.Parse(claims.FirstOrDefault(x => x.Type == "USERTYPEID").Value) == 2)
                    {
                        if(
                            (actionContext.HttpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
                            || 
                            (actionContext.HttpContext.Request.Method.Equals("PUT", StringComparison.OrdinalIgnoreCase))
                            ||
                            (actionContext.HttpContext.Request.Method.Equals("PATCH", StringComparison.OrdinalIgnoreCase))
                            ||
                            (actionContext.HttpContext.Request.Method.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
                            )
                        {
                            return TokenAuthorization.UnAuthorized;
                        }
                    }
                    return TokenAuthorization.Authorized;
                }
            }
        }
    }
}


// admin -> all
// student -> do all GET