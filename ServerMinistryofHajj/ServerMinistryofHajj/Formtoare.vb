Public Class Formtoare
    Private Sub Formtoare_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        If Store.lst1.Count > 0 Then
            For Each i In Store.lst1

                Dim s As String = "|"
                DataGridView1.Rows.Add(i.Split(s)(0), i.Split(s)(1))
            Next
        End If
    End Sub
End Class