Imports DataSet_Builder

Public Class SplitChecks
    Inherits System.Windows.Forms.UserControl

    '   Dim sql As New DataSet_Builder.SQLHelper(connectserver)

    '   Dim splitPanel2 As Panel
    Dim IsFromManager As Boolean
    Dim WithEvents transferCheckButton As Button
    Dim WithEvents splitItemButton As Button
    Dim WithEvents closeExperienceButton As Button  'one check button
    Dim WithEvents changeNumberOfGridsButton As Button
    Friend splitItemSIN As Integer
    Friend splitItemName As String
    Dim actionPanel As Panel
    Dim actionPanelLabel As Label
    Friend splitItemQuantity As Integer
    Friend splitItemInvMultiplier As Decimal
    Dim numberGridsInView As Integer

    Dim WithEvents splitItemPanel As SplitItemUserControl
    Friend CurrentSplittingChecks As SplittingCheckCollection
    Dim WithEvents transCheck As Manager_Transfer_UC
    Friend WithEvents _closeCheck As CloseCheck
    '  Dim WithEvents closeCreditCardAuth As CloseManualAuth
    Dim goingToSelectedCheck As Boolean
    Dim RemainingBalancesZero As Boolean

    '    Dim actionPanelSplit As Panel
    '    Dim actionPanelSplitLabel As Label
    '    Dim actionPanelRemainingLabel As Label
    '    Dim actionPanelRemaining As TextBox
    Dim WithEvents transferListView As ListView
    Dim nameColumn As New ColumnHeader
    '    Dim WithEvents splitListView As ListView
    '    Dim checkNameColumn As New ColumnHeader
    '   Dim checkAmountColumn As New ColumnHeader

    '   testing
    '   Dim grdSplitItem As DataGrid

   
    '    Dim CurrentSplittingChecks As new SplittingCheckCollection
    Dim checksSplitting(1) As SplittingCheck

    '    Private splitInactiveTimer As Timer
    Dim splitTimeoutCounter As Integer
   
    Friend WithEvents sgp1 As SplitGridPanel
    Friend WithEvents sgp2 As SplitGridPanel
    Friend WithEvents sgp3 As SplitGridPanel
    Friend WithEvents sgp4 As SplitGridPanel
    Friend WithEvents sgp5 As SplitGridPanel
    Friend WithEvents sgp6 As SplitGridPanel
    Friend WithEvents sgp7 As SplitGridPanel
    Friend WithEvents sgp8 As SplitGridPanel
    Friend WithEvents sgp9 As SplitGridPanel
    Friend WithEvents sgp10 As SplitGridPanel

    '   flag of active checks
    '444    Dim checkActive(10) As Boolean
    '   Dim check2 As Boolean
    '  Dim check3 As Boolean
    '    Dim check4 As Boolean
    '   Dim check5 As Boolean
    '  Dim check6 As Boolean
    '    Dim check7 As Boolean
    '   Dim check8 As Boolean
    '  Dim check9 As Boolean
    ' Dim check10 As Boolean

    Event SplitCheckClosing()
    '    Event ApplySplitCheck(ByVal sender As Object, ByVal e As System.EventArgs)
    Event ManagerClosing(ByVal fromManager As Boolean, ByVal emp As Employee, ByVal goingToSelectedCheck As Boolean)
    Event SelectedReOrder(ByVal dt As DataTable, ByVal tabTestNeeded As Boolean)
    Event SendOrder(ByVal alsoClose As Boolean)
    Event FireTabScreen(ByVal startInSearch As String, ByVal searchCriteria As String)
    Event MakeGiftAddingAmountTrue()
    Event MerchantAuthPayment(ByVal paymentID As Integer, ByVal justActive As Boolean)


