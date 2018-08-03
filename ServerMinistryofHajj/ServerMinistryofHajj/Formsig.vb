Imports System.ComponentModel

Public Class Formsig
    Private Sub Formsig_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Public b As Boolean
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        b = True
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        b = False
        Me.DialogResult = DialogResult.OK
    End Sub


End Class