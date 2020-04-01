using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    /// <summary>
    /// 
    /// </summary>
    public class NGeometryCornerRoundingExample : NDiagramExampleBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NGeometryCornerRoundingExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NGeometryCornerRoundingExample()
        {
            NGeometryCornerRoundingExampleSchema = NSchema.Create(typeof(NGeometryCornerRoundingExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
        }

        #endregion

        #region Overrides from NDiagramExampleBase

        protected override string GetExampleDescription()
        {
            return @"
<p>
    Demonstrates the geometry corner rounding.
</p>
<p>
    In NOV diagram each geometry can be easily modified to have rounded corners.
</p>
";
        }
        protected override void InitDiagram()
        {
            base.InitDiagram();

            NDrawing drawing = m_DrawingDocument.Content;
            NPage activePage = drawing.ActivePage;

            // hide the grid
            drawing.ScreenVisibility.ShowGrid = false;

            // plotter commands
            NBasicShapeFactory basicShapes = new NBasicShapeFactory();
            NConnectorShapeFactory connectorFactory = new NConnectorShapeFactory();

            // create a rounded rect
            NShape rectShape = basicShapes.CreateShape(ENBasicShape.Rectangle);
            rectShape.DefaultShapeGlue = ENDefaultShapeGlue.GlueToGeometryIntersection;
            rectShape.Geometry.CornerRounding = 10;
            rectShape.SetBounds(50, 50, 100, 100);
            activePage.Items.Add(rectShape);

            // create a rounded pentagram
            NShape pentagramShape = basicShapes.CreateShape(ENBasicShape.Pentagram);
            pentagramShape.DefaultShapeGlue = ENDefaultShapeGlue.GlueToGeometryIntersection;
            pentagramShape.Geometry.CornerRounding = 20;
            pentagramShape.SetBounds(310, 310, 100, 100);
            activePage.Items.Add(pentagramShape);

            // create a rounded routable connector
            NShape connector = connectorFactory.CreateShape(ENConnectorShape.RoutableConnector); 
            connector.Geometry.CornerRounding = 30;
            connector.GlueBeginToShape(rectShape);
            connector.GlueEndToShape(pentagramShape);
            activePage.Items.Add(connector);
        }

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NGeometryCornerRoundingExample.
        /// </summary>
        public static readonly NSchema NGeometryCornerRoundingExampleSchema;

        #endregion
    }
}