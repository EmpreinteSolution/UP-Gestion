Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient

Imports System.Runtime.InteropServices
Public Class achatedit
    Dim adpt As MySqlDataAdapter
    Private Sub achatedit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Dim SaveDirectory As String = "c:\pospictures"
        Dim SavePath As String = System.IO.Path.Combine(SaveDirectory, tableimg.Rows(0).Item(11))
        If System.IO.File.Exists(SavePath) Then
            PictureBox1.Image = Image.FromFile(SavePath)
        End If
        Label1.Text = user
        Dim w = Screen.PrimaryScreen.WorkingArea.Width
        Dim h = My.Computer.Screen.WorkingArea.Size.Height
        Me.Width = w
        Me.Height = h
        Me.Location = New Point(0, 0)


        Dim pkInstalledPrinters As String

        ' Find all printers installed
        For Each pkInstalledPrinters In
        PrinterSettings.InstalledPrinters
            ComboBox1.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        ' Set the combo to the first printer in the list
        ComboBox1.SelectedIndex = 0

        TextBox8.Text = Label15.Text

        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim tablebq As New DataTable
        adpt.Fill(tablebq)

        For i = 0 To tablebq.Rows.Count - 1
            ComboBox4.Items.Add(tablebq.Rows(i).Item(1))
        Next

    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        If e.ColumnIndex = 0 Then
            DataGridView1.Sort(DataGridView1.Columns(21), System.ComponentModel.ListSortDirection.Ascending)
        End If
    End Sub
    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim adpt As New MySqlDataAdapter("select * from clients where client = '" + ComboBox2.SelectedItem.ToString + "'", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Label7.Text = table.Rows(0).Item(1)
        Label8.Text = table.Rows(0).Item(6)
        Label12.Text = table.Rows(0).Item(7)
        Label2.Text = table.Rows(0).Item(12)
    End Sub
    Dim tc As Double

    Dim ADPS As MySqlDataAdapter
    Dim ReportToPrint As LocalReport
    Dim m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)
        Return stream
    End Function
    Dim tbl As New DataTable
    Dim tbl2 As New DataTable
    Dim stck As Double
    Dim prod As String
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        If DataGridView1.Rows.Count <= 0 Then
            Dim yesornoMsg2 As New yesorno
            yesornoMsg2.Label14.Text = "Bon Vide !"
            yesornoMsg2.Panel5.Visible = True
            yesornoMsg2.ShowDialog()
        Else
            If ComboBox1.Text <> "" Then

                Dim cmd As MySqlCommand
                Dim cmd2, cmd3 As MySqlCommand
                Dim sql As String
                Dim sql2, sql3 As String
                Dim execution As Integer

                Dim ds As New DataSet1
                Dim dt As New DataTable
                dt = ds.Tables("achatdetails")
                For i = 0 To DataGridView1.Rows.Count - 1
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(8).Value, DataGridView1.Rows(i).Cells(17).Value, DataGridView1.Rows(i).Cells(20).Value, DataGridView1.Rows(i).Cells(19).Value)
                Next

                Dim ds2 As New DataSet2
                Dim dt2 As New DataTable
                dt2 = ds2.Tables("tva")
                For i = 0 To DataGridView2.Rows.Count - 1
                    dt2.Rows.Add(DataGridView2.Rows(i).Cells(0).Value, DataGridView2.Rows(i).Cells(1).Value, DataGridView2.Rows(i).Cells(2).Value, DataGridView2.Rows(i).Cells(3).Value)
                Next
                If Label16.Text <> "Reception :" Then

                    ReportToPrint = New LocalReport()
                    formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\Report10.rdlc"
                    formata4.ReportViewer1.LocalReport.DataSources.Clear()
                    formata4.ReportViewer1.LocalReport.EnableExternalImages = True

                    Dim cliente As New ReportParameter("client", Label7.Text)
                    Dim client1(0) As ReportParameter
                    client1(0) = cliente
                    formata4.ReportViewer1.LocalReport.SetParameters(client1)

                    Dim id As New ReportParameter("id", Label11.Text.ToString)
                    Dim id1(0) As ReportParameter
                    id1(0) = id
                    formata4.ReportViewer1.LocalReport.SetParameters(id1)

                    Dim paye As String
                    If CheckBox1.Checked Then
                        paye = "Payé"
                    Else
                        paye = "Non payé"
                    End If


                    Dim datee As New ReportParameter("date", Label6.Text)
                    Dim date1(0) As ReportParameter
                    date1(0) = datee
                    formata4.ReportViewer1.LocalReport.SetParameters(date1)

                    Dim tva As New ReportParameter("tva", Label10.Text)
                    Dim tva1(0) As ReportParameter
                    tva1(0) = tva
                    formata4.ReportViewer1.LocalReport.SetParameters(tva1)

                    Dim ttc As New ReportParameter("ttc", "-" & Label15.Text)
                    Dim ttc1(0) As ReportParameter
                    ttc1(0) = ttc
                    formata4.ReportViewer1.LocalReport.SetParameters(ttc1)

                    Dim neto As Double = Convert.ToDouble(Label9.Text.Replace(" ", "")) - Convert.ToDouble(Label27.Text.Replace(" ", ""))

                    Dim net As New ReportParameter("net", neto.ToString)
                    Dim net1(0) As ReportParameter
                    net1(0) = net
                    formata4.ReportViewer1.LocalReport.SetParameters(net1)


                    adpt = New MySqlDataAdapter("select * from infos", conn2)
                    Dim tableimg As New DataTable
                    adpt.Fill(tableimg)

                    Dim appPath As String = Application.StartupPath()

                    Dim SaveDirectory As String = appPath & "\"
                    Dim SavePath As String = SaveDirectory & imgName

                    Dim img As New ReportParameter("image", "File:\" + SavePath, True)
                    Dim img1(0) As ReportParameter
                    img1(0) = img
                    formata4.ReportViewer1.LocalReport.SetParameters(img1)




                    'formata4.ReportViewer1.LocalReport.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                    formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
                    formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dt2))

                    Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>5.83in</PageWidth><PageHeight>8.3in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
                    Dim warnings As Warning()
                    m_streams = New List(Of Stream)()
#Disable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.
                    formata4.ReportViewer1.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
