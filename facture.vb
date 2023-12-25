Imports MySql.Data.MySqlClient
Public Class facture
    Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Private Sub facture_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = user
        Dim w = Screen.PrimaryScreen.WorkingArea.Width
        Dim h = My.Computer.Screen.WorkingArea.Size.Height
        Me.Width = w
        Me.Height = h
        Me.Location = New Point(0, 0)
        adpt = New MySqlDataAdapter("select * from achat WHERE Day(achat.dateAchat) = @day and month(achat.dateAchat)=@month and year(achat.dateAchat)=@year", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
        adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
        adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        For i = 0 To table.Rows.Count - 1
            Dim s As String = ""
            Dim t As String = ""
            s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")
            t = Convert.ToString(table.Rows(i).Item(5)).Replace(".", ",")
            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1) & " Dhs", table.Rows(i).Item(5) & " Dhs", table.Rows(i).Item(6) & " Dhs", table.Rows(i).Item(3), table.Rows(i).Item(2), table.Rows(i).Item(4))

            sum = sum + Convert.ToDouble(s)
        Next

        IconButton1.Text = Math.Round(sum, 2) & " Dhs"
        IconButton2.Text = table.Rows.Count


    End Sub
    Private Sub DataGridView1_CellMouseClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick



        If e.RowIndex >= 0 Then
            ventedetail.Show()
            ventedetail.Label1.Text = "Facture N° :"
            ventedetail.Label3.Text = "facture"
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim id = row.Cells(0).Value.ToString

            ventedetail.DataGridView3.Rows.Clear()

            ventedetail.Label2.Text = id
            adpt = New MySqlDataAdapter("select code,name,qty,montant,rms,id from tickets where tick = '" + id + "' and type = 'facture' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            For i = 0 To table.Rows.Count - 1
                ventedetail.DataGridView3.Rows.Add(table.Rows(i).Item(5), table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(4) & "%", Val(table.Rows(i).Item(3)))
            Next
        End If

    End Sub


    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        adpt = New MySqlDataAdapter("SELECT * FROM achat WHERE dateAchat BETWEEN @datedebut AND @datefin order by id desc", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)

        'If ComboBox1.Text = "Espèce" Then
        '    adpt = New MySqlDataAdapter("SELECT * FROM factures WHERE date BETWEEN @datedebut AND @datefin and mode = @mode order by id desc", conn2)
        '    adpt.SelectCommand.Parameters.Clear()
        '    adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
        '    adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)
        '    adpt.SelectCommand.Parameters.AddWithValue("@mode", "Espèce")
        'End If

        'If ComboBox1.Text = "Chèque" Then
        '    adpt = New MySqlDataAdapter("SELECT * FROM factures WHERE date BETWEEN @datedebut AND @datefin and mode = @mode order by id desc", conn2)
        '    adpt.SelectCommand.Parameters.Clear()
        '    adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
        '    adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)
        '    adpt.SelectCommand.Parameters.AddWithValue("@mode", "Chèque")
        'End If

        'If ComboBox1.Text = "TPE" Then
        '    adpt = New MySqlDataAdapter("SELECT * FROM factures WHERE date BETWEEN @datedebut AND @datefin and mode = @mode order by id desc", conn2)
        '    adpt.SelectCommand.Parameters.Clear()
        '    adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
        '    adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)
        '    adpt.SelectCommand.Parameters.AddWithValue("@mode", "TPE")
        'End If

        'If ComboBox1.Text = "Crédit" Then
        '    adpt = New MySqlDataAdapter("SELECT * FROM factures WHERE date BETWEEN @datedebut AND @datefin and mode = @mode order by id desc", conn2)
        '    adpt.SelectCommand.Parameters.Clear()
        '    adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
        '    adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker2.Value.Date)
        '    adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
        'End If

        Dim table As New DataTable
            adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        For i = 0 To table.Rows.Count - 1
            Dim s As String = ""
            Dim t As String = ""
            s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")
            t = Convert.ToString(table.Rows(i).Item(5)).Replace(".", ",")
            DataGridView1.Rows.Add("F-" & table.Rows(i).Item(0), table.Rows(i).Item(2) & " Dhs", Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(5), table.Rows(i).Item(6), table.Rows(i).Item(0))

            sum = sum + Convert.ToDouble(s)
        Next

        IconButton1.Text = Math.Round(sum, 2) & " Dhs"
        IconButton2.Text = table.Rows.Count

    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click


        adpt = New MySqlDataAdapter("select * from achat WHERE Day(achat.dateAchat) = @day and month(achat.dateAchat)=@month and year(achat.dateAchat)=@year", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Today.Day)
        adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Today.Month)
        adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Today.Year)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double = 0
        Dim cnt As Integer = 0
        For i = 0 To table.Rows.Count - 1
            Dim s As String = ""
            Dim t As String = ""
            s = Convert.ToString(table.Rows(i).Item(1)).Replace(".", ",")
            t = Convert.ToString(table.Rows(i).Item(5)).Replace(".", ",")
            DataGridView1.Rows.Add("F-" & table.Rows(i).Item(0), table.Rows(i).Item(2) & " Dhs", Format(table.Rows(i).Item(1), "dd/MM/yyyy"), table.Rows(i).Item(3), table.Rows(i).Item(5), table.Rows(i).Item(6), table.Rows(i).Item(0))

            sum = sum + Convert.ToDouble(s)
        Next

        IconButton1.Text = Math.Round(sum, 2) & " Dhs"
        IconButton2.Text = table.Rows.Count
        DateTimePicker1.Text = Today
            DateTimePicker2.Text = Today


    End Sub

    Private Sub IconButton16_Click(sender As Object, e As EventArgs) Handles IconButton16.Click
        Dim Rep As Integer
        Rep = MsgBox("Voulez-vous vraiment quitter ?", vbYesNo)
        If Rep = vbYes Then
            Dim log = New Form1()
            log.Show()
            Me.Close()
        End If
    End Sub

    Private Sub IconButton15_Click(sender As Object, e As EventArgs) Handles IconButton15.Click
        Home.Show()
        Me.Close()

    End Sub

    Private Sub IconButton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        stock.Show()
        Me.Close()

    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click
        dashboard.Show()
        dashboard.IconButton9.Visible = True
        dashboard.IconButton8.Visible = False
    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        archive.Show()
        Me.Close()

    End Sub

    Private Sub IconButton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
        Four.Show()
        Me.Close()

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)

    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Client.Show()
        Me.Close()
    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        devis.Show()
        Me.Close()
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Me.Show()
        Me.Close()
    End Sub

    Private Sub IconButton8_Click_1(sender As Object, e As EventArgs) Handles IconButton8.Click
        product.Show()
        Me.Close()
    End Sub

    Private Sub IconButton8_Click(sender As Object, e As EventArgs)
        product.Show()
        Me.Close()

    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        users.Show()
        Me.Close()

    End Sub
End Class