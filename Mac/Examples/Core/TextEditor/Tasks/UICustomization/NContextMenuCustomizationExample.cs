using Nevron.Nov.Dom;
using Nevron.Nov.Text;
using Nevron.Nov.Text.Commands;
using Nevron.Nov.Text.UI;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
	public class NContextMenuCustomizationExample : NTextExampleBase
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
			NContextMenuCustomizationExampleSchema = NSchema.Create(typeof(NContextMenuCustomizationExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Public Overrides

		public override void Initialize()
		{
			base.Initialize();

			// Add the custom command action to the rich text view's commander
			m_RichText.Commander.Add(new CustomCommandAction());

			// Get the context menu builder
			NRichTextContextMenuBuilder builder = m_RichText.ContextMenuBuilder;

			// Remove the common menu group, which contains commands such as Cut, Copy, Paste, etc.
			builder.RemoveGroup(NRichTextMenuGroup.Common);

			// Add the custom context menu group
			builder.AddGroup(new CustomMenuGroup());
		}

		#endregion

		#region Protected Overrides - Example

		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Context Menu Customization",
				"The example demonstrates how to customize the NOV rich text context menu.", 1));
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>This example demonstrates how to customize the NOV rich text context menu. The common menu group,
which contains command such as Cut, Copy, Paste, etc. is removed and a custom menu group which contains
only the Copy command and a custom command is added to the context menu builder of the rich text view's
selection.</p>
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

		#region Nested Types - Custom Menu Group

		/// <summary>
		/// A custom menu group, which applies to every text element and adds only a Copy menu item
		/// and a custom command menu item.
		/// </summary>
		public class CustomMenuGroup : NRichTextMenuGroup
		{
			/// <summary>
			/// Default constructor.
			/// </summary>
			public CustomMenuGroup()
				: base("Custom")
			{
			}

			/// <summary>
			/// Gets whether this context menu group should be shown for the given
			/// text element. Overriden to return true for every text element.
			/// </summary>
			/// <param name="textElement"></param>
			/// <returns></returns>
			public override bool AppliesTo(NTextElement textElement)
			{
				return true;
			}
			/// <summary>
			/// Appends the context menu action items from this group to the given menu.
			/// </summary>
			/// <param name="menu">The menu to append to.</param>
			/// <param name="textElement">The text element this menu is built for.</param>
			public override void AppendActionsTo(NMenu menu, NTextElement textElement)
			{
				// Add the "Copy" command
				menu.Items.Add(CreateMenuItem(Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, NRichTextView.CopyCommand));

				// Add the custom command
				menu.Items.Add(CreateMenuItem(NResources.Image_Ribbon_16x16_smiley_png, CustomCommand));
			}
		}

		#endregion

		#region Nested Types - Custom Command Action

		public class CustomCommandAction : NTextCommandAction
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
				CustomCommandActionSchema = NSchema.Create(typeof(CustomCommandAction), NTextCommandAction.NTextCommandActionSchema);
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
				INRichTextView richTextView = GetRichTextView(target);

				NMessageBox.Show("Rich Text Custom Command executed!", "Custom Command", ENMessageBoxButtons.OK,
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