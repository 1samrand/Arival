using Arival.Core.GenericDataResponse;
using Arival.Domain.IRepositroy;
using Arival.Domain.IService;
using Arival.Domain.Model;
using Arival.Core.DTO.Response;
using System;
using System.Collections.Generic;

namespace Arival.Domain.Service
{
    public class UserOtpService : BaseCrudService<UserOtp>, IUserOtpService
    {
        private readonly IUserOtpRepository _userOtpRepository;
        private readonly IConfigurationService _configurationService;
        private readonly IOtpGenerationService _otpGenerationService;
        private readonly ISmsService _smsService;
        public UserOtpService(IUserOtpRepository userOtpRepository, IOtpGenerationService otpGenerationService, IConfigurationService configurationService, ISmsService smsService) : base(userOtpRepository)
        {
            _userOtpRepository = userOtpRepository;
            _otpGenerationService = otpGenerationService;
            _configurationService= configurationService;
            _smsService = smsService;
        }

        public DataResponse<GeneratedOtpDto> CreateOtp(string PhoneNumber)
        {

            try
            {
                if (IsValidForCreateNewOtp(PhoneNumber))
                {
                    var response = GenerateOtp(PhoneNumber);
                    sendSms(PhoneNumber,response.Result.Code);
                    return response;
                }
                else
                    return GenerateErrorResponse();

            }
            catch (Exception e)
            {
                return GenerateExceptionResponse<GeneratedOtpDto>(e);
            }
            
        }

        public DataResponse<VerifyOtpDto> VerifyOtp(string VerificationCode,string PhoneNumber)
        {
            try
            {
                var response = new DataResponse<VerifyOtpDto>();
                var verifyOtpDto = new VerifyOtpDto();
                var hashedVerificationCode = PasswordHasher.Hash(VerificationCode);
                var verificationResult = _userOtpRepository.IsValidVerificationCode(PhoneNumber, hashedVerificationCode, _configurationService.CodeExpirationTimestamp);
                verifyOtpDto.IsCorrect = verificationResult;
                verifyOtpDto.VerificationCode = VerificationCode;
                response.Result = verifyOtpDto;
                return response;
            }
            catch (Exception e)
            {
                return GenerateExceptionResponse<VerifyOtpDto>(e);
            }
        }

        private void sendSms(string phoneNumber, string code)
        {
            string message = $"Your Arival verification code is {code}";
            _smsService.SendMessage(phoneNumber, message);
        }
        private DataResponse<GeneratedOtpDto> GenerateOtp(string phoneNumber)
        {
            var response = new DataResponse<GeneratedOtpDto>();
            var userOtp = new UserOtp();
            var generatedOtpDto = new GeneratedOtpDto();
            var generatedCode = _otpGenerationService.GenerateOtp(6);
            userOtp.PhoneNumber = phoneNumber;
            userOtp.Otp = PasswordHasher.Hash(generatedCode);
            userOtp.CreatedTime = DateTime.Now;

            _userOtpRepository.CreateOtp(userOtp);
            generatedOtpDto.Code = generatedCode;
            response.Result = generatedOtpDto;
            return response;
        }
        private DataResponse<GeneratedOtpDto> GenerateErrorResponse()
        {
            var errorMessage = $"The number of active codes exceeds the limit : {_configurationService.MaximumActiveCodePerPhone}";
            var response = new DataResponse<GeneratedOtpDto>();
            response.ErrorList = new List<ResponseError>();
            var error = new ResponseError() { ErrorCode = DataResponseErrorCode.ValidationError, ErrorDescription = errorMessage };
            response.ErrorList.Add(error);
            return response;
            return response;
        }

        private DataResponse<T> GenerateExceptionResponse<T>(Exception e)
        {
            var response = new DataResponse<T>();
            response.ErrorList = new List<ResponseError>();
            var error = new ResponseError() { ErrorCode = DataResponseErrorCode.InternalError, ErrorDescription = e.Message };
            response.ErrorList.Add(error);
            return response;
        }
        private bool IsValidForCreateNewOtp(string phoneNumber)
        {
            var countOfActiveCodes = _userOtpRepository.GetCountOfActiveCodes(phoneNumber, _configurationService.CodeExpirationTimestamp);
            return countOfActiveCodes < _configurationService.MaximumActiveCodePerPhone;
        }
    }
}
