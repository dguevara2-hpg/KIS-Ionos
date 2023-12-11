using KIS_Core.Domain.Models;

namespace KIS_Core.Domain.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
