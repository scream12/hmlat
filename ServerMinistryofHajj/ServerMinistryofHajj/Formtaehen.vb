Public Class Formtaehen
    Private Sub Formtaehen_Load(sender As Object, e As EventArgs) Handles MyBase.Load









        If Store.lst0.Count > 0 Then
            For Each i In Store.lst0
                Dim s As String = "|"
                DataGridView1.Rows.Add(i.Split(s)(0), i.Split(s)(1), i.Split(s)(2), i.Split(s)(3))
            Next
        End If



    End Sub
End Class