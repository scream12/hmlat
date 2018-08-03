Imports System.Threading
Public Class SocketClient
    Public Socket As SocketServer
    Public Client As Net.Sockets.TcpClient
    Public ClientAddressIP As String
    Public ClientRemoteAddress As String
    Public IsClose As Boolean
    Public TimeOut As Boolean
    Public buffer As Byte()
    Public MemoryStream As IO.MemoryStream
    Public Timers As Timer
    Public Maxsize As Long = -1
    Public SystemDateTime As System.DateTime
    Private Out_1 As System.DateTime
    Public hml As String

    Public Sub New(ByVal ParametersClient As Net.Sockets.TcpClient, ByVal ParametersSocket As SocketServer)
        Me.TimeOut = False
        Me.IsClose = False
        Try

            Me.Timers = New Threading.Timer(New TimerCallback(AddressOf Me.SubTimers), Nothing, 0, 1000)

            Me.MemoryStream = New IO.MemoryStream
            Me.buffer = New Byte(1 - 1) {}
            Me.Client = ParametersClient
            Me.Socket = ParametersSocket
            Me.ClientRemoteAddress = CType(Me.Client.Client.RemoteEndPoint, Net.IPEndPoint).ToString()
            Me.ClientAddressIP = CType(Me.Client.Client.RemoteEndPoint, Net.IPEndPoint).Address.ToString()
            Me.Client.Client.BeginReceive(Me.buffer, 0, Me.buffer.Length, Net.Sockets.SocketFlags.None, New AsyncCallback(AddressOf Me.DataRecieved), Nothing)
        Catch Exception As Exception
            If Me.IsClose = False Then Me.Close(False)
        End Try
    End Sub
    Public Sub Send(ByVal ParametersString As String)
        Try
            Dim slng As String = ParametersString & Data.SplitData
            ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Me.SendToClient), Store.Encoding().GetBytes("-" & slng.Length & ChrW(0) & slng))
        Catch ex As Exception
        End Try
    End Sub
    Public Sub SendToClient(ByVal ParametersByte As Byte())
        Try
            Dim Sync As SocketClient = Me
            SyncLock Sync
                'Me.Client.Client.Poll(-1, Net.Sockets.SelectMode.SelectWrite)
                'Me.Client.Client.SendBufferSize = ParametersByte.Length
                Try
                    Me.Client.Client.Send(ParametersByte, 0, ParametersByte.Length, Net.Sockets.SocketFlags.None)
                Catch SocketException As Net.Sockets.SocketException
                    If Me.IsClose = False Then Me.Close(False)
                End Try
            End SyncLock
        Catch Exception As Exception
            If Me.IsClose = False Then Me.Close(False)
        End Try
    End Sub
    Public Sub SubTimers()
        Try
            Dim Sync As SocketClient = Me
            SyncLock Sync
                If Me.IsClose = False Then


                    Dim s As String = "poing" & Data.SplitData
                    SendToClient(Store.Encoding().GetBytes("-" & s.Length & ChrW(0) & s))
                End If
            End SyncLock
        Catch ex As Exception
        End Try
    End Sub

    Public Sub DataRecieved(ByVal ar As IAsyncResult)
        Try
            If ar Is Nothing Then
                If Me.IsClose = False Then Me.Close(False)
                GoTo finish
            End If
            Dim DataSize As Integer = 0
            Try
                If Me.IsClose = False Then
                    If Me.Client.Client.Connected Then
                        DataSize = Me.Client.Client.EndReceive(ar)
                    Else
                        If Me.IsClose = False Then Me.Close(False)
                        GoTo finish
                    End If
                Else
                    GoTo finish
                End If
            Catch SocketException As Net.Sockets.SocketException
                If Me.IsClose = False Then Me.Close(False)
                GoTo finish
            End Try
            If DataSize > 0 Then
                If Maxsize = -1 Then
                    If Me.buffer(0) = 0 Then
                        Dim GetSize As String = Store.Encoding().GetString(Me.MemoryStream.ToArray).Trim
                        Maxsize = CLng(If(IsNumeric(GetSize), GetSize, -1))
                        Me.buffer = New Byte((CInt((Me.Maxsize - 1)) + 1) - 1) {}
                        Me.MemoryStream.Dispose()
                        Me.MemoryStream = New IO.MemoryStream
                    Else
                        Me.MemoryStream.WriteByte(Me.buffer(0))
                    End If
                Else
                    Me.MemoryStream.Write(Me.buffer, 0, DataSize)
                    If Me.MemoryStream.ToArray.Length = Maxsize Then
                        Dim i As New PassData(Me, Me.MemoryStream.ToArray)
                        Dim Sync As List(Of PassData) = Me.Socket.RequestData
                        SyncLock Sync
                            Me.Socket.RequestData.Add(i)
                        End SyncLock
                        Do While Not i.wait
                            Thread.Sleep(1)
                        Loop
                        Me.MemoryStream.Dispose()
                        Me.MemoryStream = New IO.MemoryStream
                        Maxsize = -1
                        Me.buffer = New Byte(1 - 1) {}
                    Else
                        Me.buffer = New Byte((CInt(((Me.Maxsize - Me.MemoryStream.Length) - 1)) + 1) - 1) {}
                    End If
                End If
                Try
                    If Me.IsClose = False Then
                        If Me.Client.Client.Connected Then
                            Me.Client.Client.BeginReceive(Me.buffer, 0, Me.buffer.Length, Net.Sockets.SocketFlags.None, New AsyncCallback(AddressOf Me.DataRecieved), Nothing)
                        Else
                            If Me.IsClose = False Then Me.Close(False)
                            GoTo finish
                        End If
                    Else
                        GoTo finish
                    End If
                Catch SocketException As Net.Sockets.SocketException
                    If Me.IsClose = False Then Me.Close(False)
                    GoTo finish
                End Try
            End If
