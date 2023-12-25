Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports ClosedXML.Excel

Public Class bcmnd
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Private Sub bcmnd_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Dim pkInstalledPrinters As String

        ' Find all printers installed
        For Each pkInstalledPrinters In
        PrinterSettings.InstalledPrinters
            ComboBox5.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        ' Set the combo to the first printer in the list
        ComboBox5.SelectedIndex = 0

        Label2.Text = user
        Dim w = Screen.PrimaryScreen.WorkingArea.Width
        Dim h = My.Computer.Screen.WorkingArea.Size.Height
        Me.Width = w
        Me.Height = h
        Me.Location = New Point(0, 0)
        adpt = New MySqlDataAdapter("select * from bcmnds WHERE Day(bcmnds.created_at) = @day and month(bcmnds.created_at)=@month and year(bcmnds.created_at)=@year order by id desc", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
        adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
        adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        For i = 0 To table.Rows.Count - 1



            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(6), table.Rows(i).Item(7))
        Next

        IconButton1.Text = Math.Round(sum, 2) & " Dhs"
        IconButton2.Text = table.Rows.Count


    End Sub
    Dim id As Integer
    Private Sub DataGridView1_CellMouseClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick



        If e.RowIndex >= 0 Then
            IconButton10.Visible = True
            IconButton17.Visible = True
            ComboBox5.Visible = True
            Label6.Visible = True
            DataGridView1.Visible = False
            Label5.Visible = True
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            id = row.Cells(0).Value.ToString
            Label5.Text = "Commande N° " & id
            adpt = New MySqlDataAdapter("select dasignation,qte from bcmnds_details where bcmnd_id = '" & id & "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            DataGridView2.Visible = True
            DataGridView2.Rows.Clear()
            For i = 0 To table.Rows.Count - 1
                DataGridView2.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), 0, 0)
            Next
        End If

    End Sub


    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click


        adpt = New MySqlDataAdapter("SELECT * FROM bcmnds WHERE created_at BETWEEN @datedebut AND @datefin order by id desc", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)


        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        DataGridView1.Rows.Clear()

        For i = 0 To table.Rows.Count - 1

            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(6), table.Rows(i).Item(7))
        Next
        IconButton1.Text = Math.Round(sum, 2) & " Dhs"
        IconButton2.Text = table.Rows.Count

    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        DataGridView1.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from bcmnds WHERE Day(bcmnds.created_at) = @day and month(bcmnds.created_at)=@month and year(bcmnds.created_at)=@year order by id desc", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
        adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
        adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        For i = 0 To table.Rows.Count - 1



            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(6), table.Rows(i).Item(7))

        Next

        IconButton1.Text = Math.Round(sum, 2) & " Dhs"
        IconButton2.Text = table.Rows.Count

        DateTimePicker1.Text = Today
        DateTimePicker2.Text = Today


    End Sub

    Private Sub IconButton16_Click(sender As Object, e As EventArgs) Handles IconButton16.Click

        Me.Close()

    End Sub

    Private Sub IconButton15_Click(sender As Object, e As EventArgs) Handles IconButton15.Click
        Home.Show()
        Me.Close()

    End Sub

    Private Sub IconButton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        stock.Show()
        Me.Close()

    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click
        dashboard.Show()
        dashboard.IconButton9.Visible = True
        dashboard.IconButton8.Visible = False
    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Show()

    End Sub

    Private Sub IconButton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
        Four.Show()
        Me.Close()

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)

    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Client.Show()
        Me.Close()
    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        devis.Show()
        Me.Close()
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        achats.Show()
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
    Dim m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Dim ReportToPrint As LocalReport
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)
        Return stream
    End Function
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
    Private Sub IconButton18_Click(sender As Object, e As EventArgs) Handles IconButton18.Click

        Dim dte As String = DateTimePicker1.Text & " - " & DateTimePicker2.Text
        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("Vente")
        For i = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(8).Value)
        Next
        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\VentEtatGlobal.rdlc"
        ReportToPrint.DataSources.Clear()
        Dim dateM As New ReportParameter("date", dte)
        Dim dateM1(0) As ReportParameter
        dateM1(0) = dateM
        ReportToPrint.SetParameters(dateM1)


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

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox2.Text = "" Then
                adpt = New MySqlDataAdapter("select * from bcmnds WHERE Day(bcmnds.created_at) = @day and month(bcmnds.created_at)=@month and year(bcmnds.created_at)=@year", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
                adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
                adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                For i = 0 To table.Rows.Count - 1



                    DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(6), table.Rows(i).Item(7))
                Next

                IconButton1.Text = Math.Round(sum, 2) & " Dhs"
                IconButton2.Text = table.Rows.Count

            Else
                adpt = New MySqlDataAdapter("select * from bcmnds WHERE id = @va", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@va", TextBox2.Text)

                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                DataGridView1.Rows.Clear()

                For i = 0 To table.Rows.Count - 1



                    DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(6), table.Rows(i).Item(7))
                Next

                IconButton1.Text = Math.Round(sum, 2) & " Dhs"
                IconButton2.Text = table.Rows.Count

            End If
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "" Then
                adpt = New MySqlDataAdapter("select * from orders WHERE Day(orders.OrderDate) = @day and month(orders.OrderDate)=@month and year(orders.OrderDate)=@year", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
                adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
                adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                DataGridView1.Rows.Clear()
                For i = 0 To table.Rows.Count - 1



                    DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(2) & " Dhs", table.Rows(i).Item(6) & " %", table.Rows(i).Item(4) & " Dhs", table.Rows(i).Item(13) & " Dhs", table.Rows(i).Item(1), table.Rows(i).Item(3), table.Rows(i).Item(10), table.Rows(i).Item(12))

                    sum = sum + table.Rows(i).Item(2)
                Next

                IconButton1.Text = Math.Round(sum, 2) & " Dhs"
                IconButton2.Text = table.Rows.Count

            Else
                adpt = New MySqlDataAdapter("select * from orders WHERE client LIKE @va AND OrderDate BETWEEN @datedebut AND @datefin", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@va", "%" + TextBox1.Text + "%")
                adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)

                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                DataGridView1.Rows.Clear()

                For i = 0 To table.Rows.Count - 1



                    DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(2) & " Dhs", table.Rows(i).Item(6) & " %", table.Rows(i).Item(4) & " Dhs", table.Rows(i).Item(13) & " Dhs", table.Rows(i).Item(1), table.Rows(i).Item(3), table.Rows(i).Item(10), table.Rows(i).Item(12))

                    sum = sum + table.Rows(i).Item(2)
                Next

                IconButton1.Text = Math.Round(sum, 2) & " Dhs"
                IconButton2.Text = table.Rows.Count

            End If
        End If
    End Sub

    Private Sub IconButton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        DataGridView2.Rows.Clear()
        DataGridView2.Visible = False
        DataGridView1.Visible = True
        IconButton10.Visible = False
        IconButton17.Visible = False
        ComboBox5.Visible = False
        Label6.Visible = False
        Label5.Visible = False
    End Sub

    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click

        Dim cmd As MySqlCommand
        Dim cmd2, cmd3 As MySqlCommand
        Dim sql As String
        Dim sql2, sql3 As String
        Dim execution As Integer

        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("tick")
        Dim sumqty As Double
        For i = 0 To DataGridView2.Rows.Count - 1
            dt.Rows.Add("", DataGridView2.Rows(i).Cells(0).Value, "", DataGridView2.Rows(i).Cells(1).Value, "", DataGridView2.Rows(i).Cells(2).Value, "", DataGridView2.Rows(i).Cells(3).Value)
            sumqty += DataGridView2.Rows(i).Cells(1).Value
        Next

        'ADPS = New MySqlDataAdapter("select * from num where id =" + (tc).ToString, conn2)


        'Try
        '    ADPS.Fill(tbl)
        '    ADPS.Dispose()
        '    ADPS = New MySqlDataAdapter("select * from tickets where tick =" + (tc).ToString, conn2)
        '    ADPS.Fill(tbl2)
        '    ADPS.Dispose()
        'Catch ex As Exception

        'End Try



        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\Report4.rdlc"
        ReportToPrint.DataSources.Clear()
        ReportToPrint.EnableExternalImages = True


        Dim lpar3 As New ReportParameter("totale", Label6.Text)
        Dim lpar31(0) As ReportParameter
                lpar31(0) = lpar3
        ReportToPrint.SetParameters(lpar31)

        Dim ide As New ReportParameter("id", id)
        Dim id1(0) As ReportParameter
                id1(0) = ide
                ReportToPrint.SetParameters(id1)

        Dim daty As New ReportParameter("date", DateTime.Now.ToString("yyyy-MM-dd"))
        Dim daty1(0) As ReportParameter
                daty1(0) = daty
                ReportToPrint.SetParameters(daty1)


        Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
                Dim table23 As New DataTable
                adpt23.Fill(table23)

                Dim info As New ReportParameter("info", table23.Rows(0).Item(1).ToString + vbCrLf + table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString)
                Dim info1(0) As ReportParameter
                info1(0) = info
        ReportToPrint.SetParameters(info1)

        Dim adpt2 As New MySqlDataAdapter("select name from bcmnds where id = '" & id & "' ", conn2)
        Dim table2 As New DataTable
        adpt2.Fill(table2)

        Dim client As New ReportParameter("client", table2.Rows(0).Item(0).ToString)
        Dim client1(0) As ReportParameter
        client1(0) = client
        ReportToPrint.SetParameters(client1)

        Dim msg As New ReportParameter("msg", table23.Rows(0).Item(6).ToString)
                Dim msg1(0) As ReportParameter
                msg1(0) = msg
                ReportToPrint.SetParameters(msg1)

        Dim count As New ReportParameter("count", DataGridView2.Rows.Count.ToString)
        Dim count1(0) As ReportParameter
                count1(0) = count
        ReportToPrint.SetParameters(count1)

        Dim qty As New ReportParameter("qty", sumqty.ToString)
                Dim qty1(0) As ReportParameter
                qty1(0) = qty
                ReportToPrint.SetParameters(qty1)


                ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

                'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
                'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
                'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
                'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
                Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.04985in</PageWidth><PageHeight>12in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
                Dim warnings As Warning()
                m_streams = New List(Of Stream)()

                ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)



                For Each stream As Stream In m_streams
                    stream.Position = 0
                Next

                Dim printDoc As New PrintDocument()
                'Microsoft Print To PDF
                'C80250 Series
                'C80250 Series init
                Dim printname As String = System.Configuration.ConfigurationSettings.AppSettings("printar")
                printDoc.PrinterSettings.PrinterName = ComboBox5.Text
                Dim ps As New PrinterSettings()
                ps.PrinterName = printDoc.PrinterSettings.PrinterName
                printDoc.PrinterSettings = ps

                AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
                m_currentPageIndex = 0
                printDoc.Print()

    End Sub

    Private Sub IconButton8_Click_1(sender As Object, e As EventArgs) Handles IconButton8.Click
        product.Show()
        Me.Close()
    End Sub


    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        users.Show()
        Me.Close()

    End Sub

    Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter

    End Sub

    Private Sub DataGridView2_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit
        Dim sum As Double = 0
        If e.RowIndex > -1 Then

            DataGridView2.Rows(e.RowIndex).Cells(3).Value = DataGridView2.Rows(e.RowIndex).Cells(1).Value * DataGridView2.Rows(e.RowIndex).Cells(2).Value
            For i = 0 To DataGridView2.Rows.Count - 1
                sum = sum + Convert.ToDouble(DataGridView2.Rows(i).Cells(3).Value)
            Next
        End If
        Label6.Text = sum & " Dhs"
    End Sub

End Class