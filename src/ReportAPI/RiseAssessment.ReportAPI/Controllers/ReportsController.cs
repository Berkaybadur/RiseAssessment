using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiseAssesment.Infrastructure.CQRS.Commands.Request;
using RiseAssesment.Infrastructure.CQRS.Queries.Request;
using RiseAssessment.Manager.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseAssessment.ReportAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsManager _reportsManager;
        public ReportsController(IReportsManager reportsManager)
        {
            _reportsManager = reportsManager;
        }
        /// <summary>
        /// List
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListReportsQueryRequest requestModel)
        {
            var result = await _reportsManager.GetAllReportsAsync(requestModel);
            if (result == null || !result.Any())
                return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var requestModel = new GetReportsQueryRequest
            {
                Id = id
            };

            var result = await _reportsManager.GetReportsAsync(requestModel);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
        /// Post
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateReportsCommandRequest requestModel)
        {
            var result = await _reportsManager.CreateReportsAsync(requestModel);

            return StatusCode(201, result);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateReportsCommandRequest requestModel)
        {
            var result = await _reportsManager.UpdateReportsAsync(requestModel);
            if (result == null)
                return NotFound();

            return Ok();
        }
    }
}
