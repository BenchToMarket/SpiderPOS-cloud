Imports DataSet_Builder

Public Class Tab_Screen
    Inherits System.Windows.Forms.UserControl

    '  Dim WithEvents readAuthTab As New ReadCredit(False)
    Dim WithEvents readAuthTab222 As ReadCredit

    '    Dim tempPayment As New DataSet_Builder.Payment
    Dim currentCustomer As New Customer
    Dim currentTabInfo As TabInfo
    Dim activeLabel As Label
    Dim activeLabelString As String
    Dim tabRow As DataRow
    Dim tempMethodUse As String

    Public isDisplaying222 As Boolean
    Public StartInSearch As String
    Public attemptedToEdit As Boolean
    Public enteringNewTab As Boolean = False

    Dim tabDoubleClickTimer As Timer
    Dim tabTimerActive As Boolean

    Public currentSearchBy As String
    '   choices are:
    '   "TabID"
    '   "Phone"
    '   "Name"
    '   "Account"
    '
    Dim currentSearchLocation As String
    '   choices are:
    '   "Location"
    '   "Company"

    Dim CustomerCurrencyMan As CurrencyManager

    Dim _tempTabID As Int64
    Dim _tempTabName As String
    Dim _tempAccountPhone As String
    Dim _tempAccountNumber As String
    Dim _hasAddress As Boolean

    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents lblTabEmail As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents lblNewTabEmail As System.Windows.Forms.Label

    Friend Property TempTabID() As Int64
        Get
            Return _tempTabID
        End Get
        Set(ByVal Value As Int64)
            _tempTabID = Value
        End Set
    End Property

    Friend Property TempTabName()
        Get
            Return _tempTabName
        End Get
        Set(ByVal Value)
            _tempTabName = Value
        End Set
    End Property

    Friend Property TempAccountNumber() As String
        Get
            Return _tempAccountNumber
        End Get
        Set(ByVal value As String)
            _tempAccountNumber = value
        End Set
    End Property

    Friend Property HasAddress() As Boolean
        Get
            Return _hasAddress
        End Get
        Set(ByVal Value As Boolean)
            _hasAddress = Value
        End Set
    End Property

    Event TabScreenDisposing()
    Event SelectedReOrder(ByVal dt As DataTable, ByVal tabTestNeeded As Boolean)
    Event SelectedNewOrder()
    Event ChangedMethodUse()

