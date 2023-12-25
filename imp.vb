Imports System.IO
Imports ExcelDataReader
Imports MySql.Data.MySqlClient

Public Class imp
    Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim conn2 As New MySqlConnection("datasource=localhost; username=root; password=; database=librairie_rafiq")
    Dim adpt, adpt2 As MySqlDataAdapter
    Dim tables As DataTableCollection

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Using ofd As OpenFileDialog = New OpenFileDialog()
            If ofd.ShowDialog = DialogResult.OK Then
                Try


                    Using stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read)
                        Using Reader As IExcelDataReader = ExcelReaderFactory.CreateReader(stream)
                            Dim Result As DataSet = Reader.AsDataSet(New ExcelDataSetConfiguration() With {
                                     .ConfigureDataTable = Function(__) New ExcelDataTableConfiguration() With {
                                     .UseHeaderRow = True
                                       }})

                            tables = Result.Tables

                            ComboBox1.Items.Clear()
                            For Each table As DataTable In tables
                                ComboBox1.Items.Add(table.TableName)
                            Next
                        End Using
                    End Using

                    TextBox1.Text = Path.GetFileName(ofd.FileName).Replace(".xls", "")
                    ComboBox1.SelectedIndex = 0
                Catch ex As Exception
                    MsgBox("le fichier et deja ouvert dans une autre application")
                End Try
            End If
        End Using
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim dt As DataTable = tables(ComboBox1.SelectedItem.ToString)
        dt.Columns.RemoveAt(3)
        dt.Columns.RemoveAt(4)
        dt.Columns.RemoveAt(6)
        dt.Columns.RemoveAt(6)
        dt.Columns.RemoveAt(6)
        dt.Columns.RemoveAt(6)
        dt.Columns.RemoveAt(6)


        DataGridView3.DataSource = dt
    End Sub
    Dim sql2, sql3 As String

    Private Sub imp_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Dim cmd2, cmd3 As MySqlCommand
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        conn2.Open()
        For i = 0 To DataGridView3.Rows.Count - 1
            sql2 = "INSERT INTO products (id,Code, name, price, fournisseur, cat) VALUES (@value1, @value2, @value3, @value4, @value5, @value6)"
            cmd2 = New MySqlCommand(sql2, conn2)

            cmd2.Parameters.Add("@value1", MySqlDbType.VarChar).Value = DataGridView3.Rows(i).Cells(0).Value
            cmd2.Parameters.Add("@value2", MySqlDbType.VarChar).Value = DataGridView3.Rows(i).Cells(1).Value
            cmd2.Parameters.Add("@value3", MySqlDbType.VarChar).Value = DataGridView3.Rows(i).Cells(2).Value
            cmd2.Parameters.Add("@value4", MySqlDbType.VarChar).Value = DataGridView3.Rows(i).Cells(3).Value
            cmd2.Parameters.Add("@value5", MySqlDbType.VarChar).Value = DataGridView3.Rows(i).Cells(4).Value
            cmd2.Parameters.Add("@value6", MySqlDbType.VarChar).Value = TextBox1.Text


            cmd2.ExecuteNonQuery()
        Next

        Dim sql As String
        Dim cmd As MySqlCommand
        sql = "INSERT INTO categories (name) VALUES ('" & TextBox1.Text & "')"
        cmd = New MySqlCommand(sql, conn2)

        cmd.ExecuteNonQuery()

        conn2.Close()

        MsgBox("Opération bien faites")
    End Sub
End Class