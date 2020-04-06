using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using TestFail.Templates;

namespace TestFail.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly IFluentEmailFactory _fluentEmailFactory;

		public WeatherForecastController(IFluentEmailFactory fluentEmailFactory)
		{
			_fluentEmailFactory = fluentEmailFactory;
		}

		[HttpGet]
		public string Get()
		{
			var email = _fluentEmailFactory.Create()
				.To("person@email.com")
				.Subject("Email Subject")
				.UsingTemplateFromEmbedded("TestFail.Templates.Template.cshtml", new EmailModel { Heading = "ExpectedString" }, System.Reflection.Assembly.GetAssembly(typeof(Startup)));
			return email.Data.Body;
		}
	}
}