#Region " Windows Form Designer generated code "

    Public Sub New() 'ByVal _startInSearch As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        StartInSearch = "Phone"
        BindData()

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

    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblTabAddress1 As System.Windows.Forms.Label
    Friend WithEvents lblTabAddress2 As System.Windows.Forms.Label
    Friend WithEvents lblTabCity As System.Windows.Forms.Label
    Friend WithEvents lblTabState As System.Windows.Forms.Label
    Friend WithEvents lblTabPostalCode As System.Windows.Forms.Label
    Friend WithEvents lblTabPhone1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblTabExt2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblTabPhone2 As System.Windows.Forms.Label
    Friend WithEvents lblTabSpecial As System.Windows.Forms.Label
    Friend WithEvents lblTabCrossRoads As System.Windows.Forms.Label
    Friend WithEvents lblTabDeliveryZone As System.Windows.Forms.Label
    Friend WithEvents chkTabVIP As System.Windows.Forms.CheckBox
    Friend WithEvents chkTabDoNotDeliver As System.Windows.Forms.CheckBox
    Friend WithEvents pnlTabInfo As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblTabAcctNumber As System.Windows.Forms.Label
    Friend WithEvents TabKeyboard As DataSet_Builder.KeyBoard_UC_Black
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblTabSearchLocation As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TabPageSearch As System.Windows.Forms.TabPage
    Friend WithEvents lblTabLastName As System.Windows.Forms.Label
    Friend WithEvents PreviousOrder_UC1 As DataSet_Builder.PreviousOrder_UC
    Friend WithEvents btnTabEnterNew As System.Windows.Forms.Button
    Friend WithEvents pnlNewTabInfo As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblNewTabAcctNumber As System.Windows.Forms.Label
    Friend WithEvents chkNewTabDoNotDeliver As System.Windows.Forms.CheckBox
    Friend WithEvents chkNewTabVIP As System.Windows.Forms.CheckBox
    Friend WithEvents lblNewTabPhone2 As System.Windows.Forms.Label
    Friend WithEvents lblNewTabSpecial As System.Windows.Forms.Label
    Friend WithEvents lblNewTabCrossRoads As System.Windows.Forms.Label
    Friend WithEvents lblNewTabDeliveryZone As System.Windows.Forms.Label
    Friend WithEvents lblNewTabPhone1 As System.Windows.Forms.Label
    Friend WithEvents lblNewTabPostalCode As System.Windows.Forms.Label
    Friend WithEvents lblNewTabState As System.Windows.Forms.Label
    Friend WithEvents lblNewTabCity As System.Windows.Forms.Label
    Friend WithEvents lblNewTabAddress2 As System.Windows.Forms.Label
    Friend WithEvents lblNewTabAddress1 As System.Windows.Forms.Label
    Friend WithEvents lblNewTabLastName As System.Windows.Forms.Label
    Friend WithEvents lblNewTabFirstName As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblNewTabExt2 As System.Windows.Forms.Label
    Friend WithEvents lblTabFirstName As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents lblTabExt1 As System.Windows.Forms.Label
    Friend WithEvents btnSearchAcctNum As System.Windows.Forms.Button
    Friend WithEvents btnSearchLastName As System.Windows.Forms.Button
    Friend WithEvents btnSearchPhone As System.Windows.Forms.Button
    Friend WithEvents btnLocationStore As System.Windows.Forms.Button
    Friend WithEvents btnEditEntry As System.Windows.Forms.Button
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents pnlEdit As System.Windows.Forms.Panel
    Friend WithEvents btnLocationAll As System.Windows.Forms.Button
    Friend WithEvents btnCloseSave As System.Windows.Forms.Button
    Friend WithEvents lblNewTabExt1 As System.Windows.Forms.Label
    Friend WithEvents lblMethodUse As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnSearchAcctNum = New System.Windows.Forms.Button
        Me.btnSearchLastName = New System.Windows.Forms.Button
        Me.btnSearchPhone = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabKeyboard = New DataSet_Builder.KeyBoard_UC_Black
        Me.pnlTabInfo = New System.Windows.Forms.Panel
        Me.Label33 = New System.Windows.Forms.Label
        Me.lblTabEmail = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.pnlNewTabInfo = New System.Windows.Forms.Panel
        Me.Label35 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.lblNewTabEmail = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblNewTabFirstName = New System.Windows.Forms.Label
        Me.lblNewTabAcctNumber = New System.Windows.Forms.Label
        Me.chkNewTabDoNotDeliver = New System.Windows.Forms.CheckBox
        Me.chkNewTabVIP = New System.Windows.Forms.CheckBox
        Me.lblNewTabExt2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblNewTabPhone2 = New System.Windows.Forms.Label
        Me.lblNewTabSpecial = New System.Windows.Forms.Label
        Me.lblNewTabCrossRoads = New System.Windows.Forms.Label
        Me.lblNewTabDeliveryZone = New System.Windows.Forms.Label
        Me.lblNewTabExt1 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblNewTabPhone1 = New System.Windows.Forms.Label
        Me.lblNewTabPostalCode = New System.Windows.Forms.Label
        Me.lblNewTabState = New System.Windows.Forms.Label
        Me.lblNewTabCity = New System.Windows.Forms.Label
        Me.lblNewTabAddress2 = New System.Windows.Forms.Label
        Me.lblNewTabAddress1 = New System.Windows.Forms.Label
        Me.lblNewTabLastName = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.lblTabFirstName = New System.Windows.Forms.Label
        Me.lblTabAcctNumber = New System.Windows.Forms.Label
        Me.chkTabDoNotDeliver = New System.Windows.Forms.CheckBox
        Me.chkTabVIP = New System.Windows.Forms.CheckBox
        Me.lblTabExt2 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblTabPhone2 = New System.Windows.Forms.Label
        Me.lblTabSpecial = New System.Windows.Forms.Label
        Me.lblTabCrossRoads = New System.Windows.Forms.Label
        Me.lblTabDeliveryZone = New System.Windows.Forms.Label
        Me.lblTabExt1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblTabPhone1 = New System.Windows.Forms.Label
        Me.lblTabPostalCode = New System.Windows.Forms.Label
        Me.lblTabState = New System.Windows.Forms.Label
        Me.lblTabCity = New System.Windows.Forms.Label
        Me.lblTabAddress2 = New System.Windows.Forms.Label
        Me.lblTabAddress1 = New System.Windows.Forms.Label
        Me.lblTabLastName = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPageSearch = New System.Windows.Forms.TabPage
        Me.btnCloseSave = New System.Windows.Forms.Button
        Me.pnlEdit = New System.Windows.Forms.Panel
        Me.btnEditEntry = New System.Windows.Forms.Button
        Me.Label32 = New System.Windows.Forms.Label
        Me.btnTabEnterNew = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnLocationAll = New System.Windows.Forms.Button
        Me.btnLocationStore = New System.Windows.Forms.Button
        Me.lblTabSearchLocation = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.PreviousOrder_UC1 = New DataSet_Builder.PreviousOrder_UC
        Me.lblMethodUse = New System.Windows.Forms.Label
        Me.Panel4.SuspendLayout()
        Me.TabKeyboard.SuspendLayout()
        Me.pnlTabInfo.SuspendLayout()
        Me.pnlNewTabInfo.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageSearch.SuspendLayout()
        Me.pnlEdit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btnCancel.Location = New System.Drawing.Point(488, 4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 32)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.btnSearchAcctNum)
        Me.Panel4.Controls.Add(Me.btnSearchLastName)
        Me.Panel4.Controls.Add(Me.btnSearchPhone)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Location = New System.Drawing.Point(8, 8)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(160, 96)
        Me.Panel4.TabIndex = 5
        '
        'btnSearchAcctNum
        '
        Me.btnSearchAcctNum.Location = New System.Drawing.Point(108, 4)
        Me.btnSearchAcctNum.Name = "btnSearchAcctNum"
        Me.btnSearchAcctNum.Size = New System.Drawing.Size(48, 68)
        Me.btnSearchAcctNum.TabIndex = 3
        Me.btnSearchAcctNum.Text = "Acct Num"
        '
        'btnSearchLastName
        '
        Me.btnSearchLastName.Location = New System.Drawing.Point(56, 4)
        Me.btnSearchLastName.Name = "btnSearchLastName"
        Me.btnSearchLastName.Size = New System.Drawing.Size(48, 68)
        Me.btnSearchLastName.TabIndex = 2
        Me.btnSearchLastName.Text = "Last Name"
        '
        'btnSearchPhone
        '
        Me.btnSearchPhone.BackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btnSearchPhone.Location = New System.Drawing.Point(4, 4)
        Me.btnSearchPhone.Name = "btnSearchPhone"
        Me.btnSearchPhone.Size = New System.Drawing.Size(48, 68)
        Me.btnSearchPhone.TabIndex = 1
        Me.btnSearchPhone.Text = "Phone"
        Me.btnSearchPhone.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(0, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search By"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabKeyboard
        '
        Me.TabKeyboard.BackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.TabKeyboard.Controls.Add(Me.pnlTabInfo)
        Me.TabKeyboard.EnteredString = ""
        Me.TabKeyboard.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabKeyboard.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.TabKeyboard.IsCapital = True
        Me.TabKeyboard.Location = New System.Drawing.Point(8, 128)
        Me.TabKeyboard.Name = "TabKeyboard"
        Me.TabKeyboard.OnlyOneCap = True
        Me.TabKeyboard.Size = New System.Drawing.Size(584, 544)
        Me.TabKeyboard.TabIndex = 0
        '
        'pnlTabInfo
        '
        Me.pnlTabInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.pnlTabInfo.Controls.Add(Me.Label33)
        Me.pnlTabInfo.Controls.Add(Me.lblTabEmail)
        Me.pnlTabInfo.Controls.Add(Me.Label19)
        Me.pnlTabInfo.Controls.Add(Me.Label20)
        Me.pnlTabInfo.Controls.Add(Me.pnlNewTabInfo)
        Me.pnlTabInfo.Controls.Add(Me.Label21)
        Me.pnlTabInfo.Controls.Add(Me.Label22)
        Me.pnlTabInfo.Controls.Add(Me.Label23)
        Me.pnlTabInfo.Controls.Add(Me.Label24)
        Me.pnlTabInfo.Controls.Add(Me.Label25)
        Me.pnlTabInfo.Controls.Add(Me.Label26)
        Me.pnlTabInfo.Controls.Add(Me.Label27)
        Me.pnlTabInfo.Controls.Add(Me.Label28)
        Me.pnlTabInfo.Controls.Add(Me.Label29)
        Me.pnlTabInfo.Controls.Add(Me.Label30)
        Me.pnlTabInfo.Controls.Add(Me.Label31)
        Me.pnlTabInfo.Controls.Add(Me.lblTabFirstName)
        Me.pnlTabInfo.Controls.Add(Me.lblTabAcctNumber)
        Me.pnlTabInfo.Controls.Add(Me.chkTabDoNotDeliver)
        Me.pnlTabInfo.Controls.Add(Me.chkTabVIP)
        Me.pnlTabInfo.Controls.Add(Me.lblTabExt2)
        Me.pnlTabInfo.Controls.Add(Me.Label7)
        Me.pnlTabInfo.Controls.Add(Me.lblTabPhone2)
        Me.pnlTabInfo.Controls.Add(Me.lblTabSpecial)
        Me.pnlTabInfo.Controls.Add(Me.lblTabCrossRoads)
        Me.pnlTabInfo.Controls.Add(Me.lblTabDeliveryZone)
        Me.pnlTabInfo.Controls.Add(Me.lblTabExt1)
        Me.pnlTabInfo.Controls.Add(Me.Label8)
        Me.pnlTabInfo.Controls.Add(Me.lblTabPhone1)
        Me.pnlTabInfo.Controls.Add(Me.lblTabPostalCode)
        Me.pnlTabInfo.Controls.Add(Me.lblTabState)
        Me.pnlTabInfo.Controls.Add(Me.lblTabCity)
        Me.pnlTabInfo.Controls.Add(Me.lblTabAddress2)
        Me.pnlTabInfo.Controls.Add(Me.lblTabAddress1)
        Me.pnlTabInfo.Controls.Add(Me.lblTabLastName)
        Me.pnlTabInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTabInfo.ForeColor = System.Drawing.Color.Black
        Me.pnlTabInfo.Location = New System.Drawing.Point(4, 8)
        Me.pnlTabInfo.Name = "pnlTabInfo"
        Me.pnlTabInfo.Size = New System.Drawing.Size(360, 292)
        Me.pnlTabInfo.TabIndex = 6
        '
        'Label33
        '
        Me.Label33.Location = New System.Drawing.Point(21, 160)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(40, 18)
        Me.Label33.TabIndex = 127
        Me.Label33.Text = "email"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTabEmail
        '
        Me.lblTabEmail.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabEmail.Location = New System.Drawing.Point(68, 158)
        Me.lblTabEmail.Name = "lblTabEmail"
        Me.lblTabEmail.Size = New System.Drawing.Size(224, 20)
        Me.lblTabEmail.TabIndex = 11
        Me.lblTabEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(4, 237)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 36)
        Me.Label19.TabIndex = 125
        Me.Label19.Text = "Special Instructions"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(4, 215)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(72, 20)
        Me.Label20.TabIndex = 124
        Me.Label20.Text = "CrossRoads: "
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlNewTabInfo
        '
        Me.pnlNewTabInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlNewTabInfo.Controls.Add(Me.Label35)
        Me.pnlNewTabInfo.Controls.Add(Me.Label18)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabEmail)
        Me.pnlNewTabInfo.Controls.Add(Me.Label17)
        Me.pnlNewTabInfo.Controls.Add(Me.Label16)
        Me.pnlNewTabInfo.Controls.Add(Me.Label15)
        Me.pnlNewTabInfo.Controls.Add(Me.Label14)
        Me.pnlNewTabInfo.Controls.Add(Me.Label13)
        Me.pnlNewTabInfo.Controls.Add(Me.Label11)
        Me.pnlNewTabInfo.Controls.Add(Me.Label10)
        Me.pnlNewTabInfo.Controls.Add(Me.Label9)
        Me.pnlNewTabInfo.Controls.Add(Me.Label6)
        Me.pnlNewTabInfo.Controls.Add(Me.Label5)
        Me.pnlNewTabInfo.Controls.Add(Me.Label3)
        Me.pnlNewTabInfo.Controls.Add(Me.Label2)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabFirstName)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabAcctNumber)
        Me.pnlNewTabInfo.Controls.Add(Me.chkNewTabDoNotDeliver)
        Me.pnlNewTabInfo.Controls.Add(Me.chkNewTabVIP)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabExt2)
        Me.pnlNewTabInfo.Controls.Add(Me.Label4)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabPhone2)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabSpecial)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabCrossRoads)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabDeliveryZone)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabExt1)
        Me.pnlNewTabInfo.Controls.Add(Me.Label12)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabPhone1)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabPostalCode)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabState)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabCity)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabAddress2)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabAddress1)
        Me.pnlNewTabInfo.Controls.Add(Me.lblNewTabLastName)
        Me.pnlNewTabInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlNewTabInfo.ForeColor = System.Drawing.Color.Black
        Me.pnlNewTabInfo.Location = New System.Drawing.Point(199, 252)
        Me.pnlNewTabInfo.Name = "pnlNewTabInfo"
        Me.pnlNewTabInfo.Size = New System.Drawing.Size(360, 292)
        Me.pnlNewTabInfo.TabIndex = 20
        Me.pnlNewTabInfo.Visible = False
        '
        'Label35
        '
        Me.Label35.Location = New System.Drawing.Point(22, 156)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(40, 18)
        Me.Label35.TabIndex = 129
        Me.Label35.Text = "email"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(4, 237)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(72, 36)
        Me.Label18.TabIndex = 112
        Me.Label18.Text = "Special Instructions"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNewTabEmail
        '
        Me.lblNewTabEmail.BackColor = System.Drawing.Color.White
        Me.lblNewTabEmail.Location = New System.Drawing.Point(68, 158)
        Me.lblNewTabEmail.Name = "lblNewTabEmail"
        Me.lblNewTabEmail.Size = New System.Drawing.Size(224, 20)
        Me.lblNewTabEmail.TabIndex = 128
        Me.lblNewTabEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(2, 214)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(72, 20)
        Me.Label17.TabIndex = 111
        Me.Label17.Text = "CrossRoads: "
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(4, 195)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(72, 20)
        Me.Label16.TabIndex = 110
        Me.Label16.Text = "Deliver Zone: "
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(22, 136)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(40, 18)
        Me.Label15.TabIndex = 109
        Me.Label15.Text = "Acct#: "
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(18, 116)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(44, 18)
        Me.Label14.TabIndex = 108
        Me.Label14.Text = "Phone2"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(18, 94)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(44, 20)
        Me.Label13.TabIndex = 107
        Me.Label13.Text = "Phone1"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(210, 72)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(20, 20)
        Me.Label11.TabIndex = 106
        Me.Label11.Text = "Zip"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(162, 72)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(20, 20)
        Me.Label10.TabIndex = 105
        Me.Label10.Text = "St: "
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(26, 72)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(36, 20)
        Me.Label9.TabIndex = 104
        Me.Label9.Text = "City: "
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(4, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 18)
        Me.Label6.TabIndex = 103
        Me.Label6.Text = "Addr2: "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(4, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 20)
        Me.Label5.TabIndex = 102
        Me.Label5.Text = "Addr1: "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(198, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 20)
        Me.Label3.TabIndex = 101
        Me.Label3.Text = "First: "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(4, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 20)
        Me.Label2.TabIndex = 100
        Me.Label2.Text = "LastName: "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNewTabFirstName
        '
        Me.lblNewTabFirstName.BackColor = System.Drawing.Color.White
        Me.lblNewTabFirstName.Location = New System.Drawing.Point(228, 8)
        Me.lblNewTabFirstName.Name = "lblNewTabFirstName"
        Me.lblNewTabFirstName.Size = New System.Drawing.Size(64, 20)
        Me.lblNewTabFirstName.TabIndex = 1
        Me.lblNewTabFirstName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNewTabAcctNumber
        '
        Me.lblNewTabAcctNumber.BackColor = System.Drawing.Color.White
        Me.lblNewTabAcctNumber.Location = New System.Drawing.Point(68, 136)
        Me.lblNewTabAcctNumber.Name = "lblNewTabAcctNumber"
        Me.lblNewTabAcctNumber.Size = New System.Drawing.Size(224, 20)
        Me.lblNewTabAcctNumber.TabIndex = 99
        Me.lblNewTabAcctNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkNewTabDoNotDeliver
        '
        Me.chkNewTabDoNotDeliver.Location = New System.Drawing.Point(244, 212)
        Me.chkNewTabDoNotDeliver.Name = "chkNewTabDoNotDeliver"
        Me.chkNewTabDoNotDeliver.Size = New System.Drawing.Size(100, 24)
        Me.chkNewTabDoNotDeliver.TabIndex = 18
        Me.chkNewTabDoNotDeliver.Text = "Do Not Deliver"
        '
        'chkNewTabVIP
        '
        Me.chkNewTabVIP.Location = New System.Drawing.Point(244, 190)
        Me.chkNewTabVIP.Name = "chkNewTabVIP"
        Me.chkNewTabVIP.Size = New System.Drawing.Size(48, 24)
        Me.chkNewTabVIP.TabIndex = 17
        Me.chkNewTabVIP.Text = "VIP"
        '
        'lblNewTabExt2
        '
        Me.lblNewTabExt2.BackColor = System.Drawing.Color.White
        Me.lblNewTabExt2.ForeColor = System.Drawing.Color.Black
        Me.lblNewTabExt2.Location = New System.Drawing.Point(196, 116)
        Me.lblNewTabExt2.Name = "lblNewTabExt2"
        Me.lblNewTabExt2.Size = New System.Drawing.Size(32, 18)
        Me.lblNewTabExt2.TabIndex = 16
        Me.lblNewTabExt2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Enabled = False
        Me.Label4.Location = New System.Drawing.Point(170, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 18)
        Me.Label4.TabIndex = 99
        Me.Label4.Text = "ext."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNewTabPhone2
        '
        Me.lblNewTabPhone2.BackColor = System.Drawing.Color.White
        Me.lblNewTabPhone2.Location = New System.Drawing.Point(68, 116)
        Me.lblNewTabPhone2.Name = "lblNewTabPhone2"
        Me.lblNewTabPhone2.Size = New System.Drawing.Size(92, 18)
        Me.lblNewTabPhone2.TabIndex = 14
        Me.lblNewTabPhone2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNewTabSpecial
        '
        Me.lblNewTabSpecial.BackColor = System.Drawing.Color.White
        Me.lblNewTabSpecial.Location = New System.Drawing.Point(80, 237)
        Me.lblNewTabSpecial.Name = "lblNewTabSpecial"
        Me.lblNewTabSpecial.Size = New System.Drawing.Size(242, 54)
        Me.lblNewTabSpecial.TabIndex = 11
        Me.lblNewTabSpecial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNewTabCrossRoads
        '
        Me.lblNewTabCrossRoads.BackColor = System.Drawing.Color.White
        Me.lblNewTabCrossRoads.Location = New System.Drawing.Point(80, 215)
        Me.lblNewTabCrossRoads.Name = "lblNewTabCrossRoads"
        Me.lblNewTabCrossRoads.Size = New System.Drawing.Size(156, 20)
        Me.lblNewTabCrossRoads.TabIndex = 10
        Me.lblNewTabCrossRoads.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNewTabDeliveryZone
        '
        Me.lblNewTabDeliveryZone.BackColor = System.Drawing.Color.White
        Me.lblNewTabDeliveryZone.Location = New System.Drawing.Point(80, 193)
        Me.lblNewTabDeliveryZone.Name = "lblNewTabDeliveryZone"
        Me.lblNewTabDeliveryZone.Size = New System.Drawing.Size(156, 20)
        Me.lblNewTabDeliveryZone.TabIndex = 9
        Me.lblNewTabDeliveryZone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNewTabExt1
        '
        Me.lblNewTabExt1.BackColor = System.Drawing.Color.White
        Me.lblNewTabExt1.Location = New System.Drawing.Point(196, 94)
        Me.lblNewTabExt1.Name = "lblNewTabExt1"
        Me.lblNewTabExt1.Size = New System.Drawing.Size(32, 20)
        Me.lblNewTabExt1.TabIndex = 8
        Me.lblNewTabExt1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Enabled = False
        Me.Label12.Location = New System.Drawing.Point(170, 94)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(24, 20)
        Me.Label12.TabIndex = 99
        Me.Label12.Text = "ext."
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNewTabPhone1
        '
        Me.lblNewTabPhone1.BackColor = System.Drawing.Color.White
        Me.lblNewTabPhone1.Location = New System.Drawing.Point(68, 94)
        Me.lblNewTabPhone1.Name = "lblNewTabPhone1"
        Me.lblNewTabPhone1.Size = New System.Drawing.Size(92, 20)
        Me.lblNewTabPhone1.TabIndex = 7
        Me.lblNewTabPhone1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNewTabPostalCode
        '
        Me.lblNewTabPostalCode.BackColor = System.Drawing.Color.White
        Me.lblNewTabPostalCode.Location = New System.Drawing.Point(230, 72)
        Me.lblNewTabPostalCode.Name = "lblNewTabPostalCode"
        Me.lblNewTabPostalCode.Size = New System.Drawing.Size(62, 20)
        Me.lblNewTabPostalCode.TabIndex = 6
        Me.lblNewTabPostalCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNewTabState
        '
        Me.lblNewTabState.BackColor = System.Drawing.Color.White
        Me.lblNewTabState.Location = New System.Drawing.Point(184, 72)
        Me.lblNewTabState.Name = "lblNewTabState"
        Me.lblNewTabState.Size = New System.Drawing.Size(26, 20)
        Me.lblNewTabState.TabIndex = 5
        Me.lblNewTabState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNewTabCity
        '
        Me.lblNewTabCity.BackColor = System.Drawing.Color.White
        Me.lblNewTabCity.Location = New System.Drawing.Point(68, 72)
        Me.lblNewTabCity.Name = "lblNewTabCity"
        Me.lblNewTabCity.Size = New System.Drawing.Size(92, 20)
        Me.lblNewTabCity.TabIndex = 4
        Me.lblNewTabCity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNewTabAddress2
        '
        Me.lblNewTabAddress2.BackColor = System.Drawing.Color.White
        Me.lblNewTabAddress2.Location = New System.Drawing.Point(68, 52)
        Me.lblNewTabAddress2.Name = "lblNewTabAddress2"
        Me.lblNewTabAddress2.Size = New System.Drawing.Size(224, 18)
        Me.lblNewTabAddress2.TabIndex = 3
        Me.lblNewTabAddress2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNewTabAddress1
        '
        Me.lblNewTabAddress1.BackColor = System.Drawing.Color.White
        Me.lblNewTabAddress1.Location = New System.Drawing.Point(68, 30)
        Me.lblNewTabAddress1.Name = "lblNewTabAddress1"
        Me.lblNewTabAddress1.Size = New System.Drawing.Size(224, 20)
        Me.lblNewTabAddress1.TabIndex = 2
        Me.lblNewTabAddress1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNewTabLastName
        '
        Me.lblNewTabLastName.BackColor = System.Drawing.Color.White
        Me.lblNewTabLastName.Location = New System.Drawing.Point(68, 8)
        Me.lblNewTabLastName.Name = "lblNewTabLastName"
        Me.lblNewTabLastName.Size = New System.Drawing.Size(126, 20)
        Me.lblNewTabLastName.TabIndex = 0
        Me.lblNewTabLastName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(5, 195)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(72, 20)
        Me.Label21.TabIndex = 123
        Me.Label21.Text = "Deliver Zone: "
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(21, 138)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(40, 18)
        Me.Label22.TabIndex = 122
        Me.Label22.Text = "Acct#: "
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(18, 116)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(44, 18)
        Me.Label23.TabIndex = 121
        Me.Label23.Text = "Phone2"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(18, 94)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(44, 20)
        Me.Label24.TabIndex = 120
        Me.Label24.Text = "Phone1"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(210, 72)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(20, 20)
        Me.Label25.TabIndex = 119
        Me.Label25.Text = "Zip"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.Location = New System.Drawing.Point(164, 72)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(20, 20)
        Me.Label26.TabIndex = 118
        Me.Label26.Text = "St: "
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label27
        '
        Me.Label27.Location = New System.Drawing.Point(26, 72)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(36, 20)
        Me.Label27.TabIndex = 117
        Me.Label27.Text = "City: "
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label28
        '
        Me.Label28.Location = New System.Drawing.Point(4, 52)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(60, 18)
        Me.Label28.TabIndex = 116
        Me.Label28.Text = "Addr2: "
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label29
        '
        Me.Label29.Location = New System.Drawing.Point(4, 30)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(60, 20)
        Me.Label29.TabIndex = 115
        Me.Label29.Text = "Addr1: "
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(198, 8)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(30, 20)
        Me.Label30.TabIndex = 114
        Me.Label30.Text = "First: "
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label31
        '
        Me.Label31.Location = New System.Drawing.Point(4, 8)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(60, 20)
        Me.Label31.TabIndex = 113
        Me.Label31.Text = "LastName: "
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTabFirstName
        '
        Me.lblTabFirstName.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabFirstName.Location = New System.Drawing.Point(228, 8)
        Me.lblTabFirstName.Name = "lblTabFirstName"
        Me.lblTabFirstName.Size = New System.Drawing.Size(64, 20)
        Me.lblTabFirstName.TabIndex = 2
        Me.lblTabFirstName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTabAcctNumber
        '
        Me.lblTabAcctNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabAcctNumber.Location = New System.Drawing.Point(68, 136)
        Me.lblTabAcctNumber.Name = "lblTabAcctNumber"
        Me.lblTabAcctNumber.Size = New System.Drawing.Size(224, 20)
        Me.lblTabAcctNumber.TabIndex = 99
        Me.lblTabAcctNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkTabDoNotDeliver
        '
        Me.chkTabDoNotDeliver.Location = New System.Drawing.Point(248, 215)
        Me.chkTabDoNotDeliver.Name = "chkTabDoNotDeliver"
        Me.chkTabDoNotDeliver.Size = New System.Drawing.Size(70, 22)
        Me.chkTabDoNotDeliver.TabIndex = 18
        Me.chkTabDoNotDeliver.Text = "Warning"
        '
        'chkTabVIP
        '
        Me.chkTabVIP.Location = New System.Drawing.Point(248, 197)
        Me.chkTabVIP.Name = "chkTabVIP"
        Me.chkTabVIP.Size = New System.Drawing.Size(64, 18)
        Me.chkTabVIP.TabIndex = 17
        Me.chkTabVIP.Text = "VIP"
        '
        'lblTabExt2
        '
        Me.lblTabExt2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabExt2.Location = New System.Drawing.Point(196, 116)
        Me.lblTabExt2.Name = "lblTabExt2"
        Me.lblTabExt2.Size = New System.Drawing.Size(32, 18)
        Me.lblTabExt2.TabIndex = 11
        Me.lblTabExt2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(168, 116)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 20)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "ext."
        '
        'lblTabPhone2
        '
        Me.lblTabPhone2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabPhone2.Location = New System.Drawing.Point(68, 116)
        Me.lblTabPhone2.Name = "lblTabPhone2"
        Me.lblTabPhone2.Size = New System.Drawing.Size(92, 18)
        Me.lblTabPhone2.TabIndex = 10
        Me.lblTabPhone2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTabSpecial
        '
        Me.lblTabSpecial.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabSpecial.Location = New System.Drawing.Point(80, 237)
        Me.lblTabSpecial.Name = "lblTabSpecial"
        Me.lblTabSpecial.Size = New System.Drawing.Size(242, 54)
        Me.lblTabSpecial.TabIndex = 14
        '
        'lblTabCrossRoads
        '
        Me.lblTabCrossRoads.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabCrossRoads.Location = New System.Drawing.Point(80, 215)
        Me.lblTabCrossRoads.Name = "lblTabCrossRoads"
        Me.lblTabCrossRoads.Size = New System.Drawing.Size(156, 20)
        Me.lblTabCrossRoads.TabIndex = 13
        Me.lblTabCrossRoads.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTabDeliveryZone
        '
        Me.lblTabDeliveryZone.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabDeliveryZone.Location = New System.Drawing.Point(80, 193)
        Me.lblTabDeliveryZone.Name = "lblTabDeliveryZone"
        Me.lblTabDeliveryZone.Size = New System.Drawing.Size(156, 20)
        Me.lblTabDeliveryZone.TabIndex = 12
        Me.lblTabDeliveryZone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTabExt1
        '
        Me.lblTabExt1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabExt1.Location = New System.Drawing.Point(196, 94)
        Me.lblTabExt1.Name = "lblTabExt1"
        Me.lblTabExt1.Size = New System.Drawing.Size(32, 20)
        Me.lblTabExt1.TabIndex = 9
        Me.lblTabExt1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(168, 94)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(24, 20)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "ext."
        '
        'lblTabPhone1
        '
        Me.lblTabPhone1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabPhone1.Location = New System.Drawing.Point(68, 94)
        Me.lblTabPhone1.Name = "lblTabPhone1"
        Me.lblTabPhone1.Size = New System.Drawing.Size(92, 20)
        Me.lblTabPhone1.TabIndex = 8
        Me.lblTabPhone1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTabPostalCode
        '
        Me.lblTabPostalCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabPostalCode.Location = New System.Drawing.Point(230, 72)
        Me.lblTabPostalCode.Name = "lblTabPostalCode"
        Me.lblTabPostalCode.Size = New System.Drawing.Size(62, 20)
        Me.lblTabPostalCode.TabIndex = 7
        Me.lblTabPostalCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTabState
        '
        Me.lblTabState.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabState.Location = New System.Drawing.Point(186, 72)
        Me.lblTabState.Name = "lblTabState"
        Me.lblTabState.Size = New System.Drawing.Size(24, 20)
        Me.lblTabState.TabIndex = 6
        Me.lblTabState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTabCity
        '
        Me.lblTabCity.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabCity.Location = New System.Drawing.Point(68, 72)
        Me.lblTabCity.Name = "lblTabCity"
        Me.lblTabCity.Size = New System.Drawing.Size(92, 20)
        Me.lblTabCity.TabIndex = 5
        Me.lblTabCity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTabAddress2
        '
        Me.lblTabAddress2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabAddress2.Location = New System.Drawing.Point(68, 52)
        Me.lblTabAddress2.Name = "lblTabAddress2"
        Me.lblTabAddress2.Size = New System.Drawing.Size(224, 18)
        Me.lblTabAddress2.TabIndex = 4
        Me.lblTabAddress2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTabAddress1
        '
        Me.lblTabAddress1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabAddress1.Location = New System.Drawing.Point(68, 30)
        Me.lblTabAddress1.Name = "lblTabAddress1"
        Me.lblTabAddress1.Size = New System.Drawing.Size(224, 20)
        Me.lblTabAddress1.TabIndex = 3
        Me.lblTabAddress1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTabLastName
        '
        Me.lblTabLastName.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.lblTabLastName.Location = New System.Drawing.Point(68, 8)
        Me.lblTabLastName.Name = "lblTabLastName"
        Me.lblTabLastName.Size = New System.Drawing.Size(126, 20)
        Me.lblTabLastName.TabIndex = 1
        Me.lblTabLastName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.Panel3.Controls.Add(Me.TabControl1)
        Me.Panel3.Controls.Add(Me.Panel2)
        Me.Panel3.Controls.Add(Me.TabKeyboard)
        Me.Panel3.Location = New System.Drawing.Point(356, 8)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(600, 676)
        Me.Panel3.TabIndex = 7
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageSearch)
        Me.TabControl1.ItemSize = New System.Drawing.Size(90, 18)
        Me.TabControl1.Location = New System.Drawing.Point(8, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(584, 132)
        Me.TabControl1.TabIndex = 7
        '
        'TabPageSearch
        '
        Me.TabPageSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.TabPageSearch.Controls.Add(Me.btnCloseSave)
        Me.TabPageSearch.Controls.Add(Me.pnlEdit)
        Me.TabPageSearch.Controls.Add(Me.Panel4)
        Me.TabPageSearch.Controls.Add(Me.Panel1)
        Me.TabPageSearch.Controls.Add(Me.btnCancel)
        Me.TabPageSearch.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSearch.Name = "TabPageSearch"
        Me.TabPageSearch.Size = New System.Drawing.Size(576, 106)
        Me.TabPageSearch.TabIndex = 0
        Me.TabPageSearch.Text = "Search"
        '
        'btnCloseSave
        '
        Me.btnCloseSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btnCloseSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseSave.Location = New System.Drawing.Point(488, 44)
        Me.btnCloseSave.Name = "btnCloseSave"
        Me.btnCloseSave.Size = New System.Drawing.Size(80, 56)
        Me.btnCloseSave.TabIndex = 9
        Me.btnCloseSave.Text = "Save && Close"
        Me.btnCloseSave.UseVisualStyleBackColor = False
        '
        'pnlEdit
        '
        Me.pnlEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlEdit.Controls.Add(Me.btnEditEntry)
        Me.pnlEdit.Controls.Add(Me.Label32)
        Me.pnlEdit.Controls.Add(Me.btnTabEnterNew)
        Me.pnlEdit.Location = New System.Drawing.Point(292, 8)
        Me.pnlEdit.Name = "pnlEdit"
        Me.pnlEdit.Size = New System.Drawing.Size(108, 96)
        Me.pnlEdit.TabIndex = 8
        Me.pnlEdit.Visible = False
        '
        'btnEditEntry
        '
        Me.btnEditEntry.Location = New System.Drawing.Point(56, 4)
        Me.btnEditEntry.Name = "btnEditEntry"
        Me.btnEditEntry.Size = New System.Drawing.Size(48, 68)
        Me.btnEditEntry.TabIndex = 2
        Me.btnEditEntry.Text = "Edit Entry"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.Label32.Location = New System.Drawing.Point(0, 76)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(112, 20)
        Me.Label32.TabIndex = 0
        Me.Label32.Text = "Edit"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnTabEnterNew
        '
        Me.btnTabEnterNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.btnTabEnterNew.Location = New System.Drawing.Point(4, 4)
        Me.btnTabEnterNew.Name = "btnTabEnterNew"
        Me.btnTabEnterNew.Size = New System.Drawing.Size(48, 68)
        Me.btnTabEnterNew.TabIndex = 7
        Me.btnTabEnterNew.Text = "Enter New"
        Me.btnTabEnterNew.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnLocationAll)
        Me.Panel1.Controls.Add(Me.btnLocationStore)
        Me.Panel1.Controls.Add(Me.lblTabSearchLocation)
        Me.Panel1.Location = New System.Drawing.Point(176, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(108, 96)
        Me.Panel1.TabIndex = 6
        '
        'btnLocationAll
        '
        Me.btnLocationAll.Location = New System.Drawing.Point(56, 4)
        Me.btnLocationAll.Name = "btnLocationAll"
        Me.btnLocationAll.Size = New System.Drawing.Size(48, 68)
        Me.btnLocationAll.TabIndex = 2
        Me.btnLocationAll.Text = "All Stores"
        '
        'btnLocationStore
        '
        Me.btnLocationStore.BackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btnLocationStore.Location = New System.Drawing.Point(4, 4)
        Me.btnLocationStore.Name = "btnLocationStore"
        Me.btnLocationStore.Size = New System.Drawing.Size(48, 68)
        Me.btnLocationStore.TabIndex = 1
        Me.btnLocationStore.Text = "This Store"
        Me.btnLocationStore.UseVisualStyleBackColor = False
        '
        'lblTabSearchLocation
        '
        Me.lblTabSearchLocation.BackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.lblTabSearchLocation.Location = New System.Drawing.Point(0, 76)
        Me.lblTabSearchLocation.Name = "lblTabSearchLocation"
        Me.lblTabSearchLocation.Size = New System.Drawing.Size(112, 20)
        Me.lblTabSearchLocation.TabIndex = 0
        Me.lblTabSearchLocation.Text = "Location"
        Me.lblTabSearchLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(360, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(232, 132)
        Me.Panel2.TabIndex = 8
        '
        'PreviousOrder_UC1
        '
        Me.PreviousOrder_UC1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.PreviousOrder_UC1.ExperienceNumber = CType(0, Long)
        Me.PreviousOrder_UC1.Location = New System.Drawing.Point(4, 8)
        Me.PreviousOrder_UC1.Name = "PreviousOrder_UC1"
        Me.PreviousOrder_UC1.Size = New System.Drawing.Size(356, 676)
        Me.PreviousOrder_UC1.TabIndex = 8
        '
        'lblMethodUse
        '
        Me.lblMethodUse.BackColor = System.Drawing.SystemColors.Control
        Me.lblMethodUse.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMethodUse.Location = New System.Drawing.Point(744, 10)
        Me.lblMethodUse.Name = "lblMethodUse"
        Me.lblMethodUse.Size = New System.Drawing.Size(136, 26)
        Me.lblMethodUse.TabIndex = 9
        Me.lblMethodUse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tab_Screen
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.Controls.Add(Me.lblMethodUse)
        Me.Controls.Add(Me.PreviousOrder_UC1)
        Me.Controls.Add(Me.Panel3)
        Me.Name = "Tab_Screen"
        Me.Size = New System.Drawing.Size(960, 688)
        Me.Panel4.ResumeLayout(False)
        Me.TabKeyboard.ResumeLayout(False)
        Me.pnlTabInfo.ResumeLayout(False)
        Me.pnlNewTabInfo.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageSearch.ResumeLayout(False)
        Me.pnlEdit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Public Sub InitializeOther()

        '       currentSearchBy = StartInSearch '"Phone"
        '      currentSearchLocation = "Location"
        '222 isDisplaying = True

        '     SearchPhone("BlankSearch", -123456789)

        '        TestForCurrentTabInfo()
        '       BindDataAfterSearch()

        Me.pnlNewTabInfo.Location = New Point(4, 12)
        Me.pnlTabInfo.Location = New Point(4, 12)
        '    tempMethodUse = currentTable.MethodUse
        '   ChangeLabelToCurrentMethod()

        '      DetermineSearch(StartInSearch)

        '      If typeProgram = "Online_Demo" Then
        '     MsgBox("Actual program shows all previous order detail and save changes. Hit New Order button to save Customer detail.", MsgBoxStyle.Information, "DEMO Purposes only")
        '    End If

    End Sub

    Public Sub DetermineSearch(ByVal _startInSearch As String, ByVal searchCriteria As String)

        ResetButtonColors()

        'currently only doing by location
        SetLocationSearch()

        StartInSearch = _startInSearch
        attemptedToEdit = False
        enteringNewTab = False

        If StartInSearch = "Account" Then
            SetAccountSearch()
            SearchAccount(searchCriteria)
        ElseIf StartInSearch = "Phone" Then
            SetPhoneSearch()
            SearchPhone(searchCriteria)
        ElseIf StartInSearch = "TabID" Then
            currentSearchBy = "TabID"
            SearchTabID(searchCriteria)
        End If

        BindDataAfterSearch()

    End Sub

    Private Sub BindData()

        CustomerCurrencyMan = Me.BindingContext(dsCustomer.Tables("TabDirectorySearch"))

        Me.lblTabLastName.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "LastName")
        Me.lblTabFirstName.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "FirstName")
        Me.lblTabAddress1.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "Address1")
        Me.lblTabAddress2.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "Address2")
        Me.lblTabCity.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "City")
        Me.lblTabState.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "State")
        Me.lblTabPostalCode.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "PostalCode")

        Me.lblTabPhone1.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "Phone1")
        Me.lblTabExt1.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "Ext1")
        Me.lblTabPhone2.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "Phone2")
        Me.lblTabExt2.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "Ext2")
        Me.lblTabEmail.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "OpenChar1")

        Me.lblTabAcctNumber.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "AccountNumber")
        Me.lblTabDeliveryZone.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "DeliveryZone")
        Me.lblTabCrossRoads.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "CrossRoads")
        Me.lblTabSpecial.DataBindings.Add("Text", dsCustomer.Tables("TabDirectorySearch"), "SpecialInstructions")

        Me.chkTabVIP.DataBindings.Add("Checked", dsCustomer.Tables("TabDirectorySearch"), "VIP")
        Me.chkTabDoNotDeliver.DataBindings.Add("Checked", dsCustomer.Tables("TabDirectorySearch"), "DoNotDeliver")

    End Sub

    Friend Sub TestForCurrentTabInfo()

        If currentTable.TabID > 0 Then
            ' this means experience already is associated with a tab
            _tempTabID = currentTable.TabID
            SearchPhone("****----****----") ', currentTable.TabID)
            FillPreviousOrders()
            '   Me.btnTabEnterNew.Visible = True
            Me.pnlEdit.Visible = True
            TestForCompleteAddress()
            'need to provide a way to change tab info
            '       Me.PreviousOrder_UC1.btnNewOrder.Text = "Change"
        End If

    End Sub

    Private Sub TestForCompleteAddress()

        If dsCustomer.Tables("TabDirectorySearch").Rows.Count > 0 Then
            If dsCustomer.Tables("TabDirectorySearch").Rows(0)("Address1").ToString.Length > 0 Then
                _hasAddress = True
            End If
        Else

        End If

    End Sub

    Private Sub TabKeyboard_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabKeyboard.StringEntered

        If currentSearchBy = Nothing Then
            'this is we are entering new info
            If enteringNewTab = True Then
                PlaceNewInfoIntoLabel()
            Else
                If Not tabRow Is Nothing Then
                    PlaceEditedInfoIntoLabel()
                End If
            End If
            Exit Sub
        End If

        Me.pnlEdit.Visible = True

        Select Case currentSearchLocation
            Case "Location"

                Select Case currentSearchBy

                    Case "Phone"
                        If TabKeyboard.EnteredString.Length = 10 Then
                            SearchPhone(TabKeyboard.EnteredString) ', -123456789)
                        Else
                            MsgBox("Your Search Phone Number must be 10 digits.")
                        End If
                    Case "Account"
                        SearchAccount(TabKeyboard.EnteredString) ', -123456789)

                End Select

        End Select

        BindDataAfterSearch()

    End Sub

    Friend Sub BindDataAfterSearch()


        '     BindData()
        dsCustomer.Tables("TabPreviousOrders").Rows.Clear()
        Me.PreviousOrder_UC1.ClearPreviousOrders()

        If dsCustomer.Tables("TabDirectorySearch").Rows.Count = 0 Then
            ' we enter new customer

            Me.btnTabEnterNew.BackColor = Color.FromArgb(119, 154, 198)

            '   Me.PreviousOrder_UC1.DisplayFirstOrder(dsCustomer.Tables("TabPreviousOrders"))
            StartEnteringNewTab()

        ElseIf dsCustomer.Tables("TabDirectorySearch").Rows.Count = 1 Then
            ' customer info will bind automatically
            ' we now need to bind any previous orders
            tabRow = dsCustomer.Tables("TabDirectorySearch").Rows(0)
            PopulateTabInfo(tabRow)
            FillPreviousOrders()
            Me.pnlTabInfo.Visible = True
            Me.pnlNewTabInfo.Visible = False
            Me.TabKeyboard.ClearEnteredString()

        ElseIf dsCustomer.Tables("TabDirectorySearch").Rows.Count > 1 Then

            '******************
            '   this is WRONG
            '   we need to list all accounts in the directory
            tabRow = dsCustomer.Tables("TabDirectorySearch").Rows(dsCustomer.Tables("TabDirectorySearch").Rows.Count - 1)
            PopulateTabInfo(tabRow)
            FillPreviousOrders()
            Me.pnlTabInfo.Visible = True
            Me.pnlNewTabInfo.Visible = False
            Me.TabKeyboard.ClearEnteredString()

        End If

        tempMethodUse = currentTable.MethodUse
        ChangeLabelToCurrentMethod()
        '    AddCardHandler()

    End Sub

    Private Sub PopulateTabInfo(ByVal drTab As DataRow)

        _tempTabID = drTab("TabID")

        Try
            _tempTabName = drTab("LastName")
        Catch ex As Exception
            _tempTabName = "Customer"
            Exit Sub
        End Try

        Try
            _tempTabName = _tempTabName + ", " & drTab("FirstName")
        Catch ex As Exception
        End Try

    End Sub

    Friend Sub SearchPhone(ByVal searchCriteriaString As String) ', ByVal searchTabID As Int64)

        '444        If attemptedToEdit = True Then
        '      attemptedToEdit = False
        '     GenerateOrderTables.UpdateTabInfo(StartInSearch)
        '444      Else
        '444         RemoveFromEditMode()
        '    End If

        GenerateOrderTables.PopulateSearchPhone(searchCriteriaString) ', searchTabID)

    End Sub

    Friend Sub SearchAccount(ByVal searchCriteriaString As String) ', ByVal searchTabID As Int64)

        '444     If attemptedToEdit = True Then
        '     attemptedToEdit = False
        '    GenerateOrderTables.UpdateTabInfo(StartInSearch)
        '   End If

        GenerateOrderTables.PopulateSearchAccount(searchCriteriaString) ', searchTabID)

    End Sub

    Friend Sub SearchTabID(ByVal searchCriteriaString As String) ', ByVal searchTabID As Int64)

        '444       If attemptedToEdit = True Then
        '      attemptedToEdit = False
        '     GenerateOrderTables.UpdateTabInfo(StartInSearch)
        '    End If

        GenerateOrderTables.PopulateSearchTabID(searchCriteriaString) ', searchTabID)

    End Sub

    Private Sub FillPreviousOrders()

        dsCustomer.Tables("TabPreviousOrders").Rows.Clear()
        '     dsCustomer.Tables("TabPreviousOrdersbyItem").Clear()

        If typeProgram = "Online_Demo" Then
            Dim filterString As String
            filterString = "TabID = " & TempTabID
            Demo_FilterDontDelete(dsCustomerDemo.Tables("TabPreviousOrders"), dsCustomer.Tables("TabPreviousOrders"), filterString)
            If dsCustomer.Tables("TabPreviousOrders").Rows.Count > 0 Then
                Me.PreviousOrder_UC1.DisplayFirstOrder(dsCustomer.Tables("TabPreviousOrders")) ', dsCustomer.Tables("TabPreviousOrdersbyItem"))
                '      If typeProgram = "Online_Demo" Then
                '     MsgBox("non-DEMO would normally show all previous order detail. Hit New Order button to save Customer detail.", MsgBoxStyle.Information, "DEMO Purposes only")
                'End If

            End If
            Exit Sub
        End If

        sql.SqlSelectCommandTabPreviousOrdersLocation.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandTabPreviousOrdersLocation.Parameters("@TabID").Value = TempTabID

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlTabPreviousOrdersLocation.Fill(dsCustomer.Tables("TabPreviousOrders"))
            '          If dsCustomer.Tables("TabPreviousOrders").Rows.Count > 0 Then
            '         sql.SqlSelectCommandTabPreviousOrdersByItem.Parameters("@ExperienceNumber").Value = dsCustomer.Tables("TabPreviousOrders").Rows(0)("ExperienceNumber")
            '        sql.SqlTabPreviousOrdersByItem.Fill(dsCustomer.Tables("TabPreviousOrdersbyItem"))
            '       End If
            'ccc       dsCustomer.WriteXml("CustomerData.xml", XmlWriteMode.WriteSchema)
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

        If dsCustomer.Tables("TabPreviousOrders").Rows.Count > 0 Then
            Me.PreviousOrder_UC1.DisplayFirstOrder(dsCustomer.Tables("TabPreviousOrders")) ', dsCustomer.Tables("TabPreviousOrdersbyItem"))
            '          MsgBox(dsCustomer.Tables("TabPreviousOrders").Rows.Count)
        End If

    End Sub

    Private Sub DifferentOrderSelected(ByRef dt As DataTable) Handles PreviousOrder_UC1.SelectedPanel
        'was ByRef .. dtTabPreviousOrdersByItem
        'we have to send to here through an event then resend datatable to user control
        '   we do this so we can use the same sql connection but also have the previousOrder_UC avail for all projects

        '    dsCustomer.Tables("TabPreviousOrdersbyItem").Clear()

        dt.Rows.Clear()

        If typeProgram = "Online_Demo" Then
            MsgBox("Actual program shows all previous order detail and saves changes. Hit 'NEW ORDER' button to attach Customer detail.", MsgBoxStyle.Information, "DEMO Purposes only")
            '         MsgBox("non-DEMO would normally show all previous order detail. Hit New Order button to save Customer detail.", MsgBoxStyle.Information, "DEMO Purposes only")
            '         Dim filterString As String
            '        filterString = "ExperienceNumber = " & PreviousOrder_UC1.ExperienceNumber
            '       Demo_FilterDontDelete(dsCustomerDemo.Tables("TabPreviousOrdersByItem"), dt, filterString)
            '      PreviousOrder_UC1.OrderBeingSentBackAfterPopulate(dsCustomer.Tables("TabPreviousOrders"), dt)
            Exit Sub
        End If

        sql.SqlSelectCommandTabPreviousOrdersByItem.Parameters("@ExperienceNumber").Value = PreviousOrder_UC1.ExperienceNumber

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlTabPreviousOrdersByItem.Fill(dt)
            'ccc     dsCustomer2.WriteXml("CustomerData2.xml", XmlWriteMode.WriteSchema)
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

        PreviousOrder_UC1.OrderBeingSentBackAfterPopulate(dsCustomer.Tables("TabPreviousOrders"), dt)

    End Sub

    Private Sub ReorderButtonSelected(ByRef dtTabPreviousOrdersByItem2 As DataTable, ByVal tabTestNeeded As Boolean) Handles PreviousOrder_UC1.SelectedReOrder

        If enteringNewTab = True Then
            StartEnteringNewTab()
        End If
        '444    RemoveCardHandler()
        '222 isDisplaying = False
        '     dsCustomer.Tables("TabPreviousOrdersbyItem")
        '      MsgBox(dtTabPreviousOrdersByItem.Rows.Count)
        '     dtTabPreviousOrdersByItem = dtTabPreviousOrdersByItem2.Copy()
        '    MsgBox(dtTabPreviousOrdersByItem.Rows.Count)
        RaiseEvent SelectedReOrder(dtTabPreviousOrdersByItem2, True)
        Me.Visible = False
        '444
        Exit Sub

        RaiseEvent TabScreenDisposing()

    End Sub


    Private Sub NewOrderButtonSelected() Handles PreviousOrder_UC1.SelectedNewOrder


        If enteringNewTab = True Then
            StartEnteringNewTab()
        End If

        '444      RemoveCardHandler()
        '222   isDisplaying = False
        RaiseEvent SelectedNewOrder()
        RaiseEvent TabScreenDisposing()
        '444
        Exit Sub


    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        '    Me.Visible = False
        '444     RemoveCardHandler()
        RaiseEvent TabScreenDisposing()

    End Sub

    Private Sub btnCloseSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseSave.Click

        '       If Not currentTable.TabName = _tempTabName Then
        '      enteringNewTab = True
        '     End If

        If attemptedToEdit = True Then 'or enteringNewTab = True 
            VerifyTextBoxLengths()
        End If
        '       If enteringNewTab = True Then
        '     VerifyTextBoxLengths()
        '    StartEnteringNewTab()
        '   End If
        If attemptedToEdit = True And Not tabRow Is Nothing Then 'typeProgram = "Online_Demo" Then
            ' if me._tempAccountPhone = 
            tabRow("UpdatedDate") = Now
            tabRow("UpdatedByEmployee") = currentTable.EmployeeID
        End If
        If Not tempMethodUse = currentTable.MethodUse Then
            currentTable.MethodUse = tempMethodUse
            DefineMethodDirection()
            GenerateOrderTables.UpdateMethodDataset()
            RaiseEvent ChangedMethodUse()
        End If

        If attemptedToEdit = True Then
            GenerateOrderTables.UpdateTabInfo(StartInSearch)
            attemptedToEdit = False
        End If
        '     Me.Visible = False
        '444     RemoveCardHandler()
        RaiseEvent TabScreenDisposing()
        RaiseEvent SelectedNewOrder()
     
    End Sub

    Private Sub VerifyTextBoxLengths()

        '999
        '**************
        'we need to verify all text boxes
        'maybe both new and existing


        With currentTabInfo

            If lblNewTabState.Text.Length < 2 Then
                .State = ""
            Else
                .State = lblNewTabState.Text.Substring(0, 2).ToUpper  'Format(Me.lblNewTabState.Text, "##")
            End If
        End With

    End Sub
    Private Sub StartEnteringNewTab()

        If enteringNewTab = True Then
            'already set up for entering, already created new datarow

            With currentTabInfo
                '         If Me.lblNewTabAcctNumber.Text.Length = 0 Then
                '        .AccountNumber = Nothing
                '       Else
                '      End If

                .AccountNumber = Me.lblNewTabAcctNumber.Text    '_tempAccountNumber '
                .AccountPhone = _tempAccountPhone
                .LastName = Me.lblNewTabLastName.Text
                .FirstName = Me.lblNewTabFirstName.Text
                .MiddleName = ""
                .NickName = ""
                .Address1 = Me.lblNewTabAddress1.Text
                .Address2 = Me.lblNewTabAddress2.Text
                .City = Me.lblNewTabCity.Text
                If lblNewTabState.Text.Length < 2 Then
                    .State = ""
                Else
                    .State = lblNewTabState.Text.Substring(0, 2).ToUpper  'Format(Me.lblNewTabState.Text, "##")
                End If
                .PostalCode = Me.lblNewTabPostalCode.Text
                .Country = "USA"
                If Me.lblNewTabPhone1.Text = "Phone 1" Or Me.lblNewTabPhone1.Text.Length = 0 Then
                    .Phone1 = Nothing
                Else
                    .Phone1 = Me.lblNewTabPhone1.Text
                End If

                .Ext1 = Me.lblNewTabExt1.Text
                .Phone2 = Me.lblNewTabPhone2.Text
                .Ext2 = Me.lblNewTabExt2.Text
                .Email = Me.lblNewTabEmail.Text

                .DeliverZone = 0  '****** needs to be INT **** CInt(Me.lblNewTabDeliveryZone.Text)
                '****** needs to be INT ****  right now entering only DBNULL.value
                'we will make delivery zone a drop down list
                'that will be in ds dataset (same as menu)
                .CrossRoads = Me.lblNewTabCrossRoads.Text
                .SpecialInstructions = Me.lblNewTabSpecial.Text
                .DoNotDeliver = Me.chkTabDoNotDeliver.Checked
                .VIP = Me.chkTabVIP.Checked
                .UpdatedDate = Now
                .UpdatedByEmployee = currentTable.EmployeeID
                .Active = True

                _tempTabID = GenerateOrderTables.CreateNewTabInfoData(currentTabInfo, StartInSearch)
                Try
                    _tempTabName = .LastName & ", " & .FirstName
                Catch ex As Exception
                    _tempTabName = "Customer"
                    Exit Sub
                End Try

            End With


        Else
            currentTabInfo = New TabInfo

            enteringNewTab = True
            '     Me.btnTabEnterNew.Text = "Accept New"
            '     Me.pnlEdit.Visible = True
            '    Me.btnTabEnterNew.Visible = True
            '       Me.btnTabEnterNew.BackColor = Color.FromArgb(119, 154, 198)

            Me.pnlTabInfo.Visible = False
            Me.pnlNewTabInfo.Visible = True
            '     Me.pnlNewTabInfo.BringToFront()

            Select Case currentSearchBy
                Case "Phone"
                    _tempAccountPhone = TabKeyboard.EnteredString
                    If TabKeyboard.EnteredString.Length = 10 Then
                        Me.lblNewTabPhone1.Text = ConvertToPhoneString(TabKeyboard.EnteredString)
                    ElseIf TabKeyboard.EnteredString.Length > 0 Then
                        MsgBox("Phone Number Must be 10 Digits.")
                        '                        Me.lblNewTabPhone1.Text = Me.TabKeyboard.EnteredString
                    End If
                Case "Account"
                    Me.lblNewTabPhone1.Text = _tempAccountNumber
            End Select

            PlaceInEditMode()

            activeLabelString = "lblNewTabLastName"
            activeLabel = Me.lblNewTabLastName
            Me.TabKeyboard.ClearEnteredString()

        End If
    End Sub

    Private Function ConvertToPhoneString(ByVal phone As String)
        Dim phoneString As String

        Try
            phoneString = "(" & phone.Substring(0, 3) & ") " & phone.Substring(3, 3) & "-" & phone.Substring(6, 4)
        Catch ex As Exception
            phoneString = phone
        End Try

        Return phoneString

    End Function

    '   Private Sub TabLabels_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTabAcctNumber.DoubleClick, lblTabAddress1.DoubleClick, lblTabAddress2.DoubleClick, lblTabCity.DoubleClick, lblTabCrossRoads.DoubleClick, lblTabDeliveryZone.DoubleClick, lblTabExt1.DoubleClick, lblTabExt2.DoubleClick, lblTabFirstName.DoubleClick, lblTabLastName.DoubleClick, lblTabPhone1.DoubleClick, lblTabPhone2.DoubleClick, lblTabPostalCode.DoubleClick, lblTabSpecial.DoubleClick, lblTabState.DoubleClick

    '      If Not tabRow Is Nothing Then
    '          PlaceEditedInfoIntoLabel()
    '      End If
    '     activeLabel = sender
    '     GotoLabelSelected()
    ''     Me.TabKeyboard.ClearEnteredString()
    '       Me.TabKeyboard.PopulateEnteredString(activeLabel.Text)
    '
    '  End Sub

    Private Sub TabLabels_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTabAddress1.Click, lblTabAddress2.Click, lblTabCity.Click, lblTabCrossRoads.Click, lblTabDeliveryZone.Click, lblTabExt1.Click, lblTabExt2.Click, lblTabFirstName.Click, lblTabLastName.Click, lblTabPhone1.Click, lblTabPhone2.Click, lblTabPostalCode.Click, lblTabSpecial.Click, lblTabState.Click, lblTabEmail.Click

        If tabTimerActive = False Then
            tabDoubleClickTimer = New Timer
            tabTimerActive = True
            AddHandler tabDoubleClickTimer.Tick, AddressOf TabTimerExpired
            tabDoubleClickTimer.Interval = 500
            tabDoubleClickTimer.Start()

            If Not tabRow Is Nothing Then
                PlaceEditedInfoIntoLabel()
            End If
            activeLabel = sender
            GotoLabelSelected()
            Me.TabKeyboard.ClearEnteredString()
            If sender Is lblTabEmail Then
                TabKeyboard.IsCapital = False
            End If

        Else
            'this means we just DOUBLE clicked
            tabTimerActive = False
            tabDoubleClickTimer.Dispose()
            Me.TabKeyboard.PopulateEnteredString(activeLabel.Text)
        End If

    End Sub

    Private Sub TabTimerExpired(ByVal sender As Object, ByVal e As System.EventArgs)
        tabTimerActive = False
        tabDoubleClickTimer.Dispose()
        '                               **** this means we clicked
    End Sub


    Private Sub GotoLabelSelected()

        If Not activeLabel Is Nothing Then
            activeLabelString = activeLabel.Name.ToString
            ResetLabelBackcolors(Color.FromArgb(224, 241, 254))
            activeLabel.BackColor = Color.White
        End If

    End Sub

    Private Sub PlaceEditedInfoIntoLabel()

        attemptedToEdit = True

        Select Case activeLabelString
            Case "lblTabLastName"
                tabRow("LastName") = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblTabFirstName

            Case "lblTabFirstName"
                tabRow("FirstName") = Me.TabKeyboard.EnteredString
                '       Me.lblTabFirstName.Text = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblTabAddress1

            Case "lblTabAddress1"
                tabRow("Address1") = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblTabAddress2

            Case "lblTabAddress2"
                tabRow("Address2") = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblTabCity

            Case "lblTabCity"
                tabRow("City") = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblTabState

            Case "lblTabState"
                If Me.TabKeyboard.EnteredString.Length < 2 Then
                    tabRow("State") = ""
                Else
                    tabRow("State") = Me.TabKeyboard.EnteredString.Substring(0, 2).ToUpper
                End If
                activeLabel = Me.lblTabPostalCode

            Case "lblTabPostalCode"
                tabRow("PostalCode") = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblTabPhone1

            Case "lblTabPhone1"

                If TabKeyboard.EnteredString.Length = 10 Then
                    '   _tempAccountPhone = TabKeyboard.EnteredString
                    tabRow("AccountPhone") = TabKeyboard.EnteredString
                    tabRow("Phone1") = ConvertToPhoneString(TabKeyboard.EnteredString)
                    activeLabel = Me.lblNewTabExt1
                Else
                    MsgBox("Phone Number Must be 10 Digits")
                End If

                '    tabRow("Phone1") = Me.TabKeyboard.EnteredString
                '   activeLabel = Me.lblTabExt1

            Case "lblTabExt1"
                tabRow("Ext1") = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblTabPhone2

            Case "lblTabPhone2"
                If TabKeyboard.EnteredString.Length = 10 Then
                    tabRow("Phone2") = ConvertToPhoneString(Me.TabKeyboard.EnteredString)
                    activeLabel = Me.lblTabExt2
                Else
                    If MsgBox("Phone Number is not 10 Digits", MsgBoxStyle.OKCancel) = MsgBoxResult.OK Then
                        tabRow("Phone2") = Me.TabKeyboard.EnteredString
                        activeLabel = Me.lblTabExt2
                    End If
                End If


            Case "lblTabExt2"
                tabRow("Ext2") = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblTabEmail

                '           Case "lblTabAcctNumber"
                '              Me.lblTabAcctNumber.Text = Me.TabKeyboard.EnteredString
                '             activeLabel = Me.lblTabAddress2


            Case "lblTabEmail"
                tabRow("OpenChar1") = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblTabDeliveryZone

            Case "lblTabDeliveryZone"
                tabRow("DeliveryZone") = 0 'Me.TabKeyboard.EnteredString
                activeLabel = Me.lblTabCrossRoads

            Case "lblTabCrossRoads"
                tabRow("CrossRoads") = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblTabSpecial

            Case "lblTabSpecial"
                tabRow("SpecialInstructions") = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblTabLastName

        End Select

        If Not activeLabel Is Nothing Then
            GotoLabelSelected()
            TabKeyboard.PopulateEnteredString(activeLabel.Text.ToString)
            If activeLabel.Text.Length = 0 Then
                TabKeyboard.MakeNextCap()
            End If
        End If

    End Sub

    '****************************
    'this is when adding new tabs

    Private Sub NewTabLabels_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblNewTabAddress1.Click, lblNewTabAddress2.Click, lblNewTabCity.Click, lblNewTabCrossRoads.Click, lblNewTabDeliveryZone.Click, lblNewTabExt1.Click, lblNewTabExt2.Click, lblNewTabFirstName.Click, lblNewTabLastName.Click, lblNewTabPhone1.Click, lblNewTabPhone2.Click,  lblNewTabPostalCode.Click, lblNewTabSpecial.Click, lblNewTabState.Click, lblNewTabEmail.Click

        If enteringNewTab = True Then
            PlaceNewInfoIntoLabel()
        End If

        activeLabel = sender
        activeLabelString = activeLabel.Name.ToString
        TabKeyboard.PopulateEnteredString(activeLabel.Text.ToString)
        If sender Is lblNewTabEmail Then
            TabKeyboard.IsCapital = False
        Else
            TabKeyboard.MakeNextCap()
        End If

    End Sub

    Private Sub PlaceNewInfoIntoLabel()

        Select Case activeLabelString
            Case "lblNewTabLastName"
                Me.lblNewTabLastName.Text = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblNewTabFirstName

            Case "lblNewTabFirstName"
                Me.lblNewTabFirstName.Text = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblNewTabAddress1

            Case "lblNewTabAddress1"
                Me.lblNewTabAddress1.Text = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblNewTabAddress2

            Case "lblNewTabAddress2"
                Me.lblNewTabAddress2.Text = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblNewTabCity

            Case "lblNewTabCity"
                Me.lblNewTabCity.Text = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblNewTabState

            Case "lblNewTabState"
                If Me.TabKeyboard.EnteredString.Length < 2 Then
                    Me.lblNewTabState.Text = ""
                Else
                    Me.lblNewTabState.Text = Me.TabKeyboard.EnteredString.Substring(0, 2).ToUpper
                End If
                activeLabel = Me.lblNewTabPostalCode

            Case "lblNewTabPostalCode"
                Me.lblNewTabPostalCode.Text = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblNewTabPhone1

            Case "lblNewTabPhone1"
                If TabKeyboard.EnteredString.Length = 10 Then
                    _tempAccountPhone = TabKeyboard.EnteredString
                    Me.lblNewTabPhone1.Text = ConvertToPhoneString(TabKeyboard.EnteredString)
                    activeLabel = Me.lblNewTabExt1
                Else
                    MsgBox("Phone Number Must be 10 Digits")
                End If

            Case "lblNewTabExt1"
                Me.lblNewTabExt1.Text = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblNewTabPhone2

            Case "lblNewTabPhone2"
                Me.lblNewTabPhone2.Text = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblNewTabExt2

            Case "lblNewTabExt2"
                Me.lblNewTabExt2.Text = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblNewTabEmail

                '     Case "lblNewTabAcctNumber"
                '        Me.lblNewTabAcctNumber.Text = Me.TabKeyboard.EnteredString
                '       activeLabel = Me.lblNewTabEmail

            Case "lblNewTabEmail"
                Me.lblNewTabEmail.Text = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblNewTabDeliveryZone

            Case "lblNewTabDeliveryZone"
                Me.lblNewTabDeliveryZone.Text = 0 'Me.TabKeyboard.EnteredString
                activeLabel = Me.lblNewTabCrossRoads

            Case "lblNewTabCrossRoads"
                Me.lblNewTabCrossRoads.Text = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblNewTabSpecial

            Case "lblNewTabSpecial"
                Me.lblNewTabSpecial.Text = Me.TabKeyboard.EnteredString
                activeLabel = Me.lblNewTabLastName

        End Select


        If Not activeLabel Is Nothing Then
            activeLabelString = activeLabel.Name.ToString
            TabKeyboard.PopulateEnteredString(activeLabel.Text.ToString)
            If activeLabel.Text.Length = 0 Then
                TabKeyboard.MakeNextCap()
            End If
        End If

    End Sub

    Private Sub btnTabEnterNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTabEnterNew.Click

        ' at some point we need to be able to add by swiping card
        '444      RemoveCardHandler()

        If attemptedToEdit = True Then
            attemptedToEdit = False
            GenerateOrderTables.UpdateTabInfo(StartInSearch)
        End If
        Me.btnEditEntry.BackColor = Color.FromArgb(224, 241, 254)
        Me.btnTabEnterNew.BackColor = Color.FromArgb(119, 154, 198)
        StartEnteringNewTab()
        enteringNewTab = True 'this must be after we created datarow

    End Sub

    Private Sub btnEditEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditEntry.Click

        PlaceInEditMode()

    End Sub

    Private Sub PlaceInEditMode()

        '444     RemoveCardHandler()

        Select Case currentSearchBy
            Case "Phone"
                Me.btnSearchPhone.BackColor = Color.FromArgb(224, 241, 254)
            Case "Account"
                Me.btnSearchAcctNum.BackColor = Color.FromArgb(224, 241, 254)
        End Select

        '      Select Case currentSearchLocation
        '         Case "Location"
        '    Me.btnLocationStore.BackColor = Color.FromArgb(224, 241, 254)
        '   End Select

        Me.btnEditEntry.BackColor = Color.FromArgb(119, 154, 198)
        Me.btnTabEnterNew.BackColor = Color.FromArgb(224, 241, 254)

        '   currentSearchLocation = Nothing
        currentSearchBy = Nothing
        Me.pnlNewTabInfo.Visible = False
        Me.pnlTabInfo.Visible = True
        enteringNewTab = False
        attemptedToEdit = True

    End Sub

    Private Sub RemoveFromEditMode()

        If attemptedToEdit = True Then
            attemptedToEdit = False
            GenerateOrderTables.UpdateTabInfo(StartInSearch)
        End If

        activeLabelString = Nothing
        Me.TabKeyboard.ClearEnteredString()
        Me.pnlNewTabInfo.Visible = False
        Me.pnlTabInfo.Visible = True
        attemptedToEdit = False
        enteringNewTab = False
        Me.btnEditEntry.BackColor = Color.FromArgb(224, 241, 254)
        Me.btnTabEnterNew.BackColor = Color.FromArgb(224, 241, 254)

    End Sub

    Private Sub btnSearchPhone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPhone.Click

        RemoveFromEditMode()
        SetPhoneSearch()

    End Sub

    Private Sub btnSearchLastName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchLastName.Click

    End Sub

    Private Sub btnSearchAcctNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchAcctNum.Click

        RemoveFromEditMode()
        SetAccountSearch()

    End Sub

    Private Sub SetLocationSearch()

        currentSearchLocation = "Location"
        Me.btnLocationStore.BackColor = Color.FromArgb(119, 154, 198)
        Me.btnLocationAll.BackColor = Color.FromArgb(224, 241, 254)

    End Sub
    Private Sub SetPhoneSearch()

        '444    RemoveCardHandler()

        currentSearchBy = "Phone"
        Me.btnSearchPhone.BackColor = Color.FromArgb(119, 154, 198)
        Me.btnSearchLastName.BackColor = Color.FromArgb(224, 241, 254)
        Me.btnSearchAcctNum.BackColor = Color.FromArgb(224, 241, 254)

    End Sub

    Private Sub SetAccountSearch()

        '444     AddCardHandler()
    
        currentSearchBy = "Account"
        Me.btnSearchPhone.BackColor = Color.FromArgb(224, 241, 254)
        Me.btnSearchLastName.BackColor = Color.FromArgb(224, 241, 254)
        Me.btnSearchAcctNum.BackColor = Color.FromArgb(119, 154, 198)

    End Sub

    Private Sub btnLocationStore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocationStore.Click

        SetLocationSearch()
       
    End Sub

    Private Sub ResetButtonColors()

        Me.btnSearchPhone.BackColor = Color.FromArgb(224, 241, 254)
        Me.btnSearchLastName.BackColor = Color.FromArgb(224, 241, 254)
        Me.btnSearchAcctNum.BackColor = Color.FromArgb(224, 241, 254)

        Me.btnLocationStore.BackColor = Color.FromArgb(224, 241, 254)
        Me.btnLocationAll.BackColor = Color.FromArgb(224, 241, 254)

        Me.btnEditEntry.BackColor = Color.FromArgb(224, 241, 254)
        Me.btnTabEnterNew.BackColor = Color.FromArgb(224, 241, 254)

    End Sub

    Private Sub ResetLabelBackcolors(ByVal bc As Color)

        Me.lblTabAcctNumber.BackColor = bc
        Me.lblTabAddress1.BackColor = bc
        Me.lblTabAddress2.BackColor = bc
        Me.lblTabCity.BackColor = bc
        Me.lblTabCrossRoads.BackColor = bc
        Me.lblTabDeliveryZone.BackColor = bc
        Me.lblTabExt1.BackColor = bc
        Me.lblTabExt2.BackColor = bc
        Me.lblTabFirstName.BackColor = bc
        Me.lblTabLastName.BackColor = bc    'Color.FromArgb(224, 241, 254)
        Me.lblTabPhone1.BackColor = bc
        Me.lblTabPhone2.BackColor = bc
        Me.lblTabEmail.BackColor = bc
        Me.lblTabPostalCode.BackColor = bc
        Me.lblTabSpecial.BackColor = bc
        Me.lblTabState.BackColor = bc



    End Sub

    Private Sub CustomerCardRead(ByRef newpayment As DataSet_Builder.Payment) Handles readAuthTab222.CardReadSuccessful

        ' *** not sure if this is correct,not sure about
        ' the AddPayment Collection in Read Credit ????
        '444      GenerateOrderTables.CreateTabAcctPlaceInExperience(newpayment)
        _tempAccountNumber = newpayment.SpiderAcct
        'might need to  SetAccountSearch() , currently we are doing before swipe activate

        If attemptedToEdit = True Then
            attemptedToEdit = False
            GenerateOrderTables.UpdateTabInfo(StartInSearch)
        End If
        GenerateOrderTables.PopulateSearchAccount(newpayment.SpiderAcct) ', -123456789)
        BindDataAfterSearch()

        Exit Sub '444

        newpayment.SpiderAcct = CreateAccountNumber(newpayment) '.LastName, newpayment.AccountNumber)
        _tempAccountNumber = newpayment.SpiderAcct
        '    AddPaymentToCollection(newpayment)
        '     tempPayment = newpayment    'this way we can populate exp if saving 
        GenerateOrderTables.CreateTabAcctPlaceInExperience(newpayment)
        SearchAccount(newpayment.SpiderAcct) ', -123456789)
        BindDataAfterSearch()

    End Sub

    Private Sub AddCardHandler222()
        Exit Sub
        readAuthTab222 = New ReadCredit(False)
        tmrCardRead.Start()
        AddHandler tmrCardRead.Tick, AddressOf readAuthTab222.tmrCardRead_Tick

    End Sub
    Private Sub RemoveCardHandler222()
        Exit Sub
        If Not readAuthTab222 Is Nothing Then
            tmrCardRead.Stop()
            RemoveHandler tmrCardRead.Tick, AddressOf readAuthTab222.tmrCardRead_Tick
            readAuthTab222.Shutdown()
            readAuthTab222 = Nothing
        End If

    End Sub

    Private Sub lblMethodUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMethodUse.Click

        If tempMethodUse = "Delivery" Then
            tempMethodUse = "Take Out"
        Else : tempMethodUse = "Take Out"
            tempMethodUse = "Delivery"
        End If

        ChangeLabelToCurrentMethod()
        '    GenerateOrderTables.UpdateMethodDataset()

    End Sub

    Private Sub ChangeLabelToCurrentMethod()

        Me.lblMethodUse.Text = tempMethodUse

    End Sub

End Class
