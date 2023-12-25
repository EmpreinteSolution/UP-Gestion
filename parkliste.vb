Imports MySql.Data.MySqlClient

Public Class parkliste
    Dim adpt1, adpt2 As MySqlDataAdapter

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Close()
    End Sub

    Private Sub parkliste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        Dim cmd As MySqlCommand

        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
            Dim id = row.Cells(3).Value.ToString
            If e.ColumnIndex = 2 Then
                Dim Rep As Integer
                Rep = MsgBox("Voulez-vous vraiment supprimer cet commande parqué ?", vbYesNo)
                If Rep = vbYes Then

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
                End If
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
End Class