using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;
using System.Text;

namespace Nevron.Nov.Examples.Diagram
{
    /// <summary>
    /// 
    /// </summary>
    public class NSpellCheckExample : NDiagramExampleBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NSpellCheckExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NSpellCheckExample()
        {
            NSpellCheckExampleSchema = NSchema.Create(typeof(NSpellCheckExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
        }

        #endregion

        #region Overrides from NDiagramExampleBase

        protected override string GetExampleDescription()
        {
            return @"<p>
						Demonstrates how to enable the build in spell check.
					</p>";
        }
        protected override void InitDiagram()
        {
            base.InitDiagram();

            NDrawing drawing = m_DrawingDocument.Content;
            NPage activePage = drawing.ActivePage;

            // hide the grid
            drawing.ScreenVisibility.ShowGrid = false;

			NBasicShapeFactory basicShapesFactory = new NBasicShapeFactory();

			NShape shape1 = basicShapesFactory.CreateShape(ENBasicShape.Rectangle);
			shape1.SetBounds(10, 10, 200, 200);
			shape1.TextBlock = new NTextBlock();
			shape1.TextBlock.Padding = new NMargins(20);
			shape1.TextBlock.Text = "This text cantains many typpos. This text contuins manyy typos.";
			drawing.ActivePage.Items.Add(shape1);
		}
		protected override NWidget CreateExampleControls()
		{
			NStackPanel stack = new NStackPanel();

			NCheckBox enableSpellCheck = new NCheckBox("Enable Spell Check");
			enableSpellCheck.Click += new Function<NEventArgs>(OnEnableSpellCheckButtonClick);
			stack.Add(enableSpellCheck);

			return stack;
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Called when the user presses the find all button
		/// </summary>
		/// <param name="arg"></param>
		void OnEnableSpellCheckButtonClick(NEventArgs arg)
		{
			m_DrawingView.SpellChecker.Enabled = ((NCheckBox)arg.TargetNode).Checked;
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NSpellCheckExample.
		/// </summary>
		public static readonly NSchema NSpellCheckExampleSchema;

        #endregion
    }
}