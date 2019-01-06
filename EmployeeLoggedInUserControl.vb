Public Class EmployeeLoggedInUserControl
    Inherits System.Windows.Forms.UserControl

    '   Dim sql As New DataSet_Builder.SQLHelper(connectserver)

    Dim empRowNumber As Integer
    Dim empColNumber As Integer
    Dim isByEmployee As Boolean
    Dim employeeRemovingList As New ArrayList
    Dim employeeDataview As DataView

    Dim logoutErased As Boolean

    Friend WithEvents NumberPadEmployee2 As DataSet_Builder.NumberPadMedium

    Event ClosedEmployeeLog(ByVal sender As Object, ByVal e As System.EventArgs)


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal byEmp As Boolean)
        MyBase.New()

        isByEmployee = byEmp

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnApplySplit As System.Windows.Forms.Button
    Friend WithEvents NumberPadEmployee1 As DataSet_Builder.NumberPadEmployee
    Friend WithEvents grdEmpoyeeLoggedIn As System.Windows.Forms.DataGrid
    Friend WithEvents lblEmployeeLoggedIn As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnEraseLogout As System.Windows.Forms.Button
    Friend WithEvents lblNumberClockedIn As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnEraseLogout = New System.Windows.Forms.Button
        Me.grdEmpoyeeLoggedIn = New System.Windows.Forms.DataGrid
        Me.NumberPadEmployee1 = New DataSet_Builder.NumberPadEmployee
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnApplySplit = New System.Windows.Forms.Button
        Me.lblEmployeeLoggedIn = New System.Windows.Forms.Label
        Me.lblNumberClockedIn = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.grdEmpoyeeLoggedIn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.RoyalBlue
        Me.Panel1.Controls.Add(Me.btnEraseLogout)
        Me.Panel1.Controls.Add(Me.grdEmpoyeeLoggedIn)
        Me.Panel1.Controls.Add(Me.NumberPadEmployee1)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.btnApplySplit)
        Me.Panel1.Location = New System.Drawing.Point(8, 48)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(776, 360)
        Me.Panel1.TabIndex = 0
        '
        'btnEraseLogout
        '
        Me.btnEraseLogout.Location = New System.Drawing.Point(48, 296)
        Me.btnEraseLogout.Name = "btnEraseLogout"
        Me.btnEraseLogout.Size = New System.Drawing.Size(96, 40)
        Me.btnEraseLogout.TabIndex = 14
        Me.btnEraseLogout.Text = "Erase Logout"
        '
        'grdEmpoyeeLoggedIn
        '
        Me.grdEmpoyeeLoggedIn.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grdEmpoyeeLoggedIn.CaptionVisible = False
        Me.grdEmpoyeeLoggedIn.DataMember = ""
        Me.grdEmpoyeeLoggedIn.Font = New System.Drawing.Font("Bookman Old Style", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdEmpoyeeLoggedIn.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdEmpoyeeLoggedIn.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdEmpoyeeLoggedIn.Location = New System.Drawing.Point(8, 8)
        Me.grdEmpoyeeLoggedIn.Name = "grdEmpoyeeLoggedIn"
        Me.grdEmpoyeeLoggedIn.PreferredRowHeight = 30
        Me.grdEmpoyeeLoggedIn.ReadOnly = True
        Me.grdEmpoyeeLoggedIn.RowHeadersVisible = False
        Me.grdEmpoyeeLoggedIn.Size = New System.Drawing.Size(560, 272)
        Me.grdEmpoyeeLoggedIn.TabIndex = 13
        '
        'NumberPadEmployee1
        '
        Me.NumberPadEmployee1.AM_PMEnter = Nothing
        Me.NumberPadEmployee1.BackColor = System.Drawing.Color.SlateGray
        Me.NumberPadEmployee1.DayEnter = 0
        Me.NumberPadEmployee1.HourEnter = 0
        Me.NumberPadEmployee1.Location = New System.Drawing.Point(576, 8)
        Me.NumberPadEmployee1.MinuteEnter = 0
        Me.NumberPadEmployee1.MonthEnter = 0
        Me.NumberPadEmployee1.Name = "NumberPadEmployee1"
        Me.NumberPadEmployee1.Size = New System.Drawing.Size(192, 344)
        Me.NumberPadEmployee1.TabIndex = 12
        Me.NumberPadEmployee1.YearEnter = 0
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(288, 296)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 48)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "Cancel"
        '
        'btnApplySplit
        '
        Me.btnApplySplit.BackColor = System.Drawing.Color.Red
        Me.btnApplySplit.Location = New System.Drawing.Point(408, 296)
        Me.btnApplySplit.Name = "btnApplySplit"
        Me.btnApplySplit.Size = New System.Drawing.Size(128, 48)
        Me.btnApplySplit.TabIndex = 8
        Me.btnApplySplit.Text = "Apply"
        '
        'lblEmployeeLoggedIn
        '
        Me.lblEmployeeLoggedIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeLoggedIn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblEmployeeLoggedIn.Location = New System.Drawing.Point(136, 16)
        Me.lblEmployeeLoggedIn.Name = "lblEmployeeLoggedIn"
        Me.lblEmployeeLoggedIn.Size = New System.Drawing.Size(400, 24)
        Me.lblEmployeeLoggedIn.TabIndex = 1
        Me.lblEmployeeLoggedIn.Text = "Current Employees Clocked-In SpiderPOS:"
        Me.lblEmployeeLoggedIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNumberClockedIn
        '
        Me.lblNumberClockedIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumberClockedIn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblNumberClockedIn.Location = New System.Drawing.Point(544, 16)
        Me.lblNumberClockedIn.Name = "lblNumberClockedIn"
        Me.lblNumberClockedIn.Size = New System.Drawing.Size(86, 24)
        Me.lblNumberClockedIn.TabIndex = 2
        Me.lblNumberClockedIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'EmployeeLoggedInUserControl
        '
        Me.BackColor = System.Drawing.Color.SlateGray
        Me.Controls.Add(Me.lblNumberClockedIn)
        Me.Controls.Add(Me.lblEmployeeLoggedIn)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "EmployeeLoggedInUserControl"
        Me.Size = New System.Drawing.Size(792, 416)
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdEmpoyeeLoggedIn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub InitializeOther()

        CreateEmpDataview()
        BindEmployeeGrid()

        If isByEmployee = True Then
            If empActive Is Nothing Then
                Me.lblEmployeeLoggedIn.Text = currentServer.FullName
            Else
                Me.lblEmployeeLoggedIn.Text = empActive.FullName
            End If
            btnEraseLogout.Visible = False
        Else
            Me.lblNumberClockedIn.Text = employeeDataview.Count.ToString
        End If

        NumberPadEmployee2 = New DataSet_Builder.NumberPadMedium
        Me.NumberPadEmployee2.DecimalUsed = True
        Me.NumberPadEmployee2.Location = New Point(648, 8)
        Me.NumberPadEmployee2.Visible = False
        Me.Panel1.Controls.Add(Me.NumberPadEmployee2)

    End Sub

    Private Sub CreateEmpDataview()

        employeeDataview = New DataView

        With employeeDataview
            .Table = dsEmployee.Tables("LoggedInEmployees")
            '         .RowFilter = "ClockInReq = 1"
        End With

    End Sub
    Private Sub BindEmployeeGrid()

        '*** can pull a dataview here with non salaried employees (or no clock in)
        Me.grdEmpoyeeLoggedIn.DataSource = dsEmployee.Tables("LoggedInEmployees")
        '        employeeDataview() '

        Dim tsEmployeeLoggedIn As New DataGridTableStyle
        tsEmployeeLoggedIn.MappingName = "LoggedInEmployees"
        tsEmployeeLoggedIn.RowHeadersVisible = False
        tsEmployeeLoggedIn.AllowSorting = False
        tsEmployeeLoggedIn.GridLineColor = Color.White

        Dim csEmpNum As New DataGridTextBoxColumn
        csEmpNum.MappingName = "EmployeeID"
        If isByEmployee = True Then
            csEmpNum.Width = 0
        Else
            csEmpNum.Width = 0
        End If

        Dim csEmpFirst As New DataGridTextBoxColumn
        csEmpFirst.MappingName = "FirstName"
        If isByEmployee = True Then
            csEmpFirst.Width = 0
        Else
            csEmpFirst.Width = 68
        End If

        '        Dim csBlank As New DataGridTextBoxColumn
        '       csBlank.HeaderText = "   "
        '      If isByEmployee = True Then
        '         csBlank.Width = 0
        '    Else
        '       csBlank.Width = 20
        '  End If

        Dim csEmpLast As New DataGridTextBoxColumn
        csEmpLast.MappingName = "LastName"
        If isByEmployee = True Then
            csEmpLast.Width = 0
        Else
            csEmpLast.Width = 128
        End If

        Dim csLogIn As New DataGridTextBoxColumn
        csLogIn.MappingName = "LogInTime"
        csLogIn.Width = 175
        csLogIn.HeaderText = "          Login"

        Dim csLogOut As New DataGridTextBoxColumn
        csLogOut.MappingName = "LogOutTime"
        csLogOut.Width = 175
        csLogOut.HeaderText = "          Logout"
        csLogOut.NullText = ""

        Dim csCharges As New DataGridTextBoxColumn
        csCharges.MappingName = "TipableSales"
        If isByEmployee = True Then
            csCharges.Width = 65
            csCharges.HeaderText = "  Sales"
        Else
            csCharges.Width = 0
        End If
        csCharges.NullText = ""

        Dim csChargeTips As New DataGridTextBoxColumn
        csChargeTips.MappingName = "ChargedTips"
        If isByEmployee = True Then
            csChargeTips.Width = 63
            csChargeTips.HeaderText = "Chg Tips"
        Else
            csChargeTips.Width = 0
        End If
        csChargeTips.NullText = ""

        Dim csTips As New DataGridTextBoxColumn
        csTips.MappingName = "DeclaredTips"
        If isByEmployee = True Then
            csTips.Width = 63
            csTips.HeaderText = "   Tips"
        Else
            csTips.Width = 0
        End If
        csTips.NullText = ""

        '      Dim csTerminals As New DataGridTextBoxColumn
        '     csTerminals.MappingName = "TerminalsGroupID"
        '    csTerminals.Width = 0
       

        tsEmployeeLoggedIn.GridColumnStyles.Add(csEmpNum)
        tsEmployeeLoggedIn.GridColumnStyles.Add(csEmpFirst)
        '     tsEmployeeLoggedIn.GridColumnStyles.Add(csBlank)
        tsEmployeeLoggedIn.GridColumnStyles.Add(csEmpLast)
        tsEmployeeLoggedIn.GridColumnStyles.Add(csLogIn)
        tsEmployeeLoggedIn.GridColumnStyles.Add(csLogOut)
        tsEmployeeLoggedIn.GridColumnStyles.Add(csCharges)
        tsEmployeeLoggedIn.GridColumnStyles.Add(csChargeTips)
        tsEmployeeLoggedIn.GridColumnStyles.Add(csTips)
        '        tsEmployeeLoggedIn.GridColumnStyles.Add(csTerminals)

        Me.grdEmpoyeeLoggedIn.TableStyles.Add(tsEmployeeLoggedIn)

    End Sub

    Private Sub grdEmpoyeeLoggedIn_Slected(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdEmpoyeeLoggedIn.CurrentCellChanged
        Dim currentMonth As Integer
        Dim currentDay As Integer
        Dim currentYear As Integer
        Dim currentHour As Integer
        Dim currentMinute As Integer
        Dim currentAMPM As String

        If logoutErased = False Then
            NewClockInEntered_Click(sender, e)
        Else
            logoutErased = False
        End If

        empRowNumber = Me.grdEmpoyeeLoggedIn.CurrentCell.RowNumber
        empColNumber = Me.grdEmpoyeeLoggedIn.CurrentCell.ColumnNumber

        If empColNumber = 3 Then
            '   this is to adjust log in time
            currentMonth = ((Format(Me.grdEmpoyeeLoggedIn(empRowNumber, 3), "MM,dd,yyyy")).ToString.Substring(0, 2))
            currentDay = ((Format(Me.grdEmpoyeeLoggedIn(empRowNumber, 3), "MM,dd,yyyy")).ToString.Substring(3, 2))
            currentYear = ((Format(Me.grdEmpoyeeLoggedIn(empRowNumber, 3), "MM,dd,yyyy")).ToString.Substring(6, 4))
            currentHour = ((Format(Me.grdEmpoyeeLoggedIn(empRowNumber, 3), "hh,mm,tt")).ToString.Substring(0, 2))
            currentMinute = ((Format(Me.grdEmpoyeeLoggedIn(empRowNumber, 3), "hh,mm,tt")).ToString.Substring(3, 2))
            currentAMPM = ((Format(Me.grdEmpoyeeLoggedIn(empRowNumber, 3), "hh,mm,tt")).ToString.Substring(6, 2))


        ElseIf empColNumber = 4 Then
            '   this is for log out time
            If Me.grdEmpoyeeLoggedIn(empRowNumber, 4) Is DBNull.Value Then

                currentMonth = (Format((Now), "MM,dd,yyyy").ToString.Substring(0, 2))
                currentDay = (Format((Now), "MM,dd,yyyy").ToString.Substring(3, 2))
                currentYear = (Format((Now), "MM,dd,yyyy").ToString.Substring(6, 4))
                currentHour = (Format((Now), "hh,mm,tt").ToString.Substring(0, 2)) '(11, 2))
                currentMinute = (Format((Now), "hh,mm,tt").ToString.Substring(3, 2)) '(14, 2))
                currentAMPM = (Format((Now), "hh,mm,tt").ToString.Substring(6, 2)) '(20, 2))

            Else
                currentMonth = ((Format(Me.grdEmpoyeeLoggedIn(empRowNumber, 4), "MM,dd,yyyy")).ToString.Substring(0, 2))
                currentDay = ((Format(Me.grdEmpoyeeLoggedIn(empRowNumber, 4), "MM,dd,yyyy")).ToString.Substring(3, 2))
                currentYear = ((Format(Me.grdEmpoyeeLoggedIn(empRowNumber, 4), "MM,dd,yyyy")).ToString.Substring(6, 4))
                currentHour = ((Format(Me.grdEmpoyeeLoggedIn(empRowNumber, 4), "hh,mm,tt")).ToString.Substring(0, 2))
                currentMinute = ((Format(Me.grdEmpoyeeLoggedIn(empRowNumber, 4), "hh,mm,tt")).ToString.Substring(3, 2))
                currentAMPM = ((Format(Me.grdEmpoyeeLoggedIn(empRowNumber, 4), "hh,mm,tt")).ToString.Substring(6, 2))

            End If

        ElseIf empColNumber = 6 Then
            If Me.grdEmpoyeeLoggedIn(empRowNumber, 6) Is DBNull.Value Then
                Me.NumberPadEmployee2.NumberTotal = 0
            Else
                Me.NumberPadEmployee2.NumberTotal = (Me.grdEmpoyeeLoggedIn(empRowNumber, 6))
            End If

            '      Me.NumberPadEmployee2.ShowNumberString()

        ElseIf empColNumber = 7 Then
            If Me.grdEmpoyeeLoggedIn(empRowNumber, 7) Is DBNull.Value Then
                Me.NumberPadEmployee2.NumberTotal = 0
            Else
                Me.NumberPadEmployee2.NumberTotal = (Me.grdEmpoyeeLoggedIn(empRowNumber, 7))
            End If

            '     Me.NumberPadEmployee2.ShowNumberString()

        End If


        If empColNumber = 3 Or empColNumber = 4 Then
            Me.NumberPadEmployee1.Visible = True
            Me.NumberPadEmployee2.Visible = False

            Me.NumberPadEmployee1.MonthEnter = currentMonth
            Me.NumberPadEmployee1.DayEnter = currentDay
            Me.NumberPadEmployee1.YearEnter = currentYear
            Me.NumberPadEmployee1.HourEnter = currentHour
            Me.NumberPadEmployee1.MinuteEnter = currentMinute
            Me.NumberPadEmployee1.AM_PMEnter = currentAMPM

            Me.NumberPadEmployee1.ShowNumberTotal()     ' string???
            Me.NumberPadEmployee1.Focus()

            If empColNumber = 4 Then
                NewClockInEntered_Click(sender, e)
            End If

        ElseIf empColNumber = 6 Or empColNumber = 7 Then
            Me.NumberPadEmployee1.Visible = False
            Me.NumberPadEmployee2.Visible = True


            '         Me.NumberPadEmployee2.NumberString = ""
            Me.NumberPadEmployee2.ShowNumberString()
            Me.NumberPadEmployee2.IntegerNumber = 0
            Me.NumberPadEmployee2.Focus()

        End If

    End Sub


    Private Sub NewClockInEntered_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumberPadEmployee1.NumberEntered 'btnApplySplit.Click
        NumberPadEmployee1.ReflectChanges()

        Dim oRow As DataRow
        Dim dateStr As System.DateTime  'System.Data.SqlDbType.datetime
        Dim newTipAmount As Decimal
        Dim info As New System.Globalization.CultureInfo("en-US", False)
        Dim cal As System.Globalization.Calendar
        cal = info.Calendar
        Dim newHour As Integer

        If Me.NumberPadEmployee1.AM_PMEnter = "PM" Then
            newHour = Me.NumberPadEmployee1.HourEnter + 12
            If newHour = 24 Then newHour = 12
        Else
            newHour = Me.NumberPadEmployee1.HourEnter
            '   if 12:30 AM we need to reflect 00:30
            If newHour = 12 Then newHour = 0
        End If

        '  ****  should have some kind of check for abnormal logout times (anything over 24 hours)

        If empColNumber = 3 Then
            dateStr = New System.DateTime(Me.NumberPadEmployee1.YearEnter, Me.NumberPadEmployee1.MonthEnter, Me.NumberPadEmployee1.DayEnter, newHour, Me.NumberPadEmployee1.MinuteEnter, 0, 15, cal)
            dsEmployee.Tables("LoggedInEmployees").Rows(empRowNumber)("LogInTime") = dateStr
        ElseIf empColNumber = 4 Then
            dateStr = New System.DateTime(Me.NumberPadEmployee1.YearEnter, Me.NumberPadEmployee1.MonthEnter, Me.NumberPadEmployee1.DayEnter, newHour, Me.NumberPadEmployee1.MinuteEnter, 0, 15, cal)
            '      MsgBox(dateStr)
            '     MsgBox(Now)

            '    ***********
            '   if empcolnumber = 4 and currentinfo IS NULL (emp was not logged out)
            '   we must determine which tables are being held by employee before we log them out


            dsEmployee.Tables("LoggedInEmployees").Rows(empRowNumber)("LogOutTime") = dateStr
            employeeRemovingList.Add(Me.grdEmpoyeeLoggedIn(empRowNumber, 0))

        ElseIf empColNumber = 6 Then
            newTipAmount = Me.NumberPadEmployee2.NumberTotal 'Format(Me.NumberPadEmployee2.NumberTotal, "#,###.00")
            dsEmployee.Tables("LoggedInEmployees").Rows(empRowNumber)("ChargedTips") = newTipAmount

        ElseIf empColNumber = 7 Then
            newTipAmount = Me.NumberPadEmployee2.NumberTotal 'Format(Me.NumberPadEmployee2.NumberTotal, "#,###.00")
            dsEmployee.Tables("LoggedInEmployees").Rows(empRowNumber)("DeclaredTips") = newTipAmount
        End If

    End Sub

    Private Sub NumberPad_EnterClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumberPadEmployee2.NumberEntered

        NewClockInEntered_Click(sender, e)

    End Sub

    Private Sub btnApplySplit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplySplit.Click

        Dim i As Integer
        If logoutErased = False Then
            NewClockInEntered_Click(sender, e)
        End If

        If typeProgram = "Online_Demo" Then
            MsgBox("Demo does not allow. Licensed program will allow you to clock last night's employees out.")

            dsEmployee.Tables("LoggedInEmployees").RejectChanges()
            RaiseEvent ClosedEmployeeLog(sender, e)
            Me.Dispose()
            Exit Sub

            Dim oRow As DataRow
            Dim empID As Integer

            empID = Me.grdEmpoyeeLoggedIn(empRowNumber, 0)
            For Each oRow In dsEmployee.Tables("LoggedInEmployees").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If empID = oRow("EmployeeID") Then
                        oRow.Delete()
                        Me.Dispose()
                        Exit Sub
                    End If
                End If
            Next
            Me.Dispose()
            Exit Sub
        End If

            Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            If isByEmployee = False Then

                sql.SqlClockedInList.Update(dsEmployee.Tables("LoggedInEmployees"))
            Else

                sql.SqlCLockedInByEmp.Update(dsEmployee.Tables("LoggedInEmployees"))
            End If
            sql.cn.Close()

            dsEmployee.Tables("LoggedInEmployees").AcceptChanges()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
            If Err.Number = "5" Then
                ServerJustWentDown()
            End If
            '   we might want to put something here to keep track of clock outs when down
        End Try
     
        RaiseEvent ClosedEmployeeLog(sender, e)
        Me.Dispose()

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        dsEmployee.Tables("LoggedInEmployees").RejectChanges()

        RaiseEvent ClosedEmployeeLog(sender, e)
        Me.Dispose()

    End Sub


    Private Sub btnEraseLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEraseLogout.Click

        If Not dsEmployee.Tables("LoggedInEmployees").Rows(empRowNumber)("LogOutTime") Is DBNull.Value Then
            dsEmployee.Tables("LoggedInEmployees").Rows(empRowNumber)("LogOutTime") = DBNull.Value
            employeeRemovingList.Remove(Me.grdEmpoyeeLoggedIn(empRowNumber, 0))
            logoutErased = True
        End If

    End Sub
End Class
