using System;
using CHAOS.Common.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using Math = CHAOS.Common.Utilities.Math;

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

		private double _UploadProgress;
		private double _DownloadProgress;

		public double UploadProgress
		{
			get { return _UploadProgress; }
			set
			{
				value = Math.Clamp(value, 0, 1);

				if (value == _UploadProgress)
					return;

				var old = _UploadProgress;
				_UploadProgress = value;

				Feedback(() => UploadProgressChanged(this, new DataChangedEventArgs<double>(old, _UploadProgress)));
			}
		}

		public double DownloadProgress
		{
			get { return _DownloadProgress; }
			set
			{
				value = Math.Clamp(value, 0, 1);

				if (value == _DownloadProgress)
					return;

				var old = _DownloadProgress;
				_DownloadProgress = value;

				Feedback(() => DownloadProgressChanged(this, new DataChangedEventArgs<double>(old, _DownloadProgress)));
			}
		}

		public object Token { get; set; }

		public T Result { get; private set; }
		public Exception Error { get; private set; }

		public ServiceCallback<T> Callback { get; set; }

		public bool FeedbackOnDispatcher { get; set; }

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
			         		var callback = Callback;
							if (callback != null)
								callback(result, error, Token);

							OperationCompleted(this, new DataOperationEventArgs<T>(result, error));
			         	});
		}
	}
}