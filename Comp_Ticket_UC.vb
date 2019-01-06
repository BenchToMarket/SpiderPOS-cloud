Public Class Comp_Ticket_UC
    Inherits System.Windows.Forms.UserControl

    Dim _checkFood As Decimal
    Dim _checkDrinks As Decimal
    Dim _checkTicket As Decimal
    Dim _checkTax As Decimal
    Dim _checkSinTax As Decimal
    Dim _manualTicket As Decimal
    Dim _manualTax As Decimal
    Dim compTax As Boolean
    Dim onManualTicket As Boolean
    Dim _totalTicket As Decimal

    Friend AllFood As Boolean
    Friend AllDrinks As Boolean
    Friend AdjPrice As Decimal
    Friend AdjTax As Decimal
    Friend AdjSinTax As Decimal



    Friend Property CheckFood() As Decimal
        Get
            Return _checkFood
        End Get
        Set(ByVal Value As Decimal)
            _checkFood = Value
        End Set
    End Property

    Friend Property CheckDrinks() As Decimal
        Get
            Return _checkDrinks
        End Get
        Set(ByVal Value As Decimal)
            _checkDrinks = Value
        End Set
    End Property

    Friend Property CheckTicket() As Decimal
        Get
            Return _checkTicket
        End Get
        Set(ByVal Value As Decimal)
            _checkTicket = Value
        End Set
    End Property

    Friend Property CheckTax() As Decimal
        Get
            Return _checkTax
        End Get
        Set(ByVal Value As Decimal)
            _checkTax = Value
        End Set
    End Property

    Friend Property CheckSinTax() As Decimal
        Get
            Return _checkSinTax
        End Get
        Set(ByVal Value As Decimal)
            _checkSinTax = Value
        End Set
    End Property

    Friend Property ManualTicket() As Decimal
        Get
            Return _manualTicket
        End Get
        Set(ByVal Value As Decimal)
            _manualTicket = Value
        End Set
    End Property

    Friend Property ManualTax() As Decimal
        Get
            Return _manualTax
        End Get
        Set(ByVal Value As Decimal)
            _manualTax = Value
        End Set
    End Property

    Event DisposeCompTicket()
    Event AcceptCompTicket()

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal cf As Decimal, ByVal cd As Decimal, ByVal tkt As Decimal, ByVal tax As Decimal, ByVal SinTax As Decimal)
        MyBase.New()

        _checkFood = cf
        _checkDrinks = cd
        _checkTicket = tkt
        _checkTax = tax
        _checkSinTax = SinTax
        _totalTicket = tkt + tax + SinTax

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        '     MsgBox(dsOrder.Tables("PaymentsAndCredits").Rows.Count)

        '       If dsOrder.Tables("PaymentsAndCredits").Rows.Count = 0 Then
        'if no payments 
        _manualTicket = tkt
        _manualTax = tax + SinTax
        Me.btnTicket.BackColor = Color.CornflowerBlue
        Me.btnTax.BackColor = Color.CornflowerBlue
        compTax = True
        '     Else
            Me.btnManual.BackColor = Color.CornflowerBlue
            onManualTicket = True
        '      End If


        'Add any initialization after the InitializeComponent() call
        DisplayCompAmounts()

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
    Friend WithEvents NumberPadMedium1 As DataSet_Builder.NumberPadMedium
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnFood As System.Windows.Forms.Button
    Friend WithEvents btnDrinks As System.Windows.Forms.Button
    Friend WithEvents btnTax As System.Windows.Forms.Button
    Friend WithEvents btnManual As System.Windows.Forms.Button
    Friend WithEvents btnTicket As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents lblTax As System.Windows.Forms.Label
    Friend WithEvents lblTicket As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.NumberPadMedium1 = New DataSet_Builder.NumberPadMedium
        Me.btnTicket = New System.Windows.Forms.Button
        Me.btnFood = New System.Windows.Forms.Button
        Me.btnDrinks = New System.Windows.Forms.Button
        Me.btnTax = New System.Windows.Forms.Button
        Me.btnManual = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnApply = New System.Windows.Forms.Button
        Me.lblTax = New System.Windows.Forms.Label
        Me.lblTicket = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'NumberPadMedium1
        '
        Me.NumberPadMedium1.BackColor = System.Drawing.Color.SlateGray
        Me.NumberPadMedium1.DecimalUsed = True
        Me.NumberPadMedium1.IntegerNumber = 0
        Me.NumberPadMedium1.Location = New System.Drawing.Point(280, 0)
        Me.NumberPadMedium1.Name = "NumberPadMedium1"
        Me.NumberPadMedium1.NumberString = ""
        Me.NumberPadMedium1.NumberTotal = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumberPadMedium1.Size = New System.Drawing.Size(192, 296)
        Me.NumberPadMedium1.TabIndex = 0
        '
        'btnTicket
        '
        Me.btnTicket.BackColor = System.Drawing.Color.SlateGray
        Me.btnTicket.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTicket.Location = New System.Drawing.Point(160, 120)
        Me.btnTicket.Name = "btnTicket"
        Me.btnTicket.Size = New System.Drawing.Size(96, 40)
        Me.btnTicket.TabIndex = 1
        Me.btnTicket.Text = "Ticket"
        '
        'btnFood
        '
        Me.btnFood.BackColor = System.Drawing.Color.SlateGray
        Me.btnFood.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFood.Location = New System.Drawing.Point(160, 8)
        Me.btnFood.Name = "btnFood"
        Me.btnFood.Size = New System.Drawing.Size(96, 40)
        Me.btnFood.TabIndex = 2
        Me.btnFood.Text = "Food"
        '
        'btnDrinks
        '
        Me.btnDrinks.BackColor = System.Drawing.Color.SlateGray
        Me.btnDrinks.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDrinks.Location = New System.Drawing.Point(160, 64)
        Me.btnDrinks.Name = "btnDrinks"
        Me.btnDrinks.Size = New System.Drawing.Size(96, 40)
        Me.btnDrinks.TabIndex = 3
        Me.btnDrinks.Text = "Drinks"
        '
        'btnTax
        '
        Me.btnTax.BackColor = System.Drawing.Color.SlateGray
        Me.btnTax.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTax.Location = New System.Drawing.Point(160, 176)
        Me.btnTax.Name = "btnTax"
        Me.btnTax.Size = New System.Drawing.Size(96, 40)
        Me.btnTax.TabIndex = 4
        Me.btnTax.Text = "Tax"
        '
        'btnManual
        '
        Me.btnManual.BackColor = System.Drawing.Color.SlateGray
        Me.btnManual.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnManual.Location = New System.Drawing.Point(160, 232)
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Size = New System.Drawing.Size(96, 56)
        Me.btnManual.TabIndex = 5
        Me.btnManual.Text = "Manual Ticket"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(40, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 32)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        '
        'btnApply
        '
        Me.btnApply.BackColor = System.Drawing.Color.Red
        Me.btnApply.Location = New System.Drawing.Point(32, 240)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(88, 40)
        Me.btnApply.TabIndex = 7
        Me.btnApply.Text = "Apply"
        '
        'lblTax
        '
        Me.lblTax.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTax.Location = New System.Drawing.Point(32, 192)
        Me.lblTax.Name = "lblTax"
        Me.lblTax.Size = New System.Drawing.Size(88, 23)
        Me.lblTax.TabIndex = 8
        Me.lblTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTicket
        '
        Me.lblTicket.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTicket.Location = New System.Drawing.Point(32, 128)
        Me.lblTicket.Name = "lblTicket"
        Me.lblTicket.Size = New System.Drawing.Size(88, 23)
        Me.lblTicket.TabIndex = 9
        Me.lblTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 128)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(24, 24)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "$"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 192)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 24)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "$"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.lblTax)
        Me.Panel1.Controls.Add(Me.lblTicket)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnApply)
        Me.Panel1.Controls.Add(Me.btnFood)
        Me.Panel1.Controls.Add(Me.btnDrinks)
        Me.Panel1.Controls.Add(Me.btnTicket)
        Me.Panel1.Controls.Add(Me.btnTax)
        Me.Panel1.Controls.Add(Me.btnManual)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(280, 296)
        Me.Panel1.TabIndex = 12
        '
        'Comp_Ticket_UC
        '
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.NumberPadMedium1)
        Me.Name = "Comp_Ticket_UC"
        Me.Size = New System.Drawing.Size(472, 296)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub



