using FluentEmail.Core;
using FluentEmail.Core.Interfaces;
using FluentEmail.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestFail
{
	/// <summary>
	/// Fake FluentEmail sender for testing.
	/// </summary>
	public class FakeFluentEmailSender : ISender
	{
		/// <summary>
		/// All emails sent, in order of sending.
		/// </summary>
		public readonly List<IFluentEmail> SentEmails = new List<IFluentEmail>();
		private readonly ILogger<FakeFluentEmailSender> _logger;

		/// <inheritdoc />
		public FakeFluentEmailSender(ILogger<FakeFluentEmailSender> logger)
		{
			_logger = logger;
		}

		/// <inheritdoc />
		public SendResponse Send(IFluentEmail email, CancellationToken? token = null)
		{
			var address = string.Join(", ", email.Data.ToAddresses.Select(x => x.EmailAddress));
			_logger.LogInformation($"To: {address}, Subject: '{email.Data.Subject}'. Body: {email.Data.Body}");
			SentEmails.Add(email);

			return new SendResponse();
		}

		/// <inheritdoc />
		public Task<SendResponse> SendAsync(IFluentEmail email, CancellationToken? token = null)
		{
			var address = string.Join(", ", email.Data.ToAddresses.Select(x => x.EmailAddress));
			_logger.LogInformation($"To: {address}, Subject: '{email.Data.Subject}'. Body: {email.Data.Body}");
			SentEmails.Add(email);

			return Task.FromResult(new SendResponse());
		}
	}

	public static class FakeFluentEmailSenderExtensions
	{
		public static FluentEmailServicesBuilder AddFakeFluentEmailSender(this FluentEmailServicesBuilder builder)
		{
			builder.Services.Add(ServiceDescriptor.Singleton<ISender>(services => new FakeFluentEmailSender(services.GetRequiredService<ILogger<FakeFluentEmailSender>>())));
			return builder;
		}
	}
}
