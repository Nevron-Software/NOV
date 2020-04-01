using Nevron.Nov.Chart;
using Nevron.Nov.Dom;
using Nevron.Nov.Editors;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;
using System;

namespace Nevron.Nov.Examples.Chart
{
    /// <summary>
	/// This example demonstrates how to associate a palette with a bar series
    /// </summary>
    public class NBarPaletteExample : NChartExampleBase
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public NBarPaletteExample()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        static NBarPaletteExample()
        {
			NBarPaletteExampleSchema = NSchema.Create(typeof(NBarPaletteExample), NChartExampleBase.NChartExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides - Example

        protected override NWidget CreateExampleContent()
		{
            NChartView chartView = CreateCartesianChartView();

            // configure title
            chartView.Surface.Titles[0].Text = "Bar Palette";

            // configure chart
            m_Chart = (NCartesianChart)chartView.Surface.Charts[0];
            m_Chart.SetPredefinedCartesianAxes(ENPredefinedCartesianAxis.XOrdinalYLinear);

            // add interlace stripe
            NLinearScale linearScale = m_Chart.Axes[ENCartesianAxis.PrimaryY].Scale as NLinearScale;
            NScaleStrip strip = new NScaleStrip(new NColorFill(ENNamedColor.Beige), null, true, 0, 0, 1, 1);
            strip.Interlaced = true;
            //linearScale.Strips.Add(strip);

            // setup a bar series
            m_Bar = new NBarSeries();
            m_Bar.Name = "Bar Series";
            m_Bar.InflateMargins = true;
            m_Bar.UseXValues = false;
			m_Bar.DataLabelStyle = new NDataLabelStyle(false);

			m_Bar.Palette = new NColorValuePalette(new NColorValuePair[] { new NColorValuePair(0, NColor.Green), new NColorValuePair(60, NColor.Yellow), new NColorValuePair(120, NColor.Red) });

			m_AxisRange = new NRange(0, 130);

			// limit the axis range to 0, 130
			NCartesianAxis yAxis = m_Chart.Axes[ENCartesianAxis.PrimaryY];
			yAxis.ViewRangeMode = ENAxisViewRangeMode.FixedRange;
			yAxis.MinViewRangeValue = m_AxisRange.Begin;
			yAxis.MaxViewRangeValue = m_AxisRange.End;
            m_Chart.Series.Add(m_Bar);
			
            int indicatorCount = 10;
            m_IndicatorPhase = new double[indicatorCount];

            // add some data to the bar series
            for (int i = 0; i < indicatorCount; i++)
			{
				m_IndicatorPhase[i] = i * 30;
                m_Bar.DataPoints.Add(new NBarDataPoint(0));
			}            

			return chartView;
		}

        protected override NWidget CreateExampleControls()
        {
			NStackPanel stack = new NStackPanel();

            NButton toggleTimerButton = new NButton("Stop Timer");
			toggleTimerButton.Click += OnToggleTimerButtonClick;
			toggleTimerButton.Tag = 0;
			stack.Add(toggleTimerButton);

            NComboBox paletteColorModeCombo = new NComboBox();
            paletteColorModeCombo.FillFromEnum<ENPaletteColorMode>();
            paletteColorModeCombo.SelectedIndexChanged += OnPaletteColorModeComboSelectedIndexChanged;
            paletteColorModeCombo.SelectedIndex = (int)ENPaletteColorMode.Spread;
            stack.Add(NPairBox.Create("Palette Color Mode:", paletteColorModeCombo));
            
			NCheckBox invertScaleCheckBox = new NCheckBox("Invert Scale");
			invertScaleCheckBox.CheckedChanged += OnInvertScaleCheckBoxCheckedChanged;
			invertScaleCheckBox.Checked = false;
			stack.Add(invertScaleCheckBox);

			NCheckBox smoothPaletteCheckBox = new NCheckBox("Smooth Palette");
			smoothPaletteCheckBox.CheckedChanged += OnSmoothPaletteCheckBoxCheckedChanged;
			smoothPaletteCheckBox.Checked = true;
			stack.Add(smoothPaletteCheckBox);

			return stack;
        }

		protected override string GetExampleDescription()
		{
			return @"<p>This example demonstrates how to associate a palette with a bar series.</p>";
		}


		#endregion 

		#region Implementation

		private NRangeIndicator CreateRangeIndicator(double offsetFromScale)
		{
			NRangeIndicator rangeIndicator = new NRangeIndicator();
			rangeIndicator.Value = 0;
			rangeIndicator.Stroke = null;
			rangeIndicator.OffsetFromScale = offsetFromScale;
			rangeIndicator.BeginWidth = 10;
			rangeIndicator.EndWidth = 10;

			rangeIndicator.BeginWidth = 10;
			rangeIndicator.EndWidth = 10;

			// assign palette to the indicator
			NColorValuePalette palette = new NColorValuePalette(new NColorValuePair[] { new NColorValuePair(80, NColor.Green), new NColorValuePair(100, NColor.Yellow), new NColorValuePair(120, NColor.Red) });
			rangeIndicator.Palette = palette; 

			return rangeIndicator;
		}

		#endregion

		#region Override

		protected override void OnRegistered()
		{
			base.OnRegistered();

			m_Timer = new NTimer();
			m_Timer.Tick += OnTimerTick;
			m_Timer.Start();
		}

		protected override void OnUnregistered()
		{
			base.OnUnregistered();

			m_Timer.Stop();
			m_Timer.Tick -= OnTimerTick;
			m_Timer = null;
		}

		#endregion
		
		#region Event Handlers

		void OnInvertScaleCheckBoxCheckedChanged(NValueChangeEventArgs arg)
		{
			m_Chart.Axes[ENCartesianAxis.PrimaryY].Scale.Invert = ((NCheckBox)arg.TargetNode).Checked;
		}

        void OnPaletteColorModeComboSelectedIndexChanged(NValueChangeEventArgs arg)
        {
            ENPaletteColorMode paletteColorMode = (ENPaletteColorMode)((NComboBox)arg.TargetNode).SelectedIndex;

            m_Bar.PaletteColorMode = paletteColorMode;
        }

    	void OnToggleTimerButtonClick(NEventArgs arg)
		{
			NButton button = (NButton)arg.TargetNode;
			if ((int)button.Tag == 0)
			{
				m_Timer.Stop();

				button.Content = new NLabel("Start Timer");
				button.Tag = 1;
			}
			else
			{
				m_Timer.Start();
				button.Content = new NLabel("Stop Timer");
				button.Tag = 0;
			}
		}

		void OnTimerTick()
		{
			Random random = new Random();

			for (int i = 0; i < m_Bar.DataPoints.Count; i++)
			{
				double value = (m_AxisRange.Begin + m_AxisRange.End) / 2.0 + Math.Sin(m_IndicatorPhase[i] * NAngle.Degree2Rad) * m_AxisRange.GetLength() / 2 + random.Next(20);
				value = m_AxisRange.GetValueInRange(value);

				m_Bar.DataPoints[i].Value = value;
				m_IndicatorPhase[i] += 10;
			}
		}

		void OnSmoothPaletteCheckBoxCheckedChanged(NValueChangeEventArgs arg)
		{
			bool smoothPalette = ((NCheckBox)arg.TargetNode).Checked;
            m_Bar.Palette.SmoothColors = smoothPalette;
		}

		#endregion

		#region Fields

        NCartesianChart m_Chart;
		NBarSeries m_Bar;
		NTimer m_Timer;
		double[] m_IndicatorPhase;
        NRange m_AxisRange;

		#endregion

		#region Static

		public static readonly NSchema NBarPaletteExampleSchema;

        #endregion

		#region Default Values

		#endregion
	}
}
