using System;
using Nevron.Nov.Dom;
using Nevron.Nov.Schedule;

namespace Nevron.Nov.Examples.Schedule
{
	public class NRecurrenceStressTestExample : NScheduleExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NRecurrenceStressTestExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NRecurrenceStressTestExample()
		{
			NRecurrenceStressTestExampleSchema = NSchema.Create(typeof(NRecurrenceStressTestExample), NScheduleExampleBase.NScheduleExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void InitSchedule(NSchedule schedule)
		{
			DateTime today = DateTime.Today;
			DateTime startDate = new DateTime(today.Year, 1, 1);

			// Create an appointment which occurs per 3 hours
			NAppointment appointment = new NAppointment("Meeting", today, today.AddHours(2));
			NRecurrenceHourlyRule rule = new NRecurrenceHourlyRule();
			rule.StartDate = startDate;
			rule.Interval = 3;
			appointment.RecurrenceRule = rule;
			schedule.Appointments.Add(appointment);

			// Create an appointment which occurs every hour and categorize it
			appointment = new NAppointment("Talking", today, today.AddHours(0.5));
			rule = new NRecurrenceHourlyRule();
			rule.StartDate = startDate;
			rule.Interval = 1;
			appointment.RecurrenceRule = rule;
			appointment.Category = "Red";
			schedule.Appointments.Add(appointment);

			// Swicth schedule to week view mode
			schedule.ViewMode = ENScheduleViewMode.Week;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
    This example demonstrates how NOV Schedule deals with a large number of occurrences of recurring appointments.
</p>
";
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NRecurrenceStressTestExample.
		/// </summary>
		public static readonly NSchema NRecurrenceStressTestExampleSchema;

		#endregion
	}
}