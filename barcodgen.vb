Imports System.Drawing.Imaging
Imports IDAutomation.Windows.Forms.LinearBarCode
Imports System.Drawing.Printing
Imports System.Configuration
Imports GenCode128

Public Class barcodgen
    Dim pd_PrintDialog As New PrintDialog
    Dim PrintDoc As Printing.PrintDocument = New Printing.PrintDocument()
    Private WithEvents pdPrint As PrintDocument
    Private PrintDocType As String = "Barcode"
    Private StrPrinterName As String = "Microsoft XPS Document Writer"
    Dim pbImage2 As New PictureBox

    Private Sub barcodgen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Dim h, w, h2, w2 As Integer

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        pd_PrintDialog.UseEXDialog = True
        pd_PrintDialog.AllowPrintToFile = False
        'Specifies the number of copies to print.
        pd_PrintDialog.PrinterSettings.Copies = 1


        If pd_PrintDialog.ShowDialog = Windows.Forms.DialogResult.OK Then

            PrintDoc.PrinterSettings = pd_PrintDialog.PrinterSettings



            AddHandler PrintDoc.PrintPage, AddressOf PrintDocHandler
            'Print
            PrintDoc.Print()

        ElseIf pd_PrintDialog.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Exit Sub

        End If
    End Sub

    Private Sub PrintDocHandler(ByVal sender As Object, ByVal ev As Printing.PrintPageEventArgs)
        Panel1.Width = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg").Width
        Panel1.Height = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg").Height + Label6.Height
        Panel1.BackColor = Color.White
        Using bmp = New Bitmap(Panel1.Width, Panel1.Height)
            Panel1.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))
            Panel1.BackgroundImage = bmp
            bmp.Save("D:\Logiciel\image.png")
        End Using
        Dim id As String = "22137471"
        Dim folder As String = "D:\Logiciel\image.png"
        Dim filename As String = System.IO.Path.Combine(folder)
        Panel1.BackgroundImage = Image.FromFile(filename)
        ev.Graphics.DrawImage(Panel1.BackgroundImage, nud_MarginH.Value, nud_MarginW.Value, nud_MarginWBrcode.Value, nud_MarginHBrcode.Value)
    End Sub

    Private Sub pdPrint_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdPrint.PrintPage

        Dim rect As New Rectangle(0, 10, 280, 85)
        Dim sf As New StringFormat
        sf.LineAlignment = StringAlignment.Center
        Dim printFont10_Normal As New Font("Calibri", 13, FontStyle.Bold, GraphicsUnit.Point)
        rect = New Rectangle(0, 10, 280, 15)

        e.Graphics.DrawString("----------IDAutomation---------------", printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)

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
        e.Graphics.DrawString(txtBarcode.Text.ToString(), printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)

        rect = New Rectangle(0, 215, 280, 15)
        e.Graphics.DrawString("------------------------------------------------", printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        'ID Automation
        'Free only with the Code39 and Code39Ext
        Dim NewBarcode As IDAutomation.Windows.Forms.LinearBarCode.Barcode = New Barcode()
        NewBarcode.DataToEncode = txtBarcode.Text.ToString() 'Input of textbox to generate barcode 
        NewBarcode.SymbologyID = Symbologies.Code39
        NewBarcode.Code128Set = Code128CharacterSets.A
        NewBarcode.RotationAngle = RotationAngles.Zero_Degrees
        NewBarcode.RefreshImage()
        NewBarcode.Resolution = Resolutions.Screen
        NewBarcode.ResolutionCustomDPI = 96
        NewBarcode.RefreshImage()

        NewBarcode.SaveImageAs("SavedBarcode.Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg)
        NewBarcode.Resolution = Resolutions.Printer
        imgIDAutomation.Image = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg")

        'Barcode using the GenCode128
        Dim myimg As Image = Code128Rendering.MakeBarcodeImage(txtBarcode.Text.ToString(), Integer.Parse(txtWeight.Text.ToString()), False)
        imgGenCode.Image = myimg
        pbImage2.Image = myimg
        'Barcode using the GenCode128

        Try
            Panel1.Width = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg").Width
            Panel1.Height = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg").Height + Label6.Height
            Panel1.BackColor = Color.White
            Using bmp = New Bitmap(Panel1.Width, Panel1.Height)
                Panel1.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))
                bmp.Save("D:\Logiciel\image.png")
            End Using
            MessageBox.Show("Image saved successfully.")
        Catch
            MessageBox.Show("Error.....")
        End Try



    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
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

End Class
