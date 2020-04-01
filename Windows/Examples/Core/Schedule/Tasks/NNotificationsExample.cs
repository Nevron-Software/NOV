using System;
using Nevron.Nov.Dom;
using Nevron.Nov.Schedule;

namespace Nevron.Nov.Examples.Schedule
{
	public class NNotificationsExample : NScheduleExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NNotificationsExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NNotificationsExample()
		{
			NNotificationsExampleSchema = NSchema.Create(typeof(NNotificationsExample), NScheduleExampleBase.NScheduleExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void InitSchedule(NSchedule schedule)
		{
			schedule.ViewMode = ENScheduleViewMode.Day;

			// Create an old appointment
			DateTime oldStart = DateTime.Now.AddHours(-3);
			NAppointment oldAppointment = new NAppointment("Old Meeting", oldStart, oldStart.AddHours(2));
			oldAppointment.Notification = TimeSpan.Zero;
			schedule.Appointments.Add(oldAppointment);

			// Create an appointment and assign a notification 10 minutes before its start
			DateTime newStart = DateTime.Now.AddMinutes(10);
			NAppointment newAppointment = new NAppointment("New Meeting", newStart, newStart.AddHours(2));
			newAppointment.Notification = TimeSpan.FromMinutes(10);
			schedule.Appointments.Add(newAppointment);

			// Scroll the schedule to the current hour
			schedule.ScrollToTime(TimeSpan.FromHours(Math.Floor((double)oldStart.Hour)));

			// Configure the schedule view to check for pending notifications every 60 seconds
			m_ScheduleView.NotificationCheckInterval = 60;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
    This example demonstrates how to assign notifications to appointments and how to configure NOV Schedule to
	show notification messages.
</p>
";
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NNotificationsExample.
		/// </summary>
		public static readonly NSchema NNotificationsExampleSchema;

		#endregion
	}
}