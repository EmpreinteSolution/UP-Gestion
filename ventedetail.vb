Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient

Imports System.Runtime.InteropServices
Public Class ventedetail
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

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Modification des ventes'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then


                If Label7.Text = archive.Label2.Text Or role = "Administration" Or role = "Gerant" Then

                    If dashboard.Label28.Text = "numbl" Then


                        conn2.Close()
                        conn2.Open()
                        If role = "Caissier" Then
                            dashboard.BringToFront()
                        Else
                            dashboard.Show()
                            dashboard.BringToFront()
                            dashboard.IconButton8.Visible = False
                            dashboard.IconButton9.Visible = True
                        End If


                        dashboard.IconButton66.PerformClick()
                        Dim id = Label2.Text

                        dashboard.Label28.Text = id.ToString
                        Dim adt As New MySqlDataAdapter("SELECT  `name`,  `Quantity`, `Price`, `ProductID`, `pa` , `Total`,`rms`,`gr`,`pv`, `tva`  FROM `orderdetails` WHERE `OrderID` = " + id.ToString, conn2)
                        Dim tb As New DataTable
                        adt.Fill(tb)
                        dashboard.DataGridView3.Rows.Clear()
                        For i = 0 To tb.Rows.Count - 1

                            dashboard.DataGridView3.Rows.Add(tb.Rows(i).Item(3), tb.Rows(i).Item(0), tb.Rows(i).Item(1).ToString.Replace(".", ",").Replace(" ", ""), tb.Rows(i).Item(2).ToString.Replace(".", ",").Replace(" ", ""), tb.Rows(i).Item(3), tb.Rows(i).Item(4).ToString.Replace(".", ",").Replace(" ", ""), tb.Rows(i).Item(5).ToString.Replace(".", ",").Replace(" ", ""), Nothing, tb.Rows(i).Item(6).ToString.Replace(".", ",").Replace(" ", ""), tb.Rows(i).Item(7).ToString.Replace(".", ",").Replace(" ", ""), tb.Rows(i).Item(8).ToString.Replace(".", ",").Replace(" ", ""), tb.Rows(i).Item(9).ToString.Replace(".", ",").Replace(" ", ""))
                        Next

                        adt = New MySqlDataAdapter("SELECT `modePayement` ,`client`,`paye`,`reste`,`OrderDate`,`transport`,`livreur`,`fac_id` FROM `orders` WHERE `OrderID` = " + id.ToString, conn2)
                        tb = New DataTable
                        adt.Fill(tb)
                        dashboard.ComboBox1.SelectedItem = tb.Rows(0).Item(0)
                        dashboard.ComboBox2.SelectedItem = tb.Rows(0).Item(1)
                        dashboard.Label29.Text = tb.Rows(0).Item(4)
                        dashboard.Label30.Text = tb.Rows(0).Item(5)
                        dashboard.Label55.Text = tb.Rows(0).Item(7)

                        If tb.Rows(0).Item(6) = "-" Then
                            dashboard.Label32.Text = "Sans livraison"
                        Else
                            dashboard.Label32.Text = tb.Rows(0).Item(6)
                        End If

                        dashboard.TextBox8.Text = tb.Rows(0).Item(1)
                        If dashboard.ComboBox2.Text = "comptoir" Then
                            dashboard.ComboBox2.Visible = False
                            dashboard.TextBox8.Text = ""
                        Else
                            dashboard.ComboBox2.Visible = True
                        End If
                        dashboard.TextBox3.Text = ""
                        dashboard.Label4.Text = 0.00
                        dashboard.Label19.Text = id
                        dashboard.Label20.Text = "Ticket N° " & id.ToString

                        Dim sum As Double = 0
                        Dim sum2 As Double = 0
                        Dim sum3 As Double = 0
                        Dim prixHT As Decimal = 0
                        Dim quantite As Decimal = 0
                        Dim tauxTVA As Decimal = 0
                        Dim pourcentageRemise As Decimal = 0
                        Dim montantHTApresRemise As Decimal = 0

                        ' Étape 2 : Calculer le montant TTC avant TVA
                        Dim montantTTCAvantTVA As Decimal = 0

                        ' Étape 3 : Calculer le montant de la TVA
                        Dim montantTVA As Decimal = 0

                        ' Étape 4 : Calculer le prix TTC
                        Dim prixTTC As Decimal = 0

                        For i = 0 To dashboard.DataGridView3.Rows.Count - 1
                            sum = sum + Convert.ToDouble(dashboard.DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", ""))

                            prixHT = dashboard.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + dashboard.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                            quantite = dashboard.DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - dashboard.DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                            tauxTVA = dashboard.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                            pourcentageRemise = dashboard.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                            ' Étape 1 : Calculer le montant HT après la remise
                            montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

                            ' Étape 2 : Calculer le montant TTC avant TVA
                            montantTTCAvantTVA = montantHTApresRemise * quantite

                            ' Étape 3 : Calculer le montant de la TVA
                            montantTVA = montantTTCAvantTVA * tauxTVA

                            sum2 += montantTTCAvantTVA
                            sum3 += montantTVA


                            dashboard.DataGridView3.Rows(i).Cells(7).Value = i
                        Next
                        dashboard.Label23.Text = sum2
                        dashboard.Label24.Text = sum3
                        dashboard.Label25.Text = dashboard.Label23.Text

                        dashboard.Label6.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2")
                        dashboard.Label7.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("N2")
                        dashboard.IconButton17.Text = dashboard.DataGridView3.Rows.Count

                        Dim cmd As New MySqlCommand
                        For i = 0 To DataGridView3.Rows.Count - 1
                            cmd = New MySqlCommand("UPDATE `article` SET `Stock` = `Stock` +  @stck  WHERE `Code` = @cd", conn2)
                            cmd.Parameters.AddWithValue("@stck", DataGridView3.Rows(i).Cells(2).Value)
                            cmd.Parameters.AddWithValue("@cd", DataGridView3.Rows(i).Cells(0).Value)

                            cmd.ExecuteNonQuery()

                        Next
                        'cmd = New MySqlCommand("DELETE FROM `orderdetails` WHERE `OrderID` = '" & Label2.Text.ToString & "' ", conn2)
                        'cmd.ExecuteNonQuery()
                        'cmd = New MySqlCommand("DELETE FROM `orders` WHERE `OrderID` = '" & Label2.Text.ToString & "' ", conn2)
                        'cmd.ExecuteNonQuery()

                        conn2.Close()

                        conn2.Open()
                        cmd = New MySqlCommand("INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op)", conn2)
                        cmd.Parameters.Clear()
                        cmd.Parameters.AddWithValue("@name", user)
                        cmd.Parameters.AddWithValue("@op", "Modification du ticket N° " & Label2.Text)
                        cmd.ExecuteNonQuery()
                        conn2.Close()
                        Me.Close()
                    Else
                        MsgBox("Veuillez Terminer où vider la commande En cours !")
                    End If
                Else
                    MsgBox("Seul le caissier qui a valider ce Bon peut le modifier !")
                End If

            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If
    End Sub
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

    <Obsolete>
    Private Sub ventedetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim printerticket As String = System.Configuration.ConfigurationSettings.AppSettings("printerticket")
        Dim printera5 As String = System.Configuration.ConfigurationSettings.AppSettings("printera5")
        Dim printera4 As String = System.Configuration.ConfigurationSettings.AppSettings("printera4")

        receiptprinter = printerticket
        a4printer = printera4
        a5printer = printera5
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


                    conn2.Close()
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

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("tick")
        Dim id As Integer
        id = Label2.Text

        adpt = New MySqlDataAdapter("select Price,name,Quantity,Total from orderdetails where OrderID = " + id.ToString + " and type = 'ventes' ", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sumqty As Double
        For i = 0 To table.Rows.Count - 1
            dt.Rows.Add("", table.Rows(i).Item(1), "", table.Rows(i).Item(2), "", table.Rows(i).Item(0), "", table.Rows(i).Item(3), "")
            sumqty += Convert.ToDouble(table.Rows(i).Item(2))
        Next

        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\Report1.rdlc"
        ReportToPrint.DataSources.Clear()
        ReportToPrint.EnableExternalImages = True

        adpt = New MySqlDataAdapter("select MontantOrder,client,OrderDate,modePayement,ServeurRef,paye from orders where OrderID = " + id.ToString + "", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)

        Dim lpar3 As New ReportParameter("totale", table2.Rows(0).Item(0).ToString)
        Dim lpar31(0) As ReportParameter
        lpar31(0) = lpar3
        ReportToPrint.SetParameters(lpar31)

        Dim Acre As New ReportParameter("CreditAncien", "-")
        Dim Acre1(0) As ReportParameter
        Acre1(0) = Acre
        ReportToPrint.SetParameters(Acre1)



        Dim cliente As New ReportParameter("client", table2.Rows(0).Item(1).ToString)
        Dim client1(0) As ReportParameter
        client1(0) = cliente
        ReportToPrint.SetParameters(client1)

        Dim ide As New ReportParameter("id", id)
        Dim id1(0) As ReportParameter
        id1(0) = ide
        ReportToPrint.SetParameters(id1)

        Dim daty As New ReportParameter("date", table2.Rows(0).Item(2).ToString)
        Dim daty1(0) As ReportParameter
        daty1(0) = daty
        ReportToPrint.SetParameters(daty1)

        Dim mode As New ReportParameter("mode", table2.Rows(0).Item(3).ToString)
        Dim mode1(0) As ReportParameter
        mode1(0) = mode
        ReportToPrint.SetParameters(mode1)

        Dim caissier As New ReportParameter("caissier", table2.Rows(0).Item(4).ToString)
        Dim caissier1(0) As ReportParameter
        caissier1(0) = caissier
        ReportToPrint.SetParameters(caissier1)

        Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
        Dim table23 As New DataTable
        adpt23.Fill(table23)

        Dim info As New ReportParameter("info", table23.Rows(0).Item(1).ToString + vbCrLf + table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString)
        Dim info1(0) As ReportParameter
        info1(0) = info
        ReportToPrint.SetParameters(info1)


        Dim msg As New ReportParameter("msg", table23.Rows(0).Item(6).ToString)
        Dim msg1(0) As ReportParameter
        msg1(0) = msg
        ReportToPrint.SetParameters(msg1)

        Dim count As New ReportParameter("count", table.Rows.Count.ToString)
        Dim count1(0) As ReportParameter
        count1(0) = count
        ReportToPrint.SetParameters(count1)

        Dim rnd As New ReportParameter("TypeRend", "-")
        Dim rnd1(0) As ReportParameter
        rnd1(0) = rnd
        ReportToPrint.SetParameters(rnd1)


        Dim paye As New ReportParameter("paye", table2.Rows(0).Item(5).ToString)
        Dim paye1(0) As ReportParameter
        paye1(0) = paye
        ReportToPrint.SetParameters(paye1)

        Dim fraisN As New ReportParameter("FraisN", "-")
        Dim fraisN1(0) As ReportParameter
        fraisN1(0) = fraisN
        ReportToPrint.SetParameters(fraisN1)

        Dim fraisM As New ReportParameter("FraisM", "-")
        Dim fraisM1(0) As ReportParameter
        fraisM1(0) = fraisM
        ReportToPrint.SetParameters(fraisM1)



        Dim Rendu As New ReportParameter("Rendu", "-")
        Dim Rendu1(0) As ReportParameter
        Rendu1(0) = Rendu
        ReportToPrint.SetParameters(Rendu1)

        Dim qty As New ReportParameter("qty", sumqty.ToString)
        Dim qty1(0) As ReportParameter
        qty1(0) = qty
        ReportToPrint.SetParameters(qty1)

        Dim img As New ReportParameter("img", "File:\" + "C:\pospictures\" + table23.Rows(0).Item(11).ToString, True)
        Dim img1(0) As ReportParameter
        img1(0) = img
        ReportToPrint.SetParameters(img1)

        ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

        'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
        'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>3.7in</PageWidth><PageHeight>12in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()

        ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)



        For Each stream As Stream In m_streams
            stream.Position = 0
        Next

        Dim printDoc As New PrintDocument()
        'Microsoft Print To PDF
        'C80250 Series
        'C80250 Series init
        Dim printname As String = System.Configuration.ConfigurationSettings.AppSettings("printar")
        printDoc.PrinterSettings.PrinterName = printname
        Dim ps As New PrinterSettings()
        ps.PrinterName = printDoc.PrinterSettings.PrinterName
        printDoc.PrinterSettings = ps

        AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
        m_currentPageIndex = 0
        printDoc.Print()
        tbl.Rows.Clear()
        tbl2.Rows.Clear()
        Dim cmd3 As MySqlCommand

        conn2.Close()
        conn2.Open()
        cmd3 = New MySqlCommand("INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op)", conn2)
        cmd3.Parameters.Clear()
        cmd3.Parameters.AddWithValue("@name", archive.Label2.Text)
        cmd3.Parameters.AddWithValue("@op", "Impression du copie du ticket N° " & Label2.Text)
        cmd3.ExecuteNonQuery()
        conn2.Close()


    End Sub



    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        depense.Show()
        depense.Label11.Text = Label2.Text
        depense.DataGridView2.Rows.Add(0 & " %", 0, 0, 0)
        depense.DataGridView2.Rows.Add(7 & " %", 0, 0, 0)
        depense.DataGridView2.Rows.Add(10 & " %", 0, 0, 0)
        depense.DataGridView2.Rows.Add(14 & " %", 0, 0, 0)
        depense.DataGridView2.Rows.Add(20 & " %", 0, 0, 0)

        Dim sumttc As Double = 0
        Dim sumht As Double = 0
        Dim sumrms As Double = 0
        For i = 0 To DataGridView3.Rows.Count - 1
            Dim codebar As String
            codebar = DataGridView3.Rows(i).Cells(0).Value.ToString
            adpt = New MySqlDataAdapter("select prod_code from code_supp where code_supp = '" + codebar + "' ", conn2)
            Dim table12 As New DataTable
            adpt.Fill(table12)
            If table12.Rows.Count() = 0 Then
                adpt = New MySqlDataAdapter("select Article,unit,TVA,PV_HT,PV_TTC from article where Code = '" + codebar + "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    Dim puht As Double
                    Dim puttc As Double
                    Dim totht As Double
                    Dim totttc As Double

                    puht = table.Rows(0).Item(3)
                    puttc = table.Rows(0).Item(4)
                    totht = (puht * (DataGridView3.Rows(i).Cells(2).Value.ToString().Replace(" ", "") - DataGridView3.Rows(i).Cells(6).Value.ToString().Replace(" ", ""))) - ((puht * (DataGridView3.Rows(i).Cells(2).Value.ToString().Replace(" ", "") - DataGridView3.Rows(i).Cells(6).Value.ToString().Replace(" ", ""))) * (DataGridView3.Rows(i).Cells(3).Value.ToString().Replace(" ", "") / 100))
                    totttc = totht + (totht * (table.Rows(0).Item(2) / 100))


                    depense.DataGridView1.Rows.Add(DataGridView3.Rows(i).Cells(0).Value, DataGridView3.Rows(i).Cells(1).Value, "U", DataGridView3.Rows(i).Cells(2).Value, Convert.ToDouble(puht).ToString("# ##0.00"), Convert.ToDouble(puttc).ToString("# ##0.00"), DataGridView3.Rows(i).Cells(3).Value, Convert.ToDouble(totht).ToString("# ##0.00"), Convert.ToDouble(totttc).ToString("# ##0.00"), table.Rows(0).Item(2), DataGridView3.Rows(i).Cells(5).Value, DataGridView3.Rows(i).Cells(6).Value)
                End If
            Else
                adpt = New MySqlDataAdapter("select Article,unit,TVA,PV_HT,PV_TTC from article where Code = '" + table12.Rows(0).Item(0) + "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    Dim puht As Double
                    Dim puttc As Double
                    Dim totht As Double
                    Dim totttc As Double

                    puht = table.Rows(0).Item(3)
                    puttc = table.Rows(0).Item(4)
                    totht = (puht * (DataGridView3.Rows(i).Cells(2).Value - DataGridView3.Rows(i).Cells(6).Value)) - ((puht * (DataGridView3.Rows(i).Cells(2).Value - DataGridView3.Rows(i).Cells(6).Value)) * (DataGridView3.Rows(i).Cells(3).Value / 100))
                    totttc = totht + (totht * (table.Rows(0).Item(2) / 100))


                    depense.DataGridView1.Rows.Add(DataGridView3.Rows(i).Cells(0).Value, DataGridView3.Rows(i).Cells(1).Value, "U", DataGridView3.Rows(i).Cells(2).Value, Convert.ToDouble(puht).ToString("# ##0.00"), Convert.ToDouble(puttc).ToString("# ##0.00"), DataGridView3.Rows(i).Cells(3).Value, Convert.ToDouble(totht).ToString("# ##0.00"), Convert.ToDouble(totttc).ToString("# ##0.00"), table.Rows(0).Item(2), DataGridView3.Rows(i).Cells(5).Value, DataGridView3.Rows(i).Cells(6).Value)
                End If
            End If
        Next



        Dim ht, ht7, ht10, ht14, ht20 As Double
        Dim tva, tva7, tva10, tva14, tva20 As Double
        Dim sum0 As Double
        Dim sum7 As Double
        Dim sum10 As Double
        Dim sum14 As Double
        Dim sum20 As Double

        For j = 0 To depense.DataGridView1.Rows.Count() - 1

            sumttc += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "")
            sumht += depense.DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "") * (depense.DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "") - depense.DataGridView1.Rows(j).Cells(11).Value.ToString.Replace(" ", ""))
            sumrms += (depense.DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "") * (depense.DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "") - depense.DataGridView1.Rows(j).Cells(11).Value.ToString.Replace(" ", ""))) * (depense.DataGridView1.Rows(j).Cells(6).Value.ToString.Replace(" ", "") / 100)

            If depense.DataGridView1.Rows(j).Cells(9).Value = 0 Then
                sum0 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "")
                ht += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "")
                tva = 0

            End If

            If depense.DataGridView1.Rows(j).Cells(9).Value = 7 Then
                sum7 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "")
                ht7 += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "")
                tva7 = (ht7 * 7) / 100
            End If

            If depense.DataGridView1.Rows(j).Cells(9).Value = 10 Then
                sum10 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "")
                ht10 += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "")
                tva10 = (ht10 * 10) / 100
            End If

            If depense.DataGridView1.Rows(j).Cells(9).Value = 14 Then
                sum14 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "")
                ht14 += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "")
                tva14 = (ht14 * 14) / 100
            End If
            If depense.DataGridView1.Rows(j).Cells(9).Value = 20 Then
                sum20 += depense.DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "")
                ht20 += depense.DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "")
                tva20 = (ht20 * 20) / 100
            End If

        Next

        depense.DataGridView2.Rows(0).Cells(1).Value = Convert.ToDouble(ht.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(0).Cells(2).Value = Convert.ToDouble(tva.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(0).Cells(3).Value = Convert.ToDouble(sum0.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(1).Cells(1).Value = Convert.ToDouble(ht7.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(1).Cells(2).Value = Convert.ToDouble(tva7.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(1).Cells(3).Value = Convert.ToDouble(sum7.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(2).Cells(1).Value = Convert.ToDouble(ht10.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(2).Cells(2).Value = Convert.ToDouble(tva10.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(2).Cells(3).Value = Convert.ToDouble(sum10.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(3).Cells(1).Value = Convert.ToDouble(ht14.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(3).Cells(2).Value = Convert.ToDouble(tva14.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(3).Cells(3).Value = Convert.ToDouble(sum14.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(4).Cells(1).Value = Convert.ToDouble(ht20.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(4).Cells(2).Value = Convert.ToDouble(tva20.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.DataGridView2.Rows(4).Cells(3).Value = Convert.ToDouble(sum20.ToString.Replace(".", ",")).ToString("# #00.00")


        adpt = New MySqlDataAdapter("select client,transport from orders where OrderID = '" + Label2.Text.ToString + "' ", conn2)
        Dim table3 As New DataTable
        adpt.Fill(table3)

        depense.Label15.Text = Convert.ToDouble(sumttc.ToString.Replace(".", ",") + Convert.ToDouble(table3.Rows(0).Item(1).ToString.Replace(".", ",").Replace(" ", ""))).ToString("# #00.00")
        depense.Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.Label27.Text = Convert.ToDouble(sumrms.ToString.Replace(".", ",")).ToString("# #00.00")
        depense.Label10.Text = Convert.ToDouble(sumttc - (sumht - sumrms)).ToString("# #00.00")



        adpt2 = New MySqlDataAdapter("select client,adresse,ville,ICE from clients where client = '" + table3.Rows(0).Item(0).ToString + "' ", conn2)
        Dim table2 As New DataTable
        adpt2.Fill(table2)

        depense.Label7.Text = table2.Rows(0).Item(0)
        depense.Label34.Text = table3.Rows(0).Item(1)
        depense.Label8.Text = table2.Rows(0).Item(1)
        depense.Label12.Text = table2.Rows(0).Item(2)

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
            num = Val(table4.Rows(0).Item(0)) + 1
        Else
            num = Year(Now) & "0001"
        End If

        depense.TextBox1.Text = "Facture N° " & num
        depense.Label6.Text = DateTimePicker1.Text

        depense.Label33.Text = Label6.Text

        If Label6.Text <= 0 Then
            depense.Label32.Text = "Facture payée"
            depense.Label31.Text = Label5.Text
            depense.Label32.ForeColor = Color.Green
        End If

    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Close()

    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        conn2.Close()

        Dim cmd As MySqlCommand
                        Dim cmd2, cmd3 As MySqlCommand





        adpt = New MySqlDataAdapter("SELECT * from orderdetails where OrderID = '" & Label2.Text & "'", conn2)
        Dim tabletick As New DataTable
        adpt.Fill(tabletick)

        adpt = New MySqlDataAdapter("SELECT * from orders where OrderID = '" & Label2.Text & "'", conn2)
        Dim tabletick2 As New DataTable
        adpt.Fill(tabletick2)


        Dim sumqty As Double = 0
        Dim ds As New DataSet1
                            Dim dt As New DataTable
                            dt = ds.Tables("tick")
        For i = 0 To tabletick.Rows.Count - 1
            If tabletick.Rows(i).Item(12) = 0 Then
                dt.Rows.Add("", tabletick.Rows(i).Item(9), "", Convert.ToDouble(tabletick.Rows(i).Item(3).ToString.Replace(" ", "")).ToString("N2"), "", tabletick.Rows(i).Item(2), "", Convert.ToDouble(tabletick.Rows(i).Item(4).ToString.Replace(" ", "")).ToString("N2"), tabletick.Rows(i).Item(14))
                sumqty += Convert.ToDecimal(tabletick.Rows(i).Item(3).ToString.Replace(" ", "").Replace(".", ","))
            End If
        Next
        dt.Rows.Add("", "----- Produits Gratuits -----")
        For i = 0 To tabletick.Rows.Count - 1
            If tabletick.Rows(i).Item(12) > 0 Then
                dt.Rows.Add("", tabletick.Rows(i).Item(9) & " (Gratuit)", "", tabletick.Rows(i).Item(3), "", 0, "", 0, 0)
                sumqty += Convert.ToDecimal(tabletick.Rows(i).Item(3).ToString.Replace(" ", "").Replace(".", ","))
            End If
        Next

        adpt = New MySqlDataAdapter("SELECT id from bondelivr where order_id = '" & Label2.Text & "'", conn2)
        Dim tabletick3 As New DataTable
        adpt.Fill(tabletick3)
        If tabletick3.Rows().Count() <> 0 Then

            ReportToPrint = New LocalReport()
            ReportToPrint.ReportPath = Application.StartupPath + "\Report4.rdlc"
            ReportToPrint.DataSources.Clear()
            ReportToPrint.EnableExternalImages = True


            Dim cliente As New ReportParameter("client", tabletick2.Rows(0).Item(12).ToString)
            Dim client1(0) As ReportParameter
            client1(0) = cliente
            ReportToPrint.SetParameters(client1)



            Dim ide As New ReportParameter("id", tabletick3.Rows(0).Item(0).ToString)
            Dim id1(0) As ReportParameter
            id1(0) = ide
            ReportToPrint.SetParameters(id1)

            Dim vt As New ReportParameter("vente", tabletick2.Rows(0).Item(0).ToString)
            Dim vt1(0) As ReportParameter
            vt1(0) = vt
            ReportToPrint.SetParameters(vt1)

            Dim daty As New ReportParameter("date", tabletick2.Rows(0).Item(1).ToString)
            Dim daty1(0) As ReportParameter
            daty1(0) = daty
            ReportToPrint.SetParameters(daty1)

            Dim fraisN As New ReportParameter("FraisN", "Cout Logistiques :")
            Dim fraisN1(0) As ReportParameter
            fraisN1(0) = fraisN
            ReportToPrint.SetParameters(fraisN1)

            Dim fraisM As New ReportParameter("FraisM", tabletick2.Rows(0).Item(14).ToString)
            Dim fraisM1(0) As ReportParameter
            fraisM1(0) = fraisM
            ReportToPrint.SetParameters(fraisM1)

            Dim caissier As New ReportParameter("caissier", tabletick2.Rows(0).Item(3).ToString)
            Dim caissier1(0) As ReportParameter
            caissier1(0) = caissier
            ReportToPrint.SetParameters(caissier1)

            Dim total As New ReportParameter("total", Convert.ToDouble(tabletick2.Rows(0).Item(2).ToString.Replace(" ", "")))
            Dim total1(0) As ReportParameter
            total1(0) = total
            ReportToPrint.SetParameters(total1)

            Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
            Dim table23 As New DataTable
            adpt23.Fill(table23)

            Dim info As New ReportParameter("info", table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString & vbCrLf & table23.Rows(0).Item(6).ToString)
            Dim info1(0) As ReportParameter
            info1(0) = info
            ReportToPrint.SetParameters(info1)

            Dim livreur As New ReportParameter("livreur", tabletick2.Rows(0).Item(15).ToString)
            Dim livreur1(0) As ReportParameter
            livreur1(0) = livreur
            ReportToPrint.SetParameters(livreur1)


            Dim count As New ReportParameter("count", tabletick.Rows.Count.ToString)
            Dim count1(0) As ReportParameter
            count1(0) = count
            ReportToPrint.SetParameters(count1)

            Dim qty As New ReportParameter("qty", sumqty.ToString)
            Dim qty1(0) As ReportParameter
            qty1(0) = qty
            ReportToPrint.SetParameters(qty1)

            Dim appPath As String = Application.StartupPath()

            Dim SaveDirectory As String = appPath & "\"
            Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

            Dim img As New ReportParameter("img", "File:\" + SavePath, True)
            Dim img1(0) As ReportParameter
            img1(0) = img
            ReportToPrint.SetParameters(img1)

            ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

            'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
            'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
            Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>5.83in</PageWidth><PageHeight>8.3in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
            Dim warnings As Warning()
            m_streams = New List(Of Stream)()

            ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)



            For Each stream As Stream In m_streams
                stream.Position = 0
            Next

            Dim printDoc As New PrintDocument()
            'Microsoft Print To PDF
            'C80250 Series
            'C80250 Series init
            Dim printname As String = a5printer
            printDoc.PrinterSettings.PrinterName = printname
            Dim ps As New PrinterSettings()
            ps.PrinterName = printDoc.PrinterSettings.PrinterName
            printDoc.PrinterSettings = ps

            AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
            m_currentPageIndex = 0
            printDoc.DefaultPageSettings.PaperSize = New PaperSize("A5", 583, 827)
            ' Demander à l'utilisateur le nombre de copies à imprimer
            Dim input As String = InputBox("Veuillez entrer le nombre de copies à imprimer:", "Nombre de copies", "1")

            ' Vérifier si l'utilisateur a cliqué sur "Annuler" ou n'a rien saisi
            If String.IsNullOrEmpty(input) Then
                Return ' Annuler l'impression si l'utilisateur n'a rien saisi ou a cliqué sur "Annuler"
            End If

            ' Convertir la saisie en un nombre entier
            Dim numberOfCopies As Integer
            If Not Integer.TryParse(input, numberOfCopies) Then
                MessageBox.Show("Veuillez entrer un nombre valide pour le nombre de copies.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return ' Annuler l'impression en cas de saisie invalide
            End If
            printDoc.PrinterSettings.Copies = numberOfCopies
            ' Vérifier et ajuster les marges par défaut de l'imprimante si nécessaire
            ps.DefaultPageSettings.Margins = New Margins(0, 0, 0, 0)
            printDoc.Print()
        Else
            adpt = New MySqlDataAdapter("SELECT id from bondelivr order by id desc", conn2)
            Dim tabletick4 As New DataTable
            adpt.Fill(tabletick4)
            Dim blnum As Integer
            If tabletick4.Rows.Count() = 0 Then
                blnum = 1
            Else
                blnum = tabletick4.Rows(0).Item(0) + 1
            End If

            ReportToPrint = New LocalReport()
            ReportToPrint.ReportPath = Application.StartupPath + "\Report4.rdlc"
            ReportToPrint.DataSources.Clear()
            ReportToPrint.EnableExternalImages = True


            Dim cliente As New ReportParameter("client", tabletick2.Rows(0).Item(12).ToString)
            Dim client1(0) As ReportParameter
            client1(0) = cliente
            ReportToPrint.SetParameters(client1)



            Dim ide As New ReportParameter("id", blnum.ToString)
            Dim id1(0) As ReportParameter
            id1(0) = ide
            ReportToPrint.SetParameters(id1)

            Dim vt As New ReportParameter("vente", tabletick2.Rows(0).Item(0).ToString)
            Dim vt1(0) As ReportParameter
            vt1(0) = vt
            ReportToPrint.SetParameters(vt1)

            Dim daty As New ReportParameter("date", tabletick2.Rows(0).Item(1).ToString)
            Dim daty1(0) As ReportParameter
            daty1(0) = daty
            ReportToPrint.SetParameters(daty1)

            Dim fraisN As New ReportParameter("FraisN", "Cout Logistiques :")
            Dim fraisN1(0) As ReportParameter
            fraisN1(0) = fraisN
            ReportToPrint.SetParameters(fraisN1)

            Dim fraisM As New ReportParameter("FraisM", tabletick2.Rows(0).Item(14).ToString)
            Dim fraisM1(0) As ReportParameter
            fraisM1(0) = fraisM
            ReportToPrint.SetParameters(fraisM1)

            Dim caissier As New ReportParameter("caissier", tabletick2.Rows(0).Item(3).ToString)
            Dim caissier1(0) As ReportParameter
            caissier1(0) = caissier
            ReportToPrint.SetParameters(caissier1)

            Dim total As New ReportParameter("total", Convert.ToDouble(tabletick2.Rows(0).Item(2)).ToString("# ##0.00"))
            Dim total1(0) As ReportParameter
            total1(0) = total
            ReportToPrint.SetParameters(total1)

            Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
            Dim table23 As New DataTable
            adpt23.Fill(table23)

            Dim info As New ReportParameter("info", table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString & vbCrLf & table23.Rows(0).Item(6).ToString)
            Dim info1(0) As ReportParameter
            info1(0) = info
            ReportToPrint.SetParameters(info1)

            Dim livreur As New ReportParameter("livreur", tabletick2.Rows(0).Item(15).ToString)
            Dim livreur1(0) As ReportParameter
            livreur1(0) = livreur
            ReportToPrint.SetParameters(livreur1)


            Dim count As New ReportParameter("count", tabletick.Rows.Count.ToString)
            Dim count1(0) As ReportParameter
            count1(0) = count
            ReportToPrint.SetParameters(count1)

            Dim qty As New ReportParameter("qty", sumqty.ToString)
            Dim qty1(0) As ReportParameter
            qty1(0) = qty
            ReportToPrint.SetParameters(qty1)

            Dim appPath As String = Application.StartupPath()

            Dim SaveDirectory As String = appPath & "\"
            Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

            Dim img As New ReportParameter("img", "File:\" + SavePath, True)
            Dim img1(0) As ReportParameter
            img1(0) = img
            ReportToPrint.SetParameters(img1)

            ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

            'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
            'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
            Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>5.83in</PageWidth><PageHeight>8.3in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
            Dim warnings As Warning()
            m_streams = New List(Of Stream)()

            ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)



            For Each stream As Stream In m_streams
                stream.Position = 0
            Next

            Dim printDoc As New PrintDocument()
            'Microsoft Print To PDF
            'C80250 Series
            'C80250 Series init
            Dim printname As String = a5printer
            printDoc.PrinterSettings.PrinterName = printname
            Dim ps As New PrinterSettings()
            ps.PrinterName = printDoc.PrinterSettings.PrinterName
            printDoc.PrinterSettings = ps

            AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
            m_currentPageIndex = 0
            printDoc.DefaultPageSettings.PaperSize = New PaperSize("A5", 583, 827)
            ' Demander à l'utilisateur le nombre de copies à imprimer
            Dim input As String = InputBox("Veuillez entrer le nombre de copies à imprimer:", "Nombre de copies", "1")

            ' Vérifier si l'utilisateur a cliqué sur "Annuler" ou n'a rien saisi
            If String.IsNullOrEmpty(input) Then
                Return ' Annuler l'impression si l'utilisateur n'a rien saisi ou a cliqué sur "Annuler"
            End If

            ' Convertir la saisie en un nombre entier
            Dim numberOfCopies As Integer
            If Not Integer.TryParse(input, numberOfCopies) Then
                MessageBox.Show("Veuillez entrer un nombre valide pour le nombre de copies.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return ' Annuler l'impression en cas de saisie invalide
            End If
            printDoc.PrinterSettings.Copies = numberOfCopies
            ' Vérifier et ajuster les marges par défaut de l'imprimante si nécessaire
            ps.DefaultPageSettings.Margins = New Margins(0, 0, 0, 0)
            printDoc.Print()
        End If
    End Sub



    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click

        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Annulation des ventes'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Dim id = Label2.Text
                Dim Rep As Integer = MsgBox("Voulez-vous vraiment annuler cette vente ?", vbYesNo)
                If Rep = vbYes Then
                    conn2.Close()
                    conn2.Open()
                    Dim cmd As New MySqlCommand
                    For i = 0 To DataGridView3.Rows.Count - 1
                        cmd = New MySqlCommand("UPDATE `article` SET `Stock` = `Stock` +  @stck  WHERE `Code` = @cd", conn2)
                        cmd.Parameters.AddWithValue("@stck", DataGridView3.Rows(i).Cells(2).Value)
                        cmd.Parameters.AddWithValue("@cd", DataGridView3.Rows(i).Cells(0).Value)
                        cmd.ExecuteNonQuery()
                    Next
                    cmd = New MySqlCommand("DELETE FROM `orderdetails` WHERE `OrderID` = " + Label2.Text, conn2)
                    cmd.ExecuteNonQuery()
                    cmd = New MySqlCommand("DELETE FROM `orders` WHERE `OrderID` = " + Label2.Text, conn2)
                    cmd.ExecuteNonQuery()
                    conn2.Close()

                    conn2.Open()
                    cmd = New MySqlCommand("INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op)", conn2)
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@name", user)
                    cmd.Parameters.AddWithValue("@op", "Annulation du ticket N° " & Label2.Text)
                    cmd.ExecuteNonQuery()
                    conn2.Close()

                    Me.Close()
                End If
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub ventedetail_Click(sender As Object, e As EventArgs) Handles Me.Click
        depense.Close()
    End Sub
End Class
