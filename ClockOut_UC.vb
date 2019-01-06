Imports DataSet_Builder


Public Class ClockOut_UC
    Inherits System.Windows.Forms.UserControl

    '   Dim sql As New DataSet_Builder.SQLHelper(connectserver)
    '    Dim prt As New PrintHelper

    '   Private _empClockOut As Employee
    Dim _hasOpenTables As Boolean

    Dim totalSales As Decimal
    Dim totalTaxes As Decimal
    Dim grossSales As Decimal
    Dim totalPayments As Decimal
    Dim cashPayments As Decimal
    Dim chargePayments As Decimal
    Dim chargeTips As Decimal
    Dim declaredTips As Decimal
    Dim lessChargeTips As Decimal
    Dim adjustingClaimTips As Boolean

    Dim clockOutJunk As ClockOutInfo
    Dim paymentPanel(50) As DataSet_Builder.Payment_UC
    Dim paymentRowIndex As Integer
    Dim startPaymentIndex As Integer = 1

    Event ClockOutComplete() '(ByVal sender As Object, ByVal e As System.EventArgs)
    Event ClockOutCancel()

    '    Friend Property EmpClockOut() As Employee
    '       Get
    '          Return _empClockOut
    '     End Get
    '    Set(ByVal Value As Employee)
    '       _empClockOut = Value
    '      End Set
    ' End Property

