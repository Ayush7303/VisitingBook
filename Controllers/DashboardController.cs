using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VisitingBook.Services;
using VisitingBook.Models;
using Dapper;

namespace VisitingBook.Controllers
{
    // [Route("[controller]")]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string countOfVisitQuery = @"SELECT v.[Status] AS Name, COUNT(v.[VisitID]) AS Count FROM [dbo].[VisitMaster] v GROUP BY v.[Status]";
            var countOfVisit=DapperORM<object>.ReturnCount(countOfVisitQuery);
            var totalVisits = countOfVisit.Values.Sum();
            var scheduledVisits = countOfVisit.ContainsKey("Scheduled") ? countOfVisit["Scheduled"] : 0;
            var pendingVisits = countOfVisit.ContainsKey("Pending") ? countOfVisit["Pending"] : 0;
            string employeesCountQuery = "SELECT * FROM [dbo].[UserProfile]";
            var employeesList = DapperORM<UserProfile>.ReturnList(employeesCountQuery,null);
            var employeeCountValue = employeesList.Count();

            ViewBag.TotalVisits = totalVisits;
            ViewBag.ScheduledVisits = scheduledVisits;
            ViewBag.PendingVisits = pendingVisits;
            ViewBag.EmployeesCount = employeeCountValue;
            return View();
        }
        [HttpGet]
public IActionResult GetEmployeeCount()
{
    string employeesCountQuery = "SELECT * FROM [dbo].[UserProfile]";
            var employeesList = DapperORM<UserProfile>.ReturnList(employeesCountQuery,null);
            var employeeCountValue = employeesList.Count();
    return Json(new { count = employeeCountValue });
}
        [HttpGet]
        public IActionResult GetVisitCount()
        {
            string countOfVisitQuery = @"SELECT v.[Status] AS Name, COUNT(v.[VisitID]) AS Count FROM [dbo].[VisitMaster] v GROUP BY v.[Status]";
            var countOfVisit=DapperORM<object>.ReturnCount(countOfVisitQuery);
            var totalVisits = countOfVisit.Values.Sum();

            return Json(new {count = totalVisits});
        }
        [HttpGet]
        public IActionResult GetPendingVisitCount()
        {
            string countOfVisitQuery = @"SELECT v.[Status] AS Name, COUNT(v.[VisitID]) AS Count FROM [dbo].[VisitMaster] v GROUP BY v.[Status]";
            var countOfVisit=DapperORM<object>.ReturnCount(countOfVisitQuery);
                      
                       var pendingVisits = countOfVisit.ContainsKey("Pending") ? countOfVisit["Pending"] : 0;
            return Json(new {count = pendingVisits});
        }
        [HttpGet]
        public IActionResult GetScheduledVisitCount()
        {
            string countOfVisitQuery = @"SELECT v.[Status] AS Name, COUNT(v.[VisitID]) AS Count FROM [dbo].[VisitMaster] v GROUP BY v.[Status]";
            var countOfVisit=DapperORM<object>.ReturnCount(countOfVisitQuery);
            var scheduledVisits = countOfVisit.ContainsKey("Scheduled") ? countOfVisit["Scheduled"] : 0;
            return Json(new {count = scheduledVisits});
        }
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}