using System;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;

namespace CHAOS.Portal.Client.ServiceCall
{
	/// <summary>
	/// Represents the state of a service call.
	/// </summary>
	/// <typeparam name="T">The type of the result from the service.</typeparam>
	public interface IServiceCallState<T> where T : class, IServiceBody
	{
		/// <summary>
		/// Is raised every time the upload progress change.
		/// </summary>
		event EventHandler<DataChangedEventArgs<double>> UploadProgressChanged;
		/// <summary>
		/// Is raised every time the download progress change.
		/// </summary>
		event EventHandler<DataChangedEventArgs<double>> DownloadProgressChanged;
		/// <summary>
		/// Is raised when the service call has completed, successful or not.
		/// </summary>
		event EventHandler<DataEventArgs<ServiceResponse<T>>> OperationCompleted;
		
		/// <summary>
		/// Indicates the upload progress from 0 to 1.
		/// </summary>
		double UploadProgress { get; }
		/// <summary>
		/// Indicates the download progress from 0 to 1.
		/// </summary>
		double DownloadProgress { get; }

		/// <summary>
		/// Any object used to track the service call.
		/// </summary>
		object Token { get; set; }

		/// <summary>
		/// The result of the service call, is set when the call completes successfully.
		/// </summary>
		ServiceResponse<T> Response { get; }

		/// <summary>
		/// A callback to be called when a service call completes, successful or not.
		/// This is called just before the <code>OperationCompleted</code> <code>event</code> is called.
		/// </summary>
		ServiceCallback<T> Callback { get; set; }

		/// <summary>
		/// If true; callback and events will be called on dispatcher thread.
		/// </summary>
		bool FeedbackOnDispatcher { get; set; }

#if !SILVERLIGHT
		/// <summary>
		/// Blocks the thread.
		/// </summary>
		/// <param name="timeout">The timeout in milliseconds or 0 for no timeout (other timeout may apply)</param>
		/// <returns>Returns the IServiceCallState for chaining</returns>
		IServiceCallState<T> Synchronous(uint timeout = 0);
#endif

		/// <summary>
		/// Sets the callback and optionally the token.
		/// </summary>
		/// <param name="callback">A callback to be called when a service call completes, successful or not</param>
		/// <param name="token">Any object used to track the service call</param>
		/// <returns>Returns the IServiceCallState for chaining</returns>
		IServiceCallState<T> WithCallback(ServiceCallback<T> callback, object token = null);

		/// <summary>
		/// If parameter is true; callback and events will be called on dispatcher thread.
		/// </summary>
		/// <param name="invokeFeedbackOnDispatcher">If true; callback and events will be called on dispatcher thread</param>
		/// <returns>Returns the IServiceCallState for chaining</returns>
		IServiceCallState<T> InvokeFeedbackOnDispatcher(bool invokeFeedbackOnDispatcher = true);

		/// <summary>
		/// Throws any error returned by the service.
		/// </summary>
		/// <returns>Returns the IServiceCallState for chaining</returns>
		IServiceCallState<T> ThrowError();
	}

	/// <summary>
	/// A delegate used as the callback when a service call completes, successful or not.
	/// </summary>
	/// <typeparam name="TResult">The type of result from the service call.</typeparam>
	/// <param name="response">The response from the service.</param>
	/// <param name="token">An object if set on <code>IServiceCallState&gr;T&lt;.Object</code></param>
	public delegate void ServiceCallback<TResult>(ServiceResponse<TResult> response, object token) where TResult : class, IServiceBody;
}