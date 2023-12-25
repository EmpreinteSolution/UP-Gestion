Imports MySql.Data.MySqlClient

Public Class getdata
    Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_dipo_db; password=J2cd@2021; database=u398697865_dipo_db")
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter

    Private Sub getdata_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Dim cmd2 As MySqlCommand
    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        adpt = New MySqlDataAdapter("select * from users", conn)
        Dim table As New DataTable
        adpt.Fill(table)
        conn2.Open()

        For i = 0 To table.Rows.Count - 1
            cmd2 = New MySqlCommand("INSERT INTO `users`( `name`,`badgeID`, `gmail`,`pass`,`roll`, `active`, `ref`) VALUES (@value1,@value2,@value3,@value4,@value5,@value6,@value7)", conn2)
            cmd2.Parameters.AddWithValue("@value1", table.Rows(i).Item(1))
            cmd2.Parameters.AddWithValue("@value2", table.Rows(i).Item(2))
            cmd2.Parameters.AddWithValue("@value3", table.Rows(i).Item(3))
            cmd2.Parameters.AddWithValue("@value4", table.Rows(i).Item(4))
            cmd2.Parameters.AddWithValue("@value5", table.Rows(i).Item(5))
            cmd2.Parameters.AddWithValue("@value6", table.Rows(i).Item(6))
            cmd2.Parameters.AddWithValue("@value7", table.Rows(i).Item(7))



            cmd2.ExecuteNonQuery()
            adpt2 = New MySqlDataAdapter("select * from users", conn2)
            Dim table2 As New DataTable
            adpt2.Fill(table2)
            Label15.Text = table2.Rows.Count
        Next
        conn2.Close()
        MsgBox("sf koulchi daz")

    End Sub
End Class