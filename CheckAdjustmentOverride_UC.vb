Imports DataSet_Builder

Public Class CheckAdjustmentOverride_UC
    Inherits System.Windows.Forms.UserControl

    Private _adjGridDataSource As DataView
    Dim prt As New PrintHelper
    Private WithEvents PaywarePCCharge As SIM.Charge

    Dim _paymentRowNum As Integer
    Dim _paymentColNum As Integer
    '   Dim _cashFlag As Boolean
    Dim _remainingBalance As Decimal     'on both


    Public Property AdjGridDataSource() As DataView
        Get
            Return _adjGridDataSource
        End Get
        Set(ByVal Value As DataView)
            _adjGridDataSource = Value
        End Set
    End Property

    Public Property PaymentRowNum() As Integer
        Get
            Return _paymentRowNum
        End Get
        Set(ByVal Value As Integer)
            _paymentRowNum = Value
        End Set
    End Property

    Public Property PaymentColNum() As Integer
        Get
            Return _paymentColNum
        End Get
        Set(ByVal Value As Integer)
            _paymentColNum = Value
        End Set
    End Property

    '   Friend Property CashFlag() As Boolean
    '      Get
    '         Return _cashFlag
    '    End Get
    '   Set(ByVal Value As Boolean)
    '      _cashFlag = Value
    ' End Set
    ' End Property
    Public Property RemainingBalance() As Decimal
        Get
            Return _remainingBalance
        End Get
        Set(ByVal Value As Decimal)
            _remainingBalance = Value
        End Set
    End Property


    '   ******************************************************
    '   waiting to do until after credit card payment in place
    '   ******************************************************


    Event UpdateAdjGridPayment()






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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtCloseRemain As System.Windows.Forms.TextBox
    Friend WithEvents btnDeletePayment As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdPaymentTotals = New System.Windows.Forms.DataGrid
        Me.txtCloseRemain = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnDeletePayment = New System.Windows.Forms.Button
        CType(Me.grdPaymentTotals, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdPaymentTotals
        '
        Me.grdPaymentTotals.AllowSorting = False
        Me.grdPaymentTotals.AlternatingBackColor = System.Drawing.Color.Black
        Me.grdPaymentTotals.BackColor = System.Drawing.Color.Black
        Me.grdPaymentTotals.BackgroundColor = System.Drawing.Color.Black
        Me.grdPaymentTotals.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPaymentTotals.CaptionVisible = False
        Me.grdPaymentTotals.DataMember = ""
        Me.grdPaymentTotals.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPaymentTotals.ForeColor = System.Drawing.Color.White
        Me.grdPaymentTotals.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdPaymentTotals.Location = New System.Drawing.Point(8, 8)
        Me.grdPaymentTotals.Name = "grdPaymentTotals"
        Me.grdPaymentTotals.ReadOnly = True
        Me.grdPaymentTotals.RowHeadersVisible = False
        Me.grdPaymentTotals.Size = New System.Drawing.Size(422, 224)
        Me.grdPaymentTotals.TabIndex = 15
        '
        'txtCloseRemain
        '
        Me.txtCloseRemain.BackColor = System.Drawing.Color.LightSlateGray
        Me.txtCloseRemain.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCloseRemain.Location = New System.Drawing.Point(184, 264)
        Me.txtCloseRemain.Name = "txtCloseRemain"
        Me.txtCloseRemain.Size = New System.Drawing.Size(88, 20)
        Me.txtCloseRemain.TabIndex = 17
        Me.txtCloseRemain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCloseRemain.Visible = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(120, 264)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Balance:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label1.Visible = False
        '
        'btnDeletePayment
        '
        Me.btnDeletePayment.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnDeletePayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeletePayment.Location = New System.Drawing.Point(16, 240)
        Me.btnDeletePayment.Name = "btnDeletePayment"
        Me.btnDeletePayment.Size = New System.Drawing.Size(80, 48)
        Me.btnDeletePayment.TabIndex = 21
        Me.btnDeletePayment.Text = "Delete"
        Me.btnDeletePayment.UseVisualStyleBackColor = False
        '
        'CheckAdjustmentOverride_UC
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.Controls.Add(Me.btnDeletePayment)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCloseRemain)
        Me.Controls.Add(Me.grdPaymentTotals)
        Me.Name = "CheckAdjustmentOverride_UC"
        Me.Size = New System.Drawing.Size(472, 296)
        CType(Me.grdPaymentTotals, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Sub InitializeOther()

        '      MsgBox(dvPaymentsAndCredits.Count)
        '     MsgBox(dsOrder.Tables("PaymentsAndCredits").Rows.Count)


        Dim tsPaymentTotals As New DataGridTableStyle
        tsPaymentTotals.MappingName = "PaymentsAndCredits"
        tsPaymentTotals.RowHeadersVisible = False
        tsPaymentTotals.AllowSorting = False
        tsPaymentTotals.GridLineColor = Color.Black
        tsPaymentTotals.AlternatingBackColor = Color.Black
        tsPaymentTotals.BackColor = Color.Black
        tsPaymentTotals.ForeColor = Color.White
        tsPaymentTotals.SelectionBackColor = c15

        Dim csPaymentType As New DataGridTextBoxColumn
        csPaymentType.MappingName = "PaymentTypeName"
        csPaymentType.HeaderText = "Pay Type"
        csPaymentType.NullText = ""
        csPaymentType.Width = 95

        Dim csAccount As New DataGridTextBoxColumn
        csAccount.MappingName = "AccountNumber"
        csAccount.HeaderText = "Acct "
        csAccount.NullText = ""
        csAccount.Alignment = HorizontalAlignment.Right
        csAccount.Width = 140

        Dim csDescription As New DataGridTextBoxColumn
        csDescription.MappingName = "Description"
        csDescription.HeaderText = " " '"Disposition"
        csDescription.NullText = ""
        csDescription.Alignment = HorizontalAlignment.Right
        csDescription.Width = 100

        Dim csPaymentAmount As New DataGridTextBoxColumn
        csPaymentAmount.MappingName = "PaymentAmount"
        csPaymentAmount.HeaderText = "Amount "
        csPaymentAmount.NullText = ""
        csPaymentAmount.Alignment = HorizontalAlignment.Right
        csPaymentAmount.Width = 83


        '       Dim csTipAdj As New DataGridTextBoxColumn
        '      csTipAdj.MappingName = "Surcharge"  '"TipAdjustment"
        '     csTipAdj.HeaderText = "Tip adj "
        '    '    csTipAdj.Alignment = HorizontalAlignment.Right
        '   csTipAdj.Width = 50

        Dim csBlankPay As New DataGridTextBoxColumn

        tsPaymentTotals.GridColumnStyles.Add(csPaymentType)
        tsPaymentTotals.GridColumnStyles.Add(csAccount)
        tsPaymentTotals.GridColumnStyles.Add(csDescription)
        tsPaymentTotals.GridColumnStyles.Add(csPaymentAmount)
        '    tsPaymentTotals.GridColumnStyles.Add(csTipAdj)
        Me.grdPaymentTotals.TableStyles.Add(tsPaymentTotals)
        Me.AdjGridDataSource = dvPaymentsAndCredits
        Me.ChangePaymentDataSource()

        If dvPaymentsAndCredits.Count = 0 Then
            _paymentRowNum = -1
        End If
        ShowRemainingBalance()

    End Sub

    Private Sub gridPaymentTotals_CellChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdPaymentTotals.CurrentCellChanged
        _paymentRowNum = grdPaymentTotals.CurrentRowIndex

    End Sub


    Public Sub ChangePaymentDataSource()
        Me.grdPaymentTotals.DataSource = Me.AdjGridDataSource

    End Sub

    Friend Sub ShowRemainingBalance()
        Dim unauthorizedRemainingBalance As Decimal
        Dim vrow As DataRowView


        unauthorizedRemainingBalance = RemainingBalance

        '      For Each vrow In dvUnAppliedPaymentsAndCredits
        '     unauthorizedRemainingBalance -= vrow("PaymentAmount")
        '    Next

        Me.txtCloseRemain.Text = Format(unauthorizedRemainingBalance, "####0.00")

    End Sub

    Private Sub PrintCreditCardReceipt(ByRef orow As DataRow, ByRef vRow As DataRowView, ByVal useVIEW As Boolean)

        prt.ccDataRow = orow
        prt.ccDataRowView = vRow
        prt.useVIEW = useVIEW
        prt.StartPrintCreditCardVoid()

        '   ***
        '  vRow("AccountNumber") = TruncateAccountNumber(vRow("AccountNumber"))

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePayment.Click

        Dim voidWentThrough As Boolean
        Dim authStatus As String

        If PaymentRowNum > -1 Then
            If dvPaymentsAndCredits(PaymentRowNum)("PaymentTypeID") = -98 Then
                'gift certificate
                dvPaymentsAndCredits(PaymentRowNum)("TransactionCode") = "Voided"
                dvPaymentsAndCredits(PaymentRowNum)("SwipeType") = -9
                dvPaymentsAndCredits(PaymentRowNum)("TerminalID") = actingManager.EmployeeID
                dvPaymentsAndCredits(PaymentRowNum)("Description") = "Voided"
                '    dvPaymentsAndCredits(PaymentRowNum)("AuthCode") = DBNull.Value
                '   dvPaymentsAndCredits(PaymentRowNum)("Applied") = False

                PrintCreditCardReceipt(Nothing, dvPaymentsAndCredits(PaymentRowNum), True)

                RaiseEvent UpdateAdjGridPayment()
                ShowRemainingBalance()

                If dvPaymentsAndCredits.Count = 0 Then
                    _paymentRowNum = -1
                End If
                Exit Sub
            End If

            If dvPaymentsAndCredits(PaymentRowNum)("TransactionCode") = "PreAuth" Or dvPaymentsAndCredits(PaymentRowNum)("TransactionCode") = "Credit" Or dvPaymentsAndCredits(PaymentRowNum)("TransactionCode") = "Sale" Then
                'transaction Code "Credit" is for manual credit cards, or outside
                ' code "Sale" is for Gift
                If companyInfo.processor = "Mercury" Then

                    If dvPaymentsAndCredits(PaymentRowNum)("TransactionCode") = "Sale" Then
                        'this is for void sale of Gift Card (MPS only)
                        authStatus = GenerateOrderTables.GiftCardTransaction(dvPaymentsAndCredits(PaymentRowNum), Nothing, "VoidSale")
                        If authStatus = "MPS Gift Card" Then
                            MsgBox(authStatus)
                            Exit Sub
                        ElseIf authStatus = "Approved" Then
                            voidWentThrough = True
                            '      dvPaymentsAndCredits(PaymentRowNum)("OpenBigInt1") = currentTable.TabID
                        End If
                    Else 'this is just regular Mercury PreAuth
                        'we just need to not send Capture
                        voidWentThrough = True
                    End If
                ElseIf companyInfo.processor = "PaywarePC" Then
                    voidWentThrough = VoidPaywarePCSale(dvPaymentsAndCredits(PaymentRowNum))
                End If


                If voidWentThrough = True Then
                    dvPaymentsAndCredits(PaymentRowNum)("TransactionCode") = "Voided"
                    dvPaymentsAndCredits(PaymentRowNum)("SwipeType") = -9
                    dvPaymentsAndCredits(PaymentRowNum)("TerminalID") = actingManager.EmployeeID
                    dvPaymentsAndCredits(PaymentRowNum)("Description") = "Voided"
                    '    dvPaymentsAndCredits(PaymentRowNum)("AuthCode") = DBNull.Value
                    '   dvPaymentsAndCredits(PaymentRowNum)("Applied") = False

                    PrintCreditCardReceipt(Nothing, dvPaymentsAndCredits(PaymentRowNum), True)

                End If

            Else

                '        dvPaymentsAndCredits(PaymentRowNum).Delete()
            End If

            RaiseEvent UpdateAdjGridPayment()
            ShowRemainingBalance()

            If dvPaymentsAndCredits.Count = 0 Then
                _paymentRowNum = -1
            End If

        End If

        Exit Sub
        '222 below
        If PaymentRowNum > -1 Then
            dvPaymentsAndCredits(PaymentRowNum).Delete()
            ShowRemainingBalance()

            If dvPaymentsAndCredits.Count = 0 Then
                _paymentRowNum = -1
            End If

        End If


    End Sub

    Private Function VoidPaywarePCSale(ByVal vRow As DataRowView) As Boolean

        PaywarePCCharge = New SIM.Charge

        GenerateOrderTables.ReadyToProcessPaywarePC(PaywarePCCharge)

        With PaywarePCCharge

            .Amount = (Format(vRow("PaymentAmount") + vRow("Surcharge"), "#####0.00")).ToString
            .TroutD = vRow("RefNum")
            .Action = SIM.Charge.Command.Credit_Void

            If .Process Then
                Try
                    If .GetResult = "VOIDED" Then

                        Return True

                    Else ' If .GetResult = "DECLINED" Or .GetResultCode = "6" Then
                        MsgBox("CARD '" & vRow("AccountNumber") & "' " & .GetResult & ": " & .GetResponseText)
                        'MsgBox("CARD '" & vRow("AccountNumber") & "' DECLINED: " & .GetResponseText)
                    End If

                Catch ex As Exception

                End Try

                '            MsgBox(.GetResult)
                '           MsgBox(.GetAuthCode)
                '          MsgBox(.GetReference)
                '         MsgBox(.GetResultCode)
                '        MsgBox(.GetTroutD)
                '       MsgBox(.GetResponseText)

            Else
                MsgBox("" & .ErrorCode & ": " & .ErrorDescription)
            End If
        End With
    End Function
End Class
