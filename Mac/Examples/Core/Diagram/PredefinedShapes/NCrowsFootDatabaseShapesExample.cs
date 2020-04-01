
using Nevron.Nov.Dom;

namespace Nevron.Nov.Examples.Diagram
{
	public class NCrowsFootDatabaseShapesExample : NDiagramExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NCrowsFootDatabaseShapesExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NCrowsFootDatabaseShapesExample()
		{
			NCrowsFootDatabaseShapesExampleSchema = NSchema.Create(typeof(NCrowsFootDatabaseShapesExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
		}

		#endregion

		#region Overrides

		protected override void InitDiagram()
		{
			base.InitDiagram();
			//m_DrawingView.LoadFromResource(NResources.RBIN_NDX_PersonAdventureWorks_ndx);
		}

		protected override string GetExampleDescription()
		{
			return @"
<p>
    This example demonstrates the Crows Foot Database shapes, which are created by the NDatabaseShapesFactory.
</p>
";
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NCrowsFootDatabaseShapesExample.
		/// </summary>
		public static readonly NSchema NCrowsFootDatabaseShapesExampleSchema;

		#endregion
	}
}
