using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BankPortalAggregator.Models;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BankPortalAggregator.Helpers
{
    public class TokenHelper
    {
        private readonly IConfiguration _configuration;

        public TokenHelper(IConfiguration configuration, BankContext context)
        {
            _configuration = configuration;
        }

        //public async Task<GoogleAccessTokenInfo> Validate(string accessToken)
        //{
        //    HttpClient client = new HttpClient();

        //    string path = "https://oauth2.googleapis.com/tokeninfo?id_token=" + accessToken;
        //    HttpResponseMessage response = await client.GetAsync(path);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        GoogleAccessTokenInfo tokenInfo = await response.Content.ReadAsAsync<GoogleAccessTokenInfo>();
        //        if (tokenInfo.aud == _configuration["Authentication:Google:ClientId"] || tokenInfo.exp > (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds)
        //        {
        //            return tokenInfo;
        //        }
        //    }
        //    return null;
        //}

        public async Task<GoogleJsonWebSignature.Payload> Validate(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _configuration["Authentication:Google:ClientId"] },
            };

            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            return payload;
        }

        public string GetJwtToken(User tokenInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, tokenInfo.Email),
                new Claim(ClaimTypes.Name, tokenInfo.Name),
                new Claim(ClaimTypes.Surname, tokenInfo.Surname),
                new Claim("Sub", tokenInfo.Id.ToString()),
             };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token");

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: _configuration["Authentication:ISSUER"],
                    audience: _configuration["Authentication:AUDIENCE"],
                    notBefore: now,
                    claims: claimsIdentity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(5)),
                    signingCredentials: new SigningCredentials(
                        GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretKey"]));
        }

        public async Task<User> ExchangeAuthorizationCode(string code)
        {
            using (HttpClient client = new HttpClient())
            {

                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("client_id", _configuration["Authentication:Google:ClientId"]),
                new KeyValuePair<string, string>("client_secret", _configuration["Authentication:Google:ClientSecret"]),
                new KeyValuePair<string, string>("redirect_uri", "https://localhost:44345"),
                new KeyValuePair<string, string>("grant_type", "authorization_code")
                });

                string path = "https://www.googleapis.com/oauth2/v4/token";
                HttpResponseMessage response = await client.PostAsync(path, content);

                response.EnsureSuccessStatusCode();

                GoogleCodeExchange tokenInfo = await response.Content.ReadAsAsync<GoogleCodeExchange>();
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _configuration["Authentication:Google:ClientId"] },
                };

                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(tokenInfo.id_token, settings);

                User user = new User
                {
                    Sub = payload.Subject,
                    Email = payload.Email,
                    Name = payload.Name,
                    Surname = payload.FamilyName,
                    RefreshToken = tokenInfo.refresh_token,
                    AccessToken = tokenInfo.access_token,
                    IdToken = tokenInfo.id_token
                };

                return user;
            }
        }

        private string GetExchangeEndPoint(string code)
        {
            return $@"https://www.googleapis.com/oauth2/v4/token
                            code={code}
                            &client_id={_configuration["Authentication:Google:ClientId"]}
                            &client_secret={_configuration["Authentication:Google:ClientSecret"]}
                            &redirect_uri=https://localhost:44345/
                            &grant_type=authorization_code";
        }
    }
}
