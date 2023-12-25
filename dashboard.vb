Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient

Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Media
Imports WindowsInput
Imports System.Threading

Public Class dashboard
    'Dim conn As New MySqlConnection("datasource=45.87.81.78; username=u398697865_Animal; password=J2cd@2021; database=u398697865_Animal")
    Dim adpt As MySqlDataAdapter
    Dim wit = Screen.PrimaryScreen.WorkingArea.Width
    Dim hit = My.Computer.Screen.WorkingArea.Size.Height
    Dim secw = wit - 548 - 36
    Dim sech = hit - 59 - 44
    Dim f9clicked As Boolean = False
    Public Property Caisserol As String

    Private soundPlayer As New SoundPlayer(Application.StartupPath() & "\" & "logos\Wrong audio.WAV")


    Private Sub dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Dim appPath As String = Application.StartupPath()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = System.IO.Path.Combine(SaveDirectory, imgName)
        If System.IO.File.Exists(SavePath) Then
            PictureBox2.Image = Image.FromFile(SavePath)
        End If

        DataGridView3.Columns(3).DefaultCellStyle.Format = "# ##0.00"
        DataGridView3.Columns(6).DefaultCellStyle.Format = "# ##0.00"

        adpt = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
        Dim tabletick As New DataTable
        adpt.Fill(tabletick)
        Dim id As Integer
        If tabletick.Rows().Count = 0 Then
            id = 1

        Else
            id = tabletick.Rows(0).Item(0) + 1

        End If
        Label20.Text = "Ticket N° " & id.ToString

        Dim printerticket As String = System.Configuration.ConfigurationSettings.AppSettings("printerticket")
        Dim printera5 As String = System.Configuration.ConfigurationSettings.AppSettings("printera5")
        Dim printera4 As String = System.Configuration.ConfigurationSettings.AppSettings("printera4")

        receiptprinter = printerticket
        a4printer = printera4
        a5printer = printera5
        Me.KeyPreview = True
        Dim pkInstalledPrinters As String

        ' Find all printers installed
        For Each pkInstalledPrinters In
        PrinterSettings.InstalledPrinters
            ComboBox5.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        ' Set the combo to the first printer in the list
        ComboBox5.SelectedIndex = 0

        DataGridView3.Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells

        If ComboBox1.Items.Count > 0 Then
            ComboBox1.SelectedIndex = 0
        End If

        If ComboBox4.Items.Count > 0 Then
            ComboBox4.SelectedIndex = 0
        End If

        Label2.Text = user
        TextBox1.Text = 0
        If Label2.Text <> "Admin" Then
            TextBox1.Visible = False
        End If

        Me.Width = wit
        Me.Height = hit
        Me.Location = New Point(0, 0)

        adpt = New MySqlDataAdapter("select client from clients where masq = 'non' order by client asc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        DataGridView1.Rows.Clear()
        ComboBox2.Items.Clear()
        ComboBox6.Items.Clear()
        If table.Rows.Count <> 0 Then
            For i = 0 To table.Rows.Count - 1
                ComboBox2.Items.Add(table.Rows(i).Item(0))
                ComboBox6.Items.Add(table.Rows(i).Item(0))
            Next
        End If
        ComboBox2.SelectedItem = "comptoir"
        ComboBox6.SelectedItem = "comptoir"
        'Panel2.Width = wit - 548
        'Panel2.Height = hit - 59
        'Panel2.Location = New Point(548, 59)

        'Panel4.Width = secw
        'Panel4.Height = sech
        'Panel4.Location = New Point(36, 44)

        'Panel4.Visible = False
        'adpt = New MySqlDataAdapter("select id, name, price from products", conn2)
        'Dim table As New DataTable
        'adpt.Fill(table)
        'DataGridView1.Rows.Clear()
        'If table.Rows.Count <> 0 Then
        '    For i = 0 To table.Rows.Count - 1
        '        DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2))
        '    Next
        'End If

        'Dim L As Integer = 0
        'Dim T As Integer = 10

        'Dim W As Integer = 116 'px
        'Dim H As Integer = 128 'px

        'Dim i = 0

        'For Each row As DataRow In table.Rows

        '    If L >= Panel3.Width - W Then
        '        L = 0
        '        T += 135
        '    End If

        '    Dim pnl As New Panel
        '    pnl.Name = "panelcat" & i
        '    pnl.Left = L
        '    pnl.Top = T
        '    pnl.Width = W
        '    pnl.Height = H


        '    Dim pic As New PictureBox
        '    pic.Name = row.Item(0)
        '    pic.Left = 0
        '    pic.Top = 0
        '    pic.Width = W
        '    pic.Height = 96
        '    pic.SizeMode = PictureBoxSizeMode.Zoom

        '    If row.Item(1).ToString <> "" Then
        '        Dim id As String = row.Item(1)
        '        Dim SaveDirectory As String = "c:\pospictures"
        '        Dim SavePath As String = System.IO.Path.Combine(SaveDirectory, id)
        '        If System.IO.File.Exists(SavePath) Then
        '            pic.Image = Image.FromFile(SavePath)
        '        End If



        '    End If

        '    Dim lbl As New Label
        '    lbl.Name = "lbl" & row.Item(0)
        '    lbl.Text = row.Item(0)
        '    lbl.Left = 0
        '    lbl.Top = 98
        '    lbl.Width = W
        '    lbl.Height = 30
        '    lbl.Font = New Font("Helvetica", 9, FontStyle.Bold)
        '    lbl.ForeColor = Color.Black
        '    lbl.TextAlign = ContentAlignment.MiddleCenter

        '    Panel3.Controls.Add(pnl)
        '    pnl.Controls.Add(pic)
        '    pnl.Controls.Add(lbl)

        '    L += W + 5
        '    AddHandler pic.Click, AddressOf Me.picturebox_done_clicked
        'Next
        charg = True
        TextBox2.Select()
        TextBox2.Text = ""
        If role = "Caissier" Then
            IconButton10.Visible = False
            IconButton27.Visible = True
            IconButton38.Visible = True
        Else
            IconButton10.Visible = True
        End If



    End Sub
    Dim catname As String


    Private Sub DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Dim Rep As Integer
        Rep = MsgBox("Voulez-vous vraiment vider la commande ?", vbYesNo)
        If Rep = vbYes Then
            If Label28.Text = "numbl" Then
                If Label18.Visible = False Then

                    DataGridView3.Rows.Clear()
                    Label6.Text = 0.ToString("# ##0.00")
                    Try
                        If IsNumeric(TextBox3.Text) = False Then
                            If Label9.Text = "Rendu :" Then
                                Label4.Text = 0.00
                            End If
                            If Label9.Text = "Reste :" Then
                                Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                            End If
                        Else
                            If Label9.Text = "Rendu :" Then
                                Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                            End If
                            If Label9.Text = "Reste :" Then
                                Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                            End If
                        End If
                    Catch ex As MySqlException
                        MsgBox(ex.Message)
                    End Try
                    Label23.Text = 0.ToString("# ##0.00")
                    Label24.Text = 0.ToString("# ##0.00")
                    Label25.Text = 0.ToString("# ##0.00")
                    TextBox21.Text = ""


                    Label7.Text = 0.ToString("# ##0.00")
                    TextBox2.Select()
                    TextBox2.Text = ""
                    TextBox1.Text = 0
                    TextBox3.Text = ""
                    ComboBox2.SelectedItem = "comptoir"
                    ComboBox1.SelectedItem = "Espèce"
                    Label18.Visible = False

                    If ComboBox2.SelectedItem = "comptoir" Then
                        ComboBox2.Visible = False
                    Else
                        ComboBox2.Visible = True
                    End If
                Else
                    MsgBox("Merci de Parquer la commande en cours pour la vider !")
                End If
            Else
                MsgBox("Merci de Continuer la modification du bon avant de vider la caisse ou la fermer !!")


            End If
        End If
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs)

        TextBox4.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub
    Dim tc As Decimal

    Dim ADPS As MySqlDataAdapter
    Dim ReportToPrint As LocalReport
    Dim m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)
        Return stream
    End Function
    Dim tbl As New DataTable
    Dim tbl2 As New DataTable
    Dim stck As Decimal
    Dim prod As String
    Dim newClient As Boolean = False
    <Obsolete>
    Dim mReode As String
    Dim anciencrdt As String
    Dim idasync As Integer
    <Obsolete>
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Try
            conn2.Close()
            conn2.Open()
            anciencrdt = "-"
            Dim mxcrdt As Decimal = 0
            'mxCredt.ExecuteScalar
            conn2.Close()
            If mxcrdt = 0 Then
                If DataGridView3.Rows.Count <= 0 Then
                    MsgBox(“Ticket Vide ! ”)
                Else

                    If ComboBox1.Text <> "" Then


                        If Label17.Text = 1 Then
                            If ComboBox2.Text = "comptoir" Or ComboBox2.Text = "Comptoir" Or ComboBox2.Text = "Client fidèle" Then
                                Casef = True
                                tfraisN = "Coût Logistique"
                                tfraisM = 0
                                Livr = "-"
                            Else
                                frais.ShowDialog()

                            End If
                            Dim cmd As MySqlCommand
                            Dim cmd2, cmd3 As MySqlCommand
                            If Casef = True Then

                                Dim sql As String
                                Dim sql2, sql3 As String
                                Dim execution As Integer

                                If Label18.Visible = True Then
                                    conn2.Close()
                                    conn2.Open()
                                    cmd = New MySqlCommand("DELETE FROM `orderdetails` WHERE type = 'ventes' and `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `orders` WHERE `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()
                                    conn2.Close()

                                End If

                                adpt = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
                                Dim table As New DataTable
                                adpt.Fill(table)

                                adpt = New MySqlDataAdapter("SELECT id from bondelivr ORDER BY id DESC LIMIT 1", conn2)
                                Dim tablebl As New DataTable
                                adpt.Fill(tablebl)
                                Dim rms As String
                                If TextBox1.Text = "" Then
                                    rms = 0
                                Else
                                    rms = Label7.Text.ToString.Replace(" ", "") * TextBox1.Text / 100

                                End If

                                Dim reste As String
                                Dim client As String
                                Dim payer As String



                                If Label9.Text = "Reste :" Then
                                    If TextBox3.Text = "" Then
                                        payer = 0
                                        reste = Label6.Text.ToString.Replace(" ", "").Replace(".", ",")


                                    Else
                                        If TextBox3.Text <= 0 Then
                                            payer = 0
                                            reste = Label6.Text.ToString.Replace(" ", "").Replace(".", ",")
                                        End If
                                        If TextBox3.Text >= Label6.Text Then
                                            payer = Label6.Text.ToString.Replace(" ", "").Replace(".", ",")
                                            reste = 0
                                        ElseIf TextBox3.Text > 0 Then
                                            payer = TextBox3.Text
                                            reste = Convert.ToDouble(Label6.Text.ToString.Replace(" ", "").Replace(".", ",")) - payer
                                        End If
                                    End If
                                Else
                                    reste = 0

                                    payer = Label6.Text.ToString.Replace(" ", "").Replace(".", ",")
                                End If

                                If newClient = True Then
                                    client = ComboBox2.Text
                                Else
                                    client = ComboBox2.Text
                                End If

                                conn2.Close()
                                conn2.Open()
                                If table.Rows().Count = 0 Then
                                    id = 1

                                Else
                                    id = table.Rows(0).Item(0) + 1

                                End If
                                Dim blnum As Integer

                                If tablebl.Rows().Count = 0 Then
                                    blnum = 1

                                Else
                                    blnum = tablebl.Rows(0).Item(0) + 1

                                End If
                                If ComboBox2.Text <> "comptoir" AndAlso ComboBox2.Text <> "Comptoir" Then
                                    sql2 = "INSERT INTO bondelivr (`order_id`) 
                    VALUES ('" & id & "')"
                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()
                                    CheckBox2.Checked = False
                                End If

                                Dim remiseprc As Decimal = 0

                                If TextBox21.Text <> "" Then
                                    remiseprc = TextBox21.Text
                                End If

                                If Label28.Text = "numbl" Then

                                    adpt = New MySqlDataAdapter("select OrderID from orderssauv where OrderID = '" & id & "' and client = '" & client & "' ", conn2)
                                    Dim tablesauv As New DataTable
                                    adpt.Fill(tablesauv)
                                    If tablesauv.Rows.Count <> 0 Then
                                        cmd2 = New MySqlCommand("DELETE FROM `orderssauv` where `OrderID` = '" & id & "' ", conn2)
                                        cmd2.ExecuteNonQuery()

                                        cmd2 = New MySqlCommand("DELETE FROM `orderdetailssauv` where `OrderID` = '" & id & "' ", conn2)
                                        cmd2.ExecuteNonQuery()
                                    Else
                                        adpt = New MySqlDataAdapter("select OrderID from orderssauv where OrderID = '" & id & "'", conn2)
                                        Dim tablesauv2 As New DataTable
                                        adpt.Fill(tablesauv2)
                                        If tablesauv2.Rows.Count <> 0 Then
                                            id = id + 1
                                        End If
                                    End If

                                    sql2 = "INSERT INTO orders (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",").Replace(" ", "") + "','" + Label4.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text.Replace(" ", "") * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste.Replace(" ", "") + "','" + tfraisM + "','" & Livr & "')"
                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()

                                    sql2 = "INSERT INTO orderssauv (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",").Replace(" ", "") + "','" + Label4.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text.Replace(" ", "") * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste.Replace(" ", "") + "','" + tfraisM + "','" & Livr & "')"
                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()
                                Else
                                    id = Label28.Text
                                    Dim clot As String = 0
                                    If Convert.ToDateTime(Label29.Text).ToString("yyyy-MM-dd") = DateTime.Now.ToString("yyyy-MM-dd") Then
                                    Else

                                        clot = 1
                                    End If
                                    cmd = New MySqlCommand("DELETE FROM `orderdetails` WHERE `OrderID` = '" & id & "' ", conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `orders` WHERE `OrderID` = '" & id & "' ", conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `cheques` WHERE `tick` = '" & id & "' ", conn2)
                                    cmd.ExecuteNonQuery()
                                    sql2 = "INSERT INTO orders (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`,`cloture`) 
                    VALUES ('" & id & "', '" + Convert.ToDateTime(Label29.Text).ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",").Replace(" ", "") + "','" + Label4.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text.Replace(" ", "") * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste.Replace(" ", "") + "','" + tfraisM + "','" & Livr & "','" & clot & "')"

                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()

                                    cmd = New MySqlCommand("DELETE FROM `orderdetailssauv` WHERE `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `orderssauv` WHERE `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()

                                    sql2 = "INSERT INTO orderssauv (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`,`cloture`) 
                    VALUES ('" & id & "', '" + Convert.ToDateTime(Label29.Text).ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",").Replace(" ", "") + "','" + Label4.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text.Replace(" ", "") * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste.Replace(" ", "") + "','" + tfraisM + "','" & Livr & "','" & clot & "')"

                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()
                                End If

                                If ComboBox1.Text = "TPE" Then
                                    adpt = New MySqlDataAdapter("SELECT organisme FROM `banques` WHERE `tpe` = '1'", conn2)
                                    Dim tabletpe As New DataTable
                                    adpt.Fill(tabletpe)

                                    sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `tick`,`bq`,`montant`,`client`) 
                    VALUES ('tpe','" + TextBox10.Text + "','tpe','" + TextBox9.Text + "','tpe', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" & id & "','" + tabletpe.Rows(0).Item(0) + "','" + Convert.ToDecimal(Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + tfraisM).ToString("# ##0.00") + "','" & ComboBox2.Text & "')"
                                    cmd3 = New MySqlCommand(sql3, conn2)
                                    cmd3.Parameters.Clear()
                                    cmd3.ExecuteNonQuery()

                                End If
                                idasync = id

                                'adpt = New MySqlDataAdapter("select * from fideles WHERE code = '" + TextBox8.Text + "'", conn2)
                                'Dim table3 As New DataTable
                                'adpt.Fill(table3)
                                'If table3.Rows.Count() <> 0 Then
                                '    If table3.Rows(0).Item(2) < 5 Then
                                '        cmd3 = New MySqlCommand("UPDATE `fideles` SET `lastachats`= '" + textbox21.text.ToString + "', `achats` = `achats` + '" & Convert.ToDecimal(Label23.Text - (Label23.Text * (textbox21.text.Replace(".", ",") / 100))).ToString("#0.00") & "'  WHERE code = '" + TextBox8.Text + "'", conn2)
                                '        cmd3.Parameters.Clear()
                                '        cmd3.ExecuteNonQuery()
                                '    End If

                                '    If table3.Rows(0).Item(2) >= 5 Then
                                '        cmd3 = New MySqlCommand("UPDATE `fideles` SET `lastachats`= '" + textbox21.text.ToString + "', `achats` = '" + Convert.ToDecimal(Label23.Text - (Label23.Text * (textbox21.text.Replace(".", ",") / 100))).ToString("#0.00") + "'  WHERE code = '" + TextBox8.Text + "'", conn2)
                                '        cmd3.Parameters.Clear()
                                '        cmd3.ExecuteNonQuery()
                                '    End If

                                'End If

                                For Each dr As DataGridViewRow In Me.DataGridView3.Rows
                                    conn2.Close()
                                    conn2.Open()
                                    Try
                                        adpt = New MySqlDataAdapter("select * from article where Code = '" + dr.Cells(4).Value.ToString + "' and mask = '0'", conn2)
                                        Dim table2 As New DataTable
                                        adpt.Fill(table2)
                                        Dim marg As Decimal = 0
                                        If table2.Rows.Count <> 0 Then
                                            If Label28.Text = "numbl" Then
                                                stck = Convert.ToDecimal(table2.Rows(0).Item(8)) - Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                            Else
                                                If Convert.ToDecimal(dr.Cells(2).Value.ToString.Replace(" ", "")) < 0 Then
                                                    stck = Convert.ToDecimal(table2.Rows(0).Item(8)) + Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                                Else
                                                    stck = Convert.ToDecimal(table2.Rows(0).Item(8)) - Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                                End If
                                            End If
                                            marg = (((Convert.ToDecimal(dr.Cells(10).Value.ToString.Replace(".", ",")) - (Convert.ToDecimal(dr.Cells(10).Value.ToString.Replace(".", ",")) * (Convert.ToDecimal(dr.Cells(8).Value.ToString.Replace(".", ",")) / 100))) - Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")))) * (Convert.ToDecimal(dr.Cells(2).Value.ToString.Replace(".", ",")) - Convert.ToDecimal(dr.Cells(9).Value.ToString.Replace(".", ",")))
                                            If marg <= 0 Then
                                                marg = 0
                                            End If
                                        End If

                                        cmd3 = New MySqlCommand("UPDATE `article` SET `Stock`= '" + stck.ToString + "', `sell` = 1  WHERE Code = '" + dr.Cells(4).Value.ToString + "'", conn2)
                                        cmd3.ExecuteNonQuery()
                                        cmd = New MySqlCommand
                                        prod = dr.Cells(5).Value
                                        sql = “INSERT INTO orderdetails (OrderID ,ProductID ,Price,Quantity,Total,date,pa,type,name,marge,rms,gr,pv,tva) VALUES (@tick, @code, @price, @qty, @montant, @date,@pa,@type,@name,@mrg,@rms,@gr,@pv,@tva);”
                                        sql2 = “INSERT INTO orderdetailssauv (OrderID ,ProductID ,Price,Quantity,Total,date,pa,type,name,marge,rms,gr,pv,tva) VALUES (@tick, @code, @price, @qty, @montant, @date,@pa,@type,@name,@mrg,@rms,@gr,@pv,@tva);”

                                        With cmd
                                            .Connection = conn2
                                            .CommandText = sql
                                            .Parameters.Clear()
                                            .Parameters.AddWithValue(“@tick”, id)

                                            .Parameters.AddWithValue(“@code”, dr.Cells(4).Value)
                                            .Parameters.AddWithValue(“@price”, Convert.ToDecimal(dr.Cells(3).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@qty”, Convert.ToDecimal(dr.Cells(2).Value).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@montant”, Convert.ToDecimal(dr.Cells(6).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@date”, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                            .Parameters.AddWithValue(“@pa”, Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@type”, "ventes")
                                            .Parameters.AddWithValue(“@name”, dr.Cells(1).Value)
                                            .Parameters.AddWithValue(“@mrg”, Convert.ToDecimal(marg.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@rms”, dr.Cells(8).Value)
                                            .Parameters.AddWithValue(“@gr”, dr.Cells(9).Value)
                                            .Parameters.AddWithValue(“@pv”, dr.Cells(10).Value)
                                            .Parameters.AddWithValue(“@tva”, dr.Cells(11).Value)
                                            execution = .ExecuteNonQuery()

                                            .CommandText = sql2
                                            .Parameters.Clear()
                                            .Parameters.AddWithValue(“@tick”, id)

                                            .Parameters.AddWithValue(“@code”, dr.Cells(4).Value)
                                            .Parameters.AddWithValue(“@price”, Convert.ToDecimal(dr.Cells(3).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@qty”, Convert.ToDecimal(dr.Cells(2).Value).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@montant”, Convert.ToDecimal(dr.Cells(6).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@date”, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                            .Parameters.AddWithValue(“@pa”, Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@type”, "ventes")
                                            .Parameters.AddWithValue(“@name”, dr.Cells(1).Value)
                                            .Parameters.AddWithValue(“@mrg”, Convert.ToDecimal(marg.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@rms”, dr.Cells(8).Value)
                                            .Parameters.AddWithValue(“@gr”, dr.Cells(9).Value)
                                            .Parameters.AddWithValue(“@pv”, dr.Cells(10).Value)
                                            .Parameters.AddWithValue(“@tva”, dr.Cells(11).Value)
                                            execution = .ExecuteNonQuery()
                                        End With
                                        conn2.Close()

                                    Catch ex As MySqlException
                                        MsgBox(ex.Message)
                                    End Try

                                Next

                                Dim sumqty As Decimal

                                adpt = New MySqlDataAdapter("SELECT reste FROM `orders` WHERE `client` = '" & ComboBox2.Text & "'", conn2)
                                Dim tablecrdt As New DataTable
                                adpt.Fill(tablecrdt)
                                Dim ancreste As Decimal = 0
                                For j = 0 To tablecrdt.Rows.Count() - 1
                                    ancreste += Convert.ToDecimal(tablecrdt.Rows(j).Item(0))
                                Next

                                If ComboBox2.Text = "comptoir" Then
                                    anciencrdt = 0
                                Else

                                    anciencrdt = ancreste.ToString("# ##0.00")

                                End If

                                Dim ds As New DataSet1
                                Dim dt As New DataTable
                                dt = ds.Tables("tick")
                                For i = 0 To DataGridView3.Rows.Count - 1
                                    If DataGridView3.Rows(i).Cells(9).Value = 0 Then
                                        Dim found As Boolean = False
                                        For K = 0 To dt.Rows.Count - 1
                                            If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                                                found = True
                                                dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(2).Value
                                                dt.Rows(K).Item(7) = Convert.ToDouble(dt.Rows(K).Item(7)) + DataGridView3.Rows(i).Cells(6).Value
                                                sumqty += DataGridView3.Rows(i).Cells(2).Value
                                                Exit For

                                            End If
                                        Next
                                        If Not found Then
                                            dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value, "", Convert.ToDouble(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "")).ToString("N2"), "", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "")).ToString("N2"), "", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")).ToString("N2"), DataGridView3.Rows(i).Cells(7).Value)
                                            sumqty += DataGridView3.Rows(i).Cells(2).Value
                                        End If
                                    End If
                                Next
                                For i = 0 To DataGridView3.Rows.Count - 1
                                    If DataGridView3.Rows(i).Cells(9).Value > 0 Then
                                        Dim found As Boolean = False
                                        For K = 0 To dt.Rows.Count - 1
                                            If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                                                found = True
                                                dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(9).Value
                                                sumqty += DataGridView3.Rows(i).Cells(9).Value
                                                Exit For

                                            End If
                                        Next
                                        If Not found Then
                                            dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value & " (Gratuit)", "", DataGridView3.Rows(i).Cells(9).Value, "", 0, "", 0, 0)
                                            sumqty += DataGridView3.Rows(i).Cells(9).Value
                                        End If
                                    End If
                                Next

                                Try

                                    Casef = True
                                    If Livr = "Livreur" Then
                                        tfraisM = 0
                                    End If

                                    If CheckBox2.Checked Then
                                        ReportToPrint = New LocalReport()
                                        ReportToPrint.ReportPath = Application.StartupPath + "\Report1.rdlc"
                                        ReportToPrint.DataSources.Clear()
                                        ReportToPrint.EnableExternalImages = True

                                        Dim clt As String

                                        If ComboBox2.Text = "" Then
                                            clt = "comptoir"
                                        Else
                                            clt = ComboBox2.Text
                                        End If


                                        Dim lpar3 As New ReportParameter("totale", Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") + Convert.ToDecimal(tfraisM.Replace(".", ","))).ToString("# ##0.00"))
                                        Dim lpar31(0) As ReportParameter
                                        lpar31(0) = lpar3
                                        ReportToPrint.SetParameters(lpar31)

                                        Dim Acre As New ReportParameter("CreditAncien", anciencrdt)
                                        Dim Acre1(0) As ReportParameter
                                        Acre1(0) = Acre
                                        ReportToPrint.SetParameters(Acre1)



                                        Dim cliente As New ReportParameter("client", clt)
                                        Dim client1(0) As ReportParameter
                                        client1(0) = cliente
                                        ReportToPrint.SetParameters(client1)

                                        Dim ide As New ReportParameter("id", id)
                                        Dim id1(0) As ReportParameter
                                        id1(0) = ide
                                        ReportToPrint.SetParameters(id1)


                                        Dim tpe As New ReportParameter("tpe", "")
                                        Dim tpe1(0) As ReportParameter
                                        tpe1(0) = tpe
                                        ReportToPrint.SetParameters(tpe1)

                                        Dim daty As New ReportParameter("date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                        Dim daty1(0) As ReportParameter
                                        daty1(0) = daty
                                        ReportToPrint.SetParameters(daty1)

                                        Dim mode As New ReportParameter("mode", ComboBox1.Text)
                                        Dim mode1(0) As ReportParameter
                                        mode1(0) = mode
                                        ReportToPrint.SetParameters(mode1)

                                        Dim caissier As New ReportParameter("caissier", Label2.Text)
                                        Dim caissier1(0) As ReportParameter
                                        caissier1(0) = caissier
                                        ReportToPrint.SetParameters(caissier1)

                                        Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
                                        Dim table23 As New DataTable
                                        adpt23.Fill(table23)

                                        Dim info As New ReportParameter("info", table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString)
                                        Dim info1(0) As ReportParameter
                                        info1(0) = info
                                        ReportToPrint.SetParameters(info1)


                                        Dim msg As New ReportParameter("msg", table23.Rows(0).Item(6).ToString)
                                        Dim msg1(0) As ReportParameter
                                        msg1(0) = msg
                                        ReportToPrint.SetParameters(msg1)

                                        Dim count As New ReportParameter("count", dt.Rows.Count.ToString)
                                        Dim count1(0) As ReportParameter
                                        count1(0) = count
                                        ReportToPrint.SetParameters(count1)

                                        Dim fraisN As New ReportParameter("FraisN", tfraisN)
                                        Dim fraisN1(0) As ReportParameter
                                        fraisN1(0) = fraisN
                                        ReportToPrint.SetParameters(fraisN1)

                                        Dim fraisM As New ReportParameter("FraisM", tfraisM)
                                        Dim fraisM1(0) As ReportParameter
                                        fraisM1(0) = fraisM
                                        ReportToPrint.SetParameters(fraisM1)

                                        Dim livreur As New ReportParameter("livreur", Livr)
                                        Dim livreur1(0) As ReportParameter
                                        livreur1(0) = livreur
                                        ReportToPrint.SetParameters(livreur1)

                                        Dim qty As New ReportParameter("qty", sumqty.ToString)
                                        Dim qty1(0) As ReportParameter
                                        qty1(0) = qty
                                        ReportToPrint.SetParameters(qty1)


                                        Dim appPath As String = Application.StartupPath()

                                        Dim SaveDirectory As String = appPath & "\"
                                        Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                                        Dim img As New ReportParameter("image", "File:\" + SavePath, True)
                                        Dim img1(0) As ReportParameter
                                        img1(0) = img
                                        ReportToPrint.SetParameters(img1)

                                        ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

                                        'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
                                        'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                                        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
                                        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
                                        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
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

                                        adpt = New MySqlDataAdapter("select ticket from parameters", conn2)
                                        Dim params As New DataTable
                                        adpt.Fill(params)

                                        If params.Rows(0).Item(0) = "Active" Then

                                        End If

                                        'tbl.Rows.Clear()
                                        'tbl2.Rows.Clear()

                                    End If


                                Catch ex As Exception
                                    MsgBox(ex.Message)
                                End Try
                                Try
                                    If IconButton3.Text = "Avec BL" Then

                                    End If


                                    If ComboBox2.Text <> "comptoir" AndAlso ComboBox2.Text <> "Comptoir" Then

                                        ReportToPrint = New LocalReport()
                                        ReportToPrint.ReportPath = Application.StartupPath + "\Report4.rdlc"
                                        ReportToPrint.DataSources.Clear()
                                        ReportToPrint.EnableExternalImages = True

                                        Dim clt As String

                                        If ComboBox2.Text = "" Then
                                            clt = "comptoir"
                                        Else
                                            clt = ComboBox2.Text
                                        End If

                                        Dim cliente As New ReportParameter("client", clt)
                                        Dim client1(0) As ReportParameter
                                        client1(0) = cliente
                                        ReportToPrint.SetParameters(client1)

                                        Dim ide As New ReportParameter("id", blnum)
                                        Dim id1(0) As ReportParameter
                                        id1(0) = ide
                                        ReportToPrint.SetParameters(id1)

                                        Dim vt As New ReportParameter("vente", id)
                                        Dim vt1(0) As ReportParameter
                                        vt1(0) = vt
                                        ReportToPrint.SetParameters(vt1)

                                        Dim daty As New ReportParameter("date", DateTime.Now.ToString("dd/MM/yyyy"))
                                        Dim daty1(0) As ReportParameter
                                        daty1(0) = daty
                                        ReportToPrint.SetParameters(daty1)

                                        Dim fraisN As New ReportParameter("FraisN", tfraisN)
                                        Dim fraisN1(0) As ReportParameter
                                        fraisN1(0) = fraisN
                                        ReportToPrint.SetParameters(fraisN1)

                                        Dim fraisM As New ReportParameter("FraisM", tfraisM)
                                        Dim fraisM1(0) As ReportParameter
                                        fraisM1(0) = fraisM
                                        ReportToPrint.SetParameters(fraisM1)

                                        Dim caissier As New ReportParameter("caissier", Label2.Text)
                                        Dim caissier1(0) As ReportParameter
                                        caissier1(0) = caissier
                                        ReportToPrint.SetParameters(caissier1)

                                        Dim total As New ReportParameter("total", Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") + Convert.ToDecimal(tfraisM.Replace(".", ","))).ToString("# ##0.00"))
                                        Dim total1(0) As ReportParameter
                                        total1(0) = total
                                        ReportToPrint.SetParameters(total1)

                                        Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
                                        Dim table23 As New DataTable
                                        adpt23.Fill(table23)

                                        Dim info As New ReportParameter("info", table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString & vbCrLf & table23.Rows(0).Item(6).ToString)
                                        Dim info1(0) As ReportParameter
                                        info1(0) = info
                                        ReportToPrint.SetParameters(info1)

                                        Dim livreur As New ReportParameter("livreur", Livr)
                                        Dim livreur1(0) As ReportParameter
                                        livreur1(0) = livreur
                                        ReportToPrint.SetParameters(livreur1)


                                        Dim count As New ReportParameter("count", DataGridView3.Rows.Count.ToString)
                                        Dim count1(0) As ReportParameter
                                        count1(0) = count
                                        ReportToPrint.SetParameters(count1)

                                        Dim qty As New ReportParameter("qty", sumqty.ToString)
                                        Dim qty1(0) As ReportParameter
                                        qty1(0) = qty
                                        ReportToPrint.SetParameters(qty1)

                                        Dim appPath As String = Application.StartupPath()

                                        Dim SaveDirectory As String = appPath & "\"
                                        Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                                        Dim img As New ReportParameter("img", "File:\" + SavePath, True)
                                        Dim img1(0) As ReportParameter
                                        img1(0) = img
                                        ReportToPrint.SetParameters(img1)

                                        ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

                                        'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
                                        'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                                        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
                                        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
                                        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
                                        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>5.83in</PageWidth><PageHeight>8.3in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
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
                                        Dim printname As String = a5printer
                                        printDoc.PrinterSettings.PrinterName = printname
                                        Dim ps As New PrinterSettings()
                                        ps.PrinterName = printDoc.PrinterSettings.PrinterName
                                        printDoc.PrinterSettings = ps

                                        AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
                                        m_currentPageIndex = 0
                                        printDoc.DefaultPageSettings.PaperSize = New PaperSize("A5", 583, 827)


                                        ' Vérifier et ajuster les marges par défaut de l'imprimante si nécessaire
                                        ps.DefaultPageSettings.Margins = New Margins(0, 0, 0, 0)
                                        printDoc.Print()

                                    End If

                                Catch ex As Exception
                                    MsgBox(ex.Message)
                                End Try
                                If tpevar = False Then
                                    DataGridView3.Rows.Clear()
                                    Label28.Text = "numbl"
                                    Label30.Text = 0
                                    Label32.Text = "Sans livraison"
                                End If

                                Dim adpt29 As New MySqlDataAdapter("select * from infos", conn2)
                                Dim table29 As New DataTable
                                adpt29.Fill(table29)
                                Dim gifttop As Decimal = table29.Rows(0).Item(24)
                                If Label6.Text.Replace(" ", "").Replace(" ", "") >= gifttop Then
                                    adpt = New MySqlDataAdapter("select * from gifts", conn2)
                                    Dim tablegift As New DataTable
                                    adpt.Fill(tablegift)
                                    If tablegift.Rows.Count() <> 0 Then
                                        gift.ShowDialog()

                                    End If

                                End If
                                If tpevar = False Then
                                    Label6.Text = 0.00

                                End If
                                Try
                                    If IsNumeric(TextBox3.Text) = False Then
                                        If Label9.Text = "Rendu :" Then
                                            Label4.Text = 0.00
                                        End If
                                        If Label9.Text = "Reste :" Then
                                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                                        End If
                                    Else
                                        If Label9.Text = "Rendu :" Then
                                            Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                                        End If
                                        If Label9.Text = "Reste :" Then
                                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                                        End If
                                    End If
                                Catch ex As MySqlException
                                    MsgBox(ex.Message)
                                End Try
                                Label23.Text = 0.ToString("# ##0.00")
                                Label24.Text = 0.ToString("# ##0.00")
                                Label25.Text = 0.ToString("# ##0.00")
                                TextBox21.Text = ""

                                Label7.Text = 0.ToString("# ##0.00")
                                Label4.Text = 0
                                TextBox1.Text = 0
                                TextBox3.Text = ""
                                TextBox8.Text = ""
                                ComboBox2.SelectedItem = "comptoir"
                                ComboBox1.SelectedItem = "Espèce"
                                IconButton31.BackColor = Color.DarkOrange
                                IconButton31.ForeColor = Color.White
                                ComboBox1.Text = "Espèce"
                                IconButton52.PerformClick()

                                IconButton32.BackColor = Color.White
                                IconButton32.ForeColor = Color.Black

                                IconButton33.BackColor = Color.White
                                IconButton33.ForeColor = Color.Black
                                TextBox2.Select()
                                TextBox2.Text = ""
                                Label18.Visible = False

                                adpt = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
                                Dim tabletick As New DataTable
                                adpt.Fill(tabletick)
                                Dim idlast As Integer
                                If tabletick.Rows().Count = 0 Then
                                    idlast = 1

                                Else
                                    idlast = tabletick.Rows(0).Item(0) + 1

                                End If
                                Label20.Text = "Ticket N° " & idlast.ToString
                                'IconButton24.PerformClick()
                                Casef = False
                                CheckBox2.Checked = True
                                If ComboBox1.Text = "Espèce" Then
                                    Dim codeOpenCashDrawer As Byte() = New Byte() {27, 112, 48, 55, 121}
                                    Dim rw As RawPrinterHelper = New RawPrinterHelper
                                    Dim pUnmanagedBytes As New IntPtr(0)
                                    pUnmanagedBytes = Marshal.AllocCoTaskMem(5)
                                    Dim printname As String = a5printer
                                    Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5)
                                    rw.SendBytesToPrinter(printname, pUnmanagedBytes, 5)
                                    Marshal.FreeCoTaskMem(pUnmanagedBytes)
                                End If
                            End If
                        End If
                    Else
                        MsgBox("Merci de choisir le mode de paiement")

                    End If
                End If

            Else
                MsgBox("Ce client a déjà atteind le maximum du crédit")
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        TextBox2.Select()
        TextBox2.Text = ""

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


    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        If DataGridView3.Rows.Count > 0 Then


            Dim x As Decimal
            x = DataGridView3.SelectedCells(0).RowIndex
            Dim qty As Decimal = DataGridView3.Rows(x).Cells(2).Value
            DataGridView3.Rows(x).Cells(2).Value = qty + 1

            Dim price As String
            Dim newpr As String

            price = DataGridView3.Rows(x).Cells(4).Value
            newpr = DataGridView3.Rows(x).Cells(2).Value * price
            DataGridView3.Rows(x).Cells(3).Value = newpr
            Dim sum As Decimal = 0
            For i = 0 To DataGridView3.Rows.Count - 1
                sum = sum + DataGridView3.Rows(i).Cells(3).Value
            Next
            Label6.Text = Convert.ToDecimal(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
            Try
                If IsNumeric(TextBox3.Text) = False Then
                    If Label9.Text = "Rendu :" Then
                        Label4.Text = 0.00
                    End If
                    If Label9.Text = "Reste :" Then
                        Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                    End If
                Else
                    If Label9.Text = "Rendu :" Then
                        Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                    End If
                    If Label9.Text = "Reste :" Then
                        Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                    End If
                End If
            Catch ex As MySqlException
                MsgBox(ex.Message)
            End Try
            Label7.Text = sum.ToString("# ##0.00")
        End If
        TextBox4.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""

    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        If DataGridView3.Rows.Count > 0 Then


            Dim x As Decimal

            x = DataGridView3.SelectedCells(0).RowIndex
            Dim qty As Decimal = DataGridView3.Rows(x).Cells(2).Value
            If DataGridView3.Rows(x).Cells(2).Value > 1 Then


                DataGridView3.Rows(x).Cells(2).Value = qty - 1

                Dim price As String
                Dim newpr As String

                price = DataGridView3.Rows(x).Cells(4).Value
                newpr = DataGridView3.Rows(x).Cells(2).Value * price
                DataGridView3.Rows(x).Cells(3).Value = newpr
                Dim sum As Decimal = 0
                For i = 0 To DataGridView3.Rows.Count - 1
                    sum = sum + DataGridView3.Rows(i).Cells(3).Value
                Next
                Label6.Text = Convert.ToDecimal(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                Try
                    If IsNumeric(TextBox3.Text) = False Then
                        If Label9.Text = "Rendu :" Then
                            Label4.Text = 0.00
                        End If
                        If Label9.Text = "Reste :" Then
                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                        End If
                    Else
                        If Label9.Text = "Rendu :" Then
                            Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                        End If
                        If Label9.Text = "Reste :" Then
                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                        End If
                    End If
                Catch ex As MySqlException
                    MsgBox(ex.Message)
                End Try
                Label7.Text = sum.ToString("# ##0.00")

            End If
        End If
        TextBox4.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""

    End Sub

    Dim idco As String
    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            'IconButton3.Text = ""
            'Panel4.Controls.Clear()
            'Panel4.Visible = True

            Dim codebar As String
            If TextBox2.Text.Length.ToString >= 3 AndAlso IsNumeric(TextBox2.Text) Then
                If TextBox2.Text.Chars(0) & TextBox2.Text.Chars(1) & TextBox2.Text.Chars(2) = 260 Then
                    codebar = 260 & TextBox2.Text.Chars(3) & TextBox2.Text.Chars(4) & TextBox2.Text.Chars(5) & TextBox2.Text.Chars(6)
                Else
                    codebar = TextBox2.Text
                End If
            Else
                codebar = TextBox2.Text
            End If


            Try


                adpt = New MySqlDataAdapter("select prod_code from code_supp where code_supp = '" & codebar & "' ", conn2)
                Dim table2 As New DataTable
                adpt.Fill(table2)
                If table2.Rows.Count() = 0 Then
                    adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_HT,pv_gros,PV_HT,TVA,Stock from article where Code = '" & codebar & "' and mask = '0'", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    If table.Rows.Count() <> 0 Then
                        Dim rowindex As String
                        Dim price As Decimal
                        Dim pu As Decimal
                        Dim qty As Decimal = 1
                        If ComboBox4.Text = "Détails" Then
                            pu = Convert.ToString(table.Rows(0).Item(2)).Replace(".", ",")
                            If TextBox2.Text.Length.ToString >= 3 Then
                                If Convert.ToString(TextBox2.Text).Chars(0) & Convert.ToString(TextBox2.Text).Chars(1) & Convert.ToString(TextBox2.Text).Chars(2) = "260" And TextBox2.Text.Length.ToString > 7 Then
                                    price = Convert.ToDecimal(TextBox2.Text.Chars(7) & TextBox2.Text.Chars(8) & TextBox2.Text.Chars(9) & "," & TextBox2.Text.Chars(10) & TextBox2.Text.Chars(11))
                                    qty = Convert.ToDecimal(price) / Convert.ToDecimal(pu)
                                Else
                                    price = Convert.ToString(table.Rows(0).Item(2)).Replace(".", ",")
                                    qty = 1
                                End If

                            Else
                                price = Convert.ToString(table.Rows(0).Item(2)).Replace(".", ",")
                                qty = 1
                            End If
                        Else
                            pu = Convert.ToString(table.Rows(0).Item(4)).Replace(".", ",")
                            price = Convert.ToString(table.Rows(0).Item(4)).Replace(".", ",")
                            qty = 1
                        End If
                        adpt = New MySqlDataAdapter("select stock_negatif from parameters", conn2)
                        Dim params As New DataTable
                        adpt.Fill(params)
                        If params.Rows(0).Item(0) = "Active" Then
                            DataGridView3.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1).ToString.Replace(".", ","), qty.ToString("# ##0.00").Replace(".", ","), pu.ToString("# ##0.00").Replace(".", ","), table.Rows(0).Item(0).ToString.Replace(".", ","), Convert.ToString(table.Rows(0).Item(3)).Replace(".", ","), price.ToString("# ##0.00").Replace(".", ","), "", 0, 0, Convert.ToString(table.Rows(0).Item(5)).Replace(".", ","), Convert.ToString(Val(Convert.ToDecimal(table.Rows(0).Item(6)).ToString.Replace(".", ",")) / 100).Replace(".", ","))
                        Else
                            If Convert.ToDouble(table.Rows(0).Item(7).ToString.Replace(".", ",").Replace(" ", "")) <= 0 Then
                                MsgBox("Cet article est épuisé, Stock négatif !")
                            Else
                                DataGridView3.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1).ToString.Replace(".", ","), qty.ToString("# ##0.00").Replace(".", ","), pu.ToString("# ##0.00").Replace(".", ","), table.Rows(0).Item(0).ToString.Replace(".", ","), Convert.ToString(table.Rows(0).Item(3)).Replace(".", ","), price.ToString("# ##0.00").Replace(".", ","), "", 0, 0, Convert.ToString(table.Rows(0).Item(5)).Replace(".", ","), Convert.ToString(Val(Convert.ToDecimal(table.Rows(0).Item(6)).ToString.Replace(".", ",")) / 100).Replace(".", ","))
                            End If
                        End If

                        If DataGridView3.Rows.Count <> 0 Then

                            DataGridView3.ClearSelection()

                            'Scroll to the last row.
                            Me.DataGridView3.FirstDisplayedScrollingRowIndex = Me.DataGridView3.RowCount - 1

                            'Select the last row.
                            Me.DataGridView3.Rows(Me.DataGridView3.RowCount - 1).Selected = True

                            Dim sum As Decimal = 0
                            Dim sum2 As Decimal = 0
                            Dim sum3 As Decimal = 0
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

                            For i = 0 To DataGridView3.Rows.Count - 1
                                sum = sum + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", ""))

                                prixHT = DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                                quantite = DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                                tauxTVA = DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                                pourcentageRemise = DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                                ' Étape 1 : Calculer le montant HT après la remise
                                montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

                                ' Étape 2 : Calculer le montant TTC avant TVA
                                montantTTCAvantTVA = montantHTApresRemise * quantite

                                ' Étape 3 : Calculer le montant de la TVA
                                montantTVA = montantTTCAvantTVA * tauxTVA

                                sum2 += montantTTCAvantTVA
                                sum3 += montantTVA


                                DataGridView3.Rows(i).Cells(7).Value = i
                            Next
                            Label23.Text = sum2
                            Label24.Text = sum - sum2
                            Label25.Text = Label23.Text

                            Label6.Text = Convert.ToDecimal(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00").Replace(" ", "")
                            Try
                                If IsNumeric(TextBox3.Text) = False Then
                                    If Label9.Text = "Rendu :" Then
                                        Label4.Text = 0.00
                                    End If
                                    If Label9.Text = "Reste :" Then
                                        Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                                    End If
                                Else
                                    If Label9.Text = "Rendu :" Then
                                        Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                                    End If
                                    If Label9.Text = "Reste :" Then
                                        Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                                    End If
                                End If
                            Catch ex As MySqlException
                                MsgBox(ex.Message)
                            End Try
                            Label7.Text = sum.ToString("# ##0.00").Replace(" ", "")

                            Dim totalpanier As Double = 0
                            For i = 0 To DataGridView3.Rows.Count - 1
                                totalpanier += Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ","))
                            Next
                            IconButton17.Text = totalpanier
                        End If
                        TextBox2.Text = ""

                        Else
                            soundPlayer.Play()
                        MsgBox("Produit introuvable !")
                        TextBox2.Text = ""
                    End If
                Else
                    adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_HT,pv_gros,PV_HT, TVA,Stock from article where Code = '" + table2.Rows(0).Item(0) + "' and mask = '0'", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    If table.Rows.Count() <> 0 Then
                        Dim rowindex As String
                        Dim price As Decimal
                        Dim pu As Decimal
                        Dim qty As Decimal = 1
                        If ComboBox4.Text = "Détails" Then
                            pu = Convert.ToString(table.Rows(0).Item(2)).Replace(".", ",")
                            If TextBox2.Text.Length.ToString >= 3 Then
                                If Convert.ToString(TextBox2.Text).Chars(0) & Convert.ToString(TextBox2.Text).Chars(1) & Convert.ToString(TextBox2.Text).Chars(2) = "260" And TextBox2.Text.Length.ToString > 7 Then
                                    price = Convert.ToDecimal(TextBox2.Text.Chars(7) & TextBox2.Text.Chars(8) & TextBox2.Text.Chars(9) & "," & TextBox2.Text.Chars(10) & TextBox2.Text.Chars(11))
                                    qty = Convert.ToDecimal(price) / Convert.ToDecimal(pu)
                                Else
                                    price = Convert.ToString(table.Rows(0).Item(2)).Replace(".", ",")
                                    qty = 1
                                End If

                            Else
                                price = Convert.ToString(table.Rows(0).Item(2)).Replace(".", ",")
                                qty = 1
                            End If
                        Else
                            pu = Convert.ToString(table.Rows(0).Item(4)).Replace(".", ",")
                            price = Convert.ToString(table.Rows(0).Item(4)).Replace(".", ",")
                            qty = 1
                        End If

                        adpt = New MySqlDataAdapter("select stock_negatif from parameters", conn2)
                        Dim params As New DataTable
                        adpt.Fill(params)
                        If params.Rows(0).Item(0) = "Active" Then
                            DataGridView3.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1), qty, pu, table.Rows(0).Item(0), Convert.ToString(table.Rows(0).Item(3)).Replace(".", ","), price, "", 0, 0, Convert.ToString(table.Rows(0).Item(5)).Replace(".", ","), Convert.ToString(Val(table.Rows(0).Item(6)) / 100).Replace(".", ","))
                        Else
                            If Convert.ToDouble(table.Rows(0).Item(7).ToString.Replace(".", ",").Replace(" ", "")) <= 0 Then
                                MsgBox("Cet article est épuisé, Stock négatif !")
                            Else
                                DataGridView3.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1), qty, pu, table.Rows(0).Item(0), Convert.ToString(table.Rows(0).Item(3)).Replace(".", ","), price, "", 0, 0, Convert.ToString(table.Rows(0).Item(5)).Replace(".", ","), Convert.ToString(Val(table.Rows(0).Item(6)) / 100).Replace(".", ","))
                            End If
                        End If
                        If DataGridView3.Rows.Count <> 0 Then
                            'Scroll to the last row.
                            DataGridView3.ClearSelection()

                            Me.DataGridView3.FirstDisplayedScrollingRowIndex = Me.DataGridView3.RowCount - 1

                            'Select the last row.
                            Me.DataGridView3.Rows(Me.DataGridView3.RowCount - 1).Selected = True


                            Dim sum As Decimal = 0
                            Dim sum2 As Decimal = 0
                            Dim sum3 As Decimal = 0
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


                            For i = 0 To DataGridView3.Rows.Count - 1
                                sum = sum + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", ""))

                                prixHT = DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                                quantite = DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                                tauxTVA = DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                                pourcentageRemise = DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                                ' Étape 1 : Calculer le montant HT après la remise
                                montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

                                ' Étape 2 : Calculer le montant TTC avant TVA
                                montantTTCAvantTVA = montantHTApresRemise * quantite

                                ' Étape 3 : Calculer le montant de la TVA
                                montantTVA = montantTTCAvantTVA * tauxTVA

                                sum2 += montantTTCAvantTVA
                                sum3 += montantTVA


                                DataGridView3.Rows(i).Cells(7).Value = i
                            Next
                            Label23.Text = sum2
                            Label24.Text = sum - sum2
                            Label25.Text = Label23.Text

                            Label6.Text = Convert.ToDecimal(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00").Replace(" ", "")
                            Try
                                If IsNumeric(TextBox3.Text) = False Then
                                    If Label9.Text = "Rendu :" Then
                                        Label4.Text = 0.00
                                    End If
                                    If Label9.Text = "Reste :" Then
                                        Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                                    End If
                                Else
                                    If Label9.Text = "Rendu :" Then
                                        Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                                    End If
                                    If Label9.Text = "Reste :" Then
                                        Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                                    End If
                                End If
                            Catch ex As MySqlException
                                MsgBox(ex.Message)
                            End Try
                            Label7.Text = sum.ToString("# ##0.00").Replace(" ", "")

                            Dim totalpanier As Double = 0
                        For i = 0 To DataGridView3.Rows.Count - 1
                            totalpanier += Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ","))
                        Next
                            IconButton17.Text = totalpanier

                        End If
                        TextBox2.Text = ""

                    Else
                        soundPlayer.Play()
                        MsgBox("Produit introuvable !")
                        TextBox2.Text = ""
                    End If


                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            TextBox2.Select()
        End If

        If e.Control AndAlso e.KeyCode = Keys.Space Then
            TextBox2.Text = ""
            TextBox3.Select()
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        If Label28.Text = "numbl" Then

            Me.Close()
        Else
            MsgBox("Merci de Continuer la modification du bon avant de vider la caisse ou la fermer !!")


        End If


    End Sub

    Private Sub IconButton8_Click_1(sender As Object, e As EventArgs) Handles IconButton8.Click
        If Label28.Text = "numbl" Then

            Dim Rep As Integer
            Rep = MsgBox("Voulez-vous vraiment quitter ?", vbYesNo)
            If Rep = vbYes Then
                Dim log = New Form1()
                log.Show()
                Me.Close()
            End If
        Else
            MsgBox("Merci de Continuer la modification du bon avant de vider la caisse ou la fermer !!")


        End If

        TextBox2.Select()
        TextBox2.Text = ""

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        If TextBox1.Text = "" Then
            Label6.Text = Label7.Text.ToString("# ##0.00")
        Else
            Label6.Text = (Label7.Text - (Label7.Text * TextBox1.Text / 100)).ToString("# ##0.00")
        End If
        Try
            If IsNumeric(TextBox3.Text) = False Then
                If Label9.Text = "Rendu :" Then
                    Label4.Text = 0.00
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
            Else
                If Label9.Text = "Rendu :" Then
                    Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                End If
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs)
        If TextBox1.Text = 0 Then
            TextBox1.Text = ""
        Else

        End If

    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs)
        If TextBox1.Text = "" Then
            TextBox1.Text = 0
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        ' Utilisation de Regex pour supprimer les caractères non alphabétiques et non numériques
        TextBox2.Text = System.Text.RegularExpressions.Regex.Replace(TextBox2.Text, "[^a-zA-Z0-9]", "")
        ' Déplacez le curseur à la fin du texte
        TextBox2.SelectionStart = TextBox2.TextLength
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Try
            If IsNumeric(TextBox3.Text) = False Then
                If Label9.Text = "Rendu :" Then
                    Label4.Text = 0.00
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
            Else
                If Label9.Text = "Rendu :" Then
                    Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                End If
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)


    End Sub

    Private Sub IconButton11_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub IconButton11_Click_1(sender As Object, e As EventArgs) Handles IconButton11.Click
        If My.Computer.Network.IsAvailable Then
            Synchronisation.Show()
        Else
            MsgBox("Vérifier votre connexion internet !")

        End If
    End Sub

    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox4.Text.Trim() <> "" Then
                Try
                    Dim query As String = "SELECT Code, Article, PV_TTC, pv_gros, Stock FROM article WHERE Article LIKE @searchKeyword and mask = '0'"

                    Using conn2
                        conn2.Close()
                        conn2.Open()

                        Using cmd As New MySqlCommand(query, conn2)
                            ' Paramètres de la requête
                            cmd.Parameters.AddWithValue("@searchKeyword", "%" & TextBox4.Text.Trim() & "%")

                            Dim table As New DataTable
                            Dim adapter As New MySqlDataAdapter(cmd)
                            adapter.Fill(table)

                            If table.Rows.Count > 0 Then
                                DataGridView1.Rows.Clear()
                                Dim rowsToAdd As New List(Of DataGridViewRow)
                                For i = 0 To table.Rows.Count - 1
                                    Dim row As New DataGridViewRow()

                                    Dim first As New DataGridViewTextBoxCell()
                                    Dim second As New DataGridViewTextBoxCell()
                                    Dim third As New DataGridViewTextBoxCell()
                                    Dim cell1 As New DataGridViewTextBoxCell()
                                    Dim cell2 As New DataGridViewTextBoxCell()
                                    first.Value = table.Rows(i).Item(0)
                                    second.Value = table.Rows(i).Item(1)
                                    third.Value = table.Rows(i).Item(2)
                                    cell1.Value = table.Rows(i).Item(3)
                                    cell2.Value = table.Rows(i).Item(4)

                                    row.Cells.Add(first)
                                    row.Cells.Add(second)
                                    row.Cells.Add(third)
                                    row.Cells.Add(cell1)
                                    row.Cells.Add(cell2)

                                    rowsToAdd.Add(row)

                                Next
                                DataGridView1.Rows.Clear()
                                DataGridView1.Rows.AddRange(rowsToAdd.ToArray())

                            Else
                                DataGridView1.Rows.Clear()
                                MessageBox.Show("Aucun résultat trouvé.")
                                TextBox4.Focus()

                            End If
                        End Using
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Une erreur s'est produite lors de la recherche : " & ex.Message)
                End Try
            Else
                DataGridView1.Rows.Clear()
            End If
            TextBox4.Select()

        End If

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        adpt = New MySqlDataAdapter("select Code, Article, PV_TTC, PA_HT,pv_gros,PV_HT,TVA,Stock from article where Code = '" + DataGridView1.CurrentRow.Cells(0).Value + "' and mask = '0'", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        Dim rowindex As String
        Dim found As Boolean = False
        Dim price As Decimal
        If ComboBox4.Text = "Détails" Then
            price = Convert.ToString(table.Rows(0).Item(2)).Replace(".", ",")
        Else
            price = Convert.ToString(table.Rows(0).Item(4)).Replace(".", ",")
        End If
        For Each row As DataGridViewRow In DataGridView3.Rows
            If row.Cells.Item(4).Value = table.Rows(0).Item(0) Then
                rowindex = row.Index.ToString()
                found = True
                row.Cells.Item(2).Value = row.Cells.Item(2).Value + 1
                row.Cells.Item(6).Value = row.Cells.Item(3).Value * row.Cells.Item(2).Value
                Exit For
            End If
        Next
        If Not found Then
            adpt = New MySqlDataAdapter("select stock_negatif from parameters", conn2)
            Dim params As New DataTable
            adpt.Fill(params)
            If params.Rows(0).Item(0) = "Active" Then
                DataGridView3.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1), 1, price, table.Rows(0).Item(0), Convert.ToString(table.Rows(0).Item(3)).Replace(".", ","), price, "", 0, 0, Convert.ToString(table.Rows(0).Item(5)).Replace(".", ","), Convert.ToString(Val(table.Rows(0).Item(6)) / 100).Replace(".", ","))
            Else
                If Convert.ToDouble(table.Rows(0).Item(7).ToString.Replace(".", ",").Replace(" ", "")) <= 0 Then
                    MsgBox("Cet article est épuisé, Stock négatif !")
                Else
                    DataGridView3.Rows.Add(table.Rows(0).Item(0), table.Rows(0).Item(1), 1, price, table.Rows(0).Item(0), Convert.ToString(table.Rows(0).Item(3)).Replace(".", ","), price, "", 0, 0, Convert.ToString(table.Rows(0).Item(5)).Replace(".", ","), Convert.ToString(Val(table.Rows(0).Item(6)) / 100).Replace(".", ","))

                End If
            End If

            DataGridView3.ClearSelection()

            'Scroll to the last row.
            Me.DataGridView3.FirstDisplayedScrollingRowIndex = Me.DataGridView3.RowCount - 1

            'Select the last row.
            Me.DataGridView3.Rows(Me.DataGridView3.RowCount - 1).Selected = True

        End If
        If DataGridView3.Rows.Count <> 0 Then
            Dim sum As Decimal = 0
            Dim sum2 As Decimal = 0
            Dim sum3 As Decimal = 0
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

            For i = 0 To DataGridView3.Rows.Count - 1
                sum = sum + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", ""))

                prixHT = DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                quantite = DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                tauxTVA = DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                pourcentageRemise = DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                ' Étape 1 : Calculer le montant HT après la remise
                montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

                ' Étape 2 : Calculer le montant TTC avant TVA
                montantTTCAvantTVA = montantHTApresRemise * quantite

                ' Étape 3 : Calculer le montant de la TVA
                montantTVA = montantTTCAvantTVA * tauxTVA

                sum2 += montantTTCAvantTVA
                sum3 += montantTVA


                DataGridView3.Rows(i).Cells(7).Value = i
            Next
            Label23.Text = sum2
            Label24.Text = sum - sum2
            Label25.Text = Label23.Text

            Label6.Text = Convert.ToDecimal(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
            Try
                If IsNumeric(TextBox3.Text) = False Then
                    If Label9.Text = "Rendu :" Then
                        Label4.Text = 0.00
                    End If
                    If Label9.Text = "Reste :" Then
                        Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                    End If
                Else
                    If Label9.Text = "Rendu :" Then
                        Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                    End If
                    If Label9.Text = "Reste :" Then
                        Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                    End If
                End If
            Catch ex As MySqlException
                MsgBox(ex.Message)
            End Try
            Label7.Text = sum.ToString("# ##0.00").Replace(" ", "")

            Dim totalpanier As Double = 0
            For i = 0 To DataGridView3.Rows.Count - 1
                totalpanier += Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ","))
            Next
            IconButton17.Text = totalpanier
        End If
        TextBox4.Text = ""
        TextBox4.Select()
        Panel7.Visible = False
        TextBox2.Select()
    End Sub
    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click

        If TextBox4.Text.Trim() <> "" Then
            Try
                Dim query As String = "SELECT Code, Article, PV_TTC, pv_gros, Stock FROM article WHERE Article LIKE @searchKeyword and mask = '0'"

                Using conn2
                    conn2.Close()
                    conn2.Open()

                    Using cmd As New MySqlCommand(query, conn2)
                        ' Paramètres de la requête
                        cmd.Parameters.AddWithValue("@searchKeyword", "%" & TextBox4.Text.Trim() & "%")

                        Dim table As New DataTable
                        Dim adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(table)

                        If table.Rows.Count > 0 Then
                            DataGridView1.Rows.Clear()
                            Dim rowsToAdd As New List(Of DataGridViewRow)
                            For i = 0 To table.Rows.Count - 1
                                Dim row As New DataGridViewRow()

                                Dim first As New DataGridViewTextBoxCell()
                                Dim second As New DataGridViewTextBoxCell()
                                Dim third As New DataGridViewTextBoxCell()
                                Dim cell1 As New DataGridViewTextBoxCell()
                                Dim cell2 As New DataGridViewTextBoxCell()
                                first.Value = table.Rows(i).Item(0)
                                second.Value = table.Rows(i).Item(1)
                                third.Value = table.Rows(i).Item(2)
                                cell1.Value = table.Rows(i).Item(3)
                                cell2.Value = table.Rows(i).Item(4)

                                row.Cells.Add(first)
                                row.Cells.Add(second)
                                row.Cells.Add(third)
                                row.Cells.Add(cell1)
                                row.Cells.Add(cell2)

                                rowsToAdd.Add(row)

                            Next
                            DataGridView1.Rows.Clear()
                            DataGridView1.Rows.AddRange(rowsToAdd.ToArray())

                        Else
                            DataGridView1.Rows.Clear()
                            MessageBox.Show("Aucun résultat trouvé.")
                            TextBox4.Focus()
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Une erreur s'est produite lors de la recherche : " & ex.Message)
            End Try
        Else
            DataGridView1.Rows.Clear()
        End If
        TextBox4.Select()
    End Sub

    Private Sub IconButton2_Click_1(sender As Object, e As EventArgs) Handles IconButton2.Click
        'Dim newfrm As New dashboard
        'newfrm.Show()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "Crédit" Then
            Label9.Text = "Reste :"
            Label4.Text = Label6.Text
            TextBox3.Text = ""
        Else
            Label9.Text = "Rendu :"
            Label4.Text = 0
        End If
        TextBox4.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub
    Dim id As Integer

    <Obsolete>
    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        If IconButton3.BackColor = Color.DodgerBlue Then
            IconButton3.BackColor = Color.DimGray
            IconButton3.Text = "Sans BL"
        Else
            IconButton3.BackColor = Color.DodgerBlue
            IconButton3.Text = "Avec BL"
        End If
        TextBox4.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton10_Click(sender As Object, e As EventArgs) Handles IconButton10.Click
        adpt = New MySqlDataAdapter("select OrderID from devis order by OrderID desc", conn2)
        Dim table4 As New DataTable
        adpt.Fill(table4)

        If table4.Rows.Count <> 0 Then
            id = Val(table4.Rows(0).Item(0)) + 1
        Else
            id = 1
        End If

        depense.Show()
        Dim sumttc As Decimal
        Dim sumht As Decimal
        For i = 0 To DataGridView3.Rows.Count - 1
            adpt = New MySqlDataAdapter("select Article,unit,TVA from article where Code = '" + DataGridView3.Rows(i).Cells(4).Value.ToString + "' and mask = '0' ", conn2)
            Dim table As New DataTable
            adpt.Fill(table)

            Dim puht As Decimal = DataGridView3.Rows(i).Cells(3).Value / (1 + (table.Rows(0).Item(2) / 100))
            Dim puttc As Decimal = DataGridView3.Rows(i).Cells(3).Value
            Dim totht As Decimal = puht * DataGridView3.Rows(i).Cells(2).Value
            Dim totttc As Decimal = DataGridView3.Rows(i).Cells(6).Value

            depense.DataGridView1.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, table.Rows(0).Item(0), table.Rows(0).Item(1), DataGridView3.Rows(i).Cells(2).Value, Math.Round(puht, 2), Math.Round(puttc, 2), Math.Round(totht, 2), Math.Round(totttc, 2), "TVA " + table.Rows(0).Item(2) + "%")

            sumttc += totttc
            sumht += totht

        Next

        depense.Label15.Text = Math.Round(sumttc, 2)
        depense.Label9.Text = Math.Round(sumht, 2)
        depense.Label10.Text = Math.Round(sumttc - sumht, 2)

        adpt = New MySqlDataAdapter("select client,adresse,ville,ICE from clients where client = '" + ComboBox2.Text.ToString + "' and masq = 'non' ", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)

        depense.Label7.Text = table2.Rows(0).Item(0)
        depense.Label8.Text = table2.Rows(0).Item(1)
        depense.Label12.Text = table2.Rows(0).Item(2)
        depense.Label2.Text = table2.Rows(0).Item(3)
        depense.Label16.Text = "Devis :"
        depense.IconButton1.Visible = False
        depense.IconButton2.Visible = True

        depense.TextBox1.Text = "Devis N° " + id.ToString
        depense.Label6.Text = DateTime.Now.ToString("dd/MM/yyyy")
        TextBox2.Select()
    End Sub

    Private Sub IconButton13_Click(sender As Object, e As EventArgs) Handles IconButton13.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Ajouter Client'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                newClient = True
                ComboBox2.Visible = False
                TextBox5.Visible = True
                TextBox23.Visible = True
                Label59.Visible = True
                TextBox5.Text = ""
                TextBox23.Text = ""
                IconButton14.Visible = True
                IconButton15.Visible = True
                IconButton13.Visible = False
                IconButton72.Visible = False
                TextBox5.Select()
                TextBox4.Text = ""
                TextBox2.Select()
                TextBox2.Text = ""
            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If

    End Sub

    Private Sub IconButton15_Click(sender As Object, e As EventArgs) Handles IconButton15.Click
        newClient = False
        ComboBox2.Visible = True
        adpt = New MySqlDataAdapter("select client from clients where masq = 'non' order by client asc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        If table.Rows.Count <> 0 Then
            ComboBox2.Items.Clear()
            ComboBox6.Items.Clear()
            For i = 0 To table.Rows.Count - 1
                ComboBox2.Items.Add(table.Rows(i).Item(0))
                ComboBox6.Items.Add(table.Rows(i).Item(0))
            Next
        End If
        ComboBox2.SelectedItem = "comptoir"
        ComboBox6.SelectedItem = "comptoir"
        TextBox5.Visible = False
        TextBox23.Visible = False
        Label59.Visible = False
        TextBox8.Text = ""
        TextBox23.Text = ""
        IconButton14.Visible = False
        IconButton15.Visible = False
        IconButton13.Visible = True
        TextBox4.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton14_Click(sender As Object, e As EventArgs) Handles IconButton14.Click
        Try
            conn2.Close()
            conn2.Open()

            If IconButton72.Visible = False Then

                Dim sql2 As String
                Dim cmd2 As MySqlCommand
                sql2 = "INSERT INTO clients (client, ICE) VALUES ('" & TextBox5.Text.Replace("'", "") & "','" & TextBox23.Text & "')"
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.ExecuteNonQuery()
                IconButton72.Visible = True
            Else

                Dim sql2 As String
                Dim cmd2 As MySqlCommand
                sql2 = "UPDATE `clients` SET `ICE`='" & TextBox23.Text & "' where client = '" & ComboBox2.Text & "'"
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.ExecuteNonQuery()
            End If

            conn2.Close()

            adpt = New MySqlDataAdapter("select client from clients where masq = 'non' order by client asc", conn2)
            Dim table As New DataTable
            adpt.Fill(table)
            If table.Rows.Count <> 0 Then
                ComboBox2.Items.Clear()
                For i = 0 To table.Rows.Count - 1
                    ComboBox2.Items.Add(table.Rows(i).Item(0))
                Next
                ComboBox2.SelectedItem = TextBox5.Text.Replace("'", "")

            End If

            ComboBox2.Visible = True
            TextBox5.Visible = False
            TextBox23.Visible = False
            Label59.Visible = False
            IconButton14.Visible = False
            IconButton13.Visible = True
            IconButton15.Visible = False
            TextBox4.Text = ""
            TextBox2.Select()
            TextBox2.Text = ""
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IconButton16_Click(sender As Object, e As EventArgs) Handles IconButton16.Click
        If IconButton16.BackColor = Color.ForestGreen Then
            CheckBox2.Checked = False
            IconButton16.BackColor = Color.DimGray
        Else
            CheckBox2.Checked = True
            IconButton16.BackColor = Color.ForestGreen
        End If
        TextBox4.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        If TextBox6.Text = "" Then
            Label6.Text = Convert.ToDecimal(Label7.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
        Else
            Label6.Text = Convert.ToDecimal(Label7.Text.Replace(".", ",").Replace(" ", "") - (Label7.Text.Replace(".", ",").Replace(" ", "") * TextBox6.Text.Replace(".", ",") / 100)).ToString("# ##0.00")
        End If
        Try
            If IsNumeric(TextBox3.Text) = False Then
                If Label9.Text = "Rendu :" Then
                    Label4.Text = 0.00
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
            Else
                If Label9.Text = "Rendu :" Then
                    Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                End If
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IconButton19_Click(sender As Object, e As EventArgs) Handles IconButton19.Click
        Panel15.Visible = False
        Try
            adpt = New MySqlDataAdapter("select * from orders WHERE parked = @day and ServeurRef = @caissier", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@day", "yes")
            adpt.SelectCommand.Parameters.AddWithValue("@caissier", Me.Label2.Text)

            Dim table3 As New DataTable
            adpt.Fill(table3)

            If table3.Rows.Count() < 5 Then

                adpt = New MySqlDataAdapter("select count(*) from `orders` WHERE `parked` = 'yes' and `ServeurRef` = '" & Me.Label2.Text & "'", conn2)
                Dim table2 As New DataTable
                adpt.Fill(table2)
                If DataGridView3.Rows.Count() > 0 Then

                    Dim cmd As MySqlCommand
                    Dim cmd2, cmd3 As MySqlCommand
                    Dim sql As String
                    Dim sql2, sql3 As String
                    Dim execution As Integer



                    adpt = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    Dim rms As String
                    If TextBox1.Text = "" Then
                        rms = 0
                    Else
                        rms = Label7.Text.Replace(" ", "") * TextBox1.Text / 100

                    End If


                    conn2.Close()
                    conn2.Open()
                    Dim id As Integer
                    If table.Rows().Count = 0 Then
                        id = 1

                    Else
                        id = table.Rows(0).Item(0) + 1

                    End If
                    Dim remiseprc As Decimal = 0

                    If TextBox21.Text <> "" Then
                        remiseprc = TextBox21.Text
                    End If
                    If Label28.Text = "numbl" Then
                    Else

                        id = Label28.Text
                    End If
                    If ComboBox1.Text = "TPE" Then
                        adpt = New MySqlDataAdapter("SELECT organisme FROM `banques` WHERE `tpe` = '1'", conn2)
                        Dim tabletpe As New DataTable
                        adpt.Fill(tabletpe)

                        sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `tick`,`bq`,`montant`,`client`) 
                    VALUES ('tpe','" + TextBox10.Text + "','tpe','" + TextBox9.Text + "','tpe', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" & id & "','" + tabletpe.Rows(0).Item(0) + "','" + Convert.ToDecimal(Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + tfraisM).ToString("# ##0.00") + "','" & ComboBox2.Text & "')"
                        cmd3 = New MySqlCommand(sql3, conn2)
                        cmd3.Parameters.Clear()
                        cmd3.ExecuteNonQuery()

                    End If

                    cmd = New MySqlCommand("DELETE FROM `orderdetails` WHERE `OrderID` = '" & id & "' ", conn2)
                    cmd.ExecuteNonQuery()
                    cmd = New MySqlCommand("DELETE FROM `orders` WHERE `OrderID` = '" & id & "' ", conn2)
                    cmd.ExecuteNonQuery()
                    cmd = New MySqlCommand("DELETE FROM `cheques` WHERE `tick` = '" & id & "' ", conn2)
                    cmd.ExecuteNonQuery()

                    sql2 = "INSERT INTO orders (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`,`parked`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','0','" + Label4.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + ComboBox2.Text + "','0','" + tfraisM + "','" & Livr & "','yes')"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.Parameters.Clear()
                    cmd2.ExecuteNonQuery()

                    sql2 = "INSERT INTO orderssauv (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`,`parked`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','0','" + Label4.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + ComboBox2.Text + "','0','" + tfraisM + "','" & Livr & "','yes')"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.Parameters.Clear()
                    cmd2.ExecuteNonQuery()

                    idasync = id

                    For Each dr As DataGridViewRow In Me.DataGridView3.Rows
                        conn2.Close()
                        conn2.Open()
                        Try
                            adpt = New MySqlDataAdapter("select * from article where Code = '" + dr.Cells(4).Value.ToString + "' and mask = '0'", conn2)
                            Dim tabl As New DataTable
                            adpt.Fill(tabl)
                            Dim marg As Decimal = 0
                            If tabl.Rows.Count <> 0 Then
                                If Label28.Text = "numbl" Then
                                    stck = Convert.ToDecimal(tabl.Rows(0).Item(8)) - Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                Else
                                    If Convert.ToDecimal(dr.Cells(2).Value.ToString.Replace(" ", "")) < 0 Then
                                        stck = Convert.ToDecimal(tabl.Rows(0).Item(8)) + Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                    Else
                                        stck = Convert.ToDecimal(tabl.Rows(0).Item(8)) - Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                    End If
                                End If
                                marg = (((Convert.ToDecimal(dr.Cells(10).Value.ToString.Replace(".", ",")) - (Convert.ToDecimal(dr.Cells(10).Value.ToString.Replace(".", ",")) * (Convert.ToDecimal(dr.Cells(8).Value.ToString.Replace(".", ",")) / 100))) - Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")))) * (Convert.ToDecimal(dr.Cells(2).Value.ToString.Replace(".", ",")) - Convert.ToDecimal(dr.Cells(9).Value.ToString.Replace(".", ",")))
                                If marg <= 0 Then
                                    marg = 0
                                End If
                            End If
                            cmd = New MySqlCommand
                            prod = dr.Cells(5).Value
                            sql = “INSERT INTO orderdetails (OrderID ,ProductID ,Price,Quantity,Total,date,pa,type,name,marge,rms,gr,pv,tva) VALUES (@tick, @code, @price, @qty, @montant, @date,@pa,@type,@name,@mrg,@rms,@gr,@pv,@tva);”
                            sql2 = “INSERT INTO orderdetailssauv (OrderID ,ProductID ,Price,Quantity,Total,date,pa,type,name,marge,rms,gr,pv,tva) VALUES (@tick, @code, @price, @qty, @montant, @date,@pa,@type,@name,@mrg,@rms,@gr,@pv,@tva);”

                            With cmd
                                .Connection = conn2
                                .CommandText = sql
                                .Parameters.Clear()
                                .Parameters.AddWithValue(“@tick”, id)

                                .Parameters.AddWithValue(“@code”, dr.Cells(4).Value)
                                .Parameters.AddWithValue(“@price”, Convert.ToDecimal(dr.Cells(3).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                .Parameters.AddWithValue(“@qty”, Convert.ToDecimal(dr.Cells(2).Value).ToString("# ##0.00"))
                                .Parameters.AddWithValue(“@montant”, Convert.ToDecimal(dr.Cells(6).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                .Parameters.AddWithValue(“@date”, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                .Parameters.AddWithValue(“@pa”, Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                .Parameters.AddWithValue(“@type”, "ventes")
                                .Parameters.AddWithValue(“@name”, dr.Cells(1).Value)
                                .Parameters.AddWithValue(“@mrg”, Convert.ToDecimal(marg.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                .Parameters.AddWithValue(“@rms”, dr.Cells(8).Value)
                                .Parameters.AddWithValue(“@gr”, dr.Cells(9).Value)
                                .Parameters.AddWithValue(“@pv”, dr.Cells(10).Value)
                                .Parameters.AddWithValue(“@tva”, dr.Cells(11).Value)
                                execution = .ExecuteNonQuery()

                                .CommandText = sql2
                                .Parameters.Clear()
                                .Parameters.AddWithValue(“@tick”, id)

                                .Parameters.AddWithValue(“@code”, dr.Cells(4).Value)
                                .Parameters.AddWithValue(“@price”, Convert.ToDecimal(dr.Cells(3).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                .Parameters.AddWithValue(“@qty”, Convert.ToDecimal(dr.Cells(2).Value).ToString("# ##0.00"))
                                .Parameters.AddWithValue(“@montant”, Convert.ToDecimal(dr.Cells(6).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                .Parameters.AddWithValue(“@date”, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                .Parameters.AddWithValue(“@pa”, Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                .Parameters.AddWithValue(“@type”, "ventes")
                                .Parameters.AddWithValue(“@name”, dr.Cells(1).Value)
                                .Parameters.AddWithValue(“@mrg”, Convert.ToDecimal(marg.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                .Parameters.AddWithValue(“@rms”, dr.Cells(8).Value)
                                .Parameters.AddWithValue(“@gr”, dr.Cells(9).Value)
                                .Parameters.AddWithValue(“@pv”, dr.Cells(10).Value)
                                .Parameters.AddWithValue(“@tva”, dr.Cells(11).Value)
                                execution = .ExecuteNonQuery()
                            End With
                            conn2.Close()

                        Catch ex As MySqlException
                            MsgBox(ex.Message)
                        End Try

                    Next


                    adpt = New MySqlDataAdapter("select * from orders WHERE parked = @day and ServeurRef = @caissier", conn2)
                    adpt.SelectCommand.Parameters.Clear()
                    adpt.SelectCommand.Parameters.AddWithValue("@day", "yes")
                    adpt.SelectCommand.Parameters.AddWithValue("@caissier", Label2.Text)
                    Dim table1 As New DataTable
                    adpt.Fill(table1)

                    DataGridView3.Rows.Clear()
                    Label28.Text = "numbl"
                    Label30.Text = 0
                    Label32.Text = "Sans livraison"
                    Label6.Text = 0.ToString("# ##0.00")
                    Try
                        If IsNumeric(TextBox3.Text) = False Then
                            If Label9.Text = "Rendu :" Then
                                Label4.Text = 0.00
                            End If
                            If Label9.Text = "Reste :" Then
                                Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                            End If
                        Else
                            If Label9.Text = "Rendu :" Then
                                Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                            End If
                            If Label9.Text = "Reste :" Then
                                Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                            End If
                        End If
                    Catch ex As MySqlException
                        MsgBox(ex.Message)
                    End Try
                    Label23.Text = 0.ToString("# ##0.00")
                    Label24.Text = 0.ToString("# ##0.00")
                    Label25.Text = 0.ToString("# ##0.00")


                    TextBox21.Text = ""

                    Label7.Text = 0.ToString("# ##0.00")
                    TextBox4.Text = ""
                    TextBox2.Select()
                    TextBox2.Text = ""
                    TextBox1.Text = 0
                    TextBox3.Text = ""
                    ComboBox2.SelectedItem = "comptoir"
                    TextBox8.Text = ""
                    ComboBox1.SelectedItem = "Espèce"

                    Label18.Visible = False

                    adpt = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
                    Dim tabletick As New DataTable
                    adpt.Fill(tabletick)
                    Dim idlast As Integer
                    If tabletick.Rows().Count = 0 Then
                        idlast = 1

                    Else
                        idlast = tabletick.Rows(0).Item(0) + 1

                    End If
                    Label20.Text = "Ticket N° " & idlast.ToString
                Else
                    MsgBox("Ticket vide !")
                End If


            Else
                MsgBox("Votre parking de commande est plein, veuillez valider ou supprimer des commandes parqué !")
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        TextBox2.Select()
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        Try
            If TextBox8.Text = "" Then
                adpt = New MySqlDataAdapter("select client from clients where masq = 'non' order by client asc", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count <> 0 Then
                    ComboBox2.Items.Clear()
                    For i = 0 To table.Rows.Count - 1
                        ComboBox2.Items.Add(table.Rows(i).Item(0))
                    Next
                End If
                ComboBox2.SelectedItem = "comptoir"
                TextBox21.Text = 0
                Label6.Text = Convert.ToDecimal(Label7.Text.Replace(" ", "").Replace(".", ",")).ToString("#.00")

                Try
                    If IsNumeric(TextBox3.Text) = False Then
                        If Label9.Text = "Rendu :" Then
                            Label4.Text = 0.00
                        End If
                        If Label9.Text = "Reste :" Then
                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                        End If
                    Else
                        If Label9.Text = "Rendu :" Then
                            Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                        End If
                        If Label9.Text = "Reste :" Then
                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                        End If
                    End If
                Catch ex As MySqlException
                    MsgBox(ex.Message)
                End Try
                ComboBox2.Visible = False
                IconButton46.Visible = False
                Panel8.Visible = False
            Else


                Dim inputText As String = TextBox8.Text

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
                    RemoveHandler TextBox8.TextChanged, AddressOf TextBox8_TextChanged

                    ' Mettez à jour le texte dans le TextBox
                    TextBox8.Text = modifiedText

                    ' Replacez le curseur à la position correcte après la modification
                    TextBox8.SelectionStart = TextBox8.Text.Length

                    ' Réactivez le gestionnaire d'événements TextChanged
                    AddHandler TextBox8.TextChanged, AddressOf TextBox8_TextChanged
                End If

                ComboBox2.Items.Clear()

                adpt = New MySqlDataAdapter("select * from fideles WHERE code = '" + TextBox8.Text + "'", conn2)
                Dim table2 As New DataTable
                adpt.Fill(table2)

                If table2.Rows.Count() <> 0 Then

                    ComboBox2.Items.Add("Client fidèle")
                    ComboBox2.Text = "Client fidèle"


                    ComboBox2.Visible = False
                    IconButton46.Visible = False

                Else
                    adpt = New MySqlDataAdapter("select client from clients WHERE client LIKE '%" + TextBox8.Text.Replace("'", " ") + "%' and masq = 'non' order by client asc", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    If table.Rows.Count <> 0 Then
                        For i = 0 To table.Rows.Count - 1
                            ComboBox2.Items.Add(table.Rows(i).Item(0))
                        Next
                        ComboBox2.Visible = True

                        ComboBox2.SelectedIndex = 0
                        IconButton46.Visible = True

                    End If

                End If

            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TextBox3_GotFocus(sender As Object, e As EventArgs) Handles TextBox3.GotFocus
        txt = sender
        'Dim cl As New clavier
        'cl.ShowDialog()
    End Sub

    Private Sub TextBox6_GotFocus(sender As Object, e As EventArgs) Handles TextBox6.GotFocus
        txt = sender

        'Dim cl As New clavier
        'cl.ShowDialog()
    End Sub

    Private Sub IconButton20_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub IconButton20_Click_1(sender As Object, e As EventArgs) Handles IconButton20.Click
        Try
            Dim cl As New clavier
            cl.ShowDialog()
            TextBox2.Select()
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IconButton18_Click(sender As Object, e As EventArgs) Handles IconButton18.Click
        Panel6.Visible = True
        TextBox11.Text = 0
        TextBox12.Text = 0
        TextBox13.Text = 0
        TextBox14.Text = 0
        TextBox15.Text = 0
        TextBox16.Text = 0
        TextBox17.Text = 0
        TextBox18.Text = 0
        TextBox19.Text = 0
        TextBox20.Text = 0
        TextBox11.Select()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub IconButton17_Click(sender As Object, e As EventArgs) Handles IconButton17.Click
        IconButton17.Text = 0
    End Sub
    Dim rowIndex As Integer = 0
    Private Sub IconButton21_Click(sender As Object, e As EventArgs) Handles IconButton21.Click
        If DataGridView3.Rows(rowIndex).Cells(2).Value > 1 Then
            DataGridView3.Rows(rowIndex).Cells(2).Value = DataGridView3.Rows(rowIndex).Cells(2).Value + 10
        Else
            DataGridView3.Rows(rowIndex).Cells(2).Value = 10
        End If

        ds.DataGridView3.CurrentRow.Cells(6).Value = Convert.ToDecimal(((ds.DataGridView3.CurrentRow.Cells(10).Value * (ds.DataGridView3.CurrentRow.Cells(2).Value - ds.DataGridView3.CurrentRow.Cells(9).Value) * (1 - (ds.DataGridView3.CurrentRow.Cells(8).Value / 100)))) * (1 + ds.DataGridView3.CurrentRow.Cells(11).Value)).ToString("# ##0.00")
        Dim sum As Decimal = 0
        For i = 0 To ds.DataGridView3.Rows.Count - 1
            sum = sum + Convert.ToDecimal(ds.DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", ""))
        Next
        Label6.Text = sum.ToString("# ##0.00").Replace(" ", "")
        Try
            If IsNumeric(TextBox3.Text) = False Then
                If Label9.Text = "Rendu :" Then
                    Label4.Text = 0.00
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
            Else
                If Label9.Text = "Rendu :" Then
                    Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                End If
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        Label7.Text = sum.ToString("# ##0.00")
        TextBox4.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton22_Click(sender As Object, e As EventArgs) Handles IconButton22.Click

        If DataGridView3.Rows(rowIndex).Cells(2).Value <= 10 Then
            DataGridView3.Rows(rowIndex).Cells(2).Value = 0
        Else
            DataGridView3.Rows(rowIndex).Cells(2).Value = DataGridView3.Rows(rowIndex).Cells(2).Value - 10
        End If

        ds.DataGridView3.CurrentRow.Cells(6).Value = Convert.ToDecimal(((ds.DataGridView3.CurrentRow.Cells(10).Value * (ds.DataGridView3.CurrentRow.Cells(2).Value - ds.DataGridView3.CurrentRow.Cells(9).Value) * (1 - (ds.DataGridView3.CurrentRow.Cells(8).Value / 100)))) * (1 + ds.DataGridView3.CurrentRow.Cells(11).Value)).ToString("# ##0.00")
        Dim sum As Decimal = 0
        For i = 0 To ds.DataGridView3.Rows.Count - 1
            sum = sum + Convert.ToDecimal(ds.DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", ""))
        Next
        Label6.Text = sum.ToString("# ##0.00").Replace(" ", "")
        Try
            If IsNumeric(TextBox3.Text) = False Then
                If Label9.Text = "Rendu :" Then
                    Label4.Text = 0.00
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
            Else
                If Label9.Text = "Rendu :" Then
                    Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                End If
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        Label7.Text = sum.ToString("# ##0.00")
        TextBox4.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub IconButton23_Click(sender As Object, e As EventArgs) Handles IconButton23.Click
        Panel15.Visible = False
        tiroir.ShowDialog()
        If Caseopen = True Then
            Dim printDoc As New PrintDocument()
            Dim printname As String = receiptprinter
            Dim codeOpenCashDrawer As Byte() = New Byte() {27, 112, 48, 55, 121}
            Dim rw As RawPrinterHelper = New RawPrinterHelper
            Dim pUnmanagedBytes As New IntPtr(0)
            pUnmanagedBytes = Marshal.AllocCoTaskMem(5)
            Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5)
            rw.SendBytesToPrinter(printname, pUnmanagedBytes, 5)
            Marshal.FreeCoTaskMem(pUnmanagedBytes)
            Caseopen = False
            TextBox2.Select()
            TextBox2.Text = ""
        End If
        TextBox4.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton24_Click(sender As Object, e As EventArgs)
        Synchronisation.ShowDialog()
    End Sub

    Private Sub IconButton24_Click_1(sender As Object, e As EventArgs) Handles IconButton24.Click

    End Sub

    Private Sub DataGridView3_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)

    End Sub
    Private Sub dashboard_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.Control AndAlso e.KeyCode = Keys.T Then
            IconButton23.PerformClick()
        End If
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Caisse - Vendre'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then

                If e.KeyCode = Keys.F9 Then
                    If f9clicked = True Then
                        If DataGridView3.Rows.Count <> 0 Then
                            If Panel3.Visible = False Then
                                IconButton1.PerformClick()
                                f9clicked = False
                            Else
                                If tpevar = True Then
                                    IconButton1.PerformClick()
                                    f9clicked = False
                                End If
                            End If
                        End If
                    Else
                        If DataGridView3.Rows.Count <> 0 Then
                            Panel5.Visible = True
                            IconButton31.PerformClick()
                            TextBox3.Select()
                            f9clicked = True
                        End If
                    End If
                End If

                If e.KeyCode = Keys.F11 Then
                    If DataGridView3.Rows.Count <> 0 Then
                        Panel5.Visible = True
                        IconButton32.PerformClick()
                        TextBox8.Select()
                    End If
                End If

                If e.KeyCode = Keys.F10 Then
                    If f9clicked = True Then
                        If DataGridView3.Rows.Count <> 0 Then
                            If Panel3.Visible = True Then

                            Else
                                If tpevar = False Then
                                    IconButton37.PerformClick()
                                    f9clicked = False
                                    IconButton52.PerformClick()
                                End If

                            End If
                        End If
                    Else
                        If DataGridView3.Rows.Count <> 0 Then
                            Panel5.Visible = True
                            IconButton31.PerformClick()
                            TextBox3.Select()
                            f9clicked = True
                        End If
                    End If
                End If
            Else
                If e.KeyCode = Keys.F9 Or e.KeyCode = Keys.F11 Or e.KeyCode = Keys.F10 Then
                    MsgBox("Vous n'avez pas l'autorisation !")
                End If

            End If
        Else
            If e.KeyCode = Keys.F9 Then
                If f9clicked = True Then
                    If DataGridView3.Rows.Count <> 0 Then
                        If Panel3.Visible = False Then
                            IconButton1.PerformClick()
                            f9clicked = False
                        Else
                            If tpevar = True Then
                                IconButton1.PerformClick()
                                f9clicked = False
                            End If
                        End If
                    End If
                Else
                    If DataGridView3.Rows.Count <> 0 Then
                        Panel5.Visible = True
                        IconButton31.PerformClick()
                        TextBox3.Select()
                        f9clicked = True
                    End If
                End If
            End If

            If e.KeyCode = Keys.F11 Then
                If DataGridView3.Rows.Count <> 0 Then
                    Panel5.Visible = True
                    IconButton32.PerformClick()
                    TextBox8.Select()
                End If
            End If

            If e.KeyCode = Keys.F10 Then
                If f9clicked = True Then
                    If DataGridView3.Rows.Count <> 0 Then
                        If Panel3.Visible = True Then

                        Else
                            If tpevar = False Then
                                IconButton37.PerformClick()
                                f9clicked = False
                                IconButton52.PerformClick()
                            End If

                        End If
                    End If
                Else
                    If DataGridView3.Rows.Count <> 0 Then
                        Panel5.Visible = True
                        IconButton31.PerformClick()
                        TextBox3.Select()
                        f9clicked = True
                    End If
                End If
            End If
        End If


        If e.KeyCode = Keys.Escape Then
            IconButton52.PerformClick()
            TextBox2.Text = ""
            TextBox2.Select()
            f9clicked = False
        End If


        If e.KeyCode = Keys.Multiply Then
            If DataGridView3.Rows.Count() <> 0 Then
                calcul.Close()
                ds = Me
                calcul.ShowDialog()
            End If

        End If


    End Sub

    Private Sub IconButton25_Click(sender As Object, e As EventArgs) Handles IconButton25.Click
        credi.ShowDialog()
        TextBox2.Select()
    End Sub

    Private Sub dashboard_Click(sender As Object, e As EventArgs) Handles Me.Click
        TextBox2.Select()
    End Sub

    Private Sub IconButton26_Click(sender As Object, e As EventArgs) Handles IconButton26.Click
        Dim sql2 As String
        Dim cmd2, cmd3 As New MySqlCommand
        If DataGridView3.Rows.Count <= 0 Then
            MsgBox(“Saisir un produit ! ”)
        Else
            conn2.Close()
            conn2.Open()

            For Each dr As DataGridViewRow In Me.DataGridView3.Rows

                Try
                    sql2 = "INSERT INTO retours (`code`, `name`, `qty`,`price`,`date`) 
                    VALUES ('" & dr.Cells(4).Value.ToString & "','" + dr.Cells(1).Value.ToString.Replace(".", ",") + "','" + dr.Cells(2).Value.ToString.Replace(".", ",") + "','" + dr.Cells(6).Value.ToString.Replace(".", ",") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')"
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.Parameters.Clear()
                    cmd2.ExecuteNonQuery()
                    cmd3 = New MySqlCommand("UPDATE `article` SET `Stock`= `Stock` + '" & dr.Cells(2).Value.ToString.Replace(".", ",") & "', `sell` = 1  WHERE Code = '" + dr.Cells(4).Value.ToString + "'", conn2)
                    cmd3.ExecuteNonQuery()

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Next
            conn2.Close()
            DataGridView3.Rows.Clear()
            Label28.Text = "numbl"
            Label30.Text = 0
            Label32.Text = "Sans livraison"
            TextBox4.Text = ""
            TextBox2.Select()
            TextBox2.Text = ""
            ComboBox2.Text = "comptoir"
            ComboBox6.Text = "comptoir"
            Label61.Text = "comptoir"
            ComboBox2.Visible = False

            ComboBox1.Text = "Espèce"

        End If



    End Sub

    Private Sub IconButton27_Click(sender As Object, e As EventArgs) Handles IconButton27.Click
        archive.Show()
        archive.Panel1.Visible = True
        archive.IconButton16.Visible = False
        TextBox4.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton28_Click(sender As Object, e As EventArgs) Handles IconButton28.Click
        Dim sumqty As Decimal

        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("tick")
        For i = 0 To DataGridView3.Rows.Count - 1
            If DataGridView3.Rows(i).Cells(9).Value = 0 Then
                Dim found As Boolean = False
                For K = 0 To dt.Rows.Count - 1
                    If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                        found = True
                        dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(2).Value
                        dt.Rows(K).Item(7) = Convert.ToDouble(dt.Rows(K).Item(7)) + DataGridView3.Rows(i).Cells(6).Value
                        sumqty += DataGridView3.Rows(i).Cells(2).Value
                        Exit For

                    End If
                Next
                If Not found Then
                    dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value, "", DataGridView3.Rows(i).Cells(2).Value, "", DataGridView3.Rows(i).Cells(3).Value, "", DataGridView3.Rows(i).Cells(6).Value, DataGridView3.Rows(i).Cells(7).Value)
                    sumqty += DataGridView3.Rows(i).Cells(2).Value
                End If
            End If
        Next
        For i = 0 To DataGridView3.Rows.Count - 1
            If DataGridView3.Rows(i).Cells(9).Value > 0 Then
                Dim found As Boolean = False
                For K = 0 To dt.Rows.Count - 1
                    If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                        found = True
                        dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(9).Value
                        sumqty += DataGridView3.Rows(i).Cells(9).Value
                        Exit For

                    End If
                Next
                If Not found Then
                    dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value & " (Gratuit)", "", DataGridView3.Rows(i).Cells(9).Value, "", 0, "", 0, 0)
                    sumqty += DataGridView3.Rows(i).Cells(9).Value
                End If
            End If
        Next

        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\Report1.rdlc"
        ReportToPrint.DataSources.Clear()
        ReportToPrint.EnableExternalImages = True

        Dim clt As String

        If ComboBox2.Text = "" Then
            clt = "comptoir"
        Else
            clt = ComboBox2.Text
        End If


        Dim lpar3 As New ReportParameter("totale", Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") + Convert.ToDecimal(tfraisM.Replace(".", ","))).ToString("# ##0.00"))
        Dim lpar31(0) As ReportParameter
        lpar31(0) = lpar3
        ReportToPrint.SetParameters(lpar31)

        Dim Acre As New ReportParameter("CreditAncien", anciencrdt)
        Dim Acre1(0) As ReportParameter
        Acre1(0) = Acre
        ReportToPrint.SetParameters(Acre1)



        Dim cliente As New ReportParameter("client", clt)
        Dim client1(0) As ReportParameter
        client1(0) = cliente
        ReportToPrint.SetParameters(client1)

        Dim ide As New ReportParameter("id", tc)
        Dim id1(0) As ReportParameter
        id1(0) = ide
        ReportToPrint.SetParameters(id1)

        Dim daty As New ReportParameter("date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
        Dim daty1(0) As ReportParameter
        daty1(0) = daty
        ReportToPrint.SetParameters(daty1)

        Dim mode As New ReportParameter("mode", ComboBox1.Text)
        Dim mode1(0) As ReportParameter
        mode1(0) = mode
        ReportToPrint.SetParameters(mode1)

        Dim caissier As New ReportParameter("caissier", Label2.Text)
        Dim caissier1(0) As ReportParameter
        caissier1(0) = caissier
        ReportToPrint.SetParameters(caissier1)

        Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
        Dim table23 As New DataTable
        adpt23.Fill(table23)

        Dim info As New ReportParameter("info", table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString)
        Dim info1(0) As ReportParameter
        info1(0) = info
        ReportToPrint.SetParameters(info1)


        Dim msg As New ReportParameter("msg", table23.Rows(0).Item(6).ToString)
        Dim msg1(0) As ReportParameter
        msg1(0) = msg
        ReportToPrint.SetParameters(msg1)

        Dim count As New ReportParameter("count", DataGridView3.Rows.Count.ToString)
        Dim count1(0) As ReportParameter
        count1(0) = count
        ReportToPrint.SetParameters(count1)

        Dim fraisN As New ReportParameter("FraisN", tfraisN)
        Dim fraisN1(0) As ReportParameter
        fraisN1(0) = fraisN
        ReportToPrint.SetParameters(fraisN1)

        Dim fraisM As New ReportParameter("FraisM", tfraisM)
        Dim fraisM1(0) As ReportParameter
        fraisM1(0) = fraisM
        ReportToPrint.SetParameters(fraisM1)

        Dim qty As New ReportParameter("qty", sumqty.ToString)
        Dim qty1(0) As ReportParameter
        qty1(0) = qty
        ReportToPrint.SetParameters(qty1)

        Dim appPath As String = Application.StartupPath()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

        Dim img As New ReportParameter("image", "File:\" + SavePath, True)
        Dim img1(0) As ReportParameter
        img1(0) = img
        ReportToPrint.SetParameters(img1)

        ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

        'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
        'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
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

        TextBox4.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub


    Private Sub IconButton29_Click(sender As Object, e As EventArgs) Handles IconButton29.Click
        Panel15.Visible = False
        parkliste.ShowDialog()
        TextBox4.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton30_Click(sender As Object, e As EventArgs) Handles IconButton30.Click
        Dim sumqty As Decimal

        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("tick")
        For i = 0 To DataGridView3.Rows.Count - 1
            If DataGridView3.Rows(i).Cells(9).Value = 0 Then
                Dim found As Boolean = False
                For K = 0 To dt.Rows.Count - 1
                    If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                        found = True
                        dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(2).Value
                        dt.Rows(K).Item(7) = Convert.ToDouble(dt.Rows(K).Item(7)) + DataGridView3.Rows(i).Cells(6).Value
                        sumqty += DataGridView3.Rows(i).Cells(2).Value
                        Exit For

                    End If
                Next
                If Not found Then
                    dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value, "", Convert.ToDouble(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "")).ToString("N2"), "", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "")).ToString("N2"), "", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")).ToString("N2"), DataGridView3.Rows(i).Cells(7).Value)
                    sumqty += DataGridView3.Rows(i).Cells(2).Value
                End If
            End If
        Next
        For i = 0 To DataGridView3.Rows.Count - 1
            If DataGridView3.Rows(i).Cells(9).Value > 0 Then
                Dim found As Boolean = False
                For K = 0 To dt.Rows.Count - 1
                    If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                        found = True
                        dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(9).Value
                        sumqty += DataGridView3.Rows(i).Cells(9).Value
                        Exit For

                    End If
                Next
                If Not found Then
                    dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value & " (Gratuit)", "", DataGridView3.Rows(i).Cells(9).Value, "", 0, "", 0, 0)
                    sumqty += DataGridView3.Rows(i).Cells(9).Value
                End If
            End If
        Next
        ReportToPrint = New LocalReport()
        ReportToPrint.ReportPath = Application.StartupPath + "\Report4.rdlc"
        ReportToPrint.DataSources.Clear()
        ReportToPrint.EnableExternalImages = True

        Dim clt As String

        If ComboBox2.Text = "" Then
            clt = "comptoir"
        Else
            clt = ComboBox2.Text
        End If

        Dim cliente As New ReportParameter("client", clt)
        Dim client1(0) As ReportParameter
        client1(0) = cliente
        ReportToPrint.SetParameters(client1)

        Dim ide As New ReportParameter("id", tc)
        Dim id1(0) As ReportParameter
        id1(0) = ide
        ReportToPrint.SetParameters(id1)

        Dim vt As New ReportParameter("vente", tc)
        Dim vt1(0) As ReportParameter
        vt1(0) = vt
        ReportToPrint.SetParameters(vt1)

        Dim daty As New ReportParameter("date", DateTime.Now.ToString("dd/MM/yyyy"))
        Dim daty1(0) As ReportParameter
        daty1(0) = daty
        ReportToPrint.SetParameters(daty1)

        Dim caissier As New ReportParameter("caissier", Label2.Text)
        Dim caissier1(0) As ReportParameter
        caissier1(0) = caissier
        ReportToPrint.SetParameters(caissier1)

        Dim total As New ReportParameter("total", Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") + Convert.ToDecimal(tfraisM.Replace(".", ","))).ToString("# ##0.00"))
        Dim total1(0) As ReportParameter
        total1(0) = total
        ReportToPrint.SetParameters(total1)

        Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
        Dim table23 As New DataTable
        adpt23.Fill(table23)

        Dim info As New ReportParameter("info", table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString & vbCrLf & table23.Rows(0).Item(6).ToString)
        Dim info1(0) As ReportParameter
        info1(0) = info
        ReportToPrint.SetParameters(info1)

        Dim livreur As New ReportParameter("livreur", Livr)
        Dim livreur1(0) As ReportParameter
        livreur1(0) = livreur
        ReportToPrint.SetParameters(livreur1)


        Dim msg As New ReportParameter("msg", table23.Rows(0).Item(6).ToString)
        Dim msg1(0) As ReportParameter
        msg1(0) = msg
        ReportToPrint.SetParameters(msg1)

        Dim count As New ReportParameter("count", DataGridView3.Rows.Count.ToString)
        Dim count1(0) As ReportParameter
        count1(0) = count
        ReportToPrint.SetParameters(count1)

        Dim qty As New ReportParameter("qty", sumqty.ToString)
        Dim qty1(0) As ReportParameter
        qty1(0) = qty
        ReportToPrint.SetParameters(qty1)

        Dim appPath As String = Application.StartupPath()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

        Dim img As New ReportParameter("image", "File:\" + SavePath, True)
        Dim img1(0) As ReportParameter
        img1(0) = img
        ReportToPrint.SetParameters(img1)

        ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))


        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>5.83in</PageWidth><PageHeight>8.3in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"

        Dim warnings As Warning()
        m_streams = New List(Of Stream)()

        ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)



        For Each stream As Stream In m_streams
            stream.Position = 0
        Next


        Dim printname As String = a5printer
        Dim printDoc As New PrintDocument()
        Dim ps As New PrinterSettings()
        ps.PrinterName = printname

        ' Définir le format de papier sur A5
        Dim paperSize As New PaperSize("A5", CInt(5.83 * 100), CInt(8.3 * 100)) ' Les dimensions sont en centièmes de pouce (1 pouce = 100)
        printDoc.DefaultPageSettings.PaperSize = paperSize
        printDoc.PrinterSettings = ps

        AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
        m_currentPageIndex = 0
        printDoc.Print()

        TextBox2.Select()
        TextBox2.Text = ""
    End Sub


    Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles DataGridView1.Click
        TextBox2.Select()
    End Sub

    Private Sub IconButton31_Click(sender As Object, e As EventArgs) Handles IconButton31.Click
        IconButton31.BackColor = Color.DarkGreen
        IconButton31.ForeColor = Color.White
        ComboBox1.Text = "Espèce"
        TextBox9.Text = ""
        TextBox10.Text = ""

        IconButton32.BackColor = Color.White
        IconButton32.ForeColor = Color.Black

        IconButton33.BackColor = Color.White
        IconButton33.ForeColor = Color.Black

        Panel3.Visible = False

    End Sub

    Private Sub IconButton32_Click(sender As Object, e As EventArgs) Handles IconButton32.Click
        IconButton32.BackColor = Color.DarkGreen
        IconButton32.ForeColor = Color.White
        ComboBox1.Text = "Crédit"
        TextBox9.Text = ""
        TextBox10.Text = ""

        IconButton31.BackColor = Color.White
        IconButton31.ForeColor = Color.Black

        IconButton33.BackColor = Color.White
        IconButton33.ForeColor = Color.Black

        Panel3.Visible = False
    End Sub
    Dim tpevar As Boolean = False
    Private Sub IconButton33_Click(sender As Object, e As EventArgs) Handles IconButton33.Click
        IconButton33.BackColor = Color.DarkGreen
        IconButton33.ForeColor = Color.White
        ComboBox1.Text = "TPE"
        TextBox9.Text = ""
        TextBox10.Text = ""
        IconButton32.BackColor = Color.White
        IconButton32.ForeColor = Color.Black

        IconButton31.BackColor = Color.White
        IconButton31.ForeColor = Color.Black

        Panel3.Visible = True
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        IconButton31.PerformClick()

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        IconButton33.PerformClick()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        IconButton32.PerformClick()
    End Sub

    Private Sub IconButton34_Click(sender As Object, e As EventArgs) Handles IconButton34.Click
        simpleajout.ShowDialog()
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton35_Click(sender As Object, e As EventArgs) Handles IconButton35.Click
        Me.TopMost = False
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton36_Click(sender As Object, e As EventArgs) Handles IconButton36.Click
        Panel15.Visible = False
        balisage.Show()
        balisage.Panel6.Visible = True
        balisage.Label16.Text = "Gestion des cadeaux"
        balisage.DataGridView1.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from gifts order by id desc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        For i = 0 To table.Rows.Count() - 1
            balisage.DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), "")
        Next
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton37_Click(sender As Object, e As EventArgs) Handles IconButton37.Click
        CheckBox2.Checked = False
        IconButton1.PerformClick()
        'If IconButton37.Text = "2 BL" Then

        '    Dim dt1 As New DataTable
        '    Dim ds As New DataSet1
        '    dt1 = ds.Tables("tick")
        '    Dim sumqty As Decimal

        '    For i = 0 To DataGridView3.Rows.Count - 1
        '        If DataGridView3.Rows(i).Cells(9).Value = 0 Then
        '            dt1.Rows.Add("", DataGridView3.Rows(i).Cells(1).Value, "", DataGridView3.Rows(i).Cells(2).Value, "", DataGridView3.Rows(i).Cells(3).Value, "", DataGridView3.Rows(i).Cells(6).Value, DataGridView3.Rows(i).Cells(7).Value)
        '            sumqty += DataGridView3.Rows(i).Cells(2).Value
        '        End If
        '    Next
        '    dt1.Rows.Add("", "----- Produits Gratuits -----")
        '    adpt = New MySqlDataAdapter("SELECT id from bondelivr ORDER BY id DESC LIMIT 1", conn2)
        '    Dim tablebl As New DataTable
        '    adpt.Fill(tablebl)
        '    Dim blnum As Integer
        '    If tablebl.Rows().Count = 0 Then
        '        blnum = 1

        '    Else
        '        blnum = tablebl.Rows(0).Item(0) + 1

        '    End If

        '    ReportToPrint = New LocalReport()
        '    ReportToPrint.ReportPath = Application.StartupPath + "\Report4.rdlc"
        '    ReportToPrint.DataSources.Clear()
        '    ReportToPrint.EnableExternalImages = True

        '    Dim clt As String

        '    If ComboBox2.Text = "" Then
        '        clt = "comptoir"
        '    Else
        '        clt = ComboBox2.Text
        '    End If

        '    Dim cliente As New ReportParameter("client", clt)
        '    Dim client1(0) As ReportParameter
        '    client1(0) = cliente
        '    ReportToPrint.SetParameters(client1)

        '    Dim ide As New ReportParameter("id", blnum)
        '    Dim id1(0) As ReportParameter
        '    id1(0) = ide
        '    ReportToPrint.SetParameters(id1)

        '    Dim vt As New ReportParameter("vente", id)
        '    Dim vt1(0) As ReportParameter
        '    vt1(0) = vt
        '    ReportToPrint.SetParameters(vt1)

        '    Dim daty As New ReportParameter("date", DateTime.Now.ToString("dd/MM/yyyy"))
        '    Dim daty1(0) As ReportParameter
        '    daty1(0) = daty
        '    ReportToPrint.SetParameters(daty1)

        '    Dim fraisN As New ReportParameter("FraisN", tfraisN)
        '    Dim fraisN1(0) As ReportParameter
        '    fraisN1(0) = fraisN
        '    ReportToPrint.SetParameters(fraisN1)

        '    Dim fraisM As New ReportParameter("FraisM", tfraisM)
        '    Dim fraisM1(0) As ReportParameter
        '    fraisM1(0) = fraisM
        '    ReportToPrint.SetParameters(fraisM1)

        '    Dim caissier As New ReportParameter("caissier", Label2.Text)
        '    Dim caissier1(0) As ReportParameter
        '    caissier1(0) = caissier
        '    ReportToPrint.SetParameters(caissier1)

        '    Dim total As New ReportParameter("total", Convert.ToDecimal(Label6.Text.Replace(".", ",") + Convert.ToDecimal(tfraisM.Replace(".", ","))).ToString("# ##0.00"))
        '    Dim total1(0) As ReportParameter
        '    total1(0) = total
        '    ReportToPrint.SetParameters(total1)

        '    Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
        '    Dim table23 As New DataTable
        '    adpt23.Fill(table23)

        '    Dim info As New ReportParameter("info", table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString)
        '    Dim info1(0) As ReportParameter
        '    info1(0) = info
        '    ReportToPrint.SetParameters(info1)

        '    Dim livreur As New ReportParameter("livreur", Livr)
        '    Dim livreur1(0) As ReportParameter
        '    livreur1(0) = livreur
        '    ReportToPrint.SetParameters(livreur1)


        '    Dim count As New ReportParameter("count", DataGridView3.Rows.Count.ToString)
        '    Dim count1(0) As ReportParameter
        '    count1(0) = count
        '    ReportToPrint.SetParameters(count1)

        '    Dim qty As New ReportParameter("qty", sumqty.ToString)
        '    Dim qty1(0) As ReportParameter
        '    qty1(0) = qty
        '    ReportToPrint.SetParameters(qty1)

        '    Dim appPath As String = Application.StartupPath()

        '    Dim SaveDirectory As String = appPath & "\"
        '    Dim SavePath As String = SaveDirectory & table23.Rows(0).Item(11).ToString.Replace("/", "\")

        '    Dim img As New ReportParameter("img", "File:\" + SavePath, True)
        '    Dim img1(0) As ReportParameter
        '    img1(0) = img
        '    ReportToPrint.SetParameters(img1)

        '    ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt1))

        '    'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
        '    'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
        '    'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
        '    'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
        '    'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
        '    Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>5.83in</PageWidth><PageHeight>8.3in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        '    Dim warnings As Warning()
        '    m_streams = New List(Of Stream)()

        '    ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)



        '    For Each stream As Stream In m_streams
        '        stream.Position = 0
        '    Next

        '    Dim printDoc As New PrintDocument()
        '    'Microsoft Print To PDF
        '    'C80250 Series
        '    'C80250 Series init
        '    Dim printname As String = a5printer
        '    printDoc.PrinterSettings.PrinterName = printname
        '    Dim ps As New PrinterSettings()
        '    ps.PrinterName = printDoc.PrinterSettings.PrinterName
        '    printDoc.PrinterSettings = ps

        '    AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
        '    m_currentPageIndex = 0
        '    printDoc.DefaultPageSettings.PaperSize = New PaperSize("A5", 583, 827)


        '    ' Vérifier et ajuster les marges par défaut de l'imprimante si nécessaire
        '    ps.DefaultPageSettings.Margins = New Margins(0, 0, 0, 0)
        '    printDoc.Print()
        'End If
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton38_Click(sender As Object, e As EventArgs) Handles IconButton38.Click
        Panel15.Visible = False
        achats.Show()
        achats.Panel8.Visible = True
        achats.IconButton16.Visible = False
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton40_Click(sender As Object, e As EventArgs) Handles IconButton40.Click
        If TextBox21.Text = "" Then
            TextBox21.Text = 0
        End If
        If Convert.ToDouble(TextBox21.Text.Replace(" ", "").Replace(".", ",")) > 0 Then

            Label6.Text = Convert.ToDecimal(Label23.Text - (Label23.Text * (TextBox21.Text.Replace(".", ",") / 100)) + Label24.Text).ToString("N2")
        Else
            Label6.Text = Label7.Text
        End If
        Try
            If IsNumeric(TextBox3.Text) = False Then
                If Label9.Text = "Rendu :" Then
                    Label4.Text = 0.00
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
            Else
                If Label9.Text = "Rendu :" Then
                    Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                End If
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        TextBox21.Text = ""

    End Sub

    Private Sub IconButton39_Click(sender As Object, e As EventArgs) Handles IconButton39.Click
        Panel15.Visible = False
        'balisage.Show()
        'balisage.Panel7.Visible = True
        'balisage.Label16.Text = "Code de fidélité"
        'balisage.DataGridView3.Rows.Clear()
        'adpt = New MySqlDataAdapter("select * from fideles order by id desc", conn2)
        'Dim table As New DataTable
        'adpt.Fill(table)

        'For i = 0 To table.Rows.Count() - 1
        '    balisage.DataGridView3.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3))
        'Next

        rapproche.Show()
        rapproche.Panel6.Visible = True
        rapproche.Label16.Text = "Encaissement Client"
        adpt = New MySqlDataAdapter("select client from clients where masq = 'non' order by client asc", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)
        If table2.Rows.Count <> 0 Then
            rapproche.ComboBox2.Items.Clear()
            rapproche.ComboBox2.Items.Add("Tout")
            For i = 0 To table2.Rows.Count - 1
                rapproche.ComboBox2.Items.Add(table2.Rows(i).Item(0))
            Next
            rapproche.ComboBox2.SelectedIndex = 0

        End If
        rapproche.Label1.Visible = False
        rapproche.Label2.Visible = False
        rapproche.Label8.Visible = False
        rapproche.Label15.Visible = False

        rapproche.IconButton6.Visible = False
        rapproche.TextBox1.Visible = False
        rapproche.ComboBox2.Visible = False
        rapproche.ComboBox4.Visible = False

        rapproche.DateTimePicker4.Visible = False
        rapproche.DateTimePicker3.Visible = False


        rapproche.IconButton1.PerformClick()

        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton41_Click(sender As Object, e As EventArgs) Handles IconButton41.Click
        If Panel10.Visible = True Then
            Panel10.Visible = False
        Else
            Panel10.Visible = True
        End If
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton42_Click(sender As Object, e As EventArgs) Handles IconButton42.Click
        If TextBox9.Text <> "" AndAlso TextBox10.Text <> "" Then
            Panel3.Visible = False
            tpevar = True
            If IconButton46.Visible = False Then
                If tpevar = True Then
                    IconButton1.PerformClick()
                    Dim ds As New DataSet1
                    Dim dt As New DataTable
                    dt = ds.Tables("tick")
                    Dim sumqty As Decimal = 0
                    For i = 0 To DataGridView3.Rows.Count - 1
                        If DataGridView3.Rows(i).Cells(9).Value = 0 Then
                            Dim found As Boolean = False
                            For K = 0 To dt.Rows.Count - 1
                                If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                                    found = True
                                    dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(2).Value
                                    dt.Rows(K).Item(7) = Convert.ToDouble(dt.Rows(K).Item(7)) + DataGridView3.Rows(i).Cells(6).Value
                                    sumqty += DataGridView3.Rows(i).Cells(2).Value
                                    Exit For

                                End If
                            Next
                            If Not found Then
                                dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value, "", DataGridView3.Rows(i).Cells(2).Value, "", DataGridView3.Rows(i).Cells(3).Value, "", DataGridView3.Rows(i).Cells(6).Value, DataGridView3.Rows(i).Cells(7).Value)
                                sumqty += DataGridView3.Rows(i).Cells(2).Value
                            End If
                        End If
                    Next
                    For i = 0 To DataGridView3.Rows.Count - 1
                        If DataGridView3.Rows(i).Cells(9).Value > 0 Then
                            Dim found As Boolean = False
                            For K = 0 To dt.Rows.Count - 1
                                If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                                    found = True
                                    dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(9).Value
                                    sumqty += DataGridView3.Rows(i).Cells(9).Value
                                    Exit For

                                End If
                            Next
                            If Not found Then
                                dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value & " (Gratuit)", "", DataGridView3.Rows(i).Cells(9).Value, "", 0, "", 0, 0)
                                sumqty += DataGridView3.Rows(i).Cells(9).Value
                            End If
                        End If
                    Next

                    Try

                        Casef = True
                        tfraisN = "Coût Logistique"
                        tfraisM = 0
                        Livr = "-"

                        If CheckBox2.Checked Then
                            ReportToPrint = New LocalReport()
                            ReportToPrint.ReportPath = Application.StartupPath + "\Report1.rdlc"
                            ReportToPrint.DataSources.Clear()
                            ReportToPrint.EnableExternalImages = True

                            Dim clt As String

                            If ComboBox2.Text = "" Then
                                clt = "comptoir"
                            Else
                                clt = ComboBox2.Text
                            End If


                            Dim lpar3 As New ReportParameter("totale", Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") + Convert.ToDecimal(tfraisM.Replace(".", ","))).ToString("# ##0.00"))
                            Dim lpar31(0) As ReportParameter
                            lpar31(0) = lpar3
                            ReportToPrint.SetParameters(lpar31)

                            Dim Acre As New ReportParameter("CreditAncien", anciencrdt)
                            Dim Acre1(0) As ReportParameter
                            Acre1(0) = Acre
                            ReportToPrint.SetParameters(Acre1)



                            Dim cliente As New ReportParameter("client", clt)
                            Dim client1(0) As ReportParameter
                            client1(0) = cliente
                            ReportToPrint.SetParameters(client1)

                            Dim ide As New ReportParameter("id", id)
                            Dim id1(0) As ReportParameter
                            id1(0) = ide
                            ReportToPrint.SetParameters(id1)

                            Dim tpe As New ReportParameter("tpe", "N° STAN : " & TextBox9.Text & vbCrLf & "N° Autorisation : " & TextBox10.Text)
                            Dim tpe1(0) As ReportParameter
                            tpe1(0) = tpe
                            ReportToPrint.SetParameters(tpe1)

                            Dim daty As New ReportParameter("date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                            Dim daty1(0) As ReportParameter
                            daty1(0) = daty
                            ReportToPrint.SetParameters(daty1)

                            Dim mode As New ReportParameter("mode", "TPE")
                            Dim mode1(0) As ReportParameter
                            mode1(0) = mode
                            ReportToPrint.SetParameters(mode1)

                            Dim caissier As New ReportParameter("caissier", Label2.Text)
                            Dim caissier1(0) As ReportParameter
                            caissier1(0) = caissier
                            ReportToPrint.SetParameters(caissier1)

                            Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
                            Dim table23 As New DataTable
                            adpt23.Fill(table23)

                            Dim info As New ReportParameter("info", table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString)
                            Dim info1(0) As ReportParameter
                            info1(0) = info
                            ReportToPrint.SetParameters(info1)


                            Dim msg As New ReportParameter("msg", table23.Rows(0).Item(6).ToString)
                            Dim msg1(0) As ReportParameter
                            msg1(0) = msg
                            ReportToPrint.SetParameters(msg1)

                            Dim count As New ReportParameter("count", dt.Rows.Count.ToString)
                            Dim count1(0) As ReportParameter
                            count1(0) = count
                            ReportToPrint.SetParameters(count1)

                            Dim fraisN As New ReportParameter("FraisN", tfraisN)
                            Dim fraisN1(0) As ReportParameter
                            fraisN1(0) = fraisN
                            ReportToPrint.SetParameters(fraisN1)

                            Dim fraisM As New ReportParameter("FraisM", tfraisM)
                            Dim fraisM1(0) As ReportParameter
                            fraisM1(0) = fraisM
                            ReportToPrint.SetParameters(fraisM1)

                            Dim livreur As New ReportParameter("livreur", Livr)
                            Dim livreur1(0) As ReportParameter
                            livreur1(0) = livreur
                            ReportToPrint.SetParameters(livreur1)

                            Dim qty As New ReportParameter("qty", sumqty.ToString)
                            Dim qty1(0) As ReportParameter
                            qty1(0) = qty
                            ReportToPrint.SetParameters(qty1)


                            Dim appPath As String = Application.StartupPath()

                            Dim SaveDirectory As String = appPath & "\"
                            Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                            Dim img As New ReportParameter("image", "File:\" + SavePath, True)
                            Dim img1(0) As ReportParameter
                            img1(0) = img
                            ReportToPrint.SetParameters(img1)

                            ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

                            'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
                            'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
                            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
                            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
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
                            'tbl.Rows.Clear()
                            'tbl2.Rows.Clear()

                            DataGridView3.Rows.Clear()

                        End If


                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


                End If
                tpevar = False
                Label6.Text = 0.00
            Else
                tpevar = True
                IconButton46.PerformClick()
            End If
            TextBox4.Text = ""

            TextBox2.Select()
            TextBox2.Text = ""

        Else
            MsgBox("Veuillez remplir le N° de STAN et d'Autorisation !")
        End If
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton45_Click(sender As Object, e As EventArgs) Handles IconButton45.Click
        Panel15.Visible = False
        Panel10.Visible = False
        adpt = New MySqlDataAdapter("SELECT * from orders where parked = 'no' ORDER BY OrderID DESC LIMIT 1", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        adpt = New MySqlDataAdapter("SELECT * from orderdetails where OrderID = '" & table.Rows(0).Item(0) & "'", conn2)
        Dim table2 As New DataTable
        adpt.Fill(table2)


        Dim sumqty As Decimal = 0
        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("tick")
        Dim numa As Integer = 0

        For i = 0 To table2.Rows.Count - 1
            If table2.Rows(i).Item(12) = 0 Then

            End If
            If table2.Rows(i).Item(12) = 0 Then
                Dim found As Boolean = False
                For K = 0 To dt.Rows.Count - 1
                    If table2.Rows(i).Item(1) = dt.Rows(K).Item(0) Then

                        found = True
                        dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + table2.Rows(i).Item(3)
                        dt.Rows(K).Item(7) = Convert.ToDouble(dt.Rows(K).Item(7)) + table2.Rows(i).Item(4)
                        sumqty += table2.Rows(i).Item(3)
                        Exit For

                    End If
                Next
                If Not found Then
                    dt.Rows.Add("", table2.Rows(i).Item(9), "", table2.Rows(i).Item(3), "", table2.Rows(i).Item(2), "", table2.Rows(i).Item(4), numa)
                    sumqty += table2.Rows(i).Item(3)
                    numa = numa + 1
                End If
            End If
        Next
        For i = 0 To table2.Rows.Count - 1
            If table2.Rows(i).Item(12) > 0 Then
                Dim found As Boolean = False
                For K = 0 To dt.Rows.Count - 1
                    If table2.Rows(i).Item(1) = dt.Rows(K).Item(0) Then

                        found = True
                        dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + table2.Rows(i).Item(3)
                        sumqty += table2.Rows(i).Item(12)
                        Exit For

                    End If
                Next
                If Not found Then
                    dt.Rows.Add("", table2.Rows(i).Item(9) & " (Gratuit)", "", table2.Rows(i).Item(12), "", 0, "", 0, 0)
                    sumqty += table2.Rows(i).Item(12)
                End If
            End If
        Next



        Try

            Casef = True
            tfraisN = "Coût Logistique"
            tfraisM = 0
            Livr = "-"

            If CheckBox2.Checked Then
                ReportToPrint = New LocalReport()
                ReportToPrint.ReportPath = Application.StartupPath + "\Report1.rdlc"
                ReportToPrint.DataSources.Clear()
                ReportToPrint.EnableExternalImages = True

                Dim clt As String

                If ComboBox2.Text = "" Then
                    clt = "comptoir"
                Else
                    clt = ComboBox2.Text
                End If


                Dim lpar3 As New ReportParameter("totale", Convert.ToDecimal(Convert.ToDecimal(table.Rows(0).Item(2)) + Convert.ToDecimal(table.Rows(0).Item(14))).ToString("#0.00"))
                Dim lpar31(0) As ReportParameter
                lpar31(0) = lpar3
                ReportToPrint.SetParameters(lpar31)

                Dim Acre As New ReportParameter("CreditAncien", 0)
                Dim Acre1(0) As ReportParameter
                Acre1(0) = Acre
                ReportToPrint.SetParameters(Acre1)

                Dim cliente As New ReportParameter("client", table.Rows(0).Item(12).ToString)
                Dim client1(0) As ReportParameter
                client1(0) = cliente
                ReportToPrint.SetParameters(client1)

                Dim ide As New ReportParameter("id", table.Rows(0).Item(0).ToString)
                Dim id1(0) As ReportParameter
                id1(0) = ide
                ReportToPrint.SetParameters(id1)

                Dim daty As New ReportParameter("date", Format(table.Rows(0).Item(1), "yyyy-MM-dd HH:mm:ss"))
                Dim daty1(0) As ReportParameter
                daty1(0) = daty
                ReportToPrint.SetParameters(daty1)

                Dim mode As New ReportParameter("mode", table.Rows(0).Item(10).ToString)
                Dim mode1(0) As ReportParameter
                mode1(0) = mode
                ReportToPrint.SetParameters(mode1)

                Dim caissier As New ReportParameter("caissier", Label2.Text)
                Dim caissier1(0) As ReportParameter
                caissier1(0) = caissier
                ReportToPrint.SetParameters(caissier1)

                Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
                Dim table23 As New DataTable
                adpt23.Fill(table23)

                Dim info As New ReportParameter("info", table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString)
                Dim info1(0) As ReportParameter
                info1(0) = info
                ReportToPrint.SetParameters(info1)


                Dim msg As New ReportParameter("msg", table23.Rows(0).Item(6).ToString)
                Dim msg1(0) As ReportParameter
                msg1(0) = msg
                ReportToPrint.SetParameters(msg1)

                Dim count As New ReportParameter("count", table2.Rows.Count.ToString)
                Dim count1(0) As ReportParameter
                count1(0) = count
                ReportToPrint.SetParameters(count1)

                Dim fraisN As New ReportParameter("FraisN", tfraisN)
                Dim fraisN1(0) As ReportParameter
                fraisN1(0) = fraisN
                ReportToPrint.SetParameters(fraisN1)

                Dim fraisM As New ReportParameter("FraisM", table.Rows(0).Item(14).ToString)
                Dim fraisM1(0) As ReportParameter
                fraisM1(0) = fraisM
                ReportToPrint.SetParameters(fraisM1)

                Dim livreur As New ReportParameter("livreur", table.Rows(0).Item(15).ToString)
                Dim livreur1(0) As ReportParameter
                livreur1(0) = livreur
                ReportToPrint.SetParameters(livreur1)

                Dim qty As New ReportParameter("qty", sumqty.ToString)
                Dim qty1(0) As ReportParameter
                qty1(0) = qty
                ReportToPrint.SetParameters(qty1)

                Dim appPath As String = Application.StartupPath()

                Dim SaveDirectory As String = appPath & "\"
                Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                Dim img As New ReportParameter("image", "File:\" + SavePath, True)
                Dim img1(0) As ReportParameter
                img1(0) = img
                ReportToPrint.SetParameters(img1)

                ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

                'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
                'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
                'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
                'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
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
                'tbl.Rows.Clear()
                'tbl2.Rows.Clear()

            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton44_Click(sender As Object, e As EventArgs) Handles IconButton44.Click
        Panel15.Visible = False
        'If Panel6.Visible = False Then
        '    Panel6.Visible = True
        '    DataGridView2.Rows.Clear()
        '    adpt = New MySqlDataAdapter("select * from infos", conn2)
        '    Dim tableimg As New DataTable
        '    adpt.Fill(tableimg)

        '    adpt = New MySqlDataAdapter("select * from orders WHERE OrderID > @maxclot and etat = @etat order by OrderID desc", conn2)
        '    adpt.SelectCommand.Parameters.Clear()
        '    adpt.SelectCommand.Parameters.AddWithValue("@maxclot", tableimg.Rows(0).Item(18))
        '    adpt.SelectCommand.Parameters.AddWithValue("@etat", "receipt")
        '    Dim tablelike As New DataTable
        '    adpt.Fill(tablelike)
        '    If tablelike.Rows.Count() <> 0 Then
        '        For i = 0 To tablelike.Rows.Count() - 1
        '            DataGridView2.Rows.Add(tablelike.Rows(i).Item(10), tablelike.Rows(i).Item(0), tablelike.Rows(i).Item(13))
        '        Next
        '    End If
        'Else
        '    Panel6.Visible = False
        'End If
        Panel10.Visible = False
        archive.Show()
        archive.BringToFront()
        archive.Panel1.Visible = True
        archive.IconButton16.Visible = False
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub


    Private Sub IconButton47_Click(sender As Object, e As EventArgs) Handles IconButton47.Click
        Label62.Text = "no"
        Try
            conn2.Close()
            conn2.Open()
            anciencrdt = "-"
            Dim mxcrdt As Decimal = 0
            'mxCredt.ExecuteScalar
            conn2.Close()
            If mxcrdt = 0 Then
                If DataGridView3.Rows.Count <= 0 Then
                    MsgBox(“Ticket Vide ! ”)
                Else

                    If ComboBox1.Text <> "" Then


                        If Label17.Text = 1 Then
                            Dim input As String = InputBox("Veuillez entrer le nombre de copies à imprimer:", "Nombre de copies", "1")

                            ' Vérifier si l'utilisateur a cliqué sur "Annuler" ou n'a rien saisi
                            If String.IsNullOrEmpty(input) Then
                                Return ' Annuler l'impression si l'utilisateur n'a rien saisi ou a cliqué sur "Annuler"
                            End If

                            ' Convertir la saisie en un nombre entier
                            Dim numberOfCopies As Integer
                            If Not Integer.TryParse(input, numberOfCopies) Then
                                MessageBox.Show("Veuillez entrer un nombre valide pour le nombre de copies.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Return ' Annuler l'impression en cas de saisie invalide
                            End If
                            If ComboBox2.Text = "comptoir" Or ComboBox2.Text = "Comptoir" Or ComboBox2.Text = "Client fidèle" Then
                                Casef = True
                                tfraisN = "Coût Logistique"
                                tfraisM = 0
                                Livr = "-"
                            Else
                                frais.ShowDialog()

                            End If
                            Dim cmd As MySqlCommand
                            Dim cmd2, cmd3 As MySqlCommand
                            If Casef = True Then


                                Dim sql As String
                                Dim sql2, sql3 As String
                                Dim execution As Integer

                                If Label18.Visible = True Then
                                    conn2.Close()
                                    conn2.Open()
                                    cmd = New MySqlCommand("DELETE FROM `orderdetails` WHERE type = 'ventes' and `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `orders` WHERE `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()
                                    conn2.Close()

                                End If

                                adpt = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
                                Dim table As New DataTable
                                adpt.Fill(table)

                                adpt = New MySqlDataAdapter("SELECT id from bondelivr ORDER BY id DESC LIMIT 1", conn2)
                                Dim tablebl As New DataTable
                                adpt.Fill(tablebl)
                                Dim rms As String
                                If TextBox1.Text = "" Then
                                    rms = 0
                                Else
                                    rms = Label7.Text * TextBox1.Text / 100

                                End If

                                Dim reste As String
                                Dim client As String
                                Dim payer As String



                                If Label9.Text = "Reste :" Then
                                    reste = Convert.ToDouble(Label6.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(tfraisM)
                                    payer = 0

                                Else
                                    reste = 0

                                    payer = Convert.ToDouble(Label6.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(tfraisM)
                                End If

                                If newClient = True Then
                                    client = ComboBox2.Text
                                Else
                                    client = ComboBox2.Text
                                End If

                                conn2.Close()
                                conn2.Open()
                                Dim id As Integer
                                If table.Rows().Count = 0 Then
                                    id = 1

                                Else
                                    id = table.Rows(0).Item(0) + 1

                                End If
                                Dim blnum As Integer

                                If tablebl.Rows().Count = 0 Then
                                    blnum = 1

                                Else
                                    blnum = tablebl.Rows(0).Item(0) + 1

                                End If
                                If ComboBox2.Text <> "comptoir" AndAlso ComboBox2.Text <> "Comptoir" Then
                                    sql2 = "INSERT INTO bondelivr (`order_id`) 
                    VALUES ('" & id & "')"
                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()
                                    CheckBox2.Checked = False
                                End If

                                Dim remiseprc As Decimal = 0

                                If TextBox21.Text <> "" Then
                                    remiseprc = TextBox21.Text
                                End If
                                If Label28.Text = "numbl" Then
                                    adpt = New MySqlDataAdapter("select OrderID from orderssauv where OrderID = '" & id & "' and client = '" & client & "' ", conn2)
                                    Dim tablesauv As New DataTable
                                    adpt.Fill(tablesauv)
                                    If tablesauv.Rows.Count <> 0 Then
                                        cmd2 = New MySqlCommand("DELETE FROM `orderssauv` where `OrderID` = '" & id & "' ", conn2)
                                        cmd2.ExecuteNonQuery()

                                        cmd2 = New MySqlCommand("DELETE FROM `orderdetailssauv` where `OrderID` = '" & id & "' ", conn2)
                                        cmd2.ExecuteNonQuery()
                                    Else
                                        adpt = New MySqlDataAdapter("select OrderID from orderssauv where OrderID = '" & id & "'", conn2)
                                        Dim tablesauv2 As New DataTable
                                        adpt.Fill(tablesauv2)
                                        If tablesauv2.Rows.Count <> 0 Then
                                            id = id + 1
                                        End If
                                    End If
                                    sql2 = "INSERT INTO orders (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",") + "','" + Label4.Text.ToString.Replace(".", ",") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste + "','" + tfraisM + "','" & Livr & "')"
                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()

                                    sql2 = "INSERT INTO orderssauv (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",") + "','" + Label4.Text.ToString.Replace(".", ",") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste + "','" + tfraisM + "','" & Livr & "')"
                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()

                                    idasync = id

                                Else
                                    id = Label28.Text
                                    Dim clot As String = 0
                                    If Convert.ToDateTime(Label29.Text).ToString("yyyy-MM-dd") = DateTime.Now.ToString("yyyy-MM-dd") Then
                                    Else

                                        clot = 1
                                    End If
                                    cmd = New MySqlCommand("DELETE FROM `orderdetails` WHERE `OrderID` = '" & id & "' ", conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `orders` WHERE `OrderID` = '" & id & "' ", conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `cheques` WHERE `tick` = '" & id & "' ", conn2)
                                    cmd.ExecuteNonQuery()
                                    sql2 = "INSERT INTO orders (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`,`fac_id`,`cloture`) 
                    VALUES ('" & id & "', '" + Convert.ToDateTime(Label29.Text).ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",") + "','" + Label4.Text.ToString.Replace(".", ",") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste + "','" + tfraisM + "','" & Livr & "','" & Label55.Text & "','" & clot & "')"

                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()

                                    cmd = New MySqlCommand("DELETE FROM `orderdetailssauv` WHERE `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `orderssauv` WHERE `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()

                                    sql2 = "INSERT INTO orderssauv (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`,`fac_id`,`cloture`) 
                    VALUES ('" & id & "', '" + Convert.ToDateTime(Label29.Text).ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",") + "','" + Label4.Text.ToString.Replace(".", ",") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste + "','" + tfraisM + "','" & Livr & "','" & Label55.Text & "','" & clot & "')"

                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()

                                    idasync = id

                                    If ComboBox1.Text = "Crédit" Then

                                        If Label55.Text <> "-" Then

                                            cmd3 = New MySqlCommand("UPDATE `facture` SET `paye`= '0',`etat`= 'Non Payé', `reste` = '" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "'  WHERE OrderID = '" + Label55.Text + "'", conn2)
                                            cmd3.Parameters.Clear()
                                            cmd3.ExecuteNonQuery()

                                        End If
                                    Else

                                        If Label55.Text <> "-" Then

                                            cmd3 = New MySqlCommand("UPDATE `facture` SET `paye`= '" & payer.ToString.Replace(".", ",") & "',`etat`= 'Payé', `reste` = '0'  WHERE OrderID = '" + Label55.Text + "'", conn2)
                                            cmd3.Parameters.Clear()
                                            cmd3.ExecuteNonQuery()

                                        End If
                                    End If


                                End If
                                If ComboBox1.Text = "TPE" Then
                                    adpt = New MySqlDataAdapter("SELECT organisme FROM `banques` WHERE `tpe` = '1'", conn2)
                                    Dim tabletpe As New DataTable
                                    adpt.Fill(tabletpe)
                                    sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `tick`,`bq`,`montant`,`client`) 
                    VALUES ('tpe','" + TextBox10.Text + "','tpe','" + TextBox9.Text + "','tpe', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" & id & "','" + tabletpe.Rows(0).Item(0) + "','" + Convert.ToDecimal(Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + tfraisM).ToString("# ##0.00") + "','" & ComboBox2.Text & "')"
                                    cmd3 = New MySqlCommand(sql3, conn2)
                                    cmd3.Parameters.Clear()
                                    cmd3.ExecuteNonQuery()
                                End If
                                sql3 = "INSERT INTO livreurs_cout (`name`, `cout`, `order_id`,`date`) 
                    VALUES ('" & Livr & "','" & tfraisM & "','" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')"
                                cmd3 = New MySqlCommand(sql3, conn2)
                                cmd3.Parameters.Clear()
                                cmd3.ExecuteNonQuery()

                                adpt = New MySqlDataAdapter("select * from fideles WHERE code = '" + TextBox8.Text + "'", conn2)
                                Dim table3 As New DataTable
                                adpt.Fill(table3)
                                If table3.Rows.Count() <> 0 Then
                                    If table3.Rows(0).Item(2) < 5 Then
                                        cmd3 = New MySqlCommand("UPDATE `fideles` SET `lastachats`= '" + TextBox21.Text.ToString + "', `achats` = `achats` + '" & Convert.ToDecimal(Label23.Text - (Label23.Text * (TextBox21.Text.Replace(".", ",") / 100))).ToString("#0.00") & "'  WHERE code = '" + TextBox8.Text + "'", conn2)
                                        cmd3.Parameters.Clear()
                                        cmd3.ExecuteNonQuery()
                                    End If

                                    If table3.Rows(0).Item(2) >= 5 Then
                                        cmd3 = New MySqlCommand("UPDATE `fideles` SET `lastachats`= '" + TextBox21.Text.ToString + "', `achats` = '" + Convert.ToDecimal(Label23.Text - (Label23.Text * (TextBox21.Text.Replace(".", ",") / 100))).ToString("#0.00") + "'  WHERE code = '" + TextBox8.Text + "'", conn2)
                                        cmd3.Parameters.Clear()
                                        cmd3.ExecuteNonQuery()
                                    End If

                                End If

                                For Each dr As DataGridViewRow In Me.DataGridView3.Rows
                                    conn2.Close()
                                    conn2.Open()
                                    Try
                                        adpt = New MySqlDataAdapter("select * from article where Code = '" + dr.Cells(4).Value.ToString + "' and mask = '0'", conn2)
                                        Dim table2 As New DataTable
                                        adpt.Fill(table2)
                                        Dim marg As Decimal = 0
                                        If table2.Rows.Count <> 0 Then
                                            If Label28.Text = "numbl" Then
                                                stck = Convert.ToDecimal(table2.Rows(0).Item(8)) - Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                            Else
                                                If Convert.ToDecimal(dr.Cells(2).Value.ToString.Replace(" ", "")) < 0 Then
                                                    stck = Convert.ToDecimal(table2.Rows(0).Item(8)) + Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                                Else
                                                    stck = Convert.ToDecimal(table2.Rows(0).Item(8)) - Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                                End If
                                            End If
                                            marg = (((Convert.ToDecimal(dr.Cells(10).Value.ToString.Replace(".", ",")) - (Convert.ToDecimal(dr.Cells(10).Value.ToString.Replace(".", ",")) * (Convert.ToDecimal(dr.Cells(8).Value.ToString.Replace(".", ",")) / 100))) - Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")))) * (Convert.ToDecimal(dr.Cells(2).Value.ToString.Replace(".", ",")) - Convert.ToDecimal(dr.Cells(9).Value.ToString.Replace(".", ",")))
                                            If marg <= 0 Then
                                                marg = 0
                                            End If
                                        End If
                                        cmd3 = New MySqlCommand("UPDATE `article` SET `Stock`= '" + stck.ToString + "', `sell` = 1  WHERE Code = '" + dr.Cells(4).Value.ToString + "'", conn2)
                                        cmd3.ExecuteNonQuery()
                                        cmd = New MySqlCommand
                                        prod = dr.Cells(5).Value
                                        sql = “INSERT INTO orderdetails (OrderID ,ProductID ,Price,Quantity,Total,date,pa,type,name,marge,rms,gr,pv,tva) VALUES (@tick, @code, @price, @qty, @montant, @date,@pa,@type,@name,@mrg,@rms,@gr,@pv,@tva);”
                                        sql2 = “INSERT INTO orderdetailssauv (OrderID ,ProductID ,Price,Quantity,Total,date,pa,type,name,marge,rms,gr,pv,tva) VALUES (@tick, @code, @price, @qty, @montant, @date,@pa,@type,@name,@mrg,@rms,@gr,@pv,@tva);”

                                        With cmd
                                            .Connection = conn2
                                            .CommandText = sql
                                            .Parameters.Clear()
                                            .Parameters.AddWithValue(“@tick”, id)

                                            .Parameters.AddWithValue(“@code”, dr.Cells(4).Value)
                                            .Parameters.AddWithValue(“@price”, Convert.ToDecimal(dr.Cells(3).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@qty”, Convert.ToDecimal(dr.Cells(2).Value).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@montant”, Convert.ToDecimal(dr.Cells(6).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@date”, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                            .Parameters.AddWithValue(“@pa”, Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@type”, "ventes")
                                            .Parameters.AddWithValue(“@name”, dr.Cells(1).Value)
                                            .Parameters.AddWithValue(“@mrg”, Convert.ToDecimal(marg.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@rms”, dr.Cells(8).Value)
                                            .Parameters.AddWithValue(“@gr”, dr.Cells(9).Value)
                                            .Parameters.AddWithValue(“@pv”, dr.Cells(10).Value)
                                            .Parameters.AddWithValue(“@tva”, dr.Cells(11).Value)
                                            execution = .ExecuteNonQuery()

                                            .CommandText = sql2
                                            .Parameters.Clear()
                                            .Parameters.AddWithValue(“@tick”, id)

                                            .Parameters.AddWithValue(“@code”, dr.Cells(4).Value)
                                            .Parameters.AddWithValue(“@price”, Convert.ToDecimal(dr.Cells(3).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@qty”, Convert.ToDecimal(dr.Cells(2).Value).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@montant”, Convert.ToDecimal(dr.Cells(6).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@date”, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                            .Parameters.AddWithValue(“@pa”, Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@type”, "ventes")
                                            .Parameters.AddWithValue(“@name”, dr.Cells(1).Value)
                                            .Parameters.AddWithValue(“@mrg”, Convert.ToDecimal(marg.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@rms”, dr.Cells(8).Value)
                                            .Parameters.AddWithValue(“@gr”, dr.Cells(9).Value)
                                            .Parameters.AddWithValue(“@pv”, dr.Cells(10).Value)
                                            .Parameters.AddWithValue(“@tva”, dr.Cells(11).Value)
                                            execution = .ExecuteNonQuery()
                                        End With
                                        conn2.Close()

                                    Catch ex As MySqlException
                                        MsgBox(ex.Message)
                                    End Try

                                Next
                                Dim sumqty As Decimal


                                adpt = New MySqlDataAdapter("SELECT reste FROM `orders` WHERE `client` = '" & ComboBox2.Text & "'", conn2)
                                Dim tablecrdt As New DataTable
                                adpt.Fill(tablecrdt)
                                Dim ancreste As Decimal = 0
                                For j = 0 To tablecrdt.Rows.Count() - 1
                                    ancreste += Convert.ToDecimal(tablecrdt.Rows(j).Item(0).ToString.Replace(".", ",").Replace(" ", ""))
                                Next
                                If ComboBox2.Text = "comptoir" Then
                                    anciencrdt = 0
                                Else

                                    anciencrdt = ancreste.ToString("# ##0.00")

                                End If


                                Dim ds As New DataSet1
                                Dim dt As New DataTable
                                dt = ds.Tables("tick")
                                For i = 0 To DataGridView3.Rows.Count - 1
                                    If DataGridView3.Rows(i).Cells(9).Value = 0 Then
                                        Dim found As Boolean = False
                                        For K = 0 To dt.Rows.Count - 1
                                            If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                                                found = True
                                                dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(2).Value
                                                dt.Rows(K).Item(7) = Convert.ToDouble(dt.Rows(K).Item(7)) + DataGridView3.Rows(i).Cells(6).Value
                                                sumqty += DataGridView3.Rows(i).Cells(2).Value
                                                Exit For

                                            End If
                                        Next
                                        If Not found Then
                                            dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value, "", Convert.ToDouble(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "")).ToString("N2"), "", Convert.ToDouble(DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "")).ToString("N2"), "", Convert.ToDouble(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", "")).ToString("N2"), DataGridView3.Rows(i).Cells(7).Value)
                                            sumqty += DataGridView3.Rows(i).Cells(2).Value
                                        End If
                                    End If
                                Next
                                Dim founx As Boolean = False
                                For i = 0 To DataGridView3.Rows.Count - 1
                                    If DataGridView3.Rows(i).Cells(9).Value > 0 Then
                                        Dim found As Boolean = False
                                        For K = 0 To dt.Rows.Count - 1
                                            If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                                                found = True
                                                dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(9).Value
                                                sumqty += DataGridView3.Rows(i).Cells(9).Value
                                                Exit For

                                            End If
                                        Next
                                        If Not found Then
                                            dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value & " (Gratuit)", "", DataGridView3.Rows(i).Cells(9).Value, "", 0, "", 0, 0)
                                            sumqty += DataGridView3.Rows(i).Cells(9).Value
                                        End If
                                    End If
                                Next

                                Try

                                    Casef = True
                                    If Livr = "Livreur" Then
                                        tfraisM = 0
                                    End If


                                Catch ex As Exception
                                    MsgBox(ex.Message)
                                End Try
                                Try
                                    If IconButton3.Text = "Avec BL" Then

                                    End If


                                    If ComboBox2.Text <> "comptoir" AndAlso ComboBox2.Text <> "Comptoir" Then

                                        ReportToPrint = New LocalReport()
                                        ReportToPrint.ReportPath = Application.StartupPath + "\Report4.rdlc"
                                        ReportToPrint.DataSources.Clear()
                                        ReportToPrint.EnableExternalImages = True

                                        Dim clt As String

                                        If ComboBox2.Text = "" Then
                                            clt = "comptoir"
                                        Else
                                            clt = ComboBox2.Text
                                        End If

                                        Dim cliente As New ReportParameter("client", clt)
                                        Dim client1(0) As ReportParameter
                                        client1(0) = cliente
                                        ReportToPrint.SetParameters(client1)

                                        Dim ide As New ReportParameter("id", blnum)
                                        Dim id1(0) As ReportParameter
                                        id1(0) = ide
                                        ReportToPrint.SetParameters(id1)

                                        Dim vt As New ReportParameter("vente", id)
                                        Dim vt1(0) As ReportParameter
                                        vt1(0) = vt
                                        ReportToPrint.SetParameters(vt1)

                                        Dim daty As New ReportParameter("date", DateTime.Now.ToString("dd/MM/yyyy"))
                                        Dim daty1(0) As ReportParameter
                                        daty1(0) = daty
                                        ReportToPrint.SetParameters(daty1)

                                        Dim fraisN As New ReportParameter("FraisN", tfraisN)
                                        Dim fraisN1(0) As ReportParameter
                                        fraisN1(0) = fraisN
                                        ReportToPrint.SetParameters(fraisN1)

                                        Dim fraisM As New ReportParameter("FraisM", tfraisM)
                                        Dim fraisM1(0) As ReportParameter
                                        fraisM1(0) = fraisM
                                        ReportToPrint.SetParameters(fraisM1)

                                        Dim caissier As New ReportParameter("caissier", Label2.Text)
                                        Dim caissier1(0) As ReportParameter
                                        caissier1(0) = caissier
                                        ReportToPrint.SetParameters(caissier1)

                                        Dim total As New ReportParameter("total", Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") + Convert.ToDecimal(tfraisM.Replace(".", ","))).ToString("# ##0.00"))
                                        Dim total1(0) As ReportParameter
                                        total1(0) = total
                                        ReportToPrint.SetParameters(total1)

                                        Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
                                        Dim table23 As New DataTable
                                        adpt23.Fill(table23)

                                        Dim info As New ReportParameter("info", table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString & vbCrLf & table23.Rows(0).Item(6).ToString)
                                        Dim info1(0) As ReportParameter
                                        info1(0) = info
                                        ReportToPrint.SetParameters(info1)

                                        Dim livreur As New ReportParameter("livreur", Livr)
                                        Dim livreur1(0) As ReportParameter
                                        livreur1(0) = livreur
                                        ReportToPrint.SetParameters(livreur1)


                                        Dim count As New ReportParameter("count", dt.Rows.Count.ToString)
                                        Dim count1(0) As ReportParameter
                                        count1(0) = count
                                        ReportToPrint.SetParameters(count1)

                                        Dim qty As New ReportParameter("qty", sumqty.ToString)
                                        Dim qty1(0) As ReportParameter
                                        qty1(0) = qty
                                        ReportToPrint.SetParameters(qty1)

                                        Dim appPath As String = Application.StartupPath()

                                        Dim SaveDirectory As String = appPath & "\"
                                        Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                                        Dim img As New ReportParameter("img", "File:\" + SavePath, True)
                                        Dim img1(0) As ReportParameter
                                        img1(0) = img
                                        ReportToPrint.SetParameters(img1)

                                        ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

                                        'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
                                        'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                                        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
                                        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
                                        'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
                                        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>5.83in</PageWidth><PageHeight>8.3in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
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
                                        Dim printname As String = a5printer
                                        printDoc.PrinterSettings.PrinterName = printname
                                        Dim ps As New PrinterSettings()
                                        ps.PrinterName = printDoc.PrinterSettings.PrinterName
                                        printDoc.PrinterSettings = ps

                                        AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
                                        m_currentPageIndex = 0
                                        printDoc.DefaultPageSettings.PaperSize = New PaperSize("A5", 583, 827)
                                        ' Demander à l'utilisateur le nombre de copies à imprimer

                                        printDoc.PrinterSettings.Copies = numberOfCopies
                                        ' Vérifier et ajuster les marges par défaut de l'imprimante si nécessaire
                                        ps.DefaultPageSettings.Margins = New Margins(0, 0, 0, 0)
                                        printDoc.Print()

                                    End If

                                Catch ex As Exception
                                    MsgBox(ex.Message)
                                End Try

                                If tpevar = True Then
                                    Dim ds2 As New DataSet1
                                    Dim dt2 As New DataTable
                                    dt2 = ds2.Tables("tick")
                                    sumqty = 0
                                    For i = 0 To DataGridView3.Rows.Count - 1
                                        If DataGridView3.Rows(i).Cells(9).Value = 0 Then
                                            dt2.Rows.Add("", DataGridView3.Rows(i).Cells(1).Value, "", DataGridView3.Rows(i).Cells(2).Value, "", DataGridView3.Rows(i).Cells(3).Value, "", DataGridView3.Rows(i).Cells(6).Value, DataGridView3.Rows(i).Cells(7).Value)
                                            sumqty += DataGridView3.Rows(i).Cells(2).Value
                                        End If
                                    Next
                                    For i = 0 To DataGridView3.Rows.Count - 1
                                        If DataGridView3.Rows(i).Cells(9).Value > 0 Then
                                            dt2.Rows.Add("", DataGridView3.Rows(i).Cells(1).Value & " (Gratuit)", "", DataGridView3.Rows(i).Cells(9).Value, "", 0, "", 0, 0)
                                            sumqty += DataGridView3.Rows(i).Cells(9).Value
                                        End If
                                    Next

                                    Try

                                        Casef = True
                                        tfraisN = "Coût Logistique"
                                        tfraisM = 0
                                        Livr = "-"

                                        If CheckBox2.Checked Then
                                            ReportToPrint = New LocalReport()
                                            ReportToPrint.ReportPath = Application.StartupPath + "\Report1.rdlc"
                                            ReportToPrint.DataSources.Clear()
                                            ReportToPrint.EnableExternalImages = True

                                            Dim clt As String

                                            If ComboBox2.Text = "" Then
                                                clt = "comptoir"
                                            Else
                                                clt = ComboBox2.Text
                                            End If


                                            Dim lpar3 As New ReportParameter("totale", Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") + Convert.ToDecimal(tfraisM.Replace(".", ","))).ToString("# ##0.00"))
                                            Dim lpar31(0) As ReportParameter
                                            lpar31(0) = lpar3
                                            ReportToPrint.SetParameters(lpar31)

                                            Dim Acre As New ReportParameter("CreditAncien", anciencrdt)
                                            Dim Acre1(0) As ReportParameter
                                            Acre1(0) = Acre
                                            ReportToPrint.SetParameters(Acre1)



                                            Dim cliente As New ReportParameter("client", clt)
                                            Dim client1(0) As ReportParameter
                                            client1(0) = cliente
                                            ReportToPrint.SetParameters(client1)

                                            Dim ide As New ReportParameter("id", tc)
                                            Dim id1(0) As ReportParameter
                                            id1(0) = ide
                                            ReportToPrint.SetParameters(id1)

                                            Dim tpe As New ReportParameter("tpe", "N° STAN : " & TextBox9.Text & vbCrLf & "N° Autorisation : " & TextBox10.Text)
                                            Dim tpe1(0) As ReportParameter
                                            tpe1(0) = tpe
                                            ReportToPrint.SetParameters(tpe1)

                                            Dim daty As New ReportParameter("date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                            Dim daty1(0) As ReportParameter
                                            daty1(0) = daty
                                            ReportToPrint.SetParameters(daty1)

                                            Dim mode As New ReportParameter("mode", ComboBox1.Text)
                                            Dim mode1(0) As ReportParameter
                                            mode1(0) = mode
                                            ReportToPrint.SetParameters(mode1)

                                            Dim caissier As New ReportParameter("caissier", Label2.Text)
                                            Dim caissier1(0) As ReportParameter
                                            caissier1(0) = caissier
                                            ReportToPrint.SetParameters(caissier1)

                                            Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
                                            Dim table23 As New DataTable
                                            adpt23.Fill(table23)

                                            Dim info As New ReportParameter("info", table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString)
                                            Dim info1(0) As ReportParameter
                                            info1(0) = info
                                            ReportToPrint.SetParameters(info1)


                                            Dim msg As New ReportParameter("msg", table23.Rows(0).Item(6).ToString)
                                            Dim msg1(0) As ReportParameter
                                            msg1(0) = msg
                                            ReportToPrint.SetParameters(msg1)

                                            Dim count As New ReportParameter("count", DataGridView3.Rows.Count.ToString)
                                            Dim count1(0) As ReportParameter
                                            count1(0) = count
                                            ReportToPrint.SetParameters(count1)

                                            Dim fraisN As New ReportParameter("FraisN", tfraisN)
                                            Dim fraisN1(0) As ReportParameter
                                            fraisN1(0) = fraisN
                                            ReportToPrint.SetParameters(fraisN1)

                                            Dim fraisM As New ReportParameter("FraisM", tfraisM)
                                            Dim fraisM1(0) As ReportParameter
                                            fraisM1(0) = fraisM
                                            ReportToPrint.SetParameters(fraisM1)

                                            Dim livreur As New ReportParameter("livreur", Livr)
                                            Dim livreur1(0) As ReportParameter
                                            livreur1(0) = livreur
                                            ReportToPrint.SetParameters(livreur1)

                                            Dim qty As New ReportParameter("qty", sumqty.ToString)
                                            Dim qty1(0) As ReportParameter
                                            qty1(0) = qty
                                            ReportToPrint.SetParameters(qty1)


                                            Dim appPath As String = Application.StartupPath()

                                            Dim SaveDirectory As String = appPath & "\"
                                            Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                                            Dim img As New ReportParameter("image", "File:\" + SavePath, True)
                                            Dim img1(0) As ReportParameter
                                            img1(0) = img
                                            ReportToPrint.SetParameters(img1)

                                            ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

                                            'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
                                            'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                                            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
                                            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
                                            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
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
                                            'tbl.Rows.Clear()
                                            'tbl2.Rows.Clear()

                                        End If
                                    Catch ex As Exception
                                        MsgBox(ex.Message)
                                    End Try
                                    tpevar = False
                                End If

                                DataGridView3.Rows.Clear()
                                Label28.Text = "numbl"
                                Label30.Text = 0
                                Label32.Text = "Sans livraison"
                                Dim adpt29 As New MySqlDataAdapter("select * from infos", conn2)
                                Dim table29 As New DataTable
                                adpt29.Fill(table29)
                                Dim gifttop As Decimal = table29.Rows(0).Item(24)
                                If Label6.Text >= gifttop Then
                                    adpt = New MySqlDataAdapter("select * from gifts", conn2)
                                    Dim tablegift As New DataTable
                                    adpt.Fill(tablegift)
                                    If tablegift.Rows.Count() <> 0 Then
                                        gift.ShowDialog()

                                    End If

                                End If
                                Label6.Text = 0.ToString("# ##0.00")
                                Try
                                    If IsNumeric(TextBox3.Text) = False Then
                                        If Label9.Text = "Rendu :" Then
                                            Label4.Text = 0.00
                                        End If
                                        If Label9.Text = "Reste :" Then
                                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                                        End If
                                    Else
                                        If Label9.Text = "Rendu :" Then
                                            Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                                        End If
                                        If Label9.Text = "Reste :" Then
                                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                                        End If
                                    End If
                                Catch ex As MySqlException
                                    MsgBox(ex.Message)
                                End Try
                                Label23.Text = 0.ToString("# ##0.00")
                                Label24.Text = 0.ToString("# ##0.00")
                                Label25.Text = 0.ToString("# ##0.00")
                                TextBox21.Text = ""

                                Label7.Text = 0.ToString("# ##0.00")
                                Label4.Text = 0
                                TextBox1.Text = 0
                                TextBox3.Text = ""
                                TextBox8.Text = ""
                                Panel8.Visible = False
                                IconButton52.PerformClick()
                                ComboBox2.SelectedItem = "comptoir"
                                ComboBox1.SelectedItem = "Espèce"
                                IconButton31.BackColor = Color.DarkOrange
                                IconButton31.ForeColor = Color.White
                                ComboBox1.Text = "Espèce"

                                IconButton32.BackColor = Color.White
                                IconButton32.ForeColor = Color.Black

                                IconButton33.BackColor = Color.White
                                IconButton33.ForeColor = Color.Black
                                TextBox2.Select()
                                TextBox2.Text = ""
                                Label18.Visible = False

                                adpt = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
                                Dim tabletick As New DataTable
                                adpt.Fill(tabletick)
                                Dim idlast As Integer
                                If tabletick.Rows().Count = 0 Then
                                    idlast = 1

                                Else
                                    idlast = tabletick.Rows(0).Item(0) + 1

                                End If
                                Label20.Text = "Ticket N° " & idlast.ToString
                                'IconButton24.PerformClick()
                                Casef = False
                                CheckBox2.Checked = True
                                If ComboBox1.Text = "Espèce" Then
                                    Dim codeOpenCashDrawer As Byte() = New Byte() {27, 112, 48, 55, 121}
                                    Dim rw As RawPrinterHelper = New RawPrinterHelper
                                    Dim pUnmanagedBytes As New IntPtr(0)
                                    pUnmanagedBytes = Marshal.AllocCoTaskMem(5)
                                    Dim printname As String = a5printer
                                    Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5)
                                    rw.SendBytesToPrinter(printname, pUnmanagedBytes, 5)
                                    Marshal.FreeCoTaskMem(pUnmanagedBytes)
                                End If
                            End If
                        End If
                    Else
                        MsgBox("Merci de choisir le mode de paiement")

                    End If
                End If

            Else
                MsgBox("Ce client a déjà atteind le maximum du crédit")
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        TextBox2.Select()
        TextBox2.Text = ""
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub IconButton48_Click(sender As Object, e As EventArgs) Handles IconButton48.Click
        Label62.Text = "no"
        Try
            conn2.Close()
            conn2.Open()
            anciencrdt = "-"
            Dim mxcrdt As Decimal = 0
            'mxCredt.ExecuteScalar
            conn2.Close()
            If mxcrdt = 0 Then
                If DataGridView3.Rows.Count <= 0 Then
                    MsgBox(“Ticket Vide ! ”)
                Else

                    If ComboBox1.Text <> "" Then


                        If Label17.Text = 1 Then
                            If ComboBox2.Text = "comptoir" Or ComboBox2.Text = "Comptoir" Or ComboBox2.Text = "Client fidèle" Then
                                Casef = True
                                tfraisN = "Coût Logistique"
                                tfraisM = 0
                                Livr = "-"
                            Else
                                frais.ShowDialog()

                            End If

                            Dim cmd As MySqlCommand
                            Dim cmd2, cmd3 As MySqlCommand
                            If Casef = True Then




                                Dim sql As String
                                Dim sql2, sql3 As String
                                Dim execution As Integer

                                If Label18.Visible = True Then
                                    conn2.Close()
                                    conn2.Open()
                                    cmd = New MySqlCommand("DELETE FROM `orderdetails` WHERE type = 'ventes' and `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `orders` WHERE `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()
                                    conn2.Close()

                                End If

                                adpt = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
                                Dim table As New DataTable
                                adpt.Fill(table)

                                adpt = New MySqlDataAdapter("SELECT id from bondelivr ORDER BY id DESC LIMIT 1", conn2)
                                Dim tablebl As New DataTable
                                adpt.Fill(tablebl)
                                Dim rms As String
                                If TextBox1.Text = "" Then
                                    rms = 0
                                Else
                                    rms = Label7.Text * TextBox1.Text / 100

                                End If

                                Dim reste As String
                                Dim client As String
                                Dim payer As String



                                If Label9.Text = "Reste :" Then
                                    If TextBox3.Text = "" Then
                                        payer = 0
                                        reste = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(tfraisM)


                                    Else
                                        If TextBox3.Text <= 0 Then
                                            payer = 0
                                            reste = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(tfraisM)
                                        End If
                                        If TextBox3.Text >= Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(tfraisM) Then
                                            payer = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(tfraisM)
                                            reste = 0
                                        Else
                                            payer = TextBox3.Text
                                            reste = Convert.ToDouble(Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(tfraisM)) - payer
                                        End If
                                    End If

                                Else
                                    reste = 0

                                    payer = Convert.ToDecimal(Label6.Text) + Convert.ToDecimal(tfraisM)
                                End If

                                If newClient = True Then
                                    client = ComboBox2.Text
                                Else
                                    client = ComboBox2.Text
                                End If

                                conn2.Close()
                                conn2.Open()
                                Dim id As Integer
                                If table.Rows().Count = 0 Then
                                    id = 1

                                Else
                                    id = table.Rows(0).Item(0) + 1

                                End If
                                Dim blnum As Integer

                                If tablebl.Rows().Count = 0 Then
                                    blnum = 1

                                Else
                                    blnum = tablebl.Rows(0).Item(0) + 1

                                End If
                                If ComboBox2.Text <> "comptoir" AndAlso ComboBox2.Text <> "Comptoir" Then
                                    sql2 = "INSERT INTO bondelivr (`order_id`) 
                    VALUES ('" & id & "')"
                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()
                                    CheckBox2.Checked = False
                                End If

                                Dim remiseprc As Decimal = 0

                                If TextBox21.Text <> "" Then
                                    remiseprc = TextBox21.Text
                                End If
                                If Label28.Text = "numbl" Then
                                    adpt = New MySqlDataAdapter("select OrderID from orderssauv where OrderID = '" & id & "' and client = '" & client & "' ", conn2)
                                    Dim tablesauv As New DataTable
                                    adpt.Fill(tablesauv)
                                    If tablesauv.Rows.Count <> 0 Then
                                        cmd2 = New MySqlCommand("DELETE FROM `orderssauv` where `OrderID` = '" & id & "' ", conn2)
                                        cmd2.ExecuteNonQuery()

                                        cmd2 = New MySqlCommand("DELETE FROM `orderdetailssauv` where `OrderID` = '" & id & "' ", conn2)
                                        cmd2.ExecuteNonQuery()
                                    Else
                                        adpt = New MySqlDataAdapter("select OrderID from orderssauv where OrderID = '" & id & "'", conn2)
                                        Dim tablesauv2 As New DataTable
                                        adpt.Fill(tablesauv2)
                                        If tablesauv2.Rows.Count <> 0 Then
                                            id = id + 1
                                        End If
                                    End If

                                    sql2 = "INSERT INTO orders (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",") + "','" + Label4.Text.ToString.Replace(".", ",") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste + "','" + tfraisM + "','" & Livr & "')"
                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()

                                    sql2 = "INSERT INTO orderssauv (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",") + "','" + Label4.Text.ToString.Replace(".", ",") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste + "','" + tfraisM + "','" & Livr & "')"
                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()
                                    idasync = id
                                Else
                                    id = Label28.Text
                                    Dim clot As String = 0
                                    If Convert.ToDateTime(Label29.Text).ToString("yyyy-MM-dd") = DateTime.Now.ToString("yyyy-MM-dd") Then
                                    Else

                                        clot = 1
                                    End If
                                    cmd = New MySqlCommand("DELETE FROM `orderdetails` WHERE `OrderID` = '" & id & "' ", conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `orders` WHERE `OrderID` = '" & id & "' ", conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `cheques` WHERE `tick` = '" & id & "' ", conn2)
                                    cmd.ExecuteNonQuery()
                                    sql2 = "INSERT INTO orders (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`,`fac_id`,`cloture`) 
                    VALUES ('" & id & "', '" + Convert.ToDateTime(Label29.Text).ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",") + "','" + Label4.Text.ToString.Replace(".", ",") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste + "','" + tfraisM + "','" & Livr & "','" & Label55.Text & "','" & clot & "')"

                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()


                                    cmd = New MySqlCommand("DELETE FROM `orderdetailssauv` WHERE `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `orderssauv` WHERE `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()



                                    sql2 = "INSERT INTO orderssauv (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`,`fac_id`,`cloture`) 
                    VALUES ('" & id & "', '" + Convert.ToDateTime(Label29.Text).ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",") + "','" + Label4.Text.ToString.Replace(".", ",") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste + "','" + tfraisM + "','" & Livr & "','" & Label55.Text & "','" & clot & "')"

                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()

                                    idasync = id

                                    If ComboBox1.Text = "Crédit" Then

                                        If Label55.Text <> "-" Then

                                            cmd3 = New MySqlCommand("UPDATE `facture` SET `paye`= '0',`etat`= 'Non Payé', `reste` = '" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "'  WHERE OrderID = '" + Label55.Text + "'", conn2)
                                            cmd3.Parameters.Clear()
                                            cmd3.ExecuteNonQuery()

                                        End If
                                    Else

                                        If Label55.Text <> "-" Then

                                            cmd3 = New MySqlCommand("UPDATE `facture` SET `paye`= '" & payer.ToString.Replace(".", ",") & "',`etat`= 'Payé', `reste` = '0'  WHERE OrderID = '" + Label55.Text + "'", conn2)
                                            cmd3.Parameters.Clear()
                                            cmd3.ExecuteNonQuery()

                                        End If
                                    End If

                                End If
                                If ComboBox1.Text = "TPE" Then
                                    adpt = New MySqlDataAdapter("SELECT organisme FROM `banques` WHERE `tpe` = '1'", conn2)
                                    Dim tabletpe As New DataTable
                                    adpt.Fill(tabletpe)

                                    sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `tick`,`bq`,`montant`,`client`) 
                    VALUES ('tpe','" + TextBox10.Text + "','tpe','" + TextBox9.Text + "','tpe', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" & id & "','" + tabletpe.Rows(0).Item(0) + "','" + Convert.ToDecimal(Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + tfraisM).ToString("# ##0.00") + "','" & ComboBox2.Text & "')"
                                    cmd3 = New MySqlCommand(sql3, conn2)
                                    cmd3.Parameters.Clear()
                                    cmd3.ExecuteNonQuery()

                                End If
                                sql3 = "INSERT INTO livreurs_cout (`name`, `cout`, `order_id`,`date`) 
                    VALUES ('" & Livr & "','" & tfraisM & "','" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')"
                                cmd3 = New MySqlCommand(sql3, conn2)
                                cmd3.Parameters.Clear()
                                cmd3.ExecuteNonQuery()

                                adpt = New MySqlDataAdapter("select * from fideles WHERE code = '" + TextBox8.Text + "'", conn2)
                                Dim table3 As New DataTable
                                adpt.Fill(table3)
                                If table3.Rows.Count() <> 0 Then
                                    If table3.Rows(0).Item(2) < 5 Then
                                        cmd3 = New MySqlCommand("UPDATE `fideles` SET `lastachats`= '" + TextBox21.Text.ToString + "', `achats` = `achats` + '" & Convert.ToDecimal(Label23.Text - (Label23.Text * (TextBox21.Text.Replace(".", ",") / 100))).ToString("#0.00") & "'  WHERE code = '" + TextBox8.Text + "'", conn2)
                                        cmd3.Parameters.Clear()
                                        cmd3.ExecuteNonQuery()
                                    End If

                                    If table3.Rows(0).Item(2) >= 5 Then
                                        cmd3 = New MySqlCommand("UPDATE `fideles` SET `lastachats`= '" + TextBox21.Text.ToString + "', `achats` = '" + Convert.ToDecimal(Label23.Text - (Label23.Text * (TextBox21.Text.Replace(".", ",") / 100))).ToString("#0.00") + "'  WHERE code = '" + TextBox8.Text + "'", conn2)
                                        cmd3.Parameters.Clear()
                                        cmd3.ExecuteNonQuery()
                                    End If

                                End If

                                For Each dr As DataGridViewRow In Me.DataGridView3.Rows
                                    conn2.Close()
                                    conn2.Open()
                                    Try
                                        adpt = New MySqlDataAdapter("select * from article where Code = '" + dr.Cells(4).Value.ToString + "' and mask = '0'", conn2)
                                        Dim table2 As New DataTable
                                        adpt.Fill(table2)
                                        Dim marg As Decimal = 0
                                        If table2.Rows.Count <> 0 Then
                                            If Label28.Text = "numbl" Then
                                                stck = Convert.ToDecimal(table2.Rows(0).Item(8)) - Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                            Else
                                                If Convert.ToDecimal(dr.Cells(2).Value.ToString.Replace(" ", "")) < 0 Then
                                                    stck = Convert.ToDecimal(table2.Rows(0).Item(8)) + Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                                Else
                                                    stck = Convert.ToDecimal(table2.Rows(0).Item(8)) - Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                                End If
                                            End If
                                            marg = (((Convert.ToDecimal(dr.Cells(10).Value.ToString.Replace(".", ",")) - (Convert.ToDecimal(dr.Cells(10).Value.ToString.Replace(".", ",")) * (Convert.ToDecimal(dr.Cells(8).Value.ToString.Replace(".", ",")) / 100))) - Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")))) * (Convert.ToDecimal(dr.Cells(2).Value.ToString.Replace(".", ",")) - Convert.ToDecimal(dr.Cells(9).Value.ToString.Replace(".", ",")))
                                            If marg <= 0 Then
                                                marg = 0
                                            End If
                                        End If
                                        cmd3 = New MySqlCommand("UPDATE `article` SET `Stock`= '" + stck.ToString + "', `sell` = 1  WHERE Code = '" + dr.Cells(4).Value.ToString + "'", conn2)
                                        cmd3.ExecuteNonQuery()

                                        cmd = New MySqlCommand
                                        prod = dr.Cells(5).Value
                                        sql = “INSERT INTO orderdetails (OrderID ,ProductID ,Price,Quantity,Total,date,pa,type,name,marge,rms,gr,pv,tva) VALUES (@tick, @code, @price, @qty, @montant, @date,@pa,@type,@name,@mrg,@rms,@gr,@pv,@tva);”
                                        sql2 = “INSERT INTO orderdetailssauv (OrderID ,ProductID ,Price,Quantity,Total,date,pa,type,name,marge,rms,gr,pv,tva) VALUES (@tick, @code, @price, @qty, @montant, @date,@pa,@type,@name,@mrg,@rms,@gr,@pv,@tva);”

                                        With cmd
                                            .Connection = conn2
                                            .CommandText = sql
                                            .Parameters.Clear()
                                            .Parameters.AddWithValue(“@tick”, id)

                                            .Parameters.AddWithValue(“@code”, dr.Cells(4).Value)
                                            .Parameters.AddWithValue(“@price”, Convert.ToDecimal(dr.Cells(3).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@qty”, Convert.ToDecimal(dr.Cells(2).Value).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@montant”, Convert.ToDecimal(dr.Cells(6).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@date”, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                            .Parameters.AddWithValue(“@pa”, Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@type”, "ventes")
                                            .Parameters.AddWithValue(“@name”, dr.Cells(1).Value)
                                            .Parameters.AddWithValue(“@mrg”, Convert.ToDecimal(marg.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@rms”, dr.Cells(8).Value)
                                            .Parameters.AddWithValue(“@gr”, dr.Cells(9).Value)
                                            .Parameters.AddWithValue(“@pv”, dr.Cells(10).Value)
                                            .Parameters.AddWithValue(“@tva”, dr.Cells(11).Value)
                                            execution = .ExecuteNonQuery()

                                            .CommandText = sql2
                                            .Parameters.Clear()
                                            .Parameters.AddWithValue(“@tick”, id)

                                            .Parameters.AddWithValue(“@code”, dr.Cells(4).Value)
                                            .Parameters.AddWithValue(“@price”, Convert.ToDecimal(dr.Cells(3).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@qty”, Convert.ToDecimal(dr.Cells(2).Value).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@montant”, Convert.ToDecimal(dr.Cells(6).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@date”, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                            .Parameters.AddWithValue(“@pa”, Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@type”, "ventes")
                                            .Parameters.AddWithValue(“@name”, dr.Cells(1).Value)
                                            .Parameters.AddWithValue(“@mrg”, Convert.ToDecimal(marg.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@rms”, dr.Cells(8).Value)
                                            .Parameters.AddWithValue(“@gr”, dr.Cells(9).Value)
                                            .Parameters.AddWithValue(“@pv”, dr.Cells(10).Value)
                                            .Parameters.AddWithValue(“@tva”, dr.Cells(11).Value)
                                            execution = .ExecuteNonQuery()
                                        End With
                                        conn2.Close()

                                    Catch ex As MySqlException
                                        MsgBox(ex.Message)
                                    End Try

                                Next
                                Dim sumqty As Decimal


                                adpt = New MySqlDataAdapter("SELECT reste FROM `orders` WHERE `client` = '" & ComboBox2.Text & "'", conn2)
                                Dim tablecrdt As New DataTable
                                adpt.Fill(tablecrdt)
                                Dim ancreste As Decimal = 0
                                For j = 0 To tablecrdt.Rows.Count() - 1
                                    ancreste += Convert.ToDecimal(tablecrdt.Rows(j).Item(0))
                                Next

                                If ComboBox2.Text = "comptoir" Then
                                    anciencrdt = 0
                                Else

                                    anciencrdt = ancreste.ToString("# ##0.00")

                                End If

                                Dim ds As New DataSet1
                                Dim dt As New DataTable
                                dt = ds.Tables("tick")
                                For i = 0 To DataGridView3.Rows.Count - 1
                                    If DataGridView3.Rows(i).Cells(9).Value = 0 Then
                                        Dim found As Boolean = False
                                        For K = 0 To dt.Rows.Count - 1
                                            If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                                                found = True
                                                dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(2).Value
                                                dt.Rows(K).Item(7) = Convert.ToDouble(dt.Rows(K).Item(7)) + DataGridView3.Rows(i).Cells(6).Value
                                                sumqty += DataGridView3.Rows(i).Cells(2).Value
                                                Exit For

                                            End If
                                        Next
                                        If Not found Then
                                            dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value, "", DataGridView3.Rows(i).Cells(2).Value, "", DataGridView3.Rows(i).Cells(3).Value, "", DataGridView3.Rows(i).Cells(6).Value, DataGridView3.Rows(i).Cells(7).Value)
                                            sumqty += DataGridView3.Rows(i).Cells(2).Value
                                        End If
                                    End If
                                Next
                                For i = 0 To DataGridView3.Rows.Count - 1
                                    If DataGridView3.Rows(i).Cells(9).Value > 0 Then
                                        Dim found As Boolean = False
                                        For K = 0 To dt.Rows.Count - 1
                                            If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                                                found = True
                                                dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(9).Value
                                                sumqty += DataGridView3.Rows(i).Cells(9).Value
                                                Exit For

                                            End If
                                        Next
                                        If Not found Then
                                            dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value & " (Gratuit)", "", DataGridView3.Rows(i).Cells(9).Value, "", 0, "", 0, 0)
                                            sumqty += DataGridView3.Rows(i).Cells(9).Value
                                        End If
                                    End If
                                Next

                                Try

                                    Casef = True
                                    If Livr = "Livreur" Then
                                        tfraisM = 0
                                    End If

                                    ReportToPrint = New LocalReport()
                                    ReportToPrint.ReportPath = Application.StartupPath + "\Report1.rdlc"
                                    ReportToPrint.DataSources.Clear()
                                    ReportToPrint.EnableExternalImages = True

                                    Dim clt As String

                                    If ComboBox2.Text = "" Then
                                        clt = "comptoir"
                                    Else
                                        clt = ComboBox2.Text
                                    End If


                                    Dim lpar3 As New ReportParameter("totale", Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") + Convert.ToDecimal(tfraisM.Replace(".", ","))).ToString("# ##0.00"))
                                    Dim lpar31(0) As ReportParameter
                                    lpar31(0) = lpar3
                                    ReportToPrint.SetParameters(lpar31)

                                    Dim Acre As New ReportParameter("CreditAncien", anciencrdt)
                                    Dim Acre1(0) As ReportParameter
                                    Acre1(0) = Acre
                                    ReportToPrint.SetParameters(Acre1)



                                    Dim cliente As New ReportParameter("client", clt)
                                    Dim client1(0) As ReportParameter
                                    client1(0) = cliente
                                    ReportToPrint.SetParameters(client1)

                                    Dim ide As New ReportParameter("id", id)
                                    Dim id1(0) As ReportParameter
                                    id1(0) = ide
                                    ReportToPrint.SetParameters(id1)


                                    Dim tpe As New ReportParameter("tpe", "")
                                    Dim tpe1(0) As ReportParameter
                                    tpe1(0) = tpe
                                    ReportToPrint.SetParameters(tpe1)

                                    Dim daty As New ReportParameter("date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                    Dim daty1(0) As ReportParameter
                                    daty1(0) = daty
                                    ReportToPrint.SetParameters(daty1)

                                    Dim mode As New ReportParameter("mode", ComboBox1.Text)
                                    Dim mode1(0) As ReportParameter
                                    mode1(0) = mode
                                    ReportToPrint.SetParameters(mode1)

                                    Dim caissier As New ReportParameter("caissier", Label2.Text)
                                    Dim caissier1(0) As ReportParameter
                                    caissier1(0) = caissier
                                    ReportToPrint.SetParameters(caissier1)

                                    Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
                                    Dim table23 As New DataTable
                                    adpt23.Fill(table23)

                                    Dim info As New ReportParameter("info", table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString)
                                    Dim info1(0) As ReportParameter
                                    info1(0) = info
                                    ReportToPrint.SetParameters(info1)


                                    Dim msg As New ReportParameter("msg", table23.Rows(0).Item(6).ToString)
                                    Dim msg1(0) As ReportParameter
                                    msg1(0) = msg
                                    ReportToPrint.SetParameters(msg1)

                                    Dim count As New ReportParameter("count", dt.Rows.Count.ToString)
                                    Dim count1(0) As ReportParameter
                                    count1(0) = count
                                    ReportToPrint.SetParameters(count1)

                                    Dim fraisN As New ReportParameter("FraisN", tfraisN)
                                    Dim fraisN1(0) As ReportParameter
                                    fraisN1(0) = fraisN
                                    ReportToPrint.SetParameters(fraisN1)

                                    Dim fraisM As New ReportParameter("FraisM", tfraisM)
                                    Dim fraisM1(0) As ReportParameter
                                    fraisM1(0) = fraisM
                                    ReportToPrint.SetParameters(fraisM1)

                                    Dim livreur As New ReportParameter("livreur", Livr)
                                    Dim livreur1(0) As ReportParameter
                                    livreur1(0) = livreur
                                    ReportToPrint.SetParameters(livreur1)

                                    Dim qty As New ReportParameter("qty", sumqty.ToString)
                                    Dim qty1(0) As ReportParameter
                                    qty1(0) = qty
                                    ReportToPrint.SetParameters(qty1)


                                    Dim appPath As String = Application.StartupPath()

                                    Dim SaveDirectory As String = appPath & "\"
                                    Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                                    Dim img As New ReportParameter("image", "File:\" + SavePath, True)
                                    Dim img1(0) As ReportParameter
                                    img1(0) = img
                                    ReportToPrint.SetParameters(img1)

                                    ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

                                    'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
                                    'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                                    'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
                                    'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
                                    'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
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
                                    adpt = New MySqlDataAdapter("select ticket from parameters", conn2)
                                    Dim params As New DataTable
                                    adpt.Fill(params)

                                    If params.Rows(0).Item(0) = "Desactive" Then
                                        printDoc.PrinterSettings.Copies = 1
                                    Else

                                        printDoc.PrinterSettings.Copies = 2
                                    End If
                                    printDoc.Print()
                                    'tbl.Rows.Clear()
                                    'tbl2.Rows.Clear()




                                Catch ex As Exception
                                    MsgBox(ex.Message)
                                End Try

                                If tpevar = True Then
                                    Dim ds2 As New DataSet1
                                    Dim dt2 As New DataTable
                                    dt2 = ds2.Tables("tick")
                                    sumqty = 0
                                    For i = 0 To DataGridView3.Rows.Count - 1
                                        If DataGridView3.Rows(i).Cells(9).Value = 0 Then
                                            dt2.Rows.Add("", DataGridView3.Rows(i).Cells(1).Value, "", DataGridView3.Rows(i).Cells(2).Value, "", DataGridView3.Rows(i).Cells(3).Value, "", DataGridView3.Rows(i).Cells(6).Value, DataGridView3.Rows(i).Cells(7).Value)
                                            sumqty += DataGridView3.Rows(i).Cells(2).Value
                                        End If
                                    Next
                                    For i = 0 To DataGridView3.Rows.Count - 1
                                        If DataGridView3.Rows(i).Cells(9).Value > 0 Then
                                            dt2.Rows.Add("", DataGridView3.Rows(i).Cells(1).Value & " (Gratuit)", "", DataGridView3.Rows(i).Cells(9).Value, "", 0, "", 0, 0)
                                            sumqty += DataGridView3.Rows(i).Cells(9).Value
                                        End If
                                    Next

                                    Try

                                        Casef = True
                                        tfraisN = "Coût Logistique"
                                        tfraisM = 0
                                        Livr = "-"

                                        If CheckBox2.Checked Then
                                            ReportToPrint = New LocalReport()
                                            ReportToPrint.ReportPath = Application.StartupPath + "\Report1.rdlc"
                                            ReportToPrint.DataSources.Clear()
                                            ReportToPrint.EnableExternalImages = True

                                            Dim clt As String

                                            If ComboBox2.Text = "" Then
                                                clt = "comptoir"
                                            Else
                                                clt = ComboBox2.Text
                                            End If


                                            Dim lpar3 As New ReportParameter("totale", Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") + Convert.ToDecimal(tfraisM.Replace(".", ","))).ToString("# ##0.00"))
                                            Dim lpar31(0) As ReportParameter
                                            lpar31(0) = lpar3
                                            ReportToPrint.SetParameters(lpar31)

                                            Dim Acre As New ReportParameter("CreditAncien", anciencrdt)
                                            Dim Acre1(0) As ReportParameter
                                            Acre1(0) = Acre
                                            ReportToPrint.SetParameters(Acre1)



                                            Dim cliente As New ReportParameter("client", clt)
                                            Dim client1(0) As ReportParameter
                                            client1(0) = cliente
                                            ReportToPrint.SetParameters(client1)

                                            Dim ide As New ReportParameter("id", tc)
                                            Dim id1(0) As ReportParameter
                                            id1(0) = ide
                                            ReportToPrint.SetParameters(id1)

                                            Dim tpe As New ReportParameter("tpe", "N° STAN : " & TextBox9.Text & vbCrLf & "N° Autorisation : " & TextBox10.Text)
                                            Dim tpe1(0) As ReportParameter
                                            tpe1(0) = tpe
                                            ReportToPrint.SetParameters(tpe1)

                                            Dim daty As New ReportParameter("date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                            Dim daty1(0) As ReportParameter
                                            daty1(0) = daty
                                            ReportToPrint.SetParameters(daty1)

                                            Dim mode As New ReportParameter("mode", ComboBox1.Text)
                                            Dim mode1(0) As ReportParameter
                                            mode1(0) = mode
                                            ReportToPrint.SetParameters(mode1)

                                            Dim caissier As New ReportParameter("caissier", Label2.Text)
                                            Dim caissier1(0) As ReportParameter
                                            caissier1(0) = caissier
                                            ReportToPrint.SetParameters(caissier1)

                                            Dim adpt23 As New MySqlDataAdapter("select * from infos", conn2)
                                            Dim table23 As New DataTable
                                            adpt23.Fill(table23)

                                            Dim info As New ReportParameter("info", table23.Rows(0).Item(2).ToString & vbCrLf & table23.Rows(0).Item(3).ToString)
                                            Dim info1(0) As ReportParameter
                                            info1(0) = info
                                            ReportToPrint.SetParameters(info1)


                                            Dim msg As New ReportParameter("msg", table23.Rows(0).Item(6).ToString)
                                            Dim msg1(0) As ReportParameter
                                            msg1(0) = msg
                                            ReportToPrint.SetParameters(msg1)

                                            Dim count As New ReportParameter("count", DataGridView3.Rows.Count.ToString)
                                            Dim count1(0) As ReportParameter
                                            count1(0) = count
                                            ReportToPrint.SetParameters(count1)

                                            Dim fraisN As New ReportParameter("FraisN", tfraisN)
                                            Dim fraisN1(0) As ReportParameter
                                            fraisN1(0) = fraisN
                                            ReportToPrint.SetParameters(fraisN1)

                                            Dim fraisM As New ReportParameter("FraisM", tfraisM)
                                            Dim fraisM1(0) As ReportParameter
                                            fraisM1(0) = fraisM
                                            ReportToPrint.SetParameters(fraisM1)

                                            Dim livreur As New ReportParameter("livreur", Livr)
                                            Dim livreur1(0) As ReportParameter
                                            livreur1(0) = livreur
                                            ReportToPrint.SetParameters(livreur1)

                                            Dim qty As New ReportParameter("qty", sumqty.ToString)
                                            Dim qty1(0) As ReportParameter
                                            qty1(0) = qty
                                            ReportToPrint.SetParameters(qty1)


                                            Dim appPath As String = Application.StartupPath()

                                            Dim SaveDirectory As String = appPath & "\"
                                            Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                                            Dim img As New ReportParameter("image", "File:\" + SavePath, True)
                                            Dim img1(0) As ReportParameter
                                            img1(0) = img
                                            ReportToPrint.SetParameters(img1)

                                            ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))

                                            'ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtfrais))
                                            'ReportToPrint.ReportEmbeddedResource = "Myapp.Report1.rdlc"
                                            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSetnum", tbl))
                                            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSettickets", tbl2))
                                            'ReportToPrint.DataSources.Add(New ReportDataSource("DataSet1", tbl))
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
                                            adpt = New MySqlDataAdapter("select ticket from parameters", conn2)
                                            Dim params As New DataTable
                                            adpt.Fill(params)
                                            If params.Rows(0).Item(0) = "Desactive" Then
                                                printDoc.PrinterSettings.Copies = 1
                                            Else

                                                printDoc.PrinterSettings.Copies = 2
                                            End If
                                            printDoc.Print()
                                            'tbl.Rows.Clear()
                                            'tbl2.Rows.Clear()

                                        End If
                                    Catch ex As Exception
                                        MsgBox(ex.Message)
                                    End Try
                                    tpevar = False
                                End If

                                DataGridView3.Rows.Clear()
                                Label28.Text = "numbl"
                                Label30.Text = 0
                                Label32.Text = "Sans livraison"

                                Dim adpt29 As New MySqlDataAdapter("select * from infos", conn2)
                                Dim table29 As New DataTable
                                adpt29.Fill(table29)
                                Dim gifttop As Decimal = table29.Rows(0).Item(24)
                                If Label6.Text >= gifttop Then
                                    adpt = New MySqlDataAdapter("select * from gifts", conn2)
                                    Dim tablegift As New DataTable
                                    adpt.Fill(tablegift)
                                    If tablegift.Rows.Count() <> 0 Then
                                        gift.ShowDialog()

                                    End If

                                End If
                                Label6.Text = 0.ToString("# ##0.00")
                                Try
                                    If IsNumeric(TextBox3.Text) = False Then
                                        If Label9.Text = "Rendu :" Then
                                            Label4.Text = 0.00
                                        End If
                                        If Label9.Text = "Reste :" Then
                                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                                        End If
                                    Else
                                        If Label9.Text = "Rendu :" Then
                                            Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                                        End If
                                        If Label9.Text = "Reste :" Then
                                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                                        End If
                                    End If
                                Catch ex As MySqlException
                                    MsgBox(ex.Message)
                                End Try
                                Label23.Text = 0.ToString("# ##0.00")
                                Label24.Text = 0.ToString("# ##0.00")
                                Label25.Text = 0.ToString("# ##0.00")
                                TextBox21.Text = ""

                                Label7.Text = 0.ToString("# ##0.00")
                                Label4.Text = 0
                                TextBox1.Text = 0
                                TextBox3.Text = ""
                                TextBox8.Text = ""
                                Panel8.Visible = False
                                IconButton52.PerformClick()

                                ComboBox2.SelectedItem = "comptoir"
                                ComboBox1.SelectedItem = "Espèce"
                                IconButton31.BackColor = Color.DarkOrange
                                IconButton31.ForeColor = Color.White
                                ComboBox1.Text = "Espèce"

                                IconButton32.BackColor = Color.White
                                IconButton32.ForeColor = Color.Black

                                IconButton33.BackColor = Color.White
                                IconButton33.ForeColor = Color.Black
                                TextBox2.Select()
                                TextBox2.Text = ""
                                Label18.Visible = False

                                adpt = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
                                Dim tabletick As New DataTable
                                adpt.Fill(tabletick)
                                Dim idlast As Integer
                                If tabletick.Rows().Count = 0 Then
                                    idlast = 1

                                Else
                                    idlast = tabletick.Rows(0).Item(0) + 1

                                End If
                                Label20.Text = "Ticket N° " & idlast.ToString
                                'IconButton24.PerformClick()
                                Casef = False
                                CheckBox2.Checked = True
                                If ComboBox1.Text = "Espèce" Then
                                    Dim codeOpenCashDrawer As Byte() = New Byte() {27, 112, 48, 55, 121}
                                    Dim rw As RawPrinterHelper = New RawPrinterHelper
                                    Dim pUnmanagedBytes As New IntPtr(0)
                                    pUnmanagedBytes = Marshal.AllocCoTaskMem(5)
                                    Dim printname As String = a5printer
                                    Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5)
                                    rw.SendBytesToPrinter(printname, pUnmanagedBytes, 5)
                                    Marshal.FreeCoTaskMem(pUnmanagedBytes)
                                End If
                            End If
                        End If
                    Else
                        MsgBox("Merci de choisir le mode de paiement")

                    End If
                End If

            Else
                MsgBox("Ce client a déjà atteind le maximum du crédit")
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        TextBox2.Select()
        TextBox2.Text = ""
        BackgroundWorker1.RunWorkerAsync()

    End Sub

    Private Sub IconButton49_Click(sender As Object, e As EventArgs) Handles IconButton49.Click
        Label62.Text = "no"
        Try
            conn2.Close()
            conn2.Open()
            anciencrdt = "-"
            Dim mxcrdt As Decimal = 0
            'mxCredt.ExecuteScalar
            conn2.Close()
            If mxcrdt = 0 Then
                If DataGridView3.Rows.Count <= 0 Then
                    MsgBox(“Ticket Vide ! ”)
                Else

                    If ComboBox1.Text <> "" Then


                        If Label17.Text = 1 Then
                            If ComboBox2.Text = "comptoir" Or ComboBox2.Text = "Comptoir" Or ComboBox2.Text = "Client fidèle" Then
                                Casef = True
                                tfraisN = "Coût Logistique"
                                tfraisM = 0
                                Livr = "-"
                            Else
                                frais.ShowDialog()

                            End If

                            Dim cmd As MySqlCommand
                            Dim cmd2, cmd3 As MySqlCommand
                            If Casef = True Then




                                Dim sql As String
                                Dim sql2, sql3 As String
                                Dim execution As Integer

                                If Label18.Visible = True Then
                                    conn2.Close()
                                    conn2.Open()
                                    cmd = New MySqlCommand("DELETE FROM `orderdetails` WHERE type = 'ventes' and `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `orders` WHERE `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()
                                    conn2.Close()

                                End If

                                adpt = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
                                Dim table As New DataTable
                                adpt.Fill(table)

                                adpt = New MySqlDataAdapter("SELECT id from bondelivr ORDER BY id DESC LIMIT 1", conn2)
                                Dim tablebl As New DataTable
                                adpt.Fill(tablebl)
                                Dim rms As String
                                If TextBox1.Text = "" Then
                                    rms = 0
                                Else
                                    rms = Label7.Text * TextBox1.Text / 100

                                End If

                                Dim reste As String
                                Dim client As String
                                Dim payer As String



                                If Label9.Text = "Reste :" Then
                                    If TextBox3.Text = "" Then
                                        payer = 0
                                        reste = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(tfraisM)


                                    Else
                                        If TextBox3.Text <= 0 Then
                                            payer = 0
                                            reste = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(tfraisM)
                                        End If
                                        If TextBox3.Text >= Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(tfraisM) Then
                                            payer = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(tfraisM)
                                            reste = 0
                                        Else
                                            payer = TextBox3.Text
                                            reste = Convert.ToDouble(Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(tfraisM)) - payer
                                        End If
                                    End If

                                Else
                                    reste = 0

                                    payer = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(tfraisM)
                                End If

                                If newClient = True Then
                                    client = ComboBox2.Text
                                Else
                                    client = ComboBox2.Text
                                End If

                                conn2.Close()
                                conn2.Open()
                                Dim id As Integer
                                If table.Rows().Count = 0 Then
                                    id = 1

                                Else
                                    id = table.Rows(0).Item(0) + 1

                                End If
                                Dim blnum As Integer

                                If tablebl.Rows().Count = 0 Then
                                    blnum = 1

                                Else
                                    blnum = tablebl.Rows(0).Item(0) + 1

                                End If
                                If ComboBox2.Text <> "comptoir" AndAlso ComboBox2.Text <> "Comptoir" Then
                                    sql2 = "INSERT INTO bondelivr (`order_id`) 
                    VALUES ('" & id & "')"
                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()
                                    CheckBox2.Checked = False
                                End If

                                Dim remiseprc As Decimal = 0

                                If TextBox21.Text <> "" Then
                                    remiseprc = TextBox21.Text
                                End If
                                If Label28.Text = "numbl" Then
                                    adpt = New MySqlDataAdapter("select OrderID from orderssauv where OrderID = '" & id & "' and client = '" & client & "' ", conn2)
                                    Dim tablesauv As New DataTable
                                    adpt.Fill(tablesauv)
                                    If tablesauv.Rows.Count <> 0 Then
                                        cmd2 = New MySqlCommand("DELETE FROM `orderssauv` where `OrderID` = '" & id & "' ", conn2)
                                        cmd2.ExecuteNonQuery()

                                        cmd2 = New MySqlCommand("DELETE FROM `orderdetailssauv` where `OrderID` = '" & id & "' ", conn2)
                                        cmd2.ExecuteNonQuery()
                                    Else
                                        adpt = New MySqlDataAdapter("select OrderID from orderssauv where OrderID = '" & id & "'", conn2)
                                        Dim tablesauv2 As New DataTable
                                        adpt.Fill(tablesauv2)
                                        If tablesauv2.Rows.Count <> 0 Then
                                            id = id + 1
                                        End If
                                    End If
                                    sql2 = "INSERT INTO orders (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",") + "','" + Label4.Text.ToString.Replace(".", ",") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste + "','" + tfraisM + "','" & Livr & "')"
                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()

                                    sql2 = "INSERT INTO orderssauv (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`) 
                    VALUES ('" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",") + "','" + Label4.Text.ToString.Replace(".", ",") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste + "','" + tfraisM + "','" & Livr & "')"
                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()
                                    idasync = id

                                Else
                                    id = Label28.Text
                                    Dim clot As String = 0
                                    If Convert.ToDateTime(Label29.Text).ToString("yyyy-MM-dd") = DateTime.Now.ToString("yyyy-MM-dd") Then
                                    Else

                                        clot = 1
                                    End If
                                    cmd = New MySqlCommand("DELETE FROM `orderdetails` WHERE `OrderID` = '" & id & "' ", conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `orders` WHERE `OrderID` = '" & id & "' ", conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `cheques` WHERE `tick` = '" & id & "' ", conn2)
                                    cmd.ExecuteNonQuery()
                                    sql2 = "INSERT INTO orders (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`,`fac_id`,`cloture`) 
                    VALUES ('" & id & "', '" + Convert.ToDateTime(Label29.Text).ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",") + "','" + Label4.Text.ToString.Replace(".", ",") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste + "','" + tfraisM + "','" & Livr & "','" & Label55.Text & "','" & clot & "')"

                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()

                                    cmd = New MySqlCommand("DELETE FROM `orderdetailssauv` WHERE `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()
                                    cmd = New MySqlCommand("DELETE FROM `orderssauv` WHERE `OrderID` = " + Label19.Text, conn2)
                                    cmd.ExecuteNonQuery()

                                    sql2 = "INSERT INTO orderssauv (`OrderID`, `OrderDate`, `MontantOrder`,`ServeurRef`,`paye`,`rendu`,`remisePerc`,`remiseMt`,`montantTotal`,`modePayement`,`orderType`,`client`,`reste`,`transport`,`livreur`,`fac_id`,`cloture`) 
                    VALUES ('" & id & "', '" + Convert.ToDateTime(Label29.Text).ToString("yyyy-MM-dd HH:mm") + "','" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "','" + Label2.Text + "','" + payer.ToString.Replace(".", ",") + "','" + Label4.Text.ToString.Replace(".", ",") + "','" + remiseprc.ToString.Replace(".", ",") + "','" + Convert.ToDecimal(Label23.Text * (remiseprc.ToString.Replace(".", ",") / 100)).ToString.Replace(".", ",") + "','" + Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + "','" + ComboBox1.Text + "','" + ComboBox3.Text + "','" + client + "','" + reste + "','" + tfraisM + "','" & Livr & "','" & Label55.Text & "','" & clot & "')"

                                    cmd2 = New MySqlCommand(sql2, conn2)
                                    cmd2.Parameters.Clear()
                                    cmd2.ExecuteNonQuery()
                                    idasync = id

                                    If ComboBox1.Text = "Crédit" Then

                                        If Label55.Text <> "-" Then

                                            cmd3 = New MySqlCommand("UPDATE `facture` SET `paye`= '0',`etat`= 'Non Payé', `reste` = '" & Convert.ToDouble(Label6.Text.ToString.Replace(".", ",").Replace(" ", "")) + tfraisM & "'  WHERE OrderID = '" + Label55.Text + "'", conn2)
                                            cmd3.Parameters.Clear()
                                            cmd3.ExecuteNonQuery()

                                        End If
                                    Else

                                        If Label55.Text <> "-" Then

                                            cmd3 = New MySqlCommand("UPDATE `facture` SET `paye`= '" & payer.ToString.Replace(".", ",") & "',`etat`= 'Payé', `reste` = '0'  WHERE OrderID = '" + Label55.Text + "'", conn2)
                                            cmd3.Parameters.Clear()
                                            cmd3.ExecuteNonQuery()

                                        End If
                                    End If


                                End If
                                If ComboBox1.Text = "TPE" Then
                                    adpt = New MySqlDataAdapter("SELECT organisme FROM `banques` WHERE `tpe` = '1'", conn2)
                                    Dim tabletpe As New DataTable
                                    adpt.Fill(tabletpe)

                                    sql3 = "INSERT INTO cheques (`organisme`, `agence`, `compte`, `num`, `echeance`, `date`, `tick`,`bq`,`montant`,`client`) 
                    VALUES ('tpe','" + TextBox10.Text + "','tpe','" + TextBox9.Text + "','tpe', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" & id & "','" + tabletpe.Rows(0).Item(0) + "','" + Convert.ToDecimal(Label6.Text.ToString.Replace(".", ",").Replace(" ", "") + tfraisM).ToString("# ##0.00") + "','" & ComboBox2.Text & "')"
                                    cmd3 = New MySqlCommand(sql3, conn2)
                                    cmd3.Parameters.Clear()
                                    cmd3.ExecuteNonQuery()

                                End If
                                sql3 = "INSERT INTO livreurs_cout (`name`, `cout`, `order_id`,`date`) 
                    VALUES ('" & Livr & "','" & tfraisM & "','" & id & "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')"
                                cmd3 = New MySqlCommand(sql3, conn2)
                                cmd3.Parameters.Clear()
                                cmd3.ExecuteNonQuery()

                                adpt = New MySqlDataAdapter("select * from fideles WHERE code = '" + TextBox8.Text + "'", conn2)
                                Dim table3 As New DataTable
                                adpt.Fill(table3)
                                If table3.Rows.Count() <> 0 Then
                                    If table3.Rows(0).Item(2) < 5 Then
                                        cmd3 = New MySqlCommand("UPDATE `fideles` SET `lastachats`= '" + TextBox21.Text.ToString + "', `achats` = `achats` + '" & Convert.ToDecimal(Label23.Text - (Label23.Text * (TextBox21.Text.Replace(".", ",") / 100))).ToString("#0.00") & "'  WHERE code = '" + TextBox8.Text + "'", conn2)
                                        cmd3.Parameters.Clear()
                                        cmd3.ExecuteNonQuery()
                                    End If

                                    If table3.Rows(0).Item(2) >= 5 Then
                                        cmd3 = New MySqlCommand("UPDATE `fideles` SET `lastachats`= '" + TextBox21.Text.ToString + "', `achats` = '" + Convert.ToDecimal(Label23.Text - (Label23.Text * (TextBox21.Text.Replace(".", ",") / 100))).ToString("#0.00") + "'  WHERE code = '" + TextBox8.Text + "'", conn2)
                                        cmd3.Parameters.Clear()
                                        cmd3.ExecuteNonQuery()
                                    End If

                                End If

                                For Each dr As DataGridViewRow In Me.DataGridView3.Rows
                                    conn2.Close()
                                    conn2.Open()
                                    Try
                                        adpt = New MySqlDataAdapter("select * from article where Code = '" + dr.Cells(4).Value.ToString + "' and mask = '0'", conn2)
                                        Dim table2 As New DataTable
                                        adpt.Fill(table2)
                                        Dim marg As Decimal = 0
                                        If table2.Rows.Count <> 0 Then
                                            If Label28.Text = "numbl" Then
                                                stck = Convert.ToDecimal(table2.Rows(0).Item(8)) - Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                            Else
                                                If Convert.ToDecimal(dr.Cells(2).Value.ToString.Replace(" ", "")) < 0 Then
                                                    stck = Convert.ToDecimal(table2.Rows(0).Item(8)) + Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                                Else
                                                    stck = Convert.ToDecimal(table2.Rows(0).Item(8)) - Convert.ToDecimal(dr.Cells(2).Value - Convert.ToDecimal(dr.Cells(9).Value))
                                                End If
                                            End If
                                            marg = (((Convert.ToDecimal(dr.Cells(10).Value.ToString.Replace(".", ",")) - (Convert.ToDecimal(dr.Cells(10).Value.ToString.Replace(".", ",")) * (Convert.ToDecimal(dr.Cells(8).Value.ToString.Replace(".", ",")) / 100))) - Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")))) * (Convert.ToDecimal(dr.Cells(2).Value.ToString.Replace(".", ",")) - Convert.ToDecimal(dr.Cells(9).Value.ToString.Replace(".", ",")))
                                            If marg <= 0 Then
                                                marg = 0
                                            End If
                                        End If
                                        cmd3 = New MySqlCommand("UPDATE `article` SET `Stock`= '" + stck.ToString + "' , `sell` = 1 WHERE Code = '" + dr.Cells(4).Value.ToString + "'", conn2)
                                        cmd3.ExecuteNonQuery()

                                        cmd = New MySqlCommand
                                        prod = dr.Cells(5).Value
                                        sql = “INSERT INTO orderdetails (OrderID ,ProductID ,Price,Quantity,Total,date,pa,type,name,marge,rms,gr,pv,tva) VALUES (@tick, @code, @price, @qty, @montant, @date,@pa,@type,@name,@mrg,@rms,@gr,@pv,@tva);”
                                        sql2 = “INSERT INTO orderdetailssauv (OrderID ,ProductID ,Price,Quantity,Total,date,pa,type,name,marge,rms,gr,pv,tva) VALUES (@tick, @code, @price, @qty, @montant, @date,@pa,@type,@name,@mrg,@rms,@gr,@pv,@tva);”

                                        With cmd
                                            .Connection = conn2
                                            .CommandText = sql
                                            .Parameters.Clear()
                                            .Parameters.AddWithValue(“@tick”, id)

                                            .Parameters.AddWithValue(“@code”, dr.Cells(4).Value)
                                            .Parameters.AddWithValue(“@price”, Convert.ToDecimal(dr.Cells(3).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@qty”, Convert.ToDecimal(dr.Cells(2).Value).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@montant”, Convert.ToDecimal(dr.Cells(6).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@date”, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                            .Parameters.AddWithValue(“@pa”, Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@type”, "ventes")
                                            .Parameters.AddWithValue(“@name”, dr.Cells(1).Value)
                                            .Parameters.AddWithValue(“@mrg”, Convert.ToDecimal(marg.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@rms”, dr.Cells(8).Value)
                                            .Parameters.AddWithValue(“@gr”, dr.Cells(9).Value)
                                            .Parameters.AddWithValue(“@pv”, dr.Cells(10).Value)
                                            .Parameters.AddWithValue(“@tva”, dr.Cells(11).Value)
                                            execution = .ExecuteNonQuery()

                                            .CommandText = sql2
                                            .Parameters.Clear()
                                            .Parameters.AddWithValue(“@tick”, id)

                                            .Parameters.AddWithValue(“@code”, dr.Cells(4).Value)
                                            .Parameters.AddWithValue(“@price”, Convert.ToDecimal(dr.Cells(3).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@qty”, Convert.ToDecimal(dr.Cells(2).Value).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@montant”, Convert.ToDecimal(dr.Cells(6).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@date”, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                            .Parameters.AddWithValue(“@pa”, Convert.ToDecimal(dr.Cells(5).Value.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@type”, "ventes")
                                            .Parameters.AddWithValue(“@name”, dr.Cells(1).Value)
                                            .Parameters.AddWithValue(“@mrg”, Convert.ToDecimal(marg.ToString.Replace(".", ",")).ToString("# ##0.00"))
                                            .Parameters.AddWithValue(“@rms”, dr.Cells(8).Value)
                                            .Parameters.AddWithValue(“@gr”, dr.Cells(9).Value)
                                            .Parameters.AddWithValue(“@pv”, dr.Cells(10).Value)
                                            .Parameters.AddWithValue(“@tva”, dr.Cells(11).Value)
                                            execution = .ExecuteNonQuery()
                                        End With
                                        conn2.Close()

                                    Catch ex As MySqlException
                                        MsgBox(ex.Message)
                                    End Try

                                Next
                                Dim sumqty As Decimal


                                adpt = New MySqlDataAdapter("SELECT reste FROM `orders` WHERE `client` = '" & ComboBox2.Text & "'", conn2)
                                Dim tablecrdt As New DataTable
                                adpt.Fill(tablecrdt)
                                Dim ancreste As Decimal = 0
                                For j = 0 To tablecrdt.Rows.Count() - 1
                                    ancreste += Convert.ToDecimal(tablecrdt.Rows(j).Item(0).ToString.Replace(".", ",").Replace(" ", ""))
                                Next

                                If ComboBox2.Text = "comptoir" Then
                                    anciencrdt = 0
                                Else

                                    anciencrdt = ancreste.ToString("# ##0.00")

                                End If

                                Dim ds As New DataSet1
                                Dim dt As New DataTable
                                dt = ds.Tables("tick")
                                For i = 0 To DataGridView3.Rows.Count - 1
                                    If DataGridView3.Rows(i).Cells(9).Value = 0 Then
                                        Dim found As Boolean = False
                                        For K = 0 To dt.Rows.Count - 1
                                            If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                                                found = True
                                                dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(2).Value
                                                dt.Rows(K).Item(7) = Convert.ToDouble(dt.Rows(K).Item(7)) + DataGridView3.Rows(i).Cells(6).Value
                                                sumqty += DataGridView3.Rows(i).Cells(2).Value
                                                Exit For

                                            End If
                                        Next
                                        If Not found Then
                                            dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value, "", DataGridView3.Rows(i).Cells(2).Value, "", DataGridView3.Rows(i).Cells(3).Value, "", DataGridView3.Rows(i).Cells(6).Value, DataGridView3.Rows(i).Cells(7).Value)
                                            sumqty += DataGridView3.Rows(i).Cells(2).Value
                                        End If
                                    End If
                                Next
                                For i = 0 To DataGridView3.Rows.Count - 1
                                    If DataGridView3.Rows(i).Cells(9).Value > 0 Then
                                        Dim found As Boolean = False
                                        For K = 0 To dt.Rows.Count - 1
                                            If DataGridView3.Rows(i).Cells(4).Value = dt.Rows(K).Item(0) Then

                                                found = True
                                                dt.Rows(K).Item(3) = Convert.ToDouble(dt.Rows(K).Item(3)) + DataGridView3.Rows(i).Cells(9).Value
                                                sumqty += DataGridView3.Rows(i).Cells(9).Value
                                                Exit For

                                            End If
                                        Next
                                        If Not found Then
                                            dt.Rows.Add(DataGridView3.Rows(i).Cells(4).Value, DataGridView3.Rows(i).Cells(1).Value & " (Gratuit)", "", DataGridView3.Rows(i).Cells(9).Value, "", 0, "", 0, 0)
                                            sumqty += DataGridView3.Rows(i).Cells(9).Value
                                        End If
                                    End If
                                Next


                                DataGridView3.Rows.Clear()
                                Label28.Text = "numbl"
                                Label30.Text = 0
                                Label32.Text = "Sans livraison"

                                Dim adpt29 As New MySqlDataAdapter("select * from infos", conn2)
                                Dim table29 As New DataTable
                                adpt29.Fill(table29)
                                Dim gifttop As Decimal = table29.Rows(0).Item(24)
                                If Label6.Text >= gifttop Then
                                    adpt = New MySqlDataAdapter("select * from gifts", conn2)
                                    Dim tablegift As New DataTable
                                    adpt.Fill(tablegift)
                                    If tablegift.Rows.Count() <> 0 Then
                                        gift.ShowDialog()

                                    End If

                                End If
                                Label6.Text = 0.ToString("# ##0.00")
                                Try
                                    If IsNumeric(TextBox3.Text) = False Then
                                        If Label9.Text = "Rendu :" Then
                                            Label4.Text = 0.00
                                        End If
                                        If Label9.Text = "Reste :" Then
                                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                                        End If
                                    Else
                                        If Label9.Text = "Rendu :" Then
                                            Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",")).ToString("# ##0.00")
                                        End If
                                        If Label9.Text = "Reste :" Then
                                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                                        End If
                                    End If
                                Catch ex As MySqlException
                                    MsgBox(ex.Message)
                                End Try
                                Label23.Text = 0.ToString("# ##0.00")
                                Label24.Text = 0.ToString("# ##0.00")
                                Label25.Text = 0.ToString("# ##0.00")
                                TextBox21.Text = ""

                                Label7.Text = 0.ToString("# ##0.00")
                                Label4.Text = 0
                                TextBox1.Text = 0
                                TextBox3.Text = ""
                                TextBox8.Text = ""
                                ComboBox2.SelectedItem = "comptoir"
                                ComboBox1.SelectedItem = "Espèce"
                                IconButton31.BackColor = Color.DarkOrange
                                IconButton31.ForeColor = Color.White
                                ComboBox1.Text = "Espèce"
                                IconButton52.PerformClick()
                                Panel8.Visible = False


                                IconButton32.BackColor = Color.White
                                IconButton32.ForeColor = Color.Black

                                IconButton33.BackColor = Color.White
                                IconButton33.ForeColor = Color.Black
                                TextBox2.Select()
                                TextBox2.Text = ""
                                Label18.Visible = False

                                adpt = New MySqlDataAdapter("SELECT OrderID from orders ORDER BY OrderID DESC LIMIT 1", conn2)
                                Dim tabletick As New DataTable
                                adpt.Fill(tabletick)
                                Dim idlast As Integer
                                If tabletick.Rows().Count = 0 Then
                                    idlast = 1

                                Else
                                    idlast = tabletick.Rows(0).Item(0) + 1

                                End If
                                Label20.Text = "Ticket N° " & idlast.ToString
                                'IconButton24.PerformClick()
                                Casef = False
                                CheckBox2.Checked = True
                                If ComboBox1.Text = "Espèce" Then
                                    Dim codeOpenCashDrawer As Byte() = New Byte() {27, 112, 48, 55, 121}
                                    Dim rw As RawPrinterHelper = New RawPrinterHelper
                                    Dim pUnmanagedBytes As New IntPtr(0)
                                    pUnmanagedBytes = Marshal.AllocCoTaskMem(5)
                                    Dim printname As String = a5printer
                                    Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5)
                                    rw.SendBytesToPrinter(printname, pUnmanagedBytes, 5)
                                    Marshal.FreeCoTaskMem(pUnmanagedBytes)
                                End If
                            End If
                        End If
                    Else
                        MsgBox("Merci de choisir le mode de paiement")

                    End If
                End If

            Else
                MsgBox("Ce client a déjà atteind le maximum du crédit")
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        TextBox2.Select()
        TextBox2.Text = ""
        BackgroundWorker1.RunWorkerAsync()

    End Sub

    Private Sub IconButton46_Click(sender As Object, e As EventArgs) Handles IconButton46.Click
        If Label62.Text = "no" Then

            Dim sum As Double = 0
            For i = 0 To DataGridView3.Rows.Count - 1
                If DataGridView3.Rows(i).Cells(6).Value > 0 Then
                    If IsNothing(DataGridView3.Rows(i).Cells(12).Value) Then

                        adpt = New MySqlDataAdapter("select pv_ht,pv_ttc from client_price where client = '" & ComboBox2.Text & "' and code = '" & DataGridView3.Rows(i).Cells(0).Value & "'", conn2)
                        Dim tablepricing As New DataTable
                        adpt.Fill(tablepricing)

                        If tablepricing.Rows.Count <> 0 Then
                            DataGridView3.Rows(i).Cells(10).Value = tablepricing.Rows(0).Item(0)
                            DataGridView3.Rows(i).Cells(3).Value = tablepricing.Rows(0).Item(1)
                            DataGridView3.Rows(i).Cells(6).Value = DataGridView3.Rows(i).Cells(3).Value * DataGridView3.Rows(i).Cells(2).Value
                        Else
                            adpt = New MySqlDataAdapter("select PV_HT, PV_TTC from article where Code = '" & DataGridView3.Rows(i).Cells(0).Value & "'", conn2)
                            Dim tableno As New DataTable
                            adpt.Fill(tableno)
                            DataGridView3.Rows(i).Cells(10).Value = tableno.Rows(0).Item(0)
                            DataGridView3.Rows(i).Cells(3).Value = tableno.Rows(0).Item(1)
                            DataGridView3.Rows(i).Cells(6).Value = DataGridView3.Rows(i).Cells(3).Value * DataGridView3.Rows(i).Cells(2).Value
                        End If

                    End If
                End If
                sum = sum + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", ""))

            Next

            Label6.Text = sum.ToString("# ##0.00").Replace(" ", "")
            Label7.Text = sum.ToString("# ##0.00").Replace(" ", "")
        End If

        Panel8.Visible = True
        TextBox2.Select()
        TextBox2.Text = ""

    End Sub

    Private Sub IconButton50_Click(sender As Object, e As EventArgs) Handles IconButton50.Click
        Panel8.Visible = False
        TextBox8.Text = ""
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs) Handles Panel7.Paint

    End Sub

    Private Sub DataGridView1_MouseWheel(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseWheel
        ' Vérifier si la molette de la souris est déplacée vers le haut ou le bas
        If e.Delta > 0 Then
            ' Déplacer le contenu de la DataGridView vers le haut
            If DataGridView1.FirstDisplayedScrollingRowIndex > 0 Then
                DataGridView1.FirstDisplayedScrollingRowIndex -= 1
            End If
        ElseIf e.Delta < 0 Then
            ' Déplacer le contenu de la DataGridView vers le bas
            If DataGridView1.FirstDisplayedScrollingRowIndex < DataGridView1.RowCount - 1 Then
                DataGridView1.FirstDisplayedScrollingRowIndex += 1
            End If
        End If
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label16_TextChanged(sender As Object, e As EventArgs) Handles Label16.TextChanged
        Try
            If IsNumeric(TextBox3.Text) = False Then
                If Label9.Text = "Rendu :" Then
                    Label4.Text = 0.00
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
            Else
                If Label9.Text = "Rendu :" Then
                    Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                End If
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IconButton51_Click(sender As Object, e As EventArgs)
        If DataGridView3.Rows.Count() <> 0 Then
            Panel5.Visible = True
        Else
            MsgBox("Ticket Vide !")
            TextBox2.Select()
        End If
    End Sub

    Private Sub IconButton52_Click(sender As Object, e As EventArgs) Handles IconButton52.Click
        Dim sum As Double = 0
        For i = 0 To DataGridView3.Rows.Count - 1
            If DataGridView3.Rows(i).Cells(6).Value > 0 Then
                If IsNothing(DataGridView3.Rows(i).Cells(12).Value) Then
                    adpt = New MySqlDataAdapter("select PV_HT, PV_TTC from article where Code = '" & DataGridView3.Rows(i).Cells(0).Value & "'", conn2)
                    Dim tableno As New DataTable
                    adpt.Fill(tableno)
                    DataGridView3.Rows(i).Cells(10).Value = tableno.Rows(0).Item(0)
                    DataGridView3.Rows(i).Cells(3).Value = tableno.Rows(0).Item(1)
                    DataGridView3.Rows(i).Cells(6).Value = DataGridView3.Rows(i).Cells(3).Value * DataGridView3.Rows(i).Cells(2).Value
                End If

            End If
            sum = sum + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", ""))

        Next
        Label6.Text = sum.ToString("# ##0.00").Replace(" ", "")

        Panel5.Visible = False
        IconButton31.PerformClick()
        TextBox8.Text = ""
        TextBox2.Select()
        f9clicked = False
        Panel13.Visible = False
        TextBox21.Text = ""
    End Sub

    Private Sub IconButton51_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub IconButton55_Click(sender As Object, e As EventArgs) Handles IconButton55.Click
        Panel15.Visible = False
        opentr.Show()
        opentr.IconButton1.Visible = False
        opentr.DateTimePicker3.Visible = False
        opentr.DateTimePicker4.Visible = False
        opentr.Label1.Visible = False
        opentr.Label2.Visible = False
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton54_Click(sender As Object, e As EventArgs) Handles IconButton54.Click
        Panel15.Visible = False
        opentr.Show()
        opentr.Panel7.Visible = True
        adpt = New MySqlDataAdapter("SELECT sum(transport), livreur, count(*) FROM orders where cloture = @day and livreur NOT IN ('-', 'Livreur') AND livreur IS NOT NULL GROUP BY livreur", conn2)
        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@day", 0)
        opentr.DataGridView3.Rows.Clear()
        Dim tablelivreur As New DataTable
        adpt.Fill(tablelivreur)
        For i = 0 To tablelivreur.Rows().Count() - 1
            opentr.DataGridView3.Rows.Add(tablelivreur.Rows(i).Item(1).ToString.Replace(".", ","), tablelivreur.Rows(i).Item(0).ToString.Replace(".", ","), tablelivreur.Rows(i).Item(2))
        Next
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub IconButton53_Click(sender As Object, e As EventArgs) Handles IconButton53.Click
        Panel6.Visible = False
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub


    Private Sub TextBox11_Click(sender As Object, e As EventArgs) Handles TextBox11.Click
        TextBox11.SelectAll()
    End Sub
    Private Sub TextBox12_Click(sender As Object, e As EventArgs) Handles TextBox12.Click
        TextBox12.SelectAll()
    End Sub
    Private Sub TextBox13_Click(sender As Object, e As EventArgs) Handles TextBox13.Click
        TextBox13.SelectAll()
    End Sub
    Private Sub TextBox14_Click(sender As Object, e As EventArgs) Handles TextBox14.Click
        TextBox14.SelectAll()
    End Sub
    Private Sub TextBox15_Click(sender As Object, e As EventArgs) Handles TextBox15.Click
        TextBox15.SelectAll()
    End Sub
    Private Sub TextBox16_Click(sender As Object, e As EventArgs) Handles TextBox16.Click
        TextBox16.SelectAll()
    End Sub
    Private Sub TextBox17_Click(sender As Object, e As EventArgs) Handles TextBox17.Click
        TextBox17.SelectAll()
    End Sub
    Private Sub TextBox18_Click(sender As Object, e As EventArgs) Handles TextBox18.Click
        TextBox18.SelectAll()
    End Sub
    Private Sub TextBox19_Click(sender As Object, e As EventArgs) Handles TextBox19.Click
        TextBox19.SelectAll()
    End Sub
    Private Sub TextBox20_Click(sender As Object, e As EventArgs) Handles TextBox20.Click
        TextBox20.SelectAll()
    End Sub

    Private Sub TextBox11_LostFocus(sender As Object, e As EventArgs) Handles TextBox11.LostFocus
        If TextBox11.Text = "" Then
            TextBox11.Text = 0
        End If
    End Sub
    Private Sub TextBox12_LostFocus(sender As Object, e As EventArgs) Handles TextBox12.LostFocus
        If TextBox12.Text = "" Then
            TextBox12.Text = 0
        End If
    End Sub
    Private Sub TextBox13_LostFocus(sender As Object, e As EventArgs) Handles TextBox13.LostFocus
        If TextBox13.Text = "" Then
            TextBox13.Text = 0
        End If
    End Sub
    Private Sub TextBox14_LostFocus(sender As Object, e As EventArgs) Handles TextBox14.LostFocus
        If TextBox14.Text = "" Then
            TextBox14.Text = 0
        End If
    End Sub
    Private Sub TextBox15_LostFocus(sender As Object, e As EventArgs) Handles TextBox15.LostFocus
        If TextBox15.Text = "" Then
            TextBox15.Text = 0
        End If
    End Sub
    Private Sub TextBox16_LostFocus(sender As Object, e As EventArgs) Handles TextBox16.LostFocus
        If TextBox16.Text = "" Then
            TextBox16.Text = 0
        End If
    End Sub
    Private Sub TextBox17_LostFocus(sender As Object, e As EventArgs) Handles TextBox17.LostFocus
        If TextBox17.Text = "" Then
            TextBox17.Text = 0
        End If
    End Sub
    Private Sub TextBox18_LostFocus(sender As Object, e As EventArgs) Handles TextBox18.LostFocus
        If TextBox18.Text = "" Then
            TextBox18.Text = 0
        End If
    End Sub
    Private Sub TextBox19_LostFocus(sender As Object, e As EventArgs) Handles TextBox19.LostFocus
        If TextBox19.Text = "" Then
            TextBox19.Text = 0
        End If
    End Sub
    Private Sub TextBox20_LostFocus(sender As Object, e As EventArgs) Handles TextBox20.LostFocus
        If TextBox20.Text = "" Then
            TextBox20.Text = 0
        End If
    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        Dim total11 As Double = 0.00
        Dim total12 As Double = 0.00
        Dim total13 As Double = 0.00
        Dim total14 As Double = 0.00
        Dim total15 As Double = 0.00
        Dim total16 As Double = 0.00
        Dim total17 As Double = 0.00
        Dim total18 As Double = 0.00
        Dim total19 As Double = 0.00
        Dim total20 As Double = 0.00
        Dim value11 As Double
        Dim value12 As Double
        Dim value13 As Double
        Dim value14 As Double
        Dim value15 As Double
        Dim value16 As Double
        Dim value17 As Double
        Dim value18 As Double
        Dim value19 As Double
        Dim value20 As Double

        If TextBox11.Text = "" Then
            value11 = 0
        Else
            value11 = Convert.ToDouble(TextBox11.Text)
        End If
        total11 = value11 * Convert.ToDecimal(TextBox11.Tag.ToString.Replace(".", ","))
        If TextBox12.Text = "" Then
            value12 = 0
        Else
            value12 = Convert.ToDouble(TextBox12.Text)
        End If
        total12 = value12 * Convert.ToDecimal(TextBox12.Tag.ToString.Replace(".", ","))
        If TextBox13.Text = "" Then
            value13 = 0
        Else
            value13 = Convert.ToDouble(TextBox13.Text)
        End If
        total13 = value13 * Convert.ToDecimal(TextBox13.Tag.ToString.Replace(".", ","))
        If TextBox14.Text = "" Then
            value14 = 0
        Else
            value14 = Convert.ToDouble(TextBox14.Text)
        End If
        total14 = value14 * Convert.ToDecimal(TextBox14.Tag.ToString.Replace(".", ","))
        If TextBox15.Text = "" Then
            value15 = 0
        Else
            value15 = Convert.ToDouble(TextBox15.Text)
        End If
        total15 = value15 * Convert.ToDecimal(TextBox15.Tag.ToString.Replace(".", ","))
        If TextBox16.Text = "" Then
            value16 = 0
        Else
            value16 = Convert.ToDouble(TextBox16.Text)
        End If
        total16 = value16 * Convert.ToDecimal(TextBox16.Tag.ToString.Replace(".", ","))
        If TextBox17.Text = "" Then
            value17 = 0
        Else
            value17 = Convert.ToDouble(TextBox17.Text)
        End If
        total17 = value17 * Convert.ToDecimal(TextBox17.Tag.ToString.Replace(".", ","))
        If TextBox18.Text = "" Then
            value18 = 0
        Else
            value18 = Convert.ToDouble(TextBox18.Text)
        End If
        total18 = value18 * Convert.ToDecimal(TextBox18.Tag.ToString.Replace(".", ","))
        If TextBox19.Text = "" Then
            value19 = 0
        Else
            value19 = Convert.ToDouble(TextBox19.Text)
        End If
        total19 = value19 * Convert.ToDecimal(TextBox19.Tag.ToString.Replace(".", ","))
        If TextBox20.Text = "" Then
            value20 = 0
        Else
            value20 = Convert.ToDouble(TextBox20.Text)
        End If
        total20 = value20 * Convert.ToDecimal(TextBox20.Tag.ToString.Replace(".", ","))

        Label52.Text = (Convert.ToDecimal(total11.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total12.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total13.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total14.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total15.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total16.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total17.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total18.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total19.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total20.ToString().Replace(".", ",").Replace(" ", ""))).ToString("# ##0.00")

    End Sub
    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged
        Dim total11 As Double = 0.00
        Dim total12 As Double = 0.00
        Dim total13 As Double = 0.00
        Dim total14 As Double = 0.00
        Dim total15 As Double = 0.00
        Dim total16 As Double = 0.00
        Dim total17 As Double = 0.00
        Dim total18 As Double = 0.00
        Dim total19 As Double = 0.00
        Dim total20 As Double = 0.00
        Dim value11 As Double
        Dim value12 As Double
        Dim value13 As Double
        Dim value14 As Double
        Dim value15 As Double
        Dim value16 As Double
        Dim value17 As Double
        Dim value18 As Double
        Dim value19 As Double
        Dim value20 As Double

        If TextBox11.Text = "" Then
            value11 = 0
        Else
            value11 = Convert.ToDouble(TextBox11.Text)
        End If
        total11 = value11 * Convert.ToDecimal(TextBox11.Tag.ToString.Replace(".", ","))
        If TextBox12.Text = "" Then
            value12 = 0
        Else
            value12 = Convert.ToDouble(TextBox12.Text)
        End If
        total12 = value12 * Convert.ToDecimal(TextBox12.Tag.ToString.Replace(".", ","))
        If TextBox13.Text = "" Then
            value13 = 0
        Else
            value13 = Convert.ToDouble(TextBox13.Text)
        End If
        total13 = value13 * Convert.ToDecimal(TextBox13.Tag.ToString.Replace(".", ","))
        If TextBox14.Text = "" Then
            value14 = 0
        Else
            value14 = Convert.ToDouble(TextBox14.Text)
        End If
        total14 = value14 * Convert.ToDecimal(TextBox14.Tag.ToString.Replace(".", ","))
        If TextBox15.Text = "" Then
            value15 = 0
        Else
            value15 = Convert.ToDouble(TextBox15.Text)
        End If
        total15 = value15 * Convert.ToDecimal(TextBox15.Tag.ToString.Replace(".", ","))
        If TextBox16.Text = "" Then
            value16 = 0
        Else
            value16 = Convert.ToDouble(TextBox16.Text)
        End If
        total16 = value16 * Convert.ToDecimal(TextBox16.Tag.ToString.Replace(".", ","))
        If TextBox17.Text = "" Then
            value17 = 0
        Else
            value17 = Convert.ToDouble(TextBox17.Text)
        End If
        total17 = value17 * Convert.ToDecimal(TextBox17.Tag.ToString.Replace(".", ","))
        If TextBox18.Text = "" Then
            value18 = 0
        Else
            value18 = Convert.ToDouble(TextBox18.Text)
        End If
        total18 = value18 * Convert.ToDecimal(TextBox18.Tag.ToString.Replace(".", ","))
        If TextBox19.Text = "" Then
            value19 = 0
        Else
            value19 = Convert.ToDouble(TextBox19.Text)
        End If
        total19 = value19 * Convert.ToDecimal(TextBox19.Tag.ToString.Replace(".", ","))
        If TextBox20.Text = "" Then
            value20 = 0
        Else
            value20 = Convert.ToDouble(TextBox20.Text)
        End If
        total20 = value20 * Convert.ToDecimal(TextBox20.Tag.ToString.Replace(".", ","))

        Label52.Text = (Convert.ToDecimal(total11.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total12.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total13.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total14.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total15.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total16.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total17.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total18.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total19.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total20.ToString().Replace(".", ",").Replace(" ", ""))).ToString("# ##0.00")

    End Sub
    Private Sub TextBox13_TextChanged(sender As Object, e As EventArgs) Handles TextBox13.TextChanged
        Dim total11 As Double = 0.00
        Dim total12 As Double = 0.00
        Dim total13 As Double = 0.00
        Dim total14 As Double = 0.00
        Dim total15 As Double = 0.00
        Dim total16 As Double = 0.00
        Dim total17 As Double = 0.00
        Dim total18 As Double = 0.00
        Dim total19 As Double = 0.00
        Dim total20 As Double = 0.00
        Dim value11 As Double
        Dim value12 As Double
        Dim value13 As Double
        Dim value14 As Double
        Dim value15 As Double
        Dim value16 As Double
        Dim value17 As Double
        Dim value18 As Double
        Dim value19 As Double
        Dim value20 As Double

        If TextBox11.Text = "" Then
            value11 = 0
        Else
            value11 = Convert.ToDouble(TextBox11.Text)
        End If
        total11 = value11 * Convert.ToDecimal(TextBox11.Tag.ToString.Replace(".", ","))
        If TextBox12.Text = "" Then
            value12 = 0
        Else
            value12 = Convert.ToDouble(TextBox12.Text)
        End If
        total12 = value12 * Convert.ToDecimal(TextBox12.Tag.ToString.Replace(".", ","))
        If TextBox13.Text = "" Then
            value13 = 0
        Else
            value13 = Convert.ToDouble(TextBox13.Text)
        End If
        total13 = value13 * Convert.ToDecimal(TextBox13.Tag.ToString.Replace(".", ","))
        If TextBox14.Text = "" Then
            value14 = 0
        Else
            value14 = Convert.ToDouble(TextBox14.Text)
        End If
        total14 = value14 * Convert.ToDecimal(TextBox14.Tag.ToString.Replace(".", ","))
        If TextBox15.Text = "" Then
            value15 = 0
        Else
            value15 = Convert.ToDouble(TextBox15.Text)
        End If
        total15 = value15 * Convert.ToDecimal(TextBox15.Tag.ToString.Replace(".", ","))
        If TextBox16.Text = "" Then
            value16 = 0
        Else
            value16 = Convert.ToDouble(TextBox16.Text)
        End If
        total16 = value16 * Convert.ToDecimal(TextBox16.Tag.ToString.Replace(".", ","))
        If TextBox17.Text = "" Then
            value17 = 0
        Else
            value17 = Convert.ToDouble(TextBox17.Text)
        End If
        total17 = value17 * Convert.ToDecimal(TextBox17.Tag.ToString.Replace(".", ","))
        If TextBox18.Text = "" Then
            value18 = 0
        Else
            value18 = Convert.ToDouble(TextBox18.Text)
        End If
        total18 = value18 * Convert.ToDecimal(TextBox18.Tag.ToString.Replace(".", ","))
        If TextBox19.Text = "" Then
            value19 = 0
        Else
            value19 = Convert.ToDouble(TextBox19.Text)
        End If
        total19 = value19 * Convert.ToDecimal(TextBox19.Tag.ToString.Replace(".", ","))
        If TextBox20.Text = "" Then
            value20 = 0
        Else
            value20 = Convert.ToDouble(TextBox20.Text)
        End If
        total20 = value20 * Convert.ToDecimal(TextBox20.Tag.ToString.Replace(".", ","))

        Label52.Text = (Convert.ToDecimal(total11.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total12.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total13.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total14.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total15.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total16.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total17.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total18.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total19.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total20.ToString().Replace(".", ",").Replace(" ", ""))).ToString("# ##0.00")
    End Sub
    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged
        Dim total11 As Double = 0.00
        Dim total12 As Double = 0.00
        Dim total13 As Double = 0.00
        Dim total14 As Double = 0.00
        Dim total15 As Double = 0.00
        Dim total16 As Double = 0.00
        Dim total17 As Double = 0.00
        Dim total18 As Double = 0.00
        Dim total19 As Double = 0.00
        Dim total20 As Double = 0.00
        Dim value11 As Double
        Dim value12 As Double
        Dim value13 As Double
        Dim value14 As Double
        Dim value15 As Double
        Dim value16 As Double
        Dim value17 As Double
        Dim value18 As Double
        Dim value19 As Double
        Dim value20 As Double

        If TextBox11.Text = "" Then
            value11 = 0
        Else
            value11 = Convert.ToDouble(TextBox11.Text)
        End If
        total11 = value11 * Convert.ToDecimal(TextBox11.Tag.ToString.Replace(".", ","))
        If TextBox12.Text = "" Then
            value12 = 0
        Else
            value12 = Convert.ToDouble(TextBox12.Text)
        End If
        total12 = value12 * Convert.ToDecimal(TextBox12.Tag.ToString.Replace(".", ","))
        If TextBox13.Text = "" Then
            value13 = 0
        Else
            value13 = Convert.ToDouble(TextBox13.Text)
        End If
        total13 = value13 * Convert.ToDecimal(TextBox13.Tag.ToString.Replace(".", ","))
        If TextBox14.Text = "" Then
            value14 = 0
        Else
            value14 = Convert.ToDouble(TextBox14.Text)
        End If
        total14 = value14 * Convert.ToDecimal(TextBox14.Tag.ToString.Replace(".", ","))
        If TextBox15.Text = "" Then
            value15 = 0
        Else
            value15 = Convert.ToDouble(TextBox15.Text)
        End If
        total15 = value15 * Convert.ToDecimal(TextBox15.Tag.ToString.Replace(".", ","))
        If TextBox16.Text = "" Then
            value16 = 0
        Else
            value16 = Convert.ToDouble(TextBox16.Text)
        End If
        total16 = value16 * Convert.ToDecimal(TextBox16.Tag.ToString.Replace(".", ","))
        If TextBox17.Text = "" Then
            value17 = 0
        Else
            value17 = Convert.ToDouble(TextBox17.Text)
        End If
        total17 = value17 * Convert.ToDecimal(TextBox17.Tag.ToString.Replace(".", ","))
        If TextBox18.Text = "" Then
            value18 = 0
        Else
            value18 = Convert.ToDouble(TextBox18.Text)
        End If
        total18 = value18 * Convert.ToDecimal(TextBox18.Tag.ToString.Replace(".", ","))
        If TextBox19.Text = "" Then
            value19 = 0
        Else
            value19 = Convert.ToDouble(TextBox19.Text)
        End If
        total19 = value19 * Convert.ToDecimal(TextBox19.Tag.ToString.Replace(".", ","))
        If TextBox20.Text = "" Then
            value20 = 0
        Else
            value20 = Convert.ToDouble(TextBox20.Text)
        End If
        total20 = value20 * Convert.ToDecimal(TextBox20.Tag.ToString.Replace(".", ","))

        Label52.Text = (Convert.ToDecimal(total11.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total12.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total13.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total14.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total15.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total16.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total17.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total18.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total19.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total20.ToString().Replace(".", ",").Replace(" ", ""))).ToString("# ##0.00")
    End Sub
    Private Sub TextBox15_TextChanged(sender As Object, e As EventArgs) Handles TextBox15.TextChanged
        Dim total11 As Double = 0.00
        Dim total12 As Double = 0.00
        Dim total13 As Double = 0.00
        Dim total14 As Double = 0.00
        Dim total15 As Double = 0.00
        Dim total16 As Double = 0.00
        Dim total17 As Double = 0.00
        Dim total18 As Double = 0.00
        Dim total19 As Double = 0.00
        Dim total20 As Double = 0.00
        Dim value11 As Double
        Dim value12 As Double
        Dim value13 As Double
        Dim value14 As Double
        Dim value15 As Double
        Dim value16 As Double
        Dim value17 As Double
        Dim value18 As Double
        Dim value19 As Double
        Dim value20 As Double

        If TextBox11.Text = "" Then
            value11 = 0
        Else
            value11 = Convert.ToDouble(TextBox11.Text)
        End If
        total11 = value11 * Convert.ToDecimal(TextBox11.Tag.ToString.Replace(".", ","))
        If TextBox12.Text = "" Then
            value12 = 0
        Else
            value12 = Convert.ToDouble(TextBox12.Text)
        End If
        total12 = value12 * Convert.ToDecimal(TextBox12.Tag.ToString.Replace(".", ","))
        If TextBox13.Text = "" Then
            value13 = 0
        Else
            value13 = Convert.ToDouble(TextBox13.Text)
        End If
        total13 = value13 * Convert.ToDecimal(TextBox13.Tag.ToString.Replace(".", ","))
        If TextBox14.Text = "" Then
            value14 = 0
        Else
            value14 = Convert.ToDouble(TextBox14.Text)
        End If
        total14 = value14 * Convert.ToDecimal(TextBox14.Tag.ToString.Replace(".", ","))
        If TextBox15.Text = "" Then
            value15 = 0
        Else
            value15 = Convert.ToDouble(TextBox15.Text)
        End If
        total15 = value15 * Convert.ToDecimal(TextBox15.Tag.ToString.Replace(".", ","))
        If TextBox16.Text = "" Then
            value16 = 0
        Else
            value16 = Convert.ToDouble(TextBox16.Text)
        End If
        total16 = value16 * Convert.ToDecimal(TextBox16.Tag.ToString.Replace(".", ","))
        If TextBox17.Text = "" Then
            value17 = 0
        Else
            value17 = Convert.ToDouble(TextBox17.Text)
        End If
        total17 = value17 * Convert.ToDecimal(TextBox17.Tag.ToString.Replace(".", ","))
        If TextBox18.Text = "" Then
            value18 = 0
        Else
            value18 = Convert.ToDouble(TextBox18.Text)
        End If
        total18 = value18 * Convert.ToDecimal(TextBox18.Tag.ToString.Replace(".", ","))
        If TextBox19.Text = "" Then
            value19 = 0
        Else
            value19 = Convert.ToDouble(TextBox19.Text)
        End If
        total19 = value19 * Convert.ToDecimal(TextBox19.Tag.ToString.Replace(".", ","))
        If TextBox20.Text = "" Then
            value20 = 0
        Else
            value20 = Convert.ToDouble(TextBox20.Text)
        End If
        total20 = value20 * Convert.ToDecimal(TextBox20.Tag.ToString.Replace(".", ","))

        Label52.Text = (Convert.ToDecimal(total11.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total12.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total13.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total14.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total15.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total16.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total17.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total18.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total19.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total20.ToString().Replace(".", ",").Replace(" ", ""))).ToString("# ##0.00")
    End Sub
    Private Sub TextBox16_TextChanged(sender As Object, e As EventArgs) Handles TextBox16.TextChanged
        Dim total11 As Double = 0.00
        Dim total12 As Double = 0.00
        Dim total13 As Double = 0.00
        Dim total14 As Double = 0.00
        Dim total15 As Double = 0.00
        Dim total16 As Double = 0.00
        Dim total17 As Double = 0.00
        Dim total18 As Double = 0.00
        Dim total19 As Double = 0.00
        Dim total20 As Double = 0.00
        Dim value11 As Double
        Dim value12 As Double
        Dim value13 As Double
        Dim value14 As Double
        Dim value15 As Double
        Dim value16 As Double
        Dim value17 As Double
        Dim value18 As Double
        Dim value19 As Double
        Dim value20 As Double

        If TextBox11.Text = "" Then
            value11 = 0
        Else
            value11 = Convert.ToDouble(TextBox11.Text)
        End If
        total11 = value11 * Convert.ToDecimal(TextBox11.Tag.ToString.Replace(".", ","))
        If TextBox12.Text = "" Then
            value12 = 0
        Else
            value12 = Convert.ToDouble(TextBox12.Text)
        End If
        total12 = value12 * Convert.ToDecimal(TextBox12.Tag.ToString.Replace(".", ","))
        If TextBox13.Text = "" Then
            value13 = 0
        Else
            value13 = Convert.ToDouble(TextBox13.Text)
        End If
        total13 = value13 * Convert.ToDecimal(TextBox13.Tag.ToString.Replace(".", ","))
        If TextBox14.Text = "" Then
            value14 = 0
        Else
            value14 = Convert.ToDouble(TextBox14.Text)
        End If
        total14 = value14 * Convert.ToDecimal(TextBox14.Tag.ToString.Replace(".", ","))
        If TextBox15.Text = "" Then
            value15 = 0
        Else
            value15 = Convert.ToDouble(TextBox15.Text)
        End If
        total15 = value15 * Convert.ToDecimal(TextBox15.Tag.ToString.Replace(".", ","))
        If TextBox16.Text = "" Then
            value16 = 0
        Else
            value16 = Convert.ToDouble(TextBox16.Text)
        End If
        total16 = value16 * Convert.ToDecimal(TextBox16.Tag.ToString.Replace(".", ","))
        If TextBox17.Text = "" Then
            value17 = 0
        Else
            value17 = Convert.ToDouble(TextBox17.Text)
        End If
        total17 = value17 * Convert.ToDecimal(TextBox17.Tag.ToString.Replace(".", ","))
        If TextBox18.Text = "" Then
            value18 = 0
        Else
            value18 = Convert.ToDouble(TextBox18.Text)
        End If
        total18 = value18 * Convert.ToDecimal(TextBox18.Tag.ToString.Replace(".", ","))
        If TextBox19.Text = "" Then
            value19 = 0
        Else
            value19 = Convert.ToDouble(TextBox19.Text)
        End If
        total19 = value19 * Convert.ToDecimal(TextBox19.Tag.ToString.Replace(".", ","))
        If TextBox20.Text = "" Then
            value20 = 0
        Else
            value20 = Convert.ToDouble(TextBox20.Text)
        End If
        total20 = value20 * Convert.ToDecimal(TextBox20.Tag.ToString.Replace(".", ","))

        Label52.Text = (Convert.ToDecimal(total11.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total12.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total13.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total14.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total15.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total16.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total17.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total18.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total19.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total20.ToString().Replace(".", ",").Replace(" ", ""))).ToString("# ##0.00")
    End Sub
    Private Sub TextBox17_TextChanged(sender As Object, e As EventArgs) Handles TextBox17.TextChanged
        Dim total11 As Double = 0.00
        Dim total12 As Double = 0.00
        Dim total13 As Double = 0.00
        Dim total14 As Double = 0.00
        Dim total15 As Double = 0.00
        Dim total16 As Double = 0.00
        Dim total17 As Double = 0.00
        Dim total18 As Double = 0.00
        Dim total19 As Double = 0.00
        Dim total20 As Double = 0.00
        Dim value11 As Double
        Dim value12 As Double
        Dim value13 As Double
        Dim value14 As Double
        Dim value15 As Double
        Dim value16 As Double
        Dim value17 As Double
        Dim value18 As Double
        Dim value19 As Double
        Dim value20 As Double

        If TextBox11.Text = "" Then
            value11 = 0
        Else
            value11 = Convert.ToDouble(TextBox11.Text)
        End If
        total11 = value11 * Convert.ToDecimal(TextBox11.Tag.ToString.Replace(".", ","))
        If TextBox12.Text = "" Then
            value12 = 0
        Else
            value12 = Convert.ToDouble(TextBox12.Text)
        End If
        total12 = value12 * Convert.ToDecimal(TextBox12.Tag.ToString.Replace(".", ","))
        If TextBox13.Text = "" Then
            value13 = 0
        Else
            value13 = Convert.ToDouble(TextBox13.Text)
        End If
        total13 = value13 * Convert.ToDecimal(TextBox13.Tag.ToString.Replace(".", ","))
        If TextBox14.Text = "" Then
            value14 = 0
        Else
            value14 = Convert.ToDouble(TextBox14.Text)
        End If
        total14 = value14 * Convert.ToDecimal(TextBox14.Tag.ToString.Replace(".", ","))
        If TextBox15.Text = "" Then
            value15 = 0
        Else
            value15 = Convert.ToDouble(TextBox15.Text)
        End If
        total15 = value15 * Convert.ToDecimal(TextBox15.Tag.ToString.Replace(".", ","))
        If TextBox16.Text = "" Then
            value16 = 0
        Else
            value16 = Convert.ToDouble(TextBox16.Text)
        End If
        total16 = value16 * Convert.ToDecimal(TextBox16.Tag.ToString.Replace(".", ","))
        If TextBox17.Text = "" Then
            value17 = 0
        Else
            value17 = Convert.ToDouble(TextBox17.Text)
        End If
        total17 = value17 * Convert.ToDecimal(TextBox17.Tag.ToString.Replace(".", ","))
        If TextBox18.Text = "" Then
            value18 = 0
        Else
            value18 = Convert.ToDouble(TextBox18.Text)
        End If
        total18 = value18 * Convert.ToDecimal(TextBox18.Tag.ToString.Replace(".", ","))
        If TextBox19.Text = "" Then
            value19 = 0
        Else
            value19 = Convert.ToDouble(TextBox19.Text)
        End If
        total19 = value19 * Convert.ToDecimal(TextBox19.Tag.ToString.Replace(".", ","))
        If TextBox20.Text = "" Then
            value20 = 0
        Else
            value20 = Convert.ToDouble(TextBox20.Text)
        End If
        total20 = value20 * Convert.ToDecimal(TextBox20.Tag.ToString.Replace(".", ","))

        Label52.Text = (Convert.ToDecimal(total11.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total12.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total13.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total14.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total15.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total16.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total17.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total18.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total19.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total20.ToString().Replace(".", ",").Replace(" ", ""))).ToString("# ##0.00")
    End Sub
    Private Sub TextBox18_TextChanged(sender As Object, e As EventArgs) Handles TextBox18.TextChanged
        Dim total11 As Double = 0.00
        Dim total12 As Double = 0.00
        Dim total13 As Double = 0.00
        Dim total14 As Double = 0.00
        Dim total15 As Double = 0.00
        Dim total16 As Double = 0.00
        Dim total17 As Double = 0.00
        Dim total18 As Double = 0.00
        Dim total19 As Double = 0.00
        Dim total20 As Double = 0.00
        Dim value11 As Double
        Dim value12 As Double
        Dim value13 As Double
        Dim value14 As Double
        Dim value15 As Double
        Dim value16 As Double
        Dim value17 As Double
        Dim value18 As Double
        Dim value19 As Double
        Dim value20 As Double

        If TextBox11.Text = "" Then
            value11 = 0
        Else
            value11 = Convert.ToDouble(TextBox11.Text)
        End If
        total11 = value11 * Convert.ToDecimal(TextBox11.Tag.ToString.Replace(".", ","))
        If TextBox12.Text = "" Then
            value12 = 0
        Else
            value12 = Convert.ToDouble(TextBox12.Text)
        End If
        total12 = value12 * Convert.ToDecimal(TextBox12.Tag.ToString.Replace(".", ","))
        If TextBox13.Text = "" Then
            value13 = 0
        Else
            value13 = Convert.ToDouble(TextBox13.Text)
        End If
        total13 = value13 * Convert.ToDecimal(TextBox13.Tag.ToString.Replace(".", ","))
        If TextBox14.Text = "" Then
            value14 = 0
        Else
            value14 = Convert.ToDouble(TextBox14.Text)
        End If
        total14 = value14 * Convert.ToDecimal(TextBox14.Tag.ToString.Replace(".", ","))
        If TextBox15.Text = "" Then
            value15 = 0
        Else
            value15 = Convert.ToDouble(TextBox15.Text)
        End If
        total15 = value15 * Convert.ToDecimal(TextBox15.Tag.ToString.Replace(".", ","))
        If TextBox16.Text = "" Then
            value16 = 0
        Else
            value16 = Convert.ToDouble(TextBox16.Text)
        End If
        total16 = value16 * Convert.ToDecimal(TextBox16.Tag.ToString.Replace(".", ","))
        If TextBox17.Text = "" Then
            value17 = 0
        Else
            value17 = Convert.ToDouble(TextBox17.Text)
        End If
        total17 = value17 * Convert.ToDecimal(TextBox17.Tag.ToString.Replace(".", ","))
        If TextBox18.Text = "" Then
            value18 = 0
        Else
            value18 = Convert.ToDouble(TextBox18.Text)
        End If
        total18 = value18 * Convert.ToDecimal(TextBox18.Tag.ToString.Replace(".", ","))
        If TextBox19.Text = "" Then
            value19 = 0
        Else
            value19 = Convert.ToDouble(TextBox19.Text)
        End If
        total19 = value19 * Convert.ToDecimal(TextBox19.Tag.ToString.Replace(".", ","))
        If TextBox20.Text = "" Then
            value20 = 0
        Else
            value20 = Convert.ToDouble(TextBox20.Text)
        End If
        total20 = value20 * Convert.ToDecimal(TextBox20.Tag.ToString.Replace(".", ","))

        Label52.Text = (Convert.ToDecimal(total11.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total12.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total13.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total14.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total15.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total16.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total17.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total18.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total19.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total20.ToString().Replace(".", ",").Replace(" ", ""))).ToString("# ##0.00")
    End Sub
    Private Sub TextBox19_TextChanged(sender As Object, e As EventArgs) Handles TextBox19.TextChanged
        Dim total11 As Double = 0.00
        Dim total12 As Double = 0.00
        Dim total13 As Double = 0.00
        Dim total14 As Double = 0.00
        Dim total15 As Double = 0.00
        Dim total16 As Double = 0.00
        Dim total17 As Double = 0.00
        Dim total18 As Double = 0.00
        Dim total19 As Double = 0.00
        Dim total20 As Double = 0.00
        Dim value11 As Double
        Dim value12 As Double
        Dim value13 As Double
        Dim value14 As Double
        Dim value15 As Double
        Dim value16 As Double
        Dim value17 As Double
        Dim value18 As Double
        Dim value19 As Double
        Dim value20 As Double

        If TextBox11.Text = "" Then
            value11 = 0
        Else
            value11 = Convert.ToDouble(TextBox11.Text)
        End If
        total11 = value11 * Convert.ToDecimal(TextBox11.Tag.ToString.Replace(".", ","))
        If TextBox12.Text = "" Then
            value12 = 0
        Else
            value12 = Convert.ToDouble(TextBox12.Text)
        End If
        total12 = value12 * Convert.ToDecimal(TextBox12.Tag.ToString.Replace(".", ","))
        If TextBox13.Text = "" Then
            value13 = 0
        Else
            value13 = Convert.ToDouble(TextBox13.Text)
        End If
        total13 = value13 * Convert.ToDecimal(TextBox13.Tag.ToString.Replace(".", ","))
        If TextBox14.Text = "" Then
            value14 = 0
        Else
            value14 = Convert.ToDouble(TextBox14.Text)
        End If
        total14 = value14 * Convert.ToDecimal(TextBox14.Tag.ToString.Replace(".", ","))
        If TextBox15.Text = "" Then
            value15 = 0
        Else
            value15 = Convert.ToDouble(TextBox15.Text)
        End If
        total15 = value15 * Convert.ToDecimal(TextBox15.Tag.ToString.Replace(".", ","))
        If TextBox16.Text = "" Then
            value16 = 0
        Else
            value16 = Convert.ToDouble(TextBox16.Text)
        End If
        total16 = value16 * Convert.ToDecimal(TextBox16.Tag.ToString.Replace(".", ","))
        If TextBox17.Text = "" Then
            value17 = 0
        Else
            value17 = Convert.ToDouble(TextBox17.Text)
        End If
        total17 = value17 * Convert.ToDecimal(TextBox17.Tag.ToString.Replace(".", ","))
        If TextBox18.Text = "" Then
            value18 = 0
        Else
            value18 = Convert.ToDouble(TextBox18.Text)
        End If
        total18 = value18 * Convert.ToDecimal(TextBox18.Tag.ToString.Replace(".", ","))
        If TextBox19.Text = "" Then
            value19 = 0
        Else
            value19 = Convert.ToDouble(TextBox19.Text)
        End If
        total19 = value19 * Convert.ToDecimal(TextBox19.Tag.ToString.Replace(".", ","))
        If TextBox20.Text = "" Then
            value20 = 0
        Else
            value20 = Convert.ToDouble(TextBox20.Text)
        End If
        total20 = value20 * Convert.ToDecimal(TextBox20.Tag.ToString.Replace(".", ","))

        Label52.Text = (Convert.ToDecimal(total11.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total12.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total13.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total14.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total15.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total16.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total17.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total18.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total19.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total20.ToString().Replace(".", ",").Replace(" ", ""))).ToString("# ##0.00")
    End Sub
    Private Sub TextBox20_TextChanged(sender As Object, e As EventArgs) Handles TextBox20.TextChanged
        Dim total11 As Double = 0.00
        Dim total12 As Double = 0.00
        Dim total13 As Double = 0.00
        Dim total14 As Double = 0.00
        Dim total15 As Double = 0.00
        Dim total16 As Double = 0.00
        Dim total17 As Double = 0.00
        Dim total18 As Double = 0.00
        Dim total19 As Double = 0.00
        Dim total20 As Double = 0.00
        Dim value11 As Double
        Dim value12 As Double
        Dim value13 As Double
        Dim value14 As Double
        Dim value15 As Double
        Dim value16 As Double
        Dim value17 As Double
        Dim value18 As Double
        Dim value19 As Double
        Dim value20 As Double

        If TextBox11.Text = "" Then
            value11 = 0
        Else
            value11 = Convert.ToDouble(TextBox11.Text)
        End If
        total11 = value11 * Convert.ToDecimal(TextBox11.Tag.ToString.Replace(".", ","))
        If TextBox12.Text = "" Then
            value12 = 0
        Else
            value12 = Convert.ToDouble(TextBox12.Text)
        End If
        total12 = value12 * Convert.ToDecimal(TextBox12.Tag.ToString.Replace(".", ","))
        If TextBox13.Text = "" Then
            value13 = 0
        Else
            value13 = Convert.ToDouble(TextBox13.Text)
        End If
        total13 = value13 * Convert.ToDecimal(TextBox13.Tag.ToString.Replace(".", ","))
        If TextBox14.Text = "" Then
            value14 = 0
        Else
            value14 = Convert.ToDouble(TextBox14.Text)
        End If
        total14 = value14 * Convert.ToDecimal(TextBox14.Tag.ToString.Replace(".", ","))
        If TextBox15.Text = "" Then
            value15 = 0
        Else
            value15 = Convert.ToDouble(TextBox15.Text)
        End If
        total15 = value15 * Convert.ToDecimal(TextBox15.Tag.ToString.Replace(".", ","))
        If TextBox16.Text = "" Then
            value16 = 0
        Else
            value16 = Convert.ToDouble(TextBox16.Text)
        End If
        total16 = value16 * Convert.ToDecimal(TextBox16.Tag.ToString.Replace(".", ","))
        If TextBox17.Text = "" Then
            value17 = 0
        Else
            value17 = Convert.ToDouble(TextBox17.Text)
        End If
        total17 = value17 * Convert.ToDecimal(TextBox17.Tag.ToString.Replace(".", ","))
        If TextBox18.Text = "" Then
            value18 = 0
        Else
            value18 = Convert.ToDouble(TextBox18.Text)
        End If
        total18 = value18 * Convert.ToDecimal(TextBox18.Tag.ToString.Replace(".", ","))
        If TextBox19.Text = "" Then
            value19 = 0
        Else
            value19 = Convert.ToDouble(TextBox19.Text)
        End If
        total19 = value19 * Convert.ToDecimal(TextBox19.Tag.ToString.Replace(".", ","))
        If TextBox20.Text = "" Then
            value20 = 0
        Else
            value20 = Convert.ToDouble(TextBox20.Text)
        End If
        total20 = value20 * Convert.ToDecimal(TextBox20.Tag.ToString.Replace(".", ","))

        Label52.Text = (Convert.ToDecimal(total11.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total12.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total13.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total14.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total15.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total16.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total17.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total18.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total19.ToString().Replace(".", ",").Replace(" ", "")) + Convert.ToDecimal(total20.ToString().Replace(".", ",").Replace(" ", ""))).ToString("# ##0.00")
    End Sub
    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox11.KeyDown, TextBox12.KeyDown, TextBox13.KeyDown, TextBox14.KeyDown, TextBox15.KeyDown, TextBox16.KeyDown, TextBox17.KeyDown, TextBox18.KeyDown, TextBox19.KeyDown, TextBox20.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Supprimer la touche "Enter"
            SendKeys.Send("{TAB}")    ' Simuler la touche "Tab"
        End If
    End Sub
    Private Sub IconButton51_Click_2(sender As Object, e As EventArgs) Handles IconButton51.Click
        Try


            adpt = New MySqlDataAdapter("select * from orders WHERE parked = @day and cloture = @dayo", conn2)
            adpt.SelectCommand.Parameters.Clear()
            adpt.SelectCommand.Parameters.AddWithValue("@day", "yes")
            adpt.SelectCommand.Parameters.AddWithValue("@dayo", 0)
            Dim table1 As New DataTable
            adpt.Fill(table1)

            If table1.Rows.Count() = 0 Then

                Dim dte As String = DateTime.Today.ToString("dd/MM/yyyy")
                Dim sumord As Decimal = 0
                Dim sumfac As Decimal = 0
                Dim crdord As Decimal = 0
                Dim crdfac As Decimal = 0
                Dim sumchq As Decimal = 0
                Dim tpeord As Decimal = 0
                Dim logi As Decimal = 0

                adpt = New MySqlDataAdapter("select MontantOrder from orders WHERE cloture = @day and modePayement = @mode", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", 0)
                adpt.SelectCommand.Parameters.AddWithValue("@mode", "Espèce")
                Dim tableespecetick As New DataTable
                adpt.Fill(tableespecetick)

                adpt = New MySqlDataAdapter("select montant from reglement WHERE achat IS NULL and `mode` = 'Espèce (Comptoir)' and cloture = @day", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", 0)
                Dim tableenc As New DataTable
                adpt.Fill(tableenc)

                Dim sumenc As Decimal = 0

                If tableespecetick.Rows().Count() = 0 Then
                    sumord = 0
                Else
                    For i = 0 To tableespecetick.Rows().Count() - 1
                        sumord += Convert.ToDecimal(tableespecetick.Rows(i).Item(0).ToString.Replace(" ", "").Replace(".", ","))
                    Next
                End If
                If tableenc.Rows().Count() = 0 Then
                    sumenc += 0
                Else
                    For i = 0 To tableenc.Rows().Count() - 1
                        sumenc += Convert.ToDecimal(tableenc.Rows(i).Item(0).ToString.Replace(" ", "").Replace(".", ","))
                    Next
                End If


                adpt = New MySqlDataAdapter("select MontantOrder from orders WHERE cloture = @day and modePayement = @mode", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", 0)

                adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
                Dim tablecredittick As New DataTable
                adpt.Fill(tablecredittick)

                If tablecredittick.Rows().Count() = 0 Then
                    crdord = 0
                Else
                    For i = 0 To tablecredittick.Rows().Count() - 1
                        crdord += Convert.ToDecimal(tablecredittick.Rows(i).Item(0).ToString.Replace(".", ","))
                    Next
                End If

                adpt = New MySqlDataAdapter("select MontantOrder from orders WHERE cloture = @day and modePayement = @mode", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", 0)
                adpt.SelectCommand.Parameters.AddWithValue("@mode", "TPE")
                Dim tabletpetick As New DataTable
                adpt.Fill(tabletpetick)

                adpt = New MySqlDataAdapter("select montant from reglement WHERE achat IS NULL and mode = 'TPE' and cloture = @day", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", 0)
                Dim tabletpe As New DataTable
                adpt.Fill(tabletpe)

                If tabletpetick.Rows().Count() = 0 Then
                    tpeord = 0
                Else
                    For i = 0 To tabletpetick.Rows().Count() - 1
                        tpeord += Convert.ToDecimal(tabletpetick.Rows(i).Item(0).ToString.Replace(".", ","))
                    Next
                End If

                If tabletpe.Rows().Count() = 0 Then
                    tpeord += 0
                Else
                    For i = 0 To tabletpe.Rows().Count() - 1
                        tpeord += Convert.ToDecimal(tabletpe.Rows(i).Item(0).ToString.Replace(".", ","))
                    Next
                End If

                adpt = New MySqlDataAdapter("select montant from reglement WHERE achat IS NULL and mode = 'Chèque' and cloture = @day", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", 0)
                Dim tablechq As New DataTable
                adpt.Fill(tablechq)

                For i = 0 To tablechq.Rows.Count() - 1
                    sumchq += Convert.ToDecimal(tablechq.Rows(i).Item(0).ToString.Replace(".", ","))

                Next

                Dim sum As Decimal = Convert.ToDecimal(sumord) + Convert.ToDecimal(sumenc) + Convert.ToDecimal(sumchq) + Convert.ToDecimal(tpeord)

                Dim especevar As Decimal = Convert.ToDecimal(sumord)
                Dim chèquevar As Decimal = Convert.ToDecimal(sumchq)
                Dim cartevar As Decimal = Convert.ToDecimal(tpeord)
                Dim creditvar As Decimal = Convert.ToDecimal(crdord)

                Dim sortievar As Decimal = 0
                adpt = New MySqlDataAdapter("select mtn from tiroir WHERE cloture = @day", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", 0)

                Dim tablesortie As New DataTable
                adpt.Fill(tablesortie)
                If tablesortie.Rows.Count() = 0 Then

                    sortievar = 0
                Else
                    For i = 0 To tablesortie.Rows().Count() - 1
                        sortievar += Convert.ToDecimal(tablesortie.Rows(i).Item(0).ToString.Replace(".", ","))
                    Next

                End If


                adpt = New MySqlDataAdapter("SELECT sum(transport), livreur, count(*) FROM orders where cloture = @day and livreur NOT IN ('-', 'Livreur') AND livreur IS NOT NULL GROUP BY livreur", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", 0)

                Dim tablelivreur As New DataTable
                adpt.Fill(tablelivreur)
                Dim sumlivreur As Decimal = 0
                For i = 0 To tablelivreur.Rows.Count - 1
                    sumlivreur += Convert.ToDecimal(tablelivreur.Rows(i).Item(0).ToString.Replace(".", ","))
                Next

                adpt = New MySqlDataAdapter("SELECT Entre, Sortie FROM caisse where Day(date) = @day and Month(date) = @month and Year(date) = @year and Compte = 'Caisse comptoir'", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", DateTime.Now.Day)
                adpt.SelectCommand.Parameters.AddWithValue("@month", DateTime.Now.Month)
                adpt.SelectCommand.Parameters.AddWithValue("@year", DateTime.Now.Year)

                Dim tablecaisse As New DataTable
                adpt.Fill(tablecaisse)
                Dim sumcaissein As Decimal = 0
                Dim sumcaisseout As Decimal = 0
                For i = 0 To tablecaisse.Rows.Count - 1
                    sumcaissein += Convert.ToDecimal(tablecaisse.Rows(i).Item(0).ToString.Replace(".", ","))
                    sumcaisseout += Convert.ToDecimal(tablecaisse.Rows(i).Item(1).ToString.Replace(".", ","))
                Next

                Dim soldevar As Decimal = Convert.ToDecimal(especevar) + Convert.ToDecimal(sumenc) + sumcaissein - Convert.ToDecimal(sortievar) - Convert.ToDecimal(sumlivreur) - sumcaisseout
                Dim timbrevar As Decimal = (Convert.ToDecimal(especevar) * 0.25) / 100
                Dim caissiervar As String = Label2.Text

                adpt = New MySqlDataAdapter("select * from tiroir WHERE cloture = @day order by date desc ", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", 0)

                Dim table As New DataTable
                adpt.Fill(table)


                adpt = New MySqlDataAdapter("select OrderID,client,MontantOrder from orders WHERE cloture = @day and modePayement = @mode", conn2)
                adpt.SelectCommand.Parameters.Clear()
                adpt.SelectCommand.Parameters.AddWithValue("@day", 0)

                adpt.SelectCommand.Parameters.AddWithValue("@mode", "Crédit")
                Dim tablecredit As New DataTable
                adpt.Fill(tablecredit)

                Dim ds1 As New DataSet1
                Dim dt1 As New DataTable
                dt1 = ds1.Tables("Livreurs")
                For i = 0 To tablelivreur.Rows().Count() - 1
                    dt1.Rows.Add(tablelivreur.Rows(i).Item(1).ToString.Replace(".", ","), tablelivreur.Rows(i).Item(0).ToString.Replace(".", ","), tablelivreur.Rows(i).Item(2))
                Next

                Dim ds3 As New DataSet1
                Dim dt3 As New DataTable
                dt3 = ds3.Tables("credit")
                For i = 0 To tablecredit.Rows().Count() - 1
                    dt3.Rows.Add("(T-" & tablecredit.Rows(i).Item(0) & ") " & tablecredit.Rows(i).Item(1), tablecredit.Rows(i).Item(2).ToString.Replace(".", ","))
                Next

                Dim ds2 As New DataSet1
                Dim dt2 As New DataTable
                dt2 = ds2.Tables("clot")
                dt2.Rows.Add(Label51.Text, TextBox11.Text)
                dt2.Rows.Add(Label33.Text, TextBox12.Text)
                dt2.Rows.Add(Label35.Text, TextBox13.Text)
                dt2.Rows.Add(Label37.Text, TextBox14.Text)
                dt2.Rows.Add(Label39.Text, TextBox15.Text)
                dt2.Rows.Add(Label41.Text, TextBox16.Text)
                dt2.Rows.Add(Label43.Text, TextBox17.Text)
                dt2.Rows.Add(Label45.Text, TextBox18.Text)
                dt2.Rows.Add(Label47.Text, TextBox19.Text)
                dt2.Rows.Add(Label49.Text, TextBox20.Text)

                Dim ds As New DataSet1
                Dim dt As New DataTable
                dt = ds.Tables("sortie")
                For i = 0 To table.Rows().Count() - 1
                    dt.Rows.Add(table.Rows(i).Item(1).ToString.Replace(".", ","), table.Rows(i).Item(2).ToString.Replace(".", ","))
                Next
                dt.Rows.Add("Retrait en espèce", sumcaisseout)

                ReportToPrint = New LocalReport()
                ReportToPrint.ReportPath = Application.StartupPath + "\etatcaisse.rdlc"
                ReportToPrint.DataSources.Clear()
                ReportToPrint.EnableExternalImages = True

                Dim dateM As New ReportParameter("date", dte)
                Dim dateM1(0) As ReportParameter
                dateM1(0) = dateM
                ReportToPrint.SetParameters(dateM1)

                Dim total As New ReportParameter("total", sum)
                Dim total1(0) As ReportParameter
                total1(0) = total
                ReportToPrint.SetParameters(total1)

                Dim journal As New ReportParameter("Journal", "Journal X")
                Dim journal1(0) As ReportParameter
                journal1(0) = journal
                ReportToPrint.SetParameters(journal1)

                Dim caissier As New ReportParameter("caissier", caissiervar)
                Dim caissier1(0) As ReportParameter
                caissier1(0) = caissier
                ReportToPrint.SetParameters(caissier1)

                Dim espece As New ReportParameter("espece", especevar)
                Dim espece1(0) As ReportParameter
                espece1(0) = espece
                ReportToPrint.SetParameters(espece1)

                Dim chèque As New ReportParameter("chèque", chèquevar)
                Dim chèque1(0) As ReportParameter
                chèque1(0) = chèque
                ReportToPrint.SetParameters(chèque1)

                Dim alim As New ReportParameter("alim", sumcaissein)
                Dim alim1(0) As ReportParameter
                alim1(0) = alim
                ReportToPrint.SetParameters(alim1)

                Dim encclient As New ReportParameter("encclient", sumenc)
                Dim encclient1(0) As ReportParameter
                encclient1(0) = encclient
                ReportToPrint.SetParameters(encclient1)

                Dim carte As New ReportParameter("carte", cartevar)
                Dim carte1(0) As ReportParameter
                carte1(0) = carte
                ReportToPrint.SetParameters(carte1)

                Dim credit As New ReportParameter("credit", creditvar)
                Dim credit1(0) As ReportParameter
                credit1(0) = credit
                ReportToPrint.SetParameters(credit1)

                Dim sortie As New ReportParameter("sortie", sortievar + sumcaisseout)
                Dim sortie1(0) As ReportParameter
                sortie1(0) = sortie
                ReportToPrint.SetParameters(sortie1)

                Dim solde As New ReportParameter("solde", soldevar)
                Dim solde1(0) As ReportParameter
                solde1(0) = solde
                ReportToPrint.SetParameters(solde1)

                Dim timbre As New ReportParameter("timbre", timbrevar)
                Dim timbre1(0) As ReportParameter
                timbre1(0) = timbre
                ReportToPrint.SetParameters(timbre1)


                adpt = New MySqlDataAdapter("Select * from infos", conn2)
                Dim tableimg As New DataTable
                adpt.Fill(tableimg)
                Dim appPath As String = Application.StartupPath()

                Dim SaveDirectory As String = appPath & "\"
                Dim SavePath As String = SaveDirectory & imgName.Replace("/", "\")

                Dim img As New ReportParameter("image", "File:\" + SavePath, True)
                Dim img1(0) As ReportParameter
                img1(0) = img
                ReportToPrint.SetParameters(img1)

                ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt1))
                ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dt))
                ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", dt2))
                ReportToPrint.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet4", dt3))


                Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWidth>3.7in</PageWidth><PageHeight>12in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
                Dim warnings As Warning()
                m_streams = New List(Of Stream)()
                ReportToPrint.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
                For Each stream As Stream In m_streams
                    stream.Position = 0
                Next
                Dim printDoc As New PrintDocument()
                Dim printname As String = receiptprinter
                printDoc.PrinterSettings.PrinterName = printname
                Dim ps As New PrinterSettings()
                ps.PrinterName = printDoc.PrinterSettings.PrinterName
                printDoc.PrinterSettings = ps

                AddHandler printDoc.PrintPage, AddressOf PrintDocument_PrintPage
                m_currentPageIndex = 0
                printDoc.Print()
                TextBox2.Select()
                TextBox2.Text = ""

                conn2.Close()
                conn2.Open()

                Dim sql2 As String
                Dim cmd2 As New MySqlCommand
                sql2 = "INSERT INTO `archivecaisse`(`systeme`, `caisse`, `caissier`, `ecart`,`cloture`) VALUES (@v1,@v2,@v3,@v4,0)"
                cmd2 = New MySqlCommand(sql2, conn2)
                cmd2.Parameters.AddWithValue("@v1", soldevar)
                cmd2.Parameters.AddWithValue("@v2", Convert.ToDecimal(Label52.Text).ToString("# ##0.00"))
                cmd2.Parameters.AddWithValue("@v3", Label2.Text)
                cmd2.Parameters.AddWithValue("@v4", Convert.ToDecimal(Label52.Text - soldevar).ToString("# ##0.00"))

                cmd2.ExecuteNonQuery()

                conn2.Close()

            Else
                MsgBox("Veuillez vider votre Park de commande !")
            End If


        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IconButton56_Click(sender As Object, e As EventArgs) Handles IconButton56.Click
        Panel15.Visible = False
        factures.Show()
        factures.Panel8.Visible = True
        factures.IconButton16.Visible = False
        TextBox2.Select()
        TextBox2.Text = ""
    End Sub

    Private Sub TextBox30_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox30.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim price As Double = Convert.ToDouble(TextBox30.Text.Replace(".", ",").Replace(" ", "")).ToString("N2")
            DataGridView3.Rows.Add("DIVERS", "PRODUIT DIVERS", 1, price, "DIVERS", price, price, "", 0, 0, price, 0.00)
            DataGridView3.ClearSelection()

            'Scroll to the last row.
            Me.DataGridView3.FirstDisplayedScrollingRowIndex = Me.DataGridView3.RowCount - 1

            'Select the last row.
            Me.DataGridView3.Rows(Me.DataGridView3.RowCount - 1).Selected = True
            Dim sum As Decimal = 0
            Dim sum2 As Decimal = 0
            Dim sum3 As Decimal = 0
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

            For i = 0 To DataGridView3.Rows.Count - 1
                sum = sum + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", ""))

                prixHT = DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                quantite = DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                tauxTVA = DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                pourcentageRemise = DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                ' Étape 1 : Calculer le montant HT après la remise
                montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

                ' Étape 2 : Calculer le montant TTC avant TVA
                montantTTCAvantTVA = montantHTApresRemise * quantite

                ' Étape 3 : Calculer le montant de la TVA
                montantTVA = montantTTCAvantTVA * tauxTVA

                sum2 += montantTTCAvantTVA
                sum3 += montantTVA


                DataGridView3.Rows(i).Cells(7).Value = i
            Next
            Label23.Text = sum2
            Label24.Text = sum - sum2
            Label25.Text = Label23.Text

            Label6.Text = Convert.ToDecimal(sum.ToString.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
            Try
                If IsNumeric(TextBox3.Text) = False Then
                    If Label9.Text = "Rendu :" Then
                        Label4.Text = 0.00
                    End If
                    If Label9.Text = "Reste :" Then
                        Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                    End If
                Else
                    If Label9.Text = "Rendu :" Then
                        Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                    End If
                    If Label9.Text = "Reste :" Then
                        Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                    End If
                End If
            Catch ex As MySqlException
                MsgBox(ex.Message)
            End Try
            Label7.Text = sum.ToString("# ##0.00")
            Dim totalpanier As Double = 0
            For i = 0 To DataGridView3.Rows.Count - 1
                totalpanier += Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ","))
            Next
            IconButton17.Text = totalpanier
            Panel11.Visible = False
            TextBox2.Text = ""
            TextBox2.Select()
        End If
        If e.KeyCode = Keys.Escape Then
            Panel11.Visible = False
            TextBox2.Text = ""
            TextBox2.Select()
        End If

    End Sub

    Private Sub IconButton57_Click(sender As Object, e As EventArgs) Handles IconButton57.Click
        If Panel11.Visible = False Then
            Panel11.Visible = True
            TextBox30.Text = 0
            TextBox30.Select()
        Else
            Panel11.Visible = False
            TextBox2.Text = ""
            TextBox2.Select()
        End If

    End Sub

    Private Sub IconButton58_Click(sender As Object, e As EventArgs) Handles IconButton58.Click
        Panel15.Visible = False
        Panel7.Visible = True
        TextBox4.Select()
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub DataGridView3_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellDoubleClick
        If e.RowIndex > -1 Then
            rowIndex = e.RowIndex
        End If


        If e.ColumnIndex <> 0 Then

            calcul.Close()
            ds = Me
            calcul.ShowDialog()
        End If
    End Sub

    Private Sub IconButton59_Click(sender As Object, e As EventArgs) Handles IconButton59.Click
        Panel7.Visible = False
        TextBox2.Select()
    End Sub

    Private Sub IconButton60_Click(sender As Object, e As EventArgs) Handles IconButton60.Click
        If DataGridView3.SelectedRows.Count > 0 Then
            DataGridView3.Rows.RemoveAt(DataGridView3.CurrentRow.Index)

            Dim sum As Decimal = 0
            Dim sum2 As Decimal = 0
            Dim sum3 As Decimal = 0
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

            For i = 0 To DataGridView3.Rows.Count - 1
                sum = sum + Convert.ToDecimal(DataGridView3.Rows(i).Cells(6).Value.ToString.Replace(" ", ""))

                prixHT = DataGridView3.Rows(i).Cells(3).Value.ToString.Replace(" ", "") / (1 + DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "")) ' Remplacez cette valeur par votre prix HT
                quantite = DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "") - DataGridView3.Rows(i).Cells(9).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre quantité
                tauxTVA = DataGridView3.Rows(i).Cells(11).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre taux de TVA (exprimé en décimales)
                pourcentageRemise = DataGridView3.Rows(i).Cells(8).Value.ToString.Replace(" ", "") ' Remplacez cette valeur par votre pourcentage de remise

                ' Étape 1 : Calculer le montant HT après la remise
                montantHTApresRemise = prixHT * (1 - pourcentageRemise / 100)

                ' Étape 2 : Calculer le montant TTC avant TVA
                montantTTCAvantTVA = montantHTApresRemise * quantite

                ' Étape 3 : Calculer le montant de la TVA
                montantTVA = montantTTCAvantTVA * tauxTVA

                sum2 += montantTTCAvantTVA
                sum3 += montantTVA

            Next
            Label23.Text = sum2
            Label24.Text = sum - sum2
            Label25.Text = Label23.Text
            Label6.Text = sum.ToString("# ##0.00").Replace(" ", "")
            Try
                If IsNumeric(TextBox3.Text) = False Then
                    If Label9.Text = "Rendu :" Then
                        Label4.Text = 0.00
                    End If
                    If Label9.Text = "Reste :" Then
                        Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                    End If
                Else
                    If Label9.Text = "Rendu :" Then
                        Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                    End If
                    If Label9.Text = "Reste :" Then
                        Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                    End If
                End If
            Catch ex As MySqlException
                MsgBox(ex.Message)
            End Try
            Label7.Text = sum.ToString("# ##0.00")
            Dim totalpanier As Double = 0
            For i = 0 To DataGridView3.Rows.Count - 1
                totalpanier += Convert.ToDecimal(DataGridView3.Rows(i).Cells(2).Value.ToString.Replace(" ", "").Replace(".", ","))
            Next
            IconButton17.Text = totalpanier




            TextBox4.Text = ""
            TextBox2.Select()
            TextBox2.Text = ""

        End If
    End Sub

    Private Sub IconButton61_Click(sender As Object, e As EventArgs) Handles IconButton61.Click
        If TextBox21.Text = "" Then
            TextBox21.Text = 0
        End If
        If Convert.ToDouble(TextBox21.Text.Replace(" ", "").Replace(".", ",")) > 0 Then

            Label6.Text = Convert.ToDecimal(Label7.Text.Replace(" ", "") - (Label7.Text.Replace(" ", "") * (TextBox21.Text.Replace(".", ",") / 100))).ToString("N2")
        Else
            Label6.Text = Label7.Text.Replace(" ", "")
        End If
        Try
            If IsNumeric(TextBox3.Text) = False Then
                If Label9.Text = "Rendu :" Then
                    Label4.Text = 0.00
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
            Else
                If Label9.Text = "Rendu :" Then
                    Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                End If
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        Label62.Text = "yes"
        Panel13.Visible = False
    End Sub

    Private Sub IconButton63_Click(sender As Object, e As EventArgs) Handles IconButton63.Click
        If TextBox21.Text = "" Then
            TextBox21.Text = 0
        End If
        If Convert.ToDouble(TextBox21.Text.Replace(" ", "").Replace(".", ",")) > 0 Then

            Label6.Text = Convert.ToDecimal(Label23.Text - TextBox21.Text.Replace(".", ",") + Label24.Text).ToString("N2")
        Else
            Label6.Text = Label7.Text.Replace(" ", "")
        End If
        Try
            If IsNumeric(TextBox3.Text) = False Then
                If Label9.Text = "Rendu :" Then
                    Label4.Text = 0.00
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
            Else
                If Label9.Text = "Rendu :" Then
                    Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                End If
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        Panel13.Visible = False
        TextBox21.Text = ""
        Label62.Text = "yes"
    End Sub

    Private Sub IconButton64_Click(sender As Object, e As EventArgs) Handles IconButton64.Click
        If TextBox21.Text = "" Then
            TextBox21.Text = 0
        End If

        Label6.Text = Convert.ToDouble(TextBox21.Text.Replace(" ", "").Replace(".", ",")).ToString("N2")

        Try
            If IsNumeric(TextBox3.Text) = False Then
                If Label9.Text = "Rendu :" Then
                    Label4.Text = 0.00
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
            Else
                If Label9.Text = "Rendu :" Then
                    Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                End If
                If Label9.Text = "Reste :" Then
                    Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                End If
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        Panel13.Visible = False
        TextBox21.Text = ""
        Label62.Text = "yes"
    End Sub

    Private Sub IconButton62_Click(sender As Object, e As EventArgs) Handles IconButton62.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Caisse - Ticket Operations'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Non Autorise" Then
                MsgBox("Vous n'avez pas l'Autorisation !")
            Else
                If Panel13.Visible = False Then
                    Panel13.Visible = True
                    TextBox21.Text = ""
                    TextBox21.Select()
                Else
                    Panel13.Visible = False
                    TextBox21.Text = ""
                End If
            End If
        End If


    End Sub

    Private Sub IconButton65_Click(sender As Object, e As EventArgs) Handles IconButton65.Click
        Panel15.Visible = False
        ajout.Show()
        Panel10.Visible = False
        ajout.Panel8.Visible = True
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        Dim inputText As String = TextBox4.Text

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
            RemoveHandler TextBox4.TextChanged, AddressOf TextBox4_TextChanged

            ' Mettez à jour le texte dans le TextBox
            TextBox4.Text = modifiedText

            ' Replacez le curseur à la position correcte après la modification
            TextBox4.SelectionStart = TextBox4.Text.Length

            ' Réactivez le gestionnaire d'événements TextChanged
            AddHandler TextBox4.TextChanged, AddressOf TextBox4_TextChanged
        End If
    End Sub

    Private Sub IconButton67_Click(sender As Object, e As EventArgs) Handles IconButton67.Click
        If Panel15.Visible = True Then
            Panel15.Visible = False
        Else
            Panel15.Visible = True

        End If
    End Sub

    Private Sub IconButton66_Click(sender As Object, e As EventArgs) Handles IconButton66.Click
        Panel15.Visible = False
    End Sub

    Private Sub IconButton68_Click(sender As Object, e As EventArgs) Handles IconButton68.Click
        Panel15.Visible = False
        balisage.ShowDialog()
        Panel10.Visible = False
    End Sub

    Private Sub IconButton69_Click(sender As Object, e As EventArgs)
        BackgroundWorker2.RunWorkerAsync()

    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker2.DoWork


    End Sub

    Private Sub IconButton69_Click_1(sender As Object, e As EventArgs) Handles IconButton69.Click
        Panel15.Visible = False
        opentr.Show()
        opentr.Panel8.Visible = True
        adpt = New MySqlDataAdapter("select * from clients where remise > 0 and masq = 'non' order by client asc", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim sum As Double
        opentr.DataGridView4.Rows.Clear()
        For i = 0 To table.Rows.Count() - 1
            opentr.DataGridView4.Rows.Add(table.Rows(i).Item(1), table.Rows(i).Item(22), "Valider")
        Next
    End Sub
    Dim search As Boolean = False
    Private Sub dashboard_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If search = False Then
            If Panel6.Visible = False Then
                Panel15.Visible = False
            End If
            If Panel5.Visible = False AndAlso Char.IsLetterOrDigit(e.KeyChar) AndAlso Panel7.Visible = False AndAlso Panel15.Visible = False AndAlso Panel11.Visible = False And Panel18.Visible = False Then
                ' Définir le focus sur TextBox2
                TextBox2.Focus()

                ' Ajouter le caractère actuellement tapé à la fin du contenu de TextBox2
                TextBox2.Text &= e.KeyChar

                ' Empêcher le caractère tapé d'être traité à nouveau par d'autres contrôles
                e.Handled = True
            End If

        End If
    End Sub

    Private Sub dashboard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Label28.Text = "numbl" Then
        Else
            If Panel8.Visible = True Then
                IconButton49.PerformClick()
            Else
                IconButton37.PerformClick()
            End If
        End If
    End Sub

    Private Sub TextBox22_TextChanged(sender As Object, e As EventArgs) Handles TextBox22.TextChanged

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

        Dim texteRecherche As String = TextBox22.Text.Trim().ToLower()

        If Not String.IsNullOrEmpty(texteRecherche) Then
            For Each row As DataGridViewRow In DataGridView3.Rows
                Dim cell As DataGridViewCell = row.Cells(1) ' Remplacez "Designation" par le nom réel de la colonne
                If cell.Value IsNot Nothing AndAlso cell.Value.ToString().ToLower().Contains(texteRecherche) Then
                    DataGridView3.ClearSelection()
                    row.Selected = True
                    DataGridView3.FirstDisplayedScrollingRowIndex = row.Index ' Fait défiler la grille jusqu'à la ligne sélectionnée
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub Label58_Click(sender As Object, e As EventArgs) Handles Label58.Click

    End Sub

    Private Sub TextBox22_LostFocus(sender As Object, e As EventArgs) Handles TextBox22.LostFocus
        search = False
    End Sub

    Private Sub TextBox22_GotFocus(sender As Object, e As EventArgs) Handles TextBox22.GotFocus
        search = True
    End Sub

    Private Sub IconButton70_Click(sender As Object, e As EventArgs) Handles IconButton70.Click
        If Panel18.Visible = True Then
            Panel18.Visible = False
        Else
            TextBox24.Select()
            Panel18.Visible = True
        End If
    End Sub

    Private Sub IconButton71_Click(sender As Object, e As EventArgs) Handles IconButton71.Click
        Panel18.Visible = False
        ComboBox2.Text = ComboBox6.Text
        Label61.Text = ComboBox6.Text
        TextBox8.Text = ComboBox6.Text
    End Sub

    Private Sub TextBox24_TextChanged(sender As Object, e As EventArgs) Handles TextBox24.TextChanged
        Try
            If TextBox24.Text = "" Then
                adpt = New MySqlDataAdapter("select client from clients where masq = 'non' order by client asc", conn2)
                Dim table As New DataTable
                adpt.Fill(table)
                If table.Rows.Count <> 0 Then
                    ComboBox6.Items.Clear()
                    For i = 0 To table.Rows.Count - 1
                        ComboBox6.Items.Add(table.Rows(i).Item(0))
                    Next
                End If
                ComboBox6.SelectedItem = "comptoir"
                TextBox21.Text = 0
                Label6.Text = Convert.ToDecimal(Label7.Text.Replace(" ", "")).ToString("#.00")

                Try
                    If IsNumeric(TextBox3.Text) = False Then
                        If Label9.Text = "Rendu :" Then
                            Label4.Text = 0.00
                        End If
                        If Label9.Text = "Reste :" Then
                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                        End If
                    Else
                        If Label9.Text = "Rendu :" Then
                            Label4.Text = Convert.ToDecimal(TextBox3.Text.Replace(".", ",") - Label6.Text.Replace(".", ",").Replace(" ", "")).ToString("# ##0.00")
                        End If
                        If Label9.Text = "Reste :" Then
                            Label4.Text = Convert.ToDecimal(Label6.Text.Replace(".", ",").Replace(" ", "") - TextBox3.Text.Replace(".", ",")).ToString("# ##0.00")
                        End If
                    End If
                Catch ex As MySqlException
                    MsgBox(ex.Message)
                End Try
                ComboBox6.Visible = False
                IconButton46.Visible = False
                Panel8.Visible = False
            Else


                Dim inputText As String = TextBox24.Text

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
                    RemoveHandler TextBox24.TextChanged, AddressOf TextBox24_TextChanged

                    ' Mettez à jour le texte dans le TextBox
                    TextBox24.Text = modifiedText

                    ' Replacez le curseur à la position correcte après la modification
                    TextBox24.SelectionStart = TextBox24.Text.Length

                    ' Réactivez le gestionnaire d'événements TextChanged
                    AddHandler TextBox24.TextChanged, AddressOf TextBox24_TextChanged
                End If

                ComboBox6.Items.Clear()

                adpt = New MySqlDataAdapter("select * from fideles WHERE code = '" + TextBox24.Text + "'", conn2)
                Dim table2 As New DataTable
                adpt.Fill(table2)

                If table2.Rows.Count() <> 0 Then

                    ComboBox6.Items.Add("Client fidèle")
                    ComboBox6.Text = "Client fidèle"


                    ComboBox6.Visible = False
                    IconButton46.Visible = False

                Else
                    adpt = New MySqlDataAdapter("select client from clients WHERE client LIKE '%" + TextBox24.Text.Replace("'", " ") + "%' and masq = 'non' order by client asc", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    If table.Rows.Count <> 0 Then
                        For i = 0 To table.Rows.Count - 1
                            ComboBox6.Items.Add(table.Rows(i).Item(0))
                        Next
                        ComboBox6.Visible = True

                        ComboBox6.SelectedIndex = 0
                        IconButton46.Visible = True

                    End If

                End If

            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        IconButton46.Visible = True
        Panel8.Visible = False

    End Sub

    Private Sub IconButton72_Click(sender As Object, e As EventArgs) Handles IconButton72.Click
        adpt = New MySqlDataAdapter("select * from droits where role = '" & role & "' and droit = 'Modifier Client'", conn2)
        Dim tabledroit As New DataTable
        adpt.Fill(tabledroit)
        If tabledroit.Rows.Count <> 0 Then
            If tabledroit.Rows(0).Item(3) = "Autorise" Then


                ComboBox2.Visible = False
        TextBox5.Visible = True
        TextBox23.Visible = True
        Label59.Visible = True
        TextBox5.Text = ComboBox2.Text
        adpt = New MySqlDataAdapter("select ICE from clients where masq = 'non' and client = '" & ComboBox2.Text & "'", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        TextBox23.Text = table.Rows(0).Item(0)
        IconButton14.Visible = True
        IconButton15.Visible = True
        IconButton13.Visible = False
                IconButton13.Visible = False

            Else
                MsgBox("Vous n'avez pas l'autorisation !")
            End If
        End If
    End Sub
End Class

'Friend Class RawPrinterHelper
'    ' Structure and API declarions:
'    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
'    Public Class DOCINFOA
'        <MarshalAs(UnmanagedType.LPStr)>
'        Public pDocName As String
'        <MarshalAs(UnmanagedType.LPStr)>
'        Public pOutputFile As String
'        <MarshalAs(UnmanagedType.LPStr)>
'        Public pDataType As String
'    End Class

'    <DllImport("winspool.Drv", EntryPoint:="OpenPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
'    Public Shared Function OpenPrinter(
'    <MarshalAs(UnmanagedType.LPStr)> ByVal szPrinter As String, <Out> ByRef hPrinter As IntPtr, ByVal pd As IntPtr) As Boolean
'    End Function

'    <DllImport("winspool.Drv", EntryPoint:="ClosePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
'    Public Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As Boolean
'    End Function

'    <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
'    Public Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Integer,
'    <[In], MarshalAs(UnmanagedType.LPStruct)> ByVal di As DOCINFOA) As Boolean
'    End Function

'    <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
'    Public Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As Boolean
'    End Function

'    <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
'    Public Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As Boolean
'    End Function

'    <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
'    Public Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As Boolean
'    End Function

'    <DllImport("winspool.Drv", EntryPoint:="WritePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
'    Public Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Integer, <Out> ByRef dwWritten As Integer) As Boolean
'    End Function


'    ' SendBytesToPrinter()
'    ' When the function is given a printer name and an unmanaged array
'    ' of bytes, the function sends those bytes to the print queue.
'    ' Returns true on success, false on failure.
'    Public Shared Function SendBytesToPrinter(ByVal szPrinterName As String, ByVal pBytes As IntPtr, ByVal dwCount As Integer) As Boolean
'        Dim dwError As Integer = 0, dwWritten As Integer = 0
'        Dim hPrinter As IntPtr = New IntPtr(0)
'        Dim di As DOCINFOA = New DOCINFOA()
'        Dim bSuccess As Boolean = False ' Assume failure unless you specifically succeed.
'        di.pDocName = "My C#.NET RAW Document"
'        di.pDataType = "RAW"


'        ' Open the printer.
'        If OpenPrinter(szPrinterName.Normalize(), hPrinter, IntPtr.Zero) Then

'            ' Start a document.
'            If StartDocPrinter(hPrinter, 1, di) Then

'                ' Start a page.
'                If StartPagePrinter(hPrinter) Then
'                    ' Write your bytes.
'                    bSuccess = WritePrinter(hPrinter, pBytes, dwCount, dwWritten)
'                    EndPagePrinter(hPrinter)
'                End If

'                EndDocPrinter(hPrinter)
'            End If

'            ClosePrinter(hPrinter)
'        End If

'        ' If you did not succeed, GetLastError may give more information
'        ' about why not.
'        If bSuccess = False Then
'            dwError = Marshal.GetLastWin32Error()
'        End If

'        Return bSuccess
'    End Function
'End Class