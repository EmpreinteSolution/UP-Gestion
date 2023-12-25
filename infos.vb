Imports System.Configuration
Imports System.Drawing.Printing
Imports System.IO
Imports MySql.Data.MySqlClient

Public Class infos
    Dim adpt, adpt2, adpt3, adpt9 As MySqlDataAdapter
    Dim sql2, sql3 As String

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Close()
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Dim opf As New OpenFileDialog
        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then

            PictureBox3.Image = Image.FromFile(opf.FileName)
            imgName = "logos/" & Path.GetFileName(opf.FileName)
        End If
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim cDialog As New ColorDialog()
        cDialog.Color = Panel6.BackColor ' initial selection is current color.

        If (cDialog.ShowDialog() = DialogResult.OK) Then
            Panel6.BackColor = cDialog.Color ' update with user selected color.
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        conn2.Close()
        conn2.Open()
        If CheckBox2.Checked = True Then
            sql2 = "UPDATE `infos` SET `doubletickets`= 1 WHERE id = 1 "
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()
        Else
            sql2 = "UPDATE `infos` SET `doubletickets`= 0 WHERE id = 1 "
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.ExecuteNonQuery()
        End If
        conn2.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
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
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Dim inputText As String = TextBox3.Text

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
            RemoveHandler TextBox3.TextChanged, AddressOf TextBox3_TextChanged

            ' Mettez à jour le texte dans le TextBox
            TextBox3.Text = modifiedText

            ' Replacez le curseur à la position correcte après la modification
            TextBox3.SelectionStart = TextBox3.Text.Length

            ' Réactivez le gestionnaire d'événements TextChanged
            AddHandler TextBox3.TextChanged, AddressOf TextBox3_TextChanged
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        Dim inputText As String = TextBox5.Text

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
            RemoveHandler TextBox5.TextChanged, AddressOf TextBox5_TextChanged

            ' Mettez à jour le texte dans le TextBox
            TextBox5.Text = modifiedText

            ' Replacez le curseur à la position correcte après la modification
            TextBox5.SelectionStart = TextBox5.Text.Length

            ' Réactivez le gestionnaire d'événements TextChanged
            AddHandler TextBox5.TextChanged, AddressOf TextBox5_TextChanged
        End If
    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        Dim inputText As String = TextBox11.Text

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
            RemoveHandler TextBox11.TextChanged, AddressOf TextBox11_TextChanged

            ' Mettez à jour le texte dans le TextBox
            TextBox11.Text = modifiedText

            ' Replacez le curseur à la position correcte après la modification
            TextBox11.SelectionStart = TextBox11.Text.Length

            ' Réactivez le gestionnaire d'événements TextChanged
            AddHandler TextBox11.TextChanged, AddressOf TextBox11_TextChanged
        End If
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Panel8.Visible = True
        Panel41.Visible = False
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Panel8.Visible = False
        Panel41.Visible = False
    End Sub

    Private Sub ComboBox16_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox16.SelectedIndexChanged
        adpt = New MySqlDataAdapter("select * from droits where role = '" & ComboBox16.Text & "'", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        DataGridView1.Rows.Clear()

        If table.Rows.Count <> 0 Then
            For i = 0 To table.Rows.Count - 1
                If table.Rows(i).Item(3) = "Autorise" Then
                    DataGridView1.Rows.Add(table.Rows(i).Item(2), True, table.Rows(i).Item(0))
                Else
                    DataGridView1.Rows.Add(table.Rows(i).Item(2), False, table.Rows(i).Item(0))
                End If

            Next
        End If

    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        conn2.Close()
        conn2.Open()
        sql2 = "UPDATE `parameters` SET `stock_fac`='" & ComboBox8.Text & "',`stock_negatif`='" & ComboBox6.Text & "',`balance`='" & ComboBox9.Text & "',`ticket`='" & ComboBox5.Text & "',`ice_fac`='" & ComboBox7.Text & "',`sauvegarde`='" & ComboBox10.Text & "' "
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.ExecuteNonQuery()
        MsgBox("Informations modifiées !")

        Dim Rep As Integer
        Rep = MsgBox("Merci de redémarrer le système pour appliquer les nouvelles paramètres modifiés !" & vbCrLf & "Redémarrer maintenant ?", vbYesNo)
        If Rep = vbYes Then
            System.Windows.Forms.Application.Restart()

        End If


        conn2.Close()

    End Sub

    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs)

    End Sub



    Dim cmd2, cmd3 As MySqlCommand

    Private Sub DateTimePicker5_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker5.ValueChanged

    End Sub

    Private Sub Label42_Click(sender As Object, e As EventArgs) Handles Label42.Click

    End Sub

    Private Sub DateTimePicker4_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker4.ValueChanged

    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs) Handles IconButton7.Click
        Panel41.Visible = True
        DataGridView3.Rows.Clear()
    End Sub

    Private Sub IconButton58_Click(sender As Object, e As EventArgs) Handles IconButton58.Click
        DataGridView3.Rows.Clear()

        adpt = New MySqlDataAdapter("SELECT * FROM traces WHERE date BETWEEN @datedebut AND @datefin order by id desc", conn2)

        adpt.SelectCommand.Parameters.Clear()
        adpt.SelectCommand.Parameters.AddWithValue("@datedebut", DateTimePicker5.Value.Date)
        adpt.SelectCommand.Parameters.AddWithValue("@datefin", DateTimePicker4.Value.Date.AddDays(1))

        Dim tabletick As New DataTable
        adpt.Fill(tabletick)
        For i = 0 To tabletick.Rows.Count() - 1
            DataGridView3.Rows.Add(tabletick.Rows(i).Item(1), tabletick.Rows(i).Item(2), Convert.ToDateTime(tabletick.Rows(i).Item(3)).ToString("dd/MM/yyyy HH:mm:ss"))
        Next
    End Sub

    Private Sub Label43_Click(sender As Object, e As EventArgs) Handles Label43.Click

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Dim pathe As String = imgName
        conn2.Close()
        conn2.Open()
        sql2 = "UPDATE `infos` SET `adresse`='" + TextBox1.Text + "',`phone`='" + TextBox2.Text + "',`email`='" + TextBox3.Text + "',`ice`='" + TextBox4.Text + "',`ife`='" + TextBox7.Text + "', `msg`='" + TextBox5.Text + "',`pt`='" + TextBox10.Text + "',`rc`='" + TextBox9.Text + "',`cb`='" + TextBox8.Text + "', `fax`='" + TextBox6.Text + "', `logo`='" + pathe + "', `ticket`='" + ComboBox2.Text.ToString().Replace("\", "/") + "', `printer`='" + ComboBox1.Text.ToString().Replace("\", "/") + "', `printertwo`='" + ComboBox3.Text.ToString().Replace("\", "/") + "', `color`= '" & ColorTranslator.ToHtml(Panel6.BackColor).ToString & "', `businessname`= '" & TextBox11.Text & "', `gifts`= '" & TextBox12.Text & "', `fidele`= '" & TextBox13.Text & "' WHERE id = 1 "
        cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.ExecuteNonQuery()


        ' Ouvrir une instance modifiable de la configuration
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

        ' Vérifier si la clé existe dans le fichier "App.config"
        ' Modifier la valeur de la clé "NomDeLaCle1" dans App.config
        config.AppSettings.Settings("printerticket").Value = ComboBox2.Text
        config.AppSettings.Settings("printera5").Value = ComboBox3.Text
        config.AppSettings.Settings("printera4").Value = ComboBox1.Text
        config.AppSettings.Settings("printerbc").Value = ComboBox4.Text
        config.AppSettings.Settings("logo").Value = pathe

        ' Sauvegarder les modifications dans App.config
        config.Save(ConfigurationSaveMode.Modified)


        ' Recharger la configuration pour refléter les modifications
        ConfigurationManager.RefreshSection("appSettings")
        Dim appPath As String = Application.StartupPath()

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = SaveDirectory & imgName
        If File.Exists(SavePath) Then

        Else
            If PictureBox3.Image Is Nothing Then
            Else
                PictureBox3.Image.Save(SavePath)

            End If
        End If

        MsgBox("Informations modifiées !")

        Dim Rep As Integer
        Rep = MsgBox("Merci de redémarrer le système pour appliquer les nouvelles paramètres modifiés !" & vbCrLf & "Redémarrer maintenant ?", vbYesNo)
        If Rep = vbYes Then
            System.Windows.Forms.Application.Restart()

        End If


        conn2.Close()
    End Sub

    <Obsolete>
    Private Sub infos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        Panel1.BackColor = System.Drawing.ColorTranslator.FromHtml(table.Rows(0).Item(16))
        Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(table.Rows(0).Item(16))
        Panel3.BackColor = System.Drawing.ColorTranslator.FromHtml(table.Rows(0).Item(16))
        Panel4.BackColor = System.Drawing.ColorTranslator.FromHtml(table.Rows(0).Item(16))

        TextBox1.Text = table.Rows(0).Item(1)
        TextBox2.Text = table.Rows(0).Item(2)
        TextBox3.Text = table.Rows(0).Item(3)
        TextBox4.Text = table.Rows(0).Item(4)
        TextBox7.Text = table.Rows(0).Item(5)
        TextBox5.Text = table.Rows(0).Item(6)
        TextBox10.Text = table.Rows(0).Item(7)
        TextBox9.Text = table.Rows(0).Item(8)
        TextBox8.Text = table.Rows(0).Item(9)
        TextBox6.Text = table.Rows(0).Item(10)
        TextBox11.Text = table.Rows(0).Item(23)
        TextBox12.Text = table.Rows(0).Item(24)
        TextBox13.Text = table.Rows(0).Item(25)


        Panel6.BackColor = System.Drawing.ColorTranslator.FromHtml(table.Rows(0).Item(16))
        imgName = System.Configuration.ConfigurationSettings.AppSettings("logo")

        Dim SaveDirectory As String = Application.StartupPath & "\"
        Dim SavePath As String = SaveDirectory & imgName

        'If System.IO.File.Exists(SavePath) Then
        '    PictureBox3.Image = Image.FromFile(SavePath)
        'End If
        Dim pkInstalledPrinters As String

        ' Find all printers installed
        For Each pkInstalledPrinters In
        PrinterSettings.InstalledPrinters
            ComboBox2.Items.Add(pkInstalledPrinters)
            ComboBox1.Items.Add(pkInstalledPrinters)
            ComboBox3.Items.Add(pkInstalledPrinters)
            ComboBox4.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        Dim printerticket As String = System.Configuration.ConfigurationSettings.AppSettings("printerticket")
        Dim printera5 As String = System.Configuration.ConfigurationSettings.AppSettings("printera5")
        Dim printera4 As String = System.Configuration.ConfigurationSettings.AppSettings("printera4")
        Dim printerbc As String = System.Configuration.ConfigurationSettings.AppSettings("printerbc")


        ComboBox2.Text = printerticket

        ComboBox1.Text = printera4

        ComboBox3.Text = printera5
        ComboBox4.Text = printerbc


        adpt = New MySqlDataAdapter("select * from parameters", conn2)
        Dim tableparam As New DataTable
        adpt.Fill(tableparam)

        ComboBox5.Text = tableparam.Rows(0).Item(4)
        ComboBox6.Text = tableparam.Rows(0).Item(2)
        ComboBox7.Text = tableparam.Rows(0).Item(5)
        ComboBox8.Text = tableparam.Rows(0).Item(1)
        ComboBox9.Text = tableparam.Rows(0).Item(3)
        ComboBox10.Text = tableparam.Rows(0).Item(6)



    End Sub

    Private Sub DataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing

    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        If e.ColumnIndex = 1 Then
            Dim auto As String
            conn2.Close()
        conn2.Open()
            If DataGridView1.Rows(e.RowIndex).Cells(1).Value = True Then
                auto = "Non Autorise"
            Else
                auto = "Autorise"
            End If
            sql2 = "UPDATE `droits` SET `aut`='" & auto & "' WHERE id = '" & DataGridView1.Rows(e.RowIndex).Cells(2).Value & "'"
            cmd2 = New MySqlCommand(sql2, conn2)
        cmd2.ExecuteNonQuery()
            conn2.Close()

        End If
    End Sub
End Class