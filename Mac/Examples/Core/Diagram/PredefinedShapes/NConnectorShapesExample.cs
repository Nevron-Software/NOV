using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;

namespace Nevron.Nov.Examples.Diagram
{
    public class NConnectorShapesExample : NDiagramExampleBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NConnectorShapesExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NConnectorShapesExample()
        {
            NConnectorShapesExampleSchema = NSchema.Create(typeof(NConnectorShapesExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
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

                drawing.ScreenVisibility.ShowGrid = false;

                // create all shapes
                NConnectorShapeFactory factory = new NConnectorShapeFactory();
                factory.DefaultSize = new NSize(120, 90);

                int row = 0, col = 0;
                double cellWidth = 300;
                double cellHeight = 200;

                for (int i = 0; i < factory.ShapeCount; i++, col++)
                {
                    NShape shape = factory.CreateShape(i);
                    shape.Text = factory.GetShapeInfo(i).Name;
                    activePage.Items.Add(shape);

                    if (col >= 4)
                    {
                        row++;
                        col = 0;
                    }

                    NPoint beginPoint = new NPoint(50 + col * cellWidth, 50 + row * cellHeight);
                    NPoint endPoint = beginPoint + new NPoint(cellWidth - 50, cellHeight - 50);
                    shape.SetBeginPoint(beginPoint);
                    shape.SetEndPoint(endPoint);
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
    This example demonstrates the connector shapes, which are created by the NConnectorShapeFactory.
</p>
";
        }

		#endregion

		#region Schema

		/// <summary>
        /// Schema associated with NConnectorShapesExample.
        /// </summary>
        public static readonly NSchema NConnectorShapesExampleSchema;

        #endregion
    }
}