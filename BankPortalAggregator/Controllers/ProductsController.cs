using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BankPortalAggregator.Models;
using BankPortalAggregator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BankPortalAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly BankContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ProductsController(BankContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        [Route("token")]
        public ActionResult token()
        {
            var obj = new
            {
                accessToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjVkODg3ZjI2Y2UzMjU3N2M0YjVhOGExZTFhNTJlMTlkMzAxZjgxODEiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJhY2NvdW50cy5nb29nbGUuY29tIiwiYXpwIjoiNDE5OTQwODYxMTAwLTRsaGVrNzBkMHI0ZDI5YXRwN2FobGkwcW10OHVxb212LmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwiYXVkIjoiNDE5OTQwODYxMTAwLTRsaGVrNzBkMHI0ZDI5YXRwN2FobGkwcW10OHVxb212LmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwic3ViIjoiMTE4MTg4NDExNzI1MTkyODMwOTkzIiwiZW1haWwiOiJjdGFuaW4yQGdtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJhdF9oYXNoIjoic0EwTXAydEhWLUJSZUlwRkhvdHVzdyIsIm5hbWUiOiJJc2thbmRlciBLaGFraW16aGFub3YiLCJwaWN0dXJlIjoiaHR0cHM6Ly9saDMuZ29vZ2xldXNlcmNvbnRlbnQuY29tLy1haFF4Q3BzTlVUdy9BQUFBQUFBQUFBSS9BQUFBQUFBQUFBQS9BQ0hpM3JmYmNXbFFYdUZhS3Q2REJXZHF2WjdtdGt4ZnNnL3M5Ni1jL3Bob3RvLmpwZyIsImdpdmVuX25hbWUiOiJJc2thbmRlciIsImZhbWlseV9uYW1lIjoiS2hha2ltemhhbm92IiwibG9jYWxlIjoicnUiLCJpYXQiOjE1NTYzODUyMDMsImV4cCI6MTU1NjM4ODgwMywianRpIjoiYTY2YWNhYTczYjkwMzE5M2UyMDI5OWFhM2NmMjRiMTJlOWFmODhiYiJ9.N6MvCLZ2W9qq9OIaN_apmh4igtXuK1ayp69U-tgrD47m4cr0I-YaNQAlKsRRjHQj2tisjlijMkG6UgKr9-xjSAc7-C8jQkcMGaBjkhsOiMm-DpByjQqOxcTDKQCw9uSpKjd_2k-OClWLS9_KjJfGTjgTVK5Cvz7xIz4ph2YW7X8ad-x7YSZ29srIXkW7US3MiW8ILx2iOutqghVhIO3qDXFSHPiqaYMY7MP1qenm9sSsj2peMrI_6wPoCeXbq3xdTkOmOd79UjyfrWpb9N44BWxu51fQOtEacOxyW6FzS9mcSpCIGgiktlGDzTzS1WHkK901GFfzFx7L1dKbUfj_vw"
            };
            return Ok(obj);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetAuthorizeResource")]
        public async Task<ActionResult> GetAuthorizeResource([FromBody] DepositDto product)
        {
            try
            {
                var userId = HttpContext.User.Claims.Where(c => c.Type == "Sub").SingleOrDefault().Value;

                if (userId == null)
                {
                    return BadRequest();
                }

                var user = _userService.GetById(int.Parse(userId));

                var endpoint = _context.Endpoints.Join(_context.Deposits,
                    e => e.BankId,
                    d => d.BankId,
                    (e, d) => new
                    {
                        Endpoint = e.EndpointUrl,
                        DepositId = d.Id,
                        BankDepositId = d.BankDepositId
                    })
                    .Where(e => e.DepositId == product.Id).FirstOrDefault();

                HttpClient client = new HttpClient();

                var json = JsonConvert.SerializeObject(new
                {
                    ProductId = endpoint.BankDepositId.Value.ToString()
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("tokens", user.AccessToken + "," + user.IdToken);

                string path = endpoint.Endpoint + "Product";
                HttpResponseMessage response = await client.PostAsync(path, content);

                if (response.IsSuccessStatusCode)
                {

                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IEnumerable<DepositDto>> GetDeposits()
        {
            var query = _context.Deposits.Include(d => d.Bank).Where(d => d.IsActive == true);
            return await _mapper.ProjectTo<DepositDto>(query).ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeposit([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deposit = await _context.Deposits.FindAsync(id);

            if (deposit == null)
            {
                return NotFound();
            }

            return Ok(deposit);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeposit([FromRoute] int id, [FromBody] Deposit deposit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deposit.Id)
            {
                return BadRequest();
            }

            _context.Entry(deposit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepositExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostDeposit([FromBody] Deposit deposit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Deposits.Add(deposit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeposit", new { id = deposit.Id }, deposit);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeposit([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deposit = await _context.Deposits.FindAsync(id);
            if (deposit == null)
            {
                return NotFound();
            }

            _context.Deposits.Remove(deposit);
            await _context.SaveChangesAsync();

            return Ok(deposit);
        }

        private bool DepositExists(int id)
        {
            return _context.Deposits.Any(e => e.Id == id);
        }
    }
}