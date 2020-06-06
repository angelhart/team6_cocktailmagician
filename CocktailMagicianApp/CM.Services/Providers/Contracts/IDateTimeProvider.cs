using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Services.Providers.Contracts
{
	public interface IDateTimeProvider
	{
		DateTimeOffset GetDateTimeDateTimeOffset();
	}
}
