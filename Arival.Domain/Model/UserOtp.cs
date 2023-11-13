using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arival.Domain.Model
{
    public class UserOtp : Entity<int>
    {
        public string PhoneNumber { get; set; }
        public string Otp { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}
