using AutoMapper;
using JobNet.Core.DTOs;
using JobNet.Core.Entities;
using JobNet.Core.Services;
using JobNet.Models;
using JobNet.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public SubscriptionsController(ISubscriptionService subscriptionService,UserService userService,IMapper mapper)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
            _mapper = mapper;
        }
        // GET: api/<JobsController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var list= await _subscriptionService.GetAllAsync();
            var listDto = _mapper.Map<IEnumerable<SubscriptionDto>>(list);
            return Ok(listDto);
        }

        // GET api/<JobsController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var subscription = _subscriptionService.Get(id);
            if (subscription == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SubscriptionDto>(subscription));
        }

        // POST api/<JobsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SubscriptionPostModel value)
        {
            //var subscription = _subscriptionService.Get(value.SubscriberID);
            //if (subscription == null)
            //{

            var user = new User { UserName = value.UserName, Password = value.Password, Email = value.Email, Role = eRole.subscription };
            var User = await _userService.AddAsync(user);
            var subscription =  _mapper.Map<Subscription>(value);
            subscription.User = user;
            subscription.UserId = User.UserID;
            var s = await _subscriptionService.AddAsync(subscription);
            return Ok(s);
            //}
            //return Conflict();
        }

        // PUT api/<JobsController>/5
        //[HttpPut("{id}")]
        //public Subscription Put(int id, [FromBody] Subscription value)
        //{
        //    int index = _subscriptionService.GetList().FindIndex(x => x.SubscriberID == id);
        //    _subscriptionService.GetList()[index].SubscriberID = value.SubscriberID;
        //    _subscriptionService.GetList()[index].UserId = value.UserId;
        //    _subscriptionService.GetList()[index].SubscriptionDate = value.SubscriptionDate;
        //    return _subscriptionService.GetList()[index];
        //}

        // DELETE api/<JobsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var subscription = await _subscriptionService.DeleteAsync(id);
            return Ok(subscription);
        }
    }
}
