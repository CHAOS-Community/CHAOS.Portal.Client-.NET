namespace CHAOS.Portal.Client.Standard.Managers
{
	internal interface ICallbackToken<T>
	{
		T Token { get; set; }

		void CallCallback(bool success);
	}
}