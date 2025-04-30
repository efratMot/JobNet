using AutoMapper;
using JobNet.Core.DTOs;
using JobNet.Core.Entities;
using JobNet.Core.Services;
using JobNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService requestService,IMapper mapper)
        {
            _requestService = requestService;
            _mapper = mapper;
        }

        // GET: api/<RequestsController>
        [HttpGet]
        [Authorize(Roles = "employer")]
        public async Task<ActionResult> Get()
        {
            var requests = await _requestService.GetAllAsync();
            var requestsDto = _mapper.Map<IEnumerable<RequestDto>>(requests);
            return Ok(requestsDto);
        }

        // GET api/<RequestsController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "employer")]
        public ActionResult Get(int id)
        {
            var request = _requestService.Get(id);
            if (request == null)
            {
                return NotFound();
            }
            var requestDto=_mapper.Map<RequestDto>(request);
            return Ok(requestDto);
        }

        // POST api/<RequestsController>
        [HttpPost]
        [Authorize(Roles = "subscription")]
        public async Task<ActionResult> Post([FromBody] RequestPostModel value)
        {
            //var request = _requestService.Get(value.RequestID);
            //if (request == null)
            //{
            var request = _mapper.Map<Request>(value);
            var r = await _requestService.AddAsync(request);
            return Ok(r);
            //}
            //return Conflict();
        }

        // PUT api/<RequestsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "subscription")]
        public async Task<ActionResult> Put(int id, [FromBody] RequestPostModel request)
        {
            var req = _mapper.Map<Request>(request);
            var r = await _requestService.UpdateAsync(req, id);
            return Ok(r);
        }

        //// DELETE api/<RequestsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "subscription, employer")]
        public async Task<ActionResult> Delete(int id)
        {
            var request = await _requestService.DeleteAsync(id);
            return Ok(request);
        }
    }
}
