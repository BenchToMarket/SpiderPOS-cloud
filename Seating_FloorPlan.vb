Public Class Seating_FloorPlan
    Inherits System.Windows.Forms.UserControl

    Friend previousFloorPlan As Seating_FloorPlan
    Friend nextFloorPlan As Seating_FloorPlan
   
   

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlFloorPlan As System.Windows.Forms.Panel
    Friend WithEvents lblFloorPlanName As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Seating_FloorPlan))
        Me.panel1 = New System.Windows.Forms.Panel
        Me.pnlFloorPlan = New System.Windows.Forms.Panel
        Me.lblFloorPlanName = New System.Windows.Forms.Label
        Me.panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.DarkGray
        Me.panel1.Controls.Add(Me.pnlFloorPlan)
        Me.panel1.Location = New System.Drawing.Point(88, 80)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(560, 360)
        Me.panel1.TabIndex = 0
        '
        'pnlFloorPlan
        '
        Me.pnlFloorPlan.BackColor = System.Drawing.Color.Black
        Me.pnlFloorPlan.Location = New System.Drawing.Point(8, 8)
        Me.pnlFloorPlan.Name = "pnlFloorPlan"
        Me.pnlFloorPlan.Size = New System.Drawing.Size(296, 272)
        Me.pnlFloorPlan.TabIndex = 0
        '
        'lblFloorPlanName
        '
        Me.lblFloorPlanName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFloorPlanName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblFloorPlanName.Location = New System.Drawing.Point(16, 16)
        Me.lblFloorPlanName.Name = "lblFloorPlanName"
        Me.lblFloorPlanName.Size = New System.Drawing.Size(208, 40)
        Me.lblFloorPlanName.TabIndex = 3
        Me.lblFloorPlanName.Text = "Floor Plan"
        Me.lblFloorPlanName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Seating_FloorPlan
        '
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.Controls.Add(Me.lblFloorPlanName)
        Me.Controls.Add(Me.panel1)
        Me.Name = "Seating_FloorPlan"
        Me.Size = New System.Drawing.Size(900, 650)
        Me.panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Friend Sub DisplayEachTable(ByRef oRow As DataRow, ByVal i As Integer)

        btnTable(i) = New Seating_Table_UC2(i, oRow("xLoc"), oRow("yLoc"), oRow("myWidth"), oRow("myHeight"), oRow("TableNumber"), oRow("FloorPlanID"), True)

        Me.pnlFloorPlan.Controls.Add(btnTable(i))

    End Sub

    Friend Sub DisplayEachWall(ByRef oRow As DataRow, ByVal i As Integer)

        btnWall(i) = New Panel
        With btnWall(i)
            .Location = New Point(oRow("xLoc"), oRow("yLoc"))
            .BackColor = Color.DarkGray 'Black
            .Size = New Size(oRow("myWidth"), oRow("myHeight"))
        End With
        Me.pnlFloorPlan.Controls.Add(btnWall(i))

    End Sub

End Class
