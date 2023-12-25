Imports System.IO
Imports System.Text
Imports MySql.Data.MySqlClient
Imports WindowsInput

Public Class ajout
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

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text <> "" Then
            adpt = New MySqlDataAdapter("select * from famille where nomFamille = '" + ComboBox1.Text.ToString + "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)

            adpt2 = New MySqlDataAdapter("select * from sous_famille where idFamille = '" + table.Rows(0).Item(0).ToString + "' order by nomSfamille asc", conn2)
            Dim table2 As New DataTable
            adpt2.Fill(table2)
            ComboBox2.Items.Clear()
            ComboBox2.Items.Add("")
            For i = 0 To table2.Rows.Count - 1
                ComboBox2.Items.Add(table2.Rows(i).Item(2))
            Next
        End If
    End Sub

    Private Sub IconButton1_Click_1(sender As Object, e As EventArgs) Handles IconButton1.Click
        Try
            If TextBox17.Text = "" Then
                MsgBox("Veuillez saisir le N° du Bon de reception !")

            Else
                adpt2 = New MySqlDataAdapter("select * from achat where id = '" + TextBox17.Text + "'", conn2)
                Dim tableachat As New DataTable
                adpt2.Fill(tableachat)
                If tableachat.Rows.Count() = 0 Then
                    conn2.Close()
                    conn2.Open()
                    Dim id As String
                    adpt2 = New MySqlDataAdapter("select `id` from achat order by id DESC", conn2)
                    Dim table0 As New DataTable
                    adpt2.Fill(table0)
                    id = TextBox17.Text

                    Dim s As String = ""
                    Dim t As String = ""
                    s = Convert.ToString(Label25.Text).Replace(".", ",")
                    t = Convert.ToString(TextBox11.Text).Replace(".", ",")

                    sql2 = "INSERT INTO achat (id,montant,Fournisseur,dateAchat,typePay,paye,reste,mtn_ht,rms,ord) VALUES ('" & id & "', '" + Label25.Text + "', '" + ComboBox4.Text + "', '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "', '" & ComboBox5.Text & "', '" & TextBox11.Text & "', '" + Convert.ToString(Convert.ToDouble(s) - Convert.ToDouble(t)) + "', '" + Label18.Text + "', '" + Label23.Text + "','" & num.ToString & "' )"
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
                        table0 = New DataTable
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

                    Try
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
                    Catch
                    End Try
                    MsgBox("Achat bien effectuée")

                    conn2.Close()
                    Me.Close()
                Else
                    MsgBox("Bon déjà saisie !")
                End If

            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
    End Sub
    Dim num As Integer
    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus

        TextBox1.Text = ""
    End Sub
    Private Sub datagridview6_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView6.CellPainting
        ' Vérifiez si c'est une ligne de données (pas la ligne d'en-tête ni la ligne de total)
        If e.RowIndex >= 0 Then
            ' Supprimez les bordures des cellules sauf pour la dernière ligne
            If e.RowIndex < DataGridView6.Rows.Count - 1 Then
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None
            End If
            e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None
        End If
    End Sub
    Private Sub datagridview4_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView4.CellPainting
        ' Vérifiez si c'est une ligne de données (pas la ligne d'en-tête ni la ligne de total)
        If e.RowIndex >= 0 Then
            ' Supprimez les bordures des cellules sauf pour la dernière ligne
            If e.RowIndex < DataGridView4.Rows.Count - 1 Then
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None
            End If
            e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None
        End If
    End Sub
    Private Sub datagridview2_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView2.CellPainting
        ' Vérifiez si c'est une ligne de données (pas la ligne d'en-tête ni la ligne de total)
        If e.RowIndex >= 0 Then
            ' Supprimez les bordures des cellules sauf pour la dernière ligne
            If e.RowIndex < DataGridView2.Rows.Count - 1 Then
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None
            End If
            e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None
        End If
    End Sub
    Private Sub datagridview5_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView5.CellPainting
        ' Vérifiez si c'est une ligne de données (pas la ligne d'en-tête ni la ligne de total)
        If e.RowIndex >= 0 Then
            ' Supprimez les bordures des cellules sauf pour la dernière ligne
            If e.RowIndex < DataGridView5.Rows.Count - 1 Then
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None
            End If
            e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None
        End If
    End Sub

    Private Sub ajout_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        adpt = New MySqlDataAdapter("select stock_fac, balance from parameters", conn2)
        Dim params As New DataTable
        adpt.Fill(params)

        If params.Rows(0).Item(0) = "Desactive" Then
            TextBox26.ReadOnly = True
            TextBox26.Enabled = False
            Label62.ForeColor = Color.Silver
        End If

        If params.Rows(0).Item(1) = "Desactive" Then
            TextBox18.ReadOnly = True
            TextBox18.Enabled = False
            TextBox19.ReadOnly = True
            TextBox19.Enabled = False
            CheckBox1.Enabled = False
            Label54.ForeColor = Color.Silver
            Label36.ForeColor = Color.Silver
            Label39.ForeColor = Color.Silver
        End If

        ComboBox9.Text = Date.Today.Year.ToString()
        TextBox1.Select()
        adpt = New MySqlDataAdapter("select * from infos ", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Panel1.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel3.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel4.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))



        adpt = New MySqlDataAdapter("select * from famille ORDER BY nomFamille asc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        ComboBox1.Items.Add("")
        For i = 0 To table.Rows.Count - 1

            ComboBox1.Items.Add(table.Rows(i).Item(1))
        Next

        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim tablebq As New DataTable
        adpt.Fill(tablebq)

        For i = 0 To tablebq.Rows.Count - 1
            ComboBox7.Items.Add(tablebq.Rows(i).Item(1))
            ComboBox8.Items.Add(tablebq.Rows(i).Item(1))
        Next

        adpt3 = New MySqlDataAdapter("select * from fours order by name asc", conn2)
        Dim table3 As New DataTable
        adpt3.Fill(table3)

        For i = 0 To table3.Rows.Count - 1
            ComboBox4.Items.Add(table3.Rows(i).Item(1))
        Next
        ComboBox6.Text = 0

    End Sub


    Private Sub TextBox7_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox7.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox7.Text = "" Then

                TextBox7.Text = 0
                TextBox8.Text = Convert.ToDouble(((TextBox6.Text.Replace(".", ",") - TextBox3.Text.Replace(".", ",")) / TextBox3.Text.Replace(".", ",")) * 100).ToString("# ##0.00")
                TextBox3.Text = Convert.ToDouble(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToDouble(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox3.Text = Convert.ToDouble(TextBox3.Text).ToString("N2")
            Else
                TextBox8.Text = 0
                TextBox3.Text = Convert.ToDouble(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToDouble(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox3.Text = Convert.ToDouble(TextBox3.Text).ToString("N2")

            End If
            TextBox8.Select()
            TextBox7.Text = Convert.ToDouble(TextBox7.Text.Replace(" ", "").Replace(".", ",")).ToString("N2")

        End If
    End Sub

    Private Sub TextBox8_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox8.Text = "" Then

                TextBox8.Text = 0
                TextBox7.Text = Convert.ToDouble(Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))).ToString("N2")
                TextBox3.Text = Convert.ToDouble(Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))).ToString("N2")
                TextBox6.Text = Convert.ToDouble(Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))).ToString("N2")
                TextBox5.Text = Convert.ToDouble(Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))).ToString("N2")
            Else
                TextBox7.Text = Convert.ToDouble(Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))).ToString("N2")
                TextBox3.Text = Convert.ToDouble(Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))).ToString("N2")
                TextBox6.Text = Convert.ToDouble(Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))).ToString("N2")
                TextBox5.Text = Convert.ToDouble(Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))).ToString("N2")
            End If
            TextBox5.Select()
            TextBox8.Text = Convert.ToDouble(TextBox8.Text.Replace(" ", "").Replace(".", ",")).ToString("N2")

        End If
    End Sub

    Private Sub TextBox5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim TVA As Double

            TVA = ComboBox6.Text

            If TextBox5.Text = "" Then

                TextBox5.Text = 0
                If TextBox3.Text = 0 Then
                    TextBox3.Text = TextBox5.Text
                End If
                TextBox6.Text = Convert.ToDouble(TextBox5.Text.Replace(".", ",").Replace(" ", "") / (1 + (TVA.ToString.Replace(".", ",") / 100))).ToString("# ##0.00")
                TextBox8.Text = Convert.ToDouble(((TextBox6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",").Replace(" ", "")) / TextBox3.Text.Replace(".", ",").Replace(" ", "")) * 100).ToString("# ##0.00")
                TextBox7.Text = Convert.ToDouble(Convert.ToString(TextBox3.Text.Replace(".", ",").Replace(" ", "")) + (Convert.ToString(TextBox3.Text.Replace(".", ",").Replace(" ", "")) * (Convert.ToString(TVA.ToString.Replace(".", ",")) / 100))).ToString("N2")
                TextBox3.Text = Convert.ToDouble(Convert.ToString(TextBox7.Text.Replace(".", ",").Replace(" ", "")) / (1 + (Convert.ToString(TVA.ToString.Replace(".", ",").Replace(" ", "")) / 100))).ToString("N2")
                TextBox6.Text = Convert.ToDouble(Convert.ToString(TextBox3.Text.Replace(".", ",").Replace(" ", "")) + (Convert.ToString(TextBox3.Text.Replace(".", ",").Replace(" ", "")) * (Convert.ToString(TextBox8.Text.Replace(".", ",").Replace(" ", "")) / 100))).ToString("N2")
            Else
                If TextBox3.Text = 0 Then
                    TextBox3.Text = TextBox5.Text
                End If
                TextBox6.Text = Convert.ToDouble(TextBox5.Text.Replace(".", ",").Replace(" ", "") / (1 + (TVA.ToString.Replace(".", ",") / 100))).ToString("N2")
                TextBox8.Text = Convert.ToDouble(((TextBox6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",").Replace(" ", "")) / TextBox3.Text.Replace(".", ",").Replace(" ", "")) * 100).ToString("N2")
                TextBox7.Text = Convert.ToDouble(Convert.ToString(TextBox3.Text.Replace(".", ",").Replace(" ", "")) + (Convert.ToString(TextBox3.Text.Replace(".", ",").Replace(" ", "")) * (Convert.ToString(TVA.ToString.Replace(".", ",")) / 100))).ToString("N2")
                TextBox3.Text = Convert.ToDouble(Convert.ToString(TextBox7.Text.Replace(".", ",").Replace(" ", "")) / (1 + (Convert.ToString(TVA.ToString.Replace(".", ",")) / 100))).ToString("N2")
                TextBox6.Text = Convert.ToDouble(Convert.ToString(TextBox3.Text.Replace(".", ",").Replace(" ", "")) + (Convert.ToString(TextBox3.Text.Replace(".", ",").Replace(" ", "")) * (Convert.ToString(TextBox8.Text.Replace(".", ",").Replace(" ", "")) / 100))).ToString("N2")
            End If

            TextBox5.Text = Convert.ToDouble(TextBox5.Text.Replace(" ", "").Replace(".", ",")).ToString("N2")

        End If
    End Sub
    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Text = "" Then
                TextBox3.Text = 0
                TextBox7.Text = Convert.ToDouble(Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))).ToString("N2")
            Else

                TextBox7.Text = Convert.ToDouble(Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))).ToString("N2")
            End If
            ComboBox6.Select()
            ComboBox6.DroppedDown = True
            TextBox3.Text = Convert.ToDouble(TextBox3.Text.Replace(" ", "").Replace(".", ",")).ToString("N2")
        End If
    End Sub
    Private Sub DataGridView2_CellMouseClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 1 Then

                Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)

                Dim yesornoMsg2 As New yesorno
                yesornoMsg2.Label14.Text = "Voulez-vous vraiment supprimer ce code supplémentaire ?"
                yesornoMsg2.ShowDialog()
                If Msgboxresult = True Then

                    conn2.Close()
                    conn2.Open()
                    Dim sql2, sql3 As String
                    Dim cmd2, cmd3 As MySqlCommand
                    sql2 = "delete from code_supp where code_supp = '" + row.Cells(0).Value.ToString + "' "
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()
                    conn2.Close()

                    DataGridView2.Rows.Clear()
                    adpt = New MySqlDataAdapter("select * from code_supp where prod_code = '" + TextBox1.Text + "'", conn2)
                    Dim tablesupp As New DataTable
                    adpt.Fill(tablesupp)
                    For j = 0 To tablesupp.Rows.Count() - 1
                        DataGridView2.Rows.Add(tablesupp.Rows(j).Item(2))
                    Next
                End If
            End If
        End If

    End Sub
    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

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
                Dim yesornoMsg2 As New yesorno
                yesornoMsg2.Label14.Text = "Voulez-vous vraiment supprimer ce produit ?"
                yesornoMsg2.ShowDialog()
                If Msgboxresult = True Then
                    Dim row As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
                    DataGridView3.Rows.Remove(row)
                    Dim sum As Double = 0
                    Dim ht As Double = 0
                    Dim remise As Double = 0
                    For i = 0 To DataGridView3.Rows.Count - 1
                        sum = sum + DataGridView3.Rows(i).Cells(14).Value.ToString.Replace(".", ",")
                        remise = remise + (DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",") * DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",") * (DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ",") / 100))
                        ht = (sum / (1 + DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",") / 100)) - remise
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
            TextBox2.Select()

            TextBox1.Text = cod
            Label49.Text = TextBox1.Text

            TextBox2.Text = ""
            TextBox3.Text = 0
            ComboBox6.Text = "0"
            TextBox7.Text = 0
            TextBox8.Text = 0
            TextBox6.Text = 0
            TextBox5.Text = 0
            TextBox10.Text = 0
            TextBox26.Text = 0
            TextBox9.Text = 0
            ComboBox3.Text = "U"
            ComboBox1.Text = ""
            ComboBox2.Text = ""
            ComboBox4.Text = ""
            TextBox13.Text = 0
            TextBox18.Text = 0
            TextBox23.Text = ""
            TextBox22.Text = ""
            TextBox24.Text = ""
            If TextBox18.Text <> 0 Then
                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If
            TextBox19.Text = 0
            DataGridView4.Rows.Clear()
            DataGridView1.Rows.Clear()
            DataGridView2.Rows.Clear()
            DataGridView5.Rows.Clear()


            Label44.Text = 0
            Label42.Text = 0
            Label58.Text = "-"
            Label53.Text = "00/00/0000"
            IconButton16.Visible = False
            IconButton13.Visible = True

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
            If TextBox2.Text = "" Then
                adpt = New MySqlDataAdapter("select Code from article WHERE LEFT(Code, 3) = '260' AND LENGTH(Code) <= 7 order by Code desc", conn2)
                Dim tablebq As New DataTable
                adpt.Fill(tablebq)
                Dim plu As Double

                If tablebq.Rows.Count <> 0 Then
                    plu = tablebq.Rows(0).Item(0) + 1
                Else
                    plu = 2600001
                End If

                TextBox18.Text = plu.ToString.Replace("260", "")
                TextBox1.Text = plu
                Label49.Text = plu
                TextBox2.Select()
                IconButton16.Visible = False
                IconButton13.Visible = True
            End If
            TextBox18.Enabled = True
            TextBox19.Enabled = True
        Else
            TextBox18.Text = 0
            TextBox19.Text = 0
            TextBox23.Text = ""
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
                    conn2.Close()
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

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        Dim id As String
        conn2.Close()
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
        Try
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
        Catch
        End Try
        Dim yesornoMsg2 As New success
        yesornoMsg2.Label14.Text = "Modification bien effectuée"
        yesornoMsg2.Panel5.Visible = True
        yesornoMsg2.ShowDialog()
        Me.Close()

        conn2.Close()
    End Sub

    Private Sub IconButton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        If Panel6.Visible = False Then
            TextBox1.Text = ""
            TextBox25.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = 0
            ComboBox6.Text = "0"
            TextBox7.Text = 0
            TextBox8.Text = 0
            TextBox6.Text = 0
            TextBox5.Text = 0
            TextBox10.Text = 0
            TextBox26.Text = 0
            TextBox9.Text = 0
            ComboBox3.Text = "U"
            ComboBox1.Text = ""
            ComboBox2.Text = ""
            ComboBox4.Text = ""
            TextBox13.Text = 0
            TextBox18.Text = 0
            TextBox23.Text = ""
            TextBox22.Text = ""
            TextBox24.Text = ""
            If TextBox18.Text <> 0 Then
                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If
            TextBox19.Text = 0
            DataGridView4.Rows.Clear()
            DataGridView1.Rows.Clear()
            DataGridView2.Rows.Clear()
            DataGridView5.Rows.Clear()


            Label44.Text = 0
            Label42.Text = 0
            Label49.Text = ""
            Label58.Text = "-"
            Label53.Text = "00/00/0000"
            TextBox2.Select()
            Panel6.Visible = True
            charg = True
        Else
            TextBox2.Select()
            Panel6.Visible = False
        End If

    End Sub

    Private Sub TextBox22_TextChanged(sender As Object, e As EventArgs) Handles TextBox22.TextChanged
        If TextBox22.Text = "" Then
            adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
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
            Dim inputText As String = TextBox22.Text

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
                RemoveHandler TextBox22.TextChanged, AddressOf TextBox22_TextChanged

                ' Mettez à jour le texte dans le TextBox
                TextBox22.Text = modifiedText

                ' Replacez le curseur à la position correcte après la modification
                TextBox22.SelectionStart = TextBox22.Text.Length

                ' Réactivez le gestionnaire d'événements TextChanged
                AddHandler TextBox22.TextChanged, AddressOf TextBox22_TextChanged
            End If
            ComboBox4.Items.Clear()
            adpt = New MySqlDataAdapter("select name from fours WHERE name LIKE '%" + TextBox22.Text.Replace("'", " ") + "%' order by name asc", conn2)
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

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            IconButton16.Visible = True
            IconButton13.Visible = False
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            TextBox1.Text = row.Cells(0).Value
            adpt = New MySqlDataAdapter("select * from article where Code = '" & TextBox1.Text & "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            Label49.Text = table.Rows(0).Item(0)
            TextBox1.Text = table.Rows(0).Item(0)
            TextBox2.Text = table.Rows(0).Item(1)
            TextBox3.Text = table.Rows(0).Item(2).ToString.Replace(".", ",")
            ComboBox6.Text = table.Rows(0).Item(3).ToString.Replace(",00", "")
            TextBox5.Text = table.Rows(0).Item(7).ToString.Replace(".", ",")
            TextBox6.Text = table.Rows(0).Item(6).ToString.Replace(".", ",")
            TextBox7.Text = table.Rows(0).Item(4).ToString.Replace(".", ",")
            TextBox8.Text = table.Rows(0).Item(5).ToString.Replace(".", ",")
            TextBox9.Text = table.Rows(0).Item(9)
            TextBox10.Text = table.Rows(0).Item(8)
            TextBox26.Text = table.Rows(0).Item(25)
            TextBox18.Text = table.Rows(0).Item(18)
            TextBox19.Text = table.Rows(0).Item(19)
            TextBox25.Text = table.Rows(0).Item(23)
            If TextBox18.Text <> 0 Then
                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If
            ComboBox1.SelectedItem = table.Rows(0).Item(11)
            ComboBox2.SelectedItem = table.Rows(0).Item(12)
            ComboBox3.SelectedItem = table.Rows(0).Item(13)
            ComboBox4.SelectedItem = table.Rows(0).Item(10)
            If table.Rows(0).Item(22) = 0 Then
                IconButton2.Text = "Masquer"
                IconButton2.IconChar = FontAwesome.Sharp.IconChar.EyeSlash
                IconButton2.BackColor = Color.RoyalBlue
            Else
                IconButton2.Text = "Démasquer"
                IconButton2.IconChar = FontAwesome.Sharp.IconChar.Eye
                IconButton2.BackColor = Color.DimGray
            End If
            DataGridView2.Rows.Clear()
            adpt = New MySqlDataAdapter("select * from code_supp where prod_code = '" + TextBox1.Text + "'", conn2)
            Dim tablesupp2 As New DataTable
            adpt.Fill(tablesupp2)
            For j = 0 To tablesupp2.Rows.Count() - 1
                DataGridView2.Rows.Add(tablesupp2.Rows(j).Item(2))
            Next

            adpt = New MySqlDataAdapter("select * from demarque where year(demarque.date) = '" + ComboBox9.Text + "' and `code` = '" + Label49.Text + "' order by id desc ", conn2)
            Dim tabledemarque As New DataTable
            adpt.Fill(tabledemarque)
            Dim qteannul, chrgannul, qte1, valr1, qte2, valr2, qte3, valr3, qte4, valr4, qte5, valr5, qte6, valr6, qte7, valr7, qte8, valr8, qte9, valr9, qte10, valr10, qte11, valr11, qte12, valr12 As Double

            DataGridView4.Rows.Clear()
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
            DataGridView4.Rows.Add("Janvier", qte1, valr1)
            DataGridView4.Rows.Add("Février", qte2, valr2)
            DataGridView4.Rows.Add("Mars", qte3, valr3)
            DataGridView4.Rows.Add("Avril", qte4, valr4)
            DataGridView4.Rows.Add("Mai", qte5, valr5)
            DataGridView4.Rows.Add("Juin", qte6, valr6)
            DataGridView4.Rows.Add("Juillet", qte7, valr7)
            DataGridView4.Rows.Add("Aout", qte8, valr8)
            DataGridView4.Rows.Add("Séptembre", qte9, valr9)
            DataGridView4.Rows.Add("Octobre", qte10, valr10)
            DataGridView4.Rows.Add("Novembre", qte11, valr11)
            DataGridView4.Rows.Add("Décembre", qte12, valr12)

            Label44.Text = qteannul.ToString("#0.00")
            Label42.Text = chrgannul.ToString("#0.00 DHs")

            DataGridView5.Rows.Clear()

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

                DataGridView5.Rows.Add(tablehisto.Rows(m).Item(0), tablehisto.Rows(m).Item(2))
            Next

            charg = True
        End If

        Panel6.Visible = False
        TextBox2.Select()

    End Sub

    Private Sub IconButton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
        If TextBox1.Text <> "" AndAlso TextBox23.Text <> "" Then
            adpt = New MySqlDataAdapter("select * from code_supp where code_supp = '" + TextBox23.Text + "'", conn2)
            Dim tablesupp As New DataTable
            adpt.Fill(tablesupp)
            If tablesupp.Rows.Count() = 0 Then
                If IconButton16.Visible = False Then
                    DataGridView2.Rows.Add(TextBox23.Text)
                Else
                    conn2.Close()
                    conn2.Open()
                    sql2 = "INSERT INTO code_supp (prod_code,code_supp) VALUES ('" + TextBox1.Text + "', '" + TextBox23.Text + "')"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()
                    conn2.Close()

                    DataGridView2.Rows.Clear()
                    adpt = New MySqlDataAdapter("select * from code_supp where prod_code = '" + TextBox1.Text + "'", conn2)
                    Dim tablesupp2 As New DataTable
                    adpt.Fill(tablesupp2)
                    For j = 0 To tablesupp2.Rows.Count() - 1
                        DataGridView2.Rows.Add(tablesupp2.Rows(j).Item(2))
                    Next
                    Try
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
                    Catch
                    End Try
                End If
            Else
                Dim yesornoMsg2 As New eror
                yesornoMsg2.Label14.Text = "Ce code existe déjà"
                yesornoMsg2.Panel5.Visible = True
                yesornoMsg2.ShowDialog()
            End If
        Else
            Dim yesornoMsg2 As New yesorno
            yesornoMsg2.Label14.Text = "Insérer d'abord un Code !"
            yesornoMsg2.Panel5.Visible = True
            yesornoMsg2.ShowDialog()
        End If
        TextBox23.Text = ""
    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Ajouter un article'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then


                If TextBox1.Text <> "" AndAlso TextBox2.Text <> "" Then

                    Dim table0 As DataTable
                    adpt2 = New MySqlDataAdapter("SELECT * FROM `article` WHERE `Code`= '" + TextBox1.Text + "' ", conn2)
                    table0 = New DataTable
                    adpt2.Fill(table0)

                    If table0.Rows.Count = 0 Then
                        conn2.Close()
                        conn2.Open()

                        cmd2 = New MySqlCommand("INSERT INTO `article`(`Code`, `Article`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `Stock`, `Securite_stock`, `unit`, `idFamille`, `idSFamille`, `fournisseur`, `pv_gros`, `plu`, `dlc`,`user`,`ref`,`facture`) VALUES (@value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8, @value9, @value10, @value11, @value12, @value13, @value14, @value15, @value16, @value17,@user,@ref,@fac)", conn2)
                        cmd2.Parameters.AddWithValue("@value1", TextBox1.Text.Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value2", TextBox2.Text.Replace("'", " "))
                        cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(TextBox3.Text.Replace(".", ",")).ToString("#0.00"))
                        cmd2.Parameters.AddWithValue("@value4", Convert.ToDouble(ComboBox6.Text.Replace(".", ",")).ToString("#0.00"))
                        cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(TextBox7.Text.Replace(".", ",")).ToString("#0.00"))
                        cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(TextBox8.Text.Replace(".", ",")).ToString("#0.00"))
                        cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(TextBox6.Text.Replace(".", ",")).ToString("#0.00"))
                        cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(TextBox5.Text.Replace(".", ",")).ToString("#0.00"))
                        cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(TextBox10.Text.Replace(".", ",")).ToString("#0.00"))
                        cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(TextBox9.Text.Replace(".", ",")).ToString("#0.00"))
                        cmd2.Parameters.AddWithValue("@value11", ComboBox3.Text)
                        cmd2.Parameters.AddWithValue("@value12", ComboBox1.Text)
                        cmd2.Parameters.AddWithValue("@value13", ComboBox2.Text)
                        cmd2.Parameters.AddWithValue("@value14", ComboBox4.Text)
                        cmd2.Parameters.AddWithValue("@value15", Convert.ToDouble(TextBox13.Text.Replace(".", ",")).ToString("#0.00"))
                        cmd2.Parameters.AddWithValue("@value16", TextBox18.Text)
                        cmd2.Parameters.AddWithValue("@value17", Convert.ToDouble(TextBox19.Text.Replace(".", ",")))
                        cmd2.Parameters.AddWithValue("@user", Home.Label2.Text)
                        If TextBox25.Text = "" Then
                            TextBox25.Text = "-"
                        End If
                        cmd2.Parameters.AddWithValue("@ref", TextBox25.Text)
                        cmd2.Parameters.AddWithValue("@fac", TextBox26.Text)
                        cmd2.ExecuteNonQuery()


                        For i = 0 To DataGridView2.Rows.Count - 1
                            sql2 = "INSERT INTO code_supp (prod_code,code_supp) VALUES ('" + TextBox1.Text + "', '" + DataGridView2.Rows(i).Cells(0).Value + "')"
                            cmd2 = New MySqlCommand(sql2, conn2)
                            cmd2.ExecuteNonQuery()
                        Next
                        If CheckBox1.Checked = True Then
                            sql2 = "INSERT INTO code_supp (prod_code,code_supp) VALUES ('" + TextBox1.Text + "', '" + TextBox18.Text + "')"
                            cmd2 = New MySqlCommand(sql2, conn2)
                            cmd2.ExecuteNonQuery()
                        End If

                        cmd2 = New MySqlCommand("INSERT INTO `ref_frs`(`product_id`, `fournisseur`, `ref`) VALUES (@value1, @value2, @value3)", conn2)
                        cmd2.Parameters.AddWithValue("@value1", TextBox1.Text.Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value2", ComboBox4.Text)
                        cmd2.Parameters.AddWithValue("@value3", "-")
                        cmd2.ExecuteNonQuery()

                        sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        If role = "Caissier" Then
                            cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
                        Else
                            cmd3.Parameters.AddWithValue("@name", Home.Label2.Text)
                        End If
                        cmd3.Parameters.AddWithValue("@op", "Ajout de l'Article : " & TextBox2.Text.Replace("'", " "))
                        cmd3.ExecuteNonQuery()

                        conn2.Close()

                        Dim yesornoMsg2 As New success
                        yesornoMsg2.Label14.Text = "Article bien ajouté !"
                        yesornoMsg2.Panel5.Visible = True
                        yesornoMsg2.ShowDialog()

                        TextBox1.Text = ""
                        TextBox25.Text = ""
                        TextBox2.Text = ""
                        TextBox3.Text = 0
                        ComboBox6.Text = "0"
                        TextBox7.Text = 0
                        TextBox8.Text = 0
                        TextBox6.Text = 0
                        TextBox5.Text = 0
                        TextBox10.Text = 0
                        TextBox26.Text = 0
                        TextBox9.Text = 0
                        ComboBox3.Text = "U"
                        ComboBox1.Text = ""
                        ComboBox2.Text = ""
                        ComboBox4.Text = ""
                        TextBox13.Text = 0
                        TextBox18.Text = 0
                        TextBox23.Text = ""
                        TextBox22.Text = ""
                        TextBox24.Text = ""
                        If TextBox18.Text <> 0 Then
                            CheckBox1.Checked = True
                        Else
                            CheckBox1.Checked = False
                        End If
                        TextBox19.Text = 0
                        DataGridView4.Rows.Clear()
                        DataGridView1.Rows.Clear()
                        DataGridView2.Rows.Clear()
                        DataGridView5.Rows.Clear()


                        Label44.Text = 0
                        Label42.Text = 0
                        Label49.Text = ""
                        Label58.Text = "-"
                        Label53.Text = "00/00/0000"
                        TextBox1.Select()

                        Try
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
                                Dim info As Byte() = New UTF8Encoding(True).GetBytes("260" & table.Rows(i).Item(18).ToString.PadLeft(4, "0"c) & "EEEEE|" & 1 & " |" & table.Rows(i).Item(18).ToString.PadLeft(4, "0"c) & "|" & "   |" & "    |" & Convert.ToString(Convert.ToDouble(table.Rows(i).Item(7)).ToString("N2")).Replace(".", "").Replace(",", "").Replace(" ", "").PadLeft(7, " "c) & "|" & "W|" & Convert.ToString(table.Rows(i).Item(19)).PadLeft(3, " "c) & "|" & 0 & "|" & Convert.ToString(table.Rows(i).Item(1)).PadRight(25, " "c) & "|     |" & vbCrLf)
                                fs.Write(info, 0, info.Length)
                            Next
                            fs.Close()
                        Catch
                        End Try

                    Else
                        Dim yesornoMsg2 As New eror
                        yesornoMsg2.Label14.Text = "Cet Article existe déjà !"
                        yesornoMsg2.Panel5.Visible = True
                        yesornoMsg2.ShowDialog()
                        TextBox1.Text = ""
                        TextBox25.Text = ""
                        TextBox2.Text = ""
                        TextBox3.Text = 0
                        ComboBox6.Text = "0"
                        TextBox7.Text = 0
                        TextBox8.Text = 0
                        TextBox6.Text = 0
                        TextBox5.Text = 0
                        TextBox10.Text = 0
                        TextBox26.Text = 0
                        TextBox9.Text = 0
                        ComboBox3.Text = "U"
                        ComboBox1.Text = ""
                        ComboBox2.Text = ""
                        ComboBox4.Text = ""
                        TextBox13.Text = 0
                        TextBox18.Text = 0
                        TextBox23.Text = ""
                        TextBox22.Text = ""
                        TextBox24.Text = ""
                        If TextBox18.Text <> 0 Then
                            CheckBox1.Checked = True
                        Else
                            CheckBox1.Checked = False
                        End If
                        TextBox19.Text = 0
                        DataGridView4.Rows.Clear()
                        DataGridView1.Rows.Clear()
                        DataGridView2.Rows.Clear()
                        DataGridView5.Rows.Clear()


                        Label44.Text = 0
                        Label42.Text = 0
                        Label49.Text = ""
                        Label58.Text = "-"
                        Label53.Text = "00/00/0000"
                        TextBox1.Select()
                    End If
                Else
                    Dim yesornoMsg2 As New eror
                    yesornoMsg2.Label14.Text = "Veuillez remplir les champs d'article !"
                    yesornoMsg2.Panel5.Visible = True
                    yesornoMsg2.ShowDialog()
                End If
            Else
                Dim yesornoMsg2 As New eror
                yesornoMsg2.Label14.Text = "Vous n'avez pas l'autorisation !"
                yesornoMsg2.Panel5.Visible = True
                yesornoMsg2.ShowDialog()
            End If
        End If
    End Sub

    Private Sub IconButton15_Click(sender As Object, e As EventArgs) Handles IconButton15.Click
        Dim yesornoMsg2 As New yesorno
        yesornoMsg2.Label14.Text = "voullez-vous vraiment démarquer ce produit ?"
        yesornoMsg2.ShowDialog()
        If Msgboxresult = True Then
            conn2.Close()
            conn2.Open()
            sql2 = "INSERT INTO demarque ( `code`, `qte`,`valeur`, `date`) VALUES (@v1,@v3,@v6,@v7)"

            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.AddWithValue("@v1", Label49.Text)
            cmd2.Parameters.AddWithValue("@v3", Convert.ToDouble(TextBox24.Text.ToString.Replace(".", ",")))
            cmd2.Parameters.AddWithValue("@v6", Convert.ToDouble((TextBox24.Text * TextBox7.Text).ToString.Replace(".", ",")))
            cmd2.Parameters.AddWithValue("@v7", DateTime.Today)

            cmd2.ExecuteNonQuery()

            cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  REPLACE(REPLACE(`Stock`,',','.'),' ','') - '" & Convert.ToDouble(TextBox24.Text.ToString.Replace(".", ",")) & "' WHERE `Code` = '" + TextBox1.Text + "' ", conn2)
            cmd2.ExecuteNonQuery()

            conn2.Close()


        End If
        DataGridView4.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from demarque where year(demarque.date) = '" + ComboBox9.Text + "' and `code` = '" + Label49.Text + "' order by id desc ", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim qteannul, chrgannul, qte1, valr1, qte2, valr2, qte3, valr3, qte4, valr4, qte5, valr5, qte6, valr6, qte7, valr7, qte8, valr8, qte9, valr9, qte10, valr10, qte11, valr11, qte12, valr12 As Double

        DataGridView4.Rows.Clear()
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
        DataGridView4.Rows.Add("Janvier", qte1, valr1)
        DataGridView4.Rows.Add("Février", qte2, valr2)
        DataGridView4.Rows.Add("Mars", qte3, valr3)
        DataGridView4.Rows.Add("Avril", qte4, valr4)
        DataGridView4.Rows.Add("Mai", qte5, valr5)
        DataGridView4.Rows.Add("Juin", qte6, valr6)
        DataGridView4.Rows.Add("Juillet", qte7, valr7)
        DataGridView4.Rows.Add("Aout", qte8, valr8)
        DataGridView4.Rows.Add("Séptembre", qte9, valr9)
        DataGridView4.Rows.Add("Octobre", qte10, valr10)
        DataGridView4.Rows.Add("Novembre", qte11, valr11)
        DataGridView4.Rows.Add("Décembre", qte12, valr12)

        Label44.Text = qteannul.ToString("#0.00")
        Label42.Text = chrgannul.ToString("#0.00 DHs")
        conn2.Close()
        conn2.Open()
        sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.Parameters.Clear()
        cmd3.Parameters.AddWithValue("@name", Home.Label2.Text)
        cmd3.Parameters.AddWithValue("@op", "Démarquage du produit " & TextBox2.Text & " par une quantité de " & TextBox24.Text)
        cmd3.ExecuteNonQuery()
        conn2.Close()
        TextBox24.Text = ""
    End Sub

    Private Sub IconButton16_Click(sender As Object, e As EventArgs) Handles IconButton16.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Modifier un article'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then



                Dim table0 As DataTable
                adpt2 = New MySqlDataAdapter("SELECT * FROM `article` WHERE `Code`= '" + Label49.Text + "' ", conn2)
                table0 = New DataTable
                adpt2.Fill(table0)

                If table0.Rows.Count <> 0 Then
                    conn2.Close()
                    conn2.Open()

                    cmd2 = New MySqlCommand("UPDATE `article` SET `ref`=@ref, `Code`= @value1,`Article` = @value2, `PA_HT` = @value3, `TVA` = @value4, `PA_TTC` = @value5, `TM` = @value6, `PV_HT` = @value7, `PV_TTC` = @value8, `Stock` = @value9, `Securite_stock` = @value10, `unit` = @value11, `idFamille` = @value12, `idSFamille` = @value13, `fournisseur` = @value14, `pv_gros` = @value15, `plu` = @value16, `dlc` = @value17,`facture` = @fac WHERE `Code` = @value18", conn2)
                    cmd2.Parameters.AddWithValue("@value1", TextBox1.Text)
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
                    cmd2.Parameters.AddWithValue("@value12", ComboBox1.Text)
                    cmd2.Parameters.AddWithValue("@value13", ComboBox2.Text)
                    cmd2.Parameters.AddWithValue("@value14", ComboBox4.Text)
                    cmd2.Parameters.AddWithValue("@value15", Convert.ToDouble(TextBox13.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                    cmd2.Parameters.AddWithValue("@value16", TextBox18.Text)
                    cmd2.Parameters.AddWithValue("@value17", Convert.ToDouble(TextBox19.Text.ToString.Replace(".", ",")))
                    cmd2.Parameters.AddWithValue("@value18", Label49.Text)
                    If TextBox25.Text = "" Then
                        TextBox25.Text = "-"
                    End If
                    cmd2.Parameters.AddWithValue("@ref", TextBox25.Text)
                    cmd2.Parameters.AddWithValue("@fac", TextBox26.Text)
                    cmd2.ExecuteNonQuery()

                    adpt = New MySqlDataAdapter("select * from ref_frs where product_id = '" & Label49.Text & "' and fournisseur ='" & ComboBox4.Text & "'", conn2)
                    Dim tablelike2 As New DataTable
                    adpt.Fill(tablelike2)
                    If tablelike2.Rows.Count() = 0 Then
                        cmd2 = New MySqlCommand("INSERT INTO `ref_frs`(`product_id`, `fournisseur`) VALUES (@value1, @value2)", conn2)
                        cmd2.Parameters.AddWithValue("@value1", TextBox1.Text)
                        cmd2.Parameters.AddWithValue("@value2", ComboBox4.Text)
                        cmd2.ExecuteNonQuery()
                    End If

                    cmd2 = New MySqlCommand("UPDATE `achatdetails` SET `NameArticle`='" & TextBox2.Text.Replace("'", " ") & "' WHERE `CodeArticle` = '" & Label49.Text & "'", conn2)
                    cmd2.ExecuteNonQuery()

                    cmd2 = New MySqlCommand("UPDATE `achatdetailssauv` SET `NameArticle`='" & TextBox2.Text.Replace("'", " ") & "' WHERE `CodeArticle` = '" & Label49.Text & "'", conn2)
                    cmd2.ExecuteNonQuery()

                    sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    If role = "Caissier" Then
                        cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
                    Else
                        cmd3.Parameters.AddWithValue("@name", Home.Label2.Text)
                    End If
                    cmd3.Parameters.AddWithValue("@op", "Modification de l'Article : " & TextBox2.Text.Replace("'", " "))
                    cmd3.ExecuteNonQuery()

                    conn2.Close()

                    Dim yesornoMsg2 As New success
                    yesornoMsg2.Label14.Text = "Article bien modifié !"
                    yesornoMsg2.Panel5.Visible = True
                    yesornoMsg2.ShowDialog()

                    TextBox1.Text = ""
                    TextBox25.Text = ""
                    TextBox2.Text = ""
                    TextBox3.Text = 0
                    ComboBox6.Text = "0"
                    TextBox7.Text = 0
                    TextBox8.Text = 0
                    TextBox6.Text = 0
                    TextBox5.Text = 0
                    TextBox10.Text = 0
                    TextBox26.Text = 0
                    TextBox9.Text = 0
                    ComboBox3.Text = "U"
                    ComboBox1.Text = ""
                    ComboBox2.Text = ""
                    ComboBox4.Text = ""
                    TextBox13.Text = 0
                    TextBox18.Text = 0
                    TextBox23.Text = ""
                    TextBox22.Text = ""
                    TextBox24.Text = ""
                    If TextBox18.Text <> 0 Then
                        CheckBox1.Checked = True
                    Else
                        CheckBox1.Checked = False
                    End If
                    TextBox19.Text = 0
                    DataGridView4.Rows.Clear()
                    DataGridView1.Rows.Clear()
                    DataGridView2.Rows.Clear()
                    DataGridView5.Rows.Clear()


                    Label44.Text = 0
                    Label42.Text = 0
                    Label49.Text = ""
                    Label58.Text = "-"
                    Label53.Text = "00/00/0000"
                    TextBox1.Select()

                    Try
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
                            Dim info As Byte() = New UTF8Encoding(True).GetBytes("260" & table.Rows(i).Item(18).ToString.PadLeft(4, "0"c) & "EEEEE|" & 1 & " |" & table.Rows(i).Item(18).ToString.PadLeft(4, "0"c) & "|" & "   |" & "    |" & Convert.ToString(Convert.ToDouble(table.Rows(i).Item(7)).ToString("N2")).Replace(".", "").Replace(",", "").Replace(" ", "").PadLeft(7, " "c) & "|" & "W|" & Convert.ToString(table.Rows(i).Item(19)).PadLeft(3, " "c) & "|" & 0 & "|" & Convert.ToString(table.Rows(i).Item(1)).PadRight(25, " "c) & "|     |" & vbCrLf)
                            fs.Write(info, 0, info.Length)
                        Next
                        fs.Close()
                    Catch
                    End Try


                Else
                    Dim yesornoMsg2 As New eror
                    yesornoMsg2.Label14.Text = "Cette article est non disponible !"
                    yesornoMsg2.Panel5.Visible = True
                    yesornoMsg2.ShowDialog()

                    TextBox1.Text = ""
                    TextBox25.Text = ""
                    TextBox2.Text = ""
                    TextBox3.Text = 0
                    ComboBox6.Text = "0"
                    TextBox7.Text = 0
                    TextBox8.Text = 0
                    TextBox6.Text = 0
                    TextBox5.Text = 0
                    TextBox10.Text = 0
                    TextBox26.Text = 0
                    TextBox9.Text = 0
                    ComboBox3.Text = "U"
                    ComboBox1.Text = ""
                    ComboBox2.Text = ""
                    ComboBox4.Text = ""
                    TextBox13.Text = 0
                    TextBox18.Text = 0
                    TextBox23.Text = ""
                    TextBox22.Text = ""
                    TextBox24.Text = ""
                    If TextBox18.Text <> 0 Then
                        CheckBox1.Checked = True
                    Else
                        CheckBox1.Checked = False
                    End If
                    TextBox19.Text = 0
                    DataGridView4.Rows.Clear()
                    DataGridView1.Rows.Clear()
                    DataGridView2.Rows.Clear()
                    DataGridView5.Rows.Clear()


                    Label44.Text = 0
                    Label42.Text = 0
                    Label49.Text = ""
                    Label58.Text = "-"
                    Label53.Text = "00/00/0000"
                    TextBox1.Select()
                End If
            Else
                Dim yesornoMsg2 As New eror
                yesornoMsg2.Label14.Text = "Vous n'avez pas l'autorisation !"
                yesornoMsg2.Panel5.Visible = True
                yesornoMsg2.ShowDialog()
            End If
        End If
    End Sub

    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        codbar.Show()
        codbar.PicBarCode.BackgroundImage = Code128(TextBox1.Text, "A")
        codbar.TextBox1.Text = TextBox2.Text
        codbar.TextBox3.Text = TextBox1.Text
    End Sub

    Private Sub IconButton18_Click(sender As Object, e As EventArgs) Handles IconButton18.Click
        Panel10.Visible = True
    End Sub

    Private Sub IconButton19_Click(sender As Object, e As EventArgs) Handles IconButton19.Click
        Panel10.Visible = False
    End Sub

    Private Sub ComboBox9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox9.SelectedIndexChanged
        DataGridView4.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from demarque where year(demarque.date) = '" + ComboBox9.Text + "' and `code` = '" + Label49.Text + "' order by id desc ", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim qteannul, chrgannul, qte1, valr1, qte2, valr2, qte3, valr3, qte4, valr4, qte5, valr5, qte6, valr6, qte7, valr7, qte8, valr8, qte9, valr9, qte10, valr10, qte11, valr11, qte12, valr12 As Double

        DataGridView4.Rows.Clear()
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
        DataGridView4.Rows.Add("Janvier", qte1, valr1)
        DataGridView4.Rows.Add("Février", qte2, valr2)
        DataGridView4.Rows.Add("Mars", qte3, valr3)
        DataGridView4.Rows.Add("Avril", qte4, valr4)
        DataGridView4.Rows.Add("Mai", qte5, valr5)
        DataGridView4.Rows.Add("Juin", qte6, valr6)
        DataGridView4.Rows.Add("Juillet", qte7, valr7)
        DataGridView4.Rows.Add("Aout", qte8, valr8)
        DataGridView4.Rows.Add("Séptembre", qte9, valr9)
        DataGridView4.Rows.Add("Octobre", qte10, valr10)
        DataGridView4.Rows.Add("Novembre", qte11, valr11)
        DataGridView4.Rows.Add("Décembre", qte12, valr12)

        Label44.Text = qteannul.ToString("#0.00")
        Label42.Text = chrgannul.ToString("#0.00 DHs")
    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub



    Private Sub DataGridView3_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellEndEdit
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
            row.Cells(3).Value = row.Cells(5).Value.ToString.Replace(".", ",") / (1 + (row.Cells(4).Value.ToString.Replace(".", ",") / 100))
            row.Cells(14).Value = Convert.ToDouble(((row.Cells(3).Value.ToString.Replace(".", ",") - (row.Cells(3).Value.ToString.Replace(".", ",") * (row.Cells(16).Value.ToString.Replace(".", ",") / 100))) * (row.Cells(9).Value.ToString.Replace(".", ",") - row.Cells(18).Value.ToString.Replace(".", ","))) + (((row.Cells(3).Value.ToString.Replace(".", ",") - (row.Cells(3).Value.ToString.Replace(".", ",") * (row.Cells(16).Value.ToString.Replace(".", ",") / 100))) * (row.Cells(9).Value.ToString.Replace(".", ",") - row.Cells(18).Value.ToString.Replace(".", ","))) * (row.Cells(4).Value.ToString.Replace(".", ",") / 100))).ToString("# ##0.00")

            Dim sum As Double = 0
            Dim ht As Double = 0
            Dim remise As Double = 0
            For i = 0 To DataGridView3.Rows.Count - 1
                sum = sum + DataGridView3.Rows(i).Cells(14).Value.ToString.Replace(".", ",")
                remise = remise + (DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",") * DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",") * (DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ",") / 100))
                ht = (sum / (1 + DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",") / 100)) - remise
            Next

            Label25.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00")
            Label23.Text = Convert.ToDouble(remise.ToString.Replace(".", ",")).ToString("# ##0.00")
            Label18.Text = Convert.ToDouble(ht.ToString.Replace(".", ",")).ToString("# ##0.00")
            TextBox11.Text = Convert.ToDouble(sum.ToString.Replace(".", ","))
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
                    IconButton16.Visible = True
                    IconButton13.Visible = False
                    For i = 0 To table.Rows.Count - 1
                        Label49.Text = table.Rows(i).Item(0)
                        Label53.Text = Convert.ToDateTime(table.Rows(i).Item(20)).ToString("dd/MM/yyyy")
                        Label58.Text = table.Rows(i).Item(21)
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
                        TextBox26.Text = table.Rows(i).Item(25)
                        TextBox18.Text = table.Rows(i).Item(18)
                        TextBox19.Text = table.Rows(i).Item(19)
                        TextBox25.Text = table.Rows(i).Item(23)

                        If TextBox18.Text <> 0 Then
                            CheckBox1.Checked = True
                        Else
                            CheckBox1.Checked = False
                        End If
                        ComboBox1.SelectedItem = table.Rows(i).Item(11)
                        ComboBox2.SelectedItem = table.Rows(i).Item(12)
                        ComboBox3.SelectedItem = table.Rows(i).Item(13)
                        ComboBox4.SelectedItem = table.Rows(i).Item(10)

                        If table.Rows(0).Item(22) = 0 Then
                            IconButton2.Text = "Masquer"
                            IconButton2.IconChar = FontAwesome.Sharp.IconChar.EyeSlash
                            IconButton2.BackColor = Color.RoyalBlue
                        Else
                            IconButton2.Text = "Démasquer"
                            IconButton2.IconChar = FontAwesome.Sharp.IconChar.Eye
                            IconButton2.BackColor = Color.DimGray
                        End If

                        DataGridView2.Rows.Clear()
                        adpt = New MySqlDataAdapter("select * from code_supp where prod_code = '" + TextBox1.Text + "'", conn2)
                        Dim tablesupp2 As New DataTable
                        adpt.Fill(tablesupp2)
                        For j = 0 To tablesupp2.Rows.Count() - 1
                            DataGridView2.Rows.Add(tablesupp2.Rows(j).Item(2))
                        Next
                        DataGridView5.Rows.Clear()

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

                            DataGridView5.Rows.Add(tablehisto.Rows(m).Item(0), tablehisto.Rows(m).Item(2))
                        Next





                        adpt = New MySqlDataAdapter("select * from demarque where year(demarque.date) = '" + ComboBox9.Text + "' and `code` = '" + Label49.Text + "' order by id desc ", conn2)
                        Dim tabledemarque As New DataTable
                        adpt.Fill(tabledemarque)
                        Dim qteannul, chrgannul, qte1, valr1, qte2, valr2, qte3, valr3, qte4, valr4, qte5, valr5, qte6, valr6, qte7, valr7, qte8, valr8, qte9, valr9, qte10, valr10, qte11, valr11, qte12, valr12 As Double

                        DataGridView4.Rows.Clear()
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
                        DataGridView4.Rows.Add("Janvier", qte1, valr1)
                        DataGridView4.Rows.Add("Février", qte2, valr2)
                        DataGridView4.Rows.Add("Mars", qte3, valr3)
                        DataGridView4.Rows.Add("Avril", qte4, valr4)
                        DataGridView4.Rows.Add("Mai", qte5, valr5)
                        DataGridView4.Rows.Add("Juin", qte6, valr6)
                        DataGridView4.Rows.Add("Juillet", qte7, valr7)
                        DataGridView4.Rows.Add("Aout", qte8, valr8)
                        DataGridView4.Rows.Add("Séptembre", qte9, valr9)
                        DataGridView4.Rows.Add("Octobre", qte10, valr10)
                        DataGridView4.Rows.Add("Novembre", qte11, valr11)
                        DataGridView4.Rows.Add("Décembre", qte12, valr12)

                        Label44.Text = qteannul.ToString("#0.00")
                        Label42.Text = chrgannul.ToString("#0.00 DHs")
                        TextBox2.Select()
                        charg = True
                    Next
                Else
                    IconButton16.Visible = False
                    IconButton13.Visible = True
                    Label49.Text = TextBox1.Text

                    TextBox2.Select()

                    TextBox2.Text = ""
                    TextBox3.Text = 0
                    ComboBox6.Text = "0"
                    TextBox7.Text = 0
                    TextBox8.Text = 0
                    TextBox6.Text = 0
                    TextBox5.Text = 0
                    TextBox10.Text = 0
                    TextBox26.Text = 0
                    TextBox9.Text = 0
                    ComboBox3.Text = "U"
                    ComboBox1.Text = ""
                    ComboBox2.Text = ""
                    ComboBox4.Text = ""
                    TextBox13.Text = 0
                    TextBox18.Text = 0
                    TextBox23.Text = ""
                    TextBox22.Text = ""
                    TextBox24.Text = ""
                    If TextBox18.Text <> 0 Then
                        CheckBox1.Checked = True
                    Else
                        CheckBox1.Checked = False
                    End If
                    TextBox19.Text = 0
                    DataGridView4.Rows.Clear()
                    DataGridView1.Rows.Clear()
                    DataGridView2.Rows.Clear()
                    DataGridView5.Rows.Clear()


                    Label44.Text = 0
                    Label42.Text = 0
                    Label58.Text = "-"
                    Label53.Text = "00/00/0000"

                    charg = True
                End If
            Else
                adpt = New MySqlDataAdapter("select * from article where Code = '" + table2.Rows(0).Item(0) + "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    IconButton16.Visible = True
                    IconButton13.Visible = False
                    For i = 0 To table.Rows.Count - 1
                        Label49.Text = table.Rows(i).Item(0)
                        Label53.Text = Convert.ToDateTime(table.Rows(i).Item(20)).ToString("dd/MM/yyyy")
                        Label58.Text = table.Rows(i).Item(21)
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
                        TextBox26.Text = table.Rows(i).Item(25)
                        TextBox18.Text = table.Rows(i).Item(18)
                        TextBox19.Text = table.Rows(i).Item(19)
                        TextBox25.Text = table.Rows(i).Item(23)

                        If TextBox18.Text <> 0 Then
                            CheckBox1.Checked = True
                        Else
                            CheckBox1.Checked = False
                        End If
                        ComboBox1.SelectedItem = table.Rows(i).Item(11)
                        ComboBox2.SelectedItem = table.Rows(i).Item(12)
                        ComboBox3.SelectedItem = table.Rows(i).Item(13)
                        ComboBox4.SelectedItem = table.Rows(i).Item(10)

                        If table.Rows(0).Item(22) = 0 Then
                            IconButton2.Text = "Masquer"
                            IconButton2.IconChar = FontAwesome.Sharp.IconChar.EyeSlash
                            IconButton2.BackColor = Color.RoyalBlue
                        Else
                            IconButton2.Text = "Démasquer"
                            IconButton2.IconChar = FontAwesome.Sharp.IconChar.Eye
                            IconButton2.BackColor = Color.DimGray
                        End If

                        DataGridView2.Rows.Clear()
                        adpt = New MySqlDataAdapter("select * from code_supp where prod_code = '" + TextBox1.Text + "'", conn2)
                        Dim tablesupp2 As New DataTable
                        adpt.Fill(tablesupp2)
                        For j = 0 To tablesupp2.Rows.Count() - 1
                            DataGridView2.Rows.Add(tablesupp2.Rows(j).Item(2))
                        Next

                        ' Requête SQL pour récupérer l'historique d'achat de l'article
                        Dim sql As String = "SELECT achat.Fournisseur, achatdetails.PA_TTC " &
                    "FROM achat " &
                    "INNER JOIN achatdetails ON achat.id = achatdetails.achat_Id " &
                    "WHERE achatdetails.CodeArticle = @idArticle"

                        ' Créer un objet Command avec la requête SQL et la connexion à la base de données
                        Dim cmd As New MySqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@idArticle", table.Rows(i).Item(0))
                        Dim adapter As New MySqlDataAdapter(cmd)
                        Dim tablehisto As New DataTable
                        adapter.Fill(tablehisto)
                        DataGridView5.Rows.Clear()
                        For j = 0 To tablehisto.Rows.Count() - 1
                            DataGridView5.Rows.Add(tablehisto.Rows(j).Item(0), tablehisto.Rows(j).Item(1))
                        Next

                        adpt = New MySqlDataAdapter("select * from demarque where year(demarque.date) = '" + ComboBox9.Text + "' and `code` = '" + Label49.Text + "' order by id desc ", conn2)
                        Dim tabledemarque As New DataTable
                        adpt.Fill(tabledemarque)
                        Dim qteannul, chrgannul, qte1, valr1, qte2, valr2, qte3, valr3, qte4, valr4, qte5, valr5, qte6, valr6, qte7, valr7, qte8, valr8, qte9, valr9, qte10, valr10, qte11, valr11, qte12, valr12 As Double

                        DataGridView4.Rows.Clear()
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
                        DataGridView4.Rows.Add("Janvier", qte1, valr1)
                        DataGridView4.Rows.Add("Février", qte2, valr2)
                        DataGridView4.Rows.Add("Mars", qte3, valr3)
                        DataGridView4.Rows.Add("Avril", qte4, valr4)
                        DataGridView4.Rows.Add("Mai", qte5, valr5)
                        DataGridView4.Rows.Add("Juin", qte6, valr6)
                        DataGridView4.Rows.Add("Juillet", qte7, valr7)
                        DataGridView4.Rows.Add("Aout", qte8, valr8)
                        DataGridView4.Rows.Add("Séptembre", qte9, valr9)
                        DataGridView4.Rows.Add("Octobre", qte10, valr10)
                        DataGridView4.Rows.Add("Novembre", qte11, valr11)
                        DataGridView4.Rows.Add("Décembre", qte12, valr12)

                        Label44.Text = qteannul.ToString("#0.00")
                        Label42.Text = chrgannul.ToString("#0.00 DHs")
                        TextBox2.Select()
                        charg = True
                    Next
                Else
                    IconButton16.Visible = False
                    IconButton13.Visible = True
                    TextBox2.Select()
                    Label49.Text = TextBox1.Text
                    TextBox2.Text = ""
                    TextBox3.Text = 0
                    ComboBox6.Text = "0"
                    TextBox7.Text = 0
                    TextBox8.Text = 0
                    TextBox6.Text = 0
                    TextBox5.Text = 0
                    TextBox10.Text = 0
                    TextBox26.Text = 0
                    TextBox9.Text = 0
                    ComboBox3.Text = "U"
                    ComboBox1.Text = ""
                    ComboBox2.Text = ""
                    ComboBox4.Text = ""
                    TextBox13.Text = 0
                    TextBox18.Text = 0
                    TextBox23.Text = ""
                    TextBox22.Text = ""
                    TextBox24.Text = ""
                    If TextBox18.Text <> 0 Then
                        CheckBox1.Checked = True
                    Else
                        CheckBox1.Checked = False
                    End If
                    TextBox19.Text = 0
                    DataGridView4.Rows.Clear()
                    DataGridView1.Rows.Clear()
                    DataGridView2.Rows.Clear()
                    DataGridView5.Rows.Clear()


                    Label44.Text = 0
                    Label42.Text = 0
                    Label58.Text = "-"
                    Label53.Text = "00/00/0000"


                    charg = True
                End If

            End If

        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        ' Vérifie si la longueur actuelle du texte dépasse la limite (13)
        If TextBox1.Text.Length >= 13 Then
            ' Ignore la touche pressée si la limite est atteinte
            e.Handled = True
        End If
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Masquer un article'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then


                If IconButton2.Text = "Masquer" Then
                    If TextBox1.Text <> "" Then
                        adpt = New MySqlDataAdapter("select * from article where Code = '" + TextBox1.Text + "'", conn2)
                        Dim tablelike As New DataTable
                        adpt.Fill(tablelike)
                        If tablelike.Rows.Count() <> 0 Then
                            Dim yesornoMsg2 As New yesorno
                            yesornoMsg2.Label14.Text = "Voulez vous vraiment masquer cet article ?"
                            yesornoMsg2.ShowDialog()

                            If Msgboxresult = True Then
                                conn2.Close()
                                conn2.Open()
                                cmd2 = New MySqlCommand("UPDATE `article` SET `mask` =  '1' WHERE `Code` = '" + TextBox1.Text + "' ", conn2)
                                cmd2.ExecuteNonQuery()
                                conn2.Close()
                                IconButton2.Text = "Démasquer"
                                IconButton2.IconChar = FontAwesome.Sharp.IconChar.Eye
                                IconButton2.BackColor = Color.DimGray

                            End If
                        End If


                    Else
                        Dim yesornoMsg2 As New yesorno
                        yesornoMsg2.Label14.Text = "Veuillez chercher un Article à masquer !"
                        yesornoMsg2.Panel5.Visible = True
                        yesornoMsg2.ShowDialog()
                    End If
                Else
                    If TextBox1.Text <> "" Then
                        adpt = New MySqlDataAdapter("select * from article where Code = '" + TextBox1.Text + "'", conn2)
                        Dim tablelike As New DataTable
                        adpt.Fill(tablelike)
                        If tablelike.Rows.Count() <> 0 Then
                            Dim yesornoMsg2 As New yesorno
                            yesornoMsg2.Label14.Text = "Voulez vous vraiment afficher cet article ?"
                            yesornoMsg2.ShowDialog()
                            If Msgboxresult = True Then
                                conn2.Close()
                                conn2.Open()
                                cmd2 = New MySqlCommand("UPDATE `article` SET `mask` =  '0' WHERE `Code` = '" + TextBox1.Text + "' ", conn2)
                                cmd2.ExecuteNonQuery()
                                conn2.Close()
                                IconButton2.Text = "Masquer"
                                IconButton2.IconChar = FontAwesome.Sharp.IconChar.EyeSlash
                                IconButton2.BackColor = Color.RoyalBlue
                            End If
                        End If


                    Else
                        Dim yesornoMsg2 As New yesorno
                        yesornoMsg2.Label14.Text = "Veuillez chercher un Article à afficher !"
                        yesornoMsg2.Panel5.Visible = True
                        yesornoMsg2.ShowDialog()
                    End If
                End If
            Else
                Dim yesornoMsg2 As New eror
                yesornoMsg2.Label14.Text = "Vous n'avez pas l'autorisation !"
                yesornoMsg2.Panel5.Visible = True
                yesornoMsg2.ShowDialog()
            End If
        End If
    End Sub

    Private Sub Panel8_Paint(sender As Object, e As PaintEventArgs) Handles Panel8.Paint

    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Panel6.Visible = True Then
                If TextBox2.Text <> "" Then
                    DataGridView1.Rows.Clear()
                    adpt = New MySqlDataAdapter("select Code, Article from article where Article like '%" + TextBox2.Text + "%' order by Article asc", conn2)
                    Dim tablelike As New DataTable
                    adpt.Fill(tablelike)
                    If tablelike.Rows.Count() <> 0 Then
                        For i = 0 To tablelike.Rows.Count() - 1
                            DataGridView1.Rows.Add(tablelike.Rows(i).Item(0), tablelike.Rows(i).Item(1))
                        Next
                    End If
                    DataGridView1.Select()

                End If

            Else

                TextBox3.Select()
            End If
        End If
    End Sub

    Private cursorPosition As Integer = 0

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        ' Sauvegarder la position actuelle du curseur
        cursorPosition = TextBox2.SelectionStart

        Dim inputText As String = TextBox2.Text

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
        RemoveHandler TextBox2.TextChanged, AddressOf TextBox2_TextChanged

        ' Mettez à jour le texte dans le TextBox
        TextBox2.Text = modifiedText

        ' Restaurer la position du curseur
        TextBox2.SelectionStart = cursorPosition

        ' Réactivez le gestionnaire d'événements TextChanged
        AddHandler TextBox2.TextChanged, AddressOf TextBox2_TextChanged
    End Sub

    Private Sub IconButton30_Click(sender As Object, e As EventArgs) Handles IconButton30.Click
        If TextBox2.Text <> "" Then
            Dim nomProduit As String = TextBox2.Text

            ' Divisez la chaîne en mots en utilisant des espaces comme séparateurs
            Dim mots As String() = nomProduit.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

            ' Vérifiez s'il y a au moins un mot
            If mots.Length > 0 Then
                ' Le premier mot est dans mots(0)
                Dim premierMot As String = mots(0)
                Panel16.Visible = True

                adpt = New MySqlDataAdapter("select * from article where Article like '%" + premierMot + "%' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    DataGridView6.Rows.Clear()
                    For i = 0 To table.Rows.Count - 1

                        DataGridView6.Rows.Add(table.Rows(i).Item(1), table.Rows(i).Item(10), Convert.ToDouble(table.Rows(i).Item(4)).ToString("N2"))
                    Next

                End If
            End If
        End If

    End Sub

    Private Sub IconButton20_Click(sender As Object, e As EventArgs) Handles IconButton20.Click
        Panel16.Visible = False
    End Sub

    Private Sub ComboBox6_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox6.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox7.Select()
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim row As DataGridViewRow = DataGridView1.SelectedRows(0)
            TextBox1.Text = row.Cells(0).Value
            adpt = New MySqlDataAdapter("select * from article where Code = '" & TextBox1.Text & "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            Label49.Text = table.Rows(0).Item(0)
            TextBox1.Text = table.Rows(0).Item(0)
            TextBox2.Text = table.Rows(0).Item(1)
            TextBox3.Text = table.Rows(0).Item(2).ToString.Replace(".", ",")
            ComboBox6.Text = table.Rows(0).Item(3).ToString.Replace(",00", "")
            TextBox5.Text = table.Rows(0).Item(7).ToString.Replace(".", ",")
            TextBox6.Text = table.Rows(0).Item(6).ToString.Replace(".", ",")
            TextBox7.Text = table.Rows(0).Item(4).ToString.Replace(".", ",")
            TextBox8.Text = table.Rows(0).Item(5).ToString.Replace(".", ",")
            TextBox9.Text = table.Rows(0).Item(9)
            TextBox10.Text = table.Rows(0).Item(8)
            TextBox26.Text = table.Rows(0).Item(25)
            TextBox18.Text = table.Rows(0).Item(18)
            TextBox19.Text = table.Rows(0).Item(19)
            TextBox25.Text = table.Rows(0).Item(23)

            If table.Rows(0).Item(22) = 0 Then
                IconButton2.Text = "Masquer"
                IconButton2.IconChar = FontAwesome.Sharp.IconChar.EyeSlash
                IconButton2.BackColor = Color.RoyalBlue
            Else
                IconButton2.Text = "Démasquer"
                IconButton2.IconChar = FontAwesome.Sharp.IconChar.Eye
                IconButton2.BackColor = Color.DimGray
            End If
            If TextBox18.Text <> 0 Then
                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If
            ComboBox1.SelectedItem = table.Rows(0).Item(11)
            ComboBox2.SelectedItem = table.Rows(0).Item(12)
            ComboBox3.SelectedItem = table.Rows(0).Item(13)
            ComboBox4.SelectedItem = table.Rows(0).Item(10)

            DataGridView2.Rows.Clear()
            adpt = New MySqlDataAdapter("select * from code_supp where prod_code = '" + TextBox1.Text + "'", conn2)
            Dim tablesupp2 As New DataTable
            adpt.Fill(tablesupp2)
            For j = 0 To tablesupp2.Rows.Count() - 1
                DataGridView2.Rows.Add(tablesupp2.Rows(j).Item(2))
            Next

            adpt = New MySqlDataAdapter("select * from demarque where year(demarque.date) = '" + ComboBox9.Text + "' and `code` = '" + Label49.Text + "' order by id desc ", conn2)
            Dim tabledemarque As New DataTable
            adpt.Fill(tabledemarque)
            Dim qteannul, chrgannul, qte1, valr1, qte2, valr2, qte3, valr3, qte4, valr4, qte5, valr5, qte6, valr6, qte7, valr7, qte8, valr8, qte9, valr9, qte10, valr10, qte11, valr11, qte12, valr12 As Double

            DataGridView4.Rows.Clear()
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
            DataGridView4.Rows.Add("Janvier", qte1, valr1)
            DataGridView4.Rows.Add("Février", qte2, valr2)
            DataGridView4.Rows.Add("Mars", qte3, valr3)
            DataGridView4.Rows.Add("Avril", qte4, valr4)
            DataGridView4.Rows.Add("Mai", qte5, valr5)
            DataGridView4.Rows.Add("Juin", qte6, valr6)
            DataGridView4.Rows.Add("Juillet", qte7, valr7)
            DataGridView4.Rows.Add("Aout", qte8, valr8)
            DataGridView4.Rows.Add("Séptembre", qte9, valr9)
            DataGridView4.Rows.Add("Octobre", qte10, valr10)
            DataGridView4.Rows.Add("Novembre", qte11, valr11)
            DataGridView4.Rows.Add("Décembre", qte12, valr12)

            Label44.Text = qteannul.ToString("#0.00")
            Label42.Text = chrgannul.ToString("#0.00 DHs")

            DataGridView5.Rows.Clear()

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

                DataGridView5.Rows.Add(tablehisto.Rows(m).Item(0), tablehisto.Rows(m).Item(2))
            Next

            charg = True
            Panel6.Visible = False
            TextBox2.Select()
        End If

    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click

    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        If TextBox1.Text = "" Then
            TextBox1.Text = Label49.Text
        End If


    End Sub
End Class