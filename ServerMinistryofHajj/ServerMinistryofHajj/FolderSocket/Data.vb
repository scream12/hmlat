Imports System.Threading
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.CompilerServices

Public NotInheritable Class Data
    Public Shared srv As Server

    Public Shared Socket As SocketServer
    Public Shared SplitData, SplitByte As String
    Public Shared ClientsOnline As Collection = New Collection

    Shared Sub New()
        Data.SplitData = "0x00x0"
        Data.SplitByte = "0b00b0"
    End Sub


    Public Delegate Sub Delegate0(ByVal ParametersObject As Object)
    Public Shared Sub Data_0(ByVal ParametersObject As Object)
        Try
            Dim Client As SocketClient = DirectCast(NewLateBinding.LateIndexGet(ParametersObject, New Object(0) {DirectCast(0, Object)}, DirectCast(Nothing, String())), SocketClient)
            Dim Byte_0 As Byte() = DirectCast(NewLateBinding.LateIndexGet(ParametersObject, New Object(0) {DirectCast(1, Object)}, DirectCast(Nothing, String())), Byte())

            Dim ArrayL As Array() = Store.SplitByte(Store.Decompress(Byte_0), SplitByte)
            If Not ArrayL.Length = 2 Then
                Exit Sub
            End If

            Dim DataArray As String() = Store.Encoding().GetString(DirectCast(ArrayL.GetValue(0), Byte())).Split({SplitData}, StringSplitOptions.None)

            If DirectCast(Data.srv, Control).IsDisposed Then
                Exit Sub
            Else
                If Data.srv.InvokeRequired Then
                    Data.srv.Invoke(New Delegate0(AddressOf Data.Data_0), New Object() {Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(ParametersObject)})
                    Exit Sub
                Else

                    If Client.IsClose = False Then
                        Select Case DataArray(0)
                            Case "L"


                                Try
                                    Dim str As String() = IO.File.ReadAllText(Store.Resources(0) & "data\" & DataArray(1) & "\d.HML").Trim.Split("|")
                                    Dim pass As String = str(1).Trim
                                    If DataArray(2).Trim = pass Then
                                        Client.Send("OK" & SplitData)
                                    Else
                                        Client.Send("ER" & SplitData)
                                        'Client.Close(False)
                                        Return
                                    End If
                                Catch ex As Exception
                                    Client.Send("ER" & SplitData)
                                    'Client.Close(False)
                                    Return
                                End Try

                                Try
                                    Dim i As Integer = 0
                                    Do While (srv.DataGridView1.Rows.Count > 0)
                                        Dim dRows As DataGridViewRow = srv.DataGridView1.Rows(i)
                                        If dRows.Cells(0).Value.ToString = DataArray(1) Then

                                            dRows.Cells(4).Value = "متصل"


                                            If DataArray(3) = "true" Then
                                                dRows.Cells(5).Value = "مسموح"
                                            Else
                                                dRows.Cells(5).Value = "متوقف"
                                            End If


                                            dRows.Tag = Client
                                            Client.hml = DataArray(1)
                                            Exit Do
                                        End If
                                        If i = srv.DataGridView1.Rows.Count - 1 Then
                                            Exit Do
                                        Else
                                            i += 1
                                        End If
                                    Loop
                                Catch ex As Exception

                                End Try



                            Case "Location"

                                Try
                                    Dim i As Integer = 0
                                    Do While (srv.DataGridView1.Rows.Count > 0)
                                        Dim dRows As DataGridViewRow = srv.DataGridView1.Rows(i)
                                        If dRows.Cells(0).Value.ToString = DataArray(1) Then
                                            dRows.Cells(6).Value = DataArray(2) & "," & DataArray(3)

                                            Dim handle As String = "GPS_" & "I300" & Client.hml
                                            Dim t As Formgps = My.Application.OpenForms(handle)
                                            If t IsNot Nothing Then
                                                t.x = DataArray(2)
                                                t.y = DataArray(3)
                                                t.REMap()
                                            End If



                                            Exit Do
                                        End If
                                        If i = srv.DataGridView1.Rows.Count - 1 Then
                                            Exit Do
                                        Else
                                            i += 1
                                        End If
                                    Loop



                                Catch ex As Exception

                                End Try


                            Case "tp"




                                Dim s As String = "|"
                                Store.lst1.Add(DataArray(1) & s & DataArray(2) & "," & DataArray(3))



                            Case "T"

                                Try
                                    Dim i As Integer = 0
                                    Do While (srv.DataGridView1.Rows.Count > 0)
                                        Dim dRows As DataGridViewRow = srv.DataGridView1.Rows(i)
                                        If dRows.Cells(0).Value.ToString = DataArray(1) Then
                                            'dRows.Cells(5).Value = DataArray(2)

                                            If DataArray(2) = "true" Then
                                                dRows.Cells(5).Value = "مسموح"
                                            Else
                                                dRows.Cells(5).Value = "متوقف"
                                            End If


                                            Exit Do
                                        End If
                                        If i = srv.DataGridView1.Rows.Count - 1 Then
                                            Exit Do
                                        Else
                                            i += 1
                                        End If
                                    Loop
                                Catch ex As Exception

                                End Try


                            Case "mk"

                                Store.lst0.Add(DataArray(1) & "|" & DataArray(2) & "|" & DataArray(3) & "|" & DataArray(4))





                            Case Else
                        End Select



                    End If
                End If
            End If


        Catch Exception As Exception
            MsgBox(Exception.ToString)
        End Try
    End Sub





End Class
