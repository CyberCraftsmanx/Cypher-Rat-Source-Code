Public Class Settings
     Sub New()
         InitializeComponent()
         Me.Font = reso.f
     End Sub
    Private Sub TOpacity_Tick(sender As Object, e As EventArgs) Handles TOpacity.Tick
        If Not Me.Opacity = 1 Then
            Me.Opacity = Me.Opacity + 0.1
        Else
            Me.TOpacity.Enabled = False
        End If
    End Sub
     Private Sub SpyStyle()
         For Each gr As DataGridView In Panel1.Controls.OfType(Of DataGridView)()
            gr.BackgroundColor = SpySettings.DefaultColor_Background
            gr.BackColor = SpySettings.DefaultColor_Background
            gr.ColumnHeadersDefaultCellStyle.BackColor = SpySettings.DefaultColor_Background
            gr.DefaultCellStyle.BackColor = SpySettings.DefaultColor_Background
            gr.DefaultCellStyle.SelectionForeColor = SpySettings.DefaultColor_Background
              gr.DefaultCellStyle.ForeColor = SpySettings.DefaultColor_Foreground
            gr.DefaultCellStyle.SelectionBackColor = SpySettings.DefaultColor_Foreground
            gr.ColumnHeadersDefaultCellStyle.ForeColor = SpySettings.DefaultColor_Foreground
            gr.CellBorderStyle = DataGridViewCellBorderStyle.Single
            gr.GridColor = SpySettings.DefaultColor_Foreground
             gr.BorderStyle = System.Windows.Forms.BorderStyle.None
            gr.ColumnHeadersVisible = False
            gr.EnableHeadersVisualStyles = False
            gr.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
            gr.RowHeadersVisible = False
             gr.SelectionMode = DataGridViewSelectionMode.CellSelect
            gr.MultiSelect = False
         Next
        For Each gr As Label In Panel1.Controls.OfType(Of Label)()
            gr.BackColor = SpySettings.DefaultColor_Background
            gr.ForeColor = SpySettings.DefaultColor_ColorTitles
        Next
         For Each gr As Button In Panel2.Controls.OfType(Of Button)()
            gr.BackColor = SpySettings.DefaultColor_Foreground
            gr.ForeColor = SpySettings.DefaultColor_Background
        Next
        For Each gr As Panel In Me.Controls.OfType(Of Panel)()
            gr.BackColor = SpySettings.DefaultColor_Background
            gr.ForeColor = SpySettings.DefaultColor_Foreground
        Next
    End Sub
    Private Sub grreSize()
        For Each gr As DataGridView In Panel1.Controls.OfType(Of DataGridView)()
            Dim reSize As Integer = gr.Rows.Count * gr.Rows(0).Height
            gr.Height = reSize + 5
        Next
    End Sub
    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
         Me.Icon = My.Resources.max
        SpyStyle()
         R()
        grreSize()
         Me.ctxPacket.Renderer = New ThemeToolStrip
        DGV5.ContextMenuStrip = Me.ctxPacket
        Me.TOpacity.Interval = SpySettings.T_Interval
        Me.TOpacity.Enabled = True
    End Sub
     Private Sub R()
         Dim ListItems As New List(Of String)
         Dim rowID As Integer = DGV0.Rows.Add("Performance", Nothing)
        Dim cell0 As DataGridViewComboBoxCell = DGV0.Rows(rowID).Cells(1)
        ListItems.Add("High")
        ListItems.Add("Normal")
        ListItems.Add("Low")
        cell0.DataSource = ListItems
        cell0.Value = ListItems(ListItems.IndexOf(My.Settings.performance))
         ListItems = New List(Of String)
         rowID = DGV0.Rows.Add("Encoding", Nothing)
        Dim cell1 As DataGridViewComboBoxCell = DGV0.Rows(rowID).Cells(1)
        ListItems.Add("Default")
        ListItems.Add("UTF8")
        ListItems.Add("UTF32")
        ListItems.Add("ASCII")
        cell1.DataSource = ListItems
        cell1.Value = ListItems(ListItems.IndexOf(My.Settings.encoding8))
         ListItems = New List(Of String)
         rowID = DGV0.Rows.Add("Disconnected", Nothing)
        Dim cell2 As DataGridViewComboBoxCell = DGV0.Rows(rowID).Cells(1)
        ListItems.Add("Close windows")
        ListItems.Add("Just tell me")
        cell2.DataSource = ListItems
        cell2.Value = ListItems(ListItems.IndexOf(My.Settings.disconnected))
         ListItems = New List(Of String)
        rowID = DGV0.Rows.Add("Removing Duplicates", Nothing)
        Dim cellRD As DataGridViewComboBoxCell = DGV0.Rows(rowID).Cells(1)
        ListItems.Add("Yes")
        ListItems.Add("No")
        cellRD.DataSource = ListItems
        cellRD.Value = ListItems(ListItems.IndexOf(My.Settings.Removing_Duplicates))
         ListItems = New List(Of String)
         rowID = DGV1.Rows.Add("Show Alert", Nothing)
        Dim cell3 As DataGridViewComboBoxCell = DGV1.Rows(rowID).Cells(1)
        ListItems.Add("Yes")
        ListItems.Add("No")
        cell3.DataSource = ListItems
        cell3.Value = ListItems(ListItems.IndexOf(My.Settings.show_alert))
         ListItems = New List(Of String)
         rowID = DGV1.Rows.Add("Location", Nothing)
        Dim cell4 As DataGridViewComboBoxCell = DGV1.Rows(rowID).Cells(1)
        ListItems.Add("Right")
        ListItems.Add("Left")
        cell4.DataSource = ListItems
        cell4.Value = ListItems(ListItems.IndexOf(My.Settings.location))
         ListItems = New List(Of String)
        rowID = DGV1.Rows.Add("Play Sound", Nothing)
        Dim cellSound As DataGridViewComboBoxCell = DGV1.Rows(rowID).Cells(1)
        ListItems.Add("Yes")
        ListItems.Add("No")
        cellSound.DataSource = ListItems
        cellSound.Value = ListItems(ListItems.IndexOf(If(My.Settings.NOTI_SOUND = True, "Yes", "No")))
         ListItems = New List(Of String)
        rowID = DGV1.Rows.Add("Multi-Sounds", Nothing)
        Dim cellmulti As DataGridViewComboBoxCell = DGV1.Rows(rowID).Cells(1)
        ListItems.Add("Yes")
        ListItems.Add("No")
        cellmulti.DataSource = ListItems
        cellmulti.Value = ListItems(ListItems.IndexOf(My.Settings._multi_sounds))
         ListItems = New List(Of String)
        rowID = DGV1.Rows.Add("Round", Nothing)
        Dim cellRound As DataGridViewComboBoxCell = DGV1.Rows(rowID).Cells(1)
        ListItems.Add("Yes")
        ListItems.Add("No")
        cellRound.DataSource = ListItems
        cellRound.Value = ListItems(ListItems.IndexOf(My.Settings.Round))
         ListItems = New List(Of String)
         rowID = DGV2.Rows.Add("Auto focus", Nothing)
        Dim cell5 As DataGridViewComboBoxCell = DGV2.Rows(rowID).Cells(1)
        ListItems.Add("Yes")
        ListItems.Add("No")
        cell5.DataSource = ListItems
        cell5.Value = ListItems(ListItems.IndexOf(My.Settings.Auto_focus))
         ListItems = New List(Of String)
         rowID = DGV2.Rows.Add("Effects", Nothing)
        Dim cell6 As DataGridViewComboBoxCell = DGV2.Rows(rowID).Cells(1)
        ListItems.Add("Normal")
        ListItems.Add("Gray")
        ListItems.Add("Raw-01")
        ListItems.Add("Raw-02")
        cell6.DataSource = ListItems
        cell6.Value = ListItems(ListItems.IndexOf(My.Settings.Effects_CAM))
         ListItems = New List(Of String)
         rowID = DGV2.Rows.Add("Quality", Nothing)
        Dim cell7 As DataGridViewComboBoxCell = DGV2.Rows(rowID).Cells(1)
        ListItems.Add("Auto")
        ListItems.Add("high quality")
        cell7.DataSource = ListItems
        cell7.Value = ListItems(ListItems.IndexOf(My.Settings.CAMQuality))
         ListItems = New List(Of String)
         rowID = DGV3.Rows.Add("Style", Nothing)
        Dim cell8 As DataGridViewComboBoxCell = DGV3.Rows(rowID).Cells(1)
        ListItems.Add("Navigation_Preview_Day")
        ListItems.Add("Dark")
        ListItems.Add("Basic_Template")
        ListItems.Add("Streets")
        ListItems.Add("Le_Shine")
        ListItems.Add("Ice_Cream")
        ListItems.Add("Navigation_Preview_Night")
        ListItems.Add("Moonlight")
        ListItems.Add("Decimal")
        cell8.DataSource = ListItems
        cell8.Value = ListItems(ListItems.IndexOf(My.Settings.Style_Maps))
         ListItems = New List(Of String)
         rowID = DGV4.Rows.Add("Auto save", Nothing)
        Dim cell9 As DataGridViewComboBoxCell = DGV4.Rows(rowID).Cells(1)
        ListItems.Add("Yes")
        ListItems.Add("No")
        cell9.DataSource = ListItems
        cell9.Value = ListItems(ListItems.IndexOf(My.Settings.Saving_data))
         Dim bit As New Bitmap(21, 17)
        Dim g As Graphics = Graphics.FromImage(bit)
        Dim clr As Color = My.Settings.DefaultColorForeground
        g.Clear(clr)
        Dim p As Pen = New Pen(ControlPaint.Light(My.Settings.DefaultColorForeground), 1)
        g.DrawRectangle(p, 0, 0, bit.Width - 1, bit.Height - 1)
        rowID = DGV5.Rows.Add("Foreground", bit)
        DGV5.Rows(rowID).Tag = clr
        g.Dispose()
         bit = New Bitmap(21, 17)
        g = Graphics.FromImage(bit)
        clr = My.Settings.DefaultColorBackground
        g.Clear(clr)
        p = New Pen(ControlPaint.Light(My.Settings.DefaultColorBackground), 1)
        g.DrawRectangle(p, 0, 0, bit.Width - 1, bit.Height - 1)
        rowID = DGV5.Rows.Add("Background", bit)
        DGV5.Rows(rowID).Tag = clr
        g.Dispose()
         bit = New Bitmap(21, 17)
        g = Graphics.FromImage(bit)
        clr = My.Settings.DefaultColor_ColorTitles
        g.Clear(clr)
        p = New Pen(ControlPaint.Light(My.Settings.DefaultColor_ColorTitles), 1)
        g.DrawRectangle(p, 0, 0, bit.Width - 1, bit.Height - 1)
        rowID = DGV5.Rows.Add("Titles", bit)
        DGV5.Rows(rowID).Tag = clr
        g.Dispose()
         bit = New Bitmap(21, 17)
        g = Graphics.FromImage(bit)
        clr = My.Settings.DefaultColor_NewColorFiles
        g.Clear(clr)
        p = New Pen(ControlPaint.Light(My.Settings.DefaultColor_NewColorFiles), 1)
        g.DrawRectangle(p, 0, 0, bit.Width - 1, bit.Height - 1)
        rowID = DGV5.Rows.Add("New Files", bit)
        DGV5.Rows(rowID).Tag = clr
        g.Dispose()
         ListItems = New List(Of String)
        rowID = DGV6.Rows.Add("Size", Nothing)
        Dim cell10 As DataGridViewComboBoxCell = DGV6.Rows(rowID).Cells(1)
        ListItems.Add("8")
        ListItems.Add("9")
        ListItems.Add("10")
        ListItems.Add("11")
        ListItems.Add("12")
        cell10.DataSource = ListItems
        cell10.Value = ListItems(ListItems.IndexOf(My.Settings.FontSize))
         ListItems = New List(Of String)
        rowID = DGV6.Rows.Add("Style", Nothing)
        Dim cell12 As DataGridViewComboBoxCell = DGV6.Rows(rowID).Cells(1)
        ListItems.Add("Bold")
        ListItems.Add("Regular")
        cell12.DataSource = ListItems
        cell12.Value = ListItems(ListItems.IndexOf(My.Settings.FontStyle))
         ListItems = New List(Of String)
        rowID = DGV7.Rows.Add("Visible", Nothing)
        Dim cell13 As DataGridViewComboBoxCell = DGV7.Rows(rowID).Cells(1)
        ListItems.Add("Yes")
        ListItems.Add("No")
        cell13.DataSource = ListItems
        cell13.Value = ListItems(ListItems.IndexOf(My.Settings.Flags_Visible))
         ListItems = New List(Of String)
        rowID = DGV7.Rows.Add("Size", Nothing)
        Dim cell14 As DataGridViewComboBoxCell = DGV7.Rows(rowID).Cells(1)
        ListItems.Add("16px")
        ListItems.Add("24px")
        ListItems.Add("32px")
        cell14.DataSource = ListItems
        cell14.Value = ListItems(ListItems.IndexOf(My.Settings.Flags_Size))
         ListItems = New List(Of String)
        rowID = DGV8.Rows.Add("Visible", Nothing)
        Dim cell15 As DataGridViewComboBoxCell = DGV8.Rows(rowID).Cells(1)
        ListItems.Add("Yes")
        ListItems.Add("No")
        cell15.DataSource = ListItems
        cell15.Value = ListItems(ListItems.IndexOf(My.Settings.SStatus_Visible))
         ListItems = New List(Of String)
        rowID = DGV9.Rows.Add("Icon files size", Nothing)
        Dim cell18 As DataGridViewComboBoxCell = DGV9.Rows(rowID).Cells(1)
        ListItems.Add("Small")
        ListItems.Add("Large")
        cell18.DataSource = ListItems
        cell18.Value = ListItems(ListItems.IndexOf(My.Settings.FM_IC_Size))
         DGV0.ClearSelection()
        DGV1.ClearSelection()
        DGV2.ClearSelection()
        DGV3.ClearSelection()
        DGV4.ClearSelection()
        DGV5.ClearSelection()
        DGV6.ClearSelection()
        DGV7.ClearSelection()
        DGV8.ClearSelection()
        DGV9.ClearSelection()
    End Sub
     Private Sub SV_Click(sender As Object, e As EventArgs) Handles SV.Click
        Dim Count As Integer = 0
        For Each row In DGV0.Rows
            Select Case Count
                Case 0
                    My.Settings.performance = row.Cells(1).Value
                Case 1
                    My.Settings.encoding8 = row.Cells(1).Value
                Case 2
                    My.Settings.disconnected = row.Cells(1).Value
                Case 3
                    My.Settings.Removing_Duplicates = row.Cells(1).Value
            End Select
            Count += 1
        Next
        Count = 0
        For Each row In DGV1.Rows
            Select Case Count
                Case 0
                    My.Settings.show_alert = row.Cells(1).Value
                Case 1
                    My.Settings.location = row.Cells(1).Value
                Case 2
                    My.Settings.NOTI_SOUND = If(row.Cells(1).Value = "Yes", True, False)
                Case 3
                    My.Settings._multi_sounds = row.Cells(1).Value
                Case 4
                    My.Settings.Round = row.Cells(1).Value
            End Select
            Count += 1
        Next
         Count = 0
        For Each row In DGV2.Rows
            Select Case Count
                Case 0
                    My.Settings.Auto_focus = row.Cells(1).Value
                Case 1
                    My.Settings.Effects_CAM = row.Cells(1).Value
                Case 2
                    My.Settings.CAMQuality = row.Cells(1).Value
            End Select
            Count += 1
        Next
         Count = 0
        For Each row In DGV3.Rows
            Select Case Count
                Case 0
                    My.Settings.Style_Maps = row.Cells(1).Value
            End Select
            Count += 1
        Next
         Count = 0
        For Each row In DGV4.Rows
            Select Case Count
                Case 0
                    My.Settings.Saving_data = row.Cells(1).Value
            End Select
            Count += 1
        Next
         Count = 0
        For Each row In DGV5.Rows
            Select Case Count
                Case 0
                    My.Settings.DefaultColorForeground = DirectCast(row.Tag, Color)
                Case 1
                    My.Settings.DefaultColorBackground = DirectCast(row.Tag, Color)
                Case 2
                    My.Settings.DefaultColor_ColorTitles = DirectCast(row.Tag, Color)
                Case 3
                    My.Settings.DefaultColor_NewColorFiles = DirectCast(row.Tag, Color)
            End Select
            Count += 1
        Next
         Count = 0
        For Each row In DGV6.Rows
            Select Case Count
                Case 0
                    My.Settings.FontSize = row.Cells(1).Value
                Case 1
                    My.Settings.FontStyle = row.Cells(1).Value
            End Select
            Count += 1
        Next
         Count = 0
        For Each row In DGV7.Rows
            Select Case Count
                Case 0
                    My.Settings.Flags_Visible = row.Cells(1).Value
                Case 1
                    My.Settings.Flags_Size = row.Cells(1).Value
            End Select
            Count += 1
        Next
         Count = 0
        For Each row In DGV8.Rows
            Select Case Count
                Case 0
                    My.Settings.SStatus_Visible = row.Cells(1).Value
            End Select
            Count += 1
        Next
         Count = 0
        For Each row In DGV9.Rows
            Select Case Count
                Case 0
                    My.Settings.FM_IC_Size = row.Cells(1).Value
            End Select
            Count += 1
        Next
         My.Settings.Save()
         MsgBox("Saved Changes will be made after restarting the program", MsgBoxStyle.Information, reso.nameRAT)
         Me.Close()
    End Sub
     Private Sub DL_Click(sender As Object, e As EventArgs) Handles DL.Click
         My.Settings.Reset()
         DGV0.Rows.Clear()
         DGV1.Rows.Clear()
         DGV2.Rows.Clear()
         DGV3.Rows.Clear()
         DGV4.Rows.Clear()
         DGV5.Rows.Clear()
         DGV6.Rows.Clear()
         DGV7.Rows.Clear()
         DGV8.Rows.Clear()
         DGV9.Rows.Clear()
         R()
         Dim xcol As String = SpySettings.ColumnsIndex
       End Sub
     Private Sub ClearSEL(ByVal DG0 As DataGridView)
        For Each gr As DataGridView In Panel1.Controls.OfType(Of DataGridView)()
            If Not gr.Name = DG0.Name Then
                If gr.Rows.Count > 0 Then
                    gr.CurrentCell = gr.Rows(0).Cells(0)
                    gr.ClearSelection()
                End If
            End If
        Next
    End Sub
    Private Sub DGV0_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV0.CellEnter
        ClearSEL(DGV0)
     End Sub
    Private Sub DGV1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV1.CellEnter
        ClearSEL(DGV1)
    End Sub
    Private Sub DGV2_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV2.CellEnter
        ClearSEL(DGV2)
    End Sub
    Private Sub DGV3_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV3.CellEnter
        ClearSEL(DGV3)
    End Sub
    Private Sub DGV4_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV4.CellEnter
        ClearSEL(DGV4)
    End Sub
    Private Sub DGV5_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV5.CellEnter
        ClearSEL(DGV5)
    End Sub
    Private Sub DGV6_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV6.CellEnter
        ClearSEL(DGV6)
    End Sub
    Private Sub DGV7_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV7.CellEnter
        ClearSEL(DGV7)
    End Sub
    Private Sub DGV8_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV8.CellEnter
        ClearSEL(DGV8)
    End Sub
     Private Sub DGV9_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV9.CellEnter
        ClearSEL(DGV9)
    End Sub
     Private Sub DGV5_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV5.CellContentClick
        If e.RowIndex = 0 Or e.RowIndex = 1 Or e.RowIndex = 2 Or e.RowIndex = 3 Then
            If e.ColumnIndex = 1 Then
                 Dim ColorDialog1 As New Color_Box0
                 If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim bit As New Bitmap(21, 17)
                    Dim g As Graphics = Graphics.FromImage(bit)
                    Dim clr As Color = ColorDialog1.C_Box3.BackColor
                    g.Clear(clr)
                     Dim p As Pen = New Pen(ControlPaint.Light(clr), 1)
                    g.DrawRectangle(p, 0, 0, bit.Width - 1, bit.Height - 1)
                     DGV5.Rows(e.RowIndex).Tag = clr
                    DGV5.Rows(e.RowIndex).Cells(1).Value = bit
                    g.Dispose()
                End If
                ColorDialog1.Close()
            End If
         End If
    End Sub
    Private Sub CrateColor(ByVal packet As Color())
        Dim cou As Integer = 0
        For Each i In DGV5.Rows
            Dim r As Windows.Forms.DataGridViewRow = DirectCast(i, Windows.Forms.DataGridViewRow)
            Dim bit As New Bitmap(21, 17)
            Dim g As Graphics = Graphics.FromImage(bit)
            Dim clr As Color = packet(cou)
            g.Clear(clr)
            Dim p As Pen = New Pen(ControlPaint.Light(clr), 1)
            g.DrawRectangle(p, 0, 0, bit.Width - 1, bit.Height - 1)
            r.Tag = clr
            r.Cells(1).Value = bit
            g.Dispose()
            cou += 1
        Next
    End Sub
    Private Sub DGV1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV1.CellContentClick
     End Sub
     Private Sub DefaultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DefaultToolStripMenuItem.Click
        Dim packet As Color() = {Color.FromArgb(106, 106, 106), Color.FromArgb(206, 206, 206), Color.FromArgb(70, 130, 180), Color.FromArgb(95, 158, 160)}
        CrateColor(packet)
       End Sub
     Private Sub clr_1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles clr_1.Click
        Dim packet As Color() = {Color.FromArgb(45, 156, 202),
            Color.FromArgb(37, 39, 77),
            Color.FromArgb(169, 171, 184),
            Color.FromArgb(159, 64, 103)}
        CrateColor(packet)
    End Sub
     Private Sub clr_2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles clr_2.Click
        Dim packet As Color() = {Color.FromArgb(55, 176, 169),
           Color.FromArgb(222, 242, 241),
           Color.FromArgb(43, 122, 119),
           Color.FromArgb(23, 36, 42)}
        CrateColor(packet)
    End Sub
     Private Sub Clr3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Clr3ToolStripMenuItem.Click
        Dim packet As Color() = {Color.FromArgb(47, 68, 85),
   Color.FromArgb(220, 220, 220),
   Color.FromArgb(84, 102, 116),
   Color.FromArgb(218, 123, 147)}
        CrateColor(packet)
    End Sub
     Private Sub Clr4ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Clr4ToolStripMenuItem.Click
        Dim packet As Color() = {Color.FromArgb(217, 63, 135),
Color.FromArgb(42, 27, 60),
Color.FromArgb(130, 101, 167),
Color.FromArgb(68, 49, 141)}
        CrateColor(packet)
    End Sub
     Private Sub Clr5ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Clr5ToolStripMenuItem.Click
        Dim packet As Color() = {Color.FromArgb(61, 61, 61),
Color.FromArgb(222, 222, 222),
Color.FromArgb(4, 94, 175),
Color.FromArgb(30, 175, 4)}
        CrateColor(packet)
    End Sub
     Private Sub Clr6ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Clr6ToolStripMenuItem.Click
        Dim packet As Color() = {Color.FromArgb(0, 122, 204),
Color.FromArgb(37, 37, 38),
Color.FromArgb(241, 241, 241),
Color.FromArgb(87, 116, 48)}
        CrateColor(packet)
    End Sub
     Private Sub Clr7ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Clr7ToolStripMenuItem.Click
        Dim packet As Color() = {Color.FromArgb(45, 40, 62),
Color.FromArgb(208, 215, 225),
Color.FromArgb(129, 43, 178),
Color.FromArgb(158, 165, 172)}
        CrateColor(packet)
    End Sub
     Private Sub Clr8ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Clr8ToolStripMenuItem.Click
        Dim packet As Color() = {Color.FromArgb(94, 94, 94),
Color.FromArgb(40, 40, 40),
Color.FromArgb(198, 198, 198),
Color.FromArgb(12, 159, 26)}
        CrateColor(packet)
    End Sub
     Private Sub Clr9ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Clr9ToolStripMenuItem.Click
        Dim packet As Color() = {Color.FromArgb(232, 191, 106),
Color.FromArgb(43, 43, 43),
Color.FromArgb(169, 183, 198),
Color.FromArgb(75, 119, 81)}
        CrateColor(packet)
    End Sub
End Class