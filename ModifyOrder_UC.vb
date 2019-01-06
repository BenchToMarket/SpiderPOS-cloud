Imports DataSet_Builder

Public Class ModifyOrder_UC
    Inherits System.Windows.Forms.UserControl

    '   Dim sql As New DataSet_Builder.SQLHelper(connectserver)

    Private dvModifyOrder As DataView
    Private _currentItemID As Integer
    Private _currentPrice As Decimal
    Dim newItemIsFree As Boolean
    Dim _funID As Integer
    Dim _funFlag As String
    Dim modifyingOrderGroup As Boolean = False

    Friend isFoodItem As Boolean
    Friend ModifyCurrentSIN As Integer
    Friend ModifyCurrentSII As Integer
    Friend ModifyCurrentName As String
    Friend ModifyCurrentChitName As String

    Dim _modifyItemID As Integer
    Dim _modifyItemName As String
    Dim _modifyAbrevName As String
    Dim _modifyChitName As String
    Dim _modifySurcharge As Decimal
    Dim _modifyTaxId As Integer
    Dim _modifyRoutingID As Integer
    Dim _modifyPrepareTime As Integer           '?????????????????
    Dim _modifyCategoryID As Integer
    Dim _modifySIN As Integer

    Dim _modifyCustomerNumber As Integer
    Dim _modifyQuantity As Integer
    Dim _modifyCourse As Integer
    Dim _alreadyOrdered As Boolean

    Dim btnModifyQuantity(10) As KitchenButton
    Dim btnModifyCustomer(10) As KitchenButton
    Dim btnModifyCourse(5) As KitchenButton

    Event AddingItemToOrder(ByVal sender As Object)

    Friend Property ModifyItemID() As Integer
        Get
            Return _modifyItemID
        End Get
        Set(ByVal Value As Integer)
            _modifyItemID = Value
        End Set
    End Property

    Friend Property ModifyItemName() As String
        Get
            Return _modifyItemName
        End Get
        Set(ByVal Value As String)
            _modifyItemName = Value
        End Set
    End Property

    Friend Property ModifyAbrevName() As String
        Get
            Return _modifyAbrevName
        End Get
        Set(ByVal Value As String)
            _modifyAbrevName = Value
        End Set
    End Property

    Friend Property ModifyChitName() As String
        Get
            Return _modifyChitName
        End Get
        Set(ByVal Value As String)
            _modifyChitName = Value
        End Set
    End Property

    Friend Property ModifySurcharge() As Decimal
        Get
            Return _modifySurcharge
        End Get
        Set(ByVal Value As Decimal)
            _modifySurcharge = Value
        End Set
    End Property

    Friend Property ModifyTaxID() As Integer
        Get
            Return _modifyTaxId
        End Get
        Set(ByVal Value As Integer)
            _modifyTaxId = Value
        End Set
    End Property

    Friend Property ModifyRoutingID() As Integer
        Get
            Return _modifyRoutingID
        End Get
        Set(ByVal Value As Integer)
            _modifyRoutingID = Value
        End Set
    End Property

    Friend Property ModifyPrepareTime() As Integer
        Get
            Return _modifyPrepareTime
        End Get
        Set(ByVal Value As Integer)
            _modifyPrepareTime = Value
        End Set
    End Property

    Friend Property ModifyCategoryID() As Integer
        Get
            Return _modifyCategoryID
        End Get
        Set(ByVal Value As Integer)
            _modifyCategoryID = Value
        End Set
    End Property

    Friend Property ModifySIN() As Integer
        Get
            Return _modifySIN
        End Get
        Set(ByVal Value As Integer)
            _modifySIN = Value
        End Set
    End Property

    Friend Property ModifyQuantity() As Integer
        Get
            Return _modifyQuantity
        End Get
        Set(ByVal Value As Integer)
            _modifyQuantity = Value
        End Set
    End Property

    Friend Property ModifyCustomerNumber() As Integer
        Get
            Return _modifyCustomerNumber
        End Get
        Set(ByVal Value As Integer)
            _modifyCustomerNumber = Value
        End Set

    End Property

    Friend Property ModifyCourse() As Integer
        Get
            Return _modifyCourse
        End Get
        Set(ByVal Value As Integer)
            _modifyCourse = Value
        End Set
    End Property


    Event AcceptModify()
    Event AcceptModifySubTotal()
    Event CancelModify()



