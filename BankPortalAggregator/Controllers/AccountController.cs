using System;
using System.Threading.Tasks;
using AutoMapper;
using BankPortalAggregator.Models;
using BankPortalAggregator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BankPortalAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly BankContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AccountController(BankContext context, IConfiguration configuration, IMapper mapper, IUserService userService)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [Route("Test")]
        public async Task<ActionResult> Test([FromBody] GoogleToken accessToken)
        {
            //TokenHelper tokenHelper = new TokenHelper(_configuration)
            //{
            //    Context = _context
            //};

            try
            {
                //GoogleJsonWebSignature.Payload tokenInfo = await tokenHelper.Validate(accessToken.AccessToken);
                //string serverAccessToken = tokenHelper.GetJwtToken(tokenInfo);

                //return Ok(serverAccessToken);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] GoogleToken accessToken)
        {
            try
            {
                var user = await _userService.Authenticate(accessToken.Code);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("loginTest")]
        public ActionResult LoginTest([FromBody]string accessToken)
        {
            return Ok(accessToken);
        }

        [HttpGet]
        [Route("/GoogleCallBack")]
        public ActionResult SignInGoogle()
        {
            return Ok();
        }
    }

    public class GoogleToken
    {
        public string AccessToken { get; set; }
        public string Code { get; set; }
    }
}
