using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NSymmetricalGraphLayoutExample : NLayoutExampleBase<NSymmetricalGraphLayout>
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NSymmetricalGraphLayoutExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NSymmetricalGraphLayoutExample()
        {
            NSymmetricalGraphLayoutExampleSchema = NSchema.Create(typeof(NSymmetricalGraphLayoutExample), NLayoutExampleBase<NSymmetricalGraphLayout>.NLayoutExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides

        protected override void InitDiagram()
        {
            base.InitDiagram();

            // create random diagram
            NGenericTreeTemplate template = new NGenericTreeTemplate();
            template.Balanced = false;
            template.Levels = 6;
            template.BranchNodes = 3;
            template.HorizontalSpacing = 10;
            template.VerticalSpacing = 10;
            template.VerticesShape = ENBasicShape.Circle;
            template.VerticesSize = new NSize(50, 50);
            template.VertexSizeDeviation = 0;
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
    The symmetrical layout represents an implementation of the Fruchertman and Reingold force directed layout (with some modifications).
</p>
<p>
    It uses attractive and repulsive forces, which aim to produce a drawing with uniform distance between each set of connected vertices. Because of that the drawing tends to be symmetrical.
</p>
<p>
	The attractive and repulsive forces are coupled in an instance of the <b>NDesiredDistanceForce</b> class, accessible from the <b>DesiredDistanceForce</b> property. 
</p>
<p>
	To experiment with the layout behavior just change the properties of the layout in the property grid and click the <b>Layout</b> button.
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
        /// Schema associated with NSymmetricalGraphLayoutExample.
        /// </summary>
        public static readonly NSchema NSymmetricalGraphLayoutExampleSchema;

        #endregion
    }
}