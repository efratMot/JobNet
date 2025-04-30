using AutoMapper;
using JobNet.Core.DTOs;
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
    //[Authorize]

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
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> Get()
        {
            var employers = await _employerService.GetAllAsync();
            var employersDto=_mapper.Map<IEnumerable<EmployerDto>>(employers);
            return Ok(employersDto);
        }

        // GET api/<EmployersController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "manager, employer")]
        public ActionResult Get(int id)
        {
            var employer = _employerService.Get(id);
            if (employer == null)
            {
                return NotFound();
            }
            var employerDto=_mapper.Map<EmployerDto>(employer);
            return Ok(employerDto);
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
        [HttpPut("{id}")]
        [Authorize(Roles = "manager, employer")]
        public async Task<ActionResult> Put(int id, [FromBody] EmployerPutModel employer)
        {
            var emp = _mapper.Map<Employer>(employer);
            var e = await _employerService.UpdateAsync(emp,id);
            var em= _mapper.Map<EmployerDto>(e);
            return Ok(em);
        }

        //// DELETE api/<EmployersController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "manager, employer")]
        public async Task<ActionResult> Delete(int id)
        {
           var employer= await _employerService.DeleteAsync(id);
            return Ok(employer);
        }

    }
}
