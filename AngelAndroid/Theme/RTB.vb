'-----------------------------------
'--------Cypher Rat By EVLF
'------------------------
'-------t . me /evlfdev
'-----------------------------------
Imports System.Runtime.InteropServices
Public Class RTB
     Inherits RichTextBox
    Private ctrlPressed As Boolean
    Private Const WM_MOUSEWHEEL As Integer = &H20A
    Public Sub New()
        Me.SetStyle(ControlStyles.EnableNotifyMessage, True)
        HideCaret(Me.Handle)
    End Sub
     Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If Not (m.Msg = WM_MOUSEWHEEL AndAlso ctrlPressed) Then MyBase.WndProc(m)
        HideCaret(Me.Handle)
    End Sub
     Protected Overrides Sub OnKeyDown(e As System.Windows.Forms.KeyEventArgs)
        If e.Modifiers = Keys.Control Then ctrlPressed = True
        MyBase.OnKeyDown(e)
        HideCaret(Me.Handle)
    End Sub
     Protected Overrides Sub OnKeyUp(e As System.Windows.Forms.KeyEventArgs)
        ctrlPressed = False
        MyBase.OnKeyUp(e)
        HideCaret(Me.Handle)
    End Sub
    <DllImport("user32.dll", EntryPoint:="HideCaret")> Public Shared Function HideCaret(ByVal hwnd As IntPtr) As Boolean
    End Function
 End Class
