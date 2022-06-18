Imports System.ComponentModel
 Public Class Win_Users
    Private Sub Win_Users_Load(sender As Object, e As EventArgs) Handles MyBase.Load
         Me.Icon = New Icon(reso.res_Path & "\Icons\win\1.ico")
         Me.ctxz.Renderer = New ThemeToolStrip
         DGV0.ContextMenuStrip = Me.ctxz
         DGV0.ColumnHeadersDefaultCellStyle.Font = reso.f
         DGV0.DefaultCellStyle.Font = reso.f
         SpyStyle()
         MYTEXT()
         Me.TOpacity.Interval = SpySettings.T_Interval
         Me.TOpacity.Enabled = True
     End Sub
    Private Sub SpyStyle()
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
    Private Sub Win_Users_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CypherRat.WU = Nothing
    End Sub
     Private Sub DGV0_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV0.CellContentClick
     End Sub
    Public Sub MYTEXT()
        Me.Text = "Users (" & CStr(DGV0.Rows.Count) & ")"
    End Sub
     Private Sub DGV0_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV0.CellMouseDoubleClick
        If DGV0.Rows.Count > 0 Then
            If Not e.RowIndex = -1 Then
                Dim folder As String = DGV0.Rows(e.RowIndex).Cells(2).Value
                Dim user As String = reso.res_Path & "\Users\" & folder
                If IO.Directory.Exists(user) Then
                    Process.Start(user)
                Else
                    DGV0.Rows.RemoveAt(e.RowIndex)
                    MYTEXT()
                End If
            End If
        End If
    End Sub
     Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        If DGV0.SelectedRows.Count > 0 Then
            For i As Integer = DGV0.SelectedRows.Count - 1 To 0 Step -1
                Dim folder As String = DGV0.Rows(DGV0.SelectedRows(i).Index).Cells(2).Value
                Dim user As String = reso.res_Path & "\Users\" & folder
                If IO.Directory.Exists(user) Then
                    Process.Start(user)
                Else
                    DGV0.Rows.RemoveAt(DGV0.SelectedRows(i).Index)
                    MYTEXT()
                End If
            Next
        End If
    End Sub
     Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If DGV0.SelectedRows.Count > 0 Then
            For i As Integer = DGV0.SelectedRows.Count - 1 To 0 Step -1
                Dim folder As String = DGV0.Rows(DGV0.SelectedRows(i).Index).Cells(2).Value
                Dim user As String = reso.res_Path & "\Users\" & folder
                DGV0.Rows.RemoveAt(DGV0.SelectedRows(i).Index)
                MYTEXT()
                If IO.Directory.Exists(user) Then
                    Try
                        IO.Directory.Delete(user, True)
                    Catch ex As Exception
                     End Try
                End If
            Next
        End If
    End Sub
End Class