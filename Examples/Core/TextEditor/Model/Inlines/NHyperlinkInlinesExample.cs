using Nevron.Nov.Dom;
using Nevron.Nov.Editors;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to programmatically create inline elements with different formatting
	/// </summary>
	public class NHyperlinkInlinesExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NHyperlinkInlinesExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NHyperlinkInlinesExample()
		{
			NHyperlinkInlinesExampleSchema = NSchema.Create(typeof(NHyperlinkInlinesExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Examples

		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Hyperlink Inlines", "The example shows how to use hyperlinks inlines.", 1));

			// Hyperlink inline with a hyperlink to an URL
			{
				NHyperlinkInline hyperlinkInline = new NHyperlinkInline();
				hyperlinkInline.Hyperlink = new NUrlHyperlink("http://www.nevron.com", ENUrlHyperlinkTarget.SameWindowSameFrame);
				hyperlinkInline.Text = "Jump to www.nevron.com";

				NParagraph paragraph = new NParagraph();
				paragraph.Inlines.Add(hyperlinkInline);
				section.Blocks.Add(paragraph);
			}

			// Image inline with a hyperlink to an URL
			{
				NImageInline imageInline = new NImageInline();
				imageInline.Image = Nov.Diagram.NResources.Image_Other_MyDrawLogo_png;
				imageInline.Hyperlink = new NUrlHyperlink("http://www.mydraw.com", ENUrlHyperlinkTarget.SameWindowSameFrame);

				NParagraph paragraph = new NParagraph();
				paragraph.Inlines.Add(imageInline);
				section.Blocks.Add(paragraph);
			}

			for (int i = 0; i < 10; i++)
			{
				section.Blocks.Add(new NParagraph("Some paragraph"));
			}

			// Bookmark inline
			{
				NParagraph paragraph = new NParagraph();

				NBookmarkInline bookmark = new NBookmarkInline();
				bookmark.Name = "MyBookmark";
				bookmark.Text = "This is a bookmark";
				bookmark.Fill = new NColorFill(NColor.Red);
				paragraph.Inlines.Add(bookmark);

				section.Blocks.Add(paragraph);
			}
		}
		protected override NWidget CreateExampleControls()
		{
			NStackPanel stack = new NStackPanel();

			NButton jumpToBookmarkButton = new NButton("Jump to Bookmark");
			jumpToBookmarkButton.Click += new Function<NEventArgs>(OnJumpToBookmarkButtonClick);

			stack.Add(jumpToBookmarkButton);

			return stack;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>This example demonstrates how to add hyperlinks and bookmarks and how to create image hyperlinks.</p>
<p>Press the ""Jump to Bookmark"" button to position the caret to ""MyBookmark"" bookmark.</p>
";
		}

		#region Event Handlers

		void OnJumpToBookmarkButtonClick(NEventArgs arg)
		{
			m_RichText.Content.Goto(ENTextDocumentPart.Bookmark, "MyBookmark", true);
		}

		#endregion

		#endregion

		#region Schema

		public static readonly NSchema NHyperlinkInlinesExampleSchema;

		#endregion
	}
}