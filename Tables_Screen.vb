Public Class Tables_Screen
    Inherits System.Windows.Forms.Form

    Dim ActiveOrder As term_OrderForm

    Dim btnAvailTable(24) As AvailTableButton
    Dim btnTransferTable(9) As AvailTableButton
    '    Dim availTableList As New ArrayList
    Dim transferTableList As New ArrayList


    Private sql As New DataSet_Builder.SQLHelper


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal employeeID As Integer, ByVal currentMenu As Integer)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        InitializeOther()


    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnAddTable As System.Windows.Forms.Button
    Friend WithEvents txtAddTable As System.Windows.Forms.TextBox
    Friend WithEvents pnlAvailTables As System.Windows.Forms.Panel
    Friend WithEvents pnlTransferTables As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlDataAdapterTablesAvail As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlAvailTables = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnAddTable = New System.Windows.Forms.Button
        Me.txtAddTable = New System.Windows.Forms.TextBox
        Me.pnlTransferTables = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.SqlDataAdapterTablesAvail = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SuspendLayout()
        '
        'pnlAvailTables
        '
        Me.pnlAvailTables.BackColor = System.Drawing.SystemColors.Desktop
        Me.pnlAvailTables.Location = New System.Drawing.Point(112, 48)
        Me.pnlAvailTables.Name = "pnlAvailTables"
        Me.pnlAvailTables.Size = New System.Drawing.Size(560, 310)
        Me.pnlAvailTables.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(264, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(296, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Current Tables"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAddTable
        '
        Me.btnAddTable.Location = New System.Drawing.Point(184, 520)
        Me.btnAddTable.Name = "btnAddTable"
        Me.btnAddTable.Size = New System.Drawing.Size(120, 48)
        Me.btnAddTable.TabIndex = 2
        Me.btnAddTable.Text = "Add Table"
        '
        'txtAddTable
        '
        Me.txtAddTable.Location = New System.Drawing.Point(320, 536)
        Me.txtAddTable.Name = "txtAddTable"
        Me.txtAddTable.Size = New System.Drawing.Size(64, 20)
        Me.txtAddTable.TabIndex = 3
        Me.txtAddTable.Text = "TextBox1"
        '
        'pnlTransferTables
        '
        Me.pnlTransferTables.BackColor = System.Drawing.SystemColors.Desktop
        Me.pnlTransferTables.Location = New System.Drawing.Point(112, 400)
        Me.pnlTransferTables.Name = "pnlTransferTables"
        Me.pnlTransferTables.Size = New System.Drawing.Size(560, 72)
        Me.pnlTransferTables.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(344, 368)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(160, 32)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Transfer Tables"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "workstation id=VAIO;packet size=4096;integrated security=SSPI;data source=VAIO;pe" & _
        "rsist security info=False;initial catalog=Restaurant_Server"
        '
        'SqlDataAdapterTablesAvail
        '
        Me.SqlDataAdapterTablesAvail.DeleteCommand = Me.SqlDeleteCommand1
        Me.SqlDataAdapterTablesAvail.InsertCommand = Me.SqlInsertCommand1
        Me.SqlDataAdapterTablesAvail.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapterTablesAvail.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ExperienceTable", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ExperienceNumber", "ExperienceNumber"), New System.Data.Common.DataColumnMapping("ExperienceDate", "ExperienceDate"), New System.Data.Common.DataColumnMapping("TableNumber", "TableNumber"), New System.Data.Common.DataColumnMapping("EmployeeID", "EmployeeID"), New System.Data.Common.DataColumnMapping("NumberOfCustomers", "NumberOfCustomers"), New System.Data.Common.DataColumnMapping("LastStatus", "LastStatus")})})
        Me.SqlDataAdapterTablesAvail.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "[TablesAvailSelectCommand]"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeID", System.Data.SqlDbType.Int, 4, "EmployeeID"))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ExperienceDate", System.Data.SqlDbType.DateTime, 8, "ExperienceDate"))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "[TablesAvailInsertCommand]"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ExperienceNumber", System.Data.SqlDbType.Int, 4, "ExperienceNumber"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ExperienceDate", System.Data.SqlDbType.DateTime, 8, "ExperienceDate"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TableNumber", System.Data.SqlDbType.Int, 4, "TableNumber"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeID", System.Data.SqlDbType.Int, 4, "EmployeeID"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NumberOfCustomers", System.Data.SqlDbType.Int, 4, "NumberOfCustomers"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LastStatus", System.Data.SqlDbType.Int, 4, "LastStatus"))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "[TablesAvailUpdateCommand]"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ExperienceNumber", System.Data.SqlDbType.Int, 4, "ExperienceNumber"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ExperienceDate", System.Data.SqlDbType.DateTime, 8, "ExperienceDate"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TableNumber", System.Data.SqlDbType.Int, 4, "TableNumber"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeID", System.Data.SqlDbType.Int, 4, "EmployeeID"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NumberOfCustomers", System.Data.SqlDbType.Int, 4, "NumberOfCustomers"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LastStatus", System.Data.SqlDbType.Int, 4, "LastStatus"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ExperienceNumber", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ExperienceNumber", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_EmployeeID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "EmployeeID", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ExperienceDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ExperienceDate", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LastStatus", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "LastStatus", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_NumberOfCustomers", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "NumberOfCustomers", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_TableNumber", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "TableNumber", System.Data.DataRowVersion.Original, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "[TablesAvailDeleteCommand]"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ExperienceNumber", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ExperienceNumber", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_EmployeeID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "EmployeeID", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ExperienceDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ExperienceDate", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LastStatus", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "LastStatus", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_NumberOfCustomers", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "NumberOfCustomers", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_TableNumber", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "TableNumber", System.Data.DataRowVersion.Original, Nothing))
        '
        'Tables_Screen
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(128, Byte), CType(128, Byte), CType(255, Byte))
        Me.ClientSize = New System.Drawing.Size(792, 573)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pnlTransferTables)
        Me.Controls.Add(Me.txtAddTable)
        Me.Controls.Add(Me.btnAddTable)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pnlAvailTables)
        Me.Name = "Tables_Screen"
        Me.Text = "Tables Screen"
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub InitializeOther()

        '     PopulateAvailTables(currentServer.EmployeeID)
        populateStoredProcedure(currentServer.EmployeeID)

        CreateDataViewsAvailTables()
        DisplayAvailTables()

    End Sub

    Private Sub PopulateStoredProcedure(ByVal empID As Integer)
        Dim todaysDate As Date
        Dim yesterdaysDate As Date

        '   will need to add yesterdays date to parameters if we can not adjust day definition
        '   to do this: perform 2 seperate FILL requests back to back
        todaysDate = Format(Today, "D")
        yesterdaysDate = Format(Today.AddDays(-1), "D")

        Me.SqlConnection1.Open()
        Me.SqlSelectCommand1.Parameters("@EmployeeID").Value = empID
        Me.SqlSelectCommand1.Parameters("@ExperienceDate").Value = todaysDate
        Me.SqlDataAdapterTablesAvail.Fill(dsOrder.Tables("AvailTables"))
        Me.SqlConnection1.Close()

    End Sub

    Private Sub CreateDataViewsAvailTables()

        dvAvailTables = New DataView
        With dvAvailTables
            .Table = dsOrder.Tables("AvailTables")
            .RowFilter = "LastStatus < 8"
            .RowStateFilter = DataViewRowState.CurrentRows

        End With

        dvTransferTables = New DataView
        With dvTransferTables
            .Table = dsOrder.Tables("AvailTables")
            .RowFilter = "LastStatus = 8"
        End With

    End Sub

    Private Sub DisplayAvailTables()

        Dim index As Integer
        Dim counterIndex As Integer
        Dim bs = 10
        Dim x As Integer = 10
        Dim y As Integer = 10
        Dim w As Integer = 100
        Dim h As Integer = 50


        For index = 0 To dvAvailTables.Count - 1
            btnAvailTable(index) = New AvailTableButton
            With btnAvailTable(index)
                .Text = dvAvailTables(index)("TableNumber")
                .Size = New Size(w, h)
                .Location = New Point(x, y)
                .TabStop = False
                .ExperienceNumber = dvAvailTables(index)("ExperienceNumber")
                If dvAvailTables(index)("LastStatus") = 8 Then
                    .BackColor = Color.Blue 'transfered
                ElseIf dvAvailTables(index)("LastStatus") = 2 Then
                    .BackColor = c7  'sat
                ElseIf dvAvailTables(index)("LastStatus") > 2 Then
                    .BackColor = c9
                End If
                .ForeColor = c3
                pnlAvailTables.Controls.Add(btnAvailTable(index))
                AddHandler btnAvailTable(index).Click, AddressOf TableSelect

            End With

            x = x + w + bs

            counterIndex += 1
            If counterIndex = 5 Then
                x = 10
                y = y + h + bs
                counterIndex = 0
            End If

        Next


    End Sub

    Private Sub DisplayTransferTables()

    End Sub


    Private Sub TableSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlAvailTables.Click
        Dim objButton As New AvailTableButton
        If Not sender.GetType Is objButton.GetType Then Exit Sub

        Dim tableNumber As Integer
        Dim ExperienceNumber As Integer

        objButton = CType(sender, AvailTableButton)
        tableNumber = CType(objButton.Text, Integer)
        ExperienceNumber = CType(objButton.ExperienceNumber, Integer)

        ActiveOrder = New term_OrderForm(currentMenu, currentServer.EmployeeID, tableNumber, ExperienceNumber)
        currentTable.TableNumber = tableNumber
        currentTable.NumberOfCustomers = CType(objButton.NumberOfCustomers, Integer)

        ActiveOrder.Show()

    End Sub

    Private Sub btnAddTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTable.Click

        Dim tn As Integer
        tn = CType(txtAddTable.Text, Integer)

        InitializeAvailTable(tn)

    End Sub

    Private Sub InitializeAvailTable(ByVal tn As Integer)

    End Sub

    '    Private Function CreateTableList(ByVal empID As Integer, ByVal tableType As String)
    '        sql.cn.Open()
    '        If tableType Is "Avail" Then
    '            cmd = New SqlClient.SqlCommand("SELECT TableNumber From TablesAvail WHERE TableStatus < 5 AND EmployeeID = '" & empID & "'", sql.cn)
    '            dtr = cmd.ExecuteReader()'
    '
    '           While dtr.Read
    '   '               availTableList.Add(dtr.Item("TableNumber"))
    '          End While

    '        ElseIf tableType Is "Transfer" Then
    '            cmd = New SqlClient.SqlCommand("SELECT TableNumber From TablesAvail WHERE TableStatus = 5 and EmployeeID = '" & empID & "'", sql.cn)
    '        End If
    '        dtr.Close()
    '       sql.cn.Close()
    '  End Function
End Class
