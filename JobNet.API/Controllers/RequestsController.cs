using JobNet.Core.Entities;
using JobNet.Core.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {

        private readonly IRequestService _requestService;

        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // GET: api/<RequestsController>
        [HttpGet]
        public IEnumerable<Request> Get()
        {
            return _requestService.GetList();
        }

        // GET api/<RequestsController>/5
        [HttpGet("{id}")]
        public Request Get(int id)
        {
            int index = _requestService.GetList().FindIndex(x => x.RequestID == id);
            return _requestService.GetList()[index];
        }

        // POST api/<RequestsController>
        [HttpPost]
        public Request Post([FromBody] Request value)
        {
            _requestService.GetList().Add(value);
            return value;
        }

        // PUT api/<RequestsController>/5
        [HttpPut("{id}")]
        public Request Put(int id, [FromBody] Request value)
        {
            int index= _requestService.GetList().FindIndex(x => x.RequestID == id);
            _requestService.GetList()[index].RequestID = value.RequestID;
            _requestService.GetList()[index].JobID = value.JobID;
            _requestService.GetList()[index].UserID = value.UserID;
            _requestService.GetList()[index].Message = value.Message;
            _requestService.GetList()[index].RequestDate = value.RequestDate;
            return _requestService.GetList()[index];
        }

        // DELETE api/<RequestsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var index = _requestService.GetList().FindIndex(e => e.RequestID == id);
            _requestService.GetList().Remove(_requestService.GetList()[index]);
        }
    }
}
