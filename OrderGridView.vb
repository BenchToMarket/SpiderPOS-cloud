Imports DataSet_Builder

Public Class OrderGridView
    Inherits System.Windows.Forms.UserControl

    Dim pnlDirection As Panel
    Dim WithEvents btnDown As Button
    Dim WithEvents btnUp As Button
    Dim WithEvents lblOrderView As Label
    Dim holdDoubleClickTimer As Timer
    Friend holdTimerActive As Boolean
    Friend isInHoldMode As Boolean
    Friend justAddedPanel As Boolean
   
    Friend WithEvents gridViewOrder As New OrderGrid
    Dim csCourse As OrderGridColumn
    Dim csItemName As OrderGridColumn
    Dim csQuantity As OrderGridColumn
    Dim csItemDefaultWidth As Single

    Friend gridByCheck As Boolean
    Friend qtRow As Integer    'quick ticket row

    Private pnlSubTotal As Panel
    Private WithEvents checkNumberButton As New Button
    Private totalOrderLabel As New Label
    Friend WithEvents totalOrderTax As New Label
    Friend WithEvents totalOrder As New Label
    Dim totalOrderLabelString As String = "Sub Total:"
    Dim info As DataSet_Builder.Information_UC

    Private WithEvents coursePanel As Panel
    Private btnCourse1 As KitchenButton
    Private btnCourse2 As KitchenButton
    Private btnCourse3 As KitchenButton
    Private btnCourse4 As KitchenButton
    '   Private btnCourse5 As KitchenButton

    Private kitchenCommands As New Panel
    Private WithEvents btnSend As KitchenButton
    Private WithEvents btnHold As KitchenButton
    Private WithEvents btnVoid As KitchenButton
    Private WithEvents btnModify As KitchenButton
    Private WithEvents btnView As KitchenButton
    Private WithEvents btnLeave As KitchenButton
    '   Quick Service
    Private WithEvents btnPrevious As KitchenButton
    Private WithEvents btnNew As KitchenButton

    Private WithEvents ViewStatus As New Panel
    Private WithEvents btnViewDetail As KitchenButton
    Private WithEvents btnViewHolds As KitchenButton
    Private WithEvents btnViewKitchen As KitchenButton
    Private WithEvents btnViewMain As KitchenButton
    Private WithEvents btnViewCourse As KitchenButton
    Private WithEvents btnViewTable As KitchenButton



    '    Event AddingItemToOrder(ByVal sender As Object)
    Event ChangeCustColor(ByVal c As Color)
    Event ChangeCustomerEvent(ByVal btnText As String, ByVal testForPanel1 As Boolean)
    '  Event RePopulateDataViews()
    Event UpdatingViewsByCheck()
    Event JustSaveOrder()
    Event LeaveOrderView()
    Event ModifyItem(ByVal sender As Object, ByVal e As System.EventArgs)
    Event ClearControls()
    Event ClearPanels()
    Event CloseFast(ByVal sender As Object, ByVal e As System.EventArgs)
    Event CloseCheck(ByVal sender As Object, ByVal e As System.EventArgs)
    Event SendOrder(ByVal alsoClose As Boolean)
    Event VoidItem()
    Event UC_Hit()
    Event DisplayInfo(ByVal msg As Object)
    Event NewQuickServiceOrder(ByVal expNum As Int64)
    Event RunPizzaRoutine(ByVal valuesi2 As Integer)
    Event ResetPizzaRoutine()
    Event DeliverStart()
    Event DineInStart(ByVal fromNewTab As Boolean)
    Event OnFullPizza(ByVal sender As Object, ByVal e As System.EventArgs)
    Event DrinkButtonsON()
    Event DrinkButtonsOFF()
    Event EndingItem(ByVal LeavingAnyway As Boolean)


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'OrderGridView
        '
        Me.Name = "OrderGridView"
        Me.Size = New System.Drawing.Size(312, 448)

    End Sub

