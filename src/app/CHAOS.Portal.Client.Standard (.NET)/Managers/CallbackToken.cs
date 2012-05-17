using System;

namespace CHAOS.Portal.Client.Standard.Managers
{
	internal class CallbackToken<TInternal, TExternal> : ICallbackToken<TInternal>
	{
		public TInternal InternalToken { get; set; }

		public TExternal ExternalToken { get; set; }

		public Action<bool, TExternal> Callback { get; set; }

		public CallbackToken(TInternal internalToken, TExternal externalToken, Action<bool, TExternal> callback)
		{
			InternalToken = internalToken;
			ExternalToken = externalToken;
			Callback = callback;
		}

		public void CallCallback(bool success)
		{
			if (Callback != null)
				Callback(success, ExternalToken);
		}
	}
}