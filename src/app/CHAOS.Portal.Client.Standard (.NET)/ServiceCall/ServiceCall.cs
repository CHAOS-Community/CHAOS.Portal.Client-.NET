using System;
using System.Collections.Generic;
using CHAOS.Common.Events;
using CHAOS.Common.Utilities;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.ServiceCall
{
	public class ServiceCall<T> : IServiceCall<T> where T : class, IServiceResult
	{
		private readonly ServiceCallState<T> _State;
		private readonly ResultParser<T> _ResultParser;
		private readonly SmartHTTPRequest _Request;

		public IServiceCallState<T> State { get { return _State; } }

		public ServiceCall(ServiceCallState<T> state, ResultParser<T> resultParser, SmartHTTPRequest request)
		{
			_State = ArgumentUtilities.ValidateIsNotNull("state", state);
			_ResultParser = ArgumentUtilities.ValidateIsNotNull("resultParser", resultParser);
			_Request = ArgumentUtilities.ValidateIsNotNull("request", request);

			_Request.UploadProgressChanged += Request_UploadProgressChanged;
			_Request.DownloadProgressChanged += Request_DownloadProgressChanged;
			_Request.Completed += Request_Completed;
		}

		public void Call(string servicePath, IDictionary<string, object> parameters, HTTPMethod method)
		{
			ArgumentUtilities.ValidateIsNotNullOrEmpty("servicePath", servicePath);
			ArgumentUtilities.ValidateIsNotNull("parameters", parameters);

			if (method == HTTPMethod.GET)
				parameters["_"] = DateTime.Now.Ticks;

			_Request.Call(servicePath, parameters, method);
		}

		private void Request_UploadProgressChanged(object sender, DataChangedEventArgs<double> e)
		{
			_State.UploadProgress = e.NewValue;
		}

		private void Request_DownloadProgressChanged(object sender, DataChangedEventArgs<double> e)
		{
			_State.DownloadProgress = e.NewValue;
		}

		private void Request_Completed(object sender, DataOperationEventArgs<string> e)
		{
			try
			{
				if(e.HasError)
					_State.ReportResult(null, e.Error);
				else
					_State.ReportResult(_ResultParser.Parse(e.Data), null);
			}
			catch (Exception error)
			{
				_State.ReportResult(null, new Exception("Failed to parse service result", error));
			}
		}
	}
}