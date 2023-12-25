Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports ZXing
Imports ZXing.Common

Public Class balisage
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Dim sql2, sql3 As String

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click


        adpt = New MySqlDataAdapter("select prod_code from code_supp where code_supp = '" + TextBox1.Text + "' ", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count() = 0 Then
            adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_HT,pv_gros,PV_HT,TVA from article where Code = '" + TextBox1.Text + "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count() <> 0 Then
                DataGridView2.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1), table.Rows(0).Item(2) & " DHs", 1)
                TextBox1.Text = ""
            Else
                MsgBox("Produit introuvable !")
                TextBox1.Text = ""
            End If
        Else
            adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_HT,pv_gros,PV_HT, TVA from article where Code = '" + table2.Rows(0).Item(0) + "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count() <> 0 Then
                DataGridView2.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1), table.Rows(0).Item(2) & " DHs", 1)
                TextBox1.Text = ""
            Else
                MsgBox("Produit introuvable !")
                TextBox1.Text = ""
            End If

        End If

    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
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
        dt = ds.Tables("balisage")
        For i = 0 To DataGridView2.Rows.Count - 1
            adpt = New MySqlDataAdapter("select * from article where Code = '" + DataGridView2.Rows(i).Cells(0).Value.ToString + "'", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            For j = 0 To DataGridView2.Rows(i).Cells(3).Value - 1
                dt.Rows.Add(table.Rows(0).Item(1), table.Rows(0).Item(0) & vbCrLf & "---------------------------------", table.Rows(0).Item(7) & " DHs")
            Next
        Next
        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\balisage.rdlc"
        ReportToPrint.DataSources.Clear()


        ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>3.7in</PageWidth><PageHeight>12in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
        ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next
        Dim printDoc As New PrintDocument()
        printDoc.PrinterSettings.PrinterName = receiptprinter
        Dim ps As New PrinterSettings()
        ps.PrinterName = printDoc.PrinterSettings.PrinterName
        printDoc.PrinterSettings = ps

        AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
        m_currentPageIndex = 0
        printDoc.Print()
        MsgBox("Terminé !")
        TextBox1.Text = ""
        DataGridView2.Rows.Clear()
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
    Dim cmd2, cmd3 As MySqlCommand

    Private Sub Panel6_Paint(sender As Object, e As PaintEventArgs) Handles Panel6.Paint

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then

            adpt = New MySqlDataAdapter("select prod_code from code_supp where code_supp = '" + TextBox1.Text + "' ", conn2)
            Dim table2 As New DataTable
            adpt.Fill(table2)
            If table2.Rows.Count() = 0 Then
                adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_HT,pv_gros,PV_HT,TVA from article where Code = '" + TextBox1.Text + "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    DataGridView2.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1), table.Rows(0).Item(2) & " DHs", 1)
                    TextBox1.Text = ""
                Else
                    MsgBox("Produit introuvable !")
                    TextBox1.Text = ""
                End If
            Else
                adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_HT,pv_gros,PV_HT, TVA from article where Code = '" + table2.Rows(0).Item(0) + "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    DataGridView2.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1), table.Rows(0).Item(2) & " DHs", 1)
                    TextBox1.Text = ""
                Else
                    MsgBox("Produit introuvable !")
                    TextBox1.Text = ""
                End If

            End If

        End If

    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        adpt = New MySqlDataAdapter("select prod_code from code_supp where code_supp = '" + TextBox4.Text + "' ", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count() = 0 Then
            adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_HT,pv_gros,PV_HT,TVA,PA_TTC from article where Code = '" + TextBox4.Text + "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count() <> 0 Then
                conn2.Open()
                Dim sql2, sql3 As String
                Dim cmd2, cmd3 As MySqlCommand
                sql3 = "INSERT INTO `gifts`(`name`, `price`) VALUES (@name,@op) "
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.Parameters.AddWithValue("@name", table.Rows(0).Item(1))
                cmd3.Parameters.AddWithValue("@op", table.Rows(0).Item(7))
                cmd3.ExecuteNonQuery()

                conn2.Close()

                TextBox4.Text = ""
            Else
                MsgBox("Produit introuvable !")
                TextBox4.Text = ""
            End If
        Else
            adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_HT,pv_gros,PV_HT,TVA,PA_TTC from article where Code = '" + TextBox4.Text + "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count() <> 0 Then
                conn2.Open()
                Dim sql2, sql3 As String
                Dim cmd2, cmd3 As MySqlCommand
                sql3 = "INSERT INTO `gifts`(`name`, `price`) VALUES (@name,@op) "
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.Parameters.AddWithValue("@name", table.Rows(0).Item(1))
                cmd3.Parameters.AddWithValue("@op", table.Rows(0).Item(7))
                cmd3.ExecuteNonQuery()

                conn2.Close()
                TextBox4.Text = ""

            Else
                MsgBox("Produit introuvable !")
                TextBox4.Text = ""
            End If

        End If
        DataGridView1.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from gifts order by id desc", conn2)
        Dim tablegift As New DataTable
        adpt.Fill(tablegift)

        For i = 0 To tablegift.Rows.Count() - 1
            DataGridView1.Rows.Add(tablegift.Rows(i).Item(0), tablegift.Rows(i).Item(1), tablegift.Rows(i).Item(2), tablegift.Rows(i).Item(3), "")
        Next

    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Dim cod As Integer = CInt(Math.Ceiling(Rnd() * 999999999)) + 1
        adpt = New MySqlDataAdapter("select * from fideles where code = '" + cod.ToString + "'", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        If table.Rows.Count = 0 Then
            TextBox5.Text = cod
        Else
            IconButton6.PerformClick()
        End If

    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        conn2.Open()
        Dim sql3 As String
        Dim cmd3 As MySqlCommand
        sql3 = "INSERT INTO `fideles`(`code`, `lastachats`,`achats`) VALUES (@name,@op,@achat) "
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.Parameters.Clear()
        cmd3.Parameters.AddWithValue("@name", TextBox5.Text)
        cmd3.Parameters.AddWithValue("@op", 0)
        cmd3.Parameters.AddWithValue("@achat", 0)
        cmd3.ExecuteNonQuery()
        TextBox5.Text = ""

        DataGridView3.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from fideles order by id desc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        For i = 0 To table.Rows.Count() - 1
            DataGridView3.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3))
        Next
        conn2.Close()
    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        Dim searchTerm As String = TextBox6.Text

        ' Parcours les lignes du DataGridView pour trouver la correspondance
        For Each row As DataGridViewRow In DataGridView3.Rows
            For Each cell As DataGridViewCell In row.Cells
                If cell.Value IsNot Nothing AndAlso cell.Value.ToString().Contains(searchTerm) Then
                    ' Sélectionne la ligne correspondante
                    row.Selected = True
                    DataGridView3.FirstDisplayedScrollingRowIndex = row.Index
                    Return ' Sort de la boucle si une correspondance est trouvée
                End If
            Next
        Next

        ' Aucune correspondance trouvée, désélectionne toutes les lignes
        DataGridView1.ClearSelection()
    End Sub
    Dim PrintDoc As Printing.PrintDocument = New Printing.PrintDocument()
    Dim pd_PrintDialog As New PrintDialog
    Private Sub IconButton8_Click(sender As Object, e As EventArgs) Handles IconButton8.Click

        For i = 0 To DataGridView4.Rows.Count() - 1
            Dim texte As String = DataGridView4.Rows(i).Cells(0).Value

            If texte <> "" Then


                Dim writer As New BarcodeWriter
                writer.Format = BarcodeFormat.CODE_128
                PictureBox1.Image = writer.Write(texte)

                Dim pictureBoxImage As Image = PictureBox1.Image
                Dim folderPath As String = "C:\bcgen\"
                Dim fileName As String = "image_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".png"
                Dim filePath As String = Path.Combine(folderPath, fileName)

                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If
                pictureBoxImage.Save(filePath)


                ReportToPrint = New LocalReport()
                ReportToPrint.ReportPath = Application.StartupPath + "\barcode.rdlc"
                ReportToPrint.DataSources.Clear()
                ReportToPrint.EnableExternalImages = True

                Dim img As New ReportParameter("image", "File:\" + filePath, True)
                Dim img1(0) As ReportParameter
                img1(0) = img
                ReportToPrint.SetParameters(img1)

                Dim mode As New ReportParameter("mode", DataGridView4.Rows(i).Cells(1).Value.ToString)
                Dim mode1(0) As ReportParameter
                mode1(0) = mode
                ReportToPrint.SetParameters(mode1)

                Dim appPath As String = Application.StartupPath()

                Dim SaveDirectory As String = appPath & "\"
                Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                Dim Logo As New ReportParameter("Logo", "File:\" + SavePath, True)
                Dim Logo1(0) As ReportParameter
                Logo1(0) = Logo
                ReportToPrint.SetParameters(Logo1)

                Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>4in</PageWidth><PageHeight>2.5in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
                Dim warnings As Warning()
                m_streams = New List(Of Stream)()
                ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
                For Each stream As Stream In m_streams
                    stream.Position = 0
                Next
                Dim printDoc As New PrintDocument()
                printDoc.PrinterSettings.PrinterName = bcprinter
                Dim ps As New PrinterSettings()
                ps.PrinterName = printDoc.PrinterSettings.PrinterName


                AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
                m_currentPageIndex = 0
                printDoc.PrinterSettings.Copies = DataGridView4.Rows(i).Cells(3).Value
                printDoc.Print()

            End If

        Next






    End Sub
    Dim nud_MarginH As Integer = 10
    Dim nud_MarginW As Integer = 140
    Dim nud_MarginHBrcode As Integer = 90
    Dim nud_MarginWBrcode As Integer = 100.0
    Private Sub PrintDocHandler(ByVal sender As Object, ByVal ev As Printing.PrintPageEventArgs)
        ev.Graphics.DrawImage(PictureBox1.Image, nud_MarginH, nud_MarginW, nud_MarginWBrcode, nud_MarginHBrcode)
    End Sub
    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs)
        ' Obtenir l'image du PictureBox à imprimer
        Dim image As Image = PictureBox1.Image

        ' Calculer les coordonnées pour centrer l'image sur la page
        Dim largeurPage As Integer = e.PageBounds.Width
        Dim hauteurPage As Integer = e.PageBounds.Height
        Dim positionX As Integer = (largeurPage - image.Width) \ 2
        Dim positionY As Integer = (hauteurPage - image.Height) \ 2

        ' Dessiner l'image sur la page
        e.Graphics.DrawImage(image, positionX, positionY)
    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click


        adpt = New MySqlDataAdapter("select prod_code from code_supp where code_supp = '" + TextBox9.Text + "' ", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count() = 0 Then
            adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_HT,pv_gros,PV_HT,TVA from article where Code = '" + TextBox9.Text + "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count() <> 0 Then
                DataGridView4.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1), table.Rows(0).Item(2) & " DHs", 1)
                TextBox9.Text = ""
            Else
                MsgBox("Produit introuvable !")
                TextBox9.Text = ""
            End If
        Else
            adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_HT,pv_gros,PV_HT, TVA from article where Code = '" + table2.Rows(0).Item(0) + "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count() <> 0 Then
                DataGridView4.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1), table.Rows(0).Item(2) & " DHs", 1)
                TextBox9.Text = ""
            Else
                MsgBox("Produit introuvable !")
                TextBox9.Text = ""
            End If

        End If
    End Sub

    Private Sub IconButton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        If Panel9.Visible = False Then
            Panel9.Visible = True
            TextBox7.Select()
        Else
            Panel9.Visible = False
            DataGridView5.Rows.Clear()
            TextBox7.Text = ""
        End If
    End Sub

    Private Sub TextBox7_Keydown(sender As Object, e As KeyEventArgs) Handles TextBox7.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Panel9.Visible = True Then
                If TextBox7.Text <> "" Then
                    DataGridView5.Rows.Clear()
                    adpt = New MySqlDataAdapter("select Code, Article from article where Article like '%" + TextBox7.Text + "%' order by Article asc", conn2)
                    Dim tablelike As New DataTable
                    adpt.Fill(tablelike)
                    If tablelike.Rows.Count() <> 0 Then
                        For i = 0 To tablelike.Rows.Count() - 1
                            DataGridView5.Rows.Add(tablelike.Rows(i).Item(0), tablelike.Rows(i).Item(1))
                        Next
                    End If
                End If

            End If
        End If
    End Sub
    Private Sub DataGridView5_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView5.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView5.Rows(e.RowIndex)
            adpt = New MySqlDataAdapter("select Code, Article, PV_TTC from article where Code = '" & row.Cells(0).Value & "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            DataGridView4.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1), table.Rows(0).Item(2) & " DHs", 1)
            DataGridView2.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1), table.Rows(0).Item(2) & " DHs", 1)
        End If

    End Sub
    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Enter Then

            adpt = New MySqlDataAdapter("select prod_code from code_supp where code_supp = '" + TextBox4.Text + "' ", conn2)
            Dim table2 As New DataTable
            adpt.Fill(table2)
            If table2.Rows.Count() = 0 Then
                adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_HT,pv_gros,PV_HT,TVA,PA_TTC from article where Code = '" + TextBox4.Text + "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    conn2.Open()
                    Dim sql2, sql3 As String
                    Dim cmd2, cmd3 As MySqlCommand
                    sql3 = "INSERT INTO `gifts`(`name`, `price`) VALUES (@name,@op) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@name", table.Rows(0).Item(1))
                    cmd3.Parameters.AddWithValue("@op", table.Rows(0).Item(7))
                    cmd3.ExecuteNonQuery()

                    conn2.Close()

                    TextBox4.Text = ""
                Else
                    MsgBox("Produit introuvable !")
                    TextBox4.Text = ""
                End If
            Else
                adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_HT,pv_gros,PV_HT,TVA,PA_TTC from article where Code = '" + TextBox4.Text + "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    conn2.Open()
                    Dim sql2, sql3 As String
                    Dim cmd2, cmd3 As MySqlCommand
                    sql3 = "INSERT INTO `gifts`(`name`, `price`) VALUES (@name,@op) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@name", table.Rows(0).Item(1))
                    cmd3.Parameters.AddWithValue("@op", table.Rows(0).Item(7))
                    cmd3.ExecuteNonQuery()

                    conn2.Close()
                    TextBox4.Text = ""
                Else
                    MsgBox("Produit introuvable !")
                    TextBox4.Text = ""
                End If

            End If

        End If
        DataGridView1.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from gifts order by id desc", conn2)
        Dim tablegift As New DataTable
        adpt.Fill(tablegift)

        For i = 0 To tablegift.Rows.Count() - 1
            DataGridView1.Rows.Add(tablegift.Rows(i).Item(0), tablegift.Rows(i).Item(1), tablegift.Rows(i).Item(2), tablegift.Rows(i).Item(3), "")
        Next
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        Dim inputText As String = TextBox7.Text

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
            RemoveHandler TextBox7.TextChanged, AddressOf TextBox7_TextChanged

            ' Mettez à jour le texte dans le TextBox
            TextBox7.Text = modifiedText

            ' Replacez le curseur à la position correcte après la modification
            TextBox7.SelectionStart = TextBox7.Text.Length

            ' Réactivez le gestionnaire d'événements TextChanged
            AddHandler TextBox7.TextChanged, AddressOf TextBox7_TextChanged
        End If
    End Sub

    <Obsolete>
    Private Sub balisage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim printerticket As String = System.Configuration.ConfigurationSettings.AppSettings("printerticket")
        Dim printera5 As String = System.Configuration.ConfigurationSettings.AppSettings("printera5")
        Dim printera4 As String = System.Configuration.ConfigurationSettings.AppSettings("printera4")
        Dim printerbc As String = System.Configuration.ConfigurationSettings.AppSettings("printerbc")

        receiptprinter = printerticket
        a4printer = printera4
        a5printer = printera5
        bcprinter = printerbc

    End Sub

    Private Sub DataGridView2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex <> 3 Then
                DataGridView2.Rows.RemoveAt(e.RowIndex)
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 4 Then
                Dim Rep As Integer
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Rep = MsgBox("Voulez-vous vraiment supprimer ce cadeaux ?", vbYesNo)
                If Rep = vbYes Then

                    conn2.Open()
                    Dim sql2, sql3 As String
                    Dim cmd2, cmd3 As MySqlCommand
                    sql2 = "delete from gifts where id = '" + row.Cells(0).Value.ToString + "' "
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()


                    conn2.Close()

                    DataGridView1.Rows.Clear()
                    adpt = New MySqlDataAdapter("select * from gifts order by id desc", conn2)
                    Dim tablegift As New DataTable
                    adpt.Fill(tablegift)

                    For i = 0 To tablegift.Rows.Count() - 1
                        DataGridView1.Rows.Add(tablegift.Rows(i).Item(0), tablegift.Rows(i).Item(1), tablegift.Rows(i).Item(2), tablegift.Rows(i).Item(3), "")
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView4_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellDoubleClick
        If e.RowIndex <> -1 Then
            If e.ColumnIndex <> 3 Then
                DataGridView4.Rows.RemoveAt(DataGridView4.CurrentRow.Index)
            End If
        End If
    End Sub
End Class