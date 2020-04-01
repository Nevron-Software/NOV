using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NBalloonTreeLayoutExample : NLayoutExampleBase<NBalloonTreeLayout>
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NBalloonTreeLayoutExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NBalloonTreeLayoutExample()
        {
            NBalloonTreeLayoutExampleSchema = NSchema.Create(typeof(NBalloonTreeLayoutExample), NLayoutExampleBase<NBalloonTreeLayout>.NLayoutExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides

        protected override void InitDiagram()
        {
            base.InitDiagram();

            // create a template graph
            NGenericTreeTemplate tree = new NGenericTreeTemplate();
            tree.EdgesUserClass = "Connector";
            tree.Levels = 4;
            tree.BranchNodes = 4;
            tree.HorizontalSpacing = 10;
            tree.VerticalSpacing = 10;
            tree.ConnectorType = ENConnectorShape.RoutableConnector;
            tree.VerticesShape = ENBasicShape.Circle;
			tree.VerticesSize = new NSize(60, 60);
            tree.Create(m_DrawingDocument);

            // arrange diagram
            ArrangeDiagram();

            // fit active page
            m_DrawingDocument.Content.ActivePage.ZoomMode = ENZoomMode.Fit;
        }
        protected override string GetExampleDescription()
        {
            return @"
<p>
    The balloon tree layout tries to compact the drawing area of the tree 
    by placing the vertices in balloons around the tree root.
    It produces straight line tree drawings. 
</p>
<p>        
    Following is a brief description of its properties:
</p>
<ul>
	<li>
		<b>ParentChildSpacing</b> - the preferred spacing between a parent and a child
		vertex in the layout direction. The real spacing may be different for some nodes,
		because the layout does not allow overlapping.
	</li>
	<li>
		<b>VertexSpacing</b> - the minimal spacing between 2 nodes in the layout.
		If set to 0, the nodes may touch each other.
	</li>
	<li>
		<b>ChildWedge</b> - the sector angle (measured in degrees) for the children
		of each vertex.
	</li>
	<li>
		<b>RootWedge</b> - the sector angle (measured in degrees) for the children
		of the root vertex.
	</li>
	<li>
		<b>StartAngle</b> - the start angle for the children of the root vertex, measured in
		degrees anticlockwise from the x-axis.
	</li>
</ul>
<p>
	To experiment with the layout just change its properties from the property grid and click the <b>Layout</b> button.
</p>            
            ";
        }
        protected override ENBasicShape GetDefaultShapeType()
        {
            return ENBasicShape.Circle;
        }

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NBalloonTreeLayoutExample.
        /// </summary>
        public static readonly NSchema NBalloonTreeLayoutExampleSchema;

        #endregion
    }
}