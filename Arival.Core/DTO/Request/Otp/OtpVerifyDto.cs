using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arival.Core.DTO.Request.Otp
{
    public class OtpVerifyDto
    {
        public string VerificationCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
