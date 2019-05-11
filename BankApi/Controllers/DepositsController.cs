using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepositsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult PullProducts()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> PostProductAsync([FromBody] Product product)
        {
            try
            {
                string authHeader = Request.Headers["Authorization"];
                var tokens = authHeader.Substring("tokens ".Length).Trim();
                var tokenArr = tokens.Split(',');
                string accessToken = tokenArr[0];
                string idToken = tokenArr[1];

                using (HttpClient client = new HttpClient())
                {
                    //string path = "https://oauth2.googleapis.com/tokeninfo?access_token=" + accessToken;
                    string path = "https://www.googleapis.com/oauth2/v3/userinfo";

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    HttpResponseMessage response = await client.GetAsync(path);

                    response.EnsureSuccessStatusCode();
                    var obj = await response.Content.ReadAsAsync<object>();

                    var idTokenPayload = await Validate(idToken);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<GoogleJsonWebSignature.Payload> Validate(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _configuration["Authentication:AUDIENCE"] },
            };

            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            return payload;
        }
    }

    public class Product
    {
        public int ProductId { get; set; }
    }

    public class TokenInfo
    {
        public string iss { get; set; }
        public string aud { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string family_name { get; set; }
        public int exp { get; set; }
    }
}
