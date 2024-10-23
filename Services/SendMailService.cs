using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace VisitingBook.Services
{
    public static  class SendMailService
    {
         public static bool SendEmail(string email, string subject,string body)
        {
            try
            {
                var fromAddress = new MailAddress("crimerakshak2023@gmail.com", "VisitingBook");
                var toAddress = new MailAddress(email);
                const string fromPassword = "ufekwglofxnvwbfv"; // Use environment variables for security
                // const string subject = "Your OTP Code";
                // string body = $"Your OTP code is {otp}. Please use this to proceed.";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // Your SMTP server
                    Port = 587, // Or another port depending on your email provider
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception email " + ex);
                return false;
            }
        }
    }
}