'------------------------------------
'---------Cypher Rat By EVLF
'-----------------------------------
'---------t.me/evlfdev
'------------------------------------

Public NotInheritable Class ListData
    Public wait As Boolean
    Public Sub New(ByVal ParametersClient As sockets.Client, ByVal ParametersByte As Byte(),
                   ByVal ParametersObject As Object(), ByVal ParametersTcpClient As Net.Sockets.TcpClient)
        Me.ClassClient = ParametersClient
        Me.bByte = ParametersByte
        Me.SizeData = ParametersObject
        Me.TcpClient = ParametersTcpClient
        Me.wait = False
    End Sub
    Public bByte As Byte()
    Public ClassClient As sockets.Client
    Public SizeData As Object()
    Public TcpClient As Net.Sockets.TcpClient
End Class
