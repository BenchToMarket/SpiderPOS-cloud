Public Class Seating_Dining2
    Inherits System.Windows.Forms.UserControl

    Dim sql As DataSet_Builder.SQLHelper


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
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tbl904 As System.Windows.Forms.Button
    Friend WithEvents tbl903 As System.Windows.Forms.Button
    Friend WithEvents tbl902 As System.Windows.Forms.Button
    Friend WithEvents tbl901 As System.Windows.Forms.Button
    Friend WithEvents tbl905 As System.Windows.Forms.Button
    Friend WithEvents tbl917 As System.Windows.Forms.Button
    Friend WithEvents tbl916 As System.Windows.Forms.Button
    Friend WithEvents tbl915 As System.Windows.Forms.Button
    Friend WithEvents tbl906 As System.Windows.Forms.Button
    Friend WithEvents tbl920 As System.Windows.Forms.Button
    Friend WithEvents tbl922 As System.Windows.Forms.Button
    Friend WithEvents tbl919 As System.Windows.Forms.Button
    Friend WithEvents tbl914 As System.Windows.Forms.Button
    Friend WithEvents tbl921 As System.Windows.Forms.Button
    Friend WithEvents tbl918 As System.Windows.Forms.Button
    Friend WithEvents tbl913 As System.Windows.Forms.Button
    Friend WithEvents tbl912 As System.Windows.Forms.Button
    Friend WithEvents tbl911 As System.Windows.Forms.Button
    Friend WithEvents tbl910 As System.Windows.Forms.Button
    Friend WithEvents tbl909 As System.Windows.Forms.Button
    Friend WithEvents tbl908 As System.Windows.Forms.Button
    Friend WithEvents tbl907 As System.Windows.Forms.Button
    Friend WithEvents pnlBar As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlBar = New System.Windows.Forms.Panel
        Me.tbl905 = New System.Windows.Forms.Button
        Me.tbl907 = New System.Windows.Forms.Button
        Me.tbl908 = New System.Windows.Forms.Button
        Me.tbl909 = New System.Windows.Forms.Button
        Me.tbl910 = New System.Windows.Forms.Button
        Me.tbl901 = New System.Windows.Forms.Button
        Me.tbl902 = New System.Windows.Forms.Button
        Me.tbl903 = New System.Windows.Forms.Button
        Me.tbl904 = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.tbl917 = New System.Windows.Forms.Button
        Me.tbl916 = New System.Windows.Forms.Button
        Me.tbl915 = New System.Windows.Forms.Button
        Me.tbl906 = New System.Windows.Forms.Button
        Me.tbl920 = New System.Windows.Forms.Button
        Me.tbl922 = New System.Windows.Forms.Button
        Me.tbl919 = New System.Windows.Forms.Button
        Me.tbl914 = New System.Windows.Forms.Button
        Me.tbl921 = New System.Windows.Forms.Button
        Me.tbl918 = New System.Windows.Forms.Button
        Me.tbl913 = New System.Windows.Forms.Button
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.tbl912 = New System.Windows.Forms.Button
        Me.tbl911 = New System.Windows.Forms.Button
        Me.pnlBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBar
        '
        Me.pnlBar.BackColor = System.Drawing.Color.Black
        Me.pnlBar.Controls.Add(Me.tbl905)
        Me.pnlBar.Controls.Add(Me.tbl907)
        Me.pnlBar.Controls.Add(Me.tbl908)
        Me.pnlBar.Controls.Add(Me.tbl909)
        Me.pnlBar.Controls.Add(Me.tbl910)
        Me.pnlBar.Controls.Add(Me.tbl901)
        Me.pnlBar.Controls.Add(Me.tbl902)
        Me.pnlBar.Controls.Add(Me.tbl903)
        Me.pnlBar.Controls.Add(Me.tbl904)
        Me.pnlBar.Controls.Add(Me.Panel1)
        Me.pnlBar.Controls.Add(Me.Panel5)
        Me.pnlBar.Controls.Add(Me.tbl917)
        Me.pnlBar.Controls.Add(Me.tbl916)
        Me.pnlBar.Controls.Add(Me.tbl915)
        Me.pnlBar.Controls.Add(Me.tbl906)
        Me.pnlBar.Controls.Add(Me.tbl920)
        Me.pnlBar.Controls.Add(Me.tbl922)
        Me.pnlBar.Controls.Add(Me.tbl919)
        Me.pnlBar.Controls.Add(Me.tbl914)
        Me.pnlBar.Controls.Add(Me.tbl921)
        Me.pnlBar.Controls.Add(Me.tbl918)
        Me.pnlBar.Controls.Add(Me.tbl913)
        Me.pnlBar.Controls.Add(Me.Panel3)
        Me.pnlBar.Controls.Add(Me.Panel4)
        Me.pnlBar.Controls.Add(Me.tbl912)
        Me.pnlBar.Controls.Add(Me.tbl911)
        Me.pnlBar.Location = New System.Drawing.Point(40, 24)
        Me.pnlBar.Name = "pnlBar"
        Me.pnlBar.Size = New System.Drawing.Size(696, 512)
        Me.pnlBar.TabIndex = 19
        '
        'tbl905
        '
        Me.tbl905.BackColor = System.Drawing.Color.DarkGray
        Me.tbl905.ForeColor = System.Drawing.Color.White
        Me.tbl905.Location = New System.Drawing.Point(216, 96)
        Me.tbl905.Name = "tbl905"
        Me.tbl905.Size = New System.Drawing.Size(40, 40)
        Me.tbl905.TabIndex = 43
        Me.tbl905.Text = "905"
        '
        'tbl907
        '
        Me.tbl907.BackColor = System.Drawing.Color.DarkGray
        Me.tbl907.ForeColor = System.Drawing.Color.White
        Me.tbl907.Location = New System.Drawing.Point(64, 144)
        Me.tbl907.Name = "tbl907"
        Me.tbl907.Size = New System.Drawing.Size(40, 40)
        Me.tbl907.TabIndex = 42
        Me.tbl907.Text = "907"
        '
        'tbl908
        '
        Me.tbl908.BackColor = System.Drawing.Color.DarkGray
        Me.tbl908.ForeColor = System.Drawing.Color.White
        Me.tbl908.Location = New System.Drawing.Point(112, 144)
        Me.tbl908.Name = "tbl908"
        Me.tbl908.Size = New System.Drawing.Size(40, 40)
        Me.tbl908.TabIndex = 41
        Me.tbl908.Text = "908"
        '
        'tbl909
        '
        Me.tbl909.BackColor = System.Drawing.Color.DarkGray
        Me.tbl909.ForeColor = System.Drawing.Color.White
        Me.tbl909.Location = New System.Drawing.Point(160, 144)
        Me.tbl909.Name = "tbl909"
        Me.tbl909.Size = New System.Drawing.Size(40, 40)
        Me.tbl909.TabIndex = 40
        Me.tbl909.Text = "909"
        '
        'tbl910
        '
        Me.tbl910.BackColor = System.Drawing.Color.DarkGray
        Me.tbl910.ForeColor = System.Drawing.Color.White
        Me.tbl910.Location = New System.Drawing.Point(216, 144)
        Me.tbl910.Name = "tbl910"
        Me.tbl910.Size = New System.Drawing.Size(40, 40)
        Me.tbl910.TabIndex = 39
        Me.tbl910.Text = "910"
        '
        'tbl901
        '
        Me.tbl901.BackColor = System.Drawing.Color.DarkGray
        Me.tbl901.ForeColor = System.Drawing.Color.White
        Me.tbl901.Location = New System.Drawing.Point(16, 96)
        Me.tbl901.Name = "tbl901"
        Me.tbl901.Size = New System.Drawing.Size(40, 40)
        Me.tbl901.TabIndex = 38
        Me.tbl901.Text = "901"
        '
        'tbl902
        '
        Me.tbl902.BackColor = System.Drawing.Color.DarkGray
        Me.tbl902.ForeColor = System.Drawing.Color.White
        Me.tbl902.Location = New System.Drawing.Point(64, 96)
        Me.tbl902.Name = "tbl902"
        Me.tbl902.Size = New System.Drawing.Size(40, 40)
        Me.tbl902.TabIndex = 37
        Me.tbl902.Text = "902"
        '
        'tbl903
        '
        Me.tbl903.BackColor = System.Drawing.Color.DarkGray
        Me.tbl903.ForeColor = System.Drawing.Color.White
        Me.tbl903.Location = New System.Drawing.Point(112, 96)
        Me.tbl903.Name = "tbl903"
        Me.tbl903.Size = New System.Drawing.Size(40, 40)
        Me.tbl903.TabIndex = 36
        Me.tbl903.Text = "903"
        '
        'tbl904
        '
        Me.tbl904.BackColor = System.Drawing.Color.DarkGray
        Me.tbl904.ForeColor = System.Drawing.Color.White
        Me.tbl904.Location = New System.Drawing.Point(160, 96)
        Me.tbl904.Name = "tbl904"
        Me.tbl904.Size = New System.Drawing.Size(40, 40)
        Me.tbl904.TabIndex = 35
        Me.tbl904.Text = "904"
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
        Me.Panel5.Location = New System.Drawing.Point(120, 192)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(16, 304)
        Me.Panel5.TabIndex = 33
        '
        'tbl917
        '
        Me.tbl917.BackColor = System.Drawing.Color.DarkGray
        Me.tbl917.ForeColor = System.Drawing.Color.White
        Me.tbl917.Location = New System.Drawing.Point(216, 192)
        Me.tbl917.Name = "tbl917"
        Me.tbl917.Size = New System.Drawing.Size(40, 40)
        Me.tbl917.TabIndex = 29
        Me.tbl917.Text = "917"
        '
        'tbl916
        '
        Me.tbl916.BackColor = System.Drawing.Color.DarkGray
        Me.tbl916.ForeColor = System.Drawing.Color.White
        Me.tbl916.Location = New System.Drawing.Point(160, 432)
        Me.tbl916.Name = "tbl916"
        Me.tbl916.Size = New System.Drawing.Size(40, 40)
        Me.tbl916.TabIndex = 28
        Me.tbl916.Text = "916"
        '
        'tbl915
        '
        Me.tbl915.BackColor = System.Drawing.Color.DarkGray
        Me.tbl915.ForeColor = System.Drawing.Color.White
        Me.tbl915.Location = New System.Drawing.Point(160, 384)
        Me.tbl915.Name = "tbl915"
        Me.tbl915.Size = New System.Drawing.Size(40, 40)
        Me.tbl915.TabIndex = 27
        Me.tbl915.Text = "915"
        '
        'tbl906
        '
        Me.tbl906.BackColor = System.Drawing.Color.DarkGray
        Me.tbl906.ForeColor = System.Drawing.Color.White
        Me.tbl906.Location = New System.Drawing.Point(16, 144)
        Me.tbl906.Name = "tbl906"
        Me.tbl906.Size = New System.Drawing.Size(40, 40)
        Me.tbl906.TabIndex = 26
        Me.tbl906.Text = "906"
        '
        'tbl920
        '
        Me.tbl920.BackColor = System.Drawing.Color.DarkGray
        Me.tbl920.ForeColor = System.Drawing.Color.White
        Me.tbl920.Location = New System.Drawing.Point(216, 336)
        Me.tbl920.Name = "tbl920"
        Me.tbl920.Size = New System.Drawing.Size(40, 40)
        Me.tbl920.TabIndex = 25
        Me.tbl920.Text = "920"
        '
        'tbl922
        '
        Me.tbl922.BackColor = System.Drawing.Color.DarkGray
        Me.tbl922.ForeColor = System.Drawing.Color.White
        Me.tbl922.Location = New System.Drawing.Point(216, 432)
        Me.tbl922.Name = "tbl922"
        Me.tbl922.Size = New System.Drawing.Size(40, 40)
        Me.tbl922.TabIndex = 24
        Me.tbl922.Text = "922"
        '
        'tbl919
        '
        Me.tbl919.BackColor = System.Drawing.Color.DarkGray
        Me.tbl919.ForeColor = System.Drawing.Color.White
        Me.tbl919.Location = New System.Drawing.Point(216, 288)
        Me.tbl919.Name = "tbl919"
        Me.tbl919.Size = New System.Drawing.Size(40, 40)
        Me.tbl919.TabIndex = 23
        Me.tbl919.Text = "919"
        '
        'tbl914
        '
        Me.tbl914.BackColor = System.Drawing.Color.DarkGray
        Me.tbl914.ForeColor = System.Drawing.Color.White
        Me.tbl914.Location = New System.Drawing.Point(160, 336)
        Me.tbl914.Name = "tbl914"
        Me.tbl914.Size = New System.Drawing.Size(40, 40)
        Me.tbl914.TabIndex = 22
        Me.tbl914.Text = "914"
        '
        'tbl921
        '
        Me.tbl921.BackColor = System.Drawing.Color.DarkGray
        Me.tbl921.ForeColor = System.Drawing.Color.White
        Me.tbl921.Location = New System.Drawing.Point(216, 384)
        Me.tbl921.Name = "tbl921"
        Me.tbl921.Size = New System.Drawing.Size(40, 40)
        Me.tbl921.TabIndex = 21
        Me.tbl921.Text = "921"
        '
        'tbl918
        '
        Me.tbl918.BackColor = System.Drawing.Color.DarkGray
        Me.tbl918.ForeColor = System.Drawing.Color.White
        Me.tbl918.Location = New System.Drawing.Point(216, 240)
        Me.tbl918.Name = "tbl918"
        Me.tbl918.Size = New System.Drawing.Size(40, 40)
        Me.tbl918.TabIndex = 20
        Me.tbl918.Text = "918"
        '
        'tbl913
        '
        Me.tbl913.BackColor = System.Drawing.Color.DarkGray
        Me.tbl913.ForeColor = System.Drawing.Color.White
        Me.tbl913.Location = New System.Drawing.Point(160, 288)
        Me.tbl913.Name = "tbl913"
        Me.tbl913.Size = New System.Drawing.Size(40, 40)
        Me.tbl913.TabIndex = 19
        Me.tbl913.Text = "913"
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
        'tbl912
        '
        Me.tbl912.BackColor = System.Drawing.Color.DarkGray
        Me.tbl912.ForeColor = System.Drawing.Color.White
        Me.tbl912.Location = New System.Drawing.Point(160, 240)
        Me.tbl912.Name = "tbl912"
        Me.tbl912.Size = New System.Drawing.Size(40, 40)
        Me.tbl912.TabIndex = 15
        Me.tbl912.Text = "912"
        '
        'tbl911
        '
        Me.tbl911.BackColor = System.Drawing.Color.DarkGray
        Me.tbl911.ForeColor = System.Drawing.Color.White
        Me.tbl911.Location = New System.Drawing.Point(160, 192)
        Me.tbl911.Name = "tbl911"
        Me.tbl911.Size = New System.Drawing.Size(40, 40)
        Me.tbl911.TabIndex = 1
        Me.tbl911.Text = "911"
        '
        'Seating_Dining2
        '
        Me.BackColor = System.Drawing.Color.LightSlateGray
        Me.Controls.Add(Me.pnlBar)
        Me.Name = "Seating_Dining2"
        Me.Size = New System.Drawing.Size(792, 573)
        Me.pnlBar.ResumeLayout(False)
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
            Case 901
                tbl901.BackColor = cc
            Case 902 : tbl902.BackColor = cc
            Case 903 : tbl903.BackColor = cc
            Case 904 : tbl904.BackColor = cc
            Case 905 : tbl905.BackColor = cc
            Case 906 : tbl906.BackColor = cc
            Case 907 : tbl907.BackColor = cc
            Case 908 : tbl908.BackColor = cc
            Case 909 : tbl909.BackColor = cc
            Case 910 : tbl910.BackColor = cc
            Case 911 : tbl911.BackColor = cc
            Case 912 : tbl912.BackColor = cc
            Case 913 : tbl913.BackColor = cc
            Case 914 : tbl914.BackColor = cc
            Case 915 : tbl915.BackColor = cc
            Case 916 : tbl916.BackColor = cc
            Case 917 : tbl917.BackColor = cc
            Case 918 : tbl918.BackColor = cc
            Case 919 : tbl919.BackColor = cc
            Case 920 : tbl920.BackColor = cc
            Case 921 : tbl921.BackColor = cc
            Case 922 : tbl922.BackColor = cc


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
    Private Sub Seating_Dining_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbl917.Click, tbl916.Click, tbl915.Click, tbl906.Click, tbl920.Click, tbl922.Click, tbl919.Click, tbl914.Click, tbl921.Click, tbl918.Click, tbl913.Click, tbl912.Click, tbl911.Click, tbl901.Click, tbl902.Click, tbl903.Click, tbl904.Click, tbl905.Click, tbl906.Click, tbl907.Click, tbl908.Click, tbl909.Click, tbl910.Click

        RaiseEvent TableSelectedEvent(sender, e)


    End Sub




End Class
