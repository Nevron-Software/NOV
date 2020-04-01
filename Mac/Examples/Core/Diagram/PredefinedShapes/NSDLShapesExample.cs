using Nevron.Nov.DataStructures;
using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Expressions;
using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Layout;

namespace Nevron.Nov.Examples.Diagram
{
	public class NSDLShapesExample : NDiagramExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NSDLShapesExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NSDLShapesExample()
		{
			NSDLShapesExampleSchema = NSchema.Create(typeof(NSDLShapesExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
		}

		#endregion

		#region Overrides

		protected override void InitDiagram()
		{
			base.InitDiagram();

			m_DrawingDocument.HistoryService.Pause();
			try
			{
				NDrawing drawing = m_DrawingDocument.Content;
				NPage activePage = drawing.ActivePage;

				// Hide grid and ports
				drawing.ScreenVisibility.ShowGrid = false;
				drawing.ScreenVisibility.ShowPorts = false;

				// Create all shapes
				NSdlShapeFactory factory = new NSdlShapeFactory();
				factory.DefaultSize = new NSize(80, 60);
				
				int row = 0, col = 0;
				double cellWidth = 180;
				double cellHeight = 120;

				for (int i = 0; i < factory.ShapeCount; i++, col++)
				{
					NShape shape = factory.CreateShape(i);
					shape.HorizontalPlacement = ENHorizontalPlacement.Center;
					shape.VerticalPlacement = ENVerticalPlacement.Center;
					shape.Text = factory.GetShapeInfo(i).Name;
					MoveTextBelowShape(shape);
					activePage.Items.Add(shape);

					if (col >= 5)
					{
						row++;
						col = 0;
					}

					NPoint beginPoint = new NPoint(50 + col * cellWidth, 50 + row * cellHeight);
					if (shape.ShapeType == ENShapeType.Shape1D)
					{
						NPoint endPoint = beginPoint + new NPoint(cellWidth - 100, cellHeight - 100);
						shape.SetBeginPoint(beginPoint);
						shape.SetEndPoint(endPoint);
					}
					else
					{
						shape.SetBounds(beginPoint.X, beginPoint.Y, shape.Width, shape.Height);
					}
				}
				
				// size page to content
                activePage.Layout.ContentPadding = new NMargins(40);
				activePage.SizeToContent();
			}
			finally
			{
				m_DrawingDocument.HistoryService.Resume();
			}
		}

		protected override string GetExampleDescription()
		{
			return @"
<p>
    This example demonstrates the SDL shapes, which are created by the NSdlShapeFactory.
</p>
";
		}

		#endregion
		
		#region Schema

		/// <summary>
		/// Schema associated with NSDLShapesExample.
		/// </summary>
		public static readonly NSchema NSDLShapesExampleSchema;

		#endregion
	}
}
