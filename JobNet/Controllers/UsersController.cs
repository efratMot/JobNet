using JobNet.Core.Entities;
using JobNet.Core.Services;
using JobNet.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
       
        // GET api/<UsersController>/5
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_userService.GetList());
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult Post([FromBody] UserPostModel value)
        {
            var user = new User {UserName=value.UserName,Password=value.Password, Email = value.Email,Role=value.Role };
            //if (user == null)
            //{
                return Ok(_userService.Add(user));
            //}
            //return Conflict();
        }

        // PUT api/<UsersController>/5
        //[HttpPut("{id}")]
        //public User Put(int id, [FromBody] User value)
        //{
        //    int index = _userService.GetList().FindIndex(x => x.UserID == id);
        //    _userService.GetList()[index].UserName = value.UserName;
        //    _userService.GetList()[index].Password = value.Password;
        //    _userService.GetList()[index].Email = value.Email;
        //    _userService.GetList()[index].Role = value.Role;
        //    return _userService.GetList()[index];
        //}

        // DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    int index = _userService.GetList().FindIndex(x => x.UserID == id);
        //    _userService.GetList().RemoveAt(index);

        //}
    }
}
