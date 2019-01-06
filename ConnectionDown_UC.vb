Imports System.IO
Imports DataSet_Builder

Public Class ConnectionDown_UC
    Inherits System.Windows.Forms.UserControl

    Dim btnSavedMenu(30) As Button
    Friend menuString As String

    Event ArchiveMenuLoaded()
    Event ConnectionHelpCanceled()

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(40, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(296, 80)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Connection to Phoenix is down.  Report  issue   404.869.4700          Please choo" & _
        "se your Primary_Secondary Menu from list."
        '
        'btnCancel
        '
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(264, 464)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 40)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'ConnectionDown_UC
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(59, Byte), CType(96, Byte), CType(141, Byte))
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ConnectionDown_UC"
        Me.Size = New System.Drawing.Size(384, 520)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther()

        If typeProgram = "Online_Demo" Then Exit Sub

        Dim i As Integer
        Dim x As Single = 16
        Dim y As Single = 120

        Dim myDirectory As System.IO.DirectoryInfo
        myDirectory = New System.IO.DirectoryInfo(Path:="c:\Data Files\spiderPOS\Menu")

        'get the files
        Dim myfiles() As System.io.FileInfo
        myfiles = myDirectory.GetFiles()
        '  myfiles.Sort(Of Date)()

        'check file in the files collection

        Dim fs As System.IO.FileInfo
        Dim fString As String

        For Each fs In myfiles
            If fs.Name.Length > 3 Then
                fString = fs.Name.ToString.Substring(0, fs.Name.Length - 4)
            Else
                fString = "No Menu"
            End If
            btnSavedMenu(i) = New Button
            With btnSavedMenu(i)
                .Size = New Size(172, 32)
                .Location = New Point(x, y)
                .Text = fString 'fs.Name.ToString
                .ForeColor = c3
                .BackColor = c16
                AddHandler .Click, AddressOf MenuChoice_Click
            End With

            Me.Controls.Add(btnSavedMenu(i))


            y += 40
            i += 1
            If i = 14 Then
                x = 200
                y = 120
            ElseIf i = 29 Then
                Exit Sub
            End If

        Next

    End Sub

    Private Sub MenuChoice_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        ' we structure the tables in REVERSE order from how they load
        Dim fString As String
        fString = sender.text & ".xml"

        MenuChoiceRoutine(fString)

    End Sub

    Friend Sub MenuChoiceRoutine(ByVal fString As String)

        '    Dim sRow As DataRow
        Dim oRow As DataRow
        '    Dim mTable As String


        If dsStarter.Tables("StarterLocationOverview").Rows.Count = 0 Then
            Try
                LoadStarterDataSet()

                oRow = dsStarter.Tables("StarterLocationOverview").Rows(0)
                FillLocationOverviewData(oRow)


            Catch ex As Exception
                MsgBox(ex.Message, "   Can't load Starter Menu. Call spiderPOS at 404.869.4700")
                Exit Sub
            End Try

        End If

        '***this was the place for creating table structure


        'currently pulling backup automatically
        Try

            ds.Clear()

            If typeProgram = "Online_Demo" Then

                '       If currentTerminal.TermMethod = "Quick" Then
                'ds.ReadXml("Lunch_Dinner_QuickDemo.xml", XmlReadMode.ReadSchema)
                '    Else
                '       ds.ReadXml("Lunch_Dinner.xml", XmlReadMode.ReadSchema)
                '  End If
                dsOrder.ReadXml("OrderData.xml", XmlReadMode.ReadSchema)
                dsEmployee.ReadXml("EmployeeData.xml", XmlReadMode.ReadSchema)
                dsOrderDemo.ReadXml("OrderData.xml", XmlReadMode.ReadSchema)
                dsEmployeeDemo.ReadXml("EmployeeData.xml", XmlReadMode.ReadSchema)
                dsCustomer.ReadXml("CustomerData.xml", XmlReadMode.ReadSchema)
                dsCustomerDemo.ReadXml("CustomerData.xml", XmlReadMode.ReadSchema)
                '         dsInventory.ReadXml("InventoryData.xml", XmlReadMode.ReadSchema)
                '     dsOrderDemo.Tables("OpenOrders").PrimaryKey = Nothing
                '     dsOrderDemo.dtCurrentlyHeld.Rows(0)("CurrentlyHeld") = False

                ds.AcceptChanges()
                dsOrder.AcceptChanges()
                dsEmployee.AcceptChanges()
                dsCustomer.AcceptChanges()
                dsCustomer.AcceptChanges()

                dsOrderDemo.AcceptChanges()
                dsEmployeeDemo.AcceptChanges()
                dsCustomerDemo.AcceptChanges()


                orderInactiveTimer = New Timer
                tablesInactiveTimer = New Timer
                splitInactiveTimer = New Timer
                tmrCardRead = New Timer

                '     currentTerminal = New Terminal
                '    GenerateOrderTables.CreateTerminals()
                PopulateAllEmployeeColloection()

                '    MsgBox(dsOrder.Tables(0).Rows.Count)
                '    MsgBox(dsEmployee.Tables("LoggedInEmployees").Rows.Count)
                '      MsgBox(dsEmployee.Tables("AllEmployees").Rows.Count)

                '    aaaaa()

            Else
                'not online demo
                ds.ReadXml("c:\Data Files\spiderPOS\Menu\" & fString, XmlReadMode.ReadSchema)
            End If

            RaiseEvent ArchiveMenuLoaded()
            SetUpPrimaryKeys()

            '999 right before this is our trouble
            '      JustTestingTermsTables()

        Catch ex As Exception
            MsgBox(ex.Message)

            MsgBox("Menu NOT Found")
            RaiseEvent ConnectionHelpCanceled()
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        RaiseEvent ConnectionHelpCanceled()

    End Sub
End Class
