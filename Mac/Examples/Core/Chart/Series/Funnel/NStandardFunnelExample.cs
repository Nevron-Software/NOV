﻿using Nevron.Nov.Chart;
using Nevron.Nov.Dom;
using Nevron.Nov.Editors;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Chart
{
	/// <summary>
	/// Standard Funnel Example
	/// </summary>
	public class NStandardFunnelExample : NExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public NStandardFunnelExample()
		{
			
		}
		/// <summary>
		/// Static constructor
		/// </summary>
		static NStandardFunnelExample()
		{
			NStandardFunnelExampleSchema = NSchema.Create(typeof(NStandardFunnelExample), NExampleBaseSchema);
		}

		#endregion

		#region Example

		protected override NWidget CreateExampleContent()
		{
			NChartView chartView = CreateFunnelChartView();

			// configure title
			chartView.Surface.Titles[0].Text = "Standard Funnel";

			NFunnelChart funnelChart = (NFunnelChart)chartView.Surface.Charts[0];

			m_FunnelSeries = new NFunnelSeries();
			funnelChart.Series.Add(m_FunnelSeries);

			m_FunnelSeries.DataPoints.Add(new NFunnelDataPoint(20.0, "Awareness"));
			m_FunnelSeries.DataPoints.Add(new NFunnelDataPoint(10.0, "First Hear"));
			m_FunnelSeries.DataPoints.Add(new NFunnelDataPoint(15.0, "Further Learn"));
			m_FunnelSeries.DataPoints.Add(new NFunnelDataPoint(7.0, "Liking"));
			m_FunnelSeries.DataPoints.Add(new NFunnelDataPoint(28.0, "Decision"));

			chartView.Document.StyleSheets.ApplyTheme(new NChartTheme(ENChartPalette.Bright, true));

			return chartView;
		}
		protected override NWidget CreateExampleControls()
		{
			NStackPanel stack = new NStackPanel();
			NUniSizeBoxGroup group = new NUniSizeBoxGroup(stack);
			
			NComboBox funnelShapeCombo = new NComboBox();
			funnelShapeCombo.FillFromEnum<ENFunnelShape>();
			funnelShapeCombo.SelectedIndexChanged += OnFunnelShapeComboSelectedIndexChanged;
			funnelShapeCombo.SelectedIndex = (int)ENFunnelShape.Trapezoid;
			stack.Add(NPairBox.Create("Funnel Shape:", funnelShapeCombo));

			NComboBox labelAligmentModeCombo = new NComboBox();
			labelAligmentModeCombo.FillFromEnum<ENFunnelLabelMode>();
			labelAligmentModeCombo.SelectedIndexChanged += OnLabelAligmentModeComboSelectedIndexChanged;
			labelAligmentModeCombo.SelectedIndex = (int)ENFunnelLabelMode.Center;
			stack.Add(NPairBox.Create("Label Alignment:", labelAligmentModeCombo));

			NNumericUpDown labelArrowLengthUpDown = new NNumericUpDown();
			labelArrowLengthUpDown.Value = m_FunnelSeries.LabelArrowLength;
			labelArrowLengthUpDown.ValueChanged += OnLabelArrowLengthUpDownValueChanged;
			stack.Add(NPairBox.Create("Label Arrow Length:", labelArrowLengthUpDown));

			NNumericUpDown pointGapUpDown = new NNumericUpDown();
			pointGapUpDown.Value = m_FunnelSeries.PointGapPercent;
			pointGapUpDown.ValueChanged += OnPointGapUpDownValueChanged;
			stack.Add(NPairBox.Create("Point Gap Percent:", pointGapUpDown));

			return group;
		}
		protected override string GetExampleDescription()
		{
			return @"<p>This example demonstrates how to create a standard funnel chart.</p>";
		}

		#endregion

		#region Event Handlers

		void OnLabelArrowLengthUpDownValueChanged(NValueChangeEventArgs arg)
		{
			m_FunnelSeries.LabelArrowLength = ((NNumericUpDown)arg.TargetNode).Value;
		}

		void OnLabelAligmentModeComboSelectedIndexChanged(NValueChangeEventArgs arg)
		{
			m_FunnelSeries.LabelMode = (ENFunnelLabelMode)((NComboBox)arg.TargetNode).SelectedIndex;
		}

		void OnPointGapUpDownValueChanged(NValueChangeEventArgs arg)
		{
			m_FunnelSeries.PointGapPercent = ((NNumericUpDown)arg.TargetNode).Value;
		}

		void OnFunnelShapeComboSelectedIndexChanged(NValueChangeEventArgs arg)
		{
			m_FunnelSeries.Shape = (ENFunnelShape)((NComboBox)arg.TargetNode).SelectedIndex;
		}

		#endregion

		#region Fields

		NFunnelSeries m_FunnelSeries;

		#endregion

		#region Schema

		public static readonly NSchema NStandardFunnelExampleSchema;

		#endregion

		#region Static Methods

		private static NChartView CreateFunnelChartView()
		{
			NChartView chartView = new NChartView();
			chartView.Surface.CreatePredefinedChart(ENPredefinedChartType.Funnel);
			return chartView;
		}

		#endregion
	}
}