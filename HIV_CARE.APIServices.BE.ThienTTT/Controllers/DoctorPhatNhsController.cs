using HIV_CARE.Repositories.ThienTTT.Models;
using HIV_CARE.Services.ThienTTT;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIV_CARE.APIServices.BE.ThienTTT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorPhatNhsController : ControllerBase
    {
        private readonly DoctorPhatNhService _doctorPhatNhService;
        public DoctorPhatNhsController(DoctorPhatNhService doctorPhatNhService) => _doctorPhatNhService = doctorPhatNhService;

        // GET: api/<DoctorNhsController>
        [HttpGet]
        public async Task<IEnumerable<DoctorPhatNh>> Get()
        {
            return await _doctorPhatNhService.GetAllAsync();
        }

        //// GET api/<DoctorNhsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<DoctorNhsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<DoctorNhsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<DoctorNhsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
