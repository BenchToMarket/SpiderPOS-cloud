Public Class Seating_Dining_Cesar
    Inherits System.Windows.Forms.UserControl
    Private _tableSelected As Integer
    ' Private _numberCustomers As Integer

    Friend Property TableSelected() As Integer
        Get
            Return _tableSelected
        End Get
        Set(ByVal Value As Integer)
            _tableSelected = Value
        End Set
    End Property

    '   Friend Property NumberCustomers() As Integer
    '       Get
    '           Return _numberCustomers
    '       End Get
    '       Set(ByVal Value As Integer)
    '           _numberCustomers = Value
    '       End Set
    '  End Property


    Event TableSelectedEvent(ByVal sender As Object, ByVal e As System.EventArgs)
    '   Event NumberCustomerEvent(ByVal sender As Object, ByVal e As System.EventArgs)


#Region " Windows Form Designer generated code "

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
    Friend WithEvents pnlDownStairs As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tbl9 As System.Windows.Forms.Button
    Friend WithEvents tbl101 As System.Windows.Forms.Button
    Friend WithEvents tbl105 As System.Windows.Forms.Button
    Friend WithEvents tbl106 As System.Windows.Forms.Button
    Friend WithEvents tbl107 As System.Windows.Forms.Button
    Friend WithEvents tbl108 As System.Windows.Forms.Button
    Friend WithEvents tbl103 As System.Windows.Forms.Button
    Friend WithEvents tbl102 As System.Windows.Forms.Button
    Friend WithEvents tbl104 As System.Windows.Forms.Button
    Friend WithEvents tbl7 As System.Windows.Forms.Button
    Friend WithEvents tbl6 As System.Windows.Forms.Button
    Friend WithEvents tbl5 As System.Windows.Forms.Button
    Friend WithEvents tbl4 As System.Windows.Forms.Button
    Friend WithEvents tbl8 As System.Windows.Forms.Button
    Friend WithEvents tbl2 As System.Windows.Forms.Button
    Friend WithEvents tbl1 As System.Windows.Forms.Button
    Friend WithEvents tbl11 As System.Windows.Forms.Button
    Friend WithEvents tbl12 As System.Windows.Forms.Button
    Friend WithEvents tbl10 As System.Windows.Forms.Button
    Friend WithEvents tbl3 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlDownStairs = New System.Windows.Forms.Panel
        Me.tbl104 = New System.Windows.Forms.Button
        Me.tbl108 = New System.Windows.Forms.Button
        Me.tbl107 = New System.Windows.Forms.Button
        Me.tbl106 = New System.Windows.Forms.Button
        Me.tbl105 = New System.Windows.Forms.Button
        Me.tbl103 = New System.Windows.Forms.Button
        Me.tbl102 = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.tbl7 = New System.Windows.Forms.Button
        Me.tbl6 = New System.Windows.Forms.Button
        Me.tbl5 = New System.Windows.Forms.Button
        Me.tbl11 = New System.Windows.Forms.Button
        Me.tbl12 = New System.Windows.Forms.Button
        Me.tbl9 = New System.Windows.Forms.Button
        Me.tbl10 = New System.Windows.Forms.Button
        Me.tbl4 = New System.Windows.Forms.Button
        Me.tbl101 = New System.Windows.Forms.Button
        Me.tbl8 = New System.Windows.Forms.Button
        Me.tbl3 = New System.Windows.Forms.Button
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.tbl2 = New System.Windows.Forms.Button
        Me.tbl1 = New System.Windows.Forms.Button
        Me.pnlDownStairs.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlDownStairs
        '
        Me.pnlDownStairs.BackColor = System.Drawing.Color.Black
        Me.pnlDownStairs.Controls.Add(Me.tbl104)
        Me.pnlDownStairs.Controls.Add(Me.tbl108)
        Me.pnlDownStairs.Controls.Add(Me.tbl107)
        Me.pnlDownStairs.Controls.Add(Me.tbl106)
        Me.pnlDownStairs.Controls.Add(Me.tbl105)
        Me.pnlDownStairs.Controls.Add(Me.tbl103)
        Me.pnlDownStairs.Controls.Add(Me.tbl102)
        Me.pnlDownStairs.Controls.Add(Me.Panel1)
        Me.pnlDownStairs.Controls.Add(Me.Panel5)
        Me.pnlDownStairs.Controls.Add(Me.Panel6)
        Me.pnlDownStairs.Controls.Add(Me.Panel2)
        Me.pnlDownStairs.Controls.Add(Me.tbl7)
        Me.pnlDownStairs.Controls.Add(Me.tbl6)
        Me.pnlDownStairs.Controls.Add(Me.tbl5)
        Me.pnlDownStairs.Controls.Add(Me.tbl11)
        Me.pnlDownStairs.Controls.Add(Me.tbl12)
        Me.pnlDownStairs.Controls.Add(Me.tbl9)
        Me.pnlDownStairs.Controls.Add(Me.tbl10)
        Me.pnlDownStairs.Controls.Add(Me.tbl4)
        Me.pnlDownStairs.Controls.Add(Me.tbl101)
        Me.pnlDownStairs.Controls.Add(Me.tbl8)
        Me.pnlDownStairs.Controls.Add(Me.tbl3)
        Me.pnlDownStairs.Controls.Add(Me.Panel3)
        Me.pnlDownStairs.Controls.Add(Me.Panel4)
        Me.pnlDownStairs.Controls.Add(Me.tbl2)
        Me.pnlDownStairs.Controls.Add(Me.tbl1)
        Me.pnlDownStairs.Location = New System.Drawing.Point(40, 24)
        Me.pnlDownStairs.Name = "pnlDownStairs"
        Me.pnlDownStairs.Size = New System.Drawing.Size(712, 528)
        Me.pnlDownStairs.TabIndex = 19
        '
        'tbl104
        '
        Me.tbl104.BackColor = System.Drawing.Color.DarkGray
        Me.tbl104.ForeColor = System.Drawing.Color.White
        Me.tbl104.Location = New System.Drawing.Point(312, 72)
        Me.tbl104.Name = "tbl104"
        Me.tbl104.Size = New System.Drawing.Size(40, 40)
        Me.tbl104.TabIndex = 41
        Me.tbl104.Text = "104"
        '
        'tbl108
        '
        Me.tbl108.BackColor = System.Drawing.Color.DarkGray
        Me.tbl108.ForeColor = System.Drawing.Color.White
        Me.tbl108.Location = New System.Drawing.Point(536, 72)
        Me.tbl108.Name = "tbl108"
        Me.tbl108.Size = New System.Drawing.Size(40, 40)
        Me.tbl108.TabIndex = 40
        Me.tbl108.Text = "108"
        '
        'tbl107
        '
        Me.tbl107.BackColor = System.Drawing.Color.DarkGray
        Me.tbl107.ForeColor = System.Drawing.Color.White
        Me.tbl107.Location = New System.Drawing.Point(480, 72)
        Me.tbl107.Name = "tbl107"
        Me.tbl107.Size = New System.Drawing.Size(40, 40)
        Me.tbl107.TabIndex = 39
        Me.tbl107.Text = "107"
        '
        'tbl106
        '
        Me.tbl106.BackColor = System.Drawing.Color.DarkGray
        Me.tbl106.ForeColor = System.Drawing.Color.White
        Me.tbl106.Location = New System.Drawing.Point(424, 72)
        Me.tbl106.Name = "tbl106"
        Me.tbl106.Size = New System.Drawing.Size(40, 40)
        Me.tbl106.TabIndex = 38
        Me.tbl106.Text = "106"
        '
        'tbl105
        '
        Me.tbl105.BackColor = System.Drawing.Color.DarkGray
        Me.tbl105.ForeColor = System.Drawing.Color.White
        Me.tbl105.Location = New System.Drawing.Point(368, 72)
        Me.tbl105.Name = "tbl105"
        Me.tbl105.Size = New System.Drawing.Size(40, 40)
        Me.tbl105.TabIndex = 37
        Me.tbl105.Text = "105"
        '
        'tbl103
        '
        Me.tbl103.BackColor = System.Drawing.Color.DarkGray
        Me.tbl103.ForeColor = System.Drawing.Color.White
        Me.tbl103.Location = New System.Drawing.Point(256, 72)
        Me.tbl103.Name = "tbl103"
        Me.tbl103.Size = New System.Drawing.Size(40, 40)
        Me.tbl103.TabIndex = 36
        Me.tbl103.Text = "103"
        '
        'tbl102
        '
        Me.tbl102.BackColor = System.Drawing.Color.DarkGray
        Me.tbl102.ForeColor = System.Drawing.Color.White
        Me.tbl102.Location = New System.Drawing.Point(200, 72)
        Me.tbl102.Name = "tbl102"
        Me.tbl102.Size = New System.Drawing.Size(40, 40)
        Me.tbl102.TabIndex = 35
        Me.tbl102.Text = "102"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.SlateBlue
        Me.Panel1.Location = New System.Drawing.Point(696, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(16, 528)
        Me.Panel1.TabIndex = 34
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.SlateBlue
        Me.Panel5.Location = New System.Drawing.Point(584, 64)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(32, 48)
        Me.Panel5.TabIndex = 33
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.SlateBlue
        Me.Panel6.Location = New System.Drawing.Point(160, 32)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(456, 32)
        Me.Panel6.TabIndex = 32
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.SlateBlue
        Me.Panel2.Location = New System.Drawing.Point(568, 400)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(128, 16)
        Me.Panel2.TabIndex = 30
        '
        'tbl7
        '
        Me.tbl7.BackColor = System.Drawing.Color.DarkGray
        Me.tbl7.ForeColor = System.Drawing.Color.White
        Me.tbl7.Location = New System.Drawing.Point(336, 168)
        Me.tbl7.Name = "tbl7"
        Me.tbl7.Size = New System.Drawing.Size(64, 48)
        Me.tbl7.TabIndex = 29
        Me.tbl7.Text = "7"
        '
        'tbl6
        '
        Me.tbl6.BackColor = System.Drawing.Color.DarkGray
        Me.tbl6.ForeColor = System.Drawing.Color.White
        Me.tbl6.Location = New System.Drawing.Point(192, 336)
        Me.tbl6.Name = "tbl6"
        Me.tbl6.Size = New System.Drawing.Size(64, 48)
        Me.tbl6.TabIndex = 28
        Me.tbl6.Text = "6"
        '
        'tbl5
        '
        Me.tbl5.BackColor = System.Drawing.Color.DarkGray
        Me.tbl5.ForeColor = System.Drawing.Color.White
        Me.tbl5.Location = New System.Drawing.Point(192, 248)
        Me.tbl5.Name = "tbl5"
        Me.tbl5.Size = New System.Drawing.Size(64, 48)
        Me.tbl5.TabIndex = 27
        Me.tbl5.Text = "5"
        '
        'tbl11
        '
        Me.tbl11.BackColor = System.Drawing.Color.DarkGray
        Me.tbl11.ForeColor = System.Drawing.Color.White
        Me.tbl11.Location = New System.Drawing.Point(400, 336)
        Me.tbl11.Name = "tbl11"
        Me.tbl11.Size = New System.Drawing.Size(96, 48)
        Me.tbl11.TabIndex = 26
        Me.tbl11.Text = "11"
        '
        'tbl12
        '
        Me.tbl12.BackColor = System.Drawing.Color.DarkGray
        Me.tbl12.ForeColor = System.Drawing.Color.White
        Me.tbl12.Location = New System.Drawing.Point(600, 200)
        Me.tbl12.Name = "tbl12"
        Me.tbl12.Size = New System.Drawing.Size(48, 80)
        Me.tbl12.TabIndex = 25
        Me.tbl12.Text = "12"
        '
        'tbl9
        '
        Me.tbl9.BackColor = System.Drawing.Color.DarkGray
        Me.tbl9.ForeColor = System.Drawing.Color.White
        Me.tbl9.Location = New System.Drawing.Point(472, 168)
        Me.tbl9.Name = "tbl9"
        Me.tbl9.Size = New System.Drawing.Size(48, 48)
        Me.tbl9.TabIndex = 24
        Me.tbl9.Text = "9"
        '
        'tbl10
        '
        Me.tbl10.BackColor = System.Drawing.Color.DarkGray
        Me.tbl10.ForeColor = System.Drawing.Color.White
        Me.tbl10.Location = New System.Drawing.Point(464, 248)
        Me.tbl10.Name = "tbl10"
        Me.tbl10.Size = New System.Drawing.Size(64, 48)
        Me.tbl10.TabIndex = 23
        Me.tbl10.Text = "10"
        '
        'tbl4
        '
        Me.tbl4.BackColor = System.Drawing.Color.DarkGray
        Me.tbl4.ForeColor = System.Drawing.Color.White
        Me.tbl4.Location = New System.Drawing.Point(192, 168)
        Me.tbl4.Name = "tbl4"
        Me.tbl4.Size = New System.Drawing.Size(64, 48)
        Me.tbl4.TabIndex = 22
        Me.tbl4.Text = "4"
        '
        'tbl101
        '
        Me.tbl101.BackColor = System.Drawing.Color.DarkGray
        Me.tbl101.ForeColor = System.Drawing.Color.White
        Me.tbl101.Location = New System.Drawing.Point(144, 72)
        Me.tbl101.Name = "tbl101"
        Me.tbl101.Size = New System.Drawing.Size(40, 40)
        Me.tbl101.TabIndex = 21
        Me.tbl101.Text = "101"
        '
        'tbl8
        '
        Me.tbl8.BackColor = System.Drawing.Color.DarkGray
        Me.tbl8.ForeColor = System.Drawing.Color.White
        Me.tbl8.Location = New System.Drawing.Point(336, 248)
        Me.tbl8.Name = "tbl8"
        Me.tbl8.Size = New System.Drawing.Size(64, 48)
        Me.tbl8.TabIndex = 20
        Me.tbl8.Text = "8"
        '
        'tbl3
        '
        Me.tbl3.BackColor = System.Drawing.Color.DarkGray
        Me.tbl3.ForeColor = System.Drawing.Color.White
        Me.tbl3.Location = New System.Drawing.Point(16, 336)
        Me.tbl3.Name = "tbl3"
        Me.tbl3.Size = New System.Drawing.Size(64, 48)
        Me.tbl3.TabIndex = 19
        Me.tbl3.Text = "3"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.SlateBlue
        Me.Panel3.Location = New System.Drawing.Point(144, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(16, 64)
        Me.Panel3.TabIndex = 18
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.SlateBlue
        Me.Panel4.Location = New System.Drawing.Point(0, 408)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(120, 16)
        Me.Panel4.TabIndex = 17
        '
        'tbl2
        '
        Me.tbl2.BackColor = System.Drawing.Color.DarkGray
        Me.tbl2.ForeColor = System.Drawing.Color.White
        Me.tbl2.Location = New System.Drawing.Point(16, 248)
        Me.tbl2.Name = "tbl2"
        Me.tbl2.Size = New System.Drawing.Size(64, 48)
        Me.tbl2.TabIndex = 15
        Me.tbl2.Text = "2"
        '
        'tbl1
        '
        Me.tbl1.BackColor = System.Drawing.Color.DarkGray
        Me.tbl1.ForeColor = System.Drawing.Color.White
        Me.tbl1.Location = New System.Drawing.Point(16, 168)
        Me.tbl1.Name = "tbl1"
        Me.tbl1.Size = New System.Drawing.Size(64, 48)
        Me.tbl1.TabIndex = 1
        Me.tbl1.Text = "1"
        '
        'Seating_Dining_Cesar
        '
        Me.BackColor = System.Drawing.Color.LightSlateGray
        Me.Controls.Add(Me.pnlDownStairs)
        Me.Name = "Seating_Dining_Cesar"
        Me.Size = New System.Drawing.Size(792, 573)
        Me.pnlDownStairs.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther()


        AdjustTableColor()
        '       not using below line while testing
        Me.AdjustTableColorForCurrentEmployee()

    End Sub

    Private Sub AdjustTableColor()
        Dim cc As Color
        Dim oRow As DataRow  'PhysicalTable

        For Each oRow In dsOrder.Tables("AllTables").Rows    ' currentPhysicalTables
            cc = DetermineColor(oRow("TableStatusID"))    '.CurrentStatus)
            ChangeColor(oRow("TableNumber"), cc)     '.PhysicalTableNumber, cc)
        Next

    End Sub

    Friend Sub ChangeColor(ByVal tableNumber As Integer, ByVal cc As Color)

        Select Case tableNumber
            Case 1
                tbl1.BackColor = cc
            Case 2 : tbl2.BackColor = cc
            Case 3 : tbl3.BackColor = cc
            Case 4 : tbl4.BackColor = cc
            Case 5 : tbl5.BackColor = cc
            Case 6 : tbl6.BackColor = cc
            Case 7 : tbl7.BackColor = cc
            Case 8 : tbl8.BackColor = cc
            Case 9 : tbl9.BackColor = cc
            Case 10 : tbl10.BackColor = cc
            Case 11 : tbl11.BackColor = cc
            Case 12 : tbl12.BackColor = cc
            Case 101 : tbl101.BackColor = cc
            Case 102 : tbl102.BackColor = cc
            Case 103 : tbl103.BackColor = cc
            Case 104 : tbl104.BackColor = cc
            Case 105 : tbl105.BackColor = cc
            Case 106 : tbl106.BackColor = cc
            Case 107 : tbl107.BackColor = cc
            Case 108 : tbl108.BackColor = cc

        End Select

    End Sub


    Friend Function DetermineColor(ByVal currentStatus As Integer)
        Dim colorChoice As Color
        '   do not change colors
        '   status is dependant on colors in other parts of program

        If currentStatus = 0 Then      'unavailable
            colorChoice = c5            'dim gray
        ElseIf currentStatus = 1 Then  'available for seating
            colorChoice = c6            'slate blue
        ElseIf currentStatus = 7 Then 'check down
            colorChoice = c1            'yellow
        Else                                'table sat (includes all)
            colorChoice = c9            'red
        End If

        Return colorChoice
    End Function

    Private Sub AdjustTableColorForCurrentEmployee()
        Dim oRow As DataRow

        For Each oRow In dsOrder.Tables("AvailTables").Rows
            ChangeColor(oRow("TableNumber"), Color.LightGreen)
        Next

    End Sub
    Private Sub Seating_Dining_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbl9.Click, tbl7.Click, tbl6.Click, tbl5.Click, tbl4.Click, tbl8.Click, tbl2.Click, tbl1.Click, tbl11.Click, tbl12.Click, tbl10.Click, tbl3.Click, tbl101.Click, tbl102.Click, tbl103.Click, tbl104.Click, tbl105.Click, tbl106.Click, tbl107.Click, tbl108.Click

        RaiseEvent TableSelectedEvent(sender, e)


    End Sub


End Class
