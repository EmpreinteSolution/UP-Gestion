Imports MySql.Data.MySqlClient
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms

Public Class detailVenteCamillon
    Dim adpt As MySqlDataAdapter
    Dim tbl As DataTable
    Private Sub detailVenteCamillon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("Select `code_article`, `article`, `prix`, `qte`, `montant` FROM `order_details_camions` WHERE `id_order_camion` = " + ventecomionid.ToString, conn2)
        tbl = New DataTable
        adpt.Fill(tbl)

        For Each dt As DataRow In tbl.Rows
            DataGridView1.Rows.Add(dt.Item(0), dt.Item(1), dt.Item(2), dt.Item(3), dt.Item(4))
        Next

        Dim pkInstalledPrinters As String

        ' Find all printers installed
        ComboBox5.Items.Clear()
        For Each pkInstalledPrinters In
        PrinterSettings.InstalledPrinters
            ComboBox5.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        ' Set the combo to the first printer in the list
        ComboBox5.SelectedIndex = 0
    End Sub

    Private Sub IconButton19_Click(sender As Object, e As EventArgs) Handles IconButton19.Click
        Me.Close()
    End Sub
    Dim m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Dim ReportToPrint As LocalReport
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)
        Return stream
    End Function
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("EtatVentedetails")
        For i = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value.ToString, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value)
        Next
        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\VenteEtatDetails.rdlc"
        ReportToPrint.DataSources.Clear()
        Dim matricule As New ReportParameter("matricule", ventematricule)
        Dim matricule1(0) As ReportParameter
        matricule1(0) = matricule
        ReportToPrint.SetParameters(matricule1)
        Dim total As New ReportParameter("total", ventemontant & " DHS")
        Dim total1(0) As ReportParameter
        total1(0) = total
        ReportToPrint.SetParameters(total1)
        Dim dater As New ReportParameter("date", ventedate)
        Dim date1(0) As ReportParameter
        date1(0) = dater
        ReportToPrint.SetParameters(date1)
        Dim reste As New ReportParameter("reste", venteReste & " DHS")
        Dim reste1(0) As ReportParameter
        reste1(0) = reste
        ReportToPrint.SetParameters(reste1)

        Dim pay As New ReportParameter("paye", ventepaye & " DHS")
        Dim pay1(0) As ReportParameter
        pay1(0) = pay
        ReportToPrint.SetParameters(pay1)

        ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
        ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next
        Dim printDoc As New PrintDocument()
        Dim printname As String = System.Configuration.ConfigurationSettings.AppSettings("printar")
        printDoc.PrinterSettings.PrinterName = ComboBox5.Text
        Dim ps As New PrinterSettings()
        ps.PrinterName = printDoc.PrinterSettings.PrinterName
        printDoc.PrinterSettings = ps

        AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
        m_currentPageIndex = 0
        printDoc.Print()
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
End Class