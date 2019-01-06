Public Class CheckAdjustment_UC
    Inherits System.Windows.Forms.UserControl

    '   numberPadSamll is really NumberPadMEDIUM here

    Dim _paymentRowNum As Integer
    Dim _paymentColNum As Integer
    Dim _creditAmountAdjusted As Boolean
    Dim _cashFlag As Boolean
    Dim _remainingBalance As Decimal     'on both


    Friend Property PaymentRowNum() As Integer
        Get
            Return _paymentRowNum
        End Get
        Set(ByVal Value As Integer)
            _paymentRowNum = Value
        End Set
    End Property

    Friend Property PaymentColNum() As Integer
        Get
            Return _paymentColNum
        End Get
        Set(ByVal Value As Integer)
            _paymentColNum = Value
        End Set
    End Property

    Friend Property CreditAmountAdjusted() As Boolean
        Get
            Return _creditAmountAdjusted
        End Get
        Set(ByVal Value As Boolean)
            _creditAmountAdjusted = Value
        End Set
    End Property

    Friend Property CashFlag() As Boolean
        Get
            Return _cashFlag
        End Get
        Set(ByVal Value As Boolean)
            _cashFlag = Value
        End Set
    End Property

    Friend Property RemainingBalance() As Decimal
        Get
            Return _remainingBalance
        End Get
        Set(ByVal Value As Decimal)
            _remainingBalance = Value
        End Set
    End Property

    Event ApplyPayments(ByVal sender As Object, ByVal e As System.EventArgs)
    Event AuthButtonHit(ByRef authamount As PreAuthAmountClass, ByRef authtransaction As PreAuthTransactionClass, ByVal cardSwipedDatabaseInfo As Boolean)
    Event UC_Hit()



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
    Friend WithEvents grdPaymentTotals As System.Windows.Forms.DataGrid
    Friend WithEvents txtCloseRemain As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnApplyPayments As System.Windows.Forms.Button
    Friend WithEvents NumberPadSmall1 As DataSet_Builder.NumberPadMedium
    Friend WithEvents btnPaymentsAuth As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdPaymentTotals = New System.Windows.Forms.DataGrid
        Me.txtCloseRemain = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnApplyPayments = New System.Windows.Forms.Button
        Me.NumberPadSmall1 = New DataSet_Builder.NumberPadMedium
        Me.btnPaymentsAuth = New System.Windows.Forms.Button
        CType(Me.grdPaymentTotals, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdPaymentTotals
        '
        Me.grdPaymentTotals.AllowSorting = False
        Me.grdPaymentTotals.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grdPaymentTotals.CaptionVisible = False
        Me.grdPaymentTotals.DataMember = ""
        Me.grdPaymentTotals.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdPaymentTotals.Location = New System.Drawing.Point(8, 8)
        Me.grdPaymentTotals.Name = "grdPaymentTotals"
        Me.grdPaymentTotals.ReadOnly = True
        Me.grdPaymentTotals.RowHeadersVisible = False
        Me.grdPaymentTotals.Size = New System.Drawing.Size(272, 224)
        Me.grdPaymentTotals.TabIndex = 15
        '
        'txtCloseRemain
        '
        Me.txtCloseRemain.BackColor = System.Drawing.Color.LightSlateGray
        Me.txtCloseRemain.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCloseRemain.Location = New System.Drawing.Point(200, 264)
        Me.txtCloseRemain.Name = "txtCloseRemain"
        Me.txtCloseRemain.Size = New System.Drawing.Size(72, 20)
        Me.txtCloseRemain.TabIndex = 17
        Me.txtCloseRemain.Text = ""
        Me.txtCloseRemain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(208, 240)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Balance:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnApplyPayments
        '
        Me.btnApplyPayments.BackColor = System.Drawing.Color.Red
        Me.btnApplyPayments.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApplyPayments.Location = New System.Drawing.Point(8, 248)
        Me.btnApplyPayments.Name = "btnApplyPayments"
        Me.btnApplyPayments.Size = New System.Drawing.Size(88, 40)
        Me.btnApplyPayments.TabIndex = 19
        Me.btnApplyPayments.Text = "Apply"
        '
        'NumberPadSmall1
        '
        Me.NumberPadSmall1.BackColor = System.Drawing.Color.SlateGray
        Me.NumberPadSmall1.DecimalUsed = False
        Me.NumberPadSmall1.IntegerNumber = 0
        Me.NumberPadSmall1.Location = New System.Drawing.Point(280, 0)
        Me.NumberPadSmall1.Name = "NumberPadSmall1"
        Me.NumberPadSmall1.NumberString = Nothing
        Me.NumberPadSmall1.NumberTotal = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumberPadSmall1.Size = New System.Drawing.Size(192, 304)
        Me.NumberPadSmall1.TabIndex = 20
        '
        'btnPaymentsAuth
        '
        Me.btnPaymentsAuth.BackColor = System.Drawing.Color.SlateGray
        Me.btnPaymentsAuth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPaymentsAuth.Location = New System.Drawing.Point(104, 248)
        Me.btnPaymentsAuth.Name = "btnPaymentsAuth"
        Me.btnPaymentsAuth.Size = New System.Drawing.Size(88, 40)
        Me.btnPaymentsAuth.TabIndex = 21
        Me.btnPaymentsAuth.Text = "Auth"
        '
        'CheckAdjustment_UC
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.btnPaymentsAuth)
        Me.Controls.Add(Me.NumberPadSmall1)
        Me.Controls.Add(Me.btnApplyPayments)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCloseRemain)
        Me.Controls.Add(Me.grdPaymentTotals)
        Me.Name = "CheckAdjustment_UC"
        Me.Size = New System.Drawing.Size(472, 296)
        CType(Me.grdPaymentTotals, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region




    Private Sub InitializeOther()

        Me.NumberPadSmall1.DecimalUsed = True
        DisplayPaymentsAndCredits()

    End Sub


    Private Sub DisplayPaymentsAndCredits()

        '   we are using this in CLose Check class after we filter our dv
        Me.grdPaymentTotals.DataSource = dvUnAppliedPaymentsAndCredits 'dsOrder.Tables("PaymentsAndCredits") '

        Dim tsPaymentTotals As New DataGridTableStyle
        tsPaymentTotals.MappingName = "PaymentsAndCredits"
        tsPaymentTotals.RowHeadersVisible = False
        tsPaymentTotals.AllowSorting = False
        tsPaymentTotals.GridLineColor = Color.White

        Dim csPaymentType As New DataGridTextBoxColumn
        csPaymentType.MappingName = "PaymentTypeName"
        csPaymentType.HeaderText = "Pay Type"
        csPaymentType.Width = 70

        Dim csPaymentAmount As New DataGridTextBoxColumn
        csPaymentAmount.MappingName = "PaymentAmount"
        csPaymentAmount.HeaderText = "    Amount "
        '      csPaymentAmount.Alignment = HorizontalAlignment.Right
        csPaymentAmount.Width = 80

        Dim csTipAmount As New DataGridTextBoxColumn
        csTipAmount.MappingName = "Tip"
        csTipAmount.HeaderText = "    Tip "
        '     csTipAmount.Alignment = HorizontalAlignment.Right
        csTipAmount.Width = 53

        Dim csAuthCode As New DataGridTextBoxColumn
        csAuthCode.MappingName = "AuthCode"
        csAuthCode.HeaderText = " Auth    _"
        csAuthCode.Width = 65
        csAuthCode.ReadOnly = True
        csAuthCode.Alignment = HorizontalAlignment.Right

        '     Dim csBlankPay As New DataGridTextBoxColumn


        tsPaymentTotals.GridColumnStyles.Add(csPaymentType)
        tsPaymentTotals.GridColumnStyles.Add(csPaymentAmount)
        tsPaymentTotals.GridColumnStyles.Add(csTipAmount)
        tsPaymentTotals.GridColumnStyles.Add(csAuthCode)
        Me.grdPaymentTotals.TableStyles.Add(tsPaymentTotals)



    End Sub

    Private Sub NumberPadActivity() Handles NumberPadSmall1.NumberChanged
        RaiseEvent UC_Hit()

    End Sub

    Private Sub PaymentGrid_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPaymentTotals.CurrentCellChanged

        RaiseEvent UC_Hit()

        PaymentRowNum = Me.grdPaymentTotals.CurrentCell.RowNumber
        PaymentColNum = Me.grdPaymentTotals.CurrentCell.ColumnNumber

        '   numberPadSamll is really NumberPadMEDIUM here
        If PaymentColNum > 0 Then
            NumberPadSmall1.NumberTotal = Me.grdPaymentTotals(PaymentRowNum, PaymentColNum)
            NumberPadSmall1.ShowNumberString()
            NumberPadSmall1.Focus()
            NumberPadSmall1.IntegerNumber = 0
            NumberPadSmall1.NumberString = Nothing
        End If

        Dim authCode As String

        authCode = grdPaymentTotals(PaymentRowNum, 3)
        If authCode.Length > 1 Then
            Me.btnPaymentsAuth.Text = "Auth"
        Else
            Me.btnPaymentsAuth.Text = "Pre-Auth"
        End If


    End Sub

    Private Sub AdjustPaymentAmount(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumberPadSmall1.NumberEntered
        RaiseEvent UC_Hit()

        If PaymentColNum = 1 Then
            dvUnAppliedPaymentsAndCredits(PaymentRowNum)("PaymentAmount") = NumberPadSmall1.NumberTotal
            '   flags account to state already made manual adjustment
            '   therefore will not auto calculate
            If CashFlag = False Then
                CreditAmountAdjusted = True
            Else
                CashFlag = False
            End If
        ElseIf PaymentColNum = 2 Then
            dvUnAppliedPaymentsAndCredits(PaymentRowNum)("Tip") = NumberPadSmall1.NumberTotal
        ElseIf PaymentColNum = 3 Then
            '          dvUnAppliedPaymentsAndCredits(PaymentRowNum)("TipAdjustment") = NumberPadSmall1.NumberTotal
        End If

        ShowRemainingBalance()

    End Sub


    Friend Sub ShowRemainingBalance()
        Dim unauthorizedRemainingBalance As Decimal
        Dim vrow As DataRowView

        unauthorizedRemainingBalance = RemainingBalance

        For Each vrow In dvUnAppliedPaymentsAndCredits
            unauthorizedRemainingBalance -= vrow("PaymentAmount")
        Next
        Me.txtCloseRemain.Text = Format(unauthorizedRemainingBalance, "####0.00")


    End Sub


    Private Sub btnApplyPayments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyPayments.Click
        RaiseEvent UC_Hit()

        Dim oRow As DataRow
        Dim vRow As DataRowView
        Dim unappliedPayments As Decimal

        '   this is a test to verify payments are not more than check

        For Each vRow In dvUnAppliedPaymentsAndCredits
            unappliedPayments += vRow("PaymentAmount")

        Next

        '   ***************
        '   might change to not allow this
        If unappliedPayments > (RemainingBalance + 0.02) Then
            '   remainingBalance is what has already been applied
            If MsgBox("You are applying more than the balance. Are you sure?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

        RemainingBalance -= unappliedPayments

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If oRow("PaymentAmount") <> 0 Then
                If oRow("Applied") = False Then
                    oRow("Applied") = True
                End If
            End If
        Next


        RaiseEvent ApplyPayments(sender, e)


    End Sub


    Private Sub btnPaymentsAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentsAuth.Click
        RaiseEvent UC_Hit()

        '      Exit Sub
        '   ***    currently not using


        '     Dim authPayment As TStream
        Dim authTransaction As New PreAuthTransactionClass
        Dim authAmount As New PreAuthAmountClass
        Dim cardSwipedDatabaseInfo As Boolean

        PaymentRowNum = Me.grdPaymentTotals.CurrentCell.RowNumber


        authAmount.Purchase = dvUnAppliedPaymentsAndCredits(PaymentRowNum)("PaymentAmount").ToString
        authAmount.Authorize = Format((dvUnAppliedPaymentsAndCredits(PaymentRowNum)("PaymentAmount") * 1.2), "######.00").ToString
        '   authAmount.Gratuity = Nothing

        authTransaction.MerchantID = "494901"       'CompanyID & LocationID
        authTransaction.OperatorID = "eGlobal"       'currentServer.EmployeeID.ToString
        authTransaction.TranType = "Credit"
        authTransaction.TranCode = "PreAuth"
        authTransaction.InvoiceNo = dvUnAppliedPaymentsAndCredits(PaymentRowNum)("ExperienceNumber") & "-" & dvUnAppliedPaymentsAndCredits(PaymentRowNum)("CheckNumber")
        authTransaction.RefNo = dvUnAppliedPaymentsAndCredits(PaymentRowNum)("ExperienceNumber") & "-" & dvUnAppliedPaymentsAndCredits(PaymentRowNum)("CheckNumber")

        If Not dvUnAppliedPaymentsAndCredits(PaymentRowNum)("SwipeType") Is DBNull.Value Then
            If dvUnAppliedPaymentsAndCredits(PaymentRowNum)("SwipeType") = 1 Then
                cardSwipedDatabaseInfo = True
            Else
                cardSwipedDatabaseInfo = False
            End If
        End If


        RaiseEvent AuthButtonHit(authAmount, authTransaction, cardSwipedDatabaseInfo)



    End Sub


    Private Sub OldAuth222()

        Dim authPayment As New DataSet_Builder.Payment

        If PaymentRowNum >= 0 Then
            authPayment.InvoiceNo = dvUnAppliedPaymentsAndCredits(PaymentRowNum)("CheckNumber")
            If Not dvUnAppliedPaymentsAndCredits(PaymentRowNum)("RefNum") Is DBNull.Value Then
                authPayment.RefNo = dvUnAppliedPaymentsAndCredits(PaymentRowNum)("RefNum")
            End If

            If Not dvUnAppliedPaymentsAndCredits(PaymentRowNum)("SwipeType") Is DBNull.Value Then
                If dvUnAppliedPaymentsAndCredits(PaymentRowNum)("SwipeType") = 1 Then
                    authPayment.Swiped = True
                Else
                    authPayment.Swiped = False
                End If
            End If

            If Not dvUnAppliedPaymentsAndCredits(PaymentRowNum)("Track2") Is DBNull.Value Then
                authPayment.Track2 = dvUnAppliedPaymentsAndCredits(PaymentRowNum)("Track2")
            End If
            If Not dvUnAppliedPaymentsAndCredits(PaymentRowNum)("CustomerName") Is DBNull.Value Then
                authPayment.Name = dvUnAppliedPaymentsAndCredits(PaymentRowNum)("CustomerName")
            End If

            If Not dvUnAppliedPaymentsAndCredits(PaymentRowNum)("PaymentAmount") Is DBNull.Value Then
                authPayment.Purchase = dvUnAppliedPaymentsAndCredits(PaymentRowNum)("PaymentAmount")
                authPayment.Authorize = authPayment.Purchase * 1.2
            End If
            If Not dvUnAppliedPaymentsAndCredits(PaymentRowNum)("Tip") Is DBNull.Value Then
                authPayment.Gratuity = dvUnAppliedPaymentsAndCredits(PaymentRowNum)("Tip")
            End If
            authPayment.AuthCode = dvUnAppliedPaymentsAndCredits(PaymentRowNum)("AuthCode")
            If Not dvUnAppliedPaymentsAndCredits(PaymentRowNum)("AcqRefData") Is DBNull.Value Then
                authPayment.AcqRefData = dvUnAppliedPaymentsAndCredits(PaymentRowNum)("AcqRefData")
            End If

        End If

    End Sub


End Class
