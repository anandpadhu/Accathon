Friend Class OrderItem
    Public Name As String
    Public Quantity As String
    Public Price As Decimal

    Public Sub New(ByVal name As String, ByVal quantity As String, ByVal price As Decimal)
        Me.Name = name
        Me.Quantity = quantity
        Me.Price = price
    End Sub
End Class

