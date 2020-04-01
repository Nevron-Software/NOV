using Nevron.Nov.Barcode;
using Nevron.Nov.Dom;
using Nevron.Nov.Text;

namespace Nevron.Nov.Examples.Text
{
	public class NBarcodesInRichTextExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NBarcodesInRichTextExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NBarcodesInRichTextExample()
		{
			NBarcodesInRichTextExampleSchema = NSchema.Create(typeof(NBarcodesInRichTextExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to embed barcodes in rich text documents as widget inlines.
	If you right click the barcode widget you will be able to edit its content and appearance
	through the ""Edit Barcode..."" option.
</p>
";
		}
		protected override void PopulateRichText()
		{
			NDocumentBlock documentBlock = m_RichText.Content;
			documentBlock.Layout = ENTextLayout.Print;

			NSection section = new NSection();
			documentBlock.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Barcode Widget Inlines",
				"Nevron Open Vision lets you easily insert barcodes in text documents as widget inlines.", 1));

			// Create a table
			NTable table = new NTable(2, 2);
			section.Blocks.Add(table);

			// Create a linear barcode
			NLinearBarcode linearBarcode = new NLinearBarcode(ENLinearBarcodeSymbology.EAN13, "0123456789012");
			NWidgetInline widgetInline = new NWidgetInline(linearBarcode);

			// Create a paragraph to host the linear barcode widget inline
			NTableCell cell = table.Rows[0].Cells[0];
			cell.HorizontalAlignment = ENAlign.Center;
			NParagraph paragraph = (NParagraph)cell.Blocks[0];
			paragraph.Inlines.Add(widgetInline);

			// Create a paragraph to the right with some text
			cell = table.Rows[0].Cells[1];
			paragraph = (NParagraph)cell.Blocks[0];
			paragraph.Inlines.Add(new NTextInline("The linear barcode to the left uses the EAN13 symbology."));

			// Create a QR code widget inline
			NMatrixBarcode qrCode = new NMatrixBarcode(ENMatrixBarcodeSymbology.QrCode, "https://www.nevron.com");
			widgetInline = new NWidgetInline(qrCode);

			// Create a paragraph to host the QR code widget inline
			cell = table.Rows[1].Cells[0];
			cell.HorizontalAlignment = ENAlign.Center;
			paragraph = (NParagraph)cell.Blocks[0];
			paragraph.Inlines.Add(widgetInline);

			// Create a paragraph to the right with some text
			cell = table.Rows[1].Cells[1];
			paragraph = (NParagraph)cell.Blocks[0];
			paragraph.Inlines.Add(new NTextInline("The QR code to the left contains a link to "));
			paragraph.Inlines.Add(new NHyperlinkInline("https://www.nevron.com", "https://www.nevron.com"));
			paragraph.Inlines.Add(new NTextInline("."));
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NBarcodesInRichTextExample.
		/// </summary>
		public static readonly NSchema NBarcodesInRichTextExampleSchema;

		#endregion
	}
}