#Enable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.

                    For Each stream As Stream In m_streams
                        stream.Position = 0
                    Next

                    Dim printDoc As New PrintDocument()
                    'Microsoft Print To PDF
                    'C80250 Series
                    'C80250 Series init


                    AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
                    m_currentPageIndex = 0
                    Dim pageSettings As New PageSettings()

                    ' Définissez la taille de la page
                    pageSettings.PaperSize = New PaperSize("A5", 583, 827) ' Convertissez les pouces en centièmes de pouce (1 pouce = 100 centièmes de pouce)

                    ' Définissez les marges si nécessaire
                    pageSettings.Margins = New Margins(0, 0, 0, 0) ' Vous pouvez ajuster ces valeurs en fonction de vos besoins

                    ' Assurez-vous que l'orientation est en mode portrait (si nécessaire)
                    pageSettings.Landscape = False

                    ' Appliquez les paramètres de page à ReportViewer
                    formata4.ReportViewer1.SetPageSettings(pageSettings)

                    ' Configuration du PrintPreviewDialog
                    formata4.Show()
                    formata4.ReportViewer1.Refresh()

                    tbl.Rows.Clear()
                    tbl2.Rows.Clear()
                Else

                    ReportToPrint = New LocalReport()
                    formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\reception.rdlc"
                    formata4.ReportViewer1.LocalReport.DataSources.Clear()
                    formata4.ReportViewer1.LocalReport.EnableExternalImages = True

                    Dim cliente As New ReportParameter("client", Label7.Text)
                    Dim client1(0) As ReportParameter
                    client1(0) = cliente
                    formata4.ReportViewer1.LocalReport.SetParameters(client1)

                    Dim id As New ReportParameter("id", Label11.Text.ToString)
                    Dim id1(0) As ReportParameter
                    id1(0) = id
                    formata4.ReportViewer1.LocalReport.SetParameters(id1)


                    Dim datee As New ReportParameter("date", Label6.Text)
                    Dim date1(0) As ReportParameter
                    date1(0) = datee
                    formata4.ReportViewer1.LocalReport.SetParameters(date1)

                    Dim tva As New ReportParameter("tva", Label10.Text)
                    Dim tva1(0) As ReportParameter
                    tva1(0) = tva
                    formata4.ReportViewer1.LocalReport.SetParameters(tva1)

                    Dim ttc As New ReportParameter("ttc", Label15.Text)
                    Dim ttc1(0) As ReportParameter
                    ttc1(0) = ttc
                    formata4.ReportViewer1.LocalReport.SetParameters(ttc1)

                    Dim neto As Double = Convert.ToDouble(Label9.Text.Replace(" ", "")) - Convert.ToDouble(Label27.Text.Replace(" ", ""))

                    Dim net As New ReportParameter("net", neto.ToString)
                    Dim net1(0) As ReportParameter
                    net1(0) = net
                    formata4.ReportViewer1.LocalReport.SetParameters(net1)


                    adpt = New MySqlDataAdapter("select * from infos", conn2)
                    Dim tableimg As New DataTable
                    adpt.Fill(tableimg)

                    Dim appPath As String = Application.StartupPath()

                    Dim SaveDirectory As String = appPath & "\"
                    Dim SavePath As String = SaveDirectory & imgName

                    Dim img As New ReportParameter("image", "File:\" + SavePath, True)
                    Dim img1(0) As ReportParameter
                    img1(0) = img
                    formata4.ReportViewer1.LocalReport.SetParameters(img1)




                    'formata4.ReportViewer1.LocalReport.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                    formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
                    formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dt2))

                    Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>5.83in</PageWidth><PageHeight>8.3in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
                    Dim warnings As Warning()
                    m_streams = New List(Of Stream)()
#Disable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.
                    formata4.ReportViewer1.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
