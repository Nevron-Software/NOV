using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Layout;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NStackLayoutExample : NLayoutExampleBase<NStackLayout>
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NStackLayoutExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NStackLayoutExample()
        {
            NStackLayoutExampleSchema = NSchema.Create(typeof(NStackLayoutExample), NLayoutExampleBase<NStackLayout>.NLayoutExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides - Example

        protected override void InitDiagram()
        {
            base.InitDiagram();

            // create some shapes
            NPage activePage = m_DrawingDocument.Content.ActivePage;
            NBasicShapeFactory basicShapes = new NBasicShapeFactory();

            for (int i = 0; i < 20; i++)
            {
                NShape shape = basicShapes.CreateShape(ENBasicShape.Rectangle);
                activePage.Items.Add(shape);
            }

            // arrange diagram
            ArrangeDiagram();

            // fit page
            m_DrawingDocument.Content.ActivePage.ZoomMode = ENZoomMode.Fit;
        }

        protected override string GetExampleDescription()
        {
            return @"
<p>
    The stack layout is a directed constrained cells layout, which stacks the cells in horizontal or vertical order.
    Depending on the layout direction the layout is constrained by either width or height.
</p>
<p>
	The most important properties of this layout are:
	<ul>
		<li>
			<b>Direction</b> - determines the direction in which the layout arranges adjacent cells.
		</li>
		<li>
		    <b>HorizontalContentPlacement and VerticalContentPlacement</b> - determine the default placement
		        of the cell content in regards to the X or the Y dimension of the cell bounds.
		</li>
		<li>
		    <b>HorizontalSpacing and VerticalSpacing</b> - determine the minimal spacing between 2 cells in
		        horizontal and vertical direction respectively.
		</li>
		<li>
			<b>FillMode</b> - when the size of the content is smaller than the container size 
			the FillMode property is taken into account. Possible values are:
			<ul>
			    <li>None - no filling is performed</li>
			    <li>Equal - all shapes are equally inflated</li>
			    <li>Proportional - shapes are inflated proportionally to their size</li>
			    <li>First - shapes are inflated in forward order until content fills the area</li>
			    <li>Last - shapes are inflated in reverse order until content fills the area</li>
			</ul>
			In all cases the maximal size constraints of each shape are not broken.
		</li>
		<li>
			<b>FitMode</b> - when the size of the content is larger than the container size 
			the FitMode property is taken into account. Possible values are:
			<ul>
			    <li>None - no fitting is performed</li>
			    <li>Equal - all shapes are equally deflated</li>
			    <li>Proportional - shapes are deflated proportionally to their size</li>
			    <li>First - shapes are deflated in forward order until content fits the area</li>
			    <li>Last - shapes are deflated in reverse order until content fits the area</li>
			</ul>
			In all cases the minimal size constraints of each shape are not broken.
		</li>
	</ul>
</p>
<p>
	To experiment with their behavior just change the properties of the layout in the property
	grid and click the button <b>Layout</b> button. 
</p>		
<p>	
	Change the drawing width and height to see how the layout behaves with a different layout area.
</p>
            ";
        }

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NStackLayoutExample.
        /// </summary>
        public static readonly NSchema NStackLayoutExampleSchema;

        #endregion
    }
}