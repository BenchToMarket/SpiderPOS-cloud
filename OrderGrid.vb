Public Class OrderGrid
    Inherits DataGrid


    



    Public Sub New()




    End Sub

    

    Public Sub ScrollToRow(ByVal row As Integer)
        If Not Me.DataSource Is Nothing Then
            Me.GridVScrolled(Me, New ScrollEventArgs(ScrollEventType.LargeIncrement, row))
        End If
    End Sub

   

End Class
