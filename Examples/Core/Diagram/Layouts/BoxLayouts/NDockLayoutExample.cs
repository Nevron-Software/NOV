using System;

using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Layout;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NDockLayoutExample : NLayoutExampleBase<NDockLayout>
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NDockLayoutExample()
        {
        }
        /// <summary>
        /// Static constructor.
        /// </summary>
        static NDockLayoutExample()
        {
            NDockLayoutExampleSchema = NSchema.Create(typeof(NDockLayoutExample), NLayoutExampleBase<NDockLayout>.NLayoutExampleBaseSchema);
        }

        #endregion

        #region Overrides - Example

        protected override void InitDiagram()
        {
            base.InitDiagram();

            NPage activePage = m_DrawingDocument.Content.ActivePage;
            NBasicShapeFactory basicShapes = new NBasicShapeFactory();

            int min = 100;
            int max = 200;

            NShape shape;
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                shape = basicShapes.CreateShape(ENBasicShape.Rectangle);

                NColor[] shapeLightColors = new NColor[] {
                                                    new NColor(236, 97, 49),
                                                    new NColor(247, 150, 56),
                                                    new NColor(68, 90, 108),
                                                    new NColor(129, 133, 133),
                                                    new NColor(255, 165, 109)};

                NColor[] shapeDarkColors = new NColor[] {
                                                    new NColor(246, 176, 152),
                                                    new NColor(251, 203, 156),
                                                    new NColor(162, 173, 182),
                                                    new NColor(192, 194, 194),
                                                    new NColor(255, 210, 182)};

                shape.Geometry.Fill = new NStockGradientFill(ENGradientStyle.Horizontal, ENGradientVariant.Variant3, shapeLightColors[i], shapeDarkColors[i]);
                shape.Geometry.Stroke = new NStroke(1, new NColor(68, 90, 108));


                // generate random width and height
                float width = random.Next(min, max);
                float height = random.Next(min, max);

                // instruct the layouts to use fixed, uses specified desired width and desired height
                // shape.LayoutData.UseShapeWidth = false;
                // shape.LayoutData.DesiredWidth = width;

                // shape.LayoutData.UseShapeHeight = false;
                // shape.LayoutData.DesiredHeight = height;

                switch (i)
                {
                    case 0:
                        shape.LayoutData.DockArea = ENDockArea.Top;
                        shape.Text = "Top (" + i.ToString() + ")";
                        break;

                    case 1:
                        shape.LayoutData.DockArea = ENDockArea.Bottom;
                        shape.Text = "Bottom (" + i.ToString() + ")";
                        break;

                    case 2:
                        shape.LayoutData.DockArea = ENDockArea.Left;
                        shape.Text = "Left (" + i.ToString() + ")";
                        break;

                    case 3:
                        shape.LayoutData.DockArea = ENDockArea.Right;
                        shape.Text = "Right (" + i.ToString() + ")";
                        break;

                    case 4:
                        shape.LayoutData.DockArea = ENDockArea.Center;
                        shape.Text = "Center (" + i.ToString() + ")";
                        break;
                }

                //shape.Style.FillStyle = new NColorFillStyle(GetPredefinedColor(i));
                shape.SetBounds(new NRectangle(0, 0, width, height));
                activePage.Items.Add(shape);
            }

            // arrange diagram
            ArrangeDiagram();

            // fit page
            m_DrawingDocument.Content.ActivePage.ZoomMode = ENZoomMode.Fit;
        }
        protected override void CreateItemsControls(NStackPanel stack)
        {
        }
        protected override string GetExampleDescription()
        {
            return @"
<p>     
    The dock layout is a space eating cells layout, which places vertices at per-vertex specified docking areas of the currently available layout area.
</p>
<p>
	The most important properties of this layout are:
	<ul>
		<li>
		    <b>HorizontalContentPlacement and VerticalContentPlacement</b> - determine the default placement
		        of the cell content in regards to the X or the Y dimension of the cell bounds.
		</li>
		<li>
		    <b>HorizontalSpacing and VerticalSpacing</b> - determine the minimal spacing between 2 cells in
		        horizontal and vertical direction respectively.
		</li>
		<li>
			<b>FillMode and FitMode</b> - when the size of the content is smaller than the container size 
			the FillMode property is taken into account. If the content size is greater than the container,
			then the layout takes the value of the FitMode into account. Possible values are:
			<ul>
			    <li>None - the dock layout does not attempt to resolve the available/insufficient area problem</li>
			    <li>Equal - the dock inflates/deflates the size of each object with equal amount of space in order
			        to resolve the available/insufficient area problem</li>
			    <li>CenterFirst - the dock inflates/deflates the size of the center object in the dock, then the size of the
			        pair formed by the previous and the next one and so on until the available/insufficient area
			        problem is resolved</li>
			    <li>SidesFirst - the dock inflates/deflates the size of the pair formed by the first and the last
			        object in the dock, then the size of the pair formed by the next and the previous one and so
			        on until the available/insufficient area problem is resolved</li>
			    <li>ForwardOrder - the bodies are resized in the order they were added</li>
			    <li>ReverseOrder - the bodies are resized in reverse order to the order they were added</li>
			</ul>
			In all cases the minimal and maximal size constraints of each shape are not broken, so it is possible
			the dock cannot resolve the available/insufficient area problem completely.
		</li>
	</ul>
</p>
<p>
	To experiment with the layout just change the properties of the layout in the property grid and click the <b>Layout</b> button.
</p>
            ";
        }

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NDockLayoutExample.
        /// </summary>
        public static readonly NSchema NDockLayoutExampleSchema;

        #endregion
    }
}