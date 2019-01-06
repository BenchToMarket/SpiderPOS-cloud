Public Class SplitGridPanel
    Inherits System.Windows.Forms.UserControl

    '   Dim sql As New DataSet_Builder.SQLHelper(connectserver)


    Friend WithEvents splitGrid As ListBox
    Friend WithEvents btnSelectCheck As Button
    Friend WithEvents btnCloseCheck As Button
    Friend lblRemainingBalance As Label
    Friend txtRemainingBalance As TextBox

    Dim dvSplitCheck As DataView
    Dim dvSplitCheckPrices As DataView
    Dim dvPaymentsAndCreditsSplit As DataView

    Dim checkTotal As Decimal
    Dim paymentTotal As Decimal
    Friend remainingBalance As Decimal

    Private indexOfItemUnderMouseToDrag As Integer
    Private indexOfItemUnderMouseToDrop As Integer

    Private dragBoxFromMouseDown As Rectangle
    Private screenOffset As Point

    Private MyNoDropCursor As Cursor
    Private MyNormalCursor As Cursor

    Dim _checkIndex As Integer
    Dim _totalGrids As Integer
    Dim totalRows As Integer
    Dim totalColumns As Integer
    Dim _selectedItemName As String
    Dim gridWidth As Single
    Dim gridHeight As Single

 


    Friend Property CheckIndex() As Integer
        Get
            Return _checkIndex
        End Get
        Set(ByVal Value As Integer)
            _checkIndex = Value
        End Set
    End Property

    Friend Property TotalGrids() As Integer
        Get
            Return _totalGrids
        End Get
        Set(ByVal Value As Integer)
            _totalGrids = Value
        End Set
    End Property

    Friend Property SelectedItemName() As String
        Get
            Return _selectedItemName
        End Get
        Set(ByVal Value As String)
            _selectedItemName = Value
        End Set
    End Property

    Friend Property SIN_Split() As Integer
        Get
            Return sinDragSource
        End Get
        Set(ByVal Value As Integer)
            sinDragSource = Value
        End Set
    End Property

    Event GridMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
    Event GridMouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
    Event GridMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
    Event GridDragOver(ByVal sender As Object, ByVal e As DragEventArgs)
    Event GridDragDrop(ByVal sender As Object, ByVal e As DragEventArgs)
    Event GridQueryContinueDrag(ByVal sender As Object, ByVal e As QueryContinueDragEventArgs)
    Event ButtonSelectClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Event ButtonCloseClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Event ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
    Event ResetTimerFromPanel()

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal gridNumber As Integer, ByVal tg As Integer)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _checkIndex = gridNumber
        _totalGrids = tg
        If TotalGrids = 4 Then
            totalRows = 1
            totalColumns = 4
        Else
            totalRows = 2
            totalColumns = 5
        End If

        InitializeOther(gridNumber)
        CreateSplitDataView(gridNumber)


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
        'SplitGridPanel
        '
        Me.Name = "SplitGridPanel"
        Me.Size = New System.Drawing.Size(264, 336)

    End Sub

