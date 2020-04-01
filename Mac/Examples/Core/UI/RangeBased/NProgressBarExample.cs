using Nevron.Nov.Dom;
using Nevron.Nov.Editors;
using Nevron.Nov.Graphics;
using Nevron.Nov.Layout;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.UI
{
	public class NProgressBarExample : NExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NProgressBarExample()
		{
		}
		/// <summary>
		/// Static constructor.
		/// </summary>
		static NProgressBarExample()
		{
			NProgressBarExampleSchema = NSchema.Create(typeof(NProgressBarExample), NExampleBase.NExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override NWidget CreateExampleContent()
		{
			NStackPanel stack = new NStackPanel();
			stack.HorizontalPlacement = ENHorizontalPlacement.Left;
			stack.VerticalPlacement = ENVerticalPlacement.Top;

			// Horizontal progress bar
			m_HorizontalProgressBar = new NProgressBar();
			m_HorizontalProgressBar.Style = ENProgressBarStyle.Horizontal;
			m_HorizontalProgressBar.Value = DefaultProgress;
			m_HorizontalProgressBar.PreferredSize = new NSize(300, 30);
			m_HorizontalProgressBar.VerticalPlacement = ENVerticalPlacement.Top;
			stack.Add(new NGroupBox("Horizontal", m_HorizontalProgressBar));

			// Vertical progress bar
			m_VerticalProgressBar = new NProgressBar();
			m_VerticalProgressBar.Style = ENProgressBarStyle.Vertical;
			m_VerticalProgressBar.Value = DefaultProgress;
			m_VerticalProgressBar.PreferredSize = new NSize(30, 300);
			m_VerticalProgressBar.HorizontalPlacement = ENHorizontalPlacement.Left;			
			stack.Add(new NGroupBox("Vertical", m_VerticalProgressBar));

			// Circular progress bar - 50% rim
			m_CircularProgressBar1 = new NProgressBar();
			m_CircularProgressBar1.Style = ENProgressBarStyle.Circular;
			m_CircularProgressBar1.Value = DefaultProgress;
			m_CircularProgressBar1.PreferredSize = new NSize(150, 150);

			// Circular progress bar - 100% rim
			m_CircularProgressBar2 = new NProgressBar();
			m_CircularProgressBar2.Style = ENProgressBarStyle.Circular;
			m_CircularProgressBar2.Value = DefaultProgress;
			m_CircularProgressBar2.RimWidthPercent = 100;
			m_CircularProgressBar2.PreferredSize = new NSize(150, 150);

			NPairBox pairBox = new NPairBox(m_CircularProgressBar1, m_CircularProgressBar2);
			pairBox.Spacing = 30;

			stack.Add(new NGroupBox("Circular", pairBox));

			return stack;
		}
		protected override NWidget CreateExampleControls()
		{
			NTableFlowPanel table = new NTableFlowPanel();
			table.MaxOrdinal = 2;

			NLabel label = new NLabel("Value:");
			label.HorizontalPlacement = ENHorizontalPlacement.Right;
			label.VerticalPlacement = ENVerticalPlacement.Center;
			table.Add(label);

			NNumericUpDown numericUpDown = new NNumericUpDown();
			numericUpDown.Minimum = 0;
			numericUpDown.Maximum = 100;
			numericUpDown.Value = DefaultProgress;
			numericUpDown.ValueChanged += new Function<NValueChangeEventArgs>(OnValueChanged);
			table.Add(numericUpDown);

			label = new NLabel("Label Style:");
			label.HorizontalPlacement = ENHorizontalPlacement.Right;
			label.VerticalPlacement = ENVerticalPlacement.Center;
			table.Add(label);

			NComboBox comboBox = new NComboBox();
			comboBox.FillFromEnum<ENProgressBarLabelStyle>();
			comboBox.SelectedIndex = (int)m_HorizontalProgressBar.LabelStyle;
			comboBox.SelectedIndexChanged += OnLabelStyleSelected;
			table.Add(comboBox);

			return table;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to create and use progress bars. The progress bar is a widget that
	fills to indicate the progress of an operation. The <b>Style</b> property determines whether
	it is horizontally, vertically oriented or circular. The <b>Minimum</b> and <b>Maximum</b> properties
	determine the start and the end of the operation and the <b>Value</b> property indicates its current progress.
	All progress bars can have a label and its style is controlled through the <b>LabelStyle</b> property.
	Circular progress bars let you specify the width of their rim in percent relative to the size of the
	progress bar as this example demonstrates.
</p>
";
		}

		#endregion

		#region Event Handlers

		private void OnValueChanged(NValueChangeEventArgs args)
		{
			double value = (double)args.NewValue;
			m_HorizontalProgressBar.Value = value;
			m_VerticalProgressBar.Value = value;
			m_CircularProgressBar1.Value = value;
			m_CircularProgressBar2.Value = value;
		}
		private void OnLabelStyleSelected(NValueChangeEventArgs arg)
		{
			ENProgressBarLabelStyle labelStyle = (ENProgressBarLabelStyle)(int)arg.NewValue;
			m_HorizontalProgressBar.LabelStyle = labelStyle;
			m_VerticalProgressBar.LabelStyle = labelStyle;
			m_CircularProgressBar1.LabelStyle = labelStyle;
			m_CircularProgressBar2.LabelStyle = labelStyle;
		}

		#endregion

		#region Fields

		private NProgressBar m_HorizontalProgressBar;
		private NProgressBar m_VerticalProgressBar;
		private NProgressBar m_CircularProgressBar1;
		private NProgressBar m_CircularProgressBar2;

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NProgressBarExample.
		/// </summary>
		public static readonly NSchema NProgressBarExampleSchema;

		#endregion

		#region Constants

		private const double DefaultProgress = 50;

		#endregion
	}
}