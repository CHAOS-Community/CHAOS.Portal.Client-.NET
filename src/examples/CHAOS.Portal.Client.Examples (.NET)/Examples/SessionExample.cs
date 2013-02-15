using System;
using CHAOS.Portal.Client.Extensions;

namespace CHAOS.Portal.Client.Examples.Examples
{
	public class SessionExample : AExample
	{
		protected override void Run()
		{
			Console.WriteLine("Calling service...");

			var response = Client.Session().Create().Synchronous().Response;

			if (response.Error == null)
			{
				Console.WriteLine("Created session with GUID: {0}", response.Result.Results[0].Guid);
			}
			else
			{
				Console.WriteLine("Servicecall failed with: {0}", response.Error.Message);
			}
		}
	}
}