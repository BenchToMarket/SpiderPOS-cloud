Imports System.IO
Imports System.Drawing.Printing
Imports DataSet_Builder

Public Class term_OrderForm
    Inherits System.Windows.Forms.UserControl

    '   Friend currentTable As New DinnerTable

    '   Private openCheck As Check

    '   Private _dinnerTableCollection As New DinnerTableCollection
    '    Private _checks As CheckCollection

    Dim WithEvents ActiveSplit As SplitChecks      'form
    Dim WithEvents SpecialItem As SpecialFood
    '444   Dim WithEvents TabEnterScreen As Tab_Screen
    Dim WithEvents TabIdentifierScreen As TabSelection_UC
    Dim WithEvents cashPaymentDue As DataSet_Builder.Information_UC
    Dim WithEvents repeatOrderUserControl As LastOrder_UC
    Dim WithEvents modifyOrderUserControl As ModifyOrder_UC
    Dim WithEvents info As DataSet_Builder.Information_UC
    Dim WithEvents changeNumberOfCustomers As DataSet_Builder.NumberOfCustomers_UC
    Dim WithEvents extraNoUserControl As ExtraNo_UC
    Dim WithEvents demoHelp As DemoInformation_UC
    Dim WithEvents drinkPrep As New DrinkPrep_UC
    Friend WithEvents SeatingTab As Seating_EnterTab

    '   Private sql As New DataSet_Builder.SQLHelper(connectserver)
    '   Private prt As New PrintHelper

    Dim tabDoubleClickTimer As Timer
    Dim tabTimerActive As Boolean


    '   ***********************************************************
    '   Defines the names, and values which will determine location
    '   and size of all panels
    '   ***********************************************************


    '************************************************************************
    '   For both Main and MainModifier Panels
    '   (allow user to choose initial foods and modifiers)
    '************************************************************************

    '   Define both main and mainModifer panels
    Friend WithEvents pnlMain As New Panel
    Friend WithEvents pnlMain2 As New Panel
    Friend WithEvents pnlMain3 As New Panel
    Dim pastFirstCategory As Boolean = False
    Dim firstCategory As New OrderButton("10")

    '********************************************************************************
    '   For Both Order Panel and OrderModifier Panel
    '********************************************************************************

    Friend WithEvents pnlOrder As New Panel
    Friend WithEvents pnlOrderQuick As New Panel
    Friend WithEvents pnlOrderDrink As New Panel
    Friend WithEvents pnlOrderModifier As New Panel
    Friend WithEvents pnlOrderModifierExt As New Panel
    Friend WithEvents pnlDescription As New Panel

    Dim panelButtonWidth As Double
    Dim panelButtonHeight As Double

    '   position and size of Order Panel (op)
    Dim opLocationX As Double = ssX * 0.32
    Dim opLocationY As Double = ssY * 0.01
    Dim opWidth As Double = ssX * 0.54 '.50
    Dim opHeight As Double = ssY * 0.76 '.80

    Dim opButtonWidth As Double = (opWidth - (5 * buttonSpace)) / 4
    Dim opButtonHeight As Double = (opHeight - (9 * buttonSpace)) / 8     'mathmatically should be 9
    Dim drinkButtonWidth As Double = (opWidth - (7 * buttonSpace)) / 6 '(opHeight - (13 * buttonSpace)) / 12
    Dim drinkButtonHeight As Double = (opHeight - (12 * buttonSpace)) / 10 '(opHeight - (13 * buttonSpace)) / 12

    '   position and size of modifier panel (mp)
    Dim mpLocationX As Double = ssX * 0.45  '0.38
    Dim mpLocationY As Double = ssY * 0.18   '0.31
    Dim mpWidth As Double = ssX * 0.42
    Dim mpHeight As Double = ssY * 0.59
    Dim mpButtonWidth As Double = (mpWidth - (5 * buttonSpace)) / 4
    Dim mpButtonHeight As Double = (mpHeight - (8 * buttonSpace)) / 7

    '   Buttons for the order(op) and modifier(mp) panel
    Private btnMain(20) As OrderButton
    Private btnModifier(10) As OrderButton
    Private WithEvents btnMainNext As OrderButton
    Private WithEvents btnMainNextMain3 As OrderButton
    Private WithEvents btnMainPrevious As OrderButton
    '    Private btnModifier() As OrderButton        'do not use any more
    Private btnOrder(32) As OrderButton
    Private btnOrderQuick(60) As OrderButton
    Private btnOrderDrink(60) As OrderButton
    Private btnOrderModifier(60) As OrderButton         'changed from 24
    Private btnOrderModifierExt(60) As OrderButton
    Private btnOrderModifierCancel As OrderButton

    Dim opButtonText() As String
    Dim opButtonId() As Integer
    Dim opButtonBackColor() As Color
    Dim opButtonForeColor() As Color
    Dim opButtonCategoryID() As Integer
    Dim opButtonHalfSplit() As Boolean
    Dim opButtonFunctionID() As Integer
    Dim opButtonFunctionGroupID() As Integer
    Dim opButtonFunFlag() As String
    Dim opButtonDrinkSubCat() As Boolean
    Friend tabIdentifierDisplaying As Boolean
    Public tabScreenDisplaying As Boolean

    '*****************************************************************
    '   Current Order View with totals and kitchen commands
    '*****************************************************************
    '  Private WithEvents viewOrder As New ListView 'CurrentOrderView

    Private WithEvents testgridview As OrderGridView

    '   Friend WithEvents tableStatusView As New ListView
    '   Friend WithEvents byServerView As New ListView

    '   descriptions of all status categories
    Friend statusName(10) As String

    Private pnlMainModifier As New Panel
    Private WithEvents btnModifierNo As KitchenButton
    Private WithEvents btnModifierAdd As KitchenButton
    Private WithEvents btnModifierExtra As KitchenButton
    Private WithEvents btnModifierOnFly As KitchenButton
    Private WithEvents btnModifierNoMake As KitchenButton
    Private WithEvents btnModifierOnSide As KitchenButton
    Private WithEvents btnModifierNoCharge As KitchenButton
    Private WithEvents btnModifierSpecial As KitchenButton
    Private WithEvents btnModifierRepeat As KitchenButton
    Private btnModifierBlank As KitchenButton
    Dim WithEvents specialKeyboard As DataSet_Builder.KeyBoard_UC
    Dim ADDorNOmode As Boolean

    Dim panelModLocationY As Integer


    '   Table Information Panel
    Dim pnlTableInfo As New Panel
    Dim WithEvents btnTableInfoMenu As New Button
    Dim WithEvents btnTableInfoServerNumber As New Button
    Dim WithEvents btnTableInfoTableNumber As New Button
    Dim WithEvents btnTableInfoNumberOfCustomers As New Button
    Dim tableMethodChanged As Boolean = False
    Friend wasPickupMethod As Boolean

    Dim WithEvents customerPanel As Panel
    Dim btnCustomer(9) As KitchenButton
    Dim WithEvents btnCustomerNext As KitchenButton
    '  Dim currenttable.markForNewCustomerPanel As Boolean
    '   Dim currenttable.markForNextCustomer As Boolean

    '    Friend WithEvents pnlDrinkModifier As New Panel
    '   Friend WithEvents drinkTall As Button
    '   Friend WithEvents drinkDouble As Button
    '  Friend WithEvents drinkRocks As Button
    '   Friend WithEvents drinkSplash As Button
    '  Friend WithEvents drinkCall As Button
    ' Friend WithEvents drinkUp As Button
   
    Friend WithEvents pnlWineParring As New Panel
    Friend WithEvents lblWineParring As Label
    Friend WithEvents lblRecipe As Label

    Friend WithEvents pnlPizzaSplit As New Panel
    Friend pnlOnFullPizza As New Panel
    Friend pnlOnFirstHalf As New Panel
    Friend pnlOnSecondHalf As New Panel
    Friend WithEvents onFullPizza As New ListBox    'DataGrid
    Friend WithEvents onFirstHalf As New ListBox 'DataGrid
    Friend WithEvents onSecondHalf As New ListBox   'DataGrid


    '    Friend numberFree(15) As Integer
    Friend freeFood(23) As Boolean
    Friend freeFoodActive As Boolean
    Friend modifierIndex As Integer
    Friend categoryIndex() As Boolean
    Friend GTCIndex As Integer = -1
    Dim isSecondLoop As Boolean
    Dim onlyHalf As Boolean

    Dim performedIndividualJoinTest As Boolean
    Dim cntModifierLoop As Integer
    Dim previousCategory As Integer
    Friend modifierIndexSecondLoop As Integer
    Friend categoryIndexSecondLoop() As Boolean

    Friend _IsBartenderMode As Boolean
    Friend _isManagerMode As Boolean
    Friend TabAccountInfo As New DataSet_Builder.Payment

    Friend Property IsBartenderMode() As Boolean
        Get
            Return _IsBartenderMode
        End Get
        Set(ByVal Value As Boolean)
            _IsBartenderMode = Value
        End Set
    End Property

    Friend Property IsManagerMode() As Boolean
        Get
            Return _isManagerMode
        End Get
        Set(ByVal Value As Boolean)
            _isManagerMode = Value
        End Set
    End Property



    '   Dim currentTableList As New ArrayList
    '   Event DisposeTableScreen(ByVal sender As Object, ByVal e As System.EventArgs)
    Event TermOrder_Disposing(ByVal emp As Employee, ByRef ccDisplay As CashClose_UC)
    Event QuickOrder_NotDisposing()
    Event ClosingCheck()
    Event ClosingCheckExit()
    Event FireTabScreen(ByVal startInSearch As String, ByVal searchCriteria As String)
    Event TestForCurrentTabInfo()
    Event TabScreenDisposing()
    Event FireSeatingTab(ByVal startedFrom As String, ByVal tn As String) ' As Boolean)
    Event CloseFastCash()

    '   Friend orderInactiveTimer As System.Windows.Forms.Timer
    Private orderTimeoutCounter As Integer = 1



#Region " Windows Form Designer generated code "

    Public Sub New(ByVal barMode As Boolean, ByVal managerMode As Boolean, ByVal _tabAccountInfo As DataSet_Builder.Payment)
        '(ByVal cm As Integer, ByVal empID As Integer, ByVal IsTab As Boolean, ByVal tn As Integer, ByVal tabName As String, ByVal expNum As Integer, ByVal numChecks As Integer, ByVal numCust As Integer, ByVal barMode As Boolean, ByVal tktNum As Integer, ByVal ls As Integer)
        MyBase.New()

        'moved to 2nd step
        '       If currentTerminal.CurrentDailyCode = 0 Then
        'MsgBox("No Daily Business Day Active. Please See Manager.")
        '    Me.Dispose()
        '   End If

        Me._IsBartenderMode = barMode
        Me._isManagerMode = managerMode
        TabAccountInfo = _tabAccountInfo

        '      inBarMode = barMode

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        InitializeOther()

        '     time2 = Now
        '    timeDiff = time2.Subtract(time1)
        '   MsgBox(timeDiff.ToString)

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
        'term_OrderForm
        '
        Me.Name = "term_OrderForm"
        Me.Size = New System.Drawing.Size(1024, 768)

    End Sub

