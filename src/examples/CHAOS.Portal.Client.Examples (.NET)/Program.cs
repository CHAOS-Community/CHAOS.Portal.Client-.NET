using System;
using System.Reflection;
using CHAOS.Portal.Client.Examples.Examples;
using CHAOS.Portal.Client.Standard;

namespace CHAOS.Portal.Client.Examples
{
	class Program
	{
		private IPortalClient _client;

		private void InitializeClient()
		{
			_client = new PortalClient {};
		}

		private void GetServicePath()
		{
			Console.Write("Enter API path: http://");
			_client.ServicePath = "http://" + Console.ReadLine();
		}

		private void RunExamples()
		{
			new SessionExample().InitializeAndRun(_client);
			new EmailPasswordExample().InitializeAndRun(_client);
		}

		#region Not important

		private void Run()
		{
			InitializeClient();
			ShowIntro();
			GetServicePath();

			RunExamples();

			WaitForExit();
		}

		private void ShowIntro()
		{
			Console.Write(@"   ________  _____   ____  _____
  / ____/ / / /   | / __ \/ ___/
 / /   / /_/ / /| |/ / / /\__ \ 
/ /___/ __  / ___ / /_/ /___/ / 
\____/_/ /_/_/  |_\____//____/");
			Console.WriteLine();
			var version = Assembly.GetAssembly(typeof(IPortalClient)).GetName().Version;
			Console.WriteLine("Portal Client (version {0}.{1}.{2}) example", version.Major, version.Minor, version.Build);
			Console.WriteLine();
		}

		private void WaitForExit()
		{
			Console.WriteLine();
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}

		public static void Main(string[] args)
		{
			new Program().Run();
		}
		
		#endregion
	}
}