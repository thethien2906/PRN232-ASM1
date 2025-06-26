using HIV_CARE.Repositories.ThienTTT.Basic;
using HIV_CARE.Repositories.ThienTTT.DBContext;
using HIV_CARE.Repositories.ThienTTT.ModelExtension;
using HIV_CARE.Repositories.ThienTTT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIV_CARE.Repositories.ThienTTT
{
    public class AppointmentThienTttRepository : GenericRepository<AppointmentThienTtt>
    {
        public AppointmentThienTttRepository() => _context ??= new DBContext.SU25_PRN232_SE1725_G4_HIVcareContext();

        public AppointmentThienTttRepository(SU25_PRN232_SE1725_G4_HIVcareContext context) => _context = context;

        public async Task<List<AppointmentThienTtt>> GetAllAsync()
        {
            var visits = await _context.AppointmentThienTtts
                .Include(v => v.DoctorsPhatNh)
                .ToListAsync();

            return visits ?? new List<AppointmentThienTtt>();
        }
        public async Task<AppointmentThienTtt> GetByIdAsync(int id)
        {
            var visit = await _context.AppointmentThienTtts
                .Include(v => v.DoctorsPhatNh)
                .FirstOrDefaultAsync(v => v.AppointmentsThienTttid == id);

            return visit ?? new AppointmentThienTtt();
        }
        public async Task<List<AppointmentThienTtt>> SearchAsync(int? appointmentId, DateTime? date, int? doctorId)
        {
            var query = _context.AppointmentThienTtts
                .Include(v => v.DoctorsPhatNh)
                .AsQueryable();
            if (appointmentId != null)
            {
                query = query.Where(v => v.AppointmentsThienTttid == appointmentId);
            }
            if (date != null)
            {
                query = query.Where(v => v.AppointmentDate == date);
            }
            if (doctorId != null)
            {
                query = query.Where(v => v.DoctorsPhatNh.DoctorsPhatNhid == doctorId);
            }

            return await query.ToListAsync();
        }
        public async Task<PaginationResult<List<AppointmentThienTtt>>> GetAllAsync(int currentPage, int pageSize)
        {
            var pageMedical = await this.GetAllAsync();

            //// Paging
            var totalItems = pageMedical.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            pageMedical = pageMedical.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<AppointmentThienTtt>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = pageMedical
            };

            return result;

        }
        public async Task<List<AppointmentThienTtt>> SearchAsync(int id, DateTime date, int doctorId, int currentPage, int pageSize)
        {
            var query = _context.AppointmentThienTtts
                .Include(v => v.DoctorsPhatNh)
                .AsQueryable();
            if (id != null)
            {
                query = query.Where(v => v.AppointmentsThienTttid == id);
            }
            if (date != null)
            {
                query = query.Where(v => v.AppointmentDate == date);
            }
            if (doctorId != null)
            {
                query = query.Where(v => v.DoctorsPhatNh.DoctorsPhatNhid == doctorId);
            }

            return await query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

    }

}
