Imports System.Text
Imports MySql.Data.MySqlClient
Public Class devis
    Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Private Sub devis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler DataGridView1.RowPrePaint, AddressOf DataGridView1_RowPrePaint

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
        adpt = New MySqlDataAdapter("select * from devis WHERE Day(devis.dateAchat) = @day and month(devis.dateAchat)=@month and year(devis.dateAchat)=@year order by id desc", conn2)
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

            sum = sum + Convert.ToDouble(table.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ","))
        Next

        IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00") & " Dhs"

        adpt = New MySqlDataAdapter("select client from clients order by client asc", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count <> 0 Then
            ComboBox3.Items.Clear()
            ComboBox3.Items.Add("Tout")
            For i = 0 To table2.Rows.Count - 1
                ComboBox3.Items.Add(table2.Rows(i).Item(0))
            Next
            ComboBox3.SelectedIndex = 0

        End If


    End Sub


    Private Sub DataGridView1_CellMouseClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick

        If e.RowIndex >= 0 Then

            achatedit.Show()
                adpt = New MySqlDataAdapter("select client from clients order by client asc", conn2)
                Dim tableimg As New DataTable
                adpt.Fill(tableimg)

                For i = 0 To tableimg.Rows.Count() - 1
                    achatedit.ComboBox5.Items.Add(tableimg.Rows(i).Item(0))
                Next

                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim id As String = row.Cells(0).Value.ToString
                achatedit.Label11.Text = id
                achatedit.Label16.Text = "Devis :"

                achatedit.Label31.Text = row.Cells(6).Value.ToString
                achatedit.ComboBox5.Text = row.Cells(6).Value.ToString

                achatedit.Label32.Text = ""

                achatedit.DataGridView2.Rows.Add(0 & " %", 0, 0, 0)
                achatedit.DataGridView2.Rows.Add(7 & " %", 0, 0, 0)
                achatedit.DataGridView2.Rows.Add(10 & " %", 0, 0, 0)
                achatedit.DataGridView2.Rows.Add(14 & " %", 0, 0, 0)
                achatedit.DataGridView2.Rows.Add(20 & " %", 0, 0, 0)

                Dim sumttc As Double = 0
                Dim sumht As Double = 0
                Dim sumrms As Double = 0
                Dim sumtva As Double = 0

                adpt = New MySqlDataAdapter("select `rms`,`mtn_ht` from devis where id = '" & id & "' ", conn2)
                Dim tablerms As New DataTable
                adpt.Fill(tablerms)

                adpt = New MySqlDataAdapter("select `CodeArticle`, `NameArticle`, `qte`,`PA_HT`,`TVA`,`PA_TTC`,`total`,`TM`,`PV_HT`,`PV_TTC`,`rms`,`rt`,`gr`,`total_ht`  from devisdetails where achat_Id = '" & id & "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                Dim ord As Integer = 0
                For i = 0 To table.Rows.Count - 1
                    ord += 1
                    If table.Rows.Count() <> 0 Then

                        achatedit.DataGridView1.Rows.Add(ord.ToString("N0"), table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(13), table.Rows(i).Item(6), table.Rows(i).Item(7), table.Rows(i).Item(8), table.Rows(i).Item(9), Nothing, Nothing, Nothing, Nothing, Nothing, table.Rows(i).Item(10), table.Rows(i).Item(3), table.Rows(i).Item(11), table.Rows(i).Item(12), Convert.ToDouble(ord.ToString("N0")))
                    Else
                        MsgBox("Devis Vide, Veuillez resaisir le Devis.")
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
                    achatedit.Label15.Text = Convert.ToDouble(row.Cells(2).Value.ToString.Replace(".", ",")).ToString("# #00.00")
                    achatedit.Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("# #00.00")
                    achatedit.Label27.Text = Convert.ToDouble(sumrms.ToString.Replace(".", ",")).ToString("# #00.00")
                    achatedit.Label10.Text = Convert.ToDouble(sumtva.ToString.Replace(".", ",")).ToString("# #00.00")

                Else
                    achatedit.Label27.Text = Convert.ToDouble(tablerms.Rows(0).Item(0)).ToString("# #00.00")
                    achatedit.Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("# #00.00")
                    achatedit.Label15.Text = Convert.ToDouble(row.Cells(2).Value.ToString.Replace(".", ",")).ToString("# #00.00")
                    achatedit.Label10.Text = Convert.ToDouble(sumtva.ToString.Replace(".", ",")).ToString("# #00.00")

                End If



                adpt = New MySqlDataAdapter("select Fournisseur from devis where id = '" + achatedit.Label11.Text.ToString + "' ", conn2)
                Dim table3 As New DataTable
                adpt.Fill(table3)
                If IsDBNull(table3.Rows(0).Item(0)) Then
                    adpt2 = New MySqlDataAdapter("select client,ICE from clients where client = '" + table3.Rows(0).Item(0).ToString + "' ", conn2)
                    Dim table2 As New DataTable
                    adpt2.Fill(table2)

                    achatedit.Label7.Text = table2.Rows(0).Item(0)
                    achatedit.Label8.Text = "-"
                    achatedit.Label12.Text = "-"

                    Dim output As StringBuilder = New StringBuilder
                    For i = 0 To table2.Rows(0).Item(1).Length - 1
                        If IsNumeric(table2.Rows(0).Item(1)(i)) Then
                            output.Append(table2.Rows(0).Item(1)(i))
                        End If
                    Next
                    achatedit.Label2.Text = output.ToString()
                End If

                achatedit.TextBox1.Text = "Devis N° " & id

                achatedit.TextBox10.Text = row.Cells(1).Value.ToString
                achatedit.Label6.Text = row.Cells(5).Value.ToString
            achatedit.IconButton8.Visible = True

        End If

    End Sub
    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs)


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




    Private Sub IconButton1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        factures.Show()
        Me.Close()
    End Sub

    Private Sub IconButton17_Click(sender As Object, e As EventArgs)
        ajoutdevis.Show()

    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Four.Show()
        Me.Close()

    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        users.Show()
        Me.Close()

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

    End Sub

    Private Sub TextBox14_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox14.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox14.Text = "" Then
                Dim client As String = "Tout"
                client = ComboBox3.Text
                adpt = New MySqlDataAdapter("SELECT * FROM devis WHERE Fournisseur = @clt order by id desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox3.Text)

                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                DataGridView1.Rows.Clear()

                For i = 0 To table.Rows.Count - 1
                    DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(8), Convert.ToDecimal(table.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDecimal(table.Rows(i).Item(4).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDecimal(table.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))
                    sum = sum + table.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")


                Next

                IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2")


                DataGridView1.ClearSelection()
                DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"



                TextBox14.Text = ""
                ComboBox3.Text = client

            End If
        End If

    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Try
            If ComboBox3.Text = "Tout" Then
                adpt = New MySqlDataAdapter("SELECT * FROM devis WHERE dateAchat BETWEEN @datedebut AND @datefin order by id desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
            Else
                adpt = New MySqlDataAdapter("SELECT * FROM devis WHERE dateAchat BETWEEN @datedebut AND @datefin and Fournisseur = @frs order by id desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                adpt.SelectCommand.Parameters.AddWithValue("@frs", ComboBox3.Text)
            End If
            Dim table As New DataTable
            adpt.Fill(table)
            Dim sum As Double = 0
            Dim cnt As Integer = 0
            DataGridView1.Rows.Clear()
            For i = 0 To table.Rows.Count - 1
                Dim s As Double = 0
                Dim t As Double = 0
                Dim r As Double = 0
                s = table.Rows(i).Item(1).ToString.Replace(".", ",").Replace(" ", "")
                t = table.Rows(i).Item(4).ToString.Replace(".", ",").Replace(" ", "")
                r = table.Rows(i).Item(5).ToString.Replace(".", ",").Replace(" ", "")



                DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(8), s.ToString("N2"), t.ToString("N2"), r.ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))

                sum = sum + Convert.ToDouble(s)
            Next
            IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00") & " Dhs"
            DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        TextBox14.Text = ""

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        ajoutachat.Show()
        ajoutachat.IconButton1.Visible = False
        ajoutachat.IconButton9.Visible = False
        ajoutachat.IconButton14.Visible = False
        ajoutachat.IconButton19.Visible = True
        ajoutachat.Label16.Text = "Devis"
        ajoutachat.Panel9.Visible = True
        ajoutachat.Label35.Visible = False
        ajoutachat.Label41.Visible = False
        ajoutachat.TextBox17.Visible = False
        ajoutachat.TextBox25.Visible = False
        ajoutachat.IconButton13.Visible = True


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
            adpt = New MySqlDataAdapter("select client from clients WHERE client LIKE '%" + TextBox14.Text.Replace("'", " ") + "%' order by client asc", conn2)
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

    Private Sub ComboBox3_DropDownClosed(sender As Object, e As EventArgs) Handles ComboBox3.DropDownClosed
        Dim client As String = "Tout"
        client = ComboBox3.Text
        adpt = New MySqlDataAdapter("SELECT * FROM devis WHERE Fournisseur = @clt order by id desc", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox3.Text)

        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        DataGridView1.Rows.Clear()

        For i = 0 To table.Rows.Count - 1
            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(8), Convert.ToDecimal(table.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDecimal(table.Rows(i).Item(4).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDecimal(table.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Format(table.Rows(i).Item(3), "dd/MM/yyyy"), table.Rows(i).Item(2), Convert.ToDouble(table.Rows(i).Item(1).ToString.Replace(".", ",")), Convert.ToDateTime(table.Rows(i).Item(3)).ToString("yyyy/MM/dd"))
            sum = sum + table.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")


        Next

        IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2")


        DataGridView1.ClearSelection()
        DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"



        TextBox14.Text = ""
        ComboBox3.Text = client
    End Sub
End Class