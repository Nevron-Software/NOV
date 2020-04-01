using Nevron.Nov.Dom;
using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.DrawingCommands;
using Nevron.Nov.UI;
using Nevron.Nov.Diagram.Shapes;

namespace Nevron.Nov.Examples.Diagram
{
	public class NCommandBarsCustomizationExample : NDiagramExampleBase
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
			NCommandBarsCustomizationExampleSchema = NSchema.Create(typeof(NCommandBarsCustomizationExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
		}

		#endregion

		#region Public Overrides

		public override void Initialize()
		{
			base.Initialize();

			// Add the custom command action to the drawing view's commander
			m_DrawingView.Commander.Add(new CustomCommandAction());

			// Remove the "Edit" menu and insert a custom one
			m_CommandBarBuilder = new NDiagramCommandBarBuilder();
			m_CommandBarBuilder.MenuDropDownBuilders.Remove(NDiagramCommandBarBuilder.MenuEditName);
			m_CommandBarBuilder.MenuDropDownBuilders.Insert(1, new CustomMenuBuilder());

			// Remove the "Standard" toolbar and insert a custom one
			m_CommandBarBuilder.ToolBarBuilders.Remove(NDiagramCommandBarBuilder.ToolbarStandardName);
			m_CommandBarBuilder.ToolBarBuilders.Insert(0, new CustomToolBarBuilder());

			// Remove the drawing view from its parent and recreate the command bar UI
			m_DrawingView.ParentNode.RemoveChild(m_DrawingView);
			m_ExampleTabPage.Content = m_CommandBarBuilder.CreateUI(m_DrawingView);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void InitDiagram()
		{
			base.InitDiagram();

			NBasicShapeFactory factory = new NBasicShapeFactory();
			NShape shape = factory.CreateShape(ENBasicShape.Rectangle);
			shape.SetBounds(100, 100, 150, 100);

			NPage activePage = m_DrawingDocument.Content.ActivePage;
			activePage.Items.Add(shape);
		}
		protected override NWidget CreateExampleControls()
		{
			return null;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>This example demonstrates how to customize the NOV diagram command bars (menus and toolbars).</p>
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
				items.Add(CreateMenuItem(Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, NDrawingView.CopyCommand));

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
				items.Add(CreateButton(Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, NDrawingView.CopyCommand));

				// Add the custom command button
				items.Add(CreateButton(NResources.Image_Ribbon_16x16_smiley_png, CustomCommand));
			}
		}

		#endregion

		#region Nested Types - Custom Command Action

		public class CustomCommandAction : NDrawingCommandAction
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
				CustomCommandActionSchema = NSchema.Create(typeof(CustomCommandAction), NDrawingCommandAction.NDrawingCommandActionSchema);
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
				NDrawingView drawingView = GetDrawingView(target);

				NMessageBox.Show("Drawing Custom Command executed!", "Custom Command", ENMessageBoxButtons.OK,
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