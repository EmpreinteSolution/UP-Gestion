Imports MySql.Data.MySqlClient
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms

Imports System.Runtime.InteropServices
Imports ClosedXML.Excel
Imports System.ComponentModel

Public Class stock
    Dim ReportToPrint As LocalReport
    Dim m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    'Dim conn2 As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub

    Dim N As Integer = 0



    Sub stockthread()

    End Sub
    Dim pagepv As Integer = 0
    Dim pagenx As Integer = 0

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
    Private Sub stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select stock_fac, balance from parameters", conn2)
        Dim params As New DataTable
        adpt.Fill(params)

        If params.Rows(0).Item(0) = "Desactive" Then
            IconButton33.Visible = False
            DataGridView1.Columns(9).Visible = False
        End If

        If params.Rows(0).Item(1) = "Desactive" Then

            IconButton20.Visible = False
            TextBox1.Visible = False

        End If

        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Dim appPath As String = Application.StartupPath()
        TextBox3.Select()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = System.IO.Path.Combine(SaveDirectory, tableimg.Rows(0).Item(11))
        If System.IO.File.Exists(SavePath) Then
            PictureBox2.Image = Image.FromFile(SavePath)
        End If

        Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))

        Label2.Text = user
        Dim w = Screen.PrimaryScreen.WorkingArea.Width
        Dim h = My.Computer.Screen.WorkingArea.Size.Height
        Me.Width = w
        Me.Height = h
        Me.Location = New Point(0, 0)
        adpt = New MySqlDataAdapter("select path from infos", conn2)
        Dim tableinfo As New DataTable
        adpt.Fill(tableinfo)
        TextBox1.Text = tableinfo.Rows(0).Item(0).ToString.Replace("/", "\")







        adpt = New MySqlDataAdapter("select * from article where id > '" & pagenx & "' order by id asc limit 100", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        For i = 0 To table.Rows.Count - 1

            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(8), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(3)).ToString("N0") & "%", Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(5) & "%", Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(25).ToString.Replace(".", ",")).ToString("N2"))
            pagenx = table.Rows(i).Item(16)
            pagepv = table.Rows(i).Item(16)
        Next
        adpt = New MySqlDataAdapter("select * from article", conn2)
        Dim tablecal As New DataTable
        adpt.Fill(tablecal)
        Dim pv, pa As Double
        For i = 0 To tablecal.Rows.Count() - 1
            pv += Convert.ToDouble(tablecal.Rows(i).Item(4).ToString.Replace(".", ",")) * Convert.ToDouble(tablecal.Rows(i).Item(8).ToString.Replace(".", ","))
            pa += Convert.ToDouble(tablecal.Rows(i).Item(2).ToString.Replace(".", ",")) * Convert.ToDouble(tablecal.Rows(i).Item(8).ToString.Replace(".", ","))
        Next
        Label5.Text = Convert.ToDouble(pv).ToString("N2") & " DHs"
        Label6.Text = Convert.ToDouble(pa).ToString("N2") & " DHs"
        Label7.Text = tablecal.Rows.Count().ToString("N2")

        adpt = New MySqlDataAdapter("select name from fours order by name ASC", conn2)
        Dim tablefour As New DataTable
        adpt.Fill(tablefour)
        For i = 0 To tablefour.Rows.Count - 1
            ComboBox2.Items.Add(tablefour.Rows(i).Item(0))
        Next

        adpt = New MySqlDataAdapter("select nomFamille from famille order by nomFamille ASC", conn2)
        Dim tablefam As New DataTable
        adpt.Fill(tablefam)
        For i = 0 To tablefam.Rows.Count - 1
            ComboBox4.Items.Add(tablefam.Rows(i).Item(0))
        Next
        KeyPreview = True
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs)
        Dim Rep As Integer
        Rep = MsgBox("Voulez-vous vraiment quitter ?", vbYesNo)
        If Rep = vbYes Then
            Dim log = New Form1()
            log.Show()
            Me.Close()
        End If
    End Sub
    Dim ind As String
    Dim imgName As String



    Private Sub IconButton8_Click(sender As Object, e As EventArgs) Handles IconButton8.Click
        Dim Rep As Integer
        Rep = MsgBox("Voulez-vous vraiment quitter ?", vbYesNo)
        If Rep = vbYes Then
            Dim log = New Form1()
            log.Show()
            Me.Close()
        End If
    End Sub



    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Dim DT As New DataTable
        For i = 0 To DataGridView1.Columns.Count - 1
            Dim dc As New DataColumn
            dc.ColumnName = DataGridView1.Columns(i).HeaderText
            DT.Columns.Add(dc)
        Next
        Dim cell = New Object(DataGridView1.Columns.Count - 1) {}

        For Each dataGridViewRow As DataGridViewRow In DataGridView1.Rows

            For i As Integer = 0 To dataGridViewRow.Cells.Count - 1
                cell(i) = dataGridViewRow.Cells(i).Value
            Next

            DT.Rows.Add(cell)
        Next
        Using sfd As SaveFileDialog = New SaveFileDialog() With {.FileName = "Stock.xlsx"}
            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    Using workbook As XLWorkbook = New XLWorkbook()
                        workbook.Worksheets.Add(DT, "STOCK")
                        workbook.SaveAs(sfd.FileName)
                    End Using
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End Using
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text = "Code ou désignation" Or ComboBox1.Text = "Quantité" Then
            ComboBox3.Visible = False
            TextBox3.Visible = True
        End If

        If ComboBox1.Text = "Familles" Then

            ComboBox3.Visible = True
            TextBox3.Visible = False

            adpt = New MySqlDataAdapter("select * from famille order by nomFamille", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            ComboBox3.DataSource = table
            ComboBox3.DisplayMember = "nomFamille"
            ComboBox3.ValueMember = "idFamille"

        End If

        If ComboBox1.Text = "Sous-familles" Then

            ComboBox3.Visible = True
            TextBox3.Visible = False

            adpt = New MySqlDataAdapter("select * from sous_famille order by nomSFamille", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            ComboBox3.DataSource = table
            ComboBox3.DisplayMember = "nomSfamille"
            ComboBox3.ValueMember = "idSFamille"

        End If
    End Sub


    Sub ComboSearch()



    End Sub












    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        'Dim t1 As New System.Threading.Thread(AddressOf ComboSearch
        '                                 )
        'CheckForIllegalCrossThreadCalls = False

        't1.Start()

        If ComboBox1.Text = "Familles" Then
            adpt = New MySqlDataAdapter("select * from article where idFamille = '" + ComboBox3.SelectedValue.ToString + "' order by Article", conn2)
        End If
        If ComboBox1.Text = "Sous-familles" Then
            adpt = New MySqlDataAdapter("select * from article where idSFamille = '" + ComboBox3.SelectedValue.ToString + "' order by Article", conn2)
        End If
        Dim table As New DataTable
        conn2.Close()
        adpt.Fill(table)

        DataGridView1.Rows.Clear()
        For i = 0 To table.Rows.Count - 1
            Dim cmd As New MySqlCommand("select `nomFamille` from famille where idFamille = @id", conn2)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@id", table.Rows(i).Item(11).ToString)
            cmd.Connection.Open()
            Dim fa As String = cmd.ExecuteScalar()
            conn2.Close()

            Dim cmd2 As New MySqlCommand("select `nomSFamille` from sous_famille where idSFamille = @id", conn2)
            cmd2.Parameters.Clear()
            cmd2.Parameters.AddWithValue("@id", table.Rows(i).Item(12).ToString)
            cmd2.Connection.Open()
            Dim sous As String = cmd2.ExecuteScalar()
            conn2.Close()
            DataGridView1.Rows.Add("", fa, sous, table.Rows(i).Item(1), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(7), table.Rows(i).Item(13), table.Rows(i).Item(8), table.Rows(i).Item(9), table.Rows(i).Item(10), table.Rows(i).Item(14), table.Rows(i).Item(0))
        Next


    End Sub

    Private Sub IconButton16_Click(sender As Object, e As EventArgs) Handles IconButton16.Click
        Dim ad As New MySqlDataAdapter

        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("stock")

        ad = New MySqlDataAdapter("SELECT `Code`, `Article` , `PA_TTC` , `PV_TTC` , `unit` , `Stock` FROM `article` order by `Article` limit 20", conn2)
        Dim dtl As New DataTable
        ad.Fill(dtl)

        For j = 0 To dtl.Rows.Count - 1
            dt.Rows.Add(dtl.Rows(j).Item(0), dtl.Rows(j).Item(1), dtl.Rows(j).Item(2), dtl.Rows(j).Item(3), dtl.Rows(j).Item(4), dtl.Rows(j).Item(5))
        Next
        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\Report3.rdlc"
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
        'Microsoft Print To PDF
        'C802100 Series
        'C802100 Series init
        printDoc.PrinterSettings.PrinterName = ComboBox2.Text
        Dim ps As New PrinterSettings()
        ps.PrinterName = printDoc.PrinterSettings.PrinterName
        printDoc.PrinterSettings = ps

        AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
        m_currentPageIndex = 0
        printDoc.Print()
        dt.Clear()




    End Sub
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

    Private Sub IconButton14_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged, TextBox2.TextChanged
        'DataGridView1.Rows.Clear()
        'If TextBox3.Text = "" Then
        'Else
        '    If ComboBox1.Text = "Code ou désignation" Then
        '        adpt = New MySqlDataAdapter("select * from article where Article = '" + TextBox3.Text + "' OR Code = '" + TextBox3.Text + "'  order by Article", conn2)
        '    End If
        '    If ComboBox1.Text = "Quantité" Then
        '        adpt = New MySqlDataAdapter("select * from article where Stock = " + TextBox3.Text + " order by Article", conn2)
        '    End If
        '    Dim table As New DataTable
        '    adpt.Fill(table)
        '    DataGridView1.Rows.Clear()
        '    For i = 0 To table.Rows.Count - 1
        '        Dim cmd As New MySqlCommand("select `nomFamille` from famille where idFamille = @id", conn2)
        '        cmd.Parameters.Clear()
        '        cmd.Parameters.AddWithValue("@id", table.Rows(i).Item(11).ToString)
        '        cmd.Connection.Open()
        '        Dim fa As String = cmd.ExecuteScalar()
        '        conn2.Close()

        '        Dim cmd2 As New MySqlCommand("select `nomSFamille` from sous_famille where idSFamille = @id", conn2)
        '        cmd2.Parameters.Clear()
        '        cmd2.Parameters.AddWithValue("@id", table.Rows(i).Item(12).ToString)
        '        cmd2.Connection.Open()
        '        Dim sous As String = cmd2.ExecuteScalar()
        '        conn2.Close()
        '        DataGridView1.Rows.Add(table.Rows(i).Item(0), fa, sous, table.Rows(i).Item(1), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(7), table.Rows(i).Item(13), table.Rows(i).Item(8), table.Rows(i).Item(9), table.Rows(i).Item(10), "", table.Rows(i).Item(14))
        '    Next

        '    Dim pv, pa As Double
        '    For i = 0 To DataGridView1.Rows.Count - 1
        '        pv = pv + DataGridView1.Rows(i).Cells(6).Value.ToString.Replace(".", ",") * DataGridView1.Rows(i).Cells(8).Value.ToString.Replace(".", ",")
        '        pa = pa + DataGridView1.Rows(i).Cells(4).Value.ToString.Replace(".", ",") * DataGridView1.Rows(i).Cells(8).Value.ToString.Replace(".", ",")
        '    Next
        '    Label5.Text = pv
        '    Label6.Text = pa
        ' End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        adpt2 = New MySqlDataAdapter("SELECT * FROM article where sell = 0 and REPLACE(Stock,',','.') >= 0", conn2)
        Dim tablecountstock As New DataTable
        adpt2.Fill(tablecountstock)
        Dim count As Integer = 0
        Dim ttc As Double = 0

        For i = 0 To tablecountstock.Rows.Count - 1
            'adpt2 = New MySqlDataAdapter("SELECT * FROM orderdetails where ProductID = '" & tablecountstock.Rows(i).Item(0) & "' limit 1", conn2)
            'Dim tablecountstock2 As New DataTable
            'adpt2.Fill(tablecountstock2)
            count = i
            'Dim cmd3 As MySqlCommand

            'conn2.Open()
            'cmd3 = New MySqlCommand("UPDATE `article` SET `sell` = 1  WHERE Code = '" + tablecountstock.Rows(i).Item(0) + "'", conn2)
            'cmd3.ExecuteNonQuery()
            'conn2.Close()
            ttc = ttc + Convert.ToDouble(tablecountstock.Rows(i).Item(4).ToString.Replace(".", ",").Replace(" ", "") * tablecountstock.Rows(i).Item(8).ToString.Replace(".", ",").Replace(" ", ""))



            Me.Invoke(Sub()

                          DataGridView1.Rows.Add(tablecountstock.Rows(i).Item(0), tablecountstock.Rows(i).Item(1), tablecountstock.Rows(i).Item(8), tablecountstock.Rows(i).Item(2), Convert.ToDouble(tablecountstock.Rows(i).Item(3)).ToString("N0") & "%", Convert.ToDouble(tablecountstock.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2"), tablecountstock.Rows(i).Item(5) & "%", Convert.ToDouble(tablecountstock.Rows(i).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(tablecountstock.Rows(i).Item(7).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(tablecountstock.Rows(i).Item(25).ToString.Replace(".", ",")).ToString("N2"))



                          Panel8.Width = CInt((count / tablecountstock.Rows.Count) * 546)
                          Label16.Text = Convert.ToDouble(((count + 1) / tablecountstock.Rows.Count) * 100).ToString("N0") & "%"
                          Label15.Text = "Filtrage du stock en cours..."
                      End Sub)
        Next
        Me.Invoke(Sub()
                      Panel6.Visible = False
                      TextBox20.Text = ttc.ToString("N2")

                  End Sub)
    End Sub




    Private Sub IconPictureBox3_Click(sender As Object, e As EventArgs) Handles IconPictureBox3.Click

    End Sub







    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        zgaa.Close()
    End Sub


    Private Sub IconButton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        dashboard.Show()
        dashboard.IconButton9.Visible = True
        dashboard.IconButton8.Visible = False
    End Sub
    Private Sub iconbutton5_Click_1(sender As Object, e As EventArgs) Handles IconButton5.Click
        product.Show()
        Me.Close()
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Client.Show()
        Me.Close()
    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click
        achats.Show()
        Me.Close()
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        devis.Show()
        Me.Close()
    End Sub
    Private Sub IconButton23_Click(sender As Object, e As EventArgs) Handles IconButton23.Click
        Charges.Show()
        Me.Close()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        banques.Show()
        Me.Close()
    End Sub
    Private Sub IconButton18_Click(sender As Object, e As EventArgs) Handles IconButton18.Click
        factures.Show()
        Me.Close()
    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        archive.Show()
        Me.Close()

    End Sub

    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        Home.Show()
        Me.Close()
    End Sub

    Private Sub IconButton19_Click(sender As Object, e As EventArgs) Handles IconButton19.Click
        Dim path As String = TextBox1.Text & "\orden.dat"

        ' Create or overwrite the file.
        Dim fs As FileStream = File.Create(path)
        adpt = New MySqlDataAdapter("select * from article where plu <> 0 order by id", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        ' Add text to the file.
        For i = 0 To table.Rows.Count - 1
            Dim info As Byte() = New UTF8Encoding(True).GetBytes("260" & table.Rows(i).Item(18) & "EEEEE|" & 1 & " |" & table.Rows(i).Item(18) & "|" & "   |" & "    |" & Convert.ToString(table.Rows(i).Item(7)).Replace(",", "").Replace(".", "") & "|" & "W|" & table.Rows(i).Item(19) & "|" & 0 & "|" & table.Rows(i).Item(1) & vbTab & "|     |" & vbCrLf)
            fs.Write(info, 0, info.Length)
        Next
        fs.Close()
        MsgBox("Terminé !")
    End Sub

    Private Sub FolderBrowserDialog1_HelpRequest(sender As Object, e As EventArgs) Handles FolderBrowserDialog1.HelpRequest

    End Sub
    Dim sql2 As String
    Dim cmd2 As MySqlCommand
    Private Sub IconButton20_Click(sender As Object, e As EventArgs) Handles IconButton20.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
            conn2.Close()
            conn2.Open()
            sql2 = "UPDATE `infos` SET `path`='" & TextBox1.Text.ToString().Replace("\", "/") & "' WHERE id = 1 "
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()

            conn2.Close()

            Dim path As String = TextBox1.Text & "\orden.dat"

            ' Create or overwrite the file.
            Dim fs As FileStream = File.Create(path)
            adpt = New MySqlDataAdapter("select * from article where plu <> 0 order by id", conn2)
            Dim table As New DataTable
            adpt.Fill(table)

            ' Add text to the file.
            For i = 0 To table.Rows.Count - 1
                Dim info As Byte() = New UTF8Encoding(True).GetBytes("260" & table.Rows(i).Item(18).ToString.PadLeft(4, "0"c) & "EEEEE|" & 1 & " |" & table.Rows(i).Item(18).ToString.PadLeft(4, "0"c) & "|" & "   |" & "    |" & Convert.ToString(Convert.ToDouble(table.Rows(i).Item(7)).ToString("N2")).Replace(".", "").Replace(",", "").Replace(" ", "").PadLeft(7, " "c) & "|" & "W|" & Convert.ToString(table.Rows(i).Item(19)).PadLeft(3, " "c) & "|" & 0 & "|" & Convert.ToString(table.Rows(i).Item(1)).PadRight(25, " "c) & "|     |" & vbCrLf)
                fs.Write(info, 0, info.Length)
            Next
            fs.Close()
            MsgBox("Terminé !")
        End If
    End Sub

    Private Sub IconButton21_Click(sender As Object, e As EventArgs) Handles IconButton21.Click
        balisage.ShowDialog()
    End Sub

    Private Sub IconButton22_Click(sender As Object, e As EventArgs) Handles IconButton22.Click
        simpleajout.Show()
    End Sub



    Private Sub IconButton26_Click(sender As Object, e As EventArgs) Handles IconButton26.Click
        adpt = New MySqlDataAdapter("select * from article where id > 0 order by id asc limit 100", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        DataGridView1.Rows.Clear()

        For i = 0 To table.Rows.Count - 1

            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(8), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(3)).ToString("N0") & "%", Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(5) & "%", Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(25).ToString.Replace(".", ",")).ToString("N2"))
        Next
        Label9.Text = 1

    End Sub

    Private Sub IconButton27_Click(sender As Object, e As EventArgs) Handles IconButton27.Click
        adpt2 = New MySqlDataAdapter("select * from article order by id desc", conn2)
        Dim tablecountstock As New DataTable
        adpt2.Fill(tablecountstock)

        DataGridView1.Rows.Clear()

        For i = 0 To tablecountstock.Rows.Count - 1
            If IsDBNull(tablecountstock.Rows(i).Item(1)) Or tablecountstock.Rows(i).Item(1) = "Aucun" Or tablecountstock.Rows(i).Item(1) = "-" Or IsDBNull(tablecountstock.Rows(i).Item(0)) Or Convert.ToDouble(tablecountstock.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")) <= 0 Or Convert.ToDouble(tablecountstock.Rows(i).Item(4).ToString.Replace(" ", "").Replace(".", ",")) <= 0 Or Convert.ToDouble(tablecountstock.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")) <= 0 Or Convert.ToDouble(tablecountstock.Rows(i).Item(6).ToString.Replace(" ", "").Replace(".", ",")) <= 0 Or Convert.ToDouble(tablecountstock.Rows(i).Item(7).ToString.Replace(" ", "").Replace(".", ",")) <= 0 Or Convert.ToDouble(tablecountstock.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")) > Convert.ToDouble(tablecountstock.Rows(i).Item(6).ToString.Replace(" ", "").Replace(".", ",")) Or Convert.ToDouble(tablecountstock.Rows(i).Item(4).ToString.Replace(" ", "").Replace(".", ",")) > Convert.ToDouble(tablecountstock.Rows(i).Item(7).ToString.Replace(" ", "").Replace(".", ",")) Then

                DataGridView1.Rows.Add(tablecountstock.Rows(i).Item(0), tablecountstock.Rows(i).Item(1), tablecountstock.Rows(i).Item(8), tablecountstock.Rows(i).Item(2), Convert.ToDouble(tablecountstock.Rows(i).Item(3)).ToString("N0") & "%", Convert.ToDouble(tablecountstock.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2"), tablecountstock.Rows(i).Item(5) & "%", Convert.ToDouble(tablecountstock.Rows(i).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(tablecountstock.Rows(i).Item(7).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(tablecountstock.Rows(i).Item(25).ToString.Replace(".", ",")).ToString("N2"))
            End If
        Next
    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Four.Show()
        Me.Close()

    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        users.Show()
        Me.Close()

    End Sub

    Private Sub IconButton25_Click(sender As Object, e As EventArgs) Handles IconButton25.Click
        adpt = New MySqlDataAdapter("select * from article where id > '" & pagenx & "' order by id asc limit 100", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        If table.Rows.Count <> 0 Then
            pagepv = pagenx
            DataGridView1.Rows.Clear()

            For i = 0 To table.Rows.Count - 1

                DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(8), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(3)).ToString("N0") & "%", Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(5) & "%", Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(25).ToString.Replace(".", ",")).ToString("N2"))
                pagenx = table.Rows(i).Item(16)
            Next
            Label9.Text = Label9.Text + 1
        End If

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub IconButton30_Click(sender As Object, e As EventArgs) Handles IconButton30.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Supprimer un article'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then


                Dim cmd As MySqlCommand
        Dim sql As String
        If DataGridView1.SelectedRows.Count <> 0 Then
            For i = 0 To DataGridView1.SelectedRows.Count - 1
                adpt = New MySqlDataAdapter("select * from orderdetails where ProductID = '" + DataGridView1.SelectedRows(i).Cells(0).Value + "'", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    Dim Rep As Integer
                    Rep = MsgBox("Se produit est déjà passé dans plusieurs commandes ! Si vous le supprimez vous allez rencontrer des problèmes au niveaux des ventes !" & vbCrLf & "Voulez vous vraiment le supprimer ?", vbYesNo)
                    If Rep = vbYes Then
                        conn2.Open()
                        cmd = New MySqlCommand("DELETE FROM `article` WHERE Code = " + DataGridView1.SelectedRows(i).Cells(0).Value, conn2)
                        cmd.ExecuteNonQuery()
                        conn2.Close()
                        conn2.Open()
                        sql = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                        cmd = New MySqlCommand(sql, conn2)
                        cmd.Parameters.Clear()
                        cmd.Parameters.AddWithValue("@name", user)
                        cmd.Parameters.AddWithValue("@op", "Suppression de l'Article " & DataGridView1.SelectedRows(i).Cells(1).Value)
                        cmd.ExecuteNonQuery()
                        conn2.Close()
                        MsgBox("Opération bien faite !")
                        DataGridView1.Rows.RemoveAt(DataGridView1.SelectedRows(i).Index)
                    End If
                Else
                    adpt = New MySqlDataAdapter("select * from achatdetails where CodeArticle = '" + DataGridView1.SelectedRows(i).Cells(0).Value + "'", conn2)
                    Dim table2 As New DataTable
                    adpt.Fill(table2)
                    If table2.Rows.Count() <> 0 Then
                        Dim Rep As Integer
                        Rep = MsgBox("Se produit est déjà passé dans plusieurs réceptions ! Si vous le supprimez vous allez rencontrer des problèmes au niveaux des achats !" & vbCrLf & "Voulez vous vraiment le supprimer ?", vbYesNo)
                        If Rep = vbYes Then
                            conn2.Open()
                            cmd = New MySqlCommand("DELETE FROM `article` WHERE Code = " + DataGridView1.SelectedRows(i).Cells(0).Value, conn2)
                            cmd.ExecuteNonQuery()
                            conn2.Close()
                            conn2.Open()
                            sql = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                            cmd = New MySqlCommand(sql, conn2)
                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@name", user)
                            cmd.Parameters.AddWithValue("@op", "Suppression de l'Article " & DataGridView1.SelectedRows(i).Cells(1).Value)
                            cmd.ExecuteNonQuery()
                            conn2.Close()
                            MsgBox("Opération bien faite !")
                            DataGridView1.Rows.RemoveAt(DataGridView1.SelectedRows(i).Index)
                        End If
                    Else

                        Dim Rep As Integer
                        Rep = MsgBox("Voulez vous vraiment supprimer ce produit ?", vbYesNo)
                        If Rep = vbYes Then
                            conn2.Open()
                            cmd = New MySqlCommand("DELETE FROM `article` WHERE Code = '" + DataGridView1.SelectedRows(i).Cells(0).Value + "'", conn2)
                            cmd.ExecuteNonQuery()
                            conn2.Close()
                            conn2.Open()
                            sql = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                            cmd = New MySqlCommand(sql, conn2)
                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@name", user)
                            cmd.Parameters.AddWithValue("@op", "Suppression de l'Article " & DataGridView1.SelectedRows(i).Cells(1).Value)
                            cmd.ExecuteNonQuery()
                            conn2.Close()
                            MsgBox("Opération bien faite !")
                            DataGridView1.Rows.RemoveAt(DataGridView1.SelectedRows(i).Index)
                        End If
                    End If
                End If
            Next
        End If

            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If
    End Sub


    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Enter Then

            If TextBox4.Text <> "" Then


                adpt = New MySqlDataAdapter("select * from article where Article like '%" + TextBox4.Text.Replace("'", " ") + "%' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    DataGridView1.Rows.Clear()
                    For i = 0 To table.Rows.Count - 1

                        DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(8), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(3)).ToString("N0") & "%", Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(5) & "%", Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(25).ToString.Replace(".", ",")).ToString("N2"))
                    Next
                    TextBox4.Text = ""
                Else
                    MsgBox("Produit introuvable !")
                    TextBox4.Text = ""
                End If


            Else
                TextBox4.Text = ""

            End If

        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        Dim inputText As String = TextBox4.Text

        ' Vérifiez si le texte est vide ou nul
        If Not String.IsNullOrEmpty(inputText) Then
            ' Divisez le texte en mots en utilisant des espaces comme séparateurs
            Dim words As String() = inputText.Split(New Char() {" "c}, StringSplitOptions.None)

            ' Convertissez la première lettre de chaque mot en majuscule
            For i As Integer = 0 To words.Length - 1
                If words(i).Length > 0 Then
                    words(i) = words(i).Substring(0, 1).ToUpper() & words(i).Substring(1).ToLower()
                End If
            Next

            ' Recréez le texte modifié avec des espaces entre les mots
            Dim modifiedText As String = String.Join(" ", words)

            ' Désactivez temporairement le gestionnaire d'événements TextChanged
            RemoveHandler TextBox4.TextChanged, AddressOf TextBox4_TextChanged

            ' Mettez à jour le texte dans le TextBox
            TextBox4.Text = modifiedText

            ' Replacez le curseur à la position correcte après la modification
            TextBox4.SelectionStart = TextBox4.Text.Length

            ' Réactivez le gestionnaire d'événements TextChanged
            AddHandler TextBox4.TextChanged, AddressOf TextBox4_TextChanged
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        adpt = New MySqlDataAdapter("select * from article where Fournisseur = '" + ComboBox2.Text + "' ", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim ttc As Double = 0

        If table.Rows.Count() <> 0 Then
            DataGridView1.Rows.Clear()
            For i = 0 To table.Rows.Count - 1
                ttc = ttc + Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ",").Replace(" ", "") * table.Rows(i).Item(8).ToString.Replace(".", ",").Replace(" ", ""))


                DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(8), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(3)).ToString("N0") & "%", Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(5) & "%", Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(25).ToString.Replace(".", ",")).ToString("N2"))
            Next
            TextBox4.Text = ""

        Else
            MsgBox("Aucun produit trouvé dans le stock de ce fournisseur !")
            TextBox4.Text = ""
        End If
        TextBox20.Text = ttc.ToString("N2")

    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        adpt = New MySqlDataAdapter("select * from article where idFamille = '" + ComboBox4.Text + "' ", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim ttc As Double = 0
        If table.Rows.Count() <> 0 Then
            DataGridView1.Rows.Clear()
            For i = 0 To table.Rows.Count - 1
                ttc = ttc + Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ",").Replace(" ", "") * table.Rows(i).Item(8).ToString.Replace(".", ",").Replace(" ", ""))

                DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(8), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(3)).ToString("N0") & "%", Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(5) & "%", Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(25).ToString.Replace(".", ",")).ToString("N2"))
            Next
            TextBox4.Text = ""
        Else
            MsgBox("Aucun produit trouvé dans le stock de cette famille !")
            TextBox4.Text = ""
        End If
        TextBox20.Text = ttc.ToString("N2")

    End Sub

    Private Sub IconButton28_Click(sender As Object, e As EventArgs) Handles IconButton28.Click
        statistique.Show()
        statistique.Panel10.Visible = True
        statistique.Label29.Text = DataGridView1.SelectedRows(0).Cells(0).Value
        statistique.Label7.Text = DataGridView1.SelectedRows(0).Cells(1).Value
        statistique.TextBox4.Text = DataGridView1.SelectedRows(0).Cells(2).Value
        statistique.IconButton2.PerformClick()
        statistique.Label16.Text = "Statistiques du produit"
    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Text <> "" Then

                Dim codebar As String
                If TextBox3.Text.Length.ToString >= 3 AndAlso IsNumeric(TextBox3.Text) Then
                    If TextBox3.Text.Chars(0) & TextBox3.Text.Chars(1) & TextBox3.Text.Chars(2) = 260 Then
                        codebar = 260 & TextBox3.Text.Chars(3) & TextBox3.Text.Chars(4) & TextBox3.Text.Chars(5) & TextBox3.Text.Chars(6)
                    Else
                        codebar = TextBox3.Text
                    End If
                Else
                    codebar = TextBox3.Text
                End If





                adpt = New MySqlDataAdapter("select prod_code from code_supp where code_supp = '" & codebar & "' ", conn2)
                Dim table2 As New DataTable
                adpt.Fill(table2)
                If table2.Rows.Count() = 0 Then
                    adpt = New MySqlDataAdapter("select * from article where Code = '" & codebar & "'", conn2)
                    Dim table3 As New DataTable
                    adpt.Fill(table3)
                    If table3.Rows.Count() <> 0 Then
                        DataGridView1.Rows.Clear()

                        For i = 0 To table3.Rows.Count - 1

                            DataGridView1.Rows.Add(table3.Rows(i).Item(0), table3.Rows(i).Item(1), table3.Rows(i).Item(8), table3.Rows(i).Item(2), Convert.ToDouble(table3.Rows(i).Item(3)).ToString("N0") & "%", Convert.ToDouble(table3.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2"), table3.Rows(i).Item(5) & "%", Convert.ToDouble(table3.Rows(i).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table3.Rows(i).Item(7).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table3.Rows(i).Item(25).ToString.Replace(".", ",")).ToString("N2"))
                        Next
                        TextBox3.Text = ""
                    Else
                        MsgBox("Produit introuvable !")
                        TextBox3.Text = ""
                    End If
                Else

                    adpt = New MySqlDataAdapter("select * from article where Code = '" + table2.Rows(0).Item(0) + "' ", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    If table.Rows.Count() <> 0 Then
                        DataGridView1.Rows.Clear()

                        For i = 0 To table.Rows.Count - 1

                            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(8), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(3)).ToString("N0") & "%", Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(5) & "%", Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(25).ToString.Replace(".", ",")).ToString("N2"))
                        Next
                        TextBox3.Text = ""

                    Else
                        MsgBox("Produit introuvable !")
                        TextBox3.Text = ""
                    End If
                End If


            Else
                TextBox3.Text = ""

            End If

        End If
    End Sub

    Private Sub IconButton29_Click(sender As Object, e As EventArgs) Handles IconButton29.Click
        adpt2 = New MySqlDataAdapter("select * from article order by id desc", conn2)
        Dim tablecountstock As New DataTable
        adpt2.Fill(tablecountstock)

        DataGridView1.Rows.Clear()
        Dim ttc As Double = 0
        For i = 0 To tablecountstock.Rows.Count - 1
            If tablecountstock.Rows(i).Item(8) < 0 Then

                DataGridView1.Rows.Add(tablecountstock.Rows(i).Item(0), tablecountstock.Rows(i).Item(1), tablecountstock.Rows(i).Item(8), tablecountstock.Rows(i).Item(2), Convert.ToDouble(tablecountstock.Rows(i).Item(3)).ToString("N0") & "%", Convert.ToDouble(tablecountstock.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2"), tablecountstock.Rows(i).Item(5) & "%", Convert.ToDouble(tablecountstock.Rows(i).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(tablecountstock.Rows(i).Item(7).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(tablecountstock.Rows(i).Item(25).ToString.Replace(".", ",")).ToString("N2"))
                ttc = ttc + Convert.ToDouble(tablecountstock.Rows(i).Item(4).ToString.Replace(".", ",").Replace(" ", "") * tablecountstock.Rows(i).Item(8).ToString.Replace(".", ",").Replace(" ", ""))
            End If
        Next
        TextBox20.Text = ttc.ToString("N2")
    End Sub

    Private Sub IconButton31_Click(sender As Object, e As EventArgs) Handles IconButton31.Click
        Panel6.Visible = True
        DataGridView1.Rows.Clear()

        ' Lancer le BackgroundWorker pour effectuer le chargement de données en arrière-plan
        BackgroundWorker1.RunWorkerAsync()


    End Sub

    Private Sub IconButton32_Click(sender As Object, e As EventArgs) Handles IconButton32.Click
        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("stock")
        For i = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(2).Value)
        Next
        ReportToPrint = New LocalReport()
        formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\Report3.rdlc"
        formata4.ReportViewer1.LocalReport.DataSources.Clear()

        formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
        formata4.ReportViewer1.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next
        Dim printDoc As New PrintDocument()


        AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
        m_currentPageIndex = 0

        Dim pageSettings As New PageSettings()

        ' Définissez la taille de la page
        pageSettings.PaperSize = New PaperSize("Custom Size", CInt(8.27 * 100), CInt(11.69 * 100)) ' Convertissez les pouces en centièmes de pouce (1 pouce = 100 centièmes de pouce)

        ' Définissez les marges si nécessaire
        pageSettings.Margins = New Margins(0, 0, 0, 0) ' Vous pouvez ajuster ces valeurs en fonction de vos besoins

        ' Assurez-vous que l'orientation est en mode portrait (si nécessaire)
        pageSettings.Landscape = False

        ' Appliquez les paramètres de page à ReportViewer
        formata4.ReportViewer1.SetPageSettings(pageSettings)

        ' Configuration du PrintPreviewDialog
        formata4.Show()
        formata4.ReportViewer1.Refresh()
    End Sub

    Private Sub IconButton33_Click(sender As Object, e As EventArgs) Handles IconButton33.Click
        adpt2 = New MySqlDataAdapter("select * from article order by id desc", conn2)
        Dim tablecountstock As New DataTable
        adpt2.Fill(tablecountstock)

        DataGridView1.Rows.Clear()
        Dim ttc As Double = 0
        For i = 0 To tablecountstock.Rows.Count - 1
            If tablecountstock.Rows(i).Item(25).ToString.Replace(" ", "").Replace(".", ",") > 0 Then

                DataGridView1.Rows.Add(tablecountstock.Rows(i).Item(0), tablecountstock.Rows(i).Item(1), tablecountstock.Rows(i).Item(8), tablecountstock.Rows(i).Item(2), Convert.ToDouble(tablecountstock.Rows(i).Item(3)).ToString("N0") & "%", Convert.ToDouble(tablecountstock.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2"), tablecountstock.Rows(i).Item(5) & "%", Convert.ToDouble(tablecountstock.Rows(i).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(tablecountstock.Rows(i).Item(7).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(tablecountstock.Rows(i).Item(25).ToString.Replace(".", ",")).ToString("N2"))
                ttc = ttc + Convert.ToDouble(tablecountstock.Rows(i).Item(25).ToString.Replace(".", ",").Replace(" ", "") * tablecountstock.Rows(i).Item(8).ToString.Replace(".", ",").Replace(" ", ""))
            End If
        Next
        TextBox20.Text = ttc.ToString("N2")
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ajout.Show()
            ajout.Panel8.Visible = True
            ajout.Panel9.Visible = True
            ajout.IconButton16.Visible = True
            ajout.IconButton13.Visible = False
            ajout.IconButton3.Visible = False
            ajout.Label16.Text = "Afficher ou modifier un Article :"

            adpt = New MySqlDataAdapter("select * from article where Code = '" & row.Cells(0).Value & "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            For i = 0 To table.Rows.Count - 1
                ajout.Label49.Text = table.Rows(i).Item(0)
                ajout.Label53.Text = Convert.ToDateTime(table.Rows(i).Item(20)).ToString("dd/MM/yyyy")
                ajout.Label58.Text = table.Rows(i).Item(21)
                ajout.TextBox1.Text = table.Rows(i).Item(0)
                ajout.TextBox2.Text = table.Rows(i).Item(1)
                ajout.TextBox3.Text = table.Rows(i).Item(2).ToString.Replace(".", ",")
                Dim index As Integer = ajout.ComboBox6.FindStringExact(table.Rows(i).Item(3).ToString.Replace(",00", ""))
                If index <> -1 Then
                    ajout.ComboBox6.SelectedIndex = index
                End If
                ajout.TextBox5.Text = table.Rows(i).Item(7).ToString.Replace(".", ",")
                ajout.TextBox6.Text = table.Rows(i).Item(6).ToString.Replace(".", ",")
                ajout.TextBox7.Text = table.Rows(i).Item(4).ToString.Replace(".", ",")
                ajout.TextBox8.Text = table.Rows(i).Item(5).ToString.Replace(".", ",")
                ajout.TextBox9.Text = table.Rows(i).Item(9)
                ajout.TextBox25.Text = table.Rows(i).Item(23)
                ajout.TextBox10.Text = table.Rows(i).Item(8)
                ajout.TextBox26.Text = table.Rows(i).Item(25)
                ajout.TextBox18.Text = table.Rows(i).Item(18)
                ajout.TextBox19.Text = table.Rows(i).Item(19)
                If ajout.TextBox18.Text <> 0 Then
                    ajout.CheckBox1.Checked = True
                Else
                    ajout.CheckBox1.Checked = False
                End If
                ajout.ComboBox1.SelectedItem = table.Rows(i).Item(11)
                ajout.ComboBox2.SelectedItem = table.Rows(i).Item(12)
                ajout.ComboBox3.SelectedItem = table.Rows(i).Item(13)
                ajout.ComboBox4.SelectedItem = table.Rows(i).Item(10)

                If table.Rows(0).Item(22) = 0 Then
                    ajout.IconButton2.Text = "Masquer"
                    ajout.IconButton2.IconChar = FontAwesome.Sharp.IconChar.EyeSlash
                    ajout.IconButton2.BackColor = Color.RoyalBlue
                Else
                    ajout.IconButton2.Text = "Démasquer"
                    ajout.IconButton2.IconChar = FontAwesome.Sharp.IconChar.Eye
                    ajout.IconButton2.BackColor = Color.DimGray
                End If

                ajout.DataGridView2.Rows.Clear()
                adpt = New MySqlDataAdapter("select * from code_supp where prod_code = '" + ajout.TextBox1.Text + "'", conn2)
                Dim tablesupp2 As New DataTable
                adpt.Fill(tablesupp2)
                For j = 0 To tablesupp2.Rows.Count() - 1
                    ajout.DataGridView2.Rows.Add(tablesupp2.Rows(j).Item(2))
                Next
                ajout.DataGridView5.Rows.Clear()

                ' Requête SQL pour récupérer l'historique d'achat de l'article
                Dim sql As String = "SELECT a.`Fournisseur`, ad.`CodeArticle`, ad.`PA_TTC`
FROM `achat` a
INNER JOIN `achatdetails` ad ON a.`id` = ad.`achat_Id` where ad.`CodeArticle` = @idArticle
ORDER BY a.`Fournisseur`, ad.`CodeArticle`, ad.`PA_TTC`"
                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@idArticle", table.Rows(0).Item(0))
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim tablehisto As New DataTable
                adapter.Fill(tablehisto)
                For m = 0 To tablehisto.Rows.Count() - 1

                    ajout.DataGridView5.Rows.Add(tablehisto.Rows(m).Item(0), tablehisto.Rows(m).Item(2))
                Next





                adpt = New MySqlDataAdapter("select * from demarque where year(demarque.date) = '" + ajout.ComboBox9.Text + "' and `code` = '" + ajout.Label49.Text + "' order by id desc ", conn2)
                Dim tabledemarque As New DataTable
                adpt.Fill(tabledemarque)
                Dim qteannul, chrgannul, qte1, valr1, qte2, valr2, qte3, valr3, qte4, valr4, qte5, valr5, qte6, valr6, qte7, valr7, qte8, valr8, qte9, valr9, qte10, valr10, qte11, valr11, qte12, valr12 As Double

                ajout.DataGridView4.Rows.Clear()
                For j = 0 To tabledemarque.Rows.Count - 1
                    If tabledemarque.Rows(j).Item(4).Month = "01" Then
                        qte1 += Convert.ToDouble(tabledemarque.Rows(j).Item(2).ToString.Replace(".", ","))
                        valr1 += Convert.ToDouble(tabledemarque.Rows(j).Item(3).ToString.Replace(".", ","))
                    End If
                    If tabledemarque.Rows(j).Item(4).Month = "02" Then
                        qte2 += Convert.ToDouble(tabledemarque.Rows(j).Item(2).ToString.Replace(".", ","))
                        valr2 += Convert.ToDouble(tabledemarque.Rows(j).Item(3).ToString.Replace(".", ","))

                    End If
                    If tabledemarque.Rows(j).Item(4).Month = "03" Then
                        qte3 += Convert.ToDouble(tabledemarque.Rows(j).Item(2).ToString.Replace(".", ","))
                        valr3 += Convert.ToDouble(tabledemarque.Rows(j).Item(3).ToString.Replace(".", ","))

                    End If
                    If tabledemarque.Rows(j).Item(4).Month = "04" Then
                        qte4 += Convert.ToDouble(tabledemarque.Rows(j).Item(2).ToString.Replace(".", ","))
                        valr4 += Convert.ToDouble(tabledemarque.Rows(j).Item(3).ToString.Replace(".", ","))

                    End If
                    If tabledemarque.Rows(j).Item(4).Month = "05" Then
                        qte5 += Convert.ToDouble(tabledemarque.Rows(j).Item(2).ToString.Replace(".", ","))
                        valr5 += Convert.ToDouble(tabledemarque.Rows(j).Item(3).ToString.Replace(".", ","))

                    End If
                    If tabledemarque.Rows(j).Item(4).Month = "06" Then
                        qte6 += Convert.ToDouble(tabledemarque.Rows(j).Item(2).ToString.Replace(".", ","))
                        valr6 += Convert.ToDouble(tabledemarque.Rows(j).Item(3).ToString.Replace(".", ","))

                    End If
                    If tabledemarque.Rows(j).Item(4).Month = "07" Then
                        qte7 += Convert.ToDouble(tabledemarque.Rows(j).Item(2).ToString.Replace(".", ","))
                        valr7 += Convert.ToDouble(tabledemarque.Rows(j).Item(3).ToString.Replace(".", ","))

                    End If
                    If tabledemarque.Rows(j).Item(4).Month = "08" Then
                        qte8 += Convert.ToDouble(tabledemarque.Rows(j).Item(2).ToString.Replace(".", ","))
                        valr8 += Convert.ToDouble(tabledemarque.Rows(j).Item(3).ToString.Replace(".", ","))

                    End If
                    If tabledemarque.Rows(j).Item(4).Month = "09" Then
                        qte9 += Convert.ToDouble(tabledemarque.Rows(j).Item(2).ToString.Replace(".", ","))
                        valr9 += Convert.ToDouble(tabledemarque.Rows(j).Item(3).ToString.Replace(".", ","))

                    End If
                    If tabledemarque.Rows(j).Item(4).Month = "10" Then
                        qte10 += Convert.ToDouble(tabledemarque.Rows(j).Item(2).ToString.Replace(".", ","))
                        valr10 += Convert.ToDouble(tabledemarque.Rows(j).Item(3).ToString.Replace(".", ","))

                    End If
                    If tabledemarque.Rows(j).Item(4).Month = "11" Then
                        qte11 += Convert.ToDouble(tabledemarque.Rows(j).Item(2).ToString.Replace(".", ","))
                        valr11 += Convert.ToDouble(tabledemarque.Rows(j).Item(3).ToString.Replace(".", ","))

                    End If
                    If tabledemarque.Rows(j).Item(4).Month = "12" Then
                        qte12 += Convert.ToDouble(tabledemarque.Rows(j).Item(2).ToString.Replace(".", ","))
                        valr12 += Convert.ToDouble(tabledemarque.Rows(j).Item(3).ToString.Replace(".", ","))

                    End If
                    qteannul += Convert.ToDouble(tabledemarque.Rows(j).Item(2).ToString.Replace(".", ","))
                    chrgannul += Convert.ToDouble(tabledemarque.Rows(j).Item(3).ToString.Replace(".", ","))
                Next
                ajout.DataGridView4.Rows.Add("Janvier", qte1, valr1)
                ajout.DataGridView4.Rows.Add("Février", qte2, valr2)
                ajout.DataGridView4.Rows.Add("Mars", qte3, valr3)
                ajout.DataGridView4.Rows.Add("Avril", qte4, valr4)
                ajout.DataGridView4.Rows.Add("Mai", qte5, valr5)
                ajout.DataGridView4.Rows.Add("Juin", qte6, valr6)
                ajout.DataGridView4.Rows.Add("Juillet", qte7, valr7)
                ajout.DataGridView4.Rows.Add("Aout", qte8, valr8)
                ajout.DataGridView4.Rows.Add("Séptembre", qte9, valr9)
                ajout.DataGridView4.Rows.Add("Octobre", qte10, valr10)
                ajout.DataGridView4.Rows.Add("Novembre", qte11, valr11)
                ajout.DataGridView4.Rows.Add("Décembre", qte12, valr12)

                ajout.Label44.Text = qteannul.ToString("#0.00")
                ajout.Label42.Text = chrgannul.ToString("#0.00 DHs")
                ajout.TextBox2.Select()
                charg = True
            Next



        End If
    End Sub

    Private Sub stock_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

    End Sub
End Class