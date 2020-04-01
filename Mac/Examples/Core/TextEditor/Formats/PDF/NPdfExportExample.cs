using Nevron.Nov.Dom;
using Nevron.Nov.Text.Formats;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
	public class NPdfExportExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NPdfExportExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NPdfExportExample()
		{
			NPdfExportExampleSchema = NSchema.Create(typeof(NPdfExportExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void PopulateRichText()
		{
			// Load a document from resource
			m_RichText.LoadFromResource(NResources.RBIN_DOCX_ComplexDocument_docx);
		}
		protected override NWidget CreateExampleControls()
		{
			NStackPanel stack = (NStackPanel)base.CreateExampleControls();

			NButton exportToPdfButton = new NButton("Export to PDF...");
			exportToPdfButton.Click += OnExportToPdfButtonClick;
			stack.Add(exportToPdfButton);

			return stack;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to export a Nevron Rich Text document to PDF.
</p>
";
		}

		#endregion

		#region Event Handlers

		private void OnExportToPdfButtonClick(NEventArgs arg)
		{
			NPdfTextFormat pdfFormat = new NPdfTextFormat();

			// Create and show a save file dialog
			NSaveFileDialog saveDialog = new NSaveFileDialog();
			saveDialog.Title = "Export to PDF";
			saveDialog.DefaultFileName = "Document1.pdf";
			saveDialog.FileTypes = new NFileDialogFileType[] {
				new NFileDialogFileType(pdfFormat)
			};

			saveDialog.Closed += OnSaveDialogClosed;
			saveDialog.RequestShow();
		}
		private void OnSaveDialogClosed(NSaveFileDialogResult arg)
		{
			if (arg.Result == ENCommonDialogResult.OK)
			{
				m_RichText.SaveToFile(arg.File);
			}
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NPdfExportExample.
		/// </summary>
		public static readonly NSchema NPdfExportExampleSchema;

		#endregion
	}
}