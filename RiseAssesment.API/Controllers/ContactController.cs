using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiseAssesment.Infrastructure.CQRS.Commands.Request;
using RiseAssesment.Infrastructure.CQRS.Queries;
using RiseAssesment.Infrastructure.CQRS.Queries.Request;
using RiseAssessment.Manager.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseAssesment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactManager _contactManager;
        public ContactController(IContactManager contactManager)
        {
            _contactManager = contactManager;
        }
        /// <summary>
        /// List
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListContactQueryRequest requestModel)
        {
            var result = await _contactManager.GetAllContactAsync(requestModel);
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
            var requestModel = new GetContactQueryRequest
            {
                Id = id
            };

            var result = await _contactManager.GetContactAsync(requestModel);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
        /// Post
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateContactCommandRequest requestModel)
        {
            var result = await _contactManager.CreateContactAsync(requestModel);
            return StatusCode(201, result);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateContactCommandRequest requestModel)
        {
            var result = await _contactManager.UpdateContactAsync(requestModel);
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
            var requestModel = new DeleteContactCommandRequest
            {
                Id = id
            };
            var result = await _contactManager.DeleteContactAsync(requestModel);
            if (result == null)
                return NotFound();

            return Ok();
        }
    }
}
