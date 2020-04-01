using System;

using Nevron.Nov.Dom;
using Nevron.Nov.Schedule;
using Nevron.Nov.Schedule.Commands;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Schedule
{
	public class NCommandBarsCustomizationExample : NScheduleExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NCommandBarsCustomizationExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NCommandBarsCustomizationExample()
		{
			NCommandBarsCustomizationExampleSchema = NSchema.Create(typeof(NCommandBarsCustomizationExample), NScheduleExampleBase.NScheduleExampleBaseSchema);
		}

		#endregion

		#region Public Overrides

		public override void Initialize()
		{
			base.Initialize();

			// Add the custom command action to the schedule view's commander
			m_ScheduleView.Commander.Add(new CustomCommandAction());

			// Remove the "Edit" menu and insert a custom one
			m_CommandBarBuilder = new NScheduleCommandBarBuilder();
			m_CommandBarBuilder.MenuDropDownBuilders.Remove(NScheduleCommandBarBuilder.MenuEditName);
			m_CommandBarBuilder.MenuDropDownBuilders.Insert(1, new CustomMenuBuilder());

			// Remove the "Standard" toolbar and insert a custom one
			m_CommandBarBuilder.ToolBarBuilders.Remove(NScheduleCommandBarBuilder.ToolbarStandardName);
			m_CommandBarBuilder.ToolBarBuilders.Insert(0, new CustomToolBarBuilder());

			// Remove the schedule view from its parent and recreate the command bar UI
			m_ScheduleView.ParentNode.RemoveChild(m_ScheduleView);
			m_ExampleTabPage.Content = m_CommandBarBuilder.CreateUI(m_ScheduleView);
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
<p>This example demonstrates how to customize the NOV schedule command bars (menus and toolbars).</p>
";
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NCommandBarsCustomizationExample.
		/// </summary>
		public static readonly NSchema NCommandBarsCustomizationExampleSchema;

		#endregion

		#region Commands

		public static readonly NCommand CustomCommand = NCommand.Create(typeof(NCommandBarsCustomizationExample), "CustomCommand", "Custom Command");

		#endregion

		#region Nested Types - Custom Menu Builder

		public class CustomMenuBuilder : NMenuDropDownBuilder
		{
			public CustomMenuBuilder()
				: base("Custom Menu")
			{
			}

			protected override void AddItems(NMenuItemCollection items)
			{
				// Add the "Add Appointment" menu item
				items.Add(CreateMenuItem(Nevron.Nov.Schedule.NResources.Image_Edit_AddAppointment_png, NScheduleView.AddAppointmentCommand));

				// Add the custom command menu item
				items.Add(CreateMenuItem(NResources.Image_Ribbon_16x16_smiley_png, CustomCommand));
			}
		}

		#endregion

		#region Nested Types - Custom ToolBar Builder

		public class CustomToolBarBuilder : NToolBarBuilder
		{
			public CustomToolBarBuilder()
				: base("Custom Toolbar")
			{
			}

			protected override void AddItems(NCommandBarItemCollection items)
			{
				// Add the "Add Appointment" button
				items.Add(CreateButton(Nevron.Nov.Schedule.NResources.Image_Edit_AddAppointment_png, NScheduleView.AddAppointmentCommand));

				// Add the custom command button
				items.Add(CreateButton(NResources.Image_Ribbon_16x16_smiley_png, CustomCommand));
			}
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