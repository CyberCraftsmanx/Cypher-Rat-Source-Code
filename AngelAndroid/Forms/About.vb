﻿Imports System.ComponentModel
'------------------------------------
'---------Cypher Rat By EVLF
'-----------------------------------
'---------t.me/evlfdev
'------------------------------------
Public Class About
    Private mge As Image
    Private tele As String
    Private yt As String
    Private gm As String
    Private Ghb As String
    Private tip As New ThemeToolTip
     Public Sub defoultMsg()
        Interaction.MsgBox(Decrypt("9OR3DjQMZNzbgh7/91egFy/1GS1GEMO0MVRRy2LVq9q94ZDfKLkT6Lk6osg0wHYDzcIQVtvNC/pk
wY6cSHzpbV1nkGsLybZdHU9nx+m9vmJUinHmUq+wOJ7QOcb2PHruX3++yHLR9zQOH6zuuly/cIa6
P5G6gR8XF02LLRPguSc=", "Nagato"))
     End Sub
     Private Sub About_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Text = ""
        Label1.Text = ""
         Try
            Dim datastr() As String
            If sockets.Data.aboutdata IsNot Nothing Then
                datastr = sockets.Data.aboutdata
            Else
                datastr = Decrypt("DxT4xqvep3X1/srPPSPiNvorkLl+KsRdHyylgh2zDAuIiEkrDCOaGfoW14WfHpgxdubZITKWce3t
WrXkDJ6GsU986LpRF4pRFixZG1aJOvf3v6J8pt9w1XmjUv7VbOjJNsJnUy4Up4j7r6s5MyZv9Bau
QtFltp7MOa0R/i//hqc=", "FontsStyle").Split("~")
                tele = datastr(0)
                yt = datastr(1)
                gm = datastr(2)
                Ghb = datastr(3)
            End If
         Catch ex As Exception
            defoultMsg()
        End Try
             Label2.Text = "ـــــــــــــــــــ"
          Label4.Text = "Cypher Rat , is a android remote administration tool , Allows you to Control and Monitor any android device , By using this Program You are the only responsible for any kind of usage to this tool , and only install apk on device you have permission to"
         Label4.Tag = "t.me/evlfdev"
           mge = BXICO.Image
         Label1.ForeColor = Color.Cyan
        Label2.ForeColor = Color.Cyan
        Label3.ForeColor = Color.Cyan
        Label4.ForeColor = Color.Cyan






        Me.TOpacity.Interval = SpySettings.T_Interval
        TOpacity.Enabled = True
        tmr.Enabled = True
    End Sub
     Private Sub About_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
         Me.DialogResult = DialogResult.OK
     End Sub
                    Private Sub tmr_Tick(sender As Object, e As EventArgs) Handles tmr.Tick
          BXICO.Height = 148
              BXICO.Image = mge
                tmr.Enabled = False
            chtmr.Enabled = True
     End Sub
        Private count As Integer = 0
    Private count2 As Integer = 0
     Private TargerText2 As String = reso.nameRAT
    Private TargerText As String = "Advanced Android Remote Tool" +
        vbNewLine +
        Decrypt("5M38tjMPzm2qYgFtqZ28tA==", "OIRGOHVAERGO3") +
        vbNewLine +
        Decrypt("j5bEEL7Hb7LyzsyKFOAbzA==", "65fa1ewf51aw")
    Private Sub chtmr_Tick(sender As Object, e As EventArgs) Handles chtmr.Tick
         If Label1.Text.Length < TargerText2.Length Then
             Label1.Text += TargerText2.Substring(count2, 1)
            count2 += 1
         ElseIf Label3.Text.Length < TargerText.Length Then
             Label3.Text += TargerText.Substring(count, 1)
            count += 1
         Else
            Label4.Visible = True
            chtmr.Stop()
        End If
     End Sub
    Function GenRandom(ByVal Length As Integer) As String
        Dim Output As String = Nothing
        Dim UsedLetters As String = "▒⅝℮▌▒▌▓▌♪♣▓♠▓▒▓▒▌▓▌♪♣▓♠▓▒▌⅞▌▌⅛"
        For i = 1 To Length
            Threading.Thread.Sleep(5)
            Dim Rnd As New Random(Now.Millisecond)
            Output &= UsedLetters(Rnd.Next(0, UsedLetters.Length))
        Next
        Return Output
    End Function
     Private Sub TOpacity_Tick(sender As Object, e As EventArgs) Handles TOpacity.Tick
        If Not Me.Opacity = 1 Then
            Me.Opacity = Me.Opacity + 0.1
        Else
             Me.TOpacity.Enabled = False
        End If
    End Sub
     Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
         If Label4.Tag.ToString.Length > 0 Then
             Process.Start(Label4.Tag.ToString)
         End If
     End Sub
     Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Process.Start(tele)
        Catch ex As Exception
            defoultMsg()
        End Try
    End Sub
     Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Process.Start(yt)
        Catch ex As Exception
            defoultMsg()
        End Try
    End Sub
     Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            MsgBox("Contact with ME : " + gm)
        Catch ex As Exception
            defoultMsg()
        End Try
    End Sub
     Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Process.Start(Ghb)
        Catch ex As Exception
            defoultMsg()
        End Try
    End Sub
End Class