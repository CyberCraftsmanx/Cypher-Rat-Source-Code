Public Class ThemeToolTip
#Region " ToolTip "
    Inherits Control
    Public WithEvents _ToolTip As ToolTip
    Sub New()
        _ToolTip = New ToolTip
        With _ToolTip
            _ToolTip.Active = False
            _ToolTip.UseAnimation = True
            _ToolTip.UseFading = True
            _ToolTip.AutomaticDelay = 1000
            _ToolTip.AutoPopDelay = 3 * 60 * 100
            _ToolTip.OwnerDraw = True
            _ToolTip.BackColor = SpySettings.DefaultColor_Background
        End With
        AddHandler _ToolTip.Popup, AddressOf _ToolTip1_Popup
        AddHandler _ToolTip.Draw, AddressOf ToolTip1_Draw
    End Sub
    Private Sub _ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles _ToolTip.Popup
        Dim toolSize As Size = TextRenderer.MeasureText(_ToolTip.GetToolTip(e.AssociatedWindow), reso.f)
        toolSize.Width += 16
        toolSize.Height += 10
        e.ToolTipSize = toolSize
    End Sub
    Private Sub ToolTip1_Draw(sender As Object, e As DrawToolTipEventArgs) Handles _ToolTip.Draw
        e.DrawBackground()
        Dim r As New Rectangle(0, 0, e.Bounds.Width, e.Bounds.Height)
        ControlPaint.DrawBorder(e.Graphics, r, SpySettings.DefaultColor_Foreground, ButtonBorderStyle.Solid)
        Using sf As New StringFormat()
            sf.Alignment = StringAlignment.Near
            sf.LineAlignment = StringAlignment.Center
            Dim rect As New Rectangle(0, 0, e.Bounds.Width, e.Bounds.Height)
            e.Graphics.MeasureString(e.ToolTipText, reso.f)
            e.Graphics.DrawString(e.ToolTipText, reso.f, New SolidBrush(SpySettings.DefaultColor_Foreground), rect, sf)
        End Using
    End Sub
#End Region
End Class
