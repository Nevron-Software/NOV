using Nevron.Nov.Dom;
using Nevron.Nov.Text;
using Nevron.Nov.Text.Commands;
using Nevron.Nov.Text.UI;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
	public class NRibbonCustomizationExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NRibbonCustomizationExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NRibbonCustomizationExample()
		{
			NRibbonCustomizationExampleSchema = NSchema.Create(typeof(NRibbonCustomizationExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Public Overrides

		public override void Initialize()
		{
			base.Initialize();

			// Add the custom command action to the rich text view's commander
			m_RichText.Commander.Add(new CustomCommandAction());

			// Rename the "Home" ribbon tab page
			NRibbonTabPageBuilder homeTabBuilder = m_RibbonBuilder.TabPageBuilders[NRichTextRibbonBuilder.TabPageHomeName];
			homeTabBuilder.Name = "Start";

			// Rename the "Font" ribbon group of the "Home" tab page
			NRibbonGroupBuilder fontGroupBuilder = homeTabBuilder.RibbonGroupBuilders[NHomeTabPageBuilder.GroupFontName];
			fontGroupBuilder.Name = "Text";

			// Remove the "Clipboard" ribbon group of the "Home" tab page
			homeTabBuilder.RibbonGroupBuilders.Remove(NHomeTabPageBuilder.GroupClipboardName);

			// Insert the custom ribbon group at the beginning of the home tab page
			homeTabBuilder.RibbonGroupBuilders.Insert(0, new CustomRibbonGroup());

			// Remove the rich text view from its parent and recreate the UI
			m_RichText.ParentNode.RemoveChild(m_RichText);
			m_ExampleTabPage.Content = m_RibbonBuilder.CreateUI(m_RichText);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Ribbon Customization",
				"This example demonstrates how to customize the NOV rich text ribbon.", 1));
		}
		protected override NWidget CreateExampleControls()
		{
			return null;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>This example demonstrates how to customize the NOV rich text ribbon.</p>
";
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NRibbonCustomizationExample.
		/// </summary>
		public static readonly NSchema NRibbonCustomizationExampleSchema;

		#endregion

		#region Commands

		public static readonly NCommand CustomCommand = NCommand.Create(typeof(NRibbonCustomizationExample), "CustomCommand", "Custom Command");

		#endregion

		#region Nested Types - Custom Ribbon Group Builder

		public class CustomRibbonGroup : NRibbonGroupBuilder
		{
			public CustomRibbonGroup()
				: base("Custom Group", NResources.Image_Ribbon_16x16_smiley_png)
			{
			}

			protected override void AddRibbonGroupItems(NRibbonGroupItemCollection items)
			{
				// Add the copy command
				items.Add(CreateRibbonButton(NResources.Image_Ribbon_32x32_clipboard_copy_png,
					Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, NRichTextView.CopyCommand));

				// Add the custom command
				items.Add(CreateRibbonButton(NResources.Image_Ribbon_32x32_smiley_png,
					NResources.Image_Ribbon_16x16_smiley_png, CustomCommand));
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