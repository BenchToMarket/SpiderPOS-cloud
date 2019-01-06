Public Class Seating_Dining
    Inherits System.Windows.Forms.UserControl

    Private _tableSelected As Integer
  
    Friend Property TableSelected() As Integer
        Get
            Return _tableSelected
        End Get
        Set(ByVal Value As Integer)
            _tableSelected = Value
        End Set
    End Property

    Event TableSelectedEvent(ByVal sender As Object, ByVal e As System.EventArgs)


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
    Friend WithEvents tbl11 As System.Windows.Forms.Button
    Friend WithEvents tbl12 As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents tbl13 As System.Windows.Forms.Button
    Friend WithEvents tbl20 As System.Windows.Forms.Button
    Friend WithEvents tbl30 As System.Windows.Forms.Button
    Friend WithEvents tbl14 As System.Windows.Forms.Button
    Friend WithEvents tbl21 As System.Windows.Forms.Button
    Friend WithEvents tbl31 As System.Windows.Forms.Button
    Friend WithEvents tbl22 As System.Windows.Forms.Button
    Friend WithEvents tbl32 As System.Windows.Forms.Button
    Friend WithEvents tbl15 As System.Windows.Forms.Button
    Friend WithEvents tbl16 As System.Windows.Forms.Button
    Friend WithEvents tbl17 As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlDownStairs = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.tbl17 = New System.Windows.Forms.Button
        Me.tbl16 = New System.Windows.Forms.Button
        Me.tbl15 = New System.Windows.Forms.Button
        Me.tbl32 = New System.Windows.Forms.Button
        Me.tbl22 = New System.Windows.Forms.Button
        Me.tbl31 = New System.Windows.Forms.Button
        Me.tbl21 = New System.Windows.Forms.Button
        Me.tbl14 = New System.Windows.Forms.Button
        Me.tbl30 = New System.Windows.Forms.Button
        Me.tbl20 = New System.Windows.Forms.Button
        Me.tbl13 = New System.Windows.Forms.Button
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.tbl12 = New System.Windows.Forms.Button
        Me.tbl11 = New System.Windows.Forms.Button
        Me.pnlDownStairs.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlDownStairs
        '
        Me.pnlDownStairs.BackColor = System.Drawing.Color.Black
        Me.pnlDownStairs.Controls.Add(Me.Panel1)
        Me.pnlDownStairs.Controls.Add(Me.Panel5)
        Me.pnlDownStairs.Controls.Add(Me.Panel6)
        Me.pnlDownStairs.Controls.Add(Me.Panel2)
        Me.pnlDownStairs.Controls.Add(Me.tbl17)
        Me.pnlDownStairs.Controls.Add(Me.tbl16)
        Me.pnlDownStairs.Controls.Add(Me.tbl15)
        Me.pnlDownStairs.Controls.Add(Me.tbl32)
        Me.pnlDownStairs.Controls.Add(Me.tbl22)
        Me.pnlDownStairs.Controls.Add(Me.tbl31)
        Me.pnlDownStairs.Controls.Add(Me.tbl21)
        Me.pnlDownStairs.Controls.Add(Me.tbl14)
        Me.pnlDownStairs.Controls.Add(Me.tbl30)
        Me.pnlDownStairs.Controls.Add(Me.tbl20)
        Me.pnlDownStairs.Controls.Add(Me.tbl13)
        Me.pnlDownStairs.Controls.Add(Me.Panel3)
        Me.pnlDownStairs.Controls.Add(Me.Panel4)
        Me.pnlDownStairs.Controls.Add(Me.tbl12)
        Me.pnlDownStairs.Controls.Add(Me.tbl11)
        Me.pnlDownStairs.Location = New System.Drawing.Point(40, 24)
        Me.pnlDownStairs.Name = "pnlDownStairs"
        Me.pnlDownStairs.Size = New System.Drawing.Size(704, 512)
        Me.pnlDownStairs.TabIndex = 19
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.SlateBlue
        Me.Panel1.Location = New System.Drawing.Point(568, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(16, 480)
        Me.Panel1.TabIndex = 34
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.SlateBlue
        Me.Panel5.Location = New System.Drawing.Point(248, 240)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(16, 96)
        Me.Panel5.TabIndex = 33
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.SlateBlue
        Me.Panel6.Location = New System.Drawing.Point(264, 240)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(304, 16)
        Me.Panel6.TabIndex = 32
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.SlateBlue
        Me.Panel2.Location = New System.Drawing.Point(184, 192)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(384, 16)
        Me.Panel2.TabIndex = 30
        '
        'tbl17
        '
        Me.tbl17.BackColor = System.Drawing.Color.DarkGray
        Me.tbl17.ForeColor = System.Drawing.Color.White
        Me.tbl17.Location = New System.Drawing.Point(448, 64)
        Me.tbl17.Name = "tbl17"
        Me.tbl17.Size = New System.Drawing.Size(72, 72)
        Me.tbl17.TabIndex = 29
        Me.tbl17.Text = "17"
        '
        'tbl16
        '
        Me.tbl16.BackColor = System.Drawing.Color.DarkGray
        Me.tbl16.ForeColor = System.Drawing.Color.White
        Me.tbl16.Location = New System.Drawing.Point(480, 8)
        Me.tbl16.Name = "tbl16"
        Me.tbl16.Size = New System.Drawing.Size(72, 40)
        Me.tbl16.TabIndex = 28
        Me.tbl16.Text = "16"
        '
        'tbl15
        '
        Me.tbl15.BackColor = System.Drawing.Color.DarkGray
        Me.tbl15.ForeColor = System.Drawing.Color.White
        Me.tbl15.Location = New System.Drawing.Point(400, 8)
        Me.tbl15.Name = "tbl15"
        Me.tbl15.Size = New System.Drawing.Size(72, 40)
        Me.tbl15.TabIndex = 27
        Me.tbl15.Text = "15"
        '
        'tbl32
        '
        Me.tbl32.BackColor = System.Drawing.Color.DarkGray
        Me.tbl32.ForeColor = System.Drawing.Color.White
        Me.tbl32.Location = New System.Drawing.Point(376, 128)
        Me.tbl32.Name = "tbl32"
        Me.tbl32.Size = New System.Drawing.Size(48, 48)
        Me.tbl32.TabIndex = 26
        Me.tbl32.Text = "32"
        '
        'tbl22
        '
        Me.tbl22.BackColor = System.Drawing.Color.DarkGray
        Me.tbl22.ForeColor = System.Drawing.Color.White
        Me.tbl22.Location = New System.Drawing.Point(376, 64)
        Me.tbl22.Name = "tbl22"
        Me.tbl22.Size = New System.Drawing.Size(48, 48)
        Me.tbl22.TabIndex = 25
        Me.tbl22.Text = "22"
        '
        'tbl31
        '
        Me.tbl31.BackColor = System.Drawing.Color.DarkGray
        Me.tbl31.ForeColor = System.Drawing.Color.White
        Me.tbl31.Location = New System.Drawing.Point(304, 128)
        Me.tbl31.Name = "tbl31"
        Me.tbl31.Size = New System.Drawing.Size(48, 48)
        Me.tbl31.TabIndex = 24
        Me.tbl31.Text = "31"
        '
        'tbl21
        '
        Me.tbl21.BackColor = System.Drawing.Color.DarkGray
        Me.tbl21.ForeColor = System.Drawing.Color.White
        Me.tbl21.Location = New System.Drawing.Point(304, 64)
        Me.tbl21.Name = "tbl21"
        Me.tbl21.Size = New System.Drawing.Size(48, 48)
        Me.tbl21.TabIndex = 23
        Me.tbl21.Text = "21"
        '
        'tbl14
        '
        Me.tbl14.BackColor = System.Drawing.Color.DarkGray
        Me.tbl14.ForeColor = System.Drawing.Color.White
        Me.tbl14.Location = New System.Drawing.Point(312, 8)
        Me.tbl14.Name = "tbl14"
        Me.tbl14.Size = New System.Drawing.Size(72, 40)
        Me.tbl14.TabIndex = 22
        Me.tbl14.Text = "14"
        '
        'tbl30
        '
        Me.tbl30.BackColor = System.Drawing.Color.DarkGray
        Me.tbl30.ForeColor = System.Drawing.Color.White
        Me.tbl30.Location = New System.Drawing.Point(224, 128)
        Me.tbl30.Name = "tbl30"
        Me.tbl30.Size = New System.Drawing.Size(56, 48)
        Me.tbl30.TabIndex = 21
        Me.tbl30.Text = "30"
        '
        'tbl20
        '
        Me.tbl20.BackColor = System.Drawing.Color.DarkGray
        Me.tbl20.ForeColor = System.Drawing.Color.White
        Me.tbl20.Location = New System.Drawing.Point(224, 64)
        Me.tbl20.Name = "tbl20"
        Me.tbl20.Size = New System.Drawing.Size(56, 48)
        Me.tbl20.TabIndex = 20
        Me.tbl20.Text = "20"
        '
        'tbl13
        '
        Me.tbl13.BackColor = System.Drawing.Color.DarkGray
        Me.tbl13.ForeColor = System.Drawing.Color.White
        Me.tbl13.Location = New System.Drawing.Point(224, 8)
        Me.tbl13.Name = "tbl13"
        Me.tbl13.Size = New System.Drawing.Size(72, 40)
        Me.tbl13.TabIndex = 19
        Me.tbl13.Text = "13"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Crimson
        Me.Panel3.Location = New System.Drawing.Point(72, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(136, 16)
        Me.Panel3.TabIndex = 18
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.SlateBlue
        Me.Panel4.Location = New System.Drawing.Point(0, 192)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(120, 16)
        Me.Panel4.TabIndex = 17
        '
        'tbl12
        '
        Me.tbl12.BackColor = System.Drawing.Color.DarkGray
        Me.tbl12.ForeColor = System.Drawing.Color.White
        Me.tbl12.Location = New System.Drawing.Point(8, 96)
        Me.tbl12.Name = "tbl12"
        Me.tbl12.Size = New System.Drawing.Size(48, 80)
        Me.tbl12.TabIndex = 15
        Me.tbl12.Text = "12"
        '
        'tbl11
        '
        Me.tbl11.BackColor = System.Drawing.Color.DarkGray
        Me.tbl11.ForeColor = System.Drawing.Color.White
        Me.tbl11.Location = New System.Drawing.Point(8, 8)
        Me.tbl11.Name = "tbl11"
        Me.tbl11.Size = New System.Drawing.Size(48, 72)
        Me.tbl11.TabIndex = 1
        Me.tbl11.Text = "11"
        '
        'Seating_Dining
        '
        Me.BackColor = System.Drawing.Color.LightSlateGray
        Me.Controls.Add(Me.pnlDownStairs)
        Me.Name = "Seating_Dining"
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
            Case 11
                tbl11.BackColor = cc
            Case 12 : tbl12.BackColor = cc
            Case 13 : tbl13.BackColor = cc
            Case 14 : tbl14.BackColor = cc
            Case 15 : tbl15.BackColor = cc
            Case 16 : tbl16.BackColor = cc
            Case 17 : tbl17.BackColor = cc
            Case 20 : tbl20.BackColor = cc
            Case 21 : tbl21.BackColor = cc
            Case 22 : tbl22.BackColor = cc
            Case 30 : tbl30.BackColor = cc
            Case 31 : tbl31.BackColor = cc
            Case 32 : tbl32.BackColor = cc

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
    Private Sub Seating_Dining_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbl11.Click, tbl12.Click, tbl13.Click, tbl14.Click, tbl15.Click, tbl16.Click, tbl17.Click, tbl20.Click, tbl21.Click, tbl22.Click, tbl30.Click, tbl31.Click, tbl32.Click

        RaiseEvent TableSelectedEvent(sender, e)

    End Sub

End Class
