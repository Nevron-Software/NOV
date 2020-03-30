using System;
using Nevron.Nov.Barcode;
using Nevron.Nov.Diagram;
using Nevron.Nov.Dom;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
	public class NBarcodesInDiagramExample : NDiagramExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NBarcodesInDiagramExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NBarcodesInDiagramExample()
		{
			NBarcodesInDiagramExampleSchema = NSchema.Create(typeof(NBarcodesInDiagramExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
		}

		#endregion

		protected override void InitDiagram()
		{
			NPage activePage = m_DrawingDocument.Content.ActivePage;

			// Create a barcode widget
			NMatrixBarcode barcode = new NMatrixBarcode(ENMatrixBarcodeSymbology.QrCode, "https://www.nevron.com");			

			// Create a shape and place the barcode widget in it
			NShape shape = new NShape();
			shape.SetBounds(100, 100, 100, 100);
			shape.Widget = barcode;
			activePage.Items.Add(shape);
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
    Demonstrates how to create and host barcodes in diagram shapes.
</p>
";
		}

		#region Schema

		/// <summary>
		/// Schema associated with NBarcodesInDiagramExample.
		/// </summary>
		public static readonly NSchema NBarcodesInDiagramExampleSchema;

		#endregion
	}
}