#End Region

    Private Sub InitializeOther(ByVal gridnumber As Integer)
        Dim width As Single = (ssX - 60) / totalColumns
        Dim height As Single = (ssY - 80) / totalRows
        Dim splitSpace As Single = 10
        Dim x As Single
        Dim y As Single
        Dim btnWidth As Single = width * 0.44
        Dim btnHeight As Single = 45    '0.1 * height
        Dim btnSelectX As Single = (width * 0.04) 'btnWidth / 3
        Dim btnRemoveX As Single = (width * 0.08) + btnWidth '(btnWidth * (2 / 3)) + btnWidth
        Dim btnLocationY As Single = height - 50 'height * 0.85
        Dim balanceLocationY = height - 80   'height * 0.75

        If gridnumber < 6 Then
            x = (splitSpace + width) * gridnumber - width
            y = splitSpace
        ElseIf gridnumber < 11 Then
            x = (splitSpace + width) * (gridnumber - 5) - width
            y = (2 * splitSpace) + height
        End If

        '       Dim panelString = "splitGridPanel" & gridNumber
        '      Dim gridString = "splitGrid" & gridNumber


        splitGrid = New ListBox
        lblRemainingBalance = New Label
        txtRemainingBalance = New TextBox
        btnSelectCheck = New Button
        btnCloseCheck = New Button

        Me.Size = New Size(width, height)
        Me.Location = New Point(x, y)
        Me.BackColor = c2 ' c6

        splitGrid.Size = New Size(width * 0.98, height - 85) '* 0.75)
        splitGrid.Location = New Point(width * 0.01, height * 0.01)
        If TotalGrids = 4 Then
            splitGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Else
            splitGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
        splitGrid.BackColor = c2
        splitGrid.AllowDrop = True
        splitGrid.BorderStyle = BorderStyle.Fixed3D
        splitGrid.ForeColor = c3

        '   444 tried to make a larger move area
        '      Dim r As Rectangle
        '     r.Height = 100
        '    splitGrid.RectangleToClient(r)

        lblRemainingBalance.Size = New Size(btnWidth, btnHeight / 2)
        lblRemainingBalance.Location = New Point(btnSelectX, balanceLocationY)
        Me.lblRemainingBalance.ForeColor = c1

        txtRemainingBalance.Size = New Size(btnWidth, btnHeight / 2)
        txtRemainingBalance.Location = New Point(btnRemoveX, balanceLocationY)
        txtRemainingBalance.TextAlign = HorizontalAlignment.Right

        btnSelectCheck.Size = New Size(btnWidth, btnHeight)
        btnSelectCheck.Location = New Point(btnSelectX, btnLocationY)
        btnSelectCheck.BackColor = c6
        btnSelectCheck.ForeColor = c3

        btnCloseCheck.Size = New Size(btnWidth, btnHeight)
        btnCloseCheck.Location = New Point(btnRemoveX, btnLocationY)
        btnCloseCheck.BackColor = c6
        btnCloseCheck.ForeColor = c3


        Me.lblRemainingBalance.Text = "Balance: "

        Me.Controls.Add(splitGrid)
        Me.Controls.Add(Me.lblRemainingBalance)
        Me.Controls.Add(Me.txtRemainingBalance)
        Me.Controls.Add(btnSelectCheck)
        Me.Controls.Add(btnCloseCheck)

        dvSplitCheck = New DataView
        dvSplitCheckPrices = New DataView
        dvPaymentsAndCreditsSplit = New DataView


    End Sub



    Friend Sub CreateSplitDataView(ByVal gridNumber As Integer)
        Dim vRow As DataRowView

        '444     dvSplitCheck = New DataView

        splitGrid.Items.Clear()
        '     Dim itemPanel As Label            tried to create a label to grab but did not work


        With dvSplitCheck
            .Table = dsOrder.Tables("OpenOrders")
            .RowFilter = "CheckNumber = '" & gridNumber & "' AND sii = sin"
            .RowStateFilter = DataViewRowState.CurrentRows
            .Sort = "CustomerNumber, sin"
        End With


        For Each vRow In dvSplitCheck
            '            itemPanel = New Label
            '            With itemPanel
            '            .Size = New Size(splitGrid.Width, 75)
            '            .BackColor = c9
            '           End With
            '
            '           itemPanel.Text = (vRow("ItemName"))
            '          splitGrid.Items.Add(itemPanel)
            splitGrid.Items.Add(vRow("ItemName"))

        Next

        '444      dvSplitCheckPrices = New DataView

        With dvSplitCheckPrices
            .Table = dsOrder.Tables("OpenOrders")
            .RowFilter = "CheckNumber = '" & gridNumber & "'"
            .RowStateFilter = DataViewRowState.CurrentRows
            '     .Sort = "CustomerNumber, sin"
        End With

        '444      dvPaymentsAndCreditsSplit = New DataView

        With dvPaymentsAndCreditsSplit
            .Table = dsOrder.Tables("PaymentsAndCredits")
            .RowFilter = "CheckNumber = '" & gridNumber & "'"
            .RowStateFilter = DataViewRowState.CurrentRows
            '     .Sort = "CustomerNumber, sin"
        End With

        CalculatePriceTaxAndRemaining()
        DisplayButtonText()


    End Sub

    Friend Sub CalculatePriceTaxAndRemaining()
        Dim vRow As DataRowView
        checkTotal = 0
        paymentTotal = 0

        For Each vRow In dvSplitCheckPrices
            checkTotal += vRow("Price")
            checkTotal += vRow("TaxPrice")
            checkTotal += vRow("SinTax")
        Next

        For Each vRow In dvPaymentsAndCreditsSplit
            If vRow("Applied") = True Then
                paymentTotal += vRow("PaymentAmount")
            End If
        Next

        checkTotal = Format(checkTotal, "####0.00")
        paymentTotal = Format(paymentTotal, "####0.00")
        remainingBalance = checkTotal - paymentTotal

        Me.txtRemainingBalance.Text = remainingBalance

        '     MsgBox(checkTotal, , "CheckTotal")
        '    MsgBox(paymentTotal, , "Payment")
        '   MsgBox(remainingBalance, , "Remain")


    End Sub

    Private Sub DisplayButtonText()
        If Me.dvSplitCheck.Count = 0 Then
            btnSelectCheck.Text = "Create"
            btnCloseCheck.Text = ""
        Else
            btnSelectCheck.Text = "Select"
            btnCloseCheck.Text = "Close"
        End If

        '       Dim maxCN As Integer
        '      maxCN = (dsOrder.Tables("OpenOrders")).Compute("Max(CheckNumber)", Nothing)

        '     If CheckIndex > maxCN Then                'currentTable.NumberOfChecks Then
        '    btnSelectCheck.Text = "Create"
        '   Else
        '      If Me.dvSplitCheck.Count = 0 Then
        '      btnSelectCheck.Text = "Create"
        '     '                btnSelectCheck.Text = "Select"
        '    '               btnCloseCheck.Text = "Remove"
        '   Else
        '      btnSelectCheck.Text = "Select"
        '     btnCloseCheck.Text = "Close"
        'End If
        '  End If

    End Sub

    Private Sub ListDragSource_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles splitGrid.MouseDown
        RaiseEvent ResetTimerFromPanel()

        Dim oRow As DataRow

        ' Get the index of the item the mouse is below.
        indexOfItemUnderMouseToDrag = splitGrid.IndexFromPoint(e.X, e.Y)

        '   *** having problems if we move an item away from its customer panel
        '           then we move the panel.. program places all customer items with 
        '           panel but does not remove items from other panel
        Try
            If (indexOfItemUnderMouseToDrag <> ListBox.NoMatches) Then

                '   not sure if this should be before above stmt?
                sinDragSource = dvSplitCheck(indexOfItemUnderMouseToDrag)("sin")

                'this determines if we are selecting a customer panel
                '   if yes, we place the customer number in memory
                '   if not, we assign 0
                For Each oRow In dsOrder.Tables("OpenOrders").Rows
                    If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                        If oRow("sin") = sinDragSource Then
                            If oRow("ItemID") = 0 Then
                                ' is a customer panel
                                movingCustomer = oRow("CustomerNumber")
                            Else
                                movingCustomer = 0
                            End If
                        End If
                    End If
                Next

                '   for split checks name only
                _selectedItemName = dvSplitCheck(indexOfItemUnderMouseToDrag)("ItemName")

                '******           RaiseEvent ItemSelected(Me, e) 'sinDragSource, e)

                ' Remember the point where the mouse down occurred. The DragSize indicates
                ' the size that the mouse can move before a drag event should be started.                
                Dim dragSize As Size = SystemInformation.DragSize

                ' Create a rectangle using the DragSize, with the mouse position being
                ' at the center of the rectangle.
                dragBoxFromMouseDown = New Rectangle(New Point(e.X - (dragSize.Width / 2), _
                                                                e.Y - (dragSize.Height / 2)), dragSize)
            Else
                ' Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty
            End If

            RaiseEvent GridMouseDown(sender, e)
        Catch ex As Exception

        End Try


    End Sub


    Private Sub ListDragSource_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles splitGrid.MouseMove

        If ((e.Button And MouseButtons.Left) = MouseButtons.Left) Then

            ' If the mouse moves outside the rectangle, start the drag.
            If (Rectangle.op_Inequality(dragBoxFromMouseDown, Rectangle.Empty) And _
                Not dragBoxFromMouseDown.Contains(e.X, e.Y)) Then


                ' The screenOffset is used to account for any desktop bands 
                ' that may be at the top or left side of the screen when 
                ' determining when to cancel the drag drop operation.
                screenOffset = SystemInformation.WorkingArea.Location

                ' Proceed with the drag and drop, passing in the list item.                    
                Dim dropEffect As DragDropEffects = splitGrid.DoDragDrop(splitGrid.Items(indexOfItemUnderMouseToDrag), _
                                                                              DragDropEffects.All Or DragDropEffects.Link)

                ' If the drag operation was a move then remove the item.
                If (dropEffect = DragDropEffects.Move) Then
                    splitGrid.Items.RemoveAt(indexOfItemUnderMouseToDrag)

                    'if > 0 then we have selected to move customer panel
                    ' and all its order for that customer
                    If movingCustomer > 0 Then
                        CreateSplitDataView(CheckIndex)
                    End If

                    CalculatePriceTaxAndRemaining()

                    If splitGrid.Items.Count = 0 Then
                        currentTable.NumberOfChecks -= 1
                        RecalculateCheckNumber(currentTable.ExperienceNumber, -1)
                    End If
                    ' Select the previous item in the list as long as the list has an item.
                    If (indexOfItemUnderMouseToDrag > 0) Then
                        splitGrid.SelectedIndex = indexOfItemUnderMouseToDrag - 1

                    ElseIf (splitGrid.Items.Count > 0) Then
                        ' Selects the first item.
                        splitGrid.SelectedIndex = 0
                    End If

                End If
            End If
        End If

        RaiseEvent GridMouseMove(sender, e)
    End Sub

    Private Sub ListDragTarget_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles splitGrid.DragOver

        ' Determine whether string data exists in the drop data. If not, then
        ' the drop effect reflects that the drop cannot occur.
        If Not (e.Data.GetDataPresent(GetType(System.String))) Then

            e.Effect = DragDropEffects.None
            '            DropLocationLabel.Text = "None - no string data."
            Return
        End If

        ' Set the effect based upon the KeyState.
        If ((e.KeyState And (8 + 32)) = (8 + 32) And _
            (e.AllowedEffect And DragDropEffects.Link) = DragDropEffects.Link) Then
            ' KeyState 8 + 32 = CTL + ALT

            ' Link drag and drop effect.
            e.Effect = DragDropEffects.Link

        ElseIf ((e.KeyState And 32) = 32 And _
            (e.AllowedEffect And DragDropEffects.Link) = DragDropEffects.Link) Then

            ' ALT KeyState for link.
            e.Effect = DragDropEffects.Link

        ElseIf ((e.KeyState And 4) = 4 And _
            (e.AllowedEffect And DragDropEffects.Move) = DragDropEffects.Move) Then

            ' SHIFT KeyState for move.
            e.Effect = DragDropEffects.Move

        ElseIf ((e.KeyState And 8) = 8 And _
            (e.AllowedEffect And DragDropEffects.Copy) = DragDropEffects.Copy) Then

            ' CTL KeyState for copy.
            e.Effect = DragDropEffects.Copy

        ElseIf ((e.AllowedEffect And DragDropEffects.Move) = DragDropEffects.Move) Then

            ' By default, the drop action should be move, if allowed.
            e.Effect = DragDropEffects.Move

        Else
            e.Effect = DragDropEffects.None
        End If

        ' Gets the index of the item the mouse is below. 

        ' The mouse locations are relative to the screen, so they must be 
        ' converted to client coordinates.

        indexOfItemUnderMouseToDrop = _
            splitGrid.IndexFromPoint(splitGrid.PointToClient(New Point(e.X, e.Y)))

        ' Updates the label text.
        If (indexOfItemUnderMouseToDrop <> ListBox.NoMatches) Then

            '            DropLocationLabel.Text = "Drops before item #" & (indexOfItemUnderMouseToDrop + 1)
        Else
            '           DropLocationLabel.Text = "Drops at the end."
        End If

        RaiseEvent GridDragOver(sender, e)
    End Sub

    Private Sub ListDragTarget_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles splitGrid.DragDrop
        ' Ensures that the list item index is contained in the data.

        'placed here just for a test if we are have errors when moving too fast
        '   RemoveHandler splitGrid.MouseDown, AddressOf ListDragSource_MouseDown

        If (e.Data.GetDataPresent(GetType(System.String))) Then

            Dim item As Object = CType(e.Data.GetData(GetType(System.String)), System.Object)
            checkDragTarget = _checkIndex

            ' Perform drag and drop, depending upon the effect.
            If (e.Effect = DragDropEffects.Copy Or _
                e.Effect = DragDropEffects.Move) Then

                ' Insert the item.
                If (indexOfItemUnderMouseToDrop <> ListBox.NoMatches) Then
                    splitGrid.Items.Insert(indexOfItemUnderMouseToDrop, item)
                Else
                    splitGrid.Items.Add(item)
                End If

                If splitGrid.Items.Count = 1 Then
                    currentTable.NumberOfChecks += 1
                    RecalculateCheckNumber(currentTable.ExperienceNumber, 1)
                    '    currentTable.CustomerNumber = CheckIndex
                    currentTable.CheckNumber = CheckIndex
                End If
                '               MsgBox("drop before update")
                UpdateCheckNumbersForOrder()
            End If
            '            MsgBox("drop after update")

            CalculatePriceTaxAndRemaining()

            RaiseEvent GridDragDrop(sender, e)
            '            AddHandler splitGrid.MouseDown, AddressOf ListDragSource_MouseDown

        End If
    End Sub

    Private Sub ListDragSource_QueryContinueDrag(ByVal sender As Object, ByVal e As QueryContinueDragEventArgs) Handles splitGrid.QueryContinueDrag
        ' Cancel the drag if the mouse moves off the form.
        Dim lb As ListBox = CType(sender, System.Windows.Forms.ListBox)

        If Not (lb Is Nothing) Then

            Dim f As Form = lb.FindForm()

            ' Cancel the drag if the mouse moves off the form. The screenOffset
            ' takes into account any desktop bands that may be at the top or left
            ' side of the screen.
            If (((Control.MousePosition.X - screenOffset.X) < f.DesktopBounds.Left) Or _
                ((Control.MousePosition.X - screenOffset.X) > f.DesktopBounds.Right) Or _
                ((Control.MousePosition.Y - screenOffset.Y) < f.DesktopBounds.Top) Or _
                ((Control.MousePosition.Y - screenOffset.Y) > f.DesktopBounds.Bottom)) Then

                e.Action = DragAction.Cancel
            End If
        End If

        RaiseEvent GridQueryContinueDrag(sender, e)
    End Sub


    Private Sub UpdateCheckNumbersForOrder()
        Dim oRow As DataRow

        If movingCustomer > 0 Then
            MoveEntireCustomer(movingCustomer)
        Else
            ' this is to move just an item with all attachments
            For Each oRow In dsOrder.Tables("OpenOrders").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("sii") = sinDragSource Then
                        oRow("CheckNumber") = checkDragTarget
                    End If
                End If
            Next
        End If

    End Sub

    Private Sub MoveEntireCustomer(ByVal cn As Integer)
        Dim oRow As DataRow

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("CustomerNumber") = cn Then
                    oRow("CheckNumber") = checkDragTarget
                End If
            End If
        Next

        CreateSplitDataView(checkDragTarget)

    End Sub

    Private Sub btnSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectCheck.Click

        If Me.dvSplitCheck.Count = 0 Then   'And CheckIndex > currentTable.NumberOfChecks Then
            currentTable.NumberOfChecks += 1
            RecalculateCheckNumber(currentTable.ExperienceNumber, 1)
            '    currentTable.CustomerNumber = CheckIndex
            currentTable.CheckNumber = CheckIndex
            newlyCreatedCheck = True    'we will remove check if no items ordered
        Else
            '           If btnSelectCheck.Text = "Create" Then
            '           '   this is when we have items but have not created the check yet
            '          currentTable.NumberOfChecks += 1
            '         RecalculateCheckNumber(currentTable.ExperienceNumber, 1)
            '    End If
        End If

        RaiseEvent ButtonSelectClick(CheckIndex, e)

    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseCheck.Click
        If splitGrid.Items.Count = 0 Then
            If Me.btnCloseCheck.Text = "Remove" Then
                If CheckIndex > currentTable.NumberOfChecks Then

                End If
                currentTable.NumberOfChecks -= 1
                RecalculateCheckNumber(currentTable.ExperienceNumber, -1)
                Me.btnSelectCheck.Text = "Create"
                Me.btnCloseCheck.Text = ""

                '          Else
                '             MsgBox("You can not close a empty Check.")
            End If
            '      Return
        Else
            If btnCloseCheck.Text = "Close" Then
                '   verifys check was created
                '          Dim vRow As DataRowView
                '         For Each vRow In dvSplitCheck
                '        If vRow("ItemStatus") < 2 Then
                '              MsgBox("All Items must be at least Sent to the Kitchen before closing a Check")
                '              Return
                '       End If
                '         Next
                RaiseEvent ButtonCloseClick(CheckIndex, e)
            End If
        End If

    End Sub

    Private Sub SplitGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles splitGrid.Click

        RaiseEvent ItemSelected(Me, e)

    End Sub



End Class


