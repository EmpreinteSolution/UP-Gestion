Imports System.IO
Imports MySql.Data.MySqlClient
Imports System.Linq
Imports System.Net
Imports IWshRuntimeLibrary

Public Class Synchronisation

    Dim adpt, adpt2, adpt3, adpt4, adpt5, adpt6, adpt7 As MySqlDataAdapter

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Public WithEvents download As WebClient
    Dim programNameUpdatedVerison = "\UP-Gestion(V-1).exe"
    Dim dirPath = My.Application.Info.DirectoryPath
    Private Sub Synchronisation_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'BackgroundWorker1.RunWorkerAsync()

    End Sub



    Dim productid As Integer
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork


        conn.Open()

        Me.Invoke(Sub()
                      Label1.Text = "Importation du stock... "
                  End Sub
            )
        ' products



        Dim sql11 As String = "SET GLOBAL FOREIGN_KEY_CHECKS = 0"
        Dim cmd11 As MySqlCommand
        cmd11 = New MySqlCommand(sql11, conn)
        Dim sql10 As String
        Dim cmd10 As MySqlCommand
        sql10 = "TRUNCATE TABLE `article` "
        cmd10 = New MySqlCommand(sql10, conn)
        cmd11.ExecuteNonQuery()
        cmd10.ExecuteNonQuery()

        adpt3 = New MySqlDataAdapter("select * from article", conn2)
        Dim products As New DataTable
        adpt3.Fill(products)
        Dim sumcount As Double = 0
        For i = 0 To products.Rows.Count - 1

            'MsgBox(products.Rows(i).Item(1))

            Dim sql9 As String
            Dim cmd9 As MySqlCommand
            sql9 = "INSERT INTO `article`" +
           "(`Code`, `Article`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `Stock`, `Securite_stock`, `fournisseur`, `idFamille`, `idSFamille`, `unit`, `pv_gros`)" +
                  "VALUES (@v1,@v2,@v3,@v4,@v5,@v6,@v7,@v8,@v9,@v10,@v11,@v12,@v13,@v14,@v15)"
            cmd9 = New MySqlCommand(sql9, conn)
            cmd9.Parameters.Clear()
            cmd9.Parameters.AddWithValue("@v1", products.Rows(i).Item(0))
            cmd9.Parameters.AddWithValue("@v2", products.Rows(i).Item(1))
            cmd9.Parameters.AddWithValue("@v3", products.Rows(i).Item(2))
            cmd9.Parameters.AddWithValue("@v4", products.Rows(i).Item(3))
            cmd9.Parameters.AddWithValue("@v5", products.Rows(i).Item(4))
            cmd9.Parameters.AddWithValue("@v6", products.Rows(i).Item(5))
            cmd9.Parameters.AddWithValue("@v7", products.Rows(i).Item(6))
            cmd9.Parameters.AddWithValue("@v8", products.Rows(i).Item(7))
            cmd9.Parameters.AddWithValue("@v9", products.Rows(i).Item(8))
            cmd9.Parameters.AddWithValue("@v10", products.Rows(i).Item(9))
            cmd9.Parameters.AddWithValue("@v11", products.Rows(i).Item(10))
            cmd9.Parameters.AddWithValue("@v12", products.Rows(i).Item(11))
            cmd9.Parameters.AddWithValue("@v13", products.Rows(i).Item(12))
            cmd9.Parameters.AddWithValue("@v14", products.Rows(i).Item(13))
            cmd9.Parameters.AddWithValue("@v15", products.Rows(i).Item(14))
            cmd9.ExecuteNonQuery()

            sumcount += 1

            Me.Invoke(Sub()
                          Label1.Text = "Chargement du stock... " & sumcount & "/" & products.Rows.Count
                      End Sub
            )

        Next


        Me.Invoke(Sub()
                      Label1.Text = "Importation des sous_familles... "
                  End Sub
            )
        ' products



        Dim sql112 As String = "SET GLOBAL FOREIGN_KEY_CHECKS = 0"
        Dim cmd112 As MySqlCommand
        cmd112 = New MySqlCommand(sql11, conn)
        Dim sql102 As String
        Dim cmd102 As MySqlCommand
        sql102 = "TRUNCATE TABLE `sous_famille` "
        cmd102 = New MySqlCommand(sql102, conn)
        cmd112.ExecuteNonQuery()
        cmd102.ExecuteNonQuery()

        adpt4 = New MySqlDataAdapter("select * from sous_famille", conn2)
        Dim sfamille As New DataTable
        adpt4.Fill(sfamille)
        Dim sfamillecount As Double = 0
        For i = 0 To sfamille.Rows.Count - 1


            Dim sql92 As String
            Dim cmd92 As MySqlCommand
            sql92 = "INSERT INTO `sous_famille`" +
           "(`idSFamille`, `nomSFamille`,`idFamille`)" +
                  "VALUES (@v1,@v2,@v3)"
            cmd92 = New MySqlCommand(sql92, conn)
            cmd92.Parameters.Clear()
            cmd92.Parameters.AddWithValue("@v1", sfamille.Rows(i).Item(0))
            cmd92.Parameters.AddWithValue("@v2", sfamille.Rows(i).Item(2))
            cmd92.Parameters.AddWithValue("@v3", sfamille.Rows(i).Item(1))
            cmd92.ExecuteNonQuery()

            sfamillecount += 1

            Me.Invoke(Sub()
                          Label1.Text = "Chargement des sous_familles... " & sfamillecount & "/" & sfamille.Rows.Count
                      End Sub
            )

        Next


        Me.Invoke(Sub()
                      Label1.Text = "Importation des familles... "
                  End Sub
            )
        ' products



        Dim sql111 As String = "SET GLOBAL FOREIGN_KEY_CHECKS = 0"
        Dim cmd111 As MySqlCommand
        cmd111 = New MySqlCommand(sql111, conn)
        Dim sql101 As String
        Dim cmd101 As MySqlCommand
        sql101 = "TRUNCATE TABLE `famille` "
        cmd101 = New MySqlCommand(sql101, conn)
        cmd111.ExecuteNonQuery()
        cmd101.ExecuteNonQuery()

        adpt4 = New MySqlDataAdapter("select * from famille", conn2)
        Dim famille As New DataTable
        adpt4.Fill(famille)

        Dim famillecount As Double = 0
        For i = 0 To famille.Rows.Count - 1


            Dim sql91 As String
            Dim cmd91 As MySqlCommand
            sql91 = "INSERT INTO `famille`" +
           "(`idFamille`, `nomFamille`)" +
                  "VALUES (@v1,@v2)"
            cmd91 = New MySqlCommand(sql91, conn)
            cmd91.Parameters.Clear()
            cmd91.Parameters.AddWithValue("@v1", famille.Rows(i).Item(0))
            cmd91.Parameters.AddWithValue("@v2", famille.Rows(i).Item(1))

            cmd91.ExecuteNonQuery()


            famillecount += 1

            Me.Invoke(Sub()
                          Label1.Text = "Chargement des familles... " & famillecount & "/" & famille.Rows.Count
                      End Sub
            )

        Next






        ' finalise

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
        conn.Close()

        If finish = True Then
            Me.Invoke(Sub()
                          Me.Close()

                      End Sub
            )

        End If



    End Sub

End Class