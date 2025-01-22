﻿using AutoMapper;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
       
        // GET api/<UsersController>/5
        [HttpGet]
        public async Task< ActionResult> Get()
        {
            var user = await _userService.GetAllAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(user);
            return Ok(usersDto);
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
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserPostModel value)
        {
            //var user = _userService.Get(value.UserID);
            //if (user == null)
            //{
            var user = _mapper.Map<User>(value);
            var u = await _userService.AddAsync(user);
            return Ok(u);
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
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userService.DeleteAsync(id);
            return Ok(user);

        }
    }
}
