using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Layout;

namespace Nevron.Nov.Examples.Diagram
{
    public class NBasicShapesExample : NDiagramExampleBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NBasicShapesExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NBasicShapesExample()
        {
            NBasicShapesExampleSchema = NSchema.Create(typeof(NBasicShapesExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
        }

        #endregion

		#region Protected Overrides - Example

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

				// create all shapes
				NBasicShapeFactory factory = new NBasicShapeFactory();
				factory.DefaultSize = new NSize(120, 90);

				int row = 0, col = 0;
				double cellWidth = 240;
				double cellHeight = 150;

				for (int i = 0; i < factory.ShapeCount; i++, col++)
				{
					NShape shape = factory.CreateShape(i);
					shape.HorizontalPlacement = ENHorizontalPlacement.Center;
					shape.VerticalPlacement = ENVerticalPlacement.Center;
					shape.Text = factory.GetShapeInfo(i).Name;
					MoveTextBelowShape(shape);

					if (i == (int)ENBasicShape.ThreeDBox)
					{
						shape.TextBlock.Padding = new NMargins(0, 15, 0, 0);
					}
					else if (i == (int)ENBasicShape.Concentric)
					{
						shape.TextBlock.Angle = NAngle.Zero;
					}

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
						if (i == (int)ENBasicShape.CenterDragCircle)
						{
							beginPoint.Translate(cellWidth / 3, cellHeight / 3);
							endPoint.Translate(-cellWidth / 3, -cellHeight / 3);
						}

						shape.SetBeginPoint(beginPoint);
						shape.SetEndPoint(endPoint);
					}
					else
					{
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
    This example demonstrates the basic shapes, which are created by the NBasicShapeFactory.
</p>
";
        }

		#endregion
		
		#region Schema

		/// <summary>
        /// Schema associated with NBasicShapesExample.
        /// </summary>
        public static readonly NSchema NBasicShapesExampleSchema;

        #endregion
    }
}