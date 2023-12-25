Imports System.IO
Imports MySql.Data.MySqlClient

Public Class credi
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Dim sql2, sql3 As String

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        DataGridView2.Rows.Clear()

        Label2.Text = ""
        Label4.Text = "000 000,00"
        Label12.Text = "-"
        Label6.Text = "000 000,00"
        TextBox1.Text = ""
        TextBox2.Text = ""
        Me.Close()
    End Sub


    Dim cmd2, cmd3 As MySqlCommand

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click

        Dim pay As Double = Convert.ToDouble(TextBox2.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")

        conn2.Open()

        ' Vérifier si la case à cocher de la ligne est cochée

        Dim sql3 As String
        Dim cmd3 As MySqlCommand



        sql3 = "INSERT INTO `encaissement`(`client`, `montant`,`user`) VALUES (@id, @credit,@user) "
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.Parameters.Clear()
        cmd3.Parameters.AddWithValue("@id", Label12.Text)
        cmd3.Parameters.AddWithValue("@user", dashboard.Label2.Text)
        cmd3.Parameters.AddWithValue("@credit", pay)
        cmd3.ExecuteNonQuery()



        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and etat = @etat and client = @clt order by OrderID desc", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
        adpt.SelectCommand.Parameters.AddWithValue("@clt", Label12.Text)
        Dim tabletick As New DataTable
        adpt.Fill(tabletick)
        Dim resteencaisse As Decimal = pay
        For i = 0 To tabletick.Rows.Count - 1
            sql3 = "UPDATE `orders` SET `paye`=@paye,`modePayement`=@mode,`reste`=@reste WHERE `OrderID` = @id"
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            cmd3.Parameters.AddWithValue("@id", tabletick.Rows(i).Item(0))
            If resteencaisse >= Convert.ToDecimal(tabletick.Rows(i).Item(2).ToString.Replace(".", ",").Replace(" ", "")) Then
                cmd3.Parameters.AddWithValue("@paye", Convert.ToDecimal(tabletick.Rows(i).Item(2).ToString.Replace(".", ",").Replace(" ", "")))
                cmd3.Parameters.AddWithValue("@reste", 0.00)
                cmd3.Parameters.AddWithValue("@mode", "Espèce")
            Else
                cmd3.Parameters.AddWithValue("@paye", resteencaisse)
                cmd3.Parameters.AddWithValue("@reste", Convert.ToDecimal(tabletick.Rows(i).Item(2).ToString.Replace(".", ",").Replace(" ", "")) - resteencaisse)
                cmd3.Parameters.AddWithValue("@mode", "Crédit")
            End If
            cmd3.ExecuteNonQuery()
            resteencaisse = resteencaisse - Convert.ToDecimal(tabletick.Rows(i).Item(13).ToString.Replace(".", ",").Replace(" ", ""))
            If resteencaisse <= 0 Then
                Exit For
            End If
        Next
        conn2.Close()
        MsgBox("Encaissement bien effectué !")
        adpt = New MySqlDataAdapter("select * from clients order by client asc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        DataGridView2.Rows.Clear()
        For i = 0 To table.Rows.Count() - 1
            adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and client = @clt order by OrderID desc", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
            adpt.SelectCommand.Parameters.AddWithValue("@clt", table.Rows(i).Item(1))
            Dim tablo As New DataTable
            adpt.Fill(tablo)
            Dim sum As Double = 0
            Dim cnt As Integer = 0
            For j = 0 To tablo.Rows.Count - 1
                Dim t As Decimal = 0
                t = Convert.ToDecimal(tablo.Rows(j).Item(13).Replace(".", ","))
                sum += t
            Next
            adpt = New MySqlDataAdapter("select sum(montant) from encaissement where client = '" & table.Rows(i).Item(1) & "'", conn2)
            Dim tableo As New DataTable
            adpt.Fill(tableo)
            Dim paye As Double = 0
            If IsDBNull(tableo.Rows(0).Item(0)) Then
            Else
                paye = Convert.ToDouble(tableo.Rows(0).Item(0).ToString.Replace(".", ",").Replace(" ", "")).ToString("#0.00")

            End If
            DataGridView2.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")) - paye, table.Rows(i).Item(0), table.Rows(i).Item(17))
        Next
        Label2.Text = ""
        Label12.Text = "-"
        Label4.Text = "000 000,00"
        Label6.Text = "000 000,00"
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        If TextBox1.Text = "" Then
            adpt = New MySqlDataAdapter("select * from clients order by client asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            DataGridView2.Rows.Clear()
            For i = 0 To table.Rows.Count() - 1
                adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and client = @clt order by OrderID desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
                adpt.SelectCommand.Parameters.AddWithValue("@clt", table.Rows(i).Item(1))
                Dim tablo As New DataTable
                adpt.Fill(tablo)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                For j = 0 To tablo.Rows.Count - 1
                    Dim t As Decimal = 0
                    t = Convert.ToDecimal(tablo.Rows(j).Item(13).Replace(".", ","))
                    sum += t
                Next
                adpt = New MySqlDataAdapter("select sum(montant) from encaissement where client = '" & table.Rows(i).Item(1) & "'", conn2)
                Dim tableo As New DataTable
                adpt.Fill(tableo)
                Dim paye As Double = 0
                If IsDBNull(tableo.Rows(0).Item(0)) Then
                Else
                    paye = Convert.ToDouble(tableo.Rows(0).Item(0).ToString.Replace(".", ",").Replace(" ", "")).ToString("#0.00")

                End If
                DataGridView2.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")) - paye, table.Rows(i).Item(0), table.Rows(i).Item(17))
            Next


        Else
            adpt = New MySqlDataAdapter("select * from clients where client like '%" + TextBox1.Text + "%' order by id desc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count = 0 Then
                MsgBox("Aucun client trouvé")

            Else
                DataGridView2.Rows.Clear()
                For i = 0 To table.Rows.Count() - 1
                    adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and client = @clt order by OrderID desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
                    adpt.SelectCommand.Parameters.AddWithValue("@clt", table.Rows(i).Item(1))
                    Dim tablo As New DataTable
                    adpt.Fill(tablo)
                    Dim sum As Double = 0
                    Dim cnt As Integer = 0
                    For j = 0 To tablo.Rows.Count - 1
                        Dim t As Decimal = 0
                        t = Convert.ToDecimal(tablo.Rows(j).Item(13).Replace(".", ","))
                        sum += t
                    Next
                    adpt = New MySqlDataAdapter("select sum(montant) from encaissement where client = '" & table.Rows(i).Item(1) & "'", conn2)
                    Dim tableo As New DataTable
                    adpt.Fill(tableo)
                    Dim paye As Double = 0
                    If IsDBNull(tableo.Rows(0).Item(0)) Then
                    Else
                        paye = Convert.ToDouble(tableo.Rows(0).Item(0).ToString.Replace(".", ",").Replace(" ", "")).ToString("#0.00")

                    End If
                    DataGridView2.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")) - paye, table.Rows(i).Item(0), table.Rows(i).Item(17))
                Next

            End If
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "" Then
                adpt = New MySqlDataAdapter("select * from clients order by client asc", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                DataGridView2.Rows.Clear()
                For i = 0 To table.Rows.Count() - 1
                    adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and client = @clt order by OrderID desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
                    adpt.SelectCommand.Parameters.AddWithValue("@clt", table.Rows(i).Item(1))
                    Dim tablo As New DataTable
                    adpt.Fill(tablo)
                    Dim sum As Double = 0
                    Dim cnt As Integer = 0
                    For j = 0 To tablo.Rows.Count - 1
                        Dim t As Decimal = 0
                        t = Convert.ToDecimal(tablo.Rows(j).Item(13).Replace(".", ","))
                        sum += t
                    Next
                    adpt = New MySqlDataAdapter("select sum(montant) from encaissement where client = '" & table.Rows(i).Item(1) & "'", conn2)
                    Dim tableo As New DataTable
                    adpt.Fill(tableo)
                    Dim paye As Double = 0
                    If IsDBNull(tableo.Rows(0).Item(0)) Then
                    Else
                        paye = Convert.ToDouble(tableo.Rows(0).Item(0).ToString.Replace(".", ",").Replace(" ", "")).ToString("#0.00")

                    End If
                    DataGridView2.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")) - paye, table.Rows(i).Item(0), table.Rows(i).Item(17))
                Next


            Else
                adpt = New MySqlDataAdapter("select * from clients where client like '%" + TextBox1.Text + "%' order by id desc", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count = 0 Then
                    MsgBox("Aucun client trouvé")

                Else
                    DataGridView2.Rows.Clear()
                    For i = 0 To table.Rows.Count() - 1
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and client = @clt order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
                        adpt.SelectCommand.Parameters.AddWithValue("@clt", table.Rows(i).Item(1))
                        Dim tablo As New DataTable
                        adpt.Fill(tablo)
                        Dim sum As Double = 0
                        Dim cnt As Integer = 0
                        For j = 0 To tablo.Rows.Count - 1
                            Dim t As Decimal = 0
                            t = Convert.ToDecimal(tablo.Rows(j).Item(13).Replace(".", ","))
                            sum += t
                        Next
                        adpt = New MySqlDataAdapter("select sum(montant) from encaissement where client = '" & table.Rows(i).Item(1) & "'", conn2)
                        Dim tableo As New DataTable
                        adpt.Fill(tableo)
                        Dim paye As Double = 0
                        If IsDBNull(tableo.Rows(0).Item(0)) Then
                        Else
                            paye = Convert.ToDouble(tableo.Rows(0).Item(0).ToString.Replace(".", ",").Replace(" ", "")).ToString("#0.00")

                        End If
                        DataGridView2.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")) - paye, table.Rows(i).Item(0), table.Rows(i).Item(17))
                    Next

                End If
            End If



        End If
    End Sub

    Private Sub credi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select * from clients order by client asc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        DataGridView2.Rows.Clear()
        For i = 0 To table.Rows.Count() - 1
            adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and client = @clt order by OrderID desc", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
            adpt.SelectCommand.Parameters.AddWithValue("@clt", table.Rows(i).Item(1))
            Dim tablo As New DataTable
            adpt.Fill(tablo)
            Dim sum As Double = 0
            Dim cnt As Integer = 0
            For j = 0 To tablo.Rows.Count - 1
                Dim t As Decimal = 0
                t = Convert.ToDecimal(tablo.Rows(j).Item(13).Replace(".", ","))
                sum += t
            Next
            adpt = New MySqlDataAdapter("select sum(montant) from encaissement where client = '" & table.Rows(i).Item(1) & "'", conn2)
            Dim tableo As New DataTable
            adpt.Fill(tableo)
            Dim paye As Double = 0
            If IsDBNull(tableo.Rows(0).Item(0)) Then
            Else
                paye = Convert.ToDouble(tableo.Rows(0).Item(0).ToString.Replace(".", ",").Replace(" ", "")).ToString("#0.00")

            End If
            DataGridView2.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")) - paye, table.Rows(i).Item(0), table.Rows(i).Item(17))
        Next
    End Sub

    Private Sub DataGridView2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick
        If e.RowIndex >= 0 Then

            Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)

            Label2.Text = row.Cells(2).Value
            Label12.Text = row.Cells(0).Value
            Label4.Text = Convert.ToDouble(row.Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00").Replace(".", ",")
            Label6.Text = Convert.ToDouble(row.Cells(1).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00").Replace(".", ",")
        End If
    End Sub
End Class