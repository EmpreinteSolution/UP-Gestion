Imports MySql.Data.MySqlClient

Public Class calcul
    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Close()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        ds.DataGridView3.SelectedCells(2).Value = Convert.ToDouble(TextBox1.Text).ToString("# ##0.00").Replace(".", ",")

        Dim prixHT As Decimal = ds.DataGridView3.SelectedCells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
        Dim quantite As Decimal = ds.DataGridView3.SelectedCells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.SelectedCells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
        Dim tauxTVA As Decimal = ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
        Dim pourcentageRemise As Decimal = ds.DataGridView3.SelectedCells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

        ' Étape 1 : Calculer le montant HT après la remise
        Dim montantHTApresRemise As Decimal = prixHT * (1 - pourcentageRemise / 100)

        ' Étape 2 : Calculer le montant TTC avant TVA
        Dim montantTTCAvantTVA As Decimal = montantHTApresRemise * quantite

        ' Étape 3 : Calculer le montant de la TVA
        Dim montantTVA As Decimal = montantTTCAvantTVA * tauxTVA

        ' Étape 4 : Calculer le prix TTC
        Dim prixTTC As Decimal = montantTTCAvantTVA + montantTVA


        Dim result As Decimal
        result = Math.Round(prixTTC, 2) ' Arrondir à deux décimales
        ds.DataGridView3.SelectedCells(6).Value = result


        Dim sum As Double = 0
        Dim sum2 As Double = 0
        Dim sum3 As Double = 0


        For j = 0 To ds.DataGridView3.Rows.Count - 1
            sum = sum + ds.DataGridView3.Rows(j).Cells(6).Value
        Next
        For i = 0 To ds.DataGridView3.Rows.Count - 1
            prixHT = ds.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
            quantite = ds.DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
            tauxTVA = ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
            pourcentageRemise = ds.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

            ' Étape 1 : Calculer le montant HT après la remise
            montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

            ' Étape 2 : Calculer le montant TTC avant TVA
            montantTTCAvantTVA = montantHTApresRemise * quantite

            ' Étape 3 : Calculer le montant de la TVA
            montantTVA = montantTTCAvantTVA * tauxTVA

            sum2 += montantTTCAvantTVA
            sum3 += montantTVA
        Next
        ds.Label23.Text = sum2
        ds.Label24.Text = sum3
        ds.Label25.Text = ds.Label23.Text


        ds.Label6.Text = Convert.ToDouble(sum).ToString("N2")
        ds.Label7.Text = Convert.ToDouble(sum).ToString("N2")
        Me.Close()
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        ds.DataGridView3.SelectedCells(9).Value = TextBox1.Text.ToString.Replace(".", ",")

        Dim prixHT As Decimal = ds.DataGridView3.SelectedCells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
        Dim quantite As Decimal = ds.DataGridView3.SelectedCells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.SelectedCells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
        Dim tauxTVA As Decimal = ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
        Dim pourcentageRemise As Decimal = ds.DataGridView3.SelectedCells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

        ' Étape 1 : Calculer le montant HT après la remise
        Dim montantHTApresRemise As Decimal = prixHT * (1 - pourcentageRemise / 100)

        ' Étape 2 : Calculer le montant TTC avant TVA
        Dim montantTTCAvantTVA As Decimal = montantHTApresRemise * quantite

        ' Étape 3 : Calculer le montant de la TVA
        Dim montantTVA As Decimal = montantTTCAvantTVA * tauxTVA

        ' Étape 4 : Calculer le prix TTC
        Dim prixTTC As Decimal = montantTTCAvantTVA + montantTVA


        Dim result As Decimal
        result = Math.Round(prixTTC, 2) ' Arrondir à deux décimales
        ds.DataGridView3.SelectedCells(6).Value = result


        Dim sum As Double = 0
        Dim sum2 As Double = 0
        Dim sum3 As Double = 0


        For j = 0 To ds.DataGridView3.Rows.Count - 1
            sum = sum + ds.DataGridView3.Rows(j).Cells(6).Value
        Next
        For i = 0 To ds.DataGridView3.Rows.Count - 1
            prixHT = ds.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
            quantite = ds.DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
            tauxTVA = ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
            pourcentageRemise = ds.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

            ' Étape 1 : Calculer le montant HT après la remise
            montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

            ' Étape 2 : Calculer le montant TTC avant TVA
            montantTTCAvantTVA = montantHTApresRemise * quantite

            ' Étape 3 : Calculer le montant de la TVA
            montantTVA = montantTTCAvantTVA * tauxTVA

            sum2 += montantTTCAvantTVA
            sum3 += montantTVA
        Next
        ds.Label23.Text = sum2
        ds.Label24.Text = sum3
        ds.Label25.Text = ds.Label23.Text


        ds.Label6.Text = Convert.ToDouble(sum).ToString("N2")
        ds.Label7.Text = Convert.ToDouble(sum).ToString("N2")
        Me.Close()
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        ds.DataGridView3.SelectedCells(8).Value = TextBox1.Text.ToString.Replace(".", ",")
        ds.DataGridView3.SelectedCells(12).Value = "change"

        Dim prixHT As Decimal = ds.DataGridView3.SelectedCells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
        Dim quantite As Decimal = ds.DataGridView3.SelectedCells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.SelectedCells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
        Dim tauxTVA As Decimal = ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
        Dim pourcentageRemise As Decimal = ds.DataGridView3.SelectedCells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

        ' Étape 1 : Calculer le montant HT après la remise
        Dim montantHTApresRemise As Decimal = prixHT * (1 - pourcentageRemise / 100)

        ' Étape 2 : Calculer le montant TTC avant TVA
        Dim montantTTCAvantTVA As Decimal = montantHTApresRemise * quantite

        ' Étape 3 : Calculer le montant de la TVA
        Dim montantTVA As Decimal = montantTTCAvantTVA * tauxTVA

        ' Étape 4 : Calculer le prix TTC
        Dim prixTTC As Decimal = montantTTCAvantTVA + montantTVA


        Dim result As Decimal
        result = Math.Round(prixTTC, 2) ' Arrondir à deux décimales
        ds.DataGridView3.SelectedCells(6).Value = result


        Dim sum As Double = 0
        Dim sum2 As Double = 0
        Dim sum3 As Double = 0


        For j = 0 To ds.DataGridView3.Rows.Count - 1
            sum = sum + ds.DataGridView3.Rows(j).Cells(6).Value
        Next
        For i = 0 To ds.DataGridView3.Rows.Count - 1
            prixHT = ds.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
            quantite = ds.DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
            tauxTVA = ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
            pourcentageRemise = ds.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

            ' Étape 1 : Calculer le montant HT après la remise
            montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

            ' Étape 2 : Calculer le montant TTC avant TVA
            montantTTCAvantTVA = montantHTApresRemise * quantite

            ' Étape 3 : Calculer le montant de la TVA
            montantTVA = montantTTCAvantTVA * tauxTVA

            sum2 += montantTTCAvantTVA
            sum3 += montantTVA
        Next
        ds.Label23.Text = sum2
        ds.Label24.Text = sum3
        ds.Label25.Text = ds.Label23.Text


        ds.Label6.Text = Convert.ToDouble(sum).ToString("N2")
        ds.Label7.Text = Convert.ToDouble(sum).ToString("N2")
        Me.Close()

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        txt = sender


    End Sub

    Private Sub IconButton20_Click(sender As Object, e As EventArgs) Handles IconButton20.Click
        Dim cl As New clavier
        cl.Show()
    End Sub
    Dim adpt As MySqlDataAdapter
    Private Sub calcul_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds.TextBox2.Text = ""

        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Panel1.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel3.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel4.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))

        Me.KeyPreview = True

        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Caisse - R% article'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Non Autorise" Then
                IconButton1.Visible = False
            End If
        End If

        tabledroit.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Caisse - Changer le Prix'", conn2)
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Non Autorise" Then
                IconButton5.Visible = False
            End If
        End If

        tabledroit.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Caisse - Gratuite'", conn2)
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Non Autorise" Then
                IconButton3.Visible = False
            End If
        End If


        TextBox1.Focus()

    End Sub

    Private Sub calcul_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Q Then
            If TextBox1.Text <> "" Then

                ds.DataGridView3.SelectedCells(2).Value = Convert.ToDouble(TextBox1.Text).ToString("# ##0.00").Replace(".", ",")

                Dim prixHT As Decimal = ds.DataGridView3.SelectedCells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                Dim quantite As Decimal = ds.DataGridView3.SelectedCells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.SelectedCells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                Dim tauxTVA As Decimal = ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                Dim pourcentageRemise As Decimal = ds.DataGridView3.SelectedCells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                ' Étape 1 : Calculer le montant HT après la remise
                Dim montantHTApresRemise As Decimal = prixHT * (1 - pourcentageRemise / 100)

                ' Étape 2 : Calculer le montant TTC avant TVA
                Dim montantTTCAvantTVA As Decimal = montantHTApresRemise * quantite

                ' Étape 3 : Calculer le montant de la TVA
                Dim montantTVA As Decimal = montantTTCAvantTVA * tauxTVA

                ' Étape 4 : Calculer le prix TTC
                Dim prixTTC As Decimal = montantTTCAvantTVA + montantTVA


                Dim result As Decimal
                result = Math.Round(prixTTC, 2) ' Arrondir à deux décimales
                ds.DataGridView3.SelectedCells(6).Value = result


                Dim sum As Double = 0
                Dim sum2 As Double = 0
                Dim sum3 As Double = 0


                For j = 0 To ds.DataGridView3.Rows.Count - 1
                    sum = sum + ds.DataGridView3.Rows(j).Cells(6).Value
                Next
                For i = 0 To ds.DataGridView3.Rows.Count - 1
                    prixHT = ds.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                    quantite = ds.DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                    tauxTVA = ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                    pourcentageRemise = ds.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                    ' Étape 1 : Calculer le montant HT après la remise
                    montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

                    ' Étape 2 : Calculer le montant TTC avant TVA
                    montantTTCAvantTVA = montantHTApresRemise * quantite

                    ' Étape 3 : Calculer le montant de la TVA
                    montantTVA = montantTTCAvantTVA * tauxTVA

                    sum2 += montantTTCAvantTVA
                    sum3 += montantTVA
                Next
                ds.Label23.Text = sum2
                ds.Label24.Text = sum3
                ds.Label25.Text = ds.Label23.Text


                ds.Label6.Text = Convert.ToDouble(sum).ToString("N2")
                ds.Label7.Text = Convert.ToDouble(sum).ToString("N2")
                Me.Close()

            Else
                MsgBox("insérer une valeur valide")
            End If
        End If

            If e.KeyCode = Keys.Enter Then
            If TextBox1.Text <> "" Then
                ds.DataGridView3.SelectedCells(2).Value = Convert.ToDouble(TextBox1.Text.Replace(".", ",")).ToString("# ##0.00")
                ds.DataGridView3.SelectedCells(12).Value = "change"


                Dim prixHT As Decimal = ds.DataGridView3.SelectedCells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                Dim quantite As Decimal = ds.DataGridView3.SelectedCells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.SelectedCells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                Dim tauxTVA As Decimal = ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                Dim pourcentageRemise As Decimal = ds.DataGridView3.SelectedCells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                ' Étape 1 : Calculer le montant HT après la remise
                Dim montantHTApresRemise As Decimal = prixHT * (1 - pourcentageRemise / 100)

                ' Étape 2 : Calculer le montant TTC avant TVA
                Dim montantTTCAvantTVA As Decimal = montantHTApresRemise * quantite

                ' Étape 3 : Calculer le montant de la TVA
                Dim montantTVA As Decimal = montantTTCAvantTVA * tauxTVA

                ' Étape 4 : Calculer le prix TTC
                Dim prixTTC As Decimal = montantTTCAvantTVA + montantTVA


                Dim result As Decimal
                result = Math.Round(prixTTC, 2) ' Arrondir à deux décimales
                ds.DataGridView3.SelectedCells(6).Value = result


                Dim sum As Double = 0
                Dim sum2 As Double = 0
                Dim sum3 As Double = 0


                For j = 0 To ds.DataGridView3.Rows.Count - 1
                    sum = sum + ds.DataGridView3.Rows(j).Cells(6).Value
                Next
                For i = 0 To ds.DataGridView3.Rows.Count - 1
                    prixHT = ds.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                    quantite = ds.DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                    tauxTVA = ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                    pourcentageRemise = ds.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                    ' Étape 1 : Calculer le montant HT après la remise
                    montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

                    ' Étape 2 : Calculer le montant TTC avant TVA
                    montantTTCAvantTVA = montantHTApresRemise * quantite

                    ' Étape 3 : Calculer le montant de la TVA
                    montantTVA = montantTTCAvantTVA * tauxTVA

                    sum2 += montantTTCAvantTVA
                    sum3 += montantTVA
                Next
                ds.Label23.Text = sum2
                ds.Label24.Text = sum3
                ds.Label25.Text = ds.Label23.Text


                ds.Label6.Text = Convert.ToDouble(sum).ToString("N2")
                ds.Label7.Text = Convert.ToDouble(sum).ToString("N2")
                Me.Close()

            Else
                MsgBox("insérer une valeur valide")
            End If
        End If

            If e.KeyCode = Keys.R Then
            If TextBox1.Text <> "" Then
                ds.DataGridView3.SelectedCells(8).Value = TextBox1.Text.ToString.Replace(".", ",")
                ds.DataGridView3.SelectedCells(12).Value = "change"

                Dim prixHT As Decimal = ds.DataGridView3.SelectedCells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                Dim quantite As Decimal = ds.DataGridView3.SelectedCells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.SelectedCells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                Dim tauxTVA As Decimal = ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                Dim pourcentageRemise As Decimal = ds.DataGridView3.SelectedCells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                ' Étape 1 : Calculer le montant HT après la remise
                Dim montantHTApresRemise As Decimal = prixHT * (1 - pourcentageRemise / 100)

                ' Étape 2 : Calculer le montant TTC avant TVA
                Dim montantTTCAvantTVA As Decimal = montantHTApresRemise * quantite

                ' Étape 3 : Calculer le montant de la TVA
                Dim montantTVA As Decimal = montantTTCAvantTVA * tauxTVA

                ' Étape 4 : Calculer le prix TTC
                Dim prixTTC As Decimal = montantTTCAvantTVA + montantTVA


                Dim result As Decimal
                result = Math.Round(prixTTC, 2) ' Arrondir à deux décimales
                ds.DataGridView3.SelectedCells(6).Value = result


                Dim sum As Double = 0
                Dim sum2 As Double = 0
                Dim sum3 As Double = 0


                For j = 0 To ds.DataGridView3.Rows.Count - 1
                    sum = sum + ds.DataGridView3.Rows(j).Cells(6).Value
                Next
                For i = 0 To ds.DataGridView3.Rows.Count - 1
                    prixHT = ds.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                    quantite = ds.DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                    tauxTVA = ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                    pourcentageRemise = ds.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                    ' Étape 1 : Calculer le montant HT après la remise
                    montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

                    ' Étape 2 : Calculer le montant TTC avant TVA
                    montantTTCAvantTVA = montantHTApresRemise * quantite

                    ' Étape 3 : Calculer le montant de la TVA
                    montantTVA = montantTTCAvantTVA * tauxTVA

                    sum2 += montantTTCAvantTVA
                    sum3 += montantTVA
                Next
                ds.Label23.Text = sum2
                ds.Label24.Text = sum3
                ds.Label25.Text = ds.Label23.Text


                ds.Label6.Text = Convert.ToDouble(sum).ToString("N2")
                ds.Label7.Text = Convert.ToDouble(sum).ToString("N2")
                Me.Close()

            Else
                MsgBox("insérer une valeur valide")
            End If
        End If

            If e.KeyCode = Keys.G Then
            If TextBox1.Text <> "" Then
                ds.DataGridView3.SelectedCells(9).Value = TextBox1.Text.ToString.Replace(".", ",")

                Dim prixHT As Decimal = ds.DataGridView3.SelectedCells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                Dim quantite As Decimal = ds.DataGridView3.SelectedCells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.SelectedCells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                Dim tauxTVA As Decimal = ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                Dim pourcentageRemise As Decimal = ds.DataGridView3.SelectedCells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                ' Étape 1 : Calculer le montant HT après la remise
                Dim montantHTApresRemise As Decimal = prixHT * (1 - pourcentageRemise / 100)

                ' Étape 2 : Calculer le montant TTC avant TVA
                Dim montantTTCAvantTVA As Decimal = montantHTApresRemise * quantite

                ' Étape 3 : Calculer le montant de la TVA
                Dim montantTVA As Decimal = montantTTCAvantTVA * tauxTVA

                ' Étape 4 : Calculer le prix TTC
                Dim prixTTC As Decimal = montantTTCAvantTVA + montantTVA


                Dim result As Decimal
                result = Math.Round(prixTTC, 2) ' Arrondir à deux décimales
                ds.DataGridView3.SelectedCells(6).Value = result


                Dim sum As Double = 0
                Dim sum2 As Double = 0
                Dim sum3 As Double = 0


                For j = 0 To ds.DataGridView3.Rows.Count - 1
                    sum = sum + ds.DataGridView3.Rows(j).Cells(6).Value
                Next
                For i = 0 To ds.DataGridView3.Rows.Count - 1
                    prixHT = ds.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                    quantite = ds.DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                    tauxTVA = ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                    pourcentageRemise = ds.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                    ' Étape 1 : Calculer le montant HT après la remise
                    montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

                    ' Étape 2 : Calculer le montant TTC avant TVA
                    montantTTCAvantTVA = montantHTApresRemise * quantite

                    ' Étape 3 : Calculer le montant de la TVA
                    montantTVA = montantTTCAvantTVA * tauxTVA

                    sum2 += montantTTCAvantTVA
                    sum3 += montantTVA
                Next
                ds.Label23.Text = sum2
                ds.Label24.Text = sum3
                ds.Label25.Text = ds.Label23.Text


                ds.Label6.Text = Convert.ToDouble(sum).ToString("N2")
                ds.Label7.Text = Convert.ToDouble(sum).ToString("N2")
                Me.Close()

            Else
                MsgBox("insérer une valeur valide")
            End If
        End If

            If e.KeyCode = Keys.Subtract Then
            If TextBox1.Text <> "" Then
                ds.DataGridView3.SelectedCells(2).Value = Convert.ToDecimal(-1 * Convert.ToDouble(TextBox1.Text)).ToString.Replace(" ", "")

                Dim prixHT As Decimal = ds.DataGridView3.SelectedCells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                Dim quantite As Decimal = ds.DataGridView3.SelectedCells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.SelectedCells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                Dim tauxTVA As Decimal = ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                Dim pourcentageRemise As Decimal = ds.DataGridView3.SelectedCells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                ' Étape 1 : Calculer le montant HT après la remise
                Dim montantHTApresRemise As Decimal = prixHT * (1 - pourcentageRemise / 100)

                ' Étape 2 : Calculer le montant TTC avant TVA
                Dim montantTTCAvantTVA As Decimal = montantHTApresRemise * quantite

                ' Étape 3 : Calculer le montant de la TVA
                Dim montantTVA As Decimal = montantTTCAvantTVA * tauxTVA

                ' Étape 4 : Calculer le prix TTC
                Dim prixTTC As Decimal = montantTTCAvantTVA + montantTVA


                Dim result As Decimal
                result = Math.Round(prixTTC, 2) ' Arrondir à deux décimales
                ds.DataGridView3.SelectedCells(6).Value = result


                Dim sum As Double = 0
                Dim sum2 As Double = 0
                Dim sum3 As Double = 0


                For j = 0 To ds.DataGridView3.Rows.Count - 1
                    sum = sum + ds.DataGridView3.Rows(j).Cells(6).Value
                Next
                For i = 0 To ds.DataGridView3.Rows.Count - 1
                    prixHT = ds.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                    quantite = ds.DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                    tauxTVA = ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                    pourcentageRemise = ds.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                    ' Étape 1 : Calculer le montant HT après la remise
                    montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

                    ' Étape 2 : Calculer le montant TTC avant TVA
                    montantTTCAvantTVA = montantHTApresRemise * quantite

                    ' Étape 3 : Calculer le montant de la TVA
                    montantTVA = montantTTCAvantTVA * tauxTVA

                    sum2 += montantTTCAvantTVA
                    sum3 += montantTVA
                Next
                ds.Label23.Text = sum2
                ds.Label24.Text = sum3
                ds.Label25.Text = ds.Label23.Text


                ds.Label6.Text = Convert.ToDouble(sum).ToString("N2")
                ds.Label7.Text = Convert.ToDouble(sum).ToString("N2")
                Me.Close()

            Else
                MsgBox("insérer une valeur valide")
            End If
        End If

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = TextBox1.Text.ToString.Replace(".", ",") + Button2.Text
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = TextBox1.Text.ToString.Replace(".", ",") + Button3.Text
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.Text = TextBox1.Text.ToString.Replace(".", ",") + Button4.Text
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Text = TextBox1.Text.ToString.Replace(".", ",") + Button5.Text
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox1.Text = TextBox1.Text.ToString.Replace(".", ",") + Button6.Text
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        TextBox1.Text = TextBox1.Text.ToString.Replace(".", ",") + Button11.Text
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TextBox1.Text = TextBox1.Text.ToString.Replace(".", ",") + Button10.Text
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TextBox1.Text = TextBox1.Text.ToString.Replace(".", ",") + Button9.Text
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        TextBox1.Text = TextBox1.Text.ToString.Replace(".", ",") + Button8.Text
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TextBox1.Text = TextBox1.Text.ToString.Replace(".", ",") + Button7.Text
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If TextBox1.Text = "" Then
        Else
            TextBox1.Text = TextBox1.Text.ToString.Replace(".", ",").Remove(TextBox1.Text.ToString.Replace(".", ",").Length - 1)
        End If

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = TextBox1.Text.ToString.Replace(".", ",") + Button1.Text
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        TextBox1.Text = TextBox1.Text.ToString.Replace(".", ",") + Button13.Text
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        ds.DataGridView3.SelectedCells(2).Value = Convert.ToDecimal(-1 * Convert.ToDouble(TextBox1.Text)).ToString.Replace(" ", "")

        Dim prixHT As Decimal = ds.DataGridView3.SelectedCells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
        Dim quantite As Decimal = ds.DataGridView3.SelectedCells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.SelectedCells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
        Dim tauxTVA As Decimal = ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
        Dim pourcentageRemise As Decimal = ds.DataGridView3.SelectedCells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

        ' Étape 1 : Calculer le montant HT après la remise
        Dim montantHTApresRemise As Decimal = prixHT * (1 - pourcentageRemise / 100)

        ' Étape 2 : Calculer le montant TTC avant TVA
        Dim montantTTCAvantTVA As Decimal = montantHTApresRemise * quantite

        ' Étape 3 : Calculer le montant de la TVA
        Dim montantTVA As Decimal = montantTTCAvantTVA * tauxTVA

        ' Étape 4 : Calculer le prix TTC
        Dim prixTTC As Decimal = montantTTCAvantTVA + montantTVA


        Dim result As Decimal
        result = Math.Round(prixTTC, 2) ' Arrondir à deux décimales
        ds.DataGridView3.SelectedCells(6).Value = result


        Dim sum As Double = 0
        Dim sum2 As Double = 0
        Dim sum3 As Double = 0


        For j = 0 To ds.DataGridView3.Rows.Count - 1
            sum = sum + ds.DataGridView3.Rows(j).Cells(6).Value
        Next
        For i = 0 To ds.DataGridView3.Rows.Count - 1
            prixHT = ds.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
            quantite = ds.DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
            tauxTVA = ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
            pourcentageRemise = ds.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

            ' Étape 1 : Calculer le montant HT après la remise
            montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

            ' Étape 2 : Calculer le montant TTC avant TVA
            montantTTCAvantTVA = montantHTApresRemise * quantite

            ' Étape 3 : Calculer le montant de la TVA
            montantTVA = montantTTCAvantTVA * tauxTVA

            sum2 += montantTTCAvantTVA
            sum3 += montantTVA
        Next
        ds.Label23.Text = sum2
        ds.Label24.Text = sum3
        ds.Label25.Text = ds.Label23.Text


        ds.Label6.Text = Convert.ToDouble(sum).ToString("N2")
        ds.Label7.Text = Convert.ToDouble(sum).ToString("N2")
        Me.Close()

    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        ds.DataGridView3.SelectedCells(3).Value = Convert.ToDouble(TextBox1.Text.Replace(".", ",")).ToString("# ##0.00")
        ds.DataGridView3.SelectedCells(12).Value = "change"

        Dim prixHT As Decimal = ds.DataGridView3.SelectedCells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
        Dim quantite As Decimal = ds.DataGridView3.SelectedCells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.SelectedCells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
        Dim tauxTVA As Decimal = ds.DataGridView3.SelectedCells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
        Dim pourcentageRemise As Decimal = ds.DataGridView3.SelectedCells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

        ' Étape 1 : Calculer le montant HT après la remise
        Dim montantHTApresRemise As Decimal = prixHT * (1 - pourcentageRemise / 100)

        ' Étape 2 : Calculer le montant TTC avant TVA
        Dim montantTTCAvantTVA As Decimal = montantHTApresRemise * quantite

        ' Étape 3 : Calculer le montant de la TVA
        Dim montantTVA As Decimal = montantTTCAvantTVA * tauxTVA

        ' Étape 4 : Calculer le prix TTC
        Dim prixTTC As Decimal = montantTTCAvantTVA + montantTVA


        Dim result As Decimal
        result = Math.Round(prixTTC, 2) ' Arrondir à deux décimales
        ds.DataGridView3.SelectedCells(6).Value = result


        Dim sum As Double = 0
        Dim sum2 As Double = 0
        Dim sum3 As Double = 0


        For j = 0 To ds.DataGridView3.Rows.Count - 1
            sum = sum + ds.DataGridView3.Rows(j).Cells(6).Value
        Next
        For i = 0 To ds.DataGridView3.Rows.Count - 1
            prixHT = ds.DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
            quantite = ds.DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - ds.DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
            tauxTVA = ds.DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
            pourcentageRemise = ds.DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

            ' Étape 1 : Calculer le montant HT après la remise
            montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

            ' Étape 2 : Calculer le montant TTC avant TVA
            montantTTCAvantTVA = montantHTApresRemise * quantite

            ' Étape 3 : Calculer le montant de la TVA
            montantTVA = montantTTCAvantTVA * tauxTVA

            sum2 += montantTTCAvantTVA
            sum3 += montantTVA
        Next
        ds.Label23.Text = sum2
        ds.Label24.Text = sum3
        ds.Label25.Text = ds.Label23.Text


        ds.Label6.Text = Convert.ToDouble(sum).ToString("N2")
        ds.Label7.Text = Convert.ToDouble(sum).ToString("N2")
        Me.Close()
    End Sub
End Class