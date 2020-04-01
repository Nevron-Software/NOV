using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nevron.Nov.Dom;

namespace Nevron.Nov.Examples.Diagram
{
	public class NFishboneShapesExample : NDiagramExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NFishboneShapesExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NFishboneShapesExample()
		{
			NFishboneShapesExampleSchema = NSchema.Create(typeof(NFishboneShapesExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void InitDiagram()
		{
			base.InitDiagram();
			m_DrawingView.LoadFromResource(NResources.RBIN_NDX_FishboneDiagram_ndx);
		}

		protected override string GetExampleDescription()
		{
			return @"
<p>
    This example demonstrates the fishbone shapes, which are created by the NFishboneShapeFactory.
</p>
";
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NFishboneShapesExample.
		/// </summary>
		public static readonly NSchema NFishboneShapesExampleSchema;

		#endregion
	}
}
