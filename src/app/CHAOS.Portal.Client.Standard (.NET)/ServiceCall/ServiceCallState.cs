using System;
using System.Threading;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using Math = CHAOS.Utilities.Math;

#if SILVERLIGHT

using System.Windows;

#endif

namespace CHAOS.Portal.Client.Standard.ServiceCall
{
	public class ServiceCallState<T> : IServiceCallState<T> where T : class, IServiceResult
	{
		public event EventHandler<DataChangedEventArgs<double>> UploadProgressChanged = delegate { };
		public event EventHandler<DataChangedEventArgs<double>> DownloadProgressChanged = delegate { };
		public event EventHandler<DataOperationEventArgs<T>> OperationCompleted = delegate { };

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

		public T Result { get; private set; }
		public Exception Error { get; private set; }

		public ServiceCallback<T> Callback { get; set; }

		public bool FeedbackOnDispatcher { get; set; }

		public IServiceCallState<T> Synchronous(uint timeout)
		{
			var endTime = DateTime.Now.AddMilliseconds(timeout);

			while (Result == null && Error == null)
			{
				if (timeout != 0 && endTime.CompareTo(DateTime.Now) < 0)
					throw new TimeoutException();

				Thread.Sleep(50);
			}

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

		public void ReportResult(T result, Exception error)
		{
			if (!(result == null ^ error == null))
				throw new ArgumentException("Exactly one of result or error must be set");

			Result = result;
			Error = error;

			Feedback(() =>
			         	{
							OperationCompleted(this, new DataOperationEventArgs<T>(result, error));

							var callback = Callback;
							if (callback != null)
								callback(result, error, Token);
			         	});
		}
	}
}