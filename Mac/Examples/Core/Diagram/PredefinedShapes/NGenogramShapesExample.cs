using System;
using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Layout;

namespace Nevron.Nov.Examples.Diagram
{
	public class NGenogramShapesExample : NDiagramExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NGenogramShapesExample()
		{ 
			
		}

		/// <summary>
		/// Static constructor
		/// </summary>
		static NGenogramShapesExample()
		{
			NGenogramShapesExampleSchema = NSchema.Create(typeof(NGenogramShapesExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
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
				NGenogramShapeFactory factory = new NGenogramShapeFactory();				

				int row = 0, col = 0;
				double cellWidth = 240;
				double cellHeight = 150;

				for (int i = 0; i < factory.ShapeCount; i++, col++)
				{
					NShape shape = factory.CreateShape(i);
					shape.HorizontalPlacement = ENHorizontalPlacement.Center;
					shape.VerticalPlacement = ENVerticalPlacement.Center;

					NTextBlock textBlock = shape.GetFirstDescendant<NTextBlock>(); 

					if (textBlock == null || 
						i == (int)ENGenogramShape.Male ||
					    i == (int)ENGenogramShape.Female ||
					    i == (int)ENGenogramShape.Pet ||
					    i == (int)ENGenogramShape.UnknownGender)
					{
						textBlock = (NTextBlock)shape.TextBlock;
					}
					
					textBlock.Text = factory.GetShapeInfo(i).Name;

					activePage.Items.Add(shape);

					if (col >= 4)
					{
						row++;
						col = 0;
					}

					NPoint beginPoint = new NPoint(50 + col * cellWidth, 50 + row * cellHeight);
					if (shape.ShapeType == ENShapeType.Shape1D)
					{
						NPoint endPoint = beginPoint + new NPoint(cellWidth - 50, cellHeight - 50);						

						shape.SetBeginPoint(beginPoint);
						shape.SetEndPoint(endPoint);
					}
					else
					{						
						textBlock.SetFx(NTextBlock.PinYProperty, "$Parent.Height + Height + 10");
						textBlock.ResizeMode = ENTextBlockResizeMode.TextSize;
						shape.SetBounds(beginPoint.X, beginPoint.Y, shape.Width, shape.Height);
					}					
				}

				// size page to content
                activePage.Layout.ContentPadding = new NMargins(50);
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
    This example demonstrates the genogram shapes, which are created by the NGenogramShapeFactory.
</p>
";
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NGenogramShapesExample.
		/// </summary>
		public static readonly NSchema NGenogramShapesExampleSchema;

		#endregion
	}
}
