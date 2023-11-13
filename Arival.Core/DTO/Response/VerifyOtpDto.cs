using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arival.Core.DTO.Response
{
    public class VerifyOtpDto
    {
        public string VerificationCode { get; set;}
        public bool IsCorrect { get; set; }
    }
}
