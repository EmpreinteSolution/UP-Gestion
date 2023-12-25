Imports System.ComponentModel
Imports System.Configuration
Imports System.Drawing.Printing
Imports System.IO
Imports MySql.Data.MySqlClient

Public Class statistique
    Dim adpt1, adpt2, adpt3, adpt9 As MySqlDataAdapter

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Panel6.Visible = True
        DataGridView1.Rows.Clear()

        ' Lancer le BackgroundWorker pour effectuer le chargement de données en arrière-plan
        BackgroundWorker2.RunWorkerAsync()

    End Sub
    Private Sub Backgroundworker2_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker2.DoWork

        conn2.Close()
        conn2.Open()
        adpt1 = New MySqlDataAdapter("SELECT `OrderID`,`client` FROM `orders` where total_ht = 0 AND OrderDate BETWEEN '" & Convert.ToDateTime(DateTimePicker4.Value.Date).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(DateTimePicker3.Value.Date.AddDays(+1)).ToString("yyyy-MM-dd") & "'", conn)
        Dim products As New DataTable
        adpt1.Fill(products)
        Dim totalRowCount As Integer ' Nombre total de lignes à charger
        Dim rowCountLoaded As Integer = 0 ' Nombre de lignes déjà chargées

        totalRowCount = products.Rows.Count

        For K = 0 To products.Rows.Count - 1
            Dim ca As Double = 0
            Dim marge As Double = 0
            adpt1 = New MySqlDataAdapter("SELECT `pa`,`pv`,`Quantity` FROM `orderdetails` where OrderID = '" & products.Rows(K).Item(0) & "'", conn)
            Dim productsdet As New DataTable
            adpt1.Fill(productsdet)
            For j = 0 To productsdet.Rows.Count - 1
                ca = ca + (productsdet.Rows(j).Item(1).ToString.Replace(" ", "").Replace(".", ",") * productsdet.Rows(j).Item(2).ToString.Replace(" ", "").Replace(".", ","))
                marge = marge + ((productsdet.Rows(j).Item(1).ToString.Replace(" ", "").Replace(".", ",") - productsdet.Rows(j).Item(0).ToString.Replace(" ", "").Replace(".", ",")) * productsdet.Rows(j).Item(2).ToString.Replace(" ", "").Replace(".", ","))
            Next
            Dim sql9 As String
            Dim cmd9 As MySqlCommand
            sql9 = "UPDATE `orders` SET `total_ht`= @v1, marge = @v3 WHERE `OrderID` = @v2"
            cmd9 = New MySqlCommand(sql9, conn2)
            cmd9.Parameters.Clear()
            cmd9.Parameters.AddWithValue("@v1", Convert.ToDouble(ca.ToString.Replace(" ", "")).ToString("N2"))
            cmd9.Parameters.AddWithValue("@v3", Convert.ToDouble(marge.ToString.Replace(" ", "")).ToString("N2"))
            cmd9.Parameters.AddWithValue("@v2", products.Rows(K).Item(0))
            cmd9.ExecuteNonQuery()

            rowCountLoaded += 1
            Me.Invoke(Sub()
                          Panel8.Width = CInt((K / products.Rows.Count) * 546)
                          Label3.Text = Convert.ToDouble((rowCountLoaded / products.Rows.Count) * 100).ToString("N0") & "%"
                          Label4.Text = "Collecte des données ..."
                      End Sub)

        Next
        conn2.Close()

        adpt1 = New MySqlDataAdapter("select client from orders WHERE OrderDate BETWEEN @debut and @fin group by client", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@debut", Convert.ToDateTime(DateTimePicker4.Value.Date).ToString("yyyy-MM-dd"))
        adpt1.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(DateTimePicker3.Value.Date.AddDays(+1)).ToString("yyyy-MM-dd"))
        Dim tableclientgroup As New DataTable
        adpt1.Fill(tableclientgroup)


        ' Obtenez le nombre total de lignes à charger (par exemple, à partir de votre requête SQL)
        ' Remplacez cette ligne par votre propre méthode pour obtenir le nombre total de lignes
        totalRowCount = tableclientgroup.Rows.Count
        rowCountLoaded = 0
        ' Parcourez et chargez les données ici
        ' Chargez la ligne i de la base de données
        ' Remplacez cette ligne par votre propre logique de chargement de données

        ' Mettez à jour la ProgressBar en fonction de l'avancement du chargement
        Dim sumCA As Double = 0
        Dim sumMARG As Double = 0
        For i = 0 To tableclientgroup.Rows.Count - 1
            Dim ca As Double = 0
            Dim mrg As Double = 0
            adpt2 = New MySqlDataAdapter("select total_ht,marge from orders WHERE OrderDate BETWEEN @debut and @fin and client = '" & tableclientgroup.Rows(i).Item(0) & "'", conn2)
            adpt2.SelectCommand.Parameters.Clear()
            adpt2.SelectCommand.Parameters.AddWithValue("@debut", Convert.ToDateTime(DateTimePicker4.Value.Date).ToString("yyyy-MM-dd"))
            adpt2.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(DateTimePicker3.Value.Date.AddDays(+1)).ToString("yyyy-MM-dd"))
            Dim tableclientcalc As New DataTable
            adpt2.Fill(tableclientcalc)
            For j = 0 To tableclientcalc.Rows.Count - 1
                ca = ca + Convert.ToDouble(tableclientcalc.Rows(j).Item(0).ToString.Replace(" ", "").Replace(".", ","))
                mrg = mrg + Convert.ToDouble(tableclientcalc.Rows(j).Item(1).ToString.Replace(" ", "").Replace(".", ","))
            Next
            adpt3 = New MySqlDataAdapter("SELECT `remisepour`,`remise` FROM `clients` where client = '" & tableclientgroup.Rows(i).Item(0) & "'", conn)
            Dim clt As New DataTable
            adpt3.Fill(clt)
            If clt.Rows.Count <> 0 Then
                Me.Invoke(Sub()

                              DataGridView1.Rows.Add(tableclientgroup.Rows(i).Item(0), Convert.ToDouble(ca.ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(mrg.ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(((mrg * 100) / ca).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(clt.Rows(0).Item(0).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(clt.Rows(0).Item(1).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(ca.ToString.Replace(" ", "").Replace(".", ",")), Convert.ToDouble(mrg.ToString.Replace(" ", "").Replace(".", ",")))
                          End Sub)
                rowCountLoaded += 1
                Me.Invoke(Sub()
                              Panel8.Width = CInt((i / tableclientgroup.Rows.Count) * 546)
                              Label3.Text = Convert.ToDouble((rowCountLoaded / tableclientgroup.Rows.Count) * 100).ToString("N0") & "%"
                              Label4.Text = "Chargement des données ..."
                          End Sub)
                sumCA = sumCA + Convert.ToDouble(ca.ToString.Replace(" ", "").Replace(".", ","))
                sumMARG = sumMARG + Convert.ToDouble(mrg.ToString.Replace(" ", "").Replace(".", ","))
            End If

        Next
        Dim tm As Double = (sumMARG / sumCA) * 100
        Me.Invoke(Sub()
                      Panel6.Visible = False
                      TextBox20.Text = sumCA.ToString("N2")
                      TextBox2.Text = sumMARG.ToString("N2")
                      TextBox3.Text = tm.ToString("N2") & " %"

                  End Sub)
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
    Private Sub datagridview6_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView6.CellPainting
        ' Vérifiez si c'est une ligne de données (pas la ligne d'en-tête ni la ligne de total)
        If e.RowIndex >= 0 Then
            ' Supprimez les bordures des cellules sauf pour la dernière ligne
            If e.RowIndex < DataGridView6.Rows.Count - 1 Then
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None
            End If
            e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None
        End If
    End Sub
    Private Sub datagridview7_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView7.CellPainting
        ' Vérifiez si c'est une ligne de données (pas la ligne d'en-tête ni la ligne de total)
        If e.RowIndex >= 0 Then
            ' Supprimez les bordures des cellules sauf pour la dernière ligne
            If e.RowIndex < DataGridView7.Rows.Count - 1 Then
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None
            End If
            e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None
        End If
    End Sub


    Private Sub statistique_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Dim sroted As Integer = 0
    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        If sroted = 0 Then
            If e.ColumnIndex = 1 Then
                DataGridView1.Sort(DataGridView1.Columns(6), System.ComponentModel.ListSortDirection.Descending)
                sroted = 1
            End If
            If e.ColumnIndex = 2 Then
                DataGridView1.Sort(DataGridView1.Columns(7), System.ComponentModel.ListSortDirection.Descending)
                sroted = 1
            End If
        Else
            If e.ColumnIndex = 1 Then
                DataGridView1.Sort(DataGridView1.Columns(6), System.ComponentModel.ListSortDirection.Ascending)
                sroted = 0
            End If
            If e.ColumnIndex = 2 Then
                DataGridView1.Sort(DataGridView1.Columns(7), System.ComponentModel.ListSortDirection.Ascending)
                sroted = 0
            End If
        End If

    End Sub

    Private Sub DataGridView4_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView4.ColumnHeaderMouseClick
        If sroted = 0 Then
            If e.ColumnIndex = 1 Then
                DataGridView4.Sort(DataGridView4.Columns(4), System.ComponentModel.ListSortDirection.Descending)
                sroted = 1
            End If
            If e.ColumnIndex = 2 Then
                DataGridView4.Sort(DataGridView4.Columns(5), System.ComponentModel.ListSortDirection.Descending)
                sroted = 1
            End If
            If e.ColumnIndex = 3 Then
                DataGridView4.Sort(DataGridView4.Columns(6), System.ComponentModel.ListSortDirection.Descending)
                sroted = 1
            End If
        Else
            If e.ColumnIndex = 1 Then
                DataGridView4.Sort(DataGridView4.Columns(4), System.ComponentModel.ListSortDirection.Ascending)
                sroted = 0
            End If
            If e.ColumnIndex = 2 Then
                DataGridView4.Sort(DataGridView4.Columns(5), System.ComponentModel.ListSortDirection.Ascending)
                sroted = 0
            End If

            If e.ColumnIndex = 3 Then
                DataGridView4.Sort(DataGridView4.Columns(6), System.ComponentModel.ListSortDirection.Ascending)
                sroted = 0
            End If
        End If

    End Sub
    Private Sub DateTimePicker4_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker4.ValueChanged

    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Dim sql2, sql3 As String

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click

        DataGridView5.Rows.Clear()

        ' Lancer le BackgroundWorker pour effectuer le chargement de données en arrière-plan
        BackgroundWorker3.RunWorkerAsync()

    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Panel6.Visible = True
        DataGridView4.Rows.Clear()

        ' Lancer le BackgroundWorker pour effectuer le chargement de données en arrière-plan
        BackgroundWorker4.RunWorkerAsync()
    End Sub

    Private Sub Backgroundworker3_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker3.DoWork

        conn2.Close()
        conn2.Open()

        Dim totalRowCount As Integer ' Nombre total de lignes à charger
        Dim rowCountLoaded As Integer = 0 ' Nombre de lignes déjà chargées
        Dim vaht As Double = 0
        Dim vattc As Double = 0
        Dim caht As Double = 0
        Dim cattc As Double = 0
        Dim mrg As Double = 0
        Dim qtea As Double = 0
        Dim qtev As Double = 0

        adpt1 = New MySqlDataAdapter("SELECT article,sum(REPLACE(`Stock`,',','.')),date FROM `inventaire` where valid = 1 and Code = '" & Label29.Text & "' group by article", conn)
        Dim inventaire As New DataTable
        adpt1.Fill(inventaire)
        For i = 0 To inventaire.Rows.Count - 1
            totalRowCount += inventaire.Rows.Count
            rowCountLoaded += 1
            Me.Invoke(Sub()

                          DataGridView5.Rows.Add(Convert.ToDateTime(inventaire.Rows(i).Item(2)).ToString("yyyy/MM/dd"), "Inventaire", Convert.ToDouble(inventaire.Rows(i).Item(1).ToString.Replace(".", ",")).ToString("N2"))

                      End Sub)

        Next

        adpt3 = New MySqlDataAdapter("SELECT `id`,dateAchat,`Fournisseur` FROM `achat` where avoir = 0", conn)
        Dim achatfirst As New DataTable
        adpt3.Fill(achatfirst)
        For j = 0 To achatfirst.Rows.Count - 1
            adpt3 = New MySqlDataAdapter("SELECT `CodeArticle`, `qte`,PA_HT,PA_TTC FROM `achatdetails` where achat_Id = '" & achatfirst.Rows(j).Item(0) & "' and CodeArticle = '" & Label29.Text & "'", conn)
            Dim achat As New DataTable
            adpt3.Fill(achat)
            For i = 0 To achat.Rows.Count - 1
                totalRowCount += achat.Rows.Count
                rowCountLoaded += 1
                Me.Invoke(Sub()
                              DataGridView5.Rows.Add(Convert.ToDateTime(achatfirst.Rows(j).Item(1)).ToString("yyyy/MM/dd"), "Réception de l'Article | Frs ==> " & achatfirst.Rows(j).Item(2), Convert.ToDouble(achat.Rows(i).Item(1).ToString.Replace(".", ",")).ToString("N2"))
                              vaht += Convert.ToDouble(achat.Rows(i).Item(2).ToString.Replace(".", ",") * achat.Rows(i).Item(1).ToString.Replace(".", ","))
                              vattc += Convert.ToDouble(achat.Rows(i).Item(3).ToString.Replace(".", ",") * achat.Rows(i).Item(1).ToString.Replace(".", ","))
                              qtea += Convert.ToDouble(achat.Rows(i).Item(1).ToString.Replace(".", ","))

                          End Sub)

            Next

        Next

        adpt3 = New MySqlDataAdapter("SELECT `id`,dateAchat,`Fournisseur` FROM `achat` where avoir = 1", conn)
        Dim avoirfirst As New DataTable
        adpt3.Fill(avoirfirst)
        For j = 0 To avoirfirst.Rows.Count - 1
            adpt3 = New MySqlDataAdapter("SELECT `CodeArticle`, `qte`  FROM `achatdetails` where achat_Id = '" & avoirfirst.Rows(j).Item(0) & "' and CodeArticle = '" & Label29.Text & "'", conn)
            Dim avoir As New DataTable
            adpt3.Fill(avoir)
            For i = 0 To avoir.Rows.Count - 1
                totalRowCount += avoir.Rows.Count
                rowCountLoaded += 1
                Me.Invoke(Sub()

                              DataGridView5.Rows.Add(Convert.ToDateTime(avoirfirst.Rows(j).Item(1)).ToString("yyyy/MM/dd"), "Retour de l'Article en Avoir | Frs ==> " & achatfirst.Rows(j).Item(2), Convert.ToDouble(avoir.Rows(i).Item(1).ToString.Replace(".", ",")).ToString("N2"))

                          End Sub)


            Next
        Next

        adpt3 = New MySqlDataAdapter("SELECT `code`, qte, date FROM `demarque` where code = '" & Label29.Text & "'", conn)
        Dim demarq As New DataTable
        adpt3.Fill(demarq)
        For j = 0 To demarq.Rows.Count - 1

            Me.Invoke(Sub()

                          DataGridView5.Rows.Add(Convert.ToDateTime(demarq.Rows(j).Item(2)).ToString("yyyy/MM/dd"), "Démarque de l'Article", Convert.ToDouble(demarq.Rows(j).Item(1).ToString.Replace(".", ",")).ToString("N2"))

                      End Sub)

        Next

        adpt1 = New MySqlDataAdapter("SELECT ProductID,sum(REPLACE(`Quantity`,',','.')),date,sum(REPLACE(`Quantity`,',','.') * REPLACE(`pv`,',','.')),sum(REPLACE(`Quantity`,',','.') * REPLACE(`Price`,',','.')),sum(REPLACE(`marge`,',','.')),OrderID FROM `orderdetails` where ProductID = '" & Label29.Text & "' group by OrderID", conn)
        Dim orders As New DataTable
        adpt1.Fill(orders)
        For i = 0 To orders.Rows.Count - 1
            totalRowCount += orders.Rows.Count
            rowCountLoaded += 1
            adpt3 = New MySqlDataAdapter("SELECT `client` FROM `orders` where OrderID = '" & orders.Rows(i).Item(6) & "'", conn)
            Dim avoirord As New DataTable
            adpt3.Fill(avoirord)
            Dim clt As String
            If avoirord.Rows.Count = 0 Then
                clt = ""
            Else
                clt = avoirord.Rows(0).Item(0)
            End If
            Me.Invoke(Sub()

                          DataGridView5.Rows.Add(Convert.ToDateTime(orders.Rows(i).Item(2)).ToString("yyyy/MM/dd"), "Vente de l'Article | Client ==> " & clt, Convert.ToDouble(orders.Rows(i).Item(1).ToString.Replace(".", ",")).ToString("N2"))
                          caht += Convert.ToDouble(orders.Rows(i).Item(3).ToString.Replace(".", ","))
                          cattc += Convert.ToDouble(orders.Rows(i).Item(4).ToString.Replace(".", ","))
                          mrg += Convert.ToDouble(orders.Rows(i).Item(5).ToString.Replace(".", ","))
                          qtev += Convert.ToDouble(orders.Rows(i).Item(1).ToString.Replace(".", ","))

                      End Sub)

        Next

        Me.Invoke(Sub()
                      DataGridView5.Sort(DataGridView5.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
                      TextBox1.Text = vaht.ToString("N2")
                      TextBox5.Text = vattc.ToString("N2")
                      TextBox6.Text = caht.ToString("N2")
                      TextBox7.Text = cattc.ToString("N2")
                      TextBox8.Text = mrg.ToString("N2")
                      TextBox9.Text = qtea.ToString("N2")
                      TextBox10.Text = qtev.ToString("N2")
                      For i = 0 To DataGridView5.Rows.Count - 1
                          If DataGridView5.Rows(i).Cells(1).Value = "Démarque de l'Article" Then
                              DataGridView5.Rows(i).DefaultCellStyle.ForeColor = Color.Red
                          End If
                          If DataGridView5.Rows(i).Cells(1).Value = "Réception de l'Article" Then
                              DataGridView5.Rows(i).DefaultCellStyle.ForeColor = Color.Blue
                          End If
                          If DataGridView5.Rows(i).Cells(1).Value = "Inventaire" Then
                              DataGridView5.Rows(i).DefaultCellStyle.ForeColor = Color.Blue
                          End If
                      Next
                  End Sub)





        conn2.Close()



    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Panel13.Visible = True
        Label43.Text = "Ajouter un participant :"
        TextBox16.Text = ""
        TextBox15.Text = 0.00
        ComboBox1.Text = "-"
        TextBox21.Text = 0.00
        TextBox17.Text = 0.00 & " %"
        Label54.Text = ""
        IconButton4.Visible = True
        IconButton13.Visible = False
    End Sub
    Dim idList As New List(Of Integer)
    Private Sub IconButton8_Click(sender As Object, e As EventArgs) Handles IconButton8.Click
        Panel14.Visible = True
        adpt3 = New MySqlDataAdapter("SELECT * FROM `participants` order by name asc", conn)
        Dim clt2 As New DataTable
        adpt3.Fill(clt2)
        ComboBox8.Items.Clear()

        If clt2.Rows.Count <> 0 Then
            For i = 0 To clt2.Rows.Count - 1
                ComboBox8.Items.Add(clt2.Rows(i).Item(1))
                idList.Add(clt2.Rows(i).Item(0))
            Next
        End If
        TextBox22.Text = 0
    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        Panel14.Visible = False

    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Panel13.Visible = False
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        conn2.Close()
        conn2.Open()
        Dim sql3 As String
        Dim cmd3 As MySqlCommand
        adpt3 = New MySqlDataAdapter("SELECT * FROM `participants` order by name asc", conn)
        Dim clt2 As New DataTable
        adpt3.Fill(clt2)
        Dim newpourc As Double = 0
        If clt2.Rows.Count <> 0 Then
            For i = 0 To clt2.Rows.Count - 1
                If IsNumeric(clt2.Rows(i).Item(2)) Then
                    If clt2.Rows(i).Item(2) > 0 Then
                        newpourc = Convert.ToDouble((Convert.ToDouble(clt2.Rows(i).Item(2)) * 100) / (Convert.ToDouble(TextBox18.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDouble(TextBox15.Text.Replace(".", ",").Replace(" ", "")))).ToString("N2")
                    Else
                        newpourc = 0

                    End If
                Else
                    newpourc = 0

                End If
                sql3 = "UPDATE `participants` SET `pourc`='" & newpourc.ToString("N2") & "' WHERE `id` = '" & clt2.Rows(i).Item(0) & "'"
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.ExecuteNonQuery()

            Next

        End If


        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = "INSERT INTO participants ( `name`, `apports`,`pourc`,`type`,`montant`) VALUES (@v1,@v3,@v6,@v7,@v8)"
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.AddWithValue("@v1", TextBox16.Text)
        cmd2.Parameters.AddWithValue("@v3", Convert.ToDouble(TextBox15.Text.ToString.Replace(".", ",")))
        cmd2.Parameters.AddWithValue("@v6", Convert.ToDouble(TextBox17.Text.ToString.Replace(".", ",").Replace(" %", "")))
        cmd2.Parameters.AddWithValue("@v7", ComboBox1.Text)
        cmd2.Parameters.AddWithValue("@v8", Convert.ToDouble(TextBox21.Text.ToString.Replace(".", ",").Replace(" %", "")))

        cmd2.ExecuteNonQuery()
        conn2.Close()
        adpt3 = New MySqlDataAdapter("SELECT * FROM `participants` order by name asc", conn)
        Dim clt As New DataTable
        adpt3.Fill(clt)
        Chart1.Series("CA").Points.Clear()

        DataGridView6.Rows.Clear()
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
                DataGridView6.Rows.Add(clt.Rows(i).Item(0), clt.Rows(i).Item(1), Convert.ToDouble(clt.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(clt.Rows(i).Item(3).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2") & " %", sumdivid.ToString("N2"), clt.Rows(i).Item(4), Convert.ToDouble(clt.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
                ' Ajoutez des données au Pie Chart
                Chart1.Series("CA").Points.AddXY(clt.Rows(i).Item(1), Convert.ToDouble(clt.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")))
            Next

        End If

        TextBox18.Text = Convert.ToDouble(TextBox18.Text + Convert.ToDouble(TextBox15.Text.ToString.Replace(".", ","))).ToString("N2")
        TextBox16.Text = ""
        TextBox15.Text = 0
        TextBox21.Text = 0
        TextBox17.Text = "0 %"
        ComboBox1.Text = "-"

        MsgBox("Opération bien faite !")


    End Sub

    Private Sub TextBox15_TextChanged(sender As Object, e As EventArgs) Handles TextBox15.TextChanged
        If IsNumeric(TextBox15.Text) Then
            If TextBox15.Text > 0 Then
                TextBox17.Text = Convert.ToDouble((Convert.ToDouble(TextBox15.Text.Replace(".", ",").Replace(" ", "")) * 100) / (Convert.ToDouble(TextBox18.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDouble(TextBox15.Text.Replace(".", ",").Replace(" ", "")))).ToString("N2") & " %"
            Else
                TextBox17.Text = 0 & " %"

            End If
        Else
            TextBox17.Text = 0 & " %"

        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text = "distribution égale" Then
            TextBox21.Visible = True
            Label53.Visible = True
            TextBox21.Text = 0
        Else
            TextBox21.Visible = False
            Label53.Visible = False
            TextBox21.Text = 0
        End If

    End Sub

    Private Sub IconButton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click
        Panel13.Visible = True
        Label43.Text = "Modifier un participant :"
        TextBox16.Text = DataGridView6.SelectedRows(0).Cells(1).Value
        TextBox15.Text = DataGridView6.SelectedRows(0).Cells(2).Value
        ComboBox1.Text = DataGridView6.SelectedRows(0).Cells(5).Value
        TextBox21.Text = DataGridView6.SelectedRows(0).Cells(6).Value
        TextBox17.Text = DataGridView6.SelectedRows(0).Cells(3).Value
        Label54.Text = DataGridView6.SelectedRows(0).Cells(0).Value
        IconButton4.Visible = False
        IconButton13.Visible = True

    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        Dim Rep As Integer
        Rep = MsgBox("Voulez-vous vraiment supprimer ce participant ?" & vbCrLf & "Si vous supprimer un participant, l'historique des dividendes distribuées à ce participant sera supprimé aussi", vbYesNo)
        If Rep = vbYes Then
            conn2.Close()
            conn2.Open()

            Dim sql3 As String
            Dim cmd3 As MySqlCommand

            sql3 = "DELETE FROM `participants` WHERE `id` = '" & DataGridView6.SelectedRows(0).Cells(0).Value & "'"
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.ExecuteNonQuery()

            sql3 = "DELETE FROM `dividendes` WHERE `part_id` = '" & DataGridView6.SelectedRows(0).Cells(0).Value & "'"
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.ExecuteNonQuery()
            adpt3 = New MySqlDataAdapter("SELECT * FROM `participants` order by name asc", conn)
            Dim clt2 As New DataTable
            adpt3.Fill(clt2)
            Dim newpourc As Double = 0
            If clt2.Rows.Count <> 0 Then
                For i = 0 To clt2.Rows.Count - 1
                    If IsNumeric(clt2.Rows(i).Item(2)) Then
                        If clt2.Rows(i).Item(2) > 0 Then
                            newpourc = Convert.ToDouble((Convert.ToDouble(clt2.Rows(i).Item(2)) * 100) / (Convert.ToDouble(TextBox18.Text.Replace(".", ",").Replace(" ", "")) - Convert.ToDouble(DataGridView6.SelectedRows(0).Cells(2).Value.Replace(".", ",").Replace(" ", "")))).ToString("N2")
                        Else
                            newpourc = 0

                        End If
                    Else
                        newpourc = 0

                    End If
                    sql3 = "UPDATE `participants` SET `pourc`='" & newpourc.ToString("N2") & "' WHERE `id` = '" & clt2.Rows(i).Item(0) & "'"
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.ExecuteNonQuery()

                Next

            End If
            adpt3 = New MySqlDataAdapter("SELECT * FROM `participants` order by name asc", conn)
            Dim clt As New DataTable
            adpt3.Fill(clt)
            Chart1.Series("CA").Points.Clear()
            Dim sumcapital As Double = 0
            DataGridView6.Rows.Clear()
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
                    DataGridView6.Rows.Add(clt.Rows(i).Item(0), clt.Rows(i).Item(1), Convert.ToDouble(clt.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(clt.Rows(i).Item(3).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2") & " %", sumdivid.ToString("N2"), clt.Rows(i).Item(4), Convert.ToDouble(clt.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
                    ' Ajoutez des données au Pie Chart
                    sumcapital = sumcapital + Convert.ToDouble(clt.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ","))

                    Chart1.Series("CA").Points.AddXY(clt.Rows(i).Item(1), Convert.ToDouble(clt.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")))
                Next

            End If

            TextBox18.Text = sumcapital.ToString("N2")

            conn2.Close()
            MsgBox("Opération bien faite !")

        End If

    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click

        conn2.Open()
        Dim cmd3 As MySqlCommand
        sql3 = "UPDATE `participants` SET `name`='" & TextBox16.Text & "',`apports`='" & TextBox15.Text & "',`pourc`='" & TextBox17.Text.Replace(" %", "") & "',`type`='" & ComboBox1.Text & "',`montant`='" & TextBox21.Text & "' where id = '" & Label54.Text & "'"
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.ExecuteNonQuery()

        adpt3 = New MySqlDataAdapter("SELECT * FROM `participants` order by name asc", conn)
        Dim clt2 As New DataTable
        adpt3.Fill(clt2)
        Dim newpourc As Double = 0
        Dim sumcapital As Double = 0
        If clt2.Rows.Count <> 0 Then
            For i = 0 To clt2.Rows.Count - 1

                sumcapital = sumcapital + Convert.ToDouble(clt2.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ","))

            Next
            For i = 0 To clt2.Rows.Count - 1

                If IsNumeric(clt2.Rows(i).Item(2)) Then
                    If clt2.Rows(i).Item(2) > 0 Then
                        newpourc = Convert.ToDouble((Convert.ToDouble(clt2.Rows(i).Item(2)) * 100) / (sumcapital)).ToString("N2")
                    Else
                        newpourc = 0

                    End If
                Else
                    newpourc = 0

                End If
                sql3 = "UPDATE `participants` SET `pourc`='" & newpourc.ToString("N2") & "' WHERE `id` = '" & clt2.Rows(i).Item(0) & "'"
                cmd3 = New MySqlCommand(sql3, conn2)
                cmd3.ExecuteNonQuery()

            Next

        End If

        adpt3 = New MySqlDataAdapter("SELECT * FROM `participants` order by name asc", conn)
        Dim clt As New DataTable
        adpt3.Fill(clt)
        Chart1.Series("CA").Points.Clear()
        DataGridView6.Rows.Clear()
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
                DataGridView6.Rows.Add(clt.Rows(i).Item(0), clt.Rows(i).Item(1), Convert.ToDouble(clt.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(clt.Rows(i).Item(3).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2") & " %", sumdivid.ToString("N2"), clt.Rows(i).Item(4), Convert.ToDouble(clt.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
                ' Ajoutez des données au Pie Chart

                Chart1.Series("CA").Points.AddXY(clt.Rows(i).Item(1), Convert.ToDouble(clt.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")))
            Next

        End If

        TextBox18.Text = sumcapital.ToString("N2")
        TextBox16.Text = ""
        TextBox15.Text = 0
        TextBox21.Text = 0
        TextBox17.Text = "0 %"
        ComboBox1.Text = "-"
        conn2.Close()
        MsgBox("Opération bien faite !")
    End Sub

    Private Sub IconButton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        Dim selectedIndex As Integer = ComboBox8.SelectedIndex
        conn2.Close()
        conn2.Open()
        Dim sql2 As String
        Dim cmd2 As MySqlCommand
        sql2 = "INSERT INTO dividendes (`part_id`, `montant`,`date`) VALUES (@v1,@v3,@v6)"
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.AddWithValue("@v1", idList(selectedIndex))
        cmd2.Parameters.AddWithValue("@v3", Convert.ToDouble(TextBox22.Text.ToString.Replace(".", ",")))
        cmd2.Parameters.AddWithValue("@v6", DateTimePicker5.Value.ToString("yyyy-MM-dd"))

        cmd2.ExecuteNonQuery()
        conn2.Close()

        adpt3 = New MySqlDataAdapter("SELECT * FROM `participants` order by name asc", conn)
        Dim clt As New DataTable
        adpt3.Fill(clt)
        Chart1.Series("CA").Points.Clear()

        DataGridView6.Rows.Clear()
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
                DataGridView6.Rows.Add(clt.Rows(i).Item(0), clt.Rows(i).Item(1), Convert.ToDouble(clt.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(clt.Rows(i).Item(3).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2") & " %", sumdivid.ToString("N2"), clt.Rows(i).Item(4), Convert.ToDouble(clt.Rows(i).Item(5).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
                ' Ajoutez des données au Pie Chart
                Chart1.Series("CA").Points.AddXY(clt.Rows(i).Item(1), Convert.ToDouble(clt.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")))
            Next

        End If
        TextBox22.Text = 0
        TextBox22.ReadOnly = False
        DateTimePicker5.Value = DateTime.Today
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        Dim selectedIndex As Integer = ComboBox8.SelectedIndex
        If selectedIndex >= 0 Then
            adpt3 = New MySqlDataAdapter("SELECT * FROM `participants` where id = '" & idList(selectedIndex) & "'", conn)
            Dim clt2 As New DataTable
            adpt3.Fill(clt2)
            If clt2.Rows.Count <> 0 Then
                If clt2.Rows(0).Item(4) = "distribution égale" Then
                    TextBox22.Text = Convert.ToDouble(clt2.Rows(0).Item(5).ToString.Replace(".", ",")).ToString("N2")
                    TextBox22.ReadOnly = True
                End If
                Dim Fin As DateTime
                If clt2.Rows(0).Item(4) = "distribution proportionnelle" Then

                    adpt3 = New MySqlDataAdapter("SELECT * FROM `dividendes` where part_id = '" & idList(selectedIndex) & "' order by id desc limit 1", conn)
                    Dim clt As New DataTable
                    adpt3.Fill(clt)
                    If clt.Rows.Count <> 0 Then
                        Fin = clt.Rows(0).Item(3)
                    Else
                        Fin = DateTime.Today().ToString("yyyy-MM-dd")
                    End If
                    adpt1 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(marge,',','.'),' ','')) from orderdetails WHERE date >= @fin", conn2)
                    adpt1.SelectCommand.Parameters.Clear()
                    adpt1.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(Fin).ToString("yyyy-MM-dd"))
                    Dim tablemargemois As New DataTable
                        adpt1.Fill(tablemargemois)

                        Dim margemois As Double = 0


                        If IsDBNull(tablemargemois.Rows(0).Item(0)) Then
                        Else
                            margemois = Convert.ToDouble(tablemargemois.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))
                        End If


                        Dim chargyear As Double = 0

                    adpt1 = New MySqlDataAdapter("SELECT SUM(REPLACE(REPLACE(j,',','.'),' ','') + REPLACE(REPLACE(f,',','.'),' ','') + REPLACE(REPLACE(m,',','.'),' ','') +REPLACE(REPLACE(a,',','.'),' ','') +REPLACE(REPLACE(mai,',','.'),' ','') +REPLACE(REPLACE(juin,',','.'),' ','') +REPLACE(REPLACE(juil,',','.'),' ','') +REPLACE(REPLACE(ao,',','.'),' ','') +REPLACE(REPLACE(sept,',','.'),' ','') +REPLACE(REPLACE(oct,',','.'),' ','') +REPLACE(REPLACE(nov,',','.'),' ','') +REPLACE(REPLACE(dece,',','.'),' ','') ) FROM charges WHERE date >= @fin", conn2)
                    adpt1.SelectCommand.Parameters.Clear()
                    adpt1.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(Fin).ToString("yyyy-MM-dd"))
                    Dim tableorder3 As New DataTable
                        adpt1.Fill(tableorder3)
                        If IsDBNull(tableorder3.Rows(0).Item(0)) Then
                        Else
                            chargyear = Convert.ToDouble(tableorder3.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

                        End If


                        Dim rmsmois As Double = 0

                    adpt1 = New MySqlDataAdapter("select sum(Replace(REPLACE(`remise`,',','.'),' ','')) from facture where remise > 0 and OrderDate >= @fin", conn2)
                    adpt1.SelectCommand.Parameters.Clear()
                    adpt1.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(Fin).ToString("yyyy-MM-dd"))
                    Dim tablermsmois As New DataTable
                    adpt1.Fill(tablermsmois)
                    If IsDBNull(tablermsmois.Rows(0).Item(0)) Then
                    Else
                        rmsmois = Convert.ToDouble(tablermsmois.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

                    End If



                    Dim demarqyear As Double = 0

                    adpt1 = New MySqlDataAdapter("select sum(Replace(REPLACE(`valeur`,',','.'),' ','')) from demarque where date >=  @fin", conn2)
                    adpt1.SelectCommand.Parameters.Clear()
                    adpt1.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(Fin).ToString("yyyy-MM-dd"))
                    Dim tabledemarqyear As New DataTable
                        adpt1.Fill(tabledemarqyear)
                        If IsDBNull(tabledemarqyear.Rows(0).Item(0)) Then
                        Else
                            demarqyear = Convert.ToDouble(tabledemarqyear.Rows(0).Item(0).ToString.Replace(" ", "").Replace(".", ","))

                        End If
                    TextBox22.Text = Convert.ToDouble((margemois - chargyear - rmsmois - demarqyear) * (clt2.Rows(0).Item(3).ToString.Replace(".", ",") / 100)).ToString("N2")

                    TextBox22.ReadOnly = True
                End If
                    If clt2.Rows(0).Item(4) = "distribution personnalisée" Then
                    TextBox22.Text = 0
                    TextBox22.ReadOnly = False
                End If
            End If

        End If
    End Sub

    Private Sub Panel12_Paint(sender As Object, e As PaintEventArgs) Handles Panel12.Paint

    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Close()
    End Sub

    Private Sub IconButton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        Panel15.Visible = False
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        If DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> "" Then
            Dim val As Double = DataGridView1.Rows(e.RowIndex).Cells(1).Value * (DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") / 100)
            DataGridView1.Rows(e.RowIndex).Cells(5).Value = Convert.ToDouble(val.ToString.Replace(" ", "").Replace(".", ",")).ToString("N2")
            DataGridView1.Rows(e.RowIndex).Cells(4).Value = Convert.ToDouble(DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",")).ToString("N2")
            conn2.Open()
            Dim cmd3 As MySqlCommand
            sql3 = "Update `clients` SET `remisepour`='" & DataGridView1.Rows(e.RowIndex).Cells(4).Value & "',`remise`= '" & DataGridView1.Rows(e.RowIndex).Cells(5).Value & "' WHERE `client` = '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value & "'"
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.ExecuteNonQuery()

            conn2.Close()
        Else
            DataGridView1.Rows(e.RowIndex).Cells(4).Value = 0.00
            DataGridView1.Rows(e.RowIndex).Cells(5).Value = 0.00
            conn2.Open()
            Dim cmd3 As MySqlCommand
            sql3 = "Update `clients` SET `remisepour`='" & DataGridView1.Rows(e.RowIndex).Cells(4).Value & "',`remise`= '" & DataGridView1.Rows(e.RowIndex).Cells(5).Value & "' WHERE `client` = '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value & "'"
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.ExecuteNonQuery()

            conn2.Close()
        End If
    End Sub

    Private Sub IconButton15_Click(sender As Object, e As EventArgs) Handles IconButton15.Click
        DataGridView5.Rows.Clear()

        ' Lancer le BackgroundWorker pour effectuer le chargement de données en arrière-plan
        BackgroundWorker5.RunWorkerAsync()
    End Sub

    Private Sub DataGridView2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        adpt1 = New MySqlDataAdapter("select * from inventaire where inv_id = '" & DataGridView2.Rows(e.RowIndex).Cells(4).Value & "'", conn2)
        Dim tableinv As New DataTable
        adpt1.Fill(tableinv)
        DataGridView3.Rows.Clear()
        If tableinv.Rows.Count <> 0 Then
            For i = 0 To tableinv.Rows.Count - 1
                DataGridView3.Rows.Add(tableinv.Rows(i).Item(1), tableinv.Rows(i).Item(2), Convert.ToDouble(tableinv.Rows(i).Item(3).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(tableinv.Rows(i).Item(6).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(tableinv.Rows(i).Item(7).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
            Next
        End If
        Label6.Text = Convert.ToDateTime(DataGridView2.Rows(e.RowIndex).Cells(0).Value).ToString("dd/MM/yyyy")
    End Sub

    Private Sub IconButton18_Click(sender As Object, e As EventArgs) Handles IconButton18.Click
        adpt1 = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Correction du Stock'", conn2)
        Dim tabledroit As New DataTable
        adpt1.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then
                adpt2 = New MySqlDataAdapter("SELECT count(*) FROM orders where cloture = 0 ", conn2)
                Dim tablecountstock As New DataTable
                adpt2.Fill(tablecountstock)
                If tablecountstock.Rows.Count <> 0 Then
                    MsgBox("La correction de stock peut commencer juste après la cloture de la caisse, veuillez cloture votre caisse afin de pouvoir corriger votre stock !")
                Else
                    Dim Rep As Integer
                    Rep = MsgBox("Voulez-vous vraiment Commencer la correction du stock ?", vbYesNo)
                    If Rep = vbYes Then
                        expot.Show()

                    End If
                End If

            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub BackgroundWorker4_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker4.DoWork

        adpt1 = New MySqlDataAdapter("select Fournisseur,id from achat WHERE dateAchat BETWEEN @debut and @fin group by Fournisseur", conn2)
        adpt1.SelectCommand.Parameters.Clear()
        adpt1.SelectCommand.Parameters.AddWithValue("@debut", Convert.ToDateTime(DateTimePicker2.Value.Date).ToString("yyyy-MM-dd"))
        adpt1.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(DateTimePicker1.Value.Date.AddDays(+1)).ToString("yyyy-MM-dd"))
        Dim tableclientgroup As New DataTable
        adpt1.Fill(tableclientgroup)
        Dim ca As Double = 0
        Dim retour As Double = 0
        Dim rms As Double = 0
        Dim gratuit As Double = 0

        Dim sumca As Double = 0
        Dim sumretour As Double = 0
        Dim sumrms As Double = 0
        Dim sumgratuit As Double = 0

        For i = 0 To tableclientgroup.Rows.Count - 1

            adpt2 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(montant,',','.'),' ','')) from achat WHERE `avoir` = 0 and Fournisseur = '" & tableclientgroup.Rows(i).Item(0) & "' and dateAchat BETWEEN @debut and @fin", conn2)
            adpt2.SelectCommand.Parameters.Clear()
            adpt2.SelectCommand.Parameters.AddWithValue("@debut", Convert.ToDateTime(DateTimePicker2.Value.Date).ToString("yyyy-MM-dd"))
            adpt2.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(DateTimePicker1.Value.Date.AddDays(+1)).ToString("yyyy-MM-dd"))
            Dim tableca As New DataTable
            adpt2.Fill(tableca)
            For j = 0 To tableca.Rows.Count - 1
                If IsDBNull(tableca.Rows(j).Item(0)) Then
                    ca = 0
                Else
                    ca = ca + Convert.ToDouble(tableca.Rows(j).Item(0).ToString.Replace(" ", "").Replace(".", ","))
                End If
            Next

            adpt2 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(montant,',','.'),' ','') * (-1)) from achat WHERE dateAchat BETWEEN @debut and @fin and `avoir` = 1 and Fournisseur = '" & tableclientgroup.Rows(i).Item(0) & "'", conn2)
            adpt2.SelectCommand.Parameters.Clear()
            adpt2.SelectCommand.Parameters.AddWithValue("@debut", Convert.ToDateTime(DateTimePicker2.Value.Date).ToString("yyyy-MM-dd"))
            adpt2.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(DateTimePicker1.Value.Date.AddDays(+1)).ToString("yyyy-MM-dd"))
            Dim tableretour As New DataTable
            adpt2.Fill(tableretour)

            For j = 0 To tableretour.Rows.Count - 1
                If IsDBNull(tableretour.Rows(j).Item(0)) Then
                    retour = 0
                Else
                    retour = retour + Convert.ToDouble(tableretour.Rows(j).Item(0).ToString.Replace(" ", "").Replace(".", ","))

                End If
            Next



            adpt2 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(rms,',','.'),' ','')) from achat WHERE dateAchat BETWEEN @debut and @fin and `avoir` = 0 and Fournisseur = '" & tableclientgroup.Rows(i).Item(0) & "'", conn2)
            adpt2.SelectCommand.Parameters.Clear()
            adpt2.SelectCommand.Parameters.AddWithValue("@debut", Convert.ToDateTime(DateTimePicker2.Value.Date).ToString("yyyy-MM-dd"))
            adpt2.SelectCommand.Parameters.AddWithValue("@fin", Convert.ToDateTime(DateTimePicker1.Value.Date.AddDays(+1)).ToString("yyyy-MM-dd"))
            Dim tablerms As New DataTable
            adpt2.Fill(tablerms)
            For j = 0 To tablerms.Rows.Count - 1
                If IsDBNull(tablerms.Rows(j).Item(0)) Then
                    rms = 0
                Else
                    rms = rms + Convert.ToDouble(tablerms.Rows(j).Item(0).ToString.Replace(" ", "").Replace(".", ","))

                End If
            Next

            adpt2 = New MySqlDataAdapter("select SUM(REPLACE(REPLACE(gr,',','.'),' ','') * REPLACE(REPLACE(PA_TTC,',','.'),' ','')) from achatdetails WHERE `achat_Id` = '" & tableclientgroup.Rows(i).Item(1) & "' ", conn2)
            adpt2.SelectCommand.Parameters.Clear()
            Dim tablegratuit As New DataTable
            adpt2.Fill(tablegratuit)
            For j = 0 To tablegratuit.Rows.Count - 1
                If IsDBNull(tablegratuit.Rows(j).Item(0)) Then
                    gratuit = 0
                Else
                    gratuit = gratuit + Convert.ToDouble(tablegratuit.Rows(j).Item(0).ToString.Replace(" ", "").Replace(".", ","))
                End If
            Next

            Me.Invoke(Sub()

                          DataGridView4.Rows.Add(tableclientgroup.Rows(i).Item(0), Convert.ToDouble(ca.ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(retour.ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(rms.ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), Convert.ToDouble(ca.ToString.Replace(" ", "").Replace(".", ",")), Convert.ToDouble(retour.ToString.Replace(" ", "").Replace(".", ",")), Convert.ToDouble(rms.ToString.Replace(" ", "").Replace(".", ",")), Convert.ToDouble(gratuit.ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
                      End Sub)


            Me.Invoke(Sub()
                          Panel8.Width = CInt((i / tableclientgroup.Rows.Count) * 546)
                          Label3.Text = Convert.ToDouble((i / tableclientgroup.Rows.Count) * 100).ToString("N0") & "%"
                          Label4.Text = "Chargement des données ..."

                          sumca = sumca + ca
                          sumretour = sumretour + retour
                          sumrms = sumrms + rms
                          sumgratuit = sumgratuit + gratuit
                      End Sub)
            ca = 0
            retour = 0
            rms = 0
            gratuit = 0
        Next
        Me.Invoke(Sub()
                      Panel6.Visible = False
                      TextBox13.Text = sumca.ToString("N2")
                      TextBox12.Text = sumretour.ToString("N2")
                      TextBox11.Text = sumrms.ToString("N2")
                      TextBox14.Text = sumgratuit.ToString("N2")

                  End Sub)
    End Sub

    Private Sub DataGridView6_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView6.CellDoubleClick
        Panel15.Visible = True
        Label55.Text = DataGridView6.Rows(e.RowIndex).Cells(1).Value
        Label59.Text = DataGridView6.Rows(e.RowIndex).Cells(2).Value
        adpt3 = New MySqlDataAdapter("SELECT * FROM `dividendes` where part_id = '" & DataGridView6.Rows(e.RowIndex).Cells(0).Value & "' order by date desc", conn)
        Dim div As New DataTable
        adpt3.Fill(div)
        Dim sumdivid As Double = 0
        If div.Rows.Count <> 0 Then
            DataGridView7.Rows.Clear()
            For i = 0 To div.Rows.Count - 1
                DataGridView7.Rows.Add(Convert.ToDateTime(div.Rows(i).Item(3)).ToString("yyyy/MM/dd"), Convert.ToDouble(div.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"), div.Rows(i).Item(0), "X")
                sumdivid = sumdivid + div.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")
            Next
        End If
        Label60.Text = sumdivid.ToString("N2")
        Panel17.Width = CInt((sumdivid / DataGridView6.Rows(e.RowIndex).Cells(2).Value) * 546)
        Label58.Text = Convert.ToDouble((sumdivid / DataGridView6.Rows(e.RowIndex).Cells(2).Value) * 100).ToString("N0") & "%"
    End Sub

    Private Sub DataGridView7_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView7.CellClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 3 Then
                Dim Rep As Integer
                Rep = MsgBox("Voulez-vous vraiment supprimer cette dividendes ?", vbYesNo)
                If Rep = vbYes Then
                    conn2.Close()
                    conn2.Open()
                    Dim sql2 As String
                    Dim cmd2 As MySqlCommand
                    sql2 = "delete from dividendes where id = '" & DataGridView7.Rows(e.RowIndex).Cells(2).Value & "' "
                    cmd2 = New MySqlCommand(sql2, conn2)

                    cmd2.ExecuteNonQuery()
                    conn2.Close()
                    DataGridView7.Rows.RemoveAt(e.RowIndex)
                End If
            End If
        End If

    End Sub

    Private Sub BackgroundWorker5_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker5.DoWork
        conn2.Close()
        conn2.Open()

        Dim totalRowCount As Integer ' Nombre total de lignes à charger
        Dim rowCountLoaded As Integer = 0 ' Nombre de lignes déjà chargées
        Dim vaht As Double = 0
        Dim vattc As Double = 0
        Dim caht As Double = 0
        Dim cattc As Double = 0
        Dim mrg As Double = 0
        Dim qtea As Double = 0
        Dim qtev As Double = 0

        adpt1 = New MySqlDataAdapter("SELECT article,sum(REPLACE(`Stock`,',','.')),date FROM `inventaire` where valid = 1 and Code = '" & Label29.Text & "' and date BETWEEN '" & Convert.ToDateTime(DateTimePicker7.Value).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(DateTimePicker6.Value.AddDays(+1)).ToString("yyyy-MM-dd") & "' group by article", conn)
        Dim inventaire As New DataTable
        adpt1.Fill(inventaire)
        For i = 0 To inventaire.Rows.Count - 1
            totalRowCount += inventaire.Rows.Count
            rowCountLoaded += 1
            Me.Invoke(Sub()

                          DataGridView5.Rows.Add(Convert.ToDateTime(inventaire.Rows(i).Item(2)).ToString("yyyy/MM/dd"), "Inventaire", Convert.ToDouble(inventaire.Rows(i).Item(1).ToString.Replace(".", ",")).ToString("N2"))

                      End Sub)

        Next

        adpt3 = New MySqlDataAdapter("SELECT `id`,dateAchat,`Fournisseur` FROM `achat` where avoir = 0 and dateAchat BETWEEN '" & Convert.ToDateTime(DateTimePicker7.Value).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(DateTimePicker6.Value.AddDays(+1)).ToString("yyyy-MM-dd") & "'", conn)
        Dim achatfirst As New DataTable
        adpt3.Fill(achatfirst)
        For j = 0 To achatfirst.Rows.Count - 1
            adpt3 = New MySqlDataAdapter("SELECT `CodeArticle`, `qte`,PA_HT,PA_TTC FROM `achatdetails` where achat_Id = '" & achatfirst.Rows(j).Item(0) & "' and CodeArticle = '" & Label29.Text & "'", conn)
            Dim achat As New DataTable
            adpt3.Fill(achat)
            For i = 0 To achat.Rows.Count - 1
                totalRowCount += achat.Rows.Count
                rowCountLoaded += 1
                Me.Invoke(Sub()
                              DataGridView5.Rows.Add(Convert.ToDateTime(achatfirst.Rows(j).Item(1)).ToString("yyyy/MM/dd"), "Réception de l'Article | Frs ==> " & achatfirst.Rows(j).Item(2), Convert.ToDouble(achat.Rows(i).Item(1).ToString.Replace(".", ",")).ToString("N2"))
                              vaht += Convert.ToDouble(achat.Rows(i).Item(2).ToString.Replace(".", ",") * achat.Rows(i).Item(1).ToString.Replace(".", ","))
                              vattc += Convert.ToDouble(achat.Rows(i).Item(3).ToString.Replace(".", ",") * achat.Rows(i).Item(1).ToString.Replace(".", ","))
                              qtea += Convert.ToDouble(achat.Rows(i).Item(1).ToString.Replace(".", ","))

                          End Sub)

            Next

        Next

        adpt3 = New MySqlDataAdapter("SELECT `id`,dateAchat,`Fournisseur` FROM `achat` where avoir = 1 and dateAchat BETWEEN '" & Convert.ToDateTime(DateTimePicker7.Value).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(DateTimePicker6.Value.AddDays(+1)).ToString("yyyy-MM-dd") & "'", conn)
        Dim avoirfirst As New DataTable
        adpt3.Fill(avoirfirst)
        For j = 0 To avoirfirst.Rows.Count - 1
            adpt3 = New MySqlDataAdapter("SELECT `CodeArticle`, `qte`  FROM `achatdetails` where achat_Id = '" & avoirfirst.Rows(j).Item(0) & "' and CodeArticle = '" & Label29.Text & "'", conn)
            Dim avoir As New DataTable
            adpt3.Fill(avoir)
            For i = 0 To avoir.Rows.Count - 1
                totalRowCount += avoir.Rows.Count
                rowCountLoaded += 1
                Me.Invoke(Sub()

                              DataGridView5.Rows.Add(Convert.ToDateTime(avoirfirst.Rows(j).Item(1)).ToString("yyyy/MM/dd"), "Retour de l'Article en Avoir | Frs ==> " & achatfirst.Rows(j).Item(2), Convert.ToDouble(avoir.Rows(i).Item(1).ToString.Replace(".", ",")).ToString("N2"))

                          End Sub)


            Next
        Next

        adpt3 = New MySqlDataAdapter("SELECT `code`, qte, date FROM `demarque` where code = '" & Label29.Text & "' and date BETWEEN '" & Convert.ToDateTime(DateTimePicker7.Value).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(DateTimePicker6.Value.AddDays(+1)).ToString("yyyy-MM-dd") & "'", conn)
        Dim demarq As New DataTable
        adpt3.Fill(demarq)
        For j = 0 To demarq.Rows.Count - 1

            Me.Invoke(Sub()

                          DataGridView5.Rows.Add(Convert.ToDateTime(demarq.Rows(j).Item(2)).ToString("yyyy/MM/dd"), "Démarque de l'Article", Convert.ToDouble(demarq.Rows(j).Item(1).ToString.Replace(".", ",")).ToString("N2"))

                      End Sub)

        Next

        adpt1 = New MySqlDataAdapter("SELECT ProductID,sum(REPLACE(`Quantity`,',','.')),date,sum(REPLACE(`Quantity`,',','.') * REPLACE(`pv`,',','.')),sum(REPLACE(`Quantity`,',','.') * REPLACE(`Price`,',','.')),sum(REPLACE(`marge`,',','.')),OrderID FROM `orderdetails` where ProductID = '" & Label29.Text & "' and date BETWEEN '" & Convert.ToDateTime(DateTimePicker7.Value).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(DateTimePicker6.Value.AddDays(+1)).ToString("yyyy-MM-dd") & "' group by OrderID", conn)
        Dim orders As New DataTable
        adpt1.Fill(orders)
        For i = 0 To orders.Rows.Count - 1
            totalRowCount += orders.Rows.Count
            rowCountLoaded += 1
            adpt3 = New MySqlDataAdapter("SELECT `client` FROM `orders` where OrderID = '" & orders.Rows(i).Item(6) & "'", conn)
            Dim avoirord As New DataTable
            adpt3.Fill(avoirord)
            Dim clt As String
            If avoirord.Rows.Count = 0 Then
                clt = ""
            Else
                clt = avoirord.Rows(0).Item(0)
            End If
            Me.Invoke(Sub()

                          DataGridView5.Rows.Add(Convert.ToDateTime(orders.Rows(i).Item(2)).ToString("yyyy/MM/dd"), "Vente de l'Article | Client ==> " & clt, Convert.ToDouble(orders.Rows(i).Item(1).ToString.Replace(".", ",")).ToString("N2"))
                          caht += Convert.ToDouble(orders.Rows(i).Item(3).ToString.Replace(".", ","))
                          cattc += Convert.ToDouble(orders.Rows(i).Item(4).ToString.Replace(".", ","))
                          mrg += Convert.ToDouble(orders.Rows(i).Item(5).ToString.Replace(".", ","))
                          qtev += Convert.ToDouble(orders.Rows(i).Item(1).ToString.Replace(".", ","))

                      End Sub)

        Next

        Me.Invoke(Sub()
                      DataGridView5.Sort(DataGridView5.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
                      TextBox1.Text = vaht.ToString("N2")
                      TextBox5.Text = vattc.ToString("N2")
                      TextBox6.Text = caht.ToString("N2")
                      TextBox7.Text = cattc.ToString("N2")
                      TextBox8.Text = mrg.ToString("N2")
                      TextBox9.Text = qtea.ToString("N2")
                      TextBox10.Text = qtev.ToString("N2")
                      For i = 0 To DataGridView5.Rows.Count - 1
                          If DataGridView5.Rows(i).Cells(1).Value = "Démarque de l'Article" Then
                              DataGridView5.Rows(i).DefaultCellStyle.ForeColor = Color.Red
                          End If
                          If DataGridView5.Rows(i).Cells(1).Value = "Réception de l'Article" Then
                              DataGridView5.Rows(i).DefaultCellStyle.ForeColor = Color.Blue
                          End If
                          If DataGridView5.Rows(i).Cells(1).Value = "Inventaire" Then
                              DataGridView5.Rows(i).DefaultCellStyle.ForeColor = Color.Blue
                          End If
                      Next
                  End Sub)





        conn2.Close()

    End Sub
End Class