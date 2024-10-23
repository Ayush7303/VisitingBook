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
            var agenda = DapperORM<AgendaMaster>.ReturnList(sqlQuery, null);
            return View(agenda);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        public IActionResult Details(int AgendaMasterID)
        {
            string sqlQuery = "SELECT * FROM AgendaMaster WHERE AgendMasterID = @AgendaMasterID";
            DynamicParameters param = new DynamicParameters();
            param.Add("@AgendaMasterID", AgendaMasterID);
            var agendaById = DapperORM<AgendaMaster>.ReturnList(sqlQuery, param);
            return View(agendaById);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AgendaMaster agendaMaster)
        {

            agendaMaster.CreatedOn = DateTime.Now;
            agendaMaster.UpdatedOn = DateTime.Now;
            agendaMaster.CreatedBy = TempData["SessionEmail"].ToString();
            agendaMaster.UpdatedBy = TempData["SessionEmail"].ToString();


            string sqlQuery = @"
        INSERT INTO AgendaMaster 
        (Title, Description, CreatedOn, CreatedBy, UpdatedOn, UpdatedBy) 
        VALUES 
        (@Title, @Description, @CreatedOn, @CreatedBy, @UpdatedOn, @UpdatedBy)";


            DapperORM<AgendaMaster>.AddOrUpdate(sqlQuery, agendaMaster);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int AgendaMasterID)
        {
            string sqlQuery = "DELETE FROM AgendaMAster WHERE AgendaMasterID = @AgendaMasterID";
            DynamicParameters param = new DynamicParameters();
            param.Add("@AgendaMasterID", AgendaMasterID);
            DapperORM<AgendaMaster>.ReturnList(sqlQuery, param);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int AgendaMasterID)
        {
            var agendaByID = Details(AgendaMasterID);
            return View(agendaByID);
        }
        [HttpPost]
        public IActionResult Edit(AgendaMaster agendaMaster)
        {
            agendaMaster.UpdatedOn = DateTime.Now;
            agendaMaster.UpdatedBy = TempData["SessionEmail"].ToString();

            string sqlQuery = @"
    UPDATE AgendaMaster 
    SET 
        Title = @Title,
        Description = @Description,
        UpdatedOn = @UpdatedOn,
        UpdatedBy = @UpdatedBy
    WHERE 
        AgendaMasterID = @AgendaMasterID";

            DynamicParameters param = new DynamicParameters();
            param.Add("@AgendaMasterID", agendaMaster.AgendaMasterID);  

            DapperORM<AgendaMaster>.AddOrUpdate(sqlQuery, agendaMaster);  

            return RedirectToAction("Index");
        }
    }
}