Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Public Class archive

    Dim adpt, adpt2, adpt3 As MySqlDataAdapter

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

    Private Sub archive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Dim appPath As String = Application.StartupPath()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = System.IO.Path.Combine(SaveDirectory, imgName)
        If System.IO.File.Exists(SavePath) Then
            PictureBox2.Image = Image.FromFile(SavePath)
        End If

        Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))


        Dim pkInstalledPrinters As String

        ' Find all printers installed
        For Each pkInstalledPrinters In
        PrinterSettings.InstalledPrinters
            ComboBox5.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        ' Set the combo to the first printer in the list
        ComboBox5.SelectedIndex = 0

        Label2.Text = user
        Dim w = Screen.PrimaryScreen.WorkingArea.Width
        Dim h = My.Computer.Screen.WorkingArea.Size.Height
        Me.Width = w
        Me.Height = h
        Me.Location = New Point(0, 0)

        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim tablebq As New DataTable
        adpt.Fill(tablebq)

        For i = 0 To tablebq.Rows.Count - 1
            ComboBox8.Items.Add(tablebq.Rows(i).Item(1))
            ComboBox7.Items.Add(tablebq.Rows(i).Item(1))
        Next

        adpt = New MySqlDataAdapter("select * from orders WHERE REPLACE(reste,',','.') > 0 and parked='no' and etat = @etat order by OrderID desc", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")

        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        DataGridView1.Rows.Clear()
        ventedetail.DataGridView3.Rows.Clear()

        For i = 0 To table.Rows.Count - 1

            Dim s As String = ""
            Dim t As String = ""
            s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")
            t = Convert.ToString(table.Rows(i).Item(2)).Replace(".", ",")


            DataGridView1.Rows.Add("Tick N° " & table.Rows(i).Item(0), Convert.ToDecimal(table.Rows(i).Item(2).ToString.Replace(".", ",")).ToString("# ##0.00"), table.Rows(i).Item(6) & " %", Convert.ToDecimal(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("# ##0.00"), Convert.ToDecimal(table.Rows(i).Item(13).ToString.Replace(".", ",")).ToString("# ##0.00"), Format(table.Rows(i).Item(1), "dd/MM/yyyy HH:mm:ss"), table.Rows(i).Item(3), table.Rows(i).Item(10), table.Rows(i).Item(12), table.Rows(i).Item(0), table.Rows(i).Item(9), table.Rows(i).Item(17))

            sum = sum + Convert.ToDouble(table.Rows(i).Item(13).ToString.Replace(" ", "").Replace(".", ","))
        Next
        IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00")
        IconButton2.Text = table.Rows.Count
        DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"

        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(3).Value <= 0 Then
                row.Cells(3).Style.ForeColor = Color.Red
                row.Cells(4).Style.ForeColor = Color.Red
            Else
                row.Cells(3).Style.ForeColor = Color.Green
                row.Cells(4).Style.ForeColor = Color.Red
            End If
        Next


        adpt = New MySqlDataAdapter("select client from clients order by client asc", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count <> 0 Then
            ComboBox2.Items.Clear()
            ComboBox2.Items.Add("Tout")
            For i = 0 To table2.Rows.Count - 1
                ComboBox2.Items.Add(table2.Rows(i).Item(0))
            Next
            ComboBox2.SelectedIndex = 0

        End If
        ComboBox1.SelectedIndex = 0
        DataGridView1.ClearSelection()
        DataGridView1.MultiSelect = True

    End Sub
    Private Sub DataGridView1_CellMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick

        If e.RowIndex >= 0 Then
            ventedetail.Show()
            ventedetail.Label1.Text = "Vente N° :"
            ventedetail.Label3.Text = 0
            If IconButton19.Visible = True Then
                ventedetail.IconButton4.Visible = False

            End If
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim id = row.Cells(9).Value.ToString

            ventedetail.DataGridView3.Rows.Clear()

            ventedetail.Label2.Text = id
            ventedetail.Label4.Text = row.Cells(11).Value.ToString
            ventedetail.Label7.Text = row.Cells(6).Value.ToString

            Dim nonpaye As Boolean = False
            Dim sumreste As Double = Convert.ToDouble(sumreste + DataGridView1.SelectedRows(0).Cells(4).Value.ToString.Replace(" ", "")).ToString("#0.00")
            adpt = New MySqlDataAdapter("select ProductID,name,Quantity,rms,Total,marge,gr from orderdetails where OrderID = '" + id + "' and type = 'ventes' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            For i = 0 To table.Rows.Count - 1

                ventedetail.DataGridView3.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), Convert.ToString(table.Rows(i).Item(3)).Replace(".", ","), Convert.ToString(table.Rows(i).Item(4)).Replace(".", ","), Convert.ToString(table.Rows(i).Item(5)).Replace(".", ","), table.Rows(i).Item(6))

            Next
            If sumreste > 0 Then
                nonpaye = True
            End If
            If nonpaye = False Then
                ventedetail.Label5.Text = DataGridView1.SelectedRows(0).Cells(7).Value.ToString
            End If

            ventedetail.Label6.Text = sumreste.ToString("#0.00").Replace(".", ",")

        End If

    End Sub



    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Panel6.Visible = False
        Try
            If ComboBox2.Text = "Tout" Then
                If ComboBox1.Text = "" OrElse ComboBox1.Text = "Tout" Then
                    If CheckBox1.Checked = False Then

                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and etat = @etat order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                    Else
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                    End If
                End If

                If ComboBox1.Text = "Espèce" Then
                    If CheckBox1.Checked = False Then
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and modePayement = @mode and etat=@etat order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "Espèce")
                        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                    Else
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and modePayement = @mode order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "Espèce")
                    End If
                End If

                If ComboBox1.Text = "Chèque" Then
                    If CheckBox1.Checked = False Then
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and modePayement = @mode and etat = @etat order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "Chèque")
                        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                    Else
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and modePayement = @mode order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "Chèque")
                    End If
                End If

                If ComboBox1.Text = "TPE" Then
                    If CheckBox1.Checked = False Then
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and modePayement = @mode and etat = @etat order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "TPE")
                        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                    Else
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and modePayement = @mode order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "TPE")
                    End If
                End If
                If ComboBox1.Text = "Crédit" Then
                    If CheckBox1.Checked = False Then

                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and REPLACE(reste,',','.') > 0 and etat = @etat order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                    Else
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and REPLACE(reste,',','.') > 0 order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                    End If
                End If
            Else
                If ComboBox1.Text = "" OrElse ComboBox1.Text = "Tout" Then
                    If CheckBox1.Checked = False Then
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and etat = @etat and client = @clt order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                    Else
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and client = @clt order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                    End If
                End If

                If ComboBox1.Text = "Espèce" Then
                    If CheckBox1.Checked = False Then
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and modePayement = @mode and etat=@etat and client = @clt order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "Espèce")
                        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                    Else
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and modePayement = @mode and client = @clt order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "Espèce")
                        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                    End If

                End If

                If ComboBox1.Text = "Chèque" Then
                    If CheckBox1.Checked = False Then

                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and modePayement = @mode and etat = @etat and client = @clt order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "Chèque")
                        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                    Else
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and modePayement = @mode and client = @clt order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "Chèque")
                        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                    End If
                End If

                If ComboBox1.Text = "TPE" Then
                    If CheckBox1.Checked = False Then
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and modePayement = @mode and etat = @etat and client = @clt order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "TPE")
                        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                    Else
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and modePayement = @mode and client = @clt order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "TPE")
                        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                    End If
                End If

                If ComboBox1.Text = "Crédit" Then
                    If CheckBox1.Checked = False Then
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and REPLACE(reste,',','.') > 0 and etat = @etat and client = @clt and reste > 0 order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                    Else
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE OrderDate BETWEEN @datedebut AND @datefin and parked='no' and REPLACE(reste,',','.') > 0 and client = @clt and reste > 0 order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
                        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date.AddDays(1))
                        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                    End If
                End If
            End If

            Dim table As New DataTable
            adpt.Fill(table)
            Dim sum As Double = 0
            Dim cnt As Integer = 0
            DataGridView1.Rows.Clear()
            ventedetail.DataGridView3.Rows.Clear()

            For i = 0 To table.Rows.Count - 1

                Dim s As String = ""
                s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",").Replace(" ", "")
                DataGridView1.Rows.Add("Tick N° " & table.Rows(i).Item(0), Convert.ToDecimal(table.Rows(i).Item(2).ToString.Replace(".", ",")).ToString("# ##0.00"), table.Rows(i).Item(6) & " %", Convert.ToDecimal(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("# ##0.00"), Convert.ToDecimal(table.Rows(i).Item(13).ToString.Replace(".", ",")).ToString("# ##0.00"), Format(table.Rows(i).Item(1), "dd/MM/yyyy HH:mm:ss"), table.Rows(i).Item(3), table.Rows(i).Item(10), table.Rows(i).Item(12), table.Rows(i).Item(0), table.Rows(i).Item(9), table.Rows(i).Item(17))

            Next
            For i = 0 To DataGridView1.Rows.Count() - 1
                If ComboBox1.Text = "Crédit" Then
                    sum += Convert.ToDouble(DataGridView1.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ","))
                Else
                    sum += Convert.ToDouble(DataGridView1.Rows(i).Cells(1).Value.ToString.Replace(" ", "").Replace(".", ","))

                End If
            Next
            IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00")
            IconButton2.Text = table.Rows.Count
            DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"
            DataGridView1.ClearSelection()
            For Each row As DataGridViewRow In DataGridView1.Rows
                If Convert.ToDouble(row.Cells(3).Value.ToString.Replace(" ", "")) <= 0 Then
                    row.Cells(3).Style.ForeColor = Color.Red
                    row.Cells(4).Style.ForeColor = Color.Red
                Else
                    row.Cells(3).Style.ForeColor = Color.Green
                        row.Cells(4).Style.ForeColor = Color.Red
                    End If
                If row.Cells(10).Value.ToString = "fac" Then
                    row.DefaultCellStyle.ForeColor = Color.Blue
                End If
            Next

            Panel3.Visible = False
        Catch ex As MySqlException
            Dim yesornoMsg2 As New eror
            yesornoMsg2.Label14.Text = ex.Message
            yesornoMsg2.Panel5.Visible = True
            yesornoMsg2.ShowDialog()
            'If Msgboxresult = True Then
        End Try
    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        Try
            DataGridView1.Rows.Clear()
            adpt = New MySqlDataAdapter("select * from infos", conn2)
            Dim tableimg As New DataTable
            adpt.Fill(tableimg)
            adpt = New MySqlDataAdapter("select * from orders WHERE Day(orders.OrderDate) = @day and month(orders.OrderDate)=@month and year(orders.OrderDate)=@year and parked='no' and etat = @etat order by OrderID desc", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
            adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
            adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
            adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")

            Dim table As New DataTable
            adpt.Fill(table)
            Dim sum As Double = 0
            Dim cnt As Integer = 0
            DataGridView1.Rows.Clear()
            ventedetail.DataGridView3.Rows.Clear()

            For i = 0 To table.Rows.Count - 1

                Dim s As String = ""
                Dim t As String = ""
                s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")
                t = Convert.ToString(table.Rows(i).Item(2)).Replace(".", ",")


                DataGridView1.Rows.Add("Tick N° " & table.Rows(i).Item(0), Convert.ToDecimal(table.Rows(i).Item(2).ToString.Replace(".", ",")).ToString("# ##0.00"), table.Rows(i).Item(6) & " %", Convert.ToDecimal(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("# ##0.00"), Convert.ToDecimal(table.Rows(i).Item(13).ToString.Replace(".", ",")).ToString("# ##0.00"), Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(10), table.Rows(i).Item(12), table.Rows(i).Item(0), table.Rows(i).Item(9), table.Rows(i).Item(17))

                sum = sum + Convert.ToDouble(t)
            Next
            IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00")
            IconButton2.Text = table.Rows.Count
            DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"


            DateTimePicker1.Text = Today
            DateTimePicker2.Text = Today
        Catch ex As MySqlException
            Dim yesornoMsg2 As New eror
            yesornoMsg2.Label14.Text = ex.Message
            yesornoMsg2.Panel5.Visible = True
            yesornoMsg2.ShowDialog()
            'If Msgboxresult = True Then
        End Try

    End Sub

    Private Sub IconButton16_Click(sender As Object, e As EventArgs) Handles IconButton16.Click
        Dim yesornoMsg2 As New yesorno
        yesornoMsg2.Label14.Text = "Voulez-vous vraiment quitter ?"
        'yesornoMsg2.Panel5.Visible = True
        yesornoMsg2.ShowDialog()
        If Msgboxresult = True Then
            Dim log = New Form1()
            log.Show()
            Me.Close()
        End If
    End Sub


    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub
    Dim m_currentPageIndex As Integer
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
    Private Sub IconButton18_Click(sender As Object, e As EventArgs) Handles IconButton18.Click

        Dim dte As String = DateTimePicker1.Text & " - " & DateTimePicker2.Text
        Dim ds As New DataSet1
        Dim dt As New DataTable
        Dim espèce As Double = 0
        Dim crédit As Double = 0
        Dim chèque As Double = 0
        dt = ds.Tables("Vente")
        For i = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(8).Value)

            If DataGridView1.Rows(i).Cells(7).Value = "Crédit" Then
                crédit = Math.Round(crédit + Convert.ToDouble(DataGridView1.Rows(i).Cells(1).Value), 2)
            End If
            If DataGridView1.Rows(i).Cells(7).Value = "Espèce" Then
                espèce = Math.Round(espèce + Convert.ToDouble(DataGridView1.Rows(i).Cells(1).Value), 2)
            End If
            If DataGridView1.Rows(i).Cells(7).Value = "Chèque" Then
                chèque = Math.Round(chèque + Convert.ToDouble(DataGridView1.Rows(i).Cells(1).Value), 2)
            End If

        Next
        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\VentEtatGlobal.rdlc"
        ReportToPrint.DataSources.Clear()

        Dim dateM As New ReportParameter("date", dte)
        Dim dateM1(0) As ReportParameter
        dateM1(0) = dateM
        ReportToPrint.SetParameters(dateM1)

        Dim espèces As New ReportParameter("espèce", espèce)
        Dim espèce1(0) As ReportParameter
        espèce1(0) = espèces
        ReportToPrint.SetParameters(espèce1)

        Dim crédits As New ReportParameter("crédit", crédit)
        Dim crédit1(0) As ReportParameter
        crédit1(0) = crédits
        ReportToPrint.SetParameters(crédit1)

        Dim chèques As New ReportParameter("chèque", chèque)
        Dim chèque1(0) As ReportParameter
        chèque1(0) = chèques
        ReportToPrint.SetParameters(chèque1)


        ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
        ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next
        Dim printDoc As New PrintDocument()
        Dim printname As String = System.Configuration.ConfigurationSettings.AppSettings("printar")
        printDoc.PrinterSettings.PrinterName = ComboBox5.Text
        Dim ps As New PrinterSettings()
        ps.PrinterName = printDoc.PrinterSettings.PrinterName
        printDoc.PrinterSettings = ps

        AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
        m_currentPageIndex = 0
        printDoc.Print()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox2.Text = "" Then
                adpt = New MySqlDataAdapter("select * from orders WHERE Day(orders.OrderDate) = @day and month(orders.OrderDate)=@month and year(orders.OrderDate)=@year and etat = @etat", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
                adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
                adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
                adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")

                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                DataGridView1.Rows.Clear()
                For i = 0 To table.Rows.Count - 1



                    DataGridView1.Rows.Add("Tick N° " & table.Rows(i).Item(0), Convert.ToDecimal(table.Rows(i).Item(2).ToString.Replace(".", ",")).ToString("# ##0.00"), table.Rows(i).Item(6) & " %", Convert.ToDecimal(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("# ##0.00"), Convert.ToDecimal(table.Rows(i).Item(13).ToString.Replace(".", ",")).ToString("# ##0.00"), Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(10), table.Rows(i).Item(12), table.Rows(i).Item(0), table.Rows(i).Item(9), table.Rows(i).Item(17))

                    sum = sum + table.Rows(i).Item(2)
                Next

                IconButton1.Text = Math.Round(sum, 2)
                IconButton2.Text = table.Rows.Count

            Else
                adpt = New MySqlDataAdapter("select * from orders WHERE OrderID = @va ", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@va", TextBox2.Text)


                Dim table As New DataTable
                adpt.Fill(table)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                DataGridView1.Rows.Clear()

                For i = 0 To table.Rows.Count - 1



                    DataGridView1.Rows.Add("Tick N° " & table.Rows(i).Item(0), Convert.ToDecimal(table.Rows(i).Item(2).ToString.Replace(".", ",")).ToString("# ##0.00"), table.Rows(i).Item(6) & " %", Convert.ToDecimal(table.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("# ##0.00"), Convert.ToDecimal(table.Rows(i).Item(13).ToString.Replace(".", ",")).ToString("# ##0.00"), Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(10), table.Rows(i).Item(12), table.Rows(i).Item(0), table.Rows(i).Item(9), table.Rows(i).Item(17))

                    sum = sum + table.Rows(i).Item(2)
                Next

                IconButton1.Text = Math.Round(sum, 2)
                IconButton2.Text = table.Rows.Count

            End If
            TextBox2.Text = ""
            DataGridView1.ClearSelection()
        End If
    End Sub



    Private Sub IconButton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Facturation des ventes'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                If ComboBox2.Text <> "Tout" Then

                    Panel7.Visible = True
                    IconButton33.Visible = True
                    IconButton37.Visible = True
                    IconButton38.Visible = True
                    Panel7.BringToFront()

                    If DataGridView1.Columns.Count = 12 Then

                        ' Créer la colonne avec des cases à cocher
                        Dim checkboxColumn As New DataGridViewCheckBoxColumn()
                        checkboxColumn.HeaderText = "Sélectionner"
                        checkboxColumn.Name = "CheckBoxColumn"
                        checkboxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells ' Définir l'AutoSizeMode sur AllCells
                        ' Ajouter la colonne au début de la DataGridView
                        DataGridView1.Columns.Insert(0, checkboxColumn)

                        ' Parcourir les lignes pour cocher toutes les cases
                        For Each row As DataGridViewRow In DataGridView1.Rows
                            row.Cells("CheckBoxColumn").Value = False
                        Next
                        DataGridView1.ClearSelection()

                    End If
                Else
                    Dim yesornoMsg2 As New yesorno
                    yesornoMsg2.Label14.Text = "Veuillez séléectionner un client ! "
                    yesornoMsg2.Panel5.Visible = True
                    yesornoMsg2.ShowDialog()
                    'If Msgboxresult = True Then
                End If
            Else
                Dim yesornoMsg2 As New eror
                yesornoMsg2.Label14.Text = "Vous n'avez pas l'autorisation !"
                yesornoMsg2.Panel5.Visible = True
                yesornoMsg2.ShowDialog()
                'If Msgboxresult = True Then
            End If
        End If

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Me.Show()
    End Sub

    Private Sub iconbutton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        stock.Show()
        Me.Close()
    End Sub

    Private Sub iconbutton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
        dashboard.Show()
        dashboard.IconButton9.Visible = True
        dashboard.IconButton8.Visible = False
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
    Private Sub IconButton23_Click(sender As Object, e As EventArgs) Handles IconButton23.Click
        Charges.Show()
        Me.Close()
    End Sub

    Private Sub IconButton24_Click(sender As Object, e As EventArgs) Handles IconButton24.Click
        banques.Show()
        Me.Close()
    End Sub

    Private Sub IconButton8_Click(sender As Object, e As EventArgs)
        product.Show()
        Me.Close()

    End Sub

    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        Home.Show()
        Me.Close()
    End Sub

    Private Sub IconButton3_Click_1(sender As Object, e As EventArgs) Handles IconButton3.Click
        factures.Show()
        Me.Close()
    End Sub

    Private Sub IconButton19_Click(sender As Object, e As EventArgs) Handles IconButton19.Click
        IconButton7.PerformClick()
        Me.Close()

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

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        'If ComboBox2.Text = "Tout" Then
        '    Label9.Visible = False
        '    Label9.Text = 0.00
        '    Label8.Visible = False
        '    DataGridView1.ClearSelection()
        'Else
        '    adpt = New MySqlDataAdapter("select * from clients where client = '" & ComboBox2.Text & "'", conn2)
        '    Dim table As New DataTable
        '    adpt.Fill(table)
        '    Label9.Visible = True
        '    Label8.Visible = True
        '    Label9.Text = Convert.ToDouble(table.Rows(0).Item(17)).ToString("#0.00")


        '    adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and etat = @etat and client = @clt order by OrderID desc", conn2)
        '    adpt.SelectCommand.Parameters.Clear()
        '    adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
        '    adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
        '    adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
        '    Dim table As New DataTable
        '    adpt.Fill(table)
        '    Dim sum As Double = 0
        '    Dim cnt As Integer = 0
        '    DataGridView1.Rows.Clear()
        '    For i = 0 To table.Rows.Count - 1

        '        Dim s As String = ""
        '        Dim t As String = ""
        '        s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")
        '        t = Convert.ToString(table.Rows(i).Item(2)).Replace(".", ",")


        '        DataGridView1.Rows.Add("Tick N° " & table.Rows(i).Item(0), table.Rows(i).Item(2), table.Rows(i).Item(6) & " %", table.Rows(i).Item(4), table.Rows(i).Item(13), Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(10), table.Rows(i).Item(12), table.Rows(i).Item(0))

        '        sum += Convert.ToDouble(table.Rows(i).Item(2).ToString.Replace(".", ","))
        '    Next
        '    IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00") 
        '    IconButton2.Text = table.Rows.Count
        '    DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
        '    DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
        '    DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
        '    DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"

        '    DataGridView1.ClearSelection()


        '    ' Ajouter la colonne au début de la DataGridView

        'End If
    End Sub
    Private Sub IconButton20_Click(sender As Object, e As EventArgs) Handles IconButton20.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Encaissement BL / Ventes'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                If ComboBox2.Text = "Tout" Then
                    Dim yesornoMsg2 As New yesorno
                    yesornoMsg2.Label14.Text = "Merci de Choisir un client !"
                    yesornoMsg2.Panel5.Visible = True
                    yesornoMsg2.ShowDialog()
                    'If Msgboxresult = True Then
                Else

                    Panel7.Visible = True
                    Panel7.BringToFront()
                    If DataGridView1.Columns.Count = 12 Then
                        ' Créer la colonne avec des cases à cocher
                        Dim checkboxColumn As New DataGridViewCheckBoxColumn()
                        checkboxColumn.HeaderText = "Sélectionner"
                        checkboxColumn.Name = "CheckBoxColumn"
                        checkboxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells ' Définir l'AutoSizeMode sur AllCells
                        ' Ajouter la colonne au début de la DataGridView
                        DataGridView1.Columns.Insert(0, checkboxColumn)

                        ' Parcourir les lignes pour cocher toutes les cases
                        For Each row As DataGridViewRow In DataGridView1.Rows
                            row.Cells("CheckBoxColumn").Value = False
                        Next
                        DataGridView1.ClearSelection()
                    End If
                End If


            Else
                Dim yesornoMsg2 As New eror
                yesornoMsg2.Label14.Text = "Vous n'avez pas l'autorisation !"
                yesornoMsg2.Panel5.Visible = True
                yesornoMsg2.ShowDialog()
                'If Msgboxresult = True Then
            End If
        End If


    End Sub

    Private Sub IconButton30_Click(sender As Object, e As EventArgs) Handles IconButton30.Click
        Panel3.Visible = False
        tpepanel.Visible = False
        chqpanel.Visible = False
        DataGridView1.Visible = True
    End Sub

    Private Sub IconButton29_Click(sender As Object, e As EventArgs) Handles IconButton29.Click

        Dim Rep As Integer
        Dim sql3 As String
        Dim cmd3 As MySqlCommand
        Dim yesornoMsg2 As New yesorno
        yesornoMsg2.Label14.Text = "Voulez-vous vraiment effectuer cet encaissement "
        'yesornoMsg2.Panel5.Visible = True
        yesornoMsg2.ShowDialog()
        If Msgboxresult = True Then

            conn2.Close()
            conn2.Open()

            Dim client As String
            Dim resteencaisse As Decimal = Convert.ToDecimal(TextBox3.Text.Replace(" ", "").Replace(".", ","))

            adpt = New MySqlDataAdapter("SELECT * FROM reglement_fac order by id desc", conn2)
            Dim tablereglement_id As New DataTable
            adpt.Fill(tablereglement_id)
            Dim regl_id As Integer
            If tablereglement_id.Rows.Count = 0 Then
                regl_id = 1
            Else
                regl_id = tablereglement_id.Rows(0).Item(0) + 1
            End If
            Dim selectedRows As New List(Of DataGridViewRow)
            For Each row As DataGridViewRow In DataGridView1.Rows
                ' Vérifier si la case à cocher de la ligne est cochée
                If Convert.ToBoolean(row.Cells("CheckBoxColumn").Value) = True Then
                    selectedRows.Add(row)
                End If
            Next
            For i As Integer = selectedRows.Count - 1 To 0 Step -1
                Dim row As DataGridViewRow = selectedRows(i)
                sql3 = "UPDATE `orders` SET `paye`=@paye,`modePayement`=@mode,`reste`=@reste WHERE `OrderID` = @id"
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@id", row.Cells(10).Value)
                    If (Convert.ToDecimal(row.Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")) + resteencaisse).ToString.Replace(".", ",") >= Convert.ToDecimal(row.Cells(2).Value.ToString.Replace(".", ",").Replace(" ", "")) Then
                        cmd3.Parameters.AddWithValue("@paye", Convert.ToDecimal(row.Cells(2).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString.Replace(".", ",").Replace(" ", ""))
                        cmd3.Parameters.AddWithValue("@reste", 0.00)
                        cmd3.Parameters.AddWithValue("@mode", ComboBox3.Text)
                        cmd3.ExecuteNonQuery()
                        client = row.Cells(9).Value.ToString()
                        sql3 = "INSERT INTO `reglement_fac`(`reglement`, `fac`, `montant`, `date`) VALUES (@type,@fac,@montant,@date) "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@type", regl_id)
                        cmd3.Parameters.AddWithValue("@fac", row.Cells(10).Value.ToString())
                        cmd3.Parameters.AddWithValue("@montant", Convert.ToDecimal(row.Cells(2).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString.Replace(".", ",").Replace(" ", ""))
                        cmd3.Parameters.AddWithValue("@date", Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd3.ExecuteNonQuery()

                    Else
                        cmd3.Parameters.AddWithValue("@paye", (Convert.ToDecimal(row.Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")) + resteencaisse).ToString.Replace(".", ","))
                        cmd3.Parameters.AddWithValue("@reste", (Convert.ToDecimal(row.Cells(2).Value.ToString.Replace(".", ",").Replace(" ", "")) - Convert.ToDecimal(row.Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")) - resteencaisse).ToString.Replace(" ", "").Replace(".", ","))
                        cmd3.Parameters.AddWithValue("@mode", "Crédit")
                        cmd3.ExecuteNonQuery()
                        client = row.Cells(9).Value.ToString()
                        sql3 = "INSERT INTO `reglement_fac`(`reglement`, `fac`, `montant`, `date`) VALUES (@type,@fac,@montant,@date) "
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@type", regl_id)
                        cmd3.Parameters.AddWithValue("@fac", row.Cells(10).Value.ToString())
                        cmd3.Parameters.AddWithValue("@montant", (resteencaisse).ToString.Replace(".", ","))
                        cmd3.Parameters.AddWithValue("@date", Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd3.ExecuteNonQuery()
                    End If


                sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                If role = "Caissier" Then
                    cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
                Else
                    cmd3.Parameters.AddWithValue("@name", Label2.Text)
                End If
                cmd3.Parameters.AddWithValue("@op", "Encaissement de la Vente : " & row.Cells(10).Value.ToString)
                cmd3.ExecuteNonQuery()


                resteencaisse = resteencaisse - Convert.ToDecimal(row.Cells(5).Value.ToString.Replace(".", ",").Replace(" ", ""))
                    If resteencaisse <= 0 Then
                        Exit For
                    End If

            Next
            Dim clot As String = 0
            If DateTimePicker5.Value.ToString("yyyy-MM-dd") = DateTime.Now.ToString("yyyy-MM-dd") Then
            Else

                clot = 1
            End If
            sql3 = "INSERT INTO `reglement`( `type`, `montant`, `date`, `mode`, `client`,`tick`, `echeance`,`cloture`) VALUES (@type,@montant,@date,@mode,@clt,@achat,@ech,@clot) "
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.Parameters.Clear()
                cmd3.Parameters.AddWithValue("@type", "fac")
                cmd3.Parameters.AddWithValue("@montant", Convert.ToDouble(TextBox3.Text.Replace(".", ",")).ToString.Replace(".", ",").Replace(" ", ""))
                cmd3.Parameters.AddWithValue("@date", Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss"))
                cmd3.Parameters.AddWithValue("@ech", Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss"))
                cmd3.Parameters.AddWithValue("@mode", ComboBox3.Text)
                cmd3.Parameters.AddWithValue("@clt", client)
                cmd3.Parameters.AddWithValue("@achat", regl_id)
                cmd3.Parameters.AddWithValue("@clot", clot)
                cmd3.ExecuteNonQuery()

                If ComboBox3.Text = "TPE" Then
                sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `tick`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('tpe','" + TextBox21.Text + "','tpe','" + TextBox20.Text + "','tpe', '" + Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & regl_id & "','" + ComboBox8.Text + "','" + TextBox3.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & client & "','" & ComboBox3.Text & "')"
                cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.ExecuteNonQuery()
                End If
                If ComboBox3.Text = "Chèque" Then
                    sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `tick`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('" + TextBox8.Text + "','" + TextBox16.Text + "','" + TextBox15.Text + "','" + TextBox14.Text + "','" + Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & regl_id & "','" + ComboBox7.Text + "','" + TextBox3.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & client & "','" & ComboBox3.Text & "')"
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.ExecuteNonQuery()
                End If
                If ComboBox3.Text = "Virement" Then
                    sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `tick`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('" + TextBox8.Text + "','" + TextBox16.Text + "','" + TextBox15.Text + "','" + TextBox14.Text + "','" + Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & regl_id & "','" + ComboBox7.Text + "','" + TextBox3.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & client & "','" & ComboBox3.Text & "')"
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.ExecuteNonQuery()
                End If
                If ComboBox3.Text = "Effet" Then
                    sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `tick`,`bq`,`montant`,`client`,`mode`) 
                    VALUES ('" + TextBox8.Text + "','" + TextBox16.Text + "','" + TextBox15.Text + "','" + TextBox14.Text + "','" + Convert.ToDateTime(DateTimePicker3.Value).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Convert.ToDateTime(DateTimePicker5.Value).ToString("yyyy-MM-dd HH:mm:ss") + "','" & regl_id & "','" + ComboBox7.Text + "','" + TextBox3.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" & client & "','" & ComboBox3.Text & "')"
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.ExecuteNonQuery()
                End If

            Dim yesornoMsg As New success
            yesornoMsg.Label14.Text = "Encaissement bien effectué !"
            yesornoMsg.Panel5.Visible = True
            yesornoMsg.ShowDialog()
            'If Msgboxresult = True Then

            ReportToPrint = New LocalReport()
                ReportToPrint.ReportPath = Application.StartupPath + "\recus.rdlc"
                ReportToPrint.DataSources.Clear()
                ReportToPrint.EnableExternalImages = True

                Dim cliente As New ReportParameter("client", client)
                Dim client1(0) As ReportParameter
                client1(0) = cliente
                ReportToPrint.SetParameters(client1)

                Dim ancien As New ReportParameter("ancien", Convert.ToDouble(Label16.Text).ToString("N2"))
                Dim ancien1(0) As ReportParameter
                ancien1(0) = ancien
                ReportToPrint.SetParameters(ancien1)

                Dim regle As New ReportParameter("regle", Convert.ToDouble(TextBox3.Text.Replace(".", ",").Replace(" ", "")).ToString("N2"))
                Dim regle1(0) As ReportParameter
                regle1(0) = regle
                ReportToPrint.SetParameters(regle1)

                Dim reste As New ReportParameter("reste", (Convert.ToDouble(Label16.Text) - Convert.ToDouble(TextBox3.Text.Replace(".", ",").Replace(" ", ""))).ToString("N2"))
                Dim reste1(0) As ReportParameter
                reste1(0) = reste
                ReportToPrint.SetParameters(reste1)

                Dim daty As New ReportParameter("date", DateTime.Now.ToString("dd/MM/yyyy"))
                Dim daty1(0) As ReportParameter
                daty1(0) = daty
                ReportToPrint.SetParameters(daty1)
                Dim appPath As String = Application.StartupPath()

                Dim SaveDirectory As String = appPath & "\"
                Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                Dim img As New ReportParameter("image", "File:\" + SavePath, True)
                Dim img1(0) As ReportParameter
                img1(0) = img
                ReportToPrint.SetParameters(img1)
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
                Dim printname As String = receiptprinter
                printDoc.PrinterSettings.PrinterName = printname
                Dim ps As New PrinterSettings()
                ps.PrinterName = printDoc.PrinterSettings.PrinterName
                printDoc.PrinterSettings = ps
                AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
                m_currentPageIndex = 0
                printDoc.Print()

                ReportToPrintcopy = New LocalReport()
                ReportToPrintcopy.ReportPath = Application.StartupPath + "\recuscopy.rdlc"
                ReportToPrintcopy.DataSources.Clear()
                ReportToPrintcopy.EnableExternalImages = True

                Dim cliente2 As New ReportParameter("client", client)
                Dim client21(0) As ReportParameter
                client21(0) = cliente2
                ReportToPrintcopy.SetParameters(client21)

                Dim ancien2 As New ReportParameter("ancien", Convert.ToDouble(Label16.Text).ToString("N2"))
                Dim ancien21(0) As ReportParameter
                ancien21(0) = ancien2
                ReportToPrintcopy.SetParameters(ancien21)

                Dim regle2 As New ReportParameter("regle", Convert.ToDouble(TextBox3.Text.Replace(".", ",").Replace(" ", "")).ToString("N2"))
                Dim regle21(0) As ReportParameter
                regle21(0) = regle2
                ReportToPrintcopy.SetParameters(regle21)

                Dim reste2 As New ReportParameter("reste", (Convert.ToDouble(Label16.Text) - Convert.ToDouble(TextBox3.Text.Replace(".", ",").Replace(" ", ""))).ToString("N2"))
                Dim reste21(0) As ReportParameter
                reste21(0) = reste2
                ReportToPrintcopy.SetParameters(reste21)

                Dim daty2 As New ReportParameter("date", DateTime.Now.ToString("dd/MM/yyyy"))
                Dim daty21(0) As ReportParameter
                daty21(0) = daty2
                ReportToPrintcopy.SetParameters(daty21)

                Dim img2 As New ReportParameter("image", "File:\" + SavePath, True)
                Dim img21(0) As ReportParameter
                img21(0) = img2
                ReportToPrintcopy.SetParameters(img21)
                m_streams = New List(Of Stream)()

                ReportToPrintcopy.Render("Image", deviceInfo, AddressOf CreateStream, warnings)



                For Each stream As Stream In m_streams
                    stream.Position = 0
                Next

                Dim printDoc2 As New PrintDocument()
                'Microsoft Print To PDF
                'C80250 Series
                'C80250 Series init
                printDoc2.PrinterSettings.PrinterName = printname
                ps.PrinterName = printDoc.PrinterSettings.PrinterName
                printDoc2.PrinterSettings = ps
                AddHandler printDoc2.PrintPage, AddressOf PrintDocument_PrintPage
                m_currentPageIndex = 0
                printDoc2.Print()

                IconButton27.PerformClick()
                conn2.Close()

                ComboBox2.DroppedDown = True
                ComboBox2.DroppedDown = False
            End If

    End Sub

    Private Sub IconButton35_Click(sender As Object, e As EventArgs) Handles IconButton35.Click
        Panel4.Visible = False
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub

    Private Sub IconButton32_Click(sender As Object, e As EventArgs) Handles IconButton32.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Ajouter Creance'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                Panel4.Visible = True
                Panel4.BringToFront()
            Else
                Dim yesornoMsg As New eror
                yesornoMsg.Label14.Text = "Vous n'avez pas l'autorisation !"
                yesornoMsg.Panel5.Visible = True
                yesornoMsg.ShowDialog()
                'If Msgboxresult = True Then
            End If
        End If

    End Sub

    Private Sub IconButton36_Click(sender As Object, e As EventArgs) Handles IconButton36.Click
        adpt = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim id As Integer
        If table.Rows().Count = 0 Then
            id = 1

        Else
            id = table.Rows(0).Item(0) + 1

        End If
        conn2.Close()
        conn2.Open()
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        Dim clot As String = 0
        If Convert.ToDateTime(DateTimePicker4.Value.Date).ToString("yyyy-MM-dd") = DateTime.Now.ToString("yyyy-MM-dd") Then
        Else

            clot = 1
        End If
        sql2 = "INSERT INTO orders (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`client`,`reste`,`transport`,`livreur`,`cloture`) 
                    VALUES ('" & id & "', '" + DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") + "','" & Convert.ToDouble(TextBox4.Text.ToString.Replace(".", ",")) & "','" + Label2.Text + "','0','0','0','0','" & Convert.ToDouble(TextBox4.Text.ToString.Replace(".", ",")) & "','Crédit','" + ComboBox2.Text + "','" & Convert.ToDouble(TextBox4.Text.ToString.Replace(".", ",")) & "','0','-','" & clot & "')"
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.Clear()
        cmd2.ExecuteNonQuery()

        sql2 = "INSERT INTO orderdetails (`OrderID`, `ProductID`, `Price`,`Quantity`,`Total`,`date`,`pa`,`name`,`marge`,`gr`,`pv`,`tva`) 
                    VALUES ('" & id & "', 'DIVERS','" & Convert.ToDouble(TextBox4.Text.ToString.Replace(".", ",")) & "','1','" & Convert.ToDouble(TextBox4.Text.ToString.Replace(".", ",")) & "','" + DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") + "','" & Convert.ToDouble(TextBox4.Text.ToString.Replace(".", ",")) & "','PRODUIT DIVERS','0','0','" & Convert.ToDouble(TextBox4.Text.ToString.Replace(".", ",")) & "','0')"
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.Clear()
        cmd2.ExecuteNonQuery()


        sql2 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.Clear()
        If role = "Caissier" Then
            cmd2.Parameters.AddWithValue("@name", dashboard.Label2.Text)
        Else
            cmd2.Parameters.AddWithValue("@name", Label2.Text)
        End If
        cmd2.Parameters.AddWithValue("@op", "Ajout d'une créance avec un montant de : " & Convert.ToDouble(TextBox4.Text.Replace(".", ",")).ToString("N2"))
        cmd2.ExecuteNonQuery()

        conn2.Close()
        Panel4.Visible = False
        TextBox4.Text = ""
        TextBox5.Text = ""

        adpt = New MySqlDataAdapter("select * from clients where client = '" & ComboBox2.Text & "'", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)



        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and etat = @etat and client = @clt order by OrderID desc", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
        Dim tablo As New DataTable
        adpt.Fill(tablo)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        DataGridView1.Rows.Clear()
        For i = 0 To tablo.Rows.Count - 1

            Dim s As String = ""
            Dim t As String = ""
            s = Convert.ToString(tablo.Rows(i).Item(1)).Replace(".", ",")
            t = Convert.ToString(tablo.Rows(i).Item(2)).Replace(".", ",")


            DataGridView1.Rows.Add("Tick N° " & tablo.Rows(i).Item(0), Convert.ToDecimal(tablo.Rows(i).Item(2).ToString.Replace(".", ",")).ToString("# ##0.00"), tablo.Rows(i).Item(6) & " %", Convert.ToDecimal(tablo.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("# ##0.00"), Convert.ToDecimal(tablo.Rows(i).Item(13).ToString.Replace(".", ",")).ToString("# ##0.00"), Format(tablo.Rows(i).Item(1), "dd/MM/yyyy"), tablo.Rows(i).Item(3), tablo.Rows(i).Item(10), tablo.Rows(i).Item(12), tablo.Rows(i).Item(0), tablo.Rows(i).Item(9), tablo.Rows(i).Item(17))

            sum = sum + Convert.ToDouble(t)
        Next
        IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",")).ToString("# ##0.00")
        IconButton2.Text = tablo.Rows.Count
        DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"

        DataGridView1.ClearSelection()


    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Four.Show()
        Me.Close()

    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        users.Show()
        Me.Close()

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = False Then
            If ComboBox2.Text = "Tout" Or ComboBox2.Text = "" Then
                Panel6.Visible = False
                Label16.Text = 0.00
                DataGridView1.ClearSelection()
                DataGridView1.Rows.Clear()
                IconButton1.Text = 0.00
            Else

                If CheckBox1.Checked = False Then
                    adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and etat = @etat and client = @clt order by OrderID desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
                    adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                    adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                Else
                    adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and client = @clt order by OrderID desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
                    adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                End If

                Dim tablo As New DataTable
                adpt.Fill(tablo)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                DataGridView1.Rows.Clear()
                For i = 0 To tablo.Rows.Count - 1

                    Dim s As String = ""
                    Dim t As Decimal = 0
                    s = Convert.ToString(tablo.Rows(i).Item(1))
                    t = Convert.ToDecimal(tablo.Rows(i).Item(13).Replace(".", ","))


                    DataGridView1.Rows.Add("Tick N° " & tablo.Rows(i).Item(0), Convert.ToDecimal(tablo.Rows(i).Item(2).ToString.Replace(".", ",")).ToString("# ##0.00"), tablo.Rows(i).Item(6) & " %", Convert.ToDecimal(tablo.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("# ##0.00"), Convert.ToDecimal(tablo.Rows(i).Item(13).ToString.Replace(".", ",")).ToString("# ##0.00"), Format(tablo.Rows(i).Item(1), "dd/MM/yyyy"), tablo.Rows(i).Item(3), tablo.Rows(i).Item(10), tablo.Rows(i).Item(12), tablo.Rows(i).Item(0), tablo.Rows(i).Item(9), tablo.Rows(i).Item(17))
                    sum += t
                Next
                IconButton1.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("#0.00")
                Panel6.Visible = True
                Panel7.BringToFront()
                IconButton20.Visible = True

                Label16.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", ""))
                IconButton2.Text = tablo.Rows.Count
                DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"

                DataGridView1.ClearSelection()
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells(3).Value <= 0 Then
                        row.Cells(3).Style.ForeColor = Color.Red
                        row.Cells(4).Style.ForeColor = Color.Red
                    Else
                        row.Cells(3).Style.ForeColor = Color.Green
                        row.Cells(4).Style.ForeColor = Color.Red
                    End If
                Next
                ' Ajouter la colonne au début de la DataGridView

            End If
        Else
            If ComboBox2.Text = "Tout" Or ComboBox2.Text = "" Then
                Panel6.Visible = False
                Label16.Text = 0.00
                DataGridView1.ClearSelection()
                DataGridView1.Rows.Clear()
                IconButton1.Text = 0.00
            Else

                If CheckBox1.Checked = False Then
                    adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and etat = @etat and client = @clt order by OrderID desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                    adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                Else
                    adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and client = @clt order by OrderID desc", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                End If

                Dim tablo As New DataTable
                adpt.Fill(tablo)
                Dim sum As Double = 0
                Dim cnt As Integer = 0
                DataGridView1.Rows.Clear()
                For i = 0 To tablo.Rows.Count - 1

                    Dim s As String = ""
                    Dim t As Decimal = 0
                    s = Convert.ToString(tablo.Rows(i).Item(1))
                    t = Convert.ToDecimal(tablo.Rows(i).Item(13).Replace(".", ","))


                    DataGridView1.Rows.Add("Tick N° " & tablo.Rows(i).Item(0), Convert.ToDecimal(tablo.Rows(i).Item(2).ToString.Replace(".", ",")).ToString("# ##0.00"), tablo.Rows(i).Item(6) & " %", Convert.ToDecimal(tablo.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("# ##0.00"), Convert.ToDecimal(tablo.Rows(i).Item(13).ToString.Replace(".", ",")).ToString("# ##0.00"), Format(tablo.Rows(i).Item(1), "dd/MM/yyyy"), tablo.Rows(i).Item(3), tablo.Rows(i).Item(10), tablo.Rows(i).Item(12), tablo.Rows(i).Item(0), tablo.Rows(i).Item(9), tablo.Rows(i).Item(17))
                    sum += t
                Next
                Panel6.Visible = True
                IconButton20.Visible = True

                Label16.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", ""))
                IconButton2.Text = tablo.Rows.Count
                DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
                DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"

                DataGridView1.ClearSelection()
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If Convert.ToDouble(row.Cells(3).Value.ToString.Replace(" ", "")) <= 0 Then
                        row.Cells(3).Style.ForeColor = Color.Red
                        row.Cells(4).Style.ForeColor = Color.Red
                    Else
                        row.Cells(3).Style.ForeColor = Color.Green
                        row.Cells(4).Style.ForeColor = Color.Red
                    End If
                Next


                ' Ajouter la colonne au début de la DataGridView

            End If

        End If

    End Sub

    Private Sub IconButton21_Click(sender As Object, e As EventArgs) Handles IconButton21.Click
        If IconButton21.BackColor = Color.Goldenrod Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                row.Selected = False
            Next

            IconButton1.Text = 0.00
            IconButton21.BackColor = Color.Gold
        Else
            For Each row As DataGridViewRow In DataGridView1.Rows
                row.Selected = True
            Next
            Dim sum As Decimal = 0
            For i = 0 To DataGridView1.Rows.Count - 1
                sum += Convert.ToDecimal(DataGridView1.Rows(i).Cells(1).Value.ToString.Replace(" ", ""))
            Next
            IconButton1.Text = sum.ToString("# ##0.00")
            IconButton21.BackColor = Color.Goldenrod
        End If

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Text = "Espèce (Comptoir)" Or ComboBox3.Text = "Espèce (Coffre)" Then
            tpepanel.Visible = False
            chqpanel.Visible = False
        End If

        If ComboBox3.Text = "Chèque" Or ComboBox3.Text = "Virement" Or ComboBox3.Text = "Effet" Then
            tpepanel.Visible = False
            chqpanel.Visible = True
        End If

        If ComboBox3.Text = "TPE" Then
            tpepanel.Visible = True
            chqpanel.Visible = False
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub ComboBox2_DropDownClosed(sender As Object, e As EventArgs) Handles ComboBox2.DropDownClosed
        Dim client As String = "Tout"
        If ComboBox2.Text = "Tout" Or ComboBox2.Text = "" Then
            Panel6.Visible = False
            Label16.Text = 0.00
            DataGridView1.ClearSelection()
            DataGridView1.Rows.Clear()
            IconButton1.Text = 0.00
        Else

            client = ComboBox2.Text
            If CheckBox1.Checked = False Then
                adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and etat = @etat and client = @clt order by OrderID desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
                adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
            Else
                adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and client = @clt order by OrderID desc", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
                adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
            End If

            Dim tablo As New DataTable
            adpt.Fill(tablo)
            Dim sum As Double = 0
            Dim cnt As Integer = 0
            DataGridView1.Rows.Clear()
            For i = 0 To tablo.Rows.Count - 1

                Dim s As String = ""
                Dim t As Decimal = 0
                s = Convert.ToString(tablo.Rows(i).Item(1))
                t = Convert.ToDecimal(tablo.Rows(i).Item(13).Replace(".", ","))


                DataGridView1.Rows.Add("Tick N° " & tablo.Rows(i).Item(0), Convert.ToDecimal(tablo.Rows(i).Item(2).ToString.Replace(".", ",")).ToString("# ##0.00"), tablo.Rows(i).Item(6) & " %", Convert.ToDecimal(tablo.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("# ##0.00"), Convert.ToDecimal(tablo.Rows(i).Item(13).ToString.Replace(".", ",")).ToString("# ##0.00"), Format(tablo.Rows(i).Item(1), "dd/MM/yyyy"), tablo.Rows(i).Item(3), tablo.Rows(i).Item(10), tablo.Rows(i).Item(12), tablo.Rows(i).Item(0), tablo.Rows(i).Item(9), tablo.Rows(i).Item(17))
                sum += t
            Next
            IconButton1.Text = 0.00
            Panel6.Visible = True
            IconButton20.Visible = True

            Label16.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", ""))

            IconButton2.Text = tablo.Rows.Count
            DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"

            DataGridView1.ClearSelection()
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells(3).Value <= 0 Then
                    row.Cells(3).Style.ForeColor = Color.Red
                    row.Cells(4).Style.ForeColor = Color.Red
                Else
                    row.Cells(3).Style.ForeColor = Color.Green
                    row.Cells(4).Style.ForeColor = Color.Red
                End If
            Next


            ' Ajouter la colonne au début de la DataGridView

        End If

        TextBox1.Text = ""
        ComboBox2.Text = client

    End Sub

    Private Sub IconButton27_Click(sender As Object, e As EventArgs) Handles IconButton27.Click
        Panel7.Visible = False
        Panel3.Visible = False
        IconButton33.Visible = False
        IconButton37.Visible = False
        IconButton38.Visible = False
        IconButton3.Visible = False
        IconButton30.PerformClick()
        ComboBox3.Text = "Espèce (Comptoir)"
        ComboBox7.Text = ""
        ComboBox8.Text = ""
        TextBox3.Text = 0.00
        TextBox14.Text = ""
        TextBox15.Text = ""
        TextBox16.Text = ""
        TextBox8.Text = ""
        TextBox20.Text = ""
        TextBox21.Text = ""
        DateTimePicker5.Value = DateTime.Today
        DateTimePicker3.Value = DateTime.Today
        If DataGridView1.Columns.Contains("CheckBoxColumn") Then
            ' Supprimer la colonne CheckBox
            DataGridView1.Columns.Remove("CheckBoxColumn")
        End If
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(3).Value <= 0 Then
                row.Cells(3).Style.ForeColor = Color.Red
                row.Cells(4).Style.ForeColor = Color.Red
            Else
                row.Cells(3).Style.ForeColor = Color.Green
                row.Cells(4).Style.ForeColor = Color.Red
            End If
        Next
        DataGridView1.ClearSelection()
        DataGridView1.Visible = True
    End Sub

    Private Sub IconButton31_Click(sender As Object, e As EventArgs) Handles IconButton31.Click
        Panel3.Visible = True
        TextBox3.Text = IconButton25.Text
        ComboBox3.Text = "Espèce (Comptoir)"
        DataGridView1.Visible = False
    End Sub

    Private Sub IconButton34_Click(sender As Object, e As EventArgs) Handles IconButton34.Click
        Dim debit As Double = 0
        Dim credit As Double = 0
        Dim solde As Double = 0

        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("Vente")
        For i = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(8).Value)
            debit = debit + Convert.ToDouble(DataGridView1.Rows(i).Cells(1).Value.ToString.Replace(" ", ""))
            credit = credit + Convert.ToDouble(DataGridView1.Rows(i).Cells(3).Value.ToString.Replace(" ", ""))
        Next
        solde = debit - credit
        ReportToPrint = New LocalReport()
        formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\etatvente.rdlc"
        formata4.ReportViewer1.LocalReport.DataSources.Clear()
        Dim du As New ReportParameter("du", DateTimePicker1.Text)
        Dim du1(0) As ReportParameter
        du1(0) = du
        formata4.ReportViewer1.LocalReport.SetParameters(du1)

        Dim au As New ReportParameter("au", DateTimePicker2.Text)
        Dim au1(0) As ReportParameter
        au1(0) = au
        formata4.ReportViewer1.LocalReport.SetParameters(au1)

        Dim frs As New ReportParameter("frs", ComboBox2.Text)
        Dim frs1(0) As ReportParameter
        frs1(0) = frs
        formata4.ReportViewer1.LocalReport.SetParameters(frs1)

        Dim etat As New ReportParameter("etat", "Etat Client")
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


        formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
        formata4.ReportViewer1.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
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
        formata4.ReportViewer1.Refresh()
    End Sub

    Private Sub IconButton37_Click(sender As Object, e As EventArgs) Handles IconButton37.Click
        For i = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.SelectedRows.Count <> 0 Then
                If DataGridView1.Rows(i).Cells(9).Value = DataGridView1.SelectedRows(0).Cells(9).Value Then
                    Dim cell As DataGridViewCheckBoxCell = DataGridView1.Rows(i).Cells(0)
                    cell.Value = True
                End If

            End If
        Next
        Dim totalCumule As Decimal = 0
        For Each row As DataGridViewRow In DataGridView1.Rows
            Dim cell As DataGridViewCheckBoxCell = row.Cells(0)
            If Convert.ToBoolean(cell.Value) Then
                Dim valeurColonne As Decimal = Convert.ToDecimal(row.Cells(5).Value.ToString.Replace(" ", "")) ' Remplacez "NomDeLaColonne" par le nom réel de la colonne
                totalCumule += valeurColonne
            End If
        Next
        IconButton25.Text = totalCumule.ToString("# ##0.00")
        IconButton33.PerformClick()
    End Sub

    Private Sub IconButton38_Click(sender As Object, e As EventArgs) Handles IconButton38.Click
        For i = 0 To DataGridView1.Rows.Count - 1
            Dim cell As DataGridViewCheckBoxCell = DataGridView1.Rows(i).Cells(0)
            cell.Value = False
        Next
        IconButton25.Text = 0.00

    End Sub

    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs) Handles Panel7.Paint

    End Sub

    Private Sub IconButton33_Click(sender As Object, e As EventArgs) Handles IconButton33.Click
        facturedetails.Show()
        facturedetails.Label1.Text = "Facture N° :"
        facturedetails.Label3.Text = 0

        For Each row As DataGridViewRow In DataGridView1.Rows
            Dim cell As DataGridViewCheckBoxCell = row.Cells(0)
            If Convert.ToBoolean(cell.Value) Then
                facturedetails.Label4.Text = row.Cells(10).Value.ToString
                facturedetails.Label8.Text = row.Cells(3).Value.ToString
                Exit For
            End If
        Next
        Dim sumreste As Double = 0
        Dim sumtrs As Double = 0
        Dim nonpaye As Boolean = False
        For Each row As DataGridViewRow In DataGridView1.Rows
            Dim cell As DataGridViewCheckBoxCell = row.Cells(0)
            If Convert.ToBoolean(cell.Value) Then
                sumreste = Convert.ToDouble(sumreste + row.Cells(5).Value.ToString.Replace(" ", "")).ToString("#0.00")
                If sumreste <> "0,00" Then
                    nonpaye = True

                End If
                Dim id = row.Cells(10).Value.ToString
                adpt = New MySqlDataAdapter("select transport from orders where OrderID = '" + id + "' ", conn2)
                Dim tabletrs As New DataTable
                adpt.Fill(tabletrs)
                sumtrs += tabletrs.Rows(0).Item(0)
                adpt = New MySqlDataAdapter("select ProductID,name,Quantity,Total,marge,pv,tva,Price from orderdetails where OrderID = '" + id + "' and type = 'ventes' ", conn2)
                Dim table2 As New DataTable
                adpt.Fill(table2)
                For j = 0 To table2.Rows.Count - 1
                    facturedetails.DataGridView3.Rows.Add(table2.Rows(j).Item(0).ToString.Replace(" ", ""), table2.Rows(j).Item(1), table2.Rows(j).Item(2).ToString.Replace(" ", ""), Convert.ToString(table2.Rows(j).Item(3)).Replace(".", ",").Replace(" ", ""), Convert.ToString(table2.Rows(j).Item(4)).Replace(".", ",").Replace(" ", ""), Convert.ToString(table2.Rows(j).Item(5)).Replace(".", ",").Replace(" ", ""), Convert.ToString(table2.Rows(j).Item(6)).Replace(".", ",").Replace(" ", ""), Convert.ToString(table2.Rows(j).Item(7)).Replace(".", ",").Replace(" ", ""))
                Next
                If nonpaye = False Then
                    facturedetails.Label5.Text = row.Cells(8).Value.ToString
                End If
            End If
        Next

        facturedetails.Label6.Text = sumreste.ToString("#0.00").Replace(".", ",")

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
        facturedetails.Label2.Text = num
        facturedetails.Label7.Text = sumtrs
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "" Then
                ComboBox2.Text = "Tout"
                Panel6.Visible = False
                Label16.Text = 0.00
                DataGridView1.ClearSelection()
                DataGridView1.Rows.Clear()
                IconButton1.Text = 0.00
                IconButton4.PerformClick()
            Else

                Dim client As String = "Tout"
                If ComboBox2.Text = "Tout" Or ComboBox2.Text = "" Then
                    Panel6.Visible = False
                    Label16.Text = 0.00
                    DataGridView1.ClearSelection()
                    DataGridView1.Rows.Clear()
                    IconButton1.Text = 0.00
                Else
                    client = ComboBox2.Text
                    If CheckBox1.Checked = False Then
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and etat = @etat and client = @clt order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
                        adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
                        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                    Else
                        adpt = New MySqlDataAdapter("SELECT * FROM orders WHERE parked='no' and modePayement = @mode and client = @clt order by OrderID desc", conn2)
                        adpt.SelectCommand.Parameters.Clear()
                        adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
                        adpt.SelectCommand.Parameters.AddWithValue("@clt", ComboBox2.Text)
                    End If

                    Dim tablo As New DataTable
                    adpt.Fill(tablo)
                    Dim sum As Double = 0
                    Dim cnt As Integer = 0
                    DataGridView1.Rows.Clear()
                    For i = 0 To tablo.Rows.Count - 1

                        Dim s As String = ""
                        Dim t As Decimal = 0
                        s = Convert.ToString(tablo.Rows(i).Item(1))
                        t = Convert.ToDecimal(tablo.Rows(i).Item(13).Replace(".", ","))


                        DataGridView1.Rows.Add("Tick N° " & tablo.Rows(i).Item(0), Convert.ToDecimal(tablo.Rows(i).Item(2).ToString.Replace(".", ",")).ToString("# ##0.00"), tablo.Rows(i).Item(6) & " %", Convert.ToDecimal(tablo.Rows(i).Item(4).ToString.Replace(".", ",")).ToString("# ##0.00"), Convert.ToDecimal(tablo.Rows(i).Item(13).ToString.Replace(".", ",")).ToString("# ##0.00"), Format(tablo.Rows(i).Item(1), "dd/MM/yyyy"), tablo.Rows(i).Item(3), tablo.Rows(i).Item(10), tablo.Rows(i).Item(12), tablo.Rows(i).Item(0), tablo.Rows(i).Item(9), tablo.Rows(i).Item(17))
                        sum += t
                    Next
                    IconButton1.Text = 0.00
                    Panel6.Visible = True
                    IconButton20.Visible = True

                    Label16.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", ""))
                    IconButton2.Text = tablo.Rows.Count
                    DataGridView1.Columns(1).DefaultCellStyle.Format = "N2"
                    DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
                    DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
                    DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"

                    DataGridView1.ClearSelection()
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If row.Cells(3).Value <= 0 Then
                            row.Cells(3).Style.ForeColor = Color.Red
                            row.Cells(4).Style.ForeColor = Color.Red
                        Else
                            row.Cells(3).Style.ForeColor = Color.Green
                            row.Cells(4).Style.ForeColor = Color.Red
                        End If
                    Next



                    ' Ajouter la colonne au début de la DataGridView

                End If
                TextBox1.Text = ""
                ComboBox2.Text = client

            End If
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Dim totalCumule As Decimal = 0
        If DataGridView1.Columns.Count() = 13 Then
            If e.ColumnIndex = 0 AndAlso e.RowIndex >= 0 Then
                'Inverse l'état de la case à cocher
                If DataGridView1.Rows(e.RowIndex).Cells(11).Value() = "fac" Then
                    Dim yesornoMsg As New eror
                    yesornoMsg.Label14.Text = "Cette ventes est déjà facturée !"
                    yesornoMsg.Panel5.Visible = True
                    yesornoMsg.ShowDialog()

                    ' Ne définissez pas la valeur de la case à cocher ici
                    ' DataGridView1.Rows(e.RowIndex).Cells(0).Value = False
                ElseIf DataGridView1.Rows(e.RowIndex).Cells(11).Value() = "receipt" Then
                    Dim cell As DataGridViewCheckBoxCell = DataGridView1.Rows(e.RowIndex).Cells(0)
                    cell.Value = Not Convert.ToBoolean(cell.Value)
                End If

                For Each row As DataGridViewRow In DataGridView1.Rows
                    Dim cell As DataGridViewCheckBoxCell = row.Cells(0)
                    If Convert.ToBoolean(cell.Value) Then
                        If IconButton33.Visible = False Then
                            Dim valeurColonne As Decimal = Convert.ToDecimal(row.Cells(5).Value.ToString.Replace(" ", "")) ' Remplacez "NomDeLaColonne" par le nom réel de la colonne
                            totalCumule += valeurColonne
                        Else
                            Dim valeurColonne As Decimal = Convert.ToDecimal(row.Cells(2).Value.ToString.Replace(" ", "")) ' Remplacez "NomDeLaColonne" par le nom réel de la colonne
                            totalCumule += valeurColonne
                        End If
                    End If
                Next

                IconButton25.Text = totalCumule.ToString("# ##0.00")
            End If


        End If
    End Sub

    Private Sub ComboBox2_Enter(sender As Object, e As EventArgs) Handles ComboBox2.Enter

    End Sub
End Class