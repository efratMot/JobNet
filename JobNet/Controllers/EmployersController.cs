using AutoMapper;
using JobNet.Core.Entities;
using JobNet.Core.Services;
using JobNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class EmployersController : ControllerBase
    {
        private readonly IEmployerService _employerService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public EmployersController(IEmployerService employerService,IMapper mapper,IUserService userService)
        {
            _userService = userService;
            _employerService = employerService;
            _mapper = mapper;
        }



        // GET: api/<EmployersController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var employers = await _employerService.GetAllAsync();
            return Ok(employers);
        }

        // GET api/<EmployersController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var employer = _employerService.Get(id);
            if (employer == null)
            {
                return NotFound();
            }
            return Ok(employer);
        }

        // POST api/<EmployersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmployerPostModel value)
        {
            var user=new User { UserName = value.UserName,Password=value.Password,Email=value.Email,Role=eRole.employer };
            var User= await _userService.AddAsync(user);
            var employer= _mapper.Map<Employer>(value);
            employer.User = User;
            employer.UserID=User.UserID;
            //if (employer != null)
            //{
            //   return Conflict();
            //}

            var e = await _employerService.AddAsync(employer);
            return Ok(e);
        }

        // PUT api/<EmployersController>/5
        //[HttpPut("{id}")]
        //public Employer Put(int id, [FromBody] Employer value)
        //{
        //    int index = _employerService.GetList().FindIndex(x => x.EmployerID == id);
        //    _employerService.GetList()[index].UserID = value.UserID;
        //    _employerService.GetList()[index].CompanyName = value.CompanyName;
        //    _employerService.GetList()[index].Industry = value.Industry;
        //    return _employerService.GetList()[index];

        //}

        //// DELETE api/<EmployersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
           var employer= await _employerService.DeleteAsync(id);
            return Ok(employer);
        }

    }
}
