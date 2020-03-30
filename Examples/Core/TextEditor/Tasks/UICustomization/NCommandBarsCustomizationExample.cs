using Nevron.Nov.Dom;
using Nevron.Nov.Text;
using Nevron.Nov.Text.Commands;
using Nevron.Nov.Text.UI;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
	public class NCommandBarsCustomizationExample : NTextExampleBase
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
			NCommandBarsCustomizationExampleSchema = NSchema.Create(typeof(NCommandBarsCustomizationExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Public Overrides

		public override void Initialize()
		{
			base.Initialize();

			// Add the custom command action to the rich text view's commander
			m_RichText.Commander.Add(new CustomCommandAction());

			// Remove the "Edit" menu and insert a custom one
			m_CommandBarBuilder = new NRichTextCommandBarBuilder();
			m_CommandBarBuilder.MenuDropDownBuilders.Remove(NLoc.Get("Edit"));
			m_CommandBarBuilder.MenuDropDownBuilders.Insert(1, new CustomMenuBuilder());

			// Remove the "Standard" toolbar and insert a custom one
			m_CommandBarBuilder.ToolBarBuilders.Remove(NLoc.Get("Standard"));
			m_CommandBarBuilder.ToolBarBuilders.Insert(0, new CustomToolBarBuilder());

			// Remove the rich text view from its parent and recreate the command bar UI
			m_RichText.ParentNode.RemoveChild(m_RichText);
			m_ExampleTabPage.Content = m_CommandBarBuilder.CreateUI(m_RichText);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Ribbon Customization",
				"This example demonstrates how to customize the NOV rich text command bars (menus and toolbars).", 1));
		}
		protected override NWidget CreateExampleControls()
		{
			return null;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>This example demonstrates how to customize the NOV rich text command bars (menus and toolbars).</p>
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
				// Add the "Copy" menu item
				items.Add(CreateMenuItem(Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, NRichTextView.CopyCommand));

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
				// Add the "Copy" button
				items.Add(CreateButton(Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, NRichTextView.CopyCommand));

				// Add the custom command button
				items.Add(CreateButton(NResources.Image_Ribbon_16x16_smiley_png, CustomCommand));
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