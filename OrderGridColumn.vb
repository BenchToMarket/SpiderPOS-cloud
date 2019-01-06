Public Class OrderGridColumn
    Inherits DataGridTextBoxColumn

    Dim forPrice As Boolean
    Dim forQuantity As Boolean
    Dim forCourse As Boolean


    Public Sub New(ByVal price As Boolean, ByVal quantity As Boolean, ByVal course As Boolean)
        MyBase.New()

        forPrice = price
        forQuantity = quantity
        forCourse = course

    End Sub


    Protected Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal backBrush As Brush, ByVal foreBrush As Brush, ByVal alignToRight As Boolean)

        Dim thisGrid As DataGrid = Me.DataGridTableStyle.DataGrid
        Dim statusValue As Object = thisGrid(rowNum, 0)
        Dim sinIDValue As Object = thisGrid(rowNum, 1)
        Dim siiIDValue As Object = thisGrid(rowNum, 2)
        Dim itemIDValue As Object = thisGrid(rowNum, 6)
        Dim quantityIDValue As Object = thisGrid(rowNum, 7)
        Dim priceIDValue As Object = thisGrid(rowNum, 9)


        If forPrice = True Then '444 And itemIDValue = 0 Then
            '444   foreBrush = New SolidBrush(c2)
            If itemIDValue = 0 Then
                foreBrush = New SolidBrush(c2)
            ElseIf sinIDValue <> siiIDValue And priceIDValue = 0 Then
                foreBrush = New SolidBrush(c2)
            Else
                If statusValue = 0 Then             'not sent yet
                    If sinIDValue = siiIDValue Then
                        foreBrush = New SolidBrush(c3)
                    Else
                        foreBrush = New SolidBrush(Color.AntiqueWhite)
                    End If

                ElseIf statusValue = 1 Then                         '   Hold
                    foreBrush = New SolidBrush(Color.Red)

                ElseIf statusValue = 2 Then             '   Sent to Kitchen/Bar
                    foreBrush = New SolidBrush(c17) '(c1)  '(c6)

                ElseIf statusValue = 3 Then             '   Ready for Delivery
                    foreBrush = New SolidBrush(Color.Gold)

                ElseIf statusValue = 4 Then             '   Delivered
                    foreBrush = New SolidBrush(c10)  '(Color.LightGray)

                ElseIf statusValue = 8 Or statusValue = 9 Or statusValue = 10 Then               ' xFer or   Voided
                    foreBrush = New SolidBrush(c10)  '(Color.LightGray)

                ElseIf statusValue = -1 Then
                    foreBrush = New SolidBrush(Color.LightGray)

                End If

                If priceIDValue < 0 Then
                    foreBrush = New SolidBrush(c1)
                End If
            End If

        ElseIf forCourse = True Then
            If itemIDValue = 0 Or sinIDValue <> siiIDValue Then
                foreBrush = New SolidBrush(c2)
            Else
                foreBrush = New SolidBrush(c15)
            End If

        ElseIf forQuantity = True Then
            '   only display quantity if on main item and > 1     otherwise black
            If quantityIDValue = 1 Then
                foreBrush = New SolidBrush(c2)
                '          ElseIf quantityIDValue > 9 Then
                '             foreBrush = New SolidBrush(c1)
                '            g.ScaleTransform(0.95F, 1.0F)
            Else
                If sinIDValue <> siiIDValue Then
                    foreBrush = New SolidBrush(c2)
                Else
                    foreBrush = New SolidBrush(c1)
                End If
                '       MsgBox(bounds.ToString)
                '      bounds = New Rectangle(2, 48, 15, 23)

                If quantityIDValue > 9 Then

                    '          Me.TextBox.Font = New Font("Microsoft Sans Serif", 2.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    '                    Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                End If
            End If
        Else
            'not sure if this is just a repeat of price (or there is some other criteria passed)
            ' ithink this is for Item Name as well
            If statusValue = 0 Then                     'not ordered yet

                If sinIDValue = siiIDValue Then
                    foreBrush = New SolidBrush(c3)
                Else
                    foreBrush = New SolidBrush(Color.AntiqueWhite)
                End If

            ElseIf statusValue = 1 Then                         '   Hold
                foreBrush = New SolidBrush(Color.Red)

            ElseIf statusValue = 2 Then             '   Sent to Kitchen/Bar
                foreBrush = New SolidBrush(c17) '(c1)  '(c6)

            ElseIf statusValue = 3 Then             '   Ready for Delivery
                foreBrush = New SolidBrush(Color.Gold)

            ElseIf statusValue = 4 Then             '   Delivered
                foreBrush = New SolidBrush(c10)  '(Color.LightGray)

            ElseIf statusValue = 8 Or statusValue = 9 Or statusValue = 10 Then               ' xFer or   Voided
                foreBrush = New SolidBrush(c10)  '(Color.LightGray)

            ElseIf statusValue = -1 Then
                foreBrush = New SolidBrush(Color.LightGray)

            End If

            If priceIDValue < 0 Then
                foreBrush = New SolidBrush(c1)
            End If

        End If

        backBrush = New SolidBrush(Color.Black)



        MyBase.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight)

    End Sub
End Class
