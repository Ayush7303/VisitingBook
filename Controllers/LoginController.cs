using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VisitingBook.Models;
using VisitingBook.Services;

namespace VisitingBook.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(IFormCollection formcollection)
        {
            string LoginForUser = formcollection["LoginForUser"].ToString(); 
            Console.WriteLine("LoginForUser"+LoginForUser);
            string EmailID = LoginForUser.Split(',')[0];
            string Password = formcollection["Password"].ToString();
            var data = new {
                EmailID = EmailID,
                Password = Password
            };

            string jsonObj = JsonConvert.SerializeObject(data);

            using(HttpClient httpClient = new HttpClient())
            {
                var content = new StringContent(jsonObj,Encoding.UTF8,@"application/json"); 
                string apiRespone;
                using(var respone = await httpClient.PostAsync("http://edusprinttools.edusprint.in/ProjectManagement/Login/VerifyEmailPassword/",content))
                {                    
                    apiRespone = await respone.Content.ReadAsStringAsync();
                    Console.WriteLine(apiRespone);
                }
                if (apiRespone == "1")
                {
                    Common c = Common.NewObj(Request,Response);
                    c.SetSession("EmailID",EmailID);
                    var disp = c.GetSession("EmailID");
                    ViewData["SessionEmail"] = disp;
                    Console.Write("session ehjdhejhd " + ViewData["SessionEmail"]);
                    return RedirectToAction("Index","Home");
                }
            }
            return View();
        }
    }
}


