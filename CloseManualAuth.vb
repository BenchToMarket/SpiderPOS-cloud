
'Imports VB = Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.io
Imports System.Xml
Imports System.Xml.Serialization



Public Class CloseManualAuth
    Inherits System.Windows.Forms.UserControl

    Dim dsi As New DSICLIENTXLib.DSICLientX

    '  Dim dddi As New DSICLIENTXLib.DSICLientXClass
    Dim auth As New TStream


    '   HID device
    Const MyVendorID As Short = 801
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


    Dim activeAccountNo As Boolean
    Dim activeExpDate As Boolean
    Dim activeAmount As Boolean
    Dim activeTip As Boolean

    Dim authTest As Boolean

    Dim testCounter As Integer

    Dim info As DataSet_Builder.Information_UC
    Dim cardSwiped As Boolean

    Private Shared _closeAuthAmount As PreAuthAmountClass
    Private Shared _closeAuthTransaction As PreAuthTransactionClass
    Private Shared _closeAuthAccount As AccountClass
   

    Friend Shared Property CloseAuthAmount() As PreAuthAmountClass
        Get
            Return _closeAuthAmount
        End Get
        Set(ByVal Value As PreAuthAmountClass)
            _closeAuthAmount = Value
        End Set
    End Property

    Friend Shared Property CloseAuthTransaction() As PreAuthTransactionClass
        Get
            Return _closeAuthTransaction
        End Get
        Set(ByVal Value As PreAuthTransactionClass)
            _closeAuthTransaction = Value
        End Set
    End Property

    Friend Shared Property CloseAuthAccount() As AccountClass
        Get
            Return _closeAuthAccount
        End Get
        Set(ByVal Value As AccountClass)
            _closeAuthAccount = Value
        End Set
    End Property



    Friend WithEvents paymentKeyboard As DataSet_Builder.KeyBoard_UC




