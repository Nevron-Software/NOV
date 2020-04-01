using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Layout;
using Nevron.Nov.UI;
using System;

namespace Nevron.Nov.Examples.UI
{
    public class NButtonsExample : NExampleBase
    {
        #region Constructors

        public NButtonsExample()
        {
        }
        static NButtonsExample()
        {
            NButtonsExampleSchema = NSchema.Create(typeof(NButtonsExample), NExampleBase.NExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides - Example

        protected override NWidget CreateExampleContent()
        {
            // create the host
            NStackPanel stack = new NStackPanel();
            stack.HorizontalPlacement = ENHorizontalPlacement.Left;
            stack.VerticalPlacement = ENVerticalPlacement.Top;
            stack.VerticalSpacing = 5;

            // text only push button
            NButton textOnlyButton = new NButton("Text only button");
            textOnlyButton.Click += new Function<NEventArgs>(OnButtonClicked);
            stack.Add(textOnlyButton);

            // image only push button
            NButton imageOnlyButton = new NButton(NResources.Image__16x16_Contacts_png);
            imageOnlyButton.Click += new Function<NEventArgs>(OnButtonClicked);
            stack.Add(imageOnlyButton);

            // image and text button
            NImage image2 = NResources.Image__16x16_Mail_png;
            NButton imageAndTextButton = new NButton(new NPairBox("Image before text", image2, ENPairBoxRelation.Box2BeforeBox1, false));
            imageAndTextButton.Click += new Function<NEventArgs>(OnButtonClicked);
            stack.Add(imageAndTextButton);

            // repeat button
            NRepeatButton repeatButton = new NRepeatButton("Repeat button");
            repeatButton.StartClicking += new Function<NEventArgs>(OnRepeatButtonStartClicking);
            repeatButton.Click += new Function<NEventArgs>(OnButtonClicked);
            repeatButton.EndClicking += new Function<NEventArgs>(OnRepeatButtonEndClicking);
            stack.Add(repeatButton);

            // toggle button
            NToggleButton toggleButton = new NToggleButton("Toggle button");
            toggleButton.Click += new Function<NEventArgs>(OnButtonClicked);
            stack.Add(toggleButton);

            // disabled button
            NButton disabledButton = new NButton("Disabled Button");
            disabledButton.Enabled = false;
            stack.Add(disabledButton);

            return stack;
        }
        protected override NWidget CreateExampleControls()
        {
            NStackPanel stack = new NStackPanel();
            stack.FillMode = ENStackFillMode.Last;
            stack.FitMode = ENStackFitMode.Last;

            // create the events list box
            m_EventsLog = new NExampleEventsLog();
            stack.Add(m_EventsLog);

            return stack;
        }
        protected override string GetExampleDescription()
        {
            return @"
<p>
	This example demonstrates the different types of buttons supported by NOV.
</p>
";
        }

        #endregion

        #region Event Handlers

        private void OnButtonClicked(NEventArgs arg1)
        {
            m_EventsLog.LogEvent("Button clicked");
        }
        private void OnRepeatButtonStartClicking(NEventArgs arg1)
        {
            m_EventsLog.LogEvent("Repeat Button Start Clicking");
        }
        private void OnRepeatButtonEndClicking(NEventArgs arg1)
        {
            m_EventsLog.LogEvent("Repeat Button End Clicking");
        }

        #endregion

        #region Fields

        private NExampleEventsLog m_EventsLog;

        #endregion

        #region Schema

        public static readonly NSchema NButtonsExampleSchema;

        #endregion
    }
}