#End Region

    Private Sub DisplayCompAmounts()

        Me.lblTicket.Text = ManualTicket
        Me.lblTax.Text = ManualTax

    End Sub

    Private Sub btnFood_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFood.Click
        'will just comp food items

        ResetButtonColors()
        Me.btnFood.BackColor = Color.CornflowerBlue
        _manualTicket = CheckFood
      
        '666
        '****** need to loop through for tax ID
        _manualTax = DetermineTaxPrice(ds.Tables("Tax").Rows(0)("TaxID"), CheckFood)
        DisplayCompAmounts()
        compTax = False
        AllFood = True
        AllDrinks = False

    End Sub

    Private Sub btnDrinks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrinks.Click
        'will just comp drink items

        ResetButtonColors()
        Me.btnDrinks.BackColor = Color.CornflowerBlue
        '      CheckTax = GenerateOrderTables.DetermineTaxPrice(TaxID???, CheckDrinks)
        _manualTicket = CheckDrinks
        _manualTax = DetermineTaxPrice(ds.Tables("Tax").Rows(0)("TaxID"), CheckDrinks)
        DisplayCompAmounts()
        compTax = False
        AllFood = False
        AllDrinks = True
    End Sub

    Private Sub btnTicket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTicket.Click

        ResetButtonColors()
        Me.btnTicket.BackColor = Color.CornflowerBlue
        _manualTicket = CheckTicket
        If compTax = True Then
            _manualTax = DetermineTaxPrice(ds.Tables("Tax").Rows(0)("TaxID"), CheckTicket)
            '666 need to loop
            '    _manualTax = CheckTax + CheckSinTax
        End If

        DisplayCompAmounts()
        AllFood = False
        AllDrinks = False

    End Sub

    Private Sub btnTax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTax.Click
        Exit Sub

        If compTax = True Then
            compTax = False
            Me.btnTax.BackColor = Color.LightGray
            _manualTax = 0
            DisplayCompAmounts()
        Else
            compTax = True
            Me.btnTax.BackColor = Color.CornflowerBlue
            _manualTax = CheckTax + CheckSinTax
            DisplayCompAmounts()
        End If

    End Sub

    Private Sub btnManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManual.Click

        ResetButtonColors()
        Me.btnManual.BackColor = Color.CornflowerBlue
        If onManualTicket = True Then
            _manualTicket = 0
            Me.lblTicket.Text = ""

            '666        onManualTicket = False
            '       Me.btnManual.Text = "Manual Tax"
        Else
            _manualTax = 0
            Me.lblTax.Text = ""

            onManualTicket = True
            Me.btnManual.Text = "Manual Ticket"
        End If

    End Sub

    Private Sub ManualCompEntered(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumberPadMedium1.NumberEntered
    
        If onManualTicket = False Then
            If Me.NumberPadMedium1.NumberTotal > (CheckTax + CheckSinTax) Then
                MsgBox("You can not comp more than Check Tax")
                NumberPadMedium1.ResetValues()
                Exit Sub
            End If
            _manualTax = NumberPadMedium1.NumberTotal  '(Format(Me.NumberPadMedium1.NumberTotal, "####0.00"))
            onManualTicket = True
            Me.btnManual.Text = "Manual Ticket"
            NumberPadMedium1.IntegerNumber = 0
            Me.NumberPadMedium1.ShowNumberString()
        Else
            If Me.NumberPadMedium1.NumberTotal > CheckTicket Then
                MsgBox("You can not comp more than Check Ticket")
                NumberPadMedium1.ResetValues()
                Exit Sub
            End If
            _manualTicket = NumberPadMedium1.NumberTotal '(Format(Me.NumberPadMedium1.NumberTotal, "####0.00"))
            '666    '    _manualTax = 0
            _manualTax = DetermineTaxPrice(ds.Tables("Tax").Rows(0)("TaxID"), ManualTicket)
            '666        onManualTicket = False
            '     Me.btnManual.Text = "Manual Tax"
            NumberPadMedium1.IntegerNumber = 0
            Me.NumberPadMedium1.ShowNumberString()
        End If

        ResetButtonColors()
        Me.btnManual.BackColor = Color.CornflowerBlue
        DisplayCompAmounts()

    End Sub

    Private Sub ResetButtonColors()

        Me.btnFood.BackColor = Color.LightGray
        Me.btnDrinks.BackColor = Color.LightGray
        Me.btnTicket.BackColor = Color.LightGray
        Me.btnTax.BackColor = Color.LightGray
        Me.btnManual.BackColor = Color.LightGray

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        RaiseEvent DisposeCompTicket()

    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        '   Dim compTotal As Decimal
        '      Dim newPayment As DataSet_Builder.Payment
     
        If ManualTax > CheckTax Then
            AdjSinTax = ManualTax - CheckTax
            AdjTax = CheckTax
        Else
            AdjSinTax = 0
            AdjTax = ManualTax
        End If
        AdjPrice = ManualTicket


        '     compTotal = ManualTicket + ManualTax

        '     newPayment.Purchase = Format(compTotal, "##,###.00")
        '     newPayment.PaymentTypeID = -7
        '     newPayment.PaymentTypeName = "Comp'd"   ' & currentTable.EmployeeID
        '    AddPaymentToDataRow(newPayment, True, currentTable.ExperienceNumber, actingManager.EmployeeID)

        RaiseEvent AcceptCompTicket()

    End Sub
End Class
