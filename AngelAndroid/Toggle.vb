Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Public Class Toggle
    Inherits System.Windows.Forms.UserControl
    Private _checked As Boolean
    Public Property Checked As Boolean
        Get
            Return _checked
        End Get
        Set(ByVal value As Boolean)
            If Not _checked.Equals(value) Then
                _checked = value
                Me.OnCheckedChanged()
            End If
        End Set
    End Property
    Protected Overridable Sub OnCheckedChanged()
        RaiseEvent CheckedChanged(Me, EventArgs.Empty)
    End Sub
    Public Event CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        Me.Checked = Not Me.Checked
        Me.Invalidate()
        MyBase.OnMouseClick(e)
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Me.OnPaintBackground(e)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        Using path = New GraphicsPath()
            Dim d = Padding.All
            Dim r = Me.Height - 2 * d
            path.AddArc(d, d, r, r, 90, 180)
            path.AddArc(Me.Width - r - d, d, r, r, -90, 180)
            path.CloseFigure()
            e.Graphics.FillPath(If(Checked, Brushes.DarkGray, Brushes.LightGray), path)
            r = Height - 1
            Dim rect = If(Checked, New System.Drawing.Rectangle(Width - r - 1, 0, r, r), New System.Drawing.Rectangle(0, 0, r, r))
            e.Graphics.FillEllipse(If(Checked, Brushes.Lime, Brushes.White), rect)
        End Using
    End Sub
End Class