#End Region

    Private Sub InitializeOther()

        Me.SuspendLayout()

        Me.Height = 768 - (2 * buttonSpace) 'totalArea.Height - (2 * buttonSpace)

        OpenOrdersCurrencyMan = Me.BindingContext(dsOrder.Tables("OpenOrders"))

        DisplayDirectionPanel()

        DisplayOrderView()

        AddGridViewOrderColumns()

        CreateSubTotalPanel()

        CreateCourseButtonPanel()

        CreateKitchenCommandPanel()

        InitializeViewSecondStep()

        Me.ResumeLayout()

    End Sub

    Friend Sub InitializeViewSecondStep()

        Dim numberNONCourse2 As Integer
        Dim maxQuantity As Integer

        holdTimerActive = False
        isInHoldMode = False
        justAddedPanel = False

        If dsOrder.Tables("OpenOrders").Rows.Count = 0 Then
            numberNONCourse2 = 0
        Else
            numberNONCourse2 = dsOrder.Tables("OpenOrders").Compute("Count(Quantity)", "CourseNumber > 2 OR CourseNumber = 1")
        End If

        If numberNONCourse2 = 0 Then
            Me.csCourse.Width = 0
            Me.csItemName.Width = Me.csItemDefaultWidth
        Else
            maxQuantity = dsOrder.Tables("OpenOrders").Compute("Max(Quantity)", "sin = sii")
            MakeRoomForCourseInfo()
            '     Me.csCourse.Width = 20
            '    csItemName.Width = Me.csItemDefaultWidth - 20
            TestQuantityForDisplay(maxQuantity)
        End If

        If currentTable.IsClosed = True Then
            totalOrder.BackColor = c7
            totalOrderTax.BackColor = c7
        Else
            totalOrder.BackColor = c3
            totalOrderTax.BackColor = c3
        End If

        If Not currentTerminal.TermMethod = "Quick" And Not currentTerminal.TermMethod = "Bar" Then
            btnCourse1.BackColor = c7
            btnCourse2.BackColor = c9
        Else
            btnCourse1.BackColor = c9
            btnCourse2.BackColor = c7
        End If

        Select Case currentTerminal.TermMethod
            Case "Table"

            Case "Bar"
                If currentTable.TabID = -888 Then
                    '-888 is for TabGroup  
                    btnPrevious.Visible = True
                    btnNew.Visible = True
                    btnHold.Visible = False
                    btnView.Visible = False
                Else
                    btnPrevious.Visible = False
                    btnNew.Visible = False
                    btnHold.Visible = True
                    btnView.Visible = True
                End If

            Case "Quick"
                Try
                    If dvQuickTickets(dvQuickTickets.Count - 1)("ExperienceNumber") = currentTable.ExperienceNumber Then
                        btnNew.Text = "New"
                    Else
                        btnNew.Text = "Next"
                    End If
                Catch ex As Exception

                End Try
        End Select

        ReStateOrderView()

        UpdateCheckNumberButton()

        CalculateSubTotal()

        DetermineQuickTicketRow()

    End Sub

    Private Sub DisplayDirectionPanel()
        Dim dirHeight As Single
        Dim dirButtonWidth As Single

        pnlDirection = New Panel
        btnDown = New Button
        btnUp = New Button
        lblOrderView = New Label

        dirHeight = Me.Height * 0.07
        dirButtonWidth = Me.Width / 3

        Me.pnlDirection.Location = New Point(0, 0)
        Me.pnlDirection.Size = New Size(Me.Width, dirHeight)
        Me.pnlDirection.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.btnDown.Location = New Point(0, 0)
        Me.lblOrderView.Location = New Point(dirButtonWidth, 0)
        Me.btnUp.Location = New Point((2 * dirButtonWidth), 0)

        Me.btnDown.Size = New Size(dirButtonWidth, dirHeight)
        Me.lblOrderView.Size = New Size(dirButtonWidth, dirHeight)
        Me.btnUp.Size = New Size(dirButtonWidth, dirHeight)

        Me.btnDown.BackColor = c6
        Me.lblOrderView.BackColor = c6
        Me.btnUp.BackColor = c6

        Me.btnDown.ForeColor = c3
        Me.lblOrderView.ForeColor = c3
        Me.btnUp.ForeColor = c3

        Me.btnDown.TextAlign = ContentAlignment.BottomCenter
        Me.btnUp.TextAlign = ContentAlignment.TopCenter
        Me.lblOrderView.TextAlign = ContentAlignment.MiddleCenter
        '     Me.lblDirection.Te()  word wrap??


        Me.pnlDirection.Controls.Add(Me.btnDown)
        Me.pnlDirection.Controls.Add(Me.lblOrderView)
        Me.pnlDirection.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.pnlDirection)

        DisplayDownButtonText()
        DisplayUpButtonText()
        DisplayDirectionLabel("Detail")


    End Sub

    Private Sub DisplayOrderView()

        '   gridview
        Me.gridViewOrder.Location = New Point(0, Me.pnlDirection.Height)
        Me.gridViewOrder.Size = New Size(Me.Width, (Me.Height * 0.63))
        '   Me.gridViewOrder.BackColor = Color.White
        Me.gridViewOrder.BackgroundColor = c2
        Me.gridViewOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '       Me.gridViewOrder.Font = New System.Drawing.Font("Myriad Condensed Web", 12.0, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '       Me.gridViewOrder.Font = New System.Drawing.Font("Century Gothic", 12.0, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '     Me.gridViewOrder.Font = New System.Drawing.Font("TImes New Roman", 12.0, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.gridViewOrder.ReadOnly = True
        Me.gridViewOrder.Controls(0).Height = 0
        Me.gridViewOrder.Controls(1).Width = 0

        Me.gridViewOrder.ColumnHeadersVisible = False
        gridViewOrder.RowHeadersVisible = False
        Me.gridViewOrder.CaptionVisible = False
        gridViewOrder.DataSource = dvOrder 'dsOrder.Tables("Order")
        currentTable.OrderView = "Detail"
        gridViewOrder.SelectionBackColor = Color.Blue
        gridViewOrder.AllowSorting = False
        Me.BackColor = c2

        Me.Controls.Add(Me.gridViewOrder)


    End Sub

    Private Sub ReStateOrderView()

        OpenOrdersCurrencyMan = Me.BindingContext(dsOrder.Tables("OpenOrders"))
        gridViewOrder.DataSource = dvOrder 'dsOrder.Tables("Order")
        currentTable.OrderView = "Detail"

    End Sub


    Private Sub AddGridViewOrderColumns()

        Dim tsOrder As New DataGridTableStyle
        tsOrder.MappingName = "OpenOrders" '"Order"
        tsOrder.RowHeadersVisible = False
        tsOrder.GridLineStyle = DataGridLineStyle.None

        Dim csStatus As New DataGridTextBoxColumn
        csStatus.MappingName = "ItemStatus"
        csStatus.Width = 0

        Dim csSIN As New DataGridTextBoxColumn
        csSIN.MappingName = "sin"
        csSIN.Width = 0

        Dim csSII As New DataGridTextBoxColumn
        csSII.MappingName = "sii"
        csSII.Width = 0

        Dim csSI2 As New DataGridTextBoxColumn
        csSI2.MappingName = "si2"
        csSI2.Width = 0

        Dim csCust As New DataGridTextBoxColumn
        csCust.MappingName = "CustomerNumber"
        csCust.Width = 0

        '   *** changed from dataGridTextBoxColumn (which gave it a reverse text)
        csCourse = New OrderGridColumn(False, False, True) '(Price, Quantity, Course)
        csCourse.MappingName = "CourseNumber"
        'csCourse.width is below

        Dim csItemID As New DataGridTextBoxColumn
        csItemID.MappingName = "ItemID"
        csItemID.Width = 0

        csQuantity = New OrderGridColumn(False, True, False) '(Price, Quantity, Course)
        csQuantity.MappingName = "Quantity"
        csQuantity.Width = 15
        csQuantity.Alignment = HorizontalAlignment.Right

        csItemName = New OrderGridColumn(False, False, False) 'DataGridTextBoxColumn 
        Me.csItemDefaultWidth = 235 '(0.8 * Me.Width) - 15
        csItemName.MappingName = "TerminalName"
        '     csItemName.TextBox.WordWrap = True
        '     csItemName.Width = Me.csItemDefaultWidth (below)

        Dim csItemCost As New OrderGridColumn(True, False, False) '(Price, Quantity)   'Price   'DataGridTextBoxColumn
        csItemCost.MappingName = "Price"
        csItemCost.Width = 0.19 * Me.Width                  'this is column 9
        csItemCost.Alignment = HorizontalAlignment.Right

        'moved courseColumn to 2nd step
        Me.csCourse.Width = 0
        Me.csItemName.Width = Me.csItemDefaultWidth

        Dim csRoutingID As New DataGridTextBoxColumn
        csRoutingID.MappingName = "RoutingID"
        csRoutingID.Width = 0

        Dim csDrinkFlag As New DataGridTextBoxColumn
        csDrinkFlag.MappingName = "FunctionFlag"
        csDrinkFlag.Width = 0

        tsOrder.GridColumnStyles.Add(csStatus)
        tsOrder.GridColumnStyles.Add(csSIN)
        tsOrder.GridColumnStyles.Add(csSII)
        tsOrder.GridColumnStyles.Add(csSI2)
        tsOrder.GridColumnStyles.Add(csCust)
        tsOrder.GridColumnStyles.Add(csCourse)
        tsOrder.GridColumnStyles.Add(csItemID)
        tsOrder.GridColumnStyles.Add(csQuantity)
        tsOrder.GridColumnStyles.Add(csItemName)
        tsOrder.GridColumnStyles.Add(csItemCost)
        tsOrder.GridColumnStyles.Add(csRoutingID)
        tsOrder.GridColumnStyles.Add(csDrinkFlag)
        '      tsOrder.GridColumnStyles.Add(csBlank)
        gridViewOrder.TableStyles.Add(tsOrder)

    End Sub

    Private Sub gridViewOrder_CellChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gridViewOrder.CurrentCellChanged
        RaiseEvent UC_Hit()

        Dim tempReferenceSIN As Integer
        Dim oRow As DataRow
        Dim valueSIN As Integer
        Dim valueSII As Integer
        Dim valueSI2 As Integer
        Dim rowNum As Integer

        '      MsgBox(currentTable.SIN)

        '   if in middle of order do not adjust customer number
        '       If currentTable.SIN > currentTable.ReferenceSIN Then
        If currentTable.MiddleOfOrder = True Then
            '     Exit Sub
            RaiseEvent EndingItem(False)
            ' this would be reset to false if no longer req modifier
            If currentTable.ReqModifier = True Then
                '          Me.gridViewOrder.CurrentRowIndex = OpenOrdersCurrencyMan.Position
                Exit Sub
            End If

            OpenOrdersCurrencyMan.Position = Me.gridViewOrder.CurrentRowIndex
            '444      currentTable.MiddleOfOrder = False
            '       Exit Sub
        Else
            RaiseEvent ClearControls()
            OpenOrdersCurrencyMan.Position = Me.gridViewOrder.CurrentRowIndex
            currentTable.TempReferenceSIN = CType(Me.gridViewOrder.Item(OpenOrdersCurrencyMan.Position, 2), Integer)
        End If

        rowNum = OpenOrdersCurrencyMan.Position
        '   If CType(gridViewOrder.Item(rowNum, 4), Integer) = currentTable.CustomerNumber Or currentTable.MiddleOfOrder = False Then
        '         currentTable.TempReferenceSIN = CType(Me.gridViewOrder.Item(OpenOrdersCurrencyMan.Position, 2), Integer)
        '    End If

        Try
            valueSI2 = CType(gridViewOrder.Item(rowNum, 3), Integer)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try
            valueSIN = CType(gridViewOrder.Item(rowNum, 1), Integer)
            valueSII = CType(gridViewOrder.Item(rowNum, 2), Integer)
        Catch ex As Exception
            Exit Sub
        End Try

        If isInHoldMode = True Then
            RunHoldRoutine(rowNum, valueSIN, valueSII)
            Exit Sub
        End If

        If valueSI2 > 0 And valueSI2 < 10 Then
            currentTable.ReferenceSIN = currentTable.TempReferenceSIN   '????
            currentTable.SecondRound = True
            RaiseEvent RunPizzaRoutine(valueSI2)
            currentTable.si2 = valueSI2 'CType(Me.gridViewOrder.Item(rowNum, 3), Integer)

        Else

            '444    If currentTable.si2 > 0 Then
            'RaiseEvent ResetPizzaRoutine()
            '   End If

        End If

        '444      If Not currentTerminal.TermMethod = "Quick" Then ' And Not currentTerminal.TermMethod = "Bar" Then
        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("sin") = currentTable.TempReferenceSIN Then
                    If Not currentTerminal.TermMethod = "Quick" And Not currentTerminal.TermMethod = "Bar" Then
                        If Not oRow("CourseNumber") = 0 Then
                            currentTable.CourseNumber = oRow("CourseNumber")
                            ChangeCourseButton(currentTable.CourseNumber)
                        End If
                    Else
                        If Not oRow("Quantity") = 0 Then
                            currentTable.Quantity = oRow("Quantity")
                            ChangeCourseButton(currentTable.Quantity)
                        End If

                    End If

                    If justAddedPanel = True Then
                        justAddedPanel = False
                    Else
                        RaiseEvent ChangeCustColor(c7)
                        currentTable.CustomerNumber = oRow("CustomerNumber")
                        RaiseEvent ChangeCustomerEvent(oRow("CustomerNumber").ToString, False)
                        '*****************************************
                        'this is for problem jumping customers after deleting
                        '           If currentTable.JumpedToNextCustomer = False Then
                        '                'this is defined oin delete (btnVoidClick)
                        '          RaiseEvent ChangeCustomerEvent(oRow("CustomerNumber").ToString, False)
                        '         Else
                        '            currentTable.JumpedToNextCustomer = False
                        '       End If
                    End If
                    Exit For
                End If
            End If
        Next
        '444 End If

        If (gridViewOrder.Item(rowNum, 11)) = "D" Then

            RaiseEvent DrinkButtonsON()
        Else
            RaiseEvent DrinkButtonsOFF()
        End If

    End Sub

    Private Sub CreateSubTotalPanel()
        Dim adjHeight As Single

        pnlSubTotal = New Panel
        pnlSubTotal.Location = New Point(0, (Me.pnlDirection.Height + Me.gridViewOrder.Height))
        pnlSubTotal.Size = New Size(Me.Width, (Me.Height * 0.07))

        adjHeight = Me.pnlSubTotal.Height * 0.7

        '**********************
        '   Panel with SubTotal
        checkNumberButton.Location = New Point(0, 0)
        totalOrderLabel.Location = New Point(0.35 * Me.Width, 0)
        totalOrder.Location = New Point(0.55 * Me.Width, 20)
        totalOrderTax.Location = New Point(0.55 * Me.Width, 0)
        Me.totalOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.totalOrderTax.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        checkNumberButton.Size = New Size(0.34 * Me.Width, adjHeight)
        totalOrderLabel.Size = New Size(0.2 * Me.Width, adjHeight)
        totalOrder.Size = New Size(0.45 * Me.Width, Me.pnlSubTotal.Height - 20)
        totalOrderTax.Size = New Size(0.45 * Me.Width, 20) 'Me.pnlSubTotal.Height)
        'totalOrder displays subtotal

        totalOrderLabel.Text = totalOrderLabelString
        totalOrder.Text = Format(CType(currentTable.SubTotal + currentTable.SubTax, Decimal), "##,##0.00")
        totalOrderTax.Text = Format(CType(currentTable.SubTax, Decimal), "##,##0.00")

        totalOrderLabel.TextAlign = ContentAlignment.MiddleLeft
        totalOrder.TextAlign = ContentAlignment.MiddleRight
        totalOrderTax.TextAlign = ContentAlignment.TopRight
        If currentTable.IsClosed = True Then
            totalOrder.BackColor = c7
            totalOrderTax.BackColor = c7
        Else
            totalOrder.BackColor = c3
            totalOrderTax.BackColor = c3
        End If
        totalOrderTax.BackColor = Color.Transparent
        checkNumberButton.BackColor = c1

        Me.pnlSubTotal.Controls.Add(Me.checkNumberButton)
        Me.pnlSubTotal.Controls.Add(Me.totalOrderLabel)
        Me.pnlSubTotal.Controls.Add(Me.totalOrder)
        Me.pnlSubTotal.Controls.Add(Me.totalOrderTax)
        Me.Controls.Add(Me.pnlSubTotal)

    End Sub

    Private Sub CreateCourseButtonPanel()

        coursePanel = New Panel
        coursePanel.SuspendLayout()

        coursePanel.Location = New Point(1, (Me.pnlDirection.Height + Me.gridViewOrder.Height + Me.pnlSubTotal.Height))
        coursePanel.Size = New Size(Me.Width, (Me.Height * 0.05))
        coursePanel.BackColor = c7

        Dim w As Single = Me.coursePanel.Width / 4
        Dim h As Single = Me.coursePanel.Height
        Dim x As Single = 0

        If Not currentTerminal.TermMethod = "Quick" And Not currentTerminal.TermMethod = "Bar" Then
            btnCourse1 = New KitchenButton(1, w, h, c7, c2)
        Else
            btnCourse1 = New KitchenButton(1, w, h, c9, c2)
        End If
        With btnCourse1
            .Location = New Point(x, 0)
            .ForeColor = c3
            AddHandler btnCourse1.Click, AddressOf CourseButton_Click
        End With
        x = x + w

        If Not currentTerminal.TermMethod = "Quick" And Not currentTerminal.TermMethod = "Bar" Then
            btnCourse2 = New KitchenButton(2, w, h, c9, c2)
        Else
            btnCourse2 = New KitchenButton(2, w, h, c7, c2)
        End If
        With btnCourse2
            .Location = New Point(x, 0)
            .ForeColor = c3
            AddHandler btnCourse2.Click, AddressOf CourseButton_Click
        End With
        x = x + w

        btnCourse3 = New KitchenButton(3, w, h, c7, c2)
        With btnCourse3
            .Location = New Point(x, 0)
            .ForeColor = c3
            AddHandler btnCourse3.Click, AddressOf CourseButton_Click
        End With
        x = x + w

        btnCourse4 = New KitchenButton(4, w, h, c7, c2)
        With btnCourse4
            .Location = New Point(x, 0)
            .ForeColor = c3
            AddHandler btnCourse4.Click, AddressOf CourseButton_Click
        End With
        '     x = x + w

        '       btnCourse5 = New KitchenButton(5, w, h, c7, c2)
        '      With btnCourse5
        '     .Location = New Point(x, 0)
        '    .ForeColor = c3
        '   AddHandler btnCourse5.Click, AddressOf CourseButton_Click
        '  End With
        ' x = x + w

        coursePanel.Controls.Add(btnCourse1)
        coursePanel.Controls.Add(btnCourse2)
        coursePanel.Controls.Add(btnCourse3)
        coursePanel.Controls.Add(btnCourse4)
        '     coursePanel.Controls.Add(btnCourse5)

        coursePanel.ResumeLayout()
        Me.Controls.Add(Me.coursePanel)

    End Sub

    Private Sub CreateKitchenCommandPanel()

        Dim xPanelSize As Double = Me.Width
        Dim yPanelSize As Double = Me.Height * 0.18
        Dim xButtonSize As Double = ((xPanelSize - (4 * buttonSpace)) / 3)
        Dim yButtonSize As Double = ((yPanelSize - (3 * buttonSpace)) / 2)
        Dim xButtonStep As Double = xButtonSize + buttonSpace
        Dim yButtonStep As Double = yButtonSize + buttonSpace
        Dim x As Double = buttonSpace
        Dim y As Double = buttonSpace

        Select Case currentTerminal.TermMethod
            Case "Table"
                btnHold = New KitchenButton("Hold", xButtonSize, yButtonSize, c4, c3)
                btnHold.Location = New Point(x, y)
            Case "Bar"
                '444    If currentTable.TabID = -888 Then
                '-888 is for TabGroup
                btnPrevious = New KitchenButton("Previous", xButtonSize, yButtonSize, c4, c3)
                btnPrevious.Location = New Point(x, y)
                '    Else
                btnHold = New KitchenButton("Hold", xButtonSize, yButtonSize, c4, c3)
                btnHold.Location = New Point(x, y)
                '   End If

            Case "Quick"
                btnPrevious = New KitchenButton("Previous", xButtonSize, yButtonSize, c4, c3)
                btnPrevious.Location = New Point(x, y)
        End Select
        x = x + xButtonStep
        btnVoid = New KitchenButton("Delete", xButtonSize, yButtonSize, c4, c3)
        btnVoid.Location = New Point(x, y)
        x = x + xButtonStep

        Select Case currentTerminal.TermMethod
            Case "Table"
                btnView = New KitchenButton("View", xButtonSize, yButtonSize, c4, c3)
                btnView.Location = New Point(x, y)
            Case "Bar"
                '444       If currentTable.TabID = -888 Then
                '-888 is for TabGroup
                btnNew = New KitchenButton("New", xButtonSize, yButtonSize, c4, c3)
                btnNew.Location = New Point(x, y)
                '     Else
                btnView = New KitchenButton("View", xButtonSize, yButtonSize, c4, c3)
                btnView.Location = New Point(x, y)
                '    End If

            Case "Quick"
                Try
                    If dvQuickTickets(dvQuickTickets.Count - 1)("ExperienceNumber") = currentTable.ExperienceNumber Then
                        btnNew = New KitchenButton("New", xButtonSize, yButtonSize, c4, c3)
                        btnNew.Location = New Point(x, y)
                    Else
                        btnNew = New KitchenButton("Next", xButtonSize, yButtonSize, c4, c3)
                        btnNew.Location = New Point(x, y)
                    End If
                Catch ex As Exception

                End Try

        End Select
        x = buttonSpace
        y = y + yButtonStep

        btnSend = New KitchenButton("Send", xButtonSize, yButtonSize, c4, c3)
        btnSend.Location = New Point(x, y)
        x = x + xButtonStep
        btnModify = New KitchenButton("Modify", xButtonSize, yButtonSize, c4, c3)
        btnModify.Location = New Point(x, y)
        x = x + xButtonStep
        btnLeave = New KitchenButton("Leave", xButtonSize, yButtonSize, c4, c3)
        btnLeave.Location = New Point(x, y)

        '   in reverse order
        kitchenCommands.Controls.Add(btnLeave)
        kitchenCommands.Controls.Add(btnModify)
        kitchenCommands.Controls.Add(btnSend)
        kitchenCommands.Controls.Add(btnVoid)

        Select Case currentTerminal.TermMethod
            Case "Table"
                kitchenCommands.Controls.Add(btnView)
                kitchenCommands.Controls.Add(btnHold)
            Case "Bar"
                '444      If currentTable.TabID = -888 Then
                '-888 is for TabGroup
                kitchenCommands.Controls.Add(btnNew)
                kitchenCommands.Controls.Add(btnPrevious)
                '      Else
                kitchenCommands.Controls.Add(btnView)
                kitchenCommands.Controls.Add(btnHold)
                '     End If
            Case "Quick"
                kitchenCommands.Controls.Add(btnNew)
                kitchenCommands.Controls.Add(btnPrevious)
        End Select
        x = buttonSpace
        y = buttonSpace

        btnViewKitchen = New KitchenButton("Sent", xButtonSize, yButtonSize, c1, c2)
        btnViewKitchen.Location = New Point(x, y)
        x = x + xButtonStep
        btnViewHolds = New KitchenButton("Holds", xButtonSize, yButtonSize, c1, c2)
        btnViewHolds.Location = New Point(x, y)
        x = x + xButtonStep
        btnViewDetail = New KitchenButton("Detail", xButtonSize, yButtonSize, c1, c2)
        btnViewDetail.Location = New Point(x, y)
        x = buttonSpace
        y = y + yButtonStep
        btnViewTable = New KitchenButton("By Check", xButtonSize, yButtonSize, c1, c2)
        btnViewTable.Location = New Point(x, y)
        x = x + xButtonStep
        btnViewCourse = New KitchenButton("Course", xButtonSize, yButtonSize, c1, c2)
        btnViewCourse.Location = New Point(x, y)
        x = x + xButtonStep
        btnViewMain = New KitchenButton("Main", xButtonSize, yButtonSize, c1, c2)
        btnViewMain.Location = New Point(x, y)

        AddHandler btnViewDetail.Click, AddressOf ViewStatusClick
        AddHandler btnViewHolds.Click, AddressOf ViewStatusClick
        AddHandler btnViewKitchen.Click, AddressOf ViewStatusClick
        AddHandler btnViewMain.Click, AddressOf ViewStatusClick
        AddHandler btnViewCourse.Click, AddressOf ViewStatusClick
        AddHandler btnViewTable.Click, AddressOf ViewStatusClick

        '   in reverse order
        ViewStatus.Controls.Add(btnViewTable)
        ViewStatus.Controls.Add(btnViewCourse)
        ViewStatus.Controls.Add(btnViewMain)
        ViewStatus.Controls.Add(btnViewKitchen)
        ViewStatus.Controls.Add(btnViewHolds)
        ViewStatus.Controls.Add(btnViewDetail)

        Me.kitchenCommands.Location = New Point(0, (Me.pnlDirection.Height + Me.gridViewOrder.Height + Me.pnlSubTotal.Height + Me.coursePanel.Height + buttonSpace))
        Me.kitchenCommands.Size = New Size(Me.Width, yPanelSize)
        '    kitchenCommands.BackColor = c7
        Me.kitchenCommands.BackColor = c2
        Me.kitchenCommands.BorderStyle = BorderStyle.FixedSingle

        ViewStatus.Location = New Point(0, (Me.pnlDirection.Height + Me.gridViewOrder.Height + Me.pnlSubTotal.Height + Me.coursePanel.Height + buttonSpace))
        ViewStatus.Size = New Size(Me.Width, yPanelSize)
        ViewStatus.BackColor = c7
        ViewStatus.Visible = False

        Me.Controls.Add(Me.kitchenCommands)
        Me.Controls.Add(Me.ViewStatus)

    End Sub

    Private Sub CourseButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles coursePanel.Click
        RaiseEvent UC_Hit()
        If currentTable.IsClosed = True Then
            Exit Sub
        End If

        Dim objButton As KitchenButton
        Dim increaseQuanity As Integer


        Dim rowNum As Integer = OpenOrdersCurrencyMan.Position  'gridViewOrder.CurrentCell.RowNumber
        Dim valueSIN As Integer
        Dim valueSII As Integer


        Try
            objButton = CType(sender, KitchenButton)
        Catch ex As Exception
            Exit Sub
        End Try
        Try
            valueSIN = CType(gridViewOrder.Item(rowNum, 1), Integer)
            valueSII = CType(gridViewOrder.Item(rowNum, 2), Integer)
        Catch ex As Exception
            Exit Sub
        End Try


        If Not objButton.GetType Is btnCourse1.GetType Then Exit Sub
        If Not currentTerminal.TermMethod = "Quick" And Not currentTerminal.TermMethod = "Bar" Then
            currentTable.CourseNumber = CInt(objButton.Text)
            ChangeCourseButton(currentTable.CourseNumber)
        Else
            If Not valueSII = valueSIN Then
                RaiseEvent EndingItem(True)
                ' this would be reset to false if no longer req modifier
                If currentTable.ReqModifier = True Then Exit Sub

                'this will not allow you to change quantity mid orders
                '444       Exit Sub
            End If

            increaseQuanity = CInt(objButton.Text)
            If currentTable.Quantity = 1 Then
                'this is b/c the first time we hit we are only selecting quantithy
                currentTable.Quantity = increaseQuanity
            Else
                currentTable.Quantity += increaseQuanity    'CInt(objButton.Text)
            End If
            ChangeCourseButton(increaseQuanity)
        End If

        If Not currentTerminal.TermMethod = "Quick" And Not currentTerminal.TermMethod = "Bar" Then

            If isInHoldMode = True Then
                Dim oRow As DataRow
                For Each oRow In dsOrder.Tables("OpenOrders").Rows
                    If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                        If oRow("CourseNumber") = currentTable.CourseNumber Then
                            If oRow("ItemStatus") = 0 Then
                                oRow("ItemStatus") = 1
                            ElseIf oRow("ItemStatus") = 1 Then
                                oRow("ItemStatus") = 0
                            End If
                        End If
                    End If
                Next
            End If
        Else
            'we change quantity for current item by this much

            If dsOrder.Tables("OpenOrders").Rows.Count = 0 Then Exit Sub

            Dim oRow As DataRow
            Dim oldQuantity As Integer
            Dim newQuantity As Integer


            If valueSIN = valueSII Then
                'this is if it is a main food, therefore we can change the quantity
                For Each oRow In dsOrder.Tables("OpenOrders").Rows
                    If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                        If Not oRow("ItemStatus") > 1 And Not oRow("ItemID") = 0 Then
                            'not already ordered or customer button
                            If oRow("sin") = valueSIN Then
                                oldQuantity = oRow("Quantity")
                                If oldQuantity = 1 Then
                                    increaseQuanity -= 1
                                End If
                                newQuantity = oldQuantity + increaseQuanity
                            End If
                            If oRow("sii") = valueSII Then
                                oRow("Quantity") = newQuantity
                                '   the below updates prices for the change
                                oRow("Price") = Format(oRow("Price") * ((newQuantity) / oldQuantity), "####0.00")
                                oRow("TaxPrice") = Format(oRow("TaxPrice") * ((newQuantity) / oldQuantity), "####0.00")
                            End If
                        End If
                    End If
                Next
            Else

                For Each oRow In dsOrder.Tables("OpenOrders").Rows
                    If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                        If Not oRow("ItemStatus") > 1 And Not oRow("ItemID") = 0 Then
                            'not already ordered or customer button
                            If oRow("sin") = valueSII Then
                                oldQuantity = oRow("Quantity")
                                If oldQuantity = 1 Then
                                    increaseQuanity -= 1
                                End If
                                newQuantity = oldQuantity + increaseQuanity
                            End If
                            If oRow("sii") = valueSII Then
                                oRow("Quantity") = newQuantity
                                '   the below updates prices for the change
                                oRow("Price") = Format(oRow("Price") * ((newQuantity) / oldQuantity), "####0.00")
                                oRow("TaxPrice") = Format(oRow("TaxPrice") * ((newQuantity) / oldQuantity), "####0.00")
                            End If
                        End If
                    End If
                Next
            End If

            TestQuantityForDisplay(newQuantity)

            Me.CalculateSubTotal()
        End If
        ' End If

    End Sub

    Friend Sub TestQuantityForDisplay(ByVal maxQuantity As Integer)

        If maxQuantity > 9 Then
            csQuantity.Width = 25
            If csItemName.Width = Me.csItemDefaultWidth Then
                csItemName.Width = Me.csItemDefaultWidth - 10
            ElseIf csItemName.Width = (Me.csItemDefaultWidth - 20) Then
                'when couse panel is showing
                csItemName.Width = Me.csItemDefaultWidth - 30
            End If
        End If

    End Sub

    Friend Sub MakeRoomForCourseInfo()

        Me.csCourse.Width = 20
        csItemName.Width = Me.csItemDefaultWidth - 20
    End Sub

    Friend Sub ChangeCourseButton(ByVal btnChange As Integer)

        btnCourse1.BackColor = c7
        btnCourse2.BackColor = c7
        btnCourse3.BackColor = c7
        btnCourse4.BackColor = c7
        '       btnCourse5.BackColor = c7

        Select Case btnChange   'currentTable.CourseNumber
            Case 1
                btnCourse1.BackColor = c9
            Case 2
                btnCourse2.BackColor = c9
            Case 3
                btnCourse3.BackColor = c9
            Case 4
                btnCourse4.BackColor = c9
            Case 5
                '              btnCourse5.BackColor = c9
        End Select


    End Sub



    Friend Function DetermineQuickTicketRow()
        Dim vRow As DataRowView
        Dim count As Integer = 0
        '   this is a one time routine

        For Each vRow In dvQuickTickets
            If vRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                qtRow = count
                Exit Function
            End If
            count += 1
        Next

        Return qtRow

    End Function

    Private Sub ViewStatusClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ViewStatus.Click
        RaiseEvent UC_Hit()

        Dim objButton As KitchenButton = CType(sender, KitchenButton)

        If objButton Is btnViewDetail Then             'all items with detail     by check
            gridViewOrder.DataSource = dvOrder 'dsOrder.Tables("Order")
            currentTable.OrderView = "Detail"

        ElseIf objButton Is btnViewHolds Then         'just holds for all checks
            gridViewOrder.DataSource = dvOrderHolds
            currentTable.OrderView = "Holds"

        ElseIf objButton Is btnViewKitchen Then         'to kitchen for all checks
            gridViewOrder.DataSource = dvKitchen
            currentTable.OrderView = "Sent"

        ElseIf objButton Is btnViewMain Then         'main items
            gridViewOrder.DataSource = dvOrderTopHierarcy
            currentTable.OrderView = "Main Items"

        ElseIf objButton Is btnViewCourse Then         'open
            If Me.csCourse.Width = 0 Then
                Me.csCourse.Width = 20

                If csItemName.Width = Me.csItemDefaultWidth Then
                    csItemName.Width = Me.csItemDefaultWidth - 20
                ElseIf csItemName.Width = (Me.csItemDefaultWidth - 10) Then
                    'when quantity > 10
                    csItemName.Width = Me.csItemDefaultWidth - 30
                End If
            Else
                Me.csCourse.Width = 0
                Me.csItemName.Width = Me.csItemDefaultWidth
            End If

        ElseIf objButton Is btnViewTable Then         'All Checks
            If gridByCheck = True Then
                gridByCheck = False
                btnViewTable.Text = "By Check"
                CreateDataViewsOrder()
                '     RaiseEvent RePopulateDataViews()
                '        CreateDataViews()
            Else
                gridByCheck = True
                btnViewTable.Text = "Table"
                RaiseEvent UpdatingViewsByCheck()

                '                UpdateDataViewsByCheck()
            End If


            Select Case currentTable.OrderView
                Case "Detail"
                    gridViewOrder.DataSource = dvOrder
                Case "Holds"
                    gridViewOrder.DataSource = dvOrderHolds
                Case "Sent"
                    gridViewOrder.DataSource = dvKitchen
                Case "Main Items"
                    gridViewOrder.DataSource = dvOrderTopHierarcy
            End Select

        End If


        '   *******************
        '   table / ByCheck could be better

        UpdateCheckNumberButton()

        '   not sure if we need to update subtotal  ?????????????
        '       CalculateSubTotal()
        ViewStatus.Visible = False
        kitchenCommands.Visible = True

    End Sub

    Private Sub HoldTimerExpired(ByVal sender As Object, ByVal e As System.EventArgs)
        holdTimerActive = False
        holdDoubleClickTimer.Dispose()

    End Sub

    Private Sub btnNewClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim expNum As Int64
        Dim tktNum As Integer
        Dim tabNameString As String

        RaiseEvent EndingItem(True)
        ' this would be reset to false if no longer req modifier
        If currentTable.ReqModifier = True Then Exit Sub

        If qtRow < (dvQuickTickets.Count - 1) Then
            qtRow += 1
            expNum = dvQuickTickets(qtRow)("ExperienceNumber")
            If qtRow = dvQuickTickets.Count - 1 Then
                btnNew.Text = "New"
            End If

            GotoDifferentTable(expNum)
            RaiseEvent NewQuickServiceOrder(expNum)
        Else
            '   here we are at the end creating a new ticket
            If dsOrder.Tables("OpenOrders").Rows.Count = 0 And currentTable.IsClosed = False Then Exit Sub

            Try
                qtRow += 1
                expNum = GenerateOrderTables.CreatingNewTicket()
                currentTable.TabName = "Tkt# " & currentTable.TicketNumber.ToString
                If qtRow > (dvQuickTickets.Count - 1) Then
                    'this is a just in case
                    qtRow = (dvQuickTickets.Count - 1)
                End If
            Catch ex As Exception
                MsgBox("Error Creating New Ticket: " & ex.Message)
            End Try

            Try
                If Not currentTable.TabID = -888 Then
                    If currentTable.MethodUse = "Delivery" Then
                        RaiseEvent DeliverStart()
                    ElseIf currentTable.MethodUse = "Dine In" Then
                        '444   If ds.Tables("TabIdentifier").Rows.Count > 0 Then
                        RaiseEvent DineInStart(True)
                        '444   End If
                    End If
                End If
            Catch ex As Exception
                MsgBox("Error Dine In Start: " & ex.Message)
            End Try

            Try
                GotoDifferentTable(expNum)
            Catch ex As Exception
                MsgBox("Error Goto Different Table: " & ex.Message)
            End Try
            Try
                RaiseEvent NewQuickServiceOrder(expNum)
            Catch ex As Exception
                MsgBox("New Quick Service Order: " & ex.Message)
            End Try

        End If


    End Sub

    Private Sub btnPreviousClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        If qtRow = 0 Then Exit Sub
        RaiseEvent EndingItem(True)
        ' this would be reset to false if no longer req modifier
        If currentTable.ReqModifier = True Then Exit Sub

        Dim expNum As Int64

        qtRow -= 1
        expNum = dvQuickTickets(qtRow)("ExperienceNumber")
        btnNew.Text = "Next"

        GotoDifferentTable(expNum)

        RaiseEvent NewQuickServiceOrder(expNum)

    End Sub

    Private Sub GotoDifferentTable(ByVal expNum As Int64)

        RaiseEvent ClearControls()
        If currentTable.si2 > 0 And currentTable.si2 < 10 Then
            RaiseEvent ClearPanels()
        End If

        If Not typeProgram = "Online_Demo" Then
            GenerateOrderTables.ReleaseCurrentlyHeld()
            'sss     GenerateOrderTables.SaveAvailTabsAndTables()
            GenerateOrderTables.SaveOpenOrderData()
            '     dsOrder.Tables("OpenOrders").Rows.Clear()
        Else
            GenerateOrderTables.SaveOpenOrderDataExceptQuick()
        End If

        currentTable = Nothing

        Dim isCurrentlyHeld As Boolean
        Dim oRow As DataRow

        isCurrentlyHeld = PopulateThisExperience(dvQuickTickets(qtRow)("ExperienceNumber"), False)
        currentTable = New DinnerTable

        currentTable.CurrentMenu = dvQuickTickets(qtRow)("MenuID")
        currentTable.StartingMenu = dvQuickTickets(qtRow)("MenuID")
        currentTable.EmployeeID = dvQuickTickets(qtRow)("EmployeeID")
        For Each oRow In dsEmployee.Tables("AllEmployees").Rows
            If oRow("EmployeeID") = currentTable.EmployeeID Then
                currentTable.EmployeeName = oRow("NickName")
                Exit For
            End If
        Next
        '  currentTable.EmployeeName = dvQuickTickets(qtRow)("NickName")

        currentTable.IsTabNotTable = True
        currentTable.TableNumber = Nothing
        currentTable.TabID = dvQuickTickets(qtRow)("TabID")
        currentTable.TabName = dvQuickTickets(qtRow)("TabName")
        currentTable.TicketNumber = dvQuickTickets(qtRow)("TicketNumber")

        currentTable.ExperienceNumber = dvQuickTickets(qtRow)("ExperienceNumber")
        currentTable.NumberOfChecks = dvQuickTickets(qtRow)("NumberOfChecks")
        currentTable.CheckNumber = 1
        currentTable.NumberOfCustomers = dvQuickTickets(qtRow)("NumberOfCustomers")
        currentTable.LastStatus = dvQuickTickets(qtRow)("LastStatus")
        currentTable.OrderView = dvQuickTickets(qtRow)("LastView")
        currentTable.MethodUse = dvQuickTickets(qtRow)("MethodUse")
        DefineMethodDirection()

        If Not dvQuickTickets(qtRow)("ClosedSubTotal") Is DBNull.Value Then
            currentTable.IsClosed = True
        Else
            currentTable.IsClosed = False
        End If
        currentTable.ItemsOnHold = dvQuickTickets(qtRow)("ItemsOnHold")

        UpdateCheckNumberButton()

    End Sub

    Private Sub btnHoldClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHold.Click
        RaiseEvent UC_Hit()
        '     If dvOrder.Count = 0 Then Exit Sub

        If holdTimerActive = False Then
            holdDoubleClickTimer = New Timer
            holdTimerActive = True
            AddHandler holdDoubleClickTimer.Tick, AddressOf HoldTimerExpired
            holdDoubleClickTimer.Interval = 500
            holdDoubleClickTimer.Start()
        Else
            If isInHoldMode = False Then
                isInHoldMode = True
                btnHold.BackColor = Color.Red
            Else
                isInHoldMode = False
                btnHold.BackColor = c4
            End If
        End If


        Dim rowNum As Integer = OpenOrdersCurrencyMan.Position  'gridViewOrder.CurrentCell.RowNumber
        Dim valueSIN As Integer
        Dim valueSII As Integer
        Try
            valueSIN = CType(gridViewOrder.Item(rowNum, 1), Integer)
            valueSII = CType(gridViewOrder.Item(rowNum, 2), Integer)
        Catch ex As Exception
            Exit Sub
        End Try

        RunHoldRoutine(rowNum, valueSIN, valueSII)

    End Sub

    Private Sub RunHoldRoutine(ByVal rowNum As Integer, ByVal valueSIN As Integer, ByVal valueSII As Integer)

        Dim oRow As DataRow
        Dim newStatus As Integer

        '    If dvOrder(rowNum)("ItemStatus") = 0 Then
        If Me.gridViewOrder.DataSource(rowNum)("ItemStatus") = 0 Then
            newStatus = 1
        ElseIf Me.gridViewOrder.DataSource(rowNum)("ItemStatus") = 1 Then
            newStatus = 0
        ElseIf Me.gridViewOrder.DataSource(rowNum)("ItemStatus") = 2 Then
            Exit Sub
        ElseIf Me.gridViewOrder.DataSource(rowNum)("ItemStatus") = 3 Then
            Exit Sub
        ElseIf Me.gridViewOrder.DataSource(rowNum)("ItemStatus") = 4 Then
            Exit Sub
        End If

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            '          If oRow("sii") = valueSIN Then       this is if we just want to hold if press main item
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("sii") = valueSII Then
                    oRow("ItemStatus") = newStatus
                End If
            End If
        Next

    End Sub

    Private Sub btnVoidClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoid.Click
        '   this is for DELETE not void
        RaiseEvent UC_Hit()
        If dvOrder.Count = 0 Then Exit Sub
        If currentTable.IsClosed = True Then
            Exit Sub
        End If
        If gridViewOrder.Item(gridViewOrder.CurrentRowIndex, 9) < 0 Then Exit Sub

        Dim rowNum As Integer = OpenOrdersCurrencyMan.Position

        Dim valueSIN As Integer
        Dim valueSII As Integer
        Try
            valueSIN = CType(gridViewOrder.Item(rowNum, 1), Integer)
            valueSII = CType(gridViewOrder.Item(rowNum, 2), Integer)
        Catch ex As Exception
            Exit Sub
        End Try

        If valueSIN = valueSII Then
            RaiseEvent ClearControls()
        End If

        DeleteItem(valueSIN, valueSII)

    End Sub

    Friend Sub DeleteItem(ByVal valueSIN As Integer, ByVal valueSII As Integer)
        Dim oRow As DataRow
        Dim pRow As DataRow
        Dim sinArray(dsOrder.Tables("OpenOrders").Rows.Count + 1) As Integer
        Dim count As Integer

        '   this is so when we start deleting rows it won't place new rows as detached and give us error
        If valueSII = valueSIN Then
            '   *** main food
            For Each oRow In dsOrder.Tables("OpenOrders").Rows
                '          oRow.BeginEdit()
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("sii") = valueSIN Then
                        If oRow("ItemID") = 0 And oRow("si2") < 2 Then
                            'this is only cutomer panels (pizza panels will have si2 > 1)
                            Dim cnCount As Integer
                            cnCount = DetermineCnTest(oRow("CustomerNumber"))
                            If cnCount > 1 Then
                                Exit Sub    'we exit if it is a customer panel associated with an order
                            Else
                                currentTable.EmptyCustPanel = 0
                            End If
                        End If
                        If oRow("ItemStatus") < 0 Or oRow("ItemStatus") > 1 Then
                            RaiseEvent DisplayInfo("Can not delete this Item. Please see Manager")
                            Exit Sub
                        End If
                        If oRow("ItemStatus") = 0 Or oRow("itemStatus") = 1 Then
                            sinArray(count) = oRow("sin")
                            '    terminalID(count) = oRow("OpenOrderID")
                            count += 1
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
                If currentTable.si2 = 1 Then
                    RaiseEvent ClearPanels()
                    RaiseEvent ResetPizzaRoutine()
                End If
            End If

            If Not currentTable.Quantity = 1 Then
                currentTable.Quantity = 1
                ChangeCourseButton(currentTable.Quantity)
            End If

            If currentTable.ReqModifier = True Then
                currentTable.ReqModifier = False 'this is because we deleted the item, so who cares
                'we already tested EndingItem, so don't repeat
            Else
                RaiseEvent EndingItem(True)
            End If

            '*****************************************
            'this is for problem jumping customers after deleting
            ' if we delete an item and and we fall to the next customer 
            ' this will prevent us from defaulting to nextCustomer in gridViewOrder
            '(which is called automatically when we delete and change current cell)

            '          currentTable.JumpedToNextCustomer = False

            '       For Each oRow In dsOrder.Tables("OpenOrders").Rows
            '      If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
            '      If oRow("CustomerNumber") = currentTable.CustomerNumber Then
            '      If oRow("sin") > valueSIN Then
            '     currentTable.JumpedToNextCustomer = False
            '     'this means that there still are items below this deleted item
            '     Exit For
            '        End If
            '        End If
            '        If oRow("CustomerNumber") > currentTable.CustomerNumber Then
            '           currentTable.JumpedToNextCustomer = True
            '       End If
            ''       End If
            '           Next


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
                If oRow("ItemStatus") < 0 Or oRow("ItemStatus") > 1 Then
                    RaiseEvent DisplayInfo("Can not delete this Item. Please see Manager")
                    Exit Sub
                End If
                If oRow("ItemID") = 0 And oRow("si2") > 1 Then
                    'for pizza half panels
                    For Each pRow In dsOrder.Tables("OpenOrders").Rows
                        If Not pRow.RowState = DataRowState.Deleted And Not pRow.RowState = DataRowState.Detached Then
                            If pRow("sii") = valueSII And pRow("si2") = oRow("si2") Then
                                If pRow("ItemStatus") < 0 Or pRow("ItemStatus") > 1 Then
                                    RaiseEvent DisplayInfo("Can not delete this Item. Please see Manager")
                                    Exit Sub
                                End If
                                If pRow("ItemStatus") = 0 Or pRow("itemStatus") = 1 Then
                                    sinArray(count) = pRow("sin")
                                    count += 1
                                End If
                            End If
                        End If
                    Next
                End If

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

                    If currentTable.si2 = 2 Or currentTable.si2 = 3 Then
                        'this is for fist or second panel
                        currentTable.si2 = 1
                        Me.gridViewOrder.CurrentRowIndex -= count
                    End If
                Else
                    GenerateOrderTables.DeleteOpenOrdersRowTerminal(oRow)
                End If

            End If
        End If

        CalculateSubTotal()

        '*****************************************
        'this is for problem jumping customers after deleting
        '    currentTable.MiddleOfOrder = False
        currentTable.MarkForNextCustomer = False
        currentTable.NextCustomerNumber = currentTable.CustomerNumber

    End Sub

    Private Sub btnModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModify.Click
        If dvOrder.Count = 0 Then Exit Sub
        RaiseEvent EndingItem(True)
        ' this would be reset to false if no longer req modifier
        If currentTable.ReqModifier = True Then Exit Sub

        If currentTable.IsClosed = True Then
            Exit Sub
        End If

        '   we kept on term_Order b/c that is where we display Modify UC
        RaiseEvent ModifyItem(sender, e)

    End Sub










    Private Sub TotalOrderButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles totalOrder.Click

        If dsOrder.Tables("OpenOrders").Rows.Count = 0 Or currentTable.IsClosed = True Then Exit Sub
        RaiseEvent CloseFast(sender, e)

    End Sub

    Private Sub TotalOrderTaxButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles totalOrderTax.Click

        If dsOrder.Tables("OpenOrders").Rows.Count = 0 Or currentTable.IsClosed = True Then Exit Sub
        RaiseEvent CloseFast(sender, e)

    End Sub

    Private Sub CheckNumberButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkNumberButton.Click

        If currentTable.IsClosed = True Then Exit Sub

        'currently forcing send in SplitChecks after Exit/Release
        '444    RaiseEvent SendOrder(False)

        RaiseEvent CloseCheck(sender, e)

    End Sub

    Friend Sub UpdateCheckNumberButton()
        Dim orderHeaderText As String

        If currentTerminal.TermMethod = "Quick" Or currentTable.TicketNumber > 0 Then
            '444      If Not currentTerminal.TermMethod = "Quick" Then
            Dim vrow As DataRowView
            currentTerminal.NumOpenTickets = 0
            If currentTerminal.TermMethod = "Quick" Then
                For Each vrow In dvQuickTickets
                    If vrow("ClosedSubTotal") Is DBNull.Value Then
                        currentTerminal.NumOpenTickets += 1
                    End If
                Next
            Else
                For Each vrow In dvQuickTickets
                    If vrow("ClosedSubTotal") Is DBNull.Value And vrow("EmployeeID") = currentTable.EmployeeID Then
                        currentTerminal.NumOpenTickets += 1
                    End If
                Next
            End If

            '444 End If
            checkNumberButton.Text = currentTerminal.NumOpenTickets.ToString & "   Open"

            If currentTable.NumberOfChecks = 1 Then
                orderHeaderText = "                  " & currentTable.OrderView.ToString    '18 spaces
                Me.lblOrderView.BackColor = c6
                Me.lblOrderView.ForeColor = c3
            Else
                orderHeaderText = "Check   " & currentTable.CheckNumber & " of " & currentTable.NumberOfChecks
                Me.lblOrderView.BackColor = c1
                Me.lblOrderView.ForeColor = c2
            End If

        Else
            If currentTable.NumberOfChecks = 1 Then
                checkNumberButton.Text = "Check   " & currentTable.CheckNumber & " of " & currentTable.NumberOfChecks
                orderHeaderText = "                  " & currentTable.OrderView.ToString    '18 spaces
                Me.lblOrderView.BackColor = c6
                Me.lblOrderView.ForeColor = c3
            Else
                If gridByCheck = False Then
                    checkNumberButton.Text = "Check   All of " & currentTable.NumberOfChecks
                    orderHeaderText = "                  " & currentTable.OrderView.ToString    '18 spaces
                    Me.lblOrderView.BackColor = c6
                    Me.lblOrderView.ForeColor = c3
                Else
                    checkNumberButton.Text = "Check   " & currentTable.CheckNumber & " of " & currentTable.NumberOfChecks 'currentTable._checkCollection.Count
                    orderHeaderText = "  " & currentTable.OrderView.ToString     '6 spaces
                    Me.lblOrderView.BackColor = c1
                    Me.lblOrderView.ForeColor = c2
                End If
            End If

        End If

        DisplayDirectionLabel(orderHeaderText)

    End Sub










    Friend Sub DisplayDownButtonText()
        Dim downString As String

        '     chrString = ChrW(&HDACE)
        downString = "Down"
        Me.btnDown.Text = downString

    End Sub

    Private Sub DisplayUpButtonText()
        Dim upString As String

        upString = "Up"
        Me.btnUp.Text = upString

    End Sub

    Private Sub DisplayDirectionLabel(ByVal orderviewText As String)

        Me.lblOrderView.Text = orderviewText

    End Sub

    Private Sub ChangeCheckNumberLabelHit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblOrderView.Click

        RaiseEvent UC_Hit()
        If newlyCreatedCheck = True Then
            'this is when we create a check in split check but add no items
            DoWeRemoveNewCheck()
        End If

        If currentTable.NumberOfChecks > 1 Then
            If gridByCheck = False Then
                gridByCheck = True
                btnViewTable.Text = "Table"
                currentTable.CheckNumber = 0    'so we start at # 1
            End If
            GotoNextCheckNumber()
            UpdateCheckNumberButton()
            CalculateSubTotal()
            RaiseEvent UpdatingViewsByCheck()
        End If

    End Sub
    Private Sub DoWeRemoveNewCheck()

        newlyCreatedCheck = False
        If dvOrder.Count = 0 Then
            currentTable.NumberOfChecks -= 1
            RecalculateCheckNumber(currentTable.ExperienceNumber, -1)
            GotoNextCheckNumber()
        End If

    End Sub
    Private Sub btnOrderViewDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click

        RaiseEvent UC_Hit()
        '???       RaiseEvent EndOfItem(True)
        ' this would be reset to false if no longer req modifier
        '   If currentTable.ReqModifier = True Then Exit Sub

        If OpenOrdersCurrencyMan.Position < Me.gridViewOrder.DataSource.count Then
            If Me.gridViewOrder.DataSource.count - OpenOrdersCurrencyMan.Position > 15 Then
                OpenOrdersCurrencyMan.Position += 10
            ElseIf Me.gridViewOrder.DataSource.count - OpenOrdersCurrencyMan.Position > 10 Then
                OpenOrdersCurrencyMan.Position += 5
            ElseIf Me.gridViewOrder.DataSource.count - OpenOrdersCurrencyMan.Position > 5 Then
                OpenOrdersCurrencyMan.Position += 2
            Else
                OpenOrdersCurrencyMan.Position += 1
            End If

            Me.gridViewOrder.ScrollToRow(OpenOrdersCurrencyMan.Position)
        End If

    End Sub

    Private Sub btnOrderViewUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click

        RaiseEvent UC_Hit()
        If OpenOrdersCurrencyMan.Position > 0 Then
            If OpenOrdersCurrencyMan.Position - 15 > 0 Then
                OpenOrdersCurrencyMan.Position -= 15
            ElseIf OpenOrdersCurrencyMan.Position - 10 > 0 Then
                OpenOrdersCurrencyMan.Position -= 10
            ElseIf OpenOrdersCurrencyMan.Position - 5 > 0 Then
                OpenOrdersCurrencyMan.Position -= 5
            Else
                OpenOrdersCurrencyMan.Position -= 1
            End If

            Me.gridViewOrder.ScrollToRow(OpenOrdersCurrencyMan.Position)
        End If
    End Sub

    Friend Sub SplitCheckClosed222()


        btnViewTable.Text = "Table"
        UpdateCheckNumberButton()
        CalculateSubTotal()

        Me.gridViewOrder.DataSource = dvOrder
        currentTable.OrderView = "Detailed Order"

    End Sub

    Private Sub btnStatusClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click

        RaiseEvent UC_Hit()
        kitchenCommands.Visible = False
        ViewStatus.Visible = True

    End Sub

    Friend Sub CalculateSubTotal()

        Dim dvByCheck As New DataView
        Dim UnNamedTabPayments As Decimal

        With dvByCheck
            .Table = dsOrder.Tables("OpenOrders")
            .RowFilter = "CheckNumber = '" & currentTable.CheckNumber & "'"
        End With

        '     If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
        If dvByCheck.Count > 0 Then
            If gridByCheck = True Then
                currentTable.SubTotal = dsOrder.Tables("OpenOrders").Compute("Sum(Price)", "CheckNumber = '" & currentTable.CheckNumber & "'")
                currentTable.SubTax = dsOrder.Tables("OpenOrders").Compute("Sum(TaxPrice)+ SUM(SinTax)", "CheckNumber = '" & currentTable.CheckNumber & "'")
            Else
                currentTable.SubTotal = dsOrder.Tables("OpenOrders").Compute("Sum(Price)", "")
                currentTable.SubTax = dsOrder.Tables("OpenOrders").Compute("Sum(TaxPrice)+ SUM(SinTax)", "")
            End If
        Else
            currentTable.SubTotal = 0
            currentTable.SubTax = 0
        End If

        totalOrder.Text = Format(currentTable.SubTotal + currentTable.SubTax, "##,###.00")
        totalOrderTax.Text = Format(currentTable.SubTax, "##,###.00")
        If currentTable.IsClosed = True Then
            totalOrder.BackColor = c7
            totalOrderTax.BackColor = c7
        Else
            totalOrder.BackColor = c3
            totalOrderTax.BackColor = c3
        End If
        '      If currentTable.TabID < 0 Then
        '   for unNamedSubTotal
        '     If gridByCheck = True Then
        '    UnNamedTabPayments = dsOrder.Tables("PaymentsAndCredits").Compute("Sum(PaymentAmount)", "CheckNumber = '" & currentTable.CheckNumber & "'")
        '   Else
        '      UnNamedTabPayments = dsOrder.Tables("PaymentsAndCredits").Compute("Sum(PaymentAmount)", "")
        ' End If
        '   currentTable.SubTotal -= UnNamedTabPayments
        '  End If

    End Sub



    Private Sub PlaceItemOnHold222(ByVal rowNum As Integer, ByVal valueSIN As Integer)
        '   this is for HOLD ALL ITEMS
        '   not used currently

        Dim oRow As DataRow
        Dim newStatus As Integer

        '   DataSource may be either dtOrder or specific DataViews
        '   so the testing is done on the same information(view)
        If gridViewOrder.DataSource Is dsOrder.Tables("OpenOrders") Then
            dvOrder = New DataView(dsOrder.Tables("OpenOrders"))
            '   might have to add cuurenttable.orderview = "dvOrder"
        End If

        If dvOrder(rowNum)("ItemStatus") = 0 Then
            newStatus = 1
        ElseIf dvOrder(rowNum)("ItemStatus") = 1 Then
            newStatus = 0
        ElseIf dvOrder(rowNum)("ItemStatus") = 2 Then
            Exit Sub
        ElseIf dvOrder(rowNum)("ItemStatus") = 3 Then
            Exit Sub
        ElseIf dvOrder(rowNum)("ItemStatus") = 4 Then
            Exit Sub
        End If

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("sii") = valueSIN Then
                    oRow("ItemStatus") = newStatus
                End If
            End If
        Next

    End Sub

    Private Sub btnSendClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click

        '444     RaiseEvent EndingItem(False)
        ' this would be reset to false if no longer req modifier
        '444     If currentTable.ReqModifier = True Then Exit Sub

        RaiseEvent SendOrder(True)

    End Sub

    Private Sub btnLeaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLeave.Click
        RaiseEvent UC_Hit()

        If newlyCreatedCheck = True Then
            'this is when we create a check in split check but add no items
            DoWeRemoveNewCheck()
        End If
        RaiseEvent EndingItem(False)
        ' this would be reset to false if no longer req modifier
        If currentTable.ReqModifier = True Then Exit Sub

        RaiseEvent LeaveOrderView()
    End Sub


End Class
