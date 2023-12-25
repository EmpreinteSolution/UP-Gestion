Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Imports My_animals.numconv
Imports System.Runtime.InteropServices
Public Class depense
    Dim adpt As MySqlDataAdapter
    Private Sub depense_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Dim SaveDirectory As String = "c:\pospictures"
        Dim SavePath As String = System.IO.Path.Combine(SaveDirectory, imgName)
        If System.IO.File.Exists(SavePath) Then
            PictureBox1.Image = Image.FromFile(SavePath)
        End If

        Label1.Text = user
        Dim w = Screen.PrimaryScreen.WorkingArea.Width
        Dim h = My.Computer.Screen.WorkingArea.Size.Height
        Me.Width = w
        Me.Height = h
        Me.Location = New Point(0, 0)

        Dim pkInstalledPrinters As String

        ' Find all printers installed
        For Each pkInstalledPrinters In
        PrinterSettings.InstalledPrinters
            ComboBox1.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        ' Set the combo to the first printer in the list
        ComboBox1.SelectedIndex = 0

        TextBox8.Text = Label15.Text

        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim tablebq As New DataTable
        adpt.Fill(tablebq)

        For i = 0 To tablebq.Rows.Count - 1
            ComboBox4.Items.Add(tablebq.Rows(i).Item(1))
        Next

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
    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Me.Close()
    End Sub


    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim adpt As New MySqlDataAdapter("select * from clients where client = '" + ComboBox2.SelectedItem.ToString + "'", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Label7.Text = table.Rows(0).Item(1)
        Label8.Text = table.Rows(0).Item(6)
        Label12.Text = table.Rows(0).Item(7)
        Label2.Text = table.Rows(0).Item(12)
    End Sub
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

    <Obsolete>
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        If DataGridView1.Rows.Count <= 0 Then
            MsgBox(“Ticket Vide ! ”)
        Else


            Dim cmd As MySqlCommand
            Dim cmd2, cmd3 As MySqlCommand
            Dim sql As String
            Dim sql2, sql3 As String
            Dim execution As Integer
            adpt = New MySqlDataAdapter("select OrderID from facture order by OrderID desc", conn2)
            Dim table4 As New DataTable
            adpt.Fill(table4)
            Dim id As Integer
            If table4.Rows.Count <> 0 Then
                If Convert.ToDouble(Year(Now) & "0001") > (Val(table4.Rows(0).Item(0)) + 1) Then
                    id = Year(Now) & "0001"
                Else

                    id = Val(table4.Rows(0).Item(0)) + 1
                End If
            Else
                id = Year(Now) & "0001"
            End If
            Dim etat As String
            Dim mode As String
            Dim reste As Double
            Dim paye As Double
            If Label32.Text = "Non Payée" Then
                reste = Convert.ToDouble(Label15.Text).ToString("#0.00").Replace(".", ",")
                paye = 0.00
            Else
                reste = 0.00
                paye = Convert.ToDouble(Label15.Text).ToString("#0.00").Replace(".", ",")
            End If

            If Label33.Text > 0 Then
                etat = "Non Payé"
                mode = "Crédit"
            Else
                conn2.Open()
                etat = "Payé"
                mode = Label31.Text


            End If
            conn2.Close()
            conn2.Open()
            For Each row As DataGridViewRow In archive.DataGridView1.Rows
                ' Vérifier si la case à cocher de la ligne est cochée
                If Convert.ToBoolean(row.Cells("CheckBoxColumn").Value) = True Then
                    Dim ticid As String = row.Cells(10).Value.ToString
                    sql2 = "UPDATE `orders` SET `etat`='fac',`fac_id` = '" & id & "' WHERE `OrderID` = '" + ticid.ToString + "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@name", archive.Label2.Text)
                    cmd3.Parameters.AddWithValue("@op", "Transformation de la Vente N° " & ticid & " en Facture N° " & id.ToString)
                    cmd3.ExecuteNonQuery()

                    sql2 = "UPDATE `cheques` SET `fac`= '" & id & "' WHERE `tick` = '" + ticid.ToString + "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                End If
            Next



            conn2.Close()
                    Dim infs As String = "Le " & DateTime.Now.ToString("yyyy-MM-dd HH:mm") & " par " & Label1.Text
                    conn2.Open()
            sql2 = "INSERT INTO facture (`OrderID`, `OrderDate`, `MontantOrder`,`etat`,`client`,`infos`,`paye`,`reste`,`modePayement`,`trsprt`,`remisepour`,`remise`) 
                    VALUES ('" & id & "', '" & Convert.ToDateTime(Label6.Text).ToString("yyyy-MM-dd HH:mm:ss") & "','" + Label15.Text + "','" + etat + "','" + Label7.Text + "','" & infs & "','" & paye & "','" & reste & "','" & mode & "','" & Label34.Text.Replace(".", ",") & "','" & TextBox9.Text & "','" & Label27.Text & "')"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
                    cmd2.ExecuteNonQuery()
                    For Each dr As DataGridViewRow In Me.DataGridView1.Rows
                        conn2.Close()

                Try
                    conn2.Open()
                    cmd = New MySqlCommand
                    sql2 = "UPDATE `article` SET `facture`= REPLACE(`facture`,',','.') - '" & dr.Cells(3).Value.ToString.Replace(",", ".").Replace(" ", "") & "' '" & id & "' WHERE `Code` = '" + dr.Cells(0).Value + "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()
                    cmd = New MySqlCommand
                    sql = “INSERT INTO facturedetails (`fac_id`, `ref`, `des`, `unite`, `qte`, `pu_ht`, `pu_ttc`, `remise`, `tot_ht`, `tot_ttc`, `tva`,`marge`,`gr`) VALUES (@facid, @ref,@des,@unite,@qte,@puht,@puttc,@remise,@totht,@totttc,@tva,@marge,@gr);”
                    prod = dr.Cells(5).Value
                    With cmd
                        .Connection = conn2
                        .CommandText = sql
                        .Parameters.Clear()
                        .Parameters.AddWithValue(“@facid”, id)
                        .Parameters.AddWithValue(“@ref”, dr.Cells(0).Value)
                        .Parameters.AddWithValue(“@des”, dr.Cells(1).Value)
                        .Parameters.AddWithValue(“@unite”, "U")
                        .Parameters.AddWithValue(“@qte”, dr.Cells(3).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@puht”, dr.Cells(4).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@puttc”, dr.Cells(5).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@remise”, dr.Cells(6).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@totht”, dr.Cells(7).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@totttc”, dr.Cells(8).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@tva”, dr.Cells(9).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@marge”, dr.Cells(10).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@gr”, dr.Cells(11).Value.ToString.Replace(".", ","))
                        execution = .ExecuteNonQuery()
                    End With

                    conn2.Close()
                Catch ex As MySqlException
                    MsgBox(ex.Message)
                        End Try
                    Next




            Dim ds As New DataSet1
                    Dim dt As New DataTable
                    dt = ds.Tables("tick")
                    For i = 0 To DataGridView1.Rows.Count - 1
                dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(7).Value.ToString.Replace(" ", ""), DataGridView1.Rows(i).Cells(8).Value, Convert.ToDouble(DataGridView1.Rows(i).Cells(9).Value).ToString("N0"), DataGridView1.Rows(i).Cells(11).Value, DataGridView1.Rows(i).Cells(6).Value)
            Next

            Dim ht0, ht7, ht10, ht14, ht20 As Double
            Dim tx0, tx7, tx10, tx14, tx20 As Double
            Dim ttc0, ttc7, ttc10, ttc14, ttc20 As Double

            Dim ds2 As New DataSet2
                    Dim dt2 As New DataTable
                    dt2 = ds2.Tables("tva")
            ht0 = DataGridView2.Rows(0).Cells(1).Value.ToString.Replace(" ", "")
            ht7 = DataGridView2.Rows(1).Cells(1).Value.ToString.Replace(" ", "")
            ht10 = DataGridView2.Rows(2).Cells(1).Value.ToString.Replace(" ", "")
            ht14 = DataGridView2.Rows(3).Cells(1).Value.ToString.Replace(" ", "")
            ht20 = DataGridView2.Rows(4).Cells(1).Value.ToString.Replace(" ", "")

            tx0 = DataGridView2.Rows(0).Cells(2).Value.ToString.Replace(" ", "")
            tx7 = DataGridView2.Rows(1).Cells(2).Value.ToString.Replace(" ", "")
            tx10 = DataGridView2.Rows(2).Cells(2).Value.ToString.Replace(" ", "")
            tx14 = DataGridView2.Rows(3).Cells(2).Value.ToString.Replace(" ", "")
            tx20 = DataGridView2.Rows(4).Cells(2).Value.ToString.Replace(" ", "")

            ttc0 = DataGridView2.Rows(0).Cells(3).Value.ToString.Replace(" ", "")
            ttc7 = DataGridView2.Rows(1).Cells(3).Value.ToString.Replace(" ", "")
            ttc10 = DataGridView2.Rows(2).Cells(3).Value.ToString.Replace(" ", "")
            ttc14 = DataGridView2.Rows(3).Cells(3).Value.ToString.Replace(" ", "")
            ttc20 = DataGridView2.Rows(4).Cells(3).Value.ToString.Replace(" ", "")

            Try
                ReportToPrint = New LocalReport()
                formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\Report2.rdlc"
                formata4.ReportViewer1.LocalReport.DataSources.Clear()
                formata4.ReportViewer1.LocalReport.EnableExternalImages = True

                Dim cliente As New ReportParameter("client", Label7.Text)
                Dim client1(0) As ReportParameter
                client1(0) = cliente
                formata4.ReportViewer1.LocalReport.SetParameters(client1)

                Dim fac As New ReportParameter("fac", "Facture N° " & id.ToString)
                Dim fac1(0) As ReportParameter
                fac1(0) = fac
                formata4.ReportViewer1.LocalReport.SetParameters(fac1)

                Dim remarque As New ReportParameter("remarque", Label32.Text)
                Dim remarque1(0) As ReportParameter
                remarque1(0) = remarque
                formata4.ReportViewer1.LocalReport.SetParameters(remarque1)

                Dim type As New ReportParameter("type", "Facture")
                Dim type1(0) As ReportParameter
                type1(0) = type
                formata4.ReportViewer1.LocalReport.SetParameters(type1)

                Dim logistique As New ReportParameter("logistique", Label34.Text)
                Dim logistique1(0) As ReportParameter
                logistique1(0) = logistique
                formata4.ReportViewer1.LocalReport.SetParameters(logistique1)

                adpt = New MySqlDataAdapter("select * from infos", conn2)
                Dim table3 As New DataTable
                adpt.Fill(table3)

                Dim datee As New ReportParameter("date", Label6.Text)
                Dim date1(0) As ReportParameter
                date1(0) = datee
                formata4.ReportViewer1.LocalReport.SetParameters(date1)

                Dim tva As New ReportParameter("tva", Convert.ToDouble(Label10.Text).ToString("N2"))
                Dim tva1(0) As ReportParameter
                tva1(0) = tva
                formata4.ReportViewer1.LocalReport.SetParameters(tva1)

                Dim ttc As New ReportParameter("ttc", Convert.ToDouble(Label15.Text).ToString("N2"))
                Dim ttc1(0) As ReportParameter
                ttc1(0) = ttc
                formata4.ReportViewer1.LocalReport.SetParameters(ttc1)

                Dim ht00 As New ReportParameter("ht0", ht0)
                Dim ht01(0) As ReportParameter
                ht01(0) = ht00
                formata4.ReportViewer1.LocalReport.SetParameters(ht01)
                Dim ht70 As New ReportParameter("ht7", ht7)
                Dim ht71(0) As ReportParameter
                ht71(0) = ht70
                formata4.ReportViewer1.LocalReport.SetParameters(ht71)
                Dim ht100 As New ReportParameter("ht10", ht10)
                Dim ht101(0) As ReportParameter
                ht101(0) = ht100
                formata4.ReportViewer1.LocalReport.SetParameters(ht101)
                Dim ht140 As New ReportParameter("ht14", ht14)
                Dim ht141(0) As ReportParameter
                ht141(0) = ht140
                formata4.ReportViewer1.LocalReport.SetParameters(ht141)
                Dim ht200 As New ReportParameter("ht20", ht20)
                Dim ht201(0) As ReportParameter
                ht201(0) = ht200
                formata4.ReportViewer1.LocalReport.SetParameters(ht201)

                Dim tx00 As New ReportParameter("tx0", tx0)
                Dim tx01(0) As ReportParameter
                tx01(0) = tx00
                formata4.ReportViewer1.LocalReport.SetParameters(tx01)
                Dim tx70 As New ReportParameter("tx7", tx7)
                Dim tx71(0) As ReportParameter
                tx71(0) = tx70
                formata4.ReportViewer1.LocalReport.SetParameters(tx71)
                Dim tx100 As New ReportParameter("tx10", tx10)
                Dim tx101(0) As ReportParameter
                tx101(0) = tx100
                formata4.ReportViewer1.LocalReport.SetParameters(tx101)
                Dim tx140 As New ReportParameter("tx14", tx14)
                Dim tx141(0) As ReportParameter
                tx141(0) = tx140
                formata4.ReportViewer1.LocalReport.SetParameters(tx141)
                Dim tx200 As New ReportParameter("tx20", tx20)
                Dim tx201(0) As ReportParameter
                tx201(0) = tx200
                formata4.ReportViewer1.LocalReport.SetParameters(tx201)

                Dim ttc00 As New ReportParameter("ttc0", ttc0)
                Dim ttc01(0) As ReportParameter
                ttc01(0) = ttc00
                formata4.ReportViewer1.LocalReport.SetParameters(ttc01)
                Dim ttc70 As New ReportParameter("ttc7", ttc7)
                Dim ttc71(0) As ReportParameter
                ttc71(0) = ttc70
                formata4.ReportViewer1.LocalReport.SetParameters(ttc71)
                Dim ttc100 As New ReportParameter("ttc10", ttc10)
                Dim ttc101(0) As ReportParameter
                ttc101(0) = ttc100
                formata4.ReportViewer1.LocalReport.SetParameters(ttc101)
                Dim ttc140 As New ReportParameter("ttc14", ttc14)
                Dim ttc141(0) As ReportParameter
                ttc141(0) = ttc140
                formata4.ReportViewer1.LocalReport.SetParameters(ttc141)
                Dim ttc200 As New ReportParameter("ttc20", ttc20)
                Dim ttc201(0) As ReportParameter
                ttc201(0) = ttc200
                formata4.ReportViewer1.LocalReport.SetParameters(ttc201)


                Dim number As Double
                Dim convertednumbre As String
                If Double.TryParse(Label15.Text.Replace(",00", "").Replace(".00", "").Replace(".", ","), number) Then
                    Dim numberInWords As String = number.ToLettres(Pays.France, Devise.Aucune)
                    convertednumbre = numberInWords.Replace(" et zéro centimes", "").Replace(" et zéro", "")
                Else
                    convertednumbre = "Entrez un nombre valide dans Label1"
                End If

                Dim lettre As New ReportParameter("lettre", convertednumbre)
                Dim lettre1(0) As ReportParameter
                lettre1(0) = lettre
                formata4.ReportViewer1.LocalReport.SetParameters(lettre1)

                If Label27.Text = "0,00" Or Convert.ToDouble(Label27.Text) = 0 Then
                    Dim rmspr As New ReportParameter("remiso", "")
                    Dim rmspr1(0) As ReportParameter
                    rmspr1(0) = rmspr
                    formata4.ReportViewer1.LocalReport.SetParameters(rmspr1)
                Else
                    If TextBox9.Text = 0 Then
                        Dim rmspr As New ReportParameter("remiso", "Remise : " & Convert.ToDouble(Label27.Text).ToString("N2") & " DHs")
                        Dim rmspr1(0) As ReportParameter
                        rmspr1(0) = rmspr
                        formata4.ReportViewer1.LocalReport.SetParameters(rmspr1)
                    Else

                        Dim rmspr As New ReportParameter("remiso", "Remise (" & TextBox9.Text & "%) : " & Convert.ToDouble(Label27.Text).ToString("N2") & " DHs")
                        Dim rmspr1(0) As ReportParameter
                        rmspr1(0) = rmspr
                        formata4.ReportViewer1.LocalReport.SetParameters(rmspr1)
                    End If

                End If

                Dim neto As Double = Convert.ToDouble(Label9.Text) - Convert.ToDouble(Label27.Text)


                Dim net As New ReportParameter("net", Convert.ToDouble(neto).ToString("N2"))
                Dim net1(0) As ReportParameter
                net1(0) = net
                formata4.ReportViewer1.LocalReport.SetParameters(net1)

                Dim appPath As String = Application.StartupPath()

                Dim SaveDirectory As String = appPath & "\"
                Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                Dim img As New ReportParameter("img", "File:\" + SavePath, True)
                Dim img1(0) As ReportParameter
                img1(0) = img
                formata4.ReportViewer1.LocalReport.SetParameters(img1)

                Dim ice As New ReportParameter("ice", Label2.Text)
                Dim ice1(0) As ReportParameter
                ice1(0) = ice
                formata4.ReportViewer1.LocalReport.SetParameters(ice1)


                Dim details As New ReportParameter("infos", table3.Rows(0).Item(1).ToString + " | " + "Tél: " + table3.Rows(0).Item(2).ToString + " | " + "Email: " + table3.Rows(0).Item(3).ToString + " | " + "ICE: " + table3.Rows(0).Item(4).ToString + " | " + "IF: " + table3.Rows(0).Item(5).ToString + " | " + "TP: " + table3.Rows(0).Item(7).ToString + " | " + "RC: " + table3.Rows(0).Item(8).ToString)
                Dim details1(0) As ReportParameter
                details1(0) = details
                formata4.ReportViewer1.LocalReport.SetParameters(details1)



                'formata4.ReportViewer1.LocalReport.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
                formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dt2))

                Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
                Dim warnings As Warning()
                m_streams = New List(Of Stream)()
