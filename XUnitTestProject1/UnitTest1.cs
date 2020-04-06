using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using System;
using TestFail;
using Xunit;

namespace XUnitTestProject1
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			using var server = new TestServer(new WebHostBuilder()
				//.UseContentRoot(Directory.GetCurrentDirectory())
				//.UseConfiguration(configBuilder.Build())
				.UseEnvironment("Development")
				.ConfigureLogging(logging =>
				{
					logging.AddConsole();
					logging.SetMinimumLevel(LogLevel.Warning);
					//For when things are really hard to track down:
					//logging.SetMinimumLevel(LogLevel.Trace);
				})
				.UseStartup<Startup>());

			using var client = server.CreateClient();

			var res = client.GetAsync("/WeatherForecast").Result;

			var str = res.Content.ReadAsStringAsync().Result;

			Console.WriteLine(str);

			Assert.Contains("ExpectedString", str);
		}
	}
}
