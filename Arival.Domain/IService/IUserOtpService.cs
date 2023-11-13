using Arival.Core.GenericDataResponse;
using Arival.Domain.Model;
using Arival.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arival.Domain.IService
{
    public interface IUserOtpService
    {
        DataResponse<GeneratedOtpDto> CreateOtp(string PhoneNumber);
        DataResponse<VerifyOtpDto> VerifyOtp(string VerificationCode, string PhoneNumber);
    }
}
