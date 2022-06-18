Imports System.Runtime.InteropServices
 Public Class ShellTerminal
    Public Client As Net.Sockets.TcpClient
     Public classClient As sockets.Client
     Public Title As String = "null"
     Public Color_Default As Color
     Private rect As Rectangle = Nothing
     Private hidCareat As Integer = -1
     Private CareatSize As New Size(6, 12)
     Private TAGx As String = "SHEEL:~$"
     Public ignore As Boolean = False
     Public IAMNew As Boolean = True
     Private Sub SpyStyle()
         For Each gr As RichTextBox In Me.Controls.OfType(Of RichTextBox)()
            gr.ForeColor = SpySettings.DefaultColor_Foreground
            gr.BackColor = SpySettings.DefaultColor_Background
        Next
     End Sub
    Private Sub TOpacity_Tick(sender As Object, e As EventArgs) Handles TOpacity.Tick
        If Not Me.Opacity = 1 Then
            Me.Opacity = Me.Opacity + 0.1
        Else
            Me.TOpacity.Enabled = False
        End If
          End Sub
    Private Sub ShellTerminal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
         Me.Icon = New Icon(reso.res_Path & "\Icons\win\15.ico")
         OutText.Font = reso.f
         Dim xsize As Size = TextRenderer.MeasureText("S", reso.f)
         CareatSize = New Size(CInt(xsize.Width / 2), xsize.Height)
         SpyStyle()
         Me.Text = Title
         Me.ctxMenu.Renderer = New ThemeToolStrip
         OutText.ContextMenuStrip = Me.ctxMenu
         Color_Default = OutText.ForeColor
         OutText.TabStop = False
         Me.TOpacity.Interval = SpySettings.T_Interval
         Me.TOpacity.Enabled = True
         OutText.Focus()
        OutText.Select()
     End Sub
     Private Sub CaretShell(ByVal alva As Integer)
         Dim ch As String = Me.OutText.Text
         If ch.Length > 0 Then
             If Not rect = Nothing Then
                 OutText.Invalidate(rect)
             End If
             OutText.Update()
             Dim gr As Graphics = Me.OutText.CreateGraphics()
             Dim pi As Point = Me.OutText.GetPositionFromCharIndex(Me.OutText.SelectionStart)
             rect = New Rectangle(pi.X, pi.Y, CareatSize.Width, CareatSize.Height)
             Dim a As Integer = Color_Default.A
             If Not Me.OutText.SelectionStart = ch.Length Then
                 a = alva
             End If
             gr.FillRectangle(New Pen(Color.FromArgb(a, Color_Default.R, Color_Default.G, Color_Default.B), CareatSize.Width).Brush, rect)
             gr.Dispose()
         End If
     End Sub
    Private Sub Caret_Tick(sender As Object, e As EventArgs) Handles Caret.Tick
         If Not ignore Then
             If hidCareat = -1 Then
                 If Not rect = Nothing Then
                     OutText.Invalidate(rect)
                 End If
                 hidCareat = 1
             Else
                 CaretShell(128)
                 hidCareat = -1
             End If
         End If
     End Sub
     Private Sub OutText_TextChanged(sender As Object, e As EventArgs) Handles OutText.TextChanged
         If Not IAMNew Then
             If OutText.Text.Length = 0 Then
                 NewTag(False)
                 Return
             End If
         End If
         If Not ignore Then
             RestCaret()
         End If
     End Sub
    Private Sub OutText_MouseClick(sender As Object, e As MouseEventArgs) Handles OutText.MouseClick
         If Not ignore Then
             RestCaret()
         End If
     End Sub
     Private Sub OutText_KeyDown(sender As Object, e As KeyEventArgs) Handles OutText.KeyDown
         If e.KeyData = Keys.Up Or e.KeyData = Keys.Down Or e.KeyData = Keys.Right Or e.KeyData = Keys.Left Then
            e.SuppressKeyPress = True
            Return
        End If
         If Control.ModifierKeys = Keys.Shift Or Control.ModifierKeys = Keys.Control Then
            e.SuppressKeyPress = True
            Return
        End If
         If OutText.ReadOnly = True Then
             Return
         End If
         If Not ignore Then
             If e.KeyData = Keys.Enter Then
                 e.SuppressKeyPress = True
                 If OutText.Lines.Length > 0 Then
                     Dim line As String = OutText.Lines(OutText.Lines.Length - 1)
                     If line.Contains(TAGx) Then
                         line = line.Substring(TAGx.Length)
                     Else
                         NewTag(True)
                         Return
                     End If
                     If line.Length = 0 Then
                         Return
                     End If
                     If Not classClient Is Nothing Then
                         Dim objs As Object() = {Client, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".terminal" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.getCommand & sockets.Data.SPL_SOCKET & "command" & sockets.Data.SPL_DATA & line, Codes.Encoding().GetBytes("null"), classClient}
                         OutText.ReadOnly = True
                         ignore = True
                         If Not rect = Nothing Then
                             OutText.Invalidate(rect)
                         End If
                         classClient.SendAsync(objs)
                     End If
                 End If
             End If
             RestCaret()
         End If
     End Sub
    Public Sub NewTag(ByVal isLine As Boolean)
        OutText.DeselectAll()
        If isLine Then
            With OutText
                 .AppendText(vbNewLine & TAGx)
             End With
        Else
            With OutText
                 .AppendText(TAGx)
             End With
        End If
         OutText.SelectionStart = OutText.Text.Length
    End Sub
    Private Sub RestCaret()
         If Not ignore Then
            CaretShell(128)
            Caret.Enabled = False
            Caret.Enabled = True
        End If
    End Sub
     Private Sub OutText_HScroll(sender As Object, e As EventArgs) Handles OutText.HScroll
         OutText.Invalidate()
        CaretShell(255)
     End Sub
     Private Sub OutText_VScroll(sender As Object, e As EventArgs) Handles OutText.VScroll
        OutText.Invalidate()
        CaretShell(255)
    End Sub
     Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        If OutText.ReadOnly = False Then
            OutText.Paste()
        End If
    End Sub
     Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        OutText.Copy()
    End Sub
 End Class