#Region " Windows Form Designer generated code "

    Public Sub New(ByVal currentSII As Integer, ByVal currentSIN As Integer, ByVal currentTerminalName As String, ByVal currentName As String, ByVal currentChitName As String, ByRef dvModify As DataView, ByVal isFood As Boolean, ByVal currentItemID As Integer, ByVal currentPrice As Decimal, ByVal funID As Integer, ByVal funFlag As String, ByVal cn As Integer, ByVal q As Integer, ByVal c As Integer, ByVal alrOrdered As Boolean)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        dvModifyOrder = dvModify
        isFoodItem = isFood
        _currentItemID = currentItemID
        _currentPrice = currentPrice
        ModifyCurrentSIN = currentSIN
        ModifyCurrentSII = currentSII
        ModifyCurrentName = currentName
        ModifyAbrevName = currentTerminalName
        ModifyCurrentChitName = currentChitName
        _funID = funID
        _funFlag = funFlag
        _modifyQuantity = q
        _modifyCustomerNumber = cn
        _modifyCourse = c
        If ModifyCurrentSIN = ModifyCurrentSII Then
            modifyingOrderGroup = True
        Else : modifyingOrderGroup = False
        End If
        _alreadyOrdered = alrOrdered

        CreateModifyButtonPanel()
        If _alreadyOrdered = True Then
            Me.pnlCourse.Enabled = False
            Me.pnlQuantity.Enabled = False
            Me.lblModifyOld.Text = "Can Only Modify Customer"
        Else
            PopulateModifyListView()
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstModify As System.Windows.Forms.ListView
    Friend WithEvents ModifyName As System.Windows.Forms.ColumnHeader
    Friend WithEvents ModifyID As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnModifyCancel As System.Windows.Forms.Button
    Friend WithEvents btnModifyAccept As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblModifyOld As System.Windows.Forms.Label
    Friend WithEvents lblModifyNew As System.Windows.Forms.Label
    Friend WithEvents pnlQuantity As System.Windows.Forms.Panel
    Friend WithEvents pnlCustomer As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CategoryID As System.Windows.Forms.ColumnHeader
    Friend WithEvents SelectedItemSIN As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlCourse As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lstModify = New System.Windows.Forms.ListView
        Me.ModifyName = New System.Windows.Forms.ColumnHeader
        Me.ModifyID = New System.Windows.Forms.ColumnHeader
        Me.CategoryID = New System.Windows.Forms.ColumnHeader
        Me.SelectedItemSIN = New System.Windows.Forms.ColumnHeader
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblModifyOld = New System.Windows.Forms.Label
        Me.btnModifyCancel = New System.Windows.Forms.Button
        Me.btnModifyAccept = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblModifyNew = New System.Windows.Forms.Label
        Me.pnlQuantity = New System.Windows.Forms.Panel
        Me.pnlCustomer = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.pnlCourse = New System.Windows.Forms.Panel
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightSlateGray
        Me.Panel1.Controls.Add(Me.lstModify)
        Me.Panel1.Location = New System.Drawing.Point(472, 48)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(280, 504)
        Me.Panel1.TabIndex = 0
        '
        'lstModify
        '
        Me.lstModify.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ModifyName, Me.ModifyID, Me.CategoryID, Me.SelectedItemSIN})
        Me.lstModify.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstModify.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstModify.Location = New System.Drawing.Point(16, 16)
        Me.lstModify.Name = "lstModify"
        Me.lstModify.Size = New System.Drawing.Size(248, 472)
        Me.lstModify.TabIndex = 0
        Me.lstModify.View = System.Windows.Forms.View.Details
        '
        'ModifyName
        '
        Me.ModifyName.Text = ""
        Me.ModifyName.Width = 240
        '
        'ModifyID
        '
        Me.ModifyID.Text = ""
        Me.ModifyID.Width = 0
        '
        'CategoryID
        '
        Me.CategoryID.Width = 0
        '
        'SelectedItemSIN
        '
        Me.SelectedItemSIN.Width = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(16, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 32)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Modify:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblModifyOld
        '
        Me.lblModifyOld.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModifyOld.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblModifyOld.Location = New System.Drawing.Point(104, 16)
        Me.lblModifyOld.Name = "lblModifyOld"
        Me.lblModifyOld.Size = New System.Drawing.Size(160, 32)
        Me.lblModifyOld.TabIndex = 2
        Me.lblModifyOld.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnModifyCancel
        '
        Me.btnModifyCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModifyCancel.Location = New System.Drawing.Point(56, 552)
        Me.btnModifyCancel.Name = "btnModifyCancel"
        Me.btnModifyCancel.Size = New System.Drawing.Size(144, 48)
        Me.btnModifyCancel.TabIndex = 3
        Me.btnModifyCancel.Text = "Cancel"
        '
        'btnModifyAccept
        '
        Me.btnModifyAccept.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModifyAccept.Location = New System.Drawing.Point(232, 552)
        Me.btnModifyAccept.Name = "btnModifyAccept"
        Me.btnModifyAccept.Size = New System.Drawing.Size(152, 48)
        Me.btnModifyAccept.TabIndex = 4
        Me.btnModifyAccept.Text = "Accept"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label3.Location = New System.Drawing.Point(56, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 32)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "To:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblModifyNew
        '
        Me.lblModifyNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModifyNew.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblModifyNew.Location = New System.Drawing.Point(120, 56)
        Me.lblModifyNew.Name = "lblModifyNew"
        Me.lblModifyNew.Size = New System.Drawing.Size(192, 32)
        Me.lblModifyNew.TabIndex = 6
        Me.lblModifyNew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlQuantity
        '
        Me.pnlQuantity.BackColor = System.Drawing.Color.LightSlateGray
        Me.pnlQuantity.Location = New System.Drawing.Point(24, 152)
        Me.pnlQuantity.Name = "pnlQuantity"
        Me.pnlQuantity.Size = New System.Drawing.Size(392, 112)
        Me.pnlQuantity.TabIndex = 7
        '
        'pnlCustomer
        '
        Me.pnlCustomer.BackColor = System.Drawing.Color.LightSlateGray
        Me.pnlCustomer.Location = New System.Drawing.Point(24, 312)
        Me.pnlCustomer.Name = "pnlCustomer"
        Me.pnlCustomer.Size = New System.Drawing.Size(392, 112)
        Me.pnlCustomer.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(16, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Quantity"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label4.Location = New System.Drawing.Point(16, 280)
        Me.Label4.Name = "Label4"
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Customer"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label5.Location = New System.Drawing.Point(16, 440)
        Me.Label5.Name = "Label5"
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Course"
        '
        'pnlCourse
        '
        Me.pnlCourse.BackColor = System.Drawing.Color.LightSlateGray
        Me.pnlCourse.Location = New System.Drawing.Point(24, 472)
        Me.pnlCourse.Name = "pnlCourse"
        Me.pnlCourse.Size = New System.Drawing.Size(392, 56)
        Me.pnlCourse.TabIndex = 12
        '
        'ModifyOrder_UC
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(59, Byte), CType(96, Byte), CType(141, Byte))
        Me.Controls.Add(Me.pnlCourse)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pnlQuantity)
        Me.Controls.Add(Me.lblModifyNew)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnModifyAccept)
        Me.Controls.Add(Me.btnModifyCancel)
        Me.Controls.Add(Me.lblModifyOld)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlCustomer)
        Me.Name = "ModifyOrder_UC"
        Me.Size = New System.Drawing.Size(784, 616)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub PopulateModifyListView()

        Me.lstModify.Items.Clear()

        Dim vRow As DataRowView

        If isFoodItem = True Then

            For Each vRow In dvModifyOrder


                If modifyingOrderGroup = True Then
                    '      If vRow("ItemID") = _currentItemID Then
                    If vRow("sin") = ModifyCurrentSIN Then
                        lblModifyOld.Text = vRow("ItemName")
                        Exit Sub
                    End If
                Else
                    If vRow("FoodID") = _currentItemID Then
                        lblModifyOld.Text = vRow("FoodName")
                        If _currentPrice = 0 Then
                            'test to see if Food usually has price

                            newItemIsFree = IsItemIncluded(vRow("FoodID"), _funID, vRow("CategoryID"))
                        End If
                    End If
                End If

                If vRow("FoodID") <> _currentItemID Or modifyingOrderGroup = True Then

                    Dim modifyItem As New ListViewItem


                    If modifyingOrderGroup = True Then
                        '   not populating for main item
                        '                  modifyItem.Text = vRow("ItemName")
                        '                  modifyItem.SubItems.Add(vRow("ItemID"))
                        '                  modifyItem.SubItems.Add(vRow("CategoryID"))
                        '                  modifyItem.SubItems.Add(vRow("sin"))
                    Else
                        modifyItem.Text = vRow("FoodName")
                        modifyItem.SubItems.Add(vRow("FoodID"))
                        modifyItem.SubItems.Add(vRow("CategoryID"))
                        modifyItem.SubItems.Add(0)
                    End If

                    Me.lstModify.Items.Add(modifyItem)
                End If
            Next
        Else            'for drink item

            For Each vRow In dvModifyOrder

                If vRow("DrinkID") = _currentItemID Then
                    If modifyingOrderGroup = True Then

                        lblModifyOld.Text = vRow("ItemName")
                    Else
                        lblModifyOld.Text = vRow("DrinkName")
                    End If

                    If _currentPrice = 0 Then
                        'test to see if Drink usually has price
                        '222 not sure about DrinkCategoryID...
                        newItemIsFree = IsItemIncluded(vRow("DrinkID"), _funID, vRow("DrinkCategoryID"))
                    End If
                    Exit Sub
                Else
                    Dim modifyItem As New ListViewItem


                    If modifyingOrderGroup = True Then
                        '                 modifyItem.Text = vRow("ItemName")
                        '                 modifyItem.SubItems.Add(vRow("ItemID"))
                        '                 modifyItem.SubItems.Add(vRow("DrinkCategoryID"))
                        '                 modifyItem.SubItems.Add(vRow("sin"))
                    Else
                        modifyItem.Text = vRow("DrinkName")
                        modifyItem.SubItems.Add(vRow("DrinkID"))
                        modifyItem.SubItems.Add(vRow("DrinkCategoryID"))
                        modifyItem.SubItems.Add(0)
                    End If
                    Me.lstModify.Items.Add(modifyItem)
                End If
            Next
        End If
    End Sub

    Private Sub ModifyListView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstModify.SelectedIndexChanged

        Dim vRow As DataRowView
        Dim sItem As ListViewItem

        For Each sItem In Me.lstModify.Items

            If sItem.Selected = True Then
                If modifyingOrderGroup = True Then
                    _modifySIN = CInt(sItem.SubItems(3).Text)   'selected sin
                    If ModifyCurrentSIN = ModifySIN Then
                        '   we just selected to modify the main item in a order group
                        '   can't do this
                        Exit Sub
                    End If
                    If lstModify.Items.Count > 1 Then
                        '   ???????????
                    End If
                    '   we are now going to modify part of the group order
                    _modifyCategoryID = CInt(sItem.SubItems(2).Text) '   CategroyID
                    _currentItemID = CInt(sItem.SubItems(1).Text)    'FoodID or DrinkID
                    ModifyCurrentSIN = _modifySIN
                    CreateNewModifyDataView()
                    modifyingOrderGroup = False
                    PopulateModifyListView()
                    Exit Sub                            '**** not sure to exit??
                End If
                _modifyItemID = CInt(sItem.SubItems(1).Text)    'FoodID or DrinkID
                sItem.ForeColor = Color.Red

            Else
                sItem.ForeColor = Color.Black
            End If
        Next


        Dim subString As String

        If isFoodItem = True Then

            '   **********************************
            '   need to verify which of these we need 
            '   we are taking this info from Foods Table and placing in OpenOrders Table

            For Each vRow In dvModifyOrder
                If vRow("FoodID") = _modifyItemID Then
                    If Not ModifyCurrentName.Substring(0, 5) = "*Sub*" Then
                        subString = "*Sub* "
                    End If
                    If Not vRow("ChitFoodName") Is DBNull.Value Then
                        _modifyChitName = "   " & subString & vRow("ChitFoodName")
                        '         lblModifyNew.Text = vRow("ChitFoodName")
                    End If

                    If Not vRow("AbrevFoodName") Is DBNull.Value Then
                        _modifyAbrevName = "   " & subString & vRow("AbrevFoodName")
                        lblModifyNew.Text = vRow("AbrevFoodName")
                    End If
                    If Not vRow("FoodName") Is DBNull.Value Then
                        _modifyItemName = "   " & subString & vRow("FoodName")
                        '     lblModifyNew.Text = vRow("FoodName")
                    End If

                    If newItemIsFree = True Then
                        _modifySurcharge = 0
                    Else
                        If Not vRow("Surcharge") Is DBNull.Value Then
                            If currentTable.si2 > 1 And currentTable.si2 < 10 Then
                                _modifySurcharge = Format((vRow("Surcharge") * 0.5), "##,###.00")
                            Else
                                _modifySurcharge = Format(vRow("Surcharge"), "##,###.00")
                            End If
                        Else
                            _modifySurcharge = 0
                        End If
                    End If
                    If Not vRow("TaxID") Is DBNull.Value Then
                        _modifyTaxId = vRow("TaxID")
                    Else
                        _modifyTaxId = 0
                    End If

                    If Not vRow("RoutingID") Is DBNull.Value Then
                        _modifyRoutingID = vRow("RoutingID")
                    Else
                        _modifyRoutingID = 0
                    End If
                    '   If modifyingOrderGroup = False Then
                    If Not vRow("PrepareTime") Is DBNull.Value Then
                        _modifyPrepareTime = vRow("PrepareTime")
                    Else
                        _modifyPrepareTime = 0
                    End If
                    '   End If


                    Exit For
                End If
            Next
        Else                'when drink item
            For Each vRow In dvModifyOrder
                If vRow("DrinkID") = _modifyItemID Then
                    If Not ModifyCurrentName.Substring(0, 5) = "*Sub*" Then
                        subString = "*Sub* "
                    End If
                    _modifyChitName = "   " & subString & vRow("DrinkName")
                    _modifyAbrevName = "   " & subString & vRow("DrinkName")
                    _modifyItemName = "   " & subString & vRow("DrinkName")
                    lblModifyNew.Text = vRow("DrinkName")
                    If newItemIsFree = True Then
                        _modifySurcharge = 0
                    Else
                        _modifySurcharge = vRow("AddOnPrice")
                    End If
                    _modifyTaxId = vRow("TaxID")
                    _modifyRoutingID = vRow("RoutingID")            'this is only temp
                    _modifyPrepareTime = Nothing
                    Exit For
                End If
            Next
        End If

        RaiseEvent AcceptModify()

        Me.Dispose()

    End Sub

    Private Sub CreateNewModifyDataView()
        Dim populatingTable As String

        If ModifyCategoryID > 100 Then
            populatingTable = "ModifierTable"
        Else
            If currentTable.IsPrimaryMenu = True Then
                populatingTable = "MainTable"
            Else
                populatingTable = "SecondaryMainTable"
            End If
        End If

        If isFoodItem = True Then
            dvModifyOrder = New DataView
            With dvModifyOrder
                .Table = ds.Tables(populatingTable & ModifyCategoryID)
                .Sort = "FoodName"
            End With


        Else    '*** for drink    redo   ??????
            '            With dvModifyOrder
            '           .Table = ds.Tables(populatingTable & catID)
            '          .Sort = "FoodName"
            '         End With

        End If

    End Sub
    Private Function IsItemIncluded(ByVal testingFoodID As Integer, ByVal funID As Integer, ByVal catID As Integer)
        ' if current item has no price, we check to see if it nomally does
        '   if it normally does we consider it included and flag true
        '   for Drink Adds may not work as good b/c we would have used add-on price

        Dim oRow As DataRow
        Dim dtr As SqlClient.SqlDataReader
        Dim originalPrice As Decimal

        '    *222checking(FoodTable)

        If isFoodItem = True Then

            Dim populatingTable As String
            '         If catID > 100 Then
            If _funFlag = "M" Then
                populatingTable = "ModifierTable"
            Else
                If currentTable.IsPrimaryMenu = True Then
                    populatingTable = "MainTable"
                Else
                    populatingTable = "SecondaryMainTable"
                End If
            End If

            If funID = 2 Or funID = 3 Then
                For Each oRow In ds.Tables(populatingTable & catID).Rows
                    If oRow("FoodID") = testingFoodID Then
                        If oRow("Surcharge") Is DBNull.Value Then
                            originalPrice = 0
                        Else
                            originalPrice = oRow("Surcharge")
                        End If
                        Exit For
                    End If
                Next

            End If

            '      For Each oRow In ds.Tables("FoodTable").Rows
            '     If oRow("FoodID") = testingFoodID Then
            '    originalPrice = oRow("Surcharge")
            '   Exit For
            ' End If
            '    Next

        Else
            For Each oRow In ds.Tables("Drink").Rows
                If oRow("DrinkID") = testingFoodID Then
                    If Not oRow("AddOnPrice") Is DBNull.Value Then
                        originalPrice = oRow("AddOnPrice")
                    Else
                        If Not oRow("DrinkPrice") Is DBNull.Value Then
                            originalPrice = oRow("DrinkPrice")
                        Else
                            originalPrice = 0
                        End If

                    End If
                    Exit For
                End If
            Next

        End If

        If originalPrice > 0 Then
            newItemIsFree = True
        End If

        Return newItemIsFree

    End Function

    Private Sub btnModifyCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModifyCancel.Click
        RaiseEvent CancelModify()
        Me.Dispose()

    End Sub

    Private Sub btnModifyAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModifyAccept.Click

        RaiseEvent AcceptModify()
        Me.Dispose()

    End Sub



    Private Sub CreateModifyButtonPanel()
        Dim w As Single = pnlQuantity.Width / 5
        Dim h As Single = Me.pnlQuantity.Height / 2
        Dim x As Single = 0
        Dim y As Single = 0
        Me.pnlQuantity.SuspendLayout()
        Me.pnlCustomer.SuspendLayout()
        Me.pnlCourse.SuspendLayout()


        Dim index As Integer

        For index = 1 To 10
            If ModifyQuantity = index Then
                CreateModifyOrderQuantityButton(index, x, y, w, h, c9)
            Else
                CreateModifyOrderQuantityButton(index, x, y, w, h, c7)
            End If
            If index = 5 Then
                x = 0
                y = y + h
            ElseIf index > 5 Then
                x = (w * (index - 5))
            Else
                x = (w * index)
            End If

        Next

        x = 0
        y = 0

        For index = 1 To 10
            If ModifyCustomerNumber = index Then
                CreateModifyOrderCustomerButton(index, x, y, w, h, c9)
            Else
                CreateModifyOrderCustomerButton(index, x, y, w, h, c7)
            End If
            If index = 5 Then
                x = 0
                y = y + h
            ElseIf index > 5 Then
                x = (w * (index - 5))
            Else
                x = (w * index)
            End If
        Next

        x = 0
        y = 0

        For index = 1 To 5
            If ModifyCourse = index Then
                CreateModifyOrderCourseButton(index, x, y, w, h, c9)
            Else
                CreateModifyOrderCourseButton(index, x, y, w, h, c7)
            End If
            x = (w * index)
        Next

        If ModifyCurrentSIN <> ModifyCurrentSII Then
            Me.pnlQuantity.Enabled = False
            Me.pnlCustomer.Enabled = False
            Me.pnlCourse.Enabled = False
        End If

        Me.pnlQuantity.ResumeLayout()
        Me.pnlCustomer.ResumeLayout()
        Me.pnlCourse.ResumeLayout()


    End Sub

    Private Sub CreateModifyOrderQuantityButton(ByVal btnNo As Integer, ByVal xPos As Single, ByVal yPos As Single, ByVal w As Single, ByVal h As Single, ByVal cc As Color)

        '   we don't create the first one son they match with the ID integer
        btnModifyQuantity(btnNo) = New KitchenButton(btnNo, w, h, cc, c2)
        With btnModifyQuantity(btnNo)
            .Location = New Point(xPos, yPos)
            .ForeColor = c3
            AddHandler btnModifyQuantity(btnNo).Click, AddressOf ModifyOrderQuantityButton_Click

            btnModifyQuantity(btnNo).ID = btnNo
        End With

        Me.pnlQuantity.Controls.Add(btnModifyQuantity(btnNo))

    End Sub

    Private Sub CreateModifyOrderCourseButton(ByVal btnNo As Integer, ByVal xPos As Single, ByVal yPos As Single, ByVal w As Single, ByVal h As Single, ByVal cc As Color)
        '   we don't create the first one son they match with the ID integer
        btnModifyCourse(btnNo) = New KitchenButton(btnNo, w, h, cc, c2)
        With btnModifyCourse(btnNo)
            .Location = New Point(xPos, yPos)
            .ForeColor = c3
            AddHandler btnModifyCourse(btnNo).Click, AddressOf ModifyOrderCourseButton_Click

            btnModifyCourse(btnNo).ID = btnNo
        End With

        Me.pnlCourse.Controls.Add(btnModifyCourse(btnNo))

    End Sub

    Private Sub CreateModifyOrderCustomerButton(ByVal btnNo As Integer, ByVal xPos As Single, ByVal yPos As Single, ByVal w As Single, ByVal h As Single, ByVal cc As Color)
        '   we don't create the first one son they match with the ID integer
        btnModifyCustomer(btnNo) = New KitchenButton(btnNo, w, h, cc, c2)
        With btnModifyCustomer(btnNo)
            .Location = New Point(xPos, yPos)
            .ForeColor = c3
            AddHandler btnModifyCustomer(btnNo).Click, AddressOf ModifyOrderCustomerButton_Click

            btnModifyCustomer(btnNo).ID = btnNo
        End With

        Me.pnlCustomer.Controls.Add(btnModifyCustomer(btnNo))

    End Sub

    Private Sub ModifyOrderCourseButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles coursePanel.Click

        Dim objButton As KitchenButton
        Dim index As Integer
        Dim oRow As DataRow
        Dim oldCourse = ModifyCourse

        Try
            objButton = CType(sender, KitchenButton)
        Catch ex As Exception
            Exit Sub
        End Try
        '     If Not objButton.GetType Is btnCourse1.GetType Then Exit Sub

        For index = 1 To 5
            btnModifyCourse(index).BackColor = c7
        Next
        _modifyCourse = CInt(objButton.ID)

        objButton.BackColor = c9

        If Not ModifyCurrentSII = Nothing Then
            'this is if it is a main food, therefore we can change the quantity
            For Each oRow In dsOrder.Tables("OpenOrders").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("sii") = ModifyCurrentSII Then
                        oRow("CourseNumber") = ModifyCourse
                    End If
                End If
            Next
        End If

        RaiseEvent AcceptModifySubTotal()
        Me.Dispose()

    End Sub

    Private Sub ModifyOrderQuantityButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles coursePanel.Click

        Dim objButton As KitchenButton
        Dim index As Integer
        Dim oRow As DataRow
        Dim oldQuantity = ModifyQuantity

        Try
            objButton = CType(sender, KitchenButton)
        Catch ex As Exception
            Exit Sub
        End Try
        '     If Not objButton.GetType Is btnCourse1.GetType Then Exit Sub

        For index = 1 To 10
            btnModifyQuantity(index).BackColor = c7
        Next
        ModifyQuantity = CInt(objButton.ID)

        objButton.BackColor = c9

        If Not ModifyCurrentSII = Nothing Then
            'this is if it is a main food, therefore we can change the quantity
            For Each oRow In dsOrder.Tables("OpenOrders").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("sii") = ModifyCurrentSII Then
                        oRow("Quantity") = ModifyQuantity
                        '   the below updates prices for the change
                        oRow("Price") = Format(oRow("Price") * (ModifyQuantity / oldQuantity), "####0.00")
                        oRow("TaxPrice") = Format(oRow("TaxPrice") * (ModifyQuantity / oldQuantity), "####0.00")
                        oRow("SinTax") = Format(oRow("SinTax") * (ModifyQuantity / oldQuantity), "####0.00")
                    End If
                End If
            Next
        End If

        RaiseEvent AcceptModifySubTotal()
        Me.Dispose()

    End Sub

    Private Sub ModifyOrderCustomerButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles coursePanel.Click
        Dim objButton As KitchenButton
        Dim index As Integer
        Dim oRow As DataRow
        Dim newCNTest As Integer

        Try
            objButton = CType(sender, KitchenButton)
        Catch ex As Exception
            Exit Sub
        End Try
        '     If Not objButton.GetType Is btnCourse1.GetType Then Exit Sub

        '   *** currently not allowing us to change customer number on sub item
        If Not ModifyCurrentSIN = ModifyCurrentSII Then
            RaiseEvent CancelModify()
            Me.Dispose()
            Exit Sub
        End If

        For index = 1 To 9
            btnModifyCustomer(index).BackColor = c7
        Next
        ModifyCustomerNumber = CInt(objButton.ID)
        objButton.BackColor = c9

        newCNTest = GenerateOrderTables.DetermineCnTest(ModifyCustomerNumber)

        If newCNTest = 0 Then
            Dim currentItem As SelectedItemDetail = New SelectedItemDetail
            Dim custNumString As String = "               " + ModifyCustomerNumber.ToString + "   CUSTOMER   " + ModifyCustomerNumber.ToString

            With currentItem
                .Check = currentTable.CheckNumber
                .Customer = ModifyCustomerNumber
                .Course = currentTable.CourseNumber
                .FunctionFlag = "N"
                '      If currentTable.MiddleOfOrder = True Then
                '     .SIN = currentTable.ReferenceSIN - 1
                '    .SII = currentTable.ReferenceSIN - 1
                '   Else

                .SIN = currentTable.SIN
                .SII = currentTable.SIN
                .si2 = currentTable.si2
                '  End If
                .ID = 0
                .Name = custNumString
                .TerminalName = custNumString
                .ChitName = custNumString
                .Price = Nothing
                .Category = Nothing
            End With

            currentTable.ReferenceSIN = currentTable.SIN

            RaiseEvent AddingItemToOrder(currentItem)
            '     If currentTable.CustomerNumber <> 1 Then
            '    currentTable.EmptyCustPanel = currentTable.CustomerNumber
            '    End If
            GenerateOrderTables.CustomerPanelOneTest()


        End If

        If Not ModifyCurrentSII = Nothing Then
            'this is if it is a main food, therefore we can change the quantity
            For Each oRow In dsOrder.Tables("OpenOrders").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("sii") = ModifyCurrentSII Then
                        oRow("CustomerNumber") = ModifyCustomerNumber
                    End If
                End If
            Next
        End If

        ' I just added this back without knowing ?????
        '      RaiseEvent AcceptModifySubTotal()
        '     Me.Dispose()
        '    Exit Sub

        '   *** i think we can get rid of all this
        currentTable.ReferenceSIN = currentTable.SIN

        '       ElseIf newCNTest > 0 Then
        Dim sinArray(dvModifyOrder.Count) As Integer
        Dim count As Integer
        Dim vRow As DataRowView
        '     Dim OOnewRow As DataRow = dsOrder.Tables("OpenOrders").NewRow

        For Each vRow In dvModifyOrder
            ' add a new oRow(OOnewRow) for each row in dvModifyOrder and delete old vrow from Table

            '       If vRow("sii") = ModifyCurrentSII Then
            If vRow("CustomerNumber") = ModifyCustomerNumber Then
                CopyViewForTransferItem(vRow, vRow("EmployeeID"), vRow("ExperienceNumber"), False, Nothing)

                '     dsOrder.Tables("OpenOrders").Rows.Add(OOnewRow)
                '                     bRow = (dsBackup.Tables("OpenOrdersTerminal").Rows.Find(oRow("sin")))
                '                    bRow.Delete()
                '              oRow.Delete()
                sinArray(count) = vRow("sin")
                count += 1
                '                   dsBackup.Tables("OpenOrdersTerminal").AcceptChanges()
            End If
        Next

        If count > 0 Then
            Dim i As Integer
            For i = 0 To count - 1
                If Not typeProgram = "Online_Demo" Then
                    oRow = (dsOrder.Tables("OpenOrders").Rows.Find(sinArray(i)))
                Else
                    For Each oRow In dsOrder.Tables("OpenOrders").Rows
                        If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                            If oRow("sin") = sinArray(i) Then
                                Exit For
                            End If
                        End If
                    Next
                End If
                GenerateOrderTables.DeleteOpenOrdersRowTerminal(oRow)
            Next
            count = 0
        End If

        RaiseEvent AcceptModifySubTotal()
        Me.Dispose()

    End Sub


    '        sql.cn.Open()
    '        If isFoodItem = True Then
    '        Dim cmd As New SqlClient.SqlCommand("SELECT Price From MenuJoin WHERE MenuID = '" & currentTable.CurrentMenu & "' AND FoodID = '" & testingFoodID & "'", sql.cn)
    '        dtr = cmd.ExecuteReader
    '        dtr.Read()
    '        originalPrice = (dtr("Price"))
    '        Else
    '            Dim cmd As New SqlClient.SqlCommand("SELECT DrinkPrice From Drinks WHERE DrinkID = '" & testingFoodID & "'", sql.cn)
    '           dtr = cmd.ExecuteReader
    ''           dtr.Read()
    '           originalPrice = (dtr("DrinkPrice"))
    '       End If
    '       dtr.Close()
    '       sql.cn.Close()






End Class
