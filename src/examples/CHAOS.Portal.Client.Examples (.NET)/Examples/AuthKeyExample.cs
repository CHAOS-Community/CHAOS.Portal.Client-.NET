using System;
using CHAOS.Portal.Client.Extensions;

namespace CHAOS.Portal.Client.Examples.Examples
{
	public class AuthKeyExample : AExample
	{
		protected override void Run()
		{
			Console.Write("Enter access token: ");
			var accessToken = Console.ReadLine();
			

			Console.WriteLine();
			Console.WriteLine("Calling service...");

			var response = Client.AuthKey().Login(accessToken).Synchronous().Response;

			if (response.Error == null)
			{
				Console.WriteLine("Logged in. User GUID: {0}", response.Body.Results[0].Guid);
			}
			else
			{
				Console.WriteLine("Servicecall failed with: {0}", response.Error.Message);
			}
		}
	}
}