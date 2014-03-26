using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using CHAOS.Portal.Client.Data;
using CHAOS.Utilities;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public abstract class AExtension : IExtension
	{
		protected IServiceCaller _serviceCaller;

		private readonly string _extensionName;

		protected AExtension()
		{
			var typeName = GetType().Name;

			if (typeName.Substring(typeName.Length - 9) != "Extension")
				throw new Exception("Class name must end on \"Extension\"");

			_extensionName = typeName.Remove(typeName.Length - 9);
		}

		public void Initialize(IServiceCaller portalClient)
		{
			if(_serviceCaller != null)
				throw new InvalidOperationException("Already initialized");
			
			_serviceCaller = ArgumentUtilities.ValidateIsNotNull("portalClient", portalClient);

			_serviceCaller.RegisterExtension(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		protected IServiceCallState<T> CallService<T>(HTTPMethod callMethod, params object[] parameters) where T : class, IServiceBody
		{
			return CallService<T>(callMethod, parameters, true);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		protected IServiceCallState<T> CallServiceWithoutSession<T>(HTTPMethod callMethod, params object[] parameters) where T : class, IServiceBody
		{
			return CallService<T>(callMethod, parameters, false);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private IServiceCallState<T> CallService<T>(HTTPMethod httpMethod, IList<object> parameters, bool requiresSession) where T : class, IServiceBody
		{
			var method = new StackTrace().GetFrame(2).GetMethod(); //Jump two steps back, to get public extension method
			var methodParameters = method.GetParameters();

			if(methodParameters.Count() != parameters.Count)
				throw new Exception(string.Format("Number of values ({0}) and number of method parameters ({1}) does no match", methodParameters.Count(), parameters.Count));

			var serviceParameters = new Dictionary<string, object>();

			for (var i = 0; i < parameters.Count; i++)
				serviceParameters[methodParameters[i].Name] = parameters[i];

			return _serviceCaller.CallService<T>(_extensionName, method.Name, serviceParameters, httpMethod, requiresSession);
		}
	}
}