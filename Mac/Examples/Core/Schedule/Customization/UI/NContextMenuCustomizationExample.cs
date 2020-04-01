using System;

using Nevron.Nov.Dom;
using Nevron.Nov.Schedule;
using Nevron.Nov.Schedule.Commands;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Schedule
{
	public class NContextMenuCustomizationExample : NScheduleExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NContextMenuCustomizationExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NContextMenuCustomizationExample()
		{
			NContextMenuCustomizationExampleSchema = NSchema.Create(typeof(NContextMenuCustomizationExample), NScheduleExampleBase.NScheduleExampleBaseSchema);
		}

		#endregion

		#region Public Overrides

		public override void Initialize()
		{
			base.Initialize();

			// Add the custom command action to the schedule view's commander
			m_ScheduleView.Commander.Add(new CustomCommandAction());

			// Change the context menu factory to the custom one
			m_ScheduleView.ContextMenu = new CustomContextMenu();
		}

		#endregion

		#region Protected Overrides - Example

		protected override void InitSchedule(NSchedule schedule)
		{
			DateTime start = DateTime.Now;

			// Create an appointment
			NAppointment appointment = new NAppointment("Meeting", start, start.AddHours(2));
			schedule.Appointments.Add(appointment);
			schedule.ScrollToTime(start.TimeOfDay);
		}
		protected override NWidget CreateExampleControls()
		{
			return null;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>This example demonstrates how to customize the NOV schedule context menu. A custom command is added
at the end of the context menu.</p>
";
		}		

		#endregion

		#region Commands

		public static readonly NCommand CustomCommand = NCommand.Create(typeof(NContextMenuCustomizationExample), "CustomCommand", "Custom Command");

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NContextMenuCustomizationExample.
		/// </summary>
		public static readonly NSchema NContextMenuCustomizationExampleSchema;

		#endregion

		#region Nested Types - Custom Context Menu Factory

		public class CustomContextMenu : NScheduleContextMenu
		{
			/// <summary>
			/// Default constructor.
			/// </summary>
			public CustomContextMenu()
			{
			}
			/// <summary>
			/// Static constructor.
			/// </summary>
			static CustomContextMenu()
			{
				CustomContextMenuSchema = NSchema.Create(typeof(CustomContextMenu), NScheduleContextMenu.NScheduleContextMenuSchema);
			}

			protected override void CreateCustomCommands(NMenu menu)
			{
				base.CreateCustomCommands(menu);

				// Create a context menu builder
				NContextMenuBuilder builder = new NContextMenuBuilder();

				// Add a custom command
				builder.AddMenuItem(menu, NResources.Image_Ribbon_16x16_smiley_png, CustomCommand);
			}

			/// <summary>
			/// Schema associated with CustomContextMenu.
			/// </summary>
			public static readonly NSchema CustomContextMenuSchema;
		}

		#endregion

		#region Nested Types - Custom Command Action

		public class CustomCommandAction : NScheduleCommandAction
		{
			#region Constructors

			/// <summary>
			/// Default constructor.
			/// </summary>
			public CustomCommandAction()
			{
			}

			/// <summary>
			/// Static constructor.
			/// </summary>
			static CustomCommandAction()
			{
				CustomCommandActionSchema = NSchema.Create(typeof(CustomCommandAction), NScheduleCommandAction.NScheduleCommandActionSchema);
			}

			#endregion

			#region Public Overrides

			/// <summary>
			/// Gets the command associated with this command action.
			/// </summary>
			/// <returns></returns>
			public override NCommand GetCommand()
			{
				return CustomCommand;
			}
			/// <summary>
			/// Executes the command action.
			/// </summary>
			/// <param name="target"></param>
			/// <param name="parameter"></param>
			public override void Execute(NNode target, object parameter)
			{
				NScheduleView scheduleView = GetScheduleView(target);

				NMessageBox.Show("Schedule Custom Command executed!", "Custom Command", ENMessageBoxButtons.OK,
					ENMessageBoxIcon.Information);
			}

			#endregion

			#region Schema

			/// <summary>
			/// Schema associated with CustomCommandAction.
			/// </summary>
			public static readonly NSchema CustomCommandActionSchema;

			#endregion
		}

		#endregion
	}
}