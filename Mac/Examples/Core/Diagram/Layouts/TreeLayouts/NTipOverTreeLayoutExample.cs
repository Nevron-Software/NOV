using System;

using Nevron.Nov.DataStructures;
using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NTipOverTreeLayoutExample : NLayoutExampleBase<NTipOverTreeLayout>
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NTipOverTreeLayoutExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NTipOverTreeLayoutExample()
        {
            NTipOverTreeLayoutExampleSchema = NSchema.Create(typeof(NTipOverTreeLayoutExample), NLayoutExampleBase<NTipOverTreeLayout>.NLayoutExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides

        protected override void InitDiagram()
        {
            base.InitDiagram();

            NPage activePage = m_DrawingDocument.Content.ActivePage;

            // we will be using basic shapes with default size of 120, 60
            NBasicShapeFactory basicShapesFactory = new NBasicShapeFactory();
            basicShapesFactory.DefaultSize = new NSize(120, 60);

            // create the president
            NShape president = basicShapesFactory.CreateShape(ENBasicShape.Rectangle);
            president.Text = "President";
            activePage.Items.Add(president);

            // create the VPs. 
            // NOTE: The child nodes of the VPs are layed out in cols
            NShape vpMarketing = basicShapesFactory.CreateShape(ENBasicShape.Rectangle);
            vpMarketing.Text = "VP Marketing";
            vpMarketing.Geometry.Stroke = new NStroke(1, new NColor(68, 90, 108));
            activePage.Items.Add(vpMarketing);

            NShape vpSales = basicShapesFactory.CreateShape(ENBasicShape.Rectangle);
            vpSales.Text = "VP Sales";
            vpSales.Geometry.Stroke = new NStroke(1, new NColor(68, 90, 108));
            activePage.Items.Add(vpSales);

            NShape vpProduction = basicShapesFactory.CreateShape(ENBasicShape.Rectangle);
            vpProduction.Text = "VP Production";
            vpProduction.Geometry.Stroke = new NStroke(1, new NColor(68, 90, 108));
            activePage.Items.Add(vpProduction);

            // connect president with VP
            NRoutableConnector connector = new NRoutableConnector();
            activePage.Items.Add(connector);
            connector.GlueBeginToShape(president);
            connector.GlueEndToShape(vpMarketing);

            connector = new NRoutableConnector();
            activePage.Items.Add(connector);
            connector.GlueBeginToShape(president);
            connector.GlueEndToShape(vpSales);

            connector = new NRoutableConnector();
            activePage.Items.Add(connector);
            connector.GlueBeginToShape(president);
            connector.GlueEndToShape(vpProduction);

            // crete the marketing managers
            NShape marketingManager1 = basicShapesFactory.CreateShape(ENBasicShape.Rectangle);
            marketingManager1.Text = "Manager1";
            activePage.Items.Add(marketingManager1);

            NShape marketingManager2 = basicShapesFactory.CreateShape(ENBasicShape.Rectangle);
            marketingManager2.Text = "Manager2";
            activePage.Items.Add(marketingManager2);

            // connect the marketing manager with the marketing VP
            connector = new NRoutableConnector();
            activePage.Items.Add(connector);
            connector.GlueBeginToShape(vpMarketing);
            connector.GlueEndToShape(marketingManager1);

            connector = new NRoutableConnector();
            activePage.Items.Add(connector);
            connector.GlueBeginToShape(vpMarketing);
            connector.GlueEndToShape(marketingManager2);

            // crete the sales managers
            NShape salesManager1 = basicShapesFactory.CreateShape(ENBasicShape.Rectangle);
            salesManager1.Text = "Manager1";
            activePage.Items.Add(salesManager1);

            NShape salesManager2 = basicShapesFactory.CreateShape(ENBasicShape.Rectangle);
            salesManager2.Text = "Manager2";
            activePage.Items.Add(salesManager2);

            // connect the sales manager with the sales VP
            connector = new NRoutableConnector();
            activePage.Items.Add(connector);
            connector.GlueBeginToShape(vpSales);
            connector.GlueEndToShape(salesManager1);

            connector = new NRoutableConnector();
            activePage.Items.Add(connector);
            connector.GlueBeginToShape(vpSales);
            connector.GlueEndToShape(salesManager2);

            // crete the production managers
            NShape productionManager1 = basicShapesFactory.CreateShape(ENBasicShape.Rectangle);
            productionManager1.Text = "Manager1";
            activePage.Items.Add(productionManager1);

            NShape productionManager2 = basicShapesFactory.CreateShape(ENBasicShape.Rectangle);
            productionManager2.Text = "Manager2";
            activePage.Items.Add(productionManager2);

            // connect the production manager with the production VP
            connector = new NRoutableConnector();
            activePage.Items.Add(connector);
            connector.GlueBeginToShape(vpProduction);
            connector.GlueEndToShape(productionManager1);

            connector = new NRoutableConnector();
            activePage.Items.Add(connector);
            connector.GlueBeginToShape(vpProduction);
            connector.GlueEndToShape(productionManager2);

            // arrange diagram
            ArrangeDiagram();

            // fit active page
            m_DrawingDocument.Content.ActivePage.ZoomMode = ENZoomMode.Fit;
        }
        protected override string GetExampleDescription()
        {
            return @"
<p>
    The tip-over tree layout is a tree layout, which uses shapes provided data to determine
    whether the vertex children should be placed in a row, column or 2 columns. It produces
    orthogonal tree drawings. The layout satisfies to the following aesthetic criteria:
    <ul>
        <li><b>No edge crossings</b> - edges do not cross each other.</li>
        <li><b>No vertex-vertex overlaps</b> - vertices do not overlap each other.</li>
        <li><b>No vertex-edge overlaps</b> - vertices do not overlap edges.</li>
        <li><b>Compactness</b> - you can optimize the compactness of the drawing in both
            the breadth and depth dimensions of the tree by setting the <b>Compact</b>
            property to true.</li>
    </ul>
</p>
    You can change the way the children and the leafs are placed using the corresponding
    properties. You can also set the children placement for the children of each vertex
    individually using the <b>TipOverChildrenPlacement</b> property in the LayoutData
    collection of the shape.
<p>
    This type of layout is typically used in Org Charts, but can also be used in all cases
    where classical tree layouts (e.g. layouts with uniform parent placement) do not produce
    width/height ratio compact results.
</p>
<p>
    In this example the nodes whose children are arranged in cols are highlighted with orange.
</p>
<p>
	To experiment with the layout just change its properties from the property grid and click the <b>Layout</b> button. 
    To see the layout in action on a different trees, just click the <b>Random Tree</b> button. 
</p>
";
        }
        protected override void OnRandomTree1ButtonClick(NEventArgs arg)
        {
            m_DrawingDocument.Content.ActivePage.Items.Clear();

            // create a random tree
			CreateTreeDiagram(6, 3);

            // layout the tree
            ArrangeDiagram();
        }
        protected override void OnRandomTree2ButtonClick(NEventArgs arg)
        {
            m_DrawingDocument.Content.ActivePage.Items.Clear();

            // create a random tree
			CreateTreeDiagram(8, 2);

            // layout the tree
            ArrangeDiagram();
        }

        #endregion

		#region Implementation

		private void CreateTreeDiagram(int levels, int branchNodes)
		{
			// create a random tree
			NGenericTreeTemplate tree = new NGenericTreeTemplate();
			tree.EdgesUserClass = "Connector";
			tree.Balanced = false;
			tree.Levels = levels;
			tree.BranchNodes = branchNodes;
			tree.HorizontalSpacing = 10;
			tree.VerticalSpacing = 10;
			tree.VerticesShape = GetDefaultShapeType();
			tree.VerticesSize = GetDefaultShapeSize();
			tree.VertexSizeDeviation = 1;
			tree.Create(m_DrawingDocument);

			// randomly set the children placement of ten shapes to col
			NList<NShape> shapes = m_DrawingDocument.Content.ActivePage.GetShapes(false, NDiagramFilters.ShapeType2D);
			Random random = new Random();
			for (int i = 0; i < shapes.Count / 2; i++)
			{
				int index = random.Next(shapes.Count);
				NShape shape = shapes[index];
				shape.Geometry.Fill = new NColorFill(NColor.Orange);
				shape.LayoutData.TipOverChildrenPlacement = ENTipOverChildrenPlacement.ColRight;
			}
		}

		#endregion

		#region Schema

		/// <summary>
        /// Schema associated with NTipOverTreeLayoutExample.
        /// </summary>
        public static readonly NSchema NTipOverTreeLayoutExampleSchema;

        #endregion
    }
}