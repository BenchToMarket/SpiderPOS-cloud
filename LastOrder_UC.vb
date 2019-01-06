Imports DataSet_Builder

Public Class LastOrder_UC
    Inherits System.Windows.Forms.UserControl

    Friend currentRepeatOrderCollection As RepeatOrderItemCollection
    Dim currentRepeatItem As SelectedItemDetail

    Dim _repeatQuantity As Integer
    Dim _repeatName As String
    Dim _repeatPrice As Decimal
    Dim _repeatTaxID As Integer
    Dim _repeatFunction As Integer
    Dim _itemNumber As Integer
    Dim _forRepeat As Boolean
    Dim _orderNumber As Int64

    '   ********************************
    '   not really using these property's 
    '   we can get rid of after we no there is no other use

    Friend Property RepeatQuantity() As Integer
        Get
            Return _repeatQuantity
        End Get
        Set(ByVal Value As Integer)
            _repeatQuantity = Value
        End Set
    End Property

    Friend Property RepeatName() As String
        Get
            Return _repeatName
        End Get
        Set(ByVal Value As String)
            _repeatName = Value
        End Set
    End Property

    Friend Property RepeatPrice() As Decimal
        Get
            Return _repeatPrice
        End Get
        Set(ByVal Value As Decimal)
            _repeatPrice = Value
        End Set
    End Property

    Friend Property RepeatTaxId() As Integer
        Get
            Return _repeatTaxID
        End Get
        Set(ByVal Value As Integer)
            _repeatTaxID = Value
        End Set
    End Property

    Friend Property RepeatFunction() As Integer
        Get
            Return _repeatFunction
        End Get
        Set(ByVal Value As Integer)
            _repeatFunction = Value
        End Set
    End Property

    Friend Property ItemNumber() As Integer
        Get
            Return _itemNumber
        End Get
        Set(ByVal Value As Integer)
            _itemNumber = Value
        End Set
    End Property

    Friend Property OrderNumber() As Int64
        Get
            Return _orderNumber
        End Get
        Set(ByVal Value As Int64)
            _orderNumber = Value
        End Set
    End Property


    Event AcceptRepeat()
    Event OrderDelivered()


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal forRepeat As Boolean)
        MyBase.New()

        _forRepeat = forRepeat
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        If _forRepeat = False Then
            ReshowForOrderDelivery()
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
    Friend WithEvents btnDrinksOnly As System.Windows.Forms.Button
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lstRepeat As System.Windows.Forms.ListView
    Friend WithEvents repeatNameCol As System.Windows.Forms.ColumnHeader
    Friend WithEvents repeatPriceCol As System.Windows.Forms.ColumnHeader
    Friend WithEvents repeatItemIDCol As System.Windows.Forms.ColumnHeader
    Friend WithEvents repeatQuantityCol As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblLastOrder As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblLastOrder = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lstRepeat = New System.Windows.Forms.ListView
        Me.repeatQuantityCol = New System.Windows.Forms.ColumnHeader
        Me.repeatNameCol = New System.Windows.Forms.ColumnHeader
        Me.repeatPriceCol = New System.Windows.Forms.ColumnHeader
        Me.repeatItemIDCol = New System.Windows.Forms.ColumnHeader
        Me.btnDrinksOnly = New System.Windows.Forms.Button
        Me.btnAccept = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblLastOrder
        '
        Me.lblLastOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastOrder.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLastOrder.Location = New System.Drawing.Point(24, 24)
        Me.lblLastOrder.Name = "lblLastOrder"
        Me.lblLastOrder.Size = New System.Drawing.Size(224, 32)
        Me.lblLastOrder.TabIndex = 0
        Me.lblLastOrder.Text = "Repeat the Last Order"
        Me.lblLastOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel1.Controls.Add(Me.lstRepeat)
        Me.Panel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(8, 72)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(448, 320)
        Me.Panel1.TabIndex = 1
        '
        'lstRepeat
        '
        Me.lstRepeat.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.repeatQuantityCol, Me.repeatNameCol, Me.repeatPriceCol, Me.repeatItemIDCol})
        Me.lstRepeat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstRepeat.FullRowSelect = True
        Me.lstRepeat.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstRepeat.Location = New System.Drawing.Point(8, 8)
        Me.lstRepeat.Name = "lstRepeat"
        Me.lstRepeat.Size = New System.Drawing.Size(232, 304)
        Me.lstRepeat.TabIndex = 0
        Me.lstRepeat.View = System.Windows.Forms.View.Details
        '
        'repeatQuantityCol
        '
        Me.repeatQuantityCol.Width = 20
        '
        'repeatNameCol
        '
        Me.repeatNameCol.Text = ""
        Me.repeatNameCol.Width = 145
        '
        'repeatPriceCol
        '
        Me.repeatPriceCol.Text = ""
        Me.repeatPriceCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'repeatItemIDCol
        '
        Me.repeatItemIDCol.Text = ""
        Me.repeatItemIDCol.Width = 0
        '
        'btnDrinksOnly
        '
        Me.btnDrinksOnly.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDrinksOnly.ForeColor = System.Drawing.Color.White
        Me.btnDrinksOnly.Location = New System.Drawing.Point(288, 16)
        Me.btnDrinksOnly.Name = "btnDrinksOnly"
        Me.btnDrinksOnly.Size = New System.Drawing.Size(160, 40)
        Me.btnDrinksOnly.TabIndex = 2
        Me.btnDrinksOnly.Text = "Drinks Only"
        '
        'btnAccept
        '
        Me.btnAccept.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnAccept.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAccept.ForeColor = System.Drawing.Color.White
        Me.btnAccept.Location = New System.Drawing.Point(40, 400)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(144, 64)
        Me.btnAccept.TabIndex = 3
        Me.btnAccept.Text = "Accept"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(272, 400)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(144, 64)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Black
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnAccept)
        Me.Panel2.Controls.Add(Me.lblLastOrder)
        Me.Panel2.Controls.Add(Me.btnDrinksOnly)
        Me.Panel2.Location = New System.Drawing.Point(8, 8)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(472, 472)
        Me.Panel2.TabIndex = 5
        '
        'LastOrder_UC
        '
        Me.BackColor = System.Drawing.Color.RoyalBlue
        Me.Controls.Add(Me.Panel2)
        Me.Name = "LastOrder_UC"
        Me.Size = New System.Drawing.Size(488, 488)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Repeat_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lstRepeat.Items.Clear()
        Me.currentRepeatOrderCollection = New RepeatOrderItemCollection

        StartRepeatProcess()

    End Sub

    Private Sub ReshowForOrderDelivery()
        Me.btnAccept.Text = "Delivered"
        Me.btnCancel.Text = "Not Ready"
        Me.lblLastOrder.Text = "Pending Order"
        Me.btnDrinksOnly.Visible = False

    End Sub
    Private Sub StartRepeatProcess()
        '        Dim oRow As DataRow
        Dim vRow As DataRowView

        For Each vRow In dvRepeat   'dsOrder.Tables("RepeatOrder").Rows

            '        If vRow("Repeat") = True Then
            If _forRepeat = True And vRow("DrinkID") = 0 Then
                'this is for the non drinks     '???? just moved up
                vRow("Repeat") = 0
            Else

                currentRepeatItem = New SelectedItemDetail
                With currentRepeatItem
                    .Name = vRow("ItemName")
                    .TerminalName = vRow("ItemName")
                    .ChitName = vRow("ItemName")
                    '       .TaxID = vRow("TaxID")
                    .Check = vRow("CheckNumber")
                    .Customer = vRow("CustomerNumber")
                    .Quantity = vRow("Quantity")
                    .ID = vRow("DrinkID")
                    .Category = vRow("DrinkCategoryID")
                    .ItemStatus = 0
                    .FunctionID = vRow("FunctionID")
                    .FunctionGroup = vRow("FunctionGroupID")
                    .FunctionFlag = vRow("FunctionFlag")
                    .RoutingID = vRow("RoutingID")
                    .PrintPriorityID = vRow("PrintPriorityID")
                    .ItemPrice = vRow("ItemPrice")
                End With
                _repeatQuantity = vRow("Quantity")
                _repeatName = vRow("ItemName")

                _repeatFunction = vRow("FunctionID")
                _itemNumber = vRow("DrinkID")

                '            _repeatTaxID = vRow("TaxID")
                If vRow("sin") = vRow("sii") Then
                    currentRepeatItem.Price = vRow("Price")
                    _repeatPrice = vRow("Price")
                    PopulateRepeatListView(vRow("DrinkID"))
                    '           DetermineDrinkMainDetails(vRow("DrinkID"))
                Else
                    currentRepeatItem.Price = vRow("Price")
                    currentRepeatItem.SII = -1
                    _repeatPrice = vRow("Price")
                    _repeatQuantity = Nothing
                    PopulateRepeatListView(vRow("DrinkID"))
                    '        DetermineDrinkModifierDetails(vRow("DrinkID"))
                End If
                If vRow("si2") > 0 Then
                    currentTable.si2 = vRow("si2")
                End If
            End If

            '     End If

        Next


    End Sub

    Private Sub DetermineDrinkMainDetails222(ByVal itemID As Integer)
        '   ********************'   
        '   This is if we wnat to look up previous order original prices
        '   and recalculate all info

        Dim oRow As DataRow
        _repeatFunction = 4
        _itemNumber = itemID

        For Each oRow In ds.Tables("Drink").Rows
            If oRow("DrinkID") = itemID Then
                With currentRepeatItem
                    .Name = oRow("DrinkName")
                    .TerminalName = oRow("DrinkName")
                    .ChitName = oRow("DrinkName")
                    .Price = oRow("DrinkPrice") * RepeatQuantity
                    '               .TaxID = oRow("TaxID")

                End With
                _repeatName = oRow("DrinkName")
                _repeatPrice = oRow("DrinkPrice") * RepeatQuantity
                '            _repeatTaxID = oRow("TaxID")

            End If
        Next


        PopulateRepeatListView(itemID)



    End Sub

    Private Sub DetermineDrinkModifierDetails222(ByVal itemID As Integer)
        Dim oRow As DataRow
        _repeatFunction = 4
        _itemNumber = itemID

        For Each oRow In ds.Tables("Drink").Rows
            If oRow("DrinkID") = itemID Then
                With currentRepeatItem
                    .Name = oRow("DrinkName")
                    .TerminalName = oRow("DrinkName")
                    .ChitName = oRow("DrinkName")
                    .Price = oRow("AddOnPrice") * RepeatQuantity
                    '                .TaxID = oRow("TaxID")
                    .SII = -1
                End With
                _repeatName = "   " & oRow("DrinkName")
                _repeatPrice = oRow("AddOnPrice") * RepeatQuantity
                _repeatTaxID = oRow("TaxID")
            End If
        Next

        '   we put to nothing b/c we arleady used in calculations 
        '   but this way we don't show a modifier quantity
        _repeatQuantity = Nothing
        PopulateRepeatListView(itemID)



    End Sub

    Private Sub PopulateRepeatListView(ByVal itemID As Integer)

        '***********************
        '    creates a collection of selectedItemDetail
        currentRepeatOrderCollection.Add(currentRepeatItem)


        Dim repeatItem As New ListViewItem

        If RepeatQuantity > 1 Then
            repeatItem.Text = RepeatQuantity
        Else
            repeatItem.Text = " "
        End If
        repeatItem.SubItems.Add(RepeatName)
        repeatItem.SubItems.Add(RepeatPrice)
        repeatItem.SubItems.Add(itemID)
        Me.lstRepeat.Items.Add(repeatItem)

    End Sub



    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        currentTable.si2 = 0
        currentTable.Tempsi2 = 0
        Me.Dispose()

    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click

        If _forRepeat = True Then
            RaiseEvent AcceptRepeat()
        Else
            RaiseEvent OrderDelivered()
        End If

        Me.Dispose()

    End Sub
End Class
