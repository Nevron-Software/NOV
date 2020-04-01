using Nevron.Nov.DataStructures;
using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Expressions;
using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Layout;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NFlowChartingShapesExample : NDiagramExampleBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NFlowChartingShapesExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NFlowChartingShapesExample()
        {
            NFlowChartingShapesExampleSchema = NSchema.Create(typeof(NFlowChartingShapesExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
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
				NFlowchartingShapeFactory factory = new NFlowchartingShapeFactory();
                factory.DefaultSize = new NSize(120, 90);

                for (int i = 0; i < factory.ShapeCount; i++)
                {
                    NShape shape = factory.CreateShape(i);
                    shape.HorizontalPlacement = ENHorizontalPlacement.Center;
                    shape.VerticalPlacement = ENVerticalPlacement.Center;
					shape.Text = factory.GetShapeInfo(i).Name;
					MoveTextBelowShape(shape);
					activePage.Items.Add(shape);
                }

                // arrange them
                NList<NShape> shapes = activePage.GetShapes(false);
                NLayoutContext layoutContext = new NLayoutContext();
                layoutContext.BodyAdapter = new NShapeBodyAdapter(m_DrawingDocument);
                layoutContext.GraphAdapter = new NShapeGraphAdapter();
                layoutContext.LayoutArea = activePage.Bounds;

                NTableFlowLayout flowLayout = new NTableFlowLayout();
                flowLayout.HorizontalSpacing = 30;
                flowLayout.VerticalSpacing = 50;
                flowLayout.Direction = ENHVDirection.LeftToRight;
                flowLayout.MaxOrdinal = 5;
                flowLayout.Arrange(shapes.CastAll<object>(), layoutContext);

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
    This example demonstrates the flow charting shapes, which are created by the NFlowChartingShapesFactory.
</p>
";
        }

		#endregion
		
		#region Schema

		/// <summary>
        /// Schema associated with NFlowChartingShapesExample.
        /// </summary>
        public static readonly NSchema NFlowChartingShapesExampleSchema;

        #endregion
    }
}