using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BankPortalAggregator
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public IConfiguration Configuration { get; }

        public TokenMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this._next = next;
            Configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"];

            if (!String.IsNullOrWhiteSpace(token))
            {
                var googleInitializer = new BaseClientService.Initializer
                {
                    ApiKey = Configuration["Authentication:Google:ApiKey"]
                };
                Oauth2Service ser = new Oauth2Service(googleInitializer);
                Oauth2Service.TokeninfoRequest req = ser.Tokeninfo();
                req.AccessToken = token;  //access token received from Google SignIn button                

                try
                {
                    Tokeninfo userinfo = await req.ExecuteAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                if (token != "12345678")
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Token is invalid");
                }
                else
                {
                    await _next.Invoke(context);
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
