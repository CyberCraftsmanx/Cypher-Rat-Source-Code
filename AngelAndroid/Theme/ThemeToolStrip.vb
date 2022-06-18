'------------------------------------
'---------Cypher Rat By EVLF
'-----------------------------------
'---------t.me/evlf
'------------------------------------
#Region " ThemeThemeToolStrip "
Public Class ThemeToolStrip
     Inherits System.Windows.Forms.ToolStripSystemRenderer
     Public Sub DrawRoundedRectangle(ByVal objGraphics As Graphics,
                                ByVal m_intxAxis As Integer,
                                ByVal m_intyAxis As Integer,
                                ByVal m_intWidth As Integer,
                                ByVal m_intHeight As Integer,
                                ByVal m_diameter As Integer, ByVal color As Color)
        Try
            Dim pen As New Pen(color)
            Dim BaseRect As New RectangleF(m_intxAxis, m_intyAxis, m_intWidth, m_intHeight)
            Dim ArcRect As New RectangleF(BaseRect.Location, New SizeF(m_diameter, m_diameter))
            objGraphics.DrawArc(pen, ArcRect, 180, 90)
            objGraphics.DrawLine(pen, m_intxAxis + CInt(m_diameter / 2), m_intyAxis, m_intxAxis + m_intWidth - CInt(m_diameter / 2), m_intyAxis)
            ArcRect.X = BaseRect.Right - m_diameter
            objGraphics.DrawArc(pen, ArcRect, 270, 90)
            objGraphics.DrawLine(pen, m_intxAxis + m_intWidth, m_intyAxis + CInt(m_diameter / 2), m_intxAxis + m_intWidth, m_intyAxis + m_intHeight - CInt(m_diameter / 2))
            ArcRect.Y = BaseRect.Bottom - m_diameter
            objGraphics.DrawArc(pen, ArcRect, 0, 90)
            objGraphics.DrawLine(pen, m_intxAxis + CInt(m_diameter / 2), m_intyAxis + m_intHeight, m_intxAxis + m_intWidth - CInt(m_diameter / 2), m_intyAxis + m_intHeight)
            ArcRect.X = BaseRect.Left
            objGraphics.DrawArc(pen, ArcRect, 90, 90)
            objGraphics.DrawLine(pen, m_intxAxis, m_intyAxis + CInt(m_diameter / 2), m_intxAxis, m_intyAxis + m_intHeight - CInt(m_diameter / 2))
        Catch Exception As Exception
         End Try
    End Sub
    Protected Overrides Sub Initialize(ByVal toolStrip As System.Windows.Forms.ToolStrip)
        Try
            MyBase.Initialize(toolStrip)
            If TypeOf toolStrip Is MenuStrip Then
                toolStrip.ForeColor = ForColor
                toolStrip.BackColor = backColorStrip
             ElseIf TypeOf toolStrip Is ToolStripDropDownMenu Then
                toolStrip.ForeColor = ForColor
                toolStrip.BackColor = backColor
             ElseIf TypeOf toolStrip Is ToolStrip Then
                toolStrip.ForeColor = ForColor
                toolStrip.BackColor = backColor
             End If
            toolStrip.Font = MyFont
            toolStrip.Padding = New Padding(5, 2, 5, 2)
         Catch Exception As Exception
        End Try
    End Sub
     Protected Overrides Sub OnRenderToolStripBorder(ByVal e As System.Windows.Forms.ToolStripRenderEventArgs)
        If TypeOf e.ToolStrip Is ToolStripDropDownMenu Then
            Dim Pen1 As New Drawing.Pen(ColorBorder, 1)
            e.Graphics.DrawRectangle(Pen1, 0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1)
        End If
    End Sub
     Public Function RECOLOR(image As Image, baseColor As Color, newColor As Color) As Bitmap
        Dim transformation As New Imaging.ColorMatrix(New Single()() {
        New Single() {1, 0, 0, 0, 0},
        New Single() {0, 1, 0, 0, 0},
        New Single() {0, 0, 1, 0, 0},
        New Single() {0, 0, 0, 1, 0},
        New Single() {(CInt(newColor.R) - CInt(baseColor.R)) / 255.0!, (CInt(newColor.G) - CInt(baseColor.G)) / 255.0!, (CInt(newColor.B) - CInt(baseColor.B)) / 255.0!, 0, 1}
    })
         Dim imageAttributes As New Imaging.ImageAttributes()
        imageAttributes.SetColorMatrix(transformation)
         Dim result As New Bitmap(image.Width, image.Height)
         Using g As Graphics = Graphics.FromImage(result)
            g.DrawImage(image, New Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes)
        End Using
         Return result
    End Function
    Private colore As Boolean = False
    Protected Overrides Sub OnRenderMenuItemBackground(ByVal e As System.Windows.Forms.ToolStripItemRenderEventArgs)
         If colore = False Then
            For Each d In e.ToolStrip.Items
                If d.Image IsNot Nothing Then
                    Dim b As Image = d.Image
                    d.Image = RECOLOR(b, Color.FromArgb(190, 190, 190), SpySettings.DefaultColor_Foreground)
                End If
            Next
            colore = True
        End If
        Try
            If TypeOf e.ToolStrip Is ToolStripDropDownMenu Then
                 If e.Item.Selected Then
                     If e.Item.Enabled = True Then
                         Dim obj As Array = e.Item.Tag
                         If e.Item.Image IsNot Nothing Then
                             If CInt(obj(0)) = 0 Then
                                obj(0) = 1
                                If obj(2).ToString = "null" Then
                                    Dim b As Image = e.Item.Image
                                    obj(2) = RECOLOR(b, SpySettings.DefaultColor_Foreground, SpySettings.DefaultColor_Background)
                                    e.Item.Image = DirectCast(obj(2), Image)
                                Else
                                    e.Item.Image = DirectCast(obj(2), Image)
                                End If
                                e.Item.Tag = obj
                            End If
                        End If
                         Dim R0 As New Rectangle(1, 0, e.Item.Width - 1, e.Item.Height)
                        Dim d02D As New Drawing2D.LinearGradientBrush(R0, selected_Color, selected_Color, Drawing2D.LinearGradientMode.Vertical)
                        e.Graphics.FillRectangle(d02D, R0)
                        DrawRoundedRectangle(e.Graphics, R0.Left, R0.Top, R0.Width - 1, R0.Height - 1, 1, clrSelectedBorder)
                        e.Item.ForeColor = selected_Color_ForColor
                     End If
                 Else
                     Dim obj As Array = e.Item.Tag
                     If e.Item.Image IsNot Nothing Then
                         If CInt(obj(0)) = 1 Then
                            obj(0) = 0
                            If obj(1).ToString = "null" Then
                                Dim b As Image = e.Item.Image
                                obj(1) = RECOLOR(b, SpySettings.DefaultColor_Background, SpySettings.DefaultColor_Foreground)
                                e.Item.Image = DirectCast(obj(1), Image)
                            Else
                                e.Item.Image = DirectCast(obj(1), Image)
                            End If
                            e.Item.Tag = obj
                        End If
                    End If
                     e.Item.ForeColor = ForColor
                 End If
             ElseIf (TypeOf e.ToolStrip Is MenuStrip) Then
                If e.Item.IsOnDropDown = False AndAlso e.Item.Selected Then
                     If e.Item.Enabled = True Then
                        If e.Item.Pressed Then
                            Dim R0 As New Rectangle(0, 0, e.Item.Width - 1, e.Item.Height)
                            Dim d02D As New Drawing2D.LinearGradientBrush(R0, backColor, backColor, Drawing2D.LinearGradientMode.Vertical)
                            e.Graphics.FillRectangle(d02D, R0)
                            DrawRoundedRectangle(e.Graphics, R0.Left, R0.Top, R0.Width - 1, R0.Height - 1, 1, ColorBorder)
                         Else
                            Dim R0 As New Rectangle(0, 0, e.Item.Width - 1, e.Item.Height)
                            Dim d02D As New Drawing2D.LinearGradientBrush(R0, selected_Color, selected_Color, Drawing2D.LinearGradientMode.Vertical)
                            e.Graphics.FillRectangle(d02D, R0)
                            DrawRoundedRectangle(e.Graphics, R0.Left, R0.Top, R0.Width - 1, R0.Height - 1, 1, clrSelectedBorder)
                        End If
                     End If
                    e.Item.ForeColor = selected_Color_ForColor
                Else
                     If e.Item.Enabled = True Then
                        If e.Item.Pressed Then
                            Dim R0 As New Rectangle(0, 0, e.Item.Width - 1, e.Item.Height)
                            Dim d02D As New Drawing2D.LinearGradientBrush(R0, backColor, backColor, Drawing2D.LinearGradientMode.Vertical)
                            e.Graphics.FillRectangle(d02D, R0)
                            DrawRoundedRectangle(e.Graphics, R0.Left, R0.Top, R0.Width - 1, R0.Height - 1, 1, ColorBorder)
                        End If
                     End If
                     e.Item.ForeColor = ForColor
                 End If
            End If
         Catch Exception As Exception
         End Try
     End Sub
    Protected Overrides Sub OnRenderArrow(ByVal e As System.Windows.Forms.ToolStripArrowRenderEventArgs)
        Try
            If e.Item.Selected Then
                If e.Item.Enabled = False Then
                    e.ArrowColor = ENBArrowselectedColor
                Else
                    e.ArrowColor = ArrowselectedColor
                End If
             Else
                If e.Item.Enabled = False Then
                    e.ArrowColor = ENBArrowselectedColor
                Else
                    e.ArrowColor = ArrowColor
                End If
             End If
            MyBase.OnRenderArrow(e)
        Catch Exception As Exception
         End Try
    End Sub
    Public Property UseDefaultSeperatorStyle As Boolean = False
     Protected Overrides Sub OnRenderSeparator(ByVal e As System.Windows.Forms.ToolStripSeparatorRenderEventArgs)
        If e.Vertical OrElse TryCast(e.Item, ToolStripSeparator) Is Nothing OrElse Me.UseDefaultSeperatorStyle Then
            MyBase.OnRenderSeparator(e)
        Else
            Dim tsBounds As Rectangle = New Rectangle(Point.Empty, e.Item.Size)
            Dim LineY As Integer = tsBounds.Bottom - (tsBounds.Height / 2) - 1
            Dim LineLeft As Integer = tsBounds.Left + 30
            Dim LineRight As Integer = tsBounds.Right - 10
            e.Graphics.DrawLine(New Pen(New SolidBrush(ColorLines0)), LineLeft, LineY, LineRight, LineY)
            e.Graphics.DrawLine(New Pen(New SolidBrush(ColorLines1)), LineLeft, LineY + 1, LineRight, LineY + 1)
         End If
    End Sub
     Protected Overrides Sub OnRenderImageMargin(ByVal e As System.Windows.Forms.ToolStripRenderEventArgs)
        Try
             MyBase.OnRenderImageMargin(e)
             Dim clrMenuBorder As Color = selected_Color
            Dim Line0 As New Drawing.SolidBrush(ColorLines0)
            Dim Line2 As New Drawing.SolidBrush(ColorLines1)
            Dim R1 As New Rectangle(e.AffectedBounds.Width + 0, 2, 1, e.AffectedBounds.Height)
            Dim R2 As New Rectangle(e.AffectedBounds.Width + 1, 2, 1, e.AffectedBounds.Height)
            Dim borderPen As New Pen(clrMenuBorder)
            e.Graphics.FillRectangle(Line0, R1)
            e.Graphics.FillRectangle(Line2, R2)
            e.ToolStrip.BackColor = backColor
        Catch Exception As Exception
         End Try
    End Sub
