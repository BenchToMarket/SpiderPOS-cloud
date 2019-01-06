Public Class PaymentUserControl
    Inherits System.Windows.Forms.UserControl
    '********************************
    '   do not use anymore
    '   was for closing out check


    Private _userControlChangingAmount As Decimal

    Event ChangeClosingAmount(ByVal sender As Object, ByVal e As System.EventArgs)

    Friend Property UserControlChangingAmount() As Decimal
        Get
            Return _userControlChangingAmount
        End Get
        Set(ByVal Value As Decimal)
            _userControlChangingAmount = Value
        End Set
    End Property

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
    Friend WithEvents txtPaymentType As System.Windows.Forms.TextBox
    Friend WithEvents btnAmount As System.Windows.Forms.Button
    Friend WithEvents btnTip As System.Windows.Forms.Button
    Friend WithEvents btnTipAdjustment As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtPaymentType = New System.Windows.Forms.TextBox
        Me.btnAmount = New System.Windows.Forms.Button
        Me.btnTip = New System.Windows.Forms.Button
        Me.btnTipAdjustment = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'txtPaymentType
        '
        Me.txtPaymentType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPaymentType.Location = New System.Drawing.Point(0, 0)
        Me.txtPaymentType.Name = "txtPaymentType"
        Me.txtPaymentType.Size = New System.Drawing.Size(112, 13)
        Me.txtPaymentType.TabIndex = 0
        Me.txtPaymentType.Text = ""
        '
        'btnAmount
        '
        Me.btnAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAmount.Location = New System.Drawing.Point(112, 0)
        Me.btnAmount.Name = "btnAmount"
        Me.btnAmount.Size = New System.Drawing.Size(72, 24)
        Me.btnAmount.TabIndex = 1
        Me.btnAmount.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'btnTip
        '
        Me.btnTip.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTip.Location = New System.Drawing.Point(184, 0)
        Me.btnTip.Name = "btnTip"
        Me.btnTip.Size = New System.Drawing.Size(48, 24)
        Me.btnTip.TabIndex = 2
        Me.btnTip.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'btnTipAdjustment
        '
        Me.btnTipAdjustment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTipAdjustment.Location = New System.Drawing.Point(232, 0)
        Me.btnTipAdjustment.Name = "btnTipAdjustment"
        Me.btnTipAdjustment.Size = New System.Drawing.Size(48, 24)
        Me.btnTipAdjustment.TabIndex = 3
        Me.btnTipAdjustment.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'PaymentUserControl
        '
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Controls.Add(Me.btnTipAdjustment)
        Me.Controls.Add(Me.btnTip)
        Me.Controls.Add(Me.btnAmount)
        Me.Controls.Add(Me.txtPaymentType)
        Me.Name = "PaymentUserControl"
        Me.Size = New System.Drawing.Size(280, 24)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub btnAmount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAmount.Click

        UserControlChangingAmount = CType(btnAmount.Text, Decimal)


        RaiseEvent ChangeClosingAmount(sender, e)

    End Sub

    '  Private Sub myBase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Click

    ' End Sub

End Class
