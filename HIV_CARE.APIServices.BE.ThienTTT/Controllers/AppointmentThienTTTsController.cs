using HIV_CARE.Repositories.ThienTTT.ModelExtension;
using HIV_CARE.Repositories.ThienTTT.Models;
using HIV_CARE.Services.ThienTTT;
using Microsoft.AspNetCore.Mvc;

namespace HIV_CARE.APIServices.BE.ThienTTT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentThienTTTsController : ControllerBase
    {
        private readonly IAppointmentThienTttService _appointmentThienTttService;

        public AppointmentThienTTTsController(IAppointmentThienTttService appointmentsThienTttService)
        {
            _appointmentThienTttService = appointmentsThienTttService;
        }

        // GET: api/AppointmentThienTTTs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentThienTtt>>> Get()
        {
            var appointments = await _appointmentThienTttService.GetAllAsync();
            return Ok(appointments);
        }

        // GET api/AppointmentThienTTTs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentThienTtt>> Get(int id)
        {
            var appointment = await _appointmentThienTttService.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        // POST api/AppointmentThienTTTs
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] AppointmentThienTtt appointmentThienTtt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _appointmentThienTttService.CreateAsync(appointmentThienTtt);
            return Ok(result);
        }

        // PUT api/AppointmentThienTTTs/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] AppointmentThienTtt appointmentThienTtt)
        {
            if (id != appointmentThienTtt.AppointmentsThienTttid)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _appointmentThienTttService.UpdateAsync(appointmentThienTtt);
            return Ok(result);
        }

        // DELETE api/AppointmentThienTTTs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _appointmentThienTttService.DeleteAsync(id);
            return Ok(result);
        }

        // GET api/AppointmentThienTTTs/search?appointmentId=1&date=2025-06-05&doctorId=1
        [HttpGet("search")]
        public async Task<ActionResult<List<AppointmentThienTtt>>> Search(
            [FromQuery] int? appointmentId = null,
            [FromQuery] DateTime? date = null,
            [FromQuery] int? doctorId = null)
        {
            var results = await _appointmentThienTttService.SearchAsync(appointmentId ?? 0, date ?? DateTime.MinValue, doctorId ?? 0);
            return Ok(results);
        }

        // GET api/AppointmentThienTTTs/paged?currentPage=1&pageSize=10
        [HttpGet("paged")]
        public async Task<ActionResult<List<AppointmentThienTtt>>> GetPaged(
            [FromQuery] int currentPage = 1,
            [FromQuery] int pageSize = 10)
        {
            var results = await _appointmentThienTttService.SearchAsync(currentPage, pageSize);
            return Ok(results);
        }

        // GET api/AppointmentThienTTTs/search-paged?appointmentId=1&date=2025-06-05&doctorId=1&currentPage=1&pageSize=10
        [HttpGet("search-paged")]
        public async Task<ActionResult<List<AppointmentThienTtt>>> SearchPaged(
            [FromQuery] int? appointmentId = null,
            [FromQuery] DateTime? date = null,
            [FromQuery] int? doctorId = null,
            [FromQuery] int currentPage = 1,
            [FromQuery] int pageSize = 10)
        {
            var results = await _appointmentThienTttService.SearchAsync(
                appointmentId ?? 0,
                date ?? DateTime.MinValue,
                doctorId ?? 0,
                currentPage,
                pageSize);
            return Ok(results);
        }

        // POST api/AppointmentThienTTTs/search-request
        [HttpPost("search-request")]
        public async Task<ActionResult<List<AppointmentThienTtt>>> GetWithRequest([FromBody] SearchAppointmentThienTttRequest request)
        {
            return Ok(new List<AppointmentThienTtt>());
        }
    }
}