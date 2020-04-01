using Nevron.Nov.Chart;
using Nevron.Nov.Chart.Export;
using Nevron.Nov.Dom;
using Nevron.Nov.Editors;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;
using System;
using System.IO;

namespace Nevron.Nov.Examples.Chart
{
	/// <summary>
	/// Image Export Example
	/// </summary>
	public class NImageExportExample : NChartExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public NImageExportExample()
		{
			
		}
		/// <summary>
		/// Static constructor
		/// </summary>
		static NImageExportExample()
		{
			NImageExportExampleSchema = NSchema.Create(typeof(NImageExportExample), NChartExampleBase.NChartExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override NWidget CreateExampleContent()
		{
			NChartView chartView = CreateCartesianChartView();

			// configure title
			chartView.Surface.Titles[0].Text = "Image Export Example";

			// configure chart
			NCartesianChart chart = (NCartesianChart)chartView.Surface.Charts[0];

			chart.SetPredefinedCartesianAxes(ENPredefinedCartesianAxis.XYLinear);

			// setup X axis
			NLinearScale xScale = (NLinearScale)chart.Axes[ENCartesianAxis.PrimaryX].Scale;

			xScale.MajorGridLines.Visible = true;

			// setup Y axis
			NLinearScale yScale = (NLinearScale)chart.Axes[ENCartesianAxis.PrimaryY].Scale;
			yScale.MajorGridLines.Visible = true;
			
			// add interlaced stripe
			NScaleStrip strip = new NScaleStrip(new NColorFill(NColor.Beige), null, true, 0, 0, 1, 1);
			strip.Interlaced = true;
			yScale.Strips.Add(strip);

			// setup shape series
			NRangeSeries range = new NRangeSeries();
			chart.Series.Add(range);

			range.DataLabelStyle = new NDataLabelStyle(false);
			range.UseXValues = true;
			range.Fill = new NColorFill(NColor.DarkOrange);
			range.Stroke = new NStroke(NColor.DarkRed);

			// fill data
			double[] intervals = new double[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 15, 30, 60 };
			double[] values = new double[] { 4180, 13687, 18618, 19634, 17981, 7190, 16369, 3212, 4122, 9200, 6461, 3435 };

			int count = Math.Min(intervals.Length, values.Length);
			double x = 0;

			for (int i = 0; i < count; i++)
			{
				double interval = intervals[i];
				double value = values[i];

				double x1 = x;
				double y1 = 0;

				x += interval;
				double x2 = x;
				double y2 = value / interval;

				range.DataPoints.Add(new NRangeDataPoint(x1, y1, x2, y2));
			}			

			chartView.Document.StyleSheets.ApplyTheme(new NChartTheme(ENChartPalette.Bright, false));

			return chartView;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override NWidget CreateExampleControls()
		{
			NStackPanel stack = new NStackPanel();
			NUniSizeBoxGroup group = new NUniSizeBoxGroup(stack);

			NButton copyImageToClipboardButton = new NButton("Copy Image to Clipboard");
			copyImageToClipboardButton.Click += OnCopyImageToClipboardButtonClick;
			stack.Add(copyImageToClipboardButton);

			NButton saveAsImageFileButton = new NButton("Save as Image File...");
			saveAsImageFileButton.Click +=OnSaveAsImageFileButtonClick;
			stack.Add(saveAsImageFileButton);

			return group;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override string GetExampleDescription()
		{
			return @"<p>This example demonstrates how to export images to the clipboard or file.</p>";
		}

		#endregion

		#region Event Handlers

		void OnSaveAsImageFileButtonClick(NEventArgs arg)
		{
			NChartImageExporter imageExporter = new NChartImageExporter(m_ChartView.Document);
			imageExporter.SaveAsImage(new NSize(400, 300), 96);

			// Note: You can also use SaveToFile like:
			// imageExporter.SaveToFile("c:\\SomeImage.png", NImageFormat.Png, new NSize(400, 300), 96);

			// or SaveToStream:
			// MemoryStream memoryStream = new MemoryStream();
			// imageExporter.SaveToStream(memoryStream, NImageFormat.Png, new NSize(400, 300), 96);
			// byte[] imageBytes = memoryStream.ToArray();
		}

		void OnCopyImageToClipboardButtonClick(NEventArgs arg)
		{
			NChartImageExporter imageExporter = new NChartImageExporter(m_ChartView.Document);
			imageExporter.CopyToClipboard(new NSize(500, 400), 96);
		}

		#endregion

		#region Implementation

		#endregion

		#region Fields


		#endregion

		#region Static

		public static readonly NSchema NImageExportExampleSchema;

		#endregion
	}
}
