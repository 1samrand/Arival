using Arival.Domain.IService;

namespace Arival.Domain.Service
{
    public class SmsService : ISmsService
    {
        public void SendMessage(string PhoneNumber, string Message)
        {
            //TODO:it would be implement with real sms provider in the future
        }
    }
}