#Enable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.

                    For Each stream As Stream In m_streams
                        stream.Position = 0
                    Next

                    Dim printDoc As New PrintDocument()
                    'Microsoft Print To PDF
                    'C80250 Series
                    'C80250 Series init

                    AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
                    m_currentPageIndex = 0
                    Dim pageSettings As New PageSettings()

                    ' Définissez la taille de la page
                    pageSettings.PaperSize = New PaperSize("A5", 583, 827) ' Convertissez les pouces en centièmes de pouce (1 pouce = 100 centièmes de pouce)

                    ' Définissez les marges si nécessaire
                    pageSettings.Margins = New Margins(0, 0, 0, 0) ' Vous pouvez ajuster ces valeurs en fonction de vos besoins

                    ' Assurez-vous que l'orientation est en mode portrait (si nécessaire)
                    pageSettings.Landscape = False

                    ' Appliquez les paramètres de page à ReportViewer
                    formata4.ReportViewer1.SetPageSettings(pageSettings)

                    ' Configuration du PrintPreviewDialog
                    formata4.Show()
                    formata4.ReportViewer1.Refresh()
                    tbl.Rows.Clear()
                    tbl2.Rows.Clear()

                End If
                'If ComboBox1.Text = "Espèce" Then
                '    Dim codeOpenCashDrawer As Byte() = New Byte() {27, 112, 48, 55, 121}
                '    Dim pUnmanagedBytes As IntPtr = New IntPtr(0)
                '    pUnmanagedBytes = Marshal.AllocCoTaskMem(5)
                '    Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5)
                '    RawPrinterHelper.SendBytesToPrinter("C80250 Series init", pUnmanagedBytes, 5)
                '    Marshal.FreeCoTaskMem(pUnmanagedBytes)
                'End If
                'MsgBox("facture enregistrée")

                Me.Close()


            Else
                MsgBox("Merci de choisir l'imprimante")

            End If
        End If

    End Sub
    Private Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)
        Dim pageImage As New Metafile(m_streams(m_currentPageIndex))

        ' Adjust rectangular area with printer margins.
        Dim adjustedRect As New Rectangle(e.PageBounds.Left - CInt(e.PageSettings.HardMarginX), e.PageBounds.Top - CInt(e.PageSettings.HardMarginY), e.PageBounds.Width, e.PageBounds.Height)

        ' Draw a white background for the report
        e.Graphics.FillRectangle(Brushes.White, adjustedRect)

        ' Draw the report content
        e.Graphics.DrawImage(pageImage, adjustedRect)

        ' Prepare for the next page. Make sure we haven't hit the end.
        m_currentPageIndex += 1
        e.HasMorePages = (m_currentPageIndex < m_streams.Count)

        If e.HasMorePages = False Then
            For Each stream As Stream In m_streams
                stream.Dispose()
            Next
        End If


    End Sub
    Friend Class RawPrinterHelper
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
    End Class

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Dim cmd As MySqlCommand
        Dim cmd2 As MySqlCommand
        Dim sql As String
        Dim sql2 As String
        Dim execution As Integer
        adpt = New MySqlDataAdapter("select OrderID from devis order by OrderID desc", conn2)
        Dim table4 As New DataTable
        adpt.Fill(table4)
        Dim id As Integer
        If table4.Rows.Count <> 0 Then
            id = Val(table4.Rows(0).Item(0)) + 1
        Else
            id = 1
        End If

        If DataGridView1.Rows.Count <= 0 Then
            Dim yesornoMsg2 As New yesorno
            yesornoMsg2.Label14.Text = "Bon Vide !"
            yesornoMsg2.Panel5.Visible = True
            yesornoMsg2.ShowDialog()
        Else
            If dashboard.ComboBox2.Text <> "" Then
                conn2.Close()
                conn2.Open()
                sql2 = "INSERT INTO devis (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`client`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Label15.Text.ToString.Replace(",", ".") + "','" + Label2.Text + "','" + Label7.Text + "')"
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.Parameters.Clear()
                cmd2.ExecuteNonQuery()
                For Each dr As DataGridViewRow In Me.DataGridView1.Rows
                    conn2.Close()
                    conn2.Close()
                    conn2.Open()
                    Try
                        cmd = New MySqlCommand
                        sql = “INSERT INTO orderdetails (OrderID ,ProductID ,Price,Quantity,Total,date,pa,type,name) VALUES (@tick, @code, @price, @qty, @montant, @date,@pa,@type,@name);”
                        prod = dr.Cells(5).Value
                        With cmd
                            .Connection = conn2
                            .CommandText = sql
                            .Parameters.Clear()
                            If table4.Rows().Count = 0 Then
                                .Parameters.AddWithValue(“@tick”, 1)
                                tc = 1
                            Else
                                .Parameters.AddWithValue(“@tick”, table4.Rows(0).Item(0) + 1)
                                tc = table4.Rows(0).Item(0) + 1
                            End If
                            .Parameters.AddWithValue(“@code”, dr.Cells(0).Value)
                            .Parameters.AddWithValue(“@price”, dr.Cells(5).Value)
                            .Parameters.AddWithValue(“@qty”, dr.Cells(3).Value)
                            .Parameters.AddWithValue(“@montant”, dr.Cells(7).Value)
                            .Parameters.AddWithValue(“@date”, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                            .Parameters.AddWithValue(“@pa”, 0)
                            .Parameters.AddWithValue(“@type”, "devis")
                            .Parameters.AddWithValue(“@name”, dr.Cells(1).Value)
                            execution = .ExecuteNonQuery()
                        End With
                        conn2.Close()

                    Catch ex As MySqlException
                        Dim yesornoMsg2 As New success
                        yesornoMsg2.Label14.Text = ex.Message
                        yesornoMsg2.Panel5.Visible = True
                        yesornoMsg2.ShowDialog()
                    End Try

                Next




                Dim ds As New DataSet1
                Dim dt As New DataTable
                dt = ds.Tables("tick")
                For i = 0 To DataGridView1.Rows.Count - 1
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(8).Value)
                Next


                ReportToPrint = New LocalReport()
                ReportToPrint.ReportPath = Application.StartupPath + "\devi.rdlc"
                ReportToPrint.DataSources.Clear()
                ReportToPrint.EnableExternalImages = True


                Dim cliente As New ReportParameter("client", Label7.Text + vbCrLf + Label8.Text + vbCrLf + Label12.Text)
                Dim client1(0) As ReportParameter
                client1(0) = cliente
                ReportToPrint.SetParameters(client1)

                Dim fac As New ReportParameter("fac", TextBox1.Text)
                Dim fac1(0) As ReportParameter
                fac1(0) = fac
                ReportToPrint.SetParameters(fac1)


                adpt = New MySqlDataAdapter("select * from infos", conn2)
                Dim table3 As New DataTable
                adpt.Fill(table3)

                Dim datee As New ReportParameter("date", Label6.Text)
                Dim date1(0) As ReportParameter
                date1(0) = datee
                ReportToPrint.SetParameters(date1)

                Dim ht As New ReportParameter("HT", Label9.Text)
                Dim ht1(0) As ReportParameter
                ht1(0) = ht
                ReportToPrint.SetParameters(ht1)

                Dim tva As New ReportParameter("tva", Label10.Text)
                Dim tva1(0) As ReportParameter
                tva1(0) = tva
                ReportToPrint.SetParameters(tva1)

                Dim ttc As New ReportParameter("ttc", Label15.Text)
                Dim ttc1(0) As ReportParameter
                ttc1(0) = ttc
                ReportToPrint.SetParameters(ttc1)

                Dim ice As New ReportParameter("ice", Label2.Text)
                Dim ice1(0) As ReportParameter
                ice1(0) = ice
                ReportToPrint.SetParameters(ice1)


                Dim details As New ReportParameter("infos", table3.Rows(0).Item(1).ToString + vbCrLf + "Tél : " + table3.Rows(0).Item(2).ToString + vbCrLf + "Fax : " + table3.Rows(0).Item(10).ToString)
                Dim details1(0) As ReportParameter
                details1(0) = details
                ReportToPrint.SetParameters(details1)



                'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

                Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.04985in</PageWidth><PageHeight>12in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
                Dim warnings As Warning()
                m_streams = New List(Of Stream)()
#Disable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.
                ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
