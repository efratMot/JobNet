using JobNet.Core.Entities;
using JobNet.Core.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }


        // GET: api/<JobsController>
        [HttpGet]
        public IEnumerable<Job> Get()
        {
            
                return _jobService.GetList();
        }

            // GET api/<JobsController>/5
            [HttpGet("{id}")]
            public Job Get(int id)
            {
                int index = _jobService.GetList().FindIndex(x => x.JobID == id);
                return _jobService.GetList()[index];
            }

            // POST api/<JobsController>
            [HttpPost]
            public Job Post([FromBody] Job value)
            {
                _jobService.GetList().Add(value);
                return value;
            }

            // PUT api/<JobsController>/5
            [HttpPut("{id}")]
            public Job Put(int id, [FromBody] Job value)
            {
                int index = _jobService.GetList().FindIndex(x => x.JobID == id);
                _jobService.GetList()[index].JobID = value.JobID;
                _jobService.GetList()[index].EmployerID = value.EmployerID;
                _jobService.GetList()[index].Title = value.Title;
                _jobService.GetList()[index].Description = value.Description;
                _jobService.GetList()[index].Location = value.Location;
                _jobService.GetList()[index].Salary = value.Salary;
                _jobService.GetList()[index].PostedDate = value.PostedDate;
                return _jobService.GetList()[index];
            }

            // DELETE api/<JobsController>/5
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
                var index = _jobService.GetList().FindIndex(e => e.JobID == id);
                _jobService.GetList().Remove(_jobService.GetList()[index]);
            }
    }
}