Imports System.IO
Imports MySql.Data.MySqlClient
Public Class product
    Dim adpt, adpt2 As MySqlDataAdapter

    Private Sub IconButton8_Click(sender As Object, e As EventArgs) Handles IconButton8.Click
        Dim Rep As Integer
        Rep = MsgBox("Voulez-vous vraiment quitter ?", vbYesNo)
        If Rep = vbYes Then
            Dim log = New Form1()
            log.Show()
            Me.Close()
        End If
    End Sub
    Dim imgName As String = "default.png"
    Dim imgName2 As String = "default.png"
    Dim ind As String
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DatagridView1.CellDoubleClick
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Modifier Famille / Sous-Famille'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                If e.RowIndex >= 0 Then
                    Panel4.Visible = True
                    Panel4.BringToFront()
                    TextBox1.Select()
                    IconButton12.Visible = False
                    IconButton3.Visible = True

                    Dim row As DataGridViewRow = DatagridView1.Rows(e.RowIndex)
                    TextBox1.Text = row.Cells(0).Value



                    Label10.Text = row.Cells(1).Value
                End If
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 3 Then
                Dim Rep As Integer
                Rep = MsgBox("Voulez-vous vraiment supprimer cette sous_famille ?", vbYesNo)
                If Rep = vbYes Then

                    Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                    ind = row.Cells(0).Value.ToString
                    conn2.Close()
                    conn2.Open()
                    Dim sql2 As String
                    Dim cmd2 As MySqlCommand
                    sql2 = "delete from sous_famille where idSFamille = " + ind
                    cmd2 = New MySqlCommand(sql2, conn2)

                    cmd2.ExecuteNonQuery()
                    conn2.Close()

                    DataGridView2.Rows.Clear()
                    adpt = New MySqlDataAdapter("select * from sous_famille order by nomSfamille", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    For i = 0 To table.Rows.Count - 1

                        DataGridView2.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), "")

                    Next
                    MsgBox("Sous famille supprimée !")
                End If


            End If
            If e.ColumnIndex <> 3 Then

                Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                TextBox2.Text = row.Cells(2).Value
                ComboBox1.SelectedItem = row.Cells(1).Value
                Label3.Text = row.Cells(0).Value
            End If
        End If
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        conn2.Close()
        conn2.Open()
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = "UPDATE `famille` SET `nomFamille`='" & TextBox1.Text & "', `img` = '" & imgName & "', `caisse`='" & ComboBox2.Text & "' WHERE idFamille = " + Label10.Text.ToString
        cmd2 = New MySqlCommand(sql2, conn2)

        cmd2.ExecuteNonQuery()
        conn2.Close()



        DataGridView1.Rows.Clear()
        ComboBox1.Items.Clear()

        adpt = New MySqlDataAdapter("select * from famille order by nomFamille", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        For i = 0 To table.Rows.Count - 1
            ComboBox1.Items.Add(table.Rows(i).Item(1))
            DatagridView1.Rows.Add(table.Rows(i).Item(1), table.Rows(i).Item(0))

        Next
        MsgBox("Modification bien faite")
        Label10.Text = ""


        TextBox1.Text = ""
        Panel4.Visible = False
    End Sub


    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click


        adpt2 = New MySqlDataAdapter("select * from famille order by idFamille desc", conn2)
        Dim table2 As New DataTable
        adpt2.Fill(table2)
        Dim ida As Double
        If (table2.Rows.Count = 0) Then
            ida = 1
        Else
            ida = table2.Rows(0).Item(0) + 1
        End If
        conn2.Close()
        conn2.Open()
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = "INSERT INTO famille (nomFamille, idFamille,img,caisse) VALUES ('" & TextBox1.Text & "','" + ida.ToString + "','" & imgName & "','" & ComboBox2.Text & "')"
        cmd2 = New MySqlCommand(sql2, conn2)

        cmd2.ExecuteNonQuery()
        conn2.Close()

        DataGridView1.Rows.Clear()
        ComboBox1.Items.Clear()

        adpt = New MySqlDataAdapter("select * from famille order by nomFamille", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        For i = 0 To table.Rows.Count - 1
            ComboBox1.Items.Add(table.Rows(i).Item(1))
            DatagridView1.Rows.Add(table.Rows(i).Item(1), table.Rows(i).Item(0))

        Next
        MsgBox("Famille bien ajoutée !")

        Label10.Text = ""


        TextBox1.Text = ""
        Panel4.Visible = False

    End Sub


    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        conn2.Close()
        conn2.Open()

        adpt2 = New MySqlDataAdapter("select * from sous_famille order by idSFamille desc", conn2)
        Dim table3 As New DataTable
        adpt2.Fill(table3)
        Dim ida As Double
        If (table3.Rows.Count = 0) Then
            ida = 1
        Else
            ida = table3.Rows(0).Item(0) + 1
        End If

        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = "INSERT INTO sous_famille (idFamille, nomSfamille, idSFamille) VALUES ((SELECT `idFamille` FROM famille WHERE famille.nomFamille ='" + ComboBox1.Text + "' ) ,'" & TextBox2.Text & "','" + ida.ToString + "')"
        cmd2 = New MySqlCommand(sql2, conn2)

        cmd2.ExecuteNonQuery()
        conn2.Close()

        adpt = New MySqlDataAdapter("select * from sous_famille where idFamille = '" & DatagridView1.SelectedRows(0).Cells(1).Value & "' order by nomSfamille", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        DataGridView2.Rows.Clear()
        For i = 0 To table.Rows.Count - 1
            DataGridView2.Rows.Add(DatagridView1.SelectedRows(0).Cells(0).Value, table.Rows(i).Item(2), table.Rows(i).Item(0))

        Next

        MsgBox("Sous famille bien ajoutée !")
        Label3.Text = ""


        TextBox2.Text = ""
        Panel9.Visible = False
    End Sub

    Private Sub IconButton2_Click_1(sender As Object, e As EventArgs) Handles IconButton2.Click
        conn2.Close()
        conn2.Open()
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = "UPDATE `sous_famille` SET `nomSfamille`='" & TextBox2.Text & "',`idFamille`= (SELECT famille.idFamille FROM famille WHERE famille.nomFamille ='" + ComboBox1.Text + "' )  WHERE idSFamille = " + Label3.Text.ToString
        cmd2 = New MySqlCommand(sql2, conn2)

        cmd2.ExecuteNonQuery()
        conn2.Close()


        adpt = New MySqlDataAdapter("select * from sous_famille where idFamille = '" & DatagridView1.SelectedRows(0).Cells(1).Value & "' order by nomSfamille", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        DataGridView2.Rows.Clear()
        For i = 0 To table.Rows.Count - 1
            DataGridView2.Rows.Add(DatagridView1.SelectedRows(0).Cells(0).Value, table.Rows(i).Item(2), table.Rows(i).Item(0))

        Next

        MsgBox("Modification bien faite")
        Label3.Text = ""


        TextBox2.Text = ""
        Panel9.Visible = False
    End Sub

    Private Sub IconButton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        Home.Show()
        Me.Close()
    End Sub

    Private Sub IconButton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
        stock.Show()
        Me.Close()
    End Sub

    Private Sub IconButton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        dashboard.Show()
        dashboard.IconButton9.Visible = True
        dashboard.IconButton8.Visible = False
    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        archive.Show()
        Me.Close()
    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Four.Show()
        Me.Close()
    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        users.Show()
        Me.Close()
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click

    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Client.Show()
        Me.Close()

    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click
        achats.Show()
        Me.Close()

    End Sub

    Private Sub IconButton18_Click(sender As Object, e As EventArgs) Handles IconButton18.Click
        factures.Show()
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

    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        Dim opf As New OpenFileDialog
        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then

            PictureBox3.Image = Image.FromFile(opf.FileName)
            imgName = Path.GetFileName(opf.FileName)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
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
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Dim inputText As String = TextBox2.Text

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
            RemoveHandler TextBox2.TextChanged, AddressOf TextBox2_TextChanged

            ' Mettez à jour le texte dans le TextBox
            TextBox2.Text = modifiedText

            ' Replacez le curseur à la position correcte après la modification
            TextBox2.SelectionStart = TextBox2.Text.Length

            ' Réactivez le gestionnaire d'événements TextChanged
            AddHandler TextBox2.TextChanged, AddressOf TextBox2_TextChanged
        End If
    End Sub

    Private Sub IconButton19_Click(sender As Object, e As EventArgs) Handles IconButton19.Click

        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Ajouter Famille / Sous-Famille'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then
                Label10.Text = ""
                TextBox1.Text = ""
                Panel4.Visible = True
                Panel4.BringToFront()
                TextBox1.Select()
                IconButton12.Visible = True
                IconButton3.Visible = False
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If


    End Sub

    Private Sub IconButton16_Click(sender As Object, e As EventArgs) Handles IconButton16.Click
        Label10.Text = ""


        TextBox1.Text = ""
        Panel4.Visible = False
    End Sub

    Private Sub IconButton30_Click(sender As Object, e As EventArgs) Handles IconButton30.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Supprimer Famille / Sous-Famille'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Dim cmd As MySqlCommand

                If DatagridView1.SelectedRows.Count <> 0 Then
                    For i = 0 To DatagridView1.SelectedRows.Count - 1


                        Dim Rep As Integer
                        Rep = MsgBox("Voulez vous vraiment supprimer cette famille ?", vbYesNo)
                        If Rep = vbYes Then
                            conn2.Open()
                            cmd = New MySqlCommand("DELETE FROM `famille` WHERE idFamille = '" & DatagridView1.SelectedRows(i).Cells(1).Value & "'", conn2)
                            cmd.ExecuteNonQuery()
                            conn2.Close()
                            MsgBox("Opération bien faite !")
                            DatagridView1.Rows.RemoveAt(DatagridView1.SelectedRows(i).Index)
                        End If

                    Next
                End If
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DatagridView1.CellPainting
        ' Vérifiez si c'est une ligne de données (pas la ligne d'en-tête ni la ligne de total)
        If e.RowIndex >= 0 Then
            ' Supprimez les bordures des cellules sauf pour la dernière ligne
            If e.RowIndex < DatagridView1.Rows.Count - 1 Then
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
    Private Sub product_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
        adpt = New MySqlDataAdapter("select * from famille order by nomFamille", conn2)
        Dim table As New DataTable
        adpt.Fill(table)


        For i = 0 To table.Rows.Count - 1
            ComboBox1.Items.Add(table.Rows(i).Item(1))

            DatagridView1.Rows.Add(table.Rows(i).Item(1), table.Rows(i).Item(0))

        Next

    End Sub

    Private Sub IconButton22_Click(sender As Object, e As EventArgs) Handles IconButton22.Click

        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Ajouter Famille / Sous-Famille'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then
                Label3.Text = ""
                TextBox2.Text = ""
                Panel9.Visible = True
                Panel9.BringToFront()
                TextBox2.Select()
                IconButton1.Visible = True
                IconButton2.Visible = False
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If


    End Sub

    Private Sub IconButton21_Click(sender As Object, e As EventArgs) Handles IconButton21.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Supprimer Famille / Sous-Famille'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Dim cmd As MySqlCommand

                If DataGridView2.SelectedRows.Count <> 0 Then
                    For i = 0 To DataGridView2.SelectedRows.Count - 1


                        Dim Rep As Integer
                        Rep = MsgBox("Voulez vous vraiment supprimer cette sous-famille ?", vbYesNo)
                        If Rep = vbYes Then
                            conn2.Open()
                            cmd = New MySqlCommand("DELETE FROM `sous_famille` WHERE idSFamille = '" & DataGridView2.SelectedRows(i).Cells(2).Value & "'", conn2)
                            cmd.ExecuteNonQuery()
                            conn2.Close()
                            MsgBox("Opération bien faite !")
                            DataGridView2.Rows.RemoveAt(DataGridView2.SelectedRows(i).Index)
                        End If

                    Next
                End If
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton20_Click(sender As Object, e As EventArgs) Handles IconButton20.Click
        Label3.Text = ""


        TextBox2.Text = ""
        Panel9.Visible = False
    End Sub

    Private Sub DatagridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DatagridView1.CellClick
        adpt = New MySqlDataAdapter("select * from sous_famille where idFamille = '" & DatagridView1.Rows(e.RowIndex).Cells(1).Value & "' order by nomSfamille", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        DataGridView2.Rows.Clear()
        For i = 0 To table.Rows.Count - 1
            DataGridView2.Rows.Add(DatagridView1.Rows(e.RowIndex).Cells(0).Value, table.Rows(i).Item(2), table.Rows(i).Item(0))

        Next
    End Sub

    Private Sub DataGridView2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Modifier Famille / Sous-Famille'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                If e.RowIndex >= 0 Then
                    Panel9.Visible = True
                    Panel9.BringToFront()
                    TextBox2.Select()
                    IconButton1.Visible = False
                    IconButton2.Visible = True

                    Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                    TextBox2.Text = row.Cells(1).Value



                    Label3.Text = row.Cells(2).Value
                End If
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub
End Class
