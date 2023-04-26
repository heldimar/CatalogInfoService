using CatalogInfoCommonLibrary.Commands.Special;
using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Providers;
using CatalogInfoModelsLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Compression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace CatalogInfoCommonLibrary.Commands.MailSending
{
    public class SendMailFromStreamCommand : ICommand
    {
        public SendMailFromStreamCommand(IMailSendingProvider mailSendingProvider, object[] args)
        {
            this.mailSendingProvider = mailSendingProvider;
            data = (Stream)args[0];
            attName = (string)args[1];
            mailAddressesTo = (List<MailAddress>)args[2];
            subject = (string)args[3];
            messageText = args.Length > 4 ? (string)args[4] : string.Empty;
        }

        public bool CanUndo => false;

        readonly IMailSendingProvider mailSendingProvider;

        readonly Stream data;

        readonly List<MailAddress> mailAddressesTo;

        readonly string subject;

        readonly string attName;

        readonly string messageText;

        public async Task Execute()
        {
            MailMessage m = new MailMessage();
            m.From = new MailAddress("info@vitta.ru", "Info");
            mailAddressesTo.ForEach(m.To.Add);
            m.Subject = subject;
            data.Position = 0;
            m.Attachments.Add(new Attachment(data, attName));
            m.Body = messageText;
            m.ReplyToList.Add(new MailAddress("info@vitta.ru", "Info"));
            using (var client = mailSendingProvider.NewSmtpClient)
            {
                await client.SendMailAsync(m);
            }

        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(SendMailFromStreamCommand));
        }
    }
}
