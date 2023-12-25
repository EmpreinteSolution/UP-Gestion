Imports System.ComponentModel
Imports System.IO
Imports MySql.Data.MySqlClient

Public Class reset
    Dim adpt1, adpt2 As MySqlDataAdapter

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Panel5.Visible = True
    End Sub

    Private Sub reset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt1 = New MySqlDataAdapter("select * from orders WHERE parked = @day and ServeurRef = @caissier order by OrderID desc", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@day", "yes")
        adpt1.SelectCommand.Parameters.AddWithValue("@caissier", dashboard.Label2.Text)

        Dim table1 As New DataTable
        DataGridView2.Rows.Clear()
        adpt1.Fill(table1)
        For i = 0 To table1.Rows.Count() - 1
            DataGridView2.Rows.Add("N° " & table1.Rows(i).Item(0), table1.Rows(i).Item(2) & " DHs", Nothing, table1.Rows(i).Item(0), "Supprimer")
        Next
    End Sub
    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click
        Panel5.Visible = False
        Label11.Text = "Ventes (BL) :"
        DataGridView2.Rows.Clear()


    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        Dim cmd As MySqlCommand

        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
            Dim id = row.Cells(3).Value.ToString
            If e.ColumnIndex = 2 Then

                conn2.Close()
                conn2.Open()
                cmd = New MySqlCommand("DELETE FROM `orderdetails` WHERE type = 'ventes' and `OrderID` = " + id.ToString, conn2)
                cmd.ExecuteNonQuery()
                cmd = New MySqlCommand("DELETE FROM `orders` WHERE `OrderID` = " + id.ToString, conn2)
                cmd.ExecuteNonQuery()
                conn2.Close()

                adpt1 = New MySqlDataAdapter("select * from orders WHERE parked = @day and ServeurRef = @caissier", conn2)
                adpt1.SelectCommand.Parameters.Clear()
                adpt1.SelectCommand.Parameters.AddWithValue("@day", "yes")
                adpt1.SelectCommand.Parameters.AddWithValue("@caissier", dashboard.Label2.Text)

                Dim table1 As New DataTable
                DataGridView2.Rows.Clear()
                adpt1.Fill(table1)
                For i = 0 To table1.Rows.Count() - 1
                    DataGridView2.Rows.Add("N° " & table1.Rows(i).Item(0), table1.Rows(i).Item(2) & " DHs", Nothing, table1.Rows(i).Item(0))
                Next

                adpt1 = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
                Dim tabletick As New DataTable
                adpt1.Fill(tabletick)
                Dim idlast As Integer
                If tabletick.Rows().Count = 0 Then
                    idlast = 1

                Else
                    idlast = tabletick.Rows(0).Item(0) + 1

                End If
                dashboard.Label20.Text = "Ticket N° " & idlast.ToString
            Else


                Dim adt As New MySqlDataAdapter("SELECT  `name`,  `Quantity`, `Price`, `ProductID`, `pa` , `Total`,`rms`,`gr`,`pv`, `tva`  FROM `orderdetails` WHERE `OrderID` = " + id.ToString, conn2)
                Dim tb As New DataTable
                adt.Fill(tb)
                dashboard.DataGridView3.Rows.Clear()
                For i = 0 To tb.Rows.Count - 1

                    dashboard.DataGridView3.Rows.Add(tb.Rows(i).Item(3), tb.Rows(i).Item(0), tb.Rows(i).Item(1).ToString.Replace(".", ",").Replace(" ", ""), tb.Rows(i).Item(2).ToString.Replace(".", ",").Replace(" ", ""), tb.Rows(i).Item(3), tb.Rows(i).Item(4).ToString.Replace(".", ",").Replace(" ", ""), tb.Rows(i).Item(5).ToString.Replace(".", ",").Replace(" ", ""), Nothing, tb.Rows(i).Item(6).ToString.Replace(".", ",").Replace(" ", ""), tb.Rows(i).Item(7).ToString.Replace(".", ",").Replace(" ", ""), tb.Rows(i).Item(8).ToString.Replace(".", ",").Replace(" ", ""), tb.Rows(i).Item(9).ToString.Replace(".", ",").Replace(" ", ""))
                Next

                adt = New MySqlDataAdapter("SELECT `modePayement` ,`client`,`paye`,`reste` FROM `orders` WHERE `OrderID` = " + id.ToString, conn2)
                tb = New DataTable
                adt.Fill(tb)
                dashboard.ComboBox1.Text = tb.Rows(0).Item(0)
                dashboard.ComboBox2.Text = tb.Rows(0).Item(1)
                dashboard.TextBox8.Text = tb.Rows(0).Item(1)
                If dashboard.ComboBox2.Text = "comptoir" Then
                    dashboard.ComboBox2.Visible = False
                    dashboard.TextBox8.Text = ""
                Else
                    dashboard.ComboBox2.Visible = True
                End If
                dashboard.TextBox3.Text = 0
                dashboard.Label4.Text = 0
                dashboard.Label19.Text = id
                dashboard.Label20.Text = "Ticket N° " & id.ToString
                dashboard.Label18.Visible = True

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

                dashboard.Label6.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                dashboard.Label7.Text = Convert.ToDouble(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                dashboard.IconButton17.Text = dashboard.DataGridView3.Rows.Count
                conn2.Close()
                conn2.Open()
                cmd = New MySqlCommand("DELETE FROM `orderdetails` WHERE type = 'ventes' and `OrderID` = " + id.ToString, conn2)
                cmd.ExecuteNonQuery()
                cmd = New MySqlCommand("DELETE FROM `orders` WHERE `OrderID` = " + id.ToString, conn2)
                cmd.ExecuteNonQuery()
                conn2.Close()
                Me.Close()

            End If
        End If
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        DataGridView2.Rows.Clear()
        If Label11.Text = "Ventes (BL) :" Then
            adpt1 = New MySqlDataAdapter("SELECT OrderID,client,MontantOrder,OrderDate,ServeurRef,modePayement from orderssauv where OrderDate BETWEEN @datedebut And @datefin ORDER BY OrderID desc", conn2)
        End If

        If Label11.Text = "Réceptions :" Then
            adpt1 = New MySqlDataAdapter("SELECT id,Fournisseur,montant,dateAchat,caissier,avoir FROM achatssauv WHERE dateAchat BETWEEN @datedebut AND @datefin order by id desc", conn2)
        End If

        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker1.Value.Date)
        adpt1.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker5.Value.Date.AddDays(1))

        Dim tabletick As New DataTable
        adpt1.Fill(tabletick)
        For i = 0 To tabletick.Rows.Count() - 1
            DataGridView2.Rows.Add("PC N° " & Convert.ToString(tabletick.Rows(i).Item(0)), tabletick.Rows(i).Item(1), Convert.ToDouble(tabletick.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDateTime(tabletick.Rows(i).Item(3)).ToString("dd/MM/yyyy HH:mm:ss"), tabletick.Rows(i).Item(0), tabletick.Rows(i).Item(4), tabletick.Rows(i).Item(5))
        Next
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub DateTimePicker5_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker5.ValueChanged

    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Panel5.Visible = False
        Label11.Text = "Réceptions :"
        DataGridView2.Rows.Clear()

    End Sub
    Dim adpt As MySqlDataAdapter
    Dim sql2 As String
    Dim cmd2 As MySqlCommand
    Private Sub IconButton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
        conn2.Close()
        conn2.Open()
        For i = 0 To DataGridView2.SelectedRows.Count() - 1
            adpt = New MySqlDataAdapter("select * from orderssauv where OrderID = '" & DataGridView2.SelectedRows(i).Cells(4).Value & "' and MontantOrder = '" & Convert.ToDouble(DataGridView2.SelectedRows(i).Cells(2).Value.ToString.Replace(" ", "")) & "'", conn2)
            Dim table4 As New DataTable
            adpt.Fill(table4)
            adpt = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
            Dim tabletick As New DataTable
            adpt.Fill(tabletick)
            Dim id As Integer
            If tabletick.Rows().Count = 0 Then
                id = 1

            Else
                id = tabletick.Rows(0).Item(0) + 1

            End If
            sql2 = "INSERT INTO `orders`(`OrderID`, `OrderDate`, `MontantOrder`, `ServeurRef`, `paye`, `rendu`, `remisePerc`, `remiseMt`, `montantTotal`, `etat`, `modePayement`, `orderType`, `client`, `reste`, `transport`, `livreur`, `parked`, `fac_id`, `cloture`, `deleted`) 
VALUES ('" & id & "','" & Convert.ToDateTime(table4.Rows(0).Item(1)).ToString("yyyy-MM-dd HH:mm:ss") & "','" & table4.Rows(0).Item(2) & "','" & table4.Rows(0).Item(3) & "','" & table4.Rows(0).Item(4) & "','" & table4.Rows(0).Item(5) & "','" & table4.Rows(0).Item(6) & "','" & table4.Rows(0).Item(7) & "','" & table4.Rows(0).Item(8) & "','" & table4.Rows(0).Item(9) & "','" & table4.Rows(0).Item(10) & "','" & table4.Rows(0).Item(11) & "','" & table4.Rows(0).Item(12) & "','" & table4.Rows(0).Item(13) & "','" & table4.Rows(0).Item(14) & "','" & table4.Rows(0).Item(15) & "','" & table4.Rows(0).Item(16) & "','" & table4.Rows(0).Item(18) & "','" & table4.Rows(0).Item(19) & "','" & table4.Rows(0).Item(20) & "')"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()

            adpt = New MySqlDataAdapter("select * from orderdetailssauv where OrderID = '" & DataGridView2.SelectedRows(i).Cells(4).Value & "'", conn2)
            Dim table2 As New DataTable
            adpt.Fill(table2)
            For j = 0 To table2.Rows.Count() - 1
                sql2 = "INSERT INTO `orderdetails`(`OrderID`, `ProductID`, `Price`, `Quantity`, `Total`, `date`, `pa`, `type`, `name`, `marge`, `rms`, `gr`, `pv`, `tva`) 
VALUES ('" & id & "','" & table2.Rows(j).Item(1) & "','" & table2.Rows(j).Item(2) & "','" & table2.Rows(j).Item(3) & "','" & table2.Rows(j).Item(4) & "','" & Convert.ToDateTime(table2.Rows(j).Item(5)).ToString("yyyy-MM-dd HH:mm:ss") & "','" & table2.Rows(j).Item(6) & "','" & table2.Rows(j).Item(7) & "','" & table2.Rows(j).Item(9) & "','" & table2.Rows(j).Item(10) & "','" & table2.Rows(j).Item(11) & "','" & table2.Rows(j).Item(12) & "','" & table2.Rows(j).Item(13) & "','" & table2.Rows(j).Item(14) & "')"
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.ExecuteNonQuery()
            Next
        Next
        conn2.Close()
        MsgBox("Récupération terminée avec succès !")

    End Sub

    <Obsolete>
    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Dim server = System.Configuration.ConfigurationSettings.AppSettings("server")
        Dim database = System.Configuration.ConfigurationSettings.AppSettings("database")
        Dim user = System.Configuration.ConfigurationSettings.AppSettings("userid")
        Dim password = System.Configuration.ConfigurationSettings.AppSettings("password")

        Dim appPath As String = Application.StartupPath()
        Dim backupPath As DirectoryInfo = New DirectoryInfo(appPath & "\backups")

        ' Ask the user to select the backup file
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*"
        openFileDialog.Title = "Select a MySQL backup file"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Get the selected file path
            Dim filePath As String = openFileDialog.FileName

            ' Build the mysql command for importing the database
            Dim command As String

            If password <> "" Then
                command = $"C:\xampp\mysql\bin\mysql -h {server} -u {user} -p{password} {database} < ""{filePath}"""
            Else
                command = $"C:\xampp\mysql\bin\mysql -h {server} -u {user} {database} < ""{filePath}"""
            End If

            ' Start the process
            StartProcess(command)
        End If
    End Sub
    Private startTime As DateTime
    Private process As Process


    Private Sub StartProcess(command As String)
        Try
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
            MsgBox("Importation terminée avec succès !")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork

    End Sub
End Class