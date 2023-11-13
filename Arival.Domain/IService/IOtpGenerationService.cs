using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arival.Domain.IService
{
    public interface IOtpGenerationService
    {
        string GenerateOtp(int CodeCount);
    }
}
