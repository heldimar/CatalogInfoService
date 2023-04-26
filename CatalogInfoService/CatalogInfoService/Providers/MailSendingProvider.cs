using CatalogInfoCommonLibrary.Providers;
using System.Net.Mail;

namespace CatalogInfoService.Providers
{
    public class MailSendingProvider : IMailSendingProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string smtpHost;
        public MailSendingProvider(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            smtpHost = _configuration?.GetSection("SmtpHost").Value ?? throw new ArgumentNullException(nameof(_configuration));
        }

        public SmtpClient NewSmtpClient => new SmtpClient(smtpHost) { UseDefaultCredentials = false, EnableSsl = false };
    }
}