#Region " Windows Form Designer generated code "

    Public Sub New(ByRef authamount As PreAuthAmountClass, ByRef authTransaction As PreAuthTransactionClass, ByVal cardSwipedDatabaseInfo As Boolean)
        MyBase.New()

        _closeAuthAmount = New PreAuthAmountClass
        _closeAuthTransaction = New PreAuthTransactionClass
        _closeAuthAccount = New AccountClass

        _closeAuthAmount = authamount
        _closeAuthTransaction = authTransaction
        cardSwiped = cardSwipedDatabaseInfo

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        InitializeOther()



    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblCLoseManual As System.Windows.Forms.Label
    Friend WithEvents lblManualAcctNumberDetail As System.Windows.Forms.Label
    Friend WithEvents lblManualExpDateDetail As System.Windows.Forms.Label
    Friend WithEvents lblManualAmount As System.Windows.Forms.Label
    Friend WithEvents lblManualTip As System.Windows.Forms.Label
    Friend WithEvents lblManualAmountDetail As System.Windows.Forms.Label
    Friend WithEvents lblManualTipDetail As System.Windows.Forms.Label
    Friend WithEvents chkCloseOverrideDup As System.Windows.Forms.CheckBox
    Friend WithEvents NumberPadCreditAuth As DataSet_Builder.NumberPadLarge
    Friend WithEvents lblManualExpDate As System.Windows.Forms.Label
    Friend WithEvents lblManualAccount As System.Windows.Forms.Label
    Friend WithEvents lblManualCheck As System.Windows.Forms.Label
    Friend WithEvents lblanualName As System.Windows.Forms.Label
    Friend WithEvents lblManualNameOnCardDetail As System.Windows.Forms.Label
    Friend WithEvents lblManualAuthCode As System.Windows.Forms.Label
    Friend WithEvents lblDeclineReason As System.Windows.Forms.Label
    Friend WithEvents lblManualAuthCodeDetail As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAuth As System.Windows.Forms.Button
    Friend WithEvents btnPreAuth As System.Windows.Forms.Button
    Friend WithEvents tmrCardRead As System.Windows.Forms.Timer
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents AxDSICLientX1 As AxDSICLIENTXLib.AxDSICLientX
    Friend WithEvents AxDSICLientX2 As AxDSICLIENTXLib.AxDSICLientX
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(CloseManualAuth))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.lblManualAuthCodeDetail = New System.Windows.Forms.Label
        Me.lblDeclineReason = New System.Windows.Forms.Label
        Me.lblManualAuthCode = New System.Windows.Forms.Label
        Me.lblManualNameOnCardDetail = New System.Windows.Forms.Label
        Me.lblanualName = New System.Windows.Forms.Label
        Me.lblManualCheck = New System.Windows.Forms.Label
        Me.NumberPadCreditAuth = New DataSet_Builder.NumberPadLarge
        Me.chkCloseOverrideDup = New System.Windows.Forms.CheckBox
        Me.lblManualTipDetail = New System.Windows.Forms.Label
        Me.lblManualAmountDetail = New System.Windows.Forms.Label
        Me.lblManualTip = New System.Windows.Forms.Label
        Me.lblManualAmount = New System.Windows.Forms.Label
        Me.lblManualExpDateDetail = New System.Windows.Forms.Label
        Me.lblManualAcctNumberDetail = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnAuth = New System.Windows.Forms.Button
        Me.btnPreAuth = New System.Windows.Forms.Button
        Me.lblManualExpDate = New System.Windows.Forms.Label
        Me.lblManualAccount = New System.Windows.Forms.Label
        Me.lblCLoseManual = New System.Windows.Forms.Label
        Me.AxDSICLientX2 = New AxDSICLIENTXLib.AxDSICLientX
        Me.tmrCardRead = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.AxDSICLientX2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightSlateGray
        Me.Panel1.Controls.Add(Me.ListView1)
        Me.Panel1.Controls.Add(Me.lblManualAuthCodeDetail)
        Me.Panel1.Controls.Add(Me.lblDeclineReason)
        Me.Panel1.Controls.Add(Me.lblManualAuthCode)
        Me.Panel1.Controls.Add(Me.lblManualNameOnCardDetail)
        Me.Panel1.Controls.Add(Me.lblanualName)
        Me.Panel1.Controls.Add(Me.lblManualCheck)
        Me.Panel1.Controls.Add(Me.NumberPadCreditAuth)
        Me.Panel1.Controls.Add(Me.chkCloseOverrideDup)
        Me.Panel1.Controls.Add(Me.lblManualTipDetail)
        Me.Panel1.Controls.Add(Me.lblManualAmountDetail)
        Me.Panel1.Controls.Add(Me.lblManualTip)
        Me.Panel1.Controls.Add(Me.lblManualAmount)
        Me.Panel1.Controls.Add(Me.lblManualExpDateDetail)
        Me.Panel1.Controls.Add(Me.lblManualAcctNumberDetail)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.btnAuth)
        Me.Panel1.Controls.Add(Me.btnPreAuth)
        Me.Panel1.Controls.Add(Me.lblManualExpDate)
        Me.Panel1.Controls.Add(Me.lblManualAccount)
        Me.Panel1.Controls.Add(Me.lblCLoseManual)
        Me.Panel1.Controls.Add(Me.AxDSICLientX2)
        Me.Panel1.Location = New System.Drawing.Point(40, 24)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(608, 400)
        Me.Panel1.TabIndex = 0
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.ListView1.Location = New System.Drawing.Point(256, 352)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(352, 40)
        Me.ListView1.TabIndex = 21
        Me.ListView1.View = System.Windows.Forms.View.List
        Me.ListView1.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Width = 25
        '
        'lblManualAuthCodeDetail
        '
        Me.lblManualAuthCodeDetail.Location = New System.Drawing.Point(176, 216)
        Me.lblManualAuthCodeDetail.Name = "lblManualAuthCodeDetail"
        Me.lblManualAuthCodeDetail.Size = New System.Drawing.Size(120, 16)
        Me.lblManualAuthCodeDetail.TabIndex = 20
        '
        'lblDeclineReason
        '
        Me.lblDeclineReason.Location = New System.Drawing.Point(32, 240)
        Me.lblDeclineReason.Name = "lblDeclineReason"
        Me.lblDeclineReason.Size = New System.Drawing.Size(264, 24)
        Me.lblDeclineReason.TabIndex = 19
        '
        'lblManualAuthCode
        '
        Me.lblManualAuthCode.Location = New System.Drawing.Point(56, 216)
        Me.lblManualAuthCode.Name = "lblManualAuthCode"
        Me.lblManualAuthCode.Size = New System.Drawing.Size(112, 16)
        Me.lblManualAuthCode.TabIndex = 18
        Me.lblManualAuthCode.Text = "Authorization Code:"
        '
        'lblManualNameOnCardDetail
        '
        Me.lblManualNameOnCardDetail.Location = New System.Drawing.Point(120, 80)
        Me.lblManualNameOnCardDetail.Name = "lblManualNameOnCardDetail"
        Me.lblManualNameOnCardDetail.Size = New System.Drawing.Size(152, 16)
        Me.lblManualNameOnCardDetail.TabIndex = 17
        '
        'lblanualName
        '
        Me.lblanualName.Location = New System.Drawing.Point(24, 80)
        Me.lblanualName.Name = "lblanualName"
        Me.lblanualName.Size = New System.Drawing.Size(88, 16)
        Me.lblanualName.TabIndex = 16
        Me.lblanualName.Text = "Name on Card:"
        '
        'lblManualCheck
        '
        Me.lblManualCheck.Location = New System.Drawing.Point(24, 48)
        Me.lblManualCheck.Name = "lblManualCheck"
        Me.lblManualCheck.Size = New System.Drawing.Size(192, 16)
        Me.lblManualCheck.TabIndex = 15
        Me.lblManualCheck.Text = "Check #:      "
        '
        'NumberPadCreditAuth
        '
        Me.NumberPadCreditAuth.BackColor = System.Drawing.Color.SlateGray
        Me.NumberPadCreditAuth.DecimalUsed = False
        Me.NumberPadCreditAuth.IntegerNumber = 0
        Me.NumberPadCreditAuth.Location = New System.Drawing.Point(336, 16)
        Me.NumberPadCreditAuth.Name = "NumberPadCreditAuth"
        Me.NumberPadCreditAuth.NumberString = Nothing
        Me.NumberPadCreditAuth.NumberTotal = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumberPadCreditAuth.Size = New System.Drawing.Size(240, 368)
        Me.NumberPadCreditAuth.TabIndex = 14
        '
        'chkCloseOverrideDup
        '
        Me.chkCloseOverrideDup.Location = New System.Drawing.Point(168, 352)
        Me.chkCloseOverrideDup.Name = "chkCloseOverrideDup"
        Me.chkCloseOverrideDup.Size = New System.Drawing.Size(96, 16)
        Me.chkCloseOverrideDup.TabIndex = 13
        Me.chkCloseOverrideDup.Text = "Override Dup"
        '
        'lblManualTipDetail
        '
        Me.lblManualTipDetail.Location = New System.Drawing.Point(120, 176)
        Me.lblManualTipDetail.Name = "lblManualTipDetail"
        Me.lblManualTipDetail.Size = New System.Drawing.Size(112, 16)
        Me.lblManualTipDetail.TabIndex = 12
        '
        'lblManualAmountDetail
        '
        Me.lblManualAmountDetail.Location = New System.Drawing.Point(120, 152)
        Me.lblManualAmountDetail.Name = "lblManualAmountDetail"
        Me.lblManualAmountDetail.Size = New System.Drawing.Size(136, 16)
        Me.lblManualAmountDetail.TabIndex = 11
        '
        'lblManualTip
        '
        Me.lblManualTip.Location = New System.Drawing.Point(24, 176)
        Me.lblManualTip.Name = "lblManualTip"
        Me.lblManualTip.Size = New System.Drawing.Size(80, 16)
        Me.lblManualTip.TabIndex = 10
        Me.lblManualTip.Text = "Gratuity:         $"
        '
        'lblManualAmount
        '
        Me.lblManualAmount.Location = New System.Drawing.Point(24, 152)
        Me.lblManualAmount.Name = "lblManualAmount"
        Me.lblManualAmount.Size = New System.Drawing.Size(80, 16)
        Me.lblManualAmount.TabIndex = 9
        Me.lblManualAmount.Text = "Amount:         $"
        '
        'lblManualExpDateDetail
        '
        Me.lblManualExpDateDetail.Location = New System.Drawing.Point(120, 128)
        Me.lblManualExpDateDetail.Name = "lblManualExpDateDetail"
        Me.lblManualExpDateDetail.Size = New System.Drawing.Size(168, 16)
        Me.lblManualExpDateDetail.TabIndex = 8
        '
        'lblManualAcctNumberDetail
        '
        Me.lblManualAcctNumberDetail.Location = New System.Drawing.Point(120, 104)
        Me.lblManualAcctNumberDetail.Name = "lblManualAcctNumberDetail"
        Me.lblManualAcctNumberDetail.Size = New System.Drawing.Size(184, 16)
        Me.lblManualAcctNumberDetail.TabIndex = 7
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(16, 344)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 40)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        '
        'btnAuth
        '
        Me.btnAuth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAuth.Location = New System.Drawing.Point(176, 280)
        Me.btnAuth.Name = "btnAuth"
        Me.btnAuth.Size = New System.Drawing.Size(88, 40)
        Me.btnAuth.TabIndex = 5
        Me.btnAuth.Text = "Auth"
        '
        'btnPreAuth
        '
        Me.btnPreAuth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreAuth.Location = New System.Drawing.Point(56, 280)
        Me.btnPreAuth.Name = "btnPreAuth"
        Me.btnPreAuth.Size = New System.Drawing.Size(88, 40)
        Me.btnPreAuth.TabIndex = 4
        Me.btnPreAuth.Text = "Pre-Auth"
        '
        'lblManualExpDate
        '
        Me.lblManualExpDate.Location = New System.Drawing.Point(24, 128)
        Me.lblManualExpDate.Name = "lblManualExpDate"
        Me.lblManualExpDate.Size = New System.Drawing.Size(80, 16)
        Me.lblManualExpDate.TabIndex = 3
        Me.lblManualExpDate.Text = "Exp Date:"
        '
        'lblManualAccount
        '
        Me.lblManualAccount.Location = New System.Drawing.Point(24, 104)
        Me.lblManualAccount.Name = "lblManualAccount"
        Me.lblManualAccount.Size = New System.Drawing.Size(80, 16)
        Me.lblManualAccount.TabIndex = 2
        Me.lblManualAccount.Text = "Acoount #:"
        '
        'lblCLoseManual
        '
        Me.lblCLoseManual.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCLoseManual.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCLoseManual.Location = New System.Drawing.Point(32, 16)
        Me.lblCLoseManual.Name = "lblCLoseManual"
        Me.lblCLoseManual.Size = New System.Drawing.Size(280, 16)
        Me.lblCLoseManual.TabIndex = 1
        Me.lblCLoseManual.Text = "Credit Card Authorization"
        Me.lblCLoseManual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AxDSICLientX2
        '
        Me.AxDSICLientX2.ContainingControl = Me
        Me.AxDSICLientX2.Enabled = True
        Me.AxDSICLientX2.Location = New System.Drawing.Point(264, 264)
        Me.AxDSICLientX2.Name = "AxDSICLientX2"
        Me.AxDSICLientX2.OcxState = CType(resources.GetObject("AxDSICLientX2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxDSICLientX2.Size = New System.Drawing.Size(100, 50)
        Me.AxDSICLientX2.TabIndex = 1
        '
        'tmrCardRead
        '
        Me.tmrCardRead.Interval = 950
        '
        'CloseManualAuth
        '
        Me.BackColor = System.Drawing.Color.SlateBlue
        Me.Controls.Add(Me.Panel1)
        Me.Name = "CloseManualAuth"
        Me.Size = New System.Drawing.Size(688, 448)
        Me.Panel1.ResumeLayout(False)
        CType(Me.AxDSICLientX2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub InitializeOther()

        NumberPadCreditAuth.ChangeLabelDimensions()

        If cardSwiped = True Then
            Me.lblManualAccount.Enabled = False
            Me.lblManualAcctNumberDetail.Enabled = False
        End If



        Me.lblManualCheck.Text = "Check #       " & CloseAuthTransaction.InvoiceNo
        Me.lblManualAmountDetail.Text = CloseAuthAmount.Purchase




        '       Me.lblManualNameOnCardDetail.Text = authPayment.Name
        '       Me.lblManualAcctNumberDetail.Text = authPayment.Account
        '       Me.lblManualExpDateDetail.Text = authPayment.ExpDate
        '
        ''        Me.lblManualTipDetail.Text = authPayment.Gratuity
        '        Me.lblManualAuthCodeDetail.Text = authPayment.AuthCode''
        '
        ''        If Me.lblManualAuthCodeDetail.Text.Length = 6 Then
        '        authTest = True
        '        End If


    End Sub

    Private Sub btnPreAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreAuth.Click

        Dim preAuthReady As String

        '       If cardSwiped = False Then
        '      MsgBox("Please Swipe Credit Card")
        '     Exit Sub
        '    End If


        preAuthReady = TestPreAuthSwiped()
        If preAuthReady = "Swiped" Then
            PreAuth()
        ElseIf preAuthReady = "Keyed" Then
            PreAuth()
        End If

    End Sub


    Private Sub StartPreAuthCapture()


    End Sub

    Private Function TestPreAuthSwiped()

        Dim isReady As String

        If CloseAuthAmount.Purchase <= 0 Then
            '       isReady = False
            '        info = New DataSet_Builder.Information_UC("Auth Amount must be greater than $0.00")
            '       info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            '      Me.Controls.Add(info)
            '     info.BringToFront()
            MsgBox("Auth Amount must be greater than $0.00")
            Return isReady
        End If

        '      If CloseAuthAmount.Authorize <= 0 Then
        '     CloseAuthAmount.Authorize = Format(CloseAuthAmount.Purchase * 1.2, "#####.00")
        '    End If

        '    CloseAuthAmount.Gratuity = "0.00"

        If CloseAuthAccount.Track2 = Nothing Then       'CloseAuthAccount.Track2.Length = 0 Then
            If Me.lblManualAcctNumberDetail.Text.Length > 0 And Me.lblManualExpDateDetail.Text.Length = 4 Then
                CloseAuthAccount.AcctNo = Me.lblManualAcctNumberDetail.Text
                CloseAuthAccount.ExpDate = Me.lblManualExpDateDetail.Text
                isReady = "Keyed"
                Return isReady
            End If
            '      isReady = False
            MsgBox("Card Swipe Does Not Read Correctly")
            Return isReady
        End If

        isReady = "Swiped"
        Return isReady


    End Function

    Private Sub PreAuth()

        '      Dim serializer As New XmlSerializer(GetType(TStream))
        '     Dim writer As New StreamWriter("preauth.xml")


        Dim mpsPreAuth As New TStream
        Dim mpsPreAuthTransaction As New PreAuthTransactionClass

        mpsPreAuthTransaction = CloseAuthTransaction
        mpsPreAuthTransaction.Account = CloseAuthAccount
        mpsPreAuthTransaction.Amount = CloseAuthAmount
        '   mpsPreAuthTransaction.Amount.Gratuity = Nothing

        mpsPreAuth.Transaction = mpsPreAuthTransaction


        '      serializer.Serialize(writer, mpsPreAuth)
        '     writer.Close()


        Dim output As New StringWriter(New StringBuilder)
        Dim s As New XmlSerializer(GetType(TStream))
        s.Serialize(output, mpsPreAuth)


        ParseXMLResponse(output.ToString)
        '        TestingDSI(output.ToString)


        '       Console.Write(output)
        '        MsgBox(output.ToString)
        '      Dim p As New StringBuilder(mpsPreAuth.ToXML)
        '     p.Append(mpsPreAuth.ToXML)


    End Sub

    Public Sub XMLSerializeString222() 'old

        Dim serializer As New XmlSerializer(GetType(TStream))

    End Sub

    Public Function WriteXMLString222(ByVal fs As String)  'old

        Dim xmlString As String

        Try
            ' Create an instance of StreamReader to read from a file.
            Dim sr As StreamReader = New StreamReader(fs)
            Dim line As String

            ' Read and display the lines from the file until the end 
            ' of the file is reached.
            Do
                line = sr.ReadLine()

                Console.WriteLine(line)
            Loop Until line Is Nothing
            sr.Close()
        Catch E As Exception
            ' Let the user know what went wrong.
            Console.WriteLine("The file could not be read:")
            Console.WriteLine(E.Message)
        End Try

        Return xmlString

    End Function

    Private Sub PreAuthKeyed222()      'auth

        Dim serializer As New XmlSerializer(GetType(TStream))
        Dim writer As New StreamWriter("preauth.xml")

        Dim mpsPreAuth As New TStream
        Dim mpsPreAuthTransaction As New PreAuthTransactionClass

        mpsPreAuthTransaction = CloseAuthTransaction
        mpsPreAuthTransaction.Account = CloseAuthAccount
        mpsPreAuthTransaction.Amount = CloseAuthAmount

        mpsPreAuth.Transaction = mpsPreAuthTransaction

        serializer.Serialize(writer, mpsPreAuth)
        writer.Close()




    End Sub

    Private Sub CloseManualAuth_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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
                If (Hex(deviceAttributes.VendorID) = MyVendorID) And (Hex(deviceAttributes.ProductID) = MyProductID) Then
                    myDeviceDetected = True
                End If

                '           MsgBox(Hex(deviceAttributes.VendorID))
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
            Me.tmrCardRead.Enabled = True
        End If



    End Sub

    Private Sub PaymentInfoEntered(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumberPadCreditAuth.NumberEntered

        If activeAccountNo = True Then
            Me.lblManualAcctNumberDetail.Text = Me.NumberPadCreditAuth.NumberString
            InactivateLabels()
            activeExpDate = True
            Me.lblManualExpDate.BackColor = c9


            Me.NumberPadCreditAuth.NumberString = Me.lblManualExpDateDetail.Text
            Me.NumberPadCreditAuth.ShowNumberString()

        ElseIf activeExpDate = True Then
            If Me.NumberPadCreditAuth.NumberString.Length = 4 Then
                Me.lblManualExpDateDetail.Text = Me.NumberPadCreditAuth.NumberString
                InactivateLabels()
                activeAmount = True
                Me.lblManualAmount.BackColor = c9

                Me.NumberPadCreditAuth.DecimalUsed = True
                Me.NumberPadCreditAuth.NumberString = Me.lblManualAmountDetail.Text
                Me.NumberPadCreditAuth.ShowNumberString()

            Else : MsgBox("Expiration Date sould be in 4 digits: MMYY")
            End If


        ElseIf activeAmount = True Then
            Me.lblManualAmountDetail.Text = Me.NumberPadCreditAuth.NumberString
            InactivateLabels()

        End If

    End Sub

    Private Sub InactivateLabels()

        Me.tmrCardRead.Enabled = False
        activeAccountNo = False
        activeExpDate = False
        activeAmount = False
        activeTip = False

        Me.lblanualName.BackColor = c10
        Me.lblManualAccount.BackColor = c10
        Me.lblManualExpDate.BackColor = c10
        Me.lblManualAmount.BackColor = c10
        Me.lblManualTip.BackColor = c10



    End Sub


    Private Sub lblManualAcctNumberDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblManualAcctNumberDetail.Click

        EnterAccountNumber()

    End Sub

    Private Sub lblManualAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblManualAccount.Click

        EnterAccountNumber()

    End Sub

    Private Sub EnterAccountNumber()

        InactivateLabels()
        activeAccountNo = True
        Me.lblManualAccount.BackColor = c9

        Me.NumberPadCreditAuth.DecimalUsed = False

        NumberPadCreditAuth.NumberString = Me.lblManualAcctNumberDetail.Text
        NumberPadCreditAuth.ShowNumberString()
        NumberPadCreditAuth.Focus()


    End Sub



    Private Sub lblanualName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblanualName.Click
        EnterNameOnCard()
    End Sub

    Private Sub lblManualNameOnCardDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblManualNameOnCardDetail.Click
        EnterNameOnCard()
    End Sub


    Private Sub EnterNameOnCard()

        InactivateLabels()

        paymentKeyboard = New DataSet_Builder.KeyBoard_UC
        paymentKeyboard.Location = New Point(10, 0)
        Me.Controls.Add(paymentKeyboard)
        paymentKeyboard.BringToFront()


    End Sub


    Private Sub NameEntered(ByVal sender As Object, ByVal e As System.EventArgs) Handles paymentKeyboard.StringEntered


        Me.lblManualNameOnCardDetail.Text = paymentKeyboard.EnteredString
        paymentKeyboard.Dispose()


    End Sub



    Private Sub lblManualExpDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblManualExpDate.Click
        EnterExpDate()
    End Sub

    Private Sub lblManualExpDateDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblManualExpDateDetail.Click
        EnterExpDate()
    End Sub

    Private Sub EnterExpDate()
        InactivateLabels()
        activeExpDate = True
        Me.lblManualExpDate.BackColor = c9

        Me.NumberPadCreditAuth.DecimalUsed = False

        NumberPadCreditAuth.NumberString = Me.lblManualExpDateDetail.Text
        NumberPadCreditAuth.ShowNumberString()
        NumberPadCreditAuth.Focus()

    End Sub

    Private Sub lblManualTip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblManualTip.Click
        EnterGratuity()
    End Sub

    Private Sub lblManualTipDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblManualTipDetail.Click
        EnterGratuity()
    End Sub

    Private Sub EnterGratuity()
        InactivateLabels()
        activeTip = True
        Me.lblManualTip.BackColor = c9

        Me.NumberPadCreditAuth.DecimalUsed = True

        NumberPadCreditAuth.NumberString = Me.lblManualTipDetail.Text
        NumberPadCreditAuth.ShowNumberString()
        NumberPadCreditAuth.Focus()


    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()

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



    Private Sub tmrCardRead_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrCardRead.Tick

        Call ReadAndWriteToDevice()

    End Sub

    Private Sub ReadAndWriteToDevice()
        testCounter += 1

        ReadReport()
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

        If EventObject = 0 Then
            EventObject = CreateEvent(0, CInt(False), CInt(True), "")
        End If
        '    MsgBox(EventObject)


        'Set the members of the overlapped structure.
        HIDOverlapped.Offset = 0
        HIDOverlapped.OffsetHigh = 0
        HIDOverlapped.hEvent = EventObject
    End Sub

    Private Sub ReadReport()
        'Read data from the device.

        'Dim Count As Object
        Dim count As Integer
        Dim NumberOfBytesRead As Integer
        'Allocate a buffer for the report.
        'Byte 0 is the report ID.
        Dim ReadBuffer() As Byte
        Dim UBoundReadBuffer As Short

        '******************************************************************************
        'ReadFile
        'Returns: the report in ReadBuffer.
        'Requires: a device handle returned by CreateFile
        '(for overlapped I/O, CreateFile must be called with FILE_FLAG_OVERLAPPED),
        'the Input report length in bytes returned by HidP_GetCaps,
        'and an overlapped structure whose hEvent member is set to an event object.
        '******************************************************************************

        Dim nonHexValue As String
        Dim ByteValue As String
        'The ReadBuffer array begins at 0, so subtract 1 from the number of bytes.
        ReDim ReadBuffer(Capabilities.InputReportByteLength - 1)
        'Scroll to the bottom of the list box.
        'lstResults.SelectedIndex = lstResults.Items.Count - 1

        '       MsgBox("2")

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
        result = WaitForSingleObject(EventObject, 1000)    '30000 is a 30 second timeout


        Dim track2Length As Integer

        track2Length = ReadBuffer(4)
        Dim ascii As New ASCIIEncoding
        Dim ByteChar As Char
        Dim nextTrack2Count As Integer
        '      Dim firstName As String
        '     Dim lastName As String
        '   think about how middle name and business name are on cards


        'Find out if ReadFile completed or timeout.
        Select Case result
            Case WAIT_OBJECT_0
                'ReadFile has completed
                '          txtTest = ascii.GetString(ReadBuffer, 117, track2Length - 1)

                For count = 8 To 336
                    If Chr(ReadBuffer(count)) = "^" Then
                        nextTrack2Count = count + 1
                        Exit For
                    End If
                Next

                _closeAuthAccount.Name = " "     'place the space between names first

                '   lastName
                For count = nextTrack2Count To 336
                    If Chr(ReadBuffer(count)) = "/" Then
                        nextTrack2Count = count + 1
                        Exit For
                    End If
                    _closeAuthAccount.Name = CloseAuthAccount.Name & Chr(ReadBuffer(count))
                Next


                '   FirstName
                Dim firstNameString As String
                For count = nextTrack2Count To 336
                    If Chr(ReadBuffer(count)) = "^" Then
                        nextTrack2Count = count + 1
                        Exit For
                    End If
                    firstNameString = firstNameString & Chr(ReadBuffer(count))
                Next
                _closeAuthAccount.Name = firstNameString & CloseAuthAccount.Name
                Me.lblManualNameOnCardDetail.Text = CloseAuthAccount.Name

                '   finds the start of Track2 info
                For count = nextTrack2Count To 336
                    If Chr(ReadBuffer(count)) = ";" Then
                        nextTrack2Count = count + 1
                        Exit For
                    End If
                Next

                '   AccountNumber
                For count = nextTrack2Count To 336
                    _closeAuthAccount.Track2 = CloseAuthAccount.Track2 & Chr(ReadBuffer(count)) 'this will add the =
                    If Chr(ReadBuffer(count)) = "=" Then
                        nextTrack2Count = count + 1
                        Exit For
                    End If
                    Me.lblManualAcctNumberDetail.Text = lblManualAcctNumberDetail.Text & Chr(ReadBuffer(count))
                Next
                '       MsgBox(CloseAuthAccount.Track2)


                '   ExpYear
                '         Me.lblManualExpDateDetail.Text = " / "
                For count = nextTrack2Count To (nextTrack2Count + 1)  'reads 2 characters
                    _closeAuthAccount.Track2 = CloseAuthAccount.Track2 & Chr(ReadBuffer(count))
                    Me.lblManualExpDateDetail.Text = lblManualExpDateDetail.Text & Chr(ReadBuffer(count))
                Next
                nextTrack2Count += 2


                '   ExpMonth
                Dim monthString As String
                For count = nextTrack2Count To (nextTrack2Count + 1)  'reads 2 characters
                    _closeAuthAccount.Track2 = CloseAuthAccount.Track2 & Chr(ReadBuffer(count))
                    monthString = monthString & Chr(ReadBuffer(count))
                Next
                Me.lblManualExpDateDetail.Text = monthString & lblManualExpDateDetail.Text
                nextTrack2Count += 2


                For count = nextTrack2Count To 336
                    If Chr(ReadBuffer(count)) = "?" Then
                        If CloseAuthAccount.Track2.Length > 0 Then
                            cardSwiped = True
                            '     tmrCardRead.Dispose()
                            Me.tmrCardRead.Enabled = False
                            '    MsgBox(CloseAuthAccount.Track2)
                        Else
                            MsgBox("Card Swipe does Not Read Correctly")
                        End If

                        Exit Sub
                    End If
                    _closeAuthAccount.Track2 = CloseAuthAccount.Track2 & Chr(ReadBuffer(count))
                Next
                '        MsgBox("ReadFile completed successfully.")



            Case WAIT_TIMEOUT

                GetDeviceCapabilities()
                ReadHandle = CreateFile(devicePathName, GENERIC_READ Or GENERIC_WRITE, FILE_SHARE_READ Or FILE_SHARE_WRITE, security, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, 0)
                PrepareForOverlappedTransfer()

            Case Else

        End Select




    End Sub



    Private Sub PopulateCardInformation(ByVal track2 As String)



    End Sub

    Private Sub TestingDSI(ByVal XMLString As String)


        Dim resp As String

        dsi.ServerIPConfig("x1.mercurypay.com;x2.mercurypay.com;b1.backuppay.com;b2.backuppay.com", 0)
        resp = dsi.ProcessTransaction(XMLString, 0, "", "")


        '       Dim writer As New StreamWriter("preauthrResponse.txt")
        '      writer.Write(resp)
        '     writer.Close()



        '    ParseXMLResponse(resp)

    End Sub


    Private Sub ParseXMLResponse(ByVal resp As String)

        Dim reader As New XmlTextReader(New StringReader(resp))
        Dim someError As Boolean
        Dim isPreAuth As Boolean

      
        Try
            Do Until reader.EOF = True
                reader.Read()
                reader.MoveToContent()
                If reader.NodeType = XmlNodeType.Element Then
                    MsgBox(reader.Name)
                    Select Case reader.Name

                        Case "DSIXReturnCode"
                            If String.Compare(reader.ReadInnerXml, "000000", True) <> 0 Then
                                '   false, do not honor case (is not case sensitive)
                                '   a zero means the same (therefore this is not the same)
                                '   there is sometype of error
                                someError = True


                            End If
                            MsgBox(reader.ReadInnerXml, , "OperatorID")

                        Case "CmdStatus"
                            Select Case reader.ReadInnerXml
                                Case "Approved"

                                Case "Declined"

                                Case "Success"

                                Case "Error"

                            End Select

                        Case "TextResponse"
                            If someError = True Then
                                MsgBox(reader.ReadInnerXml)
                                Exit Sub
                            Else

                            End If

                        Case "UserTraceData"

                            '                         **************************************
                            '                                 Transaction Response
                            '                         **************************************

                        Case "TranCode"
                            If String.Compare(reader.ReadInnerXml, "PreAuth", True) = 0 Then
                                isPreAuth = True
                            End If

                        Case "AuthCode"
                            If isPreAuth = True Then
                                '   place authcode in database
                            End If

                        Case "AcqRefData"
                            If isPreAuth = True Then
                                '   place acqRefData in database
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





        Exit Sub



        Do Until reader.EOF = True
            reader.Read()
            reader.MoveToContent()
            If reader.NodeType = XmlNodeType.Element Then
                MsgBox(reader.Name)
                Select Case reader.Name

                    Case "OperatorID"
                        MsgBox(reader.ReadInnerXml, , "OperatorID")
                    Case "Name"
                        MsgBox(reader.ReadInnerXml, , "Name")
                End Select
            End If
        Loop


        Try
            While reader.Read()


                Select Case reader.NodeType
                    Case XmlNodeType.Element
                        MsgBox(reader.Name, , "Element")
                    Case XmlNodeType.Text
                        MsgBox(reader.Value, , "Text")
                End Select
            End While
        Catch ex As Exception

        Finally
            If Not reader Is Nothing Then
                reader.Close()
            End If

        End Try




        reader.WhitespaceHandling = WhitespaceHandling.None

        reader.MoveToContent()
        reader.Read()

        reader.MoveToContent()
        reader.Read()
        MsgBox(reader.Value, , "The Ref No is: ")
        '        reader.MoveToContent()

        MsgBox(reader.Name)             'MerchantID
        MsgBox(reader.ReadInnerXml)     '494901
        MsgBox(reader.Value)




        '      If reader.HasAttributes Then
        '      Console.WriteLine("Attributes of <" & reader.Name & ">")
        Dim i As Integer
        For i = 0 To reader.AttributeCount - 1
            reader.MoveToAttribute(i)
            MsgBox(" {0}={1}", reader.Name, reader.Value)
        Next
        '     End If
        '
        '    reader.MoveToAttribute("OperatorID")
        '   MsgBox(reader.Value)







    End Sub

End Class


