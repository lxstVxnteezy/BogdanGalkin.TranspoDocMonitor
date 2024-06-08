using System.Text;
using MailKit.Net.Smtp;
using MimeKit;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.Core.Nortification;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.Core.Notification
{
    public interface IEmailNotification
    {
        Task SendEmailAsync(VehicleDocument document, CancellationToken cancellationToken);
    }

    public class EmailNotification : IEmailNotification
    {
        public Task SendEmailAsync(VehicleDocument document, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
