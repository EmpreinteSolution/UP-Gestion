Imports System.ComponentModel
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Imports WindowsInput

Public Class ajoutachat

    'Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    'Dim conn2 As New MySqlConnection("datasource=localhost; username=root; password=; database=librairie_graph")
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Dim imgName As String = "default.png"

    Dim N As Integer = 0
    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        If DataGridView3.Rows.Count <> 0 Then
            If Panel10.Visible = False Then
                Dim yesornoMsg As New yesorno
                yesornoMsg.Label14.Text = "Voulez-vous enregistrer cette réception avant de quitter ?"
                'yesornoMsg.Panel5.Visible = True
                yesornoMsg.ShowDialog()
                If Not Msgboxresult Is Nothing Then
                    If Msgboxresult = True Then
                        If IconButton9.Visible = True Then
                            IconButton9.PerformClick()
                        End If
                        If IconButton1.Visible = True Then
                            IconButton1.PerformClick()
                        End If
                        Me.Close()
                    Else
                        Me.Close()

                    End If
                End If
            Else
                    Dim yesornoMsg As New yesorno
                yesornoMsg.Label14.Text = "Voulez-vous enregistrer ce bon de commande avant de quitter ?"
                'yesornoMsg.Panel5.Visible = True
                yesornoMsg.ShowDialog()
                If Not Msgboxresult Is Nothing Then
                    If Msgboxresult = True Then

                        If IconButton23.Visible = False Then
                            adpt = New MySqlDataAdapter("select id from bcmnds order by id desc", conn2)
                            Dim table4 As New DataTable
                            adpt.Fill(table4)
                            Dim ref As String
                            If table4.Rows.Count <> 0 Then
                                ref = Convert.ToDouble(table4.Rows(0).Item(0).ToString.Replace("R", "")) + 1
                            Else
                                ref = 1
                            End If

                            IconButton9.PerformClick()
                            conn2.Close()
                            conn2.Open()
                            sql2 = "INSERT INTO bcmnds (id,`name`,`date`) 
                    VALUES ('" & ref & "','" & ComboBox4.Text & "','" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                            cmd2 = New MySqlCommand(sql2, conn2)
                            cmd2.Parameters.Clear()
                            cmd2.ExecuteNonQuery()

                            For i = 0 To DataGridView3.Rows.Count - 1
                                cmd2 = New MySqlCommand("INSERT INTO `bcmnds_details`(`bcmnd_id`, `dasignation`, `Code`, `qte`) 
VALUES (@value15,@value1,@value2,@value3)", conn2)
                                cmd2.Parameters.AddWithValue("@value15", ref)
                                cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(2).Value)
                                cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(1).Value)
                                cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))

                                cmd2.ExecuteNonQuery()
                            Next

                            conn2.Close()
                        End If
                        If IconButton23.Visible = True Then
                            conn2.Close()
                            conn2.Open()

                            cmd2 = New MySqlCommand("DELETE FROM `bcmnds` where `id` = '" & TextBox25.Text & "' ", conn2)
                            cmd2.ExecuteNonQuery()

                            cmd2 = New MySqlCommand("DELETE FROM `bcmnds_details` where `bcmnd_id` = '" & TextBox25.Text & "' ", conn2)
                            cmd2.ExecuteNonQuery()

                            sql2 = "INSERT INTO bcmnds (id,`name`,`date`) 
                    VALUES ('" & TextBox25.Text & "','" & ComboBox4.Text & "','" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                            cmd2 = New MySqlCommand(sql2, conn2)
                            cmd2.Parameters.Clear()
                            cmd2.ExecuteNonQuery()

                            For i = 0 To DataGridView3.Rows.Count - 1
                                cmd2 = New MySqlCommand("INSERT INTO `bcmnds_details`(`bcmnd_id`, `dasignation`, `Code`, `qte`) 
VALUES (@value15,@value1,@value2,@value3)", conn2)
                                cmd2.Parameters.AddWithValue("@value15", TextBox25.Text)
                                cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(2).Value)
                                cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(1).Value)
                                cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))

                                cmd2.ExecuteNonQuery()
                            Next

                            conn2.Close()
                        End If

                        Me.Close()
                    Else
                        Me.Close()
                    End If
                End If
            End If
                Else
            Me.Close()
        End If


    End Sub
    Dim sql2, sql3 As String
    Dim cmd2, cmd3 As MySqlCommand
    Dim comb1 As String
    Dim comb2 As String
    Dim text1 As String
    Dim text2 As String
    Dim text5 As String
    Dim text7 As String
    Dim text8 As String
    Dim text9 As String
    Dim text10 As String
    Dim text11 As String
    Dim oldy As Double

    Dim stck As Double



    Private Sub IconButton1_Click(sender As Object, e As EventArgs)

    End Sub



    Private Sub IconButton1_Click_1(sender As Object, e As EventArgs) Handles IconButton1.Click
        Try
            If TextBox25.Text = "" AndAlso ComboBox4.Text = "" Then
                Dim yesornoMsg As New eror
                yesornoMsg.Label14.Text = "Veuillez saisir le N° du document de reception et choisir le fournisseur !"
                yesornoMsg.Panel5.Visible = True
                yesornoMsg.ShowDialog()
                'If Msgboxresult = True Then

            Else
                    adpt2 = New MySqlDataAdapter("select count(*) from achat where ord = '" & TextBox25.Text & "' and Fournisseur = '" & ComboBox4.Text & "'", conn2)
                Dim tableachat As New DataTable
                adpt2.Fill(tableachat)
                If tableachat.Rows(0).Item(0) = 0 Then
                    conn2.Close()
                    conn2.Open()
                    Dim id As String
                    adpt = New MySqlDataAdapter("select id from achat order by id desc", conn2)
                    Dim table4 As New DataTable
                    adpt.Fill(table4)
                    Dim ref As String
                    If table4.Rows.Count <> 0 Then
                        ref = Convert.ToString(Convert.ToDouble(table4.Rows(0).Item(0).ToString.Replace("R", "")) + 1).PadLeft(4, "0")
                    Else
                        ref = Convert.ToString("0001").PadLeft(4, "0")
                    End If
                    TextBox17.Text = "R" & ref
                    id = TextBox17.Text
                    adpt = New MySqlDataAdapter("select id from achatssauv where id = '" & id & "' and Fournisseur = '" & ComboBox4.Text & "' ", conn2)
                    Dim tablesauv As New DataTable
                    adpt.Fill(tablesauv)
                    If tablesauv.Rows.Count <> 0 Then
                        cmd2 = New MySqlCommand("DELETE FROM `achatssauv` where `id` = '" & id & "' ", conn2)
                        cmd2.ExecuteNonQuery()

                        cmd2 = New MySqlCommand("DELETE FROM `achatdetailssauv` where `achat_Id` = '" & id & "' ", conn2)
                        cmd2.ExecuteNonQuery()
                    Else
                        adpt = New MySqlDataAdapter("select id from achatssauv where id = '" & id & "'", conn2)
                        Dim tablesauv2 As New DataTable
                        adpt.Fill(tablesauv2)
                        If tablesauv2.Rows.Count <> 0 Then
                            id = "R" & (Convert.ToDouble(ref) + 1)
                        End If
                    End If
                    Dim montant As Double = 0
                    Dim paye As Double = 0
                    Dim reste As Double = 0
                    montant = Convert.ToString(Label25.Text).Replace(".", ",").Replace(" ", "")
                    reste = montant

                    sql2 = "INSERT INTO achat (id,montant,Fournisseur,dateAchat,paye,reste,mtn_ht,rms,ord,caissier,avoir,timbre) VALUES ('" & id & "', '" & Convert.ToDouble(Label25.Text).ToString.Replace(" ", "").Replace(".", ",") & "', '" + ComboBox4.Text + "', '" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', '" & paye & "', '" & reste & "', '" + Label18.Text + "', '" + Label23.Text + "','" & TextBox25.Text & "', '" & user & "','1','" & Label53.Text & "' )"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    sql2 = "INSERT INTO achatssauv (id,montant,Fournisseur,dateAchat,paye,reste,mtn_ht,rms,ord,caissier,avoir,timbre) VALUES ('" & id & "', '" & Convert.ToDouble(Label25.Text).ToString.Replace(" ", "").Replace(".", ",") & "', '" + ComboBox4.Text + "', '" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', '" & paye & "', '" & reste & "', '" + Label18.Text + "', '" + Label23.Text + "','" & TextBox25.Text & "', '" & user & "','1','" & Label53.Text & "' )"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()
                    idasync = id
                    DataGridView3.Sort(DataGridView3.Columns(22), System.ComponentModel.ListSortDirection.Ascending)

                    For i = 0 To DataGridView3.Rows.Count - 1
                        cmd2 = New MySqlCommand("INSERT INTO `achatdetails`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`rms`,`rt`,`gr`,`total_ht`,`facture`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value12,@value13,@value14,@value16,@fac)", conn2)
                        cmd2.Parameters.AddWithValue("@value15", id)
                        cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                        cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                        cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(5).Value)
                        cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value12", Convert.ToDouble(DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value16", DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ","))
                        If IsNothing(DataGridView3.Rows(i).Cells(24).Value) Then
                            cmd2.Parameters.AddWithValue("@fac", 0)
                        Else
                            cmd2.Parameters.AddWithValue("@fac", DataGridView3.Rows(i).Cells(24).Value.ToString.Replace(" ", "").Replace(".", ","))

                        End If
                        cmd2.ExecuteNonQuery()

                        cmd2 = New MySqlCommand("INSERT INTO `achatdetailssauv`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`rms`,`rt`,`gr`,`total_ht`,`facture`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value12,@value13,@value14,@value16,@fac)", conn2)
                        cmd2.Parameters.AddWithValue("@value15", id)
                        cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                        cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                        cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(5).Value)
                        cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value12", Convert.ToDouble(DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value16", DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ","))
                        If IsNothing(DataGridView3.Rows(i).Cells(24).Value) Then
                            cmd2.Parameters.AddWithValue("@fac", 0)
                        Else
                            cmd2.Parameters.AddWithValue("@fac", DataGridView3.Rows(i).Cells(24).Value.ToString.Replace(" ", "").Replace(".", ","))

                        End If
                        cmd2.ExecuteNonQuery()
                        If IsNothing(DataGridView3.Rows(i).Cells(24).Value) Then
                            cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  ('" & Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")) & "' - '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(".", ",")) & "' + '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(".", ",")) & "') + REPLACE(REPLACE(`Stock`,',','.'),' ',''),`PV_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",")) & "',`PA_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",")) & "', `PV_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",")) & "',`fournisseur` = '" & ComboBox4.Text & "', `PA_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",")) & "', `ref` = '" & DataGridView3.Rows(i).Cells(21).Value & "' WHERE `Code` = '" + DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") + "' ", conn2)

                        Else
                            cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  ('" & Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")) & "' - '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(".", ",")) & "' + '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(".", ",")) & "') + REPLACE(REPLACE(`Stock`,',','.'),' ','') ,`facture` =  '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(24).Value.ToString.Replace(".", ",")) & "' ,`PV_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",")) & "',`PA_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",")) & "', `PV_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",")) & "',`fournisseur` = '" & ComboBox4.Text & "', `PA_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",")) & "', `ref` = '" & DataGridView3.Rows(i).Cells(21).Value & "' WHERE `Code` = '" + DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") + "' ", conn2)
                        End If
                        cmd2.ExecuteNonQuery()
                        adpt = New MySqlDataAdapter("select * from article where Code = '" & DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") & "' ", conn2)
                        Dim tablelike As New DataTable
                        adpt.Fill(tablelike)
                        If tablelike.Rows.Count() = 0 Then
                            cmd2 = New MySqlCommand("INSERT INTO `article`(`Code`, `Article`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `Stock`, `Securite_stock`, `unit`, `idFamille`, `idSFamille`, `fournisseur`, `pv_gros`, `plu`, `dlc`,`user`,`ref`,`facture`) VALUES (@value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8, @value9, @value10, @value11, @value12, @value13, @value14, @value15, @value16, @value17,@user,@ref,@fac)", conn2)
                            cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ","))
                            cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value.ToString.Replace("'", " "))
                            cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",")))
                            cmd2.Parameters.AddWithValue("@value4", Convert.ToDouble(DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(".", ",")))
                            cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",")))
                            cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",")))
                            cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",")))
                            cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",")))
                            cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")) + Convert.ToDouble(DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(".", ",²    ")))
                            cmd2.Parameters.AddWithValue("@value10", 0)
                            cmd2.Parameters.AddWithValue("@value11", "U")
                            cmd2.Parameters.AddWithValue("@value12", "")
                            cmd2.Parameters.AddWithValue("@value13", "")
                            cmd2.Parameters.AddWithValue("@value14", ComboBox4.Text)
                            cmd2.Parameters.AddWithValue("@value15", 0)
                            cmd2.Parameters.AddWithValue("@value16", 0)
                            cmd2.Parameters.AddWithValue("@value17", 0)
                            cmd2.Parameters.AddWithValue("@user", achats.Label2.Text)
                            If IsNothing(DataGridView3.Rows(i).Cells(24).Value) Then
                                cmd2.Parameters.AddWithValue("@fac", 0)
                            Else
                                cmd2.Parameters.AddWithValue("@fac", DataGridView3.Rows(i).Cells(24).Value.ToString.Replace(" ", "").Replace(".", ","))

                            End If

                            If DataGridView3.Rows(i).Cells(21).Value = "" Then
                                cmd2.Parameters.AddWithValue("@ref", 0)

                            Else

                                cmd2.Parameters.AddWithValue("@ref", DataGridView3.Rows(i).Cells(21).Value.ToString.Replace(".", ","))

                            End If
                            cmd2.ExecuteNonQuery()
                            cmd2 = New MySqlCommand("INSERT INTO `ref_frs`(`product_id`, `fournisseur`, `ref`) VALUES (@value1, @value2, @value3)", conn2)
                            cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ","))
                            cmd2.Parameters.AddWithValue("@value2", ComboBox4.Text)
                            cmd2.Parameters.AddWithValue("@value3", DataGridView3.Rows(i).Cells(21).Value.ToString.Replace(".", ","))
                            cmd2.ExecuteNonQuery()

                        End If
                        adpt = New MySqlDataAdapter("select * from ref_frs where product_id = '" & DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") & "' and fournisseur ='" & ComboBox4.Text & "'", conn2)
                        Dim tablelike2 As New DataTable
                        adpt.Fill(tablelike2)
                        If tablelike2.Rows.Count() = 0 Then
                            cmd2 = New MySqlCommand("INSERT INTO `ref_frs`(`product_id`, `fournisseur`, `ref`) VALUES (@value1, @value2, @value3)", conn2)
                            cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ","))
                            cmd2.Parameters.AddWithValue("@value2", ComboBox4.Text)
                            cmd2.Parameters.AddWithValue("@value3", DataGridView3.Rows(i).Cells(21).Value.ToString.Replace(".", ","))
                            cmd2.ExecuteNonQuery()
                        Else
                            cmd2 = New MySqlCommand("UPDATE `ref_frs` SET `ref` =  '" & DataGridView3.Rows(i).Cells(21).Value & "' WHERE `product_id` = '" + DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") + "' and fournisseur = '" & ComboBox4.Text & "' ", conn2)
                            cmd2.ExecuteNonQuery()
                        End If

                    Next

                    sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    If role = "Caissier" Then
                        cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
                    Else
                        cmd3.Parameters.AddWithValue("@name", achats.Label2.Text)
                    End If
                    cmd3.Parameters.AddWithValue("@op", "Reception d'achat N° " & TextBox17.Text)
                    cmd3.ExecuteNonQuery()


                    Try
                        adpt = New MySqlDataAdapter("select path from infos", conn2)
                        Dim tableinfo As New DataTable
                        adpt.Fill(tableinfo)

                        Dim path As String = tableinfo.Rows(0).Item(0).ToString.Replace("/", "\") & "\orden.dat"

                        ' Create or overwrite the file.
                        Dim fs As FileStream = File.Create(path)
                        adpt = New MySqlDataAdapter("select * from article where plu <> 0 order by id", conn2)
                        Dim table As New DataTable
                        adpt.Fill(table)

                        ' Add text to the file.
                        For i = 0 To table.Rows.Count - 1
                            Dim info As Byte() = New UTF8Encoding(True).GetBytes("260" & table.Rows(i).Item(18) & "EEEEE|" & 1 & " |" & table.Rows(i).Item(18) & "|" & "   |" & "    |" & Convert.ToString(table.Rows(i).Item(7)).Replace(",", "").Replace(".", "") & "|" & "W|" & table.Rows(i).Item(19) & "|" & 0 & "|" & table.Rows(i).Item(1) & vbTab & "|     |" & vbCrLf)
                            fs.Write(info, 0, info.Length)
                        Next
                        fs.Close()
                    Catch
                    End Try
                    BackgroundWorker1.RunWorkerAsync()
                    Dim yesornoMsg As New success
                    yesornoMsg.Label14.Text = "Achat bien effectuée"
                    yesornoMsg.Panel5.Visible = True
                    yesornoMsg.ShowDialog()
                    'If Msgboxresult = True Then

                    conn2.Close()
                    Me.Close()

                Else
                    Dim yesornoMsg As New eror
                    yesornoMsg.Label14.Text = "Bon déjà saisi !"
                    yesornoMsg.Panel5.Visible = True
                    yesornoMsg.ShowDialog()
                    'If Msgboxresult = True Then
                End If

            End If
        Catch ex As MySqlException
            Dim yesornoMsg As New eror
            yesornoMsg.Label14.Text = ex.Message
            yesornoMsg.Panel5.Visible = True
            yesornoMsg.ShowDialog()
            'If Msgboxresult = True Then
        End Try
    End Sub
    Dim num As Integer

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

    Private Sub ajoutachat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        adpt = New MySqlDataAdapter("select stock_fac, balance from parameters", conn2)
        Dim params As New DataTable
        adpt.Fill(params)

        If params.Rows(0).Item(0) = "Desactive" Then
            DataGridView3.Columns(24).Visible = False
            IconButton22.Visible = False
        End If


        adpt = New MySqlDataAdapter("select * from infos ", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Panel1.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel3.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel4.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))


        adpt = New MySqlDataAdapter("select id from achat order by id desc", conn2)
        Dim table4 As New DataTable
        adpt.Fill(table4)
        Dim ref As String
        If table4.Rows.Count <> 0 Then
            ref = Convert.ToString(Convert.ToDouble(table4.Rows(0).Item(0).ToString.Replace("R", "")) + 1).PadLeft(4, "0")
        Else
            ref = Convert.ToString("0001").PadLeft(4, "0")
        End If
        TextBox17.Text = "R" & ref


        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim tablebq As New DataTable
        adpt.Fill(tablebq)

        For i = 0 To tablebq.Rows.Count - 1
            ComboBox7.Items.Add(tablebq.Rows(i).Item(1))
            ComboBox8.Items.Add(tablebq.Rows(i).Item(1))
        Next

        adpt3 = New MySqlDataAdapter("select * from fours order by name asc", conn2)
        Dim table3 As New DataTable
        adpt3.Fill(table3)

        For i = 0 To table3.Rows.Count - 1
            ComboBox4.Items.Add(table3.Rows(i).Item(1))
        Next

        adpt3 = New MySqlDataAdapter("select * from clients order by client asc", conn2)
        Dim table5 As New DataTable
        adpt3.Fill(table5)

        For i = 0 To table5.Rows.Count - 1
            ComboBox1.Items.Add(table5.Rows(i).Item(1))
        Next


    End Sub


    Private Sub TextBox7_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox7.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox7.Text = "" Then

                TextBox7.Text = 0
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            Else

                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            End If
        End If
    End Sub

    Private Sub TextBox8_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox8.Text = "" Then

                TextBox8.Text = 0
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            Else
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            End If
        End If
    End Sub

    Private Sub TextBox5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox5.Text = "" Then

                TextBox5.Text = 0
                TextBox6.Text = Convert.ToDouble(TextBox5.Text.Replace(".", ",") / (1 + (ComboBox6.Text.Replace(".", ",") / 100))).ToString("# ##0.00")
                TextBox8.Text = Convert.ToDouble(((TextBox6.Text.Replace(".", ",") - TextBox3.Text.Replace(".", ",")) / TextBox3.Text.Replace(".", ",")) * 100).ToString("# ##0.00")
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            Else
                TextBox6.Text = Convert.ToDouble(TextBox5.Text.Replace(".", ",") / (1 + (ComboBox6.Text.Replace(".", ",") / 100))).ToString("# ##0.00")
                TextBox8.Text = Convert.ToDouble(((TextBox6.Text.Replace(".", ",") - TextBox3.Text.Replace(".", ",")) / TextBox3.Text.Replace(".", ",")) * 100).ToString("# ##0.00")
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox3.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) / (1 + (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            End If
        End If
    End Sub
    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Text = "" Then

                TextBox3.Text = 0
                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            Else

                TextBox7.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(ComboBox6.Text.Replace(".", ",")) / 100))
                TextBox6.Text = Convert.ToString(TextBox3.Text.Replace(".", ",")) + (Convert.ToString(TextBox3.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
                TextBox5.Text = Convert.ToString(TextBox7.Text.Replace(".", ",")) + (Convert.ToString(TextBox7.Text.Replace(".", ",")) * (Convert.ToString(TextBox8.Text.Replace(".", ",")) / 100))
            End If
        End If

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox6.Text = "" Or TextBox8.Text = "" Or TextBox7.Text = "" Or TextBox6.Text = "" Or TextBox5.Text = "" Or TextBox10.Text = "" Or TextBox9.Text = "" Or TextBox13.Text = "" Then
            MsgBox("Remplis tous")
        Else


            Dim pattc As Double = TextBox7.Text
            Dim qte As Double = TextBox10.Text
            Dim mtnttc As Double = Convert.ToDouble(TextBox7.Text) * Convert.ToDouble(TextBox10.Text)

            DataGridView3.Rows.Add(Nothing, TextBox1.Text, TextBox2.Text.Replace("'", ""), TextBox3.Text.Replace(".", ","), ComboBox6.Text, pattc.ToString("# ##0.00"), TextBox8.Text.Replace(".", ","), TextBox6.Text, TextBox5.Text.Replace(".", ","), qte.ToString("# ##0.00"), TextBox9.Text.Replace(".", ","), ComboBox3.Text, Nothing, Nothing, mtnttc.ToString("# ##0.00"), TextBox13.Text.Replace(".", ","), "0,00", TextBox3.Text, "0,00", "0,00", TextBox18.Text, TextBox19.Text, 0, 0)
            Dim sum As Double = 0
            Dim ht As Double = 0
            Dim remise As Double = 0
            For i = 0 To DataGridView3.Rows.Count - 1
                sum = sum + DataGridView3.Rows(i).Cells(14).Value.ToString.Replace(".", ",")
                remise = remise + (DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",") * DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",") * (DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ",") / 100))
                ht = ht + (DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",") * DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",")) * (1 - (DataGridView3.Rows(i).Cells(16).Value.ToString.Replace(".", ",") / 100))
            Next

            Label25.Text = sum.ToString("# ##0.00")
            Label23.Text = Convert.ToDouble(remise + TextBox26.Text.Replace(".", ",")).ToString("# ##0.00")
            Label47.Text = remise.ToString("# ##0.00")
            Label18.Text = ht.ToString("# ##0.00")
            Label51.Text = Convert.ToDouble(Label25.Text) + Convert.ToDouble(Label53.Text)
            TextBox11.Text = 0
            CheckBox1.Checked = False
            TextBox2.Text = ""
            TextBox1.Text = ""
            TextBox3.Text = 0
            ComboBox6.Text = ""
            TextBox5.Text = 0
            TextBox6.Text = 0
            TextBox7.Text = 0
            TextBox8.Text = 0
            TextBox9.Text = 0
            TextBox10.Text = 0
            TextBox13.Text = 0

            ComboBox6.SelectedItem = ""
        End If


    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        If ComboBox5.Text = "Crédit" Then
            TextBox11.Visible = True
            chqpanel.Visible = False
            tpepanel.Visible = False
        End If

        If ComboBox5.Text = "Chèque" Then
            TextBox11.Visible = False
            chqpanel.Visible = True
            tpepanel.Visible = False
        End If
        If ComboBox5.Text = "Espèce" Then
            TextBox11.Visible = False
            chqpanel.Visible = False
            tpepanel.Visible = False
        End If
        If ComboBox5.Text = "TPE" Then
            TextBox11.Visible = False
            chqpanel.Visible = False
            tpepanel.Visible = True
        End If
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged

    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ComboBox6.Text = "" Then
            TextBox7.Text = Convert.ToDouble(TextBox3.Text.ToString.Replace(".", ","))
            TextBox3.Text = Convert.ToDouble(TextBox7.Text.ToString.Replace(".", ","))
        Else
            TextBox7.Text = Convert.ToDouble(TextBox3.Text.ToString.Replace(".", ",")) + (Convert.ToDouble(TextBox3.Text.ToString.Replace(".", ",")) * ComboBox6.Text / 100)
            TextBox3.Text = Convert.ToDouble(TextBox7.Text.ToString.Replace(".", ",")) / (1 + (ComboBox6.Text / 100))
        End If
    End Sub

    Private Sub DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick


    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Dim execution As Integer

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim cod As Integer = CInt(Math.Ceiling(Rnd() * 999999999)) + 1
        adpt = New MySqlDataAdapter("select * from article where Code = '" + cod.ToString + "'", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        If table.Rows.Count = 0 Then
            TextBox1.Text = cod
            TextBox2.Select()
            TextBox2.Text = ""
            TextBox3.Text = 0
            ComboBox6.Text = ""
            TextBox5.Text = 0
            TextBox6.Text = 0
            TextBox7.Text = 0
            TextBox8.Text = 0
            TextBox9.Text = 0
            TextBox10.Text = 0
            TextBox13.Text = 0

            ComboBox6.SelectedItem = ""
        Else
            IconButton3.PerformClick()
        End If
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        chqpanel.Visible = False
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        chqpanel.Visible = False
    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs)
        tpepanel.Visible = False

    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs)
        tpepanel.Visible = False

    End Sub

    Private Sub IconButton7_Click_1(sender As Object, e As EventArgs) Handles IconButton7.Click
        tpepanel.Visible = False
    End Sub

    Private Sub IconButton6_Click_1(sender As Object, e As EventArgs) Handles IconButton6.Click
        tpepanel.Visible = False
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        adpt = New MySqlDataAdapter("select * from banques where organisme = '" & ComboBox7.Text.ToString & "'", conn2)
        Dim tablebq As New DataTable
        adpt.Fill(tablebq)
        TextBox16.Text = tablebq.Rows(0).Item(2)
        TextBox15.Text = tablebq.Rows(0).Item(3)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then



            adpt = New MySqlDataAdapter("SELECT plu FROM `article` order by `plu` desc", conn2)
            Dim tablebq As New DataTable
            adpt.Fill(tablebq)
            For i = 0 To tablebq.Rows().Count() - 1

                TextBox1.Text = "260" & Convert.ToDouble(tablebq.Rows(0).Item(0) + 1).ToString.PadLeft(4, "0")
                TextBox18.Text = Convert.ToDouble(tablebq.Rows(0).Item(0) + 1).ToString.PadLeft(4, "0")
                TextBox2.Select()
                ComboBox3.Text = "KG"

            Next

            TextBox18.Enabled = True
            TextBox19.Enabled = True

        Else
            TextBox18.Text = ""
            TextBox19.Text = ""
            TextBox1.Text = ""
            TextBox18.Enabled = False
            TextBox19.Enabled = False
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub IconButton8_Click(sender As Object, e As EventArgs) Handles IconButton8.Click
        adpt2 = New MySqlDataAdapter("SELECT * FROM `articlenew`", conn2)
        Dim tableachat As New DataTable
        adpt2.Fill(tableachat)
        For i = 0 To tableachat.Rows.Count() - 1
            If IsDBNull(tableachat.Rows(i).Item(1)) Then
            Else

                'cmd2 = New MySqlCommand("UPDATE `achat` SET `ord` = '" & tableachat.Rows(i).Item(0).ToString.Chars(1) & tableachat.Rows(i).Item(0).ToString.Chars(2) & tableachat.Rows(i).Item(0).ToString.Chars(3) & tableachat.Rows(i).Item(0).ToString.Chars(4) & tableachat.Rows(i).Item(0).ToString.Chars(5) & "' WHERE `id` = '" & tableachat.Rows(i).Item(0).ToString & "' ", conn2)
                adpt = New MySqlDataAdapter("SELECT * FROM `article` where Code = '" & tableachat.Rows(i).Item(0) & "' and Article = '" & tableachat.Rows(i).Item(1).ToString.Replace("'", "") & "' ", conn2)
                Dim tableachat2 As New DataTable
                adpt.Fill(tableachat2)
                Dim sql2, sql3 As String
                Dim cmd2, cmd3 As MySqlCommand
                If tableachat2.Rows.Count() = 0 Then
                    conn2.Close()
                    conn2.Open()
                    cmd2 = New MySqlCommand("INSERT INTO `article`( `Code`, `Article`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `Stock`, `Securite_stock`, `unit` , `idFamille`, `idSFamille`,`fournisseur`,`pv_gros`,`plu`,`dlc`) VALUES (@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17)", conn2)
                    cmd2.Parameters.AddWithValue("@value1", tableachat.Rows(i).Item(0))
                    cmd2.Parameters.AddWithValue("@value2", tableachat.Rows(i).Item(1))
                    cmd2.Parameters.AddWithValue("@value3", tableachat.Rows(i).Item(2))
                    cmd2.Parameters.AddWithValue("@value4", tableachat.Rows(i).Item(3))
                    cmd2.Parameters.AddWithValue("@value5", tableachat.Rows(i).Item(4))
                    cmd2.Parameters.AddWithValue("@value6", tableachat.Rows(i).Item(5))
                    cmd2.Parameters.AddWithValue("@value7", tableachat.Rows(i).Item(6))
                    cmd2.Parameters.AddWithValue("@value8", tableachat.Rows(i).Item(7))
                    cmd2.Parameters.AddWithValue("@value9", tableachat.Rows(i).Item(8))
                    cmd2.Parameters.AddWithValue("@value10", tableachat.Rows(i).Item(9))
                    cmd2.Parameters.AddWithValue("@value11", tableachat.Rows(i).Item(10))
                    cmd2.Parameters.AddWithValue("@value12", tableachat.Rows(i).Item(11))
                    cmd2.Parameters.AddWithValue("@value13", tableachat.Rows(i).Item(12))
                    cmd2.Parameters.AddWithValue("@value15", tableachat.Rows(i).Item(13))
                    cmd2.Parameters.AddWithValue("@value14", tableachat.Rows(i).Item(14))
                    cmd2.Parameters.AddWithValue("@value16", tableachat.Rows(i).Item(15))
                    cmd2.Parameters.AddWithValue("@value17", tableachat.Rows(i).Item(16))
                    cmd2.ExecuteNonQuery()
                    conn2.Close()

                End If
            End If


            'sql2 = "delete from code_supp where code_supp = '" + tableachat.Rows(i).Item(0) + "' "
            'cmd2 = New MySqlCommand(sql2, conn2)

            'cmd2.ExecuteNonQuery()
            'cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` = '" & tableachat.Rows(i).Item(1) & "' WHERE `Code` = '" & tableachat.Rows(i).Item(0).ToString & "' ", conn2)



        Next
        MsgBox("termine")

        'sql2 = "INSERT INTO `achat`(`id`, `montant`, `Fournisseur`, `mtn_ht`) VALUES ('" & TextBox17.Text.ToString & "','" & Convert.ToDouble(tableachat.Rows(0).Item(0).ToString) & "','" & ComboBox4.Text.ToString & "','" & Convert.ToDouble(tableachat.Rows(0).Item(1).ToString) & "')"
        'cmd2 = New MySqlCommand(sql2, conn2)
        'cmd2.ExecuteNonQuery()
    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub IconButton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        If Panel6.Visible = False Then
            Panel6.Visible = True
            TextBox2.Select()
        Else
            Panel6.Visible = False
        End If

    End Sub

    Private Sub TextBox22_TextChanged(sender As Object, e As EventArgs) Handles TextBox22.TextChanged
        If TextBox22.Text = "" Then
            adpt = New MySqlDataAdapter("select name from fours order by name asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                ComboBox4.Items.Clear()
                ComboBox4.Items.Add("aucun")
                For i = 0 To table.Rows.Count - 1
                    ComboBox4.Items.Add(table.Rows(i).Item(0))
                Next
            End If
            ComboBox4.Text = Label31.Text
        Else

            Dim inputText As String = TextBox22.Text

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
                RemoveHandler TextBox22.TextChanged, AddressOf TextBox22_TextChanged

                ' Mettez à jour le texte dans le TextBox
                TextBox22.Text = modifiedText

                ' Replacez le curseur à la position correcte après la modification
                TextBox22.SelectionStart = TextBox22.Text.Length

                ' Réactivez le gestionnaire d'événements TextChanged
                AddHandler TextBox22.TextChanged, AddressOf TextBox22_TextChanged
            End If

            ComboBox4.Items.Clear()
            adpt = New MySqlDataAdapter("select name from fours WHERE name LIKE '%" + TextBox22.Text.Replace("'", " ") + "%' order by name asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                For i = 0 To table.Rows.Count - 1
                    ComboBox4.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox4.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub textbox28_textChanged(sender As Object, e As EventArgs) Handles TextBox28.TextChanged
        If TextBox28.Text = "" Then
            adpt = New MySqlDataAdapter("select client from clients order by client asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                ComboBox1.Items.Clear()
                ComboBox1.Items.Add("aucun")
                For i = 0 To table.Rows.Count - 1
                    ComboBox1.Items.Add(table.Rows(i).Item(0))
                Next
            End If
            ComboBox1.Text = Label31.Text
        Else

            Dim inputText As String = TextBox28.Text

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
                RemoveHandler TextBox28.TextChanged, AddressOf textbox28_textChanged

                ' Mettez à jour le texte dans le TextBox
                TextBox28.Text = modifiedText

                ' Replacez le curseur à la position correcte après la modification
                TextBox28.SelectionStart = TextBox28.Text.Length

                ' Réactivez le gestionnaire d'événements TextChanged
                AddHandler TextBox28.TextChanged, AddressOf textbox28_textChanged
            End If

            ComboBox1.Items.Clear()
            adpt = New MySqlDataAdapter("select client from clients WHERE client LIKE '%" + TextBox28.Text.Replace("'", " ") + "%' order by client asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                For i = 0 To table.Rows.Count - 1
                    ComboBox1.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox1.SelectedIndex = 0
            End If
        End If
    End Sub


    Private Sub IconButton16_Click(sender As Object, e As EventArgs)
        Dim table0 As DataTable
        adpt2 = New MySqlDataAdapter("SELECT * FROM `article` WHERE `Code`= '" + Label49.Text + "' ", conn2)
        table0 = New DataTable
        adpt2.Fill(table0)

        If table0.Rows.Count <> 0 Then
            Try
                conn2.Close()
                conn2.Open()

                cmd2 = New MySqlCommand("UPDATE `article` SET `Code` = @value1, `Article` = @value2, `PA_HT` = @value3, `TVA` = @value4, `PA_TTC` = @value5, `TM` = @value6, `PV_HT` = @value7, `PV_TTC` = @value8, `Stock` = @value9, `Securite_stock` = @value10, `unit` = @value11, `idFamille` = @value12, `idSFamille` = @value13, `fournisseur` = @value14, `pv_gros` = @value15, `plu` = @value16, `dlc` = @value17 WHERE `Code` = @value18", conn2)
                cmd2.Parameters.AddWithValue("@value2", TextBox2.Text.ToString.Replace("'", " "))
                cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(TextBox3.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value4", Convert.ToDouble(ComboBox6.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(TextBox7.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(TextBox8.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(TextBox6.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(TextBox5.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(TextBox10.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(TextBox9.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value11", ComboBox3.Text)

                cmd2.Parameters.AddWithValue("@value14", ComboBox4.Text)
                cmd2.Parameters.AddWithValue("@value15", Convert.ToDouble(TextBox13.Text.ToString.Replace(".", ",")).ToString("#0.00"))
                cmd2.Parameters.AddWithValue("@value16", Convert.ToDouble(TextBox18.Text.ToString.Replace(".", ",")))
                cmd2.Parameters.AddWithValue("@value17", Convert.ToDouble(TextBox19.Text.ToString.Replace(".", ",")))
                cmd2.Parameters.AddWithValue("@value18", Label49.Text)
                cmd2.Parameters.AddWithValue("@value1", TextBox1.Text.ToString.Replace(".", ","))
                cmd2.ExecuteNonQuery()

                conn2.Close()

                MsgBox("Article modifié !")
            Catch
                MsgBox("Veuillez remplir les champs !")
            End Try

        Else
            MsgBox("Cette article est non disponible !")
        End If
    End Sub

    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        codbar.Show()
        codbar.PicBarCode.BackgroundImage = Code128(TextBox1.Text, "A")
        codbar.TextBox1.Text = TextBox2.Text
        codbar.TextBox3.Text = TextBox1.Text
    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub


    Private Sub DataGridView3_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellEndEdit
        If e.RowIndex >= 0 Then

            Dim row As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
            If row.Cells(e.ColumnIndex).Value = "" Then
                row.Cells(e.ColumnIndex).Value = 0.00
            End If
            If e.ColumnIndex = 4 Then
                row.Cells(6).Value = Convert.ToDouble((row.Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") + (row.Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * (row.Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",") / 100)))).ToString("# ##0.00")
                row.Cells(4).Value = Convert.ToDouble(row.Cells(4).Value.ToString.Replace(".", ",")).ToString("N2")
                row.Cells(18).Value = Convert.ToDouble(row.Cells(4).Value.ToString.Replace(".", ",")).ToString("N2")

            End If
            If e.ColumnIndex = 6 Then
                row.Cells(4).Value = Convert.ToDouble(((row.Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") / (1 + (row.Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))))).ToString("# ##0.00")
                row.Cells(6).Value = Convert.ToDouble(row.Cells(6).Value.ToString.Replace(".", ",")).ToString("N2")
                row.Cells(18).Value = Convert.ToDouble(row.Cells(4).Value.ToString.Replace(".", ",")).ToString("N2")
            End If
            If e.ColumnIndex = 17 Then
                row.Cells(4).Value = Convert.ToDouble(row.Cells(18).Value.ToString.Replace(" ", "").Replace(".", ",") - (row.Cells(18).Value.ToString.Replace(" ", "").Replace(".", ",") * (row.Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))).ToString("# ##0.00")
                row.Cells(6).Value = Convert.ToDouble((row.Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") + (row.Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * (row.Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",") / 100)))).ToString("# ##0.00")
                row.Cells(17).Value = Convert.ToDouble(row.Cells(17).Value.ToString.Replace(".", ",")).ToString("N2")
            End If
            row.Cells(7).Value = Convert.ToDouble(row.Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * ((row.Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - row.Cells(19).Value.ToString.Replace(" ", "").Replace(".", ",")))).ToString("# ##0.00")
            row.Cells(8).Value = Convert.ToDouble(row.Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",") * ((row.Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - row.Cells(19).Value.ToString.Replace(" ", "").Replace(".", ",")))).ToString("# ##0.00")


            row.Cells(3).Value = Convert.ToDouble(row.Cells(3).Value.ToString.Replace(".", ",")).ToString("N2")

            If e.ColumnIndex = 11 Then
                row.Cells(9).Value = Convert.ToDouble(((row.Cells(11).Value.ToString.Replace(" ", "").Replace(".", ",") - row.Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",")) / row.Cells(6).Value.ToString.Replace(" ", "").Replace(".", ",")) * 100).ToString("# ##0.00")
                row.Cells(10).Value = Convert.ToDouble(row.Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * (1 + (row.Cells(9).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))).ToString("# ##0.00")
                row.Cells(11).Value = Convert.ToDouble(row.Cells(11).Value.ToString.Replace(".", ",")).ToString("N2")
            End If

            Dim sum As Double = 0
            Dim tva As Double = 0
            Dim ht As Double = 0
            Dim remise As Double = 0
            For i = 0 To DataGridView3.Rows.Count - 1
                sum = sum + DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                remise = remise + (DataGridView3.Rows(i).Cells(18).Value.ToString.Replace(" ", "").Replace(".", ",") * (DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") - DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ",")) * (DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                ht = ht + DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",")
                tva = tva + (DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",") - DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ","))
            Next

            Label23.Text = Convert.ToDouble(remise + TextBox26.Text.Replace(".", ",")).ToString("# ##0.00")
            Label47.Text = remise.ToString("# ##0.00")
            Label18.Text = ht.ToString("# ##0.00")
            Label43.Text = tva.ToString("# ##0.00")
            If Label47.Text.Replace(" ", "") <> 0 Then
                Label25.Text = (Convert.ToDouble(Label18.Text.Replace(" ", "")) + Convert.ToDouble(Label43.Text.Replace(" ", ""))).ToString("# ##0.00")
            Else
                Label25.Text = Convert.ToDouble(Convert.ToDouble(Label18.Text.Replace(" ", "")) - Convert.ToDouble(Label23.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDouble(Label43.Text.Replace(" ", ""))).ToString("# ##0.00")
            End If
            Label51.Text = Convert.ToDouble(Label25.Text.Replace(" ", "")) + Convert.ToDouble(Label53.Text.Replace(" ", ""))
        End If
        For i = 0 To DataGridView3.Rows.Count - 1
            Dim valeurColonne5 As Double = Convert.ToDouble(DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ",")) ' Changer 4 par l'indice de votre colonne

            ' Vérifiez si la valeur est négative
            If valeurColonne5 > 0 Then
                ' Si la valeur est négative, colorez le fond de la ligne en rouge
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.Salmon
            End If
            If valeurColonne5 = 0 Then
                ' Sinon, rétablissez la couleur de fond par défaut
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.White
            End If
        Next

    End Sub

    Private Sub DataGridView3_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.RowEnter
        ' Obtenez la valeur de la colonne 19 pour la ligne en cours
        For i = 0 To DataGridView3.Rows.Count - 1
            Dim valeurColonne5 As Double = Convert.ToDouble(DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ",")) ' Changer 4 par l'indice de votre colonne

            ' Vérifiez si la valeur est négative
            If valeurColonne5 > 0 Then
                ' Si la valeur est négative, colorez le fond de la ligne en rouge
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.Salmon
            End If
            If valeurColonne5 = 0 Then
                ' Sinon, rétablissez la couleur de fond par défaut
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.White
            End If
        Next
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

        DataGridView1.AllowUserToAddRows = False
        If e.KeyCode = Keys.Enter Then


            adpt = New MySqlDataAdapter("select prod_code from code_supp where code_supp = '" & TextBox1.Text & "' ", conn2)
            Dim table2 As New DataTable
            adpt.Fill(table2)
            If table2.Rows.Count() = 0 Then
                adpt = New MySqlDataAdapter("select * from article where Code = '" & TextBox1.Text & "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    For i = 0 To table.Rows.Count - 1
                        Label49.Text = table.Rows(i).Item(0)
                        TextBox1.Text = table.Rows(i).Item(0)
                        TextBox2.Text = table.Rows(i).Item(1)
                        TextBox3.Text = table.Rows(i).Item(2).ToString.Replace(".", ",")
                        Dim index As Integer = ComboBox6.FindStringExact(table.Rows(i).Item(3).ToString.Replace(",00", ""))
                        If index <> -1 Then
                            ComboBox6.SelectedIndex = index
                        End If
                        TextBox5.Text = table.Rows(i).Item(7).ToString.Replace(".", ",")
                        TextBox6.Text = table.Rows(i).Item(6).ToString.Replace(".", ",")
                        TextBox7.Text = table.Rows(i).Item(4).ToString.Replace(".", ",")
                        TextBox8.Text = table.Rows(i).Item(5).ToString.Replace(".", ",")
                        TextBox9.Text = table.Rows(i).Item(9)
                        TextBox10.Text = table.Rows(i).Item(8)
                        TextBox18.Text = table.Rows(i).Item(18)
                        TextBox19.Text = table.Rows(i).Item(19)

                        ComboBox3.SelectedItem = table.Rows(i).Item(13)
                        ComboBox4.SelectedItem = table.Rows(i).Item(10)

                    Next
                Else
                    TextBox2.Text = ""
                    TextBox3.Text = 0
                    TextBox5.Text = 0
                    TextBox6.Text = 0
                    TextBox7.Text = 0
                    TextBox8.Text = 0
                    TextBox9.Text = 0
                    TextBox10.Text = 0
                    TextBox13.Text = 0
                    TextBox18.Text = ""
                    TextBox19.Text = ""

                    ComboBox4.SelectedItem = ""
                    TextBox2.Select()


                End If
            Else
                adpt = New MySqlDataAdapter("select * from article where Code = '" + table2.Rows(0).Item(0) + "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    For i = 0 To table.Rows.Count - 1

                        TextBox1.Text = table.Rows(i).Item(0)
                        TextBox2.Text = table.Rows(i).Item(1)
                        TextBox3.Text = table.Rows(i).Item(2).ToString.Replace(".", ",")
                        ComboBox6.Text = table.Rows(i).Item(3).ToString.Replace(",00", "")
                        TextBox5.Text = table.Rows(i).Item(7).ToString.Replace(".", ",")
                        TextBox6.Text = table.Rows(i).Item(6).ToString.Replace(".", ",")
                        TextBox7.Text = table.Rows(i).Item(4).ToString.Replace(".", ",")
                        TextBox8.Text = table.Rows(i).Item(5).ToString.Replace(".", ",")
                        TextBox9.Text = table.Rows(i).Item(9)
                        TextBox10.Text = table.Rows(i).Item(8)
                        TextBox18.Text = table.Rows(i).Item(18)
                        TextBox19.Text = table.Rows(i).Item(19)

                        ComboBox3.SelectedItem = table.Rows(i).Item(13)
                        ComboBox4.SelectedItem = table.Rows(i).Item(10)

                        charg = True
                    Next
                Else
                    TextBox2.Text = ""
                    TextBox3.Text = 0
                    TextBox5.Text = 0
                    TextBox6.Text = 0
                    TextBox7.Text = 0
                    TextBox8.Text = 0
                    TextBox9.Text = 0
                    TextBox10.Text = 0
                    TextBox13.Text = 0
                    TextBox18.Text = ""
                    TextBox19.Text = ""

                    ComboBox6.SelectedItem = ""
                    TextBox2.Select()


                    charg = True
                End If

            End If

        End If
    End Sub

    Private Sub IconButton9_Click_1(sender As Object, e As EventArgs) Handles IconButton9.Click
        conn2.Close()
        conn2.Open()
        If TextBox25.Text = "" AndAlso ComboBox4.Text = "" Then
            Dim yesornoMsg2 As New eror
            yesornoMsg2.Label14.Text = "Veuillez saisir le N° du document de reception et choisir le fournisseur !"
            yesornoMsg2.Panel5.Visible = True
            yesornoMsg2.ShowDialog()

        Else

            Dim id As String
            id = TextBox17.Text

            sql2 = "UPDATE achat SET montant = '" & Label51.Text & "',reste = '" & Label51.Text & "', dateAchat = '" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', Fournisseur = '" & ComboBox4.Text & "', mtn_ht = '" & Label18.Text & "', rms = '" & Label23.Text & "', ord = '" & TextBox25.Text & "', timbre = '" & Label53.Text & "' WHERE id = '" & id & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()

            sql2 = "UPDATE achatssauv SET montant = '" & Label51.Text & "',reste = '" & Label51.Text & "', dateAchat = '" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', Fournisseur = '" & ComboBox4.Text & "', mtn_ht = '" & Label18.Text & "', rms = '" & Label23.Text & "', ord = '" & TextBox25.Text & "', timbre = '" & Label53.Text & "' WHERE id = '" & id & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()

            adpt = New MySqlDataAdapter("select * from achatdetails where achat_Id = '" & id & "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            For i = 0 To table.Rows.Count - 1
                cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  REPLACE(`Stock`,',','.') - '" & Convert.ToDouble(table.Rows(i).Item(9).ToString.Replace(".", ",")) & "' + '" & Convert.ToDouble(table.Rows(i).Item(13).ToString.Replace(".", ",")) & "' - '" & Convert.ToDouble(table.Rows(i).Item(14).ToString.Replace(".", ",")) & "',`facture` =  REPLACE(`facture`,',','.') - '" & Convert.ToDouble(table.Rows(i).Item(16).ToString.Replace(".", ",")) & "'  WHERE `Code` = '" + table.Rows(i).Item(1).ToString.Replace(".", ",") + "' ", conn2)
                cmd2.ExecuteNonQuery()
            Next

            cmd2 = New MySqlCommand("DELETE FROM `achatdetails` where `achat_Id` = '" & id & "' ", conn2)
            cmd2.ExecuteNonQuery()

            cmd2 = New MySqlCommand("DELETE FROM `achatdetailssauv` where `achat_Id` = '" & id & "' ", conn2)
            cmd2.ExecuteNonQuery()
            DataGridView3.Sort(DataGridView3.Columns(22), System.ComponentModel.ListSortDirection.Ascending)

            For i = 0 To DataGridView3.Rows.Count - 1
                cmd2 = New MySqlCommand("INSERT INTO `achatdetails`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`rms`,`rt`,`gr`,`total_ht`,`facture`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value12,@value13,@value14,@value16,@fac)", conn2)
                cmd2.Parameters.AddWithValue("@value15", id)
                cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(5).Value)
                cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value12", Convert.ToDouble(DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(" ", "").Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value16", DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ","))
                If IsNothing(DataGridView3.Rows(i).Cells(24).Value) Then
                    cmd2.Parameters.AddWithValue("@fac", 0)
                Else
                    cmd2.Parameters.AddWithValue("@fac", DataGridView3.Rows(i).Cells(24).Value.ToString.Replace(" ", "").Replace(".", ","))

                End If
                cmd2.ExecuteNonQuery()

                cmd2 = New MySqlCommand("INSERT INTO `achatdetailssauv`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`rms`,`rt`,`gr`,`total_ht`,`facture`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value12,@value13,@value14,@value16,@fac)", conn2)
                cmd2.Parameters.AddWithValue("@value15", id)
                cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(5).Value)
                cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value12", Convert.ToDouble(DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(" ", "").Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value16", DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ","))
                If IsNothing(DataGridView3.Rows(i).Cells(24).Value) Then
                    cmd2.Parameters.AddWithValue("@fac", 0)
                Else
                    cmd2.Parameters.AddWithValue("@fac", DataGridView3.Rows(i).Cells(24).Value.ToString.Replace(" ", "").Replace(".", ","))

                End If
                cmd2.ExecuteNonQuery()

                If IsNothing(DataGridView3.Rows(i).Cells(24).Value) Then
                    cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  REPLACE(`Stock`,',','.') + '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")) & "' - '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(".", ",")) & "' + '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(".", ",")) & "',`PV_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",")) & "',`PA_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",")) & "', `PV_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",")) & "',`fournisseur` = '" & ComboBox4.Text & "', `PA_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",")) & "',ref='" & DataGridView3.Rows(i).Cells(21).Value & "' WHERE `Code` = '" + DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") + "' ", conn2)

                Else
                    cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  REPLACE(`Stock`,',','.') + '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")) & "' - '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(".", ",")) & "' + '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(".", ",")) & "',`facture` =  REPLACE(`facture`,',','.') + '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(24).Value.ToString.Replace(".", ",")) & "',`PV_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",")) & "',`PA_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",")) & "', `PV_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",")) & "',`fournisseur` = '" & ComboBox4.Text & "', `PA_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",")) & "',ref='" & DataGridView3.Rows(i).Cells(21).Value & "' WHERE `Code` = '" + DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") + "' ", conn2)

                End If
                cmd2.ExecuteNonQuery()
                adpt = New MySqlDataAdapter("select * from article where Code = '" & DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") & "' ", conn2)
                Dim tablelike As New DataTable
                adpt.Fill(tablelike)
                If tablelike.Rows.Count() = 0 Then
                    cmd2 = New MySqlCommand("INSERT INTO `article`(`Code`, `Article`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `Stock`, `Securite_stock`, `unit`, `idFamille`, `idSFamille`, `fournisseur`, `pv_gros`, `plu`, `dlc`,`user`,`ref`,`facture`) VALUES (@value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8, @value9, @value10, @value11, @value12, @value13, @value14, @value15, @value16, @value17,@user,@ref,@fac)", conn2)
                    cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ","))
                    cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value.ToString.Replace("'", " "))
                    cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",")))
                    cmd2.Parameters.AddWithValue("@value4", Convert.ToDouble(DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(".", ",")))
                    cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",")))
                    cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",")))
                    cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",")))
                    cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",")))
                    cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")) + Convert.ToDouble(DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(".", ",")))
                    cmd2.Parameters.AddWithValue("@value10", 0)
                    cmd2.Parameters.AddWithValue("@value11", "U")
                    cmd2.Parameters.AddWithValue("@value12", "")
                    cmd2.Parameters.AddWithValue("@value13", "")
                    cmd2.Parameters.AddWithValue("@value14", ComboBox4.Text)
                    cmd2.Parameters.AddWithValue("@value15", 0)
                    cmd2.Parameters.AddWithValue("@value16", 0)
                    cmd2.Parameters.AddWithValue("@value17", 0)
                    cmd2.Parameters.AddWithValue("@user", achats.Label2.Text)
                    If IsNothing(DataGridView3.Rows(i).Cells(24).Value) Then
                        cmd2.Parameters.AddWithValue("@fac", 0)
                    Else
                        cmd2.Parameters.AddWithValue("@fac", DataGridView3.Rows(i).Cells(24).Value.ToString.Replace(" ", "").Replace(".", ","))

                    End If
                    If DataGridView3.Rows(i).Cells(21).Value = "" Then
                        cmd2.Parameters.AddWithValue("@ref", 0)

                    Else

                        cmd2.Parameters.AddWithValue("@ref", DataGridView3.Rows(i).Cells(21).Value.ToString.Replace(".", ","))

                    End If
                    cmd2.ExecuteNonQuery()

                    cmd2 = New MySqlCommand("INSERT INTO `ref_frs`(`product_id`, `fournisseur`, `ref`) VALUES (@value1, @value2, @value3)", conn2)
                    cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ","))
                    cmd2.Parameters.AddWithValue("@value2", ComboBox4.Text)
                    cmd2.Parameters.AddWithValue("@value3", DataGridView3.Rows(i).Cells(21).Value.ToString.Replace(".", ","))
                    cmd2.ExecuteNonQuery()

                End If
                adpt = New MySqlDataAdapter("select * from ref_frs where product_id = '" & DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") & "' and fournisseur ='" & ComboBox4.Text & "'", conn2)
                Dim tablelike2 As New DataTable
                adpt.Fill(tablelike2)
                If tablelike2.Rows.Count() = 0 Then
                    cmd2 = New MySqlCommand("INSERT INTO `ref_frs`(`product_id`, `fournisseur`, `ref`) VALUES (@value1, @value2, @value3)", conn2)
                    cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ","))
                    cmd2.Parameters.AddWithValue("@value2", ComboBox4.Text)
                    cmd2.Parameters.AddWithValue("@value3", DataGridView3.Rows(i).Cells(21).Value.ToString.Replace(".", ","))
                    cmd2.ExecuteNonQuery()
                End If

            Next
            sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            If role = "Caissier" Then
                cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
            Else
                cmd3.Parameters.AddWithValue("@name", achats.Label2.Text)
            End If
            cmd3.Parameters.AddWithValue("@op", "Modification de la Réception N° " & id)
            cmd3.ExecuteNonQuery()
            Dim yesornoMsg2 As New success
            yesornoMsg2.Label14.Text = "Modification bien effectuée"
            yesornoMsg2.Panel5.Visible = True
            yesornoMsg2.ShowDialog()
        End If
        conn2.Close()
        Me.Close()


    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click
        Panel6.Visible = True
        TextBox24.Select()
        If Label16.Text = "Devis" Then

        Else

            adpt = New MySqlDataAdapter("select product_id, ref from ref_frs where fournisseur = '" & ComboBox4.Text & "' ", conn2)
            Dim tablelike2 As New DataTable
            adpt.Fill(tablelike2)
            DataGridView1.Rows.Clear()

            If tablelike2.Rows.Count() <> 0 Then
                For i = 0 To tablelike2.Rows.Count - 1

                    adpt = New MySqlDataAdapter("select Code, Article, Stock,ref from article where Code = '" & tablelike2.Rows(i).Item(0) & "' ", conn2)
                    Dim tablelike As New DataTable
                    adpt.Fill(tablelike)
                    If tablelike.Rows.Count() <> 0 Then
                        Dim rowsToAdd As New List(Of DataGridViewRow)

                        ' Créer une nouvelle instance de DataGridViewRow à chaque itération
                        Dim row As New DataGridViewRow()

                        ' Créer les instances de cellules
                        Dim first As New DataGridViewTextBoxCell()
                        Dim second As New DataGridViewTextBoxCell()
                        Dim tree As New DataGridViewTextBoxCell()
                        Dim qte As New DataGridViewTextBoxCell()
                        Dim ord As New DataGridViewTextBoxCell()
                        Dim ref As New DataGridViewTextBoxCell()

                        ' Affecter les valeurs des cellules
                        first.Value = tablelike.Rows(0).Item(0)
                        second.Value = tablelike.Rows(0).Item(1)
                        tree.Value = tablelike.Rows(0).Item(2)
                        qte.Value = ""
                        ord.Value = ""
                        ref.Value = tablelike2.Rows(i).Item(1)

                        ' Ajouter les cellules à la ligne
                        row.Cells.Add(ord)
                        row.Cells.Add(ref)
                        row.Cells.Add(first)
                        row.Cells.Add(second)
                        row.Cells.Add(qte)
                        row.Cells.Add(tree)

                        ' Ajouter la ligne à la liste des lignes à ajouter
                        rowsToAdd.Add(row)


                        ' Ajouter toutes les lignes en une seule opération pour éviter l'erreur
                        DataGridView1.Rows.AddRange(rowsToAdd.ToArray())

                    End If

                Next
            End If
        End If
        If IconButton9.Visible = True Then
            ordersec = DataGridView3.Rows.Count()


        End If


    End Sub
    Dim ordersec As Integer = 0
    Private Sub IconButton11_Click(sender As Object, e As EventArgs) Handles IconButton11.Click

        Panel6.Visible = False
        For j = 0 To DataGridView2.Rows.Count - 1
            Dim selectedRow As DataGridViewRow = DataGridView2.Rows(j)
            Dim code As String = selectedRow.Cells(2).Value.ToString()
            ' Vérifier si l'article est déjà présent dans la DataGridView3
            If selectedRow.Cells(4).Value.ToString <> "" AndAlso Convert.ToDecimal(selectedRow.Cells("Qte2").Value.ToString.Replace(".", ",").Replace(" ", "")) > 0 Then
                adpt = New MySqlDataAdapter("select `Code`, `Article`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC` from article where Code = '" & code & "' ", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count() <> 0 Then
                    Dim qty As Double = Convert.ToDecimal(selectedRow.Cells("Qte2").Value.ToString.Replace(".", ",").Replace(" ", ""))
                    Dim patotal As Double = Convert.ToDouble(Convert.ToDecimal(selectedRow.Cells("Qte2").Value.ToString.Replace(".", ",").Replace(" ", "")) * table.Rows(0).Item(4).ToString.Replace(".", ",")).ToString.Replace(".", ",")
                    Dim paHTtotal As Double = Convert.ToDouble(Convert.ToDecimal(selectedRow.Cells("Qte2").Value.ToString.Replace(".", ",").Replace(" ", "")) * table.Rows(0).Item(2).ToString.Replace(".", ",")).ToString.Replace(".", ",")
                    Dim refrow As String
                    If IsDBNull(selectedRow.Cells(1).Value.ToString) Then
                        refrow = "-"
                    Else
                        refrow = selectedRow.Cells(1).Value.ToString
                    End If
                    DataGridView3.Rows.Add(Convert.ToDouble(selectedRow.Cells(0).Value).ToString("N0"), table.Rows(0).Item(0), table.Rows(0).Item(1).ToString.Replace("'", ""), Convert.ToDouble(qty).ToString("N2"), Convert.ToDouble(table.Rows(0).Item(2).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(0).Item(3).ToString.Replace(".", ",")).ToString("N0"), Convert.ToDouble(table.Rows(0).Item(4).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(paHTtotal).ToString("N2"), Convert.ToDouble(patotal).ToString("N2"), Convert.ToDouble(table.Rows(0).Item(5).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(0).Item(6).ToString.Replace(".", ",")).ToString("N2"), Convert.ToDouble(table.Rows(0).Item(7).ToString.Replace(".", ",")).ToString("N2"), Nothing, Nothing, Nothing, Nothing, Nothing, "0,00", Convert.ToDouble(table.Rows(0).Item(2).ToString.Replace(".", ",")), "0,00", "0,00", refrow, Convert.ToDouble(selectedRow.Cells(0).Value), Nothing, 0)
                    Dim sum As Double = 0
                    Dim ht As Double = 0
                    Dim tva As Double = 0
                    Dim remise As Double = 0
                    For i = 0 To DataGridView3.Rows.Count - 1
                        sum = sum + DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                        remise = remise + (DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") * (DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                        ht = ht + (DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")) * (1 - (DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                        tva = tva + (DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",") * (DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                    Next

                    Label25.Text = sum.ToString("# ##0.00")
                    Label23.Text = Convert.ToDouble(remise + TextBox26.Text.Replace(".", ",")).ToString("# ##0.00")
                    Label47.Text = remise.ToString("# ##0.00")
                    Label18.Text = ht.ToString("# ##0.00")
                    Label43.Text = tva.ToString("# ##0.00")
                    Label51.Text = Convert.ToDouble(Label25.Text) + Convert.ToDouble(Label53.Text)
                    TextBox11.Text = 0
                Else
                    Dim refrow As String
                    If selectedRow.Cells(1).Value = "" Then
                        refrow = "-"
                    Else
                        refrow = selectedRow.Cells(1).Value.ToString
                    End If
                    DataGridView3.Rows.Add(Convert.ToDouble(selectedRow.Cells(0).Value).ToString("N0"), selectedRow.Cells(2).Value.ToString(), selectedRow.Cells(3).Value.ToString(), selectedRow.Cells(4).Value.ToString(), 0.00, 0.ToString("N0"), 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, Nothing, Nothing, Nothing, Nothing, Nothing, "0,00", 0.00, "0,00", "0,00", refrow, Convert.ToDouble(selectedRow.Cells(0).Value), Convert.ToDouble(selectedRow.Cells(6).Value), 0, 0)

                    TextBox11.Text = 0

                End If


            End If

        Next
        For Each row As DataGridViewRow In DataGridView3.Rows
            Dim cellValue As String = row.Cells(22).Value.ToString()
            Dim numericValue As Double

            If Double.TryParse(cellValue, numericValue) Then
                row.Cells(22).Value = numericValue
            End If
        Next

        ' Trier la colonne "Ord2" maintenant que les valeurs sont numériques
        DataGridView3.Sort(DataGridView3.Columns(22), System.ComponentModel.ListSortDirection.Ascending)
        DataGridView3.Select()
        DataGridView2.Rows.Clear()
        DataGridView1.Rows.Clear()
        TextBox27.Text = ""
        TextBox24.Text = ""
        TextBox23.Text = ""
        confir = True

    End Sub

    Private selectedRowIndex As Integer = -1 ' Variable pour stocker l'index de la ligne sélectionnée

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        adpt = New MySqlDataAdapter("select Code, Article, Stock,ref from article where fournisseur = '" & ComboBox4.Text & "' ", conn2)
        Dim tablelike As New DataTable
        adpt.Fill(tablelike)
        DataGridView1.Rows.Clear()

        If tablelike.Rows.Count() <> 0 Then
            Dim rowsToAdd As New List(Of DataGridViewRow)

            For i = 0 To tablelike.Rows.Count - 1
                ' Créer une nouvelle instance de DataGridViewRow à chaque itération
                Dim row As New DataGridViewRow()

                ' Créer les instances de cellules
                Dim first As New DataGridViewTextBoxCell()
                Dim second As New DataGridViewTextBoxCell()
                Dim tree As New DataGridViewTextBoxCell()
                Dim qte As New DataGridViewTextBoxCell()
                Dim ord As New DataGridViewTextBoxCell()
                Dim ref As New DataGridViewTextBoxCell()

                ' Affecter les valeurs des cellules
                first.Value = tablelike.Rows(i).Item(0)
                second.Value = tablelike.Rows(i).Item(1)
                tree.Value = tablelike.Rows(i).Item(2)
                qte.Value = ""
                ord.Value = ""
                ref.Value = tablelike.Rows(i).Item(3)

                ' Ajouter les cellules à la ligne
                row.Cells.Add(ord)
                row.Cells.Add(ref)
                row.Cells.Add(first)
                row.Cells.Add(second)
                row.Cells.Add(qte)
                row.Cells.Add(tree)

                ' Ajouter la ligne à la liste des lignes à ajouter
                rowsToAdd.Add(row)
            Next

            ' Ajouter toutes les lignes en une seule opération pour éviter l'erreur
            DataGridView1.Rows.Clear()
            DataGridView1.Rows.AddRange(rowsToAdd.ToArray())
        End If
    End Sub
    Dim idasync As String
    Private Sub IconButton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        Dim yesornoMsg2 As New yesorno
        yesornoMsg2.Label14.Text = "Voulez-vous vraiment Transformer ce bon en Avoir ?"
        'yesornoMsg2.Panel5.Visible = True
        yesornoMsg2.ShowDialog()
        If Msgboxresult = True Then
            Try
                If IconButton1.Visible = False Then
                    conn2.Close()
                    conn2.Open()
                    Dim id As String
                    id = TextBox17.Text

                    sql2 = "UPDATE achat SET montant = '" & Label51.Text.Replace(" ", "") * (-1) & "',reste = '" & Label51.Text.Replace(" ", "") * (-1) & "', dateAchat = '" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', Fournisseur = '" & ComboBox4.Text & "', mtn_ht = '" & Label18.Text & "', rms = '" & Label23.Text & "', ord = '" & TextBox25.Text & "' WHERE id = '" & id & "'"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    sql2 = "UPDATE achatssauv SET montant = '" & Label51.Text.Replace(" ", "") * (-1) & "',reste = '" & Label51.Text.Replace(" ", "") * (-1) & "', dateAchat = '" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', Fournisseur = '" & ComboBox4.Text & "', mtn_ht = '" & Label18.Text & "', rms = '" & Label23.Text & "', ord = '" & TextBox25.Text & "' WHERE id = '" & id & "'"

                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    adpt = New MySqlDataAdapter("select * from achatdetails where achat_Id = '" & id & "' ", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    For i = 0 To table.Rows.Count - 1
                        cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  REPLACE(`Stock`,',','.') - '" & Convert.ToDouble(table.Rows(i).Item(9).ToString.Replace(".", ",")) & "'  WHERE `Code` = '" + table.Rows(i).Item(1).ToString.Replace(".", ",") + "' ", conn2)
                        cmd2.ExecuteNonQuery()
                    Next

                    cmd2 = New MySqlCommand("DELETE FROM `achatdetails` where `achat_Id` = '" & id & "' ", conn2)
                    cmd2.ExecuteNonQuery()
                    cmd2 = New MySqlCommand("DELETE FROM `achatdetailssauv` where `achat_Id` = '" & id & "' ", conn2)
                    cmd2.ExecuteNonQuery()
                    DataGridView3.Sort(DataGridView3.Columns(22), System.ComponentModel.ListSortDirection.Ascending)

                    For i = 0 To DataGridView3.Rows.Count - 1
                        cmd2 = New MySqlCommand("INSERT INTO `achatdetails`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`rms`,`rt`,`gr`,`total_ht`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value12,@value13,@value14,@value16)", conn2)
                        cmd2.Parameters.AddWithValue("@value15", id)
                        cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                        cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                        cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(5).Value)
                        cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value12", Convert.ToDouble(DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value16", DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.ExecuteNonQuery()

                        cmd2 = New MySqlCommand("INSERT INTO `achatdetailssauv`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`rms`,`rt`,`gr`,`total_ht`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value12,@value13,@value14,@value16)", conn2)
                        cmd2.Parameters.AddWithValue("@value15", id)
                        cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                        cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                        cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(5).Value)
                        cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value12", Convert.ToDouble(DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value16", DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.ExecuteNonQuery()

                        cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  REPLACE(`Stock`,',','.') - '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")) & "' + '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(".", ",")) & "',`PV_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",")) & "',`PA_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",")) & "', `PV_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",")) & "',`fournisseur` = '" & ComboBox4.Text & "', `PA_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",")) & "' WHERE `Code` = '" + DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") + "' ", conn2)
                        cmd2.ExecuteNonQuery()
                    Next

                    sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    If role = "Caissier" Then
                        cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
                    Else
                        cmd3.Parameters.AddWithValue("@name", achats.Label2.Text)
                    End If
                    cmd3.Parameters.AddWithValue("@op", "Modification de l'Avoir N° " & id)
                    cmd3.ExecuteNonQuery()


                    conn2.Close()
                Else

                    conn2.Close()
                    conn2.Open()
                    Dim id As String
                    adpt = New MySqlDataAdapter("select id from achat order by id desc", conn2)
                    Dim table4 As New DataTable
                    adpt.Fill(table4)
                    Dim ref As String
                    If table4.Rows.Count <> 0 Then
                        ref = Convert.ToString(Convert.ToDouble(table4.Rows(0).Item(0).ToString.Replace("R", "")) + 1).PadLeft(4, "0")
                    Else
                        ref = Convert.ToString("0001").PadLeft(4, "0")
                    End If
                    TextBox17.Text = "R" & ref
                    id = TextBox17.Text
                    adpt = New MySqlDataAdapter("select id from achatssauv where id = '" & id & "' and Fournisseur = '" & ComboBox4.Text & "' ", conn2)
                    Dim tablesauv As New DataTable
                    adpt.Fill(tablesauv)
                    If tablesauv.Rows.Count <> 0 Then
                        cmd2 = New MySqlCommand("DELETE FROM `achatssauv` where `id` = '" & id & "' ", conn2)
                        cmd2.ExecuteNonQuery()

                        cmd2 = New MySqlCommand("DELETE FROM `achatdetailssauv` where `achat_Id` = '" & id & "' ", conn2)
                        cmd2.ExecuteNonQuery()
                    Else
                        adpt = New MySqlDataAdapter("select id from achatssauv where id = '" & id & "'", conn2)
                        Dim tablesauv2 As New DataTable
                        adpt.Fill(tablesauv2)
                        If tablesauv2.Rows.Count <> 0 Then
                            id = "R" & (Convert.ToDouble(ref) + 1)
                        End If
                    End If

                    Dim montant As Double = 0
                    Dim paye As Double = 0
                    Dim reste As Double = 0
                    montant = (Convert.ToDouble(Label25.Text) * (-1)).ToString.Replace(" ", "").Replace(".", ",")
                    paye = 0
                    reste = montant - paye

                    sql2 = "INSERT INTO achat (id,montant,Fournisseur,dateAchat,paye,reste,mtn_ht,rms,ord,caissier,avoir,timbre) VALUES ('" & id & "', '" & (Convert.ToDouble(Label25.Text) * (-1)).ToString.Replace(" ", "").Replace(".", ",") & "', '" + ComboBox4.Text + "', '" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', '" & paye & "', '" & reste & "', '" + Label18.Text + "', '" + Label23.Text + "','" & TextBox25.Text & "', '" & user & "','1','" & Label53.Text & "' )"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()



                    sql2 = "INSERT INTO achatssauv (id,montant,Fournisseur,dateAchat,paye,reste,mtn_ht,rms,ord,caissier,avoir,timbre) VALUES ('" & id & "', '" & (Convert.ToDouble(Label25.Text) * (-1)).ToString.Replace(" ", "").Replace(".", ",") & "', '" + ComboBox4.Text + "', '" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', '" & paye & "', '" & reste & "', '" + Label18.Text + "', '" + Label23.Text + "','" & TextBox25.Text & "', '" & user & "','1','" & Label53.Text & "' )"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()


                    DataGridView3.Sort(DataGridView3.Columns(22), System.ComponentModel.ListSortDirection.Ascending)
                    idasync = id
                    For i = 0 To DataGridView3.Rows.Count - 1
                        cmd2 = New MySqlCommand("INSERT INTO `achatdetails`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`rms`,`rt`,`gr`,`total_ht`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value12,@value13,@value14,@value16)", conn2)
                        cmd2.Parameters.AddWithValue("@value15", id)
                        cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                        cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                        cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(5).Value)
                        cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value12", Convert.ToDouble(DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value16", DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.ExecuteNonQuery()

                        cmd2 = New MySqlCommand("INSERT INTO `achatdetailssauv`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`rms`,`rt`,`gr`,`total_ht`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value12,@value13,@value14,@value16)", conn2)
                        cmd2.Parameters.AddWithValue("@value15", id)
                        cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                        cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                        cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(5).Value)
                        cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value12", Convert.ToDouble(DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                        cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.Parameters.AddWithValue("@value16", DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ","))
                        cmd2.ExecuteNonQuery()

                        cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  REPLACE(`Stock`,',','.') - '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")) & "' WHERE `Code` = '" + DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") + "' ", conn2)
                        cmd2.ExecuteNonQuery()

                    Next

                    sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    If role = "Caissier" Then
                        cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
                    Else
                        cmd3.Parameters.AddWithValue("@name", achats.Label2.Text)
                    End If
                    cmd3.Parameters.AddWithValue("@op", "Avoir N° " & TextBox17.Text)
                    cmd3.ExecuteNonQuery()
                End If

                Try
                    adpt = New MySqlDataAdapter("select path from infos", conn2)
                    Dim tableinfo As New DataTable
                    adpt.Fill(tableinfo)

                    Dim path As String = tableinfo.Rows(0).Item(0).ToString.Replace("/", "\") & "\orden.dat"

                    ' Create or overwrite the file.
                    Dim fs As FileStream = File.Create(path)
                    adpt = New MySqlDataAdapter("select * from article where plu <> 0 order by id", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)

                    ' Add text to the file.
                    For i = 0 To table.Rows.Count - 1
                        Dim info As Byte() = New UTF8Encoding(True).GetBytes("260" & table.Rows(i).Item(18) & "EEEEE|" & 1 & " |" & table.Rows(i).Item(18) & "|" & "   |" & "    |" & Convert.ToString(table.Rows(i).Item(7)).Replace(",", "").Replace(".", "") & "|" & "W|" & table.Rows(i).Item(19) & "|" & 0 & "|" & table.Rows(i).Item(1) & vbTab & "|     |" & vbCrLf)
                        fs.Write(info, 0, info.Length)
                    Next
                    fs.Close()
                Catch
                End Try
                Dim yesornoMsg As New success
                yesornoMsg.Label14.Text = "Avoir bien effectuée"
                yesornoMsg2.Panel5.Visible = True
                yesornoMsg.ShowDialog()
                'If Msgboxresult = True Then

                conn2.Close()
                Me.Close()

                BackgroundWorker1.RunWorkerAsync()
            Catch ex As MySqlException
                Dim yesornoMsg As New eror
                yesornoMsg.Label14.Text = ex.Message
                yesornoMsg2.Panel5.Visible = True
                yesornoMsg.ShowDialog()
                'If Msgboxresult = True Then
            End Try
        End If
    End Sub

    Private Sub TextBox23_TextChanged(sender As Object, e As EventArgs) Handles TextBox23.TextChanged
        Dim inputText As String = TextBox23.Text

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
            RemoveHandler TextBox23.TextChanged, AddressOf TextBox23_TextChanged

            ' Mettez à jour le texte dans le TextBox
            TextBox23.Text = modifiedText

            ' Replacez le curseur à la position correcte après la modification
            TextBox23.SelectionStart = TextBox23.Text.Length

            ' Réactivez le gestionnaire d'événements TextChanged
            AddHandler TextBox23.TextChanged, AddressOf TextBox23_TextChanged
        End If
    End Sub

    Private Sub TextBox24_TextChanged(sender As Object, e As EventArgs) Handles TextBox24.TextChanged

    End Sub

    Private Sub Panel6_Paint(sender As Object, e As PaintEventArgs) Handles Panel6.Paint

    End Sub

    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs) Handles Panel7.Paint

    End Sub

    Private Sub TextBox24_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox24.KeyDown
        DataGridView1.AllowUserToAddRows = False

        If e.KeyCode = Keys.Enter Then
            If TextBox24.Text <> "" Then

                Dim code As String = TextBox24.Text

                If Not String.IsNullOrEmpty(code) Then

                    adpt = New MySqlDataAdapter("select prod_code from code_supp where code_supp = '" & TextBox24.Text & "' ", conn2)
                    Dim table2 As New DataTable
                    adpt.Fill(table2)
                    If table2.Rows.Count() = 0 Then
                        adpt = New MySqlDataAdapter("select Code, Article, Stock,ref from article where Code = '" & TextBox24.Text & "' ", conn2)
                        Dim tablelike As New DataTable
                        adpt.Fill(tablelike)
                        If tablelike.Rows.Count() <> 0 Then

                            Dim rowsToAdd As New List(Of DataGridViewRow)
                            Dim row As New DataGridViewRow()

                            ' Créer les instances de cellules
                            Dim first As New DataGridViewTextBoxCell()
                            Dim second As New DataGridViewTextBoxCell()
                            Dim tree As New DataGridViewTextBoxCell()
                            Dim qte As New DataGridViewTextBoxCell()
                            Dim ord As New DataGridViewTextBoxCell()
                            Dim ref As New DataGridViewTextBoxCell()

                            ' Affecter les valeurs des cellules
                            first.Value = tablelike.Rows(0).Item(0)
                            second.Value = tablelike.Rows(0).Item(1)
                            tree.Value = tablelike.Rows(0).Item(2)
                            qte.Value = ""
                            ord.Value = ""
                            adpt = New MySqlDataAdapter("select ref from ref_frs where product_id = '" & tablelike.Rows(0).Item(0) & "' and fournisseur = '" & ComboBox4.Text & "'", conn2)
                            Dim tablelike2 As New DataTable
                            adpt.Fill(tablelike2)
                            If tablelike2.Rows.Count <> 0 Then
                                ref.Value = tablelike2.Rows(0).Item(0)
                            Else
                                ref.Value = "-"
                            End If

                            ' Ajouter les cellules à la ligne
                            row.Cells.Add(ord)
                            row.Cells.Add(ref)
                            row.Cells.Add(first)
                            row.Cells.Add(second)
                            row.Cells.Add(qte)
                            row.Cells.Add(tree)


                            rowsToAdd.Add(row)
                            DataGridView1.Rows.Clear()
                            DataGridView1.Rows.AddRange(rowsToAdd.ToArray())
                            DataGridView1.ClearSelection()
                            DataGridView1.Rows(DataGridView1.Rows.Count - 1).Selected = True
                            DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.Rows.Count - 1
                            If ref.Value = "-" Then
                                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(1)
                            Else
                                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells("Qte")
                            End If
                            DataGridView1.BeginEdit(True)
                        Else

                        End If
                    Else
                        adpt = New MySqlDataAdapter("select Code, Article, Stock,ref from article where Code = '" + table2.Rows(0).Item(0) + "' ", conn2)
                        Dim tablelike As New DataTable
                        adpt.Fill(tablelike)
                        If tablelike.Rows.Count() <> 0 Then

                            Dim rowsToAdd As New List(Of DataGridViewRow)
                            For i = 0 To tablelike.Rows.Count - 1
                                Dim row As New DataGridViewRow()

                                Dim first As New DataGridViewTextBoxCell()
                                Dim second As New DataGridViewTextBoxCell()
                                Dim tree As New DataGridViewTextBoxCell()
                                Dim qte As New DataGridViewTextBoxCell()
                                Dim ord As New DataGridViewTextBoxCell()
                                Dim ref As New DataGridViewTextBoxCell()

                                ' Affecter les valeurs des cellules
                                first.Value = tablelike.Rows(i).Item(0)
                                second.Value = tablelike.Rows(i).Item(1)
                                tree.Value = tablelike.Rows(i).Item(2)
                                qte.Value = ""
                                ord.Value = ""
                                adpt = New MySqlDataAdapter("select ref from ref_frs where product_id = '" & tablelike.Rows(i).Item(0) & "' and fournisseur = '" & ComboBox4.Text & "'", conn2)
                                Dim tablelike2 As New DataTable
                                adpt.Fill(tablelike2)
                                If tablelike2.Rows.Count <> 0 Then
                                    ref.Value = tablelike2.Rows(0).Item(0)
                                Else
                                    ref.Value = "-"
                                End If


                                ' Ajouter les cellules à la ligne
                                row.Cells.Add(ord)
                                row.Cells.Add(ref)
                                row.Cells.Add(first)
                                row.Cells.Add(second)
                                row.Cells.Add(qte)
                                row.Cells.Add(tree)


                                rowsToAdd.Add(row)
                                DataGridView1.Rows.Clear()

                                DataGridView1.Rows.AddRange(rowsToAdd.ToArray())
                            Next

                            DataGridView1.ClearSelection()
                            DataGridView1.Rows(DataGridView1.Rows.Count - 1).Selected = True
                            DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.Rows.Count - 1
                            If DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(1).Value = "-" Then
                                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(1)
                            Else
                                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells("Qte")
                            End If
                            DataGridView1.BeginEdit(True)
                        End If

                    End If

                End If

            End If
            TextBox24.Text = ""
        End If
    End Sub

    Private Sub textbox27_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox27.KeyDown
        DataGridView1.AllowUserToAddRows = False

        If e.KeyCode = Keys.Enter Then
            If TextBox27.Text <> "" Then

                Dim code As String = TextBox27.Text

                If Not String.IsNullOrEmpty(code) Then



                    adpt = New MySqlDataAdapter("select product_id, ref from ref_frs where ref = '" & TextBox27.Text & "' and fournisseur = '" & ComboBox4.Text & "'", conn2)
                    Dim tablelike2 As New DataTable
                    adpt.Fill(tablelike2)
                    If tablelike2.Rows.Count() <> 0 Then
                        adpt = New MySqlDataAdapter("select Code, Article, Stock,ref from article where code = '" & tablelike2.Rows(0).Item(0) & "' ", conn2)
                        Dim tablelike As New DataTable
                        adpt.Fill(tablelike)
                        Dim rowsToAdd As New List(Of DataGridViewRow)
                        Dim row As New DataGridViewRow()

                        ' Créer les instances de cellules
                        Dim first As New DataGridViewTextBoxCell()
                        Dim second As New DataGridViewTextBoxCell()
                        Dim tree As New DataGridViewTextBoxCell()
                        Dim qte As New DataGridViewTextBoxCell()
                        Dim ord As New DataGridViewTextBoxCell()
                        Dim ref As New DataGridViewTextBoxCell()

                        ' Affecter les valeurs des cellules
                        first.Value = tablelike.Rows(0).Item(0)
                        second.Value = tablelike.Rows(0).Item(1)
                        tree.Value = tablelike.Rows(0).Item(2)
                        qte.Value = ""
                        ord.Value = ""
                        ref.Value = TextBox27.Text

                        ' Ajouter les cellules à la ligne
                        row.Cells.Add(ord)
                        row.Cells.Add(ref)
                        row.Cells.Add(first)
                        row.Cells.Add(second)
                        row.Cells.Add(qte)
                        row.Cells.Add(tree)


                        rowsToAdd.Add(row)
                        DataGridView1.Rows.Clear()
                        DataGridView1.Rows.AddRange(rowsToAdd.ToArray())
                        DataGridView1.ClearSelection()
                        DataGridView1.Rows(DataGridView1.Rows.Count - 1).Selected = True
                        DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.Rows.Count - 1
                        DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells("Qte")
                        DataGridView1.BeginEdit(True)
                    Else

                    End If


                End If

            End If
            TextBox27.Text = ""
        End If
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ' Vérifiez si l'événement est déclenché dans une cellule valide de la colonne Qte

    End Sub

    'Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
    '    ' Vérifier si un clic a été effectué sur une cellule valide (non sur l'en-tête)
    '    If e.RowIndex >= 0 Then

    '        Panel8.Visible = True
    '            TextBox27.Select()

    '    End If
    'End Sub

    Private Sub TextBox23_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox23.KeyDown
        DataGridView1.AllowUserToAddRows = False

        If e.KeyCode = Keys.Enter Then
            If TextBox23.Text <> "" Then

                adpt = New MySqlDataAdapter("select Code, Article, Stock, ref from article where Article like '%" & TextBox23.Text & "%' order by Article asc", conn2)
                Dim tablelike As New DataTable
                adpt.Fill(tablelike)

                Dim rowsToAdd As New List(Of DataGridViewRow)
                If tablelike.Rows.Count <> 0 Then

                    For i = 0 To tablelike.Rows.Count - 1
                        Dim row As New DataGridViewRow()

                        Dim first As New DataGridViewTextBoxCell()
                        Dim second As New DataGridViewTextBoxCell()
                        Dim tree As New DataGridViewTextBoxCell()
                        Dim qte As New DataGridViewTextBoxCell()
                        Dim ord As New DataGridViewTextBoxCell()
                        Dim ref As New DataGridViewTextBoxCell()

                        ' Affecter les valeurs des cellules
                        first.Value = tablelike.Rows(i).Item(0)
                        second.Value = tablelike.Rows(i).Item(1)
                        tree.Value = tablelike.Rows(i).Item(2)
                        qte.Value = ""
                        ord.Value = ""
                        adpt = New MySqlDataAdapter("select ref from ref_frs where product_id = '" & tablelike.Rows(i).Item(0) & "' and fournisseur = '" & ComboBox4.Text & "'", conn2)
                        Dim tablelike2 As New DataTable
                        adpt.Fill(tablelike2)
                        If tablelike2.Rows.Count <> 0 Then
                            ref.Value = tablelike2.Rows(0).Item(0)
                        Else
                            ref.Value = "-"
                        End If

                        ' Ajouter les cellules à la ligne
                        row.Cells.Add(ord)
                        row.Cells.Add(ref)
                        row.Cells.Add(first)
                        row.Cells.Add(second)
                        row.Cells.Add(qte)
                        row.Cells.Add(tree)


                        rowsToAdd.Add(row)
                    Next
                    DataGridView1.Rows.Clear()
                    DataGridView1.Rows.AddRange(rowsToAdd.ToArray())
                    DataGridView1.Select()
                    If DataGridView1.Rows.Count <> 0 Then
                        DataGridView1.BeginEdit(True)

                    End If
                Else
                    TextBox23.Text = ""
                    TextBox23.Select()
                End If
            Else
                TextBox23.Text = ""
                TextBox23.Select()

            End If


        End If
    End Sub
    Dim TVA As Double
    Private Sub IconButton15_Click(sender As Object, e As EventArgs) Handles IconButton15.Click
        If TextBox26.Text <> "" And IsNumeric(TextBox26.Text.ToString.Replace(".", ",")) Then
            Label23.Text = Convert.ToDouble(TextBox26.Text.Replace(".", ",") + Convert.ToDouble(Label47.Text)).ToString("N2")

            Label25.Text = Convert.ToDouble(Convert.ToDouble(Label18.Text) + Convert.ToDouble(Label43.Text) - Convert.ToDouble(Label23.Text) + Label47.Text).ToString("N2")
            Label51.Text = Convert.ToDouble(Label25.Text) + Convert.ToDouble(Label53.Text)

            TextBox26.Text = 0
        Else
            Label23.Text = Label47.Text
            Label25.Text = Convert.ToDouble(Convert.ToDouble(Label18.Text) + Convert.ToDouble(Label43.Text) - Convert.ToDouble(Label23.Text) + Label47.Text).ToString("N2")
            Label51.Text = Convert.ToDouble(Label25.Text) + Convert.ToDouble(Label53.Text)

            Dim yesornoMsg As New eror
            yesornoMsg.Label14.Text = "Entrer un nombre valide !"
            yesornoMsg.Panel5.Visible = True
            yesornoMsg.ShowDialog()
            'If Msgboxresult = True Then
            TextBox26.Text = 0
            TextBox26.Select()
        End If
        TextBox26.Visible = False
        IconButton15.Visible = False
    End Sub

    Private productOrderDictionary As New Dictionary(Of String, Integer)()
    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        ' Assurez-vous que la cellule modifiée est dans la colonne "Qte"
        If e.ColumnIndex = DataGridView1.Columns("Qte").Index AndAlso e.RowIndex >= 0 AndAlso DataGridView1.Rows(e.RowIndex).Cells("Qte").Value.ToString.Replace(".", ",") <> "" Then
            Dim productCode As String = DataGridView1.Rows(e.RowIndex).Cells("Code").Value.ToString()
            Dim quantity As Double = Convert.ToDouble(DataGridView1.Rows(e.RowIndex).Cells("Qte").Value.ToString.Replace(".", ","))

            Dim found As Boolean = False ' Variable pour vérifier si le produit a été trouvé dans DataGridView2

            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                If DataGridView2.Rows(i).Cells(1).Value = productCode Then
                    DataGridView2.Rows(i).Cells(3).Value = quantity
                    found = True
                    Exit For ' Sortez de la boucle dès que le produit est trouvé
                End If
            Next

            If Not found Then
                ordersec += 1
                DataGridView1.Rows(e.RowIndex).Cells("Ord").Value = Convert.ToDouble(ordersec.ToString("N0")).ToString("N0")
                DataGridView2.Rows.Add(Convert.ToDouble(ordersec.ToString("N0")).ToString("N0"), DataGridView1.Rows(e.RowIndex).Cells(1).Value, productCode, DataGridView1.Rows(e.RowIndex).Cells(3).Value, quantity, DataGridView1.Rows(e.RowIndex).Cells(5).Value, DataGridView1.Rows(e.RowIndex).Cells(6).Value)
            End If
        End If
    End Sub

    Dim confir As Boolean = False
    Private Sub DataGridView3_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellDoubleClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 2 Then
                Dim yesornoMsg As New yesorno
                yesornoMsg.Label14.Text = "Voulez-vous vraiment supprimer ce produit de la liste ?"
                'yesornoMsg.Panel5.Visible = True
                yesornoMsg.ShowDialog()
                If Msgboxresult = True Then
                    Dim row As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
                    If productOrderDictionary.ContainsKey(row.Cells(1).Value) Then
                        productOrderDictionary.Remove(row.Cells(1).Value)
                        productOrderDictionary.Add(row.Cells(1).Value & (productOrderDictionary.Count + 1), productOrderDictionary.Count + 1)
                    End If
                    DataGridView3.Rows.Remove(row)
                    Dim sum As Double = 0
                    Dim ht As Double = 0
                    Dim tva As Double = 0
                    Dim remise As Double = 0
                    For i = 0 To DataGridView3.Rows.Count - 1
                        sum = sum + DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "").Replace(".", ",")
                        remise = remise + (DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",") * (DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                        ht = ht + (DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(" ", "").Replace(".", ",") * DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")) * (1 - (DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                        tva = tva + (DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ",") * (DataGridView3.Rows(i).Cells(5).Value.ToString.Replace(" ", "").Replace(".", ",") / 100))
                    Next

                    Label25.Text = sum.ToString("# ##0.00")
                    Label23.Text = Convert.ToDouble(remise + TextBox26.Text.Replace(".", ",")).ToString("# ##0.00")
                    Label47.Text = remise.ToString("# ##0.00")
                    Label18.Text = ht.ToString("# ##0.00")
                    Label43.Text = tva.ToString("# ##0.00")
                    Label51.Text = Convert.ToDouble(Label25.Text) + Convert.ToDouble(Label53.Text)
                    TextBox11.Text = sum
                End If
            End If

        End If
    End Sub

    Private Sub ajoutachat_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            DataGridView1.EndEdit()
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox24.Select()
        End If
    End Sub



    Private Sub Label23_Click(sender As Object, e As EventArgs) Handles Label23.Click

    End Sub

    Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter
        DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen

    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        If e.RowIndex > 0 Then
            DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen

        End If

    End Sub

    Private Sub DataGridView1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen

    End Sub

    Private Sub DataGridView1_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellLeave
        DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White

    End Sub

    Private Sub TextBox25_TextChanged(sender As Object, e As EventArgs) Handles TextBox25.TextChanged
        If TextBox25.Text = "" AndAlso IconButton13.Visible = True Then
            IconButton13.Visible = False
        End If

        If TextBox25.Text <> "" AndAlso IconButton13.Visible = False Then
            IconButton13.Visible = True
        End If
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub DataGridView3_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellEnter
        DataGridView3.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen

    End Sub

    Private Sub DataGridView3_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView3.CellMouseClick
        If e.RowIndex > 0 Then
            DataGridView3.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen

        End If

    End Sub

    Private Sub DataGridView3_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView3.CellBeginEdit
        DataGridView3.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen

    End Sub

    Private Sub DataGridView3_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellLeave
        DataGridView3.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White

    End Sub
    Private Sub Label23_DoubleClick(sender As Object, e As EventArgs) Handles Label23.DoubleClick
        TextBox26.Visible = True
        IconButton15.Visible = True
    End Sub

    Private Sub IconButton16_Click_1(sender As Object, e As EventArgs) Handles IconButton16.Click
        DataGridView1.Rows.Add()

        ' Sélectionner la nouvelle ligne
        Dim nouvelleLigne As DataGridViewRow = DataGridView1.Rows(DataGridView1.Rows.Count - 1)
        DataGridView1.CurrentCell = nouvelleLigne.Cells(1)
        nouvelleLigne.Cells(6).Value = 1

        ' Activer le mode d'édition pour la nouvelle ligne
        DataGridView1.BeginEdit(True)

        ' Parcourir toutes les colonnes de la nouvelle ligne et définir ReadOnly sur False
        For Each cell As DataGridViewCell In nouvelleLigne.Cells
            cell.ReadOnly = False
        Next
    End Sub

    Private Sub IconButton30_Click(sender As Object, e As EventArgs) Handles IconButton30.Click
        Dim nomProduit As String = DataGridView1.CurrentRow.Cells(3).Value

        ' Divisez la chaîne en mots en utilisant des espaces comme séparateurs
        Dim mots As String() = nomProduit.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

        ' Vérifiez s'il y a au moins un mot
        If mots.Length > 0 Then
            ' Le premier mot est dans mots(0)
            Dim premierMot As String = mots(0)
            Panel8.Visible = True

            adpt = New MySqlDataAdapter("select * from article where Article like '%" + premierMot + "%' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count() <> 0 Then
                DataGridView4.Rows.Clear()
                For i = 0 To table.Rows.Count - 1

                    DataGridView4.Rows.Add(table.Rows(i).Item(1), table.Rows(i).Item(10), Convert.ToDouble(table.Rows(i).Item(4)).ToString("N2"))
                Next

            End If
        End If
    End Sub

    Private Sub IconButton18_Click(sender As Object, e As EventArgs) Handles IconButton18.Click
        Panel8.Visible = False
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            Label53.Text = Convert.ToDouble(Label25.Text * 0.0025).ToString("N2")
            Label51.Text = Convert.ToDouble(Convert.ToDouble(Label25.Text) + Label53.Text).ToString("N2")
        Else
            Label53.Text = 0.00
            Label51.Text = Label25.Text
        End If
    End Sub

    Private Sub IconButton19_Click(sender As Object, e As EventArgs) Handles IconButton19.Click
        Try
            conn2.Close()
            conn2.Open()
            Dim id As String
            adpt = New MySqlDataAdapter("select id from achat order by id desc", conn2)
            Dim table4 As New DataTable
            adpt.Fill(table4)
            Dim ref As String
            If table4.Rows.Count <> 0 Then
                ref = Convert.ToString(Convert.ToDouble(table4.Rows(0).Item(0).ToString.Replace("R", "")) + 1).PadLeft(4, "0")
            Else
                ref = Convert.ToString("0001").PadLeft(4, "0")
            End If
            TextBox17.Text = "DV" & ref
            id = TextBox17.Text

            Dim montant As Double = 0
            Dim paye As Double = 0
            Dim reste As Double = 0
            montant = Convert.ToString(Label25.Text).Replace(".", ",").Replace(" ", "")
            reste = montant

            sql2 = "INSERT INTO devis (id,montant,Fournisseur,dateAchat,paye,reste,mtn_ht,rms,ord,caissier) VALUES ('" & id & "', '" + Label25.Text + "', '" + ComboBox1.Text + "', '" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', '" & paye & "', '" & reste & "', '" + Label18.Text + "', '" + Label23.Text + "','" & TextBox25.Text & "', '" & user & "' )"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()
            idasync = id
            DataGridView3.Sort(DataGridView3.Columns(22), System.ComponentModel.ListSortDirection.Ascending)
            For i = 0 To DataGridView3.Rows.Count - 1
                cmd2 = New MySqlCommand("INSERT INTO `devisdetails`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`rms`,`rt`,`gr`,`total_ht`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value12,@value13,@value14,@value16)", conn2)
                cmd2.Parameters.AddWithValue("@value15", id)
                cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(5).Value)
                cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value12", Convert.ToDouble(DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(" ", "").Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value16", DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ","))
                cmd2.ExecuteNonQuery()
            Next
            sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            If role = "Caissier" Then
                cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
            Else
                cmd3.Parameters.AddWithValue("@name", achats.Label2.Text)
            End If
            cmd3.Parameters.AddWithValue("@op", "Devis N° " & id)
            cmd3.ExecuteNonQuery()
            Dim yesornoMsg As New success
            yesornoMsg.Label14.Text = "Devis bien effectuée"
            yesornoMsg.Panel5.Visible = True
            yesornoMsg.ShowDialog()
            'If Msgboxresult = True Then

            conn2.Close()
            Me.Close()
        Catch ex As MySqlException
            Dim yesornoMsg As New eror
            yesornoMsg.Label14.Text = ex.Message
            yesornoMsg.Panel5.Visible = True
            yesornoMsg.ShowDialog()
            'If Msgboxresult = True Then

        End Try

    End Sub

    Private Sub IconButton20_Click(sender As Object, e As EventArgs) Handles IconButton20.Click
        conn2.Close()
        conn2.Open()
        If TextBox25.Text = "" AndAlso ComboBox4.Text = "" Then

            Dim yesornoMsg2 As New eror
            yesornoMsg2.Label14.Text = "Veuillez saisir le N° du document de reception et choisir le fournisseur !"
            yesornoMsg2.Panel5.Visible = True
            yesornoMsg2.ShowDialog()
        Else


            Dim id As String
            id = TextBox17.Text

            sql2 = "UPDATE achat SET montant = '" & Label51.Text.Replace(" ", "") * (-1) & "',reste = '" & Label51.Text.Replace(" ", "") * (-1) & "', dateAchat = '" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', Fournisseur = '" & ComboBox4.Text & "', mtn_ht = '" & Label18.Text & "', rms = '" & Label23.Text & "', ord = '" & TextBox25.Text & "' WHERE id = '" & id & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()

            sql2 = "UPDATE achatssauv SET montant = '" & Label51.Text.Replace(" ", "") * (-1) & "',reste = '" & Label51.Text.Replace(" ", "") * (-1) & "', dateAchat = '" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', Fournisseur = '" & ComboBox4.Text & "', mtn_ht = '" & Label18.Text & "', rms = '" & Label23.Text & "', ord = '" & TextBox25.Text & "' WHERE id = '" & id & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()

            adpt = New MySqlDataAdapter("select * from achatdetails where achat_Id = '" & id & "' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            For i = 0 To table.Rows.Count - 1
                cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  REPLACE(`Stock`,',','.') + '" & Convert.ToDouble(table.Rows(i).Item(9).ToString.Replace(".", ",")) & "' - '" & Convert.ToDouble(table.Rows(i).Item(13).ToString.Replace(".", ",")) & "'  WHERE `Code` = '" + table.Rows(i).Item(1).ToString.Replace(".", ",") + "' ", conn2)
                cmd2.ExecuteNonQuery()
            Next

            cmd2 = New MySqlCommand("DELETE FROM `achatdetails` where `achat_Id` = '" & id & "' ", conn2)
            cmd2.ExecuteNonQuery()
            cmd2 = New MySqlCommand("DELETE FROM `achatdetailssauv` where `achat_Id` = '" & id & "' ", conn2)
            cmd2.ExecuteNonQuery()

            DataGridView3.Sort(DataGridView3.Columns(22), System.ComponentModel.ListSortDirection.Ascending)

            For i = 0 To DataGridView3.Rows.Count - 1
                cmd2 = New MySqlCommand("INSERT INTO `achatdetails`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`rms`,`rt`,`gr`,`total_ht`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value12,@value13,@value14,@value16)", conn2)
                cmd2.Parameters.AddWithValue("@value15", id)
                cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(5).Value)
                cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value12", Convert.ToDouble(DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(" ", "").Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value16", DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ","))
                cmd2.ExecuteNonQuery()

                cmd2 = New MySqlCommand("INSERT INTO `achatdetailssauv`( `achat_Id`,`CodeArticle`, `NameArticle`, `PA_HT`, `TVA`, `PA_TTC`, `TM`, `PV_HT`, `PV_TTC`, `qte`,`total`,`rms`,`rt`,`gr`,`total_ht`) 
VALUES (@value15,@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value12,@value13,@value14,@value16)", conn2)
                cmd2.Parameters.AddWithValue("@value15", id)
                cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(1).Value)
                cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(2).Value)
                cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value4", DataGridView3.Rows(i).Cells(5).Value)
                cmd2.Parameters.AddWithValue("@value5", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value6", Convert.ToDouble(DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value7", Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value8", Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value9", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value10", Convert.ToDouble(DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value12", Convert.ToDouble(DataGridView3.Rows(i).Cells(17).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@value13", DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(" ", "").Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value14", DataGridView3.Rows(i).Cells(20).Value.ToString.Replace(" ", "").Replace(".", ","))
                cmd2.Parameters.AddWithValue("@value16", DataGridView3.Rows(i).Cells(7).Value.ToString.Replace(" ", "").Replace(".", ","))
                cmd2.ExecuteNonQuery()

                cmd2 = New MySqlCommand("UPDATE `article` SET `Stock` =  REPLACE(`Stock`,',','.') - '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",")) & "' + '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(19).Value.ToString.Replace(".", ",")) & "',`PV_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(10).Value.ToString.Replace(".", ",")) & "',`PA_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(".", ",")) & "', `PV_TTC` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(".", ",")) & "',`fournisseur` = '" & ComboBox4.Text & "', `PA_HT` = '" & Convert.ToDouble(DataGridView3.Rows(i).Cells(4).Value.ToString.Replace(".", ",")) & "' WHERE `Code` = '" + DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") + "' ", conn2)
                cmd2.ExecuteNonQuery()
                adpt = New MySqlDataAdapter("select * from article where Code = '" & DataGridView3.Rows(i).Cells(1).Value.ToString.Replace(".", ",") & "' ", conn2)
                Dim tablelike As New DataTable
                adpt.Fill(tablelike)

            Next
            Dim yesornoMsg As New success
            yesornoMsg.Label14.Text = "Modification bien effectuée"
            yesornoMsg.Panel5.Visible = True
            yesornoMsg.ShowDialog()
            'If Msgboxresult = True Then
            sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
            cmd3 = New MySqlCommand(sql3, conn2)
            cmd3.Parameters.Clear()
            If role = "Caissier" Then
                cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
            Else
                cmd3.Parameters.AddWithValue("@name", achats.Label2.Text)
            End If
            cmd3.Parameters.AddWithValue("@op", "Modification de l'Avoir N° " & id)
            cmd3.ExecuteNonQuery()
        End If
        conn2.Close()
        Me.Close()

    End Sub


    Private Sub IconButton22_Click(sender As Object, e As EventArgs) Handles IconButton22.Click
        If DataGridView3.Rows.Count <> 0 Then
            For i = 0 To DataGridView3.Rows.Count - 1
                DataGridView3.Rows(i).Cells(24).Value = Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")).ToString("N2")
            Next

        End If
    End Sub
    Dim tc As Double

    Dim ADPS As MySqlDataAdapter
    Dim ReportToPrint As LocalReport
    Dim m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)
        Return stream
    End Function
    Private Sub IconButton21_Click(sender As Object, e As EventArgs) Handles IconButton21.Click
        adpt = New MySqlDataAdapter("select id from bcmnds order by id desc", conn2)
        Dim table4 As New DataTable
        adpt.Fill(table4)
        Dim ref As String
        If table4.Rows.Count <> 0 Then
            ref = Convert.ToDouble(table4.Rows(0).Item(0).ToString.Replace("R", "")) + 1
        Else
            ref = 1
        End If
        conn2.Close()
        conn2.Open()
        sql2 = "INSERT INTO bcmnds (id,`name`,`date`) 
                    VALUES ('" & ref & "','" & ComboBox4.Text & "','" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "')"
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.Clear()
        cmd2.ExecuteNonQuery()


        For i = 0 To DataGridView3.Rows.Count - 1
            cmd2 = New MySqlCommand("INSERT INTO `bcmnds_details`(`bcmnd_id`, `dasignation`, `Code`, `qte`) 
VALUES (@value15,@value1,@value2,@value3)", conn2)
            cmd2.Parameters.AddWithValue("@value15", ref)
            cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))

            cmd2.ExecuteNonQuery()
        Next

        sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.Parameters.Clear()
        If role = "Caissier" Then
            cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
        Else
            cmd3.Parameters.AddWithValue("@name", achats.Label2.Text)
        End If
        cmd3.Parameters.AddWithValue("@op", "Bon de commande N° " & ref)
        cmd3.ExecuteNonQuery()

        conn2.Close()

        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("tick")
        For i = 0 To DataGridView3.Rows.Count - 1
            dt.Rows.Add(DataGridView3.Rows(i).Cells(1).Value, DataGridView3.Rows(i).Cells(2).Value, "", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
        Next




        ReportToPrint = New LocalReport()
        formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\bonc.rdlc"
        formata4.ReportViewer1.LocalReport.DataSources.Clear()
        formata4.ReportViewer1.LocalReport.EnableExternalImages = True

        Dim cliente As New ReportParameter("client", ComboBox4.Text)
        Dim client1(0) As ReportParameter
        client1(0) = cliente
        formata4.ReportViewer1.LocalReport.SetParameters(client1)

        Dim fac As New ReportParameter("fac", "BC N° " & TextBox25.Text)
        Dim fac1(0) As ReportParameter
        fac1(0) = fac
        formata4.ReportViewer1.LocalReport.SetParameters(fac1)

        Dim type As New ReportParameter("type", "Bon de Commande")
        Dim type1(0) As ReportParameter
        type1(0) = type
        formata4.ReportViewer1.LocalReport.SetParameters(type1)

        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim table3 As New DataTable
        adpt.Fill(table3)

        Dim datee As New ReportParameter("date", Convert.ToDateTime(DateTimePicker4.Value).ToString("dd/MM/yyyy"))
        Dim date1(0) As ReportParameter
        date1(0) = datee
        formata4.ReportViewer1.LocalReport.SetParameters(date1)


        Dim appPath As String = Application.StartupPath()
        imgName = table3.Rows(0).Item(11).ToString
        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

        Dim img As New ReportParameter("image", "File:\" + SavePath, True)
        Dim img1(0) As ReportParameter
        img1(0) = img
        formata4.ReportViewer1.LocalReport.SetParameters(img1)

        Dim details As New ReportParameter("infos", table3.Rows(0).Item(1).ToString + " | " + "Tél: " + table3.Rows(0).Item(2).ToString + " | " + "Email: " + table3.Rows(0).Item(3).ToString + " | " + "ICE: " + table3.Rows(0).Item(4).ToString + " | " + "IF: " + table3.Rows(0).Item(5).ToString + " | " + "TP: " + table3.Rows(0).Item(7).ToString + " | " + "RC: " + table3.Rows(0).Item(8).ToString)
        Dim details1(0) As ReportParameter
        details1(0) = details
        formata4.ReportViewer1.LocalReport.SetParameters(details1)



        'formata4.ReportViewer1.LocalReport.ReportEmbeddedResource = "Myapp.Report1.rdlc"
        formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
#Disable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.
        formata4.ReportViewer1.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
#Enable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.
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
        Me.Close()
    End Sub

    Private Sub TextBox25_LostFocus(sender As Object, e As EventArgs) Handles TextBox25.LostFocus

    End Sub

    Private Sub TextBox25_Leave(sender As Object, e As EventArgs) Handles TextBox25.Leave
        adpt2 = New MySqlDataAdapter("select count(*) from achat where ord = '" & TextBox25.Text & "' and Fournisseur = '" & ComboBox4.Text & "'", conn2)
        Dim tableachat As New DataTable
        adpt2.Fill(tableachat)
        If tableachat.Rows(0).Item(0) <> 0 Then
            Dim yesornoMsg As New eror
            yesornoMsg.Label14.Text = "Ce Bon est déjà saisi !"
            yesornoMsg.Panel5.Visible = True
            yesornoMsg.ShowDialog()
            'If Msgboxresult = True Then
            TextBox25.Select()
        End If
    End Sub

    Private Sub IconButton23_Click(sender As Object, e As EventArgs) Handles IconButton23.Click

        conn2.Close()
        conn2.Open()


        cmd2 = New MySqlCommand("DELETE FROM `bcmnds` where `id` = '" & TextBox25.Text & "' ", conn2)
        cmd2.ExecuteNonQuery()

        cmd2 = New MySqlCommand("DELETE FROM `bcmnds_details` where `bcmnd_id` = '" & TextBox25.Text & "' ", conn2)
        cmd2.ExecuteNonQuery()

        sql2 = "INSERT INTO bcmnds (id,`name`,`date`) 
                    VALUES ('" & TextBox25.Text & "','" & ComboBox4.Text & "','" & DateTimePicker4.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") & "')"
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.Parameters.Clear()
        cmd2.ExecuteNonQuery()



        For i = 0 To DataGridView3.Rows.Count - 1
            cmd2 = New MySqlCommand("INSERT INTO `bcmnds_details`(`bcmnd_id`, `dasignation`, `Code`, `qte`) 
VALUES (@value15,@value1,@value2,@value3)", conn2)
            cmd2.Parameters.AddWithValue("@value15", TextBox25.Text)
            cmd2.Parameters.AddWithValue("@value1", DataGridView3.Rows(i).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@value2", DataGridView3.Rows(i).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@value3", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00"))

            cmd2.ExecuteNonQuery()
        Next

        sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.Parameters.Clear()
        If role = "Caissier" Then
            cmd3.Parameters.AddWithValue("@name", dashboard.Label2.Text)
        Else
            cmd3.Parameters.AddWithValue("@name", achats.Label2.Text)
        End If
        cmd3.Parameters.AddWithValue("@op", "Modification du Bon de commande N° " & TextBox25.Text)
        cmd3.ExecuteNonQuery()

        conn2.Close()

        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("tick")
        For i = 0 To DataGridView3.Rows.Count - 1
            dt.Rows.Add(DataGridView3.Rows(i).Cells(1).Value, DataGridView3.Rows(i).Cells(2).Value, "", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "").Replace(".", ",")).ToString("N2"))
        Next




        ReportToPrint = New LocalReport()
        formata4.ReportViewer1.LocalReport.ReportPath = Application.StartupPath + "\bonc.rdlc"
        formata4.ReportViewer1.LocalReport.DataSources.Clear()
        formata4.ReportViewer1.LocalReport.EnableExternalImages = True

        Dim cliente As New ReportParameter("client", ComboBox4.Text)
        Dim client1(0) As ReportParameter
        client1(0) = cliente
        formata4.ReportViewer1.LocalReport.SetParameters(client1)

        Dim fac As New ReportParameter("fac", "BC N° " & TextBox25.Text)
        Dim fac1(0) As ReportParameter
        fac1(0) = fac
        formata4.ReportViewer1.LocalReport.SetParameters(fac1)

        Dim type As New ReportParameter("type", "Bon de Commande")
        Dim type1(0) As ReportParameter
        type1(0) = type
        formata4.ReportViewer1.LocalReport.SetParameters(type1)

        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim table3 As New DataTable
        adpt.Fill(table3)

        Dim datee As New ReportParameter("date", Convert.ToDateTime(DateTimePicker4.Value).ToString("dd/MM/yyyy"))
        Dim date1(0) As ReportParameter
        date1(0) = datee
        formata4.ReportViewer1.LocalReport.SetParameters(date1)


        Dim appPath As String = Application.StartupPath()
        imgName = table3.Rows(0).Item(11).ToString
        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

        Dim img As New ReportParameter("image", "File:\" + SavePath, True)
        Dim img1(0) As ReportParameter
        img1(0) = img
        formata4.ReportViewer1.LocalReport.SetParameters(img1)

        Dim details As New ReportParameter("infos", table3.Rows(0).Item(1).ToString + " | " + "Tél: " + table3.Rows(0).Item(2).ToString + " | " + "Email: " + table3.Rows(0).Item(3).ToString + " | " + "ICE: " + table3.Rows(0).Item(4).ToString + " | " + "IF: " + table3.Rows(0).Item(5).ToString + " | " + "TP: " + table3.Rows(0).Item(7).ToString + " | " + "RC: " + table3.Rows(0).Item(8).ToString)
        Dim details1(0) As ReportParameter
        details1(0) = details
        formata4.ReportViewer1.LocalReport.SetParameters(details1)



        'formata4.ReportViewer1.LocalReport.ReportEmbeddedResource = "Myapp.Report1.rdlc"
        formata4.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
#Disable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.
        formata4.ReportViewer1.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
#Enable Warning BC42030 ' La variable 'warnings' est passée par référence avant qu'une valeur lui ait été attribuée. Cela peut provoquer une exception de référence null au moment de l'exécution.
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
        Me.Close()
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
    Friend Class RawPrinterHelper
        ' Structure and API declarions:
        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
        Public Class DOCINFOA
            <MarshalAs(UnmanagedType.LPStr)>
            Public pDocName As String
            <MarshalAs(UnmanagedType.LPStr)>
            Public pOutputFile As String
            <MarshalAs(UnmanagedType.LPStr)>
            Public pDataType As String
        End Class

        <DllImport("winspool.Drv", EntryPoint:="OpenPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function OpenPrinter(
        <MarshalAs(UnmanagedType.LPStr)> ByVal szPrinter As String, <Out> ByRef hPrinter As IntPtr, ByVal pd As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="ClosePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Integer,
        <[In], MarshalAs(UnmanagedType.LPStruct)> ByVal di As DOCINFOA) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="WritePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Integer, <Out> ByRef dwWritten As Integer) As Boolean
        End Function
    End Class

    Private Sub TextBox25_MouseLeave(sender As Object, e As EventArgs) Handles TextBox25.MouseLeave

    End Sub

    Private Sub DataGridView3_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView3.ColumnHeaderMouseClick
        If e.ColumnIndex = 0 Then
            DataGridView3.Sort(DataGridView3.Columns(22), System.ComponentModel.ListSortDirection.Ascending)
        End If
    End Sub

End Class

Public Class NumericComparer
    Implements IComparer

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        Dim numX As Double
        Dim numY As Double

        If Double.TryParse(x.ToString(), numX) AndAlso Double.TryParse(y.ToString(), numY) Then
            Return numX.CompareTo(numY)
        Else
            Return String.Compare(x.ToString(), y.ToString())
        End If
    End Function
End Class