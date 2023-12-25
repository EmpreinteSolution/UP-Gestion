Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.ComponentModel

Public Class banques
    Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Private Sub banques_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Dim appPath As String = Application.StartupPath()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = System.IO.Path.Combine(SaveDirectory, imgName)
        If System.IO.File.Exists(SavePath) Then
            PictureBox2.Image = Image.FromFile(SavePath)
        End If

        Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))


        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim tabl3 As New DataTable
        adpt.Fill(tabl3)

        adpt = New MySqlDataAdapter("select * from caisses", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)

        For i = 0 To tabl3.Rows.Count() - 1
            ComboBox1.Items.Add(tabl3.Rows(i).Item(1))
        Next
        For j = 0 To table2.Rows.Count() - 1
            ComboBox1.Items.Add(table2.Rows(j).Item(1))
        Next
        ComboBox1.SelectedIndex = 0
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
        adpt = New MySqlDataAdapter("select * from cheques WHERE Day(cheques.date_confirm) = @day and month(cheques.date_confirm)=@month and year(cheques.date_confirm)=@year", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
        adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
        adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        Dim libel As String
        Dim crd As Double = 0
        Dim deb As Double = 0

        For i = 0 To table.Rows.Count - 1

            If IsDBNull(table.Rows(i).Item(8)) Then

                libel = "Encaissement Client par " & " " & table.Rows(i).Item(15) & " N°: " & table.Rows(i).Item(4) & vbCrLf & " A l'échéance de " & Convert.ToDateTime(table.Rows(i).Item(5)).ToString("dd/MM/yyyy") & "  |  " & "Clt => " & table.Rows(i).Item(14)
                DataGridView1.Rows.Add(Format(table.Rows(i).Item(11), "dd/MM/yyyy"), libel, Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), 0.00)
                DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Red
            End If
            If Not IsDBNull(table.Rows(i).Item(8)) Then
                libel = " " & " " & " " & " " & "       " & "Règlement Fournisseur par" & " " & table.Rows(i).Item(15) & " N°: " & table.Rows(i).Item(4) & vbCrLf & " " & " " & " " & " " & "       " & " A l'échéance de " & Convert.ToDateTime(table.Rows(i).Item(5)).ToString("dd/MM/yyyy") & "  |  " & "Frs => " & table.Rows(i).Item(14)
                DataGridView1.Rows.Add(Format(table.Rows(i).Item(11), "dd/MM/yyyy"), libel, 0.00, Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
                DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Red
            End If

        Next

        For j = 0 To DataGridView1.Rows.Count() - 1
            crd += DataGridView1.Rows(j).Cells(3).Value
            deb += DataGridView1.Rows(j).Cells(2).Value
        Next
        Dim solde As Double = deb - crd
        If deb > crd Then
            Label3.Text = "Solde débiteur de :"
            Label5.Text = Convert.ToDecimal(solde.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2") & " DHs"
            Label5.ForeColor = Color.Green
        Else
            Label3.Text = "Solde créditeur de :"
            Label5.Text = Convert.ToDecimal(solde.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2") & " DHs"
            Label5.ForeColor = Color.Red
        End If

        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Gestion des mouvements'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then
            Else
                Panel1.Visible = True
                Panel1.BringToFront()
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub


    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click

        DataGridView1.Rows.Clear()
        If ComboBox1.Text = "Global" Then
            adpt = New MySqlDataAdapter("select * from cheques where etat = 1 and date_confirm BETWEEN @datedebut AND @datefin", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
        Else
            adpt = New MySqlDataAdapter("select * from cheques WHERE etat = 1 AND bq = @bq and date_confirm BETWEEN @datedebut AND @datefin", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
            adpt.SelectCommand.Parameters.AddWithValue("@bq", ComboBox1.Text)
        End If

        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        Dim libel As String
        Dim crd As Double = 0
        Dim deb As Double = 0

        For i = 0 To table.Rows.Count - 1

            If IsDBNull(table.Rows(i).Item(8)) Then

                libel = "Encaissement Client par " & " " & table.Rows(i).Item(15) & " N°: " & table.Rows(i).Item(4) & vbCrLf & " A l'échéance de " & Convert.ToDateTime(table.Rows(i).Item(5)).ToString("dd/MM/yyyy") & "  |  " & "Clt => " & table.Rows(i).Item(14)
                DataGridView1.Rows.Add(Format(table.Rows(i).Item(11), "dd/MM/yyyy"), libel, Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), 0.00)
                DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Red
            End If
            If Not IsDBNull(table.Rows(i).Item(8)) Then
                libel = " " & " " & " " & " " & "       " & "Règlement Fournisseur par" & " " & table.Rows(i).Item(15) & " N°: " & table.Rows(i).Item(4) & vbCrLf & " " & " " & " " & " " & "       " & " A l'échéance de " & Convert.ToDateTime(table.Rows(i).Item(5)).ToString("dd/MM/yyyy") & "  |  " & "Frs => " & table.Rows(i).Item(14)
                DataGridView1.Rows.Add(Format(table.Rows(i).Item(11), "dd/MM/yyyy"), libel, 0.00, Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
                DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Red
            End If

        Next

        If ComboBox1.Text = "Global" Then
            adpt = New MySqlDataAdapter("select * from caisse where date BETWEEN @datedebut AND @datefin", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
        Else
            adpt = New MySqlDataAdapter("select * from caisse WHERE compte = @bq and date BETWEEN @datedebut AND @datefin", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
            adpt.SelectCommand.Parameters.AddWithValue("@bq", ComboBox1.Text)
        End If
        Dim table5 As New DataTable
        adpt.Fill(table5)
        For i = 0 To table5.Rows.Count - 1

            If Convert.ToDouble(table5.Rows(i).Item(2)) = 0 Then

                libel = "Alimentation en espèce du compte : " & table5.Rows(i).Item(3)
                DataGridView1.Rows.Add(Format(table5.Rows(i).Item(4), "dd/MM/yyyy"), libel, Convert.ToDouble(table5.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), 0.00)
                DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Red
            Else
                libel = " " & " " & " " & " " & "       " & "Retrait en espèce du compte : " & table5.Rows(i).Item(3)
                DataGridView1.Rows.Add(Format(table5.Rows(i).Item(4), "dd/MM/yyyy"), libel, 0.00, Convert.ToDouble(table5.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
                DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Red
            End If

        Next


        For j = 0 To DataGridView1.Rows.Count() - 1
            crd += DataGridView1.Rows(j).Cells(3).Value
            deb += DataGridView1.Rows(j).Cells(2).Value
        Next
        Dim solde As Double = deb - crd
        If deb > crd Then
            Label3.Text = "Solde débiteur de :"
            Label5.Text = Convert.ToDecimal(solde.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2") & " DHs"
            Label5.ForeColor = Color.Green
        Else
            Label3.Text = "Solde créditeur de :"
            Label5.Text = Convert.ToDecimal(solde.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2") & " DHs"
            Label5.ForeColor = Color.Red
        End If

        Dim dateColumn As DataGridViewColumn = DataGridView1.Columns(0)
        If dateColumn IsNot Nothing AndAlso TypeOf dateColumn Is DataGridViewTextBoxColumn Then
            ' Trier le DataGridView par la colonne de date
            DataGridView1.Sort(dateColumn, ListSortDirection.Descending)
        End If

    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        DataGridView1.Rows.Clear()

        adpt = New MySqlDataAdapter("select * from cheques WHERE Day(cheques.date_confirm) = @day and month(cheques.date_confirm)=@month and year(cheques.date_confirm)=@year", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
        adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
        adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        Dim libel As String

        Dim crd As Double = 0
        Dim deb As Double = 0
        For i = 0 To table.Rows.Count - 1

            If Not IsDBNull(table.Rows(i).Item(7)) AndAlso table.Rows(i).Item(5) <> "tpe" Then
                libel = " " & " " & " " & " " & " " & " Ventes de marchandises Fac N° " & table.Rows(i).Item(7) & vbCrLf & " " & " " & " " & " " & " " & " Encaissement par chèque N° " & table.Rows(i).Item(4) & vbCrLf & " " & " " & " " & " " & " " & " A l'échéance de " & table.Rows(i).Item(5)
                DataGridView1.Rows.Add(Format(table.Rows(i).Item(11), "dd/MM/yyyy"), libel, 0, table.Rows(i).Item(12))
                DataGridView1.Rows(i).Cells(2).Style.ForeColor = Color.Red
            End If
            If Not IsDBNull(table.Rows(i).Item(8)) AndAlso table.Rows(i).Item(5) <> "tpe" Then
                libel = "Achats de marchandises Achat N° " & table.Rows(i).Item(8) & vbCrLf & "Règlement par chèque N° " & table.Rows(i).Item(4) & vbCrLf & "A l' échéance de " & table.Rows(i).Item(5)
                DataGridView1.Rows.Add(Format(table.Rows(i).Item(11), "dd/MM/yyyy"), libel, table.Rows(i).Item(12), 0)
                DataGridView1.Rows(i).Cells(2).Style.ForeColor = Color.Red
            End If
            If Not IsDBNull(table.Rows(i).Item(7)) AndAlso table.Rows(i).Item(5) = "tpe" Then
                libel = " " & " " & " " & " " & " " & " Ventes de marchandises Fac N° " & table.Rows(i).Item(7) & vbCrLf & "" & " " & " " & " " & " " & " Encaissement par Carte bancaire, N° d'autorisation " & table.Rows(i).Item(4) & vbCrLf & " " & " " & " " & " " & " " & " STAN N° " & table.Rows(i).Item(2)
                DataGridView1.Rows.Add(Format(table.Rows(i).Item(11), "dd/MM/yyyy"), libel, 0, table.Rows(i).Item(12))
                DataGridView1.Rows(i).Cells(2).Style.ForeColor = Color.Red
            End If
            If Not IsDBNull(table.Rows(i).Item(8)) AndAlso table.Rows(i).Item(5) = "tpe" Then
                libel = "Achats de marchandises Achat N° " & table.Rows(i).Item(8) & vbCrLf & "Règlement par Carte bancaire, N° d'autorisation " & table.Rows(i).Item(4) & vbCrLf & "STAN N° " & table.Rows(i).Item(2)
                DataGridView1.Rows.Add(Format(table.Rows(i).Item(11), "dd/MM/yyyy"), libel, table.Rows(i).Item(12), 0)
                DataGridView1.Rows(i).Cells(2).Style.ForeColor = Color.Red
            End If
        Next

        For j = 0 To DataGridView1.Rows.Count() - 1
            crd += DataGridView1.Rows(j).Cells(3).Value
            deb += DataGridView1.Rows(j).Cells(2).Value
        Next
        Dim solde As Double = crd - deb
        If deb > crd Then
            Label3.Text = "Solde débiteur de :"
            solde = solde * -1
            Label5.Text = solde.ToString("N2") & " DHs"
            Label5.ForeColor = Color.Red
        Else
            Label3.Text = "Solde créditeur de :"
            Label5.Text = solde.ToString("N2") & " DHs"
            Label5.ForeColor = Color.Green
        End If

        DateTimePicker1.Text = Today
        DateTimePicker2.Text = Today


    End Sub

    Private Sub IconButton16_Click(sender As Object, e As EventArgs) Handles IconButton16.Click
        Dim Rep As Integer
        Rep = MsgBox("Voulez-vous vraiment quitter ?", vbYesNo)
        If Rep = vbYes Then
            Dim log = New Form1()
            log.Show()
            Me.Close()
        End If
    End Sub


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub IconButton10_Click(sender As Object, e As EventArgs)
        ajout.Show()
    End Sub
    Dim m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Dim ReportToPrint As LocalReport
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)
        Return stream
    End Function
    Private Sub IconButton18_Click(sender As Object, e As EventArgs) Handles IconButton18.Click
        Dim dte As String
        Dim bq As String
        If DateTimePicker1.Text = DateTimePicker2.Text Then
            dte = "Journal du " & DateTimePicker1.Text
        Else

            dte = "Journal du " & DateTimePicker1.Text & " au " & DateTimePicker2.Text
        End If
        If ComboBox1.Text = "Global" Then
            bq = "global des banques"
        Else
            bq = "de la banque " & ComboBox1.Text
        End If
        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("journalbq")
        For i = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value)
        Next
        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\Report5.rdlc"
        ReportToPrint.DataSources.Clear()

        Dim dateM As New ReportParameter("date", dte)
        Dim dateM1(0) As ReportParameter
        dateM1(0) = dateM
        ReportToPrint.SetParameters(dateM1)

        Dim banqueM As New ReportParameter("banque", bq)
        Dim banqueM1(0) As ReportParameter
        banqueM1(0) = banqueM
        ReportToPrint.SetParameters(banqueM1)

        Dim soldeM As New ReportParameter("solde", Label5.Text)
        Dim soldeM1(0) As ReportParameter
        soldeM1(0) = soldeM
        ReportToPrint.SetParameters(soldeM1)

        Dim typesoldeM As New ReportParameter("typesolde", Label3.Text)
        Dim typesoldeM1(0) As ReportParameter
        typesoldeM1(0) = typesoldeM
        ReportToPrint.SetParameters(typesoldeM1)


        ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
        ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next
        Dim printDoc As New PrintDocument()
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

    Private Sub IconButton10_Click_1(sender As Object, e As EventArgs) Handles IconButton10.Click
        rapproche.ShowDialog()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Me.Show()
    End Sub

    Private Sub iconbutton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        stock.Show()
        Me.Close()
    End Sub

    Private Sub iconbutton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
        dashboard.Show()
        dashboard.IconButton9.Visible = True
        dashboard.IconButton8.Visible = False
    End Sub
    Private Sub IconButton8_Click_1(sender As Object, e As EventArgs) Handles IconButton8.Click
        product.Show()
        Me.Close()
    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Client.Show()
        Me.Close()
    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click
        achats.Show()
        Me.Close()
    End Sub

    Private Sub IconButton15_Click(sender As Object, e As EventArgs) Handles IconButton15.Click
        devis.Show()
        Me.Close()
    End Sub
    Private Sub IconButton23_Click(sender As Object, e As EventArgs) Handles IconButton23.Click
        Charges.Show()
        Me.Close()
    End Sub

    Private Sub iconbutton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        factures.Show()
        Me.Close()
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        archive.Show()
        Me.Close()

    End Sub

    Private Sub IconButton8_Click(sender As Object, e As EventArgs)
        product.Show()
        Me.Close()

    End Sub

    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        Home.Show()
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        DataGridView1.Rows.Clear()
        If ComboBox1.Text = "Global" Then
            adpt = New MySqlDataAdapter("select * from cheques where etat = 1", conn2)
        Else
            adpt = New MySqlDataAdapter("select * from cheques WHERE etat = 1 AND bq = @bq", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@bq", ComboBox1.Text)
        End If

        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        Dim libel As String
        Dim crd As Double = 0
        Dim deb As Double = 0
        Dim daty As String = ""
        If table.Rows.Count <> 0 Then

            For i = 0 To table.Rows.Count - 1

                If IsDBNull(table.Rows(i).Item(8)) Then

                    If table.Rows(i).Item(5) = "tpe" Then
                        daty = "-"
                    Else
                        daty = Convert.ToDateTime(table.Rows(i).Item(5)).ToString("dd/MM/yyyy")
                    End If
                    libel = "Encaissement Client par " & " " & table.Rows(i).Item(15) & " N°: " & table.Rows(i).Item(4) & vbCrLf & " A l'échéance de " & daty & "  |  " & "Clt => " & table.Rows(i).Item(14)
                    DataGridView1.Rows.Add(Format(table.Rows(i).Item(11), "dd/MM/yyyy"), libel, Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), 0.00)
                    DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Red
                End If
                If Not IsDBNull(table.Rows(i).Item(8)) Then
                    If table.Rows(i).Item(5) <> "tpe" Then
                        libel = " " & " " & " " & " " & "       " & "Règlement Fournisseur par" & " " & table.Rows(i).Item(15) & " N°: " & table.Rows(i).Item(4) & vbCrLf & " " & " " & " " & " " & "       " & " A l'échéance de " & Convert.ToDateTime(table.Rows(i).Item(5)).ToString("dd/MM/yyyy") & "  |  " & "Frs => " & table.Rows(i).Item(14)
                    Else
                        libel = " " & " " & " " & " " & "       " & "Règlement Fournisseur par" & " " & table.Rows(i).Item(15) & " N°: " & table.Rows(i).Item(4) & vbCrLf & " " & " " & " " & " " & "       " & "  |  " & "Frs => " & table.Rows(i).Item(14)
                    End If
                    DataGridView1.Rows.Add(Format(table.Rows(i).Item(11), "dd/MM/yyyy"), libel, 0.00, Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
                    DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Red
                End If

            Next

        End If
        If ComboBox1.Text = "Global" Then
            adpt = New MySqlDataAdapter("select * from caisse", conn2)
        Else
            adpt = New MySqlDataAdapter("select * from caisse WHERE compte = @bq", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@bq", ComboBox1.Text)
        End If
        Dim table5 As New DataTable
        adpt.Fill(table5)
        For i = 0 To table5.Rows.Count - 1

            If Convert.ToDouble(table5.Rows(i).Item(2)) = 0 Then

                libel = "Alimentation en espèce du compte : " & table5.Rows(i).Item(3)
                DataGridView1.Rows.Add(Format(table5.Rows(i).Item(4), "dd/MM/yyyy"), libel, Convert.ToDouble(table5.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), 0.00)
                DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Red
            Else
                libel = " " & " " & " " & " " & "       " & "Retrait en espèce du compte : " & table5.Rows(i).Item(3)
                DataGridView1.Rows.Add(Format(table5.Rows(i).Item(4), "dd/MM/yyyy"), libel, 0.00, Convert.ToDouble(table5.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
                DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Red
            End If

        Next


        For j = 0 To DataGridView1.Rows.Count() - 1
            crd += DataGridView1.Rows(j).Cells(3).Value
            deb += DataGridView1.Rows(j).Cells(2).Value
        Next
        Dim solde As Double = deb - crd
        If deb > crd Then
            Label3.Text = "Solde débiteur de :"
            Label5.Text = Convert.ToDecimal(solde.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2") & " DHs"
            Label5.ForeColor = Color.Green
        Else
            Label3.Text = "Solde créditeur de :"
            Label5.Text = Convert.ToDecimal(solde.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2") & " DHs"
            Label5.ForeColor = Color.Red
        End If
        Dim dateColumn As DataGridViewColumn = DataGridView1.Columns(0)
        If dateColumn IsNot Nothing AndAlso TypeOf dateColumn Is DataGridViewTextBoxColumn Then
            ' Trier le DataGridView par la colonne de date
            DataGridView1.Sort(dateColumn, ListSortDirection.Descending)
        End If
    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Four.Show()
        Me.Close()

    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        users.Show()
        Me.Close()

    End Sub

End Class