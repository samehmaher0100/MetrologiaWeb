Public Class Datos_Conexion
    Public Function String_Conexion() As String
        ' Return "data source = DESKTOP-48JP9GK\SQL2014; initial catalog = SisMetPrec; user id = sa; password = Sistemas123"
        Return "data source = .\SRVMETROLOGIA; initial catalog = SisMetPrec; user id = sa; password = Sistemas123*"
    End Function

End Class
