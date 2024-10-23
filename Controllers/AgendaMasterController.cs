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
    public class AgendaMasterController : Controller
    {
        private readonly ILogger<AgendaMasterController> _logger;

        public AgendaMasterController(ILogger<AgendaMasterController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
       [HttpPost]
public async Task<IActionResult> AddAgenda([FromBody] AgendaMaster agenda)
{
    // Validate the model
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // Prepare SQL query for inserting a new agenda
    string sql = @"
        INSERT INTO AgendaMaster(Title, Description, CreatedOn, CreatedBy, UpdatedOn, UpdatedBy)
        VALUES (@Title, @Description, @CreatedOn, @CreatedBy, @UpdatedOn, @UpdatedBy);
    ";

    agenda.CreatedOn = DateTime.UtcNow;
    agenda.CreatedBy = "System"; // Replace with actual user
    agenda.UpdatedOn = DateTime.UtcNow;
    agenda.UpdatedBy = "System"; // Replace with actual user

    // Call the asynchronous AddOrUpdateAsync method
    var result = await DapperORM<AgendaMaster>.AddOrUpdateAsync(sql, agenda, null);

    if (result > 0)
    {
        return Ok(new { message = "Agenda added successfully!" });
    }

    return StatusCode(500, "An error occurred while adding the agenda.");
}
    
        [HttpGet]
        public IActionResult GetAgendaMaster()
{
    string agendas = "SELECT [Title],[Description],[CreatedOn],[CreatedBy],[UpdatedOn],[UpdatedBy],[DeletedOn] FROM [dbo].[AgendaMaster]";
            var agendaList = DapperORM<object>.ReturnList(agendas,null);

    return Json(agendaList);
}


}
}