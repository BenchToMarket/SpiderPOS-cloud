Public Class NumberPadSmall
    Inherits System.Windows.Forms.UserControl

    Private _numberTotal As Decimal
    Private _integerNumber As Integer
    Private _numberString As String
    Private _decimalUsed As Boolean
    Public DisplayIntegerNumber As Boolean
    Public changesMade As Boolean

    Event NumberEntered(ByVal sender As Object, ByVal e As System.EventArgs)
    Event NumberChanged()


    Public Property NumberTotal() As Decimal
        Get
            Return _numberTotal
        End Get
        Set(ByVal Value As Decimal)
            _numberTotal = Value
        End Set
    End Property

    Public Property IntegerNumber() As Integer
        Get
            Return _integerNumber
        End Get
        Set(ByVal Value As Integer)
            _integerNumber = Value
        End Set
    End Property

    Public Property NumberString() As String
        Get
            Return _numberString
        End Get
        Set(ByVal Value As String)
            _numberString = Value
        End Set
    End Property

    Public Property DecimalUsed() As Boolean
        Get
            Return _decimalUsed
        End Get
        Set(ByVal Value As Boolean)
            _decimalUsed = Value
        End Set
    End Property

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        '      decimalUsed = decUsed

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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents lblNumberPad As System.Windows.Forms.Label
    Friend WithEvents btnDot As System.Windows.Forms.Button
    Friend WithEvents btnEnter As System.Windows.Forms.Button
    Friend WithEvents Button0 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button7 = New System.Windows.Forms.Button
        Me.Button8 = New System.Windows.Forms.Button
        Me.Button9 = New System.Windows.Forms.Button
        Me.Button0 = New System.Windows.Forms.Button
        Me.lblNumberPad = New System.Windows.Forms.Label
        Me.btnDot = New System.Windows.Forms.Button
        Me.btnEnter = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(0, 48)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(48, 48)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "1"
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(48, 48)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(48, 48)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "2"
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(96, 48)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(48, 48)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "3"
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(0, 96)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(48, 48)
        Me.Button4.TabIndex = 3
        Me.Button4.Text = "4"
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Location = New System.Drawing.Point(48, 96)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(48, 48)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "5"
        '
        'Button6
        '
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.Location = New System.Drawing.Point(96, 96)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(48, 48)
        Me.Button6.TabIndex = 5
        Me.Button6.Text = "6"
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.Location = New System.Drawing.Point(0, 144)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(48, 48)
        Me.Button7.TabIndex = 6
        Me.Button7.Text = "7"
        '
        'Button8
        '
        Me.Button8.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.Location = New System.Drawing.Point(48, 144)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(48, 48)
        Me.Button8.TabIndex = 7
        Me.Button8.Text = "8"
        '
        'Button9
        '
        Me.Button9.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button9.Location = New System.Drawing.Point(96, 144)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(48, 48)
        Me.Button9.TabIndex = 8
        Me.Button9.Text = "9"
        '
        'Button0
        '
        Me.Button0.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button0.Location = New System.Drawing.Point(0, 192)
        Me.Button0.Name = "Button0"
        Me.Button0.Size = New System.Drawing.Size(48, 48)
        Me.Button0.TabIndex = 9
        Me.Button0.Text = "0"
        '
        'lblNumberPad
        '
        Me.lblNumberPad.BackColor = System.Drawing.Color.White
        Me.lblNumberPad.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumberPad.Location = New System.Drawing.Point(8, 16)
        Me.lblNumberPad.Name = "lblNumberPad"
        Me.lblNumberPad.Size = New System.Drawing.Size(128, 24)
        Me.lblNumberPad.TabIndex = 10
        Me.lblNumberPad.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnDot
        '
        Me.btnDot.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDot.Location = New System.Drawing.Point(48, 192)
        Me.btnDot.Name = "btnDot"
        Me.btnDot.Size = New System.Drawing.Size(48, 48)
        Me.btnDot.TabIndex = 11
        Me.btnDot.Text = "00"
        '
        'btnEnter
        '
        Me.btnEnter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnter.Location = New System.Drawing.Point(96, 192)
        Me.btnEnter.Name = "btnEnter"
        Me.btnEnter.Size = New System.Drawing.Size(48, 48)
        Me.btnEnter.TabIndex = 12
        Me.btnEnter.Text = "ETR"
        '
        'NumberPadSmall
        '
        Me.BackColor = System.Drawing.Color.SlateGray
        Me.Controls.Add(Me.btnEnter)
        Me.Controls.Add(Me.btnDot)
        Me.Controls.Add(Me.lblNumberPad)
        Me.Controls.Add(Me.Button0)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "NumberPadSmall"
        Me.Size = New System.Drawing.Size(144, 240)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub CalculateNumberTotal(ByVal addOnInteger As Integer)

        If DecimalUsed = True Then
            If _integerNumber > 100000000 Then
                Exit Sub
            End If
            _integerNumber = (_integerNumber * 10) + addOnInteger
            _numberTotal = Format(_integerNumber / 100, "##,###.00")
        Else
            _numberString = _numberString & addOnInteger.ToString
        End If
        changesMade = True

    End Sub

    Public Sub ShowNumberString()

        If DecimalUsed = True Then
            If DisplayIntegerNumber = False Then
                Me.lblNumberPad.Text = NumberTotal
            Else
                Me.lblNumberPad.Text = IntegerNumber
            End If
        Else
            Me.lblNumberPad.Text = NumberString
        End If

        RaiseEvent NumberChanged()

    End Sub

    Public Sub ResetValues()

        _integerNumber = 0
        _numberTotal = 0
        _numberString = ""
        ShowNumberString()
        changesMade = False

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CalculateNumberTotal(1)
        ShowNumberString()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        CalculateNumberTotal(2)
        ShowNumberString()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        CalculateNumberTotal(3)
        ShowNumberString()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        CalculateNumberTotal(4)
        ShowNumberString()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        CalculateNumberTotal(5)
        ShowNumberString()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        CalculateNumberTotal(6)
        ShowNumberString()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        CalculateNumberTotal(7)
        ShowNumberString()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        CalculateNumberTotal(8)
        ShowNumberString()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        CalculateNumberTotal(9)
        ShowNumberString()
    End Sub

    Private Sub Button0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button0.Click
        CalculateNumberTotal(0)
        ShowNumberString()
    End Sub

    Private Sub btnDot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDot.Click
        CalculateNumberTotal(0)
        CalculateNumberTotal(0)
        ShowNumberString()

    End Sub

    Private Sub btnEnter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnter.Click

        RaiseEvent NumberEntered(sender, e)

    End Sub


    Private Sub lblNumberPad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblNumberPad.Click

        ResetValues()

    End Sub

End Class