#Region " Windows Form Designer generated code "

    Public Sub New() 'ByVal _isFromManager As Boolean, ByVal _goingToSelectedCheck As Boolean)
        MyBase.New()

        '   IsFromManager = _isFromManager
        '  goingToSelectedCheck = _goingToSelectedCheck

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        InitializeOther()       '_checks)

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
    Friend WithEvents splitPanel As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.splitPanel = New System.Windows.Forms.Panel
        Me.SuspendLayout()
        '
        'splitPanel
        '
        Me.splitPanel.Location = New System.Drawing.Point(0, 0)
        Me.splitPanel.Name = "splitPanel"
        Me.splitPanel.Size = New System.Drawing.Size(1024, 768)
        Me.splitPanel.TabIndex = 0
        '
        'SplitChecks
        '
        Me.BackColor = System.Drawing.Color.LightSlateGray
        Me.Controls.Add(Me.splitPanel)
        Me.Name = "SplitChecks"
        Me.Size = New System.Drawing.Size(1024, 768)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther()       'ByRef _checkCollection As CheckCollection)

        splitTimeoutCounter = 1
        '     splitInactiveTimer = New Timer
        AddHandler splitInactiveTimer.Tick, AddressOf MeTimeOutTick 'InactiveScreenTimeout
        splitInactiveTimer.Interval = timeoutInterval
        splitInactiveTimer.Start()

        Dim index As Integer
        '      Me.ClientSize = New Size(ssX, ssY)

        '      splitPanel2 = New Panel
        '     splitPanel2.Size = Me.ClientSize
        '    Me.Controls.Add(splitPanel2)

        transferCheckButton = New Button
        transferCheckButton.Size = New Size(ssX * 0.16, ssY * 0.07)
        transferCheckButton.Location = New Point(ssX * 0.22, ssY * 0.93)
        transferCheckButton.BackColor = c3
        Me.transferCheckButton.TextAlign = ContentAlignment.MiddleCenter
        Me.transferCheckButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        transferCheckButton.Text = "Transfer Check:  " & currentTable.CheckNumber
        splitPanel.Controls.Add(transferCheckButton)


        splitItemButton = New Button
        splitItemButton.Size = New Size(ssX * 0.16, ssY * 0.07)
        splitItemButton.Location = New Point(ssX * 0.42, ssY * 0.93)
        splitItemButton.BackColor = c3
        Me.splitItemButton.TextAlign = ContentAlignment.MiddleCenter
        Me.splitItemButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.splitItemButton.Text = "Split:  "
        splitPanel.Controls.Add(splitItemButton)

        closeExperienceButton = New Button
        closeExperienceButton.Size = New Size(ssX * 0.16, ssY * 0.07)
        closeExperienceButton.Location = New Point(ssX * 0.62, ssY * 0.93)
        closeExperienceButton.BackColor = c3
        Me.closeExperienceButton.TextAlign = ContentAlignment.MiddleCenter
        Me.closeExperienceButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.closeExperienceButton.Text = "One Check"
        splitPanel.Controls.Add(closeExperienceButton)

        changeNumberOfGridsButton = New Button
        changeNumberOfGridsButton.Size = New Size(ssX * 0.09, ssY * 0.07)
        changeNumberOfGridsButton.Location = New Point(ssX * 0.87, ssY * 0.93)
        changeNumberOfGridsButton.BackColor = c3
        Me.changeNumberOfGridsButton.TextAlign = ContentAlignment.MiddleCenter
        Me.changeNumberOfGridsButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.changeNumberOfGridsButton.Text = "Display"
        splitPanel.Controls.Add(changeNumberOfGridsButton)

        actionPanel = New Panel
        actionPanel.Size = New Size(300, 300)
        actionPanel.Location = New Point((Me.Width - actionPanel.Width) / 2, (Me.Height - actionPanel.Height) / 2)
        actionPanel.BackColor = c3
        actionPanel.BorderStyle = BorderStyle.Fixed3D

        actionPanelLabel = New Label
        actionPanelLabel.Text = "Transfer Check to:"
        actionPanelLabel.BackColor = c6
        actionPanelLabel.ForeColor = c3
        actionPanelLabel.TextAlign = ContentAlignment.MiddleCenter
        Me.actionPanelLabel.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        actionPanelLabel.Dock = DockStyle.Top
        actionPanel.Controls.Add(actionPanelLabel)

        transferListView = New ListView
        transferListView.Size = New Size(actionPanel.Width, actionPanel.Height - actionPanelLabel.Height)
        Me.transferListView.Location = New Point(0, actionPanelLabel.Height)
        Me.transferListView.BackColor = c3
        Me.transferListView.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.transferListView.Columns.Add(Me.nameColumn)
        Me.transferListView.HeaderStyle = ColumnHeaderStyle.None
        Me.transferListView.View = View.Details
        Me.nameColumn.Width = transferListView.Width - 30

        actionPanel.Controls.Add(Me.transferListView)
        actionPanel.Visible = False
        splitPanel.Controls.Add(actionPanel)

         If currentTable.NumberOfChecks > 4 Then
            numberGridsInView = 10
        Else
            numberGridsInView = 4
        End If

        InitializeSplitGridPanels()

        '444  DisplayCloseCheck()     'we are going straight to close check
        MyBase.Focus()

    End Sub

    Private Sub InitializeSplitGridPanels()

        sgp1 = New SplitGridPanel(1, numberGridsInView)
        splitPanel.Controls.Add(sgp1)
        sgp2 = New SplitGridPanel(2, numberGridsInView)
        splitPanel.Controls.Add(sgp2)
        sgp3 = New SplitGridPanel(3, numberGridsInView)
        splitPanel.Controls.Add(sgp3)
        sgp4 = New SplitGridPanel(4, numberGridsInView)
        splitPanel.Controls.Add(sgp4)

        sgp5 = New SplitGridPanel(5, numberGridsInView)
        sgp6 = New SplitGridPanel(6, numberGridsInView)
        sgp7 = New SplitGridPanel(7, numberGridsInView)
        sgp8 = New SplitGridPanel(8, numberGridsInView)
        sgp9 = New SplitGridPanel(9, numberGridsInView)
        sgp10 = New SplitGridPanel(10, numberGridsInView)

        If numberGridsInView = 4 Then Exit Sub

        splitPanel.Controls.Add(sgp5)
        splitPanel.Controls.Add(sgp6)
        splitPanel.Controls.Add(sgp7)
        splitPanel.Controls.Add(sgp8)
        splitPanel.Controls.Add(sgp9)
        splitPanel.Controls.Add(sgp10)

    End Sub


    '   combine
    Private Sub StartTimer(ByVal sender As Object, ByVal e As System.EventArgs) Handles _closeCheck.CloseGotoSplitting
        '   this is what we do when we close checkClose screen

        If currentTable.NumberOfChecks = 1 Then
        End If
        '     InitializeSplitGridPanels()

        _closeCheck.Visible = False
        '      Me.splitPanel.Enabled = True

        '     If _closeCheck.singleSplit = True Then
        '    PerformChangeOfGrids()
        '   _closeCheck.singleSplit = False
        '  End If


        Me.splitPanel.BringToFront()

        ResetTimer()

        'currently timer is never stopping
        '     AddHandler splitInactiveTimer.Tick, AddressOf MeTimeOutTick
        '    splitInactiveTimer.Start()

        RecaculatePriceAndTaxRemaining()
        '????????????????

    End Sub

    Private Sub ResetTimer()

        splitTimeoutCounter = 1

    End Sub

    Private Sub RePopulateSplitGrids()
       
        sgp1.CreateSplitDataView(1)
        sgp2.CreateSplitDataView(2)
        sgp3.CreateSplitDataView(3)
        sgp4.CreateSplitDataView(4)

        '    If numberGridsInView = 4 Then Exit Sub
        sgp5.CreateSplitDataView(5)
        sgp6.CreateSplitDataView(6)
        sgp7.CreateSplitDataView(7)
        sgp8.CreateSplitDataView(8)
        sgp9.CreateSplitDataView(9)
        sgp10.CreateSplitDataView(10)
    End Sub

    Private Sub RecaculatePriceAndTaxRemaining()

        sgp1.CalculatePriceTaxAndRemaining()
        sgp2.CalculatePriceTaxAndRemaining()
        sgp3.CalculatePriceTaxAndRemaining()
        sgp4.CalculatePriceTaxAndRemaining()

        If numberGridsInView = 4 Then Exit Sub
        sgp5.CalculatePriceTaxAndRemaining()
        sgp6.CalculatePriceTaxAndRemaining()
        sgp7.CalculatePriceTaxAndRemaining()
        sgp8.CalculatePriceTaxAndRemaining()
        sgp9.CalculatePriceTaxAndRemaining()
        sgp10.CalculatePriceTaxAndRemaining()

    End Sub
    Private Sub ItemMoving() Handles sgp1.ResetTimerFromPanel, sgp2.ResetTimerFromPanel, sgp3.ResetTimerFromPanel, sgp4.ResetTimerFromPanel, sgp5.ResetTimerFromPanel, sgp6.ResetTimerFromPanel, sgp7.ResetTimerFromPanel, sgp8.ResetTimerFromPanel, sgp9.ResetTimerFromPanel, sgp10.ResetTimerFromPanel
        ResetTimer()

    End Sub
    Private Sub ItemSelectedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sgp1.ItemSelected, sgp2.ItemSelected, sgp3.ItemSelected, sgp4.ItemSelected, sgp5.ItemSelected, sgp6.ItemSelected, sgp7.ItemSelected, sgp8.ItemSelected, sgp9.ItemSelected, sgp10.ItemSelected
        ResetTimer()

        Dim objSent As SplitGridPanel
        objSent = sender

        currentTable.CheckNumber = objSent.CheckIndex
        splitItemName = objSent.SelectedItemName
        splitItemSIN = objSent.SIN_Split

        transferCheckButton.Text = "Transfer Check:  " & objSent.CheckIndex

        With dvCloseCheck
            .Table = dsOrder.Tables("PaymentsAndCredits")
            .RowFilter = "CheckNumber = " & objSent.CheckIndex
        End With

        Me.splitItemButton.Text = "Split:  " & objSent.SelectedItemName
        '   sin dragsouce will give the "sin" so we can split check

    End Sub

    Private Sub StartCheckTransfer(ByVal sender As Object, ByVal e As System.EventArgs) Handles transferCheckButton.Click
        ResetTimer()

        Dim restrictToItemOnly As Boolean
        Dim oRow As DataRow

        actingManager = currentServer
        GenerateOrderTables.AssignManagementAuthorization(actingManager)

        If employeeAuthorization.OperationLevel < systemAuthorization.TransferCheck Then
            MsgBox(employeeAuthorization.FullName & " does not have Authorization")
            Exit Sub
            '          restrictToItemOnly = True
        End If


        If dvCloseCheck.Count = 0 Then
            transCheck = New Manager_Transfer_UC(splitItemSIN, splitItemName, currentTable.CheckNumber, currentTable.ExperienceNumber, False, restrictToItemOnly)
            transCheck.Location = New Point((Me.Width - transCheck.Width) / 2, (Me.Height - transCheck.Height) / 2)

            Me.Controls.Add(transCheck)
            transCheck.BringToFront()
        Else
            MsgBox("You may NOT transfer a check if a Payment has been applied.")
        End If

    End Sub

    Private Sub TransUC_Closed(ByVal releasingTable As Boolean) Handles transCheck.UpdatingCurrentTables
        goingToSelectedCheck = False

        If releasingTable = True Then
            CalculateClosingTotal()
            _closeCheck.releaseFlag = True
        End If
        SplitDisposeSelf()
        '     RaiseEvent UpdatingAfterTransfer()

    End Sub

    Private Sub OpenCloseCheck(ByVal sender As Object, ByVal e As System.EventArgs) Handles sgp1.ButtonCloseClick, sgp2.ButtonCloseClick, sgp3.ButtonCloseClick, sgp4.ButtonCloseClick, sgp5.ButtonCloseClick, sgp6.ButtonCloseClick, sgp7.ButtonCloseClick, sgp8.ButtonCloseClick, sgp9.ButtonCloseClick, sgp10.ButtonCloseClick

        ResetTimer()
        '      splitInactiveTimer.Stop()
        '     RemoveHandler splitInactiveTimer.Tick, AddressOf MeTimeOutTick

        Dim objGrid As PropertyAttributes 'SplitGridPanel
        objGrid = sender

        currentTable.CheckNumber = CInt(objGrid)
        '    MsgBox(currentTable.CheckNumber.GetType.ToString, , "here")
        '   currentTable.CheckNumber = 1

        _closeCheck.Visible = True
        _closeCheck.ReinitializeCloseCheck(True)
        _closeCheck.BringToFront()

        '        DisplayCloseCheck()

    End Sub

    Friend Sub DisplayCloseCheck(ByVal _isFromManager As Boolean) ', ByVal _goingToSelectedCheck As Boolean)

        'this step we do everytime we close

        IsFromManager = _isFromManager
        If IsFromManager = True Then
            goingToSelectedCheck = False
        Else
            goingToSelectedCheck = True
        End If
        RePopulateSplitGrids()
        RecaculatePriceAndTaxRemaining()

        Try
            '         splitInactiveTimer.Stop()
            '        RemoveHandler splitInactiveTimer.Tick, AddressOf MeTimeOutTick
        Catch ex As Exception
            '        MsgBox(ex.Message)
        End Try

        If Not _closeCheck Is Nothing Then
            _closeCheck.Visible = True
            _closeCheck.ReinitializeCloseCheck(True)    'False)
        Else
            _closeCheck = New CloseCheck(currentTable.CheckNumber) '(dvSplitCheck)
            _closeCheck.Location = New Point(0, 0)  '((Me.Width - Me._closeCheck.Width) / 2, (Me.Height - Me._closeCheck.Height) / 2)
            Me.Controls.Add(Me._closeCheck)
            Me._closeCheck.BringToFront()
        End If

        '     Me.splitPanel.Enabled = False

    End Sub

    ' testing for keyboard emulation of HID devices

    '   Friend Sub handlesSwipe(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles MyBase.KeyPress
    '       MsgBox(sender.ToString)
    '   End Sub
    '   Friend Sub handlesSwipe2(ByVal sender As Object, ByVal e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    '       MsgBox(sender.ToString)
    '  End Sub
    '
    '    Friend Sub handlesSwipe3(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles MyBase.KeyPress
    '       MsgBox(sender.ToString)
    '   End Sub

    Private Sub CheckSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles sgp1.ButtonSelectClick, sgp2.ButtonSelectClick, sgp3.ButtonSelectClick, sgp4.ButtonSelectClick, sgp5.ButtonSelectClick, sgp6.ButtonSelectClick, sgp7.ButtonSelectClick, sgp8.ButtonSelectClick, sgp9.ButtonSelectClick, sgp10.ButtonSelectClick
        ResetTimer()

        '        splitInactiveTimer.Stop()
        '       RemoveHandler splitInactiveTimer.Tick, AddressOf MeTimeOutTick

        Dim objGrid As PropertyAttributes 'SplitGridPanel
        objGrid = sender

        currentTable.CheckNumber = CInt(objGrid)
        '     activeCheck = objGrid
        activeCheckChanged = True
        goingToSelectedCheck = True
        '     _closeCheck.Visible = True

        SplitDisposeSelf()

        '     Me.Dispose()

    End Sub

    Private Sub SplitTimedOut()

        SplitDisposeSelf()

    End Sub

    Private Sub MeTimeOutTick(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles _closeCheck.DisposeSplitScreen

        splitTimeoutCounter += 1

        If splitTimeoutCounter = (companyInfo.timeoutMultiplier * 5) Then
            SplitDisposeSelf()
        End If

    End Sub

    Private Sub SplitDisposeSelf()

        splitInactiveTimer.Stop()
        RemoveHandler splitInactiveTimer.Tick, AddressOf MeTimeOutTick
        '444      tmrCardRead.Stop()
        '444  RemoveHandler tmrCardRead.Tick, AddressOf _closeCheck.readAuth.tmrCardRead_Tick

        '444      Dim doNotDisposeSelf As Boolean
        '444     doNotDisposeSelf = _closeCheck.CheckForUnappliedCredit222(False)       'false for removeCash
        '444    If doNotDisposeSelf = True Then Exit Sub

        '      GenerateOrderTables.UpdatePaymentsAndCredits()

        If RemainingBalancesZero = True And ds.Tables("RoutingChoice").Rows.Count > 0 Then
            '   Routing = 0 means no service printer
            ' this forces send order, if Bal zero =, but only is service printer
            RaiseEvent SendOrder(False)
        End If


        If _closeCheck.releaseFlag = True Then
            GenerateOrderTables.ReleaseTableOrTab()
        Else
            '********
            'we need to flag closed as 99 and fully paid (not released as 10)
            '            If _closeCheck.RemainingBalancesZero = True Then
            '     GenerateOrderTables.JustMarkAsCloseNoRelease()
            '     End If
        End If

        '444   _closeCheck.readAuth.Shutdown()
        '444    _closeCheck.readAuth = Nothing
        '444new043009  _closeCheck.Visible = False

        '444        _closeCheck.Dispose()
        '444    _closeCheck = Nothing

        If IsFromManager = True Then    'Or goingToSelectedCheck = False Then
            ' goingToSelectedCheck is directing user to either manager or activeOrder
            ' depends on where process orignated or if manager input their password
            If Not actingManager Is Nothing Then
                If currentServer.EmployeeID = actingManager.EmployeeID Then
                    RaiseEvent ManagerClosing(IsFromManager, actingManager, goingToSelectedCheck)
                Else
                    'this means started as a server, then manager logged in
                    RaiseEvent ManagerClosing(IsFromManager, currentServer, goingToSelectedCheck)
                End If
            Else
                'this is here just in case... should never get here
                RaiseEvent ManagerClosing(IsFromManager, currentServer, goingToSelectedCheck)
            End If
            '      RaiseEvent FireOrderScreen()
        ElseIf IsFromManager = False Then
            If goingToSelectedCheck = False Then
                RaiseEvent ManagerClosing(IsFromManager, currentServer, goingToSelectedCheck)
            Else
                RaiseEvent SplitCheckClosing()
                '       _closeCheck.Visible = True

            End If

        End If

    End Sub
    Private Sub MakingGiftAddingAmountTrue() Handles _closeCheck.MakeGiftAddingAmountTrue

        RaiseEvent MakeGiftAddingAmountTrue()

    End Sub
    Private Sub Manager_ClosingClose(ByVal going As Boolean, ByVal remainBalZero As Boolean) Handles _closeCheck.CloseExiting

        goingToSelectedCheck = going
        RemainingBalancesZero = remainBalZero
        SplitDisposeSelf()

    End Sub

    Private Sub changeNumberOfGrids(ByVal sender As Object, ByVal e As System.EventArgs) Handles changeNumberOfGridsButton.Click
        ResetTimer()

        If numberGridsInView = 4 Then
            numberGridsInView = 10
        Else
            numberGridsInView = 4
        End If

        PerformChangeOfGrids()

    End Sub

    Private Sub PerformChangeOfGrids() Handles _closeCheck.SplitSingleCheck
        sgp1.Dispose()
        sgp2.Dispose()
        sgp3.Dispose()
        sgp4.Dispose()
        sgp5.Dispose()
        sgp6.Dispose()
        sgp7.Dispose()
        sgp8.Dispose()
        sgp9.Dispose()
        sgp10.Dispose()

        InitializeSplitGridPanels()
    End Sub

    Private Sub StartSplitItem(ByVal sender As Object, ByVal e As System.EventArgs) Handles splitItemButton.Click
        '    Me.CurrentSplittingChecks.Clear()
        ResetTimer()

        Dim oRow As DataRow
        Dim splitItemName As String
        Dim splitItemPrice As Decimal
        Dim numberOfChecks As Integer
        Dim amountOnEachCheck As Decimal
        Dim roundingError As Decimal

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("sin") = splitItemSIN Then
                    '   verify food / drink item has been ordered
                    '   can not split item before ordered
                    If oRow("ForceFreeID") = -8 Or oRow("ForceFreeID") = -9 Then
                        MsgBox("This Item has been Transfered or Voided.")
                        Exit Sub
                    End If
                    If oRow("ItemStatus") < 2 Then
                        MsgBox("You can not split an item until after it is ordered.")
                        Exit Sub
                    End If
                    splitItemName = (oRow("ItemName"))
                    splitItemQuantity = oRow("Quantity")
                    splitItemInvMultiplier = oRow("OpenDecimal1")
                End If
                If oRow("sii") = splitItemSIN Then
                    splitItemPrice += oRow("Price")
                End If
            End If
        Next

        '   determine number of checks (possible)
        numberOfChecks = DetermineCheckCount()
        ReDim checksSplitting(numberOfChecks - 1)
        '   display label


        DetermineChecksToAddToCollection()

        '   determine amount to each check
        amountOnEachCheck = DetermineAmountOnEachCheck(splitItemPrice, numberOfChecks)
        '   determine rounding error
        roundingError = DetermineRoundingError(splitItemPrice, amountOnEachCheck, numberOfChecks)

        AddAmountToCollection(amountOnEachCheck, splitItemQuantity, numberOfChecks)

        splitItemPanel = New SplitItemUserControl(checksSplitting, splitItemPrice, splitItemQuantity, numberOfChecks)
        Me.splitItemPanel.Location = New Point((Me.Width - splitItemPanel.Width) / 2, (Me.Height - splitItemPanel.Height) / 2)
        Me.Controls.Add(splitItemPanel)

        Me.splitItemPanel.BringToFront()

        DisplaySplitLabel(splitItemName)

    End Sub

    Private Sub ReleaseTable(ByVal sender As Object, ByVal e As System.EventArgs) Handles closeExperienceButton.Click
        '   this will make table avail for seating and transfer any outstanding check with balances to named experience
        '   ****************************************
        '   *** this is for ONE CHECK .. no spliting
        ResetTimer()

        Dim oRow As DataRow

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If Not oRow("CheckNumber") = 1 Then
                    oRow("CheckNumber") = 1
                End If
            End If
        Next
        currentTable.NumberOfChecks = 1

        If currentTerminal.TermMethod = "Quick" Or currentTable.TicketNumber > 0 Then
            For Each oRow In dsOrder.Tables("QuickTickets").Rows
                If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                    oRow("NumberOfChecks") = 1
                End If
            Next
        Else
            If currentTable.IsTabNotTable = True Then
                For Each oRow In dsOrder.Tables("AvailTabs").Rows
                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        oRow("NumberOfChecks") = 1
                    End If
                Next
            Else
                For Each oRow In dsOrder.Tables("AvailTables").Rows
                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        oRow("NumberOfChecks") = 1
                    End If
                Next
            End If
        End If

        'sss   GenerateOrderTables.SaveAvailTabsAndTables()

        numberGridsInView = 4
        PerformChangeOfGrids()

    End Sub

    Private Sub RemoveTableFromCollection222()
        Dim btnTabsAndTables As New DataSet_Builder.AvailTableUserControl

        For Each btnTabsAndTables In currentActiveTables
            If btnTabsAndTables.ExperienceNumber = currentTable.ExperienceNumber Then
                currentActiveTables.Remove(btnTabsAndTables)
                Exit Sub
            End If
        Next

    End Sub
    Private Sub GoToTestRemainingBalance() Handles _closeCheck.CloseExitAndRelease

        '      InitializeSplitGridPanels()
        RecaculatePriceAndTaxRemaining()
        _closeCheck.RemainingBalancesZero = TestRemainingBalances()
        '   stops timer in CloseExiting

    End Sub

    Friend Function TestRemainingBalances()
        
        If sgp1.remainingBalance > 0.03 And sgp1.remainingBalance < -0.03 Then
            Return False
        ElseIf sgp2.remainingBalance > 0.03 And sgp2.remainingBalance < -0.03 Then
            Return False
        ElseIf sgp3.remainingBalance > 0.03 And sgp3.remainingBalance < -0.03 Then
            Return False
        ElseIf sgp4.remainingBalance > 0.03 And sgp4.remainingBalance < -0.03 Then
            Return False
        ElseIf sgp5.remainingBalance > 0.03 And sgp5.remainingBalance < -0.03 Then
            Return False
        ElseIf sgp6.remainingBalance > 0.03 And sgp6.remainingBalance < -0.03 Then
            Return False
        ElseIf sgp7.remainingBalance > 0.03 And sgp7.remainingBalance < -0.03 Then
            Return False
        ElseIf sgp8.remainingBalance > 0.03 And sgp8.remainingBalance < -0.03 Then
            Return False
        ElseIf sgp9.remainingBalance > 0.03 And sgp9.remainingBalance < -0.03 Then
            Return False
        ElseIf sgp10.remainingBalance > 0.03 And sgp10.remainingBalance < -0.03 Then
            Return False
        End If

        Return True

    End Function

    Private Function DetermineCheckCount()
        Dim checkCount As Integer

        If sgp1.splitGrid.Items.Count > 0 Then
            checkCount += 1
        End If
        If sgp2.splitGrid.Items.Count > 0 Then
            checkCount += 1
        End If
        If sgp3.splitGrid.Items.Count > 0 Then
            checkCount += 1
        End If
        If sgp4.splitGrid.Items.Count > 0 Then
            checkCount += 1
        End If
        If sgp5.splitGrid.Items.Count > 0 Then
            checkCount += 1
        End If
        If sgp6.splitGrid.Items.Count > 0 Then
            checkCount += 1
        End If
        If sgp7.splitGrid.Items.Count > 0 Then
            checkCount += 1
        End If
        If sgp8.splitGrid.Items.Count > 0 Then
            checkCount += 1
        End If
        If sgp9.splitGrid.Items.Count > 0 Then
            checkCount += 1
        End If
        If sgp10.splitGrid.Items.Count > 0 Then
            checkCount += 1
        End If

        Return checkCount

    End Function

    Private Sub DetermineChecksToAddToCollection()
        Dim index As Integer = 0

        If sgp1.splitGrid.Items.Count > 0 Then
            AddCheckToCollection(1, index)
            index += 1
        End If
        If sgp2.splitGrid.Items.Count > 0 Then
            AddCheckToCollection(2, index)
            index += 1
        End If
        If sgp3.splitGrid.Items.Count > 0 Then
            AddCheckToCollection(3, index)
            index += 1
        End If
        If sgp4.splitGrid.Items.Count > 0 Then
            AddCheckToCollection(4, index)
            index += 1
        End If
        If sgp5.splitGrid.Items.Count > 0 Then
            AddCheckToCollection(5, index)
            index += 1
        End If
        If sgp6.splitGrid.Items.Count > 0 Then
            AddCheckToCollection(6, index)
            index += 1
        End If
        If sgp7.splitGrid.Items.Count > 0 Then
            AddCheckToCollection(7, index)
            index += 1
        End If
        If sgp8.splitGrid.Items.Count > 0 Then
            AddCheckToCollection(8, index)
            index += 1
        End If
        If sgp9.splitGrid.Items.Count > 0 Then
            AddCheckToCollection(9, index)
            index += 1
        End If
        If sgp10.splitGrid.Items.Count > 0 Then
            AddCheckToCollection(10, index)
            index += 1
        End If


    End Sub

    Private Sub AddCheckToCollection(ByVal checkNumber As Integer, ByVal indexNumber As Integer)

        checksSplitting(indexNumber) = New SplittingCheck
        checksSplitting(indexNumber).CheckNumber = checkNumber

    End Sub

    Private Sub AddAmountToCollection(ByVal amount As Decimal, ByVal quantity As Integer, ByVal number As Integer)
        Dim extraQuantity As Integer
        Dim index As Integer
        Dim checksWithQuantitySplit As Integer

        extraQuantity = splitItemQuantity - number

        For index = 0 To number - 1
            checksSplitting(index).CheckAmount = amount

            If extraQuantity >= 0 Then
                If index = 0 Then
                    '   first check gets any extra
                    checksSplitting(index).CheckQuantity = 1 + extraQuantity
                    checksSplitting(index).CheckInvMultiplier = splitItemInvMultiplier '+ (splitItemInvMultiplier * extraQuantity)
                Else
                    checksSplitting(index).CheckQuantity = 1
                    checksSplitting(index).CheckInvMultiplier = splitItemInvMultiplier
                End If
            Else
                If extraQuantity < 0 Then
                    If index + 1 > splitItemQuantity Then
                        checksSplitting(index).CheckQuantity = 0
                        checksSplitting(index).CheckInvMultiplier = 0
                    Else
                        checksSplitting(index).CheckQuantity = 1
                        checksSplitting(index).CheckInvMultiplier = splitItemInvMultiplier
                    End If
                End If
            End If
        Next

    End Sub

    Private Function DetermineAmountOnEachCheck(ByVal totalPrice As Decimal, ByVal n As Integer)
        Dim amount As Decimal

        amount = Format((totalPrice / n), "###0.00")

        Return amount

    End Function

    Private Function DetermineRoundingError(ByVal totalPrice As Decimal, ByVal eachAmount As Decimal, ByVal n As Integer)
        Dim rounding As Decimal

        rounding = Format((totalPrice - (eachAmount * n)), "###0.00")

        Return rounding

    End Function

    Private Sub DisplaySplitLabel(ByVal itemName As String)

        splitItemPanel.splitItemLabel.Text = "Spliting Item: " & itemName

    End Sub

    Private Sub ApplingSplitCheckFirstStep(ByVal sender As Object, ByVal e As System.EventArgs) Handles splitItemPanel.ApplySplitCheck
        ResetTimer()

        CurrentSplittingChecks = New SplittingCheckCollection

        Dim check As SplittingCheck

        For Each check In splitItemPanel.checkArray
            If check.CheckAmount > 0 Then
                CurrentSplittingChecks.Add(check)
            End If
        Next

        ApplingSplitCheckSecondStep()
        '        RaiseEvent ApplySplitCheck(sender, e)

    End Sub

    Private Sub ApplingSplitCheckSecondStep()

        '   maybe, place new price on the original check
        '   eliminate the original check for the collection
        '   then place the split info on new checks w/ new sin's

        Dim oRow As DataRow
        Dim eachCheck As SplittingCheck
        Dim currentItem As SelectedItemDetail = New SelectedItemDetail

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("sii") = splitItemSIN Then
                    If oRow("sin") = splitItemSIN Then
                        '   this determines which check in the collection is the original
                        For Each eachCheck In CurrentSplittingChecks
                            If oRow("CheckNumber") = eachCheck.CheckNumber Then

                                oRow("Price") = eachCheck.CheckAmount
                                oRow("TaxPrice") = DetermineTaxPrice(oRow("TaxID"), eachCheck.CheckAmount)
                                oRow("SinTax") = DetermineSinTax(oRow("TaxID"), eachCheck.CheckAmount)
                                oRow("Quantity") = eachCheck.CheckQuantity

                                currentItem.InvMultiplier = eachCheck.CheckInvMultiplier 'oRow("OpenDecimal1") 'don't think correct
                                'not sure about table
                                currentItem.ItemPrice = oRow("ItemPrice")
                                currentItem.ID = oRow("ItemID")
                                currentItem.Name = oRow("ItemName")
                                currentItem.TerminalName = oRow("ItemName")
                                currentItem.ChitName = oRow("ItemName")
                                currentItem.TaxID = oRow("TaxID")
                                currentItem.OrderNumber = oRow("OrderNumber")

                                currentItem.Course = oRow("CourseNumber")
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
                        CurrentSplittingChecks.Remove(eachCheck)
                    Else    'this is for sub-items
                        oRow("Price") = 0
                    End If
                End If
            End If
        Next

        For Each eachCheck In CurrentSplittingChecks
            currentTable.SIN += 1       'we need to add an extra (so we have room for cust number)
            currentItem.Check = eachCheck.CheckNumber
            currentItem.Price = eachCheck.CheckAmount
            currentItem.Quantity = eachCheck.CheckQuantity
            currentItem.InvMultiplier = eachCheck.CheckInvMultiplier
            currentItem.SIN = currentTable.SIN
            currentItem.SII = currentTable.SIN
            currentItem.InvMultiplier = 0   ' ****** don't think correct, but want original to have 
            ' need to figure in SplittingCheck Collection

            currentItem.Table = currentTable.TableNumber
            currentItem.Customer = eachCheck.CheckNumber        'should ask for this if multi cust on second check

            GenerateOrderTables.PopulateDataRowForOpenOrder(currentItem)
            currentTable.SIN += 1
        Next

        '      AdjustOpenOrderPosition()
        '     GenerateOrderTables.SaveOpenOrderData()

        '   reflects the change
        ResetSplitGrids()

    End Sub

    Private Sub ResetSplitGrids()

        sgp1.splitGrid.Items.Clear()
        sgp2.splitGrid.Items.Clear()
        sgp3.splitGrid.Items.Clear()
        sgp4.splitGrid.Items.Clear()
        sgp5.splitGrid.Items.Clear()
        sgp6.splitGrid.Items.Clear()
        sgp7.splitGrid.Items.Clear()
        sgp8.splitGrid.Items.Clear()
        sgp9.splitGrid.Items.Clear()
        sgp10.splitGrid.Items.Clear()

        sgp1.CreateSplitDataView(1)
        sgp2.CreateSplitDataView(2)
        sgp3.CreateSplitDataView(3)
        sgp4.CreateSplitDataView(4)
        sgp5.CreateSplitDataView(5)
        sgp6.CreateSplitDataView(6)
        sgp7.CreateSplitDataView(7)
        sgp8.CreateSplitDataView(8)
        sgp9.CreateSplitDataView(9)
        sgp10.CreateSplitDataView(10)


    End Sub

    Private Sub CloseCheckSelectedReOrder(ByVal dt As DataTable, ByVal tabTestNeeded As Boolean) Handles _closeCheck.SelectedReOrder

        RaiseEvent SelectedReOrder(dt, True)

    End Sub

    Private Sub FiringTabScreen(ByVal startInSearch As String, ByVal searchCriteria As String) Handles _closeCheck.FireTabScreen

        RaiseEvent FireTabScreen(startInSearch, searchCriteria)

    End Sub

    '    Private Sub AuthPaymentButtonSelected(ByRef authamount As PreAuthAmountClass, ByRef authtransaction As PreAuthTransactionClass, ByVal cardSwipedDatabaseInfo As Boolean) Handles _closeCheck.AuthPayments

    '       closeCreditCardAuth = New CloseManualAuth(authamount, authtransaction, cardSwipedDatabaseInfo)
    '       Me.closeCreditCardAuth.Location = New Point((Me.Width - Me.closeCreditCardAuth.Width) / 2, (Me.Height - Me.closeCreditCardAuth.Height) / 2)
    '       Me.Controls.Add(Me.closeCreditCardAuth)
    '        Me.closeCreditCardAuth.BringToFront()

    '   End Sub

    Private Sub MerchantAuthirizingPaymentStep1(ByVal paymentID As Integer, ByVal justActive As Boolean) Handles _closeCheck.MerchantAuthPayment

        RaiseEvent MerchantAuthPayment(paymentID, justActive)

    End Sub



    '   **** not using *****
    Private Sub AddTransferedItemsToOpenOrder222(ByVal empID As Integer, ByVal expNum As Int64)
        Dim oRow As DataRow
        Dim nRow As DataRow

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("CheckNumber") = currentTable.CheckNumber Then

                    '                cmd = New SqlClient.SqlCommand("INSERT INTO OpenOrders(EmployeeID, TableNumber, ExperienceNumber) VALUES (@EmployeeID, @TableNumber, @ExperienceNumber)", sql.cn)
                    '               cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeID", System.Data.SqlDbType.Int, 4)) ', "EmployeeID"))
                    '               cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ExperienceNumber", System.Data.SqlDbType.Int, 4)) ', "ExperienceNumber"))
                    '               cmd.Parameters("@ExperienceNumber").Value = expNum
                    ''              cmd.Parameters("@EmployeeID").Value = empID

                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@TabID").Value = oRow("TabID")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@TabName").Value = oRow("TabName")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@TableNumber").Value = oRow("TableNumber")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@ExperienceNumber").Value = expNum
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@OrderNumber").Value = oRow("OrderNumber")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@MenuID").Value = oRow("MenuID")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@EmployeeID").Value = empID
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@CheckNumber").Value = oRow("CheckNumber")   '??
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@CustomerNumber").Value = oRow("CustomerNumber")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@sin").Value = oRow("sin")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@sii").Value = oRow("sii")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@ItemID").Value = oRow("ItemID")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@ItemName").Value = oRow("ItemName")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@Price").Value = oRow("Price")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@TaxID").Value = oRow("TaxID")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@ForceFreeCode").Value = 0
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@CategoryID").Value = oRow("CategoryID")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@FoodID").Value = oRow("FoodID")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@DrinkCategoryID").Value = oRow("DrinkCategoryID")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@DrinkID").Value = oRow("DrinkID")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@FunctionID").Value = oRow("FunctionID")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@RoutingID").Value = oRow("RoutingID")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@PrintPriorityID").Value = oRow("PrintPriorityID")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@ItemStatus").Value = oRow("ItemStatus") '9
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@TerminalID").Value = oRow("TerminalID")
                    sql.SqlInsertCommandOpenOrdersSP.Parameters("@dbUP").Value = oRow("dbUP")
                    oRow("ItemStatus") = 9

                    sql.cn.Open()
                    sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                    sql.SqlInsertCommandOpenOrdersSP.ExecuteNonQuery()
                    '     cmd.ExecuteNonQuery()
                    sql.cn.Close()

                End If
            End If

        Next


    End Sub





End Class


