finish:
        Catch ex As Exception
        End Try
    End Sub
    Dim int As Integer = 0
    Public Sub Close(ByVal ParametersBoolean As Boolean)









        Try
            Me.IsClose = True
            Try
                Me.Timers.Dispose()
            Catch Exception As Exception
            End Try
            Try
                Me.MemoryStream.Dispose()
            Catch Exception As Exception
            End Try
            Try
                If Me.Client.Connected Then
                    Me.Client.GetStream.Close()
                End If
            Catch Exception As Exception
            End Try
            Try
                Me.Client.Client.Close()
            Catch Exception As Exception
            End Try
            Dim Sync As Collection = Me.Socket.ClientsOnline
            SyncLock Sync
                If Me.Socket.ClientsOnline.Contains(Me.ClientRemoteAddress) Then
                    Me.Socket.ClientsOnline.Remove(Me.ClientRemoteAddress)
                End If
            End SyncLock

            Dim Sync1 As Collection = Data.ClientsOnline
            SyncLock Sync1
                If Data.ClientsOnline.Contains(Me.ClientRemoteAddress) Then
                    Data.ClientsOnline.Remove(Me.ClientRemoteAddress)
                End If
            End SyncLock










            Try
                Dim i As Integer = 0
                Do While (Data.srv.DataGridView1.Rows.Count > 0)
                    Dim dRows As DataGridViewRow = Data.srv.DataGridView1.Rows(i)
                    If dRows.Cells(0).Value.ToString = hml Then

                        Dim SocketClient As SocketClient = DirectCast(dRows.Tag, SocketClient)
                        If Not SocketClient Is Nothing Then
                            If SocketClient.IsClose = False Then
                            Else

                                dRows.Cells(4).Value = "غير متصل"
                            End If
                        End If




                        'Exit Do
                    End If


                    If i = Data.srv.DataGridView1.Rows.Count - 1 Then
                        Exit Do
                    Else
                        i += 1
                    End If
                Loop


            Catch ex As Exception

            End Try










            If ParametersBoolean = False Then

                'Console.WriteLine("Disconnect--- > " & Me.ClientRemoteAddress)
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class


