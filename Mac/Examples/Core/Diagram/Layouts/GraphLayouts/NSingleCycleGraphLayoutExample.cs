using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NSingleCycleGraphLayoutExample : NLayoutExampleBase<NSingleCycleGraphLayout>
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NSingleCycleGraphLayoutExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NSingleCycleGraphLayoutExample()
        {
            NSingleCycleGraphLayoutExampleSchema = NSchema.Create(typeof(NSingleCycleGraphLayoutExample), NLayoutExampleBase<NSingleCycleGraphLayout>.NLayoutExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides

        protected override void InitDiagram()
        {
            base.InitDiagram();

            // create a tree
            NGenericTreeTemplate tree = new NGenericTreeTemplate();
            tree.ConnectorType = ENConnectorShape.RoutableConnector;
            tree.VerticesShape = ENBasicShape.Circle;
            tree.Levels = 6;
            tree.BranchNodes = 2;
            tree.HorizontalSpacing = 10;
            tree.VerticalSpacing = 10;
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
    The single cycle layout layouts all graph vertices on a single circle, trying to minimize the
    number of edge crossings. The most important properties are:
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
		<b>RingRadius</b> - determines the size of the radius of the imaginary circle where
		nodes are placed. This value is automatically determined if the AutoSizeRings property
		is set to true.
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
        /// Schema associated with NSingleCycleGraphLayoutExample.
        /// </summary>
        public static readonly NSchema NSingleCycleGraphLayoutExampleSchema;

        #endregion
    }
}