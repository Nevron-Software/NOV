using Nevron.Nov.DataStructures;
using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Diagram
{
    public class NSpringGraphLayoutExample : NLayoutExampleBase<NSpringGraphLayout>
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NSpringGraphLayoutExample()
        {
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static NSpringGraphLayoutExample()
        {
            NSpringGraphLayoutExampleSchema = NSchema.Create(typeof(NSpringGraphLayoutExample), NLayoutExampleBase<NSpringGraphLayout>.NLayoutExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides - Example

        protected override void InitDiagram()
        {
            base.InitDiagram();

            NPage activePage = m_DrawingDocument.Content.ActivePage;

            // we will be using basic shapes for this example
            NBasicShapeFactory basicShapesFactory = new NBasicShapeFactory();
            basicShapesFactory.DefaultSize = new NSize(80, 80);

            NList<NPerson> persons = new NList<NPerson>();

            // create persons
            NPerson personEmil = new NPerson("Emil Moore", basicShapesFactory.CreateShape(ENBasicShape.Circle));
            NPerson personAndre = new NPerson("Andre Smith", basicShapesFactory.CreateShape(ENBasicShape.Circle));
            NPerson personRobert = new NPerson("Robert Johnson", basicShapesFactory.CreateShape(ENBasicShape.Circle));
            NPerson personBob = new NPerson("Bob Williams", basicShapesFactory.CreateShape(ENBasicShape.Circle));
            NPerson personPeter = new NPerson("Peter Brown", basicShapesFactory.CreateShape(ENBasicShape.Circle));
            NPerson personSilvia = new NPerson("Silvia Moore", basicShapesFactory.CreateShape(ENBasicShape.Circle));
            NPerson personEmily = new NPerson("Emily Smith", basicShapesFactory.CreateShape(ENBasicShape.Circle));
            NPerson personMonica = new NPerson("Monica Johnson", basicShapesFactory.CreateShape(ENBasicShape.Circle));
            NPerson personSamantha = new NPerson("Samantha Miller", basicShapesFactory.CreateShape(ENBasicShape.Circle));
            NPerson personIsabella = new NPerson("Isabella Davis", basicShapesFactory.CreateShape(ENBasicShape.Circle));

            persons.Add(personEmil);
            persons.Add(personAndre);
            persons.Add(personRobert);
            persons.Add(personBob);
            persons.Add(personPeter);
            persons.Add(personSilvia);
            persons.Add(personEmily);
            persons.Add(personMonica);
            persons.Add(personSamantha);
            persons.Add(personIsabella);

            // create family relashionships
            personEmil.m_Family = personSilvia;
            personAndre.m_Family = personEmily;
            personRobert.m_Family = personMonica;

            // create friend relationships
            personEmily.m_Friends.Add(personBob);
            personEmily.m_Friends.Add(personMonica);

            personAndre.m_Friends.Add(personPeter);
            personAndre.m_Friends.Add(personIsabella);

            personSilvia.m_Friends.Add(personBob);
            personSilvia.m_Friends.Add(personSamantha);
            personSilvia.m_Friends.Add(personIsabella);

            personEmily.m_Friends.Add(personIsabella);
            personEmily.m_Friends.Add(personPeter);

            personPeter.m_Friends.Add(personRobert);

            // create the person vertices
            for (int i = 0; i < persons.Count; i++)
            {
                activePage.Items.Add(persons[i].m_Shape);
            }

            // creeate the family relations
            for (int i = 0; i < persons.Count; i++)
            {
                NPerson currentPerson = persons[i];

                if (currentPerson.m_Family != null)
                {
                    NRoutableConnector connector = new NRoutableConnector();
                    connector.MakeLine();
                    activePage.Items.Add(connector);

                    connector.GlueBeginToShape(currentPerson.m_Shape);
                    connector.GlueEndToShape(currentPerson.m_Family.m_Shape);

                    connector.Geometry.Stroke = new NStroke(2, NColor.Coral);

                    connector.LayoutData.SpringStiffness = 2;
                    connector.LayoutData.SpringLength = 100;
                }
            }

            for (int i = 0; i < persons.Count; i++)
            {
                NPerson currentPerson = persons[i];
                for (int j = 0; j < currentPerson.m_Friends.Count; j++)
                {
                    NRoutableConnector connector = new NRoutableConnector();
                    connector.MakeLine();
                    activePage.Items.Add(connector);

                    connector.GlueBeginToShape(currentPerson.m_Shape);
                    connector.GlueEndToShape(currentPerson.m_Friends[j].m_Shape);

                    connector.Geometry.Stroke = new NStroke(2, NColor.Green);

                    connector.LayoutData.SpringStiffness = 1;
                    connector.LayoutData.SpringLength = 200;
                }
            }

            // arrange diagram
            ArrangeDiagram();

            // fit active page
            m_DrawingDocument.Content.ActivePage.ZoomMode = ENZoomMode.Fit;
        }
        protected override string GetExampleDescription()
        {
            return @"
<p>
    The spring layout method is a classical force directed layout, which uses spring and electrical forces.
</p>
<p>
    Graph edges are treated as springs. Springs aim to ensure that the distance between adjacent vertices is
	approximately equal to the spring length. The parameters of the spring force are controlled by an instance
	of the <b>NSpringForce</b> class, accessible from the SpringForce property.
</p>
<p>
    Graph vertices are treated as electrically charged particles, which repel each other. The electrical 
	force aims to ensure that vertices should not be close together. The parameters of the electrical force
	are controlled by an instance of the <b>NElectricalForce</b> class, accessible from the ElectricalForce property.
</p>
<p>
	The spring force accepts per edge defined spring lengths and spring stiffness. In this example the red
	connectors are with smaller spring length are with greater stiffness than the blue connectors. Because
	of that they tend to be displayed closer to each other.
</p>
<p> 
	The electrical force accepts per vertex provided electric charges. Thus some vertices may be more repulsive than others.
</p>
<p>
	To experiment with the layout behavior just change the properties of the layout in the property grid and
	click the <b>Layout</b> button.
</p>
            ";
        }
        protected override ENBasicShape GetDefaultShapeType()
        {
            return ENBasicShape.Circle;
        }

        #endregion

        #region Schema

        /// <summary>
        /// Schema associated with NSpringGraphLayoutExample.
        /// </summary>
        public static readonly NSchema NSpringGraphLayoutExampleSchema;

        #endregion

		#region Nested Types

		public class NPerson
        {
            public NPerson(string name, NShape shape)
            {
                m_Shape = shape;
                
                m_Shape.Text = name;
                m_Shape.DefaultShapeGlue = ENDefaultShapeGlue.GlueToGeometryIntersection;


                m_Name = name;
                m_Friends = new NList<NPerson>();
                m_Family = null;
            }

            public NShape m_Shape;
            public string m_Name;
            public NList<NPerson> m_Friends;
            public NPerson m_Family;
		}

		#endregion
	}
}