Imports DataSet_Builder

Public Class Manager_Main_UC
    Inherits System.Windows.Forms.UserControl

    '   Dim sql As New DataSet_Builder.SQLHelper(connectserver)

    Dim WithEvents btnClosedTable As Button
    Dim WithEvents floorPersonnel As KitchenButton
    Dim WithEvents allPersonnel As KitchenButton
    Dim closingDailyCode As Int64
    Dim expNum As Int64

    Dim primaryMenuSelected As Boolean
    Dim justChangingMenu As Boolean

    Dim WithEvents mgrClockAdjustment As Button
    Dim WithEvents mgrTipAdjustment As Button
    Dim WithEvents mgrPayRateAdjustment As Button       ' not sure if using
    Friend WithEvents mgrLargeNumberPad As NumberPad
    Dim countNumberOfAttemptsToLeave As Integer
    Dim mainPanelCounterIndex As Integer = 0
    Dim displayingOpenOrClose As String

    '   Dim WithEvents employeeLog As EmployeeLoggedInUserControl
    Dim WithEvents selectDaily As DataSet_Builder.SelectionPanel_UC
    Dim WithEvents openDaily As DataSet_Builder.MenuSelection_UC
    Dim WithEvents changeMenu As DataSet_Builder.MenuSelection_UC
    Dim WithEvents invDelivery As DataSet_Builder.InvDelivery_UC
    Dim WithEvents invPhysical As DataSet_Builder.InvPhysical_UC
    Dim WithEvents reportManager As DataSet_Builder.Manager_Reports_UC
    Dim WithEvents trainingDailys As Training_UC
    Friend WithEvents grpEmployeeClockIn As System.Windows.Forms.GroupBox
    Friend WithEvents grdEmpClockIn As System.Windows.Forms.DataGrid
    Friend WithEvents lblNumClockedIn As System.Windows.Forms.Label
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn

    '  Dim info As DataSet_Builder.Info2_UC
    '   Dim WithEvents closeBatch As BatchClose_UC
    Dim WithEvents returnCredit As ReturnCredit_UC
    Dim WithEvents cashOut As CashOut_UC
    Dim WithEvents cashDrawer As CashDrawer_UC
    Dim WithEvents employeeLog As EmployeeLoggedInUserControl

    Dim allPaymentsLoaded As Boolean
    Dim allTicketsOpen As Integer
    Dim allCashDrawersOpen As Integer
    Dim allEmployeesClockedIn As Integer
    '  Dim weAreClosingDaily As Boolean
    Dim numberOfAttemtsClosingDaily As Integer
    Dim skipClosedIndex As Integer = 0
    
    Friend availTableChangesMade As Boolean
    Dim usernameEnterOnLogin As Boolean

    Dim activeCollection As New EmployeeCollection

    Dim OperationFlag As Boolean
    Dim EmployeeFlag As Boolean
    Dim ReportsFlag As Boolean
    Dim MenuFlag As Boolean
    Dim SystemFlag As Boolean
    Dim DailysFlag As Boolean
    Dim InventoryFlag As Boolean
    Dim InventoryTable As String

    Private _tableSelected As Integer
    Private _reopenFlag As Boolean
    Friend WithEvents pnlMoreTickets As System.Windows.Forms.Panel
    Friend WithEvents btnLessTickets As System.Windows.Forms.Button
    Friend WithEvents btnMoreTickets As System.Windows.Forms.Button
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents subButton5 As System.Windows.Forms.Button
    Private _reopenIndex As Int64


    Friend Property ReopenFlag() As Boolean
        Get
            Return _reopenFlag
        End Get
        Set(ByVal Value As Boolean)
            _reopenFlag = Value
        End Set
    End Property

    Friend Property ReopenIndex() As Int64
        Get
            Return _reopenIndex
        End Get
        Set(ByVal Value As Int64)
            _reopenIndex = Value
        End Set
    End Property

    Friend Property TableSelected() As Integer
        Get
            Return _tableSelected
        End Get
        Set(ByVal Value As Integer)
            _tableSelected = Value
        End Set
    End Property

    Event OpenOrderAdjustment(ByVal sender As Object, ByVal e As System.EventArgs, ByVal closingDailyCode As Int64)
    Event CloseBatchManagerForm(ByVal cb As Int64)
    Event OpenReports()
    Event OpenNewTable()
    Event OpenNewTabEvent()
    Event StartExit()
    Event AdjustEmpClock()
    Event OverrideTableStatus()
    Event ReReadCredit()
    Event DisposeManager()


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal userEntered As Boolean)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        usernameEnterOnLogin = userEntered
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
    Friend WithEvents pnlMgrMainCategories As System.Windows.Forms.Panel
    Friend WithEvents btnOperations As System.Windows.Forms.Button
    Friend WithEvents btnEmployees As System.Windows.Forms.Button
    Friend WithEvents btnReports As System.Windows.Forms.Button
    Friend WithEvents pnlMainManager As System.Windows.Forms.Panel
    Friend WithEvents lblMgrDirectionsNumberPad As System.Windows.Forms.Label
    Friend WithEvents lblMgrDirectionsNumberPad2 As System.Windows.Forms.Label
    Friend WithEvents lblMgrDirectionsNumberPad3 As System.Windows.Forms.Label
    Friend WithEvents pnlManagerSelection As System.Windows.Forms.Panel
    Friend WithEvents lblActiingManager As System.Windows.Forms.Label
    Friend WithEvents btnMgrExit As System.Windows.Forms.Button
    Friend WithEvents pnlManagerSubSelecetion As System.Windows.Forms.Panel
    Friend WithEvents SubButtonBack As System.Windows.Forms.Button
    Friend WithEvents SubButton1 As System.Windows.Forms.Button
    Friend WithEvents SubButton2 As System.Windows.Forms.Button
    Friend WithEvents SubButton3 As System.Windows.Forms.Button
    Friend WithEvents SubButton4 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnDailys As System.Windows.Forms.Button
    Friend WithEvents lblCurrentServer As System.Windows.Forms.Label
    Friend WithEvents lblMainMgrInstructions As System.Windows.Forms.Label
    Friend WithEvents pnlMainManagerLarger As System.Windows.Forms.Panel
    Friend WithEvents pnlCDInfo As System.Windows.Forms.Panel
    Friend WithEvents btnCDCashDrawer As System.Windows.Forms.Button
    Friend WithEvents btnCDTickets As System.Windows.Forms.Button
    Friend WithEvents btnCDClockedIn As System.Windows.Forms.Button
    Friend WithEvents pnlClosingDailyDirections As System.Windows.Forms.Panel
    Friend WithEvents lblDailyCloseDesc As System.Windows.Forms.Label
    Friend WithEvents btnClosingContinue As System.Windows.Forms.Button
    Friend WithEvents btnInventory As System.Windows.Forms.Button
    Friend WithEvents btnMenu As System.Windows.Forms.Button
    Friend WithEvents btnSystem As System.Windows.Forms.Button

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Manager_Main_UC))
        Me.pnlMgrMainCategories = New System.Windows.Forms.Panel
        Me.pnlManagerSelection = New System.Windows.Forms.Panel
        Me.btnSystem = New System.Windows.Forms.Button
        Me.btnInventory = New System.Windows.Forms.Button
        Me.btnDailys = New System.Windows.Forms.Button
        Me.btnMgrExit = New System.Windows.Forms.Button
        Me.btnOperations = New System.Windows.Forms.Button
        Me.btnEmployees = New System.Windows.Forms.Button
        Me.btnMenu = New System.Windows.Forms.Button
        Me.btnReports = New System.Windows.Forms.Button
        Me.pnlManagerSubSelecetion = New System.Windows.Forms.Panel
        Me.subButton5 = New System.Windows.Forms.Button
        Me.SubButtonBack = New System.Windows.Forms.Button
        Me.SubButton1 = New System.Windows.Forms.Button
        Me.SubButton2 = New System.Windows.Forms.Button
        Me.SubButton3 = New System.Windows.Forms.Button
        Me.SubButton4 = New System.Windows.Forms.Button
        Me.pnlMainManager = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblMgrDirectionsNumberPad3 = New System.Windows.Forms.Label
        Me.lblMgrDirectionsNumberPad2 = New System.Windows.Forms.Label
        Me.lblMgrDirectionsNumberPad = New System.Windows.Forms.Label
        Me.lblActiingManager = New System.Windows.Forms.Label
        Me.lblCurrentServer = New System.Windows.Forms.Label
        Me.lblMainMgrInstructions = New System.Windows.Forms.Label
        Me.pnlMainManagerLarger = New System.Windows.Forms.Panel
        Me.pnlCDInfo = New System.Windows.Forms.Panel
        Me.btnCDClockedIn = New System.Windows.Forms.Button
        Me.btnCDTickets = New System.Windows.Forms.Button
        Me.btnCDCashDrawer = New System.Windows.Forms.Button
        Me.pnlClosingDailyDirections = New System.Windows.Forms.Panel
        Me.btnClosingContinue = New System.Windows.Forms.Button
        Me.lblDailyCloseDesc = New System.Windows.Forms.Label
        Me.pnlMoreTickets = New System.Windows.Forms.Panel
        Me.btnLessTickets = New System.Windows.Forms.Button
        Me.btnMoreTickets = New System.Windows.Forms.Button
        Me.lblVersion = New System.Windows.Forms.Label
        Me.pnlMgrMainCategories.SuspendLayout()
        Me.pnlManagerSelection.SuspendLayout()
        Me.pnlManagerSubSelecetion.SuspendLayout()
        Me.pnlMainManager.SuspendLayout()
        Me.pnlMainManagerLarger.SuspendLayout()
        Me.pnlCDInfo.SuspendLayout()
        Me.pnlClosingDailyDirections.SuspendLayout()
        Me.pnlMoreTickets.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMgrMainCategories
        '
        Me.pnlMgrMainCategories.BackColor = System.Drawing.Color.Transparent
        Me.pnlMgrMainCategories.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMgrMainCategories.Controls.Add(Me.pnlManagerSelection)
        Me.pnlMgrMainCategories.Controls.Add(Me.pnlManagerSubSelecetion)
        Me.pnlMgrMainCategories.Location = New System.Drawing.Point(16, 112)
        Me.pnlMgrMainCategories.Name = "pnlMgrMainCategories"
        Me.pnlMgrMainCategories.Size = New System.Drawing.Size(132, 616)
        Me.pnlMgrMainCategories.TabIndex = 0
        '
        'pnlManagerSelection
        '
        Me.pnlManagerSelection.BackColor = System.Drawing.Color.Transparent
        Me.pnlManagerSelection.Controls.Add(Me.btnSystem)
        Me.pnlManagerSelection.Controls.Add(Me.btnInventory)
        Me.pnlManagerSelection.Controls.Add(Me.btnDailys)
        Me.pnlManagerSelection.Controls.Add(Me.btnMgrExit)
        Me.pnlManagerSelection.Controls.Add(Me.btnOperations)
        Me.pnlManagerSelection.Controls.Add(Me.btnEmployees)
        Me.pnlManagerSelection.Controls.Add(Me.btnMenu)
        Me.pnlManagerSelection.Controls.Add(Me.btnReports)
        Me.pnlManagerSelection.Location = New System.Drawing.Point(8, 24)
        Me.pnlManagerSelection.Name = "pnlManagerSelection"
        Me.pnlManagerSelection.Size = New System.Drawing.Size(112, 576)
        Me.pnlManagerSelection.TabIndex = 4
        '
        'btnSystem
        '
        Me.btnSystem.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnSystem.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSystem.Location = New System.Drawing.Point(0, 336)
        Me.btnSystem.Name = "btnSystem"
        Me.btnSystem.Size = New System.Drawing.Size(112, 48)
        Me.btnSystem.TabIndex = 7
        Me.btnSystem.Text = "System"
        Me.btnSystem.UseVisualStyleBackColor = False
        '
        'btnInventory
        '
        Me.btnInventory.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnInventory.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventory.Location = New System.Drawing.Point(0, 272)
        Me.btnInventory.Name = "btnInventory"
        Me.btnInventory.Size = New System.Drawing.Size(112, 48)
        Me.btnInventory.TabIndex = 6
        Me.btnInventory.Text = "Inventory"
        Me.btnInventory.UseVisualStyleBackColor = False
        '
        'btnDailys
        '
        Me.btnDailys.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnDailys.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDailys.Location = New System.Drawing.Point(0, 448)
        Me.btnDailys.Name = "btnDailys"
        Me.btnDailys.Size = New System.Drawing.Size(112, 48)
        Me.btnDailys.TabIndex = 5
        Me.btnDailys.Text = "Dailys"
        Me.btnDailys.UseVisualStyleBackColor = False
        '
        'btnMgrExit
        '
        Me.btnMgrExit.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnMgrExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMgrExit.Location = New System.Drawing.Point(0, 520)
        Me.btnMgrExit.Name = "btnMgrExit"
        Me.btnMgrExit.Size = New System.Drawing.Size(112, 48)
        Me.btnMgrExit.TabIndex = 4
        Me.btnMgrExit.Text = "Close"
        Me.btnMgrExit.UseVisualStyleBackColor = False
        '
        'btnOperations
        '
        Me.btnOperations.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnOperations.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOperations.Location = New System.Drawing.Point(0, 16)
        Me.btnOperations.Name = "btnOperations"
        Me.btnOperations.Size = New System.Drawing.Size(112, 48)
        Me.btnOperations.TabIndex = 0
        Me.btnOperations.Text = "Operations"
        Me.btnOperations.UseVisualStyleBackColor = False
        '
        'btnEmployees
        '
        Me.btnEmployees.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnEmployees.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEmployees.Location = New System.Drawing.Point(0, 80)
        Me.btnEmployees.Name = "btnEmployees"
        Me.btnEmployees.Size = New System.Drawing.Size(112, 48)
        Me.btnEmployees.TabIndex = 1
        Me.btnEmployees.Text = "Employees"
        Me.btnEmployees.UseVisualStyleBackColor = False
        '
        'btnMenu
        '
        Me.btnMenu.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnMenu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMenu.Location = New System.Drawing.Point(0, 144)
        Me.btnMenu.Name = "btnMenu"
        Me.btnMenu.Size = New System.Drawing.Size(112, 48)
        Me.btnMenu.TabIndex = 2
        Me.btnMenu.Text = "Menu"
        Me.btnMenu.UseVisualStyleBackColor = False
        '
        'btnReports
        '
        Me.btnReports.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnReports.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReports.Location = New System.Drawing.Point(0, 208)
        Me.btnReports.Name = "btnReports"
        Me.btnReports.Size = New System.Drawing.Size(112, 48)
        Me.btnReports.TabIndex = 3
        Me.btnReports.Text = "Reports"
        Me.btnReports.UseVisualStyleBackColor = False
        '
        'pnlManagerSubSelecetion
        '
        Me.pnlManagerSubSelecetion.BackColor = System.Drawing.Color.Transparent
        Me.pnlManagerSubSelecetion.Controls.Add(Me.subButton5)
        Me.pnlManagerSubSelecetion.Controls.Add(Me.SubButtonBack)
        Me.pnlManagerSubSelecetion.Controls.Add(Me.SubButton1)
        Me.pnlManagerSubSelecetion.Controls.Add(Me.SubButton2)
        Me.pnlManagerSubSelecetion.Controls.Add(Me.SubButton3)
        Me.pnlManagerSubSelecetion.Controls.Add(Me.SubButton4)
        Me.pnlManagerSubSelecetion.Location = New System.Drawing.Point(-24, 8)
        Me.pnlManagerSubSelecetion.Name = "pnlManagerSubSelecetion"
        Me.pnlManagerSubSelecetion.Size = New System.Drawing.Size(112, 576)
        Me.pnlManagerSubSelecetion.TabIndex = 5
        Me.pnlManagerSubSelecetion.Visible = False
        '
        'subButton5
        '
        Me.subButton5.BackColor = System.Drawing.Color.LightSlateGray
        Me.subButton5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.subButton5.Location = New System.Drawing.Point(0, 310)
        Me.subButton5.Name = "subButton5"
        Me.subButton5.Size = New System.Drawing.Size(112, 48)
        Me.subButton5.TabIndex = 5
        Me.subButton5.UseVisualStyleBackColor = False
        '
        'SubButtonBack
        '
        Me.SubButtonBack.BackColor = System.Drawing.Color.LightSlateGray
        Me.SubButtonBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubButtonBack.Location = New System.Drawing.Point(0, 520)
        Me.SubButtonBack.Name = "SubButtonBack"
        Me.SubButtonBack.Size = New System.Drawing.Size(112, 48)
        Me.SubButtonBack.TabIndex = 4
        Me.SubButtonBack.Text = "Back"
        Me.SubButtonBack.UseVisualStyleBackColor = False
        '
        'SubButton1
        '
        Me.SubButton1.BackColor = System.Drawing.Color.LightSlateGray
        Me.SubButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubButton1.Location = New System.Drawing.Point(0, 24)
        Me.SubButton1.Name = "SubButton1"
        Me.SubButton1.Size = New System.Drawing.Size(112, 48)
        Me.SubButton1.TabIndex = 0
        Me.SubButton1.UseVisualStyleBackColor = False
        '
        'SubButton2
        '
        Me.SubButton2.BackColor = System.Drawing.Color.LightSlateGray
        Me.SubButton2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubButton2.Location = New System.Drawing.Point(0, 96)
        Me.SubButton2.Name = "SubButton2"
        Me.SubButton2.Size = New System.Drawing.Size(112, 48)
        Me.SubButton2.TabIndex = 1
        Me.SubButton2.UseVisualStyleBackColor = False
        '
        'SubButton3
        '
        Me.SubButton3.BackColor = System.Drawing.Color.LightSlateGray
        Me.SubButton3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubButton3.Location = New System.Drawing.Point(0, 168)
        Me.SubButton3.Name = "SubButton3"
        Me.SubButton3.Size = New System.Drawing.Size(112, 48)
        Me.SubButton3.TabIndex = 2
        Me.SubButton3.UseVisualStyleBackColor = False
        '
        'SubButton4
        '
        Me.SubButton4.BackColor = System.Drawing.Color.LightSlateGray
        Me.SubButton4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubButton4.Location = New System.Drawing.Point(0, 240)
        Me.SubButton4.Name = "SubButton4"
        Me.SubButton4.Size = New System.Drawing.Size(112, 48)
        Me.SubButton4.TabIndex = 3
        Me.SubButton4.UseVisualStyleBackColor = False
        '
        'pnlMainManager
        '
        Me.pnlMainManager.BackColor = System.Drawing.Color.Black
        Me.pnlMainManager.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMainManager.Controls.Add(Me.Label1)
        Me.pnlMainManager.Controls.Add(Me.lblMgrDirectionsNumberPad3)
        Me.pnlMainManager.Controls.Add(Me.lblMgrDirectionsNumberPad2)
        Me.pnlMainManager.Controls.Add(Me.lblMgrDirectionsNumberPad)
        Me.pnlMainManager.ForeColor = System.Drawing.Color.Black
        Me.pnlMainManager.Location = New System.Drawing.Point(8, 8)
        Me.pnlMainManager.Name = "pnlMainManager"
        Me.pnlMainManager.Size = New System.Drawing.Size(800, 632)
        Me.pnlMainManager.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(24, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 40)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "There are 2 panels to the left"
        Me.Label1.Visible = False
        '
        'lblMgrDirectionsNumberPad3
        '
        Me.lblMgrDirectionsNumberPad3.ForeColor = System.Drawing.Color.White
        Me.lblMgrDirectionsNumberPad3.Location = New System.Drawing.Point(72, 344)
        Me.lblMgrDirectionsNumberPad3.Name = "lblMgrDirectionsNumberPad3"
        Me.lblMgrDirectionsNumberPad3.Size = New System.Drawing.Size(176, 80)
        Me.lblMgrDirectionsNumberPad3.TabIndex = 4
        '
        'lblMgrDirectionsNumberPad2
        '
        Me.lblMgrDirectionsNumberPad2.ForeColor = System.Drawing.Color.White
        Me.lblMgrDirectionsNumberPad2.Location = New System.Drawing.Point(80, 224)
        Me.lblMgrDirectionsNumberPad2.Name = "lblMgrDirectionsNumberPad2"
        Me.lblMgrDirectionsNumberPad2.Size = New System.Drawing.Size(176, 24)
        Me.lblMgrDirectionsNumberPad2.TabIndex = 3
        Me.lblMgrDirectionsNumberPad2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMgrDirectionsNumberPad
        '
        Me.lblMgrDirectionsNumberPad.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMgrDirectionsNumberPad.ForeColor = System.Drawing.Color.White
        Me.lblMgrDirectionsNumberPad.Location = New System.Drawing.Point(80, 176)
        Me.lblMgrDirectionsNumberPad.Name = "lblMgrDirectionsNumberPad"
        Me.lblMgrDirectionsNumberPad.Size = New System.Drawing.Size(176, 32)
        Me.lblMgrDirectionsNumberPad.TabIndex = 1
        Me.lblMgrDirectionsNumberPad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblActiingManager
        '
        Me.lblActiingManager.BackColor = System.Drawing.Color.Transparent
        Me.lblActiingManager.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActiingManager.ForeColor = System.Drawing.Color.Black
        Me.lblActiingManager.Location = New System.Drawing.Point(16, 16)
        Me.lblActiingManager.Name = "lblActiingManager"
        Me.lblActiingManager.Size = New System.Drawing.Size(296, 32)
        Me.lblActiingManager.TabIndex = 0
        Me.lblActiingManager.Text = "Manager:  "
        Me.lblActiingManager.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblCurrentServer
        '
        Me.lblCurrentServer.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentServer.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentServer.ForeColor = System.Drawing.Color.Black
        Me.lblCurrentServer.Location = New System.Drawing.Point(48, 56)
        Me.lblCurrentServer.Name = "lblCurrentServer"
        Me.lblCurrentServer.Size = New System.Drawing.Size(264, 32)
        Me.lblCurrentServer.TabIndex = 3
        Me.lblCurrentServer.Text = "Server:"
        Me.lblCurrentServer.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblMainMgrInstructions
        '
        Me.lblMainMgrInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainMgrInstructions.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblMainMgrInstructions.Location = New System.Drawing.Point(152, 8)
        Me.lblMainMgrInstructions.Name = "lblMainMgrInstructions"
        Me.lblMainMgrInstructions.Size = New System.Drawing.Size(536, 32)
        Me.lblMainMgrInstructions.TabIndex = 4
        Me.lblMainMgrInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlMainManagerLarger
        '
        Me.pnlMainManagerLarger.BackColor = System.Drawing.Color.Black
        Me.pnlMainManagerLarger.Controls.Add(Me.pnlMainManager)
        Me.pnlMainManagerLarger.Controls.Add(Me.lblMainMgrInstructions)
        Me.pnlMainManagerLarger.Location = New System.Drawing.Point(176, 96)
        Me.pnlMainManagerLarger.Name = "pnlMainManagerLarger"
        Me.pnlMainManagerLarger.Size = New System.Drawing.Size(816, 648)
        Me.pnlMainManagerLarger.TabIndex = 5
        '
        'pnlCDInfo
        '
        Me.pnlCDInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlCDInfo.Controls.Add(Me.btnCDClockedIn)
        Me.pnlCDInfo.Controls.Add(Me.btnCDTickets)
        Me.pnlCDInfo.Controls.Add(Me.btnCDCashDrawer)
        Me.pnlCDInfo.Location = New System.Drawing.Point(336, 16)
        Me.pnlCDInfo.Name = "pnlCDInfo"
        Me.pnlCDInfo.Size = New System.Drawing.Size(355, 49)
        Me.pnlCDInfo.TabIndex = 6
        Me.pnlCDInfo.Visible = False
        '
        'btnCDClockedIn
        '
        Me.btnCDClockedIn.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnCDClockedIn.Location = New System.Drawing.Point(232, 9)
        Me.btnCDClockedIn.Name = "btnCDClockedIn"
        Me.btnCDClockedIn.Size = New System.Drawing.Size(112, 32)
        Me.btnCDClockedIn.TabIndex = 2
        Me.btnCDClockedIn.Text = "Clocked In"
        Me.btnCDClockedIn.UseVisualStyleBackColor = False
        '
        'btnCDTickets
        '
        Me.btnCDTickets.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnCDTickets.Location = New System.Drawing.Point(12, 8)
        Me.btnCDTickets.Name = "btnCDTickets"
        Me.btnCDTickets.Size = New System.Drawing.Size(104, 32)
        Me.btnCDTickets.TabIndex = 1
        Me.btnCDTickets.Text = "Tickets"
        Me.btnCDTickets.UseVisualStyleBackColor = False
        '
        'btnCDCashDrawer
        '
        Me.btnCDCashDrawer.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnCDCashDrawer.Location = New System.Drawing.Point(122, 8)
        Me.btnCDCashDrawer.Name = "btnCDCashDrawer"
        Me.btnCDCashDrawer.Size = New System.Drawing.Size(104, 32)
        Me.btnCDCashDrawer.TabIndex = 0
        Me.btnCDCashDrawer.Text = "Cash Drawer"
        Me.btnCDCashDrawer.UseVisualStyleBackColor = False
        '
        'pnlClosingDailyDirections
        '
        Me.pnlClosingDailyDirections.BackColor = System.Drawing.Color.Black
        Me.pnlClosingDailyDirections.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlClosingDailyDirections.Controls.Add(Me.btnClosingContinue)
        Me.pnlClosingDailyDirections.Controls.Add(Me.lblDailyCloseDesc)
        Me.pnlClosingDailyDirections.Location = New System.Drawing.Point(712, 8)
        Me.pnlClosingDailyDirections.Name = "pnlClosingDailyDirections"
        Me.pnlClosingDailyDirections.Size = New System.Drawing.Size(296, 80)
        Me.pnlClosingDailyDirections.TabIndex = 6
        Me.pnlClosingDailyDirections.Visible = False
        '
        'btnClosingContinue
        '
        Me.btnClosingContinue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClosingContinue.ForeColor = System.Drawing.Color.White
        Me.btnClosingContinue.Location = New System.Drawing.Point(200, 16)
        Me.btnClosingContinue.Name = "btnClosingContinue"
        Me.btnClosingContinue.Size = New System.Drawing.Size(80, 48)
        Me.btnClosingContinue.TabIndex = 1
        Me.btnClosingContinue.Text = "Close Batch"
        '
        'lblDailyCloseDesc
        '
        Me.lblDailyCloseDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDailyCloseDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDailyCloseDesc.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblDailyCloseDesc.Location = New System.Drawing.Point(8, 8)
        Me.lblDailyCloseDesc.Name = "lblDailyCloseDesc"
        Me.lblDailyCloseDesc.Size = New System.Drawing.Size(184, 64)
        Me.lblDailyCloseDesc.TabIndex = 0
        Me.lblDailyCloseDesc.Text = "All Tickets and Cash Drawers must be closed before Closing Batch and Daily."
        Me.lblDailyCloseDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlMoreTickets
        '
        Me.pnlMoreTickets.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMoreTickets.Controls.Add(Me.btnLessTickets)
        Me.pnlMoreTickets.Controls.Add(Me.btnMoreTickets)
        Me.pnlMoreTickets.Location = New System.Drawing.Point(697, 25)
        Me.pnlMoreTickets.Name = "pnlMoreTickets"
        Me.pnlMoreTickets.Size = New System.Drawing.Size(239, 63)
        Me.pnlMoreTickets.TabIndex = 7
        Me.pnlMoreTickets.Visible = False
        '
        'btnLessTickets
        '
        Me.btnLessTickets.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnLessTickets.Location = New System.Drawing.Point(12, 8)
        Me.btnLessTickets.Name = "btnLessTickets"
        Me.btnLessTickets.Size = New System.Drawing.Size(104, 45)
        Me.btnLessTickets.TabIndex = 1
        Me.btnLessTickets.Text = "Reset"
        Me.btnLessTickets.UseVisualStyleBackColor = False
        '
        'btnMoreTickets
        '
        Me.btnMoreTickets.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnMoreTickets.Location = New System.Drawing.Point(122, 8)
        Me.btnMoreTickets.Name = "btnMoreTickets"
        Me.btnMoreTickets.Size = New System.Drawing.Size(104, 45)
        Me.btnMoreTickets.TabIndex = 0
        Me.btnMoreTickets.Text = "More"
        Me.btnMoreTickets.UseVisualStyleBackColor = False
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblVersion.Location = New System.Drawing.Point(13, 762)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(41, 13)
        Me.lblVersion.TabIndex = 8
        Me.lblVersion.Text = "version"
        '
        'Manager_Main_UC
        '
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.pnlMoreTickets)
        Me.Controls.Add(Me.pnlCDInfo)
        Me.Controls.Add(Me.lblCurrentServer)
        Me.Controls.Add(Me.lblActiingManager)
        Me.Controls.Add(Me.pnlMgrMainCategories)
        Me.Controls.Add(Me.pnlMainManagerLarger)
        Me.Controls.Add(Me.pnlClosingDailyDirections)
        Me.ForeColor = System.Drawing.Color.White
        Me.Name = "Manager_Main_UC"
        Me.Size = New System.Drawing.Size(1024, 786)
        Me.pnlMgrMainCategories.ResumeLayout(False)
        Me.pnlManagerSelection.ResumeLayout(False)
        Me.pnlManagerSubSelecetion.ResumeLayout(False)
        Me.pnlMainManager.ResumeLayout(False)
        Me.pnlMainManagerLarger.ResumeLayout(False)
        Me.pnlCDInfo.ResumeLayout(False)
        Me.pnlClosingDailyDirections.ResumeLayout(False)
        Me.pnlMoreTickets.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub InitializeOther()

        Me.pnlManagerSubSelecetion.Location = New Point(8, 24)
        '
        'mgrLargeNumberPad
        '
        Me.mgrLargeNumberPad = New NumberPad
        Me.mgrLargeNumberPad.Location = New System.Drawing.Point(360, 140)
        Me.mgrLargeNumberPad.Name = "mgrLargeNumberPad"
        Me.mgrLargeNumberPad.MakeBuutonsWhite()
        Me.pnlMainManager.Controls.Add(Me.mgrLargeNumberPad)
        Me.lblVersion.Text = companyInfo.VersionNumber
        '    Me.lblVersion.Text = ""

        If usernameEnterOnLogin = False Then
            Me.pnlMgrMainCategories.Enabled = False
            Me.lblMgrDirectionsNumberPad.Text = currentServer.FullName
            If currentTerminal.TermMethod = "Quick" Or companyInfo.usingBartenderMethod = True Then
                Me.lblMgrDirectionsNumberPad2.Text = "Enter your Username and Password"
            Else
                Me.lblMgrDirectionsNumberPad2.Text = "Hit Enter"
                Me.lblMgrDirectionsNumberPad3.Text = "If you are not " & currentServer.FullName & " enter your Username and Passcode, then Enter."
            End If
        Else
            GenerateOrderTables.AssignManagementAuthorization(actingManager)
            DisplayLabelsBasedOnAuth()
        End If

        '      info = New DataSet_Builder.Info2_UC("")
        '     info.Location = New Point((pnlMainManager.Width - info.Width) / 2, (pnlMainManager.Height - info.Height) / 2)
        '    Me.pnlMainManager.Controls.Add(info)
        '   info.Visible = False

    End Sub

    Private Sub MgrPasscodeEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mgrLargeNumberPad.NumberEntered
        Dim emp As Employee

        If Me.mgrLargeNumberPad.NumberString.Length = 8 Then
            emp = DetermineSecondEmployeeAuthorization(Me.mgrLargeNumberPad.NumberString)
            If Not emp Is Nothing Then
                GenerateOrderTables.AssignManagementAuthorization(emp)
                DisplayLabelsBasedOnAuth()
            End If
        Else
            MsgBox("Please Combine Your EmployeeID as Passcode then Press Enter")

        End If
    End Sub

    Private Sub ActingManagerIsAccepted() Handles mgrLargeNumberPad.AcceptManager
        '   this is when the same employee is overriding system

        If currentTerminal.TermMethod = "Quick" Or companyInfo.usingBartenderMethod = True Then
            countNumberOfAttemptsToLeave += 1
            If countNumberOfAttemptsToLeave > 2 Then
                RaiseEvent DisposeManager()
            End If
            Exit Sub
        End If

        actingManager = currentServer
        GenerateOrderTables.AssignManagementAuthorization(actingManager)
        DisplayLabelsBasedOnAuth()

    End Sub

    Private Sub DisplayLabelsBasedOnAuth()

        '   make this according to auth
        Me.pnlMgrMainCategories.Enabled = True
        Me.lblMgrDirectionsNumberPad.Visible = False
        Me.lblMgrDirectionsNumberPad2.Visible = False
        Me.lblMgrDirectionsNumberPad3.Visible = False
        Me.mgrLargeNumberPad.Visible = False

        Me.lblActiingManager.Text = "Manager:   " & actingManager.FullName

    End Sub
    Private Sub AssignManagementAuthorization222(ByRef empAuth As Employee)
        ' now in geneateOrderTables
        '   employeeAuthorization = New ManagementAuthorization

        employeeAuthorization.FullName = empAuth.FullName

        If empAuth.OperationMgmtAll = True Then
            employeeAuthorization.OperationLevel = 2
        ElseIf empAuth.OperationMgmtLimited = True Then
            employeeAuthorization.OperationLevel = 1
        Else
            employeeAuthorization.OperationLevel = 0
        End If

        If empAuth.EmployeeMgmtAll = True Then
            employeeAuthorization.EmployeeLevel = 2
        ElseIf empAuth.EmployeeMgmtLimited = True Then
            employeeAuthorization.EmployeeLevel = 1
        Else
            employeeAuthorization.EmployeeLevel = 0
        End If

        If empAuth.ReportMgmtAll = True Then
            employeeAuthorization.ReportLevel = 2
        ElseIf empAuth.ReportMgmtLimited = True Then
            employeeAuthorization.ReportLevel = 1
        Else
            employeeAuthorization.ReportLevel = 0
        End If

        If empAuth.SystemMgmtAll = True Then
            employeeAuthorization.SystemLevel = 2
        ElseIf empAuth.SystemMgmtLimited = True Then
            employeeAuthorization.SystemLevel = 1
        Else
            employeeAuthorization.SystemLevel = 0
        End If

        '   do not need below anymore
        employeeAuthorization.OperationAll = empAuth.OperationMgmtAll
        employeeAuthorization.OperationLimited = empAuth.OperationMgmtLimited
        employeeAuthorization.EmployeeAll = empAuth.EmployeeMgmtAll
        employeeAuthorization.EmployeeLimited = empAuth.EmployeeMgmtLimited
        employeeAuthorization.ReportAll = empAuth.ReportMgmtAll
        employeeAuthorization.ReportLimited = empAuth.ReportMgmtLimited
        employeeAuthorization.SystemAll = empAuth.SystemMgmtAll
        employeeAuthorization.SystemLimited = empAuth.SystemMgmtLimited


        '   make this according to auth
        Me.pnlMgrMainCategories.Enabled = True
        Me.lblMgrDirectionsNumberPad.Visible = False
        Me.lblMgrDirectionsNumberPad2.Visible = False
        Me.lblMgrDirectionsNumberPad3.Visible = False
        Me.mgrLargeNumberPad.Visible = False

        Me.lblActiingManager.Text = "Manager:   " & actingManager.FullName

    End Sub

    Private Sub ResetAllFlags()

        closingDailyCode = 0
        Me.OperationFlag = False
        Me.EmployeeFlag = False
        Me.ReopenFlag = False
        Me.MenuFlag = False
        Me.SystemFlag = False
        Me.DailysFlag = False
        Me.InventoryFlag = False
        pnlCDInfo.Visible = False
        Me.pnlClosingDailyDirections.Visible = False
        Me.pnlMoreTickets.Visible = False

        Me.btnOperations.BackColor = c10
        '      Me.btnOperations.ForeColor = c2
        Me.btnEmployees.BackColor = c10
        '     Me.btnEmployees.ForeColor = c2
        Me.btnReports.BackColor = c10
        '       Me.btnReports.ForeColor = c2
        Me.btnMenu.BackColor = c10

        Me.btnSystem.BackColor = c10
        '      Me.btnSystem.ForeColor = c2
        Me.btnDailys.BackColor = c10
        '     Me.btnDailys.ForeColor = c2
        Me.btnInventory.BackColor = c10
        '    Me.btnInventory.ForeColor = c2

    End Sub



    Private Sub btnInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInventory.Click

        '        If typeProgram = "Online_Demo" Then
        '       'Inventory
        '      CreateRawCategoryTableStructure(dtRawCategory)
        '     CreateRawMatTableStructure(dtRawMat)
        '    CreateRawDeliveryTableStructure(dtRawDelivery)
        '   CreateRawDeliveryTableStructure(dtRawCycley)
        '  dsInventory.ReadXml("InventoryData.xml", XmlReadMode.ReadSchema)
        ' End If

        ResetAllFlags()
        Me.btnInventory.BackColor = Color.FromArgb(59, 96, 141)
        '    Me.btnInventory.ForeColor = c3
        Me.InventoryFlag = True

        Me.pnlMainManager.Controls.Clear()

        If employeeAuthorization.OperationLevel > 0 Then
            DisplayInventoryChoices()
        Else
            MsgBox(actingManager.FullName & " is not authorized for System changes.")
            Exit Sub
        End If

    End Sub

    Private Sub DisplayInventoryChoices()
        Me.pnlManagerSelection.Visible = False
        Me.pnlManagerSubSelecetion.Visible = True

        RemoveTextFromSubButtons()
        Me.SubButton1.Text = "Delivery"
        Me.SubButton2.Text = "Waste"
        Me.SubButton3.Text = ""
        Me.SubButton4.Text = "Physical Inventory"
        InventoryTable = ""
        '      DisplayConnectionButton()

    End Sub

    Private Sub btnOperations_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOperations.Click

        ResetAllFlags()
        Me.btnOperations.BackColor = Color.FromArgb(59, 96, 141)
        '   Me.btnOperations.ForeColor = c3
        Me.OperationFlag = True

        Me.pnlMainManager.Controls.Clear()
        Me.pnlMoreTickets.Visible = True

        If employeeAuthorization.OperationLevel > 0 Then
            If Not typeProgram = "Online_Demo" Then
                PopulateServerCollection(todaysFloorPersonnel)
            End If
            activeCollection = todaysFloorPersonnel
            DisplayFloorPersonnel() '(todaysFloorPersonnel)
        Else
            MsgBox(actingManager.FullName & " is not authorized for Operational changes.")
            Exit Sub
        End If

        Me.pnlManagerSelection.Visible = False
        Me.pnlManagerSubSelecetion.Visible = True

        RemoveTextFromSubButtons()
        Me.SubButton1.Text = "New Table"
        Me.SubButton2.Text = "New Tab"
        Me.SubButton3.Text = "Cash Out"
        Me.SubButton4.Text = "Credit Return"

    End Sub

    Private Function DetermineIfOpenTables(ByVal selectedDailyCode As Int64) As Boolean

        GenerateOrderTables.PopulateTabsAndTablesEveryone(empActive, selectedDailyCode, True, False, todaysFloorPersonnel)


        '      GenerateOrderTables.CreateDataViews()
        '
        '       If dvAvailTables.Count + dvAvailTabs.Count > 0 Then
        '      Return False
        '     Else
        '        Return True
        '   End If
        '
        Exit Function
        '********************************************************************
        '   ***222   we are no longer using "OpenTables" here
        '   we may be using them on Opening POS

        Dim oRow As DataRow

        If mainServerConnected = True Then
            Try
                dsOrder.Tables("OpenTables").Rows.Clear()
                dsOrder.Tables("OpenTabs").Rows.Clear()

                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                '          sql.SqlSelectCommandOpenTables.Parameters("@CompanyID").Value = CompanyID
                '         sql222.SqlSelectCommandOpenTables.Parameters("@LocationID").Value = companyInfo.LocationID
                '        sql222.SqlSelectCommandOpenTables.Parameters("@DailyCode").Value = selectedDailyCode
                '       sql222.SqlDataAdapterOpenTables.Fill(dsOrder.Tables("OpenTables"))

                '           sql.SqlSelectCommandOpenTabs.Parameters("@CompanyID").Value = CompanyID
                '         sql222.SqlSelectCommandOpenTabs.Parameters("@LocationID").Value = companyInfo.LocationID
                '        sql222.SqlSelectCommandOpenTabs.Parameters("@DailyCode").Value = selectedDailyCode
                '       sql222.SqlDataAdapterOpenTabs.Fill(dsOrder.Tables("OpenTabs"))
                sql.cn.Close()

                '     CreateAvailDataViews222()
                '     If dsOrder.Tables("OpenTables").Rows.Count + dsOrder.Tables("OpenTabs").Rows.Count > 0 Then
                If dvAvailTables.Count + dvAvailTabs.Count + dvQuickTickets.Count > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                CloseConnection()
                ServerJustWentDown()
                MsgBox("Server is Not Connected. You can not close Daily until Server is connected. You can close Daily another day.")
                Return True
            End Try
        Else
            MsgBox("You can not close Daily until Server is connected. You can close Daily another day.")
            Return True
        End If

    End Function

    Private Sub DisplayFloorPersonnel() 'ByRef activeCollection As EmployeeCollection)

        Dim emp As Employee
        Dim w As Single
        Dim h As Single
        Dim x As Integer = buttonSpace
        Dim y As Integer = buttonSpace
        Dim index As Integer
        Dim counterIndex As Integer = 1
        mainPanelCounterIndex = 0

        w = (Me.pnlMainManager.Width - (6 * buttonSpace)) / 5
        h = (Me.pnlMainManager.Height - (11 * buttonSpace)) / 10


        For Each emp In activeCollection

            If Not emp.EmployeeID = 6986 Or actingManager.EmployeeID = 6986 Then
                floorPersonnel = New KitchenButton(emp.FullName, w, h, c10, c2)
                floorPersonnel.Location = New Point(x, y)
                floorPersonnel.ID = emp.EmployeeID
                floorPersonnel.ButtonIndex = index
                AddHandler floorPersonnel.Click, AddressOf FloorPerson_Click

                Me.pnlMainManager.Controls.Add(Me.floorPersonnel)

                If counterIndex = 10 Then
                    y = buttonSpace
                    x += w + buttonSpace
                    counterIndex = 0    'must restart at zero b/c we add 1 right away
                Else
                    y += h + buttonSpace
                End If
                index += 1
                counterIndex += 1
            End If

        Next

        floorPersonnel = Nothing

        If Not activeCollection Is allFloorPersonnel Then

            floorPersonnel = New KitchenButton("All Floor", w, h, c10, c2)
            floorPersonnel.Location = New Point(x, y)
            floorPersonnel.ID = -1111
            floorPersonnel.ButtonIndex = index
            AddHandler floorPersonnel.Click, AddressOf FloorPerson_Click

            Me.pnlMainManager.Controls.Add(Me.floorPersonnel)
        End If

    End Sub


    Private Sub FloorPerson_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles floorPersonnel.Click
        Dim objButton As New KitchenButton("ForTesting", 0, 0, c3, c2)
        If Not sender.GetType Is objButton.GetType Then Exit Sub
        Dim emp As Employee

        objButton = CType(sender, KitchenButton)
        '     empActive = objButton.ID

        If objButton.ID = -1111 Then
            'displaying all personnel
            Me.pnlMainManager.Controls.Clear()
            activeCollection = allFloorPersonnel
            DisplayFloorPersonnel()
            Exit Sub
        End If

        For Each emp In activeCollection    'todaysFloorPersonnel
            If emp.EmployeeID = objButton.ID Then   '    empActive Then
                '          currentServer = emp
                empActive = emp
                lblCurrentServer.Text = "Server: " & empActive.FullName
                Exit For
            End If
        Next

        Me.pnlMainManager.Controls.Clear()

        If empActive Is Nothing Then
            empActive = actingManager
        End If
        currentServer = empActive


        If Me.OperationFlag = True Then

            GenerateOrderTables.PopulateTabsAndTables(empActive, currentTerminal.CurrentDailyCode, False, False, Nothing)
            CreateDataViews(empActive.EmployeeID, True)
            '       PopulateOpenTabsAndTables()
            DisplayOpenTabsAndTables()
        ElseIf Me.EmployeeFlag = True Then

            If employeeAuthorization.EmployeeLevel > 1 Then
                ClockAdjustment_Click(sender, e)
                ' we are currently go straight to emp Clock Adj
                '   we may change to below if more choices become avail 
                '            DisplayEmployeeAdjustmentOptions()
            End If
        End If

    End Sub

    Friend Sub DisplayOpenTabsAndTables()

        Dim vRow As DataRowView
        Dim index As Integer = 1
        Dim counterIndex As Integer = 1

        Dim x As Integer = buttonSpace
        Dim y As Integer = buttonSpace
        Dim w As Single
        Dim h As Single

        w = (Me.pnlMainManager.Width - (6 * buttonSpace)) / 5
        h = (Me.pnlMainManager.Height - (11 * buttonSpace)) / 10

        displayingOpenOrClose = "Open"
        '444      Me.pnlMoreTickets.Visible = True

        For Each vRow In dvAvailTables

            If index > mainPanelCounterIndex Then
                CreateOpenTabsAndTables(False, vRow, w, h, x, y, vRow("TableNumber"), False)

                If counterIndex = 10 Then
                    y = buttonSpace
                    x += w + buttonSpace
                    counterIndex = 0    'must restart at zero b/c we add 1 right away
                Else
                    y += h + buttonSpace
                End If
                counterIndex += 1
            End If

            index += 1
        Next


        For Each vRow In dvAvailTabs
            If index > mainPanelCounterIndex Then
                CreateOpenTabsAndTables(True, vRow, w, h, x, y, vRow("TabName"), False)

                If counterIndex = 10 Then
                    y = buttonSpace
                    x += w + buttonSpace
                    counterIndex = 0    'must restart at zero b/c we add 1 right away
                Else
                    y += h + buttonSpace
                End If
                counterIndex += 1
            End If

            index += 1
        Next

        For Each vRow In dvTransferTables
            If index > mainPanelCounterIndex Then
                CreateOpenTabsAndTables(False, vRow, w, h, x, y, vRow("TableNumber"), False)

                If counterIndex = 10 Then
                    y = buttonSpace
                    x += w + buttonSpace
                    counterIndex = 0    'must restart at zero b/c we add 1 right away
                Else
                    y += h + buttonSpace
                End If
                counterIndex += 1
            End If

            index += 1
        Next

        For Each vRow In dvTransferTabs
            If index > mainPanelCounterIndex Then
                CreateOpenTabsAndTables(True, vRow, w, h, x, y, vRow("TabName"), False)

                If counterIndex = 10 Then
                    y = buttonSpace
                    x += w + buttonSpace
                    counterIndex = 0    'must restart at zero b/c we add 1 right away
                Else
                    y += h + buttonSpace
                End If
                counterIndex += 1
            End If

            index += 1
        Next

        If Me.DailysFlag = True Then
            Dim tabDesc As String
            'we are closeing daily
            'only using CreateClosedTabsAndTablesButton because it formats Text
            For Each vRow In dvQuickTickets

                tabDesc = "Tkt# " & vRow("TicketNumber").ToString
                CreateOpenTabsAndTables(True, vRow, w, h, x, y, tabDesc, False)
                '   CreateClosedTabsAndTablesButton(True, vRow, w, h, x, y, Nothing, vRow("LastStatusTime"))

                If counterIndex = 10 Then
                    y = buttonSpace
                    x += w + buttonSpace
                    counterIndex = 0    'must restart at zero b/c we add 1 right away
                Else
                    y += h + buttonSpace
                End If
                index += 1
                counterIndex += 1
            Next
        Else
            'this is for individuals
            ' so we just want Beth's Tabs to show
            If Not currentTerminal.TermMethod = "Quick" Then
                If dvQuickTickets.Count > 0 Then
                    vRow = dvQuickTickets(dvQuickTickets.Count - 1)

                    '    CreateOpenTabsAndTables(True, vRow, w, h, x, y, vRow("TicketNumber"))
                    CreateOpenTabsAndTables(True, vRow, w, h, x, y, vRow("TabName"), True)

                    If counterIndex = 10 Then
                        y = buttonSpace
                        x += w + buttonSpace
                        counterIndex = 0    'must restart at zero b/c we add 1 right away
                    Else
                        y += h + buttonSpace
                    End If
                    index += 1
                    counterIndex += 1
                End If
            End If

        End If

        If Me.DailysFlag = False Then
            btnClosedTable = New Button
            With btnClosedTable
                .Text = "Closed Checks"
                .Size = New Size(w, h)
                .Location = New Point(x, y)
                .BackColor = c7
                .ForeColor = c3
                AddHandler btnClosedTable.Click, AddressOf ClosedTables_Click
            End With
            Me.pnlMainManager.Controls.Add(btnClosedTable)
        End If

    End Sub

    Private Sub DisplayGroupTabs()

        Dim vRow As DataRowView
        Dim index As Integer
        Dim counterIndex As Integer = 1
        '     Dim serverTableDesc As String
        Dim lsTime As Date
        Dim tabDesc As String

        Dim x As Integer = buttonSpace
        Dim y As Integer = buttonSpace
        Dim w As Single
        Dim h As Single

        w = (Me.pnlMainManager.Width - (6 * buttonSpace)) / 5
        h = (Me.pnlMainManager.Height - (11 * buttonSpace)) / 10

        Me.pnlMainManager.Controls.Clear()
        displayingOpenOrClose = "Beths"

        For Each vRow In dvQuickTickets
            If index > mainPanelCounterIndex Then
                '      serverTableDesc = "" 'vRow("NickName") & " " & vRow("LastName")
                If vRow("ClosedSubTotal") Is DBNull.Value Then
                    tabDesc = "Tkt# " & vRow("TicketNumber").ToString

                    CreateOpenTabsAndTables(True, vRow, w, h, x, y, tabDesc, False)
                Else
                    lsTime = (vRow("LastStatusTime"))
                    CreateClosedTabsAndTablesButton(True, vRow, w, h, x, y, Nothing, lsTime)

                End If

                If counterIndex = 10 Then
                    y = buttonSpace
                    x += w + buttonSpace
                    counterIndex = 0    'must restart at zero b/c we add 1 right away
                Else
                    y += h + buttonSpace
                End If
                counterIndex += 1
            End If

            index += 1
        Next

    End Sub

    Private Sub CreateOpenTabsAndTables(ByVal isTabNotTable As Boolean, ByVal vRowUsed As DataRowView, ByVal w As Single, ByVal h As Single, ByVal x As Single, ByVal y As Single, ByVal TabTableDesc As String, ByVal isGroup As Boolean)
        Dim tn As Integer
        Dim tab As Int64
        Dim btnTabsAndTables As DataSet_Builder.AvailTableUserControl
        Dim altMethodUse As String = ""

        If Not dvTerminalsUseOrder(0)("MethodUse") = vRowUsed("MethodUse") Then
            altMethodUse = vRowUsed("MethodUse")
        Else
            altMethodUse = ""
        End If

        If isTabNotTable = True Then
            tab = vRowUsed("TabID")
        Else
            tn = vRowUsed("TableNumber")
        End If

        '       btnTabsAndTables = New DataSet_Builder.AvailTableUserControl(isTabNotTable, tn, tab, vRowUsed("TabName"), vRowUsed("TicketNumber"), Nothing, Nothing, Nothing, Nothing, Nothing, vRowUsed("ItemsOnHold"))
        btnTabsAndTables = New DataSet_Builder.AvailTableUserControl(isTabNotTable, tn, tab, TabTableDesc, vRowUsed("TicketNumber"), Nothing, Nothing, Nothing, Nothing, Nothing, vRowUsed("ItemsOnHold"), currentTerminal.TermMethod, altMethodUse)

        With btnTabsAndTables
            'this does not change test
            If isTabNotTable = True Then
                .Text = TabTableDesc 'vRowUsed("TabName")
                .TabID = tab
            Else
                .Text = TabTableDesc 'vRowUsed("TableNumber")
                .TableNumber = tn
            End If
            .SatTime = vRowUsed("ExperienceDate")
            .ExperienceNumber = vRowUsed("ExperienceNumber")
            .NumberOfChecks = vRowUsed("NumberOfChecks")
            .NumberOfCustomers = vRowUsed("NumberOfCustomers")
            .CurrentMenu = vRowUsed("MenuID")
            .EmpID = vRowUsed("EmployeeID")
            .Size = New Size(w, h)
            .Location = New Point(x, y)
            .BackColor = c7
            .ForeColor = c3
            .IsGroup = isGroup
            AddHandler btnTabsAndTables.TableClicked, AddressOf OpenTabsAndTables_Selected
        End With

        Me.pnlMainManager.Controls.Add(btnTabsAndTables)


    End Sub



    Private Sub btnMoreTickets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoreTickets.Click
        Me.pnlMainManager.Controls.Clear()

        If displayingOpenOrClose = "Open" Then
            mainPanelCounterIndex += 10
            DisplayOpenTabsAndTables()
        ElseIf displayingOpenOrClose = "Beths" Then
            mainPanelCounterIndex += 10
            DisplayGroupTabs()
        ElseIf displayingOpenOrClose = "Closed" Then
            skipClosedIndex += 10 '40
            DisplayClosedTabsAndTables()
        End If

    End Sub

    Private Sub btnLessTickets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLessTickets.Click
        skipClosedIndex = 0
        mainPanelCounterIndex = 0
        Me.pnlMainManager.Controls.Clear()
        If displayingOpenOrClose = "Open" Then
            DisplayOpenTabsAndTables()
        ElseIf displayingOpenOrClose = "Beths" Then
            DisplayGroupTabs()
        ElseIf displayingOpenOrClose = "Closed" Then
            DisplayClosedTabsAndTables()
        End If

    End Sub
    Private Sub DisplayClosedTabsAndTables()

        Dim vRow As DataRowView
        Dim w As Single
        Dim h As Single
        Dim x As Integer = buttonSpace
        Dim y As Integer = buttonSpace
        Dim index As Integer = 1
        Dim counterIndex As Integer = 1
        '     Dim serverTableDesc As String
        Dim lsTime As Date

        Dim timeAtTable As TimeSpan

        w = (Me.pnlMainManager.Width - (6 * buttonSpace)) / 5
        h = (Me.pnlMainManager.Height - (11 * buttonSpace)) / 10

        For Each vRow In dvClosedTables

            If index > skipClosedIndex Then 'mainPanelCounterIndex  Then
                '        serverTableDesc = vRow("NickName") & " " & vRow("LastName")
                lsTime = (vRow("LastStatusTime"))
                '          timeAtTable = DetermineTimeSpan(vRow("ExperienceDate"))
                CreateClosedTabsAndTablesButton(False, vRow, w, h, x, y, Nothing, lsTime) 'serverTableDesc, lsTime)

                If counterIndex = 10 Then
                    y = buttonSpace
                    x += w + buttonSpace
                    counterIndex = 0    'must restart at zero b/c we add 1 right away
                Else
                    y += h + buttonSpace
                End If
                counterIndex += 1
            End If

            index += 1
        Next


        For Each vRow In dvClosedTabs
            If index > skipClosedIndex Then ' mainPanelCounterIndex Then 
                lsTime = (vRow("LastStatusTime"))

                CreateClosedTabsAndTablesButton(True, vRow, w, h, x, y, Nothing, lsTime)

                If counterIndex = 10 Then
                    y = buttonSpace
                    x += w + buttonSpace
                    counterIndex = 0    'must restart at zero b/c we add 1 right away
                Else
                    y += h + buttonSpace
                End If
                counterIndex += 1
            End If

            index += 1
        Next


    End Sub

    Private Sub CreateClosedTabsAndTablesButton(ByVal isTabNotTable As Boolean, ByVal vRowUsed As DataRowView, ByVal w As Single, ByVal h As Single, ByVal x As Single, ByVal y As Single, ByVal serverTableDesc As String, ByVal lsTime As Date)
        Dim tn As Integer
        Dim tab As Int64
        Dim btnClosedTabsAndTables As DataSet_Builder.AvailTableUserControl
        Dim priceString As String = ""
        Dim nameString As String
        Dim altMethodUse As String = ""

        If Not dvTerminalsUseOrder(0)("MethodUse") = vRowUsed("MethodUse") Then
            altMethodUse = vRowUsed("MethodUse")
        Else
            altMethodUse = ""
        End If

        If isTabNotTable = True Then
            tab = vRowUsed("TabID")
            If Not vRowUsed("TicketNumber") = 0 Then
                nameString = "Clsd " & vRowUsed("TicketNumber").ToString
            Else
                nameString = vRowUsed("TabName").ToString
            End If
        Else
            tn = vRowUsed("TableNumber")
            nameString = vRowUsed("TabName").ToString
        End If

        If Not vRowUsed("ClosedSubTotal") Is DBNull.Value Then
            priceString = Format(vRowUsed("ClosedSubTotal"), "$ ##,###.00")
        End If

        Try
            btnClosedTabsAndTables = New DataSet_Builder.AvailTableUserControl(isTabNotTable, tn, tab, nameString, vRowUsed("TicketNumber"), vRowUsed("LastStatus"), priceString, Nothing, Nothing, lsTime, 0, currentTerminal.TermMethod, altMethodUse)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        With btnClosedTabsAndTables
            If isTabNotTable = True Then
                .Text = vRowUsed("TabName")
                .TabID = tab
            Else
                .Text = vRowUsed("TableNumber")
                .TableNumber = tn
            End If
            .SatTime = vRowUsed("ExperienceDate")
            .ExperienceNumber = vRowUsed("ExperienceNumber")
            .NumberOfChecks = vRowUsed("NumberOfChecks")
            .NumberOfCustomers = vRowUsed("NumberOfCustomers")
            .CurrentMenu = vRowUsed("MenuID")
            .EmpID = vRowUsed("EmployeeID")
            .Size = New Size(w, h)
            .Location = New Point(x, y)
            .BackColor = c7
            .ForeColor = c3
            AddHandler btnClosedTabsAndTables.TableClicked, AddressOf ClosedTabsAndTables_Selected
        End With

        Me.pnlMainManager.Controls.Add(btnClosedTabsAndTables)

    End Sub


    Private Sub OpenTabsAndTables_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlMainManager.Click

        Dim objButton As New DataSet_Builder.AvailTableUserControl
        If Not sender.GetType Is objButton.GetType Then Exit Sub

        objButton = CType(sender, DataSet_Builder.AvailTableUserControl)

        '        If objButton.TabID = -888 Then
        If objButton.IsGroup = True Then
            'this is for Tab Group
            DisplayGroupTabs()
            Exit Sub
        Else

            RaiseEvent OpenOrderAdjustment(sender, e, closingDailyCode)
        End If

    End Sub

    Private Sub ClosedTables_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles btnClosedTable.Click

        displayingOpenOrClose = "Closed"
        '   ***
        '   we will change and allow to creopen a check if down
        '   we just need to make a ClosedTablesTerminal data
        Me.pnlMainManager.Controls.Clear()
        Try
            '         ClearClosedTabsAndTables()
            '     PopulateClosedTabsAndTables(currentTerminal.currentDailyCode)
            '444        Me.pnlMoreTickets.Visible = True
            CreateClosedDataViews()
            '      CreateAvailDataViews()
            DisplayClosedTabsAndTables()
        Catch ex As Exception
            CloseConnection()
            If Err.Number = "5" Then
                ServerJustWentDown()
            End If
            MsgBox("You can not reopen a check. The Server may be disconnected.")
        End Try

    End Sub

    Private Sub ClosedTabsAndTables_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlMainManager.Click

        Dim objButton As New DataSet_Builder.AvailTableUserControl
        If Not sender.GetType Is objButton.GetType Then Exit Sub

        objButton = CType(sender, DataSet_Builder.AvailTableUserControl)

        _reopenIndex = objButton.ExperienceNumber
        If objButton.CurrentStatus.Length > 0 Then    'length > 0 means closed
            _reopenFlag = True
        End If

        RaiseEvent OpenOrderAdjustment(sender, e, closingDailyCode)

    End Sub

    Friend Sub btnEmployees_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmployees.Click

        If typeProgram = "Online_Demo" Then
            DemoThisNotAvail()
            Exit Sub
        End If

        ResetAllFlags()
        Me.btnEmployees.BackColor = Color.FromArgb(59, 96, 141)
        '    Me.btnEmployees.ForeColor = c3
        Me.EmployeeFlag = True


        Me.pnlMainManager.Controls.Clear()
        If employeeAuthorization.EmployeeLevel > 0 Then
            DisplayLoggedInEmployees()
            PopulateLoggedInEmployees(False)
            Me.grdEmpClockIn.DataSource = dsEmployee.Tables("LoggedInEmployees")
            lblNumClockedIn.Text = dsEmployee.Tables("LoggedInEmployees").Rows.Count

            activeCollection = AllEmployees
            DisplayFloorPersonnel() '  (AllEmployees)
        Else
            MsgBox(actingManager.FullName & " is not authorized for Employee changes.")
            Exit Sub
        End If


    End Sub

    Private Sub DisplayLoggedInEmployees()

        'this is taken from Opening Screen
        'we need to just have 1 dashboard with all this

        Me.grpEmployeeClockIn = New System.Windows.Forms.GroupBox
        Me.lblNumClockedIn = New System.Windows.Forms.Label
        Me.grdEmpClockIn = New System.Windows.Forms.DataGrid
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn

        '
        'grpEmployeeClockIn
        '
        Me.grpEmployeeClockIn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grpEmployeeClockIn.Controls.Add(Me.lblNumClockedIn)
        Me.grpEmployeeClockIn.Controls.Add(Me.grdEmpClockIn)
        Me.grpEmployeeClockIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpEmployeeClockIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpEmployeeClockIn.ForeColor = System.Drawing.Color.MediumBlue
        Me.grpEmployeeClockIn.Location = New System.Drawing.Point(526, 16)
        Me.grpEmployeeClockIn.Name = "grpEmployeeClockIn"
        Me.grpEmployeeClockIn.Size = New System.Drawing.Size(254, 178)
        Me.grpEmployeeClockIn.TabIndex = 6
        Me.grpEmployeeClockIn.TabStop = False
        Me.grpEmployeeClockIn.Text = "Employees Clocked-In"
        '
        'grdEmpClockIn
        '
        Me.grdEmpClockIn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grdEmpClockIn.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.grdEmpClockIn.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdEmpClockIn.CaptionText = "           Employees"
        Me.grdEmpClockIn.CaptionVisible = False
        Me.grdEmpClockIn.ColumnHeadersVisible = False
        Me.grdEmpClockIn.DataMember = ""
        Me.grdEmpClockIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdEmpClockIn.GridLineColor = System.Drawing.Color.WhiteSmoke
        Me.grdEmpClockIn.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdEmpClockIn.Location = New System.Drawing.Point(6, 41)
        Me.grdEmpClockIn.Name = "grdEmpClockIn"
        Me.grdEmpClockIn.RowHeadersVisible = False
        Me.grdEmpClockIn.Size = New System.Drawing.Size(242, 131)
        Me.grdEmpClockIn.TabIndex = 1
        Me.grdEmpClockIn.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'lblNumClockedIn
        '
        Me.lblNumClockedIn.Location = New System.Drawing.Point(171, 18)
        Me.lblNumClockedIn.Name = "lblNumClockedIn"
        Me.lblNumClockedIn.Size = New System.Drawing.Size(56, 20)
        Me.lblNumClockedIn.TabIndex = 2
        Me.lblNumClockedIn.Text = "#"
        Me.lblNumClockedIn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.grdEmpClockIn
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3})
        Me.DataGridTableStyle1.GridLineColor = System.Drawing.SystemColors.Window
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "LoggedInEmployees"
        Me.DataGridTableStyle1.ReadOnly = True
        Me.DataGridTableStyle1.RowHeadersVisible = False
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.MappingName = "FirstName"
        Me.DataGridTextBoxColumn1.NullText = " "
        Me.DataGridTextBoxColumn1.Width = 75
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.MappingName = "LastName"
        Me.DataGridTextBoxColumn2.NullText = " "
        Me.DataGridTextBoxColumn2.Width = 75
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn3.Format = "MM/dd"
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.MappingName = "LogInTime"
        Me.DataGridTextBoxColumn3.NullText = " "
        Me.DataGridTextBoxColumn3.Width = 50
        Me.pnlMainManager.Controls.Add(grpEmployeeClockIn)

    End Sub

    Private Sub DisplayEmployeeAdjustmentOptions222()

        ' we are currently go straight to emp Clock Adj
        '   we may change to below if more choices become avail
        Dim w As Single
        Dim h As Single
        Dim x As Integer = buttonSpace
        Dim y As Integer = buttonSpace
        Dim index As Integer
        Dim counterIndex As Integer = 1

        w = (Me.pnlMainManager.Width - (6 * buttonSpace)) / 5
        h = (Me.pnlMainManager.Height - (11 * buttonSpace)) / 10

        Me.mgrClockAdjustment = New Button
        Me.mgrClockAdjustment.Location = New Point(x, y)
        Me.mgrClockAdjustment.Size = New Size(w, h)
        Me.mgrClockAdjustment.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.mgrClockAdjustment.BackColor = c10
        Me.mgrClockAdjustment.ForeColor = c3
        Me.mgrClockAdjustment.Text = "Adjust Clock Time"

        y += h + buttonSpace

        Me.mgrTipAdjustment = New Button
        Me.mgrTipAdjustment.Location = New Point(x, y)
        Me.mgrTipAdjustment.Size = New Size(w, h)
        Me.mgrTipAdjustment.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mgrTipAdjustment.BackColor = c10
        Me.mgrTipAdjustment.ForeColor = c3
        Me.mgrTipAdjustment.Text = "Adjust Declared Tips"

        y += h + buttonSpace

        Me.mgrPayRateAdjustment = New Button
        Me.mgrPayRateAdjustment.Location = New Point(x, y)
        Me.mgrPayRateAdjustment.Size = New Size(w, h)
        Me.mgrPayRateAdjustment.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mgrPayRateAdjustment.BackColor = c10
        Me.mgrPayRateAdjustment.ForeColor = c3
        Me.mgrPayRateAdjustment.Text = "Adjust Pay Rate"

        y += h + buttonSpace


        Me.pnlMainManager.Controls.Add(Me.mgrClockAdjustment)
        Me.pnlMainManager.Controls.Add(Me.mgrTipAdjustment)
        Me.pnlMainManager.Controls.Add(Me.mgrPayRateAdjustment)


    End Sub

    Friend Sub ClockAdjustment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mgrClockAdjustment.Click
        '     MsgBox("Service Not Available")
        '    Exit Sub

        dsEmployee.Tables("LoggedInEmployees").Clear()
        Me.pnlMainManager.Controls.Clear()

        '   *** need to relook at this entire Sub

        '    ReformatManagerPanels()

        Dim yesterdaysDate As Date

        '   ********************
        '   do for the last pay period
        yesterdaysDate = Format(Today.AddDays(-14), "D")

        '   we want to display employees logged out for a particular time
        '   want to do by employee
        sql.SqlSelectCommandCLockedInByEmp.Parameters("@CompanyID").Value = companyInfo.CompanyID
        sql.SqlSelectCommandCLockedInByEmp.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandCLockedInByEmp.Parameters("@EmployeeID").Value = empActive.EmployeeID
        sql.SqlSelectCommandCLockedInByEmp.Parameters("@LogInTime").Value = yesterdaysDate

        Try
            '             AddAutoSalariedEmployeesToCollection()
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlCLockedInByEmp.Fill(dsEmployee.Tables("LoggedInEmployees"))
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)

            MsgBox("You can not adjust Clock Times when not connected to the server.")
        End Try


        employeeLog = New EmployeeLoggedInUserControl(True)
        employeeLog.Location = New Point((Me.pnlMainManager.Width - employeeLog.Width) / 2, (Me.pnlMainManager.Height - employeeLog.Height) / 2)
        Me.pnlMainManager.Controls.Add(employeeLog)

    End Sub

    Private Sub EndAdjustEmployeeClock(ByVal sender As Object, ByVal e As System.EventArgs) Handles employeeLog.ClosedEmployeeLog

        employeeLog.Dispose()
        '     usernameEnterOnLogin = True
        '     DisplayMainManager()
        '    btnEmployees_Click(sender, e)

    End Sub

    Private Sub TipAdjustment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mgrTipAdjustment.Click
        MsgBox("Service Not Available")
        Exit Sub

    End Sub
    Private Sub PayRateAdjustment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mgrPayRateAdjustment.Click
        MsgBox("Service Not Available")
        Exit Sub

    End Sub

    Private Sub ReformatManagerPanels()

        Me.pnlManagerSelection.Dispose()
        Me.pnlMainManager.Location = New Point(32, 56)
        Me.pnlMainManager.Size = New Size(712, 472)
        Me.Invalidate()


    End Sub

    Private Sub btnReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReports.Click

        If employeeAuthorization.ReportLevel > 0 Then
        Else
            MsgBox(actingManager.FullName & " is not authorized for Reports.")
            Exit Sub
        End If

        'this is wrong
        'reports should be built in Manager_Main
        'we should not dispose of Maanager_Main
        actingManager = Nothing
        empActive = Nothing
        RaiseEvent OpenReports()
        Me.Dispose()
        Exit Sub

        ResetAllFlags()
        Me.btnSystem.BackColor = Color.FromArgb(59, 96, 141)
        '   Me.btnSystem.ForeColor = c3
        Me.SystemFlag = True

        Me.pnlMainManager.Controls.Clear()

        If employeeAuthorization.SystemLevel > 0 Then
            DisplaySystemChoices()
        Else
            MsgBox(actingManager.FullName & " is not authorized for System changes.")
            Exit Sub
        End If





    End Sub


    '  Private Sub DisposingSelf(ByVal sender As Object, ByVal e As System.EventArgs) Handles employeeLog.ClosedEmployeeLog
    '     Me.pnlMainManager.Controls.Clear()
    '
    ' End Sub


    Private Sub btnMgrExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMgrExit.Click

        '   ExitManager()
        RaiseEvent DisposeManager()

    End Sub

    Private Sub btnMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMenu.Click
        ResetAllFlags()
        Me.btnMenu.BackColor = Color.FromArgb(59, 96, 141)
        '      Me.btnSystem.ForeColor = c3
        Me.MenuFlag = True

        Me.pnlMainManager.Controls.Clear()

        If employeeAuthorization.OperationLevel > 0 Then
            DisplayMenuChoices()
        Else
            MsgBox(actingManager.FullName & " is not authorized for Menu changes.")
            Exit Sub
        End If

    End Sub

    Private Sub DisplayMenuChoices()
        Me.pnlManagerSelection.Visible = False
        Me.pnlManagerSubSelecetion.Visible = True

        RemoveTextFromSubButtons()
        Me.SubButton1.Text = "ChangeMenu"
        Me.SubButton2.Text = "Switch Menu"
        Me.SubButton3.Text = "Reload Menu"
        '     Me.SubButton4.Text = ""

    End Sub

    Private Sub btnSystem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSystem.Click
        ResetAllFlags()
        Me.btnSystem.BackColor = Color.FromArgb(59, 96, 141)
        Me.SystemFlag = True

        Me.pnlMainManager.Controls.Clear()

        If employeeAuthorization.SystemLevel > 0 Then
            DisplaySystemChoices()
        Else
            MsgBox(actingManager.FullName & " is not authorized for System changes.")
            Exit Sub
        End If

    End Sub
    Private Sub DisplaySystemChoices()
        Me.pnlManagerSelection.Visible = False
        Me.pnlManagerSubSelecetion.Visible = True

        RemoveTextFromSubButtons()
        Me.SubButton1.Text = "Override Table Status"
        Me.SubButton2.Text = "Minimize" '"Switch Menu"
        Me.SubButton3.Text = "Activate Credit" '"Reload Menu"

        DisplayConnectionButton()

    End Sub

    Private Sub DisplayConnectionButton()
        If connectserver = "Phoenix\Phoenix" Then
            Me.SubButton4.Text = "Connection: DataCenter"   ' & sql.connectServer
        Else
            Me.SubButton4.Text = "Connection: Local"     ' & sql.connectServer
        End If
    End Sub

    Private Sub DisplayMenuChoices222()
        Dim oRow As DataRow
        Dim w As Single
        Dim h As Single
        Dim x As Integer = buttonSpace
        Dim y As Integer = buttonSpace
        Dim index As Integer
        Dim counterIndex As Integer = 1
        Dim menuChoice As KitchenButton

        w = (Me.pnlMainManager.Width - (6 * buttonSpace)) / 5
        h = (Me.pnlMainManager.Height - (11 * buttonSpace)) / 10

        If primaryMenuSelected = False Then
            Me.lblMainMgrInstructions.Text = "Choice Primary Menu"
        Else
            Me.lblMainMgrInstructions.Text = "Choice Secondary Menu"
        End If

        For Each oRow In ds.Tables("MenuChoice").Rows
            If primaryMenuSelected = False Then
                If oRow("MenuID") = currentTerminal.primaryMenuID Then
                    menuChoice = New KitchenButton(oRow("MenuName"), w, h, c9, c2)  'makes selected primary red
                Else
                    menuChoice = New KitchenButton(oRow("MenuName"), w, h, c10, c2)
                End If
            Else
                If oRow("MenuID") = currentTerminal.secondaryMenuID Then
                    menuChoice = New KitchenButton(oRow("MenuName"), w, h, c9, c2)  'makes selected secondary red
                Else
                    menuChoice = New KitchenButton(oRow("MenuName"), w, h, c10, c2)
                End If

            End If

            menuChoice.Location = New Point(x, y)
            menuChoice.ID = oRow("MenuID")
            menuChoice.ButtonIndex = index
            AddHandler menuChoice.Click, AddressOf MenuChoice_Click222

            Me.pnlMainManager.Controls.Add(menuChoice)

            If counterIndex = 10 Then
                y = buttonSpace
                x += w + buttonSpace
                counterIndex = 0    'must restart at zero b/c we add 1 right away
            Else
                y += h + buttonSpace
            End If
            index += 1
            counterIndex += 1
        Next

    End Sub

    Private Sub MenuChoice_Click222(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles menuChoice.Click

        Dim objButton As New KitchenButton("ForTesting", 0, 0, c3, c2)
        If Not sender.GetType Is objButton.GetType Then Exit Sub

        Dim newMenuChoice As Integer

        objButton = CType(sender, KitchenButton)
        newMenuChoice = objButton.ID

        If primaryMenuSelected = True Then
            If Not newMenuChoice = currentTerminal.secondaryMenuID Then
                currentTerminal.secondaryMenuID = newMenuChoice
            End If
            Me.pnlManagerSelection.Visible = True
            Me.pnlManagerSubSelecetion.Visible = False
            Me.pnlMainManager.Controls.Clear()
            Me.lblMainMgrInstructions.Text = ""

        Else
            primaryMenuSelected = True
            If Not newMenuChoice = currentTerminal.primaryMenuID Then
                currentTerminal.primaryMenuID = newMenuChoice
            End If
            Me.pnlMainManager.Controls.Clear()
            If ds.Tables("MenuChoice").Rows.Count > 1 Then
                DisplayMenuChoices222()        'now we will select secondary menu
            Else
                currentTerminal.secondaryMenuID = newMenuChoice
                Me.pnlManagerSelection.Visible = True
                Me.pnlManagerSubSelecetion.Visible = False
                Me.lblMainMgrInstructions.Text = ""
            End If
        End If

    End Sub

    Private Sub SubButtonBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubButtonBack.Click

        Me.pnlManagerSelection.Visible = True
        Me.pnlManagerSubSelecetion.Visible = False
        If Me.DailysFlag = True Then
            Me.pnlMainManagerLarger.Controls.Clear()
            '       Me.pnlMainManagerLarger.BackColor = Color.Black
            Me.pnlMainManagerLarger.Controls.Add(Me.pnlMainManager)
        End If
        If Me.InventoryFlag = True Then
            If Not Me.InventoryTable = "" Then
                SaveInventoryData()
                Me.InventoryTable = ""
            End If
        End If

        Me.pnlMainManager.Controls.Clear()
        Me.lblMainMgrInstructions.Text = ""
        ResetSubButtonColors()

        If availTableChangesMade = True Then
            UpdateAvailTablesData()
            availTableChangesMade = False
        End If

    End Sub

    Private Sub RemoveTextFromSubButtons()
        '    Me.SubButton1.Text = ""
        Me.SubButton2.Text = ""
        Me.SubButton3.Text = ""
        Me.SubButton4.Text = ""
        Me.subButton5.Text = ""

    End Sub

    Private Sub ResetSubButtonColors()

        Me.SubButton1.BackColor = Color.LightSlateGray
        Me.SubButton2.BackColor = Color.LightSlateGray
        Me.SubButton3.BackColor = Color.LightSlateGray
        Me.SubButton4.BackColor = Color.LightSlateGray
        Me.subButton5.BackColor = Color.LightSlateGray

    End Sub

    Private Sub SubButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubButton1.Click
        If Me.InventoryFlag = False Then
            Me.pnlMainManager.Controls.Clear()
        End If
        ResetSubButtonColors()
        Me.SubButton1.BackColor = Color.FromArgb(59, 96, 141)

        If Me.SystemFlag = True Then
            If typeProgram = "Online_Demo" Then
                DemoThisNotAvail()
                Exit Sub
            End If

            RaiseEvent OverrideTableStatus()
            Me.Dispose()
            '      ResetAllFlags()
            '            DisplaySeatingChart()
        ElseIf Me.MenuFlag = True Then

            If typeProgram = "Online_Demo" Then
                DemoThisNotAvail()
                Exit Sub
            End If

            If employeeAuthorization.OperationLevel > 0 Then
                MsgBox("After Spider POS is Minimized, launch Backoffice.")
                Me.ParentForm.WindowState = FormWindowState.Minimized
            Else
                MsgBox(actingManager.FullName & " is not authorized for Operations.")
            End If

            Exit Sub
            '222 below
            Dim info As DataSet_Builder.Info2_UC

            info = New DataSet_Builder.Info2_UC("Please wait while we connect to Datacenter...")
            info.Location = New Point((pnlMainManager.Width - info.Width) / 2, (pnlMainManager.Height - info.Height) / 2)
            Me.pnlMainManager.Controls.Add(info)
            '    info.BringToFront()
            info.ThisIsForDelay()

            If employeeAuthorization.OperationLevel > 0 Then
                MsgBox(actingManager.FullName & " please launch Spider Backoffice from Desktop.")
                Exit Sub
                ' old  Shell("""Q:\Tahsc_server.exe""")
                '     Shell("""\\phoenix\eGlobalPartners\Tahsc_server.exe""")
                Shell("""\\Phoenix\eglobalpartners\Backoffice Files\Spider Backoffice.application""")
                '                reduce screen size
                '               System.Windows()
            Else
                MsgBox(actingManager.FullName & " is not authorized for Operations.")
            End If


        ElseIf Me.OperationFlag = True Then

            If Not currentServer.EmployeeID > 0 Then
                currentServer = actingManager
            End If

            RaiseEvent OpenNewTable()
            Me.Dispose()

        ElseIf Me.DailysFlag = True Then
            '        weAreClosingDaily = True

            If typeProgram = "Online_Demo" Then
                DemoThisNotAvail()
                Exit Sub
            End If

            If employeeAuthorization.OperationLevel > 0 Or employeeAuthorization.SystemLevel > 0 Then
                DisplayDailyChoices()
            Else
                MsgBox(actingManager.FullName & " is not authorized for Operations.")
            End If

        ElseIf Me.InventoryFlag = True Then

            If InventoryTable = "" Or InventoryTable = "Physical" Then
                If Not Me.InventoryTable = "" Then
                    SaveInventoryData()
                End If
                InventoryTable = "Delivery"
                Me.pnlMainManager.Controls.Clear()
                invDelivery = New DataSet_Builder.InvDelivery_UC(companyInfo, currentTerminal, connectserver, typeProgram, actingManager.EmployeeID, "Delivery")
                invDelivery.Location = New Point(((Me.pnlMainManager.Width - invDelivery.Width) / 2), 50)
                Me.pnlMainManager.Controls.Add(invDelivery)

            ElseIf InventoryTable = "Cycle" Then
                SaveInventoryData()
                InventoryTable = "Delivery"
                invDelivery.ChangeToThisType(InventoryTable)

            End If



        End If

    End Sub

    Private Sub SubButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubButton2.Click
        If Me.InventoryFlag = False Then
            Me.pnlMainManager.Controls.Clear()
        End If
        ResetSubButtonColors()
        Me.SubButton2.BackColor = Color.FromArgb(59, 96, 141)

        If Me.OperationFlag = True Then

            If Not currentServer.EmployeeID > 0 Then
                currentServer = actingManager
            End If

            RaiseEvent OpenNewTabEvent()
            Me.Dispose()

        ElseIf Me.DailysFlag = True Then
            MsgBox("You must first EXIT spiderPOS and reenter to open Daily or use current.")
            Exit Sub

            openDaily = New DataSet_Builder.MenuSelection_UC(ds.Tables("MenuChoice"), Nothing, Nothing)

            openDaily.Location = New Point(((Me.pnlMainManager.Width - openDaily.Width) / 2), 50)
            Me.pnlMainManager.Controls.Add(openDaily)
            '       OpenNewDaily()

        ElseIf Me.MenuFlag = True Then

            If typeProgram = "Online_Demo" Then
                DemoThisNotAvail()
                Exit Sub
            End If

            If currentTerminal.currentPrimaryMenuID = currentTerminal.primaryMenuID Then
                changeMenu = New DataSet_Builder.MenuSelection_UC(ds.Tables("MenuChoice"), currentTerminal.primaryMenuID, currentTerminal.secondaryMenuID)
            Else
                changeMenu = New DataSet_Builder.MenuSelection_UC(ds.Tables("MenuChoice"), currentTerminal.secondaryMenuID, currentTerminal.primaryMenuID)
            End If

            changeMenu.Location = New Point(((Me.pnlMainManager.Width - changeMenu.Width) / 2), 50)
            Me.pnlMainManager.Controls.Add(changeMenu)

        ElseIf Me.SystemFlag = True Then

            If typeProgram = "Online_Demo" Then
                DemoThisNotAvail()
                Exit Sub
            End If

            Me.ParentForm.WindowState = FormWindowState.Minimized

        ElseIf Me.InventoryFlag = True Then

            If InventoryTable = "" Or InventoryTable = "Physical" Then
                If Not Me.InventoryTable = "" Then
                    SaveInventoryData()
                End If
                InventoryTable = "Cycle"
                Me.pnlMainManager.Controls.Clear()
                invDelivery = New DataSet_Builder.InvDelivery_UC(companyInfo, currentTerminal, connectserver, typeProgram, actingManager.EmployeeID, "Cycle")
                invDelivery.Location = New Point(((Me.pnlMainManager.Width - invDelivery.Width) / 2), 50)
                Me.pnlMainManager.Controls.Add(invDelivery)

            ElseIf InventoryTable = "Delivery" Then
                SaveInventoryData()
                InventoryTable = "Cycle"
                invDelivery.ChangeToThisType(InventoryTable)

            End If


        End If

    End Sub

    Private Sub SubButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubButton3.Click
        If Me.InventoryFlag = False Then
            Me.pnlMainManager.Controls.Clear()
        End If
        ResetSubButtonColors()
        Me.SubButton3.BackColor = Color.FromArgb(59, 96, 141)

        If Me.OperationFlag = True Then
            ' cash out

            If typeProgram = "Online_Demo" Then
                DemoThisNotAvail()
                Exit Sub
            End If

            expNum = New Int64
            expNum = CreateNewExperience(actingManager.EmployeeID, Nothing, -888, "Cash Out", 0, 10, Nothing, 0, actingManager.LoginTrackingID)

            cashOut = New CashOut_UC(expNum, -3, 1000) 'expnum, payTypeID, MaxCashOut
            cashOut.Location = New Point((Me.pnlMainManager.Width - cashOut.Width) / 2, (Me.pnlMainManager.Height - cashOut.Height) / 2)
            Me.cashOut.lblCashOut.Text = "Cash Out"
            Me.pnlMainManager.Controls.Add(cashOut)

        ElseIf Me.DailysFlag = True Then

            If currentTerminal.HasCashDrawer = True Then

                If typeProgram = "Online_Demo" Then
                    DemoThisNotAvail()
                    Exit Sub
                End If

                Try
                    If employeeAuthorization.OperationLevel > 0 Or employeeAuthorization.SystemLevel > 0 Then
                        DetermineOpenCashDrawer(currentTerminal.CurrentDailyCode)
                        With dvTermsOpen
                            .Table = dtTermsOpen
                            .RowFilter = "TerminalsPrimaryKey = " & currentTerminal.TermPrimaryKey
                        End With
                        OpenCloseCashDrawer(0)  '0 means this terminal only
                    End If

                Catch ex As Exception
                    CloseConnection()
                    MsgBox(ex.Message)
                End Try


                '       currentTerminal.TermPrimaryKey()

            Else
                ' we need to allow managers to close out cash drawers from NON drawer terminals
                If employeeAuthorization.OperationLevel > 0 Or employeeAuthorization.SystemLevel > 0 Then
                    OpenCloseCashDrawer(dtTerminalsMethod.Rows.Count)
                Else
                    MsgBox("There is no Cash Drawer associated with this Terminal. Adjust in Setup.")
                    Exit Sub
                End If

            End If

        ElseIf Me.MenuFlag = True Then
            Dim info As DataSet_Builder.Info2_UC
            Dim menuReloadSuccess As Boolean

            If typeProgram = "Online_Demo" Then
                DemoThisNotAvail()
                Exit Sub
            End If

            ' trying for menu testing
            '            companyInfo.usingDefaults = False
            '           companyInfo.CompanyID = "brothe"
            '          companyInfo.LocationID = "000025"

            menuReloadSuccess = PopulateMenu(False)

            If menuReloadSuccess = True Then
                info = New DataSet_Builder.Info2_UC("Menu Reloaded")
                info.Location = New Point((pnlMainManager.Width - info.Width) / 2, (pnlMainManager.Height - info.Height) / 2)
                Me.pnlMainManager.Controls.Add(info)
                info.BringToFront()
            End If

        ElseIf Me.SystemFlag = True Then
            ' ReRead Creedit Swipe
            RaiseEvent ReReadCredit()

        ElseIf Me.InventoryFlag = True Then



        End If

    End Sub

    Private Sub SubButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubButton4.Click
        If Me.InventoryFlag = False Then
            Me.pnlMainManager.Controls.Clear()
        End If
        ResetSubButtonColors()
        Me.SubButton4.BackColor = Color.FromArgb(59, 96, 141)

        If Me.OperationFlag = True Then

            If typeProgram = "Online_Demo" Then
                DemoThisNotAvail()
                Exit Sub
            End If

            If employeeAuthorization.OperationLevel > 1 Then
                StartCreditCardReturn()
            Else
                MsgBox(actingManager.FullName & " is not authorized for Credit Card Returns.")
            End If


        ElseIf Me.DailysFlag = True Then
            If typeProgram = "Demo" Or typeProgram = "Online_Demo" Then
                RaiseEvent StartExit()
            Else
                If MsgBox("Are you sure you want to Exit spiderPOS ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    RaiseEvent StartExit()
                End If
            End If

        ElseIf Me.MenuFlag = True Then


        ElseIf Me.SystemFlag = True Then

            If typeProgram = "Online_Demo" Then
                DemoThisNotAvail()
                Exit Sub
            End If

            GenerateOrderTables.SwitchConnection()
            DisplayConnectionButton()
            MsgBox("Connection Changed to: " & connectserver)

        ElseIf Me.InventoryFlag = True Then
            'Physical Inventory
            If Not Me.InventoryTable = "" Then
                SaveInventoryData()
            End If

            InventoryTable = "Physical"
            Me.pnlMainManager.Controls.Clear()
            invPhysical = New DataSet_Builder.InvPhysical_UC(companyInfo, currentTerminal, connectserver, typeProgram, actingManager.EmployeeID)
            invPhysical.Location = New Point(((Me.pnlMainManager.Width - invPhysical.Width) / 2), 50)
            Me.pnlMainManager.Controls.Add(invPhysical)
            ' warnings
            'currently not avail
            'testing inv counting
            '      GenerateOrderTables.StartDailyBusinessClose(actingManager.EmployeeID, currentTerminal.CurrentDailyCode)

        End If

    End Sub

    Private Sub subButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles subButton5.Click

        If Me.InventoryFlag = False Then
            Me.pnlMainManager.Controls.Clear()
        End If
        ResetSubButtonColors()
        Me.subButton5.BackColor = Color.FromArgb(59, 96, 141)

        If Me.DailysFlag = True Then
            If typeProgram = "Online_Demo" Then
                DemoThisNotAvail()
                Exit Sub
            End If

            If employeeAuthorization.OperationLevel > 1 Then
                If companyInfo.CompanyID = "rasoi2" Then

                    trainingDailys = New Training_UC
                    trainingDailys.Location = New Point(((Me.pnlMainManager.Width - trainingDailys.Width) / 2), 100)
                    Me.pnlMainManager.Controls.Add(trainingDailys)

                End If

            Else
                MsgBox(actingManager.FullName & " is not authorized for Training Module.")
            End If
        End If

    End Sub

    Private Sub SaveInventoryData()

        If typeProgram = "Online_Demo" Then
            Exit Sub
        End If

        If InventoryTable = "" Then


        ElseIf InventoryTable = "Delivery" Then
            invDelivery.UpdateDeliveryData()

        ElseIf InventoryTable = "Cycle" Then
            invDelivery.UpdateCycleData()

        ElseIf InventoryTable = "Physical" Then
            invPhysical.UpdateCycleData()

        End If


    End Sub

    Private Sub OpenCloseCashDrawer(ByVal _thisCashTerminal As Integer)

        cashDrawer = New CashDrawer_UC(_thisCashTerminal)
        cashDrawer.Location = New Point((Me.pnlMainManager.Width - cashDrawer.Width) / 2, (Me.pnlMainManager.Height - cashDrawer.Height) / 2)
        Me.pnlMainManager.Controls.Add(cashDrawer)

    End Sub

    Private Sub AcceptingCashOut(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cashOut.AcceptCashOut

        Dim newPayment As DataSet_Builder.Payment
        Dim amount As Decimal

        If cashOut.ItemPrice > 0 Then
            amount = -1 * cashOut.ItemPrice
            newPayment.Purchase = Format(amount, "##,##0.00")
            newPayment.PaymentTypeID = cashOut.PaymentTypeID
            newPayment.PaymentTypeName = "Cash"   '"Enter Acct #"
            newPayment.Description = cashOut.ItemDescription

            GenerateOrderTables.AddPaymentToDataRow(newPayment, True, expNum, actingManager.EmployeeID, 1, False)
            'we need this update here b/c we are not in any experience
            GenerateOrderTables.UpdatePaymentsAndCredits()
        End If

        cashOut.Dispose()

    End Sub

    Friend Sub JustVoidedCheck(ByVal tn As Integer)

        _tableSelected = tn
        Try
            ResetSeatingChartTableStatus(tn, False)  'false means to just adjust color 
        Catch ex As Exception
            ' may fail if never created seating chart, just went right to mgmt screen
        End Try

    End Sub

    Private Sub btnDailys_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDailys.Click
        ResetAllFlags()
        Me.btnDailys.BackColor = Color.FromArgb(59, 96, 141)
        '     Me.btnDailys.ForeColor = c3
        Me.DailysFlag = True

        Me.pnlMainManager.Controls.Clear()

        If employeeAuthorization.OperationLevel > 0 Or employeeAuthorization.SystemLevel > 0 Then
            DisplayDailyButtons()
        Else
            MsgBox(actingManager.FullName & " is not authorized for Operations.")

        End If
    End Sub

    Private Sub DisplayDailyButtons()
        Me.pnlManagerSelection.Visible = False
        Me.pnlManagerSubSelecetion.Visible = True

        RemoveTextFromSubButtons()
        Me.SubButton1.Text = "Close Current Daily"
        Me.SubButton2.Text = "Open New Daily"
        Me.SubButton3.Text = "Cash Drawer"
        Me.SubButton4.Text = "Exit POS"
        Me.subButton5.Text = "Training"

    End Sub

    Private Sub DisplayDailyChoices()
        Dim oRow As DataRow

        Me.pnlMainManager.Controls.Clear()

        Dim dvOpenBusiness As New DataView
        '    GenerateOrderTables.DetermineOpenCashDrawer()

        '999999

        GenerateOrderTables.DetermineOpenBusiness()

        If dsOrder.Tables("OpenBusiness").Rows.Count > 1 Then
            dvOpenBusiness.Table = dsOrder.Tables("OpenBusiness")

            selectDaily = New DataSet_Builder.SelectionPanel_UC
            selectDaily.dvUsing = dvOpenBusiness
            selectDaily.Location = New Point((Me.pnlMainManager.Width - selectDaily.Width) / 2, (Me.pnlMainManager.Height - selectDaily.Height) / 2)
            selectDaily.DetermineButtonSizes()
            selectDaily.DetermineButtonLocations()
            Me.pnlMainManager.Controls.Add(selectDaily)
        End If

        '   currently not really using
        If dsOrder.Tables("OpenBusiness").Rows.Count = 1 Then
            If dsOrder.Tables("OpenBusiness").Rows(0)("DailyCode") = currentTerminal.CurrentDailyCode Then
                closingDailyCode = currentTerminal.CurrentDailyCode
                CloseSelectedDaily()
            Else
                MsgBox("Operating Daily Code Does Not Match Database Daily Code. Contact System Administrator")
                Exit Sub
            End If
        End If

    End Sub

    Private Sub CloseDailyBusinessSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles selectDaily.ButtonSelected

        closingDailyCode = sender.dailyCode
        CloseSelectedDaily()

    End Sub

    Private Sub CloseSelectedDaily() Handles cashDrawer.ResetClosingData


        '*******
        '666
        'just for testing
        '      PopulatePaymentsAndCreditsByDaily(closingDailyCode)
        '     RaiseEvent CloseBatchManagerForm(closingDailyCode)

        '    Exit Sub

        Try
            DetermineOpenCashDrawer(closingDailyCode)
            PopulateLoggedInEmployees(False)
            DetermineIfOpenTables(closingDailyCode)
            CreateDataViews(currentServer.EmployeeID, False)

            CloseDailyRoutine()
        Catch ex As Exception
            CloseConnection()
        End Try

    End Sub

    Private Function TestAbilityToCloseDaily()

        If allTicketsOpen + allCashDrawersOpen > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub btnClosingContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosingContinue.Click

        If numberOfAttemtsClosingDaily >= 2 Then
            StartManagerReports(sender, e)
            ProceedToBatchClose()
        Else
            If TestAbilityToCloseDaily() = False Then
                MsgBox("There are still open Tickets or Cash Drawers")
                numberOfAttemtsClosingDaily += 1
            Else
                ' I tried many things to display a message to wait
                ' they all come up too late ???
                '          Dim info As DataSet_Builder.Info2_UC
                '         info = New DataSet_Builder.Info2_UC("Running Closing Totals. This may take a few seconds.")
                '        info.Location = New Point((pnlMainManager.Width - info.Width) / 2, (pnlMainManager.Height - info.Height) / 2)
                '       Me.pnlMainManager.Controls.Add(info)
                '         info.Visible = True
                '        info.ChangeTextToThis("Running Closing Totals. This may take a few seconds.")
                '           Me.pnlMainManager.Controls.Clear()
                '          lblMgrDirectionsNumberPad.Text = "Running Closing Totals..."
                '         lblMgrDirectionsNumberPad.Visible = True
                'Comic Sans MS
                '    lblDailyCloseDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                '   lblDailyCloseDesc.Text = "Running Closing Totals. This may take a few seconds."

                StartManagerReports(sender, e)
                ProceedToBatchClose()
            End If
        End If

    End Sub


    Private Sub StartManagerReports(ByVal sender As System.Object, ByVal e As System.EventArgs)

        reportManager = New DataSet_Builder.Manager_Reports_UC(connectserver, companyInfo, currentTerminal, connectserver, typeProgram) ' DataSet_Builder.Reports_EmployeeHours '
        '   Me.Controls.Add(reportManager)
        reportManager.IsPrintMode = True
        reportManager.PrintEntireHouseDailyDetails(sender, e)
        reportManager.Dispose()

    End Sub



    Private Sub CloseDailyRoutine()

        allTicketsOpen = dvAvailTables.Count + dvAvailTabs.Count + dvQuickTickets.Count 'need to add quick
        allCashDrawersOpen = dsOrder.Tables("TermsOpen").Rows.Count
        allEmployeesClockedIn = dsEmployee.Tables("LoggedInEmployees").Rows.Count

        pnlCDInfo.Visible = True
        Me.btnCDTickets.Text = "Tickets: " & allTicketsOpen
        Me.btnCDCashDrawer.Text = "Cash Drawer: " & allCashDrawersOpen
        Me.btnCDClockedIn.Text = "ClockedIn: " & allEmployeesClockedIn
        'testing only
        '    allTicketsOpen = 0
        '   allCashDrawersOpen = 0

        '   If TestAbilityToCloseDaily() = True Then Me.pnlClosingDailyDirections.Visible = True
        Me.pnlClosingDailyDirections.Visible = True

        If allTicketsOpen > 0 Then
            '   There are open Tables
            DisplayOpenTabsAndTables()
            '       DisplayInfoAboutOpenTables()
            ' make sure above is not displaying transfered and quick
        Else
            ' no open tickets 
            '   we display open drawers
            If allCashDrawersOpen > 0 Then

                OpenCloseCashDrawer(dtTerminalsMethod.Rows.Count) ' not necessarily this terminal


            Else
                lblDailyCloseDesc.Text = "Select Close Batch --->     This may take a few seconds."

                ' no open cash drawers
                If allEmployeesClockedIn > 0 Then
                    employeeLog = New EmployeeLoggedInUserControl(False)
                    employeeLog.Location = New Point((Me.pnlMainManager.Width - employeeLog.Width) / 2, (Me.pnlMainManager.Height - employeeLog.Height) / 2)
                    Me.pnlMainManager.Controls.Add(employeeLog)

                Else
                    Dim sender As System.Object
                    Dim e As System.EventArgs
                    StartManagerReports(sender, e)
                    ProceedToBatchClose()
                End If
            End If
        End If
    End Sub

    Private Sub ProceedToBatchClose()

        allPaymentsLoaded = PopulatePaymentsAndCreditsByDaily(closingDailyCode)

        If allPaymentsLoaded = True Then
            Me.pnlClosingDailyDirections.Visible = False
            RaiseEvent CloseBatchManagerForm(closingDailyCode)
        End If

    End Sub

    Friend Sub ReinitializeOpenTicketsFromForm(ByVal weAreClosingDaily As Int64)

        closingDailyCode = weAreClosingDaily
        Me.DailysFlag = True
        CloseSelectedDaily()

    End Sub

    Friend Sub ReinitializeOpenCashDrawers(ByVal termsOpen As Integer) Handles cashDrawer.TerminalsNowOpen
        DetermineOpenCashDrawer(closingDailyCode)
        allCashDrawersOpen = dsOrder.Tables("TermsOpen").Rows.Count
        Me.btnCDCashDrawer.Text = "Cash Drawer: " & allCashDrawersOpen

    End Sub

    Private Sub btnCDTickets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCDTickets.Click
        Me.pnlMainManager.Controls.Clear()

        If allTicketsOpen > 0 Then
            '   There are open Tables
            DisplayOpenTabsAndTables()
            '        DisplayInfoAboutOpenTables()
        End If

    End Sub

    Private Sub btnCDCashDrawer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCDCashDrawer.Click
        Me.pnlMainManager.Controls.Clear()

        'testing   OpenCloseCashDrawer(True) 

        If allCashDrawersOpen > 0 Then
            OpenCloseCashDrawer(dtTerminalsMethod.Rows.Count) ' not necessarily this terminal
        End If

    End Sub

    Private Sub btnCDClockedIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCDClockedIn.Click
        Me.pnlMainManager.Controls.Clear()

        If allEmployeesClockedIn > 0 Then
            employeeLog = New EmployeeLoggedInUserControl(False)
            employeeLog.Location = New Point((Me.pnlMainManager.Width - employeeLog.Width) / 2, (Me.pnlMainManager.Height - employeeLog.Height) / 2)
            Me.pnlMainManager.Controls.Add(employeeLog)
        End If

    End Sub



    Private Sub DisplayInfoAboutOpenTables()
        Dim info As DataSet_Builder.Information_UC

        info = New DataSet_Builder.Information_UC("All displayed checks listed must be closed before Daily Close.")
        info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
        Me.Controls.Add(info)
        info.BringToFront()

    End Sub

    Private Sub OpenNewDaily() Handles openDaily.ChangeMenus

        currentTerminal.primaryMenuID = openDaily.PMenuID
        currentTerminal.secondaryMenuID = openDaily.SMenuID

        openDaily.Dispose()
        GenerateOrderTables.StartNewDaily()
        ' ExitManager()
        RaiseEvent DisposeManager()

    End Sub

    Private Sub ChangeMenu_Click() Handles changeMenu.ChangeMenus

        SwitchToSecondaryMenu()
        changeMenu.Dispose()
        RaiseEvent DisposeManager()

    End Sub

    Private Sub OpenNewDailyAndSave() Handles openDaily.AcceptMenuEvent
        currentTerminal.primaryMenuID = openDaily.PMenuID
        currentTerminal.secondaryMenuID = openDaily.SMenuID
        currentTerminal.initPrimaryMenuID = openDaily.PMenuID
        currentTerminal.currentPrimaryMenuID = openDaily.PMenuID

        openDaily.Dispose()
        GenerateOrderTables.StartNewDaily()
        RaiseEvent DisposeManager()

        Exit Sub
        '222


        '     Dim adt As New SqlClient.SqlDataAdapter("SELECT MenuID, MenuName, LastOrder, AutoChange FROM MenuChoice WHERE Active = 1 AND LocationID = '" & companyInfo.LocationID & "' ORDER BY AutoChange DESC", sql.cn)
        '    Dim cbd As New SqlClient.SqlCommandBuilder(adt)

        Dim oRow As DataRow

        For Each oRow In ds.Tables("MenuChoice").Rows
            If oRow("MenuID") = currentTerminal.primaryMenuID Then
                oRow("LastOrder") = 1
            ElseIf oRow("MenuID") = currentTerminal.secondaryMenuID Then
                oRow("LastOrder") = 2
            Else
                oRow("LastOrder") = DBNull.Value
            End If
        Next

        If mainServerConnected = True Then
            sql.SqlDailyMenuChoice.Update(ds, "MenuChoice")
            ds.Tables("MenuChoice").AcceptChanges()
        End If

        openDaily.Dispose()
        GenerateOrderTables.StartNewDaily()
        RaiseEvent DisposeManager()

    End Sub

    Private Sub ChangeMenuAndSave_Click() Handles changeMenu.AcceptMenuEvent
        currentTerminal.primaryMenuID = changeMenu.PMenuID
        currentTerminal.secondaryMenuID = changeMenu.SMenuID
        '      currentTerminal.CurrentMenuID = primaryMenuID

        changeMenu.Dispose()
        Exit Sub
        '222


        '     Dim adt As New SqlClient.SqlDataAdapter("SELECT MenuID, MenuName, LastOrder FROM MenuChoice", sql.cn)
        '    Dim cbd As New SqlClient.SqlCommandBuilder(adt)

        Dim oRow As DataRow

        For Each oRow In ds.Tables("MenuChoice").Rows
            If oRow("MenuID") = currentTerminal.primaryMenuID Then
                oRow("LastOrder") = 1
            ElseIf oRow("MenuID") = currentTerminal.secondaryMenuID Then
                oRow("LastOrder") = 2
            Else
                oRow("LastOrder") = DBNull.Value
            End If
        Next

        If mainServerConnected = True Then
            sql.SqlDailyMenuChoice.Update(ds, "MenuChoice")
            ds.Tables("MenuChoice").AcceptChanges()
        End If

        changeMenu.Dispose()

    End Sub


    Private Sub StartCreditCardReturn()

        returnCredit = New ReturnCredit_UC
        returnCredit.Location = New Point((Me.pnlMainManager.Width - returnCredit.Width) / 2, (Me.pnlMainManager.Height - returnCredit.Height) / 2)
        Me.pnlMainManager.Controls.Add(returnCredit)

    End Sub

    '  Private Sub ExitManager()

    '     actingManager = Nothing
    '    empActive = Nothing
    '   Me.Parent.Dispose()
    '    End Sub

    Private Sub PopulateOpenTabsAndTables222()
        dsOrder.Tables("AvailTables").Clear()
        dsOrder.Tables("AvailTabs").Clear()

        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()

        '     sql.SqlSelectCommandAvailTablesSP.Parameters("@CompanyID").Value = CompanyID
        sql.SqlSelectCommandAvailTablesSP.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandAvailTablesSP.Parameters("@EmployeeID").Value = empActive
        sql.SqlSelectCommandAvailTablesSP.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
        sql.SqlAvailTablesSP.Fill(dsOrder.Tables("AvailTables"))
        '      sql.SqlSelectCommandAvailTabsSP.Parameters("@CompanyID").Value = CompanyID
        sql.SqlSelectCommandAvailTabsSP.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandAvailTabsSP.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
        sql.SqlSelectCommandAvailTabsSP.Parameters("@EmployeeID").Value = empActive
        sql.SqlAvailTabsSP.Fill(dsOrder.Tables("AvailTabs"))

        sql.cn.Close()

    End Sub







    Private Sub lblCurrentServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblCurrentServer.Click

        Me.pnlMainManager.Controls.Clear()

        If displayingOpenOrClose = "Open" Then
            mainPanelCounterIndex += 10
            DisplayOpenTabsAndTables()
        ElseIf displayingOpenOrClose = "Beths" Then
            mainPanelCounterIndex += 10
            DisplayGroupTabs()
        ElseIf displayingOpenOrClose = "Closed" Then
            skipClosedIndex += 10 '40
            DisplayClosedTabsAndTables()
        End If

    End Sub

End Class
