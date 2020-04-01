using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;
using System;
using Nevron.Nov.Editors;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to programmatically create inline elements with different formatting
	/// </summary>
	public class NFieldInlinesExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NFieldInlinesExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NFieldInlinesExample()
		{
			NFieldInlinesExampleSchema = NSchema.Create(typeof(NFieldInlinesExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides

		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Field Inlines", "The example shows how to use field inlines.", 1));

			section.Blocks.Add(GetNoteBlock("Not all field values are always available. Please check the documentation for more information.", 1));

			// add numeric fields
			section.Blocks.Add(GetTitleParagraph("Numeric Fields", 2));

			ENNumericFieldName[] numericFields = NEnum.GetValues<ENNumericFieldName>();
			string[] numericFieldNames = NEnum.GetNames<ENNumericFieldName>();

			for (int i = 0; i < numericFieldNames.Length; i++)
			{
				NParagraph paragraph = new NParagraph();

				paragraph.Inlines.Add(new NTextInline(numericFieldNames[i] + " ["));

				NFieldInline fieldInline = new NFieldInline();
				fieldInline.Value = new NNumericFieldValue(numericFields[i]);
				fieldInline.Text = "Not Updated";
				paragraph.Inlines.Add(fieldInline);

				paragraph.Inlines.Add(new NTextInline("]"));

				section.Blocks.Add(paragraph);
			}

			// add date time fields
			section.Blocks.Add(GetTitleParagraph("Date/Time Fields", 2));

			ENDateTimeFieldName[] dateTimeFields = NEnum.GetValues<ENDateTimeFieldName>();
			string[] dateTimecFieldNames = NEnum.GetNames<ENDateTimeFieldName>();

			for (int i = 0; i < dateTimecFieldNames.Length; i++)
			{
				NParagraph paragraph = new NParagraph();

				paragraph.Inlines.Add(new NTextInline(dateTimecFieldNames[i] + " ["));

				NFieldInline fieldInline = new NFieldInline();
				fieldInline.Value = new NDateTimeFieldValue(dateTimeFields[i]);
				fieldInline.Text = "Not Updated";
				paragraph.Inlines.Add(fieldInline);

				paragraph.Inlines.Add(new NTextInline("]"));

				section.Blocks.Add(paragraph);
			}

			// add string fields
			section.Blocks.Add(GetTitleParagraph("String Fields", 2));

			ENStringFieldName[] stringFields = NEnum.GetValues<ENStringFieldName>();
			string[] stringcFieldNames = NEnum.GetNames<ENStringFieldName>();

			for (int i = 0; i < stringcFieldNames.Length; i++)
			{
				NParagraph paragraph = new NParagraph();

				paragraph.Inlines.Add(new NTextInline(stringcFieldNames[i] + " ["));

				NFieldInline fieldInline = new NFieldInline();
				fieldInline.Value = new NStringFieldValue(stringFields[i]);
				fieldInline.Text = "Not Updated";
				paragraph.Inlines.Add(fieldInline);

				paragraph.Inlines.Add(new NTextInline("]"));

				section.Blocks.Add(paragraph);
			}
		}
		protected override NWidget CreateExampleControls()
		{
			NStackPanel stack = new NStackPanel();
			
			NButton updateAllFieldsButton = new NButton("Update All Fields");
			updateAllFieldsButton.Click += new Function<NEventArgs>(OnUpdateAllFieldsButtonClick);

			stack.Add(updateAllFieldsButton);

			return stack;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>This example demonstrates how to add different field inlines as well as how to use a range visitor delegate to update all fields in a block tree.</p>
<p>Press the ""Update All Fields"" button to update all fields.</p>
";
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// 
		/// </summary>
		/// <param name="arg"></param>
		void OnUpdateAllFieldsButtonClick(NEventArgs arg)
		{
			m_RichText.Content.VisitRanges(delegate(NRangeTextElement range)
			{
				NFieldInline field = range as NFieldInline;

				if (field != null)
				{
					field.Update();
				}
			});
		}

		#endregion

		#region Schema

		public static readonly NSchema NFieldInlinesExampleSchema;

		#endregion
	}
}
