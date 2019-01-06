Imports DataSet_Builder





Public Class TabSelection_UC
    Inherits System.Windows.Forms.UserControl



    Dim btnTabIdentity(40) As KitchenButton
    Dim numberOfIdentifiers As Integer = 20



    Dim _tabIdentString As String
    Dim _nCustomers As Integer


    Public Property TabIdentString() As String
        Get
            Return _tabIdentString
        End Get
        Set(ByVal Value As String)
            _tabIdentString = Value
        End Set
    End Property

    Public Property NCustomers() As String
        Get
            Return _nCustomers
        End Get
        Set(ByVal Value As String)
            _nCustomers = Value
        End Set
    End Property

    Event TabIdentDispose()


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
    Friend WithEvents NumberOfCustomers_UC1 As DataSet_Builder.NumberOfCustomers_UC
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlTabIdentity As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.NumberOfCustomers_UC1 = New DataSet_Builder.NumberOfCustomers_UC
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlTabIdentity = New System.Windows.Forms.Panel
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnAccept = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'NumberOfCustomers_UC1
        '
        Me.NumberOfCustomers_UC1.BackColor = System.Drawing.Color.Black
        Me.NumberOfCustomers_UC1.Location = New System.Drawing.Point(56, 80)
        Me.NumberOfCustomers_UC1.Name = "NumberOfCustomers_UC1"
        Me.NumberOfCustomers_UC1.Size = New System.Drawing.Size(312, 480)
        Me.NumberOfCustomers_UC1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(424, 80)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(464, 480)
        Me.Panel1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.pnlTabIdentity)
        Me.Panel2.Location = New System.Drawing.Point(16, 16)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(432, 448)
        Me.Panel2.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(432, 40)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Tab Identifier"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTabIdentity
        '
        Me.pnlTabIdentity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlTabIdentity.Location = New System.Drawing.Point(24, 48)
        Me.pnlTabIdentity.Name = "pnlTabIdentity"
        Me.pnlTabIdentity.Size = New System.Drawing.Size(392, 344)
        Me.pnlTabIdentity.TabIndex = 0
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(760, 576)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 48)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        '
        'btnAccept
        '
        Me.btnAccept.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAccept.Location = New System.Drawing.Point(648, 576)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(88, 48)
        Me.btnAccept.TabIndex = 3
        Me.btnAccept.Text = "Accept"
        '
        'TabSelection_UC
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(119, Byte), CType(154, Byte), CType(198, Byte))
        Me.Controls.Add(Me.btnAccept)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.NumberOfCustomers_UC1)
        Me.Name = "TabSelection_UC"
        Me.Size = New System.Drawing.Size(920, 648)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther()

        _nCustomers = 0
        _tabIdentString = "***---***"

        CreateTabIdentityButtonPanel()

    End Sub


    Public Sub InitializeCurrentSettings(ByVal nc As Integer, ByVal idString As String)

        Dim oRow As DataRow

        _nCustomers = nc
        _tabIdentString = idString

        If Not NCustomers = 0 Then
            Me.NumberOfCustomers_UC1.ColorButtonFromStart(currentTable.NumberOfCustomers)
        Else
            NumberOfCustomers_UC1.ColorButtonFromStart(0)
        End If
        If Not TabIdentString = "***---***" Then
            For Each oRow In ds.Tables("TabIdentifier").Rows
                If oRow("TabSelectorIdentity") = TabIdentString Then
                    btnTabIdentity(oRow("TabSelectorOrder")).BackColor = Color.RoyalBlue
                    Exit Sub
                End If
            Next
        End If
        _tabIdentString = "***---***" 'this is a reset if there was no match

    End Sub

    Private Sub CreateTabIdentityButtonPanel()
        Dim w As Single = pnlTabIdentity.Width / 5
        Dim h As Single = Me.pnlTabIdentity.Height / 4
        Dim x As Single = 0
        Dim y As Single = 0
        Dim oRow As DataRow
        Dim r As Integer = 0
        Dim c As Integer = 0

        Dim index As Integer

        For index = 1 To numberOfIdentifiers

            x = w * c
            y = h * r

            CreateTabIdentityButton(index, x, y, w, h, c7)

            If index = 5 Or index = 10 Or index = 15 Or index = 20 Or index = 25 Or index = 30 Or index = 35 Or index = 40 Then
                r += 1
                c = 0
            Else
                c += 1
            End If
        Next

        For Each oRow In ds.Tables("TabIdentifier").Rows
            With btnTabIdentity(oRow("TabSelectorOrder"))
                .Text = oRow("TabSelectorIdentity")
                .BackColor = c15
                .ID = oRow("TabSelectorOrder")
                AddHandler .Click, AddressOf ModifyTabIdentityButton_Click
            End With
        Next

    End Sub



    Private Sub CreateTabIdentityButton(ByVal btnNo As Integer, ByVal xPos As Single, ByVal yPos As Single, ByVal w As Single, ByVal h As Single, ByVal cc As Color)

        '   we don't create the first one son they match with the ID integer
        btnTabIdentity(btnNo) = New KitchenButton("", w, h, cc, c2)
        With btnTabIdentity(btnNo)
            .Location = New Point(xPos, yPos)
            .ForeColor = c3
        End With

        Me.pnlTabIdentity.Controls.Add(btnTabIdentity(btnNo))

    End Sub



    Private Sub ModifyTabIdentityButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim objButton As KitchenButton

        Try
            objButton = CType(sender, KitchenButton)
        Catch ex As Exception
            Exit Sub
        End Try

        _tabIdentString = objButton.Text
        TabIdentityChange(objButton)

    End Sub

    Private Sub TabIdentityChange(ByVal obj As KitchenButton)

        Dim oRow As DataRow

        For Each oRow In ds.Tables("TabIdentifier").Rows
            With btnTabIdentity(oRow("TabSelectorOrder"))
                .BackColor = c15
            End With
        Next

        obj.BackColor = Color.RoyalBlue
        PerformAcceptTest()

    End Sub

    Private Sub NumCust_Click(ByVal custInteger As Integer) Handles NumberOfCustomers_UC1.NumberCustomerEntered

        _nCustomers = custInteger
        PerformAcceptTest()

    End Sub

    Private Sub PerformAcceptTest()

        If Not NCustomers = 0 And Not TabIdentString = "***---***" Then
            currentTable.NumberOfCustomers = NCustomers
            currentTable.TabName = TabIdentString
            '        currentTable.TabID = -555
            GenerateOrderTables.LoadTabIDinExperinceTable()
            'sss      GenerateOrderTables.SaveAvailTabsAndTables()
            RaiseEvent TabIdentDispose()
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        RaiseEvent TabIdentDispose()

    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click

        Dim somethingChanged As Boolean = False

        If Not NCustomers = 0 And Not currentTable.NumberOfCustomers = NCustomers Then
            currentTable.NumberOfCustomers = NCustomers
            somethingChanged = True
        End If

        If Not TabIdentString = "***---***" And Not currentTable.TabName = TabIdentString Then
            currentTable.TabName = TabIdentString
            somethingChanged = True
        End If

        If somethingChanged = True Then
            GenerateOrderTables.LoadTabIDinExperinceTable()
        End If
        RaiseEvent TabIdentDispose()

    End Sub
End Class
