using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            Console.WriteLine("LoginForUser" + LoginForUser);
            string EmailID = LoginForUser.Split(',')[0];
            string Password = formcollection["Password"].ToString();

            if (string.IsNullOrEmpty(LoginForUser))
            {
                ViewBag.LoginForUser = "EmailID is required.";
                return View();
            }

            if (string.IsNullOrEmpty(Password))
            {
                ViewBag.Password = "Password is required.";
                return View();
            }
            var data = new
            {
                EmailID = EmailID,
                Password = Password
            };

            string jsonObj = JsonConvert.SerializeObject(data);

            using (HttpClient httpClient = new HttpClient())
            {
                var content = new StringContent(jsonObj, Encoding.UTF8, @"application/json");
                string apiRespone;
                using (var respone = await httpClient.PostAsync("http://edusprinttools.edusprint.in/ProjectManagement/Login/VerifyEmailPassword/", content))
                {
                    apiRespone = await respone.Content.ReadAsStringAsync();
                    Console.WriteLine(apiRespone);
                }
                if (apiRespone == "1")
                {
                    Common CurrentSession = Common.NewObj(Request, Response);
                    CurrentSession.SetSession("EmailID", EmailID);
                    var disp = CurrentSession.GetSession("EmailID");
                    HttpContext.Session.SetString("Name", disp.Split('@')[0].Replace('.', ' ').ToUpper());
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Email and Password";
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetOTP()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetOTP(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Message = "Please provide an email address.";
                return View(); // Return to the form if email is empty.
            }

            // Generate a 4-digit OTP
            var otp = new Random().Next(1000, 9999).ToString();
            TempData["OTP"] = otp;

            // Send the OTP to the provided email
            const string subject = "Your OTP Code";
            string body = $"Your OTP code is {otp}. Please use this to proceed.";
            bool isEmailSent = SendMailService.SendEmail(email, subject,body);

            if (isEmailSent)
            {
                ViewBag.Message = $"OTP has been sent to {email}. Please check your inbox.";
            }
            else
            {
                ViewBag.Message = "Failed to send OTP. Please try again.";
            }

            return View(); // Return the same view after processing
        }

        // Method to send OTP via email
        // private bool SendEmailWithOTP(string email, string otp)
        // {
        //     try
        //     {
        //         var fromAddress = new MailAddress("crimerakshak2023@gmail.com", "VisitingBook");
        //         var toAddress = new MailAddress(email);
        //         const string fromPassword = "ufekwglofxnvwbfv"; // Use environment variables for security
        //         const string subject = "Your OTP Code";
        //         string body = $"Your OTP code is {otp}. Please use this to proceed.";

        //         var smtp = new SmtpClient
        //         {
        //             Host = "smtp.gmail.com", // Your SMTP server
        //             Port = 587, // Or another port depending on your email provider
        //             EnableSsl = true,
        //             DeliveryMethod = SmtpDeliveryMethod.Network,
        //             UseDefaultCredentials = false,
        //             Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        //         };
        //         using (var message = new MailMessage(fromAddress, toAddress)
        //         {
        //             Subject = subject,
        //             Body = body
        //         })
        //         {
        //             smtp.Send(message);
        //         }
        //         return true;
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine("Exception email " + ex);
        //         return false;
        //     }
        // }

        [HttpGet]
        public IActionResult VerifyPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerifyPassword(string otp1, string otp2, string otp3, string otp4)
        {
            // Combine the individual OTP inputs into one string
            string enteredOtp = $"{otp1}{otp2}{otp3}{otp4}";

            // Retrieve the OTP stored in TempData
            string? sentOtp = TempData["OTP"] as string;

            if (enteredOtp == sentOtp)
            {
                // OTP is correct, redirect to success page or next step
                return RedirectToAction("SetPassword");
            }
            else
            {
                // OTP is incorrect, return to the same page with an error message
                ViewBag.Message = "The OTP you entered is incorrect. Please try again.";
                return RedirectToAction("GetOTP");
            }
        }

        [HttpGet]
        public IActionResult SetPassword()
        {
            return View();
        }
    }
}


