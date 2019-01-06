Imports DataSet_Builder


Public Class OpeningScreen
    Inherits System.Windows.Forms.UserControl

    '   Dim sql As New DataSet_Builder.SQLHelper(connectserver)

    Dim WithEvents employeeLog As EmployeeLoggedInUserControl
    Dim WithEvents selectDaily As SelectionDaily_UC
    Dim WithEvents newBatchQuestion As DataSet_Builder.Information_UC
    Dim WithEvents infoClockin As DataSet_Builder.Information_UC
    Dim WithEvents loginPad As NumberPad 'DataSet_Builder.NumberPadLarge
    Dim WithEvents connectionDown As ConnectionDown_UC

    Private _pMenuID As Integer
    Private _sMenuID As Integer
    Private _pMenuName As String
    Private _sMenuName As String
    Private openBusinessCount As Integer
   
    Dim titleHeader As New DataSet_Builder.TitleUserControl
    Friend WithEvents grpEmployeeClockIn As System.Windows.Forms.GroupBox
    Friend WithEvents grdEmpClockIn As System.Windows.Forms.DataGrid
    Friend WithEvents lblNumClockedIn As System.Windows.Forms.Label
    Friend WithEvents grpCashDrawers As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lstOpenCashDrawers As System.Windows.Forms.ListView
    Friend WithEvents clmCashDrawer As System.Windows.Forms.ColumnHeader
    Friend WithEvents clmIsOpen As System.Windows.Forms.ColumnHeader
    Friend WithEvents grpDaily As System.Windows.Forms.GroupBox
    Friend WithEvents lblMenu1 As System.Windows.Forms.Label
    Friend WithEvents btnOpenNewDaily As System.Windows.Forms.Button
    Friend WithEvents lblMenu2 As System.Windows.Forms.Label
    Friend WithEvents lblMenuSecond As System.Windows.Forms.Label
    Friend WithEvents lblMenuMain As System.Windows.Forms.Label
    Friend WithEvents btnOpenThisDaily As System.Windows.Forms.Button
    Friend WithEvents pnlDailyChooseTermMethod As System.Windows.Forms.Panel
    Friend WithEvents lblDailyChoose As System.Windows.Forms.Label
    Friend WithEvents btnDailyBar As System.Windows.Forms.Button
    Friend WithEvents btnDailyTable As System.Windows.Forms.Button
    Friend WithEvents btnDailyQuick As System.Windows.Forms.Button
    Friend WithEvents btnSwitchDaily As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label

    Private _empOpening As String

    Event OpeningScreenClosing()    '(ByVal sender As Object, ByVal e As System.EventArgs)
    Event RestuarantOpen(ByVal openingInfo As OpenInfo)
    Event RestaurantOpening()
    Event ClosePOS()

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal empName As String)
        MyBase.New()

        _empOpening = empName

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
    Friend WithEvents pnlWelcome As System.Windows.Forms.Panel
    Friend WithEvents lblWelcome As System.Windows.Forms.Label
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OpeningScreen))
        Me.pnlWelcome = New System.Windows.Forms.Panel
        Me.lblWelcome = New System.Windows.Forms.Label
        Me.pnlTitle = New System.Windows.Forms.Panel
        Me.btnExit = New System.Windows.Forms.Button
        Me.grpEmployeeClockIn = New System.Windows.Forms.GroupBox
        Me.lblNumClockedIn = New System.Windows.Forms.Label
        Me.grdEmpClockIn = New System.Windows.Forms.DataGrid
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.grpCashDrawers = New System.Windows.Forms.GroupBox
        Me.lstOpenCashDrawers = New System.Windows.Forms.ListView
        Me.clmCashDrawer = New System.Windows.Forms.ColumnHeader
        Me.clmIsOpen = New System.Windows.Forms.ColumnHeader
        Me.grpDaily = New System.Windows.Forms.GroupBox
        Me.btnSwitchDaily = New System.Windows.Forms.Button
        Me.btnOpenThisDaily = New System.Windows.Forms.Button
        Me.lblMenuSecond = New System.Windows.Forms.Label
        Me.lblMenuMain = New System.Windows.Forms.Label
        Me.lblMenu2 = New System.Windows.Forms.Label
        Me.lblMenu1 = New System.Windows.Forms.Label
        Me.btnOpenNewDaily = New System.Windows.Forms.Button
        Me.pnlDailyChooseTermMethod = New System.Windows.Forms.Panel
        Me.lblDailyChoose = New System.Windows.Forms.Label
        Me.btnDailyBar = New System.Windows.Forms.Button
        Me.btnDailyTable = New System.Windows.Forms.Button
        Me.btnDailyQuick = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlWelcome.SuspendLayout()
        Me.grpEmployeeClockIn.SuspendLayout()
        CType(Me.grdEmpClockIn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCashDrawers.SuspendLayout()
        Me.grpDaily.SuspendLayout()
        Me.pnlDailyChooseTermMethod.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlWelcome
        '
        Me.pnlWelcome.BackColor = System.Drawing.Color.Transparent
        Me.pnlWelcome.Controls.Add(Me.lblWelcome)
        Me.pnlWelcome.ForeColor = System.Drawing.Color.White
        Me.pnlWelcome.Location = New System.Drawing.Point(8, 72)
        Me.pnlWelcome.Name = "pnlWelcome"
        Me.pnlWelcome.Size = New System.Drawing.Size(544, 52)
        Me.pnlWelcome.TabIndex = 0
        '
        'lblWelcome
        '
        Me.lblWelcome.Font = New System.Drawing.Font("Comic Sans MS", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWelcome.ForeColor = System.Drawing.Color.Black
        Me.lblWelcome.Location = New System.Drawing.Point(3, 3)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(480, 40)
        Me.lblWelcome.TabIndex = 0
        Me.lblWelcome.Text = "Welcome"
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.Transparent
        Me.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTitle.ForeColor = System.Drawing.Color.Blue
        Me.pnlTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(1024, 72)
        Me.pnlTitle.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Comic Sans MS", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(32, 683)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(96, 56)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'grpEmployeeClockIn
        '
        Me.grpEmployeeClockIn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grpEmployeeClockIn.Controls.Add(Me.lblNumClockedIn)
        Me.grpEmployeeClockIn.Controls.Add(Me.grdEmpClockIn)
        Me.grpEmployeeClockIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpEmployeeClockIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpEmployeeClockIn.ForeColor = System.Drawing.Color.MediumBlue
        Me.grpEmployeeClockIn.Location = New System.Drawing.Point(43, 259)
        Me.grpEmployeeClockIn.Name = "grpEmployeeClockIn"
        Me.grpEmployeeClockIn.Size = New System.Drawing.Size(254, 178)
        Me.grpEmployeeClockIn.TabIndex = 2
        Me.grpEmployeeClockIn.TabStop = False
        Me.grpEmployeeClockIn.Text = "Employees Clocked-In"
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
        '
        'grpCashDrawers
        '
        Me.grpCashDrawers.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grpCashDrawers.Controls.Add(Me.lstOpenCashDrawers)
        Me.grpCashDrawers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpCashDrawers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpCashDrawers.ForeColor = System.Drawing.Color.MediumBlue
        Me.grpCashDrawers.Location = New System.Drawing.Point(43, 458)
        Me.grpCashDrawers.Name = "grpCashDrawers"
        Me.grpCashDrawers.Size = New System.Drawing.Size(254, 178)
        Me.grpCashDrawers.TabIndex = 3
        Me.grpCashDrawers.TabStop = False
        Me.grpCashDrawers.Text = "Cash Drawers"
        '
        'lstOpenCashDrawers
        '
        Me.lstOpenCashDrawers.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lstOpenCashDrawers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clmCashDrawer, Me.clmIsOpen})
        Me.lstOpenCashDrawers.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstOpenCashDrawers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstOpenCashDrawers.Location = New System.Drawing.Point(6, 38)
        Me.lstOpenCashDrawers.Name = "lstOpenCashDrawers"
        Me.lstOpenCashDrawers.Size = New System.Drawing.Size(240, 97)
        Me.lstOpenCashDrawers.TabIndex = 0
        Me.lstOpenCashDrawers.UseCompatibleStateImageBehavior = False
        Me.lstOpenCashDrawers.View = System.Windows.Forms.View.Details
        '
        'clmCashDrawer
        '
        Me.clmCashDrawer.Text = ""
        Me.clmCashDrawer.Width = 120
        '
        'clmIsOpen
        '
        Me.clmIsOpen.Text = ""
        Me.clmIsOpen.Width = 115
        '
        'grpDaily
        '
        Me.grpDaily.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grpDaily.Controls.Add(Me.btnSwitchDaily)
        Me.grpDaily.Controls.Add(Me.btnOpenThisDaily)
        Me.grpDaily.Controls.Add(Me.lblMenuSecond)
        Me.grpDaily.Controls.Add(Me.lblMenuMain)
        Me.grpDaily.Controls.Add(Me.lblMenu2)
        Me.grpDaily.Controls.Add(Me.lblMenu1)
        Me.grpDaily.Controls.Add(Me.btnOpenNewDaily)
        Me.grpDaily.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDaily.ForeColor = System.Drawing.Color.MediumBlue
        Me.grpDaily.Location = New System.Drawing.Point(43, 144)
        Me.grpDaily.Name = "grpDaily"
        Me.grpDaily.Size = New System.Drawing.Size(254, 100)
        Me.grpDaily.TabIndex = 4
        Me.grpDaily.TabStop = False
        Me.grpDaily.Text = "Daily Business"
        '
        'btnSwitchDaily
        '
        Me.btnSwitchDaily.BackColor = System.Drawing.Color.IndianRed
        Me.btnSwitchDaily.Location = New System.Drawing.Point(234, 47)
        Me.btnSwitchDaily.Name = "btnSwitchDaily"
        Me.btnSwitchDaily.Size = New System.Drawing.Size(20, 47)
        Me.btnSwitchDaily.TabIndex = 6
        Me.btnSwitchDaily.UseVisualStyleBackColor = False
        Me.btnSwitchDaily.Visible = False
        '
        'btnOpenThisDaily
        '
        Me.btnOpenThisDaily.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnOpenThisDaily.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenThisDaily.ForeColor = System.Drawing.Color.Black
        Me.btnOpenThisDaily.Location = New System.Drawing.Point(130, 47)
        Me.btnOpenThisDaily.Name = "btnOpenThisDaily"
        Me.btnOpenThisDaily.Size = New System.Drawing.Size(107, 47)
        Me.btnOpenThisDaily.TabIndex = 5
        Me.btnOpenThisDaily.UseVisualStyleBackColor = False
        Me.btnOpenThisDaily.Visible = False
        '
        'lblMenuSecond
        '
        Me.lblMenuSecond.AutoSize = True
        Me.lblMenuSecond.Location = New System.Drawing.Point(30, 73)
        Me.lblMenuSecond.Name = "lblMenuSecond"
        Me.lblMenuSecond.Size = New System.Drawing.Size(61, 16)
        Me.lblMenuSecond.TabIndex = 4
        Me.lblMenuSecond.Text = "Second"
        '
        'lblMenuMain
        '
        Me.lblMenuMain.AutoSize = True
        Me.lblMenuMain.Location = New System.Drawing.Point(30, 40)
        Me.lblMenuMain.Name = "lblMenuMain"
        Me.lblMenuMain.Size = New System.Drawing.Size(41, 16)
        Me.lblMenuMain.TabIndex = 3
        Me.lblMenuMain.Text = "Main"
        '
        'lblMenu2
        '
        Me.lblMenu2.AutoSize = True
        Me.lblMenu2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMenu2.ForeColor = System.Drawing.Color.Black
        Me.lblMenu2.Location = New System.Drawing.Point(6, 60)
        Me.lblMenu2.Name = "lblMenu2"
        Me.lblMenu2.Size = New System.Drawing.Size(88, 13)
        Me.lblMenu2.TabIndex = 2
        Me.lblMenu2.Text = "Secondary Menu"
        '
        'lblMenu1
        '
        Me.lblMenu1.AutoSize = True
        Me.lblMenu1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMenu1.ForeColor = System.Drawing.Color.Black
        Me.lblMenu1.Location = New System.Drawing.Point(6, 24)
        Me.lblMenu1.Name = "lblMenu1"
        Me.lblMenu1.Size = New System.Drawing.Size(71, 13)
        Me.lblMenu1.TabIndex = 1
        Me.lblMenu1.Text = "Primary Menu"
        '
        'btnOpenNewDaily
        '
        Me.btnOpenNewDaily.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnOpenNewDaily.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenNewDaily.ForeColor = System.Drawing.Color.Black
        Me.btnOpenNewDaily.Location = New System.Drawing.Point(156, 21)
        Me.btnOpenNewDaily.Name = "btnOpenNewDaily"
        Me.btnOpenNewDaily.Size = New System.Drawing.Size(71, 47)
        Me.btnOpenNewDaily.TabIndex = 0
        Me.btnOpenNewDaily.Text = "Open New"
        Me.btnOpenNewDaily.UseVisualStyleBackColor = False
        Me.btnOpenNewDaily.Visible = False
        '
        'pnlDailyChooseTermMethod
        '
        Me.pnlDailyChooseTermMethod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlDailyChooseTermMethod.Controls.Add(Me.lblDailyChoose)
        Me.pnlDailyChooseTermMethod.Controls.Add(Me.btnDailyBar)
        Me.pnlDailyChooseTermMethod.Controls.Add(Me.btnDailyTable)
        Me.pnlDailyChooseTermMethod.Controls.Add(Me.btnDailyQuick)
        Me.pnlDailyChooseTermMethod.Location = New System.Drawing.Point(724, 259)
        Me.pnlDailyChooseTermMethod.Name = "pnlDailyChooseTermMethod"
        Me.pnlDailyChooseTermMethod.Size = New System.Drawing.Size(288, 104)
        Me.pnlDailyChooseTermMethod.TabIndex = 7
        Me.pnlDailyChooseTermMethod.Visible = False
        '
        'lblDailyChoose
        '
        Me.lblDailyChoose.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDailyChoose.Location = New System.Drawing.Point(32, 8)
        Me.lblDailyChoose.Name = "lblDailyChoose"
        Me.lblDailyChoose.Size = New System.Drawing.Size(216, 24)
        Me.lblDailyChoose.TabIndex = 3
        Me.lblDailyChoose.Text = "Choose Terminal Method"
        Me.lblDailyChoose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnDailyBar
        '
        Me.btnDailyBar.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnDailyBar.Location = New System.Drawing.Point(16, 40)
        Me.btnDailyBar.Name = "btnDailyBar"
        Me.btnDailyBar.Size = New System.Drawing.Size(80, 48)
        Me.btnDailyBar.TabIndex = 2
        Me.btnDailyBar.Text = "Bar"
        Me.btnDailyBar.UseVisualStyleBackColor = False
        '
        'btnDailyTable
        '
        Me.btnDailyTable.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnDailyTable.Location = New System.Drawing.Point(104, 40)
        Me.btnDailyTable.Name = "btnDailyTable"
        Me.btnDailyTable.Size = New System.Drawing.Size(80, 48)
        Me.btnDailyTable.TabIndex = 1
        Me.btnDailyTable.Text = "Table"
        Me.btnDailyTable.UseVisualStyleBackColor = False
        '
        'btnDailyQuick
        '
        Me.btnDailyQuick.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnDailyQuick.Location = New System.Drawing.Point(192, 40)
        Me.btnDailyQuick.Name = "btnDailyQuick"
        Me.btnDailyQuick.Size = New System.Drawing.Size(80, 48)
        Me.btnDailyQuick.TabIndex = 0
        Me.btnDailyQuick.Text = "Quick"
        Me.btnDailyQuick.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(378, 245)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(260, 280)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = resources.GetString("Label2.Text")
        Me.Label2.Visible = False
        '
        'OpeningScreen
        '
        Me.BackColor = System.Drawing.Color.Transparent
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pnlDailyChooseTermMethod)
        Me.Controls.Add(Me.grpDaily)
        Me.Controls.Add(Me.grpCashDrawers)
        Me.Controls.Add(Me.grpEmployeeClockIn)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlWelcome)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "OpeningScreen"
        Me.Size = New System.Drawing.Size(1024, 768)
        Me.pnlWelcome.ResumeLayout(False)
        Me.grpEmployeeClockIn.ResumeLayout(False)
        CType(Me.grdEmpClockIn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCashDrawers.ResumeLayout(False)
        Me.grpDaily.ResumeLayout(False)
        Me.grpDaily.PerformLayout()
        Me.pnlDailyChooseTermMethod.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub InitializeOther()
        '      Me.ClientSize = New Size(ssX, ssY)

        Me.grdEmpClockIn.DataSource = dsEmployee.Tables("LoggedInEmployees")

        lblWelcome.Text = "Welcome " & _empOpening

        titleHeader.Location = New Point((Me.pnlTitle.Width - titleHeader.Width) / 2, (Me.pnlTitle.Height - titleHeader.Height) / 2)
        Me.titleHeader.BackColor = Me.pnlTitle.BackColor
        Me.titleHeader.ForeColor = Color.Blue
        Me.pnlTitle.Controls.Add(titleHeader)

        loginPad = New NumberPad 'DataSet_Builder.NumberPadLarge
        loginPad.Location = New Point((Me.Width - loginPad.Width) / 2, (Me.Height - loginPad.Height + 75) / 2)
        Me.Controls.Add(loginPad)
        loginPad.Visible = False
        '      UpdateOpeningInfo()

    End Sub

    Public Sub UpdateOpeningInfo()

        lblNumClockedIn.Text = dsEmployee.Tables("LoggedInEmployees").Rows.Count
        '  bbbbb()

        If typeProgram = "Online_Demo" Then
            '999
            currentTerminal = New Terminal
            '   LoadingMenuRoutine()
            '  Exit Sub
        End If
       
        BegMenuRoutine()
        OpenBusinessRoutine()
        CashDrawerRoutine()

        If typeProgram = "Demo" Or typeProgram = "Online_Demo" Or SystemInformation.ComputerName = "EGLOBALMAIN" Or SystemInformation.ComputerName = "DILEO" Then
            Me.pnlDailyChooseTermMethod.Visible = True
            currentTerminal.TermMethod = "Bar"
        Else
            '444     chosenTermMethod = currentTerminal.TermMethod
        End If

        Exit Sub
        '444
        If ds.Tables("LocationOpening").Rows.Count > 0 Then
            If My.Application.Info.Version.MinorRevision < ds.Tables("LocationOpening").Rows(0)("LastAppVersion") Then
                MsgBox("There is a new version of Spider POS available. You must Exit and Restart Spider POS to download new version.")
            End If
        End If

    End Sub


    Private Sub DisplayLoginPad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click

        loginPad.btnNumberClear_Click()
        loginPad.Visible = True

    End Sub

    Private Sub Login_Entered(ByVal sender As Object, ByVal e As System.EventArgs) Handles loginPad.NumberEntered
        Dim loginEnter As String
        Dim emp As Employee
        Dim info As DataSet_Builder.Information_UC
        Dim isClockedIn As Integer

        loginEnter = loginPad.NumberString
        loginPad.Visible = False
        '     loginPad.Dispose()

        If loginEnter.Length = 8 Then
            '            emp = term_Tahsc.DetermineSecondEmployeeAuthorization(loginEnter)
            emp = GenerateOrderTables.TestUsernamePassword(loginEnter, False)
            If Not emp Is Nothing Then
                Try
                    isClockedIn = ActuallyLogIn(emp)

                Catch ex As Exception
                    CloseConnection()
                    MsgBox(ex.Message)
                    If emp.SystemMgmtAll = True Or emp.SystemMgmtLimited = True Or emp.OperationMgmtAll = True Then
                        actingManager = emp
                        WelcomeLogon()
                    End If
                    Exit Sub
                End Try

                If isClockedIn = 0 Then
                    MsgBox(emp.FullName & " is not clocked in. Remember to clock in after opening.")
                    '        
                    '444           infoClockin = New DataSet_Builder.Information_UC(emp.FullName & " is not clocked in. PLEASE CLOCK IN")
                    '          infoClockin.Location = New Point((Me.Width - infoClockin.Width) / 2, (Me.Height - infoClockin.Height) / 2)
                    '         Me.Controls.Add(infoClockin)
                    '        infoClockin.BringToFront()
                    '444           Exit Sub
                ElseIf isClockedIn = 1 Then
                    ' this will result in opening below
                Else
                    MsgBox("Employee Is Clocked in more than once. Please See Manager.")
                End If

                actingManager = emp
                WelcomeLogon()

            Else
                infoClockin = New DataSet_Builder.Information_UC("PLEASE CLOCK IN")
                infoClockin.Location = New Point((Me.Width - infoClockin.Width) / 2, (Me.Height - infoClockin.Height) / 2)
                Me.Controls.Add(infoClockin)
                infoClockin.BringToFront()
            End If
        Else
            info = New DataSet_Builder.Information_UC("Please Combine Your EmployeeID as Passcode then Press Enter")
            info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            Me.Controls.Add(info)
            info.BringToFront()

        End If

    End Sub

    Private Sub InfoClockinHit(ByVal sender As Object, ByVal e As EventArgs) Handles infoClockin.AcceptInformation
        RaiseEvent OpeningScreenClosing()
        '   Me.Dispose()

    End Sub

    Private Sub WelcomeLogon()

        Me.pnlWelcome.Dispose()
        Me.btnExit.Dispose()
        '      ClosingEmployeeLogSecondStep()
        '     Exit Sub

        'below has a problem with TerminalsGroupID

        employeeLog = New EmployeeLoggedInUserControl(False)
        employeeLog.Location = New Point((Me.Width - employeeLog.Width) / 2, (Me.Height - employeeLog.Height) / 2)
        Me.Controls.Add(employeeLog)

    End Sub

    Private Sub ClosingEmployeeLog(ByVal sender As Object, ByVal e As System.EventArgs) Handles employeeLog.ClosedEmployeeLog

        ClosingEmployeeLogSecondStep()

    End Sub

    Private Sub ClosingEmployeeLogSecondStep()
        'the reason this is here is to skip employeeLog on opening

        Dim oRow As DataRow
        Dim emp As Employee

        For Each oRow In dsEmployee.Tables("LoggedInEmployees").Rows
            For Each emp In AllEmployees
                If emp.EmployeeID = oRow("EmployeeID") Then
                    GenerateOrderTables.FillJobCodeInfo(emp, oRow("JobCode"))
                    Exit For
                End If
            Next
        Next

        BatchRoutine()

        '     DisplaySeatingChart()

    End Sub


    Private Sub BatchRoutine()
        Dim dvOpenBusiness As New DataView

        DetermineOpenBusiness()

        dvOpenBusiness.Table = dsOrder.Tables("OpenBusiness")
        selectDaily = New SelectionDaily_UC(dvOpenBusiness)
        selectDaily.Location = New Point((Me.Width - selectDaily.Width) / 2, (Me.Height - selectDaily.Height) / 2)
        Me.Controls.Add(selectDaily)

        If dsOrder.Tables("OpenBusiness").Rows.Count > 0 Then

        Else


        End If

    End Sub

    Private Sub CashDrawerRoutine()

        Try
            DetermineOpenCashDrawer(currentTerminal.CurrentDailyCode)

            Dim oRow As DataRow
            Dim dRow As DataRow
            Dim isOpen As Boolean
            For Each oRow In ds.Tables("TerminalsMethod").Rows
                If oRow("hasCashDrawer") = True Then
                    Dim itemCashDrawer As New ListViewItem
                    itemCashDrawer.Text = oRow("TerminalName")

                    isOpen = False

                    'table below has not been populated yet
                    For Each dRow In dsOrder.Tables("TermsOpen").Rows
                        If oRow("TerminalsPrimaryKey") = dRow("TerminalsPrimaryKey") Then
                            isOpen = True
                        End If
                    Next
                    If isOpen = True Then
                        itemCashDrawer.SubItems.Add("Open")
                    Else
                        If oRow("autoOpenDrawer") = True Then
                            itemCashDrawer.SubItems.Add("Open with " & oRow("normalOpenAmount"))
                        Else
                            itemCashDrawer.SubItems.Add("Closed")
                        End If
                    End If
                    lstOpenCashDrawers.Items.Add(itemCashDrawer)
                End If
            Next

            'only for eglobalmain test             currentTerminal.TermPrimaryKey = 4
            With dvTermsOpen
                .Table = dtTermsOpen
                .RowFilter = "TerminalsPrimaryKey = " & currentTerminal.TermPrimaryKey
            End With

            If dvTermsOpen.Count = 1 Then
                currentTerminal.TerminalsOpenID = dvTermsOpen(0)("TerminalsOpenID")
                actingManager = Nothing
            Else
                If currentTerminal.HasCashDrawer = True Then
                    '    MsgBox("Do NOT forget to open your Cash Drawer")
                    If actingManager.OperationMgmtAll = True Or actingManager.OperationMgmtLimited = True Then

                        '666 OpenCloseCashDrawer(0)  'this 0 means this terminal only
                        '***********
                        'we never get rid of actingManager = Nothing
                        'because we need it to open cash drawer
                        ' maybe some wierd memory leak, don;t think so 
                    Else
                        actingManager = Nothing
                    End If
                Else
                    actingManager = Nothing
                End If
                currentTerminal.TerminalsOpenID = 0
            End If
        Catch ex As Exception
            currentTerminal.TerminalsOpenID = 0
        End Try

    End Sub

    Private Sub OpenAllCashDrawers()

        Dim oRow As DataRow
        Dim tRow As DataRow
        Dim isDrawerOpen As Boolean = False
        DetermineOpenCashDrawer(currentTerminal.CurrentDailyCode)

        For Each oRow In ds.Tables("TerminalsMethod").Rows
            If oRow("hasCashDrawer") = True Then
                If oRow("autoOpenDrawer") = True Then
                    For Each tRow In dsOrder.Tables("TermsOpen").Rows
                        If tRow("TerminalsPrimaryKey") = oRow("TerminalsPrimaryKey") Then
                            'we know drawer is open
                            isDrawerOpen = True
                            Exit For
                        End If
                    Next
                    If isDrawerOpen = False Then
                        OpenCashDrawer(oRow("normalOpenAmount"), oRow("TerminalsPrimaryKey"))
                    End If
                    isDrawerOpen = False
                End If
            End If
        Next

    End Sub

    Private Sub BegMenuRoutine()
        Dim oRow As DataRow
        Dim hasSecondMenu As Boolean = False

        For Each oRow In ds.Tables("MenuChoice").Rows
            If oRow("LastOrder") = 2 Then
                _sMenuID = oRow("MenuID")
                _sMenuName = oRow("MenuName")
                lblMenuSecond.Text = _sMenuName
                hasSecondMenu = True
            End If
            If oRow("LastOrder") = 1 Then
                _pMenuID = oRow("MenuID")
                _pMenuName = oRow("MenuName")
                lblMenuMain.Text = _pMenuName
                If hasSecondMenu = False Then
                    _sMenuID = oRow("MenuID")
                    _sMenuName = oRow("MenuName")
                    lblMenuSecond.Text = _sMenuName
                End If
            End If
        Next

    End Sub

    Private Sub OpenBusinessRoutine()

        Dim rowCount As Integer
        Dim oRow As DataRow
        openBusinessCount = 0

        DetermineOpenBusiness()

        rowCount = dsOrder.Tables("OpenBusiness").Rows.Count

        If rowCount = 0 Then
            btnOpenNewDaily.Visible = True
            btnOpenThisDaily.Visible = False
        Else
            btnOpenNewDaily.Visible = False
            btnOpenThisDaily.Visible = True

            If currentTerminal.CurrentDailyCode = 0 Then
                ' if we don't have a daily with this terminal
                ' then daily was opened at other terminal
                ' we will default to the first row, then swith with below button

                oRow = dsOrder.Tables("OpenBusiness").Rows(openBusinessCount)
                currentTerminal.CurrentDailyCode = oRow("DailyCode")
                If rowCount > 1 Then
                    btnSwitchDaily.Visible = True
                End If
                '***************************
                'now we need to test for menu and initial loading

            Else
                If rowCount = 1 Then
                    oRow = dsOrder.Tables("OpenBusiness").Rows(openBusinessCount)
                Else
                    btnSwitchDaily.Visible = True
                    For Each oRow In dsOrder.Tables("OpenBusiness").Rows
                        openBusinessCount += 1
                        If oRow("DailyCode") = currentTerminal.CurrentDailyCode Then Exit For
                    Next
                End If

            End If

            PopualteDailyInfo(oRow)

            '        PopulateQuickTicket()
            '       PerformEmployeeFunctions(emp)
        End If

    End Sub

    Private Sub PopualteDailyInfo(ByRef oRow As DataRow)

        If typeProgram = "Online_Demo" Then
            btnOpenThisDaily.Text = "Select Terminal Method on right"
        Else
            btnOpenThisDaily.Text = "Open Daily " & currentTerminal.CurrentDailyCode
        End If
        currentTerminal.DailyDate = Format(oRow("StartTime"), "d")
        currentTerminal.primaryMenuID = oRow("PrimaryMenu")
        currentTerminal.secondaryMenuID = oRow("SecondaryMenu")
        currentTerminal.CurrentShift = oRow("ShiftID")
        currentTerminal.initPrimaryMenuID = oRow("PrimaryMenu")
    End Sub

    Private Sub OpenDailyBusinessSelected() Handles selectDaily.DailySelected

        Dim openingInfo As OpenInfo

        openingInfo.dailyCode = selectDaily.sDailyCode
        openingInfo.pMenu = selectDaily.sPrimaryMenu
        openingInfo.sMenu = selectDaily.sSecondaryMenu
        currentTerminal.initPrimaryMenuID = selectDaily.sPrimaryMenu
        currentTerminal.currentPrimaryMenuID = selectDaily.sPrimaryMenu
        currentTerminal.currentSecondaryMenuID = selectDaily.sSecondaryMenu
        openingInfo.termMethod = selectDaily.chosenTermMethod

        If typeProgram = "Online_Demo" Then
            '444   currentTerminal = New Terminal
            GenerateOrderTables.CreateTerminals()
        End If

        RaiseEvent RestuarantOpen(openingInfo)

    End Sub

    Private Sub NewDaily_Selected() Handles selectDaily.NewDaily

        GenerateOrderTables.StartNewDaily()
        NewDailyStarted()
    End Sub

    Private Sub batchQuestionAccepted(ByVal sender As Object, ByVal e As System.EventArgs) Handles newBatchQuestion.AcceptInformation
        GenerateOrderTables.StartNewDaily()
        NewDailyStarted()

    End Sub

    Private Sub NewDailyStarted()
        '********** temp
        Dim openingInfo As OpenInfo

        openingInfo.dailyCode = currentTerminal.CurrentDailyCode
        openingInfo.pMenu = currentTerminal.primaryMenuID
        openingInfo.sMenu = currentTerminal.secondaryMenuID
        currentTerminal.initPrimaryMenuID = currentTerminal.primaryMenuID
        currentTerminal.currentPrimaryMenuID = currentTerminal.primaryMenuID
        openingInfo.termMethod = currentTerminal.TermMethod  'selectDaily.chosenTermMethod

        RaiseEvent RestuarantOpen(openingInfo)

    End Sub



    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click

        RaiseEvent ClosePOS()
        '    RaiseEvent OpeningScreenClosing()
        '   Me.Dispose()

    End Sub






    Private Sub lblMenu1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMenu1.Click
        SwitchPrimaryMenu()
    End Sub
    Private Sub lblMenuMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMenuMain.Click
        SwitchPrimaryMenu()
    End Sub

    Private Sub lblMenu2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMenu2.Click
        SwitchSecondaryMenu()
    End Sub

    Private Sub lblMenuSecond_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMenuSecond.Click
        SwitchSecondaryMenu()
    End Sub

    Private Sub SwitchPrimaryMenu()

        'do not allow to change menu here if open daily business
        ' should be done in management control
        If currentTerminal.CurrentDailyCode > 0 Then
            Exit Sub
        End If
        DetermineNextMenu(_pMenuID, True)
        lblMenuMain.Text = _pMenuName

    End Sub
    Private Sub SwitchSecondaryMenu()

        'do not allow to change menu here if open daily business
        ' should be done in management control
        If currentTerminal.CurrentDailyCode > 0 Then
            Exit Sub
        End If
        DetermineNextMenu(_sMenuID, False)
        lblMenuSecond.Text = _sMenuName

    End Sub

    Private Sub DetermineNextMenu(ByVal menuID As Integer, ByVal isPrimary As Boolean)
        Dim oRow As DataRow
        Dim i As Integer

        For Each oRow In ds.Tables("MenuChoice").Rows
            i += 1
            If oRow("MenuID") = menuID Then
                Exit For
            End If
        Next
        If i >= ds.Tables("MenuChoice").Rows.Count Then
            'this mean we start from the beginning
            i = 0
        End If
        oRow = ds.Tables("MenuChoice").Rows(i)

        If isPrimary = True Then
            _pMenuID = oRow("MenuID")
            _pMenuName = oRow("MenuName")
        Else
            _sMenuID = oRow("MenuID")
            _sMenuName = oRow("MenuName")
        End If

    End Sub




    Private Sub btnOpenNewDaily_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenNewDaily.Click

        Dim menuName As String

        If actingManager Is Nothing Then
            'thinking of something here
            'acting Mgr is inputing into new daily and cashDrawers 
        End If

        OpenBusinessRoutine() 'this will recheck for open business
        If dsOrder.Tables("OpenBusiness").Rows.Count > 0 Then
            btnOpenThisDaily_Click(sender, e)
            Exit Sub
        End If

        currentTerminal.primaryMenuID = _pMenuID
        currentTerminal.secondaryMenuID = _sMenuID

        LoadingMenuRoutine()

        'revert to the beginninng of menu day
        If Not currentTerminal.currentPrimaryMenuID = currentTerminal.primaryMenuID Then
            currentTerminal.currentPrimaryMenuID = currentTerminal.primaryMenuID
            currentTerminal.currentSecondaryMenuID = currentTerminal.secondaryMenuID
        End If
        currentTerminal.initPrimaryMenuID = currentTerminal.primaryMenuID

        'revert to first shift
        If ds.Tables("ShiftCodes").Rows.Count > 0 Then
            currentTerminal.CurrentShift = ds.Tables("ShiftCodes").Rows(0)("ShiftID")
        End If

        'open new daily
        'input currentMenuID's into DailyBusiness
        GenerateOrderTables.StartNewDaily()

        'open cash drawers
        OpenAllCashDrawers()

        'setprimarykeys
        SetUpPrimaryKeys()

        ' this may be me.dispose or not visible
        RaiseEvent RestaurantOpening()
        Me.Dispose()

    End Sub

    Private Sub LoadingMenuRoutine()

        Dim fString As String
        Dim xmlMenuDate As DateTime
        Dim needToLoadMenu As Boolean
        '      Dim currentMenu As Menu
        '     Dim secondaryMenu As Menu
        '    Dim menuName As String


        If typeProgram = "Online_Demo" Then
            If currentTerminal.TermMethod = "Quick" Then
                ds.ReadXml("Lunch_Dinner_QuickDemo.xml", XmlReadMode.ReadSchema)
            Else
                ds.ReadXml("Lunch_Dinner.xml", XmlReadMode.ReadSchema)
            End If
            ds.AcceptChanges()
            Exit Sub
        End If

        ' fString = "Lunch_Dinner.xml"
        fString = _pMenuName + "_" + _sMenuName ' + ".xml"

        Dim menuDateObj As System.IO.FileInfo
        '  Dim menuDateObj As New System.IO.FileInfo("c:\Data Files\spiderPOS\Menu\" & fString & ".xml")
        If pointToServer = False Then
            menuDateObj = New System.IO.FileInfo("c:\Data Files\spiderPOS\Menu\" & fString & ".xml")

        Else
            menuDateObj = New System.IO.FileInfo("\\" & ourServerName & "\Data Files\spiderPOS\Menu\" & fString & ".xml")

        End If
        xmlMenuDate = menuDateObj.LastWriteTime

        If ds.Tables("LocationOpening").Rows.Count > 0 Then
            If xmlMenuDate < ds.Tables("LocationOpening").Rows(0)("menuChangeDate") Then
                'this mean a changes to the menu have been made since last saving out xml Menu
                needToLoadMenu = True

            ElseIf My.Application.Info.Version.MinorRevision < ds.Tables("LocationOpening").Rows(0)("LastAppVersion") Then
                ' this means we revised app, so we might have to reload table structure
                needToLoadMenu = True
            Else
                Try
                    ' we do not want to do this if we changed table structure
                    ' it will delete columns in new table
                    '     connectionDown = New ConnectionDown_UC
                    '    connectionDown.MenuChoiceRoutine(fString & ".xml")
                Catch ex As Exception
                    MsgBox("May be a problem with your Backup Menu. Inform Spider POS.")
                    needToLoadMenu = True
                End Try
            End If
        Else
            needToLoadMenu = True
        End If

        If currentTerminal.TermMethod = "Quick" Then
            If dtQuickCategory.Rows.Count + dtQuickDrinkCategory.Rows.Count = 0 Then
                needToLoadMenu = True
            End If
        Else
            If dtBartenderCategory.Rows.Count + dtBartenderDrinkCategory.Rows.Count + dtMainCategory.Rows.Count + dtDrinkCategory.Rows.Count = 0 Then
                needToLoadMenu = True
            End If
        End If

        '     If currentTerminal.menuLoadedDate = Nothing Or (ds.Tables("MainCategory").Rows.Count + ds.Tables("DrinkCategory").Rows.Count = 0) Then
        'needToLoadMenu = True
        '   End If

        'if menu not loaded, or want update
        If needToLoadMenu = True Then
            If PopulateMenu(False) = True Then
                currentTerminal.menuLoadedDate = Now

                'we get this info ONLY from phoenix
                '      CreateMenuString(currentTerminal.primaryMenuID, menuName)
                '     If currentTerminal.secondaryMenuID > 0 Then
                '       menuName = menuName + "_"
                '      CreateMenuString(currentTerminal.secondaryMenuID, menuName)
                '    End If
                '    SaveBackupDATAds(menuName)
                SaveBackupDATAds(fString)
            End If
        End If

    End Sub
    Private Function CreateMenuString222(ByVal mc As Integer, ByRef menuName As String)

        Dim oRow As DataRow

        For Each oRow In ds.Tables("MenuChoice").Rows
            If oRow("MenuID") = mc Then
                menuName = menuName + oRow("MenuName")
            End If
        Next

    End Function

    Private Sub SaveBackupDATAds(ByVal menuName As String)

        Try
            If typeProgram = "Online_Demo" Then Exit Sub

            GenerateOrderTables.CreatespiderPOSDirectory()

            'starter menu is just a subset of ds dataset
            dsStarter.WriteXml("c:\Data Files\spiderPOS\StarterMenu.xml", XmlWriteMode.WriteSchema)
            ds.WriteXml("c:\Data Files\spiderPOS\Menu\" & menuName & ".xml", XmlWriteMode.WriteSchema)

            '***************
            '   need for Demo
            ' DO NOT DELETE below
            ' and only write to either QUickDemo or Lunch_Dinner
            '     dsStarter.WriteXml("StarterMenu.xml", XmlWriteMode.WriteSchema)
            '    ds.WriteXml("Lunch_Dinner_QuickDemo.xml", XmlWriteMode.WriteSchema)
            '     ds.WriteXml("Lunch_Dinner.xml", XmlWriteMode.WriteSchema)
            '   dsOrder.WriteXml("OrderData.xml", XmlWriteMode.WriteSchema)
            '  dsEmployee.WriteXml("EmployeeData.xml", XmlWriteMode.WriteSchema)
            ' dsCustomer.WriteXml("CustomerData.xml", XmlWriteMode.WriteSchema)


        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub btnOpenThisDaily_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenThisDaily.Click
        'below 2 is conditional
        If typeProgram = "Online_Demo" Then
            Exit Sub
        End If

        currentTerminal.CurrentMenuID = currentTerminal.primaryMenuID
        currentTerminal.currentPrimaryMenuID = currentTerminal.primaryMenuID

        If mainServerConnected = True Then
            'if FALSE, we already loaded the xml menu
            LoadingMenuRoutine()
            SetUpPrimaryKeys()
        End If
        If typeProgram = "Online_Demo" Then
            GenerateOrderTables.CreateTerminals()
        End If

        CashDrawerRoutine()

        RaiseEvent RestaurantOpening()
        Me.Dispose()
        '   Me.Visible = False


    End Sub









    Private Sub btnDailyBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDailyBar.Click
        Me.btnDailyTable.BackColor = Color.LightSlateGray
        Me.btnDailyBar.BackColor = Color.CornflowerBlue
        Me.btnDailyQuick.BackColor = Color.LightSlateGray
        currentTerminal.TermMethod = "Bar"

        TestForDemo("Bar")

    End Sub

    Private Sub btnDailyTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDailyTable.Click
        Me.btnDailyTable.BackColor = Color.CornflowerBlue
        Me.btnDailyBar.BackColor = Color.LightSlateGray
        Me.btnDailyQuick.BackColor = Color.LightSlateGray
        currentTerminal.TermMethod = "Table"

        TestForDemo("Table")

    End Sub

    Private Sub btnDailyQuick_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDailyQuick.Click
        Me.btnDailyTable.BackColor = Color.LightSlateGray
        Me.btnDailyBar.BackColor = Color.LightSlateGray
        Me.btnDailyQuick.BackColor = Color.CornflowerBlue
        currentTerminal.TermMethod = "Quick"

        TestForDemo("Quick")

    End Sub

    Private Sub TestForDemo(ByVal termMethod As String)

        If typeProgram = "Online_Demo" Then
            '        If dsOrder.Tables("OpenBusiness").Rows.Count > 0 Then '= 1 Then '
            'If currentTerminal.TermMethod = "Quick" Then
            'ds.ReadXml("Lunch_Dinner_QuickDemo.xml", XmlReadMode.ReadSchema)
            '     Else
            '        ds.ReadXml("Lunch_Dinner.xml", XmlReadMode.ReadSchema)
            '   End If
            'ds.AcceptChanges()
            '  End If

            '    currentTerminal.CurrentMenuID = currentTerminal.primaryMenuID
            '   currentTerminal.currentPrimaryMenuID = currentTerminal.primaryMenuID

            LoadingMenuRoutine()
            SetUpPrimaryKeys()
            GenerateOrderTables.CreateTerminals()
            currentTerminal.TermMethod = termMethod
            currentTerminal.currentPrimaryMenuID = currentTerminal.initPrimaryMenuID

            CashDrawerRoutine()

            RaiseEvent RestaurantOpening()
            Me.Dispose()

        End If

    End Sub

    Private Sub btnSwitchDaily_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSwitchDaily.Click

        Dim oRow As DataRow

        'openbusinessCount is zero baased
        If openBusinessCount < (dsOrder.Tables("OpenBusiness").Rows.Count - 1) Then
            openBusinessCount += 1
        Else
            openBusinessCount = 0
        End If

        oRow = dsOrder.Tables("OpenBusiness").Rows(openBusinessCount)
        currentTerminal.CurrentDailyCode = oRow("DailyCode")
        PopualteDailyInfo(oRow)

    End Sub

End Class
