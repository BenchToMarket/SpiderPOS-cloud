Imports DataSet_Builder
Imports ReadCredit_MWE

Public Class Login
    Inherits System.Windows.Forms.Form

    '    Dim sql As New DataSet_Builder.SQLHelper(connectserver)
    Dim updateClockTimerLogin As New Timer

    Dim sqlStatement As String
    Dim tableCreating As String
   
    Private currentMenu As Menu
    Private secondaryMenu As Menu
    Friend WithEvents readAuth As ReadCredit

    '444   Friend WithEvents readAuth_MWE As ReadCredit_MWE2.MainForm_MWE


    Dim WithEvents openProgram As OpeningScreen
    Dim WithEvents tableScreen As Tables_Screen_Bar
    Dim WithEvents activeOrder As term_OrderForm
    Dim WithEvents activeSplit As SplitChecks
    Dim WithEvents mgrActiveSplit As SplitChecks
    Dim WithEvents managementScreen As Manager_Form
    Dim WithEvents QuickOrder As term_OrderForm
    Dim WithEvents SeatingChart As Seating_ChooseTable
    Dim WithEvents connectionDown As ConnectionDown_UC
    Dim WithEvents SeatingTab As Seating_EnterTab
    Dim WithEvents DeliveryScreen As Tab_Screen
    Dim WithEvents ClockingOutEmployee As ClockOut_UC
    Dim WithEvents openInfo As DataSet_Builder.Information_UC
    Dim WithEvents cashDrawer As CashDrawer_UC
    Dim WithEvents expOverride As KeyBoard_UC
    Dim expOverriding As Boolean = False

    '    Dim WithEvents numCustPad As DataSet_Builder.NumberOfCustomers_UC

    '    Dim titleHeader As New DataSet_Builder.TitleUserControl

    Dim WithEvents clockInPanel As ClockInUserControl

    Dim infoReconnect As DataSet_Builder.Information_UC
    Dim infoRecoTimer As Timer
    Dim nonServerClockout As ClockOut_UC
    Dim clockOutActiveQS As Boolean

    Private _loginUsername As String
    Private _loginPassword As String
    Dim WithEvents loginPad As DataSet_Builder.NumberPad
    Dim WithEvents initLogon As DataSet_Builder.LoginInit_UC    'NumberPad



