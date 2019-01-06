Public Class SpecialFood
    Inherits System.Windows.Forms.UserControl

    '    Dim messageToKitchen As Boolean
    Dim specialPurpose As String
    Friend associateItem As Boolean

    Private _itemDescription As String
    Private _itemPrice As Decimal
    Private _associateSIN As Integer
    Dim _currentRouting As Integer

    Dim _functionID As Integer
    Dim _functionFlag As String
    Dim _functionGroup As Integer

    Dim _taxID As Integer
    Dim _routingID As Integer
    Dim _categoryID As Integer

    Dim dvAssociateItems As DataView

    Dim fRouting As Integer
    Dim dRouting As Integer
    Dim mRouting As Integer
    Dim beerRouting As Integer
    Dim wineRouting As Integer
    Dim liquorRouting As Integer
    Dim naRouting As Integer

    Dim fTax As Integer
    Dim dTax As Integer
    Dim mTax As Integer
    Dim beerTax As Integer
    Dim wineTax As Integer
    Dim liquorTax As Integer
    Dim naTax As Integer





    Public Property ItemDescription() As String
        Get
            Return _itemDescription
        End Get
        Set(ByVal Value As String)
            _itemDescription = Value
        End Set
    End Property

    Friend Property ItemPrice() As Decimal
        Get
            Return _itemPrice
        End Get
        Set(ByVal Value As Decimal)
            _itemPrice = Value
        End Set
    End Property

    Friend Property AssociateSIN() As Integer
        Get
            Return _associateSIN
        End Get
        Set(ByVal Value As Integer)
            _associateSIN = Value
        End Set
    End Property

    Friend Property CurrentRouting() As Integer
        Get
            Return _currentRouting
        End Get
        Set(ByVal Value As Integer)
            _currentRouting = Value
        End Set
    End Property

    Friend Property FunctionID() As Integer
        Get
            Return _functionID
        End Get
        Set(ByVal Value As Integer)
            _functionID = Value
        End Set
    End Property

    Friend Property FunctionGroup() As Integer
        Get
            Return _functionGroup
        End Get
        Set(ByVal Value As Integer)
            _functionGroup = Value
        End Set
    End Property

    Friend Property FunctionFlag() As String
        Get
            Return _functionFlag
        End Get
        Set(ByVal Value As String)
            _functionFlag = Value
        End Set
    End Property

    Friend Property TaxID() As Integer
        Get
            Return _taxID
        End Get
        Set(ByVal Value As Integer)
            _taxID = Value
        End Set
    End Property

    Friend Property RoutingID() As Integer
        Get
            Return _routingID
        End Get
        Set(ByVal Value As Integer)
            _routingID = Value
        End Set
    End Property

    Friend Property CategoryID() As Integer
        Get
            Return _categoryID
        End Get
        Set(ByVal Value As Integer)
            _categoryID = Value
        End Set
    End Property

    Event UC_Hit()
    Event CancelSpecial(ByVal sender As Object, ByVal e As System.EventArgs)
    Event AcceptSpecial(ByVal sender As Object, ByVal e As System.EventArgs)


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal sin As Integer, ByVal sii As Integer, ByVal isDrink As Boolean, ByVal assocItem As Boolean, ByVal currentRouting As Integer)
        MyBase.New()

        If Not currentRouting = Nothing Then
            _currentRouting = currentRouting
        Else
            _currentRouting = 0
        End If

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        InitializeOther(sin, sii, isDrink, assocItem)


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
    Friend WithEvents NumberPadSmall1 As DataSet_Builder.NumberPadSmall
    Friend WithEvents btnOpenFood As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnMessage As System.Windows.Forms.Button
    Friend WithEvents pnlAssociateItem As System.Windows.Forms.Panel
    Friend WithEvents btnItemSeperate As System.Windows.Forms.Button
    Friend WithEvents btnItemAssociation As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblItem As System.Windows.Forms.Label
    Friend WithEvents lblTextItem As System.Windows.Forms.Label
    Friend WithEvents lblPrice As System.Windows.Forms.Label
    Friend WithEvents lblTextPrice As System.Windows.Forms.Label
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lstAssociateItem As System.Windows.Forms.ListView
    Friend WithEvents ItemName As System.Windows.Forms.ColumnHeader
    Friend WithEvents ItemSIN As System.Windows.Forms.ColumnHeader
    Friend WithEvents SpecialKeyboard As DataSet_Builder.KeyBoard_UC
    Friend WithEvents btnOpenDrink As System.Windows.Forms.Button
    Friend WithEvents btnNonAlc As System.Windows.Forms.Button
    Friend WithEvents btnBeer As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.NumberPadSmall1 = New DataSet_Builder.NumberPadSmall
        Me.btnOpenFood = New System.Windows.Forms.Button
        Me.btnMessage = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.pnlAssociateItem = New System.Windows.Forms.Panel
        Me.btnBeer = New System.Windows.Forms.Button
        Me.btnNonAlc = New System.Windows.Forms.Button
        Me.lstAssociateItem = New System.Windows.Forms.ListView
        Me.ItemName = New System.Windows.Forms.ColumnHeader
        Me.ItemSIN = New System.Windows.Forms.ColumnHeader
        Me.btnItemAssociation = New System.Windows.Forms.Button
        Me.btnItemSeperate = New System.Windows.Forms.Button
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnAccept = New System.Windows.Forms.Button
        Me.lblTextPrice = New System.Windows.Forms.Label
        Me.lblPrice = New System.Windows.Forms.Label
        Me.lblTextItem = New System.Windows.Forms.Label
        Me.lblItem = New System.Windows.Forms.Label
        Me.SpecialKeyboard = New DataSet_Builder.KeyBoard_UC
        Me.btnOpenDrink = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.pnlAssociateItem.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'NumberPadSmall1
        '
        Me.NumberPadSmall1.BackColor = System.Drawing.Color.SlateGray
        Me.NumberPadSmall1.DecimalUsed = False
        Me.NumberPadSmall1.IntegerNumber = 0
        Me.NumberPadSmall1.Location = New System.Drawing.Point(568, 32)
        Me.NumberPadSmall1.Name = "NumberPadSmall1"
        Me.NumberPadSmall1.NumberString = Nothing
        Me.NumberPadSmall1.NumberTotal = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumberPadSmall1.Size = New System.Drawing.Size(144, 240)
        Me.NumberPadSmall1.TabIndex = 1
        '
        'btnOpenFood
        '
        Me.btnOpenFood.BackColor = System.Drawing.Color.SlateGray
        Me.btnOpenFood.Location = New System.Drawing.Point(32, 8)
        Me.btnOpenFood.Name = "btnOpenFood"
        Me.btnOpenFood.Size = New System.Drawing.Size(104, 40)
        Me.btnOpenFood.TabIndex = 2
        Me.btnOpenFood.Text = "Open Food"
        '
        'btnMessage
        '
        Me.btnMessage.BackColor = System.Drawing.Color.SlateGray
        Me.btnMessage.Location = New System.Drawing.Point(256, 8)
        Me.btnMessage.Name = "btnMessage"
        Me.btnMessage.Size = New System.Drawing.Size(112, 40)
        Me.btnMessage.TabIndex = 3
        Me.btnMessage.Text = "Message to Kitchen"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.SlateGray
        Me.Panel1.Controls.Add(Me.pnlAssociateItem)
        Me.Panel1.Location = New System.Drawing.Point(32, 56)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(336, 224)
        Me.Panel1.TabIndex = 4
        '
        'pnlAssociateItem
        '
        Me.pnlAssociateItem.Controls.Add(Me.btnBeer)
        Me.pnlAssociateItem.Controls.Add(Me.btnNonAlc)
        Me.pnlAssociateItem.Controls.Add(Me.lstAssociateItem)
        Me.pnlAssociateItem.Controls.Add(Me.btnItemAssociation)
        Me.pnlAssociateItem.Controls.Add(Me.btnItemSeperate)
        Me.pnlAssociateItem.Location = New System.Drawing.Point(8, 8)
        Me.pnlAssociateItem.Name = "pnlAssociateItem"
        Me.pnlAssociateItem.Size = New System.Drawing.Size(320, 224)
        Me.pnlAssociateItem.TabIndex = 4
        '
        'btnBeer
        '
        Me.btnBeer.Location = New System.Drawing.Point(232, 112)
        Me.btnBeer.Name = "btnBeer"
        Me.btnBeer.Size = New System.Drawing.Size(88, 40)
        Me.btnBeer.TabIndex = 7
        Me.btnBeer.Text = "Beer"
        Me.btnBeer.Visible = False
        '
        'btnNonAlc
        '
        Me.btnNonAlc.Location = New System.Drawing.Point(232, 168)
        Me.btnNonAlc.Name = "btnNonAlc"
        Me.btnNonAlc.Size = New System.Drawing.Size(88, 40)
        Me.btnNonAlc.TabIndex = 6
        Me.btnNonAlc.Text = "Non - Alcoholic"
        Me.btnNonAlc.Visible = False
        '
        'lstAssociateItem
        '
        Me.lstAssociateItem.BackColor = System.Drawing.Color.SlateGray
        Me.lstAssociateItem.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ItemName, Me.ItemSIN})
        Me.lstAssociateItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstAssociateItem.FullRowSelect = True
        Me.lstAssociateItem.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstAssociateItem.Location = New System.Drawing.Point(0, 0)
        Me.lstAssociateItem.Name = "lstAssociateItem"
        Me.lstAssociateItem.Size = New System.Drawing.Size(224, 208)
        Me.lstAssociateItem.TabIndex = 1
        Me.lstAssociateItem.View = System.Windows.Forms.View.Details
        '
        'ItemName
        '
        Me.ItemName.Width = 200
        '
        'ItemSIN
        '
        Me.ItemSIN.Width = 0
        '
        'btnItemAssociation
        '
        Me.btnItemAssociation.BackColor = System.Drawing.Color.SlateGray
        Me.btnItemAssociation.Location = New System.Drawing.Point(232, 0)
        Me.btnItemAssociation.Name = "btnItemAssociation"
        Me.btnItemAssociation.Size = New System.Drawing.Size(88, 40)
        Me.btnItemAssociation.TabIndex = 0
        Me.btnItemAssociation.Text = "Associate Item"
        '
        'btnItemSeperate
        '
        Me.btnItemSeperate.BackColor = System.Drawing.Color.Red
        Me.btnItemSeperate.Location = New System.Drawing.Point(232, 56)
        Me.btnItemSeperate.Name = "btnItemSeperate"
        Me.btnItemSeperate.Size = New System.Drawing.Size(88, 40)
        Me.btnItemSeperate.TabIndex = 5
        Me.btnItemSeperate.Text = "Seperate Item"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.SlateGray
        Me.Panel3.Controls.Add(Me.btnCancel)
        Me.Panel3.Controls.Add(Me.btnAccept)
        Me.Panel3.Controls.Add(Me.lblTextPrice)
        Me.Panel3.Controls.Add(Me.lblPrice)
        Me.Panel3.Controls.Add(Me.lblTextItem)
        Me.Panel3.Controls.Add(Me.lblItem)
        Me.Panel3.Location = New System.Drawing.Point(384, 32)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(168, 240)
        Me.Panel3.TabIndex = 5
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(24, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(120, 24)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        '
        'btnAccept
        '
        Me.btnAccept.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAccept.Location = New System.Drawing.Point(16, 192)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(136, 40)
        Me.btnAccept.TabIndex = 4
        Me.btnAccept.Text = "Accept"
        '
        'lblTextPrice
        '
        Me.lblTextPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTextPrice.ForeColor = System.Drawing.Color.Red
        Me.lblTextPrice.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTextPrice.Location = New System.Drawing.Point(56, 152)
        Me.lblTextPrice.Name = "lblTextPrice"
        Me.lblTextPrice.Size = New System.Drawing.Size(96, 24)
        Me.lblTextPrice.TabIndex = 3
        Me.lblTextPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPrice
        '
        Me.lblPrice.Location = New System.Drawing.Point(8, 128)
        Me.lblPrice.Name = "lblPrice"
        Me.lblPrice.Size = New System.Drawing.Size(104, 16)
        Me.lblPrice.TabIndex = 2
        Me.lblPrice.Text = "Price:"
        '
        'lblTextItem
        '
        Me.lblTextItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTextItem.ForeColor = System.Drawing.Color.Red
        Me.lblTextItem.Location = New System.Drawing.Point(24, 88)
        Me.lblTextItem.Name = "lblTextItem"
        Me.lblTextItem.Size = New System.Drawing.Size(128, 24)
        Me.lblTextItem.TabIndex = 1
        Me.lblTextItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblItem
        '
        Me.lblItem.Location = New System.Drawing.Point(8, 64)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(100, 16)
        Me.lblItem.TabIndex = 0
        Me.lblItem.Text = "Item Description:"
        '
        'SpecialKeyboard
        '
        Me.SpecialKeyboard.BackColor = System.Drawing.Color.SlateGray
        Me.SpecialKeyboard.EnteredString = Nothing
        Me.SpecialKeyboard.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SpecialKeyboard.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SpecialKeyboard.Location = New System.Drawing.Point(32, 296)
        Me.SpecialKeyboard.Name = "SpecialKeyboard"
        Me.SpecialKeyboard.Size = New System.Drawing.Size(680, 296)
        Me.SpecialKeyboard.TabIndex = 6
        '
        'btnOpenDrink
        '
        Me.btnOpenDrink.BackColor = System.Drawing.Color.SlateGray
        Me.btnOpenDrink.Location = New System.Drawing.Point(144, 8)
        Me.btnOpenDrink.Name = "btnOpenDrink"
        Me.btnOpenDrink.Size = New System.Drawing.Size(104, 40)
        Me.btnOpenDrink.TabIndex = 7
        Me.btnOpenDrink.Text = "Open Drink"
        '
        'SpecialFood
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(59, Byte), CType(96, Byte), CType(141, Byte))
        Me.Controls.Add(Me.btnOpenDrink)
        Me.Controls.Add(Me.SpecialKeyboard)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.NumberPadSmall1)
        Me.Controls.Add(Me.btnOpenFood)
        Me.Controls.Add(Me.btnMessage)
        Me.Name = "SpecialFood"
        Me.Size = New System.Drawing.Size(744, 600)
        Me.Panel1.ResumeLayout(False)
        Me.pnlAssociateItem.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther(ByVal sin As Integer, ByVal sii As Integer, ByVal isDrink As Boolean, ByVal assocItem As Boolean)

        Me.NumberPadSmall1.DecimalUsed = True
        Me.SpecialKeyboard.EnteredString = ""
        Me.AssociateSIN = sin


        Dim oRow As DataRow
        For Each oRow In dsOrder.Tables("Functions").Rows
            If oRow("FunctionName") = "Open Food" Then
                If Not oRow("DrinkRoutingID") Is DBNull.Value Then
                    fRouting = oRow("DrinkRoutingID")
                  Else
                    fRouting = 0
                End If

            ElseIf oRow("FunctionName") = "Open Drink" Then
                If Not oRow("DrinkRoutingID") Is DBNull.Value Then
                    dRouting = oRow("DrinkRoutingID")
                Else
                    dRouting = 0
            End If

            ElseIf oRow("FunctionName") = "Special Message" Then
                If Not oRow("DrinkRoutingID") Is DBNull.Value Then
                    mRouting = oRow("DrinkRoutingID")
                Else
                    mRouting = 0
                End If
            End If

        Next


        If isDrink = True And companyInfo.servesMixedDrinks = True Then
            ThisIsOpenDrink()

        Else
            If CurrentRouting = 0 Then
                CurrentRouting = fRouting
            End If
            _functionGroup = 1
            _functionFlag = "F"
            _routingID = CurrentRouting
            specialPurpose = "Food"
            Me.btnOpenFood.BackColor = Color.Red

        End If

        If assocItem = False Then
            Me.associateItem = False
        Else
            Me.associateItem = True
            Me.btnItemAssociation.BackColor = Color.Red
            Me.btnItemSeperate.BackColor = Color.SlateGray
        End If

        FillAssociateListView(sin, sii, isDrink)

    End Sub

    Private Sub FillAssociateListView(ByVal sin As Integer, ByVal sii As Integer, ByVal isDrink As Boolean)
        '   ****************
        '   maybe we should just fill the main items since that's all we can add under

        Dim oRow As DataRow

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("sii") = oRow("sin") Then                       'sii Then
                    'add to list view if oRow("sii") = sii
                    Dim item As New ListViewItem
                    item.Text = oRow("ItemName")    'col 0
                    item.SubItems.Add(oRow("sii"))  'col 1
                    item.SubItems.Add(oRow("RoutingID"))    'col 2

                    If associateItem = True Then
                        If oRow("sin") = sii Then                           'sin Then
                            ' change color and make default assoc. item
                            _associateSIN = oRow("sii")
                            _currentRouting = oRow("RoutingID") '****not sure about this
                            item.ForeColor = Color.Red
                        End If
                    End If

                    Me.lstAssociateItem.Items.Add(item)
                End If
            End If
        Next

    End Sub

    Private Sub btnOpenFood_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFood.Click
        RaiseEvent UC_Hit()

        specialPurpose = "Food"

        If Me.associateItem = True Then
            _functionGroup = 1
            _functionFlag = "F"

            '     _functionID = 2
            _routingID = CurrentRouting
        Else
            _functionGroup = 10
            _functionFlag = "O"

            '       _functionID = 1
            _routingID = fRouting
        End If

        ResetButtonColors()
        If Me.associateItem = True Then
            Me.btnItemAssociation.BackColor = Color.Red
        Else
            Me.btnItemSeperate.BackColor = Color.Red
        End If

        Me.btnItemAssociation.Text = "Associate Item"
        Me.btnItemSeperate.Text = "Seperate Item"
        Me.btnNonAlc.Visible = False
        Me.btnBeer.Visible = False

        Me.btnOpenFood.BackColor = Color.Red
        Me.btnOpenDrink.BackColor = Color.SlateGray
        Me.btnMessage.BackColor = Color.SlateGray
        Me.NumberPadSmall1.Enabled = True

    End Sub

    Private Sub btnOpenDrink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDrink.Click

        RaiseEvent UC_Hit()
        ThisIsOpenDrink()

    End Sub

    Private Sub ThisIsOpenDrink()
        If Me.associateItem = True Then
            _routingID = CurrentRouting
        Else
            If Not specialPurpose = "Drink" Then
                'if already Drink then we keep function the same
                _routingID = dRouting
            End If
        End If

        _functionGroup = 4      '*** this is coding for Liquor..wil nedd to change
        _functionFlag = "D"

        specialPurpose = "Drink"

        Me.btnItemAssociation.Text = "Liquor"
        Me.btnItemSeperate.Text = "Wine"
        Me.btnNonAlc.Visible = True
        Me.btnBeer.Visible = True

        ResetButtonColors()
        Me.btnItemAssociation.BackColor = Color.Red
        Me.btnOpenDrink.BackColor = Color.Red
        Me.btnOpenFood.BackColor = Color.SlateGray
        Me.btnMessage.BackColor = Color.SlateGray
        Me.NumberPadSmall1.Enabled = True

    End Sub
    Private Sub btnMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMessage.Click
        RaiseEvent UC_Hit()

        specialPurpose = "Message"
        If Me.associateItem = True Then
            '      _functionID = 3
            _routingID = CurrentRouting
        Else                    'should probably always be assoc.??
            '         _functionID = 1
            _routingID = mRouting
        End If

        _functionFlag = "O"
        _functionGroup = 8

        Me.btnItemAssociation.Text = "Associate Item"
        Me.btnItemSeperate.Text = "Seperate Item"
        Me.btnNonAlc.Visible = False
        Me.btnBeer.Visible = False

        Me.btnOpenFood.BackColor = Color.SlateGray
        Me.btnOpenDrink.BackColor = Color.SlateGray
        Me.btnMessage.BackColor = Color.Red
        Me.NumberPadSmall1.Enabled = False
        Me.lblPrice.Text = Nothing
        _itemPrice = Nothing

    End Sub

    Private Sub btnItemAssociation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnItemAssociation.Click
        RaiseEvent UC_Hit()

        Me.associateItem = True
        ResetButtonColors()
        Me.btnItemAssociation.BackColor = Color.Red

        If Me.specialPurpose = "Message" Then
            _functionGroup = 8
        ElseIf Me.specialPurpose = "Food" Then
            _functionGroup = 1
        ElseIf Me.specialPurpose = "Drink" Then
            'this is for the Liquor button
            _functionGroup = 4
        End If

    End Sub

    Private Sub btnItemSeperate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnItemSeperate.Click
        RaiseEvent UC_Hit()

        Me.associateItem = False
        _associateSIN = Nothing
        ResetButtonColors()
        Me.btnItemSeperate.BackColor = Color.Red

        Dim sItem As ListViewItem
        For Each sItem In Me.lstAssociateItem.Items
            sItem.ForeColor = Color.Black
        Next

        If Me.specialPurpose = "Message" Then
            _functionGroup = 8
        ElseIf Me.specialPurpose = "Food" Then
            _functionGroup = 1
        ElseIf Me.specialPurpose = "Drink" Then
            'this is for the Wine button
            _functionGroup = 3  '4
        End If

    End Sub

    Private Sub btnNonAlc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNonAlc.Click
        RaiseEvent UC_Hit()

        _functionGroup = 5
        ResetButtonColors()
        Me.btnNonAlc.BackColor = Color.Red

    End Sub

    Private Sub btnBeer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBeer.Click
        RaiseEvent UC_Hit()

        _functionGroup = 2
        ResetButtonColors()
        Me.btnBeer.BackColor = Color.Red

    End Sub

    Private Sub ResetButtonColors()

        Me.btnBeer.BackColor = Color.SlateGray
        Me.btnNonAlc.BackColor = Color.SlateGray
        Me.btnItemAssociation.BackColor = Color.SlateGray
        Me.btnItemSeperate.BackColor = Color.SlateGray
    End Sub

    Private Sub lstAssociateItem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstAssociateItem.SelectedIndexChanged
        RaiseEvent UC_Hit()

        Dim sItem As ListViewItem

        For Each sItem In Me.lstAssociateItem.Items

            If sItem.Selected = True Then
                _associateSIN = CInt(sItem.SubItems(1).Text)
                _currentRouting = CInt(sItem.SubItems(2).Text)
                sItem.ForeColor = Color.Red
            Else
                sItem.ForeColor = Color.Black
            End If
        Next

        btnItemAssociation_Click(sender, e)

    End Sub

    Private Sub ItemDescriptionChanged() Handles SpecialKeyboard.StringChanged
        RaiseEvent UC_Hit()

        Me.lblTextItem.Text = Me.SpecialKeyboard.EnteredString
        Me.ItemDescription = Me.SpecialKeyboard.EnteredString

    End Sub

    Private Sub ItemPriceChanged() Handles NumberPadSmall1.NumberChanged
        RaiseEvent UC_Hit()

        Me.lblTextPrice.Text = Me.NumberPadSmall1.NumberTotal
        Me.ItemPrice = Me.NumberPadSmall1.NumberTotal

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        RaiseEvent CancelSpecial(sender, e)

        Me.Dispose()

    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click

        If Me.associateItem = False Then
            Me.AssociateSIN = Nothing
        End If

        RaiseEvent AcceptSpecial(sender, e)

    End Sub



End Class
