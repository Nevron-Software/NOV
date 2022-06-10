﻿using Nevron.Nov.DataStructures;
using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Editors;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NOrthogonalGraphLayoutExample : NExampleBase
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
            NOrthogonalGraphLayoutExampleSchema = NSchema.Create(typeof(NOrthogonalGraphLayoutExample), NExampleBaseSchema);
        }

		#endregion

		#region Example

		protected override NWidget CreateExampleContent()
		{
			// Create a simple drawing
			NDrawingViewWithRibbon drawingViewWithRibbon = new NDrawingViewWithRibbon();
			m_DrawingView = drawingViewWithRibbon.View;

			m_DrawingView.Document.HistoryService.Pause();
			try
			{
				InitDiagram(m_DrawingView.Document);
			}
			finally
			{
				m_DrawingView.Document.HistoryService.Resume();
			}

			return drawingViewWithRibbon;
		}
		protected override NWidget CreateExampleControls()
		{
			m_Layout.Changed += OnLayoutChanged;

			NStackPanel stack = new NStackPanel();

			// property editor
			NEditor editor = NDesigner.GetDesigner(m_Layout).CreateInstanceEditor(m_Layout);
			stack.Add(new NGroupBox("Properties", editor));

			NButton arrangeButton = new NButton("Arrange Diagram");
			arrangeButton.Click += OnArrangeButtonClick;
			stack.Add(arrangeButton);

			// items stack
			NStackPanel itemsStack = new NStackPanel();

			// NOTE: For Graph layouts we provide the user with the ability to generate random graph diagrams so that he/she can test the layouts
			NButton randomGraph1Button = new NButton("Random Graph (10 vertices, 15 edges)");
			randomGraph1Button.Click += OnRandomGraph1ButtonClick;
			itemsStack.Add(randomGraph1Button);

			NButton randomGraph2Button = new NButton("Random Graph (20 vertices, 30 edges)");
			randomGraph2Button.Click += OnRandomGraph2ButtonClick;
			itemsStack.Add(randomGraph2Button);

			stack.Add(new NGroupBox("Items", itemsStack));

			return stack;
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

        private void InitDiagram(NDrawingDocument drawingDocument)
        {
            const double width = 40;
            const double height = 40;
            const double distance = 80;

			// Hide ports
			drawingDocument.Content.ScreenVisibility.ShowPorts = false;

			NBasicShapeFactory basicShapes = new NBasicShapeFactory();
            NPage activePage = drawingDocument.Content.ActivePage;
            
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
            ArrangeDiagram(drawingDocument);

            // fit active page
            drawingDocument.Content.ActivePage.ZoomMode = ENZoomMode.Fit;
        }

		#endregion

		#region Implementation

		/// <summary>
		/// Arranges the shapes in the active page.
		/// </summary>
		/// <param name="drawingDocument"></param>
		private void ArrangeDiagram(NDrawingDocument drawingDocument)
		{
			// get all top-level shapes that reside in the active page
			NPage activePage = drawingDocument.Content.ActivePage;
			NList<NShape> shapes = activePage.GetShapes(false);

			// create a layout context and use it to arrange the shapes using the current layout
			NDrawingLayoutContext layoutContext = new NDrawingLayoutContext(drawingDocument, activePage);
			m_Layout.Arrange(shapes.CastAll<object>(), layoutContext);

			// size the page to the content size
			activePage.SizeToContent();
		}

		#endregion

		#region Event Handlers

		private void OnRandomGraph1ButtonClick(NEventArgs arg)
		{
			NDrawingDocument drawingDocument = m_DrawingView.Document;

			drawingDocument.StartHistoryTransaction("Create Random Graph 1");
			try
			{
				m_DrawingView.ActivePage.Items.Clear();

				// create a test tree
				NRandomGraphTemplate graph = new NRandomGraphTemplate();
				graph.EdgesUserClass = "Connector";
				graph.VertexCount = 10;
				graph.EdgeCount = 15;
				graph.VerticesShape = VertexShape;
				graph.VerticesSize = VertexSize;
				graph.Create(drawingDocument);

				// layout the tree
				ArrangeDiagram(drawingDocument);
			}
			finally
			{
				drawingDocument.CommitHistoryTransaction();
			}
		}
		private void OnRandomGraph2ButtonClick(NEventArgs arg)
		{
			NDrawingDocument drawingDocument = m_DrawingView.Document;

			drawingDocument.StartHistoryTransaction("Create Random Graph 2");
			try
			{
				m_DrawingView.ActivePage.Items.Clear();

				// create a test tree
				NRandomGraphTemplate graph = new NRandomGraphTemplate();
				graph.EdgesUserClass = "Connector";
				graph.VertexCount = 20;
				graph.EdgeCount = 30;
				graph.VerticesShape = VertexShape;
				graph.VerticesSize = VertexSize;
				graph.Create(drawingDocument);

				// layout the tree
				ArrangeDiagram(drawingDocument);
			}
			finally
			{
				drawingDocument.CommitHistoryTransaction();
			}
		}
		private void OnLayoutChanged(NEventArgs arg)
		{
			ArrangeDiagram(m_DrawingView.Document);
		}
		protected virtual void OnArrangeButtonClick(NEventArgs arg)
		{
			ArrangeDiagram(m_DrawingView.Document);
		}

		#endregion

		#region Fields

		private NDrawingView m_DrawingView;
        private NOrthogonalGraphLayout m_Layout = new NOrthogonalGraphLayout();

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NOrthogonalGraphLayoutExample.
        /// </summary>
        public static readonly NSchema NOrthogonalGraphLayoutExampleSchema;

		#endregion

		#region Constants

		private const ENBasicShape VertexShape = ENBasicShape.Rectangle;
		private static readonly NSize VertexSize = new NSize(50, 50);

		#endregion
	}
}