Imports System.Globalization
Imports System.Threading
Imports MySql.Data.MySqlClient
Imports System.Configuration
Imports System.Net.NetworkInformation
Imports System.IO
Imports System.Collections
Imports System.Management
Imports System.Security.Cryptography
Imports System.Net.Sockets
Imports System.Text
Imports System.Drawing.Printing
Imports WindowsInput
Imports My_animals.numconv
Imports System.Diagnostics
Imports System.Net

Public Class Form1


    Private enc As System.Text.UTF8Encoding
    Private encryptor As ICryptoTransform
    Private decryptor As ICryptoTransform
    Public Property ConfigurationManager As Object
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then

            Dim adpt As New MySqlDataAdapter(" SELECT * FROM `users` WHERE badgeID='" + TextBox1.Text + "' ", conn2)
            Dim data As New DataTable
            adpt.Fill(data)

            If data.Rows.Count > 0 Then

                If data.Rows(0).Item(5) = "Caissier" Then

                    role = "Caissier"
                    dashboard.Show()
                    user = data.Rows(0).Item(1)
                    dashboard.Label2.Text = user

                    Me.Hide()

                End If

                If data.Rows(0).Item(5) = "Magazinier" Then

                    role = "Caissier"
                    dashboard.Caisserol = data.Rows(0).Item(4)
                    dashboard.Show()
                    user = data.Rows(0).Item(1)
                    dashboard.Label2.Text = user
                    dashboard.IconButton45.Visible = False
                    dashboard.IconButton65.Visible = True
                    dashboard.IconButton69.Visible = False
                    dashboard.IconButton54.Visible = False
                    dashboard.IconButton56.Visible = False
                    dashboard.IconButton38.Visible = False
                    dashboard.IconButton55.Visible = False
                    dashboard.IconButton36.Visible = False
                    dashboard.IconButton39.Visible = False
                    dashboard.IconButton18.Visible = False
                    dashboard.IconButton23.Visible = False


                    Me.Hide()

                End If

                If data.Rows(0).Item(5) = "Gerant" Then
                    role = "Gerant"
                    Home.Show()
                    user = data.Rows(0).Item(1)
                    Home.Label2.Text = user
                    Me.Hide()
                End If

                If data.Rows(0).Item(5) = "Administration" Then

                    role = "Administration"
                    Home.Show()
                    Home.Panel3.Visible = False
                    Home.Panel4.Visible = False
                    Home.Panel7.Visible = False
                    Home.Panel6.Visible = False
                    Home.Panel8.Visible = False
                    Home.IconButton37.Visible = False
                    user = data.Rows(0).Item(1)
                    Home.Label2.Text = user

                    Me.Hide()

                End If
            Else
                MsgBox("Code incorrecte !")
            End If
        End If
        If e.KeyCode = Keys.F3 Then
            reset.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim adpt As New MySqlDataAdapter(" SELECT * FROM `users` WHERE badgeID='" + TextBox1.Text + "' ", conn2)
        Dim data As New DataTable
        adpt.Fill(data)

        If data.Rows.Count > 0 Then
            If data.Rows(0).Item(5) = "Caissier" Then

                role = "Caissier"
                dashboard.Caisserol = data.Rows(0).Item(4)
                dashboard.Show()
                user = data.Rows(0).Item(1)
                dashboard.Label2.Text = user

                Me.Hide()

            End If


            If data.Rows(0).Item(5) = "Magazinier" Then

                role = "Caissier"
                dashboard.Caisserol = data.Rows(0).Item(4)
                dashboard.Show()
                user = data.Rows(0).Item(1)
                dashboard.Label2.Text = user
                dashboard.IconButton45.Visible = False
                dashboard.IconButton65.Visible = True
                dashboard.IconButton69.Visible = False
                dashboard.IconButton54.Visible = False
                dashboard.IconButton56.Visible = False
                dashboard.IconButton38.Visible = False
                dashboard.IconButton55.Visible = False
                dashboard.IconButton36.Visible = False
                dashboard.IconButton39.Visible = False
                dashboard.IconButton18.Visible = False
                dashboard.IconButton23.Visible = False



                Me.Hide()

            End If


            If data.Rows(0).Item(5) = "Gerant" Then
                role = "Gerant"
                Home.Show()
                user = data.Rows(0).Item(1)
                Home.Label2.Text = user
                Me.Hide()
            End If

            If data.Rows(0).Item(5) = "Administration" Then

                role = "Administration"
                Home.Show()
                Home.Panel3.Visible = False
                Home.Panel4.Visible = False
                Home.Panel7.Visible = False
                Home.Panel6.Visible = False
                Home.Panel8.Visible = False
                Home.IconButton37.Visible = False
                user = data.Rows(0).Item(1)
                Home.Label2.Text = user

                Me.Hide()

            End If

            If data.Rows(0).Item(5) = "Comptabilité" Then

                role = "Comptabilité"
                banques.Show()
                banques.IconButton17.Enabled = False
                banques.IconButton14.Enabled = False
                banques.IconButton11.Enabled = False
                banques.IconButton6.Enabled = False
                banques.IconButton9.Enabled = False
                banques.IconButton8.Enabled = False
                banques.IconButton12.Enabled = False
                banques.IconButton15.Enabled = False
                banques.IconButton13.Enabled = False
                banques.IconButton23.Enabled = False
                user = data.Rows(0).Item(1)
                banques.Label2.Text = user

                Me.Hide()

            End If

        Else
            MsgBox("Code introuvable !")
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = TextBox1.Text + Button2.Text
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = TextBox1.Text + Button3.Text
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.Text = TextBox1.Text + Button4.Text
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Text = TextBox1.Text + Button5.Text
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox1.Text = TextBox1.Text + Button6.Text
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        TextBox1.Text = TextBox1.Text + Button11.Text
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TextBox1.Text = TextBox1.Text + Button10.Text
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TextBox1.Text = TextBox1.Text + Button9.Text
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        TextBox1.Text = TextBox1.Text + Button8.Text
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TextBox1.Text = TextBox1.Text + Button7.Text
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        TextBox1.Text = ""
    End Sub
    Dim sql2 As String
    Dim cmd2 As MySqlCommand
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter

    <Obsolete>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim server = System.Configuration.ConfigurationSettings.AppSettings("server")
        Dim database = System.Configuration.ConfigurationSettings.AppSettings("database")
        Dim user = System.Configuration.ConfigurationSettings.AppSettings("userid")
        Dim password = System.Configuration.ConfigurationSettings.AppSettings("password")

        Dim appPath As String = Application.StartupPath()

        Dim backupPath As DirectoryInfo = New DirectoryInfo(appPath & "\backups")

        Dim dateTimeString As String = DateTime.Now.ToString("yyyyMM")

        If backupPath.Exists Then
        Else
            backupPath.Create()
        End If

        ' Set the filename for the backup file with date and time
        Dim fileName = $"Sauvegarde_automatique_{dateTimeString}.sql"

        ' Build the mysqldump command
        Dim command As String

        If password <> "" Then
            command = $"C:\xampp\mysql\bin\mysqldump -h {server} -u {user} -p{password} {database} > ""{backupPath}\{fileName}"""
        Else
            command = $"C:\xampp\mysql\bin\mysqldump -h {server} -u {user} {database} > ""{backupPath}\{fileName}"""
        End If

        ' Start the process
        StartProcess(command)
        'adpt = New MySqlDataAdapter("SELECT Code, Article FROM article", conn2)
        'Dim tablecal2 As New DataTable
        'adpt.Fill(tablecal2)

        'For i = 0 To tablecal2.Rows.Count() - 1
        '    Using conn2
        '        conn2.Open()

        '        Dim TVA As String = tablecal2.Rows(i).Item(1)
        '        Dim clientToUpdate As String = tablecal2.Rows(i).Item(0).ToString()

        '        Dim sql2 As String = "UPDATE facturedetails SET des = @creditToAdd WHERE ref = @clientToUpdate"
        '        Using cmd2 As New MySqlCommand(sql2, conn2)
        '            cmd2.Parameters.AddWithValue("@creditToAdd", TVA.ToString.Replace(".", ","))
        '            cmd2.Parameters.AddWithValue("@clientToUpdate", clientToUpdate)
        '            cmd2.ExecuteNonQuery()
        '        End Using

        '        conn2.Close()
        '    End Using
        'Next

        'MsgBox("termine")
        BackgroundWorker1.RunWorkerAsync()


        Dim crackfolder As DirectoryInfo = New DirectoryInfo(appPath & "\zh")


        'Dim versionActuelle As String = My.Computer.FileSystem.ReadAllText(appPath & "/Vrs.txt")
        'Dim urlVersionDrive As String = "https://drive.google.com/file/d/1NIhXed4yP8HGKvbONnhDEehV34qm9znM/view?usp=sharing"
        'Dim versionDrive As String = New System.Net.WebClient().DownloadString(urlVersionDrive)
        'MsgBox(versionDrive)
        'MsgBox(versionDrive, MsgBoxStyle.Information, "Contenu du fichier texte")

        '' Comparer les versions
        'If versionActuelle < versionDrive Then
        '    Dim resultat As DialogResult = MessageBox.Show("Une mise à jour est disponible. Voulez-vous la télécharger et l'installer ?", "Mise à jour disponible", MessageBoxButtons.YesNo)
        '    If resultat = DialogResult.Yes Then
        '        Dim urlMiseAJour As String = "https://drive.google.com/file/d/17IfG976M0D-1YOcE7_Vy91ZCR2QBr9w5/view?usp=sharing"
        '        Dim cheminLocal As String = appPath & "\UP Gestion GLOBAL2.exe"

        '        Dim client As New System.Net.WebClient()
        '        client.DownloadFile(urlMiseAJour, cheminLocal)
        '    End If
        'End If
        If crackfolder.Exists Then

            Dim line As String
            Dim FilePath As String = crackfolder.ToString & "/fr.txt"
            ' Create new StreamReader instance with Using block.
            Using reader As StreamReader = New StreamReader(FilePath)
                ' Read one line from file
                line = reader.ReadLine
            End Using

            Dim DriveSerial As Integer
            'Create a FileSystemObject object
            Dim fso As Object = CreateObject("Scripting.FileSystemObject")
            Dim Drv As Object = fso.GetDrive(fso.GetDriveName(Application.StartupPath))
            With Drv
                If .IsReady Then
                    DriveSerial = .SerialNumber
                Else    '"Drive Not Ready!"
                    DriveSerial = -1
                End If
            End With

            Dim crack As String
            Using hasher As MD5 = MD5.Create()    ' create hash object

                ' Convert to byte array and get hash
                Dim dbytes As Byte() =
                 hasher.ComputeHash(Encoding.UTF8.GetBytes(DriveSerial.ToString("X2")))

                ' sb to create string from bytes
                Dim sBuilder As New StringBuilder()

                ' convert byte data to hex string
                For n As Integer = 0 To dbytes.Length - 1
                    sBuilder.Append(dbytes(n).ToString("X2"))
                Next n
                crack = sBuilder.ToString

            End Using

            If line = crack Then
            Else
                serial.Show()
                Me.Close()
            End If

        Else
            crackfolder.Create()
            Dim DriveSerial As Integer
            'Create a FileSystemObject object
            Dim fso As Object = CreateObject("Scripting.FileSystemObject")
            Dim Drv As Object = fso.GetDrive(fso.GetDriveName(Application.StartupPath))
            With Drv
                If .IsReady Then
                    DriveSerial = .SerialNumber
                Else    '"Drive Not Ready!"
                    DriveSerial = -1
                End If
            End With

            Dim crack As String
            Using hasher As MD5 = MD5.Create()    ' create hash object

                ' Convert to byte array and get hash
                Dim dbytes As Byte() =
                 hasher.ComputeHash(Encoding.UTF8.GetBytes(DriveSerial.ToString("X2")))

                ' sb to create string from bytes
                Dim sBuilder As New StringBuilder()

                ' convert byte data to hex string
                For n As Integer = 0 To dbytes.Length - 1
                    sBuilder.Append(dbytes(n).ToString("X2"))
                Next n
                crack = sBuilder.ToString

            End Using
            Dim path As String = crackfolder.ToString & "/fr.txt"

            ' Create or overwrite the file.
            Dim fs As FileStream = File.Create(path)
            Dim fs1 As FileStream = File.Create(crackfolder.ToString & "/en.txt")
            Dim fs2 As FileStream = File.Create(crackfolder.ToString & "/zh.txt")
            Dim fs3 As FileStream = File.Create(crackfolder.ToString & "/tg.txt")

            ' Add text to the file.

            Dim info As Byte() = New UTF8Encoding(True).GetBytes(crack)
            fs.Write(info, 0, info.Length)
            fs.Close()

        End If

        imgName = System.Configuration.ConfigurationSettings.AppSettings("logo")

        Dim adpt2 As MySqlDataAdapter = New MySqlDataAdapter("select * from infos", conn2)
        Dim table As New DataTable
        adpt2.Fill(table)
        imgName = table.Rows(0).Item(11)

        Dim SaveDirectory As String = appPath & "\"
        Dim SavePath As String = SaveDirectory & imgName

        If System.IO.File.Exists(SavePath) Then
            PictureBox2.Image = Image.FromFile(SavePath)
        End If

        Dim di As DirectoryInfo = New DirectoryInfo("c:\pospictures")
        If di.Exists Then
        Else
            di.Create()
        End If

        Me.KeyPreview = True
        'Synchronisation.ShowDialog()
        'Dim adpt As New MySqlDataAdapter(" SELECT mmc FROM `appconfig` WHERE id='1' ", conn2)
        'Dim data As New DataTable
        'adpt.Fill(data)
        'If data.Rows(0).Item(0) = 0 Then
        '    serial.Show()
        '    Me.Close()
        'End If

        'If DateTime.Now.ToString("yyyy-MM-dd") = "2021-07-25" Then
        '    conn2.Open()
        '    sql2 = "UPDATE `appconfig` SET `mmc`='0' WHERE id = '1' "
        '    cmd2 = New MySqlCommand(sql2, conn2)
        '    cmd2.ExecuteNonQuery()
        '    conn2.Close()

        '    serial.Show()
        '    Me.Close()
        'End If



        adpt = New MySqlDataAdapter("select * from infos", conn2)
        Dim tableimg As New DataTable
        adpt.Fill(tableimg)
        Button1.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))
        Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(tableimg.Rows(0).Item(16))

        Dim adpt3 As MySqlDataAdapter = New MySqlDataAdapter("select client from clients where client = 'comptoir'", conn2)
        Dim table2 As New DataTable
        adpt3.Fill(table2)

        If table2.Rows.Count() = 0 Then
            conn2.Open()

            Dim sql2 As String
            Dim cmd2 As MySqlCommand
            sql2 = "INSERT INTO clients ( `client`) VALUES (@v1)"
            cmd2 = New MySqlCommand(sql2, conn2)
            cmd2.Parameters.AddWithValue("@v1", "comptoir")
            cmd2.ExecuteNonQuery()

            conn2.Close()
        End If


    End Sub
    Private startTime As DateTime
    Private process As Process


    Private Sub StartProcess(command As String)
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
    End Sub
    Private Function DownloadTextFile(link As String) As String
        Dim content As String = Nothing

        Try
            Using client As New WebClient()
                ' Téléchargez le contenu du fichier
                content = client.DownloadString(link)
            End Using
        Catch ex As Exception
            ' Gérez les erreurs ici (par exemple, affichez un message d'erreur)
            MessageBox.Show("Une erreur s'est produite lors du téléchargement du fichier.")
        End Try

        Return content
    End Function
    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        End
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        TextBox1.Text = TextBox1.Text + Button14.Text
    End Sub
    Dim link As New Net.WebClient
    Dim currentVersion As Integer = 1

    Public UpdateError As Boolean = False
    Dim UpdateErrorText As String
    Dim allowWithoutUpdate As Boolean = True
    Dim NewVersion As Boolean = False
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            If My.Computer.Network.Ping("www.google.com") Then

            Else
                MsgBox("Vous n'etes pas connecté à internet !")
            End If
        Catch ex As Exception
            'السيرفر غالبا متوقف عن العمل
            MessageBox.Show("Vous n'etes pas connecté à internet !")
            UpdateError = True
            UpdateErrorText = "The Server is down"
        End Try
    End Sub


    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click

        adpt3 = New MySqlDataAdapter("SELECT `Code`,`PA_HT`,`PA_TTC` FROM `article`", conn)
        Dim products As New DataTable
        adpt3.Fill(products)
        conn2.Open()

        For K = 0 To products.Rows.Count - 1

            Dim sql9 As String
            Dim cmd9 As MySqlCommand
            'sql9 = "UPDATE `article` SET `Stock`= REPLACE(`Stock`,',','.') - @v1 WHERE `Code` = @v2"
            sql9 = "UPDATE `fac_invent` SET pa_ttc = @v3 where code_art = @v1"
            cmd9 = New MySqlCommand(sql9, conn2)
            cmd9.Parameters.Clear()
            cmd9.Parameters.AddWithValue("@v1", products.Rows(K).Item(0))
            cmd9.Parameters.AddWithValue("@v3", products.Rows(K).Item(2).ToString.Replace(",", "."))
            cmd9.ExecuteNonQuery()

        Next
        conn2.Close()


        'adpt3 = New MySqlDataAdapter("SELECT `OrderID` FROM `orders`", conn)
        'Dim products As New DataTable
        'adpt3.Fill(products)
        'conn2.Open()

        'For K = 0 To products.Rows.Count - 1
        '    Dim ca As Double = 0
        '    Dim marge As Double = 0
        '    adpt3 = New MySqlDataAdapter("SELECT `marge`,`pv`,`Quantity` FROM `orderdetails` where OrderID = '" & products.Rows(K).Item(0) & "'", conn)
        '    Dim productsdet As New DataTable
        '    adpt3.Fill(productsdet)
        '    For j = 0 To productsdet.Rows.Count - 1
        '        ca = ca + (productsdet.Rows(j).Item(1).ToString.Replace(" ", "").Replace(".", ",") * productsdet.Rows(j).Item(2).ToString.Replace(" ", "").Replace(".", ","))
        '        marge = marge + productsdet.Rows(j).Item(0).ToString.Replace(" ", "").Replace(".", ",")
        '    Next
        '    Dim sql9 As String
        '    Dim cmd9 As MySqlCommand
        '    sql9 = "UPDATE `orders` SET `total_ht`= @v1, marge = @v3 WHERE `OrderID` = @v2"
        '    cmd9 = New MySqlCommand(sql9, conn2)
        '    cmd9.Parameters.Clear()
        '    cmd9.Parameters.AddWithValue("@v1", Convert.ToDouble(ca.ToString.Replace(" ", "")).ToString("N2"))
        '    cmd9.Parameters.AddWithValue("@v3", Convert.ToDouble(marge.ToString.Replace(" ", "")).ToString("N2"))
        '    cmd9.Parameters.AddWithValue("@v2", products.Rows(K).Item(0))
        '    cmd9.ExecuteNonQuery()

        'Next
        'conn2.Close()

        'adpt3 = New MySqlDataAdapter("SELECT `Code`,PA_HT,PV_HT FROM `article`", conn)
        'Dim products As New DataTable
        'adpt3.Fill(products)
        'conn2.Open()

        'For K = 0 To products.Rows.Count - 1

        '    Dim marge As Double = 0
        '    marge = marge + (products.Rows(K).Item(2).ToString.Replace(" ", "").Replace(".", ",") - products.Rows(K).Item(1).ToString.Replace(" ", "").Replace(".", ","))

        '    Dim sql9 As String
        '    Dim cmd9 As MySqlCommand
        '    sql9 = "UPDATE `orderdetails` SET `pa`= @v1,pv = @v4, marge = (@v3 * CAST(REPLACE(REPLACE(`Quantity`, ' ', ''), ',', '.') AS DECIMAL(10, 2))) WHERE `ProductID` = @v2"
        '    cmd9 = New MySqlCommand(sql9, conn2)
        '    cmd9.Parameters.Clear()
        '    cmd9.Parameters.AddWithValue("@v1", Convert.ToDouble(products.Rows(K).Item(1)).ToString("N2"))
        '    cmd9.Parameters.AddWithValue("@v3", marge.ToString.Replace(" ", "").Replace(",", "."))
        '    cmd9.Parameters.AddWithValue("@v2", products.Rows(K).Item(0))
        '    cmd9.Parameters.AddWithValue("@v4", Convert.ToDouble(products.Rows(K).Item(2)).ToString("N2"))
        '    cmd9.ExecuteNonQuery()

        'Next
        'conn2.Close()

        MsgBox("termine")
    End Sub



    Private Function GenerateZPLCode(image As Image) As String
        Dim zplCode As String = ""

        If image IsNot Nothing Then
            Dim imageData As Byte() = ConvertImageToByteArray(image)
            Dim base64ImageData As String = Convert.ToBase64String(imageData)
            zplCode = "^XA^FO50,50^GFA,200,200,8," & base64ImageData & "^FS^XZ"
        End If

        Return zplCode
    End Function

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub


    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click

    End Sub
    <Obsolete>
    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        'Dim urlVersionDrive As String = "https://drive.google.com/uc?id=1NIhXed4yP8HGKvbONnhDEehV34qm9znM"
        'Dim versionDrive As String = New System.Net.WebClient().DownloadString(urlVersionDrive)
        'MsgBox(versionDrive)
        'MsgBox(versionDrive, MsgBoxStyle.Information, "Contenu du fichier texte")



        'Set your MySQL database information


    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim SuccessMsg As New success
        SuccessMsg.Label14.Text = "Le lorem ipsum est, en imprimerie, une suite de mots sans signification utilisée à titre provisoire pour calibrer une mise en page,"
        SuccessMsg.ShowDialog()

        Dim yesornoMsg As New yesorno
        yesornoMsg.Label14.Text = "Le lorem ipsum est, en imprimerie, une suite de mots sans signification utilisée à titre provisoire pour calibrer une mise en page,"
        yesornoMsg.ShowDialog()

        Dim erorMsg As New eror
        erorMsg.Label14.Text = "Le lorem ipsum est, en imprimerie, une suite de mots sans signification utilisée à titre provisoire pour calibrer une mise en page,"
        erorMsg.ShowDialog()
    End Sub

    Private Sub PrintWithDialog(zplCode As String)
        Dim printDialog As New PrintDialog()

        If printDialog.ShowDialog() = DialogResult.OK Then
            Dim printerSettings As PrinterSettings = printDialog.PrinterSettings

            Dim printDocument As New PrintDocument()
            printDocument.PrinterSettings = printerSettings

            AddHandler printDocument.PrintPage, Sub(sender As Object, e As PrintPageEventArgs)
                                                    e.Graphics.DrawString(zplCode, New Font("Courier New", 8), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top)
                                                End Sub

            printDocument.Print()
        End If
    End Sub

    Private Function ConvertImageToByteArray(image As Image) As Byte()
        Using ms As New MemoryStream()
            image.Save(ms, Imaging.ImageFormat.Png)
            Return ms.ToArray()
        End Using
    End Function
End Class


