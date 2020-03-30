using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NLayeredGraphLayoutExample : NLayoutExampleBase<NLayeredGraphLayout>
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NLayeredGraphLayoutExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NLayeredGraphLayoutExample()
        {
            NLayeredGraphLayoutExampleSchema = NSchema.Create(typeof(NLayeredGraphLayoutExample), NLayoutExampleBase<NLayeredGraphLayout>.NLayoutExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides

        protected override void InitDiagram()
        {
            base.InitDiagram();

            const double width = 40;
            const double height = 40;
            const double distance = 80;
            
            NBasicShapeFactory basicShapes = new NBasicShapeFactory();
            NPage activePage = m_DrawingDocument.Content.ActivePage;

            int[] from = new int[] { 1, 1, 1, 2, 2, 3, 3, 4, 4, 5, 6 };
            int[] to = new int[] { 2, 3, 4, 4, 5, 6, 7, 5, 9, 8, 9 };
            NShape[] shapes = new NShape[9];
            int vertexCount = shapes.Length, edgeCount = from.Length;
            int i, j, count = vertexCount + edgeCount;

            for (i = 0; i < count; i++)
            {
                if (i < vertexCount)
                {
                    j = vertexCount % 2 == 0 ? i : i + 1;
                    shapes[i] = basicShapes.CreateShape(ENBasicShape.Rectangle);

                    if (vertexCount % 2 != 0 && i == 0)
                    {
                        shapes[i].SetBounds(new NRectangle(
                            (width + (distance * 1.5)) / 2,
                            distance + (j / 2) * (distance * 1.5), 
                            width, 
                            height));
                    }
                    else
                    {
                        shapes[i].SetBounds(new NRectangle(
                            width / 2 + (j % 2) * (distance * 1.5),
                            height + (j / 2) * (distance * 1.5), 
                            width, 
                            height));
                    }

                    activePage.Items.Add(shapes[i]);
                }
                else
                {
                    NRoutableConnector edge = new NRoutableConnector();
                    edge.UserClass = "Connector";
                    activePage.Items.Add(edge);
                    edge.GlueBeginToShape(shapes[from[i - vertexCount] - 1]);
                    edge.GlueEndToShape(shapes[to[i - vertexCount] - 1]);
                }
            }

            // arrange diagram
            ArrangeDiagram();

            // fit active page
            m_DrawingDocument.Content.ActivePage.ZoomMode = ENZoomMode.Fit;
        }
        protected override string GetExampleDescription()
        {
            return @"
<p>     
    The layered graph layout is used to layout a graph in layers. 
    The layout strives to minimize the number of crossings between edges 
    and produces a polyline graph drawing. This type of layout is very useful
    for the arrangement of flow diagrams, since it works well on all types of graphs
    (including those with self-loops and duplicate edges).
</p>
<p>
	The most important properties are:
	<ul>
		<li>
			<b>Direction</b> - determines the flow direction of the layout. By default set to <i>TopToBottom</i>.
		</li>
		<li>
			<b>EdgeRouting</b> - determines what edge routing is applied. Possible values are:
			<ul>
			    <li><i>Polyline</i> - the edges are drawn using a polyline with few bends</li>
			    <li><i>Orthogonal</i> - the edges are drawn using only horizontal and vertical line segments</li>
			</ul>  
		</li>
		<li>
			<b>NodeRank</b> - specifies the node ranking policy used by the layout. It can be:
			<ul>
			    <li><i>TopMost</i> - all nodes without incoming edges are assigned to the topmost layer</li>
			    <li><i>Gravity</i> - layer distribution is done in such a way that the total length of all edges is minimized</li>
			    <li><i>Optimal</i> - similar to the topmost, but after the initial assignment all nodes fall downwards as much as possible</li>
			</ul>
		</li>
		<li>
			<b>PlugSpacing</b> - determines the spacing between the plugs of a node.
			You can set a fixed amount of spacing or a proportional spacing, which means that the plugs
			are distributed along the whole side of the node.
		</li>
		<li>
			<b>LayerAlignment</b> - determines the vertical alignment of the nodes in the layers.
		</li>
		<li>
			<b>NodeAlignment</b> - determines the horizontal alignment of the nodes in the layers.
		</li>
		<li>
			<b>SelfLoopSpacingFactor</b> - determines the self-loop spacing factor. It spaces the self-loops as a ratio of the body height.
		</li>
		<li>
			<b>VertexSpacing and LayerSpacing</b> - determine the spacing between nodes and layers
			respectively.
		</li>
		<li>
			<b>StraightenLines</b> - if turned on an additional step is performed that tries to
			straighten the lines as much as possible in the case of orthogonal edge routing.
		</li>
		<li>
			<b>UseSingleBus</b> - if true and the EdgeRouting is orthogonal, all edges will be
			placed on a single bus between each pair of layers.
		</li>
		<li>
			<b>Compact</b> - determines whether the layout should try to minimize the width of the drawing or not.
		</li>
	</ul>		
	You can set the index of a given node in its layer explicitly using the <b>IndexInLayer</b> property
	from the <b>LayoutData</b> collection of the node. By default the index is set to -1 which
	means that the layout will automatically calculate it. If you set a value greater than or equal
	to 0 the node will be placed at that index. If the index is greater than the total number
	of nodes in the layer, it will be set equal to the number of vertices - 1.
</p>
<p>
	To experiment with the layout just change its properties from the property grid and click the <b>Layout</b> button. 
    To see the layout in action on a different graph, just click the <b>Random Graph</b> button. 
</p>
            ";
        }
        protected override ENBasicShape GetDefaultShapeType()
        {
            return ENBasicShape.Rectangle;
        }

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NLayeredGraphLayoutExample.
        /// </summary>
        public static readonly NSchema NLayeredGraphLayoutExampleSchema;

        #endregion
    }
}