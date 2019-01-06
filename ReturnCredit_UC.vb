
Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Xml.Serialization
Imports System.Runtime.InteropServices
Imports DataSet_Builder

Public Class ReturnCredit_UC
    Inherits System.Windows.Forms.UserControl

    Dim dsi As New DSICLIENTXLib.DSICLientX
    '   Dim dsi As New AxDSICLIENTXLib.AxDSICLientX

    Dim WithEvents readAuth As New ReadCredit(False)
    '   Dim sql As New DataSet_Builder.SQLHelper(connectserver)
    '   Dim sWriter1 As StreamWriter

    Dim mpsTStream As TStream
    Dim mpsTransaction As PreAuthTransactionClass
    Dim mpsAmount As PreAuthAmountClass
    Dim mpsAccount As AccountClass


    Dim isReadyForReturn As Boolean

    Dim returnTrack2 As String
    Dim returnAcctNum As String
    Dim returnExpDate As String
    Dim returnAmount As Decimal = 0
    Dim returnInvoiceNumber As Integer
    '  Dim returnPaymentTypeID As Integer
    Dim returnPaymentTypeName As String


    Dim returnPayment As New DataSet_Builder.Payment


    Public ReturnPanelActive As PanelActive

    Public Enum PanelActive As Integer
      
        AccountPanel = 1
        ExpDatePanel = 2
        ReturnAmountPanel = 3
        InvoicePanel = 4

    End Enum

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
    Friend WithEvents NumberPadLarge1 As DataSet_Builder.NumberPadLarge
    Friend WithEvents lblAcctNum As System.Windows.Forms.Label
    Friend WithEvents lblReturnAmount As System.Windows.Forms.Label
    Friend WithEvents lblAcctNumDetail As System.Windows.Forms.Label
    Friend WithEvents lblInvoiceNum As System.Windows.Forms.Label
    Friend WithEvents lblReturnAmountDetail As System.Windows.Forms.Label
    Friend WithEvents lblInvoiceNumDetail As System.Windows.Forms.Label
    Friend WithEvents pnlInvoiceDetail As System.Windows.Forms.Panel
    Friend WithEvents lblExpDateDetail As System.Windows.Forms.Label
    Friend WithEvents btnReturn As System.Windows.Forms.Button
    Friend WithEvents btnCanel As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.NumberPadLarge1 = New DataSet_Builder.NumberPadLarge
        Me.lblAcctNum = New System.Windows.Forms.Label
        Me.lblReturnAmount = New System.Windows.Forms.Label
        Me.lblInvoiceNum = New System.Windows.Forms.Label
        Me.lblAcctNumDetail = New System.Windows.Forms.Label
        Me.lblReturnAmountDetail = New System.Windows.Forms.Label
        Me.lblInvoiceNumDetail = New System.Windows.Forms.Label
        Me.btnReturn = New System.Windows.Forms.Button
        Me.btnCanel = New System.Windows.Forms.Button
        Me.pnlInvoiceDetail = New System.Windows.Forms.Panel
        Me.lblExpDateDetail = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'NumberPadLarge1
        '
        Me.NumberPadLarge1.BackColor = System.Drawing.Color.SlateGray
        Me.NumberPadLarge1.DecimalUsed = True
        Me.NumberPadLarge1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.NumberPadLarge1.IntegerNumber = 0
        Me.NumberPadLarge1.Location = New System.Drawing.Point(392, 136)
        Me.NumberPadLarge1.Name = "NumberPadLarge1"
        Me.NumberPadLarge1.NumberString = ""
        Me.NumberPadLarge1.NumberTotal = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumberPadLarge1.Size = New System.Drawing.Size(240, 368)
        Me.NumberPadLarge1.TabIndex = 0
        '
        'lblAcctNum
        '
        Me.lblAcctNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcctNum.Location = New System.Drawing.Point(56, 16)
        Me.lblAcctNum.Name = "lblAcctNum"
        Me.lblAcctNum.Size = New System.Drawing.Size(240, 40)
        Me.lblAcctNum.TabIndex = 1
        Me.lblAcctNum.Text = "Swipe Credit Card:"
        '
        'lblReturnAmount
        '
        Me.lblReturnAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReturnAmount.Location = New System.Drawing.Point(56, 96)
        Me.lblReturnAmount.Name = "lblReturnAmount"
        Me.lblReturnAmount.Size = New System.Drawing.Size(240, 40)
        Me.lblReturnAmount.TabIndex = 2
        Me.lblReturnAmount.Text = "Enter Return Amount $:"
        '
        'lblInvoiceNum
        '
        Me.lblInvoiceNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceNum.Location = New System.Drawing.Point(56, 176)
        Me.lblInvoiceNum.Name = "lblInvoiceNum"
        Me.lblInvoiceNum.Size = New System.Drawing.Size(240, 56)
        Me.lblInvoiceNum.TabIndex = 3
        Me.lblInvoiceNum.Text = "Enter Invoice Number from old Ticket:"
        '
        'lblAcctNumDetail
        '
        Me.lblAcctNumDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcctNumDetail.Location = New System.Drawing.Point(56, 56)
        Me.lblAcctNumDetail.Name = "lblAcctNumDetail"
        Me.lblAcctNumDetail.Size = New System.Drawing.Size(168, 32)
        Me.lblAcctNumDetail.TabIndex = 4
        Me.lblAcctNumDetail.Text = "Account Number"
        Me.lblAcctNumDetail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblReturnAmountDetail
        '
        Me.lblReturnAmountDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReturnAmountDetail.Location = New System.Drawing.Point(88, 136)
        Me.lblReturnAmountDetail.Name = "lblReturnAmountDetail"
        Me.lblReturnAmountDetail.Size = New System.Drawing.Size(160, 32)
        Me.lblReturnAmountDetail.TabIndex = 5
        Me.lblReturnAmountDetail.Text = "Return Amount"
        Me.lblReturnAmountDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblInvoiceNumDetail
        '
        Me.lblInvoiceNumDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceNumDetail.Location = New System.Drawing.Point(104, 232)
        Me.lblInvoiceNumDetail.Name = "lblInvoiceNumDetail"
        Me.lblInvoiceNumDetail.Size = New System.Drawing.Size(144, 32)
        Me.lblInvoiceNumDetail.TabIndex = 6
        Me.lblInvoiceNumDetail.Text = "Invoice Number"
        Me.lblInvoiceNumDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnReturn
        '
        Me.btnReturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReturn.Location = New System.Drawing.Point(376, 40)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(120, 64)
        Me.btnReturn.TabIndex = 7
        Me.btnReturn.Text = "Return"
        '
        'btnCanel
        '
        Me.btnCanel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCanel.Location = New System.Drawing.Point(520, 40)
        Me.btnCanel.Name = "btnCanel"
        Me.btnCanel.Size = New System.Drawing.Size(120, 64)
        Me.btnCanel.TabIndex = 8
        Me.btnCanel.Text = "Cancel"
        '
        'pnlInvoiceDetail
        '
        Me.pnlInvoiceDetail.Location = New System.Drawing.Point(40, 272)
        Me.pnlInvoiceDetail.Name = "pnlInvoiceDetail"
        Me.pnlInvoiceDetail.Size = New System.Drawing.Size(272, 232)
        Me.pnlInvoiceDetail.TabIndex = 9
        '
        'lblExpDateDetail
        '
        Me.lblExpDateDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpDateDetail.Location = New System.Drawing.Point(232, 56)
        Me.lblExpDateDetail.Name = "lblExpDateDetail"
        Me.lblExpDateDetail.Size = New System.Drawing.Size(64, 32)
        Me.lblExpDateDetail.TabIndex = 10
        Me.lblExpDateDetail.Text = "Exp Date"
        Me.lblExpDateDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ReturnCredit_UC
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(236, Byte), CType(233, Byte), CType(216, Byte))
        Me.Controls.Add(Me.lblExpDateDetail)
        Me.Controls.Add(Me.pnlInvoiceDetail)
        Me.Controls.Add(Me.btnCanel)
        Me.Controls.Add(Me.btnReturn)
        Me.Controls.Add(Me.lblInvoiceNumDetail)
        Me.Controls.Add(Me.lblReturnAmountDetail)
        Me.Controls.Add(Me.lblAcctNumDetail)
        Me.Controls.Add(Me.lblInvoiceNum)
        Me.Controls.Add(Me.lblReturnAmount)
        Me.Controls.Add(Me.lblAcctNum)
        Me.Controls.Add(Me.NumberPadLarge1)
        Me.Name = "ReturnCredit_UC"
        Me.Size = New System.Drawing.Size(696, 528)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub InitializeOther()

        currentTable = New DinnerTable

        '     ReturnPanelActive = PanelActive.InvoicePanel
        Me.lblAcctNum.BackColor = c6
        Me.lblAcctNum.ForeColor = c3

        Dim returnhamount As PreAuthAmountClass
        Dim returnTransaction As PreAuthTransactionClass


 '       readAuth = New ReadCredit
        '      readAuth.CloseManualAuth_Load()

        ActivateAcctNumberPanel()

    End Sub

    Private Sub ResetPanels()
     
        Me.lblAcctNum.BackColor = c11
        Me.lblAcctNum.ForeColor = c2
        Me.lblReturnAmount.BackColor = c11
        Me.lblReturnAmount.ForeColor = c2
        Me.lblInvoiceNum.BackColor = c11
        Me.lblInvoiceNum.ForeColor = c2



    End Sub

    Private Sub ActivateAcctNumberPanel()
        ResetPanels()
        Me.ReturnPanelActive = PanelActive.AccountPanel
        Me.lblAcctNum.BackColor = c6
        Me.lblAcctNum.ForeColor = c3

        NumberPadLarge1.DecimalUsed = False
        If companyInfo.usingOutsideCreditProcessor = False Then
            NumberPadLarge1.NumberString = "Enter Acct Number "
        End If
        NumberPadLarge1.ShowNumberString()
        NumberPadLarge1.NumberString = ""
       
    End Sub

    Private Sub ActivateExpDatePanel()
        ResetPanels()
        Me.ReturnPanelActive = PanelActive.ExpDatePanel
        Me.lblAcctNum.BackColor = c6
        Me.lblAcctNum.ForeColor = c3


        NumberPadLarge1.DecimalUsed = False
        NumberPadLarge1.NumberString = "Enter Exp Date "
        NumberPadLarge1.ShowNumberString()
        NumberPadLarge1.NumberString = ""

    End Sub

    Private Sub ActiveReturnAmountPanel()
        ResetPanels()
        Me.ReturnPanelActive = PanelActive.ReturnAmountPanel
        Me.lblReturnAmount.BackColor = c6
        Me.lblReturnAmount.ForeColor = c3

        Me.NumberPadLarge1.DecimalUsed = True
        NumberPadLarge1.NumberString = Me.returnAmount


    End Sub

    Private Sub ActivateInvoiceNumPanel()
        ResetPanels()
        Me.ReturnPanelActive = PanelActive.InvoicePanel
        Me.lblInvoiceNum.BackColor = c6
        Me.lblInvoiceNum.ForeColor = c3

        Me.NumberPadLarge1.DecimalUsed = False
        Me.NumberPadLarge1.NumberString = Me.returnInvoiceNumber
        Me.NumberPadLarge1.ShowNumberString()
        Me.NumberPadLarge1.NumberString = ""

    End Sub


    Private Sub NewCardRead(ByRef newPayment As DataSet_Builder.Payment) Handles readAuth.CardReadSuccessful

        returnPayment = newPayment

        Me.returnTrack2 = newPayment.Track2
        Me.lblAcctNumDetail.Text = newPayment.AccountNumber
        Me.lblExpDateDetail.Text = newPayment.ExpDate
      
        GoToNextState()

    End Sub

    Private Sub CardRead_Failed() Handles readAuth.CardReadFailed

        MsgBox("Card Read FAILED")
        Me.lblAcctNum.Text = "Enter Account Number:"
        ActivateAcctNumberPanel()

    End Sub

    Private Sub PaymentEnterHit(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumberPadLarge1.NumberEntered
      
        Select Case ReturnPanelActive
            Case PanelActive.AccountPanel

                '        Dim ccID As Integer

                returnPaymentTypeName = DetermineCreditCardName(Me.NumberPadLarge1.NumberString)
                If returnPaymentTypeName.Length > 0 Then
                    '          ccID = DetermineCreditCardID(Me.NumberPadLarge1.NumberString)
                    Me.returnAcctNum = Me.NumberPadLarge1.NumberString
                    Me.lblAcctNumDetail.Text = Me.NumberPadLarge1.NumberString
                    GoToNextState()

                End If

            Case PanelActive.ExpDatePanel

                If Me.NumberPadLarge1.NumberString.Length = 4 Then
                    Me.returnExpDate = Me.NumberPadLarge1.NumberString
                    Me.lblExpDateDetail.Text = Me.NumberPadLarge1.NumberString
                    GoToNextState()

                Else
                    MsgBox("Expiration Date Must be in MMYY Format")
                    Me.NumberPadLarge1.NumberString = "Enter Exp Date "
                    Me.NumberPadLarge1.ShowNumberString()
                    Me.NumberPadLarge1.NumberString = ""
                End If


            Case PanelActive.ReturnAmountPanel

                Me.lblReturnAmountDetail.Text = "$  " & Format(Me.NumberPadLarge1.NumberTotal, "##,###.00")
                Me.returnAmount = (Me.NumberPadLarge1.NumberTotal)
                GoToNextState()

            Case PanelActive.InvoicePanel

                Me.returnInvoiceNumber = Me.NumberPadLarge1.NumberString
                Me.lblInvoiceNumDetail.Text = Me.NumberPadLarge1.NumberString
                GoToNextState()

        End Select

    End Sub

    Private Sub GoToNextState()

        If Me.returnTrack2 = Nothing Then

            If Me.returnAcctNum = Nothing Then
                ActivateAcctNumberPanel()
                Exit Sub
            ElseIf Me.returnExpDate = Nothing Then
                ActivateExpDatePanel()
                Exit Sub
            End If

        ElseIf Me.returnAmount = 0 Then

            ActiveReturnAmountPanel()
            Exit Sub


        ElseIf Me.returnInvoiceNumber = Nothing Then
            ActivateInvoiceNumPanel()
            '   when it passes all above it 
            '    isReadyForReturn = True    we don't need invoice#

        End If

        isReadyForReturn = True


    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCanel.Click
        '       readAuth.tmrCardRead.Dispose()
        '      readAuth.tmrCardRead = Nothing
        currentTable = Nothing
        returnPayment = Nothing
        tmrCardRead.Stop()
        RemoveHandler tmrCardRead.Tick, AddressOf readAuth.tmrCardRead_Tick
        Me.Dispose()

    End Sub

    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click

        MsgBox("Your restaurant is not setup for Credit Card Return.")
        Exit Sub



        If isReadyForReturn = False Then
            GoToNextState()
            Exit Sub
        End If

        StartTransaction()

    End Sub


    Public Sub StartTransaction()

        mpsTStream = New TStream
        mpsTransaction = New PreAuthTransactionClass
        mpsAmount = New PreAuthAmountClass
        mpsAccount = New AccountClass

        'creates new experience number
        '   this will never show up anywhere, we just need it to define the return in PaymentsAndCredits
        Dim tabRow As DataRow = dsOrder.Tables("AvailTabs").NewRow

        PerformNewExperienceAdd(tabRow, Nothing, actingManager.EmployeeID, Nothing, -999, "Return", 1, 1, -777, 0, currentServer.LoginTrackingID)
        '-999  to indicate is a tab
        '-777 to indicate is a return
        tabRow("ClosedSubTotal") = -1 * Me.returnAmount
        dsOrder.Tables("AvailTabs").Rows.Add(tabRow)
        tabRow("Reference") = Me.returnInvoiceNumber
        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlAvailTabsSP.Update(dsOrder, "AvailTabs")
            sql.cn.Close()

        Catch ex As Exception
            CloseConnection()
            If mainServerConnected = False Then
            Else
                ServerJustWentDown()
            End If
        Finally

        End Try

        'this so we can populate PaymentRow
        currentTable.ExperienceNumber = tabRow("ExperienceNumber")
        currentTable.EmployeeID = actingManager.EmployeeID
        currentTable.CheckNumber = 1

        If returnPayment.PaymentTypeName = Nothing Then
            returnPayment.PaymentTypeName = DetermineCreditCardName(returnAcctNum)
        End If

        '   other payments defined in ReadAuth or when entered from NumberPad
        With returnPayment
            .PaymentTypeID = DetermineCreditCardID(returnPayment.PaymentTypeName)
            .Purchase = (-1 * Me.returnAmount)
            .TranType = "Credit"
            .TranCode = "Return"
            .RefNo = returnInvoiceNumber

            If .Swiped = False Then
                .AccountNumber = Me.returnAcctNum
                .ExpDate = Me.returnExpDate
            End If
            returnPayment.Track2 = Nothing
        End With

        GenerateOrderTables.AddPaymentToDataRow(returnPayment, True, currentTable.ExperienceNumber, actingManager.EmployeeID, currentTable.CheckNumber, False)
        GenerateOrderTables.UpdatePaymentsAndCredits()
        tmrCardRead.Stop()
        RemoveHandler tmrCardRead.Tick, AddressOf readAuth.tmrCardRead_Tick
        '

        MsgBox("Return went through. Receipt does not print for Credit Return. Give customer a handwritten receipt.")

        '        Dim prt As New PrintHelper
        '       prt.closeDetail.isCashTendered = True
        '      prt.closeDetail.chkTendered = reducePaymentAmount
        '     prt.closeDetail.chkChangeDue = reducePaymentAmount - RemainingBalance
        '    prt.StartPrintCheckReceipt()
        '   MsgBox("Printer Not Conected")
        Me.Dispose()
        Exit Sub
        '*****************
        '   for testing only
        '   we do not send to Mercury until close of day

        With returnPayment
            If .Swiped = False Then
                mpsAccount.AcctNo = .AccountNumber
                mpsAccount.ExpDate = .ExpDate
            Else
                mpsAccount.Track2 = .Track2
            End If

            mpsAmount.Purchase = .Purchase  'Me.returnAmount

            mpsTransaction.MerchantID = companyInfo.merchantID
            mpsTransaction.OperatorID = companyInfo.operatorID
            mpsTransaction.TranType = .TranType
            mpsTransaction.TranCode = .TranCode
            mpsTransaction.InvoiceNo = .RefNo
            mpsTransaction.RefNo = .RefNo
        End With

        '*****************
        '   for testing only
        mpsAccount.AcctNo = "5499990123456781"
        mpsAccount.ExpDate = "0809"
        mpsAccount.Track2 = Nothing
        '   end testing
        '*****************


        mpsTransaction.Account = mpsAccount
        mpsTransaction.Amount = mpsAmount

        mpsTStream.Transaction = mpsTransaction


        Dim output As New StringWriter(New StringBuilder)
        Dim s As New XmlSerializer(GetType(TStream))
        s.Serialize(output, mpsTStream)

        StartReturn(output.ToString, returnPayment)

    End Sub

    Private Sub StartReturn(ByRef XMLString As String, ByRef returnPayment As DataSet_Builder.Payment)

        Dim resp As String
        Dim authStatus As String

        dsi.ServerIPConfig("x1.mercurypay.com;x2.mercurypay.com;b1.backuppay.com;b2.backuppay.com", 0)
        resp = dsi.ProcessTransaction(XMLString, 0, "", "")


        '     sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\Return.txt")
        '    sWriter1.Write(resp)
        '   sWriter1.Close()
        '  MsgBox(resp)

        '   *** not sure what we get as a response
        '   Approved ... Success    ???
        authStatus = ParseXMLResponse(resp, returnPayment)



        If authStatus = "Approved" Then     'maybe Success
            GenerateOrderTables.AddPaymentToDataRow(returnPayment, True, currentTable.ExperienceNumber, actingManager.EmployeeID, currentTable.CheckNumber, False)
            GenerateOrderTables.UpdatePaymentsAndCredits()
            tmrCardRead.Stop()
            RemoveHandler tmrCardRead.Tick, AddressOf readAuth.tmrCardRead_Tick
          
            Me.Dispose()

        End If

    End Sub

    Private Function ParseXMLResponse(ByVal resp As String, ByRef returnPayment As DataSet_Builder.Payment)

        Dim reader As New XmlTextReader(New StringReader(resp))
        Dim someError As Boolean
        Dim isPreAuth As Boolean
        Dim isReturn As Boolean
        Dim isApproved As Boolean
        Dim authStatus As String

        Try
            Do Until reader.EOF = True
                reader.Read()
                reader.MoveToContent()
                If reader.NodeType = XmlNodeType.Element Then
                    '         MsgBox(reader.Name)
                    Select Case reader.Name

                        Case "DSIXReturnCode"
                            If String.Compare(reader.ReadInnerXml, "000000", True) <> 0 Then
                                '   false, do not honor case (is not case sensitive)
                                '   a zero means the same (therefore this is not the same)
                                '   there is sometype of error
                                someError = True


                            End If
                            '               MsgBox(reader.ReadInnerXml, , "OperatorID")
                        Case "CmdStatus"
                            Select Case reader.ReadInnerXml
                                Case "Approved"
                                    '       isApproved = True
                                    '      authStatus = "Approved"

                                    MsgBox(reader.ReadInnerXml)

                                Case "Declined"
                                    MsgBox(reader.ReadInnerXml)

                                Case "Success"
                                    isApproved = True
                                    authStatus = "Approved"
                                    MsgBox(reader.ReadInnerXml)

                                Case "Error"
                                    MsgBox(reader.ReadInnerXml)

                            End Select

                        Case "TextResponse"
                            If someError = True Then
                                MsgBox(reader.ReadInnerXml)
                                Exit Function
                            Else

                            End If

                        Case "UserTraceData"

                            '                         **************************************
                            '                                 Transaction Response
                            '                         **************************************

                        Case "TranCode"
                            If String.Compare(reader.ReadInnerXml, "PreAuth", True) = 0 Then
                                isPreAuth = True
                            ElseIf String.Compare(reader.ReadInnerXml, "Return", True) = 0 Then
                                isReturn = True
                            End If

                        Case "RefNo"
                            If isPreAuth = True And isApproved = True Then
                                '  *** ? place RefNo in database
                                '      vrow("RefNo") = reader.ReadInnerXml

                            End If

                        Case "AuthCode"
                            '         If isPreAuth = True And isApproved = True Then
                            '        '   place authcode in database
                            '       vrow("AuthCode") = reader.ReadInnerXml
                            '      End If
                            If isReturn = True And isApproved = True Then
                                returnPayment.AuthCode = reader.ReadInnerXml   '*** not sure if we get response
                                MsgBox(reader.ReadInnerXml)
                            End If

                        Case "AcqRefData"
                            '   If isPreAuth = True And isApproved = True Then
                            '   '   place acqRefData in database
                            '   vrow("AcqRefData") = reader.ReadInnerXml
                            '  End If
                            If isReturn = True And isApproved = True Then
                                returnPayment.AcqRefData = reader.ReadInnerXml   '*** not sure if we get response
                            End If

                    End Select
                End If
            Loop
        Catch ex As Exception

        Finally
            If Not reader Is Nothing Then
                reader.Close()
            End If

        End Try

        Return authStatus

    End Function



    Private Sub PrintReturnReceipt()

        '   ***


    End Sub

    Private Sub lblInvoiceNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblInvoiceNum.Click
        ActivateInvoiceNumPanel()

    End Sub

    Private Sub lblReturnAmount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblReturnAmount.Click
        ActiveReturnAmountPanel()

    End Sub

    Private Sub lblAcctNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblAcctNum.Click
        If returnPayment.Swiped = False Then
            ActivateAcctNumberPanel()
        End If

    End Sub

   
End Class
