Imports System.ComponentModel
Imports System.IO

Public Class FormAdd



    Private Sub FormAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim dirs As String() = Directory.GetDirectories(Store.Resources(0) & "data\")

        Dim dir As String
        For Each dir In dirs
            If File.Exists(dir & "\d.HML") Then
                Dim spl As String() = IO.File.ReadAllText(dir & "\d.HML").Trim.Split("|")
                DataGridView1.Rows.Add(spl(0), spl(1), spl(2), spl(3))
            End If
        Next

    End Sub

    Private Sub FormAdd_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Me.DialogResult = DialogResult.OK
    End Sub



    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        If Not e.RowIndex = -1 Then

            Dim id As Integer = If(e.RowIndex = 0, 0, e.RowIndex - 1)
            Dim Folder_ALL As String = Store.Resources(0) & "data\" & CStr(DataGridView1.Rows(id).Cells(0).Value)
            If Not DataGridView1.Rows(id).Cells(0).Value = Nothing Then
                If Not My.Computer.FileSystem.DirectoryExists(Folder_ALL) Then
                    My.Computer.FileSystem.CreateDirectory(Folder_ALL)
                    sv(id, Folder_ALL)
                Else
                    sv(id, Folder_ALL)
                End If
            End If
        End If

    End Sub
    Private Sub sv(ByVal id As Integer, ByVal p As String)
        If My.Computer.FileSystem.DirectoryExists(p) Then
            Dim d As New Text.StringBuilder
            For x As Integer = 0 To DataGridView1.ColumnCount - 1
                d.Append(DataGridView1.Rows(id).Cells(x).Value & "|")
            Next
            IO.File.WriteAllText(p & "\d.HML", d.ToString)
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            Dim IEnume As IEnumerator
            Try
                IEnume = Me.DataGridView1.SelectedRows.GetEnumerator
                Do While IEnume.MoveNext
                    Dim curIndex As DataGridViewRow = DirectCast(IEnume.Current, DataGridViewRow)

                    Dim Folder_ALL As String = Store.Resources(0) & "data\" & CStr(curIndex.Cells(0).Value)
                    If IO.Directory.Exists(Folder_ALL) Then
                        IO.Directory.Delete(Folder_ALL, True)
                    End If

                Loop
            Finally
                If TypeOf IEnume Is IDisposable Then
                    DirectCast(IEnume, IDisposable).Dispose()
                End If
            End Try
        End If
    End Sub
End Class