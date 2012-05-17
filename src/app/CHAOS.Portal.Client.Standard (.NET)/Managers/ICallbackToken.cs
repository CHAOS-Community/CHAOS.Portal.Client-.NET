namespace CHAOS.Portal.Client.Standard.Managers
{
	internal interface ICallbackToken<T>
	{
		T InternalToken { get; set; }

		void CallCallback(bool success);
	}
}