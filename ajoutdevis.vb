Imports System.IO
Imports System.Text
Imports MySql.Data.MySqlClient
Public Class ajoutdevis
    'Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    'Dim conn2 As New MySqlConnection("datasource=localhost; username=root; password=; database=librairie_graph")
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Dim imgName As String = "default.png"

    Dim N As Integer = 0
    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Close()

    End Sub
    Dim sql2, sql3 As String
    Dim cmd2, cmd3 As MySqlCommand
    Dim comb1 As String
    Dim comb2 As String
    Dim text1 As String
    Dim text2 As String
    Dim text5 As String
    Dim text7 As String
    Dim text8 As String
    Dim text9 As String
    Dim text10 As String
    Dim text11 As String
    Dim oldy As Double

    Dim stck As Double



    Private Sub IconButton1_Click(sender As Object, e As EventArgs)

    End Sub



    Private Sub IconButton1_Click_1(sender As Object, e As EventArgs) Handles IconButton1.Click
        Try
            If ComboBox4.Text = "" Then
                MsgBox("Veuillez choisir le client !")

            Else

                conn2.Open()
                    Dim id As String
                    id = TextBox17.Text

                    Dim montant As Double = 0
                    Dim paye As Double = 0
                    Dim reste As Double = 0
                    montant = Convert.ToString(Label25.Text).Replace(".", ",")
                    paye = Convert.ToString(TextBox11.Text).Replace(".", ",")
                    reste = montant - paye

                sql2 = "INSERT INTO devis (OrderID,MontantOrder,client,OrderDate) VALUES ('" & id & "', '" + Label25.Text + "', '" + ComboBox4.Text + "', '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "' )"
                cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    For i = 0 To DataGridView3.Rows.Count - 1
                    cmd2 = New MySqlCommand("INSERT INTO `devisdetails`( `fac_id`,`ref`, `des`, `tva`, `pu_ht`, `pu_ttc`, `qte`,`unite`,`tot_ttc`,`remise`,`gr`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10)", conn2)
                    cmd2.Parameters.AddWithValue("@value15", id)
                        cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                        cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                    cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                    cmd2.Parameters.AddWithValue("@value4", Convert.ToDouble(DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                    cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                    cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                    cmd2.Parameters.AddWithValue("@value7", DataGridView3.Rows(i).Cells(11).Value)
                    cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(14).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                    cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                    cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                    cmd2.ExecuteNonQuery()

                Next




                'adpt = New MySqlDataAdapter("select path from infos", conn2)
                'Dim tableinfo As New DataTable
                'adpt.Fill(tableinfo)

                'Dim path As String = tableinfo.Rows(0).Item(0).ToString.Replace("/", "\") & "\orden.dat"

                '' Create or overwrite the file.
                'Dim fs As FileStream = File.Create(path)
                'adpt = New MySqlDataAdapter("select * from article where plu <> 0 order by id", conn2)
                'Dim table As New DataTable
                'adpt.Fill(table)

                '' Add text to the file.
                'For i = 0 To table.Rows.Count - 1
                '    Dim info As Byte() = New UTF8Encoding(True).GetBytes("260" & table.Rows(i).Item(18) & "EEEEE|" & 1 & " |" & table.Rows(i).Item(18) & "|" & "   |" & "    |" & Convert.ToString(table.Rows(i).Item(7)).Replace(",", "").Replace(".", "") & "|" & "W|" & table.Rows(i).Item(19) & "|" & 0 & "|" & table.Rows(i).Item(1) & vbTab & "|     |" & vbCrLf)
                '    fs.Write(info, 0, info.Length)
                'Next
                'fs.Close()
                MsgBox("Devis bien effectuée")

                conn2.Close()
                    Me.Close()

            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
    End Sub
    Dim num As Integer

    Private Sub ajoutdevis_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        adpt = New MySqlDataAdapter("select * from infos ", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Panel1.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel3.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel4.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))


        adpt = New MySqlDataAdapter("select OrderID from devis order by OrderID desc", conn2)
        Dim table4 As New DataTable
        adpt.Fill(table4)
        Dim ref As String
        If table4.Rows.Count <> 0 Then
            ref = Convert.ToString(Convert.ToDouble(table4.Rows(0).Item(0).ToString.Replace("D", "")) + 1).PadLeft(4, "0")
        Else
            ref = Convert.ToString("0001").PadLeft(4, "0")
        End If
        TextBox17.Text = "D" & ref.ToString


        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim tablebq As New DataTable
        adpt.Fill(tablebq)

        For i = 0 To tablebq.Rows.Count - 1
            ComboBox7.Items.Add(tablebq.Rows(i).Item(1))
            ComboBox8.Items.Add(tablebq.Rows(i).Item(1))
        Next

        adpt3 = New MySqlDataAdapter("select * from clients order by client asc", conn2)
        Dim table3 As New DataTable
        adpt3.Fill(table3)

        For i = 0 To table3.Rows.Count - 1
            ComboBox4.Items.Add(table3.Rows(i).Item(1))
        Next


    End Sub


    Private Sub TextBox7_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox7.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox7.Text = "" Then

                TextBox7.Text = 0
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            Else

                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
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
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            Else
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            End If
        End If
    End Sub

    Private Sub TextBox5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox5.Text = "" Then

                TextBox5.Text = 0
                TextBox6.Text = Convert.ToDouble(TextBox5.Text.Replace(".", ",") / (1 + (ComboBox6.Text.Replace(".", ",") / 100))).ToString("# ##0.00")
                TextBox8.Text = Convert.ToDouble(((TextBox6.Text.Replace(".", ",") - TextBox3.Text.Replace(".", ",")) / TextBox3.Text.Replace(".", ",")) * 100).ToString("# ##0.00")
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            Else
                TextBox6.Text = Convert.ToDouble(TextBox5.Text.Replace(".", ",") / (1 + (ComboBox6.Text.Replace(".", ",") / 100))).ToString("# ##0.00")
                TextBox8.Text = Convert.ToDouble(((TextBox6.Text.Replace(".", ",") - TextBox3.Text.Replace(".", ",")) / TextBox3.Text.Replace(".", ",")) * 100).ToString("# ##0.00")
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            End If
        End If
    End Sub
    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Text = "" Then

                TextBox3.Text = 0
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            Else

                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            End If
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox6.Text = "" Or TextBox8.Text = "" Or TextBox7.Text = "" Or TextBox6.Text = "" Or TextBox5.Text = "" Or TextBox10.Text = "" Or TextBox9.Text = "" Or TextBox13.Text = "" Then
            MsgBox("Remplis tous")
        Else


            Dim pattc As Double = TextBox7.Text
            Dim qte As Double = TextBox10.Text
            Dim mtnttc As Double = Convert.ToDouble(TextBox7.Text) * Convert.ToDouble(TextBox10.Text)

            DataGridView3.Rows.Add(Nothing, TextBox1.Text, TextBox2.Text.Replace("'", ""), TextBox3.Text.Replace(".", ","), ComboBox6.Text, pattc.ToString("# ##0.00"), TextBox8.Text.Replace(".", ","), TextBox6.Text, TextBox5.Text.Replace(".", ","), qte.ToString("# ##0.00"), TextBox9.Text.Replace(".", ","), ComboBox3.Text, Nothing, Nothing, mtnttc.ToString("# ##0.00"), TextBox13.Text.Replace(".", ","), "0,00", TextBox3.Text, "0,00", "0,00", TextBox18.Text, TextBox19.Text)
            Dim sum As Double = 0
            Dim ht As Double = 0
            Dim remise As Double = 0
            For i = 0 To DataGridView3.Rows.Count - 1
                sum = sum + DataGridView3.Rows(i).Cells(14).Value.ToString.Replace(".", ",")
                remise = remise + (DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",") * DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",") * (DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ",") / 100))
                ht = ht + (DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",") * DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",")) * (1 - (DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ",") / 100))
            Next

            Label25.Text = sum.ToString("# ##0.00")
            Label23.Text = remise.ToString("# ##0.00")
            Label18.Text = ht.ToString("# ##0.00")
            TextBox11.Text = 0
            CheckBox1.Checked = False
            TextBox2.Text = ""
            TextBox1.Text = ""
            TextBox3.Text = 0
            ComboBox6.Text = ""
            TextBox5.Text = 0
            TextBox6.Text = 0
            TextBox7.Text = 0
            TextBox8.Text = 0
            TextBox9.Text = 0
            TextBox10.Text = 0
            TextBox13.Text = 0

            ComboBox6.SelectedItem = ""
        End If


    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        If ComboBox5.Text = "Crédit" Then
            TextBox11.Visible = True
            chqpanel.Visible = False
            tpepanel.Visible = False
        End If

        If ComboBox5.Text = "Chèque" Then
            TextBox11.Visible = False
            chqpanel.Visible = True
            tpepanel.Visible = False
        End If
        If ComboBox5.Text = "Espèce" Then
            TextBox11.Visible = False
            chqpanel.Visible = False
            tpepanel.Visible = False
        End If
        If ComboBox5.Text = "TPE" Then
            TextBox11.Visible = False
            chqpanel.Visible = False
            tpepanel.Visible = True
        End If
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged

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

    Private Sub DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 0 Then
                Dim Rep As Integer
                Rep = MsgBox("Voulez-vous vraiment supprimer ce produit ?", vbYesNo)
                If Rep = vbYes Then
                    Dim row As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
                    DataGridView3.Rows.Remove(row)
                    Dim sum As Double = 0
                    Dim ht As Double = 0
                    Dim remise As Double = 0
                    For i = 0 To DataGridView3.Rows.Count - 1
                        sum = sum + DataGridView3.Rows(i).Cells(14).Value.ToString.Replace(".", ",")
                        remise = remise + (DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",") * DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",") * (DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ",") / 100))
                        ht = ht + (DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",") * DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",")) * (1 - (DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ",") / 100))
                    Next

                    Label25.Text = sum.ToString("# ##0.00")
                    Label23.Text = remise.ToString("# ##0.00")
                    Label18.Text = ht.ToString("# ##0.00")
                    TextBox11.Text = sum
                End If
            End If

        End If

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Dim execution As Integer

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim cod As Integer = CInt(Math.Ceiling(Rnd() * 999999999)) + 1
        adpt = New MySqlDataAdapter("select * from article where Code = '" + cod.ToString + "'", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        If table.Rows.Count = 0 Then
            TextBox1.Text = cod
            TextBox2.Select()
            TextBox2.Text = ""
            TextBox3.Text = 0
            ComboBox6.Text = ""
            TextBox5.Text = 0
            TextBox6.Text = 0
            TextBox7.Text = 0
            TextBox8.Text = 0
            TextBox9.Text = 0
            TextBox10.Text = 0
            TextBox13.Text = 0

            ComboBox6.SelectedItem = ""
        Else
            IconButton3.PerformClick()
        End If
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        chqpanel.Visible = False
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        chqpanel.Visible = False
    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs)
        tpepanel.Visible = False

    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs)
        tpepanel.Visible = False

    End Sub

    Private Sub IconButton7_Click_1(sender As Object, e As EventArgs) Handles IconButton7.Click
        tpepanel.Visible = False
    End Sub

    Private Sub IconButton6_Click_1(sender As Object, e As EventArgs) Handles IconButton6.Click
        tpepanel.Visible = False
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        adpt = New MySqlDataAdapter("select * from banques where organisme = '" & ComboBox7.Text.ToString & "'", conn2)
        Dim tablebq As New DataTable
        adpt.Fill(tablebq)
        TextBox16.Text = tablebq.Rows(0).Item(2)
        TextBox15.Text = tablebq.Rows(0).Item(3)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then



            adpt = New MySqlDataAdapter("SELECT plu FROM `article` order by `plu` desc", conn2)
            Dim tablebq As New DataTable
            adpt.Fill(tablebq)
            For i = 0 To tablebq.Rows().Count() - 1

                TextBox1.Text = "260" & Convert.ToDouble(tablebq.Rows(0).Item(0) + 1).ToString.PadLeft(4, "0")
                TextBox18.Text = Convert.ToDouble(tablebq.Rows(0).Item(0) + 1).ToString.PadLeft(4, "0")
                TextBox2.Select()
                ComboBox3.Text = "KG"

            Next

            TextBox18.Enabled = True
            TextBox19.Enabled = True

        Else
            TextBox18.Text = ""
            TextBox19.Text = ""
            TextBox1.Text = ""
            TextBox18.Enabled = False
            TextBox19.Enabled = False
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub IconButton8_Click(sender As Object, e As EventArgs) Handles IconButton8.Click
        adpt2 = New MySqlDataAdapter("SELECT * FROM `articlenew`", conn2)
        Dim tableachat As New DataTable
        adpt2.Fill(tableachat)
        For i = 0 To tableachat.Rows.Count() - 1
            If IsDBNull(tableachat.Rows(i).Item(1)) Then
            Else

                'cmd2 = New MySqlCommand("UPDATE `achat` SET `ord` = '" & tableachat.Rows(i).Item(0).ToString.Chars(1) & tableachat.Rows(i).Item(0).ToString.Chars(2) & tableachat.Rows(i).Item(0).ToString.Chars(3) & tableachat.Rows(i).Item(0).ToString.Chars(4) & tableachat.Rows(i).Item(0).ToString.Chars(5) & "' WHERE `id` = '" & tableachat.Rows(i).Item(0).ToString & "' ", conn2)
                adpt = New MySqlDataAdapter("SELECT * FROM `article` where Code = '" & tableachat.Rows(i).Item(0) & "' and Article = '" & tableachat.Rows(i).Item(1).ToString.Replace("'", "") & "' ", conn2)
                Dim tableachat2 As New DataTable
                adpt.Fill(tableachat2)
                Dim sql2, sql3 As String
                Dim cmd2, cmd3 As MySqlCommand
                If tableachat2.Rows.Count() = 0 Then
                    conn2.Open()
                    cmd2 = New MySqlCommand("INSERT INTO `article`( `Code`, `Article`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `Stock`, `Securite_stock`, `unit` , `idFamille`, `idSFamille`,`fournisseur`,`pv_gros`,`plu`,`dlc`) VALUES (@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17)", conn2)
                    cmd2.Parameters.AddWithValue("@value1", tableachat.Rows(i).Item(0))
                    cmd2.Parameters.AddWithValue("@value2", tableachat.Rows(i).Item(1))
                    cmd2.Parameters.AddWithValue("@value3", tableachat.Rows(i).Item(2))
                    cmd2.Parameters.AddWithValue("@value4", tableachat.Rows(i).Item(3))
                    cmd2.Parameters.AddWithValue("@value5", tableachat.Rows(i).Item(4))
                    cmd2.Parameters.AddWithValue("@value6", tableachat.Rows(i).Item(5))
                    cmd2.Parameters.AddWithValue("@value7", tableachat.Rows(i).Item(6))
                    cmd2.Parameters.AddWithValue("@value8", tableachat.Rows(i).Item(7))
                    cmd2.Parameters.AddWithValue("@value9", tableachat.Rows(i).Item(8))
                    cmd2.Parameters.AddWithValue("@value10", tableachat.Rows(i).Item(9))
                    cmd2.Parameters.AddWithValue("@value11", tableachat.Rows(i).Item(10))
                    cmd2.Parameters.AddWithValue("@value12", tableachat.Rows(i).Item(11))
                    cmd2.Parameters.AddWithValue("@value13", tableachat.Rows(i).Item(12))
                    cmd2.Parameters.AddWithValue("@value15", tableachat.Rows(i).Item(13))
                    cmd2.Parameters.AddWithValue("@value14", tableachat.Rows(i).Item(14))
                    cmd2.Parameters.AddWithValue("@value16", tableachat.Rows(i).Item(15))
                    cmd2.Parameters.AddWithValue("@value17", tableachat.Rows(i).Item(16))
                    cmd2.ExecuteNonQuery()
                    conn2.Close()

                End If
            End If


            'sql2 = "delete from code_supp where code_supp = '" + tableachat.Rows(i).Item(0) + "' "
            'cmd2 = New MySqlCommand(sql2, conn2)

            'cmd2.ExecuteNonQuery()
            'cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` = '" & tableachat.Rows(i).Item(1) & "' WHERE `Code` = '" & tableachat.Rows(i).Item(0).ToString & "' ", conn2)



        Next
        MsgBox("termine")

        'sql2 = "INSERT INTO `achat`(`id`, `montant`, `Fournisseur`, `mtn_ht`) VALUES ('" & TextBox17.Text.ToString & "','" & Convert.ToDouble(tableachat.Rows(0).Item(0).ToString) & "','" & ComboBox4.Text.ToString & "','" & Convert.ToDouble(tableachat.Rows(0).Item(1).ToString) & "')"
        'cmd2 = New MySqlCommand(sql2, conn2)
        'cmd2.ExecuteNonQuery()
    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs)
        Dim id As String
        conn2.Open()
        Dim sql2, sql3 As String
        Dim cmd2, cmd3 As MySqlCommand
        sql2 = "delete from achat where id = '" & TextBox17.Text & "' "
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.ExecuteNonQuery()

        sql3 = "delete from achatdetails where achat_Id = '" & TextBox17.Text & "' "
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.ExecuteNonQuery()

        id = TextBox17.Text

        Dim s As String = ""
        Dim t As String = ""
        s = Convert.ToString(Label25.Text).Replace(".", ",")
        t = Convert.ToString(TextBox11.Text).Replace(".", ",")

        Dim str As String = Label40.Text
        sql2 = "INSERT INTO achat (id,montant,Fournisseur,dateAchat,typePay,paye,reste,mtn_ht,rms,ord) VALUES ('" & id & "', '" + Label25.Text + "', '" + ComboBox4.Text + "', '" & str & "', '" & ComboBox5.Text & "', '" & TextBox11.Text & "', '" + Convert.ToString(Convert.ToDouble(s) - Convert.ToDouble(t)) + "', '" + Label18.Text + "', '" + Label23.Text + "','" & num.ToString & "' )"
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.ExecuteNonQuery()

        For i = 0 To DataGridView3.Rows.Count - 1
            cmd2 = New MySqlCommand("INSERT INTO `achatdetails`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`pv_gros`,`rms`,`rt`,`gr`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value11,@value12,@value13,@value14)", conn2)
            cmd2.Parameters.AddWithValue("@value15", id)
            cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
            cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
            cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
            cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
            cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
            cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
            cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(14).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
            cmd2.Parameters.AddWithValue("@value11", Convert.ToDouble(DataGridView3.Rows(i).Cells(15).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
            cmd2.Parameters.AddWithValue("@value12", Convert.ToDouble(DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
            cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(18).Value)
            cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(19).Value)
            cmd2.ExecuteNonQuery()
            adpt2 = New MySqlDataAdapter("SELECT * FROM `article` WHERE `Code`= '" + DataGridView3.Rows(i).Cells(1).Value + "' ", conn2)
            Dim table0 = New DataTable
            adpt2.Fill(table0)

            If table0.Rows.Count > 0 Then
                cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  `Stock` + '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",")) & "',`PV_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(".", ",")) & "',`PA_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(".", ",")) & "', `PV_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",")) & "',`PA_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")) & "' WHERE `Code` = '" + DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") + "' ", conn2)
                cmd2.ExecuteNonQuery()
            Else
                cmd2 = New MySqlCommand("INSERT INTO `article`( `Code`, `Article`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `Stock`, `Securite_stock`, `unit` , `idFamille`, `idSFamille`,`fournisseur`,`pv_gros`,`plu`,`dlc`) VALUES (@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17)", conn2)
                cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value6", DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value9", DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value10", DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value11", DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value12", DataGridView3.Rows(i).Cells(12).Value.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(13).Value.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value15", DataGridView3.Rows(i).Cells(15).Value.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value14", ComboBox4.Text.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value16", DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value17", DataGridView3.Rows(i).Cells(21).Value.ToString.Replace(".", ","))
                cmd2.ExecuteNonQuery()
            End If

            sql2 = "INSERT INTO code_supp (prod_code,code_supp) VALUES ('" + DataGridView3.Rows(i).Cells(1).Value + "', '" + DataGridView3.Rows(i).Cells(20).Value + "')"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()
        Next
        If ComboBox5.Text = "Chèque" Then
            sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `achat`,`bq`,`montant`) 
                    VALUES ('" + ComboBox7.Text + "','" + TextBox16.Text + "','" + TextBox15.Text + "','" + TextBox14.Text + "','" + DateTimePicker1.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" & id & "','" + ComboBox7.Text + "','" + Label25.Text + "')"
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            cmd3.ExecuteNonQuery()
        End If
        If ComboBox5.Text = "TPE" Then
            sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `achat`,`bq`,`montant`) 
                    VALUES ('tpe','" + TextBox20.Text + "','tpe','" + TextBox21.Text + "','tpe', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" & id & "','" + ComboBox8.Text + "','" + Label25.Text + "')"
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            cmd3.ExecuteNonQuery()
        End If
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
        MsgBox("Modification bien effectuée")
        Me.Close()


        conn2.Close()
    End Sub

    Private Sub IconButton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        If Panel6.Visible = False Then
            Panel6.Visible = True
            TextBox2.Select()
        Else
            Panel6.Visible = False
        End If

    End Sub

    Private Sub TextBox22_TextChanged(sender As Object, e As EventArgs) Handles TextBox22.TextChanged
        If TextBox22.Text = "" Then
            adpt = New MySqlDataAdapter("select client from clients order by client asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                ComboBox4.Items.Clear()
                ComboBox4.Items.Add("aucun")
                For i = 0 To table.Rows.Count - 1
                    ComboBox4.Items.Add(table.Rows(i).Item(0))
                Next
            End If
            ComboBox4.Text = Label31.Text
        Else
            ComboBox4.Items.Clear()
            adpt = New MySqlDataAdapter("select client from clients WHERE client LIKE '%" + TextBox22.Text.Replace("'", " ") + "%' order by client asc", conn2)
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




    Private Sub IconButton16_Click(sender As Object, e As EventArgs)
        Dim table0 As DataTable
        adpt2 = New MySqlDataAdapter("SELECT * FROM `article` WHERE `Code`= '" + Label49.Text + "' ", conn2)
        table0 = New DataTable
        adpt2.Fill(table0)

        If table0.Rows.Count <> 0 Then
            Try
                conn2.Open()

                cmd2 = New MySqlCommand("UPDATE `article` SET `Code` = @value1, `Article` = @value2, `PA_HT` = @value3, `TVA` = @value4, `PA_TTC` = @value5, `TM` = @value6, `PV_HT` = @value7, `PV_TTC` = @value8, `Stock` = @value9, `Securite_stock` = @value10, `unit` = @value11, `idFamille` = @value12, `idSFamille` = @value13, `fournisseur` = @value14, `pv_gros` = @value15, `plu` = @value16, `dlc` = @value17 WHERE `Code` = @value18", conn2)
                cmd2.Parameters.AddWithValue("@value2", TextBox2.Text.ToString.Replace("'", " "))
                cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(TextBox3.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value4", Convert.ToDouble(ComboBox6.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(TextBox7.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(TextBox8.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(TextBox6.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(TextBox5.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(TextBox10.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(TextBox9.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value11", ComboBox3.Text)

                cmd2.Parameters.AddWithValue("@value14", ComboBox4.Text)
                cmd2.Parameters.AddWithValue("@value15", Convert.ToDouble(TextBox13.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value16", Convert.ToDouble(TextBox18.Text.ToString.Replace(".", ",")))
                cmd2.Parameters.AddWithValue("@value17", Convert.ToDouble(TextBox19.Text.ToString.Replace(".", ",")))
                cmd2.Parameters.AddWithValue("@value18", Label49.Text)
                cmd2.Parameters.AddWithValue("@value1", TextBox1.Text.ToString.Replace(".", ","))
                cmd2.ExecuteNonQuery()

                conn2.Close()

                MsgBox("Article modifié !")
            Catch
                MsgBox("Veuillez remplir les champs !")
            End Try

        Else
            MsgBox("Cette article est non disponible !")
        End If
    End Sub

    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        codbar.Show()
        codbar.PicBarCode.BackgroundImage = Code128(TextBox1.Text, "A")
        codbar.TextBox1.Text = TextBox2.Text
        codbar.TextBox3.Text = TextBox1.Text
    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub


    Private Sub DataGridView3_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellEndEdit
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
            row.Cells(7).Value = (row.Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",") / (1 + (row.Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))) * (1 + (row.Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
            row.Cells(14).Value = (Convert.ToDouble(((row.Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - (row.Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") * (row.Cells(16).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))) * (row.Cells(9).Value.ToString.Replace(" ", "").Replace(".", ",") - row.Cells(18).Value.ToString.Replace(" ", "").Replace(".", ","))) + (((row.Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - (row.Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") * (row.Cells(16).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))) * (row.Cells(9).Value.ToString.Replace(" ", "").Replace(".", ",") - row.Cells(18).Value.ToString.Replace(" ", "").Replace(".", ","))) * (row.Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))).ToString("# ##0.00")) * (1 + (row.Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))

            Dim sum As Double = 0
            Dim ht As Double = 0
            Dim remise As Double = 0
            For i = 0 To DataGridView3.Rows.Count - 1
                sum = sum + DataGridView3.Rows(i).Cells(14).Value.ToString.Replace(" ", "").Replace(".", ",")
                remise = remise + (DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") * DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "").Replace(".", ",") * (DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                ht = ht + ((DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") * DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "").Replace(".", ",")) * (1 - (DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(" ", "").Replace(".", ",") / 100)) * (1 + (row.Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") / 100)))
            Next

            Label25.Text = sum.ToString("# ##0.00")
            Label23.Text = remise.ToString("# ##0.00")
            Label18.Text = ht.ToString("# ##0.00")
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown


        If e.KeyCode = Keys.Enter Then


            adpt = New MySqlDataAdapter("select prod_code from code_supp where code_supp = '" & TextBox1.Text & "' ", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count() = 0 Then
            adpt = New MySqlDataAdapter("select * from article where Code = '" & TextBox1.Text & "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count() <> 0 Then
                    For i = 0 To table.Rows.Count - 1
                        Label49.Text = table.Rows(i).Item(0)
                        TextBox1.Text = table.Rows(i).Item(0)
                        TextBox2.Text = table.Rows(i).Item(1)
                        TextBox3.Text = table.Rows(i).Item(2).ToString.Replace(".", ",")
                        Dim index As Integer = ComboBox6.FindStringExact(table.Rows(i).Item(3).ToString.Replace(",00", ""))
                        If index <> -1 Then
                            ComboBox6.SelectedIndex = index
                        End If
                        TextBox5.Text = table.Rows(i).Item(7).ToString.Replace(".", ",")
                        TextBox6.Text = table.Rows(i).Item(6).ToString.Replace(".", ",")
                        TextBox7.Text = table.Rows(i).Item(4).ToString.Replace(".", ",")
                        TextBox8.Text = table.Rows(i).Item(5).ToString.Replace(".", ",")
                        TextBox9.Text = table.Rows(i).Item(9)
                        TextBox10.Text = table.Rows(i).Item(8)
                        TextBox18.Text = table.Rows(i).Item(18)
                        TextBox19.Text = table.Rows(i).Item(19)

                        ComboBox3.SelectedItem = table.Rows(i).Item(13)
                        ComboBox4.SelectedItem = table.Rows(i).Item(10)

                    Next
                Else
                    TextBox2.Text = ""
                    TextBox3.Text = 0
                    TextBox5.Text = 0
                    TextBox6.Text = 0
                    TextBox7.Text = 0
                    TextBox8.Text = 0
                    TextBox9.Text = 0
                    TextBox10.Text = 0
                    TextBox13.Text = 0
                    TextBox18.Text = ""
                    TextBox19.Text = ""

                    ComboBox4.SelectedItem = ""
                    TextBox2.Select()


                End If
            Else
                adpt = New MySqlDataAdapter("select * from article where Code = '" + table2.Rows(0).Item(0) + "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    For i = 0 To table.Rows.Count - 1

                        TextBox1.Text = table.Rows(i).Item(0)
                        TextBox2.Text = table.Rows(i).Item(1)
                        TextBox3.Text = table.Rows(i).Item(2).ToString.Replace(".", ",")
                        ComboBox6.Text = table.Rows(i).Item(3).ToString.Replace(",00", "")
                        TextBox5.Text = table.Rows(i).Item(7).ToString.Replace(".", ",")
                        TextBox6.Text = table.Rows(i).Item(6).ToString.Replace(".", ",")
                        TextBox7.Text = table.Rows(i).Item(4).ToString.Replace(".", ",")
                        TextBox8.Text = table.Rows(i).Item(5).ToString.Replace(".", ",")
                        TextBox9.Text = table.Rows(i).Item(9)
                    TextBox10.Text = table.Rows(i).Item(8)
                    TextBox18.Text = table.Rows(i).Item(18)
                    TextBox19.Text = table.Rows(i).Item(19)

                        ComboBox3.SelectedItem = table.Rows(i).Item(13)
                    ComboBox4.SelectedItem = table.Rows(i).Item(10)

                    charg = True
                Next
            Else
                TextBox2.Text = ""
                TextBox3.Text = 0
                TextBox5.Text = 0
                TextBox6.Text = 0
                TextBox7.Text = 0
                TextBox8.Text = 0
                TextBox9.Text = 0
                TextBox10.Text = 0
                TextBox13.Text = 0
                TextBox18.Text = ""
                TextBox19.Text = ""

                    ComboBox6.SelectedItem = ""
                TextBox2.Select()


                charg = True
            End If

        End If

        End If
    End Sub

    Private Sub IconButton9_Click_1(sender As Object, e As EventArgs) Handles IconButton9.Click
        conn2.Open()
        If ComboBox4.Text = "" Then
            MsgBox("Veuillez choisir le client !")

        Else


            Dim id As String
            id = TextBox17.Text

            Dim montant As Double = 0
            Dim paye As Double = 0
            Dim reste As Double = 0
            montant = Convert.ToString(Label25.Text).Replace(".", ",")
            paye = Convert.ToString(TextBox11.Text).Replace(".", ",")
            reste = montant - paye

            sql2 = "UPDATE achat SET montant = '" & Label25.Text & "', Fournisseur = '" & ComboBox4.Text & "', paye = '" & paye & "', reste = '" & reste & "', mtn_ht = '" & Label18.Text & "', rms = '" & Label23.Text & "' WHERE id = '" & id & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()

            adpt = New MySqlDataAdapter("select * from achatdetails where achat_Id = '" & id & "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            For i = 0 To table.Rows.Count - 1
                cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  `Stock` - '" & Convert.ToDouble(table.Rows(i).Item(9).ToString.Replace(".", ",")) & "' WHERE `Code` = '" + table.Rows(i).Item(1).ToString.Replace(".", ",") + "' ", conn2)
                cmd2.ExecuteNonQuery()
            Next

            cmd2 = New MySqlCommand("DELETE FROM `achatdetails` where `achat_Id` = '" & id & "' ", conn2)
            cmd2.ExecuteNonQuery()

            For i = 0 To DataGridView3.Rows.Count - 1
                cmd2 = New MySqlCommand("INSERT INTO `achatdetails`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`rms`,`rt`,`gr`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value12,@value13,@value14)", conn2)
                cmd2.Parameters.AddWithValue("@value15", id)
                cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(4).Value)
                cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(14).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value12", Convert.ToDouble(DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(18).Value)
                cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(19).Value)
                cmd2.ExecuteNonQuery()

                cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  `Stock` + '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",")) & "',`PV_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(".", ",")) & "',`PA_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(".", ",")) & "', `PV_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",")) & "',`PA_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")) & "' WHERE `Code` = '" + DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") + "' ", conn2)
                cmd2.ExecuteNonQuery()

            Next
            MsgBox("Modification bien effectuée")
        End If
        conn2.Close()
        Me.Close()


    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click
        Panel6.Visible = True
    End Sub

    Private Sub IconButton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
        Panel6.Visible = False
        DataGridView1.Rows.Clear()
    End Sub

    Private selectedRowIndex As Integer = -1 ' Variable pour stocker l'index de la ligne sélectionnée

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        adpt = New MySqlDataAdapter("select Code, Article, Stock from article where fournisseur = '" & ComboBox4.Text & "' ", conn2)
        Dim tablelike As New DataTable
        adpt.Fill(tablelike)
        DataGridView1.Rows.Clear()

        If tablelike.Rows.Count() <> 0 Then

            Dim rowsToAdd As New List(Of DataGridViewRow)
            Dim row As New DataGridViewRow()
            For i = 0 To tablelike.Rows.Count - 1
                ' Créer les instances de cellules
                Dim first As New DataGridViewTextBoxCell()
                Dim second As New DataGridViewTextBoxCell()
                Dim tree As New DataGridViewTextBoxCell()
                Dim qte As New DataGridViewTextBoxCell()


                ' Affecter les valeurs des cellules
                first.Value = tablelike.Rows(i).Item(0)
                second.Value = tablelike.Rows(i).Item(1)
                tree.Value = tablelike.Rows(i).Item(2)
                qte.Value = ""


                ' Ajouter les cellules à la ligne
                row.Cells.Add(first)
                row.Cells.Add(second)
                row.Cells.Add(qte)
                row.Cells.Add(tree)


            Next
            rowsToAdd.Add(row)

            DataGridView1.Rows.Clear()
            DataGridView1.Rows.AddRange(rowsToAdd.ToArray())

        End If
    End Sub

    Private Sub TextBox24_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox24.KeyDown

        If e.KeyCode = Keys.Enter Then
            If TextBox24.Text <> "" Then

                adpt = New MySqlDataAdapter("select prod_code from code_supp where code_supp = '" & TextBox24.Text & "' ", conn2)
                Dim table2 As New DataTable
                adpt.Fill(table2)
                If table2.Rows.Count() = 0 Then
                    adpt = New MySqlDataAdapter("select Code, Article, Stock from article where Code = '" & TextBox24.Text & "' ", conn2)
                    Dim tablelike As New DataTable
                    adpt.Fill(tablelike)
                    If tablelike.Rows.Count() <> 0 Then

                        Dim rowsToAdd As New List(Of DataGridViewRow)
                        Dim row As New DataGridViewRow()

                        ' Créer les instances de cellules
                        Dim first As New DataGridViewTextBoxCell()
                        Dim second As New DataGridViewTextBoxCell()
                        Dim tree As New DataGridViewTextBoxCell()
                        Dim qte As New DataGridViewTextBoxCell()


                        ' Affecter les valeurs des cellules
                        first.Value = tablelike.Rows(0).Item(0)
                        second.Value = tablelike.Rows(0).Item(1)
                        tree.Value = tablelike.Rows(0).Item(2)
                        qte.Value = ""


                        ' Ajouter les cellules à la ligne
                        row.Cells.Add(first)
                        row.Cells.Add(second)
                        row.Cells.Add(qte)
                        row.Cells.Add(tree)


                        rowsToAdd.Add(row)
                        DataGridView1.Rows.Clear()
                        DataGridView1.Rows.AddRange(rowsToAdd.ToArray())

                    End If
                Else
                    adpt = New MySqlDataAdapter("select Code, Article, Stock from article where Code = '" + table2.Rows(0).Item(0) + "' ", conn2)
                    Dim tablelike As New DataTable
                    adpt.Fill(tablelike)
                    If tablelike.Rows.Count() <> 0 Then

                        Dim rowsToAdd As New List(Of DataGridViewRow)
                        For i = 0 To tablelike.Rows.Count - 1
                            Dim row As New DataGridViewRow()

                            Dim first As New DataGridViewTextBoxCell()
                            Dim second As New DataGridViewTextBoxCell()
                            Dim tree As New DataGridViewTextBoxCell()
                            Dim qte As New DataGridViewTextBoxCell()


                            ' Affecter les valeurs des cellules
                            first.Value = tablelike.Rows(0).Item(0)
                            second.Value = tablelike.Rows(0).Item(1)
                            tree.Value = tablelike.Rows(0).Item(2)
                            qte.Value = ""


                            ' Ajouter les cellules à la ligne
                            row.Cells.Add(first)
                            row.Cells.Add(second)
                            row.Cells.Add(qte)
                            row.Cells.Add(tree)


                            rowsToAdd.Add(row)
                        Next
                        DataGridView1.Rows.Clear()
                        DataGridView1.Rows.AddRange(rowsToAdd.ToArray())


                    End If

                End If




            End If
            TextBox24.Text = ""
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ' Vérifier si un clic a été effectué sur une cellule valide (non sur l'en-tête)
        If e.RowIndex >= 0 Then
            If e.ColumnIndex <> DataGridView1.Columns("Qte").Index Then
                ' Sélectionner la colonne "qte"
                DataGridView1.CurrentCell = DataGridView1.Rows(e.RowIndex).Cells("Qte")
            End If

        End If
    End Sub

    Private Sub TextBox23_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox23.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox23.Text <> "" Then

                adpt = New MySqlDataAdapter("select Code, Article, Stock from article where Article like '%" & TextBox23.Text & "%' order by Article asc", conn2)
                Dim tablelike As New DataTable
                adpt.Fill(tablelike)

                Dim rowsToAdd As New List(Of DataGridViewRow)
                For i = 0 To tablelike.Rows.Count - 1
                    Dim row As New DataGridViewRow()

                    Dim first As New DataGridViewTextBoxCell()
                    Dim second As New DataGridViewTextBoxCell()
                    Dim tree As New DataGridViewTextBoxCell()
                    Dim qte As New DataGridViewTextBoxCell()


                    ' Affecter les valeurs des cellules
                    first.Value = tablelike.Rows(i).Item(0)
                    second.Value = tablelike.Rows(i).Item(1)
                    tree.Value = tablelike.Rows(i).Item(2)
                    qte.Value = ""


                    ' Ajouter les cellules à la ligne
                    row.Cells.Add(first)
                    row.Cells.Add(second)
                    row.Cells.Add(qte)
                    row.Cells.Add(tree)


                    rowsToAdd.Add(row)
                Next
                DataGridView1.Rows.Clear()
                DataGridView1.Rows.AddRange(rowsToAdd.ToArray())

            End If
            TextBox23.Text = ""
        End If
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        ' Vérifier si un clic a été effectué sur une cellule valide (non sur l'en-tête)
        If e.RowIndex >= 0 Then
            ' Récupérer les données de l'article sélectionné dans la DataGridView1
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim code As String = selectedRow.Cells("Code").Value.ToString()
            ' Vérifier si l'article est déjà présent dans la DataGridView3
            Dim articleDejaPresent As Boolean = False
            For Each rowe As DataGridViewRow In DataGridView3.Rows
                Dim codeArticleDansGrid As String = rowe.Cells("Code2").Value.ToString()
                If codeArticleDansGrid = code Then
                    articleDejaPresent = True
                    DataGridView3.ClearSelection()
                    rowe.Selected = True ' Sélectionner la ligne existante
                    Exit For
                End If
            Next

            If Not articleDejaPresent Then
                adpt = New MySqlDataAdapter("select `Code`, `Article`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`,`unit` from article where Code = '" & code & "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    If selectedRow.Cells("Qte").Value <> "" AndAlso selectedRow.Cells("Qte").Value > 0 Then

                        Dim qty As Double = selectedRow.Cells("Qte").Value.ToString.Replace(".", ",")
                        Dim patotal As Double = Convert.ToDouble(Convert.ToDouble(selectedRow.Cells("Qte").Value.ToString.Replace(".", ",")) * table.Rows(0).Item(7)).ToString.Replace(".", ",")

                        DataGridView3.Rows.Add(Nothing, table.Rows(0).Item(0), table.Rows(0).Item(1).ToString.Replace("'", ""), Convert.ToDouble(table.Rows(0).Item(2)).ToString.Replace(".", ","), Convert.ToDouble(table.Rows(0).Item(3)).ToString.Replace(".", ","), Convert.ToDouble(table.Rows(0).Item(4)).ToString.Replace(".", ","), Convert.ToDouble(table.Rows(0).Item(5)).ToString.Replace(".", ","), Convert.ToDouble(table.Rows(0).Item(6)).ToString.Replace(".", ","), Convert.ToDouble(table.Rows(0).Item(7)).ToString.Replace(".", ","), Convert.ToDouble(qty).ToString("#0.00"), Nothing, table.Rows(0).Item(8), Nothing, Nothing, Convert.ToDouble(patotal).ToString("#0.00"), Nothing, "0,00", Convert.ToDouble(table.Rows(0).Item(2)).ToString.Replace(".", ","), "0,00", "0,00")
                        Dim sum As Double = 0
                        Dim ht As Double = 0
                        Dim remise As Double = 0
                        For i = 0 To DataGridView3.Rows.Count - 1
                            sum = sum + DataGridView3.Rows(i).Cells(14).Value.ToString.Replace(".", ",")
                            remise = remise + (DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",") * DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",") * (DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ",") / 100))
                            ht = ht + (DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",") * DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",")) * (1 - (DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ",") / 100))
                        Next

                        Label25.Text = sum.ToString("# ##0.00")
                        Label23.Text = remise.ToString("# ##0.00")
                        Label18.Text = ht.ToString("# ##0.00")
                        TextBox11.Text = 0

                    End If
                End If
            End If
        End If
    End Sub
End Class