using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Arival.Domain.Model;

namespace Arival.Infrastructure.Persistance.EFMapper
{
    public class UserOtpMap : IEntityTypeConfiguration<UserOtp>
    {
        public void Configure(EntityTypeBuilder<UserOtp> builder)
        {
            builder.ToTable(typeof(UserOtp).Name);
            builder.HasKey(key => key.Id);
        }
    }
}
