using System;

namespace CHAOS.Portal.Client.Examples.Examples
{
	public abstract class AExample
	{
		protected IPortalClient Client { get; private set; }

		public void InitializeAndRun(IPortalClient client)
		{
			Client = client;

			Console.WriteLine();
			Console.WriteLine(string.Format(" {0} example ", GetType().Name.Replace("Example", "")).PadRight(50, '=').PadLeft(70, '='));
			Console.WriteLine();
			
			Run();

			Console.WriteLine();
			Console.WriteLine("".PadLeft(70, '='));
			Console.WriteLine();
		}

		protected abstract void Run();
	}
}