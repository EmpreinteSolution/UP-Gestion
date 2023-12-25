Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Public Class listTrucks
    Dim adpt As MySqlDataAdapter
    Dim tbl As DataTable
    Dim cmd As MySqlCommand
    Dim tl9 As Boolean = False
    Private Sub listTrucks_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim pkInstalledPrinters As String

        ' Find all printers installed
        ComboBox5.Items.Clear()
        For Each pkInstalledPrinters In
        PrinterSettings.InstalledPrinters
            ComboBox5.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        ' Set the combo to the first printer in the list
        ComboBox5.SelectedIndex = 0

        adpt = New MySqlDataAdapter("SELECT `matricule` , `id` ,`active`,`password` FROM `camions` ", conn2)
        tbl = New DataTable
        adpt.Fill(tbl)

        ComboBox1.DataSource = tbl
        ComboBox1.DisplayMember = "matricule"
        ComboBox1.ValueMember = "id"
        For Each dt As DataRow In tbl.Rows
            Dim btn As New Button
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.Height = 40
            btn.Width = 80
            btn.Text = dt.Item(0)
            btn.Tag = dt.Item(1)
            btn.AccessibleName = dt.Item(2)
            btn.AccessibleDescription = dt.Item(3)
            btn.ForeColor = Color.White
            AddHandler btn.Click, AddressOf Me.btnClicked
            AddHandler btn.MouseDown, AddressOf Me.btnmouse
            If dt.Item(2) = 1 Then
                btn.BackColor = Color.DarkGreen
            Else
                btn.BackColor = Color.RosyBrown
            End If

            FlowLayoutPanel2.Controls.Add(btn)
        Next
        tl9 = True
    End Sub


    Protected Sub btnmouse(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            MsgBox(CType(sender, Button).AccessibleDescription.ToString)

        End If
    End Sub

    Protected Sub btnClicked(ByVal sender As Object, ByVal e As EventArgs)

        conn2.Open()

        If CType(sender, Button).AccessibleName = 1 Then
            CType(sender, Button).AccessibleName = 0
            CType(sender, Button).BackColor = Color.RosyBrown
            cmd = New MySqlCommand("UPDATE `camions` SET `active`= 0 WHERE `id` = " + CType(sender, Button).Tag.ToString, conn2)
            cmd.ExecuteNonQuery()
            Dim Rep As Integer
            Rep = MsgBox("Voulez-vous vraiment vider ce camion ?", vbYesNo)
            If Rep = vbYes Then
                adpt = New MySqlDataAdapter("SELECT `code_article`, `qte` FROM `camion_sous_stocks` WHERE `id_camion`  = " + CType(sender, Button).Tag.ToString, conn2)
                tbl = New DataTable
                adpt.Fill(tbl)
                cmd = New MySqlCommand("DELETE FROM `camion_sous_stocks` WHERE `id_camion` = " + CType(sender, Button).Tag.ToString, conn2)
                cmd.ExecuteNonQuery()
                For Each dt As DataRow In tbl.Rows
                    cmd = New MySqlCommand("UPDATE `article` SET `Stock` = `Stock` + @stck WHERE `Code` = @cod ", conn2)
                    cmd.Parameters.AddWithValue("@stck", dt.Item(1))
                    cmd.Parameters.AddWithValue("@cod", dt.Item(0))
                    cmd.ExecuteNonQuery()
                Next
            End If




        Else
            CType(sender, Button).AccessibleName = 1
            CType(sender, Button).BackColor = Color.DarkGreen
            cmd = New MySqlCommand("UPDATE `camions` SET `active`= 1 WHERE `id` = " + CType(sender, Button).Tag.ToString, conn2)

            cmd.ExecuteNonQuery()
            comionid = CType(sender, Button).Tag

            Addstock.Show()
        End If

        conn2.Close()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        conn2.Open()
        If tl9 = True Then
            DataGridView1.Rows.Clear()
            tbl = New DataTable
            adpt = New MySqlDataAdapter("SELECT  `code_article`, `article`, `prix_ttc`, `qte` FROM `camion_sous_stocks` WHERE `id_camion` = " + ComboBox1.SelectedValue.ToString, conn2)
            adpt.Fill(tbl)
            For Each dt As DataRow In tbl.Rows
                DataGridView1.Rows.Add(dt.Item(0), dt.Item(1), dt.Item(2), dt.Item(3), Nothing)
            Next


            cmd = New MySqlCommand("SELECT `prix` FROM `camions`  WHERE `id` = " + ComboBox1.SelectedValue.ToString, conn2)
            If cmd.ExecuteScalar = 1 Then

                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If

        End If
        conn2.Close()


    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        conn2.Close()
        conn2.Open()
        If CheckBox1.Checked = True Then
            cmd = New MySqlCommand("UPDATE `camions` SET `prix`= 1 WHERE `id` = " + ComboBox1.SelectedValue.ToString, conn2)
            cmd.ExecuteNonQuery()
        Else
            cmd = New MySqlCommand("UPDATE `camions` SET `prix`= 0 WHERE `id` = " + ComboBox1.SelectedValue.ToString, conn2)
            cmd.ExecuteNonQuery()
        End If
        conn2.Close()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

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
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Dim mat As String = ComboBox1.Text
        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("sstock")
        For i = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value)
        Next
        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\Sstock.rdlc"
        ReportToPrint.DataSources.Clear()
        Dim dateM As New ReportParameter("date", mat)
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

    Private Sub IconButton1_MouseClick(sender As Object, e As MouseEventArgs) Handles IconButton1.MouseClick

    End Sub

    Private Sub FlowLayoutPanel2_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel2.Paint

    End Sub
End Class