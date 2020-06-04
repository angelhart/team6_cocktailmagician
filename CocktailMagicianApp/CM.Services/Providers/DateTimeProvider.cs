using System;
using CM.Services.Providers.Contracts;

namespace CM.Services.Providers
{
	public class DateTimeProvider : IDateTimeProvider
	{
		public DateTimeOffset GetDateTimeDateTimeOffset() => DateTimeOffset.UtcNow;
	}

}