#Region " Windows Form Designer generated code "

    '/ <summary>
    '/ The main entry point for the application.
    '/ </summary>
    <STAThread()> Public Shared Sub Main()
        Application.Run(New Login)
    End Sub

    Public Sub New()
        MyBase.New()

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
    Friend WithEvents lblClockInDay As System.Windows.Forms.Label
    Friend WithEvents lblClockInDate As System.Windows.Forms.Label
    Friend WithEvents lblClockInTime As System.Windows.Forms.Label

    Friend WithEvents Button1 As System.Windows.Forms.Button

    Friend WithEvents btnClockIn As System.Windows.Forms.Button
    Friend WithEvents pnlTimeInfo As System.Windows.Forms.Panel
    Friend WithEvents pnlLogin As System.Windows.Forms.Panel
    Friend WithEvents lblLogin As System.Windows.Forms.Label
    Friend WithEvents btnClockOut As System.Windows.Forms.Button

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnClockIn = New System.Windows.Forms.Button
        Me.pnlTimeInfo = New System.Windows.Forms.Panel
        Me.lblClockInDay = New System.Windows.Forms.Label
        Me.lblClockInDate = New System.Windows.Forms.Label
        Me.lblClockInTime = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.pnlLogin = New System.Windows.Forms.Panel
        Me.btnClockOut = New System.Windows.Forms.Button
        Me.lblLogin = New System.Windows.Forms.Label
        Me.pnlTimeInfo.SuspendLayout()
        Me.pnlLogin.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClockIn
        '
        Me.btnClockIn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClockIn.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnClockIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClockIn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnClockIn.Location = New System.Drawing.Point(648, 488)
        Me.btnClockIn.Name = "btnClockIn"
        Me.btnClockIn.Size = New System.Drawing.Size(128, 64)
        Me.btnClockIn.TabIndex = 2
        Me.btnClockIn.TabStop = False
        Me.btnClockIn.Text = "CLOCK IN"
        Me.btnClockIn.UseVisualStyleBackColor = False
        Me.btnClockIn.Visible = False
        '
        'pnlTimeInfo
        '
        Me.pnlTimeInfo.Controls.Add(Me.lblClockInDay)
        Me.pnlTimeInfo.Controls.Add(Me.lblClockInDate)
        Me.pnlTimeInfo.Controls.Add(Me.lblClockInTime)
        Me.pnlTimeInfo.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.pnlTimeInfo.Location = New System.Drawing.Point(16, 104)
        Me.pnlTimeInfo.Name = "pnlTimeInfo"
        Me.pnlTimeInfo.Size = New System.Drawing.Size(256, 200)
        Me.pnlTimeInfo.TabIndex = 7
        '
        'lblClockInDay
        '
        Me.lblClockInDay.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblClockInDay.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClockInDay.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblClockInDay.Location = New System.Drawing.Point(32, 24)
        Me.lblClockInDay.Name = "lblClockInDay"
        Me.lblClockInDay.Size = New System.Drawing.Size(208, 40)
        Me.lblClockInDay.TabIndex = 1
        Me.lblClockInDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblClockInDate
        '
        Me.lblClockInDate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblClockInDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClockInDate.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblClockInDate.Location = New System.Drawing.Point(32, 72)
        Me.lblClockInDate.Name = "lblClockInDate"
        Me.lblClockInDate.Size = New System.Drawing.Size(208, 40)
        Me.lblClockInDate.TabIndex = 2
        Me.lblClockInDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblClockInTime
        '
        Me.lblClockInTime.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblClockInTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClockInTime.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblClockInTime.Location = New System.Drawing.Point(32, 120)
        Me.lblClockInTime.Name = "lblClockInTime"
        Me.lblClockInTime.Size = New System.Drawing.Size(208, 48)
        Me.lblClockInTime.TabIndex = 3
        Me.lblClockInTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(16, 328)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(112, 40)
        Me.Button1.TabIndex = 6
        Me.Button1.TabStop = False
        Me.Button1.Text = "Bypass Login"
        Me.Button1.Visible = False
        '
        'pnlLogin
        '
        Me.pnlLogin.BackColor = System.Drawing.Color.Black
        Me.pnlLogin.Controls.Add(Me.btnClockOut)
        Me.pnlLogin.Controls.Add(Me.lblLogin)
        Me.pnlLogin.Controls.Add(Me.btnClockIn)
        Me.pnlLogin.Controls.Add(Me.pnlTimeInfo)
        Me.pnlLogin.Controls.Add(Me.Button1)
        Me.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLogin.Location = New System.Drawing.Point(0, 0)
        Me.pnlLogin.Name = "pnlLogin"
        Me.pnlLogin.Size = New System.Drawing.Size(800, 573)
        Me.pnlLogin.TabIndex = 9
        '
        'btnClockOut
        '
        Me.btnClockOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClockOut.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnClockOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClockOut.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnClockOut.Location = New System.Drawing.Point(648, 400)
        Me.btnClockOut.Name = "btnClockOut"
        Me.btnClockOut.Size = New System.Drawing.Size(128, 64)
        Me.btnClockOut.TabIndex = 10
        Me.btnClockOut.TabStop = False
        Me.btnClockOut.Text = "CLOCK OUT"
        Me.btnClockOut.UseVisualStyleBackColor = False
        Me.btnClockOut.Visible = False
        '
        'lblLogin
        '
        Me.lblLogin.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLogin.Location = New System.Drawing.Point(0, 0)
        Me.lblLogin.Name = "lblLogin"
        Me.lblLogin.Size = New System.Drawing.Size(800, 56)
        Me.lblLogin.TabIndex = 9
        Me.lblLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Login
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(800, 573)
        Me.Controls.Add(Me.pnlLogin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Login"
        Me.Text = "Login"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTimeInfo.ResumeLayout(False)
        Me.pnlLogin.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub InitializeOther()
      
        AdjustSystemColors()
        If Not typeProgram = "Online_Demo" Then
            StartUsernameDiscovery()
        Else
            _loginUsername = "eGlobal"
            _loginPassword = "1111"
        End If

        '    Me.ClientSize = New Size(ssX, ssY)

        '      orderInactiveTimer = New Timer
        loginPad = New NumberPad
        loginPad.Location = New Point((ssX - loginPad.Width) / 2, ((ssY - loginPad.Height) / 2) + 40)
        '       lblLogin.Text = "Enter Login"
        Me.pnlLogin.Controls.Add(loginPad)

        MakeLoginPadVisibleNOT()
        '   loginPad.Visible = False


        '       titleHeader.Location = New Point((Me.pnlT.Width - titleHeader.Width) / 2, (Me.pnlTitle.Height - titleHeader.Height) / 2)
        '      Me.titleHeader.BackColor = Me.pnlTitle.BackColor
        '     Me.pnlTitle.Controls.Add(titleHeader)


        '   this is where we will add all automatic clock-in employees to collection??


        RemoveHandler pnlLogin.Click, AddressOf Me.ReceiveFocus
        '      RemoveHandler Me.btnClockIn.Click, AddressOf btnClockIn_Click

        AddHandler updateClockTimerLogin.Tick, AddressOf UpdateClock
        updateClockTimerLogin.Interval = 60000
        updateClockTimerLogin.Start()

        Dim daysBeforeExpiration As Integer

        daysBeforeExpiration = DateDiff(DateInterval.Day, Now, dateOfExpiration)
        If daysBeforeExpiration < 0 Then
            expOverriding = True
            loginPad.Visible = True
            Exit Sub
        ElseIf daysBeforeExpiration < 14 Then
            MsgBox("Your subscription will expire in " & daysBeforeExpiration & " days.")
        End If

        DisplayInitialLogon()


    End Sub

    Private Sub StartUsernameDiscovery()

        pointToServer = DetermineIfServer()

        If pointToServer = False Then        'ourservername = Nothing Then
            ReadTextLinesFromFile()
        Else
            ReadTextLinesFromServer()
        End If

    End Sub

    Private Function DetermineIfServer()

        Dim n As Integer
        Dim c As Char
        Dim tempComputerName As String

        tempComputerName = System.Windows.Forms.SystemInformation.ComputerName

        For n = 0 To tempComputerName.Length - 1
            c = tempComputerName.Chars(n)
            If c = "_" Then
                '    If tempComputerName.Chars(n + 1) = "_" Then
                ' now we know this is a client machine
                ourServerName = tempComputerName.Substring(0, n)
                Return True
                ' End If
            End If
        Next

        '      ourServerName = "kaistudiobk"
        '     Return True

        ourServerName = tempComputerName.Substring(0, n)
        Return False

    End Function


    Sub ReadTextLinesFromFile()
        '       "\\workstation\javatools\somefile.txt"

        Dim oneLine As String
        Dim lineCount As Integer = 1
        Dim i As Integer

        Try
            '  Dim file As New System.IO.StreamReader("\\Phoenix\NETLOGON\login.txt")
            Dim file As New System.IO.StreamReader("c:\Data Files\SpiderPOS\login.txt")
            '       file = New System.IO.StreamReader("c:\Data Files\SpiderPOS\login.txt")

            Do
                oneLine = file.ReadLine()
                If lineCount = 1 Then
                    _loginUsername = oneLine
                    lineCount = 2
                ElseIf lineCount = 2 Then
                    _loginPassword = oneLine
                    lineCount = 3
                    '  oneLine = ""
                End If
            Loop Until oneLine Is Nothing
            file.Close()

        Catch ex As Exception
            '        file.Close()
            '     MsgBox(ex.Message)
        End Try


    End Sub

    Sub ReadTextLinesFromServer()
        '       "\\workstation\javatools\somefile.txt"

        Dim oneLine As String
        Dim lineCount As Integer = 1
        Dim i As Integer

        Try
            '    Dim file As New System.IO.StreamReader("\\" & ourServerName & "\NETLOGON\login.txt")
            Dim file As New System.IO.StreamReader("\\" & ourServerName & "\Data Files\SpiderPOS\login.txt")
            '       file = New System.IO.StreamReader("c:\Data Files\SpiderPOS\login.txt")
            Do
                oneLine = file.ReadLine()
                If lineCount = 1 Then
                    _loginUsername = oneLine
                    lineCount = 2
                ElseIf lineCount = 2 Then
                    _loginPassword = oneLine
                    lineCount = 3
                    '  oneLine = ""
                End If
            Loop Until oneLine Is Nothing
            file.Close()

        Catch ex As Exception
            '        file.Close()
            '     MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub AdjustSystemColors()

        '    If SystemInformation.ComputerName = "VAIO" Then
        '   c4 = Color.SlateBlue
        '  c6 = Color.SlateBlue
        '' End If

    End Sub

    Friend Sub DisplayInitialLogon()

        If companyInfo.locationUsername = Nothing Then
            initLogon = New DataSet_Builder.LoginInit_UC   'NumberPad
            initLogon.Location = New Point((ssX - initLogon.Width) / 2, (ssY - initLogon.Height) / 2)
            initLogon.InputUSERinfo(_loginUsername, _loginPassword)

            Me.Controls.Add(initLogon)
            Me.initLogon.BringToFront()
            Me.initLogon.Focus()
        Else

            InitialLogIn(companyInfo.locationUsername, companyInfo.locationPassword)

        End If

    End Sub

    Private Sub TestInitLogon() Handles initLogon.SummitLogin
        Dim loginEnterUsername As String
        Dim loginEnterPassword As String


        loginEnterUsername = initLogon.LoginUsername
        loginEnterPassword = initLogon.LoginPassword

        InitialLogIn(loginEnterUsername, loginEnterPassword)


        '***************    ?????????????
        '   we must have all salaried personnel add to working employees
        '   we must change test below to see if first employee (now it is workingemployees.count = 0)
        '   if first employee is not salaried we must auto login

    End Sub

    Private Sub QSClockOutAccept(ByVal sender As Object, ByVal e As System.EventArgs) Handles openInfo.AcceptInformation

        StartClockOut(currentServer, False)
        MakeClockOutBooleanFalse()
        loginPad.btnNumberClear_Click()
        pnlLogin.Visible = False
        MakeLoginPadVisibleNOT()

    End Sub

    Private Sub QSClockOutReject(ByVal sender As Object, ByVal e As System.EventArgs) Handles openInfo.RejectInformation

        MakeClockOutBooleanFalse()
        loginPad.btnNumberClear_Click()
        pnlLogin.Visible = True
        MakeLoginPadVisible()

    End Sub
    Private Sub Login_Entered(ByVal sender As Object, ByVal e As System.EventArgs) Handles loginPad.NumberEntered

        If expOverriding = True Then
            If loginPad.NumberString = "27315" Then
                dateOfExpiration = "01/01/2099"
                expOverriding = False

                DisplayInitialLogon()
            Else
                Me.Dispose()
            End If

        Else
            Dim loginEnter As String
            loginEnter = loginPad.NumberString
            LoginRoutine(loginEnter)
        End If


    End Sub

    Private Sub LoginRoutine(ByVal loginEnter As String)

        Dim isClockedIn As Integer
        Dim doesEmpNeedToClockOut As Boolean
        Dim emp As DataSet_Builder.Employee

        emp = GenerateOrderTables.TestUsernamePassword(loginEnter, clockOutActiveQS) 'False)

        If Not emp Is Nothing Then
            If clockOutActiveQS = True Then
                Dim yesOpenTables As Boolean
                If loginEnter.Length < 8 Then
                    MsgBox("Enter both EmployeeID as Passcode")
                    Exit Sub
                End If
                doesEmpNeedToClockOut = TestClockOut(loginEnter)
                If doesEmpNeedToClockOut = False Then
                    MakeClockOutBooleanFalse()
                    loginPad.btnNumberClear_Click()
                    MsgBox(emp.FullName & " does not need to Clock Out")
                    Exit Sub
                End If

                '  check to see if there are any open tables           **********************
                yesOpenTables = GenerateOrderTables.AnyOpenTables(emp)
                If currentServer Is Nothing Then
                    currentServer = New Employee
                End If
                If currentClockEmp Is Nothing Then
                    currentClockEmp = New Employee
                End If
                currentServer = emp
                currentClockEmp = emp

                If yesOpenTables = True Then
                    openInfo = New DataSet_Builder.Information_UC(emp.FullName & " still has open checks. Press here to clock out or enter Tip Adjustments.")
                    openInfo.Location = New Point((Me.Width - openInfo.Width) / 2, (Me.Height - openInfo.Height) / 2)
                    Me.Controls.Add(openInfo)
                    openInfo.BringToFront()
                    '       Exit Sub
                Else
                    StartClockOut(emp, False)
                End If

                MakeClockOutBooleanFalse()

                loginPad.btnNumberClear_Click()
                pnlLogin.Visible = False
                MakeLoginPadVisibleNOT()
                '   loginPad.Visible = False
                Exit Sub
            End If

            Try
                isClockedIn = ActuallyLogIn(emp)
                If isClockedIn = -999 Then
                    'this means connection down but is a Salaried employee
                    MsgBox("Connection Error. Only a Manager may Login")
                    LoginMgrAfterConnectFail(emp)
                    Exit Sub
                End If
            Catch ex As Exception
                CloseConnection()
                MsgBox("Connection Error. Only a Manager may Login")
                LoginMgrAfterConnectFail(emp)
                Exit Sub
            End Try

            If isClockedIn = 0 Then
                '444       MsgBox(emp.FullName & " is not clocked in. Please Clock in after open Spider POS.")
                If currentServer Is Nothing Then
                    currentServer = New Employee
                End If
                currentServer = emp
                AttemptingToClockIn(loginEnter)
                Exit Sub

            ElseIf isClockedIn = 1 Then
                '222   LoginEmployee(emp)
                PerformEmployeeFunctions(emp)
            Else
                MsgBox("Employee Is Clocked in more than once. Please See Manager.")
            End If
        End If

        loginPad.btnNumberClear_Click()
        pnlLogin.Visible = False
        MakeLoginPadVisibleNOT()
        '  MakeClockOutBooleanFalse()

    End Sub

    Private Sub LoginMgrAfterConnectFail(ByRef emp As DataSet_Builder.Employee)

        Dim emp2 As DataSet_Builder.Employee

        For Each emp2 In AllEmployees
            If emp2.EmployeeID = emp.EmployeeID Then
                If emp2.SystemMgmtAll = True Or emp2.SystemMgmtLimited = True Or emp2.OperationMgmtAll = True Then
                    managementScreen = New Manager_Form(emp2, True)  'emp, usernameEntered?
                    managementScreen.Location = New Point(0, 0)
                    Me.Controls.Add(managementScreen)
                    managementScreen.BringToFront()
                    readAuth.ActiveScreen = "Manager"

                    loginPad.btnNumberClear_Click()
                    pnlLogin.Visible = False
                    MakeLoginPadVisibleNOT()
                    '     loginPad.Visible = False
                    '        PerformEmployeeFunctions(emp2)
                    Exit Sub
                End If
            End If
        Next

        MsgBox("Connection Error. Only a Manager may Login")

    End Sub

    Private Sub StartOfProgram(ByVal empName As String)
       
        GenerateOverrideCodes() 'for now not pulling from database

        Try
            FillLocalTablesStartOfProgram()
        Catch ex As Exception
            'this means local server down
            CloseConnection()
            '    MsgBox(ex.Message)
            If MsgBox("Local Server Down. Attempt to Reset. Otherwise Select YES and Connect to DataCenter.", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                localConnectServer = "Phoenix\Phoenix"
                connectserver = localConnectServer
                RestateConnectionString(sql.cn, connectserver)
                Try
                    FillLocalTablesStartOfProgram()
                Catch ex2 As Exception
                    CloseConnection()
                    MsgBox("Local And Datacenter connection down. Call Spider POS (404) 869-4700")
                    Exit Sub
                End Try
            Else
                Me.Dispose()
            End If
        End Try

        '3 lines below is temp 999
        '     CreateTerminals()
        '    DisplayOpeningScreen(empName)
        '   Exit Sub

        ' we are kind of repeating downloading some of this
        'mostly because we need terminal data here
        If mainServerConnected = True Then
            If PopulateMenu(True) = True Then
                'we get this info ONLY from phoenix
                currentTerminal.menuLoadedDate = Now

                tablesFilled = True
                DisplayOpeningScreen(empName)
            Else
                MsgBox("Connection Down, Populating Menu. Select saved menu.")
                ServerNOTConectedStartOfProgram()
                'not sure       PopulateAllEmployeeColloection()
            End If
        Else
            GenerateOrderTables.CreateTerminals()
            PopulateAllEmployeeColloection()
            tablesFilled = True
            DisplayOpeningScreen(empName)
        End If

        VerifyTerminals()

    End Sub

    Private Sub VerifyTerminals()

        If companyInfo.LocationID = "000002" Or companyInfo.LocationID = "000022" Or companyInfo.LocationID = "000023" Or companyInfo.LocationID = "000024" Or companyInfo.LocationID = "000026" Or companyInfo.LocationID = "000027" Or companyInfo.LocationID = "000029" Or companyInfo.LocationID = "000081" Then
            Exit Sub
        End If
        'for placing encrypted info to db
        '      Dim tempadd As String
        '     Dim tempAddResult As String
        '    tempadd = "000BAB19CCDF" 'wrong one:"00FF68FDCC89" 'change to address we are adding
        '   tempAddResult = CryOutloud.Encrypt(tempadd, "test")
        '  MsgBox(tempAddResult)
     
        Dim terminalVerified As Boolean = False
        Dim theNetworkInterfaces() As System.Net.NetworkInformation.NetworkInterface = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()

        If currentTerminal.PhyAdd.Length > 0 Then 'And Not currentTerminal.PhyAdd Is Nothing Then
            Try
                currentTerminal.PhyAdd = CryOutloud.Decrypt(currentTerminal.PhyAdd, "test")
                If currentTerminal.PhyAdd.Length = 0 Then
                    'means they tried to use another address
                    ' but will fail here and Catch
                    Me.Dispose()
                End If
            Catch ex As Exception
                'means they tried to use another address
                Me.Dispose()
                terminalVerified = False
            End Try
        End If
    
        If Not companyInfo.CompanyID = "rasoi2" Then 'And Not System.Windows.Forms.SystemInformation.ComputerName = "EGLOBALMAIN" Then 'And Not companyInfo.CompanyID = "001111" Then 'currentTerminal.PhyAdd = "0000" Then 'this was a null VALUE
            terminalVerified = True
        Else
            Try
                For Each currentInterface As System.Net.NetworkInformation.NetworkInterface In theNetworkInterfaces
                    If currentInterface.GetPhysicalAddress.ToString = currentTerminal.PhyAdd Then
                        terminalVerified = True
                        Exit For
                    End If
                    '         MessageBox.Show(currentInterface.GetPhysicalAddress().ToString())
                Next
            Catch ex As Exception
                terminalVerified = False
            End Try

        End If

        If System.Windows.Forms.SystemInformation.ComputerName = "EGLOBALMAIN" Then
            terminalVerified = True
        End If
        If terminalVerified = False Then
        '    MsgBox("Could not verify this software is licenced to this computer. Please reBoot your computer.")
            Me.Dispose()
        End If
        '   Return terminalVerified
    End Sub

    Private Sub FillLocalTablesStartOfProgram()
      
        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        PopulateOrderTables(True) '(tableNumber)
        PopulateAllTablesWithStatus(True)       '   "AllTables"
        InitializeSeatingChart()
        InitializeTabScreen()
        PopulateLoggedInEmployees(True)
        '       PopulateEmployeeData() 'this is done locally and phoenix
        ' this is done in PopulateMenu
        sql.cn.Close()

    End Sub

    Private Sub StartOfProgram222(ByVal empName As String)

        '  currentTerminal = New Terminal

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            PopulateOrderTables(True) '(tableNumber)
            PopulateAllTablesWithStatus(True)       '   "AllTables"
            PopulateLoggedInEmployees(True)
            PopulateEmployeeData()

            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

        '**********************************
        'start of filling ds
        'we get this info ONLY from phoenix
        '**********************************

        Try
            If mainServerConnected = True Then
                GenerateOrderTables.TempConnectToPhoenix()
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                PopulateMenuSupport()
                PopulateNONOrderTables()
                PopulateTerminalData()               'FloorPlan
                sql.cn.Close()
                GenerateOrderTables.ConnectBackFromTempDatabase()
            End If

            GenerateOrderTables.CreateTerminals()
            tablesFilled = True

            PopulateAllEmployeeColloection()

        Catch ex As Exception
            'nned to reload all Info stored on Phoenix from XML
            CloseConnection()
            GenerateOrderTables.ConnectBackFromTempDatabase()
            '     openProgram = New OpeningScreen(Nothing)
            '222
            ServerNOTConectedStartOfProgram()
            Try
                '      LoadStarterDataSet()
                PopulateAllEmployeeColloection()
            Catch ex2 As Exception
                MsgBox(ex2.Message, "   Can't load Starter Menu. Call spiderPOS at 404.869.4700")
            End Try
            Exit Sub
        End Try

        '      SetUpPrimaryKeys()
        '   Formats TabsAndTables 
        InitializeSeatingChart()
        DisplayOpeningScreen(empName)

    End Sub

    Private Function CreateMenuString222(ByVal mc As Integer, ByRef menuName As String)

        Dim oRow As DataRow

        For Each oRow In ds.Tables("MenuChoice").Rows
            If oRow("MenuID") = mc Then
                menuName = menuName + oRow("MenuName")
            End If
        Next

        '    Return menuName

    End Function


    Private Sub PopulateOpenTablesAtStartup222()
        '   currently not using
        '   this is to create a list of open tables that should be closed

        '    dsOrder.Tables("OpenTables").Clear()
        '   dsOrder.Tables("OpenTabs").Clear()

        Dim yesterdaysDate As Date
        Dim todaysDate As Date
        Dim oRow As DataRow
        Dim firstExpNum As Int64


        yesterdaysDate = Format(Today.AddDays(-888), "D")
        '    todaysDate = Format(Today.AddDays(-1), "D")


        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            '   this is to check for any tables tabs left open from previous day   ???
            '   we do not do this by batch because we want all previous batches
            '        sql.SqlSelectCommandOpenTables.Parameters("@CompanyID").Value = CompanyID
            '       sql222.SqlSelectCommandOpenTables.Parameters("@LocationID").Value = companyInfo.LocationID
            '      sql222.SqlSelectCommandOpenTables.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
            '       sql.SqlDataAdapterOpenTables.Fill(dsOrder.Tables("OpenTables"))

            '        sql.SqlSelectCommandOpenTabs.Parameters("@CompanyID").Value = CompanyID
            '   sql222.SqlSelectCommandOpenTabs.Parameters("@LocationID").Value = companyInfo.LocationID
            '     sql222.SqlSelectCommandOpenTabs.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
            '      sql.SqlDataAdapterOpenTabs.Fill(dsOrder.Tables("OpenTabs"))



            '   Backup

            '         sql.SqlSelectCommandAvailTablesTerminal.Parameters("@CompanyID").Value = CompanyID
            '         sql.SqlSelectCommandAvailTablesTerminal.Parameters("@LocationID").Value = LocationID
            '         sql.SqlSelectCommandAvailTablesTerminal.Parameters("@TerminalID").Value = currentTerminal.TermID
            '        sql.SqlSelectCommandAvailTablesTerminal.Parameters("@DailyCode").Value = currentTerminal.currentDailyCode
            '       sql.SqlDataAdapterAvailTablesTerminal.Fill(dsBackup.Tables("AvailTablesTerminal"))

            '         sql.SqlSelectCommandAvailTabsTerminal.Parameters("@CompanyID").Value = CompanyID
            '         sql.SqlSelectCommandAvailTabsTerminal.Parameters("@LocationID").Value = LocationID
            '        sql.SqlSelectCommandAvailTabsTerminal.Parameters("@TerminalID").Value = currentTerminal.TermID
            ''        sql.SqlSelectCommandAvailTabsTerminal.Parameters("@DailyCode").Value = currentTerminal.currentDailyCode
            '        sql.SqlDataAdapterAvailTabsTerminal.Fill(dsBackup.Tables("AvailTabsTerminal"))

            '   ********************
            '   Opne Orders and Payments


            '       sql.SqlSelectCommandOOTerminal.Parameters("@DailyCode").Value = currentTerminal.currentDailyCode
            '       sql.SqlSelectCommandOOTerminal.Parameters("@CompanyID").Value = CompanyID
            '       sql.SqlSelectCommandOOTerminal.Parameters("@LocationID").Value = LocationID
            '      sql.SqlDataAdapterOOTerminal.Fill(dsBackup.Tables("OpenOrdersTerminal"))
            '
            '           sql.SqlSelectCommandPaymentsTerminal.Parameters("@DailyCode").Value = currentTerminal.currentDailyCode
            '          sql.SqlSelectCommandPaymentsTerminal.Parameters("@CompanyID").Value = CompanyID
            '         sql.SqlSelectCommandPaymentsTerminal.Parameters("@LocationID").Value = LocationID
            '        sql.SqlSelectCommandPaymentsTerminal.Parameters("@TerminalID").Value = currentTerminal.TermID
            '       sql.SqlDataAdapterPaymentsTerminal.Fill(dsBackup.Tables("PaymentsAndCreditsTerminal"))
            '        End If



            '   *** I don;t think this should be by Terminal    ???
            '          sql.SqlSelectCommandESCTerminal.Parameters("@CompanyID").Value = CompanyID
            '         sql.SqlSelectCommandESCTerminal.Parameters("@LocationID").Value = LocationID
            '        sql.SqlSelectCommandESCTerminal.Parameters("@TerminalID").Value = currentTerminal.TermID
            '       sql.SqlSelectCommandESCTerminal.Parameters("@DailyCode").Value = currentTerminal.currentDailyCode
            '      sql.SqlDataAdapterESCTerminal.Fill(dsBackup.Tables("ESCTerminal"))

            '   Employee
            '         sql.SqlSelectCommandEmployeeTerminal.Parameters("@CompanyID").Value = CompanyID
            '        sql.SqlSelectCommandEmployeeTerminal.Parameters("@LocationID").Value = LocationID
            '       sql.SqlDataAdapterEmployeeTerminal.Fill(dsBackup.Tables("EmployeeTerminal"))

            '   ClockedId  / LoggedIn


            sql.cn.Close()

        Catch ex As Exception
            CloseConnection()

        End Try

    End Sub

    Private Sub DailyClosed(ByVal emp As Employee)

        InitializeOpeningScreen()
        DisplayOpeningScreen(emp.FullName)

    End Sub


    Private Sub InitializeOpeningScreen()

        openProgram = New OpeningScreen(companyInfo.companyName)
        openProgram.Location = New Point(0, 0)
        Me.Controls.Add(openProgram)
        openProgram.Hide()

    End Sub

    Private Sub DisplayOpeningScreen(ByVal empName As String)

        PopulateManagementSwipeCollection()
        'we must do this here because Login is where we define ReadAuth_MWE

        '   *********************
        '   Go to Opening Screen only if the first terminal 

        openProgram.Show()
        openProgram.BringToFront()
        openProgram.UpdateOpeningInfo()

        RemoveHandler pnlLogin.Click, AddressOf Me.ReceiveFocus

    End Sub

    Private Sub PopulateManagementSwipeCollection()

        Dim tempDT As New DataTable
        Dim oRow As DataRow
        '    Dim swipeEmployee As New ReadCredit_MWE.Employee_MWE
        Dim oldEmployee As Employee

        If dsEmployee.Tables("AllEmployees").Rows.Count > 0 Then
            tempDT = dsEmployee.Tables("AllEmployees")
        Else
            tempDT = dsStarter.Tables("StarterAllEmployees")
        End If

        If tempDT Is Nothing Then
            Exit Sub
        End If

        If companyInfo.processor = "MerchantWare" Then
            '444      readAuth_MWE.SwipeCodeEmployees_MWE.Clear()
            For Each oRow In tempDT.Rows
                If Not oRow("SwipeCode") Is DBNull.Value Then
                    'this means there is a swipe code for this employee
                    '         swipeEmployee = New Employee_MWE
                    For Each oldEmployee In AllEmployees
                        If oldEmployee.EmployeeID = oRow("EmployeeID") Then
                            'id is the primary Key
                            '444       readAuth_MWE.AddEmployeeToSwipeCodeEmployeesEmployeeCollectionMWE(oRow("EmployeeNumber"), oRow("SwipeCode"), oRow("Passcode")) 'swipeEmployee)
                        End If
                    Next
                End If
            Next
        End If

    End Sub

    Private Sub OpenScreenClosed() Handles openProgram.OpeningScreenClosing

        SetDateTime()

        Exit Sub
        '222

        '   Me.Show()

        SetDateTime()
        openProgram.Dispose()

    End Sub

    Private Sub DisplayCurrentLoggedInEmployees()
        '   not here

    End Sub

    Private Sub PopulateEmployeeCollectionWithLoggedIn()


    End Sub

    Private Sub UpdateClock(ByVal sender As Object, ByVal e As System.EventArgs)
        SetDateTime()
        TestForShiftAndMenuChanges()

    End Sub


    Private Sub TestForShiftAndMenuChanges()

        'shift codes
        Dim sRow As DataRow
        If ds.Tables("ShiftCodes").Rows.Count > 0 Then
            If Not ds.Tables("ShiftCodes").Rows(0)("ShiftID") = currentTerminal.CurrentShift Then
                ' 2 things
                '1. we stop from changing after midnight
                '2. we skip if its the last shift of the day
                For Each sRow In ds.Tables("ShiftCodes").Rows
                    If TimeOfDay.Hour > sRow("TimeStart").hour Then
                        currentTerminal.CurrentShift = sRow("ShiftID")
                        Exit For
                    ElseIf sRow("TimeStart").hour = TimeOfDay.Hour Then
                        If TimeOfDay.Minute > sRow("TimeStart").minute Then
                            currentTerminal.CurrentShift = sRow("ShiftID")
                            Exit For
                        End If
                    End If
                Next
            End If
        End If

        'menu aut change
        If ds.Tables("MenuChoice").Rows.Count > 0 Then
            For Each sRow In ds.Tables("MenuChoice").Rows
                If Not sRow("AutoChange") Is DBNull.Value Then
                    If sRow("LastOrder") = 2 Then
                        ' we only test the latter menu, so we never change to previous automatically
                        If sRow("MenuID") = currentTerminal.currentPrimaryMenuID Then   'currentTerminal.CurrentMenuID Then
                            Exit For
                        Else

                            If TimeOfDay.Hour > sRow("AutoChange").hour Then
                                SwitchToSecondaryMenu()
                                '    secondaryMenuID = primaryMenuID
                                '   primaryMenuID = sRow("MenuID")
                                '  we need to change in OpenBusiness
                                Exit For
                            ElseIf sRow("AutoChange").hour = TimeOfDay.Hour Then
                                If TimeOfDay.Minute > sRow("AutoChange").minute Then
                                    SwitchToSecondaryMenu()
                                    '     secondaryMenuID = primaryMenuID
                                    '    primaryMenuID = sRow("MenuID")
                                    Exit For
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Public Sub AssignStatus222(ByVal tableNumber As Integer, ByVal ss As Integer, ByVal st As DateTime)
        Dim newTable As New PhysicalTable

        newTable.PhysicalTableNumber = tableNumber
        newTable.CurrentStatus = ss
        newTable.CurrentStatusTime = st

        currentPhysicalTables.Add(newTable)

    End Sub

    Private Sub ReceiveFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlLogin.Click, MyBase.Click

        DisplayLoginScreen()
        SetDateTime()
        If currentTerminal.CurrentDailyCode = 0 Then
            InitializeOpeningScreen()
            DisplayOpeningScreen(companyInfo.companyName)
        End If

    End Sub

    Private Sub DisplayLoginScreen()

        MakeLoginPadVisible()
        pnlLogin.Visible = True

        If currentTerminal.CurrentDailyCode > 0 Then
            'update terminal

        Else

        End If


    End Sub

    Private Sub MakeLoginPadVisible()
        lblLogin.Visible = True
        If currentTerminal.TermMethod = "Quick" Then
        End If
        btnClockOut.Visible = True
        btnClockIn.Visible = True
        loginPad.Visible = True
    End Sub

    Private Sub MakeLoginPadVisibleNOT()
        lblLogin.Visible = False
        btnClockOut.Visible = False
        btnClockIn.Visible = False
        loginPad.Visible = False
        '    Me.pnlLogin.Controls.Add(Me.pnlTimeInfo)
        Button1.Visible = False

    End Sub







    '***************************************
    '
    '
    '***************************************


    Private Sub InitializeTabScreen()

        DeliveryScreen = New Tab_Screen
        DeliveryScreen.Location = New Point(50, 50) '((Me.Width - DeliveryScreen.Width - 10) / 2), ((Me.Height - DeliveryScreen.Height) / 2))
        Me.Controls.Add(DeliveryScreen)
        DeliveryScreen.Visible = False

    End Sub

    Private Sub FiringTabScreen(ByVal startInSearch As String, ByVal searchCriteria As String) Handles activeOrder.FireTabScreen, QuickOrder.FireTabScreen, activeSplit.FireTabScreen, mgrActiveSplit.FireTabScreen
        Try
            If DeliveryScreen Is Nothing Then
                InitializeTabScreen()
            End If
        Catch ex As Exception
            InitializeTabScreen()

        Finally
            'make visible
            '          If startInSearch = "Account" Then
            '        DeliveryScreen.SearchAccount(searchCriteria)
            '      ElseIf startInSearch = "Phone" Then
            '       DeliveryScreen.SearchPhone(searchCriteria)
            '     ElseIf startInSearch = "TabID" Then
            '      DeliveryScreen.SearchTabID(searchCriteria)
            '    End If

            '       If currentTable.TabID = -22 Then
            '    DeliveryScreen.currentSearchBy = "Phone"
            '   Else
            ' i do not know the difference 
            '  DeliveryScreen.currentSearchBy = startInSearch
            ' End If

            '    DeliveryScreen.BindDataAfterSearch()

            DeliveryScreen.DetermineSearch(startInSearch, searchCriteria)
            DeliveryScreen.Visible = True
            DeliveryScreen.BringToFront()

            readAuth.ActiveScreen = "DeliveryScreen"
            readAuth.GiftAddingAmount = False
            readAuth.IsNewTab = False

        End Try

    End Sub

    Private Sub InitializeSeatingChart()

        SeatingChart = New Seating_ChooseTable
        SeatingChart.Location = New Point(0, 0)
        '     SeatingChart.BringToFront()
        SeatingChart.Visible = False
        Me.Controls.Add(SeatingChart)

    End Sub

    Private Sub FiringSeatingChart(ByVal fromMgmt As Boolean) Handles tableScreen.FireSeatingChart, managementScreen.FireSeatingChart
        ' do last Floor Plan By Employee
        Try
            If SeatingChart Is Nothing Then
                InitializeSeatingChart()
            End If
            RecallSeatingChart(fromMgmt, False)
        Catch ex As Exception
            InitializeSeatingChart()
            RecallSeatingChart(fromMgmt, False)
        End Try

    End Sub

    Private Sub FiringSeatingTab(ByVal startedFrom As String, ByVal tn As String) Handles tableScreen.FireSeatingTab, managementScreen.FireSeatingTab, activeOrder.FireSeatingTab, QuickOrder.FireSeatingTab

        If startedFrom = "TableScreen" Then
            tableScreen.DisableTables_Screen()
            '  tableScreen.Visible = False
        End If

        If SeatingTab Is Nothing Then
            SeatingTab = New Seating_EnterTab()
            SeatingTab.Location = New Point((Me.Width - SeatingTab.Width) / 2, (Me.Height - SeatingTab.Height) / 2)
            Me.Controls.Add(SeatingTab)
            SeatingTab.RestartSeatingTabWithName(startedFrom, tn)
        Else
            SeatingTab.RestartSeatingTabWithName(startedFrom, tn)
            SeatingTab.Visible = True
        End If
        SeatingTab.BringToFront()

        If startedFrom = "OrderScreen" Then
            readAuth.IsNewTab = False
            SeatingTab.StartedFrom = "OrderScreen"
        ElseIf startedFrom = "Manager" Then
            readAuth.IsNewTab = True
            SeatingTab.StartedFrom = "Manager"
        Else
            readAuth.IsNewTab = True
            ' SeatingTab.StartedFrom = "Manager"
        End If
        readAuth.GiftAddingAmount = False
        readAuth.ActiveScreen = "SeatingTab" 'startedFrom

    End Sub

    Private Sub NewAddNewTab() Handles SeatingTab.OpenNewTabEvent

        '   -999 for tabID will tell it to generate New TabID (which will be experience Number)
        '   later we will have a look-up for returning customers
        Dim newTabNameString As String
        newTabNameString = SeatingTab.NewTabName

        If SeatingTab.StartedFrom = "TableScreen" Then
            OpenNewTab(-999, newTabNameString)
            tableScreen.EnableTables_Screen()
            '   tableScreen.InitializeScreen()
            OrderScreen(Nothing)

        ElseIf SeatingTab.StartedFrom = "Manager" Then
            OpenNewTab(-999, newTabNameString)
            MgrOrderScreen()

        ElseIf SeatingTab.StartedFrom = "OrderScreen" Then
            currentTable.TabName = SeatingTab.NewTabName
            currentTable.MethodUse = SeatingTab.MethedUse
            LoadTabIDinExperinceTable()
            UpdateTableInfo()
            '  OrderScreen(Nothing)
            readAuth.ActiveScreen = "OrderScreen"

            If currentTerminal.TermMethod = "Quick" Then
                QuickOrder.EnableControls()
            Else
                activeOrder.EnableControls()
            End If
            ' LoadTabIDinExperienceNumber (we really don't have a tab ID???)
        End If

        SeatingTab.Visible = False
        readAuth.IsNewTab = False
        readAuth.GiftAddingAmount = False
    End Sub

    Private Sub NewAddNewTakeOutTab() Handles SeatingTab.OpenNewTakeOutTab

        '   -990 for tabID is TAKE OUT

        Dim newTabNameString As String
        newTabNameString = SeatingTab.NewTabName

        If SeatingTab.StartedFrom = "TableScreen" Then
            If SeatingTab.MethedUse = "Take Out" Then
                OpenNewTab(-990, newTabNameString) ', False, Nothing)
            Else     ' pick up
                OpenNewTab(-991, newTabNameString) ', False, Nothing)
            End If
            tableScreen.EnableTables_Screen()
            '    tableScreen.InitializeScreen()
            OrderScreen(Nothing)  'readAuth.ActiveScreen is set to OrderScreen in sub

        ElseIf SeatingTab.StartedFrom = "Manager" Then
            If SeatingTab.MethedUse = "Take Out" Then
                OpenNewTab(-990, newTabNameString) ', False, Nothing)
            Else     ' pick up
                OpenNewTab(-991, newTabNameString) ', False, Nothing)
            End If
            MgrOrderScreen()

        ElseIf SeatingTab.StartedFrom = "OrderScreen" Then
            currentTable.TabName = SeatingTab.NewTabName
            currentTable.MethodUse = SeatingTab.MethedUse
            LoadTabIDinExperinceTable()
            UpdateTableInfo()
            '   OrderScreen(Nothing)
            If currentTerminal.TermMethod = "Quick" Then
                If SeatingTab.MethedUse = "Take Out" Then
                    QuickOrder.wasPickupMethod = False
                Else     ' pick up
                    QuickOrder.wasPickupMethod = True
                End If
                QuickOrder.EnableControls()
                '           QuickOrder.tabIdentifierDisplaying = False
            Else
                If SeatingTab.MethedUse = "Take Out" Then
                    activeOrder.wasPickupMethod = False
                Else     ' pick up
                    activeOrder.wasPickupMethod = True
                End If
                activeOrder.EnableControls()
                '       activeOrder.tabIdentifierDisplaying = False
            End If
        End If

        SeatingTab.Visible = False
        readAuth.IsNewTab = False
        readAuth.GiftAddingAmount = False

    End Sub

    Private Sub OpenNewTabStep222(ByVal tabId As Int64, ByVal tabName As String, ByVal isDineIn As Boolean, ByRef tabAccountInfo As DataSet_Builder.Payment)
        ' there is another OpenNewTab in Table_Screen_Bar ?????

        'new
        '       If SeatingTab.FromManager = True Then
        'MgrOrderScreen()
        '     Else
        '     OrderScreen(Nothing)
        '     End If

        Exit Sub '444


        Dim expNum As Int64
        Dim tktNum As Integer
        Dim isCurrentlyHeld As Boolean
        Dim satTm As DateTime

        If tabId = -888 Or currentTerminal.TermMethod = "Quick" Then
            tktNum = CreateNewTicketNumber()
            If tabName = "New Tab" Then
                tabName = "Tkt# " & tktNum.ToString
            End If
        Else
            '444      If tabName = "New Tab" Then
            'somehow this is making program change Method Use to TakeOut
            '      tktNum = CreateNewTicketNumber()
            '     tabName = "Tkt# " & tktNum.ToString
            '    Else
            tktNum = 0
            '     End If
        End If

        expNum = CreateNewExperience(currentServer.EmployeeID, Nothing, tabId, tabName, 1, 2, tktNum, 0, currentServer.LoginTrackingID)
        isCurrentlyHeld = PopulateThisExperience(expNum, False)

        currentTable = New DinnerTable
        currentTable.ExperienceNumber = expNum
        currentTable.IsTabNotTable = True
        currentTable.TabID = tabId
        currentTable.TabName = tabName
        currentTable.TableNumber = 0
        currentTable.TicketNumber = tktNum
        currentTable.EmployeeID = currentServer.EmployeeID
        currentTable.CurrentMenu = currentTerminal.currentPrimaryMenuID 'primaryMenuID  'this is the system menu - can change during order process
        currentTable.StartingMenu = currentTerminal.currentPrimaryMenuID 'primaryMenuID
        currentTable.NumberOfCustomers = 1      'is 1 when you first open
        currentTable.NumberOfChecks = 1
        currentTable.LastStatus = 2
        currentTable.SatTime = Now
        currentTable.ItemsOnHold = 0
        currentTable.MethodUse = SeatingTab.MethedUse
        tabAccountInfo.experienceNumber = currentTable.ExperienceNumber

        StartOrderProcess(currentTable.ExperienceNumber)

        'new
        If SeatingTab.StartedFrom = "Manager" Then
            MgrOrderScreen()
        Else
            OrderScreen(Nothing)
        End If

    End Sub

    Private Sub FiringOverrideSeatingChart(ByVal fromMgmt As Boolean) Handles managementScreen.OverrideTableStatus

        Try
            RecallSeatingChart(fromMgmt, True)
        Catch ex As Exception
            InitializeSeatingChart()
            RecallSeatingChart(fromMgmt, True)
        End Try

    End Sub

    Private Sub RecallSeatingChart(ByVal fromMgmt As Boolean, ByVal overridingStatus As Boolean)

        SeatingChart.StartedFromManager = fromMgmt
        SeatingChart.OverrideAvail = overridingStatus
        SeatingChart.AdjustTableColor()
        SeatingChart.Visible = True
        SeatingChart.BringToFront()

    End Sub

    Private Sub CancelNewTable(ByVal sender As Object, ByVal e As System.EventArgs) Handles SeatingChart.NoTableSelected

        If SeatingChart.StartedFromManager = True Then
            managementScreen.Visible = True
            managementScreen.ReinitializeWithoutLogon(False, False)
            ' we did not dispose management screen when bringing seating chart just in case we open order
            'b/c there we are disposing mangerment screen
        End If

        'we need this b/c the "AvailTables" is cleared
        '       If typeProgram = "Online_Demo" Then
        '      GenerateOrderTables.PopulateTabsAndTables(currentServer, currentTerminal.CurrentDailyCode, False, False, Nothing)
        '     End If

    End Sub

    Private Sub CancelNewTab() Handles SeatingTab.CancelNewTab

        If SeatingTab.StartedFrom = "TableScreen" Then
            tableScreen.EnableTables_Screen()
            tableScreen.InitializeScreen()
            readAuth.ActiveScreen = "TableScreen"
        ElseIf SeatingTab.StartedFrom = "OrderScreen" Then
            If currentTerminal.TermMethod = "Quick" Then
                QuickOrder.EnableControls()
            Else
                activeOrder.EnableControls()
            End If
        Else
            PerformEmployeeFunctions(currentServer)
        End If

        SeatingTab.Visible = False

    End Sub

    Private Sub InitializeSplitChecks(ByVal _isFromManager As Boolean) ', ByVal _goingToSelectedCheck As Boolean)

        activeSplit = New SplitChecks() 'False, True)    'false not from manager
        activeSplit.Location = New Point(0, 0)
        activeSplit.DisplayCloseCheck(_isFromManager)
        Me.Controls.Add(activeSplit)

    End Sub

    Private Sub ForceSendOrder(ByVal alsoClose As Boolean) Handles activeSplit.SendOrder
        If currentTerminal.TermMethod = "Quick" Then
            If QuickOrder Is Nothing Then
                QuickOrder = New term_OrderForm(False, True, Nothing)
                QuickOrder.Location = New Point(0, 0)
                Me.Controls.Add(QuickOrder)
            End If
            Me.QuickOrder.SendingOrderRoutine()
        Else
            If activeOrder Is Nothing Then
                activeOrder = New term_OrderForm(False, True, Nothing)
                activeOrder.Location = New Point(0, 0)
                Me.Controls.Add(activeOrder)
            End If
            Me.activeOrder.SendingOrderRoutine()
        End If

    End Sub

    '************************
    '   Delivery Screen
    '************************

    Private Sub TabReorderButtonSelected(ByVal dt As DataTable, ByVal tabTestNeeded As Boolean) Handles DeliveryScreen.SelectedReOrder '444activeSplit.SelectedReOrder

        If tabTestNeeded = True Then
            TabIDTest()
        End If

        If currentTerminal.TermMethod = "Quick" Then
            Me.QuickOrder.TabReorderButtonSelected(dt, False) 'dsCustomer.Tables("TabPreviousOrdersbyItem"))
        Else
            Me.activeOrder.TabReorderButtonSelected(dt, False) 'dsCustomer.Tables("TabPreviousOrdersbyItem"))
        End If

        CloseScreenVisible(False)

        '444      If Not activeSplit Is Nothing Then
        '    activeSplit._closeCheck.ReinitializeCloseCheck()
        '   End If

    End Sub

    Private Sub TabNewOrderButtonSelected() Handles DeliveryScreen.SelectedNewOrder

        TabIDTest()

    End Sub

    Friend Sub TabIDTest()

        If currentTable.TabID = DeliveryScreen.TempTabID Or currentTable.TabName = DeliveryScreen.TempTabName Then
            ' already set
        Else
            currentTable.TabID = DeliveryScreen.TempTabID
            currentTable.TabName = DeliveryScreen.TempTabName
            LoadTabIDinExperinceTable()
            UpdateTableInfo()
        End If

    End Sub

    Private Sub UpdateTableInfo() Handles DeliveryScreen.ChangedMethodUse
        If currentTerminal.TermMethod = "Quick" Then
            Me.QuickOrder.UpdateTableInfo()
        Else
            Me.activeOrder.UpdateTableInfo()
        End If

    End Sub

    Private Sub TestForCurrentTabInfo() Handles activeOrder.TestForCurrentTabInfo, QuickOrder.TestForCurrentTabInfo
        DeliveryScreen.TestForCurrentTabInfo()
        If DeliveryScreen.HasAddress = False Then
            If currentTerminal.TermMethod = "Quick" Then
                Me.QuickOrder.StartDeliveryMethod()
            Else
                Me.activeOrder.StartDeliveryMethod()
            End If
        End If
    End Sub

    Private Sub ClosedTabScreen() Handles DeliveryScreen.TabScreenDisposing, activeOrder.TabScreenDisposing, QuickOrder.TabScreenDisposing

        DeliveryScreen.Visible = False

        If currentTerminal.TermMethod = "Quick" Then
            QuickOrder.tabScreenDisplaying = False
            Me.QuickOrder.EnableControls()
        Else
            activeOrder.tabScreenDisplaying = False
            Me.activeOrder.EnableControls()
        End If
        readAuth.ActiveScreen = "OrderScreen"
        readAuth.GiftAddingAmount = False   'both below probably not needed
        readAuth.IsNewTab = False

    End Sub









    Private Sub InitializeOrderForm222()
        'currently having problem reloading dataset the second time 
        '   if we use visible
        '   not using this way now

        '       StartOrderProcess(0)
        activeOrder = New term_OrderForm(False, False, Nothing)

        GenerateOrderTables.CreateDataViewsOrder()
        GenerateOrderTables.PopulateOpenOrderData222(0, False)

        '    activeOrder.ReInitializeOrderView()
        '   GenerateOrderTables.PopulateOpenOrderData(0)
        '      GenerateOrderTables.StartOrderProcess(2000000015)
        '     GenerateOrderTables.CreateDataViewsOrder()

        '    Me.activeOrder.ReInitializeOrderView()
        '   GenerateOrderTables.StartOrderProcess(2000000015)

        activeOrder.Location = New Point(0, 0)
        Me.Controls.Add(activeOrder)
        activeOrder.Visible = False
        OpenOrdersCurrencyMan = Me.BindingContext(dsOrder.Tables("OpenOrders"))
        'if use currencyMan remove dup from ordergridview
        orderScreenInitialized = False
        '    StartOrderProcess(0)
        '   GenerateOrderTables.CreateDataViewsOrder()

    End Sub

    Private Sub LoginManager(ByRef currentServer As DataSet_Builder.Employee) Handles tableScreen.ManagementButton

        managementScreen = New Manager_Form(currentServer, False)
        managementScreen.Location = New Point(0, 0)
        Me.Controls.Add(managementScreen)
        managementScreen.BringToFront()
        MakeTable_ScreenNotVisible()
        readAuth.ActiveScreen = "Manager"
        '      managementScreen.Show()

    End Sub

    Private Sub LoginEmployee222(ByRef emp As DataSet_Builder.Employee)

        Dim oRow As DataRow
        Dim rowCount As Integer

        '     PopulateAllTablesWithStatus(False)
        DetermineOpenBusiness()

        rowCount = dsOrder.Tables("OpenBusiness").Rows.Count

        If rowCount > 0 = True Then

            If rowCount = 1 Then
                oRow = dsOrder.Tables("OpenBusiness").Rows(0)
            Else
                For Each oRow In dsOrder.Tables("OpenBusiness").Rows
                    If oRow("DailyCode") = currentTerminal.CurrentDailyCode Then Exit For
                Next
            End If
            '    oRow = dsOrder.Tables("OpenBusiness").Rows(rowCount - 1)
            '      currentTerminal.CurrentDailyCode = oRow("DailyCode")
            currentTerminal.DailyDate = Format(oRow("StartTime"), "d")
            currentTerminal.primaryMenuID = oRow("PrimaryMenu")
            currentTerminal.secondaryMenuID = oRow("SecondaryMenu")
            currentTerminal.CurrentShift = oRow("ShiftID")

            PopulateQuickTicket()
            PerformEmployeeFunctions(emp)

            'below for Demo
            '     dsOrder.WriteXml("OrderData.xml", XmlWriteMode.WriteSchema)

        Else    'if false
            If MsgBox("There is no Daily Business Open. Do you wish to Clock Out?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

    End Sub

    Private Sub PerformEmployeeFunctions(ByVal emp As Employee)   ' Public Shared Sub
        Dim oRow As DataRow
        Dim rowCount As Integer
        readAuth.GiftAddingAmount = False
        readAuth.IsNewTab = False

        '     PopulateAllTablesWithStatus(False)
        DetermineOpenBusiness()

        rowCount = dsOrder.Tables("OpenBusiness").Rows.Count

        If rowCount > 0 = True Then

            If rowCount = 1 Then
                oRow = dsOrder.Tables("OpenBusiness").Rows(0)
            Else
                For Each oRow In dsOrder.Tables("OpenBusiness").Rows
                    If oRow("DailyCode") = currentTerminal.CurrentDailyCode Then Exit For
                Next
            End If
            '    oRow = dsOrder.Tables("OpenBusiness").Rows(rowCount - 1)
            '      currentTerminal.CurrentDailyCode = oRow("DailyCode")
            currentTerminal.DailyDate = Format(oRow("StartTime"), "d")
            currentTerminal.primaryMenuID = oRow("PrimaryMenu")
            currentTerminal.secondaryMenuID = oRow("SecondaryMenu")
            currentTerminal.CurrentShift = oRow("ShiftID")

            PopulateQuickTicket()
            '    PerformEmployeeFunctions(emp)

            'below for Demo
            '     dsOrder.WriteXml("OrderData.xml", XmlWriteMode.WriteSchema)

        Else    'if false
            DailyClosed(emp)
            Exit Sub

        End If

        If Not dsOrder.Tables("QuickTickets") Is Nothing Then
            currentQuickTicketDataViews.Clear()
            Select Case currentTerminal.TermMethod
                Case "Quick"
                    With dvQuickTickets
                        .Table = dsOrder.Tables("QuickTickets")
                        '     .RowFilter = "EmployeeID = " & emp.EmployeeID
                        .Sort = "ExperienceDate ASC"
                    End With
            End Select
        End If

        If emp.EmployeeID = 6986 Then
            If currentTerminal.TermMethod = "Quick" Then
                emp.Bartender = False
                emp.Cashier = True
            Else
                emp.Bartender = True
                emp.Cashier = False
            End If
        End If

        If emp.Manager = True Then

            If currentServer Is Nothing Then
                currentServer = New Employee
            End If
            currentServer = emp

            managementScreen = New Manager_Form(emp, True)  'emp, usernameEntered?
            managementScreen.Location = New Point(0, 0)
            Me.Controls.Add(managementScreen)
            managementScreen.BringToFront()
            readAuth.ActiveScreen = "Manager"
            '                managementScreen.Show()
            Exit Sub

        ElseIf emp.Bartender = True Then 'Or emp.EmployeeID = 4002 Or emp.EmployeeID = 4001 Then    'and if terminal group is at bar

            If currentTerminal.TermMethod = "Table" Then

                If currentServer Is Nothing Then
                    currentServer = New Employee
                End If
                currentServer = emp

                If Not tableScreen Is Nothing Then
                    '        If companyInfo.usingBartenderMethod = True Then
                    '        Me.tableScreen.IsBartenderMode = True        'IsBartnerderMode? yes
                    '     Else
                    '         Me.tableScreen.IsBartenderMode = False        'IsBartnerderMode? no
                    '     End If
                    Me.tableScreen.IsBartenderMode = False        'IsBartnerderMode? no
                    Me.tableScreen.InitializeOther(True)
                    tableScreen.Visible = True
                Else
                    tableScreen = New Tables_Screen_Bar
                    tableScreen.Location = New Point(0, 0)
                    Me.Controls.Add(tableScreen)
                    '       If companyInfo.usingBartenderMethod = True Then
                    '           Me.tableScreen.IsBartenderMode = True        'IsBartnerderMode? yes
                    '       Else
                    '           Me.tableScreen.IsBartenderMode = False        'IsBartnerderMode? no
                    '    End If
                    Me.tableScreen.IsBartenderMode = False        'IsBartnerderMode? no
                    Me.tableScreen.InitializeOther(True)
                End If
                readAuth.ActiveScreen = "TableScreen"

                '       Me.tableScreen.Visible = True

        ElseIf currentTerminal.TermMethod = "Bar" Then 'Or emp.EmployeeID = 6986 Then

            GenerateOrderTables.PopulateBartenderCollection()

            If dsOrder.Tables("QuickTickets").Rows.Count > 0 Then
                Dim ee As Employee

                For Each ee In currentBartenders
                    Dim dvQuickTickets444 = New DataView
                    '     dvQuickTickets = New DataView
                    With dvQuickTickets444
                        .Table = dsOrder.Tables("QuickTickets")
                        .RowFilter = "EmployeeID = " & ee.EmployeeID
                        .Sort = "ExperienceDate ASC"
                    End With
                    currentQuickTicketDataViews.Add(dvQuickTickets444)
                Next
            End If

            If currentServer Is Nothing Then
                currentServer = New Employee
            End If
            currentServer = emp

            If Not tableScreen Is Nothing Then
                If companyInfo.usingBartenderMethod = True Then
                    Me.tableScreen.IsBartenderMode = True        'IsBartnerderMode? yes
                Else
                    Me.tableScreen.IsBartenderMode = False        'IsBartnerderMode? no
                End If
                Me.tableScreen.InitializeOther(True)
                tableScreen.Visible = True
            Else
                tableScreen = New Tables_Screen_Bar
                tableScreen.Location = New Point(0, 0)
                Me.Controls.Add(tableScreen)
                If companyInfo.usingBartenderMethod = True Then
                    Me.tableScreen.IsBartenderMode = True        'IsBartnerderMode? yes
                Else
                    Me.tableScreen.IsBartenderMode = False        'IsBartnerderMode? no
                End If
                Me.tableScreen.InitializeOther(True)
            End If
            readAuth.ActiveScreen = "TableScreen"
            '444     tableScreen = New Tables_Screen_Bar

        ElseIf currentTerminal.TermMethod = "Quick" Then       'Quick Server
            LoginQuickService(emp)
        End If
            Exit Sub

        ElseIf emp.Server = True Then
            If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
                If currentServer Is Nothing Then
                    currentServer = New Employee
                End If
                currentServer = emp

                If Not tableScreen Is Nothing Then
                    Me.tableScreen.IsBartenderMode = False        'IsBartnerderMode? no
                    Me.tableScreen.InitializeOther(False)   '  emp is not bartender
                    tableScreen.Visible = True
                Else
                    tableScreen = New Tables_Screen_Bar
                    tableScreen.Location = New Point(0, 0)
                    Me.Controls.Add(tableScreen)
                    Me.tableScreen.IsBartenderMode = False        'IsBartnerderMode? no
                    Me.tableScreen.InitializeOther(False)   '  emp is not bartender
                End If
                readAuth.ActiveScreen = "TableScreen"

                '444    tableScreen = New Tables_Screen_Bar
                '       Me.tableScreen.IsBartenderMode = False        'IsBartnerderMode? no
                '      Me.tableScreen.InitializeOther(False)   '  emp is not bartender

                '444     tableScreen.Location = New Point(0, 0)
                '444     Me.Controls.Add(tableScreen)

            ElseIf currentTerminal.TermMethod = "Quick" Then       'Quick Server
                LoginQuickService(emp)
            End If
            Exit Sub

        ElseIf emp.Cashier = True Then
            If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then

            ElseIf currentTerminal.TermMethod = "Quick" Then       'Quick Server
                '    LoginQuickService(emp)


                ' Exit Sub
                'below is possible start for displaying tickets on table screen
                '  GenerateOrderTables.PopulateBartenderCollection()

                If dsOrder.Tables("QuickTickets").Rows.Count > 0 Then

                    '  Dim ee As Employee
                    ' For Each ee In currentBartenders
                    Dim dvQuickTickets444 = New DataView
                    With dvQuickTickets444
                        .Table = dsOrder.Tables("QuickTickets")
                        '   .RowFilter = "EmployeeID = " & ee.EmployeeID
                        .Sort = "ExperienceDate ASC"
                    End With
                    currentQuickTicketDataViews.Add(dvQuickTickets444)
                    '   Next

                End If

                If currentServer Is Nothing Then
                    currentServer = New Employee
                End If
                currentServer = emp

                '444       tableScreen = New Tables_Screen_Bar
                If Not tableScreen Is Nothing Then
                    If companyInfo.usingBartenderMethod = True Then
                        Me.tableScreen.IsBartenderMode = True        'IsBartnerderMode? yes
                    Else
                        Me.tableScreen.IsBartenderMode = False        'IsBartnerderMode? no
                    End If

                    Me.tableScreen.InitializeOther(True)
                    tableScreen.Visible = True
                Else
                    tableScreen = New Tables_Screen_Bar
                    tableScreen.Location = New Point(0, 0)
                    Me.Controls.Add(tableScreen)
                    If companyInfo.usingBartenderMethod = True Then
                        Me.tableScreen.IsBartenderMode = True        'IsBartnerderMode? yes
                    Else
                        Me.tableScreen.IsBartenderMode = False        'IsBartnerderMode? no
                    End If

                    Me.tableScreen.InitializeOther(True)
                End If
                readAuth.ActiveScreen = "TableScreen"

                '444     tableScreen.Location = New Point(0, 0)
                '444  Me.Controls.Add(tableScreen)

            End If
            Exit Sub

        ElseIf emp.Hostess = True Then
            If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then

            ElseIf currentTerminal.TermMethod = "Quick" Then       'Quick Server
                LoginQuickService(emp)
            End If
            Exit Sub

        End If

        If currentServer Is Nothing Then
            currentServer = emp
        End If
        '   *** if there is no daily, we will allow for a clockout
        '   a server does gets a non server screen
        If currentClockEmp Is Nothing Then
            currentClockEmp = New Employee
        End If
        currentClockEmp = emp
        If currentClockEmp.ClockInReq = True Then
            nonServerClockout = New ClockOut_UC(emp, False)
            nonServerClockout.Location = New Point(0, 0) '(Me.pnlTimeInfo.Width + 20, Me.lblLogin.Height + 10)
            nonServerClockout.BringToFront()
            Me.Controls.Add(nonServerClockout)
            '        Me.pnlLogin.Controls.Add(nonServerClockout)
        Else
            MsgBox(currentClockEmp.FullName & " does not use time clock.")
        End If

    End Sub

    Private Sub QuickOrderScreen()

        GenerateOrderTables.CreateDataViewsOrder()

        If QuickOrder Is Nothing Then
            QuickOrder = New term_OrderForm(False, False, Nothing)
            QuickOrder.Location = New Point(0, 0)
            Me.Controls.Add(QuickOrder)
        Else
            'when we change to visible 
            QuickOrder.IsBartenderMode = False
            QuickOrder.IsManagerMode = False
            QuickOrder.TabAccountInfo = Nothing
            QuickOrder.InitializeScreenSecondStep()
            QuickOrder.Visible = True
        End If

        QuickOrder.BringToFront()
        readAuth.ActiveScreen = "OrderScreen"

    End Sub

    Private Sub OrderScreen(ByRef tabAccountInfo As DataSet_Builder.Payment) Handles tableScreen.FireOrderScreen

        If currentTerminal.TermMethod = "Quick" Then
            If QuickOrder Is Nothing Then
                QuickOrder = New term_OrderForm(Me.tableScreen.IsBartenderMode, False, tabAccountInfo)
                QuickOrder.Location = New Point(0, 0)
                Me.Controls.Add(QuickOrder)
            Else
                'when we change to visible 
                QuickOrder.IsBartenderMode = Me.tableScreen.IsBartenderMode
                QuickOrder.IsManagerMode = False
                QuickOrder.TabAccountInfo = tabAccountInfo
                QuickOrder.InitializeScreenSecondStep()
                QuickOrder.Visible = True
            End If

            QuickOrder.BringToFront()

            If dsOrder.Tables("QuickTickets").Rows.Count = 1 Then
                'this is the first time we come here
                If currentTable.MethodUse = "Delivery" Then
                    '???   TestForCurrentTabInfo()
                    QuickOrder.StartDeliveryMethod()
                ElseIf currentTable.MethodUse = "Dine In" Then
                    QuickOrder.StartDineInMethod(True)
                    QuickOrder.EnableControls()
                End If
            Else
                If currentTable.MethodUse = "Delivery" Or currentTable.MethodUse = "Pickup" Then
                    'may be good idea to have this for DineIn and Take-Out as well
                    TestForCurrentTabInfo()
                Else
                    QuickOrder.EnableControls()
                End If
            End If

        Else
            If activeOrder Is Nothing Then
                activeOrder = New term_OrderForm(Me.tableScreen.IsBartenderMode, False, tabAccountInfo)
                activeOrder.Location = New Point(0, 0)
                Me.Controls.Add(activeOrder)
            Else
                'when we change to visible 
                activeOrder.IsBartenderMode = Me.tableScreen.IsBartenderMode
                activeOrder.IsManagerMode = False
                activeOrder.TabAccountInfo = tabAccountInfo
                activeOrder.InitializeScreenSecondStep()
                activeOrder.Visible = True
            End If

            activeOrder.BringToFront()
            activeOrder.EnableControls()

            If currentTable.MethodUse = "Delivery" Then
                TestForCurrentTabInfo()
            End If
        End If

        '      If Not SeatingTab Is Nothing Then
        '      SeatingTab.Dispose()
        '     End If

        MakeTable_ScreenNotVisible() 'this just make visible.false
        readAuth.GiftAddingAmount = False
        readAuth.IsNewTab = False
        readAuth.ActiveScreen = "OrderScreen"

        If companyInfo.processor = "MerchantWare" Then
            '444        readAuth_MWE.currentExpNum = currentTable.ExperienceNumber
            '444        readAuth_MWE.currentCheckNum = currentTable.CheckNumber
            'we must also change check number from close check
        End If

    End Sub

    Private Sub MgrOrderScreen() Handles managementScreen.FireOrderScreen
        ' I don;t think TabAccountInfo is used in Term_OrderForm (also for above)

        If Not managementScreen Is Nothing Then
            Me.managementScreen.Dispose()
            Me.managementScreen = Nothing
        End If


        If currentTerminal.TermMethod = "Quick" Then
            If QuickOrder Is Nothing Then
                QuickOrder = New term_OrderForm(False, True, Nothing)
                QuickOrder.Location = New Point(0, 0)
                Me.Controls.Add(QuickOrder)
            Else
                'when we change to visible 
                QuickOrder.IsBartenderMode = False
                QuickOrder.IsManagerMode = True
                '     QuickOrder.TabAccountInfo = tabAccountInfo
                QuickOrder.InitializeScreenSecondStep()
                QuickOrder.Visible = True
            End If

            QuickOrder.BringToFront()

        Else
            If activeOrder Is Nothing Then
                activeOrder = New term_OrderForm(False, True, Nothing)
                activeOrder.Location = New Point(0, 0)
                Me.Controls.Add(activeOrder)
            Else
                'when we change to visible 
                activeOrder.IsBartenderMode = False
                activeOrder.IsManagerMode = True
                '     activeOrder.TabAccountInfo = tabAccountInfo
                activeOrder.InitializeScreenSecondStep()
                activeOrder.Visible = True
            End If

            activeOrder.BringToFront()

        End If

        readAuth.GiftAddingAmount = False
        readAuth.IsNewTab = False
        readAuth.ActiveScreen = "OrderScreen"

    End Sub

    Private Sub ExitManager() Handles managementScreen.DisposingOfManager

        Me.managementScreen.Dispose()
        Me.managementScreen = Nothing
        actingManager = Nothing
        empActive = Nothing
        pnlLogin.Visible = True
        readAuth.ActiveScreen = "Login"

    End Sub


    Private Sub NewAddNewTable() Handles SeatingChart.NumberCustomerEvent


        Dim oRow As DataRow
        Dim expNum As Int64
        Dim numCust As Integer
        Dim isCurrentlyHeld As Boolean
        '       Dim satTm As DateTime

        '   **** might have to have a check for the bartenders on which employee this is

        '        EnableTables_Screen()
        '       InitializeScreen()


        Try
            For Each oRow In dsOrder.Tables("AllTables").Rows     'currentPhysicalTables
                If oRow("TableNumber") = SeatingChart.TableSelected Then
                    If typeProgram = "Online_Demo" Then
                        oRow("EmployeeID") = currentServer.EmployeeID
                        oRow("TableStatusID") = 2
                    End If
                    numCust = SeatingChart.NumberCustomers
                    expNum = CreateNewExperience(currentServer.EmployeeID, SeatingChart.TableSelected, Nothing, Nothing, numCust, 2, 0, 0, currentServer.LoginTrackingID)
                    isCurrentlyHeld = PopulateThisExperience(expNum, False)

                    currentTable = New DinnerTable
                    currentTable.ExperienceNumber = expNum
                    currentTable.IsTabNotTable = False
                    currentTable.TableNumber = SeatingChart.TableSelected
                    currentTable.TabID = 0
                    currentTable.TabName = SeatingChart.TableSelected.ToString
                    currentTable.TicketNumber = 0
                    currentTable.EmployeeID = currentServer.EmployeeID
                    currentTable.EmployeeNumber = currentServer.EmployeeNumber
                    currentTable.CurrentMenu = currentTerminal.currentPrimaryMenuID 'primaryMenuID
                    currentTable.StartingMenu = currentTerminal.currentPrimaryMenuID 'primaryMenuID
                    currentTable.NumberOfChecks = 1
                    currentTable.NumberOfCustomers = numCust
                    currentTable.LastStatus = 2
                    currentTable.OrderView = "Detail"
                    currentTable.SatTime = Now
                    currentTable.ItemsOnHold = 0
                    If numCust >= companyInfo.autoGratuityNumber Then
                        currentTable.AutoGratuity = companyInfo.autoGratuityPercent
                    Else
                        currentTable.AutoGratuity = -1
                    End If
                    If dvTerminalsUseOrder.Count > 0 Then
                        currentTable.MethodUse = dvTerminalsUseOrder(0)("MethodUse")
                        currentTable.MethodDirection = dvTerminalsUseOrder(0)("MethodDirection")
                    Else
                        currentTable.MethodUse = "Dine In"
                        currentTable.MethodDirection = "None"
                    End If

                    StartOrderProcess(currentTable.ExperienceNumber)

                    '222        satTm = AddStatusChangeData(currentTable.ExperienceNumber, 2, Nothing, 0, Nothing)
                    '            SaveESCStatusChangeData(2, Nothing, 0, Nothing)

                    '           RaiseEvent FireOrderScreen()
                    If SeatingChart.StartedFromManager = True Then
                        MgrOrderScreen()
                    Else
                        OrderScreen(Nothing)
                    End If

                    SeatingChart.Visible = False

                    Exit Sub
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            'in case of failure
            SeatingChart.Visible = False
            '       EnableTables_Screen()
            '      InitializeScreen()
        End Try

    End Sub

    Private Sub MakeTable_ScreenNotVisible()

        updateClockTimer.Dispose()
        tablesInactiveTimer.Dispose()
        tableScreen.Visible = False


    End Sub

    Private Sub UpdatingTableData(ByVal emp As Employee, ByRef ccDisplay As CashClose_UC) Handles activeOrder.TermOrder_Disposing   ', managementScreen.UpdatingAfterTransfer

        '   Me.activeOrder.DisposeOrderFormObjects()
        '     Me.activeOrder.Dispose()
        '    Me.activeOrder = Nothing
        Me.activeOrder.Visible = False
        If Not SeatingTab Is Nothing Then
            SeatingTab.Visible = False
        End If
        If Not DeliveryScreen Is Nothing Then
            DeliveryScreen.Visible = False
        End If

        GenerateOrderTables.ReleaseCurrentlyHeld()
        GenerateOrderTables.SaveOpenOrderData()

        currentTable = Nothing
        currentServer = Nothing
        '        activeOrder.Visible = False
        '       Me.activeOrder.ReInitializeOrderView()

        PerformEmployeeFunctions(emp)   'currentServer)
        If Not ccDisplay Is Nothing Then
            ccDisplay.Location = New Point((Me.Width - ccDisplay.Width) / 2, (Me.Height - ccDisplay.Height) / 2)
            Me.Controls.Add(ccDisplay)
            ccDisplay.BringToFront()
        End If

    End Sub

    '**********************************
    '   CLose Check through SplitScreen
    '**********************************

    Private Sub StartCloseScreen() Handles activeOrder.ClosingCheck

        activeOrder.Visible = False
        CloseScreenVisible(False)

    End Sub

    Private Sub StartQuickCloseScreen() Handles QuickOrder.ClosingCheck

        CloseScreenVisible(False)
        QuickOrder.Visible = False

    End Sub

    Private Sub CloseScreenVisible(ByVal _isFromManager As Boolean)

        If companyInfo.processor = "MerchantWare" Then
            '444       With dvUnAppliedPaymentsAndCredits_MWE
            '444      .Table = readAuth_MWE.dtPaymentsAndCreditsUnauthorized_MWE
            '444        .RowFilter = "Applied = False AND ExperienceNumber = '" & currentTable.ExperienceNumber & "' AND CheckNumber = '" & currentTable.CheckNumber & "'"
            '444         .Sort = "PaymentFlag"
            '444      End With
        End If

        If Not activeSplit Is Nothing Then
            '     activeSplit._closeCheck.ReinitializeCloseCheck()
            '    activeSplit._closeCheck.Visible = True
            activeSplit.Visible = True
            activeSplit.DisplayCloseCheck(_isFromManager)
            activeSplit._closeCheck.BringToFront()
            activeSplit.BringToFront()
        Else
            InitializeSplitChecks(_isFromManager)
        End If

        readAuth.GiftAddingAmount = False
        readAuth.IsNewTab = False
        readAuth.ActiveScreen = "CloseCheck"

    End Sub

    Private Sub StartCloseScreenFromManager() Handles managementScreen.MgrClosingCheck

        Me.managementScreen.Dispose()
        Me.managementScreen = Nothing
        CloseScreenVisible(True)

    End Sub

    Private Sub EndClosingCheck(ByVal _isFromManager As Boolean, ByVal emp As Employee, ByVal goingToSelectedCheck As Boolean) Handles activeSplit.ManagerClosing
        pnlLogin.Visible = True
        '     readAuth.ActiveScreen = "Login"
        SetDateTime()

        If _isFromManager = True Then
            If goingToSelectedCheck = True Then
                MgrOrderScreen()
            Else
                GenerateOrderTables.ReleaseCurrentlyHeld()
                GenerateOrderTables.SaveOpenOrderData()
                DisposeDataViewsOrder() '999
                currentTable = Nothing
                currentServer = Nothing
                Me.PerformEmployeeFunctions(emp)
            End If
        Else
            If currentTerminal.TermMethod = "Quick" Then
                ' why() 'Not release And Save?/
                PerformEmployeeFunctions(emp)   'this is different
            Else
                activeOrder.Visible = False
                GenerateOrderTables.ReleaseCurrentlyHeld()
                GenerateOrderTables.SaveOpenOrderData()
                DisposeDataViewsOrder() '999
                currentTable = Nothing
                currentServer = Nothing
                If currentTerminal.TermMethod = "Bar" Then
                    PerformEmployeeFunctions(emp)   'this is different
                End If
            End If
        End If

        activeSplit.Visible = False
        readAuth.GiftAddingAmount = False
        readAuth.IsNewTab = False
        'readAuth.ActiveScreen set in PerformEmployeeFunctions

    End Sub

    Friend Sub SplitCheckClosed() Handles activeSplit.SplitCheckClosing

        If currentTerminal.TermMethod = "Quick" Then
            QuickOrder.SplitCheckClosed()
            QuickOrder.Visible = True

        Else
            activeOrder.SplitCheckClosed()
            activeOrder.Visible = True
        End If

        activeSplit.Visible = False

        readAuth.ActiveScreen = "OrderScreen"
        readAuth.GiftAddingAmount = False
        readAuth.IsNewTab = False
        '444        activeSplit.Dispose()
        '444    activeSplit = Nothing

    End Sub

    Private Sub GotoQuickTicket(ByVal experienceNumber As Int64) Handles tableScreen.QuickTicketStart
        '   ByVal tabID As Int64, ByVal tabName As String, 

        Dim expNum As Int64
        Dim tabID As Int64
        Dim tabName As String
        Dim tktNum As Integer
        Dim lStatus As Integer
        Dim lView As String
        Dim autoGratuity As Decimal
        '    Dim tabID As Int64
        '   Dim tabName As String
        Dim numCust As Integer
        Dim numCks As Integer
        Dim firstCheckClosed As Boolean
        Dim oRow As DataRow
        Dim csName As String
        Dim methodUse As String
        '     Dim menuID As Integer
        Dim selectedRow As Integer
        Dim rc As Integer
        currentTerminal.NumOpenTickets = 0

        'this is what i should be doing here
        '   currentTerminal.NumOpenTickets = dsOrder.Tables("QuickTickets").Compute("Count(ClosedSubTotal Is DBNull.Value)") ', "ClosedSubTotal Is DBNull.Value")

        For Each oRow In dsOrder.Tables("QuickTickets").Rows
            If oRow("ClosedSubTotal") Is DBNull.Value Then
                currentTerminal.NumOpenTickets += 1
            End If
            If oRow("ExperienceNumber") = experienceNumber Then
                selectedRow = rc
            End If
            rc += 1
        Next

        tktNum = dsOrder.Tables("QuickTickets").Rows(selectedRow)("TicketNumber")
        expNum = dsOrder.Tables("QuickTickets").Rows(selectedRow)("ExperienceNumber")
        lStatus = dsOrder.Tables("QuickTickets").Rows(selectedRow)("LastStatus")
        lView = dsOrder.Tables("QuickTickets").Rows(selectedRow)("LastView")
        autoGratuity = dsOrder.Tables("QuickTickets").Rows(selectedRow)("AutoGratuity")
        tabID = dsOrder.Tables("QuickTickets").Rows(selectedRow)("TabID")
        tabName = dsOrder.Tables("QuickTickets").Rows(selectedRow)("TabName")
        numCust = dsOrder.Tables("QuickTickets").Rows(selectedRow)("NumberOfCustomers")
        numCks = dsOrder.Tables("QuickTickets").Rows(selectedRow)("NumberOfChecks")
        methodUse = dsOrder.Tables("QuickTickets").Rows(selectedRow)("MethodUse")

        '     menuID = dsOrder.Tables("QuickTickets").Rows(selectedRow)("MenuID")
        If Not dsOrder.Tables("QuickTickets").Rows(selectedRow)("ClosedSubTotal") Is DBNull.Value Then
            firstCheckClosed = True
        Else
            firstCheckClosed = False
        End If

        currentTable = New DinnerTable

        currentTable.ExperienceNumber = expNum
        currentTable.IsTabNotTable = True
        currentTable.TableNumber = 0
        currentTable.TabID = tabID '"-999"         ' expNum
        currentTable.TabName = tabName  '"Tkt# " & tktNum.ToString
        currentTable.TicketNumber = tktNum
        currentTable.EmployeeID = currentServer.EmployeeID
        currentTable.EmployeeNumber = currentServer.EmployeeNumber
        For Each oRow In dsEmployee.Tables("AllEmployees").Rows
            If oRow("EmployeeID") = currentTable.EmployeeID Then
                csName = oRow("NickName")
                Exit For
            End If
        Next
        currentTable.EmployeeName = csName
        currentTable.CurrentMenu = currentTerminal.currentPrimaryMenuID 'menuID 'currentTerminal.CurrentMenuID
        currentTable.StartingMenu = currentTerminal.currentPrimaryMenuID ' menuID 'currentTerminal.CurrentMenuID
        currentTable.NumberOfCustomers = numCust
        currentTable.NumberOfChecks = numCks
        currentTable.ItemsOnHold = 0
        currentTable.IsClosed = firstCheckClosed
        currentTable.LastStatus = lStatus
        currentTable.OrderView = lView
        currentTable.AutoGratuity = autoGratuity
        currentTable.MethodUse = methodUse
        DefineMethodDirection()
        '      currentTable.NumberOfCustomers = 1
        '      currentTable.CheckNumber = 1
        '      currentTable.CustomerNumber = 1
        '      currentTable.NextCustomerNumber = 1
        '      currentTable.LastStatus = lStatus
        '      currentTable.Quantity = 1

        PopulateThisExperience(expNum, False)
        StartOrderProcess(expNum)
        MakeTable_ScreenNotVisible()
        QuickOrderScreen()

    End Sub


    Private Sub LoginQuickService(ByRef emp As Employee)

        If currentServer Is Nothing Then
            currentServer = New Employee
        End If
        currentServer = emp

        Dim expNum As Int64
        Dim tktNum As Integer
        Dim lStatus As Integer
        Dim lView As String
        Dim autoGratuity As Decimal
        Dim tabID As Int64
        Dim tabName As String
        Dim numCust As Integer
        Dim numCks As Integer
        Dim firstCheckClosed As Boolean
        Dim oRow As DataRow
        Dim csName As String
        Dim methodUse As String
        '    Dim menuID As Integer
        currentTerminal.NumOpenTickets = 0

        If dsOrder.Tables("QuickTickets").Rows.Count = 0 Then
            tktNum = CreateNewTicketNumber()
            expNum = CreateNewExperience(currentServer.EmployeeID, Nothing, -999, "Tkt# " & tktNum.ToString, 1, 2, tktNum, 0, currentServer.LoginTrackingID)
            csName = emp.NickName
            lStatus = 2
            lView = "Detail"
            firstCheckClosed = False
            tabID = -999         ' expNum
            tabName = "Tkt# " & tktNum.ToString
            numCust = 1
            numCks = 1
            autoGratuity = -1
            If dvTerminalsUseOrder.Count > 0 Then
                methodUse = dvTerminalsUseOrder(0)("MethodUse")
            Else
                methodUse = "Dine In"
            End If

        Else

            'this is what i should be doing here
            '   currentTerminal.NumOpenTickets = dsOrder.Tables("QuickTickets").Compute("Count(ClosedSubTotal Is DBNull.Value)") ', "ClosedSubTotal Is DBNull.Value")

            For Each oRow In dsOrder.Tables("QuickTickets").Rows
                If oRow("ClosedSubTotal") Is DBNull.Value Then
                    currentTerminal.NumOpenTickets += 1
                End If
            Next
            tktNum = dsOrder.Tables("QuickTickets").Rows(dsOrder.Tables("QuickTickets").Rows.Count - 1)("TicketNumber")
            expNum = dsOrder.Tables("QuickTickets").Rows(dsOrder.Tables("QuickTickets").Rows.Count - 1)("ExperienceNumber")
            lStatus = dsOrder.Tables("QuickTickets").Rows(dsOrder.Tables("QuickTickets").Rows.Count - 1)("LastStatus")
            lView = dsOrder.Tables("QuickTickets").Rows(dsOrder.Tables("QuickTickets").Rows.Count - 1)("LastView")
            autoGratuity = dsOrder.Tables("QuickTickets").Rows(dsOrder.Tables("QuickTickets").Rows.Count - 1)("AutoGratuity")
            tabID = dsOrder.Tables("QuickTickets").Rows(dsOrder.Tables("QuickTickets").Rows.Count - 1)("TabID")
            tabName = dsOrder.Tables("QuickTickets").Rows(dsOrder.Tables("QuickTickets").Rows.Count - 1)("TabName")
            numCust = dsOrder.Tables("QuickTickets").Rows(dsOrder.Tables("QuickTickets").Rows.Count - 1)("NumberOfCustomers")
            numCks = dsOrder.Tables("QuickTickets").Rows(dsOrder.Tables("QuickTickets").Rows.Count - 1)("NumberOfChecks")
            methodUse = dsOrder.Tables("QuickTickets").Rows(dsOrder.Tables("QuickTickets").Rows.Count - 1)("MethodUse")
            '      menuID = dsOrder.Tables("QuickTickets").Rows(dsOrder.Tables("QuickTickets").Rows.Count - 1)("MenuID")
            If Not dsOrder.Tables("QuickTickets").Rows(dsOrder.Tables("QuickTickets").Rows.Count - 1)("ClosedSubTotal") Is DBNull.Value Then
                firstCheckClosed = True
            Else
                firstCheckClosed = False
            End If
            '        csName = dsOrder.Tables("QuickTickets").Rows(dsOrder.Tables("QuickTickets").Rows.Count - 1)("NickName")
        End If

        currentTable = New DinnerTable

        currentTable.ExperienceNumber = expNum
        currentTable.IsTabNotTable = True
        currentTable.TableNumber = 0
        currentTable.TabID = tabID '"-999"         ' expNum
        currentTable.TabName = tabName  '"Tkt# " & tktNum.ToString
        currentTable.TicketNumber = tktNum
        currentTable.EmployeeID = currentServer.EmployeeID
        currentTable.EmployeeNumber = currentServer.EmployeeNumber
        For Each oRow In dsEmployee.Tables("AllEmployees").Rows
            If oRow("EmployeeID") = currentTable.EmployeeID Then
                csName = oRow("NickName")
                Exit For
            End If
        Next
        currentTable.EmployeeName = csName
        currentTable.CurrentMenu = currentTerminal.currentPrimaryMenuID 'menuID 'currentTerminal.CurrentMenuID
        currentTable.StartingMenu = currentTerminal.currentPrimaryMenuID ' menuID 'currentTerminal.CurrentMenuID
        currentTable.NumberOfCustomers = numCust
        currentTable.NumberOfChecks = numCks
        currentTable.ItemsOnHold = 0
        currentTable.IsClosed = firstCheckClosed
        currentTable.LastStatus = lStatus
        currentTable.OrderView = lView
        currentTable.AutoGratuity = autoGratuity
        currentTable.MethodUse = methodUse

        '      currentTable.NumberOfCustomers = 1
        '      currentTable.CheckNumber = 1
        '      currentTable.CustomerNumber = 1
        '      currentTable.NextCustomerNumber = 1
        '      currentTable.LastStatus = lStatus
        '      currentTable.Quantity = 1

        PopulateThisExperience(expNum, False)
        StartOrderProcess(expNum)
        QuickOrderScreen()

    End Sub

    Private Sub LeavingQuickServer(ByVal emp As Employee, ByRef ccDisplay As CashClose_UC) Handles QuickOrder.TermOrder_Disposing

        '     Me.QuickOrder.DisposeOrderFormObjects()
        '    Me.QuickOrder.Dispose()
        '   Me.QuickOrder = Nothing
        QuickOrder.Visible = False
        If Not SeatingTab Is Nothing Then
            SeatingTab.Visible = False
        End If
        If Not DeliveryScreen Is Nothing Then
            DeliveryScreen.Visible = False
        End If

        GenerateOrderTables.ReleaseCurrentlyHeld()
        GenerateOrderTables.SaveOpenOrderData()

        currentTable = Nothing
        currentServer = Nothing

        '   PopulateQuickTicket()
        PerformEmployeeFunctions(emp)   'currentServer)
        If Not ccDisplay Is Nothing Then
            ccDisplay.Location = New Point((Me.Width - ccDisplay.Width) / 2, (Me.Height - ccDisplay.Height) / 2)
            Me.Controls.Add(ccDisplay)
            ccDisplay.BringToFront()
        End If

    End Sub

    Private Sub NextQuickServer() Handles QuickOrder.QuickOrder_NotDisposing

        '       Me.QuickOrder.Dispose()
        '      Me.QuickOrder = Nothing
        'sss    GenerateOrderTables.SaveAvailTabsAndTables()

        If Not typeProgram = "Online_Demo" Then
            GenerateOrderTables.ReleaseCurrentlyHeld()
            GenerateOrderTables.SaveOpenOrderData()
        Else
            GenerateOrderTables.SaveOpenOrderDataExceptQuick()
        End If

        '     dsOrder.Tables("QuickTickets").Rows.Clear()
        '    DisposeDataViewsOrder()
        'old      currentTable = Nothing
        'old      currentServer = Nothing

    End Sub

    Private Sub btnClockIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClockIn.Click

        AttemptingToClockIn(loginPad.NumberString)

    End Sub

    Private Sub AttemptingToClockIn(ByVal loginEnter As String)

        Dim doesNotneedToClockIn As Boolean

        clockInPanel = New ClockInUserControl()
        clockInPanel.Location = New Point((ssX - clockInPanel.Width) / 2, (ssY - clockInPanel.Height) / 2)
        Me.Controls.Add(clockInPanel)
        doesNotneedToClockIn = clockInPanel.StartClockIn(loginEnter)

        If doesNotneedToClockIn = False Then
            'if False, this means they need to clockIn
            loginPad.btnNumberClear_Click()
            pnlLogin.Visible = False
            MakeLoginPadVisibleNOT()
            RemoveHandler pnlLogin.Click, AddressOf Me.ReceiveFocus
        Else
            clockInPanel.Dispose()
        End If

    End Sub

    Private Sub MakeClockOutBooleanFalse()
        clockOutActiveQS = False
        Me.btnClockOut.BackColor = Color.LightSlateGray
        MakeLoginPadVisibleNOT()
        '  Me.loginPad.Visible = False

    End Sub
    Private Sub btnClockOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClockOut.Click

        If clockOutActiveQS = False And loginPad.NumberString Is Nothing Then
            clockOutActiveQS = True
            Me.btnClockOut.BackColor = Color.CornflowerBlue
            MakeLoginPadVisible()
            '    Me.loginPad.Visible = True
        Else
            Dim emp As DataSet_Builder.Employee
            Dim yesOpenTables As Boolean
            Dim loginEnter As String
            Dim doesEmpNeedToClockOut As Boolean

            loginEnter = loginPad.NumberString

            emp = GenerateOrderTables.TestUsernamePassword(loginPad.NumberString, clockOutActiveQS) 'False)

            If Not emp Is Nothing Then
                '  check to see if there are any open tables           **********************
                If loginEnter.Length < 8 Then
                    MsgBox("Enter both EmployeeID as Passcode")
                    Exit Sub
                End If
                doesEmpNeedToClockOut = TestClockOut(loginEnter)
                If doesEmpNeedToClockOut = False Then
                    MakeClockOutBooleanFalse()
                    loginPad.btnNumberClear_Click()
                    MsgBox("Employee does not need to Clock Out")
                    Exit Sub
                End If

                yesOpenTables = GenerateOrderTables.AnyOpenTables(emp)
                If currentClockEmp Is Nothing Then
                    currentClockEmp = New Employee
                End If
                currentClockEmp = emp

                If yesOpenTables = True Then
                    openInfo = New DataSet_Builder.Information_UC(emp.FullName & " still has open checks. Press here to clock out or enter Tip Adjustments.")
                    openInfo.Location = New Point((Me.Width - openInfo.Width) / 2, (Me.Height - openInfo.Height) / 2)
                    Me.Controls.Add(openInfo)
                    openInfo.BringToFront()
                    '       Exit Sub
                Else
                    StartClockOut(emp, False)
                End If
            End If
            MakeClockOutBooleanFalse()

        End If

    End Sub

    Private Sub StartClockOut(ByVal emp As Employee, ByVal hasOpenTables As Boolean) 'ByVal hasOpenTables As Boolean)

        Dim salaried As Employee
        For Each salaried In SalariedEmployees
            If salaried.EmployeeID = emp.EmployeeID Then
                'this is a salaried employee
                MsgBox(emp.NickName & " is Salaried. No need to Clock Out.")
                Exit Sub
            End If
        Next

        If emp.ClockInReq = True Then
            Me.ClockingOutEmployee = New ClockOut_UC(emp, hasOpenTables)     '      , tipableSales, chargedSales, chargedTips)
            Me.ClockingOutEmployee.Location = New Point((Me.Width - Me.ClockingOutEmployee.Width) / 2, (Me.Height - Me.ClockingOutEmployee.Height) / 2)
            If currentServer.Server = False And currentServer.Bartender = False And currentServer.Cashier = False And currentServer.Manager = False Then
                ClockingOutEmployee.EitherPrintOrClockOut(True)
                ClockingOutEmployee.Dispose()
            Else
                Me.Controls.Add(Me.ClockingOutEmployee)
                Me.ClockingOutEmployee.BringToFront()
            End If

        Else
            MsgBox(emp.FullName & " does not use time clock.")
        End If

    End Sub

    Private Sub clockingOutComplete() Handles ClockingOutEmployee.ClockOutComplete, ClockingOutEmployee.ClockOutCancel
        loginPad.btnNumberClear_Click()
        pnlLogin.Visible = True
        MakeLoginPadVisible()
    End Sub

    Private Function AnyOpenTables222(ByVal emp As Employee)

        GenerateOrderTables.PopulateTabsAndTables(emp, currentTerminal.CurrentDailyCode, False, False, Nothing)
        CreateDataViews(emp.EmployeeID, True)
        If dvAvailTables.Count + dvTransferTables.Count + dvAvailTabs.Count + dvTransferTabs.Count + dvQuickTickets.Count > 0 Then
            Return True
        Else
            Return False
        End If

        Exit Function
        '222 below

        Dim oRow As DataRow

        For Each oRow In dsOrder.Tables("QuickTickets").Rows
            If oRow("ClosedSubTotal") Is DBNull.Value Then
                currentTerminal.NumOpenTickets += 1
            End If
        Next


        If currentTerminal.NumOpenTickets > 0 Then
            Return True
        Else
            Return False
        End If


    End Function




















    Private Sub ClosedClockInUserControl(ByVal sender As Object, ByVal e As System.EventArgs) Handles clockInPanel.ClosingClockIn

        AddHandler pnlLogin.Click, AddressOf Me.ReceiveFocus

    End Sub

    Private Sub SetDateTime()
        lblClockInDay.Text = Format(Now, "dddd")
        Me.lblClockInDate.Text = Format(Now, "MMM d, yyyy")
        Me.lblClockInTime.Text = Format(Now, "h:mm tt")


    End Sub


    Private Function InitialLogIn(ByVal leUsername As String, ByVal lePassword As String) As Boolean

        '     Dim daysBeforeExpiration As Integer

        '       Dim theNetworkInterfaces() As System.Net.NetworkInformation.NetworkInterface = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
        '      For Each currentInterface As System.Net.NetworkInformation.NetworkInterface In theNetworkInterfaces
        '     MessageBox.Show(currentInterface.GetPhysicalAddress().ToString())
        '    Next

        '444     daysBeforeExpiration = DateDiff(DateInterval.Day, Now, dateOfExpiration)
        '      If daysBeforeExpiration < 0 Then
        '  Me.Dispose()
        '     ElseIf daysBeforeExpiration < 14 Then
        '    MsgBox("Your subscription will expire in " & daysBeforeExpiration & " days.")
        '   End If

        If typeProgram = "Demo" Or typeProgram = "Online_Demo" Then
            timeoutInterval = 10000  '100000
        End If

        If leUsername = "e" Or leUsername = "eglobal" Then
            leUsername = "eglobal"
            '  localConnectServer = leUsername & "\" & leUsername
            localConnectServer = "eglobalmain\eglobalmain"
        Else
            localConnectServer = leUsername & "\" & leUsername
        End If

        connectserver = localConnectServer

        If typeProgram = "Online_Demo" Then
            CreateTableStructure()
            InitializeOpeningScreen()
            tablesFilled = True
            OnlineDemoStartOfProgram()
            Exit Function
        End If


        CreateTableStructure()
        PopulateCompanyInfo(leUsername)
        InitializeOpeningScreen()

        If companyInfo.processor = "MerchantWare" Then
            '444         readAuth_MWE = New ReadCredit_MWE2.MainForm_MWE(companyInfo.CompanyID, dtCreditCardDetail)
            '444         Me.readAuth_MWE.AxUSBHID1.PortOpen = True
        End If
        dvUnAppliedPaymentsAndCredits_MWE = New DataView

        '   readAuth_MWE.btnSale_Encrpted_Swipe(1)
        '       With dvUnAppliedPaymentsAndCredits_MWE
        '      .Table = readAuth_MWE.dtPaymentsAndCreditsUnauthorized_MWE
        '     .RowFilter = "Applied = False AND ExperienceNumber = '" & currentTable.ExperienceNumber & "' AND CheckNumber = '" & currentTable.CheckNumber & "'"
        '    .Sort = "PaymentFlag"
        '   End With
        '  OpenPortAtStart()
        '    readAuth_MWE.OpenPortAtStart()
        '      End If

        If ds.Tables("LocationOverview").Rows.Count = 0 Then
            Exit Function
        End If

        If companyInfo.endOfWeek = 7 Then
            companyInfo.begOfWeek = 1
        Else
            companyInfo.begOfWeek = companyInfo.endOfWeek - 6
        End If

        If lePassword = companyInfo.locationPassword Then
            If tablesFilled = False Then
                StartOfProgram(companyInfo.companyName)
            Else
                DisplayOpeningScreen(companyInfo.companyName)
            End If
            Me.initLogon.Dispose()
        Else
            '   password incorrent
            IncorrectInitLogin()
        End If

    End Function

    Private Sub ExpOverrideResult() Handles expOverride.Enter

        If expOverride.EnteredString = "Pass" Then
            dateOfExpiration = "01/01/2099"
            InitializeOther()
        Else
            Me.Dispose()
        End If

    End Sub
    Private Sub CreateTableStructure()

        Dim sRow As DataRow
        Dim mTable As String

        Try
            ' we need to do this first b/c MainTable___ & ModifierTable___ rely on these
            '      CreateAllFoodCategoryTableStructure(dtStarterAllFoodCategory)
            '     CreateModifierCategoryTableStructure(dtStarterModifierCategory)
            CreateDrinkAddsTableStructure(dtDrinkAdds)
            CreateDrinkTableStructure(dtDrink)
            CreateDrinkSubCategoryTableStructure(dtDrinkSubCategory)
            '      CreateDrinkModifiersTableStructure(dtDrinkModifiers)
            CreateDrinkPrepTableStructure(dtDrinkPrep)
            CreateLiquorTypesTableStructure(dtLiquorTypes)
            CreateCatJoinTableStructure(dtCategoryJoin)


            CreateLocationOverviewTableStructure(dtLocationOverview)
            CreateLocationOpeningTableStructure(dtLocationOpening)
            CreateModifierCategoryTableStructure(dtModifierCategory)
            CreateAllFoodCategoryTableStructure(dtAllFoodCategory)


            ' the following two are now loaded from local db
            '      CreateAllEmployeesTableStructure(dtAllEmployees)
            '     CreateJobCodeInfoTableStructure(dtJobCodeInfo)

        Catch ex As Exception
            MsgBox(ex.Message & " Creating Table Struture 1")
        End Try

        '      Try
        '     dsStarter.Clear()
        '    dsStarter.ReadXml("c:\Data Files\spiderPOS\StarterMenu.xml", XmlReadMode.ReadSchema)
        '   Catch ex As Exception
        '
        '       End Try

        'doing below
        '       Try
        '        mainCategoryIDArrayList.Clear()
        '        secondaryCategoryIDArrayList.Clear()
        '        For Each sRow In dsStarter.Tables("StarterModifierCategory").Rows
        '       mTable = "ModifierTable" & sRow("CategoryID")
        '       CreateModifierTableStructure(mTable)
        '       mainCategoryIDArrayList.Add(sRow("CategoryID"))
        ''       secondaryCategoryIDArrayList.Add(sRow("CategoryID"))
        '       Next
        '      Catch ex As Exception
        '
        '       End Try


        Try
            'stage 2
            CreateTermsWallsTableStructure(dtTermsWalls)
            CreateTermsTablesTableStructure(dtTermsTables)
            CreateTermsFloorTableStructure(dtTermsFloor)
            CreateTerminalsUseOrderTableStructure(dtTerminalsUseOrder)
            CreateTerminalsMethodTableStructure(dtTerminalsMethod)
            CreateCouponTableStructure(dtCoupon)
            CreateComboDetailTableStructure(dtComboDetail)
            CreateComboTableStructure(dtCombo)
            CreateBSGSTableStructure(dtBSGS)
            CreatePromotionTableStructure(dtPromotion)

            CreateFoodTableTableStructure(dtFoods)
            CreateIngredientsTableStructure(dtIngredients)



            'Inventory
            '       CreateRawCategoryTableStructure(dtRawCategory)
            '      CreateRawMatTableStructure(dtRawMat)
            '     CreateRawDeliveryTableStructure(dtRawDelivery)
            '    CreateRawDeliveryTableStructure(dtRawCycley)

            '***********
            'need to add
            '        Friend dtRawCategory As DataTable = ds.Tables.Add("RawCategory")
            '       Friend dtRawMat As DataTable = ds.Tables.Add("RawMat")

            CreateReasonsVoidTableStructure(dtReasonsVoid)
            CreateTabIdentifierTableStructure(dtTabIdentifier)
            CreateCreditCardDetailTableStructure(dtCreditCardDetail)
            CreateRoutingChoiceTableStructure(dtRoutingChoice)
            CreateShiftCodesTableStructure(dtShiftCodes)
            CreateMenuChoiceTableStructure(dtMenuChoice)
            CreateTaxTableStructure(dtTax)
            CreateTableStatusDescriptionTableStructure(dtTableStatusDesc)

        Catch ex As Exception
            MsgBox(ex.Message & " Creating Table Struture 2")
        End Try

        Try
            ' this is third round of data pull
            'therefore the tables are set up seperately
            ' there may be the above tables structured but not these
            CreateBarDrinkCatTableStructure(dtSecondaryBartenderDrinkCategory)
            CreateBarDrinkCatTableStructure(dtBartenderDrinkCategory)
            CreateBarCatTableStructure(dtSecondaryBartenderCategory)
            CreateBarCatTableStructure(dtBartenderCategory)

            CreateIndJoinTableStructure(dtIndividualJoinSecondary)
            CreateIndJoinTableStructure(dtIndividualJoinMain)
            CreateMainDrinkCatTableStructure(dtSecondaryDrinkCategory)
            CreateMainDrinkCatTableStructure(dtDrinkCategory)
            CreateMainCatTableStructure(dtSecondaryMainCategory)
            CreateMainCatTableStructure(dtMainCategory)


            mainCategoryIDArrayList.Clear()
            secondaryCategoryIDArrayList.Clear()

            'we need to use the Starter dataset
            For Each sRow In dsStarter.Tables("StarterAllFoodCategory").Rows

                mTable = ("SecondaryMainTable" & sRow("CategoryID"))
                CreateMainTableStructure(mTable)
                If sRow("FunctionFlag") = "G" Then
                    mTable = ("DrinkMainTable" & sRow("CategoryID"))
                    CreateDrinkMainTableStructure(mTable)
                    mTable = ("MainTable" & sRow("CategoryID"))
                    CreateMainTableStructure(mTable)
                Else
                    mTable = ("MainTable" & sRow("CategoryID"))
                    CreateMainTableStructure(mTable)
                End If
                mainCategoryIDArrayList.Add(sRow("CategoryID"))
                secondaryCategoryIDArrayList.Add(sRow("CategoryID"))
            Next
            For Each sRow In dsStarter.Tables("StarterModifierCategory").Rows
                mTable = "ModifierTable" & sRow("CategoryID")
                CreateModifierTableStructure(mTable)

                mainCategoryIDArrayList.Add(sRow("CategoryID"))
                secondaryCategoryIDArrayList.Add(sRow("CategoryID"))
            Next


            CreateQuickDrinkCatTableStructure(dtSecondaryQuickDrinkCategory)
            CreateQuickDrinkCatTableStructure(dtQuickDrinkCategory)
            CreateQuickCatTableStructure(dtSecondaryQuickCategory)
            CreateQuickCatTableStructure(dtQuickCategory)



            ' dsEmployee
            CreateLoggedInEmployeeTableStructure(dtLoggedInEmploees)
            CreateAllEmployeesTableStructure(dtAllEmployees)
            CreateJobCodeInfoTableStructure(dtJobCodeInfo)


            CreateClockOutSalesTableStructure(dtClockOutSales)


            'dsOrders
            CreateOpenOrdersTableStructure(dtOpenOrders)
            CreatePaymentsAndCreditsTableStructure(dtPaymentsAndCredits)
            CreateAvailTablesAndTabsTableStructure(dtAvailTables)
            CreateAvailTablesAndTabsTableStructure(dtAvailTabs)
            CreateAllTablesTableStructure(dtAllTables)
            CreateOpenBusinessTableStructure(dtOpenBusiness)
            CreateFunctionsTableStructure(dtFunctions)
            CreatePaymentTypeTableStructure(dtPaymentType)
            CreateOrderDetailTableStructure(dtOrderDetail)
            CreateTermsOpenTableStructure(dtTermsOpen)
            CreateAvailTablesAndTabsTableStructure(dtQuickTickets)
            CreateAvailTablesAndTabsTableStructure(dtCurrentlyHeld)

            'dsCustomers

            CreateTabDirectorySearchTableStructure(dtTabDirectorySearch)
            CreateTabPreviousOrdersTableStructure(dtTabPreviousOrders)
            CreateTabPreviousOrdersByItemTableStructure(dtTabPreviousOrdersByItem)

            'missing many others:



        Catch ex As Exception

            MsgBox(ex.Message & " Creating Table Struture 3")
        End Try
    End Sub

    Private Function InitialLogInOld222(ByVal leUsername As String, ByVal lePassword As String) As Boolean

        Dim username As String
        Dim password As String

        '      If System.Windows.Forms.SystemInformation.ComputerName = "VAIO" Then
        '      localConnectServer = "vaio\vaio"
        '      If leUsername = "l" Then
        '      leUsername = "labmain"
        '      End If
        '      ElseIf System.Windows.Forms.SystemInformation.ComputerName = "LABMAIN" Then
        '      leUsername = "labmain"
        '     localConnectServer = "LABMAIN\labmain"
        ''     ElseIf leUsername = "e" Then
        '     localConnectServer = "LABMAIN\labmain"
        '    Else
        '   localConnectServer = leUsername & "\" & leUsername
        '  End If

        '   MsgBox(System.Windows.Forms.SystemInformation.Network)

        '    localConnectServer = "vaio\vaio"

        If leUsername = "e" Then
            leUsername = "eglobalmain"
        End If

        If leUsername = "eglobalmain" Then
            'i keep changing during testing
            '       localConnectServer = "vaio\vaio"
            '      localConnectServer = "Phoenix\Phoenix"
        Else
            localConnectServer = leUsername & "\" & leUsername
        End If
        If System.Windows.Forms.SystemInformation.ComputerName = "DILEO222" Then
            '      localConnectServer = "FOLEYCOMPUTER\FoleyComputer"
            localConnectServer = "dileo\dileo"
            '            MsgBox(localConnectServer.ToString)
        End If

        '    tttt()
        '   TempConnectToPhoenix()
        connectserver = localConnectServer
        GenerateOrderTables.RestateConnectionString(sql.cn, connectserver)

        Try
            GenerateOrderTables.InitiateApplicationSecurity222()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
            If MsgBox("Local Database Not Connected. Please select Cancel and attempt to reset your local database, otherwise select OK to connect to DataCenter.", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                Me.Dispose()
            Else
                MsgBox("Call 404.869.4700 - inform them of problem: During security initiation, " & ex.Message)
            End If
            GenerateOrderTables.SwitchConnection()
            'we initaite Security in switch connection
            If securityPhoenixEst = False And securityLocalEst = False Then
                ' at least one must be true if we were to make a connection
                GenerateOrderTables.InitiateApplicationSecurity222()
            End If

        End Try


        Try
            PopulateCompanyInfo(leUsername)

        Catch ex As Exception
            CloseConnection()
            If MsgBox("Local Database Not Connected. Please select Cancel and attempt to reset your local database, otherwise select OK to connect to DataCenter.", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                Me.Dispose()
            Else
                MsgBox("Call 404.869.4700 - inform them of problem: During initial download, " & ex.Message)
            End If
            If connectserver = "Phoenix\Phoenix" Then
                MsgBox("DataCenter Not Connected. Verify all wire connection are established and your router is working. Then call 404-869-4700: " & ex.Message)
                Me.Dispose()
            Else
                GenerateOrderTables.SwitchConnection()
                Try
                    PopulateCompanyInfo(leUsername)
                Catch ex2 As Exception
                    CloseConnection()
                    MsgBox("DataCenter Not Connected. Verify all wire connection are established and your router is working. Then call 404-869-4700: " & ex2.Message)
                    Me.Dispose()
                End Try
            End If
        End Try

        If companyInfo.endOfWeek = 7 Then
            companyInfo.begOfWeek = 1
        Else
            companyInfo.begOfWeek = companyInfo.endOfWeek - 6
        End If


        If lePassword = companyInfo.locationPassword Then
            If tablesFilled = False Then
                StartOfProgram(companyInfo.companyName)
            Else
                DisplayOpeningScreen(companyInfo.companyName)
            End If
            Me.initLogon.Dispose()
        Else
            '   password incorrent
            IncorrectInitLogin()
        End If


    End Function

    Private Sub PopulateCompanyInfo(ByRef leUsername As String)

        Dim oRow As DataRow

        Try
            GenerateOrderTables.TempConnectToPhoenix()
         
            ds.Tables("LocationOverview").Rows.Clear()
            dsStarter.Tables("StarterLocationOverview").Rows.Clear()

            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlSelectCommandLocationOverview.Parameters("@Username").Value = leUsername
            sql.SqlLocationOverview.Fill(ds.Tables("LocationOverview"))
            sql.SqlLocationOverview.Fill(dsStarter.Tables("StarterLocationOverview"))

            sql.cn.Close()
            GenerateOrderTables.ConnectBackFromTempDatabase()

            If ds.Tables("LocationOverview").Rows.Count = 1 Then  'dtr.HasRows Then
                oRow = ds.Tables("LocationOverview").Rows(0)
                FillLocationOverviewData(oRow)
            Else
                IncorrectUsername(leUsername)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            CloseConnection()
            GenerateOrderTables.ConnectBackFromTempDatabase()
            MsgBox("Connection to DataCenter down. If this is not the first time you are receiving this message... Call Spider POS (404) 869-4700")
            '   MsgBox(ex.Message & " Connection Down, Populating COmpany Info. Select saved menu.")
            ServerNOTConectedStartOfProgram()

        Finally
        End Try

    End Sub


    Private Sub OldPopulateCompany222(ByRef leUsername As String)
        'use above sub
        Dim cmd As SqlClient.SqlCommand
        Dim dtr As SqlClient.SqlDataReader

        cmd = New SqlClient.SqlCommand("SELECT CompanyID, LocationID, CompanyName, LocationName, Username, Password, Address1, Address2, City, State, Zip, PhoneNumber, UsingDefaults, AutoPrint, EndOfWeek, OnlyOneLocation, MerchantID, MerchantIDPhone, OperatorID, LocalHostName, dbName, NumberTerminals, NumberFloorPlans, TimeoutSeconds, ColorAdjust, VersionNumber, LastUpdate, AutoUpdate, UsingBartenderMethod, CalculateAvgByEntrees, IsKitchenExpiditer, IsDelivery, AutoCloseCheck, DeliveryCharge, ToGoCharge, AutoGratuity, SalesTax, ReceiptMessage1, ReceiptMessage2, ReceiptMessage3, CCMessage1, CCMessage2, DigitsInTicketNumber FROM LocationOverview WHERE Username = '" & leUsername & "'", sql.cn)
        '     cmd = New SqlClient.SqlCommand("SELECT CompanyName, Address1, Address2, City, State, Zip, PhoneNumber, Username, Password, UsingDefaults, AutoPrint, EndOfWeek FROM LocationOverview WHERE CompanyID = '" & CompanyID & "' AND LocationID = '" & LocationID & "'", sql.cn)
        dtr = cmd.ExecuteReader
        dtr.Read()

        If dtr.HasRows Then 'dtr.HasRows Then
            companyInfo.CompanyID = dtr("CompanyID")
            companyInfo.LocationID = dtr("LocationID")
            companyInfo.companyName = dtr("CompanyName")
            If Not dtr("LocationName") Is DBNull.Value Then
                companyInfo.locationName = dtr("LocationName")
            End If
            companyInfo.locationUsername = dtr("Username")
            companyInfo.locationPassword = dtr("Password")
            companyInfo.locationCity = dtr("City")
            companyInfo.locationState = dtr("State")
            companyInfo.address1 = dtr("Address1")
            companyInfo.address2 = dtr("Address2")
            companyInfo.locationPhone = dtr("PhoneNumber")
            companyInfo.usingDefaults = dtr("UsingDefaults")
            companyInfo.autoCloseCheck = dtr("AutoPrint")
            companyInfo.endOfWeek = dtr("EndOfWeek")
            companyInfo.onlyOneLocation = dtr("OnlyOneLocation")
            If Not dtr("MerchantID") Is DBNull.Value Then
                companyInfo.merchantID = dtr("MerchantID")
            End If
            If Not dtr("MerchantIDPhone") Is DBNull.Value Then
                companyInfo.merchantIDPhone = dtr("MerchantIDPhone")
            End If
            If Not dtr("OperatorID") Is DBNull.Value Then
                companyInfo.operatorID = dtr("OperatorID")
            End If
            If Not dtr("LocalHostName") Is DBNull.Value Then
                companyInfo.localHostName = dtr("LocalHostName")
            End If
            If Not dtr("dbName") Is DBNull.Value Then
                companyInfo.dbName = dtr("dbName")
            End If

            companyInfo.numberOfTerminals = dtr("NumberTerminals")
            companyInfo.numberOfFloorPlans = dtr("NumberFloorPlans")
            companyInfo.timeoutMultiplier = dtr("TimeoutSeconds")
            companyInfo.colorAdjust = dtr("ColorAdjust")
            If Not dtr("VersionNumber") Is DBNull.Value Then
                companyInfo.VersionNumber = dtr("VersionNumber")
            End If
            If Not dtr("LastUpdate") Is DBNull.Value Then
                companyInfo.lastUpdate = dtr("LastUpdate")
            End If
            companyInfo.autoUpdate = dtr("AutoUpdate")
            companyInfo.usingBartenderMethod = dtr("UsingBartenderMethod")
            companyInfo.calculateAvgByEntrees = dtr("CalculateAvgByEntrees")
            companyInfo.isKitchenExpiditer = dtr("IsKitchenExpiditer")
            companyInfo.isDelivery = dtr("IsDelivery")
            companyInfo.autoCloseCheck = dtr("AutoCloseCheck")
            companyInfo.deliveryCharge = dtr("DeliveryCharge")
            companyInfo.togoCharge = dtr("ToGoCharge")
            companyInfo.autoGratuityPercent = dtr("AutoGratuity")
            companyInfo.salesTax = dtr("SalesTax")

            If Not dtr("ReceiptMessage1") Is DBNull.Value Then
                companyInfo.receiptMessage1 = dtr("ReceiptMessage1")
            End If
            If Not dtr("ReceiptMessage2") Is DBNull.Value Then
                companyInfo.receiptMessage2 = dtr("ReceiptMessage2")
            End If
            If Not dtr("ReceiptMessage3") Is DBNull.Value Then
                companyInfo.receiptMessage3 = dtr("ReceiptMessage3")
            End If
            If Not dtr("CCMessage1") Is DBNull.Value Then
                companyInfo.CCMessage1 = dtr("CCMessage1")
            End If
            If Not dtr("CCMessage2") Is DBNull.Value Then
                companyInfo.CCMessage2 = dtr("CCMessage2")
            End If

            companyInfo.digitsInTicketNumber = dtr("DigitsInTicketNumber")
        Else
            dtr.Close()
            sql.cn.Close()
            IncorrectUsername(leUsername)
            Exit Sub
        End If

        dtr.Close()
        sql.cn.Close()
    End Sub


    Private Sub IncorrectUsername(ByVal leUsername As String)
        Dim info As DataSet_Builder.Information_UC

        Me.initLogon.LoginUsername = ""
        Me.initLogon.LoginPassword = ""
        Me.initLogon.txtLoginUsername.Text = ""
        Me.initLogon.txtLoginPassword.Text = ""

        Me.initLogon.RessetFocus()

        info = New DataSet_Builder.Information_UC("Username " & leUsername & " can not be found or incorrect Password.")
        info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
        Me.Controls.Add(info)
        info.BringToFront()

    End Sub

    Private Sub IncorrectInitLogin()
        Dim info As DataSet_Builder.Information_UC

        Me.initLogon.LoginUsername = ""
        Me.initLogon.LoginPassword = ""
        Me.initLogon.txtLoginUsername.Text = ""
        Me.initLogon.txtLoginPassword.Text = ""

        Me.initLogon.RessetFocus()

        info = New DataSet_Builder.Information_UC("Incorrect Password.")
        info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
        Me.Controls.Add(info)
        info.BringToFront()

    End Sub

    Private Sub TestManagerAccess()


    End Sub


    Private Sub ClockInEmployeeClicked() Handles clockInPanel.ApplyClockInCheck
        '     Dim clockInJunk As ClockInInfo
        '
        '       clockInJunk = sender
        '      ClockInEmployee(clockInJunk, True)
        MsgBox(currentClockEmp.FullName & " has just clocked in at: " & Now.ToString)
        loginPad.btnNumberClear_Click()
        pnlLogin.Visible = True
        MakeLoginPadVisible()

        AddHandler pnlLogin.Click, AddressOf Me.ReceiveFocus

    End Sub








    Private Function ClockInEmployeeTerminal222(ByVal clockInJunk As ClockInInfo222)
        Dim newEmployee As Employee
        Dim oRow As DataRow

        For Each oRow In dsBackup.Tables("EmployeeTerminal").Rows
            If oRow("EmployeeNumber") = clockInJunk.EmpID Then
                newEmployee.EmployeeID = oRow("EmployeeID")
                newEmployee.EmployeeNumber = clockInJunk.EmpID()
                newEmployee.FullName = oRow("FirstName") & " " & oRow("LastName")
                newEmployee.NickName = oRow("NickName")
                If newEmployee.NickName.Length < 1 Then
                    newEmployee.NickName = oRow("FirstName")
                End If
                newEmployee.PasscodeID = oRow("Passcode")
                newEmployee.ReportMgmtAll = oRow("ReportMgmtAll")
                newEmployee.ReportMgmtLimited = oRow("ReportMgmtLimited")
                newEmployee.OperationMgmtAll = oRow("OperationMgmtAll")
                newEmployee.OperationMgmtLimited = oRow("OperationMgmtLimited")
                newEmployee.SystemMgmtAll = oRow("SystemMgmtAll")
                newEmployee.SystemMgmtLimited = oRow("SystemMgmtLimited")
                newEmployee.EmployeeMgmtAll = oRow("EmployeeMgmtAll")
                newEmployee.EmployeeMgmtLimited = oRow("EmployeeMgmtLimited")
            End If
        Next

        Return newEmployee

    End Function


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        GC.Collect()
        Exit Sub


        Dim eee As Employee

        currentTerminal.CurrentDailyCode = 2
        companyInfo.CompanyID = "001111"
        companyInfo.LocationID = "000001"

        Try
            PopulateOrderTables(True)

            GenerateOverrideCodes()

            '222      PopulateTableStatusDesc()

            '222      PopulateOpenTablesAtStartup()
            SetUpPrimaryKeys()

            OnlyForSkippingLogin222()
        Catch ex As Exception
            CloseConnection()


        End Try


        AddHandler pnlLogin.Click, AddressOf Me.ReceiveFocus

        '      currentMenu = New Menu(primaryMenuID, True)
        '     If secondaryMenuID > 0 Then
        '    secondaryMenu = New Menu(secondaryMenuID, False)
        '   End If


        Dim newEmployee As New Employee

        newEmployee.EmployeeID = "1234"
        newEmployee.FullName = "Eric Petruzzelli"
        newEmployee.NickName = "Eric"
        newEmployee.PasscodeID = "1111"
        newEmployee.ShiftID = currentTerminal.CurrentShift
        newEmployee.dbUP = True

        newEmployee.JobCodeName = "Manager"
        newEmployee.Manager = True
        newEmployee.Cashier = False
        newEmployee.Bartender = False
        newEmployee.Server = False
        newEmployee.Hostess = False
        newEmployee.PasswordReq = True
        newEmployee.ClockInReq = False
        newEmployee.ReportTipsReq = False
        newEmployee.ShareTipsReq = False
        newEmployee.ReportMgmtAll = True
        newEmployee.ReportMgmtLimited = True
        newEmployee.OperationMgmtAll = True
        newEmployee.OperationMgmtLimited = True
        newEmployee.SystemMgmtAll = True
        newEmployee.SystemMgmtLimited = True
        newEmployee.EmployeeMgmtAll = True
        newEmployee.EmployeeMgmtLimited = True

        GenerateWorkingCollections.AddEmployeeToWorkingCollection(newEmployee)
        currentManagers.Add(newEmployee)
        '    todaysFloorPersonnel.Add(newEmployee)

        newEmployee = New Employee

        newEmployee.EmployeeID = "1111"
        newEmployee.FullName = "Mark Manager"
        newEmployee.NickName = "Mark"
        newEmployee.JobCodeName = "Manager"
        newEmployee.PasscodeID = "1111"
        newEmployee.ShiftID = currentTerminal.CurrentShift
        newEmployee.dbUP = True

        newEmployee.Manager = True
        newEmployee.Cashier = False
        newEmployee.Bartender = False
        newEmployee.Server = False
        newEmployee.Hostess = False
        newEmployee.PasswordReq = True
        newEmployee.ClockInReq = False
        newEmployee.ReportTipsReq = False
        newEmployee.ShareTipsReq = False
        newEmployee.ReportMgmtAll = False
        newEmployee.ReportMgmtLimited = True
        newEmployee.OperationMgmtAll = False
        newEmployee.OperationMgmtLimited = True
        newEmployee.SystemMgmtAll = False
        newEmployee.SystemMgmtLimited = True
        newEmployee.EmployeeMgmtAll = False
        newEmployee.EmployeeMgmtLimited = True

        GenerateWorkingCollections.AddEmployeeToWorkingCollection(newEmployee)
        currentManagers.Add(newEmployee)
        '     todaysFloorPersonnel.Add(newEmployee)

        newEmployee = New Employee

        newEmployee.EmployeeID = "4001"
        newEmployee.FullName = "Beth Bartender"
        newEmployee.NickName = "Beth"
        newEmployee.JobCodeName = "Bartender"
        newEmployee.PasscodeID = "1111"
        newEmployee.ShiftID = currentTerminal.CurrentShift
        newEmployee.dbUP = True

        newEmployee.Manager = False
        newEmployee.Cashier = False
        newEmployee.Bartender = True
        newEmployee.Server = False
        newEmployee.Hostess = False
        newEmployee.PasswordReq = False
        newEmployee.ClockInReq = True
        newEmployee.ReportTipsReq = True
        newEmployee.ShareTipsReq = False
        newEmployee.ReportMgmtAll = False
        newEmployee.ReportMgmtLimited = True
        newEmployee.OperationMgmtAll = False
        newEmployee.OperationMgmtLimited = True
        newEmployee.SystemMgmtAll = False
        newEmployee.SystemMgmtLimited = True
        newEmployee.EmployeeMgmtAll = False
        newEmployee.EmployeeMgmtLimited = False

        GenerateWorkingCollections.AddEmployeeToWorkingCollection(newEmployee)
        '************************************
        '   next line only for bartender job
        currentBartenders.Add(newEmployee)
        '    todaysFloorPersonnel.Add(newEmployee)



        newEmployee = New Employee

        newEmployee.EmployeeID = "4002"
        newEmployee.FullName = "Benjamin Bartender"
        newEmployee.NickName = "Ben"
        newEmployee.JobCodeName = "Bartender"
        newEmployee.PasscodeID = "1111"
        newEmployee.ShiftID = currentTerminal.CurrentShift
        newEmployee.dbUP = True

        newEmployee.Manager = False
        newEmployee.Cashier = False
        newEmployee.Bartender = True
        newEmployee.Server = False
        newEmployee.Hostess = False
        newEmployee.PasswordReq = False
        newEmployee.ClockInReq = True
        newEmployee.ReportTipsReq = True
        newEmployee.ShareTipsReq = False
        newEmployee.ReportMgmtAll = False
        newEmployee.ReportMgmtLimited = False
        newEmployee.OperationMgmtAll = False
        newEmployee.OperationMgmtLimited = False
        newEmployee.SystemMgmtAll = False
        newEmployee.SystemMgmtLimited = False
        newEmployee.EmployeeMgmtAll = False
        newEmployee.EmployeeMgmtLimited = False

        GenerateWorkingCollections.AddEmployeeToWorkingCollection(newEmployee)
        '************************************
        '   next line only for bartender job
        currentBartenders.Add(newEmployee)
        '    todaysFloorPersonnel.Add(newEmployee)



        newEmployee = New Employee

        newEmployee.EmployeeID = "2000"
        newEmployee.FullName = "Samuel Server"
        newEmployee.NickName = "Sam"
        newEmployee.JobCodeName = "Server"
        newEmployee.PasscodeID = "1111"
        newEmployee.ShiftID = currentTerminal.CurrentShift
        newEmployee.dbUP = True

        newEmployee.Manager = False
        newEmployee.Cashier = False
        newEmployee.Bartender = False
        newEmployee.Server = True
        newEmployee.Hostess = False
        newEmployee.PasswordReq = False
        newEmployee.ClockInReq = True
        newEmployee.ReportTipsReq = True
        newEmployee.ShareTipsReq = True
        newEmployee.ReportMgmtAll = False
        newEmployee.ReportMgmtLimited = False
        newEmployee.OperationMgmtAll = False
        newEmployee.OperationMgmtLimited = False
        newEmployee.SystemMgmtAll = False
        newEmployee.SystemMgmtLimited = False
        newEmployee.EmployeeMgmtAll = False
        newEmployee.EmployeeMgmtLimited = False

        GenerateWorkingCollections.AddEmployeeToWorkingCollection(newEmployee)
        '************************************
        currentServers.Add(newEmployee)
        '     todaysFloorPersonnel.Add(newEmployee)

        If Not initLogon Is Nothing Then
            Me.initLogon.Dispose()
        End If

        Dim emp As Employee

        For Each emp In AllEmployees
            If emp.EmployeeNumber = "4001" Then         'this logs in Beth
                '222    LoginEmployee(emp)
                PerformEmployeeFunctions(emp)
            End If
        Next


    End Sub

    Private Sub OnlyForSkippingLogin222()

        '
        '        sql.SqlSelectCommandCurrentTables.Parameters("@CompanyID").Value = CompanyID
        '       sql.SqlSelectCommandCurrentTables.Parameters("@LocationID").Value = LocationID

        '      Try
        '           '   gets a collection of all tables in TableOverview
        '           sql.cn.Open()
        '           sql.SqlDataAdapterCurrentTables.Fill(dsOrder.Tables("CurrentTables"))
        '          sql.cn.Close()
        '      Catch ex As Exception
        ''          CloseConnection()

        '     End Try

        '      sql.SqlSelectCommandClockedIn.Parameters("@CompanyID").Value = CompanyID
        sql.SqlSelectCommandClockedIn.Parameters("@LocationID").Value = companyInfo.LocationID

        Try
            'this is used in each time the emp pulls their tables
            'it poplutes their current job codes and authorization
            '   populates logged-In employees
            '   All employees in LoginTracking where LogOut datetime IS Null
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlClockedIn.Fill(dsEmployee.Tables("LoggedInEmployees"))
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
        End Try


        Dim oRow As DataRow

        '   this is a one time sub: only when starting total solution
        '   once done we keep track of status by Statustbl#

        Dim dtr As SqlClient.SqlDataReader
        Dim tn As Integer

        Dim maxExpForTable As Integer
        Dim currentStatus As Integer
        Dim currentStatusTime As DateTime

        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        For Each oRow In dsOrder.Tables("CurrentTables").Rows
            'must change above
            maxExpForTable = 0   'reseting
            tn = oRow("TableNumber")

            Dim cmd = New SqlClient.SqlCommand("SELECT MAX(ExperienceNumber) _maxExp FROM ExperienceTable WHERE TableNumber = '" & tn & "'", sql.cn)
            dtr = cmd.executereader
            dtr.Read()
            If Not dtr("_maxExp") Is DBNull.Value Then
                maxExpForTable = dtr("_maxExp")
            End If
            dtr.Close()

            If Not maxExpForTable = 0 Then
                cmd = New SqlClient.SqlCommand("SELECT TableStatusID, StatusTime FROM ExperienceStatusChange WHERE ExperienceNumber = '" & maxExpForTable & "'", sql.cn)
                'since input to this table is cronological : the last entry is the last status
                dtr = cmd.executereader
                While dtr.Read()
                    currentStatus = dtr("TableStatusID")
                    currentStatusTime = dtr("StatusTime")
                End While
                dtr.Close()

            Else
                '   only when table has never had an experience (may change)
                Dim nRow As DataRow
                For Each nRow In dsOrder.Tables("CurrentTables").Rows
                    If nRow("TableNumber") = tn Then
                        If nRow("Available") = True Then
                            currentStatus = 1
                        Else
                            currentStatus = 0
                        End If
                        Exit For
                    End If
                Next
                currentStatusTime = Now
            End If

            AssignStatus222(tn, currentStatus, currentStatusTime)
            '   this is just for when we initialize the program
            '   at the beginning of the day
        Next
        sql.cn.Close()

    End Sub

    Private Sub StartRecoTimer222()
        infoRecoTimer = New Timer
        AddHandler infoRecoTimer.Tick, AddressOf RemoveInfoReco
        infoRecoTimer.Interval = 2000
        infoRecoTimer.Start()

        infoReconnect = New DataSet_Builder.Information_UC("Attempting to Reconnect To Server")
        infoReconnect.Location = New Point((Me.Width - infoReconnect.Width) / 2, (Me.Height - infoReconnect.Height) / 2)
        Me.Controls.Add(infoReconnect)
        infoReconnect.BringToFront()

    End Sub

    Private Sub ClosingTableScreen() Handles tableScreen.ExitingTableScreen
        'this is our exit from table_screen

        pnlLogin.Visible = True
        MakeTable_ScreenNotVisible()
        readAuth.ActiveScreen = "Login"
        '      tableScreen.Visible = False
        SetDateTime()

        '   If mainServerConnected = False Then
        '    CheckingDatabaseConection()
        '      MsgBox(mainServerConnected, , "Server UP?????")
        '  End If

        '********
        '911 for tesing only
        '      Dim emp As Employee
        '      emp = GenerateOrderTables.TestUsernamePassword("4001", False)
        '      LoginEmployee(emp)
        '      loginPad.btnNumberClear_Click()
        '      pnlLogin.Visible = False
        ''     MakeLoginPadVisibleNOT()
        '    MakeClockOutBooleanFalse()
        '911

    End Sub

    '************
    '   read Auth

    Private Sub CardRead_Failed() Handles readAuth.CardReadFailed
        ' ResetTimer()

        MsgBox("Card Read FAILED")

        Select Case readAuth.ActiveScreen
            Case "Login"

            Case "OrderScreen"

            Case "CloseCheck"

            Case "SeatingTab"

            Case "DeliveryScreen"

            Case "Manager"

        End Select
    End Sub

    Private Sub NewCardRead(ByRef newPayment As DataSet_Builder.Payment) Handles readAuth.CardReadSuccessful
        '  ResetTimer()

        Select Case readAuth.ActiveScreen
            Case "Login"

            Case "OrderScreen"

                If currentTerminal.TermMethod = "Quick" Then
                    CloseScreenVisible(QuickOrder.IsManagerMode)
                    QuickOrder.Visible = False
                Else
                    CloseScreenVisible(activeOrder.IsManagerMode)
                    activeOrder.Visible = False
                End If
                activeSplit._closeCheck.ProcessCreditRead(newPayment)

            Case "CloseCheck"
                '    ProcessCreditRead(newPayment)
                activeSplit._closeCheck.ProcessCreditRead(newPayment)

            Case "SeatingTab"

                If activeSplit Is Nothing Then
                    InitializeSplitChecks(False) '_isFromManager)
                    activeSplit.Visible = False
                End If
                activeSplit._closeCheck.ProcessCreditRead(newPayment)
                currentTable.TabID = newPayment.TabID
                currentTable.TabName = newPayment.Name
                LoadTabIDinExperinceTable()

                ' CustomerLoyalty()
                If SeatingTab.StartedFrom = "Manager" Then
                    MgrOrderScreen()
                Else 'If SeatingTab.StartedFrom = "OrderScreen" Then
                    OrderScreen(Nothing)
                    ' ElseIf SeatingTab.StartedFrom = "TableScreen" Then
                End If

                SeatingTab.Visible = False
                tableScreen.EnableTables_Screen()
                'readAuth assiged in OrderScreen & MgrOrderScreen

            Case "DeliveryScreen"
                ' *** not sure if this is correct,not sure about
                ' the AddPayment Collection in Read Credit ????
                '444      GenerateOrderTables.CreateTabAcctPlaceInExperience(newpayment)
                DeliveryScreen.TempAccountNumber = newPayment.SpiderAcct
                'might need to  SetAccountSearch() , currently we are doing before swipe activate

                If DeliveryScreen.attemptedToEdit = True Then
                    DeliveryScreen.attemptedToEdit = False
                    GenerateOrderTables.UpdateTabInfo(DeliveryScreen.StartInSearch)
                End If
                GenerateOrderTables.PopulateSearchAccount(newPayment.SpiderAcct) ', -123456789)
                DeliveryScreen.BindDataAfterSearch()

            Case "Manager"
        End Select

    End Sub

    Private Sub ManagementCardRead(ByVal emp As DataSet_Builder.Employee) Handles readAuth.ManagementCardSwiped
        '  ResetTimer()
        Dim loginEnter As String

        Select Case readAuth.ActiveScreen
            Case "Login"

                loginEnter = emp.EmployeeNumber & emp.PasscodeID
                LoginRoutine(loginEnter)
                '      If Not managementScreen Is Nothing Then
                'we are doing this in LoginRoutine if Manager = True
                '   readAuth.ActiveScreen = "Manager"
                '  End If

            Case "OrderScreen"
                If currentTerminal.TermMethod = "Quick" Then
                    QuickOrder.Visible = False
                Else
                    activeOrder.Visible = False
                End If
                If Not SeatingTab Is Nothing Then
                    SeatingTab.Visible = False
                End If
                If Not DeliveryScreen Is Nothing Then
                    DeliveryScreen.Visible = False
                End If

                GenerateOrderTables.ReleaseCurrentlyHeld()
                GenerateOrderTables.SaveOpenOrderData()
                loginEnter = emp.EmployeeNumber & emp.PasscodeID
                LoginRoutine(loginEnter)
                If Not managementScreen Is Nothing Then
                    If currentTable.IsClosed = True Then
                        GenerateOrderTables.CreateClosedDataViews()
                    End If
                    managementScreen.DisplayOrderAdjustmentStep2(currentTable.ExperienceNumber, False, currentTable.IsClosed)
                End If


            Case "CloseCheck"
                activeSplit.Visible = False
                '     readAuth.GiftAddingAmount = False
                '    readAuth.IsNewTab = False

                GenerateOrderTables.ReleaseCurrentlyHeld()
                GenerateOrderTables.SaveOpenOrderData()
                loginEnter = emp.EmployeeNumber & emp.PasscodeID
                LoginRoutine(loginEnter)

                If Not managementScreen Is Nothing Then
                    If currentTable.IsClosed = True Then
                        GenerateOrderTables.CreateClosedDataViews()
                    End If
                    managementScreen.DisplayOrderAdjustmentStep2(currentTable.ExperienceNumber, False, currentTable.IsClosed)
                End If

            Case "TableScreen"
                MakeTable_ScreenNotVisible()
                '  tableScreen.Visible = False
                loginEnter = emp.EmployeeNumber & emp.PasscodeID
                LoginRoutine(loginEnter)

            Case "SeatingTab"

            Case "DeliveryScreen"

            Case "Manager"


                If Not managementScreen Is Nothing Then
                    'should always be something here, b/c from manager
                    If Not managementScreen.mainManager Is Nothing Then
                        If managementScreen.mainManager.mgrLargeNumberPad.Visible = True Then
                            '     managementScreen.mainManager.Dispose()
                            '    managementScreen.mainManager = Nothing
                            Me.managementScreen.Dispose()
                            Me.managementScreen = Nothing

                            'if mainManager.visible  then we still not in Management area
                            loginEnter = emp.EmployeeNumber & emp.PasscodeID
                            LoginRoutine(loginEnter)
                            '     managementScreen.mainManager.Dispose()
                        End If

                    End If

                End If


                '     GenerateOrderTables.AssignManagementAuthorization(actingManager)
                '    DisplayLabelsBasedOnAuth()

        End Select

    End Sub

    Private Sub SetReadAuthValues()
        readAuth.GiftAddingAmount = False
        readAuth.IsNewTab = False
        '  readAuth.ActiveScreen = "Login"


        'we may not put below here, but in individual subs
        Select Case readAuth.ActiveScreen
            Case "Login"

            Case "CloseCheck"

            Case "SeatingTab"
                '    If tn Is Nothing Then
                'this means this is from Login, there is no Tab Name 
                'therefeor is for New Tab Open
                'readAuth.IsNewTab = True
                '      End If
            Case "DeliveryScreen"

            Case "Manager"


        End Select
    End Sub

    Friend Sub EnterTabNameFromSwipe(ByVal tabName As String) Handles readAuth.EnteringTabNameInKeyboard
        Select Case readAuth.ActiveScreen
            Case "Login"

            Case "OrderScreen"

            Case "CloseCheck"

            Case "SeatingTab"   'Seating_EnterTab
                'currently Seating Tab is instantiated from 3 different places
                ' therfore this does not work
                If Not SeatingTab Is Nothing Then
                    SeatingTab.EnterTabNameFromSwipe(tabName)
                ElseIf Not activeOrder Is Nothing Then
                    If Not activeOrder.SeatingTab Is Nothing Then
                        activeOrder.SeatingTab.Dispose()
                    End If
                ElseIf Not QuickOrder Is Nothing Then
                    If Not QuickOrder.SeatingTab Is Nothing Then
                        QuickOrder.SeatingTab.Dispose()
                    End If
                ElseIf Not managementScreen Is Nothing Then
                    If Not managementScreen.SeatingTab Is Nothing Then
                        managementScreen.SeatingTab.Dispose()
                    End If
                End If

            Case "DeliveryScreen"

            Case "Manager"

        End Select
    End Sub

    Private Sub ReturnGiftCardAddToFalseEvent() Handles readAuth.RetruningGiftAddingAmountToFalse

        Select Case readAuth.ActiveScreen
            Case "Login"

            Case "OrderScreen"

            Case "CloseCheck"
                activeSplit._closeCheck.ReturnGiftCardAddToFalse()

            Case "SeatingTab"

            Case "TabEnterScreen"

            Case "Manager"

        End Select

    End Sub

    Private Sub ClosingFastCash() Handles activeOrder.CloseFastCash, QuickOrder.CloseFastCash

        Dim oRow As DataRow

        If currentTerminal.TermMethod = "Quick" Then
            CloseScreenVisible(QuickOrder.IsManagerMode)
        Else
            CloseScreenVisible(activeOrder.IsManagerMode)
        End If

        activeSplit._closeCheck.CashButtonClicked()
        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("Applied") = False Then
                    oRow("Applied") = True
                End If
            End If
        Next

        If ds.Tables("RoutingChoice").Rows.Count > 0 Then
            '   Routing = 0 means no service printer
            ' this forces send order, if Bal zero =, but only is service printer
            If currentTerminal.TermMethod = "Quick" Then
                Me.QuickOrder.SendingOrderRoutine()
            Else
                Me.activeOrder.SendingOrderRoutine()
            End If
        End If
        GenerateOrderTables.ReleaseTableOrTab()
        GenerateOrderTables.ReleaseCurrentlyHeld()
        GenerateOrderTables.SaveOpenOrderData()
        currentTable.IsClosed = True
        If currentTerminal.TermMethod = "Quick" Then
            QuickOrder.GetReadyForNewTicket()
        Else
            activeOrder.GetReadyForNewTicket()
        End If

        '     activeSplit._closeCheck.ClosingAndReleaseRoutine(True)
        activeSplit.Visible = False
        readAuth.IsNewTab = False

    End Sub

    Private Sub MakingGiftAddingAmountTrue() Handles activeSplit.MakeGiftAddingAmountTrue

        readAuth.GiftAddingAmount = True

    End Sub


    Private Sub RemoveInfoReco(ByVal sender As Object, ByVal e As System.EventArgs)
        infoReconnect.Dispose()

    End Sub
    Private Sub RemoveOpenButtonTest() Handles openProgram.RestaurantOpening

        mainServerConnected = True ' just resetting
        tablesFilled = False       ' resetting
        AddHandler pnlLogin.Click, AddressOf Me.ReceiveFocus

        DisplayLoginScreen()
        SetDateTime()
        If currentServer Is Nothing Then
            currentServer = New Employee
        End If
        If currentClockEmp Is Nothing Then
            currentClockEmp = New Employee
        End If

        If Not typeProgram = "Online_Demo" Then
            readAuth = New ReadCredit(False)
            readAuth.GiftAddingAmount = False
            readAuth.IsNewTab = False
            readAuth.ActiveScreen = "Login"
        End If
    End Sub

    Private Sub ReReadCredit_Click() Handles managementScreen.ReReadCredit

        If Not readAuth Is Nothing Then
            readAuth = Nothing
        End If
        readAuth = New ReadCredit(False)
        readAuth.GiftAddingAmount = False
        readAuth.IsNewTab = False
        readAuth.ActiveScreen = "Login"

    End Sub

    Private Sub RemoveOpenButton(ByVal openingInfo As OpenInfo) Handles openProgram.RestuarantOpen
        '222 I think
        Dim menuName As String

        AddHandler pnlLogin.Click, AddressOf Me.ReceiveFocus

        '   currentTable = New DinnerTable
        '     currentServer = New Employee
        '    currentTerminal = New Terminal

        currentTerminal.CurrentDailyCode = openingInfo.dailyCode
        currentTerminal.primaryMenuID = openingInfo.pMenu
        currentTerminal.secondaryMenuID = openingInfo.sMenu
        currentTerminal.CurrentMenuID = openingInfo.pMenu
        currentTerminal.initPrimaryMenuID = openingInfo.pMenu
        currentTerminal.currentPrimaryMenuID = openingInfo.pMenu

        If Not openingInfo.termMethod Is Nothing Then
            currentTerminal.TermMethod = openingInfo.termMethod
        End If
        '      If currentTerminal.TermMethod = "Quick" Then
        btnClockOut.Visible = True
        '     End If

        ' ******moved open drawer from here

        '***    InitializeOrderForm()
        '     InitializeSplitChecks()
        '    actingManager = Nothing

        '    GenerateOverrideCodes() 'for now not pulling from database


        If mainServerConnected = False Then
            SetDateTime()
            openProgram.Dispose()
            'this is just b/c we used this so much incorrectly throughout the code
            '       mainServerConnected = True
            Exit Sub
        End If

        If typeProgram = "Online_Demo" Then
            openProgram.Dispose()
            Exit Sub
        End If

        If tablesFilled = True Then
            'this means the first half of the Phoenix tables are filled
            '   otherwise Phoenix connection is down
            'False means we already downloaded the LAST MENU SAVED

            Try
                GenerateOrderTables.TempConnectToPhoenix()
                '         PopulateNONOrderTables()
                '        PopulateMenuSupport()
                '       PopulateTerminalData()               'FloorPlan
                '      CreateTerminals()

                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                currentMenu = New Menu(currentTerminal.primaryMenuID, True)
                If currentTerminal.secondaryMenuID > 0 Then
                    secondaryMenu = New Menu(currentTerminal.secondaryMenuID, False)
                End If
                sql.cn.Close()

                GenerateOrderTables.ConnectBackFromTempDatabase()
            Catch ex As Exception
                'nned to reload all Info stored on Phoenix from XML
                CloseConnection()
                GenerateOrderTables.ConnectBackFromTempDatabase()
                MsgBox(ex.Message & " Connection Down, Removing Opening Screen. Select saved menu.")
                ServerNOTConectedStartOfProgram()
                Exit Sub
            End Try

            CreateMenuString222(currentTerminal.primaryMenuID, menuName)
            If currentTerminal.secondaryMenuID > 0 Then
                menuName = menuName + "_"
                CreateMenuString222(currentTerminal.secondaryMenuID, menuName)
            End If

            SetUpPrimaryKeys()
            '       GenerateOrderTables.CreateTerminals()
            SaveBackupDATAds(menuName)

            SetDateTime()
            TestForShiftAndMenuChanges()
            openProgram.Dispose()

        Else
            MsgBox("Connection Down, Removing Opening Screen Default. Select saved menu.")
            ServerNOTConectedStartOfProgram()
        End If

        Try
            DetermineOpenCashDrawer(currentTerminal.CurrentDailyCode)
            With dvTermsOpen
                .Table = dtTermsOpen
                .RowFilter = "TerminalsPrimaryKey = " & currentTerminal.TermPrimaryKey
            End With

            If dvTermsOpen.Count = 1 Then
                currentTerminal.TerminalsOpenID = dvTermsOpen(0)("TerminalsOpenID")
                actingManager = Nothing
            Else
                If currentTerminal.HasCashDrawer = True Then
                    '    MsgBox("Do NOT forget to open your Cash Drawer")
                    If actingManager.OperationMgmtAll = True Or actingManager.OperationMgmtLimited = True Then
                        '666
                        OpenCloseCashDrawer(0)  'this 0 means this terminal only
                        '***********
                        'we never get rid of actingManager = Nothing
                        'because we need it to open cash drawer
                        ' maybe some wierd memory leak, don;t think so 
                    Else
                        actingManager = Nothing
                    End If
                Else
                    actingManager = Nothing
                End If
                currentTerminal.TerminalsOpenID = 0
            End If
        Catch ex As Exception
            currentTerminal.TerminalsOpenID = 0
        End Try

    End Sub

    Private Sub OpenCloseCashDrawer(ByVal _thisCashTerminal As Integer)

        cashDrawer = New CashDrawer_UC(_thisCashTerminal)
        cashDrawer.Location = New Point((Me.Width - cashDrawer.Width) / 2, (Me.Height - cashDrawer.Height) / 2)
        Me.Controls.Add(cashDrawer)
        cashDrawer.BringToFront()

    End Sub

    Private Sub SaveBackupDATAds(ByVal menuName As String)
        '222 i think

        Try
            If typeProgram = "Online_Demo" Then Exit Sub

            GenerateOrderTables.CreatespiderPOSDirectory()

            'starter menu is just a subset of ds dataset
            dsStarter.WriteXml("c:\Data Files\spiderPOS\StarterMenu.xml", XmlWriteMode.WriteSchema)
            ds.WriteXml("c:\Data Files\spiderPOS\Menu\" & menuName & ".xml", XmlWriteMode.WriteSchema)

            '***************
            '   need for Demo
            ' DO NOT DELETE below
            '      dsStarter.WriteXml("StarterMenu.xml", XmlWriteMode.WriteSchema)
            'ds.WriteXml("Lunch_Dinner_QuickDemo.xml", XmlWriteMode.WriteSchema)
            '     ds.WriteXml("Lunch_Dinner.xml", XmlWriteMode.WriteSchema)
            '    dsOrder.WriteXml("OrderData.xml", XmlWriteMode.WriteSchema)
            '     dsEmployee.WriteXml("EmployeeData.xml", XmlWriteMode.WriteSchema)
            '     dsCustomer.WriteXml("CustomerData.xml", XmlWriteMode.WriteSchema)


        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub OnlineDemoStartOfProgram()

        connectionDown = New ConnectionDown_UC
        connectionDown.Location = New Point((Me.Width - connectionDown.Width) / 2, (Me.Height - connectionDown.Height) / 2)

        connectionDown.MenuChoiceRoutine("Lunch_Dinner")

    End Sub

    Private Sub ServerNOTConectedStartOfProgram()


        '    *** we should not bring program UP
        '   inform customer down and set timer to check for connection 

        '       CloseConnection()
        '      GenerateOrderTables.ConnectBackFromTempDatabase()
        If localConnectServer = "eglobalmain\eglobalmain" Then
            'this is temporary fix b/c of local admin for clients
            companyInfo.companyName = "eGlobalPartners"
        End If
        '    MsgBox("Connection Down. Select saved menu.")
        mainServerConnected = False

        connectionDown = New ConnectionDown_UC
        connectionDown.Location = New Point((Me.Width - connectionDown.Width) / 2, (Me.Height - connectionDown.Height) / 2)
        Me.Controls.Add(connectionDown)
        connectionDown.BringToFront()

    End Sub

    Private Sub CanceledConnectionHelp() Handles connectionDown.ConnectionHelpCanceled

        MsgBox("Connection to main server is down. You must load a Menu. Please call spiderPOS at 404.869.4700")
        OpenScreenClosed()
        connectionDown.Dispose()

    End Sub

    Private Sub OldMenuLoaded() Handles connectionDown.ArchiveMenuLoaded

        Me.initLogon.Dispose()
        connectionDown.Dispose()
        '  DisplayOpeningScreen(companyInfo.companyName)

        If tablesFilled = False Then
            StartOfProgram(companyInfo.companyName)
        Else
            DisplayOpeningScreen(companyInfo.companyName)
        End If

    End Sub

    Private Sub ClosePOS2() Handles openProgram.ClosePOS
        Me.Dispose()

    End Sub

    Private Sub ClosePOS() Handles managementScreen.ClosePOS ', openProgram.ClosePOS

        Me.Dispose()
        Exit Sub

        'testing below, it is now in PopulateLocationOpening
        Dim changedVersion As Boolean
        Dim lastVersion As Integer

        Try
            PopulateLocationOpening(False)

            If ds.Tables("LocationOpening").Rows.Count > 0 Then
                If My.Application.Info.Version.MinorRevision > ds.Tables("LocationOpening").Rows(0)("LastAppVersion") Then
                    lastVersion = My.Application.Info.Version.MinorRevision
                    changedVersion = True
                End If
            End If
            If changedVersion = True Then
                ds.Tables("LocationOpening").Rows(0)("LastAppVersion") = lastVersion
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                sql.SqlLocationOpening.Update(ds.Tables("LocationOpening"))
                sql.cn.Close()
            End If

        Catch ex As Exception
            CloseConnection()
        End Try

        Me.Dispose()
        Exit Sub

        'testing below
        Try
            PopulateLocationOpening(False)
        Catch ex As Exception
            CloseConnection()
            Me.Dispose()
        End Try

        If ds.Tables("LocationOpening").Rows.Count > 0 Then
            If My.Application.Info.Version.MinorRevision < ds.Tables("LocationOpening").Rows(0)("LastAppVersion") Then
                Me.Dispose()
            Else
                InitializeOpeningScreen()
                DisplayOpeningScreen(companyInfo.companyName)
            End If
        End If
        '  Me.Dispose()

    End Sub

    'Merchant Warehouse

    Private Sub Swiped__Encrypted__Load_MWE(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '    Me.AxUSBHID_MWE.PortOpen = True
    End Sub
    Public Sub AxUSBHID_MWE_CardDataChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' this is if we are instantiating the ActiveX in Login
        '     readAuth_MWE.AxUSBHID1_CardDataChanged(sender)
    End Sub


    Private Sub OpenPortAtStart()
        '444   Me.readAuth_MWE.AxUSBHID1.PortOpen = True

    End Sub

    Private Sub OpeningNewTabFrom_MWE(ByVal paymentID As Integer, ByVal tabNameString As String) '444 Handles readAuth_MWE.OpenNewTab

        If GenerateOrderTables.OpenNewTab(-999, tabNameString) = True Then
            'send result back to MWE
            '444        readAuth_MWE.ResultOfOpeningNewTab(paymentID, currentTable.ExperienceNumber)

        Else
            'probably do not need to send back
            'old            readAuth_MWE.ResultOfOpenigNewTab(0)

        End If

    End Sub

    Private Sub PlacingInTabAccount_MWE(ByVal paymentID As Integer, ByVal sa As String, ByVal lName As String, ByVal fname As String, ByVal fullName As String) '444Handles readAuth_MWE.PlaceInTabAccount
        Dim newpayment As New Payment

        newpayment.newPaymentID = paymentID
        newpayment.SpiderAcct = sa
        newpayment.LastName = lName
        newpayment.FirstName = fname
        newpayment.Name = fullName

        CreateTabAcctPlaceInExperience(newpayment)

    End Sub

    Private Function DetermineViewInfoForPaymets(ByVal paymentID As Integer) As DataRowView
        Dim vRow As DataRowView
        '      With dvUnAppliedPaymentsAndCredits_MWE
        '     .Table = readAuth_MWE.dtPaymentsAndCreditsUnauthorized_MWE
        '    .RowFilter = "Applied = False AND ExperienceNumber = '" & currentTable.ExperienceNumber & "' AND CheckNumber = '" & currentTable.CheckNumber & "'"
        '   .Sort = "PaymentFlag"
        '  End With
        For Each vRow In dvUnAppliedPaymentsAndCredits_MWE
            If vRow("PaymentID") = paymentID Then
                Return vRow
            End If
        Next

    End Function

    Private Sub NewCardRead_MWE(ByVal paymentID As Integer) '444Handles readAuth_MWE.CardReadSuccessful
        ' Private Sub NewCardRead_MWE(ByVal _secureNewPayment_MWE As ReadCredit_MWE2.Payment_MWE) Handles readAuth_MWE.CardReadSuccessful
        '  ResetTimer()
        Dim vRow As DataRowView
        vRow = DetermineViewInfoForPaymets(paymentID)
        If vRow Is Nothing Then
            Exit Sub
        End If
        '     Dim newPayment As New Payment
        '   With newPayment
        '          .experienceNumber = _secureNewPayment_MWE.experienceNumber
        '          .PaymentTypeID = _secureNewPayment_MWE.PaymentTypeID
        '          .PaymentTypeName = _secureNewPayment_MWE.PaymentTypeName
        '         .PaymentFlag = _secureNewPayment_MWE.PaymentFlag
        '         .TranType = _secureNewPayment_MWE.TranType
        ''         '         .TranCode=
        '         .AccountNumber=
        '         .ExpDate=
        '         .Swiped=
        '       End With

        Select Case readAuth.ActiveScreen
            Case "Login"

            Case "OrderScreen"

                If currentTerminal.TermMethod = "Quick" Then
                    CloseScreenVisible(QuickOrder.IsManagerMode)
                    QuickOrder.Visible = False
                Else
                    CloseScreenVisible(activeOrder.IsManagerMode)
                    activeOrder.Visible = False
                End If
                activeSplit._closeCheck.ProcessCreditRead_MWE(vRow) '_secureNewPayment_MWE)

            Case "CloseCheck"
                '    ProcessCreditRead(newPayment)
                activeSplit._closeCheck.ProcessCreditRead_MWE(vRow) '_secureNewPayment_MWE)

            Case "SeatingTab"

                If activeSplit Is Nothing Then
                    InitializeSplitChecks(False) '_isFromManager)
                    activeSplit.Visible = False
                End If
                activeSplit._closeCheck.ProcessCreditRead_MWE(vRow) '_secureNewPayment_MWE)
                currentTable.TabID = vRow("TabID") '_secureNewPayment_MWE.TabID '
                currentTable.TabName = vRow("TabName") '_secureNewPayment_MWE.Name '
                LoadTabIDinExperinceTable()

                ' CustomerLoyalty()
                If SeatingTab.StartedFrom = "Manager" Then
                    MgrOrderScreen()
                Else 'If SeatingTab.StartedFrom = "OrderScreen" Then
                    OrderScreen(Nothing)
                    ' ElseIf SeatingTab.StartedFrom = "TableScreen" Then
                End If

                SeatingTab.Visible = False
                tableScreen.EnableTables_Screen()
                'readAuth assiged in OrderScreen & MgrOrderScreen

            Case "DeliveryScreen"
                ' *** not sure if this is correct,not sure about
                ' the AddPayment Collection in Read Credit ????
                '444      GenerateOrderTables.CreateTabAcctPlaceInExperience(newpayment)
                DeliveryScreen.TempAccountNumber = vRow("SpiderAcct") '_secureNewPayment_MWE.SpiderAcct '
                'might need to  SetAccountSearch() , currently we are doing before swipe activate

                If DeliveryScreen.attemptedToEdit = True Then
                    DeliveryScreen.attemptedToEdit = False
                    GenerateOrderTables.UpdateTabInfo(DeliveryScreen.StartInSearch)
                End If
                GenerateOrderTables.PopulateSearchAccount(vRow("SpiderAcct")) '_secureNewPayment_MWE.SpiderAcct) ' ', -123456789)
                DeliveryScreen.BindDataAfterSearch()

            Case "Manager"
        End Select

    End Sub

    Private Sub AuthTHisPayment(ByVal paymentID As Integer, ByVal justActive As Boolean) Handles activeSplit.MerchantAuthPayment
        Dim isApproved
        '444    readAuth_MWE.btnSale_Encrpted_Swipe(paymentID)

        If justActive = False Then
            'send another payment
        End If

    End Sub



End Class
