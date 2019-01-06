Imports System.Drawing

Public Class SelectionPanel_UC
    Inherits System.Windows.Forms.UserControl

    Dim btnSelection As DataSet_Builder.BatchButton_UC

    Dim _dvUsing As New DataView
    Dim _dtUsing As DataTable
    Dim _purpose As String
    Private _pMenuID As Integer
    Private _sMenuID As Integer




    Dim btnWidth As Single
    Dim btnHeight As Single
    Dim buttonspace As Single = 2

    Dim WithEvents menuSelect As MenuSelection_


    Event CancelSelection()
    Event ButtonSelected(ByVal sender As Object, ByVal e As System.EventArgs)
    Event ChangeMenus()
    Event AcceptMenuEvent()

    Public Property dvUsing() As DataView
        Get
            Return _dvUsing
        End Get
        Set(ByVal Value As DataView)
            _dvUsing = Value
        End Set
    End Property

    Public Property dtUsing() As DataTable
        Get
            Return _dtUsing
        End Get
        Set(ByVal Value As DataTable)
            _dtUsing = Value
        End Set
    End Property

    Public Property Purpose() As String
        Get
            Return _purpose
        End Get
        Set(ByVal Value As String)
            _purpose = Value
        End Set
    End Property

    Public Property PMenuID() As Integer
        Get
            Return _pMenuID
        End Get
        Set(ByVal Value As Integer)
            _pMenuID = Value
        End Set
    End Property

    Public Property SMenuID() As Integer
        Get
            Return _sMenuID
        End Get
        Set(ByVal Value As Integer)
            _sMenuID = Value
        End Set
    End Property

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        dvUsing = New DataView
        dtUsing = New DataTable
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
    Friend WithEvents pnlBorder As System.Windows.Forms.Panel
    Friend WithEvents pnlButtonFill As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlBorder = New System.Windows.Forms.Panel
        Me.pnlButtonFill = New System.Windows.Forms.Panel
        Me.pnlBorder.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBorder
        '
        Me.pnlBorder.BackColor = System.Drawing.Color.SlateBlue
        Me.pnlBorder.Controls.Add(Me.pnlButtonFill)
        Me.pnlBorder.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.pnlBorder.Location = New System.Drawing.Point(0, 0)
        Me.pnlBorder.Name = "pnlBorder"
        Me.pnlBorder.Size = New System.Drawing.Size(624, 560)
        Me.pnlBorder.TabIndex = 0
        '
        'pnlButtonFill
        '
        Me.pnlButtonFill.BackColor = System.Drawing.Color.LightGray
        Me.pnlButtonFill.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlButtonFill.Location = New System.Drawing.Point(16, 16)
        Me.pnlButtonFill.Name = "pnlButtonFill"
        Me.pnlButtonFill.Size = New System.Drawing.Size(592, 520)
        Me.pnlButtonFill.TabIndex = 0
        '
        'SelectionPanel_UC
        '
        Me.BackColor = System.Drawing.Color.LightSlateGray
        Me.Controls.Add(Me.pnlBorder)
        Me.Name = "SelectionPanel_UC"
        Me.Size = New System.Drawing.Size(624, 552)
        Me.pnlBorder.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther()

        '     DetermineButtonSizes()
        '    DetermineButtonLocations()




    End Sub

    Public Sub DetermineButtonSizes()

        If dvUsing.Count < 41 Then
            btnWidth = (Me.pnlButtonFill.Width - (5 * buttonspace)) / 4
            btnHeight = (Me.pnlButtonFill.Height - (11 * buttonspace)) / 10

        Else
            '   *** we can change this depending on how we want to display
            btnWidth = (Me.pnlButtonFill.Width - (5 * buttonspace)) / 4
            btnHeight = (Me.pnlButtonFill.Height - (13 * buttonspace)) / 12

        End If


    End Sub

    Public Sub DetermineButtonLocations()
        Dim x As Single = buttonspace
        Dim y As Single = buttonspace
        Dim count As Integer
        Dim vRow As DataRowView

        For Each vRow In dvUsing
            CreateButtons(x, y, vRow)
            If count < 6 Then
                y = y + btnHeight + buttonspace
            Else
                x = x + btnWidth + buttonspace
                y = buttonspace
                count = 0
            End If
            count += 1
        Next


    End Sub

    Private Sub CreateButtons(ByVal x As Single, ByVal y As Single, ByRef vRow As DataRowView)
        Dim oRow As DataRow
        Dim menuName As String

        For Each oRow In dtUsing.Rows
            If oRow("MenuID") = vRow("PrimaryMenu") Then
                menuName = oRow("MenuName")
                Exit For
            End If
        Next


        btnSelection = New DataSet_Builder.BatchButton_UC(btnWidth, btnHeight, vRow, menuName)


        With btnSelection
            .Location = New Point(x, y)
            AddHandler btnSelection.TableClicked, AddressOf ButtonSelection_Click

        End With

        Me.pnlButtonFill.Controls.Add(btnSelection)


    End Sub

    Public Sub ButtonSelection_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlButtonFill.Click
        Dim objButton As New DataSet_Builder.BatchButton_UC(Nothing, Nothing, Nothing, Nothing)

        If Not sender.GetType Is objButton.GetType Then Exit Sub

        RaiseEvent ButtonSelected(sender, e)

        Me.Dispose()

    End Sub

    Public Sub StartOpenNewBusiness()
        Me.menuSelect = New MenuSelection_uc(dtUsing, Nothing, Nothing)    'nothing indicates no menuID

        menuSelect.Location = New Point(((Me.pnlButtonFill.Width - menuSelect.Width) / 2), 50)
        Me.pnlButtonFill.Controls.Add(menuSelect)


    End Sub
    Public Sub ClearSelectionPanel()
        Me.pnlButtonFill.Controls.Clear()

    End Sub

    Private Sub AccentMenu_Click() Handles menuSelect.AcceptMenuEvent
        _pMenuID = menuSelect.PMenuID
        _sMenuID = menuSelect.SMenuID
        RaiseEvent AcceptMenuEvent()

    End Sub

    Private Sub ChangeMenu() Handles menuSelect.ChangeMenus
        _pMenuID = menuSelect.PMenuID
        _sMenuID = menuSelect.SMenuID
        RaiseEvent ChangeMenus()

    End Sub


End Class
