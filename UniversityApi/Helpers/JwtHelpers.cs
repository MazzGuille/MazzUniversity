using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityApi.Models;
using UniversityApi.Models.DataModels;

namespace UniversityApi.Helpers
{
    public static class JwtHelpers
    {

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim>
           {
               new Claim("Id", userAccounts.Id.ToString()),
               new Claim(ClaimTypes.Name, userAccounts.UserName),
               new Claim(ClaimTypes.Email, userAccounts.EmailId),
               new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
               new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
           };

            if (userAccounts.UserName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }
            else if (userAccounts.UserName == "User 1")
            {
                claims.Add(new Claim(ClaimTypes.Role, "user"));
                claims.Add(new Claim("User Only", "user1"));
            }

            return claims;

        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }

        public static UserTokens GenTokenKey(UserTokens model, JwtSettings JwtSettings)
        {
            try
            {
                var userToken = new UserTokens();
                if (model == null)
                {
                    throw new ArgumentException(nameof(model));
                }

                //OBTIAN SECRET KEY
                var key = System.Text.Encoding.ASCII.GetBytes(JwtSettings.IssuerSigningKey);
                Guid Id;

                //EXPIRES IN 1 DAY
                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                //VALIDITY
                userToken.Validity = expireTime.TimeOfDay;

                //GENERATE JWT
                var jwtToken = new JwtSecurityToken(

                    issuer: JwtSettings.ValidIssuer,
                    audience: JwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.EmailId = model.EmailId;
                userToken.Validity = expireTime.TimeOfDay;
                userToken.GuidId = Id;

                return userToken;
            }
            catch (Exception E)
            {

                throw new Exception("Error generating JWT", E);
            }
        }
    }
}
