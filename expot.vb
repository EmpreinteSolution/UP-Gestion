Imports System.IO
Imports MySql.Data.MySqlClient
Imports System.Linq

Public Class expot

    Dim adpt, adpt2, adpt3, adpt4, adpt5, adpt6, adpt7 As MySqlDataAdapter


    Private Sub expot_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        BackgroundWorker1.RunWorkerAsync()

    End Sub
    Dim productid As Integer
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        conn2.Open()

        Me.Invoke(Sub()
                      Label1.Text = "Actualisation du stock... "
                  End Sub
            )
        ' products



        Dim sql11 As String = "SET SESSION foreign_key_checks=OFF;"
        Dim cmd11 As MySqlCommand
        cmd11 = New MySqlCommand(sql11, conn2)
        Dim sql10 As String
        Dim cmd10 As MySqlCommand


        Dim sql90 As String
        Dim cmd90 As MySqlCommand
        sql90 = "UPDATE `article` SET Stock = 0"
        cmd90 = New MySqlCommand(sql90, conn2)

        cmd90.ExecuteNonQuery()


        Me.Invoke(Sub()
                      Label1.Text = "Réinitialisation du Stock... "
                  End Sub
            )



        adpt3 = New MySqlDataAdapter("SELECT `id` FROM `achat` where avoir = 0", conn)
        Dim achatfirst As New DataTable
        adpt3.Fill(achatfirst)
        For j = 0 To achatfirst.Rows.Count - 1
            adpt3 = New MySqlDataAdapter("SELECT `CodeArticle`, `qte`,`rt`,`gr` FROM `achatdetails` where achat_Id = '" & achatfirst.Rows(j).Item(0) & "'", conn)
            Dim achat As New DataTable
            adpt3.Fill(achat)
            Dim sumcountachat As Double = 0
            For i = 0 To achat.Rows.Count - 1

                Dim sql9 As String
                Dim cmd9 As MySqlCommand
                sql9 = "UPDATE `article` SET Stock = REPLACE(`Stock`,',','.') + (@v1 - @v3) WHERE `Code` = @v2"
                cmd9 = New MySqlCommand(sql9, conn2)
                cmd9.Parameters.Clear()
                cmd9.Parameters.AddWithValue("@v1", Convert.ToDouble(achat.Rows(i).Item(1).ToString.Replace(",", ".")) + Convert.ToDouble(achat.Rows(i).Item(3).ToString.Replace(",", ".")))
                cmd9.Parameters.AddWithValue("@v3", achat.Rows(i).Item(2).ToString.Replace(",", "."))
                cmd9.Parameters.AddWithValue("@v2", achat.Rows(i).Item(0))
                cmd9.ExecuteNonQuery()



            Next
            sumcountachat += 1

            Me.Invoke(Sub()
                          Label1.Text = "Recalcule des achats... " & sumcountachat & "/" & achatfirst.Rows.Count
                      End Sub
            )
        Next

        adpt3 = New MySqlDataAdapter("SELECT `id` FROM `achat` where avoir = 1", conn)
        Dim avoirfirst As New DataTable
        adpt3.Fill(avoirfirst)
        For j = 0 To avoirfirst.Rows.Count - 1
            adpt3 = New MySqlDataAdapter("SELECT `CodeArticle`, `qte` FROM `achatdetails` where achat_Id = '" & avoirfirst.Rows(j).Item(0) & "'", conn)
            Dim avoir As New DataTable
            adpt3.Fill(avoir)
            Dim sumcountavoir As Double = 0
            For i = 0 To avoir.Rows.Count - 1


                Dim sql9 As String
                Dim cmd9 As MySqlCommand
                sql9 = "UPDATE `article` SET Stock = REPLACE(`Stock`,',','.') - @v1 WHERE `Code` = @v2"
                cmd9 = New MySqlCommand(sql9, conn2)
                cmd9.Parameters.Clear()
                cmd9.Parameters.AddWithValue("@v1", avoir.Rows(i).Item(1).ToString.Replace(",", "."))
                cmd9.Parameters.AddWithValue("@v2", avoir.Rows(i).Item(0))
                cmd9.ExecuteNonQuery()



            Next
            sumcountavoir += 1

            Me.Invoke(Sub()
                          Label1.Text = "Recalcule des avoirs... " & sumcountavoir & "/" & avoirfirst.Rows.Count
                      End Sub
            )
        Next



        adpt3 = New MySqlDataAdapter("SELECT `Code`, `Stock` FROM `inventaire`", conn)
        Dim inventaire As New DataTable
        adpt3.Fill(inventaire)
        Dim sumcountinventaire As Double = 0
        For i = 0 To inventaire.Rows.Count - 1


            Dim sql9 As String
            Dim cmd9 As MySqlCommand
            sql9 = "UPDATE `article` SET Stock = REPLACE(`Stock`,',','.') + @v1 WHERE `Code` = @v2"
            cmd9 = New MySqlCommand(sql9, conn2)
            cmd9.Parameters.Clear()
            cmd9.Parameters.AddWithValue("@v1", inventaire.Rows(i).Item(1).ToString.Replace(",", "."))
            cmd9.Parameters.AddWithValue("@v2", inventaire.Rows(i).Item(0))
            cmd9.ExecuteNonQuery()

            sumcountinventaire += 1

            Me.Invoke(Sub()
                          Label1.Text = "Correction d'inventaire... " & sumcountinventaire & "/" & inventaire.Rows.Count
                      End Sub
            )

        Next

        adpt3 = New MySqlDataAdapter("SELECT `ProductID`, SUM(REPLACE(REPLACE(`Quantity`,' ',''),',','.')) FROM `orderdetails` GROUP BY `ProductID`", conn)
        Dim vente As New DataTable
        adpt3.Fill(vente)
        Dim sumcountvente As Double = 0
        For i = 0 To vente.Rows.Count - 1


            Dim sql9 As String
            Dim cmd9 As MySqlCommand
            sql9 = "UPDATE `article` SET Stock = REPLACE(`Stock`,',','.') - @v1 WHERE `Code` = @v2"
            cmd9 = New MySqlCommand(sql9, conn2)
            cmd9.Parameters.Clear()
            cmd9.Parameters.AddWithValue("@v1", vente.Rows(i).Item(1).ToString.Replace(",", "."))
            cmd9.Parameters.AddWithValue("@v2", vente.Rows(i).Item(0))
            cmd9.ExecuteNonQuery()

            sumcountvente += 1

            Me.Invoke(Sub()
                          Label1.Text = "Recalcule des ventes... " & sumcountvente & "/" & vente.Rows.Count
                      End Sub
            )

        Next

        adpt3 = New MySqlDataAdapter("SELECT `code`, SUM(REPLACE(REPLACE(`qte`,' ',''),',','.')) FROM `demarque` GROUP BY `code`", conn)
        Dim demarque As New DataTable
        adpt3.Fill(demarque)
        Dim sumcountdemarque As Double = 0
        For i = 0 To demarque.Rows.Count - 1


            Dim sql9 As String
            Dim cmd9 As MySqlCommand
            sql9 = "UPDATE `article` SET Stock = REPLACE(`Stock`,',','.') - @v1 WHERE `Code` = @v2"
            cmd9 = New MySqlCommand(sql9, conn2)
            cmd9.Parameters.Clear()
            cmd9.Parameters.AddWithValue("@v1", demarque.Rows(i).Item(1).ToString.Replace(",", "."))
            cmd9.Parameters.AddWithValue("@v2", demarque.Rows(i).Item(0))
            cmd9.ExecuteNonQuery()

            sumcountdemarque += 1

            Me.Invoke(Sub()
                          Label1.Text = "Recalcule des démarques... " & sumcountdemarque & "/" & demarque.Rows.Count
                      End Sub
            )

        Next



        'adpt3 = New MySqlDataAdapter("SELECT `Code`,`PA_TTC`,`TM` FROM `article`", conn)
        'Dim article As New DataTable
        'adpt3.Fill(article)
        'Dim sumcountartic As Double = 0
        'For i = 0 To article.Rows.Count - 1
        '    Dim pvht As Double = 0
        '    pvht = Convert.ToDouble(article.Rows(i).Item(1).ToString.Replace(".", ",")) + (Convert.ToDouble(article.Rows(i).Item(1).ToString.Replace(".", ",")) * (Convert.ToDouble(article.Rows(i).Item(2).ToString.Replace(" ", "").Replace(".", ",")) / 100))

        '    Dim sql9 As String
        '    Dim cmd9 As MySqlCommand
        '    sql9 = "UPDATE `article` SET `PV_TTC`= @v1 WHERE `Code` = @v2"
        '    cmd9 = New MySqlCommand(sql9, conn2)
        '    cmd9.Parameters.Clear()
        '    cmd9.Parameters.AddWithValue("@v1", Convert.ToDouble(pvht.ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
        '    cmd9.Parameters.AddWithValue("@v2", article.Rows(i).Item(0))
        '    cmd9.ExecuteNonQuery()

        '    sumcountartic += 1

        '    Me.Invoke(Sub()
        '                  Label1.Text = "Recalcule du Stock TTC... " & sumcountartic & "/" & article.Rows.Count
        '              End Sub
        '    )

        'Next


        'conn2.Open()
        '              Dim sql10 As String
        '              Dim cmd10 As MySqlCommand
        'sql10 = "TRUNCATE TABLE `article`"
        'cmd10 = New MySqlCommand(sql10, conn2)

        '              cmd10.ExecuteNonQuery()
        Me.Invoke(Sub()
                      Label1.Text = "Finalisation..."
                  End Sub
            )


        'conn2.Close()

        Me.Invoke(Sub()
                                    Label1.Text = "Prêt !"
                                    Label1.ForeColor = Color.Green
                                End Sub
            )




        'Get data from local and insert it




        finish = True
        conn2.Close()

        conn2.Open()
        sql10 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
        cmd10 = New MySqlCommand(sql10, conn2)
        cmd10.Parameters.Clear()
        cmd10.Parameters.AddWithValue("@name", user)
        cmd10.Parameters.AddWithValue("@op", "Correction du Stock")
        cmd10.ExecuteNonQuery()
        conn2.Close()

        If finish = True Then
                          Me.Invoke(Sub()
                                        Me.Close()

                                    End Sub
            )

                      End If



                  End Sub

End Class