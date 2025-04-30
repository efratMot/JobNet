using AutoMapper;
using JobNet.Core.DTOs;
using JobNet.Core.Entities;
using JobNet.Core.Services;
using JobNet.Models;
using JobNet.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;

        public JobsController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }


        // GET: api/<JobsController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var jobs = await _jobService.GetAllAsync();
            var jobsDto = _mapper.Map<IEnumerable<JobDto>>(jobs);
            return Ok(jobsDto);
        }

        // GET api/<JobsController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var job = _jobService.Get(id);
            if (job == null)
            {
                return NotFound();
            }
            var jobDto = _mapper.Map<JobDto>(job);
            return Ok(jobDto);
        }

        // POST api/<JobsController>
        [HttpPost]
        [Authorize(Roles = "employer")]
        public async Task<ActionResult> Post([FromBody] JobPostModel value)
        {
            var job=_mapper.Map<Job>(value);
            //if (job == null)
            //{
            var j = await _jobService.AddAsync(job);
            return Ok(j);
           // return Ok(_jobService.Add(job));
            //}
            //return Conflict();
        }


        // PUT api/<JobsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "manager, employer")]
        public async Task<ActionResult> Put(int id, [FromBody] JobPutModel value)
        {
            var job = _mapper.Map<Job>(value);
            var j = await _jobService.UpdateAsync(job, id);
            return Ok(j);
        }

        //    // DELETE api/<JobsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "manager, employer")]
        public async Task<ActionResult> Delete(int id)
        {
            var job = await _jobService.DeleteAsync(id);
            return Ok(job);
        }
    }
}