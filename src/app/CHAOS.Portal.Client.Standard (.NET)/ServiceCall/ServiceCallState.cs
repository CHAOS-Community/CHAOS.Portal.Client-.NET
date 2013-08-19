using System;
using System.Threading;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using Math = CHAOS.Utilities.Math;
using CHAOS.Extensions;

#if SILVERLIGHT
using System.Windows;
#endif

namespace CHAOS.Portal.Client.Standard.ServiceCall
{
	public class ServiceCallState<T> : IServiceCallState<T> where T : class, IServiceBody
	{
		public event EventHandler<DataChangedEventArgs<double>> UploadProgressChanged = delegate { };
		public event EventHandler<DataChangedEventArgs<double>> DownloadProgressChanged = delegate { };
		public event EventHandler<DataEventArgs<ServiceResponse<T>>> OperationCompleted = delegate { };

		private double _uploadProgress;
		private double _downloadProgress;

		public double UploadProgress
		{
			get { return _uploadProgress; }
			set
			{
				value = Math.Clamp(value, 0, 1);

				if (value == _uploadProgress)
					return;

				var old = _uploadProgress;
				_uploadProgress = value;

				Feedback(() => UploadProgressChanged(this, new DataChangedEventArgs<double>(old, _uploadProgress)));
			}
		}

		public double DownloadProgress
		{
			get { return _downloadProgress; }
			set
			{
				value = Math.Clamp(value, 0, 1);

				if (value == _downloadProgress)
					return;

				var old = _downloadProgress;
				_downloadProgress = value;

				Feedback(() => DownloadProgressChanged(this, new DataChangedEventArgs<double>(old, _downloadProgress)));
			}
		}

		public object Token { get; set; }

		public ServiceResponse<T> Response { get; private set; }
		public ServiceCallback<T> Callback { get; set; }

		public bool FeedbackOnDispatcher { get; set; }

#if !SILVERLIGHT
		public IServiceCallState<T> Synchronous(uint timeout)
		{
			var endTime = DateTime.Now.AddMilliseconds(timeout);

			while (Response == null)
			{
				if (timeout != 0 && endTime.CompareTo(DateTime.Now) < 0)
					throw new TimeoutException();

				Thread.Sleep(50);
			}

			return this;
		}
#endif

		public IServiceCallState<T> WithCallback(ServiceCallback<T> callback, object token)
		{
			Callback = callback;
			Token = token;

			return this;
		}

		public IServiceCallState<T> InvokeFeedbackOnDispatcher(bool invokeFeedbackOnDispatcher)
		{
			FeedbackOnDispatcher = invokeFeedbackOnDispatcher;

			return this;
		}

		public IServiceCallState<T> ThrowError()
		{
			if(Response == null)
				throw new Exception("Service response not ready");

			if (Response.Error != null)
				throw Response.Error;

			return this;
		}

		private void Feedback(Action action)
		{
#if SILVERLIGHT
			if (!FeedbackOnDispatcher || Deployment.Current.Dispatcher.CheckAccess())
				action();
			else
				Deployment.Current.Dispatcher.BeginInvoke(action);
#else
			action(); //TODO: Find alternative to dispatcher
#endif
		}

		public void ReportResult(ServiceResponse<T> response)
		{
			response.ValidateIsNotNull("response");

			Response = response;

			Feedback(() =>
			         	{
							OperationCompleted(this, new DataEventArgs<ServiceResponse<T>>(response));

							var callback = Callback;
							if (callback != null)
								callback(response, Token);
			         	});
		}
	}
}