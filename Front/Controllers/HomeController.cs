using System;
using System.Collections.Generic;
using IdentityModel.Client;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Front.Services;
using Microsoft.Extensions.Logging;

namespace Front.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly ITestService testService;
        public HomeController(ITestService _testService,
            ILogger<IdentityController> logger)
        {
            testService = _testService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Service(string user)
        {

            //HttpClient httpClient2 = new HttpClient();
            //var dd = User.Identity;
            //var ss1 =  HttpContext.GetTokenAsync("access_token");
            //httpClient2.SetBearerToken(ss1.Result);

            //var response = Task.Run(async () => await httpClient2.GetAsync("https://ws-pc-70:5007/identity"));
            //var ss = response.Result;
            //var ww = Task.Run(async () => await ss.Content.ReadAsStringAsync());
            //var kk = ww.Result;
            _logger.LogInformation($"Call home/service, user - {user}");
            ViewData["Test"] = await testService.TestWebApi(HttpContext,$"identity/{user}");
            return View();

        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult Test()
        {
            HttpClient httpClient2 = new HttpClient();
            var response = Task.Run(async () => await httpClient2.GetAsync("http://ws-pc-70:5001/.well-known/openid-configuration"));
            var ss = response.Result;
            var ww = Task.Run(async () => await ss.Content.ReadAsStringAsync());
            var kk = ww.Result;
            ViewData["Test"] = kk;
            return View("Service");
        }
        [HttpGet("test2")]
        [AllowAnonymous]
        public IActionResult Test2()
        {
            ViewData["Test"] = "test";
            return View("Service");
        }
    }
}
