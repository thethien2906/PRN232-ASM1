using HIV_CARE.Repositories.ThienTTT;
using HIV_CARE.Repositories.ThienTTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIV_CARE.Services.ThienTTT
{
    public class SystemUserAccountService
    {
        private readonly SystemUserAccountRepository _repository;

        public SystemUserAccountService() => _repository ??= new SystemUserAccountRepository();


        public async Task<SystemUserAccount> GetUserAccount(string usermame, string password)
        {
            return await _repository.GetUserAccountAsync(usermame, password);
        }
    }
}
