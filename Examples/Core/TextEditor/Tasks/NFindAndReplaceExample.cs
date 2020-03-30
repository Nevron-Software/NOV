using Nevron.Nov.Dom;
using Nevron.Nov.Text;
using Nevron.Nov.UI;
using Nevron.Nov.Graphics;
using Nevron.Nov.Editors;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to programmatically create paragraphs with different inline formatting.
	/// </summary>
	public class NFindAndReplaceExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NFindAndReplaceExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NFindAndReplaceExample()
		{
			NFindAndReplaceExampleSchema = NSchema.Create(typeof(NFindAndReplaceExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides

		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Find and Replace Text", "The example demonstrates how to work find and replace text.", 1));

			for (int i = 0; i < 10; i++)
			{
				section.Blocks.Add(new NParagraph("Now it so happened that on one occasion the princess's golden ball did not fall into the little hand which she was holding up for it, but on to the ground beyond, and rolled straight into the water.  She followed it with her eyes, but it vanished, and the well was deep, so deep that the bottom could not be seen.  At this she began to cry, and cried louder and louder, and could not be comforted.  And as she thus lamented someone said to her, \"What ails you?  You weep so that even a stone would show pity.\""));
			}
		}
		protected override NWidget CreateExampleControls()
		{
			NStackPanel stack = new NStackPanel();

			
			m_FindTextBox = new NTextBox();
			m_FindTextBox.Text = "princess";
			stack.Add(new NPairBox(new NLabel("Find:"), m_FindTextBox, ENPairBoxRelation.Box1AboveBox2));

			m_ReplaceTextBox = new NTextBox();
			m_ReplaceTextBox.Text = "queen";
			stack.Add(new NPairBox(new NLabel("Replace:"), m_ReplaceTextBox, ENPairBoxRelation.Box1AboveBox2));

			NButton findAllButton = new NButton("Find All");
			findAllButton.Click += new Function<NEventArgs>(OnFindAllButtonClick);
			stack.Add(findAllButton);

			NButton replaceAllButton = new NButton("Replace All");
			replaceAllButton.Click += new Function<NEventArgs>(OnReplaceAllButtonClick);
			stack.Add(replaceAllButton);

			NButton clearHighlightButton = new NButton("Clear Highlight");
			clearHighlightButton.Click += new Function<NEventArgs>(OnClearHighlightButtonClick);
			stack.Add(clearHighlightButton);

			return stack;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>This example demonstrates how to find and replace text.</p>
<p>Press the ""Find All"" button to highlight all occurrences of ""Find"".</p>
<p>Press the ""Replace All"" button to replace and highlight all occurrences of ""Find"" with ""Replace""</p>
<p>Press the ""Clear Highlight"" button to clear all highlighting</p>
";
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Called when the user presses the find all button
		/// </summary>
		/// <param name="arg"></param>
		void OnFindAllButtonClick(NEventArgs arg)
		{
			// init find settings
			NFindSettings settings = new NFindSettings();
			settings.FindWhat = m_FindTextBox.Text;
			settings.SearchDirection = ENSearchDirection.Forward;

			// loop through all occurances
			NRangeI textRange = NRangeI.Zero;

			while (m_RichText.EditingRoot.FindNext(settings, ref textRange))
			{
				m_RichText.Selection.SelectRange(textRange);
				m_RichText.Selection.SetHighlightFillToSelectedInlines(new NColorFill(ENNamedColor.Red));
			}

			// move caret to beginning of document
			m_RichText.Selection.MoveCaret(ENCaretMoveDirection.DocumentBegin, false);
		}
		/// <summary>
		/// Called when the user presses the replace all button
		/// </summary>
		/// <param name="arg"></param>
		void OnReplaceAllButtonClick(NEventArgs arg)
		{
			// init find settings
			NFindSettings settings = new NFindSettings();
			settings.FindWhat = m_FindTextBox.Text;
			settings.SearchDirection = ENSearchDirection.Forward;

			// find all occurances 
			NRangeI textRange = NRangeI.Zero;
            NSelection selection = m_RichText.EditingRoot.Selection;

			while (m_RichText.EditingRoot.FindNext(settings, ref textRange))
			{
				// replace dog with cat
                selection.SelectRange(textRange);
                selection.InsertText(m_ReplaceTextBox.Text);

				if (m_ReplaceTextBox.Text.Length > 0)
				{
                    selection.SelectRange(new NRangeI(textRange.Begin, textRange.Begin + m_ReplaceTextBox.Text.Length - 1));
                    selection.SetHighlightFillToSelectedInlines(new NColorFill(ENNamedColor.LimeGreen));
				}
			}

			// move caret to beginning of document
            selection.MoveCaret(ENCaretMoveDirection.DocumentBegin, false);
		}
		/// <summary>
		/// Called when the user presses clear highlight button
		/// </summary>
		/// <param name="arg"></param>
		void OnClearHighlightButtonClick(NEventArgs arg)
		{
			((Nevron.Nov.Text.NBlock)m_RichText.EditingRoot).VisitRanges(delegate(NRangeTextElement range)
			{
				NInline inline = range as NInline;

				if (inline != null)
				{
					inline.ClearLocalValue(NInline.HighlightFillProperty);
				}
			});
		}

		#endregion

		#region Fields

		NTextBox m_FindTextBox;
		NTextBox m_ReplaceTextBox;

		#endregion

		#region Schema

		public static readonly NSchema NFindAndReplaceExampleSchema;

		#endregion
	}
}