#End Region

    Private Sub InitializeOther()

        orderTimeoutCounter = 1
        '      orderInactiveTimer = New Timer
        orderInactiveTimer.Interval = timeoutInterval

        If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            AddHandler orderInactiveTimer.Tick, AddressOf OrderInactiveScreenTimeout
            orderInactiveTimer.Start()
        End If
        '    ResetTimer()

        ReformatControls()
        CreateFormView()

        testgridview = New OrderGridView
        If currentServer.Lefty = False Then
            Me.testgridview.Location = New Point(buttonSpace, buttonSpace)
        Else
            Me.testgridview.Location = New Point(Me.Width - buttonSpace - 312, buttonSpace)
        End If
        Me.Controls.Add(Me.testgridview)
        Me.testgridview.BringToFront()

        ' this UC is different, we are only making once
        Me.extraNoUserControl = New ExtraNo_UC
        Me.extraNoUserControl.Location = New Point(opLocationX + 20, opLocationY + 70)
        Me.Controls.Add(Me.extraNoUserControl)
        Me.extraNoUserControl.Visible = False

        DisplayCustomerPanel()

        '      CreateCourseButtonPanel()

        CreateMainModifierPanel()

        CreateTableInfoPanel()

        CreateDrinkModifierPanel()

        '*****************************************************************************
        '   Populates Buttons from Database for Order Process
        '*****************************************************************************

        CreateMainButtonArray(panelButtonWidth, panelButtonHeight)

        '   If Not currentTerminal.TermMethod = "Quick" Then



        CreateOrderButtonArray()
        CreateOrderButtonQuick()
        CreateOrderDrinkButtonArray()
        CreateOrderModifierButtonArray()
        CreateOrderModifierExtendedButtonArray()

     
        '      If currentTerminal.TermMethod = "Quick" Then
        ' so we won't get error below
        '444     TabEnterScreen = New Tab_Screen() '"Phone")
        '      TabEnterScreen.Location = New Point(((Me.Width - TabEnterScreen.Width - 10) / 2), ((Me.Height - TabEnterScreen.Height) / 2))
        '     Me.Controls.Add(TabEnterScreen)
        '    TabEnterScreen.Visible = False
        '   tabIdentifierDisplaying = False

        InitializeScreenSecondStep()


        If typeProgram = "Online_Demo" And acitveDemo = True Then
            Dim s1 As String = "CLOSE CHECK - Hit the yellow button below check detail"
            Dim s2 As String
            Dim s3 As String

            If Not currentTerminal.TermMethod = "Quick" And Not currentTerminal.TermMethod = "Bar" Then
                s2 = "SELECT COURSE - Hit buttons (1 to 4) below check detail"
                s3 = "SELECT CUSTOMER - Hit buttons (1 to 5) below this panel"
            Else
                s2 = "SELECT QUANTITY - Hit buttons (1 to 4) below check detail.              couse buttons will display for non Bar terminals"
                If currentTerminal.TermMethod = "Bar" Then
                    s3 = "SELECT CUSTOMER - Hit buttons (1 to 5) below this panel"
                Else
                    s3 = ""
                End If
            End If
            demoHelp = New DemoInformation_UC(s1, s2, s3, "", "", True)
            demoHelp.Location = New Point(((Me.Width - demoHelp.Width) / 2), ((Me.Height - demoHelp.Height) / 2))
            Me.Controls.Add(demoHelp)
            demoHelp.BringToFront()
        End If

    End Sub

    Friend Sub InitializeScreenSecondStep()

        If currentTerminal.CurrentDailyCode = 0 Then
            MsgBox("No Daily Business Day Active. Please See Manager.")
            '            Me.Visible ???? then exit
            Me.Dispose()
        End If

        freeFoodActive = False
        modifierIndex = False
        isSecondLoop = False
        onlyHalf = False
        tableMethodChanged = False
        performedIndividualJoinTest = False

        tabScreenDisplaying = False
        tabIdentifierDisplaying = False
        tabTimerActive = False
        pnlMain2.Visible = False
        pnlMain3.Visible = False

        PutUsInNormalMode()

        '      pnlMain.Visible = True
        '      drinkPrep.Visible = False
        '     pnlOrderModifier.Visible = False
        '    pnlOrderModifierExt.Visible = False
        '    pnlPizzaSplit.Visible = False
        '   pnlWineParring.Visible = True
        '    GTCIndex = -1
        '     pnlOrderDrink.Visible = False
        '      ADDorNOmode = False
        '     ChangeFromDrinkButtons()

        If currentTable.MethodUse = "Pickup" Then
            wasPickupMethod = True
        Else
            wasPickupMethod = False
        End If
        'when go to visible, we need to move this to second step
        If Not currentTable.TabID = -888 And Not currentTerminal.TermMethod = "Quick" Then 'And Not IsBartenderMode = True 
            FirstStepOrdersPending()
        Else
            '444 moved RunFoodsRoutine option below
        End If

        'added below for Visible
        If Not currentTerminal.TermMethod = "Quick" Then
            btnModifierExtra.Visible = False
            btnModifierBlank.Visible = False
            btnModifierRepeat.Visible = True
            btnModifierOnFly.Visible = True
            btnModifierNoMake.Visible = True
            btnModifierNoCharge.Visible = True
        Else
            btnModifierRepeat.Visible = False
            btnModifierOnFly.Visible = False
            btnModifierNoMake.Visible = False
            btnModifierNoCharge.Visible = False

            btnModifierExtra.Visible = True
            btnModifierBlank.Visible = True
        End If

        '444    UpdateTableInfo()
        PopulateMainButtons()
        ReInitializeOrderView()
        ResetCustomerAndCourseButtons()

        If Me.pastFirstCategory = True Then
            Dim index As Integer
            For index = 1 To 10
                If btnMain(index).CategoryID > 0 Then
                    RunFoodsRoutine(btnMain(index))
                    Exit For
                End If
            Next
        End If
        UpdateTableInfo()
        If currentTable.CurrentMenu = currentTerminal.currentPrimaryMenuID Then
            btnTableInfoMenu.BackColor = c2
            btnTableInfoMenu.ForeColor = c3
        Else
            btnTableInfoMenu.BackColor = c3
            btnTableInfoMenu.ForeColor = c2
        End If
        '    currentTerminal.CurrentMenuID = tempSecondaryMenuID
        '   currentTable.CurrentMenu = tempSecondaryMenuID

    End Sub

    Friend Sub ReinitializeOrderScreen222()

        UpdateTableInfo()
        Me.testgridview.CalculateSubTotal()

    End Sub

    Private Sub DisableDemoHelp_Selected() Handles demoHelp.DisableDemoHelp
        acitveDemo = False
    End Sub

    Private Sub FirstStepOrdersPending()
        Dim lastOrderNumber As Int64

        lastOrderNumber = CheckForOpenOrderDetail()

        If lastOrderNumber > 0 Then
            FillRepeatOrderDataTable(lastOrderNumber)
            DisplayRepeatOrder(False)
            repeatOrderUserControl.OrderNumber = lastOrderNumber
        Else
            '         If Not TabAccountInfo.LastName Is Nothing Then
            '        If TabAccountInfo.LastName.Length > 0 Then
            '       StartNewCustomerTab()
            '  End If
            ' End If
        End If

    End Sub

    Private Sub UserControlHit() Handles testgridview.UC_Hit, SpecialItem.UC_Hit

        ResetTimer()

    End Sub

    Private Sub ResetTimer()

        orderTimeoutCounter = 1

        '       orderInactiveTimer.Dispose()
        '       Me.orderInactiveTimer.Stop()
        '       orderInactiveTimer = New Timer
        ''      AddHandler orderInactiveTimer.Tick, AddressOf OrderInactiveScreenTimeout

        '     orderInactiveTimer.Interval = timeoutInterval
        '    orderInactiveTimer.Start()

        '  orderInactiveTimer.Start()

    End Sub
    Private Sub OrderInactiveScreenTimeout(ByVal sender As Object, ByVal e As System.EventArgs)

        Exit Sub

        orderTimeoutCounter += 1

        '      If orderTimeoutCounter = 2 Or orderTimeoutCounter > 7 Then
        '     MsgBox(orderTimeoutCounter)
        '    End If

        If orderTimeoutCounter = companyInfo.timeoutMultiplier Then
            LeaveAndSave()
        End If

    End Sub

    Private Sub ReformatControls()

        'uc      Me.ClientSize = New Size(ssX, ssY)
        'uc    Me.FormBorderStyle = FormBorderStyle.None

        '   defines location of main panel (and modifier relative to main)
        Dim panelLocationX As Integer = ssX * 0.88
        Dim panelLocationY As Integer = ssY * 0.03
        '   DEFINES WIDTHS (percentage of screen)
        Dim mainPanelWidth As Integer = ssX * 0.1
        Dim mainPanelHeight As Integer = ssY * 0.72
        panelButtonWidth = mainPanelWidth - (2 * buttonSpace)
        panelButtonHeight = ((mainPanelHeight - (12 * buttonSpace)) / 11)


        '   position and size of description panel
        Dim dpLocationX As Double = ssX * 0.4
        Dim dpLocationY As Double = ssY * 0.01
        Dim dpWidth As Double = ssX * 0.3
        Dim dpHeight As Double = ssY * 0.2

        '   position and size of adjustment panel
        Dim apLocationX As Double = ssX * 0.31
        Dim apLocationY As Double = ssY * 0.35
        Dim apWidth As Double = ssX * 0.44
        Dim apHeight As Double = ssY * 0.5

        If currentServer.Lefty = True Then
            panelLocationX = ssX * 0.02
            opLocationX = ssX * 0.13
            mpLocationX = ssX * 0.25
            dpLocationX = (ssX * 0.2) + (2 * buttonSpace)

        End If

        '***************************************************************************
        '   Assigns location and size of panels
        '***************************************************************************

        '   Main and MainModifier Panels
        '   places the first button inside the panel one space right(pmslX) and down(pmslY)
        '   then each one is moved down by a space and a button size(pmbsY)
        Me.pnlMain.Location = New System.Drawing.Point(panelLocationX, panelLocationY)
        Me.pnlMain2.Location = New System.Drawing.Point(panelLocationX, panelLocationY)
        Me.pnlMain3.Location = New System.Drawing.Point(panelLocationX, panelLocationY)
        Me.pnlMain.Size = New System.Drawing.Size(mainPanelWidth, mainPanelHeight)
        Me.pnlMain2.Size = New System.Drawing.Size(mainPanelWidth, mainPanelHeight)
        Me.pnlMain3.Size = New System.Drawing.Size(mainPanelWidth, mainPanelHeight)
        Me.pnlMain.BorderStyle = BorderStyle.FixedSingle
        Me.pnlMain2.BorderStyle = BorderStyle.FixedSingle
        Me.pnlMain3.BorderStyle = BorderStyle.FixedSingle
        '        Me.pnlMain.BackColor = c8   'BackColor.PowderBlue
        '       Me.pnlMain2.BackColor = c8  'BackColor.PowderBlue
        '      Me.pnlMain3.BackColor = c8  'BackColor.PowderBlue
        panelModLocationY = (panelLocationY + mainPanelHeight + (2 * buttonSpace))


        '   Order and OrderModifier Panels
        Me.pnlOrder.Location = New Point(opLocationX, opLocationY)
        Me.pnlOrderQuick.Location = New Point(opLocationX, opLocationY)
        If Not currentTerminal.TermMethod = "Quick" Then
            Me.pnlOrder.Size = New Size(opWidth, opHeight)
            Me.pnlOrderQuick.Size = New Size(opWidth, opHeight)
        Else
            Me.pnlOrder.Size = New Size(opWidth, opHeight)
            Me.pnlOrderQuick.Size = New Size(opWidth, opHeight) ' + 47)
        End If

        Me.pnlOrder.BorderStyle = BorderStyle.FixedSingle
        Me.pnlOrderQuick.BorderStyle = BorderStyle.FixedSingle
        '        Me.pnlOrder.BackColor = c5
        Me.pnlOrderDrink.Location = New Point(opLocationX, opLocationY)
        Me.pnlOrderDrink.Size = New Size(opWidth, opHeight)
        Me.pnlOrderDrink.BorderStyle = BorderStyle.FixedSingle
        '      Me.pnlOrderDrink.BackColor = c5

        Me.drinkPrep.Location = New Point(opLocationX, opLocationY)
        '   Me.pnlOrderDrink.Size = New Size(opWidth, opHeight)
        '  Me.pnlOrderDrink.BorderStyle = BorderStyle.FixedSingle

        Me.pnlOrderModifier.Location = New Point(mpLocationX, mpLocationY)
        Me.pnlOrderModifier.Size = New Size(mpWidth, mpHeight)
        Me.pnlOrderModifier.BorderStyle = BorderStyle.FixedSingle

        Me.pnlOrderModifierExt.Location = New Point(opLocationX, opLocationY)
        Me.pnlOrderModifierExt.Size = New Size(opWidth, opHeight)
        Me.pnlOrderModifierExt.BorderStyle = BorderStyle.FixedSingle

        '   Description Panel
        '   ****    ???????????????????
        Me.pnlDescription.Location = New Point(dpLocationX, dpLocationY)
        Me.pnlDescription.Size = New Size(dpWidth, dpHeight)
        Me.pnlDescription.Visible = False
        '    Me.pnlDescription.BackColor = Color.PaleTurquoise
        '      Me.pnlDescription.BorderStyle = BorderStyle.FixedSingle
        Me.pnlDescription.Text = "Description"

    End Sub

    Private Sub CreateFormView()

        '
        'OrderForm
        '
        Me.Controls.Add(Me.pnlDescription)
        Me.Controls.Add(Me.pnlOrderModifier)
        Me.Controls.Add(Me.pnlOrderModifierExt)
        Me.Controls.Add(Me.pnlOrderDrink)
        Me.Controls.Add(Me.drinkPrep)
        Me.Controls.Add(Me.pnlOrder)
        Me.Controls.Add(Me.pnlOrderQuick)
        Me.Controls.Add(Me.pnlMainModifier)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlMain2)
        Me.Controls.Add(Me.pnlMain3)
        Me.pnlMain2.Visible = False
        Me.pnlMain3.Visible = False

        Me.pnlOrder.Visible = False
        Me.pnlOrderQuick.Visible = False
        Me.pnlOrderDrink.Visible = False
        Me.drinkPrep.Visible = False
        Me.pnlOrderModifier.Visible = False
        pnlOrderModifierExt.Visible = False
        '      Me.pnlDrinkModifier.Visible = False
        Me.BackColor = c2 'c12
        Me.Text = "Active Table: "

        '     Me.Controls.Add(Me.pnlDrinkModifier)
        Me.Controls.Add(Me.pnlWineParring)
        Me.Controls.Add(Me.pnlTableInfo)
        Me.Controls.Add(Me.pnlPizzaSplit)

    End Sub

    Friend Sub ReInitializeOrderView()

        Me.testgridview.InitializeViewSecondStep()
        Me.testgridview.gridViewOrder.DataSource = dvOrder
        currentTable.OrderView = "Detail"

    End Sub

    Private Sub ResetCustomerAndCourseButtons()

        Dim index As Integer

        For index = 2 To 5
            btnCustomer(index).BackColor = c7
        Next
        currentTable.MarkForNewCustomerPanel = False
        currentTable.MarkForNextCustomer = False
        currentTable.CustomerNumber = 1
        currentTable.NextCustomerNumber = 1
        currentTable.CourseNumber = 2
        currentTable.Quantity = 1

        ChangeCustomerButtonColor(c9)

         If Not currentTerminal.TermMethod = "Quick" And Not currentTerminal.TermMethod = "Bar" Then
            testgridview.ChangeCourseButton(currentTable.CourseNumber)
        Else
            testgridview.ChangeCourseButton(currentTable.Quantity)
        End If

    End Sub

    Private Sub StartNewQuickService(ByVal expNum As Int64) Handles testgridview.NewQuickServiceOrder

        If EndOfItem(True) = False Then Exit Sub

        StartOrderProcess(expNum)
        UpdateTableInfo()
        Me.testgridview.CalculateSubTotal()

        '      If currentTable.MethodUse = "Delivery" Then
        '     If dsOrder.Tables("OpenOrders").Rows.Count = 0 And currentTable.IsClosed = False Then Exit Sub
        '    StartDeliveryMethod()
        '   End If

    End Sub

    Private Sub FastCashClose(ByVal sender As Object, ByVal e As System.EventArgs) Handles testgridview.CloseFast 'totalOrder.Click
        UserControlHit()
       
        If EndOfItem(True) = False Then Exit Sub

        If Not currentTerminal.TermMethod = "Bar" And Not currentTerminal.TermMethod = "Quick" Then
            Exit Sub
        End If
      
        If companyInfo.fastCashClose = False Then Exit Sub

        Dim oRow As DataRow
        Dim hasAnyPaymentApplied As Boolean
        Dim info As DataSet_Builder.Information_UC

        If currentTable.NumberOfChecks > 1 Then
            info = New DataSet_Builder.Information_UC("You can not Fast Close a table with mulitple checks.")
            info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            Me.Controls.Add(info)
            info.BringToFront()
            Exit Sub
        End If


        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("Applied") = True Then
                    hasAnyPaymentApplied = True
                End If
            End If
        Next

        If hasAnyPaymentApplied = True Then
            info = New DataSet_Builder.Information_UC("You can not Fast Close with an Applied Payment.")
            info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            Me.Controls.Add(info)
            info.BringToFront()
            Exit Sub
        End If

        '*******
        ' if we get here we are ok to Fast CLose
        RaiseEvent CloseFastCash()


        Exit Sub
        '222 below
        Dim vRow As DataRowView
        Dim checktotal As Decimal
        Dim paymentTotal As Decimal
        Dim remainingBalance As Decimal
        Dim txtForInfo As String
        Dim numItems As Integer

        '     GenerateOrderTables.PopulatePaymentsAndCredits(currentTable.ExperienceNumber)
        For Each vRow In dvOrder
            checktotal += vRow("Price")
            checktotal += (vRow("TaxPrice") + vRow("SinTax"))
            If vRow("sin") = vRow("sii") And Not vRow("ItemID") = 0 Then
                numItems += 1
            End If
        Next

        '   at this point we do not have payments
        '   DO WE LOOK AT APPLIED ???? ***  if we dont look at applied we get the amount not waiting for cc auth
        '   its tricky b/c this really is not fast cash
        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("CheckNumber") = currentTable.CheckNumber Then
                    If oRow("Applied") = False Then
                        paymentTotal += oRow("PaymentAmount")
                        '    oRow("Applied") = 1
                    End If

                End If
            End If
        Next

        checktotal = Format(checktotal, "####0.00")
        paymentTotal = Format(paymentTotal, "####0.00")
        remainingBalance = checktotal - paymentTotal

        If remainingBalance > 0 Then
            CreateNewFastCahPaymentEntry(remainingBalance)
        Else
            '   should have options here
        End If

        txtForInfo = "AMOUNT DUE:  " & remainingBalance
        cashPaymentDue = New DataSet_Builder.Information_UC(txtForInfo)
        cashPaymentDue.Location = New Point((Me.Width - cashPaymentDue.Width) / 2, (Me.Height - cashPaymentDue.Height) / 2)
        cashPaymentDue.NumOfItems = numItems

        Me.Controls.Add(cashPaymentDue)
        '   we need to unenable order form
        cashPaymentDue.BringToFront()

    End Sub

    Private Sub FastCashInfo_Accepted222(ByVal sender As Object, ByVal e As System.EventArgs) Handles cashPaymentDue.AcceptInformation
        UserControlHit()
        Me.testgridview.Enabled = True 'just temp, remove when fast cash works
        Exit Sub '*** we are now closing Fast without this

        Dim oRow As DataRow
        Dim orderBalanceAmount As Decimal
        Dim payBalanceAmount As Decimal
        Dim numItems As Integer
        Dim ccDisplay As CashClose_UC
        '     Dim oDetail As OrderDetailInfo
        '    prt.SendingOrder(oDetail)
        '   prt.SendingOrder222()

        If EndOfItem(False) = False Then Exit Sub
        numItems = cashPaymentDue.NumOfItems
        SendingOrderRoutine()

        '       GenerateOrderTables.UpdatePaymentsAndCredits()
        Dim fastClosePrint As New CloseCheck(currentTable.CheckNumber)


        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                orderBalanceAmount += (oRow("Price"))
                orderBalanceAmount += (oRow("TaxPrice") + oRow("SinTax"))
            End If
        Next

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("Applied") = True Then
                    payBalanceAmount += oRow("PaymentAmount")
                Else
                    oRow("Applied") = True
                End If

                oRow("DailyCode") = currentTable.DailyCode
                oRow("PaymentDate") = Now
                oRow("TerminalsOpenID") = currentTerminal.TerminalsOpenID

            End If
        Next

        fastClosePrint.PrintingFromFastCash(payBalanceAmount)
        '444      tmrCardRead.Stop()
        '444  RemoveHandler tmrCardRead.Tick, AddressOf fastClosePrint.readAuth.tmrCardRead_Tick

        If orderBalanceAmount - payBalanceAmount <= 0.02 Then
            ccDisplay = New CashClose_UC(numItems, currentTable.TruncatedExpNum, orderBalanceAmount, payBalanceAmount)

            GenerateOrderTables.ReleaseTableOrTab()
            If Not currentTerminal.TermMethod = "Quick" Then
                GenerateOrderTables.PopulateAllTablesWithStatus(False)
            Else
                currentTable.IsClosed = True
                GetReadyForNewTicket()
            End If
        End If

        '444   fastClosePrint.readAuth.Shutdown()
        '444   fastClosePrint.readAuth = Nothing
        fastClosePrint.Dispose()
        fastClosePrint = Nothing
        cashPaymentDue.Dispose()

        LeaveAndSave(ccDisplay)

    End Sub

    Friend Sub GetReadyForNewTicket()
        Me.testgridview.totalOrder.BackColor = c7
        Me.testgridview.totalOrderTax.BackColor = c7
        currentTerminal.NumOpenTickets -= 1
        Me.testgridview.UpdateCheckNumberButton()

    End Sub
    Private Sub FastCashInfo_Rejected(ByVal sender As Object, ByVal e As System.EventArgs) Handles cashPaymentDue.RejectInformation
        Me.testgridview.Enabled = True 'just temp, remove when fast cash works

        dsOrder.Tables("PaymentsAndCredits").RejectChanges()

        cashPaymentDue.Dispose()

    End Sub


    Private Sub CreateNewFastCahPaymentEntry(ByVal amount As Decimal)
        Dim oRow As DataRow = dsOrder.Tables("PaymentsAndCredits").NewRow

        '      oRow("PaymentsAndCreditsID") = DBNull.Value
        oRow("CompanyID") = companyInfo.CompanyID
        oRow("LocationID") = companyInfo.LocationID
        oRow("ExperienceNumber") = currentTable.ExperienceNumber
        oRow("EmployeeID") = currentTable.EmployeeID
        oRow("CheckNumber") = currentTable.CheckNumber
        oRow("PaymentTypeID") = DetermineCreditCardID("Cash")
        oRow("PaymentFlag") = "Cash"
        oRow("PaymentAmount") = amount
        oRow("Surcharge") = CType(0, Decimal)
        oRow("Tip") = CType(0, Decimal)
        '      oRow("TipAdjustment") = CType(0, Decimal)
        oRow("Applied") = True
        oRow("AuthCode") = ""

        oRow("SwipeType") = 0
        oRow("TerminalID") = currentTerminal.TermPrimaryKey
        If mainServerConnected = True Then
            oRow("dbUP") = 1
        Else
            oRow("dbUP") = 0
        End If

        If typeProgram = "Online_Demo" Then
            oRow("PaymentsAndCreditsID") = demoPaymentID
            demoPaymentID += 1
        End If
        dsOrder.Tables("PaymentsAndCredits").Rows.Add(oRow)


    End Sub




    Private Function DetermineCurrentStatus222()
        Dim currentStatus As Integer
        Dim oRow As DataRow

        currentStatus = oRow(dsOrder.Tables("StatusChange").Rows.Count - 1)("TableStatusID")

        Return currentStatus

    End Function

    Private Function DetermineCurrentStatusTime222()
        '   not sure if we need this function

        Dim currentStatusTime As DateTime
        Dim oRow As DataRow

        currentStatusTime = oRow(dsOrder.Tables("StatusChange").Rows.Count - 1)("StatusTime")

        Return currentStatusTime

    End Function



    Private Sub CreateTableInfoPanel()
        pnlTableInfo.SuspendLayout()

        '   Table Info Panel
        If currentServer.Lefty = False Then
            pnlTableInfo.Location = New Point(opLocationX - (2 * buttonSpace), viewOrderHeight * 1.2) 'opHeight + (5 * buttonSpace))
        Else
            pnlTableInfo.Location = New Point(ssX * 0.26 + (2 * buttonSpace), viewOrderHeight * 1.2) 'opHeight + (5 * buttonSpace))
        End If

        Dim w As Double = opWidth * 0.27
        Dim h As Double = ssY - (viewOrderHeight * 1.148)
        Dim bHeight As Single = (h / 4) - 1 '(h / 4.4)
        Dim bWidth As Single = w - (4) '(2*buttonspace)
        pnlTableInfo.Size = New Size(w, h)
        pnlTableInfo.BorderStyle = BorderStyle.FixedSingle
        pnlTableInfo.BackColor = c3

        btnTableInfoMenu.Location = New Point(1, 1) '(buttonSpace, 0.5 * buttonSpace)
        btnTableInfoMenu.Size = New Size(bWidth, bHeight)
        btnTableInfoMenu.BackColor = c2
        btnTableInfoMenu.ForeColor = c3
        btnTableInfoMenu.TextAlign = ContentAlignment.MiddleCenter

        btnTableInfoServerNumber.Location = New Point(1, bHeight + 1) '(buttonSpace, bHeight + buttonSpace)
        btnTableInfoServerNumber.Size = New Size(bWidth, bHeight)
        btnTableInfoServerNumber.BackColor = c2
        btnTableInfoServerNumber.ForeColor = c3
        btnTableInfoServerNumber.TextAlign = ContentAlignment.MiddleCenter

        btnTableInfoTableNumber.Location = New Point(1, (2 * bHeight) + 1) '(buttonSpace, (2 * bHeight) + (1.5 * buttonSpace))
        btnTableInfoTableNumber.Size = New Size(bWidth, bHeight)
        btnTableInfoTableNumber.BackColor = c2
        btnTableInfoTableNumber.ForeColor = c3
        btnTableInfoTableNumber.TextAlign = ContentAlignment.MiddleCenter

        btnTableInfoNumberOfCustomers.Location = New Point(1, (3 * bHeight) + 1) '(buttonSpace, (3 * bHeight) + (2 * buttonSpace))
        btnTableInfoNumberOfCustomers.Size = New Size(bWidth, bHeight)
        btnTableInfoNumberOfCustomers.BackColor = c2
        btnTableInfoNumberOfCustomers.ForeColor = c3
        btnTableInfoNumberOfCustomers.TextAlign = ContentAlignment.MiddleCenter

        pnlTableInfo.Controls.Add(btnTableInfoMenu)
        pnlTableInfo.Controls.Add(btnTableInfoServerNumber)
        pnlTableInfo.Controls.Add(btnTableInfoTableNumber)
        pnlTableInfo.Controls.Add(btnTableInfoNumberOfCustomers)

        pnlTableInfo.ResumeLayout()

        UpdateTableInfo()

    End Sub

    Private Sub CreateDrinkModifierPanel()

        Dim dmLocationX As Double
        Dim dmLocationY As Double
        Dim dmWidth As Double
        Dim dmHeight As Double
        Dim bw As Double
        Dim bh As Double

        If currentServer.Lefty = False Then
            dmLocationX = viewOrderWidth + pnlTableInfo.Width + (4 * buttonSpace)
        Else
            dmLocationX = ssX * 0.41
        End If

        dmLocationY = viewOrderHeight * 1.2
        dmWidth = pnlOrder.Width * 0.5     ' - (pnlTableInfo.Width + (3 * buttonSpace))
        dmHeight = pnlTableInfo.Height

        bw = (dmWidth - (5 * buttonSpace)) / 6
        bh = (dmHeight - (3 * buttonSpace)) / 6

        Me.pnlWineParring.Location = New Point(dmLocationX, dmLocationY)
        Me.pnlWineParring.Size = New Size(dmWidth, dmHeight)
        Me.pnlWineParring.BackColor = c3 ' System.Drawing.SystemColors.Control
        Me.pnlWineParring.BorderStyle = BorderStyle.FixedSingle

        Me.lblWineParring = New Label
        Me.lblRecipe = New Label

        Me.lblWineParring.Location = New Point(1, 1) '(buttonSpace, buttonSpace)
        Me.lblWineParring.Size = New Size(dmWidth - 4, dmHeight - 4) '((dmWidth - (2 * buttonSpace)), (dmHeight - (2 * buttonSpace)))
        Me.lblWineParring.Text = ""
        Me.lblWineParring.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWineParring.BackColor = c2 'System.Drawing.SystemColors.Control
        Me.lblWineParring.ForeColor = c3 'c2

        Me.lblRecipe.Location = New Point(1, 1) '(buttonSpace, buttonSpace)
        Me.lblRecipe.Size = New Size(dmWidth - 4, dmHeight - 4) '((dmWidth - (2 * buttonSpace)), (dmHeight - (2 * buttonSpace)))
        Me.lblRecipe.Text = ""
        Me.lblRecipe.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecipe.BackColor = c2 'System.Drawing.SystemColors.Control
        Me.lblRecipe.ForeColor = c3 'c2
        Me.lblRecipe.Visible = False

        Me.pnlPizzaSplit.Location = New Point(dmLocationX, dmLocationY)
        Me.pnlPizzaSplit.Size = New Size(dmWidth, dmHeight)
        Me.pnlPizzaSplit.BackColor = c3 ' System.Drawing.SystemColors.Control
        Me.pnlPizzaSplit.BorderStyle = BorderStyle.FixedSingle
        Me.pnlPizzaSplit.Visible = False

        Me.pnlOnFullPizza.Location = New Point(0, 0)
        Me.pnlOnFullPizza.Size = New Size((dmWidth / 2), dmHeight)
        Me.pnlOnFullPizza.BackColor = c18

        Me.pnlOnFirstHalf.Location = New Point((dmWidth / 2), 0)
        Me.pnlOnFirstHalf.Size = New Size(((dmWidth / 2)), ((dmHeight / 2)))
        Me.pnlOnFirstHalf.BackColor = c3

        Me.pnlOnSecondHalf.Location = New Point((dmWidth / 2), ((dmHeight) / 2))
        Me.pnlOnSecondHalf.Size = New Size(((dmWidth / 2)), ((dmHeight / 2)))
        Me.pnlOnSecondHalf.BackColor = c3

        Me.onFullPizza.Location = New Point(2, 5)
        Me.onFullPizza.Size = New Size((dmWidth / 2) - 4, dmHeight - 4)
        Me.onFullPizza.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.onFullPizza.BorderStyle = BorderStyle.FixedSingle
        Me.onFullPizza.BackColor = c2
        Me.onFullPizza.ForeColor = c3

        Me.onFirstHalf.Location = New Point(2, 2)
        Me.onFirstHalf.Size = New Size(((dmWidth / 2) - 8), ((dmHeight / 2)))
        Me.onFirstHalf.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.onFirstHalf.BorderStyle = BorderStyle.FixedSingle
        Me.onFirstHalf.BackColor = c2
        Me.onFirstHalf.ForeColor = c3

        Me.onSecondHalf.Location = New Point(2, 1)
        Me.onSecondHalf.Size = New Size(((dmWidth / 2) - 8), ((dmHeight / 2) - 2))
        Me.onSecondHalf.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.onSecondHalf.BorderStyle = BorderStyle.FixedSingle
        Me.onSecondHalf.BackColor = c2
        Me.onSecondHalf.ForeColor = c3

        Me.onFullPizza.DataSource = dvPizzaFull
        Me.onFirstHalf.DataSource = dvPizzaFirst
        Me.onSecondHalf.DataSource = dvPizzaSecond

        Me.onFullPizza.DisplayMember = "ItemName"
        Me.onFirstHalf.DisplayMember = "ItemName"
        Me.onSecondHalf.DisplayMember = "ItemName"


        Me.pnlWineParring.Controls.Add(Me.lblWineParring)
        Me.pnlWineParring.Controls.Add(Me.lblRecipe)

        Me.pnlOnFullPizza.Controls.Add(Me.onFullPizza)
        Me.pnlOnFirstHalf.Controls.Add(Me.onFirstHalf)
        Me.pnlOnSecondHalf.Controls.Add(Me.onSecondHalf)
        Me.pnlPizzaSplit.Controls.Add(Me.pnlOnFullPizza)
        Me.pnlPizzaSplit.Controls.Add(Me.pnlOnFirstHalf)
        Me.pnlPizzaSplit.Controls.Add(Me.pnlOnSecondHalf)

    End Sub

    Private Sub CreateMainButtonArray(ByVal width As Integer, ByVal height As Integer)
        Dim index As Integer
        Dim oRow As DataRow
        Dim x As Integer = buttonSpace
        Dim y As Integer = buttonSpace

        For index = 1 To 20
            btnMain(index) = New OrderButton("10")
            With btnMain(index)
                .Size = New Size(width, height)
                .Location = New Point(x, y)
                .FoodTableIndex = index
                .BackColor = c8
            End With

            y = y + height + buttonSpace
            If index = 10 Then
                y = height + (2 * buttonSpace)
            End If
        Next

        btnMainNext = New OrderButton("10")
        With btnMainNext
            .Size = New Size(width, height)
            .Location = New Point(buttonSpace, ((height * 10) + (buttonSpace * 11)))
            .Text = "Next"
            .BackColor = c7
            .ForeColor = c3
            pnlMain.Controls.Add(btnMainNext)
        End With

        btnMainPrevious = New OrderButton("10")
        With btnMainPrevious
            .Size = New Size(width, height)
            .Location = New Point(buttonSpace, buttonSpace)
            .Text = "Previous"
            .BackColor = c7
            .ForeColor = c3
            pnlMain2.Controls.Add(btnMainPrevious)
        End With

        PopulateMainButtons()

        For index = 20 To 0 Step -1
            pnlMain2.Controls.Add(btnMain(index))
            If index < 11 Then
                pnlMain.Controls.Add(btnMain(index))
            End If
        Next

        y = buttonSpace

        For index = 1 To 10
            btnModifier(index) = New OrderButton("10")
            With btnModifier(index)
                .Size = New Size(width, height)
                .Location = New Point(x, y)
                .BackColor = c8
            End With
            y = y + height + buttonSpace
        Next

        Exit Sub
        '222 below
        For Each oRow In dtModifierCategory.Rows
            With btnModifier(oRow("CategoryOrder"))  '(oRow("CategoryID") - 100)
                .Text = oRow("CategoryAbrev")
                .CategoryID = oRow("CategoryID")
                .CatName = oRow("CategoryName")
                .Functions = 3
                .BackColor = c7
                .ForeColor = c3
                AddHandler btnModifier(oRow("CategoryOrder")).Click, AddressOf ModifierClick222
            End With
        Next

        For index = 10 To 1 Step -1
            Me.pnlMain3.Controls.Add(btnModifier(index))
        Next
        btnMainNextMain3 = New OrderButton("10")
        With btnMainNextMain3
            .Size = New Size(width, height)
            .Location = New Point(buttonSpace, ((height * 10) + (buttonSpace * 11)))
            .Text = "Next"
            .BackColor = c7
            .ForeColor = c3
            pnlMain3.Controls.Add(btnMainNextMain3)
        End With

    End Sub

    Private Sub NextButton(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMainNext.Click
        UserControlHit()
        '     If EndOfItem(True) = False Then Exit Sub
        pnlMain.Visible = False
        pnlMain2.Visible = True

    End Sub

    Private Sub PreviousButton(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMainPrevious.Click
        UserControlHit()
        '     If EndOfItem(True) = False Then Exit Sub
        pnlMain2.Visible = False
        pnlMain.Visible = True

    End Sub

    Private Sub NextButtonMain3(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMainNextMain3.Click
        UserControlHit()
        '???    If EndOfItem(True) = False Then Exit Sub
        pnlMain.Visible = True
        pnlMain3.Visible = False

    End Sub

    Private Sub PopulateMainButtons()
        Dim oRow As DataRow
        Dim populatingTable As DataTable
        Dim populatingDrinkTable As DataTable
        Dim index As Integer
        Dim IsPrimary As Boolean
        Dim moreThan10Categories As Boolean = False

        '  If currentTable.StartingMenu = currentPrimaryMenuID Then
        '     If currentTable.CurrentMenu = initPrimaryMenuID Then
        If currentTable.IsPrimaryMenu = True Then
            IsPrimary = True
        Else
            IsPrimary = False
        End If
        If currentTerminal.TermMethod = "Quick" Then
            If IsPrimary = True Then
                populatingTable = dtQuickCategory
                populatingDrinkTable = dtQuickDrinkCategory
            Else
                populatingTable = dtSecondaryQuickCategory
                populatingDrinkTable = dtSecondaryQuickDrinkCategory
            End If
        Else
            If currentServer.Bartender = True Then '444 IsBartenderMode = True Then                ' can also add a screen size restriction
                If IsPrimary = True Then
                    populatingTable = dtBartenderCategory
                    populatingDrinkTable = dtBartenderDrinkCategory
                Else
                    populatingTable = dtSecondaryBartenderCategory
                    populatingDrinkTable = dtSecondaryBartenderDrinkCategory
                End If

            Else
                If IsPrimary = True Then
                    populatingTable = dtMainCategory
                    populatingDrinkTable = dtDrinkCategory
                Else
                    populatingTable = dtSecondaryMainCategory
                    populatingDrinkTable = dtSecondaryDrinkCategory
                End If
            End If
        End If


        For index = 1 To 20
            With btnMain(index)
                .Text = ""
                .CategoryID = Nothing
                .CatName = Nothing
                .Functions = Nothing
                .IsPrimary = Nothing 'IsPrimary
                .MainButtonIndex = Nothing
                .BackColor = c8
                .ForeColor = c2
                ' Handlers
                RemoveHandler btnMain(index).Click, AddressOf Foods
            End With
        Next




        For Each oRow In populatingTable.Rows


            If oRow("OrderIndex") > 10 Then
                moreThan10Categories = True
            End If
            With btnMain(oRow("OrderIndex"))
                .Text = oRow("CategoryAbrev")
                .CategoryID = oRow("CategoryID")
                .CatName = oRow("CategoryName")
                .Functions = oRow("FunctionID")
                .FunctionGroup = oRow("FunctionGroupID")
                .FunctionFlag = oRow("FunctionFlag")
                .IsPrimary = IsPrimary
                .MainButtonIndex = 1
                .Extended = oRow("Extended")
                If pastFirstCategory = False And oRow("FunctionFlag") = "G" Then
                    firstCategory.CategoryID = .CategoryID
                    firstCategory.FunctionFlag = .FunctionFlag
                    firstCategory.IsPrimary = .IsPrimary
                    firstCategory.Extended = .Extended
                    pastFirstCategory = True
                End If
                If Not oRow("ButtonColor") Is DBNull.Value Then
                    .BackColor = Color.FromArgb(oRow("ButtonColor"))
                    .ForeColor = Color.FromArgb(oRow("ButtonForeColor"))
                Else
                    .BackColor = c6
                    .ForeColor = c3
                End If

                ' Handlers
                AddHandler btnMain(oRow("OrderIndex")).Click, AddressOf Foods
            End With
        Next

        For Each oRow In populatingDrinkTable.Rows
            If oRow("OrderIndex") > 10 Then
                moreThan10Categories = True
            End If
            With btnMain(oRow("OrderIndex"))
                .Text = oRow("DrinkCategoryName")   'should be DrinkCategoryAbrev
                If oRow("DrinkCategoryNumber") = -1 Then
                    .CategoryID = -1
                Else
                    .CategoryID = oRow("DrinkCategoryID")
                End If
                .CatName = oRow("DrinkCategoryName")

                .Functions = 4  'oRow("DrinkFunctionID")              'need to update
                .FunctionFlag = "D"
                .FunctionGroup = Nothing       'at this point we don't have group (we get w/ specific drink)
                .MainButtonIndex = 1
                If Not oRow("ButtonColor") Is DBNull.Value Then
                    .BackColor = Color.FromArgb(oRow("ButtonColor"))
                    .ForeColor = Color.FromArgb(oRow("ButtonForeColor"))
                Else
                    .BackColor = c16
                    .ForeColor = c3
                End If

                ' Handlers
                AddHandler btnMain(oRow("OrderIndex")).Click, AddressOf Foods
            End With
        Next

        If moreThan10Categories = True Then
            btnMainNext.Visible = True
        Else
            btnMainNext.Visible = False
        End If
        pnlMain.Visible = True

    End Sub


    Private Sub CreateOrderButtonArray()

        Dim index As Integer
        Dim x As Integer = buttonSpace
        Dim y As Integer = buttonSpace
        Dim count As Integer

        For index = 0 To 31

            '444  btnOrder(index) = New OrderButtonRaised
            btnOrder(index) = New OrderButton("12")

            With btnOrder(index)
                .Size() = New Size(opButtonWidth, opButtonHeight)
                .Location = New Point(x, y)
                '         .BackgroundImage = Image.FromFile("testBack.png")
                AddHandler btnOrder(index).Click, AddressOf BtnOrder_Click
            End With

            count = count + 1
            If count < 4 Then
                x = x + opButtonWidth + buttonSpace
            Else
                x = buttonSpace
                y = y + opButtonHeight + buttonSpace
                count = 0
            End If
        Next

        For index = 31 To 0 Step -1
            pnlOrder.Controls.Add(btnOrder(index))
        Next

    End Sub

    Private Sub CreateOrderButtonQuick()

        Dim index As Integer
        Dim x As Integer = buttonSpace
        Dim y As Integer = buttonSpace
        Dim count As Integer
        '      opButtonWidth = (opWidth - (7 * buttonSpace)) / 6
        '     opButtonHeight = (opHeight + 47 - (11 * buttonSpace)) / 10     'mathmatically should be 11
        '   ReDim btnOrderQuick(60)

        For index = 0 To 59

            btnOrderQuick(index) = New OrderButton("10")

            With btnOrderQuick(index)
                .Size() = New Size(drinkButtonWidth, drinkButtonHeight)
                '     .Size() = New Size(opButtonWidth, opButtonHeight)
                .Location = New Point(x, y)
                AddHandler btnOrderQuick(index).Click, AddressOf BtnOrder_Click
            End With

            count = count + 1
            If count < 6 Then
                x = x + drinkButtonWidth + buttonSpace
                '     x = x + opButtonWidth + buttonSpace
            Else
                x = buttonSpace
                y = y + drinkButtonHeight + buttonSpace
                '      y = y + opButtonHeight + buttonSpace
                count = 0
            End If
        Next

        For index = 59 To 0 Step -1
            pnlOrderQuick.Controls.Add(btnOrderQuick(index))
        Next

    End Sub

    Private Sub CreateOrderDrinkButtonArray()

        Dim index As Integer
        Dim x As Integer = buttonSpace
        Dim y As Integer = buttonSpace
        Dim count As Integer

        For index = 1 To 60 '48

            btnOrderDrink(index) = New OrderButton("10")

            With btnOrderDrink(index)
                .Size() = New Size(drinkButtonWidth, drinkButtonHeight)
                .Location = New Point(x, y)
                AddHandler btnOrderDrink(index).Click, AddressOf BtnOrder_Click   'BtnOrderDrink_Click
            End With

            count = count + 1
            If count < 6 Then

                x = x + drinkButtonWidth + buttonSpace
            Else
                x = buttonSpace
                y = y + drinkButtonHeight + buttonSpace
                count = 0
            End If
        Next

        For index = 60 To 1 Step -1
            pnlOrderDrink.Controls.Add(btnOrderDrink(index))
        Next

    End Sub

    Private Sub CreateOrderModifierButtonArray()
        Dim index As Integer
        Dim x As Integer = buttonSpace
        Dim y As Integer = buttonSpace
        Dim count As Integer

        For index = 0 To 23

            btnOrderModifier(index) = New OrderButton("12")

            With btnOrderModifier(index)
                .Size() = New Size(mpButtonWidth, mpButtonHeight)
                .Location = New Point(x, y)
                .BackColor = c8
                AddHandler btnOrderModifier(index).Click, AddressOf BtnOrderModifier_Click
            End With

            count = count + 1
            If count < 4 Then
                x = x + mpButtonWidth + buttonSpace

            Else
                x = buttonSpace
                y = y + mpButtonHeight + buttonSpace
                count = 0
            End If
        Next

        btnOrderModifierCancel = New OrderButton("12")
        With btnOrderModifierCancel
            .Size() = New Size(mpButtonWidth, mpButtonHeight)
            .Location = New Point((3 * mpButtonWidth) + (4 * buttonSpace), (6 * mpButtonHeight) + (7 * buttonSpace))
            .Text = "None"  '"Cancel"
            .ID = -3
            .BackColor = c4
            .ForeColor = c3
            AddHandler btnOrderModifierCancel.Click, AddressOf BtnOrderModifier_Click
        End With

        For index = 23 To 0 Step -1
            pnlOrderModifier.Controls.Add(btnOrderModifier(index))
        Next
        pnlOrderModifier.Controls.Add(btnOrderModifierCancel)

    End Sub

    Private Sub CreateOrderModifierExtendedButtonArray()
        Dim index As Integer
        Dim x As Integer = buttonSpace
        Dim y As Integer = buttonSpace
        Dim count As Integer
        Dim extModButtonHeight As Double
        extModButtonHeight = (drinkButtonHeight * (8 / 9))

        For index = 0 To 59

            btnOrderModifierExt(index) = New OrderButton("10")

            With btnOrderModifierExt(index)
                .Size() = New Size(drinkButtonWidth, extModButtonHeight)
                .Location = New Point(x, y)
                .BackColor = c8
                AddHandler btnOrderModifierExt(index).Click, AddressOf BtnOrderModifier_Click
            End With

            count = count + 1
            If count < 6 Then
                x = x + drinkButtonWidth + buttonSpace

            Else
                x = buttonSpace
                y = y + extModButtonHeight + buttonSpace
                count = 0
            End If
        Next

        btnOrderModifierCancel = New OrderButton("10")
        With btnOrderModifierCancel
            .Size() = New Size(drinkButtonWidth, extModButtonHeight)
            .Location = New Point((5 * drinkButtonWidth) + (5 * buttonSpace), (10 * extModButtonHeight) + (11 * buttonSpace))
            .Text = "None"  '"Cancel"
            .ID = -3
            .BackColor = c4
            .ForeColor = c3
            AddHandler btnOrderModifierCancel.Click, AddressOf BtnOrderModifier_Click
        End With

        For index = 59 To 0 Step -1
            pnlOrderModifierExt.Controls.Add(btnOrderModifierExt(index))
        Next
        pnlOrderModifierExt.Controls.Add(btnOrderModifierCancel)

    End Sub

    Private Sub UpdateDataViewsByCheck() Handles testgridview.UpdatingViewsByCheck
        '   activeCheck (once created) should always be = to currenttable.checknumber - it gone

        dvOrder.RowFilter = "CheckNumber ='" & currentTable.CheckNumber & "'"
        dvOrderTopHierarcy.RowFilter = "sii = sin AND CheckNumber ='" & currentTable.CheckNumber & "'"
        dvOrderHolds.RowFilter = "ItemStatus = 1 AND CheckNumber ='" & currentTable.CheckNumber & "'"
        dvKitchen.RowFilter = "ItemStatus = 2 AND CheckNumber ='" & currentTable.CheckNumber & "'"


    End Sub

    Private Sub DisplayCustomerPanel()

        customerPanel = New Panel
        customerPanel.SuspendLayout()

        '      customerPanel.Location = New Point(1, (Me.pnlDirection.Height + Me.gridViewOrder.Height + Me.pnlSubTotal.Height))
        '     customerPanel.Size = New Size(Me.Width, (Me.Height * 0.05))
        '    customerPanel.BackColor = c7
        If currentServer.Lefty = False Then
            customerPanel.Location = New Point(opLocationX - (buttonSpace), opLocationY + opHeight + buttonSpace)
        Else
            customerPanel.Location = New Point(ssX * 0.26 + (2 * buttonSpace), opLocationY + opHeight + buttonSpace)
        End If

        customerPanel.Size = New Size(ssX * 0.41, ssY * 0.06)
        customerPanel.BackColor = c7

        Dim index As Integer
        Dim w As Double = Me.customerPanel.Width / 6      '9 buttons and 1 (3 times size)
        Dim h As Double = Me.customerPanel.Height
        Dim x As Double

        btnCustomerNext = New KitchenButton("Next", (w), h, c9, c2)
        btnCustomerNext.Location = New Point(x, 0)
        btnCustomerNext.ForeColor = c3
        AddHandler btnCustomerNext.Click, AddressOf CustomerButton_Click
        customerPanel.Controls.Add(btnCustomerNext)

        x += (w)

        For index = 1 To 5
            btnCustomer(index) = New KitchenButton(index, w, h, c7, c2)
            With btnCustomer(index)
                .Location = New Point(x, 0)
                .ForeColor = c3
                AddHandler btnCustomer(index).Click, AddressOf CustomerButton_Click
                customerPanel.Controls.Add(btnCustomer(index))
            End With
            x += w
        Next

        customerPanel.ResumeLayout()
        Me.Controls.Add(Me.customerPanel)
        ChangeCustomerButtonColor(c9)

    End Sub

    Private Sub CustomerButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles customerPanel.Click
        UserControlHit()
        Dim btnText As String
        '444    If currentTerminal.TermMethod = "Quick" Then Exit Sub

        Dim objButton As KitchenButton
        Try
            objButton = CType(sender, KitchenButton)
        Catch ex As Exception
            Exit Sub
        End Try

        If Not objButton.GetType Is btnCustomer(1).GetType Then Exit Sub
        btnText = objButton.Text

        AttemptToChangeCustomer(btnText, True)

    End Sub

    Private Sub AttemptToChangeCustomer(ByVal btnText As String, ByVal doWeTestForPanel1 As Boolean) Handles testgridview.ChangeCustomerEvent
        Dim newcnTest As Integer
        Dim oRow As DataRow
        Dim bRow As DataRow
        Dim newCustLabelExist As Boolean

        ChangeCustomerButtonColor(c7)


        If Not btnText = "Next" Then
            Dim cust1Test As Boolean

            If CInt(btnText) = 1 Then
                If doWeTestForPanel1 = True Then
                    cust1Test = CustomerPanelOneTest()

                    If cust1Test = False Then
                        'this means we added the panel
                        '         currentTable.NextCustomerNumber = 1
                        If dvOrder.Count = 1 Then
                            'this is only needed if we just put Cust Panel 1 only
                            Me.testgridview.gridViewOrder.DataSource = dvOrder
                        End If
                        ChangeCustomerButtonColor(c9)
                        Exit Sub
                    Else
                        currentTable.NextCustomerNumber = 1 'not sure if this should be inside if/then
                        If currentTable.MiddleOfOrder = True Then
                            currentTable.MarkForNextCustomer = True
                            Exit Sub
                        End If
                    End If
                Else
                    ChangeCustomerButtonColor(c9)
                    Exit Sub
                End If
            Else
                ' this is not btn 1 hit
                btnCustomer(1).BackColor = c7
            End If
        End If

        If currentTable.EmptyCustPanel = 0 Then          'same as oldcnTest > 1
            '   no empty panels

            If btnText = "Next" Then
                currentTable.NextCustomerNumber = currentTable.CustomerNumber + 1

            Else
                currentTable.NextCustomerNumber = CInt(btnText)
            End If
            '        ChangeCustomerButtonColor(c9)
            newCustLabelExist = DetermineCustomerLabelExists(currentTable.NextCustomerNumber)

            If currentTable.MiddleOfOrder = True Then

                If newCustLabelExist = False Then
                    currentTable.MarkForNewCustomerPanel = True
                    currentTable.MarkForNextCustomer = True
                    testgridview.justAddedPanel = True
                    '          If doWeTestForPanel1 = False Then
                    Exit Sub
                    '     End If
                Else
                    currentTable.MarkForNextCustomer = True
                    Exit Sub
                    '     currentTable.CustomerNumber = currentTable.NextCustomerNumber
                    '    ChangeCustomerButtonColor(c9)
                End If
            Else

                If newCustLabelExist = False Then
                    currentTable.CustomerNumber = currentTable.NextCustomerNumber
                    ChangeCustomerButtonColor(c9)
                    AddCustomerPanel()
                    If btnText = "Next" Then
                        Exit Sub
                    End If
                End If
            End If

        End If

        ' only way we get here is for there to be an empty panel
        '   if we only have 1 customer item and it does not have an itemID then it is a blank customer title
        '   we are changing that title to new customer then exiting
        If currentTable.CustomerNumber = currentTable.EmptyCustPanel Then
            'your old Cust Number has an empty panel
            If Not btnText = "Next" Then
                If currentTable.CustomerNumber = CInt(btnText) Then
                    ChangeCustomerButtonColor(c9)   ' must change back
                    Exit Sub
                    '   selected the same panel
                End If
            End If

            For Each oRow In dsOrder.Tables("OpenOrders").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("CustomerNumber") = currentTable.CustomerNumber And oRow("ItemID") = 0 Then
                        '   find your empty panel

                        If btnText = "Next" Then
                            '    currentTable.NextCustomerNumber += 1
                            currentTable.CustomerNumber += 1
                        Else
                            '      currentTable.NextCustomerNumber = CInt(btnText)
                            currentTable.CustomerNumber = CInt(btnText)
                        End If
                        '   currentTable.CustomerNumber = currentTable.NextCustomerNumber

                        '   ****   new test count
                        newcnTest = GenerateOrderTables.DetermineCnTest(currentTable.CustomerNumber)

                        If newcnTest = 0 Then   '<= 1 Then
                            oRow("ItemName") = "              " + currentTable.CustomerNumber.ToString + "   CUSTOMER   " + currentTable.CustomerNumber.ToString 'btnText
                            oRow("TerminalName") = "              " + currentTable.CustomerNumber.ToString + "   CUSTOMER   " + currentTable.CustomerNumber.ToString 'btnText
                            oRow("ChitName") = "              " + currentTable.CustomerNumber.ToString + "   CUSTOMER   " + currentTable.CustomerNumber.ToString 'btnText
                            oRow("CustomerNumber") = currentTable.CustomerNumber

                            If currentTable.CustomerNumber <> 1 Then
                                currentTable.EmptyCustPanel = currentTable.CustomerNumber
                            End If
                        Else
                            '   do we need to check to see if we have panel?????
                            '   if not customer 1 we should have panel
                        End If
                        ChangeCustomerButtonColor(c9)
                        Exit Sub
                    End If
                End If
            Next
        Else
            'currenttable.custometNumber < currenttable.EmptyCustomerPanel
            '    this happens if we hit testOrderView to reset ct.CustomerNumber
            If btnText = "Next" Then
                currentTable.NextCustomerNumber = currentTable.CustomerNumber + 1
            Else
                currentTable.NextCustomerNumber = CInt(btnText)
            End If
            currentTable.CustomerNumber = currentTable.NextCustomerNumber
            ChangeCustomerButtonColor(c9)
        End If

    End Sub

    Private Sub AttemptToChangeCustomer222(ByVal btnText As String, ByVal doWeTestForPanel1 As Boolean)
        Dim newcnTest As Integer
        Dim oRow As DataRow
        Dim bRow As DataRow
        Dim newCustLabelExist As Boolean

        ChangeCustomerButtonColor(c7)

        If Not btnText = "Next" Then
            Dim cust1Test As Boolean

            If CInt(btnText) = 1 Then
                If doWeTestForPanel1 = True Then
                    cust1Test = CustomerPanelOneTest()

                    If cust1Test = False Then
                        'this means we added the panel
                        '         currentTable.NextCustomerNumber = 1
                        If dvOrder.Count = 1 Then
                            'this is only needed if we just put Cust Panel 1 only
                            Me.testgridview.gridViewOrder.DataSource = dvOrder
                        End If
                        ChangeCustomerButtonColor(c9)
                        Exit Sub
                    Else
                        If currentTable.MiddleOfOrder = True Then
                            currentTable.NextCustomerNumber = 1
                            currentTable.MarkForNextCustomer = True
                            Exit Sub
                        End If
                    End If
                Else
                    ChangeCustomerButtonColor(c9)
                    Exit Sub
                End If
            Else
                ' this is not btn 1 hit
                btnCustomer(1).BackColor = c7
            End If
        End If

        If currentTable.EmptyCustPanel = 0 Then          'same as oldcnTest > 1
            '   no empty panels

            If btnText = "Next" Then
                currentTable.NextCustomerNumber = currentTable.CustomerNumber + 1

            Else
                currentTable.NextCustomerNumber = CInt(btnText)
            End If
            '        ChangeCustomerButtonColor(c9)
            newCustLabelExist = DetermineCustomerLabelExists(currentTable.NextCustomerNumber)

            If currentTable.MiddleOfOrder = True Then

                If newCustLabelExist = False Then
                    currentTable.MarkForNewCustomerPanel = True
                    currentTable.MarkForNextCustomer = True
                    testgridview.justAddedPanel = True
                    '          If doWeTestForPanel1 = False Then
                    Exit Sub
                    '     End If
                Else
                    currentTable.MarkForNextCustomer = True
                    Exit Sub
                    '     currentTable.CustomerNumber = currentTable.NextCustomerNumber
                    '    ChangeCustomerButtonColor(c9)
                End If
            Else

                If newCustLabelExist = False Then
                    currentTable.CustomerNumber = currentTable.NextCustomerNumber
                    ChangeCustomerButtonColor(c9)
                    AddCustomerPanel()
                    If btnText = "Next" Then
                        Exit Sub
                    End If
                End If
            End If

        End If

        ' only way we get here is for there to be an empty panel
        '   if we only have 1 customer item and it does not have an itemID then it is a blank customer title
        '   we are changing that title to new customer then exiting
        If currentTable.CustomerNumber = currentTable.EmptyCustPanel Then
            'your old Cust Number has an empty panel
            If Not btnText = "Next" Then
                If currentTable.CustomerNumber = CInt(btnText) Then
                    ChangeCustomerButtonColor(c9)   ' must change back
                    Exit Sub
                    '   selected the same panel
                End If
            End If

            For Each oRow In dsOrder.Tables("OpenOrders").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("CustomerNumber") = currentTable.CustomerNumber And oRow("ItemID") = 0 Then
                        '   find your empty panel

                        If btnText = "Next" Then
                            '    currentTable.NextCustomerNumber += 1
                            currentTable.CustomerNumber += 1
                        Else
                            '      currentTable.NextCustomerNumber = CInt(btnText)
                            currentTable.CustomerNumber = CInt(btnText)
                        End If
                        '   currentTable.CustomerNumber = currentTable.NextCustomerNumber

                        '   ****   new test count
                        newcnTest = GenerateOrderTables.DetermineCnTest(currentTable.CustomerNumber)

                        If newcnTest = 0 Then   '<= 1 Then
                            oRow("ItemName") = "              " + currentTable.CustomerNumber.ToString + "   CUSTOMER   " + currentTable.CustomerNumber.ToString 'btnText
                            oRow("TerminalName") = "              " + currentTable.CustomerNumber.ToString + "   CUSTOMER   " + currentTable.CustomerNumber.ToString 'btnText
                            oRow("ChitName") = "              " + currentTable.CustomerNumber.ToString + "   CUSTOMER   " + currentTable.CustomerNumber.ToString 'btnText
                            oRow("CustomerNumber") = currentTable.CustomerNumber

                            If currentTable.CustomerNumber <> 1 Then
                                currentTable.EmptyCustPanel = currentTable.CustomerNumber
                            End If
                        Else
                            '   do we need to check to see if we have panel?????
                            '   if not customer 1 we should have panel
                        End If
                        ChangeCustomerButtonColor(c9)
                        Exit Sub
                    End If
                End If
            Next
        Else
            'currenttable.custometNumber < currenttable.EmptyCustomerPanel
            '    this happens if we hit testOrderView to reset ct.CustomerNumber
            If btnText = "Next" Then
                currentTable.NextCustomerNumber = currentTable.CustomerNumber + 1
            Else
                currentTable.NextCustomerNumber = CInt(btnText)
            End If
            currentTable.CustomerNumber = currentTable.NextCustomerNumber
            ChangeCustomerButtonColor(c9)
        End If

    End Sub

    Private Sub AddCustomerPanel()

        Dim currentItem As SelectedItemDetail = New SelectedItemDetail
        Dim custNumString As String = "              " + currentTable.CustomerNumber.ToString + "   CUSTOMER   " + currentTable.CustomerNumber.ToString

        With currentItem
            '            If currentTable.IsTabNotTable = False Then
            '           .Table = currentTable.TableNumber
            '          Else
            '             .Table = currentTable.TabID
            '        End If
            .Check = currentTable.CheckNumber
            .Customer = currentTable.CustomerNumber
            .Course = currentTable.CourseNumber
            .Quantity = 0
            .InvMultiplier = 0
            .FunctionFlag = "N"
            '        If currentTable.MiddleOfOrder = True Then
            '       .SIN = currentTable.ReferenceSIN - 1
            '      .SII = currentTable.ReferenceSIN - 1
            '     Else
            .SIN = currentTable.SIN
            .SII = currentTable.SIN
            .si2 = currentTable.si2
            '    End If
            .ID = 0
            .Name = custNumString
            .TerminalName = custNumString
            .ChitName = custNumString
            .Price = Nothing
            .Category = Nothing
        End With

        currentTable.ReferenceSIN = currentTable.SIN
        currentTable.SIN += 1
        AddItemToOrderTable(currentItem)
        '                         RaiseEvent AddingItemToOrder(currentItem)
        If currentTable.CustomerNumber <> 1 Then
            currentTable.EmptyCustPanel = currentTable.CustomerNumber
        End If

        Dim cust1Test As Boolean
        cust1Test = CustomerPanelOneTest()
        currentTable.ReferenceSIN = currentTable.SIN

        If cust1Test = False Then
            'this means we added the panel
            '         currentTable.NextCustomerNumber = 1
            If dvOrder.Count = 1 Then
                'this is only needed if we just put Cust Panel 1 only
                Me.testgridview.gridViewOrder.DataSource = dvOrder
            End If
            ChangeCustomerButtonColor(c9)
        End If

        btnCustomer(1).BackColor = c7
        '     testgridview.justAddedPanel = True

    End Sub

    Private Function DetermineCustomerLabelExists(ByVal cn As Integer)
        Dim IsCustLabel As Boolean
        Dim oRow As DataRow

        '      If currentTable.CustomerNumber = 1 Then
        '       CustomerPanelOneTest()
        '    IsCustLabel = True
        '     Else
        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("ItemID") = 0 Then
                    If oRow("CustomerNumber") = cn Then         'currentTable.CustomerNumber Then
                        IsCustLabel = True
                        Exit For
                    End If
                End If
            End If

        Next
        '    End If

        Return IsCustLabel

    End Function


    Private Sub ChangeCustomerButtonColor(ByVal c As Color) Handles testgridview.ChangeCustColor
        '444   If currentTerminal.TermMethod = "Quick" Then Exit Sub

        If currentTable.CustomerNumber <= 5 Then
            btnCustomer(currentTable.CustomerNumber).BackColor = c
        End If

        If currentTable.NumberOfChecks > 1 Then
            Dim cc As Integer
            cc = DetermineCheckCount(currentTable.CustomerNumber)
            If cc > 0 Then
                currentTable.CheckNumber = currentTable.NumberOfChecks
                Me.testgridview.UpdateCheckNumberButton()
            End If
        End If

    End Sub


    Private Function DetermineCnTest(ByVal currentTable As Integer)
        '   this tests to see how if the new or old customer number has any information and how much
        Dim cnTestValue As Integer

        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
            cnTestValue = (dsOrder.Tables("OpenOrders")).Compute("Count(CustomerNumber)", "CustomerNumber ='" & currentTable & "'")
        Else : cnTestValue = 0
        End If

        Return cnTestValue

    End Function

    Private Sub CreateMainModifierPanel()
        pnlMainModifier.SuspendLayout()

        '      Dim mainModifierPanelWidth As Integer = (ssX - (viewOrderWidth + opWidth + (4 * buttonSpace)))
        '     Dim mainModifierPanelHeight As Integer = ssY * 0.27 'this seems too short but is 99% w/o spaces
        Dim mainModifierPanelWidth As Integer = (ssX * 0.26)
        Dim mainModifierPanelHeight As Integer = ssY * 0.26

        If Not currentTerminal.TermMethod = "Quick" Then
            If currentServer.Lefty = False Then
                Me.pnlMainModifier.Location = New Point(ssX * 0.735, opHeight + (3 * buttonSpace))
            Else
                Me.pnlMainModifier.Location = New Point(buttonSpace, opHeight + (3 * buttonSpace))
            End If
        Else
            If currentServer.Lefty = False Then
                Me.pnlMainModifier.Location = New Point(ssX * 0.735, opHeight + (3 * buttonSpace) + 47)
            Else
                Me.pnlMainModifier.Location = New Point(buttonSpace, opHeight + (3 * buttonSpace) + 47)
            End If
        End If

        '       Me.pnlMainModifier.Location = New Point((3 * buttonSpace) + viewOrderWidth + opWidth, panelModLocationY)
        Me.pnlMainModifier.Size = New Size(mainModifierPanelWidth, mainModifierPanelHeight)
        '     Me.pnlMainModifier.BackColor = c7
        Me.pnlMainModifier.BackColor = c2
        Me.pnlMainModifier.BorderStyle = BorderStyle.FixedSingle


        Dim btnModifierWidth As Integer = (mainModifierPanelWidth - (3 * buttonSpace)) / 2
        Dim btnModifierHeight As Integer = (mainModifierPanelHeight - (5 * buttonSpace)) / 4

        btnModifierAdd = New KitchenButton("Add", btnModifierWidth, btnModifierHeight, c4, c3)
        btnModifierAdd.Location = New Point(buttonSpace, buttonSpace)
        btnModifierNo = New KitchenButton("No", btnModifierWidth, btnModifierHeight, c4, c3)
        btnModifierNo.Location = New Point(buttonSpace, (2 * buttonSpace) + (btnModifierHeight))
        btnModifierSpecial = New KitchenButton("Special", btnModifierWidth, btnModifierHeight, c4, c3)
        btnModifierSpecial.Location = New Point(buttonSpace, (3 * buttonSpace) + (2 * btnModifierHeight))
        btnModifierRepeat = New KitchenButton("Repeat", btnModifierWidth, btnModifierHeight, c4, c3)
        btnModifierRepeat.Location = New Point(buttonSpace, (4 * buttonSpace) + (3 * btnModifierHeight))
        btnModifierBlank = New KitchenButton("", btnModifierWidth, btnModifierHeight, c4, c3)
        btnModifierBlank.Location = New Point((2 * buttonSpace) + btnModifierWidth, (3 * buttonSpace) + (2 * btnModifierHeight))

        btnModifierExtra = New KitchenButton("Extra", btnModifierWidth, btnModifierHeight, c4, c3)
        btnModifierExtra.Location = New Point((2 * buttonSpace) + btnModifierWidth, buttonSpace)
        btnModifierOnFly = New KitchenButton("On Fly", btnModifierWidth, btnModifierHeight, c4, c3)
        btnModifierOnFly.Location = New Point((2 * buttonSpace) + btnModifierWidth, buttonSpace)
        btnModifierNoMake = New KitchenButton("No Make", btnModifierWidth, btnModifierHeight, c4, c3)
        btnModifierNoMake.Location = New Point((2 * buttonSpace) + btnModifierWidth, (3 * buttonSpace) + (2 * btnModifierHeight))
        btnModifierOnSide = New KitchenButton("On Side", btnModifierWidth, btnModifierHeight, c4, c3)
        btnModifierOnSide.Location = New Point((2 * buttonSpace) + btnModifierWidth, (2 * buttonSpace) + btnModifierHeight)
        btnModifierNoCharge = New KitchenButton("No Charge", btnModifierWidth, btnModifierHeight, c4, c3)
        btnModifierNoCharge.Location = New Point((2 * buttonSpace) + btnModifierWidth, (4 * buttonSpace) + (3 * btnModifierHeight))

        Me.pnlMainModifier.Controls.Add(btnModifierSpecial)
        Me.pnlMainModifier.Controls.Add(btnModifierNo)
        Me.pnlMainModifier.Controls.Add(btnModifierAdd)
        Me.pnlMainModifier.Controls.Add(btnModifierOnSide)

        '444    If Not currentTerminal.TermMethod = "Quick" Then
        Me.pnlMainModifier.Controls.Add(btnModifierRepeat)
        Me.pnlMainModifier.Controls.Add(btnModifierOnFly)
        Me.pnlMainModifier.Controls.Add(btnModifierNoMake)
        Me.pnlMainModifier.Controls.Add(btnModifierNoCharge)
        '     Else
        Me.pnlMainModifier.Controls.Add(btnModifierExtra)
        Me.pnlMainModifier.Controls.Add(btnModifierBlank)
        '      End If


        pnlMainModifier.ResumeLayout()

    End Sub

    Private Sub Foods(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlMain.Click, pnlMain2.Click
        UserControlHit()
        If EndOfItem(True) = False Then Exit Sub
        '    Me.GotoLastOrderedItem()

        '      ClearOrderPanel()
        '   can do this whole sub with just the food table
        '   we create a view each time based on categoryID
        '   no need for names and we can sort within category

        Dim objButton As OrderButton
        Try
            objButton = CType(sender, OrderButton)
        Catch ex As Exception
            Exit Sub
        End Try

        RunFoodsRoutine(objButton)

    End Sub

    Private Sub RunFoodsRoutine(ByVal objButton As OrderButton)
        Dim maxMenuIndex As Integer
        '    Dim lastMaxMenuIndex As Integer
        Dim oRow As DataRow
        Dim index As Integer
        Dim lastButton As Integer
        '      ClearOrderPanel()
        '   can do this whole sub with just the food table
        '   we create a view each time based on categoryID
        '   no need for names and we can sort within category

        '   in case we skipped whole order we need to reset (informing we are starting new order)
        currentTable.ReferenceSIN = currentTable.SIN
        currentTable.MiddleOfOrder = False
        currentTable.si2 = 0
        currentTable.Tempsi2 = 0
        currentTable.IsPizza = False
        '     Me.pnlPizzaSplit.Visible = False
        currentTable.IsExtended = objButton.Extended

        If Not currentTable.Quantity = 1 Then
            currentTable.Quantity = 1
            Me.testgridview.ChangeCourseButton(currentTable.Quantity)
        End If
        If currentTable.MarkForNextCustomer = True Then
            currentTable.CustomerNumber = currentTable.NextCustomerNumber
            ChangeCustomerButtonColor(c9)
            If currentTable.MarkForNewCustomerPanel = True Then
                AddCustomerPanel()
            End If
            currentTable.MarkForNewCustomerPanel = False
            currentTable.MarkForNextCustomer = False
        End If

        If objButton.FunctionFlag = "F" Or objButton.FunctionFlag = "O" Or objButton.FunctionFlag = "G" Then  'objButton.Functions = 1 Or objButton.Functions = 2 Then
            '   Me.pnlDrinkModifier.Visible = False
            ChangeFromDrinkButtons()
            drinkPrep.Visible = False
            pnlOrderModifier.Visible = False
            pnlOrderModifierExt.Visible = False
            pnlOrderDrink.Visible = False
            Me.pnlWineParring.Visible = True

            If currentTable.IsExtended = True Then   'currentTerminal.TermMethod = "Quick" Then
                currentTable.ActivePanel = "pnlOrderQuick"
                pnlOrder.Visible = False
                lastButton = 59
            Else
                currentTable.ActivePanel = "pnlOrder"
                pnlOrderQuick.Visible = False
                lastButton = 31
            End If

            Dim populatingTable As String

            If currentTable.IsPrimaryMenu = True Then
                populatingTable = "MainTable"
            Else
                populatingTable = "SecondaryMainTable"
            End If

            '   gets the last menu index of table which is the max
            If ds.Tables(populatingTable & objButton.CategoryID).Rows.Count > 0 Then
                maxMenuIndex = ds.Tables(populatingTable & objButton.CategoryID).Compute("Max(MenuIndex)", "") 'Compute("Max(OrderIndex)", "")??
            Else
                maxMenuIndex = 0
            End If
            If currentTable.IsExtended = True Then
                btnOrderQuick(lastButton).MaxMenuIndex = maxMenuIndex
                btnOrderQuick(0).MainButtonIndex = 1
                btnOrderQuick(lastButton).MainButtonIndex = 1
            Else
                btnOrder(lastButton).MaxMenuIndex = maxMenuIndex
                btnOrder(0).MainButtonIndex = 1
                btnOrder(lastButton).MainButtonIndex = 1
            End If


            '   we should change so it dims in increments of 32
            ReDim opButtonText(maxMenuIndex + 64)
            ReDim opButtonId(maxMenuIndex + 64)
            ReDim opButtonBackColor(maxMenuIndex + 64)
            ReDim opButtonForeColor(maxMenuIndex + 64)
            ReDim opButtonCategoryID(maxMenuIndex + 64)
            ReDim opButtonHalfSplit(maxMenuIndex + 64)
            ReDim opButtonFunctionID(maxMenuIndex + 64)
            ReDim opButtonFunctionGroupID(maxMenuIndex + 64)
            ReDim opButtonFunFlag(maxMenuIndex + 64)
            ReDim opButtonDrinkSubCat(maxMenuIndex + 64)

            For Each oRow In ds.Tables(populatingTable & objButton.CategoryID).Rows '("MainTable" & objButton.CategoryID).Rows
                If oRow("menuIndex") > 0 Then
                    index = oRow("MenuIndex")
                    opButtonText(index) = oRow("AbrevFoodName")
                    opButtonId(index) = oRow("FoodID")
                    opButtonCategoryID(index) = oRow("CategoryID")
                    opButtonHalfSplit(index) = oRow("HalfSplit")

                    opButtonFunctionID(index) = oRow("FunctionID")
                    opButtonFunctionGroupID(index) = oRow("FunctionGroupID")
                    opButtonFunFlag(index) = oRow("FunctionFlag")
                    opButtonDrinkSubCat(index) = True
                    If Not oRow("ButtonColor") Is DBNull.Value Then
                        opButtonBackColor(index) = Color.FromArgb(oRow("ButtonColor"))
                        opButtonForeColor(index) = Color.FromArgb(oRow("ButtonForeColor"))
                    Else
                        opButtonBackColor(index) = c6
                        opButtonForeColor(index) = c3
                    End If
                End If
            Next

            '***************************************
            '****this is within the other subroutine
            '**** this is for Drink portion of the G
            If objButton.FunctionFlag = "G" Then

                If currentTable.IsPrimaryMenu = True Then
                    populatingTable = "DrinkMainTable"
                Else
                    populatingTable = "DrinkMainTable"
                    'currently no Drink Secondary Table
                    'populatingTable = "DrinkSecondaryMainTable"
                End If

                For Each oRow In ds.Tables(populatingTable & objButton.CategoryID).Rows '("MainTable" & objButton.CategoryID).Rows
                    If oRow("menuIndex") > 0 Then
                        index = oRow("MenuIndex")
                        opButtonText(index) = oRow("DrinkName")
                        opButtonId(index) = oRow("DrinkID")
                        opButtonCategoryID(index) = oRow("DrinkCategoryID")
                        '    opButtonHalfSplit(index) = oRow("HalfSplit")
                        opButtonFunctionID(index) = oRow("FunctionID")
                        opButtonFunctionGroupID(index) = oRow("FunctionGroupID")
                        opButtonFunFlag(index) = oRow("FunctionFlag")
                        opButtonDrinkSubCat(index) = True
                        If Not oRow("ButtonColor") Is DBNull.Value Then
                            opButtonBackColor(index) = Color.FromArgb(oRow("ButtonColor"))
                            opButtonForeColor(index) = Color.FromArgb(oRow("ButtonForeColor"))
                        Else
                            opButtonBackColor(index) = c6
                            opButtonForeColor(index) = c3
                        End If
                    End If
                Next

            End If

        Else
            If objButton.FunctionFlag = "D" Then 'objButton.Functions >= 4 And objButton.Functions <= 7 Then
                If companyInfo.servesMixedDrinks = True Then
                    '          Me.pnlDrinkModifier.Visible = True
                    '      drinkPrep.Visible = True
                    '   Me.pnlWineParring.Visible = False
                End If
                drinkPrep.Visible = False
                pnlOrderModifier.Visible = False
                pnlOrderModifierExt.Visible = False
                pnlOrder.Visible = False
                pnlOrderQuick.Visible = False

                '     pnlOrderDrink.Visible = True

                ChangeToDrinkButtons()

                If objButton.CategoryID = -1 Then
                    ReDim opButtonText(32)
                    ReDim opButtonId(32)
                    'old?      ReDim opButtonCategoryID(maxMenuIndex + 64)
                    ReDim opButtonFunctionID(32)
                    ReDim opButtonFunctionGroupID(32)
                    ReDim opButtonFunFlag(32)
                    ReDim opButtonBackColor(32)
                    ReDim opButtonForeColor(32)
                    ReDim opButtonHalfSplit(32)
                    ReDim opButtonDrinkSubCat(32)

                    btnOrder(31).MaxMenuIndex = 31

                    For Each oRow In dtDrinkSubCategory.Rows
                        '       index = index + 1
                        If Not oRow("DrinkCategoryName") Is DBNull.Value Then
                            If Not (oRow("DrinkCategoryNumber")) = -1 Then
                                opButtonText(oRow("DrinkCategoryNumber")) = oRow("DrinkCategoryName")
                                opButtonId(oRow("DrinkCategoryNumber")) = oRow("DrinkCategoryID")
                                opButtonHalfSplit(oRow("DrinkCategoryNumber")) = False
                                opButtonDrinkSubCat(oRow("DrinkCategoryNumber")) = False

                                opButtonFunctionID(oRow("DrinkCategoryNumber")) = objButton.Functions      'oRow("DrinkFunctionID")
                                opButtonFunctionGroupID(oRow("DrinkCategoryNumber")) = objButton.FunctionGroup  'oRow("FunctionGroupID")
                                opButtonFunFlag(oRow("DrinkCategoryNumber")) = objButton.FunctionFlag       'oRow("FunctionFlag")

                                If Not oRow("ButtonColor") Is DBNull.Value Then
                                    opButtonBackColor(oRow("DrinkCategoryNumber")) = Color.FromArgb(oRow("ButtonColor"))
                                    opButtonForeColor(oRow("DrinkCategoryNumber")) = Color.FromArgb(oRow("ButtonForeColor"))
                                Else
                                    opButtonBackColor(oRow("DrinkCategoryNumber")) = c16
                                    opButtonForeColor(oRow("DrinkCategoryNumber")) = c3
                                End If
                            End If
                        End If
                    Next
                Else
                    Me.PopulateDrinkSubCategory(objButton)
                    pnlOrder.Visible = False
                    pnlOrderQuick.Visible = False
                    pnlOrderModifier.Visible = False
                    pnlOrderModifierExt.Visible = False
                    Exit Sub
                End If
            End If

        End If

        GTCIndex = -1

        ' *** will not need to pass all this
        DisplayMainOrderButtons(objButton.MainButtonIndex, objButton.CategoryID, objButton.Functions, objButton.FunctionGroup, objButton.FunctionFlag, objButton.IsPrimary, objButton.FoodTableIndex)

    End Sub

    Private Sub ChangeToDrinkButtons() Handles testgridview.DrinkButtonsON

        If companyInfo.servesMixedDrinks = True Then
            Me.btnModifierAdd.Text = "Prep"
            Me.btnModifierNo.Text = "Call"
        End If
        If drinkPrep.Visible = True And currentTable.MarkForNextCustomer = False And OpenOrdersCurrencyMan.Position > -1 Then
            currentTable.ReferenceSIN = CType(Me.testgridview.gridViewOrder.Item(OpenOrdersCurrencyMan.Position, 2), Integer)
        End If

    End Sub

    Private Sub ChangeFromDrinkButtons() Handles testgridview.DrinkButtonsOFF

        If companyInfo.servesMixedDrinks = True Then
            Me.btnModifierAdd.Text = "Add"
            Me.btnModifierNo.Text = "No"
        End If
    End Sub

    Private Sub DisplayMainOrderButtons(ByVal mainButtonIndex As Integer, ByVal catID As Integer, ByVal funID As Integer, ByVal funGroup As Integer, ByVal funFlag As String, ByVal isPrimary As Boolean, ByVal fti As Integer)
        Dim index As Integer
        '    Dim mbi As Integer = mainButtonIndex
        Dim n As Integer = 0
        Dim lastButton As Integer


        If currentTable.IsExtended = True Then 'currentTerminal.TermMethod = "Quick" Or funFlag = "G" Then
            lastButton = 59

            If mainButtonIndex > 1 Then
                btnOrderQuick(0).Text = "Previous"
                btnOrderQuick(0).ID = -1
                btnOrderQuick(0).BackColor = c4
                btnOrderQuick(0).ForeColor = c3
                btnOrderQuick(0).Functions = funID
                btnOrderQuick(0).FunctionGroup = funGroup
                btnOrderQuick(0).FunctionFlag = funFlag
                btnOrderQuick(0).CategoryID = catID
                btnOrderQuick(0).IsPrimary = isPrimary

                n += 1
                '           mainButtonIndex += 1
            End If
            '    mainButtonIndex -= 1


            For index = mainButtonIndex To mainButtonIndex + lastButton
                If n = lastButton Then
                    If btnOrderQuick(n).MaxMenuIndex > index Then
                        '   create MORE button
                        btnOrderQuick(n).Text = "More"
                        btnOrderQuick(n).ID = -2
                        btnOrderQuick(n).BackColor = c4
                        btnOrderQuick(n).ForeColor = c3
                        btnOrderQuick(n).CategoryID = catID
                        btnOrderQuick(n).Functions = funID
                        btnOrderQuick(n).FunctionGroup = funGroup
                        btnOrderQuick(n).FunctionFlag = funFlag
                        btnOrderQuick(n).IsPrimary = isPrimary

                        '          If btnOrderQuick(31).MainButtonIndex = 0 Then
                        If mainButtonIndex = 1 Then
                            btnOrderQuick(lastButton).MainButtonIndex = mainButtonIndex + lastButton + 2
                        Else
                            btnOrderQuick(lastButton).MainButtonIndex = mainButtonIndex + lastButton + 1
                        End If
                        btnOrderQuick(0).MainButtonIndex = btnOrderQuick(31).MainButtonIndex()
                        '              If btnOrderQuick(0).ID = -1 Then btnOrderQuick(0).MainButtonIndex = btnOrderQuick(31).MainButtonIndex()
                        Exit For
                    End If

                End If


                If Not opButtonText(index) Is Nothing Then

                    btnOrderQuick(n).Text = opButtonText(index)
                    btnOrderQuick(n).ID = opButtonId(index)

                    btnOrderQuick(n).BackColor = opButtonBackColor(index) 'c4      'c2
                    btnOrderQuick(n).ForeColor = opButtonForeColor(index)    'c3      'c15
                    btnOrderQuick(n).Functions = opButtonFunctionID(index)   'funID
                    btnOrderQuick(n).FunctionGroup = opButtonFunctionGroupID(index)  ' funGroup
                    btnOrderQuick(n).FunctionFlag = opButtonFunFlag(index)  'funFlag

                    If catID = -1 Then
                        btnOrderQuick(n).CategoryID = opButtonId(index) 'need this duplication for drinks   
                    Else
                        'have no idea if this is correct
                        btnOrderQuick(n).CategoryID = opButtonCategoryID(index)  'catID
                    End If
                    btnOrderQuick(n).HalfSplit = opButtonHalfSplit(index)
                    btnOrderQuick(n).SubCategory = opButtonDrinkSubCat(index)    'False
                    btnOrderQuick(n).IsPrimary = isPrimary
                    btnOrderQuick(n).FoodTableIndex = fti
                    '         If objButton.Name = "Drinks" Then
                    '         btnOrderQuick(index).DrinkCategory = True
                    ''    Else
                    '        btnOrderQuick(index).DrinkCategory = False
                    '   End If
                    btnOrderQuick(n).Invalidate()
                Else
                    '    If Not btnOrderQuick(index).ID = 0 Then  'this might be opButtonID(index)=0 ???
                    '   ********** I have no idea what this is for *********
                    With btnOrderQuick(n)
                        .Text = Nothing
                        .ID = Nothing
                        .BackColor = c13    'c8
                        .DrinkCategory = False          'probably remove subst w/ functions
                        .HalfSplit = False
                        .SubCategory = False
                        .Functions = Nothing         ' this is a test
                        .FunctionFlag = Nothing
                        .FunctionGroup = Nothing
                        .IsPrimary = isPrimary
                    End With
                    btnOrderQuick(n).Invalidate()
                    '  End If
                End If
                n += 1
                If n = lastButton + 1 Then Exit For '   must have this for 2nd page b/c index starts at 1
            Next

            pnlOrderQuick.Visible = True
    
        Else        ' this is NOT extended view(general category)
            lastButton = 31

            If mainButtonIndex > 1 Then
                btnOrder(0).Text = "Previous"
                btnOrder(0).ID = -1
                btnOrder(0).BackColor = c4
                btnOrder(0).ForeColor = c3
                btnOrder(0).Functions = funID
                btnOrder(0).FunctionGroup = funGroup
                btnOrder(0).FunctionFlag = funFlag
                btnOrder(0).CategoryID = catID
                btnOrder(0).IsPrimary = isPrimary

                n += 1
                '           mainButtonIndex += 1
            End If
            '    mainButtonIndex -= 1


            For index = mainButtonIndex To mainButtonIndex + lastButton
                If n = lastButton Then
                    If btnOrder(n).MaxMenuIndex > index Then
                        '   create MORE button
                        btnOrder(n).Text = "More"
                        btnOrder(n).ID = -2
                        btnOrder(n).BackColor = c4
                        btnOrder(n).ForeColor = c3
                        btnOrder(n).CategoryID = catID
                        btnOrder(n).Functions = funID
                        btnOrder(n).FunctionGroup = funGroup
                        btnOrder(n).FunctionFlag = funFlag
                        btnOrder(n).IsPrimary = isPrimary

                        '          If btnOrder(31).MainButtonIndex = 0 Then
                        If mainButtonIndex = 1 Then
                            btnOrder(lastButton).MainButtonIndex = mainButtonIndex + lastButton + 2
                        Else
                            btnOrder(lastButton).MainButtonIndex = mainButtonIndex + lastButton + 1
                        End If
                        btnOrder(0).MainButtonIndex = btnOrder(31).MainButtonIndex()
                        '              If btnOrder(0).ID = -1 Then btnOrder(0).MainButtonIndex = btnOrder(31).MainButtonIndex()
                        Exit For
                    End If

                End If


                If Not opButtonText(index) Is Nothing Then

                    btnOrder(n).Text = opButtonText(index)
                    btnOrder(n).ID = opButtonId(index)

                    btnOrder(n).BackColor = opButtonBackColor(index) 'c4      'c2
                    btnOrder(n).ForeColor = opButtonForeColor(index)    'c3      'c15
                    btnOrder(n).Functions = opButtonFunctionID(index)   'funID
                    btnOrder(n).FunctionGroup = opButtonFunctionGroupID(index)  ' funGroup
                    btnOrder(n).FunctionFlag = opButtonFunFlag(index)  'funFlag

                    If catID = -1 Then
                        btnOrder(n).CategoryID = opButtonId(index) 'need this duplication for drinks   
                    Else
                        'have no idea if this is correct
                        btnOrder(n).CategoryID = opButtonCategoryID(index)  'catID
                    End If
                    btnOrder(n).HalfSplit = opButtonHalfSplit(index)
                    btnOrder(n).SubCategory = opButtonDrinkSubCat(index)    'False
                    btnOrder(n).IsPrimary = isPrimary
                    btnOrder(n).FoodTableIndex = fti
                    '         If objButton.Name = "Drinks" Then
                    '         btnOrder(index).DrinkCategory = True
                    ''    Else
                    '        btnOrder(index).DrinkCategory = False
                    '   End If
                    btnOrder(n).Invalidate()
                Else
                    '    If Not btnOrder(index).ID = 0 Then  'this might be opButtonID(index)=0 ???
                    '   ********** I have no idea what this is for *********
                    With btnOrder(n)
                        .Text = Nothing
                        .ID = Nothing
                        .BackColor = c13    'c8
                        .DrinkCategory = False          'probably remove subst w/ functions
                        .HalfSplit = False
                        .SubCategory = False
                        .Functions = Nothing         ' this is a test
                        .FunctionFlag = Nothing
                        .FunctionGroup = Nothing
                        .IsPrimary = isPrimary
                    End With
                    btnOrder(n).Invalidate()
                    '  End If
                End If
                n += 1
                If n = lastButton + 1 Then Exit For '   must have this for 2nd page b/c index starts at 1
            Next

            If btnOrder(0).FunctionFlag = "D" And btnOrder(0).SubCategory = False Then
                pnlOrderDrink.Visible = False
                pnlOrderQuick.Visible = False
            End If
            pnlOrder.Visible = True


        End If
        '   ??????????
        '      If btnOrder(0).ID = -1 Then btnOrder(0).MainButtonIndex = btnOrder(31).MainButtonIndex()

    End Sub

    Private Function EndOfItem(ByVal resetCurrentTable As Boolean) Handles testgridview.EndingItem

        '    MsgBox("this is end of item   " & resetCurrentTable)

        ' check for req modifiers
        If currentTable.ReqModifier = True Then
            If MsgBox("Must Select Modifier", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then

                Return False
            Else
                'delete last Complete Item
                testgridview.DeleteItem(currentTable.ReferenceSIN, currentTable.ReferenceSIN)
                currentTable.ReqModifier = False
                resetCurrentTable = True
                'will skip any invMultiplier info on purpose
            End If

        Else    'only change inventory if Modifier was not required
            '    change InvMultiplier information
            If Not currentTable.InvMultiplier = 1 Then  'Or Not currentTable.Quantity = 1 Then
                Dim oRow As DataRow
                For Each oRow In dsOrder.Tables("OpenOrders").Rows
                    If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                        If oRow("sii") = currentTable.ReferenceSIN Then
                            '****     oRow("InvMultiplier") = currentTable.InvMultiplier
                            '           oRow("Quantity") = currentTable.Quantity
                            oRow("OpenDecimal1") = currentTable.InvMultiplier
                        End If
                    End If
                Next
            End If
            
        End If

        ' null all current table info
        'make sure to display correct panel

        If resetCurrentTable = True Then
            currentTable.ReferenceSIN = currentTable.SIN
            currentTable.MiddleOfOrder = False
            currentTable.si2 = 0
            currentTable.Tempsi2 = 0
            currentTable.IsPizza = False
            freeFoodActive = False
            currentTable.InvMultiplier = 1
            '    GTCIndex = -1 done in PutUsInNormalMode
            PutUsInNormalMode()

            If currentTable.IsPizza = False Then
                Me.pnlPizzaSplit.Visible = False    '***** added???  8/29/2007
                Me.pnlWineParring.Visible = True
            End If

            pnlOrderModifier.Visible = False
            pnlOrderModifierExt.Visible = False
            drinkPrep.Visible = False

            ' **********  i think, if this is end of Food only 
            '           If currentTable.IsExtended = True Then
            'pnlOrderQuick.Visible = True
            '     Else 
            '        pnlOrder.Visible = True
            '   End If
            Select Case currentTable.ActivePanel
                Case "pnlOrderQuick"
                    pnlOrderQuick.Visible = True
                Case "pnlOrder"
                    pnlOrder.Visible = True
                Case "pnlOrderDrink"
                    pnlOrderDrink.Visible = True
                Case Else
                    currentTable.ActivePanel = Nothing
            End Select

            '      pnlDescription.Visible = False
            If Not currentTable.Quantity = 1 Then
                currentTable.Quantity = 1
                Me.testgridview.ChangeCourseButton(currentTable.Quantity)
            End If
            '    PutUsInNormalMode()
            If currentTable.MarkForNextCustomer = True Then
                currentTable.CustomerNumber = currentTable.NextCustomerNumber
                ChangeCustomerButtonColor(c9)
                If currentTable.MarkForNewCustomerPanel = True Then
                    AddCustomerPanel()
                End If
                currentTable.MarkForNewCustomerPanel = False
                currentTable.MarkForNextCustomer = False
            End If

            Return True 'tells that we can end Item
        Else : Return True
        End If

    End Function

    Private Sub ModifierClick222(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlMain3.Click
        '   4 add item/detail to current order
        '   5 add item/detail to new order
        '   6 description
        '     ClearOrderPanel()
        '      dvFreeFood.Dispose()
        UserControlHit()
        If ADDorNOmode = False Then
            If EndOfItem(True) = False Then Exit Sub
        End If

        Dim index As Integer
        Dim vRow As DataRowView
        Dim objButton As OrderButton

        Try
            objButton = CType(sender, OrderButton)
        Catch ex As Exception
            Exit Sub
        End Try

        '   we don't need a food item
        With dvCategoryModifiers
            .Table = ds.Tables("ModifierTable" & objButton.CategoryID)
            '     .RowFilter = "MenuIndex > 0"
            .Sort = "MenuIndex"
        End With

        If dvCategoryModifiers.Count > 0 Then
            SelectModifier(0, dvCategoryModifiers, 0, "Food", 0, dvCategoryModifiers(0)("Extended"))
            'not sure if we should use (like PerformModifierLoop): dvCategoryJoin(i)("Extended")
        End If

    End Sub

    Private Sub BtnOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles pnlOrder.Click
        UserControlHit()
        If currentTable.IsClosed = True Then Exit Sub 'can't order on closed check

        '      Me.testgridview.gridViewOrder.CurrentRowIndex = OpenOrdersCurrencyMan.Position

        Dim objButton As OrderButton
        Dim index As Integer
        Try
            objButton = CType(sender, OrderButton)
        Catch ex As Exception
            Exit Sub
        End Try

        If objButton.HalfSplit = True Then
            currentTable.TempReferenceSIN = currentTable.ReferenceSIN
            currentTable.SecondRound = False
            onlyHalf = False
            PizzaRoutine(1)
        Else
            currentTable.si2 = 0
            currentTable.Tempsi2 = 0
            currentTable.IsPizza = False
            Me.pnlPizzaSplit.Visible = False
            Me.pnlWineParring.Visible = True
        End If

        '   this should be changed: currently it go back 2 pages for 3 page views
        If objButton.ID = -1 Then
            If objButton.MainButtonIndex = 34 Then
                objButton.MainButtonIndex -= 33
            Else
                '       objButton.MainButtonIndex -= 30
                objButton.MainButtonIndex = 1   '-= 61
                '               If btnOrder(31).ID = -2 Then btnOrder(31).MainButtonIndex = objButton.MainButtonIndex
            End If
            '   may have to add code for btn(31) = btn(0)
            DisplayMainOrderButtons(objButton.MainButtonIndex, objButton.CategoryID, objButton.Functions, objButton.FunctionGroup, objButton.FunctionFlag, objButton.IsPrimary, objButton.FoodTableIndex)
            '       PopulateMainOrderButtons(objButton)
            Exit Sub
        ElseIf objButton.ID = -2 Then
            DisplayMainOrderButtons(objButton.MainButtonIndex, objButton.CategoryID, objButton.Functions, objButton.FunctionGroup, objButton.FunctionFlag, objButton.IsPrimary, objButton.FoodTableIndex)
            '       PopulateMainOrderButtons(objButton)
            Exit Sub
        End If


        currentTable.MiddleOfOrder = True

        If objButton.FunctionFlag = "D" Then 'objButton.Functions >= 4 And objButton.Functions <= 7 Then

            '   *** for Drink
            If objButton.SubCategory = True And objButton.ID > 0 Then

                Dim currentItem As SelectedItemDetail = New SelectedItemDetail
                Dim drinkAddOnBoolean As Boolean = False

                '   *** step 4 ***
                '   Select Drink Adds


                If objButton.DrinkAdds = True Then    ' if true we have gone through this step once
                    '                                 now we are returning to choose drink add
                    '    dvDrink = New DataView(ds.Tables("DrinkAdds"), "DrinkID ='" & objButton.ID & "'", "DrinkID", DataViewRowState.CurrentRows)

                    With dvDrink
                        .Table = ds.Tables("DrinkAdds")
                        .RowFilter = "DrinkID ='" & objButton.ID & "'"
                        .Sort = "DrinkID"
                    End With
                    '         If dvDrink.Count > 0 Then

                    drinkAddOnBoolean = True '(dvDrink(0)("DrinkAddOnChoice"))
                    currentTable.InvMultiplier *= dvDrink(0)("InvMultiplier")

                    With currentItem
                        .ID = dvDrink(0)("DrinkID")
                        If currentTable.OrderingStatus = "NO" Then
                            .Name = " *** " & currentTable.OrderingStatus & " " & dvDrink(0)("DrinkName")
                            .TerminalName = " *** " & currentTable.OrderingStatus & " " & dvDrink(0)("DrinkName")
                            .ChitName = " *** " & currentTable.OrderingStatus & " " & dvDrink(0)("DrinkName")
                            .Price = 0
                            .Quantity = -1 * currentTable.Quantity
                            .InvMultiplier = -1 * currentTable.InvMultiplier

                            If Not currentTerminal.TermMethod = "Quick" Then
                                PutUsInNormalMode()
                                pnlMain.Visible = True
                                pnlMain3.Visible = False
                            End If

                        Else
                            .Quantity = 1 * currentTable.Quantity
                            .InvMultiplier = currentTable.InvMultiplier
                            .Name = "  " & dvDrink(0)("DrinkName")
                            .TerminalName = "  " & dvDrink(0)("DrinkName")
                            .ChitName = "  " & dvDrink(0)("DrinkName")
                            If Not dvDrink(0)("AddOnPrice") Is DBNull.Value Then
                                .Price = dvDrink(0)("AddOnPrice") * currentTable.Quantity
                            Else
                                .Price = 0
                            End If
                        End If

                        '          .TaxID = dvDrink(0)("TaxID")
                        .Category = dvDrink(0)("DrinkCategoryID")
                        .ItemStatus = 0
                        .FunctionID = dvDrink(0)("DrinkFunctionID") 'objButton.Functions
                        .FunctionGroup = dvDrink(0)("FunctionGroupID")
                        .FunctionFlag = dvDrink(0)("FunctionFlag")    'objButton.FunctionFlag
                        If Not dvDrink(0)("DrinkRoutingID") Is DBNull.Value Then    'currentTable.DrinkRouting = 0 And
                            .RoutingID = dvDrink(0)("DrinkRoutingID")
                        Else
                            .RoutingID = currentTable.DrinkRouting
                        End If

                        .SIN = currentTable.SIN
                        .SII = currentTable.ReferenceSIN
                        .si2 = currentTable.si2
                    End With

                Else
                    '   *** step 2 ***
                    '   Select a Drink Choice (when subCategory true but drinkAdds false)
                    ChangeToDrinkButtons()

                    If Not currentTable.Quantity = 1 Then
                        currentTable.Quantity = 1
                        Me.testgridview.ChangeCourseButton(currentTable.Quantity)
                    End If

                    '     dvDrink = New DataView(ds.Tables("Drink"), "DrinkID ='" & objButton.ID & "'", "DrinkID", DataViewRowState.CurrentRows)
                    With dvDrink
                        .Table = ds.Tables("Drink")
                        .RowFilter = "DrinkID ='" & objButton.ID & "'"
                        .Sort = "DrinkID"
                    End With
                    drinkAddOnBoolean = (dvDrink(0)("DrinkAddOnChoice"))
                    currentTable.InvMultiplier *= dvDrink(0)("InvMultiplier")
                    Me.lblRecipe.Text = dvDrink(0)("DrinkDesc")

                    With currentItem
                        .ID = dvDrink(0)("DrinkID")
                        If currentTable.OrderingStatus = "NO" Then
                            .Quantity = currentTable.Quantity * -1
                            .InvMultiplier = -1 * currentTable.InvMultiplier
                            .Name = " *** " & currentTable.OrderingStatus & " " & dvDrink(0)("DrinkName")
                            .TerminalName = " *** " & currentTable.OrderingStatus & " " & dvDrink(0)("DrinkName")
                            .ChitName = " *** " & currentTable.OrderingStatus & " " & dvDrink(0)("DrinkName")
                            .Price = 0
                            If Not currentTerminal.TermMethod = "Quick" Then
                                PutUsInNormalMode()
                                pnlMain.Visible = True
                                pnlMain3.Visible = False
                            End If

                        ElseIf currentTable.OrderingStatus = "Call" Then
                            'ddd
                            .Quantity = currentTable.Quantity
                            .InvMultiplier = currentTable.InvMultiplier
                            .Name = dvDrink(0)("DrinkName")
                            .TerminalName = "   " & dvDrink(0)("DrinkName")
                            .ChitName = "   " & dvDrink(0)("DrinkName")
                            If Not dvDrink(0)("CallPrice") Is DBNull.Value Then
                                .Price = dvDrink(0)("CallPrice") * currentTable.Quantity
                            Else
                                .Price = 0
                            End If

                            Dim oRow As DataRow
                            'this removes the inventory count for main item
                            'all inventory will be from call, 
                            'should build something so you can't delete the call w/o the main
                            For Each oRow In dsOrder.Tables("OpenOrders").Rows
                                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                                    If oRow("sin") = currentTable.ReferenceSIN Then
                                        oRow("OpenDecimal1") = 0
                                    End If
                                End If
                            Next
                       
                            objButton.CategoryID = previousCategory
                            objButton.BackColor = c4
                            objButton.ForeColor = c3
                            PutUsInNormalMode()
                            drinkAddOnBoolean = False

                            PopulateDrinkSubCategory(objButton)

                        Else
                            .Quantity = currentTable.Quantity
                            .InvMultiplier = currentTable.InvMultiplier
                            .Name = dvDrink(0)("DrinkName")
                            .TerminalName = dvDrink(0)("DrinkName")
                            .ChitName = dvDrink(0)("DrinkName")
                            If Not dvDrink(0)("DrinkPrice") Is DBNull.Value Then
                                .Price = dvDrink(0)("DrinkPrice") * currentTable.Quantity
                            Else
                                .Price = 0
                            End If

                        End If

                        '                   .TaxID = dvDrink(0)("TaxID")
                        .Category = dvDrink(0)("DrinkCategoryID")
                        .ItemStatus = 0
                        .FunctionID = dvDrink(0)("DrinkFunctionID") 'objButton.Functions
                        .FunctionGroup = dvDrink(0)("FunctionGroupID")
                        .FunctionFlag = dvDrink(0)("FunctionFlag")  'objButton.FunctionFlag

                        If Not dvDrink(0)("DrinkRoutingID") Is DBNull.Value Then        'currentTable.DrinkRouting = 0 And
                            .RoutingID = dvDrink(0)("DrinkRoutingID")
                        Else
                            .RoutingID = currentTable.DrinkRouting
                        End If
                        .SIN = currentTable.SIN
                        .SII = currentTable.ReferenceSIN
                        .si2 = currentTable.si2
                    End With
                    '          End If
                    '     End If

                End If
                '         MsgBox(dvDrink(0)("DrinkAddID"))


                With currentItem
                    '          If currentTable.IsTabNotTable = False Then
                    '         .Table = currentTable.TableNumber
                    '        Else
                    '           .Table = currentTable.TabID
                    '      End If
                    .Check = currentTable.CheckNumber
                    .Customer = currentTable.CustomerNumber
                    .Course = currentTable.CourseNumber

                End With



                AddItemToOrderTable(currentItem)
                Me.testgridview.CalculateSubTotal()
                currentTable.SIN += 1
                '        If objButton.DrinkAdds = False Then
                '       currentTable.ReferenceSIN = currentTable.SIN
                '  End If


                '   *** step 3 ***
                '   Populates Drink Adds if necessary
                '         If objButton.DrinkAdds = False Then
                If drinkAddOnBoolean = True Then

                    '444      SelectDrinkAdds(objButton.ID, objButton.CategoryID, currentItem.RoutingID, objButton.Functions, objButton.FunctionGroup)
                    SelectDrinkAdds(currentItem.ID, currentItem.Category, currentItem.RoutingID, currentItem.FunctionID, currentItem.FunctionGroup)
                    currentTable.MiddleOfOrder = True   '????      ' will become false below if no adds

                    If Not currentTable.Quantity = 1 Then
                        currentTable.Quantity = 1
                        Me.testgridview.ChangeCourseButton(currentTable.Quantity)
                    End If
                    If currentTable.MarkForNextCustomer = True Then
                        currentTable.CustomerNumber = currentTable.NextCustomerNumber
                        ChangeCustomerButtonColor(c9)
                        If currentTable.MarkForNewCustomerPanel = True Then
                            AddCustomerPanel()
                        End If
                        currentTable.MarkForNewCustomerPanel = False
                        currentTable.MarkForNextCustomer = False
                    End If
                    Exit Sub
                Else
                    currentTable.ReferenceSIN = currentTable.SIN
                    '               currentTable.SIN += 1
                    '          currentTable.MiddleOfOrder = False
                End If
                '        Else
                '         currentTable.ReferenceSIN = currentTable.SIN
                '         currentTable.SIN += 1
                If EndOfItem(True) = False Then Exit Sub
                currentTable.MiddleOfOrder = False
                currentTable.si2 = 0
                currentTable.Tempsi2 = 0
                currentTable.IsPizza = False
                '     Me.pnlPizzaSplit.Visible = False

                If Not currentTable.Quantity = 1 Then
                    currentTable.Quantity = 1
                    Me.testgridview.ChangeCourseButton(currentTable.Quantity)
                End If
                If currentTable.MarkForNextCustomer = True Then
                    currentTable.CustomerNumber = currentTable.NextCustomerNumber
                    ChangeCustomerButtonColor(c9)
                    If currentTable.MarkForNewCustomerPanel = True Then
                        AddCustomerPanel()
                    End If
                    currentTable.MarkForNewCustomerPanel = False
                    currentTable.MarkForNextCustomer = False
                End If
                '       End If
                '         MsgBox(objButton.ID)



                '   ************
                '   testing
                '            Me.PopulateDrinkSubCategory(objButton)
                '           ResetDrinkCategories()
                Exit Sub
                '       ClearOrderPanel()


                '   *** step 1 ***
                '   Populates the drink choices based on selected category
                '   When: DrinkCategory is true and subCategory is false
            Else
                PopulateDrinkSubCategory(objButton)
            End If

            '   *** for food items ***
        Else
            If objButton.ID > 0 Then

                '666         If objButton.FunctionFlag = "M" Then
                '         With dvCategoryModifiers
                '    .Table = ds.Tables("ModifierTable" & objButton.CategoryID)
                '   .Sort = "MenuIndex"
                '    End With

                '     If dvCategoryModifiers.Count > 0 Then
                '    SelectModifier(0, dvCategoryModifiers, 0, "Food", 0, dvCategoryModifiers(0)("Extended"))
                '   End If
                '    Exit Sub
                '   End If

                Me.lblWineParring.Text = ""
                Me.lblRecipe.Text = ""
                If Not currentTable.Quantity = 1 Then
                    currentTable.Quantity = 1
                    Me.testgridview.ChangeCourseButton(currentTable.Quantity)
                End If

                Dim currentItem As SelectedItemDetail = New SelectedItemDetail
                Dim populatingTable As String

                If currentTable.IsPrimaryMenu = True Then
                    populatingTable = "MainTable"
                    cntModifierLoop = mainCategoryIDArrayList.Count
                Else
                    populatingTable = "SecondaryMainTable"
                    cntModifierLoop = secondaryCategoryIDArrayList.Count
                End If
                '      If typeProgram = "Online_Demo" Then
                '     cntModifierLoop = 30
                'End If
                '   dvFood = New DataView(ds.Tables(populatingTable & objButton.CategoryID), "FoodID ='" & objButton.ID & "'", "FoodID", DataViewRowState.CurrentRows)

                With dvFood
                    .Table = ds.Tables(populatingTable & objButton.CategoryID)
                    .RowFilter = "FoodID ='" & objButton.ID & "'"
                    .Sort = "FoodID"
                End With

                If dvFood.Count > 0 Then
                    '   ************
                    '   dvFood should change to use only the datatable
                    '   since we know which table we use now just use that
                    If dvFood(0)("WineParringID") > 0 Then
                        Dim oRow As DataRow
                        Dim wpiText As String
                        For Each oRow In ds.Tables("Drink").Rows
                            If oRow("DrinkID") = dvFood(0)("WineParringID") Then
                                wpiText = oRow("DrinkName") & "  (" & oRow("DrinkDesc") & ")"
                                Me.lblWineParring.Text = wpiText
                            End If
                        Next
                    End If
                    Me.lblRecipe.Text = dvFood(0)("FoodDesc")
                    currentTable.InvMultiplier *= dvFood(0)("InvMultiplier")

                    With currentItem
                        .ID = dvFood(0)("FoodID")
                        If currentTable.OrderingStatus = "NO" Then
                            .Quantity = currentTable.Quantity * -1
                            .InvMultiplier = -1 * currentTable.InvMultiplier
                            .Name = " *** " & currentTable.OrderingStatus & " " & dvFood(0)("AbrevFoodName")
                            .TerminalName = " *** " & currentTable.OrderingStatus & " " & dvFood(0)("AbrevFoodName")
                            .ChitName = " *** " & currentTable.OrderingStatus & " " & dvFood(0)("ChitFoodName")
                            .Price = 0
                            '              PutUsInNormalMode()
                            '             .FunctionID = 0
                            '            .FunctionGroup = 0
                        Else
                            .Quantity = currentTable.Quantity
                            .InvMultiplier = currentTable.InvMultiplier
                            .Name = dvFood(0)("AbrevFoodName")
                            .TerminalName = dvFood(0)("AbrevFoodName")
                            .ChitName = dvFood(0)("ChitFoodName")
                            If Not currentTable.CurrentMenu = currentTerminal.currentPrimaryMenuID Then
                                .TerminalName = .Name & "  * " & currentTable.CurrentMenuName.ToUpper
                                .ChitName = .ChitName & "  * " & currentTable.CurrentMenuName.ToUpper
                            End If
                            .Price = dvFood(0)("Price") * currentTable.Quantity
                            If dvFood(0)("TaxExempt") = True Then
                                .TaxID = -1
                            End If


                        End If
                        .FunctionID = dvFood(0)("FunctionID") 'objButton.Functions
                        .FunctionGroup = dvFood(0)("FunctionGroupID") 'objButton.FunctionGroup
                        .FunctionFlag = dvFood(0)("FunctionFlag")  'objButton.FunctionFlag
                        .Category = dvFood(0)("CategoryID")
                        If Not dvFood(0)("RoutingID") Is DBNull.Value Then
                            .RoutingID = dvFood(0)("RoutingID")
                        Else
                            .RoutingID = 0
                        End If
                        If Not dvFood(0)("PrintPriorityID") Is DBNull.Value Then
                            .PrintPriorityID = dvFood(0)("PrintPriorityID")
                        Else
                            .PrintPriorityID = 0
                        End If


                    End With
                    '   the above code has to change FoodCost *** it has to be dependant on menu

                    With currentItem
                        '                If currentTable.IsTabNotTable = False Then
                        '               .Table = currentTable.TableNumber
                        '              Else
                        '                 .Table = currentTable.TabID
                        '            End If
                        .Check = currentTable.CheckNumber
                        .Customer = currentTable.CustomerNumber
                        .Course = currentTable.CourseNumber
                        .SIN = currentTable.SIN
                        .SII = currentTable.SIN
                        .si2 = currentTable.si2

                    End With
                    currentTable.ReferenceSIN = currentTable.SIN
                    '          currentTable.MiddleOfOrder = False
                    currentTable.SIN += 1

                    '444        pnlOrder.Visible = False
                    '444        pnlOrderQuick.Visible = False
                    '444    pnlOrderDrink.Visible = False
                    AddItemToOrderTable(currentItem)
                    Me.testgridview.CalculateSubTotal()

                    '********************
                    '       PerformModifierLoop()

                    StartModifierLoop(currentItem.ID)

                End If
            End If
        End If

    End Sub

    Private Sub StartModifierLoop(ByVal fID As Integer)
        Dim vRow As DataRowView
        Dim i As Integer
        modifierIndex = 0
        performedIndividualJoinTest = False

        '      dvIndividualJoinGroup = New DataView

        If currentTable.IsPrimaryMenu = True Then
            With dvIndividualJoinGroup
                .Table = ds.Tables("IndividualJoinMain")
                .RowFilter = "FoodID = '" & fID & "'"
                .Sort = "MenuIndex"
            End With
        Else
            With dvIndividualJoinGroup
                .Table = ds.Tables("IndividualJoinSecondary")
                .RowFilter = "FoodID = '" & fID & "'"
                .Sort = "MenuIndex"
            End With
        End If

        '       dvCategoryJoin = New DataView
        With dvCategoryJoin
            .Table = ds.Tables("CategoryJoin")
            .RowFilter = "FoodID = '" & fID & "'"
            .Sort = "Priority, FreeFlag"        '"MenuIndex" '
        End With

        ReDim categoryIndex(dvCategoryJoin.Count)
        For Each vRow In dvCategoryJoin
            categoryIndex(i) = True
            i += 1
        Next

        If dvIndividualJoinGroup.Count + dvCategoryJoin.Count > 0 Then
            pnlOrder.Visible = False
            pnlOrderQuick.Visible = False
            pnlOrderDrink.Visible = False
        End If

        If currentTable.OrderingStatus = "NO" Then
            '   *** not sure about
            '         ADDorNOmode = False
            '     btnModifierAdd.BackColor = c4
            '      btnModifierNo.BackColor = c4

            '      currentTable.OrderingStatus = Nothing
            If Not currentTerminal.TermMethod = "Quick" Then
                PutUsInNormalMode()
            End If
            '  
        Else
            PerformModifierLoop(fID)
        End If


    End Sub

    Private Sub PizzaRoutine(ByVal valuesi2 As Integer) Handles testgridview.RunPizzaRoutine

        currentTable.IsPizza = True
        CreateDataViewsPizza()
        '      Me.onFullPizza.DataSource = dvPizzaFull
        '     Me.onFirstHalf.DataSource = dvPizzaFirst
        '    Me.onSecondHalf.DataSource = dvPizzaSecond
        Me.pnlPizzaSplit.Visible = True
        '      Me.pnlDrinkModifier.Visible = False
        drinkPrep.Visible = False
        Me.pnlWineParring.Visible = False

        If valuesi2 = 1 Then
            Me.pnlOnFullPizza.BackColor = c18
            Me.pnlOnFirstHalf.BackColor = c3
            Me.pnlOnSecondHalf.BackColor = c3
            currentTable.si2 = 1
        ElseIf valuesi2 = 2 Then
            Me.pnlOnFullPizza.BackColor = c3
            Me.pnlOnFirstHalf.BackColor = c18
            Me.pnlOnSecondHalf.BackColor = c3
            currentTable.si2 = 2
        ElseIf valuesi2 = 3 Then
            Me.pnlOnFullPizza.BackColor = c3
            Me.pnlOnFirstHalf.BackColor = c3
            Me.pnlOnSecondHalf.BackColor = c18
            currentTable.si2 = 3
        End If

        If currentTable.SecondRound = True Then
            Dim oRow As DataRow
            Dim fID As Integer

            For Each oRow In dsOrder.Tables("OpenOrders").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("sin") = currentTable.ReferenceSIN Then
                        ' if we do this with drinks we will need to distinguish between food
                        fID = oRow("ItemID")
                        Exit For
                    End If
                End If
            Next
            pnlOrder.Visible = False
            pnlOrderQuick.Visible = False
            pnlOrderDrink.Visible = False
            drinkPrep.Visible = False
            StartModifierLoop(fID)
            '            PerformModifierLoop(fID)
            '   currentTable.SecondRound = False
        End If

        onFullPizza.SelectedIndex = -1
        onFirstHalf.SelectedIndex = -1
        onSecondHalf.SelectedIndex = -1

    End Sub

    '   Private Sub BtnOrderDrink_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlOrderDrink.Click
    '
    '   End Sub

    Private Sub PopulateDrinkSubCategory(ByVal objButton As OrderButton)


        Dim vRow As DataRowView
        Dim opButtonText(60) As String      'changed 
        Dim opButtonID(60) As Integer
        Dim opButtonBackColor(60)
        Dim opButtonForeColor(60)
        '     Dim opButtonCategoryID(maxMenuIndex + 64)
        '    Dim opButtonFunctionID(maxMenuIndex + 64)
        '   Dim opButtonFunctionGroupID(maxMenuIndex + 64)
        '  Dim opButtonFunFlag(maxMenuIndex + 64)
        ' Dim opButtonDrinkSubCat(maxMenuIndex + 64))

        Dim index As Integer

        If objButton.CategoryID > 0 Then
            '       dvDrink = New DataView(ds.Tables("Drink"), "DrinkCategoryID ='" & objButton.CategoryID & "'", "DrinkID", DataViewRowState.CurrentRows)
            '           PopulateDrinkTable(objButton.ID)
            With dvDrink
                .Table = ds.Tables("Drink")
                .RowFilter = "DrinkCategoryID ='" & objButton.CategoryID & "'"
                .Sort = "DrinkID"
            End With

            For Each vRow In dvDrink
                opButtonText(vRow("DrinkIndex")) = vRow("DrinkName")
                opButtonID(vRow("DrinkIndex")) = vRow("DrinkID")
                'not sure if this is always right
                '       opButtonBackColor(vRow("DrinkIndex")) = objButton.BackColor
                '      opButtonForeColor(vRow("DrinkIndex")) = objButton.ForeColor

            Next
        End If

        pnlOrder.Visible = False
        pnlOrderQuick.Visible = False

        '     index = 0
        For index = 1 To 60 '48
            If opButtonText(index) Is Nothing Then
                btnOrderDrink(index).Text = Nothing
                btnOrderDrink(index).ID = Nothing
                btnOrderDrink(index).CategoryID = Nothing
                btnOrderDrink(index).SubCategory = True      ' probably false
                '      btnOrderDrink(index).DrinkAdds = False
                btnOrderDrink(index).Functions = Nothing
                btnOrderDrink(index).FunctionGroup = Nothing
                btnOrderDrink(index).FunctionFlag = Nothing

                btnOrderDrink(index).BackColor = c13    'c8
            Else
                btnOrderDrink(index).Text = opButtonText(index)
                btnOrderDrink(index).ID = opButtonID(index)
                btnOrderDrink(index).CategoryID = objButton.CategoryID
                btnOrderDrink(index).SubCategory = True
                '         btnOrderDrink(index).DrinkAdds = objButton.DrinkAdds
                btnOrderDrink(index).Functions = objButton.Functions
                btnOrderDrink(index).FunctionGroup = objButton.FunctionGroup
                btnOrderDrink(index).FunctionFlag = objButton.FunctionFlag
                btnOrderDrink(index).BackColor = objButton.BackColor    'c4
                btnOrderDrink(index).ForeColor = objButton.ForeColor    'c3

            End If
        Next
        '            objButton.DrinkCategory = False
        Me.pnlOrderDrink.Visible = True
        currentTable.ActivePanel = "pnlOrderDrink"
        '        pnlOrder.Visible = True

    End Sub

    Private Sub PerformModifierLoop(ByVal foodItem As Integer)
        Dim index As Integer
        Dim vRow As DataRowView
        Dim cRow As DataRowView
        Dim i As Integer
        Dim c As Integer
        Dim tableType As String
        Dim currentModifierID As Integer
        Dim oRow As DataRow

        '       currentTable.IsExtended = False
        btnOrderModifier(23).ModifierButtonIndex = 0
        btnOrderModifier(23).ModifierMenuIndex = 0
        btnOrderModifierExt(59).ModifierButtonIndex = 0
        btnOrderModifierExt(59).ModifierMenuIndex = 0

        If dvIndividualJoinGroup.Count > 0 Then
            Do Until modifierIndex > cntModifierLoop - 1

                If currentTable.IsPrimaryMenu = True Then
                    currentModifierID = mainCategoryIDArrayList(modifierIndex)
                    '     dvIndividualJoinAuto = New DataView

                    With dvIndividualJoinAuto
                        .Table = ds.Tables("IndividualJoinMain")
                        .RowFilter = "FoodID = '" & foodItem & "' and CategoryID = '" & currentModifierID & "' and GroupFlag = 'A'"
                    End With

                    '     dvIndividualJoinGroup = New DataView
                    With dvIndividualJoinGroup
                        .Table = ds.Tables("IndividualJoinMain")
                        .RowFilter = "FoodID = '" & foodItem & "'and CategoryID = '" & currentModifierID & "' and GroupFlag = 'G'"
                    End With
                Else
                    currentModifierID = secondaryCategoryIDArrayList(modifierIndex)
                    '        dvIndividualJoinAuto = New DataView
                    With dvIndividualJoinAuto
                        .Table = ds.Tables("IndividualJoinSecondary")
                        .RowFilter = "FoodID = '" & foodItem & "' and CategoryID = '" & currentModifierID & "' and GroupFlag = 'A'"
                    End With

                    '       dvIndividualJoinGroup = New DataView
                    With dvIndividualJoinGroup
                        .Table = ds.Tables("IndividualJoinSecondary")
                        .RowFilter = "FoodID = '" & foodItem & "'and CategoryID = '" & currentModifierID & "' and GroupFlag = 'G'"
                    End With
                End If

                If dvIndividualJoinAuto.Count > 0 And currentTable.SecondRound = False Then
                    '                Exit Do
                    For Each vRow In dvIndividualJoinAuto
                        Dim currentmodifier As SelectedItemDetail = New SelectedItemDetail
                        Dim populatingTable As String
                        Dim catID As Integer
                        Dim funFlag As String
                        funFlag = vRow("FunctionFlag")
                        catID = vRow("CategoryID")
                        currentTable.InvMultiplier *= vRow("InvMultiplier")

                        If currentTable.IsPrimaryMenu = True Then
                            populatingTable = "MainTable"
                        Else
                            populatingTable = "SecondaryMainTable"
                        End If

                        With currentmodifier
                            .ID = vRow("ModifierID")
                            '   may get rid of .Name (duplicate?)
                            .Name = "   " & vRow("AbrevFoodName")
                            .TerminalName = "   " & vRow("AbrevFoodName")
                            .ChitName = "   " & vRow("ChitFoodName")
                            '   added spaces here b/c it goes directly to datagrid
                            If vRow("FreeFlag") = "F" And currentTable.SecondRound = False Then 'Or dvIndividualJoin(0)("Surcharge") Is DBNull.Value Then
                                .Price = 0
                            Else
                                If Not vRow("Surcharge") Is DBNull.Value Then
                                    .Price = vRow("Surcharge") * currentTable.Quantity
                                Else
                                    .Price = 0
                                End If
                            End If
                            .FunctionID = vRow("FunctionID")
                            .FunctionGroup = vRow("FunctionGroupID")
                            .FunctionFlag = vRow("FunctionFlag")
                            .Category = vRow("CategoryID")

                            If currentTable.IsTabNotTable = False Then
                                .Table = currentTable.TableNumber
                            Else
                                .Tab = currentTable.TabID
                            End If
                            .Check = currentTable.CheckNumber
                            .Customer = currentTable.CustomerNumber
                            .Course = currentTable.CourseNumber
                            .Quantity = currentTable.Quantity
                            .InvMultiplier = currentTable.InvMultiplier
                            .SII = currentTable.ReferenceSIN
                            .SIN = currentTable.SIN
                            .si2 = currentTable.si2
                        End With
                        currentTable.SIN += 1
                        AddItemToOrderTable(currentmodifier)
                        '    i += 1
                    Next
                End If

                If dvIndividualJoinGroup.Count > 0 Then
                    If dvIndividualJoinGroup.Count > 23 Then ReDim freeFood(dvIndividualJoinGroup.Count)
                    '        If currentTable.IsPizza = False  Then
                    '   End If
                    freeFoodActive = True
                    For Each vRow In dvIndividualJoinGroup

                        If vRow("FreeFlag") = "F" And currentTable.SecondRound = False Then      'has NO association to function Flag
                            freeFood(index) = True
                        Else
                            freeFood(index) = False
                        End If
                        index += 1
                        '        numberFree(index) = dvIndividualJoin(0)("NumberFree")    'uses the first record only
                    Next
                    Exit Do
                End If

                modifierIndex += 1
            Loop

            If dvIndividualJoinGroup.Count > 0 Then
                SelectModifier(foodItem, dvIndividualJoinGroup, 0, "Modifier", 0, False)
                modifierIndex += 1
                '***         dvIndividualJoinGroup = New DataView   'resets dv for next modifierIndex value
                freeFoodActive = False
                Exit Sub
            End If

            If dvIndividualJoinAuto.Count > 0 Then
                '***         dvIndividualJoinAuto = New DataView
                modifierIndex += 1
                PerformModifierLoop(foodItem)
                Exit Sub 'this is so it won't keep looping
            End If

        End If


        '************************
        '       Category Loop

        i = 0
        For Each vRow In dvCategoryJoin

            If categoryIndex(i) = True Then
                '         numberFree(i) = dvCategoryJoin(i)("NumberFree")
                '     dvCategoryModifiers = New DataView

                If dvCategoryJoin(i)("FunctionFlag") = "F" Or dvCategoryJoin(i)("FunctionFlag") = "O" Or dvCategoryJoin(i)("FunctionFlag") = "G" Then

                    If currentTable.IsPrimaryMenu = True Then
                        '   *****    there is a problem if Category is not on Menu      ********
                        If ds.Tables("MainTable" & dvCategoryJoin(i)("CategoryID")).rows.count > 0 Then
                            With dvCategoryModifiers
                                .Table = ds.Tables("MainTable" & dvCategoryJoin(i)("CategoryID"))
                                .Sort = "MenuIndex"
                                '*** currently Foods do not have order when they are attached modifiers
                            End With
                        End If
                    Else
                        If ds.Tables("SecondaryMainTable" & dvCategoryJoin(i)("CategoryID")).rows.count > 0 Then
                            With dvCategoryModifiers
                                .Table = ds.Tables("SecondaryMainTable" & dvCategoryJoin(i)("CategoryID"))
                                .Sort = "MenuIndex"
                                '*** currently Foods do not have order when they are attached modifiers
                            End With
                        End If
                    End If

                Else
                    Try
                        With dvCategoryModifiers
                            .Table = ds.Tables("ModifierTable" & dvCategoryJoin(i)("CategoryID"))
                            '   *** do we want to filter out MenuIndex =0 ????
                            '        .RowFilter = "MenuIndex > 0"
                            .Sort = "MenuIndex"
                        End With
                    Catch ex As Exception
                        MsgBox(dvCategoryJoin(i)("CategoryID"))
                    End Try
                End If

                'freefree 
                If dvCategoryModifiers.Count > 23 Then ReDim freeFood(dvCategoryModifiers.Count)

                If dvCategoryJoin(i)("FreeFlag") = "F" And currentTable.SecondRound = False Then

                    c = 0
                    For Each cRow In dvCategoryModifiers
                        freeFood(c) = True
                        c += 1
                    Next
                    freeFoodActive = True
                Else
                    freeFoodActive = False
                End If

                If dvCategoryModifiers.Count > 0 Then

                    SelectModifier(foodItem, dvCategoryModifiers, 0, "Food", 0, dvCategoryJoin(i)("Extended"))
                    If Not dvCategoryJoin(i)("GTCFlag") = "C" Then

                        If onlyHalf = True Then
                            GTCIndex = i
                        Else
                            GTCIndex += 1
                            categoryIndex(i) = False
                        End If

                    Else
                        GTCIndex = i
                     
                    End If
                    If dvCategoryJoin(i)("ReqFlag") = "R" Then
                        currentTable.ReqModifier = True
                    End If
                    Exit Sub
                Else
                    ' nothing, go to next category
                End If

            End If
            i += 1
        Next


        ' *****  this is the end of the current item after modifiers
        currentTable.ReferenceSIN = currentTable.SIN
        GTCIndex = -1
        freeFoodActive = False

        'this one below removes the highlight of last hit grid cell
        'but it will add items in the begining (if hit ADD button)
        If EndOfItem(True) = False Then Exit Sub

        currentTable.MiddleOfOrder = False
        If currentTable.IsPizza = False Then
            Me.pnlPizzaSplit.Visible = False    '***** added???  8/29/2007
            Me.pnlWineParring.Visible = True
        End If
        '      currentTable.si2 = 0
        '     currentTable.Tempsi2 = 0
        '     currentTable.IsPizza = False

        pnlOrderModifier.Visible = False
        pnlOrderModifierExt.Visible = False

        ' **********  i think, if this is end of Food only 
        If currentTable.IsExtended = True Then
            pnlOrderQuick.Visible = True
        Else
            pnlOrder.Visible = True
        End If

        '      pnlDescription.Visible = False
        If Not currentTable.Quantity = 1 Then
            currentTable.Quantity = 1
            Me.testgridview.ChangeCourseButton(currentTable.Quantity)
        End If
        '    PutUsInNormalMode()
        If currentTable.MarkForNextCustomer = True Then
            currentTable.CustomerNumber = currentTable.NextCustomerNumber
            ChangeCustomerButtonColor(c9)
            If currentTable.MarkForNewCustomerPanel = True Then
                AddCustomerPanel()
            End If
            currentTable.MarkForNewCustomerPanel = False
            currentTable.MarkForNextCustomer = False
        End If
    End Sub

    Private Sub BtnOrderModifier_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles pnlOrderModifier.Click
        UserControlHit()
        If currentTable.LastStatus = 1 Then Exit Sub 'can't order on closed check

        Dim objButton As OrderButton
        Dim index As Integer

        Try
            objButton = CType(sender, OrderButton)
        Catch ex As Exception
            Exit Sub
        End Try

        If objButton.ID = -1 Then
            If objButton.ModifierButtonIndex > 0 Then   '= 23 Then
                btnOrderModifier(0).ModifierButtonIndex = 0 '-= 23
                btnOrderModifier(0).ModifierMenuIndex = 0
            Else
                btnOrderModifier(0).ModifierButtonIndex = 0  '-= 22
                btnOrderModifier(0).ModifierMenuIndex = 0
            End If
            If objButton.Extended = True Then
                btnOrderModifier(59).ModifierButtonIndex = btnOrderModifier(0).ModifierButtonIndex
                btnOrderModifier(59).ModifierMenuIndex = btnOrderModifier(0).ModifierMenuIndex
            Else
                btnOrderModifier(23).ModifierButtonIndex = btnOrderModifier(0).ModifierButtonIndex
                btnOrderModifier(23).ModifierMenuIndex = btnOrderModifier(0).ModifierMenuIndex
            End If

            '************************   put Food in for tableType (2 places below) to test **********************
            If objButton.CategoryID < 100 Then

            End If
            SelectModifier(objButton.FoodID, objButton.ActiveDataView, objButton.ModifierButtonIndex, "Food", objButton.ModifierMenuIndex, objButton.Extended)
            Exit Sub
        ElseIf objButton.ID = -2 Then
            SelectModifier(objButton.FoodID, objButton.ActiveDataView, objButton.ModifierButtonIndex, "Food", objButton.ModifierMenuIndex, objButton.Extended)
            Exit Sub
        End If

        If objButton.ID > 0 Then
            If ADDorNOmode = True Then
                '   we are in AddMODE or NoMode
                Dim oRow As DataRow
                For Each oRow In dsOrder.Tables("OpenOrders").Rows
                    If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                        If oRow("sin") = currentTable.ReferenceSIN Then
                            If oRow("ItemStatus") > 1 Then
                                info = New DataSet_Builder.Information_UC("This Item Has already been sent to the Kitchen.")
                                info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
                                Me.Controls.Add(info)
                                info.BringToFront()
                                '                            MsgBox("This Item Has already been sent to the Kitchen.")
                                Exit Sub
                            End If
                        End If
                    End If
                Next
            End If

            Dim currentmodifier As SelectedItemDetail = New SelectedItemDetail
            currentTable.InvMultiplier *= objButton.InvMultiplier

            '   add 3 space for indention
            '   must also add when modifing
            With currentmodifier
                .ID = objButton.ID ' (dtr("ModifierID"))
                .Category = objButton.CategoryID '(dtr("CategoryID"))
                If currentTable.OrderingStatus = "NO" Then
                    .Quantity = currentTable.Quantity * -1
                    .InvMultiplier = -1 * currentTable.InvMultiplier
                    .Name = " *** " & currentTable.OrderingStatus & " " & objButton.Name
                    .TerminalName = " *** " & currentTable.OrderingStatus & " " & objButton.Name
                    .ChitName = " *** " & currentTable.OrderingStatus & " " & objButton.Name
                    .Price = 0
                    If Not currentTerminal.TermMethod = "Quick" Then
                        PutUsInNormalMode()
                        pnlMain.Visible = True
                        pnlMain3.Visible = False
                    End If

                    .FunctionID = objButton.Functions          '3
                    .FunctionGroup = objButton.FunctionGroup
                    .FunctionFlag = objButton.FunctionFlag
                    '                    .FunctionID = 0
                    '                   .FunctionGroup = 0
                Else
                    .Quantity = currentTable.Quantity
                    'moved below  .InvMultiplier = currentTable.Quantity * objButton.InvMultiplier
                    If currentTable.OrderingStatus = "ADD" Then
                        .Name = " *** " & currentTable.OrderingStatus & " " & objButton.Name
                        .TerminalName = " *** " & currentTable.OrderingStatus & " " & objButton.Name
                        .ChitName = " *** " & currentTable.OrderingStatus & " " & objButton.Name
                        .RoutingID = CType(Me.testgridview.gridViewOrder.Item(OpenOrdersCurrencyMan.Position, 10), Integer)
                        '       If Not currentTerminal.TermMethod = "Quick" Then
                        '        PutUsInNormalMode()
                        '   End If

                    Else
                        .Name = "   " & objButton.Name '(dtr("ModifierName"))
                        .TerminalName = "   " & objButton.Name '(dtr("ModifierName"))
                        .ChitName = "   " & objButton.Name '(dtr("ModifierName"))
                    End If
                    If currentTable.si2 > 1 And currentTable.si2 < 10 Then

                        .Price = (objButton.Cost * currentTable.Quantity * 0.5)
                        .InvMultiplier = 0.5 * currentTable.InvMultiplier
                        If onlyHalf = False Then
                            If Not GTCIndex = -1 Then
                                categoryIndex(GTCIndex) = True
                            End If
                            onlyHalf = True
                        Else
                            If Not GTCIndex = -1 Then 'GTCIndex = 0 Then '
                                If Not dvCategoryJoin(GTCIndex)("GTCFlag") = "C" Then
                                    categoryIndex(GTCIndex) = False
                                End If

                            End If
                            onlyHalf = False
                        End If

                    Else
                        .Price = (objButton.Cost * currentTable.Quantity)
                        .InvMultiplier = currentTable.InvMultiplier
                    End If
                    .FunctionID = objButton.Functions          '3
                    .FunctionGroup = objButton.FunctionGroup
                    .FunctionFlag = objButton.FunctionFlag
                End If

        If isSecondLoop = True Then
            .Name = ("   " & .Name).ToLower
            .TerminalName = ("   " & .Name).ToLower
            .ChitName = ("   " & .Name).ToLower
        End If
            End With

            With currentmodifier
                If currentTable.IsTabNotTable = False Then
                    .Table = currentTable.TableNumber
                Else
                    .Tab = currentTable.TabID
                End If
                .Check = currentTable.CheckNumber
                .Customer = currentTable.CustomerNumber
                .Course = currentTable.CourseNumber
                .SII = currentTable.ReferenceSIN
                .SIN = currentTable.SIN
                .si2 = currentTable.si2
            End With
            currentTable.SIN += 1

            isSecondLoop = CheckForSecondaryModifierLoop(objButton.ID)
            If isSecondLoop = True Then
                If currentTable.Tempsi2 < 10 Then
                    'we only record the temp once (memory in case of pizza)
                    currentTable.Tempsi2 = currentTable.si2
                End If
                currentTable.si2 += 10
                currentmodifier.si2 = currentTable.si2

            End If

            AddItemToOrderTable(currentmodifier)
            Me.testgridview.CalculateSubTotal()
            currentTable.ReqModifier = False

            If isSecondLoop = True Then
                modifierIndexSecondLoop = 0
                PerformSecondModifierLoop(objButton.ID)
                Exit Sub
            Else
                If currentTable.si2 >= 10 Then
                    currentTable.si2 += 10
                    currentmodifier.si2 = currentTable.si2
                End If
            End If

            If pnlMain3.Visible = False Then
                PerformModifierLoop(objButton.FoodID)
                '     Else
            End If
            '       PutUsInNormalMode()

        ElseIf objButton.ID < 0 Then
            '   this is for cancel button
            If currentTable.ReqModifier = True Then
                '  If EndOfItem(False) = True Then Exit Sub
                EndOfItem(False)
                Exit Sub
            End If
            If Not GTCIndex = -1 Then
                categoryIndex(GTCIndex) = False
                GTCIndex = -1
            End If
            PerformModifierLoop(objButton.FoodID)

            '          PutUsInNormalMode()
        End If

    End Sub

    Private Function CheckForSecondaryModifierLoop(ByVal foodItem As Integer)

        ' this is for modifiers to modifiers
        'ex: dressing for a salad, that the salad came with the steak

        Dim vRow As DataRowView
        Dim i As Integer

        If currentTable.IsPrimaryMenu = True Then
            With dvIndividualJoinGroup
                .Table = ds.Tables("IndividualJoinMain")
                .RowFilter = "FoodID = '" & foodItem & "'"
            End With
        Else
            With dvIndividualJoinGroup
                .Table = ds.Tables("IndividualJoinSecondary")
                .RowFilter = "FoodID = '" & foodItem & "'"
            End With
        End If

        With dvCategoryJoinSecondLoop
            .Table = ds.Tables("CategoryJoin")
            .RowFilter = "FoodID = '" & foodItem & "'"
            .Sort = "Priority, FreeFlag"
        End With

        ReDim categoryIndexSecondLoop(dvCategoryJoinSecondLoop.Count)
        For Each vRow In dvCategoryJoinSecondLoop
            categoryIndexSecondLoop(i) = True
            i += 1
        Next

        If dvIndividualJoinGroup.Count + dvCategoryJoinSecondLoop.Count > 0 Then
            Return True
        Else
            '   currentTable.si2 = currentTable.Tempsi2
            isSecondLoop = False
            Return False
        End If


    End Function


    Private Sub PerformSecondModifierLoop(ByVal foodItem As Integer)
        Dim index As Integer
        Dim vRow As DataRowView
        Dim cRow As DataRowView
        Dim i As Integer
        Dim c As Integer
        Dim tableType As String
        Dim currentModifierID As Integer
        Dim oRow As DataRow


        If dvIndividualJoinGroup.Count > 0 Then
            Do Until modifierIndexSecondLoop > cntModifierLoop - 1

                If currentTable.IsPrimaryMenu = True Then
                    currentModifierID = mainCategoryIDArrayList(modifierIndexSecondLoop)
                    '          dvIndividualJoinAuto = New DataView
                    With dvIndividualJoinAuto
                        .Table = ds.Tables("IndividualJoinMain")
                        .RowFilter = "FoodID = '" & foodItem & "' and CategoryID = '" & currentModifierID & "' and GroupFlag = 'A'"
                    End With

                    '         dvIndividualJoinGroup = New DataView
                    With dvIndividualJoinGroup
                        .Table = ds.Tables("IndividualJoinMain")
                        .RowFilter = "FoodID = '" & foodItem & "'and CategoryID = '" & currentModifierID & "' and GroupFlag = 'G'"
                    End With
                Else
                    currentModifierID = secondaryCategoryIDArrayList(modifierIndexSecondLoop)
                    '         dvIndividualJoinAuto = New DataView
                    With dvIndividualJoinAuto
                        .Table = ds.Tables("IndividualJoinSecondary")
                        .RowFilter = "FoodID = '" & foodItem & "' and CategoryID = '" & currentModifierID & "' and GroupFlag = 'A'"
                    End With

                    '     dvIndividualJoinGroup = New DataView
                    With dvIndividualJoinGroup
                        .Table = ds.Tables("IndividualJoinSecondary")
                        .RowFilter = "FoodID = '" & foodItem & "'and CategoryID = '" & currentModifierID & "' and GroupFlag = 'G'"
                    End With
                End If

                If dvIndividualJoinAuto.Count > 0 And currentTable.SecondRound = False Then
                    '                Exit Do
                    For Each vRow In dvIndividualJoinAuto
                        Dim currentmodifier As SelectedItemDetail = New SelectedItemDetail
                        Dim populatingTable As String
                        Dim catID As Integer
                        Dim funFlag As String
                        funFlag = vRow("FunctionFlag")
                        catID = vRow("CategoryID")
                        'this is NOT needed now
                        'maybe if sometime we want to have an automatic Inv Multiplier added to item
                        currentTable.InvMultiplier *= vRow("InvMultiplier")

                        If currentTable.IsPrimaryMenu = True Then
                            populatingTable = "MainTable"
                        Else
                            populatingTable = "SecondaryMainTable"
                        End If

                        With currentmodifier
                            .ID = vRow("ModifierID")
                            '   may get rid of .Name (duplicate?)
                            .Name = "   " & vRow("AbrevFoodName")
                            .TerminalName = "   " & vRow("AbrevFoodName")
                            .ChitName = "   " & vRow("ChitFoodName")
                            '   added spaces here b/c it goes directly to datagrid
                            If vRow("FreeFlag") = "F" And currentTable.SecondRound = False Then 'Or dvIndividualJoin(0)("Surcharge") Is DBNull.Value Then
                                .Price = 0
                            Else
                                If Not vRow("Surcharge") Is DBNull.Value Then
                                    .Price = vRow("Surcharge") * currentTable.Quantity
                                Else
                                    .Price = 0
                                End If
                            End If
                            .FunctionID = vRow("FunctionID")
                            .FunctionGroup = vRow("FunctionGroupID")
                            .FunctionFlag = vRow("FunctionFlag")
                            .Category = vRow("CategoryID")

                            If currentTable.IsTabNotTable = False Then
                                .Table = currentTable.TableNumber
                            Else
                                .Tab = currentTable.TabID
                            End If
                            .Check = currentTable.CheckNumber
                            .Customer = currentTable.CustomerNumber
                            .Course = currentTable.CourseNumber
                            .Quantity = currentTable.Quantity
                            .InvMultiplier = currentTable.InvMultiplier
                            .SII = currentTable.ReferenceSIN
                            .SIN = currentTable.SIN
                            .si2 = currentTable.si2
                        End With
                        currentTable.SIN += 1
                        AddItemToOrderTable(currentmodifier)
                        i += 1
                    Next
                End If

                If dvIndividualJoinGroup.Count > 0 Then
                    If dvIndividualJoinGroup.Count > 23 Then ReDim freeFood(dvIndividualJoinGroup.Count)
                    '        If currentTable.IsPizza = False  Then
                    '   End If
                    freeFoodActive = True
                    For Each vRow In dvIndividualJoinGroup

                        If vRow("FreeFlag") = "F" And currentTable.SecondRound = False Then      'has NO association to function Flag
                            freeFood(index) = True
                        Else
                            freeFood(index) = False
                        End If
                        index += 1
                        '        numberFree(index) = dvIndividualJoin(0)("NumberFree")    'uses the first record only
                    Next
                    Exit Do
                End If

                modifierIndexSecondLoop += 1
            Loop

            If dvIndividualJoinGroup.Count > 0 Then
                SelectModifier(foodItem, dvIndividualJoinGroup, 0, "Modifier", 0, False)
                modifierIndexSecondLoop += 1
                '***         dvIndividualJoinGroup = New DataView   'resets dv for next modifierIndexSecondLoop value
                freeFoodActive = False
                Exit Sub
            End If

            If dvIndividualJoinAuto.Count > 0 Then
                '***         dvIndividualJoinAuto = New DataView
                modifierIndexSecondLoop += 1
                PerformModifierLoop(foodItem)
                Exit Sub 'this is so it won't keep looping
            End If
        End If


        '************************
        '       Category Loop

        i = 0
        For Each vRow In dvCategoryJoinSecondLoop

            If categoryIndexSecondLoop(i) = True Then
                '         numberFree(i) = dvCategoryJoin(i)("NumberFree")
                '***        dvCategoryModifiersSecondLoop = New DataView

                If dvCategoryJoinSecondLoop(i)("FunctionFlag") = "F" Or dvCategoryJoinSecondLoop(i)("FunctionFlag") = "O" Then

                    If currentTable.IsPrimaryMenu = True Then
                        '   *****    there is a problem if Category is not on Menu      ********
                        If ds.Tables("MainTable" & dvCategoryJoinSecondLoop(i)("CategoryID")).rows.count > 0 Then
                            With dvCategoryModifiersSecondLoop
                                .Table = ds.Tables("MainTable" & dvCategoryJoinSecondLoop(i)("CategoryID"))
                                .Sort = "MenuIndex"
                                '*** currently Foods do not have order when they are attached modifiers
                            End With
                        End If
                    Else
                        '   *****    there is a problem if Category is not on Menu      ********
                        If ds.Tables("SecondaryMainTable" & dvCategoryJoinSecondLoop(i)("CategoryID")).rows.count > 0 Then
                            With dvCategoryModifiersSecondLoop
                                .Table = ds.Tables("SecondaryMainTable" & dvCategoryJoinSecondLoop(i)("CategoryID"))
                                .Sort = "MenuIndex"
                                '*** currently Foods do not have order when they are attached modifiers
                            End With
                        End If
                    End If

                Else

                    With dvCategoryModifiersSecondLoop
                        .Table = ds.Tables("ModifierTable" & dvCategoryJoinSecondLoop(i)("CategoryID"))
                        '   *** do we want to filter out MenuIndex =0 ????
                        '       .RowFilter = "MenuIndex > 0"
                        .Sort = "MenuIndex"
                    End With

                End If

                If dvCategoryJoin.Count > 0 Then
                    If dvCategoryJoin(i)("FreeFlag") = "F" And currentTable.SecondRound = False Then
                        c = 0
                        For Each cRow In dvCategoryModifiersSecondLoop
                            freeFood(c) = True
                            c += 1
                        Next
                        freeFoodActive = True
                    Else
                        freeFoodActive = False
                    End If
                Else

                End If

                If dvCategoryModifiersSecondLoop.Count > 0 Then
                    SelectModifier(foodItem, dvCategoryModifiersSecondLoop, 0, "Food", 0, dvCategoryJoin(i)("Extended")) 'dvCategoryModifiersSecondLoop(0)("Extended"))
                End If
                If Not dvCategoryJoinSecondLoop(i)("GTCFlag") = "C" Then
                    categoryIndexSecondLoop(i) = False
                Else
                    GTCIndex = i
                End If
                Exit Sub
            End If
            i += 1
        Next


        Exit Sub
        '222 none of this nneded for second loop

        ' *****  this is the end of the current item after modifiers
        currentTable.ReferenceSIN = currentTable.SIN
        GTCIndex = -1
        freeFoodActive = False

        'this one below removes the highlight of last hit grid cell
        'but it will add items in the begining (if hit ADD button)
        '   EndOfItem()
        currentTable.MiddleOfOrder = False
        currentTable.si2 = 0
        currentTable.Tempsi2 = 0
        currentTable.IsPizza = False
        '     Me.pnlPizzaSplit.Visible = False
        pnlOrderModifier.Visible = False
        pnlOrderModifierExt.Visible = False
        If currentTable.IsExtended = True Then
            pnlOrderQuick.Visible = True
        Else
            pnlOrder.Visible = True
        End If
        '      pnlDescription.Visible = False
        If Not currentTable.Quantity = 1 Then
            currentTable.Quantity = 1
            Me.testgridview.ChangeCourseButton(currentTable.Quantity)
        End If
        '    PutUsInNormalMode()
        If currentTable.MarkForNextCustomer = True Then
            currentTable.CustomerNumber = currentTable.NextCustomerNumber
            ChangeCustomerButtonColor(c9)
            If currentTable.MarkForNewCustomerPanel = True Then
                AddCustomerPanel()
            End If
            currentTable.MarkForNewCustomerPanel = False
            currentTable.MarkForNextCustomer = False
        End If
    End Sub

    Private Sub SelectModifier(ByVal foodItem As Integer, ByRef dv As DataView, ByVal modButtonIndex As Integer, ByVal tableType As String, ByVal modMenuIndex As Integer, ByVal modIsExtended As Boolean)
        Dim index As Integer
        Dim secondindex As Integer
        Dim vRow As DataRowView
        Dim mbi = modMenuIndex  ' modButtonIndex
        Dim n As Integer = 0
        Dim lastButton As Integer

        Dim populatingTable As String
        Dim catID As Integer
        Dim funFlag As String

        Try
            funFlag = dv(0)("FunctionFlag")   'dv(mbi)("FunctionFlag")
            catID = dv(0)("CategoryID")   'dv(mbi)("CategoryID")

        Catch ex As Exception
            MsgBox("There is a problem with your Menu Setup. Please verify your Menu Joins.")
            '     modifierIndex += 1
            '    PerformModifierLoop(foodItem)
            Exit Sub
        End Try

        If currentTable.IsPrimaryMenu = True Then
            populatingTable = "MainTable"
        Else
            populatingTable = "SecondaryMainTable"
        End If

        If modIsExtended = True Then
            lastButton = 59

            For n = 0 To lastButton
                ' clear button 
                btnOrderModifierExt(n).Text = Nothing
                btnOrderModifierExt(n).ID = 0
                btnOrderModifierExt(n).BackColor = c13   'c8
            Next

            If mbi = 0 Then
                index = 0
            Else
                index = 1
                btnOrderModifierExt(0).Text = "Previous"
                btnOrderModifierExt(0).ID = -1
                btnOrderModifierExt(0).FoodID = foodItem
                btnOrderModifierExt(0).ActiveDataView = dv
                btnOrderModifierExt(0).BackColor = c4
                btnOrderModifierExt(0).ForeColor = c3
                btnOrderModifierExt(0).Extended = modIsExtended
            End If

            For Each vRow In dv

                If mbi >= dv.Count Then Exit For

                If dv(mbi)("MenuIndex") Is DBNull.Value Then
                    ' I don't think we will ever get here
                    'we should get info from:   Menu Class  -  tableCreating = "IndividualJoinMain"
                    'this is for food items that were not put on a menu
                    'like sides (that are listed as food items)
                    'this only works for a limited amount
                    index = mbi
                Else
                    If modButtonIndex < dv(mbi)("MenuIndex") Then
                        index = dv(mbi)("MenuIndex") - modButtonIndex
                    Else
                        index = dv(mbi)("MenuIndex")
                    End If
                End If


                If index > 0 Then
                    index -= 1  'b/c the buttons are Zero based

                    If index >= lastButton Then Exit For 'this gives the first 24 records (0-23) w/ last button at 23

                    '   changed both from modifiername to foodname
                    btnOrderModifierExt(index).Text = dv(mbi)("AbrevFoodName")  '(tableType & "Name")
                    btnOrderModifierExt(index).Name = dv(mbi)("AbrevFoodName")  '(tableType & "Name")
                    '     btnOrderModifierExt(index).ch = dv(mbi)("ChitFoodName")  '(tableType & "Name")

                    '   the above two can come from 2 diff places (abrev)
                    btnOrderModifierExt(index).ID = dv(mbi)(tableType & "ID")  '("FoodID")    '

                    '   need to do this by index if I can populate by index (in Category Loop 6 leaps above)
                    If freeFood(mbi) = True And freeFoodActive = True Then    'numberFree(0) > 0 Then   'Or dv(mbi)("Surcharge") Is DBNull.Value Then
                        btnOrderModifierExt(index).Cost = 0
                        '          numberFree(index) -= 1
                    Else
                        If Not dv(mbi)("Surcharge") Is DBNull.Value Then
                            btnOrderModifierExt(index).Cost = dv(mbi)("Surcharge") '(tableType & "Cost")
                        Else
                            btnOrderModifierExt(index).Cost = 0
                        End If
                    End If

                    btnOrderModifierExt(index).CategoryID = dv(mbi)("CategoryID")
                    btnOrderModifierExt(index).Functions = dv(mbi)("FunctionID")
                    btnOrderModifierExt(index).FunctionGroup = dv(mbi)("FunctionGroupID")
                    btnOrderModifierExt(index).FunctionFlag = dv(mbi)("FunctionFlag")
                    btnOrderModifierExt(index).InvMultiplier = dv(mbi)("InvMultiplier")
                    btnOrderModifierExt(index).FoodID = foodItem  '    dv(mbi)("FoodID")

                    If Not dv(mbi)("ButtonColor") Is DBNull.Value Then
                        btnOrderModifierExt(index).BackColor = Color.FromArgb(dv(mbi)("ButtonColor"))
                        btnOrderModifierExt(index).ForeColor = Color.FromArgb(dv(mbi)("ButtonForeColor"))
                    Else
                        ' only this was here last
                        btnOrderModifierExt(index).BackColor = c4
                        btnOrderModifierExt(index).ForeColor = c3
                    End If

                Else
                    'should never get here, therefore we skip this item
                    'this is now for the item in staging, index = 0 
                    index = 0
                End If
                '     index += 1
                mbi += 1
                '    If index = 23 Then Exit For 'this gives the first 15 records (0-14)
            Next

            If index >= lastButton Then
                If index = lastButton And (mbi + 1) = dv.Count Then  'dv.Count = modButtonIndex + 24 Then
                    '   this is the 16th record (15)
                    btnOrderModifierExt(index).Text = dv(mbi)(tableType & "Name")
                    btnOrderModifierExt(index).Name = dv(mbi)(tableType & "Name")
                    btnOrderModifierExt(index).ID = dv(mbi)(tableType & "ID")
                    If (freeFood(mbi) = True And freeFoodActive = True) Or dv(mbi)("Surcharge") Is DBNull.Value Then      'numberFree(index) > 0
                        btnOrderModifierExt(index).Cost = 0
                        '       numberFree(index) -= 1
                    Else
                        btnOrderModifierExt(index).Cost = dv(mbi)("Surcharge") '(tableType & "Cost")
                    End If
                    btnOrderModifierExt(index).CategoryID = dv(mbi)("CategoryID")
                    btnOrderModifierExt(index).Functions = dv(mbi)("FunctionID")
                    btnOrderModifierExt(index).FunctionGroup = dv(mbi)("FunctionGroupID")
                    btnOrderModifierExt(index).FunctionFlag = dv(mbi)("FunctionFlag")
                    btnOrderModifierExt(index).InvMultiplier = dv(mbi)("InvMultiplier")
                    btnOrderModifierExt(index).FoodID = foodItem
                Else  'elseIf dv.Count > modButtonIndex + 25 Then
                    btnOrderModifierExt(lastButton).Text = "More"
                    btnOrderModifierExt(lastButton).ID = -2
                    btnOrderModifierExt(lastButton).FoodID = foodItem
                    btnOrderModifierExt(lastButton).ActiveDataView = dv
                    btnOrderModifierExt(lastButton).Extended = modIsExtended
                    If btnOrderModifierExt(lastButton).ModifierButtonIndex = 0 Then
                        btnOrderModifierExt(lastButton).ModifierButtonIndex = modButtonIndex + lastButton  '24
                        btnOrderModifierExt(lastButton).ModifierMenuIndex += mbi  'modButtonIndex + 23  '24
                    Else
                        btnOrderModifierExt(lastButton).ModifierButtonIndex = modButtonIndex + (lastButton - 1) '23
                        btnOrderModifierExt(lastButton).ModifierMenuIndex += mbi  'modButtonIndex + 23  '24
                    End If
                End If
                If Not dv(mbi)("ButtonColor") Is DBNull.Value Then
                    btnOrderModifierExt(index).BackColor = Color.FromArgb(dv(mbi)("ButtonColor"))
                    btnOrderModifierExt(index).ForeColor = Color.FromArgb(dv(mbi)("ButtonForeColor"))
                Else
                    ' only this was here last
                    btnOrderModifierExt(index).BackColor = c4
                    btnOrderModifierExt(index).ForeColor = c3
                End If
             
                If btnOrderModifierExt(lastButton).ModifierButtonIndex > 0 Then btnOrderModifierExt(0).ModifierButtonIndex = btnOrderModifierExt(lastButton).ModifierButtonIndex
            End If

            If pnlOrderModifierExt.Visible = False Then
                pnlOrderModifierExt.Visible = True
                pnlOrderModifier.Visible = False

            End If
        Else
            lastButton = 23

            For n = 0 To lastButton
                ' clear button 
                btnOrderModifier(n).Text = Nothing
                btnOrderModifier(n).ID = 0
                btnOrderModifier(n).BackColor = c13   'c8
            Next

            If mbi = 0 Then
                index = 0
            Else
                index = 1
                btnOrderModifier(0).Text = "Previous"
                btnOrderModifier(0).ID = -1
                btnOrderModifier(0).FoodID = foodItem
                btnOrderModifier(0).ActiveDataView = dv
                btnOrderModifier(0).BackColor = c4
                btnOrderModifier(0).ForeColor = c3
                btnOrderModifier(0).Extended = modIsExtended
            End If

            For Each vRow In dv

                If mbi >= dv.Count Then Exit For

                If dv(mbi)("MenuIndex") Is DBNull.Value Then
                    '999
                    ' this is for indivual joins that display as a group
                    '   index = mbi
                    If Not mbi = 0 Then
                        ' do not advance the first button (o based)
                        index += 1
                    End If

                Else
                    If modButtonIndex < dv(mbi)("MenuIndex") Then
                        index = dv(mbi)("MenuIndex") - modButtonIndex - 1
                    Else
                        index = dv(mbi)("MenuIndex") - 1
                    End If
                End If


                If index > -1 Then
                    '999 index -= 1  'b/c the buttons are Zero based

                    If index >= lastButton Then Exit For 'this gives the first 24 records (0-23) w/ last button at 23

                    '   changed both from modifiername to foodname
                    btnOrderModifier(index).Text = dv(mbi)("AbrevFoodName")  '(tableType & "Name")
                    btnOrderModifier(index).Name = dv(mbi)("AbrevFoodName")  '(tableType & "Name")
                    '     btnOrderModifier(index).ch = dv(mbi)("ChitFoodName")  '(tableType & "Name")

                    '   the above two can come from 2 diff places (abrev)
                    btnOrderModifier(index).ID = dv(mbi)(tableType & "ID")  '("FoodID")    '

                    '   need to do this by index if I can populate by index (in Category Loop 6 leaps above)
                    If freeFood(mbi) = True And freeFoodActive = True Then    'numberFree(0) > 0 Then   'Or dv(mbi)("Surcharge") Is DBNull.Value Then
                        btnOrderModifier(index).Cost = 0
                        '          numberFree(index) -= 1
                    Else
                        If Not dv(mbi)("Surcharge") Is DBNull.Value Then
                            btnOrderModifier(index).Cost = dv(mbi)("Surcharge") '(tableType & "Cost")
                        Else
                            btnOrderModifier(index).Cost = 0
                        End If
                    End If

                    btnOrderModifier(index).CategoryID = dv(mbi)("CategoryID")
                    btnOrderModifier(index).Functions = dv(mbi)("FunctionID")
                    btnOrderModifier(index).FunctionGroup = dv(mbi)("FunctionGroupID")
                    btnOrderModifier(index).FunctionFlag = dv(mbi)("FunctionFlag")
                    btnOrderModifier(index).InvMultiplier = dv(mbi)("InvMultiplier")
                    btnOrderModifier(index).FoodID = foodItem  '    dv(mbi)("FoodID")
                    If Not dv(mbi)("ButtonColor") Is DBNull.Value Then
                        btnOrderModifier(index).BackColor = Color.FromArgb(dv(mbi)("ButtonColor"))
                        btnOrderModifier(index).ForeColor = Color.FromArgb(dv(mbi)("ButtonForeColor"))
                    Else
                        ' only this was here last
                        btnOrderModifier(index).BackColor = c4
                        btnOrderModifier(index).ForeColor = c3
                    End If

                Else
                    'should never get here, therefore we skip this item
                    'this is now for the item in staging, index = 0 
                    index = 0
                End If
                '     index += 1
                mbi += 1
                '    If index = 23 Then Exit For 'this gives the first 15 records (0-14)

            Next

            If index >= lastButton Then
                If index = lastButton And (mbi + 1) = dv.Count Then  'dv.Count = modButtonIndex + 24 Then
                    '   this is the 16th record (15)
                    btnOrderModifier(index).Text = dv(mbi)(tableType & "Name")
                    btnOrderModifier(index).Name = dv(mbi)(tableType & "Name")
                    btnOrderModifier(index).ID = dv(mbi)(tableType & "ID")
                    If (freeFood(mbi) = True And freeFoodActive = True) Or dv(mbi)("Surcharge") Is DBNull.Value Then      'numberFree(index) > 0
                        btnOrderModifier(index).Cost = 0
                        '       numberFree(index) -= 1
                    Else
                        btnOrderModifier(index).Cost = dv(mbi)("Surcharge") '(tableType & "Cost")
                    End If
                    btnOrderModifier(index).CategoryID = dv(mbi)("CategoryID")
                    btnOrderModifier(index).Functions = dv(mbi)("FunctionID")
                    btnOrderModifier(index).FunctionGroup = dv(mbi)("FunctionGroupID")
                    btnOrderModifier(index).FunctionFlag = dv(mbi)("FunctionFlag")
                    btnOrderModifier(index).InvMultiplier = dv(mbi)("InvMultiplier")
                    btnOrderModifier(index).FoodID = foodItem
                Else  'elseIf dv.Count > modButtonIndex + 25 Then
                    btnOrderModifier(lastButton).Text = "More"
                    btnOrderModifier(lastButton).ID = -2
                    btnOrderModifier(lastButton).FoodID = foodItem
                    btnOrderModifier(lastButton).ActiveDataView = dv
                    btnOrderModifier(lastButton).Extended = modIsExtended
                    If btnOrderModifier(lastButton).ModifierButtonIndex = 0 Then
                        btnOrderModifier(lastButton).ModifierButtonIndex = modButtonIndex + lastButton  '24
                        btnOrderModifier(lastButton).ModifierMenuIndex += mbi  'modButtonIndex + 23  '24
                    Else
                        btnOrderModifier(lastButton).ModifierButtonIndex = modButtonIndex + (lastButton - 1) '23
                        btnOrderModifier(lastButton).ModifierMenuIndex += mbi  'modButtonIndex + 23  '24
                    End If
                End If
                If Not dv(mbi)("ButtonColor") Is DBNull.Value Then
                    btnOrderModifier(index).BackColor = Color.FromArgb(dv(mbi)("ButtonColor"))
                    btnOrderModifier(index).ForeColor = Color.FromArgb(dv(mbi)("ButtonForeColor"))
                Else
                    ' only this was here last
                    btnOrderModifier(index).BackColor = c4
                    btnOrderModifier(index).ForeColor = c3
                End If
              
                If btnOrderModifier(lastButton).ModifierButtonIndex > 0 Then btnOrderModifier(0).ModifierButtonIndex = btnOrderModifier(lastButton).ModifierButtonIndex
            End If

            If pnlOrderModifier.Visible = False Then
                pnlOrderModifier.Visible = True
                pnlOrderModifierExt.Visible = False

            End If
        End If



        '       btnOrderModifierCancel.Text = "NO " & catName  'we need to pull catName to do this
        btnOrderModifierCancel.FoodID = foodItem

    End Sub

    Private Sub SelectModifier222(ByVal foodItem As Integer, ByRef dv As DataView, ByVal modButtonIndex As Integer, ByVal tableType As String)
        Dim index As Integer
        Dim secondIndex As Integer
        Dim vRow As DataRowView
        Dim mbi = modButtonIndex

        Dim populatingTable As String
        Dim catID As Integer
        Dim funFlag As String

        Try
            funFlag = dv(mbi)("FunctionFlag")
            catID = dv(mbi)("CategoryID")
        Catch ex As Exception
            MsgBox(ex.Message)
            modifierIndex += 1
            PerformModifierLoop(foodItem)
            Exit Sub
        End Try

        If currentTable.IsPrimaryMenu = True Then
            populatingTable = "MainTable"
        Else
            populatingTable = "SecondaryMainTable"
        End If
        '    If funFlag = "F" Then
        '       dvSurcharge = New DataView(ds.Tables(populatingTable & catID), "FoodID ='" & foodItem & "'", "FoodID", DataViewRowState.CurrentRows)
        '      Else
        '
        '       End If
        '       dvSurcharge = New DataView

        If mbi = 0 Then
            index = 0
        Else
            index = 1
            btnOrderModifier(0).Text = "Previous"
            btnOrderModifier(0).ID = -1
            btnOrderModifier(0).FoodID = foodItem
            btnOrderModifier(0).ActiveDataView = dv
            btnOrderModifier(0).BackColor = c4
            btnOrderModifier(0).ForeColor = c3
        End If
        For Each vRow In dv

            If mbi >= dv.Count Then Exit For

            '   changed both from modifiername to foodname
            btnOrderModifier(index).Text = dv(mbi)("AbrevFoodName")  '(tableType & "Name")
            btnOrderModifier(index).Name = dv(mbi)("AbrevFoodName")  '(tableType & "Name")
            '     btnOrderModifier(index).ch = dv(mbi)("ChitFoodName")  '(tableType & "Name")

            '   the above two can come from 2 diff places (abrev)
            btnOrderModifier(index).ID = dv(mbi)(tableType & "ID")  '("FoodID")    '

            '   need to do this by index if I can populate by index (in Category Loop 6 leaps above)
            If freeFood(mbi) = True And freeFoodActive = True Then    'numberFree(0) > 0 Then   'Or dv(mbi)("Surcharge") Is DBNull.Value Then
                btnOrderModifier(index).Cost = 0
                '          numberFree(index) -= 1
            Else
                '              If dvSurcharge.Count > 0 Then
                '             If Not dvSurcharge(0)("Surcharge") Is DBNull.Value Then
                '                btnOrderModifier(index).Cost = dvSurcharge(0)("Surcharge")
                '           Else
                '              btnOrderModifier(index).Cost = 0
                '         End If
                '        Else


                If Not dv(mbi)("Surcharge") Is DBNull.Value Then
                    btnOrderModifier(index).Cost = dv(mbi)("Surcharge") '(tableType & "Cost")
                Else
                    btnOrderModifier(index).Cost = 0
                End If

                '       End If

            End If

            btnOrderModifier(index).CategoryID = dv(mbi)("CategoryID")
            btnOrderModifier(index).Functions = dv(mbi)("FunctionID")
            btnOrderModifier(index).FunctionGroup = dv(mbi)("FunctionGroupID")
            btnOrderModifier(index).FunctionFlag = dv(mbi)("FunctionFlag")
            btnOrderModifier(index).FoodID = foodItem  '    dv(mbi)("FoodID")
            btnOrderModifier(index).BackColor = c4
            btnOrderModifier(index).ForeColor = c3
            index += 1
            mbi += 1
            If index = 23 Then Exit For 'this gives the first 15 records (0-14)
        Next

        If index = 23 Then
            If dv.Count = modButtonIndex + 24 Then
                '   this is the 16th record (15)
                btnOrderModifier(index).Text = dv(mbi)(tableType & "Name")
                btnOrderModifier(index).Name = dv(mbi)(tableType & "Name")
                btnOrderModifier(index).ID = dv(mbi)(tableType & "ID")
                If (freeFood(mbi) = True And freeFoodActive = True) Or dv(mbi)("Surcharge") Is DBNull.Value Then      'numberFree(index) > 0
                    btnOrderModifier(index).Cost = 0
                    '       numberFree(index) -= 1
                Else
                    btnOrderModifier(index).Cost = dv(mbi)("Surcharge") '(tableType & "Cost")
                End If
                btnOrderModifier(index).CategoryID = dv(mbi)("CategoryID")
                btnOrderModifier(index).Functions = dv(mbi)("FunctionID")
                btnOrderModifier(index).FunctionGroup = dv(mbi)("FunctionGroupID")
                btnOrderModifier(index).FunctionFlag = dv(mbi)("FunctionFlag")
                btnOrderModifier(index).FoodID = foodItem
            ElseIf dv.Count > modButtonIndex + 25 Then
                btnOrderModifier(index).Text = "More"
                btnOrderModifier(index).ID = -2
                btnOrderModifier(index).FoodID = foodItem
                btnOrderModifier(index).ActiveDataView = dv
                If btnOrderModifier(index).ModifierButtonIndex = 0 Then
                    btnOrderModifier(index).ModifierButtonIndex = modButtonIndex + 24
                Else
                    btnOrderModifier(index).ModifierButtonIndex = modButtonIndex + 23
                End If
            End If
            btnOrderModifier(index).BackColor = c4
            btnOrderModifier(index).ForeColor = c3
            If btnOrderModifier(index).ModifierButtonIndex > 0 Then btnOrderModifier(0).ModifierButtonIndex = btnOrderModifier(24).ModifierButtonIndex
        End If

        If index < 23 Then
            For secondIndex = index To 23
                btnOrderModifier(secondIndex).Text = Nothing
                btnOrderModifier(secondIndex).ID = 0
                btnOrderModifier(secondIndex).BackColor = c13   'c8
            Next
        End If

        If pnlOrderModifier.Visible = False Then
            pnlOrderModifier.Visible = True
            '**************   pnlOrderModifierExt.Visible = False

        End If

        '       btnOrderModifierCancel.Text = "NO " & catName  'we need to pull catName to do this
        btnOrderModifierCancel.FoodID = foodItem

    End Sub

    Private Sub CancelDrinkAdds() Handles drinkPrep.Cancel

        UserControlHit()
        drinkPrep.Visible = False

        If EndOfItem(True) = False Then Exit Sub

    End Sub

    Private Sub DrinkPrepOrdered(ByVal currentItem As SelectedItemDetail) Handles drinkPrep.AcceptPrep
        UserControlHit()

        If currentItem.dpMethod = "percent" Then
            Dim oRow As DataRow
            Dim rowNum As Integer = OpenOrdersCurrencyMan.Position
            Dim valueSIN As Integer
            Dim valueSII As Integer
            Dim originalItemPrice As Decimal
            valueSIN = CType(Me.testgridview.gridViewOrder.Item(rowNum, 1), Integer)
            valueSII = CType(Me.testgridview.gridViewOrder.Item(rowNum, 2), Integer)
            For Each oRow In dsOrder.Tables("OpenOrders").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("sin") = valueSII Then
                        originalItemPrice = oRow("Price")
                    End If
                End If
            Next

            If currentItem.Price < 1 Then
                ' this means customer entered Ex: 0.50 for 50%
                currentItem.ItemPrice = currentItem.Price * originalItemPrice
                currentItem.Price = currentItem.Price * originalItemPrice
            Else
                'this means customer entered Ex: 50 for 50%
                currentItem.ItemPrice = (currentItem.Price / 100) * originalItemPrice
                currentItem.Price = (currentItem.Price / 100) * originalItemPrice
            End If
        End If

        With currentItem
            .Check = currentTable.CheckNumber
            .Customer = currentTable.CustomerNumber
            .Course = currentTable.CourseNumber
            .SIN = currentTable.SIN
            .SII = currentTable.ReferenceSIN
            .si2 = currentTable.si2
            .PrintPriorityID = 1
        End With

        '   GenerateOrderTables.DetermineFunctionAndTaxInfo(currentItem, currentItem.FunctionGroup, True)
        Dim functionRow As DataRow

        For Each functionRow In dsOrder.Tables("Functions").Rows
            If functionRow("FunctionID") = currentItem.FunctionID Then
                With currentItem
                    '  .FunctionID = functionRow("FunctionID")
                    .TaxID = functionRow("TaxID")
                End With
                Exit For
            End If
        Next

        currentTable.SIN += 1
        AddItemToOrderTable(currentItem)
        Me.testgridview.CalculateSubTotal()

    End Sub


    Private Sub SelectDrinkAdds(ByVal drinkID As Integer, ByVal catID As Integer, ByVal routeID As Integer, ByVal funID As Integer, ByVal funGroupID As Integer)

        '     If companyInfo.servesMixedDrinks = False Then
        ' we can always do now, b/c it wont show if no preps
        '    Exit Sub
        '   End If

        Me.drinkPrep.StartDrinkPrep(drinkID, catID, routeID, funID, funGroupID)
        pnlOrderDrink.Visible = False
        pnlOrderModifier.Visible = False
        pnlOrderModifierExt.Visible = False
        pnlOrder.Visible = False
        pnlOrderQuick.Visible = False
        drinkPrep.Visible = True
        currentTable.MiddleOfOrder = True
        '       Me.DisableControls()

        Exit Sub

        Dim index As Integer
        Dim secondIndex As Integer
        Dim oRow As DataRow

        For Each oRow In dtDrinkAdds.Rows
            btnOrder(index).Text = dtDrinkAdds.Rows(index)("DrinkName")
            btnOrder(index).ID = dtDrinkAdds.Rows(index)("DrinkID")
            '       btnOrder(index).UpdateText(btnOrder(index).Text)
            btnOrder(index).CategoryID = catID
            btnOrder(index).SubCategory = True
            btnOrder(index).Functions = dtDrinkAdds.Rows(index)("DrinkFunctionID")
            btnOrder(index).FunctionGroup = dtDrinkAdds.Rows(index)("FunctionGroupID")
            btnOrder(index).FunctionFlag = dtDrinkAdds.Rows(index)("FunctionFlag")
            btnOrder(index).DrinkAdds = True
            btnOrder(index).BackColor = c4
            btnOrder(index).ForeColor = c3
            'old        btnOrder(index).InvMultiplier = dtDrinkAdds.Rows(index)("InvMultiplier")
            index += 1
            If index = 32 Then Exit For
        Next

        If index < 32 Then
            For secondIndex = index To 31
                btnOrder(secondIndex).Text = Nothing
                btnOrder(secondIndex).ID = 0
                btnOrder(secondIndex).BackColor = c13
                '          btnOrder(secondIndex).UpdateText(btnOrder(index).Text)
            Next
        End If

        pnlOrderDrink.Visible = False
        pnlOrderModifier.Visible = False
        pnlOrderModifierExt.Visible = False
        pnlOrder.Visible = False
        pnlOrderQuick.Visible = False
        drinkPrep.Visible = True
        currentTable.MiddleOfOrder = True

    End Sub

    Private Sub EventForAddingItem(ByVal sender As Object) Handles modifyOrderUserControl.AddingItemToOrder, extraNoUserControl.AddingItemToOrder
        Dim objItem As SelectedItemDetail

        objItem = CType(sender, SelectedItemDetail)

        '     currentTable.ReferenceSIN = currentTable.SIN
        '      currentTable.MiddleOfOrder = False
        currentTable.SIN += 1
        AddItemToOrderTable(sender)
        Me.testgridview.CalculateSubTotal()

        EnableControls()

    End Sub

    Private Sub AddItemToOrderTable(ByRef newItem As SelectedItemDetail)

        If currentTable.IsClosed = True Then
            '        info = New DataSet_Builder.Information_UC("Check is Closed")
            '       info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            '      Me.Controls.Add(info)
            Exit Sub
        End If

        If dvOrder.Count > 0 Then
            PopulateDataRowForOpenOrder(newItem)
            '        dsOrder.Tables("OpenOrders").Rows.Add(oRow)
        Else
            '444       DisposeDataViewsOrder()
            '444 this dispose was a big issue before 
            ' i don't remember why, i removed all over
            PopulateDataRowForOpenOrder(newItem)
            '444    CreateDataViewsOrder()
            '444        Me.testgridview.gridViewOrder.DataSource = dvOrder
        End If


        Dim i As Integer = 0
        Dim vRow As DataRowView
        '   not sure which one we are using ????????
        If newItem.SIN = newItem.SII Then
            For Each vRow In Me.testgridview.gridViewOrder.DataSource
                If vRow("sin") = newItem.SIN Then
                    Exit For
                End If
                i += 1
            Next
            OpenOrdersCurrencyMan.Position = i
            '   Me.testgridview.gridViewOrder.CurrentRowIndex = i
            '        OpenOrdersCurrencyMan.Position = Me.testgridview.gridViewOrder.DataSource.count - 1  'dsOrder.Tables("OpenOrders").Rows.Count - 1
            Me.testgridview.gridViewOrder.ScrollToRow(OpenOrdersCurrencyMan.Position)
        Else
            For Each vRow In Me.testgridview.gridViewOrder.DataSource
                If vRow("sin") = newItem.SIN Then
                    Exit For
                End If
                i += 1
            Next
            OpenOrdersCurrencyMan.Position = i
            '    Me.testgridview.gridViewOrder.CurrentRowIndex = i '+= 1
            Me.testgridview.gridViewOrder.ScrollToRow(OpenOrdersCurrencyMan.Position) ' + 1)
        End If

    End Sub

    Friend Sub UpdateTableInfo() ' Handles TabEnterScreen.ChangedMethodUse

        If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            btnTableInfoMenu.Text = "Menu:  " & currentTable.CurrentMenuName
            btnTableInfoServerNumber.Text = "Server:  " & currentServer.NickName
            If currentTable.IsTabNotTable = True Then
                If currentTable.TabID = -888 Then
                    btnTableInfoTableNumber.Text = "Ticket:  " & currentTable.TicketNumber
                    '   btnTableInfoTableNumber.Text = currentTable.TabName
                Else
                    btnTableInfoTableNumber.Text = "Tab:  " & currentTable.TabName
                End If
            Else
                btnTableInfoTableNumber.Text = "Table:  " & currentTable.TableNumber
            End If

            If currentTable.MethodUse = "Delivery" Then
                btnTableInfoNumberOfCustomers.Text = currentTable.MethodUse.ToString & ": " & currentTable.TabName
            Else
                If currentTable.TabID = -888 Then
                    'this is b/c earlier we displayed ticket # not tab name
                    btnTableInfoNumberOfCustomers.Text = currentTable.NumberOfCustomers & " " & currentTable.MethodUse.ToString & ": " & currentTable.TabName
                Else
                    btnTableInfoNumberOfCustomers.Text = currentTable.NumberOfCustomers & " " & currentTable.MethodUse.ToString '& ": " & currentTable.TabName
                End If
            End If
            '          If currentTable.MethodUse = "Take Out" Then
            '            btnTableInfoNumberOfCustomers.Text = "Take Out"
            '       ElseIf currentTable.MethodUse = "Dine In" Then
            '          btnTableInfoNumberOfCustomers.Text = currentTable.NumberOfCustomers & " " & "Guests: " & currentTable.TabName
            '     Else
            '        btnTableInfoNumberOfCustomers.Text = "Guests:  " & currentTable.NumberOfCustomers
            '   End If


        Else : currentTerminal.TermMethod = "Quick" 'Quick Service
            btnTableInfoMenu.Text = "Menu:  " & currentTable.CurrentMenuName
            btnTableInfoServerNumber.Text = "Cashier:  " & currentTable.EmployeeName
            btnTableInfoTableNumber.Text = "Ticket:  " & currentTable.TicketNumber

            If currentTable.MethodUse = "Delivery" Then
                btnTableInfoNumberOfCustomers.Text = currentTable.MethodUse.ToString & ": " & currentTable.TabName
            Else
                btnTableInfoNumberOfCustomers.Text = currentTable.NumberOfCustomers & " " & currentTable.MethodUse.ToString & ": " & currentTable.TabName
            End If

        End If

    End Sub

    Private Sub ButtonTableNumber_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTableInfoTableNumber.Click
        UserControlHit()
        '     PutUsInNormalMode()

        Exit Sub

        '222
        Select Case currentTerminal.TermMethod
            Case "Table"
                '   for Table Service
                '   we just change for this order...we can have someone order 1 item ToGo
                If currentTable.MethodUse = "Dine In" Then
                    currentTable.MethodUse = "Take Out"
                Else
                    currentTable.MethodUse = "Dine In"
                End If
                UpdateTableInfo()
            Case "Bar"
                '

                DisposingMethodScreen()

                If tabTimerActive = False Then

                    tabDoubleClickTimer = New Timer
                    tabTimerActive = True
                    AddHandler tabDoubleClickTimer.Tick, AddressOf TabTimerExpired
                    tabDoubleClickTimer.Interval = 500
                    tabDoubleClickTimer.Start()

                Else
                    'this means we just DOUBLE clicked
                    tabTimerActive = False
                    tabDoubleClickTimer.Dispose()

                    If currentTable.MethodUse = "Delivery" Then
                        If tabScreenDisplaying = False then'TabEnterScreen.isDisplaying = False Then 
                            MethodChanged()
                        Else
                            DetermineNextMethodRow()
                        End If
                    Else
                        MethodChanged()             ' not set
                    End If
                End If


                '   for Table Service
                '   we just change for this order...we can have someone order 1 item ToGo
                '          If currentTable.MethodUse = "Dine In" Then
                '         currentTable.MethodUse = "Take Out"
                '        Else
                '           currentTable.MethodUse = "Dine In"
                '      End If
                '     UpdateTableInfo()
            Case "Quick"
                'using guest button for changing quick service


        End Select

    End Sub

    Private Function DetermineNextMethodRow()

        ' **** we only get here in "Quick Service" ?????

        Dim vRow As DataRowView
        Dim count As Integer = 0
        Dim definingMethodUse As String

        If dvTerminalsUseOrder.Count <= 1 Then
            MethodChanged()
            Exit Function
        End If

        'i am trying to go right to tab history if already set
        'it does not work if on take out or dine in
        '       If TabEnterScreen Is Nothing Then
        '      ' so we won't get error below
        '     TabEnterScreen = New Tab_Screen("Phone")
        '    TabEnterScreen.isDisplaying = False
        '   TabEnterScreen.Dispose()
        '  End If

        '      If TabEnterScreen.isDisplaying = False And currentTable.TabID > 0 Then
        '     If currentTable.MethodUse = "Delivery" Then
        '    MethodChanged()
        '   Exit Function
        '  End If
        ' End If

        If currentTable.MethodUse = "Pickup" Then
            'we may have Pickup used also for delivery
            'someday we will need a check here
            definingMethodUse = "Take Out"
            wasPickupMethod = True
        Else
            definingMethodUse = currentTable.MethodUse
            
        End If

        For Each vRow In dvTerminalsUseOrder
            If vRow("MethodUse") = definingMethodUse Then '444 currentTable.MethodUse Then
                If count = dvTerminalsUseOrder.Count - 1 Then
                    count = 0
                    Exit For      ' telling us our next method is row zero
                Else
                    count += 1
                    Exit For
                End If
            End If
            count += 1
        Next
        If dvTerminalsUseOrder.Count > 0 And Not count >= dvTerminalsUseOrder.Count Then
            '                               this makes sure we have row in dataview
            currentTable.MethodUse = dvTerminalsUseOrder(count)("MethodUse")
            currentTable.MethodDirection = dvTerminalsUseOrder(count)("MethodDirection")
        Else
            currentTable.MethodUse = "Dine In"
            currentTable.MethodDirection = "None"
        End If
        If currentTable.MethodUse = "Take Out" Then
            If wasPickupMethod = True Then
                currentTable.MethodUse = "Pickup"
            End If
        End If

        UpdateTableInfo()
        GenerateOrderTables.UpdateMethodDataset()
        If currentTable.MethodUse = "Delivery" Then
            RaiseEvent TestForCurrentTabInfo()
            '          TabEnterScreen.TestForCurrentTabInfo()
            '         If TabEnterScreen.HasAddress = False Then
            'Me.StartDeliveryMethod()
            '        Exit Function
            '   End If
        End If
        '   If tabIdentifierDisplaying = True Then
        'MethodChanged()
        '  End If

    End Function

    Private Sub MethodChanged()

        '    DisposingMethodScreen()
        '   this is after change or first selecting method

        Select Case currentTable.MethodUse
            Case "Dine In"
                StartDineInMethod(False)

            Case "Take Out" 'Or "Pickup"

                If currentTable.TabID > 0 Then
                    StartDeliveryMethod()
                Else
                    StartTakeOutMethod()
                End If
            Case "Pickup"

                If currentTable.TabID > 0 Then
                    StartDeliveryMethod()
                Else
                    StartTakeOutMethod()
                End If
                '   If dsorder.tables("TerminalsMethod")...termDineInIdentifier = true then go to keyboard
            Case "Delivery"
                StartDeliveryMethod()

        End Select

    End Sub


    Private Sub StartTakeOutMethod() ' Handles testgridview.DeliverStart
        If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            orderInactiveTimer.Stop()
            RemoveHandler orderInactiveTimer.Tick, AddressOf OrderInactiveScreenTimeout
        End If
        DisableControls()
        Me.pnlTableInfo.Enabled = True

        '       SeatingTab = New Seating_EnterTab(False, currentTable.TabName)  'from mgmt
        '      SeatingTab.Location = New Point((Me.Width - SeatingTab.Width) / 2, (Me.Height - SeatingTab.Height) / 2)
        '     Me.Controls.Add(SeatingTab)
        '    SeatingTab.BringToFront()
        tabIdentifierDisplaying = True
        RaiseEvent FireSeatingTab("OrderScreen", currentTable.TabName)

    End Sub

    Private Sub UpdateTabToTakeOut222() '444Handles SeatingTab.OpenNewTakeOutTab 

        Dim newTabNameString As String
        newTabNameString = SeatingTab.NewTabName

        '444      If Not currentTable.MethodUse = "Take Out" Then
        '444      currentTable.MethodUse = "Take Out"
        '444     DefineMethodDirection()
        '444    End If
        If currentTable.MethodUse = "Pickup" Then
            wasPickupMethod = True
        Else
            wasPickupMethod = False
        End If
        currentTable.MethodUse = SeatingTab.MethedUse
        '        If currentTable.MethodUse = "Take Out" Then
        'currentTable.TabID = -990 ' -990 is Take Out       'TabEnterScreen.TempTabID
        '     Else
        '   currentTable.TabID = -991 ' -991 is Pickup
        '    End If
        currentTable.TabName = newTabNameString 'TabEnterScreen.TempTabName
        LoadTabIDinExperinceTable()
        SeatingTab.Dispose()
        EnableControls()
        UpdateTableInfo()
        tabIdentifierDisplaying = False
   
    End Sub

    Private Sub UpdateTabName222() '444Handles SeatingTab.OpenNewTabEvent

        wasPickupMethod = False
        currentTable.TabName = SeatingTab.NewTabName
        currentTable.MethodUse = SeatingTab.MethedUse

        LoadTabIDinExperinceTable()
        SeatingTab.Dispose()
        EnableControls()
        UpdateTableInfo()
        tabIdentifierDisplaying = False
     
    End Sub

    Private Sub CancelNewTab() Handles SeatingTab.CancelNewTab

        SeatingTab.Dispose()
        EnableControls()
        tabIdentifierDisplaying = False

    End Sub

    Friend Sub StartDeliveryMethod() Handles testgridview.DeliverStart
        If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            orderInactiveTimer.Stop()
            RemoveHandler orderInactiveTimer.Tick, AddressOf OrderInactiveScreenTimeout
        End If
        DisableControls()
        Me.pnlTableInfo.Enabled = True
        tabScreenDisplaying = True

        RaiseEvent FireTabScreen("TabID", currentTable.TabID)

        Exit Sub
        '222 below

        '      If TabEnterScreen Is Nothing Then
        '    TabEnterScreen = New Tab_Screen("Phone")
        '      TabEnterScreen.Location = New Point(((Me.Width - TabEnterScreen.Width - 10) / 2), ((Me.Height - TabEnterScreen.Height) / 2))
        '     Me.Controls.Add(TabEnterScreen)

        '       Else
        '          TabEnterScreen.StartInSearch = "Phone"
        '         TabEnterScreen.DetermineSearch()
        '        TabEnterScreen.Show()
        '   End If
        '       TabEnterScreen.Visible = True
        '      TabEnterScreen.InitializeOther()
        '     TabEnterScreen.BringToFront()
        '    tabIdentifierDisplaying = True

    End Sub

    Private Sub StartNewCustomerTab222() 'Handles testgridview.DeliverStart
        If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            orderInactiveTimer.Stop()
            RemoveHandler orderInactiveTimer.Tick, AddressOf OrderInactiveScreenTimeout
        End If
        DisableControls()
        Me.pnlTableInfo.Enabled = True
        tabScreenDisplaying = True

        RaiseEvent FireTabScreen("TabID", currentTable.TabID)
        Exit Sub
        '222
        '       TabEnterScreen = New Tab_Screen("Account")
        '   TabEnterScreen.Location = New Point(((Me.Width - TabEnterScreen.Width - 10) / 2), ((Me.Height - TabEnterScreen.Height) / 2))
        '     Me.Controls.Add(TabEnterScreen)

        '      TabEnterScreen.StartInSearch = "Phone"
        '     TabEnterScreen.DetermineSearch()
        '    TabEnterScreen.Show()
        '       TabEnterScreen.Visible = True
        '      TabEnterScreen.BringToFront()
        '     tabIdentifierDisplaying = True

    End Sub

    Friend Sub StartDineInMethod(ByVal fromNewTab As Boolean) Handles testgridview.DineInStart

        If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            orderInactiveTimer.Stop()
            RemoveHandler orderInactiveTimer.Tick, AddressOf OrderInactiveScreenTimeout
        End If
        DisableControls()
        Me.pnlTableInfo.Enabled = True

        If currentTable.MethodDirection = "KeyboardAuto" Or currentTable.MethodDirection = "Keyboard" Then
            If fromNewTab = True And currentTable.MethodDirection = "Keyboard" Then
                'new Tab but not automatically asking for Keyboard
                Exit Sub
            End If
      
            '       SeatingTab = New Seating_EnterTab(False, currentTable.TabName)  'from mgmt
            '      SeatingTab.Location = New Point((Me.Width - SeatingTab.Width) / 2, (Me.Height - SeatingTab.Height) / 2)
            '     Me.Controls.Add(SeatingTab)
            '    SeatingTab.BringToFront()
            RaiseEvent FireSeatingTab("OrderScreen", currentTable.TabName)

        Else
            If ds.Tables("TabIdentifier").Rows.Count > 0 Then
                TabIdentifierScreen = New TabSelection_UC
                TabIdentifierScreen.InitializeCurrentSettings(currentTable.NumberOfCustomers, currentTable.TabName)

                TabIdentifierScreen.Location = New Point(((Me.Width - TabIdentifierScreen.Width - 10) / 2), ((Me.Height - TabIdentifierScreen.Height) / 2))
                Me.Controls.Add(TabIdentifierScreen)

                TabIdentifierScreen.BringToFront()
            Else
                EnableControls()
            End If
        End If
        tabIdentifierDisplaying = True

    End Sub

    Private Sub DisposingMethodScreen()

        '        If Not TabEnterScreen Is Nothing Then
        If tabScreenDisplaying = True Then 'TabEnterScreen.Visible = True Then
            StopDeliveryMethod()
            'currently not saving any changes, unless hit Save in TabScreen
        End If
        If Not TabIdentifierScreen Is Nothing Then
            StopTabMethod()
        End If
        If Not SeatingTab Is Nothing Then
            CancelNewTab()
        End If
        tabIdentifierDisplaying = False

    End Sub


    Friend Sub TabReorderButtonSelected(ByVal dt As DataTable, ByVal tabTestNeeded As Boolean) 'Handles TabEnterScreen.SelectedReOrder

        '      If tabTestNeeded = True Then
        '       TabIDTest()
        '      Else
        '         'this is comming from 
        '        currentTable.TabID = TabEnterScreen.TempTabID
        '       currentTable.TabName = TabEnterScreen.TempTabName
        '      LoadTabIDinExperinceTable()
        '      End If

        Dim oldRow As DataRow
        Dim funRow As DataRow

        For Each oldRow In dt.Rows  'dsCustomer.Tables("TabPreviousOrdersbyItem").Rows    'dtTabPreviousOrdersByItem.Rows '
            Dim currentItem As SelectedItemDetail = New SelectedItemDetail

            With currentItem

                .ExperienceNumber = currentTable.ExperienceNumber 'expNum  
                .OrderNumber = Nothing

                If oldRow("MenuID") Is DBNull.Value Then
                    .MenuID = currentTable.CurrentMenu
                Else
                    .MenuID = oldRow("MenuID")
                End If
                .ShiftID = currentTerminal.CurrentShift 'currentServer.ShiftID  'oldRow("ShiftID")
                .EmployeeID = currentTable.EmployeeID 'empID
                .Check = currentTable.CheckNumber   'oldRow("CheckNumber")
                .Customer = oldRow("CustomerNumber")
                .Course = oldRow("CourseNumber")

                If oldRow("sin") = oldRow("sii") Then
                    ' this is a main item
                    currentTable.ReferenceSIN = currentTable.SIN
                Else

                End If
                .SIN = currentTable.SIN
                .SII = currentTable.ReferenceSIN
                .si2 = oldRow("si2")

                .Quantity = oldRow("Quantity")
                'below is wrong ********** 666
                'we are using OpenDecimal1
                'we are not putting anything in OrederDetail as to InvMultiplier
                'dt we are using: TabPreviousOrdersByItem2
                'sqlAdapter to fill: sqlTabPreviousOrdersLocation
                .InvMultiplier = 1  '*** 666 oldRow("InvMultiplier")
                .ItemStatus = 0 ' oldRow("ItemStatus")
                .Name = oldRow("ItemName")
                .TerminalName = oldRow("TerminalName")
                .ChitName = oldRow("ChitName")
                .ItemPrice = oldRow("ItemPrice")

                .Price = oldRow("ItemPrice") * oldRow("Quantity")
                '                .TaxPrice = oldRow("TaxPrice")
                '               .TaxID = oldRow("TaxID")

                .ForceFreeID = 0 'Nothing
                .ForceFreeAuth = 0 'Nothing
                .ForceFreeCode = 0 'Nothing

                .FunctionID = oldRow("FunctionID")
                Try
                    '999
                    'change this where we don't pull with current item
                    'do it where we only pull when flag is needed 
                    ' as in an if, then statmenet
                    funRow = (dsOrder.Tables("Functions").Rows.Find(oldRow("FunctionID")))
                    .FunctionGroup = funRow("FunctionGroupID")
                    .FunctionFlag = funRow("FunctionFlag")
                Catch ex As Exception
                    .FunctionGroup = 0
                    .FunctionFlag = "O"
                End Try

                .ID = oldRow("ItemID")
                .Category = oldRow("CategoryID")
                .FoodID = oldRow("FoodID")
                .DrinkCategoryID = oldRow("DrinkCategoryID")
                .DrinkID = oldRow("DrinkID")
                .RoutingID = oldRow("RoutingID")
                .PrintPriorityID = oldRow("PrintPriorityID")
                .TerminalID = currentTerminal.TermID    'oldRow("TerminalID")

            End With

            currentTable.SIN += 1
            AddItemToOrderTable(currentItem)
        Next

        EnableControls()
        Me.testgridview.CalculateSubTotal()

    End Sub

    Private Sub StopDeliveryMethod() 'Handles TabEnterScreen.TabScreenDisposing

        ' we will need to fix if we turn back on timer 
        ' TabEnterScreen is initialized in Login

        orderTimeoutCounter = 1
        If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            AddHandler orderInactiveTimer.Tick, AddressOf OrderInactiveScreenTimeout
            orderInactiveTimer.Start()
        End If

        '    If TabEnterScreen.attemptedToEdit = True Then
        'GenerateOrderTables.UpdateTabInfo(TabEnterScreen.StartInSearch)
        '    End If

        tabScreenDisplaying = False
        RaiseEvent TabScreenDisposing()

    End Sub

    Private Sub StopTabMethod() Handles TabIdentifierScreen.TabIdentDispose

        orderTimeoutCounter = 1
        If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            AddHandler orderInactiveTimer.Tick, AddressOf OrderInactiveScreenTimeout
            orderInactiveTimer.Start()
        End If

        Me.TabIdentifierScreen.Dispose()
        Me.TabIdentifierScreen = Nothing
        tabIdentifierDisplaying = False

        UpdateTableInfo()
        EnableControls()

    End Sub


    Private Sub ButtonMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTableInfoMenu.Click
        UserControlHit()
        If EndOfItem(True) = False Then Exit Sub
        PutUsInNormalMode()
        pnlMain.Visible = True
        pnlMain3.Visible = False

        Dim oRow As DataRow
        Dim index As Integer
        Dim tempPrimaryMenuID As Integer
        Dim tempSecondaryMenuID As Integer

        If currentTerminal.currentPrimaryMenuID = currentTerminal.initPrimaryMenuID Then
            tempPrimaryMenuID = currentTerminal.primaryMenuID
            tempSecondaryMenuID = currentTerminal.secondaryMenuID
        Else
            tempPrimaryMenuID = currentTerminal.secondaryMenuID
            tempSecondaryMenuID = currentTerminal.primaryMenuID
        End If

        If currentTable.StartingMenu = currentTerminal.currentPrimaryMenuID Then
            If currentTable.CurrentMenu = currentTerminal.currentPrimaryMenuID Then
                currentTerminal.CurrentMenuID = tempSecondaryMenuID
                currentTable.CurrentMenu = tempSecondaryMenuID
                btnTableInfoMenu.BackColor = c3
                btnTableInfoMenu.ForeColor = c2
                If currentTerminal.currentPrimaryMenuID = currentTerminal.initPrimaryMenuID Then
                    currentTable.IsPrimaryMenu = False
                Else
                    currentTable.IsPrimaryMenu = True
                End If
            Else
                currentTerminal.CurrentMenuID = tempPrimaryMenuID
                currentTable.CurrentMenu = tempPrimaryMenuID
                btnTableInfoMenu.BackColor = c2
                btnTableInfoMenu.ForeColor = c3
                If currentTerminal.currentPrimaryMenuID = currentTerminal.initPrimaryMenuID Then
                    currentTable.IsPrimaryMenu = True
                Else
                    currentTable.IsPrimaryMenu = False
                End If
            End If

        Else
            If currentTable.CurrentMenu = currentTerminal.currentPrimaryMenuID Then
                currentTerminal.CurrentMenuID = currentTable.StartingMenu
                currentTable.CurrentMenu = currentTable.StartingMenu
                btnTableInfoMenu.BackColor = c3
                btnTableInfoMenu.ForeColor = c2
                If currentTerminal.currentPrimaryMenuID = currentTerminal.initPrimaryMenuID Then
                    currentTable.IsPrimaryMenu = False
                Else
                    currentTable.IsPrimaryMenu = True
                End If
            Else
                currentTerminal.CurrentMenuID = tempPrimaryMenuID
                currentTable.CurrentMenu = tempPrimaryMenuID
                btnTableInfoMenu.BackColor = c2
                btnTableInfoMenu.ForeColor = c3
                If currentTerminal.currentPrimaryMenuID = currentTerminal.initPrimaryMenuID Then
                    currentTable.IsPrimaryMenu = True
                Else
                    currentTable.IsPrimaryMenu = False
                End If
            End If
        End If

        PopulateMainButtons()

        For Each oRow In ds.Tables("MenuChoice").Rows
            If oRow("MenuID") = currentTable.CurrentMenu Then
                currentTable.CurrentMenuName = oRow("MenuName")
                Exit For
            End If
        Next
        UpdateTableInfo()

        If Not currentTable.TabID = -888 And Not currentTerminal.TermMethod = "Quick" Then
            ClearOrderPanels()
            pnlOrder.Visible = False
            pnlOrderQuick.Visible = False
            '       FirstStepOrdersPending() only at beginning
        Else
            For index = 1 To 10
                If btnMain(index).CategoryID > 0 Then
                    RunFoodsRoutine(btnMain(index))
                    Exit For
                End If
            Next
        End If

    End Sub

    Private Sub ButtonNumberOfCustomers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTableInfoNumberOfCustomers.Click
        UserControlHit()
        If EndOfItem(True) = False Then Exit Sub

        If currentTable.LastStatus = 1 Then Exit Sub 'can't change on closed check

        If tabTimerActive = False Then
            If tabScreenDisplaying = False Then 'Me.TabEnterScreen.Visible = False Then
                tabDoubleClickTimer = New Timer
                tabTimerActive = True
                AddHandler tabDoubleClickTimer.Tick, AddressOf TabTimerExpired
                tabDoubleClickTimer.Interval = 750
                tabDoubleClickTimer.Start()
            End If

            If tabIdentifierDisplaying = True Then
                DisposingMethodScreen()
                If dvTerminalsUseOrder.Count > 1 Then
                    DetermineNextMethodRow()
                    MethodChanged()
                End If
                tabTimerActive = False
                If Not tabDoubleClickTimer Is Nothing Then
                    'this is Nothing verification is just for the times when we have
                    ' no address in delivery, b/c Delivery Panel begins at start
                    tabDoubleClickTimer.Dispose()
                End If
            Else
                DisposingMethodScreen()
            End If

        Else

            'this means we just DOUBLE clicked
            DisposingMethodScreen()
            tabTimerActive = False
            tabDoubleClickTimer.Dispose()

            If currentTable.MethodUse = "Delivery" Then
                '     If TabEnterScreen.isDisplaying = False Then 'And currentTable.TabID > 0 Then   
                If tabScreenDisplaying = False Then ' Me.TabEnterScreen.Visible = False Then
                    MethodChanged()
                Else
                    DetermineNextMethodRow()
                End If
            Else
                MethodChanged()             ' not set
            End If

        End If
        Exit Sub




        '*** we could make this double click action for everything
        '   downfall, there is a delay to wait for the second click

        Select Case currentTerminal.TermMethod
            Case "Table"
                changeNumberOfCustomers = New DataSet_Builder.NumberOfCustomers_UC
                changeNumberOfCustomers.ColorButtonFromStart(currentTable.NumberOfCustomers)
                changeNumberOfCustomers.Location = New Point((Me.Width - changeNumberOfCustomers.Width) / 2, (Me.Height - changeNumberOfCustomers.Height) / 2)
                Me.Controls.Add(changeNumberOfCustomers)

                changeNumberOfCustomers.BringToFront()
                DisableControls()
            Case "Bar"
                changeNumberOfCustomers = New DataSet_Builder.NumberOfCustomers_UC
                changeNumberOfCustomers.ColorButtonFromStart(currentTable.NumberOfCustomers)
                changeNumberOfCustomers.Location = New Point((Me.Width - changeNumberOfCustomers.Width) / 2, (Me.Height - changeNumberOfCustomers.Height) / 2)
                Me.Controls.Add(changeNumberOfCustomers)

                changeNumberOfCustomers.BringToFront()
                DisableControls()
            Case "Quick"
                DisposingMethodScreen()

                If tabTimerActive = False Then
                    tabDoubleClickTimer = New Timer
                    tabTimerActive = True
                    AddHandler tabDoubleClickTimer.Tick, AddressOf TabTimerExpired
                    tabDoubleClickTimer.Interval = 500
                    tabDoubleClickTimer.Start()
                Else
                    'this means we just DOUBLE clicked
                    tabTimerActive = False
                    tabDoubleClickTimer.Dispose()

                    If currentTable.MethodUse = "Delivery" Then
                        If tabScreenDisplaying = False Then '  TabEnterScreen.isDisplaying = False Then 'And currentTable.TabID > 0 Then   
                            MethodChanged()
                        Else
                            DetermineNextMethodRow()
                        End If
                    Else
                        MethodChanged()             ' not set
                    End If

                End If

        End Select

    End Sub

    Private Sub TabTimerExpired(ByVal sender As Object, ByVal e As System.EventArgs)
        tabTimerActive = False
        tabDoubleClickTimer.Dispose()
        '                               **** this means we clicked


        '     If currentTable.MethodUse = "Delivery" And TabEnterScreen.isDisplaying = False And currentTable.TabID > 0 Then
        '      MethodChanged() 'pulls up
        '     Else
        DetermineNextMethodRow()
        If tabScreenDisplaying = True Then ' TabEnterScreen.isDisplaying = True Then
            'this is b/c determineNextRow will remove delivery screen
            '  TabEnterScreen.isDisplaying = False
            '444      tabScreenDisplaying = False
        End If
        '    End If

    End Sub

    Private Sub NumberOfCustomersSelected(ByVal newNumber As Integer) Handles changeNumberOfCustomers.NumberCustomerEntered
        UserControlHit()

        Dim oRow As DataRow
        '      Dim bRow As DataRow

        If newNumber = currentTable.NumberOfCustomers Then
            EnableControls()
            Me.changeNumberOfCustomers.Dispose()
            Exit Sub
        End If

        currentTable.NumberOfCustomers = newNumber

        If currentTable.IsTabNotTable = True Then
            For Each oRow In dsOrder.Tables("AvailTabs").Rows
                If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                    oRow("NumberOfCustomers") = currentTable.NumberOfCustomers
                End If
            Next
            '         bRow = (dsBackup.Tables("AvailTabsTerminal").Rows.Find(currentTable.ExperienceNumber))
            '         If Not (bRow Is Nothing) Then
            '         bRow("NumberOfCustomers") = currentTable.NumberOfCustomers
            '    End If
        Else
            For Each oRow In dsOrder.Tables("AvailTables").Rows
                If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                    oRow("NumberOfCustomers") = currentTable.NumberOfCustomers
                End If
            Next
            '       bRow = (dsBackup.Tables("AvailTablesTerminal").Rows.Find(currentTable.ExperienceNumber))
            '      If Not (bRow Is Nothing) Then
            '         bRow("NumberOfCustomers") = currentTable.NumberOfCustomers
            '    End If
        End If

        'sss      GenerateOrderTables.SaveAvailTabsAndTables()
        EnableControls()
        Me.changeNumberOfCustomers.Dispose()
        UpdateTableInfo()

    End Sub

    Private Sub ClearOrderPanel()
        Dim index As Integer

        For index = 0 To 31
            If Not btnOrder(index).ID = 0 Then
                With btnOrder(index)
                    .Text = Nothing
                    .ID = 0
                    .DrinkCategory = False
                    .SubCategory = False
                    .Invalidate()
                End With
            End If

        Next

    End Sub

    Private Sub ResetDrinkCategories()
        Dim index As Integer

        For index = 0 To 31
            With btnOrder(index)
                .DrinkCategory = False
                '         .SubCategory = False
                .DrinkAdds = False
            End With
        Next
    End Sub

    Private Sub SendOrderButton_Click(ByVal alsoClose As Boolean) Handles testgridview.SendOrder
        UserControlHit()
        If EndOfItem(False) = False Then Exit Sub

        SendingOrderRoutine()
        If alsoClose = True Then
            LeaveAndSave()
        End If

    End Sub

    Friend Sub SendingOrderRoutine()
        Dim oDetail As OrderDetailInfo
        Dim oRow As DataRow
        Dim limittoOneCourse As Boolean = False
        Dim thereIsAnOrder As Boolean = False

        Try
            If limittoOneCourse = False Then
                For Each oRow In dsOrder.Tables("OpenOrders").Rows  'dvOrderPrint
                    If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                        If oRow("ItemStatus") = 0 Then
                            thereIsAnOrder = True
                            If oRow("FunctionFlag") = "F" Then
                                oDetail.NumDinners += oRow("Quantity")
                            ElseIf oRow("FunctionFlag") = "O" Then
                                oDetail.numApps += oRow("Quantity")     'not just apps
                                '   If oRow("FunctionID") = 1 Then
                                '   oDetail.NumDinners += oRow("Quantity")
                                '   Else
                                '      oDetail.numApps += oRow("Quantity")
                                ' End If
                            ElseIf oRow("FunctionFlag") = "D" Then
                                If oRow("sin") = oRow("sii") Then
                                    oDetail.numDrinks += oRow("Quantity")
                                End If
                            End If
                            oDetail.totalDollar += oRow("Price")
                        End If
                    End If
                Next
            Else
                oDetail.isMainCourse = True
                For Each oRow In dsOrder.Tables("OpenOrders").Rows
                    If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                        If oRow("ItemStatus") = 0 Then
                            thereIsAnOrder = True
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        If thereIsAnOrder = False Then Exit Sub

        If oDetail.NumDinners >= (currentTable.NumberOfCustomers / 2) Then
            oDetail.isMainCourse = True
        End If

        '      If oDetail.NumDinners > oDetail.numApps Then
        '     oDetail.isMainCourse = True
        '    Else
        '       If currentTable.NumberOfCustomers > 0 And oDetail.NumDinners > (currentTable.NumberOfCustomers / 2) Then
        '  oDetail.isMainCourse = True
        '     End If
        ' End If
        Dim prt As New PrintHelper

        oDetail.orderTime = Now
        oDetail.orderNumber = CreateNewOrder(oDetail)
        DetermineTruncatedOrderNumber(oDetail)
        prt.oDetail = oDetail
        prt.SendingOrder(Nothing) '(oDetail)

        GenerateOrderTables.ChangeStatusInDataBase(3, oDetail.orderNumber, oDetail.isMainCourse, oDetail.totalDollar, Nothing, Nothing)
        '   prt.SendingOrder222()

        For Each oRow In dsOrder.Tables("OpenOrders").Rows  'dvOrderPrint
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("ItemStatus") = 0 Then
                    oRow("OrderNumber") = oDetail.orderNumber
                    oRow("ItemStatus") = 2
                End If
            End If
        Next

        'currently just putting this in order detail
        '    AddStatusChangeData(currentTable.ExperienceNumber, 3, oDetail.orderNumber, oDetail.isMainCourse, oDetail.totalDollar)
        '   SaveESCStatusChangeData(3, oDetail.orderNumber, oDetail.isMainCourse, oDetail.totalDollar)

    End Sub

    Private Function DeterminePrintString222(ByVal sRow As DataRow) As String

        '     this is the old printing routine 

        '        If vRow("RoutingID") = printingRouting(1) Then
        '        If vRow("FunctionID") = 1 Or vRow("FunctionID") = 2 Then
        '        sWriter1.Write("*RT*")        'RT ....Reverse Text
        '       End If
        '       sWriter1.WriteLine(vRow("ItemName"))
        '       If s1 = False Then s1 = True
        ''
        '        ElseIf vRow("RoutingID") = printingRouting(2) Then
        '        If vRow("FunctionID") = 1 Or vRow("FunctionID") = 2 Then
        '        sWriter2.Write("*RT*")        'RT ....Reverse Text
        '       End If
        ''       sWriter2.WriteLine(vRow("ItemName"))
        '       If s2 = False Then s2 = True'

        '       ElseIf vRow("RoutingID") = printingRouting(3) Then   'function 4 is drink
        '       If vRow("FunctionFlag") = "D" Then
        '       If vRow("sin") = vRow("sii") Then
        '          sWriter3.Write("*RT*")        'RT ....Reverse Text
        '    End If
        ''    sWriter3.WriteLine(vRow("ItemName"))
        '    If s3 = False Then s3 = True

        '        End If
        '
        '       End If
        ''

    End Function







    Private Sub PlaceOrderInExperienceStatusChange222(ByVal status As Integer, ByVal orderNumber As Integer, ByVal isMainCourse As Boolean, ByVal avgDollar As Single)

        Dim cmd As SqlClient.SqlCommand

        cmd = New SqlClient.SqlCommand("INSERT INTO ExperienceStatusChange (ExperienceNumber, StatusTime, TableStatusID, OrderNumber, IsMainCourse, AverageDollar) VALUES (@ExperienceNumber, @StatusTime, @TableStatusID, @OrderNumber, @IsMainCourse, @AverageDollar)", sql.cn)

        cmd.Parameters.Add(New SqlClient.SqlParameter("@ExperienceNumber", SqlDbType.BigInt, 8))
        cmd.Parameters("@ExperienceNumber").Value = currentTable.ExperienceNumber
        cmd.Parameters.Add(New SqlClient.SqlParameter("@StatusTime", SqlDbType.DateTime, 8))
        cmd.Parameters("@StatusTime").Value = Now
        cmd.Parameters.Add(New SqlClient.SqlParameter("@TableStatusID", SqlDbType.Int, 4))
        cmd.Parameters("@TableStatusID").Value = status
        cmd.Parameters.Add(New SqlClient.SqlParameter("@OrderNumber", SqlDbType.Int, 4))
        cmd.Parameters("@OrderNumber").Value = orderNumber
        cmd.Parameters.Add(New SqlClient.SqlParameter("@IsMainCourse", SqlDbType.Bit, 1))
        cmd.Parameters("@IsMainCourse").Value = isMainCourse
        cmd.Parameters.Add(New SqlClient.SqlParameter("@AverageDollar", SqlDbType.Money, 8))
        cmd.Parameters("@AverageDollar").Value = avgDollar

        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        cmd.ExecuteNonQuery()
        sql.cn.Close()


    End Sub


    Private Function GetLastOrderNumber()
        Dim _lastOrderNumber As Integer

    End Function







    Private Sub btnModifyClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles testgridview.ModifyItem
        UserControlHit()

        '       If modifyComboBox.Visible = True Then
        '      modifyComboBox.Visible = False
        '     Me.testgridview.gridViewOrder.Enabled = True
        '    Else

        Dim rowNum As Integer = OpenOrdersCurrencyMan.Position  'gridViewOrder.CurrentCell.RowNumber

        Dim valueSIN As Integer
        Dim valueSII As Integer
        Dim valueSI2 As Integer
        '     Dim valueText As String
        Dim index As Integer
        Dim valueIndex As Integer

        Try
            valueSIN = CType(Me.testgridview.gridViewOrder.Item(rowNum, 1), Integer)
            valueSII = CType(Me.testgridview.gridViewOrder.Item(rowNum, 2), Integer)
            '        valueText = CType(Me.testgridview.gridViewOrder.Item(rowNum, 8), String)
            valueSI2 = CType(Me.testgridview.gridViewOrder.Item(rowNum, 3), Integer)

        Catch ex As Exception
            Exit Sub
        End Try

        Dim oRow As DataRow
        Dim newStatus As Integer


        '      If valueSII = valueSIN Then
        '     '   *** need to change to reflect quantity
        '    info = New DataSet_Builder.Information_UC("You can only Modify the sub-Items of an Order. Please Delete Item and reorder.")
        '   info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
        '  Me.Controls.Add(info)
        ' info.BringToFront()

        '    Else
        Dim catID As Integer
        Dim drinkCatID As Integer
        Dim funID As Integer
        Dim funFlag As String
        Dim currentItemID As Integer
        Dim currentPrice As Decimal
        Dim currentSIN As Integer
        Dim currentSII As Integer
        Dim currentName As String
        Dim currentTerminalName As String
        Dim currentChitName As String
        Dim currentQ As Integer
        Dim currentCN As Integer
        Dim currentC As Integer
        Dim alreadyOrdered As Boolean

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

        If Not oRow("ItemID") > 0 Then Exit Sub
        '   this is any non ordered item, transfered, customer panel.....

        If oRow("ItemStatus") > 1 Then
            alreadyOrdered = True

            '         info = New DataSet_Builder.Information_UC("You can only Modify items not sent to the Kitchen.")
            '        info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            '       Me.Controls.Add(info)
            '      info.BringToFront()
            '     Exit Sub
        End If


        catID = oRow("CategoryID")
        drinkCatID = oRow("DrinkCategoryID")
        funID = oRow("FunctionID")
        funFlag = oRow("FunctionFlag")
        currentItemID = oRow("ItemID")
        currentPrice = oRow("Price")
        currentSIN = oRow("sin")
        currentSII = oRow("sii")
        currentName = oRow("ItemName")
        currentTerminalName = oRow("TerminalName")
        currentChitName = oRow("ChitName")
        currentQ = oRow("Quantity")
        currentCN = oRow("CustomerNumber")
        currentC = oRow("CourseNumber")

        Dim populatingTable As String

        If funFlag = "M" Then   'catID > 100 Then
            populatingTable = "ModifierTable"
        Else
            If currentTable.IsPrimaryMenu = True Then
                populatingTable = "MainTable"
            Else
                populatingTable = "SecondaryMainTable"
            End If
        End If

        '       If valueSI2 >= 10 Then
        '      MsgBox("You may not Modify this item. You must delete and ReOrder.")
        '     Exit Sub
        '    End If

        If valueSII = valueSIN Then
            With dvSendToModify
                .Table = dsOrder.Tables("OpenOrders")
                .RowFilter = "sii = '" & valueSII & "'"
            End With

            If funFlag = "F" Or funFlag = "O" Or funFlag = "M" Then       'funID < 4 Then
                DisplayModifyOrder(currentSII, currentSIN, currentTerminalName, currentName, currentChitName, dvSendToModify, True, currentItemID, currentPrice, funID, funFlag, currentCN, currentQ, currentC, alreadyOrdered)
            ElseIf funFlag = "D" Then   '  funID >= 4 And funID <= 7 Then
                DisplayModifyOrder(currentSII, currentSIN, currentTerminalName, currentName, currentChitName, dvSendToModify, False, currentItemID, currentPrice, funID, funFlag, currentCN, currentQ, currentC, alreadyOrdered)
            End If

        Else

            If funFlag = "F" Or funFlag = "O" Or funFlag = "M" Then  'funID = 2 Or funID = 3 Then
                '     dvFoodJoin = New DataView(ds.Tables("FoodTable"), "CategoryID ='" & catID & "'", "FoodID", DataViewRowState.CurrentRows)
                With dvFoodJoin
                    .Table = ds.Tables(populatingTable & catID)
                    .Sort = "FoodName"
                End With
                DisplayModifyOrder(Nothing, currentSIN, currentTerminalName, currentName, currentChitName, dvFoodJoin, True, currentItemID, currentPrice, funID, funFlag, currentCN, currentQ, currentC, alreadyOrdered)
            ElseIf funFlag = "D" Then       'funID >= 4 And funID <= 7 Then
                '            MsgBox("You can not Modify a Drink Order. Please Delete Item and reorder.")
                '           Exit Sub
                '     dvDrink = New DataView(ds.Tables("Drink"), "DrinkCategoryID ='" & drinkCatID & "'", "DrinkIndex", DataViewRowState.CurrentRows)
                With dvDrink
                    .Table = ds.Tables("Drink")
                    .RowFilter = "DrinkCategoryID ='" & drinkCatID & "'"
                    .Sort = "DrinkIndex"
                End With

                DisplayModifyOrder(Nothing, currentSIN, currentTerminalName, currentName, currentChitName, dvDrink, False, currentItemID, currentPrice, funID, funFlag, currentCN, currentQ, currentC, alreadyOrdered)
            End If
        End If


    End Sub

    Private Sub DisplayModifyOrder(ByVal currentSII As Integer, ByVal currentSIN As Integer, ByVal currentTerminalName As String, ByVal currentname As String, ByVal currentChitName As String, ByVal dvModify As DataView, ByVal isFood As Boolean, ByVal currentItemID As Integer, ByVal currentPrice As Decimal, ByVal funID As Integer, ByVal funFlag As String, ByVal cn As Integer, ByVal q As Integer, ByVal c As Integer, ByVal alreadyOrdered As Boolean)

        Me.modifyOrderUserControl = New ModifyOrder_UC(currentSII, currentSIN, currentTerminalName, currentname, currentChitName, dvModify, isFood, currentItemID, currentPrice, funID, funFlag, cn, q, c, alreadyOrdered)
        Me.modifyOrderUserControl.Location = New Point((Me.Width - Me.modifyOrderUserControl.Width) / 2, (Me.Height - Me.modifyOrderUserControl.Height) / 2)
        Me.Controls.Add(Me.modifyOrderUserControl)
        Me.modifyOrderUserControl.BringToFront()
        DisableControls()

    End Sub

    Private Sub UpdateModifiedSubTotal() Handles modifyOrderUserControl.AcceptModifySubTotal

        Me.testgridview.CalculateSubTotal()
        EnableControls()

        Dim numberNONCourse2 As Integer
        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
            numberNONCourse2 = dsOrder.Tables("OpenOrders").Compute("Count(Quantity)", "CourseNumber > 2 OR CourseNumber = 1")
        Else : numberNONCourse2 = 0
        End If


        If Not numberNONCourse2 = 0 Then
            Me.testgridview.MakeRoomForCourseInfo()
        End If

        Dim maxQuantity As Integer
        maxQuantity = dsOrder.Tables("OpenOrders").Compute("Max(Quantity)", "sin = sii")
        Me.testgridview.TestQuantityForDisplay(maxQuantity)


    End Sub

    Private Sub UpdateModifiedItem() Handles modifyOrderUserControl.AcceptModify
        UserControlHit()
        EnableControls()

        If Me.modifyOrderUserControl.ModifyItemID > 0 Then
            Dim oRow As DataRow

            If Not typeProgram = "Online_Demo" Then
                oRow = (dsOrder.Tables("OpenOrders").Rows.Find(Me.modifyOrderUserControl.ModifyCurrentSIN))
            Else
                For Each oRow In dsOrder.Tables("OpenOrders").Rows
                    If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                        If oRow("sin") = Me.modifyOrderUserControl.ModifyCurrentSIN Then
                            Exit For
                        End If
                    End If
                Next
            End If
            '     For Each oRow In dsOrder.Tables("OpenOrders").Rows
            '    If oRow("sin") = Me.modifyOrderUserControl.ModifyCurrentSIN Then
            oRow("ItemID") = Me.modifyOrderUserControl.ModifyItemID
            oRow("ItemName") = Me.modifyOrderUserControl.ModifyItemName
            oRow("TerminalName") = Me.modifyOrderUserControl.ModifyAbrevName
            oRow("ChitName") = Me.modifyOrderUserControl.ModifyChitName
            oRow("Price") = Me.modifyOrderUserControl.ModifySurcharge
            oRow("TaxPrice") = GenerateOrderTables.DetermineTaxPrice(Me.modifyOrderUserControl.ModifyTaxID, Me.modifyOrderUserControl.ModifySurcharge)
            oRow("TaxID") = Me.modifyOrderUserControl.ModifyTaxID
            If Me.modifyOrderUserControl.isFoodItem = True Then
                oRow("FoodID") = Me.modifyOrderUserControl.ModifyItemID
            Else
                oRow("DrinkID") = Me.modifyOrderUserControl.ModifyItemID
            End If
            oRow("RoutingID") = Me.modifyOrderUserControl.ModifyRoutingID

            Me.testgridview.CalculateSubTotal()
            '      End If
            '     Next
        End If

    End Sub

    Private Sub CancelModifiedItem() Handles modifyOrderUserControl.CancelModify
        EnableControls()

    End Sub

    Private Sub Leave_Click() Handles testgridview.LeaveOrderView

        UserControlHit()
        'already done     If EndOfItem(False) = False Then Exit Sub

        If Not currentTerminal.TermMethod = "Quick" Or currentServer.EmployeeID = 6986 Then
            LeaveAndSave()
        Else
            DisposeDataViewsOrder()
            AdjustOpenOrderPosition()

            If currentTable.EmptyCustPanel > 0 Then RemoveEmptyPanel()
            If IsManagerMode = True Then
                RaiseEvent TermOrder_Disposing(actingManager, Nothing)
            Else
                RaiseEvent TermOrder_Disposing(currentServer, Nothing)
            End If
        End If

    End Sub

    '  Private Sub SaveDontLeave() Handles testgridview.JustSaveOrder

    '     If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
    '         AdjustOpenOrderPosition()
    '         GenerateOrderTables.SaveOpenOrderData()
    '     End If

    '    End Sub

    Private Sub LeaveAndSave(Optional ByRef ccDisplay As CashClose_UC = Nothing)  ' Handles testgridview.LeaveOrderView

        If Not repeatOrderUserControl Is Nothing Then
            'this is different b/c we use for repeat order and pending order
            'we can change
            repeatOrderUserControl.Dispose()
        End If
        If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Or currentServer.EmployeeID = 6986 Then
            orderInactiveTimer.Stop()
            RemoveHandler orderInactiveTimer.Tick, AddressOf OrderInactiveScreenTimeout

            DisposeDataViewsOrder()
            AdjustOpenOrderPosition()

            If currentTable.EmptyCustPanel > 0 Then RemoveEmptyPanel()
            If IsManagerMode = True Then
                '       DisposeObjects()
                RaiseEvent TermOrder_Disposing(actingManager, ccDisplay)
            Else
                '       DisposeObjects()
                RaiseEvent TermOrder_Disposing(currentServer, ccDisplay)
            End If

        Else

            RaiseEvent QuickOrder_NotDisposing()
        End If

        '     GC.Collect()
        '     Me.Dispose()

    End Sub

    Friend Sub DisposeOrderFormObjects()
        Dim i As Integer

        For i = 1 To 20
            '           btnMain(i).Dispose()
            btnMain(i) = Nothing
        Next
        For i = 1 To 10
            '          btnModifier(i).Dispose()
            btnModifier(i) = Nothing
        Next
        If Not currentTerminal.TermMethod = "Quick" Then
            For i = 0 To 31
                '             btnOrder(i).Dispose()
                btnOrder(i) = Nothing
            Next
        Else
            For i = 0 To 59
                '              btnOrder(i).Dispose()
                btnOrderQuick(i) = Nothing
            Next
        End If

        For i = 1 To 60
            '         btnOrderDrink(i).Dispose()
            btnOrderDrink(i) = Nothing
        Next
        For i = 0 To 23
            '        btnOrderModifier(i).Dispose()
            btnOrderModifier(i) = Nothing
        Next

        btnOrderModifierCancel = Nothing
        btnMainNext = Nothing
        btnMainNextMain3 = Nothing
        btnMainPrevious = Nothing

        btnModifierNo = Nothing
        btnModifierAdd = Nothing
        btnModifierExtra = Nothing
        btnModifierOnFly = Nothing
        btnModifierNoMake = Nothing
        btnModifierOnSide = Nothing
        btnModifierNoCharge = Nothing
        btnModifierSpecial = Nothing
        btnModifierRepeat = Nothing
        btnModifierBlank = Nothing

        For i = 1 To 5
            btnCustomer(i) = Nothing
        Next

        btnCustomerNext = Nothing

        Me.onFullPizza.DataSource = Nothing
        Me.onFirstHalf.DataSource = Nothing
        Me.onSecondHalf.DataSource = Nothing

        extraNoUserControl.Dispose()
        extraNoUserControl = Nothing
        Me.testgridview.Dispose()
        Me.testgridview = Nothing

    End Sub

    Private Sub RemoveEmptyPanel()
        Dim oRow As DataRow
        '   for terminal data

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("CustomerNumber") = currentTable.EmptyCustPanel Then
                    GenerateOrderTables.DeleteOpenOrdersRowTerminal(oRow)
                    Exit Sub
                End If
            End If
        Next
    End Sub

    Private Sub UpdateTableStatus222(ByVal tn As Integer, ByVal newStatus As Integer, ByVal avg As Single)
        '   may be able to improve efficiency by not looking through each row
        '   this will take knowing the row index prior and sending to sub

        MsgBox("WE do not use this step, Use AddStatusChangeData")

        Dim dt As DateTime = DateTime.Now
        Dim oRow As DataRow

        If newStatus = 2 Then
            For Each oRow In dtCurrentStatus.Rows
                If (oRow("TableNumber")) = tn Then
                    oRow("SatTime") = dt
                    oRow("LastStatusTime") = dt
                    oRow("AverageDollar") = avg
                    oRow("LastStatus") = newStatus
                End If
            Next
        Else
            For Each oRow In dtCurrentStatus.Rows
                If (oRow("TableNumber")) = tn Then
                    oRow("LastStatusTime") = dt
                    oRow("AverageDollar") = avg
                    oRow("LastStatus") = newStatus
                End If
            Next
        End If

    End Sub

    Private Sub BtnModifierAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModifierAdd.Click

        UserControlHit()
        If EndOfItem(False) = False Then Exit Sub

        If ADDorNOmode = True Then
            PutUsInNormalMode()
        Else
            If btnModifierAdd.Text = "Prep" Then
                currentTable.OrderingStatus = "Prep" 'Nothing
            Else
                If sender.text = "Extra" Then
                    currentTable.OrderingStatus = "EXTRA"
                Else
                    currentTable.OrderingStatus = "ADD"
                End If
            End If

            btnModifierAdd.BackColor = c9
            btnModifierNo.BackColor = c4
            StartAddNOMode()
        End If

    End Sub

    Private Sub BtnModifierNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModifierNo.Click, btnModifierExtra.Click

        UserControlHit()
        If EndOfItem(False) = False Then Exit Sub

        If ADDorNOmode = True Then
            PutUsInNormalMode()
        Else
            If btnModifierAdd.Text = "Prep" Then
                currentTable.OrderingStatus = "Call" 'Nothing
            Else
                If sender.text = "Extra" Then
                    currentTable.OrderingStatus = "EXTRA"
                Else
                    currentTable.OrderingStatus = "NO"
                End If
            End If

            btnModifierNo.BackColor = c9
            btnModifierAdd.BackColor = c4
            StartAddNOMode()
        End If

    End Sub

    Private Sub StartAddNOMode()

        Dim rowNum As Integer = OpenOrdersCurrencyMan.Position
        Dim oRow As DataRow
        Dim itemCatID As Integer
        Dim itemID As Integer
        Dim routeID As Integer
        Dim valueSIN As Integer
        Dim valueSII As Integer

        Try
            valueSIN = CType(Me.testgridview.gridViewOrder.Item(rowNum, 1), Integer)
            valueSII = CType(Me.testgridview.gridViewOrder.Item(rowNum, 2), Integer)
        Catch ex As Exception
            Exit Sub
        End Try

        If Not typeProgram = "Online_Demo" Then
            oRow = (dsOrder.Tables("OpenOrders").Rows.Find(valueSII))
        Else
            For Each oRow In dsOrder.Tables("OpenOrders").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("sin") = valueSII Then
                        Exit For
                    End If
                End If
            Next
        End If

        With dvIngredients
            .Table = ds.Tables("Ingredients")
            .RowFilter = "FoodID = '" & oRow("FoodID") & "'"
        End With
        With dvIngredientsEXTRA
            .Table = ds.Tables("Ingredients")
            .RowFilter = "FoodID = '" & oRow("FoodID") & "' AND SelectExtra = True"
        End With
        With dvIngredientsNO
            .Table = ds.Tables("Ingredients")
            .RowFilter = "FoodID = '" & oRow("FoodID") & "' AND SelectNo = True"
        End With

        currentTable.si2 = oRow("si2")
        '    If btnModifierAdd.Text = "Prep" And oRow("FunctionFlag") = "D" Then
        If currentTable.OrderingStatus = "Prep" And oRow("FunctionFlag") = "D" Then
            'this is hit Add/No/Extra

            'they should both happen or neither
            itemCatID = oRow("DrinkCategoryID")
            itemID = oRow("DrinkID")
            routeID = oRow("RoutingID")
            currentTable.ReferenceSIN = valueSII
            SelectDrinkAdds(itemID, itemCatID, routeID, oRow("FunctionID"), oRow("FunctionGroupID"))
        ElseIf currentTable.OrderingStatus = "Call" And oRow("FunctionFlag") = "D" Then
            'this hit call drink 
            '    drinkcatid = oRow("DrinkCategoryID")
            drinkCall_Click()
        Else
            itemCatID = oRow("CategoryID")
            itemID = oRow("FoodID")
            routeID = oRow("RoutingID")
            currentTable.ReferenceSIN = valueSII
            PutUsInAddNOMode(itemID, itemCatID, routeID, oRow("FunctionID"), oRow("FunctionGroupID"))
        End If

    End Sub

    Private Sub PutUsInAddNOMode(ByVal itemID As Integer, ByVal catID As Integer, ByVal routeID As Integer, ByVal funID As Integer, ByVal funGroupID As Integer)

        ADDorNOmode = True
        '    currentTable.OrderingStatus = "ADD"
        Me.drinkPrep.StartAddNo(itemID, catID, routeID, funID, funGroupID)
        '444   AssignReferenceSIN()
        '      pnlMain.Visible = False
        '     pnlMain2.Visible = False
        '    pnlMain3.Visible = True
        pnlOrder.Visible = False
        pnlOrderQuick.Visible = False
        pnlOrderModifier.Visible = False
        pnlOrderModifierExt.Visible = False
        pnlOrderDrink.Visible = False
        drinkPrep.Visible = True
        '444     extraNoUserControl.Visible = False

        currentTable.MiddleOfOrder = True
        SetCategoryIndexToFalse()

    End Sub

    Private Sub PutUsInNormalMode()
        ADDorNOmode = False
        '    pnlMain.Visible = True
        '   pnlMain3.Visible = False
        btnModifierAdd.BackColor = c4
        btnModifierNo.BackColor = c4
        btnModifierExtra.BackColor = c4
        previousCategory = Nothing
        Me.extraNoUserControl.Visible = False
        '       Me.pnlOrder.Visible = False
        '     pnlOrderQuick.Visible = False
        '       Me.pnlOrderDrink.Visible = False
        '       Me.pnlOrderModifier.Visible = False
        GTCIndex = -1

        currentTable.OrderingStatus = Nothing
    End Sub

    Private Sub ClearOrderPanels() Handles testgridview.ClearPanels
        'also called from this class

        '      Me.pnlOrder.Visible = False
        Me.pnlOrderDrink.Visible = False
        Me.pnlOrderModifier.Visible = False
        pnlOrderModifierExt.Visible = False
        Me.extraNoUserControl.Visible = False

        If OpenOrdersCurrencyMan.Position > -1 Then
            If Me.testgridview.gridViewOrder.Item(OpenOrdersCurrencyMan.Position, 1) = Me.testgridview.gridViewOrder.Item(OpenOrdersCurrencyMan.Position, 2) Then
                'this is for main drink item
                'sin = sii
                drinkPrep.Visible = False
            End If
        Else
            drinkPrep.Visible = False
        End If

    End Sub

    Private Sub SetCategoryIndexToFalse()
        Dim vRow As DataRowView
        Dim i As Integer = 0
        ReDim categoryIndex(dvCategoryJoin.Count)
        For Each vRow In dvCategoryJoin
            categoryIndex(i) = False
            i += 1
        Next

        i = 0
        ReDim categoryIndexSecondLoop(dvCategoryJoinSecondLoop.Count)
        For Each vRow In dvCategoryJoinSecondLoop
            categoryIndex(i) = False
            i += 1
        Next

    End Sub

    Private Sub ClosingExtraNo() Handles extraNoUserControl.SelectedClose

        PutUsInNormalMode()

    End Sub


    Private Sub BtnModifierSpecial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModifierSpecial.Click
        '   maybe add to time out interval (only temp)
        UserControlHit()
        'maybe don't make req modifier with special
        'this allows for flexibility
        'if we do, (True) make it so we don't reset currenttable.info
        '    If EndOfItem(False) = False Then Exit Sub

        Dim rowNum As Integer = OpenOrdersCurrencyMan.Position  'gridViewOrder.CurrentCell.RowNumber
        Dim isDrink As Boolean
        Dim assocItem As Boolean
        Dim currentRouting As Integer

        Dim valueSIN As Integer
        Dim valueSII As Integer
        Dim valueSI2 As Integer
        Dim oRow As DataRow
        Dim hasRow As Boolean = False

        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
            If OpenOrdersCurrencyMan.Count = 0 Then
                currentTable.SIN += 1
                currentTable.ReferenceSIN += 1
                valueSIN = currentTable.SIN
                valueSII = currentTable.ReferenceSIN
            Else
                Try
                    valueSIN = CType(Me.testgridview.gridViewOrder.Item(rowNum, 1), Integer)
                    valueSII = CType(Me.testgridview.gridViewOrder.Item(rowNum, 2), Integer)
                    valueSI2 = CType(Me.testgridview.gridViewOrder.Item(rowNum, 3), Integer)
                Catch ex As Exception
                    Exit Sub
                End Try
            End If
            If Not typeProgram = "Online_Demo" Then
                oRow = (dsOrder.Tables("OpenOrders").Rows.Find(valueSIN))
            Else
                For Each oRow In dsOrder.Tables("OpenOrders").Rows
                    If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                        If oRow("sin") = valueSIN Then
                            hasRow = True
                            Exit For
                        End If
                    End If
                Next
            End If

            If hasRow = True Then
                currentRouting = oRow("RoutingID")
            Else
                currentRouting = 0
            End If

        Else
            valueSIN = currentTable.SIN
            valueSII = currentTable.ReferenceSIN
            currentRouting = 0
        End If


        If currentTable.MiddleOfOrder = True Then
            assocItem = True
            If drinkPrep.Visible = True Then  'pnlDrinkModifier.Visible = True Then
                '  ffff()
                isDrink = True
            End If
        Else
            If IsBartenderMode = True Then
                isDrink = True
            End If
        End If

        SpecialItem = New SpecialFood(valueSIN, valueSII, isDrink, assocItem, currentRouting)
        SpecialItem.Location = New Point((Me.Width - SpecialItem.Width) / 2, (Me.Height - SpecialItem.Height) / 2)
        Me.Controls.Add(SpecialItem)

        SpecialItem.BringToFront()
        DisableControls()
        '   Me.Enabled = False

    End Sub

    Private Sub SpecialCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SpecialItem.CancelSpecial

        UserControlHit()
        EnableControls()
        '    Me.Enabled = True

    End Sub

    Private Sub DisableControls()
        Me.pnlDescription.Enabled = False
        '      Me.pnlDrinkModifier.Enabled = False
        Me.pnlMain.Enabled = False
        Me.pnlMain2.Enabled = False
        Me.pnlMain3.Enabled = False
        Me.pnlMainModifier.Enabled = False
        Me.pnlOrder.Enabled = False
        Me.pnlOrderDrink.Enabled = False
        Me.pnlOrderModifier.Enabled = False
        Me.pnlTableInfo.Enabled = False
        Me.pnlWineParring.Enabled = False
        Me.testgridview.Enabled = False
        If Not currentTerminal.TermMethod = "Quick" Then
            Me.customerPanel.Enabled = False
        End If

    End Sub

    Friend Sub EnableControls()
        Me.pnlDescription.Enabled = True
        '      Me.pnlDrinkModifier.Enabled = True
        Me.pnlMain.Enabled = True
        Me.pnlMain2.Enabled = True
        Me.pnlMain3.Enabled = True
        Me.pnlMainModifier.Enabled = True
        Me.pnlOrder.Enabled = True
        Me.pnlOrderDrink.Enabled = True
        Me.pnlOrderModifier.Enabled = True
        Me.pnlTableInfo.Enabled = True
        Me.pnlWineParring.Enabled = True
        Me.testgridview.Enabled = True
        If Not currentTerminal.TermMethod = "Quick" Then
            Me.customerPanel.Enabled = True
        End If

    End Sub

    Private Sub SpecialInstructionsEntered(ByVal sender As Object, ByVal e As System.EventArgs) Handles SpecialItem.AcceptSpecial
        UserControlHit()

        Dim specialInstructions As String
        Dim specialPrice As Decimal
        Dim associateSIN As Integer

        '  If SpecialItem.SpecialKeyboard.EnteredString Is Nothing Or SpecialItem.SpecialKeyboard.EnteredString.Length = 0 Then
        '      we do this on the keyboard

        If SpecialItem.FunctionGroup = 4 Then
            specialInstructions = "**  Open Liquor *** "
        ElseIf SpecialItem.FunctionGroup = 2 Then
            specialInstructions = "**  Open Beer  *** "
        ElseIf SpecialItem.FunctionGroup = 3 Then
            specialInstructions = "**  Open Wine  *** "
        ElseIf SpecialItem.FunctionGroup = 5 Then
            specialInstructions = "**  Open Drink *** "
        ElseIf SpecialItem.FunctionGroup = 1 Or SpecialItem.FunctionGroup = 10 Then
            specialInstructions = "**  Open Food  *** "
        ElseIf SpecialItem.FunctionGroup = 8 Then
            If SpecialItem.SpecialKeyboard.EnteredString.Length = 0 Then
                EnableControls()
                SpecialItem.Dispose()
                Exit Sub
            Else
                specialInstructions = "**  Special  *** "
            End If
        End If
        '      End If

        If SpecialItem.SpecialKeyboard.EnteredString.Length > 0 Then
            specialInstructions = "**  " & SpecialItem.ItemDescription & "  **"
        End If

        specialPrice = Me.SpecialItem.NumberPadSmall1.NumberTotal

        '      If specialInstructions.Length > 34 Then
        '    specialInstructions = specialInstructions.Substring(0, 20)
        '     End If

        Dim currentItem As New SelectedItemDetail

        With currentItem
            '         If currentTable.IsTabNotTable = False Then
            '        .Table = currentTable.TableNumber
            '       Else
            '          .Table = currentTable.TabID
            '     End If
            .Check = currentTable.CheckNumber
            .Customer = currentTable.CustomerNumber
            .Course = currentTable.CourseNumber

            .SIN = currentTable.SIN
            If Not SpecialItem.AssociateSIN = Nothing Then
                .SII = SpecialItem.AssociateSIN
                .Name = "   " & specialInstructions
                .TerminalName = "   " & specialInstructions
                .ChitName = "   " & specialInstructions
            Else
                .SII = currentTable.SIN
                .Name = specialInstructions
                .TerminalName = specialInstructions
                .ChitName = specialInstructions
            End If
            .si2 = currentTable.si2
            .ID = -1

            If Not SpecialItem.AssociateSIN = Nothing Then
                .Quantity = currentTable.Quantity
                .InvMultiplier = currentTable.InvMultiplier    'can make more variable sometime
                .ItemPrice = specialPrice
                .Price = (specialPrice * currentTable.Quantity)
            Else
                .Quantity = 1
                .InvMultiplier = 1    'can make more variable sometime
                .ItemPrice = specialPrice
                .Price = (specialPrice)
            End If
            .PrintPriorityID = 1

            .FunctionGroup = SpecialItem.FunctionGroup
            .FunctionFlag = SpecialItem.FunctionFlag

            .Category = -1  'SpecialItem.CategoryID
            '     .FunctionID = SpecialItem.FunctionID
            '    .FunctionFlag = SpecialItem.FunctionFlag
            '   .RoutingID = SpecialItem.RoutingID
            '           .TaxID = GenerateOrderTables.DetermineTaxID(SpecialItem.FunctionID)       '    = SpecialItem.TaxID
        End With

        GenerateOrderTables.DetermineFunctionAndTaxInfo(currentItem, SpecialItem.FunctionGroup, True)
        ' If SpecialItem.associateItem = True Then
        currentItem.RoutingID = SpecialItem.RoutingID 'CurrentRouting
        'End If

        currentTable.SIN += 1
        AddItemToOrderTable(currentItem)
        Me.testgridview.CalculateSubTotal()

        EnableControls()
        SpecialItem.Dispose()

    End Sub

    Private Sub SpecialInstructionsEnteredOld222(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles SpecialItem.AcceptSpecial
        UserControlHit()

        '   ********** can remove these two b/c we changed to a UC

        '        Me.BringToFront()

        Dim specialInstructions As String
        Dim specialPrice As Decimal
        Dim associateSIN As Integer

        If SpecialItem.SpecialKeyboard.EnteredString Is Nothing Or SpecialItem.SpecialKeyboard.EnteredString.Length = 0 Then
            '      we do this on the keyboard
            If SpecialItem.CategoryID = -1 Then
                If SpecialItem.FunctionFlag = "D" Then
                    specialInstructions = "Open Drink *** "
                ElseIf SpecialItem.FunctionID = 1 Then
                    specialInstructions = "Open Food *** "
                Else
                    specialInstructions = "   Open Food *** "
                End If
            Else

            End If
        Else
            If SpecialItem.CategoryID = -1 Then
                If SpecialItem.FunctionFlag = "D" Then
                    specialInstructions = SpecialItem.ItemDescription 'SpecialItem.SpecialKeyboard.EnteredString
                ElseIf SpecialItem.FunctionID = 1 Then
                    specialInstructions = SpecialItem.ItemDescription 'SpecialItem.SpecialKeyboard.EnteredString
                Else
                    specialInstructions = "   " & SpecialItem.ItemDescription 'SpecialItem.SpecialKeyboard.EnteredString
                End If
            Else
                specialInstructions = SpecialItem.ItemDescription '"*** " & SpecialItem.SpecialKeyboard.EnteredString
            End If
            '        specialInstructions = specialInstructions & SpecialItem.SpecialKeyboard.EnteredString
        End If

        specialPrice = Me.SpecialItem.NumberPadSmall1.NumberTotal
        EnableControls()
        SpecialItem.Dispose()

        '      If specialInstructions.Length > 34 Then
        '    specialInstructions = specialInstructions.Substring(0, 20)
        '     End If

        Dim currentItem As New SelectedItemDetail

        With currentItem
            '         If currentTable.IsTabNotTable = False Then
            '        .Table = currentTable.TableNumber
            '       Else
            '          .Table = currentTable.TabID
            '     End If
            .Check = currentTable.CheckNumber
            .Customer = currentTable.CustomerNumber
            .Course = currentTable.CourseNumber

            .SIN = currentTable.SIN
            If Not SpecialItem.AssociateSIN = Nothing Then
                .SII = SpecialItem.AssociateSIN
            Else
                .SII = currentTable.SIN
            End If
            .si2 = currentTable.si2
            .ID = -1
            .Name = specialInstructions
            .TerminalName = specialInstructions
            .ChitName = specialInstructions
            If Not SpecialItem.AssociateSIN = Nothing Then
                .Quantity = currentTable.Quantity
                .Price = (specialPrice * currentTable.Quantity)
            Else
                .Quantity = 1
                .Price = (specialPrice)
            End If

            .Category = SpecialItem.CategoryID
            .FunctionID = SpecialItem.FunctionID
            .FunctionGroup = SpecialItem.FunctionGroup
            .FunctionFlag = SpecialItem.FunctionFlag
            .RoutingID = SpecialItem.RoutingID
            '           .TaxID = GenerateOrderTables.DetermineTaxID(SpecialItem.FunctionID)       '    = SpecialItem.TaxID
            .PrintPriorityID = 1

        End With

        'EndOfItem
        currentTable.ReferenceSIN = currentTable.SIN
        currentTable.MiddleOfOrder = False
        If Not currentTable.Quantity = 1 Then
            currentTable.Quantity = 1
            Me.testgridview.ChangeCourseButton(currentTable.Quantity)
        End If
        currentTable.SIN += 1

        AddItemToOrderTable(currentItem)
        Me.testgridview.CalculateSubTotal()
        If currentTable.MarkForNextCustomer = True Then
            currentTable.CustomerNumber = currentTable.NextCustomerNumber
            ChangeCustomerButtonColor(c9)
            If currentTable.MarkForNewCustomerPanel = True Then
                AddCustomerPanel()
            End If
            currentTable.MarkForNewCustomerPanel = False
            currentTable.MarkForNextCustomer = False
        End If

    End Sub


    Private Sub BtnModifierRepeat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModifierRepeat.Click
        UserControlHit()
        If EndOfItem(True) = False Then Exit Sub

        Dim lastOrderNumber As Int64

        lastOrderNumber = DetermineLastOrderNumber()

        If lastOrderNumber = 0 Then
            info = New DataSet_Builder.Information_UC("There are no previous orders for this Table")
            info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            Me.Controls.Add(info)
            info.BringToFront()
            '            MsgBox()
            Exit Sub
        End If

        FillRepeatOrderDataTable(lastOrderNumber)

        DisplayRepeatOrder(True)

    End Sub


    '*** this is for when we want to list all open orders
    Private Function CheckForOpenOrderDetail()
        Dim lon As Int64
        Dim dtr As SqlClient.SqlDataReader
        Dim i As Integer
        Dim oRow As DataRow
        Dim rowCount As Integer

        rowCount = dsOrder.Tables("OrderDetail").Rows.Count

        'this will give LIFO
        'we will have to show all orders at sometime
        If rowCount > 0 Then
            For i = 0 To (rowCount - 1) 'To 0 Step -1
                oRow = dsOrder.Tables("OrderDetail").Rows(i)
                If oRow("OrderFilled") Is DBNull.Value Then 'And oRow("isMainCourse") = True Then
                    If currentServer.Bartender = True Then
                        If oRow("NumberOfDinners") + oRow("NumberOfApps") > 0 Then
                            lon = oRow("OrderNumber")
                            Exit For
                        End If
                    Else
                        lon = oRow("OrderNumber")
                        Exit For
                    End If
                End If
            Next
        Else
            lon = 0
        End If

        Return lon

    End Function
    Private Function DetermineLastOrderNumber()
        Dim lon As Int64

        If dsOrder.Tables("OrderDetail").Rows.Count > 0 Then
            lon = dsOrder.Tables("OrderDetail").Rows(dsOrder.Tables("OrderDetail").Rows.Count - 1)("OrderNumber")
        Else
            lon = 0
        End If

        Return lon

        Exit Function

        'below is 222
        Dim dtr As SqlClient.SqlDataReader

        If mainServerConnected = True Then
            Try
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                Dim cmd = New SqlClient.SqlCommand("SELECT MAX(OrderNumber) ordNum FROM ExperienceStatusChange WHERE ExperienceNumber ='" & currentTable.ExperienceNumber & "'", sql.cn)
                dtr = cmd.executereader
                dtr.Read()

                '   ***********************  change all datareaders to this
                If dtr.IsDBNull(0) = False Then     'dtr.HasRows Then
                    lon = (dtr("ordNum"))
                Else
                    '   this means we did not have an order sent to the kitchen for this experience
                    lon = 0
                End If
                dtr.Close()
                sql.cn.Close()

            Catch ex As Exception
                CloseConnection()
                ServerJustWentDown()
                '            lon = DetermineLastOrderNumberWhenDown()

            End Try
        Else
            '          lon = DetermineLastOrderNumberWhenDown()

        End If

        Return lon

    End Function

    Private Function DetermineLastOrderNumberWhenDown222()
        Dim lonDOWN As Integer

        Dim copyRows As DataRow()
        Dim bRow As DataRow

        copyRows = dsBackup.Tables("ESCTerminal").Select("ExperienceNumber = '" & currentTable.ExperienceNumber & "'")

        '   finds the max value
        If Not copyRows Is Nothing Then
            For Each bRow In copyRows
                If bRow("OrderNumber") > lonDOWN Then
                    lonDOWN = bRow("OrderNumber")
                End If
            Next
        End If

        Return lonDOWN

    End Function

    Private Sub FillRepeatOrderDataTable(ByVal LastOrderNumber As Int64)

        '     dvRepeat = New DataView
        Dim vRow As DataRowView
        Dim newRow As DataRow

        With dvRepeat
            .Table = dsOrder.Tables("OpenOrders")
            .RowFilter = "ExperienceNumber = '" & currentTable.ExperienceNumber & "' AND OrderNumber = '" & LastOrderNumber & "'"
            .Sort = "sii, sin"
        End With

    End Sub

    Private Sub DisplayRepeatOrder(ByVal forRepeat As Boolean)

        Me.repeatOrderUserControl = New LastOrder_UC(forRepeat)
        Me.repeatOrderUserControl.Location = New Point((Me.Width - Me.repeatOrderUserControl.Width) / 2, (Me.Height - Me.repeatOrderUserControl.Height) / 2)

        Me.Controls.Add(Me.repeatOrderUserControl)
        Me.repeatOrderUserControl.BringToFront()

    End Sub

    Private Sub AcceptOrderDelivered() Handles repeatOrderUserControl.OrderDelivered

        Dim oRow As DataRow
        Dim allOrdersFilled As Boolean = True

        For Each oRow In dsOrder.Tables("OrderDetail").Rows
            If oRow("OrderFilled") Is DBNull.Value Then
                If Not oRow("OrderNumber") = repeatOrderUserControl.OrderNumber Then
                    allOrdersFilled = False
                Else
                    oRow("OrderFilled") = Now
                End If
            End If
        Next

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If Not oRow("OrderNumber") Is DBNull.Value Then
                    If oRow("OrderNumber") = repeatOrderUserControl.OrderNumber Then
                        oRow("ItemStatus") = 4
                    End If
                End If
            End If
        Next

        If allOrdersFilled = True Then
            GenerateOrderTables.ChangeStatusInDataBase(6, Nothing, 0, Nothing, Nothing, Nothing)
        End If

    End Sub

    Private Sub AcceptRepeatOrder() Handles repeatOrderUserControl.AcceptRepeat
        UserControlHit()

        Dim repeatedItem As New SelectedItemDetail

        For Each repeatedItem In Me.repeatOrderUserControl.currentRepeatOrderCollection
            With repeatedItem
                If currentTable.IsTabNotTable = False Then
                    .Table = currentTable.TableNumber
                Else
                    .Tab = currentTable.TabID
                End If
                .SIN = currentTable.SIN
                .si2 = currentTable.si2
            End With

            If repeatedItem.SII = -1 Then
                '   we coded it like this for a modifier
                repeatedItem.SII = currentTable.ReferenceSIN
            Else
                repeatedItem.SII = currentTable.SIN()
                currentTable.ReferenceSIN = currentTable.SIN
                '      EndOfItem()
                currentTable.MiddleOfOrder = False
                If Not currentTable.Quantity = 1 Then
                    currentTable.Quantity = 1
                    Me.testgridview.ChangeCourseButton(currentTable.Quantity)
                End If
            End If

            currentTable.SIN += 1

            AddItemToOrderTable(repeatedItem)
            Me.testgridview.CalculateSubTotal()
            If currentTable.MiddleOfOrder = False Then
                If currentTable.MarkForNextCustomer = True Then
                    currentTable.CustomerNumber = currentTable.NextCustomerNumber
                    ChangeCustomerButtonColor(c9)
                    If currentTable.MarkForNewCustomerPanel = True Then
                        AddCustomerPanel()
                    End If
                    currentTable.MarkForNewCustomerPanel = False
                    currentTable.MarkForNextCustomer = False
                End If
            End If
        Next

    End Sub


    Private Sub BtnModifierOnFly_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModifierOnFly.Click
        UserControlHit()

        Dim rowNum As Integer = OpenOrdersCurrencyMan.Position  'gridViewOrder.CurrentCell.RowNumber
        Dim oRow As DataRow
        Dim valueSIN As Integer
        Try
            valueSIN = CType(Me.testgridview.gridViewOrder.Item(rowNum, 1), Integer)
        Catch ex As Exception
            Exit Sub
        End Try

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

        If oRow("ItemStatus") < 2 Then
            oRow("TerminalName") = oRow("TerminalName") & "     ** ON FLY  **"
            oRow("ChitName") = oRow("ChitName") & "     ** ON FLY  **"
        Else
            info = New DataSet_Builder.Information_UC("This Item Has already been sent to the Kitchen.")
            info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            Me.Controls.Add(info)
            info.BringToFront()
            '      MsgBox()
        End If

    End Sub

    Private Sub BtnModifierNoMake_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModifierNoMake.Click
        ' change this to the same as On Fly     ?????
        UserControlHit()

        Dim rowNum As Integer = OpenOrdersCurrencyMan.Position  'gridViewOrder.CurrentCell.RowNumber
        Dim oRow As DataRow
        Dim valueSIN As Integer
        Try
            valueSIN = CType(Me.testgridview.gridViewOrder.Item(rowNum, 1), Integer)
        Catch ex As Exception
            Exit Sub
        End Try

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

        If oRow("ItemStatus") > 1 Then
            info = New DataSet_Builder.Information_UC("This Item Has already been sent to the Kitchen.")
            info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            Me.Controls.Add(info)
            info.BringToFront()
            '    MsgBox
            Exit Sub
        Else
            oRow("TerminalName") = oRow("TerminalName") & "     ** NO Make  **"
            oRow("ChitName") = oRow("ChitName") & "     ** NO Make  **"
        End If

        Exit Sub

        '   not using below
        Dim currentitem As New SelectedItemDetail

        AssignReferenceSIN()

        With currentitem
            .Name = "      ***   No Make      Above   ***"
            .TerminalName = "      ***   No Make      Above   ***"
            .ChitName = "      ***   No Make      Above   ***"
            .Price = 0
            If currentTable.IsTabNotTable = False Then
                .Table = currentTable.TableNumber
            Else
                .Tab = currentTable.TabID
            End If
            .Check = currentTable.CheckNumber
            .Customer = currentTable.CustomerNumber
            .SII = currentTable.ReferenceSIN
            .SIN = currentTable.SIN
            .si2 = currentTable.si2
            .Category = -2
            '            .FunctionID = 10
        End With
        currentTable.SIN += 1
        AddItemToOrderTable(currentitem)

    End Sub

    Private Sub BtnModifierOnSide_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModifierOnSide.Click
        UserControlHit()

        Dim currentitem As New SelectedItemDetail
        Dim rowNum As Integer = OpenOrdersCurrencyMan.Position  'gridViewOrder.CurrentCell.RowNumber
        Dim oRow As DataRow
        Dim valueSIN As Integer
        Try
            valueSIN = CType(Me.testgridview.gridViewOrder.Item(rowNum, 1), Integer)

        Catch ex As Exception
            Exit Sub
        End Try

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

        If oRow("ItemStatus") < 2 Then
            oRow("TerminalName") = oRow("TerminalName") & "     ** On Side  **"
            oRow("ChitName") = oRow("ChitName") & "     ** On Side  **"
        Else
            info = New DataSet_Builder.Information_UC("This Item Has already been sent to the Kitchen.")
            info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            Me.Controls.Add(info)
            info.BringToFront()
            '        MsgBox()
        End If

    End Sub

    Private Sub BtnModifierNoCharge_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModifierNoCharge.Click
        UserControlHit()

        Dim currentitem As New SelectedItemDetail
        Dim rowNum As Integer = OpenOrdersCurrencyMan.Position  'gridViewOrder.CurrentCell.RowNumber
        Dim oRow As DataRow
        Dim valueSIN As Integer
        Dim valueSII As Integer
        '   Dim valueSI2
        Try
            valueSIN = CType(Me.testgridview.gridViewOrder.Item(rowNum, 1), Integer)
            valueSII = CType(Me.testgridview.gridViewOrder.Item(rowNum, 2), Integer)
            '       valueSI2 = CType(Me.testgridview.gridViewOrder.Item(rowNum, 3), Integer)
        Catch ex As Exception
            Exit Sub
        End Try

        If valueSIN = valueSII Then
            info = New DataSet_Builder.Information_UC("Can Not Force **No Charge** On This Item")
            info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            Me.Controls.Add(info)
            info.BringToFront()
            '    MsgBox()
        Else
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

            '       For Each oRow In dsOrder.Tables("OpenOrders").Rows
            '      If oRow("sin") = valueSIN Then
            '     oRow("ItemName") = oRow("ItemName") & "     ** No Charge  **"
            oRow("TerminalName") = oRow("TerminalName") & "     ** No Charge  **"
            oRow("ForceFreeCode") = 0   'oRow("Price")
            oRow("Price") = 0
            Me.testgridview.CalculateSubTotal()
            ' End If
            '    Next
        End If
        '   I should record all forced no charges

    End Sub
    Private Sub ClearingControls() Handles testgridview.ClearControls

        '      Me.pnlOrderModifier.Visible = False
        PutUsInNormalMode()
        If currentTable.MiddleOfOrder = True Then
            '     Me.pnlOrderDrink.Visible = False
            '    Me.pnlOrderModifier.Visible = False
            ClearOrderPanels()

        End If
        '   can add special food and modify order here

    End Sub

    Private Sub ResetPizzaControls() Handles testgridview.ResetPizzaRoutine

        currentTable.si2 = 0
        currentTable.Tempsi2 = 0
        currentTable.IsPizza = False
        Me.pnlPizzaSplit.Visible = False
        Me.pnlWineParring.Visible = True

    End Sub

    Private Sub AssignReferenceSIN()
        Dim rowNum As Integer = OpenOrdersCurrencyMan.Position  'gridViewOrder.CurrentCell.RowNumber
        Dim valueSII As Integer

        Try
            valueSII = CType(Me.testgridview.gridViewOrder.Item(rowNum, 2), Integer)
            currentTable.CustomerNumber = CInt(Me.testgridview.gridViewOrder.Item(rowNum, 4))
            currentTable.CourseNumber = CInt(Me.testgridview.gridViewOrder.Item(rowNum, 5))
            currentTable.si2 = CType(Me.testgridview.gridViewOrder.Item(rowNum, 3), Integer)
        Catch ex As Exception
            Exit Sub
        End Try

        currentTable.ReferenceSIN = valueSII


    End Sub


    Private Sub drinkDouble_Click222(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles drinkDouble.Click
        UserControlHit()

        Dim currentitem As New SelectedItemDetail

        With currentitem
            .Name = "      ***   Double     Double  ***"
            .TerminalName = "      ***   Double     Double  ***"
            .ChitName = "      ***   Double     Double  ***"
            .Price = ds.Tables("DrinkModifiers").Rows(0)("DrinkPrice") * currentTable.Quantity
            .ID = ds.Tables("DrinkModifiers").Rows(0)("DrinkID")

            '           .TaxID = ds.Tables("Drink").Rows(0)("TaxID")
        End With

        AddDrinkModifier(currentitem)

    End Sub

    Private Sub drinkRocks_Click222(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles drinkRocks.Click
        UserControlHit()

        Dim currentitem As New SelectedItemDetail

        With currentitem
            .Name = "      ***   Rocks      Rocks   ***"
            .TerminalName = "      ***   Rocks      Rocks   ***"
            .ChitName = "      ***   Rocks      Rocks   ***"
            .Price = ds.Tables("DrinkModifiers").Rows(1)("DrinkPrice") * currentTable.Quantity
            .ID = ds.Tables("DrinkModifiers").Rows(1)("DrinkID")
            '          .TaxID = ds.Tables("Drink").Rows(1)("TaxID")
        End With

        AddDrinkModifier(currentitem)
    End Sub

    Private Sub drinkUp_Click222(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles drinkUp.Click
        UserControlHit()

        Dim currentitem As New SelectedItemDetail

        With currentitem
            .Name = "      ***   Up            Up        ***"
            .TerminalName = "      ***   Up            Up        ***"
            .ChitName = "      ***   Up            Up        ***"
            .Price = ds.Tables("DrinkModifiers").Rows(2)("DrinkPrice") * currentTable.Quantity
            .ID = ds.Tables("DrinkModifiers").Rows(2)("DrinkID")
            '          .TaxID = ds.Tables("Drink").Rows(2)("TaxID")
        End With

        AddDrinkModifier(currentitem)
    End Sub

    Private Sub drinkTall_Click222(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles drinkTall.Click
        UserControlHit()

        Dim currentitem As New SelectedItemDetail

        With currentitem
            .Name = "      ***   Tall           Tall       ***"
            .TerminalName = "      ***   Tall           Tall       ***"
            .ChitName = "      ***   Tall           Tall       ***"
            .Price = ds.Tables("DrinkModifiers").Rows(3)("DrinkPrice") * currentTable.Quantity
            .ID = ds.Tables("DrinkModifiers").Rows(3)("DrinkID")
            '          .TaxID = ds.Tables("Drink").Rows(3)("TaxID")
        End With

        AddDrinkModifier(currentitem)
    End Sub

    Private Sub drinkSplash_Click222(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles drinkSplash.Click
        UserControlHit()

        Dim currentitem As New SelectedItemDetail

        With currentitem
            .Name = "      ***   Splash      Splash   ***"
            .TerminalName = "      ***   Splash      Splash   ***"
            .ChitName = "      ***   Splash      Splash   ***"
            .Price = ds.Tables("DrinkModifiers").Rows(4)("DrinkPrice") * currentTable.Quantity
            .ID = ds.Tables("DrinkModifiers").Rows(4)("DrinkID")
            '          .TaxID = ds.Tables("Drink").Rows(4)("TaxID")
        End With

        AddDrinkModifier(currentitem)
    End Sub

    Private Sub drinkCall_Click()   'ByVal sender As Object, ByVal e As System.EventArgs) Handles drinkCall.Click
        UserControlHit()

        Dim rowNum As Integer = OpenOrdersCurrencyMan.Position
        Dim oRow As DataRow
        Dim dRow As DataRow
        Dim liquorTypeID As Integer
        Dim funID As Integer
        Dim funGroup As Integer
        Dim funFlag As String
        Dim preID As Integer
        Dim isALiquorType As Boolean
        Dim objButton As New OrderButton("10")

        Dim valueSIN As Integer
        Dim valueSII As Integer
        Try
            valueSIN = CType(Me.testgridview.gridViewOrder.Item(rowNum, 1), Integer)
            valueSII = CType(Me.testgridview.gridViewOrder.Item(rowNum, 2), Integer)
        Catch ex As Exception
            Exit Sub
        End Try

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


        If oRow("DrinkID") > 0 Then
            For Each dRow In ds.Tables("Drink").Rows

                If dRow("DrinkID") = oRow("DrinkID") Then
                    If dRow("LiquorTypeID") > 0 Then
                        'liquor typeID is from drinkTable
                        liquorTypeID = dRow("LiquorTypeID")
                        'funGroup and Flag are from OpenOrders
                        funID = oRow("FunctionID")
                        funGroup = oRow("FunctionGroupID")
                        funFlag = oRow("FunctionFlag")
                        preID = oRow("DrinkCategoryID")
                        isALiquorType = True
                    End If
                    Exit For
                End If
            Next
        End If

        If isALiquorType = True Then
            currentTable.MiddleOfOrder = True
            currentTable.ReferenceSIN = valueSII
            currentTable.OrderingStatus = "Call"
            objButton.CategoryID = liquorTypeID
            objButton.Functions = funID
            objButton.FunctionGroup = funGroup
            objButton.FunctionFlag = funFlag
            '       objButton.DrinkAdds = True
            previousCategory = preID
            PopulateDrinkSubCategory(objButton)
            '            MsgBox(liquorTypeID)
            ' perform routing to select a brand from this liquor type
        End If

    End Sub

    Private Sub AddDrinkModifier(ByVal currentitem As SelectedItemDetail)
        AssignReferenceSIN()

        With currentitem
            '         If currentTable.IsTabNotTable = False Then
            '        .Table = currentTable.TableNumber
            '       Else
            '          .Table = currentTable.TabID
            '     End If
            .Check = currentTable.CheckNumber
            .FunctionID = 6
            .FunctionGroup = 4
            .FunctionFlag = "D"
            .Category = -1
            .ID = -1
            .Customer = currentTable.CustomerNumber
            .SII = currentTable.ReferenceSIN
            .SIN = currentTable.SIN
            .si2 = currentTable.si2
        End With
        currentTable.SIN += 1
        AddItemToOrderTable(currentitem)
        Me.testgridview.CalculateSubTotal()

    End Sub

    Private Sub WineParring_RecipeClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblRecipe.Click
        UserControlHit()

        Me.lblRecipe.Visible = False
        Me.lblWineParring.Visible = True

    End Sub

    Private Sub WineParring_WineClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblWineParring.Click
        UserControlHit()

        Me.lblRecipe.Visible = True
        Me.lblWineParring.Visible = False

    End Sub

    Private Sub FullPizza_Clicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles onFullPizza.Click, testgridview.OnFullPizza
        UserControlHit()
        Dim gridNumberCount As Integer

        Me.pnlOnFullPizza.BackColor = c18
        Me.pnlOnFirstHalf.BackColor = c3
        Me.pnlOnSecondHalf.BackColor = c3
        currentTable.si2 = 1
        onFullPizza.SelectedIndex = -1

        gridNumberCount = PizzaPanelTest("Main")    'this is just to adjust gridvieworder
        If gridNumberCount > 0 Then
            Me.testgridview.gridViewOrder.CurrentRowIndex = gridNumberCount
        End If

        '    currentTable.ReferenceSIN = currentTable.TempReferenceSIN
        '   currentTable.SecondRound = True
        '     GTCIndex = -1

    End Sub

    Private Sub FirstHalfPizza_Clicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles onFirstHalf.Click
        UserControlHit()
        Dim gridNumberCount As Integer

        Me.pnlOnFullPizza.BackColor = c3
        Me.pnlOnFirstHalf.BackColor = c18
        Me.pnlOnSecondHalf.BackColor = c3
        currentTable.si2 = 2
        onFirstHalf.SelectedIndex = -1

        gridNumberCount = PizzaPanelTest("First")
        If gridNumberCount > 0 Then
            Me.testgridview.gridViewOrder.CurrentRowIndex = gridNumberCount
        End If

        '      currentTable.SecondRound = True

    End Sub


    Private Sub SecondHalfPizza_Clicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles onSecondHalf.Click
        UserControlHit()
        Dim gridNumberCount As Integer

        Me.pnlOnFullPizza.BackColor = c3
        Me.pnlOnFirstHalf.BackColor = c3
        Me.pnlOnSecondHalf.BackColor = c18
        currentTable.si2 = 3
        onSecondHalf.SelectedIndex = -1

        gridNumberCount = PizzaPanelTest("Second")
        If gridNumberCount > 0 Then
            Me.testgridview.gridViewOrder.CurrentRowIndex = gridNumberCount
        End If

        '      currentTable.SecondRound = True

    End Sub


    Friend Function PizzaPanelTest(ByVal whichPizzaPanel As String)
        Dim oRow As DataRow
        Dim vRow As DataRowView
        Dim pizzaPanelShowing As Boolean
        Dim gridNumberCount As Integer

        If whichPizzaPanel = "Main" Then
            pizzaPanelShowing = True    'there is no panel to add, just adjusting datavieworder
            For Each vRow In Me.testgridview.gridViewOrder.DataSource
                gridNumberCount += 1
                If vRow("si2") = 1 And vRow("sii") = currentTable.TempReferenceSIN Then 'And vRow("ItemID") = 0 Then
                    Exit For
                End If
            Next
        ElseIf whichPizzaPanel = "First" Then
            For Each vRow In Me.testgridview.gridViewOrder.DataSource
                gridNumberCount += 1
                If vRow("si2") = 2 And vRow("sii") = currentTable.TempReferenceSIN And vRow("ItemID") = 0 Then
                    pizzaPanelShowing = True
                    Exit For
                End If
            Next

            '         For Each oRow In dsOrder.Tables("OpenOrders").Rows
            '         If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
            '         gridNumberCount += 1
            '         If oRow("si2") = 2 And oRow("ItemID") = 0 Then
            '        pizzaPanelShowing = True
            '        Exit For
            '    End If
            '   End If
            '      Next
        ElseIf whichPizzaPanel = "Second" Then
            For Each vRow In Me.testgridview.gridViewOrder.DataSource 'currentdataview 'dvOrder '
                gridNumberCount += 1
                If vRow("si2") = 3 And vRow("sii") = currentTable.TempReferenceSIN And vRow("ItemID") = 0 Then
                    pizzaPanelShowing = True
                    Exit For
                End If
            Next
            '      For Each oRow In dsOrder.Tables("OpenOrders").Rows
            '            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
            '            gridNumberCount += 1
            '           If oRow("si2") = 3 And oRow("ItemID") = 0 Then
            '          pizzaPanelShowing = True
            '         Exit For
            '        End If
            '       End If
            '  Next
        End If

        If pizzaPanelShowing = False Then
            currentTable.TempReferenceSIN = vRow("sii") 'the first time won't matter
            '                                             b/c we want to add a panel
            Dim currentItem As SelectedItemDetail = New SelectedItemDetail
            Dim custNumString As String = "               " & whichPizzaPanel & " Half"

            With currentItem
                .Check = currentTable.CheckNumber
                .Customer = currentTable.CustomerNumber
                .Course = currentTable.CourseNumber
                .SIN = currentTable.SIN
                .SII = currentTable.ReferenceSIN
                .si2 = currentTable.si2
                .ID = 0
                .FunctionFlag = "N"
                .Name = custNumString
                .TerminalName = custNumString
                .ChitName = custNumString
                .Price = Nothing
                .Category = Nothing
            End With

            currentTable.SIN += 1            '********???????????????????
            If dvOrder.Count > 0 Then
                PopulateDataRowForOpenOrder(currentItem)
            Else
                '444     DisposeDataViewsOrder()
                PopulateDataRowForOpenOrder(currentItem)
                '444     CreateDataViewsOrder()
            End If
            gridNumberCount = 0

        End If

        Return gridNumberCount

    End Function
    Private Sub checkNumberButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles testgridview.CloseCheck 'checkNumberButton.Click

        If EndOfItem(False) = False Then Exit Sub

        If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            orderInactiveTimer.Stop()
            RemoveHandler orderInactiveTimer.Tick, AddressOf OrderInactiveScreenTimeout
        End If
        '     If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
        '     AdjustOpenOrderPosition()
        '    GenerateOrderTables.SaveOpenOrderData()
        '   End If
        '        GenerateOrderTables.PopulatePaymentsAndCredits(currentTable.ExperienceNumber)
        '     time1 = Now


        RaiseEvent ClosingCheck()

        '    LeaveAndSave()
        '     ActiveSplit = New SplitChecks           'currentTable._checkCollection) '(cm, empID, tableNumber)

        '      Me.Hide()
        '    ActiveSplit.Show()
        '    time2 = Now
        '   timeDiff = time2.Subtract(time1)
        '  MsgBox(timeDiff.ToString)
    End Sub

    Friend Sub SplitCheckClosed() 'Handles ActiveSplit.SplitCheckClosing


        '  Me.Show()
        '   do not want to reset timer, this way it times out fast ????? 
        '   the only way it reach this point was inactivity in closing the check
        orderTimeoutCounter = 1
        If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            AddHandler orderInactiveTimer.Tick, AddressOf OrderInactiveScreenTimeout
            orderInactiveTimer.Start()
        End If

        Me.testgridview.gridByCheck = True
        UpdateDataViewsByCheck()
        Me.testgridview.UpdateCheckNumberButton()
        Me.testgridview.CalculateSubTotal()

        '   *************** not sure if right
        '   do we need this or always do?????
        '        If activeCheckChanged = True Then
        '        UpdateCheckNumberButton()
        '        UpdateDataViewsByCheck()
        '       CalculateSubTotal()
        ''       activeCheckChanged = False
        '       End If


    End Sub


    Private Sub ApplingSplitCheckSecondStep222(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles ActiveSplit.ApplySplitCheck

        '   maybe, place new price on the original check
        '   eliminate the original check for the collection
        '   then place the split info on new checks w/ new sin's

        Dim oRow As DataRow
        Dim eachCheck As SplittingCheck
        Dim currentItem As SelectedItemDetail = New SelectedItemDetail

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("sii") = ActiveSplit.splitItemSIN Then
                    If oRow("sin") = ActiveSplit.splitItemSIN Then
                        '   this determines which check in the collection is the original
                        For Each eachCheck In ActiveSplit.CurrentSplittingChecks
                            If oRow("CheckNumber") = eachCheck.CheckNumber Then
                                oRow("Price") = eachCheck.CheckAmount
                                oRow("TaxPrice") = DetermineTaxPrice(oRow("TaxID"), eachCheck.CheckAmount)
                                oRow("Quantity") = eachCheck.CheckQuantity

                                'not sure about table
                                currentItem.ItemPrice = oRow("ItemPrice")
                                currentItem.ID = oRow("ItemID")
                                currentItem.Name = oRow("ItemName")
                                currentItem.TerminalName = oRow("TerminalName")
                                currentItem.ChitName = oRow("ChitName")
                                currentItem.TaxID = oRow("TaxID")
                                currentItem.OrderNumber = oRow("OrderNumber")

                                currentItem.Course = oRow("CourseNumber")
                                ' .Quantity = currentTable.Quantity
                                currentItem.FunctionID = oRow("FunctionID")
                                currentItem.FunctionGroup = oRow("FunctionGroupID")
                                currentItem.ItemStatus = oRow("ItemStatus")
                                currentItem.RoutingID = oRow("RoutingID")

                                If oRow("CategoryID") > 0 Then
                                    currentItem.Category = oRow("CategoryID")
                                    currentItem.FunctionFlag = oRow("FunctionFlag")
                                    currentItem.ID = oRow("FoodID")
                                ElseIf oRow("DrinkCategoryID") > 0 Then
                                    currentItem.Category = oRow("DrinkCategoryID")
                                    currentItem.FunctionFlag = "D"
                                    currentItem.ID = oRow("DrinkID")
                                End If

                                Exit For    'this sould keep each check the same ??
                            End If
                        Next
                        ActiveSplit.CurrentSplittingChecks.Remove(eachCheck)
                    Else    'this is for sub-items
                        oRow("Price") = 0
                    End If
                End If
            End If
        Next


        For Each eachCheck In ActiveSplit.CurrentSplittingChecks
            currentTable.SIN += 1       'we need to add an extra (so we have room for cust number)
            currentItem.Check = eachCheck.CheckNumber
            currentItem.Price = eachCheck.CheckAmount
            currentItem.Quantity = eachCheck.CheckQuantity
            currentItem.SIN = currentTable.SIN
            currentItem.SII = currentTable.SIN
            currentItem.si2 = currentTable.si2
            currentItem.Table = currentTable.TableNumber
            currentItem.Customer = 1        'should ask for this if multi cust on second check

            AddItemToOrderTable(currentItem)
            currentTable.SIN += 1
        Next

        '      AdjustOpenOrderPosition()
        '     GenerateOrderTables.SaveOpenOrderData()

        '   reflects the change
        ResetSplitGrids222()


    End Sub

    Private Sub ResetSplitGrids222()

        ActiveSplit.sgp1.splitGrid.Items.Clear()
        ActiveSplit.sgp2.splitGrid.Items.Clear()
        ActiveSplit.sgp3.splitGrid.Items.Clear()
        ActiveSplit.sgp4.splitGrid.Items.Clear()
        ActiveSplit.sgp5.splitGrid.Items.Clear()
        ActiveSplit.sgp6.splitGrid.Items.Clear()
        ActiveSplit.sgp7.splitGrid.Items.Clear()
        ActiveSplit.sgp8.splitGrid.Items.Clear()
        ActiveSplit.sgp9.splitGrid.Items.Clear()
        ActiveSplit.sgp10.splitGrid.Items.Clear()

        ActiveSplit.sgp1.CreateSplitDataView(1)
        ActiveSplit.sgp2.CreateSplitDataView(2)
        ActiveSplit.sgp3.CreateSplitDataView(3)
        ActiveSplit.sgp4.CreateSplitDataView(4)
        ActiveSplit.sgp5.CreateSplitDataView(5)
        ActiveSplit.sgp6.CreateSplitDataView(6)
        ActiveSplit.sgp7.CreateSplitDataView(7)
        ActiveSplit.sgp8.CreateSplitDataView(8)
        ActiveSplit.sgp9.CreateSplitDataView(9)
        ActiveSplit.sgp10.CreateSplitDataView(10)


    End Sub

    Private Sub AdjustOpenOrderPosition()
        If OpenOrdersCurrencyMan.Position = 0 Then
            OpenOrdersCurrencyMan.Position += 1
            OpenOrdersCurrencyMan.Position -= 1
        Else
            OpenOrdersCurrencyMan.Position -= 1
            OpenOrdersCurrencyMan.Position += 1
        End If

    End Sub

    Private Sub DisplayingMessage(ByVal msg As Object) Handles testgridview.DisplayInfo

        info = New DataSet_Builder.Information_UC(msg.ToString)
        info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)

        Me.Controls.Add(info)
        info.BringToFront()

    End Sub
 



End Class
