Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Public Class factures
    Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter

    <Obsolete>
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
    Private Sub factures_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler DataGridView1.RowPrePaint, AddressOf DataGridView1_RowPrePaint
        DataGridView1.MultiSelect = True
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


        Label2.Text = user
        Dim w = Screen.PrimaryScreen.WorkingArea.Width
        Dim h = My.Computer.Screen.WorkingArea.Size.Height
        Me.Width = w
        Me.Height = h
        Me.Location = New Point(0, 0)
        adpt = New MySqlDataAdapter("select * from facture WHERE REPLACE(reste,',','.') > 0 order by OrderID desc", conn2)
        adpt.SelectCommand.Parameters.Clear()
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        Dim mode As String = ""
        For i = 0 To table.Rows.Count - 1
            If table.Rows(i).Item(3) = "Non Payé" Then
                mode = "Crédit"
            Else
                adpt = New MySqlDataAdapter("select reglement from reglement_fac where fac = '" & table.Rows(i).Item(0) & "'", conn2)
                Dim table3 As New DataTable
                adpt.Fill(table3)
                If table3.Rows.Count <> 0 Then

                    For j = 0 To table3.Rows.Count - 1
                        adpt = New MySqlDataAdapter("select `montant`, `date`, `mode` FROM `reglement` where fac = '" & table3.Rows(j).Item(0) & "'", conn2)
                        Dim table4 As New DataTable
                        adpt.Fill(table4)
                        If table4.Rows.Count <> 0 Then
                            mode = table4.Rows(0).Item(2)
                        Else
                            mode = "Espèce"
                        End If
                    Next
                Else
                    adpt = New MySqlDataAdapter("select `modePayement` FROM `orders` where fac_id = '" & table.Rows(i).Item(0) & "' and modePayement <> 'Crédit'", conn2)
                    Dim table4 As New DataTable
                    adpt.Fill(table4)
                    If table4.Rows.Count <> 0 Then
                        mode = table4.Rows(0).Item(0)
                    End If
                End If
            End If
            DataGridView1.Rows.Add("F-" & table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(5), mode, table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")), table.Rows(i).Item(14) & " (" & table.Rows(i).Item(15) & ")")

            If table.Rows(i).Item(3) = "Non Payé" Then
                sum = sum + table.Rows(i).Item(8).ToString.Replace(".", ",")

            Else
                sum = sum + table.Rows(i).Item(7).ToString.Replace(".", ",")
            End If
        Next

        IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00") & " Dhs"
        IconButton2.Text = table.Rows.Count
        DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(8).Value <= 0 Then
                row.Cells(8).Style.ForeColor = Color.Red
                row.Cells(7).Style.ForeColor = Color.Red
            Else
                row.Cells(8).Style.ForeColor = Color.Green
                row.Cells(7).Style.ForeColor = Color.Red
            End If
        Next
        DataGridView1.ClearSelection()


        adpt = New MySqlDataAdapter("select client from clients order by client asc", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count <> 0 Then
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Tout")
            For i = 0 To table2.Rows.Count - 1
                ComboBox1.Items.Add(table2.Rows(i).Item(0))
            Next
            ComboBox1.SelectedIndex = 0

        End If
        ComboBox2.SelectedIndex = 0

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

    End Sub
    Dim sroted As Integer = 0
    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        If sroted = 0 Then
            If e.ColumnIndex = 1 Then
                DataGridView1.Sort(DataGridView1.Columns(9), System.ComponentModel.ListSortDirection.Descending)
                sroted = 1
            End If

        Else
            If e.ColumnIndex = 1 Then
                DataGridView1.Sort(DataGridView1.Columns(9), System.ComponentModel.ListSortDirection.Ascending)
                sroted = 0
            End If

        End If

    End Sub
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "" Then
                ComboBox1.Text = "Tout"
                Panel6.Visible = False
                Label16.Text = 0.00
                DataGridView1.ClearSelection()
                DataGridView1.Rows.Clear()
                IconButton1.Text = 0.00
                IconButton4.PerformClick()
            Else

                Dim client As String = "Tout"
                If ComboBox1.Text = "Tout" Or ComboBox1.Text = "" Then
                    Panel6.Visible = False
                    Label16.Text = 0.00
                    DataGridView1.ClearSelection()
                    DataGridView1.Rows.Clear()
                    IconButton1.Text = 0.00
                Else
                    client = ComboBox1.Text
                    adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE etat = @mode and client = @clt order by OrderID desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@mode", "Non Payé")
                    adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox1.Text)

                    Dim table As New DataTable
                    adpt.Fill(table)
                    Dim sum As Double = 0
                    Dim cnt As Integer = 0
                    DataGridView1.Rows.Clear()

                    Dim mode As String = ""
                    For i = 0 To table.Rows.Count - 1
                        If table.Rows(i).Item(3) = "Non Payé" Then
                            mode = "Crédit"
                        Else
                            adpt = New MySqlDataAdapter("select reglement from reglement_fac where fac = '" & table.Rows(i).Item(0) & "'", conn2)
                            Dim table2 As New DataTable
                            adpt.Fill(table2)
                            If table2.Rows.Count <> 0 Then

                                For j = 0 To table2.Rows.Count - 1
                                    adpt = New MySqlDataAdapter("select `montant`, `date`, `mode` FROM `reglement` where fac = '" & table2.Rows(j).Item(0) & "'", conn2)
                                    Dim table4 As New DataTable
                                    adpt.Fill(table4)
                                    If table4.Rows.Count <> 0 Then
                                        mode = table4.Rows(0).Item(2)
                                    Else
                                        mode = "Espèce"
                                    End If
                                Next
                            Else
                                adpt = New MySqlDataAdapter("select `modePayement` FROM `orders` where fac_id = '" & table.Rows(i).Item(0) & "' and modePayement <> 'Crédit'", conn2)
                                Dim table4 As New DataTable
                                adpt.Fill(table4)
                                If table4.Rows.Count <> 0 Then
                                    mode = table4.Rows(0).Item(0)
                                End If
                            End If
                        End If
                        DataGridView1.Rows.Add("F-" & table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(5), mode, table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")), table.Rows(i).Item(14) & " (" & table.Rows(i).Item(15) & ")")

                        sum = sum + table.Rows(i).Item(8).ToString.Replace(".", ",")
                    Next

                    IconButton1.Text = 0.00

                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If row.Cells(8).Value <= 0 Then
                            row.Cells(8).Style.ForeColor = Color.Red
                            row.Cells(7).Style.ForeColor = Color.Red
                        Else
                            row.Cells(8).Style.ForeColor = Color.Green
                            row.Cells(7).Style.ForeColor = Color.Red
                        End If
                    Next
                    DataGridView1.ClearSelection()
                    Panel6.Visible = True

                    Label16.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", ""))
                    DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
                    DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
                    DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
                    DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"



                    ' Ajouter la colonne au début de la DataGridView

                End If
                TextBox1.Text = ""
                ComboBox1.Text = client

            End If
        End If
    End Sub
    Private Sub DataGridView1_CellMouseClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick


        If e.RowIndex >= 0 Then
            If DataGridView1.Columns.Count = 11 Then



                editfac.Show()

                editfac.DataGridView2.Rows.Add(0 & " %", 0, 0, 0)
                editfac.DataGridView2.Rows.Add(7 & " %", 0, 0, 0)
                editfac.DataGridView2.Rows.Add(10 & " %", 0, 0, 0)
                editfac.DataGridView2.Rows.Add(14 & " %", 0, 0, 0)
                editfac.DataGridView2.Rows.Add(20 & " %", 0, 0, 0)

                Dim row2 As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

                Dim id = row2.Cells(6).Value.ToString

                If row2.Cells(3).Value = "Payé" Then
                    editfac.Label24.Text = "Facture payée"
                    editfac.Label24.ForeColor = Color.Green
                    editfac.Label37.Text = "Facture payée"
                    editfac.Label37.ForeColor = Color.Green
                    editfac.Label13.Visible = False
                    editfac.Label29.Visible = False
                    editfac.Label30.Visible = False
                    editfac.TextBox5.Visible = False
                    editfac.IconButton2.Visible = False
                End If
                adpt2 = New MySqlDataAdapter("select * from facture WHERE OrderID = @day ", conn2)
                adpt2.SelectCommand.Parameters.Clear()
                adpt2.SelectCommand.Parameters.AddWithValue("@day", id)
                Dim tablepay As New DataTable
                adpt2.Fill(tablepay)
                adpt2 = New MySqlDataAdapter("select client,adresse,ville,ICE from clients where client = '" + row2.Cells(4).Value.ToString + "' ", conn2)
                Dim table2 As New DataTable
                adpt2.Fill(table2)
                If table2.Rows.Count() <> 0 Then
                    If IsDBNull(table2.Rows(0).Item(0)) Then
                        editfac.Label7.Text = "-"

                    Else
                        editfac.Label7.Text = table2.Rows(0).Item(0)
                    End If

                    If IsDBNull(table2.Rows(0).Item(1)) Then
                        editfac.Label8.Text = "-"

                    Else
                        editfac.Label8.Text = table2.Rows(0).Item(1)

                    End If
                    If IsDBNull(table2.Rows(0).Item(2)) Then
                        editfac.Label12.Text = "-"
                    Else
                        editfac.Label12.Text = table2.Rows(0).Item(2)
                    End If
                    editfac.Label2.Text = table2.Rows(0).Item(3)
                End If
                editfac.Label26.Text = row2.Cells(4).Value
                    editfac.Label11.Text = id
                    editfac.Label6.Text = Convert.ToDateTime(row2.Cells(2).Value).ToString("dd/MM/yyyy")
                    editfac.TextBox1.Text = "Facture N° " + id
                    editfac.DataGridView1.Visible = True
                    editfac.DataGridView3.Visible = False
                    editfac.DataGridView1.Rows.Clear()
                    Dim sumttc As Double
                    Dim sumht As Double
                    Dim sumrms As Double

                    adpt = New MySqlDataAdapter("select fac_id, ref, des, unite, qte, pu_ht, pu_ttc, remise, tot_ht, tot_ttc, tva,marge,gr from facturedetails where fac_id = '" + id + "' ", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    For i = 0 To table.Rows.Count - 1
                        Dim totht As Double = Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(" ", "").Replace(".", ","))
                        Dim totttc As Double = Convert.ToDouble(table.Rows(i).Item(9).ToString.Replace(" ", "").Replace(".", ","))
                        editfac.DataGridView1.Rows.Add(table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(" ", "").Replace(".", ",")), Convert.ToDouble(table.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")), Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(" ", "").Replace(".", ",")), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(" ", "").Replace(".", ",")), Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(" ", "").Replace(".", ",")), Convert.ToDouble(table.Rows(i).Item(9).ToString.Replace(" ", "").Replace(".", ",")), Convert.ToDouble(table.Rows(i).Item(10).ToString.Replace(" ", "").Replace(".", ",")), Convert.ToDouble(table.Rows(i).Item(11).ToString.Replace(" ", "").Replace(".", ",")), Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(" ", "").Replace(".", ",")))

                    Next

                    Dim sumremise As Double = 0
                    Dim rms As Double = 0


                    Dim ht, ht7, ht10, ht14, ht20 As Double
                    Dim tva, tva7, tva10, tva14, tva20 As Double
                    Dim sum0 As Double
                    Dim sum7 As Double
                    Dim sum10 As Double
                    Dim sum14 As Double
                    Dim sum20 As Double

                    For j = 0 To editfac.DataGridView1.Rows.Count() - 1

                        sumttc += editfac.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                        sumht += editfac.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                        sumrms += (editfac.DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * (editfac.DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - editfac.DataGridView1.Rows(j).Cells(11).Value.ToString.Replace(" ", "").Replace(".", ","))) * (editfac.DataGridView1.Rows(j).Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") / 100)

                        If editfac.DataGridView1.Rows(j).Cells(9).Value = 0 Then
                            sum0 += editfac.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht += editfac.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva = 0

                        End If

                        If editfac.DataGridView1.Rows(j).Cells(9).Value = 7 Then
                            sum7 += editfac.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht7 += editfac.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva7 = sum7 - ht7
                        End If

                        If editfac.DataGridView1.Rows(j).Cells(9).Value = 10 Then
                            sum10 += editfac.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht10 += editfac.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva10 = sum10 - ht10
                        End If

                        If editfac.DataGridView1.Rows(j).Cells(9).Value = 14 Then
                            sum14 += editfac.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht14 += editfac.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva14 = sum14 - ht14
                        End If
                        If editfac.DataGridView1.Rows(j).Cells(9).Value = 20 Then
                            sum20 += editfac.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht20 += editfac.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva20 = sum20 - ht20
                        End If

                    Next

                    editfac.DataGridView2.Rows(0).Cells(1).Value = Convert.ToDouble(ht.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(0).Cells(2).Value = Convert.ToDouble(tva.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(0).Cells(3).Value = Convert.ToDouble(sum0.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(1).Cells(1).Value = Convert.ToDouble(ht7.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(1).Cells(2).Value = Convert.ToDouble(tva7.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(1).Cells(3).Value = Convert.ToDouble(sum7.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(2).Cells(1).Value = Convert.ToDouble(ht10.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(2).Cells(2).Value = Convert.ToDouble(tva10.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(2).Cells(3).Value = Convert.ToDouble(sum10.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(3).Cells(1).Value = Convert.ToDouble(ht14.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(3).Cells(2).Value = Convert.ToDouble(tva14.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(3).Cells(3).Value = Convert.ToDouble(sum14.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(4).Cells(1).Value = Convert.ToDouble(ht20.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(4).Cells(2).Value = Convert.ToDouble(tva20.ToString.Replace(".", ",")).ToString("N2")
                    editfac.DataGridView2.Rows(4).Cells(3).Value = Convert.ToDouble(sum20.ToString.Replace(".", ",")).ToString("N2")

                    editfac.Label36.Text = Convert.ToDouble(tablepay.Rows(0).Item(9).ToString.Replace(".", ",")).ToString("N2")
                    editfac.Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("N2")
                    If IsDBNull(tablepay.Rows(0).Item(11)) Then
                        editfac.Label28.Text = Convert.ToDouble(tablepay.Rows(0).Item(12).ToString.Replace(".", ",")).ToString("N2")
                        editfac.Label43.Text = 0
                    Else
                        editfac.Label28.Text = tablepay.Rows(0).Item(12)
                        editfac.Label43.Text = tablepay.Rows(0).Item(11)
                    End If
                    editfac.Label10.Text = Convert.ToDouble(sumttc - (sumht - sumremise)).ToString("N2")
                    If IsNumeric(editfac.Label43.Text) AndAlso editfac.Label43.Text = 0 Then
                        editfac.Label15.Text = Convert.ToDouble(Convert.ToDouble(editfac.Label9.Text.Replace(".", ",")) + Convert.ToDouble(editfac.Label10.Text) + editfac.Label36.Text).ToString("N2")
                    Else
                        editfac.Label15.Text = Convert.ToDouble((Convert.ToDouble(editfac.Label9.Text.Replace(".", ",")) + Convert.ToDouble(editfac.Label10.Text) + editfac.Label36.Text) - Convert.ToDouble(editfac.Label28.Text)).ToString("N2")
                    End If

                Else
                    If Panel7.Visible = False Then

                    Dim totalCumule As Decimal = 0

                    If e.ColumnIndex = 0 AndAlso e.RowIndex >= 0 Then
                        ' Inverse l'état de la case à cocher
                        If DataGridView1.Rows(e.RowIndex).Cells(8).Value() <= 0 Then
                            MsgBox("Cette facture est déjà encaissée !")
                        Else

                            Dim cell As DataGridViewCheckBoxCell = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)
                            cell.Value = Not Convert.ToBoolean(cell.Value)

                        End If
                        For Each row As DataGridViewRow In DataGridView1.Rows
                            Dim cell As DataGridViewCheckBoxCell = row.Cells(0)
                            If Convert.ToBoolean(cell.Value) Then
                                Dim valeurColonne As Decimal = Convert.ToDecimal(row.Cells(8).Value) ' Remplacez "NomDeLaColonne" par le nom réel de la colonne
                                totalCumule += valeurColonne
                            End If
                        Next
                        IconButton22.Text = totalCumule.ToString("# ##0.00")
                    End If

                Else
                    Dim totalCumule As Decimal = 0
                    Dim count As Integer = 0

                    If e.ColumnIndex = 0 AndAlso e.RowIndex >= 0 Then
                        Dim cell2 As DataGridViewCheckBoxCell = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)
                        cell2.Value = Not Convert.ToBoolean(cell2.Value)
                        For Each row As DataGridViewRow In DataGridView1.Rows
                            Dim cell As DataGridViewCheckBoxCell = row.Cells(0)
                            If Convert.ToBoolean(cell.Value) Then
                                Dim valeurColonne As Decimal = Convert.ToDecimal(row.Cells(2).Value) ' Remplacez "NomDeLaColonne" par le nom réel de la colonne
                                totalCumule += valeurColonne
                                count += 1
                            End If
                        Next
                        IconButton37.Text = totalCumule.ToString("# ##0.00")
                        IconButton34.Text = count
                    End If


                End If
            End If
        End If
    End Sub
    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs)


    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        DataGridView1.Rows.Clear()

        If ComboBox1.Text = "" OrElse ComboBox1.Text = "Tout" Then
            If ComboBox2.Text = "" OrElse ComboBox2.Text = "Tout" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin order by OrderID desc", conn2)
            ElseIf ComboBox2.Text = "Espèce" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin AND etat='Payé' order by OrderID desc", conn2)
            ElseIf ComboBox2.Text = "Chèque" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin AND etat='Payé' order by OrderID desc", conn2)
            ElseIf ComboBox2.Text = "TPE" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin AND etat='Payé' order by OrderID desc", conn2)
            ElseIf ComboBox2.Text = "Effet" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin AND etat='Payé' order by OrderID desc", conn2)
            ElseIf ComboBox2.Text = "Virement" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin AND etat='Payé' order by OrderID desc", conn2)
            ElseIf ComboBox2.Text = "Crédit" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin AND etat='Non Payé' order by OrderID desc", conn2)
            End If
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)
        Else
            If ComboBox2.Text = "" OrElse ComboBox2.Text = "Tout" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin AND client = @clt order by OrderID desc", conn2)
            ElseIf ComboBox2.Text = "Espèce" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin AND client = @clt AND etat='Payé' order by OrderID desc", conn2)
            ElseIf ComboBox2.Text = "Chèque" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin AND client = @clt AND etat='Payé' order by OrderID desc", conn2)
            ElseIf ComboBox2.Text = "TPE" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin AND client = @clt AND etat='Payé' order by OrderID desc", conn2)
            ElseIf ComboBox2.Text = "Effet" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin AND client = @clt AND etat='Payé' order by OrderID desc", conn2)
            ElseIf ComboBox2.Text = "Virement" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin AND client = @clt AND etat='Payé' order by OrderID desc", conn2)
            ElseIf ComboBox2.Text = "Crédit" Then
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE OrderDate BETWEEN @datedebut AND @datefin AND client = @clt AND etat='Non Payé' order by OrderID desc", conn2)
            End If
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox1.Text)
        End If
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        Dim mode As String = ""
        For i = 0 To table.Rows.Count - 1
            If ComboBox2.Text = "Crédit" Then
                mode = "Crédit"
                DataGridView1.Rows.Add("F-" & table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(5), mode, table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")), table.Rows(i).Item(14) & " (" & table.Rows(i).Item(15) & ")")
                If table.Rows(i).Item(3) = "Non Payé" Then
                    sum = sum + table.Rows(i).Item(8).ToString.Replace(".", ",")

                Else
                    sum = sum + table.Rows(i).Item(7).ToString.Replace(".", ",")
                End If
            Else
                If ComboBox2.Text = "Tout" Then
                    If table.Rows(i).Item(3) = "Non Payé" Then
                        mode = "Crédit"
                    Else
                        adpt = New MySqlDataAdapter("select reglement from reglement_fac where fac = '" & table.Rows(i).Item(0) & "'", conn2)
                        Dim table2 As New DataTable
                        adpt.Fill(table2)
                        If table2.Rows.Count <> 0 Then

                            For j = 0 To table2.Rows.Count - 1
                                adpt = New MySqlDataAdapter("select `montant`, `date`, `mode` FROM `reglement` where fac = '" & table2.Rows(j).Item(0) & "'", conn2)
                                Dim table4 As New DataTable
                                adpt.Fill(table4)
                                If table4.Rows.Count <> 0 Then
                                    mode = table4.Rows(0).Item(2).ToString.Replace(" (Comptoir)", "").Replace(" (Coffre)", "")
                                End If
                            Next
                        Else
                            adpt = New MySqlDataAdapter("select `modePayement` FROM `orders` where fac_id = '" & table.Rows(i).Item(0) & "' and modePayement <> 'Crédit'", conn2)
                            Dim table4 As New DataTable
                            adpt.Fill(table4)
                            If table4.Rows.Count <> 0 Then
                                mode = table4.Rows(0).Item(0).ToString.Replace(" (Comptoir)", "").Replace(" (Coffre)", "")
                            End If
                        End If

                    End If
                    DataGridView1.Rows.Add("F-" & table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(5), mode, table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")), table.Rows(i).Item(14) & " (" & table.Rows(i).Item(15) & ")")
                    If table.Rows(i).Item(3) = "Non Payé" Then
                        sum = sum + table.Rows(i).Item(8).ToString.Replace(".", ",")

                    Else
                        sum = sum + table.Rows(i).Item(7).ToString.Replace(".", ",")
                    End If
                Else
                    adpt = New MySqlDataAdapter("select reglement from reglement_fac where fac = '" & table.Rows(i).Item(0) & "'", conn2)
                    Dim table2 As New DataTable
                    adpt.Fill(table2)
                    If table2.Rows.Count <> 0 Then

                        For j = 0 To table2.Rows.Count - 1
                            adpt = New MySqlDataAdapter("select `montant`, `date`, `mode` FROM `reglement` where fac = '" & table2.Rows(j).Item(0) & "'", conn2)
                            Dim table4 As New DataTable
                            adpt.Fill(table4)
                            If table4.Rows.Count <> 0 Then
                                mode = table4.Rows(0).Item(2).ToString.Replace(" (Comptoir)", "").Replace(" (Coffre)", "")
                            Else
                                mode = "Espèce"
                            End If
                        Next
                    Else
                        adpt = New MySqlDataAdapter("select `modePayement` FROM `orders` where fac_id = '" & table.Rows(i).Item(0) & "' and modePayement <> 'Crédit'", conn2)
                        Dim table4 As New DataTable
                        adpt.Fill(table4)
                        If table4.Rows.Count <> 0 Then
                            mode = table4.Rows(0).Item(0)
                        End If
                    End If
                    If mode = ComboBox2.Text Then
                        DataGridView1.Rows.Add("F-" & table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(5), mode, table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")), table.Rows(i).Item(14) & " (" & table.Rows(i).Item(15) & ")")
                        If table.Rows(i).Item(3) = "Non Payé" Then
                            sum = sum + table.Rows(i).Item(8).ToString.Replace(".", ",")

                        Else
                            sum = sum + table.Rows(i).Item(7).ToString.Replace(".", ",")
                        End If
                    End If
                End If


            End If



        Next

        IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00") & " Dhs"
        IconButton2.Text = table.Rows.Count
        DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"

        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(8).Value <= 0 Then
                row.Cells(8).Style.ForeColor = Color.Red
                row.Cells(7).Style.ForeColor = Color.Red
            Else
                row.Cells(8).Style.ForeColor = Color.Green
                row.Cells(7).Style.ForeColor = Color.Red
            End If
        Next
        DataGridView1.ClearSelection()
        Panel6.Visible = False
    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click


        DataGridView1.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from facture WHERE Day(facture.OrderDate) = @day and month(facture.OrderDate)=@month and year(facture.OrderDate)=@year order by OrderID desc", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
        adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
        adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        For i = 0 To table.Rows.Count - 1
            DataGridView1.Rows.Add("F-" & table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(5), mode, table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")), table.Rows(i).Item(14) & " (" & table.Rows(i).Item(15) & ")")

            sum = sum + table.Rows(i).Item(2).ToString.Replace(".", ",")
        Next

        IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00") & " Dhs"
        IconButton2.Text = table.Rows.Count
        DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"

        adpt = New MySqlDataAdapter("select client from clients order by client asc", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count <> 0 Then
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Tout")
            For i = 0 To table2.Rows.Count - 1
                ComboBox1.Items.Add(table2.Rows(i).Item(0))
            Next
            ComboBox1.SelectedIndex = 0

        End If
        ComboBox2.SelectedIndex = 0
        DateTimePicker1.Text = Today
        DateTimePicker2.Text = Today

        AddHandler DataGridView1.RowPrePaint, AddressOf DataGridView1_RowPrePaint

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


    Private Sub iconbutton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
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

    Private Sub IconButton24_Click(sender As Object, e As EventArgs) Handles IconButton24.Click
        banques.Show()
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

    Private Sub IconButton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        Home.Show()
        Me.Close()
    End Sub

    Private Sub IconButton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
        stock.Show()
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            adpt = New MySqlDataAdapter("select client from clients order by client asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                ComboBox1.Items.Clear()
                ComboBox1.Items.Add("Tout")
                For i = 0 To table.Rows.Count - 1
                    ComboBox1.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox1.SelectedIndex = 0

            End If
        Else

            Dim inputText As String = TextBox1.Text

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
                RemoveHandler TextBox1.TextChanged, AddressOf TextBox1_TextChanged

                ' Mettez à jour le texte dans le TextBox
                TextBox1.Text = modifiedText

                ' Replacez le curseur à la position correcte après la modification
                TextBox1.SelectionStart = TextBox1.Text.Length

                ' Réactivez le gestionnaire d'événements TextChanged
                AddHandler TextBox1.TextChanged, AddressOf TextBox1_TextChanged
            End If

            ComboBox1.Items.Clear()
            adpt = New MySqlDataAdapter("select client from clients WHERE client LIKE '%" + TextBox1.Text.Replace("'", " ") + "%' order by client asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                For i = 0 To table.Rows.Count - 1
                    ComboBox1.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox1.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Encaissement Facture'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Panel1.Visible = True
                Panel1.BringToFront()
                ' Créer la colonne avec des cases à cocher
                If DataGridView1.Columns.Count = 11 Then
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
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton20_Click(sender As Object, e As EventArgs) Handles IconButton20.Click
        Panel1.Visible = False
        DateTimePicker5.Value = DateTime.Today
        DateTimePicker3.Value = DateTime.Today
        DataGridView1.Visible = True
        ComboBox8.Text = ""
        ComboBox7.Text = ""
        TextBox21.Text = ""
        TextBox20.Text = ""
        TextBox8.Text = ""
        TextBox16.Text = ""
        TextBox15.Text = ""
        TextBox14.Text = ""
        If DataGridView1.Columns.Contains("CheckBoxColumn") Then
            ' Supprimer la colonne CheckBox
            DataGridView1.Columns.Remove("CheckBoxColumn")
        End If
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(8).Value <= 0 Then
                row.Cells(8).Style.ForeColor = Color.Red
                row.Cells(7).Style.ForeColor = Color.Red
            Else
                row.Cells(8).Style.ForeColor = Color.Green
                row.Cells(7).Style.ForeColor = Color.Red
            End If
        Next
        Panel3.Visible = False
        DataGridView1.ClearSelection()
    End Sub

    Private Sub IconButton19_Click(sender As Object, e As EventArgs) Handles IconButton19.Click
        Panel3.Visible = True
        TextBox2.Text = IconButton22.Text
        ComboBox3.Text = "Espèce (Comptoir)"
        DataGridView1.Visible = False
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Text = "Espèce (Comptoir)" Or ComboBox3.Text = "Espèce (Coffre)" Then
            tpepanel.Visible = False
            chqpanel.Visible = False
        End If
        If ComboBox3.Text = "Chèque" Or ComboBox3.Text = "Virement" Or ComboBox3.Text = "Effet" Then
            tpepanel.Visible = False
            chqpanel.Visible = True
        End If

        If ComboBox3.Text = "TPE" Then
            tpepanel.Visible = True
            chqpanel.Visible = False
        End If
    End Sub

    Private Sub IconButton28_Click(sender As Object, e As EventArgs) Handles IconButton28.Click
        chqpanel.Visible = False
    End Sub

    Private Sub IconButton26_Click(sender As Object, e As EventArgs) Handles IconButton26.Click
        tpepanel.Visible = False
    End Sub

    Private Sub IconButton30_Click(sender As Object, e As EventArgs) Handles IconButton30.Click
        Panel3.Visible = False
        tpepanel.Visible = False
        chqpanel.Visible = False
        DataGridView1.Visible = True
    End Sub
    Dim sql3 As String
    Dim cmd3 As MySqlCommand

    Private Sub IconButton29_Click(sender As Object, e As EventArgs) Handles IconButton29.Click
        Dim Rep As Integer
        Rep = MsgBox("Voulez-vous vraiment effectuer cet encaissement ?", vbYesNo)
        If Rep = vbYes Then

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
            For i As Integer = selectedRows.Count - 1 To 0 Step -1
                Dim row As DataGridViewRow = selectedRows(i)
                sql3 = "UPDATE `facture` SET `etat` = @etat, `paye`=@paye,`modePayement`=@mode,`reste`=@reste WHERE `OrderID` = @id"
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@id", row.Cells(7).Value)
                    If (Convert.ToDecimal(row.Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")) + resteencaisse).ToString.Replace(".", ",").Replace(" ", "") >= Convert.ToDecimal(row.Cells(2).Value.ToString.Replace(".", ",").Replace(" ", "")) Then
                        cmd3.Parameters.AddWithValue("@paye", Convert.ToDecimal(row.Cells(2).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString.Replace(".", ",").Replace(" ", ""))
                        cmd3.Parameters.AddWithValue("@reste", 0.00)
                        cmd3.Parameters.AddWithValue("@mode", ComboBox3.Text)
                        cmd3.Parameters.AddWithValue("@etat", "Payé")
                        cmd3.ExecuteNonQuery()

                        sql3 = "INSERT INTO `reglement_fac`(`reglement`, `fac`, `montant`, `date`) VALUES (@type,@fac,@montant,@date) "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@type", regl_id)
                        cmd3.Parameters.AddWithValue("@fac", row.Cells(7).Value.ToString())
                        cmd3.Parameters.AddWithValue("@montant", Convert.ToDecimal(row.Cells(2).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString.Replace(".", ",").Replace(" ", ""))
                        cmd3.Parameters.AddWithValue("@date", Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd3.ExecuteNonQuery()
                    Else
                        cmd3.Parameters.AddWithValue("@paye", (Convert.ToDecimal(row.Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")) + resteencaisse).ToString.Replace(".", ",").Replace(" ", ""))
                        cmd3.Parameters.AddWithValue("@reste", (Convert.ToDecimal(row.Cells(2).Value.ToString.Replace(".", ",").Replace(" ", "")) - Convert.ToDecimal(row.Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")) - resteencaisse).ToString.Replace(" ", "").Replace(".", ","))
                        cmd3.Parameters.AddWithValue("@mode", "Crédit")
                        cmd3.Parameters.AddWithValue("@etat", "Non Payé")
                        cmd3.ExecuteNonQuery()

                        sql3 = "INSERT INTO `reglement_fac`(`reglement`, `fac`, `montant`, `date`) VALUES (@type,@fac,@montant,@date) "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@type", regl_id)
                        cmd3.Parameters.AddWithValue("@fac", row.Cells(7).Value.ToString())
                        cmd3.Parameters.AddWithValue("@montant", resteencaisse.ToString.Replace(".", ",").Replace(" ", ""))
                        cmd3.Parameters.AddWithValue("@date", Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd3.ExecuteNonQuery()
                    End If

                    sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@name", user)
                    cmd3.Parameters.AddWithValue("@op", "Réglement de la Facture N° " & row.Cells(7).Value.ToString())
                    cmd3.ExecuteNonQuery()
                    client = row.Cells(5).Value.ToString()


                    resteencaisse = resteencaisse - Convert.ToDecimal(row.Cells(8).Value.ToString.Replace(".", ",").Replace(" ", ""))
                    If resteencaisse <= 0 Then
                        Exit For
                    End If

            Next

            Dim clot As String = 0
            If DateTimePicker5.Value.ToString("yyyy-MM-dd") = DateTime.Now.ToString("yyyy-MM-dd") Then
            Else

                clot = 1
            End If

            sql3 = "INSERT INTO `reglement`( `type`, `montant`, `date`, `mode`, `client`,`fac`, `echeance`,`cloture`) VALUES (@type,@montant,@date,@mode,@clt,@achat,@ech,@clot) "
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            cmd3.Parameters.AddWithValue("@type", "fac")
            cmd3.Parameters.AddWithValue("@montant", Convert.ToDouble(TextBox2.Text.Replace(".", ",")).ToString.Replace(".", ",").Replace(" ", ""))
            cmd3.Parameters.AddWithValue("@date", Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss"))
            cmd3.Parameters.AddWithValue("@ech", Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss"))
            cmd3.Parameters.AddWithValue("@mode", ComboBox3.Text)
            cmd3.Parameters.AddWithValue("@clt", client)
            cmd3.Parameters.AddWithValue("@achat", regl_id)
            cmd3.Parameters.AddWithValue("@clot", clot)
            cmd3.ExecuteNonQuery()

            If ComboBox3.Text = "TPE" Then
                sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `fac`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('tpe','" + TextBox21.Text + "','tpe','" + TextBox20.Text + "','tpe', '" + Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & regl_id & "','" + ComboBox8.Text + "','" + TextBox2.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & client & "','" & ComboBox3.Text & "')"
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.ExecuteNonQuery()
            End If
            If ComboBox3.Text = "Chèque" Then
                sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `fac`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('" + TextBox8.Text + "','" + TextBox16.Text + "','" + TextBox15.Text + "','" + TextBox14.Text + "','" + Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & regl_id & "','" + ComboBox7.Text + "','" + TextBox2.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & client & "','" & ComboBox3.Text & "')"
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.ExecuteNonQuery()
            End If
            If ComboBox3.Text = "Virement" Then
                sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `fac`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('" + TextBox8.Text + "','" + TextBox16.Text + "','" + TextBox15.Text + "','" + TextBox14.Text + "','" + Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & regl_id & "','" + ComboBox7.Text + "','" + TextBox2.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & client & "','" & ComboBox3.Text & "')"
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.ExecuteNonQuery()
            End If
            If ComboBox3.Text = "Effet" Then
                sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `fac`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('" + TextBox8.Text + "','" + TextBox16.Text + "','" + TextBox15.Text + "','" + TextBox14.Text + "','" + Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & regl_id & "','" + ComboBox7.Text + "','" + TextBox2.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & client & "','" & ComboBox3.Text & "')"
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.ExecuteNonQuery()
            End If

            MsgBox("Encaissement bien effectué !")

            IconButton20.PerformClick()
            conn2.Close()

        End If
        AddHandler DataGridView1.RowPrePaint, AddressOf DataGridView1_RowPrePaint

    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click

    End Sub

    Private Sub IconButton25_Click(sender As Object, e As EventArgs) Handles IconButton25.Click
        rapproche.Show()
        rapproche.Panel6.Visible = True
        rapproche.Panel8.BringToFront()
        rapproche.Label16.Text = "Paiements encaissés"

        adpt = New MySqlDataAdapter("select client from clients order by client asc", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count <> 0 Then
            rapproche.ComboBox2.Items.Clear()
            rapproche.ComboBox2.Items.Add("Tout")
            For i = 0 To table2.Rows.Count - 1
                rapproche.ComboBox2.Items.Add(table2.Rows(i).Item(0))
            Next
            rapproche.ComboBox2.SelectedIndex = 0

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
    Private Sub IconButton31_Click(sender As Object, e As EventArgs) Handles IconButton31.Click
        Dim debit As Double = 0
        Dim credit As Double = 0
        Dim solde As Double = 0

        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("Vente")
        For i = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(4).Value)
            debit = debit + Convert.ToDouble(DataGridView1.Rows(i).Cells(1).Value.ToString.Replace(" ", ""))
            credit = credit + Convert.ToDouble(DataGridView1.Rows(i).Cells(8).Value.ToString.Replace(" ", ""))
        Next
        solde = debit - credit

        ReportToPrint = New LocalReport()
        formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\etatvente.rdlc"
        formata4.ReportViewer1.LocalReport.DataSources.Clear()
        Dim du As New ReportParameter("du", DateTimePicker1.Text)
        Dim du1(0) As ReportParameter
        du1(0) = du
        formata4.ReportViewer1.LocalReport.SetParameters(du1)

        Dim au As New ReportParameter("au", DateTimePicker2.Text)
        Dim au1(0) As ReportParameter
        au1(0) = au
        formata4.ReportViewer1.LocalReport.SetParameters(au1)

        Dim frs As New ReportParameter("frs", ComboBox1.Text)
        Dim frs1(0) As ReportParameter
        frs1(0) = frs
        formata4.ReportViewer1.LocalReport.SetParameters(frs1)

        Dim etat As New ReportParameter("etat", "Etat Client")
        Dim etat1(0) As ReportParameter
        etat1(0) = etat
        formata4.ReportViewer1.LocalReport.SetParameters(etat1)

        Dim debito As New ReportParameter("debit", debit.ToString("N2"))
        Dim debito1(0) As ReportParameter
        debito1(0) = debito
        formata4.ReportViewer1.LocalReport.SetParameters(debito1)

        Dim credito As New ReportParameter("credit", credit.ToString("N2"))
        Dim credito1(0) As ReportParameter
        credito1(0) = credito
        formata4.ReportViewer1.LocalReport.SetParameters(credito1)

        Dim soldito As New ReportParameter("solde", solde.ToString("N2"))
        Dim soldito1(0) As ReportParameter
        soldito1(0) = soldito
        formata4.ReportViewer1.LocalReport.SetParameters(soldito1)


        formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
        formata4.ReportViewer1.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next
        Dim printDoc As New PrintDocument()
        m_currentPageIndex = 0
        AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage

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

    Private Sub IconButton32_Click(sender As Object, e As EventArgs) Handles IconButton32.Click
        Panel4.Visible = True
    End Sub

    Private Sub IconButton35_Click(sender As Object, e As EventArgs) Handles IconButton35.Click
        Panel4.Visible = False
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub

    Private Sub IconButton36_Click(sender As Object, e As EventArgs) Handles IconButton36.Click
        adpt = New MySqlDataAdapter("select OrderID from facture order by OrderID desc", conn2)
        Dim table4 As New DataTable
        adpt.Fill(table4)
        Dim id As Integer
        If table4.Rows.Count <> 0 Then
            id = Val(table4.Rows(0).Item(0)) + 1
        Else
            id = Year(Now).ToString & "0001"
        End If
        conn2.Close()
        conn2.Open()
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = "INSERT INTO facture (`OrderID`, `OrderDate`, `MontantOrder`,`etat`,`modePayement`,`client`,`infos`,`paye`,`reste`) 
                    VALUES ('" & id & "', '" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "','" & TextBox4.Text.Replace(".", ",") & "','Non Payé','Crédit','" + ComboBox1.Text + "','créance','0','" & TextBox4.Text.Replace(".", ",") & "')"
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.ExecuteNonQuery()
        conn2.Close()
        MsgBox("Créance bien ajoutée !")
        IconButton35.PerformClick()
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged

    End Sub

    Private Sub IconButton27_Click(sender As Object, e As EventArgs) Handles IconButton27.Click
        If IconButton27.BackColor = Color.Goldenrod Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                row.Selected = False
            Next

            IconButton1.Text = 0.00
            IconButton27.BackColor = Color.Gold
        Else
            For Each row As DataGridViewRow In DataGridView1.Rows
                row.Selected = True
            Next
            Dim sum As Decimal = 0
            For i = 0 To DataGridView1.Rows.Count - 1
                sum += Convert.ToDecimal(DataGridView1.Rows(i).Cells(1).Value)
            Next
            IconButton1.Text = sum.ToString("# ##0.00")
            IconButton27.BackColor = Color.Goldenrod
        End If
    End Sub

    Private Sub Panel6_Paint(sender As Object, e As PaintEventArgs) Handles Panel6.Paint

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = False Then
            If ComboBox1.Text = "Tout" Or ComboBox1.Text = "" Then
                ComboBox1.Text = "Tout"
                Panel6.Visible = False
                Label16.Text = 0.00
                DataGridView1.ClearSelection()
                DataGridView1.Rows.Clear()
                IconButton1.Text = 0.00
                IconButton4.PerformClick()
            Else

                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE etat = @mode and client = @clt order by OrderID desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@mode", "Non Payé")
                adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox1.Text)
                DataGridView1.Rows.Clear()

                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                Dim mode As String = ""
                For i = 0 To table.Rows.Count - 1
                    If table.Rows(i).Item(3) = "Non Payé" Then
                        mode = "Crédit"
                    Else
                        adpt = New MySqlDataAdapter("select reglement from reglement_fac where fac = '" & table.Rows(i).Item(0) & "'", conn2)
                        Dim table2 As New DataTable
                        adpt.Fill(table2)
                        If table2.Rows.Count <> 0 Then

                            For j = 0 To table2.Rows.Count - 1
                                adpt = New MySqlDataAdapter("select `montant`, `date`, `mode` FROM `reglement` where fac = '" & table2.Rows(j).Item(0) & "'", conn2)
                                Dim table4 As New DataTable
                                adpt.Fill(table4)
                                If table4.Rows.Count <> 0 Then
                                    mode = table4.Rows(0).Item(2)
                                Else
                                    mode = "Espèce"
                                End If
                            Next
                        Else
                            adpt = New MySqlDataAdapter("select `modePayement` FROM `orders` where fac_id = '" & table.Rows(i).Item(0) & "' and modePayement <> 'Crédit'", conn2)
                            Dim table4 As New DataTable
                            adpt.Fill(table4)
                            If table4.Rows.Count <> 0 Then
                                mode = table4.Rows(0).Item(0)
                            End If
                        End If
                    End If
                    DataGridView1.Rows.Add("F-" & table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(5), mode, table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")), table.Rows(i).Item(14) & " (" & table.Rows(i).Item(15) & ")")

                    sum = sum + table.Rows(i).Item(8).ToString.Replace(".", ",")
                Next

                IconButton1.Text = 0.00

                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells(8).Value <= 0 Then
                        row.Cells(8).Style.ForeColor = Color.Red
                        row.Cells(7).Style.ForeColor = Color.Red
                    Else
                        row.Cells(8).Style.ForeColor = Color.Green
                        row.Cells(7).Style.ForeColor = Color.Red
                    End If
                Next
                DataGridView1.ClearSelection()
                Panel6.Visible = True

                Label16.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", ""))
                DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"

            End If
        Else
            If ComboBox1.Text = "Tout" Or ComboBox1.Text = "" Then
                ComboBox1.Text = "Tout"
                Panel6.Visible = False
                Label16.Text = 0.00
                DataGridView1.ClearSelection()
                DataGridView1.Rows.Clear()
                IconButton1.Text = 0.00
                IconButton4.PerformClick()
            Else

                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE client = @clt order by OrderID desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox1.Text)
                DataGridView1.Rows.Clear()
                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                Dim mode As String = ""
                For i = 0 To table.Rows.Count - 1
                    If table.Rows(i).Item(3) = "Non Payé" Then
                        mode = "Crédit"
                    Else
                        adpt = New MySqlDataAdapter("select reglement from reglement_fac where fac = '" & table.Rows(i).Item(0) & "'", conn2)
                        Dim table2 As New DataTable
                        adpt.Fill(table2)
                        If table2.Rows.Count <> 0 Then

                            For j = 0 To table2.Rows.Count - 1
                                adpt = New MySqlDataAdapter("select `montant`, `date`, `mode` FROM `reglement` where fac = '" & table2.Rows(j).Item(0) & "'", conn2)
                                Dim table4 As New DataTable
                                adpt.Fill(table4)
                                If table4.Rows.Count <> 0 Then
                                    mode = table4.Rows(0).Item(2)
                                Else
                                    mode = "Espèce"
                                End If
                            Next
                        Else
                            adpt = New MySqlDataAdapter("select `modePayement` FROM `orders` where fac_id = '" & table.Rows(i).Item(0) & "' and modePayement <> 'Crédit'", conn2)
                            Dim table4 As New DataTable
                            adpt.Fill(table4)
                            If table4.Rows.Count <> 0 Then
                                mode = table4.Rows(0).Item(0)
                            End If
                        End If
                    End If
                    DataGridView1.Rows.Add("F-" & table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(5), mode, table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")), table.Rows(i).Item(14) & " (" & table.Rows(i).Item(15) & ")")

                    sum = sum + table.Rows(i).Item(8).ToString.Replace(".", ",")
                Next

                IconButton1.Text = 0.00

                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells(8).Value <= 0 Then
                        row.Cells(8).Style.ForeColor = Color.Red
                        row.Cells(7).Style.ForeColor = Color.Red
                    Else
                        row.Cells(8).Style.ForeColor = Color.Green
                        row.Cells(7).Style.ForeColor = Color.Red
                    End If
                Next
                DataGridView1.ClearSelection()
                Panel6.Visible = True

                Label16.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", ""))
                DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"

            End If

        End If
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged

    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Four.Show()
        Me.Close()

    End Sub

    Private Sub IconButton39_Click(sender As Object, e As EventArgs) Handles IconButton39.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub IconButton33_Click(sender As Object, e As EventArgs) Handles IconButton33.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Declaration TVA'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Panel1.Visible = True
                Panel7.Visible = True
                Panel1.BringToFront()
                ' Créer la colonne avec des cases à cocher
                If DataGridView1.Columns.Count = 11 Then
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
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton38_Click(sender As Object, e As EventArgs) Handles IconButton38.Click
        Panel1.Visible = False
        Panel7.Visible = False
        If DataGridView1.Columns.Contains("CheckBoxColumn") Then
            ' Supprimer la colonne CheckBox
            DataGridView1.Columns.Remove("CheckBoxColumn")
        End If
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(8).Value <= 0 Then
                row.Cells(8).Style.ForeColor = Color.Red
                row.Cells(7).Style.ForeColor = Color.Red
            Else
                row.Cells(8).Style.ForeColor = Color.Green
                row.Cells(7).Style.ForeColor = Color.Red
            End If
        Next
        Panel3.Visible = False
        DataGridView1.ClearSelection()

    End Sub

    Private Sub IconButton40_Click(sender As Object, e As EventArgs) Handles IconButton40.Click
        Dim Rep As Integer
        Rep = MsgBox("Avez-vous vraiment déclarer ces factures ?", vbYesNo)
        If Rep = vbYes Then
            conn2.Close()
            conn2.Open()
            For Each row As DataGridViewRow In DataGridView1.Rows
                ' Vérifier si la case à cocher de la ligne est cochée
                If Convert.ToBoolean(row.Cells("CheckBoxColumn").Value) = True Then
                    sql3 = "UPDATE `facture` SET `declared` = 'Déclarée',`declardate` = '" & Convert.ToDateTime(DateTimePicker6.Value).ToString("yyyy-MM-dd HH:mm:ss") & "' WHERE `OrderID` = @id"
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@id", row.Cells(7).Value)
                    cmd3.ExecuteNonQuery()
                End If
            Next

            sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            cmd3.Parameters.AddWithValue("@name", user)
            cmd3.Parameters.AddWithValue("@op", "Déclaration de la TVA Facture le Mois " & Convert.ToDateTime(DateTimePicker6.Value).ToString("Y-MMMM"))
            cmd3.ExecuteNonQuery()

            MsgBox("Déclaration bien effectué !")
            IconButton38.PerformClick()
            conn2.Close()
            AddHandler DataGridView1.RowPrePaint, AddressOf DataGridView1_RowPrePaint
        End If
    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        users.Show()
        Me.Close()

    End Sub

    Private Sub ComboBox1_DropDownClosed(sender As Object, e As EventArgs) Handles ComboBox1.DropDownClosed
        If TextBox1.Text = "" Then
            ComboBox1.Text = "Tout"
            Panel6.Visible = False
            Label16.Text = 0.00
            DataGridView1.ClearSelection()
            DataGridView1.Rows.Clear()
            IconButton1.Text = 0.00
            IconButton4.PerformClick()
        Else

            Dim client As String = "Tout"
            If ComboBox1.Text = "Tout" Or ComboBox1.Text = "" Then
                Panel6.Visible = False
                Label16.Text = 0.00
                DataGridView1.ClearSelection()
                DataGridView1.Rows.Clear()
                IconButton1.Text = 0.00
            Else
                client = ComboBox1.Text
                adpt = New MySqlDataAdapter("SELECT * FROM facture WHERE etat = @mode and client = @clt order by OrderID desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@mode", "Non Payé")
                adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox1.Text)

                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                DataGridView1.Rows.Clear()

                For i = 0 To table.Rows.Count - 1
                    DataGridView1.Rows.Add("F-" & table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(5), mode, table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")), table.Rows(i).Item(14) & " (" & table.Rows(i).Item(15) & ")")

                    sum = sum + table.Rows(i).Item(8).ToString.Replace(".", ",")
                Next

                IconButton1.Text = 0.00

                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells(8).Value <= 0 Then
                        row.Cells(8).Style.ForeColor = Color.Red
                        row.Cells(7).Style.ForeColor = Color.Red
                    Else
                        row.Cells(8).Style.ForeColor = Color.Green
                        row.Cells(7).Style.ForeColor = Color.Red
                    End If
                Next
                DataGridView1.ClearSelection()
                Panel6.Visible = True

                Label16.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", ""))
                DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"



                ' Ajouter la colonne au début de la DataGridView

            End If
            TextBox1.Text = ""
            ComboBox1.Text = client

        End If
    End Sub
End Class