using Arival.Domain.IRepositroy;
using Arival.Domain.Model;
using Framework.Persistance;
using System;
using System.Linq;

namespace Arival.Infrastructure.Persistance.Repository
{
    public class UserOtpRepository : EFRepositoryBase<ArivalDbContext, UserOtp>, IUserOtpRepository
    {
        public UserOtpRepository(ArivalDbContext context) : base(context)
        {
        }
        public void CreateOtp(UserOtp UserOtp)
        {
            Insert(UserOtp);
            Commit();
        }

        public int GetCountOfActiveCodes(string PhoneNumber,TimeSpan Expiration)
        {
            return _dbContext.Set<UserOtp>()
                .Count(a => a.PhoneNumber == PhoneNumber &&
                            (DateTime.Now - a.CreatedTime).TotalSeconds < Expiration.TotalSeconds);

        }

        public bool IsValidVerificationCode(string PhoneNumber,string VerificationCode, TimeSpan Expiration)
        {
            return _dbContext.Set<UserOtp>()
                .Any(a => a.PhoneNumber == PhoneNumber && a.Otp == VerificationCode &&
                       (DateTime.Now - a.CreatedTime).TotalSeconds < Expiration.TotalSeconds);

        }
    }
}
