using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NCompactDepthTreeLayoutExample : NLayoutExampleBase<NCompactDepthTreeLayout>
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NCompactDepthTreeLayoutExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NCompactDepthTreeLayoutExample()
        {
            NCompactDepthTreeLayoutExampleSchema = NSchema.Create(typeof(NCompactDepthTreeLayoutExample), NLayoutExampleBase<NCompactDepthTreeLayout>.NLayoutExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides

        protected override void InitDiagram()
        {
            base.InitDiagram();

            // create a random diagram 
            NGenericTreeTemplate template = new NGenericTreeTemplate();
            template.EdgesUserClass = "Connector";
            template.Balanced = false;
            template.Levels = 6;
            template.BranchNodes = 3;
            template.HorizontalSpacing = 10;
            template.VerticalSpacing = 10;
            template.VerticesSize = new NSize(50, 50);
            template.VertexSizeDeviation = 1;
            template.Create(m_DrawingDocument);

            // arrange the diagram
            ArrangeDiagram();

            // fit active page
            m_DrawingDocument.Content.ActivePage.ZoomMode = ENZoomMode.Fit;
        }

        protected override string GetExampleDescription()
        {
            return @"
<p>
    The compact depth tree layout represents a classical directed tree layout 
    (e.g. with uniform parent placement), which compacts the depth of the tree drawing area. 
    It produces both straight line and orthogonal tree drawings, which is controlled by the <b>OrthogonalEdgeRouting</b> property.    
	The <b>PlugSpacing</b> property controls the spacing between the plugs of a node.
	You can set a fixed amount of spacing or a proportional spacing, which means that the plugs
	are distributed along the whole side of the node.
    The layout satisfies to the following aesthetic criteria:
    <ul>
        <li>No edge crossings - edges do not cross each other.</li>
        <li>No vertex-vertex overlaps - vertices do not overlap each other.</li>
        <li>No vertex-edge overlaps - vertices do not overlap edges.</li>
        <li>Compactness - you can optimize the compactness of the drawing in the tree breadth dimension 
        by setting the <b>CompactBreadth</b> property to true. This type of layout is by default depth compact.</li>
    </ul>
</p>    
<p>
    This layout is very useful when arranging deep, unbalanced trees with different node sizes 
    (class diagrams being a perfect example). In cases like these the layout guarantees 
    that the drawing is with minimal depth.
</p>
<p>
	To experiment with the layout just change its properties from the property grid and click the <b>Layout</b> button. 
    To see the layout in action on a different trees, just click the <b>Random Tree</b> button. 
</p>
";
        }

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NCompactDepthTreeLayoutExample.
        /// </summary>
        public static readonly NSchema NCompactDepthTreeLayoutExampleSchema;

        #endregion
    }
}