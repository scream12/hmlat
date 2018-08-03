Public Class SocketServer
    Public Listener As Net.Sockets.TcpListener
    Public ClientsOnline As Collection = New Collection
    Public RequestData As List(Of PassData)
    Public Sub New(ByVal ParametersInteger As Integer)
        Me.ClientsOnline = New Collection
        Me.RequestData = New List(Of PassData)
        Me.Listener = New Net.Sockets.TcpListener(Net.IPAddress.Any, ParametersInteger)
        Me.Listener.Server.SendTimeout = -1
        Me.Listener.Server.ReceiveTimeout = -1
        Me.Listener.Server.SendBufferSize = 5120000 '5mb
        Me.Listener.Server.ReceiveBufferSize = 5120000 '5mb
        Me.Listener.Start()
        Me.Listener.BeginAcceptTcpClient(New AsyncCallback(AddressOf Me.Accept), Nothing)

        Dim Threading_0 As New Threading.Thread(AddressOf Me.PassData)
        Threading_0.Priority = System.Threading.ThreadPriority.Normal
        Threading_0.IsBackground = True
        Threading_0.Start()
    End Sub
    Public Sub Accept(ByVal ar As IAsyncResult)
        Try
            Dim TCPAccept As Net.Sockets.TcpClient = Me.Listener.EndAcceptTcpClient(ar)
            Dim IPEndPointClient As System.Net.IPEndPoint = TCPAccept.Client.RemoteEndPoint
            Dim SocketClient As SocketClient = New SocketClient(TCPAccept, Me)
            Try
                SocketClient.Client.ReceiveTimeout = -1
                SocketClient.Client.SendTimeout = -1
                SocketClient.Client.SendBufferSize = 1024000 '1mb
                SocketClient.Client.ReceiveBufferSize = 1024000 '1mb
                Dim Sync As Collection = Me.ClientsOnline
                SyncLock Sync
                    Me.ClientsOnline.Add(SocketClient, SocketClient.ClientRemoteAddress, Nothing, Nothing)
                    Data.ClientsOnline.Add(SocketClient)
                End SyncLock
            Catch ex As Exception

            End Try
        Catch Exception As Exception

        End Try
        Threading.Thread.Sleep(1)
        Me.Listener.BeginAcceptTcpClient(New AsyncCallback(AddressOf Me.Accept), Nothing)
    End Sub
    Public Sub PassData()
        Try
            Do While True
                Dim i As PassData = Nothing
                Dim Sync As List(Of PassData) = Me.RequestData
                SyncLock Sync
                    If (Me.RequestData.Count > 0) Then
                        i = Me.RequestData.Item(0)
                        Me.RequestData.RemoveAt(0)
                    End If
                End SyncLock
                If Not i Is Nothing Then
                    i.wait = True
                    Data.Data_0(New Object() {i.client, i.byte_0})
                End If
                Threading.Thread.Sleep(1)
            Loop
        Catch Exception As Exception

        End Try
    End Sub

End Class

