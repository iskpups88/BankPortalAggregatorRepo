using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BankApi.Dtos;
using BankApi.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositsController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IConfiguration _configuration;

        public DepositsController(IConfiguration configuration, ProductContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Deposit> PullProducts()
        {

            List<Deposit> products = _context.Deposits.Include(d => d.DepositVariations).Where(d => d.IsActive == true).ToList();
            return products;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> PostProductAsync([FromBody] DepositDto product)
        {
            try
            {
                string authHeader = Request.Headers["Authorization"];
                string accessToken = authHeader.Substring("Bearer ".Length).Trim();

                using (var client = new HttpClient())
                {
                    var response = await client.GetUserInfoAsync(new UserInfoRequest
                    {
                        Address = _configuration["IdpProvider"] + "/connect/userinfo",
                        Token = accessToken
                    });


                    if (response.IsError) throw new Exception(response.Error);

                    //Bank getting UserInfo
                    var claims = response.Claims;
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
