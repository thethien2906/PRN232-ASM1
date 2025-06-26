using HIV_CARE.Repositories.ThienTTT.Basic;
using HIV_CARE.Repositories.ThienTTT.DBContext;
using HIV_CARE.Repositories.ThienTTT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIV_CARE.Repositories.ThienTTT
{
    public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
    {
        public SystemUserAccountRepository() => _context ??= new DBContext.SU25_PRN232_SE1725_G4_HIVcareContext();
        public SystemUserAccountRepository(SU25_PRN232_SE1725_G4_HIVcareContext context) => _context = context;

        public async Task<SystemUserAccount> GetUserAccountAsync(string username, string password)
        {
            return await _context.SystemUserAccounts
                .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

            //return await _context.SystemUserAccounts
            //    .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password
            //    && u.IsActive == true);

            //return await _context.SystemUserAccounts
            //    .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password
            //    && u.IsActive == true);

            //return await _context.SystemUserAccounts
            //    .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password
            //    && u.IsActive == true);
        }
    }
}
