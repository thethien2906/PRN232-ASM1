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

        // POST api/DoctorPhatNHs
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] DoctorPhatNh doctorPhatNh)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _doctorPhatNhService.CreateAsync(doctorPhatNh);
            return Ok(result);
        }
    }
}
