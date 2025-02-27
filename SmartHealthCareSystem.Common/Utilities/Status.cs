namespace SmartHealthCareSystem.Common.Utilities
{
	public enum AppointmentStatus
	{
		SH = 0, // Scheduled
		RS = 1, // Rescheduled
		CN = 2, // Canceled
		NS = 3, // NoShow
		CM = 4  // Completed
	}
	public static class AppointmentStatusExtensions
	{
		private static readonly Dictionary<AppointmentStatus, string> StatusDictionary = new Dictionary<AppointmentStatus, string>
		{
			{ AppointmentStatus.SH, "Scheduled" },
			{ AppointmentStatus.RS, "Rescheduled" },
			{ AppointmentStatus.CN, "Canceled" },
			{ AppointmentStatus.NS, "No Show" },
			{ AppointmentStatus.CM, "Completed" }
		};

		public static string GetDescription(this AppointmentStatus status)
		{
			return StatusDictionary[status];
		}
	}
}
