using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NOrthogonalGraphLayoutExample : NLayoutExampleBase<NOrthogonalGraphLayout>
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NOrthogonalGraphLayoutExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NOrthogonalGraphLayoutExample()
        {
            NOrthogonalGraphLayoutExampleSchema = NSchema.Create(typeof(NOrthogonalGraphLayoutExample), NLayoutExampleBase<NOrthogonalGraphLayout>.NLayoutExampleBaseSchema);
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
            
            int[] from = new int[] { 1, 1, 1, 2, 2, 2, 3, 3, 4, 4, 4, 5, 5, 6 };
            int[] to = new int[] { 2, 3, 4, 4, 5, 8, 6, 7, 5, 8, 10, 8, 9, 10 };
            NShape[] shapes = new NShape[10];
            int vertexCount = shapes.Length;
            int edgeCount = from.Length;
            int count = vertexCount + edgeCount;

            for (int i = 0; i < count; i++)
            {
                if (i < vertexCount)
                {
                    int j = vertexCount % 2 == 0 ? i : i + 1;
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
    The orthogonal graph layout produces orthogonal graph drawings of all types of graphs
    (including those with self-loops and duplicate edges). It tries to compact the graph
    drawing area and also to minimize the number of edge crossings and bends.
</p>
<p>
	The most important properties are:
	<ul>
		<li>
			<b>CellSpacing</b> - determines the distance between 2 grid cells. For example if a grid
			cell is calculated to have a size of 100 x 100 and the CellSpacing property is set to
			10, then the cell size will be 120 x 120. Note that the node is always placed in the
			middle of the cell.
		</li>
		<li>
			<b>GridCellSizeMode</b> - this property is an enum with 2 possible values: GridCellSizeMode.
			GridBased and GridCellSizeMode.CellBased. If set to the first the maximal size of a
			node in the graph is determined and all cells are scaled to that size. More area
			efficient is the second value - it causes the dimensions of each column and row
			dimensions to be determined according to the size of the cells they contain.
		</li>
		<li>
			<b>Compact</b> - if set to true, a compaction algorithm will be applied to the embedded
			graph. This will decrease the total area of the drawing with 20 to 50 % (in the average
			case) at the cost of some additional time needed for the calculations.
		</li>
		<li>
			<b>PlugSpacing</b> - determines the spacing between the plugs of a node.
			You can set a fixed amount of spacing or a proportional spacing, which means that the plugs
			are distributed along the whole side of the node.
		</li>
	</ul>
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
        /// Schema associated with NOrthogonalGraphLayoutExample.
        /// </summary>
        public static readonly NSchema NOrthogonalGraphLayoutExampleSchema;

        #endregion
    }
}