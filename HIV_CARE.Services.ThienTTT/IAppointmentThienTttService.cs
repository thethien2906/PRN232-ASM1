using HIV_CARE.Repositories.ThienTTT.ModelExtension;
using HIV_CARE.Repositories.ThienTTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIV_CARE.Services.ThienTTT
{
    public interface IAppointmentThienTttService
    {
        Task<List<AppointmentThienTtt>> GetAllAsync();
        Task<AppointmentThienTtt> GetByIdAsync(int id);
        Task<PaginationResult<List<AppointmentThienTtt>>> SearchAsync(int currentPage, int pageSize);
        Task<List<AppointmentThienTtt>> SearchAsync(int id, DateTime date, int doctorId);
        Task<PaginationResult<List<AppointmentThienTtt>>> SearchAsync(int id, DateTime date, int doctorId, int currentPage, int pageSize);
        Task<PaginationResult<List<AppointmentThienTtt>>> SearchWithRequestAsync(SearchAppointmentThienTttRequest request);
        Task<int> CreateAsync(AppointmentThienTtt AppointmentThienTtt);
        Task<int> UpdateAsync(AppointmentThienTtt AppointmentThienTtt);
        Task<bool> DeleteAsync(int id);
    }
}
