Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient

Imports System.Runtime.InteropServices
Public Class facturedetails
    Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter

    Dim tc As Double

    Dim ADPS As MySqlDataAdapter
    Dim ReportToPrint As LocalReport
    Dim m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)
        Return stream
    End Function
    Dim tbl As New DataTable
    Dim tbl2 As New DataTable
    Dim stck As Double
    Dim prod As String

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
    Private Sub facturedetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Dim ind As String

    Private Sub DataGridView3_CellMouseClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView3.CellMouseClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 6 Then
                Dim Rep As Integer
                Dim row As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
                Rep = MsgBox("Voulez-vous vraiment supprimer ce produit ?", vbYesNo)
                If Rep = vbYes Then


                    ind = row.Cells(0).Value.ToString


                    conn2.Open()
                    Dim sql2 As String
                    Dim cmd2 As MySqlCommand
                    sql2 = "delete from tickets where id = '" + ind + "' "
                    cmd2 = New MySqlCommand(sql2, conn2)

                    cmd2.ExecuteNonQuery()
                    conn2.Close()
                    DataGridView3.Rows.Clear()
                    If Label1.Text = "Ticket N° :" Then
                        conn2.Open()
                        Dim sql3 As String
                        Dim cmd3 As MySqlCommand
                        sql3 = "UPDATE `num` SET montant = montant - @mnt, `pay`= (pay - @pay)  WHERE id = @id "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@id", Label2.Text.ToString)
                        cmd3.Parameters.AddWithValue("@mnt", Convert.ToDouble(row.Cells(5).Value))
                        cmd3.Parameters.AddWithValue("@pay", Convert.ToDouble(row.Cells(5).Value))
                        cmd3.ExecuteNonQuery()

                        Dim sql4 As String
                        Dim cmd4 As MySqlCommand
                        sql4 = "UPDATE `products` SET `stock`= (stock + @stock) WHERE id = @id "
                        cmd4 = New MySqlCommand(sql4, conn2)
                        cmd4.Parameters.Clear()
                        cmd4.Parameters.AddWithValue("@id", row.Cells(1).Value.ToString)
                        cmd4.Parameters.AddWithValue("@stock", row.Cells(3).Value)
                        cmd4.ExecuteNonQuery()

                        conn2.Close()
                        adpt = New MySqlDataAdapter("select code,name,qty,montant,rms,id from tickets where tick = '" + Label2.Text + "' and type = 0", conn2)
                    End If
                    If Label1.Text = "Facture N° :" Then
                        conn2.Open()
                        Dim sql3 As String
                        Dim cmd3 As MySqlCommand
                        sql3 = "UPDATE `factures` SET `montant ttc`= (`montant ttc` - @ttc), `payer`= (payer - @payer), `montant ht`= (`montant ht` - @ht)  WHERE id = @id "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@id", Label2.Text.ToString)
                        cmd3.Parameters.AddWithValue("@ttc", Convert.ToDouble(row.Cells(5).Value * 1.2))
                        cmd3.Parameters.AddWithValue("@payer", Convert.ToDouble(row.Cells(5).Value * 1.2))
                        cmd3.Parameters.AddWithValue("@ht", Convert.ToDouble(row.Cells(5).Value))
                        cmd3.ExecuteNonQuery()

                        Dim sql4 As String
                        Dim cmd4 As MySqlCommand
                        sql4 = "UPDATE `products` SET `stock`= (stock + @stock) WHERE id = @id "
                        cmd4 = New MySqlCommand(sql4, conn2)
                        cmd4.Parameters.Clear()
                        cmd4.Parameters.AddWithValue("@id", row.Cells(1).Value.ToString)
                        cmd4.Parameters.AddWithValue("@stock", row.Cells(3).Value)
                        cmd4.ExecuteNonQuery()

                        conn2.Close()
                        adpt = New MySqlDataAdapter("select code,name,qty,montant,rms,id from tickets where tick = '" + Label2.Text + "' and type = 'facture' ", conn2)
                    End If
                    If Label1.Text = "Devis N° :" Then
                        conn2.Open()
                        Dim sql3 As String
                        Dim cmd3 As MySqlCommand
                        sql3 = "UPDATE `devis` SET `montant_ttc`= (montant_ttc - @ttc), `montant_ht`= (montant_ht - @ht)  WHERE id = @id "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@id", Label2.Text.ToString)
                        cmd3.Parameters.AddWithValue("@ttc", Convert.ToDouble(row.Cells(5).Value * 1.2))
                        cmd3.Parameters.AddWithValue("@ht", Convert.ToDouble(row.Cells(5).Value))
                        cmd3.ExecuteNonQuery()
                        conn2.Close()
                        adpt = New MySqlDataAdapter("select code,name,qty,montant,rms,id from tickets where tick = '" + Label2.Text + "' and type = 'devis' ", conn2)
                    End If
                    Dim table As New DataTable
                        adpt.Fill(table)
                        For i = 0 To table.Rows.Count - 1
                        DataGridView3.Rows.Add(table.Rows(i).Item(5), table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(4) & "%", Val(table.Rows(i).Item(3)))

                    Next
                        MsgBox("Produit supprimé !")
                    End If
                End If
        End If

    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label21_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        adpt = New MySqlDataAdapter("select stock_fac, ice_fac from parameters", conn2)
        Dim params As New DataTable
        adpt.Fill(params)

        adpt = New MySqlDataAdapter("select client from orders where OrderID = '" + Label4.Text.ToString + "' ", conn2)
        Dim table3 As New DataTable
        adpt.Fill(table3)

        adpt2 = New MySqlDataAdapter("select client,adresse,ville,ICE,remisepour from clients where client = '" + table3.Rows(0).Item(0).ToString + "' ", conn2)
        Dim table2 As New DataTable
        adpt2.Fill(table2)
        If table2.Rows(0).Item(3) <> "" AndAlso table2.Rows(0).Item(3) <> "-" Then

            depense.Show()
            depense.Label11.Text = Label2.Text
            depense.DataGridView2.Rows.Add(0 & " %", 0, 0, 0)
            depense.DataGridView2.Rows.Add(7 & " %", 0, 0, 0)
            depense.DataGridView2.Rows.Add(10 & " %", 0, 0, 0)
            depense.DataGridView2.Rows.Add(14 & " %", 0, 0, 0)
            depense.DataGridView2.Rows.Add(20 & " %", 0, 0, 0)

            Dim sumttc As Double
            Dim sumht As Double
            Dim sumrms As Double

            Dim rowindex As String
            Dim found As Boolean = False

            Dim sum0 As Double
            Dim sum7 As Double
            Dim sum10 As Double
            Dim sum14 As Double
            Dim sum20 As Double
            Dim stoped As Integer = 0

            For i = 0 To DataGridView3.Rows.Count - 1

                For Each row As DataGridViewRow In depense.DataGridView1.Rows


                    If DataGridView3.Rows(i).Cells(0).Value = row.Cells.Item(0).Value And DataGridView3.Rows(i).Cells(1).Value = row.Cells.Item(1).Value Then
                        rowindex = row.Index.ToString()
                        found = True


                        If params.Rows(0).Item(0) = "Desactive" Then
                            row.Cells.Item(3).Value = Convert.ToDecimal(row.Cells.Item(3).Value.ToString.Replace(" ", "").Replace(".", ",")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ","))
                            row.Cells.Item(4).Value = DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "")
                            row.Cells.Item(5).Value = DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "")
                            row.Cells.Item(8).Value = Convert.ToDecimal(row.Cells.Item(8).Value.ToString.Replace(" ", "")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", ""))
                            row.Cells.Item(7).Value = Convert.ToDecimal(row.Cells.Item(8).Value.ToString.Replace(" ", "")) / (1 + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")))
                            row.Cells.Item(10).Value = Convert.ToDecimal(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "")) + Convert.ToDecimal(row.Cells.Item(10).Value.ToString.Replace(" ", ""))
                        Else

                            adpt = New MySqlDataAdapter("select facture, Article from article where Code = '" & DataGridView3.Rows(i).Cells(0).Value & "'", conn2)
                            Dim tablefact As New DataTable
                            adpt.Fill(tablefact)
                            If tablefact.Rows.Count() <> 0 Then
                                If Convert.ToDouble(tablefact.Rows(0).Item(0).ToString().Replace(".", ",")) >= (Convert.ToDecimal(row.Cells.Item(3).Value.ToString.Replace(" ", "").Replace(".", ",")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ","))) Then
                                    row.Cells.Item(3).Value = Convert.ToDecimal(row.Cells.Item(3).Value.ToString.Replace(" ", "").Replace(".", ",")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ","))
                                    row.Cells.Item(4).Value = DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "")
                                    row.Cells.Item(5).Value = DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "")
                                    row.Cells.Item(8).Value = Convert.ToDecimal(row.Cells.Item(8).Value.ToString.Replace(" ", "")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", ""))
                                    row.Cells.Item(7).Value = Convert.ToDecimal(row.Cells.Item(8).Value.ToString.Replace(" ", "")) / (1 + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")))
                                    row.Cells.Item(10).Value = Convert.ToDecimal(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "")) + Convert.ToDecimal(row.Cells.Item(10).Value.ToString.Replace(" ", ""))
                                    stoped = 0
                                Else
                                    MsgBox("Impossible de facturer l'article << " & tablefact.Rows(0).Item(1) & " >>, car il dépasse le Stock facturé !")
                                    depense.Close()
                                    stoped = 1
                                End If
                            Else
                                row.Cells.Item(3).Value = Convert.ToDecimal(row.Cells.Item(3).Value.ToString.Replace(" ", "").Replace(".", ",")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ","))
                                row.Cells.Item(4).Value = DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "")
                                row.Cells.Item(5).Value = DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "")
                                row.Cells.Item(8).Value = Convert.ToDecimal(row.Cells.Item(8).Value.ToString.Replace(" ", "")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", ""))
                                row.Cells.Item(7).Value = Convert.ToDecimal(row.Cells.Item(8).Value.ToString.Replace(" ", "")) / (1 + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")))
                                row.Cells.Item(10).Value = Convert.ToDecimal(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "")) + Convert.ToDecimal(row.Cells.Item(10).Value.ToString.Replace(" ", ""))
                                stoped = 0
                            End If

                        End If
                        Exit For
                    Else
                        found = False
                    End If

                Next
                Dim puht As Double
                Dim puttc As Double
                Dim totht As Double
                Dim totttc As Double

                If Not found Then
                    If params.Rows(0).Item(0) = "Desactive" Then
                        puht = DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "")
                        puttc = DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "")
                        totttc = Convert.ToDecimal(DataGridView3.Rows(i).Cells(3).Value)
                        totht = totttc / (1 + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value))
                        depense.DataGridView1.Rows.Add(DataGridView3.Rows(i).Cells(0).Value, DataGridView3.Rows(i).Cells(1).Value.ToString, "U", DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", ""), Convert.ToDouble(puht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(puttc.ToString.Replace(" ", "")).ToString("# ##0.00"), 0, Convert.ToDouble(totht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(totttc.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDecimal(Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")) * 100).ToString("N0"), DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", ""), 0)
                    Else

                        adpt = New MySqlDataAdapter("select facture, Article from article where Code = '" & DataGridView3.Rows(i).Cells(0).Value & "'", conn2)
                        Dim tablefact As New DataTable
                        adpt.Fill(tablefact)
                        If tablefact.Rows.Count() <> 0 Then
                            If Convert.ToDouble(tablefact.Rows(0).Item(0).ToString().Replace(".", ",")) >= Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ",")) Then

                                puht = DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "")
                                puttc = DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "")
                                totttc = Convert.ToDecimal(DataGridView3.Rows(i).Cells(3).Value)
                                totht = totttc / (1 + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value))
                                depense.DataGridView1.Rows.Add(DataGridView3.Rows(i).Cells(0).Value, DataGridView3.Rows(i).Cells(1).Value.ToString, "U", DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", ""), Convert.ToDouble(puht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(puttc.ToString.Replace(" ", "")).ToString("# ##0.00"), 0, Convert.ToDouble(totht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(totttc.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDecimal(Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")) * 100).ToString("N0"), DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", ""), 0)
                                stoped = 0
                            Else
                                MsgBox("Impossible de facturer l'article << " & tablefact.Rows(0).Item(1) & " >>, car il dépasse le Stock facturé !")
                                depense.Close()
                                stoped = 1
                            End If
                        Else
                            stoped = 0
                            puht = DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "")
                            puttc = DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "")
                            totttc = Convert.ToDecimal(DataGridView3.Rows(i).Cells(3).Value)
                            totht = totttc / (1 + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value))
                            depense.DataGridView1.Rows.Add(DataGridView3.Rows(i).Cells(0).Value, DataGridView3.Rows(i).Cells(1).Value.ToString, "U", DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", ""), Convert.ToDouble(puht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(puttc.ToString.Replace(" ", "")).ToString("# ##0.00"), 0, Convert.ToDouble(totht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(totttc.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDecimal(Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")) * 100).ToString("N0"), DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", ""), 0)

                        End If

                    End If
                End If
            Next
            If stoped = 0 Then

                Dim ht, ht7, ht10, ht14, ht20 As Double
                Dim tva, tva7, tva10, tva14, tva20 As Double
                If depense.DataGridView1.Rows.Count() <> 0 Then

                    For j = 0 To depense.DataGridView1.Rows.Count() - 1

                        sumttc += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                        sumht += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                        sumrms += (depense.DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * (depense.DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - depense.DataGridView1.Rows(j).Cells(11).Value.ToString.Replace(" ", "").Replace(".", ","))) * (depense.DataGridView1.Rows(j).Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") / 100)

                        If depense.DataGridView1.Rows(j).Cells(9).Value = 0 Then
                            sum0 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva = 0

                        End If

                        If depense.DataGridView1.Rows(j).Cells(9).Value = 7 Then
                            sum7 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht7 += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva7 = sum7 - ht7
                        End If

                        If depense.DataGridView1.Rows(j).Cells(9).Value = 10 Then
                            sum10 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht10 += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva10 = sum10 - ht10
                        End If

                        If depense.DataGridView1.Rows(j).Cells(9).Value = 14 Then
                            sum14 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht14 += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva14 = sum14 - ht14
                        End If
                        If depense.DataGridView1.Rows(j).Cells(9).Value = 20 Then
                            sum20 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht20 += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva20 = sum20 - ht20
                        End If

                    Next

                End If
                If depense.DataGridView2.Rows.Count <> 0 Then


                    depense.DataGridView2.Rows(0).Cells(1).Value = Convert.ToDouble(ht.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(0).Cells(2).Value = Convert.ToDouble(tva.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(0).Cells(3).Value = Convert.ToDouble(sum0.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(1).Cells(1).Value = Convert.ToDouble(ht7.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(1).Cells(2).Value = Convert.ToDouble(tva7.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(1).Cells(3).Value = Convert.ToDouble(sum7.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(2).Cells(1).Value = Convert.ToDouble(ht10.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(2).Cells(2).Value = Convert.ToDouble(tva10.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(2).Cells(3).Value = Convert.ToDouble(sum10.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(3).Cells(1).Value = Convert.ToDouble(ht14.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(3).Cells(2).Value = Convert.ToDouble(tva14.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(3).Cells(3).Value = Convert.ToDouble(sum14.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(4).Cells(1).Value = Convert.ToDouble(ht20.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(4).Cells(2).Value = Convert.ToDouble(tva20.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(4).Cells(3).Value = Convert.ToDouble(sum20.ToString.Replace(".", ",")).ToString("N2")
                End If
                depense.Label15.Text = Convert.ToDouble(sumttc.ToString.Replace(".", ",") + Convert.ToDouble(Label7.Text.Replace(".", ",").Replace(" ", ""))).ToString("N2")
                depense.Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("N2")
                depense.Label27.Text = Convert.ToDouble(sumrms.ToString.Replace(".", ",")).ToString("N2")
                depense.Label10.Text = Convert.ToDouble(sumttc - (sumht - sumrms)).ToString("N2")



                depense.Label7.Text = table2.Rows(0).Item(0)
                If Label8.Text.Replace(" %", "") = "0" Then
                    depense.TextBox9.Text = table2.Rows(0).Item(4)
                Else
                    depense.TextBox9.Text = Label8.Text.Replace(" %", "")

                End If
                depense.Label34.Text = Label7.Text
                depense.IconButton9.PerformClick()
                If IsDBNull(table2.Rows(0).Item(1)) Then
                    depense.Label8.Text = "-"
                Else

                    depense.Label8.Text = table2.Rows(0).Item(1)

                End If
                If IsDBNull(table2.Rows(0).Item(2)) Then
                    depense.Label12.Text = "-"

                Else
                    depense.Label12.Text = table2.Rows(0).Item(2)

                End If

                Dim output As StringBuilder = New StringBuilder
                For i = 0 To table2.Rows(0).Item(3).Length - 1
                    If IsNumeric(table2.Rows(0).Item(3)(i)) Then
                        output.Append(table2.Rows(0).Item(3)(i))
                    End If
                Next
                depense.Label2.Text = output.ToString()


                adpt = New MySqlDataAdapter("select OrderID from facture order by OrderID desc", conn2)
                Dim table4 As New DataTable
                adpt.Fill(table4)
                Dim num As Integer
                If table4.Rows.Count <> 0 Then
                    If Convert.ToDouble(Year(Now) & "0001") > (Val(table4.Rows(0).Item(0)) + 1) Then
                        num = Year(Now) & "0001"
                    Else

                        num = Val(table4.Rows(0).Item(0)) + 1
                    End If
                Else
                    num = Year(Now) & "0001"
                End If

                depense.Label33.Text = Label6.Text
                depense.IconButton9.PerformClick()

                depense.TextBox1.Text = "Facture N° " & num
                depense.Label6.Text = DateTimePicker1.Text
                If Label6.Text <= 0 Then
                    depense.Label32.Text = "Facture payée"
                    depense.Label31.Text = Label5.Text
                    depense.Label32.ForeColor = Color.Green
                End If

            End If
        Else
            If params.Rows(0).Item(1) = "Desactive" Then
                depense.Show()
                depense.Label11.Text = Label2.Text
                depense.DataGridView2.Rows.Add(0 & " %", 0, 0, 0)
                depense.DataGridView2.Rows.Add(7 & " %", 0, 0, 0)
                depense.DataGridView2.Rows.Add(10 & " %", 0, 0, 0)
                depense.DataGridView2.Rows.Add(14 & " %", 0, 0, 0)
                depense.DataGridView2.Rows.Add(20 & " %", 0, 0, 0)

                Dim sumttc As Double
                Dim sumht As Double
                Dim sumrms As Double

                Dim rowindex As String
                Dim found As Boolean = False

                Dim sum0 As Double
                Dim sum7 As Double
                Dim sum10 As Double
                Dim sum14 As Double
                Dim sum20 As Double
                Dim stoped As Integer = 0

                For i = 0 To DataGridView3.Rows.Count - 1

                    For Each row As DataGridViewRow In depense.DataGridView1.Rows


                        If DataGridView3.Rows(i).Cells(0).Value = row.Cells.Item(0).Value And DataGridView3.Rows(i).Cells(1).Value = row.Cells.Item(1).Value Then
                            rowindex = row.Index.ToString()
                            found = True


                            If params.Rows(0).Item(0) = "Desactive" Then
                                row.Cells.Item(3).Value = Convert.ToDecimal(row.Cells.Item(3).Value.ToString.Replace(" ", "").Replace(".", ",")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ","))
                                row.Cells.Item(4).Value = DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "")
                                row.Cells.Item(5).Value = DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "")
                                row.Cells.Item(8).Value = Convert.ToDecimal(row.Cells.Item(8).Value.ToString.Replace(" ", "")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", ""))
                                row.Cells.Item(7).Value = Convert.ToDecimal(row.Cells.Item(8).Value.ToString.Replace(" ", "")) / (1 + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")))
                                row.Cells.Item(10).Value = Convert.ToDecimal(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "")) + Convert.ToDecimal(row.Cells.Item(10).Value.ToString.Replace(" ", ""))
                            Else

                                adpt = New MySqlDataAdapter("select facture, Article from article where Code = '" & DataGridView3.Rows(i).Cells(0).Value & "'", conn2)
                                Dim tablefact As New DataTable
                                adpt.Fill(tablefact)
                                If tablefact.Rows.Count() <> 0 Then
                                    If Convert.ToDouble(tablefact.Rows(0).Item(0).ToString().Replace(".", ",")) >= (Convert.ToDecimal(row.Cells.Item(3).Value.ToString.Replace(" ", "").Replace(".", ",")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ","))) Then
                                        row.Cells.Item(3).Value = Convert.ToDecimal(row.Cells.Item(3).Value.ToString.Replace(" ", "").Replace(".", ",")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ","))
                                        row.Cells.Item(4).Value = DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "")
                                        row.Cells.Item(5).Value = DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "")
                                        row.Cells.Item(8).Value = Convert.ToDecimal(row.Cells.Item(8).Value.ToString.Replace(" ", "")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", ""))
                                        row.Cells.Item(7).Value = Convert.ToDecimal(row.Cells.Item(8).Value.ToString.Replace(" ", "")) / (1 + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")))
                                        row.Cells.Item(10).Value = Convert.ToDecimal(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "")) + Convert.ToDecimal(row.Cells.Item(10).Value.ToString.Replace(" ", ""))
                                        stoped = 0
                                    Else
                                        MsgBox("Impossible de facturer l'article << " & tablefact.Rows(0).Item(1) & " >>, car il dépasse le Stock facturé !")
                                        depense.Close()
                                        stoped = 1
                                    End If
                                Else
                                    row.Cells.Item(3).Value = Convert.ToDecimal(row.Cells.Item(3).Value.ToString.Replace(" ", "").Replace(".", ",")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ","))
                                    row.Cells.Item(4).Value = DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "")
                                    row.Cells.Item(5).Value = DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "")
                                    row.Cells.Item(8).Value = Convert.ToDecimal(row.Cells.Item(8).Value.ToString.Replace(" ", "")) + Convert.ToDecimal(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", ""))
                                    row.Cells.Item(7).Value = Convert.ToDecimal(row.Cells.Item(8).Value.ToString.Replace(" ", "")) / (1 + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")))
                                    row.Cells.Item(10).Value = Convert.ToDecimal(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "")) + Convert.ToDecimal(row.Cells.Item(10).Value.ToString.Replace(" ", ""))
                                    stoped = 0
                                End If

                            End If
                            Exit For
                        Else
                            found = False
                        End If

                    Next
                    Dim puht As Double
                    Dim puttc As Double
                    Dim totht As Double
                    Dim totttc As Double

                    If Not found Then
                        If params.Rows(0).Item(0) = "Desactive" Then
                            puht = DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "")
                            puttc = DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "")
                            totttc = Convert.ToDecimal(DataGridView3.Rows(i).Cells(3).Value)
                            totht = totttc / (1 + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value))
                            depense.DataGridView1.Rows.Add(DataGridView3.Rows(i).Cells(0).Value, DataGridView3.Rows(i).Cells(1).Value.ToString, "U", DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", ""), Convert.ToDouble(puht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(puttc.ToString.Replace(" ", "")).ToString("# ##0.00"), 0, Convert.ToDouble(totht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(totttc.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDecimal(Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")) * 100).ToString("N0"), DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", ""), 0)
                        Else

                            adpt = New MySqlDataAdapter("select facture, Article from article where Code = '" & DataGridView3.Rows(i).Cells(0).Value & "'", conn2)
                            Dim tablefact As New DataTable
                            adpt.Fill(tablefact)
                            If tablefact.Rows.Count() <> 0 Then
                                If Convert.ToDouble(tablefact.Rows(0).Item(0).ToString().Replace(".", ",")) >= Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ",")) Then

                                    puht = DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "")
                                    puttc = DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "")
                                    totttc = Convert.ToDecimal(DataGridView3.Rows(i).Cells(3).Value)
                                    totht = totttc / (1 + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value))
                                    depense.DataGridView1.Rows.Add(DataGridView3.Rows(i).Cells(0).Value, DataGridView3.Rows(i).Cells(1).Value.ToString, "U", DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", ""), Convert.ToDouble(puht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(puttc.ToString.Replace(" ", "")).ToString("# ##0.00"), 0, Convert.ToDouble(totht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(totttc.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDecimal(Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")) * 100).ToString("N0"), DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", ""), 0)
                                    stoped = 0
                                Else
                                    MsgBox("Impossible de facturer l'article << " & tablefact.Rows(0).Item(1) & " >>, car il dépasse le Stock facturé !")
                                    depense.Close()
                                    stoped = 1
                                End If
                            Else
                                stoped = 0
                                puht = DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "")
                                puttc = DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "")
                                totttc = Convert.ToDecimal(DataGridView3.Rows(i).Cells(3).Value)
                                totht = totttc / (1 + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value))
                                depense.DataGridView1.Rows.Add(DataGridView3.Rows(i).Cells(0).Value, DataGridView3.Rows(i).Cells(1).Value.ToString, "U", DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", ""), Convert.ToDouble(puht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(puttc.ToString.Replace(" ", "")).ToString("# ##0.00"), 0, Convert.ToDouble(totht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(totttc.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDecimal(Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")) * 100).ToString("N0"), DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", ""), 0)

                            End If

                        End If
                    End If
                Next
                If stoped = 0 Then

                    Dim ht, ht7, ht10, ht14, ht20 As Double
                    Dim tva, tva7, tva10, tva14, tva20 As Double

                    For j = 0 To depense.DataGridView1.Rows.Count() - 1

                        sumttc += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                        sumht += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                        sumrms += (depense.DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * (depense.DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - depense.DataGridView1.Rows(j).Cells(11).Value.ToString.Replace(" ", "").Replace(".", ","))) * (depense.DataGridView1.Rows(j).Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") / 100)

                        If depense.DataGridView1.Rows(j).Cells(9).Value = 0 Then
                            sum0 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva = 0

                        End If

                        If depense.DataGridView1.Rows(j).Cells(9).Value = 7 Then
                            sum7 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht7 += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva7 = sum7 - ht7
                        End If

                        If depense.DataGridView1.Rows(j).Cells(9).Value = 10 Then
                            sum10 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht10 += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva10 = sum10 - ht10
                        End If

                        If depense.DataGridView1.Rows(j).Cells(9).Value = 14 Then
                            sum14 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht14 += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva14 = sum14 - ht14
                        End If
                        If depense.DataGridView1.Rows(j).Cells(9).Value = 20 Then
                            sum20 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                            ht20 += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                            tva20 = sum20 - ht20
                        End If

                    Next

                    depense.DataGridView2.Rows(0).Cells(1).Value = Convert.ToDouble(ht.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(0).Cells(2).Value = Convert.ToDouble(tva.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(0).Cells(3).Value = Convert.ToDouble(sum0.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(1).Cells(1).Value = Convert.ToDouble(ht7.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(1).Cells(2).Value = Convert.ToDouble(tva7.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(1).Cells(3).Value = Convert.ToDouble(sum7.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(2).Cells(1).Value = Convert.ToDouble(ht10.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(2).Cells(2).Value = Convert.ToDouble(tva10.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(2).Cells(3).Value = Convert.ToDouble(sum10.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(3).Cells(1).Value = Convert.ToDouble(ht14.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(3).Cells(2).Value = Convert.ToDouble(tva14.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(3).Cells(3).Value = Convert.ToDouble(sum14.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(4).Cells(1).Value = Convert.ToDouble(ht20.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(4).Cells(2).Value = Convert.ToDouble(tva20.ToString.Replace(".", ",")).ToString("N2")
                    depense.DataGridView2.Rows(4).Cells(3).Value = Convert.ToDouble(sum20.ToString.Replace(".", ",")).ToString("N2")

                    depense.Label15.Text = Convert.ToDouble(sumttc.ToString.Replace(".", ",") + Convert.ToDouble(Label7.Text.Replace(".", ",").Replace(" ", ""))).ToString("N2")
                    depense.Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("N2")
                    depense.Label27.Text = Convert.ToDouble(sumrms.ToString.Replace(".", ",")).ToString("N2")
                    depense.Label10.Text = Convert.ToDouble(sumttc - (sumht - sumrms)).ToString("N2")



                    depense.Label7.Text = table2.Rows(0).Item(0)
                    If Label8.Text.Replace(" %", "") = "0" Then
                        depense.TextBox9.Text = table2.Rows(0).Item(4)
                    Else
                        depense.TextBox9.Text = Label8.Text.Replace(" %", "")

                    End If
                    depense.Label34.Text = Label7.Text
                    depense.IconButton9.PerformClick()
                    If IsDBNull(table2.Rows(0).Item(1)) Then
                        depense.Label8.Text = "-"
                    Else

                        depense.Label8.Text = table2.Rows(0).Item(1)

                    End If
                    If IsDBNull(table2.Rows(0).Item(2)) Then
                        depense.Label12.Text = "-"

                    Else
                        depense.Label12.Text = table2.Rows(0).Item(2)

                    End If

                    Dim output As StringBuilder = New StringBuilder
                    For i = 0 To table2.Rows(0).Item(3).Length - 1
                        If IsNumeric(table2.Rows(0).Item(3)(i)) Then
                            output.Append(table2.Rows(0).Item(3)(i))
                        End If
                    Next
                    depense.Label2.Text = output.ToString()


                    adpt = New MySqlDataAdapter("select OrderID from facture order by OrderID desc", conn2)
                    Dim table4 As New DataTable
                    adpt.Fill(table4)
                    Dim num As Integer
                    If table4.Rows.Count <> 0 Then
                        If Convert.ToDouble(Year(Now) & "0001") > (Val(table4.Rows(0).Item(0)) + 1) Then
                            num = Year(Now) & "0001"
                        Else

                            num = Val(table4.Rows(0).Item(0)) + 1
                        End If
                    Else
                        num = Year(Now) & "0001"
                    End If

                    depense.Label33.Text = Label6.Text
                    depense.IconButton9.PerformClick()

                    depense.TextBox1.Text = "Facture N° " & num
                    depense.Label6.Text = DateTimePicker1.Text
                    If Label6.Text <= 0 Then
                        depense.Label32.Text = "Facture payée"
                        depense.Label31.Text = Label5.Text
                        depense.Label32.ForeColor = Color.Green
                    End If

                End If
            Else
                MsgBox("Ce Client n'a pas d'ICE veuillez le saisir avant de passer la facture !")

            End If

        End If



    End Sub



    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Close()

    End Sub

End Class
