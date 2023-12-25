Imports MySql.Data.MySqlClient

Module Module1

    Public user As String = ""
    Public role As String = ""
    Public caisse As String = ""
    Public pas As String = ""
    Public charg As Boolean = False
    Public finish As Boolean = False

    Public imgName As String = "logos\logo.png"
    'Public conn2 As New MySqlConnection("datasource=localhost; username=root; password=; database=db_dipo_online")
    'Dim connscale As String = "datasource=localHost;database=sys_datos_dfs;username=user;password=dibal;connection timeout=10000;port=3306;provider=&quot;Provider MySQL&quot;"
    <Obsolete>
    Dim connvps As String = System.Configuration.ConfigurationSettings.AppSettings("connvps")
    <Obsolete>
    Dim connlocal As String = System.Configuration.ConfigurationSettings.AppSettings("connlocal")
    <Obsolete>
    Dim connserver As String = System.Configuration.ConfigurationSettings.AppSettings("connserver")

    Public conn2 As New MySqlConnection(connserver)
    Public conn As New MySqlConnection(connserver)
    Public conn3 As New MySqlConnection(connvps)
    Public tfraisM As String = 0
    Public tfraisN As String = "Coût Logistique"
    Public Livr As String = "-"
    Public ventedate As String = ""
    Public ventematricule As String = ""
    Public Casef As Boolean = False
    Public receiptprinter As String = ""
    Public a4printer As String = ""
    Public a5printer As String = ""
    Public bcprinter As String = ""
    Public Caseopen As Boolean = False
    Public txt As New TextBox
    Public comionid, ventecomionid, ventepaye, venteReste, ventemontant As Double
    Public ds As dashboard
    Public stopchecking As Boolean = False
    Public Msgboxresult As String = False



End Module
