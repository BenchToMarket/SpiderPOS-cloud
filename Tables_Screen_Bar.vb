Imports DataSet_Builder


Public Class Tables_Screen_Bar
    Inherits System.Windows.Forms.UserControl

    Dim testCOunt As Integer

    '   Dim sql As New DataSet_Builder.SQLHelper(connectserver)
    '    Dim keepActiveTimer As New Timer
    '   Dim keepActiveInteger As Integer

    '   Private tablesInactiveTimer As Timer
    '  Dim updateClockTimer As New Timer
    Dim tablesInactiveCounter As Integer = 1
    Dim WithEvents openInfo As DataSet_Builder.Information_UC

    '   Dim WithEvents SeatingChart As Seating_ChooseTable 'Seating_Dining
    Dim WithEvents SeatingTab222 As Seating_EnterTab
    'we use setaing tab in Login and maybe Customer Loyalty here
    Dim WithEvents ActiveOrder As term_OrderForm
    '   Dim WithEvents ManagementForm As Manager_Form
    Dim WithEvents clockInPanel As ClockInUserControl
    Dim WithEvents ClockingOutEmployee As ClockOut_UC
    Dim WithEvents nosaleLoginPad As NumberPad
    Dim isNoSaleOrClockOut As String
    Dim ccDisplay As CashClose_UC
    Dim lastTablePanel As Integer
    Dim quickEndCount As Integer = 0

    '   Dim blankPanel As New Panel

    Friend _IsBartenderMode As Boolean
    '    Dim IsOneBartender As Boolean
    Dim NumBar As Integer
    'this is because some reason the system resizes pnlAvailTabs control
    Dim needToCorrectRoundingError As Boolean = True

    Event ManagementButton(ByRef sender As Employee)
    Event ExitingTableScreen()    'CheckDataDaseConnection()
    Event FireOrderScreen(ByRef tabAccountInfo As DataSet_Builder.Payment)
    Event FireSeatingChart(ByVal fromMgmt As Boolean)
    Event FireSeatingTab(ByVal startedFrom As String, ByVal tn As String) ' As Boolean)
    Event QuickTicketStart(ByVal experienceNumber As Int64) 'ByVal tabID As Int64, ByVal tabName As String, 

    Friend Property IsBartenderMode() As Boolean
        Get
            Return _IsBartenderMode
        End Get
        Set(ByVal Value As Boolean)
            _IsBartenderMode = Value
        End Set
    End Property

