Imports System.Windows.Forms

Public Class SelectionDaily_UC
    Inherits System.Windows.Forms.UserControl
    '   Dim sql As New DataSet_Builder.SQLHelper(connectserver)

    Friend sDailyCode As Int64
    Friend sPrimaryMenu As Integer
    Friend sSecondaryMenu As Integer
    Friend chosenTermMethod As String

    Event NewDaily()
    Event DailySelected() 'ByVal sender As Object, ByVal e As System.EventArgs)

#Region " Windows Form Designer generated code "

    '  Dim dvUsing As DataView
    ' Dim dtUsing As DataTable

    Public Sub New(ByRef dv As DataView)    'dv is dvOpenBusiness
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        '     Me.SelectionPanel_Daily.dvUsing = New DataView

        Me.SelectionPanel_DailyNew.dvUsing = dv
        Me.SelectionPanel_DailyNew.dtUsing = ds.Tables("MenuChoice")

        If dv.Count = 0 Then
            Me.SelectionPanel_DailyNew.StartOpenNewBusiness()
        Else
            Me.SelectionPanel_DailyNew.DetermineButtonSizes()
            Me.SelectionPanel_DailyNew.DetermineButtonLocations()
        End If

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
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    '  Friend WithEvents SelectionPanel_Daily As DataSet_Builder.SelectionPanel_UC
    '   Friend WithEvents SelectionPanel_DailyNew As DataSet_Builder.SelectionPanel_UC
    Friend WithEvents SelectionPanel_DailyNew As DataSet_Builder.SelectionPanel_UC
    Friend WithEvents btnDailyQuick As System.Windows.Forms.Button
    Friend WithEvents btnDailyTable As System.Windows.Forms.Button
    Friend WithEvents btnDailyBar As System.Windows.Forms.Button
    Friend WithEvents lblDailyChoose As System.Windows.Forms.Label
    Friend WithEvents pnlDailyChooseTermMethod As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(SelectionDaily_UC))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.SelectionPanel_DailyNew = New DataSet_Builder.SelectionPanel_UC
        Me.pnlDailyChooseTermMethod = New System.Windows.Forms.Panel
        Me.lblDailyChoose = New System.Windows.Forms.Label
        Me.btnDailyBar = New System.Windows.Forms.Button
        Me.btnDailyTable = New System.Windows.Forms.Button
        Me.btnDailyQuick = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.pnlDailyChooseTermMethod.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Button6)
        Me.Panel1.Controls.Add(Me.Button5)
        Me.Panel1.Controls.Add(Me.Button4)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Location = New System.Drawing.Point(16, 88)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(128, 448)
        Me.Panel1.TabIndex = 3
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.LightSlateGray
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.Location = New System.Drawing.Point(16, 376)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(96, 56)
        Me.Button6.TabIndex = 5
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.Color.LightSlateGray
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Location = New System.Drawing.Point(16, 304)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(96, 56)
        Me.Button5.TabIndex = 4
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.LightSlateGray
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(16, 232)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(96, 56)
        Me.Button4.TabIndex = 3
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.LightSlateGray
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(16, 160)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(96, 56)
        Me.Button3.TabIndex = 2
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.LightSlateGray
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(16, 88)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(96, 56)
        Me.Button2.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.LightSlateGray
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button1.Location = New System.Drawing.Point(16, 16)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 56)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Create New Daily Business"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(312, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(328, 32)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Current Open Daily Business"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SelectionPanel_DailyNew
        '
        Me.SelectionPanel_DailyNew.BackColor = System.Drawing.Color.LightSlateGray
        Me.SelectionPanel_DailyNew.Location = New System.Drawing.Point(160, 32)
        Me.SelectionPanel_DailyNew.Name = "SelectionPanel_DailyNew"
        Me.SelectionPanel_DailyNew.PMenuID = 0
        Me.SelectionPanel_DailyNew.Purpose = Nothing
        Me.SelectionPanel_DailyNew.Size = New System.Drawing.Size(728, 552)
        Me.SelectionPanel_DailyNew.SMenuID = 0
        Me.SelectionPanel_DailyNew.TabIndex = 5
        '
        'pnlDailyChooseTermMethod
        '
        Me.pnlDailyChooseTermMethod.Controls.Add(Me.lblDailyChoose)
        Me.pnlDailyChooseTermMethod.Controls.Add(Me.btnDailyBar)
        Me.pnlDailyChooseTermMethod.Controls.Add(Me.btnDailyTable)
        Me.pnlDailyChooseTermMethod.Controls.Add(Me.btnDailyQuick)
        Me.pnlDailyChooseTermMethod.Location = New System.Drawing.Point(392, 288)
        Me.pnlDailyChooseTermMethod.Name = "pnlDailyChooseTermMethod"
        Me.pnlDailyChooseTermMethod.Size = New System.Drawing.Size(288, 104)
        Me.pnlDailyChooseTermMethod.TabIndex = 6
        Me.pnlDailyChooseTermMethod.Visible = False
        '
        'lblDailyChoose
        '
        Me.lblDailyChoose.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDailyChoose.Location = New System.Drawing.Point(32, 8)
        Me.lblDailyChoose.Name = "lblDailyChoose"
        Me.lblDailyChoose.Size = New System.Drawing.Size(216, 24)
        Me.lblDailyChoose.TabIndex = 3
        Me.lblDailyChoose.Text = "Choose Terminal Method"
        Me.lblDailyChoose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnDailyBar
        '
        Me.btnDailyBar.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnDailyBar.Location = New System.Drawing.Point(16, 40)
        Me.btnDailyBar.Name = "btnDailyBar"
        Me.btnDailyBar.Size = New System.Drawing.Size(80, 48)
        Me.btnDailyBar.TabIndex = 2
        Me.btnDailyBar.Text = "Bar"
        '
        'btnDailyTable
        '
        Me.btnDailyTable.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnDailyTable.Location = New System.Drawing.Point(104, 40)
        Me.btnDailyTable.Name = "btnDailyTable"
        Me.btnDailyTable.Size = New System.Drawing.Size(80, 48)
        Me.btnDailyTable.TabIndex = 1
        Me.btnDailyTable.Text = "Table"
        '
        'btnDailyQuick
        '
        Me.btnDailyQuick.Location = New System.Drawing.Point(192, 40)
        Me.btnDailyQuick.Name = "btnDailyQuick"
        Me.btnDailyQuick.Size = New System.Drawing.Size(80, 48)
        Me.btnDailyQuick.TabIndex = 0
        Me.btnDailyQuick.Text = "Quick"
        '
        'SelectionDaily_UC
        '
        Me.BackColor = System.Drawing.Color.LightSlateGray
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.Controls.Add(Me.pnlDailyChooseTermMethod)
        Me.Controls.Add(Me.SelectionPanel_DailyNew)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "SelectionDaily_UC"
        Me.Size = New System.Drawing.Size(920, 600)
        Me.Panel1.ResumeLayout(False)
        Me.pnlDailyChooseTermMethod.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther()

        TestNumberOpenBusiness(1)
        '      If System.Windows.Forms.SystemInformation.ComputerName = "DILEO" Or System.Windows.Forms.SystemInformation.ComputerName = "EGLOBAL" Then
        If typeProgram = "Demo" Or typeProgram = "Online_Demo" Or SystemInformation.ComputerName = "EGLOBALMAIN" Or SystemInformation.ComputerName = "DILEO" Then
            Me.pnlDailyChooseTermMethod.Visible = True
            chosenTermMethod = "Bar"
        Else
            chosenTermMethod = currentTerminal.TermMethod
        End If

    End Sub

    Private Sub TestNumberOpenBusiness(ByVal maxAllowed As Integer)

        If dsOrder.Tables("OpenBusiness").Rows.Count > maxAllowed Then
            MsgBox("You should have only 1 (one) Daily Business Open at a time. The only Daily Code active will be the Last Opened. Please close all others.", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub OpenDailyBusinessSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectionPanel_DailyNew.ButtonSelected

        sDailyCode = sender.dailyCode
        sPrimaryMenu = sender.PrimaryMenu
        sSecondaryMenu = sender.SecondaryMenu

        '    currentTerminal.currentDailyCode = sender.dailyCode
        If typeProgram = "Online_Demo" Then
            If chosenTermMethod = "Quick" Then
                ds.ReadXml("Lunch_Dinner_QuickDemo.xml", XmlReadMode.ReadSchema)
            Else
                ds.ReadXml("Lunch_Dinner.xml", XmlReadMode.ReadSchema)
            End If
            ds.AcceptChanges()
        End If

        RaiseEvent DailySelected()  'sender, e)

        '      Dim info As New DataSet_Builder.Info2_UC("Loading ...")
        '     info.Location = New Point((SelectionPanel_DailyNew.Width - info.Width) / 2, (SelectionPanel_DailyNew.Height - info.Height) / 2)
        '    Me.SelectionPanel_DailyNew.Controls.Add(info)
        '   info.BringToFront()

        Me.Dispose()

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If typeProgram = "Online_Demo" Then
            MsgBox("Select which module you want. This will take you to the Demo.")
        Else
            MsgBox("Select the button on the bottom right to open new Daily or Exit and Re-enter spiderPOS.")
        End If
        Exit Sub

        If dsOrder.Tables("OpenBusiness").Rows.Count > 0 Then
            If MsgBox("You should have only 1 (one) Daily Business Open at a time. This new Daily Code will be the only one active. All Tables and Tabs started in other Daily Business will not be available to any Employees. Please Verify Opening New Daily Business.", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                Me.SelectionPanel_DailyNew.ClearSelectionPanel()
                Me.SelectionPanel_DailyNew.StartOpenNewBusiness()
            End If
        End If

    End Sub

    Private Sub AcceptMenu_Click() Handles SelectionPanel_DailyNew.AcceptMenuEvent

        Dim oRow As DataRow


        currentTerminal.primaryMenuID = SelectionPanel_DailyNew.PMenuID
        currentTerminal.secondaryMenuID = SelectionPanel_DailyNew.SMenuID
        '     RepopulateMenu()

        RaiseEvent NewDaily()

        For Each oRow In ds.Tables("MenuChoice").Rows
            If oRow("MenuID") = currentTerminal.primaryMenuID Then
                oRow("LastOrder") = 1
            ElseIf oRow("MenuID") = currentTerminal.secondaryMenuID Then
                oRow("LastOrder") = 2
            Else
                oRow("LastOrder") = DBNull.Value
            End If
        Next

        '444     If mainServerConnected = True Then
        Try
            GenerateOrderTables.TempConnectToPhoenix()
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlDailyMenuChoice.Update(ds.Tables("MenuChoice"))
            ds.Tables("MenuChoice").AcceptChanges()
            sql.cn.Close()

        Catch ex As Exception
            CloseConnection()
            GenerateOrderTables.ConnectBackFromTempDatabase()
            MsgBox(ex.Message)
        End Try
        GenerateOrderTables.ConnectBackFromTempDatabase()
        '444     End If

        Me.SelectionPanel_DailyNew.Dispose()
        Me.Dispose()

    End Sub

    Private Sub ChangeMenu_Click() Handles SelectionPanel_DailyNew.ChangeMenus

        currentTerminal.primaryMenuID = SelectionPanel_DailyNew.PMenuID
        currentTerminal.secondaryMenuID = SelectionPanel_DailyNew.SMenuID
        Me.SelectionPanel_DailyNew.Dispose()
        '      RaiseEvent NewDaily(sender, e)
        Me.Dispose()

    End Sub




    Private Sub btnDailyBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDailyBar.Click
        Me.btnDailyTable.BackColor = Color.LightSlateGray
        Me.btnDailyBar.BackColor = Color.CornflowerBlue
        Me.btnDailyQuick.BackColor = Color.LightSlateGray
        Me.chosenTermMethod = "Bar"

        TestForDemo()

    End Sub

    Private Sub btnDailyTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDailyTable.Click
        Me.btnDailyTable.BackColor = Color.CornflowerBlue
        Me.btnDailyBar.BackColor = Color.LightSlateGray
        Me.btnDailyQuick.BackColor = Color.LightSlateGray
        Me.chosenTermMethod = "Table"

        TestForDemo()

    End Sub

    Private Sub btnDailyQuick_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDailyQuick.Click
        Me.btnDailyTable.BackColor = Color.LightSlateGray
        Me.btnDailyBar.BackColor = Color.LightSlateGray
        Me.btnDailyQuick.BackColor = Color.CornflowerBlue
        Me.chosenTermMethod = "Quick"

        TestForDemo()

    End Sub

    Private Sub TestForDemo()

        If typeProgram = "Online_Demo" Then
            If dsOrder.Tables("OpenBusiness").Rows.Count = 1 Then '> 0 Then
                sDailyCode = dsOrder.Tables("OpenBusiness").Rows(0)("DailyCode")
                sPrimaryMenu = dsOrder.Tables("OpenBusiness").Rows(0)("PrimaryMenu")
                sSecondaryMenu = dsOrder.Tables("OpenBusiness").Rows(0)("SecondaryMenu")

                If chosenTermMethod = "Quick" Then
                    ds.ReadXml("Lunch_Dinner_QuickDemo.xml", XmlReadMode.ReadSchema)
                Else
                    ds.ReadXml("Lunch_Dinner.xml", XmlReadMode.ReadSchema)
                End If
                ds.AcceptChanges()

                RaiseEvent DailySelected()
                Me.Dispose()
            End If
        End If
    End Sub

End Class
