Public Class Seating_ColorCode
    Inherits System.Windows.Forms.UserControl

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
    Friend WithEvents lblCodeUnavail As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblCodeAvail As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents lblCodeSat As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnCodeClose As System.Windows.Forms.Button
    Friend WithEvents lblCodeCheckDown As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblCodeUnavail = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.lblCodeAvail = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.lblCodeSat = New System.Windows.Forms.Label
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.lblCodeCheckDown = New System.Windows.Forms.Label
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnCodeClose = New System.Windows.Forms.Button
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblCodeUnavail
        '
        Me.lblCodeUnavail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodeUnavail.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCodeUnavail.Location = New System.Drawing.Point(112, 16)
        Me.lblCodeUnavail.Name = "lblCodeUnavail"
        Me.lblCodeUnavail.Size = New System.Drawing.Size(104, 24)
        Me.lblCodeUnavail.TabIndex = 1
        Me.lblCodeUnavail.Text = "Unavailable"
        Me.lblCodeUnavail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.lblCodeUnavail)
        Me.Panel2.Location = New System.Drawing.Point(16, 16)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(224, 48)
        Me.Panel2.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(72, 48)
        Me.Panel1.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Controls.Add(Me.lblCodeAvail)
        Me.Panel3.Location = New System.Drawing.Point(16, 80)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(224, 48)
        Me.Panel3.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(72, 48)
        Me.Panel4.TabIndex = 1
        '
        'lblCodeAvail
        '
        Me.lblCodeAvail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodeAvail.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCodeAvail.Location = New System.Drawing.Point(112, 16)
        Me.lblCodeAvail.Name = "lblCodeAvail"
        Me.lblCodeAvail.Size = New System.Drawing.Size(104, 24)
        Me.lblCodeAvail.TabIndex = 1
        Me.lblCodeAvail.Text = "Available"
        Me.lblCodeAvail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Controls.Add(Me.lblCodeSat)
        Me.Panel5.Location = New System.Drawing.Point(16, 144)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(224, 48)
        Me.Panel5.TabIndex = 4
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Crimson
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(72, 48)
        Me.Panel6.TabIndex = 1
        '
        'lblCodeSat
        '
        Me.lblCodeSat.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodeSat.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCodeSat.Location = New System.Drawing.Point(112, 16)
        Me.lblCodeSat.Name = "lblCodeSat"
        Me.lblCodeSat.Size = New System.Drawing.Size(104, 24)
        Me.lblCodeSat.TabIndex = 1
        Me.lblCodeSat.Text = "Seated"
        Me.lblCodeSat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Panel8)
        Me.Panel7.Controls.Add(Me.lblCodeCheckDown)
        Me.Panel7.Location = New System.Drawing.Point(16, 208)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(224, 48)
        Me.Panel7.TabIndex = 5
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Yellow
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(72, 48)
        Me.Panel8.TabIndex = 1
        '
        'lblCodeCheckDown
        '
        Me.lblCodeCheckDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodeCheckDown.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCodeCheckDown.Location = New System.Drawing.Point(112, 16)
        Me.lblCodeCheckDown.Name = "lblCodeCheckDown"
        Me.lblCodeCheckDown.Size = New System.Drawing.Size(104, 24)
        Me.lblCodeCheckDown.TabIndex = 1
        Me.lblCodeCheckDown.Text = "Check Down"
        Me.lblCodeCheckDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Panel10)
        Me.Panel9.Controls.Add(Me.Label4)
        Me.Panel9.Location = New System.Drawing.Point(16, 272)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(224, 48)
        Me.Panel9.TabIndex = 6
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.LightGreen
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(72, 48)
        Me.Panel10.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.Location = New System.Drawing.Point(112, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 24)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Your Table"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCodeClose
        '
        Me.btnCodeClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCodeClose.Location = New System.Drawing.Point(120, 336)
        Me.btnCodeClose.Name = "btnCodeClose"
        Me.btnCodeClose.Size = New System.Drawing.Size(112, 48)
        Me.btnCodeClose.TabIndex = 8
        Me.btnCodeClose.Text = "Close"
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.LightSlateGray
        Me.Panel11.Controls.Add(Me.Panel2)
        Me.Panel11.Controls.Add(Me.Panel3)
        Me.Panel11.Controls.Add(Me.Panel5)
        Me.Panel11.Controls.Add(Me.Panel7)
        Me.Panel11.Controls.Add(Me.Panel9)
        Me.Panel11.Controls.Add(Me.btnCodeClose)
        Me.Panel11.Location = New System.Drawing.Point(8, 8)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(256, 400)
        Me.Panel11.TabIndex = 9
        '
        'Seating_ColorCode
        '
        Me.BackColor = System.Drawing.Color.SlateBlue
        Me.Controls.Add(Me.Panel11)
        Me.Name = "Seating_ColorCode"
        Me.Size = New System.Drawing.Size(272, 416)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnCodeClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCodeClose.Click

        Me.Dispose()

    End Sub
End Class
