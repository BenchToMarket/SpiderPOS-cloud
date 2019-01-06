
Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing
Imports DataSet_Builder



Public Class PrintHelper

    'Left       0
    'Right      2
    'Center     6

    Dim yPos As Single = 80
    Dim leftMargin As Single = 0    'ev.MarginBounds.Left
    Dim count As Integer
    Dim lastHeight As Integer
    Dim vRow As DataRowView
    Dim midWidth As Integer = 250
    Dim pageWidth As Integer = 500

    Dim code39 As New FontInfo("Free 3 of 9 Extended", 20, 1)

    Dim pInfoA11 As New FontInfo("FontA11", 9.5, 1)
    Dim pInfoA12 As New FontInfo("FontA12", 18, 1) '("FontA12", 16, 1) '
    Dim pInfoA21 As New FontInfo("FontA21", 9.5, 1)

    Dim pInfoB12 As New FontInfo("FontB12", 13.5, 1)
    Dim pInfoB21 As New FontInfo("FontB21", 7, 1)
    Dim pInfoB22 As New FontInfo("FontB22", 13.5, 1)
    Dim pInfoB24 As New FontInfo("FontB24", 27, 1)

    Dim ControlFont As New FontInfo("control", 9.5, 1) '10, 1)

    '    Dim sql As New DataSet_Builder.SQLHelper(connectserver)
    Dim dvOrderByPrinter As DataView

    Private printFont As Font
    Private StreamToFile As StreamWriter
    Private streamToPrint As StreamReader

    Dim closeCheckWriter1 As StreamWriter
    Dim clockoutWriter1 As StreamWriter

    Dim sWriter1 As StreamWriter
    Dim sWriter2 As StreamWriter
    Dim sWriter3 As StreamWriter
    Dim sWriter4 As StreamWriter
    Dim sWriter5 As StreamWriter

    Dim allCourse1 As Boolean
    Dim lastCourseNumber As Integer = 0
    Dim currentRoutingName As String
    Dim isReprinting As Boolean
    Dim isExpediterPrinter As Boolean

    Dim LoopReprint As Int64
    Dim LoopRoutingID As Integer

    Dim s1 As Boolean
    Dim s2 As Boolean
    Dim s3 As Boolean
    Dim s4 As Boolean
    Dim s5 As Boolean

    Dim cashInfo As CashInfoStructure
    Friend oDetail As OrderDetailInfo
    Friend closeDetail As CloseDetailInfo
    Friend useVIEW As Boolean
    Private _ccDataRow As DataRow
    Private _ccDataRowView As DataRowView
    Private _clockOutJunk As ClockOutInfo

    Friend Property ccDataRow() As DataRow
        Get
            Return _ccDataRow
        End Get
        Set(ByVal Value As DataRow)
            _ccDataRow = Value
        End Set
    End Property

    Friend Property ccDataRowView() As DataRowView
        Get
            Return _ccDataRowView
        End Get
        Set(ByVal Value As DataRowView)
            _ccDataRowView = Value
        End Set
    End Property

    Friend Property ClockOutJunk() As ClockOutInfo
        Get
            Return _clockOutJunk
        End Get
        Set(ByVal Value As ClockOutInfo)
            _clockOutJunk = Value
        End Set
    End Property

    Friend Sub SendingOrder(ByVal reprint As Int64)  '(ByRef oDetail As OrderDetailInfo)

        Dim sRow As DataRow
        Dim countCourse1 As Integer
        Dim countTotalSending As Integer
      
        Me.LoopReprint = reprint

        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
            countCourse1 = (dsOrder.Tables("OpenOrders")).Compute("Count(CourseNumber)", "CourseNumber = 2 AND ItemStatus = 0") ' (FunctionFlag = 'F' or FunctionFlag = 'M')")
            countTotalSending = (dsOrder.Tables("OpenOrders")).Compute("Count(ItemStatus)", "ItemStatus = 0")
        Else
            countCourse1 = 0
            countTotalSending = 0
        End If

        If countCourse1 = countTotalSending Then
            allCourse1 = True
        End If

        If companyInfo.isKitchenExpiditer = True Then
            '   send order from dvOrderPrint
            '   must determine if sent to kitchen group
        End If

        For Each sRow In ds.Tables("RoutingChoice").Rows
            If Not sRow("RoutingName") = "Do Not Route" Then
                currentRoutingName = sRow("RoutingName")
                Me.LoopRoutingID = sRow("RoutingID")
                dvOrderByPrinter = New DataView
                If sRow("isExpediterPrinter") = True Then

                    isExpediterPrinter = True
                    With dvOrderByPrinter
                        .Table = dsOrder.Tables("OpenOrders")
                        If reprint = Nothing Or reprint = 0 Then
                            .RowFilter = "(FunctionFlag = 'F' Or FunctionFlag ='O' Or FunctionFlag = 'M') AND ItemStatus = 0"
                            '      .RowFilter = "FunctionFlag = " & sRow("RoutingID") & " AND ItemStatus = 0"
                        Else
                            .RowFilter = "(FunctionFlag = 'F' Or FunctionFlag ='O' Or FunctionFlag = 'M') AND OrderNumber = " & reprint
                            '    .RowFilter = "FunctionFlag = " & sRow("RoutingID") & " AND OrderNumber = " & reprint
                            isReprinting = True
                        End If
                        .Sort = "RoutingID, CourseNumber, CustomerNumber, sii, si2, sin" 'printpriorityID" 
                    End With

                Else
                    isExpediterPrinter = False
                    With dvOrderByPrinter
                        .Table = dsOrder.Tables("OpenOrders")
                        If reprint = Nothing Or reprint = 0 Then
                            .RowFilter = "RoutingID = " & sRow("RoutingID") & " AND ItemStatus = 0"
                        Else
                            .RowFilter = "RoutingID = " & sRow("RoutingID") & " AND OrderNumber = " & reprint
                            isReprinting = True
                        End If
                        .Sort = "CourseNumber, CustomerNumber, sii, si2, sin" 'printpriorityID" 
                    End With
                End If

                Try
                    If dvOrderByPrinter.Count > 0 Then
                        Dim pd As New PrintDocument
                        pd.PrintController = New StandardPrintController
                        AddHandler pd.PrintPage, AddressOf pd_PrintPageEPSONFix
                        pd.PrinterSettings.PrinterName = sRow("RoutingName")
                        '     pd.PrinterSettings.PrinterName = "Receipt2"
                        pd.Print()
                        pd.Dispose()

                    End If
                Catch ex As Exception
                    MsgBox("Printer " & sRow("RoutingName").ToString & " is not connected")
                End Try
            End If
        Next

    End Sub

    Private Sub pd_PrintPageEPSONFix(ByVal sender As Object, ByVal ev As PrintPageEventArgs)

        'page width is 500
        leftMargin = 0
        count = 0
        yPos = 0
        Dim quantityCount As Integer
        Dim dvSecondLoop As DataView
        Dim slRow As DataRowView

        'ticket Header
        If isReprinting = True Then
            lastHeight = DoPrinting(ev, 80, yPos, 0, pInfoB24, "Reprint")
        Else
            '      lastHeight = DoPrinting(ev, 80, yPos, 0, pInfoB24, "Testing")
            lastHeight = DoPrinting(ev, 80, yPos, 0, pInfoB24, currentTable.MethodUse)
        End If
        yPos += lastHeight

        If ds.Tables("RoutingChoice").Rows.Count > 1 Then
            lastHeight = DoPrinting(ev, leftMargin, yPos, 0, pInfoB21, currentRoutingName)
            yPos += (3 + lastHeight)
        End If
        '     lastHeight = DoPrinting(ev, leftMargin, yPos, 0, pInfoA12, "****************************")
        '    yPos += (lastHeight)
        lastHeight = DoPrinting(ev, leftMargin, yPos, 0, pInfoB21, Format(Now, "hh:mm tt"))
        yPos += (3 + lastHeight)
        If currentTable.IsTabNotTable = False Then
            lastHeight = DoPrinting(ev, leftMargin, yPos, 0, pInfoB21, "Table: " & currentTable.TabName)
            yPos += (3 + lastHeight)
            lastHeight = DoPrinting(ev, leftMargin, yPos, 0, pInfoB21, "Server: " & currentServer.NickName)
            yPos += (3 + lastHeight)
        Else
            lastHeight = DoPrinting(ev, leftMargin, yPos, 0, pInfoB21, "Tab Name: " & currentTable.TabName)
            yPos += (3 + lastHeight)
        End If
        If currentTable.NumberOfCustomers > 1 Then
            lastHeight = DoPrinting(ev, leftMargin, yPos, 0, pInfoB21, "Guests: " & currentTable.NumberOfCustomers)
            yPos += (3 + lastHeight)
        End If
        lastHeight = DoPrinting(ev, leftMargin, yPos, 0, pInfoB21, "Order: " & oDetail.trunkOrderNumber)
        yPos += (3 + lastHeight)

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA12, "---------------------------------")
        yPos += lastHeight '(20 + lastHeight)

        Dim lastCourseNumber As Integer
        Dim firstCustomer As Boolean = False
        Dim lastCustNumber As Integer
       Dim lastPrinterID As Integer = -99 'something it will never be
        Dim expRoutingName As String
        Dim eRow As DataRow

        For Each vRow In Me.dvOrderByPrinter
            Try 'i don't think try works in a print routine?

                If isExpediterPrinter = True Then
                    If lastPrinterID <> vRow("RoutingID") Then
                        'this is new routing, under expediter
                        lastPrinterID = vRow("RoutingID")
                        For Each eRow In ds.Tables("RoutingChoice").Rows
                            'this is because we don't keep routingName in OpenOrders
                            If eRow("RoutingID") = vRow("RoutingID") Then
                                expRoutingName = eRow("RoutingName")
                            End If
                        Next
                
                        yPos += 20
                        lastHeight = DoPrinting(ev, 50, yPos, 0, pInfoA12, "*** " & expRoutingName & " station ***")
                        yPos += lastHeight     '100 + (count * nHeight)

                     

                    End If
                End If

                If allCourse1 = False Then
                    If vRow("CourseNumber") <> lastCourseNumber Then
                        firstCustomer = True
                        lastCourseNumber = vRow("CourseNumber")
                        yPos += 20
                        lastHeight = DoPrinting(ev, 0, yPos, 2, pInfoB24, "Course    " & vRow("CourseNumber"))
                        yPos += lastHeight
                        lastHeight = DoPrinting(ev, 0, yPos, 2, pInfoA12, "*********************")
                        yPos += (2 * lastHeight)
                    End If
                End If

                If vRow("Quantity") > 1 And vRow("Quantity") < 5 And vRow("sin") = vRow("sii") Then
                    dvSecondLoop = New DataView
                    With dvSecondLoop
                        .Table = dsOrder.Tables("OpenOrders")
                        If Me.LoopReprint = Nothing Or Me.LoopReprint = 0 Then
                            .RowFilter = "RoutingID = " & Me.LoopRoutingID & " AND ItemStatus = 0 AND sii = " & vRow("sii")
                        Else
                            .RowFilter = "RoutingID = " & Me.LoopRoutingID & " AND OrderNumber = " & Me.LoopReprint & " AND sii = " & vRow("sii")
                            isReprinting = True
                        End If
                        .Sort = "si2, sin"
                    End With
                End If

                '         If vRow("FunctionFlag") = "F" Or vRow("FunctionFlag") = "O" Or vRow("FunctionFlag") = "M" Or vRow("FunctionFlag") = "D" Then
                If vRow("Quantity") > 1 And vRow("Quantity") < 5 Then
                    Do Until quantityCount >= vRow("Quantity")
                        For Each slRow In dvSecondLoop
                            If slRow("sin") = slRow("sii") Then
                                If firstCustomer = False Then
                                    ' just giving space
                                    yPos += (lastHeight * 0.5)
                                    If vRow("CustomerNumber") > lastCustNumber Then
                                        'this is new customer
                                        lastCourseNumber = vRow("CustomerNumber")
                                        '      yPos += (lastHeight * 0.75)
                                    End If
                                Else
                                    firstCustomer = False
                                End If
                                lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA12, "C" & slRow("CustomerNumber"))
                            End If
                            lastHeight = DoPrinting(ev, 50, yPos, 0, pInfoA12, slRow("ChitName"))             'need price too
                            yPos += lastHeight     '100 + (count * nHeight)
                        Next
                        quantityCount += 1
                    Loop
                    quantityCount = 0
                    dvSecondLoop = New DataView

                ElseIf vRow("Quantity") > 4 Then
                    lastHeight = DoPrinting(ev, 10, yPos, 0, pInfoB24, "Quantity:    " & vRow("Quantity"))
                    yPos += (lastHeight)
                    If vRow("sin") = vRow("sii") Then
                        If firstCustomer = False Then
                            ' just giving space
                            yPos += (lastHeight * 0.5)
                            If vRow("CustomerNumber") > lastCustNumber Then
                                'this is new customer
                                lastCourseNumber = vRow("CustomerNumber")
                                '    yPos += (lastHeight * 0.75)
                            End If
                        Else
                            firstCustomer = False
                        End If
                        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA12, "C" & vRow("CustomerNumber"))
                    End If
                    lastHeight = DoPrinting(ev, 50, yPos, 0, pInfoA12, vRow("ChitName"))             'need price too
                    yPos += lastHeight     '100 + (count * nHeight)
                    '           Exit Do

                Else
                    'this is single order
                    If vRow("sin") = vRow("sii") Then
                        If firstCustomer = False Then
                            ' just giving space
                            yPos += (lastHeight * 0.5)
                            If vRow("CustomerNumber") > lastCustNumber Then
                                'this is next customer
                                lastCourseNumber = vRow("CustomerNumber")
                                '*** need to test below when get printer
                                '     yPos += 40 '(lastHeight * 0.6)
                            End If
                        Else
                            firstCustomer = False
                        End If
                        'this may add height to new customer   yPos += (lastHeight * 0.3)
                        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA12, "C" & vRow("CustomerNumber"))
                    End If
                    lastHeight = DoPrinting(ev, 50, yPos, 0, pInfoA12, vRow("ChitName"))             'need price too
                    yPos += lastHeight     '100 + (count * nHeight)
                End If

                '      End If
                '       MsgBox(ev.MarginBounds.ToString, , "Margin Bounds")
                '      MsgBox(ev.PageBounds.ToString, , "Page Bounds")
                '     MsgBox(ev.PageBounds.Width.ToString, , "Page Bounds Width")
                '    MsgBox(ev.PageSettings.PaperSize.Width.ToString, , "Paper Width")
                '     count += 1
            Catch ex As Exception
                '    MsgBox("here")
                '   MsgBox("Printer " & sender.PrinterSettings.PrinterName.ToString & " is not connected")
            End Try
        Next

        'space on bottom
        yPos += (20 + lastHeight)
        '        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA12, "----------------------------------")
        '       lastHeight += 50
        '      yPos += lastHeight
        lastHeight = DoPrinting(ev, 50, yPos, 0, pInfoA12, "    ")


    End Sub

    Private Function DoPrinting(ByVal ev As System.Drawing.Printing.PrintPageEventArgs, ByVal nXPosition As Integer, _
                             ByVal yPos As Integer, ByVal tAlign As Integer, ByVal pInfo As FontInfo, ByVal pText As String)
        Dim hdc As IntPtr = New IntPtr
        Dim font As IntPtr = New IntPtr

        hdc = ev.Graphics.GetHdc()


        Dim nHeight As Integer
        nHeight = pInfo.nFontSize * UsingGDI.GetDeviceCaps(hdc, 90) / 72
      
        font = UsingGDI.CreateFont(nHeight, 0, 0, 0, 400, 0, 0, 0, _
                                   pInfo.nCharSet, 0, 0, 0, 0, pInfo.sFontName)

        '   nXPosition = (ev.PageBounds.Width)
        UsingGDI.SetTextAlign(hdc, tAlign)
        '       UsingGDI.SetTextColor( Color.Red)

        UsingGDI.SelectObject(hdc, font)
        UsingGDI.TextOut(hdc, nXPosition, yPos, pText, pText.Length)
        'we can truncate line by changing the pText.Length value
        ev.Graphics.ReleaseHdc(hdc)

        Return nHeight

    End Function

    Private Function DoPrinting2(ByVal ev As System.Drawing.Printing.PrintPageEventArgs, ByVal nXPosition As Integer, _
                                   ByVal yPos As Integer, ByVal pInfo As FontInfo, ByVal pText As String)

        Dim nHeight As Integer
        Dim g As Graphics
        g = ev.Graphics
        Dim f As New Font("FontA11", 9.5)
        Dim sf As New StringFormat
        Dim r As New Rectangle(10, 10, 80, 80)
        sf.Alignment = StringAlignment.Center
        nHeight = f.Height

        g.DrawString(pText, f, Brushes.Black, nXPosition, yPos, sf)

        Return nHeight

    End Function

    Friend Sub StartPrintCreditCardVoid()

        yPos = 80
        Dim pd As New PrintDocument

        Try
            pd.PrintController = New StandardPrintController
            AddHandler pd.PrintPage, AddressOf pd_PrintPageCreditCardVoid

            '     pd.PrinterSettings.PrinterName = "Receipt"
            pd.PrinterSettings.PrinterName = currentTerminal.ReceiptName '"Receipt" & currentTerminal.TermID
            pd.Print()
            pd.Dispose()
        Catch ex As Exception
            MsgBox("Printer " & pd.PrinterSettings.PrinterName & " is not connected")
        End Try

    End Sub

    Friend Sub StartPrintCreditCardRest()

        yPos = 80
        Dim pd As New PrintDocument

        Try
            pd.PrintController = New StandardPrintController
            AddHandler pd.PrintPage, AddressOf pd_PrintPageCreditCardRest

            '     pd.PrinterSettings.PrinterName = "Receipt"
            pd.PrinterSettings.PrinterName = currentTerminal.ReceiptName '"Receipt" & currentTerminal.TermID
            pd.Print()
            pd.Dispose()
        Catch ex As Exception
            MsgBox("Printer " & pd.PrinterSettings.PrinterName & " is not connected")
        End Try

    End Sub

    Friend Sub StartPrintCreditCardGuest()

        yPos = 80
        Dim pd As New PrintDocument

        Try
            pd.PrintController = New StandardPrintController
            AddHandler pd.PrintPage, AddressOf pd_PrintPageCreditCardGuest

            '     pd.PrinterSettings.PrinterName = "Receipt"
            pd.PrinterSettings.PrinterName = currentTerminal.ReceiptName     '"Receipt" & currentTerminal.TermID
            pd.Print()
            pd.Dispose()
        Catch ex As Exception
            MsgBox("Printer " & pd.PrinterSettings.PrinterName & " is not connected")
        End Try


    End Sub

    Private Sub pd_PrintPageCreditCardVoid(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Dim invNumber As String

        '    DetermineTruncatedExperienceNumber()
        '   yPos = CreateCheckHeader(ev)

        If useVIEW = True Then
            invNumber = ccDataRowView("RefNum")
            yPos = CreateCheckHeader(ev, invNumber)
            FillCreditCardDetailAfterVoid(ev, yPos, False)
            '       Else
            '          invNumber = ccDataRow("RefNum")
            '         yPos = CreateCheckHeader(ev, invNumber)
            '        FillCreditCardDetail(ev, yPos, True)    'true is restaurant copy
        End If

    End Sub

    Private Sub pd_PrintPageCreditCardRest(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Dim invNumber As String

        '    DetermineTruncatedExperienceNumber()
        '   yPos = CreateCheckHeader(ev)

        If useVIEW = True Then
            invNumber = ccDataRowView("RefNum")
            yPos = CreateCheckHeader(ev, invNumber)
            FillCreditCardDetailView(ev, yPos, True)
        Else
            invNumber = ccDataRow("RefNum")
            yPos = CreateCheckHeader(ev, invNumber)
            FillCreditCardDetail(ev, yPos, True)    'true is restaurant copy
        End If

    End Sub

    Private Sub pd_PrintPageCreditCardGuest(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Dim invNumber As String

        '    DetermineTruncatedExperienceNumber()
        '    yPos = CreateCheckHeader(ev)

        If useVIEW = True Then
            invNumber = ccDataRowView("RefNum")
            yPos = CreateCheckHeader(ev, invNumber)
            FillCreditCardDetailView(ev, yPos, False)
        Else
            invNumber = ccDataRow("RefNum")
            yPos = CreateCheckHeader(ev, invNumber)
            FillCreditCardDetail(ev, yPos, False)
        End If

    End Sub

    Private Function FillCreditCardDetail(ByVal ev As System.Drawing.Printing.PrintPageEventArgs, ByVal yPos As Single, ByVal isRestCopy As Boolean) 'ByVal copyString As String)

        Dim leftMargin As Single = 0
        Dim count As Integer
        Dim lastHeight As Integer
        Dim vRow As DataRowView
        Dim pInfo2 As New FontInfo("FontB12", 13.5, 1) '("FontA11", 9.5, 1) '("FontB48", 15, 1) '("FontB42", 13.5, 1)
        Dim pInfo As New FontInfo("FontA11", 9.5, 1)
        Dim pInfo3 As New FontInfo("FontB21", 7, 1)
        Dim pageWidth As Integer
        Dim midWidth As Integer
        Dim runingPaymentTotal As Decimal
        Dim ccName As String

        pageWidth = 500
        midWidth = 250

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, ccDataRow("PaymentTypeName"))
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(currentTable.SatTime, "M/d/yyyy"))
        'using sat time b/c if we reprint this ticket another day we want to date it by open time
        yPos += (lastHeight)

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Card # " & TruncateAccountNumber(ccDataRow("AccountNumber")))
        If isRestCopy = True Then
            '           lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Card # " & ccDataRow("AccountNumber"))
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "Exp:" & ccDataRow("CCExpiration"))
        End If


        yPos += (lastHeight)
        If Not ccDataRow("CustomerName") Is DBNull.Value Then
            ccName = ccDataRow("CustomerName")
        Else
            ccName = "-none-"
        End If
        If ccDataRow("SwipeType") = 1 Then
            lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Magnetic card present: " & ccName)
        Else
            lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "No card present: " & ccName)
        End If

        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Approval: " & ccDataRow("AuthCode"))
        yPos += (60 + lastHeight)



        lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, "Amount:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(ccDataRow("PaymentAmount"), "##,###.00"))


        If ccDataRow("Surcharge") > 0 Then
            yPos += (lastHeight + 15)
            '        lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, "Auto Gratuity (" & Format(companyInfo.autoGratuityPercent, "### %") & ")")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(ccDataRow("Surcharge"), "##,###.00"))
        End If

        If ccDataRow("BatchCleared") = True Or ccDataRow("Tip") > 0 Then    'Is Nothing
            yPos += (lastHeight + 15)
            If currentTable.AutoGratuity = True Then
                lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, "Auto Gratuity (" & Format(companyInfo.autoGratuityPercent, "### %") & ")")
            Else
                lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, "   + Tip:")
            End If
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(ccDataRow("Tip"), "##,###.00"))
            yPos += (lastHeight + 15)
            lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, " = Total:")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(ccDataRow("PaymentAmount") + ccDataRow("Tip") + ccDataRow("Surcharge"), "##,###.00"))
        Else
            yPos += (lastHeight + 40)
            lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, "   + Tip:")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "____________")
            yPos += (lastHeight + 40)
            lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, " = Total:")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "____________")
        End If


        yPos += (60 + lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "X________________________________________")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Approval: " & ccDataRow("AuthCode"))
        If isRestCopy = True Then
            yPos += (30 + lastHeight)
            lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "    I agree to pay above total amount")
            yPos += (lastHeight)
            lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "    according to card issuer agreement.")
        End If
        yPos += (100 + lastHeight)

        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "Thank You")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "Please Come Again")
        yPos += (40 + lastHeight)

        '       If Not companyInfo.receiptMessage1 Is Nothing Then
        '       lastHeight = DoPrinting(ev, midWidth, yPos, 6, pinfoB21, companyInfo.receiptMessage1)
        '       yPos += (40 + lastHeight)
        '       End If
        '      If Not companyInfo.receiptMessage2 Is Nothing Then
        ''      lastHeight = DoPrinting(ev, midWidth, yPos, 6, pinfoB21, companyInfo.receiptMessage2)
        '      yPos += (40 + lastHeight)
        '      End If
        '      If Not companyInfo.receiptMessage3 Is Nothing Then
        '     lastHeight = DoPrinting(ev, midWidth, yPos, 6, pinfoB21, companyInfo.receiptMessage3)
        ''     yPos += (40 + lastHeight)
        '     End If

        If isRestCopy = True Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "*****Restaurant's Copy*****")
        Else
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "*****Guest's Copy*****")
        End If

    End Function

    Private Function FillCreditCardDetailAfterVoid(ByVal ev As System.Drawing.Printing.PrintPageEventArgs, ByVal yPos As Single, ByVal isRestCopy As Boolean) 'ByVal copyString As String)

        Dim leftMargin As Single = 0
        Dim count As Integer
        Dim lastHeight As Integer
        Dim vRow As DataRowView
        Dim pInfo2 As New FontInfo("FontB12", 13.5, 1) '("FontA11", 9.5, 1) '("FontB48", 15, 1) '("FontB42", 13.5, 1)
        Dim pInfo As New FontInfo("FontA11", 9.5, 1)
        Dim pInfo3 As New FontInfo("FontB21", 7, 1)
        Dim pageWidth As Integer
        Dim midWidth As Integer
        Dim runingPaymentTotal As Decimal
        Dim ccName As String

        pageWidth = 500
        midWidth = 250

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, ccDataRowView("PaymentTypeName"))
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(currentTable.SatTime, "M/d/yyyy"))
        'using sat time b/c if we reprint this ticket another day we want to date it by open time
        yPos += (lastHeight)

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Card # " & TruncateAccountNumber(ccDataRowView("AccountNumber")))
        If isRestCopy = True Then
            '     lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Card # " & ccDataRowView("AccountNumber"))
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "Exp:" & ccDataRowView("CCExpiration"))
        End If

        yPos += (lastHeight)
        If Not ccDataRowView("CustomerName") Is DBNull.Value Then
            ccName = ccDataRowView("CustomerName")
        Else
            ccName = "-none-"
        End If
        If ccDataRowView("SwipeType") = 1 Then
            lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Magnetic card present: " & ccName)
        Else
            lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "No card present: " & ccName)
        End If

        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Voided: " & ccDataRowView("AuthCode"))
        yPos += (60 + lastHeight)

        runingPaymentTotal = ccDataRowView("PaymentAmount") + ccDataRowView("Surcharge")

        lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, "Voided Amount:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(runingPaymentTotal, "##,###.00"))

        yPos += (100 + lastHeight)

        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "Thank You")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "Keep for your records.")
        yPos += (40 + lastHeight)

        If isRestCopy = True Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "*****Restaurant's Copy*****")
        Else
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "*****Guest's Copy*****")
        End If

    End Function

    Private Function FillCreditCardDetailView(ByVal ev As System.Drawing.Printing.PrintPageEventArgs, ByVal yPos As Single, ByVal isRestCopy As Boolean) 'ByVal copyString As String)

        Dim leftMargin As Single = 0
        Dim count As Integer
        Dim lastHeight As Integer
        Dim vRow As DataRowView
        Dim pInfo2 As New FontInfo("FontB12", 13.5, 1) '("FontA11", 9.5, 1) '("FontB48", 15, 1) '("FontB42", 13.5, 1)
        Dim pInfo As New FontInfo("FontA11", 9.5, 1)
        Dim pInfo3 As New FontInfo("FontB21", 7, 1)
        Dim pageWidth As Integer
        Dim midWidth As Integer
        Dim runingPaymentTotal As Decimal
        Dim ccName As String

        pageWidth = 500
        midWidth = 250

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, ccDataRowView("PaymentTypeName"))
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(currentTable.SatTime, "M/d/yyyy"))
        'using sat time b/c if we reprint this ticket another day we want to date it by open time
        yPos += (lastHeight)

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Card # " & TruncateAccountNumber(ccDataRowView("AccountNumber")))
        If isRestCopy = True Then
            '     lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Card # " & ccDataRowView("AccountNumber"))
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "Exp:" & ccDataRowView("CCExpiration"))
        End If

        yPos += (lastHeight)
        If Not ccDataRowView("CustomerName") Is DBNull.Value Then
            ccName = ccDataRowView("CustomerName")
        Else
            ccName = "-none-"
        End If
        If ccDataRowView("SwipeType") = 1 Then
            lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Magnetic card present: " & ccName)
        Else
            lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "No card present: " & ccName)
        End If

        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Approval: " & ccDataRowView("AuthCode"))
        yPos += (60 + lastHeight)



        lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, "Amount:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(ccDataRowView("PaymentAmount"), "##,###.00"))


        If ccDataRowView("Surcharge") > 0 Then
            yPos += (lastHeight + 15)
            '     lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, "Auto Gratuity (" & Format(companyInfo.autoGratuityPercent, "### %") & ")")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(ccDataRowView("Surcharge"), "##,###.00"))
        End If

        If ccDataRowView("BatchCleared") = True Or ccDataRowView("Tip") > 0 Then    'Is Nothing
            yPos += (lastHeight + 15)
            If currentTable.AutoGratuity = True Then
                lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, "Auto Gratuity (" & Format(companyInfo.autoGratuityPercent, "### %") & ")")
            Else
                lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, "   + Tip:")
            End If
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(ccDataRowView("Tip"), "##,###.00"))
            yPos += (lastHeight + 15)
            lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, " = Total:")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(ccDataRowView("PaymentAmount") + ccDataRowView("Tip") + ccDataRowView("Surcharge"), "##,###.00"))
        Else
            yPos += (lastHeight + 40)
            lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, "   + Tip:")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "____________")
            yPos += (lastHeight + 40)
            lastHeight = DoPrinting(ev, 350, yPos, 2, pInfoA11, " = Total:")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "____________")
        End If


        yPos += (60 + lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "X________________________________________")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Approval: " & ccDataRowView("AuthCode"))
        If isRestCopy = True Then
            yPos += (30 + lastHeight)
            lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "    I agree to pay above total amount")
            yPos += (lastHeight)
            lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "    according to card issuer agreement.")
        End If
        yPos += (100 + lastHeight)

        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "Thank You")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "Please Come Again")
        yPos += (40 + lastHeight)

        '       If Not companyInfo.receiptMessage1 Is Nothing Then
        '       lastHeight = DoPrinting(ev, midWidth, yPos, 6, pinfoB21, companyInfo.receiptMessage1)
        '       yPos += (40 + lastHeight)
        '       End If
        '      If Not companyInfo.receiptMessage2 Is Nothing Then
        ''      lastHeight = DoPrinting(ev, midWidth, yPos, 6, pinfoB21, companyInfo.receiptMessage2)
        '      yPos += (40 + lastHeight)
        '      End If
        '      If Not companyInfo.receiptMessage3 Is Nothing Then
        '     lastHeight = DoPrinting(ev, midWidth, yPos, 6, pinfoB21, companyInfo.receiptMessage3)
        ''     yPos += (40 + lastHeight)
        '     End If

        If isRestCopy = True Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "*****Restaurant's Copy*****")
        Else
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "*****Guest's Copy*****")
        End If

    End Function


    Private Function CreateCreditCardHeader222(ByVal ev As System.Drawing.Printing.PrintPageEventArgs)

    End Function

    Friend Sub StartPrintCheckReceipt() 'ByRef dvClosing As DataView, ByVal chkSubTotal As Decimal, ByVal checkTax As Decimal)

        Dim pd As New PrintDocument

        Try
            pd.PrintController = New StandardPrintController
            AddHandler pd.PrintPage, AddressOf pd_PrintPageCloseCheck
            pd.PrinterSettings.PrinterName = currentTerminal.ReceiptName     '"Receipt" & currentTerminal.TermID     '  number by terminal
            '      pd.PrinterSettings.PrinterName = "Receipt4"
            '  pd.PrinterSettings.PrinterName = "Auto HP LaserJet 4 Plus on ALURI-OKAZR9XB8"
            pd.Print()
            pd.Dispose()
        Catch ex As Exception
            '        MsgBox(ex.Message)
            MsgBox("Printer " & pd.PrinterSettings.PrinterName & " is not connected")

        End Try

    End Sub

    Friend Sub StartPrintCreditReturn() 'ByRef dvClosing As DataView, ByVal chkSubTotal As Decimal, ByVal checkTax As Decimal)

        Dim pd As New PrintDocument

        Try
            pd.PrintController = New StandardPrintController
            AddHandler pd.PrintPage, AddressOf pd_PrintPageCreditReturn
            pd.PrinterSettings.PrinterName = currentTerminal.ReceiptName     '"Receipt" & currentTerminal.TermID     '  number by terminal
            pd.Print()
            pd.Dispose()
        Catch ex As Exception
            MsgBox("Printer " & pd.PrinterSettings.PrinterName & " is not connected")

        End Try

    End Sub

    Private Sub pd_PrintPageCloseCheck(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        leftMargin = 0
        count = 0
        yPos = 80
        Dim invNumber As String


        invNumber = DetermineTruncatedExperienceNumberFunction(dvOrder(0)("ExperienceNumber"))

        yPos = CreateCheckHeader(ev, invNumber)

        FillCheckDetail(ev, yPos, closeDetail.dvClosing, closeDetail.chkSubTotal, closeDetail.chktaxTotal, closeDetail.chktaxName)

        '   if..then.... FillCheckPayemtns

    End Sub
    Private Sub pd_PrintPageCreditReturn(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        leftMargin = 0
        count = 0
        yPos = 80
        Dim invNumber As String

        '   invNumber = DetermineTruncatedExperienceNumberFunction(dvOrder(0)("ExperienceNumber"))

        yPos = CreateCheckHeaderReturn(ev, invNumber) 'invNumber nothing now

   '     FillCheckDetailReturn(ev, yPos, closeDetail.dvClosing, closeDetail.chkSubTotal, closeDetail.chktaxTotal, closeDetail.chktaxName)

        '   if..then.... FillCheckPayemtns

    End Sub


    Private Function CreateCheckHeaderReturn(ByVal ev As System.Drawing.Printing.PrintPageEventArgs, ByVal invoiceNumber As String)

        If Not companyInfo.companyName Is Nothing Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB22, companyInfo.companyName)
            yPos += (lastHeight)
        End If
        If Not companyInfo.address1 Is Nothing Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoA11, companyInfo.address1)
            yPos += (lastHeight)
        End If
        '    lastHeight = DoPrinting(ev, midWidth, yPos, 6, pinfoA11, companyInfo.address2)
        '   yPos += (lastHeight)
        If Not companyInfo.locationCity = Nothing Or Not companyInfo.locationState = Nothing Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoA11, companyInfo.locationCity & ",  " & companyInfo.locationState)
            yPos += (lastHeight)
        End If
        If Not companyInfo.locationPhone Is Nothing Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoA11, companyInfo.locationPhone)
            yPos += (lastHeight)
        End If
        yPos += (60)

    End Function

    Private Function CreateCheckHeader(ByVal ev As System.Drawing.Printing.PrintPageEventArgs, ByVal invoiceNumber As String)

        If Not companyInfo.companyName Is Nothing Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB22, companyInfo.companyName)
            yPos += (lastHeight)
        End If
        If Not companyInfo.address1 Is Nothing Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoA11, companyInfo.address1)
            yPos += (lastHeight)
        End If
        '    lastHeight = DoPrinting(ev, midWidth, yPos, 6, pinfoA11, companyInfo.address2)
        '   yPos += (lastHeight)
        If Not companyInfo.locationCity = Nothing Or Not companyInfo.locationState = Nothing Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoA11, companyInfo.locationCity & ",  " & companyInfo.locationState)
            yPos += (lastHeight)
        End If
        If Not companyInfo.locationPhone Is Nothing Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoA11, companyInfo.locationPhone)
            yPos += (lastHeight)
        End If
        yPos += (60)

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Server: " & currentServer.NickName)
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(currentTable.SatTime, "M/d/yyyy"))
        'using sat time b/c if we reprint this ticket another day we want to date it by open time
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Table: " & currentTable.TabName)
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(Now, "h:mm tt")) 'Format(currentTable.SatTime, "h:mm tt"))
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Check: " & currentTable.CheckNumber & " of " & currentTable.NumberOfChecks)
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Guests: " & currentTable.NumberOfCustomers)
        If invoiceNumber = Nothing Then
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "# " & currentTable.TruncatedExpNum)
        Else
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "# " & invoiceNumber)
        End If

        yPos += (60 + lastHeight)

        Return yPos

    End Function

    Private Sub FillCheckDetail(ByVal ev As System.Drawing.Printing.PrintPageEventArgs, ByVal yPos As Single, ByRef dvClosing As DataView, ByVal chkSubTotal As Decimal, ByVal checkTax() As Decimal, ByVal checkName() As String)

      
        Dim runningTotal As Decimal
        Dim index As Integer
        Dim oRow As DataRow

        For Each vRow In dvClosing
            If Not vRow("ItemID") = 0 Then
      
                If vRow("sin") = vRow("sii") And vRow("Quantity") > 1 Then
                    lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, vRow("Quantity"))
                End If

                lastHeight = DoPrinting(ev, 40, yPos, 0, pInfoA11, vRow("ItemName"))
                If vRow("sin") = vRow("sii") Or vRow("Price") <> 0 Then
                    lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(vRow("Price"), "##,###.00"))
                End If
                yPos += (lastHeight)
            End If
        Next

        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, midWidth - 75, yPos, 0, pInfoA11, "SubTotal:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(chkSubTotal, "##,###.00"))
        yPos += (lastHeight)
        runningTotal = chkSubTotal


        lastHeight = DoPrinting(ev, midWidth - 75, yPos, 0, pInfoA11, checkName(0)) ')"Tax:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(checkTax(0), "##,###.00"))
        yPos += (lastHeight)
        runningTotal = runningTotal + checkTax(0)

        index = 1
        For Each oRow In ds.Tables("Tax").Rows
            If checkTax(index) > 0 Then
                lastHeight = DoPrinting(ev, midWidth - 75, yPos, 0, pInfoA11, checkName(index) & " Tax:")
                lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(checkTax(index), "##,###.00"))
                yPos += (lastHeight)
                runningTotal = runningTotal + checkTax(index)
            End If
            index += 1
        Next

        If currentTable.AutoGratuity > 0 Then
            Dim adjTip As Decimal = Format(runningTotal * companyInfo.autoGratuityPercent, "##,###.00")
            Dim adjPercent As Decimal = Format((companyInfo.autoGratuityPercent * 100), "##0.##")
            lastHeight = DoPrinting(ev, midWidth - 75, yPos, 0, pInfoA11, "Auto Gratuity (" & adjPercent & "%) ")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(adjTip, "##,###.00"))
            yPos += (lastHeight)
            runningTotal = runningTotal + adjTip
        End If

        lastHeight = DoPrinting(ev, midWidth - 75, yPos, 0, pInfoB12, "TOTAL:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoB12, Format(runningTotal, "##,###.00"))
        yPos += (3 * lastHeight)
     

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("CheckNumber") = currentTable.CheckNumber Then
                    If oRow("Applied") = True Then
                        If Not oRow("PaymentFlag") = "Cash" Then
                            lastHeight = DoPrinting(ev, midWidth - 75, yPos, 0, pInfoA11, oRow("PaymentTypeName"))
                            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, (oRow("PaymentAmount") * -1))
                            yPos += (lastHeight)
                            runningTotal -= (oRow("PaymentAmount"))
                        End If
                    End If
                    End If

                End If
        Next

        runningTotal -= closeDetail.cashTendered
        runningTotal -= Me.closeDetail.cashAppliedPrevious
      
        If closeDetail.cashAppliedPrevious > 0 Then
            lastHeight = DoPrinting(ev, midWidth - 75, yPos, 0, pInfoA11, "Cash Previous:")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format((closeDetail.cashAppliedPrevious * -1), "##,###.00"))
            yPos += (lastHeight)
        End If
        If closeDetail.cashTendered > 0 Then
            lastHeight = DoPrinting(ev, midWidth - 75, yPos, 0, pInfoA11, "Cash Tendered:")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format((closeDetail.cashTendered * -1), "##,###.00"))
            yPos += (lastHeight)
        End If

        If runningTotal < 0 Then
            lastHeight = DoPrinting(ev, midWidth - 75, yPos, 0, pInfoB12, "Change:")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoB12, Format((runningTotal * -1), "##,###.00"))
        Else
            lastHeight = DoPrinting(ev, midWidth - 75, yPos, 0, pInfoB12, "Balance Due:")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoB12, Format(runningTotal, "##,###.00"))
        End If
      
        yPos += (100 + lastHeight)

        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB22, "Thank You")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB22, "Please Come Again")
        yPos += (140 + lastHeight)

        '       If Not companyInfo.receiptMessage1 Is Nothing Then
        '       lastHeight = DoPrinting(ev, midWidth, yPos, 6, pinfoB21, companyInfo.receiptMessage1)
        '       yPos += (40 + lastHeight)
        '       End If
        '      If Not companyInfo.receiptMessage2 Is Nothing Then
        ''      lastHeight = DoPrinting(ev, midWidth, yPos, 6, pinfoB21, companyInfo.receiptMessage2)
        '      yPos += (40 + lastHeight)
        '      End If
        '      If Not companyInfo.receiptMessage3 Is Nothing Then
        '     lastHeight = DoPrinting(ev, midWidth, yPos, 6, pinfoB21, companyInfo.receiptMessage3)
        ''     yPos += (40 + lastHeight)
        '     End If

        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoA11, "   ")


    End Sub

    Friend Sub StartClockOutPrint()

        Dim pd As New PrintDocument

        Try
            pd.PrintController = New StandardPrintController
            AddHandler pd.PrintPage, AddressOf pd_PrintPageClockOut
            pd.PrinterSettings.PrinterName = currentTerminal.ReceiptName     '"Receipt"                   '  number by terminal
            pd.Print()

            If dsEmployee.Tables("ClockOutSales").Rows.Count > 0 Then
                AddHandler pd.PrintPage, AddressOf pd_PrintPageClockOutSales
                pd.PrinterSettings.PrinterName = currentTerminal.ReceiptName     '"Receipt"                   '  number by terminal
                pd.Print()
                pd.Dispose()
            End If

        Catch ex As Exception
            MsgBox("Printer " & pd.PrinterSettings.PrinterName & " is not connected")
        End Try

    End Sub

    Private Sub pd_PrintPageClockOut(ByVal sender As Object, ByVal ev As PrintPageEventArgs)

        leftMargin = 0
        count = 0
        yPos = 40

        CreateClockoutHeader(ev)
        PrintClockOut(ev)
        PrintClockOutFooter(ev)

    End Sub

    Private Sub pd_PrintPageClockOutSales(ByVal sender As Object, ByVal ev As PrintPageEventArgs)

        '      leftMargin = 0
        '     count = 0
        '    yPos = 40
        yPos += (lastHeight + 50)

        CreateClockoutHeader(ev)
        PrintSalesAndPayments(ev)
        PrintClockOutFooterSales(ev)

    End Sub

    Private Sub CreateClockoutHeader(ByVal ev As System.Drawing.Printing.PrintPageEventArgs)

        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB22, "EMPLOYEE CLOCK OUT")
        yPos += (lastHeight + 30)
        If Not companyInfo.companyName Is Nothing Then
            lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, companyInfo.companyName)
        End If
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(Now, "M/d/yyyy"))
        'using sat time b/c if we reprint this ticket another day we want to date it by open time
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Employee: " & currentClockEmp.FullName)
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Format(Now, "h:mm tt"))
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Job: " & currentClockEmp.JobCodeName)
        yPos += (lastHeight + 30)

    End Sub

    Private Sub PrintClockOut(ByVal ev As System.Drawing.Printing.PrintPageEventArgs)

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Shift Date:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(ClockOutJunk.TimeIn, "M/d/yyyy"))
        'using sat time b/c if we reprint this ticket another day we want to date it by open time
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Time in: ")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(currentClockEmp.LogInTime, "h:mm tt"))
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Time out: ")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(ClockOutJunk.TimeOut, "h:mm tt"))
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Hours this shift:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, (ClockOutJunk.ShiftHours & ":" & ClockOutJunk.ShiftMins))
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Hours this week:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, (ClockOutJunk.WeekHours & ":" & ClockOutJunk.WeekMins))
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Tipable Sales:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(ClockOutJunk.TipableSales, "###,###.00"))
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Charge Sales:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(ClockOutJunk.ChargedSales, "###,###.00"))
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Charge Tips:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(ClockOutJunk.ChargedTips, "###,###.00"))
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Declared Tips:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(ClockOutJunk.DeclaredTips, "###,###.00"))


    End Sub

    Private Sub PrintClockOutFooter(ByVal ev As System.Drawing.Printing.PrintPageEventArgs)

        yPos += (lastHeight + 30)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB21, "**********************")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB21, "Verification of hours worked")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB21, "Keep a copy for your records")
        yPos += (lastHeight + 30)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB21, "    ")

    End Sub


    Private Sub PrintClockOutFooterSales(ByVal ev As System.Drawing.Printing.PrintPageEventArgs)

        yPos += (lastHeight + 30)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB21, "**********************")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB21, "Cash out Totals")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB21, "Present to Manager")
        yPos += (lastHeight + 30)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB21, "    ")

    End Sub

    Private Sub PrintSalesAndPayments(ByVal ev As System.Drawing.Printing.PrintPageEventArgs)
        Dim oRow As DataRow
        Dim x As Single = buttonSpace
        Dim y As Single
        Dim aTaxName As String


        Dim totalSales As Decimal = 0
        Dim totalTaxes As Decimal = 0
        Dim grossSales As Decimal = 0
        Dim totalPayments As Decimal = 0
        Dim cashPayments As Decimal = 0
        Dim chargePayments As Decimal = 0
        '       Dim chargeTips As Decimal = 0
        Dim lessChargeTips As Decimal = 0

        yPos += (20 + lastHeight)
        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "*** SALES ***")
        yPos += (20 + lastHeight)

        For Each oRow In dsEmployee.Tables("ClockOutSales").Rows
            If oRow("FunctionGroupSales") <> 0 Then
                lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, oRow("FunctionName"))
                lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, oRow("FunctionGroupSales"))
                totalSales += oRow("FunctionGroupSales")
                yPos += (lastHeight)

            End If
        Next
        yPos += 20
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Total:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, totalSales)
        yPos += (40 + lastHeight)


        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "*** TAXES ***")
        yPos += (20 + lastHeight)

        For Each oRow In dsEmployee.Tables("ClockOutTaxes").Rows
            If oRow("GroupTaxes") <> 0 Then
                aTaxName = DetermineTaxName(oRow("TaxID"))
                lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, aTaxName)
                lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(oRow("GroupTaxes"), "###,###.00"))
                totalTaxes += oRow("GroupTaxes")
                yPos += (lastHeight)
            End If
        Next
        yPos += 20
        grossSales = totalSales + totalTaxes
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Total:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(totalTaxes, "###,###.00"))
        yPos += (20 + lastHeight)

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Gross Sales:")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(grossSales, "###,###.00"))
        yPos += (20 + lastHeight)

        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "*** PAYMENTS ***")
        yPos += (40 + lastHeight)


        For Each oRow In dsEmployee.Tables("ClockOutPayments").Rows
            If oRow("GroupPayments") <> 0 Then
                lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, oRow("PaymentTypeName"))
                lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(oRow("GroupPayments"), "###,###.00"))
                totalPayments += oRow("GroupPayments")
                If oRow("PaymentFlag") = "Cash" Then
                    cashPayments += oRow("GroupPayments")
                    '      ElseIf oRow("PaymentFlag") = "cc" Then
                    '         chargePayments += oRow("GroupPayments")
                    '        chargeTips += oRow("GroupTips")
                End If
                yPos += (lastHeight)
            End If
        Next
        yPos += 20

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Total Payments")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(totalPayments, "###,###.00"))
        yPos += (40 + lastHeight)

        lessChargeTips = -1 * ClockOutJunk.ChargedTips

        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "*** CASH OWED ***")
        yPos += (20 + lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "Cash Payments")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(cashPayments, "###,###.00"))
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "CC Tips")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format(lessChargeTips, "###,###.00"))
        yPos += (20 + lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoA11, "TOTAL CASH OWED")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA21, Format((cashPayments + lessChargeTips), "$ ###,###.00"))
        yPos += (20 + lastHeight)

    End Sub

    Public Sub PrintAllAuthDuringBatch()
        yPos = 80
        Dim pd As New PrintDocument

        Try
            pd.PrintController = New StandardPrintController
            AddHandler pd.PrintPage, AddressOf pd_PrintAllAuth

            '     pd.PrinterSettings.PrinterName = "Receipt"
            pd.PrinterSettings.PrinterName = currentTerminal.ReceiptName  '"Receipt" & currentTerminal.TermID
            pd.Print()
            pd.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub

    Public Sub PrintOpenCashDrawer()

        If companyInfo.locationUsername = "ssikona" Then
            Exit Sub
        End If

        Dim pd As New PrintDocument

        Try
            pd.PrintController = New StandardPrintController
            AddHandler pd.PrintPage, AddressOf pd_PrintNothing

            pd.PrinterSettings.PrinterName = currentTerminal.ReceiptName
            pd.PrinterSettings.PrinterName = "Bar" 'for testing only
            pd.Print()
            pd.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        'A: Open drawer 1 at 50ms.
        'B: Open drawer 1 at 100ms.
        'C: Open drawer 1 at 150ms.
        'D: Open drawer 1 at 200ms.
        'E: Open drawer 1 at 250ms.
        'a: Open drawer 2 at 50ms.
        'b: Open drawer 2 at 100ms.
        'c: Open drawer 2 at 150ms.
        'd: Open drawer 2 at 200ms.
        'e: Open drawer 2 at 250ms.

    End Sub
    Public Sub open_cashdrawer222()
        'does not work
        Dim intFileNo As Integer = FreeFile()
        FileOpen(1, "c:\escapes2.txt", OpenMode.Output)
        PrintLine(1, "Hello World")
        PrintLine(1, Chr(27) & Chr(112) & Chr(0) & Chr(25) & Chr(250))
        FileClose(1)
        Shell("print /d:lpt1 c:\escapes.txt", vbNormalFocus)
    End Sub

    Private Sub pd_PrintNothing(ByVal sender As Object, ByVal ev As PrintPageEventArgs)

        Dim oRow As DataRow
        Dim isApproved As String
        Dim SwipeType As String
        Dim tempExpNum As String
        Dim trunkExpNum As String
        Dim tempAcctNum As String
        Dim trunkAcctNum As String
        '     Dim f As New Font("control", 10)
        '    Dim writtenData As String = Chr(27) & Chr(112) & Chr(0) & Chr(25) & Chr(250)
        'page width is 500
        '      leftMargin = 0
        '     yPos = 1

        Try
            '      ev.Graphics.DrawString("A", f, Brushes.Black, 0, 0, New StringFormat)

            '      DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "Test")
            DoPrinting(ev, 0, 0, 0, ControlFont, "A")
            '     DoPrinting(ev, 0, 0, 0, ControlFont, "F")
            '    DoPrinting(ev, 0, 0, 0, ControlFont, "P") 'cuts paper
            '    DoPrinting(ev, 0, 0, 0, ControlFont, "F")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub pd_PrintAllAuth(ByVal sender As Object, ByVal ev As PrintPageEventArgs)

        Dim oRow As DataRow
        Dim isApproved As String
        Dim SwipeType As String
        Dim tempExpNum As String
        Dim trunkExpNum As String
        Dim tempAcctNum As String
        Dim trunkAcctNum As String


        'page width is 500
        leftMargin = 0
        yPos = 80

        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "Go to www.MercuryPay.com")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB21, "(800) 846 - 4472")
        yPos += (lastHeight + 60)

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            Try
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("AuthCode") Is DBNull.Value Then
                        isApproved = "Declined"
                    Else
                        isApproved = "Approved"
                    End If

                    If oRow("SwipeType") = 1 Then
                        SwipeType = "Swiped"
                    Else
                        SwipeType = "Manual"
                    End If

                    '              tempExpNum = oRow("ExperienceNumber").ToString
                    '             If tempExpNum.Length > 6 Then
                    '            trunkExpNum = tempExpNum.Substring(tempExpNum.Length - 6, 6)
                    '       Else
                    '          trunkExpNum = tempExpNum
                    '     End If
                    If Not oRow("AccountNumber").Substring(0, 4) = "xxxx" And Not oRow("AccountNumber") = "Manual" Then
                        tempAcctNum = TruncateAccountNumber(oRow("AccountNumber"))
                    Else
                        tempAcctNum = (oRow("AccountNumber"))
                    End If

                    If tempAcctNum.Length > 6 Then
                        trunkAcctNum = tempAcctNum.Substring(tempAcctNum.Length - 6, 6)
                    Else
                        trunkAcctNum = tempAcctNum
                    End If

                    lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, oRow("PaymentDate"))
                    yPos += (lastHeight + 3)
                    lastHeight = DoPrinting(ev, 100, yPos, 2, pInfoA11, oRow("TransactionCode"))
                    lastHeight = DoPrinting(ev, 230, yPos, 2, pInfoA11, trunkAcctNum)
                    lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, SwipeType)
                    yPos += (lastHeight + 3)
                    lastHeight = DoPrinting(ev, 100, yPos, 2, pInfoA11, isApproved)
                    lastHeight = DoPrinting(ev, 230, yPos, 2, pInfoA11, Format((oRow("PaymentAmount") + oRow("Surcharge") + oRow("Tip")), "$###,##0.00"))
                    lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "# " & oRow("RefNum"))  'trunkExpNum)
                    yPos += (lastHeight + 30)
                End If
            Catch ex As Exception
                yPos += (lastHeight + 3)
                lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "Error: " & trunkAcctNum)
                yPos += (lastHeight + 3)

            End Try

        Next

    End Sub


    Friend Sub PrintCashSalesDrawer(ByVal ci As CashInfoStructure)

        Dim pd As New PrintDocument
        cashInfo = ci

        Try
            pd.PrintController = New StandardPrintController
            AddHandler pd.PrintPage, AddressOf pd_PrintPageCashDrawer1
            pd.PrinterSettings.PrinterName = currentTerminal.ReceiptName    '"Receipt" & CurrentTerminal.TermID
            pd.Print()
            pd.Dispose()

        Catch ex As Exception
            '         MsgBox(ex.Message)
            MsgBox("Printer " & pd.PrinterSettings.PrinterName & " is not connected")

        End Try

    End Sub

    Private Sub pd_PrintPageCashDrawer1(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        leftMargin = 0
        count = 0
        yPos = 0
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, Now.ToString)
        yPos = 120
        Dim oRow As DataRow

        PrintHeader(ev)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB22, "CASHOUT")
        DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, currentTerminal.DailyDate)
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB22, currentTerminal.TermName)
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, currentServer.FullName)
        yPos += (lastHeight + 60)

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB22, "Open Cash")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, cashInfo._openCash)
        yPos += (lastHeight + 20)

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB22, "   Net Sales")
        DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, cashInfo._netsales)
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB22, "   cc Sales")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "- " & cashInfo._ccSales)
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "-------------")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB22, "Cash Sales")
        DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, cashInfo._cashSales)
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB22, "   cc Tips")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "- " & cashInfo._ccTips)
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "-------------")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB22, "   ")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, cashInfo._cashBeforeOut)
        yPos += (lastHeight + 20)

        For Each oRow In dsOrder.Tables("CashOut").Rows
            lastHeight = DoPrinting(ev, 150, yPos, 0, pInfoA11, oRow("Description"))
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, oRow("PaymentAmount"))
            yPos += (lastHeight)
        Next
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, "-------------")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB22, "Total")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, (cashInfo._drawerTotal)) '_cashBeforeOut - cashInfo._cashOut))
        yPos += (lastHeight + 20)

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB22, "Close Cash")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, (cashInfo._closeCash))
        yPos += (lastHeight + 20)

        If cashInfo._overShort > 0 Then
            ' the drawer is over
            lastHeight = DoPrinting(ev, 250, yPos, 0, pInfoB22, "Over $")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, (cashInfo._overShort))
            yPos += (lastHeight + 40)
        Else
            lastHeight = DoPrinting(ev, 250, yPos, 0, pInfoB22, "Short $")
            lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, (cashInfo._overShort))
            yPos += (lastHeight + 40)
        End If

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB22, "DEPOSIT")
        lastHeight = DoPrinting(ev, pageWidth, yPos, 2, pInfoA11, (cashInfo._closeCash))
        yPos += (lastHeight + 60)

        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfoB22, "   ")

        yPos = 0


    End Sub

    Private Sub PrintHeader(ByVal ev As System.Drawing.Printing.PrintPageEventArgs)

        If Not companyInfo.companyName Is Nothing Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoB22, companyInfo.companyName)
            yPos += (lastHeight)
        End If

        If Not companyInfo.locationCity = Nothing Or Not companyInfo.locationState = Nothing Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, pInfoA11, companyInfo.locationCity & ",  " & companyInfo.locationState)
            yPos += (lastHeight)
        End If

        yPos += (50)

    End Sub



    Public Sub PrintBarCodeStart()

        Dim pd As New PrintDocument
        Try
            pd.PrintController = New StandardPrintController
            AddHandler pd.PrintPage, AddressOf PrintBarcode
            '      Me.printFont = New Font("Free 3 of 9 Extended Regular", 13.5)
            '    Me.printFont = New Font("Free 3 of 9 Extended", 13.5)
            pd.PrinterSettings.PrinterName = currentTerminal.ReceiptName     '"Receipt" & currentTerminal.TermID
            pd.Print()
            pd.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub PrintBarcode(ByVal sender As Object, ByVal ev As PrintPageEventArgs)

        If Not companyInfo.companyName Is Nothing Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, code39, companyInfo.companyName)

            yPos += (lastHeight)
        End If

        If Not companyInfo.locationCity = Nothing Or Not companyInfo.locationState = Nothing Then
            lastHeight = DoPrinting(ev, midWidth, yPos, 6, code39, companyInfo.locationCity & ",  " & companyInfo.locationState)
            yPos += (lastHeight)
        End If

        yPos += (50)

    End Sub












    '222
    ' there is some code at bottom that is needed

    Private Sub PrintCloseCheck222()

        Try
            streamToPrint = New StreamReader("c:\Data Files\spiderPOS\Receipt.txt")
            Try
                '         Me.printFont = New Font("Times New Roman", 12)
                '        Me.printFont = New Font("Microsoft Sans Serif", 12)
                Me.printFont = New Font("FontB12", 13.5)  '("Arial", 11) '("FontA42", 8)  '
                Dim pd As New PrintDocument
                pd.PrintController = New StandardPrintController
                AddHandler pd.PrintPage, AddressOf pd_PrintPage222                 'EPSONFix
                pd.PrinterSettings.PrinterName = "Bar"
                pd.PrinterSettings.PrinterName = "Receipt"
                '           pd.PrinterSettings.PrinterName = "HP 722 local"
                pd.Print()
                pd.Dispose()

            Finally
                streamToPrint.Close()
            End Try
        Catch ex As Exception
            '            info = New DataSet_Builder.Information_UC(ex.Message)
            '           info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            '          Me.Controls.Add(info)
            '         info.BringToFront()
        End Try

    End Sub

    Friend Sub StartClockOutPrint222(ByRef clockOutJunk As ClockOutInfo)
        clockoutWriter1 = New StreamWriter("c:\Data Files\spiderPOS\ClockOut.txt")

        CreateCloseoutHeader222(clockOutJunk)

        clockoutWriter1.Close()
        PrintClockOut222()

    End Sub

    Private Sub CreateCloseoutHeader222(ByRef clockOutJunk As ClockOutInfo)

        clockoutWriter1.Write("*CN*")
        clockoutWriter1.WriteLine("EMPLOYEE CLOCK OUT")

        clockoutWriter1.WriteLine("*BL*")
        clockoutWriter1.Write("*RJ*")
        clockoutWriter1.Write(companyInfo.companyName)
        clockoutWriter1.Write("%")
        clockoutWriter1.WriteLine(Format(Now, "M/d/yyyy"))

        clockoutWriter1.Write("*RJ*")
        clockoutWriter1.Write(currentServer.JobCodeName & ":  ")   '("Server: ")
        clockoutWriter1.Write(currentServer.FullName)
        clockoutWriter1.Write("%")
        clockoutWriter1.WriteLine(Format(Now, "h:mm tt"))

        clockoutWriter1.WriteLine("*BL*")
        clockoutWriter1.WriteLine("Job: " & currentServer.JobCodeName)

        clockoutWriter1.Write("*RJ*")
        clockoutWriter1.Write("Time in:")
        clockoutWriter1.Write("%")
        clockoutWriter1.WriteLine(Format(clockOutJunk.TimeIn, "h:m tt"))

        clockoutWriter1.Write("*RJ*")
        clockoutWriter1.Write("Time out:")
        clockoutWriter1.Write("%")
        clockoutWriter1.WriteLine(Format(clockOutJunk.TimeOut, "h:m tt"))

        clockoutWriter1.Write("*RJ*")
        clockoutWriter1.Write("Hours this shift:")
        clockoutWriter1.Write("%")
        clockoutWriter1.WriteLine(clockOutJunk.ShiftHours & ":" & Format(clockOutJunk.ShiftMins, "##"))

        clockoutWriter1.Write("*RJ*")
        clockoutWriter1.Write("Hours this week:")
        clockoutWriter1.Write("%")
        clockoutWriter1.WriteLine(clockOutJunk.WeekHours & ":" & Format(clockOutJunk.WeekMins, "##"))

        clockoutWriter1.Write("*RJ*")
        clockoutWriter1.Write("Tipable Sales:")
        clockoutWriter1.Write("%")
        clockoutWriter1.WriteLine("$  " & clockOutJunk.TipableSales)

        clockoutWriter1.Write("*RJ*")
        clockoutWriter1.Write("Charge Sales:")
        clockoutWriter1.Write("%")
        clockoutWriter1.WriteLine("$  " & clockOutJunk.ChargedSales)

        clockoutWriter1.Write("*RJ*")
        clockoutWriter1.Write("Charge Tips:")
        clockoutWriter1.Write("%")
        clockoutWriter1.WriteLine("$  " & clockOutJunk.ChargedTips)

        clockoutWriter1.Write("*RJ*")
        clockoutWriter1.Write("Declared Tips:")
        clockoutWriter1.Write("%")
        clockoutWriter1.WriteLine("$  " & clockOutJunk.DeclaredTips)

        clockoutWriter1.WriteLine("*BL*")
        clockoutWriter1.WriteLine("*BL*")
        clockoutWriter1.WriteLine("**********************************")
        clockoutWriter1.WriteLine("Verification for hours worked.")
        clockoutWriter1.WriteLine("Keep this for your records.")
        clockoutWriter1.WriteLine("*BL*")
        clockoutWriter1.WriteLine("*BL*")

    End Sub



    Private Sub PrintClockOut222()
        Try
            '   streamToPrint = New StreamReader("c:\Data Files\spiderPOS\ClockOut.txt")
            Try
                '         Me.printFont = New Font("Times New Roman", 12)
                '        Me.printFont = New Font("Microsoft Sans Serif", 12)
                Me.printFont = New Font("Arial", 11)
                Dim pd As New PrintDocument
                pd.PrintController = New StandardPrintController
                AddHandler pd.PrintPage, AddressOf pd_PrintPage222
                pd.PrinterSettings.PrinterName = currentTerminal.ReceiptName '"Bar"
                '     pd.PrinterSettings.PrinterName = "Receipt"
                '       pd.PrinterSettings.PrinterName = "Kitchen"
                pd.Print()
                pd.Dispose()
            Finally
                '          streamToPrint.Close()
            End Try
        Catch ex As Exception
            '            info = New DataSet_Builder.Information_UC(ex.Message)
            '           info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            '          Me.Controls.Add(info)
            '         info.BringToFront()
        End Try

    End Sub
    Private Function PlaceOrderInOrderDetail222(ByVal isMainCourse As Boolean, ByVal avgDollar As Decimal)

        Dim newOrderNumber As Int64
        Dim cmdOrderNumber = New SqlClient.SqlCommand("SELECT MAX(OrderNumber) lastOrderNumber FROM ExperienceStatusChange WHERE LocationID = '" & companyInfo.LocationID & "' AND CompanyID = '" & companyInfo.CompanyID & "'", sql.cn)

        Dim cmd As SqlClient.SqlCommand
        Dim dtr As SqlClient.SqlDataReader

        Try
            '   must keep database open so nobody else retreives this OrderNumber
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            dtr = cmdOrderNumber.executereader
            dtr.Read()
            If Not dtr("lastOrderNumber") Is DBNull.Value Then
                newOrderNumber = (dtr("lastOrderNumber")) + 1
            Else
                newOrderNumber = 1
            End If
            '     ??            AddStatusChangeData(3, newOrderNumber, isMainCourse, avgDollar)

            dtr.Close()

            '   this is duplicated in GenerateOrderTables but we need not to close the SQL connection
            cmd = New SqlClient.SqlCommand("INSERT INTO ExperienceStatusChange (CompanyID, LocationID, DailyCode, ExperienceNumber, StatusTime, TableStatusID, OrderNumber, IsMainCourse, AverageDollar, TerminalID, dbUP) VALUES (@CompanyID, @LocationID, @DailyCode, @ExperienceNumber, @StatusTime, @TableStatusID, @OrderNumber, @IsMainCourse, @AverageDollar, @TerminalID, @dbUP)", sql.cn)

            cmd.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", System.Data.SqlDbType.NChar, 6))
            cmd.Parameters("@CompanyID").Value = companyInfo.CompanyID
            cmd.Parameters.Add(New SqlClient.SqlParameter("@LocationID", System.Data.SqlDbType.NChar, 6))
            cmd.Parameters("@LocationID").Value = companyInfo.LocationID
            cmd.Parameters.Add(New SqlClient.SqlParameter("@DailyCode", SqlDbType.BigInt, 8))
            cmd.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
            cmd.Parameters.Add(New SqlClient.SqlParameter("@ExperienceNumber", SqlDbType.BigInt, 8))
            cmd.Parameters("@ExperienceNumber").Value = currentTable.ExperienceNumber
            cmd.Parameters.Add(New SqlClient.SqlParameter("@StatusTime", SqlDbType.DateTime, 8))
            cmd.Parameters("@StatusTime").Value = Now
            cmd.Parameters.Add(New SqlClient.SqlParameter("@TableStatusID", SqlDbType.Int, 4))
            cmd.Parameters("@TableStatusID").Value = 3
            cmd.Parameters.Add(New SqlClient.SqlParameter("@OrderNumber", SqlDbType.BigInt, 8))
            cmd.Parameters("@OrderNumber").Value = newOrderNumber
            cmd.Parameters.Add(New SqlClient.SqlParameter("@IsMainCourse", SqlDbType.Bit, 1))
            cmd.Parameters("@IsMainCourse").Value = isMainCourse
            cmd.Parameters.Add(New SqlClient.SqlParameter("@AverageDollar", SqlDbType.Decimal, 5))
            cmd.Parameters("@AverageDollar").Value = avgDollar
            cmd.Parameters.Add(New SqlClient.SqlParameter("@TerminalID", SqlDbType.Int, 4))
            cmd.Parameters("@TerminalID").Value = currentTerminal.TermID
            cmd.Parameters.Add(New SqlClient.SqlParameter("@dbUP", SqlDbType.Bit, 1))
            cmd.Parameters("@dbUP").Value = 1 'True


            cmd.ExecuteNonQuery()

            sql.cn.Close()

            '   ***    not sure .. 
            '222      GenerateOrderTables.ChangeStatusInDataBase(3, newOrderNumber, isMainCourse, avgDollar)
            '222  PlaceOrderNumberInOpenOrders222(newOrderNumber)
            '    AddStatusChangeData(3, newOrderNumber, isMainCourse, avgDollar)
            '          TerminalAddStatusChangeData(3, newOrderNumber, isMainCourse, avgDollar)

        Catch ex As Exception
            CloseConnection()
            If mainServerConnected = True Then
                ServerJustWentDown()
            End If
            '        TerminalAddStatusChangeData(3, -1, isMainCourse, avgDollar) '-1 indicates server down when ordering
            '        GenerateOrderTables.ChangeStatusInDataBase(3, -1, isMainCourse, avgDollar)
            '       PlaceOrderNumberInOpenOrders(-1)
        End Try


        '   we sould add in OrderByPrinter      ****************************************
        '   we will create a collection keeping track of each printer totals



    End Function

    Private Sub PlaceOrderNumberInOpenOrders222(ByVal ordNum As Int64)
        Dim oRow As DataRow

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("ItemStatus") = 0 Then
                    oRow("ItemStatus") = 2
                    oRow("OrderNumber") = ordNum
                End If
            End If
        Next

    End Sub

    Private Function CalculateAverageDollar222()
        Dim avgCalculation As Single
        Dim denominatorCurrent As Single

        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
            If companyInfo.calculateAvgByEntrees = True Then
                '      If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
                denominatorCurrent = (dsOrder.Tables("OpenOrders").Compute("Count(FunctionID)", "FunctionID = 1 AND ItemStatus = 0"))
                '    Else : denominatorCurrent = 0
                '   End If
                If denominatorCurrent = 0 Then denominatorCurrent = 1
            Else        '   calculate avg by number of guests
                denominatorCurrent = currentTable.NumberOfCustomers
                If denominatorCurrent = 0 Then denominatorCurrent = 1
            End If
            avgCalculation = (dsOrder.Tables("OpenOrders").Compute("Sum(Price)", "")) / denominatorCurrent
        Else
            avgCalculation = 0
        End If

        Return avgCalculation

    End Function


    Private Function TerminalOrderNumber222()
        Dim lowestOrderNumber
        Dim dvOrderNumber As New DataView

        With dvOrderNumber
            .Table = dsBackup.Tables("ESCTerminal")
            .Sort = "OrderNumber"
        End With


        lowestOrderNumber = dvOrderNumber(0)("OrderNumber")


        If lowestOrderNumber > 0 Then
            lowestOrderNumber = 0
        End If

        lowestOrderNumber -= 1
        Return lowestOrderNumber


    End Function




    Friend Sub SendingOrder222()

        Dim avgDollar As Single

        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
            avgDollar = CalculateAverageDollar222()
        Else
            Exit Sub
        End If

        Dim oRow As DataRow
        Dim i As Integer

        i = 1
        For Each oRow In ds.Tables("RoutingChoice").Rows
            If Not oRow("RoutingName") = "Do Not Route" Then
                Select Case i
                    Case 1
                        sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\Printer1.txt")
                    Case 2
                        sWriter2 = New StreamWriter("c:\Data Files\spiderPOS\Printer2.txt")
                    Case 3
                        sWriter3 = New StreamWriter("c:\Data Files\spiderPOS\Printer3.txt")
                    Case 4
                        sWriter4 = New StreamWriter("c:\Data Files\spiderPOS\Printer4.txt")
                    Case 5
                        sWriter5 = New StreamWriter("c:\Data Files\spiderPOS\Printer5.txt")
                End Select
            End If
            i += 1
            If i = 20 Then Exit For
        Next


        Dim vRow As DataRowView
        Dim numberOfApps As Integer
        Dim numberOfDinners As Integer
        Dim isMainCourse As Boolean
        Dim newOrder As Boolean = False
        Dim RouteNameOnTicket As String
        Dim countCourse1 As Integer
        Dim countTotalSending As Integer


        'in 222
        countCourse1 = (dsOrder.Tables("OpenOrders")).Compute("Count(CourseNumber)", "CourseNumber = 1 AND ItemStatus = 0") ' (FunctionFlag = 'F' or FunctionFlag = 'M')")
        countTotalSending = (dsOrder.Tables("OpenOrders")).Compute("Count(ItemStatus)", "ItemStatus = 0")
        If countCourse1 = countTotalSending Then
            allCourse1 = True
        End If

        For Each vRow In dvOrderPrint            'dsOrder.Tables("OpenOrders").Rows
            '          For Each vRow In dvOrder
            '     If Not vRow.RowState = DataRowState.Deleted Then

            If vRow("ItemStatus") = 0 Then
                newOrder = True
                If vRow("sin") = vRow("sii") Then    ' and not a drink
                    If vRow("FunctionFlag") = "F" Then
                        numberOfDinners += 1
                    ElseIf vRow("FunctionFlag") = "M" Then
                        numberOfApps += 1
                    End If
                End If

                Select Case vRow("RoutingID")
                    Case printingRouting(1)
                        '            RouteNameOnTicket = printingName(1)
                        CreateOrderString(vRow, sWriter1, s1) ', 1) ', printingName(1))

                    Case printingRouting(2)
                        '           RouteNameOnTicket = printingName(2)
                        CreateOrderString(vRow, sWriter2, s2) ', 2) ', printingName(2))

                    Case printingRouting(3)
                        '          RouteNameOnTicket = printingName(3)
                        CreateOrderString(vRow, sWriter3, s3) ', 3) ', printingName(3))
                    Case printingRouting(4)
                        '         RouteNameOnTicket = printingName(4)
                        CreateOrderString(vRow, sWriter4, s4) ', 4) ', printingName(4))
                    Case printingRouting(5)
                        '        RouteNameOnTicket = printingName(5)
                        CreateOrderString(vRow, sWriter5, s5) ', 5) ', printingName(5))
                End Select
            End If
            '        End If
        Next

        i = 1
        For Each oRow In ds.Tables("RoutingChoice").Rows
            If Not oRow("RoutingName") = "Do Not Route" Then
                Select Case i
                    Case 1
                        sWriter1.Close()
                        If s1 = True Then
                            PrintTo(1)
                        End If
                    Case 2
                        sWriter2.Close()
                        If s2 = True Then
                            PrintTo(2)
                        End If
                    Case 3
                        sWriter3.Close()
                        If s3 = True Then
                            PrintTo(3)
                        End If
                    Case 4
                        sWriter4.Close()
                        If s4 = True Then
                            PrintTo(4)
                        End If
                    Case 5
                        sWriter5.Close()
                        If s5 = True Then
                            PrintTo(5)
                        End If
                End Select
            End If
            i += 1
            If i = 20 Then Exit For
        Next


        '   *** need to put below first if we want to print ORDER NUMBER on ticket
        '   we can do after we have a better understanding how we will generate order numbers 
        '   with multiple terminals
        If numberOfDinners > numberOfApps Then
            isMainCourse = True
        Else
            If currentTable.NumberOfCustomers > 0 And numberOfDinners > (currentTable.NumberOfCustomers / 2) Then
                isMainCourse = True
            End If
        End If

        '   we do this after printing 
        If newOrder = True Then
            If mainServerConnected = True Then
                PlaceOrderInOrderDetail222(isMainCourse, avgDollar)
            Else
                '            Dim termOrderNumber As Integer
                '           termOrderNumber = TerminalOrderNumber()
                '                TerminalAddStatusChangeData(3, termOrderNumber, isMainCourse, avgDollar)  '-1 indicates server down when ordering
                '               GenerateOrderTables.ChangeStatusInDataBase(3, termOrderNumber, isMainCourse, avgDollar)
                '              PlaceOrderNumberInOpenOrders(termOrderNumber)
            End If
        End If

    End Sub
    Private Sub pd_PrintPageEPSONFix222(ByVal sender As Object, ByVal ev As PrintPageEventArgs)

        Dim yPos As Single = 80
        Dim leftMargin As Single = 0    'ev.MarginBounds.Left
        Dim count As Integer
        Dim lastHeight As Integer
        Dim vRow As DataRowView
        Dim pInfo As New FontInfo("FontA12", 19, 1) '("FontA11", 9.5, 1) '("FontB48", 15, 1) '("FontB42", 13.5, 1)
        Dim pInfo2 As New FontInfo("FontB24", 27, 1)
        Dim midWidth As Integer
        'page width is 500

        midWidth = 250

        'ticket Header
        lastHeight = DoPrinting(ev, leftMargin, yPos, 0, pInfo, currentRoutingName)
        lastHeight = DoPrinting(ev, midWidth, yPos, 0, pInfo2, currentTable.MethodUse)
        yPos += (60 + lastHeight)
        lastHeight = DoPrinting(ev, leftMargin, yPos, 0, pInfo, "******************************************")
        yPos += (lastHeight)
        lastHeight = DoPrinting(ev, 500, yPos, 2, pInfo, Format(Now, "hh:mm tt"))
        yPos += lastHeight
        lastHeight = DoPrinting(ev, leftMargin, yPos, 0, pInfo, "Table: " & currentTable.TabName)
        yPos += lastHeight
        lastHeight = DoPrinting(ev, leftMargin, yPos, 0, pInfo, "Guests: " & currentTable.NumberOfCustomers)
        yPos += lastHeight
        lastHeight = DoPrinting(ev, leftMargin, yPos, 0, pInfo, "Order: " & oDetail.trunkOrderNumber)
        yPos += (40 + lastHeight)


        Dim lastCourseNumber As Integer
        Dim firstCustomer As Boolean = True

        For Each vRow In Me.dvOrderByPrinter
            If allCourse1 = False Then
                If vRow("CourseNumber") <> lastCourseNumber Then
                    firstCustomer = True
                    lastCourseNumber = vRow("CourseNumber")
                    yPos += 80
                    lastHeight = DoPrinting(ev, 500, yPos, 2, pInfo2, "Course    " & vRow("CourseNumber"))
                    yPos += lastHeight
                    lastHeight = DoPrinting(ev, 500, yPos, 2, pInfo, "*****************")
                    yPos += (lastHeight)
                End If
            End If
            If vRow("FunctionFlag") = "F" Or vRow("FunctionFlag") = "O" Or vRow("FunctionFlag") = "M" Or vRow("FunctionFlag") = "D" Then
                If vRow("sin") = vRow("sii") Then
                    If vRow("Quantity") > 1 Then
                        lastHeight = DoPrinting(ev, 50, yPos, 0, pInfo2, "Quantity:    " & vRow("Quantity"))
                        yPos += lastHeight
                    End If
                    If firstCustomer = False Then
                        lastHeight = DoPrinting(ev, 0, yPos, 0, pInfo, "-------------------------------")
                        yPos += lastHeight
                    Else
                        firstCustomer = False
                    End If
                    lastHeight = DoPrinting(ev, 0, yPos, 0, pInfo, "C" & vRow("CustomerNumber"))
                End If
                lastHeight = DoPrinting(ev, 50, yPos, 0, pInfo, vRow("ChitName"))             'need price too
                yPos += lastHeight     '100 + (count * nHeight)
            End If
            '       MsgBox(ev.MarginBounds.ToString, , "Margin Bounds")
            '      MsgBox(ev.PageBounds.ToString, , "Page Bounds")
            '     MsgBox(ev.PageBounds.Width.ToString, , "Page Bounds Width")
            '    MsgBox(ev.PageSettings.PaperSize.Width.ToString, , "Paper Width")
            '     count += 1
        Next

        'space on bottom
        yPos += lastHeight
        lastHeight = DoPrinting(ev, 50, yPos, 0, pInfo, "    ")

        Exit Sub

        '***  this is just testing for epson
        Dim linesPerPage As Single

        Dim nHeight As Integer

        Dim topMargin As Single = ev.MarginBounds.Top
        Dim line As String = Nothing
        '    Dim boldFont = New Font(printFont, FontStyle.Bold)
        Dim pageWidthUsing As Single
        pageWidthUsing = ev.MarginBounds.Right - leftMargin
        Dim drawFormat As New StringFormat

        linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics)


        Dim hdc As IntPtr = New IntPtr
        Dim font As IntPtr = New IntPtr
        nHeight = -pInfo.nFontSize * UsingGDI.GetDeviceCaps(hdc, 90) / 72
        font = UsingGDI.CreateFont(nHeight, 0, 0, 0, 400, 0, 0, 0, _
                                              pInfo.nCharSet, 0, 0, 0, 0, pInfo.sFontName)


        For count = 0 To linesPerPage

        Next


        While count < linesPerPage
            line = streamToPrint.ReadLine

            yPos = 100 + (count * nHeight)

            DoPrinting(ev, leftMargin, yPos, 0, pInfo, line)
            '       If line Is Nothing Then Exit While 


            count += 1
        End While

        Exit Sub

        While count < linesPerPage
            line = streamToPrint.ReadLine
            If line Is Nothing Then Exit While
            '       yPos = topMargin + count * printFont.GetHeight(ev.Graphics)
            If line.Substring(0, 4) = "*RT*" Then       'REVERSE TEXT

                line = line.Remove(0, 4)
                Dim rect As New Rectangle(0, yPos, ev.PageBounds.Width / 4, printFont.GetHeight(ev.Graphics))

                ev.Graphics.FillRectangle(Brushes.Black, rect)
                ev.Graphics.DrawString(line, printFont, Brushes.White, leftMargin, yPos, New StringFormat)
            ElseIf line.Substring(0, 4) = "*BL*" Then   'BLANK LINE
                line = line.Remove(0, 4)
                Dim rect As New Rectangle(0, yPos, ev.PageBounds.Width / 4, printFont.GetHeight(ev.Graphics))
                '       ev.Graphics.FillRectangle(Brushes.White, rect)
            ElseIf line.Substring(0, 4) = "*CN*" Then   'CENTER
                line = line.Remove(0, 4)
                '       drawFormat.Alignment = StringAlignment.Center
                '      DoPrinting(ev, ((ev.PageBounds.Width) / 2) - 15, count, pInfo, line)
                DoPrinting(ev, leftMargin, count, 0, pInfo, line)

                '        ev.Graphics.DrawString(line, printFont, Brushes.Black, ((ev.PageBounds.Width) / 2) - 15, yPos, drawFormat)
            ElseIf line.Substring(0, 4) = "*RJ*" Then   'RIGHT JUSTIFY
                line = line.Remove(0, 4)

                Dim split As String()
                '        split = line.Split("%", 1)
                '       split = line.Split("%", 2)

                DoPrinting(ev, leftMargin, count, 0, pInfo, split(0))
                '     ev.Graphics.DrawString(split(0), printFont, Brushes.Black, leftMargin, yPos, New StringFormat)
                drawFormat.Alignment = StringAlignment.Far
                DoPrinting(ev, ev.PageBounds.Width - line.Length, count, 0, pInfo, split(0))
                '      ev.Graphics.DrawString(split(1), printFont, Brushes.Black, ev.PageBounds.Width - 45, yPos, drawFormat)

            Else
                DoPrinting(ev, leftMargin, count, 0, pInfo, line)
                '            ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, New StringFormat)
            End If
            count += 1
        End While

    End Sub
    Private Function CreateOrderString(ByRef vRow As DataRowView, ByRef sw As StreamWriter, ByRef sFlag As Boolean) ', ByVal routingid As Integer)

        Dim currentPrintString As String
        Dim stringCheckNumber As String
        Dim quantityOrdered As Integer

        If vRow("FunctionFlag") = "F" Or vRow("FunctionFlag") = "O" Or vRow("FunctionFlag") = "M" Or vRow("FunctionFlag") = "D" Then
            If vRow("sin") = vRow("sii") Then
                If sFlag = False Then
                    stringCheckNumber = vRow("CheckNumber") & "  of  " & currentTable.NumberOfChecks.ToString & "          " & Format(Now, "hh:mm tt")
                    sw.WriteLine("New Ticket ")
                    '        sw.WriteLine(printingName(routingid))
                    '      sw.WriteLine(routingName)
                    '       sw.WriteLine(printingName(1))
                    sw.WriteLine("*BL*")
                    sw.WriteLine("*****************************")
                    sw.WriteLine("*BL*")
                    sw.WriteLine("*BL*")
                    sw.WriteLine(stringCheckNumber)    'add time
                    sw.WriteLine("Table:    " & currentTable.TabName) 'vRow("TabName")) 'this is table#
                    sw.WriteLine("Guests:  " & currentTable.NumberOfCustomers)
                    sw.WriteLine("Order:   " & vRow("OrderNumber"))
                    sw.WriteLine("*BL*")

                    sFlag = True
                Else
                    sw.WriteLine("------------------------------")      'divider
                End If

                If allCourse1 = False Then
                    If vRow("CourseNumber") <> lastCourseNumber Then
                        '   start of new course
                        lastCourseNumber = vRow("CourseNumber")
                        sw.WriteLine("*BL*")
                        sw.WriteLine("*RT*   COURSE  " & lastCourseNumber)
                        sw.WriteLine("*BL*")
                        '      sw.WriteLine("*BL*")
                    End If
                End If
                If vRow("Quantity") > 1 Then
                    quantityOrdered = vRow("Quantity")
                    sw.WriteLine("   Quantity  " & quantityOrdered)
                End If
                currentPrintString = "C" & vRow("CustomerNumber") & "   "   '3 spaces
            Else
                currentPrintString = "        "     'eight spaces
            End If
            currentPrintString = currentPrintString & vRow("ChitName")
            sw.WriteLine(currentPrintString)
        End If
        '    sw.WriteLine("  ") screws printing up


    End Function
    Private Sub CreateCheckHeader222()
        Dim numberLeft As Integer
        closeCheckWriter1 = New StreamWriter("c:\Data Files\spiderPOS\ClockOut.txt")

        If Not companyInfo.companyName = Nothing Then
            '     closeCheckWriter1.Write("*CN*")
            closeCheckWriter1.WriteLine(companyInfo.companyName)
        End If
        If Not companyInfo.locationName = Nothing Then
            closeCheckWriter1.Write("*CN*")
            closeCheckWriter1.WriteLine(companyInfo.locationName)
        End If
        If Not companyInfo.locationCity = Nothing Or Not companyInfo.locationState = Nothing Then
            closeCheckWriter1.Write("*CN*")
            closeCheckWriter1.WriteLine(companyInfo.locationCity & ",  " & companyInfo.locationState)
        End If
        If Not companyInfo.locationPhone = Nothing Then
            closeCheckWriter1.Write("*CN*")
            closeCheckWriter1.WriteLine(companyInfo.locationPhone)
        End If
        closeCheckWriter1.WriteLine("*BL*")
        closeCheckWriter1.WriteLine("*BL*")

        closeCheckWriter1.Write("*RJ*")
        closeCheckWriter1.Write("Server: ")
        closeCheckWriter1.Write(currentServer.NickName)
        closeCheckWriter1.Write("%")
        closeCheckWriter1.WriteLine(Format(Now, "M/d/yyyy"))

        closeCheckWriter1.Write("*RJ*")
        closeCheckWriter1.Write("Table: ")
        closeCheckWriter1.Write(currentTable.TabName)
        closeCheckWriter1.Write("%")
        closeCheckWriter1.WriteLine(Format(Now, "h:mm tt"))

        closeCheckWriter1.WriteLine("Guests: " & currentTable.NumberOfCustomers)

        closeCheckWriter1.Write("*RJ*")
        closeCheckWriter1.Write("Check: ")
        closeCheckWriter1.Write(currentTable.CheckNumber)
        closeCheckWriter1.Write(" of " & currentTable.NumberOfChecks)
        closeCheckWriter1.Write("%")
        closeCheckWriter1.WriteLine("# " & currentTable.ExperienceNumber)


        closeCheckWriter1.WriteLine("*BL*")
        closeCheckWriter1.WriteLine("*BL*")


    End Sub

    Private Sub FillCheckDetail222(ByRef dvClosing As DataView, ByVal chkSubTotal As Decimal, ByVal checkTax As Decimal)
        Dim vRow As DataRowView
        Dim runingPaymentTotal As Decimal

        For Each vRow In dvClosing
            If Not vRow("ItemID") = 0 Then
                closeCheckWriter1.Write("*RJ*")
                closeCheckWriter1.Write(vRow("ChitName"))
                closeCheckWriter1.Write("%")
                closeCheckWriter1.WriteLine(vRow("Price"))
            End If
        Next

        closeCheckWriter1.WriteLine("*BL*")

        closeCheckWriter1.Write("*RJ*")
        closeCheckWriter1.Write("Sub Total: ")
        closeCheckWriter1.Write("%")
        closeCheckWriter1.WriteLine(chkSubTotal)

        closeCheckWriter1.Write("*RJ*")
        closeCheckWriter1.Write("Tax: ")
        closeCheckWriter1.Write("%")
        closeCheckWriter1.WriteLine(checkTax)

        closeCheckWriter1.WriteLine("*BL*")
        closeCheckWriter1.Write("*RJ*")
        closeCheckWriter1.Write("Total: ")
        closeCheckWriter1.Write("%")
        closeCheckWriter1.WriteLine(chkSubTotal + checkTax)

        closeCheckWriter1.WriteLine("*BL*")


    End Sub

    Private Sub FillCheckPayments()


        ' For Each vRow In Payments

        'Next

        closeCheckWriter1.Write("*RJ*")
        closeCheckWriter1.Write("Change: ")
        closeCheckWriter1.Write("%")
        closeCheckWriter1.WriteLine() 'change)


    End Sub

    Private Sub CreateCheckFooter222()
        closeCheckWriter1.WriteLine("*BL*")
        closeCheckWriter1.WriteLine("*BL*")

        closeCheckWriter1.Write("*CN*")
        closeCheckWriter1.WriteLine("Thank You")
        closeCheckWriter1.Write("*CN*")
        closeCheckWriter1.WriteLine("Please Come Again")

        closeCheckWriter1.WriteLine("*BL*")
        closeCheckWriter1.WriteLine("*BL*")

    End Sub

    Friend Sub PrintTo(ByVal printerFile As Integer)

        Try
            streamToPrint = New StreamReader("c:\Data Files\spiderPOS\Printer" & printerFile.ToString & ".txt")
            '      streamToPrint = New StreamReader("c:\Data Files\spiderPOS\Printer2.txt")

            Try
                Me.printFont = New Font("Arial", 16)    '("FontA12", 19)  '
                Dim pd As New PrintDocument
                pd.PrintController = New StandardPrintController
                AddHandler pd.PrintPage, AddressOf pd_PrintPageEPSONFix
                pd.PrinterSettings.PrinterName = printingName(printerFile)

                pd.PrinterSettings.PrinterName = "Receipt"                         '*** need to remove
                '      pd.PrinterSettings.PrinterName = "Kitchen"                  '*** need to remove
                '         pd.PrinterSettings.PrinterName = "HP 722 local"
                pd.Print()
                pd.Dispose()
            Finally
                streamToPrint.Close()
            End Try
        Catch ex As Exception
            '            info = New DataSet_Builder.Information_UC(ex.Message)
            '           info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            '          Me.Controls.Add(info)
            '         info.BringToFront()
        End Try

    End Sub


    Private Sub pd_PrintPage222(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Dim linesPerPage As Single
        Dim yPos As Single
        Dim count As Integer
        Dim leftMargin As Single = 0    'ev.MarginBounds.Left
        Dim topMargin As Single = ev.MarginBounds.Top
        Dim line As String = Nothing
        '    Dim boldFont = New Font(printFont, FontStyle.Bold)
        Dim pageWidthUsing As Single
        pageWidthUsing = ev.MarginBounds.Right - leftMargin
        Dim drawFormat As New StringFormat

        linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics)

        While count < linesPerPage
            line = streamToPrint.ReadLine
            If line Is Nothing Then Exit While
            yPos = topMargin + count * printFont.GetHeight(ev.Graphics)
            If line.Substring(0, 4) = "*RT*" Then       'REVERSE TEXT

                line = line.Remove(0, 4)
                Dim rect As New Rectangle(0, yPos, ev.PageBounds.Width / 4, printFont.GetHeight(ev.Graphics))

                ev.Graphics.FillRectangle(Brushes.Black, rect)
                ev.Graphics.DrawString(line, printFont, Brushes.White, leftMargin, yPos, New StringFormat)
            ElseIf line.Substring(0, 4) = "*BL*" Then   'BLANK LINE
                line = line.Remove(0, 4)
                Dim rect As New Rectangle(0, yPos, ev.PageBounds.Width / 4, printFont.GetHeight(ev.Graphics))
                ev.Graphics.FillRectangle(Brushes.White, rect)
            ElseIf line.Substring(0, 4) = "*CN*" Then   'CENTER
                line = line.Remove(0, 4)
                drawFormat.Alignment = StringAlignment.Center
                ev.Graphics.DrawString(line, printFont, Brushes.Black, ((ev.PageBounds.Width) / 2) - 15, yPos, drawFormat)
            ElseIf line.Substring(0, 4) = "*RJ*" Then   'RIGHT JUSTIFY
                line = line.Remove(0, 4)

                Dim split As String()
                '         split = line.Split("%", 1)
                '        split = line.Split("%", 2)

                ev.Graphics.DrawString(split(0), printFont, Brushes.Black, leftMargin, yPos, New StringFormat)
                drawFormat.Alignment = StringAlignment.Far
                ev.Graphics.DrawString(split(1), printFont, Brushes.Black, ev.PageBounds.Width - 45, yPos, drawFormat)

            Else
                ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, New StringFormat)
            End If
            count += 1
        End While

    End Sub

    Private Sub CreateTicketHeader222(ByVal sender As Object, ByVal ev As PrintPageEventArgs)

        Dim ppInfo(9) As FontInfo
        ppInfo(0) = New FontInfo("FontA11", 9.5, 1)
        ppInfo(1) = New FontInfo("FontA12", 19, 1)
        ppInfo(2) = New FontInfo("FontA21", 9.5, 1)
        ppInfo(3) = New FontInfo("FontA22", 19, 1)

        ppInfo(4) = New FontInfo("FontB11", 7, 1)
        ppInfo(5) = New FontInfo("FontB12", 13.5, 1)
        ppInfo(6) = New FontInfo("FontB21", 7.0, 1)
        ppInfo(7) = New FontInfo("FontB22", 13.5, 1)
        ppInfo(8) = New FontInfo("FontB24", 27, 1)
        ppInfo(9) = New FontInfo("FontB48", 54.5, 1)

        Dim i As Integer
        Dim s As String


        For i = 0 To 9
            s = ppInfo(i).sFontName & "  " & ppInfo(i).nFontSize
            DoPrinting(ev, 0, 100 * i, 0, ppInfo(i), s)
        Next
    End Sub


