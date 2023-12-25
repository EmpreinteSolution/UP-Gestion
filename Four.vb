Imports System.IO
Imports MySql.Data.MySqlClient
Public Class Four
    Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt, adpt2 As MySqlDataAdapter

    Private Sub dgv_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgv.CellPainting
        ' Vérifiez si c'est une ligne de données (pas la ligne d'en-tête ni la ligne de total)
        If e.RowIndex >= 0 Then
            ' Supprimez les bordures des cellules sauf pour la dernière ligne
            If e.RowIndex < dgv.Rows.Count - 1 Then
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None
            End If
            e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None
        End If
    End Sub
    Private Sub Four_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Dim appPath As String = Application.StartupPath()

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

        adpt = New MySqlDataAdapter("select * from fours order by name asc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        For i = 0 To table.Rows.Count - 1

            dgv.Rows.Add("Frs-" & table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(7), table.Rows(i).Item(0))

        Next
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Modifier Fournisseur'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                If Label10.Text <> "" Then
                    conn2.Close()
                    conn2.Open()
                    Dim sql2 As String
                    Dim cmd2 As MySqlCommand
                    sql2 = "UPDATE `fours` SET `name`= @v2,`compte`= @v3,`telephone`= @v4,`fax`= @v5,`mobile`= @v6,`adresse`= @v7,`ville`= @v8,`email`= @v9,`siteweb`= @v10,`ICE`= @v11,`idFiscal`= @v12,`TP`= @v13 WHERE id = " + Label10.Text
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.Parameters.AddWithValue("@v2", t1.Text.Replace("'", ""))
                    cmd2.Parameters.AddWithValue("@v3", t2.Text)
                    cmd2.Parameters.AddWithValue("@v4", t3.Text)
                    cmd2.Parameters.AddWithValue("@v5", t4.Text)
                    cmd2.Parameters.AddWithValue("@v6", t5.Text)
                    cmd2.Parameters.AddWithValue("@v7", t6.Text)
                    cmd2.Parameters.AddWithValue("@v8", t7.Text)
                    cmd2.Parameters.AddWithValue("@v9", t8.Text)
                    cmd2.Parameters.AddWithValue("@v10", t9.Text)
                    cmd2.Parameters.AddWithValue("@v11", t10.Text)
                    cmd2.Parameters.AddWithValue("@v12", t11.Text)
                    cmd2.Parameters.AddWithValue("@v13", t12.Text)
                    cmd2.ExecuteNonQuery()

                    sql2 = "UPDATE `achat` SET `Fournisseur`= '" & t1.Text.Replace("'", "") & "' WHERE Fournisseur = '" & Label17.Text & "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    sql2 = "UPDATE `bcmnds` SET `name`= '" & t1.Text.Replace("'", "") & "' WHERE name = '" & Label17.Text & "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    sql2 = "UPDATE `achatssauv` SET `Fournisseur`= '" & t1.Text.Replace("'", "") & "' WHERE Fournisseur = '" & Label17.Text & "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    sql2 = "UPDATE `ref_frs` SET `fournisseur`='" & t1.Text.Replace("'", "") & "' WHERE `fournisseur` =  '" & Label17.Text & "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    sql2 = "UPDATE `article` SET `fournisseur`='" & t1.Text.Replace("'", "") & "' WHERE `fournisseur` =  '" & Label17.Text & "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    sql2 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.Parameters.Clear()
                    cmd2.Parameters.AddWithValue("@name", user)
                    cmd2.Parameters.AddWithValue("@op", "Modification du Fournisseur " & t1.Text.Replace("'", ""))
                    cmd2.ExecuteNonQuery()

                    conn2.Close()
                    dgv.Rows.Clear()

                    adpt = New MySqlDataAdapter("select * from fours order by name asc", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    For i = 0 To table.Rows.Count - 1

                        dgv.Rows.Add("Frs-" & table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(7), table.Rows(i).Item(0))
                    Next


                    MsgBox("Modification bien faite")
                    For Each ctrl As Control In Me.Controls

                        If ctrl.GetType.Name = "TextBox" Then
                            Dim t As TextBox = ctrl
                            t.Text = "-"
                        End If

                    Next
                    Label10.Text = ""


                    t1.Text = ""
                    t7.Text = "-"
                    Panel3.Visible = False
                End If
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        For Each ctrl As Control In Me.Controls

            If ctrl.GetType.Name = "TextBox" Then
                Dim t As TextBox = ctrl
                t.Text = "-"
            End If

        Next
        TextBox1.Text = ""
        t1.Text = ""
        Label10.Text = ""
        Panel3.Visible = True
        dgv.Height = 632
        dgv.Top = 117
        dgv.Visible = True
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        If t1.Text <> "" Then

            conn2.Close()
            conn2.Open()

            Dim sql2 As String
            Dim cmd2 As MySqlCommand
            sql2 = "INSERT INTO fours (`name`, `compte`, `telephone`, `fax`, `mobile`, `adresse`, `ville`, `email`, `siteweb`, `ICE`, `idFiscal`, `TP`) VALUES (@v1,@v2,@v3,@v4,@v5,@v6,@v7,@v8,@v9,@v10,@v11,@v12)"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.AddWithValue("@v1", t1.Text.Replace("'", ""))
            cmd2.Parameters.AddWithValue("@v2", t2.Text)
            cmd2.Parameters.AddWithValue("@v3", t3.Text)
            cmd2.Parameters.AddWithValue("@v4", t4.Text)
            cmd2.Parameters.AddWithValue("@v5", t5.Text)
            cmd2.Parameters.AddWithValue("@v6", t6.Text)
            cmd2.Parameters.AddWithValue("@v7", t7.Text)
            cmd2.Parameters.AddWithValue("@v8", t8.Text)
            cmd2.Parameters.AddWithValue("@v9", t9.Text)
            cmd2.Parameters.AddWithValue("@v10", t10.Text)
            cmd2.Parameters.AddWithValue("@v11", t11.Text)
            cmd2.Parameters.AddWithValue("@v12", t12.Text)
            cmd2.ExecuteNonQuery()

            sql2 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            cmd2.Parameters.AddWithValue("@name", user)
            cmd2.Parameters.AddWithValue("@op", "Ajout du Fournisseur " & t1.Text.Replace("'", ""))
            cmd2.ExecuteNonQuery()

            conn2.Close()

            dgv.Rows.Clear()

            adpt = New MySqlDataAdapter("select * from fours order by name asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            For i = 0 To table.Rows.Count - 1

                dgv.Rows.Add("Frs-" & table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(7), table.Rows(i).Item(0))
            Next


            MsgBox("Fournisseur bien ajouté !")
            For Each ctrl As Control In Me.Controls

                If ctrl.GetType.Name = "TextBox" Then
                    Dim t As TextBox = ctrl
                    t.Text = "-"
                End If

            Next
            Label10.Text = ""


            t1.Text = ""
            t7.Text = "-"
            Panel3.Visible = False
        End If

    End Sub
    Dim ind As String
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 13 Then
                Dim Rep As Integer
                Rep = MsgBox("Voulez-vous vraiment supprimer ce fournisseur ?", vbYesNo)
                If Rep = vbYes Then

                    Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)
                    ind = row.Cells(14).Value.ToString
                    conn2.Close()
                    conn2.Open()
                    Dim sql2 As String
                    Dim cmd2 As MySqlCommand
                    sql2 = "delete from fours where id = " + ind
                    cmd2 = New MySqlCommand(sql2, conn2)

                    cmd2.ExecuteNonQuery()

                    sql2 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.Parameters.Clear()
                    cmd2.Parameters.AddWithValue("@name", user)
                    cmd2.Parameters.AddWithValue("@op", "Suppression du Fournisseur " & row.Cells(1).Value.ToString)
                    cmd2.ExecuteNonQuery()

                    conn2.Close()
                    dgv.Rows.Clear()

                    adpt = New MySqlDataAdapter("select * from fours order by name asc", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    For i = 0 To table.Rows.Count - 1

                        dgv.Rows.Add("Frs-" & table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(6), table.Rows(i).Item(7), table.Rows(i).Item(8), table.Rows(i).Item(9), table.Rows(i).Item(10), table.Rows(i).Item(11), table.Rows(i).Item(12), Nothing, table.Rows(i).Item(0))
                    Next


                End If

            End If

            If e.ColumnIndex <> 13 Then

                Panel3.Visible = False
                dgv.Height = 392
                dgv.Top = 357
                dgv.Visible = False

                Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)

                Label10.Text = row.Cells(14).Value

                t1.Text = row.Cells(1).Value
                t2.Text = row.Cells(2).Value
                t3.Text = row.Cells(3).Value
                t4.Text = row.Cells(4).Value
                t5.Text = row.Cells(5).Value
                t6.Text = row.Cells(6).Value
                t7.Text = row.Cells(7).Value
                t8.Text = row.Cells(8).Value
                t9.Text = row.Cells(9).Value
                t10.Text = row.Cells(10).Value
                t11.Text = row.Cells(11).Value
                t12.Text = row.Cells(12).Value
            End If
        End If
    End Sub

    Private Sub IconButton8_Click(sender As Object, e As EventArgs) Handles IconButton8.Click
        Dim Rep As Integer
        Rep = MsgBox("Voulez-vous vraiment quitter ?", vbYesNo)
        If Rep = vbYes Then
            Dim log = New Form1()
            log.Show()
            Me.Close()
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "" Then
                adpt = New MySqlDataAdapter("select * from fours order by name asc", conn2)
                Dim table As New DataTable
                adpt.Fill(table)

                dgv.Rows.Clear()
                For i = 0 To table.Rows.Count - 1

                    dgv.Rows.Add("Frs-" & table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(7), table.Rows(i).Item(0))
                Next


            Else
                adpt = New MySqlDataAdapter("select * from fours where name like '%" + TextBox1.Text.Replace("'", " ") + "%' order by name asc", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count = 0 Then
                    MsgBox("Aucun fournisseur trouvé")

                Else

                    dgv.Rows.Clear()
                    For i = 0 To table.Rows.Count - 1

                        dgv.Rows.Add("Frs-" & table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(7), table.Rows(i).Item(0))
                    Next

                End If
            End If



        End If
    End Sub


    Private Sub iconbutton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        stock.Show()
        Me.Close()
    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        dashboard.Show()
        dashboard.IconButton9.Visible = True
        dashboard.IconButton8.Visible = False
    End Sub
    Private Sub iconbutton4_Click_1(sender As Object, e As EventArgs) Handles IconButton4.Click
        product.Show()
        Me.Close()
    End Sub

    Private Sub iconbutton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
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
    Private Sub IconButton18_Click(sender As Object, e As EventArgs) Handles IconButton18.Click
        factures.Show()
        Me.Close()
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        archive.Show()
        Me.Close()

    End Sub

    Private Sub IconButton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
        Home.Show()
        Me.Close()
    End Sub

    Private Sub IconButton14_Click(sender As Object, e As EventArgs)
        If Panel3.Visible = True Then
            Panel3.Visible = False
            dgv.Height = 392
            dgv.Top = 357
            dgv.Visible = False
        Else
            Panel3.Visible = True
            dgv.Height = 632
            dgv.Top = 117
            dgv.Visible = True
        End If

    End Sub

    Private Sub IconButton14_Click_1(sender As Object, e As EventArgs) Handles IconButton14.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Ajouter Fournisseur'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Label10.Text = ""
                t1.Text = ""
                t7.Text = "-"
                Panel3.Visible = True
                Panel3.BringToFront()
                t1.Select()
                IconButton3.Visible = True
                IconButton17.Visible = False
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton16_Click(sender As Object, e As EventArgs) Handles IconButton16.Click
        Label10.Text = ""


        t1.Text = ""
        t7.Text = "-"

        Panel3.Visible = False
    End Sub

    Private Sub IconButton30_Click(sender As Object, e As EventArgs) Handles IconButton30.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Supprimer Fournisseur'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Dim cmd As MySqlCommand

                If dgv.SelectedRows.Count <> 0 Then
                    For i = 0 To dgv.SelectedRows.Count - 1


                        Dim Rep As Integer
                        Rep = MsgBox("Voulez vous vraiment supprimer ce fournisseur ?", vbYesNo)
                        If Rep = vbYes Then
                            conn2.Open()
                            cmd = New MySqlCommand("DELETE FROM `fours` WHERE id = '" & dgv.SelectedRows(i).Cells(3).Value & "'", conn2)
                            cmd.ExecuteNonQuery()
                            conn2.Close()
                            MsgBox("Opération bien faite !")
                            dgv.Rows.RemoveAt(dgv.SelectedRows(i).Index)
                        End If

                    Next
                End If
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        users.Show()
        Me.Close()

    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        Panel3.Visible = True
        Panel3.BringToFront()
        t1.Select()
        IconButton3.Visible = False
        IconButton17.Visible = True

        Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)
        t1.Text = row.Cells(1).Value
        Label17.Text = row.Cells(1).Value

        t7.Text = row.Cells(2).Value


        Label10.Text = row.Cells(3).Value


    End Sub
End Class