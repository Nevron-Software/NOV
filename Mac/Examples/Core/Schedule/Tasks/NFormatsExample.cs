using System;

using Nevron.Nov.Dom;
using Nevron.Nov.Schedule;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Schedule
{
	public class NFormatsExample : NScheduleExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NFormatsExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NFormatsExample()
		{
			NFormatsExampleSchema = NSchema.Create(typeof(NFormatsExample), NScheduleExampleBase.NScheduleExampleBaseSchema);
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
		protected override NWidget CreateExampleControls()
		{
			NStackPanel stack = (NStackPanel)base.CreateExampleControls();

			NButton loadButton = new NButton("Load...");
			loadButton.Click += OnLoadButtonClick;
			stack.Add(loadButton);

			NButton saveButton = new NButton("Save...");
			saveButton.Click += OnSaveButtonClick;
			stack.Add(saveButton);

			return stack;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
    This example demonstrates how to load and save schedule documents from and to all currently
	supported formats using the default open and save dialogs of the schedule view.
</p>
";
		}

		#endregion

		#region Event Handlers

		private void OnLoadButtonClick(NEventArgs arg)
		{
			// Calling the LoadFromFile method without arguments will show the Open File
			// dialog of the schedule view
			m_ScheduleView.LoadFromFile();
		}
		private void OnSaveButtonClick(NEventArgs arg)
		{
			// Calling the SaveToFile method without arguments will show the Save File
			// dialog of the schedule view
			m_ScheduleView.SaveToFile();
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NFormatsExample.
		/// </summary>
		public static readonly NSchema NFormatsExampleSchema;

		#endregion
	}
}