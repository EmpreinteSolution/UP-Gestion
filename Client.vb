Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Public Class Client
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

    Private Sub TextBox24_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox24.KeyDown

        If e.KeyCode = Keys.Enter Then
            For i = DataGridView3.Rows.Count - 1 To 0 Step -1
                If DataGridView3.Rows(i).Cells(0).Value = False Then
                    DataGridView3.Rows.RemoveAt(i)
                End If
            Next
            If TextBox24.Text <> "" Then

                Dim code As String = TextBox24.Text

                If Not String.IsNullOrEmpty(code) Then

                    adpt = New MySqlDataAdapter("select prod_code from code_supp where code_supp = '" & TextBox24.Text & "' ", conn2)
                    Dim table2 As New DataTable
                    adpt.Fill(table2)
                    If table2.Rows.Count() = 0 Then
                        adpt = New MySqlDataAdapter("select Code, Article, PV_TTC,PA_TTC,id from article where Code = '" & TextBox24.Text & "' ", conn2)
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
                            Dim ord As New DataGridViewTextBoxCell()
                            Dim ref As New DataGridViewCheckBoxCell()

                            ' Affecter les valeurs des cellules
                            ref.Value = False
                            first.Value = tablelike.Rows(0).Item(0)
                            second.Value = tablelike.Rows(0).Item(1)
                            tree.Value = tablelike.Rows(0).Item(2)
                            qte.Value = tablelike.Rows(0).Item(3)
                            ord.Value = tablelike.Rows(0).Item(4)

                            ' Ajouter les cellules à la ligne

                            row.Cells.Add(ref)
                            row.Cells.Add(first)
                            row.Cells.Add(second)
                            row.Cells.Add(tree)
                            row.Cells.Add(qte)
                            row.Cells.Add(ord)


                            rowsToAdd.Add(row)
                            DataGridView3.Rows.AddRange(rowsToAdd.ToArray())
                            DataGridView3.ClearSelection()

                        Else

                        End If
                    Else
                        adpt = New MySqlDataAdapter("select Code, Article, PV_TTC,PA_TTC, id from article where Code = '" + table2.Rows(0).Item(0) + "' ", conn2)
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
                                Dim ord As New DataGridViewTextBoxCell()
                                Dim ref As New DataGridViewCheckBoxCell()

                                ' Affecter les valeurs des cellules
                                first.Value = tablelike.Rows(0).Item(0)
                                second.Value = tablelike.Rows(0).Item(1)
                                tree.Value = tablelike.Rows(0).Item(2)
                                qte.Value = tablelike.Rows(0).Item(3)
                                ord.Value = tablelike.Rows(0).Item(4)
                                ref.Value = False



                                ' Ajouter les cellules à la ligne
                                row.Cells.Add(ref)
                                row.Cells.Add(first)
                                row.Cells.Add(second)
                                row.Cells.Add(tree)
                                row.Cells.Add(qte)
                                row.Cells.Add(ord)


                                rowsToAdd.Add(row)

                                DataGridView3.Rows.AddRange(rowsToAdd.ToArray())
                            Next

                            DataGridView3.ClearSelection()
                        End If

                    End If

                End If

            End If
            TextBox24.Text = ""
        End If
    End Sub

    Private Sub TextBox23_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox23.KeyDown

        If e.KeyCode = Keys.Enter Then
            For i = DataGridView3.Rows.Count - 1 To 0 Step -1
                If DataGridView3.Rows(i).Cells(0).Value = False Then
                    DataGridView3.Rows.RemoveAt(i)
                End If
            Next

            If TextBox23.Text <> "" Then

                adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_TTC, id from article where Article like '%" & TextBox23.Text & "%' order by Article asc", conn2)
                Dim tablelike As New DataTable
                adpt.Fill(tablelike)

                Dim rowsToAdd As New List(Of DataGridViewRow)
                If tablelike.Rows.Count <> 0 Then

                    For i = 0 To tablelike.Rows.Count - 1
                        Dim row As New DataGridViewRow()

                        Dim first As New DataGridViewTextBoxCell()
                        Dim second As New DataGridViewTextBoxCell()
                        Dim tree As New DataGridViewTextBoxCell()
                        Dim qte As New DataGridViewTextBoxCell()
                        Dim ord As New DataGridViewTextBoxCell()
                        Dim ref As New DataGridViewCheckBoxCell()


                        ' Affecter les valeurs des cellules
                        first.Value = tablelike.Rows(i).Item(0)
                        second.Value = tablelike.Rows(i).Item(1)
                        tree.Value = tablelike.Rows(i).Item(2)
                        qte.Value = tablelike.Rows(i).Item(3)
                        ord.Value = tablelike.Rows(i).Item(4)
                        ref.Value = False

                        ' Ajouter les cellules à la ligne
                        row.Cells.Add(ref)
                        row.Cells.Add(first)
                        row.Cells.Add(second)
                        row.Cells.Add(tree)
                        row.Cells.Add(qte)
                        row.Cells.Add(ord)



                        rowsToAdd.Add(row)
                    Next
                    DataGridView3.Rows.AddRange(rowsToAdd.ToArray())
                    DataGridView3.Select()
                    If DataGridView3.Rows.Count <> 0 Then
                        DataGridView3.BeginEdit(True)

                    End If
                Else
                    TextBox23.Text = ""
                    TextBox23.Select()
                End If
            Else
                TextBox23.Text = ""
                TextBox23.Select()

            End If


        End If
    End Sub

    Private Sub Client_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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

        adpt = New MySqlDataAdapter("select * from clients order by client asc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        If table.Rows.Count <> 0 Then

            For i = 0 To table.Rows.Count - 1


                DataGridView1.Rows.Add("Clt-" & table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(12), table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(14).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(18))

            Next
        End If
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click

        If Label10.Text <> "" Then
            conn2.Close()
            conn2.Open()
            Dim sql2 As String
            Dim cmd2 As MySqlCommand
            sql2 = "UPDATE `clients` SET `client` = @v1,`telephone`= @v3,`adresse`= @v6,`ville`= @v7,`ICE`= @v12,`credit`= @v13,`maxCredit`= @v14,`note` = @v11, `fac` = @v2 WHERE id = " + Label10.Text
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.AddWithValue("@v1", t1.Text.Replace("'", ""))
            cmd2.Parameters.AddWithValue("@v3", t3.Text)
            cmd2.Parameters.AddWithValue("@v6", t6.Text)
            cmd2.Parameters.AddWithValue("@v7", t7.Text)
            cmd2.Parameters.AddWithValue("@v12", t12.Text)
            cmd2.Parameters.AddWithValue("@v13", Convert.ToDouble(t13.Text.ToString.Replace(".", ",")))
            cmd2.Parameters.AddWithValue("@v14", Convert.ToDouble(t14.Text.ToString.Replace(".", ",")))
            cmd2.Parameters.AddWithValue("@v11", t11.Text)
            cmd2.Parameters.AddWithValue("@v2", TextBox2.Text)
            cmd2.ExecuteNonQuery()

            sql2 = "UPDATE `devis` SET `Fournisseur`= '" & t1.Text.Replace("'", "") & "' WHERE Fournisseur = '" & Label24.Text & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()

            sql2 = "UPDATE `facture` SET `client`='" & t1.Text.Replace("'", "") & "' WHERE `client` =  '" & Label24.Text & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()

            sql2 = "UPDATE `orders` SET `client`='" & t1.Text.Replace("'", "") & "' WHERE `client` =  '" & Label24.Text & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()

            sql2 = "UPDATE `orderssauv` SET `client`='" & t1.Text.Replace("'", "") & "' WHERE `client` =  '" & Label24.Text & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()

            sql2 = "UPDATE `client_price` SET `client`='" & t1.Text.Replace("'", "") & "' WHERE `client` =  '" & Label24.Text & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()


            sql2 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            If role = "Caissier" Then
                cmd2.Parameters.AddWithValue("@name", dashboard.Label2.Text)
            Else
                cmd2.Parameters.AddWithValue("@name", Label2.Text)
            End If
            cmd2.Parameters.AddWithValue("@op", "Modification du client : " & t1.Text.Replace("'", ""))
            cmd2.ExecuteNonQuery()

            conn2.Close()
            DataGridView1.Rows.Clear()

            adpt = New MySqlDataAdapter("select * from clients order by client asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then

                For i = 0 To table.Rows.Count - 1


                    DataGridView1.Rows.Add("Clt-" & table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(12), table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(14).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(18))

                Next
            End If


            MsgBox("Modification bien faite")

            Label10.Text = ""


            t1.Text = ""
            t3.Text = "-"
            t6.Text = "-"
            t7.Text = "-"
            t11.Text = "-"
            t12.Text = 0
            t13.Text = 0
            t14.Text = 999999

            Panel3.Visible = False

        End If

    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Label10.Text = ""


        t1.Text = ""
        t3.Text = "-"
        t6.Text = "-"
        t7.Text = "-"
        t11.Text = "-"
        t12.Text = 0
        t13.Text = 0
        t14.Text = 999999
        Panel3.Visible = False


    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        If t1.Text <> "" Then

            conn2.Close()
            conn2.Open()

            Dim sql2 As String
            Dim cmd2 As MySqlCommand
            sql2 = "INSERT INTO clients ( `client`, `telephone`,`adresse`, `ville`, `ICE`, `credit`, `maxCredit`,`note`,`fac`) VALUES (@v1,@v3,@v6,@v7,@v12,@v13,@v14,@v11,@v2)"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.AddWithValue("@v1", t1.Text.Replace("'", ""))
            cmd2.Parameters.AddWithValue("@v3", t3.Text)
            cmd2.Parameters.AddWithValue("@v6", t6.Text)
            cmd2.Parameters.AddWithValue("@v7", t7.Text)
            cmd2.Parameters.AddWithValue("@v12", t12.Text)
            cmd2.Parameters.AddWithValue("@v13", Convert.ToDouble(t13.Text.ToString.Replace(".", ",")))
            cmd2.Parameters.AddWithValue("@v14", Convert.ToDouble(t14.Text.ToString.Replace(".", ",")))
            cmd2.Parameters.AddWithValue("@v11", t11.Text)
            cmd2.Parameters.AddWithValue("@v2", TextBox2.Text)
            cmd2.ExecuteNonQuery()

            sql2 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            If role = "Caissier" Then
                cmd2.Parameters.AddWithValue("@name", dashboard.Label2.Text)
            Else
                cmd2.Parameters.AddWithValue("@name", Label2.Text)
            End If
            cmd2.Parameters.AddWithValue("@op", "Ajout du client : " & t1.Text.Replace("'", ""))
            cmd2.ExecuteNonQuery()

            conn2.Close()
            DataGridView1.Rows.Clear()

            adpt = New MySqlDataAdapter("select * from clients order by client asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then

                For i = 0 To table.Rows.Count - 1


                    DataGridView1.Rows.Add("Clt-" & table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(12), table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(14).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(18))

                Next
            End If


            MsgBox("Client bien ajouté !")
            Label10.Text = ""


            t1.Text = ""
            t3.Text = "-"
            t6.Text = "-"
            t7.Text = "-"
            t11.Text = "-"
            t12.Text = 0
            t13.Text = 0
            t14.Text = 999999
            Panel3.Visible = False
        End If

    End Sub
    Dim ind As String


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
                adpt = New MySqlDataAdapter("select * from clients order by client asc", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                DataGridView1.Rows.Clear()

                If table.Rows.Count <> 0 Then

                    For i = 0 To table.Rows.Count - 1


                        DataGridView1.Rows.Add("Clt-" & table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(12), table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(14).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(18))

                    Next
                End If


            Else
                adpt = New MySqlDataAdapter("select * from clients where client like '%" + TextBox1.Text.Replace("'", " ") + "%' order by client asc", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count = 0 Then
                    MsgBox("Aucun client trouvé")

                Else
                    DataGridView1.Rows.Clear()
                    For i = 0 To table.Rows.Count - 1


                        DataGridView1.Rows.Add("Clt-" & table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(12), table.Rows(i).Item(0), Convert.ToDouble(table.Rows(i).Item(14).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(18))

                    Next

                End If
            End If



        End If
    End Sub
    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Me.Show()
    End Sub


    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        dashboard.Show()
        dashboard.IconButton9.Visible = True
        dashboard.IconButton8.Visible = False
    End Sub
    Private Sub iconbutton4_Click_1(sender As Object, e As EventArgs) Handles IconButton4.Click
        product.Show()
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


    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Four.Show()
        Me.Close()

    End Sub

    Private Sub IconButton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
        Home.Show()
        Me.Close()
    End Sub

    Private Sub IconButton14_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub IconButton16_Click(sender As Object, e As EventArgs) Handles IconButton16.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Ajouter Client'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Label10.Text = ""


                t1.Text = ""
                t3.Text = "-"
                t6.Text = "-"
                t7.Text = "-"
                t11.Text = "-"
                t12.Text = 0
                t13.Text = 0
                t14.Text = 999999
                Panel3.Visible = True
                Panel3.BringToFront()
                t1.Select()
                IconButton3.Visible = True
                IconButton1.Visible = False
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If


    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        users.Show()
        Me.Close()

    End Sub

    Private Sub IconButton30_Click(sender As Object, e As EventArgs) Handles IconButton30.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Supprimer Client'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Dim cmd As MySqlCommand
                Dim sql As String
                If DataGridView1.SelectedRows.Count <> 0 Then
                    For i = 0 To DataGridView1.SelectedRows.Count - 1


                        Dim Rep As Integer
                        Rep = MsgBox("Voulez vous vraiment supprimer ce client ?", vbYesNo)
                        If Rep = vbYes Then
                            conn2.Open()
                            cmd = New MySqlCommand("DELETE FROM `clients` WHERE id = '" & DataGridView1.SelectedRows(i).Cells(3).Value & "'", conn2)
                            cmd.ExecuteNonQuery()


                            sql = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                            cmd = New MySqlCommand(sql, conn2)
                            cmd.Parameters.Clear()
                            If role = "Caissier" Then
                                cmd.Parameters.AddWithValue("@name", dashboard.Label2.Text)
                            Else
                                cmd.Parameters.AddWithValue("@name", Label2.Text)
                            End If
                            cmd.Parameters.AddWithValue("@op", "Suppression du client : " & t1.Text.Replace("'", ""))
                            cmd.ExecuteNonQuery()

                            conn2.Close()
                            MsgBox("Opération bien faite !")
                            DataGridView1.Rows.RemoveAt(DataGridView1.SelectedRows(i).Index)
                        End If

                    Next
                End If
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
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

    Private Sub t1_TextChanged(sender As Object, e As EventArgs) Handles t1.TextChanged
        Dim inputText As String = t1.Text

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
            RemoveHandler t1.TextChanged, AddressOf t1_TextChanged

            ' Mettez à jour le texte dans le TextBox
            t1.Text = modifiedText

            ' Replacez le curseur à la position correcte après la modification
            t1.SelectionStart = t1.Text.Length

            ' Réactivez le gestionnaire d'événements TextChanged
            AddHandler t1.TextChanged, AddressOf t1_TextChanged
        End If
    End Sub

    Private Sub IconButton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        Dim frmstock As New stock
        frmstock.Show()
        Me.Close()

    End Sub

    Private Sub IconButton14_Click_1(sender As Object, e As EventArgs) Handles IconButton14.Click
        Panel8.Visible = False
    End Sub

    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        Panel8.Visible = True
        Panel8.BringToFront()
        DataGridView3.Rows.Clear()
        DataGridView3.Columns(0).Visible = False
        adpt = New MySqlDataAdapter("select * from client_price where client = '" & t1.Text.Replace("'", "") & "'", conn2)
        Dim tableprice As New DataTable
        adpt.Fill(tableprice)
        For i = 0 To tableprice.Rows.Count - 1
            DataGridView3.Rows.Add(False, tableprice.Rows(i).Item(2), tableprice.Rows(i).Item(3), tableprice.Rows(i).Item(6), tableprice.Rows(i).Item(7), tableprice.Rows(i).Item(0))
        Next
    End Sub

    Private Sub IconButton19_Click(sender As Object, e As EventArgs) Handles IconButton19.Click
        Panel13.Visible = False
        DataGridView3.Rows.Clear()
        DataGridView3.Columns(0).Visible = True
        TextBox23.Text = ""
        TextBox24.Text = ""
    End Sub

    Private Sub IconButton20_Click(sender As Object, e As EventArgs) Handles IconButton20.Click
        conn2.Close()
        conn2.Open()
        For i = 0 To DataGridView3.Rows.Count - 1
            If DataGridView3.Rows(i).Cells(0).Value = True Then
                adpt = New MySqlDataAdapter("select Code, Article, PV_HT, TVA, PV_TTC, PA_TTC from article where Code = '" & DataGridView3.Rows(i).Cells(1).Value & "'", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                Dim sql2 As String
                Dim cmd2 As MySqlCommand
                sql2 = "INSERT INTO client_price (`client`, `code`, `article`, `pv_ht`, `tva`, `pv_ttc`, `pa_ttc`) VALUES (@v1,@v2,@v3,@v4,@v5,@v6,@v7)"
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.Parameters.AddWithValue("@v1", t1.Text.Replace("'", ""))
                cmd2.Parameters.AddWithValue("@v2", table.Rows(0).Item(0))
                cmd2.Parameters.AddWithValue("@v3", table.Rows(0).Item(1))
                cmd2.Parameters.AddWithValue("@v4", table.Rows(0).Item(2))
                cmd2.Parameters.AddWithValue("@v5", table.Rows(0).Item(3))
                cmd2.Parameters.AddWithValue("@v6", table.Rows(0).Item(4))
                cmd2.Parameters.AddWithValue("@v7", table.Rows(0).Item(5))
                cmd2.ExecuteNonQuery()

            End If

        Next

        Panel13.Visible = True
        DataGridView3.Rows.Clear()
        DataGridView3.Columns(0).Visible = False
        adpt = New MySqlDataAdapter("select * from client_price where client = '" & t1.Text.Replace("'", "") & "'", conn2)
        Dim tableprice As New DataTable
        adpt.Fill(tableprice)
        For i = 0 To tableprice.Rows.Count - 1
            DataGridView3.Rows.Add(False, tableprice.Rows(i).Item(2), tableprice.Rows(i).Item(3), tableprice.Rows(i).Item(6), tableprice.Rows(i).Item(7), tableprice.Rows(i).Item(0))
        Next
        conn2.Close()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Modifier Client'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Panel3.Visible = True
                Panel3.BringToFront()
                IconButton17.Visible = True
                t1.Select()
                IconButton3.Visible = False
                IconButton1.Visible = True

                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                t1.Text = row.Cells(1).Value
                Label24.Text = row.Cells(1).Value
                t12.Text = row.Cells(2).Value
                t14.Text = row.Cells(4).Value

                TextBox2.Text = row.Cells(5).Value

                Label10.Text = row.Cells(3).Value
                adpt = New MySqlDataAdapter("select masq from clients where id = '" & row.Cells(3).Value & "'", conn2)
                Dim tableprice As New DataTable
                adpt.Fill(tableprice)

                If tableprice.Rows(0).Item(0) = "oui" Then
                    IconButton25.Visible = True
                Else
                    IconButton25.Visible = False
                End If

            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub DataGridView3_CancelRowEdit(sender As Object, e As QuestionEventArgs) Handles DataGridView3.CancelRowEdit

    End Sub

    Private Sub Panel8_Paint(sender As Object, e As PaintEventArgs) Handles Panel8.Paint

    End Sub

    Private Sub DataGridView3_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellEndEdit
        DataGridView3.Rows(e.RowIndex).Cells(3).Value = Convert.ToDouble(DataGridView3.Rows(e.RowIndex).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")).ToString("N2")
        adpt = New MySqlDataAdapter("select TVA from article where Code = '" & DataGridView3.Rows(e.RowIndex).Cells(1).Value & "'", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim pa_ht As Double = DataGridView3.Rows(e.RowIndex).Cells(3).Value / (1 + (table.Rows(0).Item(0) / 100))
        conn2.Close()
        conn2.Open()
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = "UPDATE `client_price` SET `pv_ttc` = @v1, `pv_ht` = @v2 WHERE id = '" & DataGridView3.Rows(e.RowIndex).Cells(5).Value & "' "
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.AddWithValue("@v1", DataGridView3.Rows(e.RowIndex).Cells(3).Value)
        cmd2.Parameters.AddWithValue("@v2", pa_ht.ToString("N2"))

        cmd2.ExecuteNonQuery()
        conn2.Close()
    End Sub
    Dim m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Dim ReportToPrint, ReportToPrintcopy As LocalReport
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)
        Return stream
    End Function
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

    Private Sub IconButton22_Click(sender As Object, e As EventArgs) Handles IconButton22.Click
        If Label10.Text <> "" Then
            conn2.Close()
            conn2.Open()
            Dim sql2 As String
            Dim cmd2 As MySqlCommand
            sql2 = "UPDATE `clients` SET `masq` = @v1 WHERE id = " + Label10.Text
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.AddWithValue("@v1", "oui")
            cmd2.ExecuteNonQuery()


            sql2 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            If role = "Caissier" Then
                cmd2.Parameters.AddWithValue("@name", dashboard.Label2.Text)
            Else
                cmd2.Parameters.AddWithValue("@name", Label2.Text)
            End If
            cmd2.Parameters.AddWithValue("@op", "Bloquage du client : " & t1.Text.Replace("'", ""))
            cmd2.ExecuteNonQuery()

            conn2.Close()

            IconButton25.Visible = True
            MsgBox("Client bloqué !")
        End If

    End Sub

    Private Sub IconButton25_Click(sender As Object, e As EventArgs) Handles IconButton25.Click
        If Label10.Text <> "" Then
            conn2.Close()
            conn2.Open()
            Dim sql2 As String
            Dim cmd2 As MySqlCommand
            sql2 = "UPDATE `clients` SET `masq` = @v1 WHERE id = " + Label10.Text
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.AddWithValue("@v1", "non")
            cmd2.ExecuteNonQuery()


            sql2 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            If role = "Caissier" Then
                cmd2.Parameters.AddWithValue("@name", dashboard.Label2.Text)
            Else
                cmd2.Parameters.AddWithValue("@name", Label2.Text)
            End If
            cmd2.Parameters.AddWithValue("@op", "Débloquage du client : " & t1.Text.Replace("'", ""))
            cmd2.ExecuteNonQuery()

            conn2.Close()

            IconButton25.Visible = False
            MsgBox("Client débloqué !")
        End If

    End Sub

    Private Sub IconButton21_Click(sender As Object, e As EventArgs) Handles IconButton21.Click
        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("tick")
        For i = 0 To DataGridView3.Rows.Count - 1
            dt.Rows.Add(DataGridView3.Rows(i).Cells(1).Value, DataGridView3.Rows(i).Cells(2).Value, "", DataGridView3.Rows(i).Cells(3).Value, "", "", "", "", "")

        Next

        ReportToPrint = New LocalReport()
        formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\Tarifs.rdlc"
        formata4.ReportViewer1.LocalReport.DataSources.Clear()
        formata4.ReportViewer1.LocalReport.EnableExternalImages = True
        Dim du As New ReportParameter("date", DateTime.Now.ToString("dd/MM/yyyy"))
        Dim du1(0) As ReportParameter
        du1(0) = du
        formata4.ReportViewer1.LocalReport.SetParameters(du1)


        Dim client As New ReportParameter("client", t1.Text)
        Dim client1(0) As ReportParameter
        client1(0) = client
        formata4.ReportViewer1.LocalReport.SetParameters(client1)

        Dim esp As New ReportParameter("ice", t12.Text)
        Dim esp1(0) As ReportParameter
        esp1(0) = esp
        formata4.ReportViewer1.LocalReport.SetParameters(esp1)

        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim table3 As New DataTable
        adpt.Fill(table3)
        Dim details As New ReportParameter("infos", table3.Rows(0).Item(1).ToString + " | " + "Tél: " + table3.Rows(0).Item(2).ToString + " | " + "Email: " + table3.Rows(0).Item(3).ToString + " | " + "ICE: " + table3.Rows(0).Item(4).ToString + " | " + "IF: " + table3.Rows(0).Item(5).ToString + " | " + "TP: " + table3.Rows(0).Item(7).ToString + " | " + "RC: " + table3.Rows(0).Item(8).ToString)
        Dim details1(0) As ReportParameter
        details1(0) = details
        formata4.ReportViewer1.LocalReport.SetParameters(details1)


        Dim appPath As String = Application.StartupPath()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

        Dim img As New ReportParameter("img", "File:\" + SavePath, True)
        Dim img1(0) As ReportParameter
        img1(0) = img
        formata4.ReportViewer1.LocalReport.SetParameters(img1)


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

    Private Sub DataGridView3_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellDoubleClick
        If e.ColumnIndex = 2 Then
            Dim Rep As Integer
            Dim sql As String
            Dim cmd As MySqlCommand
            Rep = MsgBox("Voulez vous vraiment supprimer ce produit de la liste ?", vbYesNo)
            If Rep = vbYes Then
                conn2.Open()
                cmd = New MySqlCommand("DELETE FROM `client_price` WHERE id = '" & DataGridView3.Rows(e.RowIndex).Cells(5).Value & "'", conn2)
                cmd.ExecuteNonQuery()
                conn2.Close()
                DataGridView3.Rows.RemoveAt(e.RowIndex)
            End If
        End If


    End Sub
End Class