Imports System.Runtime.InteropServices
Imports System.Text
Imports System.io


Public Class ReadCreditOld
    Implements IDisposable


    '    Dim dsi As New DSICLIENTXLib.DSICLientX

    '   Dim dddi As New DSICLIENTXLib.DSICLientXClass
    Dim auth As New TStream
    '   Friend WithEvents tmrCardRead As System.Windows.Forms.Timer


    '   HID device
    Const MyVendorID As Short = 2049        '801 (this is the Hex VendorID)
    Const MyProductID As Short = 2
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

    '    Dim testCounter As Integer
    Dim cardSwiped As Boolean

    Dim NumberOfBytesRead As Integer
    'Allocate a buffer for the report.
    'Byte 0 is the report ID.
    Dim ReadBuffer() As Byte

    '   Private Shared _closeAuthAmount As PreAuthAmountClass
    '   Private Shared _closeAuthTransaction As PreAuthTransactionClass
    '   Private Shared _closeAuthAccount As AccountClass

    Friend newPayment As DataSet_Builder.Payment


    '    Friend Shared Property CloseAuthAmount() As PreAuthAmountClass
    '        Get
    '            Return _closeAuthAmount
    '       End Get
    '       Set(ByVal Value As PreAuthAmountClass)
    ''           _closeAuthAmount = Value
    '       End Set
    '  End Property

    '  Friend Shared Property CloseAuthTransaction() As PreAuthTransactionClass
    '      Get
    '          Return _closeAuthTransaction
    ''      End Get
    '      Set(ByVal Value As PreAuthTransactionClass)
    '          _closeAuthTransaction = Value
    ''     End Set
    'End Property

    '   Friend Shared Property CloseAuthAccount() As AccountClass
    '       Get
    '           Return _closeAuthAccount
    '      End Get
    ''      Set(ByVal Value As AccountClass)
    '          _closeAuthAccount = Value
    '     End Set
    'End Property


    Dim _payName As String

    Friend Property PayName() As String
        Get
            Return _payName
        End Get
        Set(ByVal Value As String)
            _payName = Value
        End Set
    End Property


    Private components As System.ComponentModel.IContainer


    Event CardReadSuccessful(ByRef newPayment As DataSet_Builder.Payment)
    Event CardReadFailed()



    Sub New()
        '   MyBase.new()
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

        '      _closeAuthAmount = New PreAuthAmountClass
        '      _closeAuthTransaction = New PreAuthTransactionClass
        '      _closeAuthAccount = New AccountClass

        '    Me.tmrCardRead = New System.Windows.Forms.Timer
        '     AddHandler tmrCardRead.Tick, AddressOf tmrCardRead_Tick
        tmrCardRead.Interval = 100
        '     tmrCardRead.Start()

        HidGuid = Guid.Empty
        lastDevice = False

        security.lpSecurityDescriptor = 0
        security.bInheritHandle = CInt(True)
        security.nLength = (Len(security))

        result = HidD_GetHidGuid(HidGuid)

        deviceInfoSet = SetupDiGetClassDevs(HidGuid, vbNullString, 0, DIGCF_PRESENT Or DIGCF_DEVICEINTERFACE)       '16) '2 Or 16)
        '       MsgBox(deviceInfoSet.GetHashCode)

        MemberIndex = 0

        Do

            myDeviceInterfaceData.cbSize = Marshal.SizeOf(myDeviceInterfaceData)

            result = SetupDiEnumDeviceInterfaces(deviceInfoSet, 0, HidGuid, MemberIndex, myDeviceInterfaceData)
            If result = 0 Then lastDevice = True

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

                Marshal.FreeHGlobal(detailDataBuffer)   'free's memory allocated earlier

                HIDHandle = CreateFile(devicePathName, GENERIC_READ Or GENERIC_WRITE, FILE_SHARE_READ Or FILE_SHARE_WRITE, security, OPEN_EXISTING, 0, 0)

                deviceAttributes.Size = Marshal.SizeOf(deviceAttributes)

                result = HidD_GetAttributes(HIDHandle, deviceAttributes)

                'the Hex Values for VendorID give a "56A" on the tablet PC
                'so we must use the true values
                If ((deviceAttributes.VendorID) = MyVendorID) And ((deviceAttributes.ProductID) = MyProductID) Then
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
        '    MsgBox(EventObject)


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

        '    MsgBox("2")

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
                '           Beep()
                '          PrepareForOverlappedTransfer()
                '          Exit Select

                Dim track2Length As Integer
                Dim count As Integer

                newPayment = New DataSet_Builder.Payment

                track2Length = ReadBuffer(4)
                Dim ascii As New ASCIIEncoding
                Dim ByteChar As Char
                Dim nextTrack2Count As Integer
                '      Dim firstName As String
                '     Dim lastName As String
                '   think about how middle name and business name are on cards

                '          Dim txtTest As String
                '       txtTest = ascii.GetString(ReadBuffer, 117, track2Length - 1)
                '      MsgBox(txtTest)

                ''                Dim testTrack As String
                '                For count = 8 To 336    '8 To UBound(ReadBuffer) ' 336
                '                testTrack = testTrack & Chr(ReadBuffer(count))
                '                Next
                '               MsgBox(count)
                '              MsgBox(testTrack)
                '               MsgBox(track2Length)
                '        MsgBox(UBound(ReadBuffer))

                Try

                    For count = 8 To UBound(ReadBuffer) ' 336
                        If Chr(ReadBuffer(count)) = "^" Then
                            nextTrack2Count = count + 1
                            Exit For
                        End If
                    Next

                    newPayment.Name = " "     'place the space between names first

                    '   lastName
                    For count = nextTrack2Count To UBound(ReadBuffer) ' 336
                        If Chr(ReadBuffer(count)) = "/" Then
                            nextTrack2Count = count + 1
                            Exit For
                        End If
                        newPayment.Name = newPayment.Name & Chr(ReadBuffer(count))
                    Next

                    '   FirstName
                    Dim firstNameString As String
                    For count = nextTrack2Count To UBound(ReadBuffer) ' 336
                        If Chr(ReadBuffer(count)) = "^" Then
                            nextTrack2Count = count + 1
                            Exit For
                        End If
                        firstNameString = firstNameString & Chr(ReadBuffer(count))
                    Next
                    newPayment.Name = firstNameString & newPayment.Name
                    '***             Me.lblManualNameOnCardDetail.Text = CloseAuthAccount.Name
                    '          MsgBox(newPayment.Name)

                    '   finds the start of Track2 info
                    For count = nextTrack2Count To UBound(ReadBuffer) ' 336
                        If Chr(ReadBuffer(count)) = ";" Then
                            nextTrack2Count = count + 1
                            Exit For
                        End If
                    Next

                    '   AccountNumber
                    For count = nextTrack2Count To UBound(ReadBuffer) ' 336
                        newPayment.Track2 = newPayment.Track2 & Chr(ReadBuffer(count)) 'this will add the =
                        If Chr(ReadBuffer(count)) = "=" Then
                            nextTrack2Count = count + 1
                            Exit For
                        End If
                        newPayment.AccountNumber = newPayment.AccountNumber & Chr(ReadBuffer(count))
                        '***                Me.lblManualAcctNumberDetail.Text = lblManualAcctNumberDetail.Text & Chr(ReadBuffer(count))
                    Next
                    '            MsgBox(newPayment.Track2)

                    newPayment.PaymentTypeName = GenerateOrderTables.DetermineCreditCardName(newPayment.AccountNumber)
                  
                    If newPayment.PaymentTypeName = "" Then
                        RaiseEvent CardReadFailed()
                        '         PrepareForOverlappedTransfer()
                        '        result = 258
                        '            result = CloseHandle(HIDHandle)
                        '           result = CloseHandle(ReadHandle)
                        Exit Try
                    End If

                    '         Dim firstDigit As String
                    '         firstDigit = (newPayment.Track2.Substring(0, 1))'
                    '
                    ''                    If firstDigit = "3" Then
                    '                    newPayment.PaymentTypeName = "AMEX"
                    '                    ElseIf firstDigit = "4" Then
                    '                        newPayment.PaymentTypeName = "VISA"
                    '                   ElseIf firstDigit = "5" Then
                    '                        newPayment.PaymentTypeName = "M/C"
                    '                   ElseIf firstDigit = "6" Then
                    '                       newPayment.PaymentTypeName = "DCVR"
                    '                  Else
                    '
                    '           RaiseEvent CardReadFailed()
                    '          Exit Sub
                    '         End If


                    '   ExpYear
                    '         Me.lblManualExpDateDetail.Text = " / "
                    For count = nextTrack2Count To (nextTrack2Count + 1)  'reads 2 characters
                        newPayment.Track2 = newPayment.Track2 & Chr(ReadBuffer(count))
                        newPayment.ExpDate = newPayment.ExpDate & Chr(ReadBuffer(count))
                        '***                   Me.lblManualExpDateDetail.Text = lblManualExpDateDetail.Text & Chr(ReadBuffer(count))
                    Next
                    nextTrack2Count += 2


                    '   ExpMonth
                    Dim monthString As String
                    For count = nextTrack2Count To (nextTrack2Count + 1)  'reads 2 characters
                        newPayment.Track2 = newPayment.Track2 & Chr(ReadBuffer(count))
                        monthString = monthString & Chr(ReadBuffer(count))
                    Next
                    newPayment.ExpDate = monthString & newPayment.ExpDate
                    '***             Me.lblManualExpDateDetail.Text = monthString & lblManualExpDateDetail.Text
                    nextTrack2Count += 2

                    For count = nextTrack2Count To UBound(ReadBuffer) ' 336

                        If Chr(ReadBuffer(count)) = "?" Then
                            If Not track2Length = 0 Then 'newPayment.Track2.Length > 0 Then

                                cardSwiped = True
                                '     tmrCardRead.Dispose()
                                '     RemoveHandler tmrCardRead.Tick, AddressOf tmrCardRead_Tick
                                '    tmrCardRead.Stop()

                                '         MsgBox(newPayment.Track2)
                                newPayment.Swiped = True

                                RaiseEvent CardReadSuccessful(newPayment)

                            Else
                                MsgBox("Card Swipe does Not Read Correctly")
                            End If

                            '           result = CloseHandle(HIDHandle)
                            '          result = CloseHandle(ReadHandle)

                            '       PrepareForOverlappedTransfer()
                            '         result = 258
                            Exit For
                        End If
                        newPayment.Track2 = newPayment.Track2 & Chr(ReadBuffer(count))
                    Next
                    '        MsgBox("ReadFile completed successfully.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                    '       RemoveHandler tmrCardRead.Tick, AddressOf tmrCardRead_Tick
                    '      tmrCardRead.Stop()

                    '               ReadBuffer = Nothing
                    RaiseEvent CardReadFailed()
                    '        PrepareForOverlappedTransfer()
                    '       result = Nothing
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
