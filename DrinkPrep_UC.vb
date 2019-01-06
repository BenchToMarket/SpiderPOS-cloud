Imports DataSet_Builder

Public Class DrinkPrep_UC
    Inherits System.Windows.Forms.UserControl

    Dim modifierIndex As Integer
    Dim prepIndex As Integer
    Dim _drinkCatID As Integer
    Dim _drinkID As Integer
    Dim _drinkRoute As Integer
    Dim _drinkFunctionID As Integer
    Dim _drinkFunGroup As Integer

    Dim _foodCatID As Integer
    Dim _foodID As Integer
    Dim _foodRoute As Integer
    Dim _foodFunctionID As Integer
    Dim _foodFunGroup As Integer
    Dim _modifierCatID As Integer

    Dim _selectedRawItemID As Integer
    Dim _selectedRawItemName As String
    Dim _selectedRawPriceTotal As Decimal
    Dim _selectedRawTrackAs As Integer

    Dim _associateSIN As Integer
    Dim whatWereDoing As String
    Dim isForDrink As Boolean
    Dim maxmenuindex As Integer
    Dim isExtended As Boolean


    Public Property DrinkCatID() As Integer
        Get
            Return _drinkCatID
        End Get
        Set(ByVal Value As Integer)
            _drinkCatID = Value
        End Set
    End Property

    Public Property DrinkID() As Integer
        Get
            Return _drinkID
        End Get
        Set(ByVal Value As Integer)
            _drinkID = Value
        End Set
    End Property

    Public Property DrinkRoute() As Integer
        Get
            Return _drinkRoute
        End Get
        Set(ByVal Value As Integer)
            _drinkRoute = Value
        End Set
    End Property

    Public Property DrinkFuctionID() As Integer
        Get
            Return _drinkFunctionID
        End Get
        Set(ByVal value As Integer)
            _drinkFunctionID = value
        End Set
    End Property

    Public Property DrinkFunGroup() As Integer
        Get
            Return _drinkFunGroup
        End Get
        Set(ByVal value As Integer)
            _drinkFunGroup = value
        End Set
    End Property

    Public Property FoodCatID() As Integer
        Get
            Return _foodCatID
        End Get
        Set(ByVal Value As Integer)
            _foodCatID = Value
        End Set
    End Property

    Public Property FoodID() As Integer
        Get
            Return _foodID
        End Get
        Set(ByVal Value As Integer)
            _foodID = Value
        End Set
    End Property

    Public Property FoodRoute() As Integer
        Get
            Return _foodRoute
        End Get
        Set(ByVal Value As Integer)
            _foodRoute = Value
        End Set
    End Property

    Public Property FoodFuctionID() As Integer
        Get
            Return _foodFunctionID
        End Get
        Set(ByVal value As Integer)
            _foodFunctionID = value
        End Set
    End Property

    Public Property FoodFunGroup() As Integer
        Get
            Return _foodFunGroup
        End Get
        Set(ByVal value As Integer)
            _foodFunGroup = value
        End Set
    End Property

    Public Property ModifierCatID() As Integer
        Get
            Return _modifierCatID
        End Get
        Set(ByVal Value As Integer)
            _modifierCatID = Value
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

    Public Property AssociateSIN() As Integer
        Get
            Return _associateSIN
        End Get
        Set(ByVal Value As Integer)
            _associateSIN = Value
        End Set
    End Property

    Private btnOrderDrinkAdds(32) As OrderButton
    Friend WithEvents pnlExtraNo As System.Windows.Forms.Panel
    Friend WithEvents lstIngredientsExtraNo As System.Windows.Forms.ListView
    Friend WithEvents cRawItemID As System.Windows.Forms.ColumnHeader
    Friend WithEvents cRawItemName As System.Windows.Forms.ColumnHeader
    Friend WithEvents cRawPriceTotal As System.Windows.Forms.ColumnHeader
    Friend WithEvents cRawTrackAs As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstIngredientsAll As System.Windows.Forms.ListView
    Friend WithEvents lstAllBreak As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstAllUnit As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstAllItemName As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private btnOrderDrinkPrep(32) As OrderButton


    Event AcceptPrep(ByVal currentItem As SelectedItemDetail)
    Event Cancel()



