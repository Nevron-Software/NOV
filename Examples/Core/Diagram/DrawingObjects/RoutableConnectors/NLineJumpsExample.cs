using Nevron.Nov.Diagram;
using Nevron.Nov.Dom;
using Nevron.Nov.Editors;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
	public class NLineJumpsExample : NDiagramExampleBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NLineJumpsExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NLineJumpsExample()
        {
            NLineJumpsExampleSchema = NSchema.Create(typeof(NLineJumpsExample), NDiagramExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides - Example

        protected override void InitDiagram()
        {
            NDrawing drawing = m_DrawingDocument.Content;
            NPage activePage = drawing.ActivePage;

            activePage.Items.Add(CreateSampleLine1());
            activePage.Items.Add(CreateSampleLine2());
            activePage.Items.Add(CreateSamplePolyline1());
            activePage.Items.Add(CreateSamplePolyline2());
            activePage.Items.Add(CreateSampleLineDoubleBridge());
        }
        protected override NWidget CreateExampleControls()
        {
            NStackPanel stack = (NStackPanel)base.CreateExampleControls();

            NDrawing drawing = m_DrawingDocument.Content;
            NPage activePage = drawing.ActivePage;

            NEditor editor = NDesigner.GetDesigner(activePage.LineJumps).CreateStateEditor(activePage.LineJumps);
            stack.Add(editor);

            return stack;
        }
        protected override string GetExampleDescription()
        {
            return @"
<p>This example demonstrates the line jumps. Line jumps are shown at connector crossing points.</p>
";
        }

        #endregion

        #region Implementation - Connectors

        private NRoutableConnector CreateSampleLine1()
		{
            NRoutableConnector connector = new NRoutableConnector();
            connector.MakeLine();
			connector.Geometry.Stroke = new NStroke(NColor.BlueViolet);

            connector.BeginX = 10;
            connector.BeginY = 130;

            connector.EndX = 250;
            connector.EndY = 130;

            return connector;
		}
		private NRoutableConnector CreateSampleLine2()
		{
            NRoutableConnector connector = new NRoutableConnector();
            connector.MakeLine();
            connector.Geometry.Stroke = new NStroke(NColor.Orange);

            connector.BeginX = 10;
            connector.BeginY = 75;

            connector.EndX = 280;
            connector.EndY = 75;

            return connector;
        }
        private NRoutableConnector CreateSamplePolyline1()
        {
            NPoint[] points = new NPoint[] {
                new NPoint(10, 210),
                new NPoint(75, 10),
                new NPoint(75, 10),
                new NPoint(75, 175),
                new NPoint(145, 175),
                new NPoint(145, 10),
                new NPoint(210, 75),
                new NPoint(210, 210),
                new NPoint(105, 210),
                new NPoint(105, 105),
            };

            NRoutableConnector connector = new NRoutableConnector();
            connector.MakePolyline(points);

            connector.SetBeginPoint(points[0]);
            connector.SetEndPoint(points[points.Length - 1]);

            return connector;
        }		
		private NRoutableConnector CreateSamplePolyline2()
		{
			NPoint[] points = new NPoint[] {
				new NPoint(212, 250),
				new NPoint(174, 250),
				new NPoint(174, 169),
				new NPoint(242, 169),
				new NPoint(242, 208),
			};

            NRoutableConnector connector = new NRoutableConnector();
            connector.MakePolyline(points);
            connector.Geometry.Stroke = new NStroke(NColor.OrangeRed);

            connector.SetBeginPoint(points[0]);
            connector.SetEndPoint(points[points.Length - 1]);

            return connector;
		}		
		private NRoutableConnector CreateSampleLineDoubleBridge()
		{
            NRoutableConnector connector = new NRoutableConnector();
            connector.MakeLine();
            connector.Geometry.Stroke = new NStroke(NColor.Green);

            connector.BeginX = 50;
            connector.BeginY = 300;

            connector.EndX = 206;
            connector.EndY = 14;

            return connector;
		}

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NLineJumpsExample.
        /// </summary>
        public static readonly NSchema NLineJumpsExampleSchema;

        #endregion
    }
}
