Friend Class SQLHelper

    Friend WithEvents cn As New System.Data.SqlClient.SqlConnection("integrated security=SSPI;data source=VAIO;initial catalog=Restaurant_Server") ';User ID=VAIO\Administrator;Password=;") '("Data Source=\SC_Restaurant.sdf")



    Function PopulateSelectedItemTable(ByVal strSelectedCategoy As String, ByVal sql As String, ByRef dsItem As DataSet) As DataSet

        cn.Open()
        Dim itemAdapter As New SqlClient.SqlDataAdapter

        itemAdapter.TableMappings.Add("Table", strSelectedCategoy)
        Dim cmdItemTable As SqlClient.SqlCommand = New SqlClient.SqlCommand(sql, cn)

        itemAdapter.SelectCommand = cmdItemTable
        itemAdapter.Fill(dsItem)

        cn.Close()

        Return dsItem


    End Function



End Class
