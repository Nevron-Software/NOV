using Nevron.Nov.Chart;
using Nevron.Nov.Chart.Export;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;
using Nevron.Nov.Chart.Formats;
using System;
using System.IO;
using Nevron.Nov.DataStructures;
using Nevron.Nov.Editors;

namespace Nevron.Nov.Examples.Chart
{
	/// <summary>
	/// Serialization Example
	/// </summary>
	public class NSerializationExample : NChartExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public NSerializationExample()
		{
			
		}
		/// <summary>
		/// Static constructor
		/// </summary>
		static NSerializationExample()
		{
			NSerializationExampleSchema = NSchema.Create(typeof(NSerializationExample), NChartExampleBase.NChartExampleBaseSchema);
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
			chartView.Surface.Titles[0].Text = "Serialization";

			// configure chart
			NCartesianChart chart = (NCartesianChart)chartView.Surface.Charts[0];

			chart.SetPredefinedCartesianAxes(ENPredefinedCartesianAxis.XOrdinalYLinear);

			// add interlace stripe
			NLinearScale linearScale = (NLinearScale)chart.Axes[ENCartesianAxis.PrimaryY].Scale;
			NScaleStrip stripStyle = new NScaleStrip(new NColorFill(NColor.Beige), null, true, 0, 0, 1, 1);
			stripStyle.Interlaced = true;
			linearScale.Strips.Add(stripStyle);

			// add the first bar
			m_Bar1 = new NBarSeries();
			m_Bar1.Name = "Bar1";
			m_Bar1.MultiBarMode = ENMultiBarMode.Series;
			m_Bar1.DataLabelStyle = new NDataLabelStyle(false);
			chart.Series.Add(m_Bar1);

			// add the second bar
			m_Bar2 = new NBarSeries();
			m_Bar2.Name = "Bar2";
			m_Bar2.MultiBarMode = ENMultiBarMode.Stacked;
			m_Bar2.DataLabelStyle = new NDataLabelStyle(false);
			chart.Series.Add(m_Bar2);

			// add the third bar
			m_Bar3 = new NBarSeries();
			m_Bar3.Name = "Bar3";
			m_Bar3.MultiBarMode = ENMultiBarMode.Stacked;
			m_Bar3.DataLabelStyle = new NDataLabelStyle(false);
			chart.Series.Add(m_Bar3);

			chartView.Document.StyleSheets.ApplyTheme(new NChartTheme(ENChartPalette.Bright, false));

			FillRandomData();

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
			
			NButton changeDataButton = new NButton("Change Data");
			changeDataButton.Click += OnChangeDataButtonClick;
			stack.Add(changeDataButton);

			NButton saveStateToFileButton = new NButton("Save State To File...");
			saveStateToFileButton.Click += OnSaveStateToFileButtonClick;
			stack.Add(saveStateToFileButton);

			NButton loadStateFromFileButton = new NButton("Load State From File...");
			loadStateFromFileButton.Click += OnLoadStateFromFileButtonClick;
			stack.Add(loadStateFromFileButton);

			NButton saveStateToStreamButton = new NButton("Save State To Stream");
			saveStateToStreamButton.Click += OnSaveStateToStreamButtonClick;
			stack.Add(saveStateToStreamButton);

			NButton loadStateFromStreamButton = new NButton("Load State from Stream");
			loadStateFromStreamButton.Click += OnLoadStateFromStreamButtonClick;
			stack.Add(loadStateFromStreamButton);

			return group;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override string GetExampleDescription()
		{
			return @"<p>This example demonstrates how to save / load the chart state from a file or stream.</p>";
		}

		#endregion

		#region Event Handlers

		void OnLoadStateFromFileButtonClick(NEventArgs arg)
		{
			m_ChartView.LoadFromFile();
		}

		void OnSaveStateToFileButtonClick(NEventArgs arg)
		{
			m_ChartView.SaveToFile();
		}

		void OnLoadStateFromStreamButtonClick(NEventArgs arg)
		{
			if (m_Stream != null)
			{
				m_Stream.Seek(0, SeekOrigin.Begin);
				m_ChartView.LoadFromStream(m_Stream);
			}
		}

		void OnSaveStateToStreamButtonClick(NEventArgs arg)
		{
			m_Stream = new MemoryStream();
			m_ChartView.SaveToStream(m_Stream, new NNevronXmlChartFormat());
		}

		void OnChangeDataButtonClick(NEventArgs arg)
		{
			FillRandomData();
		}

		#endregion

		#region Implementation

		/// <summary>
		/// 
		/// </summary>
		private void FillRandomData()
		{
			NList<NBarSeries> barSeriesList = new NList<NBarSeries>();

			// collect all bar series in the view
			for (int chartIndex = 0; chartIndex < m_ChartView.Surface.Charts.Length; chartIndex++)
			{
				NCartesianChart chart = (NCartesianChart)m_ChartView.Surface.Charts[chartIndex];

				for (int seriesIndex = 0; seriesIndex < chart.Series.Count; seriesIndex++)
				{
					NBarSeries barSeries = chart.Series[seriesIndex] as NBarSeries;

					if (barSeries != null)
					{
						barSeriesList.Add(barSeries);
						barSeries.DataPoints.Clear();
					}
				}
			}

			// fill all bar series with random data
			Random random = new Random();
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < barSeriesList.Count; j++)
				{
					NBarSeries barSeries = barSeriesList[j];
					barSeries.DataPoints.Add(new NBarDataPoint(random.Next(10, 100)));
				}
			}
		}

		#endregion

		#region Fields

		NBarSeries m_Bar1;
		NBarSeries m_Bar2;
		NBarSeries m_Bar3;

		MemoryStream m_Stream;

		#endregion

		#region Static

		public static readonly NSchema NSerializationExampleSchema;

		#endregion
	}
}
