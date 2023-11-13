using Arival.Domain.IService;
using System.Security.Cryptography;

namespace Arival.Domain.Service
{
    public class OtpGenerationService : IOtpGenerationService
    {

        public string GenerateOtp(int CodeCount)
        {
            string generatedCode = string.Empty;
            for (int i = 1; i <= CodeCount; i++)
            {
               generatedCode += RandomNumberGenerator.GetInt32(0, 9).ToString();
            }
            return generatedCode;
        }

    }
}
