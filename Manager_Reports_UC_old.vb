Imports System.io
Imports System.Drawing.Printing

Public Class Manager_Reports_UC
    Inherits System.Windows.Forms.UserControl

    Dim WithEvents pdoc As New PrintDocument
    '   Dim sql As New DataSet_Builder.SQLHelper(connectserver)

    Friend WithEvents crv As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Dim rptLocationSales As New Report_LocationSales
    Dim rptSalesByItem As New Report_SalesByItem


    Dim selectedDateRange As String

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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cbxLocationSalesDate As System.Windows.Forms.ComboBox
    Friend WithEvents cbxSelectReport As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrintReport As System.Windows.Forms.Button
    Friend WithEvents btnPrintPreviewReport As System.Windows.Forms.Button
    Friend WithEvents btnPageSetupReport As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button
        Me.cbxLocationSalesDate = New System.Windows.Forms.ComboBox
        Me.cbxSelectReport = New System.Windows.Forms.ComboBox
        Me.btnPrintReport = New System.Windows.Forms.Button
        Me.btnPrintPreviewReport = New System.Windows.Forms.Button
        Me.btnPageSetupReport = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(736, 16)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(112, 40)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Exit"
        '
        'cbxLocationSalesDate
        '
        Me.cbxLocationSalesDate.Location = New System.Drawing.Point(40, 24)
        Me.cbxLocationSalesDate.MaxDropDownItems = 12
        Me.cbxLocationSalesDate.Name = "cbxLocationSalesDate"
        Me.cbxLocationSalesDate.Size = New System.Drawing.Size(160, 21)
        Me.cbxLocationSalesDate.TabIndex = 7
        Me.cbxLocationSalesDate.Text = "Select Date Range"
        '
        'cbxSelectReport
        '
        Me.cbxSelectReport.Location = New System.Drawing.Point(560, 16)
        Me.cbxSelectReport.Name = "cbxSelectReport"
        Me.cbxSelectReport.Size = New System.Drawing.Size(152, 21)
        Me.cbxSelectReport.TabIndex = 8
        Me.cbxSelectReport.Text = "Select Report"
        '
        'btnPrintReport
        '
        Me.btnPrintReport.Location = New System.Drawing.Point(360, 16)
        Me.btnPrintReport.Name = "btnPrintReport"
        Me.btnPrintReport.TabIndex = 9
        Me.btnPrintReport.Text = "Print"
        '
        'btnPrintPreviewReport
        '
        Me.btnPrintPreviewReport.Location = New System.Drawing.Point(216, 16)
        Me.btnPrintPreviewReport.Name = "btnPrintPreviewReport"
        Me.btnPrintPreviewReport.Size = New System.Drawing.Size(104, 24)
        Me.btnPrintPreviewReport.TabIndex = 10
        Me.btnPrintPreviewReport.Text = "Print Preview"
        '
        'btnPageSetupReport
        '
        Me.btnPageSetupReport.Location = New System.Drawing.Point(216, 48)
        Me.btnPageSetupReport.Name = "btnPageSetupReport"
        Me.btnPageSetupReport.Size = New System.Drawing.Size(104, 24)
        Me.btnPageSetupReport.TabIndex = 11
        Me.btnPageSetupReport.Text = "Page Setup"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(472, 16)
        Me.Button2.Name = "Button2"
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "employee"
        '
        'Manager_Reports_UC
        '
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnPageSetupReport)
        Me.Controls.Add(Me.btnPrintPreviewReport)
        Me.Controls.Add(Me.btnPrintReport)
        Me.Controls.Add(Me.cbxSelectReport)
        Me.Controls.Add(Me.cbxLocationSalesDate)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Manager_Reports_UC"
        Me.Size = New System.Drawing.Size(912, 632)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub InitializeOther()

        PopulateComboBoxes()

        '
        'rptSalesByItem
        '
        Me.rptSalesByItem.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
        Me.rptSalesByItem.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        Me.rptSalesByItem.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Upper
        Me.rptSalesByItem.PrintOptions.PrinterDuplex = CrystalDecisions.Shared.PrinterDuplex.Default
        Me.rptSalesByItem.SetDataSource(dtOpenOrders)


        '
        'rptLocationSales
        '
        Me.rptLocationSales.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
        Me.rptLocationSales.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        Me.rptLocationSales.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Upper
        Me.rptLocationSales.PrintOptions.PrinterDuplex = CrystalDecisions.Shared.PrinterDuplex.Default

    End Sub

    Private Sub PopulateComboBoxes()

        '   cbx DataRanges
        Me.cbxLocationSalesDate.Items.Add("WeekToDateFromSun")
        Me.cbxLocationSalesDate.Items.Add("Last7Days")
        Me.cbxLocationSalesDate.Items.Add("LastFullWeek")
        Me.cbxLocationSalesDate.Items.Add("Last4WeeksToSun")
        Me.cbxLocationSalesDate.Items.Add("MonthToDate")
        Me.cbxLocationSalesDate.Items.Add("LastFullMonth")
        Me.cbxLocationSalesDate.Items.Add("YearToDate")
        Me.cbxLocationSalesDate.Items.Add("Calendar1stQrt")
        Me.cbxLocationSalesDate.Items.Add("Calendar2ndQrt")
        Me.cbxLocationSalesDate.Items.Add("Calendar3rdQrt")
        Me.cbxLocationSalesDate.Items.Add("Calendar4thQrt")


        Me.cbxSelectReport.Items.Add("Location Sales")
        Me.cbxSelectReport.Items.Add("Items Sold")


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Parent.Dispose()

    End Sub

    Private Sub cbxLocationSalesDate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxLocationSalesDate.SelectedIndexChanged

        selectedDateRange = "{ExperienceTable.ExperienceDate} in " & Me.cbxLocationSalesDate.SelectedItem.ToString



    End Sub

    Private Sub cbxSelectReport_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxSelectReport.SelectedIndexChanged
        Me.crv = New CrystalDecisions.Windows.Forms.CrystalReportViewer

        '
        'crv
        '
        Me.crv.ActiveViewIndex = -1
        Me.crv.Location = New System.Drawing.Point(24, 72)
        Me.crv.Name = "crv"
        '     Me.crv.ReportSource = "c:\Data Files\TahscPOS\term_Tahsc\Report_LocationSales.rpt"
        Me.crv.Size = New System.Drawing.Size(872, 520)
        Me.crv.TabIndex = 8


        If Me.cbxSelectReport.SelectedItem = "Location Sales" Then
            '       Me.crv.ReportSource = "c:\Data Files\TahscPOS\Reports\Report_LocationSales.rpt"
            '     Me.crv.ReportSource = "c:\Data Files\VisualStudioProjects\term_Tahsc\Report_LocationSales.rpt"
            crv.ReportSource = Me.rptLocationSales

        ElseIf Me.cbxSelectReport.SelectedItem = "Items Sold" Then
            '      Me.crv.ReportSource = "c:\Data Files\TahscPOS\Reports\Report_SalesByItem.rpt"
            '            Me.crv.ReportSource = "c:\Data Files\VisualStudioProjects\term_Tahsc\Report_SalesByItem.rpt"
            crv.ReportSource = Me.rptSalesByItem

        End If
        '     Me.crv.SelectionFormula = selectedDateRange

        Me.Controls.Add(Me.crv)

    End Sub





    Private Sub btnPrintReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintReport.Click

        crv.PrintReport()

        '      Dim dialog As New PrintDialog
        '     dialog.Document = pdoc
        '
        '       If dialog.ShowDialog = DialogResult.OK Then
        '      pdoc.Print()
        '     End If
    End Sub

    Private Sub btnPrintPreviewReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintPreviewReport.Click

        Dim ppd As PrintPreviewDialog
        Try
            ppd.Document = pdoc
            ppd.ShowDialog()

        Catch ex As Exception
            MessageBox.Show("An error has occurred.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnPageSetupReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageSetupReport.Click
           DetermineReportPrintString()


        Dim psd As New PageSetupDialog
        With psd
            .Document = pdoc
            .PageSettings = pdoc.DefaultPageSettings
        End With

        '      If psd.ShowDialog = DialogResult.OK Then
        '     pdoc.DefaultPageSettings = psd.PageSettings
        '   End If

    End Sub


    Private Sub pdoc_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdoc.PrintPage
        'Decalre variable to hold the position of the last printed char. Declare as STatic so the subsequent PrintPage events can reference it.
        Static intCurrentCar As Int32

        Dim font As New Font("Microsoft Sans Serif", 24)

        Dim intPrintAreaHeight As Int32
        Dim intPrintAreaWidth As Int32
        Dim marginLeft As Int32
        Dim marginTop As Int32

        With pdoc.DefaultPageSettings

            intPrintAreaHeight = .PaperSize.Height - .Margins.Top - .Margins.Bottom
            intPrintAreaWidth = .PaperSize.Width - .Margins.Top - .Margins.Bottom

            marginLeft = .Margins.Left
            marginTop = .Margins.Top

        End With

        If pdoc.DefaultPageSettings.Landscape Then
            Dim intTemp As Int32
            intTemp = intPrintAreaHeight
            intPrintAreaHeight = intPrintAreaWidth
            intPrintAreaWidth = intTemp
        End If



    End Sub


    Private Function DetermineReportPrintString()

        '     Dim sWriter1 As StreamWriter = New StreamWriter("c:\Data Files\TahscPOS\Reports\Report1.txt")

        Dim yesterdaysDate As Date

        yesterdaysDate = Format(Today.AddDays(-7), "D")

        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        sql.SqlSelectCommandReportItemsSold.Parameters("@ExperienceDate").Value = yesterdaysDate
        sql.SqlDataAdapterReportItemsSold.Fill(dsReport.Tables("Report_ItemsSold"))
        sql.cn.Close()

        Dim oRow As DataRow
        Dim lstReport As New ListView
        lstReport.View = View.Details

        Dim clmCheckTotalName = New System.Windows.Forms.ColumnHeader
        Dim clmCheckTotalAmount = New System.Windows.Forms.ColumnHeader
        lstReport.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {clmCheckTotalName, clmCheckTotalAmount})
        lstReport.Location = New Point(200, 200)
        lstReport.Size = New Size(200, 400)
        Me.Controls.Add(lstReport)

        MsgBox(dsReport.Tables("Report_ItemsSold").Rows.Count)

        For Each oRow In dsReport.Tables("Report_ItemsSold").Rows
            Dim itemReport As New ListViewItem
            itemReport.Text = oRow("FunctionName").ToString
            itemReport.SubItems.Add(oRow("FoodName").ToString)
            '        itemReport.SubItems.Add(EXPR7)
            lstReport.Items.Add(itemReport)

        Next



    End Function



    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Parent.Dispose()

    End Sub

End Class
