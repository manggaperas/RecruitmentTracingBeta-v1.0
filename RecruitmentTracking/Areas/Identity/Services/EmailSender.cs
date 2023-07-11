﻿using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using RecruitmentTracking.Models;


namespace RecruitmentTracking.Areas.Identity.Services
{
	public class MailtrapEmailSender : IEmailSender
	{
		private readonly MailSettings _mailSettings;

		public MailtrapEmailSender(IOptions<MailSettings> mailSettings)
		{
			_mailSettings = mailSettings.Value;
		}

		public async Task SendEmailAsync(string email, string subject, string message)
		{
			var emailMessage = new MimeMessage();

			emailMessage.From.Add(new MailboxAddress(_mailSettings.FromName, _mailSettings.FromAddress));
			emailMessage.To.Add(new MailboxAddress("", email));
			emailMessage.Subject = subject;
			emailMessage.Body = new TextPart("html")
			{
				Text = message
			};

			using var client = new SmtpClient();
			await client.ConnectAsync(_mailSettings.SmtpServer, _mailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.None);
			await client.AuthenticateAsync(_mailSettings.Username, _mailSettings.Password);
			await client.SendAsync(emailMessage);
			await client.DisconnectAsync(true);
		}
	}
}
