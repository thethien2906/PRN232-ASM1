using HIV_CARE.Repositories.ThienTTT.Models;
using HIV_CARE.Repositories.ThienTTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIV_CARE.Repositories.ThienTTT.ModelExtension;

namespace HIV_CARE.Services.ThienTTT
{
    public class AppointmentThienTttService : IAppointmentThienTttService
    {
        private readonly AppointmentThienTttRepository _appointmentThienTttRepository;

        public AppointmentThienTttService() => _appointmentThienTttRepository ??=
        new AppointmentThienTttRepository();

        public async Task<int> CreateAsync(AppointmentThienTtt appointmentThienTtt)
        {
            return await _appointmentThienTttRepository.CreateAsync(appointmentThienTtt);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var termAppointment = await _appointmentThienTttRepository.GetByIdAsync(id);
            if (termAppointment != null)
            {
                return await _appointmentThienTttRepository.RemoveAsync(termAppointment);
            }
            return false;
        }

        public async Task<List<AppointmentThienTtt>> GetAllAsync()
        {
            return await _appointmentThienTttRepository.GetAllAsync();
        }

        public async Task<AppointmentThienTtt> GetByIdAsync(int id)
        {
            return await _appointmentThienTttRepository.GetByIdAsync(id);
        }

        public async Task<List<AppointmentThienTtt>> SearchAsync(int id, DateTime date, int doctorId)
        {
            return await _appointmentThienTttRepository.SearchAsync(
                id == 0 ? null : id,
                date == DateTime.MinValue ? null : date,
                doctorId == 0 ? null : doctorId);
        }

        public async Task<PaginationResult<List<AppointmentThienTtt>>> SearchAsync(int currentPage, int pageSize)
        {
            return await _appointmentThienTttRepository.GetAllAsync(currentPage, pageSize);
        }

        public async Task<PaginationResult<List<AppointmentThienTtt>>> SearchAsync(int id, DateTime date, int doctorId, int currentPage, int pageSize)
        {
            return await _appointmentThienTttRepository.SearchAsync(id, date, doctorId, currentPage, pageSize);
        }
        public async Task<PaginationResult<List<AppointmentThienTtt>>> SearchWithRequestAsync(SearchAppointmentThienTttRequest request)
        {
            return await _appointmentThienTttRepository.SearchWithRequestAsync(request);
        }
        public async Task<int> UpdateAsync(AppointmentThienTtt appointmentThienTtt)
        {
            return await _appointmentThienTttRepository.UpdateAsync(appointmentThienTtt);
        }
    }
}