Imports DataSet_Builder

Public Class Seating_EnterTab
    Inherits System.Windows.Forms.UserControl

    Dim _newTabName As String
    '   Friend WithEvents readAuthSeating As ReadCredit
  
    Dim tabAccountInfo As New DataSet_Builder.Payment
    Dim cardReadFailedTimer As Timer

    Dim _methodUse As String
    Friend WithEvents btnPickup As System.Windows.Forms.Button
    Friend WithEvents lblCardReadFailed As System.Windows.Forms.Label
    Dim _startedFrom As String

    Friend Property NewTabName() As String
        Get
            Return _newTabName
        End Get
        Set(ByVal Value As String)
            _newTabName = Value
        End Set
    End Property

    Friend Property MethedUse() As String
        Get
            Return _methodUse
        End Get
        Set(ByVal Value As String)
            _methodUse = Value
        End Set
    End Property

    Friend Property StartedFrom() As String
        Get
            Return _startedFrom
        End Get
        Set(ByVal Value As String)
            _startedFrom = Value
        End Set
    End Property


    Event OpenNewTabEvent()
    Event OpenNewTakeOutTab()
    Event CancelNewTab()
    Event CustomerCardEvent() 'ByRef tabAccountInfo As DataSet_Builder.Payment)


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        '  _startedFrom = fm

        'Add any initialization after the InitializeComponent() call
        If typeProgram = "Online_Demo" Then
            Me.btnDemoCC.Visible = True
            Me.btnDemoCC.BringToFront()
        End If

        '444      If tn Is Nothing Then
        'this means this is from Login, there is no Tab Name 
        'therefeor is for New Tab Open
        '444       readAuthSeating = New ReadCredit(True)
        '444        Else
        '444        readAuthSeating = New ReadCredit(False)
        '444      End If

        '     readAuth.IsNewTab = True

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnEnterTabCancel As System.Windows.Forms.Button
    Friend WithEvents tabNameKeyboard As DataSet_Builder.KeyBoard_UC
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents btnTakeOut As System.Windows.Forms.Button
    Friend WithEvents btnDemoCC As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnPickup = New System.Windows.Forms.Button
        Me.btnDemoCC = New System.Windows.Forms.Button
        Me.btnTakeOut = New System.Windows.Forms.Button
        Me.btnOpen = New System.Windows.Forms.Button
        Me.btnEnterTabCancel = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.tabNameKeyboard = New DataSet_Builder.KeyBoard_UC
        Me.lblCardReadFailed = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.RoyalBlue
        Me.Panel1.Controls.Add(Me.lblCardReadFailed)
        Me.Panel1.Controls.Add(Me.btnPickup)
        Me.Panel1.Controls.Add(Me.btnDemoCC)
        Me.Panel1.Controls.Add(Me.btnTakeOut)
        Me.Panel1.Controls.Add(Me.btnOpen)
        Me.Panel1.Controls.Add(Me.btnEnterTabCancel)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.tabNameKeyboard)
        Me.Panel1.Location = New System.Drawing.Point(24, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(728, 416)
        Me.Panel1.TabIndex = 1
        '
        'btnPickup
        '
        Me.btnPickup.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnPickup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPickup.ForeColor = System.Drawing.Color.White
        Me.btnPickup.Location = New System.Drawing.Point(600, 16)
        Me.btnPickup.Name = "btnPickup"
        Me.btnPickup.Size = New System.Drawing.Size(104, 56)
        Me.btnPickup.TabIndex = 22
        Me.btnPickup.Text = "Pickup"
        Me.btnPickup.UseVisualStyleBackColor = False
        '
        'btnDemoCC
        '
        Me.btnDemoCC.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnDemoCC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDemoCC.Location = New System.Drawing.Point(128, 40)
        Me.btnDemoCC.Name = "btnDemoCC"
        Me.btnDemoCC.Size = New System.Drawing.Size(104, 64)
        Me.btnDemoCC.TabIndex = 21
        Me.btnDemoCC.Text = "Demo Card Swipe"
        Me.btnDemoCC.UseVisualStyleBackColor = False
        Me.btnDemoCC.Visible = False
        '
        'btnTakeOut
        '
        Me.btnTakeOut.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnTakeOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTakeOut.ForeColor = System.Drawing.Color.White
        Me.btnTakeOut.Location = New System.Drawing.Point(472, 16)
        Me.btnTakeOut.Name = "btnTakeOut"
        Me.btnTakeOut.Size = New System.Drawing.Size(104, 56)
        Me.btnTakeOut.TabIndex = 4
        Me.btnTakeOut.Text = "Take Out"
        Me.btnTakeOut.UseVisualStyleBackColor = False
        '
        'btnOpen
        '
        Me.btnOpen.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpen.ForeColor = System.Drawing.Color.White
        Me.btnOpen.Location = New System.Drawing.Point(346, 16)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(104, 56)
        Me.btnOpen.TabIndex = 3
        Me.btnOpen.Text = "Dine In"
        Me.btnOpen.UseVisualStyleBackColor = False
        '
        'btnEnterTabCancel
        '
        Me.btnEnterTabCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnterTabCancel.ForeColor = System.Drawing.Color.White
        Me.btnEnterTabCancel.Location = New System.Drawing.Point(24, 16)
        Me.btnEnterTabCancel.Name = "btnEnterTabCancel"
        Me.btnEnterTabCancel.Size = New System.Drawing.Size(104, 48)
        Me.btnEnterTabCancel.TabIndex = 2
        Me.btnEnterTabCancel.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(168, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 40)
        Me.Label1.TabIndex = 1
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tabNameKeyboard
        '
        Me.tabNameKeyboard.BackColor = System.Drawing.Color.SlateGray
        Me.tabNameKeyboard.EnteredString = Nothing
        Me.tabNameKeyboard.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabNameKeyboard.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.tabNameKeyboard.Location = New System.Drawing.Point(16, 80)
        Me.tabNameKeyboard.Name = "tabNameKeyboard"
        Me.tabNameKeyboard.Size = New System.Drawing.Size(688, 312)
        Me.tabNameKeyboard.TabIndex = 0
        '
        'lblCardReadFailed
        '
        Me.lblCardReadFailed.AutoSize = True
        Me.lblCardReadFailed.BackColor = System.Drawing.Color.White
        Me.lblCardReadFailed.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCardReadFailed.Location = New System.Drawing.Point(252, 109)
        Me.lblCardReadFailed.Name = "lblCardReadFailed"
        Me.lblCardReadFailed.Size = New System.Drawing.Size(240, 29)
        Me.lblCardReadFailed.TabIndex = 23
        Me.lblCardReadFailed.Text = "Card Read Failed..."
        Me.lblCardReadFailed.Visible = False
        '
        'Seating_EnterTab
        '
        Me.BackColor = System.Drawing.Color.Black
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Seating_EnterTab"
        Me.Size = New System.Drawing.Size(776, 485)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Friend Sub RestartSeatingTabWithName(ByVal fm As String, ByVal tn As String)

        If Not tn Is Nothing Then
            _newTabName = tn
            Me.tabNameKeyboard.StartingWithText(tn)
        Else
            tabNameKeyboard.StartingWithText("")
        End If
        _startedFrom = fm

    End Sub

    Private Sub TabNameEntered(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabNameKeyboard.StringEntered

        _methodUse = "Dine In"

        If Not tabNameKeyboard.EnteredString Is Nothing Then
            _newTabName = tabNameKeyboard.EnteredString
        Else
            _newTabName = "New Tab"
        End If

        RemoveCardHandler()
        RaiseEvent OpenNewTabEvent()

    End Sub

    Private Sub btnEnterTabCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnterTabCancel.Click

        RemoveCardHandler()
        RaiseEvent CancelNewTab()

    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click

        _methodUse = "Dine In"

        If tabNameKeyboard.EnteredString Is Nothing Or tabNameKeyboard.EnteredString.Length = 0 Then
            _newTabName = _methodUse & " Tab"
        Else
            _newTabName = tabNameKeyboard.EnteredString
        End If

        RemoveCardHandler()
        RaiseEvent OpenNewTabEvent()
    End Sub

    Private Sub btnTakeOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTakeOut.Click

        _methodUse = "Take Out"
        '     currentTable.MethodUse = "Take Out"

        If tabNameKeyboard.EnteredString Is Nothing Or tabNameKeyboard.EnteredString.Length = 0 Then
            _newTabName = _methodUse & " Tab"
        Else
            _newTabName = tabNameKeyboard.EnteredString
        End If

        RemoveCardHandler()
        RaiseEvent OpenNewTakeOutTab() 'OpenNewTabEve)  '
    End Sub

    Private Sub btnPickup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPickup.Click
        _methodUse = "Pickup"
        '     currentTable.MethodUse = "Pickup"

        If tabNameKeyboard.EnteredString Is Nothing Or tabNameKeyboard.EnteredString.Length = 0 Then
            _newTabName = _methodUse & " Tab"
        Else
            _newTabName = tabNameKeyboard.EnteredString
        End If

        RemoveCardHandler()
        RaiseEvent OpenNewTakeOutTab()
    End Sub

    Private Sub CustomerCardRead222(ByRef newpayment As DataSet_Builder.Payment) '444 Handles readAuthSeating.CardReadSuccessful

        '     AddPaymentToCollection(newpayment)
        '    _methodUse = "Dine In"
        '   tabAccountInfo = newpayment

        RemoveCardHandler()
        RaiseEvent CustomerCardEvent() '444tabAccountInfo)

    End Sub

    Friend Sub CardRead_Failed222() '444 Handles readAuthSeating.CardReadFailed

        lblCardReadFailed.Visible = True

        Exit Sub
        '        cardReadFailedTimer = New Timer
        '       AddHandler cardReadFailedTimer.Tick, AddressOf CardReadTimerExpired222
        '      cardReadFailedTimer.Interval = 3000
        '     cardReadFailedTimer.Start()

    End Sub
    Private Sub CardReadTimerExpired222(ByVal sender As Object, ByVal e As System.EventArgs)

        cardReadFailedTimer.Dispose()
        lblCardReadFailed.Visible = False

    End Sub

    Friend Sub EnterTabNameFromSwipe(ByVal tabName As String) '444 Handles readAuthSeating.EnteringTabNameInKeyboard

        lblCardReadFailed.Visible = False
        tabNameKeyboard.StartingWithText(tabName)
        Me.Update()

    End Sub

    Private Sub RemoveCardHandler()
        '222
        '    tmrCardRead.Stop()
        '   RemoveHandler tmrCardRead.Tick, AddressOf readAuthSeating.tmrCardRead_Tick

    End Sub


    Private Sub btnDemoCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDemoCC.Click
        Dim newPayment As Payment

        With newPayment
            .experienceNumber = demoExpNumID    'currentTable.ExperienceNumber
            .Name = "Test Credit"
            .FirstName = "Test"
            .LastName = "Credit"
            .Track2 = "37130000000000"
            .AccountNumber = "371301234567890" & demoCcNumberAddon.ToString
            .PaymentTypeID = 2
            .PaymentTypeName = "AMEX"
            .ExpDate = "0809"
            .Swiped = True
        End With
        demoCcNumberAddon += 1

        _methodUse = "Dine In"
        tabAccountInfo = newPayment

        If typeProgram = "Online_Demo" Then
            'it is always Online_Demo here
            _newTabName = newPayment.Name ' "New Tab"
            RemoveCardHandler()
            RaiseEvent OpenNewTabEvent()
            Exit Sub
        End If

        '444     RemoveCardHandler()
        '444 RaiseEvent CustomerCardEvent() 

    End Sub



End Class