End Class




'*******************
' below is good code
'*******************

Public Class FontInfo
    Public sFontName As String
    Public nFontSize As Single
    Public nCharSet As Integer

    Public Sub New(ByVal font As String, ByVal size As Single, ByVal charSet As Integer)
        sFontName = font
        nFontSize = size
        nCharSet = charSet
    End Sub

End Class

Public Class UsingGDI
    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
    Public Shared Function TextOut(ByVal hdc As IntPtr, _
                                   ByVal xStart As Integer, ByVal yStart As Integer, _
                                   ByVal str As String, ByVal length As Integer) As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
       Public Shared Function SetTextAlign(ByVal hdc As IntPtr, ByVal wFlags As Integer) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
      Public Shared Function SetTextColor(ByVal crColor As Color) As Color
    End Function
 

    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
    Public Shared Function CreateFont(ByVal nHeight As Integer, ByVal nWidth As Integer, _
                                       ByVal nEscapement As Integer, ByVal nOrientation As Integer, _
                                       ByVal fnWeight As Integer, ByVal fdwItalic As Integer, _
                                       ByVal fdwUnderline As Integer, ByVal fdwStrikeOut As Integer, _
                                       ByVal fdwCharSet As Integer, ByVal fdwOutputPrecision As Integer, _
                                       ByVal fdwClipPrec As Integer, ByVal fdwQuality As Integer, _
                                       ByVal fdwPitch As Integer, ByVal fontFace As String) As IntPtr
    End Function

    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
    Public Shared Function SelectObject(ByVal hdc As IntPtr, ByVal obj As IntPtr) As IntPtr
    End Function

    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
    Public Shared Function GetDeviceCaps(ByVal hdc As IntPtr, ByVal nIndex As Integer) As Integer
    End Function

End Class
