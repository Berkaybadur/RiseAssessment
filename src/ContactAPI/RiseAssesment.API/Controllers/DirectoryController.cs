using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiseAssesment.Infrastructure.CQRS.Commands.Request;
using RiseAssesment.Infrastructure.CQRS.Queries.Request;
using RiseAssessment.Manager.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseAssesment.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DirectoryController : ControllerBase
    {
        private readonly IDirectoryManager _directoryManager;
        public DirectoryController(IDirectoryManager DirectoryManager)
        {
            _directoryManager = DirectoryManager;
        }
        /// <summary>
        /// List
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListDirectoryQueryRequest requestModel)
        {
            var result = await _directoryManager.GetAllDirectoryAsync(requestModel);
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
            var requestModel = new GetDirectoryQueryRequest
            {
                Id = id
            };

            var result = await _directoryManager.GetDirectoryAsync(requestModel);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
        /// Post
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDirectoryCommandRequest requestModel)
        {
            var result = await _directoryManager.CreateDirectoryAsync(requestModel);
            return StatusCode(201, result);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateDirectoryCommandRequest requestModel)
        {
            var result = await _directoryManager.UpdateDirectoryAsync(requestModel);
            if (result == null)
                return NotFound();

            return Ok();
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var requestModel = new DeleteDirectoryCommandRequest
            {
                Id = id
            };
            var result = await _directoryManager.DeleteDirectoryAsync(requestModel);
            if (result == null)
                return NotFound();

            return Ok();
        }
    }
}
