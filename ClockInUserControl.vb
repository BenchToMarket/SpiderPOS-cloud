Imports DataSet_Builder

Friend Structure ClockInInfo222
    Private _empID As Integer
    Private _passcodeID As Integer
    Private _jobCodeID As Integer
    Private _regPayRate As Decimal
    Private _otPayRate As Decimal
    Private _logInTime As DateTime


    Friend Property EmpID() As Integer
        Get
            Return _empID
        End Get
        Set(ByVal Value As Integer)
            _empID = Value
        End Set
    End Property

    Friend Property PasscodeID() As Integer
        Get
            Return _passcodeID
        End Get
        Set(ByVal Value As Integer)
            _passcodeID = Value
        End Set
    End Property

    Friend Property JobCodeID() As Integer
        Get
            Return _jobCodeID
        End Get
        Set(ByVal Value As Integer)
            _jobCodeID = Value
        End Set
    End Property

    Friend Property RegPayRate() As Decimal
        Get
            Return _regPayRate
        End Get
        Set(ByVal Value As Decimal)
            _regPayRate = Value
        End Set
    End Property

    Friend Property OTPayRate() As Decimal
        Get
            Return _otPayRate
        End Get
        Set(ByVal Value As Decimal)
            _otPayRate = Value
        End Set
    End Property

    Friend Property LogInTime() As DateTime
        Get
            Return _logInTime
        End Get
        Set(ByVal Value As DateTime)
            _logInTime = Value
        End Set
    End Property

End Structure

Friend Structure ClockOutInfo

    Private _timeIn As DateTime
    Private _timeOut As DateTime
    Private _shiftHours As Integer
    Private _shiftMins As Integer
    Private _weekHours As Integer
    Private _weekMins As Integer


    Private _tipableSales As Decimal
    Private _declaredTips As Decimal
    Private _chargedSales As Decimal
    Private _chargedTips As Decimal
    Private _dailyCode As Int64

    Friend Property TimeIn() As DateTime
        Get
            Return _timeIn
        End Get
        Set(ByVal Value As DateTime)
            _timeIn = Value
        End Set
    End Property

    Friend Property TimeOut() As DateTime
        Get
            Return _timeOut
        End Get
        Set(ByVal Value As DateTime)
            _timeOut = Value
        End Set
    End Property

    Friend Property ShiftHours() As Integer
        Get
            Return _shiftHours
        End Get
        Set(ByVal Value As Integer)
            _shiftHours = Value
        End Set
    End Property

    Friend Property ShiftMins() As Integer
        Get
            Return _shiftMins
        End Get
        Set(ByVal Value As Integer)
            _shiftMins = Value
        End Set
    End Property

    Friend Property WeekHours() As Integer
        Get
            Return _weekHours
        End Get
        Set(ByVal Value As Integer)
            _weekHours = Value
        End Set
    End Property

    Friend Property WeekMins() As Integer
        Get
            Return _weekMins
        End Get
        Set(ByVal Value As Integer)
            _weekMins = Value
        End Set
    End Property

    Friend Property TipableSales() As Decimal
        Get
            Return _tipableSales
        End Get
        Set(ByVal Value As Decimal)
            _tipableSales = Value
        End Set
    End Property

    Friend Property DeclaredTips() As Decimal
        Get
            Return _declaredTips
        End Get
        Set(ByVal Value As Decimal)
            _declaredTips = Value
        End Set
    End Property

    Friend Property ChargedSales() As Decimal
        Get
            Return _chargedSales
        End Get
        Set(ByVal Value As Decimal)
            _chargedSales = Value
        End Set
    End Property

    Friend Property ChargedTips() As Decimal
        Get
            Return _chargedTips
        End Get
        Set(ByVal Value As Decimal)
            _chargedTips = Value
        End Set
    End Property

    Friend Property DailyCode() As Int64
        Get
            Return _dailyCode
        End Get
        Set(ByVal Value As Int64)
            _dailyCode = Value
        End Set
    End Property

