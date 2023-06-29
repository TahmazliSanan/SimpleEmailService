using DataAccess.Entities.EmailEntity;

namespace Services.Abstracts
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}