#Enable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.

                For Each stream As Stream In m_streams
                    stream.Position = 0
                Next

                Dim printDoc As New PrintDocument()
                'Microsoft Print To PDF
                'C80250 Series
                'C80250 Series init
                printDoc.PrinterSettings.PrinterName = ComboBox1.Text
                Dim ps As New PrinterSettings()
                ps.PrinterName = printDoc.PrinterSettings.PrinterName
                printDoc.PrinterSettings = ps

                AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
                m_currentPageIndex = 0
                printDoc.Print()
                tbl.Rows.Clear()
                tbl2.Rows.Clear()

                'If ComboBox1.Text = "Espèce" Then
                '    Dim codeOpenCashDrawer As Byte() = New Byte() {27, 112, 48, 55, 121}
                '    Dim pUnmanagedBytes As IntPtr = New IntPtr(0)
                '    pUnmanagedBytes = Marshal.AllocCoTaskMem(5)
                '    Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5)
                '    RawPrinterHelper.SendBytesToPrinter("C80250 Series init", pUnmanagedBytes, 5)
                '    Marshal.FreeCoTaskMem(pUnmanagedBytes)
                'End If
                'MsgBox("facture enregistrée")

            Else
                MsgBox("Merci de choisir le client")

            End If
        End If
    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click
        MsgBox(Today.Date)
        ComboBox2.Visible = False
        TextBox5.Visible = True
        IconButton14.Visible = True
        IconButton15.Visible = True
    End Sub

    Private Sub IconButton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        conn2.Close()
        conn2.Open()
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = "INSERT INTO clients (name) VALUES ('" & TextBox5.Text & "')"
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.ExecuteNonQuery()
        conn2.Close()

        adpt = New MySqlDataAdapter("select name from clients", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        ComboBox2.Items.Clear()
        If table.Rows.Count <> 0 Then
            For i = 0 To table.Rows.Count - 1
                ComboBox2.Items.Add(table.Rows(i).Item(0))
            Next
        End If

        ComboBox2.SelectedItem = TextBox5.Text

        ComboBox2.Visible = True
        TextBox5.Visible = False
        IconButton14.Visible = False
        IconButton15.Visible = False
    End Sub

    Private Sub IconButton15_Click(sender As Object, e As EventArgs) Handles IconButton15.Click

        ComboBox2.Visible = True
        TextBox5.Visible = False
        IconButton14.Visible = False
        IconButton15.Visible = False

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        Dim sumttc As Double = 0
        Dim sumht As Double = 0
        Dim sumrms As Double = 0
        Dim rms As Double = 0

        rms = Val(DataGridView1.CurrentRow.Cells.Item(6).Value / 100)
        Dim totht As Double = (Val(DataGridView1.CurrentRow.Cells.Item(4).Value) * (DataGridView1.CurrentRow.Cells.Item(3).Value - DataGridView1.CurrentRow.Cells.Item(11).Value)) - ((Val(DataGridView1.CurrentRow.Cells.Item(4).Value) * (DataGridView1.CurrentRow.Cells.Item(3).Value - DataGridView1.CurrentRow.Cells.Item(11).Value)) * (DataGridView1.CurrentRow.Cells.Item(6).Value / 100))
        Dim totttc As Double = totht * (1 + (Val(DataGridView1.CurrentRow.Cells.Item(9).Value) / 100))
        DataGridView1.CurrentRow.Cells.Item(7).Value = Convert.ToDouble(totht).ToString("# ##0.00")
        DataGridView1.CurrentRow.Cells.Item(8).Value = Convert.ToDouble(totttc).ToString("# ##0.00")

        Dim ht, ht7, ht10, ht14, ht20 As Double
        Dim tva, tva7, tva10, tva14, tva20 As Double
        Dim sum0 As Double
        Dim sum7 As Double
        Dim sum10 As Double
        Dim sum14 As Double
        Dim sum20 As Double

        For j = 0 To DataGridView1.Rows.Count() - 1

            sumttc += DataGridView1.Rows(j).Cells(8).Value
            sumht += DataGridView1.Rows(j).Cells(4).Value * (DataGridView1.Rows(j).Cells(3).Value - DataGridView1.Rows(j).Cells(11).Value)
            sumrms += (DataGridView1.Rows(j).Cells(4).Value * (DataGridView1.Rows(j).Cells(3).Value - DataGridView1.Rows(j).Cells(11).Value)) * (DataGridView1.Rows(j).Cells(6).Value / 100)

            If DataGridView1.Rows(j).Cells(9).Value = 0 Then
                sum0 += DataGridView1.Rows(j).Cells(8).Value
                ht += DataGridView1.Rows(j).Cells(7).Value
                tva = 0

            End If

            If DataGridView1.Rows(j).Cells(9).Value = 7 Then
                sum7 += DataGridView1.Rows(j).Cells(8).Value
                ht7 += DataGridView1.Rows(j).Cells(7).Value
                tva7 = (ht7 * 7) / 100
            End If

            If DataGridView1.Rows(j).Cells(9).Value = 10 Then
                sum10 += DataGridView1.Rows(j).Cells(8).Value
                ht10 += DataGridView1.Rows(j).Cells(7).Value
                tva10 = (ht10 * 10) / 100
            End If

            If DataGridView1.Rows(j).Cells(9).Value = 14 Then
                sum14 += DataGridView1.Rows(j).Cells(8).Value
                ht14 += DataGridView1.Rows(j).Cells(7).Value
                tva14 = (ht14 * 14) / 100
            End If
            If DataGridView1.Rows(j).Cells(9).Value = 20 Then
                sum20 += DataGridView1.Rows(j).Cells(8).Value
                ht20 += DataGridView1.Rows(j).Cells(7).Value
                tva20 = (ht20 * 20) / 100
            End If

        Next

        DataGridView2.Rows(0).Cells(1).Value = Convert.ToDouble(ht.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(0).Cells(2).Value = Convert.ToDouble(tva.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(0).Cells(3).Value = Convert.ToDouble(sum0.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(1).Cells(1).Value = Convert.ToDouble(ht7.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(1).Cells(2).Value = Convert.ToDouble(tva7.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(1).Cells(3).Value = Convert.ToDouble(sum7.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(2).Cells(1).Value = Convert.ToDouble(ht10.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(2).Cells(2).Value = Convert.ToDouble(tva10.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(2).Cells(3).Value = Convert.ToDouble(sum10.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(3).Cells(1).Value = Convert.ToDouble(ht14.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(3).Cells(2).Value = Convert.ToDouble(tva14.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(3).Cells(3).Value = Convert.ToDouble(sum14.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(4).Cells(1).Value = Convert.ToDouble(ht20.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(4).Cells(2).Value = Convert.ToDouble(tva20.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(4).Cells(3).Value = Convert.ToDouble(sum20.ToString.Replace(".", ",")).ToString("# #00.00")

        Label15.Text = Convert.ToDouble(sumttc.ToString.Replace(".", ",")).ToString("# #00.00")
        Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("# #00.00")
        Label27.Text = Convert.ToDouble(sumrms.ToString.Replace(".", ",")).ToString("# #00.00")
        Label10.Text = Convert.ToDouble(sumttc - (sumht - sumrms)).ToString("# #00.00")

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

        If ComboBox3.Text = "Chèque" Then
            Label13.Visible = True
            Label14.Visible = True
            Label18.Visible = True
            Label22.Visible = True
            Label23.Visible = True
            Label24.Visible = False
            Label25.Visible = False
            TextBox5.Visible = True
            TextBox6.Visible = False
            TextBox7.Visible = False
            TextBox2.Visible = True
            TextBox3.Visible = True
            TextBox4.Visible = True
            DateTimePicker1.Visible = True
            ComboBox4.Visible = True
            Label29.Visible = True
        ElseIf ComboBox3.Text = "TPE" Then
            ComboBox4.Visible = True
            Label29.Visible = True
            Label13.Visible = False
            Label14.Visible = False
            Label18.Visible = False
            Label22.Visible = False
            Label23.Visible = False
            Label24.Visible = True
            Label25.Visible = True
            TextBox5.Visible = False
            TextBox6.Visible = True
            TextBox7.Visible = True
            TextBox2.Visible = False
            TextBox3.Visible = False
            TextBox4.Visible = False
            DateTimePicker1.Visible = False
        Else
            ComboBox4.Visible = False
            Label29.Visible = False
            Label13.Visible = False
            Label14.Visible = False
            Label18.Visible = False
            Label22.Visible = False
            Label23.Visible = False
            Label24.Visible = False
            Label25.Visible = False
            TextBox5.Visible = False
            TextBox6.Visible = False
            TextBox7.Visible = False
            TextBox2.Visible = False
            TextBox3.Visible = False
            TextBox4.Visible = False
            DateTimePicker1.Visible = False
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index)

            Dim sumttc As Double = 0
            Dim sumht As Double = 0
            Dim sumrms As Double = 0
            Dim ht, ht7, ht10, ht14, ht20 As Double
            Dim tva, tva7, tva10, tva14, tva20 As Double
            Dim sum0 As Double
            Dim sum7 As Double
            Dim sum10 As Double
            Dim sum14 As Double
            Dim sum20 As Double

            For j = 0 To DataGridView1.Rows.Count() - 1

                sumttc += DataGridView1.Rows(j).Cells(10).Value
                sumht += DataGridView1.Rows(j).Cells(3).Value * (DataGridView1.Rows(j).Cells(9).Value - DataGridView1.Rows(j).Cells(12).Value)
                sumrms += (DataGridView1.Rows(j).Cells(3).Value * (DataGridView1.Rows(j).Cells(9).Value - DataGridView1.Rows(j).Cells(12).Value)) * (DataGridView1.Rows(j).Cells(11).Value / 100)

                If DataGridView1.Rows(j).Cells(4).Value = 0 Then
                    sum0 += DataGridView1.Rows(j).Cells(10).Value
                    ht += DataGridView1.Rows(j).Cells(3).Value * (DataGridView1.Rows(j).Cells(9).Value - DataGridView1.Rows(j).Cells(12).Value)
                    tva = 0

                End If

                If DataGridView1.Rows(j).Cells(4).Value = 7 Then
                    sum7 += DataGridView1.Rows(j).Cells(10).Value
                    ht7 += DataGridView1.Rows(j).Cells(3).Value * (DataGridView1.Rows(j).Cells(9).Value - DataGridView1.Rows(j).Cells(12).Value)
                    tva7 = (ht7 * 7) / 100
                End If

                If DataGridView1.Rows(j).Cells(4).Value = 10 Then
                    sum10 += DataGridView1.Rows(j).Cells(10).Value
                    ht10 += DataGridView1.Rows(j).Cells(3).Value * (DataGridView1.Rows(j).Cells(9).Value - DataGridView1.Rows(j).Cells(12).Value)
                    tva10 = (ht10 * 10) / 100
                End If

                If DataGridView1.Rows(j).Cells(4).Value = 14 Then
                    sum14 += DataGridView1.Rows(j).Cells(10).Value
                    ht14 += DataGridView1.Rows(j).Cells(3).Value * (DataGridView1.Rows(j).Cells(9).Value - DataGridView1.Rows(j).Cells(12).Value)
                    tva14 = (ht14 * 14) / 100
                End If
                If DataGridView1.Rows(j).Cells(4).Value = 20 Then
                    sum20 += DataGridView1.Rows(j).Cells(10).Value
                    ht20 += DataGridView1.Rows(j).Cells(3).Value * (DataGridView1.Rows(j).Cells(9).Value - DataGridView1.Rows(j).Cells(12).Value)
                    tva20 = (ht20 * 20) / 100
                End If

            Next

            DataGridView2.Rows(0).Cells(1).Value = Convert.ToDouble(ht.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(0).Cells(2).Value = Convert.ToDouble(tva.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(0).Cells(3).Value = Convert.ToDouble(sum0.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(1).Cells(1).Value = Convert.ToDouble(ht7.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(1).Cells(2).Value = Convert.ToDouble(tva7.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(1).Cells(3).Value = Convert.ToDouble(sum7.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(2).Cells(1).Value = Convert.ToDouble(ht10.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(2).Cells(2).Value = Convert.ToDouble(tva10.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(2).Cells(3).Value = Convert.ToDouble(sum10.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(3).Cells(1).Value = Convert.ToDouble(ht14.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(3).Cells(2).Value = Convert.ToDouble(tva14.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(3).Cells(3).Value = Convert.ToDouble(sum14.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(4).Cells(1).Value = Convert.ToDouble(ht20.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(4).Cells(2).Value = Convert.ToDouble(tva20.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(4).Cells(3).Value = Convert.ToDouble(sum20.ToString.Replace(".", ",")).ToString("# #00.00")

            Label15.Text = Convert.ToDouble(sumttc.ToString.Replace(".", ",")).ToString("# #00.00")
            Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("# #00.00")
            Label27.Text = Convert.ToDouble(sumrms.ToString.Replace(".", ",")).ToString("# #00.00")
            Label10.Text = Convert.ToDouble(sumttc - (sumht - sumrms)).ToString("# #00.00")

        Else
            MessageBox.Show("Veuillez choisir un article !")
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        If TextBox9.Text = "" Then
            adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                ComboBox5.Items.Clear()
                ComboBox5.Items.Add("aucun")
                For i = 0 To table.Rows.Count - 1
                    ComboBox5.Items.Add(table.Rows(i).Item(0))
                Next
            End If
            ComboBox5.Text = Label31.Text
        Else
            ComboBox5.Items.Clear()
            adpt = New MySqlDataAdapter("select name from fours WHERE name LIKE '%" + TextBox9.Text.Replace("'", " ") + "%' order by name asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                For i = 0 To table.Rows.Count - 1
                    ComboBox5.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox5.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Try
            Dim cmd As MySqlCommand
            Dim cmd2, cmd3 As MySqlCommand
            Dim sql As String
            Dim sql2, sql3 As String
            Dim execution As Integer

            Dim etat As String
            If CheckBox1.Checked = True Then
                etat = "Payé"
            Else
                etat = "Non Payé"
            End If

            conn2.Close()
            conn2.Open()

            sql2 = "UPDATE `achat` SET `typePay`='" & ComboBox3.Text & "', `Fournisseur`='" & ComboBox5.Text & "' WHERE `id` = '" & Label11.Text.ToString & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()


            If ComboBox3.Text = "Chèque" Then
                sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `fac`,`bq`,`montant`) 
                    VALUES ('" + TextBox5.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + DateTimePicker1.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" & Label11.Text.ToString & "','" + ComboBox4.Text + "','" + Label15.Text + "')"
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.ExecuteNonQuery()
            End If
            If ComboBox3.Text = "TPE" Then
                sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `fac`,`bq`,`montant`) 
                    VALUES ('tpe','" + TextBox6.Text + "','tpe','" + TextBox7.Text + "','tpe', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" & Label11.Text.ToString & "','" + ComboBox4.Text + "','" + Label15.Text + "')"
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.ExecuteNonQuery()
            End If
            conn2.Close()

            MsgBox("Informations bien modifiés !")
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        adpt = New MySqlDataAdapter("select name,adresse,ville,ice from fours WHERE name = '" + ComboBox5.Text.Replace("'", " ") + "' ", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        If table.Rows.Count() <> 0 Then
            Label7.Text = table.Rows(0).Item(0)
            Label8.Text = "-"
            Label12.Text = "-"
            Label2.Text = "-"
        Else
            Label7.Text = "-0"
            Label8.Text = "-0"
            Label12.Text = "-0"
            Label2.Text = "-0"
        End If

    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Modifier Reception'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                If Label16.Text = "Reception :" Then
                    If Label32.Text = "Non Payée" Then

                        Dim id As String = Label11.Text

                        adpt = New MySqlDataAdapter("select dateAchat from achat where id = '" & id & "' ", conn2)
                        Dim tabledate As New DataTable
                        adpt.Fill(tabledate)

                        ajoutachat.DataGridView3.Rows.Clear()

                        If DataGridView1.Rows.Count() <> 0 Then
                            ajoutachat.Show()
                            ajoutachat.IconButton9.Visible = True
                            ajoutachat.IconButton1.Visible = False

                            adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
                            Dim tableimg As New DataTable
                            adpt.Fill(tableimg)

                            ajoutachat.ComboBox4.Items.Clear()
                            For i = 0 To tableimg.Rows.Count() - 1
                                ajoutachat.ComboBox4.Items.Add(tableimg.Rows(i).Item(0))
                            Next
                            ajoutachat.TextBox17.Text = id
                            ajoutachat.TextBox25.Text = TextBox10.Text.Replace("Document N°", "").Replace(" ", "")
                            ajoutachat.ComboBox4.Text = ComboBox5.Text
                            ajoutachat.Label40.Text = Convert.ToDateTime(tabledate.Rows(0).Item(0)).ToString("yyyy-MM-dd HH:mm:ss")
                            ajoutachat.DateTimePicker4.Value = Convert.ToDateTime(tabledate.Rows(0).Item(0))

                            For i = 0 To DataGridView1.Rows.Count - 1
                                ajoutachat.DataGridView3.Rows.Add(Convert.ToDouble(DataGridView1.Rows(i).Cells(0).Value).ToString("N0"), DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(8).Value, DataGridView1.Rows(i).Cells(9).Value, DataGridView1.Rows(i).Cells(10).Value, DataGridView1.Rows(i).Cells(11).Value, DataGridView1.Rows(i).Cells(12).Value, DataGridView1.Rows(i).Cells(13).Value, DataGridView1.Rows(i).Cells(14).Value, DataGridView1.Rows(i).Cells(15).Value, DataGridView1.Rows(i).Cells(16).Value, DataGridView1.Rows(i).Cells(17).Value, DataGridView1.Rows(i).Cells(18).Value, DataGridView1.Rows(i).Cells(19).Value, DataGridView1.Rows(i).Cells(20).Value, "-", Convert.ToDouble(DataGridView1.Rows(i).Cells(0).Value).ToString("N2"), Nothing, DataGridView1.Rows(i).Cells(22).Value)
                            Next
                            For Each row As DataGridViewRow In ajoutachat.DataGridView3.Rows
                                Dim cellValue As String = row.Cells(22).Value.ToString()
                                Dim numericValue As Double

                                If Double.TryParse(cellValue, numericValue) Then
                                    row.Cells(22).Value = numericValue
                                End If
                            Next
                            Dim sum As Double = 0
                            Dim ht As Double = 0
                            Dim remise As Double = 0
                            Dim tva As Double = 0
                            For i = 0 To ajoutachat.DataGridView3.Rows.Count - 1
                                sum = sum + ajoutachat.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                                remise = remise + (ajoutachat.DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * ajoutachat.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") * (ajoutachat.DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                                ht = ht + (ajoutachat.DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * ajoutachat.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")) * (1 - (ajoutachat.DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                                If ajoutachat.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",") >= 0 Then
                                    tva = tva + ((ajoutachat.DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") - ajoutachat.DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",")) * (ajoutachat.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - ajoutachat.DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ",")))
                                End If
                            Next

                            ajoutachat.Label25.Text = Label51.Text - Label53.Text
                            ajoutachat.Label51.Text = Label51.Text
                            ajoutachat.Label53.Text = Label53.Text
                            ajoutachat.Label23.Text = Label27.Text
                            If Label53.Text = 0 Then
                                ajoutachat.CheckBox2.Checked = False
                            Else
                                ajoutachat.CheckBox2.Checked = True
                            End If
                            If (Label27.Text - remise) <= 0 Then
                                ajoutachat.TextBox26.Text = 0.00
                            Else
                                ajoutachat.TextBox26.Text = Label27.Text - remise
                            End If
                            ajoutachat.Label47.Text = remise.ToString("# ##0.00")
                            ajoutachat.Label18.Text = Label9.Text
                            ajoutachat.Label43.Text = Label10.Text
                            ajoutachat.TextBox11.Text = 0
                            ajoutachat.TextBox25.Enabled = True
                            Me.Close()
                        Else
                            Dim yesornoMsg2 As New yesorno
                            yesornoMsg2.Label14.Text = "Bon de réception Vide, Veuillez resaisir le Bon."
                            yesornoMsg2.Panel5.Visible = True
                            yesornoMsg2.ShowDialog()
                        End If
                    Else
                        Dim yesornoMsg2 As New eror
                        yesornoMsg2.Label14.Text = "Impossible de modifier une réception réglée, veuillez supprimer le réglement avant de la modifier !"
                        yesornoMsg2.Panel5.Visible = True
                        yesornoMsg2.ShowDialog()
                    End If
                Else
                    If Label16.Text = "Devis :" Then

                        Dim id As String = Label11.Text

                        adpt = New MySqlDataAdapter("select dateAchat from devis where id = '" & id & "' ", conn2)
                        Dim tabledate As New DataTable
                        adpt.Fill(tabledate)

                        ajoutachat.DataGridView3.Rows.Clear()

                        If DataGridView1.Rows.Count() <> 0 Then
                            ajoutachat.Show()
                            ajoutachat.IconButton9.Visible = False
                            ajoutachat.IconButton1.Visible = False
                            ajoutachat.IconButton19.Visible = False
                            ajoutachat.IconButton20.Visible = False
                            ajoutachat.IconButton14.Visible = False
                            ajoutachat.TextBox25.Visible = False
                            ajoutachat.Panel9.Visible = True

                            adpt = New MySqlDataAdapter("select client from clients order by client asc", conn2)
                            Dim tableimg As New DataTable
                            adpt.Fill(tableimg)

                            ajoutachat.ComboBox1.Items.Clear()
                            For i = 0 To tableimg.Rows.Count() - 1
                                ajoutachat.ComboBox1.Items.Add(tableimg.Rows(i).Item(0))
                            Next
                            ajoutachat.TextBox17.Text = id
                            ajoutachat.TextBox25.Text = TextBox10.Text.Replace("Document N°", "").Replace(" ", "")
                            ajoutachat.ComboBox1.Text = ComboBox5.Text
                            ajoutachat.Label40.Text = Convert.ToDateTime(tabledate.Rows(0).Item(0)).ToString("yyyy-MM-dd HH:mm:ss")
                            ajoutachat.DateTimePicker4.Value = Convert.ToDateTime(tabledate.Rows(0).Item(0))

                            For i = 0 To DataGridView1.Rows.Count - 1
                                ajoutachat.DataGridView3.Rows.Add(Convert.ToDouble(DataGridView1.Rows(i).Cells(0).Value).ToString("N0"), DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(8).Value, DataGridView1.Rows(i).Cells(9).Value, DataGridView1.Rows(i).Cells(10).Value, DataGridView1.Rows(i).Cells(11).Value, DataGridView1.Rows(i).Cells(12).Value, DataGridView1.Rows(i).Cells(13).Value, DataGridView1.Rows(i).Cells(14).Value, DataGridView1.Rows(i).Cells(15).Value, DataGridView1.Rows(i).Cells(16).Value, DataGridView1.Rows(i).Cells(17).Value, DataGridView1.Rows(i).Cells(18).Value, DataGridView1.Rows(i).Cells(19).Value, DataGridView1.Rows(i).Cells(20).Value, "-", Convert.ToDouble(DataGridView1.Rows(i).Cells(0).Value).ToString("N2"), Nothing, DataGridView1.Rows(i).Cells(22).Value)
                            Next
                            For Each row As DataGridViewRow In ajoutachat.DataGridView3.Rows
                                Dim cellValue As String = row.Cells(22).Value.ToString()
                                Dim numericValue As Double

                                If Double.TryParse(cellValue, numericValue) Then
                                    row.Cells(22).Value = numericValue
                                End If
                            Next
                            Dim sum As Double = 0
                            Dim ht As Double = 0
                            Dim remise As Double = 0
                            Dim tva As Double = 0
                            For i = 0 To ajoutachat.DataGridView3.Rows.Count - 1
                                sum = sum + ajoutachat.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                                remise = remise + (ajoutachat.DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * ajoutachat.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") * (ajoutachat.DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                                ht = ht + (ajoutachat.DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * ajoutachat.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")) * (1 - (ajoutachat.DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                                If ajoutachat.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",") >= 0 Then
                                    tva = tva + ((ajoutachat.DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") - ajoutachat.DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",")) * (ajoutachat.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - ajoutachat.DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ",")))
                                End If
                            Next

                            ajoutachat.Label25.Text = Label51.Text - Label53.Text
                            ajoutachat.Label51.Text = Label51.Text
                            ajoutachat.Label53.Text = Label53.Text
                            ajoutachat.Label23.Text = Label27.Text
                            If Label53.Text = 0 Then
                                ajoutachat.CheckBox2.Checked = False
                            Else
                                ajoutachat.CheckBox2.Checked = True
                            End If
                            If (Label27.Text - remise) <= 0 Then
                                ajoutachat.TextBox26.Text = 0.00
                            Else
                                ajoutachat.TextBox26.Text = Label27.Text - remise
                            End If
                            ajoutachat.Label47.Text = remise.ToString("# ##0.00")
                            ajoutachat.Label18.Text = Label9.Text
                            ajoutachat.Label43.Text = Label10.Text
                            ajoutachat.TextBox11.Text = 0
                            Me.Close()
                        Else
                            Dim yesornoMsg2 As New yesorno
                            yesornoMsg2.Label14.Text = "Devis Vide, Veuillez resaisir le devis."
                            yesornoMsg2.Panel5.Visible = True
                            yesornoMsg2.ShowDialog()
                        End If
                    Else
                        Dim id As String = Label11.Text

                        adpt = New MySqlDataAdapter("select dateAchat from achat where id = '" & id & "' ", conn2)
                        Dim tabledate As New DataTable
                        adpt.Fill(tabledate)

                        ajoutachat.DataGridView3.Rows.Clear()

                        If DataGridView1.Rows.Count() <> 0 Then
                            ajoutachat.Show()
                            ajoutachat.IconButton9.Visible = True
                            ajoutachat.IconButton1.Visible = False

                            adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
                            Dim tableimg As New DataTable
                            adpt.Fill(tableimg)

                            ajoutachat.ComboBox4.Items.Clear()
                            For i = 0 To tableimg.Rows.Count() - 1
                                ajoutachat.ComboBox4.Items.Add(tableimg.Rows(i).Item(0))
                            Next
                            ajoutachat.TextBox17.Text = id
                            ajoutachat.TextBox25.Text = TextBox10.Text.Replace("Document N°", "").Replace(" ", "")
                            ajoutachat.ComboBox4.Text = ComboBox5.Text
                            ajoutachat.Label40.Text = Convert.ToDateTime(tabledate.Rows(0).Item(0)).ToString("yyyy-MM-dd HH:mm:ss")
                            ajoutachat.DateTimePicker4.Value = Convert.ToDateTime(tabledate.Rows(0).Item(0))

                            For i = 0 To DataGridView1.Rows.Count - 1
                                ajoutachat.DataGridView3.Rows.Add(Convert.ToDouble(DataGridView1.Rows(i).Cells(0).Value).ToString("N0"), DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(8).Value, DataGridView1.Rows(i).Cells(9).Value, DataGridView1.Rows(i).Cells(10).Value, DataGridView1.Rows(i).Cells(11).Value, DataGridView1.Rows(i).Cells(12).Value, DataGridView1.Rows(i).Cells(13).Value, DataGridView1.Rows(i).Cells(14).Value, DataGridView1.Rows(i).Cells(15).Value, DataGridView1.Rows(i).Cells(16).Value, DataGridView1.Rows(i).Cells(17).Value, DataGridView1.Rows(i).Cells(18).Value, DataGridView1.Rows(i).Cells(19).Value, DataGridView1.Rows(i).Cells(20).Value, "-", Convert.ToDouble(DataGridView1.Rows(i).Cells(0).Value).ToString("N2"), Nothing, DataGridView1.Rows(i).Cells(22).Value)
                            Next
                            For Each row As DataGridViewRow In ajoutachat.DataGridView3.Rows
                                Dim cellValue As String = row.Cells(22).Value.ToString()
                                Dim numericValue As Double

                                If Double.TryParse(cellValue, numericValue) Then
                                    row.Cells(22).Value = numericValue
                                End If
                            Next
                            Dim sum As Double = 0
                            Dim ht As Double = 0
                            Dim remise As Double = 0
                            Dim tva As Double = 0
                            For i = 0 To ajoutachat.DataGridView3.Rows.Count - 1
                                sum = sum + ajoutachat.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                                remise = remise + (ajoutachat.DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * ajoutachat.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") * (ajoutachat.DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                                ht = ht + (ajoutachat.DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * ajoutachat.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")) * (1 - (ajoutachat.DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                                If ajoutachat.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",") >= 0 Then
                                    tva = tva + ((ajoutachat.DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") - ajoutachat.DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",")) * (ajoutachat.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - ajoutachat.DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ",")))
                                End If
                            Next

                            ajoutachat.Label25.Text = Label51.Text.ToString.Replace(" ", "") - Label53.Text.ToString.Replace(" ", "")
                            ajoutachat.Label51.Text = Label51.Text
                            ajoutachat.Label53.Text = Label53.Text
                            ajoutachat.Label23.Text = Label27.Text

                            If Label53.Text = 0 Then
                                ajoutachat.CheckBox2.Checked = False
                            Else
                                ajoutachat.CheckBox2.Checked = True
                            End If

                            If (Label27.Text - remise) <= 0 Then
                                ajoutachat.TextBox26.Text = 0.00
                            Else
                                ajoutachat.TextBox26.Text = Label27.Text - remise
                            End If
                            ajoutachat.Label47.Text = remise.ToString("# ##0.00")
                            ajoutachat.Label18.Text = Label9.Text
                            ajoutachat.Label43.Text = Label10.Text
                            ajoutachat.TextBox11.Text = 0
                            ajoutachat.IconButton9.Visible = False
                            ajoutachat.IconButton19.Visible = False
                            ajoutachat.IconButton1.Visible = False
                            ajoutachat.IconButton14.Visible = False
                            ajoutachat.IconButton20.Visible = True

                            Me.Close()
                        Else
                            Dim yesornoMsg2 As New yesorno
                            yesornoMsg2.Label14.Text = "Bon de réception Vide, Veuillez resaisir le Bon."
                            yesornoMsg2.Panel5.Visible = True
                            yesornoMsg2.ShowDialog()
                        End If
                    End If
                End If

            Else
                Dim yesornoMsg2 As New eror
                yesornoMsg2.Label14.Text = "Vous n'avez pas l'autorisation !"
                yesornoMsg2.Panel5.Visible = True
                yesornoMsg2.ShowDialog()
            End If
        End If



    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        If Label32.Text = "Non Payée" Then
            If Label16.Text = "Avoir :" Then
                Dim Rep As Integer = MsgBox("Voulez-vous vraiment supprimer cet Avoir ?", vbYesNo)
                If Rep = vbYes Then
                    conn2.Close()
                    conn2.Open()
                    Dim cmd As MySqlCommand
                    cmd = New MySqlCommand("DELETE FROM `achatdetails` WHERE `achat_Id` = '" & Label11.Text & "' ", conn2)
                    cmd.ExecuteNonQuery()

                    cmd = New MySqlCommand("DELETE FROM `achat` WHERE `id` = '" & Label11.Text & "' ", conn2)
                    cmd.ExecuteNonQuery()

                    For i = 0 To DataGridView1.Rows.Count() - 1
                        cmd = New MySqlCommand("UPDATE `article` SET `Stock` =  REPLACE(`Stock`,',','.') + '" & DataGridView1.Rows(i).Cells(3).Value.ToString.Replace(",", ".") & "' WHERE `Code` = '" + DataGridView1.Rows(i).Cells(1).Value.ToString.Replace(".", ",") + "' ", conn2)
                        cmd.ExecuteNonQuery()
                    Next

                    conn2.Close()

                    Me.Close()
                End If
            Else
                Dim Rep As Integer = MsgBox("Voulez-vous vraiment supprimer cette reception ?", vbYesNo)
                If Rep = vbYes Then
                    conn2.Close()
                    conn2.Open()
                    Dim cmd As MySqlCommand
                    cmd = New MySqlCommand("DELETE FROM `achatdetails` WHERE `achat_Id` = '" & Label11.Text & "' ", conn2)
                    cmd.ExecuteNonQuery()

                    cmd = New MySqlCommand("DELETE FROM `achat` WHERE `id` = '" & Label11.Text & "' ", conn2)
                    cmd.ExecuteNonQuery()
                    For i = 0 To DataGridView1.Rows.Count() - 1
                        cmd = New MySqlCommand("UPDATE `article` SET `Stock` =  REPLACE(`Stock`,',','.') - '" & DataGridView1.Rows(i).Cells(3).Value.ToString.Replace(",", ".") & "' WHERE `Code` = '" + DataGridView1.Rows(i).Cells(1).Value.ToString.Replace(".", ",") + "' ", conn2)
                        cmd.ExecuteNonQuery()
                    Next


                    conn2.Close()

                    Me.Close()
                End If
            End If

        Else
            MsgBox("Vous ne pouvez pas supprimer une réception déjà réglé !")
        End If
    End Sub

    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        ' Vérifiez si c'est une ligne de données (pas la ligne d'en-tête ni la ligne de total)
        If e.RowIndex >= 0 Then
            ' Supprimez les bordures des cellules sauf pour la dernière ligne
            If e.RowIndex < DataGridView1.Rows.Count - 1 Then
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None
            End If
            e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None
        End If
    End Sub
End Class