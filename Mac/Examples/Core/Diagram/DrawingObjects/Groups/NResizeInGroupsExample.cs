using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;

namespace Nevron.Nov.Examples.Diagram
{
    /// <summary>
    /// 
    /// </summary>
    public class NResizeInGroupsExample : NDiagramExampleBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NResizeInGroupsExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NResizeInGroupsExample()
        {
            NResizeInGroupsExampleSchema = NSchema.Create(typeof(NResizeInGroupsExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides - Example

        protected override string GetExampleDescription()
        {
            return @"
<p>
    Demonstrates how 2D and 1D shapes can be resized when they are placed in groups.
</p>
<p>
    When a 2D shape whose ResizeInGroup property is set to ScaleAndReposition is resized inside a group, 
    NOV Diagram will express the shape size as a fraction of the group Width and Height, 
    and bind the shape pin point to a relative position inside group coordinate system.
    When the group is resized, the 2D shape will scale and reposition with the group.
</p>
<p>
    When a 2D shape whose ResizeInGroup property is set to RepositionOnly is resized inside a group, 
    NOV Diagram will set a constant size to the shape and bind the shape pin point 
    to a relative position inside group coordinate system.
    When the group is resized, the 2D shape will only reposition itself.
</p>
<p>
    When you move the Begin or End points of a 1D shape inside a group, 
    NOV Diagram will bind the respective end-point to a relative position inside group coordinate system.
    When the group is resized, the 1D shape will get stretched.
</p>
";
        }
        protected override void InitDiagram()
        {
            base.InitDiagram();

            // create all shapes
            NBasicShapeFactory basicShapes = new NBasicShapeFactory();
            NConnectorShapeFactory connectorShapes = new NConnectorShapeFactory();

            // create the group
            NGroup group = new NGroup();

            // make some background for the group
            NDrawRectangle drawRect = new NDrawRectangle(0, 0, 1, 1);
            drawRect.Relative = true;
            group.Geometry.Add(drawRect);
            group.Geometry.Fill = new NColorFill(NColor.LightCoral);
            group.SetBounds(new Nov.Graphics.NRectangle(50, 50, 230, 330));

            // create a rectangle that is scaled and repositioned
            NShape rect1 = basicShapes.CreateShape(ENBasicShape.Rectangle);
            rect1.Text = "Scale and Reposition";
            group.Shapes.Add(rect1);
            rect1.ResizeInGroup = ENResizeInGroup.ScaleAndReposition;
            rect1.SetBounds(new Nov.Graphics.NRectangle(10, 10, 100, 100));

            // create a rectangle that is only repositioned
            NShape rect2 = basicShapes.CreateShape(ENBasicShape.Rectangle);
            rect2.Text = "Reposition Only";
            group.Shapes.Add(rect2);
            rect2.ResizeInGroup = ENResizeInGroup.RepositionOnly;
            rect2.SetBounds(new Nov.Graphics.NRectangle(120, 120, 100, 100));

            // create a 1D shape
			NShape arrow = connectorShapes.CreateShape(ENConnectorShape.Single45DegreesArrow);
            arrow.Text = "1D Shape";
            group.Shapes.Add(arrow);
            arrow.SetBeginPoint(new NPoint(10, 250));
            arrow.SetEndPoint(new NPoint(220, 290));

            // add the group
            m_DrawingDocument.Content.ActivePage.Items.Add(group);
        }

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NResizeInGroupsExample.
        /// </summary>
        public static readonly NSchema NResizeInGroupsExampleSchema;

        #endregion
    }
}