#Region " Windows Form Designer generated code "

    Public Sub New() 'ByRef emp As Employee, ByVal _IsBartenderMode As Boolean) ', ByVal barmanID As Integer)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        '   InitializeOther(emp.Bartender)  '(IsBartenderMode, barmanID)
        needToCorrectRoundingError = False

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
    Friend WithEvents pnlAvailTabs As System.Windows.Forms.Panel
    Friend WithEvents pnlBartenderButtons As System.Windows.Forms.Panel
    Friend WithEvents btnAddTable As System.Windows.Forms.Button
    Friend WithEvents btnAddTab As System.Windows.Forms.Button
    Friend WithEvents btnAddTabGroup As System.Windows.Forms.Button
    Friend WithEvents pnlTableCommands As System.Windows.Forms.Panel
    Friend WithEvents btnBartenderLogin As System.Windows.Forms.Button
    Friend WithEvents btnTablesExit As System.Windows.Forms.Button
    Friend WithEvents btnManager As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnClockOut As System.Windows.Forms.Button
    Friend WithEvents lblTablesName As System.Windows.Forms.Label
    Friend WithEvents lblTableTime As System.Windows.Forms.Label
    Friend WithEvents btnNoSale As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Tables_Screen_Bar))
        Me.pnlAvailTabs = New System.Windows.Forms.Panel
        Me.pnlBartenderButtons = New System.Windows.Forms.Panel
        Me.lblTablesName = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnAddTable = New System.Windows.Forms.Button
        Me.btnAddTab = New System.Windows.Forms.Button
        Me.btnAddTabGroup = New System.Windows.Forms.Button
        Me.pnlTableCommands = New System.Windows.Forms.Panel
        Me.btnClockOut = New System.Windows.Forms.Button
        Me.btnManager = New System.Windows.Forms.Button
        Me.btnTablesExit = New System.Windows.Forms.Button
        Me.btnBartenderLogin = New System.Windows.Forms.Button
        Me.btnNoSale = New System.Windows.Forms.Button
        Me.lblTableTime = New System.Windows.Forms.Label
        Me.pnlTableCommands.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlAvailTabs
        '
        Me.pnlAvailTabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlAvailTabs.BackColor = System.Drawing.Color.Black
        Me.pnlAvailTabs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAvailTabs.Location = New System.Drawing.Point(56, 96)
        Me.pnlAvailTabs.Name = "pnlAvailTabs"
        Me.pnlAvailTabs.Size = New System.Drawing.Size(864, 550)
        Me.pnlAvailTabs.TabIndex = 0
        '
        'pnlBartenderButtons
        '
        Me.pnlBartenderButtons.BackColor = System.Drawing.Color.Transparent
        Me.pnlBartenderButtons.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlBartenderButtons.Location = New System.Drawing.Point(0, 0)
        Me.pnlBartenderButtons.Name = "pnlBartenderButtons"
        Me.pnlBartenderButtons.Size = New System.Drawing.Size(1024, 80)
        Me.pnlBartenderButtons.TabIndex = 1
        '
        'lblTablesName
        '
        Me.lblTablesName.BackColor = System.Drawing.Color.Transparent
        Me.lblTablesName.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTablesName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblTablesName.Location = New System.Drawing.Point(16, 8)
        Me.lblTablesName.Name = "lblTablesName"
        Me.lblTablesName.Size = New System.Drawing.Size(312, 32)
        Me.lblTablesName.TabIndex = 1
        Me.lblTablesName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button1
        '
        Me.Button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button1.Location = New System.Drawing.Point(176, 24)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(120, 48)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Test RealTIme"
        Me.Button1.Visible = False
        '
        'btnAddTable
        '
        Me.btnAddTable.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnAddTable.BackColor = System.Drawing.Color.SlateGray
        Me.btnAddTable.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddTable.Location = New System.Drawing.Point(880, 210)
        Me.btnAddTable.Name = "btnAddTable"
        Me.btnAddTable.Size = New System.Drawing.Size(120, 56)
        Me.btnAddTable.TabIndex = 2
        Me.btnAddTable.Text = "Add Table"
        Me.btnAddTable.UseVisualStyleBackColor = False
        '
        'btnAddTab
        '
        Me.btnAddTab.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnAddTab.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnAddTab.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddTab.Location = New System.Drawing.Point(880, 290)
        Me.btnAddTab.Name = "btnAddTab"
        Me.btnAddTab.Size = New System.Drawing.Size(120, 56)
        Me.btnAddTab.TabIndex = 3
        Me.btnAddTab.Text = "Add Tab"
        Me.btnAddTab.UseVisualStyleBackColor = False
        '
        'btnAddTabGroup
        '
        Me.btnAddTabGroup.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnAddTabGroup.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnAddTabGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddTabGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAddTabGroup.Location = New System.Drawing.Point(880, 370)
        Me.btnAddTabGroup.Name = "btnAddTabGroup"
        Me.btnAddTabGroup.Size = New System.Drawing.Size(120, 56)
        Me.btnAddTabGroup.TabIndex = 4
        Me.btnAddTabGroup.Text = "Add Tab Group"
        Me.btnAddTabGroup.UseVisualStyleBackColor = False
        '
        'pnlTableCommands
        '
        Me.pnlTableCommands.BackColor = System.Drawing.Color.Transparent
        Me.pnlTableCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTableCommands.Controls.Add(Me.btnClockOut)
        Me.pnlTableCommands.Controls.Add(Me.btnManager)
        Me.pnlTableCommands.Controls.Add(Me.btnTablesExit)
        Me.pnlTableCommands.Controls.Add(Me.btnBartenderLogin)
        Me.pnlTableCommands.Controls.Add(Me.Button1)
        Me.pnlTableCommands.Controls.Add(Me.btnNoSale)
        Me.pnlTableCommands.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlTableCommands.Location = New System.Drawing.Point(0, 688)
        Me.pnlTableCommands.Name = "pnlTableCommands"
        Me.pnlTableCommands.Size = New System.Drawing.Size(1024, 80)
        Me.pnlTableCommands.TabIndex = 5
        '
        'btnClockOut
        '
        Me.btnClockOut.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnClockOut.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnClockOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClockOut.Location = New System.Drawing.Point(879, 6)
        Me.btnClockOut.Name = "btnClockOut"
        Me.btnClockOut.Size = New System.Drawing.Size(112, 64)
        Me.btnClockOut.TabIndex = 4
        Me.btnClockOut.Text = "Clock Out"
        Me.btnClockOut.UseVisualStyleBackColor = False
        '
        'btnManager
        '
        Me.btnManager.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnManager.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnManager.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnManager.Location = New System.Drawing.Point(315, 6)
        Me.btnManager.Name = "btnManager"
        Me.btnManager.Size = New System.Drawing.Size(120, 64)
        Me.btnManager.TabIndex = 3
        Me.btnManager.Text = "Manager"
        Me.btnManager.UseVisualStyleBackColor = False
        '
        'btnTablesExit
        '
        Me.btnTablesExit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTablesExit.BackColor = System.Drawing.Color.SlateGray
        Me.btnTablesExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTablesExit.Location = New System.Drawing.Point(32, 7)
        Me.btnTablesExit.Name = "btnTablesExit"
        Me.btnTablesExit.Size = New System.Drawing.Size(112, 64)
        Me.btnTablesExit.TabIndex = 2
        Me.btnTablesExit.Text = "Exit"
        Me.btnTablesExit.UseVisualStyleBackColor = False
        '
        'btnBartenderLogin
        '
        Me.btnBartenderLogin.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnBartenderLogin.BackColor = System.Drawing.Color.SlateGray
        Me.btnBartenderLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBartenderLogin.Location = New System.Drawing.Point(742, 7)
        Me.btnBartenderLogin.Name = "btnBartenderLogin"
        Me.btnBartenderLogin.Size = New System.Drawing.Size(112, 64)
        Me.btnBartenderLogin.TabIndex = 0
        Me.btnBartenderLogin.Text = "Clock In"
        Me.btnBartenderLogin.UseVisualStyleBackColor = False
        Me.btnBartenderLogin.Visible = False
        '
        'btnNoSale
        '
        Me.btnNoSale.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnNoSale.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnNoSale.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNoSale.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNoSale.Location = New System.Drawing.Point(606, 7)
        Me.btnNoSale.Name = "btnNoSale"
        Me.btnNoSale.Size = New System.Drawing.Size(112, 64)
        Me.btnNoSale.TabIndex = 5
        Me.btnNoSale.Text = "No Sale"
        Me.btnNoSale.UseVisualStyleBackColor = False
        '
        'lblTableTime
        '
        Me.lblTableTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTableTime.BackColor = System.Drawing.Color.Transparent
        Me.lblTableTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTableTime.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblTableTime.Location = New System.Drawing.Point(872, 624)
        Me.lblTableTime.Name = "lblTableTime"
        Me.lblTableTime.Size = New System.Drawing.Size(120, 48)
        Me.lblTableTime.TabIndex = 6
        Me.lblTableTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tables_Screen_Bar
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.Controls.Add(Me.lblTablesName)
        Me.Controls.Add(Me.lblTableTime)
        Me.Controls.Add(Me.pnlTableCommands)
        Me.Controls.Add(Me.btnAddTabGroup)
        Me.Controls.Add(Me.btnAddTab)
        Me.Controls.Add(Me.btnAddTable)
        Me.Controls.Add(Me.pnlBartenderButtons)
        Me.Controls.Add(Me.pnlAvailTabs)
        Me.Name = "Tables_Screen_Bar"
        Me.Size = New System.Drawing.Size(1024, 768)
        Me.pnlTableCommands.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Friend Sub InitializeOther(ByVal IsBar As Boolean)
      
        '    If IsBar = True And usingBartenderMethod = True Then
        '    IsBartenderMode = True
        '   Else
        '      IsBartenderMode = False
        ' End If

        '       blankPanel = New Panel
        '      blankPanel.Location = New Point(0, 0)
        '     blankPanel.Size = New Size(1024, 768)
        '    Me.Controls.Add(blankPanel)
        '   Me.blankPanel.Visible = False

        '      NumBar = CalculateNumberOfBartenders()
        '     If NumBar > 1 Then
        '    IsOneBartender = False
        '   End If

        '       Me.ClientSize = New Size(ssX, ssY)
        '      Me.pnlAvailTabs.Size = New Size(ssX * 0.85, ssY * 0.67)
        updateClockTimer = New Timer
        updateClockTimer.Interval = 60000

        If IsBartenderMode = False And Not currentTerminal.TermMethod = "Quick" Then
            tablesInactiveCounter = 1
            tablesInactiveTimer = New Timer
            AddHandler tablesInactiveTimer.Tick, AddressOf TablesInactiveScreenTimeout
            tablesInactiveTimer.Interval = timeoutInterval
            tablesInactiveTimer.Start()
            '   Else
            '      currentTerminal.IsOneBartender = True
        End If

        InitializeScreen()

    End Sub

    '    Private Sub KeepActive(ByVal sender As Object, ByVal e As System.EventArgs)
    '        keepActiveInteger += 1
    '    End Sub

    Friend Sub InitializeScreen()

        PopulateAllTablesWithStatus(False)

        If IsBartenderMode = True Then
            DisplayBartenderButtons()
            TableAndTabButtons()
            pnlBartenderButtons.Visible = True
            btnBartenderLogin.Visible = True
            lblTablesName.Visible = False
        Else
            pnlBartenderButtons.Visible = False
            lblTablesName.Visible = True
            lblTablesName.Text = currentServer.FullName
            '      tablesInactiveCounter = 1
            If Not currentTerminal.TermMethod = "Quick" Then
                AddHandler tablesInactiveTimer.Tick, AddressOf TablesInactiveScreenTimeout
                '     tablesInactiveTimer.Interval = timeoutInterval
                tablesInactiveTimer.Start()
            Else
                btnBartenderLogin.Visible = True
            End If

        End If
        '    MsgBox(Me.pnlAvailTabs.Height)

        If currentServer.Cashier = True Or currentServer.Bartender = True Or currentServer.Manager = True Then
            Me.btnNoSale.Visible = True
        Else
            Me.btnNoSale.Visible = False
        End If
        '444       If currentServer.Bartender = False And currentServer.Manager = False Then
        If IsBartenderMode = False Then 'And Not currentTerminal.TermMethod = "Quick" Then
            'if one is true let us perform
            btnAddTabGroup.Visible = False
        End If
        If currentTerminal.TermMethod = "Quick" Then
            If dsOrder.Tables("QuickTickets").Rows.Count > 0 Then
                btnAddTab.Visible = False
            Else
                btnAddTab.Text = "Start Tabs"
            End If
        End If
        If ds.Tables("TermsTables").Rows.Count = 0 Then
            btnAddTable.Visible = False
        End If

        GenerateOrderTables.PopulateTabsAndTables(currentServer, currentTerminal.CurrentDailyCode, IsBartenderMode, currentTerminal.IsOneBartender, currentBartenders)
        CreateDataViews(currentServer.EmployeeID, True)
        DisplayTabsAndTables(quickEndCount)
        SetTableScreenTime()
        AddHandler updateClockTimer.Tick, AddressOf UpdateTableScreenTime
        updateClockTimer.Start()


    End Sub

    Private Sub TablesInactiveScreenTimeout(ByVal sender As Object, ByVal e As System.EventArgs)

        tablesInactiveCounter += 1

        If tablesInactiveCounter = companyInfo.timeoutMultiplier Then       '14 Then
            '         updateClockTimer.Dispose()
            '        tablesInactiveTimer.Dispose()
            RaiseEvent ExitingTableScreen()
        End If

    End Sub

    Private Sub StopTablesTimer()

        If IsBartenderMode = False And Not currentTerminal.TermMethod = "Quick" Then
            tablesInactiveTimer.Stop()
            RemoveHandler tablesInactiveTimer.Tick, AddressOf TablesInactiveScreenTimeout
        End If

        updateClockTimer.Stop()
        RemoveHandler updateClockTimer.Tick, AddressOf UpdateTableScreenTime

    End Sub

    Private Sub UpdatingTableData222(ByVal emp As Employee, ByRef ccDisplay As CashClose_UC) Handles ActiveOrder.TermOrder_Disposing  ', ManagementForm.UpdatingAfterTransfer
        Dim info As DataSet_Builder.Information_UC
        info = New DataSet_Builder.Information_UC("Attempting to Reconnect To Server")

        If mainServerConnected = False Then
            info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            Me.Controls.Add(info)
            info.BringToFront()

        End If

        '   *******
        '   having problem with thread
        '   would like to stop here until info is displayed

        '      ActiveOrder.Visible = False
        '     Me.ActiveOrder.ReInitializeOrderView()
        '    GenerateOrderTables.SaveOpenOrderData()
        '   dsOrder.Tables("OpenOrders").Rows.Clear()
        '  GenerateOrderTables.ReleaseCurrentlyHeld()
        ' currentTable = Nothing
        'Exit Sub


        ActiveOrder.Dispose()
        ActiveOrder = Nothing
        '   ActiveOrder.Dispose()
        GenerateOrderTables.ReleaseCurrentlyHeld()
        GenerateOrderTables.SaveOpenOrderData()
        '       dsOrder.Tables("OpenOrders").Rows.Clear()
        currentTable = Nothing
        '   don't need here if we save every time we status change
        '       GenerateOrderTables.SaveESCStatusChangeData()
        If Not ccDisplay Is Nothing Then
            ccDisplay.Location = New Point((Me.Width - ccDisplay.Width) / 2, (Me.Height - ccDisplay.Height) / 2)
            Me.Controls.Add(ccDisplay)
            ccDisplay.BringToFront()
        End If

        InitializeScreen()
        tablesInactiveCounter = 10
        info.Dispose()

        '      MsgBox("111111111")
        '     GC.Collect()
        '    MsgBox("2222222222")


        '******************
        '    maybe this is where we update experience table

    End Sub

    '    Private Function CalculateNumberOfBartenders222()
    '       Dim NumberOfBartenders As Integer
    '      NumberOfBartenders = currentBartenders.Count
    '       Return NumberOfBartenders
    '   End Function

    Private Sub DisplayBartenderButtons()
      
        Dim NumBar As Integer
        Dim barMan As Employee
        Dim w As Integer = 112
        Dim h As Integer = 64
        Dim x As Integer = 2 * buttonSpace
        Dim y As Integer = 2 * buttonSpace
        Dim index As Integer
        Dim backColorBarButton As Color

        pnlBartenderButtons.Controls.Clear()

        For Each barMan In loggedInBartenders   'currentBartenders

            If barMan.EmployeeID = currentServer.EmployeeID Then
                backColorBarButton = c9
            Else
                backColorBarButton = c7
            End If

            AddBartenderButton(x, y, w, h, barMan.NickName, barMan.EmployeeID, backColorBarButton)
            x = x + w + (2 * buttonSpace)
        Next

        '   ALL Button
        x = Me.Width - (w + (6 * buttonSpace))

        If currentTerminal.IsOneBartender = False Then
            backColorBarButton = c9
        Else
            backColorBarButton = c7
        End If

        AddBartenderButton(x, y, w, h, "All", -1, backColorBarButton)

    End Sub

    Private Sub AddBartenderButton(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer, ByVal barButtonText As String, ByVal barButtonID As Integer, ByVal barBackColor As Color)
        Dim btnBartenders As KitchenButton

        btnBartenders = New KitchenButton(barButtonText, w, h, barBackColor, c2)
        btnBartenders.Location = New Point(x, y)
        btnBartenders.ID = barButtonID
        '       btnBartenders.ForeColor = c3

        AddHandler btnBartenders.Click, AddressOf Bartender_Click
        Me.pnlBartenderButtons.Controls.Add(btnBartenders)

    End Sub

    Private Sub TableAndTabButtons()
        Me.btnAddTab.BackColor = System.Drawing.Color.SlateGray
        Me.btnAddTab.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.btnAddTabGroup.BackColor = System.Drawing.Color.SlateGray
        Me.btnAddTabGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

    End Sub

    Private Sub DisplayTabsAndTables(ByVal endCount As Integer)

        Me.pnlAvailTabs.Controls.Clear()

        Dim vRow As DataRowView
        Dim dv As DataView
        Dim index As Integer
        Dim counterIndex As Integer = 1
        Dim startingCount As Integer = 0
        Dim w As Single
        Dim h As Single
        Dim x As Single = buttonSpace
        Dim y As Single = buttonSpace
        Dim ls As String
        Dim lsTime As TimeSpan
        Dim timeAtTable As TimeSpan
        Dim altMethodUse As String = ""
        Dim numberPanelsInColumn As Integer


        If Not currentTerminal.TermMethod = "Quick" Then
            '   Also it is using a smaller pnlAvailTabs.Height the first time bring up Tables_Screen
            '   probvably doesn't matter
            '   somewhere a rounding error???
            w = (Me.pnlAvailTabs.Width - (5 * buttonSpace)) / 4
            h = (Me.pnlAvailTabs.Height - (9 * buttonSpace)) / 8
            numberPanelsInColumn = 8

            '      ReDim btnTabsAndTables(dsOrder.Tables("AvailTables").Rows.Count + dsOrder.Tables("AvailTabs").Rows.Count - 1)
            If needToCorrectRoundingError = True Then
                h -= 1.5
            End If

            If endCount > 32 Then
                startingCount = endCount - 32
            Else
                startingCount = 0
            End If


            For Each vRow In dvAvailTables
                If (index >= startingCount And index <= endCount) Or endCount = 0 Then
                    'test         MsgBox(vRow("TableNumber"))
                    'test    MsgBox(vRow("ExperienceNumber"))

                    ls = AssignCurrentStatusString(vRow("LastStatus"))
                    lsTime = DetermineTimeSpan(vRow("LastStatusTime"))
                    timeAtTable = DetermineTimeSpan(vRow("ExperienceDate"))

                    Try
                        If Not dvTerminalsUseOrder(0)("MethodUse") = vRow("MethodUse") Then
                            altMethodUse = vRow("MethodUse")
                        Else
                            altMethodUse = ""
                        End If
                        Dim btnTabsAndTables As New DataSet_Builder.AvailTableUserControl(False, vRow("TableNumber"), Nothing, vRow("TabName").ToString, vRow("TicketNumber"), vRow("LastStatus"), ls, lsTime, timeAtTable, Nothing, vRow("ItemsOnHold"), currentTerminal.TermMethod, altMethodUse)
                        '            btnTabsAndTables(index) = New DataSet_Builder.AvailTableUserControl(vRow("TableNumber"), vRow("TabName").ToString, vRow("LastStatus"), ls, lsTime, timeAtTable) 'AvailTableButton
                        With btnTabsAndTables
                            .SatTime = vRow("ExperienceDate")
                            .Text = vRow("TabName") 'vRow("TableNumber")
                            .NumberOfCustomers = vRow("NumberOfCustomers")
                            .NumberOfChecks = vRow("NumberOfChecks")
                            .ExperienceNumber = vRow("ExperienceNumber")
                            .EmpID = vRow("EmployeeID")
                            .CurrentMenu = vRow("MenuID")
                            .Size = New Size(w, h)
                            .Location = New Point(x, y)

                            '        .BackColor = c7
                            '       .ForeColor = c3
                            AddHandler btnTabsAndTables.TableClicked, AddressOf Tables_Click
                        End With
                        Me.pnlAvailTabs.Controls.Add(btnTabsAndTables)
                        '*****         btnTabsAndTables(index).ShowTableStatistics()
                    Catch ex As Exception

                    End Try

                    If counterIndex = numberPanelsInColumn Then
                        y = buttonSpace
                        x += w + buttonSpace
                        counterIndex = 0    'must restart at zero b/c we add 1 right away
                    Else
                        y += h + buttonSpace
                    End If
                    index += 1
                    counterIndex += 1
                Else
                    index += 1
                End If

            Next

            For Each vRow In dvAvailTabs
                If (index > startingCount And index <= endCount) Or endCount = 0 Then
                    ls = AssignCurrentStatusString(vRow("LastStatus"))
                    lsTime = DetermineTimeSpan(vRow("LastStatusTime"))

                    timeAtTable = DetermineTimeSpan(vRow("ExperienceDate"))

                    Try
                        If Not dvTerminalsUseOrder(0)("MethodUse") = vRow("MethodUse") Then
                            altMethodUse = vRow("MethodUse")
                        Else
                            altMethodUse = ""
                        End If
                        Dim btnTabsAndTables As New DataSet_Builder.AvailTableUserControl(True, Nothing, vRow("TabID"), vRow("TabName"), vRow("TicketNumber"), vRow("LastStatus"), ls, lsTime, timeAtTable, Nothing, vRow("ItemsOnHold"), currentTerminal.TermMethod, altMethodUse)
                        '      Dim btnTabsAndTables As New DataSet_Builder.AvailTableUserControl(True, Nothing, 0, vRow("TabName"), vRow("TicketNumber"), vRow("LastStatus"), ls, lsTime, timeAtTable, Nothing, vRow("ItemsOnHold"))
                        With btnTabsAndTables
                            .SatTime = vRow("ExperienceDate")
                            .Text = vRow("TabName")
                            .NumberOfCustomers = vRow("NumberOfCustomers")
                            .NumberOfChecks = vRow("NumberOfChecks")
                            .ExperienceNumber = vRow("ExperienceNumber")
                            .EmpID = vRow("EmployeeID")
                            .CurrentStatus = AssignCurrentStatusString(vRow("LastStatus"))
                            .CurrentMenu = vRow("MenuID")
                            .TabID = vRow("TabID")
                            .Size = New Size(w, h)
                            .Location = New Point(x, y)
                            '             .BackColor = c7
                            '            .ForeColor = c3
                            AddHandler btnTabsAndTables.TableClicked, AddressOf Tabs_Click
                        End With
                        Me.pnlAvailTabs.Controls.Add(btnTabsAndTables)
                    Catch ex As Exception

                    End Try


                    If counterIndex = numberPanelsInColumn Then
                        y = buttonSpace
                        x += w + buttonSpace
                        counterIndex = 0    'must restart at zero b/c we add 1 right away
                    Else
                        y += h + buttonSpace
                    End If
                    index += 1
                    counterIndex += 1
                Else
                    index += 1
                End If

            Next

            For Each vRow In dvTransferTables
                If (index > startingCount And index <= endCount) Or endCount = 0 Then
                    ls = AssignCurrentStatusString(vRow("LastStatus"))
                    lsTime = DetermineTimeSpan(vRow("LastStatusTime"))

                    timeAtTable = DetermineTimeSpan(vRow("ExperienceDate"))

                    Try
                        If Not dvTerminalsUseOrder(0)("MethodUse") = vRow("MethodUse") Then
                            altMethodUse = vRow("MethodUse")
                        Else
                            altMethodUse = ""
                        End If
                        Dim btnTabsAndTables As New DataSet_Builder.AvailTableUserControl(False, 0, Nothing, vRow("TabName").ToString, vRow("TicketNumber"), vRow("LastStatus"), ls, lsTime, timeAtTable, Nothing, vRow("ItemsOnHold"), currentTerminal.TermMethod, altMethodUse)
                        '      Dim btnTabsAndTables As New DataSet_Builder.AvailTableUserControl(False, vrow("TableNumber") , Nothing, vRow("TabName").ToString, vRow("TicketNumber"), vRow("LastStatus"), ls, lsTime, timeAtTable, Nothing, vRow("ItemsOnHold"))
                        With btnTabsAndTables
                            .SatTime = vRow("ExperienceDate")
                            .Text = vRow("TabName") 'vRow("TableNumber")
                            .NumberOfCustomers = vRow("NumberOfCustomers")
                            .NumberOfChecks = vRow("NumberOfChecks")
                            .ExperienceNumber = vRow("ExperienceNumber")
                            .EmpID = vRow("EmployeeID")
                            .CurrentStatus = AssignCurrentStatusString(vRow("LastStatus"))
                            .CurrentMenu = vRow("MenuID")
                            .Size = New Size(w, h)
                            .Location = New Point(x, y)
                            '               .BackColor = c1
                            '              .ForeColor = c2
                            AddHandler btnTabsAndTables.TableClicked, AddressOf Tables_Click
                        End With
                        Me.pnlAvailTabs.Controls.Add(btnTabsAndTables)
                    Catch ex As Exception

                    End Try

                    If counterIndex = numberPanelsInColumn Then
                        y = buttonSpace
                        x += w + buttonSpace
                        counterIndex = 0    'must restart at zero b/c we add 1 right away
                    Else
                        y += h + buttonSpace
                    End If
                    index += 1
                    counterIndex += 1
                Else
                    index += 1
                End If

            Next

            '   *** should probably use tab name
            For Each vRow In dvTransferTabs
                If (index > startingCount And index <= endCount) Or endCount = 0 Then
                    ls = AssignCurrentStatusString(vRow("LastStatus"))
                    lsTime = DetermineTimeSpan(vRow("LastStatusTime"))
                    timeAtTable = DetermineTimeSpan(vRow("ExperienceDate"))

                    Try
                        If Not dvTerminalsUseOrder(0)("MethodUse") = vRow("MethodUse") Then
                            altMethodUse = vRow("MethodUse")
                        Else
                            altMethodUse = ""
                        End If
                        Dim btnTabsAndTables As New DataSet_Builder.AvailTableUserControl(True, Nothing, vRow("TabID"), vRow("TabName"), vRow("TicketNumber"), vRow("LastStatus"), ls, lsTime, timeAtTable, Nothing, vRow("ItemsOnHold"), currentTerminal.TermMethod, altMethodUse)
                        '      Dim btnTabsAndTables As New DataSet_Builder.AvailTableUserControl(True, Nothing, vRow("TableNumber"), vRow("TabName"), vRow("TicketNumber"), vRow("LastStatus"), ls, lsTime, timeAtTable, Nothing, vRow("ItemsOnHold"))
                        With btnTabsAndTables
                            .SatTime = vRow("ExperienceDate")
                            .Text = vRow("TabName") ' vRow("TableNumber")
                            .NumberOfCustomers = vRow("NumberOfCustomers")
                            .NumberOfChecks = vRow("NumberOfChecks")
                            .ExperienceNumber = vRow("ExperienceNumber")
                            .EmpID = vRow("EmployeeID")
                            .CurrentStatus = AssignCurrentStatusString(vRow("LastStatus"))
                            .CurrentMenu = vRow("MenuID")
                            .Size = New Size(w, h)
                            .Location = New Point(x, y)
                            '           .BackColor = c1
                            '          .ForeColor = c2
                            AddHandler btnTabsAndTables.TableClicked, AddressOf Tables_Click
                        End With
                        Me.pnlAvailTabs.Controls.Add(btnTabsAndTables)
                    Catch ex As Exception

                    End Try

                    If counterIndex = numberPanelsInColumn Then
                        y = buttonSpace
                        x += w + buttonSpace
                        counterIndex = 0    'must restart at zero b/c we add 1 right away
                    Else
                        y += h + buttonSpace
                    End If
                    index += 1
                    counterIndex += 1
                Else
                    index += 1
                End If

            Next

            If dsOrder.Tables("QuickTickets").Rows.Count > 0 Then

                For Each dv In currentQuickTicketDataViews
                    If (index > startingCount And index <= endCount) Or endCount = 0 Then
                        'this is the last row in each of the DataView Collections
                        'the dataviews are grouped by bartender

                        '     If currentTerminal.TermMethod = "Quick" Then
                        '       If quickEndCount < 12 Then
                        '      quickEndCount = (dv.Count - 1)
                        '     Else
                        '    quickEndCount -= 12
                        '    End If
                        '        DisplayTIcketsForQuick(dv, dv.Count - 1)
                        '       Exit For
                        '     End If

                        'this was supposed to show alternative methods on Table pad
                        'does not work for Beth's Tabs, b/c multiple dataviews
                        'probably not needed
                        '         If Not dvTerminalsUseOrder(0)("MethodUse") = vRow("MethodUse") Then
                        '        altMethodUse = vRow("MethodUse")
                        '       Else
                        altMethodUse = ""
                        '      End If

                        If dv.Count > 0 Then
                            vRow = dv(dv.Count - 1)


                            If currentTerminal.IsOneBartender = True Then
                                If vRow("EmployeeID") = currentServer.EmployeeID Then
                                    ls = AssignCurrentStatusString(vRow("LastStatus"))
                                    lsTime = DetermineTimeSpan(vRow("LastStatusTime"))

                                    timeAtTable = DetermineTimeSpan(vRow("ExperienceDate"))

                                    Try

                                        Dim btnTabsAndTables As New DataSet_Builder.AvailTableUserControl(True, 0, 0, vRow("TabName"), vRow("TicketNumber"), vRow("LastStatus"), ls, lsTime, timeAtTable, Nothing, 0, currentTerminal.TermMethod, altMethodUse)
                                        '        btnTabsAndTables(index) = New DataSet_Builder.AvailTableUserControl(0, vRow("TabName"), vRow("LastStatus"), ls, lsTime, timeAtTable) 'AvailTableButton
                                        With btnTabsAndTables
                                            .SatTime = vRow("ExperienceDate")
                                            .Text = vRow("TicketNumber")    'vRow("TabName")
                                            .NumberOfCustomers = vRow("NumberOfCustomers")
                                            .NumberOfChecks = vRow("NumberOfChecks")
                                            .ExperienceNumber = vRow("ExperienceNumber")
                                            .EmpID = vRow("EmployeeID")
                                            .CurrentStatus = AssignCurrentStatusString(vRow("LastStatus"))
                                            .CurrentMenu = vRow("MenuID")
                                            .TabID = vRow("TabID")
                                            .Size = New Size(w, h)
                                            .Location = New Point(x, y)
                                            '             .BackColor = c7
                                            '            .ForeColor = c3
                                            AddHandler btnTabsAndTables.TableClicked, AddressOf Tabs_Click
                                        End With
                                        Me.pnlAvailTabs.Controls.Add(btnTabsAndTables)
                                    Catch ex As Exception

                                    End Try

                                    If counterIndex = numberPanelsInColumn Then
                                        y = buttonSpace
                                        x += w + buttonSpace
                                        counterIndex = 0    'must restart at zero b/c we add 1 right away
                                    Else
                                        y += h + buttonSpace
                                    End If
                                    index += 1
                                    counterIndex += 1
                                End If
                            Else
                                ls = AssignCurrentStatusString(vRow("LastStatus"))
                                lsTime = DetermineTimeSpan(vRow("LastStatusTime"))

                                timeAtTable = DetermineTimeSpan(vRow("ExperienceDate"))

                                Try

                                    Dim btnTabsAndTables As New DataSet_Builder.AvailTableUserControl(True, 0, 0, vRow("TabName"), vRow("TicketNumber"), vRow("LastStatus"), ls, lsTime, timeAtTable, Nothing, 0, currentTerminal.TermMethod, altMethodUse)
                                    '        btnTabsAndTables(index) = New DataSet_Builder.AvailTableUserControl(0, vRow("TabName"), vRow("LastStatus"), ls, lsTime, timeAtTable) 'AvailTableButton
                                    With btnTabsAndTables
                                        .SatTime = vRow("ExperienceDate")
                                        .Text = vRow("TicketNumber") 'vRow("TabName")
                                        .NumberOfCustomers = vRow("NumberOfCustomers")
                                        .NumberOfChecks = vRow("NumberOfChecks")
                                        .ExperienceNumber = vRow("ExperienceNumber")
                                        .EmpID = vRow("EmployeeID")
                                        .CurrentStatus = AssignCurrentStatusString(vRow("LastStatus"))
                                        .CurrentMenu = vRow("MenuID")
                                        .TabID = vRow("TabID")
                                        .Size = New Size(w, h)
                                        .Location = New Point(x, y)
                                        '             .BackColor = c7
                                        '            .ForeColor = c3
                                        AddHandler btnTabsAndTables.TableClicked, AddressOf Tabs_Click
                                    End With
                                    Me.pnlAvailTabs.Controls.Add(btnTabsAndTables)
                                Catch ex As Exception

                                End Try

                                If counterIndex = numberPanelsInColumn Then
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
                    Else
                        index += 1
                    End If

                Next

            End If

        Else    ' this is Quick Service

            For Each dv In currentQuickTicketDataViews
                '    If currentTerminal.TermMethod = "Quick" Then
                If quickEndCount < 12 Then
                    quickEndCount = (dv.Count - 1)
                Else
                    quickEndCount -= 12
                End If
                DisplayTIcketsForQuick(dv, dv.Count - 1)
                Exit For
                '        End If
            Next

        End If


        Me.BringToFront()       '??????? to make sure in front

    End Sub

    Private Sub DisplayTIcketsForQuick(ByRef dv As DataView, ByVal endCount As Integer)

        Dim startingCount As Integer
        Dim index As Integer
        Dim vRow As DataRowView
        Dim counterIndex As Integer = 1
        Dim w As Single
        Dim h As Single
        Dim x As Single = buttonSpace
        Dim y As Single = buttonSpace
        Dim ls As String
        Dim lsTime As TimeSpan
        Dim timeAtTable As TimeSpan
        Dim altMethodUse As String = ""

        w = (Me.pnlAvailTabs.Width - (7 * buttonSpace)) / 6
        h = (Me.pnlAvailTabs.Height - (13 * buttonSpace)) / 12

        '    w = (Me.pnlAvailTabs.Width - (5 * buttonSpace)) / 4
        '   h = (Me.pnlAvailTabs.Height - (9 * buttonSpace)) / 8
        '       w = (Me.pnlAvailTabs.Width - (7 * buttonSpace)) / 6
        '      h = (Me.pnlAvailTabs.Height - (11 * buttonSpace)) / 10
        If needToCorrectRoundingError = True Then
            h -= 1.5
        End If

        '      startingCount = endCount - (dv.Count - 1)
        '     If startingCount < 0 Then startingCount = 0
        If endCount > 71 Then 'dv.Count > 71 Then
            startingCount = endCount - 71
        Else
            startingCount = 0
        End If

        For index = endCount To startingCount Step -1
            vRow = dv(index)

            ls = AssignCurrentStatusString(vRow("LastStatus"))
            lsTime = DetermineTimeSpan(vRow("LastStatusTime"))

            timeAtTable = DetermineTimeSpan(vRow("ExperienceDate"))

            Try
                If Not dvTerminalsUseOrder(0)("MethodUse") = vRow("MethodUse") Then
                    altMethodUse = vRow("MethodUse")
                Else
                    altMethodUse = ""
                End If

                Dim btnTabsAndTables As New DataSet_Builder.AvailTableUserControl(True, 0, 0, vRow("TabName"), vRow("TicketNumber"), vRow("LastStatus"), ls, lsTime, timeAtTable, Nothing, 0, currentTerminal.TermMethod, altMethodUse)
                '        btnTabsAndTables(index) = New DataSet_Builder.AvailTableUserControl(0, vRow("TabName"), vRow("LastStatus"), ls, lsTime, timeAtTable) 'AvailTableButton
                With btnTabsAndTables
                    .SatTime = vRow("ExperienceDate")
                    .Text = vRow("TicketNumber") 'vRow("TabName")
                    .NumberOfCustomers = vRow("NumberOfCustomers")
                    .NumberOfChecks = vRow("NumberOfChecks")
                    .ExperienceNumber = vRow("ExperienceNumber")
                    .EmpID = vRow("EmployeeID")
                    .CurrentStatus = AssignCurrentStatusString(vRow("LastStatus"))
                    .CurrentMenu = vRow("MenuID")
                    .TabID = vRow("TabID")
                    .Size = New Size(w, h)
                    .Location = New Point(x, y)
                    '             .BackColor = c7
                    '            .ForeColor = c3
                    AddHandler btnTabsAndTables.TableClicked, AddressOf Tabs_Click
                End With
                Me.pnlAvailTabs.Controls.Add(btnTabsAndTables)
            Catch ex As Exception

            End Try

            If counterIndex = 12 Then
                y = buttonSpace
                x += w + buttonSpace
                counterIndex = 0    'must restart at zero b/c we add 1 right away
            Else
                y += h + buttonSpace
            End If
            '     index += 1
            counterIndex += 1
        Next

    End Sub

    Private Function AssignCurrentStatusString(ByVal statusID As Integer)
        Dim currentStatus As String
        Dim oRow As DataRow

        For Each oRow In ds.Tables("TableStatusDesc").Rows
            '         If Not oRow("TableStatusID") Is DBNull.Value Then
            '        End If
            If oRow("TableStatusID") = statusID Then
                currentStatus = oRow("TableStatusDesc")
                Exit For
            End If
        Next

        Return currentStatus

    End Function

    Private Function DetermineTimeSpan(ByVal timeQuestion As DateTime)
        Dim timeDifference As TimeSpan

        timeDifference = Now.Subtract(timeQuestion)
        '        timeDifference = Format(timeDifference.Minutes, "###")

        Return timeDifference

    End Function

    Private Sub Bartender_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlBartenderButtons.Click
        Dim objButton As New KitchenButton("ForTesting", 0, 0, c3, c2)
        If Not sender.GetType Is objButton.GetType Then Exit Sub

        Dim index As Integer
        Me.pnlAvailTabs.Controls.Clear()

        objButton = CType(sender, KitchenButton)
        If objButton.ID = -1 Then
            If currentTerminal.IsOneBartender = False Then
                currentTerminal.IsOneBartender = True
                objButton.BackColor = c7
                GenerateOrderTables.PopulateTabsAndTables(currentServer, currentTerminal.CurrentDailyCode, IsBartenderMode, currentTerminal.IsOneBartender, currentBartenders)
            Else
                currentTerminal.IsOneBartender = False
                objButton.BackColor = c9
                GenerateOrderTables.PopulateTabsAndTables(currentServer, currentTerminal.CurrentDailyCode, IsBartenderMode, currentTerminal.IsOneBartender, currentBartenders)
            End If
            CreateDataViews(currentServer.EmployeeID, True)
            DisplayTabsAndTables(0)
        Else
            Dim BarMan As Employee
            For Each BarMan In currentBartenders
                If BarMan.EmployeeID = objButton.ID Then
                    currentServer = BarMan
                End If
            Next
            '   this is used if we want to change where tables are displayed only by selected bartender button
            '          IsOneBartender = True

            InitializeScreen()

        End If

    End Sub

    Private Sub Tables_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles pnlAvailTabs.Click
        StopTablesTimer()
        '      time1 = Now
        '    Dim prt As New PrintHelper
        '   prt.PrintBarCodeStart()

        Dim objButton As New DataSet_Builder.AvailTableUserControl 'AvailTableButton

        If Not sender.GetType Is objButton.GetType Then Exit Sub

        Dim oRow As DataRow
        Dim isCurrentlyHeld As Boolean
        Dim tableNumber As Integer
        Dim experienceNumber As Int64

        objButton = CType(sender, DataSet_Builder.AvailTableUserControl) 'AvailTableButton)
        tableNumber = CType(objButton.TableNumber, Integer)
        experienceNumber = objButton.ExperienceNumber

        'this instantiates currentTable
        isCurrentlyHeld = PopulateThisExperience(experienceNumber, False)

        If typeProgram = "Online_Demo" Then
            Dim filterString As String = "ExperienceNumber = " & experienceNumber
            Dim NotfilterString As String = " NOT ExperienceNumber = " & experienceNumber
            Demo_FilterDontDelete(dsOrder.Tables("AvailTables"), dsOrder.Tables("CurrentlyHeld"), filterString) ', NotfilterString) '"ExperienceNumber = '" & experienceNumber & "'")
            dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld") = False
            dsOrder.Tables("AvailTables").Clear()
        Else
            If dsOrder.Tables("CurrentlyHeld").Rows.Count = 0 Then
                'this means either table is gone or local database down
                Exit Sub
            End If
        End If

        currentTable = New DinnerTable

        If isCurrentlyHeld = False Then

            If currentTable Is Nothing Then
                MsgBox("Table Does Not Exist")
                Exit Sub
            End If
            currentTable.SatTime = objButton.SatTime
            oRow = dsOrder.Tables("CurrentlyHeld").Rows(0)

            PopulateCurrentTableData(oRow)
            StartOrderProcess(currentTable.ExperienceNumber)

            '   tests the status (c1 is transfer) then give it sat status
            If objButton.CurrentStatus = "Transfering" Then
                TransferTableToOpenOrder(currentServer.EmployeeID, experienceNumber, 2)
            End If

            RaiseEvent FireOrderScreen(Nothing)

        Else
            MsgBox("Table " & tableNumber & " is currently held by " & dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld"))
            InitializeScreen()
            Exit Sub
        End If

    End Sub

    Private Sub Tabs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) ' Handles pnlAvailTabs.Click

        StopTablesTimer()

        Dim objButton As New DataSet_Builder.AvailTableUserControl 'AvailTableButton

        If Not sender.GetType Is objButton.GetType Then Exit Sub

        Dim oRow As DataRow
        Dim isCurrentlyHeld As Boolean
        Dim tabID As Int64
        Dim tabName As String
        Dim experienceNumber As Int64



        objButton = CType(sender, DataSet_Builder.AvailTableUserControl) 'AvailTableButton)
        tabID = CType(objButton.TabID, Int64)
        tabName = CType(objButton.Text, String)
        experienceNumber = objButton.ExperienceNumber

        '     If tabID = -888 Then
        '    '   -888 indicates TabGroup, true is for Dine IN
        '   OpenNewTab(-888, currentServer.NickName & "'s Tabs", True)
        '  Exit Sub
        ' End If

        '444
        If currentTerminal.TermMethod = "Quick" Then
            RaiseEvent QuickTicketStart(experienceNumber) 'tabID, tabName,
            Exit Sub
        End If

        isCurrentlyHeld = PopulateThisExperience(experienceNumber, False)

        If typeProgram = "Online_Demo" Then
            Dim filterString As String = "ExperienceNumber = " & experienceNumber
            Dim NotfilterString As String = " NOT ExperienceNumber = " & experienceNumber
            If tabID = -888 Then
                'this is group tabs
                Demo_FilterDontDelete(dsOrder.Tables("QuickTickets"), dsOrder.Tables("CurrentlyHeld"), filterString) ', NotfilterString) '"ExperienceNumber = '" & experienceNumber & "'")
            Else
                Demo_FilterDontDelete(dsOrder.Tables("AvailTabs"), dsOrder.Tables("CurrentlyHeld"), filterString) ', NotfilterString) '"ExperienceNumber = '" & experienceNumber & "'")
            End If
            dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld") = False
            dsOrder.Tables("AvailTabs").Clear()
        Else
            If dsOrder.Tables("CurrentlyHeld").Rows.Count = 0 Then
                'this means either table is gone or local database down
                Exit Sub
            End If
        End If

        currentTable = New DinnerTable

        If isCurrentlyHeld = False Then

            If currentTable Is Nothing Then
                MsgBox("Tab Does Not Exist")
                Exit Sub
            End If

            oRow = dsOrder.Tables("CurrentlyHeld").Rows(0)
            '         UpdateCurrentlyHeld(currentServer.FullName, experienceNumber)
            PopulateCurrentTableData(oRow)

            '            currentTable.ExperienceNumber = experienceNumber
            '        currentTable.IsTabNotTable = True
            '        currentTable.TableNumber = 0
            '       currentTable.TabID = tabID
            ''       currentTable.TabName = tabName
            '       currentTable.TicketNumber = oRow("TicketNumber")
            '       currentTable.EmployeeID = oRow("EmployeeID")
            '       currentTable.CurrentMenu = oRow("MenuID")
            ''      currentTable.StartingMenu = oRow("MenuID")
            '      currentTable.NumberOfChecks = oRow("NumberOfChecks")
            '      currentTable.NumberOfCustomers = oRow("NumberOfCustomers")
            '     currentTable.LastStatus = oRow("LastStatus")


            ' this is for Tab Groups
            'we are choosing dataview that matches employee
            If tabID = -888 Then
                If Not currentTable.EmployeeID = currentServer.EmployeeID Then
                    GenerateOrderTables.ReleaseCurrentlyHeld()
                    GenerateOrderTables.SaveOpenOrderData()
                    MsgBox(currentServer.FullName & " does not have access")
                    Exit Sub
                End If

                With dvQuickTickets
                    .Table = dsOrder.Tables("QuickTickets")
                    .RowFilter = "EmployeeID = " & currentTable.EmployeeID
                    .Sort = "ExperienceDate ASC"
                    '         .Sort = "LastStatus" ' DESC"
                End With
                'was just testing below
                'does not work at all
                '       OpenNewTab(-888, currentServer.NickName & "'s Tabs", True)
                '      Exit Sub
            End If

            '       time1 = Now

            StartOrderProcess(currentTable.ExperienceNumber)
            '      If orderScreenInitialized = False Then
            '     StartOrderProcess(currentTable.ExperienceNumber)

            '    StartOrderProcess(currentTable.ExperienceNumber)
            '   orderScreenInitialized = True
            ' End If

            '      ActiveOrder = New term_OrderForm
            '     Me.ActiveOrder.IsBartenderMode = Me.IsBartenderMode

            '   tests the status (c1 is transfer) then give it sat status
            If objButton.CurrentStatus = "Transfering" Then
                TransferTableToOpenOrder(currentServer.EmployeeID, experienceNumber, 2)
            End If


            '       ActiveOrder.Location = New Point(0, 0)
            '      Me.Controls.Add(ActiveOrder)
            '     ActiveOrder.BringToFront()

            RaiseEvent FireOrderScreen(Nothing)

            '      time2 = Now
            '     timeDiff = time2.Subtract(time1)
            '    MsgBox(timeDiff.ToString)

            '      ActiveOrder = New term_OrderForm(IsBartenderMode)
            '      ActiveOrder.Location = New Point(0, 0)
            '      Me.Controls.Add(ActiveOrder)
            '      ActiveOrder.BringToFront()
            '     'uc ActiveOrder.Show()
            '     testBoolean = True
            '    StartOrderProcess(currentTable.ExperienceNumber)



        Else
            MsgBox("Tab " & tabName & " is currently held by " & dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld"))
            InitializeScreen()
            Exit Sub

        End If

        Exit Sub



        '      Dim objButton As New DataSet_Builder.AvailTableUserControl 'AvailTableButton
        If Not sender.GetType Is objButton.GetType Then Exit Sub

        '      Dim tabID As Integer
        '      Dim tabName As String
        '   Dim experienceNumber As Integer
        Dim numberOfChecks As Integer
        Dim numberOfCustomers As Integer
        Dim empID As Integer
        Dim tktNum As Integer
        Dim lStatus As Integer

        objButton = CType(sender, DataSet_Builder.AvailTableUserControl) 'AvailTableButton)

        '       tabID = CType(objButton.TabID, Integer)
        '       tabName = CType(objButton.Text, String)
        experienceNumber = CType(objButton.ExperienceNumber, Integer)
        numberOfChecks = CType(objButton.NumberOfChecks, Integer)
        currentTerminal.CurrentMenuID = CType(objButton.CurrentMenu, Integer)
        numberOfCustomers = CType(objButton.NumberOfCustomers, Integer)
        empID = CType(objButton.EmpID, Integer)
        tktNum = CType(objButton.TicketNumber, Integer)
        lStatus = CType(objButton.CurrentStatusID, Integer)

        '      If IsBartenderMode = False Then
        '        End If

        StopTablesTimer()

        '      ActiveOrder = New term_OrderForm(currentMenuID, empID, True, tabID, tabName, experienceNumber, numberOfChecks, numberOfCustomers, IsBartenderMode, tktNum, lStatus)

        '   tests the status (c1 is transfer) then give it sat status
        '   ???????????
        If objButton.CurrentStatus = "Transfering" Then
            TransferTableToOpenOrder(currentServer.EmployeeID, experienceNumber, 2)
        End If

        ActiveOrder.Show()
        '    StopTablesTimer()


    End Sub

    Private Sub btnAddTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTable.Click
        StopTablesTimer()
        '      DisableTables_Screen()

        If typeProgram = "Online_Demo" Then
            dsOrder.Tables("AvailTables").Clear()
        End If

        '    SeatingChart._seatingUsernameEnterOnLogin = False
        RaiseEvent FireSeatingChart(False)
        Exit Sub

        '     SeatingChart = New Seating_ChooseTable   'Seating_Dining(currentServer.EmployeeID)
        '     SeatingChart.Location = New Point(0, 0)
        '     Me.Controls.Add(SeatingChart)
        '    SeatingChart.BringToFront()


    End Sub

    Private Sub NoTableAdded222(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles SeatingChart.NoTableSelected

        '     Try
        '    SeatingChart.Dispose()
        '   Catch ex As Exception
        '
        '       End Try

        EnableTables_Screen()
        InitializeScreen()

    End Sub

    '****************
    '****************
    '****************

    Private Sub btnAddTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTab.Click
        StopTablesTimer()
        If typeProgram = "Online_Demo" Then
            dsOrder.Tables("AvailTabs").Clear()
        End If

        If currentTerminal.TermMethod = "Quick" Then
            Dim expnum As Int64
            '999test
            'right now just doing for first tab, because we dispose current table otherwise
            currentTable = New DinnerTable
            currentTable.TabID = -999
            expnum = GenerateOrderTables.CreatingNewTicket()
            RaiseEvent QuickTicketStart(expnum)

        Else
            RaiseEvent FireSeatingTab("TableScreen", Nothing) 'FireTabChart(False)
        End If

    End Sub

    Private Sub CustomerLoyalty222() Handles SeatingTab222.CustomerCardEvent '(ByRef tabAccountInfo As DataSet_Builder.Payment) Handles SeatingTab222.CustomerCardEvent

        Dim tabAccountInfo As DataSet_Builder.Payment
        Dim tabString As String
        tabString = tabAccountInfo.LastName
        If tabAccountInfo.FirstName.Length > 0 Then
            tabString = tabString + ", " & tabAccountInfo.FirstName
        End If


        '    MsgBox(tabAccountInfo.LastName)
        'need to open new tab, but go right to Tab_Screen
        OpenNewTab222(-999, tabString, True, tabAccountInfo)
        AddPaymentToCollection(tabAccountInfo)
        SeatingTab222.Dispose()


        EnableTables_Screen()
        InitializeScreen()

    End Sub
    Private Sub NewAddNewTab222() Handles SeatingTab222.OpenNewTabEvent


        '   -999 for tabID will tell it to generate New TabID (which will be experience Number)
        '   later we will have a look-up for returning customers
        Dim newTabNameString As String
        newTabNameString = SeatingTab222.NewTabName

        OpenNewTab222(-999, newTabNameString, True, Nothing)
        SeatingTab222.Dispose()

        EnableTables_Screen()
        InitializeScreen()

    End Sub
    Private Sub NewAddNewTakeOutTab222() Handles SeatingTab222.OpenNewTakeOutTab

        Me.Enabled = True
        Dim newTabNameString As String
        newTabNameString = SeatingTab222.NewTabName

        '   -999 for tabID will tell it to generate New TabID (which will be experience Number)
        '   later we will have a look-up for returning customers
        OpenNewTab222(-990, newTabNameString, False, Nothing)
        SeatingTab222.Dispose()


        EnableTables_Screen()
        InitializeScreen()

    End Sub
    Private Sub CancelNewTab222() Handles SeatingTab222.CancelNewTab


        SeatingTab222.Dispose()

        EnableTables_Screen()
        InitializeScreen()

    End Sub
    Private Sub OpenNewTab222(ByVal tabId As Int64, ByVal tabName As String, ByVal isDineIn As Boolean, ByRef tabAccountInfo As DataSet_Builder.Payment)

    End Sub

    Private Sub OpenNewTab(ByVal tabId As Int64, ByVal tabName As String, ByVal isDineIn As Boolean, ByRef tabAccountInfo As DataSet_Builder.Payment)
        ' there is another OpenNewTab in Login ?????
        '     Using this for Group Tab's (probably need to change)

        Dim expNum As Int64
        Dim tktNum As Integer
        Dim isCurrentlyHeld As Boolean
        Dim satTm As DateTime

        If tabId = -888 Or currentTerminal.TermMethod = "Quick" Then
            tktNum = CreateNewTicketNumber()
            If tabName = "New Tab" Then
                tabName = "Tkt# " & tktNum.ToString
            End If
        Else
            '444      If tabName = "New Tab" Then
            'somehow this is making program change Method Use to TakeOut
            '      tktNum = CreateNewTicketNumber()
            '     tabName = "Tkt# " & tktNum.ToString
            '    Else
            tktNum = 0
            '     End If
        End If

        expNum = CreateNewExperience(currentServer.EmployeeID, Nothing, tabId, tabName, 1, 2, tktNum, 0, currentServer.LoginTrackingID)
        isCurrentlyHeld = PopulateThisExperience(expNum, False)

        currentTable = New DinnerTable
        currentTable.ExperienceNumber = expNum
        currentTable.IsTabNotTable = True
        currentTable.TabID = tabId
        currentTable.TabName = tabName
        currentTable.TableNumber = 0
        currentTable.TicketNumber = tktNum
        currentTable.EmployeeID = currentServer.EmployeeID
        currentTable.CurrentMenu = currentTerminal.currentPrimaryMenuID '444primaryMenuID  'this is the system menu - can change during order process
        currentTable.StartingMenu = currentTerminal.currentPrimaryMenuID '444primaryMenuID
        currentTable.NumberOfCustomers = 1      'is 1 when you first open
        currentTable.NumberOfChecks = 1
        currentTable.LastStatus = 2
        currentTable.SatTime = Now
        currentTable.ItemsOnHold = 0
        If dvTerminalsUseOrder.Count > 0 Then
            currentTable.MethodUse = dvTerminalsUseOrder(0)("MethodUse")
            currentTable.MethodDirection = dvTerminalsUseOrder(0)("MethodDirection")
        Else
            currentTable.MethodUse = "Dine In"
            currentTable.MethodDirection = "None"
        End If


        StartOrderProcess(currentTable.ExperienceNumber)
        'sss   satTm = AddStatusChangeData(currentTable.ExperienceNumber, 2, Nothing, 0, Nothing)
        '      SaveESCStatusChangeData(2, Nothing, 0, Nothing)


        RaiseEvent FireOrderScreen(tabAccountInfo)

    End Sub

    Private Sub btnAddTabGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTabGroup.Click

        Try
            With dvQuickTickets
                .Table = dsOrder.Tables("QuickTickets")
                .RowFilter = "EmployeeID = " & currentServer.EmployeeID
                .Sort = "ExperienceDate ASC"
                '       .Sort = "ExperienceDate" ' DESC"
            End With
            If dvQuickTickets.Count > 0 Then
                MsgBox(currentServer.FullName & " already has a tab group open.")
                Exit Sub
            End If

            StopTablesTimer()
            '   -888 indicates TabGroup, true is for Dine IN
            OpenNewTab(-888, currentServer.NickName & "'s Tabs", True, Nothing)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnBartenderLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBartenderLogin.Click
        StopTablesTimer()
        DisableTables_Screen()

        clockInPanel = New ClockInUserControl()
        clockInPanel.Location = New Point((ssX - clockInPanel.Width) / 2, (ssY - clockInPanel.Height) / 2)
        Me.Controls.Add(clockInPanel)
        clockInPanel.BringToFront()

    End Sub

    Private Sub ClockInEmployeeClicked() Handles clockInPanel.ApplyClockInCheck ', clockInPanel.ClosingClockIn

        MsgBox(currentClockEmp.FullName & " has just clocked in at: " & Now.ToString)
        ClockInEnding()

    End Sub

    Private Sub ClockInEnding() Handles clockInPanel.ClosingClockIn

        If currentTerminal.TermMethod = "Bar" Then
            GenerateOrderTables.PopulateBartenderCollection()
            If currentBartenders.Count > 1 Then
                currentTerminal.IsOneBartender = False
            End If
        End If

        EnableTables_Screen()
        InitializeScreen()

    End Sub

    Private Sub btnTablesExit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTablesExit.Click ', ClockingOutEmployee.ClockOutComplete

        '444     currentServer = New Employee
        '      updateClockTimer.Dispose()
        '     tablesInactiveTimer.Dispose()
        RaiseEvent ExitingTableScreen()

    End Sub

    Private Sub btnTablesExit_Click_1() Handles ClockingOutEmployee.ClockOutComplete

        '444    currentServer = New Employee
        RaiseEvent ExitingTableScreen()
        'currently we are exiting, we should stay here if Bar or Quick
        'but with the memory leak I figured this is good for now
        '     If IsBartenderMode = False And Not currentTerminal.TermMethod = "Quick" Then
        '    Else
        '???? not sure of below, debug
        '     GenerateOrderTables.PopulateBartenderCollection()
        '   InitializeScreen()
        '  End If

    End Sub

    Private Sub btnManager_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnManager.Click
        StopTablesTimer()
        RaiseEvent ManagementButton(currentServer)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        GC.Collect()
        Exit Sub

        Dim avgTime As Integer

        dsOrder.Tables("OrderByPrinter").Clear()

        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        sql.SqlDataAdapterByPrinter2.Fill(dsOrder.Tables("OrderByPrinter"))
        sql.cn.Close()

        Dim oRow As DataRow


        For Each oRow In dsOrder.Tables("OrderByPrinter").Rows

            MsgBox(oRow("RoutingID"), , "Routing")
            MsgBox(oRow("Count"), , "Count")
            MsgBox(oRow("Schedule"), , "Schedule")
            '      MsgBox(oRow("Actual"), , "Actual")
            avgTime = Format(oRow("Actual") / oRow("Count"), "####")
            MsgBox(avgTime, , "Avg")

        Next


    End Sub

    Private Sub btnClockOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClockOut.Click
        Dim yesOpenTables As Boolean

        If IsBartenderMode = False And Not currentTerminal.TermMethod = "Quick" Then
            StopTablesTimer()

            '  check to see if there are any open tables           **********************
            If currentClockEmp Is Nothing Then
                currentClockEmp = New Employee
            End If
            currentClockEmp = currentServer
            yesOpenTables = GenerateOrderTables.AnyOpenTables(currentClockEmp)
            StartClockOut(currentClockEmp, yesOpenTables)

        Else
            StopTablesTimer()
            DisableTables_Screen()
            isNoSaleOrClockOut = "clockout"

            nosaleLoginPad = New NumberPad
            nosaleLoginPad.Location = New Point((Me.Width - nosaleLoginPad.Width) / 2, ((Me.Height - nosaleLoginPad.Height) / 2) + 40)
            Me.Controls.Add(nosaleLoginPad)
            Me.nosaleLoginPad.BringToFront()
        End If


        Exit Sub
        '222 444


        '444   Dim yesOpenTables As Boolean

        '  check to see if there are any open tables           **********************
        yesOpenTables = AnyOpenTables(currentServer)

        If yesOpenTables = True Then
            openInfo = New DataSet_Builder.Information_UC(currentServer.FullName & " still has open checks. Press here to clock out or enter Tip Adjustments.")
            openInfo.Location = New Point((Me.Width - openInfo.Width) / 2, (Me.Height - openInfo.Height) / 2)
            Me.Controls.Add(openInfo)
            openInfo.BringToFront()
            '       Exit Sub
        Else
            StartClockOut(currentServer, False)
        End If

    End Sub

    Private Sub OkToLeaveOpenTables(ByVal sender As Object, ByVal e As System.EventArgs) Handles openInfo.AcceptInformation

        'we can use currentSever here because we changed when there were open tables
        StartClockOut(currentServer, False)

    End Sub
    Private Sub StartClockOut(ByVal emp As Employee, ByVal hasOpenTables As Boolean) '

        Dim salaried As Employee
        For Each salaried In SalariedEmployees
            If salaried.EmployeeID = currentClockEmp.EmployeeID Then
                'this is a salaried employee
                MsgBox(currentServer.NickName & " is Salaried. No need to Clock Out.")
                Exit Sub
            End If
        Next

        If currentClockEmp.ClockInReq = True Then 'currentServer.ClockInReq = True Then

            Me.ClockingOutEmployee = New ClockOut_UC(currentClockEmp, hasOpenTables) 'currentServer, hasOpenTables)     '      , tipableSales, chargedSales, chargedTips)
            Me.ClockingOutEmployee.Location = New Point(0, 0) '((Me.Width - Me.ClockingOutEmployee.Width) / 2, (Me.Height - Me.ClockingOutEmployee.Height) / 2)
            If currentClockEmp.Server = False And currentClockEmp.Bartender = False And currentClockEmp.Cashier = False And currentClockEmp.Manager = False Then
                ClockingOutEmployee.EitherPrintOrClockOut(True)
                ClockingOutEmployee.Dispose()
            Else
                Me.Controls.Add(Me.ClockingOutEmployee)
                Me.ClockingOutEmployee.BringToFront()
                '444       DisableTables_Screen()
            End If

        Else
            MsgBox(currentClockEmp.FullName & " does not use time clock.")
        End If

    End Sub

    Private Sub CLockOutCanceled() Handles ClockingOutEmployee.ClockOutCancel

        Try
            ClockingOutEmployee.Dispose()
        Catch ex As Exception

        End Try
        EnableTables_Screen()
        InitializeScreen()

    End Sub


    Private Function BuildSQLWHEREClauseFromOpenOrders222(ByVal strClockOut As String, ByVal expNum As Int64, ByVal firstTime As Boolean)
        Dim strWhereClause As String = strClockOut

        If firstTime = True Then
            strWhereClause = strWhereClause & " OpenOrders.ExperienceNumber = '" & expNum & "'"
        Else
            strWhereClause = strWhereClause & " OR OpenOrders.ExperienceNumber = '" & expNum & "'"
        End If

        Return strWhereClause

    End Function

    Private Function BuildSQLWHEREClauseFromPaymentsAndCredits222(ByVal strClockOut As String, ByVal expNum As Int64, ByVal firstTime As Boolean)
        Dim strWhereClause As String = strClockOut

        If firstTime = True Then
            strWhereClause = strWhereClause & " PaymentsAndCredits.ExperienceNumber = '" & expNum & "'"
        Else
            strWhereClause = strWhereClause & " OR PaymentsAndCredits.ExperienceNumber = '" & expNum & "'"
        End If

        Return strWhereClause

    End Function

    Private Function AnyOpenTables222(ByVal emp As Employee)

        GenerateOrderTables.PopulateTabsAndTables(emp, currentTerminal.CurrentDailyCode, False, False, Nothing)
        CreateDataViews(emp.EmployeeID, True)
        If dvAvailTables.Count + dvTransferTables.Count + dvAvailTabs.Count + dvTransferTabs.Count + dvQuickTickets.Count > 0 Then
            Return True
        Else
            Return False
        End If


        '222 444
        Exit Function
        Dim oRow As DataRowView

        For Each oRow In dvAvailTables  'dsOrder.Tables("AvailTables").Rows
            If oRow("EmployeeID") = currentServer.EmployeeID Then
                Return True
            End If
        Next

        For Each oRow In dvAvailTabs    'dsOrder.Tables("AvailTabs").Rows
            If oRow("EmployeeID") = currentServer.EmployeeID Then
                Return True
            End If
        Next

        Return False

    End Function

    Private Sub CreateClosingDataViews222()

        dvClosedTables = New DataView
        dvClosedTabs = New DataView

        If dsOrder.Tables("ClosedTables").Rows.Count > 0 Then
            With dvClosedTables
                .Table = dsOrder.Tables("AvailTables")
                .RowFilter = "LastStatus = 1" '"EmployeeID = " & currentServer.EmployeeID
            End With
        End If

        If dsOrder.Tables("ClosedTabs").Rows.Count > 0 Then
            With dvClosedTabs
                .Table = dsOrder.Tables("AvailTabs")
                .RowFilter = "LastStatus = 1"  '"EmployeeID = " & currentServer.EmployeeID
            End With
        End If

    End Sub

    Private Sub CloseCheckFailed()
        Dim info As DataSet_Builder.Information_UC
        info = New DataSet_Builder.Information_UC("You can not close out. One possible reason may be the Server Connection is down.")
        info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
        Me.Controls.Add(info)
        info.BringToFront()

    End Sub

    Private Sub SetTableScreenTime()
        Me.lblTableTime.Text = Format(Now, "h:mm tt")

    End Sub

    Private Sub UpdateTableScreenTime(ByVal sender As Object, ByVal e As System.EventArgs)
        '     InitializeScreen()
        If currentTerminal.CurrentDailyCode = 0 Then
            RaiseEvent ExitingTableScreen()
        End If

        If connectionDown = True Then
            'this is just so we don't loop
            connectionDown = False
            RaiseEvent ExitingTableScreen()
        End If

        If Not currentTerminal.TermMethod = "Quick" Then
            If currentTerminal.TermMethod = "Bar" Then
                GenerateOrderTables.PopulateBartenderCollection()
            End If
            GenerateOrderTables.PopulateTabsAndTables(currentServer, currentTerminal.CurrentDailyCode, IsBartenderMode, currentTerminal.IsOneBartender, currentBartenders)
            CreateDataViews(currentServer.EmployeeID, True)
            DisplayTabsAndTables(quickEndCount)
        End If

        SetTableScreenTime()

    End Sub



    Private Sub btnNoSale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoSale.Click
        StopTablesTimer()
        isNoSaleOrClockOut = "nosale"

        '    currentTerminal.HasCashDrawer = True
        If currentTerminal.HasCashDrawer = True Then
            DisableTables_Screen()
           
            'make bartender or cashier enter their password if in bartender mode
            '    If Me.IsBartenderMode Then
            nosaleLoginPad = New NumberPad
            nosaleLoginPad.Location = New Point((Me.Width - nosaleLoginPad.Width) / 2, ((Me.Height - nosaleLoginPad.Height) / 2) + 40)
            '       lblLogin.Text = "Enter Login"
            Me.Controls.Add(nosaleLoginPad)
            Me.nosaleLoginPad.BringToFront()
        End If

        '    End If
        ' then open drawer
        '   then show NO SALE usercontrol


    End Sub

    Private Sub NoSalePassword(ByVal sender As Object, ByVal e As System.EventArgs) Handles nosaleLoginPad.NumberEntered
        'this is the accepted NoSale

        Dim loginEnter As String
        Dim emp As DataSet_Builder.Employee
        Dim yesOpenTables As Boolean

        EnableTables_Screen()
        loginEnter = nosaleLoginPad.NumberString
        emp = GenerateOrderTables.TestUsernamePassword(loginEnter, True)

        If isNoSaleOrClockOut = "nosale" Then
            If Not emp Is Nothing Then
                If emp.OperationMgmtLimited = True Or emp.OperationMgmtAll = True Then
                    Dim prt As New PrintHelper
                    prt.PrintOpenCashDrawer()
                    ' need to send to database to record Cash Drawer opening

                    Dim newPayment As DataSet_Builder.Payment

                    newPayment.Purchase = 0
                    newPayment.PaymentTypeID = -2
                    newPayment.PaymentTypeName = "Cash"   '"Enter Acct #"
                    newPayment.Description = "no-sale"

                    GenerateOrderTables.AddPaymentToDataRow(newPayment, True, 0, emp.EmployeeID, 1, False)
                    'we need this update here b/c we are not in any experience
                    GenerateOrderTables.UpdatePaymentsAndCredits()

                    ccDisplay = New CashClose_UC(0, 0, 0, 0)
                    ccDisplay.Location = New Point((Me.Width - ccDisplay.Width) / 2, (Me.Height - ccDisplay.Height) / 2)
                    Me.Controls.Add(ccDisplay)
                    ccDisplay.BringToFront()
                    ccDisplay.BeginNoSale()
                End If
            End If

        ElseIf isNoSaleOrClockOut = "clockout" Then

            If Not emp Is Nothing Then
                '  check to see if there are any open tables           **********************
                If loginEnter.Length < 8 Then
                    MsgBox("Enter both EmployeeID as Passcode")
                    Exit Sub
                End If
                Dim doesEmpNeedToClockOut As Boolean
                doesEmpNeedToClockOut = TestClockOut(loginEnter)
                If doesEmpNeedToClockOut = False Then
                    '      MakeClockOutBooleanFalse()
                    nosaleLoginPad.btnNumberClear_Click()
                    MsgBox("Employee does not need to Clock Out")
                    Me.nosaleLoginPad.Dispose()
                    Exit Sub
                End If
                If currentClockEmp Is Nothing Then
                    currentClockEmp = New Employee
                End If
                currentClockEmp = emp
                yesOpenTables = GenerateOrderTables.AnyOpenTables(emp)

                If yesOpenTables = True Then
                    openInfo = New DataSet_Builder.Information_UC(emp.FullName & " still has open checks. Press here to clock out or enter Tip Adjustments.")
                    openInfo.Location = New Point((Me.Width - openInfo.Width) / 2, (Me.Height - openInfo.Height) / 2)
                    Me.Controls.Add(openInfo)
                    openInfo.BringToFront()
                    '       Exit Sub
                Else
                    StartClockOut(currentClockEmp, False)
                End If
            End If
        End If

        Me.nosaleLoginPad.Dispose()

    End Sub
    Private Sub NoSalePasswordBlank() Handles nosaleLoginPad.AcceptManager
        'this just removes the nosale panel

        EnableTables_Screen()
        Me.nosaleLoginPad.Dispose()

    End Sub


    Friend Sub DisableTables_Screen()

        Me.pnlBartenderButtons.Enabled = False
        Me.pnlAvailTabs.Enabled = False
        Me.pnlTableCommands.Enabled = False
        Me.btnAddTab.Enabled = False
        Me.btnAddTabGroup.Enabled = False
        Me.btnAddTable.Enabled = False
        '    Me.btnNoSale.Enabled = False



    End Sub

    Friend Sub EnableTables_Screen()

        Me.pnlBartenderButtons.Enabled = True
        Me.pnlAvailTabs.Enabled = True
        Me.pnlTableCommands.Enabled = True
        Me.btnAddTab.Enabled = True
        Me.btnAddTabGroup.Enabled = True
        Me.btnAddTable.Enabled = True
        '       Me.btnNoSale.Enabled = True


    End Sub


    Private Sub lblTableTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTableTime.Click
        Dim dv As DataView

        If currentTerminal.TermMethod = "Quick" Then

            If dsOrder.Tables("QuickTickets").Rows.Count > 0 Then
                Me.pnlAvailTabs.Controls.Clear()

                For Each dv In currentQuickTicketDataViews
                    'this is the last row in each of the DataView Collections
                    'the dataviews are grouped by bartender


                    If quickEndCount < 12 Then
                        quickEndCount = (dv.Count - 1)
                    Else
                        quickEndCount -= 12
                    End If

                    DisplayTIcketsForQuick(dv, quickEndCount)
                    Exit For
                Next
            End If
        Else
            ' not for quick

            If quickEndCount = 0 Then
                quickEndCount = 40
                '        quickEndCount = (dvAvailTables.Count + dvAvailTabs.Count + dvTransferTables.Count + dvTransferTabs.Count)

            ElseIf quickEndCount > 39 Then
                quickEndCount += 8

            End If

            If quickEndCount > (16 + dvAvailTables.Count + dvAvailTabs.Count + dvTransferTables.Count + dvTransferTabs.Count) Then
                quickEndCount = 0
            End If
            DisplayTabsAndTables(quickEndCount)

        End If


    End Sub

    
End Class
