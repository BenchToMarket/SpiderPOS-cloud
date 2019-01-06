Imports DataSet_Builder

Public Class ForcePrice_UC
    Inherits System.Windows.Forms.UserControl

    Dim splitRowNumber As Integer

    Dim begAdjustmentAmount As Decimal
    '    Dim totalAdjustmentAmount As Decimal
    '    Dim amountForcedAdj As Decimal

    Event UpdateAdjGrid(ByVal newAdjAmount As Decimal, ByVal discountedAmount As Decimal)
    '   Event UpdateForcePrice()
    Event DisposeForcePrice()


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal firstValue As Decimal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        InitializeOther()
        begAdjustmentAmount = firstValue
        Me.lblTotalAmount.Text = Format(firstValue, "$ ##,###.00")

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblAdjustmentCredit As System.Windows.Forms.Label
    Friend WithEvents grdForcePrice As System.Windows.Forms.DataGrid
    Friend WithEvents txtForceAdjustment As System.Windows.Forms.TextBox
    Friend WithEvents NumberPadSmallForce As DataSet_Builder.NumberPadMedium
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblTotalAmount As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnCancel = New System.Windows.Forms.Button
        Me.NumberPadSmallForce = New DataSet_Builder.NumberPadMedium
        Me.grdForcePrice = New System.Windows.Forms.DataGrid
        Me.lblAdjustmentCredit = New System.Windows.Forms.Label
        Me.txtForceAdjustment = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblTotalAmount = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.grdForcePrice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.lblTotalAmount)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.NumberPadSmallForce)
        Me.Panel1.Controls.Add(Me.grdForcePrice)
        Me.Panel1.Controls.Add(Me.lblAdjustmentCredit)
        Me.Panel1.Controls.Add(Me.txtForceAdjustment)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(472, 296)
        Me.Panel1.TabIndex = 16
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(16, 256)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 23)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Close"
        '
        'NumberPadSmallForce
        '
        Me.NumberPadSmallForce.BackColor = System.Drawing.Color.SlateGray
        Me.NumberPadSmallForce.DecimalUsed = False
        Me.NumberPadSmallForce.IntegerNumber = 0
        Me.NumberPadSmallForce.Location = New System.Drawing.Point(280, 0)
        Me.NumberPadSmallForce.Name = "NumberPadSmallForce"
        Me.NumberPadSmallForce.NumberString = Nothing
        Me.NumberPadSmallForce.NumberTotal = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumberPadSmallForce.Size = New System.Drawing.Size(192, 296)
        Me.NumberPadSmallForce.TabIndex = 9
        '
        'grdForcePrice
        '
        Me.grdForcePrice.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grdForcePrice.CaptionVisible = False
        Me.grdForcePrice.DataMember = ""
        Me.grdForcePrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdForcePrice.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdForcePrice.Location = New System.Drawing.Point(8, 8)
        Me.grdForcePrice.Name = "grdForcePrice"
        Me.grdForcePrice.RowHeadersVisible = False
        Me.grdForcePrice.Size = New System.Drawing.Size(264, 208)
        Me.grdForcePrice.TabIndex = 8
        '
        'lblAdjustmentCredit
        '
        Me.lblAdjustmentCredit.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblAdjustmentCredit.Location = New System.Drawing.Point(112, 264)
        Me.lblAdjustmentCredit.Name = "lblAdjustmentCredit"
        Me.lblAdjustmentCredit.Size = New System.Drawing.Size(64, 16)
        Me.lblAdjustmentCredit.TabIndex = 6
        Me.lblAdjustmentCredit.Text = "Adjustment:"
        Me.lblAdjustmentCredit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtForceAdjustment
        '
        Me.txtForceAdjustment.Location = New System.Drawing.Point(176, 264)
        Me.txtForceAdjustment.Name = "txtForceAdjustment"
        Me.txtForceAdjustment.Size = New System.Drawing.Size(72, 20)
        Me.txtForceAdjustment.TabIndex = 5
        Me.txtForceAdjustment.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(72, 232)
        Me.Label1.Name = "Label1"
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Amount Total:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.Location = New System.Drawing.Point(184, 232)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(72, 23)
        Me.lblTotalAmount.TabIndex = 12
        Me.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ForcePrice_UC
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ForcePrice_UC"
        Me.Size = New System.Drawing.Size(472, 296)
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdForcePrice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther()

        Me.NumberPadSmallForce.DecimalUsed = True
        DisplayForcePriceDataGrid()

    End Sub

    Private Sub DisplayForcePriceDataGrid()

        Me.grdForcePrice.DataSource = dvForcePrice

        Dim tsForcePrice As New DataGridTableStyle
        tsForcePrice.MappingName = "OpenOrders"
        tsForcePrice.RowHeadersVisible = False
        tsForcePrice.GridLineColor = Color.White

        Dim csForceSIN As New DataGridTextBoxColumn
        csForceSIN.MappingName = "sin"
        csForceSIN.Width = 0

        Dim csForceItemName As New DataGridTextBoxColumn
        csForceItemName.MappingName = "TerminalName"
        csForceItemName.HeaderText = "Item Name"
        csForceItemName.Alignment = HorizontalAlignment.Left
        csForceItemName.Width = 180

        Dim csForcePrice As New DataGridTextBoxColumn
        csForcePrice.MappingName = "Price"
        csForcePrice.HeaderText = "Price"
        csForcePrice.Alignment = HorizontalAlignment.Right
        csForcePrice.Width = 60

        Dim csBlank As New DataGridTextBoxColumn
        csBlank.HeaderText = " "
        csBlank.Width = 20

        tsForcePrice.GridColumnStyles.Add(csForceSIN)
        tsForcePrice.GridColumnStyles.Add(csForceItemName)
        tsForcePrice.GridColumnStyles.Add(csForcePrice)
        tsForcePrice.GridColumnStyles.Add(csBlank)
        Me.grdForcePrice.TableStyles.Add(tsForcePrice)


    End Sub


    Private Sub ForcePriceGrid_Selected() '(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdForcePrice.CurrentCellChanged

        '      splitRowNumber = Me.grdForcePrice.CurrentCell.RowNumber
        '     Me.NumberPadSmallForce.NumberTotal = Me.grdForcePrice(splitRowNumber, 2)
        '    Me.NumberPadSmallForce.ShowNumberString()

        '      Me.NumberPadSmallForce.Focus()
        '     Me.NumberPadSmallForce.IntegerNumber = 0
        '    Me.NumberPadSmallForce.NumberString = Nothing
        Me.NumberPadSmallForce.ResetValues()
        Me.NumberPadSmallForce.ShowNumberString()

        '    begAdjustmentAmount = (Format(Me.NumberPadSmallForce.NumberTotal, "####0.00"))
        '     begAdjustmentAmount = (Format(Me.grdForcePrice(splitRowNumber, 2), "####0.00"))

    End Sub

    Private Sub AdjustForcePriceAmount(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumberPadSmallForce.NumberEntered

        Dim newAdjAmount As Decimal
        Dim discountedAmount As Decimal

        discountedAmount = (Format(Me.NumberPadSmallForce.NumberTotal, "####0.00"))

        '      grdForcePrice(splitRowNumber, 2) = adjAmount

        newAdjAmount = (begAdjustmentAmount - discountedAmount)
        If newAdjAmount < 0 Then
            MsgBox("You can not Credit more than the total amount: " & begAdjustmentAmount)
            ForcePriceGrid_Selected()
            Exit Sub
        End If
        '     totalAdjustmentAmount = adjAmount

        DisplayAdjustmentAmount(newAdjAmount * -1)
        '   ApplyForcePrice()
        RaiseEvent UpdateAdjGrid(newAdjAmount, discountedAmount)

    End Sub

    Private Sub DisplayAdjustmentAmount(ByVal adjAmount As Decimal)

        Me.txtForceAdjustment.Text = adjAmount  'totalAdjustmentAmount

    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        RaiseEvent DisposeForcePrice()

    End Sub
End Class