#Disable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.
                formata4.ReportViewer1.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
#Enable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.
                For Each stream As Stream In m_streams
                    stream.Position = 0
                Next

                Dim printDoc As New PrintDocument()
                AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
                m_currentPageIndex = 0

                Dim pageSettings As New PageSettings()

                ' Définissez la taille de la page
                pageSettings.PaperSize = New PaperSize("Custom Size", CInt(8.27 * 100), CInt(11.69 * 100)) ' Convertissez les pouces en centièmes de pouce (1 pouce = 100 centièmes de pouce)

                ' Définissez les marges si nécessaire
                pageSettings.Margins = New Margins(0, 0, 0, 0) ' Vous pouvez ajuster ces valeurs en fonction de vos besoins

                ' Assurez-vous que l'orientation est en mode portrait (si nécessaire)
                pageSettings.Landscape = False

                ' Appliquez les paramètres de page à ReportViewer
                formata4.ReportViewer1.SetPageSettings(pageSettings)

                ' Configuration du PrintPreviewDialog
                formata4.Show()

                tbl.Rows.Clear()
                tbl2.Rows.Clear()
            Catch
            End Try
            '                'If ComboBox1.Text = "Espèce" Then
            '                '    Dim codeOpenCashDrawer As Byte() = New Byte() {27, 112, 48, 55, 121}
            '                '    Dim pUnmanagedBytes As IntPtr = New IntPtr(0)
            '                '    pUnmanagedBytes = Marshal.AllocCoTaskMem(5)
            '                '    Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5)
            '                '    RawPrinterHelper.SendBytesToPrinter("C80250 Series init", pUnmanagedBytes, 5)
            '                '    Marshal.FreeCoTaskMem(pUnmanagedBytes)
            '                'End If
            '                'MsgBox("facture enregistrée")


            'IconButton5.PerformClick()


            Me.Close()
            ventedetail.Close()
            facturedetails.Close()
            conn2.Close()
            conn2.Open()
            sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@name", archive.Label2.Text)
                    cmd3.Parameters.AddWithValue("@op", "Transformation du ticket N° " & ventedetail.Label2.Text & " en Facture N° " & id.ToString)
            cmd3.ExecuteNonQuery()


            sql3 = "UPDATE `clients` SET `remisepour` = @pourc, `remise`=@rms WHERE `client` = @id"
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            cmd3.Parameters.AddWithValue("@id", Label7.Text)
            cmd3.Parameters.AddWithValue("@pourc", 0)
            cmd3.Parameters.AddWithValue("@rms", 0)

            cmd3.ExecuteNonQuery()

            conn2.Close()



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
    Friend Class RawPrinterHelper
        ' Structure and API declarions:
        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
        Public Class DOCINFOA
            <MarshalAs(UnmanagedType.LPStr)>
            Public pDocName As String
            <MarshalAs(UnmanagedType.LPStr)>
            Public pOutputFile As String
            <MarshalAs(UnmanagedType.LPStr)>
            Public pDataType As String
        End Class

        <DllImport("winspool.Drv", EntryPoint:="OpenPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function OpenPrinter(
        <MarshalAs(UnmanagedType.LPStr)> ByVal szPrinter As String, <Out> ByRef hPrinter As IntPtr, ByVal pd As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="ClosePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Integer,
        <[In], MarshalAs(UnmanagedType.LPStruct)> ByVal di As DOCINFOA) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="WritePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Integer, <Out> ByRef dwWritten As Integer) As Boolean
        End Function
    End Class

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Dim cmd As MySqlCommand
        Dim cmd2 As MySqlCommand
        Dim sql As String
        Dim sql2 As String
        Dim execution As Integer
        adpt = New MySqlDataAdapter("select OrderID from devis order by OrderID desc", conn2)
        Dim table4 As New DataTable
        adpt.Fill(table4)
        Dim id As Integer
        If table4.Rows.Count <> 0 Then
            id = Val(table4.Rows(0).Item(0)) + 1
        Else
            id = 1
        End If

        If DataGridView1.Rows.Count <= 0 Then
            MsgBox(“Ticket Vide ! ”)
        Else
            If dashboard.ComboBox2.Text <> "" Then
                conn2.Close()
                conn2.Open()
                sql2 = "INSERT INTO devis (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`client`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Label15.Text.ToString.Replace(",", ".") + "','" + Label2.Text + "','" + Label7.Text + "')"
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.Parameters.Clear()
                cmd2.ExecuteNonQuery()
                For Each dr As DataGridViewRow In Me.DataGridView1.Rows
                    conn2.Close()
                    conn2.Open()
                    Try
                        cmd = New MySqlCommand
                        sql = “INSERT INTO orderdetails (OrderID ,ProductID ,Price,Quantity,Total,date,pa,type,name) VALUES (@tick, @code, @price, @qty, @montant, @date,@pa,@type,@name);”
                        prod = dr.Cells(5).Value
                        With cmd
                            .Connection = conn2
                            .CommandText = sql
                            .Parameters.Clear()
                            If table4.Rows().Count = 0 Then
                                .Parameters.AddWithValue(“@tick”, 1)
                                tc = 1
                            Else
                                .Parameters.AddWithValue(“@tick”, table4.Rows(0).Item(0) + 1)
                                tc = table4.Rows(0).Item(0) + 1
                            End If
                            .Parameters.AddWithValue(“@code”, dr.Cells(0).Value)
                            .Parameters.AddWithValue(“@price”, dr.Cells(5).Value)
                            .Parameters.AddWithValue(“@qty”, dr.Cells(3).Value)
                            .Parameters.AddWithValue(“@montant”, dr.Cells(7).Value)
                            .Parameters.AddWithValue(“@date”, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                            .Parameters.AddWithValue(“@pa”, 0)
                            .Parameters.AddWithValue(“@type”, "devis")
                            .Parameters.AddWithValue(“@name”, dr.Cells(1).Value)
                            execution = .ExecuteNonQuery()
                        End With
                        conn2.Close()

                    Catch ex As MySqlException
                        MsgBox(ex.Message)
                    End Try

                Next




                Dim ds As New DataSet1
                Dim dt As New DataTable
                dt = ds.Tables("tick")
                For i = 0 To DataGridView1.Rows.Count - 1
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(8).Value)
                Next


                ReportToPrint = New LocalReport()
                ReportToPrint.ReportPath = Application.StartupPath + "\devi.rdlc"
                ReportToPrint.DataSources.Clear()
                ReportToPrint.EnableExternalImages = True


                Dim cliente As New ReportParameter("client", Label7.Text + vbCrLf + Label8.Text + vbCrLf + Label12.Text)
                Dim client1(0) As ReportParameter
                client1(0) = cliente
                ReportToPrint.SetParameters(client1)

                Dim fac As New ReportParameter("fac", TextBox1.Text)
                Dim fac1(0) As ReportParameter
                fac1(0) = fac
                ReportToPrint.SetParameters(fac1)


                adpt = New MySqlDataAdapter("select * from infos", conn2)
                Dim table3 As New DataTable
                adpt.Fill(table3)

                Dim datee As New ReportParameter("date", Label6.Text)
                Dim date1(0) As ReportParameter
                date1(0) = datee
                ReportToPrint.SetParameters(date1)

                Dim ht As New ReportParameter("HT", Label9.Text)
                Dim ht1(0) As ReportParameter
                ht1(0) = ht
                ReportToPrint.SetParameters(ht1)

                Dim tva As New ReportParameter("tva", Label10.Text)
                Dim tva1(0) As ReportParameter
                tva1(0) = tva
                ReportToPrint.SetParameters(tva1)

                Dim ttc As New ReportParameter("ttc", Label15.Text)
                Dim ttc1(0) As ReportParameter
                ttc1(0) = ttc
                ReportToPrint.SetParameters(ttc1)

                Dim ice As New ReportParameter("ice", Label2.Text)
                Dim ice1(0) As ReportParameter
                ice1(0) = ice
                ReportToPrint.SetParameters(ice1)


                Dim details As New ReportParameter("infos", table3.Rows(0).Item(1).ToString + vbCrLf + "Tél : " + table3.Rows(0).Item(2).ToString + vbCrLf + "Fax : " + table3.Rows(0).Item(10).ToString)
                Dim details1(0) As ReportParameter
                details1(0) = details
                ReportToPrint.SetParameters(details1)



                'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

                Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.04985in</PageWidth><PageHeight>12in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
                Dim warnings As Warning()
                m_streams = New List(Of Stream)()
