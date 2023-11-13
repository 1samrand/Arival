using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arival.Domain.Model;
using Framework.Persistance;

namespace Arival.Domain.IRepositroy
{
    public interface IUserOtpRepository : IRepository<UserOtp>
    {
        void CreateOtp(UserOtp UserOtp);
        int GetCountOfActiveCodes(string PhoneNumber, TimeSpan Expiration);
        bool IsValidVerificationCode(string PhoneNumber, string VerificationCode, TimeSpan Expiration);
    }
}
