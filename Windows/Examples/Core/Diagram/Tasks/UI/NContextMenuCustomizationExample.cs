using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.DrawingCommands;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
	public class NContextMenuCustomizationExample : NDiagramExampleBase
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
			NContextMenuCustomizationExampleSchema = NSchema.Create(typeof(NContextMenuCustomizationExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
		}

		#endregion

		#region Public Overrides

		public override void Initialize()
		{
			base.Initialize();

			// Add the custom command action to the drawing view's commander
			m_DrawingView.Commander.Add(new CustomCommandAction());

			// Change the context menu factory to the custom one
			m_DrawingView.ContextMenu = new CustomContextMenu();
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
<p>This example demonstrates how to customize the NOV drawing view's context menu. A custom command is added
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

		public class CustomContextMenu : NDrawingContextMenu
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
				CustomContextMenuSchema = NSchema.Create(typeof(CustomContextMenu), NDrawingContextMenu.NDrawingContextMenuSchema);
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