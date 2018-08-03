Public Class Formgps
    Private Sub Formgps_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        bolStats = True
        Dim Threading_0 As New Threading.Thread(AddressOf Me.go)
        Threading_0.Priority = System.Threading.ThreadPriority.Normal
        Threading_0.IsBackground = True
        Threading_0.Start()
    End Sub
    Private bolStats As Boolean
    Public lnk As String = "-1"
    Dim zoom As Integer = 15


    Public x, y As String
    Private Sub go()
        Try
            While bolStats
                Threading.Thread.Sleep(1)
                If Not lnk = "-1" Then
                        Dim w As System.Threading.AutoResetEvent = New System.Threading.AutoResetEvent(False)
                        AddHandler wc2.DownloadDataCompleted, AddressOf DownloadDataCallback
                        wc2.DownloadDataAsync(New Uri(lnk), w)
                        w.WaitOne()
                        wc2.Dispose()
                    lnk = "-1"
                End If

            End While
        Catch ex As Exception
            Return
        End Try

    End Sub
    Private Function GMap(ByVal Latitude As String, ByVal Longitude As String, ByVal Zoom As Integer, ByVal h As Integer, ByVal w As Integer, ByVal st As String) As String
        Return "https://maps.googleapis.com/maps/api/staticmap?center=" & CStr(Latitude) & "," + CStr(Longitude) & "&zoom=" & CStr(Zoom) & "&size=" & CStr(h) & "x" & CStr(w) & "&maptype=" & st & "&markers=color:red%7Clabel:H%7C" & CStr(Latitude) & "," & CStr(Longitude) & "&key=" & "AIzaSyAf2Z-9OWLi_xUuq6YM8fycVk1vZPSuRI8"
    End Function



    Public Sub REMap()

        lnk = Me.GMap(x, y, zoom, 600, 600, If(MapBox.Tag = "0", "hybrid", "roadmap"))

    End Sub






    Public Delegate Sub Delegate1(ByVal img As Image)
    Private Sub d(ByVal img As Image)
        If Me.InvokeRequired Then
            Me.Invoke(New Delegate1(AddressOf d), New Image() {img})
            Exit Sub
        Else
            MapBox.Image = (img)
            MapBox.Invalidate()

        End If
    End Sub

    Dim WithEvents wc2 As New Net.WebClient
    Private Sub DownloadDataCallback(ByVal sender As Object, ByVal e As Net.DownloadDataCompletedEventArgs)
        Dim mem As New IO.MemoryStream
        Dim waiter As System.Threading.AutoResetEvent = CType(e.UserState, System.Threading.AutoResetEvent)
        Try
            If e.Cancelled = False AndAlso e.Error Is Nothing Then
                Dim data() As Byte = CType(e.Result, Byte())
                mem.Write(data, 0, data.Length)
            End If
        Finally
            Try
                If mem.Length > 0 Then
                    Dim returnImage As Image = Image.FromStream(mem)
                    d(returnImage)


                End If
            Catch ex As Exception
            End Try
            ProgressBar1.Value = 0
            waiter.Set()
        End Try
        Return
    End Sub
    Private Sub wc2_DownloadProgressChanged1(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles wc2.DownloadProgressChanged
        Try
            ProgressBar1.Maximum = 100
            ProgressBar1.Value = e.ProgressPercentage
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        zoom -= 1
            If zoom <= 1 Then
                zoom = 1
            End If
            REMap()

    End Sub

    Private Sub MapBox_Click(sender As Object, e As EventArgs) Handles MapBox.Click
        If MapBox.Tag = 0 Then
            MapBox.Tag = 1
        Else
            MapBox.Tag = 0
        End If
        REMap()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        zoom += 1
        If zoom >= 21 Then
            zoom = 21
        End If
        REMap()
    End Sub
End Class