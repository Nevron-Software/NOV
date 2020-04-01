using Nevron.Nov.Dom;

namespace Nevron.Nov.Examples.Diagram
{
    public class NGedcomImportExample : NDiagramExampleBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NGedcomImportExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NGedcomImportExample()
        {
            NGedcomImportExampleSchema = NSchema.Create(typeof(NGedcomImportExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides

        protected override void InitDiagram()
        {
            // Import a Visio diagram
            m_DrawingView.LoadFromResource(NResources.RSTR_LincolnFamily_ged);

            // Hide ports
            m_DrawingView.Drawing.ScreenVisibility.ShowPorts = false;
        }
        protected override string GetExampleDescription()
        {
            return @"
<p>Demonstrates how to import a family tree in GEDCOM format (""*.ged"") to Nevron Diagram.
This example imports the family tree of the US president Abraham Lincoln.</p>";
        }

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NGedcomImportExample.
        /// </summary>
        public static readonly NSchema NGedcomImportExampleSchema;

        #endregion
    }
}