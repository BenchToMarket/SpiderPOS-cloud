Imports DataSet_Builder

Public Class Seating_ChooseTable
    Inherits System.Windows.Forms.UserControl

    Dim WithEvents floorPlan1 As Seating_FloorPlan
    Dim WithEvents floorPlan2 As Seating_FloorPlan
    Dim WithEvents floorPlan3 As Seating_FloorPlan
    Dim WithEvents floorPlan4 As Seating_FloorPlan
    Dim WithEvents floorPlan5 As Seating_FloorPlan

    Dim FloorPlanFirst As Integer
    Dim FloorPlanSecond As Integer
    Dim FloorPlanThird As Integer
    Dim FloorPlanForth As Integer
    Dim FloorPlanFifth As Integer

    Dim currentFloorPlan As floorPlanEnum
    Dim _startedFromManager As Boolean


    '    Dim WithEvents seatingChartCesar As Seating_Dining_Cesar
    '   Dim WithEvents seatingChart As Seating_Dining
    '  Dim WithEvents seatingChart2 As Seating_Dining2
    Dim WithEvents seatingCode As Seating_ColorCode

    Dim WithEvents SeatingOverrideInfo As DataSet_Builder.Information_UC
    Dim WithEvents numCustPad As DataSet_Builder.NumberOfCustomers_UC
    Dim WithEvents btnClose2 As Button

    Dim currentSeatingView As Integer
    Dim changesMade As Boolean
    Dim changingAvail As Boolean
    Dim _overrideAvail As Boolean


    Private _tableSelected As Integer
    Private _numberCustomers As Integer

    'true if we started this from ManagerForm
    Friend Property StartedFromManager() As Boolean
        Get
            Return _startedFromManager
        End Get
        Set(ByVal Value As Boolean)
            _startedFromManager = Value
        End Set
    End Property


    Friend Property TableSelected() As Integer
        Get
            Return _tableSelected
        End Get
        Set(ByVal Value As Integer)
            _tableSelected = Value
        End Set
    End Property

    Friend Property NumberCustomers() As Integer
        Get
            Return _numberCustomers
        End Get
        Set(ByVal Value As Integer)
            _numberCustomers = Value
        End Set
    End Property

    Friend Property OverrideAvail() As Boolean
        Get
            Return _overrideAvail
        End Get
        Set(ByVal Value As Boolean)
            _overrideAvail = Value
        End Set
    End Property


    Event TableSelectedEventSecond(ByVal sender As Object, ByVal e As System.EventArgs)
    Event NumberCustomerEvent()
    Event NoTableSelected(ByVal sender As Object, ByVal e As System.EventArgs)


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        currentSeatingView = 1

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
    Friend WithEvents pnlSeating As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSeatingPrevious As System.Windows.Forms.Button
    Friend WithEvents btnSeatingNext As System.Windows.Forms.Button
    Friend WithEvents btnSeatingCode As System.Windows.Forms.Button
    Friend WithEvents btnChangeAvail As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlSeating = New System.Windows.Forms.Panel
        Me.btnChangeAvail = New System.Windows.Forms.Button
        Me.btnSeatingCode = New System.Windows.Forms.Button
        Me.btnSeatingPrevious = New System.Windows.Forms.Button
        Me.btnSeatingNext = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.pnlSeating.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSeating
        '
        Me.pnlSeating.BackColor = System.Drawing.Color.SlateGray
        Me.pnlSeating.Controls.Add(Me.btnChangeAvail)
        Me.pnlSeating.Controls.Add(Me.btnSeatingCode)
        Me.pnlSeating.Controls.Add(Me.btnSeatingPrevious)
        Me.pnlSeating.Controls.Add(Me.btnSeatingNext)
        Me.pnlSeating.Controls.Add(Me.btnClose)
        Me.pnlSeating.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSeating.Location = New System.Drawing.Point(0, 696)
        Me.pnlSeating.Name = "pnlSeating"
        Me.pnlSeating.Size = New System.Drawing.Size(1024, 72)
        Me.pnlSeating.TabIndex = 0
        '
        'btnChangeAvail
        '
        Me.btnChangeAvail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnChangeAvail.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnChangeAvail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangeAvail.Location = New System.Drawing.Point(320, 8)
        Me.btnChangeAvail.Name = "btnChangeAvail"
        Me.btnChangeAvail.Size = New System.Drawing.Size(96, 56)
        Me.btnChangeAvail.TabIndex = 4
        Me.btnChangeAvail.Text = "Change Avail"
        '
        'btnSeatingCode
        '
        Me.btnSeatingCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSeatingCode.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnSeatingCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSeatingCode.Location = New System.Drawing.Point(184, 8)
        Me.btnSeatingCode.Name = "btnSeatingCode"
        Me.btnSeatingCode.Size = New System.Drawing.Size(96, 56)
        Me.btnSeatingCode.TabIndex = 3
        Me.btnSeatingCode.Text = "Color Code"
        '
        'btnSeatingPrevious
        '
        Me.btnSeatingPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSeatingPrevious.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnSeatingPrevious.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSeatingPrevious.Location = New System.Drawing.Point(712, 8)
        Me.btnSeatingPrevious.Name = "btnSeatingPrevious"
        Me.btnSeatingPrevious.Size = New System.Drawing.Size(96, 56)
        Me.btnSeatingPrevious.TabIndex = 2
        Me.btnSeatingPrevious.Text = "Previous"
        '
        'btnSeatingNext
        '
        Me.btnSeatingNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSeatingNext.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnSeatingNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSeatingNext.Location = New System.Drawing.Point(848, 8)
        Me.btnSeatingNext.Name = "btnSeatingNext"
        Me.btnSeatingNext.Size = New System.Drawing.Size(96, 56)
        Me.btnSeatingNext.TabIndex = 1
        Me.btnSeatingNext.Text = "Next"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(48, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(96, 56)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close"
        '
        'Seating_ChooseTable
        '
        Me.BackColor = System.Drawing.Color.LightSlateGray
        Me.Controls.Add(Me.pnlSeating)
        Me.Name = "Seating_ChooseTable"
        Me.Size = New System.Drawing.Size(1024, 768)
        Me.pnlSeating.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther()

        PopulateFloorPlanData()
        DisplayWalls()
        DisplayTables()
        InitiateNumberCustomerPad()
      
    End Sub

    Private Sub InitiateNumberCustomerPad()

        numCustPad = New DataSet_Builder.NumberOfCustomers_UC
        numCustPad.ColorButtonFromStart(0)
        numCustPad.Location = New Point((Me.Width - numCustPad.Width) / 2, (Me.Height - numCustPad.Height) / 2)
        numCustPad.Visible = False
        Me.Controls.Add(numCustPad)
    End Sub

    Private Sub DisposeNumberCustoemrPad() Handles numCustPad.canceled

        '      RaiseEvent NumberCustomerEvent()
        ResetFloorPlan()
        numCustPad.Visible = False
        Me.Visible = True

    End Sub

    Private Sub PopulateFloorPlanData()
        Dim fpRow As DataRow
        Dim fpCount As Integer = 1

        'we need to instatiate all just in case there is a  
        'table assigned to a floor plan that was deleted 
        floorPlan1 = New Seating_FloorPlan
        floorPlan2 = New Seating_FloorPlan
        floorPlan3 = New Seating_FloorPlan
        floorPlan4 = New Seating_FloorPlan
        floorPlan5 = New Seating_FloorPlan

        For Each fpRow In ds.Tables("TermsFloor").Rows
            Select Case fpCount
                Case 1
                    '              floorPlan1.SuspendLayout()
                    Me.floorPlan1.lblFloorPlanName.Text = fpRow("FloorPlanName") & "   "
                    FloorPlanFirst = fpRow("FloorPlanID")


                    floorPlan1.panel1.Size = New Size(fpRow("meWidth"), fpRow("meHeight"))
                    floorPlan1.panel1.Location = New Point((Me.floorPlan1.Width - fpRow("meWidth")) / 2, (Me.floorPlan1.Height - fpRow("meHeight") + 60) / 2)
                    'adding 60 to height b/c of lblFloorPlanName
                    floorPlan1.pnlFloorPlan.Size = New Size(fpRow("meWidth") - 16, fpRow("meHeight") - 16)
                    floorPlan1.pnlFloorPlan.Location = New Point(8, 8)
                    floorPlan1.previousFloorPlan = floorPlan1
                    floorPlan1.nextFloorPlan = floorPlan1
                    currentFloorPlan = 1

                    floorPlan1.Location = New Point(50, 40)
                    Me.Controls.Add(floorPlan1)

                Case 2


                    Me.floorPlan2.lblFloorPlanName.Text = fpRow("FloorPlanName") & "   "
                    FloorPlanSecond = fpRow("FloorPlanID")

                    floorPlan2.panel1.Size = New Size(fpRow("meWidth"), fpRow("meHeight"))
                    floorPlan2.panel1.Location = New Point((Me.floorPlan2.Width - fpRow("meWidth")) / 2, (Me.floorPlan2.Height - fpRow("meHeight") + 60) / 2)
                    floorPlan2.pnlFloorPlan.Size = New Size(fpRow("meWidth") - 16, fpRow("meHeight") - 16)
                    floorPlan2.pnlFloorPlan.Location = New Point(8, 8)
                    floorPlan2.previousFloorPlan = floorPlan1
                    floorPlan2.nextFloorPlan = floorPlan2
                    floorPlan1.nextFloorPlan = floorPlan2   'we must restate the last floorplan

                    floorPlan2.Location = New Point(50, 40)
                    Me.Controls.Add(floorPlan2)
                Case 3


                    Me.floorPlan2.lblFloorPlanName.Text = fpRow("FloorPlanName") & "   "
                    FloorPlanThird = fpRow("FloorPlanID")

                    floorPlan3.panel1.Size = New Size(fpRow("meWidth"), fpRow("meHeight"))
                    floorPlan3.panel1.Location = New Point((Me.floorPlan3.Width - fpRow("meWidth")) / 2, (Me.floorPlan3.Height - fpRow("meHeight") + 60) / 2)
                    floorPlan3.pnlFloorPlan.Size = New Size(fpRow("meWidth") - 16, fpRow("meHeight") - 16)
                    floorPlan3.pnlFloorPlan.Location = New Point(8, 8)
                    floorPlan3.previousFloorPlan = floorPlan2
                    floorPlan3.nextFloorPlan = floorPlan3
                    floorPlan2.nextFloorPlan = floorPlan3   'we must restate the last floorplan

                    floorPlan3.Location = New Point(50, 40)
                    Me.Controls.Add(floorPlan3)
                Case 4


                    Me.floorPlan4.lblFloorPlanName.Text = fpRow("FloorPlanName") & "   "
                    FloorPlanForth = fpRow("FloorPlanID")

                    floorPlan4.panel1.Size = New Size(fpRow("meWidth"), fpRow("meHeight"))
                    floorPlan4.panel1.Location = New Point((Me.floorPlan4.Width - fpRow("meWidth")) / 2, (Me.floorPlan4.Height - fpRow("meHeight") + 60) / 2)
                    floorPlan4.pnlFloorPlan.Size = New Size(fpRow("meWidth") - 16, fpRow("meHeight") - 16)
                    floorPlan4.pnlFloorPlan.Location = New Point(8, 8)
                    floorPlan4.previousFloorPlan = floorPlan3
                    floorPlan4.nextFloorPlan = floorPlan4
                    floorPlan3.nextFloorPlan = floorPlan4   'we must restate the last floorplan

                    floorPlan4.Location = New Point(50, 40)
                    Me.Controls.Add(floorPlan4)
                Case 5


                    Me.floorPlan5.lblFloorPlanName.Text = fpRow("FloorPlanName") & "   "
                    FloorPlanFifth = fpRow("FloorPlanID")

                    floorPlan5.panel1.Size = New Size(fpRow("meWidth"), fpRow("meHeight"))
                    floorPlan5.panel1.Location = New Point((Me.floorPlan5.Width - fpRow("meWidth")) / 2, (Me.floorPlan5.Height - fpRow("meHeight") + 60) / 2)
                    floorPlan5.pnlFloorPlan.Size = New Size(fpRow("meWidth") - 16, fpRow("meHeight") - 16)
                    floorPlan5.pnlFloorPlan.Location = New Point(8, 8)
                    floorPlan5.previousFloorPlan = floorPlan4
                    floorPlan5.nextFloorPlan = floorPlan1
                    floorPlan4.nextFloorPlan = floorPlan5   'we must restate the last floorplan
                    floorPlan1.previousFloorPlan = floorPlan5

                    floorPlan5.Location = New Point(50, 40)
                    Me.Controls.Add(floorPlan5)
            End Select

            fpCount += 1
        Next

    End Sub

    Private Sub DisplayWalls()
        Dim oRow As DataRow
        Dim i As Integer

        For Each oRow In ds.Tables("TermsWalls").Rows
            If oRow("Active") = True Then

                Select Case oRow("FloorPlanID")
                    Case FloorPlanFirst
                        floorPlan1.DisplayEachWall(oRow, i)

                    Case FloorPlanSecond
                        floorPlan2.DisplayEachWall(oRow, i)
                    Case FloorPlanThird
                        floorPlan3.DisplayEachWall(oRow, i)
                    Case FloorPlanForth
                        floorPlan4.DisplayEachWall(oRow, i)
                    Case FloorPlanFifth
                        floorPlan5.DisplayEachWall(oRow, i)
                End Select

            End If
            i += 1
        Next

    End Sub

    Private Sub DisplayTables()
        Dim oRow As DataRow
        Dim i As Integer

        For Each oRow In ds.Tables("TermsTables").Rows
            '444     If oRow("Active") = True Then
            '   we have to check for active here just in case
            '   they make a table inactive in middle of shift
            '   we order by TableOverviewID and adj color by this index (i)
            '   TermsTables and AllTables will have the same index(i)
            '   we also count inactive in this index count
            Select Case oRow("FloorPlanID")
                Case FloorPlanFirst
                    floorPlan1.DisplayEachTable(oRow, i)

                Case FloorPlanSecond
                    floorPlan2.DisplayEachTable(oRow, i)
                Case FloorPlanThird
                    floorPlan3.DisplayEachTable(oRow, i)
                Case FloorPlanForth
                    floorPlan4.DisplayEachTable(oRow, i)
                Case FloorPlanFifth
                    floorPlan5.DisplayEachTable(oRow, i)
            End Select
            AddHandler btnTable(i).TableSelectedEvent, AddressOf TableReadyToOpen

            '444        End If
            i += 1
        Next
        '       floorPlan1.ResumeLayout(False)
    End Sub

    Friend Sub AdjustTableColor()
        Dim cc As Color
        Dim oRow As DataRow
        Dim i As Integer

        For Each oRow In dsOrder.Tables("AllTables").Rows
            '444   If oRow("Active") = True Then
            If Not oRow("EmployeeID") Is DBNull.Value And Not oRow("TableStatusID") Is DBNull.Value Then

                If oRow("EmployeeID") = currentServer.EmployeeID And oRow("TableStatusID") > 1 And oRow("TableStatusID") < 8 Then
                    '   is this employees table
                    cc = Color.LightGreen
                Else
                    cc = DetermineColor(oRow("TableStatusID"))
                End If
            Else
                cc = DetermineColor(1) 'oRow("TableStatusID"))
            End If

            btnTable(i).lblTableNum.BackColor = cc
            If cc.ToString = c15.ToString Then
                btnTable(i).IsAvail = True
            Else
                btnTable(i).IsAvail = False
            End If

            '444   End If
            i += 1
        Next

    End Sub


    Private Sub TableReadyToOpen(ByVal tn As Integer, ByVal isAvail As Boolean)

        _tableSelected = tn

        If OverrideAvail = True Then
            'comes in from Mgr OverrideTableStatus      ????????????
            ResetSeatingChartTableStatus(TableSelected, OverrideAvail)
            '       AdjustSeatingChartTableColor()
            changesMade = True
            Exit Sub
        End If

        If isAvail = True Then 'avail for seating
            '        numCustPad.Visible = False
            '       Me.Visible = False
            SelectNumberOfCustomers()
            Exit Sub
        End If

        If allowTableOverride = True Then   'currently hardcoded to true
            SeatingOverrideInfo = New DataSet_Builder.Information_UC("This Table Is Unavailable:      Select To Override?")
            SeatingOverrideInfo.Location = New Point((Me.Width - SeatingOverrideInfo.Width) / 2, (Me.Height - SeatingOverrideInfo.Height) / 2)
            Me.Controls.Add(SeatingOverrideInfo)
            SeatingOverrideInfo.BringToFront()
        Else
            Dim info As DataSet_Builder.Information_UC
            info = New DataSet_Builder.Information_UC("This Table Is Unavailable.")
            info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            Me.Controls.Add(info)
            info.BringToFront()
        End If

    End Sub


    Private Sub SelectNumberOfCustomers()

        Try
            numCustPad.ColorButtonFromStart(0)
            numCustPad.Visible = True
            numCustPad.BringToFront()
        Catch ex As Exception
            InitiateNumberCustomerPad()
        End Try

    End Sub

    Private Sub NumCustPad_Entered(ByVal custInteger As Integer) Handles numCustPad.NumberCustomerEntered

        '     Me.Visible = False
        Me._numberCustomers = custInteger
        '      RaiseEvent NumberCustomerEvent()
        ResetFloorPlan()
        numCustPad.Visible = False
        Me.Visible = False
        RaiseEvent NumberCustomerEvent()
        '    NewAddNewTable(custInteger)


        '     UpdateTableStatusAfterSelection()

        '       If mainServerConnected = True Then
        '      If changesMade = True Then
        '     UpdateAvailTablesData()
        '    End If
        '   End If


    End Sub

    Private Sub btnSeatingCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeatingCode.Click

        seatingCode = New Seating_ColorCode

        seatingCode.Location = New Point(25, (((Me.Height - Me.pnlSeating.Height) - seatingCode.Height) / 2))

        Me.Controls.Add(seatingCode)
        Me.seatingCode.BringToFront()


    End Sub

    Private Sub btnChangeAvail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeAvail.Click

        If typeProgram = "Online_Demo" Then
            MsgBox("Demo will not allow.", MsgBoxStyle.Information, "DEMO Purposes only")
            Exit Sub
        End If

        If changingAvail = False Then
            changingAvail = True
            Me.btnChangeAvail.BackColor = c9
        Else
            changingAvail = False
            Me.btnChangeAvail.BackColor = c15
        End If

    End Sub

    Private Sub btnClose_Clicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click

        If typeProgram = "Online_Demo" Then
            MsgBox("Demo will not allow you to exit this screen without selecting a table.", MsgBoxStyle.Information, "DEMO Purposes only")
            Exit Sub
        End If

        If changesMade = True Then
            UpdateAvailTablesData()
        End If


        '       If Not currentTable Is Nothing Then
        RaiseEvent NoTableSelected(sender, e)
        '         MsgBox(currentTable.ExperienceNumber)
        '      End If

        '   Me.Dispose()
        ResetFloorPlan()
        numCustPad.Visible = False
        Me.Visible = False

    End Sub

    Private Sub ResetFloorPlan()
        Dim fpRow As DataRow
        Dim fpCount As Integer = 1

        For Each fpRow In ds.Tables("TermsFloor").Rows
            Select Case fpCount
                Case 1 : floorPlan1.Visible = True
                Case 2 : floorPlan2.Visible = False
                Case 3 : floorPlan3.Visible = False
                Case 4 : floorPlan4.Visible = False
                Case 5 : floorPlan5.Visible = False
            End Select
            fpCount += 1
        Next

    End Sub

    Private Sub SeatingOverrideAcepted(ByVal sender As Object, ByVal e As System.EventArgs) Handles SeatingOverrideInfo.AcceptInformation
        '   ChangeAvailStatus()
        '      Me.Visible = False
        '      numCustPad.Visible = False
        '     Me.Visible = False
        SeatingOverrideInfo.Dispose()
        SelectNumberOfCustomers()

    End Sub



    Private Sub btnSeatingNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeatingNext.Click

        Select Case currentFloorPlan
            Case 1
                floorPlan1.Visible = False
                floorPlan1.nextFloorPlan.Visible = True
                '           If floorPlan1.nextFloorPlan Is floorPlan2 Then
                '          currentFloorPlan = 2
                '         End If
            Case 2
                floorPlan2.Visible = False
                floorPlan2.nextFloorPlan.Visible = True
                '          If floorPlan2.nextFloorPlan Is floorPlan3 Then
                '         currentFloorPlan = 3
                '        Else
                '           currentFloorPlan = 1
                '      End If
        End Select


    End Sub

    Private Sub btnSeatingPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeatingPrevious.Click

        Select Case currentFloorPlan
            Case 1
                floorPlan1.Visible = False
                floorPlan1.previousFloorPlan.Visible = True
            Case 2
                floorPlan2.Visible = False
                floorPlan2.previousFloorPlan.Visible = True
        End Select
    End Sub









    '222
    '*********************
    '   everything below is old





    Private Sub NewTableSelected222(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles seatingChart.TableSelectedEvent ', seatingChartCesar.TableSelectedEvent, seatingChart2.TableSelectedEvent

        Dim objButton As New Button
        Dim tbl As PhysicalTable

        objButton = CType(sender, Button)
        _tableSelected = CType(objButton.Text, Integer)

        If OverrideAvail = True Then
            'comes in from Mgr OverrideTableStatus
            ResetSeatingChartTableStatus(TableSelected, OverrideAvail)
            '          AdjustSeatingChartTableColor()
            changesMade = True
            Exit Sub
        End If

        If changingAvail = True Then
            ChangeAvailStatus()
            Exit Sub
        End If

        If objButton.BackColor.ToString = c1.ToString Then  'check down
            ChangeAvailStatus()
            Exit Sub
        End If

        If objButton.BackColor.ToString = c15.ToString Then

            '       DisposeAllSeatingCharts()
            '      SelectNumberOfCustomers()
            Exit Sub
        End If

        '      If objButton.BackColor.ToString = c5.ToString Then
        If allowTableOverride = True Then
            SeatingOverrideInfo = New DataSet_Builder.Information_UC("This Table Is Unavailable:      Select To Override?")
            SeatingOverrideInfo.Location = New Point((Me.Width - SeatingOverrideInfo.Width) / 2, (Me.Height - SeatingOverrideInfo.Height) / 2)
            Me.Controls.Add(SeatingOverrideInfo)
            SeatingOverrideInfo.BringToFront()
            '       If MsgBox("This Table Is Unavailable For Seating, Would You Like To Override?", MsgBoxStyle.YesNo, ) = MsgBoxResult.Yes Then
            '  End If
        Else
            Dim info As DataSet_Builder.Information_UC
            info = New DataSet_Builder.Information_UC("This Table Is Unavailable.")
            info.Location = New Point((Me.Width - info.Width) / 2, (Me.Height - info.Height) / 2)
            Me.Controls.Add(info)
            info.BringToFront()
        End If
        '     End If

    End Sub



    Private Sub UpdateTableStatusAfterSelection()
        Dim oRow As DataRow

        For Each oRow In dsOrder.Tables("AllTables").Rows     'currentPhysicalTables
            If oRow("TableNumber") = TableSelected Then
                oRow("TableStatusID") = 2
                Exit For
            End If
        Next

        '       Dim tbl As PhysicalTable
        '
        '       For Each tbl In currentPhysicalTables
        '      If tbl.PhysicalTableNumber = TableSelected Then
        '     tbl.CurrentStatus = 2
        '    Exit For
        '   End If
        '  Next


    End Sub



    Private Sub ChangeAvailStatus()
        Dim oRow As DataRow
        Dim cc As Color
        Dim tbl As PhysicalTable

        For Each oRow In dsOrder.Tables("AllTables").Rows  '  For Each tbl In currentPhysicalTables
            If oRow("TableNumber") = TableSelected Then '.PhysicalTableNumber = TableSelected Then
                If Not oRow("TableStatusID") Is DBNull.Value Then
                    If oRow("TableStatusID") = 0 Then            'unavail
                        oRow("TableStatusID") = 1
                    ElseIf oRow("TableStatusID") = 1 Then        'avail for seating
                        oRow("TableStatusID") = 0
                    ElseIf oRow("TableStatusID") = 7 Then        'check down
                        oRow("TableStatusID") = 10
                    Else                                    'all other including sat
                        '   bring up override question
                        '   or make manager do override
                    End If
                  
                    Exit For
                End If
            End If
        Next

        changesMade = True

    End Sub



End Class
