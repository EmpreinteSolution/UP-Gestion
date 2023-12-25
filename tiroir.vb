Imports MySql.Data.MySqlClient

Public Class tiroir
    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox4.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Remplir tous les champs !")
        Else
            If Convert.ToDouble(TextBox2.Text.ToString.Replace(".", ",")) = 0 Then
                MsgBox("Veuillez saisir un montant valide !")
            Else
                Caseopen = True
                conn2.Open()
                Dim sql2 As String
                Dim cmd2 As MySqlCommand
                sql2 = "INSERT INTO tiroir (`cause`, `mtn`, `date`, `caissier`,`remarque`) VALUES ('" & ComboBox4.Text & "','" + Convert.ToDouble(TextBox2.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" + dashboard.Label2.Text + "','" & TextBox3.Text.Replace("'", "") & "')"
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.ExecuteNonQuery()
                conn2.Close()

                If ComboBox4.Text = "Réception" Then
                Else

                    conn2.Open()

                    If DateTime.Today.Month = "01" Then
                        sql2 = "INSERT INTO charges ( `charge`, `j`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
                    End If
                    If DateTime.Today.Month = "02" Then
                        sql2 = "INSERT INTO charges ( `charge`, `f`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
                    End If
                    If DateTime.Today.Month = "03" Then
                        sql2 = "INSERT INTO charges ( `charge`, `m`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
                    End If
                    If DateTime.Today.Month = "04" Then
                        sql2 = "INSERT INTO charges ( `charge`, `a`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
                    End If
                    If DateTime.Today.Month = "05" Then
                        sql2 = "INSERT INTO charges ( `charge`, `mai`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
                    End If
                    If DateTime.Today.Month = "06" Then
                        sql2 = "INSERT INTO charges ( `charge`, `juin`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
                    End If
                    If DateTime.Today.Month = "07" Then
                        sql2 = "INSERT INTO charges ( `charge`, `juil`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
                    End If
                    If DateTime.Today.Month = "08" Then
                        sql2 = "INSERT INTO charges ( `charge`, `ao`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
                    End If
                    If DateTime.Today.Month = "09" Then
                        sql2 = "INSERT INTO charges ( `charge`, `sept`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
                    End If
                    If DateTime.Today.Month = "10" Then
                        sql2 = "INSERT INTO charges ( `charge`, `oct`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
                    End If
                    If DateTime.Today.Month = "11" Then
                        sql2 = "INSERT INTO charges ( `charge`, `nov`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
                    End If
                    If DateTime.Today.Month = "12" Then
                        sql2 = "INSERT INTO charges ( `charge`, `dece`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
                    End If

                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.Parameters.AddWithValue("@v1", ComboBox4.Text)
                    cmd2.Parameters.AddWithValue("@v3", Convert.ToDouble(TextBox2.Text.ToString.Replace(".", ",")))
                    cmd2.Parameters.AddWithValue("@v9", TextBox3.Text.ToString.Replace("'", ""))
                    cmd2.Parameters.AddWithValue("@v6", DateTime.Today.Year)
                    cmd2.Parameters.AddWithValue("@v7", DateTime.Today)
                    cmd2.Parameters.AddWithValue("@v8", "Espèce")

                    cmd2.ExecuteNonQuery()
                    conn2.Close()
                End If

                TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    Me.Close()
                End If
            End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Caseopen = False
        Me.Close()
        dashboard.TextBox2.Select()
    End Sub

    Private Sub tiroir_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        Dim adpt As MySqlDataAdapter
        adpt = New MySqlDataAdapter("Select * from typecharges", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        ComboBox4.Items.Clear()

        If table2.Rows.Count <> 0 Then

            For i = 0 To table2.Rows.Count - 1

                ComboBox4.Items.Add(table2.Rows(i).Item(1))

            Next
            ComboBox4.Items.Add("Réception")
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

    End Sub

    Private Sub tiroir_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button2.PerformClick()
        End If
        If e.KeyCode = Keys.Escape Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        TextBox3.Select()

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Dim inputText As String = TextBox3.Text

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
            RemoveHandler TextBox3.TextChanged, AddressOf TextBox3_TextChanged

            ' Mettez à jour le texte dans le TextBox
            TextBox3.Text = modifiedText

            ' Replacez le curseur à la position correcte après la modification
            TextBox3.SelectionStart = TextBox3.Text.Length

            ' Réactivez le gestionnaire d'événements TextChanged
            AddHandler TextBox3.TextChanged, AddressOf TextBox3_TextChanged
        End If
    End Sub
End Class