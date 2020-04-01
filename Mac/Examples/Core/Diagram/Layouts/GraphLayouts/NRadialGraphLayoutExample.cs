using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NRadialGraphLayoutExample : NLayoutExampleBase<NRadialGraphLayout>
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NRadialGraphLayoutExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NRadialGraphLayoutExample()
        {
            NRadialGraphLayoutExampleSchema = NSchema.Create(typeof(NRadialGraphLayoutExample), NLayoutExampleBase<NRadialGraphLayout>.NLayoutExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides

        protected override void InitDiagram()
        {
            base.InitDiagram();

            // create a random tre
            NGenericTreeTemplate tree = new NGenericTreeTemplate();
            tree.Levels = 4;
            tree.BranchNodes = 4;
            tree.HorizontalSpacing = 10;
            tree.VerticalSpacing = 10;
            tree.ConnectorType = ENConnectorShape.RoutableConnector;
            tree.VerticesShape = ENBasicShape.Circle;
            tree.VerticesSize = new NSize(40, 40);
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
    The radial graph layout layouts the graphs in concentric circles. The vertices with no
    predecessors are placed in the center and their descendants are placed on the next circle
    and so on. It produces a straight line graph drawing. The most important properties are:
</p>
<ul>
	<li>
		<b>Aspect Ratio</b> - determines the aspect (width/height) ratio of the layout.
		By default set to 1 which layouts the nodes in a circle. A value different from 1
		will make the layout order the nodes in an ellipse.
	</li>
	<li>
		<b>AutoSizeRings</b> - if set to true the RingRadius property is automatically
		calculated to have such value that the total area of the drawing is minimized and there
		is no node overlapping.
	</li>
	<li>
		<b>RingRadius</b> - determines the size of the radius of the first imaginary circle where
		nodes are placed. The radius of each other circle is a sum of the RingRadius value and
		the radius of the previous circle. This value is automatically determined if the 
		AutoSizeRings property is set to true.
	</li>
</ul>
<p>
	To experiment with their behavior just change the properties of the layout in the property
	grid and click the <b>Layout</b> button.
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
        /// Schema associated with NRadialGraphLayoutExample.
        /// </summary>
        public static readonly NSchema NRadialGraphLayoutExampleSchema;

        #endregion
    }
}