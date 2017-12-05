using System;

namespace EasyCQRS.Web.Features.Shared
{
	public class DateRange
	{
		private DateRange(DateTime startDate, DateTime endDate)
		{
			Start = startDate;
			End = endDate;
		}

		public DateTime Start { get; }

		public DateTime End { get; }

		public static DateRange Create(DateTime start, DateTime end)
		{
			return new DateRange(start, end);
		}
	}
}