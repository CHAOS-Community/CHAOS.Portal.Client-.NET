﻿using System;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.Indexing.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Indexing.Extensions
{
	public class IndexExtension : AExtension, IIndexExtension
	{
		public IServiceCallState<IndexResponse> Search(string query, string facet, string sort, uint pageIndex, uint pageSize, Guid? accessPointGUID = null)
		{
			return CallService<IndexResponse>(HTTPMethod.GET, query, facet, sort, pageIndex, pageSize, accessPointGUID);
		}
	}
}