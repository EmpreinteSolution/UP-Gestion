Imports System.Windows.Forms

Public Class Class2

    Inherits NativeWindow

    Private Shared _instance As Class2

    Private Sub New()
        ' Constructeur privé pour empêcher l'instanciation directe
    End Sub

    Public Shared Sub Initialize()
        If _instance Is Nothing Then
            _instance = New Class2()
        End If
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &H102 Then ' WM_CHAR
            Dim keyChar As Char = ChrW(CInt(m.WParam))
            If keyChar = "." Then
                ' Remplace le point par une virgule
                NativeMethods.PostMessage(Handle, &H102, AscW(","c), 0)
                Return
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    Private NotInheritable Class NativeMethods
        Private Sub New()
        End Sub

        Friend Declare Auto Function PostMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
    End Class

End Class
