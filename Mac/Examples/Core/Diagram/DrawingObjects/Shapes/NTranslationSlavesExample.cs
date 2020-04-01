using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Batches;
using Nevron.Nov.Diagram.Expressions;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.UI;
using Nevron.Nov.Graphics;
using Nevron.Nov.Editors;
using Nevron.Nov.DataStructures;

namespace Nevron.Nov.Examples.Diagram
{
    /// <summary>
    /// 
    /// </summary>
    public class NTranslationSlavesExample : NDiagramExampleBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NTranslationSlavesExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NTranslationSlavesExample()
        {
            NTranslationSlavesExampleSchema = NSchema.Create(typeof(NTranslationSlavesExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
        }

        #endregion

        #region Overrides from NDiagramExampleBase

        protected override string GetExampleDescription()
        {
            return @"
<p>
This example demonstrates the shape move slaves. 
</p>
<p>
It is in many cases necessary to implement rigid connected shapes structures. In NOV Diagram this is achieved with the help of shape move slaves. 
</p>
<p>
The move slaves of a shape are such shapes, which are moved together with the shape. The accumulation of the shape move slaves is recursive. For example: if a shape has to be moved, because it is a move slave, then its move slaves will also be moved. 
</p>
<p>
To explore move slaves behavior just select a shape in the diagram and check the shapes, which must be moved when it is move. Then move the shape. 
</p>
<p>
Note that the example will automatically highlight the move slaves of the currently selected shape. 
</p>
            ";
        }
        protected override void InitDiagram()
        {
            base.InitDiagram();

			NGraphTemplate template;

			// create rectangular grid template
			template = new NRectangularGridTemplate();
			template.Origin = new NPoint(10, 23);
			template.VerticesShape = ENBasicShape.Rectangle;
            template.EdgesUserClass = NDR.StyleSheetNameConnectors;
            template.Create(m_DrawingDocument);

			// create tree template
			template = new NGenericTreeTemplate();
			template.Origin = new NPoint(250, 23);
            template.VerticesShape = ENBasicShape.Triangle;
            template.EdgesUserClass = NDR.StyleSheetNameConnectors;
            template.Create(m_DrawingDocument);

			// create elliptical grid template
			template = new NEllipticalGridTemplate();
			template.Origin = new NPoint(10, 250);
            template.VerticesShape = ENBasicShape.Ellipse;
            template.EdgesUserClass = NDR.StyleSheetNameConnectors;
			template.Create(m_DrawingDocument);

            // hook selection events
            NPageSelection selection = m_DrawingDocument.Content.ActivePage.Selection;
            selection.Mode = Nov.UI.ENSelectionMode.Single;
            selection.Selected += OnSelectionSelected;
            selection.Deselected += OnSelectionDeselected;
        }

        protected override Nov.UI.NWidget CreateExampleControls()
        {
            m_PropertyEditorHolder = new NContentHolder();
            return m_PropertyEditorHolder;
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Called when a diagram item has been selected.
        /// </summary>
        /// <param name="arg"></param>
        void OnSelectionSelected(NSelectEventArgs<NDiagramItem> arg)
        {
            NShape shape = arg.Item as NShape;
            if (shape == null)
                return;
            
            // create the shape move slaves property editor
            m_PropertyEditorHolder.Content = NDesigner.GetDesigner(shape).CreatePropertyEditor(shape, NShape.MoveSlavesProperty);
                
            // subscribe for move slaves property changes.
            shape.AddEventHandler(NShape.MoveSlavesProperty.ValueChangedEvent, new NEventHandler<NValueChangeEventArgs>(OnMoveSlavesPropertyChanged));

            // highlight the shape current slaves
            HighlightSlaves(shape);
        }
        /// <summary>
        /// Called when a diagram item has been deselected.
        /// </summary>
        /// <param name="arg"></param>
        void OnSelectionDeselected(NSelectEventArgs<NDiagramItem> arg)
        {
            NShape shape = arg.Item as NShape;
            if (shape == null)
                return;

            // unhook move slaves property changed event
            shape.RemoveEventHandler(NShape.MoveSlavesProperty.ValueChangedEvent, new NEventHandler<NValueChangeEventArgs>(OnMoveSlavesPropertyChanged));
            
            // clear hightlights
            ClearHighlights();

            // destroy property editor
            m_PropertyEditorHolder.Content = null;
        }
        /// <summary>
        /// Called when the MoveSlaves property of the currently selected shape has changed.
        /// </summary>
        /// <param name="arg"></param>
        void OnMoveSlavesPropertyChanged(NValueChangeEventArgs arg)
        {
            NShape shape = arg.TargetNode as NShape;
            if (shape == null)
                return;

            // highligh the shape move slaves
            HighlightSlaves(shape);
        }
        /// <summary>
        /// Clears the highlighting on all shapes in the active page.
        /// </summary>
        void ClearHighlights()
        {
            NList<NShape> shapes = m_DrawingDocument.Content.ActivePage.GetShapes(true);
            for (int i = 0; i < shapes.Count; i++)
            {
                NShape shape = shapes[i];
                shape.Geometry.ClearValue(NGeometry.FillProperty);
                shape.Geometry.ClearValue(NGeometry.StrokeProperty);
            }
        }
        /// <summary>
        /// Highlights the move slaves of the specified shape
        /// </summary>
        /// <param name="shape"></param>
        void HighlightSlaves(NShape shape)
        {
            ClearHighlights();

            NList<NShape> shapes = shape.GetMoveSlaves();
            for (int i = 0; i < shapes.Count; i++)
            {
                NShape cur = shapes[i];
                cur.Geometry.Fill = new NColorFill(NColor.LightCoral);
                cur.Geometry.Stroke = new NStroke(2, NColor.DarkRed);
            }
        }

        #endregion

        #region Fields

        NContentHolder m_PropertyEditorHolder;

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NTranslationSlavesExample.
        /// </summary>
        public static readonly NSchema NTranslationSlavesExampleSchema;

        #endregion
    }
}