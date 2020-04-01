using System;

using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Layout;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.UI
{
	public class NPrintDialogExample : NExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NPrintDialogExample()
		{
		}
		/// <summary>
		/// Static constructor.
		/// </summary>
		static NPrintDialogExample()
		{
			NPrintDialogExampleSchema = NSchema.Create(typeof(NPrintDialogExample), NExampleBase.NExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override NWidget CreateExampleContent()
		{
			NButton printButton = new NButton("Print...");
			printButton.HorizontalPlacement = ENHorizontalPlacement.Left;
			printButton.VerticalPlacement = ENVerticalPlacement.Top;
			printButton.Click += new Function<NEventArgs>(OnPrintButtonClick);

			return printButton;
		}
		protected override NWidget CreateExampleControls()
		{
			return null;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to create and use the print dialog provided by NOV.
</p>";
		}

		#endregion

		#region Event Handlers

		private void OnPrintButtonClick(NEventArgs args)
		{
			NPrintDocument printDocument = new NPrintDocument();
			printDocument.DocumentName = "Test Document 1";
			printDocument.BeginPrint += new Function<NPrintDocument, NBeginPrintEventArgs>(OnBeginPrint);
			printDocument.PrintPage += new Function<NPrintDocument, NPrintPageEventArgs>(OnPrintPage);
			printDocument.EndPrint += new Function<NPrintDocument, NEndPrintEventArgs>(OnEndPrint);

			NPrintDialog pd = new NPrintDialog();
			pd.EnableCustomPageRange = true;
			pd.EnableCurrentPage = true;
			pd.PrintRangeMode = ENPrintRangeMode.AllPages;
			pd.CustomPageRange = new NRangeI(1, 100);
			pd.NumberOfCopies = 2;
			pd.Collate = true;
			pd.PrintDocument = printDocument;
			pd.Closed += new Function<NPrintDialogResult>(OnPrintDialogClosed);

			pd.RequestShow();
		}
		private void OnBeginPrint(NPrintDocument sender, NBeginPrintEventArgs e)
		{
		}
		private void OnEndPrint(NPrintDocument sender, NEndPrintEventArgs e)
		{
		}
		private void OnPrintPage(NPrintDocument sender, NPrintPageEventArgs e)
		{
			NSize pageSizeDIP = new NSize(this.Width, this.Height);

			try
			{
				NMargins pageMargins = NMargins.Zero;

                NRegion clip = NRegion.FromRectangle(new NRectangle(0, 0, e.PrintableArea.Width, e.PrintableArea.Height));
                NMatrix transform = new NMatrix(e.PrintableArea.X, e.PrintableArea.Y);

				NPaintVisitor visitor = new NPaintVisitor(e.Graphics, 300, transform, clip);
				
				// forward traverse the display tree
				this.OwnerWindow.VisitDisplaySubtree(visitor);
				
				e.HasMorePages = false;
			}
			catch (Exception x)
			{
				NMessageBox.Show(x.Message, "Exception", ENMessageBoxButtons.OK, ENMessageBoxIcon.Error);
			}
	
		}
		private void OnPrintDialogClosed(NPrintDialogResult result)
		{
			if (result.Result == ENCommonDialogResult.Error)
			{
				NMessageBox.Show("Error Message: " + result.ErrorException.Message, "Print Dialog Error", ENMessageBoxButtons.OK, ENMessageBoxIcon.Error);
			}
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NPrintDialogExample.
		/// </summary>
		public static readonly NSchema NPrintDialogExampleSchema;

		#endregion
	}
}