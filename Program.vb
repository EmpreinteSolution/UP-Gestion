Module Program
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        ' Initialise le filtre pour convertir les points en virgules
        Class2.Initialize()

        Application.Run(New Form1()) ' Remplacez Form1 par votre formulaire principal
    End Sub
End Module
