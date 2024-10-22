using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VisitingBook.Models;
using VisitingBook.Services;

namespace VisitingBook.Controllers
{
    public class AgendaMasterController : Controller
    {
        private readonly ILogger<AgendaMasterController> _logger;

        public AgendaMasterController(ILogger<AgendaMasterController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string sqlQuery = "SELECT Title,Description from AgendaMaster";
            var agenda = DapperORM<AgendaMaster>.ReturnList(sqlQuery,null);
            return View(agenda);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AgendaMaster agendaMaster)
        {
          
                string sqlQuery = @"
                    INSERT INTO AgendaMaster 
                    (Title, Description, CreatedOn, CreatedBy, UpdatedOn, UpdatedBy) 
                    VALUES 
                    (@Title, @Description, @CreatedOn, @CreatedBy, @UpdatedOn, @UpdatedBy)";

                    string CreatedBy = "Admin";
                    string UpdatedBy = "Admin";
                
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Title", agendaMaster.Title);
                parameters.Add("@Description", agendaMaster.Description);
                parameters.Add("@CreatedOn", DateTime.Now);
                parameters.Add("@CreatedBy",CreatedBy);
                parameters.Add("@UpdatedOn", DateTime.Now);
                parameters.Add("@UpdatedBy", UpdatedBy);

                // Execute the query using DapperORM
                DapperORM<AgendaMaster>.AddOrUpdate(sqlQuery, null, parameters);

                // Redirect to Index or return a success message
                return RedirectToAction("Index");

        }
    }
}