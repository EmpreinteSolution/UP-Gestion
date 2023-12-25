Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient

Public Class rapproche
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Dim sql2, sql3 As String

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        DataGridView2.Rows.Clear()

        If ComboBox1.Text = "Rapprochement Achats" Then
            If ComboBox5.Text = "Tout" Then
                If ComboBox6.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("select * from cheques where fac IS NULL and tick IS NULL and echeance BETWEEN @datedebut AND @datefin order by date desc", conn2)
                Else
                    adpt = New MySqlDataAdapter("select * from cheques where fac IS NULL and tick IS NULL and echeance BETWEEN @datedebut AND @datefin and bq = '" & ComboBox6.Text & "' order by date desc", conn2)
                End If
            End If
            If ComboBox5.Text = "Pointé" Then
                If ComboBox6.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("select * from cheques where fac IS NULL and tick IS NULL and echeance BETWEEN @datedebut AND @datefin and etat = '1' order by date desc", conn2)
                Else
                    adpt = New MySqlDataAdapter("select * from cheques where fac IS NULL and tick IS NULL and echeance BETWEEN @datedebut AND @datefin and bq = '" & ComboBox6.Text & "' and etat = '1' order by date desc", conn2)
                End If
            End If
            If ComboBox5.Text = "Non Pointé" Then
                If ComboBox6.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("select * from cheques where fac IS NULL and tick IS NULL and echeance BETWEEN @datedebut AND @datefin and etat = '0' order by date desc", conn2)
                Else
                    adpt = New MySqlDataAdapter("select * from cheques where fac IS NULL and tick IS NULL and echeance BETWEEN @datedebut AND @datefin and bq = '" & ComboBox6.Text & "' and etat = '0' order by date desc", conn2)
                End If
            End If
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
            DataGridView2.Columns(2).Visible = False
            DataGridView2.Columns(3).Visible = True
        End If

        If ComboBox1.Text = "Rapprochement Ventes" Then
            If ComboBox5.Text = "Tout" Then
                If ComboBox6.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("select * from cheques where achat IS NULL and echeance BETWEEN @datedebut AND @datefin order by date desc", conn2)
                Else
                    adpt = New MySqlDataAdapter("select * from cheques where achat IS NULL and echeance BETWEEN @datedebut AND @datefin and bq = '" & ComboBox6.Text & "' order by date desc", conn2)
                End If
            End If
            If ComboBox5.Text = "Pointé" Then
                If ComboBox6.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("select * from cheques where achat IS NULL and echeance BETWEEN @datedebut AND @datefin and etat = '1' order by date desc", conn2)
                Else
                    adpt = New MySqlDataAdapter("select * from cheques where achat IS NULL and echeance BETWEEN @datedebut AND @datefin and bq = '" & ComboBox6.Text & "' and etat = '1' order by date desc", conn2)
                End If
            End If
            If ComboBox5.Text = "Non Pointé" Then
                If ComboBox6.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("select * from cheques where achat IS NULL and echeance BETWEEN @datedebut AND @datefin and etat = '0' order by date desc", conn2)
                Else
                    adpt = New MySqlDataAdapter("select * from cheques where achat IS NULL and echeance BETWEEN @datedebut AND @datefin and bq = '" & ComboBox6.Text & "' and etat = '0' order by date desc", conn2)
                End If
            End If
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
            DataGridView2.Columns(3).Visible = False
            DataGridView2.Columns(2).Visible = True

        End If

        Dim table As New DataTable
        adpt.Fill(table)
        Dim modep As String
        Dim daty As String
        Dim daty2 As String
        Dim details As String
        Dim client As String = ""
        Dim frs As String = ""
        Dim pointage As String = ""
        Dim sum As Double = 0
        Dim sump As Double = 0
        Dim sumnp As Double = 0
        Dim sumimp As Double = 0

        For i = 0 To table.Rows.Count() - 1

            If table.Rows(i).Item(1) = "tpe" Then
                modep = "Carte bancaire"
                details = "STAN : " & table.Rows(i).Item(4) & vbCrLf & "AUT : " & table.Rows(i).Item(2)
            Else
                If table.Rows(i).Item(15) = "Chèque" Then
                    details = "CHQ N° : " & table.Rows(i).Item(4)
                    modep = "Chèque"
                End If
                If table.Rows(i).Item(15) = "Virement" Then
                    details = "VIR N° : " & table.Rows(i).Item(4)
                    modep = "Virement"
                End If
                If table.Rows(i).Item(15) = "Effet" Then
                    details = "Effet N° : " & table.Rows(i).Item(4)
                    modep = "Effet"
                End If
            End If
            If table.Rows(i).Item(5) = "tpe" Then
                daty = "-"
                daty2 = "-"
            Else
                daty = Convert.ToDateTime(table.Rows(i).Item(5)).ToString("dd/MM/yyyy")
                daty2 = Convert.ToDateTime(table.Rows(i).Item(5)).ToString("yyyy/MM/dd")

            End If
            If IsDBNull(table.Rows(i).Item(8)) Then
                client = table.Rows(i).Item(14)
            Else
                frs = table.Rows(i).Item(14)
            End If
            If table.Rows(i).Item(9) = 0 Then
                pointage = "..."
                sumnp = sumnp + Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ","))
            Else
                pointage = "Pointé le " & Format(table.Rows(i).Item(11), "dd/MM/yyyy")
                sump = sump + Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ","))
            End If

            Dim imp As String = "..."
            If table.Rows(i).Item(16) = 1 Then
                imp = "Impayé"
                sumimp = sumimp + Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ","))
            End If


            DataGridView2.Rows.Add(Convert.ToDateTime(table.Rows(i).Item(6)).ToString("dd/MM/yyyy"), daty, client, frs, modep, details, table.Rows(i).Item(10), Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(0), pointage, Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ",")), Convert.ToDateTime(table.Rows(i).Item(6)).ToString("yyyy/MM/dd"), daty2, imp)
            sum = sum + Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ","))
        Next
        TextBox16.Text = sum.ToString("N2")
        TextBox15.Text = sump.ToString("N2")
        TextBox13.Text = sumnp.ToString("N2")
        TextBox11.Text = sumimp.ToString("N2")
        For Each row As DataGridViewRow In DataGridView2.Rows
            If row.Cells(9).Value = "..." Then
                row.DefaultCellStyle.ForeColor = Color.Red
            Else
                row.DefaultCellStyle.ForeColor = Color.Black
            End If
        Next

    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Close()
    End Sub



    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        DataGridView1.Rows.Clear()
        If ComboBox2.Text = "Tout" Then
            If ComboBox4.Text = "Tout" Then
                adpt = New MySqlDataAdapter("select * from reglement where achat IS NULL and date BETWEEN @datedebut AND @datefin order by date desc", conn2)
            Else
                adpt = New MySqlDataAdapter("select * from reglement where achat IS NULL and date BETWEEN @datedebut AND @datefin and mode = '" & ComboBox4.Text & "' order by date desc", conn2)
            End If
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker4.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker3.Value.Date.AddDays(1))
        Else
            If ComboBox4.Text = "Tout" Then
                adpt = New MySqlDataAdapter("select * from reglement where achat IS NULL and date BETWEEN @datedebut AND @datefin and client = @clt order by date desc", conn2)
            Else
                adpt = New MySqlDataAdapter("select * from reglement where achat IS NULL and date BETWEEN @datedebut AND @datefin and client = @clt and mode = '" & ComboBox4.Text & "' order by date desc", conn2)
            End If
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker4.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker3.Value.Date.AddDays(1))
            adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
        End If

        Dim table As New DataTable
        adpt.Fill(table)
        Dim modep As String = ""
        Dim numregl As String
        Dim sum As Double = 0
        Dim sumesp As Double = 0
        Dim sumtpe As Double = 0
        Dim sumchq As Double = 0
        Dim sumvir As Double = 0
        Dim sumef As Double = 0
        For i = 0 To table.Rows.Count() - 1
            If table.Rows(i).Item(4) = "Espèce" Or table.Rows(i).Item(4) = "Espèce (Comptoir)" Or table.Rows(i).Item(4) = "Espèce (Coffre)" Then
                modep = table.Rows(i).Item(4)

            Else
                adpt = New MySqlDataAdapter("select * from cheques where fac = @reg ", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@reg", table.Rows(i).Item(6))
                Dim table2 As New DataTable
                adpt.Fill(table2)
                If table2.Rows.Count() <> 0 Then
                    modep = table.Rows(i).Item(4) & " N°:" & table2.Rows(0).Item(4) & " | " & " BQ: " & table2.Rows(0).Item(10)
                Else
                    adpt = New MySqlDataAdapter("select * from cheques where tick = @reg ", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@reg", table.Rows(i).Item(9))
                    adpt.Fill(table2)
                    If table2.Rows.Count() <> 0 Then
                        modep = table.Rows(i).Item(4) & " N°:" & table2.Rows(0).Item(4) & " | " & " BQ: " & table2.Rows(0).Item(10)
                    End If
                End If
            End If
            If IsDBNull(table.Rows(i).Item(6)) Then
                numregl = table.Rows(i).Item(9)
            Else
                numregl = table.Rows(i).Item(6)
            End If

            DataGridView1.Rows.Add(Format(table.Rows(i).Item(3), "dd/MM/yyyy"), Format(table.Rows(i).Item(8), "dd/MM/yyyy"), numregl, table.Rows(i).Item(5), modep, Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ",")).ToString("N2"), "X")

            If table.Rows(i).Item(4) = "Espèce" Or table.Rows(i).Item(4) = "Espèce (Comptoir)" Or table.Rows(i).Item(4) = "Espèce (Coffre)" Then
                sumesp = sumesp + table.Rows(i).Item(2).ToString.Replace(".", ",")
            End If
            If table.Rows(i).Item(4) = "TPE" Then
                sumtpe = sumtpe + table.Rows(i).Item(2).ToString.Replace(".", ",")
            End If
            If table.Rows(i).Item(4) = "Chèque" Then
                sumchq = sumchq + table.Rows(i).Item(2).ToString.Replace(".", ",")
            End If
            If table.Rows(i).Item(4) = "Virement" Then
                sumvir = sumvir + table.Rows(i).Item(2).ToString.Replace(".", ",")
            End If
            If table.Rows(i).Item(4) = "Effet" Then
                sumef = sumef + table.Rows(i).Item(2).ToString.Replace(".", ",")
            End If
            sum = sum + table.Rows(i).Item(2).ToString.Replace(".", ",")
        Next
        Label9.Text = sum.ToString("N2") & " DHs"
        TextBox20.Text = sumesp.ToString("N2")
        TextBox2.Text = sumchq.ToString("N2")
        TextBox3.Text = sumtpe.ToString("N2")
        TextBox4.Text = sumef.ToString("N2")
        TextBox5.Text = sumvir.ToString("N2")
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        DataGridView3.Rows.Clear()
        If ComboBox3.Text = "Tout" Then
            If ComboBox4.Text = "Tout" Then
                adpt = New MySqlDataAdapter("select * from reglement where fac IS NULL and tick = '-' and date BETWEEN @datedebut AND @datefin order by date desc", conn2)
            Else
                adpt = New MySqlDataAdapter("select * from reglement where fac IS NULL and tick = '-' and date BETWEEN @datedebut AND @datefin and mode = '" & ComboBox4.Text & "' order by date desc", conn2)
            End If
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker6.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker5.Value.Date.AddDays(1))
        Else
            If ComboBox4.Text = "Tout" Then
                adpt = New MySqlDataAdapter("select * from reglement where fac IS NULL and tick = '-' and date BETWEEN @datedebut AND @datefin and client = @clt order by date desc", conn2)
            Else
                adpt = New MySqlDataAdapter("select * from reglement where fac IS NULL and tick = '-' and date BETWEEN @datedebut AND @datefin and client = @clt and mode = '" & ComboBox4.Text & "' order by date desc", conn2)
            End If
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker6.Value.Date)
            adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker5.Value.Date.AddDays(1))
            adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox3.Text)
        End If

        Dim table As New DataTable
        adpt.Fill(table)
        Dim modep As String
        Dim numregl As String
        Dim sum As Double = 0
        Dim sumesp As Double = 0
        Dim sumtpe As Double = 0
        Dim sumchq As Double = 0
        Dim sumvir As Double = 0
        Dim sumef As Double = 0


        For i = 0 To table.Rows.Count() - 1
            If table.Rows(i).Item(4) = "Espèce" Or table.Rows(i).Item(4) = "Espèce (Comptoir)" Or table.Rows(i).Item(4) = "Espèce (Coffre)" Then
                modep = table.Rows(i).Item(4)

            Else
                adpt = New MySqlDataAdapter("select * from cheques where achat = @reg", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@reg", table.Rows(i).Item(7))
                Dim table2 As New DataTable
                adpt.Fill(table2)
                If table2.Rows.Count() <> 0 Then
                    modep = table.Rows(i).Item(4) & " N°:" & table2.Rows(0).Item(4) & " | " & " BQ: " & table2.Rows(0).Item(10)
                Else
                    modep = table.Rows(i).Item(4)
                End If
            End If


            DataGridView3.Rows.Add(Format(table.Rows(i).Item(3), "dd/MM/yyyy"), Format(table.Rows(i).Item(8), "dd/MM/yyyy"), table.Rows(i).Item(7), table.Rows(i).Item(5), modep, Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ",")).ToString("N2"), "X")
            If table.Rows(i).Item(4) = "Espèce" Or table.Rows(i).Item(4) = "Espèce (Comptoir)" Or table.Rows(i).Item(4) = "Espèce (Coffre)" Then
                sumesp = sumesp + table.Rows(i).Item(2).ToString.Replace(".", ",")
            End If
            If table.Rows(i).Item(4) = "TPE" Then
                sumtpe = sumtpe + table.Rows(i).Item(2).ToString.Replace(".", ",")
            End If
            If table.Rows(i).Item(4) = "Chèque" Then
                sumchq = sumchq + table.Rows(i).Item(2).ToString.Replace(".", ",")
            End If
            If table.Rows(i).Item(4) = "Virement" Then
                sumvir = sumvir + table.Rows(i).Item(2).ToString.Replace(".", ",")
            End If
            If table.Rows(i).Item(4) = "Effet" Then
                sumef = sumef + table.Rows(i).Item(2).ToString.Replace(".", ",")
            End If
            sum = sum + table.Rows(i).Item(2).ToString.Replace(".", ",")
        Next
        Label7.Text = sum.ToString("N2") & " DHs"

        TextBox10.Text = sumesp.ToString("N2")
        TextBox9.Text = sumchq.ToString("N2")
        TextBox8.Text = sumtpe.ToString("N2")
        TextBox7.Text = sumef.ToString("N2")
        TextBox6.Text = sumvir.ToString("N2")
    End Sub

    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged
        If TextBox14.Text = "" Then
            adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                ComboBox3.Items.Clear()
                ComboBox3.Items.Add("Tout")
                For i = 0 To table.Rows.Count - 1
                    ComboBox3.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox3.SelectedIndex = 0

            End If
        Else

            Dim inputText As String = TextBox14.Text

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
                RemoveHandler TextBox14.TextChanged, AddressOf TextBox14_TextChanged

                ' Mettez à jour le texte dans le TextBox
                TextBox14.Text = modifiedText

                ' Replacez le curseur à la position correcte après la modification
                TextBox14.SelectionStart = TextBox14.Text.Length

                ' Réactivez le gestionnaire d'événements TextChanged
                AddHandler TextBox14.TextChanged, AddressOf TextBox14_TextChanged
            End If

            ComboBox3.Items.Clear()
            adpt = New MySqlDataAdapter("select name from fours WHERE name LIKE '%" + TextBox14.Text.Replace("'", " ") + "%' order by name asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                For i = 0 To table.Rows.Count - 1
                    ComboBox3.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox3.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            adpt = New MySqlDataAdapter("select client from clients order by client asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                ComboBox2.Items.Clear()
                ComboBox2.Items.Add("Tout")
                For i = 0 To table.Rows.Count - 1
                    ComboBox2.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox2.SelectedIndex = 0

            End If
        Else

            Dim inputText As String = TextBox1.Text

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
                RemoveHandler TextBox1.TextChanged, AddressOf TextBox1_TextChanged

                ' Mettez à jour le texte dans le TextBox
                TextBox1.Text = modifiedText

                ' Replacez le curseur à la position correcte après la modification
                TextBox1.SelectionStart = TextBox1.Text.Length

                ' Réactivez le gestionnaire d'événements TextChanged
                AddHandler TextBox1.TextChanged, AddressOf TextBox1_TextChanged
            End If

            ComboBox2.Items.Clear()
            adpt = New MySqlDataAdapter("select client from clients WHERE client LIKE '%" + TextBox1.Text.Replace("'", " ") + "%' order by client asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                For i = 0 To table.Rows.Count - 1
                    ComboBox2.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox2.SelectedIndex = 0
            End If
        End If
    End Sub

    Dim cmd2, cmd3 As MySqlCommand

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Panel8.Visible = False
        DataGridView4.Rows.Clear()
    End Sub

    Private Sub rapproche_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim tpe As String
        ComboBox6.Items.Clear()
        ComboBox6.Items.Add("Tout")

        For i = 0 To table.Rows.Count() - 1
            ComboBox6.Items.Add(table.Rows(i).Item(1))
        Next

        ComboBox6.Text = "Tout"
        ComboBox1.Text = "Rapprochement Achats"
        ComboBox4.Text = "Tout"
        ComboBox5.Text = "Tout"
        ComboBox2.Text = "Tout"
        ComboBox3.Text = "Tout"


    End Sub

    Private Sub Panel6_Paint(sender As Object, e As PaintEventArgs) Handles Panel6.Paint

    End Sub

    Private Sub DataGridView2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Supprimer un encaissement'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                If e.ColumnIndex = 6 Then
                    Dim Rep As Integer
                    Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Rep = MsgBox("Voulez-vous vraiment supprimer cet encaissement ?", vbYesNo)
                    If Rep = vbYes Then

                        conn2.Close()
                        conn2.Open()
                        Dim sql2, sql3 As String
                        Dim cmd2, cmd3 As MySqlCommand
                        sql2 = "DELETE FROM `reglement` WHERE fac = '" + row.Cells(2).Value.ToString + "'"
                        cmd2 = New MySqlCommand(sql2, conn2)
                        cmd2.ExecuteNonQuery()

                        sql2 = "DELETE FROM `reglement` WHERE tick = '" + row.Cells(2).Value.ToString + "' "
                        cmd2 = New MySqlCommand(sql2, conn2)
                        cmd2.ExecuteNonQuery()
                        conn2.Close()

                        conn2.Open()
                        adpt = New MySqlDataAdapter("select * from reglement_fac where reglement = '" + row.Cells(2).Value.ToString + "'", conn2)
                        Dim table As New DataTable
                        adpt.Fill(table)

                        For i = 0 To table.Rows.Count - 1
                            If table.Rows.Count = 1 Then
                                sql3 = "Update `facture` Set `etat`='Non Payé',`modePayement`='Crédit',`paye`=REPLACE(`paye`,',','.') - '" & Convert.ToDouble(row.Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",")) & "',`reste`=REPLACE(`reste`,',','.') + '" & Convert.ToDouble(row.Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",")) & "' WHERE `OrderID` = '" & table.Rows(i).Item(2).ToString & "'"
                                cmd3 = New MySqlCommand(sql3, conn2)
                                cmd3.ExecuteNonQuery()

                                sql3 = "Update `orders` SET `paye`=REPLACE(`paye`,',','.') - '" & Convert.ToDouble(row.Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",")) & "',`reste`= REPLACE(`reste`,',','.') + '" & Convert.ToDouble(row.Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",")) & "',`modePayement`='Crédit' WHERE `OrderID` = '" & table.Rows(i).Item(2).ToString & "'"
                                cmd3 = New MySqlCommand(sql3, conn2)
                                cmd3.ExecuteNonQuery()
                            End If
                            If table.Rows.Count > 1 Then
                                sql3 = "Update `facture` Set `etat`='Non Payé',`modePayement`='Crédit',`paye`=0 ,`reste`= `MontantOrder` WHERE `OrderID` = '" & table.Rows(i).Item(2).ToString & "'"
                                cmd3 = New MySqlCommand(sql3, conn2)
                                cmd3.ExecuteNonQuery()

                                sql3 = "Update `orders` SET `paye`=0,`reste`= `MontantOrder` ,`modePayement`='Crédit' WHERE `OrderID` = '" & table.Rows(i).Item(2).ToString & "'"
                                cmd3 = New MySqlCommand(sql3, conn2)
                                cmd3.ExecuteNonQuery()

                                adpt = New MySqlDataAdapter("select * from reglement_fac where fac = '" + table.Rows(i).Item(2).ToString + "' ", conn2)
                                Dim tableotherfac As New DataTable
                                adpt.Fill(tableotherfac)
                                For j = 0 To tableotherfac.Rows.Count - 1
                                    sql3 = "DELETE FROM `reglement_fac` WHERE id = '" + tableotherfac.Rows(j).Item(0).ToString() + "' "
                                    cmd3 = New MySqlCommand(sql3, conn2)

                                    cmd3.ExecuteNonQuery()

                                    sql3 = "DELETE FROM `reglement` WHERE fac = '" + tableotherfac.Rows(j).Item(1).ToString() + "' Or tick = '" + tableotherfac.Rows(j).Item(1).ToString() + "' "
                                    cmd3 = New MySqlCommand(sql3, conn2)

                                    cmd3.ExecuteNonQuery()

                                    sql3 = "DELETE FROM `cheques` WHERE fac = '" + tableotherfac.Rows(j).Item(1).ToString() + "' Or tick = '" + tableotherfac.Rows(j).Item(1).ToString() + "' "
                                    cmd3 = New MySqlCommand(sql3, conn2)

                                    cmd3.ExecuteNonQuery()
                                Next
                            End If

                        Next
                        sql3 = "DELETE FROM `reglement_fac` WHERE reglement = '" + row.Cells(2).Value.ToString + "' "
                        cmd3 = New MySqlCommand(sql3, conn2)

                        cmd3.ExecuteNonQuery()

                        sql3 = "DELETE FROM `cheques` WHERE fac = '" + row.Cells(2).Value.ToString + "' Or tick = '" + row.Cells(2).Value.ToString + "' "
                        cmd3 = New MySqlCommand(sql3, conn2)

                        cmd3.ExecuteNonQuery()
                        conn2.Close()

                        conn2.Open()
                        sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@name", user)
                        cmd3.Parameters.AddWithValue("@op", "Suppression de l'Encaissement " & row.Cells(4).Value.ToString & " du Client " & row.Cells(3).Value.ToString)
                        cmd3.ExecuteNonQuery()
                        conn2.Close()

                        IconButton1.PerformClick()
                    End If
                Else
                    Panel8.Visible = True
                    Panel8.BringToFront()
                    adpt = New MySqlDataAdapter("select * from reglement_fac where reglement = '" + DataGridView1.Rows(e.RowIndex).Cells(2).Value + "' order by date desc", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    DataGridView4.Rows.Clear()
                    Dim piecenum As String
                    For i = 0 To table.Rows.Count - 1
                        adpt = New MySqlDataAdapter("select * from facture where OrderID = '" & table.Rows(i).Item(2) & "'", conn2)
                        Dim table2 As New DataTable
                        adpt.Fill(table2)
                        If table2.Rows.Count = 0 Then
                            adpt = New MySqlDataAdapter("select * from orders where OrderID = '" & table.Rows(i).Item(2) & "'", conn2)
                            adpt.Fill(table2)
                            piecenum = "T-" & table.Rows(i).Item(2)
                        Else
                            piecenum = "F-" & table.Rows(i).Item(2)
                        End If
                        DataGridView4.Rows.Add(Convert.ToDateTime(table2.Rows(0).Item(1)).ToString("dd/MM/yyyy"), piecenum, Convert.ToDecimal(table2.Rows(0).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("# ##0.00"), Convert.ToDecimal(table2.Rows(0).Item(7).ToString.Replace(" ", "").Replace(".", ",")).ToString("# ##0.00"))
                    Next
                End If
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Dim debit As Double = 0
        Dim credit As Double = 0
        Dim solde As Double = 0

        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("raproche")
        For i = 0 To DataGridView2.Rows.Count - 1
            If ComboBox1.Text = "Rapprochement Achats" Then

                dt.Rows.Add(DataGridView2.Rows(i).Cells(0).Value, DataGridView2.Rows(i).Cells(1).Value, DataGridView2.Rows(i).Cells(3).Value, DataGridView2.Rows(i).Cells(4).Value, DataGridView2.Rows(i).Cells(5).Value, DataGridView2.Rows(i).Cells(6).Value, DataGridView2.Rows(i).Cells(7).Value, DataGridView2.Rows(i).Cells(9).Value)

            Else

                dt.Rows.Add(DataGridView2.Rows(i).Cells(0).Value, DataGridView2.Rows(i).Cells(1).Value, DataGridView2.Rows(i).Cells(2).Value, DataGridView2.Rows(i).Cells(4).Value, DataGridView2.Rows(i).Cells(5).Value, DataGridView2.Rows(i).Cells(6).Value, DataGridView2.Rows(i).Cells(7).Value, DataGridView2.Rows(i).Cells(9).Value)

            End If

        Next
        debit = TextBox16.Text
        credit = TextBox15.Text
        solde = TextBox13.Text
        ReportToPrint = New LocalReport()
        formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\Report11.rdlc"
        formata4.ReportViewer1.LocalReport.DataSources.Clear()
        formata4.ReportViewer1.LocalReport.EnableExternalImages = True
        Dim du As New ReportParameter("du", DateTimePicker1.Text)
        Dim du1(0) As ReportParameter
        du1(0) = du
        formata4.ReportViewer1.LocalReport.SetParameters(du1)

        Dim au As New ReportParameter("au", DateTimePicker2.Text)
        Dim au1(0) As ReportParameter
        au1(0) = au
        formata4.ReportViewer1.LocalReport.SetParameters(au1)

        Dim etat As New ReportParameter("etat", "Etat des Echéances")
        Dim etat1(0) As ReportParameter
        etat1(0) = etat
        formata4.ReportViewer1.LocalReport.SetParameters(etat1)

        Dim debito As New ReportParameter("debit", debit.ToString("N2"))
        Dim debito1(0) As ReportParameter
        debito1(0) = debito
        formata4.ReportViewer1.LocalReport.SetParameters(debito1)

        Dim credito As New ReportParameter("credit", credit.ToString("N2"))
        Dim credito1(0) As ReportParameter
        credito1(0) = credito
        formata4.ReportViewer1.LocalReport.SetParameters(credito1)

        Dim soldito As New ReportParameter("solde", solde.ToString("N2"))
        Dim soldito1(0) As ReportParameter
        soldito1(0) = soldito
        formata4.ReportViewer1.LocalReport.SetParameters(soldito1)

        Dim appPath As String = Application.StartupPath()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

        Dim img As New ReportParameter("image", "File:\" + SavePath, True)
        Dim img1(0) As ReportParameter
        img1(0) = img
        formata4.ReportViewer1.LocalReport.SetParameters(img1)


        formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
        formata4.ReportViewer1.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next
        Dim printDoc As New PrintDocument()
        m_currentPageIndex = 0
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
    End Sub

    Private Sub DataGridView3_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellDoubleClick
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Supprimer un reglement'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                If e.ColumnIndex = 6 Then
                    Dim Rep As Integer
                    Dim row As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
                    Rep = MsgBox("Voulez-vous vraiment supprimer ce réglement ?", vbYesNo)
                    If Rep = vbYes Then

                        conn2.Close()
                        conn2.Open()
                        Dim sql2, sql3 As String
                        Dim cmd2, cmd3 As MySqlCommand
                        sql2 = "DELETE FROM `reglement` WHERE achat = '" + row.Cells(2).Value.ToString + "'"
                        cmd2 = New MySqlCommand(sql2, conn2)
                        cmd2.ExecuteNonQuery()

                        conn2.Close()

                        conn2.Open()
                        adpt = New MySqlDataAdapter("select * from reglement_fac where reglement = '" + row.Cells(2).Value.ToString + "'", conn2)
                        Dim table As New DataTable
                        adpt.Fill(table)
                        For i = 0 To table.Rows.Count - 1
                            If table.Rows.Count = 1 Then

                                sql3 = "Update `achat` SET `paye`=REPLACE(`paye`,',','.') - '" & Convert.ToDouble(row.Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",")) & "',`reste`=REPLACE(`reste`,',','.') + '" & Convert.ToDouble(row.Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",")) & "' WHERE `id` = '" & table.Rows(i).Item(2).ToString & "'"
                                cmd3 = New MySqlCommand(sql3, conn2)
                                cmd3.ExecuteNonQuery()
                            End If
                            If table.Rows.Count > 1 Then
                                sql3 = "Update `achat` SET `paye`=0,`reste`=`montant` WHERE `id` = '" & table.Rows(i).Item(2).ToString & "'"
                                cmd3 = New MySqlCommand(sql3, conn2)
                                cmd3.ExecuteNonQuery()

                                adpt = New MySqlDataAdapter("select * from reglement_fac where fac = '" + table.Rows(i).Item(2).ToString + "' ", conn2)
                                Dim tableotherfac As New DataTable
                                adpt.Fill(tableotherfac)
                                For j = 0 To tableotherfac.Rows.Count - 1
                                    sql3 = "DELETE FROM `reglement_fac` WHERE id = '" + tableotherfac.Rows(j).Item(0).ToString() + "' "
                                    cmd3 = New MySqlCommand(sql3, conn2)

                                    cmd3.ExecuteNonQuery()

                                    sql3 = "DELETE FROM `reglement` WHERE achat = '" + tableotherfac.Rows(j).Item(1).ToString() + "' "
                                    cmd3 = New MySqlCommand(sql3, conn2)

                                    cmd3.ExecuteNonQuery()

                                    sql3 = "DELETE FROM `cheques` WHERE achat = '" + tableotherfac.Rows(j).Item(1).ToString() + "' "
                                    cmd3 = New MySqlCommand(sql3, conn2)

                                    cmd3.ExecuteNonQuery()
                                Next
                            End If
                        Next
                        sql3 = "DELETE FROM `reglement_fac` WHERE reglement = '" + row.Cells(2).Value.ToString + "' "
                        cmd3 = New MySqlCommand(sql3, conn2)

                        cmd3.ExecuteNonQuery()

                        sql3 = "DELETE FROM `cheques` WHERE achat = '" + row.Cells(2).Value.ToString + "' "
                        cmd3 = New MySqlCommand(sql3, conn2)

                        cmd3.ExecuteNonQuery()
                        conn2.Close()

                        conn2.Open()
                        sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@name", user)
                        cmd3.Parameters.AddWithValue("@op", "Suppression du Règlement " & row.Cells(4).Value.ToString & " du Fournisseur " & row.Cells(3).Value.ToString)
                        cmd3.ExecuteNonQuery()
                        conn2.Close()

                        IconButton3.PerformClick()
                    End If
                Else
                    Panel8.Visible = True
                    Panel8.BringToFront()
                    adpt = New MySqlDataAdapter("select * from reglement_fac where reglement = '" + DataGridView3.Rows(e.RowIndex).Cells(2).Value + "' order by date desc", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    DataGridView4.Rows.Clear()
                    Dim piecenum As String
                    For i = 0 To table.Rows.Count - 1
                        adpt = New MySqlDataAdapter("select * from achat where id = '" & table.Rows(i).Item(2) & "'", conn2)
                        Dim table2 As New DataTable
                        adpt.Fill(table2)

                        piecenum = table2.Rows(0).Item(0)
                        DataGridView4.Rows.Add(Convert.ToDateTime(table2.Rows(0).Item(3)).ToString("dd/MM/yyyy"), piecenum, Convert.ToDecimal(table2.Rows(0).Item(1).ToString.Replace(" ", "").Replace(".", ",")).ToString("# ##0.00"), Convert.ToDecimal(table2.Rows(0).Item(4).ToString.Replace(" ", "").Replace(".", ",")).ToString("# ##0.00"))
                    Next
                End If
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub DataGridView2_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseDoubleClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 9 Then
                If DataGridView2.Rows(e.RowIndex).Cells(9).Value = "..." Then

                    Dim Rep As Integer
                    Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                    Rep = MsgBox("Voulez-vous vraiment pointer ce paiement ?", vbYesNo)
                    If Rep = vbYes Then

                        conn2.Close()
                        conn2.Open()
                        Dim sql2, sql3 As String
                        Dim cmd2, cmd3 As MySqlCommand
                        sql2 = "UPDATE `cheques` SET `etat`=1,`date_confirm`='" + Convert.ToDateTime(DateTimePicker7.Value).ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE id = '" + row.Cells(8).Value.ToString + "' "
                        cmd2 = New MySqlCommand(sql2, conn2)
                        cmd2.ExecuteNonQuery()
                        conn2.Close()

                        conn2.Open()
                        sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@name", user)
                        cmd3.Parameters.AddWithValue("@op", "Rapprochement du " & row.Cells(4).Value.ToString & " N° de la pièce " & row.Cells(5).Value.ToString)
                        cmd3.ExecuteNonQuery()
                        conn2.Close()
                        DataGridView2.Rows(e.RowIndex).Cells(9).Value = "Pointé le " & Convert.ToDateTime(DateTimePicker7.Value).ToString("dd/MM/yyyy")
                        For Each row2 As DataGridViewRow In DataGridView2.Rows
                            If row2.Cells(9).Value = "..." Then
                                row2.DefaultCellStyle.ForeColor = Color.Red
                            Else
                                row2.DefaultCellStyle.ForeColor = Color.Black
                            End If
                        Next
                    End If
                Else
                    Dim Rep As Integer
                    Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                    Rep = MsgBox("Voulez-vous vraiment annuler ce pointage ?", vbYesNo)
                    If Rep = vbYes Then

                        conn2.Close()
                        conn2.Open()
                        Dim sql2, sql3 As String
                        Dim cmd2, cmd3 As MySqlCommand
                        sql2 = "UPDATE `cheques` SET `etat`=0,`date_confirm`= NULL WHERE id = '" + row.Cells(8).Value.ToString + "' "
                        cmd2 = New MySqlCommand(sql2, conn2)
                        cmd2.ExecuteNonQuery()
                        conn2.Close()

                        conn2.Open()
                        sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@name", user)
                        cmd3.Parameters.AddWithValue("@op", "Annuler le pointage du " & row.Cells(4).Value.ToString & " N° de la pièce " & row.Cells(5).Value.ToString)
                        cmd3.ExecuteNonQuery()
                        conn2.Close()

                        DataGridView2.Rows(e.RowIndex).Cells(9).Value = "..."
                        For Each row2 As DataGridViewRow In DataGridView2.Rows
                            If row2.Cells(9).Value = "..." Then
                                row2.DefaultCellStyle.ForeColor = Color.Red
                            Else
                                row2.DefaultCellStyle.ForeColor = Color.Black
                            End If
                        Next
                    End If
                End If

            End If
            If e.ColumnIndex = 13 Then
                If DataGridView2.Rows(e.RowIndex).Cells(13).Value = "..." Then

                    Dim Rep As Integer
                    Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                    Rep = MsgBox("Ce chèque est-il retourné impayé ?", vbYesNo)
                    If Rep = vbYes Then

                        conn2.Close()
                        conn2.Open()
                        Dim sql2, sql3 As String
                        Dim cmd2, cmd3 As MySqlCommand
                        sql2 = "UPDATE `cheques` SET `impaye`=1 WHERE id = '" + row.Cells(8).Value.ToString + "' "
                        cmd2 = New MySqlCommand(sql2, conn2)
                        cmd2.ExecuteNonQuery()
                        conn2.Close()

                        conn2.Open()
                        sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@name", user)
                        cmd3.Parameters.AddWithValue("@op", "Marquer le Chèque N° " & row.Cells(5).Value.ToString & " Impayé")
                        cmd3.ExecuteNonQuery()
                        conn2.Close()

                        DataGridView2.Rows(e.RowIndex).Cells(13).Value = "Impayé"
                        For Each row2 As DataGridViewRow In DataGridView2.Rows
                            If row2.Cells(9).Value = "..." Then
                                row2.DefaultCellStyle.ForeColor = Color.Red
                            Else
                                row2.DefaultCellStyle.ForeColor = Color.Black
                            End If
                        Next
                    End If
                Else
                    Dim Rep As Integer
                    Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                    Rep = MsgBox("Annuler l'impayement de ce chèque ?", vbYesNo)
                    If Rep = vbYes Then

                        conn2.Close()
                        conn2.Open()
                        Dim sql2, sql3 As String
                        Dim cmd2, cmd3 As MySqlCommand
                        sql2 = "UPDATE `cheques` SET `impaye`=0 WHERE id = '" + row.Cells(8).Value.ToString + "' "
                        cmd2 = New MySqlCommand(sql2, conn2)
                        cmd2.ExecuteNonQuery()
                        conn2.Close()

                        conn2.Open()
                        sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@name", user)
                        cmd3.Parameters.AddWithValue("@op", "Annuler l'impayement du Chèque N° " & row.Cells(5).Value.ToString)
                        cmd3.ExecuteNonQuery()
                        conn2.Close()

                        DataGridView2.Rows(e.RowIndex).Cells(13).Value = "..."
                        For Each row2 As DataGridViewRow In DataGridView2.Rows
                            If row2.Cells(9).Value = "..." Then
                                row2.DefaultCellStyle.ForeColor = Color.Red
                            Else
                                row2.DefaultCellStyle.ForeColor = Color.Black
                            End If
                        Next
                    End If
                End If
            End If
        End If
    End Sub
    Dim sroted As Integer = 0
    Private Sub DataGridView2_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.ColumnHeaderMouseClick
        If sroted = 0 Then
            If e.ColumnIndex = 7 Then
                DataGridView2.Sort(DataGridView2.Columns(10), System.ComponentModel.ListSortDirection.Descending)
            End If
            If e.ColumnIndex = 0 Then
                DataGridView2.Sort(DataGridView2.Columns(11), System.ComponentModel.ListSortDirection.Descending)
            End If
            If e.ColumnIndex = 1 Then
                DataGridView2.Sort(DataGridView2.Columns(12), System.ComponentModel.ListSortDirection.Descending)
            End If
            sroted = 1
        Else
            If e.ColumnIndex = 7 Then
                DataGridView2.Sort(DataGridView2.Columns(10), System.ComponentModel.ListSortDirection.Ascending)
            End If
            If e.ColumnIndex = 0 Then
                DataGridView2.Sort(DataGridView2.Columns(11), System.ComponentModel.ListSortDirection.Ascending)
            End If
            If e.ColumnIndex = 1 Then
                DataGridView2.Sort(DataGridView2.Columns(12), System.ComponentModel.ListSortDirection.Ascending)
            End If
            sroted = 0
        End If

    End Sub
    Dim m_currentPageIndex As Integer

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("raproche")
        For i = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, "", "", DataGridView1.Rows(i).Cells(5).Value, "")

        Next

        ReportToPrint = New LocalReport()
        formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\etatencaissement.rdlc"
        formata4.ReportViewer1.LocalReport.DataSources.Clear()
        formata4.ReportViewer1.LocalReport.EnableExternalImages = True
        Dim du As New ReportParameter("du", DateTimePicker4.Text)
        Dim du1(0) As ReportParameter
        du1(0) = du
        formata4.ReportViewer1.LocalReport.SetParameters(du1)

        Dim au As New ReportParameter("au", DateTimePicker3.Text)
        Dim au1(0) As ReportParameter
        au1(0) = au
        formata4.ReportViewer1.LocalReport.SetParameters(au1)

        Dim etat As New ReportParameter("etat", "Etat des Encaissements")
        Dim etat1(0) As ReportParameter
        etat1(0) = etat
        formata4.ReportViewer1.LocalReport.SetParameters(etat1)


        Dim client As New ReportParameter("client", ComboBox2.Text)
        Dim client1(0) As ReportParameter
        client1(0) = client
        formata4.ReportViewer1.LocalReport.SetParameters(client1)

        Dim esp As New ReportParameter("esp", TextBox20.Text)
        Dim esp1(0) As ReportParameter
        esp1(0) = esp
        formata4.ReportViewer1.LocalReport.SetParameters(esp1)

        Dim chq As New ReportParameter("chq", TextBox2.Text)
        Dim chq1(0) As ReportParameter
        chq1(0) = chq
        formata4.ReportViewer1.LocalReport.SetParameters(chq1)

        Dim tpe As New ReportParameter("tpe", TextBox3.Text)
        Dim tpe1(0) As ReportParameter
        tpe1(0) = tpe
        formata4.ReportViewer1.LocalReport.SetParameters(tpe1)

        Dim effet As New ReportParameter("effet", TextBox4.Text)
        Dim effet1(0) As ReportParameter
        effet1(0) = effet
        formata4.ReportViewer1.LocalReport.SetParameters(effet1)

        Dim vir As New ReportParameter("vir", TextBox5.Text)
        Dim vir1(0) As ReportParameter
        vir1(0) = vir
        formata4.ReportViewer1.LocalReport.SetParameters(vir1)

        Dim total As New ReportParameter("total", Label9.Text.Replace(" DHs", ""))
        Dim total1(0) As ReportParameter
        total1(0) = total
        formata4.ReportViewer1.LocalReport.SetParameters(total1)

        Dim clt As New ReportParameter("clt", "Client")
        Dim clt1(0) As ReportParameter
        clt1(0) = clt
        formata4.ReportViewer1.LocalReport.SetParameters(clt1)

        Dim appPath As String = Application.StartupPath()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

        Dim img As New ReportParameter("image", "File:\" + SavePath, True)
        Dim img1(0) As ReportParameter
        img1(0) = img
        formata4.ReportViewer1.LocalReport.SetParameters(img1)


        formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
        formata4.ReportViewer1.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next
        Dim printDoc As New PrintDocument()
        m_currentPageIndex = 0
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
    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("raproche")
        For i = 0 To DataGridView3.Rows.Count - 1
            dt.Rows.Add(DataGridView3.Rows(i).Cells(0).Value, DataGridView3.Rows(i).Cells(1).Value, DataGridView3.Rows(i).Cells(3).Value, DataGridView3.Rows(i).Cells(4).Value, "", "", DataGridView3.Rows(i).Cells(5).Value, "")

        Next

        ReportToPrint = New LocalReport()
        formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\etatencaissement.rdlc"
        formata4.ReportViewer1.LocalReport.DataSources.Clear()
        formata4.ReportViewer1.LocalReport.EnableExternalImages = True
        Dim du As New ReportParameter("du", DateTimePicker6.Text)
        Dim du1(0) As ReportParameter
        du1(0) = du
        formata4.ReportViewer1.LocalReport.SetParameters(du1)

        Dim au As New ReportParameter("au", DateTimePicker5.Text)
        Dim au1(0) As ReportParameter
        au1(0) = au
        formata4.ReportViewer1.LocalReport.SetParameters(au1)

        Dim etat As New ReportParameter("etat", "Etat des Règlements")
        Dim etat1(0) As ReportParameter
        etat1(0) = etat
        formata4.ReportViewer1.LocalReport.SetParameters(etat1)


        Dim client As New ReportParameter("client", ComboBox3.Text)
        Dim client1(0) As ReportParameter
        client1(0) = client
        formata4.ReportViewer1.LocalReport.SetParameters(client1)

        Dim esp As New ReportParameter("esp", TextBox10.Text)
        Dim esp1(0) As ReportParameter
        esp1(0) = esp
        formata4.ReportViewer1.LocalReport.SetParameters(esp1)

        Dim chq As New ReportParameter("chq", TextBox9.Text)
        Dim chq1(0) As ReportParameter
        chq1(0) = chq
        formata4.ReportViewer1.LocalReport.SetParameters(chq1)

        Dim tpe As New ReportParameter("tpe", TextBox8.Text)
        Dim tpe1(0) As ReportParameter
        tpe1(0) = tpe
        formata4.ReportViewer1.LocalReport.SetParameters(tpe1)

        Dim effet As New ReportParameter("effet", TextBox7.Text)
        Dim effet1(0) As ReportParameter
        effet1(0) = effet
        formata4.ReportViewer1.LocalReport.SetParameters(effet1)

        Dim vir As New ReportParameter("vir", TextBox6.Text)
        Dim vir1(0) As ReportParameter
        vir1(0) = vir
        formata4.ReportViewer1.LocalReport.SetParameters(vir1)

        Dim total As New ReportParameter("total", Label7.Text.Replace(" DHs", ""))
        Dim total1(0) As ReportParameter
        total1(0) = total
        formata4.ReportViewer1.LocalReport.SetParameters(total1)

        Dim clt As New ReportParameter("clt", "Fournisseur")
        Dim clt1(0) As ReportParameter
        clt1(0) = clt
        formata4.ReportViewer1.LocalReport.SetParameters(clt1)

        Dim appPath As String = Application.StartupPath()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

        Dim img As New ReportParameter("image", "File:\" + SavePath, True)
        Dim img1(0) As ReportParameter
        img1(0) = img
        formata4.ReportViewer1.LocalReport.SetParameters(img1)


        formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
        formata4.ReportViewer1.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next
        Dim printDoc As New PrintDocument()
        m_currentPageIndex = 0
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
    End Sub



    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        DataGridView2.Rows.Clear()

        If ComboBox1.Text = "Rapprochement Achats" Then
            If ComboBox5.Text = "Tout" Then
                If ComboBox6.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("select * from cheques where fac IS NULL and tick IS NULL order by date desc", conn2)
                Else
                    adpt = New MySqlDataAdapter("select * from cheques where fac IS NULL and tick IS NULL and bq = '" & ComboBox6.Text & "' order by date desc", conn2)
                End If
            End If
            If ComboBox5.Text = "Pointé" Then
                If ComboBox6.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("select * from cheques where fac IS NULL and tick IS NULL and etat = '1' order by date desc", conn2)
                Else
                    adpt = New MySqlDataAdapter("select * from cheques where fac IS NULL and tick IS NULL and bq = '" & ComboBox6.Text & "' and etat = '1' order by date desc", conn2)
                End If
            End If
            If ComboBox5.Text = "Non Pointé" Then
                If ComboBox6.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("select * from cheques where fac IS NULL and tick IS NULL and etat = '0' order by date desc", conn2)
                Else
                    adpt = New MySqlDataAdapter("select * from cheques where fac IS NULL and tick IS NULL and bq = '" & ComboBox6.Text & "' and etat = '0' order by date desc", conn2)
                End If
            End If
            adpt.SelectCommand.Parameters.Clear()
            DataGridView2.Columns(2).Visible = False
            DataGridView2.Columns(3).Visible = True
        End If

        If ComboBox1.Text = "Rapprochement Ventes" Then
            If ComboBox5.Text = "Tout" Then
                If ComboBox6.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("select * from cheques where achat IS NULL order by date desc", conn2)
                Else
                    adpt = New MySqlDataAdapter("select * from cheques where achat IS NULL and bq = '" & ComboBox6.Text & "' order by date desc", conn2)
                End If
            End If
            If ComboBox5.Text = "Pointé" Then
                If ComboBox6.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("select * from cheques where achat IS NULL and etat = '1' order by date desc", conn2)
                Else
                    adpt = New MySqlDataAdapter("select * from cheques where achat IS NULL and bq = '" & ComboBox6.Text & "' and etat = '1' order by date desc", conn2)
                End If
            End If
            If ComboBox5.Text = "Non Pointé" Then
                If ComboBox6.Text = "Tout" Then
                    adpt = New MySqlDataAdapter("select * from cheques where achat IS NULL and etat = '0' order by date desc", conn2)
                Else
                    adpt = New MySqlDataAdapter("select * from cheques where achat IS NULL and bq = '" & ComboBox6.Text & "' and etat = '0' order by date desc", conn2)
                End If
            End If

            adpt.SelectCommand.Parameters.Clear()
            DataGridView2.Columns(3).Visible = False
            DataGridView2.Columns(2).Visible = True

        End If
        Dim table As New DataTable
        adpt.Fill(table)
        Dim modep As String
        Dim daty As String
        Dim daty2 As String
        Dim details As String
        Dim client As String = ""
        Dim frs As String = ""
        Dim pointage As String = ""
        Dim sum As Double = 0
        Dim sump As Double = 0
        Dim sumnp As Double = 0
        Dim sumimp As Double = 0
        For i = 0 To table.Rows.Count() - 1

            If table.Rows(i).Item(1) = "tpe" Then
                modep = "Carte bancaire"
                details = "STAN : " & table.Rows(i).Item(4) & vbCrLf & "AUT : " & table.Rows(i).Item(2)
            Else
                If table.Rows(i).Item(15) = "Chèque" Then
                    details = "CHQ N° : " & table.Rows(i).Item(4)
                    modep = "Chèque"
                End If
                If table.Rows(i).Item(15) = "Virement" Then
                    details = "VIR N° : " & table.Rows(i).Item(4)
                    modep = "Virement"
                End If
                If table.Rows(i).Item(15) = "Effet" Then
                    details = "Effet N° : " & table.Rows(i).Item(4)
                    modep = "Effet"
                End If
            End If
            If table.Rows(i).Item(5) = "tpe" Then
                daty = "-"
                daty2 = "-"
            Else
                daty = Convert.ToDateTime(table.Rows(i).Item(5)).ToString("dd/MM/yyyy")
                daty2 = Convert.ToDateTime(table.Rows(i).Item(5)).ToString("yyyy/MM/dd")

            End If
            If IsDBNull(table.Rows(i).Item(8)) Then
                client = table.Rows(i).Item(14)
            Else
                frs = table.Rows(i).Item(14)
            End If
            If table.Rows(i).Item(9) = 0 Then
                pointage = "..."
                sumnp = sumnp + Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ","))
            Else
                pointage = "Pointé le " & Format(table.Rows(i).Item(11), "dd/MM/yyyy")
                sump = sump + Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ","))
            End If
            Dim imp As String = "..."
            If table.Rows(i).Item(16) = 1 Then
                imp = "Impayé"
                sumimp = sumimp + Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ","))
            End If
            DataGridView2.Rows.Add(Convert.ToDateTime(table.Rows(i).Item(6)).ToString("dd/MM/yyyy"), daty, client, frs, modep, details, table.Rows(i).Item(10), Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ",")).ToString("N2"), table.Rows(i).Item(0), pointage, Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ",")), Convert.ToDateTime(table.Rows(i).Item(6)).ToString("yyyy/MM/dd"), daty2, imp)
            sum = sum + Convert.ToDouble(table.Rows(i).Item(12).ToString.Replace(".", ","))
        Next
        TextBox16.Text = sum.ToString("N2")
        TextBox15.Text = sump.ToString("N2")
        TextBox13.Text = sumnp.ToString("N2")
        TextBox11.Text = sumimp.ToString("N2")
        For Each row As DataGridViewRow In DataGridView2.Rows
            If row.Cells(9).Value = "..." Then
                row.DefaultCellStyle.ForeColor = Color.Red
            Else
                row.DefaultCellStyle.ForeColor = Color.Black
            End If
        Next
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

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
    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged

    End Sub

    Private m_streams As IList(Of Stream)
    Dim ReportToPrint, ReportToPrintcopy As LocalReport
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
End Class