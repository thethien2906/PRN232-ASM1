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
            var query = _context.AppointmentThienTtts
                .Include(v => v.DoctorsPhatNh)
                .AsQueryable();

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = await query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PaginationResult<List<AppointmentThienTtt>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = items
            };

            return result;
        }
        public async Task<PaginationResult<List<AppointmentThienTtt>>> SearchAsync(int id, DateTime date, int doctorId, int currentPage, int pageSize)
        {
            var query = _context.AppointmentThienTtts
                .Include(v => v.DoctorsPhatNh)
                .AsQueryable();

            if (id > 0)
            {
                query = query.Where(v => v.AppointmentsThienTttid == id);
            }
            if (date != DateTime.MinValue)
            {
                query = query.Where(v => v.AppointmentDate.Date == date.Date);
            }
            if (doctorId > 0)
            {
                query = query.Where(v => v.DoctorsPhatNhid == doctorId);
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = await query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PaginationResult<List<AppointmentThienTtt>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = items
            };

            return result;
        }
        public async Task<PaginationResult<List<AppointmentThienTtt>>> SearchWithRequestAsync(SearchAppointmentThienTttRequest request)
        {
            var query = _context.AppointmentThienTtts
                .Include(v => v.DoctorsPhatNh)
                .AsQueryable();

            if (request.AppointmentId != null)
            {
                query = query.Where(v => v.AppointmentsThienTttid == request.AppointmentId);
            }
            if (request.Date != null)
            {
                query = query.Where(v => v.AppointmentDate.Date == request.Date.Value.Date);
            }
            if (request.DoctorId != null)
            {
                query = query.Where(v => v.DoctorsPhatNhid == request.DoctorId);
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize.Value);

            var items = await query
                .Skip((request.CurrentPage.Value - 1) * request.PageSize.Value)
                .Take(request.PageSize.Value)
                .ToListAsync();

            var result = new PaginationResult<List<AppointmentThienTtt>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = request.CurrentPage.Value,
                PageSize = request.PageSize.Value,
                Items = items
            };

            return result;
        }
    }

}
