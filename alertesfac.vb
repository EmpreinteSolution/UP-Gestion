Imports MySql.Data.MySqlClient

Public Class alertesfac
    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        DataGridView2.Visible = False
        DataGridView1.Visible = True
        ComboBox5.SelectedIndex = 0
        Me.Close()
    End Sub
    Dim adpt1, adpt2 As MySqlDataAdapter

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        If ComboBox5.SelectedIndex = 0 Then
            DataGridView2.Visible = False
            DataGridView1.Visible = True
        Else
            DataGridView2.Visible = True
            DataGridView1.Visible = False
        End If
    End Sub

    Private Sub alertesfac_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox5.SelectedIndex = 0
        adpt1 = New MySqlDataAdapter("select * from achat WHERE reste > @day ", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", 1)
        Dim table1 As New DataTable
        adpt1.Fill(table1)
        For i = 0 To table1.Rows.Count() - 1
            DataGridView1.Rows.Add(table1.Rows(i).Item(0), table1.Rows(i).Item(0), table1.Rows(i).Item(1) & " DHs", Format(table1.Rows(i).Item(3), "dd/MM/yyyy"), table1.Rows(i).Item(2))
        Next

        adpt1 = New MySqlDataAdapter("select * from facture WHERE etat = @day ", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", "Non Payé")
        Dim table2 As New DataTable
        adpt1.Fill(table2)
        For i = 0 To table2.Rows.Count() - 1
            adpt1 = New MySqlDataAdapter("select * from clients WHERE client = @day ", conn2)
            adpt1.SelectCommand.Parameters.Clear()
            adpt1.SelectCommand.Parameters.AddWithValue("@day", table2.Rows(i).Item(5))
            Dim table8 As New DataTable
            adpt1.Fill(table8)
            Dim ech As String = DateDiff(DateInterval.Day, table2.Rows(i).Item(1), Now.Date)
            If ech >= table8.Rows(0).Item(18) Then
                DataGridView2.Rows.Add(table2.Rows(i).Item(0), Convert.ToString("F-" & table2.Rows(i).Item(0)), table2.Rows(i).Item(2) & " DHs", Format(table2.Rows(i).Item(1), "dd/MM/yyyy"), table2.Rows(i).Item(5))
            End If
        Next
    End Sub
End Class