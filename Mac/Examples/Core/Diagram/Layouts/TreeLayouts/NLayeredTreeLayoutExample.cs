using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NLayeredTreeLayoutExample : NLayoutExampleBase<NLayeredTreeLayout>
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NLayeredTreeLayoutExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NLayeredTreeLayoutExample()
        {
            NLayeredTreeLayoutExampleSchema = NSchema.Create(typeof(NLayeredTreeLayoutExample), NLayoutExampleBase<NLayeredTreeLayout>.NLayoutExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides

        protected override void InitDiagram()
        {
            base.InitDiagram();

            // create random diagram
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

            // arrange diagram
            ArrangeDiagram();

            // fit active page
            m_DrawingDocument.Content.ActivePage.ZoomMode = ENZoomMode.Fit;
        }

        protected override string GetExampleDescription()
        {
            return @"
	<p>
        The layered tree layout represents a classical directed tree layout 
        (e.g. with uniform parent placement), which places vertices from the same level in layers.
        It produces both straight line and orthogonal tree drawings, which is controlled by the <b>OrthogonalEdgeRouting</b> property.
		The <b>PlugSpacing</b> property controls the spacing between the plugs of a node.
		You can set a fixed amount of spacing or a proportional spacing, which means that the plugs
		are distributed along the whole side of the node.
        The layout satisfies the following aesthetic criteria:
        <ul>
            <li>No edge crossings - edges do not cross each other.</li>
            <li>No vertex-vertex overlaps - vertices do not overlap each other.</li>
            <li>No vertex-edge overlaps - in case of orthogonal edge routing, this criteria is met when the <b>BusBetweenLayers</b> property is set to true. </li>
            <li>Straight line routing - in case the <b>OrthogonalEdgeRouting</b> property is set to false you can consider modifying the
			    <b>PortStyle</b> property, which controls the anchoring of the lines to the vertex boxes.</li>
            <li>Compactness - you can optimize the compactness of the drawing in the tree breadth dimension 
            by setting the <b>CompactBreadth</b> property to true.</li>
        </ul>
    </p>    
    <p>
		To experiment with the layout just change its properties from the property grid 
		and click the <b>Layout</b> button.
	</p>
	<p>
		To see the layout in action on a different trees, just click the <b>Random Tree</b> buttons.
	</p>
";
        }

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NLayeredTreeLayoutExample.
        /// </summary>
        public static readonly NSchema NLayeredTreeLayoutExampleSchema;

        #endregion
    }
}