using System;
using System.Collections.Generic;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Utilities;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Web;
using HTTPMethod = CHAOS.Web.HTTPMethod;

namespace CHAOS.Portal.Client.Standard.ServiceCall
{
	public class ServiceCall<T> : IServiceCall<T> where T : class, IServiceBody
	{
		private readonly ServiceCallState<T> _state;
		private readonly ResultParser<T> _resultParser;
		private readonly SmartHTTPRequest _request;

		public IServiceCallState<T> State { get { return _state; } }

		public ServiceCall(ServiceCallState<T> state, ResultParser<T> resultParser, SmartHTTPRequest request)
		{
			_state = ArgumentUtilities.ValidateIsNotNull("state", state);
			_resultParser = ArgumentUtilities.ValidateIsNotNull("resultParser", resultParser);
			_request = ArgumentUtilities.ValidateIsNotNull("request", request);

			_request.UploadProgressChanged += Request_UploadProgressChanged;
			_request.DownloadProgressChanged += Request_DownloadProgressChanged;
			_request.Completed += Request_Completed;
		}

		public void Call(string servicePath, IDictionary<string, object> parameters, HTTPMethod method)
		{
			ArgumentUtilities.ValidateIsNotNullOrEmpty("servicePath", servicePath);
			ArgumentUtilities.ValidateIsNotNull("parameters", parameters);

			_request.Call(servicePath, parameters, method);
		}

		private void Request_UploadProgressChanged(object sender, DataChangedEventArgs<double> e)
		{
			_state.UploadProgress = e.NewValue;
		}

		private void Request_DownloadProgressChanged(object sender, DataChangedEventArgs<double> e)
		{
			_state.DownloadProgress = e.NewValue;
		}

		private void Request_Completed(object sender, DataOperationEventArgs<string> e)
		{
			_state.ReportResult(e.HasError
									? new ServiceResponse<T> {Error = new Exception("Http error", e.Error)}
									: _resultParser.Parse(e.Data));
		}
	}
}