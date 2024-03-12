using Application.Jobs;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class JobController : WorklogControllerBase 
    {
        public JobController(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            return Ok(await Mediator.Send(new List.Query()));
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetJob(Guid id)
        {
            var result = await Mediator.Send(new Details.Query() { Id = id});

            if(result == null) return NotFound(result);

            return Ok(result); 
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateJob(Guid id, Job job)
        {
            var result = await Mediator.Send(new Update.Command() { Id = id, Job = job});
            
            if(result == -1) return NotFound(result);
            if(result == 0) return BadRequest("Client Error");

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            var result = await Mediator.Send(new Delete.Command() { Id = id});
            
            if(result == -1) return NotFound(result);
            if(result == 0) return BadRequest("Client Error");

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(Job job)
        {
            var result = await Mediator.Send(new Create.Command() { Job = job });
            
            if(result == -1) return NotFound(result);
            if(result == 0) return BadRequest("Client Error");

            return NoContent();
        }
    }
}