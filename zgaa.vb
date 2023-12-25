Imports System.IO
Imports System.Text
Imports MySql.Data.MySqlClient
Imports System.Drawing.Imaging
Imports IDAutomation.Windows.Forms.LinearBarCode
Imports System.Drawing.Printing
Imports System.Configuration
Imports GenCode128

Public Class zgaa

    Private WithEvents pdPrint As PrintDocument
    Private PrintDocType As String = "Barcode"
    Private StrPrinterName As String = "Microsoft Print to PDF"
    Dim pbImage2 As New PictureBox

    Dim PrintDoc As Printing.PrintDocument = New Printing.PrintDocument()
    Dim pd_PrintDialog As New PrintDialog
    Private Sub zgaa_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Dim adpt As MySqlDataAdapter

    Private Sub pdPrint_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdPrint.PrintPage

        Dim rect As New Rectangle(0, 10, 280, 85)
        Dim sf As New StringFormat
        sf.LineAlignment = StringAlignment.Center
        Dim printFont10_Normal As New Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Point)
        rect = New Rectangle(0, 10, 280, 15)

        e.Graphics.DrawString("----------IDAutomation---------------", printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)

        Dim h, w, h2, w2 As Integer
        Dim pbImage As New PictureBox
        pbImage.Image = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg")
        w = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg").Width
        h = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg").Height
        Dim lPosition As Integer
        lPosition = (280 - w) / 2
        rect = New Rectangle(50, 25, w, h)
        e.Graphics.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        e.Graphics.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        e.Graphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
        e.Graphics.DrawImage(pbImage.Image, rect)
        e.Graphics.DrawRectangle(Pens.White, rect)

        rect = New Rectangle(0, 95, 280, 15)
        e.Graphics.DrawString("------------------------------------------------", printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)


        rect = New Rectangle(0, 105, 280, 15)
        e.Graphics.DrawString("---------------GEN128Barcode----------------------", printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)

        w2 = pbImage2.Width
        h2 = pbImage2.Height
        rect = New Rectangle(50, 130, w2, h2)
        e.Graphics.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        e.Graphics.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        e.Graphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
        e.Graphics.DrawImage(pbImage2.Image, rect)
        e.Graphics.DrawRectangle(Pens.White, rect)

        rect = New Rectangle(70, 185, 280, 15)
        e.Graphics.DrawString(TextBox1.Text.ToString(), printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)

        rect = New Rectangle(0, 215, 280, 15)
        e.Graphics.DrawString("------------------------------------------------", printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        'ID Automation
        'Free only with the Code39 and Code39Ext
        Dim NewBarcode As IDAutomation.Windows.Forms.LinearBarCode.Barcode = New Barcode()
        NewBarcode.DataToEncode = TextBox1.Text.ToString() 'Input of textbox to generate barcode 
        NewBarcode.SymbologyID = Symbologies.Code39
        NewBarcode.Code128Set = Code128CharacterSets.A
        NewBarcode.RotationAngle = RotationAngles.Zero_Degrees
        NewBarcode.RefreshImage()
        NewBarcode.Resolution = Resolutions.Screen
        NewBarcode.ResolutionCustomDPI = 96
        NewBarcode.RefreshImage()
        NewBarcode.SaveImageAs("SavedBarcode.Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg)
        NewBarcode.Resolution = Resolutions.Printer
        PictureBox1.Image = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg")

        'Barcode using the GenCode128
        Dim myimg As Image = Code128Rendering.MakeBarcodeImage(TextBox1.Text.ToString(), Integer.Parse(TextBox2.Text.ToString()), False)
        PictureBox2.Image = myimg
        pbImage2.Image = myimg
        'Barcode using the GenCode128

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Try
            pdPrint = New PrintDocument
            pdPrint.PrinterSettings.PrinterName = StrPrinterName
            pdPrint.PrintController = New StandardPrintController
            If pdPrint.PrinterSettings.IsValid Then
                pdPrint.DocumentName = PrintDocType
                pdPrint.Print()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PrintDocHandler(ByVal sender As Object, ByVal ev As Printing.PrintPageEventArgs)
        ev.Graphics.DrawImage(PicBarCode.BackgroundImage, 10, 14, 300, 100)
    End Sub

    Private Sub butPageSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butPageSetup.Click
        'PageSetup

        PageSetupDialog1.Document = PrintDoc
        PageSetupDialog1.ShowDialog()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Print preview
        If TextBox1.Text = "" Then 'Barcode is empty
            MsgBox("Please make sure you type the text to be converted to a barcode")
            Exit Sub
        Else
            AddHandler PrintDoc.PrintPage, AddressOf PrintDocHandler

            'Specifies the number of copies to print
            pd_PrintDialog.PrinterSettings.Copies = 10



            PrintPreviewDialog1.Document = PrintDoc
            PrintPreviewDialog1.ShowDialog()

        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'Write the product name on the barcode label
        Dim a As Graphics = PicBarCode.CreateGraphics
        a.DrawString(TextBox1.Text, Me.Font, Brushes.Black, 100, 5)

        'Me.PicBarCode.Image = PicBarCode.BackgroundImage



        ' Dim Bmp As New Bitmap(PicBarCode.Image)
        ' Dim Graphics1 As Graphics = Graphics.FromImage(Bmp)
        ' Dim Font1 As Font = New Font("Comic Sans MS", 20, FontStyle.Bold, GraphicsUnit.Pixel)
        ' Dim Brush1 As New SolidBrush(Color.FromArgb(92, 230, 80, 120))
        'Graphics1.DrawString(TextBox1.Text, Me.Font, Brushes.Black, 100, 5)
        'PicBarCode.Image = Bmp

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        End
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        PicBarCode.BackgroundImage = Code128(TextBox1.Text, "A")
    End Sub
End Class