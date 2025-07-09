using HIV_CARE.Repositories.ThienTTT.ModelExtension;
using HIV_CARE.Repositories.ThienTTT.Models;
using HIV_CARE.Services.ThienTTT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

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
        
        [EnableQuery]
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

        // Search 
        [HttpGet("{id}/{date}/{doctorId}")]
        public async Task<List<AppointmentThienTtt>> Get(int id, DateTime date, int doctorId)
        {
            return await _appointmentThienTttService.SearchAsync(id, date, doctorId);
        }

        // Search with paging only
        [HttpGet("{currentPage}/{pageSize}")]
        public async Task<PaginationResult<List<AppointmentThienTtt>>> Get(int currentPage, int pageSize)
        {
            return await _appointmentThienTttService.SearchAsync(currentPage, pageSize);
        }

        // Search with id, date, doctorId and paging
        [HttpGet("{id}/{date}/{doctorId}/{currentPage}/{pageSize}")]
        public async Task<PaginationResult<List<AppointmentThienTtt>>> Get(int id, DateTime date, int doctorId, int currentPage, int pageSize)
        {
            return await _appointmentThienTttService.SearchAsync(id, date, doctorId, currentPage, pageSize);
        }

        // NEW: Search with body request and paging
        [HttpPost("Search")]
        public async Task<PaginationResult<List<AppointmentThienTtt>>> GetWithRequest(SearchAppointmentThienTttRequest request)
        {
            return await _appointmentThienTttService.SearchWithRequestAsync(request);
        }

        // GET: api/AppointmentThienTTTs
        [HttpGet("Search1")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<AppointmentThienTtt>>> Search1()
        {
            var appointments = await _appointmentThienTttService.GetAllAsync();
            return Ok(appointments);
        }
    }
}