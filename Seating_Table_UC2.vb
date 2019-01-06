Public Class Seating_Table_UC2
    Inherits System.Windows.Forms.UserControl

    Private _index As Integer
    Private _term_table_Num As Integer
    Private _floorPlanID As Integer
    Private _isActive As Boolean
    Private _width As Integer
    Private _height As Integer
    Private _isAvail As Boolean


    Friend Property Index() As Integer
        Get
            Return _index
        End Get
        Set(ByVal Value As Integer)
            _index = Value
        End Set
    End Property

    Friend Property Term_Table_Num() As Integer
        Get
            Return _term_table_Num
        End Get
        Set(ByVal Value As Integer)
            _term_table_Num = Value
        End Set
    End Property

    Friend Property FloorPlanID() As Integer
        Get
            Return _floorPlanID
        End Get
        Set(ByVal Value As Integer)
            _floorPlanID = Value
        End Set
    End Property

    Friend Property IsActive() As Boolean
        Get
            Return _isActive
        End Get
        Set(ByVal Value As Boolean)
            _isActive = Value
        End Set
    End Property

    Friend Property IsAvail() As Boolean
        Get
            Return _isAvail
        End Get
        Set(ByVal Value As Boolean)
            _isAvail = Value
        End Set
    End Property



    Event TableSelectedEvent(ByVal tn As Integer, ByVal avail As Boolean)

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal i As Integer, ByVal x As Single, ByVal y As Single, ByVal w As Single, ByVal h As Single, ByVal tn As Integer, ByVal fp As Integer, ByVal isAct As Boolean)
        MyBase.New()

        _index = i
        _term_table_Num = tn    'this is index for walls
        Me.Location = New System.Drawing.Point(x, y)
        _floorPlanID = fp
        _isActive = isAct
        _width = w
        _height = h

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
    Friend WithEvents lblTableNum As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblTableNum = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblTableNum
        '
        Me.lblTableNum.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTableNum.BackColor = System.Drawing.SystemColors.Control
        Me.lblTableNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTableNum.Location = New System.Drawing.Point(2, 2)
        Me.lblTableNum.Name = "lblTableNum"
        Me.lblTableNum.Size = New System.Drawing.Size(146, 146)
        Me.lblTableNum.TabIndex = 0
        Me.lblTableNum.Text = "Table"
        Me.lblTableNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Seating_Table_UC2
        '
        Me.BackColor = System.Drawing.Color.DarkBlue
        Me.Controls.Add(Me.lblTableNum)
        Me.Name = "Seating_Table_UC2"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther()

        Me.Width = _width
        Me.Height = _height
        Me.lblTableNum.Text = Me.Term_Table_Num.ToString

    End Sub


    Private Sub Seating_Table_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click, lblTableNum.Click

        RaiseEvent TableSelectedEvent(Term_Table_Num, IsAvail)

    End Sub


End Class
