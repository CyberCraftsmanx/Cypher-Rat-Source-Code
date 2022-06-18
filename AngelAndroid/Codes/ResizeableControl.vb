Public Class ResizeableControl
     Private WithEvents mControl As Control
    Private mMouseDown As Boolean = False
    Private mEdge As EdgeEnum = EdgeEnum.None
    Private mWidth As Integer = 4
    Private mOutlineDrawn As Boolean = False
    Private UseTop As Boolean
    Private Useright As Boolean
    Private Useleft As Boolean
    Private Usebuttom As Boolean
    Private UseCorner As Boolean
     Private Enum EdgeEnum
        None
        Right
        Left
        Top
        Bottom
        TopLeft
    End Enum
     Public Sub New(ByVal Control As Control, ByVal IsUp As Boolean, ByVal Isright As Boolean, ByVal isleft As Boolean, ByVal isbuttom As Boolean, ByVal iscornere As Boolean)
        mControl = Control
        UseTop = IsUp
        Useright = Isright
        Useleft = isleft
        Usebuttom = isbuttom
        UseCorner = iscornere
    End Sub
     Private Sub mControl_MouseDown(ByVal sender As Object,
        ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles mControl.MouseDown
         If e.Button = Windows.Forms.MouseButtons.Left Then
            mMouseDown = True
        End If
    End Sub
     Private Sub mControl_MouseUp(ByVal sender As Object,
        ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles mControl.MouseUp
         mMouseDown = False
    End Sub
     Private Sub mControl_MouseMove(ByVal sender As Object,
    ByVal e As System.Windows.Forms.MouseEventArgs) _
    Handles mControl.MouseMove
         Dim c As Control = CType(sender, Control)
        Dim g As Graphics = c.CreateGraphics
        Select Case mEdge
            Case EdgeEnum.TopLeft
                g.FillRectangle(Brushes.Black,
            0, 0, mWidth * 4, mWidth * 4)
                mOutlineDrawn = True
            Case EdgeEnum.Left
                g.FillRectangle(Brushes.Black,
            0, 0, mWidth, c.Height)
                mOutlineDrawn = True
            Case EdgeEnum.Right
                g.FillRectangle(Brushes.Black,
            c.Width - mWidth, 0, c.Width, c.Height)
                mOutlineDrawn = True
            Case EdgeEnum.Top
                g.FillRectangle(Brushes.Black,
            0, 0, c.Width, mWidth)
                mOutlineDrawn = True
            Case EdgeEnum.Bottom
                g.FillRectangle(Brushes.Black,
            0, c.Height - mWidth, c.Width, mWidth)
                mOutlineDrawn = True
            Case EdgeEnum.None
                If mOutlineDrawn Then
                    c.Refresh()
                    mOutlineDrawn = False
                End If
        End Select
         If mMouseDown And mEdge <> EdgeEnum.None Then
            c.SuspendLayout()
            Select Case mEdge
                Case EdgeEnum.TopLeft
                    c.SetBounds(c.Left + e.X, c.Top + e.Y,
            c.Width, c.Height)
                Case EdgeEnum.Left
                    c.SetBounds(c.Left + e.X, c.Top,
            c.Width - e.X, c.Height)
                Case EdgeEnum.Right
                    c.SetBounds(c.Left, c.Top,
            c.Width - (c.Width - e.X), c.Height)
                Case EdgeEnum.Top
                    c.SetBounds(c.Left, c.Top + e.Y,
            c.Width, c.Height - e.Y)
                Case EdgeEnum.Bottom
                    c.SetBounds(c.Left, c.Top,
            c.Width, c.Height - (c.Height - e.Y))
            End Select
            c.ResumeLayout()
        Else
            Select Case True
                Case e.X <= (mWidth * 4) And
            e.Y <= (mWidth * 4)
                    If UseCorner Then
                        c.Cursor = Cursors.SizeAll
                        mEdge = EdgeEnum.TopLeft
                    End If
                Case e.X <= mWidth
                    If Useleft Then
                        c.Cursor = Cursors.VSplit
                        mEdge = EdgeEnum.Left
                    End If
                Case e.X > c.Width - (mWidth + 1)
                    If Useright Then
                        c.Cursor = Cursors.VSplit
                        mEdge = EdgeEnum.Right
                    End If
                Case e.Y <= mWidth
                    If UseTop Then
                        c.Cursor = Cursors.HSplit
                        mEdge = EdgeEnum.Top
                    End If
                Case e.Y > c.Height - (mWidth + 1)
                    If Usebuttom Then
                        c.Cursor = Cursors.HSplit
                        mEdge = EdgeEnum.Bottom
                    End If
                Case Else
                    c.Cursor = Cursors.Default
                    mEdge = EdgeEnum.None
            End Select
        End If
    End Sub
     Private Sub mControl_MouseLeave(ByVal sender As Object,
        ByVal e As System.EventArgs) _
        Handles mControl.MouseLeave
         Dim c As Control = CType(sender, Control)
        mEdge = EdgeEnum.None
        c.Refresh()
    End Sub
 End Class