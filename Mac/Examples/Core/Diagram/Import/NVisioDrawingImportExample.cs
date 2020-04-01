using Nevron.Nov.Dom;

namespace Nevron.Nov.Examples.Diagram
{
    public class NVisioDrawingImportExample : NDiagramExampleBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NVisioDrawingImportExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NVisioDrawingImportExample()
        {
            NVisioDrawingImportExampleSchema = NSchema.Create(typeof(NVisioDrawingImportExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides

        protected override void InitDiagram()
        {
            // Import a Visio diagram
            m_DrawingView.LoadFromResource(NResources.RBIN_VSDX_CorporateDiagramShapes_vsdx);

            // Hide ports
            m_DrawingView.Drawing.ScreenVisibility.ShowPorts = false;
        }
        protected override string GetExampleDescription()
        {
            return @"<p>Demonstrates how to import a Visio drawing (in VSDX format) to Nevron Diagram.</p>";
        }

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NVisioDrawingImportExample.
        /// </summary>
        public static readonly NSchema NVisioDrawingImportExampleSchema;

        #endregion
    }
}