using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi
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

            var sharedKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration["SigningKey"]));
            var test = Configuration["SigningKey"];

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
    }
}
