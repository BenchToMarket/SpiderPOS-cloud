
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.IO
Imports DataSet_Builder

Public Class ReadCredit

    Implements IDisposable

    '  Dim auth As New TStream


    '   HID device
    Const MagtekVendorID As Short = 2049        '801 (this is the Hex VendorID)
    Const IDTechVendorID As Short = 2765
    '  Const KanecalVendorID As Short = 2765
    '   Const MyProductID As Short = 2
    '  Const IDTechProductID As Short = 1280
    Friend myDeviceDetected As Boolean
    Friend myDevicePathName As String
    '  Friend myDevice As IDataReader
    Friend ReadHandle As Integer
    Dim EventObject As Integer
   

    Dim deviceAttributes As HIDD_ATTRIBUTES
    Dim devicePathName As String
    Dim deviceInfoSet As IntPtr
    Dim myDeviceInterfaceData As SP_DEVICE_INTERFACE_DATA
    Dim myDeviceInterfaceDetailData As SP_DEVICE_INTERFACE_DETAIL_DATA
    Dim HIDHandle As Integer
    Dim vbNullString As String = Nothing

    Dim lastDevice As Boolean = False
    Dim result As Integer
    Dim bResult As Boolean
    Dim MemberIndex As Integer
    Dim m_lBufferSize As Integer
    Dim security As SECURITY_ATTRIBUTES
    Dim HidGuid As System.Guid
    Dim Capabilities As HIDP_CAPS
    Dim HIDOverlapped As OVERLAPPED
    Dim PreparsedData As IntPtr

    Public Const HidP_Input As Short = 0

    Dim cardSwiped As Boolean

    Dim NumberOfBytesRead As Integer
    'Allocate a buffer for the report.
    'Byte 0 is the report ID.
    Dim ReadBuffer() As Byte

    Friend newPayment As DataSet_Builder.Payment

    Dim _payName As String
    Dim _isNewTab As Boolean = False
    Dim _activeScreen As String
    '   OPTIONS:
    '       Login
    '       OrderScreen
    '       CloseCheck
    '       SeatingTab
    '       TabEnterScreen

    Dim _giftAddingAmount As Boolean

    Friend Property PayName() As String
        Get
            Return _payName
        End Get
        Set(ByVal Value As String)
            _payName = Value
        End Set
    End Property

    Public Property IsNewTab() As Boolean
        Get
            Return _isNewTab
        End Get
        Set(ByVal value As Boolean)
            _isNewTab = value
        End Set
    End Property

    Public Property ActiveScreen() As String
        Get
            Return _activeScreen
        End Get
        Set(ByVal value As String)
            _activeScreen = value
        End Set
    End Property

    Public Property GiftAddingAmount() As Boolean
        Get
            Return _giftAddingAmount
        End Get
        Set(ByVal value As Boolean)
            _giftAddingAmount = value
        End Set
    End Property

    Private components As System.ComponentModel.IContainer

    Event CardReadSuccessful(ByRef newPayment As DataSet_Builder.Payment)
    Event CardReadFailed()
    Event EnteringTabNameInKeyboard(ByVal tabName As String)
    Event ManagementCardSwiped(ByVal emp As DataSet_Builder.Employee)
    Event RetruningGiftAddingAmountToFalse()


    Sub New(ByVal _isNewTabBoolean As Boolean)
        '   MyBase.new()

        'testing
        '     _isNewTabBoolean = True

        _isNewTab = _isNewTabBoolean
        CloseManualAuth_Load()
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Sub Dispose() Implements IDisposable.Dispose

        If Not (components Is Nothing) Then
            components.Dispose()
            GC.SuppressFinalize(Me)
        End If
        '        Me.Dispose()
    End Sub


    Friend Sub CloseManualAuth_Load() '(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '    Me.tmrCardRead = New System.Windows.Forms.Timer
        '     AddHandler tmrCardRead.Tick, AddressOf tmrCardRead_Tick
        tmrCardRead.Interval = 500  '100  '
        '     tmrCardRead.Start()

        HidGuid = Guid.Empty
        lastDevice = False

        security.lpSecurityDescriptor = 0
        security.bInheritHandle = CInt(True)
        security.nLength = (Len(security))

        result = HidD_GetHidGuid(HidGuid)
        '   MsgBox("HidGuid:  " & HidGuid.ToString)

        deviceInfoSet = SetupDiGetClassDevs(HidGuid, vbNullString, 0, DIGCF_PRESENT Or DIGCF_DEVICEINTERFACE)       '16) '2 Or 16)
        '    MsgBox("deviceInfoSet:  " & deviceInfoSet.ToString)

        MemberIndex = 0

        Do
            myDeviceInterfaceData.cbSize = Marshal.SizeOf(myDeviceInterfaceData)

            result = SetupDiEnumDeviceInterfaces(deviceInfoSet, 0, HidGuid, MemberIndex, myDeviceInterfaceData)
            If result = 0 Then lastDevice = True
            '        MsgBox("myDeviceInterfaceData:   " & myDeviceInterfaceData.InterfaceClassGuid.ToString)


            If result <> 0 Then

                bResult = SetupDiGetDeviceInterfaceDetail(deviceInfoSet, myDeviceInterfaceData, IntPtr.Zero, 0, m_lBufferSize, IntPtr.Zero)

                '   store the structure data
                myDeviceInterfaceDetailData.cbSize = Marshal.SizeOf(myDeviceInterfaceDetailData)

                Dim detailDataBuffer As IntPtr = Marshal.AllocHGlobal(m_lBufferSize)
                '   stores cbSize in first 4 bytes of array
                Marshal.WriteInt32(detailDataBuffer, 4 + Marshal.SystemDefaultCharSize)

                bResult = SetupDiGetDeviceInterfaceDetail(deviceInfoSet, myDeviceInterfaceData, detailDataBuffer, m_lBufferSize, m_lBufferSize, IntPtr.Zero)

                Dim pDevicePathName As IntPtr = New IntPtr(detailDataBuffer.ToInt32 + 4)
                devicePathName = Marshal.PtrToStringAuto(pDevicePathName)
                '        MsgBox("ProductID:  " & deviceAttributes.ProductID)
                '       MsgBox("DevicePathName:   " & devicePathName)

                Marshal.FreeHGlobal(detailDataBuffer)   'free's memory allocated earlier

                security.lpSecurityDescriptor = 0
                security.bInheritHandle = CInt(True)
                security.nLength = Len(security)

                HIDHandle = CreateFile(devicePathName, GENERIC_READ Or GENERIC_WRITE, FILE_SHARE_READ Or FILE_SHARE_WRITE, security, OPEN_EXISTING, 0, 0)
                '          MsgBox("HIDHandle:  " & HIDHandle)

                deviceAttributes.Size = Marshal.SizeOf(deviceAttributes)

                result = HidD_GetAttributes(HIDHandle, deviceAttributes)

                'the Hex Values for VendorID give a "56A" on the tablet PC
                'so we must use the true values
                '   MsgBox(deviceAttributes.VendorID.ToString)
                '  MsgBox(deviceAttributes.ProductID.ToString)

                If (deviceAttributes.VendorID = MagtekVendorID Or deviceAttributes.VendorID = IDTechVendorID) Then 'And deviceAttributes.ProductID = MyProductID Then
                    myDeviceDetected = True
                End If
                '        If (Hex(deviceAttributes.VendorID) = MyVendorID) And (Hex(deviceAttributes.ProductID) = MyProductID) Then
                '    myDeviceDetected = True
                '    End If

            End If

            MemberIndex += 1

        Loop Until lastDevice = True Or myDeviceDetected = True

        result = SetupDiDestroyDeviceInfoList(deviceInfoSet)

        If myDeviceDetected = True Then
            '   FindtheHid = True
            '            Dim result As Boolean
            '     result = RegisterHidNotification(devicePathName)
            '   ADD    registerHidNotification

            GetDeviceCapabilities()

            ReadHandle = CreateFile(devicePathName, GENERIC_READ Or GENERIC_WRITE, FILE_SHARE_READ Or FILE_SHARE_WRITE, security, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, 0)

            PrepareForOverlappedTransfer()
            '    tmrCardRead.Enabled = True

            AddHandler tmrCardRead.Tick, AddressOf tmrCardRead_Tick
            tmrCardRead.Start()

            '***
            ReDim ReadBuffer(Capabilities.InputReportByteLength - 1)

        End If

    End Sub

    Friend Sub tmrCardRead_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) ' Handles tmrCardRead.Tick

        Call ReadAndWriteToDevice()

    End Sub

    Friend Sub ReadAndWriteToDevice()
        '     testCounter += 1
        Try
            ReadReport()
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub GetDeviceCapabilities()

        '******************************************************************************
        'HidD_GetPreparsedData
        'Returns: a pointer to a buffer containing information about the device's capabilities.
        'Requires: A handle returned by CreateFile.
        'There's no need to access the buffer directly,
        'but HidP_GetCaps and other API functions require a pointer to the buffer.
        '******************************************************************************

        Dim ppData(29) As Byte
        Dim ppDataString As String

        'Preparsed Data is a pointer to a routine-allocated buffer.
        result = HidD_GetPreparsedData(HIDHandle, PreparsedData)

        'Copy the data at PreparsedData into a byte array.

        Try
            ppDataString = System.Convert.ToBase64String(ppData)
        Catch exp As System.ArgumentNullException
            MsgBox("PreparsedData array is null.")
            Return
        End Try

        '******************************************************************************
        'HidP_GetCaps
        'Find out the device's capabilities.
        'For standard devices such as joysticks, you can find out the specific
        'capabilities of the device.
        'For a custom device where the software knows what the device is capable of,
        'this call is unneeded.
        'Requires: The pointer to a buffer containing the information.
        'The pointer is returned by HidD_GetPreparsedData.
        'Returns: a Capabilites structure containing the information.
        '******************************************************************************

        result = HidP_GetCaps(PreparsedData, Capabilities)

        '******************************************************************************
        'HidP_GetValueCaps
        'Returns a buffer containing an array of HidP_ValueCaps structures.
        'Each structure defines the capabilities of one value.
        'This application doesn't use this data.
        '******************************************************************************

        'This is a guess. The byte array holds the structures.
        Dim ValueCaps(1023) As Byte

        result = HidP_GetValueCaps(HidP_Input, ValueCaps(0), Capabilities.NumberInputValueCaps, PreparsedData)

        '       Call DisplayResultOfAPICall("HidP_GetValueCaps")

        'To use this data, copy the ValueCaps byte array into an array of structures.

        'Free the buffer reserved by HidD_GetPreparsedData
        result = HidD_FreePreparsedData(PreparsedData)
        '     Call DisplayResultOfAPICall("HidD_FreePreparsedData")

    End Sub


    Private Sub PrepareForOverlappedTransfer()

        '******************************************************************************
        'CreateEvent
        'Creates an event object for the overlapped structure used with ReadFile.
        'Requires a security attributes structure or null,
        'Manual Reset = False (The system automatically resets the state to nonsignaled 
        'after a waiting thread has been released.),
        'Initial state = True (signaled),
        'and event object name (optional)
        'Returns a handle to the event object.
        '******************************************************************************

        '     Dim tempEvent As Integer
        '    tempEvent = EventObject
        '   EventObject = CreateEvent(tempEvent, CInt(False), CInt(True), "")

        If EventObject = 0 Then
            EventObject = CreateEvent(0, CInt(False), CInt(True), "")
        End If

        'Set the members of the overlapped structure.
        HIDOverlapped.Offset = 0
        HIDOverlapped.OffsetHigh = 0
        HIDOverlapped.hEvent = EventObject
        '     result = Nothing

    End Sub

    Private Sub ReadReport()
        'Read data from the device.

        'Dim Count As Object
        '      Dim NumberOfBytesRead As Integer
        'Allocate a buffer for the report.
        'Byte 0 is the report ID.
        '       ReDim ReadBuffer(Capabilities.InputReportByteLength - 1) ' As Byte
        '      Dim UBoundReadBuffer As Short

        '******************************************************************************
        'ReadFile
        'Returns: the report in ReadBuffer.
        'Requires: a device handle returned by CreateFile
        '(for overlapped I/O, CreateFile must be called with FILE_FLAG_OVERLAPPED),
        'the Input report length in bytes returned by HidP_GetCaps,
        'and an overlapped structure whose hEvent member is set to an event object.
        '******************************************************************************

        '      Dim nonHexValue As String
        '     Dim ByteValue As String
        'The ReadBuffer array begins at 0, so subtract 1 from the number of bytes.

        '***      ReDim ReadBuffer(Capabilities.InputReportByteLength - 1)
        'Scroll to the bottom of the list box.
        'lstResults.SelectedIndex = lstResults.Items.Count - 1

        'Do an overlapped ReadFile.
        'The function returns immediately, even if the data hasn't been received yet.
        result = ReadFile _
            (ReadHandle, _
            ReadBuffer(0), _
            CInt(Capabilities.InputReportByteLength), _
            NumberOfBytesRead, _
            HIDOverlapped)

        '******************************************************************************
        'WaitForSingleObject
        'Used with overlapped ReadFile.
        'Returns when ReadFile has received the requested amount of data or on timeout.
        'Requires an event object created with CreateEvent
        'and a timeout value in milliseconds.
        '******************************************************************************
        result = WaitForSingleObject(EventObject, 80)   '1000)    '30000 is a 30 second timeout


        'Find out if ReadFile completed or timeout.
        Select Case result
            Case WAIT_OBJECT_0

                'ReadFile has completed
                '          Beep()
                '          PrepareForOverlappedTransfer()
                '          Exit Select

                Dim track2Length As Integer
                Dim count As Integer

                newPayment = New DataSet_Builder.Payment

                track2Length = ReadBuffer(4) + ReadBuffer(5)
                '               Track1(4)    Track2(5)
                Dim ascii As New ASCIIEncoding
                Dim ByteChar As Char
                Dim nextTrack2Count As Integer
                Dim makeLowerCase As Boolean
                Dim spaceFoundInName As Boolean
                Dim possibleFirstName As String
                Dim noFirstName As Boolean
                Dim firstNameString As String
                Dim lastChrSpace As Boolean

                Try
                    For count = 0 To UBound(ReadBuffer) '8 To UBound(ReadBuffer) ' 336
                        If Chr(ReadBuffer(count)) = "%" Then 'Track1 start
                            newPayment.Track1Found = True
                            nextTrack2Count = count + 2
                            Exit For
                        End If
                    Next

                    '   AccountNumber from Track1
                    For count = nextTrack2Count To UBound(ReadBuffer) ' 336
                        If Chr(ReadBuffer(count)) = "^" Then
                            nextTrack2Count = count + 1
                            Exit For
                        End If
                        newPayment.Track1 = newPayment.Track1 & Chr(ReadBuffer(count))
                        '        newPayment.AccountNumber = newPayment.AccountNumber & Chr(ReadBuffer(count))
                    Next

                    '        For count = 8 To UBound(ReadBuffer) ' 336
                    'If Chr(ReadBuffer(count)) = "^" Then
                    '       nextTrack2Count = count + 1
                    '      Exit For
                    '     End If
                    '    Next

                    newPayment.Name = " "     'place the space between names first

                    '   lastName
                    For count = nextTrack2Count To UBound(ReadBuffer) ' 336
                        If Chr(ReadBuffer(count)) = "^" Then
                            ' this is the end of the name, so looks like no first name
                            ' We only get here if there was no seperator "/" and name is all together
                            nextTrack2Count = count + 1
                            noFirstName = True
                            Exit For
                        End If
                        If Chr(ReadBuffer(count)) = "/" Then
                            nextTrack2Count = count + 1
                            Exit For
                        End If

                        If Chr(ReadBuffer(count)) = " " And lastChrSpace = True Then

                        Else
                            If makeLowerCase = True Then
                                newPayment.LastName = newPayment.LastName & Chr(ReadBuffer(count)).ToString.ToLower
                            Else
                                newPayment.LastName = newPayment.LastName & Chr(ReadBuffer(count))
                                makeLowerCase = True
                            End If
                            '2 if thens below in case there is no seperator between first and last name
                            If spaceFoundInName = True Then
                                possibleFirstName = possibleFirstName & Chr(ReadBuffer(count))
                            End If
                        End If

                        If Chr(ReadBuffer(count)) = " " Then
                            possibleFirstName = ""
                            spaceFoundInName = True
                            lastChrSpace = True
                        Else
                            lastChrSpace = False
                        End If
                    Next
                    If newPayment.LastName Is Nothing Then '.Length = 0 Then
                        'this will be the default last name
                        newPayment.LastName = "Customer"
                    End If
                    If newPayment.LastName.Length = 0 Then
                        'just in case, will never be true
                        newPayment.LastName = "Customer"
                    End If
                    newPayment.Name = newPayment.LastName
                    makeLowerCase = False


                    '   FirstName
                    If noFirstName = True Then
                        newPayment.FirstName = possibleFirstName
                    Else
                        For count = nextTrack2Count To UBound(ReadBuffer) ' 336
                            If Chr(ReadBuffer(count)) = "^" Then
                                nextTrack2Count = count + 1
                                Exit For
                            End If
                            If Not Chr(ReadBuffer(count)) = " " Then
                                '444        firstNameString = firstNameString & Chr(ReadBuffer(count))
                                If makeLowerCase = True Then
                                    newPayment.FirstName = newPayment.FirstName & Chr(ReadBuffer(count)).ToString.ToLower
                                Else
                                    newPayment.FirstName = newPayment.FirstName & Chr(ReadBuffer(count))
                                    makeLowerCase = True
                                End If
                            End If
                        Next
                    End If

                    If newPayment.FirstName Is Nothing Then
                        'this will be the default last name
                        newPayment.FirstName = ""
                    End If
                    '          If newPayment.FirstName.Length = 0 Then
                    '   newPayment.FirstName = ""
                    '  End If

                    firstNameString = newPayment.FirstName
                    If firstNameString.Length > 0 Then
                        newPayment.Name = newPayment.Name & ", " & firstNameString
                    End If

                    '***************   end of Track 1, ends at ?

                    '   finds the start of Track2 info, starts at ;
                    For count = nextTrack2Count To UBound(ReadBuffer) ' 336
                        If Chr(ReadBuffer(count)) = ";" Then
                            nextTrack2Count = count + 1
                            Exit For
                        End If
                    Next

                    '   AccountNumber
                    For count = nextTrack2Count To UBound(ReadBuffer) ' 336
                        newPayment.Track2 = newPayment.Track2 & Chr(ReadBuffer(count))
                        'this will add the =

                        If Chr(ReadBuffer(count)) = "=" Then
                            nextTrack2Count = count + 1
                            Exit For
                        End If
                        newPayment.AccountNumber = newPayment.AccountNumber & Chr(ReadBuffer(count))
                    Next

                    '   ExpYear
                    For count = nextTrack2Count To (nextTrack2Count + 1)  'reads 2 characters
                        newPayment.Track2 = newPayment.Track2 & Chr(ReadBuffer(count))
                        newPayment.ExpDate = newPayment.ExpDate & Chr(ReadBuffer(count))
                    Next
                    nextTrack2Count += 2

                    '   ExpMonth
                    Dim monthString As String
                    For count = nextTrack2Count To (nextTrack2Count + 1)  'reads 2 characters
                        newPayment.Track2 = newPayment.Track2 & Chr(ReadBuffer(count))
                        monthString = monthString & Chr(ReadBuffer(count))
                    Next
                    nextTrack2Count += 2

                    newPayment.ExpDate = monthString & newPayment.ExpDate

                    '444     If ValidateExpDate(newPayment.ExpDate) = False Then Exit Sub

                    For count = nextTrack2Count To UBound(ReadBuffer) ' 336

                        If Chr(ReadBuffer(count)) = "?" Then
                            If Not track2Length = 0 Then

                                cardSwiped = True
                                '     tmrCardRead.Dispose()
                                '     RemoveHandler tmrCardRead.Tick, AddressOf tmrCardRead_Tick
                                '    tmrCardRead.Stop()

                                newPayment.Swiped = True


                                Dim tabString As String

                                '444              If IsNewTab = True Then
                                tabString = newPayment.LastName
                                If newPayment.FirstName.Length > 0 Then
                                    tabString = tabString + ", " & newPayment.FirstName
                                End If

                                RaiseEvent EnteringTabNameInKeyboard(tabString)
                                '444End If

                                Try
                                    SuccessfulCardReadProcessing(newPayment, tabString)
                                    '        RaiseEvent CardReadSuccessful(newPayment)
                                Catch ex As Exception
                                    RaiseEvent CardReadFailed()
                                    CancelIo(ReadHandle)
                                    Exit Sub
                                End Try

                            Else
                                MsgBox("Card Swipe does Not Read Correctly")
                            End If
                            '           result = CloseHandle(HIDHandle)
                            '           result = CloseHandle(ReadHandle)
                            '           PrepareForOverlappedTransfer()
                            '           result = 258
                            Exit For
                        End If
                        newPayment.Track2 = newPayment.Track2 & Chr(ReadBuffer(count))
                    Next
                    If track2Length = 0 Then
                        MsgBox("Card Swipe does Not Read Correctly")
                    End If

                    '        MsgBox("ReadFile completed successfully.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                    '     RemoveHandler tmrCardRead.Tick, AddressOf tmrCardRead_Tick
                    '     tmrCardRead.Stop()
                    '     ReadBuffer = Nothing
                    RaiseEvent CardReadFailed()
                    CancelIo(ReadHandle)    '444

                    '        PrepareForOverlappedTransfer()
                    '        result = Nothing
                    Exit Sub

                End Try

            Case WAIT_TIMEOUT

                result = CancelIo(ReadHandle)
                '        result = CloseHandle(HIDHandle)
                '        result = CloseHandle(ReadHandle)
                '        ReadHandle = CreateFile(devicePathName, GENERIC_READ Or GENERIC_WRITE, FILE_SHARE_READ Or FILE_SHARE_WRITE, security, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, 0)
                '        PrepareForOverlappedTransfer()
                '        CancelIo()
            Case Else

                CancelIo(ReadHandle)

        End Select

    End Sub

    Private Sub SuccessfulCardReadProcessing(ByRef newPayment As Payment, ByVal tabString As String)

        If newPayment.Track2.Length > 0 Then
            newPayment.SwipeCode = CryOutloud.Encrypt(newPayment.Track2, "test")
            If newPayment.SwipeCode.Length > 20 Then
                newPayment.SwipeCode = newPayment.SwipeCode.ToString.Substring(0, 20)
            End If
        End If

        If IsNewTab = False Then
            ' we do this so managers can open a Tab under their credit card 
            ' under Seating_EnterTab
            Dim emp As Employee
            For Each emp In SwipeCodeEmployees
                If emp.SwipeCode = newPayment.SwipeCode Then
                    RaiseEvent ManagementCardSwiped(emp)
                    Exit Sub
                End If
            Next
        End If

        newPayment.PaymentTypeName = GenerateOrderTables.DetermineCreditCardName(newPayment.AccountNumber)
        If newPayment.PaymentTypeName = "" Then
            'need to also determine if management swipe card
            '   RaiseEvent CardReadFailed()
            MsgBox("Card Type is not recognized")
            CancelIo(ReadHandle)
            Exit Sub
        End If
        If ValidateExpDate(newPayment.ExpDate) = False Then
            ' only attempt validate if this is a paying credit card
            Exit Sub
        End If

        If IsNewTab = True Then
            If OpenNewTab(-999, tabString) = False Then
                RaiseEvent CardReadFailed()
                CancelIo(ReadHandle)
                Exit Sub
            End If
        End If

        If Not currentTable Is Nothing Then
            If Not ActiveScreen = "Login" And Not currentTable.ExperienceNumber = Nothing Then  'Not ActiveScreen = "TableScreen" And 
                Try
                    newPayment.experienceNumber = currentTable.ExperienceNumber
                    newPayment.PaymentTypeID = DetermineCreditCardID(newPayment.PaymentTypeName)

                    newPayment.SpiderAcct = CreateAccountNumber(newPayment) '.LastName, newPayment.AccountNumber)
                    GenerateOrderTables.CreateTabAcctPlaceInExperience(newPayment)
                    If newPayment.PaymentTypeName = "MPS Gift" Then
                        'this may change PaymentTypeID to -97
                        PopulateGiftPaymentInfo()
                    End If

                    'payment collection is for loyalty and gift card (2 seperate things)
                    GenerateOrderTables.AddPaymentToCollection(newPayment) 'was not in CustomerCardRead in Tab_Screen
                    RaiseEvent CardReadSuccessful(newPayment)
                Catch ex As Exception
                    RaiseEvent CardReadFailed()
                    CancelIo(ReadHandle)
                End Try
            End If
        End If


    End Sub

    Private Sub PopulateGiftPaymentInfo()
        Dim authStatus As String

        ' we are checking balance on all Gift Cards
        'we have not created a DataRow, so everything is different (must be done w/ newPayment)
        If Not newPayment.PaymentTypeID = -97 Then
            'if -97, the status of this gift card already determined
            '    authStatus = DetermineGiftCardBalance(newPayment)
            authStatus = GenerateOrderTables.GiftCardTransaction(Nothing, newPayment, "Balance")
            If authStatus = "MPS Gift Card" Then
                MsgBox(authStatus)
                Exit Sub
            End If
            If authStatus = "Account Not Issued" Then
                newPayment.PaymentTypeID = -97
                newPayment.PaymentTypeName = "Issue Gift"
                'we need to make sure the payment amount is negative
                'enter amount of gift on number pad
                newPayment.Purchase = -50.0
                newPayment.PaymentFlag = "Issue"
            Else
                '        If newPayment.Balance < newPayment.Purchase Then
                'newPayment.Purchase = newPayment.Balance
                '       MsgBox("Balance remaining on card before purchase: " & newPayment.Balance)
                'otherwise defaults to purchase
                '  End If

                If _giftAddingAmount = True Then
                    ' this is Return, which is adding more money back on Gift Card
                    ' we record same as issue
                    newPayment.PaymentTypeID = -97
                    newPayment.PaymentTypeName = "Increase Gift"
                    newPayment.PaymentFlag = "Issue"
                    If newPayment.Purchase > 0 Then
                        newPayment.Purchase *= -1
                    End If
                    RaiseEvent RetruningGiftAddingAmountToFalse() '  ReturnGiftCardAddToFalse()
                Else
                    newPayment.PaymentFlag = "Gift"
                End If
            End If
        End If

    End Sub

    Public Sub Shutdown()
        '     tmrCardRead.Stop()
        '    RemoveHandler tmrCardRead.Tick, AddressOf tmrCardRead_Tick

        result = CancelIo(ReadHandle)
        result = CloseHandle(HIDHandle)
        result = CloseHandle(ReadHandle)
        '      CancelIo(ReadHandle)
        '      CloseHandle(HIDHandle)
        ''      CloseHandle(ReadHandle)
        '       UnregisterDeviceNotification(deviceNotificationHandle)
        HidD_FreePreparsedData(PreparsedData)

        '     Finalize()

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()

    End Sub

    Public Declare Function HidD_GetAttributes Lib "hid.dll" _
           (ByVal HidDeviceObject As Integer, _
           ByRef Attributes As HIDD_ATTRIBUTES) _
           As Integer

    'Declared as a function for consistency,
    'but returns nothing. (Ignore the returned value.)
    Public Declare Function HidD_GetHidGuid Lib "hid.dll" _
        (ByRef HidGuid As System.Guid) _
        As Integer


    Public Declare Auto Function SetupDiGetClassDevs Lib "setupapi.dll" _
         (ByRef ClassGuid As System.Guid, _
         ByVal Enumerator As String, _
         ByVal hwndParent As Integer, _
         ByVal Flags As Integer) _
         As IntPtr

    Public Declare Function SetupDiDestroyDeviceInfoList Lib "setupapi.dll" _
           (ByVal DeviceInfoSet As IntPtr) _
           As Integer


    Public Declare Function SetupDiEnumDeviceInterfaces Lib "setupapi.dll" _
          (ByVal DeviceInfoSet As IntPtr, _
          ByVal DeviceInfoData As Integer, _
          ByRef InterfaceClassGuid As System.Guid, _
          ByVal MemberIndex As Integer, _
          ByRef DeviceInterfaceData As SP_DEVICE_INTERFACE_DATA) _
          As Integer

    Public Declare Auto Function SetupDiGetDeviceInterfaceDetail Lib "setupapi.dll" _
          (ByVal DeviceInfoSet As IntPtr, _
          ByRef DeviceInterfaceData As SP_DEVICE_INTERFACE_DATA, _
          ByVal DeviceInterfaceDetailData As IntPtr, _
          ByVal DeviceInterfaceDetailDataSize As Integer, _
          ByRef RequiredSize As Integer, _
          ByVal DeviceInfoData As IntPtr) _
          As Boolean

    Public Declare Function CancelIo Lib "kernel32" _
          (ByVal hFile As Integer) _
          As Integer

    Public Declare Function CloseHandle Lib "kernel32" _
             (ByVal hObject As Integer) _
             As Integer


    Public Declare Auto Function CreateFile Lib "kernel32" _
          (ByVal lpFileName As String, _
          ByVal dwDesiredAccess As Integer, _
          ByVal dwShareMode As Integer, _
          ByRef lpSecurityAttributes As SECURITY_ATTRIBUTES, _
          ByVal dwCreationDisposition As Integer, _
          ByVal dwFlagsAndAttributes As Integer, _
          ByVal hTemplateFile As Integer) _
          As Integer

    Public Declare Function ReadFile Lib "kernel32" _
         (ByVal hFile As Integer, _
         ByRef lpBuffer As Byte, _
         ByVal nNumberOfBytesToRead As Integer, _
         ByRef lpNumberOfBytesRead As Integer, _
         ByRef lpOverlapped As OVERLAPPED) _
         As Integer

    Public Declare Function WaitForSingleObject Lib "kernel32" _
           (ByVal hHandle As Integer, _
           ByVal dwMilliseconds As Integer) _
           As Integer

    Public Declare Auto Function CreateEvent Lib "kernel32" _
          (ByVal SecurityAttributes As Integer, _
          ByVal bManualReset As Integer, _
          ByVal bInitialState As Integer, _
          ByVal lpName As String) As Integer

    Public Declare Function HidD_GetPreparsedData Lib "hid.dll" _
          (ByVal HidDeviceObject As Integer, _
          ByRef PreparsedData As IntPtr) _
          As Integer


    Public Declare Function HidP_GetCaps Lib "hid.dll" _
    (ByVal PreparsedData As IntPtr, _
    ByRef Capabilities As HIDP_CAPS) _
    As Integer

    Public Declare Function HidP_GetValueCaps Lib "hid.dll" _
        (ByVal ReportType As Short, _
        ByRef ValueCaps As Byte, _
        ByRef ValueCapsLength As Short, _
        ByVal PreparsedData As IntPtr) _
        As Integer

    Public Declare Function HidD_FreePreparsedData Lib "hid.dll" _
          (ByRef PreparsedData As IntPtr) _
          As Integer

    Public Declare Auto Function UnregisterDeviceNotification Lib "user32.dll" _
         (ByVal Handle As IntPtr) _
     As Boolean

End Class
