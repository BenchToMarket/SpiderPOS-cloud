Imports DataSet_Builder


Friend Structure ManagementAuthorization
    '   this is used for a second employee(manager) authorizing something 
    '   while first employee is logged-in

    Private _fullName As String
    Private _operationAll As Boolean
    Private _operationLimited As Boolean
    Private _employeeAll As Boolean
    Private _employeeLimited As Boolean
    Private _reportAll As Boolean
    Private _reportLimited As Boolean
    Private _systemAll As Boolean
    Private _systemLimited As Boolean

    Private _operationLevel As Integer
    Private _employeeLevel As Integer
    Private _reportLevel As Integer
    Private _systemLevel As Integer


    Friend Property OperationLevel() As Integer
        Get
            Return _operationLevel
        End Get
        Set(ByVal Value As Integer)
            _operationLevel = Value
        End Set
    End Property

    Friend Property EmployeeLevel() As Integer
        Get
            Return _employeeLevel
        End Get
        Set(ByVal Value As Integer)
            _employeeLevel = Value
        End Set
    End Property

    Friend Property ReportLevel() As Integer
        Get
            Return _reportLevel
        End Get
        Set(ByVal Value As Integer)
            _reportLevel = Value
        End Set
    End Property

    Friend Property SystemLevel() As Integer
        Get
            Return _systemLevel
        End Get
        Set(ByVal Value As Integer)
            _systemLevel = Value
        End Set
    End Property

    Friend Property FullName() As String
        Get
            Return _fullName
        End Get
        Set(ByVal Value As String)
            _fullName = Value
        End Set
    End Property


    Friend Property OperationAll() As Boolean
        Get
            Return _operationAll
        End Get
        Set(ByVal Value As Boolean)
            _operationAll = Value
        End Set
    End Property

    Friend Property OperationLimited() As Boolean
        Get
            Return _operationLimited
        End Get
        Set(ByVal Value As Boolean)
            _operationLimited = Value
        End Set
    End Property

    Friend Property EmployeeAll() As Boolean
        Get
            Return _employeeAll
        End Get
        Set(ByVal Value As Boolean)
            _employeeAll = Value
        End Set
    End Property

    Friend Property EmployeeLimited() As Boolean
        Get
            Return _employeeLimited
        End Get
        Set(ByVal Value As Boolean)
            _employeeLimited = Value
        End Set
    End Property

    Friend Property ReportAll() As Boolean
        Get
            Return _reportAll
        End Get
        Set(ByVal Value As Boolean)
            _reportAll = Value
        End Set
    End Property

    Friend Property ReportLimited() As Boolean
        Get
            Return _reportLimited
        End Get
        Set(ByVal Value As Boolean)
            _reportLimited = Value
        End Set
    End Property

    Friend Property SystemAll() As Boolean
        Get
            Return _systemAll
        End Get
        Set(ByVal Value As Boolean)
            _systemAll = Value
        End Set
    End Property

    Friend Property SystemLimited() As Boolean
        Get
            Return _systemLimited
        End Get
        Set(ByVal Value As Boolean)
            _systemLimited = Value
        End Set
    End Property

End Structure



Public Class Manager_Form
    Inherits System.Windows.Forms.UserControl

    '  Dim sql As New DataSet_Builder.SQLHelper(connectserver)
    Friend ManagerOpenOrdersCurrencyMan As CurrencyManager

    Friend WithEvents mainManager As Manager_Main_UC
    Dim WithEvents orderAdj As Manager_OrderAdj_UC
    '   Dim WithEvents ActiveMgrOrder As term_OrderForm
    Dim WithEvents transCheck As Manager_Transfer_UC
    '   Dim WithEvents _closeCheck As CloseCheck
    '  Dim WithEvents ActiveSplit As SplitChecks
    Dim WithEvents reportManager As DataSet_Builder.Manager_Reports_UC 'Manager_Reports_UC '  DataSet_Builder.Reports_EmployeeHours     '
    Dim WithEvents SeatingChart As Seating_ChooseTable 'not using (using FireOrderScreen)
    Friend WithEvents SeatingTab As Seating_EnterTab
    Dim WithEvents overrideSeating As Seating_ChooseTable
    Dim WithEvents closeBatch As BatchClose_UC
    '   Dim WithEvents employeeLog As EmployeeLoggedInUserControl

    '  Friend empActive As Employee
    Dim usernameEnterOnLogin As Boolean
    Dim isBartender As Boolean
    Dim weAreClosingDaily As Int64

    Private _reopenedTabNotTable As Boolean
    '   Private _reopenedTable As Boolean


    Friend Property ReopenedTabNotTable()
        Get
            Return _reopenedTabNotTable
        End Get
        Set(ByVal Value)
            _reopenedTabNotTable = Value
        End Set
    End Property


    '    Friend Property ReopenedTable()
    '        Get
    '            Return _reopenedTable
    '       End Get
    ''       Set(ByVal Value)
    '           _reopenedTable = Value
    '       End Set
    '  End Property
    Event ClosePOS()
    Event UpdatingAfterTransfer()
    Event MgrClosingCheck()
    Event FireOrderScreen()
    Event FireSeatingChart(ByVal fromMgmt As Boolean)
    Event OverrideTableStatus(ByVal fromMgmt As Boolean)
    Event DisposingOfManager()
    Event FireSeatingTab(ByVal startedFrom As String, ByVal tn As String) ' As Boolean)
    Event ReReadCredit()

   

