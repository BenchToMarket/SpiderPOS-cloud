<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Training_UC
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.grdPreviousDailys = New System.Windows.Forms.DataGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnDeleteTraining = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.DataGridTableStyleTraining1 = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumnTraining1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.DataGridTextBoxColumnTraining0 = New System.Windows.Forms.DataGridTextBoxColumn
        CType(Me.grdPreviousDailys, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdPreviousDailys
        '
        Me.grdPreviousDailys.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton
        Me.grdPreviousDailys.AllowSorting = False
        Me.grdPreviousDailys.BackgroundColor = System.Drawing.Color.White
        Me.grdPreviousDailys.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.grdPreviousDailys.CaptionText = "Training Dailys"
        Me.grdPreviousDailys.ColumnHeadersVisible = False
        Me.grdPreviousDailys.DataMember = ""
        Me.grdPreviousDailys.GridLineColor = System.Drawing.Color.White
        Me.grdPreviousDailys.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdPreviousDailys.Location = New System.Drawing.Point(19, 14)
        Me.grdPreviousDailys.Name = "grdPreviousDailys"
        Me.grdPreviousDailys.RowHeadersVisible = False
        Me.grdPreviousDailys.Size = New System.Drawing.Size(325, 139)
        Me.grdPreviousDailys.TabIndex = 12
        Me.grdPreviousDailys.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyleTraining1})
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnDeleteTraining)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Location = New System.Drawing.Point(67, 275)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(234, 64)
        Me.Panel1.TabIndex = 15
        '
        'btnDeleteTraining
        '
        Me.btnDeleteTraining.BackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btnDeleteTraining.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteTraining.Location = New System.Drawing.Point(16, 5)
        Me.btnDeleteTraining.Name = "btnDeleteTraining"
        Me.btnDeleteTraining.Size = New System.Drawing.Size(92, 48)
        Me.btnDeleteTraining.TabIndex = 8
        Me.btnDeleteTraining.Text = "Delete Training"
        Me.btnDeleteTraining.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(124, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(92, 48)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'DataGridTableStyleTraining1
        '
        Me.DataGridTableStyleTraining1.DataGrid = Me.grdPreviousDailys
        Me.DataGridTableStyleTraining1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumnTraining0, Me.DataGridTextBoxColumnTraining1})
        Me.DataGridTableStyleTraining1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyleTraining1.MappingName = "TrainingDaily"
        Me.DataGridTableStyleTraining1.RowHeadersVisible = False
        '
        'DataGridTextBoxColumnTraining1
        '
        Me.DataGridTextBoxColumnTraining1.Format = "MM/dd/yyyy"
        Me.DataGridTextBoxColumnTraining1.FormatInfo = Nothing
        Me.DataGridTextBoxColumnTraining1.MappingName = "StartTime"
        Me.DataGridTextBoxColumnTraining1.NullText = " "
        Me.DataGridTextBoxColumnTraining1.ReadOnly = True
        Me.DataGridTextBoxColumnTraining1.Width = 150
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Black
        Me.Panel2.Controls.Add(Me.grdPreviousDailys)
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Location = New System.Drawing.Point(25, 27)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(360, 368)
        Me.Panel2.TabIndex = 16
        '
        'DataGridTextBoxColumnTraining0
        '
        Me.DataGridTextBoxColumnTraining0.Format = ""
        Me.DataGridTextBoxColumnTraining0.FormatInfo = Nothing
        Me.DataGridTextBoxColumnTraining0.MappingName = "ActiveDaily"
        Me.DataGridTextBoxColumnTraining0.Width = 50
        '  Me.DataGridTextBoxColumnTraining0.
        '
        'Training_UC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Controls.Add(Me.Panel2)
        Me.Name = "Training_UC"
        Me.Size = New System.Drawing.Size(409, 424)
        CType(Me.grdPreviousDailys, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdPreviousDailys As System.Windows.Forms.DataGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnDeleteTraining As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DataGridTableStyleTraining1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumnTraining1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents DataGridTextBoxColumnTraining0 As System.Windows.Forms.DataGridTextBoxColumn

End Class
