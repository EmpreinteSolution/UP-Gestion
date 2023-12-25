Imports System
Imports System.Collections.Generic
Module numconv
    Private jusqueSeize() As String = {"zéro", "un", "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf", "dix", "onze", "douze", "treize", "quatorze", "quinze", "seize"}

    Private dizaines() As String = {"rien", "dix", "vingt", "trente", "quarante", "cinquante", "soixante", "soixante", "quatre-vingt", "quatre-vingt"}

    Private resultat As List(Of String)

    ''' <summary>
    ''' Méthode d'extension de la classe double écrivant le nombre en lettres
    ''' </summary>
    ''' <param name="Nombre">Nombre à écrire</param>
    ''' <param name="LePays">Pays d'utilisation, pour spécificitées régionnales</param>
    ''' <param name="LaDevise">Devise à utliser</param>
    ''' <returns></returns>
    <System.Runtime.CompilerServices.Extension>
    Public Function ToLettres(ByVal Nombre As Double, Optional ByVal LePays As Pays = Pays.France, Optional ByVal LaDevise As Devise = Devise.Aucune) As String

        resultat = New List(Of String)()

        Select Case Math.Sign(Nombre)
            Case -1
                resultat.Add("moins ")
                Nombre *= -1

            Case 0
                Return jusqueSeize(0)
        End Select

        If Nombre >= 1.0E+16 Then
            Return "Nombre trop grand"
        End If

        Dim partieEntiere As Int64 = CLng(Math.Floor(Nombre))
        Dim partieDecimale As Double = Nombre - partieEntiere

        Dim milliers() As String = {"", "mille", "million", "milliard", "billion", "billiard"}

        If partieEntiere > 0 Then
            Dim troisChiffres As New List(Of Integer)() 'liste qui scinde la partie entière en morceaux de 3 chiffres

            Do While partieEntiere > 0
                troisChiffres.Add(CInt(Math.Floor(partieEntiere Mod 1000)))
                partieEntiere \= 1000
            Loop

            Dim reste As Double = Nombre - partieEntiere

            For i As Integer = troisChiffres.Count - 1 To 0 Step -1
                Dim leNombre As Integer = troisChiffres(i)

                If leNombre > 1 Then 'valeurs de milliers au pluriel
                    resultat.Add(Ecrit3Chiffres(troisChiffres(i), LePays))
                    If i > 1 Then ' mille est invariable et "" ne prend pas de s
                        resultat.Add(milliers(i) & "s")
                    ElseIf i = 1 Then
                        resultat.Add(milliers(i))
                    End If
                ElseIf leNombre = 1 Then
                    If i <> 1 Then resultat.Add("un") 'on dit un million, mais pas un mille
                    resultat.Add(milliers(i))
                End If
                'on ne traite pas le 0, car on ne dit pas X millions zéro mille Y.
            Next i
        Else
            resultat.Add(jusqueSeize(0))
        End If

        If LaDevise = Devise.Aucune Then
            resultat.Add("Dhs et") ' Ajout d'une conjonction avant les décimales

            If partieDecimale > 0 Then
                Dim centimes As Integer = CInt(partieDecimale * 100)
                resultat.Add(Ecrire2Chiffres(centimes, LePays))

                If centimes = 1 Then
                    resultat.Add("centime") ' Gestion du singulier/pluriel pour les centimes
                Else
                    resultat.Add("centimes")
                End If
            Else
                resultat.Add("zéro") ' Ajout de "zéro" pour les décimales si elles sont nulles
            End If
        End If

        Return String.Join(" ", resultat)
    End Function

    ''' <summary>
    ''' Ecrit les nombres de 0 à 999
    ''' </summary>
    ''' <param name="Nombre">Nombre à écrire</param>
    ''' <param name="LePays">Pays d'utilisation, pour spécificitées régionnales</param>
    Private Function Ecrit3Chiffres(ByVal Nombre As Integer, ByVal LePays As Pays) As String
        If Nombre = 100 Then
            Return "cent"
        End If

        If Nombre < 100 Then
            Return Ecrire2Chiffres(Nombre, LePays)
        End If

        Dim centaine As Integer = Nombre \ 100
        Dim reste As Integer = Nombre Mod 100

        If reste = 0 Then 'Cent prend un s quand il est multiplié et non suivi d'un nombre, comme le cas de 100 est déjà traité on est face à un multiple
            Return jusqueSeize(centaine) & " cents"
        End If

        If centaine = 1 Then
            Return "cent " & Ecrire2Chiffres(reste, LePays) 'on ne dit pas un cent X, mais cent X
        End If

        Return jusqueSeize(centaine) & " cent " & Ecrire2Chiffres(reste, LePays)
    End Function

    ''' <summary>
    ''' Ecrit les nombres de 0 à 99
    ''' </summary>
    ''' <param name="Nombre">Nombre à écrire</param>
    ''' <param name="LePays">Pays d'utilisation, pour spécificitées régionnales</param>
    ''' <returns></returns>
    Private Function Ecrire2Chiffres(ByVal Nombre As Integer, ByVal LePays As Pays) As String
        If LePays <> Pays.France Then
            dizaines(7) = "septante"
            dizaines(9) = "nonante"
        End If
        If LePays = Pays.Suisse Then
            dizaines(8) = "huitante"
        End If

        If Nombre < 17 Then
            Return jusqueSeize(Nombre)
        End If

        Select Case Nombre 'cas particuliers de 71, 80 et 81
            Case 71 'en France 71 prend un et
                If LePays = Pays.France Then
                    Return "soixante et onze"
                End If

            Case 80 'en France et Belgique le vingt prend un s
                If LePays = Pays.Suisse Then
                    Return dizaines(8)
                Else
                    Return dizaines(8) & "s"
                End If

            Case 81 'en France et Belgique il n'y a pas de et
                If LePays <> Pays.Suisse Then
                    Return dizaines(8) & "-un"
                End If
        End Select


        Dim dizaine As Integer = Nombre \ 10
        Dim unite As Integer = Nombre Mod 10

        Dim laDizaine As String = dizaines(dizaine)

        If LePays = Pays.France AndAlso (dizaine = 7 OrElse dizaine = 9) Then
            dizaine -= 1
            unite += 10
        End If


        Select Case unite
            Case 0
                Return laDizaine

            Case 1
                Return laDizaine & " et un"

            Case 17, 18, 19 'pour 77 à 79 et 97 à 99
                unite = unite Mod 10
                Return laDizaine & "-dix-" & jusqueSeize(unite)

            Case Else
                Return laDizaine & "-" & jusqueSeize(unite)
        End Select
    End Function
End Module

Public Enum Pays
    France
    Belgique
    Suisse
End Enum

Public Enum Devise
    Aucune
    Euro
    FrancSuisse
    Dollar
End Enum

