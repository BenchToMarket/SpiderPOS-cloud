Public Class OrderGridColumnPrice
    Inherits DataGridTextBoxColumn

    '   **************
    '   no longer using ... wse just use OrdeGridColumn

    Public Sub New()
        MyBase.New()


    End Sub


    Protected Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal backBrush As Brush, ByVal foreBrush As Brush, ByVal alignToRight As Boolean)

        Dim thisGrid As DataGrid = Me.DataGridTableStyle.DataGrid
        Dim statusValue As Object = thisGrid(rowNum, 0)
        Dim itemIDValue As Object = thisGrid(rowNum, 5)

        If itemIDValue = 0 Then
            foreBrush = New SolidBrush(c2)
        Else
            If statusValue = 0 Then

                foreBrush = New SolidBrush(c3)

            ElseIf statusValue = 1 Then                         '   Hold
                '     backBrush = New SolidBrush(Color.Red)
                foreBrush = New SolidBrush(Color.Red)

            ElseIf statusValue = 2 Then             '   Sent to Kitchen/Bar
                '       backBrush = New SolidBrush(c6) '(Color.Yellow)
                foreBrush = New SolidBrush(c6)


            ElseIf statusValue = 3 Then             '   Ready for Delivery
                '      backBrush = New SolidBrush(Color.Gold)
                foreBrush = New SolidBrush(Color.Gold)


            ElseIf statusValue = 4 Then             '   Delivered
                '     backBrush = New SolidBrush(Color.LightGray)
                foreBrush = New SolidBrush(Color.LightGray)


            ElseIf statusValue = -1 Then
                foreBrush = New SolidBrush(Color.LightGray)

            End If
        End If

        backBrush = New SolidBrush(Color.Black)

        MyBase.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight)

    End Sub

End Class
