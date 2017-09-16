Imports System.Collections.Generic

Friend Class Order
    Public OrderId As Integer
    Public CustomerName As String
    Public Currency As String
    Public Items As List(Of OrderItem) = New List(Of OrderItem)()
End Class