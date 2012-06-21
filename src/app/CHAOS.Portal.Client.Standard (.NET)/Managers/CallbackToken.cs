using System;

namespace CHAOS.Portal.Client.Standard.Managers
{
	internal class CallbackToken<T>
	{
		public T Token { get; private set; }

		public Action<bool> Callback { get; private set; }

		public CallbackToken(T token, Action<bool> callback)
		{
			Token = token;
			Callback = callback;
		}

		public void CallCallback(bool success)
		{
			if (Callback != null)
				Callback(success);
		}
	}
}