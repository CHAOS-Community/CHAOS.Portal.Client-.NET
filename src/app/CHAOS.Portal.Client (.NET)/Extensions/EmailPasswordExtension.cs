﻿using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class EmailPasswordExtension : AExtension, IEmailPasswordExtension
	{
		public IServiceCallState<User> CreatePassword(Guid userGUID, string password)
		{
			return CallService<User>(HTTPMethod.GET, userGUID, password);
		}

		public IServiceCallState<User> Login(string email, string password)
		{
			return CallService<User>(HTTPMethod.GET, email, password);
		}
	}
}