End Structure

Public Class ClockInUserControl
    Inherits System.Windows.Forms.UserControl

    '  Dim sql As New DataSet_Builder.SQLHelper(connectserver)
    Dim emp As New Employee
    Dim currentPasscodeReq As Boolean
    Dim btnJobName(6) As KitchenButton

    '222 Friend currentClockIn As New ClockInInfo222

    Event ApplyClockInCheck() 'ByVal sender As Object, ByVal e As System.EventArgs)
    Event ClosingClockIn(ByVal sender As Object, ByVal e As System.EventArgs)


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnApplyClockIn As System.Windows.Forms.Button
    '    Friend WithEvents NumberPadSmallClockIn As DataSet_Builder.NumberPadSmall
    Friend WithEvents pnlJobCodes As System.Windows.Forms.Panel
    Friend WithEvents lblClockIn As System.Windows.Forms.Label
    Friend WithEvents NumberPadSmallClockIn As DataSet_Builder.NumberPadMedium
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.NumberPadSmallClockIn = New DataSet_Builder.NumberPadMedium
        Me.pnlJobCodes = New System.Windows.Forms.Panel
        Me.btnCancel = New System.Windows.Forms.Button
        Me.lblClockIn = New System.Windows.Forms.Label
        Me.btnApplyClockIn = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Panel1.Controls.Add(Me.NumberPadSmallClockIn)
        Me.Panel1.Controls.Add(Me.pnlJobCodes)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.lblClockIn)
        Me.Panel1.Controls.Add(Me.btnApplyClockIn)
        Me.Panel1.Location = New System.Drawing.Point(8, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(440, 360)
        Me.Panel1.TabIndex = 6
        '
        'NumberPadSmallClockIn
        '
        Me.NumberPadSmallClockIn.BackColor = System.Drawing.Color.SlateGray
        Me.NumberPadSmallClockIn.DecimalUsed = False
        Me.NumberPadSmallClockIn.ForeColor = System.Drawing.Color.Black
        Me.NumberPadSmallClockIn.IntegerNumber = 0
        Me.NumberPadSmallClockIn.Location = New System.Drawing.Point(240, 32)
        Me.NumberPadSmallClockIn.Name = "NumberPadSmallClockIn"
        Me.NumberPadSmallClockIn.NumberString = ""
        Me.NumberPadSmallClockIn.NumberTotal = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumberPadSmallClockIn.Size = New System.Drawing.Size(192, 296)
        Me.NumberPadSmallClockIn.TabIndex = 12
        '
        'pnlJobCodes
        '
        Me.pnlJobCodes.BackColor = System.Drawing.SystemColors.Info
        Me.pnlJobCodes.Location = New System.Drawing.Point(8, 32)
        Me.pnlJobCodes.Name = "pnlJobCodes"
        Me.pnlJobCodes.Size = New System.Drawing.Size(224, 272)
        Me.pnlJobCodes.TabIndex = 11
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(24, 312)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(64, 40)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        '
        'lblClockIn
        '
        Me.lblClockIn.BackColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(192, Byte))
        Me.lblClockIn.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblClockIn.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClockIn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblClockIn.Location = New System.Drawing.Point(0, 0)
        Me.lblClockIn.Name = "lblClockIn"
        Me.lblClockIn.Size = New System.Drawing.Size(440, 24)
        Me.lblClockIn.TabIndex = 9
        Me.lblClockIn.Text = "Clock In"
        Me.lblClockIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnApplyClockIn
        '
        Me.btnApplyClockIn.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnApplyClockIn.Enabled = False
        Me.btnApplyClockIn.Location = New System.Drawing.Point(112, 312)
        Me.btnApplyClockIn.Name = "btnApplyClockIn"
        Me.btnApplyClockIn.Size = New System.Drawing.Size(104, 40)
        Me.btnApplyClockIn.TabIndex = 7
        Me.btnApplyClockIn.Text = "Apply"
        '
        'ClockInUserControl
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.Panel1)
        Me.ForeColor = System.Drawing.Color.White
        Me.Name = "ClockInUserControl"
        Me.Size = New System.Drawing.Size(456, 376)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ClockIn_Entered(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumberPadSmallClockIn.NumberEntered

        Dim loginEnter As String

        loginEnter = Me.NumberPadSmallClockIn.NumberString

        TestClockInFirstStep(loginEnter)

    End Sub

    Friend Function StartClockIn(ByVal loginEnter As String)
        Dim doesNotneedToClockIn As Boolean = False

        If Not loginEnter Is Nothing Then
            If loginEnter.Length > 0 Then
                NumberPadSmallClockIn.NumberString = loginEnter
                NumberPadSmallClockIn.ShowNumberString()
                doesNotneedToClockIn = TestClockInFirstStep(loginEnter)
            End If
        End If

        Return doesNotneedToClockIn

    End Function

    Private Function TestClockInFirstStep(ByVal loginEnter As String)
        Dim doesNotneedToClockIn As Boolean = False
        '   Dim loginMatch As Boolean

        If loginEnter.Length = 8 Then
            Me.pnlJobCodes.Controls.Clear()
            doesNotneedToClockIn = TestClockIn(loginEnter)
        Else
            MsgBox("Please Combine Your EmployeeID and Password then Press Enter")
            If Not loginEnter.Length = 4 Then
                Me.NumberPadSmallClockIn.ResetValues()
            End If
        End If

        Return doesNotneedToClockIn

    End Function

    Private Function TestClockIn(ByVal loginEnter As String)
        Dim empID As Integer
        Dim passcode As Integer
        Dim sqlEmpID As Integer
        Dim sqlPasscode As Integer
        Dim empName As String
        Dim empInSystem As Boolean = False
        '    Dim doesNotneedToClockIn As Boolean = False

        empID = CInt(loginEnter.ToString.Substring(0, 4))
        passcode = CInt(loginEnter.ToString.Substring(4, 4))

        '      Dim emp As Employee
        Dim isClockedIn As Integer

        If loginEnter.Length < 8 Then
            MsgBox("Enter both EmployeeID as Passcode")
            Exit Function
        End If

        For Each emp In AllEmployees
            If emp.EmployeeNumber = empID Then
                If emp.PasscodeID = loginEnter.ToString.Substring(4, 4) Then
                    empInSystem = True
                    Exit For
                Else
                    MsgBox("Password Incorrect: Please Reenter or See Manager")
                    Exit Function
                End If
            End If
        Next

        If empInSystem = False Then 'emp Is Nothing Then
            MsgBox("Employee Number: " & empID & " Is Not In System")
            Exit Function
        End If

        Try
            isClockedIn = ActuallyLogIn(emp)
        Catch ex As Exception
            CloseConnection()
            Exit Function
        End Try

        If currentClockEmp Is Nothing Then
            currentClockEmp = New Employee
        End If
        currentClockEmp = emp

        If isClockedIn = 0 Then

        ElseIf isClockedIn = 1 Then
            MsgBox(emp.FullName & " is currently logged-in as " & emp.JobCodeName)
            Return True
            '         Exit Function
        Else
            MsgBox("Employee Is Clocked in more than once. Please See Manager.")
            Return True
        End If

        If emp.ClockInReq = False Then
            MsgBox(emp.FullName & " does not need to Clock In.")
            Return True
        End If

        DisplayJobNames(emp)

    End Function

    Private Sub DisplayJobNames(ByRef emp As Employee)  '(ByVal empID As Integer, ByVal passcodeID As Integer)

        Dim x As Integer = buttonSpace
        Dim y As Integer = buttonSpace
        Dim h As Integer
        Dim numberOfJobCodes As Integer

        Me.lblClockIn.Text = emp.FullName

        h = (Me.pnlJobCodes.Height - (8 * buttonSpace)) / 7
      
        If Not emp.JobCode1 = Nothing Then
            btnJobName(0) = New KitchenButton(emp.JobName1, Me.pnlJobCodes.Width - (2 * buttonSpace), h, c7, c2)
            With btnJobName(0)
                .Location = New Point(x, y)
                .ID = emp.JobCode1
                .PayRate = emp.JobRate1
                .ForeColor = c3
                Me.pnlJobCodes.Controls.Add(btnJobName(0))
                AddHandler btnJobName(0).Click, AddressOf pnlJobCodes_Click
            End With
            numberOfJobCodes += 1
            If numberOfJobCodes = 1 Then
                MakeThisJobFunctionActive(btnJobName(0), False)
            End If
        End If
        y += h + buttonSpace

        If Not emp.JobCode2 = Nothing Then
            btnJobName(1) = New KitchenButton(emp.JobName2, Me.pnlJobCodes.Width - (2 * buttonSpace), h, c7, c2)
            With btnJobName(1)
                .Location = New Point(x, y)
                .ID = emp.JobCode2
                .PayRate = emp.JobRate2
                .ForeColor = c3
                Me.pnlJobCodes.Controls.Add(btnJobName(1))
                AddHandler btnJobName(1).Click, AddressOf pnlJobCodes_Click
            End With
            numberOfJobCodes += 1
            If numberOfJobCodes = 1 Then
                MakeThisJobFunctionActive(btnJobName(1), False)
            End If
        End If
        y += h + buttonSpace

        If Not emp.JobCode3 = Nothing Then
            btnJobName(2) = New KitchenButton(emp.JobName3, Me.pnlJobCodes.Width - (2 * buttonSpace), h, c7, c2)
            With btnJobName(2)
                .Location = New Point(x, y)
                .ID = emp.JobCode3
                .PayRate = emp.JobRate3
                .ForeColor = c3
                Me.pnlJobCodes.Controls.Add(btnJobName(2))
                AddHandler btnJobName(2).Click, AddressOf pnlJobCodes_Click
            End With
            numberOfJobCodes += 1
            If numberOfJobCodes = 1 Then
                MakeThisJobFunctionActive(btnJobName(2), False)
            End If
        End If
        y += h + buttonSpace

        If Not emp.JobCode4 = Nothing Then
            btnJobName(3) = New KitchenButton(emp.JobName4, Me.pnlJobCodes.Width - (2 * buttonSpace), h, c7, c2)
            With btnJobName(3)
                .Location = New Point(x, y)
                .ID = emp.JobCode4
                .PayRate = emp.JobRate4
                .ForeColor = c3
                Me.pnlJobCodes.Controls.Add(btnJobName(3))
                AddHandler btnJobName(3).Click, AddressOf pnlJobCodes_Click
            End With
            numberOfJobCodes += 1
            If numberOfJobCodes = 1 Then
                MakeThisJobFunctionActive(btnJobName(3), False)
            End If
        End If
        y += h + buttonSpace

        If Not emp.JobCode5 = Nothing Then
            btnJobName(4) = New KitchenButton(emp.JobName5, Me.pnlJobCodes.Width - (2 * buttonSpace), h, c7, c2)
            With btnJobName(4)
                .Location = New Point(x, y)
                .ID = emp.JobCode5
                .PayRate = emp.JobRate5
                .ForeColor = c3
                Me.pnlJobCodes.Controls.Add(btnJobName(4))
                AddHandler btnJobName(4).Click, AddressOf pnlJobCodes_Click
            End With
            numberOfJobCodes += 1
            If numberOfJobCodes = 1 Then
                MakeThisJobFunctionActive(btnJobName(4), False)
            End If
        End If
        y += h + buttonSpace

        If Not emp.JobCode6 = Nothing Then
            btnJobName(5) = New KitchenButton(emp.JobName6, Me.pnlJobCodes.Width - (2 * buttonSpace), h, c7, c2)
            With btnJobName(5)
                .Location = New Point(x, y)
                .ID = emp.JobCode6
                .PayRate = emp.JobRate6
                .ForeColor = c3
                Me.pnlJobCodes.Controls.Add(btnJobName(5))
                AddHandler btnJobName(5).Click, AddressOf pnlJobCodes_Click
            End With
            numberOfJobCodes += 1
            If numberOfJobCodes = 1 Then
                MakeThisJobFunctionActive(btnJobName(5), False)
            End If
        End If
        y += h + buttonSpace

        If Not emp.JobCode7 = Nothing Then
            btnJobName(6) = New KitchenButton(emp.JobName7, Me.pnlJobCodes.Width - (2 * buttonSpace), h, c7, c2)
            With btnJobName(6)
                .Location = New Point(x, y)
                .ID = emp.JobCode7
                .PayRate = emp.JobRate7
                .ForeColor = c3
                Me.pnlJobCodes.Controls.Add(btnJobName(6))
                AddHandler btnJobName(6).Click, AddressOf pnlJobCodes_Click
            End With
            numberOfJobCodes += 1
            If numberOfJobCodes = 1 Then
                MakeThisJobFunctionActive(btnJobName(6), False)
            End If
        End If

        If numberOfJobCodes = 1 Then
            ' we need to check if they are floor personel
            'then check how many floor plans
            'then Apply ClockIn

            ApplyClockIn()

        End If

    End Sub

    Private Sub pnlJobCodes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim btnKitchen = New KitchenButton("ForTestOnly", 0, 0, c3, c2)
        Dim objButton As KitchenButton

        If Not sender.GetType Is btnKitchen.GetType Then Exit Sub

        objButton = CType(sender, KitchenButton)

        MakeThisJobFunctionActive(objButton, True)

    End Sub

    Private Sub MakeThisJobFunctionActive(ByVal objButton As KitchenButton, ByVal resettingColor As Boolean)

        If resettingColor = True Then
            ResetJobButtonColors()
        End If

        emp.JobCodeID = objButton.ID
        emp.RegPayRate = objButton.PayRate
        emp.OTPayRate = Math.Round(emp.RegPayRate * companyInfo.overtimeRate, 2)

        objButton.BackColor = c9    'Color.Red
        Me.btnApplyClockIn.Enabled = True
        Me.btnApplyClockIn.BackColor = c9   'Color.Red
    End Sub

    Private Sub ResetJobButtonColors()

        If Not btnJobName(0) Is Nothing Then
            btnJobName(0).BackColor = c7
        End If
        If Not btnJobName(1) Is Nothing Then
            btnJobName(1).BackColor = c7
        End If
        If Not btnJobName(2) Is Nothing Then
            btnJobName(2).BackColor = c7
        End If
        If Not btnJobName(3) Is Nothing Then
            btnJobName(3).BackColor = c7
        End If
        If Not btnJobName(4) Is Nothing Then
            btnJobName(4).BackColor = c7
        End If
        If Not btnJobName(5) Is Nothing Then
            btnJobName(5).BackColor = c7
        End If
        If Not btnJobName(6) Is Nothing Then
            btnJobName(6).BackColor = c7
        End If
      
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        RaiseEvent ClosingClockIn(sender, e)
        Me.Dispose()

    End Sub

    Private Sub btnApplyClockIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyClockIn.Click

        ' at startup we are allowing clockIn even without connection to Phoenix
        'this will place a 0 in terminalPrimary Key

        ApplyClockIn()

    End Sub

    Private Sub ApplyClockIn()
        ' at startup we are allowing clockIn even without connection to Phoenix
        'this will place a 0 in terminalPrimary Key

        ' could / should add enter of LastFloorPlan in emp.LastFloorPlan
        'this can change every time they leave floor plan

        GenerateOrderTables.EnterEmployeeToLoginDatabase(emp)
        GenerateOrderTables.FillJobCodeInfo(emp, emp.JobCodeID)
        RaiseEvent ApplyClockInCheck() 'ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Dispose()

    End Sub


End Class
