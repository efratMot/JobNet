using JobNet.Core.Entities;
using JobNet.Core.Services;
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
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userService.GetList();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            int index = _userService.GetList().FindIndex(x => x.UserID == id);
            return _userService.GetList()[index];
        }

        // POST api/<UsersController>
        [HttpPost]
        public User Post([FromBody] User value)
        {
            _userService.GetList().Add(value);
            return value;
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public User Put(int id, [FromBody] User value)
        {
            int index = _userService.GetList().FindIndex(x => x.UserID == id);
            _userService.GetList()[index].UserName = value.UserName;
            _userService.GetList()[index].Password = value.Password;
            _userService.GetList()[index].Email = value.Email;
            _userService.GetList()[index].Role = value.Role;
            return _userService.GetList()[index];
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            int index = _userService.GetList().FindIndex(x => x.UserID == id);
            _userService.GetList().RemoveAt(index);

        }
    }
}
