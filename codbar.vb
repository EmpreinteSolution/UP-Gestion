Imports System.Drawing.Printing
Imports BarcodeLib
Imports Zen.Barcode
Imports ZXing
Imports ZXing.Common


Public Class codbar
    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

        ' Récupérer le texte du TextBox
        Dim texte As String = TextBox3.Text


        If texte <> "" Then
            ' Générer le code à barres en utilisant le texte

            ' Ajuster la taille du PictureBox
            Dim writer As New BarcodeWriter()

            ' Spécifier les paramètres du code à barres
            Dim options As New EncodingOptions()
            options.Height = 50
            options.Width = 300
            options.PureBarcode = True

            writer.Format = BarcodeFormat.CODE_128
            writer.Options = options

            ' Générer le code à barres
            Dim imageCodeBarre As Bitmap = writer.Write(texte)
            ' Créer une nouvelle image avec le texte du produit
            Dim imageAvecTexte As New Bitmap(PictureBox1.Width, PictureBox1.Height + 40)
            Using g As Graphics = Graphics.FromImage(imageAvecTexte)
                ' Dessiner le code à barres

                ' Ajouter le nom du produit en haut du code à barres
                Dim nomProduit As String = "Tide bimo sanida kilo sfer"
                Dim fontNomProduit As New Font("Arial", 12, FontStyle.Regular)
                Dim brushNomProduit As New SolidBrush(Color.Black)
                Dim nomProduitSize As SizeF = g.MeasureString(nomProduit, fontNomProduit)
                Dim nomProduitPosition As New PointF((imageAvecTexte.Width - nomProduitSize.Width) / 2, 5)

                Dim codeBarrePosition As New Point((imageAvecTexte.Width - imageCodeBarre.Width) / 2, 31)
                g.DrawImage(imageCodeBarre, codeBarrePosition)

                g.DrawString(nomProduit, fontNomProduit, brushNomProduit, nomProduitPosition)

                ' Ajouter le prix du produit en bas du code à barres
                Dim prixProduit As String = "150 DHs"
                Dim fontPrixProduit As New Font("Arial", 18, FontStyle.Regular)
                Dim brushPrixProduit As New SolidBrush(Color.Black)
                Dim prixProduitSize As SizeF = g.MeasureString(prixProduit, fontPrixProduit)
                Dim prixProduitPosition As New PointF((imageAvecTexte.Width - prixProduitSize.Width) / 2, imageCodeBarre.Height + 41)
                g.DrawString(prixProduit, fontPrixProduit, brushPrixProduit, prixProduitPosition)
            End Using

            ' Afficher l'image avec le texte dans le PictureBox
            PictureBox1.Image = imageAvecTexte

        End If

    End Sub

    Private Sub ImprimerTicket()
        ' Créer un objet PrintDocument
        Dim printDoc As New PrintDocument()

        ' Définir les marges de la page
        Dim marges As New Margins(0, 0, 0, 0)
        printDoc.DefaultPageSettings.Margins = marges

        ' Gérer l'événement PrintPage pour effectuer le rendu du contenu du PictureBox sur la page imprimée
        AddHandler printDoc.PrintPage, AddressOf PrintDoc_PrintPage

        ' Définir la taille de la page en fonction des dimensions du ticket
        Dim largeurTicket As Integer = Convert.ToInt32(76 * 100 / 25.4) ' Conversion de mm en dpi
        Dim hauteurTicket As Integer = Convert.ToInt32(25 * 100 / 25.4) ' Conversion de mm en dpi
        printDoc.DefaultPageSettings.PaperSize = New PaperSize("Custom", largeurTicket, hauteurTicket)

        ' Lancer le dialogue d'impression
        Dim printDialog As New PrintDialog()
        printDialog.Document = printDoc
        If printDialog.ShowDialog() = DialogResult.OK Then
            printDoc.Print()
        End If
    End Sub

    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs)
        ' Obtenir l'image du PictureBox à imprimer
        Dim image As Image = PictureBox1.Image

        ' Calculer les coordonnées pour centrer l'image sur la page
        Dim largeurPage As Integer = e.PageBounds.Width
        Dim hauteurPage As Integer = e.PageBounds.Height
        Dim positionX As Integer = (largeurPage - image.Width) \ 2
        Dim positionY As Integer = (hauteurPage - image.Height) \ 2

        ' Dessiner l'image sur la page
        e.Graphics.DrawImage(image, positionX, positionY)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ImprimerTicket()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub codbar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
