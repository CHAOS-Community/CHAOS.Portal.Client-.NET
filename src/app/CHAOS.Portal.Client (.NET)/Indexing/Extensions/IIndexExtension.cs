﻿using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Indexing.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Indexing.Extensions
{
	public interface IIndexExtension
	{
		IServiceCallState<PagedResult<IndexResponse>> Search(string query, string facet, string sort, uint pageIndex, uint pageSize, Guid? accessPointGUID = null);
	}
}