#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        CreateOrderDrinkButtonArray()
        CreateOrderDrinkPrepArray()

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
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnExtra As System.Windows.Forms.Button
    Friend WithEvents pnlModifiers As System.Windows.Forms.Panel
    Friend WithEvents pnlChangeModifiers As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblPrep As System.Windows.Forms.Label
    Friend WithEvents btnDone As System.Windows.Forms.Button
    Friend WithEvents btnNo As System.Windows.Forms.Button
    Friend WithEvents btnModifierDown As System.Windows.Forms.Button
    Friend WithEvents btnModifierUp As System.Windows.Forms.Button
    Friend WithEvents pnlModiferiArrows As System.Windows.Forms.Panel
    Friend WithEvents pnlPrep As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnPrepDown As System.Windows.Forms.Button
    Friend WithEvents btnPrepUp As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DrinkPrep_UC))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.pnlPrep = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.btnPrepDown = New System.Windows.Forms.Button
        Me.btnPrepUp = New System.Windows.Forms.Button
        Me.lblPrep = New System.Windows.Forms.Label
        Me.pnlModifiers = New System.Windows.Forms.Panel
        Me.pnlExtraNo = New System.Windows.Forms.Panel
        Me.lstIngredientsAll = New System.Windows.Forms.ListView
        Me.lstAllBreak = New System.Windows.Forms.ColumnHeader
        Me.lstAllUnit = New System.Windows.Forms.ColumnHeader
        Me.lstAllItemName = New System.Windows.Forms.ColumnHeader
        Me.lstIngredientsExtraNo = New System.Windows.Forms.ListView
        Me.cRawItemID = New System.Windows.Forms.ColumnHeader
        Me.cRawItemName = New System.Windows.Forms.ColumnHeader
        Me.cRawPriceTotal = New System.Windows.Forms.ColumnHeader
        Me.cRawTrackAs = New System.Windows.Forms.ColumnHeader
        Me.pnlChangeModifiers = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlModiferiArrows = New System.Windows.Forms.Panel
        Me.btnModifierDown = New System.Windows.Forms.Button
        Me.btnModifierUp = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnDone = New System.Windows.Forms.Button
        Me.btnNo = New System.Windows.Forms.Button
        Me.btnExtra = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.pnlPrep.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlModifiers.SuspendLayout()
        Me.pnlExtraNo.SuspendLayout()
        Me.pnlChangeModifiers.SuspendLayout()
        Me.pnlModiferiArrows.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.pnlPrep)
        Me.Panel1.Controls.Add(Me.pnlModifiers)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(560, 552)
        Me.Panel1.TabIndex = 0
        '
        'pnlPrep
        '
        Me.pnlPrep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlPrep.Controls.Add(Me.Panel4)
        Me.pnlPrep.Location = New System.Drawing.Point(368, 4)
        Me.pnlPrep.Name = "pnlPrep"
        Me.pnlPrep.Size = New System.Drawing.Size(189, 480)
        Me.pnlPrep.TabIndex = 2
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.Panel3)
        Me.Panel4.Controls.Add(Me.lblPrep)
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(188, 48)
        Me.Panel4.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.btnPrepDown)
        Me.Panel3.Controls.Add(Me.btnPrepUp)
        Me.Panel3.Location = New System.Drawing.Point(88, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(96, 48)
        Me.Panel3.TabIndex = 2
        '
        'btnPrepDown
        '
        Me.btnPrepDown.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnPrepDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrepDown.Image = CType(resources.GetObject("btnPrepDown.Image"), System.Drawing.Image)
        Me.btnPrepDown.Location = New System.Drawing.Point(4, 0)
        Me.btnPrepDown.Name = "btnPrepDown"
        Me.btnPrepDown.Size = New System.Drawing.Size(40, 40)
        Me.btnPrepDown.TabIndex = 1
        Me.btnPrepDown.UseVisualStyleBackColor = False
        '
        'btnPrepUp
        '
        Me.btnPrepUp.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnPrepUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrepUp.Image = CType(resources.GetObject("btnPrepUp.Image"), System.Drawing.Image)
        Me.btnPrepUp.Location = New System.Drawing.Point(48, 0)
        Me.btnPrepUp.Name = "btnPrepUp"
        Me.btnPrepUp.Size = New System.Drawing.Size(40, 40)
        Me.btnPrepUp.TabIndex = 2
        Me.btnPrepUp.UseVisualStyleBackColor = False
        '
        'lblPrep
        '
        Me.lblPrep.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrep.ForeColor = System.Drawing.Color.White
        Me.lblPrep.Location = New System.Drawing.Point(12, 12)
        Me.lblPrep.Name = "lblPrep"
        Me.lblPrep.Size = New System.Drawing.Size(68, 24)
        Me.lblPrep.TabIndex = 0
        Me.lblPrep.Text = "Prep"
        Me.lblPrep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlModifiers
        '
        Me.pnlModifiers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlModifiers.Controls.Add(Me.pnlExtraNo)
        Me.pnlModifiers.Controls.Add(Me.pnlChangeModifiers)
        Me.pnlModifiers.Location = New System.Drawing.Point(4, 4)
        Me.pnlModifiers.Name = "pnlModifiers"
        Me.pnlModifiers.Size = New System.Drawing.Size(360, 480)
        Me.pnlModifiers.TabIndex = 1
        '
        'pnlExtraNo
        '
        Me.pnlExtraNo.Controls.Add(Me.Label3)
        Me.pnlExtraNo.Controls.Add(Me.lstIngredientsAll)
        Me.pnlExtraNo.Controls.Add(Me.lstIngredientsExtraNo)
        Me.pnlExtraNo.Location = New System.Drawing.Point(3, 52)
        Me.pnlExtraNo.Name = "pnlExtraNo"
        Me.pnlExtraNo.Size = New System.Drawing.Size(350, 421)
        Me.pnlExtraNo.TabIndex = 1
        '
        'lstIngredientsAll
        '
        Me.lstIngredientsAll.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.lstIngredientsAll.BackColor = System.Drawing.Color.LightSlateGray
        Me.lstIngredientsAll.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstIngredientsAll.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lstAllBreak, Me.lstAllUnit, Me.lstAllItemName})
        Me.lstIngredientsAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstIngredientsAll.ForeColor = System.Drawing.Color.Black
        Me.lstIngredientsAll.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstIngredientsAll.Location = New System.Drawing.Point(198, 157)
        Me.lstIngredientsAll.Name = "lstIngredientsAll"
        Me.lstIngredientsAll.Size = New System.Drawing.Size(149, 232)
        Me.lstIngredientsAll.TabIndex = 4
        Me.lstIngredientsAll.UseCompatibleStateImageBehavior = False
        Me.lstIngredientsAll.View = System.Windows.Forms.View.Details
        '
        'lstAllBreak
        '
        Me.lstAllBreak.Text = ""
        Me.lstAllBreak.Width = 20
        '
        'lstAllUnit
        '
        Me.lstAllUnit.Text = ""
        Me.lstAllUnit.Width = 35
        '
        'lstAllItemName
        '
        Me.lstAllItemName.Text = ""
        Me.lstAllItemName.Width = 90
        '
        'lstIngredientsExtraNo
        '
        Me.lstIngredientsExtraNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lstIngredientsExtraNo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cRawItemID, Me.cRawItemName, Me.cRawPriceTotal, Me.cRawTrackAs})
        Me.lstIngredientsExtraNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstIngredientsExtraNo.ForeColor = System.Drawing.Color.White
        Me.lstIngredientsExtraNo.FullRowSelect = True
        Me.lstIngredientsExtraNo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstIngredientsExtraNo.Location = New System.Drawing.Point(3, 21)
        Me.lstIngredientsExtraNo.MultiSelect = False
        Me.lstIngredientsExtraNo.Name = "lstIngredientsExtraNo"
        Me.lstIngredientsExtraNo.Size = New System.Drawing.Size(189, 368)
        Me.lstIngredientsExtraNo.TabIndex = 1
        Me.lstIngredientsExtraNo.UseCompatibleStateImageBehavior = False
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
        Me.cRawItemName.Width = 135
        '
        'cRawPriceTotal
        '
        Me.cRawPriceTotal.Text = "Price"
        Me.cRawPriceTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.cRawPriceTotal.Width = 50
        '
        'cRawTrackAs
        '
        Me.cRawTrackAs.Text = ""
        Me.cRawTrackAs.Width = 0
        '
        'pnlChangeModifiers
        '
        Me.pnlChangeModifiers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlChangeModifiers.Controls.Add(Me.Label1)
        Me.pnlChangeModifiers.Controls.Add(Me.pnlModiferiArrows)
        Me.pnlChangeModifiers.Location = New System.Drawing.Point(0, 0)
        Me.pnlChangeModifiers.Name = "pnlChangeModifiers"
        Me.pnlChangeModifiers.Size = New System.Drawing.Size(356, 48)
        Me.pnlChangeModifiers.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(20, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(164, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Modifiers"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlModiferiArrows
        '
        Me.pnlModiferiArrows.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlModiferiArrows.Controls.Add(Me.btnModifierDown)
        Me.pnlModiferiArrows.Controls.Add(Me.btnModifierUp)
        Me.pnlModiferiArrows.Location = New System.Drawing.Point(244, 0)
        Me.pnlModiferiArrows.Name = "pnlModiferiArrows"
        Me.pnlModiferiArrows.Size = New System.Drawing.Size(96, 48)
        Me.pnlModiferiArrows.TabIndex = 1
        '
        'btnModifierDown
        '
        Me.btnModifierDown.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnModifierDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModifierDown.Image = CType(resources.GetObject("btnModifierDown.Image"), System.Drawing.Image)
        Me.btnModifierDown.Location = New System.Drawing.Point(4, 0)
        Me.btnModifierDown.Name = "btnModifierDown"
        Me.btnModifierDown.Size = New System.Drawing.Size(40, 40)
        Me.btnModifierDown.TabIndex = 1
        Me.btnModifierDown.UseVisualStyleBackColor = False
        '
        'btnModifierUp
        '
        Me.btnModifierUp.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnModifierUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModifierUp.Image = CType(resources.GetObject("btnModifierUp.Image"), System.Drawing.Image)
        Me.btnModifierUp.Location = New System.Drawing.Point(48, 0)
        Me.btnModifierUp.Name = "btnModifierUp"
        Me.btnModifierUp.Size = New System.Drawing.Size(40, 40)
        Me.btnModifierUp.TabIndex = 2
        Me.btnModifierUp.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Black
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.btnDone)
        Me.Panel2.Controls.Add(Me.btnNo)
        Me.Panel2.Controls.Add(Me.btnExtra)
        Me.Panel2.Controls.Add(Me.btnAdd)
        Me.Panel2.Location = New System.Drawing.Point(8, 492)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(544, 52)
        Me.Panel2.TabIndex = 0
        '
        'btnDone
        '
        Me.btnDone.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnDone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDone.Location = New System.Drawing.Point(452, 4)
        Me.btnDone.Name = "btnDone"
        Me.btnDone.Size = New System.Drawing.Size(76, 40)
        Me.btnDone.TabIndex = 4
        Me.btnDone.Text = "DONE"
        Me.btnDone.UseVisualStyleBackColor = False
        '
        'btnNo
        '
        Me.btnNo.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNo.Location = New System.Drawing.Point(132, 4)
        Me.btnNo.Name = "btnNo"
        Me.btnNo.Size = New System.Drawing.Size(80, 40)
        Me.btnNo.TabIndex = 3
        Me.btnNo.Text = "NO"
        Me.btnNo.UseVisualStyleBackColor = False
        '
        'btnExtra
        '
        Me.btnExtra.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnExtra.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExtra.Location = New System.Drawing.Point(244, 4)
        Me.btnExtra.Name = "btnExtra"
        Me.btnExtra.Size = New System.Drawing.Size(72, 40)
        Me.btnExtra.TabIndex = 1
        Me.btnExtra.Text = "EXTRA"
        Me.btnExtra.UseVisualStyleBackColor = False
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(28, 4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(76, 40)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "ADD"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(242, 127)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Reciepe"
        '
        'DrinkPrep_UC
        '
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel1)
        Me.Name = "DrinkPrep_UC"
        Me.Size = New System.Drawing.Size(568, 560)
        Me.Panel1.ResumeLayout(False)
        Me.pnlPrep.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlModifiers.ResumeLayout(False)
        Me.pnlExtraNo.ResumeLayout(False)
        Me.pnlExtraNo.PerformLayout()
        Me.pnlChangeModifiers.ResumeLayout(False)
        Me.pnlModiferiArrows.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Friend Sub StartDrinkPrep(ByVal dID As Integer, ByVal catID As Integer, ByVal rID As Integer, ByVal funID As Integer, ByVal funGroup As Integer)

        lblPrep.Visible = True
        pnlExtraNo.Visible = False
        modifierIndex = 0
        prepIndex = 0
        _drinkCatID = catID
        _drinkID = dID
        _drinkRoute = rID
        _drinkFunctionID = funID
        _drinkFunGroup = funGroup
        PopulateDrinkSubCategory()
        PopulateDrinkPrep()
        whatWereDoing = "ADD"
        ResetButtonColors()
        isForDrink = True
     
    End Sub

    Friend Sub StartAddNo(ByVal dID As Integer, ByVal catID As Integer, ByVal rID As Integer, ByVal funID As Integer, ByVal funGroup As Integer)

        Dim hasIngredientTest As Boolean = False
        Dim index As Integer

        lblPrep.Visible = False
        modifierIndex = 0
        prepIndex = 0
        _foodCatID = catID
        _foodID = dID
        _foodRoute = rID
        _foodFunctionID = funID
        _foodFunGroup = funGroup

        PopulateAddNoCategories()
        whatWereDoing = currentTable.OrderingStatus
        isForDrink = False
        isExtended = False
        maxmenuindex = 0
        ResetButtonColors()

        If whatWereDoing = "EXTRA" Or whatWereDoing = "ADD" Then
            If dvIngredientsEXTRA.Count > 0 Then
                hasIngredientTest = True
            End If
        Else
            If dvIngredientsNO.Count > 0 Then
                hasIngredientTest = True
            End If
        End If

        If hasIngredientTest = True Then
            PopulateExtraNoListView()
            pnlExtraNo.Visible = True
            Exit Sub
        Else
            pnlExtraNo.Visible = False
        End If

        If dvCategoryJoin.Count = 1 Then
            isExtended = dvCategoryJoin(0)("Extended")
            _modifierCatID = dvCategoryJoin(0)("CategoryID")
            PrepareForAddNo(dvCategoryJoin(0)("FunctionFlag"))
            PopulateAddNoCategory()
        Else
            For index = 1 To 32
                BlankDrinkAddButton(index)
            Next
        End If

    End Sub


    Private Sub CreateOrderDrinkButtonArray()

        Dim index As Integer
        Dim x As Integer = buttonSpace
        Dim y As Integer = pnlChangeModifiers.Height + buttonSpace
        Dim count As Integer
        Dim drinkButtonWidth As Single = (pnlModifiers.Width - (6 * buttonSpace)) / 4  '(opHeight - (13 * buttonSpace)) / 12
        Dim drinkButtonHeight As Single = (pnlModifiers.Height - (10 * buttonSpace) - pnlChangeModifiers.Height) / 8 '(opHeight - (13 * buttonSpace)) / 12

        For index = 1 To 32

            btnOrderDrinkAdds(index) = New OrderButton("8")

            With btnOrderDrinkAdds(index)
                .Size() = New Size(drinkButtonWidth, drinkButtonHeight)
                .Location = New Point(x, y)
                AddHandler btnOrderDrinkAdds(index).Click, AddressOf BtnOrderDrinkModifier_Click   'BtnOrderDrink_Click
            End With

            count = count + 1
            If count < 4 Then

                x = x + drinkButtonWidth + buttonSpace
            Else
                x = buttonSpace
                y = y + drinkButtonHeight + buttonSpace
                count = 0
            End If
        Next

        For index = 32 To 1 Step -1
            pnlModifiers.Controls.Add(btnOrderDrinkAdds(index))
        Next

    End Sub

    Private Sub CreateOrderDrinkPrepArray()

        Dim index As Integer
        Dim x As Integer = buttonSpace
        Dim y As Integer = pnlChangeModifiers.Height + buttonSpace
        Dim count As Integer
        Dim drinkButtonWidth As Single = (pnlPrep.Width - (4 * buttonSpace)) / 2  '(opHeight - (13 * buttonSpace)) / 12
        Dim drinkButtonHeight As Single = (pnlPrep.Height - (10 * buttonSpace) - pnlChangeModifiers.Height) / 8 '(opHeight - (13 * buttonSpace)) / 12

        For index = 1 To 16

            btnOrderDrinkPrep(index) = New OrderButton("8")

            With btnOrderDrinkPrep(index)
                .Size() = New Size(drinkButtonWidth, drinkButtonHeight)
                .Location = New Point(x, y)
                AddHandler btnOrderDrinkPrep(index).Click, AddressOf BtnOrderPrep_Click   'BtnOrderDrink_Click
            End With

            count = count + 1
            If count < 2 Then

                x = x + drinkButtonWidth + buttonSpace
            Else
                x = buttonSpace
                y = y + drinkButtonHeight + buttonSpace
                count = 0
            End If
        Next

        For index = 16 To 1 Step -1
            pnlPrep.Controls.Add(btnOrderDrinkPrep(index))
        Next

    End Sub

    Private Sub BtnOrderDrinkModifier_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles pnlOrder.Click

        Dim objButton As OrderButton
        Try
            objButton = CType(sender, OrderButton)
        Catch ex As Exception
            Exit Sub
        End Try

        Dim currentItem As New SelectedItemDetail
      
        With currentItem

            If whatWereDoing = "ADD" Then
                If isForDrink = True Then
                    .Name = "   " & objButton.Text
                    .TerminalName = "   " & objButton.Text
                    .ChitName = "   " & objButton.Text
                Else
                    .Name = "   " & objButton.Text
                    .TerminalName = "   *** " & whatWereDoing & "  " & objButton.Text
                    .ChitName = "   *** " & whatWereDoing & "  " & objButton.Text
                End If
              
                .Quantity = currentTable.Quantity
                .ItemPrice = objButton.Price
                .Price = objButton.Price * currentTable.Quantity

            ElseIf whatWereDoing = "NO" Then
                .Name = "   " & objButton.Text
                .TerminalName = "   *** " & whatWereDoing & "  " & objButton.Text
                .ChitName = "   *** " & whatWereDoing & "  " & objButton.Text
                .Quantity = currentTable.Quantity * -1
                .ItemPrice = 0
                .Price = 0
            ElseIf whatWereDoing = "EXTRA" Then
                .Name = "   " & objButton.Text
                .TerminalName = "   *** " & whatWereDoing & "  " & objButton.Text
                .ChitName = "   *** " & whatWereDoing & "  " & objButton.Text
                .Quantity = currentTable.Quantity
                .ItemPrice = objButton.Price
                .Price = objButton.Price * currentTable.Quantity
            End If

            .ID = objButton.ID
            .Category = objButton.CategoryID
            If isForDrink = True Then
                ' this is for Drink Preps, we record these differntly
                .DrinkID = objButton.ID
            End If
            .RoutingID = DrinkRoute
            .FunctionID = objButton.Functions
            .FunctionGroup = objButton.FunctionGroup
            .FunctionFlag = objButton.FunctionFlag

            If currentTable.si2 > 1 And currentTable.si2 < 10 Then
                'half order
                .Quantity *= 0.5
                .ItemPrice *= 0.5
                .Price *= 0.5
            End If

        End With

        RaiseEvent AcceptPrep(currentItem)

    End Sub

    Private Sub BtnOrderPrep_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles pnlOrder.Click

        Dim objButton As OrderButton
        Try
            objButton = CType(sender, OrderButton)
        Catch ex As Exception
            Exit Sub
        End Try

        If isForDrink = True Then
            Dim currentItem As New SelectedItemDetail
            currentTable.InvMultiplier *= objButton.InvMultiplier

            With currentItem

                .ID = -1
                If whatWereDoing = "ADD" Then
                    .Name = "   " & objButton.Text
                    .TerminalName = "   " & objButton.Text
                    .ChitName = "   " & objButton.Text

                    .Quantity = currentTable.Quantity
                    .ItemPrice = objButton.Price
                    .Price = objButton.Price * currentTable.Quantity

                ElseIf whatWereDoing = "NO" Then
                    .Name = "   " & objButton.Text
                    .TerminalName = "   *** " & whatWereDoing & "  " & objButton.Text
                    .ChitName = "   *** " & whatWereDoing & "  " & objButton.Text
                    .Quantity = currentTable.Quantity * -1
                    .ItemPrice = 0
                    .Price = 0

                ElseIf whatWereDoing = "EXTRA" Then
                    .Name = "   " & objButton.Text
                    .TerminalName = "   *** " & whatWereDoing & "  " & objButton.Text
                    .ChitName = "   *** " & whatWereDoing & "  " & objButton.Text
                    .Quantity = currentTable.Quantity
                    .ItemPrice = objButton.Price
                    .Price = objButton.Price * currentTable.Quantity
                End If

                .dpMethod = objButton.dpMethod
                .RoutingID = DrinkRoute
                .FunctionID = DrinkFuctionID
                .FunctionGroup = DrinkFunGroup
                .FunctionFlag = "D" 'objButton.FunctionFlag

                If Not objButton.Price > 0 Then   'objButton.InvMultiplier = 1 Then    '
                    ' this is Prep, so this will be Double or addition to alcohol
                    .DrinkID = Me.DrinkID
                    .Category = Me.DrinkCatID
                Else
                    .DrinkID = objButton.ID
                    .Category = objButton.CategoryID
                End If
            End With

            RaiseEvent AcceptPrep(currentItem)
        Else
            'Add No for food items
         
            pnlExtraNo.Visible = False
            isExtended = objButton.Extended
            _modifierCatID = objButton.CategoryID

            PrepareForAddNo(objButton.FunctionFlag)
            PopulateAddNoCategory()
        End If

    End Sub


    Private Sub PopulateDrinkSubCategory()

        Dim index As Integer
        Dim secondIndex As Integer = modifierIndex
        Dim oRow As DataRow

        For index = 1 To 32
            If secondIndex < dtDrinkAdds.Rows.Count Then
                btnOrderDrinkAdds(index).Text = dtDrinkAdds.Rows(secondIndex)("DrinkName")
                btnOrderDrinkAdds(index).ID = dtDrinkAdds.Rows(secondIndex)("DrinkID")
                btnOrderDrinkAdds(index).Price = dtDrinkAdds.Rows(secondIndex)("AddOnPrice")
                btnOrderDrinkAdds(index).CategoryID = DrinkCatID
                btnOrderDrinkAdds(index).SubCategory = True
                btnOrderDrinkAdds(index).Functions = dtDrinkAdds.Rows(secondIndex)("DrinkFunctionID")
                btnOrderDrinkAdds(index).FunctionGroup = dtDrinkAdds.Rows(secondIndex)("FunctionGroupID")
                btnOrderDrinkAdds(index).FunctionFlag = dtDrinkAdds.Rows(secondIndex)("FunctionFlag")
                btnOrderDrinkAdds(index).DrinkAdds = True
                btnOrderDrinkAdds(index).BackColor = c16
                btnOrderDrinkAdds(index).ForeColor = c3
                'not here      btnOrderDrinkAdds(index).InvMultiplier = dtDrinkAdds.Rows(index)("InvMultiplier")
            Else
                btnOrderDrinkAdds(index).Text = Nothing
                btnOrderDrinkAdds(index).ID = 0
                btnOrderDrinkAdds(index).BackColor = c13
            End If
            secondIndex += 1
        Next

    End Sub

    Private Sub PrepareForAddNo(ByVal funFlag As String)

        Dim populatingTable As String

        If funFlag = "M" Then
            populatingTable = "ModifierTable"
        Else
            If currentTable.IsPrimaryMenu = True Then
                populatingTable = "MainTable"
            Else
                populatingTable = "SecondaryMainTable"
            End If
        End If

        With dvCategoryModifiers
            .Table = ds.Tables(populatingTable & ModifierCatID)
            .Sort = "CategoryID, FoodName"
        End With

        If isExtended = True Then
            maxmenuindex = 0 'we don't care, maxmenuindex does not matter
        Else
            If ds.Tables(populatingTable & ModifierCatID).Rows.Count > 0 Then
                maxmenuindex = ds.Tables(populatingTable & ModifierCatID).Compute("Max(MenuIndex)", "")
            Else
                maxmenuindex = 0
            End If
        End If
    End Sub
    Private Sub PopulateAddNoCategory() 'ByVal catID As Integer, ByVal funFlag As String)

        Dim index As Integer
        Dim secondIndex As Integer = modifierIndex
        Dim vRow As DataRowView

        If isExtended = True Or maxmenuindex > 32 Then
            For index = 1 To 32
                If secondIndex < dvCategoryModifiers.Count Then
                    vRow = dvCategoryModifiers(secondIndex)
                    btnOrderDrinkAdds(index).Text = dvCategoryModifiers(secondIndex)("FoodName")
                    btnOrderDrinkAdds(index).ID = dvCategoryModifiers(secondIndex)("FoodID")
                    If dvCategoryModifiers(secondIndex)("Surcharge") Is DBNull.Value Then
                        btnOrderDrinkAdds(index).Price = 0
                    Else
                        btnOrderDrinkAdds(index).Price = dvCategoryModifiers(secondIndex)("Surcharge") '("AddOnPrice")
                    End If
                    btnOrderDrinkAdds(index).CategoryID = DrinkCatID
                    btnOrderDrinkAdds(index).SubCategory = True
                    btnOrderDrinkAdds(index).Functions = dvCategoryModifiers(secondIndex)("FunctionID")
                    btnOrderDrinkAdds(index).FunctionGroup = dvCategoryModifiers(secondIndex)("FunctionGroupID")
                    btnOrderDrinkAdds(index).FunctionFlag = dvCategoryModifiers(secondIndex)("FunctionFlag")
                    btnOrderDrinkAdds(index).DrinkAdds = True
                    If dvCategoryModifiers(secondIndex)("ButtonColor") Is DBNull.Value Then
                        btnOrderDrinkAdds(index).BackColor = c16
                        btnOrderDrinkAdds(index).ForeColor = c3
                    Else
                        btnOrderDrinkAdds(index).BackColor = Color.FromArgb(dvCategoryModifiers(secondIndex)("ButtonColor"))
                        btnOrderDrinkAdds(index).ForeColor = Color.FromArgb(dvCategoryModifiers(secondIndex)("ButtonForeColor"))
                    End If
                         'not here      btnOrderDrinkAdds(index).InvMultiplier = dtDrinkAdds.Rows(index)("InvMultiplier")
                Else
                    btnOrderDrinkAdds(index).Text = Nothing
                    btnOrderDrinkAdds(index).ID = 0
                    btnOrderDrinkAdds(index).BackColor = c13
                End If
                secondIndex += 1
            Next
        Else
            'will not be here if MenuIndex > 32
            For index = 1 To 32
                BlankDrinkAddButton(index)
            Next
            For Each vRow In dvCategoryModifiers
                If vRow("MenuIndex") > 0 Then
                    PopulateDrinkAddButton(vRow("MenuIndex"), vRow)
                End If
            Next
        End If

    End Sub

    Private Sub PopulateDrinkAddButton(ByVal index As Integer, ByRef vRow As DataRowView)

        btnOrderDrinkAdds(index).Text = vRow("FoodName")
        btnOrderDrinkAdds(index).ID = vRow("FoodID")
        If vRow("Surcharge") Is DBNull.Value Then
            btnOrderDrinkAdds(index).Price = 0
        Else
            btnOrderDrinkAdds(index).Price = vRow("Surcharge") '("AddOnPrice")
        End If
        btnOrderDrinkAdds(index).CategoryID = DrinkCatID
        btnOrderDrinkAdds(index).SubCategory = True
        btnOrderDrinkAdds(index).Functions = vRow("FunctionID")
        btnOrderDrinkAdds(index).FunctionGroup = vRow("FunctionGroupID")
        btnOrderDrinkAdds(index).FunctionFlag = vRow("FunctionFlag")
        btnOrderDrinkAdds(index).DrinkAdds = True
        If vRow("ButtonColor") Is DBNull.Value Then
            btnOrderDrinkAdds(index).BackColor = c16
            btnOrderDrinkAdds(index).ForeColor = c3
        Else
            btnOrderDrinkAdds(index).BackColor = Color.FromArgb(vRow("ButtonColor"))
            btnOrderDrinkAdds(index).ForeColor = Color.FromArgb(vRow("ButtonForeColor"))
        End If

        'not here      btnOrderDrinkAdds(index).InvMultiplier = dtDrinkAdds.Rows(index)("InvMultiplier")

    End Sub

    Private Sub BlankDrinkAddButton(ByVal index As Integer)
        btnOrderDrinkAdds(index).Text = Nothing
        btnOrderDrinkAdds(index).ID = 0
        btnOrderDrinkAdds(index).BackColor = c13
    End Sub


    Private Sub btnModifierDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModifierDown.Click

        If isForDrink = True Then
            If dtDrinkAdds.Rows.Count > (modifierIndex + 32) Then
                modifierIndex += 32
                PopulateDrinkSubCategory()
            End If
        Else
            If dvCategoryModifiers.Count > (modifierIndex + 32) Then
                modifierIndex += 32
                PopulateAddNoCategory()
            End If
        End If

    End Sub

    Private Sub btnModifierUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModifierUp.Click

        If modifierIndex > 1 Then
            modifierIndex -= 32
            If isForDrink = True Then
                PopulateDrinkSubCategory()
            Else
                PopulateAddNoCategory()
            End If
        End If

    End Sub

    Private Sub PopulateDrinkPrep()

        Dim index As Integer
        Dim secondIndex As Integer = prepIndex
        Dim oRow As DataRow

        For index = 1 To 16
            If secondIndex < dtDrinkPrep.Rows.Count Then
                btnOrderDrinkPrep(index).Text = dtDrinkPrep.Rows(secondIndex)("DrinkPrepName")
                btnOrderDrinkPrep(index).ID = dtDrinkPrep.Rows(secondIndex)("DrinkPrepID")
                btnOrderDrinkPrep(index).dpMethod = dtDrinkPrep.Rows(secondIndex)("DrinkPrepMethod")
                btnOrderDrinkPrep(index).Price = dtDrinkPrep.Rows(secondIndex)("DrinkPrepPrice")
                If Not dtDrinkPrep.Rows(secondIndex)("InvMultiplier") Is DBNull.Value Then
                    btnOrderDrinkPrep(index).InvMultiplier = dtDrinkPrep.Rows(secondIndex)("InvMultiplier")
                Else
                    btnOrderDrinkPrep(index).InvMultiplier = 1
                End If
                btnOrderDrinkPrep(index).CategoryID = DrinkCatID
                btnOrderDrinkPrep(index).SubCategory = True
                '   btnOrderDrinkPrep(index).Functions = dtDrinkPrep.Rows(secondIndex)("DrinkFunctionID")
                btnOrderDrinkPrep(index).FunctionGroup = 11 'dtDrinkPrep.Rows(secondIndex)("FunctionGroupID")
                btnOrderDrinkPrep(index).FunctionFlag = "D" 'dtDrinkPrep.Rows(secondIndex)("FunctionFlag")
                btnOrderDrinkPrep(index).DrinkAdds = True
                btnOrderDrinkPrep(index).BackColor = c16
                btnOrderDrinkPrep(index).ForeColor = c3
            Else
                btnOrderDrinkPrep(index).Text = Nothing
                btnOrderDrinkPrep(index).ID = 0
                btnOrderDrinkPrep(index).BackColor = c13
            End If
            secondIndex += 1
        Next

    End Sub


    Private Sub PopulateAddNoCategories()

        Dim index As Integer
        Dim secondIndex As Integer = prepIndex
        Dim oRow As DataRow

        For index = 1 To 16
            If secondIndex < dtModifierCategory.Rows.Count Then
                btnOrderDrinkPrep(index).Text = dtModifierCategory.Rows(secondIndex)("CategoryAbrev")
                btnOrderDrinkPrep(index).CategoryID = dtModifierCategory.Rows(secondIndex)("CategoryID")
                '    btnOrderDrinkPrep(index).SubCategory = True
                btnOrderDrinkPrep(index).FunctionGroup = dtModifierCategory.Rows(secondIndex)("FunctionGroupID")
                btnOrderDrinkPrep(index).FunctionFlag = dtModifierCategory.Rows(secondIndex)("FunctionFlag")
                btnOrderDrinkPrep(index).Extended = dtModifierCategory.Rows(secondIndex)("Extended")
                '    dtModifierCategory.Rows(secondIndex)("CategoryAbrev")
                If dtModifierCategory.Rows(secondIndex)("ButtonColor") Is DBNull.Value Then
                    btnOrderDrinkPrep(index).BackColor = c7
                    btnOrderDrinkPrep(index).ForeColor = c3
                Else
                    btnOrderDrinkPrep(index).BackColor = Color.FromArgb(dtModifierCategory.Rows(secondIndex)("ButtonColor"))
                    btnOrderDrinkPrep(index).ForeColor = Color.FromArgb(dtModifierCategory.Rows(secondIndex)("ButtonForeColor"))

                End If
            Else

                btnOrderDrinkPrep(index).Text = Nothing
                btnOrderDrinkPrep(index).ID = 0
                btnOrderDrinkPrep(index).BackColor = c13
            End If
            secondIndex += 1
        Next

    End Sub

    Private Sub btnPrepDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrepDown.Click

        If isForDrink = True Then
            If dtDrinkPrep.Rows.Count > (prepIndex + 16) Then
                prepIndex += 16
                PopulateDrinkPrep()
            End If
        Else
            If dtModifierCategory.Rows.Count > (prepIndex + 16) Then
                prepIndex += 16
                PopulateAddNoCategories()
            End If
        End If

    End Sub

    Private Sub btnPrepUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrepUp.Click

        If prepIndex > 1 Then
            prepIndex -= 16
            If isForDrink = True Then
                PopulateDrinkPrep()
            Else
                PopulateAddNoCategories()
            End If

        End If
    End Sub

    Private Sub btnDone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDone.Click
        RaiseEvent Cancel()

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        whatWereDoing = "ADD"
        ResetButtonColors()
       
    End Sub

    Private Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo.Click
        whatWereDoing = "NO"
        ResetButtonColors()
      
    End Sub

    Private Sub btnExtra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtra.Click
        whatWereDoing = "EXTRA"
        ResetButtonColors()
      
    End Sub

    Private Sub ResetButtonColors()

        Me.btnAdd.BackColor = Color.CornflowerBlue
        Me.btnNo.BackColor = Color.CornflowerBlue
        Me.btnExtra.BackColor = Color.CornflowerBlue
        Select Case whatWereDoing
            Case "ADD"
                btnAdd.BackColor = Color.IndianRed
            Case "NO"
                btnNo.BackColor = Color.IndianRed
            Case "EXTRA"
                btnExtra.BackColor = Color.IndianRed
        End Select

    End Sub

    Public Sub PopulateExtraNoListView()

        Dim vRow As DataRowView
        Dim rawPrice As Decimal

        Me.lstIngredientsExtraNo.Items.Clear()
        Me.lstIngredientsAll.Items.Clear()

        '     Me.lblRawItemName.Text = MainFoodItemName

        For Each vRow In dvIngredients
            Dim allItem As New ListViewItem

            allItem.Text = Format(vRow("RawUsageAmount"), "##0")
            allItem.SubItems.Add(vRow("RecipeUnit"))
            allItem.SubItems.Add(vRow("RawItemName"))

            Me.lstIngredientsAll.Items.Add(allItem)

        Next

        If whatWereDoing = "EXTRA" Or whatWereDoing = "ADD" Then
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
            '       IsExtraType()
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
            '     IsNoType()
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
                If whatWereDoing = "EXTRA" Or whatWereDoing = "ADD" Then
                    .ID = -7
                    .Category = -7
                Else
                    .ID = -6
                    .Category = -6
                End If

                .PrintPriorityID = 3
                .RoutingID = FoodRoute 'currentTable.ReferenceRouting 'must do first in case drink
                .FunctionID = FoodFuctionID 'MainFoodFunID
                .FunctionGroup = FoodFunGroup 'MainFoodFunGroup
                .FunctionFlag = "M"
                '444     .TaxID = MainFoodTaxID
                '444       DetermineFunctionAndTaxInfo(currentItem, 9, False)   '9 is functionGroup = Modifier
                ' not sure why we are doing above

            End If

            If whatWereDoing = "EXTRA" Or whatWereDoing = "ADD" Then
                .Quantity = 1
                .Price = SelectedRawPriceTotal

            Else
                .Quantity = -1
                .Price = -1 * SelectedRawPriceTotal

            End If

            .Name = " *** " & whatWereDoing & "  " & vRow("RawItemName")
            .TerminalName = " *** " & whatWereDoing & "  " & vRow("RawItemName")
            .ChitName = " *** " & whatWereDoing & "  " & vRow("RawItemName")

            .ItemStatus = 0
            .Check = currentTable.CheckNumber
            .Customer = currentTable.CustomerNumber
            .Course = currentTable.CourseNumber
            .SIN = currentTable.SIN
            .SII = currentTable.ReferenceSIN
            .si2 = currentTable.si2

        End With

        RaiseEvent AcceptPrep(currentItem)

    End Sub

End Class
