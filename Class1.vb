
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Imports System.Configuration
Imports System.Net
Imports System.Net.NetworkInformation

Imports System.Runtime.InteropServices
Public Class RawPrinterHelper
    ' Structure and API declarions:
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
    Public Class DOCINFOA
        <MarshalAs(UnmanagedType.LPStr)>
        Public pDocName As String
        <MarshalAs(UnmanagedType.LPStr)>
        Public pOutputFile As String
        <MarshalAs(UnmanagedType.LPStr)>
        Public pDataType As String
    End Class

    <DllImport("winspool.Drv", EntryPoint:="OpenPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function OpenPrinter(
    <MarshalAs(UnmanagedType.LPStr)> ByVal szPrinter As String, <Out> ByRef hPrinter As IntPtr, ByVal pd As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="ClosePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Integer,
    <[In], MarshalAs(UnmanagedType.LPStruct)> ByVal di As DOCINFOA) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="WritePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Integer, <Out> ByRef dwWritten As Integer) As Boolean
    End Function


    ' SendBytesToPrinter()
    ' When the function is given a printer name and an unmanaged array
    ' of bytes, the function sends those bytes to the print queue.
    ' Returns true on success, false on failure.
    Public Function SendBytesToPrinter(ByVal szPrinterName As String, ByVal pBytes As IntPtr, ByVal dwCount As Integer) As Boolean
        Dim dwError As Integer = 0, dwWritten As Integer = 0
        Dim hPrinter As IntPtr = New IntPtr(0)
        Dim di As DOCINFOA = New DOCINFOA()
        Dim bSuccess As Boolean = False ' Assume failure unless you specifically succeed.
        di.pDocName = "My C#.NET RAW Document"
        di.pDataType = "RAW"


        ' Open the printer.
        If OpenPrinter(szPrinterName.Normalize(), hPrinter, IntPtr.Zero) Then

            ' Start a document.
            If StartDocPrinter(hPrinter, 1, di) Then

                ' Start a page.
                If StartPagePrinter(hPrinter) Then
                    ' Write your bytes.
                    bSuccess = WritePrinter(hPrinter, pBytes, dwCount, dwWritten)
                    EndPagePrinter(hPrinter)
                End If

                EndDocPrinter(hPrinter)
            End If

            ClosePrinter(hPrinter)
        End If

        ' If you did not succeed, GetLastError may give more information
        ' about why not.
        If bSuccess = False Then
            dwError = Marshal.GetLastWin32Error()
        End If

        Return bSuccess
    End Function
End Class
