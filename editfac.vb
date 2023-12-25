Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient

Imports System.Runtime.InteropServices
Public Class editfac
    Dim adpt As MySqlDataAdapter

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
    Private Sub editfac_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim tablebq As New DataTable
        adpt.Fill(tablebq)

        For i = 0 To tablebq.Rows.Count - 1
            ComboBox4.Items.Add(tablebq.Rows(i).Item(1))
        Next

    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Me.Close()

    End Sub


    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

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
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        If Label39.Text = "Devis" Then
            If DataGridView3.Rows.Count <= 0 Then
                MsgBox(“Ticket Vide ! ”)
            Else

                adpt = New MySqlDataAdapter("select OrderID from devis order by OrderID desc", conn2)
                Dim table4 As New DataTable
                adpt.Fill(table4)
                Dim id As Integer
                If table4.Rows.Count <> 0 Then
                    id = Val(table4.Rows(0).Item(0)) + 1
                Else
                    id = Year(Now).ToString & "0001"
                End If
                Dim etat As String
                If Label24.Text = "Facture payée" = True Then
                    etat = "Payé"
                Else
                    etat = "Non Payé"
                End If

                Dim ds As New DataSet1
                Dim dt As New DataTable
                dt = ds.Tables("tick")
                For i = 0 To DataGridView3.Rows.Count - 1
                    dt.Rows.Add(DataGridView3.Rows(i).Cells(0).Value, DataGridView3.Rows(i).Cells(1).Value, DataGridView3.Rows(i).Cells(2).Value, DataGridView3.Rows(i).Cells(3).Value, DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(5).Value, DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", ""), DataGridView3.Rows(i).Cells(8).Value, DataGridView3.Rows(i).Cells(9).Value)
                Next

                Dim ds2 As New DataSet2
                Dim dt2 As New DataTable
                dt2 = ds2.Tables("tva")
                For i = 0 To DataGridView2.Rows.Count - 1
                    dt2.Rows.Add(DataGridView2.Rows(i).Cells(0).Value, DataGridView2.Rows(i).Cells(1).Value, DataGridView2.Rows(i).Cells(2).Value, DataGridView2.Rows(i).Cells(3).Value)
                Next

                ReportToPrint = New LocalReport()
                ReportToPrint.ReportPath = Application.StartupPath + "\Report2.rdlc"
                ReportToPrint.DataSources.Clear()
                ReportToPrint.EnableExternalImages = True
                Dim cliente As New ReportParameter("client", Label7.Text)
                Dim client1(0) As ReportParameter
                client1(0) = cliente
                ReportToPrint.SetParameters(client1)

                Dim fac As New ReportParameter("fac", "Devis N° " & id.ToString)
                Dim fac1(0) As ReportParameter
                fac1(0) = fac
                ReportToPrint.SetParameters(fac1)

                Dim remarque As New ReportParameter("remarque", "-")
                Dim remarque1(0) As ReportParameter
                remarque1(0) = remarque
                ReportToPrint.SetParameters(remarque1)

                adpt = New MySqlDataAdapter("select * from infos", conn2)
                Dim table3 As New DataTable
                adpt.Fill(table3)

                Dim datee As New ReportParameter("date", Label6.Text)
                Dim date1(0) As ReportParameter
                date1(0) = datee
                ReportToPrint.SetParameters(date1)

                Dim tva As New ReportParameter("tva", Convert.ToDouble(Label10.Text).ToString("N2"))
                Dim tva1(0) As ReportParameter
                tva1(0) = tva
                ReportToPrint.SetParameters(tva1)

                Dim ttc As New ReportParameter("ttc", Convert.ToDouble(Label15.Text).ToString("N2"))
                Dim ttc1(0) As ReportParameter
                ttc1(0) = ttc
                ReportToPrint.SetParameters(ttc1)

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
                ReportToPrint.SetParameters(lettre1)

                If Label28.Text = "0,00" Or Convert.ToDouble(Label28.Text) = 0 Then
                    Dim rmspr As New ReportParameter("remiso", "")
                    Dim rmspr1(0) As ReportParameter
                    rmspr1(0) = rmspr
                    formata4.ReportViewer1.LocalReport.SetParameters(rmspr1)
                Else
                    If Label43.Text = "0,00" Or Convert.ToDouble(Label43.Text) = 0 = 0 Then
                        Dim rmspr As New ReportParameter("remiso", "Remise : " & Convert.ToDouble(Label28.Text).ToString("N2") & " DHs")
                        Dim rmspr1(0) As ReportParameter
                        rmspr1(0) = rmspr
                        formata4.ReportViewer1.LocalReport.SetParameters(rmspr1)
                    Else

                        Dim rmspr As New ReportParameter("remiso", "Remise (" & Label43.Text & "%) : " & Convert.ToDouble(Label28.Text).ToString("N2") & " DHs")
                        Dim rmspr1(0) As ReportParameter
                        rmspr1(0) = rmspr
                        formata4.ReportViewer1.LocalReport.SetParameters(rmspr1)
                    End If

                End If

                Dim neto As Double = Convert.ToDouble(Label9.Text) - Convert.ToDouble(Label28.Text)


                Dim net As New ReportParameter("net", Convert.ToDouble(neto).ToString("N2"))
                Dim net1(0) As ReportParameter
                net1(0) = net
                ReportToPrint.SetParameters(net1)

                Dim ice As New ReportParameter("ice", Label2.Text)
                Dim ice1(0) As ReportParameter
                ice1(0) = ice
                ReportToPrint.SetParameters(ice1)

                Dim type As New ReportParameter("type", "Devis")
                Dim type1(0) As ReportParameter
                type1(0) = type
                ReportToPrint.SetParameters(type1)

                Dim appPath As String = Application.StartupPath()

                Dim SaveDirectory As String = appPath & "\"
                Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                Dim img As New ReportParameter("img", "File:\" + SavePath, True)
                Dim img1(0) As ReportParameter
                img1(0) = img
                ReportToPrint.SetParameters(img1)

                Dim details As New ReportParameter("infos", table3.Rows(0).Item(1).ToString + " | " + "Tél: " + table3.Rows(0).Item(2).ToString + " | " + "Email: " + table3.Rows(0).Item(3).ToString + " | " + "ICE: " + table3.Rows(0).Item(4).ToString + " | " + "IF: " + table3.Rows(0).Item(5).ToString + " | " + "TP: " + table3.Rows(0).Item(7).ToString + " | " + "RC: " + table3.Rows(0).Item(8).ToString)
                Dim details1(0) As ReportParameter
                details1(0) = details
                ReportToPrint.SetParameters(details1)



                'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
                ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dt2))

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
                Dim printDialog As New PrintDialog()

                ' Configurer les propriétés de la boîte de dialogue d'impression si nécessaire
                ' printDialog.PrinterSettings = New PrinterSettings()

                ' Afficher la boîte de dialogue d'impression
                If printDialog.ShowDialog() = DialogResult.OK Then
                    ' Obtenir les paramètres d'impression sélectionnés par l'utilisateur
                    printDoc.Print()
                    ' Ici, vous pouvez exécuter le code d'impression en utilisant les paramètres d'impression
                    ' par exemple : PrintDocument1.PrinterSettings = printerSettings
                End If

                tbl.Rows.Clear()
                tbl2.Rows.Clear()
                Dim sql2, sql3 As String
                Dim cmd2, cmd3 As MySqlCommand
                conn2.Close()
                conn2.Open()
                sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.Parameters.AddWithValue("@name", factures.Label2.Text)
                cmd3.Parameters.AddWithValue("@op", "Impression d'une copie de Facture N° " & id.ToString)
                cmd3.ExecuteNonQuery()
                conn2.Close()


                'If ComboBox1.Text = "Espèce" Then
                '    Dim codeOpenCashDrawer As Byte() = New Byte() {27, 112, 48, 55, 121}
                '    Dim pUnmanagedBytes As IntPtr = New IntPtr(0)
                '    pUnmanagedBytes = Marshal.AllocCoTaskMem(5)
                '    Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5)
                '    RawPrinterHelper.SendBytesToPrinter("C80250 Series init", pUnmanagedBytes, 5)
                '    Marshal.FreeCoTaskMem(pUnmanagedBytes)
                'End If
                'MsgBox("facture enregistrée")


            End If
        Else

            If DataGridView1.Rows.Count <= 0 Then
            MsgBox(“Ticket Vide ! ”)
        Else

            adpt = New MySqlDataAdapter("select OrderID from facture order by OrderID desc", conn2)
            Dim table4 As New DataTable
            adpt.Fill(table4)
            Dim id As Integer
            If table4.Rows.Count <> 0 Then
                id = Val(table4.Rows(0).Item(0)) + 1
            Else
                id = Year(Now).ToString & "0001"
            End If
            Dim etat As String
            If Label24.Text = "Facture payée" = True Then
                etat = "Payé"
            Else
                etat = "Non Payé"
            End If

            Dim ds As New DataSet1
            Dim dt As New DataTable
            dt = ds.Tables("tick")
            For i = 0 To DataGridView1.Rows.Count - 1
                dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(8).Value, DataGridView1.Rows(i).Cells(9).Value)
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

                ReportToPrint = New LocalReport()
                formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\Report2.rdlc"
                formata4.ReportViewer1.LocalReport.DataSources.Clear()
                formata4.ReportViewer1.LocalReport.EnableExternalImages = True
                Dim cliente As New ReportParameter("client", Label7.Text)
            Dim client1(0) As ReportParameter
            client1(0) = cliente
                formata4.ReportViewer1.LocalReport.SetParameters(client1)

                If CheckBox2.Checked = False Then
                    Dim fac As New ReportParameter("fac", "Facture N° " & Label11.Text.ToString)
                    Dim fac1(0) As ReportParameter
                    fac1(0) = fac
                    formata4.ReportViewer1.LocalReport.SetParameters(fac1)
                Else
                    Dim fac As New ReportParameter("fac", "Facture N° " & Label11.Text.ToString & " (Duplicata)")
                    Dim fac1(0) As ReportParameter
                    fac1(0) = fac
                    formata4.ReportViewer1.LocalReport.SetParameters(fac1)
                End If
                Dim remarque As New ReportParameter("remarque", Label37.Text)
            Dim remarque1(0) As ReportParameter
            remarque1(0) = remarque
                formata4.ReportViewer1.LocalReport.SetParameters(remarque1)

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
                    convertednumbre = numberInWords
                Else
                    convertednumbre = "Entrez un nombre valide dans Label1"
                End If

                Dim lettre As New ReportParameter("lettre", convertednumbre)
                Dim lettre1(0) As ReportParameter
                lettre1(0) = lettre
                formata4.ReportViewer1.LocalReport.SetParameters(lettre1)



                Dim neto As Double = Convert.ToDouble(Label9.Text) - Convert.ToDouble(Label28.Text)


                Dim net As New ReportParameter("net", Convert.ToDouble(neto).ToString("N2"))
                Dim net1(0) As ReportParameter
                net1(0) = net
                formata4.ReportViewer1.LocalReport.SetParameters(net1)

                Dim ice As New ReportParameter("ice", Label2.Text)
            Dim ice1(0) As ReportParameter
            ice1(0) = ice
                formata4.ReportViewer1.LocalReport.SetParameters(ice1)

                Dim type As New ReportParameter("type", "Facture")
                Dim type1(0) As ReportParameter
                type1(0) = type
                formata4.ReportViewer1.LocalReport.SetParameters(type1)

                If Label28.Text = "0,00" Or Convert.ToDouble(Label28.Text) = 0 Then
                    Dim rmspr As New ReportParameter("remiso", "")
                    Dim rmspr1(0) As ReportParameter
                    rmspr1(0) = rmspr
                    formata4.ReportViewer1.LocalReport.SetParameters(rmspr1)
                Else
                    If Label43.Text = "0,00" Or Convert.ToDouble(Label43.Text) = 0 Then
                        Dim rmspr As New ReportParameter("remiso", "Remise : " & Convert.ToDouble(Label28.Text).ToString("N2") & " DHs")
                        Dim rmspr1(0) As ReportParameter
                        rmspr1(0) = rmspr
                        formata4.ReportViewer1.LocalReport.SetParameters(rmspr1)
                    Else

                        Dim rmspr As New ReportParameter("remiso", "Remise (" & Label43.Text & "%) : " & Convert.ToDouble(Label28.Text).ToString("N2") & " DHs")
                        Dim rmspr1(0) As ReportParameter
                        rmspr1(0) = rmspr
                        formata4.ReportViewer1.LocalReport.SetParameters(rmspr1)
                    End If

                End If

                Dim logistique As New ReportParameter("logistique", Label36.Text)
                Dim logistique1(0) As ReportParameter
                logistique1(0) = logistique
                formata4.ReportViewer1.LocalReport.SetParameters(logistique1)

                Dim appPath As String = Application.StartupPath()

                Dim SaveDirectory As String = appPath & "\"
                Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                Dim img As New ReportParameter("img", "File:\" + SavePath, True)
                Dim img1(0) As ReportParameter
                img1(0) = img
                formata4.ReportViewer1.LocalReport.SetParameters(img1)

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

                ' Générez les streams pour le rapport
                formata4.ReportViewer1.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)

                ' Ajoutez chaque stream généré à la collection m_streams
                For Each stream As Stream In m_streams
                    stream.Position = 0
                Next
                m_currentPageIndex = 0
                Dim printDoc As New PrintDocument()


                'Microsoft Print To PDF
                'C80250 Series
                'C80250 Series init
                printDoc.PrinterSettings.PrinterName = ComboBox1.Text
                Dim ps As New PrinterSettings()
                ps.PrinterName = printDoc.PrinterSettings.PrinterName
                printDoc.PrinterSettings = ps


                AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage

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
                formata4.ReportViewer1.Refresh()



                tbl.Rows.Clear()
            tbl2.Rows.Clear()
            Dim sql2, sql3 As String
            Dim cmd2, cmd3 As MySqlCommand
                conn2.Close()
                conn2.Open()
                sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            cmd3.Parameters.AddWithValue("@name", factures.Label2.Text)
            cmd3.Parameters.AddWithValue("@op", "Impression d'une copie de Facture N° " & id.ToString)
            cmd3.ExecuteNonQuery()
            conn2.Close()


            'If ComboBox1.Text = "Espèce" Then
            '    Dim codeOpenCashDrawer As Byte() = New Byte() {27, 112, 48, 55, 121}
            '    Dim pUnmanagedBytes As IntPtr = New IntPtr(0)
            '    pUnmanagedBytes = Marshal.AllocCoTaskMem(5)
            '    Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5)
            '    RawPrinterHelper.SendBytesToPrinter("C80250 Series init", pUnmanagedBytes, 5)
            '    Marshal.FreeCoTaskMem(pUnmanagedBytes)
            'End If
            'MsgBox("facture enregistrée")


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

    Private Sub IconButton2_Click(sender As Object, e As EventArgs)
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
                            .Parameters.AddWithValue(“@montant”, Math.Round(dr.Cells(7).Value, 2))
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
        If DataGridView1.CurrentCell.ColumnIndex = 6 Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim ptht As Double = Val(row.Cells.Item(3).Value) * Val(row.Cells.Item(4).Value)
                row.Cells.Item(7).Value = Math.Round(ptht - (ptht * (Val(row.Cells.Item(6).Value) / 100)), 2)
                row.Cells.Item(8).Value = Math.Round(Val(row.Cells.Item(7).Value) + ((Val(row.Cells.Item(7).Value) * Val(row.Cells.Item(9).Value)) / 100), 2)

                Dim totht As Double = row.Cells.Item(7).Value
                Dim totttc As Double = row.Cells.Item(8).Value
                sumttc += row.Cells.Item(8).Value
                sumht += row.Cells.Item(7).Value
                Dim ht As Double
                Dim tva As Double
                Dim sum7 As Double
                Dim sum10 As Double
                Dim sum14 As Double
                Dim sum20 As Double
                If row.Cells.Item(9).Value = 7 Then
                    sum7 += totttc
                    ht = sum7 / 1.07
                    tva = ht * 0.07
                    DataGridView2.Rows(0).Cells(1).Value = Math.Round(ht, 2)
                    DataGridView2.Rows(0).Cells(2).Value = Math.Round(tva, 2)
                    DataGridView2.Rows(0).Cells(3).Value = Math.Round(sum7, 2)
                End If
                If row.Cells.Item(9).Value = 10 Then
                    sum10 += totttc
                    ht = sum10 / 1.1
                    tva = ht * 0.1
                    DataGridView2.Rows(1).Cells(1).Value = Math.Round(ht, 2)
                    DataGridView2.Rows(1).Cells(2).Value = Math.Round(tva, 2)
                    DataGridView2.Rows(1).Cells(3).Value = Math.Round(sum10, 2)
                End If
                If row.Cells.Item(9).Value = 14 Then
                    sum14 += totttc
                    ht = sum14 / 1.14
                    tva = ht * 0.14
                    DataGridView2.Rows(2).Cells(1).Value = Math.Round(ht, 2)
                    DataGridView2.Rows(2).Cells(2).Value = Math.Round(tva, 2)
                    DataGridView2.Rows(2).Cells(3).Value = Math.Round(sum14, 2)
                End If
                If row.Cells.Item(9).Value = 20 Then
                    sum20 += totttc
                    ht = sum20 / 1.2
                    tva = ht * 0.2
                    DataGridView2.Rows(3).Cells(1).Value = Math.Round(ht, 2)
                    DataGridView2.Rows(3).Cells(2).Value = Math.Round(tva, 2)
                    DataGridView2.Rows(3).Cells(3).Value = Math.Round(sum20, 2)
                End If
            Next

            Label15.Text = Math.Round(sumttc, 2)
            Label9.Text = Math.Round(sumht, 2)
            Label10.Text = Math.Round(sumttc - sumht, 2)

        End If

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Text = "Chèque" Then
            Label33.Visible = True
            Label32.Visible = True
            Label31.Visible = True
            Label22.Visible = True
            Label23.Visible = True
            TextBox8.Visible = True
            TextBox2.Visible = True
            TextBox3.Visible = True
            TextBox4.Visible = True
            DateTimePicker1.Visible = True
            Label18.Visible = False
            Label14.Visible = False
            TextBox7.Visible = False
            TextBox6.Visible = False
            ComboBox4.Visible = True
            Label35.Visible = True
            Label13.Visible = False
            TextBox5.Visible = False
            IconButton2.Visible = False

        ElseIf ComboBox3.Text = "TPE" Then
            Label33.Visible = False
            Label32.Visible = False
            Label31.Visible = False
            Label22.Visible = False
            Label23.Visible = False
            TextBox8.Visible = False
            TextBox2.Visible = False
            TextBox3.Visible = False
            TextBox4.Visible = False
            DateTimePicker1.Visible = False
            Label18.Visible = True
            Label14.Visible = True
            TextBox7.Visible = True
            TextBox6.Visible = True
            ComboBox4.Visible = True
            Label35.Visible = True


        Else
            If ComboBox3.Text = "Espèce" Then
                Label33.Visible = False
                Label32.Visible = False
                Label31.Visible = False
                Label22.Visible = False
                Label23.Visible = False
                TextBox8.Visible = False
                TextBox2.Visible = False
                TextBox3.Visible = False
                TextBox4.Visible = False
                DateTimePicker1.Visible = False
                Label18.Visible = False
                Label14.Visible = False
                TextBox7.Visible = False
                TextBox6.Visible = False
                ComboBox4.Visible = False
                Label35.Visible = False
                Label13.Visible = False
                TextBox5.Visible = False
                IconButton2.Visible = False

            End If
            If ComboBox3.Text = "Crédit" Then
                Label33.Visible = False
                Label32.Visible = False
                Label31.Visible = False
                Label22.Visible = False
                Label23.Visible = False
                Label13.Visible = True
                TextBox8.Visible = False
                TextBox2.Visible = False
                TextBox3.Visible = False
                TextBox4.Visible = False
                TextBox5.Visible = True
                DateTimePicker1.Visible = False
                Label18.Visible = False
                Label14.Visible = False
                TextBox7.Visible = False
                TextBox6.Visible = False
                ComboBox4.Visible = False
                Label35.Visible = False
                IconButton2.Visible = True
                CheckBox1.Checked = False
            End If
        End If
    End Sub

    Private Sub IconButton2_Click_1(sender As Object, e As EventArgs) Handles IconButton2.Click
        If (ComboBox3.Text <> "") Then
            If (TextBox5.Text <> "" And TextBox5.Text.ToString.Replace(".", ",") > 0) Then

                adpt = New MySqlDataAdapter("select paye from facture where OrderID = '" + Label11.Text.ToString + "'", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                Dim paye As Double = Convert.ToDouble(table.Rows(0).Item(0).ToString.Replace(".", ",")) + Convert.ToDouble(TextBox5.Text.ToString.Replace(".", ","))
                conn2.Close()
                conn2.Open()
                Dim cmd3 As MySqlCommand
                cmd3 = New MySqlCommand("UPDATE `facture` SET `paye`= '" & paye.ToString("N2") & "'  WHERE OrderID = '" + Label11.Text.ToString + "'", conn2)
                cmd3.ExecuteNonQuery()
                conn2.Close()

                adpt = New MySqlDataAdapter("select id from recues order by id desc", conn2)
                Dim table4 As New DataTable
                adpt.Fill(table4)
                Dim id As Integer
                If table4.Rows.Count <> 0 Then
                    id = Val(table4.Rows(0).Item(0)) + 1
                Else
                    id = Year(Now).ToString & "0001"
                End If
                conn2.close()  
                conn2.open()
                Dim cmd2 As MySqlCommand
                Dim sql2, sql3 As String
                If ComboBox3.Text = "TPE" Then
                    sql2 = "INSERT INTO recues (`id`, `date`, `fac_id`, `regle`, `num`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "','" + Label11.Text + "','" + Convert.ToDouble(TextBox5.Text.Replace(".", ",")).ToString("N2") + "','" + TextBox7.Text + "')"
                    sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `fac`,`bq`,`montant`) 
                    VALUES ('tpe','" + TextBox6.Text + "','tpe','" + TextBox7.Text + "','tpe', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" & Label11.Text & "','" + ComboBox4.Text + "','" + Convert.ToDouble(TextBox5.Text.Replace(".", ",")).ToString("N2") + "')"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.Parameters.Clear()
                    cmd2.ExecuteNonQuery()

                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.ExecuteNonQuery()
                End If
                If ComboBox3.Text = "Chèque" Then
                    sql2 = "INSERT INTO recues (`id`, `date`, `fac_id`, `regle`, `num`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "','" + Label11.Text + "','" + Convert.ToDouble(TextBox5.Text.Replace(".", ",")).ToString("N2") + "','" + TextBox4.Text + "')"
                    sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `fac`,`bq`,`montant`) 
                    VALUES ('" + TextBox8.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + DateTimePicker1.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" & Label11.Text & "','" + ComboBox4.Text + "','" + Convert.ToDouble(TextBox5.Text.Replace(".", ",")).ToString("N2") + "')"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.Parameters.Clear()
                    cmd2.ExecuteNonQuery()

                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.ExecuteNonQuery()
                End If
                If ComboBox3.Text = "Espèce" Then
                    sql2 = "INSERT INTO recues (`id`, `date`, `fac_id`, `regle`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "','" + Label11.Text + "','" + Convert.ToDouble(TextBox5.Text.Replace(".", ",")).ToString("N2") + "')"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.Parameters.Clear()
                    cmd2.ExecuteNonQuery()
                End If

                conn2.Close()
                Dim ds As New DataSet1
                Dim dt As New DataTable
                dt = ds.Tables("tick")
                For i = 0 To 0
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(8).Value, DataGridView1.Rows(i).Cells(9).Value)
                Next

                ReportToPrint = New LocalReport()
                ReportToPrint.ReportPath = Application.StartupPath + "\recu.rdlc"
                ReportToPrint.DataSources.Clear()
                ReportToPrint.EnableExternalImages = True

                Dim cliente As New ReportParameter("client", Label7.Text + vbCrLf + Label8.Text + vbCrLf + Label12.Text)
                Dim client1(0) As ReportParameter
                client1(0) = cliente
                ReportToPrint.SetParameters(client1)

                Dim fac As New ReportParameter("fac_id", Label11.Text.ToString)
                Dim fac1(0) As ReportParameter
                fac1(0) = fac
                ReportToPrint.SetParameters(fac1)

                Dim num As New ReportParameter("num", id.ToString)
                Dim num1(0) As ReportParameter
                num1(0) = num
                ReportToPrint.SetParameters(num1)

                Dim remarque As New ReportParameter("date_fac", Label6.Text)
                Dim remarque1(0) As ReportParameter
                remarque1(0) = remarque
                ReportToPrint.SetParameters(remarque1)

                Dim datee As New ReportParameter("date", DateTime.Now.ToString("dd/MM/yyyy"))
                Dim date1(0) As ReportParameter
                date1(0) = datee
                ReportToPrint.SetParameters(date1)

                Dim neto As Double = Convert.ToDouble(Val(Label9.Text) + Val(Label28.Text))

                Dim ht As New ReportParameter("montant", Convert.ToDouble(Label15.Text).ToString("N2"))
                Dim ht1(0) As ReportParameter
                ht1(0) = ht
                ReportToPrint.SetParameters(ht1)

                Dim tva As New ReportParameter("regle", Convert.ToDouble(TextBox5.Text.Replace(".", ",")).ToString("N2"))
                Dim tva1(0) As ReportParameter
                tva1(0) = tva
                ReportToPrint.SetParameters(tva1)

                adpt = New MySqlDataAdapter("select paye from facture where OrderID = '" + Label11.Text.ToString + "'", conn2)
                Dim table2 As New DataTable
                adpt.Fill(table2)
                Dim solde As Double = Math.Round(Convert.ToDouble(Label15.Text) - Convert.ToDouble(table2.Rows(0).Item(0)), 2)

                Label30.Text = solde

                Dim ttc As New ReportParameter("solde", solde.ToString)
                Dim ttc1(0) As ReportParameter
                ttc1(0) = ttc
                ReportToPrint.SetParameters(ttc1)


                Dim mode As New ReportParameter("mode", ComboBox3.Text)
                Dim mode1(0) As ReportParameter
                mode1(0) = mode
                ReportToPrint.SetParameters(mode1)

                If ComboBox3.Text = "TPE" Then
                    Dim ice As New ReportParameter("info1", "N° d'autorisation : " + TextBox7.Text + vbCrLf + "STAN : " + TextBox6.Text)
                    Dim ice1(0) As ReportParameter
                    ice1(0) = ice
                    ReportToPrint.SetParameters(ice1)
                End If
                If ComboBox3.Text = "Chèque" Then
                    Dim ice As New ReportParameter("info1", "Organisme : " + TextBox8.Text + vbCrLf + "N° de compte : " + TextBox3.Text + vbCrLf + "N° de chèque : " + TextBox4.Text)
                    Dim ice1(0) As ReportParameter
                    ice1(0) = ice
                    ReportToPrint.SetParameters(ice1)
                End If

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

                If solde <= 0 Then
                    conn2.close()  
                    conn2.open()
                    Dim cmd4 As MySqlCommand
                    cmd4 = New MySqlCommand("UPDATE `facture` SET `etat`= 'Payé'  WHERE OrderID = '" + Label11.Text.ToString + "'", conn2)
                    cmd4.ExecuteNonQuery()
                    conn2.Close()
                End If

                conn2.Open()
                sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.Parameters.AddWithValue("@name", factures.Label2.Text)
                cmd3.Parameters.AddWithValue("@op", "Paiement de la facture N° " & Label11.Text & " par reçue N° " & id.ToString)
                cmd3.ExecuteNonQuery()
                conn2.Close()

                MsgBox("Paiement bien effectué !")
                TextBox5.Text = ""
            Else
                MsgBox("Veuillez saisir un montant valide")
            End If
        Else
                MsgBox("Merci de choisir le mode de paiement")
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Supprimer Facture'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Dim Rep As Integer = MsgBox("Voulez-vous vraiment supprimer cette facture ?", vbYesNo)
                If Rep = vbYes Then
                    conn2.Close()
                    conn2.Open()
                    Dim cmd As MySqlCommand
                    Dim sql3 As String
                    cmd = New MySqlCommand("DELETE FROM `facturedetails` WHERE `fac_id` = " + Label11.Text, conn2)
                    cmd.ExecuteNonQuery()

                    cmd = New MySqlCommand("DELETE FROM `facture` WHERE `OrderID` = " + Label11.Text, conn2)
                    cmd.ExecuteNonQuery()

                    cmd = New MySqlCommand("DELETE FROM `facture` WHERE `OrderID` = " + Label11.Text, conn2)
                    cmd.ExecuteNonQuery()

                    cmd = New MySqlCommand("UPDATE `orders` SET `etat`='receipt',`fac_id`='-' WHERE fac_id = " + Label11.Text, conn2)
                    cmd.ExecuteNonQuery()

                    sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd = New MySqlCommand(sql3, conn2)
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@name", user)
                    cmd.Parameters.AddWithValue("@op", "Suppression de la Facture N° " & Label11.Text)
                    cmd.ExecuteNonQuery()

                    conn2.Close()

                    Me.Close()
                End If
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        If Panel3.Visible = False Then
            Panel3.Visible = True
            DataGridView4.Rows.Clear()
            adpt = New MySqlDataAdapter("select OrderID,MontantOrder,OrderDate from orders where fac_id = '" & Label11.Text & "'", conn2)
            Dim table2 As New DataTable
            adpt.Fill(table2)
            For i = 0 To table2.Rows.Count - 1
                adpt = New MySqlDataAdapter("select id from bondelivr where order_id = '" & table2.Rows(i).Item(0) & "'", conn2)
                Dim table4 As New DataTable
                adpt.Fill(table4)
                If table4.Rows.Count <> 0 Then
                    DataGridView4.Rows.Add("BL N° " & table4.Rows(0).Item(0) & " (Vente N° " & table2.Rows(i).Item(0) & ")", Convert.ToDecimal(table2.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")).ToString("# ##0.00"), Convert.ToDateTime(table2.Rows(i).Item(2)).ToString("dd/MM/yyyy"))
                Else
                    DataGridView4.Rows.Add("Vente N° " & table2.Rows(i).Item(0).ToString, Convert.ToDecimal(table2.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ",")).ToString("# ##0.00"), Convert.ToDateTime(table2.Rows(i).Item(2)).ToString("dd/MM/yyyy"))
                End If
            Next
        Else
            Panel3.Visible = False
        End If

    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Modifier facture'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                depense.Show()
                depense.Label11.Text = Label11.Text
                depense.DataGridView2.Rows.Add(0 & " %", 0, 0, 0)
                depense.DataGridView2.Rows.Add(7 & " %", 0, 0, 0)
                depense.DataGridView2.Rows.Add(10 & " %", 0, 0, 0)
                depense.DataGridView2.Rows.Add(14 & " %", 0, 0, 0)
                depense.DataGridView2.Rows.Add(20 & " %", 0, 0, 0)
                depense.TextBox1.ReadOnly = False


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
                For i = 0 To DataGridView1.Rows.Count - 1

                    Dim puht As Double
                    Dim puttc As Double
                    Dim totht As Double
                    Dim totttc As Double

                    puht = DataGridView1.Rows(i).Cells(4).Value.ToString.Replace(" ", "")
                    puttc = DataGridView1.Rows(i).Cells(5).Value.ToString.Replace(" ", "")
                    totttc = Convert.ToDecimal(DataGridView1.Rows(i).Cells(8).Value)
                    totht = Convert.ToDecimal(DataGridView1.Rows(i).Cells(7).Value)
                    depense.DataGridView1.Rows.Add(DataGridView1.Rows(i).Cells(0).Value.ToString.Replace(" ", ""), DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value.ToString.Replace(" ", ""), DataGridView1.Rows(i).Cells(3).Value.ToString.Replace(" ", ""), Convert.ToDouble(puht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(puttc.ToString.Replace(" ", "")).ToString("# ##0.00"), DataGridView1.Rows(i).Cells(6).Value.ToString.Replace(" ", ""), Convert.ToDouble(totht.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDouble(totttc.ToString.Replace(" ", "")).ToString("# ##0.00"), Convert.ToDecimal(DataGridView1.Rows(i).Cells(9).Value.ToString.Replace(" ", "")).ToString("# ##0.00"), DataGridView1.Rows(i).Cells(10).Value.ToString.Replace(" ", ""), DataGridView1.Rows(i).Cells(11).Value.ToString.Replace(" ", ""))

                Next

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

                depense.Label15.Text = Label15.Text
                depense.Label9.Text = Label9.Text
                depense.Label27.Text = Label28.Text
                depense.Label10.Text = Label10.Text

                depense.Label7.Text = Label7.Text
                depense.Label34.Text = Label36.Text

                depense.Label8.Text = Label8.Text

                depense.Label12.Text = Label12.Text

                depense.Label2.Text = Label2.Text
                If Label43.Text = "0" Then
                    adpt = New MySqlDataAdapter("select remisepour from clients where client = '" + Label7.Text + "' ", conn2)
                    Dim table2 As New DataTable
                    adpt.Fill(table2)
                    depense.TextBox9.Text = table2.Rows(0).Item(0)
                    depense.IconButton9.PerformClick()
                Else
                    depense.TextBox9.Text = Label43.Text
                    depense.IconButton9.PerformClick()


                End If


                depense.TextBox1.Text = TextBox1.Text.Replace("Facture N° ", "")
                depense.DateTimePicker2.Value = Label6.Text
                depense.Label32.Text = Label37.Text
                depense.Label32.ForeColor = Label37.ForeColor

                depense.IconButton6.Visible = True
                depense.DateTimePicker2.Visible = True

                adpt = New MySqlDataAdapter("select paye, reste from facture where OrderID = '" & Label11.Text & "'", conn2)
                Dim table4 As New DataTable
                adpt.Fill(table4)
                depense.Label38.Text = table4.Rows(0).Item(0)
                depense.Label39.Text = table4.Rows(0).Item(1)
                Me.Close()
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged

        Dim inputText As String = TextBox10.Text

        ' Vérifiez si le texte est vide ou nul
        If Not String.IsNullOrEmpty(inputText) Then
            ' Divisez le texte en mots en utilisant des espaces comme séparateurs
            Dim words As String() = inputText.Split(New Char() {" "c}, StringSplitOptions.None)

            ' Convertissez la première lettre de chaque mot en majuscule
            For i As Integer = 0 To words.Length - 1
                If words(i).Length > 0 Then
                    words(i) = words(i).Substring(0, 1).ToUpper() & words(i).Substring(1).ToLower()
                End If
            Next

            ' Recréez le texte modifié avec des espaces entre les mots
            Dim modifiedText As String = String.Join(" ", words)

            ' Désactivez temporairement le gestionnaire d'événements TextChanged
            RemoveHandler TextBox10.TextChanged, AddressOf TextBox10_TextChanged

            ' Mettez à jour le texte dans le TextBox
            TextBox10.Text = modifiedText

            ' Replacez le curseur à la position correcte après la modification
            TextBox10.SelectionStart = TextBox10.Text.Length

            ' Réactivez le gestionnaire d'événements TextChanged
            AddHandler TextBox10.TextChanged, AddressOf TextBox10_TextChanged
        End If

        Dim texteRecherche As String = TextBox10.Text.Trim().ToLower()

        If Not String.IsNullOrEmpty(texteRecherche) Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim cell As DataGridViewCell = row.Cells(1) ' Remplacez "Designation" par le nom réel de la colonne
                If cell.Value IsNot Nothing AndAlso cell.Value.ToString().ToLower().Contains(texteRecherche) Then
                    DataGridView1.ClearSelection()
                    row.Selected = True
                    DataGridView1.FirstDisplayedScrollingRowIndex = row.Index ' Fait défiler la grille jusqu'à la ligne sélectionnée
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        Dim texteRecherche As String = TextBox9.Text.Trim().ToLower()

        If Not String.IsNullOrEmpty(texteRecherche) Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim cell As DataGridViewCell = row.Cells(0) ' Remplacez "Designation" par le nom réel de la colonne
                If cell.Value IsNot Nothing AndAlso cell.Value.ToString().ToLower().Contains(texteRecherche) Then
                    DataGridView1.ClearSelection()
                    row.Selected = True
                    DataGridView1.FirstDisplayedScrollingRowIndex = row.Index ' Fait défiler la grille jusqu'à la ligne sélectionnée
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        Dim ds As New DataSet1
        Dim dt3 As New DataTable
        dt3 = ds.Tables("releve")
        For i = 0 To DataGridView4.Rows.Count - 1

            dt3.Rows.Add(DataGridView4.Rows(i).Cells(0).Value.ToString, DataGridView4.Rows(i).Cells(1).Value.ToString, DataGridView4.Rows(i).Cells(2).Value.ToString)

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

        Dim clt As New ReportParameter("clt", Label7.Text)
        Dim clt1(0) As ReportParameter
        clt1(0) = clt
        ReportToPrint.SetParameters(clt1)

        Dim ttc2 As Double = Convert.ToDouble(Label9.Text) + Convert.ToDouble(Label10.Text) + Convert.ToDouble(Label36.Text)

        Dim ttc As New ReportParameter("ttc", Convert.ToDouble(ttc2).ToString("N2"))
        Dim ttc1(0) As ReportParameter
        ttc1(0) = ttc
        ReportToPrint.SetParameters(ttc1)


        Dim net As New ReportParameter("net", Convert.ToDouble(Label9.Text).ToString("N2"))
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
    End Sub

    Private Sub IconButton8_Click(sender As Object, e As EventArgs) Handles IconButton8.Click
        If Panel4.Visible = False Then
            Panel4.Visible = True
            DataGridView5.Rows.Clear()
            adpt = New MySqlDataAdapter("select reglement from reglement_fac where fac = '" & Label11.Text & "'", conn2)
            Dim table2 As New DataTable
            adpt.Fill(table2)
            If table2.Rows.Count <> 0 Then
                For i = 0 To table2.Rows.Count - 1
                    adpt = New MySqlDataAdapter("select `montant`, `date`, `mode` FROM `reglement` where fac = '" & table2.Rows(i).Item(0) & "'", conn2)
                    Dim table4 As New DataTable
                    adpt.Fill(table4)
                    If table4.Rows.Count <> 0 Then
                        DataGridView5.Rows.Add(table4.Rows(0).Item(2), Convert.ToDecimal(table4.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ",")).ToString("# ##0.00"), Convert.ToDateTime(table4.Rows(0).Item(1)).ToString("dd/MM/yyyy"))
                    End If
                Next
            Else
                If Label37.Text = "Facture payée" Then
                    adpt = New MySqlDataAdapter("select modePayement,MontantOrder, OrderDate from orders where fac_id = '" & Label11.Text & "'", conn2)
                    Dim tablereg As New DataTable
                    adpt.Fill(tablereg)
                    For j = 0 To tablereg.Rows.Count - 1
                        DataGridView5.Rows.Add(tablereg.Rows(j).Item(0), Convert.ToDecimal(tablereg.Rows(j).Item(1).ToString.Replace(" ", "").Replace(".", ",")).ToString("# ##0.00"), Convert.ToDateTime(tablereg.Rows(j).Item(2)).ToString("dd/MM/yyyy"))
                    Next
                End If
            End If

        Else
            Panel4.Visible = False
        End If
    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click

    End Sub
End Class