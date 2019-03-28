using System;
namespace Common.Helpers
{
	public class DateHelper
	{
		public static DateTime ConvertFromUnixSecondsTimestamp(long timestamp)
		{
			DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return epoch.AddSeconds(timestamp); ;
		}

		public static DateTime ConvertStringDateToDateTime(string stringdate)
		{
			return DateTime.Parse(stringdate);
		}

		public static string hourFormat(DateTime date)
		{
			string minute = date.Minute.ToString();
			if (minute.Length.Equals(1))
			{
				minute = "0" + date.Minute;
			}
			return minute;
		}
	}
}
