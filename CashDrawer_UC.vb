Imports System.Drawing.Color
Imports DataSet_Builder


Public Class CashDrawer_UC
    Inherits System.Windows.Forms.UserControl

    '   Dim prt As New PrintHelper

    Friend ThisCashTerminal As Integer
    Friend activeTerminalsOpenID As Int64
    '0 is for this cash terminal is true
    'anything over that is closing multiple terminals and the number
    'indicates the number of terminals open
    Friend WhatToCashDrawer As String

    Dim cashInfo As CashInfoStructure

    '    Dim _openCash As Decimal
    '   Dim _closeCash As Decimal

    '  Dim _cashIn As Decimal

    '    Dim _cashOut As Decimal
    '   Dim _overShort As Decimal
    '  Dim _reasonShort As String

    '    Dim _netSales As Decimal
    '   Dim _ccSales As Decimal
    '  Dim _cashSales As Decimal
    ' Dim _ccTips As Decimal
    'Dim _cashBeforeOut As Decimal


    Dim btnOtherTerminals(20) As Button

    Event TerminalsNowOpen(ByVal termOpen As Integer)
    Event ResetClosingData()

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal _thisCashTerminal As Integer)
        MyBase.New()

        ThisCashTerminal = _thisCashTerminal

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.pnlOtherDrawers.Location = New Point(144, 128)
        Me.pnlOtherDrawers.Size = New Size(208, 296)

        'Add any initialization after the InitializeComponent() call
        InitializeOther()

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
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSwitch As System.Windows.Forms.Button
    Friend WithEvents btnOther As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblTerminalName As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents NumberPadMedium1 As DataSet_Builder.NumberPadMedium
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblOpenClose As System.Windows.Forms.Label
    Friend WithEvents lblCashInstructions As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblOS As System.Windows.Forms.Label
    Friend WithEvents lblOpenedBy As System.Windows.Forms.Label
    Friend WithEvents lblDateTime As System.Windows.Forms.Label
    Friend WithEvents lblCashAtOpen As System.Windows.Forms.Label
    Friend WithEvents lblCashIn As System.Windows.Forms.Label
    Friend WithEvents lblCashOut As System.Windows.Forms.Label
    Friend WithEvents lblCashAtClose As System.Windows.Forms.Label
    Friend WithEvents lblOverShort As System.Windows.Forms.Label
    Friend WithEvents pnlCloseCash As System.Windows.Forms.Panel
    Friend WithEvents pnlOtherDrawers As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblccTips As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnOther = New System.Windows.Forms.Button
        Me.btnSwitch = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnOpen = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblTerminalName = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnAccept = New System.Windows.Forms.Button
        Me.NumberPadMedium1 = New DataSet_Builder.NumberPadMedium
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblOpenClose = New System.Windows.Forms.Label
        Me.lblCashInstructions = New System.Windows.Forms.Label
        Me.pnlCloseCash = New System.Windows.Forms.Panel
        Me.lblccTips = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblOverShort = New System.Windows.Forms.Label
        Me.lblCashAtClose = New System.Windows.Forms.Label
        Me.lblCashOut = New System.Windows.Forms.Label
        Me.lblCashIn = New System.Windows.Forms.Label
        Me.lblCashAtOpen = New System.Windows.Forms.Label
        Me.lblDateTime = New System.Windows.Forms.Label
        Me.lblOpenedBy = New System.Windows.Forms.Label
        Me.lblOS = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlOtherDrawers = New System.Windows.Forms.Panel
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlCloseCash.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.SlateGray
        Me.Panel1.Controls.Add(Me.btnOther)
        Me.Panel1.Controls.Add(Me.btnSwitch)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnOpen)
        Me.Panel1.Location = New System.Drawing.Point(16, 128)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(104, 320)
        Me.Panel1.TabIndex = 0
        '
        'btnOther
        '
        Me.btnOther.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOther.Location = New System.Drawing.Point(8, 232)
        Me.btnOther.Name = "btnOther"
        Me.btnOther.Size = New System.Drawing.Size(88, 72)
        Me.btnOther.TabIndex = 3
        Me.btnOther.Text = "Close Other"
        '
        'btnSwitch
        '
        Me.btnSwitch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSwitch.Location = New System.Drawing.Point(8, 160)
        Me.btnSwitch.Name = "btnSwitch"
        Me.btnSwitch.Size = New System.Drawing.Size(88, 64)
        Me.btnSwitch.TabIndex = 2
        Me.btnSwitch.Text = "Close && Reopen"
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(8, 88)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 64)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnOpen
        '
        Me.btnOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpen.Location = New System.Drawing.Point(8, 16)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(88, 64)
        Me.btnOpen.TabIndex = 0
        Me.btnOpen.Text = "Open"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(104, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 40)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Cash Drawer:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTerminalName
        '
        Me.lblTerminalName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTerminalName.ForeColor = System.Drawing.Color.White
        Me.lblTerminalName.Location = New System.Drawing.Point(240, 24)
        Me.lblTerminalName.Name = "lblTerminalName"
        Me.lblTerminalName.Size = New System.Drawing.Size(160, 40)
        Me.lblTerminalName.TabIndex = 2
        Me.lblTerminalName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(16, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 56)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        '
        'btnAccept
        '
        Me.btnAccept.Enabled = False
        Me.btnAccept.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAccept.Location = New System.Drawing.Point(136, 8)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(88, 56)
        Me.btnAccept.TabIndex = 4
        Me.btnAccept.Text = "Accept"
        '
        'NumberPadMedium1
        '
        Me.NumberPadMedium1.BackColor = System.Drawing.Color.SlateGray
        Me.NumberPadMedium1.DecimalUsed = False
        Me.NumberPadMedium1.IntegerNumber = 0
        Me.NumberPadMedium1.Location = New System.Drawing.Point(384, 128)
        Me.NumberPadMedium1.Name = "NumberPadMedium1"
        Me.NumberPadMedium1.NumberString = ""
        Me.NumberPadMedium1.NumberTotal = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumberPadMedium1.Size = New System.Drawing.Size(192, 296)
        Me.NumberPadMedium1.TabIndex = 5
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnAccept)
        Me.Panel2.Location = New System.Drawing.Point(360, 440)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(240, 64)
        Me.Panel2.TabIndex = 6
        '
        'lblOpenClose
        '
        Me.lblOpenClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpenClose.ForeColor = System.Drawing.Color.White
        Me.lblOpenClose.Location = New System.Drawing.Point(24, 24)
        Me.lblOpenClose.Name = "lblOpenClose"
        Me.lblOpenClose.Size = New System.Drawing.Size(72, 40)
        Me.lblOpenClose.TabIndex = 7
        Me.lblOpenClose.Text = "Open"
        Me.lblOpenClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCashInstructions
        '
        Me.lblCashInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCashInstructions.ForeColor = System.Drawing.Color.White
        Me.lblCashInstructions.Location = New System.Drawing.Point(384, 88)
        Me.lblCashInstructions.Name = "lblCashInstructions"
        Me.lblCashInstructions.Size = New System.Drawing.Size(192, 24)
        Me.lblCashInstructions.TabIndex = 8
        Me.lblCashInstructions.Text = "Enter Open Cash"
        Me.lblCashInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlCloseCash
        '
        Me.pnlCloseCash.BackColor = System.Drawing.Color.LightSlateGray
        Me.pnlCloseCash.Controls.Add(Me.lblccTips)
        Me.pnlCloseCash.Controls.Add(Me.Label7)
        Me.pnlCloseCash.Controls.Add(Me.lblOverShort)
        Me.pnlCloseCash.Controls.Add(Me.lblCashAtClose)
        Me.pnlCloseCash.Controls.Add(Me.lblCashOut)
        Me.pnlCloseCash.Controls.Add(Me.lblCashIn)
        Me.pnlCloseCash.Controls.Add(Me.lblCashAtOpen)
        Me.pnlCloseCash.Controls.Add(Me.lblDateTime)
        Me.pnlCloseCash.Controls.Add(Me.lblOpenedBy)
        Me.pnlCloseCash.Controls.Add(Me.lblOS)
        Me.pnlCloseCash.Controls.Add(Me.Label6)
        Me.pnlCloseCash.Controls.Add(Me.Label5)
        Me.pnlCloseCash.Controls.Add(Me.Label4)
        Me.pnlCloseCash.Controls.Add(Me.Label3)
        Me.pnlCloseCash.Controls.Add(Me.Label2)
        Me.pnlCloseCash.Location = New System.Drawing.Point(144, 128)
        Me.pnlCloseCash.Name = "pnlCloseCash"
        Me.pnlCloseCash.Size = New System.Drawing.Size(208, 296)
        Me.pnlCloseCash.TabIndex = 9
        Me.pnlCloseCash.Visible = False
        '
        'lblccTips
        '
        Me.lblccTips.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblccTips.ForeColor = System.Drawing.Color.White
        Me.lblccTips.Location = New System.Drawing.Point(136, 168)
        Me.lblccTips.Name = "lblccTips"
        Me.lblccTips.Size = New System.Drawing.Size(64, 24)
        Me.lblccTips.TabIndex = 14
        Me.lblccTips.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(16, 168)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 24)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "cc Tips $"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblOverShort
        '
        Me.lblOverShort.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOverShort.ForeColor = System.Drawing.Color.White
        Me.lblOverShort.Location = New System.Drawing.Point(136, 248)
        Me.lblOverShort.Name = "lblOverShort"
        Me.lblOverShort.Size = New System.Drawing.Size(64, 24)
        Me.lblOverShort.TabIndex = 12
        Me.lblOverShort.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCashAtClose
        '
        Me.lblCashAtClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCashAtClose.ForeColor = System.Drawing.Color.White
        Me.lblCashAtClose.Location = New System.Drawing.Point(136, 200)
        Me.lblCashAtClose.Name = "lblCashAtClose"
        Me.lblCashAtClose.Size = New System.Drawing.Size(64, 24)
        Me.lblCashAtClose.TabIndex = 11
        Me.lblCashAtClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCashOut
        '
        Me.lblCashOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCashOut.ForeColor = System.Drawing.Color.White
        Me.lblCashOut.Location = New System.Drawing.Point(136, 144)
        Me.lblCashOut.Name = "lblCashOut"
        Me.lblCashOut.Size = New System.Drawing.Size(64, 24)
        Me.lblCashOut.TabIndex = 10
        Me.lblCashOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCashIn
        '
        Me.lblCashIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCashIn.ForeColor = System.Drawing.Color.White
        Me.lblCashIn.Location = New System.Drawing.Point(136, 112)
        Me.lblCashIn.Name = "lblCashIn"
        Me.lblCashIn.Size = New System.Drawing.Size(64, 24)
        Me.lblCashIn.TabIndex = 9
        Me.lblCashIn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCashAtOpen
        '
        Me.lblCashAtOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCashAtOpen.ForeColor = System.Drawing.Color.White
        Me.lblCashAtOpen.Location = New System.Drawing.Point(136, 80)
        Me.lblCashAtOpen.Name = "lblCashAtOpen"
        Me.lblCashAtOpen.Size = New System.Drawing.Size(64, 24)
        Me.lblCashAtOpen.TabIndex = 8
        Me.lblCashAtOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDateTime
        '
        Me.lblDateTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateTime.ForeColor = System.Drawing.Color.White
        Me.lblDateTime.Location = New System.Drawing.Point(48, 48)
        Me.lblDateTime.Name = "lblDateTime"
        Me.lblDateTime.Size = New System.Drawing.Size(144, 24)
        Me.lblDateTime.TabIndex = 7
        Me.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOpenedBy
        '
        Me.lblOpenedBy.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpenedBy.ForeColor = System.Drawing.Color.White
        Me.lblOpenedBy.Location = New System.Drawing.Point(104, 16)
        Me.lblOpenedBy.Name = "lblOpenedBy"
        Me.lblOpenedBy.Size = New System.Drawing.Size(88, 32)
        Me.lblOpenedBy.TabIndex = 6
        Me.lblOpenedBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOS
        '
        Me.lblOS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOS.ForeColor = System.Drawing.Color.White
        Me.lblOS.Location = New System.Drawing.Point(24, 248)
        Me.lblOS.Name = "lblOS"
        Me.lblOS.Size = New System.Drawing.Size(88, 24)
        Me.lblOS.TabIndex = 5
        Me.lblOS.Text = "Short $ "
        Me.lblOS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblOS.Visible = False
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(16, 200)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 24)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Cash at Close $ "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(32, 144)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 24)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Cash Out $ "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(32, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 24)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Cash In $ "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(16, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 24)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Cash at Open $ "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(8, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 32)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Opened By :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlOtherDrawers
        '
        Me.pnlOtherDrawers.BackColor = System.Drawing.Color.LightSlateGray
        Me.pnlOtherDrawers.Location = New System.Drawing.Point(152, 456)
        Me.pnlOtherDrawers.Name = "pnlOtherDrawers"
        Me.pnlOtherDrawers.Size = New System.Drawing.Size(200, 100)
        Me.pnlOtherDrawers.TabIndex = 10
        Me.pnlOtherDrawers.Visible = False
        '
        'CashDrawer_UC
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Controls.Add(Me.pnlOtherDrawers)
        Me.Controls.Add(Me.pnlCloseCash)
        Me.Controls.Add(Me.lblCashInstructions)
        Me.Controls.Add(Me.lblOpenClose)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.NumberPadMedium1)
        Me.Controls.Add(Me.lblTerminalName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "CashDrawer_UC"
        Me.Size = New System.Drawing.Size(608, 512)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlCloseCash.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub InitializeOther()

        Me.NumberPadMedium1.DecimalUsed = True
     
        If dtTerminalsMethod.Rows.Count > 1 Then
            ' we have more than 1 terminal
            If employeeAuthorization.OperationLevel > 1 Then
                Me.btnOther.Enabled = True
            End If
        End If

        If ThisCashTerminal = 0 Or ThisCashTerminal = 1 Then '0 Then 
            Me.btnOther.Enabled = False
            PrepareThisCashTerminal()
        Else
            PrepareAnotherCashTerminal()
        End If

    End Sub

    Private Sub SwitchToOpenAnotherCashDrawer()
        WhatToCashDrawer = "Open"
        Me.lblOpenClose.Text = WhatToCashDrawer
        Me.btnOpen.Enabled = True
        Me.btnClose.Enabled = False
        Me.btnSwitch.Enabled = False

        Me.pnlCloseCash.Visible = False
        Me.pnlOtherDrawers.Visible = False
        Me.lblCashInstructions.Text = "Enter Open Cash"

        Try
            DetermineOpenCashDrawer(currentTerminal.CurrentDailyCode)
            With dvTermsOpen
                .Table = dtTermsOpen
                .RowFilter = "TerminalsPrimaryKey = " & currentTerminal.TermPrimaryKey
            End With
            InitializeOther()
        Catch ex As Exception
            CloseConnection()
        End Try

    End Sub
    Private Sub PrepareThisCashTerminal()

        '     Me.btnOther.Enabled = False
        Me.pnlOtherDrawers.Visible = False
        Me.lblTerminalName.Text = currentTerminal.TermName

        If dvTermsOpen.Count = 0 Then
            ' we are ready to open cash drawer
            Me.btnOpen.Enabled = True
            Me.btnClose.Enabled = False
            Me.btnSwitch.Enabled = False
            WhatToCashDrawer = "Open"

        ElseIf dvTermsOpen.Count = 1 Then
            ' we are ready to close or switch cash drawer
            Me.btnOpen.Enabled = False
            Me.btnClose.Enabled = True
            Me.btnSwitch.Enabled = True
            WhatToCashDrawer = "Close"

            activeTerminalsOpenID = dvTermsOpen(0)("TerminalsOpenID")

            StartCloseDrawer()

        ElseIf dvTermsOpen.Count > 1 Then
            ' there is a problem, we should not have more than one drawer open at a time

        End If

        Me.lblOpenClose.Text = WhatToCashDrawer

    End Sub


    Private Sub PrepareAnotherCashTerminal()

        Me.btnOpen.Enabled = False
        Me.btnClose.Enabled = False
        Me.btnSwitch.Enabled = False
        Me.btnOther.Enabled = True

    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        WhatToCashDrawer = "Open"
        Me.lblOpenClose.Text = WhatToCashDrawer

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        If Not WhatToCashDrawer = "Close" Then
            StartCloseDrawer()
            WhatToCashDrawer = "Close"
            Me.lblOpenClose.Text = WhatToCashDrawer
        End If

    End Sub

    Private Sub btnSwitch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSwitch.Click
        WhatToCashDrawer = "Switch"
        Me.lblOpenClose.Text = WhatToCashDrawer

    End Sub

    Private Sub btnOther_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOther.Click
        Dim oRow As DataRow
        Dim tRow As DataRow
        Dim x As Single = 10
        Dim y As Single = 10
        Dim index As Integer

        Dim t As Integer

        If employeeAuthorization.OperationLevel > 0 Or employeeAuthorization.SystemLevel > 0 Then
            'allow manager to swith terminals for close out
            Me.pnlOtherDrawers.Visible = True

            '         For t = 1 To 20

            For Each oRow In dtTermsOpen.Rows
                'add button for each terminal name
                btnOtherTerminals(index) = New Button
                With btnOtherTerminals(index)
                    .Location = New Point(x, y)
                    .BackColor = FromArgb(CType(59, Byte), CType(96, Byte), CType(141, Byte))
                    .Size = New Size(90, 30)
                    .ForeColor = c3

                    If t = 8 Then
                        .Text = "Last"
                    Else
                        For Each tRow In dtTerminalsMethod.Rows
                            If oRow("TerminalsPrimaryKey") = tRow("TerminalsPrimaryKey") Then
                                .Text = tRow("TerminalName")
                            End If
                        Next
                    End If

                    AddHandler btnOtherTerminals(index).Click, AddressOf OtherTerminalsButton_Click
                    Me.pnlOtherDrawers.Controls.Add(btnOtherTerminals(index))
                End With
                y += 35
                index += 1
                If index = 8 Then
                    x = 110
                    y = 10
                ElseIf index = 16 Then
                    Exit Sub
                End If
            Next
            '      Next

        Else
            MsgBox(actingManager.FullName & " is not authorized for Operational changes.")
            Exit Sub
        End If

    End Sub

    Private Sub OtherTerminalsButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim oRow As DataRow
        Dim tRow As DataRow

        For Each tRow In dtTerminalsMethod.Rows
            If sender.text = tRow("TerminalName") Then
                For Each oRow In dtTermsOpen.Rows 'dtTerminalsMethod.Rows
                    If oRow("TerminalsPrimaryKey") = tRow("TerminalsPrimaryKey") Then
                        With dvTermsOpen
                            .Table = dtTermsOpen
                            .RowFilter = "TerminalsOpenID = " & oRow("TerminalsOpenID")
                        End With
                        PrepareThisCashTerminal()
                        Exit For
                    End If
                Next

                '444        With dvTermsOpen
                '        .Table = dtTermsOpen
                '       .RowFilter = "TerminalsPrimaryKey = " & tRow("TerminalsPrimaryKey")
                '      End With
                '444      PrepareThisCashTerminal()
                Exit For
            End If
        Next



    End Sub


    Private Sub CashAmountEntered(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumberPadMedium1.NumberEntered

        Me.btnAccept.Enabled = True

        If WhatToCashDrawer = "Open" Then
            btnAccept_Click(sender, e)

        ElseIf WhatToCashDrawer = "Close" Or WhatToCashDrawer = "Switch" Then

            cashInfo._closeCash = Me.NumberPadMedium1.NumberTotal

            Me.lblCashAtClose.Text = cashInfo._closeCash
            cashInfo._overShort = Format((cashInfo._closeCash - (cashInfo._cashBeforeOut + cashInfo._cashOut + cashInfo._openCash)), "###,###.00") '(cashInfo._openCash + cashInfo._cashSales + cashInfo._cashOut)), "###,###.00")
            If cashInfo._overShort > 0 Then
                ' the drawer is over
                Me.lblOS.Text = "Over $ "
            Else
                Me.lblOS.Text = "Short $ "
            End If
            Me.lblOverShort.Text = cashInfo._overShort
            Me.lblOS.Visible = True

        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim prt As New PrintHelper

        If Me.btnAccept.Enabled = True Then
            Try
                prt.PrintCashSalesDrawer(cashInfo)
            Catch ex As Exception

            End Try
        End If

        Me.Dispose()

    End Sub


    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If WhatToCashDrawer = "Open" Then
            If Me.NumberPadMedium1.NumberTotal = 0 Then
                If MsgBox("You are opening the Cash Drawer with $ 0", MsgBoxStyle.OKCancel) = MsgBoxResult.Cancel Then
                    Exit Sub
                Else
                    OpenCashDrawer(NumberPadMedium1.NumberTotal, currentTerminal.TermPrimaryKey)
                    Me.Dispose()
                End If

            Else
                If MsgBox("OPENING with $ " & Me.NumberPadMedium1.NumberTotal.ToString, MsgBoxStyle.OKCancel) = MsgBoxResult.Cancel Then
                    Exit Sub
                Else
                    OpenCashDrawer(NumberPadMedium1.NumberTotal, currentTerminal.TermPrimaryKey)
                    Me.Dispose()
                End If
            End If

        ElseIf WhatToCashDrawer = "Close" Or WhatToCashDrawer = "Switch" Then
            If cashInfo._closeCash = 0 Then
                CashAmountEntered(Nothing, Nothing)
                'this just ensures that we hit Enter on NumberPad
            End If
            If cashInfo._closeCash = 0 Then
                If MsgBox("You are closing the Cash Drawer with $ 0", MsgBoxStyle.OKCancel) = MsgBoxResult.Cancel Then
                    Exit Sub
                Else
                    PopulateCloseData()
                End If
            Else
                PopulateCloseData()
            End If

        End If
    End Sub

    Private Sub StartCloseDrawer()

        If typeProgram = "Online_Demo" Then
            '           cashInfo._netsales = "1,000.00"
            '          cashInfo._ccSales = "100.00"
            '         cashInfo._cashSales = "900.00"
            '        cashInfo._ccTips = "50.00"
            cashInfo._openCash = Format(demoCashOpen, "###,###.00")
        Else

        End If

        DetermineCashOutDrawer()



        Try
            DetermineCashTransactions(activeTerminalsOpenID)
            If typeProgram = "Online_Demo" Then
                cashInfo._openCash = Format(demoCashOpen, "###,###.00")
            Else
                cashInfo._openCash = dvTermsOpen(0)("OpenCash")
            End If

            '    If dsOrder.Tables("CashIn").Rows.Count > 0 Then
            '   _cashIn = (dsOrder.Tables("CashIn").Compute("Sum(PaymentAmount)+ Sum(Surcharge)", ""))
            '    Else
            '       _cashIn = 0
            '  End If

            If dsOrder.Tables("CashOut").Rows.Count > 0 Then
                cashInfo._cashOut = Format((dsOrder.Tables("CashOut").Compute("Sum(PaymentAmount) + Sum(Surcharge)", "")), "###,###.00")
            Else
                cashInfo._cashOut = 0
            End If

            '    cashInfo._cashSales = Format(cashInfo._netsales - cashInfo._ccSales, "###,###.00")
            cashInfo._cashBeforeOut = Format(cashInfo._cashSales - cashInfo._ccTips, "###,###.00")
            cashInfo._drawerTotal = Format(cashInfo._cashBeforeOut + cashInfo._cashOut, "###,###.00")


            Me.pnlCloseCash.Visible = True
            Me.pnlOtherDrawers.Visible = False
            Me.lblCashInstructions.Text = "Enter Close Cash"
            Me.lblOpenedBy.Text = dvTermsOpen(0)("FirstName") & " " & dvTermsOpen(0)("LastName")
            Me.lblDateTime.Text = dvTermsOpen(0)("OpenTime")
            Me.lblCashAtOpen.Text = cashInfo._openCash
            Me.lblCashIn.Text = cashInfo._cashSales '_cashIn
            Me.lblCashOut.Text = cashInfo._cashOut
            Me.lblccTips.Text = cashInfo._ccTips

        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)

        End Try

    End Sub

    Friend Sub DetermineCashOutDrawer()

        If typeProgram = "Online_Demo" Then
            cashInfo._netsales = Format((dsOrderDemo.Tables("PaymentsAndCredits").Compute("Sum(PaymentAmount) + Sum(Surcharge)", "PaymentTypeID > 0")), "###,###.00")

            cashInfo._ccSales = "100.00"
            cashInfo._cashSales = "900.00"
            cashInfo._ccTips = "50.00"
        End If




        Dim cmd As SqlClient.SqlCommand

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()

            cmd = New SqlClient.SqlCommand("SELECT PaymentAmount, Surcharge FROM PaymentsAndCredits WHERE (PaymentTypeID > -1 OR PaymentTypeID < -90) AND LocationID = '" & companyInfo.LocationID & "' AND DailyCode = '" & currentTerminal.CurrentDailyCode & "' AND TerminalsOpenID = '" & activeTerminalsOpenID & "'", sql.cn)
            '    cmd = New SqlClient.SqlCommand("SELECT PaymentAmount, Surcharge FROM PaymentsAndCredits WHERE LocationID = '" & companyInfo.LocationID & "'", sql.cn)
            cashInfo._netsales = GenerateOrderTables.ReadCashOutData(cmd, "Sales") ', "###,###.00")

            '444    cmd = New SqlClient.SqlCommand("SELECT PaymentsAndCredits.PaymentAmount, PaymentsAndCredits.Surcharge, AABPaymentType.PaymentFlag FROM PaymentsAndCredits LEFT OUTER JOIN AABPaymentType ON PaymentsAndCredits.PaymentTypeID = AABPaymentType.PaymentTypeID WHERE AABPaymentType.PaymentFlag = 'cc' AND LocationID = '" & companyInfo.LocationID & "' AND DailyCode = '" & currentTerminal.CurrentDailyCode & "' AND TerminalsOpenID = '" & activeTerminalsOpenID & "'", sql.cn)
            cmd = New SqlClient.SqlCommand("SELECT PaymentAmount, Surcharge FROM PaymentsAndCredits WHERE PaymentTypeID > 1 AND LocationID = '" & companyInfo.LocationID & "' AND DailyCode = '" & currentTerminal.CurrentDailyCode & "' AND TerminalsOpenID = '" & activeTerminalsOpenID & "'", sql.cn)
            cashInfo._ccSales = Format(GenerateOrderTables.ReadCashOutData(cmd, "Sales"), "###,###.00")
         
            cmd = New SqlClient.SqlCommand("SELECT PaymentAmount, Surcharge FROM PaymentsAndCredits WHERE PaymentTypeID = 1 AND LocationID = '" & companyInfo.LocationID & "' AND DailyCode = '" & currentTerminal.CurrentDailyCode & "' AND TerminalsOpenID = '" & activeTerminalsOpenID & "'", sql.cn)
            cashInfo._cashSales = Format(GenerateOrderTables.ReadCashOutData(cmd, "Sales"), "###,###.00")

            '444   cmd = New SqlClient.SqlCommand("SELECT PaymentsAndCredits.Tip, AABPaymentType.PaymentFlag FROM PaymentsAndCredits LEFT OUTER JOIN AABPaymentType ON PaymentsAndCredits.PaymentTypeID = AABPaymentType.PaymentTypeID WHERE AABPaymentType.PaymentFlag = 'cc' AND LocationID = '" & companyInfo.LocationID & "' AND DailyCode = '" & currentTerminal.CurrentDailyCode & "' AND TerminalsOpenID = '" & activeTerminalsOpenID & "'", sql.cn)
            cmd = New SqlClient.SqlCommand("SELECT Tip FROM PaymentsAndCredits WHERE PaymentTypeID > 1 AND SwipeType > 0 AND LocationID = '" & companyInfo.LocationID & "' AND DailyCode = '" & currentTerminal.CurrentDailyCode & "' AND TerminalsOpenID = '" & activeTerminalsOpenID & "'", sql.cn)
            cashInfo._ccTips = Format(GenerateOrderTables.ReadCashOutData(cmd, "Tip"), "###,###.00")

            '     MsgBox("Net Sales  " & cashInfo._netsales)
            '    MsgBox("cc Sales  " & cashInfo._ccSales)
            '   MsgBox("cash Sales  " & cashInfo._cashSales)
            '  MsgBox("tips  " & cashInfo._ccTips)

            sql.cn.Close()

        Catch ex As Exception

            CloseConnection()
            MsgBox(ex.Message)
        End Try




    End Sub



 

    Private Sub PopulateCloseData()
        Dim prt As New PrintHelper

        If dvTermsOpen.Count > 0 Then

            dvTermsOpen(0)("CloseBy") = actingManager.EmployeeID
            dvTermsOpen(0)("CloseTime") = Now
            dvTermsOpen(0)("CloseCash") = cashInfo._closeCash
            dvTermsOpen(0)("CashIn") = cashInfo._cashSales  '_cashIn
            dvTermsOpen(0)("CashOut") = cashInfo._cashOut
            dvTermsOpen(0)("OverShort") = cashInfo._overShort
            dvTermsOpen(0)("ReasonShort") = DBNull.Value

            Try
                UpdateTermsOpen()
                dsOrder.Tables("TermsOpen").AcceptChanges()
                prt.PrintCashSalesDrawer(cashInfo)

                If WhatToCashDrawer = "Switch" Then
                    SwitchToOpenAnotherCashDrawer()
                Else
                    If ThisCashTerminal > 1 Then
                        'this means we still have terminals open
                        ThisCashTerminal -= 1
                        RaiseEvent TerminalsNowOpen(ThisCashTerminal)
                        Me.pnlCloseCash.Visible = False
                        PrepareAnotherCashTerminal()
                    Else
                        RaiseEvent ResetClosingData()
                        Me.Dispose()
                    End If
                End If
            Catch ex As Exception
                CloseConnection()
                MsgBox(ex.Message)
            End Try
        Else
            RaiseEvent ResetClosingData()
            Me.Dispose()
        End If


    End Sub

End Class
