Imports System.IO
Imports MySql.Data.MySqlClient
Public Class Charges
    Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt, adpt2 As MySqlDataAdapter

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

    Private Sub Charges_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        ComboBox1.Text = Date.Today.Year.ToString
        Label2.Text = user
        Dim w = Screen.PrimaryScreen.WorkingArea.Width
        Dim h = My.Computer.Screen.WorkingArea.Size.Height
        Me.Width = w
        Me.Height = h
        Me.Location = New Point(0, 0)

        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim tablebq As New DataTable
        adpt.Fill(tablebq)

        For i = 0 To tablebq.Rows.Count - 1
            ComboBox8.Items.Add(tablebq.Rows(i).Item(1))
            ComboBox7.Items.Add(tablebq.Rows(i).Item(1))
        Next
        adpt = New MySqlDataAdapter("select `id`, `charge`, sum(`j`), sum(`f`), sum(`m`), sum(`a`), sum(`mai`), sum(`juin`), sum(`juil`), sum(`ao`), sum(`sept`), sum(`oct`), sum(`nov`), sum(`dece`), `year`, `date`, `mode` from charges where `year` = '" + ComboBox1.Text + "' group by charge order by charge asc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sumchrgannul, chrgannul, to0, to1, to2, to3, to4, to5, to6, to7, tot7, to8, to9, to10, to11, to12 As Double
        Dim summa, ma1, ma2, ma3, ma4, ma5, ma6, ma7, mat7, ma8, ma9, ma10, ma11, ma12 As Double
        summa = 0
        Dim currentDate As DateTime = DateTime.Now

        adpt = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(marge,',','.'),' ','')),Month(date) from orderdetails WHERE Year(date) = @year group by Month(date)", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@year", currentDate.Year)
        Dim tablemargemois As New DataTable
        adpt.Fill(tablemargemois)
        For i = 0 To tablemargemois.Rows.Count - 1
            summa += tablemargemois.Rows(i).Item(0)
            If tablemargemois.Rows(i).Item(1) = 1 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma1 = 0
                Else
                    ma1 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 2 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma2 = 0
                Else
                    ma2 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 3 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma3 = 0
                Else
                    ma3 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 4 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma4 = 0
                Else
                    ma4 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 5 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma5 = 0
                Else
                    ma5 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 6 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma6 = 0
                Else
                    ma6 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 7 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma7 = 0
                Else
                    ma7 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 8 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma8 = 0
                Else
                    ma8 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 9 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma9 = 0
                Else
                    ma9 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 10 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma10 = 0
                Else
                    ma10 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 11 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma11 = 0
                Else
                    ma11 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 12 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma12 = 0
                Else
                    ma12 = tablemargemois.Rows(i).Item(0)
                End If
            End If

        Next

        DatagridView1.Rows.Clear()
        If table.Rows.Count <> 0 Then

            For i = 0 To table.Rows.Count - 1
                sumchrgannul += Convert.ToDouble(table.Rows(i).Item(2)) + Convert.ToDouble(table.Rows(i).Item(3)) + Convert.ToDouble(table.Rows(i).Item(4)) + Convert.ToDouble(table.Rows(i).Item(5)) + Convert.ToDouble(table.Rows(i).Item(6)) + Convert.ToDouble(table.Rows(i).Item(7)) + Convert.ToDouble(table.Rows(i).Item(8)) + Convert.ToDouble(table.Rows(i).Item(9)) + Convert.ToDouble(table.Rows(i).Item(10)) + Convert.ToDouble(table.Rows(i).Item(11)) + Convert.ToDouble(table.Rows(i).Item(12)) + Convert.ToDouble(table.Rows(i).Item(13))
                chrgannul = Convert.ToDouble(table.Rows(i).Item(2)) + Convert.ToDouble(table.Rows(i).Item(3)) + Convert.ToDouble(table.Rows(i).Item(4)) + Convert.ToDouble(table.Rows(i).Item(5)) + Convert.ToDouble(table.Rows(i).Item(6)) + Convert.ToDouble(table.Rows(i).Item(7)) + Convert.ToDouble(table.Rows(i).Item(8)) + Convert.ToDouble(table.Rows(i).Item(9)) + Convert.ToDouble(table.Rows(i).Item(10)) + Convert.ToDouble(table.Rows(i).Item(11)) + Convert.ToDouble(table.Rows(i).Item(12)) + Convert.ToDouble(table.Rows(i).Item(13))
                DatagridView1.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(chrgannul).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(2)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(3)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(4)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(5)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(6)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(7)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(8)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(9)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(10)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(11)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(12)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(13)).ToString("# ##0.00").Replace(".", ","), table.Rows(i).Item(0))

                to0 += Convert.ToDouble(chrgannul.ToString.Replace(".", ","))
                to1 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                to2 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))
                to3 += Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ","))
                to4 += Convert.ToDouble(table.Rows(i).Item(5).ToString.Replace(".", ","))
                to5 += Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(".", ","))
                to6 += Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(".", ","))
                tot7 += Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(".", ","))
                to8 += Convert.ToDouble(table.Rows(i).Item(9).ToString.Replace(".", ","))
                to9 += Convert.ToDouble(table.Rows(i).Item(10).ToString.Replace(".", ","))
                to10 += Convert.ToDouble(table.Rows(i).Item(11).ToString.Replace(".", ","))
                to11 += Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ","))
                to12 += Convert.ToDouble(table.Rows(i).Item(13).ToString.Replace(".", ","))


            Next
            DatagridView1.Rows.Add()
            DatagridView1.Rows.Add("TOTAL DES CHARGES", to0, to1, to2, to3, to4, to5, to6, tot7, to8, to9, to10, to11, to12, Nothing)
            DatagridView1.Rows.Add("TOTAL DES MARGES", summa, ma1, ma2, ma3, ma4, ma5, ma6, ma7, ma8, ma9, ma10, ma11, ma12, Nothing)
            DatagridView1.Rows.Add("TOTAL NET", summa - to0, ma1 - to1, ma2 - to2, ma3 - to3, ma4 - to4, ma5 - to5, ma6 - to6, ma7 - tot7, ma8 - to8, ma9 - to9, ma10 - to10, ma11 - to11, ma12 - to12, Nothing)
        End If
        Dim columnIndexToColor As Integer = 0

        For i = 0 To DatagridView1.Rows.Count - 1
            If i >= DatagridView1.RowCount - 3 Then
                DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Khaki ' Remplacez Color.Yellow par la couleur de votre choix
            End If
            If i = DatagridView1.RowCount - 1 Then
                DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Gold ' Remplacez Color.Yellow par la couleur de votre choix
            End If
        Next

        IconButton16.Text = sumchrgannul.ToString("N2") & " DHs"
        adpt = New MySqlDataAdapter("Select * from typecharges", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        DataGridView2.Rows.Clear()
        ComboBox4.Items.Clear()

        If table2.Rows.Count <> 0 Then

            For i = 0 To table2.Rows.Count - 1

                DataGridView2.Rows.Add(table2.Rows(i).Item(0), table2.Rows(i).Item(1), "X")
                ComboBox4.Items.Add(table2.Rows(i).Item(1))

            Next
        End If

        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Gestion des charges'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then
            Else
                Panel5.Visible = True
                Panel5.BringToFront()
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If


    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        conn2.Close()
        conn2.Open()
        Dim sql2 As String
        Dim cmd2 As MySqlCommand

        If DateTimePicker1.Value.Month = "01" Then
            sql2 = "INSERT INTO charges ( `charge`, `j`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
        End If
        If DateTimePicker1.Value.Month = "02" Then
            sql2 = "INSERT INTO charges ( `charge`, `f`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
        End If
        If DateTimePicker1.Value.Month = "03" Then
            sql2 = "INSERT INTO charges ( `charge`, `m`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
        End If
        If DateTimePicker1.Value.Month = "04" Then
            sql2 = "INSERT INTO charges ( `charge`, `a`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
        End If
        If DateTimePicker1.Value.Month = "05" Then
            sql2 = "INSERT INTO charges ( `charge`, `mai`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
        End If
        If DateTimePicker1.Value.Month = "06" Then
            sql2 = "INSERT INTO charges ( `charge`, `juin`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
        End If
        If DateTimePicker1.Value.Month = "07" Then
            sql2 = "INSERT INTO charges ( `charge`, `juil`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
        End If
        If DateTimePicker1.Value.Month = "08" Then
            sql2 = "INSERT INTO charges ( `charge`, `ao`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
        End If
        If DateTimePicker1.Value.Month = "09" Then
            sql2 = "INSERT INTO charges ( `charge`, `sept`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
        End If
        If DateTimePicker1.Value.Month = "10" Then
            sql2 = "INSERT INTO charges ( `charge`, `oct`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
        End If
        If DateTimePicker1.Value.Month = "11" Then
            sql2 = "INSERT INTO charges ( `charge`, `nov`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
        End If
        If DateTimePicker1.Value.Month = "12" Then
            sql2 = "INSERT INTO charges ( `charge`, `dece`,`year`, `date`,`mode`,`remarque`) VALUES (@v1,@v3,@v6,@v7,@v8,@v9)"
        End If

        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.AddWithValue("@v1", ComboBox4.Text)
        cmd2.Parameters.AddWithValue("@v3", Convert.ToDouble(t6.Text.ToString.Replace(".", ",")))
        cmd2.Parameters.AddWithValue("@v6", DateTimePicker1.Value.Year)
        cmd2.Parameters.AddWithValue("@v7", DateTimePicker1.Value)
        cmd2.Parameters.AddWithValue("@v8", ComboBox2.Text)
        cmd2.Parameters.AddWithValue("@v9", TextBox1.Text)

        cmd2.ExecuteNonQuery()

        Dim lastInsertedId As Long = cmd2.LastInsertedId()
        Dim lasid As String = "Charge" & lastInsertedId.ToString()
        If ComboBox2.Text = "TPE" Then
            sql2 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `achat`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('tpe','" + TextBox21.Text + "','tpe','" + TextBox20.Text + "','tpe', '" + Convert.ToDateTime(DateTimePicker1.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & lasid & "','" + ComboBox8.Text + "','" & t6.Text.ToString.Replace(".", ",").Replace(" ", "") & "','" & TextBox1.Text & "','" & ComboBox2.Text & "')"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            cmd2.ExecuteNonQuery()
        End If
        If ComboBox2.Text = "Chèque" Then
            sql2 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `achat`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('" + ComboBox7.Text + "','" + TextBox16.Text + "','" + TextBox15.Text + "','" + TextBox14.Text + "','" + Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Convert.ToDateTime(DateTimePicker1.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & lasid & "','" + ComboBox7.Text + "','" + t6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & TextBox1.Text & "','" & ComboBox2.Text & "')"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            cmd2.ExecuteNonQuery()
        End If
        If ComboBox2.Text = "Virement" Then
            sql2 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `achat`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('" + ComboBox7.Text + "','" + TextBox16.Text + "','" + TextBox15.Text + "','" + TextBox14.Text + "','" + Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Convert.ToDateTime(DateTimePicker1.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & lasid & "','" + ComboBox7.Text + "','" + t6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & TextBox1.Text & "','" & ComboBox2.Text & "')"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            cmd2.ExecuteNonQuery()
        End If
        If ComboBox2.Text = "Effet" Then
            sql2 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `achat`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('" + ComboBox7.Text + "','" + TextBox16.Text + "','" + TextBox15.Text + "','" + TextBox14.Text + "','" + Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Convert.ToDateTime(DateTimePicker1.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & lasid & "','" + ComboBox7.Text + "','" + t6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & TextBox1.Text & "','" & ComboBox2.Text & "')"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            cmd2.ExecuteNonQuery()
        End If

        sql2 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.Clear()
        If role = "Caissier" Then
            cmd2.Parameters.AddWithValue("@name", dashboard.Label2.Text)
        Else
            cmd2.Parameters.AddWithValue("@name", Label2.Text)
        End If
        cmd2.Parameters.AddWithValue("@op", "Ajout de la charge : " & ComboBox4.Text & " - " & TextBox1.Text.Replace("'", " "))
        cmd2.ExecuteNonQuery()

        conn2.Close()
        MsgBox("Charge bien ajouté !")
        t6.Text = ""
        TextBox1.Text = ""
        TextBox21.Text = ""
        TextBox20.Text = ""
        TextBox16.Text = ""
        TextBox15.Text = ""
        TextBox14.Text = ""
        ComboBox2.Text = ""
        ComboBox4.Text = ""
        IconButton26.PerformClick()
        IconButton28.PerformClick()
        adpt = New MySqlDataAdapter("select `id`, `charge`, sum(`j`), sum(`f`), sum(`m`), sum(`a`), sum(`mai`), sum(`juin`), sum(`juil`), sum(`ao`), sum(`sept`), sum(`oct`), sum(`nov`), sum(`dece`), `year`, `date`, `mode` from charges where `year` = '" + ComboBox1.Text + "' group by charge order by charge asc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sumchrgannul, chrgannul, to0, to1, to2, to3, to4, to5, to6, to7, tot7, to8, to9, to10, to11, to12 As Double
        Dim summa, ma1, ma2, ma3, ma4, ma5, ma6, ma7, mat7, ma8, ma9, ma10, ma11, ma12 As Double
        summa = 0
        Dim currentDate As DateTime = DateTime.Now

        adpt = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(marge,',','.'),' ','')),Month(date) from orderdetails WHERE Year(date) = @year group by Month(date)", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@year", currentDate.Year)
        Dim tablemargemois As New DataTable
        adpt.Fill(tablemargemois)
        For i = 0 To tablemargemois.Rows.Count - 1
            summa += tablemargemois.Rows(i).Item(0)
            If tablemargemois.Rows(i).Item(1) = 1 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma1 = 0
                Else
                    ma1 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 2 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma2 = 0
                Else
                    ma2 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 3 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma3 = 0
                Else
                    ma3 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 4 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma4 = 0
                Else
                    ma4 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 5 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma5 = 0
                Else
                    ma5 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 6 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma6 = 0
                Else
                    ma6 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 7 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma7 = 0
                Else
                    ma7 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 8 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma8 = 0
                Else
                    ma8 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 9 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma9 = 0
                Else
                    ma9 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 10 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma10 = 0
                Else
                    ma10 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 11 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma11 = 0
                Else
                    ma11 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 12 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma12 = 0
                Else
                    ma12 = tablemargemois.Rows(i).Item(0)
                End If
            End If

        Next

        DatagridView1.Rows.Clear()
        If table.Rows.Count <> 0 Then

            For i = 0 To table.Rows.Count - 1
                sumchrgannul += Convert.ToDouble(table.Rows(i).Item(2)) + Convert.ToDouble(table.Rows(i).Item(3)) + Convert.ToDouble(table.Rows(i).Item(4)) + Convert.ToDouble(table.Rows(i).Item(5)) + Convert.ToDouble(table.Rows(i).Item(6)) + Convert.ToDouble(table.Rows(i).Item(7)) + Convert.ToDouble(table.Rows(i).Item(8)) + Convert.ToDouble(table.Rows(i).Item(9)) + Convert.ToDouble(table.Rows(i).Item(10)) + Convert.ToDouble(table.Rows(i).Item(11)) + Convert.ToDouble(table.Rows(i).Item(12)) + Convert.ToDouble(table.Rows(i).Item(13))
                chrgannul = Convert.ToDouble(table.Rows(i).Item(2)) + Convert.ToDouble(table.Rows(i).Item(3)) + Convert.ToDouble(table.Rows(i).Item(4)) + Convert.ToDouble(table.Rows(i).Item(5)) + Convert.ToDouble(table.Rows(i).Item(6)) + Convert.ToDouble(table.Rows(i).Item(7)) + Convert.ToDouble(table.Rows(i).Item(8)) + Convert.ToDouble(table.Rows(i).Item(9)) + Convert.ToDouble(table.Rows(i).Item(10)) + Convert.ToDouble(table.Rows(i).Item(11)) + Convert.ToDouble(table.Rows(i).Item(12)) + Convert.ToDouble(table.Rows(i).Item(13))
                DatagridView1.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(chrgannul).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(2)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(3)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(4)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(5)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(6)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(7)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(8)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(9)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(10)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(11)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(12)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(13)).ToString("# ##0.00").Replace(".", ","), table.Rows(i).Item(0))

                to0 += Convert.ToDouble(chrgannul.ToString.Replace(".", ","))
                to1 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                to2 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))
                to3 += Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ","))
                to4 += Convert.ToDouble(table.Rows(i).Item(5).ToString.Replace(".", ","))
                to5 += Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(".", ","))
                to6 += Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(".", ","))
                tot7 += Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(".", ","))
                to8 += Convert.ToDouble(table.Rows(i).Item(9).ToString.Replace(".", ","))
                to9 += Convert.ToDouble(table.Rows(i).Item(10).ToString.Replace(".", ","))
                to10 += Convert.ToDouble(table.Rows(i).Item(11).ToString.Replace(".", ","))
                to11 += Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ","))
                to12 += Convert.ToDouble(table.Rows(i).Item(13).ToString.Replace(".", ","))


            Next
            DatagridView1.Rows.Add()
            DatagridView1.Rows.Add("TOTAL DES CHARGES", to0, to1, to2, to3, to4, to5, to6, tot7, to8, to9, to10, to11, to12, Nothing)
            DatagridView1.Rows.Add("TOTAL DES MARGES", summa, ma1, ma2, ma3, ma4, ma5, ma6, ma7, ma8, ma9, ma10, ma11, ma12, Nothing)
            DatagridView1.Rows.Add("TOTAL NET", summa - to0, ma1 - to1, ma2 - to2, ma3 - to3, ma4 - to4, ma5 - to5, ma6 - to6, ma7 - tot7, ma8 - to8, ma9 - to9, ma10 - to10, ma11 - to11, ma12 - to12, Nothing)
        End If
        Dim columnIndexToColor As Integer = 0

        For i = 0 To DatagridView1.Rows.Count - 1
            If i >= DatagridView1.RowCount - 3 Then
                DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Khaki ' Remplacez Color.Yellow par la couleur de votre choix
            End If
            If i = DatagridView1.RowCount - 1 Then
                DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Gold ' Remplacez Color.Yellow par la couleur de votre choix
            End If
        Next

        IconButton16.Text = sumchrgannul.ToString("N2") & " DHs"


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


    Private Sub IconButton15_Click(sender As Object, e As EventArgs) Handles IconButton15.Click
        conn2.Close()
        conn2.Open()
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = "INSERT INTO typecharges ( `type`) VALUES (@v3)"
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.AddWithValue("@v3", t13.Text)

        cmd2.ExecuteNonQuery()
        conn2.Close()
        MsgBox("Type bien ajouté !")
        t13.Text = ""
        DataGridView2.Rows.Clear()
        ComboBox4.Items.Clear()

        adpt = New MySqlDataAdapter("select * from typecharges", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count <> 0 Then

            For i = 0 To table2.Rows.Count - 1

                DataGridView2.Rows.Add(table2.Rows(i).Item(0), table2.Rows(i).Item(1), "X")
                ComboBox4.Items.Add(table2.Rows(i).Item(1))
            Next
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        adpt = New MySqlDataAdapter("select `id`, `charge`, sum(`j`), sum(`f`), sum(`m`), sum(`a`), sum(`mai`), sum(`juin`), sum(`juil`), sum(`ao`), sum(`sept`), sum(`oct`), sum(`nov`), sum(`dece`), `year`, `date`, `mode` from charges where `year` = '" + ComboBox1.Text + "' group by charge order by charge asc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sumchrgannul, chrgannul, to0, to1, to2, to3, to4, to5, to6, to7, tot7, to8, to9, to10, to11, to12 As Double
        Dim summa, ma1, ma2, ma3, ma4, ma5, ma6, ma7, mat7, ma8, ma9, ma10, ma11, ma12 As Double
        summa = 0
        Dim currentDate As DateTime = DateTime.Now

        adpt = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(marge,',','.'),' ','')),Month(date) from orderdetails WHERE Year(date) = @year group by Month(date)", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@year", ComboBox1.Text)
        Dim tablemargemois As New DataTable
        adpt.Fill(tablemargemois)
        For i = 0 To tablemargemois.Rows.Count - 1
            summa += tablemargemois.Rows(i).Item(0)
            If tablemargemois.Rows(i).Item(1) = 1 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma1 = 0
                Else
                    ma1 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 2 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma2 = 0
                Else
                    ma2 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 3 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma3 = 0
                Else
                    ma3 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 4 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma4 = 0
                Else
                    ma4 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 5 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma5 = 0
                Else
                    ma5 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 6 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma6 = 0
                Else
                    ma6 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 7 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma7 = 0
                Else
                    ma7 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 8 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma8 = 0
                Else
                    ma8 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 9 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma9 = 0
                Else
                    ma9 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 10 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma10 = 0
                Else
                    ma10 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 11 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma11 = 0
                Else
                    ma11 = tablemargemois.Rows(i).Item(0)
                End If
            End If
            If tablemargemois.Rows(i).Item(1) = 12 Then
                If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                    ma12 = 0
                Else
                    ma12 = tablemargemois.Rows(i).Item(0)
                End If
            End If

        Next

        DatagridView1.Rows.Clear()
        If table.Rows.Count <> 0 Then

            For i = 0 To table.Rows.Count - 1
                sumchrgannul += Convert.ToDouble(table.Rows(i).Item(2)) + Convert.ToDouble(table.Rows(i).Item(3)) + Convert.ToDouble(table.Rows(i).Item(4)) + Convert.ToDouble(table.Rows(i).Item(5)) + Convert.ToDouble(table.Rows(i).Item(6)) + Convert.ToDouble(table.Rows(i).Item(7)) + Convert.ToDouble(table.Rows(i).Item(8)) + Convert.ToDouble(table.Rows(i).Item(9)) + Convert.ToDouble(table.Rows(i).Item(10)) + Convert.ToDouble(table.Rows(i).Item(11)) + Convert.ToDouble(table.Rows(i).Item(12)) + Convert.ToDouble(table.Rows(i).Item(13))
                chrgannul = Convert.ToDouble(table.Rows(i).Item(2)) + Convert.ToDouble(table.Rows(i).Item(3)) + Convert.ToDouble(table.Rows(i).Item(4)) + Convert.ToDouble(table.Rows(i).Item(5)) + Convert.ToDouble(table.Rows(i).Item(6)) + Convert.ToDouble(table.Rows(i).Item(7)) + Convert.ToDouble(table.Rows(i).Item(8)) + Convert.ToDouble(table.Rows(i).Item(9)) + Convert.ToDouble(table.Rows(i).Item(10)) + Convert.ToDouble(table.Rows(i).Item(11)) + Convert.ToDouble(table.Rows(i).Item(12)) + Convert.ToDouble(table.Rows(i).Item(13))
                DatagridView1.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(chrgannul).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(2)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(3)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(4)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(5)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(6)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(7)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(8)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(9)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(10)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(11)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(12)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(13)).ToString("# ##0.00").Replace(".", ","), table.Rows(i).Item(0))

                to0 += Convert.ToDouble(chrgannul.ToString.Replace(".", ","))
                to1 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                to2 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))
                to3 += Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ","))
                to4 += Convert.ToDouble(table.Rows(i).Item(5).ToString.Replace(".", ","))
                to5 += Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(".", ","))
                to6 += Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(".", ","))
                tot7 += Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(".", ","))
                to8 += Convert.ToDouble(table.Rows(i).Item(9).ToString.Replace(".", ","))
                to9 += Convert.ToDouble(table.Rows(i).Item(10).ToString.Replace(".", ","))
                to10 += Convert.ToDouble(table.Rows(i).Item(11).ToString.Replace(".", ","))
                to11 += Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ","))
                to12 += Convert.ToDouble(table.Rows(i).Item(13).ToString.Replace(".", ","))


            Next
            DatagridView1.Rows.Add()
            DatagridView1.Rows.Add("TOTAL DES CHARGES", to0, to1, to2, to3, to4, to5, to6, tot7, to8, to9, to10, to11, to12, Nothing)
            DatagridView1.Rows.Add("TOTAL DES MARGES", summa, ma1, ma2, ma3, ma4, ma5, ma6, ma7, ma8, ma9, ma10, ma11, ma12, Nothing)
            DatagridView1.Rows.Add("TOTAL NET", summa - to0, ma1 - to1, ma2 - to2, ma3 - to3, ma4 - to4, ma5 - to5, ma6 - to6, ma7 - tot7, ma8 - to8, ma9 - to9, ma10 - to10, ma11 - to11, ma12 - to12, Nothing)
        End If
        Dim columnIndexToColor As Integer = 0

        For i = 0 To DatagridView1.Rows.Count - 1
            If i >= DatagridView1.RowCount - 3 Then
                DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Khaki ' Remplacez Color.Yellow par la couleur de votre choix
            End If
            If i = DatagridView1.RowCount - 1 Then
                DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Gold ' Remplacez Color.Yellow par la couleur de votre choix
            End If
        Next
        IconButton16.Text = sumchrgannul.ToString("N2") & " DHs"
    End Sub
    Dim ind As String
    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 2 Then
                Dim Rep As Integer
                Rep = MsgBox("Voulez-vous vraiment supprimer ce type ?", vbYesNo)
                If Rep = vbYes Then

                    Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                    ind = row.Cells(0).Value.ToString
                    conn2.Close()
                    conn2.Open()
                    Dim sql2 As String
                    Dim cmd2 As MySqlCommand
                    sql2 = "delete from typecharges where id = " + ind
                    cmd2 = New MySqlCommand(sql2, conn2)

                    cmd2.ExecuteNonQuery()
                    conn2.Close()
                    adpt = New MySqlDataAdapter("select * from typecharges", conn2)
                    Dim table2 As New DataTable
                    adpt.Fill(table2)
                    DataGridView2.Rows.Clear()
                    ComboBox4.Items.Clear()
                    If table2.Rows.Count <> 0 Then

                        For i = 0 To table2.Rows.Count - 1

                            DataGridView2.Rows.Add(table2.Rows(i).Item(0), table2.Rows(i).Item(1), "X")
                            ComboBox4.Items.Add(table2.Rows(i).Item(1))

                        Next
                    End If


                End If

            End If

        End If
    End Sub


    Private Sub iconbutton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        stock.Show()
        Me.Close()
    End Sub

    Private Sub iconbutton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        dashboard.Show()
        dashboard.IconButton9.Visible = True
        dashboard.IconButton8.Visible = False
    End Sub
    Private Sub iconbutton4_Click_1(sender As Object, e As EventArgs) Handles IconButton4.Click
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

    Private Sub iconbutton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        devis.Show()
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

    Private Sub iconbutton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        product.Show()
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

    Private Sub t13_TextChanged(sender As Object, e As EventArgs) Handles t13.TextChanged
        Dim inputText As String = t13.Text

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
            RemoveHandler t13.TextChanged, AddressOf t13_TextChanged

            ' Mettez à jour le texte dans le TextBox
            t13.Text = modifiedText

            ' Replacez le curseur à la position correcte après la modification
            t13.SelectionStart = t13.Text.Length

            ' Réactivez le gestionnaire d'événements TextChanged
            AddHandler t13.TextChanged, AddressOf t13_TextChanged
        End If
    End Sub


    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        users.Show()
        Me.Close()

    End Sub

    Private Sub IconButton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        Panel8.Visible = False
    End Sub

    Private Sub DatagridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DatagridView1.CellDoubleClick
        Panel8.Visible = True
        Panel8.BringToFront()
        Label14.Text = DatagridView1.Rows(e.RowIndex).Cells(0).Value
        adpt = New MySqlDataAdapter("select * from charges where year = '" & ComboBox1.Text & "' AND charge = '" & DatagridView1.Rows(e.RowIndex).Cells(0).Value & "' AND Month(date) = '" & e.ColumnIndex - 1 & "' order by date desc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        DataGridView4.Rows.Clear()
        Dim mnt As Double = 0

        For i = 0 To table.Rows.Count() - 1
            If e.ColumnIndex = 2 Then
                mnt = Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ",")).ToString("N2")
            End If
            If e.ColumnIndex = 3 Then
                mnt = Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ",")).ToString("N2")
            End If
            If e.ColumnIndex = 4 Then
                mnt = Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("N2")
            End If
            If e.ColumnIndex = 5 Then
                mnt = Convert.ToDouble(table.Rows(i).Item(5).ToString.Replace(".", ",")).ToString("N2")
            End If
            If e.ColumnIndex = 6 Then
                mnt = Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(".", ",")).ToString("N2")
            End If
            If e.ColumnIndex = 7 Then
                mnt = Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(".", ",")).ToString("N2")
            End If
            If e.ColumnIndex = 8 Then
                mnt = Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(".", ",")).ToString("N2")
            End If
            If e.ColumnIndex = 9 Then
                mnt = Convert.ToDouble(table.Rows(i).Item(9).ToString.Replace(".", ",")).ToString("N2")
            End If
            If e.ColumnIndex = 10 Then
                mnt = Convert.ToDouble(table.Rows(i).Item(10).ToString.Replace(".", ",")).ToString("N2")
            End If
            If e.ColumnIndex = 11 Then
                mnt = Convert.ToDouble(table.Rows(i).Item(11).ToString.Replace(".", ",")).ToString("N2")
            End If
            If e.ColumnIndex = 12 Then
                mnt = Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ",")).ToString("N2")
            End If
            If e.ColumnIndex = 13 Then
                mnt = Convert.ToDouble(table.Rows(i).Item(13).ToString.Replace(".", ",")).ToString("N2")
            End If
            DataGridView4.Rows.Add(Convert.ToDateTime(table.Rows(i).Item(15)).ToString("yyyy/MM/dd"), table.Rows(i).Item(17), mnt.ToString("N2"), table.Rows(i).Item(0), "X")
        Next
    End Sub

    Private Sub DataGridView4_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellDoubleClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 4 Then
                Dim Rep As Integer
                Rep = MsgBox("Voulez-vous vraiment supprimer cette charges ?", vbYesNo)
                If Rep = vbYes Then
                    conn2.Close()
                    conn2.Open()
                    Dim sql2 As String
                    Dim cmd2 As MySqlCommand
                    sql2 = "delete from charges where id = '" & DataGridView4.Rows(e.RowIndex).Cells(3).Value & "' "
                    cmd2 = New MySqlCommand(sql2, conn2)

                    cmd2.ExecuteNonQuery()

                    sql2 = "delete from cheques where REPLACE(achat,'Charge','') = '" & DataGridView4.Rows(e.RowIndex).Cells(3).Value & "' "
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
                    cmd2.Parameters.AddWithValue("@op", "Suppression de la charge : " & Label14.Text & " - " & DataGridView4.Rows(e.RowIndex).Cells(1).Value)
                    cmd2.ExecuteNonQuery()

                    conn2.Close()
                    DataGridView4.Rows.RemoveAt(e.RowIndex)
                    adpt = New MySqlDataAdapter("select `id`, `charge`, sum(`j`), sum(`f`), sum(`m`), sum(`a`), sum(`mai`), sum(`juin`), sum(`juil`), sum(`ao`), sum(`sept`), sum(`oct`), sum(`nov`), sum(`dece`), `year`, `date`, `mode` from charges where `year` = '" + ComboBox1.Text + "' group by charge order by charge asc", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    Dim sumchrgannul, chrgannul, to0, to1, to2, to3, to4, to5, to6, to7, tot7, to8, to9, to10, to11, to12 As Double
                    Dim summa, ma1, ma2, ma3, ma4, ma5, ma6, ma7, mat7, ma8, ma9, ma10, ma11, ma12 As Double
                    summa = 0
                    Dim currentDate As DateTime = DateTime.Now

                    adpt = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(marge,',','.'),' ','')),Month(date) from orderdetails WHERE Year(date) = @year group by Month(date)", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@year", currentDate.Year)
                    Dim tablemargemois As New DataTable
                    adpt.Fill(tablemargemois)
                    For i = 0 To tablemargemois.Rows.Count - 1
                        summa += tablemargemois.Rows(i).Item(0)
                        If tablemargemois.Rows(i).Item(1) = 1 Then
                            If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                                ma1 = 0
                            Else
                                ma1 = tablemargemois.Rows(i).Item(0)
                            End If
                        End If
                        If tablemargemois.Rows(i).Item(1) = 2 Then
                            If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                                ma2 = 0
                            Else
                                ma2 = tablemargemois.Rows(i).Item(0)
                            End If
                        End If
                        If tablemargemois.Rows(i).Item(1) = 3 Then
                            If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                                ma3 = 0
                            Else
                                ma3 = tablemargemois.Rows(i).Item(0)
                            End If
                        End If
                        If tablemargemois.Rows(i).Item(1) = 4 Then
                            If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                                ma4 = 0
                            Else
                                ma4 = tablemargemois.Rows(i).Item(0)
                            End If
                        End If
                        If tablemargemois.Rows(i).Item(1) = 5 Then
                            If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                                ma5 = 0
                            Else
                                ma5 = tablemargemois.Rows(i).Item(0)
                            End If
                        End If
                        If tablemargemois.Rows(i).Item(1) = 6 Then
                            If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                                ma6 = 0
                            Else
                                ma6 = tablemargemois.Rows(i).Item(0)
                            End If
                        End If
                        If tablemargemois.Rows(i).Item(1) = 7 Then
                            If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                                ma7 = 0
                            Else
                                ma7 = tablemargemois.Rows(i).Item(0)
                            End If
                        End If
                        If tablemargemois.Rows(i).Item(1) = 8 Then
                            If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                                ma8 = 0
                            Else
                                ma8 = tablemargemois.Rows(i).Item(0)
                            End If
                        End If
                        If tablemargemois.Rows(i).Item(1) = 9 Then
                            If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                                ma9 = 0
                            Else
                                ma9 = tablemargemois.Rows(i).Item(0)
                            End If
                        End If
                        If tablemargemois.Rows(i).Item(1) = 10 Then
                            If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                                ma10 = 0
                            Else
                                ma10 = tablemargemois.Rows(i).Item(0)
                            End If
                        End If
                        If tablemargemois.Rows(i).Item(1) = 11 Then
                            If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                                ma11 = 0
                            Else
                                ma11 = tablemargemois.Rows(i).Item(0)
                            End If
                        End If
                        If tablemargemois.Rows(i).Item(1) = 12 Then
                            If IsDBNull(tablemargemois.Rows(i).Item(0)) Then
                                ma12 = 0
                            Else
                                ma12 = tablemargemois.Rows(i).Item(0)
                            End If
                        End If

                    Next

                    DatagridView1.Rows.Clear()
                    If table.Rows.Count <> 0 Then

                        For i = 0 To table.Rows.Count - 1
                            sumchrgannul += Convert.ToDouble(table.Rows(i).Item(2)) + Convert.ToDouble(table.Rows(i).Item(3)) + Convert.ToDouble(table.Rows(i).Item(4)) + Convert.ToDouble(table.Rows(i).Item(5)) + Convert.ToDouble(table.Rows(i).Item(6)) + Convert.ToDouble(table.Rows(i).Item(7)) + Convert.ToDouble(table.Rows(i).Item(8)) + Convert.ToDouble(table.Rows(i).Item(9)) + Convert.ToDouble(table.Rows(i).Item(10)) + Convert.ToDouble(table.Rows(i).Item(11)) + Convert.ToDouble(table.Rows(i).Item(12)) + Convert.ToDouble(table.Rows(i).Item(13))
                            chrgannul = Convert.ToDouble(table.Rows(i).Item(2)) + Convert.ToDouble(table.Rows(i).Item(3)) + Convert.ToDouble(table.Rows(i).Item(4)) + Convert.ToDouble(table.Rows(i).Item(5)) + Convert.ToDouble(table.Rows(i).Item(6)) + Convert.ToDouble(table.Rows(i).Item(7)) + Convert.ToDouble(table.Rows(i).Item(8)) + Convert.ToDouble(table.Rows(i).Item(9)) + Convert.ToDouble(table.Rows(i).Item(10)) + Convert.ToDouble(table.Rows(i).Item(11)) + Convert.ToDouble(table.Rows(i).Item(12)) + Convert.ToDouble(table.Rows(i).Item(13))
                            DatagridView1.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(chrgannul).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(2)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(3)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(4)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(5)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(6)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(7)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(8)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(9)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(10)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(11)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(12)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(13)).ToString("# ##0.00").Replace(".", ","), table.Rows(i).Item(0))

                            to0 += Convert.ToDouble(chrgannul.ToString.Replace(".", ","))
                            to1 += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
                            to2 += Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ","))
                            to3 += Convert.ToDouble(table.Rows(i).Item(4).ToString.Replace(".", ","))
                            to4 += Convert.ToDouble(table.Rows(i).Item(5).ToString.Replace(".", ","))
                            to5 += Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(".", ","))
                            to6 += Convert.ToDouble(table.Rows(i).Item(7).ToString.Replace(".", ","))
                            tot7 += Convert.ToDouble(table.Rows(i).Item(8).ToString.Replace(".", ","))
                            to8 += Convert.ToDouble(table.Rows(i).Item(9).ToString.Replace(".", ","))
                            to9 += Convert.ToDouble(table.Rows(i).Item(10).ToString.Replace(".", ","))
                            to10 += Convert.ToDouble(table.Rows(i).Item(11).ToString.Replace(".", ","))
                            to11 += Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ","))
                            to12 += Convert.ToDouble(table.Rows(i).Item(13).ToString.Replace(".", ","))


                        Next
                        DatagridView1.Rows.Add()
                        DatagridView1.Rows.Add("TOTAL DES CHARGES", to0, to1, to2, to3, to4, to5, to6, tot7, to8, to9, to10, to11, to12, Nothing)
                        DatagridView1.Rows.Add("TOTAL DES MARGES", summa, ma1, ma2, ma3, ma4, ma5, ma6, ma7, ma8, ma9, ma10, ma11, ma12, Nothing)
                        DatagridView1.Rows.Add("TOTAL NET", summa - to0, ma1 - to1, ma2 - to2, ma3 - to3, ma4 - to4, ma5 - to5, ma6 - to6, ma7 - tot7, ma8 - to8, ma9 - to9, ma10 - to10, ma11 - to11, ma12 - to12, Nothing)
                    End If
                    Dim columnIndexToColor As Integer = 0

                    For i = 0 To DatagridView1.Rows.Count - 1
                        If i >= DatagridView1.RowCount - 3 Then
                            DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Khaki ' Remplacez Color.Yellow par la couleur de votre choix
                        End If
                        If i = DatagridView1.RowCount - 1 Then
                            DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Gold ' Remplacez Color.Yellow par la couleur de votre choix
                        End If
                    Next

                    IconButton16.Text = sumchrgannul.ToString("N2") & " DHs"

                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DatagridView1.CellEnter
        DatagridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen
        For i = 0 To DatagridView1.Rows.Count - 1
            If i >= DatagridView1.RowCount - 3 Then
                DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Khaki ' Remplacez Color.Yellow par la couleur de votre choix
            End If
            If i = DatagridView1.RowCount - 1 Then
                DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Gold ' Remplacez Color.Yellow par la couleur de votre choix
            End If
        Next
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DatagridView1.CellMouseClick
        If e.RowIndex > 0 Then
            DatagridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen
            For i = 0 To DatagridView1.Rows.Count - 1
                If i >= DatagridView1.RowCount - 3 Then
                    DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Khaki ' Remplacez Color.Yellow par la couleur de votre choix
                End If
                If i = DatagridView1.RowCount - 1 Then
                    DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Gold ' Remplacez Color.Yellow par la couleur de votre choix
                End If
            Next
        End If

    End Sub

    Private Sub DataGridView1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DatagridView1.CellBeginEdit
        DatagridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen
        For i = 0 To DatagridView1.Rows.Count - 1
            If i >= DatagridView1.RowCount - 3 Then
                DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Khaki ' Remplacez Color.Yellow par la couleur de votre choix
            End If
            If i = DatagridView1.RowCount - 1 Then
                DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Gold ' Remplacez Color.Yellow par la couleur de votre choix
            End If
        Next
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.Text = "Espèce (Comptoir)" Or ComboBox2.Text = "Espèce (Coffre)" Then
            tpepanel.Visible = False
            chqpanel.Visible = False
        End If

        If ComboBox2.Text = "Chèque" Or ComboBox2.Text = "Virement" Or ComboBox2.Text = "Effet" Then
            tpepanel.Visible = False
            chqpanel.Visible = True
        End If

        If ComboBox2.Text = "TPE" Then
            tpepanel.Visible = True
            chqpanel.Visible = False
        End If
    End Sub

    Private Sub IconButton26_Click(sender As Object, e As EventArgs) Handles IconButton26.Click
        tpepanel.Visible = False
    End Sub

    Private Sub IconButton28_Click(sender As Object, e As EventArgs) Handles IconButton28.Click
        chqpanel.Visible = False
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        adpt = New MySqlDataAdapter("select * from banques where organisme = '" & ComboBox7.Text & "'", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        TextBox16.Text = table.Rows(0).Item(2)
        TextBox15.Text = table.Rows(0).Item(3)
        TextBox8.Text = ComboBox7.Text
    End Sub

    Private Sub DataGridView1_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DatagridView1.CellLeave
        DatagridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
        For i = 0 To DatagridView1.Rows.Count - 1
            If i >= DatagridView1.RowCount - 3 Then
                DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Khaki ' Remplacez Color.Yellow par la couleur de votre choix
            End If
            If i = DatagridView1.RowCount - 1 Then
                DatagridView1.Rows(i).DefaultCellStyle.BackColor = Color.Gold ' Remplacez Color.Yellow par la couleur de votre choix
            End If
        Next
    End Sub

End Class