using System;
using Nevron.Nov.Dom;
using Nevron.Nov.Schedule;

namespace Nevron.Nov.Examples.Schedule
{
	public class NAppointmentsExample : NScheduleExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NAppointmentsExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NAppointmentsExample()
		{
			NAppointmentsExampleSchema = NSchema.Create(typeof(NAppointmentsExample), NScheduleExampleBase.NScheduleExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void InitSchedule(NSchedule schedule)
		{
			DateTime today = DateTime.Today;
			schedule.ViewMode = ENScheduleViewMode.Day;

			schedule.Appointments.Add(new NAppointment("Travel to Work", today.AddHours(6.5), today.AddHours(7.5)));
			schedule.Appointments.Add(new NAppointment("Meeting with John", today.AddHours(8), today.AddHours(10)));
			schedule.Appointments.Add(new NAppointment("Conference", today.AddHours(10.5), today.AddHours(11.5)));
			schedule.Appointments.Add(new NAppointment("Lunch", today.AddHours(12), today.AddHours(14)));
			schedule.Appointments.Add(new NAppointment("News Reading", today.AddHours(12.5), today.AddHours(13.5)));
			schedule.Appointments.Add(new NAppointment("Video Presentation", today.AddHours(14.5), today.AddHours(15.5)));
			schedule.Appointments.Add(new NAppointment("Web Meeting", today.AddHours(16), today.AddHours(17)));
			schedule.Appointments.Add(new NAppointment("Travel back home", today.AddHours(17.5), today.AddHours(19)));
			schedule.Appointments.Add(new NAppointment("Family Dinner", today.AddHours(20), today.AddHours(21)));
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
    This example demonstrates how to create and add appointments to a schedule.
</p>
";
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NAppointmentsExample.
		/// </summary>
		public static readonly NSchema NAppointmentsExampleSchema;

		#endregion
	}
}