Public Class Seating_Table_UC
    Inherits System.Windows.Forms.UserControl

    '***************************
    '   not using
    '   using Seating_Table_UC2


    Private _index As Integer
    Private _term_table_Num As Integer
    Private _floorPlanID As Integer
    Private _isAvail As Boolean
    Private _tblStatusID As Integer
    Friend WithEvents lblTableNum As System.Windows.Forms.Label

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

    Friend Property IsAvail() As Boolean
        Get
            Return _isAvail
        End Get
        Set(ByVal Value As Boolean)
            _isAvail = Value
        End Set
    End Property

    Friend Property tblStatusID() As Integer
        Get
            Return _tblStatusID
        End Get
        Set(ByVal Value As Integer)
            _tblStatusID = Value
        End Set
    End Property


    Event TableSelectedEvent(ByVal tn As Integer)




    Public Sub New(ByVal i As Integer, ByVal x As Single, ByVal y As Single, ByVal w As Single, ByVal h As Single, ByVal tn As Integer, ByVal fp As Integer, ByVal isAct As Boolean, ByVal ts As Integer)
        MyBase.New()

        _index = i
        _term_table_Num = tn    'this is index for walls
        Me.Location = New System.Drawing.Point(x, y)
        _floorPlanID = fp
        Me.Width = w
        Me.Height = h
        _isAvail = isAct
        _tblStatusID = ts

        Me.lblTableNum = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblTableNum
        '
        Me.lblTableNum.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTableNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTableNum.Location = New System.Drawing.Point(16, 24)
        Me.lblTableNum.Name = "lblTableNum"
        Me.lblTableNum.Size = New System.Drawing.Size(40, 16)
        Me.lblTableNum.TabIndex = 0
        Me.lblTableNum.Text = Term_Table_Num.ToString
        '
        'Seating_Table_UC
        '
        '     Me.BackColor = System.Drawing.Color.IndianRed
        Me.Controls.Add(Me.lblTableNum)
        Me.Name = "Seating_Table_UC"
        '      Me.Size = New System.Drawing.Size(64, 56)
        Me.ResumeLayout(False)



    End Sub







    Private Sub Seating_Table_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click, lblTableNum.Click

        RaiseEvent TableSelectedEvent(Term_Table_Num)

    End Sub

End Class
