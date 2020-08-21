using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Controllers
{ 
    /// <summary>
    /// CompensationController.cs
    /// Contains the endpoints related to Compensations
    /// </summary>
    [Route("api/compensation")]
    public class CompensationController:Controller
    {
        private readonly ICompensationService _compensationService;
        private readonly ILogger<CompensationController> _logger;
        
        /// <summary>
        /// Constructor for CompensationController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="compensationService"></param>
        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _compensationService = compensationService;
            _logger = logger;
            
        }

        /// <summary>
        /// CreateCompensation endpoint:
        /// Post request containing the full compensation object to be created.
        /// </summary>
        /// <param name="compensation">Compensation object to be created/added</param>
        /// <returns>HttpStatusCode.Created on success</returns>
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for employee {compensation.Employee.FirstName} {compensation.Employee.LastName}");

            //set the Id to the EmployeeId
            compensation.Id = compensation.Employee.EmployeeId;

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationByEmployeeId", new { id = compensation.Employee.EmployeeId }, compensation);

        }

        /// <summary>
        /// Get endpoint for Compensation:
        /// Given the EmployeeId GetCompensationByEmployeeId
        /// calls CompensationService GetById to get the object
        /// </summary>
        /// <param name="id">EmployeeId that the compensation object refers to</param>
        /// <returns>
        /// -HttpStatusCode OK with serialized object
        /// -HttpStatusCode NotFound if no result was found
        /// </returns>
        [HttpGet("{id}", Name ="getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(String id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var compensation = _compensationService.GetById(id);

            if(compensation == null)
            {
                return NotFound();
            }

            return Ok(compensation);

        }
    }
}
