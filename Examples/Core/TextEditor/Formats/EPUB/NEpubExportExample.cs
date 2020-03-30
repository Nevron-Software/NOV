using Nevron.Nov.Dom;
using Nevron.Nov.Text;
using Nevron.Nov.Text.Formats;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
	public class NEpubExportExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NEpubExportExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NEpubExportExample()
		{
			NEpubExportExampleSchema = NSchema.Create(typeof(NEpubExportExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void PopulateRichText()
		{
			base.PopulateRichText();

			NDocumentBlock documentBlock = m_RichText.Content;
			NRichTextStyle heading1Style = documentBlock.Styles.FindStyleByTypeAndId(
				ENRichTextStyleType.Paragraph, "Heading1");

			// Add chapter 1
			NSection section = new NSection();
			documentBlock.Sections.Add(section);

			NParagraph paragraph = new NParagraph("Chapter 1: EPUB Import");
			section.Blocks.Add(paragraph);
			heading1Style.Apply(paragraph);

			paragraph = new NParagraph("NOV Rich Text Editor lets you import Electronic Publications. " +
				"Thus you can use it to read e-books on your PC or MAC.");
			section.Blocks.Add(paragraph);

			paragraph = new NParagraph("You can also use it to import and edit existing Electronic Publications and books.");
			section.Blocks.Add(paragraph);

			// Add chapter 2
			section = new NSection();
			section.BreakType = ENSectionBreakType.NextPage;
			documentBlock.Sections.Add(section);

			paragraph = new NParagraph("Chapter 2: EPUB Export");
			section.Blocks.Add(paragraph);
			heading1Style.Apply(paragraph);

			paragraph = new NParagraph("NOV Rich Text Editor lets you export a rich text document to an Electronic Publication. " +
				"Thus you can use it to create e-books on your PC or MAC.");
			section.Blocks.Add(paragraph);

			paragraph = new NParagraph("You can also use it to import and edit existing Electronic Publications and books.");
			section.Blocks.Add(paragraph);
		}
		protected override NWidget CreateExampleControls()
		{
			NStackPanel stack = (NStackPanel)base.CreateExampleControls();

			NButton exportToEpubButton = new NButton("Export to EPUB...");
			exportToEpubButton.Click += OnExportToEpubButtonClick;
			stack.Add(exportToEpubButton);

			return stack;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to import Electronic Publications (EPUB files) in Nevron Rich Text Editor.
</p>
";
		}

		#endregion

		#region Event Handlers

		private void OnExportToEpubButtonClick(NEventArgs arg)
		{
			NEpubTextFormat epubFormat = new NEpubTextFormat();

			// Create and show a save file dialog
			NSaveFileDialog saveDialog = new NSaveFileDialog();
			saveDialog.Title = "Export to EPUB";
			saveDialog.DefaultFileName = "MyBook.epub";
			saveDialog.FileTypes = new NFileDialogFileType[] {
				new NFileDialogFileType(epubFormat)
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
		/// Schema associated with NEpubExportExample.
		/// </summary>
		public static readonly NSchema NEpubExportExampleSchema;

		#endregion
	}
}