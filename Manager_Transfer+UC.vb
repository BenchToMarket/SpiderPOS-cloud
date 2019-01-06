Imports DataSet_Builder


Public Class Manager_Transfer_UC
    Inherits System.Windows.Forms.UserControl

    '   Dim sql As New DataSet_Builder.SQLHelper(connectserver)

    Dim empIDTransferTo As Integer

    Private _transItemSelectedNumber As Integer
    Private _transItemName As String
    Private _transCheckNumber As Integer
    Private _transExpNumber As Int64
    Private _transItemFlag As Boolean
    Private _transCheckFlag As Boolean
    Private _transAllTableFlag As Boolean
    Private RestrictToItemOnly As Boolean

    Dim newStatus As Integer


    Friend Property TransItemSelectedNumber() As Integer
        Get
            Return _transItemSelectedNumber
        End Get
        Set(ByVal Value As Integer)
            _transItemSelectedNumber = Value
        End Set
    End Property

    Friend Property TransItemName() As String
        Get
            Return _transItemName
        End Get
        Set(ByVal Value As String)
            _transItemName = Value
        End Set
    End Property

    Friend Property TransCheckNumber() As Integer
        Get
            Return _transCheckNumber
        End Get
        Set(ByVal Value As Integer)
            _transCheckNumber = Value
        End Set
    End Property

    Friend Property TransExpNumber() As Int64
        Get
            Return _transExpNumber
        End Get
        Set(ByVal Value As Int64)
            _transExpNumber = Value
        End Set
    End Property

    Friend Property TransItemFlag() As Boolean
        Get
            Return _transItemFlag
        End Get
        Set(ByVal Value As Boolean)
            _transItemFlag = Value
        End Set
    End Property

    Friend Property TransCheckFlag() As Boolean
        Get
            Return _transCheckFlag
        End Get
        Set(ByVal Value As Boolean)
            _transCheckFlag = Value
        End Set
    End Property

    Friend Property TransAllTableFlag() As Boolean
        Get
            Return _transAllTableFlag
        End Get
        Set(ByVal Value As Boolean)
            _transAllTableFlag = Value
        End Set
    End Property

    Event UpdatingCurrentTables(ByVal releasingTable As Boolean)


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal itemSII As Integer, ByVal itemName As String, ByVal checkID As Integer, ByVal expNumber As Int64, ByVal itemFlag As Boolean, ByVal onlyItem As Boolean)
        MyBase.New()

        _transItemSelectedNumber = itemSII
        _transItemName = itemName

        RestrictToItemOnly = onlyItem

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        TestForOpenOrderID()

        _transItemFlag = itemFlag
        _transExpNumber = expNumber
        _transCheckNumber = checkID
        If RestrictToItemOnly = False Then
            If _transItemFlag = False Then
                _transCheckFlag = True
                UpdateCheckButton()
            Else
                UpdateItemButton()
            End If
        Else
            UpdateItemButton()
        End If

    End Sub

    Private Sub TestForOpenOrderID()

        Dim noOpenOrderID As Boolean = False
        Dim vRow As DataRowView

        For Each vRow In dvOrder 'dvClosingCheck
            If vRow("OpenOrderID") Is DBNull.Value Then
                noOpenOrderID = True
                Exit For
            End If
        Next

        If noOpenOrderID = True Then
            SaveOpenOrderData()
            DisposeDataViewsOrder()
            PopulateThisExperience(currentTable.ExperienceNumber, False)
            '444    CreateDataViewsOrder()
        End If

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents pnlMgrTransfer As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnTransItem As System.Windows.Forms.Button
    Friend WithEvents btnTransCheck As System.Windows.Forms.Button
    Friend WithEvents btnTransTable As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnTransServers As System.Windows.Forms.Button
    Friend WithEvents btnTransBar As System.Windows.Forms.Button
    Friend WithEvents btnTransManagers As System.Windows.Forms.Button
    Friend WithEvents btnTransTodaysFloor As System.Windows.Forms.Button
    Friend WithEvents btnTransAllFloor As System.Windows.Forms.Button
    Friend WithEvents lstTransNames As System.Windows.Forms.ListView
    Friend WithEvents TransTo As System.Windows.Forms.ColumnHeader
    Friend WithEvents TransEmpID As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnTransCancel As System.Windows.Forms.Button
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlMgrTransfer = New System.Windows.Forms.Panel
        Me.lstTransNames = New System.Windows.Forms.ListView
        Me.TransTo = New System.Windows.Forms.ColumnHeader
        Me.TransEmpID = New System.Windows.Forms.ColumnHeader
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnTransAllFloor = New System.Windows.Forms.Button
        Me.btnTransTodaysFloor = New System.Windows.Forms.Button
        Me.btnTransManagers = New System.Windows.Forms.Button
        Me.btnTransBar = New System.Windows.Forms.Button
        Me.btnTransServers = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnTransCheck = New System.Windows.Forms.Button
        Me.btnTransItem = New System.Windows.Forms.Button
        Me.btnTransTable = New System.Windows.Forms.Button
        Me.btnTransCancel = New System.Windows.Forms.Button
        Me.btnAccept = New System.Windows.Forms.Button
        Me.pnlMgrTransfer.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMgrTransfer
        '
        Me.pnlMgrTransfer.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pnlMgrTransfer.Controls.Add(Me.lstTransNames)
        Me.pnlMgrTransfer.Controls.Add(Me.Panel2)
        Me.pnlMgrTransfer.Controls.Add(Me.Panel1)
        Me.pnlMgrTransfer.Location = New System.Drawing.Point(32, 72)
        Me.pnlMgrTransfer.Name = "pnlMgrTransfer"
        Me.pnlMgrTransfer.Size = New System.Drawing.Size(736, 512)
        Me.pnlMgrTransfer.TabIndex = 0
        '
        'lstTransNames
        '
        Me.lstTransNames.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lstTransNames.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.TransTo, Me.TransEmpID})
        Me.lstTransNames.Font = New System.Drawing.Font("Bookman Old Style", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstTransNames.Location = New System.Drawing.Point(256, 16)
        Me.lstTransNames.Name = "lstTransNames"
        Me.lstTransNames.Size = New System.Drawing.Size(328, 480)
        Me.lstTransNames.TabIndex = 2
        Me.lstTransNames.View = System.Windows.Forms.View.Details
        '
        'TransTo
        '
        Me.TransTo.Text = "          Transferring To:"
        Me.TransTo.Width = 324
        '
        'TransEmpID
        '
        Me.TransEmpID.Text = ""
        Me.TransEmpID.Width = 0
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnTransAllFloor)
        Me.Panel2.Controls.Add(Me.btnTransTodaysFloor)
        Me.Panel2.Controls.Add(Me.btnTransManagers)
        Me.Panel2.Controls.Add(Me.btnTransBar)
        Me.Panel2.Controls.Add(Me.btnTransServers)
        Me.Panel2.Location = New System.Drawing.Point(32, 208)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(168, 288)
        Me.Panel2.TabIndex = 1
        '
        'btnTransAllFloor
        '
        Me.btnTransAllFloor.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnTransAllFloor.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransAllFloor.Location = New System.Drawing.Point(8, 232)
        Me.btnTransAllFloor.Name = "btnTransAllFloor"
        Me.btnTransAllFloor.Size = New System.Drawing.Size(152, 48)
        Me.btnTransAllFloor.TabIndex = 4
        Me.btnTransAllFloor.Text = "All Floor"
        '
        'btnTransTodaysFloor
        '
        Me.btnTransTodaysFloor.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnTransTodaysFloor.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransTodaysFloor.Location = New System.Drawing.Point(8, 176)
        Me.btnTransTodaysFloor.Name = "btnTransTodaysFloor"
        Me.btnTransTodaysFloor.Size = New System.Drawing.Size(152, 48)
        Me.btnTransTodaysFloor.TabIndex = 3
        Me.btnTransTodaysFloor.Text = "Today's Floor"
        '
        'btnTransManagers
        '
        Me.btnTransManagers.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnTransManagers.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransManagers.Location = New System.Drawing.Point(8, 120)
        Me.btnTransManagers.Name = "btnTransManagers"
        Me.btnTransManagers.Size = New System.Drawing.Size(152, 48)
        Me.btnTransManagers.TabIndex = 2
        Me.btnTransManagers.Text = "Managers"
        '
        'btnTransBar
        '
        Me.btnTransBar.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnTransBar.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransBar.Location = New System.Drawing.Point(8, 64)
        Me.btnTransBar.Name = "btnTransBar"
        Me.btnTransBar.Size = New System.Drawing.Size(152, 48)
        Me.btnTransBar.TabIndex = 1
        Me.btnTransBar.Text = "Bartenders"
        '
        'btnTransServers
        '
        Me.btnTransServers.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnTransServers.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransServers.Location = New System.Drawing.Point(8, 8)
        Me.btnTransServers.Name = "btnTransServers"
        Me.btnTransServers.Size = New System.Drawing.Size(152, 48)
        Me.btnTransServers.TabIndex = 0
        Me.btnTransServers.Text = "Servers"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnTransCheck)
        Me.Panel1.Controls.Add(Me.btnTransItem)
        Me.Panel1.Controls.Add(Me.btnTransTable)
        Me.Panel1.Location = New System.Drawing.Point(32, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(168, 176)
        Me.Panel1.TabIndex = 0
        '
        'btnTransCheck
        '
        Me.btnTransCheck.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnTransCheck.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransCheck.Location = New System.Drawing.Point(8, 64)
        Me.btnTransCheck.Name = "btnTransCheck"
        Me.btnTransCheck.Size = New System.Drawing.Size(152, 48)
        Me.btnTransCheck.TabIndex = 1
        Me.btnTransCheck.Text = "Check 1 of ..."
        '
        'btnTransItem
        '
        Me.btnTransItem.BackColor = System.Drawing.SystemColors.ControlLight
        Me.btnTransItem.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransItem.Location = New System.Drawing.Point(8, 8)
        Me.btnTransItem.Name = "btnTransItem"
        Me.btnTransItem.Size = New System.Drawing.Size(152, 48)
        Me.btnTransItem.TabIndex = 0
        Me.btnTransItem.Text = "Item: ..."
        '
        'btnTransTable
        '
        Me.btnTransTable.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnTransTable.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransTable.Location = New System.Drawing.Point(8, 120)
        Me.btnTransTable.Name = "btnTransTable"
        Me.btnTransTable.Size = New System.Drawing.Size(152, 48)
        Me.btnTransTable.TabIndex = 2
        Me.btnTransTable.Text = "Table"
        '
        'btnTransCancel
        '
        Me.btnTransCancel.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnTransCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransCancel.Location = New System.Drawing.Point(96, 16)
        Me.btnTransCancel.Name = "btnTransCancel"
        Me.btnTransCancel.Size = New System.Drawing.Size(104, 40)
        Me.btnTransCancel.TabIndex = 1
        Me.btnTransCancel.Text = "Cancel"
        '
        'btnAccept
        '
        Me.btnAccept.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnAccept.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAccept.Location = New System.Drawing.Point(360, 8)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(192, 56)
        Me.btnAccept.TabIndex = 2
        Me.btnAccept.Text = "Accept"
        '
        'Manager_Transfer_UC
        '
        Me.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Controls.Add(Me.btnAccept)
        Me.Controls.Add(Me.btnTransCancel)
        Me.Controls.Add(Me.pnlMgrTransfer)
        Me.Name = "Manager_Transfer_UC"
        Me.Size = New System.Drawing.Size(800, 600)
        Me.pnlMgrTransfer.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnTransItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransItem.Click

        MsgBox("Transfering an item is NOT allowed.")
        Exit Sub
        UpdateItemButton()

    End Sub

    Private Sub btnTransCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransCheck.Click
        If RestrictToItemOnly = False Then
            UpdateCheckButton()
        Else
            MsgBox(actingManager.FullName & " does not have Authorization")
        End If

    End Sub

    Private Sub UpdateItemButton()
        ResetColorFlagButtons()

        _transItemFlag = True
        Me.btnTransItem.BackColor = c6
        Me.btnTransItem.ForeColor = c3

        Me.btnTransItem.Text = "Item " & Me.TransItemName

    End Sub

    Private Sub UpdateCheckButton()
        ResetColorFlagButtons()

        _transCheckFlag = True
        Me.btnTransCheck.BackColor = c6
        Me.btnTransCheck.ForeColor = c3

        Me.btnTransCheck.Text = "Check " & Me.TransCheckNumber & " of " & currentTable.NumberOfChecks


    End Sub

    Private Sub btnTransTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransTable.Click
        If RestrictToItemOnly = False Then
            ResetColorFlagButtons()

            _transAllTableFlag = True
            Me.btnTransTable.BackColor = c6
            Me.btnTransTable.ForeColor = c3
        Else
            MsgBox(actingManager.FullName & " does not have Authorization")
        End If

    End Sub

    Private Sub btnTransServers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransServers.Click

        PopulateServerCollection(currentServers)

        Me.lstTransNames.Items.Clear()
        ResetColorPersonnelButtons()
        Me.btnTransServers.BackColor = c6
        Me.btnTransServers.ForeColor = c3

        PopulateTransListView(currentServers)

    End Sub

    Private Sub btnTransBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransBar.Click

        PopulateServerCollection(currentBartenders)

        Me.lstTransNames.Items.Clear()
        ResetColorPersonnelButtons()
        Me.btnTransBar.BackColor = c6
        Me.btnTransBar.ForeColor = c3
        PopulateTransListView(currentBartenders)

    End Sub

    Private Sub btnTransManagers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransManagers.Click

        PopulateServerCollection(currentManagers)

        Me.lstTransNames.Items.Clear()
        ResetColorPersonnelButtons()
        Me.btnTransManagers.BackColor = c6
        Me.btnTransManagers.ForeColor = c3

        PopulateTransListView(currentManagers)

    End Sub

    Private Sub btnTransTodaysFloor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransTodaysFloor.Click

        PopulateServerCollection(todaysFloorPersonnel)

        Me.lstTransNames.Items.Clear()
        ResetColorPersonnelButtons()
        Me.btnTransTodaysFloor.BackColor = c6
        Me.btnTransTodaysFloor.ForeColor = c3
        PopulateTransListView(todaysFloorPersonnel)

    End Sub


    Private Sub ResetColorFlagButtons()

        Me.btnTransItem.BackColor = c10
        Me.btnTransCheck.BackColor = c10
        Me.btnTransTable.BackColor = c10
        Me.btnTransItem.ForeColor = c2
        Me.btnTransCheck.ForeColor = c2
        Me.btnTransTable.ForeColor = c2

        _transItemFlag = False
        _transCheckFlag = False
        _transAllTableFlag = False
        Me.btnTransItem.Text = "Item"
        Me.btnTransCheck.Text = "Check"


    End Sub

    Private Sub ResetColorPersonnelButtons()

        Me.btnTransServers.BackColor = c10
        Me.btnTransBar.BackColor = c10
        Me.btnTransManagers.BackColor = c10
        Me.btnTransTodaysFloor.BackColor = c10
        Me.btnTransAllFloor.BackColor = c10
        Me.btnTransServers.ForeColor = c2
        Me.btnTransBar.ForeColor = c2
        Me.btnTransManagers.ForeColor = c2
        Me.btnTransTodaysFloor.ForeColor = c2
        Me.btnTransAllFloor.ForeColor = c2

    End Sub

    Private Sub PopulateTransListView(ByRef tempEmpCollection As EmployeeCollection)
        Dim emp As Employee

        If tempEmpCollection.Count > 15 Then
            Me.TransTo.Width = 308
        End If

        For Each emp In tempEmpCollection
            Dim itemEmployee As New ListViewItem

            '       If emp.EmployeeID <> currentTable.EmployeeID Then
            itemEmployee.Text = emp.FullName
            itemEmployee.SubItems.Add(emp.EmployeeID)
            Me.lstTransNames.Items.Add(itemEmployee)
            '      End If
        Next

    End Sub

    Private Sub DetermineTranseree(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTransNames.SelectedIndexChanged

        Dim itemView As ListView
        Dim item As ListViewItem

        itemView = sender

        For Each item In itemView.Items
            If item.Selected = True Then
                empIDTransferTo = item.SubItems(1).Text
                Exit For
            End If
        Next

    End Sub

    Private Sub ChangeItemStatusPriorTrans(ByVal empID As Integer, ByVal expNum As Int64)
        Dim oRow As DataRowView
        '    Dim remainingChecks As Boolean
        Dim iPromo As ItemPromoInfo
        Dim ni As SelectedItemDetail

        iPromo.PromoCode = 8
        iPromo.PromoReason = 9  'kitch mistake, need to change to a GUI choice

        Try
            '        sql.cn.Open()
            '           sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            If Me.TransItemFlag = True Then
                For Each oRow In dvOrder            'dsOrder.Tables("OpenOrders").Rows
                    'changed 1 line below for dataview
                    If Not oRow.Row.RowState = DataRowState.Deleted And Not oRow.Row.RowState = DataRowState.Detached Then
                        If oRow("sii") = Me.TransItemSelectedNumber Then
                            If Not oRow("ForceFreeID") = 9 Or Not oRow("ForceFreeID") = 8 And Not oRow("ForceFreeID") = -9 And Not oRow("ForceFreeID") = -8 Then
                                'ALREADY comp'd or transfered
                                '            AddTransferedItemsToOpenOrder(oRow, empID, expNum)
                                '    oRow("CheckNumber") = 1

                                GenerateOrderTables.CopyViewForTransferItem(oRow, empID, expNum, True, 1)

                                iPromo.empID = actingManager.EmployeeID
                                iPromo.PromoName = "   ** Transfer: " & oRow("TerminalName")
                                iPromo.ItemID = oRow("ItemID")
                                iPromo.Quantity = (oRow("Quantity") * -1)
                                iPromo.InvMultiplier = oRow("OpenDecimal1")
                                iPromo.ItemPrice = oRow("ItemPrice")
                                iPromo.Price = oRow("Price")
                                iPromo.TaxPrice = oRow("TaxPrice")
                                iPromo.SinTax = oRow("SinTax")

                                iPromo.FunctionID = oRow("FunctionID")
                                iPromo.FunctionGroup = oRow("FunctionGroupID")
                                iPromo.FunctionFlag = oRow("FunctionFlag")
                                iPromo.CategoryID = oRow("CategoryID")
                                iPromo.FoodID = oRow("FoodID")
                                iPromo.DrinkCategoryID = oRow("DrinkCategoryID")
                                iPromo.DrinkID = oRow("DrinkID")
                                iPromo.RoutingID = oRow("RoutingID")
                                iPromo.PrintPriorityID = oRow("PrintPriorityID")

                                iPromo.ItemStatus = 8
                                oRow("ForceFreeID") = -8
                                oRow("ForceFreeAuth") = empID   'this is who we transfered to
                                oRow("ItemStatus") = 8

                                iPromo.CheckNumber = TransCheckNumber '1  'oRow("CheckNumber")
                                iPromo.CustomerNumber = oRow("CustomerNumber")
                                iPromo.CourseNumber = oRow("CourseNumber")

                                iPromo.openOrderID = oRow("OpenOrderID")
                                iPromo.taxID = oRow("TaxID")
                                iPromo.sii = oRow("sii")
                                iPromo.si2 = oRow("si2")

                                CompThisItem(iPromo)

                            End If
                        End If
                    End If
                Next

            ElseIf Me.TransCheckFlag = True Then
                For Each oRow In dvOrder             'dsOrder.Tables("OpenOrders").Rows
                    If Not oRow.Row.RowState = DataRowState.Deleted And Not oRow.Row.RowState = DataRowState.Detached Then

                        If oRow("CheckNumber") = Me.TransCheckNumber Then
                            If Not oRow("ForceFreeID") = 9 And Not oRow("ForceFreeID") = 8 And Not oRow("ForceFreeID") = -9 And Not oRow("ForceFreeID") = -8 Then
                                'ALREADY comp'd or transfered
                                '      AddTransferedItemsToOpenOrder(oRow, empID, expNum)

                                '        oRow("CheckNumber") = 1
                                GenerateOrderTables.CopyViewForTransferItem(oRow, empID, expNum, True, 1)


                                iPromo.empID = actingManager.EmployeeID
                                iPromo.PromoName = "   ** Transfer: " & oRow("TerminalName")
                                iPromo.ItemID = oRow("ItemID")
                                iPromo.Quantity = (oRow("Quantity") * -1)
                                iPromo.InvMultiplier = oRow("OpenDecimal1")
                                iPromo.ItemPrice = oRow("ItemPrice")
                                iPromo.Price = oRow("Price")
                                iPromo.TaxPrice = oRow("TaxPrice")
                                iPromo.SinTax = oRow("SinTax")

                                iPromo.FunctionID = oRow("FunctionID")
                                iPromo.FunctionGroup = oRow("FunctionGroupID")
                                iPromo.FunctionFlag = oRow("FunctionFlag")
                                iPromo.CategoryID = oRow("CategoryID")
                                iPromo.FoodID = oRow("FoodID")
                                iPromo.DrinkCategoryID = oRow("DrinkCategoryID")
                                iPromo.DrinkID = oRow("DrinkID")
                                iPromo.RoutingID = oRow("RoutingID")
                                iPromo.PrintPriorityID = oRow("PrintPriorityID")

                                iPromo.ItemStatus = 8
                                oRow("ForceFreeID") = -8
                                oRow("ForceFreeAuth") = empID
                                oRow("ItemStatus") = 8

                                iPromo.CheckNumber = TransCheckNumber  '1  'oRow("CheckNumber")
                                iPromo.CustomerNumber = oRow("CustomerNumber")
                                iPromo.CourseNumber = oRow("CourseNumber")

                                iPromo.openOrderID = oRow("OpenOrderID")
                                iPromo.taxID = oRow("TaxID")

                                If oRow("sii") < oRow("sin") Then   ' <> valueSIN Then
                                    'we populate openOrder we increase sin by 1, now they equal
                                    If currentTable.ReferenceSIN = 0 Then
                                        currentTable.ReferenceSIN = currentTable.SIN + 1
                                    End If
                                Else
                                    currentTable.ReferenceSIN = currentTable.SIN + 1
                                End If
                                iPromo.sii = currentTable.ReferenceSIN
                                iPromo.si2 = oRow("si2")

                                CompThisItem(iPromo)

                            End If
                        End If

                    End If
                Next

            ElseIf Me.TransAllTableFlag = True Then
                For Each oRow In dvOrder             'dsOrder.Tables("OpenOrders").Rows
                    If Not oRow.Row.RowState = DataRowState.Deleted And Not oRow.Row.RowState = DataRowState.Detached Then
                        If oRow("ExperienceNumber") = Me.TransExpNumber Then
                            If Not oRow("ForceFreeID") = 9 And Not oRow("ForceFreeID") = 8 And Not oRow("ForceFreeID") = -9 And Not oRow("ForceFreeID") = -8 Then
                                'ALREADY comp'd or transfered
                                '        AddTransferedItemsToOpenOrder(oRow, empID, expNum)

                                GenerateOrderTables.CopyViewForTransferItem(oRow, empID, expNum, True, Nothing)

                                iPromo.empID = actingManager.EmployeeID
                                iPromo.PromoName = "   ** Transfer: " & oRow("TerminalName")
                                iPromo.ItemID = oRow("ItemID")
                                iPromo.Quantity = (oRow("Quantity") * -1)
                                iPromo.InvMultiplier = oRow("OpenDecimal1")
                                iPromo.ItemPrice = oRow("ItemPrice")
                                iPromo.Price = oRow("Price")
                                iPromo.TaxPrice = oRow("TaxPrice")
                                iPromo.SinTax = oRow("SinTax")
                                iPromo.FunctionID = oRow("FunctionID")
                                iPromo.FunctionGroup = oRow("FunctionGroupID")
                                iPromo.FunctionFlag = oRow("FunctionFlag")
                                iPromo.CategoryID = oRow("CategoryID")
                                iPromo.FoodID = oRow("FoodID")
                                iPromo.DrinkCategoryID = oRow("DrinkCategoryID")
                                iPromo.DrinkID = oRow("DrinkID")
                                iPromo.RoutingID = oRow("RoutingID")
                                iPromo.PrintPriorityID = oRow("PrintPriorityID")

                                iPromo.ItemStatus = 8
                                oRow("ForceFreeID") = -8
                                oRow("ForceFreeAuth") = empID
                                oRow("ItemStatus") = 8

                                iPromo.CheckNumber = oRow("CheckNumber")
                                iPromo.CustomerNumber = oRow("CustomerNumber")
                                iPromo.CourseNumber = oRow("CourseNumber")

                                iPromo.openOrderID = oRow("OpenOrderID")
                                iPromo.taxID = oRow("TaxID")


                                If oRow("sii") < oRow("sin") Then   ' <> valueSIN Then
                                    'we populate openOrder we increase sin by 1, now they equal
                                    If currentTable.ReferenceSIN = 0 Then
                                        currentTable.ReferenceSIN = currentTable.SIN + 1
                                    End If
                                Else
                                    currentTable.ReferenceSIN = currentTable.SIN + 1
                                End If
                                iPromo.sii = currentTable.ReferenceSIN
                                iPromo.si2 = oRow("si2")

                                CompThisItem(iPromo)

                            End If
                        End If
                    End If
                Next
            End If

            '        sql.cn.Close()
            For Each ni In newItemCollection
                GenerateOrderTables.PopulateDataRowForOpenOrder(ni)
            Next
            newItemCollection.Clear()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
            newItemCollection.Clear()
        End Try

    End Sub

    Private Sub UpdateTramsferCheckExperience222(ByVal expNum As Int64)

        Dim oRow As DataRow
        '        Dim bRow As DataRow

        If currentTable.IsTabNotTable = True Then
            For Each oRow In dsOrder.Tables("AvailTabs").Rows
                If oRow("ExperienceNumber") = expNum Then
                    oRow("LastStatusTime") = Now
                    oRow("TabName") = "Transf " & oRow("TabName")
                    oRow("LastStatus") = 1
                    oRow("ClosedSubTotal") = 0
                End If
            Next
            '           bRow = (dsBackup.Tables("AvailTabsTerminal").Rows.Find(expNum))
            '           If Not (bRow Is Nothing) Then
            '           bRow("LastStatus") = 1
            '          bRow("LastStatusTime") = Now
            ''          bRow("TabName") = "Transf " & oRow("TabName")
            '    End If
        Else
            For Each oRow In dsOrder.Tables("AvailTables").Rows
                If oRow("ExperienceNumber") = expNum Then
                    oRow("LastStatusTime") = Now
                    oRow("TabName") = "Transf " & oRow("TabName")
                    oRow("LastStatus") = 1
                    oRow("ClosedSubTotal") = 0
                End If
            Next
            '       bRow = (dsBackup.Tables("AvailTablesTerminal").Rows.Find(expNum))
            '       If Not (bRow Is Nothing) Then
            ''          bRow("LastStatus") = 1
            '          bRow("LastStatusTime") = Now
            '         bRow("TabName") = "Transf " & oRow("TabName")
            '    End If
        End If

    End Sub

    Private Sub AddTransferedItemsToOpenOrder222(ByRef oRow As DataRow, ByVal empID As Integer, ByVal expNum As Int64)
        '   in OpenOrders make duplicate all orders for check(s) with TransferedTo flag TRUE

        '   ***************************
        '   *** this is wrong
        '       should look something like  GenerateOrderTables.AddItemViewToOpenOrders
        '       we changed the database schematic

        sql.SqlInsertCommandOpenOrdersSP.Parameters("@CompanyID").Value = oRow("CompanyID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@LocationID").Value = oRow("LocationID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@DailyCode").Value = oRow("DailyCode")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ExperienceNumber").Value = expNum
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@OrderNumber").Value = oRow("OrderNumber")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ShiftID").Value = oRow("ShiftID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@MenuID").Value = oRow("MenuID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@EmployeeID").Value = empID
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@TableNumber").Value = oRow("TableNumber")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@TabID").Value = oRow("TabID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@TabName").Value = oRow("TabName")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@CheckNumber").Value = oRow("CheckNumber")   '??
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@CustomerNumber").Value = oRow("CustomerNumber")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@CourseNumber").Value = oRow("CourseNumber")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@sin").Value = oRow("sin")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@sii").Value = oRow("sii")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@Quantity").Value = oRow("Quantity")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ItemID").Value = oRow("ItemID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ItemName").Value = oRow("ItemName")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@Price").Value = oRow("Price")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@TaxPrice").Value = oRow("TaxPrice")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@TaxID").Value = oRow("TaxID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ForceFreeID").Value = oRow("ForceFreeID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ForceFreeAuth").Value = oRow("ForceFreeAuth")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ForceFreeCode").Value = oRow("ForceFreeCode")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@FunctionID").Value = oRow("FunctionID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@FunctionGroupID").Value = oRow("FunctionGroupID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@FunctionFlag").Value = oRow("FunctionFlag")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@CategoryID").Value = oRow("CategoryID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@FoodID").Value = oRow("FoodID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@DrinkCategoryID").Value = oRow("DrinkCategoryID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@DrinkID").Value = oRow("DrinkID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ItemStatus").Value = oRow("ItemStatus") '9
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@RoutingID").Value = oRow("RoutingID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@PrintPriorityID").Value = oRow("PrintPriorityID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@Repeat").Value = 1  'oRow("Repeat")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@TerminalID").Value = oRow("TerminalID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@dbUP").Value = 1

        '     sql.cn.Open()
        '          sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        sql.SqlInsertCommandOpenOrdersSP.ExecuteNonQuery()
        '   sql.cn.Close()

    End Sub

    Private Function AddTransferedExperience(ByVal empID As Integer)

        Dim transferExpNumber As Int64
        Dim oRow As DataRowView
        Dim loginTrackID As Int64
        Dim emp As Employee
        '   empID is for the new employee

        'with Quick Tickets, we will always transfer to 
        'new experience.. therefore no need for quick ticket routine here

        '   first determine if we have experience number for this transfer
        If currentTable.IsTabNotTable = True Then
            For Each oRow In dvAvailTabs    'dsOrder.Tables("AvailTabs").Rows
                If oRow("EmployeeID") = empID Then
                    If oRow("TabID") = currentTable.TabID Then
                        transferExpNumber = oRow("ExperienceNumber")
                    End If
                End If
            Next
        Else
            For Each oRow In dvAvailTables  'dsOrder.Tables("AvailTables").Rows
                If oRow("EmployeeID") = empID Then
                    If oRow("TableNumber") = currentTable.TableNumber Then
                        transferExpNumber = oRow("ExperienceNumber")
                    End If
                End If
            Next
        End If

        For Each emp In AllEmployees
            If emp.EmployeeID = empID Then
                loginTrackID = emp.LoginTrackingID
                Exit For
            End If
        Next
        If loginTrackID = Nothing Or loginTrackID = 0 Then
            loginTrackID = currentServer.LoginTrackingID
        End If

        If transferExpNumber = 0 Then
            transferExpNumber = CreateNewExperience(empID, currentTable.TableNumber, currentTable.TabID, currentTable.TabName, currentTable.NumberOfCustomers, 8, 0, currentTable.ItemsOnHold, loginTrackID)
        Else
            currentTable.NumberOfChecks += 1
            AddOneMoreCheckNumber(transferExpNumber)
        End If

        Return transferExpNumber

    End Function

    Private Sub AddOneMoreCheckNumber(ByVal expNum As Int64)
        Dim oRow As DataRow
     
        'with Quick Tickets, we will always transfer to 
        'new experience.. therefore no need for quick ticket routine here

        If currentTable.IsTabNotTable = True Then
            For Each oRow In dsOrder.Tables("AvailTabs").Rows
                If oRow("ExperienceNumber") = expNum Then
                    oRow("NumberOfChecks") += 1
                End If
            Next
        Else
            For Each oRow In dsOrder.Tables("AvailTables").Rows
                If oRow("ExperienceNumber") = expNum Then
                    oRow("NumberOfChecks") += 1
                End If
            Next
        End If

        '   possible change 
        '************ ???? only need to update experience table
        '   probably do not need at all (will do anyway next step)
        '       UpdateOpenOrdersAfterTransfer()

    End Sub

    Private Sub btnTransCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransCancel.Click
        Me.Dispose()

    End Sub



    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click

        If empIDTransferTo = 0 Then Exit Sub
        Dim transferExpNumber As Int64

        '   check to determine if any other check # with this table# has been transfered
        '   if not create another experience number
        transferExpNumber = AddTransferedExperience(empIDTransferTo)

        ChangeItemStatusPriorTrans(empIDTransferTo, transferExpNumber)

        If typeProgram = "Online_Demo" Then
            If currentTable.IsTabNotTable = False Then
                dsOrderDemo.Merge(dsOrder.Tables("AvailTables"), False, MissingSchemaAction.Add)
            Else
                dsOrderDemo.Merge(dsOrder.Tables("AvailTabs"), False, MissingSchemaAction.Add)
            End If

            dsOrder.Tables("AvailTables").Clear()
            dsOrder.Tables("AvailTabs").Clear()

            Dim filterString As String
            Dim NotfilterString As String
            filterString = "EmployeeID = " & currentTable.EmployeeID
            NotfilterString = "NOT EmployeeID = " & currentTable.EmployeeID
            Demo_FilterDemoDataTabble(dsOrderDemo.Tables("AvailTables"), dsOrder.Tables("AvailTables"), filterString, NotfilterString)
            Demo_FilterDemoDataTabble(dsOrderDemo.Tables("AvailTabs"), dsOrder.Tables("AvailTabs"), filterString, NotfilterString)
        End If

        '      If remainingChecks = False Then
        If _transCheckFlag = True Then
            currentTable.NumberOfChecks -= 1
            If currentTable.NumberOfChecks = 0 Then
                RaiseEvent UpdatingCurrentTables(True)
                Exit Sub
            End If
        End If
        If Me._transAllTableFlag = True Then
            RaiseEvent UpdatingCurrentTables(True)
            Exit Sub
        End If
        'only if we have checks or items left
        RaiseEvent UpdatingCurrentTables(False)

    End Sub

    Private Sub btnTransAllFloor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransAllFloor.Click

        Me.lstTransNames.Items.Clear()
        ResetColorPersonnelButtons()
        Me.btnTransTodaysFloor.BackColor = c6
        Me.btnTransTodaysFloor.ForeColor = c3
        PopulateTransListView(allFloorPersonnel)

    End Sub
End Class
