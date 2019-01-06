Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Xml.Serialization
Imports System.Runtime.InteropServices
Imports DataSet_Builder

Public Class BatchClose_UC
    Inherits System.Windows.Forms.UserControl

    Dim _batchDailyCode As Int64
    Dim paymentPanel() As DataSet_Builder.Payment_UC
    Dim sWriter1 As StreamWriter
    Dim printedDetail As Boolean

    Dim dsi As DSICLIENTXLib.DSICLientX
    '   Dim dsi As New DSICLIENTXLib.DSICLientX
    'old   Dim dsi As New AxDSICLIENTXLib.AxDSICLientX
    Private WithEvents PaywarePCCharge As SIM.Charge

    Dim mpsAdmin As AdminClass
    Dim mpsTStream As TStream
    Dim mpsTransaction As PreAuthTransactionClass
    Dim mpsAmount As PreAuthAmountClass
    Dim mpsAccount As AccountClass
    Dim mpsTransInfo As TranInfoClass

    Dim paymentRowIndex As Integer
    Dim panelIndex As Integer

    Dim activePanelDisplay As String
    Dim naPanelIndex As Integer = 0
    Dim cpPanelIndex As Integer = 0
    Dim crPanelIndex As Integer = 0
    Dim bdPanelIndex As Integer = 0
    Dim netPanelIndex As Integer = 0

    '   Dim displayActive As String
    Dim changesMadeToTips As Boolean

    '    Dim batchNumber As String
    '  Dim pos As BatchInfo
    Dim notAllCash As Boolean
    Dim posNonAdjCount As Integer
    Dim posNonAdjDollar As Decimal
    Dim posCreditPurchaseCount As Integer
    Dim posCreditPurchaseDollar As Decimal
    Dim posCreditReturnCount As Integer
    Dim posCreditReturnDollar As Decimal
    Dim posNetCount As Integer
    Dim posNetDollar As Decimal
    Dim posTempTipForPreAuthCaptureAdjustment As Decimal

    Dim batch As BatchInfo      'structure difined in Dinner Table
    Dim isSomethingDeclined As Boolean

    Dim batchCloseNumber As String
    Dim batchCloseNetCount As Integer
    Dim batchCloseNetDollar As Decimal

    Dim startPaymentIndex As Integer '  ?????

    Dim startVoiceIndex As Integer
    Dim endVoiceIndex As Integer
    Dim startPreAuthIndex As Integer
    Dim endPreAuthIndex As Integer
    Dim startPreAuthCaptureIndex As Integer
    Dim endPreAuthCaptureIndex As Integer
    Dim startReturnIndex As Integer
    Dim endReturnIndex As Integer


    Public Property BatchDailyCode() As Int64
        Get
            Return _batchDailyCode
        End Get
        Set(ByVal Value As Int64)
            _batchDailyCode = Value
        End Set
    End Property


    Event ExitBatchClose(ByVal bdc As Int64)
    Event ExitWithoutClose()

    Sub New()
        MyBase.new()

    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal closingDailyCode As Int64)
        MyBase.New()
        _batchDailyCode = closingDailyCode

        'This call is required by the Windows Form Designer.
        InitializeComponent()

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
    Friend WithEvents NumberPadLarge1 As DataSet_Builder.NumberPadLarge
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents pnlBatchPayments As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnNetBatch As System.Windows.Forms.Button
    Friend WithEvents btnCreditPurchase As System.Windows.Forms.Button
    Friend WithEvents btnNonAdjusted As System.Windows.Forms.Button
    Friend WithEvents btnCreditReturn As System.Windows.Forms.Button
    Friend WithEvents lblPOSNetDollar As System.Windows.Forms.Label
    Friend WithEvents lblPOSNetCount As System.Windows.Forms.Label
    Friend WithEvents lblPOSCreditReturnDollar As System.Windows.Forms.Label
    Friend WithEvents lblPOSCreditReturnCount As System.Windows.Forms.Label
    Friend WithEvents lblPOSNonAdjDollar As System.Windows.Forms.Label
    Friend WithEvents lblPOSCreditPurchaseDollar As System.Windows.Forms.Label
    Friend WithEvents lblPOSCreditPurchaseCount As System.Windows.Forms.Label
    Friend WithEvents lblBatchCreditPurchaseCount As System.Windows.Forms.Label
    Friend WithEvents lblBatchCreditPurchaseDollar As System.Windows.Forms.Label
    Friend WithEvents lblBatchCreditReturnCount As System.Windows.Forms.Label
    Friend WithEvents lblBatchCreditReturnDollar As System.Windows.Forms.Label
    Friend WithEvents lblBatchNetCount As System.Windows.Forms.Label
    Friend WithEvents lblBatchNetDollar As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents btnSendAdjustments As System.Windows.Forms.Button
    Friend WithEvents lblBatchSummary As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblPOSNonAdjCount As System.Windows.Forms.Label
    Friend WithEvents btnFinalize As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents pnlDeclined As System.Windows.Forms.Panel
    Friend WithEvents btnDeclined As System.Windows.Forms.Button
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents lblNumberDeclined As System.Windows.Forms.Label
    Friend WithEvents lblDollarDeclined As System.Windows.Forms.Label
    Friend WithEvents btnPrintAuth As System.Windows.Forms.Button
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents btnDailyPreviousPage As System.Windows.Forms.Button
    Friend WithEvents btnDailyNextPage As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BatchClose_UC))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.lblPOSNetDollar = New System.Windows.Forms.Label
        Me.lblPOSNetCount = New System.Windows.Forms.Label
        Me.btnNetBatch = New System.Windows.Forms.Button
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.btnCreditReturn = New System.Windows.Forms.Button
        Me.lblPOSCreditReturnDollar = New System.Windows.Forms.Label
        Me.lblPOSCreditReturnCount = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnNonAdjusted = New System.Windows.Forms.Button
        Me.lblPOSNonAdjDollar = New System.Windows.Forms.Label
        Me.lblPOSNonAdjCount = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lblPOSCreditPurchaseDollar = New System.Windows.Forms.Label
        Me.lblPOSCreditPurchaseCount = New System.Windows.Forms.Label
        Me.btnCreditPurchase = New System.Windows.Forms.Button
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblBatchCreditPurchaseCount = New System.Windows.Forms.Label
        Me.lblBatchCreditPurchaseDollar = New System.Windows.Forms.Label
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.lblBatchCreditReturnCount = New System.Windows.Forms.Label
        Me.lblBatchCreditReturnDollar = New System.Windows.Forms.Label
        Me.lblBatchSummary = New System.Windows.Forms.Label
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.lblBatchNetCount = New System.Windows.Forms.Label
        Me.lblBatchNetDollar = New System.Windows.Forms.Label
        Me.Panel14 = New System.Windows.Forms.Panel
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.pnlBatchPayments = New System.Windows.Forms.Panel
        Me.NumberPadLarge1 = New DataSet_Builder.NumberPadLarge
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.btnPrintAuth = New System.Windows.Forms.Button
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnFinalize = New System.Windows.Forms.Button
        Me.btnSendAdjustments = New System.Windows.Forms.Button
        Me.pnlDeclined = New System.Windows.Forms.Panel
        Me.Panel13 = New System.Windows.Forms.Panel
        Me.lblNumberDeclined = New System.Windows.Forms.Label
        Me.lblDollarDeclined = New System.Windows.Forms.Label
        Me.btnDeclined = New System.Windows.Forms.Button
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.btnDailyNextPage = New System.Windows.Forms.Button
        Me.btnDailyPreviousPage = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlDeclined.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Panel11)
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Location = New System.Drawing.Point(624, 40)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(360, 176)
        Me.Panel1.TabIndex = 0
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Label20)
        Me.Panel11.Controls.Add(Me.Label21)
        Me.Panel11.Location = New System.Drawing.Point(96, 8)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(120, 24)
        Me.Panel11.TabIndex = 5
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(56, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(64, 24)
        Me.Label20.TabIndex = 1
        Me.Label20.Text = "Dollar"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(56, 24)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "Count"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.lblPOSNetDollar)
        Me.Panel8.Controls.Add(Me.lblPOSNetCount)
        Me.Panel8.Controls.Add(Me.btnNetBatch)
        Me.Panel8.Location = New System.Drawing.Point(8, 136)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(208, 32)
        Me.Panel8.TabIndex = 4
        '
        'lblPOSNetDollar
        '
        Me.lblPOSNetDollar.Location = New System.Drawing.Point(136, 0)
        Me.lblPOSNetDollar.Name = "lblPOSNetDollar"
        Me.lblPOSNetDollar.Size = New System.Drawing.Size(72, 24)
        Me.lblPOSNetDollar.TabIndex = 2
        Me.lblPOSNetDollar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPOSNetCount
        '
        Me.lblPOSNetCount.Location = New System.Drawing.Point(96, 0)
        Me.lblPOSNetCount.Name = "lblPOSNetCount"
        Me.lblPOSNetCount.Size = New System.Drawing.Size(32, 24)
        Me.lblPOSNetCount.TabIndex = 1
        Me.lblPOSNetCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnNetBatch
        '
        Me.btnNetBatch.Location = New System.Drawing.Point(0, 0)
        Me.btnNetBatch.Name = "btnNetBatch"
        Me.btnNetBatch.Size = New System.Drawing.Size(88, 32)
        Me.btnNetBatch.TabIndex = 3
        Me.btnNetBatch.Text = "Net Batch"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.btnCreditReturn)
        Me.Panel7.Controls.Add(Me.lblPOSCreditReturnDollar)
        Me.Panel7.Controls.Add(Me.lblPOSCreditReturnCount)
        Me.Panel7.Location = New System.Drawing.Point(8, 104)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(208, 32)
        Me.Panel7.TabIndex = 3
        '
        'btnCreditReturn
        '
        Me.btnCreditReturn.Location = New System.Drawing.Point(0, 0)
        Me.btnCreditReturn.Name = "btnCreditReturn"
        Me.btnCreditReturn.Size = New System.Drawing.Size(88, 32)
        Me.btnCreditReturn.TabIndex = 3
        Me.btnCreditReturn.Text = "Credit Return"
        '
        'lblPOSCreditReturnDollar
        '
        Me.lblPOSCreditReturnDollar.Location = New System.Drawing.Point(136, 0)
        Me.lblPOSCreditReturnDollar.Name = "lblPOSCreditReturnDollar"
        Me.lblPOSCreditReturnDollar.Size = New System.Drawing.Size(72, 24)
        Me.lblPOSCreditReturnDollar.TabIndex = 2
        Me.lblPOSCreditReturnDollar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPOSCreditReturnCount
        '
        Me.lblPOSCreditReturnCount.Location = New System.Drawing.Point(96, 0)
        Me.lblPOSCreditReturnCount.Name = "lblPOSCreditReturnCount"
        Me.lblPOSCreditReturnCount.Size = New System.Drawing.Size(32, 24)
        Me.lblPOSCreditReturnCount.TabIndex = 1
        Me.lblPOSCreditReturnCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnNonAdjusted)
        Me.Panel4.Controls.Add(Me.lblPOSNonAdjDollar)
        Me.Panel4.Controls.Add(Me.lblPOSNonAdjCount)
        Me.Panel4.Location = New System.Drawing.Point(8, 40)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(208, 32)
        Me.Panel4.TabIndex = 1
        '
        'btnNonAdjusted
        '
        Me.btnNonAdjusted.Location = New System.Drawing.Point(0, 0)
        Me.btnNonAdjusted.Name = "btnNonAdjusted"
        Me.btnNonAdjusted.Size = New System.Drawing.Size(88, 32)
        Me.btnNonAdjusted.TabIndex = 3
        Me.btnNonAdjusted.Text = "Non Adjusted"
        '
        'lblPOSNonAdjDollar
        '
        Me.lblPOSNonAdjDollar.Location = New System.Drawing.Point(136, 0)
        Me.lblPOSNonAdjDollar.Name = "lblPOSNonAdjDollar"
        Me.lblPOSNonAdjDollar.Size = New System.Drawing.Size(72, 24)
        Me.lblPOSNonAdjDollar.TabIndex = 2
        Me.lblPOSNonAdjDollar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPOSNonAdjCount
        '
        Me.lblPOSNonAdjCount.Location = New System.Drawing.Point(96, 0)
        Me.lblPOSNonAdjCount.Name = "lblPOSNonAdjCount"
        Me.lblPOSNonAdjCount.Size = New System.Drawing.Size(32, 24)
        Me.lblPOSNonAdjCount.TabIndex = 1
        Me.lblPOSNonAdjCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lblPOSCreditPurchaseDollar)
        Me.Panel3.Controls.Add(Me.lblPOSCreditPurchaseCount)
        Me.Panel3.Controls.Add(Me.btnCreditPurchase)
        Me.Panel3.Location = New System.Drawing.Point(8, 72)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(208, 32)
        Me.Panel3.TabIndex = 0
        '
        'lblPOSCreditPurchaseDollar
        '
        Me.lblPOSCreditPurchaseDollar.Location = New System.Drawing.Point(136, 0)
        Me.lblPOSCreditPurchaseDollar.Name = "lblPOSCreditPurchaseDollar"
        Me.lblPOSCreditPurchaseDollar.Size = New System.Drawing.Size(72, 24)
        Me.lblPOSCreditPurchaseDollar.TabIndex = 2
        Me.lblPOSCreditPurchaseDollar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPOSCreditPurchaseCount
        '
        Me.lblPOSCreditPurchaseCount.Location = New System.Drawing.Point(96, 0)
        Me.lblPOSCreditPurchaseCount.Name = "lblPOSCreditPurchaseCount"
        Me.lblPOSCreditPurchaseCount.Size = New System.Drawing.Size(32, 24)
        Me.lblPOSCreditPurchaseCount.TabIndex = 1
        Me.lblPOSCreditPurchaseCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCreditPurchase
        '
        Me.btnCreditPurchase.Location = New System.Drawing.Point(0, 0)
        Me.btnCreditPurchase.Name = "btnCreditPurchase"
        Me.btnCreditPurchase.Size = New System.Drawing.Size(88, 32)
        Me.btnCreditPurchase.TabIndex = 3
        Me.btnCreditPurchase.Text = "Credit Purchase"
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Panel2)
        Me.Panel5.Controls.Add(Me.Panel9)
        Me.Panel5.Controls.Add(Me.lblBatchSummary)
        Me.Panel5.Controls.Add(Me.Panel10)
        Me.Panel5.Controls.Add(Me.Panel14)
        Me.Panel5.Location = New System.Drawing.Point(224, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(136, 176)
        Me.Panel5.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lblBatchCreditPurchaseCount)
        Me.Panel2.Controls.Add(Me.lblBatchCreditPurchaseDollar)
        Me.Panel2.Location = New System.Drawing.Point(8, 72)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(120, 32)
        Me.Panel2.TabIndex = 5
        '
        'lblBatchCreditPurchaseCount
        '
        Me.lblBatchCreditPurchaseCount.Location = New System.Drawing.Point(8, 0)
        Me.lblBatchCreditPurchaseCount.Name = "lblBatchCreditPurchaseCount"
        Me.lblBatchCreditPurchaseCount.Size = New System.Drawing.Size(32, 24)
        Me.lblBatchCreditPurchaseCount.TabIndex = 0
        Me.lblBatchCreditPurchaseCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBatchCreditPurchaseDollar
        '
        Me.lblBatchCreditPurchaseDollar.Location = New System.Drawing.Point(40, 0)
        Me.lblBatchCreditPurchaseDollar.Name = "lblBatchCreditPurchaseDollar"
        Me.lblBatchCreditPurchaseDollar.Size = New System.Drawing.Size(80, 24)
        Me.lblBatchCreditPurchaseDollar.TabIndex = 1
        Me.lblBatchCreditPurchaseDollar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.lblBatchCreditReturnCount)
        Me.Panel9.Controls.Add(Me.lblBatchCreditReturnDollar)
        Me.Panel9.Location = New System.Drawing.Point(8, 104)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(120, 32)
        Me.Panel9.TabIndex = 2
        '
        'lblBatchCreditReturnCount
        '
        Me.lblBatchCreditReturnCount.Location = New System.Drawing.Point(8, 0)
        Me.lblBatchCreditReturnCount.Name = "lblBatchCreditReturnCount"
        Me.lblBatchCreditReturnCount.Size = New System.Drawing.Size(32, 24)
        Me.lblBatchCreditReturnCount.TabIndex = 0
        Me.lblBatchCreditReturnCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBatchCreditReturnDollar
        '
        Me.lblBatchCreditReturnDollar.Location = New System.Drawing.Point(40, 0)
        Me.lblBatchCreditReturnDollar.Name = "lblBatchCreditReturnDollar"
        Me.lblBatchCreditReturnDollar.Size = New System.Drawing.Size(80, 24)
        Me.lblBatchCreditReturnDollar.TabIndex = 1
        Me.lblBatchCreditReturnDollar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBatchSummary
        '
        Me.lblBatchSummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchSummary.Location = New System.Drawing.Point(0, 8)
        Me.lblBatchSummary.Name = "lblBatchSummary"
        Me.lblBatchSummary.Size = New System.Drawing.Size(136, 24)
        Me.lblBatchSummary.TabIndex = 0
        Me.lblBatchSummary.Text = "Batch Summary"
        Me.lblBatchSummary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.lblBatchNetCount)
        Me.Panel10.Controls.Add(Me.lblBatchNetDollar)
        Me.Panel10.Location = New System.Drawing.Point(8, 136)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(120, 32)
        Me.Panel10.TabIndex = 2
        '
        'lblBatchNetCount
        '
        Me.lblBatchNetCount.Location = New System.Drawing.Point(8, 0)
        Me.lblBatchNetCount.Name = "lblBatchNetCount"
        Me.lblBatchNetCount.Size = New System.Drawing.Size(32, 24)
        Me.lblBatchNetCount.TabIndex = 0
        Me.lblBatchNetCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBatchNetDollar
        '
        Me.lblBatchNetDollar.Location = New System.Drawing.Point(40, 0)
        Me.lblBatchNetDollar.Name = "lblBatchNetDollar"
        Me.lblBatchNetDollar.Size = New System.Drawing.Size(80, 24)
        Me.lblBatchNetDollar.TabIndex = 1
        Me.lblBatchNetDollar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.Label29)
        Me.Panel14.Controls.Add(Me.Label30)
        Me.Panel14.Location = New System.Drawing.Point(8, 40)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(120, 24)
        Me.Panel14.TabIndex = 4
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(48, 24)
        Me.Label29.TabIndex = 0
        Me.Label29.Text = "Count"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(56, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(64, 24)
        Me.Label30.TabIndex = 1
        Me.Label30.Text = "Dollar"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlBatchPayments
        '
        Me.pnlBatchPayments.BackColor = System.Drawing.Color.Black
        Me.pnlBatchPayments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlBatchPayments.Location = New System.Drawing.Point(3, 40)
        Me.pnlBatchPayments.Name = "pnlBatchPayments"
        Me.pnlBatchPayments.Size = New System.Drawing.Size(456, 656)
        Me.pnlBatchPayments.TabIndex = 1
        '
        'NumberPadLarge1
        '
        Me.NumberPadLarge1.BackColor = System.Drawing.Color.SlateGray
        Me.NumberPadLarge1.DecimalUsed = True
        Me.NumberPadLarge1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.NumberPadLarge1.IntegerNumber = 0
        Me.NumberPadLarge1.Location = New System.Drawing.Point(481, 326)
        Me.NumberPadLarge1.Name = "NumberPadLarge1"
        Me.NumberPadLarge1.NumberString = ""
        Me.NumberPadLarge1.NumberTotal = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumberPadLarge1.Size = New System.Drawing.Size(240, 370)
        Me.NumberPadLarge1.TabIndex = 2
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Black
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel6.Controls.Add(Me.btnPrintAuth)
        Me.Panel6.Controls.Add(Me.btnRefresh)
        Me.Panel6.Controls.Add(Me.btnExit)
        Me.Panel6.Controls.Add(Me.btnFinalize)
        Me.Panel6.Controls.Add(Me.btnSendAdjustments)
        Me.Panel6.Location = New System.Drawing.Point(745, 456)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(240, 240)
        Me.Panel6.TabIndex = 3
        '
        'btnPrintAuth
        '
        Me.btnPrintAuth.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.btnPrintAuth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintAuth.Location = New System.Drawing.Point(8, 88)
        Me.btnPrintAuth.Name = "btnPrintAuth"
        Me.btnPrintAuth.Size = New System.Drawing.Size(104, 56)
        Me.btnPrintAuth.TabIndex = 4
        Me.btnPrintAuth.Text = "Print All Authorizations"
        Me.btnPrintAuth.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.btnRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(24, 184)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 40)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(144, 184)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(72, 40)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnFinalize
        '
        Me.btnFinalize.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.btnFinalize.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFinalize.Location = New System.Drawing.Point(128, 8)
        Me.btnFinalize.Name = "btnFinalize"
        Me.btnFinalize.Size = New System.Drawing.Size(104, 64)
        Me.btnFinalize.TabIndex = 1
        Me.btnFinalize.Text = "Finalize"
        Me.btnFinalize.UseVisualStyleBackColor = False
        Me.btnFinalize.Visible = False
        '
        'btnSendAdjustments
        '
        Me.btnSendAdjustments.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.btnSendAdjustments.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSendAdjustments.Location = New System.Drawing.Point(8, 8)
        Me.btnSendAdjustments.Name = "btnSendAdjustments"
        Me.btnSendAdjustments.Size = New System.Drawing.Size(104, 64)
        Me.btnSendAdjustments.TabIndex = 0
        Me.btnSendAdjustments.Text = "Close Batch"
        Me.btnSendAdjustments.UseVisualStyleBackColor = False
        '
        'pnlDeclined
        '
        Me.pnlDeclined.BackColor = System.Drawing.Color.Black
        Me.pnlDeclined.Controls.Add(Me.Panel13)
        Me.pnlDeclined.Controls.Add(Me.btnDeclined)
        Me.pnlDeclined.Location = New System.Drawing.Point(744, 232)
        Me.pnlDeclined.Name = "pnlDeclined"
        Me.pnlDeclined.Size = New System.Drawing.Size(240, 48)
        Me.pnlDeclined.TabIndex = 4
        Me.pnlDeclined.Visible = False
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.Panel13.Controls.Add(Me.lblNumberDeclined)
        Me.Panel13.Controls.Add(Me.lblDollarDeclined)
        Me.Panel13.Location = New System.Drawing.Point(112, 8)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(120, 32)
        Me.Panel13.TabIndex = 7
        '
        'lblNumberDeclined
        '
        Me.lblNumberDeclined.Location = New System.Drawing.Point(8, 0)
        Me.lblNumberDeclined.Name = "lblNumberDeclined"
        Me.lblNumberDeclined.Size = New System.Drawing.Size(32, 24)
        Me.lblNumberDeclined.TabIndex = 0
        Me.lblNumberDeclined.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDollarDeclined
        '
        Me.lblDollarDeclined.Location = New System.Drawing.Point(40, 0)
        Me.lblDollarDeclined.Name = "lblDollarDeclined"
        Me.lblDollarDeclined.Size = New System.Drawing.Size(80, 24)
        Me.lblDollarDeclined.TabIndex = 1
        Me.lblDollarDeclined.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnDeclined
        '
        Me.btnDeclined.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.btnDeclined.Location = New System.Drawing.Point(8, 8)
        Me.btnDeclined.Name = "btnDeclined"
        Me.btnDeclined.Size = New System.Drawing.Size(88, 32)
        Me.btnDeclined.TabIndex = 4
        Me.btnDeclined.Text = "Declined"
        Me.btnDeclined.UseVisualStyleBackColor = False
        '
        'Panel12
        '
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel12.Controls.Add(Me.btnDailyNextPage)
        Me.Panel12.Controls.Add(Me.btnDailyPreviousPage)
        Me.Panel12.Location = New System.Drawing.Point(480, 40)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(120, 144)
        Me.Panel12.TabIndex = 5
        '
        'btnDailyNextPage
        '
        Me.btnDailyNextPage.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.btnDailyNextPage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDailyNextPage.Location = New System.Drawing.Point(16, 80)
        Me.btnDailyNextPage.Name = "btnDailyNextPage"
        Me.btnDailyNextPage.Size = New System.Drawing.Size(88, 48)
        Me.btnDailyNextPage.TabIndex = 1
        Me.btnDailyNextPage.Text = "Next Page"
        Me.btnDailyNextPage.UseVisualStyleBackColor = False
        '
        'btnDailyPreviousPage
        '
        Me.btnDailyPreviousPage.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.btnDailyPreviousPage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDailyPreviousPage.Location = New System.Drawing.Point(16, 16)
        Me.btnDailyPreviousPage.Name = "btnDailyPreviousPage"
        Me.btnDailyPreviousPage.Size = New System.Drawing.Size(88, 48)
        Me.btnDailyPreviousPage.TabIndex = 0
        Me.btnDailyPreviousPage.Text = "Previous Page"
        Me.btnDailyPreviousPage.UseVisualStyleBackColor = False
        '
        'BatchClose_UC
        '
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.Controls.Add(Me.Panel12)
        Me.Controls.Add(Me.pnlDeclined)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.NumberPadLarge1)
        Me.Controls.Add(Me.pnlBatchPayments)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "BatchClose_UC"
        Me.Size = New System.Drawing.Size(1024, 786)
        Me.Panel1.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.pnlDeclined.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub InitializeOther()


        ' this is testing, it auto closes if no batch to do and manual
        '***   somehow it still does not exit program
        '     If companyInfo.usingOtherCreditProcessor = True And dsOrder.Tables("PaymentsAndCredits").Rows.Count = 0 Then
        '      GenerateOrderTables.StartDailyBusinessClose(actingManager.EmployeeID, BatchDailyCode)
        '     dsOrder.Tables("QuickTickets").Rows.Clear()
        '    RaiseEvent ExitBatchClose(BatchDailyCode)
        '   Exit Sub
        '  End If
        '*******
        '   dsOrder.Tables("PaymentsAndCredits") is only non cash payments in BatchClose
        '   paymentTypeID > 1

        If companyInfo.processor = "Mercury" Then
            dsi = New DSICLIENTXLib.DSICLientX
        End If

        ReDim paymentPanel(dsOrder.Tables("PaymentsAndCredits").Rows.Count)
        '     FilterBatchDataview()

        CreateNewPaymentPanels()
        activePanelDisplay = "nonAdjusted"
        DisplayNonAdjustedPaymentPanels()
        GenerateOrderTables.CreatespiderPOSDirectory()
        PrintDetail()

    End Sub



    Private Sub ResetButonColorsPanelChoices()

        Me.btnNonAdjusted.BackColor = c12
        Me.btnNonAdjusted.ForeColor = c2
        Me.btnCreditPurchase.BackColor = c12
        Me.btnCreditPurchase.ForeColor = c2
        Me.btnCreditReturn.BackColor = c12
        Me.btnCreditReturn.ForeColor = c2
        Me.btnNetBatch.BackColor = c12
        Me.btnNetBatch.ForeColor = c2

        startPaymentIndex = 0

    End Sub

    Private Sub DisplayNonAdjustedPaymentPanels()

        '   below position starts at zero for formating
        '   startPaymentIndex starts at zero and moves to 1 if panels adj 1 place

        Dim oRow As DataRow
        Dim PnlNo As Integer = 1
        Dim position As Integer = 0
        Dim panelCount As Integer = 0 'naPanelIndex
        Me.btnNonAdjusted.BackColor = c11
        Me.btnNonAdjusted.ForeColor = c3

        Me.pnlBatchPayments.Controls.Clear()
        '  displayActive = "NonAdjusted"

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                '      If not oRow("TransactionCode") Is DBNull.Value Then
                '       we might need this
                '      End If
                If oRow("TransactionCode") = "VoiceAuth" Then
                    If panelCount >= naPanelIndex And panelCount < (naPanelIndex + 10) Then
                        If oRow("Tip") = 0 Then
                            If (position + 1) > startPaymentIndex Then
                                With paymentPanel(PnlNo)
                                    .Location = New Point(0, (.Height * position))
                                    .ReverseAlignment()
                                    Me.pnlBatchPayments.Controls.Add(paymentPanel(PnlNo))
                                End With
                                position += 1
                                If position = (startPaymentIndex + 8) Then Exit For
                            End If
                        End If
                    End If
                    panelCount += 1
                ElseIf oRow("TransactionCode") = "PreAuth" Then
                    If panelCount >= naPanelIndex And panelCount < (naPanelIndex + 10) Then
                        If (position + 1) > startPaymentIndex Then
                            With paymentPanel(PnlNo)
                                .Location = New Point(0, (.Height * position))
                                .ReverseAlignment()
                                Me.pnlBatchPayments.Controls.Add(paymentPanel(PnlNo))
                            End With
                            position += 1
                            If position = (startPaymentIndex + 10) Then Exit For
                        End If
                    End If
                    panelCount += 1
                End If
                PnlNo += 1
            End If
        Next

    End Sub

    Private Sub DisplayCreditPurchasePaymentPanels()

        Dim oRow As DataRow
        Dim PnlNo As Integer = 1
        Dim position As Integer = 0 'cpPanelIndex
        Dim panelCount As Integer
        Me.btnCreditPurchase.BackColor = c11
        Me.btnCreditPurchase.ForeColor = c3

        Me.pnlBatchPayments.Controls.Clear()
        '      displayActive = "CreditPurchase"

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("TransactionCode") = "VoiceAuth" Then
                    If panelCount >= cpPanelIndex And panelCount < (cpPanelIndex + 10) Then
                        If oRow("Tip") > 0 Then
                            If (position + 1) > startPaymentIndex Then
                                With paymentPanel(PnlNo)
                                    .Location = New Point(0, (.Height * position))
                                    .StandardAlignment()
                                    Me.pnlBatchPayments.Controls.Add(paymentPanel(PnlNo))
                                End With
                                position += 1
                                If position = (startPaymentIndex + 10) Then Exit For
                            End If
                        End If
                    End If
                    panelCount += 1
                ElseIf oRow("TransactionCode") = "PreAuthCapture" Then
                    If panelCount >= cpPanelIndex And panelCount < (cpPanelIndex + 10) Then
                        If (position + 1) > startPaymentIndex Then
                            With paymentPanel(PnlNo)
                                .Location = New Point(0, (.Height * position))
                                Me.pnlBatchPayments.Controls.Add(paymentPanel(PnlNo))
                            End With
                            position += 1
                            If position = (startPaymentIndex + 10) Then Exit For
                        End If
                    End If
                    panelCount += 1
            End If
                PnlNo += 1
            End If
        Next

    End Sub

    Private Sub DisplayCreditReturnPaymentPanels()
        Dim oRow As DataRow
        Dim PnlNo As Integer = 1
        Dim position As Integer = 0 'crPanelIndex
        Dim panelCount As Integer = 0
        Me.btnCreditReturn.BackColor = c11
        Me.btnCreditReturn.ForeColor = c3

        Me.pnlBatchPayments.Controls.Clear()
        '    displayActive = "CreditRefund"

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then

                If oRow("TransactionCode") = "Return" Then
                    If panelCount >= crPanelIndex And panelCount < (crPanelIndex + 10) Then
                        If (position + 1) > startPaymentIndex Then
                            With paymentPanel(PnlNo)
                                .Location = New Point(0, (.Height * position))
                                Me.pnlBatchPayments.Controls.Add(paymentPanel(PnlNo))
                            End With
                            position += 1
                            If position = (startPaymentIndex + 10) Then Exit For
                        End If
                    End If
                    panelCount += 1
                End If
                PnlNo += 1
            End If
        Next

    End Sub

    Private Sub DisplayNetBatchPanels()


        Dim oRow As DataRow
        Dim PnlNo As Integer = 1
        Dim position As Integer = 0 'bdPanelIndex
        Dim panelCount As Integer
        Me.btnDeclined.BackColor = c11
        Me.btnDeclined.ForeColor = c3

        Me.pnlBatchPayments.Controls.Clear()
        '     displayActive = "net"

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("BatchCleared") = False And oRow("TransactionCode") = "PreAuth" Then
                    If panelCount >= netPanelIndex And panelCount < (netPanelIndex + 10) Then
                        If (position + 1) > startPaymentIndex Then
                            With paymentPanel(PnlNo)
                                .Location = New Point(0, (.Height * position))
                                .MakeDeclineReasonVisible()
                                Me.pnlBatchPayments.Controls.Add(paymentPanel(PnlNo))
                            End With
                            position += 1
                            If position = (startPaymentIndex + 10) Then Exit For
                        End If
                    End If
                    panelCount += 1
                End If
                PnlNo += 1
            End If
        Next

    End Sub

    Private Sub DisplayBatchDeclines()

        Dim oRow As DataRow
        Dim PnlNo As Integer = 1
        Dim position As Integer = 0 'bdPanelIndex
        Dim panelCount As Integer
        Me.btnDeclined.BackColor = c11
        Me.btnDeclined.ForeColor = c3

        Me.pnlBatchPayments.Controls.Clear()
        '     displayActive = "CreditRefund"

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("BatchCleared") = False And oRow("TransactionCode") = "PreAuth" Then
                    If panelCount >= bdPanelIndex And panelCount < (bdPanelIndex + 10) Then
                        If (position + 1) > startPaymentIndex Then
                            With paymentPanel(PnlNo)
                                .Location = New Point(0, (.Height * position))
                                .MakeDeclineReasonVisible()
                                Me.pnlBatchPayments.Controls.Add(paymentPanel(PnlNo))
                            End With
                            position += 1
                            If position = (startPaymentIndex + 10) Then Exit For
                        End If
                    End If
                    panelCount += 1
                End If
                PnlNo += 1
            End If
        Next

    End Sub

    Private Sub CreateNewPaymentPanels()

        Dim oRow As DataRow
        Dim PnlNo As Integer = 1
        Dim position As Integer
        Dim empName As String
        Dim truncAcctNum As String

        Try
            For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then

                    empName = oRow("FirstName") & " " & oRow("LastName")
                    If Not oRow("AccountNumber").Substring(0, 4) = "xxxx" And Not oRow("AccountNumber") = "Manual" Then
                        truncAcctNum = TruncateAccountNumber(oRow("AccountNumber"))
                    Else
                        truncAcctNum = (oRow("AccountNumber"))
                    End If
                    paymentPanel(PnlNo) = New DataSet_Builder.Payment_UC("Batch", Nothing, oRow, PnlNo, empName, truncAcctNum, 0)
                    With paymentPanel(PnlNo)
                        .BackColor = Color.DarkGray
                        AddHandler paymentPanel(PnlNo).ActivePanel, AddressOf PaymentUserControl_Click
                    End With
                    PnlNo += 1
                    If oRow("TransactionCode") = "VoiceAuth" Then
                        notAllCash = True
                        If oRow("Tip") = 0 Then
                            posNonAdjCount += 1
                            posNonAdjDollar += oRow("PaymentAmount")
                        Else
                            posCreditPurchaseCount += 1
                            posCreditPurchaseDollar += oRow("PaymentAmount")
                            posCreditPurchaseDollar += oRow("Tip")
                        End If

                    ElseIf oRow("TransactionCode") = "PreAuth" Then
                        notAllCash = True
                        posNonAdjCount += 1
                        posNonAdjDollar += oRow("PaymentAmount")

                    ElseIf oRow("TransactionCode") = "PreAuthCapture" Then
                        notAllCash = True
                        If Not oRow("AuthCode") Is DBNull.Value Then
                            If Not oRow("AuthCode") = "MERCXX" Then
                                posCreditPurchaseCount += 1
                                posCreditPurchaseDollar += oRow("PaymentAmount")
                                posCreditPurchaseDollar += oRow("Tip")
                            End If
                        End If

                    ElseIf oRow("TransactionCode") = "Return" Then
                        '   ********************
                        '   this is if returns are negative amounts in database
                        notAllCash = True
                        posCreditReturnCount += 1
                        posCreditReturnDollar += oRow("PaymentAmount")
                        ' should not need for retrun .. posCreditPurchaseDollar += oRow("Tip")
                    End If
                    If Not oRow("AuthCode") Is DBNull.Value Then
                        If Not oRow("AuthCode") = "MERCXX" Then
                            posNetCount += 1
                            posNetDollar += oRow("PaymentAmount")
                            posNetDollar += oRow("Tip")
                        End If
                    Else    'this is for returns and which do not have authcode yet
                        posNetCount += 1
                        posNetDollar += oRow("PaymentAmount")
                        posNetDollar += oRow("Tip")
                    End If

                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try


        DisplayBatchTotalPanel()

    End Sub



    Private Sub PaymentUserControl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlBatchPayments.Click
        Dim objButton As DataSet_Builder.Payment_UC

        Try
            objButton = CType(sender, DataSet_Builder.Payment_UC)
        Catch ex As Exception
            Exit Sub
        End Try

        If Not paymentRowIndex = objButton.ActiveIndex Then
            paymentRowIndex = objButton.ActiveIndex
            ActiveThisPanel(objButton.ActiveIndex)
        End If

    End Sub

    Private Sub ActiveThisPanel(ByVal ai As Integer)
        Dim oRow As DataRow
        Dim index As Integer = 1

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If index > startPaymentIndex Then
                    If Not index = ai Then
                        paymentPanel(index).BackColor = Color.DarkGray
                        paymentPanel(index).IsActive = False
                    Else
                        '   here index and paymentrowindex are the same
                        paymentPanel(index).BackColor = Color.WhiteSmoke
                        paymentPanel(paymentRowIndex).CurrentState = DataSet_Builder.Payment_UC.PanelHit.TipPanel
                        PaymentPanelActivated()
                        panelIndex = CInt((paymentPanel(paymentRowIndex).Location.Y) / 72) + 1

                        'the following is used for any adjustments to Tips 
                        If oRow("TransactionCode") = "PreAuthCapture" Then
                            posTempTipForPreAuthCaptureAdjustment = oRow("Tip")
                        Else
                            posTempTipForPreAuthCaptureAdjustment = 0
                        End If

                    End If
                End If
                index += 1
                'If index = (startPaymentIndex + 10) Then Exit For
            End If
        Next

    End Sub

    Private Sub PaymentPanelActivated()

        '    If paymentPanel(paymentRowIndex).CurrentState = DataSet_Builder.Payment_UC.PanelHit.TipPanel Then
        Me.NumberPadLarge1.NumberTotal = paymentPanel(paymentRowIndex).TipAmount
        Me.NumberPadLarge1.ShowNumberString()
        Me.NumberPadLarge1.Focus()
        Me.NumberPadLarge1.IntegerNumber = 0
        Me.NumberPadLarge1.NumberString = Nothing
        '   End If


    End Sub


    Private Sub NumberPad_EnteredHit(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumberPadLarge1.NumberEntered

        If paymentRowIndex = 0 Then
            MsgBox("Select a Panel to add Gratuity")
            Exit Sub
        End If

        If Me.NumberPadLarge1.NumberTotal > dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("PaymentAmount") Then
            If MsgBox("Gratuity Amount is greater than Purchase. Please Verify", MsgBoxStyle.OKCancel) = MsgBoxResult.Cancel Then
                Exit Sub
            End If
        End If

        If dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("TransactionCode") = "PreAuth" Then
            If dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("Tip") > 0 Then
                If MsgBox("Are you sure you want to adjust this Tip?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("Tip") = Me.NumberPadLarge1.NumberTotal
                    dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("TransactionCode") = "PreAuthCapture"
                    ' do a count adjustment
                    posNonAdjCount -= 1
                    posNonAdjDollar -= dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("PaymentAmount")
                    If Not dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("AuthCode") Is DBNull.Value Then
                        If Not dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("AuthCode") = "MERCXX" Then
                            posCreditPurchaseCount += 1
                            posCreditPurchaseDollar += dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("PaymentAmount")
                            posCreditPurchaseDollar += Me.NumberPadLarge1.NumberTotal
                            posNetDollar += Me.NumberPadLarge1.NumberTotal
                        End If
                    End If
                    paymentPanel(paymentRowIndex).UpdateTip(Me.NumberPadLarge1.NumberTotal)
                    changesMadeToTips = True
                End If
            End If

        ElseIf dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("TransactionCode") = "PreAuthCapture" Then
            If dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("Tip") > 0 Then
                If MsgBox("Are you sure you want to adjust this Tip?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("Tip") = Me.NumberPadLarge1.NumberTotal
                    posCreditPurchaseDollar += (Me.NumberPadLarge1.NumberTotal - posTempTipForPreAuthCaptureAdjustment)
                    posNetDollar += (Me.NumberPadLarge1.NumberTotal - posTempTipForPreAuthCaptureAdjustment)
                    posTempTipForPreAuthCaptureAdjustment = 0
                    paymentPanel(paymentRowIndex).UpdateTip(Me.NumberPadLarge1.NumberTotal)
                    changesMadeToTips = True
                End If
            End If

        ElseIf dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("TransactionCode") = "VoiceAuth" Then
            If dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("Tip") > 0 Then
                If MsgBox("Are you sure you want to adjust this Tip?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("Tip") = Me.NumberPadLarge1.NumberTotal
                    '          If dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("Tip") = 0 Then
                    posNonAdjCount -= 1
                    posNonAdjDollar -= dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("PaymentAmount")
                    posCreditPurchaseCount += 1
                    posCreditPurchaseDollar += dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("PaymentAmount")
                    posCreditPurchaseDollar += Me.NumberPadLarge1.NumberTotal
                    posNetDollar += Me.NumberPadLarge1.NumberTotal
                    '     End If
                    paymentPanel(paymentRowIndex).UpdateTip(Me.NumberPadLarge1.NumberTotal)
                    changesMadeToTips = True
                End If
            End If

        ElseIf dsOrder.Tables("PaymentsAndCredits").Rows(paymentRowIndex - 1)("TransactionCode") = "Return" Then
            '   we may not allow any adjustments

        End If

        DisplayBatchTotalPanel()
        MoveToNextPanel()

        If paymentRowIndex < dsOrder.Tables("PaymentsAndCredits").Rows.Count Then
            paymentRowIndex += 1
            ActiveThisPanel(paymentRowIndex)
        End If


    End Sub

    Private Sub MoveToNextPanel()


        Select Case activePanelDisplay
            Case "nonAdjusted"
                If panelIndex = 9 Then
                    '   must move all panels up one space
                    '   first check if there is another panel avail
                    startPaymentIndex += 1
                    DisplayNonAdjustedPaymentPanels()
                    ActiveThisPanel(9)
                Else
                    panelIndex += 1
                    ActiveThisPanel(panelIndex)
                End If

            Case "creditPurchase"
                If panelIndex = 9 Then
                    startPaymentIndex += 1
                    DisplayCreditPurchasePaymentPanels()
                    ActiveThisPanel(9)
                Else
                    panelIndex += 1
                    ActiveThisPanel(panelIndex)
                End If
            Case "creditReturn"
                If panelIndex = 9 Then
                    startPaymentIndex += 1
                    DisplayCreditReturnPaymentPanels()
                    ActiveThisPanel(9)
                Else
                    panelIndex += 1
                    ActiveThisPanel(panelIndex)
                End If
        End Select

    End Sub

    Private Sub DisplayBatchTotalPanel()

        Me.lblPOSNonAdjCount.Text = posNonAdjCount
        Me.lblPOSNonAdjDollar.Text = posNonAdjDollar
        Me.lblPOSCreditPurchaseCount.Text = posCreditPurchaseCount
        Me.lblPOSCreditPurchaseDollar.Text = posCreditPurchaseDollar
        Me.lblPOSCreditReturnCount.Text = posCreditReturnCount
        Me.lblPOSCreditReturnDollar.Text = posCreditReturnDollar
        Me.lblPOSNetCount.Text = posNetCount
        Me.lblPOSNetDollar.Text = posNetDollar

    End Sub


    Private Sub btnNonAdjusted_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNonAdjusted.Click
        activePanelDisplay = "nonAdjusted"
        ResetButonColorsPanelChoices()
        DisplayNonAdjustedPaymentPanels()

    End Sub

    Private Sub btnCreditPurchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreditPurchase.Click
        activePanelDisplay = "creditPurchase"
        ResetButonColorsPanelChoices()
        DisplayCreditPurchasePaymentPanels()

    End Sub

    Private Sub btnCreditReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreditReturn.Click
        activePanelDisplay = "creditReturn"
        ResetButonColorsPanelChoices()
        DisplayCreditReturnPaymentPanels()

    End Sub

    Private Sub btnNetBatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNetBatch.Click
        activePanelDisplay = "net"
        ResetButonColorsPanelChoices()
        DisplayNetBatchPanels()

    End Sub

    Private Sub btnDeclined_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeclined.Click
        activePanelDisplay = "batchDecline"
        ResetButonColorsPanelChoices()
        DisplayBatchDeclines()

    End Sub
    Private Sub btnSendAdjustments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendAdjustments.Click

        If notAllCash = False Then
            'this is all cash
            '     GenerateOrderTables.UpdatePaymentsAndCreditsBatch()
            GenerateOrderTables.StartDailyBusinessClose(actingManager.EmployeeID, BatchDailyCode)
            'insert Batch is only inserting in Phoenix
            ' NOT DOING NOW
            ' not sure if this is neccessary
            ' everything saved at Mercury or processor
            '   GenerateOrderTables.InsertBatchInfo(batch, BatchDailyCode)
            '  BeginningBatchClear()
            MsgBox("Batch Closed Successfully.") '" & batch.batchNumber & "
            dsOrder.Tables("QuickTickets").Rows.Clear()
            RaiseEvent ExitBatchClose(BatchDailyCode)
            Exit Sub
        End If

        If Me.btnSendAdjustments.Text = "Resume Close" Then

            If companyInfo.processor = "Mercury" Then
                SetupBatchSummary()
            ElseIf companyInfo.processor = "PaywarePC" Then
                StartBatchClosePayware()
            End If

        Else
            If posNonAdjCount + posCreditReturnCount > 0 Then
                If MsgBox("There is still '" & posNonAdjCount & "' Non-Adjusted check(s). If you continue, all non-adjusted gratuities will be zero.", MsgBoxStyle.OKCancel) = MsgBoxResult.Cancel Then
                    Exit Sub
                Else
                    ' have to replace all PreAuth TransCode's with PreAuthCaptures
                    '             Dim oRow As DataRow
                    '          For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
                    '        If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    '       If oRow("TransactionCode") = "PreAuth" Then
                    '      oRow("TransactionCode") = "PreAuthCapture"
                    ' End If
                    '    End If
                    '           Next
                End If

                SendBatchByRow()
            Else
                SendBatchByRow()
                '                SetupBatchSummary()
            End If



            '          mpsTStream = New TStream
            '           mpsAdmin = New AdminClass

            '          mpsAdmin.MerchantID = companyInfo.merchantID
            '         mpsAdmin.OperatorID = companyInfo.operatorID
            '        mpsAdmin.TranCode = "BatchClear"
            '       mpsAdmin.Memo = "spiderPOS " & companyInfo.VersionNumber

            '      mpsTStream.Admin = mpsAdmin

            '            Dim output As New StringWriter(New StringBuilder)
            '           Dim s As New XmlSerializer(GetType(TStream))
            '          s.Serialize(output, mpsTStream)

            '         StartBatchClear(output.ToString)

        End If

    End Sub

    Private Sub StartBatchClear(ByRef XMLString As String)

        Dim resp As String
        Dim batchStatus As String

        dsi.ServerIPConfig("x1.mercurypay.com;x2.mercurypay.com;b1.backuppay.com;b2.backuppay.com", 0)
        resp = dsi.ProcessTransaction(XMLString, 0, "", "")

        '   *** not sure what we get as a response
        '   Approved ... Success    ???

        sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendBatchClear.txt")
        sWriter1.Write(XMLString)
        sWriter1.Close()

        sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\BatchClear.txt")
        sWriter1.Write(resp)
        sWriter1.Close()

        batchStatus = ParseBatchResponse(resp, False)


        '      If batchStatus = "Success" Then
        '     SendBatchByRow()
        '    End If

    End Sub


    Private Sub SendBatchByRow()
        Dim oRow As DataRow

        '******************************************
        ' I don't think we are adjusting Gift Cards
        '******************************************
        'this runs the routine to send each payment through one at a time
        '   then marks for completion, and saves to db

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows      'dvBatchNotCaptured
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("BatchCleared") = False Then

                    If oRow("TransactionCode") = "PreAuth" Then
                        oRow("TransactionCode") = "PreAuthCapture"
                    End If
                    Try
                        If companyInfo.processor = "Mercury" Then
                            SetUpTransaction(oRow)
                        ElseIf companyInfo.processor = "PaywarePC" Then
                            If oRow("Tip") > 0 Then
                                StartGratuityByRow(oRow)
                            End If
                        End If

                    Catch ex As Exception
                        MsgBox(ex.Message)
                        MsgBox("There was a problem Sending Invoice " & oRow("RefNum") & " Payment through to Processor.")
                        '       oRow("TransactionCode") = "PreAuth"
                        isSomethingDeclined = True
                    End Try
                End If
            End If
        Next

        '      GenerateOrderTables.UpdatePaymentsAndCreditsBatch()
        '   perform batch summary automatically
        If isSomethingDeclined = False Then
            If companyInfo.processor = "Mercury" Then
                SetupBatchSummary()
            ElseIf companyInfo.processor = "PaywarePC" Then
                '   SetUpBatchSummaryPayware()
                StartBatchClosePayware()
            End If

        Else
            Me.btnSendAdjustments.Text = "Resume Close"
            pnlDeclined.Visible = True
            ResetButonColorsPanelChoices()
            DisplayBatchDeclines()

        End If

    End Sub

    Private Sub SetUpBatchSummaryPayware()




    End Sub

    Private Sub SetupBatchSummary()

        mpsTStream = New TStream
        mpsAdmin = New AdminClass

        mpsAdmin.MerchantID = companyInfo.merchantID
        mpsAdmin.OperatorID = companyInfo.operatorID
        mpsAdmin.TranCode = "BatchSummary"
        mpsAdmin.Memo = "spiderPOS " & companyInfo.VersionNumber

        mpsTStream.Admin = mpsAdmin

        Dim output As New StringWriter(New StringBuilder)
        Dim s As New XmlSerializer(GetType(TStream))
        s.Serialize(output, mpsTStream)

        StartBatchSumary(output.ToString)

    End Sub

    Private Sub StartBatchSummaryPayware()





    End Sub

    Private Sub StartBatchSumary(ByRef XMLString As String)

        Dim resp As String
        Dim batchStatus As String

        dsi.ServerIPConfig("x1.mercurypay.com;x2.mercurypay.com;b1.backuppay.com;b2.backuppay.com", 0)
        resp = dsi.ProcessTransaction(XMLString, 0, "", "")

        '   *** not sure what we get as a response
        '   Approved ... Success    ???

        sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendBatchSummary.txt")
        sWriter1.Write(XMLString)
        sWriter1.Close()


        sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\BatchSummary.txt")
        sWriter1.Write(resp)
        sWriter1.Close()

        batchStatus = ParseBatchResponse(resp, False)

        If batchStatus = "Success" Then
            Me.lblBatchSummary.Text = "Batch: " & batch.batchNumber
            Me.lblBatchNetCount.Text = batch.NetCount
            Me.lblBatchNetDollar.Text = batch.NetDollar
            Me.lblBatchCreditPurchaseCount.Text = batch.CreditPurchaseCount
            Me.lblBatchCreditPurchaseDollar.Text = batch.CreditPurchaseDollar
            Me.lblBatchCreditReturnCount.Text = batch.CreditReturnCount
            Me.lblBatchCreditReturnDollar.Text = batch.CreditReturnDollar

        End If

        If posNetCount = batch.NetCount And posNetDollar = batch.NetDollar Then
            SetupBatchClose()
        Else
            MsgBox("There is a difference in Totals. Please verify.")
            btnFinalize.Visible = True
            btnPrintAuth.Visible = True
            '   *** need to wait for user to Complete Batch Close
        End If

    End Sub

    Private Sub btnFinalize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinalize.Click
        'we have to wait until the user compares batch summary info

        SetupBatchClose()

    End Sub

    Private Sub btnPrintAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintAuth.Click

        PrintDetail()

    End Sub

    Private Sub PrintDetail()
        Dim prt As New PrintHelper
        prt.PrintAllAuthDuringBatch()
        printedDetail = True

      

    End Sub



    Private Sub SetupBatchClose()
        If batch.batchNumber Is Nothing Then Exit Sub


        mpsTStream = New TStream
        mpsAdmin = New AdminClass

        With mpsAdmin
            .MerchantID = companyInfo.merchantID
            .OperatorID = companyInfo.operatorID
            .TranCode = "BatchClose"
            .Memo = "spiderPOS " & companyInfo.VersionNumber
            .BatchNo = batch.batchNumber
            .BatchItemCount = batch.NetCount.ToString
            .NetBatchTotal = batch.NetDollar.ToString
            .CreditPurchaseCount = batch.CreditPurchaseCount
            .CreditPurchaseAmount = batch.CreditPurchaseDollar.ToString
            .CreditReturnCount = batch.CreditReturnCount
            .CreditReturnAmount = batch.CreditReturnDollar.ToString
            .DebitPurchaseCount = batch.DebitPurchaseCount
            .DebitPurchaseAmount = batch.DebitPurchaseDollar.ToString
            .DebitReturnCount = batch.DebitReturnCount
            .DebitReturnAmount = batch.DebitReturnDollar.ToString

        End With

        mpsTStream.Admin = mpsAdmin

        Dim output As New StringWriter(New StringBuilder)
        Dim s As New XmlSerializer(GetType(TStream))
        s.Serialize(output, mpsTStream)

        StartBatchClose(output.ToString)

    End Sub

    Private Sub StartBatchClose(ByRef XMLString As String)

        Dim oRow As DataRow
        Dim resp As String
        Dim batchStatus As String

        dsi.ServerIPConfig("x1.mercurypay.com;x2.mercurypay.com;b1.backuppay.com;b2.backuppay.com", 0)
        resp = dsi.ProcessTransaction(XMLString, 0, "", "")

        sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendBatchClose.txt")
        sWriter1.Write(XMLString)
        sWriter1.Close()

        sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\BatchClose.txt")
        sWriter1.Write(resp)
        sWriter1.Close()

        batchStatus = ParseBatchResponse(resp, False)

        If batchStatus = "Success" Then
            CompleteBatchClose()
        End If

    End Sub

    Private Sub CompleteBatchClose()

        Dim oRow As DataRow

        If CInt(Me.batchCloseNumber) = CInt(batch.batchNumber) Then
            If Me.batchCloseNetCount = batch.NetCount And Me.batchCloseNetDollar = batch.NetDollar Then
                'this is a successful batch close
                'we need to save into Batch Table
                For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
                    If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                        '    If oRow("PaymentFlag") Then        *** may have to add 
                        If Not oRow("AccountNumber").Substring(0, 4) = "xxxx" And Not oRow("AccountNumber") = "Manual" Then
                            oRow("AccountNumber") = TruncateAccountNumber(oRow("AccountNumber"))
                        Else

                        End If

                    End If
                Next
                GenerateOrderTables.UpdatePaymentsAndCreditsBatch()
                GenerateOrderTables.StartDailyBusinessClose(actingManager.EmployeeID, BatchDailyCode)
                'insert Batch is only inserting in Phoenix
                ' NOT DOING NOW
                ' not sure if this is neccessary
                ' everything saved at Mercury or processor
                GenerateOrderTables.InsertBatchInfo(batch, BatchDailyCode)
                BeginningBatchClear()
                MsgBox("Batch " & batch.batchNumber & " Closed Successfully.")
                dsOrder.Tables("QuickTickets").Rows.Clear()
                RaiseEvent ExitBatchClose(BatchDailyCode)

            Else
                MsgBox("There is a difference in Batch Totals.")
                MsgBox("Net Count: " & batch.NetCount & "         Net Dollar: " & batch.NetDollar)

                '***************
                '***************
                '***************
                '   *** don't know what to do 

            End If
        End If
    End Sub
    Private Sub StartBatchClosePayware()

        Dim batchStatus As String
        PaywarePCCharge = New SIM.Charge

        GenerateOrderTables.ReadyToProcessPaywarePC(PaywarePCCharge)

        With PaywarePCCharge

            .Action = SIM.Charge.Command.Batch_Credit_Settle

            If .Process Then

                '      MsgBox(.GetXMLResponse)
                '    MsgBox(.GetSettlementDetails)
                '         MsgBox(.GetTermStatus)
                '        MsgBox(.GetBatchTraceID)
                '       MsgBox(.GetResultCode)
                '      MsgBox(.GetTransSeqNum)

                Try
                    batchStatus = ParseBatchResponsePayware(.GetXMLResponse)

                    If batchStatus = "Success" Then '"SUCCESS" Then '.GetBatchResult = "SETTLED" Then
                        MsgBox("Batch '" & batchCloseNumber & "' " & batchStatus)
                        batch.batchNumber = batchCloseNumber
                        BatchSettledPayware()

                    Else
                        MsgBox("Batch '" & batchCloseNumber & "' " & batchStatus)
                    End If

                Catch ex As Exception

                End Try

                '            MsgBox(.GetResult)
                '           MsgBox(.GetAuthCode)
                '          MsgBox(.GetReference)
                '         MsgBox(.GetResultCode)
                '        MsgBox(.GetTroutD)
                '       MsgBox(.GetResponseText)

            Else
                MsgBox("" & .ErrorCode & ": " & .ErrorDescription)
            End If
        End With

    End Sub

    Private Sub BatchSettledPayware()

        Dim oRow As DataRow

        '   If Me.batchCloseNetCount = batch.NetCount And Me.batchCloseNetDollar = batch.NetDollar Then
        'this is a successful batch close
        'we need to save into Batch Table
        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                '    If oRow("PaymentFlag") Then        *** may have to add 
                If Not oRow("AccountNumber").Substring(0, 4) = "xxxx" And Not oRow("AccountNumber") = "Manual" Then
                    oRow("AccountNumber") = TruncateAccountNumber(oRow("AccountNumber"))
                Else
                End If

            End If
        Next
        GenerateOrderTables.UpdatePaymentsAndCreditsBatch()
        GenerateOrderTables.StartDailyBusinessClose(actingManager.EmployeeID, BatchDailyCode)
        'insert Batch is only inserting in Phoenix
        ' NOT DOING NOW
        ' not sure if this is neccessary
        ' everything saved at Mercury or processor
        GenerateOrderTables.InsertBatchInfo(batch, BatchDailyCode)
        If companyInfo.processor = "Mercury" Then
            BeginningBatchClear()
        End If
        MsgBox("Batch " & batch.batchNumber & " Closed Successfully.")
        dsOrder.Tables("QuickTickets").Rows.Clear()
        RaiseEvent ExitBatchClose(BatchDailyCode)

        '     Else
        '      MsgBox("There is a difference in Batch Totals.")
        '     MsgBox("Net Count: " & batch.NetCount & "         Net Dollar: " & batch.NetDollar)

        '***************
        '***************
        '***************
        '   *** don't know what to do 

        '   End If

    End Sub

    Private Sub BeginningBatchClear()

        If companyInfo.processor = "Mercury" Then
            ' maybe here is where we need to set
            '    oRow("BatchCleared") = True
        ElseIf companyInfo.processor = "PaywarePC" Then

        End If

        mpsTStream = New TStream
        mpsAdmin = New AdminClass

        mpsAdmin.MerchantID = companyInfo.merchantID
        mpsAdmin.OperatorID = companyInfo.operatorID
        mpsAdmin.TranCode = "BatchClear"
        mpsAdmin.Memo = "spiderPOS " & companyInfo.VersionNumber

        mpsTStream.Admin = mpsAdmin

        Dim output As New StringWriter(New StringBuilder)
        Dim s As New XmlSerializer(GetType(TStream))
        s.Serialize(output, mpsTStream)

        StartBatchClear(output.ToString)



    End Sub
    Private Sub SetUpTransaction(ByRef oRow As DataRow)

        mpsTStream = New TStream
        mpsTransaction = New PreAuthTransactionClass
        mpsAmount = New PreAuthAmountClass
        mpsAccount = New AccountClass
        mpsTransInfo = New TranInfoClass

        '     With oRow
        mpsTransaction.MerchantID = companyInfo.merchantID
        mpsTransaction.OperatorID = companyInfo.operatorID
        mpsTransaction.TranType = oRow("TransactionType")
        If oRow("Duplicate") = True Then
            '           mpsTransaction.Duplicate = "Override"
        End If
        mpsTransaction.TranCode = oRow("TransactionCode")
        mpsTransaction.InvoiceNo = oRow("RefNum")
        mpsTransaction.RefNo = oRow("RefNum")
        mpsTransaction.Memo = "spiderPOS " & companyInfo.VersionNumber

        mpsAccount.AcctNo = oRow("AccountNumber")
        mpsAccount.ExpDate = oRow("CCExpiration")

        If mpsTransaction.TranCode = "Return" Then
            mpsAmount.Purchase = (-1 * oRow("PaymentAmount"))
        ElseIf mpsTransaction.TranCode = "VoiceAuth" Then
            mpsAmount.Purchase = oRow("PaymentAmount")
            mpsTransInfo.AuthCode = oRow("AuthCode")
        ElseIf mpsTransaction.TranCode = "PreAuthCapture" Then
            mpsAmount.Purchase = oRow("PaymentAmount")
            mpsAmount.Gratuity = Format(oRow("Tip"), "####0.00")
            mpsAmount.Authorize = oRow("PreAuthAmount")
            mpsTransInfo.AuthCode = oRow("AuthCode")
            If Not oRow("AcqRefData") Is DBNull.Value Then
                mpsTransInfo.AcqRefData = oRow("AcqRefData")
            End If
        End If

        '     End With
        '*****************
        '   for testing only
        '       mpsAccount.AcctNo = "5499990123456781"
        '      mpsAccount.ExpDate = "0809"
        '     mpsAccount.Track2 = Nothing
        '   end testing
        '*****************


        mpsTransaction.Account = mpsAccount
        mpsTransaction.Amount = mpsAmount
        If mpsTransaction.TranCode = "PreAuthCapture" Or mpsTransaction.TranCode = "VoiceAuth" Then
            mpsTransaction.TranInfo = mpsTransInfo
        End If

        mpsTStream.Transaction = mpsTransaction


        Dim output As New StringWriter(New StringBuilder)
        Dim s As New XmlSerializer(GetType(TStream))
        s.Serialize(output, mpsTStream)

        StartTransaction(output.ToString, oRow)

    End Sub

    Private Sub StartTransaction(ByRef XMLString As String, ByRef oRow As DataRow)

        Dim resp As String
        Dim authStatus As String

        dsi.ServerIPConfig("x1.mercurypay.com;x2.mercurypay.com;b1.backuppay.com;b2.backuppay.com", 0)
        resp = dsi.ProcessTransaction(XMLString, 0, "", "")

        'just for testing 
        If oRow("TransactionCode") = "Return" Then
            '          sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendReturn.txt")
            '         sWriter1.Write(XMLString)
            '        sWriter1.Close()
        ElseIf oRow("TransactionCode") = "VoiceAuth" Then
            '           sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendVoiceAuth.txt")
            '          sWriter1.Write(XMLString)
            '         sWriter1.Close()
        ElseIf oRow("TransactionCode") = "PreAuthCapture" Then
            If oRow("Duplicate") = True Then
                '            sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendDuplicateCapture.txt")
                '           sWriter1.Write(XMLString)
                '          sWriter1.Close()
            Else
                '              sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendPreAuthCapture.txt")
                '             sWriter1.Write(XMLString)
                '            sWriter1.Close()
            End If

        End If

        '       sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\BatchTransactionResponse.txt")
        '      sWriter1.Write(resp)
        '     sWriter1.Close()
        '       MsgBox(resp)

        '   *** not sure what we get as a response
        '   Approved ... Success    ???
        authStatus = ParseTransactions(resp, True, oRow)

        If authStatus = "Approved" Then     'maybe Success
            '          oRow("AccountNumber") = TruncateAccountNumber(oRow("AccountNumber"))
            oRow("BatchCleared") = True
            If oRow("TransactionCode") = "PreAuth" Then
                oRow("TransactionCode") = "PreAuthCapture"
            End If
        Else
            isSomethingDeclined = True

        End If

    End Sub

    Private Function ParseTransactions(ByVal resp As String, ByVal isForCapture As Boolean, ByRef oRow As DataRow)

        Dim reader As New XmlTextReader(New StringReader(resp))
        Dim someError As Boolean
        Dim isSuccess As Boolean
        Dim batchStatus As String

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
                                    batchStatus = "Approved"

                                Case "Success"
                                    isSuccess = True
                                    batchStatus = "Success"
                                    '                                   MsgBox(reader.ReadInnerXml)
                                Case "Declined"
                                    batchStatus = "Declined"

                                Case "Error"
                                    '      MsgBox(reader.ReadInnerXml)
                                    batchStatus = "Error"

                            End Select
                            If isForCapture = True Then Exit Do

                        Case "TextResponse"
                            If someError = True Then
                                oRow("Description") = reader.ReadInnerXml
                                If reader.ReadInnerXml = "AP DUPE" Then
                                    oRow("Duplicate") = True
                                ElseIf reader.ReadInnerXml = "NO DUP FOUND" Then
                                    oRow("Duplicate") = False
                                End If
                                Exit Function

                            ElseIf batchStatus = "Declined" Then

                                oRow("Description") = reader.ReadInnerXml
                                Exit Function

                            Else

                            End If

                        Case "UserTraceData"

                    End Select
                End If
            Loop
        Catch ex As Exception

        Finally
            If Not reader Is Nothing Then
                reader.Close()
            End If

        End Try

        Return batchStatus

    End Function

    Private Sub StartGratuityByRow(ByRef oRow As DataRow)

        PaywarePCCharge = New SIM.Charge

        GenerateOrderTables.ReadyToProcessPaywarePC(PaywarePCCharge)

        With PaywarePCCharge

            .GratuityAmount = (Format(oRow("Tip"), "#####0.00")).ToString
            .TroutD = oRow("RefNum")
            .Action = SIM.Charge.Command.Credit_Add_Tip

            If .Process Then
                Try
                    '    If .GetResult = "CAPTURED" Or .GetResultCode = "4" Then
                    If .GetResult = "TIP MODIFIED" Or .GetResultCode = "17" Then
                        'above is the same
                        '444         oRow("TransactionCode") = "PreAuthCapture"
                        oRow("BatchCleared") = True 'we amy need to set to true after Batch settled??

                    Else ' If .GetResult = "DECLINED" Or .GetResultCode = "6" Then
                        MsgBox("CARD '" & oRow("AccountNumber") & "' " & .GetResult & ": " & .GetResponseText)
                        'MsgBox("CARD '" & vRow("AccountNumber") & "' DECLINED: " & .GetResponseText)
                    End If

                Catch ex As Exception

                End Try

                '            MsgBox(.GetResult)
                '           MsgBox(.GetAuthCode)
                '          MsgBox(.GetReference)
                '         MsgBox(.GetResultCode)
                '        MsgBox(.GetTroutD)
                '       MsgBox(.GetResponseText)

            Else
                MsgBox("" & .ErrorCode & ": " & .ErrorDescription)
            End If
        End With
    End Sub

    Private Function ParseBatchResponsePayware(ByVal resp As String) ', ByVal isForCapture As Boolean)

        Dim reader As New XmlTextReader(New StringReader(resp))
        Dim someError As Boolean
        Dim isSuccess As Boolean
        Dim batchStatus As String
        Dim isBatchSummary As Boolean
        Dim isBatchClose As Boolean


        Try
            Do Until reader.EOF = True
                reader.Read()
                reader.MoveToContent()
                If reader.NodeType = XmlNodeType.Element Then
                    '         MsgBox(reader.Name)
                    Select Case reader.Name

                        Case "TERMINATION_STATUS"
                            Select Case reader.ReadInnerXml

                                Case "SUCCESS"
                                    isSuccess = True
                                    batchStatus = "Success"

                                Case "ERROR"
                                    batchStatus = "Error"


                            End Select
                            ' If isForCapture = True Then Exit Do

                        Case "BATCH_SEQ_NUM"

                            If isBatchSummary = True Then
                                batch.batchNumber = reader.ReadInnerXml
                            ElseIf isBatchClose = True Then
                                batchCloseNumber = reader.ReadInnerXml
                            End If

                        Case "BATCH_COUNT"
                            If isBatchSummary = True Then
                                batch.NetCount = reader.ReadInnerXml
                            ElseIf isBatchClose = True Then
                                batchCloseNetCount = reader.ReadInnerXml
                            End If

                        Case "BATCH_BALANCE"
                            If isBatchSummary = True Then
                                batch.NetDollar = reader.ReadInnerXml
                            ElseIf isBatchClose = True Then
                                batchCloseNetDollar = reader.ReadInnerXml
                            End If

                        Case "CreditPurchaseCount"
                            If isBatchSummary = True Then
                                batch.CreditPurchaseCount = reader.ReadInnerXml
                                '         MsgBox(batch.CreditPurchaseCount)
                            End If


                        Case "CreditPurchaseAmount"
                            If isBatchSummary = True Then
                                batch.CreditPurchaseDollar = reader.ReadInnerXml
                            End If

                        Case "CreditReturnCount"
                            If isBatchSummary = True Then
                                batch.CreditReturnCount = reader.ReadInnerXml
                            End If

                        Case "CreditReturnAmount"
                            If isBatchSummary = True Then
                                batch.CreditReturnDollar = reader.ReadInnerXml
                            End If


                        Case "DebitPurchaseCount"
                            If isBatchSummary = True Then
                                batch.DebitPurchaseCount = reader.ReadInnerXml
                            End If

                        Case "DebitPurchaseAmount"
                            If isBatchSummary = True Then
                                batch.DebitPurchaseDollar = reader.ReadInnerXml
                            End If

                        Case "DebitReturnCount"
                            If isBatchSummary = True Then
                                batch.DebitReturnCount = reader.ReadInnerXml
                            End If

                        Case "DebitReturnAmount"
                            If isBatchSummary = True Then
                                batch.DebitReturnDollar = reader.ReadInnerXml
                            End If


                    End Select
                End If
            Loop
        Catch ex As Exception

        Finally
            If Not reader Is Nothing Then
                reader.Close()
            End If

        End Try

        Return batchStatus

    End Function

    Private Function ParseBatchResponse(ByVal resp As String, ByVal isForCapture As Boolean)

        Dim reader As New XmlTextReader(New StringReader(resp))
        Dim someError As Boolean
        Dim isSuccess As Boolean
        Dim batchStatus As String
        Dim isBatchSummary As Boolean
        Dim isBatchClose As Boolean


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
                                    batchStatus = "Approved"

                                Case "Success"
                                    isSuccess = True
                                    batchStatus = "Success"
                                    '                                   MsgBox(reader.ReadInnerXml)
                                Case "Declined"
                                    batchStatus = "Declined"

                                Case "Error"
                                    '      MsgBox(reader.ReadInnerXml)
                                    batchStatus = "Error"

                            End Select
                            If isForCapture = True Then Exit Do

                        Case "TextResponse"
                            If someError = True Then
                                MsgBox(reader.ReadInnerXml)
                                Exit Function
                            ElseIf batchStatus = "Declined" Then
                                MsgBox(reader.ReadInnerXml)
                                Exit Function
                            Else

                            End If

                        Case "UserTraceData"

                            '                         **************************************
                            '                                 Transaction Response
                            '                         **************************************

                        Case "BatchSummary"
                            If isSuccess = True Then
                                isBatchSummary = True
                            End If

                        Case "BatchClose"
                            If isSuccess = True Then
                                isBatchClose = True
                            End If

                        Case "BatchNo"

                            If isBatchSummary = True Then
                                batch.batchNumber = reader.ReadInnerXml
                            ElseIf isBatchClose = True Then
                                batchCloseNumber = reader.ReadInnerXml
                            End If

                        Case "BatchItemCount"
                            If isBatchSummary = True Then
                                batch.NetCount = reader.ReadInnerXml
                            ElseIf isBatchClose = True Then
                                batchCloseNetCount = reader.ReadInnerXml
                            End If

                        Case "NetBatchTotal"
                            If isBatchSummary = True Then
                                batch.NetDollar = reader.ReadInnerXml
                            ElseIf isBatchClose = True Then
                                batchCloseNetDollar = reader.ReadInnerXml
                            End If

                        Case "CreditPurchaseCount"
                            If isBatchSummary = True Then
                                batch.CreditPurchaseCount = reader.ReadInnerXml
                                '         MsgBox(batch.CreditPurchaseCount)
                            End If


                        Case "CreditPurchaseAmount"
                            If isBatchSummary = True Then
                                batch.CreditPurchaseDollar = reader.ReadInnerXml
                            End If

                        Case "CreditReturnCount"
                            If isBatchSummary = True Then
                                batch.CreditReturnCount = reader.ReadInnerXml
                            End If

                        Case "CreditReturnAmount"
                            If isBatchSummary = True Then
                                batch.CreditReturnDollar = reader.ReadInnerXml
                            End If


                        Case "DebitPurchaseCount"
                            If isBatchSummary = True Then
                                batch.DebitPurchaseCount = reader.ReadInnerXml
                            End If

                        Case "DebitPurchaseAmount"
                            If isBatchSummary = True Then
                                batch.DebitPurchaseDollar = reader.ReadInnerXml
                            End If

                        Case "DebitReturnCount"
                            If isBatchSummary = True Then
                                batch.DebitReturnCount = reader.ReadInnerXml
                            End If

                        Case "DebitReturnAmount"
                            If isBatchSummary = True Then
                                batch.DebitReturnDollar = reader.ReadInnerXml
                            End If


                    End Select
                End If
            Loop
        Catch ex As Exception

        Finally
            If Not reader Is Nothing Then
                reader.Close()
            End If

        End Try

        Return batchStatus

    End Function


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If changesMadeToTips = True Then
            GenerateOrderTables.UpdatePaymentsAndCreditsBatch()
        End If

        RaiseEvent ExitWithoutClose()

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim oRow As DataRow

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows      'dvBatchNotCaptured
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("BatchCleared") = True Then
                    oRow("BatchCleared") = False
                End If
            End If
        Next
        Me.btnSendAdjustments.Text = "Close Batch"
        isSomethingDeclined = False
        changesMadeToTips = True 'this is so we save any changes upon exit
        ResetButonColorsPanelChoices()
        DisplayCreditPurchasePaymentPanels()

    End Sub





    Private Sub FilterBatchDataview222()

        dvBatchPreAuth = New DataView
        dvBatchPreAuthCapture = New DataView
        dvBatchVoiceAuth = New DataView
        dvBatchReturn = New DataView

        With dvBatchVoiceAuth
            .Table = dsOrder.Tables("PaymentsAndCredits")
            .RowFilter = "TransactionCode = 'VoiceAuth'"
            '       .Sort = "TransactionCode, AuthCode ASC"
        End With

        With dvBatchPreAuth
            .Table = dsOrder.Tables("PaymentsAndCredits")
            .RowFilter = "TransactionCode = 'PreAuth'"
            '       .Sort = "TransactionCode, AuthCode ASC"
        End With

        With dvBatchPreAuthCapture
            .Table = dsOrder.Tables("PaymentsAndCredits")
            .RowFilter = "TransactionCode = 'PreAuthCapture'"
            '       .Sort = "TransactionCode, AuthCode ASC"
        End With

        With dvBatchReturn
            .Table = dsOrder.Tables("PaymentsAndCredits")
            .RowFilter = "TransactionCode = 'Return'"
            '       .Sort = "TransactionCode, AuthCode ASC"
        End With



    End Sub


    Private Sub CreateNewPaymentPanels222()
        'this is only done once

        Dim vRow As DataRowView
        Dim PnlNo As Integer = 1
        Dim position As Integer
        Dim empName As String
        Dim truncAcctNum As String
        Me.pnlBatchPayments.Controls.Clear()



        For Each vRow In dvBatchVoiceAuth
            empName = vRow("FirstName") & " " & vRow("LastName")
            If Not vRow("AccountNumber").Substring(0, 4) = "xxxx" And Not vRow("AccountNumber") = "Manual" Then
                truncAcctNum = TruncateAccountNumber(vRow("AccountNumber"))
            Else
                truncAcctNum = (vRow("AccountNumber"))
            End If
            paymentPanel(PnlNo) = New DataSet_Builder.Payment_UC("Batch", vRow, Nothing, PnlNo, empName, truncAcctNum, 0)
            With paymentPanel(PnlNo)
                .BackColor = Color.DarkGray

                AddHandler paymentPanel(PnlNo).ActivePanel, AddressOf PaymentUserControl_Click
            End With
            PnlNo += 1
        Next

        If dvBatchVoiceAuth.Count > 0 Then
            startVoiceIndex = 1
        Else
            startVoiceIndex = 0
        End If
        endVoiceIndex = dvBatchVoiceAuth.Count


        For Each vRow In dvBatchPreAuth
            empName = vRow("FirstName") & " " & vRow("LastName")
            If Not vRow("AccountNumber").Substring(0, 4) = "xxxx" And Not vRow("AccountNumber") = "Manual" Then
                truncAcctNum = TruncateAccountNumber(vRow("AccountNumber"))
            Else
                truncAcctNum = (vRow("AccountNumber"))
            End If
            paymentPanel(PnlNo) = New DataSet_Builder.Payment_UC("Batch", vRow, Nothing, PnlNo, empName, truncAcctNum, 0)
            With paymentPanel(PnlNo)
                .BackColor = Color.DarkGray

                AddHandler paymentPanel(PnlNo).ActivePanel, AddressOf PaymentUserControl_Click
            End With
            PnlNo += 1
        Next

        If dvBatchPreAuth.Count > 0 Then
            startPreAuthIndex = dvBatchVoiceAuth.Count + 1
        Else
            startPreAuthIndex = dvBatchVoiceAuth.Count
        End If
        endPreAuthIndex = endVoiceIndex + dvBatchPreAuth.Count

        For Each vRow In dvBatchPreAuthCapture
            empName = vRow("FirstName") & " " & vRow("LastName")
            If Not vRow("AccountNumber").Substring(0, 4) = "xxxx" And Not vRow("AccountNumber") = "Manual" Then
                truncAcctNum = TruncateAccountNumber(vRow("AccountNumber"))
            Else
                truncAcctNum = (vRow("AccountNumber"))
            End If
            paymentPanel(PnlNo) = New DataSet_Builder.Payment_UC("Batch", vRow, Nothing, PnlNo, empName, truncAcctNum, 0)
            With paymentPanel(PnlNo)
                .BackColor = Color.DarkGray

                AddHandler paymentPanel(PnlNo).ActivePanel, AddressOf PaymentUserControl_Click
            End With
        Next

        If dvBatchPreAuthCapture.Count > 0 Then
            startPreAuthCaptureIndex = endPreAuthIndex + 1
        Else
            startPreAuthCaptureIndex = endPreAuthIndex
        End If
        endPreAuthCaptureIndex = endPreAuthIndex + dvBatchPreAuthCapture.Count

        For Each vRow In dvBatchReturn
            empName = vRow("FirstName") & " " & vRow("LastName")
            If Not vRow("AccountNumber").Substring(0, 4) = "xxxx" And Not vRow("AccountNumber") = "Manual" Then
                truncAcctNum = TruncateAccountNumber(vRow("AccountNumber"))
            Else
                truncAcctNum = (vRow("AccountNumber"))
            End If
            paymentPanel(PnlNo) = New DataSet_Builder.Payment_UC("Batch", vRow, Nothing, PnlNo, empName, truncAcctNum, 0)
            With paymentPanel(PnlNo)
                .BackColor = Color.DarkGray
                AddHandler paymentPanel(PnlNo).ActivePanel, AddressOf PaymentUserControl_Click
            End With
        Next

        If dvBatchReturn.Count > 0 Then
            startReturnIndex = endPreAuthCaptureIndex + 1
        Else
            startReturnIndex = endPreAuthCaptureIndex
        End If
        endReturnIndex = endPreAuthCaptureIndex + dvBatchReturn.Count


    End Sub

    Private Sub DisplayNonAdjustedPaymentPanels222()
        '   we display by index values in case of deleted payments 
        '   they would not reflect in dataviews
        '   we can do some kind of Mark As Deleted in paymentPanel

        Dim PnlNo As Integer
        Dim position As Integer = 0
        Me.btnNonAdjusted.BackColor = c11
        Me.btnNonAdjusted.ForeColor = c3

        Me.pnlBatchPayments.Controls.Clear()


        If dvBatchVoiceAuth.Count > 0 Then
            For PnlNo = 1 To endVoiceIndex
                If paymentPanel(PnlNo).TipAmount = 0 Then
                    With paymentPanel(PnlNo)
                        .Location = New Point(0, (.Height * position))
                        .ReverseAlignment()
                        Me.pnlBatchPayments.Controls.Add(paymentPanel(PnlNo))
                    End With
                    position += 1
                End If
            Next
        End If

        If dvBatchPreAuth.Count > 0 Then
            For PnlNo = startPreAuthIndex To endPreAuthIndex
                With paymentPanel(PnlNo)
                    .Location = New Point(0, (.Height * position))
                    .ReverseAlignment()
                    Me.pnlBatchPayments.Controls.Add(paymentPanel(PnlNo))
                End With
                position += 1
            Next
        End If



    End Sub

    Private Sub DisplayCreditPurchasePaymentPanels222()

        Dim PnlNo As Integer
        Dim position As Integer = 0
        Me.btnNonAdjusted.BackColor = c11
        Me.btnNonAdjusted.ForeColor = c3

        Me.pnlBatchPayments.Controls.Clear()


        If dvBatchVoiceAuth.Count > 0 Then
            For PnlNo = 1 To endVoiceIndex
                With paymentPanel(PnlNo)
                    .Location = New Point(0, (.Height * position))
                    If paymentPanel(PnlNo).TipAmount = 0 Then
                        .ReverseAlignment()
                    Else
                        .StandardAlignment()
                    End If
                    Me.pnlBatchPayments.Controls.Add(paymentPanel(PnlNo))
                End With
                position += 1
            Next
        End If


        If dvBatchPreAuthCapture.Count > 0 Then
            For PnlNo = startPreAuthCaptureIndex To endPreAuthCaptureIndex
                With paymentPanel(PnlNo)
                    .Location = New Point(0, (.Height * position))
                    '          .StandardAlignment()
                    Me.pnlBatchPayments.Controls.Add(paymentPanel(PnlNo))
                End With
                position += 1
            Next
        End If


    End Sub

    Private Sub DisplayCreditRetrunPaymentPanels222()

        Dim PnlNo As Integer
        Dim position As Integer = 0
        Me.btnNonAdjusted.BackColor = c11
        Me.btnNonAdjusted.ForeColor = c3

        Me.pnlBatchPayments.Controls.Clear()


        If dvBatchReturn.Count > 0 Then
            For PnlNo = startReturnIndex To endReturnIndex
                With paymentPanel(PnlNo)
                    .Location = New Point(0, (.Height * position))
                    '       .StandardAlignment()
                    Me.pnlBatchPayments.Controls.Add(paymentPanel(PnlNo))
                End With
                position += 1
            Next
        End If


    End Sub

    Private Sub btnDailyNextPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDailyNextPage.Click

        Select Case activePanelDisplay

            Case "nonAdjusted"
                If naPanelIndex >= Me.posNonAdjCount Then Exit Sub

                naPanelIndex += 9
                Me.DisplayNonAdjustedPaymentPanels()

            Case "creditPurchase"
                If cpPanelIndex >= Me.posCreditPurchaseCount Then Exit Sub
                cpPanelIndex += 9
                Me.DisplayCreditPurchasePaymentPanels()

            Case "creditReturn"
                If naPanelIndex >= Me.posCreditReturnCount Then Exit Sub
                crPanelIndex += 9
                Me.DisplayCreditReturnPaymentPanels()

            Case "batchDecline"
                If naPanelIndex >= Me.posNetCount Then Exit Sub

                bdPanelIndex += 9
                Me.DisplayBatchDeclines()
            Case "net"
                If netPanelIndex >= Me.posNetCount Then Exit Sub
                netPanelIndex += 9
                Me.DisplayBatchTotalPanel()

        End Select

    End Sub

    Private Sub btnDailyPreviousPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDailyPreviousPage.Click



        Select Case activePanelDisplay

            Case "nonAdjusted"
                If naPanelIndex <= 1 Then Exit Sub
                naPanelIndex -= 9
                Me.DisplayNonAdjustedPaymentPanels()

            Case "creditPurchase"
                If cpPanelIndex <= 1 Then Exit Sub
                cpPanelIndex -= 9
                Me.DisplayCreditPurchasePaymentPanels()

            Case "creditReturn"
                If crPanelIndex <= 1 Then Exit Sub
                crPanelIndex -= 9
                Me.DisplayCreditReturnPaymentPanels()

            Case "batchDecline"
                If bdPanelIndex <= 1 Then Exit Sub
                bdPanelIndex -= 9
                Me.DisplayBatchDeclines()
            Case "net"
                If netPanelIndex <= 1 Then Exit Sub
                netPanelIndex -= 9
                Me.DisplayBatchTotalPanel()

        End Select
    End Sub
End Class
