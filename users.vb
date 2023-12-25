Imports System.IO
Imports MySql.Data.MySqlClient
Public Class users
    Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt, adpt2 As MySqlDataAdapter

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
    Private Sub DataGridView2_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView2.CellPainting
        ' Vérifiez si c'est une ligne de données (pas la ligne d'en-tête ni la ligne de total)
        If e.RowIndex >= 0 Then
            ' Supprimez les bordures des cellules sauf pour la dernière ligne
            If e.RowIndex < DataGridView2.Rows.Count - 1 Then
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None
            End If
            e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None
        End If
    End Sub
    Private Sub users_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Dim appPath As String = Application.StartupPath()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = System.IO.Path.Combine(SaveDirectory, imgName)
        If System.IO.File.Exists(SavePath) Then
            PictureBox1.Image = Image.FromFile(SavePath)
        End If

        Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))

        Label2.Text = user
        Dim w = Screen.PrimaryScreen.WorkingArea.Width
        Dim h = My.Computer.Screen.WorkingArea.Size.Height
        Me.Width = w
        Me.Height = h
        Me.Location = New Point(0, 0)

        adpt = New MySqlDataAdapter("select * from users order by id desc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        For i = 0 To table.Rows.Count - 1

            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(7), table.Rows(i).Item(6), Nothing)


        Next

        DataGridView2.Rows.Clear()

        adpt = New MySqlDataAdapter("select * from livreurs order by id desc", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        For i = 0 To table2.Rows.Count - 1

            DataGridView2.Rows.Add(table2.Rows(i).Item(0), table2.Rows(i).Item(1), Nothing)

        Next

        adpt = New MySqlDataAdapter(" SELECT roll FROM `users` WHERE name='" + Label2.Text + "' ", conn2)
        Dim data As New DataTable
        adpt.Fill(data)

        adpt = New MySqlDataAdapter("select * from droits where role = '" & data.Rows(0).Item(0) & "' and droit = 'Gestion des utilisateurs'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then
            Else
                Panel3.Visible = True
                Panel3.BringToFront()
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim asct As Integer

        If CheckBox1.Checked = True Then
            asct = 1
        Else
            asct = 0
        End If
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = " UPDATE `users` Set `name`= @v2,`badgeID`= @v3,`gmail`= @v4,`pass`= @v5,`roll`= @v6,`active`= @v7,`ref`= @v8 WHERE id = " + Label10.Text

        conn2.Close()
        conn2.Open()
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.AddWithValue("@v2", t1.Text)
        cmd2.Parameters.AddWithValue("@v3", t2.Text)
        cmd2.Parameters.AddWithValue("@v4", t3.Text)
        cmd2.Parameters.AddWithValue("@v5", ComboBox1.Text)
        cmd2.Parameters.AddWithValue("@v6", t5.Text)
        cmd2.Parameters.AddWithValue("@v7", asct)
        cmd2.Parameters.AddWithValue("@v8", t6.Text)

        cmd2.ExecuteNonQuery()
        conn2.Close()

        DataGridView1.Rows.Clear()

        adpt = New MySqlDataAdapter("select * from users order by id desc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        For i = 0 To table.Rows.Count - 1

            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(7), table.Rows(i).Item(6), Nothing)

        Next
        MsgBox("Modification bien faite")
    End Sub
    Dim role As String
    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click

        Dim asct As Integer

        If CheckBox1.Checked = True Then
            asct = 1
        Else
            asct = 0
        End If


        conn2.Close()
        conn2.Open()
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = "INSERT INTO `users`(`name`, `badgeID`, `gmail`, `pass`, `roll`,`ref`,`active`) VALUES (@v1,@v2,@v3,@v4,@v5,@v6,@v7)"
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.AddWithValue("@v1", t1.Text)
        cmd2.Parameters.AddWithValue("@v2", t2.Text)
        cmd2.Parameters.AddWithValue("@v3", t3.Text)
        cmd2.Parameters.AddWithValue("@v4", ComboBox1.Text)
        cmd2.Parameters.AddWithValue("@v5", t5.Text)
        cmd2.Parameters.AddWithValue("@v6", t6.Text)
        cmd2.Parameters.AddWithValue("@v7", asct)
        cmd2.ExecuteNonQuery()
        conn2.Close()

        DataGridView1.Rows.Clear()

        adpt = New MySqlDataAdapter("select * from users order by id desc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        For i = 0 To table.Rows.Count - 1

            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(7), table.Rows(i).Item(6), Nothing)

        Next
        t1.Text = ""
        t2.Text = ""
        MsgBox("utilisateur bien ajouté !")
    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Label10.Text = ""


        For Each ctrl As Control In Me.Controls

            If ctrl.GetType.Name = "TextBox" Then
                Dim t As TextBox = ctrl
                t.Clear()
            End If



        Next
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

    Dim ind As String

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 2 Then
                Dim Rep As Integer
                Rep = MsgBox("Voulez-vous vraiment supprimer ce livreur ?", vbYesNo)
                If Rep = vbYes Then

                    Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                    ind = row.Cells(0).Value.ToString
                    conn2.Close()
                    conn2.Open()
                    Dim sql2 As String
                    Dim cmd2 As MySqlCommand
                    sql2 = "delete from livreurs where id = " + ind
                    cmd2 = New MySqlCommand(sql2, conn2)

                    cmd2.ExecuteNonQuery()
                    conn2.Close()

                    DataGridView2.Rows.Clear()

                    adpt = New MySqlDataAdapter("select * from livreurs order by id desc", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    For i = 0 To table.Rows.Count - 1

                        DataGridView2.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), Nothing)

                    Next

                End If

            End If
            If e.ColumnIndex <> 2 Then

                Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                TextBox1.Text = row.Cells(1).Value

                Label12.Text = row.Cells(0).Value


            End If
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 8 Then
                Dim Rep As Integer
                Rep = MsgBox("Voulez-vous vraiment supprimer cet utilisateur ?", vbYesNo)
                If Rep = vbYes Then

                    Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    ind = row.Cells(0).Value.ToString
                    conn2.Close()
                    conn2.Open()
                    Dim sql2 As String
                    Dim cmd2 As MySqlCommand
                    sql2 = "delete from users where id = " + ind
                    cmd2 = New MySqlCommand(sql2, conn2)

                    cmd2.ExecuteNonQuery()
                    conn2.Close()

                    DataGridView1.Rows.Clear()

                    adpt = New MySqlDataAdapter("select * from users order by id desc", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    For i = 0 To table.Rows.Count - 1

                        DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(7), table.Rows(i).Item(6), Nothing)

                    Next

                End If

            End If
            If e.ColumnIndex <> 8 Then

                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                t1.Text = row.Cells(1).Value
                t2.Text = row.Cells(2).Value
                t3.Text = row.Cells(3).Value
                t4.Text = row.Cells(4).Value
                t5.Text = row.Cells(5).Value
                t6.Text = row.Cells(6).Value
                ComboBox1.Text = row.Cells(4).Value
                If row.Cells(7).Value = 1 Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
                Label10.Text = row.Cells(0).Value


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

    Private Sub iconbutton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
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

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub IconPictureBox2_Click(sender As Object, e As EventArgs) Handles IconPictureBox2.Click

    End Sub

    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        conn2.Close()
        conn2.Open()
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = "INSERT INTO `livreurs`(`name`) VALUES (@v1)"
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.AddWithValue("@v1", TextBox1.Text)
        cmd2.ExecuteNonQuery()
        conn2.Close()

        DataGridView2.Rows.Clear()

        adpt = New MySqlDataAdapter("select * from livreurs order by id desc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        For i = 0 To table.Rows.Count - 1

            DataGridView2.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), Nothing)

        Next
        MsgBox("Livreur bien ajouté !")
    End Sub

    Private Sub IconButton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        TextBox1.Text = ""
    End Sub

    Private Sub IconButton16_Click(sender As Object, e As EventArgs) Handles IconButton16.Click
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = " UPDATE `livreurs` Set `name`= @v2 WHERE id = " + Label12.Text
        conn2.Close()
        conn2.Open()
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.AddWithValue("@v2", TextBox1.Text)

        cmd2.ExecuteNonQuery()
        conn2.Close()

        DataGridView2.Rows.Clear()

        adpt = New MySqlDataAdapter("select * from livreurs order by id desc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        For i = 0 To table.Rows.Count - 1

            DataGridView2.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), Nothing)

        Next
        MsgBox("Modification bien faite")
    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Four.Show()
        Me.Close()

    End Sub


End Class