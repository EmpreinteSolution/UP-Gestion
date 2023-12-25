Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient

Imports System.Runtime.InteropServices
Public Class achatdetail
    Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter

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
        Dim cmd As MySqlCommand
        Dim cmd2, cmd3 As MySqlCommand
        Dim sql As String
        Dim sql2, sql3 As String
        Dim execution As Integer

        adpt = New MySqlDataAdapter("SELECT id from num ORDER BY id DESC LIMIT 1", conn2)
        Dim table As New DataTable
        adpt.Fill(table)



        ADPS = New MySqlDataAdapter("select * from num where id =" + Label2.Text.ToString, conn2)


        Try
            ADPS.Fill(tbl)
            ADPS.Dispose()
            ADPS = New MySqlDataAdapter("select * from tickets where tick =" + Label2.Text.ToString, conn2)
            ADPS.Fill(tbl2)
            ADPS.Dispose()
        Catch ex As Exception

        End Try

        '    ticketrep.Show()
        '    ticketrep.ReportViewer2.RefreshReport()



        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\Report4.rdlc"
        ReportToPrint.DataSources.Clear()

        Dim sum As Integer = tbl.Rows(0).Item(3)

        Dim lpar3 As New ReportParameter("totale", sum)
        Dim lpar31(0) As ReportParameter
        lpar31(0) = lpar3
        ReportToPrint.SetParameters(lpar31)
        'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
        ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
        ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
        ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>3.7in</PageWidth><PageHeight>12in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
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
        printDoc.PrinterSettings.PrinterName = "Microsoft Print To PDF"
        Dim ps As New PrinterSettings()
        ps.PrinterName = printDoc.PrinterSettings.PrinterName
        printDoc.PrinterSettings = ps

        AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
        m_currentPageIndex = 0
        printDoc.Print()
        tbl.Rows.Clear()
        tbl2.Rows.Clear()

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
    Private Sub achatdetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView3.Columns(3).DefaultCellStyle.Format = "N2"
        DataGridView3.Columns(4).DefaultCellStyle.Format = "N2"
        DataGridView3.Columns(5).DefaultCellStyle.Format = "N2"
        DataGridView3.Columns(6).DefaultCellStyle.Format = "N2"
        DataGridView3.Columns(7).DefaultCellStyle.Format = "N2"
        DataGridView3.Columns(8).DefaultCellStyle.Format = "N2"
        DataGridView3.Columns(9).DefaultCellStyle.Format = "N2"
        DataGridView3.Columns(10).DefaultCellStyle.Format = "N2"
    End Sub
    Dim ind As String

    Private Sub DataGridView3_CellMouseClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView3.CellMouseClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 11 Then
                Dim Rep As Integer
                Dim row As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
                Rep = MsgBox("Voulez-vous vraiment supprimer ce produit ?", vbYesNo)
                If Rep = vbYes Then


                    ind = row.Cells(0).Value.ToString


                    conn2.Open()
                    Dim sql2 As String
                    Dim cmd2 As MySqlCommand
                    sql2 = "delete from achatdetails where id = '" + ind + "' "
                    cmd2 = New MySqlCommand(sql2, conn2)

                    cmd2.ExecuteNonQuery()
                    conn2.Close()
                    DataGridView3.Rows.Clear()

                    conn2.Open()
                        Dim sql3 As String
                        Dim cmd3 As MySqlCommand
                    sql3 = "UPDATE `achat` SET montant = montant - @mnt  WHERE id = @id "
                    cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@id", Label2.Text.ToString)
                    cmd3.Parameters.AddWithValue("@mnt", Convert.ToDouble(row.Cells(10).Value))
                    cmd3.ExecuteNonQuery()

                    Dim sql4 As String
                        Dim cmd4 As MySqlCommand
                    sql4 = "UPDATE `article` SET `stock`= (stock - @stock) WHERE Code = @id "
                    cmd4 = New MySqlCommand(sql4, conn2)
                        cmd4.Parameters.Clear()
                        cmd4.Parameters.AddWithValue("@id", row.Cells(1).Value.ToString)
                    cmd4.Parameters.AddWithValue("@stock", row.Cells(9).Value)
                    cmd4.ExecuteNonQuery()

                        conn2.Close()
                    adpt = New MySqlDataAdapter("select * from achatdetails where achat_Id = '" + Label2.Text.ToString + "'", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    For i = 0 To table.Rows.Count - 1
                        DataGridView3.Rows.Add(table.Rows(i).Item(11), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(6), table.Rows(i).Item(7), table.Rows(i).Item(8), table.Rows(i).Item(9), table.Rows(i).Item(10), Nothing)
                    Next
                    MsgBox("Produit supprimé !")
                End If
            End If
        End If

    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Close()

    End Sub

    Public reps As Integer
    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        reps = 1
        Dim id = Label2.Text
        Dim Rep As Integer
        If reps = 1 Then
            Rep = MsgBox("Voulez-vous vraiment annuler cet achat ?", vbOK)
            If Rep = vbOK Then
                reps = 2
            End If
        End If
        If reps = 2 Then
            Rep = MsgBox("Cliquer OUI pour annuler cet achat ! ?", vbYesNo, MessageBoxDefaultButton.Button2)
        End If
        If Rep = vbYes Then
            reps = 1
            adpt = New MySqlDataAdapter("select CodeArticle,qte,id from achatdetails where achat_Id = '" + id + "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            For i = 0 To table.Rows.Count - 1

                adpt2 = New MySqlDataAdapter("select Stock from article where Code = '" + table.Rows(i).Item(0) + "'", conn2)
                Dim table2 As New DataTable
                Dim stock As String = table.Rows(i).Item(1)
                adpt2.Fill(table2)
                For j = 0 To table2.Rows.Count - 1
                    Dim news As String = table2.Rows(j).Item(0) - stock
                    conn2.Open()
                    Dim sql3, sql As String
                    Dim cmd3, cmd As MySqlCommand
                    sql3 = "UPDATE `article` SET `stock`= @stock WHERE Code = @id "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@id", table.Rows(i).Item(0))
                    cmd3.Parameters.AddWithValue("@stock", news)
                    cmd3.ExecuteNonQuery()

                    sql = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd = New MySqlCommand(sql, conn2)
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@name", achats.Label2.Text)
                    cmd.Parameters.AddWithValue("@op", "Annulation d'achat N° " & Label2.Text)
                    cmd.ExecuteNonQuery()

                    conn2.Close()

                Next
                conn2.Open()
                Dim sql2 As String
                Dim cmd2 As MySqlCommand

                sql2 = "delete from achatdetails where id = " + table.Rows(i).Item(2).ToString
                cmd2 = New MySqlCommand(sql2, conn2)

                cmd2.ExecuteNonQuery()
                conn2.Close()
            Next

            conn2.Open()
            Dim sql4 As String
            Dim cmd4 As MySqlCommand
            sql4 = "delete from achat where id = '" & id.ToString & "'"
            cmd4 = New MySqlCommand(sql4, conn2)

            cmd4.ExecuteNonQuery()
            conn2.Close()

            Me.Close()

        End If
    End Sub
End Class
