using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Expressions;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Layout;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
	public class NCalloutShapesExample : NDiagramExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NCalloutShapesExample()
		{

		}

		/// <summary>
		/// Static constructor
		/// </summary>
		static NCalloutShapesExample()
		{
			NCalloutShapesExampleSchema = NSchema.Create(typeof(NCalloutShapesExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
		}

		#endregion

		#region Protected overrides 

		protected override void InitDiagram()
		{
			const double XStep = 150;
			const double YStep = 100;

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
				NCalloutShapeFactory factory = new NCalloutShapeFactory();
				factory.DefaultSize = new NSize(70, 70);

				double x = 0;
				double y = 0;

				for (int i = 0; i < factory.ShapeCount; i++)
				{
					NShape shape = factory.CreateShape(i);
					shape.HorizontalPlacement = ENHorizontalPlacement.Center;
					shape.VerticalPlacement = ENVerticalPlacement.Center;
					shape.Tooltip = new NTooltip(factory.GetShapeInfo(i).Name);
					activePage.Items.Add(shape);

					if (shape.ShapeType == ENShapeType.Shape1D)
					{
						shape.SetBeginPoint(new NPoint(x, y));
						shape.SetEndPoint(new NPoint(x + shape.Width, y + shape.Height));
					}
					else
					{
						shape.SetBounds(x, y, shape.Width, shape.Height);
					}

					x += XStep;
					if (x > activePage.Width)
					{
						x = 0;
						y += YStep;
					}
				}

				// size page to content
                activePage.Layout.ContentPadding = new NMargins(70, 60, 70, 60);
				activePage.SizeToContent();
			}
			finally
			{
				m_DrawingDocument.HistoryService.Resume();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override string GetExampleDescription()
		{
			return @"
<p>
    This example demonstrates the callout shapes, which are created by the NCalloutShapeFactory.
</p>
";
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NCalloutShapesExample.
		/// </summary>
		public static readonly NSchema NCalloutShapesExampleSchema;

		#endregion
	}
}