Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Polar area example
	''' </summary>
	Public Class NPolarAreaExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
        End Sub
		''' <summary>
		''' Static constructor
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Chart.NPolarAreaExample.NPolarAreaExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NPolarAreaExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NPolarAreaExample.CreatePolarChartView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Polar Area"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NPolarChart)
            Me.m_Chart.SetPredefinedPolarAxes(Nevron.Nov.Chart.ENPredefinedPolarAxes.AngleValue)

			' setup polar axis
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryValue), Nevron.Nov.Chart.ENPolarAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            linearScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.MajorTick
            linearScale.InflateViewRangeBegin = True
            linearScale.InflateViewRangeEnd = True
            linearScale.MajorGridLines.Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Black)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.Beige, 0.5F))
            strip.Interlaced = True
            linearScale.Strips.Add(strip)

			' setup polar angle axis
			Dim angularScale As Nevron.Nov.Chart.NAngularScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryAngle), Nevron.Nov.Chart.ENPolarAxis)).Scale, Nevron.Nov.Chart.NAngularScale)
            strip = New Nevron.Nov.Chart.NScaleStrip()
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromRGBA(192, 192, 192, 125))
            strip.Interlaced = True
            angularScale.Strips.Add(strip)

			' polar area series 1
			Dim series1 As Nevron.Nov.Chart.NPolarAreaSeries = New Nevron.Nov.Chart.NPolarAreaSeries()
            Me.m_Chart.Series.Add(series1)
            series1.Name = "Theoretical"
            series1.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.GenerateData(series1, 100, 15.0)

			' polar area series 2
			Dim series2 As Nevron.Nov.Chart.NPolarAreaSeries = New Nevron.Nov.Chart.NPolarAreaSeries()
            Me.m_Chart.Series.Add(series2)
            series2.Name = "Experimental"
            series2.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.GenerateData(series2, 100, 10.0)

			' apply style sheet
			chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a polar area chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

	

		#EndRegion

		#Region"Implementation"

		Private Sub GenerateData(ByVal series As Nevron.Nov.Chart.NPolarAreaSeries, ByVal count As Integer, ByVal scale As Double)
            series.DataPoints.Clear()
            Dim angleStep As Double = 2 * System.Math.PI / count
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To count - 1
                Dim angle As Double = i * angleStep
                Dim c1 As Double = 1.0 * System.Math.Sin(angle * 3)
                Dim c2 As Double = 0.3 * System.Math.Sin(angle * 1.5)
                Dim c3 As Double = 0.05 * System.Math.Cos(angle * 26)
                Dim c4 As Double = 0.05 * (0.5 - random.NextDouble())
                Dim value As Double = scale * (System.Math.Abs(c1 + c2 + c3) + c4)
                If value < 0 Then value = 0
                series.DataPoints.Add(New Nevron.Nov.Chart.NPolarAreaDataPoint(angle * 180 / System.Math.PI, value))
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NPolarChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NPolarAreaExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreatePolarChartView() As Nevron.Nov.Chart.NChartView
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Polar)
            Return chartView
        End Function

		#EndRegion
	End Class
End Namespace
