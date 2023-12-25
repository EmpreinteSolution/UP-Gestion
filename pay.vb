Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Imports System.Configuration
Imports System.Net
Imports System.Net.NetworkInformation

Imports System.Runtime.InteropServices

Public Class pay
    Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt, adpt2 As MySqlDataAdapter

    <Obsolete>
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click


        Dim adpt As New MySqlDataAdapter("select * from clients where id = '" + Label1.Text.ToString + "' ", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)

        Dim credit As Double = Convert.ToDouble(Label2.Text.ToString.Replace(".", ","))
        Dim cliente As String = table2.Rows(0).Item(1)
        Dim pay As Double = TextBox1.Text.ToString.Replace(".", ",")
        Dim tot As Double = Convert.ToDouble(credit) - Convert.ToDouble(pay)

        conn2.Open()
        Dim crdt As Double
        crdt = Convert.ToDouble(Label2.Text.ToString.Replace(".", ",")) + Convert.ToDouble(TextBox1.Text.ToString.Replace(".", ","))
        Dim sql3 As String
        Dim cmd3 As MySqlCommand
        sql3 = "UPDATE `clients` SET `wallet`= @credit WHERE id = @id "
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.Parameters.Clear()
        cmd3.Parameters.AddWithValue("@id", Label1.Text)
        cmd3.Parameters.AddWithValue("@credit", crdt)
        cmd3.ExecuteNonQuery()

        Dim adpt2 As New MySqlDataAdapter("select * from orders where client = '" + Label4.Text.ToString + "' and modePayement = 'Crédit' and reste > 0", conn2)
        Dim tabletick As New DataTable
        adpt2.Fill(tabletick)

        For i = 0 To tabletick.Rows.Count() - 1
            If tabletick.Rows(i).Item(13) <= crdt Then
                Dim rst As Double = tabletick.Rows(i).Item(2).ToString.Replace(".", ",")
                Dim sql4 As String
                Dim cmd4 As MySqlCommand
                sql4 = "UPDATE `orders` SET `reste`= @reste, `paye`= @paye, `OrderDate` = @date WHERE OrderID = @id "
                cmd4 = New MySqlCommand(sql4, conn2)
                cmd4.Parameters.Clear()
                cmd4.Parameters.AddWithValue("@id", tabletick.Rows(i).Item(0))
                cmd4.Parameters.AddWithValue("@reste", 0)
                cmd4.Parameters.AddWithValue("@paye", rst.ToString)
                cmd4.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                cmd4.ExecuteNonQuery()

                Dim sql5 As String
                Dim cmd5 As MySqlCommand
                sql5 = "UPDATE `clients` SET `wallet`= @credit WHERE id = @id "
                cmd5 = New MySqlCommand(sql3, conn2)
                cmd5.Parameters.Clear()
                cmd5.Parameters.AddWithValue("@id", Label1.Text)
                cmd5.Parameters.AddWithValue("@credit", Math.Round(crdt - Convert.ToDouble(rst), 2))
                cmd5.ExecuteNonQuery()
            End If

        Next

        conn2.Close()
        MsgBox("Opération bien faite")
        TextBox1.Clear()
        Me.Close()
        adpt = New MySqlDataAdapter("select * from clients order by client asc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        If table.Rows.Count <> 0 Then
            Client.DataGridView1.Rows.Clear()
            For i = 0 To table.Rows.Count - 1

                adpt2 = New MySqlDataAdapter("SELECT reste FROM `orders` WHERE `client` = '" + table.Rows(i).Item(1).ToString() + "'", conn2)
                Dim tablecrdt As New DataTable
                adpt2.Fill(tablecrdt)
                Dim reste As Double = 0
                For j = 0 To tablecrdt.Rows.Count() - 1
                    reste += Math.Round(Convert.ToDouble(tablecrdt.Rows(j).Item(0)), 2)
                Next
                Client.DataGridView1.Rows.Add("Clt-" & table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), table.Rows(i).Item(4), table.Rows(i).Item(5), table.Rows(i).Item(6), table.Rows(i).Item(7), table.Rows(i).Item(8), table.Rows(i).Item(9), table.Rows(i).Item(10), table.Rows(i).Item(11), table.Rows(i).Item(12), reste.ToString.Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(14)).ToString("# ##0.00").Replace(".", ","), Convert.ToDouble(table.Rows(i).Item(17)).ToString("# ##0.00").Replace(".", ","), table.Rows(i).Item(15), table.Rows(i).Item(16), Nothing, table.Rows(i).Item(0))

            Next
        End If

    End Sub

    Dim ReportToPrint As LocalReport
    Dim ReportToPrint2 As LocalReport
    Dim m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Public Property ConfigurationManager As Object

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

    Private Sub pay_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Me.Close()
    End Sub
End Class