Imports DataSet_Builder


Public Class ExtraNo_UC
    Inherits System.Windows.Forms.UserControl

    Dim _mainfoodItemName As String
    Dim _mainfoodFunID As Integer
    Dim _mainfoodFunGroup As Integer
    Dim _mainfoodFunFlag As String
    Dim _mainfoodTaxID As Integer

    Dim _rawType As String
    Dim _selectedRawItemID As Integer
    Dim _selectedRawItemName As String
    Dim _selectedRawPriceTotal As Decimal
    Dim _selectedRawTrackAs As Integer



    Friend Property MainFoodItemName() As String
        Get
            Return _mainfoodItemName
        End Get
        Set(ByVal Value As String)
            _mainfoodItemName = Value
        End Set
    End Property

    Friend Property MainFoodFunID() As Integer
        Get
            Return _mainfoodFunID
        End Get
        Set(ByVal Value As Integer)
            _mainfoodFunID = Value
        End Set
    End Property

    Friend Property MainFoodFunGroup() As Integer
        Get
            Return _mainfoodFunGroup
        End Get
        Set(ByVal Value As Integer)
            _mainfoodFunGroup = Value
        End Set
    End Property

    Friend Property MainFoodFunFlag() As String
        Get
            Return _mainfoodFunFlag
        End Get
        Set(ByVal Value As String)
            _mainfoodFunFlag = Value
        End Set
    End Property

    Friend Property MainFoodTaxID() As Integer
        Get
            Return _mainfoodTaxID
        End Get
        Set(ByVal Value As Integer)
            _mainfoodTaxID = Value
        End Set
    End Property


    Friend Property RawType() As String
        Get
            Return _rawType
        End Get
        Set(ByVal Value As String)
            _rawType = Value
        End Set
    End Property

    Friend Property SelectedRawItemID() As String
        Get
            Return _selectedRawItemID
        End Get
        Set(ByVal Value As String)
            _selectedRawItemID = Value
        End Set
    End Property

    Friend Property SelectedRawItemName() As String
        Get
            Return _selectedRawItemName
        End Get
        Set(ByVal Value As String)
            _selectedRawItemName = Value
        End Set
    End Property

    Friend Property SelectedRawPriceTotal() As Decimal
        Get
            Return _selectedRawPriceTotal
        End Get
        Set(ByVal Value As Decimal)
            _selectedRawPriceTotal = Value
        End Set
    End Property

    Friend Property SelectedRawTrackAs() As Integer
        Get
            Return _selectedRawTrackAs
        End Get
        Set(ByVal Value As Integer)
            _selectedRawTrackAs = Value
        End Set
    End Property

    Event SelectedNO()
    Event SelectedEXTRA()
    Event SelectedClose()

    Event AddingItemToOrder(ByVal sender As Object)




