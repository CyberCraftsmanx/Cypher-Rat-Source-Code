Imports System.Threading
'------------------------------------
'---------Cypher Rat By EVLF
'-----------------------------------
'---------t.me/evlfdev
'------------------------------------
Public Class SyncLocker
    Dim inner As Object
    Dim outer As Object
    Public Sub New(obj1 As Object)
        inner = obj1
        outer = obj1
    End Sub
    Public Sub Enter()
        If Monitor.IsEntered(inner) Then
            Monitor.Enter(inner)
        Else
            SyncLock outer
                Monitor.Enter(inner)
            End SyncLock
        End If
    End Sub
    Public Sub Exiter()
        Monitor.Exit(inner)
    End Sub
End Class
