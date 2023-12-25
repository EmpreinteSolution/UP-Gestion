Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Public Class edit_product
    Dim ReportToPrint As LocalReport
    Dim m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
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

    'Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt, adpt2 As MySqlDataAdapter
    Dim imgName As String
    Dim N As Integer = 0
    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Close()

    End Sub
    Dim sql2, sql3 As String
    Dim cmd2, cmd3 As MySqlCommand
    Dim comb1 As String
    Dim execution As Integer


    Dim comb2, comb3 As String

    Private Sub DataGridView2_CellMouseClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 1 Then
                Dim Rep As Integer
                Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                Rep = MsgBox("Voulez-vous vraiment supprimer ce code supplémentaire ?", vbYesNo)
                If Rep = vbYes Then

                    conn2.Open()
                    Dim sql2, sql3 As String
                    Dim cmd2, cmd3 As MySqlCommand
                    sql2 = "delete from code_supp where code_supp = '" + row.Cells(0).Value.ToString + "' "
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()
                    conn2.Close()

                    DataGridView2.Rows.Clear()
                    adpt = New MySqlDataAdapter("select * from code_supp where prod_code = '" + Label6.Text + "'", conn2)
                    Dim tablesupp As New DataTable
                    adpt.Fill(tablesupp)
                    For j = 0 To tablesupp.Rows.Count() - 1
                        DataGridView2.Rows.Add(tablesupp.Rows(j).Item(2))
                    Next
                End If
            End If
        End If

    End Sub
    Private Sub IconButton1_Click_1(sender As Object, e As EventArgs) Handles IconButton1.Click
        adpt = New MySqlDataAdapter("select * from code_supp where code_supp = '" + TextBox6.Text + "'", conn2)
        Dim tablesupp As New DataTable
        adpt.Fill(tablesupp)
        If tablesupp.Rows.Count() = 0 Then
            conn2.Open()
            sql2 = "INSERT INTO code_supp (prod_code,code_supp) VALUES ('" + Label6.Text + "', '" + TextBox6.Text + "')"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()
            conn2.Close()

            DataGridView2.Rows.Clear()
            adpt = New MySqlDataAdapter("select * from code_supp where prod_code = '" + Label6.Text + "'", conn2)
            Dim tablesupp2 As New DataTable
            adpt.Fill(tablesupp2)
            For j = 0 To tablesupp2.Rows.Count() - 1
                DataGridView2.Rows.Add(tablesupp2.Rows(j).Item(2))
            Next
        Else
            MsgBox("Ce code existe déjà !")
        End If
        TextBox6.Text = ""
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click

        Dim Rep As Integer = MsgBox("voullez-vous vraiment démarquer ce produit ?", vbYesNo)
        If Rep = vbYes Then
            conn2.Open()
            sql2 = "INSERT INTO demarque ( `code`, `qte`,`valeur`, `date`) VALUES (@v1,@v3,@v6,@v7)"

            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.AddWithValue("@v1", Label6.Text)
            cmd2.Parameters.AddWithValue("@v3", Convert.ToDouble(TextBox11.Text.ToString.Replace(".", ",")))
            cmd2.Parameters.AddWithValue("@v6", Convert.ToDouble((TextBox11.Text * TextBox7.Text).ToString.Replace(".", ",")))
            cmd2.Parameters.AddWithValue("@v7", DateTime.Today)

            cmd2.ExecuteNonQuery()
            conn2.Close()

        End If
        DataGridView1.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from demarque where year(demarque.date) = '" + ComboBox5.Text + "' and `code` = '" + Label6.Text + "' order by id desc ", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim qteannul, chrgannul, qte1, valr1, qte2, valr2, qte3, valr3, qte4, valr4, qte5, valr5, qte6, valr6, qte7, valr7, qte8, valr8, qte9, valr9, qte10, valr10, qte11, valr11, qte12, valr12 As Double

        DataGridView1.Rows.Clear()
        For i = 0 To table.Rows.Count - 1
            If table.Rows(i).Item(4).Month = "01" Then
                qte1 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr1 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))
            End If
            If table.Rows(i).Item(4).Month = "02" Then
                qte2 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr2 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "03" Then
                qte4 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr4 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "04" Then
                qte4 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr4 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "05" Then
                qte5 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr5 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "06" Then
                qte6 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr6 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "07" Then
                qte7 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr7 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "08" Then
                qte8 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr8 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "09" Then
                qte9 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr9 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "10" Then
                qte10 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr10 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "11" Then
                qte11 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr11 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "12" Then
                qte12 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr12 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            qteannul += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
            chrgannul += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))
        Next
        DataGridView1.Rows.Add("Janvier", qte1, valr1)
        DataGridView1.Rows.Add("Février", qte2, valr2)
        DataGridView1.Rows.Add("Mars", qte3, valr3)
        DataGridView1.Rows.Add("Avril", qte4, valr4)
        DataGridView1.Rows.Add("Mai", qte5, valr5)
        DataGridView1.Rows.Add("Juin", qte6, valr6)
        DataGridView1.Rows.Add("Juillet", qte7, valr7)
        DataGridView1.Rows.Add("Aout", qte8, valr8)
        DataGridView1.Rows.Add("Séptembre", qte9, valr9)
        DataGridView1.Rows.Add("Octobre", qte10, valr10)
        DataGridView1.Rows.Add("Novembre", qte11, valr11)
        DataGridView1.Rows.Add("Décembre", qte12, valr12)

        Label25.Text = qteannul.ToString("### ### ###")
        Label27.Text = chrgannul.ToString("### ### ###,## DHs")
        conn2.Open()
        sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.Parameters.Clear()
        cmd3.Parameters.AddWithValue("@name", stock.Label2.Text)
        cmd3.Parameters.AddWithValue("@op", "Démarquage du produit " & TextBox2.Text & " par une quantité de " & TextBox11.Text)
        cmd3.ExecuteNonQuery()
        conn2.Close()
        TextBox11.Text = ""
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        DataGridView1.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from demarque where year(demarque.date) = '" + ComboBox5.Text + "' and `code` = '" + Label6.Text + "' order by id desc ", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim qteannul, chrgannul, qte1, valr1, qte2, valr2, qte3, valr3, qte4, valr4, qte5, valr5, qte6, valr6, qte7, valr7, qte8, valr8, qte9, valr9, qte10, valr10, qte11, valr11, qte12, valr12 As Double

        DataGridView1.Rows.Clear()
        For i = 0 To table.Rows.Count - 1
            If table.Rows(i).Item(4).Month = "01" Then
                qte1 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr1 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))
            End If
            If table.Rows(i).Item(4).Month = "02" Then
                qte2 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr2 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "03" Then
                qte4 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr4 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "04" Then
                qte4 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr4 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "05" Then
                qte5 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr5 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "06" Then
                qte6 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr6 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "07" Then
                qte7 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr7 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "08" Then
                qte8 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr8 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "09" Then
                qte9 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr9 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "10" Then
                qte10 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr10 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "11" Then
                qte11 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr11 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "12" Then
                qte12 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr12 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            qteannul += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
            chrgannul += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))
        Next
        DataGridView1.Rows.Add("Janvier", qte1, valr1)
        DataGridView1.Rows.Add("Février", qte2, valr2)
        DataGridView1.Rows.Add("Mars", qte3, valr3)
        DataGridView1.Rows.Add("Avril", qte4, valr4)
        DataGridView1.Rows.Add("Mai", qte5, valr5)
        DataGridView1.Rows.Add("Juin", qte6, valr6)
        DataGridView1.Rows.Add("Juillet", qte7, valr7)
        DataGridView1.Rows.Add("Aout", qte8, valr8)
        DataGridView1.Rows.Add("Séptembre", qte9, valr9)
        DataGridView1.Rows.Add("Octobre", qte10, valr10)
        DataGridView1.Rows.Add("Novembre", qte11, valr11)
        DataGridView1.Rows.Add("Décembre", qte12, valr12)

        Label25.Text = qteannul.ToString("### ### ###")
        Label27.Text = chrgannul.ToString("### ### ###,## DHs")
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Dim ad As New MySqlDataAdapter

        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("demarque")

        For j = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(j).Cells(0).Value, DataGridView1.Rows(j).Cells(1).Value, DataGridView1.Rows(j).Cells(2).Value)
        Next
        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\Report6.rdlc"
        ReportToPrint.DataSources.Clear()
        ReportToPrint.EnableExternalImages = True


        Dim year As New ReportParameter("year", ComboBox5.Text)
        Dim year1(0) As ReportParameter
        year1(0) = year
        ReportToPrint.SetParameters(year1)

        Dim code As New ReportParameter("code", Label6.Text)
        Dim code1(0) As ReportParameter
        code1(0) = code
        ReportToPrint.SetParameters(code1)

        Dim name As New ReportParameter("name", TextBox2.Text)
        Dim name1(0) As ReportParameter
        name1(0) = name
        ReportToPrint.SetParameters(name1)

        Dim qte As New ReportParameter("qte", Label25.Text)
        Dim qte1(0) As ReportParameter
        qte1(0) = qte
        ReportToPrint.SetParameters(qte1)

        Dim valeur As New ReportParameter("valeur", Label27.Text.ToString.Replace(" DHs", ""))
        Dim valeur1(0) As ReportParameter
        valeur1(0) = valeur
        ReportToPrint.SetParameters(valeur1)

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
        'C80250 Series
        'C80250 Series init
        printDoc.PrinterSettings.PrinterName = a4printer
        Dim ps As New PrinterSettings()
        ps.PrinterName = printDoc.PrinterSettings.PrinterName
        printDoc.PrinterSettings = ps

        AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
        m_currentPageIndex = 0
        printDoc.Print()
        dt.Clear()

        conn2.Open()
        sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.Parameters.Clear()
        cmd3.Parameters.AddWithValue("@name", stock.Label2.Text)
        cmd3.Parameters.AddWithValue("@op", "Impression de l'historique du démarquage du produit " & TextBox2.Text)
        cmd3.ExecuteNonQuery()
        conn2.Close()
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        codbar.Show()
        codbar.PicBarCode.BackgroundImage = Code128(TextBox4.Text, "A")
        codbar.TextBox1.Text = TextBox2.Text
        codbar.TextBox3.Text = TextBox4.Text
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        adpt = New MySqlDataAdapter("select * from famille where nomFamille = '" + ComboBox1.Text.ToString + "' ", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        adpt2 = New MySqlDataAdapter("select * from sous_famille where idFamille = '" + table.Rows(0).Item(0).ToString + "'", conn2)
        Dim table2 As New DataTable
        adpt2.Fill(table2)
        ComboBox2.Items.Clear()
        For i = 0 To table2.Rows.Count - 1
            ComboBox2.Items.Add(table2.Rows(i).Item(2))
        Next
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ComboBox6.Text = "" Then
            TextBox7.Text = Convert.ToDouble(TextBox3.Text.ToString.Replace(".", ","))
            TextBox3.Text = Convert.ToDouble(TextBox7.Text.ToString.Replace(".", ","))
        Else
            TextBox7.Text = Convert.ToDouble(TextBox3.Text.ToString.Replace(".", ",")) + (Convert.ToDouble(TextBox3.Text.ToString.Replace(".", ",")) * ComboBox6.Text / 100)
            TextBox3.Text = Convert.ToDouble(TextBox7.Text.ToString.Replace(".", ",")) / (1 + (ComboBox6.Text / 100))
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        adpt = New MySqlDataAdapter("select * from article where Code = '" + TextBox4.Text.ToString + "'", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)

        If table2.Rows.Count <> 0 Then
            If TextBox4.Text = Label6.Text Then


                comb1 = ComboBox4.Text
                conn2.Open()

                Dim cmd2 As MySqlCommand

                cmd2 = New MySqlCommand("UPDATE `article` SET `Code` = '" & TextBox4.Text.ToString & "',`Article` = '" & TextBox2.Text.ToString & "',`PA_HT` = '" + TextBox3.Text + "',`TVA` = '" + ComboBox6.Text + "',`PA_TTC` = '" + TextBox7.Text + "',`TM` = '" + TextBox8.Text + "',`PV_HT` = '" + TextBox1.Text + "',`PV_TTC` = '" + TextBox5.Text + "',`Stock` = '" + TextBox10.Text + "',`pv_gros` = '" + TextBox13.Text + "',`fournisseur` = '" + ComboBox3.Text + "',`Securite_stock` = '" + TextBox9.Text + "',`unit`='" + comb1 + "'  WHERE `Code` = '" + Label6.Text.ToString + "' ", conn2)
                cmd2.ExecuteNonQuery()

                adpt = New MySqlDataAdapter("select path from infos", conn2)
                Dim tableinfo As New DataTable
                adpt.Fill(tableinfo)

                Dim path As String = tableinfo.Rows(0).Item(0).ToString.Replace("/", "\") & "\orden.dat"

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


                'adpt = New MySqlDataAdapter("select * from famille where nomFamille = '" + ComboBox1.Text.ToString + "' ", conn2)
                'Dim table As New DataTable
                'adpt.Fill(table)

                'adpt2 = New MySqlDataAdapter("select * from sous_famille where nomSFamille = '" + ComboBox2.Text.ToString + "'", conn2)
                'Dim table2 As New DataTable        'adpt2.Fill(table2)


                conn2.Close()

                conn2.Open()
                sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.Parameters.AddWithValue("@name", stock.Label2.Text)
                cmd3.Parameters.AddWithValue("@op", "Modification du produit " & TextBox2.Text)
                cmd3.ExecuteNonQuery()
                conn2.Close()

                MsgBox("Produit bien modifié !")
                Me.Close()
            Else
                TextBox4.Text = Label6.Text
                MsgBox("Code existe déjà")
            End If
        Else

            comb1 = ComboBox4.Text
                conn2.Open()

                Dim cmd2 As MySqlCommand

                cmd2 = New MySqlCommand("UPDATE `article` SET `Code` = '" & TextBox4.Text.ToString & "',`Article` = '" & TextBox2.Text.ToString & "',`PA_HT` = '" + TextBox3.Text + "',`TVA` = '" + ComboBox6.Text + "',`PA_TTC` = '" + TextBox7.Text + "',`TM` = '" + TextBox8.Text + "',`PV_HT` = '" + TextBox1.Text + "',`PV_TTC` = '" + TextBox5.Text + "',`Stock` = '" + TextBox10.Text + "',`pv_gros` = '" + TextBox13.Text + "',`fournisseur` = '" + ComboBox3.Text + "',`Securite_stock` = '" + TextBox9.Text + "',`unit`='" + comb1 + "'  WHERE `Code` = '" + Label6.Text.ToString + "' ", conn2)
                cmd2.ExecuteNonQuery()

                adpt = New MySqlDataAdapter("select path from infos", conn2)
                Dim tableinfo As New DataTable
                adpt.Fill(tableinfo)

                Dim path As String = tableinfo.Rows(0).Item(0).ToString.Replace("/", "\") & "\orden.dat"

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


                'adpt = New MySqlDataAdapter("select * from famille where nomFamille = '" + ComboBox1.Text.ToString + "' ", conn2)
                'Dim table As New DataTable
                'adpt.Fill(table)

                'adpt2 = New MySqlDataAdapter("select * from sous_famille where nomSFamille = '" + ComboBox2.Text.ToString + "'", conn2)
                'Dim table2 As New DataTable        'adpt2.Fill(table2)


                conn2.Close()

                conn2.Open()
                sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.Parameters.AddWithValue("@name", stock.Label2.Text)
                cmd3.Parameters.AddWithValue("@op", "Modification du produit " & TextBox2.Text)
                cmd3.ExecuteNonQuery()
                conn2.Close()

                MsgBox("Produit bien modifié !")
                Me.Close()


        End If
    End Sub
    Dim code As String

    Private Sub edit_product_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select ticket,printer,printertwo from infos", conn2)
        Dim tableprint As New DataTable
        adpt.Fill(tableprint)
        receiptprinter = tableprint.Rows(0).Item(0)
        a4printer = tableprint.Rows(0).Item(1)
        a5printer = tableprint.Rows(0).Item(2)
        adpt = New MySqlDataAdapter("select * from demarque where year(demarque.date) = '" + ComboBox5.Text + "' and `code` = '" + Label6.Text + "' order by id desc ", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim qteannul, chrgannul, qte1, valr1, qte2, valr2, qte3, valr3, qte4, valr4, qte5, valr5, qte6, valr6, qte7, valr7, qte8, valr8, qte9, valr9, qte10, valr10, qte11, valr11, qte12, valr12 As Double

        DataGridView1.Rows.Clear()
        For i = 0 To table.Rows.Count - 1
            If table.Rows(i).Item(4).Month = "01" Then
                qte1 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr1 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))
            End If
            If table.Rows(i).Item(4).Month = "02" Then
                qte2 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr2 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "03" Then
                qte3 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr3 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "04" Then
                qte4 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr4 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "05" Then
                qte5 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr5 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "06" Then
                qte6 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr6 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "07" Then
                qte7 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr7 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "08" Then
                qte8 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr8 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "09" Then
                qte9 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr9 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "10" Then
                qte10 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr10 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "11" Then
                qte11 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr11 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            If table.Rows(i).Item(4).Month = "12" Then
                qte12 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                valr12 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))

            End If
            qteannul += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
            chrgannul += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))
        Next
        DataGridView1.Rows.Add("Janvier", qte1, valr1)
        DataGridView1.Rows.Add("Février", qte2, valr2)
        DataGridView1.Rows.Add("Mars", qte3, valr3)
        DataGridView1.Rows.Add("Avril", qte4, valr4)
        DataGridView1.Rows.Add("Mai", qte5, valr5)
        DataGridView1.Rows.Add("Juin", qte6, valr6)
        DataGridView1.Rows.Add("Juillet", qte7, valr7)
        DataGridView1.Rows.Add("Aout", qte8, valr8)
        DataGridView1.Rows.Add("Séptembre", qte9, valr9)
        DataGridView1.Rows.Add("Octobre", qte10, valr10)
        DataGridView1.Rows.Add("Novembre", qte11, valr11)
        DataGridView1.Rows.Add("Décembre", qte12, valr12)

        Label25.Text = qteannul.ToString("### ### ###")
        Label27.Text = chrgannul.ToString("### ### ###,## DHs")


    End Sub



    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged
        If TextBox14.Text = "" Then
            adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                ComboBox3.Items.Clear()
                ComboBox3.Items.Add("aucun")
                For i = 0 To table.Rows.Count - 1
                    ComboBox3.Items.Add(table.Rows(i).Item(0))
                Next
            End If
        Else
            ComboBox3.Items.Clear()
            adpt = New MySqlDataAdapter("select name from fours WHERE name LIKE '%" + TextBox14.Text.Replace("'", " ") + "%' order by name asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                For i = 0 To table.Rows.Count - 1
                    ComboBox3.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox3.SelectedIndex = 0
            End If
        End If
    End Sub


    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox7_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox7.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox7.Text = "" Then

                TextBox7.Text = 0
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox1.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            Else

                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox1.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            End If
        End If
    End Sub

    Private Sub TextBox8_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox8.Text = "" Then

                TextBox8.Text = 0
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox1.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            Else
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox1.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            End If
        End If
    End Sub

    Private Sub TextBox5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox5.Text = "" Then

                TextBox5.Text = 0
                TextBox1.Text = Convert.ToDouble(TextBox5.Text.Replace(".", ",") / (1 + (ComboBox6.Text.Replace(".", ",") / 100))).ToString("# ##0.00")
                TextBox8.Text = Convert.ToDouble(((TextBox1.Text.Replace(".", ",") - TextBox3.Text.Replace(".", ",")) / TextBox3.Text.Replace(".", ",")) * 100).ToString("# ##0.00")
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox1.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            Else
                TextBox1.Text = Convert.ToDouble(TextBox5.Text.Replace(".", ",") / (1 + (ComboBox6.Text.Replace(".", ",") / 100))).ToString("# ##0.00")
                TextBox8.Text = Convert.ToDouble(((TextBox1.Text.Replace(".", ",") - TextBox3.Text.Replace(".", ",")) / TextBox3.Text.Replace(".", ",")) * 100).ToString("# ##0.00")
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox1.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            End If
        End If
    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Text = "" Then

                TextBox3.Text = 0
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox1.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            Else

                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox1.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            End If
        End If
    End Sub
End Class