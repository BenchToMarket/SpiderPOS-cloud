Imports DataSet_Builder

Public Class SplitItemUserControl
    Inherits System.Windows.Forms.UserControl

    Dim splitRowNumber As Integer
    Dim splitColNumber As Integer

    Friend checkArray(1) As SplittingCheck
    Dim splittingQuantity As Integer

    Dim totalValue As Decimal
    Dim itemAmountTotal As Decimal
    Dim remainingTotal As Decimal

    Dim remainingQuantity As Integer

    Event ApplySplitCheck(ByVal sender As Object, ByVal e As System.EventArgs)


#Region " Windows Form Designer generated code "

    Public Sub New(ByRef checksSplitting() As SplittingCheck, ByVal price As Decimal, ByVal sq As Integer, ByVal numberOfChecks As Integer)
        MyBase.New()

        itemAmountTotal = price
        ReDim checkArray(numberOfChecks - 1)

        Array.Copy(checksSplitting, checkArray, numberOfChecks)
        splittingQuantity = sq


        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.NumberPadMediumSplit.DecimalUsed = True

        'Add any initialization after the InitializeComponent() call
        InitializeOther(checkArray, numberOfChecks)



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
    Friend WithEvents txtRemainingSplit As System.Windows.Forms.TextBox
    Friend WithEvents lblRemainingSplit As System.Windows.Forms.Label
    Friend WithEvents btnApplySplit As System.Windows.Forms.Button
    Friend WithEvents grdSplitItem As System.Windows.Forms.DataGrid
    Friend WithEvents splitItemLabel As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents NumberPadMediumSplit As DataSet_Builder.NumberPadMedium
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.NumberPadMediumSplit = New DataSet_Builder.NumberPadMedium
        Me.btnCancel = New System.Windows.Forms.Button
        Me.splitItemLabel = New System.Windows.Forms.Label
        Me.grdSplitItem = New System.Windows.Forms.DataGrid
        Me.btnApplySplit = New System.Windows.Forms.Button
        Me.lblRemainingSplit = New System.Windows.Forms.Label
        Me.txtRemainingSplit = New System.Windows.Forms.TextBox
        Me.Panel1.SuspendLayout()
        CType(Me.grdSplitItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Panel1.Controls.Add(Me.NumberPadMediumSplit)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.splitItemLabel)
        Me.Panel1.Controls.Add(Me.grdSplitItem)
        Me.Panel1.Controls.Add(Me.btnApplySplit)
        Me.Panel1.Controls.Add(Me.lblRemainingSplit)
        Me.Panel1.Controls.Add(Me.txtRemainingSplit)
        Me.Panel1.Location = New System.Drawing.Point(16, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(480, 384)
        Me.Panel1.TabIndex = 6
        '
        'NumberPadMediumSplit
        '
        Me.NumberPadMediumSplit.BackColor = System.Drawing.Color.SlateGray
        Me.NumberPadMediumSplit.DecimalUsed = True
        Me.NumberPadMediumSplit.ForeColor = System.Drawing.Color.Black
        Me.NumberPadMediumSplit.IntegerNumber = 0
        Me.NumberPadMediumSplit.Location = New System.Drawing.Point(272, 32)
        Me.NumberPadMediumSplit.Name = "NumberPadMediumSplit"
        Me.NumberPadMediumSplit.NumberString = ""
        Me.NumberPadMediumSplit.NumberTotal = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumberPadMediumSplit.Size = New System.Drawing.Size(192, 296)
        Me.NumberPadMediumSplit.TabIndex = 11
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.LightSlateGray
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(272, 344)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(64, 24)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        '
        'splitItemLabel
        '
        Me.splitItemLabel.BackColor = System.Drawing.Color.FromArgb(CType(59, Byte), CType(96, Byte), CType(141, Byte))
        Me.splitItemLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.splitItemLabel.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.splitItemLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.splitItemLabel.Location = New System.Drawing.Point(0, 0)
        Me.splitItemLabel.Name = "splitItemLabel"
        Me.splitItemLabel.Size = New System.Drawing.Size(480, 24)
        Me.splitItemLabel.TabIndex = 9
        Me.splitItemLabel.Text = "Splitting Item: "
        Me.splitItemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grdSplitItem
        '
        Me.grdSplitItem.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grdSplitItem.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSplitItem.CaptionVisible = False
        Me.grdSplitItem.DataMember = ""
        Me.grdSplitItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSplitItem.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdSplitItem.Location = New System.Drawing.Point(16, 32)
        Me.grdSplitItem.Name = "grdSplitItem"
        Me.grdSplitItem.RowHeadersVisible = False
        Me.grdSplitItem.Size = New System.Drawing.Size(224, 264)
        Me.grdSplitItem.TabIndex = 8
        '
        'btnApplySplit
        '
        Me.btnApplySplit.BackColor = System.Drawing.Color.Red
        Me.btnApplySplit.Location = New System.Drawing.Point(368, 336)
        Me.btnApplySplit.Name = "btnApplySplit"
        Me.btnApplySplit.Size = New System.Drawing.Size(88, 40)
        Me.btnApplySplit.TabIndex = 7
        Me.btnApplySplit.Text = "Apply"
        '
        'lblRemainingSplit
        '
        Me.lblRemainingSplit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemainingSplit.ForeColor = System.Drawing.Color.FromArgb(CType(59, Byte), CType(96, Byte), CType(141, Byte))
        Me.lblRemainingSplit.Location = New System.Drawing.Point(24, 320)
        Me.lblRemainingSplit.Name = "lblRemainingSplit"
        Me.lblRemainingSplit.Size = New System.Drawing.Size(112, 16)
        Me.lblRemainingSplit.TabIndex = 6
        Me.lblRemainingSplit.Text = "Remaining:"
        Me.lblRemainingSplit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRemainingSplit
        '
        Me.txtRemainingSplit.Location = New System.Drawing.Point(136, 320)
        Me.txtRemainingSplit.Name = "txtRemainingSplit"
        Me.txtRemainingSplit.Size = New System.Drawing.Size(72, 20)
        Me.txtRemainingSplit.TabIndex = 5
        Me.txtRemainingSplit.Text = ""
        '
        'SplitItemUserControl
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(59, Byte), CType(96, Byte), CType(141, Byte))
        Me.Controls.Add(Me.Panel1)
        Me.ForeColor = System.Drawing.Color.White
        Me.Name = "SplitItemUserControl"
        Me.Size = New System.Drawing.Size(512, 408)
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdSplitItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub InitializeOther(ByRef checkarray() As SplittingCheck, ByVal numberOfChecks As Integer)
        If splittingQuantity > 1 Then
            Me.NumberPadMediumSplit.DisplayIntegerNumber = True
            remainingQuantity = DetermineRemainingQuantity()
        End If


        Dim roundingError As Decimal

        '       CreateCheckCollection(checksSplitting, numberOfChecks)

        roundingError = DetermineRemainingValue()

        If roundingError <> 0 Then
            Dim checkSplit As SplittingCheck
            For Each checkSplit In checkarray   'CurrentSplittingChecks
                '   add to first one only in both array and collection
                '               checkSplit.CheckAmount += roundingError
                '   this will always be the first position
                checkarray(0).CheckAmount += roundingError
                roundingError = 0
                Exit For
            Next
        End If

        DisplayRemainingValue()


        Me.grdSplitItem.DataSource = checkarray '   CurrentSplittingChecks '

        Dim tsCheckSplit As New DataGridTableStyle
        tsCheckSplit.MappingName = "SplittingCheck[]" '  "SplittingCheckCollection[]" '
        tsCheckSplit.RowHeadersVisible = False
        tsCheckSplit.AllowSorting = False
        tsCheckSplit.GridLineColor = Color.White

        Dim csCheckName As New DataGridTextBoxColumn
        csCheckName.MappingName = "CheckNumber"
        csCheckName.HeaderText = "Check #"
        csCheckName.Alignment = HorizontalAlignment.Center
        csCheckName.Width = 100

        Dim csCheckAmount As New DataGridTextBoxColumn
        csCheckAmount.Alignment = HorizontalAlignment.Center
        csCheckAmount.MappingName = "CheckAmount"
        If splittingQuantity > 1 Then
            csCheckAmount.Width = 0
        Else
            csCheckAmount.HeaderText = "Amount"
            csCheckAmount.Width = grdSplitItem.Width - 105
        End If

        Dim csCheckQuantity As New DataGridTextBoxColumn
        csCheckQuantity.MappingName = "CheckQuantity"
        csCheckQuantity.Alignment = HorizontalAlignment.Center
        If splittingQuantity > 1 Then
            csCheckQuantity.HeaderText = "Quantity"
            csCheckQuantity.Width = grdSplitItem.Width - 105
        Else
            csCheckQuantity.Width = 0
        End If

        '       Dim csBlank As New DataGridTextBoxColumn
        '      csBlank.Width = 30


        tsCheckSplit.GridColumnStyles.Add(csCheckName)
        tsCheckSplit.GridColumnStyles.Add(csCheckAmount)
        tsCheckSplit.GridColumnStyles.Add(csCheckQuantity)
        '     tsCheckSplit.GridColumnStyles.Add(csBlank)
        Me.grdSplitItem.TableStyles.Add(tsCheckSplit)

        '   this is for quantity change
        totalValue = DetermineTotalValue()

    End Sub


    Private Sub SplitItemGrid_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSplitItem.CurrentCellChanged

        '       If Me.NumberPadMediumSplit.changesMade = True Then
        '      AdjustingSplitAmount()
        '     End If

        splitRowNumber = Me.grdSplitItem.CurrentCell.RowNumber
        splitColNumber = Me.grdSplitItem.CurrentCell.ColumnNumber


        If splitColNumber = 1 Or splitColNumber = 2 Then
            If splittingQuantity > 1 Then
                Me.NumberPadMediumSplit.NumberTotal = Me.grdSplitItem(splitRowNumber, splitColNumber)
            Else
                Me.NumberPadMediumSplit.NumberTotal = Me.grdSplitItem(splitRowNumber, splitColNumber)
            End If
            Me.NumberPadMediumSplit.ShowNumberString()
            Me.NumberPadMediumSplit.Focus()
        End If


    End Sub


    Private Sub AdjustSplitAmount(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumberPadMediumSplit.NumberEntered

        AdjustingSplitAmount()

    End Sub

    Private Sub AdjustingSplitAmount()
        Me.NumberPadMediumSplit.changesMade = False

        If splitColNumber = 1 Or splitColNumber = 2 Then
            If splittingQuantity > 1 Then
                grdSplitItem(splitRowNumber, splitColNumber) = CType(Format(Me.NumberPadMediumSplit.IntegerNumber, "####0"), Integer)
                'next line adjust price for quantity change
                grdSplitItem(splitRowNumber, 1) = CType(Format(((totalValue / splittingQuantity) * Me.NumberPadMediumSplit.IntegerNumber), "####0.00"), Decimal)

                remainingQuantity = DetermineRemainingQuantity()
            Else
                grdSplitItem(splitRowNumber, splitColNumber) = CType(Format(Me.NumberPadMediumSplit.NumberTotal, "####0.00"), Decimal)
                remainingTotal = DetermineRemainingValue()
            End If
        End If

        DisplayRemainingValue()
        Me.NumberPadMediumSplit.ResetValues()

    End Sub

    Private Function DetermineTotalValue()
        Dim amountAccountedFor As Decimal
        Dim check As SplittingCheck

        For Each check In checkArray
            amountAccountedFor += Format(check.CheckAmount, "####0.00")
        Next

        Return amountAccountedFor

    End Function

    Private Function DetermineRemainingValue() 'ByVal totalPrice As Decimal, ByVal eachAmount As Decimal, ByVal number As Integer)
        Dim remaining As Decimal
        Dim amountAccountedFor As Decimal
        Dim check As SplittingCheck

        For Each check In checkArray
            amountAccountedFor += Format(check.CheckAmount, "####0.00")
        Next

        remaining = Format((itemAmountTotal - (amountAccountedFor)), "####0.00")

        '   don't need return now
        Return remaining

    End Function

    Private Function DetermineRemainingQuantity()
        Dim remaining As Integer
        Dim quantityAccountedFor As Integer
        Dim check As SplittingCheck

        For Each check In checkArray
            quantityAccountedFor += Format(check.CheckQuantity, "###0")
        Next

        remaining = Format((splittingQuantity - (quantityAccountedFor)), "###0")

        Return remaining


    End Function



    Private Sub DisplayRemainingValue()

        If splittingQuantity > 1 Then
            txtRemainingSplit.Text = remainingQuantity
        Else
            txtRemainingSplit.Text = remainingTotal
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()

    End Sub



    Private Sub btnApplySplit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplySplit.Click

        If splittingQuantity > 1 Then
            If remainingQuantity > 0 Then
                MsgBox("You must add the remaining " & remainingQuantity & " item(s) to at least one of the checks.")
                Exit Sub
            ElseIf remainingQuantity < 0 Then
                MsgBox("You must deduct the remaining " & -remainingQuantity & " item(s) from at least one of the checks.")
                Exit Sub
            End If
        Else
            If remainingTotal > 0 Then
                MsgBox("You must add the remaining $ " & remainingTotal & " to at least one of the checks.")
                Exit Sub
            ElseIf remainingTotal < 0 Then
                MsgBox("You must deduct the remaining $ " & -remainingTotal & " from at least one of the checks.")
                Exit Sub
            End If
        End If


        RaiseEvent ApplySplitCheck(sender, e)
        Me.Dispose()

    End Sub

End Class
