Imports System.IO
Imports System.Text
Imports MySql.Data.MySqlClient
Public Class simpleajout
    'Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    'Dim conn2 As New MySqlConnection("datasource=localhost; username=root; password=; database=librairie_graph")
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Dim imgName As String = "default.png"

    Dim N As Integer = 0
    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Label25.Text = 0
        Label23.Text = 0
        Label18.Text = 0
        TextBox11.Text = 0
        CheckBox1.Checked = False
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
        ComboBox1.SelectedItem = ""
        ComboBox2.SelectedItem = ""
        ComboBox6.SelectedItem = ""
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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        adpt = New MySqlDataAdapter("select * from article where Code = '" + TextBox1.Text + "'", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        If table.Rows.Count <> 0 Then
            For i = 0 To table.Rows.Count - 1
                TextBox2.Text = table.Rows(i).Item(1)
                TextBox3.Text = table.Rows(i).Item(2)
                ComboBox6.Text = table.Rows(i).Item(3)
                TextBox5.Text = table.Rows(i).Item(7)
                TextBox6.Text = table.Rows(i).Item(6)
                TextBox7.Text = table.Rows(i).Item(4)
                TextBox8.Text = table.Rows(i).Item(5)
                TextBox9.Text = table.Rows(i).Item(9)
                TextBox10.Text = table.Rows(i).Item(8)
                ComboBox1.SelectedItem = table.Rows(i).Item(11)
                ComboBox2.SelectedItem = table.Rows(i).Item(12)
                ComboBox3.SelectedItem = table.Rows(i).Item(13)
                ComboBox4.SelectedItem = table.Rows(i).Item(10)

                charg = True
            Next
        Else
            TextBox2.Text = ""
            TextBox3.Text = 0
            ComboBox6.Text = ""
            TextBox5.Text = 0
            TextBox6.Text = 0
            TextBox7.Text = 0
            TextBox8.Text = 0
            TextBox9.Text = 0
            TextBox10.Text = 0
            ComboBox1.SelectedItem = ""
            ComboBox2.SelectedItem = ""
            TextBox2.Select()


            charg = True
        End If
        If charg = True Then
            Timer1.Stop()
        End If
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs)

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

    Private Sub IconButton1_Click_1(sender As Object, e As EventArgs) Handles IconButton1.Click
        If TextBox17.Text = "" Then
            MsgBox("Veuillez saisir le N° du Bon d'achat !")

        Else
            adpt2 = New MySqlDataAdapter("select * from achat where id = '" + TextBox17.Text + "'", conn2)
            Dim tableachat As New DataTable
            adpt2.Fill(tableachat)
            If tableachat.Rows.Count() = 0 Then
                conn2.Open()
                Dim id As Integer
                adpt2 = New MySqlDataAdapter("select `id` from achat order by id DESC", conn2)
                Dim table0 As New DataTable
                adpt2.Fill(table0)
                id = TextBox17.Text

                Dim s As String = ""
                Dim t As String = ""
                s = Convert.ToString(Label25.Text).Replace(".", ",")
                t = Convert.ToString(TextBox11.Text).Replace(".", ",")

                sql2 = "INSERT INTO achat (id,montant,Fournisseur,dateAchat,typePay,paye,reste,mtn_ht,rms) VALUES ('" & id & "', '" + Label25.Text + "', '" + ComboBox4.Text + "', '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "', '" & ComboBox5.Text & "', '" & TextBox11.Text & "', " + Convert.ToString(Convert.ToDouble(s) - Convert.ToDouble(t)) + ", '" + Label18.Text + "', '" + Label23.Text + "' )"
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.ExecuteNonQuery()

                For i = 0 To DataGridView3.Rows.Count - 1
                    cmd2 = New MySqlCommand("INSERT INTO `achatdetails`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`pv_gros`,`rms`,`rt`,`gr`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value11,@value12,@value13,@value14)", conn2)
                    cmd2.Parameters.AddWithValue("@value15", id)
                    cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                    cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                    cmd2.Parameters.AddWithValue("@value3", Math.Round(Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")), 2))
                    cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(4).Value)
                    cmd2.Parameters.AddWithValue("@value5", Math.Round(Convert.ToDouble(DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(".", ",")), 2))
                    cmd2.Parameters.AddWithValue("@value6", DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ","))
                    cmd2.Parameters.AddWithValue("@value7", Math.Round(Convert.ToDouble(DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(".", ",")), 2))
                    cmd2.Parameters.AddWithValue("@value8", Math.Round(Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",")), 2))
                    cmd2.Parameters.AddWithValue("@value9", DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ","))
                    cmd2.Parameters.AddWithValue("@value10", DataGridView3.Rows(i).Cells(14).Value.ToString.Replace(".", ","))
                    cmd2.Parameters.AddWithValue("@value11", DataGridView3.Rows(i).Cells(15).Value.ToString.Replace(".", ","))
                    cmd2.Parameters.AddWithValue("@value12", DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ","))
                    cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(18).Value)
                    cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(19).Value)
                    cmd2.ExecuteNonQuery()
                    adpt2 = New MySqlDataAdapter("SELECT * FROM `article` WHERE `Code`= '" + DataGridView3.Rows(i).Cells(1).Value + "' ", conn2)
                    table0 = New DataTable
                    adpt2.Fill(table0)

                    If table0.Rows.Count > 0 Then
                        cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  `Stock` + '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",")) & "',`PV_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(7).Value) & "',`PA_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(5).Value) & "', `PV_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value) & "',`PA_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value) & "' WHERE `Code` = '" + DataGridView3.Rows(i).Cells(1).Value + "' ", conn2)
                        cmd2.ExecuteNonQuery()
                    Else
                        cmd2 = New MySqlCommand("INSERT INTO `article`( `Code`, `Article`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `Stock`, `Securite_stock`, `unit` , `idFamille`, `idSFamille`,`fournisseur`,`pv_gros`,`plu`,`dlc`) VALUES (@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17)", conn2)
                        cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value3", Math.Round(Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")), 2))
                        cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value5", Math.Round(Convert.ToDouble(DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(".", ",")), 2))
                        cmd2.Parameters.AddWithValue("@value6", DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value7", Math.Round(Convert.ToDouble(DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(".", ",")), 2))
                        cmd2.Parameters.AddWithValue("@value8", Math.Round(Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",")), 2))
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

                    'sql3 = "INSERT INTO dat_articulo (IdArticulo,PrecioConIVA,Descripcion,DiasCaducidad,EANScanner) VALUES ('" & DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(".", ",") & "', '" + DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",") + "', '" + DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(".", ",") + "', '" + DataGridView3.Rows(i).Cells(21).Value.ToString.Replace(".", ",") + "','260" & DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(".", ",") & "EEEEE' )"
                    'cmd3 = New MySqlCommand(sql3, conn3)
                    'cmd3.ExecuteNonQuery()

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

                sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.Parameters.AddWithValue("@name", achats.Label2.Text)
                cmd3.Parameters.AddWithValue("@op", "Reception d'achat N° " & TextBox17.Text)
                cmd3.ExecuteNonQuery()


                adpt = New MySqlDataAdapter("select path from infos", conn2)
                Dim tableinfo As New DataTable
                adpt.Fill(tableinfo)

                Dim path As String = tableinfo.Rows(0).Item(0).ToString.Replace("/", "\") & "\orden.dat"

                ' Create or overwrite the file.
                Dim fs As FileStream = File.Create(path)
                adpt = New MySqlDataAdapter("select * from article where plu IS NOT NULL order by id", conn2)
                Dim table As New DataTable
                adpt.Fill(table)

                ' Add text to the file.
                For i = 0 To table.Rows.Count - 1
                    Dim info As Byte() = New UTF8Encoding(True).GetBytes("260" & table.Rows(i).Item(18) & "EEEEE|" & 1 & " |" & table.Rows(i).Item(18) & "|" & "   |" & "    |" & Convert.ToString(table.Rows(i).Item(7)).Replace(",", "").Replace(".", "") & "|" & "W|" & table.Rows(i).Item(19) & "|" & 0 & "|" & table.Rows(i).Item(1) & vbTab & "|     |" & vbCrLf)
                    fs.Write(info, 0, info.Length)
                Next
                fs.Close()
                MsgBox("Achat bien effectuée")

                conn2.Close()
            Else
                MsgBox("Bon déjà saisie !")
            End If

        End If

    End Sub

    Private Sub simpleajout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox9.Text = 0
        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Panel1.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel3.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel4.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))

        adpt = New MySqlDataAdapter("select * from famille", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        For i = 0 To table.Rows.Count - 1

            ComboBox10.Items.Add(table.Rows(i).Item(1))
        Next

        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim tablebq As New DataTable
        adpt.Fill(tablebq)

        For i = 0 To tablebq.Rows.Count - 1
            ComboBox7.Items.Add(tablebq.Rows(i).Item(1))
            ComboBox8.Items.Add(tablebq.Rows(i).Item(1))
        Next

        adpt3 = New MySqlDataAdapter("select * from fours", conn2)
        Dim table3 As New DataTable
        adpt3.Fill(table3)

        For i = 0 To table3.Rows.Count - 1
            ComboBox4.Items.Add(table3.Rows(i).Item(1))
        Next
    End Sub

    Private Sub TextBox4_LostFocus(sender As Object, e As EventArgs) Handles TextBox4.LostFocus
        If ComboBox6.Text = "" Then
            TextBox7.Text = TextBox3.Text + (TextBox3.Text * 0 / 100)
        Else
            TextBox7.Text = TextBox3.Text + (TextBox3.Text * ComboBox6.Text / 100)
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox6.Text = "" Or TextBox8.Text = "" Or TextBox7.Text = "" Or TextBox6.Text = "" Or TextBox5.Text = "" Or TextBox10.Text = "" Or TextBox9.Text = "" Or TextBox13.Text = "" Then
            MsgBox("Remplis tous")
        Else
            adpt = New MySqlDataAdapter("select * from famille where nomFamille = '" + ComboBox1.Text.ToString + "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)

            adpt2 = New MySqlDataAdapter("select * from sous_famille where nomSFamille = '" + ComboBox2.Text.ToString + "'", conn2)
            Dim table2 As New DataTable
            adpt2.Fill(table2)
            DataGridView3.Rows.Add(Nothing, TextBox1.Text, TextBox2.Text, TextBox3.Text, ComboBox6.Text, TextBox7.Text, TextBox8.Text, TextBox6.Text, TextBox5.Text, TextBox10.Text, TextBox9.Text, ComboBox3.Text, ComboBox1.Text, ComboBox2.Text, Convert.ToDouble(TextBox7.Text) * Convert.ToDouble(TextBox10.Text), TextBox13.Text, 0, TextBox3.Text, 0, 0, TextBox18.Text, TextBox19.Text)
            Dim sum As Double = 0
            Dim ht As Double = 0
            Dim remise As Double = 0
            For i = 0 To DataGridView3.Rows.Count - 1
                sum = sum + DataGridView3.Rows(i).Cells(14).Value
                remise = remise + (DataGridView3.Rows(i).Cells(3).Value * DataGridView3.Rows(i).Cells(9).Value * (DataGridView3.Rows(i).Cells(16).Value / 100))
                ht = ht + (sum / (1 + DataGridView3.Rows(i).Cells(4).Value / 100) + remise)
            Next

            Label25.Text = sum.ToString("# ##0.00")
            Label23.Text = remise.ToString("# ##0.00")
            Label18.Text = ht.ToString("# ##0.00")
            TextBox11.Text = sum
            CheckBox1.Checked = False
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
            ComboBox1.SelectedItem = ""
            ComboBox2.SelectedItem = ""
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
            TextBox7.Text = TextBox3.Text
        Else
            TextBox7.Text = TextBox3.Text + (TextBox3.Text * ComboBox6.Text / 100)
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
                        sum = sum + DataGridView3.Rows(i).Cells(14).Value
                        remise = remise + (DataGridView3.Rows(i).Cells(3).Value * DataGridView3.Rows(i).Cells(9).Value * (DataGridView3.Rows(i).Cells(16).Value / 100))
                        ht = ht + (sum / (1 + DataGridView3.Rows(i).Cells(4).Value / 100) + remise)
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

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

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
            ComboBox1.SelectedItem = ""
            ComboBox2.SelectedItem = ""
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
            adpt = New MySqlDataAdapter("select plu from article where plu IS NOT NULL order by id desc", conn2)
            Dim tablebq As New DataTable
            adpt.Fill(tablebq)
            TextBox18.Text = Convert.ToDouble(tablebq.Rows(0).Item(0) + 1)
            TextBox1.Text = "260" & Convert.ToDouble(tablebq.Rows(0).Item(0) + 1)
            TextBox2.Select()
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
        If TextBox1.Text = "" And TextBox23.Text = "" Then
            MsgBox("Veuillez remplir tous les champs !")
        Else
            adpt2 = New MySqlDataAdapter("SELECT * FROM `article` WHERE `Code`= '" + TextBox1.Text + "' ", conn2)
            Dim table0 As DataTable
            table0 = New DataTable
            adpt2.Fill(table0)

            If table0.Rows.Count() <> 0 Then
                MsgBox("Article déjà disponible")
            Else
                If TextBox24.Text = "" Then
                    TextBox24.Text = 0
                End If
                If TextBox22.Text = "" Then
                    TextBox22.Text = 0
                End If
                If ComboBox9.Text = "" Then
                    If TextBox24.Text = "" Then
                        TextBox24.Text = 0
                    End If
                    If TextBox22.Text = "" Then
                        TextBox22.Text = 0
                    End If
                    TextBox27.Text = Convert.ToDouble(TextBox24.Text.ToString.Replace(".", ","))
                    TextBox26.Text = Convert.ToDouble(TextBox22.Text.ToString.Replace(".", ","))
                    TextBox28.Text = Convert.ToDouble(((TextBox26.Text.Replace(".", ",") - TextBox27.Text.Replace(".", ",")) / TextBox27.Text.Replace(".", ",")) * 100)

                Else
                    If TextBox24.Text = "" Then
                        TextBox24.Text = 0
                    End If
                    If TextBox22.Text = "" Then
                        TextBox22.Text = 0
                    End If
                    TextBox27.Text = Convert.ToDouble(TextBox24.Text.ToString.Replace(".", ",")) / (1 + (ComboBox9.Text / 100))
                    TextBox26.Text = Convert.ToDouble(TextBox22.Text.ToString.Replace(".", ",")) / (1 + (ComboBox9.Text / 100))
                    TextBox28.Text = Convert.ToDouble(((TextBox26.Text.Replace(".", ",") - TextBox27.Text.Replace(".", ",")) / TextBox27.Text.Replace(".", ",")) * 100)

                End If
                conn2.Open()
                cmd2 = New MySqlCommand("INSERT INTO `article`( `Code`, `Article`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `Stock`, `Securite_stock`, `unit` , `idFamille`, `idSFamille`,`fournisseur`,`pv_gros`,`plu`,`dlc`,`img`) VALUES (@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@img)", conn2)
                cmd2.Parameters.AddWithValue("@value1", TextBox1.Text.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value2", TextBox23.Text.ToString.Replace("'", ""))
                cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(TextBox27.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value4", Convert.ToDouble(ComboBox9.Text).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(TextBox24.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(TextBox28.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(TextBox26.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(TextBox22.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value9", TextBox25.Text.ToString.Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value10", 0)
                cmd2.Parameters.AddWithValue("@value11", "U")
                cmd2.Parameters.AddWithValue("@value12", ComboBox10.Text)
                cmd2.Parameters.AddWithValue("@value13", "sous_Famille")
                cmd2.Parameters.AddWithValue("@value15", 0)
                cmd2.Parameters.AddWithValue("@value14", "Fournisseur")
                cmd2.Parameters.AddWithValue("@value16", 0)
                cmd2.Parameters.AddWithValue("@value17", 0)
                cmd2.Parameters.AddWithValue("@img", imgName)
                cmd2.ExecuteNonQuery()
                conn2.Close()

                MsgBox("Produit ajouté !")
                TextBox1.Text = ""
                TextBox23.Text = ""
                TextBox22.Text = 0
                TextBox24.Text = 0
                TextBox25.Text = 0
                TextBox26.Text = 0
                TextBox27.Text = 0
                TextBox28.Text = 0

            End If

        End If

    End Sub

    Private Sub Label43_Click(sender As Object, e As EventArgs) Handles Label43.Click

    End Sub

    Private Sub Label42_Click(sender As Object, e As EventArgs) Handles Label42.Click

    End Sub

    Private Sub ComboBox9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox9.SelectedIndexChanged

    End Sub

    Private Sub Panel6_Paint(sender As Object, e As PaintEventArgs) Handles Panel6.Paint

    End Sub

    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        Dim opf As New OpenFileDialog
        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then

            PictureBox3.Image = Image.FromFile(opf.FileName)
            imgName = Path.GetFileName(opf.FileName)
        End If
    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        If TextBox1.Text <> "" Then
            adpt = New MySqlDataAdapter("select * from code_supp where code_supp = '" + TextBox23.Text + "'", conn2)
            Dim tablesupp As New DataTable
            adpt.Fill(tablesupp)
            If tablesupp.Rows.Count() = 0 Then

                conn2.Open()
                sql2 = "INSERT INTO code_supp (prod_code,code_supp) VALUES ('" + TextBox1.Text + "', '" + TextBox29.Text + "')"
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.ExecuteNonQuery()
                conn2.Close()
                TextBox29.Text = ""

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
            End If

        Else
            MsgBox("Insérer d'abord un Code !")
        End If
    End Sub

    Private Sub TextBox5_LostFocus(sender As Object, e As EventArgs) Handles TextBox5.LostFocus
        If TextBox5.Text = "" Then
            TextBox8.Text = 0
            TextBox6.Text = TextBox3.Text
        Else
            TextBox6.Text = Math.Round(TextBox5.Text / (1 + (ComboBox6.Text / 100)), 2, MidpointRounding.AwayFromZero)
            TextBox8.Text = Math.Round(((TextBox6.Text - TextBox3.Text) / TextBox3.Text) * 100, 2, MidpointRounding.AwayFromZero)
        End If
        TextBox8.Select()
    End Sub

    Private Sub TextBox8_LostFocus(sender As Object, e As EventArgs) Handles TextBox8.LostFocus
        If TextBox8.Text = "" Then
            TextBox6.Text = Math.Round(Convert.ToDouble(TextBox3.Text), 2, MidpointRounding.AwayFromZero)
            TextBox5.Text = Math.Round(Convert.ToDouble(TextBox6.Text), 2, MidpointRounding.AwayFromZero)
        Else
            TextBox6.Text = Math.Round(TextBox3.Text + (TextBox3.Text * TextBox8.Text / 100), 2, MidpointRounding.AwayFromZero)
            TextBox5.Text = Math.Round(TextBox6.Text + (TextBox6.Text * ComboBox6.Text / 100), 2, MidpointRounding.AwayFromZero)
        End If
    End Sub

    Private Sub DataGridView3_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellEndEdit
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
            row.Cells(14).Value = Math.Round(((row.Cells(3).Value - (row.Cells(3).Value * (row.Cells(16).Value / 100))) * (row.Cells(9).Value - row.Cells(18).Value)) + (((row.Cells(3).Value - (row.Cells(3).Value * (row.Cells(16).Value / 100))) * (row.Cells(9).Value - row.Cells(18).Value)) * (row.Cells(4).Value / 100)), 2)

            Dim sum As Double = 0
            Dim ht As Double = 0
            Dim remise As Double = 0
            For i = 0 To DataGridView3.Rows.Count - 1
                sum = sum + DataGridView3.Rows(i).Cells(14).Value
                remise = remise + (row.Cells(3).Value * (row.Cells(16).Value / 100) * (row.Cells(9).Value - row.Cells(18).Value))
                ht = ht + ((row.Cells(3).Value - (row.Cells(3).Value * (row.Cells(16).Value / 100))) * (row.Cells(9).Value - row.Cells(18).Value))
            Next

            Label25.Text = Math.Round(sum, 2).ToString("# ##0.00")
            Label23.Text = Math.Round(remise, 2).ToString("# ##0.00")
            Label18.Text = Math.Round(ht, 2).ToString("# ##0.00")
            TextBox11.Text = Math.Round(sum, 2)
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            adpt = New MySqlDataAdapter("select * from article where Code = '" + TextBox1.Text + "'", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                For i = 0 To table.Rows.Count - 1
                    TextBox23.Text = table.Rows(i).Item(1)

                    charg = True
                Next
            Else
                TextBox23.Text = ""

                TextBox23.Select()


                charg = True
            End If

        End If
    End Sub

End Class