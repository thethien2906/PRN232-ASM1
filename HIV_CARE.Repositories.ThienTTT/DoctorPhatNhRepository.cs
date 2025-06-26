using HIV_CARE.Repositories.ThienTTT.Basic;
using HIV_CARE.Repositories.ThienTTT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIV_CARE.Repositories.ThienTTT
{
    public class DoctorPhatNhRepository : GenericRepository<DoctorPhatNh>
    {
        public DoctorPhatNhRepository() => _context ??= new DBContext.SU25_PRN232_SE1725_G4_HIVcareContext();
        public DoctorPhatNhRepository(DBContext.SU25_PRN232_SE1725_G4_HIVcareContext context) => _context = context;
    }
}