#Region " Windows Form Designer generated code "

    Public Sub New(ByRef emp As Employee, ByVal hasOpenTables As Boolean) ', ByVal tSales As Decimal, ByVal cSales As Decimal, ByVal cTips As Decimal)
        MyBase.New()

        '     _tipableSales = tSales
        '    _chargedSales = cSales
        '   _chargedTips = cTips
        '    _empClockOut = emp
        _hasOpenTables = hasOpenTables

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
    Friend WithEvents pnlClockOut As System.Windows.Forms.Panel
    Friend WithEvents NumberPadLarge1 As DataSet_Builder.NumberPadLarge
    Friend WithEvents pnlClockOutInfo As System.Windows.Forms.Panel
    Friend WithEvents lblClockOut As System.Windows.Forms.Label
    Friend WithEvents btnClockOutCancel As System.Windows.Forms.Button
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents lblClockChargedTips As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlClosePayments As System.Windows.Forms.Panel
    Friend WithEvents btnClockOut As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblbAddTips As System.Windows.Forms.Label
    Friend WithEvents btnPrintOut As System.Windows.Forms.Button
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents btnDailyNextPage As System.Windows.Forms.Button
    Friend WithEvents btnDailyPreviousPage As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlClockOut = New System.Windows.Forms.Panel
        Me.btnPrintOut = New System.Windows.Forms.Button
        Me.lblbAddTips = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblClockOut = New System.Windows.Forms.Label
        Me.btnClockOutCancel = New System.Windows.Forms.Button
        Me.pnlClosePayments = New System.Windows.Forms.Panel
        Me.NumberPadLarge1 = New DataSet_Builder.NumberPadLarge
        Me.pnlClockOutInfo = New System.Windows.Forms.Panel
        Me.label4 = New System.Windows.Forms.Label
        Me.lblClockChargedTips = New System.Windows.Forms.Label
        Me.btnClockOut = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.btnDailyNextPage = New System.Windows.Forms.Button
        Me.btnDailyPreviousPage = New System.Windows.Forms.Button
        Me.pnlClockOut.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlClockOut
        '
        Me.pnlClockOut.BackColor = System.Drawing.Color.Black
        Me.pnlClockOut.Controls.Add(Me.Panel12)
        Me.pnlClockOut.Controls.Add(Me.btnPrintOut)
        Me.pnlClockOut.Controls.Add(Me.lblbAddTips)
        Me.pnlClockOut.Controls.Add(Me.Panel2)
        Me.pnlClockOut.Controls.Add(Me.pnlClosePayments)
        Me.pnlClockOut.Controls.Add(Me.NumberPadLarge1)
        Me.pnlClockOut.Controls.Add(Me.pnlClockOutInfo)
        Me.pnlClockOut.Controls.Add(Me.label4)
        Me.pnlClockOut.Controls.Add(Me.lblClockChargedTips)
        Me.pnlClockOut.Controls.Add(Me.btnClockOut)
        Me.pnlClockOut.Location = New System.Drawing.Point(8, 8)
        Me.pnlClockOut.Name = "pnlClockOut"
        Me.pnlClockOut.Size = New System.Drawing.Size(1008, 750)
        Me.pnlClockOut.TabIndex = 0
        '
        'btnPrintOut
        '
        Me.btnPrintOut.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnPrintOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintOut.Location = New System.Drawing.Point(760, 664)
        Me.btnPrintOut.Name = "btnPrintOut"
        Me.btnPrintOut.Size = New System.Drawing.Size(104, 64)
        Me.btnPrintOut.TabIndex = 19
        Me.btnPrintOut.Text = "Print"
        '
        'lblbAddTips
        '
        Me.lblbAddTips.BackColor = System.Drawing.Color.White
        Me.lblbAddTips.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblbAddTips.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbAddTips.Location = New System.Drawing.Point(752, 96)
        Me.lblbAddTips.Name = "lblbAddTips"
        Me.lblbAddTips.Size = New System.Drawing.Size(112, 32)
        Me.lblbAddTips.TabIndex = 18
        Me.lblbAddTips.Text = "Additional Tips"
        Me.lblbAddTips.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblbAddTips.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.DodgerBlue
        Me.Panel2.Controls.Add(Me.lblClockOut)
        Me.Panel2.Controls.Add(Me.btnClockOutCancel)
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1000, 32)
        Me.Panel2.TabIndex = 17
        '
        'lblClockOut
        '
        Me.lblClockOut.BackColor = System.Drawing.Color.Transparent
        Me.lblClockOut.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClockOut.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblClockOut.Location = New System.Drawing.Point(288, 0)
        Me.lblClockOut.Name = "lblClockOut"
        Me.lblClockOut.Size = New System.Drawing.Size(448, 32)
        Me.lblClockOut.TabIndex = 0
        Me.lblClockOut.Text = "Clock Out: "
        Me.lblClockOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClockOutCancel
        '
        Me.btnClockOutCancel.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnClockOutCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClockOutCancel.Location = New System.Drawing.Point(88, 0)
        Me.btnClockOutCancel.Name = "btnClockOutCancel"
        Me.btnClockOutCancel.Size = New System.Drawing.Size(104, 32)
        Me.btnClockOutCancel.TabIndex = 1
        Me.btnClockOutCancel.Text = "Close"
        '
        'pnlClosePayments
        '
        Me.pnlClosePayments.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.pnlClosePayments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlClosePayments.Location = New System.Drawing.Point(288, 48)
        Me.pnlClosePayments.Name = "pnlClosePayments"
        Me.pnlClosePayments.Size = New System.Drawing.Size(456, 672)
        Me.pnlClosePayments.TabIndex = 15
        '
        'NumberPadLarge1
        '
        Me.NumberPadLarge1.BackColor = System.Drawing.Color.SlateGray
        Me.NumberPadLarge1.DecimalUsed = True
        Me.NumberPadLarge1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.NumberPadLarge1.IntegerNumber = 0
        Me.NumberPadLarge1.Location = New System.Drawing.Point(752, 280)
        Me.NumberPadLarge1.Name = "NumberPadLarge1"
        Me.NumberPadLarge1.NumberString = Nothing
        Me.NumberPadLarge1.NumberTotal = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumberPadLarge1.Size = New System.Drawing.Size(244, 370)
        Me.NumberPadLarge1.TabIndex = 10
        '
        'pnlClockOutInfo
        '
        Me.pnlClockOutInfo.BackColor = System.Drawing.Color.CornflowerBlue
        Me.pnlClockOutInfo.Location = New System.Drawing.Point(16, 48)
        Me.pnlClockOutInfo.Name = "pnlClockOutInfo"
        Me.pnlClockOutInfo.Size = New System.Drawing.Size(264, 672)
        Me.pnlClockOutInfo.TabIndex = 9
        '
        'label4
        '
        Me.label4.BackColor = System.Drawing.Color.White
        Me.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.Location = New System.Drawing.Point(752, 48)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(112, 32)
        Me.label4.TabIndex = 5
        Me.label4.Text = "Declaring Tips:  $"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblClockChargedTips
        '
        Me.lblClockChargedTips.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClockChargedTips.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClockChargedTips.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblClockChargedTips.Location = New System.Drawing.Point(872, 40)
        Me.lblClockChargedTips.Name = "lblClockChargedTips"
        Me.lblClockChargedTips.Size = New System.Drawing.Size(112, 40)
        Me.lblClockChargedTips.TabIndex = 8
        Me.lblClockChargedTips.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClockOut
        '
        Me.btnClockOut.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnClockOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClockOut.Location = New System.Drawing.Point(888, 664)
        Me.btnClockOut.Name = "btnClockOut"
        Me.btnClockOut.Size = New System.Drawing.Size(104, 64)
        Me.btnClockOut.TabIndex = 16
        Me.btnClockOut.Text = "Clock Out"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Panel1.Controls.Add(Me.pnlClockOut)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1024, 768)
        Me.Panel1.TabIndex = 1
        '
        'Panel12
        '
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel12.Controls.Add(Me.btnDailyNextPage)
        Me.Panel12.Controls.Add(Me.btnDailyPreviousPage)
        Me.Panel12.Location = New System.Drawing.Point(760, 144)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(104, 120)
        Me.Panel12.TabIndex = 20
        '
        'btnDailyNextPage
        '
        Me.btnDailyNextPage.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.btnDailyNextPage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDailyNextPage.Location = New System.Drawing.Point(8, 64)
        Me.btnDailyNextPage.Name = "btnDailyNextPage"
        Me.btnDailyNextPage.Size = New System.Drawing.Size(88, 48)
        Me.btnDailyNextPage.TabIndex = 1
        Me.btnDailyNextPage.Text = "Next Page"
        '
        'btnDailyPreviousPage
        '
        Me.btnDailyPreviousPage.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.btnDailyPreviousPage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDailyPreviousPage.Location = New System.Drawing.Point(8, 8)
        Me.btnDailyPreviousPage.Name = "btnDailyPreviousPage"
        Me.btnDailyPreviousPage.Size = New System.Drawing.Size(88, 48)
        Me.btnDailyPreviousPage.TabIndex = 0
        Me.btnDailyPreviousPage.Text = "Previous Page"
        '
        'ClockOut_UC
        '
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ClockOut_UC"
        Me.Size = New System.Drawing.Size(1024, 768)
        Me.pnlClockOut.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther()


        '     Me.lblClockTipableSales.Text = "$  " & Me.TipableSales
        '    Me.lblClockChargeSales.Text = "$  " & Me.ChargedSales
        '   Me.lblClockChargedTips.Text = "$  " & Me.ChargedTips

        If _hasOpenTables = True Then
            btnClockOut.Text = "You still have open Tickets"
            '444        btnClockOut.Enabled = False
        End If

        Me.lblClockOut.Text = "Clock Out:  " & currentClockEmp.FullName


        If currentServer.LoginTrackingID = 0 Then
            ' this is when bartenders are clocking out on someone else's login
            Dim isCLockedin As Integer
            Try
                isCLockedin = ActuallyLogIn(currentClockEmp)
            Catch ex As Exception
                CloseConnection()
            End Try

        End If


        PopulatePaymentsAndCreditsWhenClosing()
        DetermineSalesAndPayments()
        DisplaySalesAndPayments()
        FilterPreAuthDataview()
        DisplayPreAuthPaymentPanels()


    End Sub

    Private Sub DetermineSalesAndPayments()
        '   determine all experiences for this employee on this day (or for current batch)

        dsOrder.Tables("OpenOrders").Clear()

        '   not sure if we need the following 3
        '  ClearClosedTabsAndTables()
        dsEmployee.Tables("ClockOutSales").Clear()
        dsEmployee.Tables("ClockOutTaxes").Clear()
        dsEmployee.Tables("ClockOutPayments").Clear()

        If typeProgram = "Online_Demo" Then
            MsgBox("Demo will not display Zero's for Sales and Payment data.")
            Exit Sub
        End If

        Dim strClockOutSales As String
        Dim strClockOutTaxes As String
        Dim strClockOutPayments As String
        Dim firstTime As Boolean = True
        Dim tableCreating As String
        Dim vRow As DataRowView

        Try

            sql.SqlSelectCommandEmpClockOutSales.Parameters("@LoginID").Value = currentClockEmp.LoginTrackingID
            sql.SqlSelectCommandEmpClockOutSales.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
            sql.SqlSelectCommandEmpClockOutTaxes.Parameters("@LoginID").Value = currentClockEmp.LoginTrackingID
            sql.SqlSelectCommandEmpClockOutTaxes.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
            sql.SqlSelectCommandEmpClockOutPayments.Parameters("@LoginID").Value = currentClockEmp.LoginTrackingID
            sql.SqlSelectCommandEmpClockOutPayments.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode

            '**********
            ' can we combine all these together
            ' this might save time???
            'also FFID < 1 not 0
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()

            sql.SqlEmpClockOutSales.Fill(dsEmployee.Tables("ClockOutSales"))
            sql.SqlEmpClockOutTaxes.Fill(dsEmployee.Tables("ClockOutTaxes"))
            sql.SqlEmpClockOutPayments.Fill(dsEmployee.Tables("ClockOutPayments"))
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DisplaySalesAndPayments()
        Dim oRow As DataRow
        Dim x As Single = buttonSpace
        Dim y As Single
        Dim aTaxName As String

        Me.pnlClockOutInfo.Controls.Clear()
        totalSales = 0
        totalTaxes = 0
        grossSales = 0
        totalPayments = 0
        cashPayments = 0
        chargePayments = 0
        chargeTips = 0
        lessChargeTips = 0

        DisplaySalesAndPaymentTitle("*** SALES ***", y)

        y += 20

        For Each oRow In dsEmployee.Tables("ClockOutSales").Rows
            If oRow("FunctionGroupSales") <> 0 Then
                DisplayNewSalesAndPaymentLabel(oRow("FunctionName"), oRow("FunctionGroupSales"), y)
                totalSales += oRow("FunctionGroupSales")
                y += 20
            End If
        Next

        DisplayNewSalesAndPaymentLabel("Total:", totalSales, y)
        y += 30

        DisplaySalesAndPaymentTitle("*** TAXES ***", y)
        y += 20

        For Each oRow In dsEmployee.Tables("ClockOutTaxes").Rows
            If oRow("GroupTaxes") <> 0 Then
                '         MsgBox(oRow("TaxID"))
                aTaxName = DetermineTaxName(oRow("TaxID"))
                DisplayNewSalesAndPaymentLabel(aTaxName, oRow("GroupTaxes"), y)
                totalTaxes += oRow("GroupTaxes")
                y += 20
            End If
        Next
        grossSales = totalSales + totalTaxes

        DisplayNewSalesAndPaymentLabel("Total:", totalTaxes, y)
        y += 30
        DisplayNewSalesAndPaymentLabel("Gross Sales", grossSales, y)
        y += 30


        DisplaySalesAndPaymentTitle("*** PAYMENTS ***", y)
        y += 20

        For Each oRow In dsEmployee.Tables("ClockOutPayments").Rows
            If oRow("GroupPayments") <> 0 Then
                DisplayNewSalesAndPaymentLabel(oRow("PaymentTypeName"), oRow("GroupPayments"), y)
                totalPayments += oRow("GroupPayments")
                If oRow("PaymentFlag") = "Cash" Then
                    cashPayments += oRow("GroupPayments")
                ElseIf oRow("PaymentFlag") = "cc" Then
                    chargePayments += oRow("GroupPayments")
                    chargeTips += oRow("GroupTips")
                End If
                y += 20
            End If
        Next

        DisplayNewSalesAndPaymentLabel("Total Payments", totalPayments, y)

        If chargeTips > declaredTips Then
            declaredTips = chargeTips
        End If
        Me.lblClockChargedTips.Text = Format(declaredTips, "###,##0.00")
        lessChargeTips = -1 * chargeTips

        y += 30

        DisplaySalesAndPaymentTitle("*** CASH OWED ***", y)
        y += 20
        DisplayNewSalesAndPaymentLabel("Cash Payments", cashPayments, y)
        y += 20
        DisplayNewSalesAndPaymentLabel("CC Tips", lessChargeTips, y)
        y += 20
        DisplayNewSalesAndPaymentLabel("TOTAL CASH OWED", (cashPayments + lessChargeTips), y)
        y += 20

        '     PrepareClockOutPrint()

    End Sub

    Private Sub FilterPreAuthDataview()
        dvClosedPreAuth = New DataView

        With dvClosedPreAuth
            .Table = dsOrder.Tables("PaymentsAndCredits")
            '444  .RowFilter = "PaymentFlag = 'cc' AND BatchCleared = 'False'"     ' "TransactionCode = 'PreAuth' AND PaymentTypeID > 1"
            '  .RowFilter = "PaymentFlag = 'cc' AND BatchCleared = 'False' AND TransactionCode = 'PreAuth' AND PaymentTypeID > 1"
            .RowFilter = "BatchCleared = 'False' AND PaymentTypeID > 1"
            'currently will not pull GIFT
            '     .Sort = "TransactionCode, AuthCode ASC"
            '   sort will put all the PreAuthCatures on the bottom
        End With

        If dvClosedPreAuth.Count > 50 Then
            ReDim paymentPanel(dvClosedPreAuth.Count)
        End If

    End Sub

    Private Sub btnDailyPreviousPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDailyPreviousPage.Click
        If startPaymentIndex <= 1 Then Exit Sub
        startPaymentIndex -= 9
        DisplayPreAuthPaymentPanels()

    End Sub

    Private Sub btnDailyNextPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDailyNextPage.Click
        If startPaymentIndex >= dvClosedPreAuth.Count Then Exit Sub
        startPaymentIndex += 9
        DisplayPreAuthPaymentPanels()

    End Sub
    Private Sub DisplayPreAuthPaymentPanels()
        Dim vRow As DataRowView
        Dim count As Integer
        Dim position As Integer

        Me.pnlClosePayments.Controls.Clear()


        For Each vRow In dvClosedPreAuth    'dsOrder.Tables("PaymentsAndCredits").Rows
            '       If oRow("TransactionCode") = "PreAuth" Then
            count += 1
            If count >= startPaymentIndex Then  ' (***removed temp*****)And count <= (startPaymentIndex + 9) Then
                CreateNewPaymentPanel(vRow, count, position)
                position += 1
            End If

            '      End If
        Next

        If dvClosedPreAuth.Count > 0 Then
            paymentRowIndex = 1
            ActiveThisPanel(paymentRowIndex)
        End If

        For Each vRow In dvClosedPreAuth

        Next

    End Sub

    Private Sub CreateNewPaymentPanel(ByRef vRow As DataRowView, ByVal PnlNo As Integer, ByVal position As Integer)

        Dim truncAcctNum As String

        If Not vRow("AccountNumber") Is DBNull.Value Then
            If Not vRow("AuthCode") Is DBNull.Value Then
                'already authorized
                If Not vRow("AccountNumber").Substring(0, 4) = "xxxx" And Not vRow("AccountNumber") = "Manual" Then
                    truncAcctNum = TruncateAccountNumber(vRow("AccountNumber"))
                Else
                    truncAcctNum = (vRow("AccountNumber"))
                End If
            Else
                truncAcctNum = (vRow("AccountNumber"))
            End If
        End If

        paymentPanel(PnlNo) = New DataSet_Builder.Payment_UC("ClockOut", vRow, Nothing, PnlNo, Nothing, truncAcctNum, 0)

        With paymentPanel(PnlNo)

            .Location = New Point(0, .Height * (position))
            .BackColor = Color.DarkGray
            .ReverseAlignment()
            AddHandler paymentPanel(PnlNo).ActivePanel, AddressOf PaymentUserControl_Click
            Me.pnlClosePayments.Controls.Add(paymentPanel(PnlNo))
        End With

    End Sub

    Private Sub PaymentUserControl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlClosePayments.Click

        Dim objButton As DataSet_Builder.Payment_UC

        Try
            objButton = CType(sender, DataSet_Builder.Payment_UC)
        Catch ex As Exception
            Exit Sub
        End Try

        '     If objButton.CurrentState = DataSet_Builder.Payment_UC.PanelHit.TipPanel Then
        '       Me.NumberPadLarge1.DecimalUsed = True
        '      NumberPadLarge1.NumberString = objButton.TipAmount
        '        NumberPadLarge1.ShowNumberString()
        '       NumberPadLarge1.NumberString = ""
        '    End If

        If Not paymentRowIndex = objButton.ActiveIndex Then
            paymentRowIndex = objButton.ActiveIndex
            ActiveThisPanel(objButton.ActiveIndex)
        End If
    End Sub

    Private Sub ActiveThisPanel(ByVal ai As Integer)
        Dim oRow As DataRowView
        Dim index As Integer = 1

        '    MsgBox(dvClosedPreAuth.Count)
        '   If dvClosedPreAuth.Count > 20 Then ReDim paymentPanel(dvClosedPreAuth.Count)

        For Each oRow In dvClosedPreAuth   'dsOrder.Tables("PaymentsAndCredits").Rows
            '      If Not oRow.RowState = DataRowState.Deleted Then
            If index >= startPaymentIndex Then
                If Not index = ai Then
                    paymentPanel(index).BackColor = Color.DarkGray
                    paymentPanel(index).IsActive = False
                Else
                    paymentPanel(index).BackColor = Color.WhiteSmoke
                    paymentPanel(paymentRowIndex).CurrentState = DataSet_Builder.Payment_UC.PanelHit.TipPanel
                    PaymentPanelActivated()
                End If
            End If
            index += 1
            '       End If
        Next

    End Sub

    Private Sub PaymentPanelActivated()

        '    If paymentPanel(paymentRowIndex).CurrentState = DataSet_Builder.Payment_UC.PanelHit.TipPanel Then
        Me.NumberPadLarge1.DecimalUsed = True
        Me.NumberPadLarge1.NumberTotal = paymentPanel(paymentRowIndex).TipAmount
        Me.NumberPadLarge1.ShowNumberString()
        Me.NumberPadLarge1.Focus()
        Me.NumberPadLarge1.IntegerNumber = 0
        Me.NumberPadLarge1.NumberString = Nothing
        '   End If

    End Sub







    Private Sub DisplayNewSalesAndPaymentLabel(ByVal catName As String, ByVal catAmount As Decimal, ByVal y As Single)
        Dim isPositive As String
        Dim spWidth As Single
        Dim spHeight As Single = 50
        Dim x As Single = buttonSpace

        If catAmount < 0 Then
            isPositive = "(-)"
        Else
            isPositive = "(+)"
        End If
        spWidth = Me.pnlClockOutInfo.Width - 100

        Dim lblTitle As Label
        Dim lblAmount As Label

        lblTitle = New Label
        With lblTitle
            .Text = catName
            .TextAlign = ContentAlignment.MiddleLeft
            .Location = New Point(x, y)
            .Size = New Size(spWidth, 30)

        End With
        x = x + spWidth
        spWidth = 80

        lblAmount = New Label
        With lblAmount
            .Text = Format(catAmount, "###,##0.00") & isPositive
            .TextAlign = ContentAlignment.MiddleRight
            .Location = New Point(x, y)
            .Size = New Size(spWidth, 30)

        End With

        Me.pnlClockOutInfo.Controls.Add(lblTitle)
        Me.pnlClockOutInfo.Controls.Add(lblAmount)

    End Sub

    Private Sub DisplaySalesAndPaymentTitle(ByVal catTitle As String, ByVal y As Single)
        Dim lblTitle As Label
        Dim x As Single = buttonSpace

        lblTitle = New Label
        With lblTitle
            .Text = catTitle
            .TextAlign = ContentAlignment.MiddleCenter
            .Location = New Point(x, y)
            .Size = New Size(Me.pnlClockOutInfo.Width, 30)

        End With
        Me.pnlClockOutInfo.Controls.Add(lblTitle)

    End Sub

    Private Sub TipEnterHit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumberPadLarge1.NumberEntered

        If adjustingClaimTips = True Then
            If chargeTips > declaredTips Then
                If MsgBox("Declared Tips should be more than Charged Tips. Please Verify", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                    Exit Sub
                End If
            End If
            adjustingClaimTips = False
            declaredTips = Me.NumberPadLarge1.NumberTotal
            Me.lblClockChargedTips.Text = Format(declaredTips, "###,##0.00")
            Exit Sub
        End If

        If Me.NumberPadLarge1.NumberTotal > dvClosedPreAuth(paymentRowIndex - 1)("PaymentAmount") Then
            If MsgBox("Gratuity Amount is greater than Purchase. Please Verify", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                Exit Sub
            End If
        End If

        chargeTips -= paymentPanel(paymentRowIndex).TipAmount
        chargeTips += Me.NumberPadLarge1.NumberTotal
        If chargeTips > declaredTips Then
            declaredTips = chargeTips
        End If

        dvClosedPreAuth(paymentRowIndex - 1)("Tip") = Me.NumberPadLarge1.NumberTotal
        dvClosedPreAuth(paymentRowIndex - 1)("TransactionCode") = "PreAuthCapture"
        paymentPanel(paymentRowIndex).UpdateTip(Me.NumberPadLarge1.NumberTotal)

        Me.lblClockChargedTips.Text = Format(declaredTips, "###,##0.00")

        If paymentRowIndex < dvClosedPreAuth.Count Then
            paymentRowIndex += 1
            ActiveThisPanel(paymentRowIndex)
        End If

    End Sub

    Private Sub btnClockOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClockOut.Click

        EitherPrintOrClockOut(True)

        '   If NumberPadLarge1.NumberTotal = Nothing Then
        '   declaredTips = Format(0.0, "#0.00")
        '   Else
        '       declaredTips = Format(NumberPadLarge1.NumberTotal, "####0.00")
        ''   End If
        ' declaredTips.Round(NumberPadLarge1.NumberTotal, 2)

        '        For Each oRow In dsEmployee.Tables("LoggedInEmployees").Rows
        '        If oRow("EmployeeID") = currentServer.EmployeeID Then
        '        clockOutJunk.TimeIn = oRow("LogInTime")
        '       oRow("LogOutTime") = clockOutJunk.TimeOut
        '       oRow("TipableSales") = totalSales
        '        oRow("DeclaredTips") = chargeTips + declaredTips
        '       oRow("ChargedSales") = chargePayments
        '      oRow("ChargedTips") = chargeTips
        '     Exit For
        '    End If
        '   Next

    End Sub

    Private Sub btnPrintOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintOut.Click
        EitherPrintOrClockOut(False)

    End Sub

    Friend Sub EitherPrintOrClockOut(ByVal isFullClockout As Boolean)

        Dim oRow As DataRow
        Dim shiftMins As Integer
        'need to run auth and save tips input
        Dim amountZeroTips As Integer
        amountZeroTips = CountZeroTips()

        If typeProgram = "Online_Demo" Then
            For Each oRow In dsEmployee.Tables("LoggedInEmployees").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If currentClockEmp.EmployeeID = oRow("EmployeeID") Then
                        oRow.Delete()
                        RaiseEvent ClockOutComplete()
                        Me.Dispose()
                        Exit Sub
                    End If
                End If
            Next
            RaiseEvent ClockOutComplete()
            Me.Dispose()
            Exit Sub
        End If

        If amountZeroTips = -99 Then
            'this means we have non-auth credit cards
            Exit Sub
        End If

        If isFullClockout = True Then
            If amountZeroTips > 0 Then
                If amountZeroTips = 1 Then
                    If MsgBox("There is " & amountZeroTips & " Gratuity of $0.00 which was not physically entered. Do you wish to continue?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                        Exit Sub
                    End If
                Else
                    If MsgBox("There are " & amountZeroTips & " Gratuities of $0.00 which were not physically entered. Do you wish to continue?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                        Exit Sub
                    End If
                End If
                '   not sure if we want this here or a second catch 
                '   we we close daily
                '       Dim vRow As DataRowView
                '       For Each vRow In dvClosedPreAuth
                '      If vRow("TransactionCode") = "PreAuth" Then
                ''      vRow("TransactionCode") = "PreAuthCapture"
                '  End If
                '     Next
            End If
        End If

        UpdatePaymentsAndCreditsByEmployee()
        DetermineSalesAndPayments()
        DisplaySalesAndPayments()

        '444        For Each oRow In dsEmployee.Tables("LoggedInEmployees").Rows
        '     If oRow("EmployeeID") = currentClockEmp.EmployeeID Then
        'clockOutJunk.TimeIn = oRow("LogInTime")
        '    End If
        '   Next
        clockOutJunk.TimeIn = currentClockEmp.LogInTime

        clockOutJunk.TimeOut = (Now.AddSeconds(-1 * Now.Second))
        clockOutJunk.TipableSales = totalSales
        clockOutJunk.DeclaredTips = declaredTips
        clockOutJunk.ChargedSales = chargePayments
        clockOutJunk.ChargedTips = chargeTips


        If isFullClockout = True Then
            UpdateEmployeeToLoginDatabase(currentClockEmp, clockOutJunk)
        End If

        If Not typeProgram = "Online_Demo" Then
            PrepareClockOutPrint()
        End If

        If isFullClockout = True Then
            '   currentServer = New Employee
            RaiseEvent ClockOutComplete()
            Me.Dispose()
        End If

    End Sub

    Private Function CountZeroTips()

        Dim zeroTips As Integer
        Dim vRow As DataRowView
        Dim orow As DataRow
        '      Dim count As Integer

        For Each vRow In dvClosedPreAuth
            If Not vRow("TransactionCode") Is DBNull.Value Then
                If vRow("TransactionCode") = "PreAuth" And vRow("Tip") = 0 Then
                    zeroTips += 1
                End If
            Else
                '   MsgBox("You have at least 1 Credit Card (" & vRow("AccountNumber") & ") not processed. Please process or delete beofer clocking out.")
                For Each orow In dsOrder.Tables("AvailTables").Rows
                    If orow("ExperienceNumber") = vRow("ExperienceNumber") Then
                        MsgBox("You have at least 1 Credit Card (Table: " & orow("TabName") & ") not processed. Please process or delete beofer clocking out.")
                        Return -99
                    End If
                Next
                For Each orow In dsOrder.Tables("AvailTabs").Rows
                    If orow("ExperienceNumber") = vRow("ExperienceNumber") Then
                        MsgBox("You have at least 1 Credit Card (Tab: " & orow("TabName") & ") not processed. Please process or delete beofer clocking out.")
                        Return -99
                    End If
                Next
                '  Return -99
            End If

        Next

        Return zeroTips

    End Function

    Private Sub btnClockOutCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClockOutCancel.Click

        MsgBox("You will NOT be Clocked Out.")


        GenerateOrderTables.UpdatePaymentsAndCreditsByEmployee()
        RaiseEvent ClockOutCancel()
        Me.Dispose()

    End Sub


    Private Sub PrepareClockOutPrint()
        Dim oRow As DataRow
        Dim numberOfHoursWeek As TimeSpan
        Dim numberOfMinWeek As Integer
        Dim numberOfMinShift As Integer

        Dim begDateOfWeek As Date
        begDateOfWeek = DetermineFirstDateOfWeek(0)
        Dim endDateOfWeek As Date
        endDateOfWeek = begDateOfWeek.AddDays(7)
        Dim prt As New PrintHelper

        '   numberOfHoursWeek = DetermineHoursWorked(currentServer.EmployeeID, begDateOfWeek)
        numberOfMinWeek = DetermineHoursWorked(currentClockEmp.EmployeeID, begDateOfWeek, endDateOfWeek)

        numberOfMinShift = DateDiff(DateInterval.Minute, currentClockEmp.LogInTime, clockOutJunk.TimeOut)
        clockOutJunk.ShiftHours = DetermineHours(numberOfMinShift)
        clockOutJunk.ShiftMins = DetermineRemainingMin(numberOfMinShift, clockOutJunk.ShiftHours)

        clockOutJunk.WeekHours = DetermineHours(numberOfMinWeek)
        clockOutJunk.WeekMins = DetermineRemainingMin(numberOfMinWeek, clockOutJunk.WeekHours)

        prt.ClockOutJunk = clockOutJunk
        prt.StartClockOutPrint()


    End Sub


    Private Sub PrepareClockOutReport222()
        Dim oRow As DataRow
        Dim numberOfHoursWeek As TimeSpan
        Dim numberOfMinWeek As Integer
        Dim numberOfMinShift As Integer

        Dim begDateOfWeek As Date
        begDateOfWeek = DetermineFirstDateOfWeek(1) '# is how many weeks ago
        Dim endDateOfWeek As Date
        endDateOfWeek = begDateOfWeek.AddDays(7)
        Dim prt As New PrintHelper

        '   numberOfHoursWeek = DetermineHoursWorked(currentServer.EmployeeID, begDateOfWeek)
        numberOfMinWeek = DetermineHoursWorked(currentClockEmp.EmployeeID, begDateOfWeek, endDateOfWeek)

        numberOfMinShift = DateDiff(DateInterval.Minute, currentClockEmp.LogInTime, clockOutJunk.TimeOut)
        clockOutJunk.ShiftHours = DetermineHours(numberOfMinShift)
        clockOutJunk.ShiftMins = DetermineRemainingMin(numberOfMinShift, clockOutJunk.ShiftHours)

        clockOutJunk.WeekHours = DetermineHours(numberOfMinWeek)
        clockOutJunk.WeekMins = DetermineRemainingMin(numberOfMinWeek, clockOutJunk.WeekHours)

        prt.ClockOutJunk = clockOutJunk
        prt.StartClockOutPrint()


    End Sub


    Private Function DetermineHours(ByVal numberOfMinWeek As Single) As Integer
        Dim hrs As Integer

        hrs = Int(numberOfMinWeek / 60)

        Return hrs

    End Function

    Private Function DetermineRemainingMin(ByVal numberOfMinWeek As Single, ByVal intHours As Single) As Single
        Dim min As Integer

        min = Int((numberOfMinWeek - (intHours * 60)))
        Return min

    End Function

    Private Function DetermineFirstDateOfWeek(ByVal numWeeksAgo As Integer) As Date

        Dim begDateOfWeek As Date
        Dim numOfDaysThisWeek As Integer
        '       numWeeksAgo -= 1

        If companyInfo.begOfWeek > Now.DayOfWeek Then
            numOfDaysThisWeek = 7 - (companyInfo.begOfWeek - Now.DayOfWeek)
        Else
            numOfDaysThisWeek = Now.DayOfWeek - companyInfo.begOfWeek
        End If

        begDateOfWeek = Now.AddDays(-1 * numOfDaysThisWeek)
        begDateOfWeek = begDateOfWeek.AddDays(-7 * numWeeksAgo)

        Return begDateOfWeek.ToShortDateString

    End Function




    Private Sub lblClockChargedTips_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblClockChargedTips.Click
        AdjustDeclaredTips()

    End Sub


    Private Sub label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles label4.Click
        AdjustDeclaredTips()
    End Sub

    Private Sub AdjustDeclaredTips()
        Me.adjustingClaimTips = True
        Me.NumberPadLarge1.DecimalUsed = True
        NumberPadLarge1.NumberTotal = declaredTips
        NumberPadLarge1.ShowNumberString()

        Me.NumberPadLarge1.Focus()
        Me.NumberPadLarge1.IntegerNumber = 0
        NumberPadLarge1.NumberString = ""

    End Sub

    Private Sub lblbAddTips_Click222(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblbAddTips.Click

        'currently not using
        'we just adjust tips to whatever we want
        '   this does not change charged tips, they are always calculated

        Me.adjustingClaimTips = False
        Me.NumberPadLarge1.DecimalUsed = True
        NumberPadLarge1.NumberTotal = declaredTips
        NumberPadLarge1.ShowNumberString()

        Me.NumberPadLarge1.Focus()
        Me.NumberPadLarge1.IntegerNumber = 0
        NumberPadLarge1.NumberString = ""



    End Sub




End Class
