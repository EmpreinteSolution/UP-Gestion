Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient

Public Class venteCamion
    Dim adpt, adpt2 As MySqlDataAdapter
    Dim tbl, tbl2 As DataTable
    Private currentChildForm As Form
    Private Sub OpenChildForm(childForm As Form)
        'Open only form'
        If currentChildForm IsNot Nothing Then
            currentChildForm.Close()
        End If
        currentChildForm = childForm
        'end'
        childForm.TopLevel = False
        childForm.FormBorderStyle = FormBorderStyle.None
        childForm.Dock = DockStyle.Fill
        Panel3.Controls.Add(childForm)
        Panel3.Tag = childForm
        childForm.BringToFront()
        childForm.Show()
        Me.Text = childForm.Text
    End Sub
    Private Sub venteCamion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("SELECT `id` ,`id_camion`, (SELECT `matricule` FROM `camions` WHERE `id` = `id_camion`) AS matricule, `montant`,`paye` ,`reste`,`date_order` FROM `order_camions`", conn2)
        tbl = New DataTable
        adpt.Fill(tbl)

        For Each dt As DataRow In tbl.Rows
            DataGridView1.Rows.Add(dt.Item(0), dt.Item(1), dt.Item(2), dt.Item(3), dt.Item(4), dt.Item(5), dt.Item(6))

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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        comionid = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        ventecomionid = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        ventemontant = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        ventepaye = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        venteReste = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        ventedate = DataGridView1.Rows(e.RowIndex).Cells(6).Value
        ventematricule = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        OpenChildForm(New detailVenteCamillon)
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If DataGridView1.Rows.Count > 0 Then
            DataGridView1.Rows.Clear()
        End If

            If TextBox1.Text <> "" Then
                adpt2 = New MySqlDataAdapter("SELECT `id` FROM `camions` WHERE `matricule` = '" + TextBox1.Text.ToString + "'", conn2)
                tbl2 = New DataTable
                adpt2.Fill(tbl2)

                adpt = New MySqlDataAdapter("SELECT `id` ,`id_camion`, (SELECT `matricule` FROM `camions` WHERE `id` = `id_camion`) AS matricule, `montant`,`paye` ,`reste`,`date_order` FROM `order_camions` WHERE `id` = " + tbl2.Rows(0).Item(0).ToString, conn2)
                tbl = New DataTable
                adpt.Fill(tbl)

                For Each dt As DataRow In tbl.Rows
                    DataGridView1.Rows.Add(dt.Item(0), dt.Item(1), dt.Item(2), dt.Item(3), dt.Item(4), dt.Item(5), dt.Item(6))
                Next
            Else
                adpt = New MySqlDataAdapter("SELECT `id` ,`id_camion`, (SELECT `matricule` FROM `camions` WHERE `id` = `id_camion`) AS matricule, `montant`,`paye` ,`reste`,`date_order` FROM `order_camions`", conn2)
                tbl = New DataTable
                adpt.Fill(tbl)

                For Each dt As DataRow In tbl.Rows
                    DataGridView1.Rows.Add(dt.Item(0), dt.Item(1), dt.Item(2), dt.Item(3), dt.Item(4), dt.Item(5), dt.Item(6))

                Next
            End If
        End If
    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        If DataGridView1.Rows.Count > 0 Then
            DataGridView1.Rows.Clear()
        End If
        adpt = New MySqlDataAdapter("SELECT `id` ,`id_camion`, (SELECT `matricule` FROM `camions` WHERE `id` = `id_camion`) AS matricule, `montant`,`paye` ,`reste`,`date_order` FROM `order_camions` WHERE date_order BETWEEN @datedebut AND @datefin", conn2)
        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)
        tbl = New DataTable
        adpt.Fill(tbl)
        For Each dt As DataRow In tbl.Rows
            DataGridView1.Rows.Add(dt.Item(0), dt.Item(1), dt.Item(2), dt.Item(3), dt.Item(4), dt.Item(5), dt.Item(6))
        Next
    End Sub
    Dim m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Dim ReportToPrint As LocalReport
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)
        Return stream
    End Function
    Dim cmd As MySqlCommand
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged


        If TextBox1.Text = "" Then
            adpt = New MySqlDataAdapter("SELECT `id` ,`id_camion`, (SELECT `matricule` FROM `camions` WHERE `id` = `id_camion`) AS matricule, `montant`,`paye` ,`reste`,`date_order` FROM `order_camions`", conn2)
            tbl = New DataTable
            adpt.Fill(tbl)

            For Each dt As DataRow In tbl.Rows
                DataGridView1.Rows.Add(dt.Item(0), dt.Item(1), dt.Item(2), dt.Item(3), dt.Item(4), dt.Item(5), dt.Item(6))

            Next
        Else
            cmd = New MySqlCommand("SELECT `id` FROM `camions` WHERE `matricule` = '" + TextBox1.Text + "' ", conn2)

            conn2.Open()
            If IsDBNull(cmd.ExecuteScalar) Then
                conn2.Close()
            Else
                DataGridView1.Rows.Clear()
                adpt = New MySqlDataAdapter("SELECT `id` ,`id_camion`, (SELECT `matricule` FROM `camions` WHERE `id` = `id_camion`) AS matricule, `montant`,`paye` ,`reste`,`date_order` FROM `order_camions` WHERE id_camion = @id AND date_order BETWEEN @datedebut AND @datefin", conn2)
                adpt.SelectCommand.Parameters.AddWithValue("@id", cmd.ExecuteScalar)
                adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)

                tbl = New DataTable
                adpt.Fill(tbl)
                For Each dt As DataRow In tbl.Rows
                    DataGridView1.Rows.Add(dt.Item(0), dt.Item(1), dt.Item(2), dt.Item(3), dt.Item(4), dt.Item(5), dt.Item(6))
                Next
                conn2.Close()
            End If
        End If








    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub IconButton18_Click(sender As Object, e As EventArgs) Handles IconButton18.Click
        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("EtatVente")
        For i = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value.ToString)
        Next
        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\VenteEtat.rdlc"
        ReportToPrint.DataSources.Clear()


        ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
        ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next
        Dim printDoc As New PrintDocument()
        Dim printname As String = ComboBox5.Text
        printDoc.PrinterSettings.PrinterName = printname
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