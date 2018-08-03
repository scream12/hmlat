Imports System.IO
Imports System.Net

Public Class Server
    Public Socket As SocketServer
    Private Sub Server_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        CheckForIllegalCrossThreadCalls = False
        Threading.ThreadPool.SetMinThreads(250, 250)
        Threading.ThreadPool.SetMaxThreads(250, 250)

        Data.srv = Me
        Try
            Me.Socket = New SocketServer(10821)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Dim a As String = ""
        For sta As Integer = 0 To Store.CountryName.Length - 1
            a += Store.CountryName(sta) & vbNewLine

        Next

        getAll()

        'alg(1)


    End Sub

    Private Sub getAll()
        Dim dirs As String() = Directory.GetDirectories(Store.Resources(0) & "data\")
        Dim dir As String
        For Each dir In dirs
            If File.Exists(dir & "\d.HML") Then
                Dim spl As String() = IO.File.ReadAllText(dir & "\d.HML").Trim.Split("|")
                Dim flag As Integer = (Store.CountryName.ToList().FindIndex(Function(x) x = spl(3)))
                Dim bm As Bitmap
                If File.Exists(Store.Resources(0) & "Flags\" & Store.CountryCode(flag).ToLower & ".png") Then
                    bm = New Bitmap(Store.Resources(0) & "Flags\" & Store.CountryCode(flag).ToLower & ".png")
                Else
                    bm = New Bitmap(Store.Resources(0) & "Flags\" & "null" & ".png")
                End If

                DataGridView1.Rows.Add(spl(0), spl(2), bm, spl(3), "غير متصل", "متوقف", "غير معروف", Nothing, Nothing)


            End If
        Next
    End Sub

    Private Sub alg(ByVal p As Integer)
        Dim h_AllHAJJ As Long = 2000000 '2M جميع الحجاج
        Dim h_ALLHMLH As Long = 10000 '؟ اجمالي الحملات
        Dim h_HMLHCOUNT As Integer = 200 '؟ عدد افراد الحمله
        Dim NumberdaysHajj As Integer = 3 ' عدد ايام الحج
        Dim NUMHOURS As Integer = 12 ' عدد ساعات التحرك

        Select Case p
            Case 1
                'النقطة الاولى من منى لعرفة 
                Dim go As Long = CInt(h_ALLHMLH / 3) ''' تقسيم ع الثلث 
                MsgBox("يجب تحريك " & go & " حملة في الوقت الحالي والانتظار 30 دقيقه لتحريك الجزء الاخر") ' تقسيم ع الثلث 
            Case 2
                'من عرفة الى مزدلفة
                Dim go As Long = CInt(h_ALLHMLH / 3) ''' تقسيم ع الثلث 
                MsgBox("يجب تحريك " & go & " حملة في الوقت الحالي والانتظار 30 دقيقه لتحريك الجزء الاخر") ' تقسيم ع الثلث 
            Case 3
                'من مزدلفة الي منى ( الجمرات )
                Dim go As Long = CInt(h_ALLHMLH / 3) ''' تقسيم ع الثلث 
                MsgBox("يجب تحريك " & go & " حملة في الوقت الحالي والانتظار 30 دقيقه لتحريك الجزء الاخر") ' تقسيم ع الثلث 

        End Select




    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim form1 As New FormAdd
        Select Case form1.ShowDialog()
            Case DialogResult.OK
                DataGridView1.Rows.Clear()
                getAll()
        End Select

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim form1 As New Formtoare
        Select Case form1.ShowDialog()
            Case DialogResult.OK

        End Select

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim form1 As New Formtaehen
        Select Case form1.ShowDialog()
            Case DialogResult.OK

        End Select
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint



    End Sub

    Private Sub SigToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SigToolStripMenuItem.Click
        sg()
    End Sub
    Private Sub sg()
        Dim sig As New Formsig

        Select Case sig.ShowDialog(Me)
            Case DialogResult.OK
                Try

                    Dim IEnume As IEnumerator
                    Try
                        IEnume = Me.DataGridView1.SelectedRows.GetEnumerator
                        Do While IEnume.MoveNext
                            Dim curIndex As DataGridViewRow = DirectCast(IEnume.Current, DataGridViewRow)
                            Dim SocketClient As SocketClient = DirectCast(curIndex.Tag, SocketClient)
                            If Not SocketClient Is Nothing Then
                                If sig.b Then
                                    SocketClient.Send("GO" & Data.SplitData)
                                Else

                                    SocketClient.Send("stop" & Data.SplitData)
                                End If

                            End If
                        Loop
                    Finally
                        If TypeOf IEnume Is IDisposable Then
                            DirectCast(IEnume, IDisposable).Dispose()
                        End If
                    End Try
                    sig.Close()
                Catch ex As Exception
                    sig.Close()
                End Try
            Case Else
                sig.Close()
        End Select
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click



        Dim IEnume As IEnumerator
        Try
            IEnume = Me.DataGridView1.SelectedRows.GetEnumerator
            Do While IEnume.MoveNext
                Dim curIndex As DataGridViewRow = DirectCast(IEnume.Current, DataGridViewRow)
                Dim SocketClient As SocketClient = DirectCast(curIndex.Tag, SocketClient)
                If Not SocketClient Is Nothing Then

                    Dim lc As String = curIndex.Cells(6).Value.ToString
                    If lc.Contains(",") Then
                        Dim handle As String = "GPS_" & "I300" & SocketClient.hml
                        Dim t As Formgps = My.Application.OpenForms(handle)
                        If t Is Nothing Then
                            t = New Formgps
                            t.Name = handle
                            t.Text = "GPS -" & SocketClient.hml

                            DirectCast(t, Control).Show()

                            t.x = lc.Split(",")(0)
                            t.y = lc.Split(",")(1)
                            t.REMap()
                        End If
                    End If






                End If
            Loop
        Finally
            If TypeOf IEnume Is IDisposable Then
                DirectCast(IEnume, IDisposable).Dispose()
            End If
        End Try









    End Sub


    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        If Not e.RowIndex = -1 Then

            If e.ColumnIndex = 7 Then
                Dim IEnume As IEnumerator
                Try
                    IEnume = Me.DataGridView1.SelectedRows.GetEnumerator
                    Do While IEnume.MoveNext
                        Dim curIndex As DataGridViewRow = DirectCast(IEnume.Current, DataGridViewRow)
                        Dim SocketClient As SocketClient = DirectCast(curIndex.Tag, SocketClient)
                        If Not SocketClient Is Nothing Then
                            SocketClient.Send("stop" & Data.SplitData)
                        End If
                    Loop
                Finally
                    If TypeOf IEnume Is IDisposable Then
                        DirectCast(IEnume, IDisposable).Dispose()
                    End If
                End Try




            ElseIf e.ColumnIndex = 8 Then
                Dim IEnume As IEnumerator
                Try
                    IEnume = Me.DataGridView1.SelectedRows.GetEnumerator
                    Do While IEnume.MoveNext
                        Dim curIndex As DataGridViewRow = DirectCast(IEnume.Current, DataGridViewRow)
                        Dim SocketClient As SocketClient = DirectCast(curIndex.Tag, SocketClient)
                        If Not SocketClient Is Nothing Then
                            SocketClient.Send("GO" & Data.SplitData)
                        End If
                    Loop
                Finally
                    If TypeOf IEnume Is IDisposable Then
                        DirectCast(IEnume, IDisposable).Dispose()
                    End If
                End Try
            End If
        End If
    End Sub
End Class
