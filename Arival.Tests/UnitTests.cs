using Arival.Domain.IService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Arival.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestInitialize]
        public void TestInit()
        {
           TestHelper.DoConfiguration();
          
        }

        [TestMethod]
        public void CreateOtp()
        {
            int codeCount = 6;
            var service = TestHelper.ServiceProvider.GetService<IOtpGenerationService>();
            var generatedOtp = service.GenerateOtp(codeCount);
            Assert.IsTrue(generatedOtp.Length == codeCount);
        }
        [TestMethod]
        public void CreateOtpAndVerifyShouldWorkSuccess()
        {
            string phoneNumber = "09124272246";
            var service = TestHelper.ServiceProvider.GetService<IUserOtpService>();
            var createOtpResponse = service.CreateOtp(phoneNumber);
            var verifyOtpResponse = service.VerifyOtp(createOtpResponse.Result.Code, phoneNumber);

            Assert.IsTrue(verifyOtpResponse.Result.IsCorrect);
            Assert.IsTrue(verifyOtpResponse.IsSuccess);
        }
        [TestMethod]
        public void CreateOtpMaximumExceedShouldWorkSuccess()
        {
            var configurationService = TestHelper.ServiceProvider.GetService<IConfigurationService>();
            var otpService = TestHelper.ServiceProvider.GetService<IUserOtpService>();
            string phoneNumber = "09124272246";
            for (int i = 1; i <= configurationService.MaximumActiveCodePerPhone; i++)
            {
               var responseSuccess = otpService.CreateOtp(phoneNumber);
               Assert.IsTrue(responseSuccess.IsSuccess);
            }

            var responseError = otpService.CreateOtp(phoneNumber);
            Assert.IsFalse(responseError.IsSuccess);
        }
    }
}