#Region "- Color Table -"
    Public selected_Color As Color = SpySettings.DefaultColor_Foreground
    Public selected_Color_ForColor As Color = SpySettings.DefaultColor_Background
    Public backColor As Color = SpySettings.DefaultColor_Background
    Public backColorStrip As Color = Color.FromArgb(45, 45, 48)
    Public ForColor As Color = SpySettings.DefaultColor_Foreground
    Public clrSelectedBorder As Color = SpySettings.DefaultColor_Foreground
    Public clrSelectedBorderCheck As Color = Color.FromArgb(10, 10, 10)
    Public ColorLines0 As Color = SpySettings.DefaultColor_Background
    Public ColorLines1 As Color = SpySettings.DefaultColor_Background
    Public backColorChecked0 As Color = Color.FromArgb(101, 101, 101)
    Public backColorChecked1 As Color = Color.FromArgb(123, 123, 123)
    Public ArrowColor As Color = SpySettings.DefaultColor_Foreground
    Public ArrowselectedColor As Color = SpySettings.DefaultColor_Background
    Public ENBArrowselectedColor As Color = Color.FromArgb(70, 70, 70)
    Public ColorBorder As Color = SpySettings.DefaultColor_Foreground
 #End Region
#Region "- Font Table -"
    Public MyFont As Font = reso.f
#End Region
 End Class
#End Region
 