Imports System.ComponentModel
Imports DataSet_Builder

Public Class Manager_OrderAdj_UC
    Inherits System.Windows.Forms.UserControl

    '  Private sql As New DataSet_Builder.SQLHelper(connectserver)
    Dim prt As New PrintHelper

    Dim WithEvents checkTotalsMgmtAdj As CheckTotal_UC
    Dim WithEvents forcePriceMgmtAdj As ForcePrice_UC
    Dim WithEvents checkAdjMgmtAdj As CheckAdjustmentOverride_UC
    Dim WithEvents compTktMgmtAdj As Comp_Ticket_UC
    Dim WithEvents cashOut As CashOut_UC

    Dim voidedItems As Collection

    '   Friend AdjustCurrencyMan As CurrencyManager

    Dim forceRowNum As Integer
    Dim isForReopen As Boolean
    Dim madeChanges As Boolean

    Dim _transSIN As Integer
    Dim _transName As String

    Friend Property TransSIN() As Integer
        Get
            Return _transSIN
        End Get
        Set(ByVal Value As Integer)
            _transSIN = Value
        End Set
    End Property

    Friend Property TransName() As String
        Get
            Return _transName
        End Get
        Set(ByVal Value As String)
            _transName = Value
        End Set
    End Property


    Event SaveReopenedCheck()
    Event PlacingOrder()
    Event TransferingCheck()
    Event MgrClosingCheck()
    Event AddingItemToOrder(ByVal sender As Object)
    Event ReinitializeMain(ByVal saveChanges As Boolean, ByVal disposeOrdAdj As Boolean)
    Event VoidedCheckTableStatusChange(byval tn as Integer)

    '   *** not sure if this should be here
    '   it is also in CheckAdjustmentOverride_UC
    '   but I was getting can't find error sopmetimes
    Event UpdateAdjGridPayment()

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal isReopen As Boolean, ByVal expNum As Int64) 'ByVal cm As Integer, ByVal empID As Integer, ByVal IsTab As Boolean, ByVal expNum As Int64, ByVal numChecks As Integer, ByVal numCust As Integer)
        MyBase.New()

        '   *** neeed to redo last staus for all management screens
        '   *** just like at the beggining of Table_Screen_Bar

        '      currentTable = New DinnerTable

        '     currentTable.CurrentMenu = cm
        '     currentTable.StartingMenu = cm
        '     currentTable.EmployeeID = empID
        '    If IsTab = True Then
        '    currentTable.TabID = tn
        '    Else
        ''        currentTable.TableNumber = tn
        '    End If
        '    currentTable.IsTabNotTable = IsTab
        '    currentTable.ExperienceNumber = expNum
        ''   currentTable.NumberOfChecks = numChecks
        ' currentTable.NumberOfCustomers = numCust

        '       currentTable.CheckNumber = 1            'this to start
        isForReopen = isReopen

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        InitializeOther(expNum)

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
    Friend WithEvents btnMgrCheckNumber As System.Windows.Forms.Button
    Friend WithEvents pnlMgrCheckInfo As System.Windows.Forms.Panel
    Friend WithEvents pnlMgrByItem As System.Windows.Forms.Panel
    Friend WithEvents pnlMgrByCheck As System.Windows.Forms.Panel
    Friend WithEvents lblMgrByItem As System.Windows.Forms.Label
    Friend WithEvents lblMgrByCheck As System.Windows.Forms.Label
    Friend WithEvents pnlMgrByPayments As System.Windows.Forms.Panel
    Friend WithEvents lblMgrByPayments As System.Windows.Forms.Label
    Friend WithEvents btnMgrVoidItem As System.Windows.Forms.Button
    Friend WithEvents btnMgrForcePrice As System.Windows.Forms.Button
    Friend WithEvents btnMgrCompItem As System.Windows.Forms.Button
    Friend WithEvents btnMgrReprintCheck As System.Windows.Forms.Button
    Friend WithEvents btnMgrReprintOrder As System.Windows.Forms.Button
    Friend WithEvents btnMgrVoidCheck As System.Windows.Forms.Button
    Friend WithEvents btnMgrReopenCheck As System.Windows.Forms.Button
    Friend WithEvents btnAdjustPay As System.Windows.Forms.Button
    Friend WithEvents btnMgrAssignComps As System.Windows.Forms.Button
    Friend WithEvents btnMgrAssignGratuity As System.Windows.Forms.Button
    Friend WithEvents btnManagerCancel As System.Windows.Forms.Button
    Friend WithEvents btnAdjAccept As System.Windows.Forms.Button
    Friend WithEvents pnlMgrCalculationArea As System.Windows.Forms.Panel
    Friend WithEvents btnMgrTransfer As System.Windows.Forms.Button
    Friend WithEvents btnMgrPlaceOrder As System.Windows.Forms.Button
    Friend WithEvents btnMgrReprintCredit As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblMgrPrinting As System.Windows.Forms.Label
    Friend WithEvents pnlCheckTotalFor_UC As System.Windows.Forms.Panel
    Friend WithEvents btnMgrCashBack As System.Windows.Forms.Button
    Friend WithEvents btnMgrTaxExempt As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Manager_OrderAdj_UC))
        Me.btnManagerCancel = New System.Windows.Forms.Button
        Me.btnMgrCheckNumber = New System.Windows.Forms.Button
        Me.pnlMgrCheckInfo = New System.Windows.Forms.Panel
        Me.btnAdjAccept = New System.Windows.Forms.Button
        Me.pnlMgrByItem = New System.Windows.Forms.Panel
        Me.btnMgrCompItem = New System.Windows.Forms.Button
        Me.btnMgrForcePrice = New System.Windows.Forms.Button
        Me.btnMgrVoidItem = New System.Windows.Forms.Button
        Me.lblMgrByItem = New System.Windows.Forms.Label
        Me.btnMgrPlaceOrder = New System.Windows.Forms.Button
        Me.pnlMgrByCheck = New System.Windows.Forms.Panel
        Me.btnMgrTaxExempt = New System.Windows.Forms.Button
        Me.btnMgrReopenCheck = New System.Windows.Forms.Button
        Me.btnMgrVoidCheck = New System.Windows.Forms.Button
        Me.lblMgrByCheck = New System.Windows.Forms.Label
        Me.btnMgrTransfer = New System.Windows.Forms.Button
        Me.btnMgrReprintOrder = New System.Windows.Forms.Button
        Me.btnMgrReprintCheck = New System.Windows.Forms.Button
        Me.pnlMgrByPayments = New System.Windows.Forms.Panel
        Me.btnMgrCashBack = New System.Windows.Forms.Button
        Me.btnMgrAssignGratuity = New System.Windows.Forms.Button
        Me.btnMgrAssignComps = New System.Windows.Forms.Button
        Me.btnAdjustPay = New System.Windows.Forms.Button
        Me.lblMgrByPayments = New System.Windows.Forms.Label
        Me.btnMgrReprintCredit = New System.Windows.Forms.Button
        Me.pnlMgrCalculationArea = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblMgrPrinting = New System.Windows.Forms.Label
        Me.pnlCheckTotalFor_UC = New System.Windows.Forms.Panel
        Me.pnlMgrCheckInfo.SuspendLayout()
        Me.pnlMgrByItem.SuspendLayout()
        Me.pnlMgrByCheck.SuspendLayout()
        Me.pnlMgrByPayments.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnManagerCancel
        '
        Me.btnManagerCancel.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnManagerCancel.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnManagerCancel.Location = New System.Drawing.Point(216, 8)
        Me.btnManagerCancel.Name = "btnManagerCancel"
        Me.btnManagerCancel.Size = New System.Drawing.Size(88, 32)
        Me.btnManagerCancel.TabIndex = 5
        Me.btnManagerCancel.Text = "Cancel"
        Me.btnManagerCancel.UseVisualStyleBackColor = False
        '
        'btnMgrCheckNumber
        '
        Me.btnMgrCheckNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.btnMgrCheckNumber.Location = New System.Drawing.Point(104, 0)
        Me.btnMgrCheckNumber.Name = "btnMgrCheckNumber"
        Me.btnMgrCheckNumber.Size = New System.Drawing.Size(104, 48)
        Me.btnMgrCheckNumber.TabIndex = 6
        Me.btnMgrCheckNumber.UseVisualStyleBackColor = False
        '
        'pnlMgrCheckInfo
        '
        Me.pnlMgrCheckInfo.BackColor = System.Drawing.Color.LightSlateGray
        Me.pnlMgrCheckInfo.Controls.Add(Me.btnAdjAccept)
        Me.pnlMgrCheckInfo.Controls.Add(Me.btnMgrCheckNumber)
        Me.pnlMgrCheckInfo.Controls.Add(Me.btnManagerCancel)
        Me.pnlMgrCheckInfo.Location = New System.Drawing.Point(64, 8)
        Me.pnlMgrCheckInfo.Name = "pnlMgrCheckInfo"
        Me.pnlMgrCheckInfo.Size = New System.Drawing.Size(312, 48)
        Me.pnlMgrCheckInfo.TabIndex = 7
        '
        'btnAdjAccept
        '
        Me.btnAdjAccept.BackColor = System.Drawing.Color.SlateGray
        Me.btnAdjAccept.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdjAccept.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnAdjAccept.Location = New System.Drawing.Point(0, 0)
        Me.btnAdjAccept.Name = "btnAdjAccept"
        Me.btnAdjAccept.Size = New System.Drawing.Size(104, 48)
        Me.btnAdjAccept.TabIndex = 7
        Me.btnAdjAccept.Text = "Accept"
        Me.btnAdjAccept.UseVisualStyleBackColor = False
        '
        'pnlMgrByItem
        '
        Me.pnlMgrByItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlMgrByItem.BackgroundImage = CType(resources.GetObject("pnlMgrByItem.BackgroundImage"), System.Drawing.Image)
        Me.pnlMgrByItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMgrByItem.Controls.Add(Me.btnMgrCompItem)
        Me.pnlMgrByItem.Controls.Add(Me.btnMgrForcePrice)
        Me.pnlMgrByItem.Controls.Add(Me.btnMgrVoidItem)
        Me.pnlMgrByItem.Controls.Add(Me.lblMgrByItem)
        Me.pnlMgrByItem.Controls.Add(Me.btnMgrPlaceOrder)
        Me.pnlMgrByItem.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMgrByItem.Location = New System.Drawing.Point(8, 8)
        Me.pnlMgrByItem.Name = "pnlMgrByItem"
        Me.pnlMgrByItem.Size = New System.Drawing.Size(136, 280)
        Me.pnlMgrByItem.TabIndex = 9
        '
        'btnMgrCompItem
        '
        Me.btnMgrCompItem.BackColor = System.Drawing.Color.Transparent
        Me.btnMgrCompItem.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMgrCompItem.Location = New System.Drawing.Point(8, 168)
        Me.btnMgrCompItem.Name = "btnMgrCompItem"
        Me.btnMgrCompItem.Size = New System.Drawing.Size(120, 48)
        Me.btnMgrCompItem.TabIndex = 3
        Me.btnMgrCompItem.Text = "Comp Item"
        Me.btnMgrCompItem.UseVisualStyleBackColor = False
        '
        'btnMgrForcePrice
        '
        Me.btnMgrForcePrice.BackColor = System.Drawing.Color.Transparent
        Me.btnMgrForcePrice.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMgrForcePrice.Location = New System.Drawing.Point(8, 112)
        Me.btnMgrForcePrice.Name = "btnMgrForcePrice"
        Me.btnMgrForcePrice.Size = New System.Drawing.Size(120, 48)
        Me.btnMgrForcePrice.TabIndex = 2
        Me.btnMgrForcePrice.Text = "Adjust Price"
        Me.btnMgrForcePrice.UseVisualStyleBackColor = False
        '
        'btnMgrVoidItem
        '
        Me.btnMgrVoidItem.BackColor = System.Drawing.Color.Transparent
        Me.btnMgrVoidItem.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMgrVoidItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnMgrVoidItem.Location = New System.Drawing.Point(8, 56)
        Me.btnMgrVoidItem.Name = "btnMgrVoidItem"
        Me.btnMgrVoidItem.Size = New System.Drawing.Size(120, 48)
        Me.btnMgrVoidItem.TabIndex = 1
        Me.btnMgrVoidItem.Text = "Void Item"
        Me.btnMgrVoidItem.UseVisualStyleBackColor = False
        '
        'lblMgrByItem
        '
        Me.lblMgrByItem.BackColor = System.Drawing.Color.Transparent
        Me.lblMgrByItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMgrByItem.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblMgrByItem.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMgrByItem.Location = New System.Drawing.Point(0, 0)
        Me.lblMgrByItem.Name = "lblMgrByItem"
        Me.lblMgrByItem.Size = New System.Drawing.Size(132, 48)
        Me.lblMgrByItem.TabIndex = 0
        Me.lblMgrByItem.Text = "By Item"
        Me.lblMgrByItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnMgrPlaceOrder
        '
        Me.btnMgrPlaceOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnMgrPlaceOrder.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMgrPlaceOrder.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnMgrPlaceOrder.Location = New System.Drawing.Point(8, 224)
        Me.btnMgrPlaceOrder.Name = "btnMgrPlaceOrder"
        Me.btnMgrPlaceOrder.Size = New System.Drawing.Size(120, 48)
        Me.btnMgrPlaceOrder.TabIndex = 7
        Me.btnMgrPlaceOrder.Text = "Place Order"
        Me.btnMgrPlaceOrder.UseVisualStyleBackColor = False
        '
        'pnlMgrByCheck
        '
        Me.pnlMgrByCheck.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlMgrByCheck.BackgroundImage = CType(resources.GetObject("pnlMgrByCheck.BackgroundImage"), System.Drawing.Image)
        Me.pnlMgrByCheck.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMgrByCheck.Controls.Add(Me.btnMgrTaxExempt)
        Me.pnlMgrByCheck.Controls.Add(Me.btnMgrReopenCheck)
        Me.pnlMgrByCheck.Controls.Add(Me.btnMgrVoidCheck)
        Me.pnlMgrByCheck.Controls.Add(Me.lblMgrByCheck)
        Me.pnlMgrByCheck.Controls.Add(Me.btnMgrTransfer)
        Me.pnlMgrByCheck.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMgrByCheck.Location = New System.Drawing.Point(152, 8)
        Me.pnlMgrByCheck.Name = "pnlMgrByCheck"
        Me.pnlMgrByCheck.Size = New System.Drawing.Size(136, 280)
        Me.pnlMgrByCheck.TabIndex = 10
        '
        'btnMgrTaxExempt
        '
        Me.btnMgrTaxExempt.BackColor = System.Drawing.Color.Transparent
        Me.btnMgrTaxExempt.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMgrTaxExempt.ForeColor = System.Drawing.Color.Black
        Me.btnMgrTaxExempt.Location = New System.Drawing.Point(8, 168)
        Me.btnMgrTaxExempt.Name = "btnMgrTaxExempt"
        Me.btnMgrTaxExempt.Size = New System.Drawing.Size(120, 48)
        Me.btnMgrTaxExempt.TabIndex = 8
        Me.btnMgrTaxExempt.Text = "Tax Exempt"
        Me.btnMgrTaxExempt.UseVisualStyleBackColor = False
        '
        'btnMgrReopenCheck
        '
        Me.btnMgrReopenCheck.BackColor = System.Drawing.Color.Transparent
        Me.btnMgrReopenCheck.Location = New System.Drawing.Point(8, 56)
        Me.btnMgrReopenCheck.Name = "btnMgrReopenCheck"
        Me.btnMgrReopenCheck.Size = New System.Drawing.Size(120, 48)
        Me.btnMgrReopenCheck.TabIndex = 5
        Me.btnMgrReopenCheck.Text = "Reopen Check"
        Me.btnMgrReopenCheck.UseVisualStyleBackColor = False
        '
        'btnMgrVoidCheck
        '
        Me.btnMgrVoidCheck.BackColor = System.Drawing.Color.Transparent
        Me.btnMgrVoidCheck.Location = New System.Drawing.Point(8, 224)
        Me.btnMgrVoidCheck.Name = "btnMgrVoidCheck"
        Me.btnMgrVoidCheck.Size = New System.Drawing.Size(120, 48)
        Me.btnMgrVoidCheck.TabIndex = 4
        Me.btnMgrVoidCheck.Text = "Void Check"
        Me.btnMgrVoidCheck.UseVisualStyleBackColor = False
        '
        'lblMgrByCheck
        '
        Me.lblMgrByCheck.BackColor = System.Drawing.Color.Transparent
        Me.lblMgrByCheck.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMgrByCheck.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblMgrByCheck.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMgrByCheck.Location = New System.Drawing.Point(0, 0)
        Me.lblMgrByCheck.Name = "lblMgrByCheck"
        Me.lblMgrByCheck.Size = New System.Drawing.Size(132, 48)
        Me.lblMgrByCheck.TabIndex = 1
        Me.lblMgrByCheck.Text = "By Check"
        Me.lblMgrByCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnMgrTransfer
        '
        Me.btnMgrTransfer.BackColor = System.Drawing.Color.Transparent
        Me.btnMgrTransfer.Location = New System.Drawing.Point(8, 112)
        Me.btnMgrTransfer.Name = "btnMgrTransfer"
        Me.btnMgrTransfer.Size = New System.Drawing.Size(120, 48)
        Me.btnMgrTransfer.TabIndex = 6
        Me.btnMgrTransfer.Text = "Transfer Check"
        Me.btnMgrTransfer.UseVisualStyleBackColor = False
        '
        'btnMgrReprintOrder
        '
        Me.btnMgrReprintOrder.BackColor = System.Drawing.Color.Transparent
        Me.btnMgrReprintOrder.Location = New System.Drawing.Point(8, 168)
        Me.btnMgrReprintOrder.Name = "btnMgrReprintOrder"
        Me.btnMgrReprintOrder.Size = New System.Drawing.Size(112, 48)
        Me.btnMgrReprintOrder.TabIndex = 3
        Me.btnMgrReprintOrder.Text = "Reprint Order"
        Me.btnMgrReprintOrder.UseVisualStyleBackColor = False
        '
        'btnMgrReprintCheck
        '
        Me.btnMgrReprintCheck.BackColor = System.Drawing.Color.Transparent
        Me.btnMgrReprintCheck.Location = New System.Drawing.Point(8, 112)
        Me.btnMgrReprintCheck.Name = "btnMgrReprintCheck"
        Me.btnMgrReprintCheck.Size = New System.Drawing.Size(112, 48)
        Me.btnMgrReprintCheck.TabIndex = 2
        Me.btnMgrReprintCheck.Text = "Reprint Check"
        Me.btnMgrReprintCheck.UseVisualStyleBackColor = False
        '
        'pnlMgrByPayments
        '
        Me.pnlMgrByPayments.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlMgrByPayments.BackgroundImage = CType(resources.GetObject("pnlMgrByPayments.BackgroundImage"), System.Drawing.Image)
        Me.pnlMgrByPayments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMgrByPayments.Controls.Add(Me.btnMgrCashBack)
        Me.pnlMgrByPayments.Controls.Add(Me.btnMgrAssignGratuity)
        Me.pnlMgrByPayments.Controls.Add(Me.btnMgrAssignComps)
        Me.pnlMgrByPayments.Controls.Add(Me.btnAdjustPay)
        Me.pnlMgrByPayments.Controls.Add(Me.lblMgrByPayments)
        Me.pnlMgrByPayments.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMgrByPayments.Location = New System.Drawing.Point(296, 8)
        Me.pnlMgrByPayments.Name = "pnlMgrByPayments"
        Me.pnlMgrByPayments.Size = New System.Drawing.Size(128, 280)
        Me.pnlMgrByPayments.TabIndex = 11
        '
        'btnMgrCashBack
        '
        Me.btnMgrCashBack.BackColor = System.Drawing.Color.Transparent
        Me.btnMgrCashBack.Location = New System.Drawing.Point(8, 224)
        Me.btnMgrCashBack.Name = "btnMgrCashBack"
        Me.btnMgrCashBack.Size = New System.Drawing.Size(112, 48)
        Me.btnMgrCashBack.TabIndex = 6
        Me.btnMgrCashBack.Text = "Cash Back"
        Me.btnMgrCashBack.UseVisualStyleBackColor = False
        '
        'btnMgrAssignGratuity
        '
        Me.btnMgrAssignGratuity.BackColor = System.Drawing.Color.Transparent
        Me.btnMgrAssignGratuity.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMgrAssignGratuity.Location = New System.Drawing.Point(8, 168)
        Me.btnMgrAssignGratuity.Name = "btnMgrAssignGratuity"
        Me.btnMgrAssignGratuity.Size = New System.Drawing.Size(112, 48)
        Me.btnMgrAssignGratuity.TabIndex = 5
        Me.btnMgrAssignGratuity.Text = "Assign Gratuity"
        Me.btnMgrAssignGratuity.UseVisualStyleBackColor = False
        '
        'btnMgrAssignComps
        '
        Me.btnMgrAssignComps.BackColor = System.Drawing.Color.Transparent
        Me.btnMgrAssignComps.Location = New System.Drawing.Point(8, 112)
        Me.btnMgrAssignComps.Name = "btnMgrAssignComps"
        Me.btnMgrAssignComps.Size = New System.Drawing.Size(112, 48)
        Me.btnMgrAssignComps.TabIndex = 4
        Me.btnMgrAssignComps.Text = "Assign Comps"
        Me.btnMgrAssignComps.UseVisualStyleBackColor = False
        '
        'btnAdjustPay
        '
        Me.btnAdjustPay.BackColor = System.Drawing.Color.Transparent
        Me.btnAdjustPay.Location = New System.Drawing.Point(8, 56)
        Me.btnAdjustPay.Name = "btnAdjustPay"
        Me.btnAdjustPay.Size = New System.Drawing.Size(112, 48)
        Me.btnAdjustPay.TabIndex = 3
        Me.btnAdjustPay.Text = "Delete Credit"
        Me.btnAdjustPay.UseVisualStyleBackColor = False
        '
        'lblMgrByPayments
        '
        Me.lblMgrByPayments.BackColor = System.Drawing.Color.Transparent
        Me.lblMgrByPayments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMgrByPayments.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblMgrByPayments.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMgrByPayments.Location = New System.Drawing.Point(0, 0)
        Me.lblMgrByPayments.Name = "lblMgrByPayments"
        Me.lblMgrByPayments.Size = New System.Drawing.Size(124, 48)
        Me.lblMgrByPayments.TabIndex = 2
        Me.lblMgrByPayments.Text = "By Payments"
        Me.lblMgrByPayments.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnMgrReprintCredit
        '
        Me.btnMgrReprintCredit.BackColor = System.Drawing.Color.Transparent
        Me.btnMgrReprintCredit.Location = New System.Drawing.Point(8, 56)
        Me.btnMgrReprintCredit.Name = "btnMgrReprintCredit"
        Me.btnMgrReprintCredit.Size = New System.Drawing.Size(112, 48)
        Me.btnMgrReprintCredit.TabIndex = 6
        Me.btnMgrReprintCredit.Text = "Reprint Credit"
        Me.btnMgrReprintCredit.UseVisualStyleBackColor = False
        '
        'pnlMgrCalculationArea
        '
        Me.pnlMgrCalculationArea.BackColor = System.Drawing.Color.CornflowerBlue
        Me.pnlMgrCalculationArea.Location = New System.Drawing.Point(464, 400)
        Me.pnlMgrCalculationArea.Name = "pnlMgrCalculationArea"
        Me.pnlMgrCalculationArea.Size = New System.Drawing.Size(496, 320)
        Me.pnlMgrCalculationArea.TabIndex = 12
        Me.pnlMgrCalculationArea.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.pnlMgrByItem)
        Me.Panel1.Controls.Add(Me.pnlMgrByCheck)
        Me.Panel1.Controls.Add(Me.pnlMgrByPayments)
        Me.Panel1.Location = New System.Drawing.Point(432, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(572, 300)
        Me.Panel1.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.lblMgrPrinting)
        Me.Panel2.Controls.Add(Me.btnMgrReprintCredit)
        Me.Panel2.Controls.Add(Me.btnMgrReprintCheck)
        Me.Panel2.Controls.Add(Me.btnMgrReprintOrder)
        Me.Panel2.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(432, 8)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(128, 280)
        Me.Panel2.TabIndex = 12
        '
        'lblMgrPrinting
        '
        Me.lblMgrPrinting.BackColor = System.Drawing.Color.Transparent
        Me.lblMgrPrinting.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMgrPrinting.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblMgrPrinting.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMgrPrinting.Location = New System.Drawing.Point(0, 0)
        Me.lblMgrPrinting.Name = "lblMgrPrinting"
        Me.lblMgrPrinting.Size = New System.Drawing.Size(124, 48)
        Me.lblMgrPrinting.TabIndex = 2
        Me.lblMgrPrinting.Text = "Printing"
        Me.lblMgrPrinting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlCheckTotalFor_UC
        '
        Me.pnlCheckTotalFor_UC.BackColor = System.Drawing.Color.White
        Me.pnlCheckTotalFor_UC.Location = New System.Drawing.Point(64, 72)
        Me.pnlCheckTotalFor_UC.Name = "pnlCheckTotalFor_UC"
        Me.pnlCheckTotalFor_UC.Size = New System.Drawing.Size(312, 658)
        Me.pnlCheckTotalFor_UC.TabIndex = 14
        '
        'Manager_OrderAdj_UC
        '
        Me.BackColor = System.Drawing.Color.Black
        Me.Controls.Add(Me.pnlCheckTotalFor_UC)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlMgrCalculationArea)
        Me.Controls.Add(Me.pnlMgrCheckInfo)
        Me.Name = "Manager_OrderAdj_UC"
        Me.Size = New System.Drawing.Size(1024, 768)
        Me.pnlMgrCheckInfo.ResumeLayout(False)
        Me.pnlMgrByItem.ResumeLayout(False)
        Me.pnlMgrByCheck.ResumeLayout(False)
        Me.pnlMgrByPayments.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther(ByVal expNum As Int64)

        '     MsgBox(System.Drawing.SystemColors.Control.R.ToString)
        '    MsgBox(System.Drawing.SystemColors.Control.G.ToString)
        '   MsgBox(System.Drawing.SystemColors.Control.B.ToString)

        '   PopulateOpenOrderData(expNum)    '(currentTable.ExperienceNumber)

        '    CreateDataViews()

        '      GenerateOrderTables.PopulatePaymentsAndCredits(currentTable.ExperienceNumber)
        dvForcePrice = New DataView
        dvUnAppliedPaymentsAndCredits = New DataView
        dvPaymentsAndCredits = New DataView
        '      GeneratePaymentsAndCreditsDataviews()

        '   need *** PopulateStatusData
        DisplayOrder()
        Me.checkTotalsMgmtAdj.AttachTotalsToTotalView(currentTable.CheckNumber)

        If isForReopen = False Then
            Me.btnMgrReopenCheck.Text = "Close Check"
            ChangeButtonColor(Me.btnMgrReopenCheck)
        End If
        Me.pnlMgrByItem.BringToFront()
        Me.pnlMgrByCheck.BringToFront()
        Me.pnlMgrByPayments.BringToFront()

        currentTable.SIN = DetermineSelectedItemNumber() + 1


    End Sub

    Private Sub FillOpenOrderData222(ByVal experienceNumber As Int64)


        '      If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
        dsOrder.Tables("OpenOrders").Clear()
        '    End If

        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        '     sql.SqlSelectCommandOpenOrdersSP.Parameters("@CompanyID").Value = CompanyID
        sql.SqlSelectCommandOpenOrdersSP.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandOpenOrdersSP.Parameters("@ExperienceNumber").Value = experienceNumber
        sql.SqlDataAdapterOpenOrdersSP.Fill(dsOrder.Tables("OpenOrders"))
        sql.cn.Close()

        SetUpPrimaryKeys()

    End Sub

    Private Sub DisplayOrder()

        '     Me.checkTotalsMgmtAdj = New CheckTotal_UC
        '    Me.checkTotalsMgmtAdj.Location = New Point(88, 16 + Me.pnlMgrCheckInfo.Height)
        '   Me.Controls.Add(checkTotalsMgmtAdj)

        Me.checkTotalsMgmtAdj = New CheckTotal_UC
        Me.checkTotalsMgmtAdj.Location = New Point(1, 1)
        Me.pnlCheckTotalFor_UC.Controls.Add(checkTotalsMgmtAdj)

        UpdateCheckNumberButton()
        TestButtonColors()

    End Sub

    Private Sub btnMgrCheckNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrCheckNumber.Click

        If currentTable.CheckNumber = currentTable.NumberOfChecks Then
            currentTable.CheckNumber = 1
        Else
            currentTable.CheckNumber += 1
        End If

        '     CreateDataViews()
        Me.checkTotalsMgmtAdj.PopulateCloseGrid(dvOrder)    ' dvClosingCheck)

        Me.checkTotalsMgmtAdj.CalculatePriceAndTax(currentTable.CheckNumber)
        checkTotalsMgmtAdj.AttachTotalsToTotalView(currentTable.CheckNumber)
        GeneratePaymentsAndCreditsDataviews()

        '      Dim rb As Decimal
        '     rb = checkTotalsMgmtAdj.AttachTotalsToTotalView

        UpdateCheckNumberButton()
        '   need to add more to update screen
        '   redo datasource

    End Sub

    Private Sub GeneratePaymentsAndCreditsDataviews()

        With dvUnAppliedPaymentsAndCredits
            .Table = dsOrder.Tables("PaymentsAndCredits")
            .RowFilter = "Applied = False AND CheckNumber = " & currentTable.CheckNumber
            .Sort = "PaymentFlag"
        End With

        With dvPaymentsAndCredits
            .Table = dsOrder.Tables("PaymentsAndCredits")
            .RowFilter = "Applied = True AND CheckNumber = " & currentTable.CheckNumber
        End With


    End Sub

    Private Function DetermineSelectedItemNumber222()
        Dim currentSIN As Integer

        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
            currentSIN = dsOrder.Tables("OpenOrders").Compute("Max(sin)", "")
        End If

        Return currentSIN

    End Function



    Private Sub UpdateCheckNumberButton()
        Me.btnMgrCheckNumber.Text = "Check   " & currentTable.CheckNumber & " of " & currentTable.NumberOfChecks 'currentTable._checkCollection.Count

    End Sub

    Private Sub TestButtonColors()

        If employeeAuthorization.OperationLevel >= systemAuthorization.VoidItem Then
            ChangeButtonColor(Me.btnMgrVoidItem)
        End If
        If employeeAuthorization.OperationLevel >= systemAuthorization.ForcePrice Then
            ChangeButtonColor(Me.btnMgrForcePrice)
        End If
        If employeeAuthorization.OperationLevel >= systemAuthorization.CompItem Then
            ChangeButtonColor(Me.btnMgrCompItem)
        End If
        If employeeAuthorization.OperationLevel >= systemAuthorization.TaxExempt Then
            ChangeButtonColor(Me.btnMgrTaxExempt)
        End If
        If employeeAuthorization.OperationLevel >= systemAuthorization.ReprintCheck Then
            ChangeButtonColor(Me.btnMgrReprintCheck)
        End If
        If employeeAuthorization.OperationLevel >= systemAuthorization.ReprintOrder Then
            ChangeButtonColor(Me.btnMgrReprintOrder)
        End If
        If employeeAuthorization.OperationLevel >= systemAuthorization.ReopenCheck Then
            ChangeButtonColor(Me.btnMgrReopenCheck)
        End If
        If employeeAuthorization.OperationLevel >= systemAuthorization.VoidCheck Then
            ChangeButtonColor(Me.btnMgrVoidCheck)
        End If
        If employeeAuthorization.OperationLevel >= systemAuthorization.AdjustPayment Then
            ChangeButtonColor(Me.btnAdjustPay)
            ChangeButtonColor(Me.btnMgrCashBack)
        End If
        If employeeAuthorization.OperationLevel >= systemAuthorization.AssignComps Then
            ChangeButtonColor(Me.btnMgrAssignComps)
        End If
        If employeeAuthorization.OperationLevel >= systemAuthorization.AssignGratuity Then
            ChangeButtonColor(Me.btnMgrAssignGratuity)
        End If
        If employeeAuthorization.OperationLevel >= systemAuthorization.TransferCheck Or employeeAuthorization.OperationLevel >= systemAuthorization.TransferItem Then
            ChangeButtonColor(Me.btnMgrTransfer)
        End If
        If employeeAuthorization.OperationLevel >= systemAuthorization.ReprintCredit Then
            ChangeButtonColor(Me.btnMgrReprintCredit)
        End If


    End Sub

    Private Sub ChangeButtonColor(ByRef btn As Button)
        btn.BackColor = c16 'c10
        btn.ForeColor = c3
        btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))


    End Sub


    Private Sub btnMgrVoidCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrVoidCheck.Click

        Me.pnlMgrCalculationArea.BringToFront()

        If employeeAuthorization.OperationLevel < systemAuthorization.VoidCheck Then
            MsgBox(employeeAuthorization.FullName & " does not have Authorization")
            Exit Sub
        End If
        If isForReopen = True Then
            MsgBox("Check must be open for this function.")
            Exit Sub
        End If
        If madeChanges = True Then
            MsgBox("You Must ACCEPT changes before this action.")
            Exit Sub
        End If
        If MsgBox("Are you sure you want to VOID this check?", MsgBoxStyle.OKCancel) = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        Dim vRow As DataRowView
      
        If currentTable.IsTabNotTable = False Then
            If isForReopen = True Then
                For Each vRow In dvClosedTables 'dsOrder.Tables("ClosedTables").Rows
                    If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        vRow("TabName") = "Voided " & currentTable.TableNumber.ToString
                        vRow("LastStatusTime") = Now
                        vRow("ClosedSubTotal") = 0
                        If vRow("AvailForSeating") Is DBNull.Value Then
                            vRow("AvailForSeating") = Now
                        End If
                        vRow("LastStatus") = 9
                        Exit For
                    End If
                Next
            Else
                For Each vRow In dvAvailTables  'dsOrder.Tables("AvailTables").Rows
                    If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        vRow("TabName") = "Voided " & currentTable.TableNumber.ToString
                        vRow("LastStatusTime") = Now
                        vRow("ClosedSubTotal") = 0
                        If vRow("AvailForSeating") Is DBNull.Value Then
                            vRow("AvailForSeating") = Now
                        End If
                        vRow("LastStatus") = 9

                        Exit For
                    End If
                Next
            End If

        Else
            If isForReopen = True Then

                If currentTable.TicketNumber > 0 Then
                    For Each vRow In dvQuickTickets
                        If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                            vRow("TabName") = "Voided " & currentTable.TabName.ToString
                            vRow("LastStatusTime") = Now
                            vRow("ClosedSubTotal") = 0
                            If vRow("AvailForSeating") Is DBNull.Value Then
                                vRow("AvailForSeating") = Now
                            End If

                            vRow("LastStatus") = 9
                            Exit For
                        End If
                    Next
                Else
                    For Each vRow In dvClosedTabs   'dsOrder.Tables("ClosedTabs").Rows
                        If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                            vRow("TabName") = "Voided " & currentTable.TabName.ToString
                            vRow("LastStatusTime") = Now
                            vRow("ClosedSubTotal") = 0
                            If vRow("AvailForSeating") Is DBNull.Value Then
                                vRow("AvailForSeating") = Now
                            End If

                            vRow("LastStatus") = 9
                            Exit For
                        End If
                    Next
                End If

            Else
                If currentTable.TicketNumber > 0 Then
                    For Each vRow In dvQuickTickets
                        If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                            vRow("TabName") = "Voided " & currentTable.TabName.ToString
                            vRow("LastStatusTime") = Now
                            vRow("ClosedSubTotal") = 0
                            If vRow("AvailForSeating") Is DBNull.Value Then
                                vRow("AvailForSeating") = Now
                            End If
                            vRow("LastStatus") = 9
                            Exit For
                        End If
                    Next
                Else
                    For Each vRow In dvAvailTabs    'dsOrder.Tables("AvailTabs").Rows
                        If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                            vRow("TabName") = "Voided " & currentTable.TabName.ToString
                            vRow("LastStatusTime") = Now
                            vRow("ClosedSubTotal") = 0
                            If vRow("AvailForSeating") Is DBNull.Value Then
                                vRow("AvailForSeating") = Now
                            End If
                            vRow("LastStatus") = 9
                            Exit For
                        End If
                    Next
                End If
            End If

            If Not vRow Is Nothing Then
                If vRow("TabName").ToString.Length > 20 Then
                    vRow("TabName") = vRow("TabName").Substring(0, 20)
                End If
            End If

        End If

        'sss       GenerateOrderTables.SaveAvailTabsAndTables()
        '   *** we may need to save Closed TabsAndTables, but we only void open checks?
        '   *** (9) tells us void, then 1 tells us avail, should do all at once
        '    AddStatusChangeData(currentTable.ExperienceNumber, 9, 0, 0, 0)
        '      SaveESCStatusChangeData(9, 0, 0, 0)

        '    AddStatusChangeData(currentTable.ExperienceNumber, 1, 0, 0, 0)
        '     SaveESCStatusChangeData(1, 0, 0, 0)


        Dim sinArray(dsOrder.Tables("OpenOrders").Rows.Count) As Integer
        Dim count As Integer
        Dim ffInfo As ForceFreeInfo
        Dim oRow As DataRow
        Dim oldRow As DataRow
        Dim ni As SelectedItemDetail
        Dim iPromo As ItemPromoInfo
        iPromo.PromoName = "    ** Void Check **"
        iPromo.PromoCode = 9
        iPromo.PromoReason = 9
        iPromo.empID = actingManager.EmployeeID

        Dim hasRows As Boolean
        Dim valueSIN As Integer

        Try
            '        sql.cn.Open()
            '            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            For Each oldRow In dsOrder.Tables("OpenOrders").Rows
                If Not oldRow.RowState = DataRowState.Deleted And Not oldRow.RowState = DataRowState.Detached Then
                    '     If oldRow("ItemID") > 0 Then
                    If Not oldRow("ForceFreeID") = 8 And Not oldRow("ForceFreeID") = 9 And Not oldRow("ForceFreeID") = -8 And Not oldRow("ForceFreeID") = -9 Then     'Not oldRow("ForceFreeID") = 7 And
                        'not ALREADY comp'd / void'd or transfered
                        '    If Not oRow("ItemStatus") = 9 Or Not oRow("ItemStatus") = 8 Then
                        'above was old
                        If oldRow("ItemStatus") = 0 Or oldRow("ItemStatus") = 1 Then
                            '   this item is just deleted 
                            sinArray(count) = oldRow("sin")
                            count += 1
                        Else
                            iPromo.empID = actingManager.EmployeeID
                            iPromo.ItemID = oldRow("ItemID")
                            iPromo.Quantity = (oldRow("Quantity") * -1)
                            iPromo.InvMultiplier = oldRow("OpenDecimal1")
                            iPromo.ItemPrice = oldRow("Price")
                            iPromo.Price = oldRow("Price")
                            iPromo.TaxPrice = oldRow("TaxPrice")
                            iPromo.SinTax = oldRow("SinTax")
                            '            iPromo.FunctionID = oldRow("FunctionID")
                            '             iPromo.FunctionGroup = oldRow("FunctionGroupID")
                            iPromo.FunctionFlag = "P" ' = oldRow("FunctionFlag")
                            '           iPromo.CategoryID = oldRow("CategoryID")
                            '           iPromo.FoodID = oldRow("FoodID")
                            '          iPromo.DrinkCategoryID = oldRow("DrinkCategoryID")
                            '          iPromo.DrinkID = oldRow("DrinkID")
                            '          iPromo.RoutingID = oldRow("RoutingID")
                            '          iPromo.PrintPriorityID = oldRow("PrintPriorityID")

                            iPromo.ItemStatus = 9
                            oldRow("ForceFreeID") = -9
                            oldRow("ItemStatus") = 9
                            iPromo.openOrderID = oldRow("OpenOrderID")
                            iPromo.taxID = oldRow("TaxID")
                            iPromo.si2 = oldRow("si2")

                            If oldRow("sii") < oldRow("sin") Then   ' <> valueSIN Then
                                'we populate openOrder we increase sin by 1, now they equal
                                If currentTable.ReferenceSIN = 0 Then
                                    currentTable.ReferenceSIN = currentTable.SIN + 1
                                End If
                                'this will send the last one
                                '      valueSIN = oldRow("sin")
                                '  iPromo.sii = currentTable.SIN + 1   'thisis because before
                            Else
                                currentTable.ReferenceSIN = currentTable.SIN + 1
                            End If
                            iPromo.sii = currentTable.ReferenceSIN

                            CompThisItem(iPromo)

                        End If
                    End If
                    '   End If
                End If
            Next

            If count > 0 Then
                Dim i As Integer
                For i = 0 To count - 1
                    If Not typeProgram = "Online_Demo" Then
                        oRow = (dsOrder.Tables("OpenOrders").Rows.Find(sinArray(i)))
                    Else
                        For Each oRow In dsOrder.Tables("OpenOrders").Rows
                            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                                If oRow("sin") = sinArray(i) Then
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                    GenerateOrderTables.DeleteOpenOrdersRowTerminal(oRow)
                Next
                count = 0
            End If
            '      sql.cn.Close()

            For Each ni In newItemCollection
                GenerateOrderTables.PopulateDataRowForOpenOrder(ni)
            Next
            newItemCollection.Clear()
        Catch ex As Exception
            '     CloseConnection()
            newItemCollection.Clear()
            MsgBox(ex.Message)
        End Try

        '     Me.checkTotalsMgmtAdj.CalculatePriceAndTax(currentTable.CheckNumber)
        '    Me.checkTotalsMgmtAdj.AttachTotalsToTotalView()

        If currentTable.IsTabNotTable = False Then
            RaiseEvent VoidedCheckTableStatusChange(currentTable.TableNumber)
        End If

        RaiseEvent ReinitializeMain(True, True)

    End Sub

    Private Sub btnMgrVoidItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrVoidItem.Click

        Me.pnlMgrCalculationArea.BringToFront()

        If employeeAuthorization.OperationLevel < systemAuthorization.VoidItem Then
            MsgBox(employeeAuthorization.FullName & " does not have Authorization")
            Exit Sub
        End If
        If isForReopen = True Then
            MsgBox("Check must be open for this function.")
            Exit Sub
        End If

        Dim rowNum As Integer = Me.checkTotalsMgmtAdj.grdCloseCheck.CurrentCell.RowNumber

        Dim valueSIN As Integer
        Dim valueSII As Integer
        Try
            valueSIN = CType(Me.checkTotalsMgmtAdj.grdCloseCheck.Item(rowNum, 1), Integer)
            valueSII = CType(Me.checkTotalsMgmtAdj.grdCloseCheck.Item(rowNum, 2), Integer)
        Catch ex As Exception
            Exit Sub
        End Try

        Dim sinArray(dsOrder.Tables("OpenOrders").Rows.Count) As Integer
        Dim count As Integer
        Dim ffInfo As ForceFreeInfo
        Dim oRow As DataRow
        Dim oldRow As DataRow
        Dim ni As SelectedItemDetail
        Dim iPromo As ItemPromoInfo
        Dim hasRows As Boolean

        iPromo.PromoName = "    ** Void"
        iPromo.PromoCode = 9
        iPromo.PromoReason = 9
        iPromo.empID = actingManager.EmployeeID
        madeChanges = True

        Try
            If valueSII = valueSIN Then
                '   *** main food
                '      currentTable.ReferenceSIN = currentTable.SIN

                '            sql.cn.Open()
                '            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                For Each oldRow In dsOrder.Tables("OpenOrders").Rows
                    If Not oldRow.RowState = DataRowState.Deleted And Not oldRow.RowState = DataRowState.Detached Then
                        If Not oldRow("ItemID") = 0 Then
                            If oldRow("sii") = valueSIN Then
                                If Not oldRow("ForceFreeID") = 8 And Not oldRow("ForceFreeID") = 9 And Not oldRow("ForceFreeID") = -8 And Not oldRow("ForceFreeID") = -9 Then
                                    'not ALREADY comp'd / void'd or transfered
                                    '    If Not oRow("ItemStatus") = 9 Or Not oRow("ItemStatus") = 8 Then
                                    'above was old
                                    If oldRow("ItemStatus") = 0 Or oldRow("ItemStatus") = 1 Then
                                        '   this item is just deleted 
                                        sinArray(count) = oldRow("sin")
                                        count += 1
                                    Else
                                        iPromo.empID = actingManager.EmployeeID
                                        iPromo.ItemID = oldRow("ItemID")
                                        iPromo.Quantity = (oldRow("Quantity") * -1)
                                        iPromo.InvMultiplier = oldRow("OpenDecimal1")
                                        iPromo.ItemPrice = oldRow("Price")
                                        iPromo.Price = oldRow("Price")
                                        iPromo.TaxPrice = oldRow("TaxPrice")
                                        iPromo.SinTax = oldRow("SinTax")

                                        '                iPromo.FunctionID = oldRow("FunctionID")
                                        '                iPromo.FunctionGroup = oldRow("FunctionGroupID")
                                        iPromo.FunctionFlag = "P" '  = oldRow("FunctionFlag")
                                        '               iPromo.CategoryID = oldRow("CategoryID")
                                        '               iPromo.FoodID = oldRow("FoodID")
                                        ''               iPromo.DrinkCategoryID = oldRow("DrinkCategoryID")
                                        '               iPromo.DrinkID = oldRow("DrinkID")
                                        '               iPromo.RoutingID = oldRow("RoutingID")
                                        '              iPromo.PrintPriorityID = oldRow("PrintPriorityID")

                                        iPromo.ItemStatus = 9
                                        oldRow("ForceFreeID") = -9
                                        oldRow("ItemStatus") = 9
                                        '        If oldRow("sin") = valueSIN Then
                                        'this will send only the leading info
                                        iPromo.openOrderID = oldRow("OpenOrderID")
                                        iPromo.taxID = oldRow("TaxID")
                                        iPromo.sii = oldRow("sii")
                                        iPromo.si2 = oldRow("si2")
                                        If Not oldRow("OrderNumber") Is DBNull.Value Then
                                            iPromo.OrderNumber = oldRow("OrderNumber")
                                        Else
                                            iPromo.OrderNumber = Nothing
                                        End If
                                        iPromo.CheckNumber = oldRow("CheckNumber")
                                        iPromo.CustomerNumber = oldRow("CustomerNumber")
                                        iPromo.CourseNumber = oldRow("CourseNumber")
                                        '     hasRows = True
                                        '   End If
                                        '444       If hasRows = True Then
                                        CompThisItem(iPromo)
                                        '444    End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next

                If count > 0 Then
                    Dim i As Integer
                    For i = 0 To count - 1
                        If Not typeProgram = "Online_Demo" Then
                            oRow = (dsOrder.Tables("OpenOrders").Rows.Find(sinArray(i)))
                        Else
                            For Each oRow In dsOrder.Tables("OpenOrders").Rows
                                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                                    If oRow("sin") = sinArray(i) Then
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                        GenerateOrderTables.DeleteOpenOrdersRowTerminal(oRow)
                    Next
                    count = 0
                End If


            ElseIf valueSII < valueSIN Then
                '   *** food modifier
                If Not typeProgram = "Online_Demo" Then
                    oRow = (dsOrder.Tables("OpenOrders").Rows.Find(valueSIN))
                Else
                    For Each oRow In dsOrder.Tables("OpenOrders").Rows
                        If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                            If oRow("sin") = valueSIN Then
                                Exit For
                            End If
                        End If
                    Next
                End If

                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If Not oRow("ItemID") = 0 And Not oRow("Price") < 0 Then
                        If Not oRow("ForceFreeID") = 7 And Not oRow("ForceFreeID") = 8 And Not oRow("ForceFreeID") = 9 And Not oRow("ForceFreeID") = -7 And Not oRow("ForceFreeID") = -8 And Not oRow("ForceFreeID") = -9 Then
                            'not ALREADY comp'd or transfered
                            If oRow("ItemStatus") = 0 Or oRow("ItemStatus") = 1 Then
                                GenerateOrderTables.DeleteOpenOrdersRowTerminal(oRow)
                            Else
                                iPromo.empID = actingManager.EmployeeID
                                iPromo.ItemID = oRow("ItemID")
                                iPromo.Quantity = (oRow("Quantity") * -1)
                                iPromo.InvMultiplier = oRow("OpenDecimal1")
                                iPromo.ItemPrice = oRow("Price")
                                iPromo.Price = oRow("Price")
                                iPromo.TaxPrice = oRow("TaxPrice")
                                iPromo.SinTax = oRow("SinTax")

                                '           iPromo.FunctionID = oRow("FunctionID")
                                '           iPromo.FunctionGroup = oRow("FunctionGroupID")
                                iPromo.FunctionFlag = "P" ' = oRow("FunctionFlag")
                                '           iPromo.CategoryID = oRow("CategoryID")
                                '           iPromo.FoodID = oRow("FoodID")
                                '           iPromo.DrinkCategoryID = oRow("DrinkCategoryID")
                                '          iPromo.DrinkID = oRow("DrinkID")
                                ''          iPromo.RoutingID = oRow("RoutingID")
                                '         iPromo.PrintPriorityID = oRow("PrintPriorityID")

                                iPromo.ItemStatus = 9
                                iPromo.openOrderID = oRow("OpenOrderID")
                                iPromo.taxID = oRow("TaxID")
                                iPromo.sii = oRow("sii")
                                iPromo.si2 = oRow("si2")
                                If Not oRow("OrderNumber") Is DBNull.Value Then
                                    iPromo.OrderNumber = oRow("OrderNumber")
                                Else
                                    iPromo.OrderNumber = Nothing
                                End If
                                iPromo.CheckNumber = oRow("CheckNumber")
                                iPromo.CustomerNumber = oRow("CustomerNumber")
                                iPromo.CourseNumber = oRow("CourseNumber")

                                oRow("ForceFreeID") = -9
                                oRow("ItemStatus") = 9

                                CompThisItem(iPromo)
                            End If
                        End If
                    End If
                End If
            End If

            '       sql.cn.Close()

            For Each ni In newItemCollection
                GenerateOrderTables.PopulateDataRowForOpenOrder(ni)
            Next
            newItemCollection.Clear()
        Catch ex As Exception
            '       CloseConnection()
            newItemCollection.Clear()
            MsgBox(ex.Message)
        End Try

        Me.checkTotalsMgmtAdj.CalculatePriceAndTax(currentTable.CheckNumber)
        Me.checkTotalsMgmtAdj.AttachTotalsToTotalView(currentTable.CheckNumber)

    End Sub

    Private Sub AdjustVoidedDataRow222(ByVal oRow As DataRow)

        '     Dim nRow As DataRow = dsOrder.Tables("OpenOrders").NewRow
        Dim currentItem As SelectedItemDetail = New SelectedItemDetail
        '   this is the credit entry for voided item

        If oRow("Price") > 0 Then
            With currentItem
                '          .Table = oRow("TableNumber")
                .Check = oRow("CheckNumber")
                .Customer = oRow("CustomerNumber")
                .Course = oRow("CourseNumber")
                .SIN = currentTable.SIN
                .SII = oRow("sin")     'currentTable.ReferenceSIN
                .ID = oRow("ItemID")    'voidingFlag will adj in AddItem Sub
                .Voiding = True
                .Name = oRow("ItemName")    '"**  VOID  " & oRow("ItemName")
                .TerminalName = "**  VOID  " & oRow("ItemName")
                .ChitName = "**  VOID  " & oRow("ItemName")
                .Price = -1 * oRow("Price")
                '   tax price is calculate w/ a neg price
                .TaxID = oRow("TaxID")
                .FunctionID = oRow("FunctionID")
                If oRow("FunctionID") = 1 Or oRow("FunctionID") = 2 Or oRow("FunctionID") = 3 Then
                    .Category = oRow("CategoryID")
                ElseIf oRow("FunctionID") >= 4 And oRow("FunctionID") <= 7 Then
                    .Category = oRow("DrinkCategoryID")
                    '   i don't think we need if fun = 0
                End If
                .RoutingID = oRow("RoutingID")
                .PrintPriorityID = oRow("PrintPriorityID")
            End With

            voidedItems.Add(currentItem)
            '      nRow = PopulateDataRowForOpenOrder(currentItem)
            '     dsOrder.Tables("OpenOrders").Rows.Add(nRow)

            currentTable.SIN += 1        '   not sure about ReferenceSIN

        End If

        '   now for the debit entry
        '************* below no good , need to use force free table
        oRow("ForceFreeID") = 0       'mgr override (-1 is changed amount, 0 is voided)
        oRow("ForceFreeAuth") = actingManager.EmployeeID
        oRow("ForceFreeCode") = 0   'oRow("Price")
        oRow("ItemStatus") = 9
        '        oRow("ItemName") = "  ** Void" & oRow("ItemName")
        '       oRow("Price") = 0
        '      oRow("TaxPrice") = 0


    End Sub

    Private Sub AddVoidedItemCollection222()
        Dim vItem As SelectedItemDetail
        Dim nRow As DataRow = dsOrder.Tables("OpenOrders").NewRow

        For Each vItem In voidedItems
            PopulateDataRowForOpenOrder(vItem)
        Next

    End Sub

    Private Sub btnMgrForcePrice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrForcePrice.Click

        Me.pnlMgrCalculationArea.BringToFront()

        If employeeAuthorization.OperationLevel < systemAuthorization.ForcePrice Then
            MsgBox(employeeAuthorization.FullName & " does not have Authorization")
            Exit Sub
        End If
        If isForReopen = True Then
            MsgBox("Check must be open for this function.")
            Exit Sub
        End If

        forceRowNum = Me.checkTotalsMgmtAdj.grdCloseCheck.CurrentCell.RowNumber

        Dim valueSIN As Integer
        Dim valueSII As Integer
        Dim vRow As DataRowView
        Dim begAmount As Decimal

        Try
            valueSIN = CType(Me.checkTotalsMgmtAdj.grdCloseCheck.Item(forceRowNum, 1), Integer)
            valueSII = CType(Me.checkTotalsMgmtAdj.grdCloseCheck.Item(forceRowNum, 2), Integer)
        Catch ex As Exception
            Exit Sub
        End Try

        If valueSII = valueSIN Then
            '   *** main food

            With dvForcePrice
                .Table = dsOrder.Tables("OpenOrders")
                .RowFilter = "sii = " & valueSIN
                .AllowEdit = True
                .Sort = "sin"
            End With

        ElseIf valueSII < valueSIN Then
            '   *** food modifier
            With dvForcePrice
                .Table = dsOrder.Tables("OpenOrders")
                .RowFilter = "sin = " & valueSIN
                .AllowEdit = True
            End With
        End If

        For Each vRow In dvForcePrice
            begAmount += vRow("Price")
        Next


        RemoveAllInCalculationArea()

        Me.forcePriceMgmtAdj = New ForcePrice_UC(begAmount)
        '(CType(Me.checkTotalsMgmtAdj.grdCloseCheck.Item(forceRowNum, 4), Decimal))
        Me.forcePriceMgmtAdj.Location = New Point(12, 12) '(344, 304)
        Me.pnlMgrCalculationArea.Controls.Add(Me.forcePriceMgmtAdj)
        Me.pnlMgrCalculationArea.Visible = True
        '        Me.forcePriceMgmtAdj.Visible = True

    End Sub

    Private Sub ApplyForcePrice(ByVal newAdjAmount As Decimal, ByVal discountedAmount As Decimal) Handles forcePriceMgmtAdj.UpdateAdjGrid
        Dim rowNum As Integer
        Dim oldPrice As Decimal
        Dim oldTax As Decimal
        Dim newPrice As Decimal
        Dim newTax As Decimal
        Dim newSinTax As Decimal

        '   Dim valueSIN As Integer
        Dim vRow As DataRow
        Dim index As Integer
        Dim ffInfo As ForceFreeInfo
        '       RaiseEvent UpdateAdjGrid()

        '     rowNum = Me.grdForcePrice.CurrentCell.RowNumber
        '    valueSIN = CType(Me.grdForcePrice.Item(rowNum, 0), Integer)
        Dim valueSIN As Integer
        Dim valueSII As Integer
        Try
            valueSIN = CType(Me.checkTotalsMgmtAdj.grdCloseCheck.Item(forceRowNum, 1), Integer)
            valueSII = CType(Me.checkTotalsMgmtAdj.grdCloseCheck.Item(forceRowNum, 2), Integer)
        Catch ex As Exception
            Exit Sub
        End Try

        Dim oRow As DataRow
        Dim oldRow As DataRow
        Dim ni As SelectedItemDetail
        Dim iPromo As ItemPromoInfo
        'for voids
        Dim sinArray(dsOrder.Tables("OpenOrders").Rows.Count) As Integer
        Dim count As Integer
        Dim hasRow As Boolean

        iPromo.PromoCode = 6
        iPromo.PromoReason = 112
        iPromo.empID = actingManager.EmployeeID
        madeChanges = True

        Try
            '        sql.cn.Open()
            '           sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()

            If valueSII = valueSIN Then
                '   *** main food
                '   *** for comp'd we have to comp modifiers individually

                For Each oldRow In dsOrder.Tables("OpenOrders").Rows
                    If Not oldRow.RowState = DataRowState.Deleted And Not oldRow.RowState = DataRowState.Detached Then
                        If Not oldRow("ItemID") = 0 Then
                            If oldRow("sii") = valueSIN Then
                                If Not oldRow("ForceFreeID") = 7 And Not oldRow("ForceFreeID") = 8 And Not oldRow("ForceFreeID") = 9 And Not oldRow("ForceFreeID") = -7 And Not oldRow("ForceFreeID") = -8 And Not oldRow("ForceFreeID") = -9 Then
                                    'not ALREADY comp'd or transfered
                                    '    If Not oRow("ItemStatus") = 9 Or Not oRow("ItemStatus") = 8 Then
                                    'above was old
                                    '   iPromo is the adjustment
                                    If oldRow("sin") = valueSIN Then
                                        'this will send only the leading info
                                        iPromo.empID = actingManager.EmployeeID
                                        iPromo.PromoName = "   ** Ajust: " & oldRow("TerminalName")
                                        iPromo.ItemPrice += oldRow("Price")
                                        iPromo.Price += discountedAmount
                                        iPromo.TaxPrice += 0
                                        iPromo.SinTax += 0
                                        iPromo.openOrderID = oldRow("OpenOrderID")
                                        iPromo.taxID = oldRow("TaxID")
                                        iPromo.sii = oldRow("sii")
                                        iPromo.si2 = oldRow("si2")
                                        oldRow("ForceFreeID") = -1 * iPromo.PromoCode

                                        '               iPromo.FunctionID = oldRow("FunctionID")
                                        '              iPromo.FunctionGroup = oldRow("FunctionGroupID")
                                        iPromo.FunctionFlag = "P" '  = oldRow("FunctionFlag")
                                        '             iPromo.CategoryID = oldRow("CategoryID")
                                        '             iPromo.FoodID = oldRow("FoodID")
                                        '             iPromo.DrinkCategoryID = oldRow("DrinkCategoryID")
                                        '            iPromo.DrinkID = oldRow("DrinkID")
                                        ''             iPromo.RoutingID = oldRow("RoutingID")
                                        '           iPromo.PrintPriorityID = oldRow("PrintPriorityID")
                                        If Not oldRow("OrderNumber") Is DBNull.Value Then
                                            iPromo.OrderNumber = oldRow("OrderNumber")
                                        Else
                                            iPromo.OrderNumber = Nothing
                                        End If
                                        iPromo.CheckNumber = oldRow("CheckNumber")
                                        iPromo.CustomerNumber = oldRow("CustomerNumber")
                                        iPromo.CourseNumber = oldRow("CourseNumber")

                                        hasRow = True
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                If hasRow = True Then
                    CompThisItem(iPromo)
                End If

            ElseIf valueSII < valueSIN Then
                '   *** food modifier
                If Not typeProgram = "Online_Demo" Then
                    oRow = (dsOrder.Tables("OpenOrders").Rows.Find(valueSIN))
                Else
                    For Each oRow In dsOrder.Tables("OpenOrders").Rows
                        If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                            If oRow("sin") = valueSIN Then
                                Exit For
                            End If
                        End If
                    Next
                End If

                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If Not oRow("ItemID") = 0 And Not oRow("Price") < 0 Then
                        If Not oRow("ForceFreeID") = 7 And Not oRow("ForceFreeID") = 8 And Not oRow("ForceFreeID") = 9 And Not oRow("ForceFreeID") = -7 And Not oRow("ForceFreeID") = -8 And Not oRow("ForceFreeID") = -9 Then
                            'not ALREADY comp'd or transfered
                            iPromo.empID = actingManager.EmployeeID
                            iPromo.PromoName = "   ** Ajust: " & oRow("TerminalName")
                            iPromo.ItemPrice += oRow("Price")
                            iPromo.Price += discountedAmount
                            iPromo.TaxPrice += 0
                            iPromo.SinTax += 0
                            iPromo.openOrderID = oRow("OpenOrderID")
                            iPromo.taxID = oRow("TaxID")
                            iPromo.sii = oRow("sii")
                            iPromo.si2 = oRow("si2")
                            oRow("ForceFreeID") = -1 * iPromo.PromoCode

                            '          iPromo.FunctionID = oRow("FunctionID")
                            '          iPromo.FunctionGroup = oRow("FunctionGroupID")
                            iPromo.FunctionFlag = "P" ' = oRow("FunctionFlag")
                            '          iPromo.CategoryID = oRow("CategoryID")
                            '          iPromo.FoodID = oRow("FoodID")
                            ''          iPromo.DrinkCategoryID = oRow("DrinkCategoryID")
                            '         iPromo.DrinkID = oRow("DrinkID")
                            '         iPromo.RoutingID = oRow("RoutingID")
                            '        iPromo.PrintPriorityID = oRow("PrintPriorityID")
                            If Not oRow("OrderNumber") Is DBNull.Value Then
                                iPromo.OrderNumber = oRow("OrderNumber")
                            Else
                                iPromo.OrderNumber = Nothing
                            End If
                            iPromo.CheckNumber = oRow("CheckNumber")
                            iPromo.CustomerNumber = oRow("CustomerNumber")
                            iPromo.CourseNumber = oRow("CourseNumber")

                            CompThisItem(iPromo)

                        End If
                    End If
                End If
            End If
            '         sql.cn.Close()

            For Each ni In newItemCollection
                currentTable.SIN += 1
                GenerateOrderTables.PopulateDataRowForOpenOrder(ni)
            Next
            newItemCollection.Clear()
        Catch ex As Exception
            '          CloseConnection()
            newItemCollection.Clear()
            MsgBox(ex.Message)
        End Try

        Me.checkTotalsMgmtAdj.CalculatePriceAndTax(currentTable.CheckNumber)
        Me.checkTotalsMgmtAdj.AttachTotalsToTotalView(currentTable.CheckNumber)
        Me.forcePriceMgmtAdj.Dispose()
        Me.pnlMgrCalculationArea.Visible = False



    End Sub

    Private Sub DisposingForcePrice() Handles forcePriceMgmtAdj.DisposeForcePrice

        Me.forcePriceMgmtAdj.Dispose()
        Me.pnlMgrCalculationArea.Visible = False

    End Sub

    Private Sub btnMgrCompItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrCompItem.Click

        Me.pnlMgrCalculationArea.BringToFront()

        If employeeAuthorization.OperationLevel < systemAuthorization.CompItem Then
            MsgBox(employeeAuthorization.FullName & " does not have Authorization")
            Exit Sub
        End If
        If isForReopen = True Then
            MsgBox("Check must be open for this function.")
            Exit Sub
        End If

        Dim rowNum As Integer = Me.checkTotalsMgmtAdj.grdCloseCheck.CurrentCell.RowNumber

        Dim valueSIN As Integer
        Dim valueSII As Integer
        Try
            valueSIN = CType(Me.checkTotalsMgmtAdj.grdCloseCheck.Item(rowNum, 1), Integer)
            valueSII = CType(Me.checkTotalsMgmtAdj.grdCloseCheck.Item(rowNum, 2), Integer)
        Catch ex As Exception
            Exit Sub
        End Try

        Dim oRow As DataRow
        Dim oldRow As DataRow
        Dim ni As SelectedItemDetail
        Dim iPromo As ItemPromoInfo
        'for voids
        Dim sinArray(dsOrder.Tables("OpenOrders").Rows.Count) As Integer
        Dim count As Integer
        Dim hasRow As Boolean

        iPromo.PromoCode = 7
        iPromo.PromoReason = 112
        iPromo.empID = actingManager.EmployeeID
        madeChanges = True

        Try
            '        sql.cn.Open()
            '             sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()

            If valueSII = valueSIN Then
                '   *** main food
                '   *** for comp'd we have to comp modifiers individually

                For Each oldRow In dsOrder.Tables("OpenOrders").Rows
                    If Not oldRow.RowState = DataRowState.Deleted And Not oldRow.RowState = DataRowState.Detached Then
                        If Not oldRow("ItemID") = 0 Then    'not customer panel
                            If oldRow("sii") = valueSIN Then
                                If Not oldRow("ForceFreeID") = 7 And Not oldRow("ForceFreeID") = 8 And Not oldRow("ForceFreeID") = 9 And Not oldRow("ForceFreeID") = -7 And Not oldRow("ForceFreeID") = -8 And Not oldRow("ForceFreeID") = -9 Then
                                    'not ALREADY comp'd or transfered
                                    '        If Not oRow("ItemStatus") = 7 And Not oRow("ItemStatus") = 8 And Not oRow("ItemStatus") = 9 Then
                                    'above was old
                                    iPromo.empID = actingManager.EmployeeID
                                    iPromo.ItemPrice += oldRow("Price")
                                    iPromo.Price += oldRow("Price")
                                    iPromo.TaxPrice += oldRow("TaxPrice")
                                    iPromo.SinTax += oldRow("SinTax")

                                    oldRow("ForceFreeID") = -7

                                    If oldRow("sin") = valueSIN Then
                                        'this will send only the leading info
                                        iPromo.PromoName = "   ** Comp'd: " & oldRow("TerminalName")
                                        iPromo.openOrderID = oldRow("OpenOrderID")
                                        iPromo.taxID = oldRow("TaxID")
                                        iPromo.sii = oldRow("sii")
                                        iPromo.si2 = oldRow("si2")
                                        If Not oldRow("OrderNumber") Is DBNull.Value Then
                                            iPromo.OrderNumber = oldRow("OrderNumber")
                                        Else
                                            iPromo.OrderNumber = Nothing
                                        End If
                                        iPromo.CheckNumber = oldRow("CheckNumber")
                                        iPromo.CustomerNumber = oldRow("CustomerNumber")
                                        iPromo.CourseNumber = oldRow("CourseNumber")

                                        '            iPromo.FunctionID = oldRow("FunctionID")
                                        '            iPromo.FunctionGroup = oldRow("FunctionGroupID")
                                        iPromo.FunctionFlag = "P" '  oldRow("FunctionFlag")
                                        '            iPromo.CategoryID = oldRow("CategoryID")
                                        '           iPromo.FoodID = oldRow("FoodID")
                                        '           iPromo.DrinkCategoryID = oldRow("DrinkCategoryID")
                                        '           iPromo.DrinkID = oldRow("DrinkID")
                                        '           iPromo.RoutingID = oldRow("RoutingID")
                                        '           iPromo.PrintPriorityID = oldRow("PrintPriorityID")
                                        hasRow = True
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                If hasRow = True Then
                    CompThisItem(iPromo)
                End If

            ElseIf valueSII < valueSIN Then
                '   *** food modifier
                If Not typeProgram = "Online_Demo" Then
                    oRow = (dsOrder.Tables("OpenOrders").Rows.Find(valueSIN))
                Else
                    For Each oRow In dsOrder.Tables("OpenOrders").Rows
                        If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                            If oRow("sin") = valueSIN Then
                                Exit For
                            End If
                        End If
                    Next
                End If

                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If Not oRow("ItemID") = 0 And Not oRow("Price") < 0 Then
                        If Not oRow("ForceFreeID") = 7 And Not oRow("ForceFreeID") = 8 And Not oRow("ForceFreeID") = 9 And Not oRow("ForceFreeID") = -7 And Not oRow("ForceFreeID") = -8 And Not oRow("ForceFreeID") = -9 Then
                            'not ALREADY comp'd or transfered
                            iPromo.empID = actingManager.EmployeeID
                            iPromo.PromoName = "   ** Comp'd: " & oRow("TerminalName")
                            iPromo.ItemPrice += oRow("Price")
                            iPromo.Price += oRow("Price")
                            iPromo.TaxPrice += oRow("TaxPrice")
                            iPromo.SinTax += oRow("SinTax")
                            iPromo.openOrderID = oRow("OpenOrderID")
                            iPromo.taxID = oRow("TaxID")
                            iPromo.sii = oRow("sii")
                            iPromo.si2 = oRow("si2")

                            '            iPromo.FunctionID = oRow("FunctionID")
                            '            iPromo.FunctionGroup = oRow("FunctionGroupID")
                            iPromo.FunctionFlag = "P" '  = oRow("FunctionFlag")
                            '            iPromo.CategoryID = oRow("CategoryID")
                            '            iPromo.FoodID = oRow("FoodID")
                            '             iPromo.DrinkCategoryID = oRow("DrinkCategoryID")
                            '            iPromo.DrinkID = oRow("DrinkID")
                            '            iPromo.RoutingID = oRow("RoutingID")
                            '           iPromo.PrintPriorityID = oRow("PrintPriorityID")
                            If Not oRow("OrderNumber") Is DBNull.Value Then
                                iPromo.OrderNumber = oRow("OrderNumber")
                            Else
                                iPromo.OrderNumber = Nothing
                            End If

                            iPromo.CheckNumber = oRow("CheckNumber")
                            iPromo.CustomerNumber = oRow("CustomerNumber")
                            iPromo.CourseNumber = oRow("CourseNumber")
                            oRow("ForceFreeID") = -7    'iPromo.PromoCode
                            CompThisItem(iPromo)

                        End If
                    End If
                End If
            End If
            '       sql.cn.Close()

            For Each ni In newItemCollection
                GenerateOrderTables.PopulateDataRowForOpenOrder(ni)
            Next
            newItemCollection.Clear()
        Catch ex As Exception
            '   CloseConnection()
            newItemCollection.Clear()
            MsgBox(ex.Message)
        End Try

        Me.checkTotalsMgmtAdj.CalculatePriceAndTax(currentTable.CheckNumber)
        Me.checkTotalsMgmtAdj.AttachTotalsToTotalView(currentTable.CheckNumber)

    End Sub


    '09.10
    '      If ffInfo.ffID > 0 Then
    '      oRow("ForceFreeID") = ffInfo.ffID
    '  Else
    '      oRow("ForceFreeID") = -9
    '     '      oRow("ForceFreeCode") = oRow("Price")
    ' End If
    '     oRow("ForceFreeAuth") = actingManager.EmployeeID
    '     oRow("ItemName") = oRow("ItemName") & "  ** Comp'd"
    '     oRow("Price") = 0
    ''     oRow("TaxPrice") = 0
    '      oRow("SinTax") = 0



    '
    '    Private Sub UpdateAdjGrid() Handles forcePriceMgmtAdj.UpdateAdjGrid
    '
    '       If Me.checkTotalsMgmtAdj.grdCloseCheck.CurrentRowIndex > 0 Then
    '          Me.checkTotalsMgmtAdj.grdCloseCheck.CurrentRowIndex -= 1
    '         Me.checkTotalsMgmtAdj.grdCloseCheck.CurrentRowIndex += 1
    '    Else
    '       Me.checkTotalsMgmtAdj.grdCloseCheck.CurrentRowIndex += 1
    '            Me.checkTotalsMgmtAdj.grdCloseCheck.CurrentRowIndex -= 1
    '
    '       End If
    '
    '   End Sub

    Private Sub UpdatePaymentListView() Handles checkAdjMgmtAdj.UpdateAdjGridPayment
        madeChanges = True
        btnManagerCancel.Enabled = False
        Me.checkTotalsMgmtAdj.AttachTotalsToTotalView(currentTable.CheckNumber)

    End Sub

    Private Sub ButtonManagerCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManagerCancel.Click

        RejectChanges_Manager()
        RaiseEvent ReinitializeMain(False, True)

    End Sub

    Private Sub btnAdjAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjAccept.Click

        RaiseEvent ReinitializeMain(True, True)

    End Sub

    Private Sub RejectChanges_Manager()

        dsOrder.RejectChanges()

    End Sub

    Private Sub btnAdjustPay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjustPay.Click

        If employeeAuthorization.OperationLevel < systemAuthorization.AdjustPayment Then
            MsgBox(employeeAuthorization.FullName & " does not have Authorization")
            Exit Sub
        End If

        If isForReopen = True Then
            MsgBox("Check must be open for this function.")
            Exit Sub
        End If

        Me.pnlMgrCalculationArea.BringToFront()
        '   this is temp

        RemoveAllInCalculationArea()
        '    GeneratePaymentsAndCreditsDataviews()
        With dvPaymentsAndCredits
            .Table = dsOrder.Tables("PaymentsAndCredits")
            .RowFilter = "(Applied = True AND BatchCleared = False) AND (PaymentFlag = 'cc' OR PaymentFlag = 'Gift' OR PaymentFlag = 'outside' OR PaymentTypeID = '-98')"
        End With

        Me.checkAdjMgmtAdj = New CheckAdjustmentOverride_UC
        Me.checkAdjMgmtAdj.Location = New Point(12, 12) '(344, 304)
        Me.pnlMgrCalculationArea.Controls.Add(Me.checkAdjMgmtAdj)
        Me.pnlMgrCalculationArea.Visible = True

    End Sub


    Private Sub btnCashBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrCashBack.Click

        If employeeAuthorization.OperationLevel < systemAuthorization.AdjustPayment Then
            'same authorization as adjust payment
            MsgBox(employeeAuthorization.FullName & " does not have Authorization")
            Exit Sub
        End If

        If isForReopen = True Then
            MsgBox("Check must be open for this function.")
            Exit Sub
        End If

        With dvPaymentsAndCredits
            .Table = dsOrder.Tables("PaymentsAndCredits")
            .RowFilter = "Applied = True AND CheckNumber = " & currentTable.CheckNumber & " AND PaymentFlag = 'Cash'"
        End With

        Dim vRow As DataRowView
        Dim maxCashAmount As Decimal

        Panel1.Enabled = False
        RemoveAllInCalculationArea()
        Me.pnlMgrCalculationArea.Visible = False

        For Each vRow In dvPaymentsAndCredits
            maxCashAmount += vRow("PaymentAmount")
        Next

        cashOut = New CashOut_UC(currentTable.ExperienceNumber, 1, maxCashAmount)  'expnum, payTypeID, MaxCashOut
        cashOut.Location = New Point((Me.Width - cashOut.Width) / 2, (Me.Height - cashOut.Height) / 2)
        Me.cashOut.lblCashOut.Text = "Refund Cash"
        Me.Controls.Add(cashOut)
        cashOut.BringToFront()


    End Sub

    Private Sub CashOutCanceled(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cashOut.CancelCashOut

        Me.Panel1.Enabled = True

    End Sub

    Private Sub CashOutAccepted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cashOut.AcceptCashOut

        Me.Panel1.Enabled = True
        madeChanges = True

        Dim newPayment As DataSet_Builder.Payment
        Dim amount As Decimal

        If cashOut.ItemPrice > 0 Then
            amount = -1 * cashOut.ItemPrice
            newPayment.Purchase = Format(amount, "##,##0.00")
            newPayment.PaymentTypeID = cashOut.PaymentTypeID
            newPayment.PaymentTypeName = "Cash"   '"Enter Acct #"
            newPayment.Description = cashOut.ItemDescription
          
            GenerateOrderTables.AddPaymentToDataRow(newPayment, True, currentTable.ExperienceNumber, actingManager.EmployeeID, currentTable.CheckNumber, False)
            '        GenerateOrderTables.UpdatePaymentsAndCredits()
        End If

        cashOut.Dispose()
        prt.PrintOpenCashDrawer()
        Me.checkTotalsMgmtAdj.CalculatePriceAndTax(currentTable.CheckNumber)
        Me.checkTotalsMgmtAdj.AttachTotalsToTotalView(currentTable.CheckNumber)

    End Sub


    Private Sub RemoveAllInCalculationArea()

        Me.pnlMgrCalculationArea.Controls.Clear()

    End Sub


    Private Sub lblMgrByItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMgrByItem.Click

        Me.pnlMgrByItem.BringToFront()

    End Sub

    Private Sub lblMgrByCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMgrByCheck.Click

        Me.pnlMgrByCheck.BringToFront()

    End Sub

    Private Sub lblMgrByPayments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMgrByPayments.Click
        Me.pnlMgrByPayments.BringToFront()

    End Sub

    Private Sub btnMgrReprintCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrReprintCheck.Click

        Dim prt As New PrintHelper

        If employeeAuthorization.OperationLevel < systemAuthorization.ReprintCheck Then
            MsgBox(employeeAuthorization.FullName & " does not have Authorization")
            Exit Sub
        End If

        CreateClosingDataViews(currentTable.CheckNumber, True)
        prt.closeDetail.dvClosing = dvOrder   'dvClosingCheck
        prt.closeDetail.chkSubTotal = checkTotalsMgmtAdj.chkSubTotal
        '    prt.closeDetail.checkTax = checkTotalsMgmtAdj.checkTax
        prt.closeDetail.chktaxName = checkTotalsMgmtAdj.taxName
        prt.closeDetail.chktaxTotal = checkTotalsMgmtAdj.taxTotal

        prt.StartPrintCheckReceipt() '(dvClosingCheck, checkTotalsMgmtAdj.chkSubTotal, checkTotalsMgmtAdj.checkTax)

        '       Me.pnlMgrCalculationArea.BringToFront()

    End Sub

    Private Sub btnMgrReprintOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrReprintOrder.Click

        '      MsgBox("Order does NOT reprint. Inform Kitchen.")
        '     Exit Sub

        Dim rRow As DataRow
        Dim oDetail As OrderDetailInfo
        Dim reprintOrderNumber As Int64
        Dim prt As New PrintHelper


        If employeeAuthorization.OperationLevel < systemAuthorization.ReprintOrder Then
            MsgBox(employeeAuthorization.FullName & " does not have Authorization")
            Exit Sub
        End If


        If dsOrder.Tables("OrderDetail").Rows.Count > 0 Then
            rRow = dsOrder.Tables("OrderDetail").Rows(dsOrder.Tables("OrderDetail").Rows.Count - 1)
            '   oDetail.trunkOrderNumber = rRow("OrderNumber")
            reprintOrderNumber = (rRow("OrderNumber"))
            prt.oDetail.trunkOrderNumber = reprintOrderNumber  ' = oDetail
            prt.SendingOrder(reprintOrderNumber)
        End If
        '      prt.oDetail.trunkOrderNumber = reprintOrderNumber  ' = oDetail
        '     prt.SendingOrder(reprintOrderNumber)

        Exit Sub
        '222
        If dsOrder.Tables("OrderDetail").Rows.Count = 1 Then
            rRow = dsOrder.Tables("OrderDetail").Rows(0)
            oDetail.trunkOrderNumber = rRow("OrderNumber")
            prt.oDetail = oDetail
            prt.SendingOrder(rRow("OrderNumber"))
        Else
            For Each rRow In dsOrder.Tables("OrderDetail").Rows
                '   MsgBox(oRow("OrderNumber"))
            Next
            'currently only printing last order
            oDetail.trunkOrderNumber = rRow("OrderNumber")
            prt.oDetail = oDetail
            prt.SendingOrder(rRow("OrderNumber"))
        End If

        '   *** we need to list all the order printed for this exp number
        'routing are from term_OrderForm:
        'FirstStepOrdersPending
        'SendingOrderRoutine
        '    Me.pnlMgrCalculationArea.BringToFront()

    End Sub

    Private Sub btnMgrReopenCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrReopenCheck.Click

        '   FOR CLOSING AND REOPENGING
        Dim vRow As DataRowView
        '    Me.pnlMgrCalculationArea.BringToFront()

        If isForReopen = True Then
            If employeeAuthorization.OperationLevel < systemAuthorization.ReopenCheck Then
                MsgBox(employeeAuthorization.FullName & " does not have Authorization")
                Exit Sub
            End If

            If currentTable.IsTabNotTable = False Then
                For Each vRow In dvClosedTables 'dsOrder.Tables("ClosedTables").Rows
                    If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        vRow("TabName") = "ReOpened " & currentTable.TableNumber.ToString
                        vRow("LastStatusTime") = Now
                        vRow("ClosedSubTotal") = DBNull.Value
                        vRow("LastStatus") = 2
                        'do not change AvailForSeating Time
                        'this check may reopen and close 2 weeks later 
                        Exit For
                    End If
                Next
                For Each vRow In dvAvailTables  'dsOrder.Tables("AvailTables").Rows
                    If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        vRow("TabName") = "ReOpened " & currentTable.TableNumber.ToString
                        vRow("LastStatusTime") = Now
                        vRow("ClosedSubTotal") = DBNull.Value
                        vRow("LastStatus") = 2
                        Exit For
                    End If
                Next
                currentTable.IsClosed = False
                currentTable.LastStatus = 2

                '       If typeProgram = "Online_Demo" Then
                '      ' is table
                '     dsOrder.Tables("AvailTables").Clear()
                'End If

            Else
                For Each vRow In dvClosedTabs   'dsOrder.Tables("ClosedTabs").Rows
                    If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        vRow("LastStatusTime") = Now
                        vRow("ClosedSubTotal") = DBNull.Value
                        vRow("LastStatus") = 2
                        Exit For
                    End If
                Next
                For Each vRow In dvAvailTabs    'dsOrder.Tables("AvailTabs").Rows
                    If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        vRow("LastStatusTime") = Now
                        vRow("ClosedSubTotal") = DBNull.Value
                        vRow("LastStatus") = 2
                        Exit For
                    End If
                Next
                For Each vRow In dvQuickTickets
                    If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        vRow("LastStatusTime") = Now
                        vRow("ClosedSubTotal") = DBNull.Value
                        vRow("LastStatus") = 2
                        Exit For
                    End If
                Next

                '                If typeProgram = "Online_Demo" Then
                '               ' is Tab or ticket
                '              If currentTable.TicketNumber = 0 Then
                '             dsOrder.Tables("AvailTabs").Clear()
                '            Else
                '               dsOrder.Tables("QuickTickets").Clear()
                ''          End If
                '    End If

                currentTable.IsClosed = False
                currentTable.LastStatus = 2
            End If

            RaiseEvent PlacingOrder()

            '          If mainServerConnected = True Then
            '         GenerateOrderTables.AddStatusChangeData(currentTable.ExperienceNumber, 10, Nothing, 0, Nothing)
            '        GenerateOrderTables.SaveAvailTabsAndTables()
            '       RaiseEvent PlacingOrder()
            '  Else
            '     MsgBox("You can only reopen a check when connected to the main Server")
            'End If

        Else
            '   we close the check
            '         currentServer = actingManager
            '       actingManager = Nothing

            RaiseEvent MgrClosingCheck()
            '   event is in Manager_Form

            '******************************
            '   when doing this we have to redo the closing events in Manager Form 
            '   for close exit and release
            '   **************************************
            '      Me.Dispose()

        End If

    End Sub

    Private Sub btnMgrAssignComps_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrAssignComps.Click

        If employeeAuthorization.OperationLevel < systemAuthorization.AssignComps Then
            MsgBox(employeeAuthorization.FullName & " does not have Authorization")
            Exit Sub
        End If
        If isForReopen = True Then
            MsgBox("Check must be open for this function.")
            Exit Sub
        End If
        Me.pnlMgrCalculationArea.BringToFront()

        RemoveAllInCalculationArea()
        Me.checkTotalsMgmtAdj.CalculatePriceAndTax(currentTable.CheckNumber)

        Me.compTktMgmtAdj = New Comp_Ticket_UC(Me.checkTotalsMgmtAdj.chkFood, Me.checkTotalsMgmtAdj.chkDrinks, checkTotalsMgmtAdj.chkSubTotal, checkTotalsMgmtAdj.checkTax, checkTotalsMgmtAdj.checkSinTax)  'checkTotalsMgmtAdj.taxName, checkTotalsMgmtAdj.taxTotal) '
        Me.compTktMgmtAdj.Location = New Point(12, 12)
        Me.pnlMgrCalculationArea.Controls.Add(Me.compTktMgmtAdj)
        Me.pnlMgrCalculationArea.Visible = True

    End Sub

    Private Sub DisposingCompTicket() Handles compTktMgmtAdj.DisposeCompTicket

        Me.compTktMgmtAdj.Dispose()
        Me.pnlMgrCalculationArea.Visible = False

    End Sub

    Private Sub AcceptingCompTicket() Handles compTktMgmtAdj.AcceptCompTicket

        Dim iPromo As ItemPromoInfo
        Dim oRow As DataRow
        Dim ni As SelectedItemDetail

        iPromo.PromoCode = 7
        iPromo.PromoReason = 112
        iPromo.PromoName = "   ** Manager Comp"
        iPromo.empID = actingManager.EmployeeID
        iPromo.ItemPrice = compTktMgmtAdj.ManualTicket
        iPromo.Price = compTktMgmtAdj.AdjPrice
        iPromo.TaxPrice = compTktMgmtAdj.AdjTax
        iPromo.SinTax = compTktMgmtAdj.AdjSinTax

        'not sure of below
        iPromo.FunctionID = 0 'oRow("FunctionID")
        iPromo.FunctionGroup = 0 'oRow("FunctionGroupID")
        iPromo.FunctionFlag = "P" 'oRow("FunctionFlag")
        iPromo.CategoryID = 0 'oRow("CategoryID")
        iPromo.FoodID = 0 'oRow("FoodID")
        iPromo.DrinkCategoryID = 0 'oRow("DrinkCategoryID")
        iPromo.DrinkID = 0 'oRow("DrinkID")
        iPromo.RoutingID = 0 'oRow("RoutingID")
        iPromo.PrintPriorityID = 0 'oRow("PrintPriorityID")

        iPromo.ItemStatus = 7
        iPromo.openOrderID = 0
        '**************
        '666
        'we have to do a loop here for each tax code 
        iPromo.taxID = ds.Tables("Tax").Rows(0)("TaxID")    '0
        iPromo.sii = currentTable.SIN + 1
        iPromo.si2 = 0
        madeChanges = True

        Try
            '       sql.cn.Open()
            '             sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            CompThisItem(iPromo)
            For Each ni In newItemCollection
                GenerateOrderTables.PopulateDataRowForOpenOrder(ni)
            Next
            newItemCollection.Clear()
            '      sql.cn.Close()
            If compTktMgmtAdj.ManualTicket = compTktMgmtAdj.CheckTicket Then
                'this means we are comping the entire check
                'otherwise we are not recording comps on the items
                For Each oRow In dsOrder.Tables("OpenOrders").Rows
                    If oRow("ForceFreeID") = 0 Then
                        oRow("ForceFreeID") = iPromo.PromoCode * -1

                    End If
                Next
            ElseIf compTktMgmtAdj.AllFood = True Then
                If compTktMgmtAdj.ManualTicket = compTktMgmtAdj.CheckFood Then
                    '**** Manual Ticket is Adjustment Amount
                    For Each oRow In dsOrder.Tables("OpenOrders").Rows
                        If oRow("FunctionFlag") = "F" Or oRow("FunctionFlag") = "O" Or oRow("FunctionFlag") = "M" Then
                            If oRow("ForceFreeID") = 0 Then
                                oRow("ForceFreeID") = iPromo.PromoCode * -1

                            End If
                        End If
                    Next
                End If

            ElseIf compTktMgmtAdj.AllDrinks = True Then
                If compTktMgmtAdj.ManualTicket = compTktMgmtAdj.CheckDrinks Then
                    For Each oRow In dsOrder.Tables("OpenOrders").Rows
                        If oRow("FunctionFlag") = "D" Then
                            If oRow("ForceFreeID") = 0 Then
                                oRow("ForceFreeID") = iPromo.PromoCode * -1

                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

        Me.compTktMgmtAdj.Dispose()
        Me.pnlMgrCalculationArea.Visible = False
        Me.checkTotalsMgmtAdj.CalculatePriceAndTax(currentTable.CheckNumber)
        Me.checkTotalsMgmtAdj.AttachTotalsToTotalView(currentTable.CheckNumber)

    End Sub
    Private Sub btnMgrAssignGratuity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrAssignGratuity.Click

        MsgBox("Assign Gratuity in Close Check.")
        Exit Sub
        Me.pnlMgrCalculationArea.BringToFront()

    End Sub

    Private Sub btnMgrPlaceOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrPlaceOrder.Click
        '    time1 = Now

        '    SaveOpenOrderData()
        currentTable.SIN += 1

        If isForReopen = True Then
            MsgBox("Check must be open for this function.")
            Exit Sub
        End If

        RaiseEvent PlacingOrder()

    End Sub

    Private Sub btnMgrTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrTransfer.Click

        If employeeAuthorization.OperationLevel < systemAuthorization.TransferCheck Then 'And employeeAuthorization.OperationLevel < systemAuthorization.TransferItem Then
            MsgBox(employeeAuthorization.FullName & " does not have Authorization")
            Exit Sub
        End If
        If isForReopen = True Then
            MsgBox("Check must be open for this function.")
            Exit Sub
        End If
        If madeChanges = True Then
            MsgBox("You Must ACCEPT changes before this action.")
            Exit Sub
        End If

        Dim rowNum As Integer = Me.checkTotalsMgmtAdj.grdCloseCheck.CurrentCell.RowNumber

        Try
            _transSIN = CType(Me.checkTotalsMgmtAdj.grdCloseCheck.Item(rowNum, 1), Integer)
            _transName = CType(Me.checkTotalsMgmtAdj.grdCloseCheck.Item(rowNum, 3), String)
        Catch ex As Exception
            _transSIN = Nothing
            _transName = Nothing
            '          Exit Sub
        End Try

        RaiseEvent TransferingCheck()
        '     Me.Dispose()

    End Sub

    Private Sub btnMgrTaxExempt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrTaxExempt.Click

        Dim oRow As DataRow

        Me.pnlMgrCalculationArea.Visible = False

        If employeeAuthorization.OperationLevel < systemAuthorization.TaxExempt Then
            MsgBox(employeeAuthorization.FullName & " does not have Authorization")
            Exit Sub
        End If

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If oRow("CheckNumber") = currentTable.CheckNumber Then
                If oRow("TaxID") > 0 Then
                    oRow("TaxID") = -1
                    oRow("TaxPrice") = 0 'DetermineTaxPrice(taxID, newitem.Price)
                    oRow("SinTax") = 0 'DetermineSinTax(taxID, newitem.Price)
                End If
            End If
        Next

        Me.checkTotalsMgmtAdj.CalculatePriceAndTax(currentTable.CheckNumber)
        Me.checkTotalsMgmtAdj.AttachTotalsToTotalView(currentTable.CheckNumber)

        MsgBox("Check Number " & currentTable.CheckNumber & " is now Tax Exempt. Select Cancel to re-add Tax.")

    End Sub

    Private Sub btnMgrReprintCredit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrReprintCredit.Click

        '**************************
        ' I need to add chioices for which credit card and if customer or restaurant copy

        If employeeAuthorization.OperationLevel < systemAuthorization.ReprintCredit Then
            MsgBox(employeeAuthorization.FullName & " does not have Authorization")
            Exit Sub
        End If

        Dim oRow As DataRow
        Dim prt As New PrintHelper

        ' we are doing all the credit card payments first
        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                '         If oRow("CheckNumber") = currentTable.CheckNumber Then
                '        If oRow("PaymentAmount") <> 0 Then
                '       If oRow("Applied") = False Then
                If oRow("PaymentFlag") = "cc" Then
                    '  PrintCreditCardReceipt(oRow, Nothing, False)
                    prt.ccDataRow = oRow
                    prt.ccDataRowView = Nothing
                    prt.useVIEW = False
                    prt.StartPrintCreditCardRest()
                    prt.StartPrintCreditCardGuest()
                End If
            End If
            '  End If
            ' End If
            'End If
        Next

    End Sub


End Class
