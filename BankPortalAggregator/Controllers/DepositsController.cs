using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BankPortalAggregator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BankPortalAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositsController : ControllerBase
    {
        private readonly BankContext _context;
        private readonly IMapper _mapper;

        public DepositsController(BankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> PostDeposit([FromBody] DepositDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string authHeader = Request.Headers["Authorization"];
            string accessToken = authHeader.Substring("Bearer ".Length).Trim();

            try
            {
                var endpoint = _context.Endpoints.Join(_context.Deposits,
                    e => e.BankId,
                    d => d.BankId,
                    (e, d) => new
                    {
                        Endpoint = e.EndpointUrl,
                        DepositId = d.Id,
                        d.BankDepositId
                    })
                    .Where(e => e.DepositId == product.Id).FirstOrDefault();

                var json = JsonConvert.SerializeObject(new
                {
                    Id = endpoint.BankDepositId.Value,
                    product.DepositVariations[0].Term,
                    product.DepositVariations[0].Percent,
                    product.Sum
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    string path = endpoint.Endpoint + "Deposits";
                    HttpResponseMessage response = await client.PostAsync(path, content);

                    response.EnsureSuccessStatusCode();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<DepositDto>> GetDeposits()
        {
            var query = _context.Deposits.Include(d => d.Bank).Include(d => d.DepositVariations).Where(d => d.IsActive == true);

            return await _mapper.ProjectTo<DepositDto>(query).ToListAsync();
        }
    }
}