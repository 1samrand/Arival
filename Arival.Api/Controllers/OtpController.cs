using Arival.Domain.IService;
using Microsoft.AspNetCore.Mvc;
using Arival.Core.DTO.Request.Otp;


namespace Arival.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : Controller
    {
        private readonly IUserOtpService _userOtpService;

        public OtpController(IUserOtpService userOtpService)
        {
            _userOtpService = userOtpService;
        }
        [HttpPost("OtpLogin")]
        public IActionResult OtpLogin(OtpLoginDto OtpLoginDto)
        {
           var response = _userOtpService.CreateOtp(OtpLoginDto.PhoneNumber);
           return prepareResponse(response);
        }

        [HttpPost("OtpVerify")]
        public IActionResult OtpVerify(OtpVerifyDto OtpVerifyDto)
        {
           var response = _userOtpService.VerifyOtp(OtpVerifyDto.VerificationCode, OtpVerifyDto.PhoneNumber);
           return prepareResponse(response);
        }

        private IActionResult prepareResponse(dynamic response)
        {
          return response.IsSuccess == true ? Ok(response) : BadRequest(response);
        }

    }
}
