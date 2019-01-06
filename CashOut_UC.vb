Public Class CashOut_UC
    Inherits System.Windows.Forms.UserControl


    Private _itemDescription As String
    Private _itemPrice As Decimal

    Dim _paymentTypeID As Integer
    Dim _maxCashAmount As Decimal



    Private _expNum As Int64



    '  Event UC_Hit()



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

    Friend Property ExpNum() As Int64
        Get
            Return _expNum
        End Get
        Set(ByVal Value As Int64)
            _expNum = Value
        End Set
    End Property

    Friend Property PaymentTypeID() As Integer
        Get
            Return _paymentTypeID
        End Get
        Set(ByVal Value As Integer)
            _paymentTypeID = Value
        End Set
    End Property

    Friend Property MaxCashAmount() As Decimal
        Get
            Return _maxCashAmount
        End Get
        Set(ByVal Value As Decimal)
            _maxCashAmount = Value
        End Set
    End Property



    Event CancelCashOut(ByVal sender As Object, ByVal e As System.EventArgs)
    Event AcceptCashOut(ByVal sender As Object, ByVal e As System.EventArgs)


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal en As Int64, ByVal pt As Integer, ByVal maxCA As Decimal)
        MyBase.New()

        _paymentTypeID = pt
        _expNum = en
        _maxCashAmount = maxCA

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
    Friend WithEvents NumberPadSmall1 As DataSet_Builder.NumberPadSmall
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblItem As System.Windows.Forms.Label
    Friend WithEvents lblTextItem As System.Windows.Forms.Label
    Friend WithEvents lblPrice As System.Windows.Forms.Label
    Friend WithEvents lblTextPrice As System.Windows.Forms.Label
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents SpecialKeyboard As DataSet_Builder.KeyBoard_UC
    Friend WithEvents lblCashOut As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.NumberPadSmall1 = New DataSet_Builder.NumberPadSmall
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnAccept = New System.Windows.Forms.Button
        Me.lblTextPrice = New System.Windows.Forms.Label
        Me.lblPrice = New System.Windows.Forms.Label
        Me.lblTextItem = New System.Windows.Forms.Label
        Me.lblItem = New System.Windows.Forms.Label
        Me.SpecialKeyboard = New DataSet_Builder.KeyBoard_UC
        Me.lblCashOut = New System.Windows.Forms.Label
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'NumberPadSmall1
        '
        Me.NumberPadSmall1.BackColor = System.Drawing.Color.SlateGray
        Me.NumberPadSmall1.DecimalUsed = False
        Me.NumberPadSmall1.IntegerNumber = 0
        Me.NumberPadSmall1.Location = New System.Drawing.Point(568, 8)
        Me.NumberPadSmall1.Name = "NumberPadSmall1"
        Me.NumberPadSmall1.NumberString = Nothing
        Me.NumberPadSmall1.NumberTotal = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumberPadSmall1.Size = New System.Drawing.Size(144, 240)
        Me.NumberPadSmall1.TabIndex = 1
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
        Me.Panel3.Location = New System.Drawing.Point(384, 8)
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
        Me.lblTextPrice.Location = New System.Drawing.Point(56, 160)
        Me.lblTextPrice.Name = "lblTextPrice"
        Me.lblTextPrice.Size = New System.Drawing.Size(96, 24)
        Me.lblTextPrice.TabIndex = 3
        Me.lblTextPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPrice
        '
        Me.lblPrice.Location = New System.Drawing.Point(8, 136)
        Me.lblPrice.Name = "lblPrice"
        Me.lblPrice.Size = New System.Drawing.Size(104, 16)
        Me.lblPrice.TabIndex = 2
        Me.lblPrice.Text = "Price:"
        '
        'lblTextItem
        '
        Me.lblTextItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTextItem.ForeColor = System.Drawing.Color.Red
        Me.lblTextItem.Location = New System.Drawing.Point(24, 72)
        Me.lblTextItem.Name = "lblTextItem"
        Me.lblTextItem.Size = New System.Drawing.Size(128, 56)
        Me.lblTextItem.TabIndex = 1
        '
        'lblItem
        '
        Me.lblItem.Location = New System.Drawing.Point(8, 48)
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
        Me.SpecialKeyboard.Location = New System.Drawing.Point(32, 256)
        Me.SpecialKeyboard.Name = "SpecialKeyboard"
        Me.SpecialKeyboard.Size = New System.Drawing.Size(680, 296)
        Me.SpecialKeyboard.TabIndex = 6
        '
        'lblCashOut
        '
        Me.lblCashOut.Location = New System.Drawing.Point(16, 16)
        Me.lblCashOut.Name = "lblCashOut"
        Me.lblCashOut.Size = New System.Drawing.Size(160, 23)
        Me.lblCashOut.TabIndex = 7
        '
        'CashOut_UC
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(59, Byte), CType(96, Byte), CType(141, Byte))
        Me.Controls.Add(Me.lblCashOut)
        Me.Controls.Add(Me.SpecialKeyboard)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.NumberPadSmall1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.Name = "CashOut_UC"
        Me.Size = New System.Drawing.Size(744, 565)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther()

        Me.NumberPadSmall1.DecimalUsed = True
        Me.SpecialKeyboard.EnteredString = ""




    End Sub



    Private Sub ItemDescriptionChanged() Handles SpecialKeyboard.StringChanged
        '      RaiseEvent UC_Hit()

        If Me.SpecialKeyboard.EnteredString.Length > 50 Then
            Me.SpecialKeyboard.EnteredString = Me.SpecialKeyboard.EnteredString.Substring(0, 50)
        End If

        Me.lblTextItem.Text = Me.SpecialKeyboard.EnteredString
        Me.ItemDescription = Me.SpecialKeyboard.EnteredString

    End Sub

    Private Sub ItemPriceChanged() Handles NumberPadSmall1.NumberChanged
        '      RaiseEvent UC_Hit()

        Me.lblTextPrice.Text = Me.NumberPadSmall1.NumberTotal
        Me.ItemPrice = Me.NumberPadSmall1.NumberTotal

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        RaiseEvent CancelCashOut(sender, e)

        Me.Dispose()

    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click

        If Me.NumberPadSmall1.NumberTotal > MaxCashAmount Then
            If PaymentTypeID = 1 Then
                ' this is a refund
                MsgBox("You can not refund more cash then paid in.")
            ElseIf PaymentTypeID = -3 Then
                ' this is cash out
                MsgBox("The restaurant has a limit of cash out: $ " & MaxCashAmount)
            End If
            Exit Sub
        End If

        RaiseEvent AcceptCashOut(sender, e)

    End Sub



End Class
