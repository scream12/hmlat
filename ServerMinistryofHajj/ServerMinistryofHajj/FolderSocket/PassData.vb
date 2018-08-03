Public NotInheritable Class PassData
    Public Sub New(ByVal ParametersClient As SocketClient, ByVal ParametersByte As Byte())
        Try
            Me.client = ParametersClient
            Me.byte_0 = ParametersByte
        Catch ex As Exception
        End Try
    End Sub
    Public wait As Boolean = False
    Public byte_0 As Byte()
    Public client As SocketClient
End Class
