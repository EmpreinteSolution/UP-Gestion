Imports System.ComponentModel
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms.DataVisualization.Charting
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Imports System.Threading
Imports Tulpep.NotificationWindow

Public Class Home
    Dim adpt1, adpt, adpt2, adpt3, adpt4, adpt5, adpt6, adpt7, adpt8, adpt9 As MySqlDataAdapter

    <Obsolete>
    Private Sub Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim popup As PopupNotifier = New PopupNotifier()


        popup.TitleFont = New Font("Arial", 12, FontStyle.Bold)
        popup.ContentFont = New Font("Arial", 10)
        popup.ImagePadding = New Padding(10)
        popup.TitlePadding = New Padding(10)
        popup.ContentPadding = New Padding(10)
        popup.AnimationDuration = 500 ' Durée de l'animation en millisecondes
        popup.Delay = 987000 ' Durée d'affichage de la notification en millisecondes

        popup.HeaderColor = Color.FromArgb(255, 0, 0) ' Couleur de fond de l'en-tête
        popup.BodyColor = Color.FromArgb(255, 255, 255) ' Couleur de fond du corps
        popup.BorderColor = Color.FromArgb(0, 0, 255) ' Couleur de la bordure




        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Dim appPath As String = Application.StartupPath()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = SaveDirectory & imgName.ToString.Replace("/", "\")
        If System.IO.File.Exists(SavePath) Then
            PictureBox2.Image = Image.FromFile(SavePath)
        End If
        Dim printerticket As String = System.Configuration.ConfigurationSettings.AppSettings("printerticket")
        Dim printera5 As String = System.Configuration.ConfigurationSettings.AppSettings("printera5")
        Dim printera4 As String = System.Configuration.ConfigurationSettings.AppSettings("printera4")

        receiptprinter = printerticket
        a4printer = printera4
        a5printer = printera5
        Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Label2.Text = user

        adpt1 = New MySqlDataAdapter("select * from cheques WHERE etat = @day and organisme <> @org", conn2)
        adpt1.SelectCommand.Parameters.Clear()
            adpt1.SelectCommand.Parameters.AddWithValue("@day", 0)
            adpt1.SelectCommand.Parameters.AddWithValue("@org", "tpe")
            Dim table1 As New DataTable
            adpt1.Fill(table1)
            Dim chqcount1 As Double = 0
            Dim chqcount2 As Double = 0
            For i = 0 To table1.Rows.Count() - 1
                Dim ech As String = DateDiff(DateInterval.Day, Now.Date, table1.Rows(i).Item(5))

                If IsDBNull(table1.Rows(i).Item(8)) Then
                    If ech <= 14 Then
                        chqcount1 += 1
                    End If
                    IconButton53.Enabled = True
                    IconButton53.Text = "Chèques encaissés " & "(" & chqcount1 & ")"

                Else
                    If ech <= 14 Then
                        chqcount2 += 1
                    End If
                    IconButton52.Enabled = True
                    IconButton52.Text = "Chèques reglès " & "(" & chqcount2 & ")"

                End If
            Next

            If (chqcount1 + chqcount2) > 0 Then
                popup.TitleText = "Alertes des chèques à consulter !"
                popup.ContentText = (chqcount1 + chqcount2) & " Chèques, leurs pointage doit etre vérifié."
                popup.Popup()


                IconButton50.Visible = False
            End If

            adpt6 = New MySqlDataAdapter("select * from facture WHERE REPLACE(reste,',','.') > @day ", conn2)
            adpt6.SelectCommand.Parameters.Clear()
            adpt6.SelectCommand.Parameters.AddWithValue("@day", 0)
            Dim table6 As New DataTable
            adpt6.Fill(table6)
            Dim faccount As Double = 0
            For i = 0 To table6.Rows.Count() - 1
                adpt6 = New MySqlDataAdapter("select * from clients WHERE client = @day ", conn2)
                adpt6.SelectCommand.Parameters.Clear()
                adpt6.SelectCommand.Parameters.AddWithValue("@day", table6.Rows(i).Item(5))
                Dim table8 As New DataTable
                adpt6.Fill(table8)
                Dim ech As String = DateDiff(DateInterval.Day, Now.Date, table6.Rows(i).Item(1))
                If ech >= table8.Rows(0).Item(18) Then
                    faccount += 1

                End If
            Next

            If faccount > 0 Then
                IconButton50.Visible = False
                IconButton20.Enabled = True
                IconButton20.Text = "Factures à encaisser " & "(" & faccount & ")"
                popup.TitleText = "Alertes des factures à consulter !"
                popup.ContentText = faccount & " Factures ont dépassé leurs délai de facturation."
                popup.Popup()
            End If

            Dim currentDate As DateTime = DateTime.Now
            Dim previousMonth As Integer = currentDate.Month - 1

            Dim previousDay As DateTime = DateTime.Now.AddDays(-1)

            If previousMonth = 0 Then
                previousMonth = 12
            End If

            adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(MontantOrder,',','.'),' ','')) from orders WHERE Month(OrderDate) = @day and parked = 'no'", conn2)
            adpt1.SelectCommand.Parameters.Clear()
            adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Month)
            Dim tableorder As New DataTable
            adpt1.Fill(tableorder)
        Dim camois As Double = 0
        Dim camoisprev As Double = 0
        Dim cayear As Double = 0
            Dim creord As Double = 0
            If IsDBNull(tableorder.Rows(0).Item(0)) Then
            Else
                camois = Convert.ToDouble(tableorder.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
            End If

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(MontantOrder,',','.'),' ','')) from orders WHERE Month(OrderDate) = @day and parked = 'no'", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", previousMonth)
        Dim tableorderprev As New DataTable
        adpt1.Fill(tableorderprev)

        If IsDBNull(tableorderprev.Rows(0).Item(0)) Then
        Else
            camoisprev = Convert.ToDouble(tableorderprev.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
        End If

        Dim evol As Double = 0

        evol = ((camois - camoisprev) / camoisprev) * 100

        IconButton57.Text = evol.ToString("N0") & "%"
        If evol < 0 Then
            IconButton57.ForeColor = Color.Red
            IconButton57.IconChar = FontAwesome.Sharp.IconChar.ArrowDown
            IconButton57.IconColor = Color.Red
        Else
            IconButton57.ForeColor = Color.Green
            IconButton57.IconChar = FontAwesome.Sharp.IconChar.ArrowUp
            IconButton57.IconColor = Color.Green
        End If

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(MontantOrder,',','.'),' ','')) from orders WHERE Year(OrderDate) = @day and parked = 'no'", conn2)
                adpt1.SelectCommand.Parameters.Clear()
                adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
                Dim tableorder2 As New DataTable
            adpt1.Fill(tableorder2)
            If IsDBNull(tableorder2.Rows(0).Item(0)) Then
            Else
                cayear = Convert.ToDouble(tableorder2.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
            End If

            adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(marge,',','.'),' ','')) from orderdetails WHERE Month(date) = @day and Year(date) = @year", conn2)
                adpt1.SelectCommand.Parameters.Clear()
                adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Month)
                adpt1.SelectCommand.Parameters.AddWithValue("@year", currentDate.Year)
                Dim tablemargemois As New DataTable
                adpt1.Fill(tablemargemois)

                adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(marge,',','.'),' ','')) from orderdetails WHERE Month(date) = @mnt and Day(date) = @day and Year(date) = @year", conn2)
                adpt1.SelectCommand.Parameters.Clear()
                adpt1.SelectCommand.Parameters.AddWithValue("@mnt", currentDate.Month)
                adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Day)
                adpt1.SelectCommand.Parameters.AddWithValue("@year", currentDate.Year)
                Dim tablemargejour As New DataTable
                adpt1.Fill(tablemargejour)


                Dim margemois As Double = 0
                Dim margejour As Double = 0
                Dim margeyear As Double = 0
            Dim margenmois As Double = 0

            If IsDBNull(tablemargemois.Rows(0).Item(0)) Then
            Else
                margemois = Convert.ToDouble(tablemargemois.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
            End If

            If IsDBNull(tablemargejour.Rows(0).Item(0)) Then
            Else
                margejour = Convert.ToDouble(tablemargejour.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

            End If


            Dim chargyear As Double = 0

                adpt1 = New MySqlDataAdapter("SELECT SUM(REPLACE(REPLACE(j,',','.'),' ','') + REPLACE(REPLACE(f,',','.'),' ','') + REPLACE(REPLACE(m,',','.'),' ','') +REPLACE(REPLACE(a,',','.'),' ','') +REPLACE(REPLACE(mai,',','.'),' ','') +REPLACE(REPLACE(juin,',','.'),' ','') +REPLACE(REPLACE(juil,',','.'),' ','') +REPLACE(REPLACE(ao,',','.'),' ','') +REPLACE(REPLACE(sept,',','.'),' ','') +REPLACE(REPLACE(oct,',','.'),' ','') +REPLACE(REPLACE(nov,',','.'),' ','') +REPLACE(REPLACE(dece,',','.'),' ','') ) FROM charges WHERE year = @day", conn2)
                adpt1.SelectCommand.Parameters.Clear()
                adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
                Dim tableorder3 As New DataTable
        adpt1.Fill(tableorder3)
        If IsDBNull(tableorder3.Rows(0).Item(0)) Then
        Else
            chargyear = Convert.ToDouble(tableorder3.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(marge,',','.'),' ','')) from orderdetails WHERE Year(date) = @day", conn2)
                adpt1.SelectCommand.Parameters.Clear()
                adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
                Dim tablemargeyear As New DataTable
                adpt1.Fill(tablemargeyear)

            If IsDBNull(tablemargeyear.Rows(0).Item(0)) Then
            Else
                margeyear = Convert.ToDouble(tablemargeyear.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
            End If

            Dim ch1 As Double = 0
                Dim ch2 As Double = 0
                Dim ch3 As Double = 0
                Dim ch4 As Double = 0
                Dim ch5 As Double = 0
                Dim ch6 As Double = 0
                Dim ch7 As Double = 0
                Dim ch8 As Double = 0
                Dim ch9 As Double = 0
                Dim ch10 As Double = 0
                Dim ch11 As Double = 0
                Dim ch12 As Double = 0


                adpt1 = New MySqlDataAdapter("SELECT SUM(REPLACE(REPLACE(j,',','.'),' ','')) ,SUM(REPLACE(REPLACE(f,',','.'),' ','')) ,SUM(REPLACE(REPLACE(m,',','.'),' ','')) ,SUM(REPLACE(REPLACE(a,',','.'),' ','')) ,SUM(REPLACE(REPLACE(mai,',','.'),' ','')) ,SUM(REPLACE(REPLACE(juin,',','.'),' ','')) ,SUM(REPLACE(REPLACE(juil,',','.'),' ','')) ,SUM(REPLACE(REPLACE(ao,',','.'),' ','')) ,SUM(REPLACE(REPLACE(sept,',','.'),' ','')) ,SUM(REPLACE(REPLACE(oct,',','.'),' ','')) ,SUM(REPLACE(REPLACE(nov,',','.'),' ','')) ,SUM(REPLACE(REPLACE(dece,',','.'),' ','')) FROM charges WHERE year = @day;", conn2)
                adpt1.SelectCommand.Parameters.Clear()
                adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
                Dim tablechamens As New DataTable
        adpt1.Fill(tablechamens)
        If IsDBNull(tablechamens.Rows(0).Item(currentDate.Month - 1)) Then
        Else
            ch1 = ch1 + Convert.ToDouble(tablechamens.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
            ch2 = ch2 + Convert.ToDouble(tablechamens.Rows(0).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            ch3 = ch3 + Convert.ToDouble(tablechamens.Rows(0).Item(2).ToString.Replace(" ", "").Replace(".", ","))
            ch4 = ch4 + Convert.ToDouble(tablechamens.Rows(0).Item(3).ToString.Replace(" ", "").Replace(".", ","))
            ch5 = ch5 + Convert.ToDouble(tablechamens.Rows(0).Item(4).ToString.Replace(" ", "").Replace(".", ","))
            ch6 = ch6 + Convert.ToDouble(tablechamens.Rows(0).Item(5).ToString.Replace(" ", "").Replace(".", ","))
            ch7 = ch7 + Convert.ToDouble(tablechamens.Rows(0).Item(6).ToString.Replace(" ", "").Replace(".", ","))
            ch8 = ch8 + Convert.ToDouble(tablechamens.Rows(0).Item(7).ToString.Replace(" ", "").Replace(".", ","))
            ch9 = ch9 + Convert.ToDouble(tablechamens.Rows(0).Item(8).ToString.Replace(" ", "").Replace(".", ","))
            ch10 = ch10 + Convert.ToDouble(tablechamens.Rows(0).Item(9).ToString.Replace(" ", "").Replace(".", ","))
            ch11 = ch11 + Convert.ToDouble(tablechamens.Rows(0).Item(10).ToString.Replace(" ", "").Replace(".", ","))
            ch12 = ch12 + Convert.ToDouble(tablechamens.Rows(0).Item(11).ToString.Replace(" ", "").Replace(".", ","))
        End If


        If currentDate.Month = "01" Then
            margenmois = ch1
        End If
        If currentDate.Month = "02" Then
                    margenmois = ch2
                End If
                If currentDate.Month = "03" Then
                    margenmois = ch3
                End If
                If currentDate.Month = "04" Then
                    margenmois = ch4
                End If
                If currentDate.Month = "05" Then
                    margenmois = ch5
                End If
                If currentDate.Month = "06" Then
                    margenmois = ch6
                End If
                If currentDate.Month = "07" Then
                    margenmois = ch7
                End If
                If currentDate.Month = "08" Then
                    margenmois = ch8
                End If
                If currentDate.Month = "09" Then
                    margenmois = ch9
                End If
                If currentDate.Month = "10" Then
                    margenmois = ch10
                End If
                If currentDate.Month = "11" Then
                    margenmois = ch11
                End If
            If currentDate.Month = "12" Then
                margenmois = ch12
            End If

            Dim rmsyear As Double = 0
            Dim rmsmois As Double = 0

            adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`remise`,',','.'),' ','')) from facture where remise > 0 and Year(OrderDate) = @day", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
            Dim tablermsyear As New DataTable
            adpt.Fill(tablermsyear)

            adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`remise`,',','.'),' ','')) from facture where remise > 0 and Year(OrderDate) = @day and month(OrderDate) = @mois", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
            adpt.SelectCommand.Parameters.AddWithValue("@mois", currentDate.Month)
            Dim tablermsmois As New DataTable
            adpt.Fill(tablermsmois)
        If IsDBNull(tablermsyear.Rows(0).Item(0)) Then
        Else
            rmsyear = Convert.ToDouble(tablermsyear.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If
        If IsDBNull(tablermsmois.Rows(0).Item(0)) Then
        Else
            rmsmois = Convert.ToDouble(tablermsmois.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        Dim demarqmois As Double = 0
        Dim demarqyear As Double = 0

        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`valeur`,',','.'),' ','')) from demarque where Year(date) = @day and month(date) = @mois", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        adpt.SelectCommand.Parameters.AddWithValue("@mois", currentDate.Month)
        Dim tabledemarqmois As New DataTable
        adpt.Fill(tabledemarqmois)
        If IsDBNull(tabledemarqmois.Rows(0).Item(0)) Then
        Else
            demarqmois = Convert.ToDouble(tabledemarqmois.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`valeur`,',','.'),' ','')) from demarque where Year(date) = @day", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        Dim tabledemarqyear As New DataTable
        adpt.Fill(tabledemarqyear)
        If IsDBNull(tabledemarqyear.Rows(0).Item(0)) Then
        Else
            demarqyear = Convert.ToDouble(tabledemarqyear.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        Dim dividmois As Double = 0
        Dim dividyear As Double = 0

        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`montant`,',','.'),' ','')) from dividendes where Year(date) = @day and month(date) = @mois", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        adpt.SelectCommand.Parameters.AddWithValue("@mois", currentDate.Month)
        Dim tabledividmois As New DataTable
        adpt.Fill(tabledividmois)
        If IsDBNull(tabledividmois.Rows(0).Item(0)) Then
        Else
            dividmois = Convert.ToDouble(tabledividmois.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`montant`,',','.'),' ','')) from dividendes where Year(date) = @day", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        Dim tabledividyear As New DataTable
        adpt.Fill(tabledividyear)
        If IsDBNull(tabledividyear.Rows(0).Item(0)) Then
        Else
            dividyear = Convert.ToDouble(tabledividyear.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If


        Label15.Text = camois.ToString("N2")
        Label20.Text = Convert.ToDouble(margeyear - chargyear - rmsyear - demarqyear - dividyear).ToString("N2")
        Label19.Text = Convert.ToDouble(margemois - margenmois - rmsmois - demarqmois - dividmois).ToString("N2")
        Label24.Text = cayear.ToString("N2")
                Label18.Text = margemois.ToString("N2")
                Label21.Text = margejour.ToString("N2")

                Dim ca1 As Double = 0
                Dim ca2 As Double = 0
                Dim ca3 As Double = 0
                Dim ca4 As Double = 0
                Dim ca5 As Double = 0
                Dim ca6 As Double = 0
                Dim ca7 As Double = 0
                Dim ca8 As Double = 0
                Dim ca9 As Double = 0
                Dim ca10 As Double = 0
                Dim ca11 As Double = 0
                Dim ca12 As Double = 0

                adpt1 = New MySqlDataAdapter("SELECT DATE_FORMAT(OrderDate, '%m'), SUM(REPLACE(REPLACE(MontantOrder,',','.'),' ','')) FROM orders WHERE Year(OrderDate) = @day and parked = 'no' GROUP BY DATE_FORMAT(OrderDate, '%m') ORDER BY DATE_FORMAT(OrderDate, '%m')", conn2)
                adpt1.SelectCommand.Parameters.Clear()
                adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
                Dim tablecamens As New DataTable
                adpt1.Fill(tablecamens)
                For i = 0 To tablecamens.Rows.Count - 1
                    If tablecamens.Rows(i).Item(0) = "01" Then
                        ca1 = ca1 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
                    End If
                    If tablecamens.Rows(i).Item(0) = "02" Then
                        ca2 = ca2 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
                    End If
                    If tablecamens.Rows(i).Item(0) = "03" Then
                        ca3 = ca3 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
                    End If
                    If tablecamens.Rows(i).Item(0) = "04" Then
                        ca4 = ca4 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
                    End If
                    If tablecamens.Rows(i).Item(0) = "05" Then
                        ca5 = ca5 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
                    End If
                    If tablecamens.Rows(i).Item(0) = "06" Then
                        ca6 = ca6 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
                    End If
                    If tablecamens.Rows(i).Item(0) = "07" Then
                        ca7 = ca7 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
                    End If
                    If tablecamens.Rows(i).Item(0) = "08" Then
                        ca8 = ca8 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
                    End If
                    If tablecamens.Rows(i).Item(0) = "09" Then
                        ca9 = ca9 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
                    End If
                    If tablecamens.Rows(i).Item(0) = "10" Then
                        ca10 = ca10 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
                    End If
                    If tablecamens.Rows(i).Item(0) = "11" Then
                        ca11 = ca11 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
                    End If
                    If tablecamens.Rows(i).Item(0) = "12" Then
                        ca12 = ca12 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
                    End If

                Next

                Dim chiffreAffaires() As Double = {ca1, ca2, ca3, ca4, ca5, ca6, ca7, ca8, ca9, ca10, ca11, ca12}

                ' Définissez les mois sous forme d'abréviations
                Dim mois As String() = {"M1", "M2", "M3", "M4", "M5", "M6", "M7", "M8", "M9", "M10", "M11", "M12"}

                ' Associez les valeurs du chiffre d'affaires aux mois
                Chart1.Series(0).Points.DataBindY(chiffreAffaires)
                Chart1.ChartAreas(0).AxisX.Interval = 1
                ' Définissez les étiquettes des mois pour l'axe horizontal
                Chart1.ChartAreas(0).AxisX.CustomLabels.Clear()
                For i As Integer = 0 To mois.Length - 1
                    Chart1.ChartAreas(0).AxisX.CustomLabels.Add(i + 0.5, i + 1.5, mois(i))
                Next i


                adpt1 = New MySqlDataAdapter("select name,count(*) from orderdetails group by name order by count(*) desc limit 20", conn2)
                adpt1.SelectCommand.Parameters.Clear()
                Dim tabletop As New DataTable
                adpt1.Fill(tabletop)

                DataGridView1.Rows.Clear()
                ' Ajouter des points de données
                For i = 0 To tabletop.Rows.Count - 1
                    DataGridView1.Rows.Add(tabletop.Rows(i).Item(0), Convert.ToDouble(tabletop.Rows(i).Item(1)).ToString("N0"))
                Next

                adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(reste,',','.'),' ','')) from achat where REPLACE(REPLACE(reste,',','.'),' ','') > 0", conn2)
                adpt1.SelectCommand.Parameters.Clear()
                Dim tabledette As New DataTable
                adpt1.Fill(tabledette)
        Dim dette As Double = 0
        If IsDBNull(tabledette.Rows(0).Item(0)) Then
        Else
            dette = Convert.ToDouble(tabledette.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(reste,',','.'),' ','')) from facture where REPLACE(REPLACE(reste,',','.'),' ','') > 0", conn2)
                adpt1.SelectCommand.Parameters.Clear()
                Dim tablecreance As New DataTable
                adpt1.Fill(tablecreance)
        Dim creance As Double = 0
        If IsDBNull(tablecreance.Rows(0).Item(0)) Then
        Else
            creance = Convert.ToDouble(tablecreance.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(reste,',','.'),' ','')) from orders where REPLACE(REPLACE(reste,',','.'),' ','') > 0 and etat = 'receipt'", conn2)
                adpt1.SelectCommand.Parameters.Clear()
                Dim tablecreance2 As New DataTable
                adpt1.Fill(tablecreance2)
                If IsDBNull(tablecreance2.Rows(0).Item(0)) Then
                Else
                    creord = Convert.ToDouble(tablecreance2.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

                End If

                adpt1 = New MySqlDataAdapter("select * from cheques where etat = '0'", conn2)
                adpt1.SelectCommand.Parameters.Clear()
                Dim tableech As New DataTable
                adpt1.Fill(tableech)
                Dim echfrs As Double = 0
                Dim echclt As Double = 0
                For i = 0 To tableech.Rows.Count - 1
                    If IsDBNull(tableech.Rows(i).Item(8)) Then
                        echclt = echclt + Convert.ToDouble(tableech.Rows(i).Item(12).ToString.Replace(" ", "").Replace(".", ","))
                    Else
                        echfrs = echfrs + Convert.ToDouble(tableech.Rows(i).Item(12).ToString.Replace(" ", "").Replace(".", ","))
                    End If
                Next

                Label35.Text = dette.ToString("N2")
                Label34.Text = (creance + creord).ToString("N2")
                Label33.Text = echclt.ToString("N2")
                Label29.Text = echfrs.ToString("N2")


                adpt = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(PA_TTC,',','.'),' ','') * REPLACE(REPLACE(Stock,',','.'),' ','')) from article", conn2)
                Dim tablestock As New DataTable
                adpt.Fill(tablestock)

                adpt = New MySqlDataAdapter("select ttc from inventaire_list ORDER BY ID DESC", conn2)
                Dim tableinv As New DataTable
                adpt.Fill(tableinv)

        Dim stockfin, stockdebut As Double
        If IsDBNull(tablestock.Rows(0).Item(0)) Then
            stockfin = 0
        Else
            stockfin = Convert.ToDouble(tablestock.Rows(0).Item(0).ToString.Replace(".", ","))

        End If
        If tableinv.Rows.Count <> 0 Then

            If IsDBNull(tableinv.Rows(0).Item(0)) Then
                stockdebut = 0
            Else
                stockdebut = Convert.ToDouble(tableinv.Rows(0).Item(0).ToString.Replace(".", ","))

            End If
        Else
            stockdebut = 0
        End If
        Dim variation As Double = (stockdebut + stockfin) / 2
                Label30.Text = stockfin.ToString("N2")
                Label28.Text = Convert.ToDouble(cayear / variation).ToString("N0")

        Dim w = Screen.PrimaryScreen.WorkingArea.Width
        Dim h = My.Computer.Screen.WorkingArea.Size.Height
        Me.Width = w
        Me.Height = h
        Me.Location = New Point(0, 0)


        Panel5.Width = w - 162
        Panel5.Height = h - 60



        Panel3.Width = Panel5.Width / 3.3
        Panel3.Location = New Point(10, 97)

        Dim l As Integer = Panel3.Width
        Panel4.Width = Panel5.Width / 3.3
        Panel4.Location = New Point(l + (Panel5.Width / 24), 97)

        Dim l3 As Integer = Panel3.Width + Panel4.Width + (Panel5.Width / 35)
        Panel8.Width = Panel5.Width / 3.3
        Panel8.Location = New Point(l3 + (Panel5.Width / 20), 97)



        Dim t As Integer = Panel3.Height + Panel5.Height / 17
        Panel7.Width = Panel5.Width / 3.3
        Panel7.Location = New Point(10, t + 97)

        Dim l2 As Integer = Panel7.Width
        Panel6.Width = Panel5.Width / 3.3
        Panel6.Location = New Point(l2 + (Panel5.Width / 24), t + 97)

        Dim l4 As Integer = Panel7.Width + Panel6.Width + (Panel5.Width / 35)
        Panel26.Width = Panel5.Width / 3.3
        Panel26.Location = New Point(l4 + (Panel5.Width / 20), t + 97)
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Dashboard'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Non Autorise" Then
                Panel3.Visible = False
                Panel4.Visible = False
                Panel7.Visible = False
                Panel6.Visible = False
                Panel8.Visible = False
                Panel26.Visible = False
                Panel37.Visible = False
            End If
        End If

        Me.KeyPreview = True
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
    Private Sub VotreFonctionEnArrierePlan()

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Dim Rep As Integer
        Rep = MsgBox("Voulez-vous vraiment quitter ?", vbYesNo)
        If Rep = vbYes Then
            Dim log = New Form1()
            log.Show()
            Me.Close()
        End If
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Me.Show()
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        stock.Show()
        Me.Close()
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        dashboard.Show()
        dashboard.IconButton9.Visible = True
        dashboard.IconButton8.Visible = False
    End Sub





    Private Sub IconButton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
        If My.Computer.Network.IsAvailable Then
            Synchronisation.Show()
        Else
            MsgBox("Vérifier votre connexion internet !")

        End If
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub IconButton8_Click_1(sender As Object, e As EventArgs) Handles IconButton8.Click
        product.Show()
        Me.Close()
    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Client.Show()
        Me.Close()
    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click
        achats.Show()
        Me.Close()
    End Sub

    Private Sub IconButton15_Click(sender As Object, e As EventArgs) Handles IconButton15.Click
        devis.Show()
        Me.Close()
    End Sub

    Private Sub IconButton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Configuration'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then
                infos.ShowDialog()
                Panel10.Visible = False
                Panel11.Visible = False
                Panel12.Visible = False
                Panel33.Visible = False
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton21_Click(sender As Object, e As EventArgs) Handles IconButton21.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Gestion des banques'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then
                bank.ShowDialog()
                Panel10.Visible = False
                Panel11.Visible = False
                Panel12.Visible = False
                Panel33.Visible = False
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub


    Private Sub IconButton23_Click(sender As Object, e As EventArgs) Handles IconButton23.Click
        Charges.Show()
        Me.Close()
    End Sub

    Private Sub IconButton24_Click(sender As Object, e As EventArgs) Handles IconButton24.Click
        banques.Show()
        Me.Close()
    End Sub

    Private Sub IconButton25_Click(sender As Object, e As EventArgs) Handles IconButton25.Click
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
        If Panel10.Visible = True Then
            Panel10.Visible = False
        Else
            Panel10.Visible = True
        End If
    End Sub

    Private Sub IconButton26_Click(sender As Object, e As EventArgs) Handles IconButton26.Click
        simpleajout.ShowDialog()
        Panel10.Visible = False
    End Sub

    Private Sub IconButton28_Click(sender As Object, e As EventArgs) Handles IconButton28.Click
        balisage.ShowDialog()
        Panel10.Visible = False
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
    End Sub

    Private Sub IconButton29_Click(sender As Object, e As EventArgs) Handles IconButton29.Click
        ajout.Show()
        Panel10.Visible = False
        ajout.Panel8.Visible = True
        ajout.Panel9.Visible = True
        ajout.IconButton16.Visible = True
        ajout.IconButton13.Visible = False
        ajout.IconButton3.Visible = False
        ajout.Label16.Text = "Afficher ou modifier un Article :"
    End Sub

    Private Sub IconButton27_Click(sender As Object, e As EventArgs) Handles IconButton27.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Fiche Article'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then
                ajout.Show()
                Panel10.Visible = False
                Panel11.Visible = False
                Panel12.Visible = False
                Panel33.Visible = False

                ajout.Panel8.Visible = True

            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub


    Private Sub IconButton31_Click(sender As Object, e As EventArgs) Handles IconButton31.Click
        balisage.Show()
        balisage.Panel8.Visible = True
        balisage.Label16.Text = "Code-Barres"
        Panel10.Visible = False
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
    End Sub

    Private Sub Panel9_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub IconButton32_Click(sender As Object, e As EventArgs) Handles IconButton32.Click
        Panel10.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
        If Panel11.Visible = True Then
            Panel11.Visible = False
        Else
            Panel11.Visible = True
        End If
    End Sub

    Private Sub IconButton36_Click(sender As Object, e As EventArgs) Handles IconButton36.Click
        rapproche.Show()
        rapproche.Panel6.Visible = True
        rapproche.Label16.Text = "Encaissement Client"
        adpt = New MySqlDataAdapter("select client from clients order by client asc", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count <> 0 Then
            rapproche.ComboBox2.Items.Clear()
            rapproche.ComboBox2.Items.Add("Tout")
            For i = 0 To table2.Rows.Count - 1
                rapproche.ComboBox2.Items.Add(table2.Rows(i).Item(0))
            Next
            rapproche.ComboBox2.SelectedIndex = 0

        End If
        Panel10.Visible = False
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
    End Sub

    Private Sub IconButton35_Click(sender As Object, e As EventArgs) Handles IconButton35.Click
        rapproche.Show()
        rapproche.Panel7.Visible = True
        rapproche.Label16.Text = "Réglement fournisseur"
        adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count <> 0 Then
            rapproche.ComboBox3.Items.Clear()
            rapproche.ComboBox3.Items.Add("Tout")
            For i = 0 To table2.Rows.Count - 1
                rapproche.ComboBox3.Items.Add(table2.Rows(i).Item(0))
            Next
            rapproche.ComboBox3.SelectedIndex = 0

        End If
        Panel10.Visible = False
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
    End Sub

    Private Sub IconButton34_Click(sender As Object, e As EventArgs) Handles IconButton34.Click
        rapproche.Show()
        rapproche.Label15.Visible = False
        rapproche.ComboBox4.Visible = False
        Panel10.Visible = False
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
    End Sub

    Private Sub IconButton33_Click(sender As Object, e As EventArgs) Handles IconButton33.Click
        opentr.Show()
        Panel10.Visible = False
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
    End Sub

    Private Sub IconButton22_Click(sender As Object, e As EventArgs) Handles IconButton22.Click
        opentr.Show()
        opentr.Panel6.Visible = True
        Panel10.Visible = False
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
    End Sub

    Private Sub IconButton37_Click(sender As Object, e As EventArgs) Handles IconButton37.Click
        Panel10.Visible = False
        Panel11.Visible = False
        Panel33.Visible = False

        If Panel12.Visible = True Then
            Panel12.Visible = False
        Else
            Panel12.Visible = True
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub
    Dim ADPS As MySqlDataAdapter
    Dim ReportToPrint As LocalReport
    Dim m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)
        Return stream
    End Function
    Private Sub IconButton44_Click(sender As Object, e As EventArgs) Handles IconButton44.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Cloture caisse'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Dim Rep As Integer
                Rep = MsgBox("Voulez-vous vraiment cloturer la caisse ?", vbYesNo)
                If Rep = vbYes Then
                    Try


                        adpt = New MySqlDataAdapter("select * from orders WHERE parked = @day and cloture = @dayo", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@day", "yes")
                        adpt.SelectCommand.Parameters.AddWithValue("@dayo", 0)
                        Dim table1 As New DataTable
                        adpt.Fill(table1)

                        If table1.Rows.Count() = 0 Then

                            Dim dte As String = DateTime.Today.ToString("dd/MM/yyyy")
                            Dim sumord As Double = 0
                            Dim sumfac As Double = 0
                            Dim crdord As Double = 0
                            Dim crdfac As Double = 0
                            Dim sumchq As Double = 0
                            Dim tpeord As Double = 0
                            Dim logi As Double = 0

                            adpt = New MySqlDataAdapter("select MontantOrder from orders WHERE cloture = @day and modePayement = @mode", conn2)
                            adpt.SelectCommand.Parameters.Clear()
                            adpt.SelectCommand.Parameters.AddWithValue("@day", 0)
                            adpt.SelectCommand.Parameters.AddWithValue("@mode", "Espèce")
                            Dim tableespecetick As New DataTable
                            adpt.Fill(tableespecetick)

                            adpt = New MySqlDataAdapter("select montant from reglement WHERE achat IS NULL and `mode` = 'Espèce (Comptoir)' and cloture = @day", conn2)
                            adpt.SelectCommand.Parameters.Clear()
                            adpt.SelectCommand.Parameters.AddWithValue("@day", 0)
                            Dim tableenc As New DataTable
                            adpt.Fill(tableenc)

                            Dim sumenc As Double = 0

                            If tableespecetick.Rows().Count() = 0 Then
                                sumord = 0
                            Else
                                For i = 0 To tableespecetick.Rows().Count() - 1
                                    sumord += Convert.ToDouble(tableespecetick.Rows(i).Item(0).ToString.Replace(" ", "").Replace(".", ","))
                                Next
                            End If
                            If tableenc.Rows().Count() = 0 Then
                                sumenc += 0
                            Else
                                For i = 0 To tableenc.Rows().Count() - 1
                                    sumenc += Convert.ToDouble(tableenc.Rows(i).Item(0).ToString.Replace(" ", "").Replace(".", ","))
                                Next
                            End If


                            adpt = New MySqlDataAdapter("select MontantOrder from orders WHERE cloture = @day and modePayement = @mode", conn2)
                            adpt.SelectCommand.Parameters.Clear()
                            adpt.SelectCommand.Parameters.AddWithValue("@day", 0)

                            adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
                            Dim tablecredittick As New DataTable
                            adpt.Fill(tablecredittick)

                            If tablecredittick.Rows().Count() = 0 Then
                                crdord = 0
                            Else
                                For i = 0 To tablecredittick.Rows().Count() - 1
                                    crdord += Convert.ToDouble(tablecredittick.Rows(i).Item(0).ToString.Replace(".", ","))
                                Next
                            End If

                            adpt = New MySqlDataAdapter("select MontantOrder from orders WHERE cloture = @day and modePayement = @mode", conn2)
                            adpt.SelectCommand.Parameters.Clear()
                            adpt.SelectCommand.Parameters.AddWithValue("@day", 0)
                            adpt.SelectCommand.Parameters.AddWithValue("@mode", "TPE")
                            Dim tabletpetick As New DataTable
                            adpt.Fill(tabletpetick)

                            adpt = New MySqlDataAdapter("select montant from reglement WHERE achat IS NULL and mode = 'TPE' and cloture = @day", conn2)
                            adpt.SelectCommand.Parameters.Clear()
                            adpt.SelectCommand.Parameters.AddWithValue("@day", 0)
                            Dim tabletpe As New DataTable
                            adpt.Fill(tabletpe)

                            If tabletpetick.Rows().Count() = 0 Then
                                tpeord = 0
                            Else
                                For i = 0 To tabletpetick.Rows().Count() - 1
                                    tpeord += Convert.ToDouble(tabletpetick.Rows(i).Item(0).ToString.Replace(".", ","))
                                Next
                            End If

                            If tabletpe.Rows().Count() = 0 Then
                                tpeord += 0
                            Else
                                For i = 0 To tabletpe.Rows().Count() - 1
                                    tpeord += Convert.ToDouble(tabletpe.Rows(i).Item(0).ToString.Replace(".", ","))
                                Next
                            End If

                            adpt = New MySqlDataAdapter("select montant from reglement WHERE achat IS NULL and mode = 'Chèque' and cloture = @day", conn2)
                            adpt.SelectCommand.Parameters.Clear()
                            adpt.SelectCommand.Parameters.AddWithValue("@day", 0)
                            Dim tablechq As New DataTable
                            adpt.Fill(tablechq)

                            For i = 0 To tablechq.Rows.Count() - 1
                                sumchq += Convert.ToDouble(tablechq.Rows(i).Item(0).ToString.Replace(".", ","))

                            Next

                            Dim sum As Double = Convert.ToDouble(sumord) + Convert.ToDouble(sumenc) + Convert.ToDouble(sumchq) + Convert.ToDouble(tpeord)

                            Dim especevar As Double = Convert.ToDouble(sumord)
                            Dim chèquevar As Double = Convert.ToDouble(sumchq)
                            Dim cartevar As Double = Convert.ToDouble(tpeord)
                            Dim creditvar As Double = Convert.ToDouble(crdord)

                            Dim sortievar As Double = 0
                            adpt = New MySqlDataAdapter("select mtn from tiroir WHERE cloture = @day", conn2)
                            adpt.SelectCommand.Parameters.Clear()
                            adpt.SelectCommand.Parameters.AddWithValue("@day", 0)

                            Dim tablesortie As New DataTable
                            adpt.Fill(tablesortie)
                            If tablesortie.Rows.Count() = 0 Then

                                sortievar = 0
                            Else
                                For i = 0 To tablesortie.Rows().Count() - 1
                                    sortievar += Convert.ToDouble(tablesortie.Rows(i).Item(0).ToString.Replace(".", ","))
                                Next

                            End If


                            adpt = New MySqlDataAdapter("SELECT sum(transport), livreur, count(*) FROM orders where cloture = @day and livreur NOT IN ('-', 'Livreur') AND livreur IS NOT NULL GROUP BY livreur", conn2)
                            adpt.SelectCommand.Parameters.Clear()
                            adpt.SelectCommand.Parameters.AddWithValue("@day", 0)

                            Dim tablelivreur As New DataTable
                            adpt.Fill(tablelivreur)
                            Dim sumlivreur As Double = 0
                            For i = 0 To tablelivreur.Rows.Count - 1
                                sumlivreur += Convert.ToDouble(tablelivreur.Rows(i).Item(0).ToString.Replace(".", ","))
                            Next

                            adpt = New MySqlDataAdapter("SELECT Entre, Sortie FROM caisse where Day(date) = @day and Month(date) = @month and Year(date) = @year and Compte = 'Caisse comptoir'", conn2)
                            adpt.SelectCommand.Parameters.Clear()
                            adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Now.Day)
                            adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Now.Month)
                            adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Now.Year)

                            Dim tablecaisse As New DataTable
                            adpt.Fill(tablecaisse)
                            Dim sumcaissein As Double = 0
                            Dim sumcaisseout As Double = 0
                            For i = 0 To tablecaisse.Rows.Count - 1
                                sumcaissein += Convert.ToDouble(tablecaisse.Rows(i).Item(0).ToString.Replace(".", ","))
                                sumcaisseout += Convert.ToDouble(tablecaisse.Rows(i).Item(1).ToString.Replace(".", ","))
                            Next

                            Dim soldevar As Double = Convert.ToDouble(especevar) + Convert.ToDouble(sumenc) + sumcaissein - Convert.ToDouble(sortievar) - Convert.ToDouble(sumlivreur) - sumcaisseout
                            Dim timbrevar As Double = (Convert.ToDouble(especevar) * 0.25) / 100

                            adpt = New MySqlDataAdapter("select * from tiroir WHERE cloture = @day order by date desc ", conn2)
                            adpt.SelectCommand.Parameters.Clear()
                            adpt.SelectCommand.Parameters.AddWithValue("@day", 0)

                            Dim table As New DataTable
                            adpt.Fill(table)

                            Dim ds1 As New DataSet1
                            Dim dt1 As New DataTable
                            dt1 = ds1.Tables("Livreurs")
                            For i = 0 To tablelivreur.Rows().Count() - 1
                                dt1.Rows.Add(tablelivreur.Rows(i).Item(1).ToString.Replace(".", ","), tablelivreur.Rows(i).Item(0).ToString.Replace(".", ","), tablelivreur.Rows(i).Item(2))
                            Next

                            Dim ds2 As New DataSet1
                            Dim dt2 As New DataTable
                            dt2 = ds2.Tables("clot")
                            dt2.Rows.Add("-", "-")

                            Dim ds3 As New DataSet1
                            Dim dt3 As New DataTable
                            dt3 = ds3.Tables("credit")

                            dt3.Rows.Add("-", "-")


                            Dim ds As New DataSet1
                            Dim dt As New DataTable
                            dt = ds.Tables("sortie")
                            For i = 0 To table.Rows().Count() - 1
                                dt.Rows.Add(table.Rows(i).Item(1).ToString.Replace(".", ","), table.Rows(i).Item(2).ToString.Replace(".", ","))
                            Next
                            dt.Rows.Add("Retrait en espèce", sumcaisseout)

                            ReportToPrint = New LocalReport()
                            ReportToPrint.ReportPath = Application.StartupPath + "\etatcaisse.rdlc"
                            ReportToPrint.DataSources.Clear()
                            ReportToPrint.EnableExternalImages = True

                            Dim dateM As New ReportParameter("date", dte)
                            Dim dateM1(0) As ReportParameter
                            dateM1(0) = dateM
                            ReportToPrint.SetParameters(dateM1)

                            Dim total As New ReportParameter("total", sum)
                            Dim total1(0) As ReportParameter
                            total1(0) = total
                            ReportToPrint.SetParameters(total1)

                            Dim journal As New ReportParameter("Journal", "Journal Z")
                            Dim journal1(0) As ReportParameter
                            journal1(0) = journal
                            ReportToPrint.SetParameters(journal1)


                            Dim caissier As New ReportParameter("caissier", "-")
                            Dim caissier1(0) As ReportParameter
                            caissier1(0) = caissier
                            ReportToPrint.SetParameters(caissier1)

                            Dim espece As New ReportParameter("espece", especevar)
                            Dim espece1(0) As ReportParameter
                            espece1(0) = espece
                            ReportToPrint.SetParameters(espece1)

                            Dim chèque As New ReportParameter("chèque", chèquevar)
                            Dim chèque1(0) As ReportParameter
                            chèque1(0) = chèque
                            ReportToPrint.SetParameters(chèque1)

                            Dim alim As New ReportParameter("alim", sumcaissein)
                            Dim alim1(0) As ReportParameter
                            alim1(0) = alim
                            ReportToPrint.SetParameters(alim1)

                            Dim encclient As New ReportParameter("encclient", sumenc)
                            Dim encclient1(0) As ReportParameter
                            encclient1(0) = encclient
                            ReportToPrint.SetParameters(encclient1)

                            Dim carte As New ReportParameter("carte", cartevar)
                            Dim carte1(0) As ReportParameter
                            carte1(0) = carte
                            ReportToPrint.SetParameters(carte1)

                            Dim credit As New ReportParameter("credit", creditvar)
                            Dim credit1(0) As ReportParameter
                            credit1(0) = credit
                            ReportToPrint.SetParameters(credit1)

                            Dim sortie As New ReportParameter("sortie", sortievar + sumcaisseout)
                            Dim sortie1(0) As ReportParameter
                            sortie1(0) = sortie
                            ReportToPrint.SetParameters(sortie1)

                            Dim solde As New ReportParameter("solde", soldevar)
                            Dim solde1(0) As ReportParameter
                            solde1(0) = solde
                            ReportToPrint.SetParameters(solde1)

                            Dim timbre As New ReportParameter("timbre", timbrevar)
                            Dim timbre1(0) As ReportParameter
                            timbre1(0) = timbre
                            ReportToPrint.SetParameters(timbre1)


                            adpt = New MySqlDataAdapter("Select * from infos", conn2)
                            Dim tableimg As New DataTable
                            adpt.Fill(tableimg)
                            Dim appPath As String = Application.StartupPath()

                            Dim SaveDirectory As String = appPath & "\"
                            Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                            Dim img As New ReportParameter("image", "File:\" + SavePath, True)
                            Dim img1(0) As ReportParameter
                            img1(0) = img
                            ReportToPrint.SetParameters(img1)

                            ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt1))
                            ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dt))
                            ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", dt2))
                            ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet4", dt3))



                            Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>3.7in</PageWidth><PageHeight>12in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
                            Dim warnings As Warning()
                            m_streams = New List(Of Stream)()
                            ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
                            For Each stream As Stream In m_streams
                                stream.Position = 0
                            Next
                            Dim printDoc As New PrintDocument()
                            Dim printname As String = receiptprinter
                            printDoc.PrinterSettings.PrinterName = printname
                            Dim ps As New PrinterSettings()
                            ps.PrinterName = printDoc.PrinterSettings.PrinterName
                            printDoc.PrinterSettings = ps

                            AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
                            m_currentPageIndex = 0
                            printDoc.Print()


                            conn2.Close()
                            conn2.Open()
                            adpt = New MySqlDataAdapter("select * from archivecaisse order by id desc limit 1", conn2)
                            Dim tablelastarch As New DataTable
                            adpt.Fill(tablelastarch)

                            Dim sql2 As String
                            Dim cmd2 As New MySqlCommand
                            sql2 = "UPDATE `archivecaisse` SET `systeme` = @v1, `caisse` = @v2,  `ecart` = @v4, `cloture` = 1 where id = '" & tablelastarch.Rows(0).Item(0) & "'"
                            cmd2 = New MySqlCommand(sql2, conn2)
                            cmd2.Parameters.AddWithValue("@v1", soldevar)
                            cmd2.Parameters.AddWithValue("@v2", Convert.ToDouble(tablelastarch.Rows(0).Item(3).ToString.Replace(" ", "").Replace(".", ",")).ToString("# ##0.00"))
                            cmd2.Parameters.AddWithValue("@v4", Convert.ToDouble(tablelastarch.Rows(0).Item(3).ToString.Replace(" ", "").Replace(".", ",") - soldevar).ToString("# ##0.00"))

                            cmd2.ExecuteNonQuery()

                            sql2 = "UPDATE `orders` SET `cloture`= 1 WHERE cloture = 0 "
                            cmd2 = New MySqlCommand(sql2, conn2)
                            cmd2.ExecuteNonQuery()

                            sql2 = "UPDATE `reglement` SET `cloture`= 1 WHERE cloture = 0 "
                            cmd2 = New MySqlCommand(sql2, conn2)
                            cmd2.ExecuteNonQuery()

                            sql2 = "UPDATE `tiroir` SET `cloture`= 1 WHERE cloture = 0 "
                            cmd2 = New MySqlCommand(sql2, conn2)
                            cmd2.ExecuteNonQuery()

                            sql2 = "DELETE FROM `archivecaisse` WHERE cloture = 0 "
                            cmd2 = New MySqlCommand(sql2, conn2)
                            cmd2.ExecuteNonQuery()


                            sql2 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                            cmd2 = New MySqlCommand(sql2, conn2)
                            cmd2.Parameters.Clear()
                            cmd2.Parameters.AddWithValue("@name", user)
                            cmd2.Parameters.AddWithValue("@op", "Cloture de caisse (Z)")
                            cmd2.ExecuteNonQuery()

                            conn2.Close()

                            MsgBox("Caisse cloturée !")

                        Else
                            MsgBox("Veuillez vider votre Park de commande !")
                        End If


                    Catch ex As MySqlException
                        MsgBox(ex.Message)
                    End Try
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

    Private Sub IconButton45_Click(sender As Object, e As EventArgs) Handles IconButton45.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Opérations monetaires'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Panel13.Visible = True
                Panel10.Visible = False
                Panel11.Visible = False
                Panel12.Visible = False
                Panel33.Visible = False


                adpt = New MySqlDataAdapter("select * from banques", conn2)
                Dim table As New DataTable
                adpt.Fill(table)

                adpt = New MySqlDataAdapter("select * from caisses", conn2)
                Dim table2 As New DataTable
                adpt.Fill(table2)

                ComboBox4.Items.Clear()
                ComboBox1.Items.Clear()
                For i = 0 To table.Rows.Count() - 1
                    ComboBox4.Items.Add(table.Rows(i).Item(1))
                    ComboBox1.Items.Add(table.Rows(i).Item(1))
                Next
                For j = 0 To table2.Rows.Count() - 1
                    ComboBox4.Items.Add(table2.Rows(j).Item(1))
                    ComboBox1.Items.Add(table2.Rows(j).Item(1))
                Next
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton47_Click(sender As Object, e As EventArgs) Handles IconButton47.Click
        Panel13.Visible = False
    End Sub

    Private Sub IconButton46_Click(sender As Object, e As EventArgs) Handles IconButton46.Click
        If ComboBox4.Text <> "" AndAlso ComboBox1.Text <> "" AndAlso TextBox24.Text <> "" AndAlso TextBox24.Text <> 0 Then
            Dim sql3 As String
            Dim cmd3 As MySqlCommand
            conn2.Close()
            conn2.Open()

            adpt = New MySqlDataAdapter("select id_op from caisse order by id desc limit 1", conn2)
            Dim table As New DataTable
            adpt.Fill(table)

            Dim ope As Integer
            If table.Rows.Count <> 0 Then
                ope = table.Rows(0).Item(0) + 1
            Else
                ope = 1
            End If

            sql3 = "INSERT INTO `caisse`(`Entre`, `Compte`,`id_op`,`date`) VALUES ('" & TextBox24.Text.Replace(" ", "").Replace(".", ",") & "','" & ComboBox1.Text & "','" & ope & "','" & DateTimePicker3.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.ExecuteNonQuery()

            sql3 = "INSERT INTO `caisse`(`Sortie`, `Compte`,`id_op`,`date`) VALUES ('" & TextBox24.Text.Replace(" ", "").Replace(".", ",") & "','" & ComboBox4.Text & "','" & ope & "','" & DateTimePicker3.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.ExecuteNonQuery()
            TextBox24.Text = ""
            conn2.Close()
            MsgBox("Opération bien faite !")
            ComboBox4.Text = ""
            ComboBox1.Text = ""
            TextBox24.Text = ""
        Else
            MsgBox("Veuillez remplir tous les champs !")
            ComboBox4.Text = ""
            ComboBox1.Text = ""
            TextBox24.Text = ""
        End If

    End Sub

    Private Sub IconButton39_Click(sender As Object, e As EventArgs) Handles IconButton39.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Statistiques CA Clients'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                statistique.ShowDialog()
                Panel10.Visible = False
                Panel11.Visible = False
                Panel12.Visible = False
                Panel33.Visible = False
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton16_Click(sender As Object, e As EventArgs) Handles IconButton16.Click
        plan.ShowDialog()
    End Sub

    Private Sub IconButton40_Click(sender As Object, e As EventArgs) Handles IconButton40.Click

        statistique.Show()
        statistique.Panel9.Visible = True
        adpt = New MySqlDataAdapter("select * from inventaire_list", conn2)
        Dim tableinv As New DataTable
        adpt.Fill(tableinv)
        statistique.DataGridView2.Rows.Clear()
        If tableinv.Rows.Count <> 0 Then
            For i = 0 To tableinv.Rows.Count - 1
                statistique.DataGridView2.Rows.Add(Convert.ToDateTime(tableinv.Rows(i).Item(1)).ToString("dd/MM/yyyy"), Convert.ToDouble(tableinv.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(tableinv.Rows(i).Item(3).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), tableinv.Rows(i).Item(4), tableinv.Rows(i).Item(0))
            Next
        End If
        statistique.Label16.Text = "Inventaire"
        Panel10.Visible = False
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
    End Sub

    Private Sub IconButton42_Click(sender As Object, e As EventArgs) Handles IconButton42.Click
        If Panel18.Visible = False Then
            Panel18.Visible = True
        Else
            Panel18.Visible = False
        End If

    End Sub

    Private Sub IconButton53_Click(sender As Object, e As EventArgs) Handles IconButton53.Click
        alertes.Show()
        adpt1 = New MySqlDataAdapter("select * from cheques WHERE etat = @day and organisme <> @org", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", 0)
        adpt1.SelectCommand.Parameters.AddWithValue("@org", "tpe")
        Dim table1 As New DataTable
        adpt1.Fill(table1)
        Dim chqcount1 As Double = 0
        Dim chqcount2 As Double = 0
        alertes.DataGridView2.Rows.Clear()
        For i = 0 To table1.Rows.Count() - 1
            Dim ech As String = DateDiff(DateInterval.Day, Now.Date, table1.Rows(i).Item(5))

            If IsDBNull(table1.Rows(i).Item(8)) Then
                If ech <= 14 Then
                    alertes.DataGridView2.Rows.Add(table1.Rows(i).Item(0), "CHQ N° " & table1.Rows(i).Item(4), Convert.ToDouble(table1.Rows(i).Item(12).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDateTime(table1.Rows(i).Item(5)).ToString("yyyy/MM/dd"), ech & " Jrs", table1.Rows(i).Item(10), table1.Rows(i).Item(14))
                End If

            End If
        Next

    End Sub

    Private Sub IconButton52_Click(sender As Object, e As EventArgs) Handles IconButton52.Click
        alertes.Show()
        adpt1 = New MySqlDataAdapter("select * from cheques WHERE etat = @day and organisme <> @org", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", 0)
        adpt1.SelectCommand.Parameters.AddWithValue("@org", "tpe")
        Dim table1 As New DataTable
        adpt1.Fill(table1)
        Dim chqcount1 As Double = 0
        Dim chqcount2 As Double = 0
        alertes.DataGridView1.Rows.Clear()
        alertes.DataGridView2.Visible = False
        alertes.Label11.Text = "Chèques reglés"
        For i = 0 To table1.Rows.Count() - 1
            Dim ech As String = DateDiff(DateInterval.Day, Now.Date, table1.Rows(i).Item(5))

            If IsDBNull(table1.Rows(i).Item(8)) Then
            Else

                If ech <= 14 Then
                    alertes.DataGridView1.Rows.Add(table1.Rows(i).Item(0), "CHQ N° " & table1.Rows(i).Item(4), Convert.ToDouble(table1.Rows(i).Item(12).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDateTime(table1.Rows(i).Item(5)).ToString("yyyy/MM/dd"), ech & " Jrs", table1.Rows(i).Item(10), table1.Rows(i).Item(14))
                End If

            End If
        Next
    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub IconButton19_Click_1(sender As Object, e As EventArgs) Handles IconButton19.Click
        If Panel33.Visible = False Then

            Panel33.Visible = True
        Else
            Panel33.Visible = False
        End If
    End Sub

    Private Sub IconButton48_Click(sender As Object, e As EventArgs) Handles IconButton48.Click
        opentr.Show()
        opentr.Panel8.Visible = True
        adpt = New MySqlDataAdapter("select * from clients where remise > 0 order by client asc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double
        opentr.DataGridView4.Rows.Clear()
        For i = 0 To table.Rows.Count() - 1
            opentr.DataGridView4.Rows.Add(table.Rows(i).Item(1), table.Rows(i).Item(22), "Valider")
        Next
        Panel10.Visible = False
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
    End Sub

    Private Sub IconButton51_Click(sender As Object, e As EventArgs) Handles IconButton51.Click
        opentr.Show()
        opentr.Panel8.Visible = True
        opentr.Panel9.Visible = True
        Dim rmsfac As Double = 0
        Dim rmstir As Double = 0
        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`remise`,',','.'),' ','')), client from facture where remise > 0 Group by client order by client asc ", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        opentr.DataGridView5.Rows.Clear()
        For i = 0 To table.Rows.Count() - 1
            adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`mtn`,',','.'),' ','')) from tiroir where cause = 'Remise client' and remarque = '" & table.Rows(i).Item(1) & "' ", conn2)
            Dim table2 As New DataTable
            adpt.Fill(table2)
            If table2.Rows.Count <> 0 Then
                opentr.DataGridView5.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(table.Rows(i).Item(0).ToString.Replace(".", ",") + table2.Rows(0).Item(0).ToString.Replace(".", ",")).ToString("N2"))
                rmsfac = rmsfac + Convert.ToDouble(table.Rows(i).Item(0).ToString.Replace(".", ",") + table2.Rows(0).Item(0).ToString.Replace(".", ","))
            Else
                opentr.DataGridView5.Rows.Add(table.Rows(i).Item(1), Convert.ToDouble(table.Rows(i).Item(0).ToString.Replace(".", ",")).ToString("N2"))
                rmsfac = rmsfac + Convert.ToDouble(table.Rows(i).Item(0).ToString.Replace(".", ","))

            End If
        Next
        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`mtn`,',','.'),' ','')), remarque from tiroir where cause = 'Remise client' Group by remarque order by remarque asc ", conn2)
        Dim table3 As New DataTable
        adpt.Fill(table3)

        For i = 0 To table3.Rows.Count() - 1
            adpt = New MySqlDataAdapter("select * from facture where remise > 0 and client = '" & table3.Rows(i).Item(1) & "' ", conn2)
            Dim table2 As New DataTable
            adpt.Fill(table2)
            If table2.Rows.Count <> 0 Then
            Else
                opentr.DataGridView5.Rows.Add(table3.Rows(i).Item(1), Convert.ToDouble(table3.Rows(i).Item(0).ToString.Replace(".", ",")).ToString("N2"))
                rmstir = rmstir + Convert.ToDouble(table3.Rows(i).Item(0).ToString.Replace(".", ","))
            End If
        Next


        opentr.TextBox5.Text = (rmsfac + rmstir).ToString("N2")
        Panel10.Visible = False
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
    End Sub

    Private Sub IconButton41_Click(sender As Object, e As EventArgs) Handles IconButton41.Click
        Panel10.Visible = False
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
    End Sub

    Private Sub IconButton30_Click(sender As Object, e As EventArgs) Handles IconButton30.Click
        Panel10.Visible = False
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
    End Sub

    Private Sub IconButton43_Click(sender As Object, e As EventArgs) Handles IconButton43.Click
        If Panel34.Visible = False Then

            Panel34.Visible = True
            ComboBox2.Text = DateTime.Now.Year
            ComboBox3.Text = DateTime.Now.Month
        Else
            Panel34.Visible = False
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        adpt = New MySqlDataAdapter("select * from cheques where fac IS NULL and tick IS NULL and etat = '0' and Month(echeance) = '" & ComboBox3.Text & "' order by echeance desc", conn2)
        adpt.SelectCommand.Parameters.Clear()
        Dim table As New DataTable
        adpt.Fill(table)
        DataGridView2.Rows.Clear()
        For i = 0 To table.Rows.Count() - 1
            DataGridView2.Rows.Add(Convert.ToDateTime(table.Rows(i).Item(5)).ToString("dd/MM/yyyy"), table.Rows(i).Item(14), Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ",")).ToString("N2"))
        Next
    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        Dim Debut As DateTime = DateTimePicker1.Value
        Dim Fin As DateTime = DateTimePicker2.Value

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(MontantOrder,',','.'),' ','')) from orders WHERE OrderDate BETWEEN @debut and @fin and parked = 'no'", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@debut", Convert.ToDateTime(Debut).ToString("yyyy-MM-dd"))
        adpt1.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(Fin.AddDays(1)).ToString("yyyy-MM-dd"))
        Dim tableorder As New DataTable
        adpt1.Fill(tableorder)
        Dim camois As Double = 0
        Dim cayear As Double = 0
        Dim creord As Double = 0
        If IsDBNull(tableorder.Rows(0).Item(0)) Then
        Else
            camois = Convert.ToDouble(tableorder.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
        End If

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(marge,',','.'),' ','')) from orderdetails WHERE date between @debut and @fin", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@debut", Convert.ToDateTime(Debut).ToString("yyyy-MM-dd"))
        adpt1.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(Fin.AddDays(1)).ToString("yyyy-MM-dd"))
        Dim tablemargemois As New DataTable
        adpt1.Fill(tablemargemois)

        Dim margemois As Double = 0


        If IsDBNull(tablemargemois.Rows(0).Item(0)) Then
        Else
            margemois = Convert.ToDouble(tablemargemois.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
        End If


        Dim chargyear As Double = 0

        adpt1 = New MySqlDataAdapter("SELECT SUM(REPLACE(REPLACE(j,',','.'),' ','') + REPLACE(REPLACE(f,',','.'),' ','') + REPLACE(REPLACE(m,',','.'),' ','') +REPLACE(REPLACE(a,',','.'),' ','') +REPLACE(REPLACE(mai,',','.'),' ','') +REPLACE(REPLACE(juin,',','.'),' ','') +REPLACE(REPLACE(juil,',','.'),' ','') +REPLACE(REPLACE(ao,',','.'),' ','') +REPLACE(REPLACE(sept,',','.'),' ','') +REPLACE(REPLACE(oct,',','.'),' ','') +REPLACE(REPLACE(nov,',','.'),' ','') +REPLACE(REPLACE(dece,',','.'),' ','') ) FROM charges WHERE date between @debut and @fin", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@debut", Convert.ToDateTime(Debut).ToString("yyyy-MM-dd"))
        adpt1.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(Fin.AddDays(1)).ToString("yyyy-MM-dd"))
        Dim tableorder3 As New DataTable
        adpt1.Fill(tableorder3)
        If IsDBNull(tableorder3.Rows(0).Item(0)) Then
        Else
            chargyear = Convert.ToDouble(tableorder3.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If


        Dim rmsmois As Double = 0

        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`remise`,',','.'),' ','')) from facture where remise > 0 and OrderDate between @debut and @fin", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@debut", Convert.ToDateTime(Debut).ToString("yyyy-MM-dd"))
        adpt.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(Fin.AddDays(1)).ToString("yyyy-MM-dd"))
        Dim tablermsmois As New DataTable
        adpt.Fill(tablermsmois)
        If IsDBNull(tablermsmois.Rows(0).Item(0)) Then
        Else
            rmsmois = Convert.ToDouble(tablermsmois.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If



        Dim demarqyear As Double = 0

        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`valeur`,',','.'),' ','')) from demarque where date between @debut and @fin", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@debut", Convert.ToDateTime(Debut).ToString("yyyy-MM-dd"))
        adpt.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(Fin.AddDays(1)).ToString("yyyy-MM-dd"))
        Dim tabledemarqyear As New DataTable
        adpt.Fill(tabledemarqyear)
        If IsDBNull(tabledemarqyear.Rows(0).Item(0)) Then
        Else
            demarqyear = Convert.ToDouble(tabledemarqyear.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        Dim dividyear As Double = 0

        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`montant`,',','.'),' ','')) from dividendes where date between @debut and @fin", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@debut", Convert.ToDateTime(Debut).ToString("yyyy-MM-dd"))
        adpt.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(Fin.AddDays(1)).ToString("yyyy-MM-dd"))
        Dim tabledividyear As New DataTable
        adpt.Fill(tabledividyear)
        If IsDBNull(tabledividyear.Rows(0).Item(0)) Then
        Else
            dividyear = Convert.ToDouble(tabledividyear.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If


        Label15.Text = camois.ToString("N2")
        Label19.Text = Convert.ToDouble(margemois - chargyear - rmsmois - demarqyear - dividyear).ToString("N2")
        Label24.Text = cayear.ToString("N2")
        Label18.Text = margemois.ToString("N2")
        Label22.Visible = False
        Label20.Visible = False
        Label3.Visible = False
        Label17.Visible = False
        Label25.Visible = False
        Label21.Visible = False
        IconButton57.Visible = False

        Dim ca1 As Double = 0
        Dim ca2 As Double = 0
        Dim ca3 As Double = 0
        Dim ca4 As Double = 0
        Dim ca5 As Double = 0
        Dim ca6 As Double = 0
        Dim ca7 As Double = 0
        Dim ca8 As Double = 0
        Dim ca9 As Double = 0
        Dim ca10 As Double = 0
        Dim ca11 As Double = 0
        Dim ca12 As Double = 0

        adpt1 = New MySqlDataAdapter("SELECT DATE_FORMAT(OrderDate, '%m'), SUM(REPLACE(REPLACE(MontantOrder,',','.'),' ','')) FROM orders WHERE OrderDate between @debut and @fin and parked = 'no' GROUP BY DATE_FORMAT(OrderDate, '%m') ORDER BY DATE_FORMAT(OrderDate, '%m')", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@debut", Convert.ToDateTime(Debut).ToString("yyyy-MM-dd"))
        adpt1.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(Fin.AddDays(1)).ToString("yyyy-MM-dd"))
        Dim tablecamens As New DataTable
        adpt1.Fill(tablecamens)
        For i = 0 To tablecamens.Rows.Count - 1
            If tablecamens.Rows(i).Item(0) = "01" Then
                ca1 = ca1 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "02" Then
                ca2 = ca2 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "03" Then
                ca3 = ca3 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "04" Then
                ca4 = ca4 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "05" Then
                ca5 = ca5 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "06" Then
                ca6 = ca6 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "07" Then
                ca7 = ca7 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "08" Then
                ca8 = ca8 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "09" Then
                ca9 = ca9 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "10" Then
                ca10 = ca10 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "11" Then
                ca11 = ca11 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "12" Then
                ca12 = ca12 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If

        Next

        Dim chiffreAffaires() As Double = {ca1, ca2, ca3, ca4, ca5, ca6, ca7, ca8, ca9, ca10, ca11, ca12}

        ' Définissez les mois sous forme d'abréviations
        Dim mois As String() = {"M1", "M2", "M3", "M4", "M5", "M6", "M7", "M8", "M9", "M10", "M11", "M12"}

        ' Associez les valeurs du chiffre d'affaires aux mois
        Chart1.Series(0).Points.DataBindY(chiffreAffaires)
        Chart1.ChartAreas(0).AxisX.Interval = 1
        ' Définissez les étiquettes des mois pour l'axe horizontal
        Chart1.ChartAreas(0).AxisX.CustomLabels.Clear()
        For i As Integer = 0 To mois.Length - 1
            Chart1.ChartAreas(0).AxisX.CustomLabels.Add(i + 0.5, i + 1.5, mois(i))
        Next i




        adpt = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(PA_TTC,',','.'),' ','') * REPLACE(REPLACE(Stock,',','.'),' ','')) from article", conn2)
        Dim tablestock As New DataTable
        adpt.Fill(tablestock)

        adpt = New MySqlDataAdapter("select ttc from inventaire_list ORDER BY ID DESC", conn2)
        Dim tableinv As New DataTable
        adpt.Fill(tableinv)

        Dim stockfin, stockdebut As Double
        If IsDBNull(tablestock.Rows(0).Item(0)) Then
            stockfin = 0
        Else
            stockfin = Convert.ToDouble(tablestock.Rows(0).Item(0).ToString.Replace(".", ","))

        End If

        If IsDBNull(tableinv.Rows(0).Item(0)) Then
            stockdebut = 0
        Else
            stockdebut = Convert.ToDouble(tableinv.Rows(0).Item(0).ToString.Replace(".", ","))

        End If

        Dim variation As Double = (stockdebut + stockfin) / 2
        Label30.Text = stockfin.ToString("N2")
        Label28.Text = Convert.ToDouble(cayear / variation).ToString("N0")

    End Sub

    Private Sub IconButton56_Click(sender As Object, e As EventArgs) Handles IconButton56.Click
        Label22.Visible = True
        Label20.Visible = True
        Label3.Visible = True
        Label17.Visible = True
        Label25.Visible = True
        Label21.Visible = True
        IconButton57.Visible = True
        Dim currentDate As DateTime = DateTime.Now
        Dim previousMonth As Integer = currentDate.Month - 1

        Dim previousDay As DateTime = DateTime.Now.AddDays(-1)

        If previousMonth = 0 Then
            previousMonth = 12
        End If

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(MontantOrder,',','.'),' ','')) from orders WHERE Month(OrderDate) = @day and parked = 'no'", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Month)
        Dim tableorder As New DataTable
        adpt1.Fill(tableorder)
        Dim camois As Double = 0
        Dim camoisprev As Double = 0
        Dim cayear As Double = 0
        Dim creord As Double = 0
        If IsDBNull(tableorder.Rows(0).Item(0)) Then
        Else
            camois = Convert.ToDouble(tableorder.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
        End If

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(MontantOrder,',','.'),' ','')) from orders WHERE Month(OrderDate) = @day and parked = 'no'", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", previousMonth)
        Dim tableorderprev As New DataTable
        adpt1.Fill(tableorderprev)

        If IsDBNull(tableorderprev.Rows(0).Item(0)) Then
        Else
            camoisprev = Convert.ToDouble(tableorderprev.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
        End If

        Dim evol As Double = 0

        evol = ((camois - camoisprev) / camoisprev) * 100

        IconButton57.Text = evol.ToString("N0") & "%"
        If evol < 0 Then
            IconButton57.ForeColor = Color.Red
            IconButton57.IconChar = FontAwesome.Sharp.IconChar.ArrowDown
            IconButton57.IconColor = Color.Red
        Else
            IconButton57.ForeColor = Color.Green
            IconButton57.IconChar = FontAwesome.Sharp.IconChar.ArrowUp
            IconButton57.IconColor = Color.Green
        End If

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(MontantOrder,',','.'),' ','')) from orders WHERE Year(OrderDate) = @day and parked = 'no'", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        Dim tableorder2 As New DataTable
        adpt1.Fill(tableorder2)
        If IsDBNull(tableorder2.Rows(0).Item(0)) Then
        Else
            cayear = Convert.ToDouble(tableorder2.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
        End If

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(marge,',','.'),' ','')) from orderdetails WHERE Month(date) = @day and Year(date) = @year", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Month)
        adpt1.SelectCommand.Parameters.AddWithValue("@year", currentDate.Year)
        Dim tablemargemois As New DataTable
        adpt1.Fill(tablemargemois)

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(marge,',','.'),' ','')) from orderdetails WHERE Month(date) = @mnt and Day(date) = @day and Year(date) = @year", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@mnt", currentDate.Month)
        adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Day)
        adpt1.SelectCommand.Parameters.AddWithValue("@year", currentDate.Year)
        Dim tablemargejour As New DataTable
        adpt1.Fill(tablemargejour)


        Dim margemois As Double = 0
        Dim margejour As Double = 0
        Dim margeyear As Double = 0
        Dim margenmois As Double = 0

        If IsDBNull(tablemargemois.Rows(0).Item(0)) Then
        Else
            margemois = Convert.ToDouble(tablemargemois.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
        End If

        If IsDBNull(tablemargejour.Rows(0).Item(0)) Then
        Else
            margejour = Convert.ToDouble(tablemargejour.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If


        Dim chargyear As Double = 0

        adpt1 = New MySqlDataAdapter("SELECT SUM(REPLACE(REPLACE(j,',','.'),' ','') + REPLACE(REPLACE(f,',','.'),' ','') + REPLACE(REPLACE(m,',','.'),' ','') +REPLACE(REPLACE(a,',','.'),' ','') +REPLACE(REPLACE(mai,',','.'),' ','') +REPLACE(REPLACE(juin,',','.'),' ','') +REPLACE(REPLACE(juil,',','.'),' ','') +REPLACE(REPLACE(ao,',','.'),' ','') +REPLACE(REPLACE(sept,',','.'),' ','') +REPLACE(REPLACE(oct,',','.'),' ','') +REPLACE(REPLACE(nov,',','.'),' ','') +REPLACE(REPLACE(dece,',','.'),' ','') ) FROM charges WHERE year = @day", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        Dim tableorder3 As New DataTable
        adpt1.Fill(tableorder3)
        If IsDBNull(tableorder3.Rows(0).Item(0)) Then
        Else
            chargyear = Convert.ToDouble(tableorder3.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(marge,',','.'),' ','')) from orderdetails WHERE Year(date) = @day", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        Dim tablemargeyear As New DataTable
        adpt1.Fill(tablemargeyear)

        If IsDBNull(tablemargeyear.Rows(0).Item(0)) Then
        Else
            margeyear = Convert.ToDouble(tablemargeyear.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
        End If

        Dim ch1 As Double = 0
        Dim ch2 As Double = 0
        Dim ch3 As Double = 0
        Dim ch4 As Double = 0
        Dim ch5 As Double = 0
        Dim ch6 As Double = 0
        Dim ch7 As Double = 0
        Dim ch8 As Double = 0
        Dim ch9 As Double = 0
        Dim ch10 As Double = 0
        Dim ch11 As Double = 0
        Dim ch12 As Double = 0


        adpt1 = New MySqlDataAdapter("SELECT SUM(REPLACE(REPLACE(j,',','.'),' ','')) ,SUM(REPLACE(REPLACE(f,',','.'),' ','')) ,SUM(REPLACE(REPLACE(m,',','.'),' ','')) ,SUM(REPLACE(REPLACE(a,',','.'),' ','')) ,SUM(REPLACE(REPLACE(mai,',','.'),' ','')) ,SUM(REPLACE(REPLACE(juin,',','.'),' ','')) ,SUM(REPLACE(REPLACE(juil,',','.'),' ','')) ,SUM(REPLACE(REPLACE(ao,',','.'),' ','')) ,SUM(REPLACE(REPLACE(sept,',','.'),' ','')) ,SUM(REPLACE(REPLACE(oct,',','.'),' ','')) ,SUM(REPLACE(REPLACE(nov,',','.'),' ','')) ,SUM(REPLACE(REPLACE(dece,',','.'),' ','')) FROM charges WHERE year = @day;", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        Dim tablechamens As New DataTable
        adpt1.Fill(tablechamens)
        If IsDBNull(tablechamens.Rows(0).Item(currentDate.Month - 1)) Then
        Else
            ch1 = ch1 + Convert.ToDouble(tablechamens.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
            ch2 = ch2 + Convert.ToDouble(tablechamens.Rows(0).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            ch3 = ch3 + Convert.ToDouble(tablechamens.Rows(0).Item(2).ToString.Replace(" ", "").Replace(".", ","))
            ch4 = ch4 + Convert.ToDouble(tablechamens.Rows(0).Item(3).ToString.Replace(" ", "").Replace(".", ","))
            ch5 = ch5 + Convert.ToDouble(tablechamens.Rows(0).Item(4).ToString.Replace(" ", "").Replace(".", ","))
            ch6 = ch6 + Convert.ToDouble(tablechamens.Rows(0).Item(5).ToString.Replace(" ", "").Replace(".", ","))
            ch7 = ch7 + Convert.ToDouble(tablechamens.Rows(0).Item(6).ToString.Replace(" ", "").Replace(".", ","))
            ch8 = ch8 + Convert.ToDouble(tablechamens.Rows(0).Item(7).ToString.Replace(" ", "").Replace(".", ","))
            ch9 = ch9 + Convert.ToDouble(tablechamens.Rows(0).Item(8).ToString.Replace(" ", "").Replace(".", ","))
            ch10 = ch10 + Convert.ToDouble(tablechamens.Rows(0).Item(9).ToString.Replace(" ", "").Replace(".", ","))
            ch11 = ch11 + Convert.ToDouble(tablechamens.Rows(0).Item(10).ToString.Replace(" ", "").Replace(".", ","))
            ch12 = ch12 + Convert.ToDouble(tablechamens.Rows(0).Item(11).ToString.Replace(" ", "").Replace(".", ","))
        End If


        If currentDate.Month = "01" Then
            margenmois = ch1
        End If
        If currentDate.Month = "02" Then
            margenmois = ch2
        End If
        If currentDate.Month = "03" Then
            margenmois = ch3
        End If
        If currentDate.Month = "04" Then
            margenmois = ch4
        End If
        If currentDate.Month = "05" Then
            margenmois = ch5
        End If
        If currentDate.Month = "06" Then
            margenmois = ch6
        End If
        If currentDate.Month = "07" Then
            margenmois = ch7
        End If
        If currentDate.Month = "08" Then
            margenmois = ch8
        End If
        If currentDate.Month = "09" Then
            margenmois = ch9
        End If
        If currentDate.Month = "10" Then
            margenmois = ch10
        End If
        If currentDate.Month = "11" Then
            margenmois = ch11
        End If
        If currentDate.Month = "12" Then
            margenmois = ch12
        End If

        Dim rmsyear As Double = 0
        Dim rmsmois As Double = 0

        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`remise`,',','.'),' ','')) from facture where remise > 0 and Year(OrderDate) = @day", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        Dim tablermsyear As New DataTable
        adpt.Fill(tablermsyear)

        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`remise`,',','.'),' ','')) from facture where remise > 0 and Year(OrderDate) = @day and month(OrderDate) = @mois", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        adpt.SelectCommand.Parameters.AddWithValue("@mois", currentDate.Month)
        Dim tablermsmois As New DataTable
        adpt.Fill(tablermsmois)
        If IsDBNull(tablermsyear.Rows(0).Item(0)) Then
        Else
            rmsyear = Convert.ToDouble(tablermsyear.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If
        If IsDBNull(tablermsmois.Rows(0).Item(0)) Then
        Else
            rmsmois = Convert.ToDouble(tablermsmois.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If


        Dim demarqmois As Double = 0
        Dim demarqyear As Double = 0

        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`valeur`,',','.'),' ','')) from demarque where Year(date) = @day and month(date) = @mois", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        adpt.SelectCommand.Parameters.AddWithValue("@mois", currentDate.Month)
        Dim tabledemarqmois As New DataTable
        adpt.Fill(tabledemarqmois)
        If IsDBNull(tabledemarqmois.Rows(0).Item(0)) Then
        Else
            demarqmois = Convert.ToDouble(tabledemarqmois.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`valeur`,',','.'),' ','')) from demarque where Year(date) = @day", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        Dim tabledemarqyear As New DataTable
        adpt.Fill(tabledemarqyear)
        If IsDBNull(tabledemarqyear.Rows(0).Item(0)) Then
        Else
            demarqyear = Convert.ToDouble(tabledemarqyear.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        Dim dividmois As Double = 0
        Dim dividyear As Double = 0

        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`montant`,',','.'),' ','')) from dividendes where Year(date) = @day and month(date) = @mois", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        adpt.SelectCommand.Parameters.AddWithValue("@mois", currentDate.Month)
        Dim tabledividmois As New DataTable
        adpt.Fill(tabledividmois)
        If IsDBNull(tabledividmois.Rows(0).Item(0)) Then
        Else
            dividmois = Convert.ToDouble(tabledividmois.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        adpt = New MySqlDataAdapter("select sum(Replace(REPLACE(`montant`,',','.'),' ','')) from dividendes where Year(date) = @day", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        Dim tabledividyear As New DataTable
        adpt.Fill(tabledividyear)
        If IsDBNull(tabledividyear.Rows(0).Item(0)) Then
        Else
            dividyear = Convert.ToDouble(tabledividyear.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If


        Label15.Text = camois.ToString("N2")
        Label20.Text = Convert.ToDouble(margeyear - chargyear - rmsyear - demarqmois - dividmois).ToString("N2")
        Label19.Text = Convert.ToDouble(margemois - margenmois - rmsmois - demarqyear - dividyear).ToString("N2")
        Label24.Text = cayear.ToString("N2")
        Label18.Text = margemois.ToString("N2")
        Label21.Text = margejour.ToString("N2")

        Dim ca1 As Double = 0
        Dim ca2 As Double = 0
        Dim ca3 As Double = 0
        Dim ca4 As Double = 0
        Dim ca5 As Double = 0
        Dim ca6 As Double = 0
        Dim ca7 As Double = 0
        Dim ca8 As Double = 0
        Dim ca9 As Double = 0
        Dim ca10 As Double = 0
        Dim ca11 As Double = 0
        Dim ca12 As Double = 0

        adpt1 = New MySqlDataAdapter("SELECT DATE_FORMAT(OrderDate, '%m'), SUM(REPLACE(REPLACE(MontantOrder,',','.'),' ','')) FROM orders WHERE Year(OrderDate) = @day and parked = 'no' GROUP BY DATE_FORMAT(OrderDate, '%m') ORDER BY DATE_FORMAT(OrderDate, '%m')", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", currentDate.Year)
        Dim tablecamens As New DataTable
        adpt1.Fill(tablecamens)
        For i = 0 To tablecamens.Rows.Count - 1
            If tablecamens.Rows(i).Item(0) = "01" Then
                ca1 = ca1 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "02" Then
                ca2 = ca2 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "03" Then
                ca3 = ca3 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "04" Then
                ca4 = ca4 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "05" Then
                ca5 = ca5 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "06" Then
                ca6 = ca6 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "07" Then
                ca7 = ca7 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "08" Then
                ca8 = ca8 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "09" Then
                ca9 = ca9 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "10" Then
                ca10 = ca10 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "11" Then
                ca11 = ca11 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If
            If tablecamens.Rows(i).Item(0) = "12" Then
                ca12 = ca12 + Convert.ToDouble(tablecamens.Rows(i).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            End If

        Next

        Dim chiffreAffaires() As Double = {ca1, ca2, ca3, ca4, ca5, ca6, ca7, ca8, ca9, ca10, ca11, ca12}

        ' Définissez les mois sous forme d'abréviations
        Dim mois As String() = {"M1", "M2", "M3", "M4", "M5", "M6", "M7", "M8", "M9", "M10", "M11", "M12"}

        ' Associez les valeurs du chiffre d'affaires aux mois
        Chart1.Series(0).Points.DataBindY(chiffreAffaires)
        Chart1.ChartAreas(0).AxisX.Interval = 1
        ' Définissez les étiquettes des mois pour l'axe horizontal
        Chart1.ChartAreas(0).AxisX.CustomLabels.Clear()
        For i As Integer = 0 To mois.Length - 1
            Chart1.ChartAreas(0).AxisX.CustomLabels.Add(i + 0.5, i + 1.5, mois(i))
        Next i


        adpt1 = New MySqlDataAdapter("select name,count(*) from orderdetails group by name order by count(*) desc limit 20", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        Dim tabletop As New DataTable
        adpt1.Fill(tabletop)

        DataGridView1.Rows.Clear()
        ' Ajouter des points de données
        For i = 0 To tabletop.Rows.Count - 1
            DataGridView1.Rows.Add(tabletop.Rows(i).Item(0), Convert.ToDouble(tabletop.Rows(i).Item(1)).ToString("N0"))
        Next

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(reste,',','.'),' ','')) from achat where REPLACE(REPLACE(reste,',','.'),' ','') > 0", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        Dim tabledette As New DataTable
        adpt1.Fill(tabledette)
        Dim dette As Double = 0
        If IsDBNull(tabledette.Rows(0).Item(0)) Then
        Else
            dette = Convert.ToDouble(tabledette.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(reste,',','.'),' ','')) from facture where REPLACE(REPLACE(reste,',','.'),' ','') > 0", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        Dim tablecreance As New DataTable
        adpt1.Fill(tablecreance)
        Dim creance As Double = 0
        If IsDBNull(tablecreance.Rows(0).Item(0)) Then
        Else
            creance = Convert.ToDouble(tablecreance.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(reste,',','.'),' ','')) from orders where REPLACE(REPLACE(reste,',','.'),' ','') > 0 and etat = 'receipt'", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        Dim tablecreance2 As New DataTable
        adpt1.Fill(tablecreance2)
        If IsDBNull(tablecreance2.Rows(0).Item(0)) Then
        Else
            creord = Convert.ToDouble(tablecreance2.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

        End If

        adpt1 = New MySqlDataAdapter("select * from cheques where etat = '0'", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        Dim tableech As New DataTable
        adpt1.Fill(tableech)
        Dim echfrs As Double = 0
        Dim echclt As Double = 0
        For i = 0 To tableech.Rows.Count - 1
            If IsDBNull(tableech.Rows(i).Item(8)) Then
                echclt = echclt + Convert.ToDouble(tableech.Rows(i).Item(12).ToString.Replace(" ", "").Replace(".", ","))
            Else
                echfrs = echfrs + Convert.ToDouble(tableech.Rows(i).Item(12).ToString.Replace(" ", "").Replace(".", ","))
            End If
        Next

        Label35.Text = dette.ToString("N2")
        Label34.Text = (creance + creord).ToString("N2")
        Label33.Text = echclt.ToString("N2")
        Label29.Text = echfrs.ToString("N2")


        adpt = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(PA_TTC,',','.'),' ','') * REPLACE(REPLACE(Stock,',','.'),' ','')) from article", conn2)
        Dim tablestock As New DataTable
        adpt.Fill(tablestock)

        adpt = New MySqlDataAdapter("select ttc from inventaire_list ORDER BY ID DESC", conn2)
        Dim tableinv As New DataTable
        adpt.Fill(tableinv)

        Dim stockfin, stockdebut As Double
        If IsDBNull(tablestock.Rows(0).Item(0)) Then
            stockfin = 0
        Else
            stockfin = Convert.ToDouble(tablestock.Rows(0).Item(0).ToString.Replace(".", ","))

        End If

        If IsDBNull(tableinv.Rows(0).Item(0)) Then
            stockdebut = 0
        Else
            stockdebut = Convert.ToDouble(tableinv.Rows(0).Item(0).ToString.Replace(".", ","))

        End If

        Dim variation As Double = (stockdebut + stockfin) / 2
        Label30.Text = stockfin.ToString("N2")
        Label28.Text = Convert.ToDouble(cayear / variation).ToString("N0")
    End Sub

    Private Sub IconButton55_Click(sender As Object, e As EventArgs) Handles IconButton55.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Statistiques CA Fournisseurs'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then
                statistique.Show()
                Panel10.Visible = False
                Panel11.Visible = False
                Panel12.Visible = False
                Panel33.Visible = False
                statistique.Panel11.Visible = True
                statistique.Label16.Text = "CA Fournisseurs :"
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton54_Click(sender As Object, e As EventArgs) Handles IconButton54.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Dividendes'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                statistique.Show()
                Panel10.Visible = False
                Panel11.Visible = False
                Panel12.Visible = False
                Panel33.Visible = False
                statistique.Panel12.Visible = True
                statistique.Label16.Text = "Dividendes :"
                adpt3 = New MySqlDataAdapter("SELECT * FROM `participants` order by name asc", conn)
                Dim clt As New DataTable
                adpt3.Fill(clt)
                Dim sumcapital As Double = 0
                statistique.DataGridView6.Rows.Clear()
                If clt.Rows.Count <> 0 Then
                    For i = 0 To clt.Rows.Count - 1
                        Dim sumdivid As Double = 0
                        adpt3 = New MySqlDataAdapter("SELECT SUM(REPLACE(REPLACE(montant,',','.'),' ','')) FROM `dividendes` where part_id = '" & clt.Rows(i).Item(0) & "'", conn)
                        Dim div As New DataTable
                        adpt3.Fill(div)
                        If IsDBNull(div.Rows(0).Item(0)) Then
                            sumdivid = 0
                        Else
                            sumdivid = div.Rows(0).Item(0).ToString.Replace(".", ",")
                        End If
                        statistique.DataGridView6.Rows.Add(clt.Rows(i).Item(0), clt.Rows(i).Item(1), Convert.ToDouble(clt.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(clt.Rows(i).Item(3).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2") & " %", sumdivid.ToString("N2"), clt.Rows(i).Item(4), Convert.ToDouble(clt.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
                        sumcapital = sumcapital + Convert.ToDouble(clt.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ","))
                        statistique.Chart1.Series("CA").Points.AddXY(clt.Rows(i).Item(1), Convert.ToDouble(clt.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")))

                    Next

                End If
                statistique.TextBox18.Text = sumcapital.ToString("N2")
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    <Obsolete>
    Private Sub IconButton58_Click(sender As Object, e As EventArgs) Handles IconButton58.Click
        Dim server = System.Configuration.ConfigurationSettings.AppSettings("server")
        Dim database = System.Configuration.ConfigurationSettings.AppSettings("database")
        Dim user = System.Configuration.ConfigurationSettings.AppSettings("userid")
        Dim password = System.Configuration.ConfigurationSettings.AppSettings("password")

        Dim appPath As String = Application.StartupPath()
        Dim backupPath As DirectoryInfo = New DirectoryInfo(appPath & "\backups")

        Dim dateTimeString As String = DateTime.Now.ToString("yyyyMMdd_HHmmss")

        ' Set the filename for the backup file with date and time
        Dim fileName = $"Sauvegarde_{dateTimeString}.sql"

        ' Build the mysqldump command
        Dim command As String

        If password <> "" Then
            command = $"C:\xampp\mysql\bin\mysqldump -h {server} -u {user} -p{password} {database} > ""{backupPath}\{fileName}"""
        Else
            command = $"C:\xampp\mysql\bin\mysqldump -h {server} -u {user} {database} > ""{backupPath}\{fileName}"""
        End If

        ' Start the process
        StartProcess(command)
    End Sub

    Private startTime As DateTime
    Private process As Process


    Private Sub StartProcess(command As String)
        ' Create and configure the process
        process = New Process()
        Dim startInfo As New ProcessStartInfo()
        startInfo.FileName = "cmd.exe"
        startInfo.RedirectStandardInput = True
        startInfo.RedirectStandardOutput = True
        startInfo.UseShellExecute = False
        startInfo.CreateNoWindow = True

        process.StartInfo = startInfo

        ' Begin asynchronous read of the output stream
        process.Start()
        process.BeginOutputReadLine()

        ' Record the start time
        startTime = DateTime.Now

        ' Write the mysqldump command to the command prompt
        process.StandardInput.WriteLine(command)
        process.StandardInput.Close()
        MsgBox("Sauvegarde Terminée !")
        Panel10.Visible = False
        Panel11.Visible = False
        Panel12.Visible = False
        Panel33.Visible = False
    End Sub

    Private Sub IconButton20_Click(sender As Object, e As EventArgs) Handles IconButton20.Click
        alertes.Show()
        alertes.Label11.Text = "Factures à encaisser"
        adpt6 = New MySqlDataAdapter("select * from facture WHERE REPLACE(reste,',','.') > @day ", conn2)
        adpt6.SelectCommand.Parameters.Clear()
        adpt6.SelectCommand.Parameters.AddWithValue("@day", 0)
        Dim table1 As New DataTable
        adpt6.Fill(table1)
        Dim faccount As Double = 0
        alertes.DataGridView3.Rows.Clear()
        alertes.DataGridView3.Visible = True
        For i = 0 To table1.Rows.Count() - 1
            adpt6 = New MySqlDataAdapter("select fac from clients WHERE client = @day ", conn2)
            adpt6.SelectCommand.Parameters.Clear()
            adpt6.SelectCommand.Parameters.AddWithValue("@day", table1.Rows(i).Item(5))
            Dim table8 As New DataTable
            adpt6.Fill(table8)
            Dim ech As String = DateDiff(DateInterval.Day, Now.Date, table1.Rows(i).Item(1))
            If ech >= table8.Rows(0).Item(0) Then
                alertes.DataGridView3.Rows.Add(table1.Rows(i).Item(0), "Fac-" & table1.Rows(i).Item(0), Convert.ToDouble(table1.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDateTime(table1.Rows(i).Item(1)).ToString("yyyy/MM/dd"), table1.Rows(i).Item(5))
            End If
        Next
    End Sub


    Private Sub IconButton19_Click(sender As Object, e As EventArgs)
        alertes.ShowDialog()

    End Sub


    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        bcmnd.Show()
    End Sub

    Private Sub IconButton18_Click(sender As Object, e As EventArgs) Handles IconButton18.Click
        factures.Show()
        Me.Close()
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        archive.Show()
        Me.Close()

    End Sub

    Private Sub IconButton8_Click(sender As Object, e As EventArgs)
        product.Show()
        Me.Close()

    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Four.Show()
        Me.Close()

    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        users.Show()
        Me.Close()

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork

    End Sub

End Class