using System;
using CHAOS.Common.Events;
using CHAOS.Portal.Client.Data;

namespace CHAOS.Portal.Client.ServiceCall
{
	/// <summary>
	/// Represents the state of a service call.
	/// </summary>
	/// <typeparam name="T">The type of the result from the service.</typeparam>
	public interface IServiceCallState<T> where T : class, IServiceResult
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
		event EventHandler<DataOperationEventArgs<T>> OperationCompleted;
		
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
		T Result { get; }
		/// <summary>
		/// An error if the service call fails.
		/// </summary>
		Exception Error { get; }

		/// <summary>
		/// A callback to be called when a service call completes, successful or not.
		/// This is called just before the <code>OperationCompleted</code> <code>event</code> is called.
		/// </summary>
		ServiceCallback<T> Callback { get; set; }

		/// <summary>
		/// If true; callback and events will be called on dispatcher thread.
		/// </summary>
		bool FeedbackOnDispatcher { get; set; }
	}

	/// <summary>
	/// A delegate used as the callback when a service call completes, successful or not.
	/// </summary>
	/// <typeparam name="TResult">The type of result from the service call.</typeparam>
	/// <param name="result">The result of the service call, if successful, otherwise null.</param>
	/// <param name="error">An error if the service call failed, otherwise null.</param>
	/// <param name="token">An object if set on <code>IServiceCallState&gr;T&lt;.Object</code></param>
	public delegate void ServiceCallback<in TResult>(TResult result, Exception error, object token);
}