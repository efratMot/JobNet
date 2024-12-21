using AutoMapper;
using JobNet.Core.DTOs;
using JobNet.Core.Entities;
using JobNet.Core.Services;
using JobNet.Models;
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
        public ActionResult Get()
        {
            var list = _jobService.GetList();
            var listDto = _mapper.Map<IEnumerable<JobDto>>(list);
            return Ok(listDto);
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
        public ActionResult Post([FromBody] JobPostModel value)
        {
            var job = new Job { EmployerID = value.EmployerID, Description = value.Description, Title = value.Title, Location = value.Location, Salary = value.Salary, PostedDate = value.PostedDate };
            //if (job == null)
            //{
                return Ok(_jobService.Add(job));
            //}
            //return Conflict();
        }


        // PUT api/<JobsController>/5
        //[HttpPut("{id}")]
        //    public Job Put(int id, [FromBody] Job value)
        //    {
        //        int index = _jobService.GetList().FindIndex(x => x.JobID == id);
        //        _jobService.GetList()[index].JobID = value.JobID;
        //        _jobService.GetList()[index].EmployerID = value.EmployerID;
        //        _jobService.GetList()[index].Title = value.Title;
        //        _jobService.GetList()[index].Description = value.Description;
        //        _jobService.GetList()[index].Location = value.Location;
        //        _jobService.GetList()[index].Salary = value.Salary;
        //        _jobService.GetList()[index].PostedDate = value.PostedDate;
        //        return _jobService.GetList()[index];
        //    }

        //    // DELETE api/<JobsController>/5
        //    [HttpDelete("{id}")]
        //    public void Delete(int id)
        //    {
        //        var index = _jobService.GetList().FindIndex(e => e.JobID == id);
        //        _jobService.GetList().Remove(_jobService.GetList()[index]);
        //    }
    }
}