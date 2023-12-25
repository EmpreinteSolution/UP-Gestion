Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Imports System.Drawing.Imaging
Imports System.Drawing.Printing

Public Class achats
    Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter

    <Obsolete>
    Private Sub achats_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler DataGridView1.RowPrePaint, AddressOf DataGridView1_RowPrePaint
        DataGridView1.MultiSelect = True
        DataGridView1.ClearSelection()

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
        adpt = New MySqlDataAdapter("select * from achat WHERE REPLACE(reste,',','.') > 0 order by id desc", conn2)
        adpt.SelectCommand.Parameters.Clear()
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        For i = 0 To table.Rows.Count - 1


            Dim s As Double = 0
            Dim t As Double = 0
            Dim r As Double = 0
            s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")
            t = Convert.ToString(table.Rows(i).Item(4)).Replace(".", ",")
            r = Convert.ToString(table.Rows(i).Item(5)).Replace(".", ",")



            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(8), s.ToString("N2"), t.ToString("N2"), r.ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))

            sum = sum + Convert.ToDouble(table.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ","))
        Next

        IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00") & " Dhs"
        IconButton2.Text = table.Rows.Count

        adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count <> 0 Then
            ComboBox3.Items.Clear()
            ComboBox3.Items.Add("Tout")
            For i = 0 To table2.Rows.Count - 1
                ComboBox3.Items.Add(table2.Rows(i).Item(0))
            Next
            ComboBox3.SelectedIndex = 0

            ComboBox4.Items.Clear()
            ComboBox4.Items.Add("Tout")
            For i = 0 To table2.Rows.Count - 1
                ComboBox4.Items.Add(table2.Rows(i).Item(0))
            Next
            ComboBox4.SelectedIndex = 0

        End If
        ComboBox1.SelectedIndex = 0

        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim tablebq As New DataTable
        adpt.Fill(tablebq)

        For i = 0 To tablebq.Rows.Count - 1
            ComboBox8.Items.Add(tablebq.Rows(i).Item(1))
            ComboBox7.Items.Add(tablebq.Rows(i).Item(1))
        Next
        Dim printerticket As String = System.Configuration.ConfigurationSettings.AppSettings("printerticket")
        Dim printera5 As String = System.Configuration.ConfigurationSettings.AppSettings("printera5")
        Dim printera4 As String = System.Configuration.ConfigurationSettings.AppSettings("printera4")

        receiptprinter = printerticket
        a4printer = printera4
        a5printer = printera5
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(3).Value <= 0 Then
                row.Cells(3).Style.ForeColor = Color.Red
                row.Cells(4).Style.ForeColor = Color.Red
            Else
                row.Cells(3).Style.ForeColor = Color.Green
                row.Cells(4).Style.ForeColor = Color.Red
            End If
        Next
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
    Private Sub DataGridView1_CellMouseClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick

        If e.RowIndex >= 0 Then

            If DataGridView1.Columns.Count = 9 Then


                achatedit.Show()
                adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
                Dim tableimg As New DataTable
                adpt.Fill(tableimg)

                For i = 0 To tableimg.Rows.Count() - 1
                    achatedit.ComboBox5.Items.Add(tableimg.Rows(i).Item(0))
                Next

                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim id As String = row.Cells(0).Value.ToString
                achatedit.Label11.Text = id
                If row.Cells(2).Value > 0 Then
                    achatedit.Label16.Text = "Reception :"
                Else
                    achatedit.Label16.Text = "Avoir :"
                End If
                achatedit.Label31.Text = row.Cells(6).Value.ToString
                achatedit.ComboBox5.Text = row.Cells(6).Value.ToString

                If row.Cells(2).Value > 0 AndAlso row.Cells(4).Value <= 0 Then
                    achatedit.Label32.Text = "Payée"
                    achatedit.Label32.ForeColor = Color.ForestGreen
                End If

                achatedit.DataGridView2.Rows.Add(0 & " %", 0, 0, 0)
                achatedit.DataGridView2.Rows.Add(7 & " %", 0, 0, 0)
                achatedit.DataGridView2.Rows.Add(10 & " %", 0, 0, 0)
                achatedit.DataGridView2.Rows.Add(14 & " %", 0, 0, 0)
                achatedit.DataGridView2.Rows.Add(20 & " %", 0, 0, 0)

                Dim sumttc As Double = 0
                Dim sumht As Double = 0
                Dim sumrms As Double = 0
                Dim sumtva As Double = 0

                adpt = New MySqlDataAdapter("select `rms`,`mtn_ht` from achat where id = '" & id & "' ", conn2)
                Dim tablerms As New DataTable
                adpt.Fill(tablerms)

                adpt = New MySqlDataAdapter("select `CodeArticle`, `NameArticle`, `qte`,`PA_HT`,`TVA`,`PA_TTC`,`total`,`TM`,`PV_HT`,`PV_TTC`,`rms`,`rt`,`gr`,`total_ht`,`facture`  from achatdetails where achat_Id = '" & id & "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                Dim ord As Integer = 0
                For i = 0 To table.Rows.Count - 1
                    ord += 1
                    If table.Rows.Count() <> 0 Then

                        achatedit.DataGridView1.Rows.Add(ord.ToString("N0"), table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), Convert.ToDouble(table.Rows(i).Item(4)).ToString("N0"), table.Rows(i).Item(5), table.Rows(i).Item(13), table.Rows(i).Item(6), table.Rows(i).Item(7), table.Rows(i).Item(8), table.Rows(i).Item(9), Nothing, Nothing, Nothing, Nothing, Nothing, table.Rows(i).Item(10), table.Rows(i).Item(3), table.Rows(i).Item(11), table.Rows(i).Item(12), Convert.ToDouble(ord.ToString("N0")), table.Rows(i).Item(14))
                    Else
                        Dim yesornoMsg As New yesorno
                        yesornoMsg.Panel5.Visible = True
                        yesornoMsg.Label14.Text = "Bon de réception Vide, Veuillez resaisir le Bon."
                        yesornoMsg.ShowDialog()
                    End If
                Next



                Dim ht, ht7, ht10, ht14, ht20 As Double
                Dim tva, tva7, tva10, tva14, tva20 As Double
                Dim sum0 As Double
                Dim sum7 As Double
                Dim sum10 As Double
                Dim sum14 As Double
                Dim sum20 As Double

                For j = 0 To achatedit.DataGridView1.Rows.Count() - 1

                    sumttc = sumttc + achatedit.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                    sumrms = sumrms + (achatedit.DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * achatedit.DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") * (achatedit.DataGridView1.Rows(j).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                    sumht = sumht + (achatedit.DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * achatedit.DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")) * (1 - (achatedit.DataGridView1.Rows(j).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                    If achatedit.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",") >= 0 Then
                        sumtva = sumtva + ((achatedit.DataGridView1.Rows(j).Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") - achatedit.DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",")) * (achatedit.DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - achatedit.DataGridView1.Rows(j).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ",")))
                    End If
                    If achatedit.DataGridView1.Rows(j).Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",") = 0 Then
                        sum0 += achatedit.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                        ht += achatedit.DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * (achatedit.DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - achatedit.DataGridView1.Rows(j).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                        tva = 0

                    End If

                    If achatedit.DataGridView1.Rows(j).Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",") = 7 Then
                        sum7 += achatedit.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                        ht7 += achatedit.DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * (achatedit.DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - achatedit.DataGridView1.Rows(j).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                        tva7 = (ht7 * 7) / 100
                    End If

                    If achatedit.DataGridView1.Rows(j).Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",") = 10 Then
                        sum10 += achatedit.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                        ht10 += achatedit.DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * (achatedit.DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - achatedit.DataGridView1.Rows(j).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                        tva10 = (ht10 * 10) / 100
                    End If

                    If achatedit.DataGridView1.Rows(j).Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",") = 14 Then
                        sum14 += achatedit.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                        ht14 += achatedit.DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * (achatedit.DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - achatedit.DataGridView1.Rows(j).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                        tva14 = (ht14 * 14) / 100
                    End If
                    If achatedit.DataGridView1.Rows(j).Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",") = 20 Then
                        sum20 += achatedit.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                        ht20 += achatedit.DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * (achatedit.DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - achatedit.DataGridView1.Rows(j).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                        tva20 = (ht20 * 20) / 100
                    End If

                Next

                achatedit.DataGridView2.Rows(0).Cells(1).Value = Convert.ToDouble(ht.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(0).Cells(2).Value = Convert.ToDouble(tva.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(0).Cells(3).Value = Convert.ToDouble(sum0.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(1).Cells(1).Value = Convert.ToDouble(ht7.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(1).Cells(2).Value = Convert.ToDouble(tva7.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(1).Cells(3).Value = Convert.ToDouble(sum7.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(2).Cells(1).Value = Convert.ToDouble(ht10.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(2).Cells(2).Value = Convert.ToDouble(tva10.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(2).Cells(3).Value = Convert.ToDouble(sum10.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(3).Cells(1).Value = Convert.ToDouble(ht14.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(3).Cells(2).Value = Convert.ToDouble(tva14.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(3).Cells(3).Value = Convert.ToDouble(sum14.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(4).Cells(1).Value = Convert.ToDouble(ht20.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(4).Cells(2).Value = Convert.ToDouble(tva20.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                achatedit.DataGridView2.Rows(4).Cells(3).Value = Convert.ToDouble(sum20.ToString.Replace(".", ",").Replace(" ", "")).ToString("# #00.00")
                If row.Cells(2).Value < 0 Then
                    achatedit.Label51.Text = Convert.ToDouble(row.Cells(2).Value.ToString.Replace(".", ",")).ToString("# #00.00")
                    achatedit.Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("# #00.00")
                    achatedit.Label27.Text = Convert.ToDouble(sumrms.ToString.Replace(".", ",")).ToString("# #00.00")
                    achatedit.Label10.Text = Convert.ToDouble(sumtva.ToString.Replace(".", ",")).ToString("# #00.00")

                Else
                    achatedit.Label27.Text = Convert.ToDouble(tablerms.Rows(0).Item(0)).ToString("# #00.00")
                    achatedit.Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("# #00.00")
                    achatedit.Label51.Text = Convert.ToDouble(row.Cells(2).Value.ToString.Replace(".", ",")).ToString("# #00.00")
                    achatedit.Label10.Text = Convert.ToDouble(sumtva.ToString.Replace(".", ",")).ToString("# #00.00")

                End If



                adpt = New MySqlDataAdapter("select Fournisseur,timbre from achat where id = '" + achatedit.Label11.Text.ToString + "' ", conn2)
                Dim table3 As New DataTable
                adpt.Fill(table3)
                achatedit.Label53.Text = table3.Rows(0).Item(1)
                achatedit.Label15.Text = achatedit.Label51.Text.ToString.Replace(" ", "") - Convert.ToDouble(table3.Rows(0).Item(1).ToString.Replace(" ", ""))

                If IsDBNull(table3.Rows(0).Item(0)) Then
                    adpt2 = New MySqlDataAdapter("select name,adresse,ville,ice from fours where name = '" + table3.Rows(0).Item(0).ToString + "' ", conn2)
                    Dim table2 As New DataTable
                    adpt2.Fill(table2)

                    achatedit.Label7.Text = table2.Rows(0).Item(0)
                    achatedit.Label8.Text = table2.Rows(0).Item(1)
                    achatedit.Label12.Text = table2.Rows(0).Item(2)

                    Dim output As StringBuilder = New StringBuilder
                    For i = 0 To table2.Rows(0).Item(3).Length - 1
                        If IsNumeric(table2.Rows(0).Item(3)(i)) Then
                            output.Append(table2.Rows(0).Item(3)(i))
                        End If
                    Next
                    achatedit.Label2.Text = output.ToString()
                End If

                If row.Cells(2).Value < 0 Then
                    achatedit.TextBox1.Text = "Avoir N° " & id
                Else
                    achatedit.TextBox1.Text = "Reception N° " & id
                End If
                achatedit.TextBox10.Text = row.Cells(1).Value.ToString
                achatedit.Label6.Text = row.Cells(5).Value.ToString

            Else

                Dim totalCumule As Decimal = 0

                If e.ColumnIndex = 0 AndAlso e.RowIndex >= 0 Then
                    ' Inverse l'état de la case à cocher
                    If DataGridView1.Rows(e.RowIndex).Cells(3).Value() < 0 Then
                        If DataGridView1.Rows(e.RowIndex).Cells(5).Value() = 0 Then
                            Dim yesornoMsg As New yesorno
                            yesornoMsg.Panel5.Visible = True
                            yesornoMsg.Label14.Text = "Cette facture est déjà réglée !"
                            yesornoMsg.ShowDialog()
                        Else

                            Dim cell As DataGridViewCheckBoxCell = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)
                            cell.Value = Not Convert.ToBoolean(cell.Value)
                        End If
                    Else
                        If DataGridView1.Rows(e.RowIndex).Cells(5).Value() <= 0 Then
                            Dim yesornoMsg As New yesorno
                            yesornoMsg.Panel5.Visible = True
                            yesornoMsg.Label14.Text = "Cette facture est déjà réglée !"
                            yesornoMsg.ShowDialog()
                        Else

                            Dim cell As DataGridViewCheckBoxCell = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)
                            cell.Value = Not Convert.ToBoolean(cell.Value)
                        End If

                    End If
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        Dim cell As DataGridViewCheckBoxCell = row.Cells(0)
                        If Convert.ToBoolean(cell.Value) Then
                            Dim valeurColonne As Decimal = Convert.ToDecimal(row.Cells(5).Value) ' Remplacez "NomDeLaColonne" par le nom réel de la colonne
                            totalCumule += valeurColonne
                        End If
                    Next
                    IconButton22.Text = totalCumule.ToString("# ##0.00")
                End If



            End If
        End If

    End Sub
    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs)

    End Sub



    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Try
            If ComboBox3.Text = "Tout" Then
                If ComboBox1.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("SELECT * FROM achat WHERE dateAchat BETWEEN @datedebut AND @datefin order by id desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                    adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)
                End If
                If ComboBox1.Text = "Payée" Then
                    adpt = New MySqlDataAdapter("SELECT * FROM achat WHERE dateAchat BETWEEN @datedebut AND @datefin and reste <= 0 order by id desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                    adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)
                End If
                If ComboBox1.Text = "Non Payée" Then
                    adpt = New MySqlDataAdapter("SELECT * FROM achat WHERE dateAchat BETWEEN @datedebut AND @datefin and reste > 0 order by id desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                    adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)
                End If
            Else
                If ComboBox1.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("SELECT * FROM achat WHERE dateAchat BETWEEN @datedebut AND @datefin and Fournisseur = @frs order by id desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                    adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)
                    adpt.SelectCommand.Parameters.AddWithValue("@frs", ComboBox3.Text)
                End If
                If ComboBox1.Text = "Payée" Then
                    adpt = New MySqlDataAdapter("SELECT * FROM achat WHERE dateAchat BETWEEN @datedebut AND @datefin and Fournisseur = @frs and reste <= 0 order by id desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                    adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)
                    adpt.SelectCommand.Parameters.AddWithValue("@frs", ComboBox3.Text)
                End If
                If ComboBox1.Text = "Non Payée" Then
                    adpt = New MySqlDataAdapter("SELECT * FROM achat WHERE dateAchat BETWEEN @datedebut AND @datefin and Fournisseur = @frs and reste > 0 order by id desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                    adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)
                    adpt.SelectCommand.Parameters.AddWithValue("@frs", ComboBox3.Text)
                End If
            End If
            Dim table As New DataTable
            adpt.Fill(table)
            Dim sum As Double = 0
            Dim cnt As Integer = 0
            DataGridView1.Rows.Clear()
            ventedetail.DataGridView3.Rows.Clear()

            For i = 0 To table.Rows.Count - 1
                Dim s As Double = 0
                Dim t As Double = 0
                Dim r As Double = 0
                s = table.Rows(i).Item(1).ToString.Replace(".", ",").Replace(" ", "")
                t = table.Rows(i).Item(4).ToString.Replace(".", ",").Replace(" ", "")
                r = table.Rows(i).Item(5).ToString.Replace(".", ",").Replace(" ", "")



                DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(8), s.ToString("N2"), t.ToString("N2"), r.ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",").Replace(" ", "")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))

                sum = sum + Convert.ToDouble(s)
            Next
            IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00") & " Dhs"
            IconButton2.Text = table.Rows.Count
            DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
        Catch ex As MySqlException
            Dim yesornoMsg As New eror

            yesornoMsg.Label14.Text = ex.Message
            yesornoMsg.ShowDialog()
        End Try
        TextBox14.Text = ""
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(3).Value < row.Cells(2).Value Then
                row.DefaultCellStyle.ForeColor = Color.Red
            Else
                row.DefaultCellStyle.ForeColor = Color.Black
            End If
        Next
    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        Try
            DataGridView1.Rows.Clear()

            adpt = New MySqlDataAdapter("select * from achat WHERE Day(achat.dateAchat) = @day and month(achat.dateAchat)=@month and year(achat.dateAchat)=@year order by id desc", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
            adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
            adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
            Dim table As New DataTable
            adpt.Fill(table)
            Dim sum As Double = 0
            Dim cnt As Integer = 0
            For i = 0 To table.Rows.Count - 1


                Dim s As Double = 0
                Dim t As Double = 0
                Dim r As Double = 0
                s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")
                t = Convert.ToString(table.Rows(i).Item(4)).Replace(".", ",")
                r = Convert.ToString(table.Rows(i).Item(5)).Replace(".", ",")



                DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(8), s.ToString("N2"), t.ToString("N2"), r.ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))

                sum = sum + Convert.ToDouble(s)
            Next

            IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00") & " Dhs"
            IconButton2.Text = table.Rows.Count
            DateTimePicker1.Text = Today
            DateTimePicker2.Text = Today
        Catch ex As MySqlException
            Dim yesornoMsg As New eror
            yesornoMsg.Label14.Text = ex.Message
            yesornoMsg.ShowDialog()
        End Try

    End Sub

    Private Sub IconButton16_Click(sender As Object, e As EventArgs) Handles IconButton16.Click
        Dim yesornoMsg As New yesorno
        yesornoMsg.Label14.Text = "Voulez-vous vraiment quitter ?"
        yesornoMsg.IconButton1.Visible = False
        yesornoMsg.ShowDialog()
        If Msgboxresult = True Then
            Dim log = New Form1()
            log.Show()
            Me.Close()
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)

    End Sub


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub IconButton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Ajouter Reception'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                ajoutachat.Show()
                ajoutachat.TextBox25.Enabled = True
            Else
                Dim yesornoMsg As New eror

                yesornoMsg.Label14.Text = "Vous n'avez pas l'autorisation !"
                yesornoMsg.ShowDialog()

            End If
        End If

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

        Dim dte As String = DateTimePicker1.Text & " - " & DateTimePicker2.Text

        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("achat")
        For i = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value)
        Next
        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\achatetat.rdlc"
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

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            DataGridView1.Rows.Clear()
            If TextBox1.Text = "" Then
                adpt = New MySqlDataAdapter("select * from achat WHERE Day(achat.dateAchat) = @day and month(achat.dateAchat)=@month and year(achat.dateAchat)=@year order by id desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
                adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
                adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                For i = 0 To table.Rows.Count - 1


                    Dim s As Double = 0
                    Dim t As Double = 0
                    Dim r As Double = 0
                    s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")
                    t = Convert.ToString(table.Rows(i).Item(5)).Replace(".", ",")
                    r = Convert.ToString(table.Rows(i).Item(6)).Replace(".", ",")



                    DataGridView1.Rows.Add(table.Rows(i).Item(0), s.ToString("N2"), t.ToString("N2"), r.ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(4), table.Rows(i).Item(2), table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))

                    sum = sum + Convert.ToDouble(s)
                Next

                IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00") & " Dhs"
                IconButton2.Text = table.Rows.Count

            Else
                adpt = New MySqlDataAdapter("select * from achat WHERE dateAchat BETWEEN @datedebut AND @datefin AND Fournisseur LIKE @va  order by id desc ", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@va", "%" + TextBox1.Text + "%")
                adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                For i = 0 To table.Rows.Count - 1


                    Dim s As Double = 0
                    Dim t As Double = 0
                    Dim r As Double = 0
                    s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")
                    t = Convert.ToString(table.Rows(i).Item(5)).Replace(".", ",")
                    r = Convert.ToString(table.Rows(i).Item(6)).Replace(".", ",")

                    DataGridView1.Rows.Add(table.Rows(i).Item(0), s.ToString("N2"), t.ToString("N2"), r.ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(4), table.Rows(i).Item(2), table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))

                    sum = sum + Convert.ToDouble(s)
                Next

                IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00") & " Dhs"
                IconButton2.Text = table.Rows.Count

            End If
        End If
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Me.Show()
    End Sub

    Private Sub IconButton14_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        factures.Show()
        Me.Close()
    End Sub

    Private Sub IconButton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
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


    Private Sub IconButton15_Click(sender As Object, e As EventArgs) Handles IconButton15.Click
        devis.Show()
        Me.Close()
    End Sub
    Private Sub IconButton23_Click(sender As Object, e As EventArgs) Handles IconButton23.Click
        Charges.Show()
        Me.Close()
    End Sub

    Private Sub IconButton24_Click(sender As Object, e As EventArgs) Handles IconButton24.Click
        banques.Show()
        Me.Close()
    End Sub
    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton18.Click
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

    Private Sub IconButton14_Click_1(sender As Object, e As EventArgs) Handles IconButton14.Click
        stock.Show()
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

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
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Modifier Reception'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Panel1.Visible = True
                If DataGridView1.Columns.Count = 9 Then
                    ' Créer la colonne avec des cases à cocher
                    Dim checkboxColumn As New DataGridViewCheckBoxColumn()
                    checkboxColumn.HeaderText = "Sélectionner"
                    checkboxColumn.Name = "CheckBoxColumn"
                    checkboxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells ' Définir l'AutoSizeMode sur AllCells
                    ' Ajouter la colonne au début de la DataGridView
                    DataGridView1.Columns.Insert(0, checkboxColumn)

                    ' Parcourir les lignes pour cocher toutes les cases
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        row.Cells("CheckBoxColumn").Value = False
                    Next
                    DataGridView1.ClearSelection()
                End If
            Else
                Dim yesornoMsg As New eror

                yesornoMsg.Label14.Text = "Vous n'avez pas l'autorisation !"
                yesornoMsg.ShowDialog()
            End If
        End If

    End Sub

    Private Sub IconButton20_Click(sender As Object, e As EventArgs) Handles IconButton20.Click

        DataGridView1.Visible = True
        DateTimePicker5.Value = DateTime.Today
        DateTimePicker3.Value = DateTime.Today
        ComboBox8.Text = ""
        ComboBox7.Text = ""
        IconButton26.PerformClick()
        IconButton28.PerformClick()
        IconButton30.PerformClick()
        Panel1.Visible = False
        DataGridView1.ClearSelection()
        If DataGridView1.Columns.Contains("CheckBoxColumn") Then
            ' Supprimer la colonne CheckBox
            DataGridView1.Columns.Remove("CheckBoxColumn")
        End If
    End Sub

    Private Sub IconButton19_Click(sender As Object, e As EventArgs) Handles IconButton19.Click
        Panel3.Visible = True
        TextBox2.Text = IconButton22.Text.Replace(" Dhs", "").Replace(" ", "")
        ComboBox2.Text = "Espèce (Comptoir)"
        DataGridView1.Visible = False
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.Text = "Espèce (Comptoir)" Or ComboBox2.Text = "Espèce (Coffre)" Then
            tpepanel.Visible = False
            chqpanel.Visible = False
        End If

        If ComboBox2.Text = "Chèque" Or ComboBox2.Text = "Virement" Or ComboBox2.Text = "Effet" Then
            tpepanel.Visible = False
            chqpanel.Visible = True
        End If

        If ComboBox2.Text = "TPE" Then
            tpepanel.Visible = True
            chqpanel.Visible = False
        End If
    End Sub

    Private Sub IconButton30_Click(sender As Object, e As EventArgs) Handles IconButton30.Click
        Panel3.Visible = False
        IconButton26.PerformClick()
        IconButton28.PerformClick()
        DataGridView1.Visible = True
    End Sub

    Private Sub IconButton26_Click(sender As Object, e As EventArgs) Handles IconButton26.Click
        tpepanel.Visible = False
        TextBox21.Text = ""
        TextBox20.Text = ""
    End Sub

    Private Sub IconButton28_Click(sender As Object, e As EventArgs) Handles IconButton28.Click
        chqpanel.Visible = False
        TextBox8.Text = ""
        TextBox16.Text = ""
        TextBox15.Text = ""
        TextBox3.Text = ""
    End Sub
    Dim sql3 As String
    Dim cmd3 As MySqlCommand
    Private Sub IconButton29_Click(sender As Object, e As EventArgs) Handles IconButton29.Click
        Dim yesornoMsg As New yesorno
        yesornoMsg.Label14.Text = "Voulez-vous vraiment effectuer ce règlement ?"
        yesornoMsg.IconButton1.Visible = False
        yesornoMsg.ShowDialog()
        If Msgboxresult = True Then

            conn2.Close()
            conn2.Open()

            Dim client As String
            Dim resteencaisse As Decimal = Convert.ToDecimal(TextBox2.Text.Replace(" ", "").Replace(".", ","))

            adpt = New MySqlDataAdapter("SELECT * FROM reglement_fac order by id desc", conn2)
            Dim tablereglement_id As New DataTable
            adpt.Fill(tablereglement_id)
            Dim regl_id As Integer
            If tablereglement_id.Rows.Count = 0 Then
                regl_id = 1
            Else
                regl_id = tablereglement_id.Rows(0).Item(0) + 1
            End If
            Dim selectedRows As New List(Of DataGridViewRow)
            For Each row As DataGridViewRow In DataGridView1.Rows
                ' Vérifier si la case à cocher de la ligne est cochée
                If Convert.ToBoolean(row.Cells("CheckBoxColumn").Value) = True Then
                    selectedRows.Add(row)
                End If
            Next

            selectedRows.Sort(Function(row1, row2)
                                  Dim valeurColonne1 As Integer = Convert.ToDouble(row1.Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")) ' Changer 2 par l'indice de votre colonne
                                  Dim valeurColonne2 As Integer = Convert.ToDouble(row2.Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")) ' Changer 2 par l'indice de votre colonne

                                  ' Tri ascendant (positif d'abord, puis négatif)
                                  If valeurColonne1 > 0 AndAlso valeurColonne2 < 0 Then
                                      Return -1
                                  ElseIf valeurColonne1 < 0 AndAlso valeurColonne2 > 0 Then
                                      Return 1
                                  Else
                                      Return valeurColonne1.CompareTo(valeurColonne2)
                                  End If
                              End Function)

            For i As Integer = selectedRows.Count - 1 To 0 Step -1
                Dim row As DataGridViewRow = selectedRows(i)
                If row.Cells(3).Value < 0 Then
                    sql3 = "UPDATE `achat` SET `paye`=@paye, `reste`=@reste, `avoir` = 0 WHERE `id` = @id"
                Else
                    sql3 = "UPDATE `achat` SET `paye`=@paye, `reste`=@reste WHERE `id` = @id"
                End If
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.Parameters.AddWithValue("@id", row.Cells(1).Value)
                If Convert.ToDecimal(Convert.ToDecimal(row.Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")) + resteencaisse).ToString.Replace(".", ",") >= Convert.ToDecimal(row.Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")) Then
                    cmd3.Parameters.AddWithValue("@paye", Convert.ToDecimal(row.Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")))
                    cmd3.Parameters.AddWithValue("@reste", 0.00)
                    cmd3.ExecuteNonQuery()

                    sql3 = "INSERT INTO `reglement_fac`(`reglement`, `fac`, `montant`, `date`) VALUES (@type,@fac,@montant,@date) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@type", regl_id)
                    cmd3.Parameters.AddWithValue("@fac", row.Cells(1).Value.ToString())
                    cmd3.Parameters.AddWithValue("@montant", Convert.ToDecimal(row.Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")))
                    cmd3.Parameters.AddWithValue("@date", Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss"))
                    cmd3.ExecuteNonQuery()
                Else
                    cmd3.Parameters.AddWithValue("@paye", Convert.ToDecimal(Convert.ToDecimal(row.Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")) + resteencaisse).ToString.Replace(".", ","))
                    cmd3.Parameters.AddWithValue("@reste", Convert.ToDecimal(Convert.ToDecimal(row.Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")) - Convert.ToDecimal(row.Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")) - resteencaisse).ToString.Replace(".", ",").Replace(" ", ""))
                    cmd3.ExecuteNonQuery()

                    sql3 = "INSERT INTO `reglement_fac`(`reglement`, `fac`, `montant`, `date`) VALUES (@type,@fac,@montant,@date) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@type", regl_id)
                    cmd3.Parameters.AddWithValue("@fac", row.Cells(1).Value.ToString())
                    cmd3.Parameters.AddWithValue("@montant", Convert.ToDecimal(resteencaisse.ToString.Replace(".", ",").Replace(" ", "")))
                    cmd3.Parameters.AddWithValue("@date", Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss"))
                    cmd3.ExecuteNonQuery()
                End If

                sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                If role = "Caissier" Then
                    cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
                Else
                    cmd3.Parameters.AddWithValue("@name", Label2.Text)
                End If
                cmd3.Parameters.AddWithValue("@op", "Réglement de la réception N° " & row.Cells(1).Value.ToString())
                cmd3.ExecuteNonQuery()

                client = row.Cells(7).Value.ToString()


                resteencaisse = resteencaisse - Convert.ToDecimal(row.Cells(5).Value.ToString.Replace(".", ",").Replace(" ", ""))
                If resteencaisse <= 0 Then
                    Exit For
                End If

            Next

            sql3 = "INSERT INTO `reglement`( `type`, `montant`, `date`, `mode`, `client`,`achat`, `echeance`) VALUES (@type,@montant,@date,@mode,@clt,@achat,@ech) "
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            cmd3.Parameters.AddWithValue("@type", "achat")
            cmd3.Parameters.AddWithValue("@montant", Convert.ToDouble(TextBox2.Text.Replace(".", ",")).ToString.Replace(".", ",").Replace(" ", ""))
            cmd3.Parameters.AddWithValue("@date", Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss"))
            cmd3.Parameters.AddWithValue("@ech", Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss"))
            cmd3.Parameters.AddWithValue("@mode", ComboBox2.Text)
            cmd3.Parameters.AddWithValue("@clt", client)
            cmd3.Parameters.AddWithValue("@achat", regl_id)
            cmd3.ExecuteNonQuery()

            If ComboBox2.Text = "Espèce (Comptoir)" Then
                Dim sql2 As String
                Dim cmd2 As MySqlCommand
                sql2 = "INSERT INTO tiroir (`cause`, `mtn`, `date`, `caissier`,`remarque`) VALUES ('Réception','" + Convert.ToDouble(TextBox2.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" + Label2.Text + "','" & client & "')"
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.ExecuteNonQuery()
            End If


            If ComboBox2.Text = "TPE" Then
                sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `achat`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('tpe','" + TextBox21.Text + "','tpe','" + TextBox20.Text + "','tpe', '" + Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & regl_id & "','" + ComboBox8.Text + "','" + TextBox2.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & client & "','" & ComboBox2.Text & "')"
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.ExecuteNonQuery()
            End If
            If ComboBox2.Text = "Chèque" Then
                sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `achat`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('" + TextBox8.Text + "','" + TextBox16.Text + "','" + TextBox15.Text + "','" + TextBox3.Text + "','" + Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & regl_id & "','" + ComboBox7.Text + "','" + TextBox2.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & client & "','" & ComboBox2.Text & "')"
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.ExecuteNonQuery()
            End If
            If ComboBox2.Text = "Virement" Then
                sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `achat`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('" + TextBox8.Text + "','" + TextBox16.Text + "','" + TextBox15.Text + "','" + TextBox3.Text + "','" + Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & regl_id & "','" + ComboBox7.Text + "','" + TextBox2.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & client & "','" & ComboBox2.Text & "')"
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.ExecuteNonQuery()
            End If
            If ComboBox2.Text = "Effet" Then
                sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `achat`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('" + TextBox8.Text + "','" + TextBox16.Text + "','" + TextBox15.Text + "','" + TextBox3.Text + "','" + Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & regl_id & "','" + ComboBox7.Text + "','" + TextBox2.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & client & "','" & ComboBox2.Text & "')"
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.ExecuteNonQuery()
            End If

            Dim yesornoMsg2 As New success
            yesornoMsg2.Label14.Text = "Règlement bien effectué !"
            yesornoMsg2.ShowDialog()

            IconButton20.PerformClick()
            conn2.Close()

        End If
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click

    End Sub

    Private Sub IconButton27_Click(sender As Object, e As EventArgs) Handles IconButton27.Click
        rapproche.Show()
        rapproche.Panel7.Visible = True
        rapproche.Label16.Text = "Paiements réglés"
        adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count <> 0 Then
            rapproche.ComboBox3.Items.Clear()
            rapproche.ComboBox3.Items.Add("Tout")
            For i = 0 To table2.Rows.Count - 1
                rapproche.ComboBox3.Items.Add(table2.Rows(i).Item(0))
            Next
            rapproche.ComboBox3.SelectedIndex = 0

        End If
    End Sub


    Private Sub IconButton31_Click(sender As Object, e As EventArgs) Handles IconButton31.Click

        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("achat")
        For i = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value)
        Next
        ReportToPrint = New LocalReport()
        formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\etatrecp.rdlc"
        formata4.ReportViewer1.LocalReport.DataSources.Clear()
        Dim du As New ReportParameter("du", DateTimePicker1.Text)
        Dim du1(0) As ReportParameter
        du1(0) = du
        formata4.ReportViewer1.LocalReport.SetParameters(du1)

        Dim au As New ReportParameter("au", DateTimePicker2.Text)
        Dim au1(0) As ReportParameter
        au1(0) = au
        formata4.ReportViewer1.LocalReport.SetParameters(au1)

        Dim frs As New ReportParameter("frs", ComboBox3.Text)
        Dim frs1(0) As ReportParameter
        frs1(0) = frs
        formata4.ReportViewer1.LocalReport.SetParameters(frs1)


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

    Private Sub IconButton32_Click(sender As Object, e As EventArgs) Handles IconButton32.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Ajouter Dettes'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Panel4.Visible = True
            Else
                Dim yesornoMsg As New eror
                yesornoMsg.Label14.Text = "Vous n'avez pas l'autorisation !"
                yesornoMsg.ShowDialog()
            End If
        End If

    End Sub

    Private Sub IconButton36_Click(sender As Object, e As EventArgs) Handles IconButton36.Click
        If TextBox4.Text = "" Then
            Dim yesornoMsg As New eror
            yesornoMsg.Label14.Text = "Veuillez saisir un nombre valide !"
            yesornoMsg.ShowDialog()
        Else

            adpt = New MySqlDataAdapter("select id from achat order by id desc", conn2)
            Dim table4 As New DataTable
            adpt.Fill(table4)
            Dim ref As String
            If table4.Rows.Count <> 0 Then
                ref = Convert.ToString(Convert.ToDouble(table4.Rows(0).Item(0).ToString.Replace("R", "")) + 1).PadLeft(4, "0")
            Else
                ref = Convert.ToString("0001").PadLeft(4, "0")
            End If
            Dim refe As String = "R" & ref
            conn2.Close()
            conn2.Open()
            Dim sql2 As String
            Dim cmd2 As MySqlCommand

            sql2 = "INSERT INTO achat (id,montant,Fournisseur,dateAchat,paye,reste,mtn_ht,rms,ord,caissier) VALUES ('" & refe & "', '" + TextBox4.Text.Replace(".", ",") + "', '" + ComboBox3.Text + "', '" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', '0', '" & TextBox4.Text.Replace(".", ",") & "', '0', '0','" & TextBox5.Text & "', 'dette' )"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()

            sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            If role = "Caissier" Then
                cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
            Else
                cmd3.Parameters.AddWithValue("@name", Label2.Text)
            End If
            cmd3.Parameters.AddWithValue("@op", "Ajout d'une dette N° de pièce " & TextBox5.Text & ", avec un montant de " & Convert.ToDouble(TextBox4.Text.Replace(" ", "").Replace(".", ",")).ToString("N2"))
            cmd3.ExecuteNonQuery()

            conn2.Close()
            Dim yesornoMsg As New success
            yesornoMsg.Label14.Text = "Dettes bien ajoutée !"
            yesornoMsg.ShowDialog()
            IconButton35.PerformClick()

        End If
    End Sub

    Private Sub IconButton35_Click(sender As Object, e As EventArgs) Handles IconButton35.Click
        Panel4.Visible = False
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        adpt = New MySqlDataAdapter("select * from banques where organisme = '" & ComboBox7.Text & "'", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        TextBox16.Text = table.Rows(0).Item(2)
        TextBox15.Text = table.Rows(0).Item(3)
        TextBox8.Text = ComboBox7.Text

    End Sub

    Private Sub IconButton33_Click(sender As Object, e As EventArgs) Handles IconButton33.Click
        If Panel6.Visible = False Then
            Panel6.Visible = True
            IconButton10.Text = "Nouvel Avoir"
            Label4.Text = "Avoirs"
            IconButton33.Text = "Les Réceptions"

            adpt = New MySqlDataAdapter("select * from avoirs WHERE Day(avoirs.dateAchat) = @day and month(avoirs.dateAchat)=@month and year(avoirs.dateAchat)=@year order by id desc", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
            adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
            adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
            Dim table As New DataTable
            adpt.Fill(table)
            Dim sum As Double = 0
            Dim cnt As Integer = 0
            DataGridView2.Rows.Clear()
            For i = 0 To table.Rows.Count - 1

                Dim s As Double = 0
                Dim t As Double = 0
                Dim r As Double = 0
                s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")


                DataGridView2.Rows.Add(table.Rows(i).Item(0), s.ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2))

                sum = sum + Convert.ToDouble(s)
            Next

            IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00") & " Dhs"
            IconButton2.Text = table.Rows.Count
        Else
            Panel6.Visible = False
            IconButton10.Text = "Nouvel Réception"
            Label4.Text = "Réception"
            IconButton33.Text = "Les Avoirs"

            adpt = New MySqlDataAdapter("select * from achat WHERE Day(achat.dateAchat) = @day and month(achat.dateAchat)=@month and year(achat.dateAchat)=@year order by id desc", conn2)
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

                Dim s As Double = 0
                Dim t As Double = 0
                Dim r As Double = 0
                s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")
                t = Convert.ToString(table.Rows(i).Item(4)).Replace(".", ",")
                r = Convert.ToString(table.Rows(i).Item(5)).Replace(".", ",")

                DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(8), s.ToString("N2"), t.ToString("N2"), r.ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))

                sum = sum + Convert.ToDouble(s)
            Next

            IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00") & " Dhs"
            IconButton2.Text = table.Rows.Count

        End If

    End Sub

    Private Sub IconButton37_Click(sender As Object, e As EventArgs) Handles IconButton37.Click
        If ComboBox3.Text = "Tout" Then
            adpt = New MySqlDataAdapter("SELECT * FROM avoirs WHERE dateAchat BETWEEN @datedebut AND @datefin order by id desc", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))

        Else
            adpt = New MySqlDataAdapter("SELECT * FROM avoirs WHERE dateAchat BETWEEN @datedebut AND @datefin and Fournisseur = @frs order by id desc", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
            adpt.SelectCommand.Parameters.AddWithValue("@frs", ComboBox3.Text)

        End If
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        For i = 0 To table.Rows.Count - 1

            Dim s As Double = 0
            Dim t As Double = 0
            Dim r As Double = 0
            s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")

            DataGridView2.Rows.Add(table.Rows(i).Item(0), s.ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2))

            sum = sum + Convert.ToDouble(s)
        Next

        IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00") & " Dhs"
        IconButton2.Text = table.Rows.Count
    End Sub

    Private Sub IconButton34_Click(sender As Object, e As EventArgs) Handles IconButton34.Click
        adpt = New MySqlDataAdapter("select * from avoirs WHERE Day(avoirs.dateAchat) = @day and month(avoirs.dateAchat)=@month and year(avoirs.dateAchat)=@year order by id desc", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
        adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
        adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        For i = 0 To table.Rows.Count - 1

            Dim s As Double = 0
            Dim t As Double = 0
            Dim r As Double = 0
            s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")

            DataGridView2.Rows.Add(table.Rows(i).Item(0), s.ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2))

            sum = sum + Convert.ToDouble(s)
        Next

        IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00") & " Dhs"
        IconButton2.Text = table.Rows.Count
    End Sub

    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged
        If TextBox14.Text = "" Then
            adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                ComboBox3.Items.Clear()
                ComboBox3.Items.Add("Tout")
                For i = 0 To table.Rows.Count - 1
                    ComboBox3.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox3.SelectedIndex = 0

            End If
        Else

            Dim inputText As String = TextBox14.Text

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
                RemoveHandler TextBox14.TextChanged, AddressOf TextBox14_TextChanged

                ' Mettez à jour le texte dans le TextBox
                TextBox14.Text = modifiedText

                ' Replacez le curseur à la position correcte après la modification
                TextBox14.SelectionStart = TextBox14.Text.Length

                ' Réactivez le gestionnaire d'événements TextChanged
                AddHandler TextBox14.TextChanged, AddressOf TextBox14_TextChanged
            End If

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

    Private Sub ComboBox3_Click(sender As Object, e As EventArgs) Handles ComboBox3.Click

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = False Then

            Dim client As String = "Tout"
            If ComboBox3.Text = "Tout" Or ComboBox3.Text = "" Then
                Panel7.Visible = False
                Label16.Text = 0.00
                DataGridView1.ClearSelection()
                DataGridView1.Rows.Clear()
                IconButton1.Text = 0.00
            Else
                client = ComboBox3.Text
                adpt = New MySqlDataAdapter("SELECT * FROM achat WHERE reste > 0 and Fournisseur = @clt order by id desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox3.Text)

                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                DataGridView1.Rows.Clear()

                For i = 0 To table.Rows.Count - 1
                    DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(8), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(5).ToString.Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))


                    sum = sum + table.Rows(i).Item(5).ToString.Replace(".", ",")


                Next

                IconButton1.Text = 0.00

                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells(3).Value < row.Cells(2).Value Then
                        row.DefaultCellStyle.ForeColor = Color.Red
                    Else
                        row.DefaultCellStyle.ForeColor = Color.Black
                    End If
                Next
                DataGridView1.ClearSelection()
                Panel7.Visible = True

                Label16.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2")
                DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"



                ' Ajouter la colonne au début de la DataGridView

            End If
            TextBox14.Text = ""
            ComboBox3.Text = client

        Else


            Dim client As String = "Tout"
            If ComboBox3.Text = "Tout" Or ComboBox3.Text = "" Then
                Panel7.Visible = False
                Label16.Text = 0.00
                DataGridView1.ClearSelection()
                DataGridView1.Rows.Clear()
                IconButton1.Text = 0.00
            Else
                client = ComboBox3.Text
                adpt = New MySqlDataAdapter("SELECT * FROM achat WHERE Fournisseur = @clt order by id desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox3.Text)

                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                DataGridView1.Rows.Clear()


                For i = 0 To table.Rows.Count - 1
                    DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(8), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",").Replace(" ", "")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ",").Replace(" ", "")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(5).ToString.Replace(".", ",").Replace(" ", "")).ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",").Replace(" ", "")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))

                    sum = sum + table.Rows(i).Item(5).ToString.Replace(".", ",")


                Next

                IconButton1.Text = 0.00

                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells(3).Value < row.Cells(2).Value Then
                        row.DefaultCellStyle.ForeColor = Color.Red
                    Else
                        row.DefaultCellStyle.ForeColor = Color.Black
                    End If
                Next
                DataGridView1.ClearSelection()
                Panel7.Visible = True

                Label16.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2")
                DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"



                ' Ajouter la colonne au début de la DataGridView

            End If
            TextBox14.Text = ""
            ComboBox3.Text = client


        End If
    End Sub

    Private Sub DataGridView2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick
        If e.RowIndex >= 0 Then

            achatedit.Show()
            adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
            Dim tableimg As New DataTable
            adpt.Fill(tableimg)

            For i = 0 To tableimg.Rows.Count() - 1
                achatedit.ComboBox5.Items.Add(tableimg.Rows(i).Item(0))
            Next

            Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
            Dim id As String = row.Cells(0).Value.ToString
            achatedit.Label11.Text = id
            achatedit.Label16.Text = "Avoirs :"
            achatedit.Label31.Text = row.Cells(3).Value.ToString
            achatedit.ComboBox5.Text = row.Cells(3).Value.ToString

            achatedit.Label32.Visible = False

            achatedit.DataGridView2.Rows.Add(0 & " %", 0, 0, 0)
            achatedit.DataGridView2.Rows.Add(7 & " %", 0, 0, 0)
            achatedit.DataGridView2.Rows.Add(10 & " %", 0, 0, 0)
            achatedit.DataGridView2.Rows.Add(14 & " %", 0, 0, 0)
            achatedit.DataGridView2.Rows.Add(20 & " %", 0, 0, 0)

            Dim sumttc As Double = 0
            Dim sumht As Double = 0
            Dim sumrms As Double = 0

            adpt = New MySqlDataAdapter("select * from avoirsdetails where achat_Id = '" & id & "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            For i = 0 To table.Rows.Count - 1

                If table.Rows.Count() <> 0 Then

                    achatedit.DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(6), table.Rows(i).Item(7), table.Rows(i).Item(8), table.Rows(i).Item(9), Nothing, Nothing, Nothing, Nothing, table.Rows(i).Item(10), Nothing, table.Rows(i).Item(12), table.Rows(i).Item(3), table.Rows(i).Item(13), table.Rows(i).Item(14))
                Else
                    Dim yesornoMsg As New eror
                    yesornoMsg.Label14.Text = "Bon d'avoir Vide, Veuillez resaisir le Bon."
                    yesornoMsg.ShowDialog()
                End If
            Next



            Dim ht, ht7, ht10, ht14, ht20 As Double
            Dim tva, tva7, tva10, tva14, tva20 As Double
            Dim sum0 As Double
            Dim sum7 As Double
            Dim sum10 As Double
            Dim sum14 As Double
            Dim sum20 As Double

            For j = 0 To achatedit.DataGridView1.Rows.Count() - 1

                sumttc += achatedit.DataGridView1.Rows(j).Cells(14).Value
                sumht += achatedit.DataGridView1.Rows(j).Cells(3).Value * (achatedit.DataGridView1.Rows(j).Cells(9).Value - achatedit.DataGridView1.Rows(j).Cells(18).Value)
                sumrms += (achatedit.DataGridView1.Rows(j).Cells(3).Value * (achatedit.DataGridView1.Rows(j).Cells(9).Value - achatedit.DataGridView1.Rows(j).Cells(18).Value)) * (achatedit.DataGridView1.Rows(j).Cells(16).Value / 100)

                If achatedit.DataGridView1.Rows(j).Cells(4).Value = 0 Then
                    sum0 += achatedit.DataGridView1.Rows(j).Cells(14).Value
                    ht += achatedit.DataGridView1.Rows(j).Cells(3).Value * (achatedit.DataGridView1.Rows(j).Cells(9).Value - achatedit.DataGridView1.Rows(j).Cells(18).Value)
                    tva = 0

                End If

                If achatedit.DataGridView1.Rows(j).Cells(4).Value = 7 Then
                    sum7 += achatedit.DataGridView1.Rows(j).Cells(14).Value
                    ht7 += achatedit.DataGridView1.Rows(j).Cells(3).Value * (achatedit.DataGridView1.Rows(j).Cells(9).Value - achatedit.DataGridView1.Rows(j).Cells(18).Value)
                    tva7 = (ht7 * 7) / 100
                End If

                If achatedit.DataGridView1.Rows(j).Cells(4).Value = 10 Then
                    sum10 += achatedit.DataGridView1.Rows(j).Cells(14).Value
                    ht10 += achatedit.DataGridView1.Rows(j).Cells(3).Value * (achatedit.DataGridView1.Rows(j).Cells(9).Value - achatedit.DataGridView1.Rows(j).Cells(18).Value)
                    tva10 = (ht10 * 10) / 100
                End If

                If achatedit.DataGridView1.Rows(j).Cells(4).Value = 14 Then
                    sum14 += achatedit.DataGridView1.Rows(j).Cells(14).Value
                    ht14 += achatedit.DataGridView1.Rows(j).Cells(3).Value * (achatedit.DataGridView1.Rows(j).Cells(9).Value - achatedit.DataGridView1.Rows(j).Cells(18).Value)
                    tva14 = (ht14 * 14) / 100
                End If
                If achatedit.DataGridView1.Rows(j).Cells(4).Value = 20 Then
                    sum20 += achatedit.DataGridView1.Rows(j).Cells(14).Value
                    ht20 += achatedit.DataGridView1.Rows(j).Cells(3).Value * (achatedit.DataGridView1.Rows(j).Cells(9).Value - achatedit.DataGridView1.Rows(j).Cells(18).Value)
                    tva20 = (ht20 * 20) / 100
                End If

            Next

            achatedit.DataGridView2.Rows(0).Cells(1).Value = Convert.ToDouble(ht.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(0).Cells(2).Value = Convert.ToDouble(tva.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(0).Cells(3).Value = Convert.ToDouble(sum0.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(1).Cells(1).Value = Convert.ToDouble(ht7.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(1).Cells(2).Value = Convert.ToDouble(tva7.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(1).Cells(3).Value = Convert.ToDouble(sum7.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(2).Cells(1).Value = Convert.ToDouble(ht10.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(2).Cells(2).Value = Convert.ToDouble(tva10.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(2).Cells(3).Value = Convert.ToDouble(sum10.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(3).Cells(1).Value = Convert.ToDouble(ht14.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(3).Cells(2).Value = Convert.ToDouble(tva14.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(3).Cells(3).Value = Convert.ToDouble(sum14.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(4).Cells(1).Value = Convert.ToDouble(ht20.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(4).Cells(2).Value = Convert.ToDouble(tva20.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.DataGridView2.Rows(4).Cells(3).Value = Convert.ToDouble(sum20.ToString.Replace(".", ",")).ToString("# #00.00")

            achatedit.Label15.Text = Convert.ToDouble(sumttc.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.Label27.Text = Convert.ToDouble(sumrms.ToString.Replace(".", ",")).ToString("# #00.00")
            achatedit.Label10.Text = Convert.ToDouble(sumttc - (sumht - sumrms)).ToString("# #00.00")



            adpt = New MySqlDataAdapter("select Fournisseur,timbre from achat where id = '" + achatedit.Label11.Text.ToString + "' ", conn2)
            Dim table3 As New DataTable
            adpt.Fill(table3)
            achatedit.Label53.Text = table3.Rows(0).Item(1)
            achatedit.Label51.Text = achatedit.Label15.Text - table3.Rows(0).Item(1)
            If IsDBNull(table3.Rows(0).Item(0)) Then
                adpt2 = New MySqlDataAdapter("select name,adresse,ville,ice from fours where name = '" + table3.Rows(0).Item(0).ToString + "' ", conn2)
                Dim table2 As New DataTable
                adpt2.Fill(table2)

                achatedit.Label7.Text = table2.Rows(0).Item(0)
                achatedit.Label8.Text = table2.Rows(0).Item(1)
                achatedit.Label12.Text = table2.Rows(0).Item(2)

                Dim output As StringBuilder = New StringBuilder
                For i = 0 To table2.Rows(0).Item(3).Length - 1
                    If IsNumeric(table2.Rows(0).Item(3)(i)) Then
                        output.Append(table2.Rows(0).Item(3)(i))
                    End If
                Next
                achatedit.Label2.Text = output.ToString()
            End If


            achatedit.TextBox1.Text = "Avoir N° " & id
            achatedit.Label6.Text = row.Cells(2).Value.ToString

        End If

    End Sub

    Private Sub IconButton38_Click(sender As Object, e As EventArgs) Handles IconButton38.Click
        If IconButton38.BackColor = Color.Goldenrod Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                row.Selected = False
            Next

            IconButton1.Text = 0.00
            IconButton38.BackColor = Color.Gold
        Else
            For Each row As DataGridViewRow In DataGridView1.Rows
                row.Selected = True
            Next
            Dim sum As Decimal = 0
            For i = 0 To DataGridView1.Rows.Count - 1
                sum += Convert.ToDecimal(DataGridView1.Rows(i).Cells(2).Value)
            Next
            IconButton1.Text = sum.ToString("# ##0.00")
            IconButton38.BackColor = Color.Goldenrod
        End If
    End Sub

    Private Sub IconButton39_Click(sender As Object, e As EventArgs) Handles IconButton39.Click
        Me.Close()
    End Sub

    Private Sub TextBox14_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox14.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox14.Text = "" Then
                ComboBox3.Text = "Tout"
                Panel7.Visible = False
                Label16.Text = 0.00
                DataGridView1.ClearSelection()
                DataGridView1.Rows.Clear()
                IconButton1.Text = 0.00
                IconButton4.PerformClick()
            Else

                Dim client As String = "Tout"
                If ComboBox3.Text = "Tout" Or ComboBox3.Text = "" Then
                    Panel7.Visible = False
                    Label16.Text = 0.00
                    DataGridView1.ClearSelection()
                    DataGridView1.Rows.Clear()
                    IconButton1.Text = 0.00
                Else
                    client = ComboBox3.Text
                    adpt = New MySqlDataAdapter("SELECT * FROM achat WHERE (reste <> 0 OR avoir = 1) and Fournisseur = @clt order by id desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@mode", "Non Payé")
                    adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox3.Text)

                    Dim table As New DataTable
                    adpt.Fill(table)
                    Dim sum As Double = 0
                    Dim cnt As Integer = 0
                    DataGridView1.Rows.Clear()

                    For i = 0 To table.Rows.Count - 1
                        If Convert.ToDecimal(table.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")) > 0 Then
                            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(8), Convert.ToDecimal(table.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDecimal(table.Rows(i).Item(4).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDecimal(table.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))
                            sum = sum + table.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")

                        End If


                    Next

                    For i = 0 To table.Rows.Count - 1
                        If Convert.ToDecimal(table.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")) < 0 Then

                            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(8), Convert.ToDecimal(table.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDecimal(table.Rows(i).Item(4).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDecimal(table.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",").Replace(" ", "")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))
                            sum = sum + table.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")

                        End If


                    Next

                    IconButton1.Text = 0.00

                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If Convert.ToDecimal(row.Cells(4).Value.ToString.Replace(" ", "")) > 0 Then
                            row.DefaultCellStyle.ForeColor = Color.Red
                        Else
                            row.DefaultCellStyle.ForeColor = Color.Black
                        End If
                    Next
                    DataGridView1.ClearSelection()
                    Panel7.Visible = True

                    Label16.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2")
                    DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
                    DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
                    DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
                    DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"



                    ' Ajouter la colonne au début de la DataGridView

                End If
                TextBox14.Text = ""
                ComboBox3.Text = client

            End If
        End If
    End Sub

    Private Sub ComboBox3_DropDownClosed(sender As Object, e As EventArgs) Handles ComboBox3.DropDownClosed
        Dim client As String = "Tout"
        If ComboBox3.Text = "Tout" Or ComboBox3.Text = "" Then
            Panel7.Visible = False
            Label16.Text = 0.00
            DataGridView1.ClearSelection()
            DataGridView1.Rows.Clear()
            IconButton1.Text = 0.00
        Else
            client = ComboBox3.Text
            adpt = New MySqlDataAdapter("SELECT * FROM achat WHERE (reste <> 0 OR avoir = 1) and Fournisseur = @clt order by id desc", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@mode", "Non Payé")
            adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox3.Text)

            Dim table As New DataTable
            adpt.Fill(table)
            Dim sum As Double = 0
            Dim cnt As Integer = 0
            DataGridView1.Rows.Clear()

            For i = 0 To table.Rows.Count - 1
                If Convert.ToDecimal(table.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")) > 0 Then
                    DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(8), Convert.ToDecimal(table.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDecimal(table.Rows(i).Item(4).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDecimal(table.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",").Replace(" ", "")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))
                    sum = sum + table.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")

                End If


            Next

            For i = 0 To table.Rows.Count - 1
                If Convert.ToDecimal(table.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")) < 0 Then
                    DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(8), Convert.ToDecimal(table.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDecimal(table.Rows(i).Item(4).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDecimal(table.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",").Replace(" ", "")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))
                    sum = sum + table.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")

                End If


            Next

            IconButton1.Text = 0.00

            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells(3).Value < row.Cells(2).Value Then
                    row.DefaultCellStyle.ForeColor = Color.Red
                Else
                    row.DefaultCellStyle.ForeColor = Color.Black
                End If
            Next
            DataGridView1.ClearSelection()
            Panel7.Visible = True

            Label16.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2")
            DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"



            ' Ajouter la colonne au début de la DataGridView

        End If
        TextBox14.Text = ""
        ComboBox3.Text = client
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening

    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerToolStripMenuItem.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Supprimer Reception'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                If Convert.ToDouble(DataGridView1.SelectedRows(0).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ",")) = Convert.ToDouble(DataGridView1.SelectedRows(0).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")) AndAlso Convert.ToDouble(DataGridView1.SelectedRows(0).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ",")) <> 0 Then
                    Dim yesornoMsg As New eror
                    yesornoMsg.Label14.Text = "Vous ne pouvez pas supprimer une réception déjà réglé !"
                    yesornoMsg.ShowDialog()
                Else

                    If Convert.ToDouble(DataGridView1.SelectedRows(0).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")) < 0 Then
                        Dim yesornoMsg As New yesorno
                        yesornoMsg.Label14.Text = "Voulez-vous vraiment supprimer cet Avoir ?"
                        yesornoMsg.IconButton1.Visible = False
                        yesornoMsg.ShowDialog()
                        If Msgboxresult = True Then
                            conn2.Close()
                            conn2.Open()
                            adpt = New MySqlDataAdapter("select `CodeArticle`, `qte` from achatdetails where achat_Id = '" & DataGridView1.SelectedRows(0).Cells(0).Value & "' ", conn2)
                            Dim table As New DataTable
                            adpt.Fill(table)
                            Dim cmd As MySqlCommand

                            For i = 0 To table.Rows.Count() - 1
                                cmd = New MySqlCommand("UPDATE `article` SET `Stock` =  REPLACE(`Stock`,',','.') + '" & table.Rows(i).Item(1).ToString.Replace(",", ".") & "' WHERE `Code` = '" + table.Rows(i).Item(0).ToString.Replace(".", ",") + "' ", conn2)
                                cmd.ExecuteNonQuery()
                            Next

                            cmd = New MySqlCommand("DELETE FROM `achatdetails` WHERE `achat_Id` = '" & DataGridView1.SelectedRows(0).Cells(0).Value & "' ", conn2)
                            cmd.ExecuteNonQuery()

                            cmd = New MySqlCommand("DELETE FROM `achat` WHERE `id` = '" & DataGridView1.SelectedRows(0).Cells(0).Value & "' ", conn2)
                            cmd.ExecuteNonQuery()

                            sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                            cmd3 = New MySqlCommand(sql3, conn2)
                            cmd3.Parameters.Clear()
                            If role = "Caissier" Then
                                cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
                            Else
                                cmd3.Parameters.AddWithValue("@name", Label2.Text)
                            End If
                            cmd3.Parameters.AddWithValue("@op", "Suppression d'Avoir N° " & DataGridView1.SelectedRows(0).Cells(0).Value)
                            cmd3.ExecuteNonQuery()

                            conn2.Close()

                            DataGridView1.Rows.RemoveAt(DataGridView1.SelectedRows(0).Index)
                        End If
                    Else
                        Dim yesornoMsg As New yesorno
                        yesornoMsg.Label14.Text = "Voulez-vous vraiment supprimer cette réception ?"
                        yesornoMsg.IconButton1.Visible = False
                        yesornoMsg.ShowDialog()
                        If Msgboxresult = True Then
                            conn2.Close()
                            conn2.Open()
                            adpt = New MySqlDataAdapter("select `CodeArticle`, `qte`,`gr` from achatdetails where achat_Id = '" & DataGridView1.SelectedRows(0).Cells(0).Value & "' ", conn2)
                            Dim table As New DataTable
                            adpt.Fill(table)
                            Dim cmd As MySqlCommand

                            For i = 0 To table.Rows.Count() - 1
                                cmd = New MySqlCommand("UPDATE `article` SET `Stock` =  REPLACE(`Stock`,',','.') - '" & table.Rows(i).Item(1).ToString.Replace(",", ".") & "' - '" & table.Rows(i).Item(2).ToString.Replace(",", ".") & "' WHERE `Code` = '" + table.Rows(i).Item(0).ToString.Replace(".", ",") + "' ", conn2)
                                cmd.ExecuteNonQuery()
                            Next

                            cmd = New MySqlCommand("DELETE FROM `achatdetails` WHERE `achat_Id` = '" & DataGridView1.SelectedRows(0).Cells(0).Value & "' ", conn2)
                            cmd.ExecuteNonQuery()

                            cmd = New MySqlCommand("DELETE FROM `achat` WHERE `id` = '" & DataGridView1.SelectedRows(0).Cells(0).Value & "' ", conn2)
                            cmd.ExecuteNonQuery()

                            sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                            cmd3 = New MySqlCommand(sql3, conn2)
                            cmd3.Parameters.Clear()
                            If role = "Caissier" Then
                                cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
                            Else
                                cmd3.Parameters.AddWithValue("@name", Label2.Text)
                            End If
                            cmd3.Parameters.AddWithValue("@op", "Suppression de la Réception N° " & DataGridView1.SelectedRows(0).Cells(0).Value)
                            cmd3.ExecuteNonQuery()


                            conn2.Close()

                            DataGridView1.Rows.RemoveAt(DataGridView1.SelectedRows(0).Index)
                        End If
                    End If

                End If
            Else
                Dim yesornoMsg As New eror
                yesornoMsg.Label14.Text = "Vous n'avez pas l'autorisation !"
                yesornoMsg.ShowDialog()

            End If
        End If
    End Sub

    Private Sub DataGridView1_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDown
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then
            ' Sélectionnez la ligne cliquée
            DataGridView1.ClearSelection()
            DataGridView1.Rows(e.RowIndex).Selected = True

            ' Affichez le menu contextuel à l'emplacement du clic
            ContextMenuStrip1.Show(DataGridView1, e.Location)
        End If
    End Sub

    Private Sub IconButton40_Click(sender As Object, e As EventArgs) Handles IconButton40.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Gestion des bons de commandes'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then
                Panel9.Visible = True
                IconButton40.Visible = False
                IconButton10.Visible = False
                Label4.Visible = False
                Panel4.Visible = True
                DataGridView1.Visible = False
                IconPictureBox1.Visible = False

                IconButton43.PerformClick()
            Else
                Dim yesornoMsg As New eror
                yesornoMsg.Panel5.Visible = True
                yesornoMsg.Label14.Text = "Vous n'avez pas l'autorisation !"
                yesornoMsg.ShowDialog()

            End If
        End If


    End Sub

    Private Sub IconButton41_Click(sender As Object, e As EventArgs) Handles IconButton41.Click
        Panel9.Visible = False
        IconButton40.Visible = True
        IconButton10.Visible = True
        Label4.Visible = True
        IconPictureBox1.Visible = True
        Panel4.Visible = False
        DataGridView1.Visible = True
    End Sub

    Private Sub IconButton42_Click(sender As Object, e As EventArgs) Handles IconButton42.Click
        ajoutachat.Show()
        ajoutachat.IconButton21.Visible = True
        ajoutachat.IconButton22.Visible = False
        ajoutachat.Panel10.Visible = True
        ajoutachat.Panel11.Visible = True
        adpt = New MySqlDataAdapter("select id from bcmnds order by id desc", conn2)
        Dim table4 As New DataTable
        adpt.Fill(table4)
        Dim ref As String
        If table4.Rows.Count <> 0 Then
            ref = Convert.ToDouble(table4.Rows(0).Item(0).ToString.Replace("R", "")) + 1
        Else
            ref = 1
        End If
        ajoutachat.TextBox25.Text = ref
        ajoutachat.Label41.Text = "N° du BC :"
        ajoutachat.Label35.Visible = False
        ajoutachat.TextBox17.Visible = False

        ajoutachat.DataGridView3.Columns(4).Visible = False
        ajoutachat.DataGridView3.Columns(5).Visible = False
        ajoutachat.DataGridView3.Columns(6).Visible = False
        ajoutachat.DataGridView3.Columns(7).Visible = False
        ajoutachat.DataGridView3.Columns(8).Visible = False
        ajoutachat.DataGridView3.Columns(9).Visible = False
        ajoutachat.DataGridView3.Columns(10).Visible = False
        ajoutachat.DataGridView3.Columns(11).Visible = False
        ajoutachat.DataGridView3.Columns(12).Visible = False
        ajoutachat.DataGridView3.Columns(13).Visible = False
        ajoutachat.DataGridView3.Columns(14).Visible = False
        ajoutachat.DataGridView3.Columns(15).Visible = False
        ajoutachat.DataGridView3.Columns(16).Visible = False
        ajoutachat.DataGridView3.Columns(17).Visible = False
        ajoutachat.DataGridView3.Columns(18).Visible = False
        ajoutachat.DataGridView3.Columns(19).Visible = False
        ajoutachat.DataGridView3.Columns(20).Visible = False
        ajoutachat.DataGridView3.Columns(21).Visible = False
        ajoutachat.DataGridView3.Columns(22).Visible = False
        ajoutachat.DataGridView3.Columns(23).Visible = False
        ajoutachat.DataGridView3.Columns(24).Visible = False



    End Sub

    Private Sub IconButton43_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub IconButton43_Click_1(sender As Object, e As EventArgs) Handles IconButton43.Click
        Label21.Text = "..."
        IconButton44.Visible = False
        IconButton46.Visible = False
        IconButton45.Visible = False
        DataGridView4.Rows.Clear()
        Try
            If ComboBox4.Text = "Tout" Then
                adpt = New MySqlDataAdapter("SELECT * FROM bcmnds WHERE date BETWEEN @datedebut AND @datefin order by id desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker7.Value.Date)
                adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker6.Value.Date)
            Else
                adpt = New MySqlDataAdapter("SELECT * FROM bcmnds WHERE date BETWEEN @datedebut AND @datefin and name = @frs order by id desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker7.Value.Date)
                adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker6.Value.Date)
                adpt.SelectCommand.Parameters.AddWithValue("@frs", ComboBox4.Text)
            End If
            Dim table As New DataTable
            adpt.Fill(table)
            DataGridView3.Rows.Clear()

            For i = 0 To table.Rows.Count - 1

                DataGridView3.Rows.Add("BC-" & table.Rows(i).Item(0), table.Rows(i).Item(1), Format(table.Rows(i).Item(2), "dd/MM/yyyy"), table.Rows(i).Item(0))

            Next

        Catch ex As MySqlException
            Dim yesornoMsg As New eror
            yesornoMsg.Panel5.Visible = True
            yesornoMsg.Label14.Text = ex.Message
            yesornoMsg.ShowDialog()
        End Try
        TextBox6.Text = ""

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        If TextBox6.Text = "" Then
            adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                ComboBox4.Items.Clear()
                ComboBox4.Items.Add("Tout")
                For i = 0 To table.Rows.Count - 1
                    ComboBox4.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox4.SelectedIndex = 0

            End If
        Else

            Dim inputText As String = TextBox6.Text

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
                RemoveHandler TextBox6.TextChanged, AddressOf TextBox6_TextChanged

                ' Mettez à jour le texte dans le TextBox
                TextBox6.Text = modifiedText

                ' Replacez le curseur à la position correcte après la modification
                TextBox6.SelectionStart = TextBox6.Text.Length

                ' Réactivez le gestionnaire d'événements TextChanged
                AddHandler TextBox6.TextChanged, AddressOf TextBox6_TextChanged
            End If

            ComboBox4.Items.Clear()
            adpt = New MySqlDataAdapter("select name from fours WHERE name LIKE '%" + TextBox6.Text.Replace("'", " ") + "%' order by name asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                For i = 0 To table.Rows.Count - 1
                    ComboBox4.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox4.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub Panel9_Paint(sender As Object, e As PaintEventArgs) Handles Panel9.Paint

    End Sub

    Dim sroted As Integer = 0

    Private Sub IconButton45_Click(sender As Object, e As EventArgs) Handles IconButton45.Click
        Dim yesornoMsg As New yesorno
        yesornoMsg.Label14.Text = "Voulez-vous vraiment supprimer ce bon de commande ?"
        yesornoMsg.IconButton1.Visible = False
        yesornoMsg.ShowDialog()
        If Msgboxresult = True Then
            Dim cmd2 As MySqlCommand
            conn2.Close()
            conn2.Open()
            cmd2 = New MySqlCommand("DELETE FROM `bcmnds` where `id` = '" & Label21.Text & "' ", conn2)
            cmd2.ExecuteNonQuery()

            cmd2 = New MySqlCommand("DELETE FROM `bcmnds_details` where `bcmnd_id` = '" & Label21.Text & "' ", conn2)
            cmd2.ExecuteNonQuery()

            sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            If role = "Caissier" Then
                cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
            Else
                cmd3.Parameters.AddWithValue("@name", Label2.Text)
            End If
            cmd3.Parameters.AddWithValue("@op", "Suppression du Bon de commande N° " & Label21.Text)
            cmd3.ExecuteNonQuery()

            conn2.Close()
            DataGridView4.Rows.Clear()
            Label21.Text = "..."
            IconButton44.Visible = False
            IconButton46.Visible = False
            IconButton45.Visible = False
            IconButton43.PerformClick()
        End If
    End Sub

    Private Sub IconButton44_Click(sender As Object, e As EventArgs) Handles IconButton44.Click
        ajoutachat.Show()
        ajoutachat.IconButton21.Visible = True
        ajoutachat.IconButton22.Visible = False

        ajoutachat.Panel10.Visible = True
        ajoutachat.Panel11.Visible = True
        ajoutachat.IconButton23.Visible = True
        ajoutachat.IconButton23.BringToFront()

        ajoutachat.TextBox25.Text = Label21.Text
        ajoutachat.Label41.Text = "N° du BC :"
        ajoutachat.Label35.Visible = False
        ajoutachat.TextBox17.Visible = False

        ajoutachat.ComboBox4.Text = Label22.Text
        ajoutachat.DateTimePicker4.Value = Convert.ToDateTime(Label23.Text)

        For i = 0 To DataGridView4.Rows.Count - 1
            ajoutachat.DataGridView3.Rows.Add(0, DataGridView4.Rows(i).Cells(0).Value, DataGridView4.Rows(i).Cells(1).Value, DataGridView4.Rows(i).Cells(2).Value, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
        Next

        ajoutachat.DataGridView3.Columns(4).Visible = False
        ajoutachat.DataGridView3.Columns(5).Visible = False
        ajoutachat.DataGridView3.Columns(6).Visible = False
        ajoutachat.DataGridView3.Columns(7).Visible = False
        ajoutachat.DataGridView3.Columns(8).Visible = False
        ajoutachat.DataGridView3.Columns(9).Visible = False
        ajoutachat.DataGridView3.Columns(10).Visible = False
        ajoutachat.DataGridView3.Columns(11).Visible = False
        ajoutachat.DataGridView3.Columns(12).Visible = False
        ajoutachat.DataGridView3.Columns(13).Visible = False
        ajoutachat.DataGridView3.Columns(14).Visible = False
        ajoutachat.DataGridView3.Columns(15).Visible = False
        ajoutachat.DataGridView3.Columns(16).Visible = False
        ajoutachat.DataGridView3.Columns(17).Visible = False
        ajoutachat.DataGridView3.Columns(18).Visible = False
        ajoutachat.DataGridView3.Columns(19).Visible = False
        ajoutachat.DataGridView3.Columns(20).Visible = False
        ajoutachat.DataGridView3.Columns(21).Visible = False
        ajoutachat.DataGridView3.Columns(22).Visible = False
        ajoutachat.DataGridView3.Columns(23).Visible = False
        ajoutachat.DataGridView3.Columns(24).Visible = False


    End Sub

    Private Sub IconButton46_Click(sender As Object, e As EventArgs) Handles IconButton46.Click
        ajoutachat.Show()
        ajoutachat.TextBox25.Enabled = True
        ajoutachat.ComboBox4.Text = Label22.Text
        ajoutachat.DateTimePicker4.Text = Label23.Text
        Dim ord As Integer = 1

        For i = 0 To DataGridView4.Rows.Count - 1
            adpt = New MySqlDataAdapter("select `Code`, `Article`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC` from article where Code = '" & DataGridView4.Rows(i).Cells(0).Value & "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)

            adpt = New MySqlDataAdapter("select `ref` from ref_frs where product_id = '" & DataGridView4.Rows(i).Cells(0).Value & "' and fournisseur = '" & Label22.Text & "'", conn2)
            Dim tableref As New DataTable
            adpt.Fill(tableref)

            Dim qty As Double = Convert.ToDecimal(DataGridView4.Rows(i).Cells(2).Value.ToString.Replace(".", ",").Replace(" ", ""))
            Dim patotal As Double = Convert.ToDouble(DataGridView4.Rows(i).Cells(2).Value.ToString.Replace(".", ",").Replace(" ", "")) * table.Rows(0).Item(4).ToString.Replace(".", ",").ToString.Replace(".", ",")
            Dim paHTtotal As Double = Convert.ToDouble(Convert.ToDecimal(DataGridView4.Rows(i).Cells(2).Value.ToString.Replace(".", ",").Replace(" ", "")) * table.Rows(0).Item(2).ToString.Replace(".", ",")).ToString.Replace(".", ",")
            Dim ref As String
            If tableref.Rows.Count <> 0 Then
                ref = tableref.Rows(0).Item(0)
            Else
                ref = "-"
            End If

            ajoutachat.DataGridView3.Rows.Add(Convert.ToDouble(ord.ToString).ToString("N0"), table.Rows(0).Item(0), table.Rows(0).Item(1).ToString.Replace("'", ""), Convert.ToDouble(qty).ToString("N2"), Convert.ToDouble(table.Rows(0).Item(2).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(0).Item(3).ToString.Replace(".", ",")).ToString("N0"), Convert.ToDouble(table.Rows(0).Item(4).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(paHTtotal).ToString("N2"), Convert.ToDouble(patotal).ToString("N2"), Convert.ToDouble(table.Rows(0).Item(5).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(0).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(0).Item(7).ToString.Replace(".", ",")).ToString("N2"), Nothing, Nothing, Nothing, Nothing, Nothing, "0,00", Convert.ToDouble(table.Rows(0).Item(2).ToString.Replace(".", ",")), "0,00", "0,00", ref, ord, Nothing, 0)
                Dim sum As Double = 0
                Dim ht As Double = 0
            Dim tva As Double = 0
            Dim remise As Double = 0
            For j = 0 To ajoutachat.DataGridView3.Rows.Count - 1
                sum = sum + ajoutachat.DataGridView3.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                remise = remise + (ajoutachat.DataGridView3.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * ajoutachat.DataGridView3.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") * (ajoutachat.DataGridView3.Rows(j).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                ht = ht + (ajoutachat.DataGridView3.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * ajoutachat.DataGridView3.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")) * (1 - (ajoutachat.DataGridView3.Rows(j).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                tva = tva + (ajoutachat.DataGridView3.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",") * (ajoutachat.DataGridView3.Rows(j).Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
            Next

            ajoutachat.Label25.Text = sum.ToString("# ##0.00")
            ajoutachat.Label23.Text = Convert.ToDouble(remise + ajoutachat.TextBox26.Text.Replace(".", ",")).ToString("# ##0.00")
            ajoutachat.Label47.Text = remise.ToString("# ##0.00")
            ajoutachat.Label18.Text = ht.ToString("# ##0.00")
            ajoutachat.Label43.Text = tva.ToString("# ##0.00")
            ajoutachat.Label51.Text = Convert.ToDouble(ajoutachat.Label25.Text) + Convert.ToDouble(ajoutachat.Label53.Text)
            ajoutachat.TextBox11.Text = 0
            ord = ord + 1
        Next

    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        If sroted = 0 Then
            If e.ColumnIndex = 2 Then
                DataGridView1.Sort(DataGridView1.Columns(7), System.ComponentModel.ListSortDirection.Descending)
                sroted = 1
            End If
            If e.ColumnIndex = 6 Then
                DataGridView1.Sort(DataGridView1.Columns(8), System.ComponentModel.ListSortDirection.Descending)
                sroted = 1
            End If
        Else
            If e.ColumnIndex = 2 Then
                DataGridView1.Sort(DataGridView1.Columns(7), System.ComponentModel.ListSortDirection.Ascending)
                sroted = 0
            End If
            If e.ColumnIndex = 6 Then
                DataGridView1.Sort(DataGridView1.Columns(8), System.ComponentModel.ListSortDirection.Ascending)
                sroted = 0
            End If
        End If

    End Sub

    Private Sub DataGridView3_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellDoubleClick
        If e.RowIndex > -1 Then
            Label21.Text = DataGridView3.Rows(e.RowIndex).Cells(3).Value
            Label22.Text = DataGridView3.Rows(e.RowIndex).Cells(1).Value
            Label23.Text = DataGridView3.Rows(e.RowIndex).Cells(2).Value
            IconButton44.Visible = True
            IconButton46.Visible = True
            IconButton45.Visible = True
            adpt = New MySqlDataAdapter("select * from bcmnds_details where bcmnd_id = '" & Label21.Text & "'", conn2)
            Dim tableimg As New DataTable
            adpt.Fill(tableimg)
            DataGridView4.Rows.Clear()
            For i = 0 To tableimg.Rows.Count - 1
                DataGridView4.Rows.Add(tableimg.Rows(i).Item(3), tableimg.Rows(i).Item(2), tableimg.Rows(i).Item(4))
            Next
        End If
    End Sub
End Class