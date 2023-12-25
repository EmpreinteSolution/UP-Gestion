Imports MySql.Data.MySqlClient

Public Class alertes
    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        DataGridView2.Visible = False
        DataGridView1.Visible = True
        Me.Close()
    End Sub
    Dim adpt1, adpt2 As MySqlDataAdapter

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs)
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

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

    Private Sub alertes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt1 = New MySqlDataAdapter("select * from cheques WHERE fac IS NULL and etat = @day and organisme <> 'tpe'", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", 0)
        Dim table1 As New DataTable
        adpt1.Fill(table1)
        For i = 0 To table1.Rows.Count() - 1
            adpt2 = New MySqlDataAdapter("select * from achat WHERE id = @day ", conn2)
            adpt2.SelectCommand.Parameters.Clear()
            adpt2.SelectCommand.Parameters.AddWithValue("@day", table1.Rows(i).Item(8))
            Dim table2 As New DataTable
            adpt2.Fill(table2)
            If table2.Rows.Count() <> 0 Then
                Dim ech As String = DateDiff(DateInterval.Day, Now.Date, table1.Rows(i).Item(5))
                DataGridView1.Rows.Add(table1.Rows(i).Item(0), table1.Rows(i).Item(4), table1.Rows(i).Item(12) & " DHs", Convert.ToDateTime(table1.Rows(i).Item(5)).ToString("dd/MM/yyyy"), ech & " jrs", table2.Rows(0).Item(2), table1.Rows(i).Item(10))
            End If
        Next

        adpt1 = New MySqlDataAdapter("select * from cheques WHERE achat IS NULL and etat = @day and organisme <> 'tpe'", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", 0)
        Dim table3 As New DataTable
        adpt1.Fill(table3)
        For i = 0 To table3.Rows.Count() - 1
            adpt2 = New MySqlDataAdapter("select * from facture WHERE OrderID = @day ", conn2)
            adpt2.SelectCommand.Parameters.Clear()
            adpt2.SelectCommand.Parameters.AddWithValue("@day", table3.Rows(i).Item(7))
            Dim table2 As New DataTable
            adpt2.Fill(table2)
            If table2.Rows.Count() <> 0 Then

                Dim ech2 As String = DateDiff(DateInterval.Day, Now.Date, table3.Rows(i).Item(5))

                DataGridView2.Rows.Add(table3.Rows(i).Item(0).ToString, table3.Rows(i).Item(4), table3.Rows(i).Item(12) & " DHs", Convert.ToDateTime(table3.Rows(i).Item(5)).ToString("dd/MM/yyyy"), ech2 & " jrs", table2.Rows(0).Item(5), table3.Rows(i).Item(10))
            End If
        Next
    End Sub
End Class