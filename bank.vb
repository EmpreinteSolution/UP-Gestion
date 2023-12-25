Imports System.IO
Imports MySql.Data.MySqlClient

Public Class bank
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Dim sql2, sql3 As String

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        conn2.Close()
        conn2.Open()
        If CheckBox1.Checked = False Then
            sql3 = "INSERT INTO banques (`organisme`, `agence`, `compte`,`tpe`) 
                    VALUES ('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','0')"
        Else
            sql2 = "UPDATE `banques` SET `tpe`=0"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            cmd2.ExecuteNonQuery()

            sql3 = "INSERT INTO banques (`organisme`, `agence`, `compte`,`tpe`) 
                    VALUES ('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','1')"
        End If
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.Parameters.Clear()
        cmd3.ExecuteNonQuery()

        sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.Parameters.Clear()
        If role = "Caissier" Then
            cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
        Else
            cmd3.Parameters.AddWithValue("@name", Home.Label2.Text)
        End If
        cmd3.Parameters.AddWithValue("@op", "Ajout de la banque : " & TextBox1.Text.Replace("'", " "))
        cmd3.ExecuteNonQuery()

        conn2.Close()
        DataGridView2.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim tpe As String
        For i = 0 To table.Rows.Count() - 1
            If table.Rows(i).Item(4) = 0 Then
                tpe = "Non"
            End If
            If table.Rows(i).Item(4) = 1 Then
                tpe = "Oui"
            End If
            DataGridView2.Rows.Add(table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), "", table.Rows(i).Item(0), tpe)
        Next
        CheckBox1.Checked = False
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Close()
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Panel6.Visible = True
        DataGridView1.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from caisses", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        For i = 0 To table.Rows.Count() - 1

            DataGridView1.Rows.Add(table.Rows(i).Item(1), "", table.Rows(i).Item(0))
        Next

        TextBox4.Text = ""
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Panel6.Visible = False
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        conn2.Close()
        conn2.Open()

        sql3 = "INSERT INTO caisses (`name`) 
                    VALUES ('" + TextBox4.Text + "')"

        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.Parameters.Clear()
        cmd3.ExecuteNonQuery()

        conn2.Close()
        DataGridView1.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from caisses", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        For i = 0 To table.Rows.Count() - 1

            DataGridView1.Rows.Add(table.Rows(i).Item(1), "", table.Rows(i).Item(0))
        Next

        TextBox4.Text = ""
    End Sub

    Dim cmd2, cmd3 As MySqlCommand

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
    Private Sub bank_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim tpe As String
        DataGridView2.Rows.Clear()
        For i = 0 To table.Rows.Count() - 1
            If table.Rows(i).Item(4) = 0 Then
                tpe = "Non"
            End If
            If table.Rows(i).Item(4) = 1 Then
                tpe = "Oui"
            End If
            DataGridView2.Rows.Add(table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), "", table.Rows(i).Item(0), tpe)
        Next
    End Sub

    Private Sub DataGridView2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 3 Then
                Dim Rep As Integer
                Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                Rep = MsgBox("Voulez-vous vraiment supprimer cette banque ?", vbYesNo)
                If Rep = vbYes Then

                    conn2.Close()
                    conn2.Open()
                    Dim sql2, sql3 As String
                    Dim cmd2, cmd3 As MySqlCommand
                    sql2 = "delete from banques where id = '" + row.Cells(4).Value.ToString + "' "
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@name", Home.Label2.Text)
                    cmd3.Parameters.AddWithValue("@op", "Suppression de banque " & row.Cells(0).Value.ToString)
                    cmd3.ExecuteNonQuery()

                    sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    If role = "Caissier" Then
                        cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
                    Else
                        cmd3.Parameters.AddWithValue("@name", Home.Label2.Text)
                    End If
                    cmd3.Parameters.AddWithValue("@op", "Suppression de la banque : " & row.Cells(0).Value.ToString)
                    cmd3.ExecuteNonQuery()

                    conn2.Close()

                    DataGridView2.Rows.Clear()
                    adpt = New MySqlDataAdapter("select * from banques", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    Dim tpe As String
                    For i = 0 To table.Rows.Count() - 1
                        If table.Rows(i).Item(4) = 0 Then
                            tpe = "Non"
                        End If
                        If table.Rows(i).Item(4) = 1 Then
                            tpe = "Oui"
                        End If
                        DataGridView2.Rows.Add(table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), "", table.Rows(i).Item(0), tpe)
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit

        ' Obtenez les informations de la ligne éditée
        Dim rowIndex As Integer = e.RowIndex

        ' Vérifiez si la ligne est valide (elle peut être invalide si l'utilisateur annule la modification)
        If rowIndex >= 0 And DataGridView2.IsCurrentRowDirty Then
            adpt = New MySqlDataAdapter("select organisme from banques where id = '" & DataGridView2.Rows(rowIndex).Cells(4).Value & "'", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            Dim bqo As String = table.Rows(0).Item(0)
            ' Récupérez les valeurs modifiées
            Dim orga As String = DataGridView2.Rows(rowIndex).Cells(0).Value
            Dim agenc As String = DataGridView2.Rows(rowIndex).Cells(1).Value
            Dim numcompte As String = DataGridView2.Rows(rowIndex).Cells(2).Value


            conn2.Open()
            sql2 = "UPDATE `banques` SET `organisme`= '" & orga & "', `agence` = '" & agenc & "', `compte` = '" & numcompte & "' where id = '" & DataGridView2.Rows(rowIndex).Cells(4).Value & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            cmd2.ExecuteNonQuery()

            sql2 = "UPDATE `cheques` SET `bq`= '" & orga & "' where bq = '" & bqo & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            cmd2.ExecuteNonQuery()

            sql2 = "UPDATE `caisse` SET `Compte`= '" & orga & "' where Compte = '" & bqo & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            cmd2.ExecuteNonQuery()

            conn2.Close()


        End If
    End Sub

    Private Sub datagridview1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 1 Then
                Dim Rep As Integer
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Rep = MsgBox("Voulez-vous vraiment supprimer cette caisse ?", vbYesNo)
                If Rep = vbYes Then

                    conn2.Close()
                    conn2.Open()
                    Dim sql2, sql3 As String
                    Dim cmd2, cmd3 As MySqlCommand
                    sql2 = "delete from caisses where id = '" + row.Cells(2).Value.ToString + "' "
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    conn2.Close()

                    DataGridView1.Rows.Clear()
                    adpt = New MySqlDataAdapter("select * from caisses", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)

                    For i = 0 To table.Rows.Count() - 1

                        DataGridView1.Rows.Add(table.Rows(i).Item(1), "", table.Rows(i).Item(0))
                    Next

                    TextBox4.Text = ""
                End If
            End If
        End If
    End Sub
End Class