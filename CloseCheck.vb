Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Xml.Serialization
Imports System.Runtime.InteropServices
Imports DataSet_Builder
Imports System.Security.Cryptography
Imports System.Security.Policy
Imports DSICLIENTXLib
Imports SIM

Public Class CloseCheck
    Inherits System.Windows.Forms.UserControl

    Dim dsi As DSICLIENTXLib.DSICLientX
    '   Dim dsi As New AxDSICLIENTXLib.AxDSICLientX
    '   Private WithEvents PaywarePCCharge As New SIM.Charge
    Private WithEvents PaywarePCCharge As SIM.Charge

    Dim WithEvents closeCheckTotals As CheckTotal_UC
    '   Dim WithEvents closeCheckAdjustments As CheckAdjustment_UC
    Friend WithEvents readAuth222 As ReadCredit
    Friend releaseFlag As Boolean
    '   Friend singleSplit As Boolean
    Dim WithEvents TabEnterScreen As Tab_Screen
    Dim ccDisplay As CashClose_UC
    Dim paywareAuthInfo As Info2_UC

    Dim paymentPanel(5) As DataSet_Builder.Payment_UC
    Dim authPayment As PaymentInfo

    '  Friend WithEvents tmrCardRead As System.Windows.Forms.Timer


    Dim dvBSGS As DataView
    Dim dvCombo As DataView
    Dim comboPrice As Decimal
    Dim dvCoupon As DataView

    Friend btnPromo(20) As KitchenButton
    Dim btnKitchen As KitchenButton
    '    Dim sql As New DataSet_Builder.SQLHelper(connectserver)
    Dim prt As New PrintHelper

    Dim closeTimeoutCounter As Integer      'not used

    Dim maxDollar As Decimal     '   same as maxTable
    Dim maxCheck As Integer
    Dim maxTable As Integer     'have to DIm in Orderform or part of current table structure
    Dim customerNumber As Integer       'for promotions

    Dim PromotionApplied(20) As Boolean
    '      old   '  Dim promoNumber As Integer = 1
    '    Dim CashPaymentTendered As Boolean
    '  Dim AmountToChange As Decimal

    Dim roundingError As Decimal
    Dim _remainingBalancesZero As Boolean
    Dim doNotAutoCreditCards As Boolean
    Dim creditAmountAdjusted As Boolean
    Dim _checkGiftIssuingAmount As Decimal
    'above will be a positive number, when paymentAmount will be negative for issuing gift
    Dim _lastPurchaseIssueAmount As Decimal

    Dim numActivePaymentsByCheck As Integer
    Dim _remainingBalance As Decimal     'on both
    Dim _totalCheckBalance As Decimal
    Dim _giftAddingAmount As Boolean

    Dim paymentRowIndex As Integer
    Dim startPaymentIndex As Integer = 1
    '    Dim unappliedRowIndex As Integer

    '   Event DisposeSplitScreen() '(ByVal sender As Object, ByVal e As System.EventArgs)
    Event CloseGotoSplitting(ByVal sender As Object, ByVal e As System.EventArgs)
    Event CloseExitAndRelease()
    Event CloseExiting(ByVal going As Boolean, ByVal RemainingBalancesZero As Boolean)         'disposes splitscreen
    Event AuthPayments(ByRef authamount As PreAuthAmountClass, ByRef authtransaction As PreAuthTransactionClass, ByVal cardSwipedDatabaseInfo As Boolean)
    Event SplitSingleCheck()
    Event SelectedReOrder(ByVal dt As DataTable, ByVal tabTestNeeded As Boolean)
    Event FireTabScreen(ByVal startInSearch As String, ByVal searchCriteria As String)
    Event MakeGiftAddingAmountTrue()
    Event MerchantAuthPayment(ByVal paymentID As Integer, ByVal justActive As Boolean)

    Friend Property RemainingBalancesZero() As Boolean
        Get
            Return _remainingBalancesZero
        End Get
        Set(ByVal Value As Boolean)
            _remainingBalancesZero = Value
        End Set
    End Property

    Friend Property RemainingBalance() As Decimal
        Get
            Return _remainingBalance
        End Get
        Set(ByVal Value As Decimal)
            _remainingBalance = Value
        End Set
    End Property

    Friend Property TotalCheckBalance() As Decimal
        Get
            Return _totalCheckBalance
        End Get
        Set(ByVal Value As Decimal)
            _totalCheckBalance = Value
        End Set
    End Property

    Friend Property GiftAddingAmount() As Boolean
        Get
            Return _giftAddingAmount
        End Get
        Set(ByVal value As Boolean)
            _giftAddingAmount = value
        End Set
    End Property


    Private Shared _closeAuthAmount As PreAuthAmountClass
    Private Shared _closeAuthTransaction As PreAuthTransactionClass
    Private Shared _closeAuthAccount As AccountClass


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal closingCheck As Integer)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        InitializeOther(closingCheck)

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
    Friend WithEvents btnCloseCash As System.Windows.Forms.Button
    Friend WithEvents btnClose100 As System.Windows.Forms.Button
    Friend WithEvents btnClose50 As System.Windows.Forms.Button
    Friend WithEvents btnClose20 As System.Windows.Forms.Button
    Friend WithEvents btnClose1 As System.Windows.Forms.Button
    Friend WithEvents btnClose5 As System.Windows.Forms.Button
    Friend WithEvents btnClose10 As System.Windows.Forms.Button
    Friend WithEvents btnClosePrint As System.Windows.Forms.Button
    Friend WithEvents btnClosePrintAll As System.Windows.Forms.Button
    Friend WithEvents btnClosePayment As System.Windows.Forms.Button
    Friend WithEvents btnCloseMgr As System.Windows.Forms.Button
    Friend WithEvents btnClosePromo As System.Windows.Forms.Button
    Friend WithEvents btnCloseGift As System.Windows.Forms.Button
    Friend WithEvents btnCloseAutoTip As System.Windows.Forms.Button
    Friend WithEvents btnCloseGiftCardAdd As System.Windows.Forms.Button
    Friend WithEvents btnCloseExit As System.Windows.Forms.Button
    Friend WithEvents pnlPaymentTypes As System.Windows.Forms.Panel
    Friend WithEvents btnCloseRelease As System.Windows.Forms.Button
    Friend WithEvents btnCloseCheckNumber As System.Windows.Forms.Button
    Friend WithEvents btnCloseSplit As System.Windows.Forms.Button
    Friend WithEvents btnCloseManualcc As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents pnlClosePayments As System.Windows.Forms.Panel
    Friend WithEvents NumberPadLargeold As DataSet_Builder.NumberPadLarge
    Friend WithEvents pnlBalance As System.Windows.Forms.Panel
    Friend WithEvents lblBalance As System.Windows.Forms.Label
    Friend WithEvents btnAuthAll As System.Windows.Forms.Button
    Friend WithEvents btnAuthActive As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents lblBalanceDetail As System.Windows.Forms.Label
    Friend WithEvents btnMorePayments As System.Windows.Forms.Button
    Friend WithEvents btnDup As System.Windows.Forms.Button
    Friend WithEvents btnVoiceAuth As System.Windows.Forms.Button
    Friend WithEvents NumberPadLarge1 As DataSet_Builder.NumberPadLarge
    Friend WithEvents pnlExit As System.Windows.Forms.Panel
    Friend WithEvents pnlPayOptions As System.Windows.Forms.Panel
    Friend WithEvents pnlPayRemove As System.Windows.Forms.Panel
    Friend WithEvents btnDemoCC As System.Windows.Forms.Button

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlPaymentTypes = New System.Windows.Forms.Panel
        Me.btnCloseGift = New System.Windows.Forms.Button
        Me.btnCloseManualcc = New System.Windows.Forms.Button
        Me.btnClose1 = New System.Windows.Forms.Button
        Me.btnClose5 = New System.Windows.Forms.Button
        Me.btnClose10 = New System.Windows.Forms.Button
        Me.btnClose20 = New System.Windows.Forms.Button
        Me.btnClose50 = New System.Windows.Forms.Button
        Me.btnClose100 = New System.Windows.Forms.Button
        Me.btnCloseCash = New System.Windows.Forms.Button
        Me.btnClosePrint = New System.Windows.Forms.Button
        Me.btnClosePrintAll = New System.Windows.Forms.Button
        Me.btnClosePayment = New System.Windows.Forms.Button
        Me.btnCloseMgr = New System.Windows.Forms.Button
        Me.btnClosePromo = New System.Windows.Forms.Button
        Me.btnCloseExit = New System.Windows.Forms.Button
        Me.btnCloseAutoTip = New System.Windows.Forms.Button
        Me.btnCloseGiftCardAdd = New System.Windows.Forms.Button
        Me.pnlExit = New System.Windows.Forms.Panel
        Me.btnCloseCheckNumber = New System.Windows.Forms.Button
        Me.btnCloseRelease = New System.Windows.Forms.Button
        Me.btnCloseSplit = New System.Windows.Forms.Button
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.pnlClosePayments = New System.Windows.Forms.Panel
        Me.pnlBalance = New System.Windows.Forms.Panel
        Me.lblBalanceDetail = New System.Windows.Forms.Label
        Me.btnAuthActive = New System.Windows.Forms.Button
        Me.btnAuthAll = New System.Windows.Forms.Button
        Me.lblBalance = New System.Windows.Forms.Label
        Me.btnRemove = New System.Windows.Forms.Button
        Me.pnlPayRemove = New System.Windows.Forms.Panel
        Me.btnMorePayments = New System.Windows.Forms.Button
        Me.btnDup = New System.Windows.Forms.Button
        Me.btnVoiceAuth = New System.Windows.Forms.Button
        Me.pnlPayOptions = New System.Windows.Forms.Panel
        Me.btnDemoCC = New System.Windows.Forms.Button
        Me.NumberPadLarge1 = New DataSet_Builder.NumberPadLarge
        Me.pnlPaymentTypes.SuspendLayout()
        Me.pnlExit.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlBalance.SuspendLayout()
        Me.pnlPayRemove.SuspendLayout()
        Me.pnlPayOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlPaymentTypes
        '
        Me.pnlPaymentTypes.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlPaymentTypes.Controls.Add(Me.btnCloseGift)
        Me.pnlPaymentTypes.Controls.Add(Me.btnCloseManualcc)
        Me.pnlPaymentTypes.Controls.Add(Me.btnClose1)
        Me.pnlPaymentTypes.Controls.Add(Me.btnClose5)
        Me.pnlPaymentTypes.Controls.Add(Me.btnClose10)
        Me.pnlPaymentTypes.Controls.Add(Me.btnClose20)
        Me.pnlPaymentTypes.Controls.Add(Me.btnClose50)
        Me.pnlPaymentTypes.Controls.Add(Me.btnClose100)
        Me.pnlPaymentTypes.Controls.Add(Me.btnCloseCash)
        Me.pnlPaymentTypes.Location = New System.Drawing.Point(104, 16)
        Me.pnlPaymentTypes.Name = "pnlPaymentTypes"
        Me.pnlPaymentTypes.Size = New System.Drawing.Size(392, 200)
        Me.pnlPaymentTypes.TabIndex = 1
        '
        'btnCloseGift
        '
        Me.btnCloseGift.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnCloseGift.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseGift.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCloseGift.Location = New System.Drawing.Point(8, 72)
        Me.btnCloseGift.Name = "btnCloseGift"
        Me.btnCloseGift.Size = New System.Drawing.Size(120, 56)
        Me.btnCloseGift.TabIndex = 11
        Me.btnCloseGift.Text = "Gift Cert"
        Me.btnCloseGift.UseVisualStyleBackColor = False
        '
        'btnCloseManualcc
        '
        Me.btnCloseManualcc.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnCloseManualcc.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseManualcc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCloseManualcc.Location = New System.Drawing.Point(8, 136)
        Me.btnCloseManualcc.Name = "btnCloseManualcc"
        Me.btnCloseManualcc.Size = New System.Drawing.Size(120, 56)
        Me.btnCloseManualcc.TabIndex = 7
        Me.btnCloseManualcc.Text = "Manual CC"
        Me.btnCloseManualcc.UseVisualStyleBackColor = False
        '
        'btnClose1
        '
        Me.btnClose1.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnClose1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnClose1.Location = New System.Drawing.Point(264, 136)
        Me.btnClose1.Name = "btnClose1"
        Me.btnClose1.Size = New System.Drawing.Size(120, 56)
        Me.btnClose1.TabIndex = 6
        Me.btnClose1.Text = "$1"
        Me.btnClose1.UseVisualStyleBackColor = False
        '
        'btnClose5
        '
        Me.btnClose5.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnClose5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnClose5.Location = New System.Drawing.Point(264, 72)
        Me.btnClose5.Name = "btnClose5"
        Me.btnClose5.Size = New System.Drawing.Size(120, 56)
        Me.btnClose5.TabIndex = 5
        Me.btnClose5.Text = "$5"
        Me.btnClose5.UseVisualStyleBackColor = False
        '
        'btnClose10
        '
        Me.btnClose10.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnClose10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnClose10.Location = New System.Drawing.Point(264, 8)
        Me.btnClose10.Name = "btnClose10"
        Me.btnClose10.Size = New System.Drawing.Size(120, 56)
        Me.btnClose10.TabIndex = 4
        Me.btnClose10.Text = "$10"
        Me.btnClose10.UseVisualStyleBackColor = False
        '
        'btnClose20
        '
        Me.btnClose20.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnClose20.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose20.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnClose20.Location = New System.Drawing.Point(136, 136)
        Me.btnClose20.Name = "btnClose20"
        Me.btnClose20.Size = New System.Drawing.Size(120, 56)
        Me.btnClose20.TabIndex = 3
        Me.btnClose20.Text = "$20"
        Me.btnClose20.UseVisualStyleBackColor = False
        '
        'btnClose50
        '
        Me.btnClose50.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnClose50.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose50.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnClose50.Location = New System.Drawing.Point(136, 72)
        Me.btnClose50.Name = "btnClose50"
        Me.btnClose50.Size = New System.Drawing.Size(120, 56)
        Me.btnClose50.TabIndex = 2
        Me.btnClose50.Text = "$50"
        Me.btnClose50.UseVisualStyleBackColor = False
        '
        'btnClose100
        '
        Me.btnClose100.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnClose100.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose100.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnClose100.Location = New System.Drawing.Point(136, 8)
        Me.btnClose100.Name = "btnClose100"
        Me.btnClose100.Size = New System.Drawing.Size(120, 56)
        Me.btnClose100.TabIndex = 1
        Me.btnClose100.Text = "$100"
        Me.btnClose100.UseVisualStyleBackColor = False
        '
        'btnCloseCash
        '
        Me.btnCloseCash.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnCloseCash.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnCloseCash.FlatAppearance.BorderSize = 0
        Me.btnCloseCash.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseCash.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCloseCash.Location = New System.Drawing.Point(8, 8)
        Me.btnCloseCash.Name = "btnCloseCash"
        Me.btnCloseCash.Size = New System.Drawing.Size(120, 56)
        Me.btnCloseCash.TabIndex = 0
        Me.btnCloseCash.Text = "Cash"
        Me.btnCloseCash.UseVisualStyleBackColor = False
        '
        'btnClosePrint
        '
        Me.btnClosePrint.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnClosePrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClosePrint.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnClosePrint.Location = New System.Drawing.Point(104, 0)
        Me.btnClosePrint.Name = "btnClosePrint"
        Me.btnClosePrint.Size = New System.Drawing.Size(88, 48)
        Me.btnClosePrint.TabIndex = 2
        Me.btnClosePrint.Text = "PRINT"
        Me.btnClosePrint.UseVisualStyleBackColor = False
        '
        'btnClosePrintAll
        '
        Me.btnClosePrintAll.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnClosePrintAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClosePrintAll.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnClosePrintAll.Location = New System.Drawing.Point(104, 48)
        Me.btnClosePrintAll.Name = "btnClosePrintAll"
        Me.btnClosePrintAll.Size = New System.Drawing.Size(88, 48)
        Me.btnClosePrintAll.TabIndex = 3
        Me.btnClosePrintAll.Text = "PRINT ALL"
        Me.btnClosePrintAll.UseVisualStyleBackColor = False
        '
        'btnClosePayment
        '
        Me.btnClosePayment.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnClosePayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClosePayment.Location = New System.Drawing.Point(8, 29)
        Me.btnClosePayment.Name = "btnClosePayment"
        Me.btnClosePayment.Size = New System.Drawing.Size(88, 48)
        Me.btnClosePayment.TabIndex = 4
        Me.btnClosePayment.Text = "PAYMENT"
        Me.btnClosePayment.UseVisualStyleBackColor = False
        '
        'btnCloseMgr
        '
        Me.btnCloseMgr.BackColor = System.Drawing.Color.SlateGray
        Me.btnCloseMgr.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseMgr.Location = New System.Drawing.Point(8, 160)
        Me.btnCloseMgr.Name = "btnCloseMgr"
        Me.btnCloseMgr.Size = New System.Drawing.Size(88, 48)
        Me.btnCloseMgr.TabIndex = 5
        Me.btnCloseMgr.Text = "Manager"
        Me.btnCloseMgr.UseVisualStyleBackColor = False
        '
        'btnClosePromo
        '
        Me.btnClosePromo.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnClosePromo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClosePromo.Location = New System.Drawing.Point(8, 92)
        Me.btnClosePromo.Name = "btnClosePromo"
        Me.btnClosePromo.Size = New System.Drawing.Size(88, 48)
        Me.btnClosePromo.TabIndex = 6
        Me.btnClosePromo.Text = "PROMOs"
        Me.btnClosePromo.UseVisualStyleBackColor = False
        '
        'btnCloseExit
        '
        Me.btnCloseExit.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnCloseExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseExit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCloseExit.Location = New System.Drawing.Point(0, 0)
        Me.btnCloseExit.Name = "btnCloseExit"
        Me.btnCloseExit.Size = New System.Drawing.Size(96, 48)
        Me.btnCloseExit.TabIndex = 7
        Me.btnCloseExit.Text = "EXIT"
        Me.btnCloseExit.UseVisualStyleBackColor = False
        '
        'btnCloseAutoTip
        '
        Me.btnCloseAutoTip.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseAutoTip.Location = New System.Drawing.Point(536, 80)
        Me.btnCloseAutoTip.Name = "btnCloseAutoTip"
        Me.btnCloseAutoTip.Size = New System.Drawing.Size(88, 48)
        Me.btnCloseAutoTip.TabIndex = 8
        Me.btnCloseAutoTip.Text = "Auto Gratuity"
        '
        'btnCloseGiftCardAdd
        '
        Me.btnCloseGiftCardAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseGiftCardAdd.Location = New System.Drawing.Point(536, 16)
        Me.btnCloseGiftCardAdd.Name = "btnCloseGiftCardAdd"
        Me.btnCloseGiftCardAdd.Size = New System.Drawing.Size(88, 48)
        Me.btnCloseGiftCardAdd.TabIndex = 9
        Me.btnCloseGiftCardAdd.Text = "Gift Add"
        '
        'pnlExit
        '
        Me.pnlExit.BackColor = System.Drawing.Color.Black
        Me.pnlExit.Controls.Add(Me.btnCloseCheckNumber)
        Me.pnlExit.Controls.Add(Me.btnCloseRelease)
        Me.pnlExit.Controls.Add(Me.btnCloseExit)
        Me.pnlExit.Controls.Add(Me.btnClosePrint)
        Me.pnlExit.Controls.Add(Me.btnClosePrintAll)
        Me.pnlExit.Controls.Add(Me.btnCloseSplit)
        Me.pnlExit.Location = New System.Drawing.Point(16, 8)
        Me.pnlExit.Name = "pnlExit"
        Me.pnlExit.Size = New System.Drawing.Size(288, 96)
        Me.pnlExit.TabIndex = 11
        '
        'btnCloseCheckNumber
        '
        Me.btnCloseCheckNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.btnCloseCheckNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseCheckNumber.Location = New System.Drawing.Point(200, 0)
        Me.btnCloseCheckNumber.Name = "btnCloseCheckNumber"
        Me.btnCloseCheckNumber.Size = New System.Drawing.Size(88, 48)
        Me.btnCloseCheckNumber.TabIndex = 9
        Me.btnCloseCheckNumber.UseVisualStyleBackColor = False
        '
        'btnCloseRelease
        '
        Me.btnCloseRelease.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnCloseRelease.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseRelease.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCloseRelease.Location = New System.Drawing.Point(0, 48)
        Me.btnCloseRelease.Name = "btnCloseRelease"
        Me.btnCloseRelease.Size = New System.Drawing.Size(96, 48)
        Me.btnCloseRelease.TabIndex = 8
        Me.btnCloseRelease.Text = "RELEASE"
        Me.btnCloseRelease.UseVisualStyleBackColor = False
        '
        'btnCloseSplit
        '
        Me.btnCloseSplit.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnCloseSplit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseSplit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCloseSplit.Location = New System.Drawing.Point(200, 48)
        Me.btnCloseSplit.Name = "btnCloseSplit"
        Me.btnCloseSplit.Size = New System.Drawing.Size(88, 48)
        Me.btnCloseSplit.TabIndex = 10
        Me.btnCloseSplit.Text = "SPLIT"
        Me.btnCloseSplit.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Black
        Me.Panel3.Controls.Add(Me.btnClosePayment)
        Me.Panel3.Controls.Add(Me.btnCloseMgr)
        Me.Panel3.Controls.Add(Me.btnClosePromo)
        Me.Panel3.Controls.Add(Me.pnlPaymentTypes)
        Me.Panel3.Location = New System.Drawing.Point(8, 16)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(512, 232)
        Me.Panel3.TabIndex = 13
        '
        'pnlClosePayments
        '
        Me.pnlClosePayments.BackColor = System.Drawing.Color.Black
        Me.pnlClosePayments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlClosePayments.ForeColor = System.Drawing.Color.Black
        Me.pnlClosePayments.Location = New System.Drawing.Point(318, 312)
        Me.pnlClosePayments.Name = "pnlClosePayments"
        Me.pnlClosePayments.Size = New System.Drawing.Size(456, 384)
        Me.pnlClosePayments.TabIndex = 14
        '
        'pnlBalance
        '
        Me.pnlBalance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBalance.Controls.Add(Me.lblBalanceDetail)
        Me.pnlBalance.Controls.Add(Me.btnAuthActive)
        Me.pnlBalance.Controls.Add(Me.btnAuthAll)
        Me.pnlBalance.Controls.Add(Me.lblBalance)
        Me.pnlBalance.Location = New System.Drawing.Point(318, 696)
        Me.pnlBalance.Name = "pnlBalance"
        Me.pnlBalance.Size = New System.Drawing.Size(456, 72)
        Me.pnlBalance.TabIndex = 15
        '
        'lblBalanceDetail
        '
        Me.lblBalanceDetail.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblBalanceDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBalanceDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalanceDetail.Location = New System.Drawing.Point(336, 8)
        Me.lblBalanceDetail.Name = "lblBalanceDetail"
        Me.lblBalanceDetail.Size = New System.Drawing.Size(112, 56)
        Me.lblBalanceDetail.TabIndex = 5
        Me.lblBalanceDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAuthActive
        '
        Me.btnAuthActive.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnAuthActive.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAuthActive.ForeColor = System.Drawing.Color.White
        Me.btnAuthActive.Location = New System.Drawing.Point(160, 8)
        Me.btnAuthActive.Name = "btnAuthActive"
        Me.btnAuthActive.Size = New System.Drawing.Size(120, 56)
        Me.btnAuthActive.TabIndex = 3
        Me.btnAuthActive.Text = "Auth Active"
        Me.btnAuthActive.UseVisualStyleBackColor = False
        '
        'btnAuthAll
        '
        Me.btnAuthAll.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnAuthAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAuthAll.ForeColor = System.Drawing.Color.White
        Me.btnAuthAll.Location = New System.Drawing.Point(8, 8)
        Me.btnAuthAll.Name = "btnAuthAll"
        Me.btnAuthAll.Size = New System.Drawing.Size(120, 56)
        Me.btnAuthAll.TabIndex = 2
        Me.btnAuthAll.Text = "Auth All"
        Me.btnAuthAll.UseVisualStyleBackColor = False
        '
        'lblBalance
        '
        Me.lblBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalance.ForeColor = System.Drawing.Color.White
        Me.lblBalance.Location = New System.Drawing.Point(304, 8)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(64, 16)
        Me.lblBalance.TabIndex = 1
        Me.lblBalance.Text = "Bal"
        Me.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRemove
        '
        Me.btnRemove.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemove.ForeColor = System.Drawing.Color.White
        Me.btnRemove.Location = New System.Drawing.Point(128, 8)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(104, 56)
        Me.btnRemove.TabIndex = 4
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = False
        '
        'pnlPayRemove
        '
        Me.pnlPayRemove.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlPayRemove.Controls.Add(Me.btnMorePayments)
        Me.pnlPayRemove.Controls.Add(Me.btnRemove)
        Me.pnlPayRemove.Location = New System.Drawing.Point(780, 312)
        Me.pnlPayRemove.Name = "pnlPayRemove"
        Me.pnlPayRemove.Size = New System.Drawing.Size(240, 72)
        Me.pnlPayRemove.TabIndex = 16
        '
        'btnMorePayments
        '
        Me.btnMorePayments.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnMorePayments.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMorePayments.ForeColor = System.Drawing.Color.White
        Me.btnMorePayments.Location = New System.Drawing.Point(8, 8)
        Me.btnMorePayments.Name = "btnMorePayments"
        Me.btnMorePayments.Size = New System.Drawing.Size(104, 56)
        Me.btnMorePayments.TabIndex = 5
        Me.btnMorePayments.Text = "More Payments"
        Me.btnMorePayments.UseVisualStyleBackColor = False
        Me.btnMorePayments.Visible = False
        '
        'btnDup
        '
        Me.btnDup.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDup.Location = New System.Drawing.Point(536, 144)
        Me.btnDup.Name = "btnDup"
        Me.btnDup.Size = New System.Drawing.Size(88, 48)
        Me.btnDup.TabIndex = 17
        Me.btnDup.Text = "Duplicate Credit"
        '
        'btnVoiceAuth
        '
        Me.btnVoiceAuth.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVoiceAuth.Location = New System.Drawing.Point(536, 208)
        Me.btnVoiceAuth.Name = "btnVoiceAuth"
        Me.btnVoiceAuth.Size = New System.Drawing.Size(88, 48)
        Me.btnVoiceAuth.TabIndex = 18
        Me.btnVoiceAuth.Text = "Voice Auth"
        '
        'pnlPayOptions
        '
        Me.pnlPayOptions.BackColor = System.Drawing.Color.LightSlateGray
        Me.pnlPayOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPayOptions.Controls.Add(Me.btnDemoCC)
        Me.pnlPayOptions.Controls.Add(Me.btnVoiceAuth)
        Me.pnlPayOptions.Controls.Add(Me.btnDup)
        Me.pnlPayOptions.Controls.Add(Me.btnCloseAutoTip)
        Me.pnlPayOptions.Controls.Add(Me.btnCloseGiftCardAdd)
        Me.pnlPayOptions.Controls.Add(Me.Panel3)
        Me.pnlPayOptions.Location = New System.Drawing.Point(352, 16)
        Me.pnlPayOptions.Name = "pnlPayOptions"
        Me.pnlPayOptions.Size = New System.Drawing.Size(640, 272)
        Me.pnlPayOptions.TabIndex = 19
        '
        'btnDemoCC
        '
        Me.btnDemoCC.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnDemoCC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDemoCC.Location = New System.Drawing.Point(531, 203)
        Me.btnDemoCC.Name = "btnDemoCC"
        Me.btnDemoCC.Size = New System.Drawing.Size(104, 64)
        Me.btnDemoCC.TabIndex = 20
        Me.btnDemoCC.Text = "Demo Card Swipe"
        Me.btnDemoCC.UseVisualStyleBackColor = False
        Me.btnDemoCC.Visible = False
        '
        'NumberPadLarge1
        '
        Me.NumberPadLarge1.BackColor = System.Drawing.Color.DarkGray
        Me.NumberPadLarge1.DecimalUsed = False
        Me.NumberPadLarge1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.NumberPadLarge1.IntegerNumber = 0
        Me.NumberPadLarge1.Location = New System.Drawing.Point(778, 394)
        Me.NumberPadLarge1.Name = "NumberPadLarge1"
        Me.NumberPadLarge1.NumberString = ""
        Me.NumberPadLarge1.NumberTotal = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumberPadLarge1.Size = New System.Drawing.Size(244, 370)
        Me.NumberPadLarge1.TabIndex = 20
        '
        'CloseCheck
        '
        Me.BackColor = System.Drawing.Color.Black
        Me.Controls.Add(Me.NumberPadLarge1)
        Me.Controls.Add(Me.pnlPayOptions)
        Me.Controls.Add(Me.pnlPayRemove)
        Me.Controls.Add(Me.pnlBalance)
        Me.Controls.Add(Me.pnlClosePayments)
        Me.Controls.Add(Me.pnlExit)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "CloseCheck"
        Me.Size = New System.Drawing.Size(1024, 768)
        Me.pnlPaymentTypes.ResumeLayout(False)
        Me.pnlExit.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlBalance.ResumeLayout(False)
        Me.pnlPayRemove.ResumeLayout(False)
        Me.pnlPayOptions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub InitializeOther(ByVal closingCheck As Integer)

        Try
            'need to zero out all decimals
            _checkGiftIssuingAmount = 0
        
            If Not typeProgram = "Online_Demo" Then
                If companyInfo.processor = "Mercury" Then
                    dsi = New DSICLIENTXLib.DSICLientX
                End If
            End If

            DetermineTruncatedExperienceNumber()
            closeTimeoutCounter = 1

            If typeProgram = "Online_Demo" Then
                Me.btnDemoCC.Visible = True
                Me.btnDemoCC.BringToFront()
            End If
            '      AddHandler closeInactiveTimer.Tick, AddressOf InactiveScreenTimeout
            '     closeInactiveTimer.Interval = timeoutInterval
            '    closeInactiveTimer.Start()

            '     paymentRowIndex = dsOrder.Tables("PaymentsAndCredits").Rows.Count()
            CreateClosingDataViews(closingCheck, True)
            paymentRowIndex = dvClosingCheckPayments.Count

            GetNumberOfActivePayments(closingCheck)

            UpdateCheckNumberButton()

            closeCheckTotals = New CheckTotal_UC
            closeCheckTotals.Location = New Point(4, 112)
            Me.Controls.Add(closeCheckTotals)

            '444     DisplayAnyStoredPayments()
            'this is before balance info, so we don't have to save amounts
            '   and if the amounts change, which probably would if we exit closecheck

            RemainingBalance = closeCheckTotals.AttachTotalsToTotalView(currentTable.CheckNumber)
            TotalCheckBalance = closeCheckTotals.TotalCheckBalance
            ShowRemainingBalance()

            Dim authamount As PreAuthAmountClass
            Dim authTransaction As PreAuthTransactionClass

            '444      readAuth = New ReadCredit(False)
            '444   readAuth.GiftAddingAmount = False

            GenerateOrderTables.CreatespiderPOSDirectory()

        Catch ex As Exception

            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub ResetTimer()

        closeTimeoutCounter = 1

    End Sub

    Private Sub AddGeneratedControlsToPaymentPanel()

        Me.pnlPaymentTypes.Controls.Add(Me.btnCloseGift)
        Me.pnlPaymentTypes.Controls.Add(Me.btnCloseManualcc)
        Me.pnlPaymentTypes.Controls.Add(Me.btnClose1)
        Me.pnlPaymentTypes.Controls.Add(Me.btnClose5)
        Me.pnlPaymentTypes.Controls.Add(Me.btnClose10)
        Me.pnlPaymentTypes.Controls.Add(Me.btnClose20)
        Me.pnlPaymentTypes.Controls.Add(Me.btnClose50)
        Me.pnlPaymentTypes.Controls.Add(Me.btnClose100)
        Me.pnlPaymentTypes.Controls.Add(Me.btnCloseCash)

    End Sub

    Private Sub CreateInitialPanelsForThisCheck222(ByVal closingCheck As Integer)

        If dvClosingCheckPayments.Count > 5 Then              'paymentRowIndex > 5 Then
            ReDim paymentPanel(dvClosingCheckPayments.Count)  '(paymentRowIndex)
            btnMorePayments.Visible = True
        End If



    End Sub

    Private Function GetNumberOfActivePayments(ByVal closingCheck As Integer)

        Dim vRow As DataRowView
        Dim count As Integer
        Dim position As Integer
        Dim giftBalance As Decimal = 0

        Me.pnlClosePayments.Controls.Clear()
        '      unappliedRowIndex = 0

        If dvClosingCheckPayments.Count + dvUnAppliedPaymentsAndCredits_MWE.Count > 5 Then              'paymentRowIndex > 5 Then
            ReDim paymentPanel(dvClosingCheckPayments.Count)  '(paymentRowIndex)
            btnMorePayments.Visible = True
        End If

        '       For Each vRow In dvUnAppliedPaymentsAndCredits  'dvClosingCheckPayments      'oRow In dsOrder.Tables("PaymentsAndCredits").Rows
        '      count += 1
        '     If count >= startPaymentIndex And count <= (startPaymentIndex + 5) Then
        'CreateNewPaymentPanel(vRow, count, position)
        '      'old      unappliedRowIndex += 1
        '     position += 1
        '    End If
        '   Next

        For Each vRow In dvUnAppliedPaymentsAndCredits_MWE 'dvappliedpayments
            If vRow("PaymentFlag") = "Gift" Or vRow("PaymentFlag") = "Issue" Then ' And vRow("AuthCode") Is DBNull.Value Then
                If vRow("AuthCode") Is DBNull.Value Then
                    giftBalance = DetermineGiftBalance(vRow)
                Else
                    Dim newPayment As New Payment
                    'this is only to send balance
                    newPayment.RefNo = vRow("RefNum")
                    newPayment.AccountNumber = vRow("AccountNumber")
                    GenerateOrderTables.GiftCardTransaction(Nothing, newPayment, "Balance")
                    giftBalance = newPayment.Balance
                End If

            End If
            count += 1
            If count >= startPaymentIndex And count <= (startPaymentIndex + 5) Then
                CreateNewPaymentPanel(vRow, count, position, giftBalance)
                position += 1
            End If
        Next

        For Each vRow In dvClosingCheckPayments 'dvappliedpayments
            If vRow("PaymentFlag") = "Gift" Or vRow("PaymentFlag") = "Issue" Then ' And vRow("AuthCode") Is DBNull.Value Then
                If vRow("AuthCode") Is DBNull.Value Then
                    giftBalance = DetermineGiftBalance(vRow)
                Else
                    Dim newPayment As New Payment
                    'this is only to send balance
                    newPayment.RefNo = vRow("RefNum")
                    newPayment.AccountNumber = vRow("AccountNumber")
                    GenerateOrderTables.GiftCardTransaction(Nothing, newPayment, "Balance")
                    giftBalance = newPayment.Balance
                End If

            End If
            count += 1
            If count >= startPaymentIndex And count <= (startPaymentIndex + 5) Then
                CreateNewPaymentPanel(vRow, count, position, giftBalance)
                position += 1
            End If
        Next

        numActivePaymentsByCheck = count
        paymentRowIndex = dvClosingCheckPayments.Count + dvUnAppliedPaymentsAndCredits_MWE.Count
        ActiveThisPanel(paymentRowIndex)    'tried with 1 to activate last new payment
        '                                       too many things refer to paymentRowIndex
        '                                       is probably better flow with 1
        ShowRemainingBalance()

    End Function

    Private Function DetermineGiftBalance(ByRef vRow As DataRowView)
        ' we need to determine Gift Balance
        Dim giftPayment As New Payment
        Dim giftBalance As Decimal = 0

        For Each giftPayment In tabcc
            If giftPayment.experienceNumber = vRow("ExperienceNumber") Then
                If giftPayment.AccountNumber = vRow("AccountNumber") Then
                    giftBalance = giftPayment.Balance
                End If
            End If
        Next
        Return giftBalance
    End Function
    Private Sub btnMorePayments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMorePayments.Click

        startPaymentIndex += 1
        If (startPaymentIndex + 0) > paymentRowIndex Then
            startPaymentIndex = 1
            paymentRowIndex = 1
        End If
        GetNumberOfActivePayments(currentTable.CheckNumber)

    End Sub

    Private Sub btnClosePromo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePromo.Click
        ResetTimer()

        Me.pnlPaymentTypes.Controls.Clear()
        Dim oRow As DataRow
        Dim index As Integer
        Dim x As Integer = 8
        Dim y As Integer = 8
        '    ReDim Me.PromotionApplied(20) '(dsClosing.Tables("Promotion").Rows.Count - 1)

        For Each oRow In ds.Tables("Promotion").Rows
            btnPromo(index) = New KitchenButton(oRow("PromoName"), 120, 56, c17, c2)
            With btnPromo(index)
                '                .Size = New Size(112, 48)
                If oRow("PromoName").Length > 15 Then
                    .Font = New Font("Comic Sans MS", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                End If

                .Location = New Point(x, y)
                '               .Text = oRow("PromoName")
                .ID = oRow("PromoID")
                .ButtonIndex = index
                '              .BackColor = c6
                .ForeColor = c3

            End With

            AddHandler btnPromo(index).Click, AddressOf PromoSelect
            pnlPaymentTypes.Controls.Add(btnPromo(index))
            index += 1
            If index = 3 Or index = 6 Then
                x = x + 128
                y = 8
            ElseIf index = 9 Then
                '   max amount of promos
                Exit Sub
            Else
                y = y + 64
            End If
        Next

    End Sub

    Private Sub PromoSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlPaymentTypes.Click
        btnKitchen = New KitchenButton("ForTestOnly", 0, 0, c3, c2)

        If Not sender.GetType Is btnKitchen.GetType Then Exit Sub

        '     If typeProgram = "Online_Demo" Then
        '    DemoThisNotAvail
        '   Exit Sub
        '  End If

        Dim objButton As KitchenButton
        Dim oRow As DataRow
        objButton = CType(sender, KitchenButton)

        If PromotionApplied(objButton.ButtonIndex) = False Then
            oRow = ds.Tables("Promotion").Rows.Find(objButton.ID)
            SavingForOpenOrderID(oRow, objButton.ID, objButton.ButtonIndex)

        Else
            MsgBox("This Promotion has already been Applied")
        End If


    End Sub

    Private Sub SavingForOpenOrderID(ByRef promoRow As DataRow, ByVal promoID As Integer, ByVal btnIndex As Integer)
        Dim noOpenOrderID As Boolean = False
        Dim vRow As DataRowView

        For Each vRow In dvOrder 'dvClosingCheck
            If vRow("OpenOrderID") Is DBNull.Value Then
                noOpenOrderID = True
                Exit For
            End If
        Next

        '*****************
        '   not working when we need to get OpenOrderID's
        '   it is only saving some of the new promo info
        '   must be something to do with the dataviews

        If noOpenOrderID = True Then
            SaveOpenOrderData()
            Me.closeCheckTotals.grdCloseCheck.DataSource = Nothing
            DisposeDataViewsOrder()
            PopulateThisExperience(currentTable.ExperienceNumber, False)
            '444    CreateDataViewsOrder()
            '       dvOrder.RowFilter = "CheckNumber ='" & currentTable.CheckNumber & "'"
            ReinitializeCloseCheck(True)
        End If

        RunPromotionRoutine(promoRow, promoID, btnIndex)

    End Sub
    Private Sub RunPromotionRoutine(ByRef promoRow As DataRow, ByVal promoID As Integer, ByVal btnIndex As Integer)
        Dim vRow As DataRowView
        Dim compNextItem As Integer
        Dim possibleBuyAmount As Integer
        Dim totalBuyAmount As Integer
        Dim index As Integer
        Dim ni As SelectedItemDetail
        Dim compQuantity As Integer

        '****************************
        '   Promo BSGS
        Dim ffInfo As ForceFreeInfo
        Dim iPromo As ItemPromoInfo

        iPromo.PromoCode = promoID
        iPromo.PromoName = "  **  " & promoRow("PromoName")
        iPromo.empID = currentServer.EmployeeID

        Try
            '         sql.cn.Open()
            '           sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()

            If promoRow("BSGS") = True Then
                Dim buyAmount As Integer
                Dim getAmount As Integer

                dvBSGS = New DataView
                With dvBSGS
                    .Table = ds.Tables("BSGS")
                    .RowFilter = "PromoID = '" & promoID & "'"
                End With
                dvOrder.Sort = "Price ASC"     '("CustomerNumber, sii, sin")

                '   CheckMaxDollarAmount(promoRow)
                '  CheckMaxCheckAmount(promoRow)
                ' CheckMaxTableAmount(promoRow)

                For Each vRow In dvOrder

                    If vRow("ForceFreeID") = 0 Then 'And vRow("ItemID") > 0 Then 

                        '*** currently promo flags like this are only "F" or "D"
                        If dvBSGS(0)("BuyFD_flag") = "F" Then

                            If (vRow("FunctionFlag") = "F" Or vRow("FunctionFlag") = "O") And vRow("CategoryID") = dvBSGS(0)("BuyCategoryID") Then
                                buyAmount += 1
                                If buyAmount >= dvBSGS(0)("BuyCategoryAmount") Then
                                    compNextItem += 1
                                    buyAmount = 0
                                End If
                            End If
                        ElseIf dvBSGS(0)("BuyFD_flag") = "D" Then
                            If vRow("FunctionFlag") = "D" And vRow("CategoryID") = dvBSGS(0)("BuyDrinkCategoryID") Then
                                '    same as above
                                buyAmount += 1
                                If buyAmount >= dvBSGS(0)("BuyCategoryAmount") Then
                                    compNextItem += 1
                                    buyAmount = 0
                                End If
                            End If
                        End If

                    End If
                Next
                '   this will set the greatest amount to place as used buy item
                possibleBuyAmount = dvBSGS(0)("BuyCategoryAmount") * compNextItem

                For Each vRow In dvOrder

                    If vRow("ForceFreeID") = 0 Then     'And vRow("ItemID") > 0 Then

                        If compNextItem > 0 Then
                            If compNextItem < vRow("Quantity") Then
                                compQuantity = compNextItem
                            Else
                                compQuantity = vRow("Quantity")
                            End If

                            If dvBSGS(0)("GetFD_flag") = "F" Then
                                If (vRow("FunctionFlag") = "F" Or vRow("FunctionFlag") = "O") And vRow("CategoryID") = dvBSGS(0)("GetCategoryID") Then
                                    'here we are only coding the new row
                                    'leter we go back and code the old row

                                    vRow("ForceFreeID") = (promoID * -1)

                                    With iPromo
                                        .openOrderID = vRow("OpenOrderID")
                                        .taxID = vRow("TaxID")
                                        .sii = vRow("sii")
                                        .si2 = vRow("si2")
                                        'itemPrice is positive, .Price is negative
                                        'item price is orginal price
                                        'this price is is discount
                                        .Quantity = 0   'compQuantity
                                        .ItemPrice = vRow("Price") * compQuantity * dvBSGS(0)("GetQuantityDiscount")
                                        .Price = vRow("Price") * compQuantity * dvBSGS(0)("GetQuantityDiscount")
                                        .TaxPrice = vRow("TaxPrice") * compQuantity * dvBSGS(0)("GetQuantityDiscount")
                                        .SinTax = vRow("SinTax") * compQuantity * dvBSGS(0)("GetQuantityDiscount")

                                    End With


                                    CompThisItem(iPromo)
                                    getAmount += 1

                                End If
                            ElseIf dvBSGS(0)("GetFD_flag") = "D" Then
                                If vRow("FunctionFlag") = "D" And vRow("CategoryID") = dvBSGS(0)("GetDrinkCategoryID") Then

                                    vRow("ForceFreeID") = (promoID * -1)
                                    With iPromo
                                        .openOrderID = vRow("OpenOrderID")
                                        .taxID = vRow("TaxID")
                                        .sii = vRow("sii")
                                        .si2 = vRow("si2")
                                        'itemPrice is positive, .Price is negative
                                        'item price is orginal price
                                        'this price is is discount
                                        .Quantity = 0   'compQuantity
                                        .ItemPrice = vRow("Price") * compQuantity * dvBSGS(0)("GetQuantityDiscount")
                                        .Price = vRow("Price") * compQuantity * dvBSGS(0)("GetQuantityDiscount")
                                        .TaxPrice = vRow("TaxPrice") * compQuantity * dvBSGS(0)("GetQuantityDiscount")
                                        .SinTax = vRow("SinTax") * compQuantity * dvBSGS(0)("GetQuantityDiscount")
                                    End With

                                    CompThisItem(iPromo)
                                    getAmount += 1

                                End If
                            End If

                            If getAmount >= dvBSGS(0)("GetCategoryAmount") Then
                                compNextItem -= (1 * compQuantity)
                                totalBuyAmount = totalBuyAmount + (dvBSGS(0)("BuyCategoryAmount"))
                                getAmount = 0
                            End If
                        End If
                    End If
                Next

                '   ***    below marks buy items used for discount

                For Each ni In newItemCollection
                    GenerateOrderTables.PopulateDataRowForOpenOrder(ni)
                Next
                newItemCollection.Clear()

                If possibleBuyAmount < totalBuyAmount Then
                    totalBuyAmount = possibleBuyAmount
                End If

                For Each vRow In dvOrder
                    If totalBuyAmount > 0 Then
                        If vRow("ForceFreeID") = 0 Then     'And vRow("ItemID") > 0 Then
                            If dvBSGS(0)("BuyFD_flag") = "F" Then
                                If (vRow("FunctionFlag") = "F" Or vRow("FunctionFlag") = "O") And vRow("CategoryID") = dvBSGS(0)("BuyCategoryID") Then
                                    vRow("ForceFreeID") = (promoID * -1)
                                    totalBuyAmount -= (1 * vRow("Quantity"))
                                End If
                            ElseIf dvBSGS(0)("BuyFD_flag") = "D" Then
                                If vRow("FunctionFlag") = "D" And vRow("CategoryID") = dvBSGS(0)("BuyDrinkCategoryID") Then
                                    vRow("ForceFreeID") = (promoID * -1)
                                    totalBuyAmount -= (1 * vRow("Quantity"))
                                End If
                            End If
                        End If
                    End If
                Next

                dvOrder.Sort = "CustomerNumber, sii, si2, sin"



                '*********************************
                '   Combo

            ElseIf promoRow("Combo") = True Then

                '           sql.cn.Close()
                MsgBox("There are no Combo Promotions, call SpiderPOS as 404.869.4700.")
                Exit Sub

                Dim cpRow As DataRow
                Dim vComboRow As DataRowView
                index = 0
                Dim numberOfCombosPerCategory As Integer
                Dim numberOfCombosPerCheck As Integer

                Dim comboCategoryIndex As Integer

                dvCombo = New DataView
                With dvCombo
                    .Table = ds.Tables("Combo")
                    .RowFilter = "PromoID = '" & promoID & "'"
                End With
                cpRow = (ds.Tables("ComboDetail").Rows.Find(promoID))
                Me.comboPrice = cpRow("ComboPrice")

                '   determines # of items per category
                comboCategoryIndex = dvCombo.Count - 1
                Dim comboCount(comboCategoryIndex) As Integer
                Dim closingCount(comboCategoryIndex) As Integer

                '   ccr is comboCustomerRecord
                '   row 0 is empty to keep track by customer number (unless no cust number is input)
                '   ccr(1,2) = 3 means customer#`1 orderer 3 of the 3rd(zero indexed) category 


                '*******************************
                'customerNumber should not be zero
                '     Dim ccr(customerNumber, comboCategoryIndex) As Integer
                Dim ccr(1, comboCategoryIndex) As Integer



                For Each vComboRow In dvCombo

                    comboCount(index) = vComboRow("ComboCategoryMax")

                    '   determines # of items in check (belonging to combo category)
                    For Each vRow In dvOrder 'dvClosingCheck

                        If vRow("ForceFreeID") = 0 Then         'And vRow("ItemID") > 0 Then
                            If vComboRow("ComboFD_flag") = "F" Then
                                If (vRow("FunctionFlag") = "F" Or vRow("FunctionFlag") = "O") And vRow("CategoryID") = vComboRow("ComboCategoryID") Then
                                    ccr(vRow("CustomerNumber"), index) += 1
                                    closingCount(index) += 1
                                End If
                            ElseIf vComboRow("ComboFD_flag") = "D" Then
                                If vRow("FunctionFlag") = "D" And vRow("CategoryID") = vComboRow("ComboDrinkCategoryID") Then
                                    ccr(vRow("CustomerNumber"), index) += 1
                                    closingCount(index) += 1
                                End If
                            End If
                        End If
                        '                  MsgBox(ccr(vRow("CustomerNumber"), index))
                    Next
                    '            MsgBox(("CustomerNumber"))
                    index += 1
                Next

                '   determines the category w/ the lowest number of combo possiblities
                '   end result is numberOfCombosPerCheck
                index = 0
                For Each vComboRow In dvCombo
                    '              MsgBox(closingCount(index), , "Closing")
                    '            
                    If comboCount(index) = 0 Then Exit Sub 'means there are no items in promo
                    If closingCount(index) < comboCount(index) Then
                        numberOfCombosPerCheck = 0
                        MsgBox("You do not have all the items to make a Combo")
                        Exit Sub
                    Else
                        '   this number does not work yet(it currently rounds up)
                        '   it needs to be truncated for fractions(3/2 should = 1)
                        numberOfCombosPerCategory = closingCount(index) / comboCount(index)
                        If numberOfCombosPerCheck = 0 Then
                            numberOfCombosPerCheck = numberOfCombosPerCategory
                        ElseIf numberOfCombosPerCategory < numberOfCombosPerCheck Then
                            numberOfCombosPerCheck = numberOfCombosPerCategory
                        End If
                    End If

                    index += 1
                Next


                '   if customerBoolean is true then customer satifies all combo purchases
                Dim c As Integer        ' customer index
                Dim cmb As Integer      ' combo index
                Dim customerBoolean(customerNumber) As Integer 'Boolean
                Dim comboSatisfied(dvCombo.Count) As Boolean
                Dim customerFilled As Boolean
                Dim itemsCredited As Integer

                For c = 0 To customerNumber
                    For cmb = 0 To comboCategoryIndex
                        If comboCount(cmb) > 0 Then 'if zero: customerBoolean(c) stays the same
                            If closingCount(cmb) < comboCount(cmb) Then
                                '   this means cust did not order enough of combo items
                                customerBoolean(c) = 0
                                Exit For
                            Else
                                If customerBoolean(c) = 0 Then  '0 means just assigning value
                                    customerBoolean(c) = closingCount(cmb)
                                ElseIf closingCount(cmb) < customerBoolean(c) Then
                                    ' this assigns the lowest possible value that's not zero
                                    '   its not zero b/c zero would have been assigned above
                                    customerBoolean(c) = closingCount(cmb)
                                End If
                            End If
                        End If

                        ' if this passes one combo criteria is not met for this customer
                        '                    If ccr(c, cmb) < comboCount(cmb) Then
                        '                   Exit For
                        '                  End If
                        '                 '   if this passes we passed all the above for each combo category
                        '                If cmb = comboCategoryIndex Then
                        '               customerBoolean(c) += 1 'True
                        '              End If
                    Next
                Next

                c = 0


                '   at this point we will take numberOfCombosPerCheck and reduce by 1 until 0
                For Each vRow In dvOrder 'dvClosingCheck

                    If vRow("ForceFreeID") = 0 Then         'And vRow("ItemID") > 0 Then

                        cmb = 0
                        c = vRow("CustomerNumber")
                        If customerBoolean(c) > 0 Then '= True Then
                            For Each vComboRow In dvCombo
                                If ccr(c, cmb) > 0 Then 'comboSatisfied(cmb) = False Then
                                    If vComboRow("ComboFD_flag") = "F" Then
                                        If (vRow("FunctionFlag") = "F" Or vRow("FunctionFlag") = "O") And vRow("CategoryID") = vComboRow("ComboCategoryID") Then

                                            If numberOfCombosPerCheck < vRow("Quantity") Then

                                            End If


                                            '222
                                            ffInfo = New ForceFreeInfo
                                            ffInfo.DailyCode = currentTerminal.CurrentDailyCode
                                            ffInfo.ExpNum = currentTable.ExperienceNumber
                                            ffInfo.OpenOrderID = vRow("OpenOrderID")
                                            ffInfo.AuthID = currentServer.EmployeeID
                                            ffInfo.Price = 0 'vRow("Price")
                                            ffInfo.TaxID = vRow("TaxID")
                                            ffInfo.TaxPrice = 0 'vRow("TaxPrice")
                                            ffInfo.AmountDiscount = vRow("Price")
                                            ffInfo.TaxDicount = vRow("TaxPrice") + vRow("SinTax")

                                            ffInfo.PromoID = vComboRow("PromoID")
                                            ffInfo.PromoPrice = vRow("Price")

                                            '222       GenerateOrderTables.CreateNewOrderForceFree(ffInfo)
                                            '09.10
                                            If ffInfo.ffID > 0 Then
                                                vRow("ForceFreeID") = ffInfo.ffID
                                            Else
                                                vRow("ForceFreeID") = -2
                                                '   vRow("ForceFreeID") = vComboRow("PromoID")
                                            End If
                                            vRow("ForceFreeCode") = 0   'vRow("Price")
                                            '   vRow("ItemName") = vRow("ItemName") & " ** Combo  " & currentTable.PromoNumber 'vRow("CustomerNumber")
                                            vRow("TerminalName") = vRow("ItemName") & " ** Combo  " & currentTable.PromoNumber 'vRow("CustomerNumber")
                                            vRow("Price") = 0
                                            vRow("TaxPrice") = 0
                                            vRow("SinTax") = 0

                                            ccr(c, cmb) -= 1
                                            itemsCredited += 1
                                            customerFilled = DetermineNumberCredited(itemsCredited, dvCombo.Count, comboPrice, promoRow("PromoID"), promoRow("PromoName"), vRow("CustomerNumber"), vRow("sii"))
                                            If customerFilled = True Then
                                                customerBoolean(vRow("CustomerNumber")) -= 1
                                            End If
                                        End If

                                    ElseIf vComboRow("ComboFD_flag") = "D" Then
                                        If vRow("FunctionFlag") = "D" And vRow("CategoryID") = vComboRow("ComboDrinkCategoryID") Then
                                            ffInfo = New ForceFreeInfo
                                            ffInfo.DailyCode = currentTerminal.CurrentDailyCode
                                            ffInfo.ExpNum = currentTable.ExperienceNumber
                                            ffInfo.OpenOrderID = vRow("OpenOrderID")
                                            ffInfo.AuthID = currentServer.EmployeeID
                                            ffInfo.PromoID = vComboRow("PromoID")
                                            ffInfo.PromoPrice = vRow("Price")
                                            ffInfo.Price = 0 ' vRow("Price")
                                            ffInfo.TaxID = vRow("TaxID")
                                            ffInfo.TaxPrice = 0 'vRow("TaxPrice")
                                            ffInfo.AmountDiscount = vRow("Price")
                                            ffInfo.TaxDicount = vRow("TaxPrice") + vRow("SinTax")

                                            '222        GenerateOrderTables.CreateNewOrderForceFree(ffInfo)
                                            '09.10
                                            If ffInfo.ffID > 0 Then
                                                vRow("ForceFreeID") = ffInfo.ffID
                                            Else
                                                vRow("ForceFreeID") = -2
                                                '     vRow("ForceFreeID") = vComboRow("PromoID")
                                            End If
                                            vRow("ForceFreeCode") = 0   ' vRow("Price")
                                            '    vRow("ItemName") = vRow("ItemName") & " ** Combo  " & currentTable.PromoNumber 'vRow("CustomerNumber")
                                            vRow("TerminalName") = vRow("ItemName") & " ** Combo  " & currentTable.PromoNumber 'vRow("CustomerNumber")
                                            vRow("Price") = 0
                                            vRow("TaxPrice") = 0
                                            vRow("SinTax") = 0

                                            ccr(c, cmb) -= 1
                                            itemsCredited += 1
                                            customerFilled = DetermineNumberCredited(itemsCredited, dvCombo.Count, comboPrice, promoRow("PromoID"), promoRow("PromoName"), vRow("CustomerNumber"), vRow("sii"))
                                            If customerFilled = True Then
                                                customerBoolean(vRow("CustomerNumber")) -= 1
                                            End If
                                        End If
                                    End If
                                Else

                                End If
                                '          AddPromoToOrderTable(comboPrice, promoRow("PromoID"), promoRow("PromoName"), vRow("CustomerNumber"), vRow("sii"))
                                cmb += 1
                            Next

                            '   must add another routine if we want to circle back and offer 
                            '   combos to multiple customers (ie. cust 1 orders Item1, cust 2 ord item3)
                            '               If comboSatisfied(cmb) = True Then AddPromoToOrderTable(comboPrice, promoRow("PromoID"), promoRow("PromoName"), vRow("CustomerNumber"), vRow("sii"))
                        End If
                    End If


                Next




                '         MsgBox(numberOfCombosPerCheck, , "#Combos per check")
                '   ********** we need test here to make sure numberofcombospercheck is not > promotion limit

                '   now we must plug this number back into each category to see how many 
                '   items to subtract from check
                '   Example:
                '   If (category 1) needs 2 items for combo
                '      (category 2) needs 1 item for combo
                '   check has 6 of (category 1)       6 / 2 = 3
                '   check has 4 of (category 2)       4 / 1 = 4
                '   but we can only get 3 combos from this (the lowest number)
                '   so we look for 3 * 2 of (category 1)
                '   and            3 * 1 of (category 2)
                index = 0
                For Each vComboRow In dvCombo
                    comboCount(index) = numberOfCombosPerCheck * vComboRow("ComboCategoryMax")
                    index += 1
                Next


                '**************************************
                '   Coupon

            ElseIf promoRow("Coupon") = True Then
                Dim AtleastCatAmount As Integer
                Dim NumberOfCouponsToApply As Integer
                Dim maxToDiscount As Integer
                Dim maxDiscCount As Integer

                dvCoupon = New DataView
                With dvCoupon
                    .Table = ds.Tables("Coupon")
                    .RowFilter = "PromoID = '" & promoID & "'"
                End With
                '        dvOrder.Sort = "Price DESC"     '("CustomerNumber, sii, sin")


                '   this just determines how many coupons we can apply to check
                AtleastCatAmount = dvCoupon(0)("AtleastCategoryAmount")
                maxToDiscount = dvCoupon(0)("DiscountCategoryAmount")

                For Each vRow In dvOrder
                    If vRow("ForceFreeID") = (promoID * -1) Then
                        maxDiscCount += 1
                        If maxDiscCount = maxToDiscount Then
                            '              sql.cn.Close()
                            MsgBox("We have already applied this coupon to its maximum.")
                            Exit Sub
                        End If
                    End If
                Next

                If AtleastCatAmount > 0 Then
                    For Each vRow In dvOrder

                        If vRow("ForceFreeID") = 0 Then     'And vRow("ItemID") > 0 Then
                            If dvCoupon(0)("AtleastFD_flag") = "F" Then
                                If (vRow("FunctionFlag") = "F" Or vRow("FunctionFlag") = "O") Then
                                    If vRow("CategoryID") = dvCoupon(0)("AtleastCategoryID") Then
                                        AtleastCatAmount -= (1 * vRow("Quantity"))
                                        If AtleastCatAmount <= 0 Then   'this means we satisfied min
                                            NumberOfCouponsToApply += dvCoupon(0)("DiscountCategoryAmount")
                                            Exit For
                                        End If
                                    End If
                                End If
                            ElseIf dvCoupon(0)("AtleastFD_flag") = "D" Then
                                If vRow("FunctionFlag") = "D" Then
                                    If vRow("CategoryID") = dvCoupon(0)("AtleastDrinkCategoryID") Then
                                        AtleastCatAmount -= (1 * vRow("Quantity"))
                                        If AtleastCatAmount <= 0 Then   'this means we satisfied min
                                            NumberOfCouponsToApply += dvCoupon(0)("DiscountCategoryAmount")
                                            Exit For
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                Else
                    '   this means there was no minumum req.
                    NumberOfCouponsToApply = dvCoupon(0)("DiscountCategoryAmount")     'this is max allowed
                End If

                '      Dim discountingSIN As Integer
                '     Dim adjustingRow As Integer
                Dim newPrice As Decimal
                Dim newTax As Decimal
                Dim newSinTax As Decimal
                Dim amountSavedCoupon As Decimal
                Dim taxSavedCoupon As Decimal

                '   this will deduct the most expensive first
                index = 0
                If NumberOfCouponsToApply > 0 Then
                    For Each vRow In dvOrder

                        If vRow("ForceFreeID") = 0 Then     'And vRow("ItemID") > 0 Then
                            If dvCoupon(0)("AtleastFD_flag") = "F" Then

                                If (vRow("FunctionFlag") = "F" Or vRow("FunctionFlag") = "O") And vRow("CategoryID") = dvCoupon(0)("DiscountCategoryID") Then
                                    If dvCoupon(0)("CouponDollarFlag") = True Then
                                        amountSavedCoupon = dvCoupon(0)("CouponDollarAmount")
                                    ElseIf dvCoupon(0)("CouponPercentFlag") = True Then
                                        amountSavedCoupon = (vRow("Price") * dvCoupon(0)("CouponPercentAmount"))
                                    End If

                                    If vRow("ItemPrice") < amountSavedCoupon Then
                                        'we can't save more than the original amount of one item
                                        amountSavedCoupon = vRow("ItemPrice")
                                    End If
                                    If vRow("Quantity") > 1 Then
                                        If NumberOfCouponsToApply < vRow("Quantity") Then
                                            compQuantity = NumberOfCouponsToApply
                                        Else
                                            compQuantity = vRow("Quantity")
                                        End If
                                    Else
                                        compQuantity = vRow("Quantity")
                                    End If

                                    amountSavedCoupon = amountSavedCoupon * compQuantity

                                    vRow("ForceFreeID") = (promoID * -1)
                                    With iPromo
                                        .openOrderID = vRow("OpenOrderID")
                                        .taxID = vRow("TaxID")
                                        .sii = vRow("sii")
                                        .si2 = vRow("si2")
                                        .Quantity = 0   'compQuantity
                                        .ItemPrice = vRow("Price")
                                        .Price = amountSavedCoupon
                                        .TaxPrice = 0
                                        .SinTax = 0
                                    End With

                                    CompThisItem(iPromo)
                                    NumberOfCouponsToApply -= (1 * compQuantity)
                                    If NumberOfCouponsToApply <= 0 Then Exit For

                                End If

                            ElseIf dvCoupon(0)("AtleastFD_flag") = "D" Then
                                If vRow("FunctionFlag") = "D" And vRow("CategoryID") = dvCoupon(0)("DiscountDrinkCategoryID") Then
                                    If dvCoupon(0)("CouponDollarFlag") = True Then
                                        amountSavedCoupon = dvCoupon(0)("CouponDollarAmount")
                                    ElseIf dvCoupon(0)("CouponPercentFlag") = True Then
                                        amountSavedCoupon = (vRow("Price") * dvCoupon(0)("CouponPercentAmount"))
                                    End If

                                    If vRow("ItemPrice") < amountSavedCoupon Then
                                        'we can't save more than the original amount of one item
                                        amountSavedCoupon = vRow("ItemPrice")
                                    End If
                                    If vRow("Quantity") > 1 Then
                                        If NumberOfCouponsToApply < vRow("Quantity") Then
                                            compQuantity = NumberOfCouponsToApply
                                        Else
                                            compQuantity = vRow("Quantity")
                                        End If
                                    Else
                                        compQuantity = vRow("Quantity")
                                    End If

                                    amountSavedCoupon = amountSavedCoupon * compQuantity

                                    vRow("ForceFreeID") = (promoID * -1)
                                    With iPromo
                                        .openOrderID = vRow("OpenOrderID")
                                        .taxID = vRow("TaxID")
                                        .sii = vRow("sii")
                                        .si2 = vRow("si2")
                                        .Quantity = 0   'compQuantity
                                        .ItemPrice = vRow("Price")
                                        .Price = amountSavedCoupon
                                        .TaxPrice = 0
                                        .SinTax = 0
                                    End With

                                    CompThisItem(iPromo)
                                    NumberOfCouponsToApply -= (1 * compQuantity)
                                    If NumberOfCouponsToApply <= 0 Then Exit For
                                End If
                            End If
                        End If
                    Next
                End If
                For Each ni In newItemCollection
                    GenerateOrderTables.PopulateDataRowForOpenOrder(ni)
                Next
                newItemCollection.Clear()
                '         dvOrder.Sort = "CustomerNumber, sii, si2, sin"

            End If


            '         sql.cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            '      CloseConnection()
        End Try


        PromotionApplied(btnIndex) = True

    End Sub

    Private Function DetermineNumberCredited(ByRef itemsCredited As Integer, ByVal numberOfCombosItems As Integer, ByVal ComboPrice As Decimal, ByVal PromoID As Integer, ByVal PromoName As String, ByVal customerNumber As Integer, ByVal sii As Integer) As Boolean
        If itemsCredited >= numberOfCombosItems Then
            AddPromoToOrderTable(ComboPrice, PromoID, PromoName, customerNumber, sii)
            itemsCredited = 0
            currentTable.PromoNumber += 1
            Return True
        End If
        Return False
    End Function

    Private Sub AddPromoToOrderTable(ByVal ComboPrice As Decimal, ByVal PromoID As Integer, ByVal PromoName As String, ByVal customerNumber As Integer, ByVal sii As Integer)
        Dim nRow As DataRow = dsOrder.Tables("OpenOrders").NewRow

        nRow("TableNumber") = currentTable.TableNumber
        nRow("EmployeeID") = currentTable.EmployeeID
        nRow("CheckNumber") = currentTable.CheckNumber
        nRow("CustomerNumber") = customerNumber
        nRow("sin") = currentTable.SIN()
        nRow("sii") = sii
        nRow("ItemID") = PromoID
        nRow("ItemName") = PromoName & "  " & currentTable.PromoNumber
        nRow("TerminalName") = PromoName & "  " & currentTable.PromoNumber
        nRow("Price") = ComboPrice
        nRow("TaxID") = 1       '******** this will be determined
        nRow("CategoryID") = 0
        nRow("FunctionID") = 9      'hardcoded for Promotions

        nRow("ItemStatus") = 4

        dsOrder.Tables("OpenOrders").Rows.Add(nRow)
        currentTable.SIN += 1



    End Sub

    Private Sub RunBSGSPromotion()


    End Sub


    Private Sub CheckMaxDollarAmount(ByRef promoRow As DataRow)
        If maxDollar < promoRow("MaxAmount") Then
            Return

        Else

            'quit promo

        End If
    End Sub

    Private Sub CheckMaxCheckAmount(ByRef promoRow As DataRow)
        If maxCheck < promoRow("MaxCheck") Then
            Return
        Else

        End If
    End Sub

    Private Sub CheckMaxTableAmount(ByRef promoRow As DataRow)
        If maxTable < promoRow("MaxTable") Then
            Return
        Else

        End If
    End Sub



    Private Sub TaxingPromoAmount()

    End Sub



    Private Sub TaxingFoodCost()

    End Sub



    Private Sub MakeGuestPayTax()

    End Sub


    Private Sub CheckManagerLevel()

    End Sub


    Private Sub CalculateAddPromoAmountInAutoTip()

    End Sub


    Private Sub btnCloseExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseExit.Click

        ClosingAndReleaseRoutine(False)

        '    RaiseEvent CloseExiting(False)

    End Sub

    Private Sub btnCloseSplit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseSplit.Click
        '   should we update Payments and Credits
        ResetTimer()

        '      If currentTerminal.TermMethod = "Quick" Then
        'Exit Sub
        '    End If

        Dim oRow As DataRow
        Dim vRow As DataRowView
        Dim numberCheckCount As Integer

        '   ***
        '   we need to move this for individual moves in split checks
        '       If dsOrder.Tables("PaymentsAndCredits").Rows.Count > 0 Then
        '       For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
        '       If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
        '           If oRow("Applied") = True Then
        '      MsgBox("You can not split a Check after a Payment has been applied.")
        '      Exit Sub
        '          End If
        '      End If
        '     Next
        '    End If

        If currentTable.NumberOfChecks = 1 Then
            ' we are spliting every check with multiple customers by customer
            '      currentTable.NumberOfChecks = currentTable.NumberOfCustomers
            For Each oRow In dsOrder.Tables("OpenOrders").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    oRow("CheckNumber") = oRow("CustomerNumber")
                    If oRow("ItemID") = 0 And oRow("si2") = 0 Then
                        'counts customer panels
                        numberCheckCount += 1
                    End If
                End If

            Next
            If numberCheckCount > 0 Then
                currentTable.NumberOfChecks = numberCheckCount
            Else
                'do nothing, this is when we have one customer and no customer panel
            End If
            If numberCheckCount > currentTable.NumberOfCustomers Then
                currentTable.NumberOfCustomers = numberCheckCount
            End If

            If numberCheckCount <= 1 Then    'currentTable.NumberOfCustomers = 1 Then

                RaiseEvent CloseGotoSplitting(sender, e)
                Exit Sub
            End If

            If currentTerminal.TermMethod = "Quick" Or currentTable.TicketNumber > 0 Then
                For Each oRow In dsOrder.Tables("QuickTickets").Rows
                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        oRow("NumberOfChecks") = numberCheckCount   'oRow("NumberOfCustomers")
                        oRow("NumberOfCustomers") = currentTable.NumberOfCustomers
                    End If
                Next
            Else
                If currentTable.IsTabNotTable = True Then
                    For Each vRow In dvAvailTabs    'dsOrder.Tables("AvailTabs").Rows
                        If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                            vRow("NumberOfChecks") = numberCheckCount   'oRow("NumberOfCustomers")
                            vRow("NumberOfCustomers") = currentTable.NumberOfCustomers
                        End If
                    Next
                Else
                    For Each vRow In dvAvailTables  'dsOrder.Tables("AvailTables").Rows
                        If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                            vRow("NumberOfChecks") = numberCheckCount       'oRow("NumberOfCustomers")
                            vRow("NumberOfCustomers") = currentTable.NumberOfCustomers
                        End If
                    Next
                End If
            End If

            'all this updates screen for Check number 1
            ReinitializeCloseCheck(True)
            '    singleSplit = True
            RaiseEvent SplitSingleCheck()

        Else
            RaiseEvent CloseGotoSplitting(sender, e)

        End If

    End Sub

    Private Sub btnClosePayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePayment.Click
        ResetTimer()

        Me.pnlPaymentTypes.Controls.Clear()

        AddGeneratedControlsToPaymentPanel()

    End Sub


    Private Sub btnCloseGift_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseGift.Click
        ResetTimer()

        Dim vRow As DataRowView
        Dim newPayment As New DataSet_Builder.Payment
        Dim index As Integer = 1

        newPayment.Purchase = Format(0, "##,##0.00")
        newPayment.PaymentTypeID = -98 'DetermineCreditCardID("Cash")
        newPayment.PaymentTypeName = "Gift Certificate"

        '     For Each vRow In dvUnAppliedPaymentsAndCredits  '  dvClosingCheckPayments       'dsOrder.Tables("PaymentsAndCredits").Rows
        '      If vRow("PaymentFlag") = "Cash" Then       'And vRow("Applied") = 0 Then
        '   vRow("PaymentAmount") += 0
        '    ShowRemainingBalance()
        '     GetNumberOfActivePayments(currentTable.CheckNumber)
        '      paymentRowIndex = index
        '       Return
        '      End If
        '      index += 1
        '      Next

        CreateNewPaymentEntry(newPayment, False)
        ShowRemainingBalance()
    End Sub

    Private Sub AddAutoCash(ByVal amount As Decimal)
        ResetTimer()

        Dim vRow As DataRowView
        Dim newPayment As New DataSet_Builder.Payment
        Dim index As Integer = 1

        newPayment.Purchase = Format(amount, "##,##0.00")
        newPayment.PaymentTypeID = DetermineCreditCardID("Cash")
        newPayment.PaymentTypeName = "Cash"

        For Each vRow In dvUnAppliedPaymentsAndCredits  '  dvClosingCheckPayments       'dsOrder.Tables("PaymentsAndCredits").Rows
            If vRow("PaymentFlag") = "Cash" Then       'And vRow("Applied") = 0 Then
                vRow("PaymentAmount") += amount
                ShowRemainingBalance()
                GetNumberOfActivePayments(currentTable.CheckNumber)
                paymentRowIndex = index
                Return
            End If
            index += 1
        Next

        CreateNewPaymentEntry(newPayment, False)
        ShowRemainingBalance()

    End Sub

    Private Sub CreateNewPaymentEntry(ByRef newPayment As DataSet_Builder.Payment, ByVal doApply As Boolean)

        If newPayment.PaymentTypeID = -97 Then '"MPS Gift" Then
            ' issuing Gift Card
            'remember this was negative, so we need to reverse to compare w/ attemptingPayment
            _checkGiftIssuingAmount -= newPayment.Purchase
            _lastPurchaseIssueAmount = _checkGiftIssuingAmount
        End If

        GenerateOrderTables.AddPaymentToDataRow(newPayment, doApply, currentTable.ExperienceNumber, currentServer.EmployeeID, currentTable.CheckNumber, closeCheckTotals.AutoGratuity)

        '     numActivePaymentsByCheck += 1
        '    paymentRowIndex = dvUnAppliedPaymentsAndCredits.Count

        GetNumberOfActivePayments(currentTable.CheckNumber)

    End Sub

    Private Sub CreateNewPaymentPanel(ByRef vRow As DataRowView, ByVal PnlNo As Integer, ByVal position As Integer, ByVal giftBalance As Decimal)

        Dim truncAcctNum As String

        If Not vRow("AccountNumber") Is DBNull.Value Then
            If Not vRow("AuthCode") Is DBNull.Value Then
                'already authorized
                If Not vRow("AccountNumber").Substring(0, 4) = "xxxx" And Not vRow("AccountNumber") = "Manual" Then

                    truncAcctNum = TruncateAccountNumber(vRow("AccountNumber"))
                Else
                    truncAcctNum = (vRow("AccountNumber"))
                End If
            Else
                truncAcctNum = (vRow("AccountNumber"))
            End If
        End If

        paymentPanel(PnlNo) = New DataSet_Builder.Payment_UC("Close", vRow, Nothing, PnlNo, Nothing, truncAcctNum, giftBalance)

        With paymentPanel(PnlNo)

            .Location = New Point(0, .Height * (position))
            .BackColor = Color.DarkGray
            AddHandler paymentPanel(PnlNo).ActivePanel, AddressOf PaymentUserControl_Click
            AddHandler paymentPanel(PnlNo).SeeHistoryPanel, AddressOf PaymentUserControl_History
            Me.pnlClosePayments.Controls.Add(paymentPanel(PnlNo))
        End With

    End Sub

    Private Sub NewCardRead(ByRef newPayment As DataSet_Builder.Payment) Handles readAuth222.CardReadSuccessful
        ResetTimer()

        '444   GenerateOrderTables.CreateTabAcctPlaceInExperience(newPayment)
        '444    AddPaymentToCollection(newPayment)
        ProcessCreditRead(newPayment)

    End Sub

    Friend Sub ProcessCreditRead(ByRef newPayment As DataSet_Builder.Payment)

        If newPayment.AuthCode > 0 Then Exit Sub
        Dim authStatus As String

        Dim oRow As DataRow

        If Not newPayment.PaymentTypeID = -97 Then 'authStatus = "Account Not Issued" 
            If creditAmountAdjusted = True Or doNotAutoCreditCards = True Then
                newPayment.Purchase = 0
            Else
                newPayment.Purchase = DetermineAutomaticCreditCharge()
            End If
            If newPayment.PaymentFlag = "Gift" Then
                If newPayment.Balance < newPayment.Purchase Then
                    newPayment.Purchase = newPayment.Balance
                    MsgBox("Balance remaining on card before purchase: " & newPayment.Balance)
                    'otherwise defaults to purchase
                End If

            End If
            ApplyAutomaticCreditCharge(newPayment.Purchase)

        End If

        CreateNewPaymentEntry(newPayment, False)
        ShowRemainingBalance()

    End Sub

    Friend Sub ProcessCreditRead_MWE(ByVal vRow As DataRowView) 'ByVal _secureNewPayment_MWE As ReadCredit_MWE2.Payment_MWE)

        If Not vRow("AuthCode") Is DBNull.Value Then
            If vRow("AuthCode") > 0 Then Exit Sub
        End If

        Dim authStatus As String

        Dim oRow As DataRow

        If Not vRow("PaymentTypeID") = -97 Then 'authStatus = "Account Not Issued" 
            If creditAmountAdjusted = True Or doNotAutoCreditCards = True Then
                vRow("Purchase") = 0
            Else
                vRow("Purchase") = DetermineAutomaticCreditCharge()
            End If
            If vRow("PaymentFlag") = "Gift" Then
                If vRow("Balance") < vRow("Purchase") Then
                    vRow("Purchase") = vRow("Balance") 'balance is what is on the Gift Card
                    MsgBox("Balance remaining on card before purchase: " & vRow("Balance"))
                    'otherwise defaults to purchase
                End If

            End If
            ApplyAutomaticCreditCharge_MWE(vRow("Purchase"))

        End If

        CreateNewPaymentEntry_MWE(vRow) ', False)
        ShowRemainingBalance()

    End Sub

    Private Sub CreateNewPaymentEntry_MWE(ByVal vRow As DataRowView) 'ByRef newPayment As DataSet_Builder.Payment, ByVal doApply As Boolean)

        If vRow("PaymentTypeID") = -97 Then '"MPS Gift" Then
            ' issuing Gift Card
            'remember this was negative, so we need to reverse to compare w/ attemptingPayment
            _checkGiftIssuingAmount -= vRow("Purchase")
            _lastPurchaseIssueAmount = _checkGiftIssuingAmount
        End If

        '?????
        '444   GenerateOrderTables.AddPaymentToDataRow(newPayment, doApply, currentTable.ExperienceNumber, currentServer.EmployeeID, currentTable.CheckNumber, closeCheckTotals.AutoGratuity)

        'old     numActivePaymentsByCheck += 1
        'old    paymentRowIndex = dvUnAppliedPaymentsAndCredits.Count

        GetNumberOfActivePayments(currentTable.CheckNumber)

    End Sub

    Private Sub DisplayAnyStoredPayments222()

        Dim newPayment As New Payment

        For Each newPayment In tabcc
            If newPayment.experienceNumber = currentTable.ExperienceNumber Then
                CreateNewPaymentEntry(newPayment, False)
                '                ProcessCreditRead(storedPayment)
            End If
        Next

    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click

        Dim testPay As DataSet_Builder.Payment
        If Not Me.paymentRowIndex > 0 Then Exit Sub
        Dim UnAuthPanel As Boolean
        Dim UnAuthIndex As Integer

        If paymentRowIndex > dvClosingCheckPayments.Count Then
            ' we will use:   dvUnAppliedPaymentsAndCredits_MWE 
            UnAuthPanel = True
            UnAuthIndex = paymentRowIndex - dvClosingCheckPayments.Count - 1
            '          PaymentEnterStep2_UnAuth(UnAuthIndex)
            '    Else
            '         PaymentEnterStep2_AlreadyAuth()
        End If

        Try
            If Not paymentPanel(paymentRowIndex).AuthCode = Nothing Then Exit Sub
            'above is nothing (not dbnull) b/c it is not from dataset
            '   If paymentPanel(paymentrowindex).  cash???
        Catch ex As Exception
            Exit Sub
        End Try

        For Each testPay In tabcc
            If testPay.experienceNumber = currentTable.ExperienceNumber Then
                If testPay.AccountNumber = paymentPanel(paymentRowIndex).AcctNumber() Then
                    ' we have the same payment assc with this account, 
                    '   REMOVE, put in most current info
                    tabcc.Remove(testPay)
                    Exit For
                End If
            End If
        Next

        If UnAuthPanel = True Then
            dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex).Delete()
        Else
            '444     dvUnAppliedPaymentsAndCredits(paymentRowIndex - 1).Delete()
            dvClosingCheckPayments(paymentRowIndex - 1).Delete()
        End If

        paymentPanel(paymentRowIndex).Dispose()
        paymentRowIndex = dvClosingCheckPayments.Count + dvUnAppliedPaymentsAndCredits_MWE.Count '444dvUnAppliedPaymentsAndCredits.Count
        '   startPaymentIndex = 1
        '     If paymentRowIndex > 0 Then
        '     paymentRowIndex -= 1
        '    End If

        If (startPaymentIndex) > paymentRowIndex Then
            startPaymentIndex = 1
        End If
        GetNumberOfActivePayments(currentTable.CheckNumber)

    End Sub

    Private Sub PreAuthPaywarePC(ByRef vRow As DataRowView)

        PaywarePCCharge = New SIM.Charge

        GenerateOrderTables.ReadyToProcessPaywarePC(PaywarePCCharge)

        With PaywarePCCharge
            '       .PaymentEngine = SIM.Charge.PaymentSoftware.RiTA_PAYware
            '      .ClientID = companyInfo.ClientID '"100010001"
            '     .UserID = companyInfo.UserID '"Admin"
            '    .UserPW = companyInfo.UserPW '"PCBeta68$"
            '       .IPAddress = companyInfo.IPAddress '"127.0.0.1"
            '      .Port = "4532"
            '     .CommMethod = SIM.Charge.CommType.IP

            If authPayment.Track2 = Nothing Then
                .Card = authPayment.AcctNum
                .ExpDate = authPayment.ExpDate
            Else
                .Track = authPayment.Track2  ' "4012000033330026=12121011000001234567"
            End If
            .Amount = authPayment.paymentAmount  '"1.00"
            .Ticket = authPayment.TicketNumber  '"123456"
            .Action = SIM.Charge.Command.Credit_Sale

            paywareAuthInfo = New Info2_UC("Authorizing...")
            paywareAuthInfo.Location = New Point(300, 250)
            Me.Controls.Add(paywareAuthInfo)
            paywareAuthInfo.BringToFront()
            '     paywareAuthInfo.Update()
            paywareAuthInfo.Refresh()

            If .Process Then
                Try
                    If .GetResult = "CAPTURED" Or .GetResultCode = "4" Then
                        paywareAuthInfo.Dispose()
                        'above is the same
                        vRow("AuthCode") = .GetAuthCode
                        vRow("AcqRefData") = .GetReference
                        vRow("RefNum") = .GetTroutD
                        vRow("Description") = .GetResponseText

                    Else ' If .GetResult = "DECLINED" Or .GetResultCode = "6" Then
                        paywareAuthInfo.Dispose()
                        MsgBox("CARD '" & vRow("AccountNumber") & "' " & .GetResult & ": " & .GetResponseText)
                        'MsgBox("CARD '" & vRow("AccountNumber") & "' DECLINED: " & .GetResponseText)
                    End If

                Catch ex As Exception
                    paywareAuthInfo.Dispose()

                End Try

            Else
                paywareAuthInfo.Dispose()
                MsgBox("" & .ErrorCode & ": " & .ErrorDescription)
            End If
        End With

    End Sub

    Private Sub btnAuthActive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthActive.Click


        Dim oRow As DataRow
        Dim vrow As DataRowView
        Dim cRow As DataRowView
        Dim preAuthReady As String
        Dim reducePaymentAmount As Decimal
        Dim paymentWentThrough As String
        '   Dim ccCashClose As CashClose_UC
        Dim numItems As Integer
        '     Dim prt As New PrintHelper
        Dim displayCashClose_UC As Boolean

        If paymentRowIndex < 1 Then Exit Sub
        Dim UnAuthPanel As Boolean
        Dim UnAuthIndex As Integer
        'reset
        prt.closeDetail.cashTendered = False
        prt.closeDetail.cashAppliedPrevious = 0

        If paymentRowIndex > dvClosingCheckPayments.Count Then
            ' we will use:   dvUnAppliedPaymentsAndCredits_MWE 
            UnAuthPanel = True
            UnAuthIndex = paymentRowIndex - dvClosingCheckPayments.Count - 1
        End If

        If UnAuthPanel = True Then
            vrow = dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)
        Else
            vrow = dvClosingCheckPayments(paymentRowIndex - 1) '444dvUnAppliedPaymentsAndCredits(paymentRowIndex - 1)
        End If

        reducePaymentAmount = vrow("PaymentAmount")
        '444      If TestPayments(vrow("PaymentAmount")) = True Then
        '**** we can do above only with credit cards, does not make sence with cash

        If vrow("PaymentFlag") = "Issue" Then
            'this is for both issue and return (whixh is adding money)
            MsgBox("You must select AUTH ALL button because you are Issuing Gift Card")
            Exit Sub
        End If

        If vrow("PaymentFlag") = "Gift" Then

            paymentWentThrough = GenerateOrderTables.GiftCardTransaction(vrow, Nothing, "Sale")
            If paymentWentThrough = "MPS Gift Card" Then
                'this means trying to use MPS Gift Card w/o being Mercury Merchant
                MsgBox(paymentWentThrough)
            Else

            End If
            '444               GiftCardTransaction(vrow, "NoNSFSale")
        Else
            If companyInfo.usingOutsideCreditProcessor = False Then
                If vrow("AuthCode") Is DBNull.Value And vrow("PaymentFlag") = "cc" Then
                    If companyInfo.processor = "MerchantWare" Then

                        PreAuthMerchantWare_Active(vrow)
                        Exit Sub
                    Else
                        If TestAccountNumber(vrow) = True Then
                            preAuthReady = TestPreAuthSwiped(vrow, True)
                            If preAuthReady = "Swiped" Then
                                paymentWentThrough = PreAuth(Nothing, vrow, True)
                                '     PreAuth(vrow)
                            ElseIf preAuthReady = "Keyed" Then
                                paymentWentThrough = PreAuth(Nothing, vrow, True)
                                '    PreAuth(vrow)
                            End If
                        Else
                            Exit Sub
                        End If
                    End If
                End If
            End If
        End If

        '    If paymentWentThrough = False Then Exit Sub
        'this is to determine and previous cash applied on check
        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("CheckNumber") = currentTable.CheckNumber Then
                    If oRow("Applied") = True Then
                        'applied = true
                        If oRow("PaymentFlag") = "Cash" Then
                            prt.closeDetail.cashAppliedPrevious += oRow("PaymentAmount")
                        End If
                    End If
                End If
            End If
        Next

        If vrow("PaymentAmount") <> 0 Then
            If vrow("Applied") = False Then
                '                               has Auth therefore is approved 
                If vrow("PaymentFlag") = "Cash" Then       '1 is cash
                    '          If vrow("PaymentTypeID") = -98 Then   'Gift Certificate
                    '       If reducePaymentAmount > RemainingBalance Then
                    '      MsgBox("Gift Certificate can not be more than Balance Due")
                    '      Exit Sub
                    'End If
                    'End If
                    For Each cRow In dvOrder
                        If cRow("sin") = cRow("sii") And Not cRow("ItemID") = 0 Then
                            numItems += 1
                        End If
                    Next
                    '      PrintCreditCardReceipt(vrow)

                    If companyInfo.autoPrint = True Then
                        RunClosingPrint()
                        prt.closeDetail.isCashTendered = True
                        prt.closeDetail.cashTendered = reducePaymentAmount
                        prt.closeDetail.chkChangeDue = reducePaymentAmount - RemainingBalance
                        prt.StartPrintCheckReceipt()
                    Else
                        prt.PrintOpenCashDrawer()
                    End If

                    ccDisplay = New CashClose_UC(numItems, currentTable.TruncatedExpNum, RemainingBalance, reducePaymentAmount)
                    ccDisplay.Location = New Point((Me.Width - ccDisplay.Width) / 2, (Me.Height - ccDisplay.Height) / 2)
                    If reducePaymentAmount > RemainingBalance Then
                        ' means we are giving change
                        vrow("PaymentAmount") = RemainingBalance
                        RemainingBalance = 0
                    Else
                        RemainingBalance -= reducePaymentAmount
                    End If

                    paymentRowIndex -= 1
                    vrow("AuthCode") = "Cash"
                    vrow("Applied") = True
                    If Not RemainingBalance > 0 Then
                        displayCashClose_UC = True
                    End If

                ElseIf vrow("PaymentFlag") = "cc" And Not vrow("AuthCode") Is DBNull.Value Then
                    '                               has Auth therefore is approved 
                    vrow("Track2") = DBNull.Value
                    PrintCreditCardReceipt(Nothing, vrow, True)
                    vrow("Applied") = True
                    RemainingBalance -= reducePaymentAmount
                    paymentRowIndex -= 1
                    RemovePaymentFromCollection(vrow("AccountNumber"))

                ElseIf vrow("PaymentFlag") = "Gift" And Not vrow("AuthCode") Is DBNull.Value Then
                    '                               has Auth therefore is approved 
                    vrow("Track2") = DBNull.Value
                    PrintCreditCardReceipt(Nothing, vrow, True)
                    vrow("Applied") = True
                    RemainingBalance -= reducePaymentAmount
                    paymentRowIndex -= 1
                    RemovePaymentFromCollection(vrow("AccountNumber"))

                ElseIf vrow("PaymentFlag") = "outside" Then ' And companyInfo.usingOutsideCreditProcessor = True Then
                    vrow("PaymentTypeID") = 9
                    vrow("AuthCode") = 9
                    vrow("Applied") = True
                    RemainingBalance -= reducePaymentAmount
                    paymentRowIndex -= 1
                    RunClosingPrint()
                    prt.StartPrintCheckReceipt() 'new

                ElseIf vrow("PaymentFlag") = "Gift Cert" Then
                    '                               only approved
                    '    vrow("Track2") = DBNull.Value
                    '    PrintCreditCardReceipt(Nothing, vrow, True)
                    If vrow("PaymentAmount") > RemainingBalance Then
                        MsgBox("Gift Certificate can not be more than Balance Due")
                    Else
                        vrow("Applied") = True
                        RemainingBalance -= reducePaymentAmount
                        paymentRowIndex -= 1

                    End If

                End If

            End If

        End If

        Me.closeCheckTotals.AttachTotalsToTotalView(currentTable.CheckNumber)
        GetNumberOfActivePayments(currentTable.CheckNumber)
        If displayCashClose_UC = True Then
            Me.Controls.Add(ccDisplay)
            ccDisplay.BringToFront()
        End If

    End Sub

    Private Sub btnAuthAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthAll.Click

        Dim oRow As DataRow
        Dim vrow As DataRowView
        Dim cRow As DataRowView
        Dim preAuthReady As String
        Dim unappliedPayments As Decimal
        '     Dim ccDisplay As CashClose_UC
        Dim numItems As Integer
        '    Dim prt As New PrintHelper
        Dim printFinalReceipt As Boolean
        Dim displayCashClose_UC As Boolean
        Dim attemptingPayment As Decimal
        Dim attemptingCash As Decimal
        Dim giftIssuingAmount As Decimal
        Dim giftRemainingIssue As Decimal
        Dim issueGiftCards As Boolean = False

        Dim UnAuthPanel As Boolean
        Dim UnAuthIndex As Integer

        'reset
        prt.closeDetail.cashTendered = False
        prt.closeDetail.cashAppliedPrevious = 0

        If paymentRowIndex > dvClosingCheckPayments.Count Then
            ' we will use:   dvUnAppliedPaymentsAndCredits_MWE 
            UnAuthPanel = True
            UnAuthIndex = paymentRowIndex - dvClosingCheckPayments.Count - 1
            'change     PaymentEnterStep2_UnAuth(UnAuthIndex)
        Else
            'change      PaymentEnterStep2_AlreadyAuth()
        End If

        If _checkGiftIssuingAmount > 0 Then 'we reversed this before to make positive
            For Each vrow In dvUnAppliedPaymentsAndCredits
                If vrow("PaymentTypeID") > -1 Then
                    attemptingPayment += vrow("PaymentAmount")
                    If vrow("PaymentTypeID") = 1 Then
                        attemptingCash += vrow("PaymentAmount")
                    End If
                ElseIf vrow("PaymentTypeID") = -97 Then
                    giftIssuingAmount -= vrow("PaymentAmount")
                End If
            Next
            If attemptingPayment < giftIssuingAmount Then
                MsgBox("There is not enouph payments to cover Issue of Gift Card")
                Exit Sub
            End If
            giftRemainingIssue = giftIssuingAmount
        End If

        If companyInfo.usingOutsideCreditProcessor = False Then
            For Each vrow In dvClosingCheckPayments
                If vrow("PaymentFlag") = "Gift" Then
                    If vrow("AuthCode") Is DBNull.Value Then 'vrow("Applied") = False And 
                        preAuthReady = GenerateOrderTables.GiftCardTransaction(vrow, Nothing, "Sale")
                        If preAuthReady = "MPS Gift Card" Then
                            MsgBox(preAuthReady)
                        Else

                        End If
                        '444               GiftCardTransaction(vrow, "NoNSFSale")
                    End If

                Else
                    '444 For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
                    '444If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If vrow("CheckNumber") = currentTable.CheckNumber Then
                        If vrow("Applied") = False Then
                            If vrow("AuthCode") Is DBNull.Value And vrow("PaymentFlag") = "cc" Then

                                preAuthReady = TestPreAuthSwiped(vrow, True)
                                If preAuthReady = "Swiped" Then
                                    PreAuth(Nothing, vrow, True)
                                ElseIf preAuthReady = "Keyed" Then
                                    PreAuth(Nothing, vrow, True)
                                End If

                                '444           preAuthReady = TestPreAuthSwiped(oRow, Nothing, False)
                                '       If preAuthReady = "Swiped" Then
                                '       PreAuth(oRow, Nothing, False)
                                '  ElseIf preAuthReady = "Keyed" Then
                                '     PreAuth(oRow, Nothing, False)
                                '   End If
                            End If
                        End If
                    End If
                    '  End If
                End If
            Next
        End If
        '    Next

        '   or can do by dvClosingCheckPayments
        ' we are doing all the credit card payments first
        For Each vrow In dvClosingCheckPayments
            '444 For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            '444     If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
            If vrow("CheckNumber") = currentTable.CheckNumber Then
                If vrow("PaymentAmount") <> 0 Then
                    If vrow("Applied") = False Then
                        If vrow("PaymentFlag") = "cc" And Not vrow("AuthCode") Is DBNull.Value Then
                            '                               only approved
                            vrow("Track2") = DBNull.Value
                            PrintCreditCardReceipt(Nothing, vrow, True)
                            paymentRowIndex -= 1
                            unappliedPayments += vrow("PaymentAmount")
                            vrow("Applied") = True
                            RemovePaymentFromCollection(vrow("AccountNumber"))

                        ElseIf vrow("PaymentFlag") = "Gift" And Not vrow("AuthCode") Is DBNull.Value Then
                            '                               only approved
                            vrow("Track2") = DBNull.Value
                            PrintCreditCardReceipt(Nothing, vrow, True)
                            paymentRowIndex -= 1
                            unappliedPayments += vrow("PaymentAmount")
                            vrow("Applied") = True
                            RemovePaymentFromCollection(vrow("AccountNumber"))

                        ElseIf vrow("PaymentFlag") = "outside" Then 'And companyInfo.usingOutsideCreditProcessor = True Then
                            paymentRowIndex -= 1
                            unappliedPayments += vrow("PaymentAmount")
                            vrow("Applied") = True
                            vrow("PaymentTypeID") = 9
                            vrow("AuthCode") = 9
                            RunClosingPrint()
                            printFinalReceipt = True

                        ElseIf vrow("PaymentFlag") = "Gift Cert" Then
                            '                               only approved
                            '    vrow("Track2") = DBNull.Value
                            '    PrintCreditCardReceipt(Nothing, vrow, True)
                            If vrow("PaymentAmount") > RemainingBalance Then
                                MsgBox("Gift Certificate can not be more than Balance Due")
                            Else
                                paymentRowIndex -= 1
                                unappliedPayments += vrow("PaymentAmount")
                                vrow("Applied") = True
                            End If

                            '   RemovePaymentFromCollection(vrow("AccountNumber"))
                        End If

                    End If
                End If
            End If
            '444     End If
        Next
        RemainingBalance -= unappliedPayments
        If giftIssuingAmount > 0 Then
            If RemainingBalance > attemptingCash Then
                MsgBox("You must cover the entire Sale before Issue of Gift Card")
                issueGiftCards = False
            Else
                issueGiftCards = True
                RemainingBalance += giftIssuingAmount
            End If
            'this reduces by the amount still not covered by payments
            giftRemainingIssue -= unappliedPayments
        End If

        '   or can do by dvClosingCheckPayments
        For Each vrow In dvClosingCheckPayments
            '444  For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            '444    If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
            If vrow("CheckNumber") = currentTable.CheckNumber Then
                If vrow("PaymentAmount") <> 0 Then
                    If vrow("Applied") = False Then
                        If vrow("PaymentFlag") = "Cash" Then
                            '      If vrow("PaymentTypeID") = -98 Then   'Gift Certificate
                            '      If vrow("PaymentAmount") > RemainingBalance Then
                            '   MsgBox("Gift Certificate can not be more than Balance Due")
                            '  Exit Sub
                            '    End If
                            '   End If
                            For Each cRow In dvOrder
                                If cRow("sin") = cRow("sii") And Not cRow("ItemID") = 0 Then
                                    numItems += 1
                                End If
                            Next

                            If companyInfo.autoPrint = True Then
                                RunClosingPrint()
                                prt.closeDetail.isCashTendered = True
                                prt.closeDetail.cashTendered = vrow("PaymentAmount")
                                prt.closeDetail.chkChangeDue = vrow("PaymentAmount") - RemainingBalance
                                printFinalReceipt = True
                            Else
                                prt.PrintOpenCashDrawer()
                            End If

                            ccDisplay = New CashClose_UC(numItems, currentTable.TruncatedExpNum, RemainingBalance, vrow("PaymentAmount"))
                            ccDisplay.Location = New Point((Me.Width - ccDisplay.Width) / 2, (Me.Height - ccDisplay.Height) / 2)
                            If vrow("PaymentAmount") > RemainingBalance Then
                                ' means we are giving change
                                vrow("PaymentAmount") = RemainingBalance
                                RemainingBalance = 0
                            Else
                                RemainingBalance -= vrow("PaymentAmount")
                            End If
                            paymentRowIndex -= 1
                            '       unappliedPayments += vRow("PaymentAmount")
                            vrow("AuthCode") = "Cash"
                            vrow("Applied") = True

                            If Not RemainingBalance > 0 Then
                                displayCashClose_UC = True
                            End If
                        End If
                    Else
                        'applied = true
                        If vrow("PaymentFlag") = "Cash" Then
                            prt.closeDetail.cashAppliedPrevious += vrow("PaymentAmount")
                        End If

                    End If
                End If
            End If
            '444       End If
        Next

        If issueGiftCards = True Then
            Dim authStatus As String

            For Each vrow In dvClosingCheckPayments
                If vrow("AuthCode") Is DBNull.Value And vrow("PaymentFlag") = "Issue" Then

                    If vrow("PaymentTypeName") = "Issue Gift" Then
                        '**** issue Gift Cards
                        authStatus = GenerateOrderTables.GiftCardTransaction(vrow, Nothing, "Issue")
                    ElseIf vrow("PaymentTypeName") = "Increase Gift" Then
                        authStatus = GenerateOrderTables.GiftCardTransaction(vrow, Nothing, "Return")
                    End If
                   
                End If
            Next

            Dim index As Integer

            For Each vrow In dvClosingCheckPayments
                'just approved gift issue, seperate b/c we change row filter (Applied)
                If not vrow("AuthCode") Is DBNull.Value And vrow("PaymentFlag") = "Issue" Then
                    '                               only approved
                    vrow("Track2") = DBNull.Value
                    '    PrintCreditCardReceipt(Nothing, vrow, True)
                    _checkGiftIssuingAmount += vrow("PaymentAmount")
                    paymentPanel(index).UpdateGiftAccountBalance(vrow("paymentAmount"))
                    '   paymentRowIndex -= 1
                    vrow("Applied") = True
                    RemovePaymentFromCollection(vrow("AccountNumber"))
                End If
                If Not vrow("PaymentFlag") = "Cash" Then
                    index += 1
                End If
            Next

        End If

        If printFinalReceipt = True Then
            prt.StartPrintCheckReceipt()
        Else
            '      prt.PrintOpenCashDrawer()
        End If

        GetNumberOfActivePayments(currentTable.CheckNumber)
        Me.closeCheckTotals.AttachTotalsToTotalView(currentTable.CheckNumber)

        If displayCashClose_UC = True Then
            Me.Controls.Add(ccDisplay)
            ccDisplay.BringToFront()
        End If

    End Sub


    'encryption logic ???
    '       Dim data As String
    '       Dim result As HashAlgorithm
    '      Try
    '      Data = vrow("AccountNumber")
    '
    ''        Dim sha = New SHA1CryptoServiceProvider
    '        ' This is one implementation of the abstract class SHA1.
    '        result = sha.ComputeHash(Data)
    '        MsgBox(result.ToString)
    '
    '       Catch ex As Exception
    '      MsgBox(ex.Message)
    '     End Try
    '
    '       Exit Function 


    Private Function TestPreAuthSwiped(ByRef vRow As DataRowView, ByVal useVIEW As Boolean)

        Dim isReady As String
        Dim authAmount As Decimal

        If companyInfo.processor = "MerchantWare" Then
            Exit Function
        ElseIf companyInfo.processor = "PaywarePC" Then
            'need to fill the following:
            '   Ticket (ticket number)
            '   Amount (payment amount)
            '   Track or Account#/Exp Date

            authPayment = New PaymentInfo

            '   _closeAuthAmount.Purchase = vRow("PaymentAmount").ToString
            authAmount = (Format(vRow("PaymentAmount") + vRow("Surcharge"), "#####0.00")).ToString
            '   authAmount.Gratuity = Nothing
            vRow("PreAuthAmount") = authAmount
            vRow("TransactionCode") = "PreAuth"

            If Not vRow("RefNum") Is DBNull.Value Then
                authPayment.TicketNumber = vRow("RefNum")
            Else
                vRow("RefNum") = authPayment.TicketNumber
            End If

            If vRow("PaymentAmount") <= 0 Then
                MsgBox("Auth Amount must be greater than $0.00")
                Return isReady  'returns a Nothing value
            Else
                authPayment.paymentAmount = (Format(vRow("PaymentAmount") + vRow("Surcharge"), "#####0.00"))
            End If

            If vRow("Track2") Is DBNull.Value Then
                If paymentPanel(paymentRowIndex).AcctNumber.Length > 0 And paymentPanel(paymentRowIndex).ExpDate.Length = 4 Then
                    vRow("AccountNumber") = paymentPanel(paymentRowIndex).AcctNumber
                    vRow("CCExpiration") = paymentPanel(paymentRowIndex).ExpDate
                    authPayment.AcctNum = paymentPanel(paymentRowIndex).AcctNumber
                    authPayment.ExpDate = paymentPanel(paymentRowIndex).ExpDate

                    'for testing
                    '     authPayment.AcctNum = "4012000033330026"
                    '    authPayment.ExpDate = "1212"


                    isReady = "Keyed"
                    Return isReady
                End If

                MsgBox("Card Swipe Does Not Read Correctly")
                Return isReady
            Else
                authPayment.Track2 = vRow("Track2")

                'for testing
                '   authPayment.Track2 = "4012000033330026=12121011000001234567"


                isReady = "Swiped"
                Return isReady
            End If

        ElseIf companyInfo.processor = "Mercury" Then
            '     Dim authPayment As TStream
            _closeAuthAmount = New PreAuthAmountClass
            _closeAuthTransaction = New PreAuthTransactionClass
            _closeAuthAccount = New AccountClass

            'Mercury Authorizes Purchase Amount (even though we send both)
            _closeAuthAmount.Purchase = (Format(vRow("PaymentAmount") + vRow("Surcharge"), "#####0.00")).ToString
            _closeAuthAmount.Authorize = (Format(vRow("PaymentAmount") + vRow("Surcharge"), "#####0.00")).ToString
            '   authAmount.Gratuity = Nothing
            vRow("PreAuthAmount") = _closeAuthAmount.Authorize
            vRow("TransactionCode") = "PreAuth"

            _closeAuthTransaction.MerchantID = companyInfo.merchantID       'CompanyID & LocationID
            _closeAuthTransaction.OperatorID = companyInfo.operatorID      'currentServer.EmployeeID.ToString
            _closeAuthTransaction.TranType = vRow("TransactionType")    '"Credit"
            _closeAuthTransaction.Memo = "spiderPOS " & companyInfo.VersionNumber
            '    If vRow("Duplicate") = True Then
            '        _closeAuthTransaction.Duplicate = "Override"
            '   End If
            _closeAuthTransaction.TranCode = "PreAuth"


            ' in TestPreAuthSwiped
            If Not vRow("RefNum") Is DBNull.Value Then
                _closeAuthTransaction.InvoiceNo = vRow("RefNum")
                _closeAuthTransaction.RefNo = vRow("RefNum")
            Else
                '   _closeAuthTransaction.InvoiceNo = (currentTable.ExperienceNumber & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                '  _closeAuthTransaction.RefNo = (currentTable.ExperienceNumber & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                _closeAuthTransaction.InvoiceNo = (currentTable.TruncatedExpNum).ToString ' & "-" & (dvAppliedPayments.Count + 1)).ToString ' & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                _closeAuthTransaction.RefNo = (currentTable.TruncatedExpNum).ToString ' & "-" & (dvAppliedPayments.Count + 1)).ToString ' & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                vRow("RefNum") = _closeAuthTransaction.RefNo
            End If

            If vRow("Duplicate") = True Then
                _closeAuthTransaction.InvoiceNo = (currentTable.TruncatedExpNum & "-" & (dvAppliedPayments.Count + 1)).ToString ' & Format(currentTable.CheckNumber, "000")) ' & "-" & paymentRowIndex).ToString
                _closeAuthTransaction.RefNo = (currentTable.TruncatedExpNum & "-" & (dvAppliedPayments.Count + 1)).ToString ' & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                vRow("RefNum") = _closeAuthTransaction.RefNo
            End If

            If vRow("PaymentAmount") <= 0 Then
                MsgBox("Auth Amount must be greater than $0.00")
                Return isReady  'returns a Nothing value
            End If

            '****************
            '   for testing only
            '        _closeAuthAccount.AcctNo = "5499990123456781"
            '       _closeAuthAccount.ExpDate = "0809"
            '      isReady = "Keyed"
            '     Return isReady
            '
            '****************

            If vRow("Track2") Is DBNull.Value Then
                If paymentPanel(paymentRowIndex).AcctNumber.Length > 0 And paymentPanel(paymentRowIndex).ExpDate.Length = 4 Then
                    vRow("AccountNumber") = paymentPanel(paymentRowIndex).AcctNumber
                    vRow("CCExpiration") = paymentPanel(paymentRowIndex).ExpDate
                    _closeAuthAccount.AcctNo = paymentPanel(paymentRowIndex).AcctNumber
                    _closeAuthAccount.ExpDate = paymentPanel(paymentRowIndex).ExpDate
                    isReady = "Keyed"
                    Return isReady
                End If

                MsgBox("Card Swipe Does Not Read Correctly")
                Return isReady
            Else
                _closeAuthAccount.Track2 = vRow("Track2")
                isReady = "Swiped"
                Return isReady
            End If
        End If

    End Function

    Private Function TestPreAuthSwiped222(ByRef orow As DataRow, ByRef vRow As DataRowView, ByVal useVIEW As Boolean)

        Dim isReady As String

        '     Dim authPayment As TStream
        _closeAuthAmount = New PreAuthAmountClass
        _closeAuthTransaction = New PreAuthTransactionClass
        _closeAuthAccount = New AccountClass
        If useVIEW = True Then
            _closeAuthAmount.Purchase = vRow("PaymentAmount").ToString
            _closeAuthAmount.Authorize = (Format(vRow("PaymentAmount") + vRow("Surcharge"), "#####0.00")).ToString
            '   authAmount.Gratuity = Nothing
            vRow("PreAuthAmount") = _closeAuthAmount.Authorize
            vRow("TransactionCode") = "PreAuth"

            _closeAuthTransaction.MerchantID = companyInfo.merchantID       'CompanyID & LocationID
            _closeAuthTransaction.OperatorID = companyInfo.operatorID      'currentServer.EmployeeID.ToString
            _closeAuthTransaction.TranType = vRow("TransactionType")    '"Credit"
            _closeAuthTransaction.Memo = "spiderPOS " & companyInfo.VersionNumber
            '    If vRow("Duplicate") = True Then
            '        _closeAuthTransaction.Duplicate = "Override"
            '   End If
            _closeAuthTransaction.TranCode = "PreAuth"
        Else
            _closeAuthAmount.Purchase = orow("PaymentAmount").ToString
            _closeAuthAmount.Authorize = (Format(orow("PaymentAmount") + orow("Surcharge"), "#####0.00")).ToString
            '   authAmount.Gratuity = Nothing
            orow("PreAuthAmount") = _closeAuthAmount.Authorize

            orow("TransactionCode") = "PreAuth"

            _closeAuthTransaction.MerchantID = companyInfo.merchantID       'CompanyID & LocationID
            '999        _closeAuthTransaction.MerchantID = "888000000253"       'CompanyID & LocationID
            _closeAuthTransaction.OperatorID = companyInfo.operatorID      'currentServer.EmployeeID.ToString
            _closeAuthTransaction.TranType = orow("TransactionType")    '"Credit"
            _closeAuthTransaction.Memo = "spiderPOS " & companyInfo.VersionNumber
            '     If orow("Duplicate") = True Then
            '        _closeAuthTransaction.Duplicate = "Override"
            '   End If
            _closeAuthTransaction.TranCode = "PreAuth"
        End If

        ' in TestPreAuthSwiped
        If useVIEW = True Then
            If Not vRow("RefNum") Is DBNull.Value Then
                _closeAuthTransaction.InvoiceNo = vRow("RefNum")
                _closeAuthTransaction.RefNo = vRow("RefNum")
            Else
                '   _closeAuthTransaction.InvoiceNo = (currentTable.ExperienceNumber & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                '  _closeAuthTransaction.RefNo = (currentTable.ExperienceNumber & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                _closeAuthTransaction.InvoiceNo = (currentTable.TruncatedExpNum).ToString ' & "-" & (dvAppliedPayments.Count + 1)).ToString ' & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                _closeAuthTransaction.RefNo = (currentTable.TruncatedExpNum).ToString ' & "-" & (dvAppliedPayments.Count + 1)).ToString ' & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                vRow("RefNum") = _closeAuthTransaction.RefNo
            End If

            If vRow("Duplicate") = True Then
                _closeAuthTransaction.InvoiceNo = (currentTable.TruncatedExpNum & "-" & (dvAppliedPayments.Count + 1)).ToString ' & Format(currentTable.CheckNumber, "000")) ' & "-" & paymentRowIndex).ToString
                _closeAuthTransaction.RefNo = (currentTable.TruncatedExpNum & "-" & (dvAppliedPayments.Count + 1)).ToString ' & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                vRow("RefNum") = _closeAuthTransaction.RefNo
            End If

            If vRow("PaymentAmount") <= 0 Then
                MsgBox("Auth Amount must be greater than $0.00")
                Return isReady  'returns a Nothing value
            End If

            '****************
            '   for testing only
            '        _closeAuthAccount.AcctNo = "5499990123456781"
            '       _closeAuthAccount.ExpDate = "0809"
            '      isReady = "Keyed"
            '     Return isReady
            '
            '****************

            If vRow("Track2") Is DBNull.Value Then
                If paymentPanel(paymentRowIndex).AcctNumber.Length > 0 And paymentPanel(paymentRowIndex).ExpDate.Length = 4 Then
                    vRow("AccountNumber") = paymentPanel(paymentRowIndex).AcctNumber
                    vRow("CCExpiration") = paymentPanel(paymentRowIndex).ExpDate
                    _closeAuthAccount.AcctNo = paymentPanel(paymentRowIndex).AcctNumber
                    _closeAuthAccount.ExpDate = paymentPanel(paymentRowIndex).ExpDate
                    isReady = "Keyed"
                    Return isReady
                End If

                MsgBox("Card Swipe Does Not Read Correctly")
                Return isReady
            Else
                _closeAuthAccount.Track2 = vRow("Track2")
                isReady = "Swiped"
                Return isReady
            End If
        Else
            If Not orow("RefNum") Is DBNull.Value Then
                _closeAuthTransaction.InvoiceNo = orow("RefNum")
                _closeAuthTransaction.RefNo = orow("RefNum")
            Else
                _closeAuthTransaction.InvoiceNo = (currentTable.TruncatedExpNum).ToString ' & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                _closeAuthTransaction.RefNo = (currentTable.TruncatedExpNum).ToString ' & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                orow("RefNum") = _closeAuthTransaction.RefNo
            End If
            If orow("Duplicate") = True Then
                '?????????????
                _closeAuthTransaction.InvoiceNo = (currentTable.TruncatedExpNum & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                _closeAuthTransaction.RefNo = (currentTable.TruncatedExpNum & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
                orow("RefNum") = _closeAuthTransaction.RefNo
            End If

            If orow("PaymentAmount") <= 0 Then
                MsgBox("Auth Amount must be greater than $0.00")
                Return isReady  'returns a Nothing value
            End If

            '****************
            '   for testing only
            '        _closeAuthAccount.AcctNo = "5499990123456781"
            '       _closeAuthAccount.ExpDate = "0809"
            '      isReady = "Keyed"
            '     Return isReady
            '
            '****************

            If orow("Track2") Is DBNull.Value Then
                'this is manual 
                If paymentPanel(paymentRowIndex).AcctNumber Is Nothing Then
                    MsgBox("You must enter an account number")
                    Exit Function
                End If
                If paymentPanel(paymentRowIndex).ExpDate Is Nothing Then
                    MsgBox("You must enter an expiration date")
                    Exit Function
                End If
                If paymentPanel(paymentRowIndex).AcctNumber.Length > 0 And paymentPanel(paymentRowIndex).ExpDate.Length = 4 Then
                    orow("AccountNumber") = paymentPanel(paymentRowIndex).AcctNumber
                    orow("CCExpiration") = paymentPanel(paymentRowIndex).ExpDate
                    _closeAuthAccount.AcctNo = paymentPanel(paymentRowIndex).AcctNumber
                    _closeAuthAccount.ExpDate = paymentPanel(paymentRowIndex).ExpDate
                    isReady = "Keyed"
                    Return isReady
                End If

                MsgBox("Card Swipe Does Not Read Correctly")
                Return isReady
            Else
                _closeAuthAccount.Track2 = orow("Track2")
                isReady = "Swiped"
                Return isReady
            End If
        End If

    End Function

    Private Sub DetermineInvoiseNumber222(ByRef orow As DataRow, ByRef vRow As DataRowView, ByVal useVIEW As Boolean)
        'this is extra
        'we should use this

        If Not vRow("RefNum") Is DBNull.Value Then
            _closeAuthTransaction.InvoiceNo = vRow("RefNum")
            _closeAuthTransaction.RefNo = vRow("RefNum")
        Else
            _closeAuthTransaction.InvoiceNo = (currentTable.TruncatedExpNum).ToString
            _closeAuthTransaction.RefNo = (currentTable.TruncatedExpNum).ToString
        End If

        If vRow("Duplicate") = True Then
            _closeAuthTransaction.InvoiceNo = (currentTable.TruncatedExpNum & "-" & (dvAppliedPayments.Count + 1)).ToString
            _closeAuthTransaction.RefNo = (currentTable.TruncatedExpNum & "-" & (dvAppliedPayments.Count + 1)).ToString
        End If

        '    vRow("RefNum") = _closeAuthTransaction.RefNo

    End Sub

    Private Function PreAuth(ByRef orow As DataRow, ByRef vRow As DataRowView, ByVal useVIEW As Boolean)

        '******************
        '   only using vRow  useVIEW = True
        '******************
        Dim paymentWentThrough As Boolean

        If companyInfo.processor = "MerchantWare" Then
            'not doing here
        ElseIf companyInfo.processor = "Mercury" Then
            PreAuthMercury(vRow)
        ElseIf companyInfo.processor = "PaywarePC" Then
            '   PreAuthMercury(vRow)
            PreAuthPaywarePC(vRow)
            'PreAuthVeriFone(vRow)
        End If

        Return paymentWentThrough

        Exit Function
        '222 below

        '     If newPayment.PaymentTypeName = "MPS Gift" Then
        '     newPayment.Balance = DetermineGiftCard(newPayment, "Balance")
        '    End If

        '444     Dim paymentWentThrough As Boolean
        Dim authStatus As String

        Dim mpsPreAuth As New TStream
        Dim mpsPreAuthTransaction As New PreAuthTransactionClass

        mpsPreAuthTransaction = _closeAuthTransaction
        mpsPreAuthTransaction.Account = _closeAuthAccount
        mpsPreAuthTransaction.Amount = _closeAuthAmount

        mpsPreAuth.Transaction = mpsPreAuthTransaction

        Dim output As New StringWriter(New StringBuilder)
        Dim s As New XmlSerializer(GetType(TStream))
        s.Serialize(output, mpsPreAuth)

        If Not typeProgram = "Online_Demo" Then
            Dim transDetails As String
            If useVIEW = True Then
                If vRow("Duplicate") = True Then
                    transDetails = "DuplicateAuth"
                Else
                    transDetails = "PreAuth"
                End If
            Else
                If orow("Duplicate") = True Then
                    transDetails = "DuplicateAuth"
                Else
                    transDetails = "PreAuth"
                End If
            End If
            '444    authStatus = SendingDSI222(output.ToString, transDetails, orow, vRow, useVIEW)

        Else
            authStatus = "Approved"
            If useVIEW = True Then
                vRow("AuthCode") = "123456"
            Else
                orow("AuthCode") = "123456"
            End If
        End If

        If authStatus = "Approved" Then
            paymentWentThrough = True
        Else
            paymentWentThrough = False
        End If

        Return paymentWentThrough

    End Function

    Private Function PreAuthMerchantWare_Active(ByRef vRow As DataRowView)

        RaiseEvent MerchantAuthPayment(vRow("PaymentID"), True) ' true is JustActive

    End Function

    Private Function PreAuthMerchantWare_All(ByRef vRow As DataRowView)

        RaiseEvent MerchantAuthPayment(vRow("PaymentID"), False) ' false is AuthAll

    End Function

    Private Function PreAuthMercury(ByRef vRow As DataRowView)
        '     If newPayment.PaymentTypeName = "MPS Gift" Then
        '     newPayment.Balance = DetermineGiftCard(newPayment, "Balance")
        '    End If

        Dim paymentWentThrough As Boolean
        Dim authStatus As String

        Dim mpsPreAuth As New TStream
        Dim mpsPreAuthTransaction As New PreAuthTransactionClass

        mpsPreAuthTransaction = _closeAuthTransaction
        mpsPreAuthTransaction.Account = _closeAuthAccount
        mpsPreAuthTransaction.Amount = _closeAuthAmount

        mpsPreAuth.Transaction = mpsPreAuthTransaction

        Dim output As New StringWriter(New StringBuilder)
        Dim s As New XmlSerializer(GetType(TStream))
        s.Serialize(output, mpsPreAuth)

        If Not typeProgram = "Online_Demo" Then
            Dim transDetails As String
            If vRow("Duplicate") = True Then
                transDetails = "DuplicateAuth"
            Else
                transDetails = "PreAuth"
            End If

            authStatus = SendingDSI(output.ToString, transDetails, vRow)
            '444     authStatus = SendingDSI(output.ToString, transDetails, orow, vRow, useVIEW)
        Else
            authStatus = "Approved"
            vRow("AuthCode") = "123456"
        End If

        If authStatus = "Approved" Then
            paymentWentThrough = True
        Else
            paymentWentThrough = False
        End If

        Return paymentWentThrough

    End Function

    Private Function SendingDSI(ByVal XMLString As String, ByVal transDetails As String, ByRef vRow As DataRowView)

        Dim resp As String
        Dim authStatus As String
        Dim dataFileLocation As String
        '      Dim sWriter1 As StreamWriter
        '      Dim sWriter2 As StreamWriter

        Try
            dsi.ServerIPConfig("x1.mercurypay.com;x2.mercurypay.com;b1.backuppay.com;b2.backuppay.com", 0)
            '    dsi.ServerIPConfig("x1.mercurydev.net", 0)
            '    dsi.ServerIPConfig("127.0.0.1;x2.mercurypay.com;b1.backuppay.com;b2.backuppay.com", 0)
            resp = dsi.ProcessTransaction(XMLString, 0, "", "")
        Catch ex As Exception
            MsgBox("Could not establish connection to Payment Processor.")
            Exit Function
        End Try

        If transDetails = "DuplicateAuth" Then
            '          sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendDuplicateAuth.txt")
            '           sWriter2 = New StreamWriter("c:\Data Files\spiderPOS\PreAuthResponse.txt")
        ElseIf transDetails = "PreAuth" Then
            '         sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendPreAuth.txt")
            '         sWriter2 = New StreamWriter("c:\Data Files\spiderPOS\PreAuthResponse.txt")
        Else ' means something wrong
            Exit Function
        End If

        '       sWriter1.Write(XMLString)
        '    sWriter1.Close()

        '      sWriter2.Write(resp)
        '    sWriter2.Close()

        '    MsgBox(resp)
        authStatus = ParseXMLResponse(resp, vRow) 'orow, vRow, useVIEW)
        Return authStatus

    End Function

    Private Function ParseXMLResponse(ByVal resp As String, ByRef vRow As DataRowView)

        Dim reader As New XmlTextReader(New StringReader(resp))
        Dim someError As Boolean
        Dim isPreAuth As Boolean
        Dim isApproved As Boolean
        Dim authStatus As String
        Dim isDeclined As Boolean
        Dim pRow As DataRow
        Dim looksLikeDup As Boolean
        Dim tempAuthCode As String
        Dim tempAcqRef As String
        Dim isAmexDcvr As Boolean
        Dim tempCardType As String

        Try
            Do Until reader.EOF = True
                reader.Read()
                reader.MoveToContent()
                If reader.NodeType = XmlNodeType.Element Then
                    '         MsgBox(reader.Name)
                    Select Case reader.Name

                        Case "DSIXReturnCode"
                            If String.Compare(reader.ReadInnerXml, "000000", True) <> 0 Then
                                '   false, do not honor case (is not case sensitive)
                                '   a zero means the same (therefore this is not the same)
                                '   there is sometype of error
                                someError = True


                            End If
                            '               MsgBox(reader.ReadInnerXml, , "OperatorID")
                        Case "CmdStatus"
                            Select Case reader.ReadInnerXml
                                Case "Approved"
                                    isApproved = True
                                    authStatus = "Approved"

                                Case "Declined"
                                    isDeclined = True

                                Case "Success"

                                Case "Error"
                                    someError = True
                                    '  authStatus = "Declined"
                            End Select

                        Case "TextResponse"

                            Select Case reader.ReadInnerXml
                                Case "AP"
                                    vRow("Description") = "APPROVED"

                                Case "OTHER NOT ACCEPTED."
                                    MsgBox("CARD Account Number: '" & vRow("AccountNumber") & "' Not correct, please verify input.")
                                    vRow("Description") = "Card Number incorrect"
                                    Return "DECLINED"

                                Case "DECLINE"

                            End Select

                            '              If reader.ReadInnerXml.ToString = "AP" Then
                            '             ElseIf reader.ReadInnerXml.ToString = "OTHER NOT ACCEPTED." Then
                            '            'could also do an ElseIf Statement for "DELINE", but we flag above
                            '           Else
                            '          vRow("Description") = reader.ReadInnerXml.ToString
                            '         End If

                            '      If someError = True Then
                            '     If reader.ReadInnerXml.ToString = "OTHER NOT ACCEPTED." Then
                            '    MsgBox("CARD Account Number: '" & vRow("AccountNumber") & "' Not correct, please verify input.")
                            '    Return "DECLINED"
                            '     Else
                            '   MsgBox("Error processing card: " & reader.ReadInnerXml.ToString)
                            '    Return "Error"
                            '     End If

                            '     Else
                            If isDeclined = True Then
                                MsgBox("CARD '" & vRow("AccountNumber") & "' DECLINED: " & reader.ReadInnerXml.ToString)
                                vRow("Description") = "Declined"
                                Return "DECLINED"
                            End If

                        Case "UserTraceData"

                            '                         **************************************
                            '                                 Transaction Response
                            '                         **************************************

                        Case "TranCode"
                            If String.Compare(reader.ReadInnerXml, "PreAuth", True) = 0 Then
                                isPreAuth = True
                            End If

                        Case "RefNo"
                            If isPreAuth = True And isApproved = True Then
                                '  *** ? place RefNo in database
                                '      vrow("RefNo") = reader.ReadInnerXml

                            End If

                        Case "CardType"

                            tempCardType = reader.ReadInnerXml.ToString
                            If tempCardType = "AMEX" Or tempCardType = "DCVR" Then
                                isAmexDcvr = True
                            End If
                        Case "AuthCode"
                            tempAuthCode = reader.ReadInnerXml.ToString
                            '      looksLikeDup = TestForDups(tempAuthCode)
                            If isAmexDcvr = True Then
                                ' if is duplicate we are overriding 
                                If vRow("Duplicate") = False Then
                                    looksLikeDup = TestForDups(tempAuthCode, Nothing)
                                End If

                                If looksLikeDup = True Then
                                    MsgBox("This exact transaction has already been entered. If this is a valid duplicate transaction with the same credit card, please hit the Duplicate Credit button and resend transaction.")
                                    Return "DUPLICATE"
                                End If

                                If isPreAuth = True And isApproved = True Then
                                    '   place authcode in database
                                    vRow("AuthCode") = tempAuthCode
                                End If

                            End If

                        Case "AcqRefData"
                            'currently not getting here
                            '   so we are just checking Approval Code
                            '(which is not perfect but probably ok)
                            tempAcqRef = reader.ReadInnerXml.ToString

                            ' if is duplicate we are overriding 
                            If vRow("Duplicate") = False Then
                                looksLikeDup = TestForDups(tempAuthCode, tempAcqRef)
                            End If

                            If looksLikeDup = True Then
                                MsgBox("This exact transaction has already been entered. Verify this is correct.")
                                '    MsgBox("This exact transaction has already been entered. If this is a valid duplicate transaction with the same credit card, please hit the Duplicate Credit button and resend transaction.")
                                '   Return "DUPLICATE"
                            End If

                            If isPreAuth = True And isApproved = True Then
                                '   place authcode in database
                                vRow("AuthCode") = tempAuthCode
                            End If

                            If isPreAuth = True And isApproved = True Then
                                '   place acqRefData in database
                                vRow("AcqRefData") = tempAcqRef
                            End If

                    End Select
                End If
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            If Not reader Is Nothing Then
                reader.Close()
            End If

        End Try

        Return authStatus

    End Function

    Private Function ParseXMLResponse222(ByVal resp As String, ByRef orow As DataRow, ByRef vRow As DataRowView, ByVal useVIEW As Boolean)

        Dim reader As New XmlTextReader(New StringReader(resp))
        Dim someError As Boolean
        Dim isPreAuth As Boolean
        Dim isApproved As Boolean
        Dim authStatus As String
        Dim isDeclined As Boolean
        Dim pRow As DataRow
        Dim looksLikeDup As Boolean
        Dim tempAuthCode As String
        Dim tempAcqRef As String
        Dim isAmexDcvr As Boolean
        Dim tempCardType As String

        Try
            Do Until reader.EOF = True
                reader.Read()
                reader.MoveToContent()
                If reader.NodeType = XmlNodeType.Element Then
                    '         MsgBox(reader.Name)
                    Select Case reader.Name

                        Case "DSIXReturnCode"
                            If String.Compare(reader.ReadInnerXml, "000000", True) <> 0 Then
                                '   false, do not honor case (is not case sensitive)
                                '   a zero means the same (therefore this is not the same)
                                '   there is sometype of error
                                someError = True


                            End If
                            '               MsgBox(reader.ReadInnerXml, , "OperatorID")
                        Case "CmdStatus"
                            Select Case reader.ReadInnerXml
                                Case "Approved"
                                    isApproved = True
                                    authStatus = "Approved"

                                Case "Declined"
                                    isDeclined = True

                                Case "Success"

                                Case "Error"

                            End Select

                        Case "TextResponse"

                            If useVIEW = True Then
                                vRow("Description") = reader.ReadInnerXml.ToString
                            Else
                                orow("Description") = reader.ReadInnerXml.ToString
                            End If

                            If someError = True Then
                                MsgBox(reader.ReadInnerXml.ToString)
                                Return "Error"
                                '                                Exit Function

                            ElseIf isDeclined = True Then
                                If useVIEW = True Then
                                    MsgBox("CARD '" & vRow("AccountNumber") & "' DECLINED: " & reader.ReadInnerXml.ToString)
                                    Return "DECLINED"
                                Else
                                    MsgBox("CARD '" & orow("AccountNumber") & "' DECLINED: " & reader.ReadInnerXml.ToString)
                                    Return "DECLINED"
                                End If

                            End If

                        Case "UserTraceData"

                            '                         **************************************
                            '                                 Transaction Response
                            '                         **************************************

                        Case "TranCode"
                            If String.Compare(reader.ReadInnerXml, "PreAuth", True) = 0 Then
                                isPreAuth = True
                            End If

                        Case "RefNo"
                            If isPreAuth = True And isApproved = True Then
                                '  *** ? place RefNo in database
                                '      vrow("RefNo") = reader.ReadInnerXml

                            End If

                        Case "CardType"

                            tempCardType = reader.ReadInnerXml.ToString
                            If tempCardType = "AMEX" Or tempCardType = "DCVR" Then
                                isAmexDcvr = True
                            End If
                        Case "AuthCode"
                            tempAuthCode = reader.ReadInnerXml.ToString
                            '      looksLikeDup = TestForDups(tempAuthCode)
                            If isAmexDcvr = True Then
                                If useVIEW = True Then
                                    ' if is duplicate we are overriding 
                                    If vRow("Duplicate") = False Then
                                        looksLikeDup = TestForDups(tempAuthCode, Nothing)
                                    End If
                                Else
                                    If orow("Duplicate") = False Then
                                        looksLikeDup = TestForDups(tempAuthCode, Nothing)
                                    End If
                                End If

                                If looksLikeDup = True Then
                                    MsgBox("This exact transaction has already been entered. If this is a valid duplicate transaction with the same credit card, please hit the Duplicate Credit button and resend transaction.")
                                    Return "DUPLICATE"
                                End If



                                If looksLikeDup = True Then
                                    '                For Each pRow In dsOrder.Tables("PaymentsAndCredits").Rows
                                    '               If Not pRow.RowState = DataRowState.Deleted And Not pRow.RowState = DataRowState.Detached Then
                                    '              If Not pRow("AcqRefData") Is DBNull.Value Then
                                    '          If String.Compare(reader.ReadInnerXml, pRow("AcqRefData"), True) = 0 Then
                                    MsgBox("This exact transaction has already been entered. If this is a valid duplicate transaction with the same credit card, please hit the Duplicate Credit button and resend transaction.")
                                    '  Exit Function
                                    Return "DUPLICATE"
                                    '     End If
                                    '         End If
                                    '        End If
                                    '           Next
                                End If
                                If isPreAuth = True And isApproved = True Then
                                    '   place authcode in database
                                    If useVIEW = True Then
                                        vRow("AuthCode") = tempAuthCode
                                    Else
                                        orow("AuthCode") = tempAuthCode
                                    End If

                                End If

                            End If


                        Case "AcqRefData"
                            'currently not getting here
                            '   so we are just checking Approval Code
                            '(which is not perfect but probably ok)
                            tempAcqRef = reader.ReadInnerXml.ToString

                            If useVIEW = True Then
                                ' if is duplicate we are overriding 
                                If vRow("Duplicate") = False Then
                                    looksLikeDup = TestForDups(tempAuthCode, tempAcqRef)
                                End If
                            Else
                                If orow("Duplicate") = False Then
                                    looksLikeDup = TestForDups(tempAuthCode, tempAcqRef)
                                End If
                            End If

                            If looksLikeDup = True Then
                                MsgBox("This exact transaction has already been entered. If this is a valid duplicate transaction with the same credit card, please hit the Duplicate Credit button and resend transaction.")
                                Return "DUPLICATE"
                            End If



                            If looksLikeDup = True Then
                                '                For Each pRow In dsOrder.Tables("PaymentsAndCredits").Rows
                                '               If Not pRow.RowState = DataRowState.Deleted And Not pRow.RowState = DataRowState.Detached Then
                                '              If Not pRow("AcqRefData") Is DBNull.Value Then
                                '          If String.Compare(reader.ReadInnerXml, pRow("AcqRefData"), True) = 0 Then
                                MsgBox("This exact transaction has already been entered. If this is a valid duplicate transaction with the same credit card, please hit the Duplicate Credit button and resend transaction.")
                                '  Exit Function
                                Return "DUPLICATE"
                                '     End If
                                '         End If
                                '        End If
                                '           Next
                            End If
                            If isPreAuth = True And isApproved = True Then
                                '   place authcode in database
                                If useVIEW = True Then
                                    vRow("AuthCode") = tempAuthCode
                                Else
                                    orow("AuthCode") = tempAuthCode
                                End If

                            End If
                            If isPreAuth = True And isApproved = True Then
                                '   place acqRefData in database
                                If useVIEW = True Then
                                    vRow("AcqRefData") = tempAcqRef
                                Else
                                    orow("AcqRefData") = tempAcqRef
                                End If
                            End If

                    End Select
                End If
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            If Not reader Is Nothing Then
                reader.Close()
            End If

        End Try

        Return authStatus

    End Function


    Private Function TestForDups(ByVal authString As String, ByVal refString As String)
        Dim isDuiplicate As Boolean
        Dim pRow As DataRow

        '*** need to add back RefNum Data (not for AMEX)

        For Each pRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not pRow.RowState = DataRowState.Deleted Then
                '      If orow("CheckNumber") = currentTable.CheckNumber Then
                '     If orow("Applied") = True Then
                '    If Not pRow("AcqRefData") Is DBNull.Value And Not pRow("AuthCode") Is DBNull.Value Then
                If Not pRow("AuthCode") Is DBNull.Value Then
                    '             MsgBox(authString)
                    '            MsgBox(pRow("AuthCode"))
                    '           MsgBox(refString)
                    '          MsgBox(pRow("AcqRefData"))
                    '    If String.Compare(refString, pRow("AcqRefData"), True) = 0 And String.Compare(authString, pRow("AuthCode"), True) = 0 Then
                    If String.Compare(authString, pRow("AuthCode"), True) = 0 Then
                        '      MsgBox("This exact transaction has already been entered. If this is a valid duplicate transaction with the same credit card, please hit the Duplicate Credit button and resend transaction.")
                        '     Return "DUPLICATE"
                        isDuiplicate = True
                        '  looksLikeDup = True
                    End If
                    '    End If
                    '  End If
                End If
            End If
        Next

        Return isDuiplicate

    End Function

    Private Sub PrintCreditCardReceipt(ByRef orow As DataRow, ByRef vRow As DataRowView, ByVal useVIEW As Boolean)

        prt.ccDataRow = orow
        prt.ccDataRowView = vRow
        prt.useVIEW = useVIEW
        prt.StartPrintCreditCardRest()
        prt.StartPrintCreditCardGuest()

        '   ***
        '  vRow("AccountNumber") = TruncateAccountNumber(vRow("AccountNumber"))

    End Sub

    Private Function TestPayments(ByVal payAmount As Decimal) '(ByRef vRow As DataRowView)

        If payAmount > (RemainingBalance + 0.02) Then
            '   remainingBalance is what has already been applied
            If MsgBox("You are applying more than the balance. Are you sure?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Return False
            Else
                Return True
            End If
        End If

        Return True

    End Function

    Private Function TestAccountNumber(ByRef vRow As DataRowView)

        If vRow("AccountNumber") Is DBNull.Value Then
            MsgBox("You must Enter a valid Account Number")
            Return False
        End If

        If vRow("CCExpiration") Is DBNull.Value Then
            MsgBox("You must Enter a valid Expiration Date")
            Return False
        Else
            If ValidateExpDate(vRow("CCExpiration")) = False Then
                '444    MsgBox("You must Enter a valid Expiration Date")
                Return False
            End If
        End If

        Return True

    End Function

    Private Sub PaymentUserControl_History(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlClosePayments.DoubleClick     '(ByVal ai As Integer) Handles paymentUserControl.ActivePanel

        Dim newPayment As New DataSet_Builder.Payment
        For Each newPayment In tabcc
            If newPayment.AccountNumber = sender.AcctNumber Then
                Exit For
            End If
        Next
       
        If newPayment.LastName Is Nothing Or newPayment.AccountNumber Is Nothing Then Exit Sub

        '**** this is going to event in Split checks
        RaiseEvent FireTabScreen("Account", newPayment.SpiderAcct)
        Exit Sub
        '222

        newPayment.SpiderAcct = CreateAccountNumber(newPayment) '.LastName, newPayment.AccountNumber)
        TabEnterScreen = New Tab_Screen() '"Account")
        TabEnterScreen.Location = New Point(((Me.Width - TabEnterScreen.Width - 10) / 2), ((Me.Height - TabEnterScreen.Height) / 2))
        Me.Controls.Add(TabEnterScreen)
        TabEnterScreen.BringToFront()

        TabEnterScreen.SearchAccount(newPayment.SpiderAcct) ', -123456789)
        TabEnterScreen.BindDataAfterSearch()
        DisableControls()

    End Sub

    Private Sub CloseSelectedReOrder(ByVal dt As DataTable, ByVal tabTestNeeded As Boolean) Handles TabEnterScreen.SelectedReOrder

        'need to send data table
        RaiseEvent SelectedReOrder(dt, True)

    End Sub

    Private Sub CloseTabEnter222() Handles TabEnterScreen.TabScreenDisposing

        If TabEnterScreen.attemptedToEdit = True Then
            GenerateOrderTables.UpdateTabInfo(TabEnterScreen.StartInSearch)
        End If

        '     Me.TabEnterScreen.Dispose()
        Me.TabEnterScreen.Hide()
        EnableControls()

    End Sub

    Private Sub DisableControls()

        Me.pnlExit.Enabled = False
        Me.pnlPayOptions.Enabled = False
        Me.pnlClosePayments.Enabled = False
        Me.pnlBalance.Enabled = False
        Me.pnlPayRemove.Enabled = False
        Me.NumberPadLarge1.Enabled = False

    End Sub

    Private Sub EnableControls()

        Me.pnlExit.Enabled = True
        Me.pnlPayOptions.Enabled = True
        Me.pnlClosePayments.Enabled = True
        Me.pnlBalance.Enabled = True
        Me.pnlPayRemove.Enabled = True
        Me.NumberPadLarge1.Enabled = True

    End Sub

    Private Sub PaymentUserControl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlClosePayments.Click     '(ByVal ai As Integer) Handles paymentUserControl.ActivePanel
        ResetTimer()

        Dim objButton As DataSet_Builder.Payment_UC

        Try
            objButton = CType(sender, DataSet_Builder.Payment_UC)
        Catch ex As Exception
            Exit Sub
        End Try

        Select Case objButton.CurrentState
            Case DataSet_Builder.Payment_UC.PanelHit.AccountPanel
                Me.NumberPadLarge1.DecimalUsed = False
                NumberPadLarge1.NumberString = objButton.AcctNumber

            Case DataSet_Builder.Payment_UC.PanelHit.ExpDatePanel
                Me.NumberPadLarge1.DecimalUsed = False
                NumberPadLarge1.NumberString = objButton.ExpDate

            Case DataSet_Builder.Payment_UC.PanelHit.PurchasePanel
                Me.NumberPadLarge1.DecimalUsed = True
                NumberPadLarge1.NumberTotal = objButton.PurchaseAmount
                '444           NumberPadLarge1.NumberString = objButton.PurchaseAmount
                _lastPurchaseIssueAmount = objButton.PurchaseAmount * -1 'for Gift

            Case DataSet_Builder.Payment_UC.PanelHit.TipPanel
                Me.NumberPadLarge1.DecimalUsed = True
                NumberPadLarge1.NumberTotal = objButton.TipAmount
                '444        NumberPadLarge1.NumberString = objButton.TipAmount

        End Select
        NumberPadLarge1.ShowNumberString()
        NumberPadLarge1.NumberString = ""

        If Not paymentRowIndex = objButton.ActiveIndex Then
            paymentRowIndex = objButton.ActiveIndex
            ActiveThisPanel(objButton.ActiveIndex)
        End If


        '      Me.NumberPadLarge1.DecimalUsed = True
        '     Me.NumberPadLarge1.NumberTotal = 0
        '    Me.NumberPadLarge1.IntegerNumber = 0
        '   Me.NumberPadLarge1.NumberString = ""
        '  Me.NumberPadLarge1.ShowNumberString()

    End Sub

    Private Sub ActiveThisPanel(ByVal ai As Integer)

        If paymentRowIndex = 0 Then Exit Sub

        Dim vRow As DataRowView
        Dim index As Integer = 1

        For Each vRow In dvUnAppliedPaymentsAndCredits_MWE
            If index >= startPaymentIndex And index <= (startPaymentIndex + 5) Then
                If Not index = ai Then
                    paymentPanel(index).BackColor = Color.DarkGray
                    paymentPanel(index).IsActive = False
                Else
                    paymentPanel(index).BackColor = Color.WhiteSmoke
                    paymentPanel(paymentRowIndex).CurrentState = DataSet_Builder.Payment_UC.PanelHit.PurchasePanel
                    '      unappliedRowIndex = ai
                    PaymentPanelActivated()
                End If
            End If
            index += 1
        Next

        For Each vRow In dvClosingCheckPayments
            If index >= startPaymentIndex And index <= (startPaymentIndex + 5) Then
                If Not index = ai Then
                    paymentPanel(index).BackColor = Color.DarkGray
                    paymentPanel(index).IsActive = False
                Else
                    paymentPanel(index).BackColor = Color.WhiteSmoke
                    paymentPanel(paymentRowIndex).CurrentState = DataSet_Builder.Payment_UC.PanelHit.PurchasePanel
                    '      unappliedRowIndex = ai
                    PaymentPanelActivated()
                End If
            End If
            index += 1
        Next

    End Sub

    Private Sub PaymentPanelActivated()

        Select Case paymentPanel(paymentRowIndex).CurrentState
            Case DataSet_Builder.Payment_UC.PanelHit.PurchasePanel
                Me.NumberPadLarge1.DecimalUsed = True
                Me.NumberPadLarge1.NumberTotal = paymentPanel(paymentRowIndex).PurchaseAmount
                Me.NumberPadLarge1.ShowNumberString()
                Me.NumberPadLarge1.Focus()
                Me.NumberPadLarge1.IntegerNumber = 0
                Me.NumberPadLarge1.NumberString = ""    'Nothing

            Case DataSet_Builder.Payment_UC.PanelHit.TipPanel
                Me.NumberPadLarge1.DecimalUsed = True
                Me.NumberPadLarge1.NumberTotal = paymentPanel(paymentRowIndex).PurchaseAmount
                Me.NumberPadLarge1.ShowNumberString()
                Me.NumberPadLarge1.Focus()
                Me.NumberPadLarge1.IntegerNumber = 0
                Me.NumberPadLarge1.NumberString = ""    'Nothing

        End Select

    End Sub

    Private Sub PaymentEnterHit(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumberPadLarge1.NumberEntered
        '
        '   dvUnAppliedPaymentsAndCredits   is zero based
        If paymentRowIndex = 0 Then Exit Sub
        Dim UnAuthPanel As Boolean
        Dim UnAuthIndex As Integer

        If paymentRowIndex > dvClosingCheckPayments.Count Then
            ' we will use:   dvUnAppliedPaymentsAndCredits_MWE 
            UnAuthPanel = True
            UnAuthIndex = paymentRowIndex - dvClosingCheckPayments.Count - 1
            PaymentEnterStep2_UnAuth(UnAuthIndex)
        Else
            PaymentEnterStep2_AlreadyAuth()
        End If

    End Sub

    Private Sub PaymentEnterStep2_AlreadyAuth()
        Dim noTip As Boolean

        Select Case paymentPanel(paymentRowIndex).CurrentState
            Case DataSet_Builder.Payment_UC.PanelHit.PurchasePanel

                If Not dvClosingCheckPayments(paymentRowIndex - 1)("AuthCode") Is DBNull.Value Then Exit Sub

                If dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeID") = -97 Then
                    'this is issue of gift card
                    'we need this to be a liability on Balance sheet, therefore is negative
                    _checkGiftIssuingAmount = _checkGiftIssuingAmount + Me.NumberPadLarge1.NumberTotal - _lastPurchaseIssueAmount
                    _lastPurchaseIssueAmount = _checkGiftIssuingAmount
                    Me.NumberPadLarge1.NumberTotal *= -1
                    noTip = True
                End If
                If GiftAddingAmount = True Then
                    ' for return - which is adding money to gift card
                    dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeID") = -97
                    dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeName") = "Increase Gift"
                    dvClosingCheckPayments(paymentRowIndex - 1)("PaymentFlag") = "Issue"
                    'we can subtract below b/c we are resetting GiftCardAdd to False
                    _checkGiftIssuingAmount = _checkGiftIssuingAmount + Me.NumberPadLarge1.NumberTotal - _lastPurchaseIssueAmount
                    _lastPurchaseIssueAmount = _checkGiftIssuingAmount
                    '        _checkGiftIssuingAmount -= NumberPadLarge1.NumberTotal
                    Me.NumberPadLarge1.NumberTotal *= -1
                    noTip = True
                    ReturnGiftCardAddToFalse()
                    paymentPanel(paymentRowIndex).UpdatePayType("Increase Gift")
                End If

                dvClosingCheckPayments(paymentRowIndex - 1)("PaymentAmount") = Me.NumberPadLarge1.NumberTotal
                paymentPanel(paymentRowIndex).UpdatePurchase(Me.NumberPadLarge1.NumberTotal)

                If noTip = True Then Exit Select
                If currentTable.AutoGratuity > 0 Then
                    Dim adjTip As Decimal = Format(Me.NumberPadLarge1.NumberTotal * companyInfo.autoGratuityPercent, "##,###.00")
                    dvClosingCheckPayments(paymentRowIndex - 1)("Tip") = adjTip
                    paymentPanel(paymentRowIndex).UpdateTip(adjTip)
                End If
                creditAmountAdjusted = True
            Case DataSet_Builder.Payment_UC.PanelHit.TipPanel
                If GiftAddingAmount = True Or dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeID") = -97 Then
                    Exit Select
                End If
                If Me.NumberPadLarge1.NumberTotal > dvClosingCheckPayments(paymentRowIndex - 1)("PaymentAmount") Then
                    If MsgBox("Gratuity Amount is greater than Purchase. Please Verify", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                        Exit Sub
                    End If
                End If
                dvClosingCheckPayments(paymentRowIndex - 1)("Tip") = Me.NumberPadLarge1.NumberTotal
                paymentPanel(paymentRowIndex).UpdateTip(Me.NumberPadLarge1.NumberTotal)
                creditAmountAdjusted = True

            Case DataSet_Builder.Payment_UC.PanelHit.AccountPanel

                If companyInfo.usingOutsideCreditProcessor = True Then Exit Sub
                Dim typeName As String
                Dim ccID As Integer
                '     newPayment.AccountNumber = Me.NumberPadLarge1.NumberString
                'only holding info here
                If NumberPadLarge1.NumberString = "999" Or NumberPadLarge1.NumberString = "9999" Then
                    'this is to force outside credit processor
                    If dvClosingCheckPayments(paymentRowIndex - 1)("AuthCode") Is DBNull.Value Then
                        dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeID") = 9
                        dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeName") = "Outside Credit"
                        dvClosingCheckPayments(paymentRowIndex - 1)("PaymentFlag") = "outside"
                        paymentPanel(paymentRowIndex).UpdatePayType("Outside Credit")
                        Exit Sub
                    End If

                End If

                typeName = DetermineCreditCardName(Me.NumberPadLarge1.NumberString)
                If typeName.Length > 0 Then
                    ccID = DetermineCreditCardID(typeName) 'Me.NumberPadLarge1.NumberString)
                    dvClosingCheckPayments(paymentRowIndex - 1)("AccountNumber") = Me.NumberPadLarge1.NumberString
                    dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeID") = ccID

                    paymentPanel(paymentRowIndex).UpdateAcct(Me.NumberPadLarge1.NumberString)
                    paymentPanel(paymentRowIndex).UpdatePayType(typeName)

                    paymentPanel(paymentRowIndex).CurrentState = DataSet_Builder.Payment_UC.PanelHit.ExpDatePanel
                    Me.NumberPadLarge1.NumberString = "Enter Exp Date "
                    Me.NumberPadLarge1.ShowNumberString()
                    Me.NumberPadLarge1.NumberString = ""
                Else : MsgBox("Can not recognize Card Number. Verify Input.")

                End If

            Case DataSet_Builder.Payment_UC.PanelHit.ExpDatePanel
                If companyInfo.usingOutsideCreditProcessor = True Then Exit Sub
                If Me.NumberPadLarge1.NumberString.Length = 4 Then
                    dvClosingCheckPayments(paymentRowIndex - 1)("CCExpiration") = Me.NumberPadLarge1.NumberString
                    paymentPanel(paymentRowIndex).UpdateExpDate(Me.NumberPadLarge1.NumberString)
                Else
                    MsgBox("Expiration Date Must be in MMYY Format")
                    Me.NumberPadLarge1.NumberString = "Enter Exp Date "
                    Me.NumberPadLarge1.ShowNumberString()
                    Me.NumberPadLarge1.NumberString = ""
                End If

            Case DataSet_Builder.Payment_UC.PanelHit.VoiceAuth
                If companyInfo.usingOutsideCreditProcessor = True Then Exit Sub
                If Me.NumberPadLarge1.NumberString.Length = 6 Then
                    Dim vRow As DataRowView = dvClosingCheckPayments(paymentRowIndex - 1)
                    vRow("AuthCode") = Me.NumberPadLarge1.NumberString
                    paymentPanel(paymentRowIndex).UpdateAuthCode(Me.NumberPadLarge1.NumberString)
                    vRow("Track2") = DBNull.Value
                    '         vRow("AccountNumber") = TruncateAccountNumber(vRow("AccountNumber"))
                    vRow("TransactionCode") = "VoiceAuth"

                    If vRow("RefNum") Is DBNull.Value Then
                        vRow("RefNum") = (currentTable.TruncatedExpNum).ToString
                    End If

                    If vRow("Duplicate") = True Then
                        vRow("RefNum") = (currentTable.TruncatedExpNum & "-" & (dvAppliedPayments.Count + 1)).ToString
                    End If

                    PrintCreditCardReceipt(Nothing, vRow, True)

                    RemainingBalance -= vRow("PaymentAmount")
                    '            paymentRowIndex -= 1
                    '             unappliedRowIndex -= 1

                    vRow("Applied") = True
                    Me.closeCheckTotals.AttachTotalsToTotalView(currentTable.CheckNumber)
                    GetNumberOfActivePayments(currentTable.CheckNumber)
                End If
        End Select

        ShowRemainingBalance()
    End Sub


    Private Sub PaymentEnterStep2_UnAuth(ByVal UnAuthIndex As Integer)
        Dim noTip As Boolean

        Select Case paymentPanel(paymentRowIndex).CurrentState
            Case DataSet_Builder.Payment_UC.PanelHit.PurchasePanel

                If Not dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("AuthCode") Is DBNull.Value Then Exit Sub

                If dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("PaymentTypeID") = -97 Then
                    'this is issue of gift card
                    'we need this to be a liability on Balance sheet, therefore is negative
                    _checkGiftIssuingAmount = _checkGiftIssuingAmount + Me.NumberPadLarge1.NumberTotal - _lastPurchaseIssueAmount
                    _lastPurchaseIssueAmount = _checkGiftIssuingAmount
                    Me.NumberPadLarge1.NumberTotal *= -1
                    noTip = True
                End If
                If GiftAddingAmount = True Then
                    ' for return - which is adding money to gift card
                    dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("PaymentTypeID") = -97
                    dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("PaymentTypeName") = "Increase Gift"
                    dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("PaymentFlag") = "Issue"
                    'we can subtract below b/c we are resetting GiftCardAdd to False
                    _checkGiftIssuingAmount = _checkGiftIssuingAmount + Me.NumberPadLarge1.NumberTotal - _lastPurchaseIssueAmount
                    _lastPurchaseIssueAmount = _checkGiftIssuingAmount
                    '        _checkGiftIssuingAmount -= NumberPadLarge1.NumberTotal
                    Me.NumberPadLarge1.NumberTotal *= -1
                    noTip = True
                    ReturnGiftCardAddToFalse()
                    paymentPanel(paymentRowIndex).UpdatePayType("Increase Gift")
                End If

                dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("PaymentAmount") = Me.NumberPadLarge1.NumberTotal
                paymentPanel(paymentRowIndex).UpdatePurchase(Me.NumberPadLarge1.NumberTotal)

                If noTip = True Then Exit Select
                If currentTable.AutoGratuity > 0 Then
                    Dim adjTip As Decimal = Format(Me.NumberPadLarge1.NumberTotal * companyInfo.autoGratuityPercent, "##,###.00")
                    dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("Tip") = adjTip
                    paymentPanel(paymentRowIndex).UpdateTip(adjTip)
                End If
                creditAmountAdjusted = True
            Case DataSet_Builder.Payment_UC.PanelHit.TipPanel
                If GiftAddingAmount = True Or dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("PaymentTypeID") = -97 Then
                    Exit Select
                End If
                If Me.NumberPadLarge1.NumberTotal > dvUnAppliedPaymentsAndCredits_MWE(paymentRowIndex - 1)("PaymentAmount") Then
                    If MsgBox("Gratuity Amount is greater than Purchase. Please Verify", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                        Exit Sub
                    End If
                End If
                dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("Tip") = Me.NumberPadLarge1.NumberTotal
                paymentPanel(paymentRowIndex).UpdateTip(Me.NumberPadLarge1.NumberTotal)
                creditAmountAdjusted = True

            Case DataSet_Builder.Payment_UC.PanelHit.AccountPanel

                If companyInfo.usingOutsideCreditProcessor = True Then Exit Sub
                Dim typeName As String
                Dim ccID As Integer
                '     newPayment.AccountNumber = Me.NumberPadLarge1.NumberString
                'only holding info here
                If NumberPadLarge1.NumberString = "999" Or NumberPadLarge1.NumberString = "9999" Then
                    'this is to force outside credit processor
                    If dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("AuthCode") Is DBNull.Value Then
                        dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("PaymentTypeID") = 9
                        dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("PaymentTypeName") = "Outside Credit"
                        dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("PaymentFlag") = "outside"
                        paymentPanel(paymentRowIndex).UpdatePayType("Outside Credit")
                        Exit Sub
                    End If

                End If

                typeName = DetermineCreditCardName(Me.NumberPadLarge1.NumberString)
                If typeName.Length > 0 Then
                    ccID = DetermineCreditCardID(typeName) 'Me.NumberPadLarge1.NumberString)
                    dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("AccountNumber") = Me.NumberPadLarge1.NumberString
                    dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("PaymentTypeID") = ccID

                    paymentPanel(paymentRowIndex).UpdateAcct(Me.NumberPadLarge1.NumberString)
                    paymentPanel(paymentRowIndex).UpdatePayType(typeName)

                    paymentPanel(paymentRowIndex).CurrentState = DataSet_Builder.Payment_UC.PanelHit.ExpDatePanel
                    Me.NumberPadLarge1.NumberString = "Enter Exp Date "
                    Me.NumberPadLarge1.ShowNumberString()
                    Me.NumberPadLarge1.NumberString = ""
                Else : MsgBox("Can not recognize Card Number. Verify Input.")

                End If

            Case DataSet_Builder.Payment_UC.PanelHit.ExpDatePanel
                If companyInfo.usingOutsideCreditProcessor = True Then Exit Sub
                If Me.NumberPadLarge1.NumberString.Length = 4 Then
                    dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("CCExpiration") = Me.NumberPadLarge1.NumberString
                    paymentPanel(paymentRowIndex).UpdateExpDate(Me.NumberPadLarge1.NumberString)
                Else
                    MsgBox("Expiration Date Must be in MMYY Format")
                    Me.NumberPadLarge1.NumberString = "Enter Exp Date "
                    Me.NumberPadLarge1.ShowNumberString()
                    Me.NumberPadLarge1.NumberString = ""
                End If

            Case DataSet_Builder.Payment_UC.PanelHit.VoiceAuth
                If companyInfo.usingOutsideCreditProcessor = True Then Exit Sub
                If Me.NumberPadLarge1.NumberString.Length = 6 Then
                    Dim vRow As DataRowView = dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)
                    vRow("AuthCode") = Me.NumberPadLarge1.NumberString
                    paymentPanel(paymentRowIndex).UpdateAuthCode(Me.NumberPadLarge1.NumberString)
                    '    vRow("Track2") = DBNull.Value
                    '         vRow("AccountNumber") = TruncateAccountNumber(vRow("AccountNumber"))
                    vRow("TransactionCode") = "VoiceAuth"

                    If vRow("RefNum") Is DBNull.Value Then
                        vRow("RefNum") = (currentTable.TruncatedExpNum).ToString
                    End If

                    If vRow("Duplicate") = True Then
                        vRow("RefNum") = (currentTable.TruncatedExpNum & "-" & (dvAppliedPayments.Count + 1)).ToString
                    End If

                    PrintCreditCardReceipt(Nothing, vRow, True)

                    RemainingBalance -= vRow("PaymentAmount")
                    '            paymentRowIndex -= 1
                    '             unappliedRowIndex -= 1

                    vRow("Applied") = True
                    Me.closeCheckTotals.AttachTotalsToTotalView(currentTable.CheckNumber)
                    GetNumberOfActivePayments(currentTable.CheckNumber)
                End If
        End Select

        ShowRemainingBalance()
    End Sub


    Private Sub PaymentEnterHit_Copy222(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles NumberPadLarge1.NumberEntered
        '
        '   dvUnAppliedPaymentsAndCredits   is zero based
        If paymentRowIndex = 0 Then Exit Sub
        '     Dim newPayment As New Payment  'this is only a temp holding structure
        Dim noTip As Boolean
        Dim UnAuthPanel As Boolean
        Dim UnAuthIndex As Integer

        If paymentRowIndex > dvClosingCheckPayments.Count Then
            UnAuthPanel = True
            UnAuthIndex = paymentRowIndex - dvClosingCheckPayments.Count - 1
        End If

        Select Case paymentPanel(paymentRowIndex).CurrentState
            Case DataSet_Builder.Payment_UC.PanelHit.PurchasePanel

                If Not dvClosingCheckPayments(paymentRowIndex - 1)("AuthCode") Is DBNull.Value Then Exit Sub

                If dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeID") = -97 Then
                    'this is issue of gift card
                    'we need this to be a liability on Balance sheet, therefore is negative
                    _checkGiftIssuingAmount = _checkGiftIssuingAmount + Me.NumberPadLarge1.NumberTotal - _lastPurchaseIssueAmount
                    _lastPurchaseIssueAmount = _checkGiftIssuingAmount
                    Me.NumberPadLarge1.NumberTotal *= -1
                    noTip = True
                End If
                If GiftAddingAmount = True Then
                    ' for return - which is adding money to gift card
                    dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeID") = -97
                    dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeName") = "Increase Gift"
                    dvClosingCheckPayments(paymentRowIndex - 1)("PaymentFlag") = "Issue"
                    'we can subtract below b/c we are resetting GiftCardAdd to False
                    _checkGiftIssuingAmount = _checkGiftIssuingAmount + Me.NumberPadLarge1.NumberTotal - _lastPurchaseIssueAmount
                    _lastPurchaseIssueAmount = _checkGiftIssuingAmount
                    '        _checkGiftIssuingAmount -= NumberPadLarge1.NumberTotal
                    Me.NumberPadLarge1.NumberTotal *= -1
                    noTip = True
                    ReturnGiftCardAddToFalse()
                    paymentPanel(paymentRowIndex).UpdatePayType("Increase Gift")

                End If

                dvClosingCheckPayments(paymentRowIndex - 1)("PaymentAmount") = Me.NumberPadLarge1.NumberTotal
                paymentPanel(paymentRowIndex).UpdatePurchase(Me.NumberPadLarge1.NumberTotal)

                If noTip = True Then Exit Select
                If currentTable.AutoGratuity > 0 Then
                    Dim adjTip As Decimal = Format(Me.NumberPadLarge1.NumberTotal * companyInfo.autoGratuityPercent, "##,###.00")
                    dvClosingCheckPayments(paymentRowIndex - 1)("Tip") = adjTip
                    paymentPanel(paymentRowIndex).UpdateTip(adjTip)
                End If
                creditAmountAdjusted = True
            Case DataSet_Builder.Payment_UC.PanelHit.TipPanel
                If GiftAddingAmount = True Or dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeID") = -97 Then
                    Exit Select
                End If
                dvClosingCheckPayments(paymentRowIndex - 1)("Tip") = Me.NumberPadLarge1.NumberTotal
                paymentPanel(paymentRowIndex).UpdateTip(Me.NumberPadLarge1.NumberTotal)
                creditAmountAdjusted = True

            Case DataSet_Builder.Payment_UC.PanelHit.AccountPanel

                If companyInfo.usingOutsideCreditProcessor = True Then Exit Sub
                Dim typeName As String
                Dim ccID As Integer
                '     newPayment.AccountNumber = Me.NumberPadLarge1.NumberString
                'only holding info here
                If NumberPadLarge1.NumberString = "999" Or NumberPadLarge1.NumberString = "9999" Then
                    'this is to force outside credit processor
                    If dvClosingCheckPayments(paymentRowIndex - 1)("AuthCode") Is DBNull.Value Then
                        dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeID") = 9
                        dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeName") = "Outside Credit"
                        dvClosingCheckPayments(paymentRowIndex - 1)("PaymentFlag") = "outside"
                        paymentPanel(paymentRowIndex).UpdatePayType("Outside Credit")
                        Exit Sub
                    End If

                End If

                typeName = DetermineCreditCardName(Me.NumberPadLarge1.NumberString)
                If typeName.Length > 0 Then
                    ccID = DetermineCreditCardID(typeName) 'Me.NumberPadLarge1.NumberString)
                    dvClosingCheckPayments(paymentRowIndex - 1)("AccountNumber") = Me.NumberPadLarge1.NumberString
                    dvClosingCheckPayments(paymentRowIndex - 1)("PaymentTypeID") = ccID

                    paymentPanel(paymentRowIndex).UpdateAcct(Me.NumberPadLarge1.NumberString)
                    paymentPanel(paymentRowIndex).UpdatePayType(typeName)

                    paymentPanel(paymentRowIndex).CurrentState = DataSet_Builder.Payment_UC.PanelHit.ExpDatePanel
                    Me.NumberPadLarge1.NumberString = "Enter Exp Date "
                    Me.NumberPadLarge1.ShowNumberString()
                    Me.NumberPadLarge1.NumberString = ""
                Else : MsgBox("Can not recognize Card Number. Verify Input.")

                End If

            Case DataSet_Builder.Payment_UC.PanelHit.ExpDatePanel
                If companyInfo.usingOutsideCreditProcessor = True Then Exit Sub
                If Me.NumberPadLarge1.NumberString.Length = 4 Then
                    dvClosingCheckPayments(paymentRowIndex - 1)("CCExpiration") = Me.NumberPadLarge1.NumberString
                    paymentPanel(paymentRowIndex).UpdateExpDate(Me.NumberPadLarge1.NumberString)
                Else
                    MsgBox("Expiration Date Must be in MMYY Format")
                    Me.NumberPadLarge1.NumberString = "Enter Exp Date "
                    Me.NumberPadLarge1.ShowNumberString()
                    Me.NumberPadLarge1.NumberString = ""
                End If

            Case DataSet_Builder.Payment_UC.PanelHit.VoiceAuth
                If companyInfo.usingOutsideCreditProcessor = True Then Exit Sub
                If Me.NumberPadLarge1.NumberString.Length = 6 Then
                    Dim vRow As DataRowView = dvClosingCheckPayments(paymentRowIndex - 1)
                    vRow("AuthCode") = Me.NumberPadLarge1.NumberString
                    paymentPanel(paymentRowIndex).UpdateAuthCode(Me.NumberPadLarge1.NumberString)
                    vRow("Track2") = DBNull.Value
                    '         vRow("AccountNumber") = TruncateAccountNumber(vRow("AccountNumber"))
                    vRow("TransactionCode") = "VoiceAuth"

                    If vRow("RefNum") Is DBNull.Value Then
                        vRow("RefNum") = (currentTable.TruncatedExpNum).ToString
                    End If

                    If vRow("Duplicate") = True Then
                        vRow("RefNum") = (currentTable.TruncatedExpNum & "-" & (dvAppliedPayments.Count + 1)).ToString
                    End If

                    PrintCreditCardReceipt(Nothing, vRow, True)

                    RemainingBalance -= vRow("PaymentAmount")
                    '            paymentRowIndex -= 1
                    '             unappliedRowIndex -= 1

                    vRow("Applied") = True
                    Me.closeCheckTotals.AttachTotalsToTotalView(currentTable.CheckNumber)
                    GetNumberOfActivePayments(currentTable.CheckNumber)
                End If


        End Select

        ShowRemainingBalance()

    End Sub

    Private Sub btnClose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose1.Click

        AddAutoCash(1)

    End Sub

    Private Sub btnClose5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose5.Click
        AddAutoCash(5)

    End Sub

    Private Sub btnClose10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose10.Click
        AddAutoCash(10)
    End Sub

    Private Sub btnClose20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose20.Click
        AddAutoCash(20)

    End Sub

    Private Sub btnClose50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose50.Click
        AddAutoCash(50)

    End Sub

    Private Sub btnClose100_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose100.Click
        AddAutoCash(100)

    End Sub

    Private Sub btnCloseCash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseCash.Click

        CashButtonClicked()

    End Sub

    Friend Sub CashButtonClicked()
        Dim amount As Decimal

        amount = DetermineCashToCredit()
        AddAutoCash(amount)

    End Sub

    Private Sub btnCloseManualcc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseManualcc.Click

        Dim newPayment As DataSet_Builder.Payment
        Dim amount As Decimal

        amount = DetermineCashToCredit()
        newPayment.Purchase = Format(amount, "##,##0.00")
        If companyInfo.usingOutsideCreditProcessor = False Then
            newPayment.PaymentTypeID = 0
        Else
            newPayment.PaymentTypeID = 9
            newPayment.TranCode = "Credit"  'this is outside credit

        End If

        newPayment.PaymentTypeName = "cc"   '"Enter Acct #"
        newPayment.RefNo = currentTable.ExperienceNumber.ToString
        newPayment.AccountNumber = "Manual"
        newPayment.ExpDate = "MMYY"
        'NULL says kayed   newPayment.Track2 = ""
        newPayment.Name = currentTable.TabName

        CreateNewPaymentEntry(newPayment, False)
        ShowRemainingBalance()

        NumberPadLarge1.DecimalUsed = False
        If companyInfo.usingOutsideCreditProcessor = False Then
            NumberPadLarge1.NumberString = "Enter Acct Number "
        End If

        NumberPadLarge1.ShowNumberString()
        NumberPadLarge1.NumberString = ""
        paymentPanel(paymentRowIndex).CurrentState = DataSet_Builder.Payment_UC.PanelHit.AccountPanel

    End Sub

    Private Function DetermineCashToCredit()
        Dim creditCashAmount As Decimal
        Dim vRow As DataRowView

        creditCashAmount = RemainingBalance

        For Each vRow In dvUnAppliedPaymentsAndCredits
            creditCashAmount -= vRow("PaymentAmount")
        Next

        Return creditCashAmount

    End Function

    Private Function DetermineAutomaticCreditCharge()
        Dim creditChargeAmount As Decimal
        Dim vRow As DataRowView
        Dim numberOfCreditCardsUsed As Integer
        Dim DollarAmountToChargeCreditCards As Decimal

        DollarAmountToChargeCreditCards = RemainingBalance

        For Each vRow In dvUnAppliedPaymentsAndCredits_MWE
            If vRow("PaymentFlag") = "Cash" Then
                DollarAmountToChargeCreditCards -= vRow("PaymentAmount")
            ElseIf vRow("PaymentFlag") = "Issue" Then
                DollarAmountToChargeCreditCards -= vRow("PaymentAmount")
            Else
                numberOfCreditCardsUsed += 1
            End If
        Next

        For Each vRow In dvUnAppliedPaymentsAndCredits
            If vRow("PaymentFlag") = "Cash" Then
                DollarAmountToChargeCreditCards -= vRow("PaymentAmount")
            ElseIf vRow("PaymentFlag") = "Issue" Then
                DollarAmountToChargeCreditCards -= vRow("PaymentAmount")
            Else
                numberOfCreditCardsUsed += 1
            End If
        Next

        If Not dvUnAppliedPaymentsAndCredits_MWE.Count > 0 Then
            '   this adds the new one, for MWE row already added in MWE routine
            numberOfCreditCardsUsed += 1
        End If

        creditChargeAmount = Format((DollarAmountToChargeCreditCards / numberOfCreditCardsUsed), "####0.00")
        roundingError = Format((DollarAmountToChargeCreditCards - (creditChargeAmount * numberOfCreditCardsUsed)), "####0.00")

        Return creditChargeAmount

    End Function

    Private Sub ApplyAutomaticCreditCharge(ByVal newChargeAmount As Decimal)
        Dim vrow As DataRowView

        For Each vrow In dvUnAppliedPaymentsAndCredits
            If vrow("PaymentFlag") = "cc" Then
                If roundingError = 0 Then
                    vrow("PaymentAmount") = newChargeAmount
                Else
                    vrow("PaymentAmount") = newChargeAmount + roundingError
                    roundingError = 0
                End If
                If currentTable.AutoGratuity > 0 Then
                    Dim adjTip As Decimal = Format(newChargeAmount * companyInfo.autoGratuityPercent, "##,###.00")
                    vrow("Tip") = adjTip
                End If
            End If
        Next

    End Sub
    Private Sub ApplyAutomaticCreditCharge_MWE(ByVal newChargeAmount As Decimal)
        Dim vrow As DataRowView

        For Each vrow In dvUnAppliedPaymentsAndCredits_MWE
            If vrow("PaymentFlag") = "cc" Then
                If roundingError = 0 Then
                    vrow("PaymentAmount") = newChargeAmount
                Else
                    vrow("PaymentAmount") = newChargeAmount + roundingError
                    roundingError = 0
                End If
                If currentTable.AutoGratuity > 0 Then
                    Dim adjTip As Decimal = Format(newChargeAmount * companyInfo.autoGratuityPercent, "##,###.00")
                    vrow("Tip") = adjTip
                End If
            End If
        Next

    End Sub

    Private Sub RunClosingPrint()

        CreateClosingDataViews(currentTable.CheckNumber, True)

        prt.closeDetail.dvClosing = dvOrder 'dvClosingCheck
        prt.closeDetail.chkSubTotal = closeCheckTotals.chkSubTotal
        '      prt.closeDetail.checkTax = closeCheckTotals.checkTax   
        prt.closeDetail.chktaxName = closeCheckTotals.taxName
        prt.closeDetail.chktaxTotal = closeCheckTotals.taxTotal

    End Sub
    Private Sub btnClosePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePrint.Click
        ResetTimer()

        '   need to update ExperienceStatusChange
        GenerateOrderTables.ChangeStatusInDataBase(7, Nothing, 0, Nothing, Now, Nothing)
        '222     AddStatusChangeData(currentTable.ExperienceNumber, 7, Nothing, 0, Nothing)

        RunClosingPrint()
        prt.StartPrintCheckReceipt()

        '   *** need to redo with PaymentFlag
        Dim vRow As DataRowView
        For Each vRow In dvUnAppliedPaymentsAndCredits
            If Not vRow("PaymentTypeName") = "Cash" Then
                doNotAutoCreditCards = True
            End If
        Next

        If companyInfo.autoCloseCheck = True Then
            Dim newPayment As DataSet_Builder.Payment

            newPayment.Purchase = DetermineCashToCredit()
            newPayment.Purchase = Format(newPayment.Purchase, "##,##0.00")
            newPayment.PaymentTypeID = 1
            newPayment.PaymentTypeName = "Cash"
            CreateNewPaymentEntry(newPayment, True)
            DisposeObjects()
            RaiseEvent CloseExitAndRelease()    'test remaining balance

            If RemainingBalancesZero = True Then
                CalculateClosingTotal()
                releaseFlag = True
                DisposeObjects()
                RaiseEvent CloseExiting(False, RemainingBalancesZero)
                '                GenerateOrderTables.ReleaseTableOrTab()
                '               CloseDisposeSelf()
            Else
                ShowRemainingBalance()
            End If
        End If

    End Sub

    Private Sub btnClosePrintAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePrintAll.Click
        ResetTimer()
        '   when we print all checks
        '   we change status to check down
        '   this will change the color coding and now allow for hostess or server to 
        '   make table avail and change table status to 1(Avail for seating)

        Dim i As Integer
        Dim newPayment As DataSet_Builder.Payment

        GenerateOrderTables.ChangeStatusInDataBase(7, Nothing, 0, Nothing, Now, Nothing)

        ' the following is an attempt to print all one check
        ' then do the individual's
        ' but I need to make sure the subtotals are correct
        '        prt.closeDetail.dvClosing = dvOrder
        '        prt.closeDetail.chkSubTotal = closeCheckTotals.chkSubTotal
        '        prt.closeDetail.chktaxName = closeCheckTotals.taxName
        '        prt.closeDetail.chktaxTotal = closeCheckTotals.taxTotal
        '        prt.StartPrintCheckReceipt()

        For i = 1 To currentTable.NumberOfChecks
            currentTable.CheckNumber = i
            CreateClosingDataViews(currentTable.CheckNumber, True)
            Me.closeCheckTotals.CalculatePriceAndTax(currentTable.CheckNumber)
            prt.closeDetail.dvClosing = dvClosingCheck  'dvOrder 'dvClosingCheck
            prt.closeDetail.chkSubTotal = closeCheckTotals.chkSubTotal
            prt.closeDetail.chktaxName = closeCheckTotals.taxName
            prt.closeDetail.chktaxTotal = closeCheckTotals.taxTotal
            prt.StartPrintCheckReceipt()
        Next

        If companyInfo.autoCloseCheck = True Then

            newPayment.Purchase = DetermineCashToCredit()
            newPayment.Purchase = Format(newPayment.Purchase, "##,##0.00")
            newPayment.PaymentTypeID = 1
            newPayment.PaymentTypeName = "Cash"
            CreateNewPaymentEntry(newPayment, True)

            '   if we are auto closing all balances will be zero, therefore we release table
            CalculateClosingTotal()
            releaseFlag = True
            DisposeObjects()
            RaiseEvent CloseExiting(False, RemainingBalancesZero)
            '            GenerateOrderTables.ReleaseTableOrTab()
            '           CloseDisposeSelf()

        End If


    End Sub

    Friend Sub PrintingFromFastCash(ByVal payBalanceAmount As Decimal)
        CreateClosingDataViews(currentTable.CheckNumber, True)

        prt.closeDetail.dvClosing = dvOrder 'dvClosingCheck
        prt.closeDetail.chkSubTotal = closeCheckTotals.chkSubTotal
        '      prt.closeDetail.checkTax = closeCheckTotals.checkTax    
        prt.closeDetail.chktaxName = closeCheckTotals.taxName
        prt.closeDetail.chktaxTotal = closeCheckTotals.taxTotal

        prt.closeDetail.isCashTendered = True
        prt.closeDetail.cashTendered = payBalanceAmount
        prt.closeDetail.chkChangeDue = 0 'RemainingBalance

        prt.StartPrintCheckReceipt() '(dvClosingCheck, closeCheckTotals.chkSubTotal, closeCheckTotals.checkTax)

    End Sub

    Private Sub UpdateCheckNumberButton()
        Me.btnCloseCheckNumber.Text = "Check   " & currentTable.CheckNumber & " of " & currentTable.NumberOfChecks 'currentTable._checkCollection.Count

    End Sub

    Private Sub btnCloseCheckNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseCheckNumber.Click
        ResetTimer()

        GenerateOrderTables.GotoNextCheckNumber()
        ReinitializeCloseCheck(True)

        '   need to add more to update screen
        '   redo datasource
    End Sub

    Friend Sub ReinitializeCloseCheck(ByVal repopulatePayments As Boolean)

        Me.pnlClosePayments.Controls.Clear()
        CreateClosingDataViews(currentTable.CheckNumber, True)
        dvOrder.RowFilter = "CheckNumber ='" & currentTable.CheckNumber & "'"
        Me.closeCheckTotals.PopulateCloseGrid(dvOrder) 'dvClosingCheck)
        '      paymentRowIndex = dvClosingCheckPayments.Count
        startPaymentIndex = 1

        '444      repopulatePayments = True
        If repopulatePayments = True Then
            GetNumberOfActivePayments(currentTable.CheckNumber)
            '         DisplayAnyStoredPayments()
        End If

        'could probably consolidate these two
        Me.closeCheckTotals.CalculatePriceAndTax(currentTable.CheckNumber)
        RemainingBalance = closeCheckTotals.AttachTotalsToTotalView(currentTable.CheckNumber)
        TotalCheckBalance = closeCheckTotals.TotalCheckBalance

        Me.NumberPadLarge1.DecimalUsed = True
        Me.NumberPadLarge1.NumberTotal = 0
        Me.NumberPadLarge1.IntegerNumber = 0
        Me.NumberPadLarge1.NumberString = ""
        Me.NumberPadLarge1.ShowNumberString()

        prt.closeDetail.isCashTendered = False
        prt.closeDetail.cashTendered = 0
        prt.closeDetail.chkChangeDue = 0
        prt.closeDetail.cashAppliedPrevious = 0

     

        ReturnGiftCardAddToFalse()

        UpdateCheckNumberButton()
        ShowRemainingBalance()

        doNotAutoCreditCards = False
        creditAmountAdjusted = False
        _remainingBalancesZero = False
        _giftAddingAmount = False

        'not sure about PromotionApplied and _giftAddingAmount
        'all other somewhere else

        '       Dim PromotionApplied(20) As Boolean
        '      Dim roundingError As Decimal
        '     Dim _remainingBalancesZero As Boolean
        '   Dim _checkGiftIssuingAmount As Decimal
        'above will be a positive number, when paymentAmount will be negative for issuing gift
        '   Dim _lastPurchaseIssueAmount As Decimal
        '        Dim numActivePaymentsByCheck As Integer
        '       Dim _remainingBalance As Decimal     'on both
        '      Dim _totalCheckBalance As Decimal
        '    _giftAddingAmount = False

    End Sub

    Private Sub btnCloseRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRelease.Click

        ClosingAndReleaseRoutine(True)

    End Sub

    Friend Sub ClosingAndReleaseRoutine(ByVal isFromRelease As Boolean)
        ResetTimer()

        Dim oRow As DataRow
        Dim unappliedCashAmount As Decimal
        Dim vRow As DataRowView
        Dim notAllCash As Boolean
        Dim someUnApplied As Boolean
        Dim goingToSelectedCheck As Boolean = False
        '      GenerateOrderTables.SaveOpenOrderData()

        '   if there is only a cash payment for the whole amount
        '   we apply it and therefore can release table
        '     If dvUnAppliedPaymentsAndCredits.Count <= currentTable.NumberOfChecks Then  '1 Then

        If currentTerminal.TermMethod = "Quick" Then
            goingToSelectedCheck = True
        End If
        Try
            For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows   'dvUnAppliedPaymentsAndCredits
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("Applied") = False Then
                        someUnApplied = True
                        If oRow("PaymentFlag") = "Cash" Then
                            unappliedCashAmount += oRow("PaymentAmount")
                        Else
                            notAllCash = True
                            Exit For
                        End If
                    End If
                End If
            Next

            If someUnApplied = True And notAllCash = False Then      'means it is all cash

                If (RemainingBalance - unappliedCashAmount) < 0.02 And (RemainingBalance - unappliedCashAmount) > -0.02 Then

                    RemainingBalancesZero = True

                    For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows   'dvUnAppliedPaymentsAndCredits
                        If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                            If oRow("Applied") = False Then
                                oRow("Applied") = True
                            End If
                        End If
                    Next

                    If companyInfo.autoPrint = True Then
                        RunClosingPrint()
                        prt.closeDetail.isCashTendered = True
                        prt.closeDetail.cashTendered = unappliedCashAmount 'reducePaymentAmount
                        prt.closeDetail.chkChangeDue = 0 'RemainingBalance
                        prt.StartPrintCheckReceipt()
                    Else
                        prt.PrintOpenCashDrawer()
                    End If

                    If currentTable.NumberOfChecks = 1 Then
                        releaseFlag = True
                        currentTable.IsClosed = True

                        DisposeObjects()
                        RaiseEvent CloseExiting(goingToSelectedCheck, RemainingBalancesZero)
                        Exit Sub
                    End If

                Else
                    If isFromRelease = False Then
                        releaseFlag = False
                        DisposeObjects()
                        RaiseEvent CloseExiting(goingToSelectedCheck, RemainingBalancesZero)
                    Else
                        Dim info As DataSet_Builder.Information_UC
                        info = New DataSet_Builder.Information_UC("You must authorize your Cash Payment")
                        info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
                        Me.Controls.Add(info)
                        info.BringToFront()
                    End If
                    Exit Sub

                End If


            End If

        Catch ex As Exception

        End Try
        '   End If


        If currentTable.NumberOfChecks > 1 Then

            Dim i As Integer
            Dim tempCheckNumber As Integer
            Dim tempRemainingBalance As Decimal
            Dim checkCount As Integer
            Dim maxCN As Integer

            If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
                maxCN = (dsOrder.Tables("OpenOrders")).Compute("Max(CheckNumber)", Nothing)


                For i = 1 To maxCN   ' currentTable.NumberOfChecks
                    checkCount = DetermineCheckCount(i)
                    If checkCount > 0 Then
                        closeCheckTotals.CalculatePriceAndTax(i)
                        tempRemainingBalance += closeCheckTotals.AttachTotalsToTotalView(i)
                    End If
                Next
                If (tempRemainingBalance - unappliedCashAmount) < 0.02 And (tempRemainingBalance - unappliedCashAmount) > -0.02 Then
                    RemainingBalancesZero = True
                End If
            End If


          
        Else
            If (RemainingBalance - unappliedCashAmount) < 0.02 And (RemainingBalance - unappliedCashAmount) > -0.02 Then
                RemainingBalancesZero = True
            End If
        End If

        If isFromRelease = False Then
            releaseFlag = False
            DisposeObjects()
            RaiseEvent CloseExiting(goingToSelectedCheck, RemainingBalancesZero)
            Exit Sub
        End If

        '       If dsOrder.Tables("PaymentsAndCredits").Rows.Count And dsOrder.Tables("OpenOrders").Rows.Count = 0 Then
        '      'this is if we have not ordered anything yet and no payments
        ' can turn on later, for now letting them release table
        '     releaseFlag = False
        '    RaiseEvent CloseExiting(False)
        '   Exit Sub
        '  End If
        '      RemainingBalancesZero = TestRemainingBalances()
        'below this line is only for Release

        If RemainingBalancesZero = False Then

            Dim info As DataSet_Builder.Information_UC
            info = New DataSet_Builder.Information_UC("There is still a balance remaining for at least one check or you have not authorized a card payment.")
            info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            Me.Controls.Add(info)
            info.BringToFront()
            Exit Sub

            'not using below right now
            If MsgBox("There is still a balance remaining. Are you sure you would like to close the table?", MsgBoxStyle.YesNo, ) = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

        If dvUnAppliedPaymentsAndCredits.Count > 0 Then
            'we are removing any unused payment when releasin
            CheckForUnappliedCredit(True)
        End If

        CalculateClosingTotal()
        releaseFlag = True
        'did not work for quick ticket update   
        currentTable.IsClosed = True
        DisposeObjects()
        RaiseEvent CloseExiting(goingToSelectedCheck, RemainingBalancesZero)

    End Sub

    Friend Sub CheckForUnappliedCredit(ByVal removeCash As Boolean)
        '  Exit Function

        Dim oRow As DataRow
        Dim isUnappliedCredit As Boolean
        Dim unappliedCashAmount As Decimal
        '      Dim cashIndex As Integer
        '     Dim cashArray As New ArrayList  'Integer  'will only have one 
        '    Dim isCashPayment As Boolean
        Dim i As Integer = 0
        Dim deleteIndex As Integer

        '    cashIndex = -1      'currently we can only flag one check for cash

        '*** still have a problem, for some reason sometimes the system removes a deleted row
        '   other times it keeps it in the dataset
        '**** also don't think we use the first loop
        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows    'dvUnAppliedPaymentsAndCredits
            If Not dsOrder.Tables("PaymentsAndCredits").Rows(deleteIndex).RowState = DataRowState.Deleted And Not dsOrder.Tables("PaymentsAndCredits").Rows(deleteIndex).RowState = DataRowState.Detached Then
                ' not sure why we are using above instad of below
                '   If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("Applied") = False Then
                    If oRow("PaymentFlag") = "Cash" Then
                        '                   cashArray.Add(i) ' = i
                        unappliedCashAmount += oRow("PaymentAmount")
                    Else 'If oRow("PaymentFlag") = "cc" Then 
                        isUnappliedCredit = True
                    End If
                End If
                '     i += 1
                deleteIndex += 1
            End If
        Next

        i = 0
        deleteIndex = 0

        If isUnappliedCredit = True Then
            '        If MsgBox("FOR SECURITY - All unauthorized Credit Card information will be lost. Continue to Exit?", MsgBoxStyle.OKCancel) = MsgBoxResult.Cancel Then
            '       Return True  'true means "DO NOT EXIT"
            '  Else
            'this removes all unapplied credit card information 
            For i = 0 To (dsOrder.Tables("PaymentsAndCredits").Rows.Count - 1)
                If Not dsOrder.Tables("PaymentsAndCredits").Rows(deleteIndex).RowState = DataRowState.Deleted And Not dsOrder.Tables("PaymentsAndCredits").Rows(deleteIndex).RowState = DataRowState.Detached Then
                    If dsOrder.Tables("PaymentsAndCredits").Rows(deleteIndex)("Applied") = False Then
                        If Not dsOrder.Tables("PaymentsAndCredits").Rows(deleteIndex)("PaymentFlag") = "Cash" Then
                            dsOrder.Tables("PaymentsAndCredits").Rows(deleteIndex).Delete()
                        Else                             'below is a cash row
                            If removeCash = True Then
                                dsOrder.Tables("PaymentsAndCredits").Rows(deleteIndex).Delete()
                            Else
                                If unappliedCashAmount = RemainingBalance Then
                                    dsOrder.Tables("PaymentsAndCredits").Rows(deleteIndex)("Applied") = True
                                End If
                                deleteIndex += 1
                            End If
                        End If
                    Else
                        deleteIndex += 1
                    End If
                End If
            Next
        End If
        ' End If

    End Sub

    Friend Sub ShowRemainingBalance()
        Dim unauthorizedRemainingBalance As Decimal
        Dim vrow As DataRowView

        unauthorizedRemainingBalance = RemainingBalance

        For Each vrow In dvUnAppliedPaymentsAndCredits_MWE
            unauthorizedRemainingBalance -= vrow("PaymentAmount")
        Next
        For Each vrow In dvUnAppliedPaymentsAndCredits
            unauthorizedRemainingBalance -= vrow("PaymentAmount")
        Next
        Me.lblBalanceDetail.Text = Format(unauthorizedRemainingBalance, "####0.00")

    End Sub

    Private Sub btnCloseComp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseMgr.Click
        ResetTimer()
        MsgBox("Service Not Available")
    End Sub

    Private Sub btnCloseAutoTip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseAutoTip.Click
        ResetTimer()

        If closeCheckTotals.AutoGratuity = False Then
            closeCheckTotals.AutoGratuity = True
            ChangeAutoGratInExperience(companyInfo.autoGratuityPercent)
        Else
            closeCheckTotals.AutoGratuity = False
            ChangeAutoGratInExperience(-1)
        End If

        closeCheckTotals.CalculatePriceAndTax(currentTable.CheckNumber)
        RemainingBalance = closeCheckTotals.AttachTotalsToTotalView(currentTable.CheckNumber)
        ShowRemainingBalance()

    End Sub

    Private Sub ChangeAutoGratInExperience(ByVal autoGrat As Decimal)

        Dim oRow As DataRow
        Dim vrow As DataRowView

        If currentTerminal.TermMethod = "Quick" Or currentTable.TicketNumber > 0 Then
            For Each oRow In dsOrder.Tables("QuickTickets").Rows
                If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                    oRow("AutoGratuity") = autoGrat
                End If
            Next
        Else
            If currentTable.IsTabNotTable = True Then
                For Each vrow In dvAvailTabs    'dsOrder.Tables("AvailTabs").Rows
                    If vrow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        vrow("AutoGratuity") = autoGrat
                    End If
                Next
            Else
                For Each vrow In dvAvailTables  'dsOrder.Tables("AvailTables").Rows
                    If vrow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        vrow("AutoGratuity") = autoGrat
                    End If
                Next
            End If
        End If

    End Sub

    Private Sub btnCloseGiftCardAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseGiftCardAdd.Click
        ResetTimer()
        Dim vRow As DataRowView
        Dim UnAuthPanel As Boolean
        Dim UnAuthIndex As Integer

        If paymentRowIndex > dvClosingCheckPayments.Count Then
            ' we will use:   dvUnAppliedPaymentsAndCredits_MWE 
            UnAuthPanel = True
            UnAuthIndex = paymentRowIndex - dvClosingCheckPayments.Count - 1
          End If

        If GiftAddingAmount = False Then
            GiftAddingAmount = True
            btnCloseGiftCardAdd.BackColor = Color.RoyalBlue
            RaiseEvent MakeGiftAddingAmountTrue()

            If paymentRowIndex > 0 Then
                If UnAuthPanel = True Then
                    vRow = dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)
                Else
                    vRow = dvClosingCheckPayments(paymentRowIndex - 1) '444dvUnAppliedPaymentsAndCredits(paymentRowIndex - 1)
                End If

                If Not vRow Is Nothing Then
                    If vRow("PaymentFlag") = "Gift" And vRow("AuthCode") Is DBNull.Value Then
                        ' for return - which is adding money to gift card
                        vRow("PaymentTypeID") = -97
                        vRow("PaymentTypeName") = "Increase Gift"
                        vRow("PaymentFlag") = "Issue"
                        'we can subtract below b/c we are resetting GiftCardAdd to False
                        _checkGiftIssuingAmount = vRow("PaymentAmount")
                        _lastPurchaseIssueAmount = _checkGiftIssuingAmount
                        vRow("PaymentAmount") *= -1

                        paymentPanel(paymentRowIndex).UpdatePurchase(vRow("PaymentAmount"))
                        paymentPanel(paymentRowIndex).UpdatePayType(vRow("PaymentTypeName"))
                        ReturnGiftCardAddToFalse()
                    End If
                End If
            End If

        Else
            ReturnGiftCardAddToFalse()
        End If

    End Sub

    Friend Sub ReturnGiftCardAddToFalse() ' Handles readAuth.RetruningGiftAddingAmountToFalse
        GiftAddingAmount = False
        btnCloseGiftCardAdd.BackColor = Color.LightSlateGray
    End Sub


    Private Sub btnDup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDup.Click
        If Not Me.paymentRowIndex > 0 Then Exit Sub
        If Not paymentPanel(paymentRowIndex).AuthCode = Nothing Then Exit Sub
        Dim UnAuthPanel As Boolean
        Dim UnAuthIndex As Integer

        If paymentRowIndex > dvClosingCheckPayments.Count Then
            ' we will use:   dvUnAppliedPaymentsAndCredits_MWE 
            UnAuthPanel = True
            UnAuthIndex = paymentRowIndex - dvClosingCheckPayments.Count - 1
        End If

        If paymentPanel(paymentRowIndex).DupAuth = False Then
            paymentPanel(paymentRowIndex).DupAuth = True
            If UnAuthPanel = True Then
                dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("Duplicate") = 1
            Else
                dvClosingCheckPayments(paymentRowIndex - 1)("Duplicate") = 1
            End If

        Else
            paymentPanel(paymentRowIndex).DupAuth = False
            If UnAuthPanel = True Then
                dvUnAppliedPaymentsAndCredits_MWE(UnAuthIndex)("Duplicate") = 0
            Else
                dvClosingCheckPayments(paymentRowIndex - 1)("Duplicate") = 0
            End If

        End If

        paymentPanel(paymentRowIndex).UpdateLabelDup()

    End Sub

    Private Sub btnVoiceAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoiceAuth.Click
        If Not Me.paymentRowIndex > 0 Then Exit Sub

        'AMEX change

        '   *** below will allow you to change voice auths
        '   we want no auth code or an auth code w/ no acqRefData 
        If Not paymentPanel(paymentRowIndex).AuthCode = Nothing Then
            '   If Not paymentPanel(paymentRowIndex).AcqRefData = Nothing Then
            If paymentPanel(paymentRowIndex).AuthCode.Length > 0 Then
                Exit Sub     'we have AcqRefData
            End If
        Else
            Exit Sub    'we have AcqRefData
            ' End If
        End If

        If paymentPanel(paymentRowIndex).AcctNumber = Nothing Then
            MsgBox("You must enter an Account Number.")
            Exit Sub
        End If

        If paymentPanel(paymentRowIndex).ExpDate = Nothing Then
            MsgBox("You must enter an Experation Date.")
            Exit Sub
        End If

        NumberPadLarge1.DecimalUsed = False
        NumberPadLarge1.NumberString = "Enter Voice Auth  "
        NumberPadLarge1.ShowNumberString()
        NumberPadLarge1.NumberString = ""
        paymentPanel(paymentRowIndex).CurrentState = DataSet_Builder.Payment_UC.PanelHit.VoiceAuth


    End Sub

    Private Sub DisposeObjects()

        Me.closeCheckTotals.grdCloseCheck.DataSource = Nothing
        If Not ccDisplay Is Nothing Then
            ccDisplay.Dispose()
            ccDisplay = Nothing
        End If

    End Sub

    Private Sub btnDemoCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDemoCC.Click
        Dim newPayment As Payment

        With newPayment
            .experienceNumber = currentTable.ExperienceNumber
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

        '********************************
        'this is ok because this is not card read, this is for DEMO
        '444       GenerateOrderTables.CreateTabAcctPlaceInExperience(newPayment)
        '444      AddPaymentToCollection(newPayment)
        ProcessCreditRead(newPayment)

    End Sub



  

End Class
