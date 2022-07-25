Imports Microsoft.VisualBasic

Public Class clFunciones
    '== Función para verificar que la cédula ingresada sea correcta y coincida con el número verificador
    Public Function VerificaCedula(Cedula As String) As Boolean
        VerificaCedula = True
        If Len(Trim(Cedula)) <> 10 Then
            VerificaCedula = False
        End If

        If Val(Mid(Cedula, 1, 2)) > 25 Then
            VerificaCedula = False
        End If

        If Val(Mid(Cedula, 3, 1)) > 5 Then
            VerificaCedula = False
        End If

        If VerificaCedula = False Then
            MsgBox("Número de Cédula incorrecto.", vbInformation + vbYes, "Cédula")
        Else
            Dim Total As Integer
            Dim Cifra As Integer
            Total = 0

            For a = 1 To 9

                If (a Mod 2) = 0 Then
                    Cifra = Val(Mid(Cedula, a, 1))
                Else
                    Cifra = Val(Mid(Cedula, a, 1)) * 2
                    If Cifra > 9 Then
                        Cifra = Cifra - 9
                    End If
                End If
                Total = Total + Cifra
            Next

            Cifra = Total Mod 10

            If Cifra > 0 Then
                Cifra = 10 - Cifra
            End If

            If Cifra = Val(Mid(Cedula, 10, 1)) Then
                VerificaCedula = True
            Else
                MsgBox("Atención: El Número de Cédula ingresado NO ES VÁLIDO." & Chr(13) & _
                       "Explicación: El dígito verificador (décimo dígito de la cédula)" & Chr(13) &
                       "es el resultado de un algoritmo realizado sobre los anteriores " & Chr(13) & _
                       "9 dígitos. En éste caso el número ingresado no cumple con la condición." & Chr(13) & _
                       "El documento puede ser FALSO. Verifique por favor.", vbExclamation + vbYes, "Cédula")
                VerificaCedula = False
            End If

        End If

    End Function
    Public Function cadena_ret(ByVal llegada As String) As String
        Dim largo As Integer
        Dim sale As String = ""
        Dim trabaja As String
        Dim asci As Integer
        Dim cont As Integer = 0

        largo = Len(llegada)
        For i = 1 To largo
            trabaja = Mid(llegada, i, 1)
            asci = Asc(trabaja)
            If asci >= 32 And asci <= 126 Or asci = 164 Or asci = 209 Or asci = 193 Or asci = 201 Or asci = 205 Or asci = 211 Or asci = 218 Then
                If asci <> 32 Then
                    cont = 0
                Else
                    cont = cont + 1
                End If
                If cont <= 1 Then
                    sale = sale & (trabaja)
                Else

                End If
            End If
        Next
        Return sale
    End Function
End Class

