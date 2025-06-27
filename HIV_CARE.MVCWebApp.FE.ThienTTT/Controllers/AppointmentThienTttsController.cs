using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HIV_CARE.Repositories.ThienTTT.Models;
using Newtonsoft.Json;
using System.Text;

namespace HIV_CARE.MVCWebApp.FE.ThienTTT.Controllers
{
    public class AppointmentThienTttsController : Controller
    {
        private string APIEndPoint = "https://localhost:7094/api/";

        public AppointmentThienTttsController()
        {

        }

        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                using (var response = await httpClient.GetAsync(APIEndPoint + "AppointmentThienTtts"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<AppointmentThienTtt>>(content);

                        if (result != null)
                        {
                            ViewBag.Doctors = await this.GetDoctors();
                            ViewBag.SearchResult = false;
                            return View(result);
                        }
                    }
                }
            }

            ViewBag.Doctors = await this.GetDoctors();
            ViewBag.SearchResult = false;
            return View(new List<AppointmentThienTtt>());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int? appointmentId, DateTime? date, int? doctorId, int currentPage = 1, int pageSize = 10)
        {
            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                var searchRequest = new
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    AppointmentId = appointmentId,
                    Date = date,
                    DoctorId = doctorId
                };

                var json = JsonConvert.SerializeObject(searchRequest);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(APIEndPoint + "AppointmentThienTtts/Search", stringContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<dynamic>(content);

                        var appointments = JsonConvert.DeserializeObject<List<AppointmentThienTtt>>(result.items.ToString());

                        ViewBag.Doctors = await this.GetDoctors();
                        ViewBag.SearchResult = true;
                        ViewBag.TotalItems = (int)result.totalItems;
                        ViewBag.TotalPages = (int)result.totalPages;
                        ViewBag.CurrentPage = (int)result.currentPage;
                        ViewBag.PageSize = (int)result.pageSize;
                        ViewBag.AppointmentId = appointmentId;
                        ViewBag.Date = date;
                        ViewBag.DoctorId = doctorId;

                        return View(appointments);
                    }
                }
            }

            ViewBag.Doctors = await this.GetDoctors();
            ViewBag.SearchResult = false;
            return View(new List<AppointmentThienTtt>());
        }

        // GET: AppointmentThienTtts/Create
        public async Task<IActionResult> Create()
        {
            var termAppointment = new AppointmentThienTtt()
            {
                AppointmentDate = DateTime.Now,
                CreatedBy = HttpContext.Request.Cookies["UserName"] ?? "System",
                Status = "Pending",
                CreatedDate = DateTime.Now
            };

            var docs = await this.GetDoctors();
            ViewData["DoctorsPhatNhid"] = new SelectList(docs, "DoctorsPhatNhid", "LicenseNumber");

            return View(termAppointment);
        }

        public async Task<List<DoctorPhatNh>> GetDoctors()
        {
            var doctors = new List<DoctorPhatNh>();

            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                if (!string.IsNullOrEmpty(tokenString))
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                }

                using (var response = await httpClient.GetAsync(APIEndPoint + "DoctorPhatNhs"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        doctors = JsonConvert.DeserializeObject<List<DoctorPhatNh>>(content);
                    }
                }
            }

            return doctors ?? new List<DoctorPhatNh>();
        }

        // POST: AppointmentThienTtts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentThienTtt appointmentThienTtt)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                    if (!string.IsNullOrEmpty(tokenString))
                    {
                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                    }

                    // Set default values for required fields
                    appointmentThienTtt.CreatedDate = DateTime.Now;
                    appointmentThienTtt.CreatedBy = HttpContext.Request.Cookies["UserName"] ?? "System";
                    // Set default status if empty
                    if (string.IsNullOrEmpty(appointmentThienTtt.Status))
                    {
                        appointmentThienTtt.Status = "Pending";
                    }

                    using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "AppointmentThienTTTs", appointmentThienTtt))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<int>(content);

                            if (result > 0)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            var errorContent = await response.Content.ReadAsStringAsync();
                            ModelState.AddModelError("", "Failed to create appointment: " + errorContent);
                        }
                    }
                }
            }

            // Reload doctors for the dropdown if there's an error
            var doctors = await this.GetDoctors();
            ViewData["DoctorsPhatNhid"] = new SelectList(doctors, "DoctorsPhatNhid", "LicenseNumber", appointmentThienTtt.DoctorsPhatNhid);

            return View(appointmentThienTtt);
        }

        // GET: AppointmentThienTtts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                if (!string.IsNullOrEmpty(tokenString))
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                }

                using (var response = await httpClient.GetAsync(APIEndPoint + "AppointmentThienTTTs/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<AppointmentThienTtt>(content);

                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }

            return NotFound();
        }

        // GET: AppointmentThienTtts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id);
        }

        // POST: AppointmentThienTtts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                if (!string.IsNullOrEmpty(tokenString))
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                }

                using (var response = await httpClient.DeleteAsync(APIEndPoint + "AppointmentThienTTTs/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AppointmentThienTtts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                if (!string.IsNullOrEmpty(tokenString))
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                }

                using (var response = await httpClient.GetAsync(APIEndPoint + "AppointmentThienTTTs/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var appointment = JsonConvert.DeserializeObject<AppointmentThienTtt>(content);

                        if (appointment != null)
                        {
                            var doctors = await this.GetDoctors();
                            ViewData["DoctorsPhatNhid"] = new SelectList(doctors, "DoctorsPhatNhid", "LicenseNumber", appointment.DoctorsPhatNhid);

                            return View(appointment);
                        }
                    }
                }
            }

            return NotFound();
        }

        // POST: AppointmentThienTtts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentThienTtt appointmentThienTtt)
        {
            if (id != appointmentThienTtt.AppointmentsThienTttid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                    if (!string.IsNullOrEmpty(tokenString))
                    {
                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                    }

                    // Set modified values
                    appointmentThienTtt.ModifiedDate = DateTime.Now;
                    appointmentThienTtt.ModifiedBy = HttpContext.Request.Cookies["UserName"] ?? "System";

                    using (var response = await httpClient.PutAsJsonAsync(APIEndPoint + "AppointmentThienTTTs/" + id, appointmentThienTtt))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            var errorContent = await response.Content.ReadAsStringAsync();
                            ModelState.AddModelError("", "Failed to update appointment: " + errorContent);
                        }
                    }
                }
            }

            // Reload doctors for the dropdown if there's an error
            var doctors = await this.GetDoctors();
            ViewData["DoctorsPhatNhid"] = new SelectList(doctors, "DoctorsPhatNhid", "LicenseNumber", appointmentThienTtt.DoctorsPhatNhid);

            return View(appointmentThienTtt);
        }

   

        public async Task<ActionResult> DoctorPhatNHList()
        {
            return View();
        }
    }
}