#Disable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.
                ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
#Enable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.

                For Each stream As Stream In m_streams
                    stream.Position = 0
                Next

                Dim printDoc As New PrintDocument()
                'Microsoft Print To PDF
                'C80250 Series
                'C80250 Series init
                printDoc.PrinterSettings.PrinterName = ComboBox1.Text
                Dim ps As New PrinterSettings()
                ps.PrinterName = printDoc.PrinterSettings.PrinterName
                printDoc.PrinterSettings = ps

                AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
                m_currentPageIndex = 0
                printDoc.Print()
                tbl.Rows.Clear()
                tbl2.Rows.Clear()

                'If ComboBox1.Text = "Espèce" Then
                '    Dim codeOpenCashDrawer As Byte() = New Byte() {27, 112, 48, 55, 121}
                '    Dim pUnmanagedBytes As IntPtr = New IntPtr(0)
                '    pUnmanagedBytes = Marshal.AllocCoTaskMem(5)
                '    Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5)
                '    RawPrinterHelper.SendBytesToPrinter("C80250 Series init", pUnmanagedBytes, 5)
                '    Marshal.FreeCoTaskMem(pUnmanagedBytes)
                'End If
                'MsgBox("facture enregistrée")

            Else
                MsgBox("Merci de choisir le client")

            End If
        End If
    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click
        MsgBox(Today.Date)
        ComboBox2.Visible = False
        TextBox5.Visible = True
        IconButton14.Visible = True
        IconButton15.Visible = True
    End Sub

    Private Sub IconButton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        conn2.Close()
        conn2.Open()
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = "INSERT INTO clients (name) VALUES ('" & TextBox5.Text & "')"
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.ExecuteNonQuery()
        conn2.Close()

        adpt = New MySqlDataAdapter("select name from clients", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        ComboBox2.Items.Clear()
        If table.Rows.Count <> 0 Then
            For i = 0 To table.Rows.Count - 1
                ComboBox2.Items.Add(table.Rows(i).Item(0))
            Next
        End If

        ComboBox2.SelectedItem = TextBox5.Text

        ComboBox2.Visible = True
        TextBox5.Visible = False
        IconButton14.Visible = False
        IconButton15.Visible = False
    End Sub

    Private Sub IconButton15_Click(sender As Object, e As EventArgs) Handles IconButton15.Click

        ComboBox2.Visible = True
        TextBox5.Visible = False
        IconButton14.Visible = False
        IconButton15.Visible = False

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim sumttc As Double = 0
        Dim sumht As Double = 0
        Dim sumrms As Double = 0
        Dim rms As Double = 0

        rms = Convert.ToDecimal(DataGridView1.CurrentRow.Cells.Item(6).Value.ToString.Replace(" ", "").Replace(".", ",")) / 100
        Dim totht As Double = (DataGridView1.CurrentRow.Cells.Item(4).Value * DataGridView1.CurrentRow.Cells.Item(3).Value) - ((DataGridView1.CurrentRow.Cells.Item(4).Value * DataGridView1.CurrentRow.Cells.Item(3).Value) * rms)
        Dim totttc As Double = totht * (1 + (DataGridView1.CurrentRow.Cells.Item(9).Value / 100))
        DataGridView1.CurrentRow.Cells.Item(7).Value = Convert.ToDouble(totht).ToString("# ##0.00")
        DataGridView1.CurrentRow.Cells.Item(8).Value = Convert.ToDouble(totttc).ToString("# ##0.00")

        Dim ht, ht7, ht10, ht14, ht20 As Double
        Dim tva, tva7, tva10, tva14, tva20 As Double
        Dim sum0 As Double
        Dim sum7 As Double
        Dim sum10 As Double
        Dim sum14 As Double
        Dim sum20 As Double

        For j = 0 To DataGridView1.Rows.Count() - 1

            sumttc += DataGridView1.Rows(j).Cells(8).Value
            sumht += DataGridView1.Rows(j).Cells(4).Value * (DataGridView1.Rows(j).Cells(3).Value - DataGridView1.Rows(j).Cells(11).Value)
            sumrms += (DataGridView1.Rows(j).Cells(4).Value * (DataGridView1.Rows(j).Cells(3).Value - DataGridView1.Rows(j).Cells(11).Value)) * (DataGridView1.Rows(j).Cells(6).Value / 100)

            If DataGridView1.Rows(j).Cells(9).Value = 0 Then
                sum0 += DataGridView1.Rows(j).Cells(8).Value
                ht += DataGridView1.Rows(j).Cells(7).Value
                tva = 0

            End If

            If DataGridView1.Rows(j).Cells(9).Value = 7 Then
                sum7 += DataGridView1.Rows(j).Cells(8).Value
                ht7 += DataGridView1.Rows(j).Cells(7).Value
                tva7 = (ht7 * 7) / 100
            End If

            If DataGridView1.Rows(j).Cells(9).Value = 10 Then
                sum10 += DataGridView1.Rows(j).Cells(8).Value
                ht10 += DataGridView1.Rows(j).Cells(7).Value
                tva10 = (ht10 * 10) / 100
            End If

            If DataGridView1.Rows(j).Cells(9).Value = 14 Then
                sum14 += DataGridView1.Rows(j).Cells(8).Value
                ht14 += DataGridView1.Rows(j).Cells(7).Value
                tva14 = (ht14 * 14) / 100
            End If
            If DataGridView1.Rows(j).Cells(9).Value = 20 Then
                sum20 += DataGridView1.Rows(j).Cells(8).Value
                ht20 += DataGridView1.Rows(j).Cells(7).Value
                tva20 = (ht20 * 20) / 100
            End If

        Next

        DataGridView2.Rows(0).Cells(1).Value = Convert.ToDouble(ht.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(0).Cells(2).Value = Convert.ToDouble(tva.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(0).Cells(3).Value = Convert.ToDouble(sum0.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(1).Cells(1).Value = Convert.ToDouble(ht7.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(1).Cells(2).Value = Convert.ToDouble(tva7.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(1).Cells(3).Value = Convert.ToDouble(sum7.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(2).Cells(1).Value = Convert.ToDouble(ht10.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(2).Cells(2).Value = Convert.ToDouble(tva10.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(2).Cells(3).Value = Convert.ToDouble(sum10.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(3).Cells(1).Value = Convert.ToDouble(ht14.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(3).Cells(2).Value = Convert.ToDouble(tva14.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(3).Cells(3).Value = Convert.ToDouble(sum14.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(4).Cells(1).Value = Convert.ToDouble(ht20.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(4).Cells(2).Value = Convert.ToDouble(tva20.ToString.Replace(".", ",")).ToString("# #00.00")
        DataGridView2.Rows(4).Cells(3).Value = Convert.ToDouble(sum20.ToString.Replace(".", ",")).ToString("# #00.00")

        Label15.Text = Convert.ToDouble(sumttc.ToString.Replace(".", ",") + Convert.ToDouble(Label34.Text)).ToString("# #00.00")
        Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("# #00.00")
        Label27.Text = Convert.ToDouble(sumrms.ToString.Replace(".", ",")).ToString("# #00.00")
        Label10.Text = Convert.ToDouble(sumttc - (sumht - sumrms)).ToString("# #00.00")
        Label39.Text = Convert.ToDouble(Label15.Text) - Convert.ToDouble(Label38.Text)

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Text = "Crédit" Then
            CheckBox1.Checked = False
            TextBox8.Visible = True
            Label28.Visible = True
            ComboBox4.Visible = False
            Label29.Visible = False
            TextBox8.Text = 0
        Else
            CheckBox1.Checked = True
            TextBox8.Visible = False
            Label28.Visible = False
            TextBox8.Text = Label15.Text
            ComboBox4.Visible = False
            Label29.Visible = False
        End If

        If ComboBox3.Text = "Chèque" Then
            Label13.Visible = True
            Label14.Visible = True
            Label18.Visible = True
            Label22.Visible = True
            Label23.Visible = True
            Label24.Visible = False
            Label25.Visible = False
            TextBox5.Visible = True
            TextBox6.Visible = False
            TextBox7.Visible = False
            TextBox2.Visible = True
            TextBox3.Visible = True
            TextBox4.Visible = True
            DateTimePicker1.Visible = True
            ComboBox4.Visible = True
            Label29.Visible = True
        ElseIf ComboBox3.Text = "TPE" Then
            ComboBox4.Visible = True
            Label29.Visible = True
            Label13.Visible = False
            Label14.Visible = False
            Label18.Visible = False
            Label22.Visible = False
            Label23.Visible = False
            Label24.Visible = True
            Label25.Visible = True
            TextBox5.Visible = False
            TextBox6.Visible = True
            TextBox7.Visible = True
            TextBox2.Visible = False
            TextBox3.Visible = False
            TextBox4.Visible = False
            DateTimePicker1.Visible = False
        Else
            ComboBox4.Visible = False
            Label29.Visible = False
            Label13.Visible = False
            Label14.Visible = False
            Label18.Visible = False
            Label22.Visible = False
            Label23.Visible = False
            Label24.Visible = False
            Label25.Visible = False
            TextBox5.Visible = False
            TextBox6.Visible = False
            TextBox7.Visible = False
            TextBox2.Visible = False
            TextBox3.Visible = False
            TextBox4.Visible = False
            DateTimePicker1.Visible = False
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub
    Private supprimees As New List(Of DataGridViewRow)
    Private indicesSupprimes As New List(Of Integer)
    Private donneesColonne0Supprimes As New List(Of String)
    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            supprimees.Add(DataGridView1.CurrentRow)
            indicesSupprimes.Add(DataGridView1.CurrentRow.Index)
            donneesColonne0Supprimes.Add(DataGridView1.CurrentRow.Cells(0).Value.ToString())
            DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index)
            DataGridView1.Select()
            Dim sumttc As Double = 0
            Dim sumht As Double = 0
            Dim sumrms As Double = 0


            Dim ht, ht7, ht10, ht14, ht20 As Double
            Dim tva, tva7, tva10, tva14, tva20 As Double
            Dim sum0 As Double
            Dim sum7 As Double
            Dim sum10 As Double
            Dim sum14 As Double
            Dim sum20 As Double

            For j = 0 To DataGridView1.Rows.Count() - 1

                sumttc += DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                sumht += DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                sumrms += (DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * (DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - DataGridView1.Rows(j).Cells(11).Value.ToString.Replace(" ", "").Replace(".", ","))) * (DataGridView1.Rows(j).Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") / 100)

                If DataGridView1.Rows(j).Cells(9).Value = 0 Then
                    sum0 += DataGridView1.Rows(j).Cells(8).Value
                    ht += DataGridView1.Rows(j).Cells(7).Value
                    tva = 0

                End If

                If DataGridView1.Rows(j).Cells(9).Value = 7 Then
                    sum7 += DataGridView1.Rows(j).Cells(8).Value
                    ht7 += DataGridView1.Rows(j).Cells(7).Value
                    tva7 = (ht7 * 7) / 100
                End If

                If DataGridView1.Rows(j).Cells(9).Value = 10 Then
                    sum10 += DataGridView1.Rows(j).Cells(8).Value
                    ht10 += DataGridView1.Rows(j).Cells(7).Value
                    tva10 = (ht10 * 10) / 100
                End If

                If DataGridView1.Rows(j).Cells(9).Value = 14 Then
                    sum14 += DataGridView1.Rows(j).Cells(8).Value
                    ht14 += DataGridView1.Rows(j).Cells(7).Value
                    tva14 = (ht14 * 14) / 100
                End If
                If DataGridView1.Rows(j).Cells(9).Value = 20 Then
                    sum20 += DataGridView1.Rows(j).Cells(8).Value
                    ht20 += DataGridView1.Rows(j).Cells(7).Value
                    tva20 = (ht20 * 20) / 100
                End If

            Next

            DataGridView2.Rows(0).Cells(1).Value = Convert.ToDouble(ht.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(0).Cells(2).Value = Convert.ToDouble(tva.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(0).Cells(3).Value = Convert.ToDouble(sum0.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(1).Cells(1).Value = Convert.ToDouble(ht7.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(1).Cells(2).Value = Convert.ToDouble(tva7.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(1).Cells(3).Value = Convert.ToDouble(sum7.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(2).Cells(1).Value = Convert.ToDouble(ht10.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(2).Cells(2).Value = Convert.ToDouble(tva10.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(2).Cells(3).Value = Convert.ToDouble(sum10.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(3).Cells(1).Value = Convert.ToDouble(ht14.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(3).Cells(2).Value = Convert.ToDouble(tva14.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(3).Cells(3).Value = Convert.ToDouble(sum14.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(4).Cells(1).Value = Convert.ToDouble(ht20.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(4).Cells(2).Value = Convert.ToDouble(tva20.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(4).Cells(3).Value = Convert.ToDouble(sum20.ToString.Replace(".", ",")).ToString("# #00.00")

            Label15.Text = Convert.ToDouble(sumttc.ToString.Replace(".", ",") + Convert.ToDouble(Label34.Text)).ToString("# #00.00")
            Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("# #00.00")
            Label27.Text = Convert.ToDouble(sumrms.ToString.Replace(".", ",")).ToString("# #00.00")
            Label10.Text = Convert.ToDouble(sumttc - (sumht - sumrms)).ToString("# #00.00")
            Label39.Text = Convert.ToDouble(Label15.Text) - Convert.ToDouble(Label38.Text)

        Else
            MessageBox.Show("Veuillez choisir un article !")
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    <Obsolete>
    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Dim ds As New DataSet1
        Dim dt3 As New DataTable
        dt3 = ds.Tables("releve")
        For i = 0 To archive.DataGridView1.SelectedRows.Count - 1
            adpt = New MySqlDataAdapter("select id from bondelivr where order_id = '" & archive.DataGridView1.SelectedRows(i).Cells(9).Value.ToString & "'", conn2)
            Dim table4 As New DataTable
            adpt.Fill(table4)
            If table4.Rows.Count <> 0 Then
                dt3.Rows.Add("BL N° " & table4.Rows(0).Item(0) & " (Vente N° " & archive.DataGridView1.SelectedRows(i).Cells(9).Value.ToString & " )", archive.DataGridView1.SelectedRows(i).Cells(1).Value.ToString, archive.DataGridView1.SelectedRows(i).Cells(5).Value.ToString)
            Else
                dt3.Rows.Add("Vente N° " & archive.DataGridView1.SelectedRows(i).Cells(9).Value.ToString, archive.DataGridView1.SelectedRows(i).Cells(1).Value.ToString, archive.DataGridView1.SelectedRows(i).Cells(5).Value.ToString)
            End If
        Next
        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim table3 As New DataTable
        adpt.Fill(table3)
        Dim appPath As String = Application.StartupPath()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\Report9.rdlc"
        ReportToPrint.DataSources.Clear()
        ReportToPrint.EnableExternalImages = True

        Dim num As New ReportParameter("num", Label11.Text.ToString)
        Dim num1(0) As ReportParameter
        num1(0) = num
        ReportToPrint.SetParameters(num1)

        Dim typo As New ReportParameter("type", "Facture")
        Dim typo1(0) As ReportParameter
        typo1(0) = typo
        ReportToPrint.SetParameters(typo1)

        Dim dateo As New ReportParameter("date", Label6.Text)
        Dim dato1(0) As ReportParameter
        dato1(0) = dateo
        ReportToPrint.SetParameters(dato1)

        Dim ttc As New ReportParameter("ttc", Label15.Text)
        Dim ttc1(0) As ReportParameter
        ttc1(0) = ttc
        ReportToPrint.SetParameters(ttc1)

        Dim neto As Double = Convert.ToDouble(Label9.Text) - Convert.ToDouble(Label27.Text)


        Dim net As New ReportParameter("net", neto.ToString)
        Dim net1(0) As ReportParameter
        net1(0) = net
        ReportToPrint.SetParameters(net1)

        Dim imgo As New ReportParameter("img", "File:\" + SavePath, True)
        Dim imgo1(0) As ReportParameter
        imgo1(0) = imgo
        ReportToPrint.SetParameters(imgo1)


        Dim detailso As New ReportParameter("infos", table3.Rows(0).Item(1).ToString + " | " + "Tél: " + table3.Rows(0).Item(2).ToString + " | " + "Email: " + table3.Rows(0).Item(3).ToString + " | " + "ICE: " + table3.Rows(0).Item(4).ToString + " | " + "IF: " + table3.Rows(0).Item(5).ToString + " | " + "TP: " + table3.Rows(0).Item(7).ToString + " | " + "RC: " + table3.Rows(0).Item(8).ToString)
        Dim detailso1(0) As ReportParameter
        detailso1(0) = detailso
        ReportToPrint.SetParameters(detailso1)
        ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt3))
        ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.

        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.04985in</PageWidth><PageHeight>12in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
#Disable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.
        ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
#Enable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next

        Dim printDoc As New PrintDocument()
        'Microsoft Print To PDF
        'C80250 Series
        'C80250 Series init
        Dim printerticket As String = System.Configuration.ConfigurationSettings.AppSettings("printerticket")
        Dim printera5 As String = System.Configuration.ConfigurationSettings.AppSettings("printera5")
        Dim printera4 As String = System.Configuration.ConfigurationSettings.AppSettings("printera4")

        receiptprinter = printerticket
        a4printer = printera4
        a5printer = printera5
        printDoc.PrinterSettings.PrinterName = a4printer
        Dim ps As New PrinterSettings()
        ps.PrinterName = printDoc.PrinterSettings.PrinterName
        printDoc.PrinterSettings = ps

        AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
        m_currentPageIndex = 0


        printDoc.Print()
        tbl.Rows.Clear()
        tbl2.Rows.Clear()
        Me.Close()

    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Dim Rep As Integer = MsgBox("Voulez-vous vraiment modifier cette facture ?", vbYesNo)
        If Rep = vbYes Then
            If TextBox1.Text = Label11.Text Then

                Dim sql2 As String
                Dim cmd2 As MySqlCommand
                Dim ticid As String = Label11.Text
                conn2.Close()
                conn2.Open()
                sql2 = "UPDATE `facture` SET `OrderDate`='" & DateTimePicker2.Value.Date.ToString("yyyy-MM-dd") & "',`remisepour` ='" & TextBox9.Text.Replace(".", ",") & "',`remise` = '" & Label27.Text & "' ,`MontantOrder`= '" & Label15.Text & "',`reste`= '" & Label15.Text & "',`paye` = '" & Label38.Text & "', `reste` = '" & Label39.Text & "' WHERE `OrderID` = '" + ticid.ToString + "'"
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.ExecuteNonQuery()

                For i = 0 To DataGridView1.Rows.Count - 1
                    sql2 = "UPDATE `facturedetails` SET `pu_ht`='" & Convert.ToDecimal(DataGridView1.Rows(i).Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00") & "',`pu_ttc`='" & Convert.ToDecimal(DataGridView1.Rows(i).Cells(5).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00") & "',`remise`='" & Convert.ToDecimal(DataGridView1.Rows(i).Cells(6).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00") & "' WHERE fac_id = '" + ticid.ToString + "' and ref = '" & DataGridView1.Rows(i).Cells(0).Value & "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()
                Next
                If supprimees.Count <> 0 Then
                    For i = 0 To donneesColonne0Supprimes.Count - 1
                        sql2 = "DELETE FROM `facturedetails` WHERE `fac_id` = '" & ticid.ToString & "' and `ref` = '" & donneesColonne0Supprimes(i) & "'"
                        cmd2 = New MySqlCommand(sql2, conn2)
                        cmd2.ExecuteNonQuery()
                    Next

                End If


                sql2 = "UPDATE `clients` SET `remisepour` = @pourc, `remise`=@rms WHERE `client` = @id"
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.Parameters.Clear()
                cmd2.Parameters.AddWithValue("@id", Label7.Text)
                cmd2.Parameters.AddWithValue("@pourc", 0)
                cmd2.Parameters.AddWithValue("@rms", 0)

                cmd2.ExecuteNonQuery()


                sql2 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.Parameters.Clear()
                If role = "Caissier" Then
                    cmd2.Parameters.AddWithValue("@name", dashboard.Label2.Text)
                Else
                    cmd2.Parameters.AddWithValue("@name", user)
                End If
                cmd2.Parameters.AddWithValue("@op", "Modification de la facture N° " & ticid)
                cmd2.ExecuteNonQuery()

                conn2.Close()
                MsgBox("Facture Bien modifiée")
                Me.Close()
            Else
                adpt = New MySqlDataAdapter("SELECT * from facture WHERE `OrderID` = '" + TextBox1.Text + "' ", conn2)
                Dim tabletick As New DataTable
                adpt.Fill(tabletick)
                If tabletick.Rows.Count = 0 Then
                    Dim sql2 As String
                    Dim cmd2 As MySqlCommand
                    Dim ticid As String = Label11.Text
                    conn2.Close()
                    conn2.Open()
                    sql2 = "UPDATE `facture` SET `OrderID` = '" + TextBox1.Text + "', `OrderDate`='" & DateTimePicker2.Value.Date.ToString("yyyy-MM-dd") & "',`remisepour` ='" & TextBox9.Text.Replace(".", ",") & "',`remise` = '" & Label27.Text & "' ,`MontantOrder`= '" & Label15.Text & "',`reste`= '" & Label15.Text & "',`paye` = '" & Label38.Text & "', `reste` = '" & Label39.Text & "' WHERE `OrderID` = '" + ticid.ToString + "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    If supprimees.Count <> 0 Then
                        For i = 0 To donneesColonne0Supprimes.Count - 1
                            sql2 = "DELETE FROM `facturedetails` WHERE `fac_id` = '" & ticid.ToString & "' and `ref` = '" & donneesColonne0Supprimes(i) & "'"
                            cmd2 = New MySqlCommand(sql2, conn2)
                            cmd2.ExecuteNonQuery()
                        Next

                    End If

                    For i = 0 To DataGridView1.Rows.Count - 1
                        sql2 = "UPDATE `facturedetails` SET `fac_id` = '" + TextBox1.Text + "', `pu_ht`='" & Convert.ToDecimal(DataGridView1.Rows(i).Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00") & "',`pu_ttc`='" & Convert.ToDecimal(DataGridView1.Rows(i).Cells(5).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00") & "',`remise`='" & Convert.ToDecimal(DataGridView1.Rows(i).Cells(6).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00") & "' WHERE fac_id = '" + ticid.ToString + "' and ref = '" & DataGridView1.Rows(i).Cells(0).Value & "'"
                        cmd2 = New MySqlCommand(sql2, conn2)
                        cmd2.ExecuteNonQuery()
                    Next

                    sql2 = "UPDATE `orders` SET `fac_id` = '" + TextBox1.Text + "' WHERE `fac_id` = '" + ticid.ToString + "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    sql2 = "UPDATE `clients` SET `remisepour` = @pourc, `remise`=@rms WHERE `client` = @id"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.Parameters.Clear()
                    cmd2.Parameters.AddWithValue("@id", Label7.Text)
                    cmd2.Parameters.AddWithValue("@pourc", 0)
                    cmd2.Parameters.AddWithValue("@rms", 0)

                    cmd2.ExecuteNonQuery()

                    conn2.Close()
                    MsgBox("Facture Bien modifiée")
                    Me.Close()
                Else
                    MsgBox("Le numéro de facture que vous avez saisi existe déjà !")
                End If

            End If

        End If
    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        If supprimees.Count > 0 Then
            Dim lastRemovedRow As DataGridViewRow = supprimees(supprimees.Count - 1)
            Dim originalIndex As Integer = indicesSupprimes(indicesSupprimes.Count - 1)
            DataGridView1.Rows.Insert(originalIndex, lastRemovedRow)
            supprimees.RemoveAt(supprimees.Count - 1)
            indicesSupprimes.RemoveAt(indicesSupprimes.Count - 1)
            donneesColonne0Supprimes.RemoveAt(donneesColonne0Supprimes.Count - 1)

            Dim sumttc As Double = 0
            Dim sumht As Double = 0
            Dim sumrms As Double = 0


            Dim ht, ht7, ht10, ht14, ht20 As Double
            Dim tva, tva7, tva10, tva14, tva20 As Double
            Dim sum0 As Double
            Dim sum7 As Double
            Dim sum10 As Double
            Dim sum14 As Double
            Dim sum20 As Double

            For j = 0 To DataGridView1.Rows.Count() - 1

                sumttc += DataGridView1.Rows(j).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                sumht += DataGridView1.Rows(j).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                sumrms += (DataGridView1.Rows(j).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * (DataGridView1.Rows(j).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - DataGridView1.Rows(j).Cells(11).Value.ToString.Replace(" ", "").Replace(".", ","))) * (DataGridView1.Rows(j).Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") / 100)

                If DataGridView1.Rows(j).Cells(9).Value = 0 Then
                    sum0 += DataGridView1.Rows(j).Cells(8).Value
                    ht += DataGridView1.Rows(j).Cells(7).Value
                    tva = 0

                End If

                If DataGridView1.Rows(j).Cells(9).Value = 7 Then
                    sum7 += DataGridView1.Rows(j).Cells(8).Value
                    ht7 += DataGridView1.Rows(j).Cells(7).Value
                    tva7 = (ht7 * 7) / 100
                End If

                If DataGridView1.Rows(j).Cells(9).Value = 10 Then
                    sum10 += DataGridView1.Rows(j).Cells(8).Value
                    ht10 += DataGridView1.Rows(j).Cells(7).Value
                    tva10 = (ht10 * 10) / 100
                End If

                If DataGridView1.Rows(j).Cells(9).Value = 14 Then
                    sum14 += DataGridView1.Rows(j).Cells(8).Value
                    ht14 += DataGridView1.Rows(j).Cells(7).Value
                    tva14 = (ht14 * 14) / 100
                End If
                If DataGridView1.Rows(j).Cells(9).Value = 20 Then
                    sum20 += DataGridView1.Rows(j).Cells(8).Value
                    ht20 += DataGridView1.Rows(j).Cells(7).Value
                    tva20 = (ht20 * 20) / 100
                End If

            Next

            DataGridView2.Rows(0).Cells(1).Value = Convert.ToDouble(ht.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(0).Cells(2).Value = Convert.ToDouble(tva.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(0).Cells(3).Value = Convert.ToDouble(sum0.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(1).Cells(1).Value = Convert.ToDouble(ht7.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(1).Cells(2).Value = Convert.ToDouble(tva7.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(1).Cells(3).Value = Convert.ToDouble(sum7.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(2).Cells(1).Value = Convert.ToDouble(ht10.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(2).Cells(2).Value = Convert.ToDouble(tva10.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(2).Cells(3).Value = Convert.ToDouble(sum10.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(3).Cells(1).Value = Convert.ToDouble(ht14.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(3).Cells(2).Value = Convert.ToDouble(tva14.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(3).Cells(3).Value = Convert.ToDouble(sum14.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(4).Cells(1).Value = Convert.ToDouble(ht20.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(4).Cells(2).Value = Convert.ToDouble(tva20.ToString.Replace(".", ",")).ToString("# #00.00")
            DataGridView2.Rows(4).Cells(3).Value = Convert.ToDouble(sum20.ToString.Replace(".", ",")).ToString("# #00.00")

            Label15.Text = Convert.ToDouble(sumttc.ToString.Replace(".", ",") + Convert.ToDouble(Label34.Text)).ToString("# #00.00")
            Label9.Text = Convert.ToDouble(sumht.ToString.Replace(".", ",")).ToString("# #00.00")
            Label27.Text = Convert.ToDouble(sumrms.ToString.Replace(".", ",")).ToString("# #00.00")
            Label10.Text = Convert.ToDouble(sumttc - (sumht - sumrms)).ToString("# #00.00")
            Label39.Text = Convert.ToDouble(Label15.Text) - Convert.ToDouble(Label38.Text)

        End If
        DataGridView1.Select()
    End Sub
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Z Then
            IconButton7_Click(sender, e)
            e.Handled = True
        End If
    End Sub

    Private Sub IconButton8_Click(sender As Object, e As EventArgs) Handles IconButton8.Click
        If DataGridView1.Rows.Count <= 0 Then
            MsgBox(“Ticket Vide ! ”)
        Else


            Dim cmd As MySqlCommand
            Dim cmd2, cmd3 As MySqlCommand
            Dim sql As String
            Dim sql2, sql3 As String
            Dim execution As Integer
            adpt = New MySqlDataAdapter("select OrderID from facture order by OrderID desc", conn2)
            Dim table4 As New DataTable
            adpt.Fill(table4)
            Dim id As Integer
            If table4.Rows.Count <> 0 Then
                If Convert.ToDouble(Year(Now) & "0001") > (Val(table4.Rows(0).Item(0)) + 1) Then
                    id = Year(Now) & "0001"
                Else

                    id = Val(table4.Rows(0).Item(0)) + 1
                End If
            Else
                id = Year(Now) & "0001"
            End If
            Dim etat As String
            Dim mode As String
            Dim reste As Double
            Dim paye As Double
            If Label32.Text = "Non Payée" Then
                reste = Convert.ToDouble(Label15.Text).ToString("#0.00").Replace(".", ",")
                paye = 0.00
            Else
                reste = 0.00
                paye = Convert.ToDouble(Label15.Text).ToString("#0.00").Replace(".", ",")
            End If

            If Label33.Text > 0 Then
                etat = "Non Payé"
                mode = "Crédit"
            Else
                conn2.Close()
                conn2.Open()
                etat = "Payé"
                mode = Label31.Text


            End If
            conn2.Close()
            conn2.Open()
            For Each row As DataGridViewRow In archive.DataGridView1.Rows
                ' Vérifier si la case à cocher de la ligne est cochée
                If Convert.ToBoolean(row.Cells("CheckBoxColumn").Value) = True Then
                    Dim ticid As String = row.Cells(10).Value.ToString
                    sql2 = "UPDATE `orders` SET `etat`='fac',`fac_id` = '" & id & "' WHERE `OrderID` = '" + ticid.ToString + "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@name", archive.Label2.Text)
                    cmd3.Parameters.AddWithValue("@op", "Transformation de la Vente N° " & ticid & " en Facture N° " & id.ToString)
                    cmd3.ExecuteNonQuery()

                    sql2 = "UPDATE `cheques` SET `fac`= '" & id & "' WHERE `tick` = '" + ticid.ToString + "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                End If
            Next



            conn2.Close()
            Dim infs As String = "Le " & DateTime.Now.ToString("yyyy-MM-dd HH:mm") & " par " & Label1.Text
            conn2.Open()

            sql2 = "INSERT INTO facture (`OrderID`, `OrderDate`, `MontantOrder`,`etat`,`client`,`infos`,`paye`,`reste`,`modePayement`,`trsprt`,`remisepour`,`remise`) 
                    VALUES ('" & id & "', '" & Convert.ToDateTime(Label6.Text).ToString("yyyy-MM-dd HH:mm:ss") & "','" + Label15.Text + "','" + etat + "','" + Label7.Text + "','" & infs & "','" & paye & "','" & reste & "','" & mode & "','" & Label34.Text.Replace(".", ",") & "','" & TextBox9.Text & "','" & Label27.Text & "')"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.Clear()
            cmd2.ExecuteNonQuery()
            For Each dr As DataGridViewRow In Me.DataGridView1.Rows
                conn2.Close()
                conn2.Open()
                Try
                    cmd = New MySqlCommand
                    sql2 = "UPDATE `article` SET `facture`= REPLACE(`facture`,',','.') - '" & dr.Cells(3).Value.ToString.Replace(",", ".").Replace(" ", "") & "' '" & id & "' WHERE `Code` = '" + dr.Cells(0).Value + "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()
                    cmd = New MySqlCommand

                    sql = “INSERT INTO facturedetails (`fac_id`, `ref`, `des`, `unite`, `qte`, `pu_ht`, `pu_ttc`, `remise`, `tot_ht`, `tot_ttc`, `tva`,`marge`,`gr`) VALUES (@facid, @ref,@des,@unite,@qte,@puht,@puttc,@remise,@totht,@totttc,@tva,@marge,@gr);”
                    prod = dr.Cells(5).Value
                    With cmd
                        .Connection = conn2
                        .CommandText = sql
                        .Parameters.Clear()
                        .Parameters.AddWithValue(“@facid”, id)
                        .Parameters.AddWithValue(“@ref”, dr.Cells(0).Value)
                        .Parameters.AddWithValue(“@des”, dr.Cells(1).Value)
                        .Parameters.AddWithValue(“@unite”, "U")
                        .Parameters.AddWithValue(“@qte”, dr.Cells(3).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@puht”, dr.Cells(4).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@puttc”, dr.Cells(5).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@remise”, dr.Cells(6).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@totht”, dr.Cells(7).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@totttc”, dr.Cells(8).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@tva”, dr.Cells(9).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@marge”, dr.Cells(10).Value.ToString.Replace(".", ","))
                        .Parameters.AddWithValue(“@gr”, dr.Cells(11).Value.ToString.Replace(".", ","))
                        execution = .ExecuteNonQuery()
                    End With

                    conn2.Close()
                Catch ex As MySqlException
                    MsgBox(ex.Message)
                End Try
            Next

            conn2.Close()

            MsgBox("Facture bien enregistrée !")
            Me.Close()
            ventedetail.Close()
            conn2.Open()



            sql3 = "UPDATE `clients` SET `remisepour` = @pourc, `remise`=@rms WHERE `client` = @id"
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            cmd3.Parameters.AddWithValue("@id", Label7.Text)
            cmd3.Parameters.AddWithValue("@pourc", 0)
            cmd3.Parameters.AddWithValue("@rms", 0)

            cmd3.ExecuteNonQuery()

            conn2.Close()



        End If

    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        If TextBox9.Text <> "" And IsNumeric(TextBox9.Text) Then
            If TextBox9.Text > 5 Then
                MsgBox("La remise ne peut pas dépasser 5% !")
            Else

                Label27.Text = (Convert.ToDecimal(Label9.Text.Replace(".", ",").Replace(" ", "")) * (Convert.ToDecimal(TextBox9.Text.Replace(" ", "").Replace(".", ",")) / 100)).ToString("# ##0.00")
                Label15.Text = Label9.Text - Label27.Text + Label10.Text + Label34.Text
                Label39.Text = Convert.ToDouble(Label15.Text) - Convert.ToDouble(Label38.Text)

            End If
        Else
            MsgBox("Entrer un nombre valide !")
            TextBox9.Text = ""
            TextBox9.Select()
        End If
    End Sub
End Class