Public Class CashClose_UC
    Inherits System.Windows.Forms.UserControl

    Dim numOfItems As Integer
    Dim salesNumber As Integer
    Dim cashDue As Decimal
    Dim cashPaid As Decimal
    Dim change As Decimal

    Dim WithEvents pnlNoSale As Label

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal _numOfItems As Integer, ByVal _salesNumber As Integer, ByVal _cashDue As Decimal, ByVal _cashPaid As Decimal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        numOfItems = _numOfItems
        salesNumber = _salesNumber
        cashDue = Format(_cashDue, "###,###.00")
        cashPaid = Format(_cashPaid, "###,###.00")
        change = Format(cashPaid - cashDue, "###,###.00")

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblItemsOrdered As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblChange As System.Windows.Forms.Label
    Friend WithEvents lblCashPaid As System.Windows.Forms.Label
    Friend WithEvents lblCashDue As System.Windows.Forms.Label
    Friend WithEvents lblSalesNum As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblSalesNum = New System.Windows.Forms.Label
        Me.lblCashDue = New System.Windows.Forms.Label
        Me.lblCashPaid = New System.Windows.Forms.Label
        Me.lblChange = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblItemsOrdered = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightGray
        Me.Panel1.Controls.Add(Me.lblSalesNum)
        Me.Panel1.Controls.Add(Me.lblCashDue)
        Me.Panel1.Controls.Add(Me.lblCashPaid)
        Me.Panel1.Controls.Add(Me.lblChange)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(8, 104)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(632, 344)
        Me.Panel1.TabIndex = 0
        '
        'lblSalesNum
        '
        Me.lblSalesNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesNum.Location = New System.Drawing.Point(336, 24)
        Me.lblSalesNum.Name = "lblSalesNum"
        Me.lblSalesNum.Size = New System.Drawing.Size(224, 56)
        Me.lblSalesNum.TabIndex = 9
        Me.lblSalesNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCashDue
        '
        Me.lblCashDue.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCashDue.Location = New System.Drawing.Point(336, 96)
        Me.lblCashDue.Name = "lblCashDue"
        Me.lblCashDue.Size = New System.Drawing.Size(224, 56)
        Me.lblCashDue.TabIndex = 8
        Me.lblCashDue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCashPaid
        '
        Me.lblCashPaid.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCashPaid.Location = New System.Drawing.Point(336, 168)
        Me.lblCashPaid.Name = "lblCashPaid"
        Me.lblCashPaid.Size = New System.Drawing.Size(224, 56)
        Me.lblCashPaid.TabIndex = 7
        Me.lblCashPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblChange
        '
        Me.lblChange.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChange.ForeColor = System.Drawing.Color.MediumSeaGreen
        Me.lblChange.Location = New System.Drawing.Point(336, 264)
        Me.lblChange.Name = "lblChange"
        Me.lblChange.Size = New System.Drawing.Size(224, 56)
        Me.lblChange.TabIndex = 6
        Me.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.MediumSeaGreen
        Me.Label6.Location = New System.Drawing.Point(16, 264)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(264, 56)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Change:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(24, 232)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(584, 8)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Label5"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(56, 168)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(224, 56)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Cash Paid:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(56, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(224, 56)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Cash Due:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(56, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(224, 56)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Sales #:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblItemsOrdered
        '
        Me.lblItemsOrdered.BackColor = System.Drawing.Color.Black
        Me.lblItemsOrdered.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemsOrdered.ForeColor = System.Drawing.Color.White
        Me.lblItemsOrdered.Location = New System.Drawing.Point(8, 8)
        Me.lblItemsOrdered.Name = "lblItemsOrdered"
        Me.lblItemsOrdered.Size = New System.Drawing.Size(632, 96)
        Me.lblItemsOrdered.TabIndex = 0
        Me.lblItemsOrdered.Text = "0 Items Ordered"
        Me.lblItemsOrdered.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CashClose_UC
        '
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblItemsOrdered)
        Me.Name = "CashClose_UC"
        Me.Size = New System.Drawing.Size(648, 456)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub InitializeOther()

        Me.lblItemsOrdered.Text = numOfItems.ToString & " Items Ordered"
        Me.lblSalesNum.Text = salesNumber
        Me.lblCashDue.Text = cashDue
        Me.lblCashPaid.Text = cashPaid
        Me.lblChange.Text = change

        If change >= 0 Then
            Me.Label6.Text = "Change:"
        Else
            Me.Label6.Text = "Balance Due:"
            Me.Label6.ForeColor = c9    'Color.Red
        End If

    End Sub

    Public Sub BeginNoSale()

        Me.Panel1.Visible = False
        pnlNoSale = New Label

        Me.pnlNoSale.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlNoSale.Location = New System.Drawing.Point(0, 200)
        Me.pnlNoSale.Size = New System.Drawing.Size(450, 100)
        Me.pnlNoSale.Text = "No Sale"
        Me.pnlNoSale.TextAlign = System.Drawing.ContentAlignment.MiddleRight

        Me.Controls.Add(pnlNoSale)

    End Sub
    Private Sub lblItemsOrdered_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click, Label2.Click, Label3.Click, Label4.Click, Label5.Click, Label6.Click, lblCashDue.Click, lblCashPaid.Click, lblChange.Click, lblItemsOrdered.Click, lblSalesNum.Click, Panel1.Click, pnlNoSale.Click

        Me.Dispose()

    End Sub
End Class
