Imports System.ComponentModel
 <ToolboxBitmap("ZoomPictureBox.bmp")>
Public Class ZoomPictureBox
    Inherits UserControl
 #Region "Constructor"
     Public Sub New()
         InitializeComponent()
         Me.DoubleBuffered = True
        Me.BackColor = Color.FromKnownColor(KnownColor.AppWorkspace)
        Me.Size = New Size(200, 200)
    End Sub
 #End Region
 #Region "Public Variables"
      Public MouseWheelDivisor As Integer = 4000
    Public MinimumImageWidth As Integer = 100
    Public MinimumImageHeight As Integer = 100
    Public MaximumZoomFactor As Double = 16
    Public EnableMouseWheelZooming As Boolean = True
    Public EnableMouseDragging As Boolean = True
#End Region
 #Region "Private Variables"
    Private _ImageBounds As Rectangle
    Private _ZoomFactor As Double
    Private _Image As Image
    Private _startDrag As Point
    Private _dragging As Boolean
#End Region
 #Region "Enums"
     Public Enum ZoomType
        MousePosition
        ControlCenter
        ImageCenter
    End Enum
 #End Region
 #Region "Properties"
     <Category("_ZoomPictureBox")>
    Public Property Image() As Image
        Get
            Return _Image
        End Get
        Set(ByVal value As Image)
            _Image = value
            InitializeImage()
        End Set
    End Property
     Private _ZoomMode As ZoomType = ZoomType.MousePosition
    <Category("_ZoomPictureBox"), DefaultValue(ZoomType.MousePosition)>
    Public Property ZoomMode() As ZoomType
        Get
            Return _ZoomMode
        End Get
        Set(ByVal value As ZoomType)
            _ZoomMode = value
        End Set
    End Property
     <Browsable(False)>
    Public Property ZoomFactor() As Double
        Get
            Return _ZoomFactor
        End Get
        Set(ByVal value As Double)
            _ZoomFactor = ValidateZoomFactor(value)
            Me.Invalidate(_ImageBounds)
            _ImageBounds = GetZoomedBounds()
            Me.Invalidate(_ImageBounds)
        End Set
    End Property
     <Browsable(False)>
    Public Property ImagePosition() As Point
        Get
            Return _ImageBounds.Location
        End Get
        Set(ByVal value As Point)
            _ImageBounds.Location = value
        End Set
    End Property
 #End Region
 #Region "Event overrides"
     Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        If EnableMouseDragging AndAlso e.Button = Windows.Forms.MouseButtons.Left Then
            _startDrag = e.Location
            _dragging = True
        End If
        MyBase.OnMouseDown(e)
    End Sub
     Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        If _dragging Then
            Me.Invalidate(_ImageBounds)
            _ImageBounds.X += e.X - _startDrag.X
            _ImageBounds.Y += e.Y - _startDrag.Y
            _startDrag = e.Location
            Me.Invalidate(_ImageBounds)
        End If
        MyBase.OnMouseMove(e)
    End Sub
     Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        If _dragging Then
            _dragging = False
            Me.Invalidate()
        End If
        MyBase.OnMouseUp(e)
    End Sub
      Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        Me.Select()
        MyBase.OnMouseEnter(e)
    End Sub
      Protected Overrides Sub OnMouseWheel(ByVal e As System.Windows.Forms.MouseEventArgs)
        If EnableMouseWheelZooming AndAlso
        Me.Bounds.Contains(Me.PointToClient(MousePosition)) Then
            Dim zoom As Double = _ZoomFactor
            zoom *= 1 + e.Delta / MouseWheelDivisor
            ZoomFactor = zoom
        End If
        MyBase.OnMouseWheel(e)
    End Sub
      Protected Overrides Sub OnPaint(ByVal pe As System.Windows.Forms.PaintEventArgs)
        If _ZoomFactor > 4 Then
            pe.Graphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.Half
            pe.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        Else
            pe.Graphics.InterpolationMode = Drawing2D.InterpolationMode.Default
        End If
        If _Image IsNot Nothing Then
            pe.Graphics.DrawImage(_Image, _ImageBounds)
        End If
        MyBase.OnPaint(pe)
    End Sub
     Protected Overrides Sub OnParentChanged(ByVal e As System.EventArgs)
        InitializeImage()
        MyBase.OnParentChanged(e)
    End Sub
 #End Region
 #Region "Private methods"
     Private Sub InitializeImage()
        If _Image IsNot Nothing Then
            ZoomFactor = FitImageToControl()
            _ImageBounds = CenterImageBounds()
        End If
        Me.Invalidate()
    End Sub
      Private Function ValidateZoomFactor(ByVal zoom As Double) As Double
        zoom = Math.Min(zoom, MaximumZoomFactor)
        If _Image IsNot Nothing Then
            If CInt(_Image.Width * zoom) < MinimumImageWidth Then
                zoom = MinimumImageWidth / _Image.Width
            End If
            If CInt(_Image.Height * zoom) < MinimumImageHeight Then
                zoom = MinimumImageHeight / _Image.Height
            End If
        End If
        Return zoom
    End Function
     Public Sub RefreshLocation()
        InitializeImage()
    End Sub
      Private Function FitImageToControl() As Double
        If Me.ClientSize = Size.Empty Then Return 1
        Dim sourceAspect As Double = _Image.Width / _Image.Height
        Dim targetAspect As Double = Me.ClientSize.Width / Me.ClientSize.Height
        If sourceAspect > targetAspect Then
            Return Me.ClientSize.Width / _Image.Width
        Else
            Return Me.ClientSize.Height / _Image.Height
        End If
    End Function
      Private Function CenterImageBounds() As Rectangle
        Dim w As Integer = CInt(_Image.Width * _ZoomFactor)
        Dim h As Integer = CInt(_Image.Height * _ZoomFactor)
        Dim x As Integer = (Me.ClientSize.Width - w) \ 2
        Dim y As Integer = (Me.ClientSize.Height - h) \ 2
        Return New Rectangle(x, y, w, h)
    End Function
      Private Function GetZoomedBounds() As Rectangle
        Try
             If _Image Is Nothing Then Return Rectangle.Empty
             If _ImageBounds = Rectangle.Empty Then
                _ImageBounds = CenterImageBounds()
            End If
              Dim imageCenter As Point = FindZoomCenter(_ZoomMode)
              Dim previousZoomFactor As Double = _ImageBounds.Width / _Image.Width
            Dim zoomRatio As Double = _ZoomFactor / previousZoomFactor
            _ImageBounds.Width = CInt(_ImageBounds.Width * zoomRatio)
            _ImageBounds.Height = CInt(_ImageBounds.Height * zoomRatio)
              Dim newPRelative As Point
            newPRelative.X = CInt(imageCenter.X * zoomRatio)
            newPRelative.Y = CInt(imageCenter.Y * zoomRatio)
              _ImageBounds.X += imageCenter.X - newPRelative.X
            _ImageBounds.Y += imageCenter.Y - newPRelative.Y
             Return _ImageBounds
        Catch ex As Exception
         End Try
    End Function
      Private Function FindZoomCenter(ByVal type As ZoomType) As Point
         Dim p As Point
        Select Case type
            Case ZoomType.ControlCenter
                p.X = Me.Width \ 2 - _ImageBounds.X
                p.Y = Me.Height \ 2 - _ImageBounds.Y
            Case ZoomType.ImageCenter
                p.X = _ImageBounds.Width \ 2
                p.Y = _ImageBounds.Height \ 2
            Case ZoomType.MousePosition
                Dim mp As Point = Me.PointToClient(MousePosition)
                p.X = mp.X - _ImageBounds.X
                p.Y = mp.Y - _ImageBounds.Y
            Case Else
                p = Point.Empty
        End Select
        Return p
    End Function
 #End Region
 End Class