#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents lstIngredientsExtraNo As System.Windows.Forms.ListView
    Friend WithEvents lstIngredientsAll As System.Windows.Forms.ListView
    Friend WithEvents lblRawItemName As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lstAllBreak As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstAllUnit As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstAllItemName As System.Windows.Forms.ColumnHeader
    Friend WithEvents cRawItemID As System.Windows.Forms.ColumnHeader
    Friend WithEvents cRawItemName As System.Windows.Forms.ColumnHeader
    Friend WithEvents cRawPriceTotal As System.Windows.Forms.ColumnHeader
    Friend WithEvents cRawTrackAs As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnExtra As System.Windows.Forms.Button
    Friend WithEvents btnNo As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lstIngredientsExtraNo = New System.Windows.Forms.ListView
        Me.cRawItemID = New System.Windows.Forms.ColumnHeader
        Me.cRawItemName = New System.Windows.Forms.ColumnHeader
        Me.cRawPriceTotal = New System.Windows.Forms.ColumnHeader
        Me.cRawTrackAs = New System.Windows.Forms.ColumnHeader
        Me.btnExtra = New System.Windows.Forms.Button
        Me.btnNo = New System.Windows.Forms.Button
        Me.lstIngredientsAll = New System.Windows.Forms.ListView
        Me.lstAllBreak = New System.Windows.Forms.ColumnHeader
        Me.lstAllUnit = New System.Windows.Forms.ColumnHeader
        Me.lstAllItemName = New System.Windows.Forms.ColumnHeader
        Me.lblRawItemName = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lstIngredientsExtraNo
        '
        Me.lstIngredientsExtraNo.BackColor = System.Drawing.Color.LightSlateGray
        Me.lstIngredientsExtraNo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cRawItemID, Me.cRawItemName, Me.cRawPriceTotal, Me.cRawTrackAs})
        Me.lstIngredientsExtraNo.FullRowSelect = True
        Me.lstIngredientsExtraNo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstIngredientsExtraNo.Location = New System.Drawing.Point(16, 96)
        Me.lstIngredientsExtraNo.MultiSelect = False
        Me.lstIngredientsExtraNo.Name = "lstIngredientsExtraNo"
        Me.lstIngredientsExtraNo.Size = New System.Drawing.Size(214, 368)
        Me.lstIngredientsExtraNo.TabIndex = 0
        Me.lstIngredientsExtraNo.View = System.Windows.Forms.View.Details
        '
        'cRawItemID
        '
        Me.cRawItemID.Text = ""
        Me.cRawItemID.Width = 0
        '
        'cRawItemName
        '
        Me.cRawItemName.Text = "Item"
        Me.cRawItemName.Width = 150
        '
        'cRawPriceTotal
        '
        Me.cRawPriceTotal.Text = "Price"
        Me.cRawPriceTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cRawTrackAs
        '
        Me.cRawTrackAs.Text = ""
        Me.cRawTrackAs.Width = 0
        '
        'btnExtra
        '
        Me.btnExtra.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExtra.ForeColor = System.Drawing.Color.White
        Me.btnExtra.Location = New System.Drawing.Point(24, 32)
        Me.btnExtra.Name = "btnExtra"
        Me.btnExtra.Size = New System.Drawing.Size(88, 48)
        Me.btnExtra.TabIndex = 1
        Me.btnExtra.Text = "Extra"
        '
        'btnNo
        '
        Me.btnNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNo.ForeColor = System.Drawing.Color.White
        Me.btnNo.Location = New System.Drawing.Point(128, 32)
        Me.btnNo.Name = "btnNo"
        Me.btnNo.Size = New System.Drawing.Size(88, 48)
        Me.btnNo.TabIndex = 2
        Me.btnNo.Text = "No"
        '
        'lstIngredientsAll
        '
        Me.lstIngredientsAll.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.lstIngredientsAll.BackColor = System.Drawing.Color.FromArgb(CType(59, Byte), CType(96, Byte), CType(141, Byte))
        Me.lstIngredientsAll.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstIngredientsAll.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lstAllBreak, Me.lstAllUnit, Me.lstAllItemName})
        Me.lstIngredientsAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstIngredientsAll.ForeColor = System.Drawing.Color.White
        Me.lstIngredientsAll.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstIngredientsAll.Location = New System.Drawing.Point(240, 192)
        Me.lstIngredientsAll.Name = "lstIngredientsAll"
        Me.lstIngredientsAll.Size = New System.Drawing.Size(264, 256)
        Me.lstIngredientsAll.TabIndex = 3
        Me.lstIngredientsAll.View = System.Windows.Forms.View.Details
        '
        'lstAllBreak
        '
        Me.lstAllBreak.Text = ""
        Me.lstAllBreak.Width = 30
        '
        'lstAllUnit
        '
        Me.lstAllUnit.Text = ""
        Me.lstAllUnit.Width = 50
        '
        'lstAllItemName
        '
        Me.lstAllItemName.Text = ""
        Me.lstAllItemName.Width = 180
        '
        'lblRawItemName
        '
        Me.lblRawItemName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRawItemName.ForeColor = System.Drawing.Color.White
        Me.lblRawItemName.Location = New System.Drawing.Point(272, 160)
        Me.lblRawItemName.Name = "lblRawItemName"
        Me.lblRawItemName.Size = New System.Drawing.Size(192, 24)
        Me.lblRawItemName.TabIndex = 4
        Me.lblRawItemName.Text = "Food Item Name"
        Me.lblRawItemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(400, 32)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 48)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'ExtraNo_UC
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(59, Byte), CType(96, Byte), CType(141, Byte))
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblRawItemName)
        Me.Controls.Add(Me.lstIngredientsAll)
        Me.Controls.Add(Me.btnNo)
        Me.Controls.Add(Me.btnExtra)
        Me.Controls.Add(Me.lstIngredientsExtraNo)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ExtraNo_UC"
        Me.Size = New System.Drawing.Size(512, 480)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub PopulateExtraNoListView()

        Dim vRow As DataRowView
        Dim rawPrice As Decimal

        Me.lstIngredientsExtraNo.Items.Clear()
        Me.lstIngredientsAll.Items.Clear()

        Me.lblRawItemName.Text = MainFoodItemName

        For Each vRow In dvIngredients
            Dim allItem As New ListViewItem

            allItem.Text = Format(vRow("RawUsageAmount"), "##0")
            allItem.SubItems.Add(vRow("RecipeUnit"))
            allItem.SubItems.Add(vRow("RawItemName"))

            Me.lstIngredientsAll.Items.Add(allItem)

        Next

        If RawType = "EXTRA" Then
            For Each vRow In dvIngredientsEXTRA
                Dim extraItem As New ListViewItem

                extraItem.Text = vRow("RawItemID")
                extraItem.SubItems.Add(vRow("RawItemName"))
                '     rawPrice = (vRow("RawUsageAmount") * vRow("ExtraPrice"))
                rawPrice = vRow("ExtraPrice")
                extraItem.SubItems.Add(Format(rawPrice, "##.00"))
                If Not vRow("TrackAs") Is DBNull.Value Then
                    extraItem.SubItems.Add(vRow("TrackAs"))
                Else
                    ' (-6 for NO) (-7 for EXTRA)
                    'we place this in ItemID in OpenOrders
                    extraItem.SubItems.Add(-7)
                End If

                Me.lstIngredientsExtraNo.Items.Add(extraItem)

            Next
            IsExtraType()
        Else
            For Each vRow In dvIngredientsNO
                Dim extraItem As New ListViewItem

                extraItem.Text = vRow("RawItemID")
                extraItem.SubItems.Add(vRow("RawItemName"))
                '    rawPrice = (vRow("RawUsageAmount") * vRow("NoPrice"))
                rawPrice = vRow("NoPrice")
                extraItem.SubItems.Add(Format(rawPrice, "##.00"))
                If Not vRow("TrackAs") Is DBNull.Value Then
                    extraItem.SubItems.Add(vRow("TrackAs"))
                Else
                    ' (-6 for NO) (-7 for EXTRA)
                    'we place this in ItemID in OpenOrders
                    extraItem.SubItems.Add(-6)
                End If

                Me.lstIngredientsExtraNo.Items.Add(extraItem)

            Next
            IsNoType()
        End If

    End Sub

    Private Sub lstIngredientsExtraNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstIngredientsExtraNo.Click

        Dim sItem As ListViewItem
        Dim currentItem As New SelectedItemDetail
        Dim vRow As DataRowView
        Dim fRow As DataRow
        Dim isTrackingFoodInventory As Boolean = False

        For Each sItem In Me.lstIngredientsExtraNo.Items
            If sItem.Selected = True Then
                _selectedRawItemID = CInt(sItem.SubItems(0).Text)
                _selectedRawItemName = (sItem.SubItems(1).Text)
                _selectedRawPriceTotal = CType(sItem.SubItems(2).Text, Decimal)
                _selectedRawTrackAs = CInt(sItem.SubItems(3).Text)
                Exit For
            End If
        Next

        For Each vRow In dvIngredients
            If vRow("RawItemID") = SelectedRawItemID Then
                Exit For
            End If
        Next
        '        If vRow Is Nothing Then Exit Sub


        '   MsgBox(vRow("TrackAs").ToString)

        If Not vRow("TrackAs") Is DBNull.Value Then
            'Raw material has a corresponding food item
            'we need to make sure food item was not deleted
            For Each fRow In ds.Tables("FoodTable").Rows
                If fRow("FoodID") = vRow("TrackAs") Then
                    isTrackingFoodInventory = True
                    Exit For
                End If
            Next
        End If

        With currentItem
            If isTrackingFoodInventory = True Then
                'Raw material has a corresponding food item
                .ID = vRow("TrackAs")

                .Category = fRow("CategoryID")
                If Not fRow("PrintPriorityID") Is DBNull.Value Then
                    .PrintPriorityID = fRow("PrintPriorityID")
                Else
                    .PrintPriorityID = 3
                End If
                .RoutingID = currentTable.ReferenceRouting
                .FunctionID = fRow("FunctionID")
                .FunctionGroup = fRow("FunctionGroupID")
                .FunctionFlag = fRow("FunctionFlag")
                .TaxID = fRow("TaxID")

            Else
                If RawType = "EXTRA" Then
                    .ID = -7
                    .Category = -7
                Else
                    .ID = -6
                    .Category = -6
                End If

                .PrintPriorityID = 3
                .RoutingID = currentTable.ReferenceRouting 'must do first in case drink
                .FunctionID = MainFoodFunID
                .FunctionGroup = MainFoodFunGroup
                .FunctionFlag = MainFoodFunFlag
                .TaxID = MainFoodTaxID

                DetermineFunctionAndTaxInfo(currentItem, 9, False)   '9 is functionGroup = Modifier
                ' not sure why we are doing above

            End If

            If RawType = "EXTRA" Then
                .Quantity = 1
                .Price = SelectedRawPriceTotal

            Else
                .Quantity = -1
                .Price = -1 * SelectedRawPriceTotal

            End If

            .Name = " *** " & currentTable.OrderingStatus & "  " & vRow("RawItemName")
            .TerminalName = " *** " & currentTable.OrderingStatus & "  " & vRow("RawItemName")
            .ChitName = " *** " & currentTable.OrderingStatus & "  " & vRow("RawItemName")

            .ItemStatus = 0
            .Check = currentTable.CheckNumber
            .Customer = currentTable.CustomerNumber
            .Course = currentTable.CourseNumber
            .SIN = currentTable.SIN
            .SII = currentTable.ReferenceSIN
            .si2 = currentTable.si2

        End With

        RaiseEvent AddingItemToOrder(currentItem)
        '      RaiseEvent SelectedClose()

    End Sub

    Private Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo.Click

        RawType = "NO"
        PopulateExtraNoListView()

    End Sub

    Private Sub IsNoType()
        Me.btnNo.BackColor = Color.Brown
        Me.btnExtra.BackColor = System.Drawing.Color.FromArgb(CType(59, Byte), CType(96, Byte), CType(141, Byte))
        currentTable.OrderingStatus = "NO"
        '  RaiseEvent SelectedNO()

    End Sub
    Private Sub btnExtra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtra.Click

        RawType = "EXTRA"
        PopulateExtraNoListView()
    End Sub

    Private Sub IsExtraType()
        Me.btnExtra.BackColor = Color.Brown
        Me.btnNo.BackColor = System.Drawing.Color.FromArgb(CType(59, Byte), CType(96, Byte), CType(141, Byte))
        currentTable.OrderingStatus = "EXTRA"
        '   RaiseEvent SelectedEXTRA()

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        RaiseEvent SelectedClose()

    End Sub


End Class
