Imports System.IO
Imports MySql.Data.MySqlClient

Public Class opentr
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Dim sql2, sql3 As String

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Close()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker3.ValueChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        adpt = New MySqlDataAdapter("select * from tiroir where date BETWEEN '" & Convert.ToDateTime(DateTimePicker4.Value.Date).ToString("yyyy-MM-dd") & "' AND '" & Convert.ToDateTime(DateTimePicker3.Value.Date.AddDays(1)).ToString("yyyy-MM-dd") & "' order by id desc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double
        DataGridView2.Rows.Clear()
        For i = 0 To table.Rows.Count() - 1
            DataGridView2.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(6), table.Rows(i).Item(2), Format(table.Rows(i).Item(3), "dd/MM/yyyy HH:mm"), table.Rows(i).Item(4), "X")
            sum = sum + table.Rows(i).Item(2).ToString.Replace(".", ",")
        Next
        Label9.Text = sum.ToString("# ##0.00") & " DHs"
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        adpt = New MySqlDataAdapter("select * from archivecaisse where date BETWEEN '" & Convert.ToDateTime(DateTimePicker4.Value.Date).ToString("yyyy-MM-dd") & "' AND '" & Convert.ToDateTime(DateTimePicker3.Value.Date.AddDays(1)).ToString("yyyy-MM-dd") & "' order by id desc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        DataGridView1.Rows.Clear()
        Dim pointage As String
        Dim z As Double = 0
        Dim dec As Double = 0
        Dim ecart As Double = 0
        For i = 0 To table.Rows.Count() - 1
            If table.Rows(i).Item(5) = 0 Then
                pointage = "..."
            Else
                pointage = "Pointé le " & Format(table.Rows(i).Item(1), "dd/MM/yyyy")
            End If
            DataGridView1.Rows.Add(table.Rows(i).Item(0), Format(table.Rows(i).Item(1), "dd/MM/yyyy HH:mm"), table.Rows(i).Item(4), Convert.ToDecimal(table.Rows(i).Item(2).ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"), Convert.ToDecimal(table.Rows(i).Item(3).ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"), Convert.ToDecimal(table.Rows(i).Item(6).ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"), pointage)
            z = z + Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ",").Replace(" ", ""))
            dec = dec + Convert.ToDouble(table.Rows(i).Item(3).ToString.Replace(".", ",").Replace(" ", ""))
            ecart = ecart + Convert.ToDouble(table.Rows(i).Item(6).ToString.Replace(".", ",").Replace(" ", ""))
        Next
        TextBox20.Text = z.ToString("N2")
        TextBox2.Text = dec.ToString("N2")
        TextBox3.Text = ecart.ToString("N2")
    End Sub

    Private Sub Panel6_Paint(sender As Object, e As PaintEventArgs) Handles Panel6.Paint

    End Sub

    Dim cmd2, cmd3 As MySqlCommand

    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs) Handles Panel7.Paint

    End Sub
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
    Private Sub datagridview3_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView3.CellPainting
        ' Vérifiez si c'est une ligne de données (pas la ligne d'en-tête ni la ligne de total)
        If e.RowIndex >= 0 Then
            ' Supprimez les bordures des cellules sauf pour la dernière ligne
            If e.RowIndex < DataGridView3.Rows.Count - 1 Then
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
    Private Sub opentr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select * from tiroir where day(tiroir.date) = '" & DateTime.Today.Day & "' and Month(tiroir.date) = '" & DateTime.Today.Month & "' and year(tiroir.date) = '" & DateTime.Today.Year & "' order by id desc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double
        DataGridView2.Rows.Clear()
        For i = 0 To table.Rows.Count() - 1
            DataGridView2.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(6), table.Rows(i).Item(2), Format(table.Rows(i).Item(3), "dd/MM/yyyy HH:mm"), table.Rows(i).Item(4), "X")
            sum = sum + table.Rows(i).Item(2).ToString.Replace(".", ",")
        Next
        Label9.Text = sum.ToString("# ##0.00") & " DHs"
    End Sub

    Private Sub DataGridView2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick

    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim rmsfac As Double = 0
        Dim rmstir As Double = 0
        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`remise`,',','.'),' ','')), client from facture where remise > 0 and OrderDate BETWEEN '" & Convert.ToDateTime(DateTimePicker2.Value).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(DateTimePicker1.Value.AddDays(+1)).ToString("yyyy-MM-dd") & "' Group by client order by client asc ", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        DataGridView5.Rows.Clear()
        For i = 0 To table.Rows.Count() - 1
            adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`mtn`,',','.'),' ','')) from tiroir where cause = 'Remise client' and remarque = '" & table.Rows(i).Item(1) & "' and date BETWEEN '" & Convert.ToDateTime(DateTimePicker2.Value).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(DateTimePicker1.Value.AddDays(+1)).ToString("yyyy-MM-dd") & "' ", conn2)
            Dim table2 As New DataTable
            adpt.Fill(table2)
            If table2.Rows.Count <> 0 Then
                DataGridView5.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(table.Rows(i).Item(0).ToString.Replace(".", ",") + table2.Rows(0).Item(0).ToString.Replace(".", ",")).ToString("N2"))
                rmsfac = rmsfac + Convert.ToDouble(table.Rows(i).Item(0).ToString.Replace(".", ",") + table2.Rows(0).Item(0).ToString.Replace(".", ","))
            Else
                DataGridView5.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(table.Rows(i).Item(0).ToString.Replace(".", ",")).ToString("N2"))
                rmsfac = rmsfac + Convert.ToDouble(table.Rows(i).Item(0).ToString.Replace(".", ","))

            End If
        Next
        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`mtn`,',','.'),' ','')), remarque from tiroir where cause = 'Remise client' and date BETWEEN '" & Convert.ToDateTime(DateTimePicker2.Value).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(DateTimePicker1.Value.AddDays(+1)).ToString("yyyy-MM-dd") & "' Group by remarque order by remarque asc ", conn2)
        Dim table3 As New DataTable
        adpt.Fill(table3)

        For i = 0 To table3.Rows.Count() - 1
            adpt = New MySqlDataAdapter("select * from facture where remise > 0 and client = '" & table3.Rows(i).Item(1) & "' and OrderDate BETWEEN '" & Convert.ToDateTime(DateTimePicker2.Value).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(DateTimePicker1.Value.AddDays(+1)).ToString("yyyy-MM-dd") & "' ", conn2)
            Dim table2 As New DataTable
            adpt.Fill(table2)
            If table2.Rows.Count <> 0 Then
            Else
                DataGridView5.Rows.Add(table3.Rows(i).Item(1), Convert.ToDouble(table3.Rows(i).Item(0).ToString.Replace(".", ",")).ToString("N2"))
                rmstir = rmstir + Convert.ToDouble(table3.Rows(i).Item(0).ToString.Replace(".", ","))
            End If
        Next


        TextBox5.Text = (rmsfac + rmstir).ToString("N2")
    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 6 Then
                If DataGridView1.Rows(e.RowIndex).Cells(6).Value = "..." Then
                    Dim Rep As Integer
                    Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Rep = MsgBox("Voulez-vous vraiment pointer cette cloture ?", vbYesNo)
                    If Rep = vbYes Then

                        conn2.Close()
                        conn2.Open()
                        Dim sql2, sql3 As String
                        Dim cmd2, cmd3 As MySqlCommand
                        sql2 = "UPDATE `archivecaisse` SET `caisse`= '" & DataGridView1.Rows(e.RowIndex).Cells(4).Value & "', `ecart` = '" & DataGridView1.Rows(e.RowIndex).Cells(5).Value & "' ,`etat`=1,`date_confirm`='" + Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE id = '" + row.Cells(0).Value.ToString + "' "
                        cmd2 = New MySqlCommand(sql2, conn2)
                        cmd2.ExecuteNonQuery()

                        adpt = New MySqlDataAdapter("select id_op from caisse order by id desc limit 1", conn2)
                        Dim table As New DataTable
                        adpt.Fill(table)

                        Dim ope As Integer
                        If table.Rows.Count <> 0 Then
                            ope = table.Rows(0).Item(0) + 1
                        Else
                            ope = 1
                        End If

                        conn2.Close()
                        IconButton2.PerformClick()
                    End If
                End If
                'Else
                '    Dim Rep As Integer
                '    Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                '    Rep = MsgBox("Voulez-vous vraiment annuler ce pointage ?", vbYesNo)
                '    If Rep = vbYes Then

                '        conn2.close()  conn2.open()
                '        Dim sql2, sql3 As String
                '        Dim cmd2, cmd3 As MySqlCommand
                '        sql2 = "UPDATE `archivecaisse` SET `caisse`= '" & DataGridView1.Rows(e.RowIndex).Cells(4).Value & "', `ecart` = '" & DataGridView1.Rows(e.RowIndex).Cells(5).Value & "', `etat`=0,`date_confirm`= NULL WHERE id = '" + row.Cells(0).Value.ToString + "' "
                '        cmd2 = New MySqlCommand(sql2, conn2)
                '        cmd2.ExecuteNonQuery()
                '        conn2.Close()
                '        IconButton2.PerformClick()
                '    End If
                'End If

            End If
        End If
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        If e.RowIndex >= 0 Then
            Dim declared As Decimal = Convert.ToDecimal(DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ","))
            Dim z As Decimal = Convert.ToDecimal(DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ","))
            DataGridView1.Rows(e.RowIndex).Cells(5).Value = Convert.ToDecimal(declared.ToString.Replace(" ", "").Replace(".", ",")) - Convert.ToDecimal(z.ToString.Replace(" ", "").Replace(".", ","))
        End If
    End Sub

    Private Sub DataGridView4_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellDoubleClick
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Validation des R% Clients'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Dim Rep As Integer
                Rep = MsgBox("Voullez vous vraiment valider cette remise ?", vbYesNo)
                If Rep = vbYes Then

                    conn2.Open()
                    Dim sql2 As String
                    Dim cmd2 As MySqlCommand
                    sql2 = "INSERT INTO tiroir (`cause`, `mtn`, `date`, `caissier`,`remarque`) VALUES ('Remise client','" + DataGridView4.Rows(e.RowIndex).Cells(1).Value + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" + dashboard.Label2.Text + "','" & DataGridView4.Rows(e.RowIndex).Cells(0).Value & "')"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()
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
                    cmd2.Parameters.AddWithValue("@v1", "Remise client")
                    cmd2.Parameters.AddWithValue("@v3", DataGridView4.Rows(e.RowIndex).Cells(1).Value)
                    cmd2.Parameters.AddWithValue("@v9", DataGridView4.Rows(e.RowIndex).Cells(0).Value)
                    cmd2.Parameters.AddWithValue("@v6", DateTime.Today.Year)
                    cmd2.Parameters.AddWithValue("@v7", DateTime.Today)
                    cmd2.Parameters.AddWithValue("@v8", "Espèce")

                    cmd2.ExecuteNonQuery()


                    sql3 = "UPDATE `clients` SET `remisepour` = @pourc, `remise`=@rms WHERE `client` = @id"
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@id", DataGridView4.Rows(e.RowIndex).Cells(0).Value)
                    cmd3.Parameters.AddWithValue("@pourc", 0)
                    cmd3.Parameters.AddWithValue("@rms", 0)

                    cmd3.ExecuteNonQuery()


                    conn2.Close()
                    DataGridView4.Rows.RemoveAt(e.RowIndex)
                End If
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If


    End Sub

    Private Sub DataGridView2_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseDoubleClick
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Supprimer une depense caisse'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                If e.RowIndex >= 0 Then


                    If e.ColumnIndex = 6 Then
                        Dim Rep As Integer
                        Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                        Rep = MsgBox("Voulez-vous vraiment supprimer cet dépense ?", vbYesNo)
                        If Rep = vbYes Then

                            conn2.Close()
                            conn2.Open()
                            Dim sql2, sql3 As String
                            Dim cmd2, cmd3 As MySqlCommand
                            sql2 = "delete from `tiroir` WHERE `id` = '" + row.Cells(0).Value.ToString + "' "
                            cmd2 = New MySqlCommand(sql2, conn2)
                            cmd2.ExecuteNonQuery()

                            sql2 = "delete from charges where charge = '" & row.Cells(1).Value.ToString & "' and date = '" & Convert.ToDateTime(row.Cells(4).Value.ToString).ToString("yyyy-MM-dd") & "'"
                            cmd2 = New MySqlCommand(sql2, conn2)

                            cmd2.ExecuteNonQuery()

                            conn2.Close()

                            DataGridView2.Rows.RemoveAt(e.RowIndex)
                        End If
                    End If
                End If
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub
End Class