#Region " Windows Form Designer generated code "

    Public Sub New(ByRef emp As Employee, ByVal userEntered As Boolean)
        MyBase.New()

        actingManager = New Employee
        empActive = New Employee
        '      actingManager = emp
        '       If currentServer.EmployeeID = 0 Then
        '      currentServer = actingManager
        '     End If

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        usernameEnterOnLogin = userEntered
        If usernameEnterOnLogin = True Then
            actingManager = emp
        End If
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'Manager_Form
        '
        Me.BackColor = System.Drawing.Color.Black
        Me.Name = "Manager_Form"
        Me.Size = New System.Drawing.Size(1024, 768)

    End Sub

#End Region

    Private Sub InitializeOther()

        '    Me.ClientSize = New Size(ssX, ssY)
        PopulateAllTablesWithStatus(False)
        ManagerOpenOrdersCurrencyMan = Me.BindingContext(dsOrder.Tables("OpenOrders"))

        '      currentServer = New Employee
        DisplayMainManager()

    End Sub

    Friend Sub CommingBackFromCloseCheck()


    End Sub


    Friend Sub ReinitializeWithoutLogon(ByVal saveChanges As Boolean, ByVal disposeOrdAdj As Boolean) Handles orderAdj.ReinitializeMain

        If saveChanges = True Then
            SaveChanges_Manager()
        Else
            ReleaseWithoutSaving()
        End If
        If disposeOrdAdj = True Then
            DisposeOrderAdj()
        End If
        DisposeDataViewsOrder() '999

        usernameEnterOnLogin = True
        DisplayMainManager()

        If Not empActive Is actingManager Then

            '    If Me.OperationFlag = True Then
            If weAreClosingDaily = 0 Then
                GenerateOrderTables.PopulateTabsAndTables(empActive, currentTerminal.CurrentDailyCode, False, False, Nothing)
                CreateDataViews(currentServer.EmployeeID, True)
                '       PopulateOpenTabsAndTables()
                mainManager.DisplayOpenTabsAndTables()
            Else
                'this means we assigned a closing daily
                mainManager.ReinitializeOpenTicketsFromForm(weAreClosingDaily)
            End If

        End If
        '    End If

    End Sub

    Private Sub SaveChanges_Manager()

        If dsOrder.HasChanges Then
            GenerateOrderTables.ReleaseCurrentlyHeld()
            GenerateOrderTables.SaveOpenOrderData()
            'not sure????
            currentTable = Nothing
        End If
        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then

        End If

    End Sub

    Private Sub ClosingFromManager_Main() Handles mainManager.DisposeManager

        RaiseEvent DisposingOfManager()

    End Sub

    Private Sub DisposeOrderAdj()
        dvForcePrice.Dispose()
        dvUnAppliedPaymentsAndCredits.Dispose()
        dvPaymentsAndCredits.Dispose()
        orderAdj.Dispose()

    End Sub


    Private Sub ReinitializeWithoutLogonAfterVoidCheck(ByVal tn As Integer) Handles orderAdj.VoidedCheckTableStatusChange
        mainManager.JustVoidedCheck(tn)

    End Sub

    Public Sub DisplayMainManager()

        mainManager = New Manager_Main_UC(usernameEnterOnLogin)
        mainManager.Location = New Point((Me.Width - mainManager.Width) / 2, (Me.Height - mainManager.Height) / 2)
        Me.Controls.Add(mainManager)

    End Sub


    Private Sub DisplayOrderAdjustment(ByVal sender As Object, ByVal e As System.EventArgs, ByVal weClosingDaily As Int64) Handles mainManager.OpenOrderAdjustment
        Dim objButton As New DataSet_Builder.AvailTableUserControl
        If Not sender.GetType Is objButton.GetType Then Exit Sub

        currentTable = New DinnerTable
        weAreClosingDaily = weClosingDaily

        Dim oRow As DataRow
        Dim isCurrentlyHeld As Boolean
        Dim tableNumber As Integer
        Dim tabID As Int64
        Dim tabName As String

        Dim experienceNumber As Int64
        Dim mgrReopen As Boolean
        Dim numberOfChecks As Integer
        Dim numberOfCustomers As Integer
        Dim currentMenu As Integer
        Dim empID As Integer

        If mainManager.ReopenFlag = True Then
            mgrReopen = True
        End If
        objButton = CType(sender, DataSet_Builder.AvailTableUserControl)
        experienceNumber = objButton.ExperienceNumber

        If Not typeProgram = "Online_Demo" Then
            DisplayOrderAdjustmentStep2(experienceNumber, weClosingDaily, mgrReopen)
        Else
            isCurrentlyHeld = GenerateOrderTables.PopulateThisExperience(experienceNumber, False)

            Dim filterString As String = "ExperienceNumber = " & experienceNumber
            Dim NotfilterString As String = " NOT ExperienceNumber = " & experienceNumber

            If objButton.TableNumber = 0 = True Then
                ' is Tab or ticket
                If objButton.TicketNumber = 0 Then
                    '   is tab
                    Demo_FilterDontDelete(dsOrder.Tables("AvailTabs"), dsOrder.Tables("CurrentlyHeld"), filterString) ', NotfilterString) '"ExperienceNumber = '" & experienceNumber & "'")
                    If mainManager.ReopenFlag = False Then
                        dsOrder.Tables("AvailTabs").Clear()
                    End If
                Else
                    ' is ticket
                    Demo_FilterDontDelete(dsOrder.Tables("QuickTickets"), dsOrder.Tables("CurrentlyHeld"), filterString) ', NotfilterString) '"ExperienceNumber = '" & experienceNumber & "'")
                    If mainManager.ReopenFlag = False Then
                        dsOrder.Tables("QuickTickets").Clear()
                    End If
                End If
            Else
                ' is table
                Demo_FilterDontDelete(dsOrder.Tables("AvailTables"), dsOrder.Tables("CurrentlyHeld"), filterString) ', NotfilterString) '"ExperienceNumber = '" & experienceNumber & "'")

                If mainManager.ReopenFlag = False Then  'ReopenIndex > 0 Then
                    dsOrder.Tables("AvailTables").Clear()
                End If
            End If

            dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld") = False
        End If

        Exit Sub
        '222  below





        isCurrentlyHeld = GenerateOrderTables.PopulateThisExperience(experienceNumber, False)
        oRow = dsOrder.Tables("CurrentlyHeld").Rows(0)


        If typeProgram = "Online_Demo" Then
            Dim filterString As String = "ExperienceNumber = " & experienceNumber
            Dim NotfilterString As String = " NOT ExperienceNumber = " & experienceNumber

            If objButton.TableNumber = 0 = True Then
                ' is Tab or ticket
                If objButton.TicketNumber = 0 Then
                    '   is tab
                    Demo_FilterDontDelete(dsOrder.Tables("AvailTabs"), dsOrder.Tables("CurrentlyHeld"), filterString) ', NotfilterString) '"ExperienceNumber = '" & experienceNumber & "'")
                    If mainManager.ReopenFlag = False Then
                        dsOrder.Tables("AvailTabs").Clear()
                    End If
                Else
                    ' is ticket
                    Demo_FilterDontDelete(dsOrder.Tables("QuickTickets"), dsOrder.Tables("CurrentlyHeld"), filterString) ', NotfilterString) '"ExperienceNumber = '" & experienceNumber & "'")
                    If mainManager.ReopenFlag = False Then
                        dsOrder.Tables("QuickTickets").Clear()
                    End If
                End If
            Else
                ' is table
                Demo_FilterDontDelete(dsOrder.Tables("AvailTables"), dsOrder.Tables("CurrentlyHeld"), filterString) ', NotfilterString) '"ExperienceNumber = '" & experienceNumber & "'")

                If mainManager.ReopenFlag = False Then  'ReopenIndex > 0 Then
                    dsOrder.Tables("AvailTables").Clear()
                End If
            End If

            dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld") = False
        End If




        If objButton.TabID <> 0 Then
            tabID = objButton.TabID
            tabName = objButton.Text
            '*************  we will remove the exit sub
            ' we need to add somethinhg that will replace the held by in ExperienceTable and
            ' not let the server save changes
            ' we are allowing managers to enter currently held experiences
            '    just warning them when they do
            If isCurrentlyHeld = True Then
                If MsgBox("Tab " & tabName & " is currently held by " & dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld"), MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                    Exit Sub
                End If
                '     MsgBox("Tab " & tabName & " is currently held by " & dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld"))
            End If

            currentTable.SatTime = objButton.SatTime
            oRow = dsOrder.Tables("CurrentlyHeld").Rows(0)

            If mainManager.ReopenFlag = True Then
                ReopenCheck(True, oRow("TicketNumber"))
            End If

            GenerateOrderTables.PopulateCurrentTableData(oRow)
            GenerateOrderTables.StartOrderProcess(currentTable.ExperienceNumber)

            '444        orderAdj = New Manager_OrderAdj_UC(mainManager.ReopenFlag, objButton.CurrentMenu, objButton.EmpID, True, objButton.ExperienceNumber, objButton.NumberOfChecks, objButton.NumberOfCustomers)

        Else
            If isCurrentlyHeld = True Then
                If MsgBox("Tab " & tabName & " is currently held by " & dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld"), MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                    Exit Sub
                End If
                '     MsgBox("Tab " & tabName & " is currently held by " & dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld"))
            End If

            tableNumber = objButton.TableNumber
            currentTable.SatTime = objButton.SatTime
            oRow = dsOrder.Tables("CurrentlyHeld").Rows(0)

            If mainManager.ReopenFlag = True Then
                ReopenCheck(False, oRow("TicketNumber"))
            End If

            GenerateOrderTables.PopulateCurrentTableData(oRow)
            GenerateOrderTables.StartOrderProcess(currentTable.ExperienceNumber)

            '444      orderAdj = New Manager_OrderAdj_UC(mainManager.ReopenFlag, objButton.CurrentMenu, objButton.EmpID, False, objButton.ExperienceNumber, objButton.NumberOfChecks, objButton.NumberOfCustomers)

        End If

        orderAdj.Location = New Point((Me.Width - orderAdj.Width) / 2, (Me.Height - orderAdj.Height) / 2)
        Me.Controls.Add(orderAdj)
        mainManager.Dispose()

    End Sub

    Friend Sub DisplayOrderAdjustmentStep2(ByVal experienceNumber As Int64, ByVal weClosingDaily As Boolean, ByVal mgrReopen As Boolean)
        ' 2nd will be false from Mgr Swipe
        If typeProgram = "Online_Demo" Then Exit Sub

        '???     currentTable = New DinnerTable
        weAreClosingDaily = weClosingDaily

        Dim oRow As DataRow
        Dim isCurrentlyHeld As Boolean

        '   Dim mgrReopen As Boolean
        Dim numberOfChecks As Integer
        Dim numberOfCustomers As Integer
        Dim currentMenu As Integer
        Dim empID As Integer

        isCurrentlyHeld = GenerateOrderTables.PopulateThisExperience(experienceNumber, False)
        oRow = dsOrder.Tables("CurrentlyHeld").Rows(0)

        If isCurrentlyHeld = True Then
            If MsgBox("Tab " & oRow("TabName") & " is currently held by " & dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld"), MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                Exit Sub
            End If
        End If

        GenerateOrderTables.PopulateCurrentTableData(oRow)
        GenerateOrderTables.StartOrderProcess(experienceNumber) 'currentTable.ExperienceNumber)
        orderAdj = New Manager_OrderAdj_UC(mgrReopen, experienceNumber)
        orderAdj.Location = New Point((Me.Width - orderAdj.Width) / 2, (Me.Height - orderAdj.Height) / 2)
        Me.Controls.Add(orderAdj)
        If Not mainManager Is Nothing Then
            'will be nothing if sent from mgr swipe
            mainManager.Dispose()
        End If

    End Sub


    Private Sub InitiateCloseBatch(ByVal closingDailyCode As Int64) Handles mainManager.CloseBatchManagerForm

        closeBatch = New BatchClose_UC(closingDailyCode)
        closeBatch.Location = New Point(0, 0)
        '   closeBatch.Location = New Point((Me.Width - closeBatch.Width) / 2, (Me.Height - closeBatch.Height) / 2)
        Me.Controls.Add(closeBatch)
        mainManager.Dispose()

    End Sub

    Private Sub BatchCloseComplete(ByVal closingDailyCode As Int64) Handles closeBatch.ExitBatchClose
        Dim info As DataSet_Builder.Information_UC

        closeBatch.Dispose()

        If currentTerminal.CurrentDailyCode = closingDailyCode Then
            '         MsgBox("Current Daily Closed.") ' Restart POS to Select another Daily.")
            currentTerminal.CurrentDailyCode = 0
            RaiseEvent ClosePOS() 'StartExit()
            Exit Sub

        Else
            MsgBox("Daily Closed.  " & currentTerminal.CurrentDailyCode & " is still active.")
            '      info = New DataSet_Builder.Information_UC("Daily Closed.  " & currentTerminal.currentDailyCode & " is still active.")
        End If

        '   info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
        '  Me.Controls.Add(info)
        ' info.BringToFront()

        usernameEnterOnLogin = True
        DisplayMainManager()


    End Sub

    Private Sub BatchExitWithoutClose() Handles closeBatch.ExitWithoutClose

        closeBatch.Dispose()
        usernameEnterOnLogin = True
        DisplayMainManager()
        '     Me.Dispose()

    End Sub




    Private Sub ReopenCheck(ByVal IsTabNotTable As Boolean, ByVal tktNum As Integer)

        Dim oRow As DataRowView

        If IsTabNotTable = True Then
            If tktNum > 0 Then
                For Each oRow In dvQuickTickets
                    If oRow("ExperienceNumber") = mainManager.ReopenIndex Then
                        oRow("ClosedSubTotal") = DBNull.Value
                        oRow("LastStatus") = 2
                        _reopenedTabNotTable = True
                    End If
                    Exit Sub
                Next
            Else
                For Each oRow In dvClosedTabs 'dsOrder.Tables("ClosedTabs").Rows
                    If oRow("ExperienceNumber") = mainManager.ReopenIndex Then
                        oRow("ClosedSubTotal") = DBNull.Value
                        oRow("LastStatus") = 2
                        _reopenedTabNotTable = True
                    End If
                    Exit Sub
                Next
            End If


        Else
            For Each oRow In dvClosedTables 'dsOrder.Tables("ClosedTables").Rows
                If oRow("ExperienceNumber") = mainManager.ReopenIndex Then
                    oRow("ClosedSubTotal") = DBNull.Value
                    oRow("LastStatus") = 2
                    _reopenedTabNotTable = False
                End If
                Exit Sub
            Next
        End If

    End Sub

    Private Sub SavingReopenedCheck222() Handles orderAdj.SaveReopenedCheck
        Dim oROw As DataRow

        '    only do after we close and accept
        '   only gets here if db UP

        Try
            If ReopenedTabNotTable = True Then
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                '        sql222.SqlDataAdapterClosedTabs.Update(dsOrder.Tables("ClosedTabs"))
                sql.cn.Close()
                dsOrder.Tables("ClosedTabs").AcceptChanges()
                '222         GenerateOrderTables.AddStatusChangeData(currentTable.ExperienceNumber, 2, Nothing, Nothing, Nothing)
            ElseIf ReopenedTabNotTable = False Then
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                '         sql222.SqlDataAdapterClosedTables.Update(dsOrder.Tables("ClosedTables"))
                sql.cn.Close()
                dsOrder.Tables("ClosedTables").AcceptChanges()
                '222         GenerateOrderTables.AddStatusChangeData(currentTable.ExperienceNumber, 2, Nothing, Nothing, Nothing)
            End If
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
            If Err.Number = "5" Then
                ServerJustWentDown()
            End If
            MsgBox("You can only reopen a check when connected to the main Server")
        End Try

        '     SaveESCStatusChangeData(2, Nothing, Nothing, Nothing)

    End Sub


    Private Sub PlaceOrder() Handles orderAdj.PlacingOrder


        ' everything below      Dim oRow As DataRow
        '      currentTable.EmployeeID = actingManager.EmployeeID
        '     currentTable.EmployeeNumber = actingManager.EmployeeNumber

        '       PopulateThisExperience(currentTable.ExperienceNumber)
        '      If currentTable Is Nothing Or dsOrder.Tables("CurrentlyHeld").Rows.Count = 0 Then
        '     MsgBox("Table Does Not Exist")
        '    Exit Sub
        '   Else
        '
        '       End If
        '      'as a manager we don't care if it is held
        ''     oRow = dsOrder.Tables("CurrentlyHeld").Rows(0)
        '   PopulateCurrentTableData(oRow)
        '  StartOrderProcess(currentTable.ExperienceNumber)

        RaiseEvent FireOrderScreen()


    End Sub

    Private Sub OverrideTableStatus_Click() Handles mainManager.OverrideTableStatus

        RaiseEvent OverrideTableStatus(True)
        Exit Sub

        '222
        overrideSeating = New Seating_ChooseTable   'Seating_Dining(currentServer.EmployeeID)
        overrideSeating.Location = New Point(0, 0)
        overrideSeating.OverrideAvail = True
        Me.Controls.Add(overrideSeating)
        overrideSeating.BringToFront()

    End Sub
    Private Sub ReReadCredit_Click() Handles mainManager.ReReadCredit

        RaiseEvent ReReadCredit()

    End Sub

    '   **********************
    ' need to fire in Login
    Private Sub OpenNewTable_ButtonHit() Handles mainManager.OpenNewTable

        If typeProgram = "Online_Demo" Then
            dsOrder.Tables("AvailTables").Clear()
        End If

        '     SeatingChart._seatingUsernameEnterOnLogin = True
        RaiseEvent FireSeatingChart(True)
        '       Me.Dispose()
        Exit Sub

        Try
            SeatingChart.AdjustTableColor()
            SeatingChart.Visible = True
            SeatingChart.BringToFront()
        Catch ex As Exception
            '*********          InitializeSeatingChart()
            SeatingChart.AdjustTableColor()
            SeatingChart.Visible = True
            SeatingChart.BringToFront()
        End Try



        SeatingChart = New Seating_ChooseTable   'Seating_Dining(currentServer.EmployeeID)
        SeatingChart.Location = New Point(0, 0)
        Me.Controls.Add(SeatingChart)
        SeatingChart.BringToFront()

    End Sub

    Private Sub OpenNewTab_ButtonHit() Handles mainManager.OpenNewTabEvent

        RaiseEvent FireSeatingTab("Manager", Nothing)

        If typeProgram = "Online_Demo" Then
            dsOrder.Tables("AvailTabs").Clear()
        End If

        Exit Sub
        '
        '       SeatingTab = New Seating_EnterTab(True, Nothing)
        '      SeatingTab.Location = New Point((Me.Width - SeatingTab.Width) / 2, (Me.Height - SeatingTab.Height) / 2)
        '     Me.Controls.Add(SeatingTab)
        '    SeatingTab.BringToFront()
        '   RaiseEvent FiredSeating_EnteredTab()

    End Sub

    Private Sub NewAddNewTab222() '444Handles SeatingTab.OpenNewTabEvent

        Me.Enabled = True
        Dim newTabNameString As String
        newTabNameString = SeatingTab.NewTabName

        '   -999 for tabID will tell it to generate New TabID (which will be experience Number)
        '   later we will have a look-up for returning customers
        OpenNewTab(-999, newTabNameString, True)
        SeatingTab.Dispose()

    End Sub

    Private Sub NewAddNewTakeOutTab222() '444Handles SeatingTab.OpenNewTakeOutTab

        Me.Enabled = True
        Dim newTabNameString As String
        newTabNameString = SeatingTab.NewTabName

        '   -999 for tabID will tell it to generate New TabID (which will be experience Number)
        '   later we will have a look-up for returning customers
        OpenNewTab(-990, newTabNameString, False)
        SeatingTab.Dispose()

    End Sub
    Private Sub CancelNewTab222() '444Handles SeatingTab.CancelNewTab

        '     Me.Enabled = True
        SeatingTab.Dispose()
        '     Me.Dispose()
        usernameEnterOnLogin = True
        DisplayMainManager()

    End Sub

    Private Sub OpenNewTab(ByVal tabId As Int64, ByVal tabName As String, ByVal isDineIn As Boolean)
        Dim expNum As Int64
        Dim tktNum As Integer
        '      Dim satTm As DateTime

        '       If isDineIn = False Then
        '      tktNum = CreateNewTicketNumber()
        '     Else
        If tabId = -888 Or currentTerminal.TermMethod = "Quick" Then
            tktNum = CreateNewTicketNumber()
        Else
            tktNum = 0
        End If
        '    End If

        expNum = CreateNewExperience(currentServer.EmployeeID, Nothing, tabId, tabName, 1, 2, tktNum, 0, currentServer.LoginTrackingID)
        GenerateOrderTables.PopulateThisExperience(expNum, False)

        currentTable = New DinnerTable
        currentTable.ExperienceNumber = expNum
        currentTable.IsTabNotTable = True
        currentTable.TabID = tabId
        currentTable.TabName = tabName
        currentTable.TableNumber = 0
        currentTable.TicketNumber = tktNum
        currentTable.EmployeeID = currentServer.EmployeeID
        currentTable.EmployeeNumber = currentServer.EmployeeNumber
        currentTable.CurrentMenu = currentTerminal.currentPrimaryMenuID '444 primaryMenuID  'this is the system menu - can change during order process
        currentTable.StartingMenu = currentTerminal.currentPrimaryMenuID '444primaryMenuID
        currentTable.NumberOfCustomers = 1      'is 1 when you first open
        currentTable.NumberOfChecks = 1
        currentTable.LastStatus = 2
        currentTable.SatTime = Now
        currentTable.ItemsOnHold = 0
        currentTable.MethodUse = SeatingTab.MethedUse
        DefineMethodDirection()

        StartOrderProcess(currentTable.ExperienceNumber)
        '222      satTm = AddStatusChangeData(currentTable.ExperienceNumber, 2, Nothing, 0, Nothing)
        '     SaveESCStatusChangeData(2, Nothing, 0, Nothing)

        RaiseEvent FireOrderScreen()

    End Sub


    '   *********
    '   Employee Clock Adjustment

    '   Private Sub AdjustingEmployeeClock222() Handles mainManager.AdjustEmpClock
    '
    '       employeeLog = New EmployeeLoggedInUserControl(True)
    '      employeeLog.Location = New Point((Me.Width - employeeLog.Width) / 2, (Me.Height - employeeLog.Height) / 2)
    '     Me.Controls.Add(employeeLog)
    '    employeeLog.BringToFront()
    '
    '   End Sub

    '  Private Sub EndAdjustEmployeeClock222(ByVal sender As Object, ByVal e As System.EventArgs) Handles employeeLog.ClosedEmployeeLog
    '
    '       employeeLog.Dispose()
    '      usernameEnterOnLogin = True
    '     DisplayMainManager()
    '    Me.mainManager.btnEmployees_Click(sender, e)
    '
    '    End Sub

    Private Sub ManagerClosingCheck() Handles orderAdj.MgrClosingCheck

        RaiseEvent MgrClosingCheck()


        Exit Sub
        '   we should have below work at sometime

        Dim oRow As DataRow
        currentTable.EmployeeID = actingManager.EmployeeID
        currentTable.EmployeeNumber = actingManager.EmployeeNumber

        PopulateThisExperience(currentTable.ExperienceNumber, False)

        If currentTable Is Nothing Or dsOrder.Tables("CurrentlyHeld").Rows.Count = 0 Then
            MsgBox("Table Does Not Exist")
            Exit Sub
        Else

        End If
        'as a manager we don't care if it is held
        oRow = dsOrder.Tables("CurrentlyHeld").Rows(0)
        '     UpdateCurrentlyHeld(currentServer.FullName, currentTable.ExperienceNumber)
        PopulateCurrentTableData(oRow)

        StartOrderProcess(currentTable.ExperienceNumber)


    End Sub

    '   Private Sub ManagerCloseCheckExit222() Handles ActiveSplit.ManagerClosing
    '       ActiveSplit.Dispose()
    '   '       ActiveSplit = Nothing
    '       currentTable = Nothing
    '      Me.Dispose()
    ' End Sub

    '   Private Sub SplitCheckClosed222() Handles ActiveSplit.SplitCheckClosing
    '       StartOrderProcess(currentTable.ExperienceNumber)
    '       ActiveMgrOrder = New term_OrderForm(False)
    '      ' not sure about below
    ''      ActiveMgrOrder.Location = New Point(0, 0)
    '      Me.Controls.Add(ActiveMgrOrder)
    '      ActiveMgrOrder.BringToFront()
    ''     ActiveMgrOrder.SplitCheckClosed()
    ' End Sub

    Private Sub TransferingCheck() Handles orderAdj.TransferingCheck
        Dim restrictToItemOnly As Boolean
        Dim oRow As DataRow

        'this is before we raiseEvent
        '      If employeeAuthorization.OperationLevel < systemAuthorization.TransferCheck Then
        '     restrictToItemOnly = True
        '    End If

        With dvCloseCheck
            .Table = dsOrder.Tables("PaymentsAndCredits")
            .RowFilter = "CheckNumber = " & currentTable.CheckNumber
        End With

        If dvCloseCheck.Count = 0 Then
            transCheck = New Manager_Transfer_UC(orderAdj.TransSIN, orderAdj.TransName, currentTable.CheckNumber, currentTable.ExperienceNumber, False, restrictToItemOnly)
            transCheck.Location = New Point((Me.Width - transCheck.Width) / 2, (Me.Height - transCheck.Height) / 2)

            Me.Controls.Add(transCheck)
            transCheck.BringToFront()
        Else
            MsgBox("You may NOT transfer a check if a Payment has been applied.")
        End If

    End Sub

    Private Sub TransUC_Closed(ByVal releasingTable As Boolean) Handles transCheck.UpdatingCurrentTables
        If releasingTable = True Then
            CalculateClosingTotal()
            GenerateOrderTables.ReleaseTableOrTab()
            GenerateOrderTables.PopulateAllTablesWithStatus(False)

            '         GenerateOrderTables.ReleaseCurrentlyHeld()
            '        GenerateOrderTables.SaveOpenOrderData()
            '       currentTable = Nothing
            '      orderAdj.Dispose()
            ReinitializeWithoutLogon(True, True)
        End If
        transCheck.Dispose()
        '     SplitDisposeSelf()
        '     RaiseEvent UpdatingAfterTransfer()

    End Sub

    Private Sub StartManagerReports() Handles mainManager.OpenReports

        'reports should be built in Manager_Main
        'we should not dispose of Maanager_Mail
        reportManager = New DataSet_Builder.Manager_Reports_UC(connectserver, companyInfo, currentTerminal, connectserver, typeProgram) ' DataSet_Builder.Reports_EmployeeHours '
        reportManager.Location = New Point((Me.Width - reportManager.Width) / 2, (Me.Height - reportManager.Height) / 2)
        Me.Controls.Add(reportManager)

    End Sub

    Private Sub ExitReports_Selected() Handles reportManager.ExitReports

        reportManager.Dispose()
        RaiseEvent DisposingOfManager()

        'reports should be built in Manager_Main
        'we should not dispose of Maanager_Main
        'then we can do this:
        '    usernameEnterOnLogin = True
        '   DisplayMainManager()


    End Sub
    Private Sub ClosePOS_Selected() Handles mainManager.StartExit

        RaiseEvent ClosePOS()

    End Sub





    Private Sub DisposedActiveMgrOrder222(ByVal emp As Employee) ' Handles ActiveMgrOrder.TermOrder_Disposing

        GenerateOrderTables.ReleaseCurrentlyHeld()
        GenerateOrderTables.SaveOpenOrderData()
        '     dsOrder.Tables("OpenOrders").Rows.Clear()

        currentTable = Nothing

        '666      Me.Dispose()
        '666  ActiveMgrOrder = Nothing

    End Sub
    Private Sub CancelNewTable222(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles SeatingChart.NoTableSelected
        usernameEnterOnLogin = True
        DisplayMainManager()


    End Sub
    Private Sub NewAddNewTable222() ' Handles SeatingChart.NumberCustomerEvent

        Dim oRow As DataRow
        Dim expNum As Int64
        Dim numCust As Integer
        Me.Enabled = True
        isBartender = False     '*** may change to dynamic for rest.
        Dim satTm As DateTime

        For Each oRow In dsOrder.Tables("AllTables").Rows    'currentPhysicalTables
            If oRow("TableNumber") = SeatingChart.TableSelected Then

                numCust = SeatingChart.NumberCustomers
                expNum = CreateNewExperience(currentServer.EmployeeID, SeatingChart.TableSelected, Nothing, Nothing, numCust, 2, 0, 0, currentServer.LoginTrackingID)
                PopulateThisExperience(expNum, False)

                currentTable = New DinnerTable
                currentTable.ExperienceNumber = expNum
                currentTable.IsTabNotTable = False
                currentTable.TableNumber = SeatingChart.TableSelected
                currentTable.TabID = 0
                currentTable.TabName = SeatingChart.TableSelected.ToString
                currentTable.TicketNumber = 0
                currentTable.EmployeeID = currentServer.EmployeeID
                currentTable.EmployeeNumber = currentServer.EmployeeNumber
                currentTable.ExperienceNumber = currentServer.EmployeeNumber
                currentTable.CurrentMenu = currentTerminal.currentPrimaryMenuID '444primaryMenuID
                currentTable.StartingMenu = currentTerminal.currentPrimaryMenuID '444primaryMenuID
                currentTable.NumberOfChecks = 1
                currentTable.NumberOfCustomers = numCust
                currentTable.LastStatus = 2
                currentTable.SatTime = Now
                currentTable.ItemsOnHold = 0

                '   **** might have to have a check for the bartenders on which employee this is
                SeatingChart.Visible = False
                StartOrderProcess(currentTable.ExperienceNumber)

                '     satTm = AddStatusChangeData(currentTable.ExperienceNumber, 2, Nothing, 0, Nothing)
                '          SaveESCStatusChangeData(2, Nothing, 0, Nothing)

                RaiseEvent FireOrderScreen()

                '                ActiveMgrOrder = New term_OrderForm  'isBartender)
                '               ActiveMgrOrder.Location = New Point(0, 0)
                '              Me.Controls.Add(ActiveMgrOrder)
                '             ActiveMgrOrder.BringToFront()
                Exit For
            End If
        Next

    End Sub

End Class
