Imports System.ComponentModel

Public Class CheckTotal_UC
    Inherits System.Windows.Forms.UserControl

    Dim pnlDirection As Panel
    Dim WithEvents btnDown As Button
    Dim WithEvents btnUp As Button

    Friend WithEvents grdCloseCheck As New OrderGrid
    Dim ClosingCheckCurrencyMan As CurrencyManager

    Dim taxIDInteger() As Integer
    Friend taxName() As String
    Friend chkSubTotal As Decimal
    Friend taxTotal() As Decimal

    Friend checkTax As Decimal
    Friend checkSinTax As Decimal
    Friend chkFood As Decimal
    Friend chkDrinks As Decimal
    Dim _autoGratuity As Boolean
    Dim autoGratuityAmount As Decimal


    Dim remainingBalance As Decimal
    Dim _totalCheckBalance As Decimal
    Dim CashPaymentTendered As Boolean

    Friend Property AutoGratuity() As Boolean
        Get
            Return _autoGratuity
        End Get
        Set(ByVal Value As Boolean)
            _autoGratuity = Value
        End Set
    End Property

    Friend Property TotalCheckBalance() As Decimal
        Get
            Return _totalCheckBalance
        End Get
        Set(ByVal Value As Decimal)
            _totalCheckBalance = Value
        End Set
    End Property

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
    Friend WithEvents lstCloseCheckTotals As System.Windows.Forms.ListView
    Friend WithEvents clmCheckTotalName As System.Windows.Forms.ColumnHeader
    Friend WithEvents clmCheckTotalAmount As System.Windows.Forms.ColumnHeader
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lstCloseCheckTotals = New System.Windows.Forms.ListView
        Me.clmCheckTotalName = New System.Windows.Forms.ColumnHeader
        Me.clmCheckTotalAmount = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'lstCloseCheckTotals
        '
        Me.lstCloseCheckTotals.BackColor = System.Drawing.SystemColors.WindowText
        Me.lstCloseCheckTotals.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clmCheckTotalName, Me.clmCheckTotalAmount})
        Me.lstCloseCheckTotals.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lstCloseCheckTotals.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstCloseCheckTotals.Location = New System.Drawing.Point(0, 520)
        Me.lstCloseCheckTotals.Name = "lstCloseCheckTotals"
        Me.lstCloseCheckTotals.Size = New System.Drawing.Size(310, 136)
        Me.lstCloseCheckTotals.TabIndex = 11
        Me.lstCloseCheckTotals.View = System.Windows.Forms.View.Details
        '
        'clmCheckTotalName
        '
        Me.clmCheckTotalName.Text = ""
        Me.clmCheckTotalName.Width = 220
        '
        'clmCheckTotalAmount
        '
        Me.clmCheckTotalAmount.Text = ""
        Me.clmCheckTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.clmCheckTotalAmount.Width = 70
        '
        'CheckTotal_UC
        '
        Me.Controls.Add(Me.lstCloseCheckTotals)
        Me.Name = "CheckTotal_UC"
        Me.Size = New System.Drawing.Size(310, 656)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub InitializeOther()

        ClosingCheckCurrencyMan = Me.BindingContext(dsOrder.Tables("OpenOrders"))

        If currentTable.AutoGratuity > 0 Then
            _autoGratuity = True
        End If

        DisplayDirectionPanel()
        DisplayCloseGrid()
        PopulateCloseGrid(dvOrder) 'dvClosingCheck)


        AddGridCloseCheckColumns()
        CalculatePriceAndTax(currentTable.CheckNumber)
        '       AttachTotalsToTotalView()

    End Sub

    Private Sub DisplayDirectionPanel()
        Dim dirHeight As Single
        Dim dirButtonWidth As Single

        pnlDirection = New Panel
        btnDown = New Button
        btnUp = New Button

        dirHeight = 40  'Me.Height * 0.04
        dirButtonWidth = Me.Width / 2

        Me.pnlDirection.Location = New Point(0, 0)
        Me.pnlDirection.Size = New Size(Me.Width, dirHeight)
        Me.pnlDirection.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.btnDown.Location = New Point(0, 0)
        Me.btnUp.Location = New Point((dirButtonWidth), 0)

        Me.btnDown.Size = New Size(dirButtonWidth, dirHeight)
        Me.btnUp.Size = New Size(dirButtonWidth, dirHeight)

        Me.btnDown.BackColor = c6
        Me.btnUp.BackColor = c6
        '    Me.btnDown.BackgroundImage = CType(Resources.GetObject("pnlMgrByItem.BackgroundImage"), System.Drawing.Image)

        Me.btnDown.ForeColor = c3
        Me.btnUp.ForeColor = c3

        Me.btnDown.TextAlign = ContentAlignment.BottomCenter
        Me.btnUp.TextAlign = ContentAlignment.TopCenter

        Me.btnDown.Text = "Down"
        Me.btnUp.Text = "Up"

        Me.pnlDirection.Controls.Add(Me.btnDown)
        Me.pnlDirection.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.pnlDirection)

    End Sub


    Private Sub DisplayCloseGrid()


        '   gridview
        grdCloseCheck.Location = New Point(0, Me.pnlDirection.Height)
        grdCloseCheck.Size = New Size(Me.Width + 1, 496) '(Me.Height * 0.63))
        grdCloseCheck.BackgroundColor = c2
        grdCloseCheck.ForeColor = c14
        grdCloseCheck.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        grdCloseCheck.ReadOnly = True
        grdCloseCheck.Controls(0).Height = 0
        grdCloseCheck.Controls(1).Width = 0

        grdCloseCheck.ColumnHeadersVisible = False
        grdCloseCheck.RowHeadersVisible = False
        grdCloseCheck.CaptionVisible = False
        '     grdCloseCheck.DataSource = dvClosingCheck
        grdCloseCheck.SelectionBackColor = Color.Blue
        grdCloseCheck.AllowSorting = False


        Me.Controls.Add(grdCloseCheck)


    End Sub

    Friend Sub PopulateCloseGrid(ByRef dvDisplay As DataView)

        Me.grdCloseCheck.DataSource = dvDisplay

    End Sub



    Private Sub AddGridCloseCheckColumns()

        Dim oRow As DataRow

        '       For Each oRow In dsOrder.Tables("OpenOrders").Rows
        '      MsgBox(oRow("ItemStatus"))
        '     MsgBox(oRow("sin"))
        '    MsgBox(oRow("ItemName"))
        '   Next

        Dim tsOrder As New DataGridTableStyle
        tsOrder.MappingName = "OpenOrders"
        tsOrder.RowHeadersVisible = False
        tsOrder.GridLineStyle = DataGridLineStyle.None

        Dim csStatus As New DataGridTextBoxColumn
        csStatus.MappingName = "ItemStatus"
        csStatus.Width = 0  '0.08 * viewOrderWidth

        Dim csSIN As New DataGridTextBoxColumn
        csSIN.MappingName = "sin"
        csSIN.Width = 0

        Dim csSII As New DataGridTextBoxColumn
        csSII.MappingName = "sii"
        csSII.Width = 0

        Dim csItemName As New OrderCloseGridColumn  'DataGridTextBoxColumn 'OrderGridColumn(False, False) '
        csItemName.MappingName = "TerminalName" '"ItemName"
        csItemName.Width = 0.8 * Me.Width  'viewOrderWidth

        Dim csItemCost As New OrderCloseGridColumn  'DataGridTextBoxColumn 'OrderGridColumn(True, False)  '
        csItemCost.MappingName = "Price"
        csItemCost.Width = 0.17 * Me.Width ' viewOrderWidth
        csItemCost.Alignment = HorizontalAlignment.Right

        tsOrder.GridColumnStyles.Add(csStatus)
        tsOrder.GridColumnStyles.Add(csSIN)
        tsOrder.GridColumnStyles.Add(csSII)
        tsOrder.GridColumnStyles.Add(csItemName)
        tsOrder.GridColumnStyles.Add(csItemCost)
        grdCloseCheck.TableStyles.Add(tsOrder)

    End Sub

    Friend Sub CalculatePriceAndTax(ByVal cn As Integer)
        Dim oRow As DataRow
        Dim vRow As DataRow 'View
        Dim index As Integer
        ReDim taxIDInteger(ds.Tables("Tax").Rows.Count)
        ReDim taxName(ds.Tables("Tax").Rows.Count)
        ReDim taxTotal(ds.Tables("Tax").Rows.Count)
        _totalCheckBalance = 0
        chkSubTotal = 0
        checkTax = 0
        checkSinTax = 0
        chkFood = 0
        chkDrinks = 0

        taxName(0) = "Tax"
        index = 1

        For Each oRow In ds.Tables("Tax").Rows
            taxIDInteger(index) = oRow("TaxID")
            taxName(index) = oRow("TaxName")
            '         taxTotal(index) = Format(0, "###0.00")
            For Each vRow In dsOrder.Tables("OpenOrders").Rows 'dvOrder 'dvClosingCheck
                If Not vRow.RowState = DataRowState.Deleted And Not vRow.RowState = DataRowState.Detached Then
                    If index = 0 Then
                        'we loop through this for every tax category
                        'so we only add this 1 time
                        _totalCheckBalance += (vRow("Price") + vRow("TaxPrice") + vRow("SinTax"))
                    End If
                End If
                If Not vRow.RowState = DataRowState.Deleted And Not vRow.RowState = DataRowState.Detached Then
                    If vRow("CheckNumber") = cn Then 'currentTable.CheckNumber Then
                        If vRow("TaxID") = taxIDInteger(index) Then
                            Me.chkSubTotal = chkSubTotal + vRow("Price")
                            taxTotal(0) = taxTotal(0) + vRow("TaxPrice") 'taxTotal(index) + (vRow("Price") * oRow("TaxPercent"))
                            taxTotal(index) = taxTotal(index) + vRow("SinTax") 'taxTotal(index) + (vRow("Price") * oRow("TaxPercent"))

                            If vRow("FunctionFlag") = "F" Or vRow("FunctionFlag") = "O" Or vRow("FunctionFlag") = "M" Then
                                chkFood += vRow("Price")
                            ElseIf vRow("FunctionFlag") = "D" Then
                                chkDrinks += vRow("Price")
                            End If
                        End If
                    End If
                End If

            Next
            taxTotal(index) = Format(taxTotal(index), "###0.00")
            checkSinTax += taxTotal(index)
            index += 1
        Next

        taxTotal(0) = Format(taxTotal(0), "###0.00")
        checkTax += taxTotal(0)

        currentTable.SubTotal = chkSubTotal

    End Sub

    Friend Function AttachTotalsToTotalView(ByVal cn As Integer)
        lstCloseCheckTotals.Items.Clear()

        Dim oRow As DataRow
        Dim index As Integer
        Dim isTaxExempt As Boolean = False
        Dim taxExemptAmount As Decimal

        Dim chkTotalAmount As Decimal = chkSubTotal

        Dim itemSubTotal As New ListViewItem
        itemSubTotal.Text = "SubTotal"
        itemSubTotal.SubItems.Add(chkSubTotal)
        lstCloseCheckTotals.Items.Add(itemSubTotal)

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("CheckNumber") = cn Then ' currentTable.CheckNumber Then
                    If oRow("TaxID") = -1 Then  'taxIDInteger(index) = -1 Then
                        isTaxExempt = True
                        taxExemptAmount += oRow("Price")
                    End If
                    '  index += 1
                End If
            End If
        Next

        If isTaxExempt = True And taxTotal(0) = 0 Then
            'this is the whole check is exempt
            taxName(0) = "Tax Exempt: $" & taxExemptAmount
        End If

        '      If taxTotal(0) > 0 Then
        chkTotalAmount = chkTotalAmount + taxTotal(0)
        Dim itemTax0 As New ListViewItem
        itemTax0.Text = taxName(0)
        itemTax0.SubItems.Add(taxTotal(0))
        '     itemTax0.SubItems.Add(taxIDInteger(0))

        lstCloseCheckTotals.Items.Add(itemTax0)
        '     End If

        index = 1
        For Each oRow In ds.Tables("Tax").Rows
            If oRow("TaxID") = -1 Then
                If (isTaxExempt = True And taxTotal(0) > 0) Then
                    ' his means only some items are tax exempt
                    Dim itemTax As New ListViewItem
                    itemTax.Text = "Tax Exempt: $" & taxExemptAmount
                    itemTax.SubItems.Add(0) 'taxTotal(index))
                    lstCloseCheckTotals.Items.Add(itemTax)
                End If
            ElseIf taxTotal(index) > 0 Then
                chkTotalAmount = chkTotalAmount + taxTotal(index)
                Dim itemTax As New ListViewItem
                itemTax.Text = taxName(index) & " Tax"
                itemTax.SubItems.Add(taxTotal(index))
                lstCloseCheckTotals.Items.Add(itemTax)
            End If
            index += 1
        Next

        '   *** need to do in the database so it saves the auto gratuity
        If AutoGratuity = True Then
            Dim itemGratuity As New ListViewItem
            itemGratuity.Text = "Gratuity " & Format((companyInfo.autoGratuityPercent * 100), "##0").ToString & "%"
            'this should be calculated by NON-Tax amount
            'but when we swipe credit card it does not know ow much tax going to each card, 
            'who would know what each person is buying unless spilt checks
            '      autoGratuityAmount = Format((chkSubTotal * companyInfo.autoGratuityPercent), "####.00")
            autoGratuityAmount = Format((chkTotalAmount * companyInfo.autoGratuityPercent), "####.00")
            itemGratuity.SubItems.Add(autoGratuityAmount)
            lstCloseCheckTotals.Items.Add(itemGratuity)
            '444        chkTotalAmount = chkSubTotal + autoGratuityAmount
        End If


        Dim itemTotal As New ListViewItem
        itemTotal.Text = "Total"
        itemTotal.SubItems.Add(chkTotalAmount)
        lstCloseCheckTotals.Items.Add(itemTotal)

        remainingBalance = chkTotalAmount

        Dim cashAmount As Decimal

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("Applied") = True And oRow("CheckNumber") = cn Then 'currentTable.CheckNumber Then
                    If oRow("PaymentTypeID") = 1 Then   '444 Flag") = "Cash" Then
                        cashAmount = cashAmount + (oRow("PaymentAmount") * -1)
                    Else
                        Dim itemPayment As New ListViewItem
                        itemPayment.Text = DisplayPaymentType(oRow("PaymentTypeID"))
                        itemPayment.SubItems.Add((oRow("PaymentAmount") * -1))
                        lstCloseCheckTotals.Items.Add(itemPayment)
                    End If
                    remainingBalance -= (oRow("PaymentAmount"))
                End If
            End If
        Next

        If cashAmount < 0 Then
            Dim itemPayment As New ListViewItem
            itemPayment.Text = "Cash Tender"
            itemPayment.SubItems.Add(cashAmount)
            lstCloseCheckTotals.Items.Add(itemPayment)
            CashPaymentTendered = True
        End If

        Return remainingBalance

    End Function

    Private Function DisplayPaymentType(ByVal payID As Integer)
        Dim payName As String
        Dim oRow As DataRow

        For Each oRow In ds.Tables("CreditCardDetail").Rows
            If oRow("PaymentTypeID") = payID Then
                payName = oRow("PaymentTypeName")
                Exit For
            End If
        Next

        Return payName

    End Function

    Private Sub btnCloseViewDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click

        If Me.ClosingCheckCurrencyMan.Position < Me.grdCloseCheck.DataSource.count Then
            '          If Me.gridViewOrder.DataSource.count - Me.OpenOrdersCurrencyMan.Position > 15 Then
            '          Me.OpenOrdersCurrencyMan.Position += 10
            '      ElseIf Me.gridViewOrder.DataSource.count - Me.OpenOrdersCurrencyMan.Position > 10 Then
            '          Me.OpenOrdersCurrencyMan.Position += 5
            '     ElseIf Me.gridViewOrder.DataSource.count - Me.OpenOrdersCurrencyMan.Position > 5 Then
            '         Me.OpenOrdersCurrencyMan.Position += 2
            ''     Else
            Me.ClosingCheckCurrencyMan.Position += 1
            '    End If

            Me.grdCloseCheck.ScrollToRow(ClosingCheckCurrencyMan.Position)
        End If

    End Sub

    Private Sub btnCloseViewUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click

        If Me.ClosingCheckCurrencyMan.Position > 0 Then
            If Me.ClosingCheckCurrencyMan.Position - 15 > 0 Then
                Me.ClosingCheckCurrencyMan.Position -= 15
            ElseIf Me.ClosingCheckCurrencyMan.Position - 10 > 0 Then
                Me.ClosingCheckCurrencyMan.Position -= 10
            ElseIf Me.ClosingCheckCurrencyMan.Position - 5 > 0 Then
                Me.ClosingCheckCurrencyMan.Position -= 5
            Else
                Me.ClosingCheckCurrencyMan.Position -= 1
            End If

            Me.grdCloseCheck.ScrollToRow(ClosingCheckCurrencyMan.Position)
        End If
    End Sub


End Class
