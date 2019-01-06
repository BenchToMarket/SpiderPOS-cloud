Imports DataSet_Builder
Imports System
Imports System.Xml.Serialization

Imports System.IO
Imports System.Xml
Imports System.Text




Module GenerateOrderTables

    Public WithEvents sql As New DataSet_Builder.SQLHelper
    '   Public WithEvents sql222 As New DataSet_Builder.OldSqlHelper    'just so we don't have to delete some code (is old)

    Dim dsi As DSICLIENTXLib.DSICLientX

    Friend updateClockTimer As Timer
    Friend tablesInactiveTimer As Timer
    Friend orderInactiveTimer As Timer
    Friend splitInactiveTimer As Timer
    '    Friend closeInactiveTimer As Timer
    Friend tmrCardRead As Timer

    Friend OpenOrdersCurrencyMan As CurrencyManager


    Friend dvClosingCheck As DataView
    '    Friend dvClosingCheckApplyOrder As DataView
    Friend dvUnAppliedPaymentsAndCredits As DataView
    Friend dvAppliedPayments As DataView
    Friend dvPaymentsAndCredits As DataView
    Friend dvForcePrice As DataView
    Friend dvClosedTables As DataView
    Friend dvClosedTabs As DataView
    Friend dvClosingCheckPayments As DataView
    Friend dvClosedPreAuth As DataView
    Friend dvBatchNotCaptured As DataView
    Friend dvCloseCheck As New DataView
    Public dvUnAppliedPaymentsAndCredits_MWE As DataView

    Friend dvBatchPreAuth As DataView
    Friend dvBatchPreAuthCapture As DataView
    Friend dvBatchVoiceAuth As DataView
    Friend dvBatchReturn As DataView
    Friend dvIngredients As New DataView
    Friend dvIngredientsNO As New DataView
    Friend dvIngredientsEXTRA As New DataView
  
    Friend ds As DataSet = New DataSet("Menu")
    Friend dsStarter As DataSet = New DataSet("StarterMenu")


    ' we have this in both here and GenerateReportData
    ' this is because we want to access this from Setup
    Public dsInventory As DataSet = New DataSet("InventoryData")
    '    Friend dtRawCategory As DataTable = dsInventory.Tables.Add("RawCategory")
    Friend dtRawMatLast As DataTable = dsInventory.Tables.Add("RawMatLast")
    '   Friend dtRawDelivery As DataTable = dsInventory.Tables.Add("RawDelivery")
    '  Friend dtRawCycle As DataTable = dsInventory.Tables.Add("RawCycle")

    'other tables are created based on the results of these two
    Friend dtStarterAllFoodCategory As DataTable = dsStarter.Tables.Add("StarterAllFoodCategory")
    Friend dtStarterModifierCategory As DataTable = dsStarter.Tables.Add("StarterModifierCategory")
    Friend dtAllFoodCategory As DataTable = ds.Tables.Add("AllFoodCategory")
    Friend dtModifierCategory As DataTable = ds.Tables.Add("ModifierCategory")

    Friend dtStarterLocationOverview As DataTable = dsStarter.Tables.Add("StarterLocationOverview")
    Friend dtLocationOverview As DataTable = ds.Tables.Add("LocationOverview")
    Friend dtLocationOpening As DataTable = ds.Tables.Add("LocationOpening")
    Friend dtMainCategory As DataTable = ds.Tables.Add("MainCategory")
    Friend dtSecondaryMainCategory As DataTable = ds.Tables.Add("SecondaryMainCategory")
    Friend dtIndividualJoinMain As DataTable = ds.Tables.Add("IndividualJoinMain")
    Friend dtIndividualJoinSecondary As DataTable = ds.Tables.Add("IndividualJoinSecondary")
    Friend dtBartenderCategory As DataTable = ds.Tables.Add("BartenderCategory")
    Friend dtBartenderDrinkCategory As DataTable = ds.Tables.Add("BartenderDrinkCategory")
    Friend dtSecondaryBartenderCategory As DataTable = ds.Tables.Add("SecondaryBartenderCategory")
    Friend dtSecondaryBartenderDrinkCategory As DataTable = ds.Tables.Add("SecondaryBartenderDrinkCategory")
    Friend dtQuickCategory As DataTable = ds.Tables.Add("QuickCategory")
    Friend dtQuickDrinkCategory As DataTable = ds.Tables.Add("QuickDrinkCategory")
    Friend dtSecondaryQuickCategory As DataTable = ds.Tables.Add("SecondaryQuickCategory")
    Friend dtSecondaryQuickDrinkCategory As DataTable = ds.Tables.Add("SecondaryQuickDrinkCategory")
    Friend dtDrinkCategory As DataTable = ds.Tables.Add("DrinkCategory")
    Friend dtSecondaryDrinkCategory As DataTable = ds.Tables.Add("SecondaryDrinkCategory")

    Friend dtDrinkSubCategory As DataTable = ds.Tables.Add("DrinkSubCategory")
    Friend dtDrink As DataTable = ds.Tables.Add("Drink")
    Friend dtDrinkAdds As DataTable = ds.Tables.Add("DrinkAdds")


    Friend dtFoods As DataTable = ds.Tables.Add("FoodTable")
    '   Friend dtModifiers As DataTable = ds.Tables.Add("Modifiers")
    Friend dtCategoryJoin As DataTable = ds.Tables.Add("CategoryJoin")
    Friend dtLiquorTypes As DataTable = ds.Tables.Add("LiquorTypes")
    Friend dtDrinkModifiers As DataTable = ds.Tables.Add("DrinkModifiers")
    Friend dtDrinkPrep As DataTable = ds.Tables.Add("DrinkPrep")
    Friend dtTableStatusDesc As DataTable = ds.Tables.Add("TableStatusDesc")
    Friend dtTax As DataTable = ds.Tables.Add("Tax")
    Friend dtMenuChoice As DataTable = ds.Tables.Add("MenuChoice")
    Friend dtRoutingChoice As DataTable = ds.Tables.Add("RoutingChoice")
    '   Friend dtByServerLocal As DataTable = ds.Tables.Add("ByServerLocal")
    Friend dtCreditCardDetail As DataTable = ds.Tables.Add("CreditCardDetail")
    Friend dtTabIdentifier As DataTable = ds.Tables.Add("TabIdentifier")
    Friend dtReasonsVoid As DataTable = ds.Tables.Add("ReasonsVoid")


    '  Friend ds As DataSet = New DataSet("ClosingData")
    Friend dtPromotion As DataTable = ds.Tables.Add("Promotion")
    Friend dtBSGS As DataTable = ds.Tables.Add("BSGS")
    Friend dtCombo As DataTable = ds.Tables.Add("Combo")
    Friend dtComboDetail As DataTable = ds.Tables.Add("ComboDetail")
    Friend dtCoupon As DataTable = ds.Tables.Add("Coupon")

    Friend dtIngredients As DataTable = ds.Tables.Add("Ingredients")
    Friend dtTerminalsMethod As DataTable = ds.Tables.Add("TerminalsMethod")
    Friend dtTerminalsUseOrder As DataTable = ds.Tables.Add("TerminalsUseOrder")
    Friend dtTermsFloor As DataTable = ds.Tables.Add("TermsFloor")
    Friend dtTermsTables As DataTable = ds.Tables.Add("TermsTables")
    Friend dtTermsWalls As DataTable = ds.Tables.Add("TermsWalls")
    Friend dtShiftCodes As DataTable = ds.Tables.Add("ShiftCodes")
    Friend dtGroupBartenders As DataTable = ds.Tables.Add("GroupBartenders")

    Friend dsOrderDemo As DataSet = New DataSet("OrderDataDemo")
    Friend dsEmployeeDemo As DataSet = New DataSet("EmployeeDemo")


    Friend dsOrder As DataSet = New DataSet("OrderData")
    Friend dtOpenOrders As DataTable = dsOrder.Tables.Add("OpenOrders")
    Friend dtOrderDetail As DataTable = dsOrder.Tables.Add("OrderDetail")
    Friend dtOrderForceFree As DataTable = dsOrder.Tables.Add("OrderForceFree")
    '    Friend dtRepeatOrder As DataTable = dsOrder.Tables.Add("RepeatOrder")
    '    Friend dtOrder As DataTable = dsOrder.Tables.Add("Order")
    Friend dtCurrentlyHeld As DataTable = dsOrder.Tables.Add("CurrentlyHeld")
    Friend dtAvailTables As DataTable = dsOrder.Tables.Add("AvailTables")
    Friend dtAvailTabs As DataTable = dsOrder.Tables.Add("AvailTabs")
    Friend dtQuickTickets As DataTable = dsOrder.Tables.Add("QuickTickets")
    '   Friend dtCurrentTables As DataTable = dsOrder.Tables.Add("CurrentTables")
    Friend dtAllTables As DataTable = dsOrder.Tables.Add("AllTables")
    Friend dtUnNamedTabID As DataTable = dsOrder.Tables.Add("UnNamedTabID")
    Friend dtOpenedTabID As DataTable = dsOrder.Tables.Add("OpenedTabID")
    Friend dtInNamedTabs As DataTable = dsOrder.Tables.Add("UnNamedTabs")
    Friend dtClosedTables As DataTable = dsOrder.Tables.Add("ClosedTables")
    Friend dtClosedTabs As DataTable = dsOrder.Tables.Add("ClosedTabs")
    '   Friend dtOpenTables As DataTable = dsOrder.Tables.Add("OpenTables")
    '   Friend dtOpenTabs As DataTable = dsOrder.Tables.Add("OpenTabs")
    Friend dtOpenBusiness As DataTable = dsOrder.Tables.Add("OpenBusiness")
    Friend dtTermsOpen As DataTable = dsOrder.Tables.Add("TermsOpen")
    Friend dtCashIn As DataTable = dsOrder.Tables.Add("CashIn")
    Friend dtCashOut As DataTable = dsOrder.Tables.Add("CashOut")
    '   Friend dtOpenTickets As DataTable = dsOrder.Tables.Add("OpenTables")


    '   Friend dtExperienceTable As DataTable = dsOrder.Tables.Add("ExperienceTable")

    '    Friend dtStatusChange As DataTable = dsOrder.Tables.Add("StatusChange")
    Friend dtOrderByPrinter As DataTable = dsOrder.Tables.Add("OrderByPrinter")

    '   not sure of the following
    Friend dtCurrentStatus As DataTable = dsOrder.Tables.Add("CurrentStatus")
    Friend dtAdjustment As DataTable = dsOrder.Tables.Add("Adjustment")
    Friend dtPaymentsAndCredits As DataTable = dsOrder.Tables.Add("PaymentsAndCredits")
    Friend dtPaymentType As DataTable = dsOrder.Tables.Add("PaymentType")
    Friend dtFunctions As DataTable = dsOrder.Tables.Add("Functions")

    Friend dtTrainingDaily As DataTable = dsOrder.Tables.Add("TrainingDaily")


    Friend dsBackup As DataSet = New DataSet("Backup")
    Friend dtOpenOrdersTerminal As DataTable = dsBackup.Tables.Add("OpenOrdersTerminal")
    Friend dtAvailTablesTerminal As DataTable = dsBackup.Tables.Add("AvailTablesTerminal")
    Friend dtAvailTabsTerminal As DataTable = dsBackup.Tables.Add("AvailTabsTerminal")
    Friend dtESCTerminal As DataTable = dsBackup.Tables.Add("ESCTerminal")
    Friend dtPaymentsAndCreditsTerminal As DataTable = dsBackup.Tables.Add("PaymentsAndCreditsTerminal")
    Friend dtEmployeeTerminal As DataTable = dsBackup.Tables.Add("EmployeeTerminal")

    Friend dsEmployee As DataSet = New DataSet("Employee")
    Friend dtClockedIn As DataTable = dsEmployee.Tables.Add("ClockedIn")
    Friend dtLoggedInEmploees As DataTable = dsEmployee.Tables.Add("LoggedInEmployees")
    Friend dtClockOutSales As DataTable = dsEmployee.Tables.Add("ClockOutSales")
    Friend dtClockOutTaxes As DataTable = dsEmployee.Tables.Add("ClockOutTaxes")
    Friend dtClockOutPayments As DataTable = dsEmployee.Tables.Add("ClockOutPayments")
    Friend dtClockOutAudit As DataTable = dsEmployee.Tables.Add("ClockOutAudit")
    Friend dtSalesDetail As DataTable = dsEmployee.Tables.Add("SalesDetail")
    'these 2 are employee tables but saving in ds 
    ' so they will be backed up
    Friend dtStarterAllEmployees As DataTable = dsStarter.Tables.Add("StarterAllEmployees")
    Friend dtStarterJobCodeInfo As DataTable = dsStarter.Tables.Add("StarterJobCodeInfo")
    Friend dtAllEmployees As DataTable = dsEmployee.Tables.Add("AllEmployees")
    Friend dtJobCodeInfo As DataTable = dsEmployee.Tables.Add("JobCodeInfo")



    Friend dsCustomer As DataSet = New DataSet("Customer")
    Friend dsCustomerDemo As DataSet = New DataSet("CustomerDemo")
    Friend dtTabDirectorySearch As DataTable = dsCustomer.Tables.Add("TabDirectorySearch")
    '    Friend dtTabStorePhone As DataTable = dsCustomer.Tables.Add("TabStorePhone")
    '    Friend dtTabStoreName As DataTable = dsCustomer.Tables.Add("TabStoreName")
    '    Friend dtTabStoreAcct As DataTable = dsCustomer.Tables.Add("TabStoreAcct")
    '    Friend dtTabCompnayPhone As DataTable = dsCustomer.Tables.Add("TabCompanyPhone")
    '    Friend dtTabCompnayName As DataTable = dsCustomer.Tables.Add("TabCompanyName")
    '   Friend dtTabCompnayAcct As DataTable = dsCustomer.Tables.Add("TabCompanyAcct")
    Friend dtTabPreviousOrders As DataTable = dsCustomer.Tables.Add("TabPreviousOrders")
    Friend dtTabPreviousOrdersByItem As DataTable = dsCustomer.Tables.Add("TabPreviousOrdersbyItem")


    '    Friend dcFunctionGroupSales As DataColumn = dtClockOutSales.Columns.Add("@FunctionGroupSales", Type.GetType("System.Decimal"))
    '   Friend dcFunctionName As DataColumn = dtClockOutSales.Columns.Add("@FunctionName", Type.GetType("System.String"))


    '   *****************
    '   PaymentsAndCredits
    '    Friend pcPaymentsAndCreditsID As DataColumn = dtPaymentsAndCredits.Columns.Add("@PaymentsAndCreditsID", Type.GetType("System.Int64"))
    '   Friend pcCompanyID As DataColumn = dtPaymentsAndCredits.Columns.Add("@CompanyID", Type.GetType("System.String"))
    '   Friend pcLocationID As DataColumn = dtPaymentsAndCredits.Columns.Add("@LocationID", Type.GetType("System.String"))
    '   Friend pcOpenOrderID As DataColumn = dtPaymentsAndCredits.Columns.Add("@OpenOrderID", Type.GetType("System.Int64"))
    '   Friend pcExperienceNumber As DataColumn = dtPaymentsAndCredits.Columns.Add("@ExperienceNumber", Type.GetType("System.Int64"))
    '  Friend pcEmployeeID As DataColumn = dtPaymentsAndCredits.Columns.Add("@EmployeeID", Type.GetType("System.Int32"))
    ''  Friend pcCheckNumber As DataColumn = dtPaymentsAndCredits.Columns.Add("@CheckNumber", Type.GetType("System.Int32"))
    '  Friend pcPaymentTypeID As DataColumn = dtPaymentsAndCredits.Columns.Add("@PaymentTypeID", Type.GetType("System.Int32"))
    '  Friend pcPaymentFlag As DataColumn = dtPaymentsAndCredits.Columns.Add("@PaymentFlag", Type.GetType("System.String"))
    '  Friend pcAccountNumber As DataColumn = dtPaymentsAndCredits.Columns.Add("@AccountNumber", Type.GetType("System.String"))
    '    Friend pcCCExpiration As DataColumn = dtPaymentsAndCredits.Columns.Add("@CCExpiration", Type.GetType("System.String"))
    '    Friend pcTrack2 As DataColumn = dtPaymentsAndCredits.Columns.Add("@Track2", Type.GetType("System.String"))
    '    Friend pcCustomerName As DataColumn = dtPaymentsAndCredits.Columns.Add("@CustomerName", Type.GetType("System.Int32"))
    '   Friend pcTransactionType As DataColumn = dtPaymentsAndCredits.Columns.Add("@TransactionType", Type.GetType("System.String"))
    ''   Friend pcTransactionCode As DataColumn = dtPaymentsAndCredits.Columns.Add("@TransactionCode", Type.GetType("System.Int32"))
    '   Friend pcSwipeType As DataColumn = dtPaymentsAndCredits.Columns.Add("@SwipeType", Type.GetType("System.Int32"))
    '   Friend pcPaymentAmount As DataColumn = dtPaymentsAndCredits.Columns.Add("@PaymentAmount", Type.GetType("System.Decimal"))
    '   Friend pcTip As DataColumn = dtPaymentsAndCredits.Columns.Add("@Tip", Type.GetType("System.Decimal"))
    '   Friend pcPreAuthAmount As DataColumn = dtPaymentsAndCredits.Columns.Add("@PreAuthAmount", Type.GetType("System.Decimal"))
    '   Friend pcApplied As DataColumn = dtPaymentsAndCredits.Columns.Add("@Applied", Type.GetType("System.Boolean"))
    '   Friend pcRefNum As DataColumn = dtPaymentsAndCredits.Columns.Add("@RefNum", Type.GetType("System.String"))
    '   Friend pcAuthCode As DataColumn = dtPaymentsAndCredits.Columns.Add("@AuthCode", Type.GetType("System.Int32"))
    '   Friend pcAcqRefData As DataColumn = dtPaymentsAndCredits.Columns.Add("@AcqRefData", Type.GetType("System.String"))
    '  Friend pcTerminalID As DataColumn = dtPaymentsAndCredits.Columns.Add("@TerminalID", Type.GetType("System.Int32"))
    ''  Friend pcdbUP As DataColumn = dtPaymentsAndCredits.Columns.Add("@dbUP", Type.GetType("System.Int32"))


    '*** OpenOrders



    '    Friend ooOpenOrderID As DataColumn = dtOpenOrders.Columns.Add("@OpenOrderID", Type.GetType("System.Int64"))
    '    Friend ooCompanyID As DataColumn = dtOpenOrders.Columns.Add("@CompanyID", Type.GetType("System.String"))
    '    Friend ooLocationID As DataColumn = dtOpenOrders.Columns.Add("@LocationID", Type.GetType("System.String"))
    '    Friend ooDailyCode As DataColumn = dtOpenOrders.Columns.Add("@DailyCode", Type.GetType("System.Int64"))
    '   Friend ooExperienceNumber As DataColumn = dtOpenOrders.Columns.Add("@ExperienceNumber", Type.GetType("System.Int64"))
    ''   Friend ooOrderNumber As DataColumn = dtOpenOrders.Columns.Add("@OrderNumber", Type.GetType("System.Int64"))
    '   Friend ooShiftID As DataColumn = dtOpenOrders.Columns.Add("@ShiftID", Type.GetType("System.Int32"))
    '   Friend ooMenuID As DataColumn = dtOpenOrders.Columns.Add("@MenuID", Type.GetType("System.Int32"))
    '   Friend ooEmployeeID As DataColumn = dtOpenOrders.Columns.Add("@EmployeeID", Type.GetType("System.Int32"))
    '   'old   '  Friend ooEmployeeNumber As DataColumn = dtOpenOrders.Columns.Add("@EmployeeNumber", Type.GetType("System.Int32"))
    '   'old   '    Friend ooTableNumber As DataColumn = dtOpenOrders.Columns.Add("@TableNumber", Type.GetType("System.Int32"))
    '   'old    '   Friend ooTabID As DataColumn = dtOpenOrders.Columns.Add("@TabID", Type.GetType("System.Int32"))
    '   'old    '  Friend ooTabName As DataColumn = dtOpenOrders.Columns.Add("@TabName", Type.GetType("System.String"))
    '   Friend ooCheckNumber As DataColumn = dtOpenOrders.Columns.Add("@CheckNumber", Type.GetType("System.Int32"))
    '   Friend ooCustomerNumber As DataColumn = dtOpenOrders.Columns.Add("@CustomerNumber", Type.GetType("System.Int32"))
    '  Friend ooCourseNumber As DataColumn = dtOpenOrders.Columns.Add("@CourseNumber", Type.GetType("System.Int32"))
    '  Friend ooSIN As DataColumn = dtOpenOrders.Columns.Add("@sin", Type.GetType("System.Int32"))
    ''  Friend ooSII As DataColumn = dtOpenOrders.Columns.Add("@sii", Type.GetType("System.Int32"))
    '  Friend ooSi2 As DataColumn = dtOpenOrders.Columns.Add("@si2", Type.GetType("System.Int32"))
    '    Friend ooQuantity As DataColumn = dtOpenOrders.Columns.Add("@Quantity", Type.GetType("System.Int32"))
    '    Friend ooItemID As DataColumn = dtOpenOrders.Columns.Add("@ItemID", Type.GetType("System.Int32"))
    '    Friend ooItemName As DataColumn = dtOpenOrders.Columns.Add("@ItemName", Type.GetType("System.String"))
    '   Friend ooTerminalName As DataColumn = dtOpenOrders.Columns.Add("@TerminalName", Type.GetType("System.String"))
    ''   Friend ooChitName As DataColumn = dtOpenOrders.Columns.Add("@ChitName", Type.GetType("System.String"))
    '   'old    '   Friend ooAddChit As DataColumn = dtOpenOrders.Columns.Add("@AddChit", Type.GetType("System.String"))
    '  Friend ooItemPrice As DataColumn = dtOpenOrders.Columns.Add("@ItemPrice", Type.GetType("System.Decimal"))
    ''   Friend ooPrice As DataColumn = dtOpenOrders.Columns.Add("@Price", Type.GetType("System.Decimal"))
    '   Friend ooTaxPrice As DataColumn = dtOpenOrders.Columns.Add("@TaxPrice", Type.GetType("System.Decimal"))
    '   Friend ooSinTax As DataColumn = dtOpenOrders.Columns.Add("@SinTax", Type.GetType("System.Decimal"))
    '   Friend ooTaxID As DataColumn = dtOpenOrders.Columns.Add("@TaxID", Type.GetType("System.Int32"))
    '   Friend ooForceFreeID As DataColumn = dtOpenOrders.Columns.Add("@ForceFreeID", Type.GetType("System.Int64"))
    '   Friend ooForceFreeAuth As DataColumn = dtOpenOrders.Columns.Add("@ForceFreeAuth", Type.GetType("System.Int32"))
    '    Friend ooForceFreeCode As DataColumn = dtOpenOrders.Columns.Add("@ForceFreeCode", Type.GetType("System.Int32"))
    '    Friend ooFunctionID As DataColumn = dtOpenOrders.Columns.Add("@FunctionID", Type.GetType("System.Int32"))
    '    Friend ooCategoryID As DataColumn = dtOpenOrders.Columns.Add("@CategoryID", Type.GetType("System.Int32"))
    '   Friend ooFoodID As DataColumn = dtOpenOrders.Columns.Add("@FoodID", Type.GetType("System.Int32"))
    ''   Friend ooDrinkCategoryID As DataColumn = dtOpenOrders.Columns.Add("@DrinkCategoryID", Type.GetType("System.Int32"))
    '   Friend ooDrinkID As DataColumn = dtOpenOrders.Columns.Add("@DrinkID", Type.GetType("System.Int32"))
    '   Friend ooItemStatus As DataColumn = dtOpenOrders.Columns.Add("@ItemStatus", Type.GetType("System.Int32"))
    '   Friend ooRoutingID As DataColumn = dtOpenOrders.Columns.Add("@RoutingID", Type.GetType("System.Int32"))
    '   Friend ooPrintPriorityID As DataColumn = dtOpenOrders.Columns.Add("@PrintPriorityID", Type.GetType("System.Int32"))
    '   Friend ooRepeat As DataColumn = dtOpenOrders.Columns.Add("@Repeat", Type.GetType("System.Byte"))
    '   Friend ooTerminalID As DataColumn = dtOpenOrders.Columns.Add("@TerminalID", Type.GetType("System.Int32"))
    ''  Friend oodbUP As DataColumn = dtOpenOrders.Columns.Add("@dbUP", Type.GetType("System.Int32"))
    '  Friend ooFunctionGroupID As DataColumn = dtOpenOrders.Columns.Add("@FunctionGroupID", Type.GetType("System.Int32"))
    ' Friend ooFunctionFlag As DataColumn = dtOpenOrders.Columns.Add("@FunctionFlag", Type.GetType("System.String"))


    '*** OpenOrders Backup
    '    Friend ooTermCompanyID As DataColumn = dsBackup.Tables("OpenOrdersTerminal").Columns.Add("@CompanyID", Type.GetType("System.String"))
    '    Friend ooTermLocationID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@LocationID", Type.GetType("System.String"))
    '    Friend ooTermOpenOrderD As DataColumn = dtOpenOrdersTerminal.Columns.Add("@OpenOrderID", Type.GetType("System.Int64"))
    '    Friend ooTermDailyCode As DataColumn = dtOpenOrdersTerminal.Columns.Add("@DailyCode", Type.GetType("System.Int64"))
    ''   Friend ooTermExperienceNumber As DataColumn = dtOpenOrdersTerminal.Columns.Add("@ExperienceNumber", Type.GetType("System.Int64"))
    '   Friend ooTermOrderNumber As DataColumn = dtOpenOrdersTerminal.Columns.Add("@OrderNumberNumber", Type.GetType("System.Int64"))
    '   Friend ooTermMenuID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@MenuID", Type.GetType("System.Int32"))
    '   Friend ooTermEmployeeID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@EmployeeID", Type.GetType("System.Int32"))
    '  '   don't forget employee number
    '    Friend ooTermTableNumber As DataColumn = dtOpenOrdersTerminal.Columns.Add("@TableNumber", Type.GetType("System.Int32"))
    '    Friend ooTermTabID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@TabID", Type.GetType("System.Int32"))
    '    Friend ooTermTabName As DataColumn = dtOpenOrdersTerminal.Columns.Add("@TabName", Type.GetType("System.String"))
    '   Friend ooTermCheckNumber As DataColumn = dtOpenOrdersTerminal.Columns.Add("@CheckNumber", Type.GetType("System.Int32"))
    ''   Friend ooTermCustomerNumber As DataColumn = dtOpenOrdersTerminal.Columns.Add("@CustomerNumber", Type.GetType("System.Int32"))
    '   Friend ooTermCourseNumber As DataColumn = dtOpenOrdersTerminal.Columns.Add("@CourseNumber", Type.GetType("System.Int32"))
    '   Friend ooTermSIN As DataColumn = dtOpenOrdersTerminal.Columns.Add("@sin", Type.GetType("System.Int32"))
    '   Friend ooTermSII As DataColumn = dtOpenOrdersTerminal.Columns.Add("@sii", Type.GetType("System.Int32"))
    '  Friend ooTermQuantity As DataColumn = dtOpenOrdersTerminal.Columns.Add("@Quantity", Type.GetType("System.Int32"))
    ''  Friend ooTermItemID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@ItemID", Type.GetType("System.Int32"))
    '  Friend ooTermItemName As DataColumn = dtOpenOrdersTerminal.Columns.Add("@ItemName", Type.GetType("System.String"))
    '  Friend ooTermPrice As DataColumn = dtOpenOrdersTerminal.Columns.Add("@Price", Type.GetType("System.Decimal"))
    '    Friend ooTermTaxPrice As DataColumn = dtOpenOrdersTerminal.Columns.Add("@TaxPrice", Type.GetType("System.Decimal"))
    '    Friend ooTermTaxID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@TaxID", Type.GetType("System.Int32"))
    '    Friend ooTermForceFreeID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@ForceFreeID", Type.GetType("System.Int64"))
    '   Friend ooTermForceFreeAuth As DataColumn = dtOpenOrdersTerminal.Columns.Add("@ForceFreeAuth", Type.GetType("System.Int32"))
    ''   Friend ooTermForceFreeCode As DataColumn = dtOpenOrdersTerminal.Columns.Add("@ForceFreeCode", Type.GetType("System.Int32"))
    '   Friend ooTermFunctionID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@FunctionID", Type.GetType("System.Int32"))
    '   Friend ooTermFunctionGroupID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@FunctionGroupID", Type.GetType("System.Int32"))
    '  Friend ooTermFunctionFlag As DataColumn = dtOpenOrdersTerminal.Columns.Add("@FunctionFlag", Type.GetType("System.String"))
    ''  Friend ooTermCategoryID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@CategoryID", Type.GetType("System.Int32"))
    '  Friend ooTermFoodID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@FoodID", Type.GetType("System.Int32"))
    '  Friend ooTermDrinkCategoryID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@DrinkCategoryID", Type.GetType("System.Int32"))
    ' Friend ooTermDrinkID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@DrinkID", Type.GetType("System.Int32"))
    '   Friend ooTermItemStatus As DataColumn = dtOpenOrdersTerminal.Columns.Add("@ItemStatus", Type.GetType("System.Int32"))
    '    Friend ooTermRoutingID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@RoutingID", Type.GetType("System.Int32"))
    '    Friend ooTermPrintPriorityID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@PrintPriorityID", Type.GetType("System.Int32"))
    '    Friend ooTermRepeat As DataColumn = dtOpenOrdersTerminal.Columns.Add("@Repeat", Type.GetType("System.Byte"))
    '    Friend ooTermTerminalID As DataColumn = dtOpenOrdersTerminal.Columns.Add("@TerminalID", Type.GetType("System.Int32"))
    '    Friend ooTermdbUP As DataColumn = dtOpenOrdersTerminal.Columns.Add("@dbUP", Type.GetType("System.Int32"))



    Friend dvAvailTables As DataView = New DataView(dtOpenOrders)
    Friend dvTransferTables As DataView = New DataView(dtOpenOrders)
    Friend dvAvailTabs As DataView = New DataView(dtOpenOrders)
    Friend dvTransferTabs As DataView = New DataView(dtOpenOrders)
    Friend dvRepeat As New DataView
    Friend dvQuickTickets As DataView = New DataView(dtQuickTickets)
    '    Friend dvQuickTicketsEmployee As DataView
    Friend dvTerminalsUseOrder As DataView
    Friend dvTermsOpen As DataView = New DataView(dtTermsOpen)

    Dim sqlStatement As String
    Dim tableCreating As String


    '  Friend dtFoodJoinModifier As DataTable = ds.Tables.Add("FoodJoinModifier")
    '  Friend dtDrinkModifiers As DataTable = ds.Tables.Add("DrinkModifiers")


    '   Friend dtMainTable1 As DataTable = ds.Tables.Add("MainTable1")
    '   Friend dtMainTable2 As DataTable = ds.Tables.Add("MainTable2")
    '   Friend dtMainTable3 As DataTable = ds.Tables.Add("MainTable3")
    '  Friend dtMainTable4 As DataTable = ds.Tables.Add("MainTable4")
    '  Friend dtMainTable5 As DataTable = ds.Tables.Add("MainTable5")
    ''  Friend dtMainTable6 As DataTable = ds.Tables.Add("MainTable6")
    '  Friend dtMainTable7 As DataTable = ds.Tables.Add("MainTable7")
    '  Friend dtMainTable8 As DataTable = ds.Tables.Add("MainTable8")
    '  Friend dtMainTable9 As DataTable = ds.Tables.Add("MainTable9")
    '   Friend dtMainTable10 As DataTable = ds.Tables.Add("MainTable10")
    '   Friend dtMainTable11 As DataTable = ds.Tables.Add("MainTable11")
    ''  Friend dtMainTable12 As DataTable = ds.Tables.Add("MainTable12")
    '  Friend dtMainTable13 As DataTable = ds.Tables.Add("MainTable13")
    '  Friend dtMainTable14 As DataTable = ds.Tables.Add("MainTable14")
    '  Friend dtMainTable15 As DataTable = ds.Tables.Add("MainTable15")
    ' Friend dtMainTable16 As DataTable = ds.Tables.Add("MainTable16")
    '' Friend dtMainTable17 As DataTable = ds.Tables.Add("MainTable17")
    '    Friend dtMainTable18 As DataTable = ds.Tables.Add("MainTable18")
    '    Friend dtMainTable19 As DataTable = ds.Tables.Add("MainTable19")
    '   Friend dtMainTable20 As DataTable = ds.Tables.Add("MainTable20")
    ''
    '   Friend dtModifierTable1 As DataTable = ds.Tables.Add("ModifierTable101")
    '   Friend dtModifierTable2 As DataTable = ds.Tables.Add("ModifierTable102")
    '   Friend dtModifierTable3 As DataTable = ds.Tables.Add("ModifierTable103")
    '   Friend dtModifierTable4 As DataTable = ds.Tables.Add("ModifierTable104")
    '    Friend dtModifierTable5 As DataTable = ds.Tables.Add("ModifierTable105")
    '   Friend dtModifierTable6 As DataTable = ds.Tables.Add("ModifierTable106")
    '   Friend dtModifierTable7 As DataTable = ds.Tables.Add("ModifierTable107")
    '   Friend dtModifierTable8 As DataTable = ds.Tables.Add("ModifierTable108")
    '   Friend dtModifierTable9 As DataTable = ds.Tables.Add("ModifierTable109")
    ''   Friend dtModifierTable10 As DataTable = ds.Tables.Add("ModifierTable110")
    '  Friend dtModifierTable11 As DataTable = ds.Tables.Add("ModifierTable111")
    ''   Friend dtModifierTable12 As DataTable = ds.Tables.Add("ModifierTable112")
    '  Friend dtModifierTable13 As DataTable = ds.Tables.Add("ModifierTable113")
    '    Friend dtModifierTable14 As DataTable = ds.Tables.Add("ModifierTable114")
    '   Friend dtModifierTable15 As DataTable = ds.Tables.Add("ModifierTable115")
    '   Friend dtModifierTable16 As DataTable = ds.Tables.Add("ModifierTable116")
    '  Friend dtModifierTable17 As DataTable = ds.Tables.Add("ModifierTable117")
    ''  Friend dtModifierTable18 As DataTable = ds.Tables.Add("ModifierTable118")
    '  Friend dtModifierTable19 As DataTable = ds.Tables.Add("ModifierTable119")
    ' Friend dtModifierTable20 As DataTable = ds.Tables.Add("ModifierTable120")

    '    Friend dtSecondaryMainTable1 As DataTable = ds.Tables.Add("SecondaryMainTable1")
    '    Friend dtSecondaryMainTable2 As DataTable = ds.Tables.Add("SecondaryMainTable2")
    '    Friend dtSecondaryMainTable3 As DataTable = ds.Tables.Add("SecondaryMainTable3")
    '    Friend dtSecondaryMainTable4 As DataTable = ds.Tables.Add("SecondaryMainTable4")
    '    Friend dtSecondaryMainTable5 As DataTable = ds.Tables.Add("SecondaryMainTable5")
    '    Friend dtSecondaryMainTable6 As DataTable = ds.Tables.Add("SecondaryMainTable6")
    '    Friend dtSecondaryMainTable7 As DataTable = ds.Tables.Add("SecondaryMainTable7")
    '   Friend dtSecondaryMainTable8 As DataTable = ds.Tables.Add("SecondaryMainTable8")
    '   Friend dtSecondaryMainTable9 As DataTable = ds.Tables.Add("SecondaryMainTable9")
    ''   Friend dtSecondaryMainTable10 As DataTable = ds.Tables.Add("SecondaryMainTable10")
    '   Friend dtSecondaryMainTable11 As DataTable = ds.Tables.Add("SecondaryMainTable11")
    '   Friend dtSecondaryMainTable12 As DataTable = ds.Tables.Add("SecondaryMainTable12")
    '   Friend dtSecondaryMainTable13 As DataTable = ds.Tables.Add("SecondaryMainTable13")
    '   Friend dtSecondaryMainTable14 As DataTable = ds.Tables.Add("SecondaryMainTable14")
    '   Friend dtSecondaryMainTable15 As DataTable = ds.Tables.Add("SecondaryMainTable15")
    '  Friend dtSecondaryMainTable16 As DataTable = ds.Tables.Add("SecondaryMainTable16")
    ''  Friend dtSecondaryMainTable17 As DataTable = ds.Tables.Add("SecondaryMainTable17")
    '  Friend dtSecondaryMainTable18 As DataTable = ds.Tables.Add("SecondaryMainTable18")
    ' Friend dtSecondaryMainTable19 As DataTable = ds.Tables.Add("SecondaryMainTable19")
    'Friend dtSecondaryMainTable20 As DataTable = ds.Tables.Add("SecondaryMainTable20")


    Friend dvOrder As DataView = New DataView(dtOpenOrders)
    Friend dvOrderPrint As DataView = New DataView(dtOpenOrders)
    Friend dvOrderTopHierarcy As DataView = New DataView(dtOpenOrders)
    Friend dvOrderHolds As DataView = New DataView(dtOpenOrders)
    Friend dvAllChecks As DataView = New DataView(dtOpenOrders)
    Friend dvKitchen As DataView = New DataView(dtOpenOrders)
    Friend dvFood As DataView = New DataView
    Friend dvPizzaFull As New DataView
    Friend dvPizzaFirst As New DataView
    Friend dvPizzaSecond As New DataView
    '   Friend dvModifier As DataView = New DataView
    '    Friend dvAdjustment As DataView = New DataView
    '   Friend dvFreeFood As DataView = New DataView
    Friend dvDrink As DataView = New DataView
    Friend dvFoodJoin As DataView = New DataView
    Friend dvIndividualJoinAuto As DataView = New DataView
    Friend dvIndividualJoinGroup As DataView = New DataView

    Friend dvCategoryJoin As DataView = New DataView
    Friend dvCategoryModifiers As DataView = New DataView
    Friend dvCategoryJoinSecondLoop As DataView = New DataView
    Friend dvCategoryModifiersSecondLoop As DataView = New DataView
    Friend dvSurcharge As DataView = New DataView
    Friend dvSendToModify As DataView = New DataView(dtOpenOrders)

    '    Friend dsReport As DataSet = New DataSet("ReportData")
    '   Friend dtReport_ItemsSold As DataTable = dsReport.Tables.Add("Report_ItemsSold")

    Public Sub TempConnectToPhoenix()
        '     If System.Windows.Forms.SystemInformation.ComputerName = "DILEO" Then Exit Sub

        If localConnectServer = "rasoi2\rasoi2" And Not System.Windows.Forms.SystemInformation.ComputerName = "EGLOBALMAIN" Then
            'only rasoi2 and not testing
            RestateConnectionString(sql.cn, localConnectServer)
        Else
            RestateConnectionString(sql.cn, "Phoenix\Phoenix")
        End If

    End Sub

    Public Sub ConnectBackFromTempDatabase()
        '    If System.Windows.Forms.SystemInformation.ComputerName = "DILEO" Then Exit Sub

        RestateConnectionString(sql.cn, connectserver)

    End Sub

    Public Sub SwitchConnection()

        If connectserver = "Phoenix\Phoenix" Then
            connectserver = localConnectServer  ' "LABMAIN\labmain"      
            RestateConnectionString(sql.cn, connectserver)
            '           InitiateApplicationSecurity()
        Else

            connectserver = "Phoenix\Phoenix"
            RestateConnectionString(sql.cn, connectserver)
            '          InitiateApplicationSecurity()
        End If

    End Sub

    Public Sub InitiateApplicationSecurity222()

        'the security true/false test are meaningless
        'we have to initiate security everytime we open connection
        sql.cn.Open()
              sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        If connectserver = "Phoenix\Phoenix" Then
            If securityPhoenixEst = False Then
                Try
                          sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                    securityPhoenixEst = True
                Catch ex As Exception
                    CloseConnection()
                    MsgBox("DataCenter Not Connected. Verify all wire connection are established and your router is working. Then call 404-869-4700: " & ex.Message)
                End Try

            End If
        Else
            If securityLocalEst = False Then
                Try
                          sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                    securityLocalEst = True
                Catch ex As Exception
                    CloseConnection()
                    MsgBox("Local Database Not Connected. Verify all wire connection are established and your router is working. Then call 404-869-4700: " & ex.Message)
                End Try

            End If
        End If
        sql.cn.Close()

    End Sub


    Public Sub RestateConnectionString(ByVal myConnection As SqlClient.SqlConnection, ByVal cs As String)
        '      cs = "Phoenix\Phoenix"
        '     sql.cn.ConnectionString = "packet size=4096;integrated security=SSPI;data source='" & cs & "';pe" & _
        '     "rsist security info=False;Pooling=False;initial catalog=Restaurant_Server"
        '   Exit Sub
        connectionDown = False
        '
        'cn

        '******************
        '  when testing Connection Down 
        '   we need below cs = current sql server
        '   we need to make the below .ComputerName = "EGLOBALMAIN" fail
        '    cs = "eglobalmain\eglobalmain"

        If System.Windows.Forms.SystemInformation.ComputerName = "EGLOBALMAIN" Then ' And Not companyInfo.companyName = "eGlobal Partners" Then '** (for testing connection down) And Not companyInfo.companyName = Nothing Then
            ' below replaces above when testing Connection Down   
            '******    
            'If System.Windows.Forms.SystemInformation.ComputerName = "EGLOBALMAIN" And Not companyInfo.companyName = "eGlobal Partners" And Not companyInfo.companyName = Nothing Then
            '    sql.cn.ConnectionString = "packet size=4096;integrated security=SSPI;data source=Phoenix\Phoenix;pe" & _
            '       "rsist security info=False;Pooling=False;initial catalog=Restaurant_Server"
            ' end of connection down testing
            '******

            '******
            '   remove below when testing Connection Down 
            If companyInfo.companyName = "eGlobal Partners" Or localConnectServer = "eglobalmain\eglobalmain" Then 'Or attemptedToLoad = True Then 'companyInfo.companyName = Nothing Then 'Or cs = "eglobalmain\eglobalmain" Then
                '   below makes me able to access data base on demo
                sql.cn.ConnectionString = "packet size=4096;integrated security=SSPI;data source=eglobalmain\eglobalmain;pe" & _
               "rsist security info=False;Pooling=False;initial catalog=Restaurant_Server"
            Else
                '   below makes me able to access every account from my computer
                sql.cn.ConnectionString = "packet size=4096;integrated security=SSPI;data source=Phoenix\Phoenix;pe" & _
                         "rsist security info=False;Pooling=False;initial catalog=Restaurant_Server"
            End If
        Else
            sql.cn.ConnectionString = "packet size=4096;integrated security=SSPI;data source='" & cs & "';pe" & _
                  "rsist security info=False;Pooling=False;initial catalog=Restaurant_Server"
            '   sql.cn.ConnectionString = "packet size=4096;integrated security=SSPI;data source='" & cs & "';pe" & _
            '            "rsist security info=False;initial catalog=Restaurant_Server"
        End If


        Exit Sub

        cs = "eglobalmain\eglobalmain"
        cs = "Phoenix\Phoenix"

        '        myConnection.ConnectionString = "packet size=4096;integrated security=SSPI;data source='" & cs & "';pe" & _
        '  "rsist security info=False;initial catalog=Restaurant_Server"
        sql.cn.ConnectionString = "packet size=4096;integrated security=SSPI;Connect Timeout=240;data source='" & cs & "';pe" & _
   "rsist security info=False;initial catalog=Restaurant_Server"
        '     Me.cn.ConnectionString = "packet size=4096;integrated security=SSPI;data source=Phoenix;pe" & _
        ' "rsist security info=False;initial catalog=Restaurant_Server"

        '       Me.cn.ConnectionString = "workstation id=VAIO;packet size=4096;integrated security=SSPI;data source=VAIO;pe" & _
        ' "rsist security info=False;initial catalog=Restaurant_Server"
        '     Me.cn.ConnectionString = "workstation id=VAIO;packet size=4096;integrated security=SSPI;data source=Phoenix;pe" & _
        ' "rsist security info=False;initial catalog=Restaurant_Server"


        'thi is dup of above w/o persist security
        sql.cn.ConnectionString = "packet size=4096;integrated security=SSPI;Connect Timeout=240;Pooling=False;data source='" & cs & "';initial catalog=Restaurant_Server"


        Try
            If cs = "Phoenix\Phoenix" Then
                '      sql.cn.ConnectionString = "packet size=4096;User Id=TAHSC\LabmainAuto;Password=egghead103;data source='" & cs & "';initial catalog=Restaurant_Server"

                '*************** this above connection fails
                sql.cn.ConnectionString = "packet size=4096;integrated security=SSPI;Connect Timeout=240;Pooling=False;data source='" & cs & "';initial catalog=Restaurant_Server"

                sql.cn.ConnectionString = "packet size=4096;integrated security=SSPI;data source='" & cs & "';pe" & _
                 "rsist security info=False;initial catalog=Restaurant_Server"

            Else

                sql.cn.ConnectionString = "packet size=4096;integrated security=SSPI;data source='" & cs & "';pe" & _
           "rsist security info=False;initial catalog=Restaurant_Server"


            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try




    End Sub

    Friend Sub PopulateOrderTables(ByVal fromStart As Boolean)  'ByVal tn As Integer)

        '      updateClockTimer = New Timer
        orderInactiveTimer = New Timer
        tablesInactiveTimer = New Timer
        splitInactiveTimer = New Timer
        '      closeInactiveTimer = New Timer
        tmrCardRead = New Timer

        Dim customLocationString As String
        If companyInfo.usingDefaults = False Then
            customLocationString = companyInfo.LocationID
        Else
            customLocationString = "000000"
        End If

        '   populates to setup dataset organization , for if we go down
        '444    PopulateOpenOrderData(0, True)
        PopulateThisExperience(0, True)
        '       PopulatePaymentsAndCredits(0)   doing in above
        '      PopulateOrderDetail(0)

        '   ***** may need to fill both StatusChange & PaymnetCredit Tables
        '     tableCreating = "StatusChange"
        '    sqlStatement = "SELECT CompanyID, LocationID, DailyCode, ExperienceStatusChangeID, ExperienceNumber, StatusTime, TableStatusID, OrderNumber, IsMainCourse, AverageDollar, TerminalID, dbUP FROM ExperienceStatusChange WHERE CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & companyInfo.LocationID & "' AND DailyCode = -1"
        '   dsOrder = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, dsOrder)


        tableCreating = "Functions"
        '   *** remember to change .. we will keep the same function for all locations
        '   the database is already setup, just not pulling by location until we need to
        sqlStatement = "SELECT FunctionID, FunctionGroupID, FunctionName, FunctionFlag, TaxID, DrinkRoutingID FROM AABFunctions WHERE CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & customLocationString & "'"
        dsOrder = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, dsOrder)

        '       tableCreating = "PaymentsAndCredits"
        '      sqlStatement = "SELECT PaymentsAndCredits.PaymentsAndCreditsID, PaymentsAndCredits.CompanyID, PaymentsAndCredits.LocationID, PaymentsAndCredits.DailyCode, PaymentsAndCredits.ExperienceNumber, PaymentsAndCredits.PaymentDate, PaymentsAndCredits.EmployeeID, PaymentsAndCredits.CheckNumber, PaymentsAndCredits.PaymentTypeID, PaymentsAndCredits.AccountNumber, PaymentsAndCredits.CCExpiration, PaymentsAndCredits.Track2, PaymentsAndCredits.CustomerName, PaymentsAndCredits.TransactionType, PaymentsAndCredits.TransactionCode, PaymentsAndCredits.SwipeType, PaymentsAndCredits.PaymentAmount, PaymentsAndCredits.Surcharge, PaymentsAndCredits.Tip, PaymentsAndCredits.PreAuthAmount, PaymentsAndCredits.Applied, PaymentsAndCredits.RefNum, PaymentsAndCredits.AuthCode, PaymentsAndCredits.AcqRefData, PaymentsAndCredits.TerminalID, PaymentsAndCredits.dbUP, AABPaymentType.PaymentTypeName FROM PaymentsAndCredits LEFT OUTER JOIN AABPaymentType ON PaymentsAndCredits.PaymentTypeID = AABPaymentType.PaymentTypeID WHERE CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & companyInfo.LocationID & "' AND ExperienceNumber = 0"
        '     dsOrder = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, dsOrder)

        tableCreating = "PaymentType"
        sqlStatement = "SELECT PaymentTypeID, PaymentTypeName, PaymentFlag FROM AABPaymentType"
        dsOrder = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, dsOrder)

    End Sub

    Friend Sub PopulateLocationOpening(ByVal fromStart As Boolean)

        Dim changedVersion As Boolean
        Dim lastVersion As Integer

        ds.Tables("LocationOpening").Rows.Clear()

        'true fromStart just means connection already open
        If fromStart = False Then
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        End If
        sql.SqlSelectLocationOpening.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlLocationOpening.Fill(ds.Tables("LocationOpening"))
        If fromStart = False Then
            sql.cn.Close()
            '      Else
            '         If ds.Tables("LocationOpening").Rows.Count > 0 Then
            '     If My.Application.Info.Version.MinorRevision > ds.Tables("LocationOpening").Rows(0)("LastAppVersion") Then
            '       End If
            '   End If
        End If

        Try
            If ds.Tables("LocationOpening").Rows.Count > 0 Then
                If My.Application.Info.Version.MinorRevision > ds.Tables("LocationOpening").Rows(0)("LastAppVersion") Then
                    lastVersion = My.Application.Info.Version.MinorRevision
                    changedVersion = True
                End If
            End If
            If changedVersion = True Then
                ds.Tables("LocationOpening").Rows(0)("LastAppVersion") = lastVersion
                If fromStart = False Then
                    sql.cn.Open()
                    sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                End If
                sql.SqlLocationOpening.Update(ds.Tables("LocationOpening"))
                If fromStart = False Then
                    sql.cn.Close()
                End If
                ds.Tables("LocationOpening").AcceptChanges()
            End If

        Catch ex As Exception
            ' CloseConnection()   'is open already 
        End Try

    End Sub

    Friend Sub PopulateMenuSupport()
        Dim customLocationString As String
        If companyInfo.usingDefaults = False Then
            customLocationString = companyInfo.LocationID
        Else
            customLocationString = "000000"
        End If

        '   *** these 2 tables are needed to make other table
        tableCreating = "AllFoodCategory"
        sqlStatement = "SELECT Category.CompanyID, Category.LocationID, Category.CategoryID, Category.FunctionID, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag, AABFunctions.TaxID FROM Category LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE Category.CompanyID = '" & companyInfo.CompanyID & "' AND Category.LocationID = '" & customLocationString & "' AND Category.Active = 1 AND (AABFunctions.FunctionFlag = 'F' OR AABFunctions.FunctionFlag = 'O' OR AABFunctions.FunctionFlag = 'G' OR AABFunctions.FunctionFlag = 'M') ORDER BY Priority ASC"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        tableCreating = "ModifierCategory"
        sqlStatement = "SELECT Category.CompanyID, Category.LocationID, Category.CategoryID, Category.CategoryName, Category.CategoryAbrev, Category.CategoryOrder, Category.FunctionID, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag FROM Category RIGHT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE AABFunctions.FunctionFlag = 'M' AND Category.CompanyID = '" & companyInfo.CompanyID & "' AND Category.LocationID = '" & customLocationString & "' AND Category.Active = 1 ORDER BY Priority ASC"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        '666 below is new
        '    sqlStatement = "SELECT Category.CompanyID, Category.LocationID, Category.CategoryID, Category.CategoryName, Category.CategoryAbrev, Category.CategoryOrder, Category.FunctionID, Category.Extended, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag FROM Category RIGHT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE AABFunctions.FunctionFlag = 'G' AND Category.CompanyID = '" & companyInfo.CompanyID & "' AND Category.LocationID = '" & customLocationString & "' AND Category.Active = 1 ORDER BY Priority ASC"
        '   ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        'these 2 tables are the exact same as the above 2
        ' we just need them in 2 datasets for recovery
        tableCreating = "StarterAllFoodCategory"
        sqlStatement = "SELECT Category.CompanyID, Category.LocationID, Category.CategoryID, Category.FunctionID, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag, AABFunctions.TaxID FROM Category LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE Category.CompanyID = '" & companyInfo.CompanyID & "' AND Category.LocationID = '" & customLocationString & "' AND Category.Active = 1 AND (AABFunctions.FunctionFlag = 'F' OR AABFunctions.FunctionFlag = 'O' OR AABFunctions.FunctionFlag = 'G') ORDER BY Priority ASC"
        dsStarter = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, dsStarter)

        tableCreating = "StarterModifierCategory"
        sqlStatement = "SELECT Category.CompanyID, Category.LocationID, Category.CategoryID, Category.CategoryName, Category.CategoryAbrev, Category.CategoryOrder, Category.FunctionID, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag FROM Category RIGHT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE AABFunctions.FunctionFlag = 'M' AND Category.CompanyID = '" & companyInfo.CompanyID & "' AND Category.LocationID = '" & customLocationString & "' AND Category.Active = 1 ORDER BY Priority ASC"
        dsStarter = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, dsStarter)


        '   maybe don't need?????????????????????
        '       tableCreating = "Modifiers"
        '      sqlStatement = "SELECT FoodID, CategoryID, FoodName, FoodCost, TaxID, Surcharge, FoodDesc FROM Foods WHERE CategoryID > 100 and CategoryID < 200"
        '     ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        tableCreating = "CategoryJoin"
        sqlStatement = "SELECT FoodJoin.CompanyID, FoodJoin.LocationID, FoodJoin.FoodID, FoodJoin.CategoryID, FoodJoin.NumberFree, FoodJoin.FreeFlag, FoodJoin.GroupFlag, FoodJoin.GTCFlag, FoodJoin.ReqFlag, Category.CategoryID, Category.CategoryAbrev, Category.FunctionID, Category.Priority, Category.HalfSplit, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionID, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag FROM FoodJoin LEFT OUTER JOIN Category ON FoodJoin.CategoryID = Category.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE FoodJoin.CategoryID > 0 AND FoodJoin.CompanyID = '" & companyInfo.CompanyID & "' AND FoodJoin.LocationID = '" & customLocationString & "' ORDER BY Priority ASC"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        '       tableCreating = "IndividualJoin"
        '       sqlStatement = "SELECT FoodJoin.CompanyID, FoodJoin.LocationID, FoodJoin.FoodID, FoodJoin.ModifierID, FoodJoin.NumberFree, FoodJoin.FreeFlag, FoodJoin.GroupFlag, Foods.FoodID, Foods.CategoryID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, Foods.Surcharge, Foods.TaxID, Foods.FoodDesc, Category.FunctionID, Category.Priority, Functions.FunctionGroupID, Functions.FunctionFlag FROM FoodJoin LEFT OUTER JOIN Foods ON FoodJoin.ModifierID = Foods.FoodID LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN Functions ON Category.FunctionID = Functions.FunctionID WHERE FoodJoin.ModifierID > 0 AND Functions.FunctionFlag = 'M' AND FoodJoin.CompanyID = '" & companyInfo.CompanyID & "' AND FoodJoin.LocationID = '" & customLocationString & "' ORDER BY Priority ASC"
        '      ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)
        '        sqlStatement = "SELECT FoodJoin.CompanyID, FoodJoin.LocationID, FoodJoin.FoodID, FoodJoin.ModifierID, FoodJoin.NumberFree, FoodJoin.FreeFlag, FoodJoin.GroupFlag, Foods.FoodID, Foods.CategoryID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, MenuJoin.Surcharge, Foods.TaxID, Foods.FoodDesc, Category.FunctionID, Category.Priority, Functions.FunctionGroupID, Functions.FunctionFlag FROM FoodJoin LEFT OUTER JOIN Foods ON FoodJoin.ModifierID = Foods.FoodID LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN Functions ON Category.FunctionID = Functions.FunctionID LEFT OUTER JOIN MenuJoin ON FoodJoin.ModifierID = MenuJoin.FoodID WHERE FoodJoin.ModifierID > 0 AND MenuJoin.MenuID = 1 AND MenuJoin.GeneralMenuID IS NULL AND NOT Functions.FunctionFlag = 'M' AND FoodJoin.CompanyID = '" & companyInfo.CompanyID & "' AND FoodJoin.LocationID = '" & customLocationString & "' ORDER BY Priority ASC"
        '       ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        tableCreating = "DrinkSubCategory"
        sqlStatement = "SELECT DrinkCategory.DrinkCategoryID, DrinkCategory.DrinkCategoryName, DrinkCategory.DrinkCategoryNumber, DrinkCategory.ButtonColor, DrinkCategory.ButtonForeColor, DrinkCategory.IsALiquorType, MenuDetail.MenuID, MenuDetail.OrderIndex FROM DrinkCategory LEFT OUTER JOIN MenuDetail ON DrinkCategory.DrinkCategoryID = MenuDetail.DrinkCategoryID WHERE DrinkCategory.CompanyID = '" & companyInfo.CompanyID & "' AND DrinkCategory.LocationID = '" & customLocationString & "' AND DrinkCategory.DrinkCategoryID > 0"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        tableCreating = "Drink"
        sqlStatement = "SELECT Drinks.DrinkID, Drinks.DrinkIndex, Drinks.DrinkName, Drinks.DrinkCategoryID, Drinks.DrinkPrice, Drinks.DrinkFunctionID, Drinks.TaxID, Drinks.DrinkDesc, Drinks.DrinkAddOnChoice, Drinks.IsDrinkAdd, Drinks.IsWineParring, Drinks.LiquorTypeID, Drinks.CallPrice, Drinks.AddOnPrice, Drinks.Active, Drinks.InvMultiplier, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag, AABFunctions.TaxID, AABFunctions.DrinkRoutingID FROM Drinks LEFT OUTER JOIN AABFunctions ON Drinks.DrinkFunctionID = AABFunctions.FunctionID WHERE Drinks.CompanyID = '" & companyInfo.CompanyID & "' AND Drinks.LocationID = '" & customLocationString & "' ORDER BY DrinkIndex"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        tableCreating = "LiquorTypes"
        sqlStatement = "SELECT DrinkCategoryID, DrinkCategoryName, DrinkCategoryOrder FROM DrinkCategory WHERE CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & customLocationString & "' AND IsALiquorType = 1"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        tableCreating = "DrinkAdds"
        sqlStatement = "SELECT Drinks.DrinkID, Drinks.DrinkName, Drinks.DrinkCategoryID, Drinks.DrinkFunctionID, Drinks.AddOnPrice, Drinks.TaxID , Drinks.InvMultiplier, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag, AABFunctions.TaxID, AABFunctions.DrinkRoutingID FROM Drinks LEFT OUTER JOIN AABFunctions ON Drinks.DrinkFunctionID = AABFunctions.FunctionID WHERE Drinks.CompanyID = '" & companyInfo.CompanyID & "' AND Drinks.LocationID = '" & customLocationString & "' AND Drinks.IsDrinkAdd = 1 ORDER BY Drinks.DrinkName"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        '   ***     ***    ***   need to change
        '      tableCreating = "DrinkModifiers"
        '  sqlStatement = "SELECT DrinkModifierID, DrinkID, DrinkName, DrinkPrice, TaxID FROM DrinkModifiers WHERE LocationID = '" & customLocationString & "' ORDER BY DrinkModifierID ASC"
        ' ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        tableCreating = "DrinkPrep"
        sqlStatement = "SELECT DrinkPrepLocation.DrinkPrepID, DrinkPrepLocation.DrinkPrepMethod, DrinkPrepLocation.DrinkPrepPrice, DrinkPrepLocation.Active, DrinkPrepLocation.DrinkPrepOrder, DrinkPrepLocation.InvMultiplier, DrinkPrep.DrinkPrepName FROM DrinkPrepLocation LEFT OUTER JOIN DrinkPrep ON DrinkPrepLocation.DrinkPrepID = DrinkPrep.DrinkPrepID WHERE (DrinkPrepLocation.Active = 1) AND DrinkPrepLocation.LocationID = '" & customLocationString & "' ORDER BY DrinkPrepLocation.DrinkPrepOrder, DrinkPrep.DrinkPrepName"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)


        Dim oRow As DataRow

        Try
            For Each oRow In ds.Tables("AllFoodCategory").Rows
                mainCategoryIDArrayList.Add(oRow("CategoryID"))
                secondaryCategoryIDArrayList.Add(oRow("CategoryID"))
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Exit Sub
        '222 below
        Try
            For Each oRow In ds.Tables("ModifierCategory").Rows
                tableCreating = "ModifierTable" & oRow("CategoryID")
                '444     If oRow("FunctionFlag") = "G" Then
                ' this was an attempt to ask for GeneralMenuID categories in Modifies
                'actually is getting this from FoodJoin in Menu Sub
                '444       sqlStatement = "SELECT Foods.FoodID, Foods.CategoryID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, Foods.Surcharge, Foods.FoodDesc, Foods.PrintPriorityID, Foods.RoutingID, Foods.PrepareTime, Foods.InvMultiplier, MenuJoin.MenuIndex, Category.FunctionID, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionID, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag, AABFunctions.TaxID FROM Foods LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN MenuJoin ON Category.CategoryID = MenuJoin.GeneralMenuID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE MenuJoin.GeneralMenuID = '" & oRow("CategoryID") & "' AND MenuJoin.MenuID = '" & oRow("MenuID") & "' AND Foods.Active = 1 AND (Foods.CompanyID = '" & companyInfo.CompanyID & "') AND (Foods.LocationID = '" & customLocationString & "') ORDER BY Priority ASC"
                '444    Else
                '    sqlStatement = "SELECT Foods.FoodID, Foods.CategoryID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, Foods.Surcharge, Foods.FoodDesc, Foods.PrintPriorityID, Foods.RoutingID, Foods.PrepareTime, Foods.InvMultiplier, Foods.MenuIndex, Category.FunctionID, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionID, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag, AABFunctions.TaxID FROM Foods LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE Foods.MenuIndex > 0 AND Foods.CategoryID = '" & oRow("CategoryID") & "' AND Foods.Active = 1 AND (Foods.CompanyID = '" & companyInfo.CompanyID & "') AND (Foods.LocationID = '" & customLocationString & "') ORDER BY Priority ASC"
                '444    End If
                sqlStatement = "SELECT Foods.FoodID, Foods.CategoryID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, Foods.Surcharge, Foods.FoodDesc, Foods.PrintPriorityID, Foods.RoutingID, Foods.PrepareTime, Foods.InvMultiplier, Foods.MenuIndex, Category.FunctionID, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionID, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag, AABFunctions.TaxID FROM Foods LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE Foods.MenuIndex > 0 AND Foods.CategoryID = '" & oRow("CategoryID") & "' AND Foods.Active = 1 AND (Foods.CompanyID = '" & companyInfo.CompanyID & "') AND (Foods.LocationID = '" & customLocationString & "') ORDER BY Priority ASC"
                ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)
                'below seems to be repeat, "M" is included in AllFoodCategory
                mainCategoryIDArrayList.Add(oRow("CategoryID"))
                secondaryCategoryIDArrayList.Add(oRow("CategoryID"))
                '    DetermineMaxMenuIndex(tableCreating)
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        '999
        'this includes non-main food items for Add/No, only when category flagged 
        tableCreating = "ModifierCategory"
        sqlStatement = "SELECT Category.CompanyID, Category.LocationID, Category.CategoryID, Category.CategoryName, Category.CategoryAbrev, Category.CategoryOrder, Category.FunctionID, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag FROM Category RIGHT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE (AABFunctions.FunctionFlag = 'G' OR AABFunctions.FunctionFlag = 'O') AND Category.DisplayWithAdd = '1' AND Category.CompanyID = '" & companyInfo.CompanyID & "' AND Category.LocationID = '" & customLocationString & "' AND Category.Active = 1 ORDER BY Priority ASC"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

    End Sub

    Public Sub DetermineMaxMenuIndex222(ByVal tableCreating As String)

        '       If ds.Tables(tableCreating).Rows.Count > 0 Then
        'If ds.Tables(tableCreating).Compute("Max(MenuIndex)", "") >= currentTerminal.MaxMenuIndex Then
        '        'currentTerminal.MaxMenuIndex = (ds.Tables(tableCreating).Compute("Max(MenuIndex)", "") + 60)
        '       End If
        '      End If
    End Sub

    Friend Sub PopulateNONOrderTables()

        Dim customLocationString As String
        If companyInfo.usingDefaults = False Then
            customLocationString = companyInfo.LocationID
        Else
            customLocationString = "000000"
        End If


        tableCreating = "TableStatusDesc"
        sqlStatement = "SELECT TableStatusID, TableStatusDesc FROM TableStatusDesc"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        tableCreating = "Tax"
        '  sqlStatement = "SELECT TaxID, TaxName, TaxPercent FROM AABTaxTable WHERE CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & customLocationString & "'"
        sqlStatement = "SELECT TaxID, TaxName, TaxPercent FROM AABTaxTable WHERE TaxID = -1 OR LocationID = '" & customLocationString & "' ORDER BY TaxOrder ASC"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        '       Dim o As DataRow
        '      For Each o In ds.Tables("Tax").Rows
        '     MsgBox(o("TaxID"))
        '    MsgBox(o("TaxName"))
        '   Next
        '     tableCreating = "MenuChoice"
        '    sqlStatement = "SELECT MenuID, MenuName, LastOrder FROM MenuChoice WHERE Active = 1 AND CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & customLocationString & "'"
        '   ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        tableCreating = "RoutingChoice"
        sqlStatement = "SELECT RoutingID, RoutingName, isExpediterPrinter FROM RoutingChoice WHERE isServicePrinter = 1 AND CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & customLocationString & "'"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        tableCreating = "CreditCardDetail"
        sqlStatement = "SELECT PaymentTypeID, PaymentTypeName FROM AABPaymentType" ' WHERE LocationID = '" & customLocationString & "'"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        tableCreating = "TabIdentifier"
        sqlStatement = "SELECT TabIdentifierID, TabSelectorIdentity, TabSelectorOrder FROM TabIdentity WHERE CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & customLocationString & "' ORDER BY TabSelectorOrder ASC"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)


        tableCreating = "ReasonsVoid"
        sqlStatement = "SELECT VoidID, VoidReason, VoidDescription, DisplayOrder, TypeDiscount FROM ReasonsVoid WHERE CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & customLocationString & "' ORDER BY DisplayOrder ASC"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        If ds.Tables("ReasonsVoid").Rows.Count = 0 Then
            'this is for defaults at Location 000000
            tableCreating = "ReasonsVoid"
            sqlStatement = "SELECT VoidID, VoidReason, VoidDescription, DisplayOrder, TypeDiscount FROM ReasonsVoid WHERE LocationID = 000000 ORDER BY DisplayOrder ASC"
            ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)
        End If
        '    reasonsVoid()



        '   need only for Modifying Order with Modify USer Control
        'now i think we only use for Extra / No and Special

        '    sql.cn.Open()   ' we get error in previous sub
        '         sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()

        GenerateOrderTables.sql.SqlSelectCommandMenuRawUsage.Parameters("@LocationID").Value = customLocationString
        GenerateOrderTables.sql.SqlMenuRawUsage.Fill(ds.Tables("Ingredients"))

        sql.SqlSelectCommandFoodTable.Parameters("@LocationID").Value = customLocationString
        sql.SqlFoodTable.Fill(ds.Tables("FoodTable"))
        '      sql.cn.Close()
        sql.SqlSelectCommandDailyShifts.Parameters("@LocationID").Value = customLocationString
        sql.SqlSelectCommandDailyMenuChoice.Parameters("@LocationID").Value = customLocationString

        sql.SqlDailyShifts.Fill(ds.Tables("ShiftCodes"))
        sql.SqlDailyMenuChoice.Fill(ds.Tables("MenuChoice"))

        PopulatePromoTables()

        '      tableCreating = "Functions"
        '     sqlStatement = "SELECT FunctionJoin.FunctionJoinID, FunctionJoin.CompanyID, FunctionJoin.LocationID, FunctionJoin.TaxID, FunctionJoin.DrinkRoutingID, Functions.FunctionGroupID, Functions.FunctionName, Functions.FunctionFlag FROM FunctionJoin LEFT OUTER JOIN Functions ON FunctionJoin.FunctionID = Functions.FunctionID WHERE CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & companyInfo.LocationID & "'"
        '    dsOrder = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, dsOrder)

        '       tableCreating = "ByServerLocal"
        '      sqlStatement = "SELECT ByServerCategory, Dollar1, Dollar2, Sales1, Sales2, Time1 FROM ByServer"
        '     ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds, True)

    End Sub

    Friend Sub PopulatePromoTables()
        '*************
        '   we need to move this to the opening of the app
        '   only do this once

        Dim customLocationString As String
        If companyInfo.usingDefaults = False Then
            customLocationString = companyInfo.LocationID
        Else
            customLocationString = "000000"
        End If

        tableCreating = "Promotion"
        '  sqlStatement = "SELECT PromoID, PromoName, BSGS, Combo, Coupon, MaxAmount, MaxCheck, MaxTable, TaxPromoAmount, TaxFoodCost, EstFoodCost, GuestPayTax, ManagerLevel, TipPromo FROM Promotion WHERE CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & customLocationString & "' AND StartDate < = '" & Today & "' AND EndDate > = '" & Today & "' AND Active = 1 OR StartDate IS NUll AND EndDate IS NULL"
        sqlStatement = "SELECT PromoID, PromoName, BSGS, Combo, Coupon, MaxAmount, MaxCheck, MaxTable, TaxPromoAmount, TaxFoodCost, EstFoodCost, GuestPayTax, ManagerLevel, TipPromo FROM Promotion WHERE CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & customLocationString & "' AND (StartDate < = '" & Today & "' AND EndDate > = '" & Today & "' OR Active = 1)" ' OR StartDate IS NUll AND EndDate IS NULL"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)
        ds.Tables("Promotion").PrimaryKey = New DataColumn() {ds.Tables("Promotion").Columns("PromoID")}

        tableCreating = "BSGS"
        sqlStatement = "SELECT PromoBSGS.PromoID, PromoBSGS.BuyFD_flag, PromoBSGS.BuyCategoryID, PromoBSGS.BuyCategoryAmount, PromoBSGS.BuyDrinkCategoryID, PromoBSGS.GetFD_flag, PromoBSGS.GetCategoryID, PromoBSGS.GetCategoryAmount, PromoBSGS.GetQuantityDiscount, PromoBSGS.GetDrinkCategoryID, Promotion.BSGS FROM PromoBSGS INNER JOIN Promotion ON PromoBSGS.PromoID = Promotion.PromoID WHERE PromoBSGS.CompanyID = '" & companyInfo.CompanyID & "' AND PromoBSGS.LocationID = '" & customLocationString & "' AND Promotion.BSGS = 1"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        tableCreating = "Combo"
        sqlStatement = "SELECT PromoCombo.PromoID, PromoCombo.ComboFD_flag, PromoCombo.ComboCategoryID, PromoCombo.ComboCategoryMax, PromoCombo.ComboDrinkCategoryID, PromoCombo.ComboDrinkCategoryMax, Promotion.Combo FROM PromoCombo INNER JOIN Promotion ON PromoCombo.PromoID = Promotion.PromoID WHERE PromoCombo.CompanyID = '" & companyInfo.CompanyID & "' AND PromoCombo.LocationID = '" & customLocationString & "' AND Promotion.Combo = 1"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        tableCreating = "ComboDetail"
        sqlStatement = "SELECT PromoComboDetail.PromoID, PromoComboDetail.ComboPrice, Promotion.Combo FROM PromoComboDetail INNER JOIN Promotion ON PromoComboDetail.PromoID = Promotion.PromoID WHERE PromoComboDetail.CompanyID = '" & companyInfo.CompanyID & "' AND PromoComboDetail.LocationID = '" & customLocationString & "' AND Promotion.Combo = 1"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)
        ds.Tables("ComboDetail").PrimaryKey = New DataColumn() {ds.Tables("ComboDetail").Columns("PromoID")}

        tableCreating = "Coupon"
        sqlStatement = "SELECT PromoCoupon.PromoID, PromoCoupon.DiscountFD_flag, PromoCoupon.DiscountCategoryID, PromoCoupon.DiscountCategoryAmount, PromoCoupon.DiscountDrinkCategoryID, PromoCoupon.AtleastFD_flag, PromoCoupon.AtleastCategoryID, PromoCoupon.AtleastCategoryAmount, PromoCoupon.AtleastDrinkCategoryID, PromoCoupon.CouponDollarFlag, PromoCoupon.CouponDollarAmount, PromoCoupon.CouponPercentFlag, PromoCoupon.CouponPercentAmount, Promotion.Coupon FROM PromoCoupon INNER JOIN Promotion ON PromoCoupon.PromoID = Promotion.PromoID WHERE PromoCoupon.CompanyID = '" & companyInfo.CompanyID & "' AND PromoCoupon.LocationID = '" & customLocationString & "' AND Promotion.Coupon = 1"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

    End Sub

    Friend Sub PopulateTerminalData()

        '     dsSetup.Tables("TermGroups").Clear()
        '    dsSetup.Tables("Terminals").Clear()
        '   dsSetup.Tables("TerminalsUseOrder").Clear()

        ds.Tables("TermsFloor").Clear()
        ds.Tables("TermsTables").Clear()
        ds.Tables("TermsWalls").Clear()

        '     sql.cn.Open()
        '          sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()

        sql.SqlSelectCommandTermsFloor.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandTermsTables.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandTermsWalls.Parameters("@LocationID").Value = companyInfo.LocationID

        sql.SqlSelectCommandTerms.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandTermsUse.Parameters("@LocationID").Value = companyInfo.LocationID

        sql.SqlTermsFloor.Fill(ds.Tables("TermsFloor"))
        sql.SqlTermsTables.Fill(ds.Tables("TermsTables"))
        sql.SqlTermsWalls.Fill(ds.Tables("TermsWalls"))

        sql.SqlTerms.Fill(ds.Tables("TerminalsMethod"))
        sql.SqlTermsUse.Fill(ds.Tables("TerminalsUseOrder"))
        '       sql.cn.Close()

        TestArray()


    End Sub

    Friend Sub JustTestingTermsTables222()
        Dim oRow As DataRow

        For Each oRow In ds.Tables("TermsTables").Rows
            If oRow("TableNumber") = 12 Then

                '  don't think I need  Flag for new exp
                Exit For
            End If
        Next

    End Sub

    Friend Sub PopulateEmployeeData()

        dsEmployee.Tables("AllEmployees").Clear()
        dsEmployee.Tables("JobCodeInfo").Clear()
        dsStarter.Tables("StarterAllEmployees").Clear()
        dsStarter.Tables("StarterJobCodeInfo").Clear()

        sql.SqlSelectCommandEmployeesAll.Parameters("@CompanyID").Value = companyInfo.CompanyID
        sql.SqlSelectCommandEmployeesAll.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandJobCodeInfo.Parameters("@CompanyID").Value = companyInfo.CompanyID
        sql.SqlSelectCommandJobCodeInfo.Parameters("@LocationID").Value = companyInfo.LocationID

        'opened before we get here
        Try
            sql.SqlDataAdapterEmployeesAll.Fill(dsEmployee.Tables("AllEmployees"))
            sql.SqlDataAdapterJobCodeInfo.Fill(dsEmployee.Tables("JobCodeInfo"))

            sql.SqlDataAdapterEmployeesAll.Fill(dsStarter.Tables("StarterAllEmployees"))
            sql.SqlDataAdapterJobCodeInfo.Fill(dsStarter.Tables("StarterJobCodeInfo"))

        Catch ex As Exception
          
        End Try

    End Sub

    Friend Sub ClearEmployeeCollections()

        currentManagers.Clear()
        currentServers.Clear()
        currentBartenders.Clear()
        todaysFloorPersonnel.Clear()
        AllEmployees.Clear()
        allFloorPersonnel.Clear()
        SalariedEmployees.Clear()
        workingEmployees.Clear()

    End Sub

    Friend Sub PopulateAllEmployeeColloection()
        Dim oRow As DataRow
        Dim newEmployee As Employee
        Dim tempDT As New DataTable
        Dim isFloor As Boolean

        ClearEmployeeCollections()

        If dsEmployee.Tables("AllEmployees").Rows.Count > 0 Then
            tempDT = dsEmployee.Tables("AllEmployees")
        Else
            tempDT = dsStarter.Tables("StarterAllEmployees")
        End If

        If tempDT Is Nothing Then
            ' we have no employees
            MsgBox("There are no employees in the system. Spider POS can't operate without employees.")
        End If


        For Each oRow In tempDT.Rows  'dsEmployee.Tables("AllEmployees").Rows '
            isFloor = False
            newEmployee = New Employee

            newEmployee.EmployeeID = oRow("EmployeeID")
            newEmployee.EmployeeNumber = oRow("EmployeeNumber")
            newEmployee.FullName = oRow("FirstName") & " " & oRow("LastName")
            If oRow("NickName") Is DBNull.Value Then
                newEmployee.NickName = oRow("FirstName")
            Else
                newEmployee.NickName = oRow("NickName")
            End If
            If newEmployee.NickName.Length < 1 Then
                newEmployee.NickName = oRow("FirstName")
            End If
            newEmployee.PasscodeID = oRow("Passcode")
            If Not oRow("SwipeCode") Is DBNull.Value Then
                newEmployee.SwipeCode = oRow("SwipeCode")
            Else
                newEmployee.SwipeCode = "no swipe"
            End If
            newEmployee.Training = oRow("Training")
            newEmployee.ClockInReq = oRow("ClockInReq")
            newEmployee.ReportMgmtAll = oRow("ReportMgmtAll")
            newEmployee.ReportMgmtLimited = oRow("ReportMgmtLimited")
            newEmployee.OperationMgmtAll = oRow("OperationMgmtAll")
            newEmployee.OperationMgmtLimited = oRow("OperationMgmtLimited")
            newEmployee.SystemMgmtAll = oRow("SystemMgmtAll")
            newEmployee.SystemMgmtLimited = oRow("SystemMgmtLimited")
            newEmployee.EmployeeMgmtAll = oRow("EmployeeMgmtAll")
            newEmployee.EmployeeMgmtLimited = oRow("EmployeeMgmtLimited")
            '   this makes the first time for ewach day password is req
            newEmployee.PasswordReq = True

            If Not oRow("JobCodeID1") Is DBNull.Value Then
                newEmployee.JobCode1 = oRow("JobCodeID1")
                newEmployee.JobRate1 = oRow("JobRate1")
                newEmployee.JobName1 = oRow("JobName1")
                If isFloor = False Then
                    isFloor = TestIfFloorPersonnel(newEmployee.JobCode1)
                End If
            End If
            If Not oRow("JobCodeID2") Is DBNull.Value Then
                newEmployee.JobCode2 = oRow("JobCodeID2")
                newEmployee.JobRate2 = oRow("JobRate2")
                newEmployee.JobName2 = oRow("JobName2")
                If isFloor = False Then
                    isFloor = TestIfFloorPersonnel(newEmployee.JobCode2)
                End If
            End If
            If Not oRow("JobCodeID3") Is DBNull.Value Then
                newEmployee.JobCode3 = oRow("JobCodeID3")
                newEmployee.JobRate3 = oRow("JobRate3")
                newEmployee.JobName3 = oRow("JobName3")
                If isFloor = False Then
                    isFloor = TestIfFloorPersonnel(newEmployee.JobCode3)
                End If
            End If
            If Not oRow("JobCodeID4") Is DBNull.Value Then
                newEmployee.JobCode4 = oRow("JobCodeID4")
                newEmployee.JobRate4 = oRow("JobRate4")
                newEmployee.JobName4 = oRow("JobName4")
                If isFloor = False Then
                    isFloor = TestIfFloorPersonnel(newEmployee.JobCode4)
                End If
            End If
            If Not oRow("JobCodeID5") Is DBNull.Value Then
                newEmployee.JobCode5 = oRow("JobCodeID5")
                newEmployee.JobRate5 = oRow("JobRate5")
                newEmployee.JobName5 = oRow("JobName5")
                If isFloor = False Then
                    isFloor = TestIfFloorPersonnel(newEmployee.JobCode5)
                End If
            End If
            If Not oRow("JobCodeID6") Is DBNull.Value Then
                newEmployee.JobCode6 = oRow("JobCodeID6")
                newEmployee.JobRate6 = oRow("JobRate6")
                newEmployee.JobName6 = oRow("JobName6")
                If isFloor = False Then
                    isFloor = TestIfFloorPersonnel(newEmployee.JobCode6)
                End If
            End If
            If Not oRow("JobCodeID7") Is DBNull.Value Then
                newEmployee.JobCode7 = oRow("JobCodeID7")
                newEmployee.JobRate7 = oRow("JobRate7")
                newEmployee.JobName7 = oRow("JobName7")
                If isFloor = False Then
                    isFloor = TestIfFloorPersonnel(newEmployee.JobCode7)
                End If
            End If
            If Not oRow("Lefty") Is DBNull.Value Then
                newEmployee.Lefty = oRow("Lefty")
            Else
                newEmployee.Lefty = False
            End If

            AddEmployeeToAllEmployeeCollection(newEmployee)
            If isFloor = True And Not newEmployee.EmployeeID = 6986 Then
                AddEmployeeToAllFloorCollection(newEmployee)
            End If
            If Not newEmployee.SwipeCode = "no swipe" Then
                AddEmployeeToSwipeCodeEmployeesEmployeeCollection(newEmployee)
                'we are alos populating the same collection in ReadAuth_MWE
                'we can remove this after we go to onlyReadAuth_MWE
            End If

            If newEmployee.ClockInReq = False Then
                '     If Not newEmployee.EmployeeID = 6986 Then
                newEmployee.JobCodeID = newEmployee.JobCode1
                '   Else
                '   newEmployee.Bartender = True
                '   End If

                '************
                'currently only managers can not req a Clock In
                AddEmployeeToSalariedEmployeeCollection(newEmployee)
                FillJobCodeInfo(newEmployee, newEmployee.JobCodeID)
                AddEmployeeToCollections(newEmployee)

            End If

            If typeProgram = "Online_Demo" And Not newEmployee.EmployeeID = 6986 Then
                Demo_LoadJobCodeFunctions(newEmployee)
                GenerateWorkingCollections.AddEmployeeToCollections(newEmployee)
            End If
        Next

    End Sub

    Private Function TestIfFloorPersonnel(ByVal jID As Integer)

        Dim isFloor As Boolean = False
        Dim oRow As DataRow

        For Each oRow In dsEmployee.Tables("JobCodeInfo").Rows
            If oRow("JobCodeID") = jID Then
                If oRow("Manager") = True Or oRow("Bartender") = True Or oRow("Server") = True Or oRow("Cashier") = True Then
                    isFloor = True
                    Exit For
                End If
            End If
        Next

        Return isFloor

    End Function

    Private Sub TestArray()

        If ds.Tables("TermsTables").Rows.Count > numberOfTables Then
            numberOfTables = ds.Tables("TermsTables").Rows.Count
            ReDim btnTable(numberOfTables)
        End If

        If ds.Tables("TermsWalls").Rows.Count > numberOfWalls Then
            numberOfWalls = ds.Tables("TermsWalls").Rows.Count
            ReDim btnWall(numberOfWalls)
        End If

    End Sub

    Friend Sub GenerateOverrideCodes()

        '   2       All     (most restrictive)
        '   1       Limited
        '   0       Nothing

        systemAuthorization = New OverrideSystemCode

        systemAuthorization.VoidItem = 2
        systemAuthorization.ForcePrice = 1
        systemAuthorization.CompItem = 1
        systemAuthorization.TaxExempt = 1
        systemAuthorization.ReopenCheck = 2
        systemAuthorization.VoidCheck = 2
        systemAuthorization.AdjustPayment = 2
        systemAuthorization.AssignComps = 2
        systemAuthorization.AssignGratuity = 2
        systemAuthorization.TransferItem = 1
        systemAuthorization.TransferCheck = 1
        systemAuthorization.ReprintCheck = 1
        systemAuthorization.ReprintOrder = 2
        systemAuthorization.ReprintCredit = 2



    End Sub

    Friend Sub AssignManagementAuthorization(ByRef empAuth As Employee)

        '   employeeAuthorization = New ManagementAuthorization

        employeeAuthorization.FullName = empAuth.FullName

        If empAuth.OperationMgmtAll = True Then
            employeeAuthorization.OperationLevel = 2
        ElseIf empAuth.OperationMgmtLimited = True Then
            employeeAuthorization.OperationLevel = 1
        Else
            employeeAuthorization.OperationLevel = 0
        End If

        If empAuth.EmployeeMgmtAll = True Then
            employeeAuthorization.EmployeeLevel = 2
        ElseIf empAuth.EmployeeMgmtLimited = True Then
            employeeAuthorization.EmployeeLevel = 1
        Else
            employeeAuthorization.EmployeeLevel = 0
        End If

        If empAuth.ReportMgmtAll = True Then
            employeeAuthorization.ReportLevel = 2
        ElseIf empAuth.ReportMgmtLimited = True Then
            employeeAuthorization.ReportLevel = 1
        Else
            employeeAuthorization.ReportLevel = 0
        End If

        If empAuth.SystemMgmtAll = True Then
            employeeAuthorization.SystemLevel = 2
        ElseIf empAuth.SystemMgmtLimited = True Then
            employeeAuthorization.SystemLevel = 1
        Else
            employeeAuthorization.SystemLevel = 0
        End If

        '   do not need below anymore
        '      employeeAuthorization.OperationAll = empAuth.OperationMgmtAll
        '      employeeAuthorization.OperationLimited = empAuth.OperationMgmtLimited
        '      employeeAuthorization.EmployeeAll = empAuth.EmployeeMgmtAll
        '     employeeAuthorization.EmployeeLimited = empAuth.EmployeeMgmtLimited
        '     employeeAuthorization.ReportAll = empAuth.ReportMgmtAll
        ''     employeeAuthorization.ReportLimited = empAuth.ReportMgmtLimited
        '    employeeAuthorization.SystemAll = empAuth.SystemMgmtAll
        '   employeeAuthorization.SystemLimited = empAuth.SystemMgmtLimited

    End Sub

    '  Friend Sub PopulateAvailTables(ByVal empID As Integer)

    ' '     tableCreating = "AvailTables"
    '     sqlStatement = "SELECT TableStatus.TableNumber, TableStatus.SatTime, TableStatus.LastStatus, TableStatus.LastStatusTime, TableStatus.AverageDollar FROM TableStatus WHERE EmployeeID ='" & empID & "'"
    '    dsOrder = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, dsOrder)

    '   tableCreating = "AvailTabs"
    '   sqlStatement = "SELECT TabID, TabName, MenuID, EmployeeID, NumberOfCustomers, NumberOfChecks, SatTime, CloseTime, LastStatus, LastStatusTime, AverageDollar FROM TabStatus WHERE EmployeeID ='" & empID & "'"
    '   dsOrder = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, dsOrder)

    '  End Sub


    '   not sure if needed
    Public Sub SetUpPrimaryKeys()

        '   we must clone before we add primary keys, since all terminal tables do not have pk's
        '  don't use  dtOpenOrdersTerminal = dtOpenOrders.Clone
        '      dtPaymentsAndCreditsTerminal = dtPaymentsAndCredits.Clone

        If Not typeProgram = "Online_Demo" Then
            dsOrder.Tables("OpenOrders").PrimaryKey = New DataColumn() {dsOrder.Tables("OpenOrders").Columns("sin")}
        End If
        ' don't use      dsBackup.Tables("OpenOrdersTerminal").PrimaryKey = New DataColumn() {dsBackup.Tables("OpenOrdersTerminal").Columns("OpenOrderTerminalID")}
        ds.Tables("Tax").PrimaryKey = New DataColumn() {ds.Tables("Tax").Columns("TaxID")}
        dsOrder.Tables("Functions").PrimaryKey = New DataColumn() {dsOrder.Tables("Functions").Columns("FunctionID")}


        '       dsBackup.Tables("AvailTablesTerminal").PrimaryKey = New DataColumn() {dsBackup.Tables("AvailTablesTerminal").Columns("ExperienceNumber")}
        '      dsBackup.Tables("AvailTabsTerminal").PrimaryKey = New DataColumn() {dsBackup.Tables("AvailTabsTerminal").Columns("ExperienceNumber")}
        '      ds.Tables("Ingredients").PrimaryKey = New DataColumn() {ds.Tables("Ingredients").Columns("RawItemID")}

    End Sub

    Friend Sub SetAutoIncrement222()

        '????   don't we want auto increment when down ?????
        dsBackup.Tables("AvailTablesTerminal").Columns("ExperienceNumber").AutoIncrement = True
        dsBackup.Tables("AvailTabsTerminal").Columns("ExperienceNumber").AutoIncrement = True
        '      dsBackup.Tables("OpenOrdersTerminal").Columns("OpenOrderID").AutoIncrement = True

    End Sub

    Friend Sub FillLocationOverviewData(ByRef oRow As DataRow)

        Try
            companyInfo.CompanyID = oRow("CompanyID")
            companyInfo.LocationID = oRow("LocationID")
            companyInfo.companyName = oRow("CompanyName")
            If Not oRow("LocationName") Is DBNull.Value Then
                companyInfo.locationName = oRow("LocationName")
            End If
            companyInfo.locationUsername = oRow("Username")
            companyInfo.locationPassword = oRow("Password")
            companyInfo.locationCity = oRow("City")
            companyInfo.locationState = oRow("State")
            companyInfo.address1 = oRow("Address1")
            companyInfo.address2 = oRow("Address2")
            companyInfo.locationPhone = oRow("PhoneNumber")
            companyInfo.usingDefaults = oRow("UsingDefaults")
            companyInfo.autoPrint = oRow("AutoPrint")
            companyInfo.endOfWeek = oRow("EndOfWeek")
            companyInfo.overtimeRate = oRow("OvertimeRate")
            companyInfo.onlyOneLocation = oRow("OnlyOneLocation")
            If Not oRow("Processor") Is DBNull.Value Then
                companyInfo.processor = oRow("Processor")
            End If
            If Not oRow("MerchantID") Is DBNull.Value Then
                companyInfo.merchantID = oRow("MerchantID")
            End If
            If Not oRow("MerchantIDPhone") Is DBNull.Value Then
                companyInfo.merchantIDPhone = oRow("MerchantIDPhone")
            End If
            If Not oRow("OperatorID") Is DBNull.Value Then
                companyInfo.operatorID = oRow("OperatorID")
            End If

            companyInfo.ClientID = oRow("ClientID")
            companyInfo.UserID = oRow("UserID")
            companyInfo.UserPW = oRow("UserPW")
            companyInfo.IPAddress = oRow("IPAddress")
            If Not oRow("LocalHostName") Is DBNull.Value Then
                companyInfo.localHostName = oRow("LocalHostName")
            End If
            If Not oRow("dbName") Is DBNull.Value Then
                companyInfo.dbName = oRow("dbName")
            End If

            companyInfo.numberOfTerminals = oRow("NumberTerminals")
            companyInfo.numberOfFloorPlans = oRow("NumberFloorPlans")
            companyInfo.timeoutMultiplier = oRow("TimeoutSeconds")
            companyInfo.colorAdjust = oRow("ColorAdjust")
            companyInfo.VersionNumber = "Spider POS v" & My.Application.Info.Version.ToString()
            '    MsgBox(companyInfo.VersionNumber)

            If Not oRow("LastUpdate") Is DBNull.Value Then
                companyInfo.lastUpdate = oRow("LastUpdate")
            End If
            companyInfo.autoUpdate = oRow("AutoUpdate")
            companyInfo.usingBartenderMethod = oRow("UsingBartenderMethod")
            companyInfo.calculateAvgByEntrees = oRow("CalculateAvgByEntrees")
            companyInfo.isKitchenExpiditer = oRow("IsKitchenExpiditer")
            companyInfo.isDelivery = oRow("IsDelivery")
            companyInfo.autoCloseCheck = oRow("AutoCloseCheck")
            companyInfo.usingOutsideCreditProcessor = oRow("UsingOtherCreditProcessor")
            companyInfo.autoReleaseExperience = oRow("AutoReleaseExperience") 'True
            companyInfo.fastCashClose = oRow("FastCashClose")
            companyInfo.servesMixedDrinks = oRow("ServesMixedDrinks") 'False
            companyInfo.deliveryCharge = oRow("DeliveryCharge")
            companyInfo.togoCharge = oRow("ToGoCharge")
            companyInfo.autoGratuityPercent = oRow("AutoGratuity")
            companyInfo.autoGratuityNumber = oRow("AutoGratuityNumber")
            companyInfo.salesTax = oRow("SalesTax")
            companyInfo.empDisc = oRow("EmpDisc")

            If Not oRow("ReceiptMessage1") Is DBNull.Value Then
                companyInfo.receiptMessage1 = oRow("ReceiptMessage1")
            End If
            If Not oRow("ReceiptMessage2") Is DBNull.Value Then
                companyInfo.receiptMessage2 = oRow("ReceiptMessage2")
            End If
            If Not oRow("ReceiptMessage3") Is DBNull.Value Then
                companyInfo.receiptMessage3 = oRow("ReceiptMessage3")
            End If
            If Not oRow("CCMessage1") Is DBNull.Value Then
                companyInfo.CCMessage1 = oRow("CCMessage1")
            End If
            If Not oRow("CCMessage2") Is DBNull.Value Then
                companyInfo.CCMessage2 = oRow("CCMessage2")
            End If

            companyInfo.digitsInTicketNumber = oRow("DigitsInTicketNumber")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub



    Friend Sub LoadStarterDataSet()

        'we can try these as groups b/c they will Structure as groups
        '444      Try
        ' we want to fail in the sending sub

        CreateLocationOverviewTableStructure(dtStarterLocationOverview)
        CreateAllFoodCategoryTableStructure(dtStarterAllFoodCategory)
        CreateModifierCategoryTableStructure(dtStarterModifierCategory)
        CreateAllEmployeesTableStructure(dtStarterAllEmployees)
        CreateJobCodeInfoTableStructure(dtStarterJobCodeInfo)

        dsStarter.Clear()

        If typeProgram = "Online_Demo" Then
            dsStarter.ReadXml("StarterMenu.xml", XmlReadMode.ReadSchema)
        Else
            dsStarter.ReadXml("c:\Data Files\spiderPOS\StarterMenu.xml", XmlReadMode.ReadSchema)
        End If

        '      Catch ex As Exception

        '    MsgBox(ex.Message)

        '    End Try

        '       Try
        '       CreateAllEmployeesTableStructure(dtStarterAllEmployees)
        '      CreateJobCodeInfoTableStructure(dtStarterJobCodeInfo)
        '     Catch ex As Exception
        '    End Try

    End Sub


    Friend Sub CreateLocationOverviewTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("CompanyName", Type.GetType("System.String"))
        tName.Columns.Add("LocationName", Type.GetType("System.String"))
        tName.Columns.Add("Username", Type.GetType("System.String"))
        tName.Columns.Add("Password", Type.GetType("System.String"))
        tName.Columns.Add("BackPass", Type.GetType("System.String"))
        tName.Columns.Add("Domain_Report", Type.GetType("System.String"))
        tName.Columns.Add("Hostname_Report", Type.GetType("System.String"))

        tName.Columns.Add("Address1", Type.GetType("System.String"))
        tName.Columns.Add("Address2", Type.GetType("System.String"))
        tName.Columns.Add("City", Type.GetType("System.String"))
        tName.Columns.Add("State", Type.GetType("System.String"))
        tName.Columns.Add("Zip", Type.GetType("System.String"))
        tName.Columns.Add("PhoneNumber", Type.GetType("System.String"))

        tName.Columns.Add("UsingDefaults", Type.GetType("System.Boolean"))
        tName.Columns.Add("AutoPrint", Type.GetType("System.Boolean"))
        tName.Columns.Add("EndOfWeek", Type.GetType("System.Int16"))
        tName.Columns.Add("EndOfNightDecimal", Type.GetType("System.Decimal"))
        tName.Columns.Add("OvertimeHours", Type.GetType("System.Int16"))
        tName.Columns.Add("OvertimeRate", Type.GetType("System.Decimal"))
        tName.Columns.Add("OnlyOneLocation", Type.GetType("System.Boolean"))
        tName.Columns.Add("Processor", Type.GetType("System.String"))
        tName.Columns.Add("MerchantID", Type.GetType("System.String"))
        tName.Columns.Add("MerchantIDPhone", Type.GetType("System.String"))
        tName.Columns.Add("OperatorID", Type.GetType("System.String"))

        tName.Columns.Add("ClientID", Type.GetType("System.String"))
        tName.Columns.Add("UserID", Type.GetType("System.String"))
        tName.Columns.Add("UserPW", Type.GetType("System.String"))
        tName.Columns.Add("IPAddress", Type.GetType("System.String"))
        tName.Columns.Add("LocalHostName", Type.GetType("System.String"))
        tName.Columns.Add("dbName", Type.GetType("System.String"))

        tName.Columns.Add("NumberTerminals", Type.GetType("System.Int16"))
        tName.Columns.Add("NumberFloorPlans", Type.GetType("System.Int16"))
        tName.Columns.Add("TimeoutSeconds", Type.GetType("System.Int16"))
        tName.Columns.Add("ColorAdjust", Type.GetType("System.Int16"))
        tName.Columns.Add("VersionNumber", Type.GetType("System.String"))
        tName.Columns.Add("LastUpdate", Type.GetType("System.DateTime"))
        tName.Columns.Add("AutoUpdate", Type.GetType("System.Boolean"))
        tName.Columns.Add("UsingBartenderMethod", Type.GetType("System.Boolean"))
        tName.Columns.Add("BarDoNotPrintDrinks", Type.GetType("System.Boolean"))
        tName.Columns.Add("CalculateAvgByEntrees", Type.GetType("System.Boolean"))

        tName.Columns.Add("IsKitchenExpiditer", Type.GetType("System.Boolean"))
        tName.Columns.Add("IsDelivery", Type.GetType("System.Boolean"))
        tName.Columns.Add("AutoCloseCheck", Type.GetType("System.Boolean"))
        tName.Columns.Add("UsingOtherCreditProcessor", Type.GetType("System.Boolean"))
        tName.Columns.Add("AutoReleaseExperience", Type.GetType("System.Boolean"))
        tName.Columns.Add("FastCashClose", Type.GetType("System.Boolean"))
        tName.Columns.Add("ServesMixedDrinks", Type.GetType("System.Boolean"))
        tName.Columns.Add("DeliveryCharge", Type.GetType("System.Decimal"))
        tName.Columns.Add("ToGoCharge", Type.GetType("System.Decimal"))
        tName.Columns.Add("AutoGratuity", Type.GetType("System.Decimal"))
        tName.Columns.Add("AutoGratuityNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("SalesTax", Type.GetType("System.Decimal"))
        tName.Columns.Add("EmpDisc", Type.GetType("System.Decimal"))

        tName.Columns.Add("ReceiptMessage1", Type.GetType("System.String"))
        tName.Columns.Add("ReceiptMessage2", Type.GetType("System.String"))
        tName.Columns.Add("ReceiptMessage3", Type.GetType("System.String"))
        tName.Columns.Add("CCMessage1", Type.GetType("System.String"))
        tName.Columns.Add("CCMessage2", Type.GetType("System.String"))
        tName.Columns.Add("DigitsInTicketNumber", Type.GetType("System.Int32"))



    End Sub

    Friend Sub CreateLocationOpeningTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("LocationOpeningPK", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("AppType", Type.GetType("System.String"))
        tName.Columns.Add("LastAppVersion", Type.GetType("System.Int32"))
        tName.Columns.Add("menuVersion", Type.GetType("System.Int32"))
        tName.Columns.Add("empVersion", Type.GetType("System.Int32"))
        tName.Columns.Add("menuChangeDate", Type.GetType("System.DateTime"))
        tName.Columns.Add("empChangeDate", Type.GetType("System.DateTime"))
        tName.Columns.Add("mainTerminalsPrimaryKey", Type.GetType("System.Int32"))

    End Sub


    Friend Sub CreateAllFoodCategoryTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("CategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionID", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        tName.Columns.Add("Extended", Type.GetType("System.Boolean"))
        tName.Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionFlag", Type.GetType("System.String"))
        tName.Columns.Add("TaxID", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreateModifierCategoryTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("CategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("CategoryName", Type.GetType("System.String"))
        tName.Columns.Add("CategoryAbrev", Type.GetType("System.String"))
        tName.Columns.Add("CategoryOrder", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        tName.Columns.Add("Extended", Type.GetType("System.Boolean"))
        tName.Columns.Add("FunctionID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionFlag", Type.GetType("System.String"))

    End Sub



    Friend Sub CreateModifierTableStructure(ByVal tName As String)

        ds.Tables.Add(tName)

        ds.Tables(tName).Columns.Add("FoodID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("CategoryID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("FoodName", Type.GetType("System.String"))
        ds.Tables(tName).Columns.Add("AbrevFoodName", Type.GetType("System.String"))
        ds.Tables(tName).Columns.Add("ChitFoodName", Type.GetType("System.String"))
        ds.Tables(tName).Columns.Add("Surcharge", Type.GetType("System.Decimal"))
        ds.Tables(tName).Columns.Add("FoodDesc", Type.GetType("System.String"))
        ds.Tables(tName).Columns.Add("PrintPriorityID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("RoutingID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("PrepareTime", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("InvMultiplier", Type.GetType("System.Decimal"))
        ds.Tables(tName).Columns.Add("MenuIndex", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("FunctionID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("Extended", Type.GetType("System.Boolean"))
        ds.Tables(tName).Columns.Add("FunctionID1", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("FunctionFlag", Type.GetType("System.String"))
        ds.Tables(tName).Columns.Add("TaxID", Type.GetType("System.Int32"))

    End Sub


    Friend Sub CreateFoodTableTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("FoodID", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("FoodName", Type.GetType("System.String"))
        tName.Columns.Add("AbrevFoodName", Type.GetType("System.String"))
        tName.Columns.Add("ChitFoodName", Type.GetType("System.String"))
        tName.Columns.Add("CategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("TaxExempt", Type.GetType("System.Boolean"))
        tName.Columns.Add("FoodDesc", Type.GetType("System.String"))
        tName.Columns.Add("WineParringID", Type.GetType("System.Int32"))
        tName.Columns.Add("RoutingID", Type.GetType("System.Int32"))
        tName.Columns.Add("PrintPriorityID", Type.GetType("System.Int32"))
        tName.Columns.Add("Active", Type.GetType("System.Boolean"))
        tName.Columns.Add("PrepareTime", Type.GetType("System.Int32"))
        tName.Columns.Add("InvMultiplier", Type.GetType("System.Decimal"))
        tName.Columns.Add("FoodInvOn", Type.GetType("System.Boolean"))
        tName.Columns.Add("FoodInv", Type.GetType("System.Int32"))
        tName.Columns.Add("AvailForExtraNo", Type.GetType("System.Boolean"))
        tName.Columns.Add("FunctionID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionName", Type.GetType("System.String"))
        tName.Columns.Add("FunctionFlag", Type.GetType("System.String"))
        tName.Columns.Add("TaxID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkRoutingID", Type.GetType("System.Int32"))
        tName.Columns.Add("Expr1", Type.GetType("System.Int32"))
        tName.Columns.Add("Expr2", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreateCatJoinTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("FoodID", Type.GetType("System.Int32"))
        tName.Columns.Add("CategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("CategoryAbrev", Type.GetType("System.String"))
        tName.Columns.Add("NumberFree", Type.GetType("System.Int32"))
        tName.Columns.Add("FreeFlag", Type.GetType("System.String"))
        tName.Columns.Add("GroupFlag", Type.GetType("System.String"))
        tName.Columns.Add("GTCFlag", Type.GetType("System.String"))
        tName.Columns.Add("ReqFlag", Type.GetType("System.String"))
        tName.Columns.Add("CategoryID1", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionID", Type.GetType("System.Int32"))
        tName.Columns.Add("Priority", Type.GetType("System.Int16"))
        tName.Columns.Add("HalfSplit", Type.GetType("System.Boolean"))
        tName.Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        tName.Columns.Add("Extended", Type.GetType("System.Boolean"))
        tName.Columns.Add("FunctionID1", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionFlag", Type.GetType("System.String"))

    End Sub

    Friend Sub CreateLiquorTypesTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("DrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkCategoryName", Type.GetType("System.String"))
        tName.Columns.Add("DrinkCategoryOrder", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreateDrinkModifiersTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("DrinkModifierID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkName", Type.GetType("System.String"))
        tName.Columns.Add("DrinkPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("TaxID", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreateDrinkPrepTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("DrinkPrepID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkPrepMethod", Type.GetType("System.String"))
        tName.Columns.Add("DrinkPrepPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("Active", Type.GetType("System.Boolean"))
        tName.Columns.Add("DrinkPrepOrder", Type.GetType("System.Int32"))
        tName.Columns.Add("InvMultiplier", Type.GetType("System.Decimal"))
        tName.Columns.Add("DrinkPrepName", Type.GetType("System.String"))

    End Sub


    Friend Sub CreateTableStatusDescriptionTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("TableStatusID", Type.GetType("System.Int32"))
        tName.Columns.Add("TableStatusDesc", Type.GetType("System.String"))

    End Sub

    Friend Sub CreateTaxTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("TaxID", Type.GetType("System.Int32"))
        tName.Columns.Add("TaxName", Type.GetType("System.String"))
        tName.Columns.Add("TaxPercent", Type.GetType("System.Decimal"))

    End Sub

    Friend Sub CreateMenuChoiceTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("MenuID", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("MenuName", Type.GetType("System.String"))
        tName.Columns.Add("Active", Type.GetType("System.Boolean"))
        tName.Columns.Add("LastOrder", Type.GetType("System.Int32"))
        tName.Columns.Add("AutoChange", Type.GetType("System.DateTime"))

    End Sub

    Friend Sub CreateShiftCodesTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("ShiftID", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("ShiftName", Type.GetType("System.String"))
        tName.Columns.Add("TimeStart", Type.GetType("System.DateTime"))

    End Sub
    Friend Sub CreateRoutingChoiceTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("RoutingID", Type.GetType("System.Int32"))
        tName.Columns.Add("RoutingName", Type.GetType("System.String"))
        tName.Columns.Add("isExpediterPrinter", Type.GetType("System.Boolean"))

    End Sub

    Friend Sub CreateCreditCardDetailTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("PaymentTypeID", Type.GetType("System.Int32"))
        tName.Columns.Add("PaymentTypeName", Type.GetType("System.String"))

    End Sub

    Friend Sub CreateTabIdentifierTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("TabIdentifierID", Type.GetType("System.Int32"))
        tName.Columns.Add("TabSelectorIdentity", Type.GetType("System.String"))
        tName.Columns.Add("TabSelectorOrder", Type.GetType("System.Int16"))

    End Sub

    Friend Sub CreateReasonsVoidTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("VoidID", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("VoidReason", Type.GetType("System.String"))
        tName.Columns.Add("VoidDescription", Type.GetType("System.String"))
        tName.Columns.Add("DisplayOrder", Type.GetType("System.Int16"))
        tName.Columns.Add("TypeDiscount", Type.GetType("System.String"))

    End Sub


    Friend Sub CreateDrinkSubCategoryTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("DrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkCategoryName", Type.GetType("System.String"))
        tName.Columns.Add("DrinkCategoryNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        tName.Columns.Add("IsALiquorType", Type.GetType("System.Boolean"))
        tName.Columns.Add("MenuID", Type.GetType("System.Int32"))
        tName.Columns.Add("OrderIndex", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreateDrinkTableStructure(ByVal tName As DataTable)


        tName.Columns.Add("DrinkID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkIndex", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkName", Type.GetType("System.String"))
        tName.Columns.Add("DrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("DrinkFunctionID", Type.GetType("System.Int32"))
        tName.Columns.Add("TaxID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkDesc", Type.GetType("System.String"))
        tName.Columns.Add("DrinkAddOnChoice", Type.GetType("System.Boolean"))
        tName.Columns.Add("IsDrinkAdd", Type.GetType("System.Boolean"))
        tName.Columns.Add("IsWineParring", Type.GetType("System.Boolean"))
        tName.Columns.Add("LiquorTypeID", Type.GetType("System.Int32"))
        tName.Columns.Add("CallPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("AddOnPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("Active", Type.GetType("System.Boolean"))
        tName.Columns.Add("InvMultiplier", Type.GetType("System.Decimal"))
        tName.Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionFlag", Type.GetType("System.String"))
        tName.Columns.Add("TaxID1", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkRoutingID", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreateDrinkAddsTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("DrinkID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkName", Type.GetType("System.String"))
        tName.Columns.Add("DrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkFunctionID", Type.GetType("System.Int32"))
        tName.Columns.Add("AddOnPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("TaxID", Type.GetType("System.Int32"))
        tName.Columns.Add("InvMultiplier", Type.GetType("System.Decimal"))
        tName.Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionFlag", Type.GetType("System.String"))
        tName.Columns.Add("TaxID1", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkRoutingID", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreatePromotionTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("PromoID", Type.GetType("System.Int32"))
        tName.Columns.Add("PromoName", Type.GetType("System.String"))
        tName.Columns.Add("BSGS", Type.GetType("System.Boolean"))
        tName.Columns.Add("Combo", Type.GetType("System.Boolean"))
        tName.Columns.Add("Coupon", Type.GetType("System.Boolean"))
        tName.Columns.Add("MaxAmount", Type.GetType("System.Decimal"))
        tName.Columns.Add("MaxCheck", Type.GetType("System.Int32"))
        tName.Columns.Add("MaxTable", Type.GetType("System.Int32"))
        tName.Columns.Add("TaxPromoAmount", Type.GetType("System.Boolean"))
        tName.Columns.Add("TaxFoodCost", Type.GetType("System.Boolean"))
        tName.Columns.Add("EstFoodCost", Type.GetType("System.Decimal"))
        tName.Columns.Add("GuestPayTax", Type.GetType("System.Boolean"))
        tName.Columns.Add("ManagerLevel", Type.GetType("System.Int32"))
        tName.Columns.Add("TipPromo", Type.GetType("System.Boolean"))
        tName.PrimaryKey = New DataColumn() {tName.Columns("PromoID")}

    End Sub

    Friend Sub CreateBSGSTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("PromoID", Type.GetType("System.Int32"))
        tName.Columns.Add("BuyFD_flag", Type.GetType("System.String"))
        tName.Columns.Add("BuyCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("BuyCategoryAmount", Type.GetType("System.Int32"))
        tName.Columns.Add("BuyDrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("GetFD_flag", Type.GetType("System.String"))
        tName.Columns.Add("GetCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("GetCategoryAmount", Type.GetType("System.Int32"))
        tName.Columns.Add("GetQuantityDiscount", Type.GetType("System.Decimal"))
        tName.Columns.Add("GetDrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("BSGS", Type.GetType("System.Boolean"))

    End Sub

    Friend Sub CreateComboTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("PromoID", Type.GetType("System.Int32"))
        tName.Columns.Add("ComboFD_flag", Type.GetType("System.String"))
        tName.Columns.Add("ComboCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("ComboCategoryMax", Type.GetType("System.Int32"))
        tName.Columns.Add("ComboDrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("ComboDrinkCategoryMax", Type.GetType("System.Int32"))
        tName.Columns.Add("Combo", Type.GetType("System.Boolean"))

    End Sub

    Friend Sub CreateComboDetailTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("PromoID", Type.GetType("System.Int32"))
        tName.Columns.Add("ComboPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("Combo", Type.GetType("System.Boolean"))

    End Sub

    Friend Sub CreateCouponTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("PromoID", Type.GetType("System.Int32"))
        tName.Columns.Add("DiscountFD_flag", Type.GetType("System.String"))
        tName.Columns.Add("DiscountCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("DiscountCategoryAmount", Type.GetType("System.Int32"))
        tName.Columns.Add("DiscountDrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("AtleastFD_flag", Type.GetType("System.String"))
        tName.Columns.Add("AtleastCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("AtleastCategoryAmount", Type.GetType("System.Int32"))
        tName.Columns.Add("AtleastDrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("CouponDollarFlag", Type.GetType("System.Boolean"))
        tName.Columns.Add("CouponDollarAmount", Type.GetType("System.Decimal"))
        tName.Columns.Add("CouponPercentFlag", Type.GetType("System.Boolean"))
        tName.Columns.Add("CouponPercentAmount", Type.GetType("System.Decimal"))
        tName.Columns.Add("Coupon", Type.GetType("System.Boolean"))

    End Sub

    Friend Sub CreateIngredientsTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("FoodID", Type.GetType("System.Int32"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("RawItemID", Type.GetType("System.Int32"))
        tName.Columns.Add("RawUsageAmount", Type.GetType("System.Decimal"))
        tName.Columns.Add("RawUsageIndex", Type.GetType("System.Int16"))
        tName.Columns.Add("RawCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("RawItemName", Type.GetType("System.String"))
        tName.Columns.Add("PurchaseUnits", Type.GetType("System.String"))
        tName.Columns.Add("PurchaseCost", Type.GetType("System.Decimal"))
        tName.Columns.Add("RecipeUnit", Type.GetType("System.String"))
        tName.Columns.Add("RecipeConversion", Type.GetType("System.Int16"))
        tName.Columns.Add("UnitCost", Type.GetType("System.Decimal"))
        tName.Columns.Add("SelectNo", Type.GetType("System.Boolean"))
        tName.Columns.Add("NoPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("SelectExtra", Type.GetType("System.Boolean"))
        tName.Columns.Add("ExtraPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("PhysicalUnit", Type.GetType("System.String"))
        tName.Columns.Add("PhysicalConversion", Type.GetType("System.Int16"))
        tName.Columns.Add("PhysicalUnitCost", Type.GetType("System.Decimal"))
        tName.Columns.Add("InvDate", Type.GetType("System.DateTime"))
        tName.Columns.Add("InvQuantity", Type.GetType("System.Decimal"))
        tName.Columns.Add("InvDollarAmount", Type.GetType("System.Decimal"))
        tName.Columns.Add("CalculatedBegInvWeek", Type.GetType("System.Decimal"))
        tName.Columns.Add("RecipeQuantity", Type.GetType("System.Decimal"))
        tName.Columns.Add("TrackInvLevels", Type.GetType("System.Boolean"))
        tName.Columns.Add("PrintInReport", Type.GetType("System.Boolean"))
        tName.Columns.Add("TrackAs", Type.GetType("System.Int32"))
        tName.Columns.Add("Warning", Type.GetType("System.Boolean"))
        tName.Columns.Add("WarningLevel", Type.GetType("System.Int16"))
        tName.Columns.Add("TempRecipeQuantity", Type.GetType("System.Int16"))
        tName.Columns.Add("Expr1", Type.GetType("System.Int32"))
        tName.Columns.Add("RawUsageID", Type.GetType("System.Int32"))

    End Sub


    Friend Sub CreateRawCategoryTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("RawCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("RawCategoryName", Type.GetType("System.String"))
        tName.Columns.Add("RawSubCategory", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreateRawMatTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("RawCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("RawItemName", Type.GetType("System.String"))
        tName.Columns.Add("PurchaseUnits", Type.GetType("System.String"))
        tName.Columns.Add("PurchaseCost", Type.GetType("System.decimal"))
        tName.Columns.Add("Blank", Type.GetType("System.String"))
        tName.Columns.Add("RecipeUnit", Type.GetType("System.String"))
        tName.Columns.Add("RecipeConversion", Type.GetType("System.Int16"))
        tName.Columns.Add("UnitCost", Type.GetType("System.Decimal"))
        tName.Columns.Add("SelectNo", Type.GetType("System.Boolean"))
        tName.Columns.Add("NoPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("SelectExtra", Type.GetType("System.Boolean"))
        tName.Columns.Add("ExtraPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("Blank", Type.GetType("System.String"))
        tName.Columns.Add("PhysicalUnit", Type.GetType("System.String"))
        tName.Columns.Add("PhysicalConversion", Type.GetType("System.Int16"))
        tName.Columns.Add("PhysicalUnitCost", Type.GetType("System.Decimal"))
        tName.Columns.Add("InvDate", Type.GetType("System.DateTime"))
        tName.Columns.Add("InvQuantity", Type.GetType("System.Decimal"))
        tName.Columns.Add("InvDollarAmount", Type.GetType("System.Decimal"))
        tName.Columns.Add("CalculatedBegInvWeek", Type.GetType("System.decimal"))
        tName.Columns.Add("RecipeQuantity", Type.GetType("System.Decimal"))
        tName.Columns.Add("TrackInvLevels", Type.GetType("System.Boolean"))
        tName.Columns.Add("PrintInReport", Type.GetType("System.Boolean"))
        tName.Columns.Add("Warning", Type.GetType("System.Boolean"))
        tName.Columns.Add("WarningLevel", Type.GetType("System.Int16"))
        tName.Columns.Add("TempRecipeQuantity", Type.GetType("System.Int16"))
        tName.Columns.Add("Qty", Type.GetType("System.Decimal"))

    End Sub


    Friend Sub CreateRawDeliveryTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("InvFoodDeliveryID", Type.GetType("System.Int64"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("DailyCode", Type.GetType("System.Int64"))
        tName.Columns.Add("ReceivedDate", Type.GetType("System.DateTime"))
        tName.Columns.Add("EmployeeID", Type.GetType("System.Int32"))
        tName.Columns.Add("RawItemID", Type.GetType("System.Int32"))
        tName.Columns.Add("DeliveredQuantity", Type.GetType("System.decimal"))
        tName.Columns.Add("InvCounted", Type.GetType("System.Boolean")) 'it is Boolean here and Int16 in DailyBusiness
        tName.Columns.Add("DeliveryFlag", Type.GetType("System.String"))
        tName.Columns.Add("StoredAt", Type.GetType("System.String"))
        tName.Columns.Add("RawItemName", Type.GetType("System.String"))
        tName.Columns.Add("PurchaseUnits", Type.GetType("System.String"))
        tName.Columns.Add("Blank", Type.GetType("System.String"))


    End Sub







    Friend Sub CreateTerminalsMethodTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("TerminalsPrimaryKey", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("TerminalID", Type.GetType("System.Int32"))
        tName.Columns.Add("TerminalName", Type.GetType("System.String"))
        tName.Columns.Add("TerminalMethod", Type.GetType("System.String"))
        tName.Columns.Add("TerminalsGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FloorPlanID", Type.GetType("System.Int32"))
        tName.Columns.Add("TermX", Type.GetType("System.Int16"))
        tName.Columns.Add("TermY", Type.GetType("System.Int16"))
        tName.Columns.Add("ReceiptPrinterID", Type.GetType("System.Int32"))
        tName.Columns.Add("OpenDefaultSceen", Type.GetType("System.Boolean"))
        tName.Columns.Add("hasCashDrawer", Type.GetType("System.Boolean"))
        tName.Columns.Add("normalOpenAmount", Type.GetType("System.Decimal"))
        tName.Columns.Add("autoOpenDrawer", Type.GetType("System.Boolean"))
        tName.Columns.Add("RoutingName", Type.GetType("System.String"))
        tName.Columns.Add("PhysicalAddress", Type.GetType("System.String"))

    End Sub

    Friend Sub CreateTerminalsUseOrderTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("TerminalsMethodKey", Type.GetType("System.Int32"))
        tName.Columns.Add("TerminalsPrimaryKey", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("MethodUse", Type.GetType("System.String"))
        tName.Columns.Add("MethodDirection", Type.GetType("System.String"))
        tName.Columns.Add("UsePriority", Type.GetType("System.Int32"))
        tName.Columns.Add("Active", Type.GetType("System.Boolean"))

    End Sub

    Friend Sub CreateTermsFloorTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("FloorPlanID", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("FloorPlanName", Type.GetType("System.String"))
        tName.Columns.Add("FloorPlanOrder", Type.GetType("System.Int16"))
        tName.Columns.Add("FloorPlanVisible", Type.GetType("System.Boolean"))
        tName.Columns.Add("FloorPlanActive", Type.GetType("System.Boolean"))
        tName.Columns.Add("meWidth", Type.GetType("System.Int16"))
        tName.Columns.Add("meHeight", Type.GetType("System.Int16"))

    End Sub


    Friend Sub CreateTermsTablesTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("TableOverviewID", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("TableNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("FloorPlanID", Type.GetType("System.Int32"))
        tName.Columns.Add("TableSectionID", Type.GetType("System.Int32"))
        tName.Columns.Add("Seats", Type.GetType("System.Int32"))
        tName.Columns.Add("Available", Type.GetType("System.Boolean"))
        tName.Columns.Add("isTable", Type.GetType("System.Boolean"))
        tName.Columns.Add("isWall", Type.GetType("System.Boolean"))
        tName.Columns.Add("isOther", Type.GetType("System.Boolean"))
        tName.Columns.Add("isRound", Type.GetType("System.Boolean"))
        tName.Columns.Add("xLoc", Type.GetType("System.Int16"))
        tName.Columns.Add("yLoc", Type.GetType("System.Int16"))
        tName.Columns.Add("myWidth", Type.GetType("System.Int16"))
        tName.Columns.Add("myHeight", Type.GetType("System.Int16"))
        '444     tName.Columns.Add("Active", Type.GetType("System.Boolean"))
        tName.Columns.Add("FloorPlanName", Type.GetType("System.String"))
        tName.Columns.Add("OpenBigInt1", Type.GetType("System.Int64"))


    End Sub

    Friend Sub CreateTermsWallsTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("TableOverviewID", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("TableNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("FloorPlanID", Type.GetType("System.Int32"))
        tName.Columns.Add("isTable", Type.GetType("System.Boolean"))
        tName.Columns.Add("isWall", Type.GetType("System.Boolean"))
        tName.Columns.Add("isOther", Type.GetType("System.Boolean"))
        tName.Columns.Add("isRound", Type.GetType("System.Boolean"))
        tName.Columns.Add("xLoc", Type.GetType("System.Int16"))
        tName.Columns.Add("yLoc", Type.GetType("System.Int16"))
        tName.Columns.Add("myWidth", Type.GetType("System.Int16"))
        tName.Columns.Add("myHeight", Type.GetType("System.Int16"))
        tName.Columns.Add("Active", Type.GetType("System.Boolean"))

    End Sub

    Friend Sub CreateMainCatTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("CategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("CategoryName", Type.GetType("System.String"))
        tName.Columns.Add("CategoryAbrev", Type.GetType("System.String"))
        tName.Columns.Add("FunctionID", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        tName.Columns.Add("Extended", Type.GetType("System.Boolean"))
        tName.Columns.Add("MenuID", Type.GetType("System.Int32"))
        tName.Columns.Add("OrderIndex", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionFlag", Type.GetType("System.String"))

    End Sub

    Friend Sub CreateMainDrinkCatTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("DrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("DrinkCategoryNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkCategoryName", Type.GetType("System.String"))
        tName.Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        tName.Columns.Add("IsALiquorType", Type.GetType("System.Boolean"))
        tName.Columns.Add("MenuID", Type.GetType("System.Int32"))
        tName.Columns.Add("OrderIndex", Type.GetType("System.Int32"))

    End Sub


    Friend Sub CreateIndJoinTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("FoodID", Type.GetType("System.Int32"))
        tName.Columns.Add("ModifierID", Type.GetType("System.Int32"))
        tName.Columns.Add("NumberFree", Type.GetType("System.Int32"))
        tName.Columns.Add("FreeFlag", Type.GetType("System.String"))
        tName.Columns.Add("GroupFlag", Type.GetType("System.String"))
        '      tName.Columns.Add("FoodID1", Type.GetType("System.Int32"))
        tName.Columns.Add("CategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("FoodName", Type.GetType("System.String"))
        tName.Columns.Add("AbrevFoodName", Type.GetType("System.String"))
        tName.Columns.Add("ChitFoodName", Type.GetType("System.String"))
        tName.Columns.Add("Surcharge", Type.GetType("System.Decimal"))
        tName.Columns.Add("TaxID", Type.GetType("System.Int32"))
        tName.Columns.Add("FoodDesc", Type.GetType("System.String"))
        tName.Columns.Add("MenuIndex", Type.GetType("System.Int32"))
        tName.Columns.Add("InvMultiplier", Type.GetType("System.Decimal"))
        tName.Columns.Add("FunctionID", Type.GetType("System.Int32"))
        tName.Columns.Add("Priority", Type.GetType("System.Int16"))
        tName.Columns.Add("HalfSplit", Type.GetType("System.Boolean"))
        tName.Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        tName.Columns.Add("Extended", Type.GetType("System.Boolean"))
        tName.Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionFlag", Type.GetType("System.String"))

    End Sub


    Friend Sub CreateBarCatTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("CategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("CategoryName", Type.GetType("System.String"))
        tName.Columns.Add("CategoryAbrev", Type.GetType("System.String"))
        tName.Columns.Add("FunctionID", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        tName.Columns.Add("Extended", Type.GetType("System.Boolean"))
        tName.Columns.Add("BartenderMenuID", Type.GetType("System.Int32"))
        tName.Columns.Add("OrderIndex", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionFlag", Type.GetType("System.String"))
        tName.Columns.Add("TaxID", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreateBarDrinkCatTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("DrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("DrinkCategoryNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkCategoryName", Type.GetType("System.String"))
        tName.Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        tName.Columns.Add("IsALiquorType", Type.GetType("System.Boolean"))
        tName.Columns.Add("BartenderMenuID", Type.GetType("System.Int32"))
        tName.Columns.Add("OrderIndex", Type.GetType("System.Int32"))

    End Sub


    Friend Sub CreateMainTableStructure(ByVal tName As String)

        ds.Tables.Add(tName)

        ds.Tables(tName).Columns.Add("FoodID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("FoodName", Type.GetType("System.String"))
        ds.Tables(tName).Columns.Add("AbrevFoodName", Type.GetType("System.String"))
        ds.Tables(tName).Columns.Add("ChitFoodName", Type.GetType("System.String"))
        ds.Tables(tName).Columns.Add("CategoryID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("FoodCost", Type.GetType("System.Decimal"))
        ds.Tables(tName).Columns.Add("TaxExempt", Type.GetType("System.Boolean"))
        ds.Tables(tName).Columns.Add("FoodDesc", Type.GetType("System.String"))
        ds.Tables(tName).Columns.Add("WineParringID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("PrintPriorityID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("PrepareTime", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("InvMultiplier", Type.GetType("System.Decimal"))
        ds.Tables(tName).Columns.Add("FoodID1", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("MenuID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("Price", Type.GetType("System.Decimal"))
        ds.Tables(tName).Columns.Add("RoutingID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("Surcharge", Type.GetType("System.Decimal"))
        ds.Tables(tName).Columns.Add("MenuIndex", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("FunctionID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("HalfSplit", Type.GetType("System.Boolean"))
        ds.Tables(tName).Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("Extended", Type.GetType("System.Boolean"))
        ds.Tables(tName).Columns.Add("FunctionID1", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("FunctionFlag", Type.GetType("System.String"))
        ds.Tables(tName).Columns.Add("TaxID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreateDrinkMainTableStructure(ByVal tName As String)

        ds.Tables.Add(tName)

        ds.Tables(tName).Columns.Add("DrinkID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("DrinkName", Type.GetType("System.String"))
        ds.Tables(tName).Columns.Add("DrinkCategoryID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("DrinkCategoryNumber", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("DrinkID1", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("MenuID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("Price", Type.GetType("System.Decimal"))
        ds.Tables(tName).Columns.Add("RoutingID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("Surcharge", Type.GetType("System.Decimal"))
        ds.Tables(tName).Columns.Add("MenuIndex", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("FunctionID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("FunctionFlag", Type.GetType("System.String"))
        ds.Tables(tName).Columns.Add("TaxID", Type.GetType("System.Int32"))
        ds.Tables(tName).Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreateQuickCatTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("CategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("CategoryName", Type.GetType("System.String"))
        tName.Columns.Add("CategoryAbrev", Type.GetType("System.String"))
        tName.Columns.Add("FunctionID", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        tName.Columns.Add("Extended", Type.GetType("System.Boolean"))
        tName.Columns.Add("BarenderMenuID", Type.GetType("System.Int32"))
        tName.Columns.Add("OrderIndex", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionFlag", Type.GetType("System.String"))
        tName.Columns.Add("TaxID", Type.GetType("System.Int32"))        'not sure why TAXID?

    End Sub

    Friend Sub CreateQuickDrinkCatTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("DrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("DrinkCategoryNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkCategoryName", Type.GetType("System.String"))
        tName.Columns.Add("ButtonColor", Type.GetType("System.Int32"))
        tName.Columns.Add("ButtonForeColor", Type.GetType("System.Int32"))
        tName.Columns.Add("IsALiquorType", Type.GetType("System.Boolean"))
        tName.Columns.Add("BartenderMenuID", Type.GetType("System.Int32"))
        tName.Columns.Add("OrderIndex", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreateLoggedInEmployeeTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("LoginTrackingID", Type.GetType("System.Int64"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("EmployeeID", Type.GetType("System.Int32"))
        tName.Columns.Add("JobCode", Type.GetType("System.Int32"))
        tName.Columns.Add("LogInTime", Type.GetType("System.DateTime"))
        tName.Columns.Add("LogOutTime", Type.GetType("System.DateTime"))
        tName.Columns.Add("TerminalsGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("RegPayRate", Type.GetType("System.Decimal"))
        tName.Columns.Add("OTPayRate", Type.GetType("System.Decimal"))
        tName.Columns.Add("TipableSales", Type.GetType("System.Decimal"))
        tName.Columns.Add("DeclaredTips", Type.GetType("System.Decimal"))
        tName.Columns.Add("ChargedSales", Type.GetType("System.Decimal"))
        tName.Columns.Add("ChargedTips", Type.GetType("System.Decimal"))
        tName.Columns.Add("OverrideLogin", Type.GetType("System.Int32"))
        tName.Columns.Add("OverrideLogout", Type.GetType("System.Int32"))
        tName.Columns.Add("OverrideRegPay", Type.GetType("System.Int32"))
        tName.Columns.Add("OverrideOTPay", Type.GetType("System.Int32"))
        tName.Columns.Add("OverrideDeclaredTips", Type.GetType("System.Int32"))
        tName.Columns.Add("Terminal", Type.GetType("System.Int32"))
        tName.Columns.Add("dbUP", Type.GetType("System.Int32"))
        tName.Columns.Add("LastName", Type.GetType("System.String"))
        tName.Columns.Add("FirstName", Type.GetType("System.String"))
        tName.Columns.Add("MiddleName", Type.GetType("System.String"))
        tName.Columns.Add("NickName", Type.GetType("System.String"))
        tName.Columns.Add("ClockInReq", Type.GetType("System.Boolean"))

    End Sub


    Friend Sub CreateJobCodeInfoTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("JobCodeID", Type.GetType("System.Int32"))
        tName.Columns.Add("JobCodeName", Type.GetType("System.String"))
        tName.Columns.Add("Manager", Type.GetType("System.Boolean"))
        tName.Columns.Add("Cashier", Type.GetType("System.Boolean"))
        tName.Columns.Add("Bartender", Type.GetType("System.Boolean"))
        tName.Columns.Add("Server", Type.GetType("System.Boolean"))
        tName.Columns.Add("Hostess", Type.GetType("System.Boolean"))
        tName.Columns.Add("PasswordReq", Type.GetType("System.Boolean"))
        tName.Columns.Add("ClockInReq", Type.GetType("System.Boolean"))
        tName.Columns.Add("ReportTipsReq", Type.GetType("System.Boolean"))
        tName.Columns.Add("ShareTipsReq", Type.GetType("System.Boolean"))
        'openInt1 never pulled in front end

    End Sub


    Friend Sub CreateClockOutSalesTableStructure(ByVal tName As DataTable)
    
        tName.Columns.Add("FunctionGroupSales", Type.GetType("System.Decimal"))
        tName.Columns.Add("FunctionName", Type.GetType("System.String"))
    End Sub

    Friend Sub CreateAllEmployeesTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("EmployeeID", Type.GetType("System.Int32"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("EmployeeNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("LastName", Type.GetType("System.String"))
        tName.Columns.Add("FirstName", Type.GetType("System.String"))
        tName.Columns.Add("NickName", Type.GetType("System.String"))
        tName.Columns.Add("Passcode", Type.GetType("System.String"))
        tName.Columns.Add("SwipeCode", Type.GetType("System.String"))
        tName.Columns.Add("Training", Type.GetType("System.Boolean"))
        tName.Columns.Add("Terminated", Type.GetType("System.Boolean"))
        tName.Columns.Add("ClockInReq", Type.GetType("System.Boolean"))
        tName.Columns.Add("ReportMgmtAll", Type.GetType("System.Boolean"))
        tName.Columns.Add("ReportMgmtLimited", Type.GetType("System.Boolean"))
        tName.Columns.Add("OperationMgmtAll", Type.GetType("System.Boolean"))
        tName.Columns.Add("OperationMgmtLimited", Type.GetType("System.Boolean"))
        tName.Columns.Add("SystemMgmtAll", Type.GetType("System.Boolean"))
        tName.Columns.Add("SystemMgmtLimited", Type.GetType("System.Boolean"))
        tName.Columns.Add("EmployeeMgmtAll", Type.GetType("System.Boolean"))
        tName.Columns.Add("EmployeeMgmtLimited", Type.GetType("System.Boolean"))

        tName.Columns.Add("JobCodeID1", Type.GetType("System.Int32"))
        tName.Columns.Add("JobRate1", Type.GetType("System.Decimal"))
        tName.Columns.Add("JobCodeID2", Type.GetType("System.Int32"))
        tName.Columns.Add("JobRate2", Type.GetType("System.Decimal"))
        tName.Columns.Add("JobCodeID3", Type.GetType("System.Int32"))
        tName.Columns.Add("JobRate3", Type.GetType("System.Decimal"))
        tName.Columns.Add("JobCodeID4", Type.GetType("System.Int32"))
        tName.Columns.Add("JobRate4", Type.GetType("System.Decimal"))
        tName.Columns.Add("JobCodeID5", Type.GetType("System.Int32"))
        tName.Columns.Add("JobRate5", Type.GetType("System.Decimal"))
        tName.Columns.Add("JobCodeID6", Type.GetType("System.Int32"))
        tName.Columns.Add("JobRate6", Type.GetType("System.Decimal"))
        tName.Columns.Add("JobCodeID7", Type.GetType("System.Int32"))
        tName.Columns.Add("JobRate7", Type.GetType("System.Decimal"))
        tName.Columns.Add("Lefty", Type.GetType("System.Boolean"))

        tName.Columns.Add("JobName1", Type.GetType("System.String"))
        tName.Columns.Add("JobName2", Type.GetType("System.String"))
        tName.Columns.Add("JobName3", Type.GetType("System.String"))
        tName.Columns.Add("JobName4", Type.GetType("System.String"))
        tName.Columns.Add("JobName5", Type.GetType("System.String"))
        tName.Columns.Add("JobName6", Type.GetType("System.String"))
        tName.Columns.Add("JobName7", Type.GetType("System.String"))

    End Sub

    Friend Sub CreateOpenOrdersTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("OpenOrderID", Type.GetType("System.Int64"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("DailyCode", Type.GetType("System.Int64"))
        tName.Columns.Add("ExperienceNumber", Type.GetType("System.Int64"))
        tName.Columns.Add("OrderNumber", Type.GetType("System.Int64"))
        tName.Columns.Add("ShiftID", Type.GetType("System.Int32"))
        tName.Columns.Add("MenuID", Type.GetType("System.Int32"))
        tName.Columns.Add("EmployeeID", Type.GetType("System.Int32"))
        tName.Columns.Add("CheckNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("CustomerNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("CourseNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("sin", Type.GetType("System.Int32"))
        tName.Columns.Add("sii", Type.GetType("System.Int32"))
        tName.Columns.Add("si2", Type.GetType("System.Int32"))
        tName.Columns.Add("Quantity", Type.GetType("System.Int32"))
        tName.Columns.Add("ItemID", Type.GetType("System.Int32"))
        tName.Columns.Add("ItemName", Type.GetType("System.String"))
        tName.Columns.Add("TerminalName", Type.GetType("System.String"))
        tName.Columns.Add("ChitName", Type.GetType("System.String"))
        tName.Columns.Add("ItemPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("Price", Type.GetType("System.Decimal"))
        tName.Columns.Add("TaxPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("SinTax", Type.GetType("System.Decimal"))
        tName.Columns.Add("TaxID", Type.GetType("System.Int32"))
        tName.Columns.Add("ForceFreeID", Type.GetType("System.Int64"))
        tName.Columns.Add("ForceFreeAuth", Type.GetType("System.Int32"))
        tName.Columns.Add("ForceFreeCode", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionID", Type.GetType("System.Int32"))
        tName.Columns.Add("CategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("FoodID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkID", Type.GetType("System.Int32"))
        tName.Columns.Add("ItemStatus", Type.GetType("System.Int32"))
        tName.Columns.Add("RoutingID", Type.GetType("System.Int32"))
        tName.Columns.Add("PrintPriorityID", Type.GetType("System.Int32"))
        tName.Columns.Add("Repeat", Type.GetType("System.Boolean"))
        tName.Columns.Add("TerminalID", Type.GetType("System.Int32"))
        tName.Columns.Add("dbUP", Type.GetType("System.Int32"))
        tName.Columns.Add("OpenDecimal1", Type.GetType("System.Int64"))
        tName.Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionFlag", Type.GetType("System.String"))

    End Sub


    Friend Sub CreatePaymentsAndCreditsTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("PaymentsAndCreditsID", Type.GetType("System.Int64"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("DailyCode", Type.GetType("System.Int64"))
        tName.Columns.Add("ExperienceNumber", Type.GetType("System.Int64"))
        tName.Columns.Add("PaymentDate", Type.GetType("System.DateTime"))
        tName.Columns.Add("EmployeeID", Type.GetType("System.Int32"))
        tName.Columns.Add("CheckNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("PaymentTypeID", Type.GetType("System.Int32"))
        tName.Columns.Add("AccountNumber", Type.GetType("System.String"))
        tName.Columns.Add("CCExpiration", Type.GetType("System.String"))
        tName.Columns.Add("CVV", Type.GetType("System.String"))
        tName.Columns.Add("Track2", Type.GetType("System.String"))
        tName.Columns.Add("CustomerName", Type.GetType("System.String"))
        tName.Columns.Add("TransactionType", Type.GetType("System.String"))
        tName.Columns.Add("TransactionCode", Type.GetType("System.String"))
        tName.Columns.Add("SwipeType", Type.GetType("System.Int32"))
        tName.Columns.Add("PaymentAmount", Type.GetType("System.Decimal"))
        tName.Columns.Add("Surcharge", Type.GetType("System.Decimal"))
        tName.Columns.Add("Tip", Type.GetType("System.Decimal"))
        tName.Columns.Add("PreAuthAmount", Type.GetType("System.Decimal"))
        tName.Columns.Add("Applied", Type.GetType("System.Boolean"))
        tName.Columns.Add("RefNum", Type.GetType("System.String"))
        tName.Columns.Add("AuthCode", Type.GetType("System.String"))
        tName.Columns.Add("AcqRefData", Type.GetType("System.String"))
        tName.Columns.Add("BatchCleared", Type.GetType("System.Boolean"))
        tName.Columns.Add("Duplicate", Type.GetType("System.Boolean"))
        tName.Columns.Add("TerminalID", Type.GetType("System.Int32"))
        tName.Columns.Add("TerminalsOpenID", Type.GetType("System.Int64"))
        tName.Columns.Add("AlreadyPrinted", Type.GetType("System.Boolean"))
        tName.Columns.Add("Description", Type.GetType("System.String"))
        tName.Columns.Add("dbUP", Type.GetType("System.Int32"))
        tName.Columns.Add("LastName", Type.GetType("System.String"))
        tName.Columns.Add("FirstName", Type.GetType("System.String"))
        tName.Columns.Add("PaymentTypeName", Type.GetType("System.String"))
        tName.Columns.Add("PaymentFlag", Type.GetType("System.String"))
        tName.Columns.Add("OpenBigInt1", Type.GetType("System.Int64"))

    End Sub

    Friend Sub CreateAvailTablesAndTabsTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("ExperienceNumber", Type.GetType("System.Int64"))
        tName.Columns.Add("DailyCode", Type.GetType("System.Int64"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("ExperienceDate", Type.GetType("System.DateTime"))
        tName.Columns.Add("LoginTrackingID", Type.GetType("System.Int64"))
        tName.Columns.Add("TableNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("TabID", Type.GetType("System.Int64"))
        tName.Columns.Add("TabName", Type.GetType("System.String"))
        tName.Columns.Add("TicketNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("EmployeeID", Type.GetType("System.Int32"))
        tName.Columns.Add("NumberOfCustomers", Type.GetType("System.Int32"))
        tName.Columns.Add("NumberOfChecks", Type.GetType("System.Int32"))
        tName.Columns.Add("ShiftID", Type.GetType("System.Int32"))
        tName.Columns.Add("MenuID", Type.GetType("System.Int32"))
        tName.Columns.Add("CheckDown", Type.GetType("System.DateTime"))
        tName.Columns.Add("FoodOrdered", Type.GetType("System.DateTime"))
        tName.Columns.Add("AvailForSeating", Type.GetType("System.DateTime"))
        tName.Columns.Add("LastStatus", Type.GetType("System.Int32"))
        tName.Columns.Add("LastStatusTime", Type.GetType("System.DateTime"))
        tName.Columns.Add("ItemsOnHold", Type.GetType("System.Int16"))
        tName.Columns.Add("LastView", Type.GetType("System.String"))
        tName.Columns.Add("ClosedSubTotal", Type.GetType("System.Decimal"))
        tName.Columns.Add("TerminalID", Type.GetType("System.Int32"))
        tName.Columns.Add("AutoGratuity", Type.GetType("System.Decimal"))
        tName.Columns.Add("MethodUse", Type.GetType("System.String"))
        tName.Columns.Add("Delivery", Type.GetType("System.String"))
        tName.Columns.Add("TabIndicator", Type.GetType("System.String"))
        tName.Columns.Add("Reference", Type.GetType("System.String"))
        If tName Is dtCurrentlyHeld Then
            tName.Columns.Add("CurrentlyHeld", Type.GetType("System.String"))
        End If
        tName.Columns.Add("dbUP", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreateAllTablesTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("TableOverviewID", Type.GetType("System.Int32"))
        tName.Columns.Add("TableNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("MaxExpNumByTable", Type.GetType("System.Int64"))
        tName.Columns.Add("SatTime", Type.GetType("System.DateTime"))
        tName.Columns.Add("EmployeeID", Type.GetType("System.Int32"))
        tName.Columns.Add("DailyCode", Type.GetType("System.Int64"))
        tName.Columns.Add("TableStatusID", Type.GetType("System.Int32"))
        tName.Columns.Add("LastStatusTime", Type.GetType("System.DateTime"))
        tName.Columns.Add("ItemsOnHold", Type.GetType("System.Int16"))
        tName.Columns.Add("FloorPlanID", Type.GetType("System.Int32"))
        tName.Columns.Add("TableSectionID", Type.GetType("System.Int32"))
        tName.Columns.Add("Seats", Type.GetType("System.Int32"))
        tName.Columns.Add("Available", Type.GetType("System.Boolean"))
        '   tName.Columns.Add("Active", Type.GetType("System.Boolean"))

    End Sub

    Friend Sub CreateOpenBusinessTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("DailyCode", Type.GetType("System.Int64"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("StartTime", Type.GetType("System.DateTime"))
        tName.Columns.Add("EndTime", Type.GetType("System.DateTime"))
        tName.Columns.Add("EmployeeOpened", Type.GetType("System.Int32"))
        tName.Columns.Add("EmployeeClosed", Type.GetType("System.Int32"))
        tName.Columns.Add("PrimaryMenu", Type.GetType("System.Int32"))
        tName.Columns.Add("SecondaryMenu", Type.GetType("System.Int32"))
        tName.Columns.Add("ShiftID", Type.GetType("System.Int32"))
        tName.Columns.Add("InvCounted", Type.GetType("System.Int16"))


    End Sub

    Friend Sub CreateFunctionsTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("FunctionID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionName", Type.GetType("System.String"))
        tName.Columns.Add("FunctionFlag", Type.GetType("System.String"))
        tName.Columns.Add("TaxID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkRoutingID", Type.GetType("System.Int32"))

    End Sub

    Friend Sub CreatePaymentTypeTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("PaymentTypeID", Type.GetType("System.Int32"))
        tName.Columns.Add("PaymentTypeName", Type.GetType("System.String"))
        tName.Columns.Add("PaymentFlag", Type.GetType("System.String"))

    End Sub

    Friend Sub CreateOrderDetailTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("OrderNumber", Type.GetType("System.Int64"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("DailyCode", Type.GetType("System.Int64"))
        tName.Columns.Add("ExperienceNumber", Type.GetType("System.Int64"))
        tName.Columns.Add("OrderTime", Type.GetType("System.DateTime"))
        tName.Columns.Add("OrderReady", Type.GetType("System.DateTime"))
        tName.Columns.Add("OrderFilled", Type.GetType("System.DateTime"))
        tName.Columns.Add("NumberOfDinners", Type.GetType("System.Int16"))
        tName.Columns.Add("NumberOfApps", Type.GetType("System.Int16"))
        tName.Columns.Add("NumberOfDrinks", Type.GetType("System.Int16"))
        tName.Columns.Add("isMainCourse", Type.GetType("System.Boolean"))
        tName.Columns.Add("TotalDollar", Type.GetType("System.Decimal"))
        tName.Columns.Add("AvgDollar", Type.GetType("System.Decimal"))
        tName.Columns.Add("ExperienceDate", Type.GetType("System.DateTime"))

    End Sub

    Friend Sub CreateTermsOpenTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("TerminalsOpenID", Type.GetType("System.Int64"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("DailyCode", Type.GetType("System.Int64"))
        tName.Columns.Add("TerminalsPrimaryKey", Type.GetType("System.Int32"))
        tName.Columns.Add("OpenBy", Type.GetType("System.Int16"))
        tName.Columns.Add("OpenTime", Type.GetType("System.DateTime"))
        tName.Columns.Add("OpenCash", Type.GetType("System.Decimal"))
        tName.Columns.Add("CloseBy", Type.GetType("System.Int16"))
        tName.Columns.Add("CloseTime", Type.GetType("System.DateTime"))
        tName.Columns.Add("CloseCash", Type.GetType("System.Decimal"))
        tName.Columns.Add("CashIn", Type.GetType("System.Decimal"))
        tName.Columns.Add("CashOut", Type.GetType("System.Decimal"))
        tName.Columns.Add("OverShort", Type.GetType("System.Decimal"))
        tName.Columns.Add("ReasonShort", Type.GetType("System.String"))
        tName.Columns.Add("LastName", Type.GetType("System.String"))
        tName.Columns.Add("FirstName", Type.GetType("System.String"))

    End Sub

    Friend Sub CreateTabDirectorySearchTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("TabID", Type.GetType("System.Int64"))
        tName.Columns.Add("CompanyID", Type.GetType("System.String"))
        tName.Columns.Add("LocationID", Type.GetType("System.String"))
        tName.Columns.Add("AccountNumber", Type.GetType("System.String"))
        tName.Columns.Add("AccountPhone", Type.GetType("System.String"))
        tName.Columns.Add("LastName", Type.GetType("System.String"))
        tName.Columns.Add("FirstName", Type.GetType("System.String"))
        tName.Columns.Add("MiddleName", Type.GetType("System.String"))
        tName.Columns.Add("NickName", Type.GetType("System.String"))
        tName.Columns.Add("Address1", Type.GetType("System.String"))
        tName.Columns.Add("Address2", Type.GetType("System.String"))
        tName.Columns.Add("City", Type.GetType("System.String"))
        tName.Columns.Add("State", Type.GetType("System.String"))
        tName.Columns.Add("PostalCode", Type.GetType("System.String"))
        tName.Columns.Add("Country", Type.GetType("System.String"))
        tName.Columns.Add("Phone1", Type.GetType("System.String"))
        tName.Columns.Add("Ext1", Type.GetType("System.String"))
        tName.Columns.Add("Phone2", Type.GetType("System.String"))
        tName.Columns.Add("Ext2", Type.GetType("System.String"))
        tName.Columns.Add("DeliveryZone", Type.GetType("System.Int32"))
        tName.Columns.Add("CrossRoads", Type.GetType("System.String"))
        tName.Columns.Add("SpecialInstructions", Type.GetType("System.String"))
        tName.Columns.Add("DoNotDeliver", Type.GetType("System.Boolean"))
        tName.Columns.Add("VIP", Type.GetType("System.Boolean"))
        tName.Columns.Add("UpdatedDate", Type.GetType("System.DateTime"))
        tName.Columns.Add("UpdatedByEmployee", Type.GetType("System.Int32"))
        tName.Columns.Add("Active", Type.GetType("System.Boolean"))
        tName.Columns.Add("OpenChar1", Type.GetType("System.String"))

    End Sub

    Friend Sub CreateTabPreviousOrdersTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("ExperienceNumber", Type.GetType("System.Int64"))
        tName.Columns.Add("ExperienceDate", Type.GetType("System.DateTime"))
        tName.Columns.Add("TabID", Type.GetType("System.Int64"))
        tName.Columns.Add("ClosedSubTotal", Type.GetType("System.Decimal"))
        tName.Columns.Add("NumberOfDinners", Type.GetType("System.Int32"))
        tName.Columns.Add("NumberOfApps", Type.GetType("System.Int32"))
        tName.Columns.Add("TotalDollar", Type.GetType("System.Decimal"))

    End Sub

    Friend Sub CreateTabPreviousOrdersByItemTableStructure(ByVal tName As DataTable)

        tName.Columns.Add("ExperienceNumber", Type.GetType("System.Int64"))
        tName.Columns.Add("MenuID", Type.GetType("System.Int32"))
        tName.Columns.Add("CustomerNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("CourseNumber", Type.GetType("System.Int32"))
        tName.Columns.Add("sin", Type.GetType("System.Int32"))
        tName.Columns.Add("sii", Type.GetType("System.Int32"))
        tName.Columns.Add("si2", Type.GetType("System.Int32"))
        tName.Columns.Add("Quantity", Type.GetType("System.Int32"))
        tName.Columns.Add("ItemID", Type.GetType("System.Int32"))
        tName.Columns.Add("ItemName", Type.GetType("System.String"))
        tName.Columns.Add("TerminalName", Type.GetType("System.String"))
        tName.Columns.Add("ChitName", Type.GetType("System.String"))
        tName.Columns.Add("ItemPrice", Type.GetType("System.Decimal"))
        tName.Columns.Add("Price", Type.GetType("System.Decimal"))
        tName.Columns.Add("TaxPrice", Type.GetType("System.Decimal"))
        '    tName.Columns.Add("SinTax", Type.GetType("System.Decimal"))
        '   tName.Columns.Add("TaxID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionID", Type.GetType("System.Int32"))
        tName.Columns.Add("CategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("FoodID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkCategoryID", Type.GetType("System.Int32"))
        tName.Columns.Add("DrinkID", Type.GetType("System.Int32"))
        tName.Columns.Add("ItemStatus", Type.GetType("System.Int32"))
        tName.Columns.Add("RoutingID", Type.GetType("System.Int32"))
        tName.Columns.Add("PrintPriorityID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionGroupID", Type.GetType("System.Int32"))
        tName.Columns.Add("FunctionFlag", Type.GetType("System.String"))

    End Sub


    Friend Function CreateTerminals()

        Dim oRow As DataRow
        Dim sRow As DataRow
        Dim i As Integer
        Dim shiftMin As Integer

        i = 1
        For Each oRow In ds.Tables("RoutingChoice").Rows
            If Not oRow("RoutingName") = "Do Not Route" Then
                printingRouting(i) = oRow("RoutingID")
                printingName(i) = oRow("RoutingName")
            End If
            i += 1
            If i = 20 Then Exit For
        Next

        If Not typeProgram = "Online_Demo" Then
            currentTerminal = New Terminal
        End If

        For Each oRow In ds.Tables("TerminalsMethod").Rows

            If String.Compare(oRow("TerminalName"), SystemInformation.ComputerName, True) = 0 Or (typeProgram = "Online_Demo" And oRow("TerminalName") = "eglobal") Or (System.Windows.Forms.SystemInformation.ComputerName = "EGLOBALMAIN" And Not companyInfo.companyName = "eGlobal Partners") Then
                '                                                ****   TRUE means IGNORE CASE  ***
                '    ******
                '   the last half (the or part) makes my computer accout use their last terminal info
                '   i need to ask which terminal (for multiple terminal restaurants)
                '    ******

                With currentTerminal
                    .TermPrimaryKey = oRow("TerminalsPrimaryKey")
                    .TermID = oRow("TerminalID")
                    .TermName = oRow("TerminalName")
                    .TermMethod = oRow("TerminalMethod")
                    .TermGroupID = oRow("TerminalsGroupID")
                    .FloorPlanID = oRow("FloorPlanID")
                    .xLoc = oRow("TermX")
                    .yLoc = oRow("TermY")
                    .HasCashDrawer = oRow("hasCashDrawer")
                    .NormalOpenAmount = oRow("normalOpenAmount")
                    .AutoOpenDrawer = oRow("autoOpenDrawer")
                    If System.Windows.Forms.SystemInformation.ComputerName = "EGLOBALMAIN" Then
                        .ReceiptName = "Receipt2"
                        '     .ReceiptName = "HP DeskJet 722C on Phoenix"
                    Else
                        If Not oRow("ReceiptPrinterID") Is DBNull.Value Then
                            If oRow("ReceiptPrinterID") <> 0 Then
                                .ReceiptName = oRow("RoutingName")
                            End If
                        End If
                    End If
                    If Not oRow("PhysicalAddress") Is DBNull.Value Then
                        .PhyAdd = oRow("PhysicalAddress")
                    Else
                        .PhyAdd = "0000"
                    End If

                    'shift codes
                    If ds.Tables("ShiftCodes").Rows.Count > 0 Then
                        For Each sRow In ds.Tables("ShiftCodes").Rows
                            If TimeOfDay.Hour > sRow("TimeStart").hour Then
                                .CurrentShift = sRow("ShiftID")
                                Exit For
                            ElseIf sRow("TimeStart").hour = TimeOfDay.Hour Then
                                If TimeOfDay.Minute > sRow("TimeStart").minute Then
                                    .CurrentShift = sRow("ShiftID")
                                    Exit For
                                End If
                            End If
                        Next
                    Else
                        .CurrentShift = 0
                    End If

                    '   these are kept in Experience Table (different for each order)
                    '         .TermUseName = oRow("UseName")
                    '        .TermUsePriority = oRow("UsePriority")
                End With
                Exit For

            End If
        Next

        If currentTerminal.TermGroupID > 0 Then
            Dim groupTerminal As Terminal
            For Each oRow In ds.Tables("TerminalsMethod").Rows
                If oRow("TerminalsGroupID") = currentTerminal.TermGroupID Then
                    groupTerminal = New Terminal
                    With groupTerminal
                        .TermPrimaryKey = oRow("TerminalsPrimaryKey")
                        .TermID = oRow("TerminalID")
                        .TermName = oRow("TerminalName")
                        .TermMethod = oRow("TerminalMethod")
                        .TermGroupID = oRow("TerminalsGroupID")
                    End With
                    groupTerminals.Add(groupTerminal)
                End If
            Next
        End If

        dvTerminalsUseOrder = New DataView
        With dvTerminalsUseOrder
            .Table = ds.Tables("TerminalsUseOrder")
            .RowFilter = "TerminalsPrimaryKey = '" & currentTerminal.TermPrimaryKey & "' and Active = 1"
            .Sort = "UsePriority ASC"   'already sorted in table 
        End With

    End Function




    Friend Function OpenNewTab(ByVal tabId As Int64, ByVal tabName As String) ', ByVal isDineIn As Boolean, ByRef tabAccountInfo As DataSet_Builder.Payment)
        ' there is another OpenNewTab in Table_Screen_Bar ?????

        Dim expNum As Int64
        Dim tktNum As Integer
        Dim isCurrentlyHeld As Boolean
        Dim satTm As DateTime

        If tabId = -888 Or currentTerminal.TermMethod = "Quick" Then
            tktNum = CreateNewTicketNumber()
            If tabName = "New Tab" Then
                tabName = "Tkt# " & tktNum.ToString
            End If
        Else
            '444      If tabName = "New Tab" Then
            'somehow this is making program change Method Use to TakeOut
            '      tktNum = CreateNewTicketNumber()
            '     tabName = "Tkt# " & tktNum.ToString
            '    Else
            tktNum = 0
            '     End If
        End If

        expNum = CreateNewExperience(currentServer.EmployeeID, Nothing, tabId, tabName, 1, 2, tktNum, 0, currentServer.LoginTrackingID)
        If expNum > 0 Then
            isCurrentlyHeld = PopulateThisExperience(expNum, False)

            currentTable = New DinnerTable
            currentTable.ExperienceNumber = expNum
            currentTable.IsTabNotTable = True
            currentTable.TabID = tabId
            currentTable.TabName = tabName
            currentTable.TableNumber = 0
            currentTable.TicketNumber = tktNum
            currentTable.EmployeeID = currentServer.EmployeeID
            currentTable.CurrentMenu = currentTerminal.currentPrimaryMenuID '444primaryMenuID  'this is the system menu - can change during order process
            currentTable.StartingMenu = currentTerminal.currentPrimaryMenuID '444primaryMenuID
            currentTable.NumberOfCustomers = 1      'is 1 when you first open
            currentTable.NumberOfChecks = 1
            currentTable.LastStatus = 2
            currentTable.SatTime = Now
            currentTable.ItemsOnHold = 0
            If tabId = -888 Then 'maybe or -999 ???
                currentTable.MethodUse = "Dine In"
            ElseIf tabId = -990 Then
                currentTable.MethodUse = "Take Out"
            ElseIf tabId = -991 Then
                currentTable.MethodUse = "Pickup"
            ElseIf tabId = -777 Then
                currentTable.MethodUse = "Return"
            Else
                currentTable.MethodUse = DetermineInitialMethod(tktNum)
            End If
            '    currentTable.MethodUse = SeatingTab.MethedUse
            '444    tabAccountInfo.experienceNumber = currentTable.ExperienceNumber

            StartOrderProcess(currentTable.ExperienceNumber)
            Return True
        Else
            Return False 'this means something failed
        End If

    End Function


    Friend Function CreateNewExperience(ByVal EmployeeID As Integer, ByVal TableSelected As Integer, ByVal tabID As Int64, ByVal tabName As String, ByVal numCust As Integer, ByVal status As Integer, ByVal ntn As Integer, ByVal hold As Int16, ByVal loginTrackID As Int64)
        Dim tableRow As DataRow = dsOrder.Tables("AvailTables").NewRow
        Dim tabRow As DataRow = dsOrder.Tables("AvailTabs").NewRow
        Dim quickRow As DataRow = dsOrder.Tables("QuickTickets").NewRow
        '       Dim termTableRow As DataRow = dsBackup.Tables("AvailTablesTerminal").NewRow
        '      Dim termTabRow As DataRow = dsBackup.Tables("AvailTabsTerminal").NewRow
        Dim newTicketNumber As Integer

        If currentTerminal.TermMethod = "Quick" Or tabID = -888 Then
            newTicketNumber = ntn
            PerformNewExperienceAdd(quickRow, Nothing, EmployeeID, TableSelected, tabID, tabName, numCust, status, newTicketNumber, hold, loginTrackID)
            If typeProgram = "Online_Demo" Then
                quickRow("ExperienceNumber") = demoExpNumID
                demoExpNumID += 1
            End If

            dsOrder.Tables("QuickTickets").Rows.Add(quickRow)
            Try
                If typeProgram = "Online_Demo" Then
                    Return quickRow("ExperienceNumber")
                    Exit Function
                End If
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                If currentTerminal.TermMethod = "Quick" Then
                    sql.SqlQuickTicketSP.Update(dsOrder, "QuickTickets")
                Else
                    sql.SqlQuickTicketsBar.Update(dsOrder, "QuickTickets")
                End If
                sql.cn.Close()
                If Not TableSelected = Nothing Or TableSelected > 0 Then
                    'is a table
                    sql.cn.Open()
                    sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                    PlaceNewExpNumInAABTablesOverview(TableSelected, quickRow("ExperienceNumber"))
                    'really should be AllTables but not auto making Update COmmand in SQLHelper
                    sql.SqlDataAdapterAllTables.Update(dsOrder.Tables("AllTables"))
                    '777   sql.SqlTermsTables.Update(ds.Tables("TermsTables"))
                    sql.cn.Close()
                End If
                currentTerminal.NumOpenTickets += 1
            Catch ex As Exception
                CloseConnection()
                MsgBox(ex.Message)
            End Try

            Return quickRow("ExperienceNumber")
        Else
            newTicketNumber = ntn                                   ' (ntn is zero if isDineIn = true)

            If TableSelected = Nothing Or TableSelected = 0 Then      'is a tab
                PerformNewExperienceAdd(tabRow, Nothing, EmployeeID, TableSelected, tabID, tabName, numCust, status, newTicketNumber, hold, loginTrackID)
                If typeProgram = "Online_Demo" Then
                    tabRow("ExperienceNumber") = demoExpNumID
                    demoExpNumID += 1
                End If

                dsOrder.Tables("AvailTabs").Rows.Add(tabRow)
                Try
                    If typeProgram = "Online_Demo" Then
                        Return tabRow("ExperienceNumber")
                        Exit Function
                    End If
                    sql.cn.Open()
                    sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                    sql.SqlAvailTabsSP.Update(dsOrder, "AvailTabs")
                    sql.cn.Close()
                Catch ex As Exception
                    CloseConnection()
                    MsgBox(ex.Message)
                End Try
                Return tabRow("ExperienceNumber")



            Else    'If tabID = Nothing Or tabID = 0 Then     'is a table
                PerformNewExperienceAdd(tableRow, Nothing, EmployeeID, TableSelected, tabID, tabName, numCust, status, newTicketNumber, hold, loginTrackID)
                If typeProgram = "Online_Demo" Then
                    tableRow("ExperienceNumber") = demoExpNumID
                    demoExpNumID += 1
                End If

                dsOrder.Tables("AvailTables").Rows.Add(tableRow)
                Try
                    If typeProgram = "Online_Demo" Then
                        Return tableRow("ExperienceNumber")
                        Exit Function
                    End If
                    sql.cn.Open()
                    sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                    sql.SqlAvailTablesSP.Update(dsOrder, "AvailTables")
                    '    dsOrder.Tables("AvailTables").AcceptChanges()


                    PlaceNewExpNumInAABTablesOverview(TableSelected, tableRow("ExperienceNumber"))
                    sql.SqlDataAdapterAllTables.Update(dsOrder.Tables("AllTables"))
                    '777 sql.SqlTermsTables.Update(ds.Tables("TermsTables"))
                    sql.cn.Close()

                    '      
                    '         PlaceNewExpNumInAABTablesOverview(TableSelected, tableRow("ExperienceNumber"))
                    '        sql.cn.Open()
                    '       sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                    '      ' old   sql.SqlDataAdapterAllTables.Update(dsOrder.Tables("AllTables"))
                    '     sql.SqlTermsTables.Update(ds.Tables("TermsTables"))
                    '    sql.cn.Close()

                Catch ex As Exception
                    CloseConnection()
                    MsgBox(ex.Message)
                End Try

                Return tableRow("ExperienceNumber")
            End If

            Exit Function

            '222
            '*** below is copied above, but not changing TABID from -999
            If TableSelected = Nothing Or TableSelected = 0 Then
                '   for now we are reinserting TabID
                '   this is for new tabs
                If tabID = -999 Then
                    tabRow("TabID") = tabRow("ExperienceNumber")
                    '              termTabRow("TabID") = termTabRow("ExperienceNumber")
                    '''    *         SaveAvailTabsAndTables()
                    If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
                        Try
                            sql.cn.Open()
                            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                            sql.SqlAvailTabsSP.Update(dsOrder, "AvailTabs")
                            sql.cn.Close()
                        Catch ex As Exception
                            CloseConnection()
                            MsgBox(ex.Message)
                        End Try
                    ElseIf currentTerminal.TermMethod = "Quick" Then
                        Try
                            sql.cn.Open()
                            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                            sql.SqlQuickTicketSP.Update(dsOrder, "QuickTickets")
                            sql.cn.Close()
                        Catch ex As Exception
                            CloseConnection()
                            MsgBox(ex.Message)
                        End Try
                    End If

                End If

                '   *** not sure about the following two
                '   we send term because it has a value no matter if server up or down
                '            Return termTabRow("ExperienceNumber")
                Return tabRow("ExperienceNumber")
            Else
                '             Return termTableRow("ExperienceNumber")
                Return tableRow("ExperienceNumber")
            End If

        End If

    End Function

    Friend Sub PopulateBartenderCollection()

        Dim strBartenders As String
        Dim firstTime As Boolean = True
        Dim term As Terminal
        Dim barMan As Employee
        Dim emp As Employee
        Dim dtr As SqlClient.SqlDataReader
        Dim cmd As SqlClient.SqlCommand

        If typeProgram = "Online_Demo" Then
            loggedInBartenders = currentBartenders
            Exit Sub
        End If

        Dim lastBarID As Integer
        Dim oRow As DataRow
        currentBartenders.Clear()
        loggedInBartenders.Clear()

        ds.Tables("GroupBartenders").Clear()

        '     sql.SqlSelectCommandClockedIn.Parameters("@CompanyID").Value = CompanyID
        sql.SqlSelectGroupBartenders.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectGroupBartenders.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
        sql.SqlSelectGroupBartenders.Parameters("@TerminalsGroup").Value = currentTerminal.TermGroupID

        '   this is pulling from view that has NULL Logout's
        '   if fails we want to fail in Login_Entered
        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlGroupBartenders.Fill(ds.Tables("GroupBartenders"))
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
        End Try

        For Each oRow In ds.Tables("GroupBartenders").Rows
            If oRow("EmployeeID") = lastBarID Then
                'saem bartender, we only care about first time pass
            Else
                lastBarID = oRow("EmployeeID")
                For Each emp In AllEmployees
                    If emp.EmployeeID = oRow("EmployeeID") Then
                        barMan = New Employee
                        barMan = emp
                        barMan.Bartender = True

                        currentBartenders.Add(barMan)
                        'all bartender for day, this term group
                        If oRow("LogOutTime") Is DBNull.Value Then
                            loggedInBartenders.Add(barMan)
                        End If
                        Exit For
                    End If
                Next
            End If
        Next

        '222
        Exit Sub

        strBartenders = "SELECT EmployeeID, NickName FROM ClockedInView WHERE LocationID = '" & companyInfo.LocationID & "' AND LogOutTime IS NULL AND Bartender = '1'"

        If groupTerminals.Count > 0 Then
            strBartenders = strBartenders & " AND TerminalsGroupID = '" & currentTerminal.TermGroupID & "'"

            '     For Each term In groupTerminals
            '         If firstTime = True Then
            '     strBartenders = strBartenders & " AND TerminalsGroupID = '" & term.TermGroupID & "'"
            '       firstTime = False
            '  Else
            '     strBartenders = strBartenders & " OR TerminalGroupID = '" & term.TermGroupID & "'"
            'End If
            '     Next
        Else
            '     strBartenders = strBartenders & " Terminal = '" & term.TermID & "'"
        End If

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            cmd = New SqlClient.SqlCommand(strBartenders, sql.cn)
            dtr = cmd.ExecuteReader
            While dtr.Read()
                For Each emp In AllEmployees
                    If emp.EmployeeID = dtr("EmployeeID") Then
                        barMan = New Employee
                        barMan = emp
                        barMan.Bartender = True
                        currentBartenders.Add(barMan)
                        '               MsgBox(barMan.NickName)
                        '              MsgBox(barMan.Bartender.ToString)
                    End If
                Next
            End While
            dtr.Close()
            sql.cn.Close()
        Catch ex As Exception
            If Not dtr Is Nothing Then
                If dtr.IsClosed = False Then
                    dtr.Close()
                End If
            End If
            CloseConnection()
            MsgBox(ex.Message)
        End Try

        'salaried 
        For Each emp In SalariedEmployees
            If Not emp.EmployeeID = 6986 Then   'Or currentServer.EmployeeID = 6986 Then
                currentBartenders.Add(emp)
            End If
        Next

    End Sub

    Friend Sub PopulateServerCollection(ByRef activeCollection As EmployeeCollection)

        If typeProgram = "Online_Demo" Then
            MsgBox("Transfer is not fully functional in Demo. Not all Personnel Categories can populate in Demo.", MsgBoxStyle.Information, "DEMO Purposes only")
            Exit Sub
        End If

        Dim strCollection As String
        Dim newMan As Employee
        Dim emp As Employee
        Dim dtr As SqlClient.SqlDataReader
        Dim cmd As SqlClient.SqlCommand

        activeCollection.Clear()

        strCollection = "SELECT EmployeeID FROM ClockedInView WHERE LocationID = '" & companyInfo.LocationID & "' AND LogOutTime IS NULL" ' AND Server = '1'"

        If activeCollection Is currentServers Then
            strCollection = strCollection & " AND Server = '1'"

        ElseIf activeCollection Is currentManagers Then
            strCollection = strCollection & " AND Manager = '1'"
        ElseIf activeCollection Is currentBartenders Then
            strCollection = strCollection & " AND Bartender = '1'"
        ElseIf activeCollection Is todaysFloorPersonnel Then
            strCollection = strCollection & " AND (Server = '1' OR Bartender = '1' OR Manager = '1' OR Cashier = '1')"
        End If

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            cmd = New SqlClient.SqlCommand(strCollection, sql.cn)
            dtr = cmd.ExecuteReader
            While dtr.Read()
                For Each emp In AllEmployees
                    If emp.EmployeeID = dtr("EmployeeID") Then
                        newMan = New Employee
                        newMan = emp
                        '444           newMan.Server = True
                        activeCollection.Add(newMan)
                    End If
                Next
            End While
            dtr.Close()
            sql.cn.Close()
        Catch ex As Exception
            If Not dtr Is Nothing Then
                If dtr.IsClosed = False Then
                    dtr.Close()
                End If
            End If

            CloseConnection()
            Exit Sub

        End Try

        If activeCollection Is currentManagers Or activeCollection Is todaysFloorPersonnel Then
            For Each emp In SalariedEmployees
                If Not emp.EmployeeID = 6986 Or currentServer.EmployeeID = 6986 Then
                    activeCollection.Add(emp)
                End If
            Next
        End If

    End Sub


    Public Function PerformNewExperienceAdd(ByRef dr As DataRow, ByVal newexperiencenumber As Int64, ByVal EmployeeID As Integer, ByVal TableSelected As Integer, ByVal tabID As Int64, ByVal tabName As String, ByVal numCust As Integer, ByVal status As Integer, ByVal newTicketNumber As Integer, ByVal hold As Int16, ByVal loginTrackID As Int64)

        dr("CompanyID") = companyInfo.CompanyID
        dr("LocationID") = companyInfo.LocationID
        '       dr("DailyCode") = DailyCode
        If Not newexperiencenumber = Nothing Then
            dr("ExperienceNumber") = newexperiencenumber
        End If
        dr("ExperienceDate") = Now
        dr("LoginTrackingID") = loginTrackID
        dr("DailyCode") = currentTerminal.CurrentDailyCode
        If TableSelected = Nothing Or TableSelected = 0 Then      'is a tab
            dr("TabID") = tabID
            dr("TabName") = tabName
        Else    'If tabID = Nothing Or tabID = 0 Then     'is a table
            dr("TableNumber") = TableSelected
            dr("TabID") = -22 'i have no idea WHY? 
            dr("TabName") = TableSelected.ToString
            '     PlaceNewExpNumInAABTablesOverview(TableSelected, newexperiencenumber)
        End If
        dr("EmployeeID") = EmployeeID
        dr("NumberOfCustomers") = numCust
        dr("NumberOfChecks") = 1
        dr("ShiftID") = currentTerminal.CurrentShift 'currentServer.ShiftID
        dr("MenuID") = currentTerminal.currentPrimaryMenuID '444primaryMenuID
        dr("LastStatus") = status
        dr("LastStatusTime") = Now
        dr("ItemsOnHold") = hold
        dr("LastView") = "Detail"
        dr("TerminalID") = currentTerminal.TermID
        '                 If newTicketNumber = 0 Then
        '                 dr("TicketNumber") = 0
        dr("TicketNumber") = newTicketNumber
        If numCust >= companyInfo.autoGratuityNumber Then
            dr("AutoGratuity") = companyInfo.autoGratuityPercent
        Else
            dr("AutoGratuity") = -1
        End If

        If tabID = -888 Then
            dr("MethodUse") = "Dine In"
        ElseIf tabID = -990 Then
            dr("MethodUse") = "Take Out"
        ElseIf tabID = -991 Then
            dr("MethodUse") = "Pickup"
        ElseIf tabID = -777 Then
            dr("MethodUse") = "Return"
        Else
            dr("MethodUse") = DetermineInitialMethod(newTicketNumber)
        End If
        If mainServerConnected = True Then
            dr("dbUP") = 1
        Else
            dr("dbUP") = 0
        End If

    End Function

    Friend Sub PlaceNewExpNumInAABTablesOverview(ByVal tn As Integer, ByVal expNum As Int64)

        Dim oRow As DataRow

        Try
            '  For Each oRow In ds.Tables("TermsTables").Rows
            For Each oRow In dsOrder.Tables("AllTables").Rows  '777
                If oRow("TableNumber") = tn Then
                    oRow("OpenBigInt1") = expNum
                    '  don't think I need  Flag for new exp
                    Exit For
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub
    Friend Function CreatingNewTicket()

        Dim expNum As Int64
        Dim tktNum As Integer
        Dim tabNameString As String

        '444 in previous sub      qtRow += 1
        tktNum = CreateNewTicketNumber()
        'in new experience TabID should stay the same (-888, -111, -222)
        If currentTable.TabID = -888 Then
            '    tabNameString = "Tkt# " & tktNum.ToString
            tabNameString = currentServer.NickName & "'s Tabs"
        Else
            currentTable.TabID = -999
            tabNameString = "Tkt# " & tktNum.ToString
        End If

        '     ResetsMethodUse()

        If dvTerminalsUseOrder.Count > 0 Then
            currentTable.MethodUse = dvTerminalsUseOrder(0)("MethodUse")
            currentTable.MethodDirection = dvTerminalsUseOrder(0)("MethodDirection")
        Else
            currentTable.MethodUse = "Dine In"
            currentTable.MethodDirection = "None"
        End If

        expNum = CreateNewExperience(currentServer.EmployeeID, Nothing, currentTable.TabID, tabNameString, 1, 2, tktNum, 0, currentServer.LoginTrackingID)
        currentTerminal.NumOpenTickets += 1
        currentTable.NumberOfCustomers = 1
        currentTable.TicketNumber = tktNum
        currentTable.TabName = ""

        Return expNum

    End Function

    Friend Function CreateNewTicketNumber()

        Dim oRow As DataRow
        Dim newTicketNumber As Integer
        Dim lastTicketNumber As Integer

        If typeProgram = "Online_Demo" Then
            newTicketNumber = demoNewTicketID
            demoNewTicketID += 1
            Return newTicketNumber
            Exit Function
        End If

        '     Dim cmd = New SqlClient.SqlCommand("SELECT MAX(TicketNumber) currentTerminal.lastTicketNumber FROM ExperienceTable WHERE LocationID = '" & companyInfo.LocationID & "' AND DailyCode = '" & currentTerminal.CurrentDailyCode & "' AND TerminalID = '" & currentTerminal.TermID & "' AND (TicketNumber > 0)", sql.cn)
        Dim cmd = New SqlClient.SqlCommand("SELECT MAX(TicketNumber) lastTicketNumber FROM ExperienceTable WHERE LocationID = '" & companyInfo.LocationID & "' AND DailyCode = '" & currentTerminal.CurrentDailyCode & "' AND TerminalID = '" & currentTerminal.TermID & "' AND (TicketNumber > 0)", sql.cn)
        Dim dtr As SqlClient.SqlDataReader
        '    Dim expDate As DateTime
        '   Dim expNumber As Int64
        '  Dim tixNumber As Integer

        If Not currentTerminal.LastTicketNumber = 0 Then
            newTicketNumber = currentTerminal.LastTicketNumber + 1
            If newTicketNumber / 10000 = currentTerminal.TermID Then
                newTicketNumber = (currentTerminal.TermID * 10000) + 1
            End If
            currentTerminal.LastTicketNumber = newTicketNumber
        Else

            'no longer     'we must find the MAX exp number first becuse ticket numbers revolve (start back at tkt#1)
            Try
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                dtr = cmd.executereader
                dtr.Read()

                If Not dtr("lastTicketNumber") Is DBNull.Value Then
                    newTicketNumber = (dtr("lastTicketNumber")) + 1
                    If newTicketNumber / 10000 = currentTerminal.TermID Then
                        newTicketNumber = (currentTerminal.TermID * 10000) + 1
                    End If
                    '     expNumber = dtr("ExperienceNumber")
                Else
                    newTicketNumber = (currentTerminal.TermID * 10000) + 1
                    '             dtr.Close()
                    '            sql.cn.Close()
                    '           Return newTicketNumber
                End If
                currentTerminal.LastTicketNumber = newTicketNumber

                dtr.Close()
                sql.cn.Close()

            Catch ex As Exception
                If Not dtr Is Nothing Then
                    If dtr.IsClosed = False Then
                        dtr.Close()
                    End If
                End If
                CloseConnection()
                MsgBox(ex.Message)
            End Try
        End If

        Return newTicketNumber

    End Function

    Friend Function DetermineInitialMethod(ByRef newTicketNumber As Integer)

        Dim cm As String

        If newTicketNumber = -777 Then
            cm = "Return"
            Return cm
        End If

        If currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            '   Table Service
            '     If currentTable.TicketNumber > 0 Then
            If newTicketNumber > 0 Then
                cm = "Take Out"
            Else
                cm = "Dine In"
            End If

        ElseIf currentTerminal.TermMethod = "Quick" Then

            If dvTerminalsUseOrder.Count > 0 Then
                cm = dvTerminalsUseOrder(0)("MethodUse") 'called MethodUse in Exp Table
            Else
                cm = "Dine In"
            End If
            '       ElseIf currentTerminal.TermMethod = "Quick" Then
            '          If dsOrder.Tables("TerminalsUseOrder").Rows.Count > 0 Then
            '         cm = dsOrder.Tables("TerminalsUseOrder").Rows(0)("MethodUse") 'called MethodUse in Exp Table
            '    Else
            '       cm = "Dine In"
            '  End If
        End If

        Return cm

    End Function

    Friend Function DetermineFunctionAndTaxInfo(ByRef currentItem As SelectedItemDetail, ByVal funGroup As Integer, ByVal fromSpecial As Boolean)
        'for special and extra

        Dim functionRow As DataRow

        For Each functionRow In dsOrder.Tables("Functions").Rows
            If fromSpecial = True Then
                If functionRow("FunctionGroupID") = funGroup Then
                    With currentItem
                        .FunctionID = functionRow("FunctionID")
                        '       .FunctionFlag = functionRow("FunctionFlag")
                        .TaxID = functionRow("TaxID")
                        '444       If .FunctionFlag = "D" Or fromSpecial = True Then
                        If Not functionRow("DrinkRoutingID") Is DBNull.Value Then
                            'DrinkRoutingID in function table is routing ID
                            .RoutingID = functionRow("DrinkRoutingID")
                            '444    End If
                        End If
                    End With
                    Exit For

                End If
            Else        'not from special
                If functionRow("FunctionGroupID") = funGroup Then
                    With currentItem
                        .FunctionID = functionRow("FunctionID")
                        '   .FunctionFlag = functionRow("FunctionFlag")
                        .TaxID = functionRow("TaxID")
                        '444       If .FunctionFlag = "D" Or fromSpecial = True Then
                        '      If Not functionRow("DrinkRoutingID") Is DBNull.Value Then
                        '.RoutingID = functionRow("DrinkRoutingID")
                        '          End If
                        '  End If
                    End With
                    Exit For
                End If
            End If
        Next

    End Function

    Friend Function DetermineTaxID(ByVal aFunctionID As Integer)
        Dim functionRow As DataRow
        Dim aTaxID As Integer

        If aFunctionID > 0 Then
            functionRow = (dsOrder.Tables("Functions").Rows.Find(aFunctionID))
            aTaxID = functionRow("TaxID")
        Else
            aTaxID = 0
        End If

        Return aTaxID

    End Function

    Friend Function DetermineTaxPrice(ByVal aTaxID As Integer, ByVal aTaxPrice As Decimal)
        Dim calculatedTaxPrice As Decimal
        Dim roundedTaxPrice As Decimal
        Dim taxRow As DataRow

        If aTaxID = -1 Or aTaxID = 0 Then
            'taxExempt
            calculatedTaxPrice = 0
        Else
            '    Try
            '        taxRow = (ds.Tables("Tax").Rows.Find(aTaxID))
            '        calculatedTaxPrice = aTaxPrice * taxRow("TaxPercent") '(Format((aTaxPrice * taxRow("TaxPercent")), "#####0.00"))
            '     Catch ex As Exception
            '        calculatedTaxPrice = 0
            calculatedTaxPrice = aTaxPrice * companyInfo.salesTax 'Format((aTaxPrice * companyInfo.salesTax), "#####0.00")
            '     End Try

        End If

        Return calculatedTaxPrice

    End Function

    Friend Function DetermineSinTax(ByVal aTaxID As Integer, ByVal aTaxPrice As Decimal)
        Dim calculatedTaxPrice As Decimal
        Dim taxRow As DataRow

        If aTaxID = -1 Or aTaxID = 0 Then
            'taxExempt
            calculatedTaxPrice = 0
        Else
            taxRow = (ds.Tables("Tax").Rows.Find(aTaxID))
            calculatedTaxPrice += aTaxPrice * taxRow("TaxPercent") '(Format((aTaxPrice * taxRow("TaxPercent")), "#####0.00"))
        End If

        Return calculatedTaxPrice

    End Function

    Friend Function DetermineTaxName(ByVal aTaxID As Integer)
        Dim aTaxName As String
        Dim taxRow As DataRow

        If aTaxID = -1 Or aTaxID = 0 Then
            'taxExempt
            aTaxName = "Tax"
        Else
            taxRow = (ds.Tables("Tax").Rows.Find(aTaxID))
            aTaxName = taxRow("TaxName")
        End If

        Return aTaxName

    End Function


    Friend Function AddStatusChangeData222(ByVal status As Integer, ByVal orderNumber As Integer, ByVal isMainCourse As Boolean, ByVal avgDollar As Decimal)
        '   we do not use this for orders
        '   we have the exact same sub in term_OrderForm (b/c we must do at the same time as generate OrderNumber)
        Dim cmd As SqlClient.SqlCommand

        cmd = New SqlClient.SqlCommand("INSERT INTO ExperienceStatusChange (CompanyID, LocationID, ExperienceNumber, StatusTime, TableStatusID, OrderNumber, IsMainCourse, AverageDollar, TerminalID, dbUP) VALUES (@CompanyID, @LocationID, @ExperienceNumber, @StatusTime, @TableStatusID, @OrderNumber, @IsMainCourse, @AverageDollar, @TerminalID, @dbUP)", sql.cn)

        cmd.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", System.Data.SqlDbType.NChar, 6))
        cmd.Parameters("@CompanyID").Value = companyInfo.CompanyID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@LocationID", System.Data.SqlDbType.NChar, 6))
        cmd.Parameters("@LocationID").Value = companyInfo.LocationID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@ExperienceNumber", SqlDbType.BigInt, 8))
        cmd.Parameters("@ExperienceNumber").Value = currentTable.ExperienceNumber
        cmd.Parameters.Add(New SqlClient.SqlParameter("@StatusTime", SqlDbType.DateTime, 8))
        cmd.Parameters("@StatusTime").Value = Now
        cmd.Parameters.Add(New SqlClient.SqlParameter("@TableStatusID", SqlDbType.Int, 4))
        cmd.Parameters("@TableStatusID").Value = status
        cmd.Parameters.Add(New SqlClient.SqlParameter("@OrderNumber", SqlDbType.Int, 4))
        cmd.Parameters("@OrderNumber").Value = orderNumber
        cmd.Parameters.Add(New SqlClient.SqlParameter("@IsMainCourse", SqlDbType.Bit, 1))
        cmd.Parameters("@IsMainCourse").Value = isMainCourse
        cmd.Parameters.Add(New SqlClient.SqlParameter("@AverageDollar", SqlDbType.Decimal, 5))
        cmd.Parameters("@AverageDollar").Value = avgDollar
        cmd.Parameters.Add(New SqlClient.SqlParameter("@TerminalID", SqlDbType.Int, 4))
        cmd.Parameters("@TerminalID").Value = currentTerminal.TermID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@dbUP", SqlDbType.Bit, 1))
        cmd.Parameters("@dbUP").Value = 1

        If mainServerConnected = True Then
            Try
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                cmd.ExecuteNonQuery()
                sql.cn.Close()
            Catch ex As Exception
                CloseConnection()
                If mainServerConnected = True Then
                    ServerJustWentDown()
                End If
                '            TerminalAddStatusChangeData(status, orderNumber, isMainCourse, avgDollar)
            End Try
        Else
            '         TerminalAddStatusChangeData(status, orderNumber, isMainCourse, avgDollar)
        End If


    End Function

    Friend Function AddStatusChangeData222(ByVal expNum As Int64, ByVal status As Integer, ByVal orderNumber As Integer, ByVal isMainCourse As Boolean, ByVal avgDollar As Decimal)
        Dim statusTime As DateTime

        '   effects the experienceStatusChange table
        Dim oRow As DataRow = dsOrder.Tables("StatusChange").NewRow
        oRow("CompanyID") = companyInfo.CompanyID
        oRow("LocationID") = companyInfo.LocationID
        oRow("DailyCode") = currentTerminal.CurrentDailyCode
        oRow("ExperienceNumber") = expNum
        oRow("StatusTime") = Now
        oRow("TableStatusID") = status
        oRow("OrderNumber") = orderNumber
        oRow("IsMainCourse") = isMainCourse
        oRow("AverageDollar") = avgDollar
        oRow("TerminalID") = currentTerminal.TermPrimaryKey
        oRow("dbUP") = 1
        dsOrder.Tables("StatusChange").Rows.Add(oRow)

        '     TerminalAddStatusChangeData(status, orderNumber, isMainCourse, avgDollar)


    End Function

    Public Sub FillJobCodeInfo(ByRef emp As Employee, ByVal thisJobCode As Integer)
        '   this only supply specifics about Job Code already selected
        Dim oRow As DataRow

        Dim tempDT As New DataTable

        If dsEmployee.Tables("JobCodeInfo").Rows.Count > 0 Then
            tempDT = dsEmployee.Tables("JobCodeInfo")
        Else
            tempDT = dsStarter.Tables("StarterJobCodeInfo")
        End If

        For Each oRow In tempDT.Rows 'dsEmployee.Tables("JobCodeInfo").Rows
            If oRow("JobCodeID") = thisJobCode Then
                emp.JobCodeName = oRow("JobCodeName")
                emp.Manager = oRow("Manager")
                emp.Cashier = oRow("Cashier")
                emp.Bartender = oRow("Bartender")
                emp.Server = oRow("Server")
                emp.Hostess = oRow("Hostess")
                emp.PasswordReq = oRow("PasswordReq")
                '444       emp.ClockInReq = oRow("ClockInReq")
                emp.ReportTipsReq = oRow("ReportTipsReq")
                emp.ShareTipsReq = oRow("ShareTipsReq")
            End If
        Next

    End Sub
    Friend Sub EnterEmployeeToLoginDataSet(ByVal emp As Employee)

        Dim oRow As DataRow = dsEmployee.Tables("LoggedInEmployees").NewRow
        Dim otPay As Decimal

        If emp.OTPayRate > emp.RegPayRate Then
            otPay = emp.OTPayRate
        Else
            otPay = emp.RegPayRate
        End If

        oRow("CompanyID") = companyInfo.CompanyID
        oRow("LocationID") = companyInfo.LocationID
        oRow("EmployeeID") = emp.EmployeeID
        oRow("JobCode") = emp.JobCodeID
        oRow("LogInTime") = emp.LogInTime
        '      oRow("LogOutTime") = emp.LogOutTime       '*** change allow for clock out
        oRow("TerminalsGroupID") = currentTerminal.TermGroupID
        oRow("RegPayRate") = emp.RegPayRate
        oRow("OTPayRate") = otPay
        dsEmployee.Tables("LoggedInEmployees").Rows.Add(oRow)

        '     sql.cn.Open()
        '           sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        '    sql.SqlClockedInList.Update(dsEmployee.Tables("LoggedInEmployees"))
        '   sql.cn.Close()

        dsEmployee.Tables("LoggedInEmployees").AcceptChanges()

    End Sub

    Friend Function TestUsernamePassword(ByVal loginEnter As String, ByVal checkPassword As Boolean) As Employee
        Dim emp As Employee

        If loginEnter Is Nothing Then Exit Function

        If loginEnter.Length < 4 Then
            MsgBox("Username Incorrect: Please Reenter or See Manager")
            Exit Function
        End If

        For Each emp In AllEmployees

            If emp.EmployeeNumber = loginEnter.ToString.Substring(0, 4) Then

                If emp.PasswordReq = True Or checkPassword = True Then
                    If loginEnter.Length < 8 Then
                        MsgBox("You MUST enter your password: Please Reenter or See Manager")
                        Exit Function
                    End If
                    If emp.PasscodeID = loginEnter.ToString.Substring(4, 4) Then
                        '       LoginEmployee(emp)
                        Return emp
                    Else
                        MsgBox("Password Incorrect: Please Reenter or See Manager")
                        Exit Function
                    End If
                End If
                '         LoginEmployee(emp)
                Return emp
            End If
        Next

        MsgBox("Employee Number: " & loginEnter & " Is Not In System")

    End Function

    Friend Function TestClockOut(ByVal loginEnter As String)
        Dim emp As Employee
        Dim empID As Integer
        Dim passcode As Integer
        Dim sqlEmpID As Integer
        Dim sqlPasscode As Integer
        Dim empName As String
        Dim empInSystem As Boolean = False
        '    Dim doesNotneedToClockIn As Boolean = False


        empID = CInt(loginEnter.ToString.Substring(0, 4))
        passcode = CInt(loginEnter.ToString.Substring(4, 4))

        '      Dim emp As Employee
        Dim isClockedIn As Integer

        For Each emp In AllEmployees
            If emp.EmployeeNumber = empID Then
                If emp.PasscodeID = loginEnter.ToString.Substring(4, 4) Then
                    empInSystem = True
                    Exit For
                Else
                    MsgBox("Password Incorrect: Please Reenter or See Manager")
                    Exit Function
                End If
            End If
        Next

        If empInSystem = False Then 'emp Is Nothing Then
            MsgBox("Employee Number: " & empID & " Is Not In System")
            Return False
        End If
        If emp.ClockInReq = False Then
            '     MsgBox(emp.FullName & " does not need to Clock In.")
            Return False
        End If

        Try
            isClockedIn = ActuallyLogIn(emp)
        Catch ex As Exception
            CloseConnection()
            Exit Function
        End Try

        If isClockedIn = 0 Then
            Return False
        ElseIf isClockedIn = 1 Then
            '   MsgBox(emp.FullName & " is currently logged-in as " & emp.JobCodeName)
            Return True
        Else
            '   MsgBox("Employee Is Clocked in more than once. Please See Manager.")
            Return True
        End If

    End Function


    Friend Function TestLogin222(ByRef emp As Employee)

        dsEmployee.Tables("ClockedIn").Clear()

        '     sql.SqlSelectCommandClockedIn.Parameters("@CompanyID").Value = CompanyID
        sql.SqlSelectCommandClockedIn.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandClockedIn.Parameters("@EmployeeID").Value = emp.EmployeeID

        '   this is pulling from view that has NULL Logout's
        '   if fails we want to fail in Login_Entered
        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        sql.SqlClockedIn.Fill(dsEmployee.Tables("ClockedIn"))
        sql.cn.Close()

        If dsEmployee.Tables("ClockedIn").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub Demo_LoadJobCodeFunctions(ByRef newEmployee As Employee)

        Dim oRow As DataRow
        Dim cRow As DataRow
        '     Dim isCLockedIn As Integer

        For Each oRow In dsEmployee.Tables("LoggedInEmployees").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If newEmployee.EmployeeID = oRow("EmployeeID") Then
                    '     isCLockedIn += 1

                    newEmployee.LoginTrackingID = oRow("LoginTrackingID")
                    newEmployee.JobCodeID = oRow("JobCode")
                    newEmployee.LogInTime = oRow("LogInTime")

                    For Each cRow In dsEmployee.Tables("JobCodeInfo").Rows
                        If newEmployee.JobCodeID = cRow("JobCodeID") Then
                            newEmployee.JobCodeName = cRow("JobCodeName")
                            newEmployee.Manager = cRow("Manager")
                            newEmployee.Cashier = cRow("Cashier")
                            newEmployee.Bartender = cRow("Bartender")
                            newEmployee.Server = cRow("Server")
                            newEmployee.Hostess = cRow("Hostess")
                            newEmployee.ClockInReq = cRow("ClockInReq")
                            newEmployee.PasswordReq = cRow("PasswordReq")
                        End If
                    Next
                    Exit For
                End If
            End If

        Next
        '      Return isCLockedIn

    End Sub

    Friend Function ActuallyLogIn(ByRef emp As Employee)
        '**********************************************
        'this needs to fail from sub it is called from

        Dim strCollection As String
        Dim dtr As SqlClient.SqlDataReader
        Dim cmd As SqlClient.SqlCommand
        Dim isCLockedIn As Integer
        Dim salaried As Employee

        For Each salaried In SalariedEmployees
            If salaried.EmployeeID = emp.EmployeeID Then
                If CheckingDatabaseConection() = True Then
                    Return 1
                Else
                    Return -999
                End If
                Exit Function
            End If
        Next

        If typeProgram = "Online_Demo" Then
            Dim oRow As DataRow
            '            isCLockedIn = Demo_LoadJobCodeFunctions(emp)
            For Each oRow In dsEmployee.Tables("LoggedInEmployees").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If emp.EmployeeID = oRow("EmployeeID") Then
                        isCLockedIn += 1
                    End If
                End If
            Next
            Return isCLockedIn
            Exit Function
        End If

        '444     Try
        strCollection = "SELECT LoginTrackingID, JobCode, LogInTime, JobCodeName, Manager, Cashier, Bartender, Server, Hostess, ClockInReq, PasswordReq FROM ClockedInView WHERE LocationID = '" & companyInfo.LocationID & "' AND EmployeeID = '" & emp.EmployeeID & "'"

        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        cmd = New SqlClient.SqlCommand(strCollection, sql.cn)

        dtr = cmd.ExecuteReader

        While dtr.Read()
            isCLockedIn += 1

            emp.LoginTrackingID = dtr("LoginTrackingID")
            emp.JobCodeID = dtr("JobCode")
            emp.LogInTime = dtr("LogInTime")
            emp.JobCodeName = dtr("JobCodeName")
            emp.Manager = dtr("Manager")
            emp.Cashier = dtr("Cashier")
            emp.Bartender = dtr("Bartender")
            emp.Server = dtr("Server")
            emp.Hostess = dtr("Hostess")
            emp.ClockInReq = dtr("ClockInReq")
            emp.PasswordReq = dtr("PasswordReq")

        End While

        dtr.Close()
        sql.cn.Close()
        'in the middle of try,catch
        '444   Catch ex As Exception

        '    If dtr.IsClosed = False Then
        '   dtr.Close()
        '  End If
        ' CloseConnection()
        '  MsgBox(ex.Message)
        '   End Try

        Return isCLockedIn

    End Function

    Friend Function DetermineSecondEmployeeAuthorization(ByRef authLogin As String) As Employee
        Dim empID As String
        Dim passcode As String
        Dim emp As Employee

        empID = authLogin.Substring(0, 4)
        passcode = authLogin.Substring(4, 4)

        For Each emp In AllEmployees
            If empID = emp.EmployeeNumber Then
                If passcode = emp.PasscodeID Then

                    actingManager = emp
                    Return emp
                Else
                    MsgBox("Passcode incorrect")
                    Exit Function
                End If
            End If
        Next
    End Function



    Friend Sub EnterEmployeeToLoginDatabase(ByRef emp As Employee)    '(ByVal clockInJunk As ClockInInfo222)

        If typeProgram = "Online_Demo" Then
            EnterEmployeeToLoginDataSet(emp)
            Exit Sub
        End If

        Dim cmd As SqlClient.SqlCommand
        Dim otPay As Decimal

        If emp.OTPayRate > emp.RegPayRate Then
            otPay = emp.OTPayRate
        Else
            otPay = emp.RegPayRate
        End If

        cmd = New SqlClient.SqlCommand("INSERT INTO AAALoginTracking (CompanyID, LocationID, DailyCode, EmployeeID, JobCode, LogInTime, TerminalsGroupID, RegPayRate, OTPayRate, Terminal) VALUES (@CompanyID, @LocationID, @DailyCode, @EmployeeID, @JobCode, @LogInTime, @TerminalsGroupID, @RegPayRate, @OTPayRate, @Terminal)", sql.cn)

        cmd.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", System.Data.SqlDbType.NChar, 6))
        cmd.Parameters("@CompanyID").Value = companyInfo.CompanyID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@LocationID", System.Data.SqlDbType.NChar, 6))
        cmd.Parameters("@LocationID").Value = companyInfo.LocationID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@DailyCode", SqlDbType.BigInt, 8))
        If currentTerminal.CurrentDailyCode = Nothing Then
            cmd.Parameters("@DailyCode").Value = 0
        Else
            cmd.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
        End If
        cmd.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int, 4))
        cmd.Parameters("@EmployeeID").Value = emp.EmployeeID 'clockInJunk.EmpID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@JobCode", SqlDbType.Int, 4))
        cmd.Parameters("@JobCode").Value = emp.JobCodeID  'clockInJunk.JobCodeID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@LogInTime", SqlDbType.DateTime, 4))
        cmd.Parameters("@LogInTime").Value = (Now.AddSeconds(-1 * Now.Second))
        cmd.Parameters.Add(New SqlClient.SqlParameter("@TerminalsGroupID", SqlDbType.Int, 4))
        cmd.Parameters("@TerminalsGroupID").Value = currentTerminal.TermGroupID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@RegPayRate", SqlDbType.Decimal, 5))
        cmd.Parameters("@RegPayRate").Value = emp.RegPayRate   'clockInJunk.RegPayRate
        cmd.Parameters.Add(New SqlClient.SqlParameter("@OTPayRate", SqlDbType.Decimal, 5))
        cmd.Parameters("@OTPayRate").Value = otPay
        cmd.Parameters.Add(New SqlClient.SqlParameter("@Terminal", SqlDbType.Int, 4))
        cmd.Parameters("@Terminal").Value = currentTerminal.TermPrimaryKey


        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            cmd.ExecuteNonQuery()
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
        End Try


    End Sub

    Friend Sub UpdateEmployeeToLoginDatabase(ByRef emp As Employee, ByRef clockOutJunk As ClockOutInfo)

        Dim cmd As SqlClient.SqlCommand

        cmd = New SqlClient.SqlCommand("UPDATE AAALoginTracking SET LogOutTime = @LogOutTime, TerminalsGroupID = @TerminalsGroupID, TipableSales = @TipableSales, DeclaredTips = @DeclaredTips, ChargedSales = @ChargedSales, ChargedTips = @ChargedTips, DailyCode = @DailyCode WHERE LoginTrackingID = @LoginTrackingID", sql.cn)


        cmd.Parameters.Add(New SqlClient.SqlParameter("@LoginTrackingID", SqlDbType.BigInt, 8))
        cmd.Parameters("@LoginTrackingID").Value = emp.LoginTrackingID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@DailyCode", SqlDbType.BigInt, 8))
        cmd.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
        cmd.Parameters.Add(New SqlClient.SqlParameter("@LogOutTime", SqlDbType.DateTime, 4))
        cmd.Parameters("@LogOutTime").Value = clockOutJunk.TimeOut
        cmd.Parameters.Add(New SqlClient.SqlParameter("@TerminalsGroupID", SqlDbType.Int, 4))
        cmd.Parameters("@TerminalsGroupID").Value = currentTerminal.TermGroupID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@TipableSales", SqlDbType.Decimal, 5))
        cmd.Parameters("@TipableSales").Value = clockOutJunk.TipableSales
        cmd.Parameters.Add(New SqlClient.SqlParameter("@DeclaredTips", SqlDbType.Decimal, 5))
        cmd.Parameters("@DeclaredTips").Value = clockOutJunk.DeclaredTips
        cmd.Parameters.Add(New SqlClient.SqlParameter("@ChargedSales", SqlDbType.Decimal, 5))
        cmd.Parameters("@ChargedSales").Value = clockOutJunk.ChargedSales
        cmd.Parameters.Add(New SqlClient.SqlParameter("@ChargedTips", SqlDbType.Decimal, 5))
        cmd.Parameters("@ChargedTips").Value = clockOutJunk.ChargedTips
        '       If currentTerminal.CurrentDailyCode = Nothing Then
        'should always have a dailycode b/c otherwise 
        'we could not get tot this screen
        '      Else
        '     End If

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            cmd.ExecuteNonQuery()
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

    End Sub



    Friend Sub AddPaymentsAndCredits222(ByVal payType As String, ByVal amount As Decimal)
        '   *********************
        '   not using yet
        '   by doing this we do not update the dataset which grids are linked to

        Dim cmd As SqlClient.SqlCommand

        cmd = New SqlClient.SqlCommand("INSERT INTO PaymentsAndCredits(ExperienceNumber, CheckNumber, PaymentType, PaymentAmount, Tip, TipAdjustment, Applied) VALUES (@ExperienceNumber, @CheckNumber, @PaymentType, @PaymentAmount, @Tip, @TipAdjustment, @Applied)", sql.cn)

        cmd.Parameters.Add(New SqlClient.SqlParameter("@ExperienceNumber", SqlDbType.BigInt, 8))
        cmd.Parameters("@ExperienceNumber").Value = currentTable.ExperienceNumber
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CheckNumber", System.Data.SqlDbType.Int, 4, "CheckNumber"))
        cmd.Parameters("@CheckNumber").Value = currentTable.CheckNumber
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PaymentType", System.Data.SqlDbType.Int, 4, "PaymentTypeID"))
        cmd.Parameters("@PaymentType").Value = payType

        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PaymentAmount", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(2, Byte), "PaymentAmount", System.Data.DataRowVersion.Current, Nothing))
        cmd.Parameters("@PaymentAmount").Value = amount

        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Tip", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(2, Byte), "Tip", System.Data.DataRowVersion.Current, Nothing))
        cmd.Parameters("@Tip").Value = 0

        '       cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TipAdjustment", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(2, Byte), "TipAdjustment", System.Data.DataRowVersion.Current, Nothing))
        '      cmd.Parameters("@TipAdjustment").Value = 0

        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Applied", System.Data.SqlDbType.Bit, 1, "Applied"))
        cmd.Parameters("@Applied").Value = 0

        '        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PaymentAuthorizeID", System.Data.SqlDbType.Int, 4, "PaymentAuthorizeID"))
        '       cmd.Parameters("@PaymentType").Value = authID


        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        cmd.ExecuteNonQuery()
        sql.cn.Close()

        dsOrder.Tables("PaymentsAndCredits").Clear()

        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        sql.SqlSelectCommandPayments.Parameters("@ExperienceNumber").Value = currentTable.ExperienceNumber
        sql.SqlDataAdapterPayments.Fill(dsOrder.Tables("PaymentsAndCredits"))
        sql.cn.Close()

    End Sub

    Friend Sub PopulateAllTablesWithStatus(ByVal fromStart As Boolean)

        'this is pulled from    stored proc:  AllTablesSelectCommand
        '                       view: TableStatusView

        If typeProgram = "Online_Demo" Then
            Exit Sub
        End If

        Dim oRow As DataRow
        dsOrder.Tables("AllTables").Rows.Clear()

        sql.SqlSelectCommandAllTables.Parameters("@LocationID").Value = companyInfo.LocationID

        Try
            '   gets a collection of all tables in TableOverview
            '   with Last Status from Experience Status Table
            If fromStart = False Then
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            End If
            sql.SqlDataAdapterAllTables.Fill(dsOrder.Tables("AllTables"))
            If fromStart = False Then
                sql.cn.Close()
            End If
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

        Exit Sub
        '222 below
        '   the following finds any table with no previous experience
        '   fills in the default availability in TableOverview
        For Each oRow In dsOrder.Tables("AllTables").Rows
            If oRow("TableStatusID") Is DBNull.Value Or oRow("Available") = 0 Then
                oRow("TableStatusID") = oRow("Available")
                oRow("LastStatusTime") = Now
                oRow("SatTime") = Now
                oRow("ItemsOnHold") = 0
            End If
        Next

    End Sub

    Friend Sub ChangeStatusInDataBase(ByVal newStatus As Integer, ByVal orderNumber As Int64, ByVal isMainCourse As Boolean, ByVal avgDollar As Decimal, ByVal chkDown As DateTime, ByVal availSeating As DateTime)

        Dim oRow As DataRow

        If currentTerminal.TermMethod = "Quick" Or currentTable.TicketNumber > 0 Then 'currentTable.TabID = -888 Then
            For Each oRow In dsOrder.Tables("QuickTickets").Rows
                If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                    oRow("LastStatusTime") = Now
                    If Not chkDown = Nothing Then
                        oRow("CheckDown") = chkDown
                    End If
                    If Not availSeating = Nothing Then
                        oRow("AvailForSeating") = availSeating
                    End If
                    oRow("LastStatus") = newStatus
                End If
            Next

        ElseIf currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then

            If currentTable.IsTabNotTable = False Then

                For Each oRow In dsOrder.Tables("AvailTables").Rows
                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        oRow("LastStatusTime") = Now
                        If Not chkDown = Nothing Then
                            oRow("CheckDown") = chkDown
                        End If
                        If Not availSeating = Nothing Then
                            oRow("AvailForSeating") = availSeating
                        End If
                        oRow("LastStatus") = newStatus
                    End If
                Next

            Else
                For Each oRow In dsOrder.Tables("AvailTabs").Rows
                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        oRow("LastStatusTime") = Now
                        If Not chkDown = Nothing Then
                            oRow("CheckDown") = chkDown
                        End If
                        If Not availSeating = Nothing Then
                            oRow("AvailForSeating") = availSeating
                        End If
                        oRow("LastStatus") = newStatus
                    End If
                Next
            End If
        End If

        'sss       SaveAvailTabsAndTables()

    End Sub

    Friend Sub PopulateQuickTicket()
        dsOrder.Tables("QuickTickets").Rows.Clear()

        If typeProgram = "Online_Demo" Then


            If dsOrderDemo.Tables("QuickTickets").Rows.Count > 0 Then
                Dim filterString As String
                Dim NotfilterString As String
                filterString = "TerminalID = " & currentTerminal.TermID
                NotfilterString = "NOT TerminalID = " & currentTerminal.TermID
                Demo_FilterDemoDataTabble(dsOrderDemo.Tables("QuickTickets"), dsOrder.Tables("QuickTickets"), filterString, NotfilterString)
            End If
            '    MsgBox(dsOrderDemo.Tables("QuickTickets").Rows.Count)

            Exit Sub
        End If

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            '      sql.SqlSelectCommandQuickTicketSP.Parameters("@CompanyID").Value = CompanyID
            If currentTerminal.TermMethod = "Quick" Then
                sql.SqlSelectCommandQuickTicketSP.Parameters("@LocationID").Value = companyInfo.LocationID
                sql.SqlSelectCommandQuickTicketSP.Parameters("@TerminalID").Value = currentTerminal.TermID
                sql.SqlSelectCommandQuickTicketSP.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
                sql.SqlQuickTicketSP.Fill(dsOrder.Tables("QuickTickets"))
            Else
                sql.SqlSelectCommandQuickTicketsBar.Parameters("@LocationID").Value = companyInfo.LocationID
                sql.SqlSelectCommandQuickTicketsBar.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
                sql.SqlQuickTicketsBar.Fill(dsOrder.Tables("QuickTickets"))

            End If

            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
        End Try

    End Sub

    Friend Sub PopulateLoggedInEmployees(ByVal fromStart As Boolean)

        dsEmployee.Tables("LoggedInEmployees").Rows.Clear()

        sql.SqlSelectCommandClockedInList.Parameters("@CompanyID").Value = companyInfo.CompanyID
        sql.SqlSelectCommandClockedInList.Parameters("@LocationID").Value = companyInfo.LocationID

        If fromStart = False Then
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        End If
        sql.SqlClockedInList.Fill(dsEmployee.Tables("LoggedInEmployees"))
        If fromStart = False Then
            sql.cn.Close()
        End If

    End Sub

    Friend Sub Demo_FilterDemoDataTabble(ByVal dtFrom As DataTable, ByRef dtTO As DataTable, ByVal filter As String, ByVal NOTfilter As String)

        Dim iArray() As DataRow
        Dim dArray() As DataRow
        Dim i As Integer
        Dim demoRow As DataRow

        '        MsgBox(dtTO.TableName.ToString)
        '       MsgBox(dtTO.Rows.Count)
        '      MsgBox(dtFrom.Rows.Count)

        Try
            iArray = dtFrom.Select(filter)
            dArray = dtFrom.Select(NOTfilter)

            For i = 0 To (iArray.Length - 1)
                dtTO.ImportRow(iArray(i))

                For Each demoRow In dtFrom.Rows
                    If Not demoRow.RowState = DataRowState.Deleted Or Not demoRow.RowState = DataRowState.Detached Then
                        '                Try
                        '                If dtTO.TableName.ToString = "OpenOrders" Then
                        '                MsgBox(demoRow("ItemName"))
                        '                MsgBox(demoRow(0))
                        '               MsgBox(dtTO.Rows(i)(0))
                        '           End If
                        ''               Catch ex As Exception
                        '          End Try

                        If demoRow(0) = dtTO.Rows(i)(0) Then
                            demoRow.Delete()
                            dtFrom.AcceptChanges()
                            Exit For
                        End If
                    End If
                Next

            Next
        Catch ex As Exception

        End Try


        '      dtFrom.Clear()
        '     For i = 0 To (dArray.Length - 1)
        '    dtFrom.ImportRow(dArray(i))
        '   Next
        dtTO.AcceptChanges()
        dtFrom.AcceptChanges()

    End Sub
    Friend Sub Demo_FilterDontDelete(ByVal dtFrom As DataTable, ByRef dtTO As DataTable, ByVal filter As String)

        Dim iArray() As DataRow
        Dim i As Integer

        Try
            iArray = dtFrom.Select(filter)

            For i = 0 To (iArray.Length - 1)
                dtTO.ImportRow(iArray(i))
            Next
        Catch ex As Exception

        End Try

    End Sub

    Friend Sub PopulateTabsAndTables(ByVal emp As Employee, ByVal dc As Int64, ByVal IsBartender As Boolean, ByVal IsOneBartender As Boolean, ByRef empCollection As EmployeeCollection)
        dsOrder.Tables("AvailTables").Rows.Clear()
        dsOrder.Tables("AvailTabs").Rows.Clear()

        '     Dim todaysDate As Date
        '    Dim yesterdaysDate As Date

        '      MsgBox(dsOrderDemo.Tables("AvailTables").Rows.Count)

        If typeProgram = "Online_Demo" Then
            Dim filterString As String = ""
            Dim NotfilterString As String = ""
            Dim firstPass As Boolean = True

            If IsBartender = True And IsOneBartender = False Then
                Dim BarMan As Employee

                For Each BarMan In empCollection    'currentBartenders
                    If firstPass = True Then
                        firstPass = False
                        filterString = "EmployeeID = " & BarMan.EmployeeID
                        NotfilterString = "NOT EmployeeID = " & BarMan.EmployeeID
                    Else
                        filterString = filterString + " OR EmployeeID = " & BarMan.EmployeeID
                        NotfilterString = filterString + " OR NOT EmployeeID = " & BarMan.EmployeeID
                    End If
                Next
                Demo_FilterDontDelete(dsOrderDemo.Tables("AvailTables"), dsOrder.Tables("AvailTables"), filterString) ', NotfilterString)
                Demo_FilterDontDelete(dsOrderDemo.Tables("AvailTabs"), dsOrder.Tables("AvailTabs"), filterString) ', NotfilterString)

            Else
                filterString = "EmployeeID = " & emp.EmployeeID
                NotfilterString = "NOT EmployeeID = " & emp.EmployeeID
                Demo_FilterDontDelete(dsOrderDemo.Tables("AvailTables"), dsOrder.Tables("AvailTables"), filterString) ', NotfilterString)
                Demo_FilterDontDelete(dsOrderDemo.Tables("AvailTabs"), dsOrder.Tables("AvailTabs"), filterString) ', NotfilterString)
            End If
            Exit Sub
        End If


        '   ********************
        '   if its between mindight and 6am. (or prreset time) we look at last 24 hours
        '       todaysDate = Format(Today, "D")
        '      yesterdaysDate = Format(Today.AddDays(-52), "D")


        If IsBartender = True And IsOneBartender = False Then
            Dim BarMan As Employee

            Try
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                For Each BarMan In empCollection    'currentBartenders
                    '  **** uses parameter LastStatus > 1 in SQL SERVER stored procedures
                    '   **** we now use SQL Helper 
                    '   can remove stored procedures
                    '   later we can do in order of currentServer.empID
                    '   we will have to loop through this twice  ?????
                    '             sql.SqlSelectCommandAvailTablesSP.Parameters("@CompanyID").Value = CompanyID
                    sql.SqlSelectCommandAvailTablesSP.Parameters("@LocationID").Value = companyInfo.LocationID
                    sql.SqlSelectCommandAvailTablesSP.Parameters("@EmployeeID").Value = BarMan.EmployeeID
                    sql.SqlSelectCommandAvailTablesSP.Parameters("@DailyCode").Value = dc
                    sql.SqlAvailTablesSP.Fill(dsOrder.Tables("AvailTables"))

                    '              sql.SqlSelectCommandAvailTabsSP.Parameters("@CompanyID").Value = CompanyID
                    sql.SqlSelectCommandAvailTabsSP.Parameters("@LocationID").Value = companyInfo.LocationID
                    sql.SqlSelectCommandAvailTabsSP.Parameters("@EmployeeID").Value = BarMan.EmployeeID
                    sql.SqlSelectCommandAvailTabsSP.Parameters("@DailyCode").Value = dc
                    sql.SqlAvailTabsSP.Fill(dsOrder.Tables("AvailTabs"))
                Next
                sql.cn.Close()

            Catch ex As Exception
                CloseConnection()
                MsgBox(ex.Message)
                '               PopulateTabsAndTablesWhenDown(IsBartender, IsOneBartender)
            End Try

        Else
            Try
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                '           sql.SqlSelectCommandAvailTablesSP.Parameters("@CompanyID").Value = CompanyID
                sql.SqlSelectCommandAvailTablesSP.Parameters("@LocationID").Value = companyInfo.LocationID
                sql.SqlSelectCommandAvailTablesSP.Parameters("@EmployeeID").Value = emp.EmployeeID  'currentServer.EmployeeID
                sql.SqlSelectCommandAvailTablesSP.Parameters("@DailyCode").Value = dc
                sql.SqlAvailTablesSP.Fill(dsOrder.Tables("AvailTables"))
                '            sql.SqlSelectCommandAvailTabsSP.Parameters("@CompanyID").Value = CompanyID
                sql.SqlSelectCommandAvailTabsSP.Parameters("@LocationID").Value = companyInfo.LocationID
                sql.SqlSelectCommandAvailTabsSP.Parameters("@DailyCode").Value = dc
                sql.SqlSelectCommandAvailTabsSP.Parameters("@EmployeeID").Value = emp.EmployeeID 'currentServer.EmployeeID
                sql.SqlAvailTabsSP.Fill(dsOrder.Tables("AvailTabs"))
                sql.cn.Close()

            Catch ex As Exception
                CloseConnection()
                MsgBox(ex.Message)
                '              PopulateTabsAndTablesWhenDown(IsBartender, IsOneBartender)
            End Try

        End If

    End Sub

    Friend Sub PopulateTabsAndTablesEveryone(ByVal emp As Employee, ByVal dc As Int64, ByVal IsBartender As Boolean, ByVal IsOneBartender As Boolean, ByRef empCollection As EmployeeCollection)
        dsOrder.Tables("AvailTables").Rows.Clear()
        dsOrder.Tables("AvailTabs").Rows.Clear()
        dsOrder.Tables("QuickTickets").Rows.Clear()

        'already in middle of Try/catch
        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()

        If Not currentTerminal.TermMethod = "Quick" Then
            sql.SqlSelectCommandOpenTables.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandOpenTables.Parameters("@DailyCode").Value = dc
            sql.SqlOpenTables.Fill(dsOrder.Tables("AvailTables"))
            '     sql.SqlOpenTickets.Fill(dsOrder.Tables("OpenTickets"))
            sql.SqlSelectCommandOpenTabs.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandOpenTabs.Parameters("@DailyCode").Value = dc
            sql.SqlOpenTabs.Fill(dsOrder.Tables("AvailTabs"))
        End If

        'we fill this for non-quick for Beth's Tabs
        sql.SqlSelectCommandOpenQuick.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandOpenQuick.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
        sql.SqlOpenQuick.Fill(dsOrder.Tables("QuickTickets"))

        sql.cn.Close()

    End Sub

    Friend Sub CreateDataViews(ByVal empID As Integer, ByVal filterQuick As Boolean)

        dvAvailTables = New DataView
        With dvAvailTables
            .Table = dsOrder.Tables("AvailTables")
            .RowFilter = "LastStatus < 8 AND LastStatus > 1" '(-2)" '(-1) was voided check
            .RowStateFilter = DataViewRowState.CurrentRows
            .Sort = "ExperienceDate DESC" 'ASC"
        End With

        dvTransferTables = New DataView
        With dvTransferTables
            .Table = dsOrder.Tables("AvailTables")
            .RowFilter = "LastStatus = 8"
            .Sort = "ExperienceDate DESC" 'ASC"
        End With

        dvAvailTabs = New DataView
        With dvAvailTabs
            .Table = dsOrder.Tables("AvailTabs")
            .RowFilter = "LastStatus < 8 AND LastStatus > 1" '" ' AND LastStatus > (-2)"
            .RowStateFilter = DataViewRowState.CurrentRows
            .Sort = "TabName ASC"
        End With

        dvTransferTabs = New DataView
        With dvTransferTabs
            .Table = dsOrder.Tables("AvailTabs")
            .RowFilter = "LastStatus = 8"
            .Sort = "TabName ASC"
        End With

        If currentTerminal.TermMethod = "Bar" Then
            '444       dvQuickTickets = New DataView
            With dvQuickTickets
                .Table = dsOrder.Tables("QuickTickets")
                If filterQuick = True Then
                    .RowFilter = "EmployeeID = " & empID
                End If
                '444  .Sort = "ExperienceDate ASC"
                .Sort = "ExperienceDate DESC"
            End With
        End If

    End Sub

    Friend Sub CreateDataViewsOrder()   'Handles testgridview.RePopulateDataViews

        '444 
        dvOrder = New DataView
        dvOrderPrint = New DataView
        dvOrderTopHierarcy = New DataView
        dvOrderHolds = New DataView
        dvKitchen = New DataView

        With dvOrder
            .Table = dsOrder.Tables("OpenOrders")
            .AllowEdit = True
            .Sort = "CustomerNumber, sii, si2, sin"
        End With

        With dvOrderPrint
            .Table = dsOrder.Tables("OpenOrders")
            .AllowEdit = True
            .RowFilter = "ItemStatus = 0"
            .Sort = "CourseNumber, CustomerNumber, sii, si2, sin"
        End With

        With dvOrderTopHierarcy
            .Table = dsOrder.Tables("OpenOrders")
            .AllowEdit = True
            .RowFilter = "sin = sii" ' AND CheckNumber ='" & currentTable.CheckNumber & "'"
            '        .RowStateFilter = DataViewRowState.CurrentRows
            .Sort = "CustomerNumber, sii, si2, sin"
        End With

        With dvOrderHolds
            '   is all holds for check
            .Table = dsOrder.Tables("OpenOrders")
            .AllowEdit = True
            .RowFilter = "ItemStatus = 1"
            '         .RowStateFilter = DataViewRowState.CurrentRows
            .Sort = "CustomerNumber, sii, si2, sin"
        End With

        With dvKitchen
            '   is all items sent to kitchen but not delivered for check
            .Table = dsOrder.Tables("OpenOrders")
            .AllowEdit = True
            .RowFilter = "ItemStatus = 2"
            '        .RowStateFilter = DataViewRowState.CurrentRows
            .Sort = "CustomerNumber, sii, si2, sin"
        End With
        '      Me.testgridview.CalculateSubTotal()

    End Sub

    Friend Sub DisposeDataViewsOrder()
        '444
        '444   Exit Sub

        dvOrder.Dispose()
        dvOrderPrint.Dispose()
        dvOrderTopHierarcy.Dispose()
        dvOrderHolds.Dispose()
        dvKitchen.Dispose()


    End Sub

    Friend Sub CreateDataViewsPizza()
        '     dvPizzaFull = New DataView
        '     dvPizzaFirst = New DataView
        '     dvPizzaSecond = New DataView

        With dvPizzaFull
            .Table = dsOrder.Tables("OpenOrders")
            .RowFilter = "ItemID > 0 AND si2 = 1 AND sii = " & currentTable.ReferenceSIN
            .Sort = "sin"
        End With

        With dvPizzaFirst
            .Table = dsOrder.Tables("OpenOrders")
            .RowFilter = "ItemID > 0 AND si2 = 2 AND sii = " & currentTable.ReferenceSIN
            .Sort = "sin"
        End With

        With dvPizzaSecond
            .Table = dsOrder.Tables("OpenOrders")
            .RowFilter = "ItemID > 0 AND si2 = 3 AND sii = " & currentTable.ReferenceSIN
            .Sort = "sin"
        End With

    End Sub


    Friend Sub CalculateClosingTotal()

        Try
            If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
                currentTable.SubTotal = dsOrder.Tables("OpenOrders").Compute("Sum(Price)", "") ' "ItemStatus < 8")
            Else
                currentTable.SubTotal = 0
            End If
        Catch ex As Exception
            'this is the exception when all items were transfered
            currentTable.SubTotal = 0
        End Try

    End Sub


    Private Sub PopulateTabsAndTablesWhenDown222(ByVal IsBartender As Boolean, ByVal IsOneBartender As Boolean)

        Dim bRow As DataRow
        Dim oRow As DataRow

        If IsBartender = True And IsOneBartender = False Then
            Dim BarMan As Employee

            For Each BarMan In currentBartenders
                For Each bRow In dsBackup.Tables("AvailTablesTerminal").Rows

                    If bRow("EmployeeID") = BarMan.EmployeeID Then
                        If Not bRow("TableNumber") Is DBNull.Value And bRow("LastStatus") > 1 Then
                            oRow = dsOrder.Tables("AvailTables").NewRow
                            oRow = CopyOneRowToAnotherAvailTabsAndTables222(bRow, oRow)
                            dsOrder.Tables("AvailTables").Rows.Add(oRow)
                        End If
                    End If
                Next

                For Each bRow In dsBackup.Tables("AvailTabsTerminal").Rows
                    If bRow("EmployeeID") = BarMan.EmployeeID Then
                        If Not bRow("TabID") Is DBNull.Value And bRow("LastStatus") > 1 Then
                            oRow = dsOrder.Tables("AvailTabs").NewRow
                            oRow = CopyOneRowToAnotherAvailTabsAndTables222(bRow, oRow)
                            dsOrder.Tables("AvailTabs").Rows.Add(oRow)
                        End If
                    End If
                Next
            Next

        Else

            For Each bRow In dsBackup.Tables("AvailTablesTerminal").Rows
                If bRow("EmployeeID") = currentServer.EmployeeID Then
                    If Not bRow("TableNumber") Is DBNull.Value And bRow("LastStatus") > 1 Then
                        oRow = dsOrder.Tables("AvailTables").NewRow
                        oRow = CopyOneRowToAnotherAvailTabsAndTables222(bRow, oRow)
                        dsOrder.Tables("AvailTables").Rows.Add(oRow)
                    End If
                End If
            Next

            For Each bRow In dsBackup.Tables("AvailTabsTerminal").Rows
                If bRow("EmployeeID") = currentServer.EmployeeID Then
                    If Not bRow("TabID") Is DBNull.Value And bRow("LastStatus") > 1 Then
                        oRow = dsOrder.Tables("AvailTabs").NewRow
                        oRow = CopyOneRowToAnotherAvailTabsAndTables222(bRow, oRow)
                        dsOrder.Tables("AvailTabs").Rows.Add(oRow)
                    End If
                End If
            Next
        End If

        dsOrder.Tables("AvailTables").AcceptChanges()
        dsOrder.Tables("AvailTabs").AcceptChanges()

    End Sub

    Friend Sub UpdateMethodDataset()
        Dim orow As DataRow

        If currentTerminal.TermMethod = "Quick" Or currentTable.TicketNumber > 0 Then 'currentTable.TabID = -888 Then
            For Each orow In dsOrder.Tables("QuickTickets").Rows
                If orow("ExperienceNumber") = currentTable.ExperienceNumber Then
                    orow("MethodUse") = currentTable.MethodUse
                End If
            Next
        ElseIf currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            If currentTable.IsTabNotTable = False Then
                For Each orow In dsOrder.Tables("AvailTables").Rows
                    If orow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        orow("MethodUse") = currentTable.MethodUse
                    End If
                Next
            Else
                For Each orow In dsOrder.Tables("AvailTabs").Rows
                    If orow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        orow("MethodUse") = currentTable.MethodUse
                    End If
                Next
            End If
        End If
        '      DefineMethodDirection()
        '      GenerateOrderTables.SaveAvailTabsAndTables()    'will only effect QuickService

    End Sub

    Friend Sub SaveAvailTabsAndTables()

        If currentTerminal.TermMethod = "Quick" Or currentTable.TicketNumber > 0 Then   ' = -888 Then
            If currentTerminal.TermMethod = "Quick" Then
                sql.SqlQuickTicketSP.Update(dsOrder, "QuickTickets")
            Else
                sql.SqlQuickTicketsBar.Update(dsOrder, "QuickTickets")
            End If

        ElseIf currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            If currentTable.IsTabNotTable = False Then
                sql.SqlAvailTablesSP.Update(dsOrder, "AvailTables")
            Else
                sql.SqlAvailTabsSP.Update(dsOrder, "AvailTabs")
            End If
        End If

    End Sub

    Friend Sub SaveTerminalTabsAndTables222()
        '     Dim oRow As DataRow
        Dim bRow As DataRow

        '   we place all tabs and table avail info in both regular and terminal datasets
        '   in Perform New Experience ADD
        '   a "1" in dbUP if up, "0" if down, "2" if changed when down
        If currentTable.IsTabNotTable = False Then
            bRow = (dsBackup.Tables("AvailTablesTerminal").Rows.Find(currentTable.ExperienceNumber))
        Else
            bRow = (dsBackup.Tables("AvailTabsTerminal").Rows.Find(currentTable.ExperienceNumber))
        End If

        If Not (bRow Is Nothing) Then
            '   we assign 2 when we made a change to a row created when db UP
            If bRow("dbUP") = 1 Then
                bRow("dbUP") = 2
            End If
        End If

        Exit Sub

        ' not doing this
        If currentTable.IsTabNotTable = False Then
            '       sql222.SqlDataAdapterAvailTablesTerminal.Update(dsBackup.Tables("AvailTablesTerminal"))
        Else
            '         sql222.SqlDataAdapterAvailTabsTerminal.Update(dsBackup.Tables("AvailTabsTerminal"))
        End If

    End Sub

    Friend Function PopulateThisExperience(ByVal expNum As Int64, ByVal fromStart As Boolean) As Boolean
        Dim isHeld As Boolean
        Dim newvalueAcct As String
        Dim newvalueExpDate As String
        Dim oRow As DataRow

        dsOrder.Tables("CurrentlyHeld").Rows.Clear()
        dsOrder.Tables("OpenOrders").Rows.Clear()
        dsOrder.Tables("PaymentsAndCredits").Rows.Clear()
        '   dsOrder.Tables("StatusChange").Rows.Clear()
        dsOrder.Tables("OrderDetail").Rows.Clear()
        '   dsOrder.Tables("OrderForceFree").Rows.Clear()

        '    MsgBox(dsOrderDemo.Tables("AvailTables").Rows.Count)
        '   MsgBox(dsOrder.Tables("AvailTables").Rows.Count)
        If typeProgram = "Online_Demo" Then
            Dim filterString As String
            Dim NotfilterString As String
            filterString = "ExperienceNumber = " & expNum
            NotfilterString = "NOT ExperienceNumber = " & expNum

            Demo_FilterDemoDataTabble(dsOrderDemo.Tables("OpenOrders"), dsOrder.Tables("OpenOrders"), filterString, NotfilterString) '"ExperienceNumber = '" & expNum & "'")
            Demo_FilterDemoDataTabble(dsOrderDemo.Tables("PaymentsAndCredits"), dsOrder.Tables("PaymentsAndCredits"), filterString, NotfilterString) ' "ExperienceNumber = '" & expNum & "'")
            Demo_FilterDemoDataTabble(dsOrderDemo.Tables("OrderDetail"), dsOrder.Tables("OrderDetail"), filterString, NotfilterString) '"ExperienceNumber = '" & expNum & "'")
            'Demo_FilterDemoDataTabble(dsOrderDemo.Tables("CurrentlyHeld"), dsOrder.Tables("CurrentlyHeld"), filterString, NotfilterString) '"ExperienceNumber = '" & expNum & "'")
            Exit Function
        End If

        Try
            sql.SqlSelectCommandCurrentlyHeld.Parameters("@ExperienceNumber").Value = expNum

            sql.SqlSelectCommandOpenOrdersSP.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandOpenOrdersSP.Parameters("@ExperienceNumber").Value = expNum

            sql.SqlSelectCommandPayments.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandPayments.Parameters("@ExperienceNumber").Value = expNum

            '        sql.SqlSelectCommandESC.Parameters("@LocationID").Value = companyInfo.LocationID
            '    sql.SqlSelectCommandESC.Parameters("@ExperienceNumber").Value = expNum

            sql.SqlSelectCommandOrderDetail.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandOrderDetail.Parameters("@ExperienceNumber").Value = expNum

            '        sql.SqlSelectCommandOrderForceFree.Parameters("@LocationID").Value = companyInfo.LocationID
            '       sql.SqlSelectCommandOrderForceFree.Parameters("@ExperienceNumber").Value = expNum
            If fromStart = False Then
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            End If
            sql.SqlDataAdapterCurrentlyHeld.Fill(dsOrder.Tables("CurrentlyHeld"))
            sql.SqlDataAdapterOpenOrdersSP.Fill(dsOrder.Tables("OpenOrders"))
            sql.SqlDataAdapterPayments.Fill(dsOrder.Tables("PaymentsAndCredits"))
            '         sql.SqlDataAdapterESC.Fill(dsOrder.Tables("StatusChange"))
            sql.SqlDataAdapterOrderDetail.Fill(dsOrder.Tables("OrderDetail"))
            '        sql.SqlDataAdapterOrderForceFree.Fill(dsOrder.Tables("OrderForceFree"))
            ' at some point we can do this first
            '   if held no need to populate all other tables

            If dsOrder.Tables("CurrentlyHeld").Rows.Count = 1 Then

                If Not dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld") Is DBNull.Value Then
                    If dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld") = currentServer.FullName Then
                        isHeld = False
                    Else
                        isHeld = True
                    End If
                    '       currentTable.CurrentlyHeld = dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld")
                Else
                    ' now we will hold it
                    isHeld = False
                    UpdateCurrentlyHeld(currentServer.FullName, expNum)
                    '         dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld") = currentServer.FullName
                    '        sql.SqlDataAdapterCurrentlyHeld.Update(dsOrder.Tables("CurrentlyHeld"))
                    '       dsOrder.Tables("CurrentlyHeld").AcceptChanges()
                End If
            End If

            If fromStart = False Then
                sql.cn.Close()
            End If

        Catch ex As Exception
            CloseConnection()
            MsgBox("Data base connection problem: " & connectserver)
        End Try

        If fromStart = False Then
            For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    '   If oRow("PaymentTypeID") > 1 Then
                    If oRow("PaymentFlag") = "cc" Or oRow("PaymentFlag") = "Gift" Or oRow("PaymentFlag") = "Issue" Then
                        If Not oRow("AccountNumber").Substring(0, 4) = "xxxx" And Not oRow("AccountNumber") = "Manual" Then
                            Try
                                If oRow("AccountNumber").ToString.Length > 20 Then
                                    newvalueAcct = CryOutloud.Decrypt(oRow("AccountNumber"), "test")
                                    oRow("AccountNumber") = newvalueAcct
                                End If

                                'can't encrypt exp date b/c database only holds 4 chars
                                '     newvalueExpDate = CryOutloud.Decrypt(oRow("CCExpiration"), "test")
                                '    oRow("CCExpiration") = newvalueExpDate
                            Catch ex As Exception

                            End Try

                        End If
                    End If
                End If
            Next
        End If

        Return isHeld

    End Function

    Friend Sub UpdateCurrentlyHeld(ByVal empName As String, ByVal expNum As Int64)

        Dim cmd As SqlClient.SqlCommand

        cmd = New SqlClient.SqlCommand("UPDATE ExperienceTable SET CurrentlyHeld = @CurrentlyHeld WHERE ExperienceNumber = @ExperienceNumber", sql.cn)

        cmd.Parameters.Add(New SqlClient.SqlParameter("@ExperienceNumber", SqlDbType.BigInt, 8))
        cmd.Parameters("@ExperienceNumber").Value = expNum

        cmd.Parameters.Add(New SqlClient.SqlParameter("@CurrentlyHeld", SqlDbType.NVarChar, 50))
        If Not empName = Nothing Then
            cmd.Parameters("@CurrentlyHeld").Value = empName
        Else
            cmd.Parameters("@CurrentlyHeld").Value = DBNull.Value
        End If

        cmd.ExecuteNonQuery()

    End Sub

    Friend Sub UpdateCurrentlyHeldOnRelease(ByVal empName As String, ByVal expNum As Int64, ByVal itemsOnHold As Int16)

        Dim cmd As SqlClient.SqlCommand

        cmd = New SqlClient.SqlCommand("UPDATE ExperienceTable SET CurrentlyHeld = @CurrentlyHeld, ItemsOnHold = @ItemsOnHold WHERE ExperienceNumber = @ExperienceNumber", sql.cn)

        cmd.Parameters.Add(New SqlClient.SqlParameter("@ExperienceNumber", SqlDbType.BigInt, 8))
        cmd.Parameters("@ExperienceNumber").Value = expNum

        cmd.Parameters.Add(New SqlClient.SqlParameter("@CurrentlyHeld", SqlDbType.NVarChar, 50))
        If Not empName = Nothing Then
            cmd.Parameters("@CurrentlyHeld").Value = empName
        Else
            cmd.Parameters("@CurrentlyHeld").Value = DBNull.Value
        End If

        cmd.Parameters.Add(New SqlClient.SqlParameter("@ItemsOnHold", SqlDbType.SmallInt, 2))
        cmd.Parameters("@ItemsOnHold").Value = itemsOnHold

        cmd.ExecuteNonQuery()

    End Sub


    Friend Sub TransferTableToOpenOrder(ByVal empID As Integer, ByVal expNum As Int64, ByVal newStatus As Integer)

        Dim oRow As DataRow
        Dim bRow As DataRow
        Dim RemainingChecks As Boolean

        ' **********not sure

        '?????        If RemainingChecks = False Then
        '   place a 1 or 9 in LastStatus in ExperienceTable

        '      If currentTerminal.TermMethod = "Quick" Or currentTable.TicketNumber > 0 Then
        '     For Each oRow In dsOrder.Tables("QuickTickets").Rows

        If currentTable.IsTabNotTable = True Then
            For Each oRow In dsOrder.Tables("AvailTabs").Rows
                If oRow("ExperienceNumber") = expNum Then
                    If newStatus = 1 Then                            'not sure what this is for??   
                        '   if = 1 then must meet extra criteria
                    Else    'new status <> 1 (making active from transfer)
                        oRow("LastStatusTime") = Now
                        oRow("LastStatus") = newStatus
                    End If
                End If
            Next
        Else
            For Each oRow In dsOrder.Tables("AvailTables").Rows
                If oRow("ExperienceNumber") = expNum Then
                    If newStatus = 1 Then
                        '   if = 1 then must meet extra criteria
                    Else    'new status <> 1 (making active from transfer)
                        oRow("LastStatusTime") = Now
                        oRow("LastStatus") = newStatus

                    End If
                End If
            Next
        End If
        '     End If

        'sss      SaveAvailTabsAndTables()
        '222  AddStatusChangeData(currentTable.ExperienceNumber, 2, Nothing, 0, Nothing)

    End Sub

    Friend Function CopyViewForTransferItem(ByVal oldRow As DataRowView, ByVal empID As Integer, ByVal expNum As Int64, ByVal isTransferedItem As Boolean, ByVal checkNum As Integer)
        'for changing customer during order
        '   this could be used for any transfer

        '   *** need to update

        Dim currentItem As SelectedItemDetail = New SelectedItemDetail

        With currentItem

            .ExperienceNumber = expNum  'this will be different if xFer Table
            If oldRow("OrderNumber") Is DBNull.Value Then
                .OrderNumber = Nothing
            Else
                .OrderNumber = oldRow("OrderNumber")
            End If
            If oldRow("MenuID") Is DBNull.Value Then
                .MenuID = currentTable.CurrentMenu
            Else
                .MenuID = oldRow("MenuID")
            End If
            .ShiftID = oldRow("ShiftID")
            .EmployeeID = empID

            If Not checkNum = Nothing Then
                .Check = checkNum
            Else
                .Check = oldRow("CheckNumber")
            End If
            .Customer = oldRow("CustomerNumber")
            .Course = oldRow("CourseNumber")

            If isTransferedItem = True Then
                .SIN = oldRow("sin") + 8000000  'currentTable.SIN
                .SII = oldRow("sii") + 8000000  'currentTable.ReferenceSIN
                .si2 = oldRow("si2")
            Else
                If oldRow("sin") < 8000000 Then
                    .SIN = currentTable.SIN     ' oldRow("sin") +
                    .SII = currentTable.ReferenceSIN    'oldRow("sii") +
                    .si2 = oldRow("si2")
                Else
                    .SIN = oldRow("sin") + 10000    'add smaller amount so no duplicates of transfered items
                    .SII = oldRow("sii") + 10000
                    .si2 = oldRow("si2")
                End If
            End If
            '         If oldRow("sin") < 1000000 And isTransferedItem = True Then
            '         .SIN = oldRow("sin") + 9000000  'currentTable.SIN
            '         .SII = oldRow("sii") + 9000000  'currentTable.ReferenceSIN
            '         .si2 = oldRow("si2")
            '        Else
            '             .SIN = oldRow("sin") + 1000000
            '            .SII = oldRow("sii") + 1000000
            '            .si2 = oldRow("si2")
            ''        End If

            .Quantity = oldRow("Quantity")
            .ID = oldRow("ItemID")
            .ItemStatus = oldRow("ItemStatus")
            .Name = oldRow("ItemName")
            .TerminalName = oldRow("TerminalName")
            .ChitName = oldRow("ChitName")
            .ItemPrice = oldRow("ItemPrice")
            .Price = oldRow("Price")
            .TaxPrice = oldRow("TaxPrice")
            .SinTax = oldRow("SinTax")
            .TaxID = oldRow("TaxID")
            'these may be DBNull... keep these catches
            If oldRow("ForceFreeID") Is DBNull.Value Then
                .ForceFreeID = Nothing
            Else
                .ForceFreeID = oldRow("ForceFreeID")
            End If
            If oldRow("ForceFreeAuth") Is DBNull.Value Then
                .ForceFreeAuth = Nothing
            Else
                .ForceFreeAuth = oldRow("ForceFreeAuth")
            End If
            If oldRow("ForceFreeCode") Is DBNull.Value Then
                .ForceFreeCode = Nothing
            Else
                .ForceFreeCode = oldRow("ForceFreeCode")
            End If
            .FunctionID = oldRow("FunctionID")
            .FunctionGroup = oldRow("FunctionGroupID")
            .FunctionFlag = oldRow("FunctionFlag")
            .Category = oldRow("CategoryID")
            .FoodID = oldRow("FoodID")
            .DrinkCategoryID = oldRow("DrinkCategoryID")
            .DrinkID = oldRow("DrinkID")
            .RoutingID = oldRow("RoutingID")
            .PrintPriorityID = oldRow("PrintPriorityID")
            .TerminalID = oldRow("TerminalID")

            currentTable.SIN += 1

        End With

        PopulateDataRowForOpenOrderFromTransfer(currentItem)

    End Function

    Friend Sub SaveOpenOrderData()

        currentTable.ItemsOnHold = CountItemsOnHold()

        Dim newvalueAcct As String
        Dim newvalueExpDate As String
        Dim oRow As DataRow

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                '   If oRow("PaymentTypeID") > 1 Then
                If oRow("PaymentFlag") = "cc" Or oRow("PaymentFlag") = "Gift" Or oRow("PaymentFlag") = "Issue" Then

                    If Not oRow("AccountNumber").Substring(0, 4) = "xxxx" And Not oRow("AccountNumber") = "Manual" Then
                        If oRow("AccountNumber").ToString.Length < 20 Then
                            newvalueAcct = CryOutloud.Encrypt(oRow("AccountNumber"), "test")
                            If newvalueAcct.Length > 50 Then
                                'this will create an incorrect account number
                                MsgBox("Account Number will be truncated")
                                newvalueAcct = newvalueAcct.Substring(0, 50)
                            End If
                            oRow("AccountNumber") = newvalueAcct
                        End If


                        'can't encrypt exp date b/c database only holds 4 chars
                        '     newvalueExpDate = CryOutloud.Encrypt(oRow("CCExpiration"), "test")
                        '    oRow("CCExpiration") = newvalueExpDate

                        '     MsgBox(oRow("AccountNumber"))
                        '    MsgBox(newvalueAcct.Length)

                        '        Dim newvalue2 As String
                        '       newvalue2 = CryOutloud.Decrypt(newvalue, "test")
                        '      MsgBox(newvalue2)

                    End If
                End If
            End If
        Next

        If typeProgram = "Online_Demo" Then

            dsOrderDemo.Merge(dsOrder.Tables("OpenOrders"), False, MissingSchemaAction.Add)
            dsOrderDemo.Merge(dsOrder.Tables("PaymentsAndCredits"), False, MissingSchemaAction.Add)
            dsOrderDemo.Merge(dsOrder.Tables("OrderDetail"), False, MissingSchemaAction.Add)
            '  dsOrderDemo.Merge(dsOrder.Tables("CurrentlyHeld"), False, MissingSchemaAction.Add)

            If currentTerminal.TermMethod = "Quick" Or currentTable.TicketNumber > 0 Then   ' = -888 Then
                dsOrderDemo.Merge(dsOrder.Tables("QuickTickets"), False, MissingSchemaAction.Add)
            ElseIf currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
                If currentTable.IsTabNotTable = False Then
                    dsOrderDemo.Merge(dsOrder.Tables("AvailTables"), False, MissingSchemaAction.Add)
                Else
                    dsOrderDemo.Merge(dsOrder.Tables("AvailTabs"), False, MissingSchemaAction.Add)
                End If
            End If
            Exit Sub
        End If


        '      MergeNewOpenOrdersToTerminalBackup()
        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlDataAdapterOpenOrdersSP.Update(dsOrder, "OpenOrders")
            sql.SqlDataAdapterPayments.Update(dsOrder, "PaymentsAndCredits")
            '            sql.SqlDataAdapterESC.Update(dsOrder, "StatusChange")
            sql.SqlDataAdapterOrderDetail.Update(dsOrder, "OrderDetail")    'only for delivered status
            SaveAvailTabsAndTables()
            UpdateCurrentlyHeldOnRelease(Nothing, currentTable.ExperienceNumber, currentTable.ItemsOnHold)
            '         sql.SqlDataAdapterCurrentlyHeld.Update(dsOrder, "CurrentlyHeld")
            sql.cn.Close()

            dsOrder.AcceptChanges() ' need this so we don't repopulate Terminal Data with previous changes
            'we clear datasets when new table is populated
            '   keeping full in case we write code to pul up order right after we close 
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
            Try
                dsOrder.Tables("PaymentsAndCredits").WriteXml("c:\Data Files\spiderPOS\PaymentError'" & currentTable.TruncatedExpNum & "'.xml", XmlWriteMode.WriteSchema)
            Catch ex2 As Exception
            End Try
            ServerJustWentDown()
            '              MarkBackupAsNOTRecorded()
        End Try


    End Sub


    Friend Sub SaveOpenOrderDataExceptQuick()

        '       currentTable.ItemsOnHold = CountItemsOnHold()

        dsOrderDemo.Merge(dsOrder.Tables("OpenOrders"), False, MissingSchemaAction.Add)
        dsOrderDemo.Merge(dsOrder.Tables("PaymentsAndCredits"), False, MissingSchemaAction.Add)
        dsOrderDemo.Merge(dsOrder.Tables("OrderDetail"), False, MissingSchemaAction.Add)
        '   dsOrderDemo.Merge(dsOrder.Tables("CurrentlyHeld"), False, MissingSchemaAction.Add)

    End Sub




    Private Function CountItemsOnHold()
        Dim itemsHoldCount As Integer = 0
        Dim oRow As DataRow

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("sin") = oRow("sii") Then
                    If oRow("ItemStatus") = 1 Then
                        itemsHoldCount += 1
                    End If
                End If
            End If
        Next
        Return itemsHoldCount

    End Function

    Friend Sub ReleaseWithoutSaving()


        If typeProgram = "Online_Demo" Then

            dsOrder.Tables("OpenOrders").RejectChanges()
            dsOrder.Tables("PaymentsAndCredits").RejectChanges()
            dsOrder.Tables("OrderDetail").RejectChanges()

            dsOrderDemo.Merge(dsOrder.Tables("OpenOrders"), False, MissingSchemaAction.Add)
            dsOrderDemo.Merge(dsOrder.Tables("PaymentsAndCredits"), False, MissingSchemaAction.Add)
            dsOrderDemo.Merge(dsOrder.Tables("OrderDetail"), False, MissingSchemaAction.Add)
            '   dsOrderDemo.Merge(dsOrder.Tables("CurrentlyHeld"), False, MissingSchemaAction.Add)

            Exit Sub
        End If

        Try
            If Not currentTable Is Nothing Then
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                UpdateCurrentlyHeld(Nothing, currentTable.ExperienceNumber)
                sql.cn.Close()
            End If
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
            ServerJustWentDown()
        End Try

    End Sub

    Friend Sub SaveOrderDetailData222()

        '      MergeNewOpenOrdersToTerminalBackup()

        If mainServerConnected = True Then

            Try
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                sql.SqlDataAdapterOrderDetail.Update(dsOrder, "OrderDetail")
                sql.cn.Close()

            Catch ex As Exception
                CloseConnection()
                MsgBox(ex.Message)

                ServerJustWentDown()
                '              MarkBackupAsNOTRecorded()
            End Try
        Else
            '        MarkBackupAsNOTRecorded()
        End If

        '   dsOrder.AcceptChanges() ' need this so we don't repopulate Terminal Data with previous changes

    End Sub

    Friend Function PopulateMenu(ByVal fromStart As Boolean) 'fromStart may be temp
        Dim currentMenu As Menu
        Dim secondaryMenu As Menu
        Dim info As DataSet_Builder.Information_UC
        Dim isDataBaseConnected As Boolean

        Try
            TempConnectToPhoenix()

            '           isDataBaseConnected = CheckingDatabaseConection()
            '          If isDataBaseConnected = False Then
            'ConnectBackFromTempDatabase()
            '     MsgBox("Connection to Phoenix Down")
            '        Return False
            '       Exit Function
            '      Else
            '     mainCategoryIDArrayList.Clear()
            '    ds.Clear()
            '   End If

            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            If tablesFilled = False Then
                mainCategoryIDArrayList.Clear()
                ds.Clear() 'not sure if we want to clear, this may hurt table structure
                PopulateLocationOpening(True)
                PopulateMenuSupport()
                PopulateNONOrderTables()
                PopulateTerminalData()
                PopulateEmployeeData()
                PopulateAllEmployeeColloection()
                If fromStart = True Then
                    CreateTerminals()
                End If

            End If

            If fromStart = False Then
                currentMenu = New Menu(currentTerminal.primaryMenuID, True)
                If currentTerminal.secondaryMenuID > 0 Then
                    secondaryMenu = New Menu(currentTerminal.secondaryMenuID, False)
                End If
                currentTerminal.DailyDate = Now
            End If

            sql.cn.Close()
            ConnectBackFromTempDatabase()

            '       If fromStart = True Then
            '    CreateTerminals()
            '   End If

            Return True

        Catch ex As Exception
            'nned to reload the primary menu from XML
            CloseConnection()
            MsgBox(ex.Message & " Error in Populating Menu")
            ConnectBackFromTempDatabase()
            Return False

        End Try

    End Function

    Friend Sub ReleaseCurrentlyHeld()

        If typeProgram = "Online_Demo" Then
            Exit Sub
        End If

        Try
            If dsOrder.Tables("CurrentlyHeld").Rows.Count = 1 Then
                If Not dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld") Is DBNull.Value Then
                    dsOrder.Tables("CurrentlyHeld").Rows(0)("CurrentlyHeld") = DBNull.Value
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub MarkBackupAsNOTRecorded222()
        Dim oRow As DataRow
        Dim bRow As DataRow
        Dim terminalData As New DataView
        Dim ri As Integer

        With terminalData
            .Table = dsBackup.Tables("OpenOrdersTerminal")          'dtOpenOrdersTerminal '
            .RowFilter = "ExperienceNumber = '" & currentTable.ExperienceNumber & "'"
            .Sort = "sin"
        End With

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            '   **** not sure about the catch below
            If oRow.RowState = DataRowState.Added Then
                ri = terminalData.Find(oRow("sin"))
                If Not ri = -1 Then
                    terminalData(ri)("dbUP") = 0
                End If
            ElseIf oRow.RowState = DataRowState.Modified Then
                If oRow("dbUP") = 1 Then
                    ri = terminalData.Find(oRow("sin"))
                    If Not ri = -1 Then
                        terminalData(ri)("dbUP") = 2
                    End If
                End If
            End If
        Next

    End Sub

    Friend Sub SaveESCStatusChangeData222(ByVal status As Integer, ByVal orderNumber As Int64, ByVal isMainCourse As Boolean, ByVal avgDollar As Decimal)

        '      Try
        '      sql.cn.Open()
        '           sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        '     sql.SqlDataAdapterESC.Update(dsOrder.Tables("StatusChange"))
        '     sql.cn.Close()
        ''     Catch ex As Exception'
        '        CloseConnection() '
        '       End Try

    End Sub

    Friend Sub TerminalAddStatusChangeData222(ByVal status As Integer, ByVal orderNumber As Integer, ByVal isMainCourse As Boolean, ByVal avgDollar As Decimal)
        '*************************
        '   adding directly after we save, so if down we will know

        Dim bRow As DataRow = dsBackup.Tables("ESCTerminal").NewRow

        bRow("CompanyID") = companyInfo.CompanyID
        bRow("LocationID") = companyInfo.LocationID
        bRow("DailyCode") = currentTerminal.CurrentDailyCode
        '        bRow("ExperienceStatusChangeID") = 
        bRow("ExperienceNumber") = currentTable.ExperienceNumber
        bRow("StatusTime") = Now
        bRow("TableStatusID") = status
        If orderNumber = 0 Then
            bRow("OrderNumber") = -1          'will auto fill when db comes back up
        Else
            bRow("OrderNumber") = orderNumber
        End If
        bRow("IsMainCourse") = isMainCourse
        bRow("AverageDollar") = avgDollar
        bRow("TerminalID") = currentTerminal.TermID
        If mainServerConnected = False Then
            bRow("dbUP") = 0
        Else
            bRow("dbUP") = 1
        End If

        dsBackup.Tables("ESCTerminal").Rows.Add(bRow)

    End Sub

    Friend Sub CloseConnection()
        If sql.cn.State = ConnectionState.Open Then
            sql.cn.Close()
        End If
        connectionDown = True

    End Sub


    Friend Sub CompThisItem(ByVal iPromo As ItemPromoInfo)

        Dim currentItem As SelectedItemDetail = New SelectedItemDetail
        With currentItem
            .SIN = currentTable.SIN
            .SII = iPromo.sii   'oRow("sii")
            .si2 = iPromo.si2   'oRow("si2")
            .Name = iPromo.PromoName
            .TerminalName = iPromo.PromoName
            .ChitName = iPromo.PromoName

            If iPromo.OrderNumber = Nothing Then
                .OrderNumber = Nothing
            Else
                .OrderNumber = iPromo.OrderNumber
            End If
            .Check = iPromo.CheckNumber
            .Customer = iPromo.CustomerNumber
            .Course = iPromo.CourseNumber



            'the next 3 are the DISCOUNT
            '    the reason they are lifted directly is its COMP'd
            If iPromo.Quantity = Nothing Then
                .Quantity = 0
            Else
                .Quantity = iPromo.Quantity
            End If
            If iPromo.InvMultiplier = Nothing Then
                .InvMultiplier = 1
            Else
                .InvMultiplier = iPromo.InvMultiplier
            End If
            If iPromo.ItemID = Nothing Then
                .ID = -1
            Else
                .ID = iPromo.ItemID
            End If
            .ItemPrice = Format(iPromo.ItemPrice, "##,###.00")
            .TaxID = iPromo.taxID
            .Price = Format((iPromo.Price * -1), "##,###.00")
            .TaxPrice = iPromo.TaxPrice * -1
            .SinTax = iPromo.SinTax * -1

            .FunctionID = iPromo.FunctionID
            .FunctionGroup = iPromo.FunctionGroup
            .FunctionFlag = iPromo.FunctionFlag

            If .FunctionFlag = "F" Or .FunctionFlag = "O" Or .FunctionFlag = "M" Then
                .Category = iPromo.CategoryID
                .FoodID = iPromo.FoodID
            ElseIf .FunctionFlag = "D" Then
                .Category = iPromo.DrinkCategoryID
                .FoodID = iPromo.DrinkID
            ElseIf .FunctionFlag = "P" Then
                .Category = 0 'iPromo.DrinkCategoryID
                .FoodID = 0 'iPromo.DrinkID
            End If
            'we do not use DrinkCatID, we use general ID 
            ' then go by Function
            '     .DrinkCategoryID = iPromo.DrinkCategoryID
            '    .DrinkID = iPromo.DrinkID

            .RoutingID = iPromo.RoutingID
            .PrintPriorityID = iPromo.PrintPriorityID

            .ForceFreeID = iPromo.PromoCode
            .ForceFreeAuth = iPromo.empID
            If iPromo.PromoReason > 0 Then
                'temp coding voids as mistakes
                .ForceFreeCode = iPromo.PromoReason
            Else
                .ForceFreeCode = 9
            End If
            .ItemStatus = iPromo.ItemStatus
        End With
        currentTable.SIN += 1
        newItemCollection.Add(currentItem)


        '    Dim ffInfo As ForceFreeInfo

        '      ffInfo = New ForceFreeInfo
        '      ffInfo.DailyCode = currentTerminal.CurrentDailyCode
        '      ffInfo.ExpNum = currentTable.ExperienceNumber
        '     ffInfo.OpenOrderID = iPromo.openOrderID 'oRow("OpenOrderID")
        '     ffInfo.AuthID = iPromo.empID
        ''     ffInfo.Price = 0
        '     ffInfo.TaxID = iPromo.taxID ' oRow("TaxID")
        '    ffInfo.TaxPrice = 0
        ''    ffInfo.CompID = iPromo.PromoCode   'can letter choose from list of reasons
        '   ffInfo.CompPrice = iPromo.Price
        ''    ffInfo.AmountDiscount = iPromo.Price
        '    ffInfo.TaxDicount = iPromo.TaxPrice + iPromo.SinTax
        '    ffInfo.ffID = GenerateOrderTables.CreateNewOrderForceFree(ffInfo)

    End Sub

    Friend Sub PopulateDataRowForOpenOrder(ByRef newItem As SelectedItemDetail)

        Try
            Dim oRow As DataRow = dsOrder.Tables("OpenOrders").NewRow

            PerformOpenOrdersAdd(newItem, oRow)
            If typeProgram = "Online_Demo" Then
                oRow("OpenOrderID") = demoOpenOrdersID
                demoOpenOrdersID += 1
            End If
            dsOrder.Tables("OpenOrders").Rows.Add(oRow)
            If currentTable.EmptyCustPanel = currentTable.CustomerNumber Then
                currentTable.EmptyCustPanel = 0
            End If


            '         Dim bRow As DataRow = dsBackup.Tables("OpenOrdersTerminal").NewRow
            '        PerformOpenOrdersAdd(newItem, bRow)
            '       dsBackup.Tables("OpenOrdersTerminal").Rows.Add(bRow)

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        '   trying to adjust OpenOrderCurrencuyMan to reflect just ordered main item
        '      If newItem.SIN = newItem.SII Then
        '    OpenOrdersCurrencyMan.Position = dsOrder.Tables("OpenOrders").R
        '          Me.testgridview.gridViewOrder.ScrollToRow(OpenOrdersCurrencyMan.Position)
        '     End If


    End Sub

    Private Sub PerformOpenOrdersAdd(ByRef newitem As SelectedItemDetail, ByRef dr As DataRow)

        Dim taxID As Integer
        Dim taxPrice As Decimal
        Dim sinTax As Decimal

        If newitem.TaxID = Nothing Or newitem.TaxID = 0 Then
            taxID = DetermineTaxID(newitem.FunctionID)
        Else
            taxID = newitem.TaxID
        End If
        taxPrice = DetermineTaxPrice(taxID, newitem.Price)
        sinTax = DetermineSinTax(taxID, newitem.Price)

        dr("CompanyID") = companyInfo.CompanyID
        dr("LocationID") = companyInfo.LocationID
        dr("DailyCode") = currentTable.DailyCode
        dr("ExperienceNumber") = currentTable.ExperienceNumber
        If newitem.OrderNumber = Nothing Then
            dr("OrderNumber") = DBNull.Value
        Else
            dr("OrderNumber") = newitem.OrderNumber
        End If
        If newitem.MenuID = Nothing Then
            dr("MenuID") = currentTable.CurrentMenu
        Else
            dr("MenuID") = newitem.MenuID
        End If
        If currentTerminal.CurrentShift = Nothing Then 'currentServer.ShiftID = Nothing Then
            dr("ShiftID") = 0
        Else
            dr("ShiftID") = currentTerminal.CurrentShift
        End If

        If newitem.EmployeeID = Nothing Then
            dr("EmployeeID") = currentTable.EmployeeID
        Else
            dr("EmployeeID") = newitem.EmployeeID
        End If
        dr("CheckNumber") = newitem.Check
        dr("CustomerNumber") = newitem.Customer
        dr("CourseNumber") = newitem.Course
        dr("sin") = newitem.SIN
        dr("sii") = newitem.SII
        dr("si2") = newitem.si2
        If newitem.Quantity = Nothing Then
            dr("Quantity") = 1
        Else
            dr("Quantity") = newitem.Quantity
        End If
        If newitem.Voiding = True Then
            dr("ItemID") = -9
            dr("itemStatus") = 9           '*** changed from -9
        Else
            dr("ItemID") = newitem.ID
            If newitem.ItemStatus > 0 Then
                dr("itemStatus") = newitem.ItemStatus
            Else
                dr("itemStatus") = 0
            End If
        End If
        dr("ItemName") = newitem.Name
        dr("TerminalName") = newitem.TerminalName
        dr("ChitName") = newitem.ChitName

        If newitem.ItemPrice = Nothing Then
            dr("ItemPrice") = Format(newitem.Price, "###,##0.00")
        Else
            dr("ItemPrice") = Format(newitem.ItemPrice, "###,##0.00")
        End If
        dr("Price") = Format(newitem.Price, "###,##0.00")
        If newitem.TaxPrice = Nothing Then
            dr("TaxPrice") = taxPrice
        Else
            dr("TaxPrice") = newitem.TaxPrice
        End If
        If newitem.SinTax = Nothing Then
            dr("SinTax") = sinTax
        Else
            dr("SinTax") = newitem.SinTax
        End If
        dr("TaxID") = taxID       'newItem.TaxID
        If Not newitem.ForceFreeID = Nothing Then
            dr("ForceFreeID") = newitem.ForceFreeID
        Else
            dr("ForceFreeID") = 0
        End If
        If Not newitem.ForceFreeAuth = Nothing Then
            dr("ForceFreeAuth") = newitem.ForceFreeAuth
        Else
            dr("ForceFreeAuth") = 0
        End If
        If Not newitem.ForceFreeCode = Nothing Then
            dr("ForceFreeCode") = newitem.ForceFreeCode
        Else
            dr("ForceFreeCode") = 0
        End If
        If newitem.FunctionFlag = "F" Or newitem.FunctionFlag = "O" Or newitem.FunctionFlag = "M" Then  'newitem.FunctionID = 1 Or newitem.FunctionID = 2 Or newitem.FunctionID = 3 Then
            dr("CategoryID") = newitem.Category
            dr("FoodID") = newitem.ID
            dr("DrinkID") = 0
            dr("DrinkCategoryID") = 0
        ElseIf newitem.FunctionFlag = "D" Then 'newitem.FunctionID >= 4 And newitem.FunctionID <= 7 Then
            dr("DrinkCategoryID") = newitem.Category
            If newitem.FunctionGroup = 11 Then
                ' this is for Drink Preps, we record these differntly
                dr("DrinkID") = newitem.DrinkID
            Else
                dr("DrinkID") = newitem.ID
            End If
            dr("FoodID") = 0
            dr("CategoryID") = 0
            '   *** ??? below do we use funFlag ???
        ElseIf newitem.FunctionID = 0 Or newitem.FunctionFlag = "N" Then
            dr("FoodID") = 0
            dr("DrinkID") = 0
            dr("CategoryID") = 0
            dr("DrinkCategoryID") = 0
        End If
        dr("FunctionID") = newitem.FunctionID
        dr("FunctionGroupID") = newitem.FunctionGroup
        dr("FunctionFlag") = newitem.FunctionFlag    '  at this point not keeping Flag
        If newitem.SIN = newitem.SII Then
            dr("RoutingID") = newitem.RoutingID
            currentTable.ReferenceRouting = newitem.RoutingID
        Else
            If newitem.RoutingID = 0 Or newitem.RoutingID = Nothing Then
                dr("RoutingID") = currentTable.ReferenceRouting
            Else
                dr("RoutingID") = newitem.RoutingID
            End If
        End If
        dr("PrintPriorityID") = newitem.PrintPriorityID
        '     dr("BlankValue") = 0
        If newitem.FunctionFlag = "D" Then 'newitem.FunctionID >= 4 And newitem.FunctionID <= 7 Then
            dr("Repeat") = 1
        Else
            dr("Repeat") = 0
        End If
        dr("TerminalID") = currentTerminal.TermPrimaryKey
        dr("dbUP") = 1

        'this is InvMultiplier
        dr("OpenDecimal1") = newitem.InvMultiplier

    End Sub

    Friend Sub PopulateDataRowForOpenOrderFromTransfer(ByRef newItem As SelectedItemDetail)


        Try
            Dim oRow As DataRow = dsOrder.Tables("OpenOrders").NewRow
            PerformOpenOrdersAddFromTransfer(newItem, oRow)
            If typeProgram = "Online_Demo" Then
                oRow("OpenOrderID") = demoOpenOrdersID
                demoOpenOrdersID += 1
            End If
            dsOrder.Tables("OpenOrders").Rows.Add(oRow)
            If currentTable.EmptyCustPanel = currentTable.CustomerNumber Then
                currentTable.EmptyCustPanel = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub PerformOpenOrdersAddFromTransfer(ByRef newitem As SelectedItemDetail, ByRef dr As DataRow)

        dr("CompanyID") = companyInfo.CompanyID
        dr("LocationID") = companyInfo.LocationID
        dr("DailyCode") = currentTable.DailyCode
        dr("ExperienceNumber") = newitem.ExperienceNumber
        If newitem.OrderNumber = Nothing Then
            dr("OrderNumber") = DBNull.Value
        Else
            dr("OrderNumber") = newitem.OrderNumber
        End If
        dr("MenuID") = newitem.MenuID
        dr("ShiftID") = newitem.ShiftID
        dr("EmployeeID") = newitem.EmployeeID
        dr("CheckNumber") = newitem.Check
        dr("CustomerNumber") = newitem.Customer
        dr("CourseNumber") = newitem.Course
        dr("sin") = newitem.SIN
        dr("si2") = newitem.si2
        dr("sii") = newitem.SII
        dr("Quantity") = newitem.Quantity
        dr("ItemID") = newitem.ID
        dr("itemStatus") = newitem.ItemStatus
        dr("ItemName") = newitem.Name
        dr("TerminalName") = newitem.TerminalName
        dr("ChitName") = newitem.ChitName
        dr("ItemPrice") = Format(newitem.Price, "###,##0.00")
        dr("Price") = Format(newitem.Price, "###,##0.00")
        dr("TaxPrice") = newitem.TaxPrice
        dr("SinTax") = newitem.SinTax
        dr("TaxID") = newitem.TaxID
        dr("ForceFreeID") = newitem.ForceFreeID
        dr("ForceFreeAuth") = newitem.ForceFreeAuth
        dr("ForceFreeCode") = newitem.ForceFreeCode
        dr("CategoryID") = newitem.Category
        dr("FoodID") = newitem.FoodID
        dr("DrinkCategoryID") = newitem.DrinkCategoryID
        dr("DrinkID") = newitem.DrinkID
        dr("FunctionID") = newitem.FunctionID
        '                             flag and group are TableJoins
        dr("FunctionGroupID") = newitem.FunctionGroup
        dr("FunctionFlag") = newitem.FunctionFlag    '  at this point not keeping Flag
        dr("RoutingID") = newitem.RoutingID     'routing different in reg perform OO add
        dr("PrintPriorityID") = newitem.PrintPriorityID
        '     dr("BlankValue") = 0
        dr("Repeat") = 0        'always for transfers or voids
        dr("TerminalID") = currentTerminal.TermPrimaryKey
        dr("dbUP") = 1

    End Sub

    Private Sub MergeNewOpenOrdersToTerminalBackup222()
        Dim bRow As DataRow
        Dim oRow As DataRow
        Dim pRow As DataRow
        Dim terminalData As New DataView
        Dim ri As Integer
        Dim modifiedItem As SelectedItemDetail

        '   we do this every time we SaveOpenOrderData
        '   ***
        '    If dsBackup.Tables("OpenOrdersTerminal").Rows.Count > 0 Then
        With terminalData
            .Table = dsBackup.Tables("OpenOrdersTerminal")      'dtOpenOrdersTerminal       ' 
            .RowFilter = "ExperienceNumber = '" & currentTable.ExperienceNumber & "'"
            .Sort = "sin"
        End With
        '   End If

        For Each oRow In dsOrder.Tables("OpenOrders").Rows

            If oRow.RowState = DataRowState.Added Then
                bRow = dsBackup.Tables("OpenOrdersTerminal").NewRow
                CopyOneRowToAnotherOpenOrders222(oRow, bRow)
                dsBackup.Tables("OpenOrdersTerminal").Rows.Add(bRow)

            ElseIf oRow.RowState = DataRowState.Modified Then
                ri = terminalData.Find(oRow("sin"))
                If Not ri = -1 Then
                    CopyOneRowToViewOpenOrders222(oRow, terminalData(ri))
                End If

            ElseIf oRow.RowState = DataRowState.Deleted Then
                '   we are deleting when we delete regular oRow in  DeleteOpenOrdersRow
                '             ri = terminalData.Find(oRow("sin"))
                '            If Not ri = -1 Then
                '           terminalData.Delete(ri)
                '      End If

            ElseIf oRow.RowState = DataRowState.Detached Then
                '   do nothing .. row was never added
                '   this is when we added a row but then decided to delete
            End If
        Next
    End Sub

    Friend Function CopyOneRowToAnotherExpStatusChange(ByVal oldRow As DataRow, ByRef newRow As DataRow)

        newRow("CompanyID") = oldRow("CompanyID")
        newRow("LocationID") = oldRow("LocationID")
        newRow("DailyCode") = oldRow("DailyCode")
        newRow("ExperienceStatusChangeID") = oldRow("ExperienceStatusChangeID")
        newRow("ExperienceNumber") = oldRow("ExperienceNumber")
        newRow("StatusTime") = oldRow("StatusTime")
        If Not oldRow("TableStatusID") Is DBNull.Value Then
            newRow("TableStatusID") = oldRow("TableStatusID")
        Else
            newRow("TableStatusID") = 0
        End If
        newRow("OrderNumber") = oldRow("OrderNumber")
        newRow("IsMainCourse") = oldRow("IsMainCourse")
        newRow("AverageDollar") = oldRow("AverageDollar")
        newRow("TerminalID") = oldRow("TerminalID")
        newRow("dbUP") = oldRow("dbUP")

        Return newRow

    End Function



    Friend Function CopyOneRowToAnotherAvailTabsAndTables222(ByVal oldRow As DataRow, ByRef newRow As DataRow)

        newRow("ExperienceNumber") = oldRow("ExperienceNumber")
        newRow("DailyCode") = oldRow("DailyCode")
        newRow("CompanyID") = oldRow("CompanyID")
        newRow("LocationID") = oldRow("LocationID")
        newRow("ExperienceDate") = oldRow("ExperienceDate")
        If Not oldRow("TableNumber") Is DBNull.Value Then
            '   Availtabs does not have TableNumber column 
            newRow("TableNumber") = oldRow("TableNumber")
        End If
        newRow("TabID") = oldRow("TabID")
        newRow("TabName") = oldRow("TabName")
        newRow("EmployeeID") = oldRow("EmployeeID")
        newRow("NumberOfCustomers") = oldRow("NumberOfCustomers")
        newRow("NumberOfChecks") = oldRow("NumberOfChecks")
        newRow("ShiftID") = oldRow("ShiftID")                     '***********  ???????? are we using
        '   we use ShiftID in closed tables only right now... we need to add in Avail, open
        newRow("MenuID") = oldRow("MenuID")
        newRow("LastStatus") = oldRow("LastStatus")
        newRow("LastStatusTime") = oldRow("LastStatusTime")
        newRow("ClosedSubTotal") = oldRow("ClosedSubTotal")
        newRow("TerminalID") = oldRow("TerminalID")
        newRow("TicketNumber") = oldRow("TicketNumber")
        newRow("dbUP") = oldRow("dbUP")

        Return newRow

    End Function

    Friend Sub CopyOneRowToAnotherOpenOrders222(ByVal oldRow As DataRow, ByRef newRow As DataRow)
        Exit Sub
        '222
        '**********************
        'this is bad we should never do this
        'we would be resetting OpenOrderID IDENTITY

        newRow("CompanyID") = oldRow("CompanyID")
        newRow("LocationID") = oldRow("LocationID")
        newRow("OpenOrderID") = oldRow("OpenOrderID")
        newRow("DailyCode") = oldRow("DailyCode")
        newRow("ExperienceNumber") = oldRow("ExperienceNumber")
        newRow("OrderNumber") = oldRow("OrderNumber")
        newRow("ShiftID") = oldRow("ShiftID")
        newRow("MenuID") = oldRow("MenuID")
        newRow("EmployeeID") = oldRow("EmployeeID")
        '       newRow("TableNumber") = oldRow("TableNumber")
        '      newRow("TabID") = oldRow("TabID")
        '     newRow("TabName") = oldRow("TabName")
        newRow("CheckNumber") = oldRow("CheckNumber")
        newRow("CustomerNumber") = oldRow("CustomerNumber")
        newRow("CourseNumber") = oldRow("CourseNumber")
        newRow("sin") = oldRow("sin")
        newRow("sii") = oldRow("sii")
        newRow("Quantity") = oldRow("Quantity")
        newRow("ItemID") = oldRow("ItemID")
        newRow("ItemName") = oldRow("ItemName")
        newRow("TerminalName") = oldRow("TerminalName")
        newRow("ChitName") = oldRow("ChitName")
        newRow("ItemPrice") = oldRow("ItemPrice")
        newRow("Price") = oldRow("Price")
        newRow("TaxPrice") = oldRow("TaxPrice")
        'sin tax
        newRow("TaxID") = oldRow("TaxID")
        newRow("ForceFreeID") = oldRow("ForceFreeID")
        newRow("ForceFreeAuth") = oldRow("ForceFreeAuth")
        newRow("ForceFreeCode") = oldRow("ForceFreeCode")
        newRow("FunctionID") = oldRow("FunctionID")
        newRow("FunctionGroupID") = oldRow("FunctionGroupID")
        newRow("FunctionFlag") = oldRow("FunctionFlag")
        newRow("CategoryID") = oldRow("CategoryID")
        newRow("FoodID") = oldRow("FoodID")
        newRow("DrinkCategoryID") = oldRow("DrinkCategoryID")
        newRow("DrinkID") = oldRow("DrinkID")
        newRow("itemStatus") = oldRow("itemStatus")
        newRow("RoutingID") = oldRow("RoutingID")
        newRow("PrintPriorityID") = oldRow("PrintPriorityID")
        newRow("Repeat") = oldRow("Repeat")
        newRow("TerminalID") = oldRow("TerminalID")
        newRow("dbUP") = oldRow("dbUP")


    End Sub

    Friend Sub CopyOneRowToAnotherPaymentsAndCredits(ByVal oldRow As DataRow, ByRef newRow As DataRow, ByVal dbUP As Integer)


        newRow("PaymentsAndCreditsID") = oldRow("PaymentsAndCreditsID")

        newRow("CompanyID") = oldRow("CompanyID")
        newRow("LocationID") = oldRow("LocationID")
        newRow("DailyCode") = oldRow("DailyCode")
        newRow("ExperienceNumber") = oldRow("ExperienceNumber")
        newRow("EmployeeID") = oldRow("EmployeeID")
        newRow("CheckNumber") = oldRow("CheckNumber")
        newRow("PaymentTypeID") = oldRow("PaymentTypeID")
        newRow("PaymentTypeName") = oldRow("PaymentTypeName")
        newRow("PaymentFlag") = oldRow("PaymentFlag")
        newRow("AccountNumber") = oldRow("AccountNumber")
        newRow("CCExpiration") = oldRow("CCExpiration")
        newRow("Track2") = oldRow("Track2")
        newRow("CustomerName") = oldRow("CustomerName")
        newRow("TransactionType") = oldRow("TransactionType")
        newRow("TransactionCode") = oldRow("TransactionCode")
        newRow("SwipeType") = oldRow("SwipeType")
        newRow("PaymentAmount") = oldRow("PaymentAmount")
        newRow("Tip") = oldRow("Tip")
        newRow("PreAuthAmount") = oldRow("PreAuthAmount")
        newRow("Applied") = oldRow("Applied")
        newRow("RefNum") = oldRow("RefNum")
        newRow("AuthCode") = oldRow("AuthCode")
        newRow("AcqRefData") = oldRow("AcqRefData")
        newRow("TerminalID") = oldRow("TerminalID")
        If dbUP = -1 Then
            '   when we are pop from terminal data
            newRow("dbUP") = oldRow("dbUP")
        Else
            '   when we are pop to terminal data
            newRow("dbUP") = dbUP
        End If

    End Sub


    Private Sub CopyOneRowToViewOpenOrders222(ByVal oldRow As DataRow, ByRef newRow As DataRowView)
        Exit Sub
        '     newRow("OpenOrderID") = oldRow("OpenOrderID")
        newRow("CompanyID") = oldRow("CompanyID")
        newRow("LocationID") = oldRow("LocationID")
        newRow("DailyCode") = oldRow("DailyCode")
        newRow("ExperienceNumber") = oldRow("ExperienceNumber")
        newRow("OrderNumber") = oldRow("OrderNumber")
        newRow("ShiftID") = oldRow("ShiftID")
        newRow("MenuID") = oldRow("MenuID")
        newRow("EmployeeID") = oldRow("EmployeeID")
        '        newRow("TableNumber") = oldRow("TableNumber")
        '       newRow("TabID") = oldRow("TabID")
        '      newRow("TabName") = oldRow("TabName")
        newRow("CheckNumber") = oldRow("CheckNumber")
        newRow("CustomerNumber") = oldRow("CustomerNumber")
        newRow("CourseNumber") = oldRow("CourseNumber")
        newRow("sin") = oldRow("sin")
        newRow("sii") = oldRow("sii")
        newRow("Quantity") = oldRow("Quantity")
        newRow("ItemID") = oldRow("ItemID")
        newRow("ItemName") = oldRow("ItemName")
        newRow("Price") = oldRow("Price")
        newRow("TaxPrice") = oldRow("TaxPrice")
        'sin tax
        newRow("TaxID") = oldRow("TaxID")
        newRow("ForceFreeID") = oldRow("ForceFreeID")
        newRow("ForceFreeAuth") = oldRow("ForceFreeAuth")
        newRow("ForceFreeCode") = oldRow("ForceFreeCode")
        newRow("FunctionID") = oldRow("FunctionID")
        newRow("FunctionGroupID") = oldRow("FunctionGroupID")
        newRow("FunctionFlag") = oldRow("FunctionFlag")
        newRow("CategoryID") = oldRow("CategoryID")
        newRow("FoodID") = oldRow("FoodID")
        newRow("DrinkCategoryID") = oldRow("DrinkCategoryID")
        newRow("DrinkID") = oldRow("DrinkID")
        newRow("itemStatus") = oldRow("itemStatus")
        newRow("RoutingID") = oldRow("RoutingID")
        newRow("PrintPriorityID") = oldRow("PrintPriorityID")
        newRow("Repeat") = oldRow("Repeat")
        newRow("TerminalID") = oldRow("TerminalID")
        If mainServerConnected = True Then
            newRow("dbUP") = 1
        Else
            newRow("dbUP") = 0
        End If

        '     Return newRow

    End Sub

    Friend Overloads Sub AddItemViewToOpenOrders222(ByRef oRow As DataRowView) ', ByVal empID As Integer, ByVal expNum As Integer)
        Exit Sub

        sql.SqlInsertCommandOpenOrdersSP.Parameters("@CompanyID").Value = oRow("CompanyID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@LocationID").Value = oRow("LocationID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@DailyCode").Value = oRow("DailyCode")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ExperienceNumber").Value = oRow("ExperienceNumber") ' expNum

        sql.SqlInsertCommandOpenOrdersSP.Parameters("@OrderNumber").Value = oRow("OrderNumber")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ShiftID").Value = oRow("ShiftID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@MenuID").Value = oRow("MenuID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@EmployeeID").Value = oRow("EmployeeID")             'empID
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@TableNumber").Value = oRow("TableNumber")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@TabID").Value = oRow("TabID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@TabName").Value = oRow("TabName")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@CheckNumber").Value = oRow("CheckNumber")   '??
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@CustomerNumber").Value = oRow("CustomerNumber")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@CourseNumber").Value = oRow("CourseNumber")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@sin").Value = oRow("sin")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@sii").Value = oRow("sii")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@Quantity").Value = oRow("Quantity")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ItemID").Value = oRow("ItemID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ItemName").Value = oRow("ItemName")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ItemPrice").Value = oRow("ItemPrice")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@Price").Value = oRow("Price")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@TaxPrice").Value = oRow("TaxPrice")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@TaxID").Value = oRow("TaxID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ForceFreeID").Value = oRow("ForceFreeID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ForceFreeAuth").Value = oRow("ForceFreeAuth")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ForceFreeCode").Value = oRow("ForceFreeCode")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@FunctionID").Value = oRow("FunctionID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@FunctionGroupID").Value = oRow("FunctionGroupID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@FunctionFlag").Value = oRow("FunctionFlag")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@CategoryID").Value = oRow("CategoryID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@FoodID").Value = oRow("FoodID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@DrinkCategoryID").Value = oRow("DrinkCategoryID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@DrinkID").Value = oRow("DrinkID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@ItemStatus").Value = oRow("ItemStatus") '9
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@RoutingID").Value = oRow("RoutingID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@PrintPriorityID").Value = oRow("PrintPriorityID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@Repeat").Value = 1  'oRow("Repeat")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@TerminalID").Value = oRow("TerminalID")
        sql.SqlInsertCommandOpenOrdersSP.Parameters("@dbUP").Value = 1

        sql.SqlInsertCommandOpenOrdersSP.ExecuteNonQuery()

    End Sub

    Friend Function AddItemViewToAvailTabsAndTables222(ByRef oRow As DataRowView, ByVal IsTabNotTable As Boolean)

        Dim expNum As Int64

        sql.SqlInsertCommandAvailTablesSP.Parameters("@CompanyID").Value = oRow("CompanyID")
        sql.SqlInsertCommandAvailTablesSP.Parameters("@LocationID").Value = oRow("LocationID")
        sql.SqlInsertCommandAvailTablesSP.Parameters("@DailyCode").Value = oRow("DailyCode")
        '      sql.SqlInsertCommandAvailTablesSP.Parameters("@ExperienceNumber").Value = oRow("ExperienceNumber") ' expNum
        sql.SqlInsertCommandAvailTablesSP.Parameters("@ExperienceDate").Value = oRow("ExperienceDate")
        sql.SqlInsertCommandAvailTablesSP.Parameters("@TableNumber").Value = oRow("TableNumber")
        sql.SqlInsertCommandAvailTablesSP.Parameters("@TabID").Value = oRow("TabID")
        sql.SqlInsertCommandAvailTablesSP.Parameters("@TabName").Value = oRow("TabName")
        sql.SqlInsertCommandAvailTablesSP.Parameters("@EmployeeID").Value = oRow("EmployeeID")             'empID
        sql.SqlInsertCommandAvailTablesSP.Parameters("@NumberOfCustomers").Value = oRow("NumberOfCustomers")
        sql.SqlInsertCommandAvailTablesSP.Parameters("@NumberOfChecks").Value = oRow("NumberOfChecks")   '??
        sql.SqlInsertCommandAvailTablesSP.Parameters("@MenuID").Value = oRow("MenuID")
        sql.SqlInsertCommandAvailTablesSP.Parameters("@LastStatus").Value = oRow("LastStatus") '9
        sql.SqlInsertCommandAvailTablesSP.Parameters("@LastStatusTime").Value = oRow("LastStatusTime")
        sql.SqlInsertCommandAvailTablesSP.Parameters("@ClosedSubTotal").Value = oRow("ClosedSubTotal")
        sql.SqlInsertCommandAvailTablesSP.Parameters("@TerminalID").Value = oRow("TerminalID")
        sql.SqlInsertCommandAvailTablesSP.Parameters("@TicketNumber").Value = oRow("TicketNumber")
        sql.SqlInsertCommandAvailTablesSP.Parameters("@dbUP").Value = 1


        expNum = CType(sql.SqlInsertCommandAvailTablesSP.ExecuteScalar, Int64)

        Return expNum

    End Function

    Friend Sub AddItemViewToESCStatusChange(ByRef oRow As DataRowView)

        sql.SqlInsertCommandESC.Parameters("@CompanyID").Value = oRow("CompanyID")
        sql.SqlInsertCommandESC.Parameters("@LocationID").Value = oRow("LocationID")
        sql.SqlInsertCommandESC.Parameters("@DailyCode").Value = oRow("DailyCode")
        sql.SqlInsertCommandESC.Parameters("@ExperienceNumber").Value = oRow("ExperienceNumber")
        sql.SqlInsertCommandESC.Parameters("@StatusTime").Value = oRow("StatusTime")
        If Not oRow("TableStatusID") Is DBNull.Value Then
            sql.SqlInsertCommandESC.Parameters("@TableStatusID").Value = oRow("TableStatusID")
        Else
            sql.SqlInsertCommandESC.Parameters("@TableStatusID").Value = 0
        End If
        sql.SqlInsertCommandESC.Parameters("@OrderNumber").Value = oRow("OrderNumber")
        sql.SqlInsertCommandESC.Parameters("@IsMainCourse").Value = oRow("IsMainCourse")
        sql.SqlInsertCommandESC.Parameters("@AverageDollar").Value = oRow("AverageDollar")
        sql.SqlInsertCommandESC.Parameters("@TerminalID").Value = oRow("TerminalID")
        sql.SqlInsertCommandESC.Parameters("@dbUP").Value = 1

        sql.SqlInsertCommandESC.ExecuteNonQuery()

    End Sub

    Friend Sub AddItemViewToPaymentsAndCredits(ByRef oRow As DataRowView)

        sql.SqlInsertCommandPayments.Parameters("@CompanyID").Value = oRow("CompanyID")
        sql.SqlInsertCommandPayments.Parameters("@LocationID").Value = oRow("LocationID")
        sql.SqlInsertCommandPayments.Parameters("@DailyCode").Value = oRow("DailyCode")
        sql.SqlInsertCommandPayments.Parameters("@ExperienceNumber").Value = oRow("ExperienceNumber")
        sql.SqlInsertCommandPayments.Parameters("@EmployeeID").Value = oRow("EmployeeID")
        sql.SqlInsertCommandPayments.Parameters("@CheckNumber").Value = oRow("CheckNumber")
        sql.SqlInsertCommandPayments.Parameters("@PaymentTypeID").Value = oRow("PaymentTypeID")
        sql.SqlInsertCommandPayments.Parameters("@AccountNumber").Value = oRow("AccountNumber")
        sql.SqlInsertCommandPayments.Parameters("@CCExpiration").Value = oRow("CCExpiration")
        sql.SqlInsertCommandPayments.Parameters("@Track2").Value = oRow("Track2")
        sql.SqlInsertCommandPayments.Parameters("@CustomerName").Value = oRow("CustomerName")
        sql.SqlInsertCommandPayments.Parameters("@TransactionType").Value = oRow("TransactionType")
        sql.SqlInsertCommandPayments.Parameters("@TransactionCode").Value = oRow("TransactionCode")
        sql.SqlInsertCommandPayments.Parameters("@SwipeType").Value = oRow("SwipeType")
        sql.SqlInsertCommandPayments.Parameters("@PaymentAmount").Value = oRow("PaymentAmount")
        sql.SqlInsertCommandPayments.Parameters("@Tip").Value = oRow("Tip")
        sql.SqlInsertCommandPayments.Parameters("@PreAuthAmount").Value = oRow("PreAuthAmount")
        sql.SqlInsertCommandPayments.Parameters("@Applied").Value = oRow("Applied")
        sql.SqlInsertCommandPayments.Parameters("@RefNum").Value = oRow("RefNum")
        sql.SqlInsertCommandPayments.Parameters("@AuthCode").Value = oRow("AuthCode")
        sql.SqlInsertCommandPayments.Parameters("@AcqRefData").Value = oRow("AcqRefData")
        sql.SqlInsertCommandPayments.Parameters("@TerminalID").Value = oRow("TerminalID")
        sql.SqlInsertCommandPayments.Parameters("@dbUP").Value = 1

        sql.SqlInsertCommandESC.ExecuteNonQuery()

    End Sub

    Friend Sub CreateNewBatch()

    End Sub

    Private Sub CopyOneViewToRowOpenOrders222(ByVal oldRow As DataRowView, ByRef newRow As DataRow)

        newRow("OpenOrderID") = oldRow("OpenOrderID")
        newRow("CompanyID") = oldRow("CompanyID")
        newRow("LocationID") = oldRow("LocationID")
        newRow("DailyCode") = oldRow("DailyCode")
        newRow("ExperienceNumber") = oldRow("ExperienceNumber")
        newRow("OrderNumber") = oldRow("OrderNumber")
        newRow("ShiftID") = oldRow("ShiftID")
        newRow("MenuID") = oldRow("MenuID")
        newRow("EmployeeID") = oldRow("EmployeeID")
        newRow("TableNumber") = oldRow("TableNumber")
        newRow("TabID") = oldRow("TabID")
        newRow("TabName") = oldRow("TabName")
        newRow("CheckNumber") = oldRow("CheckNumber")
        newRow("CustomerNumber") = oldRow("CustomerNumber")
        newRow("CourseNumber") = oldRow("CourseNumber")
        newRow("sin") = oldRow("sin")
        newRow("sii") = oldRow("sii")
        newRow("Quantity") = oldRow("Quantity")
        newRow("ItemID") = oldRow("ItemID")
        newRow("ItemName") = oldRow("ItemName")
        newRow("ItemPrice") = oldRow("ItemPrice")
        newRow("Price") = oldRow("Price")
        newRow("TaxPrice") = oldRow("TaxPrice")
        newRow("TaxID") = oldRow("TaxID")
        newRow("ForceFreeID") = oldRow("ForceFreeID")
        newRow("ForceFreeAuth") = oldRow("ForceFreeAuth")
        newRow("ForceFreeCode") = oldRow("ForceFreeCode")
        newRow("FunctionID") = oldRow("FunctionID")
        newRow("FunctionGroupID") = oldRow("FunctionGroupID")
        newRow("FunctionFlag") = oldRow("FunctionFlag")
        newRow("CategoryID") = oldRow("CategoryID")
        newRow("FoodID") = oldRow("FoodID")
        newRow("DrinkCategoryID") = oldRow("DrinkCategoryID")
        newRow("DrinkID") = oldRow("DrinkID")
        newRow("itemStatus") = oldRow("itemStatus")
        newRow("RoutingID") = oldRow("RoutingID")
        newRow("PrintPriorityID") = oldRow("PrintPriorityID")
        newRow("Repeat") = oldRow("Repeat")
        newRow("TerminalID") = oldRow("TerminalID")
        If mainServerConnected = True Then
            newRow("dbUP") = 1
        Else
            newRow("dbUP") = 0
        End If

        '     Return newRow

    End Sub

    Friend Sub LoadTabIDinExperinceTable()
        Dim oRow As DataRow

        If currentTerminal.TermMethod = "Quick" Or currentTable.TicketNumber > 0 Then  'currentTable.TabID = -888 Then

            For Each oRow In dsOrder.Tables("QuickTickets").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        oRow("TabID") = currentTable.TabID
                        oRow("TabName") = currentTable.TabName
                        oRow("MethodUse") = currentTable.MethodUse
                        oRow("NumberOfCustomers") = currentTable.NumberOfCustomers
                        Exit For
                    End If
                End If
            Next
        ElseIf currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            If currentTable.IsTabNotTable = True Then
                For Each oRow In dsOrder.Tables("AvailTabs").Rows
                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        oRow("TabID") = currentTable.TabID
                        oRow("TabName") = currentTable.TabName
                        oRow("MethodUse") = currentTable.MethodUse
                        oRow("NumberOfCustomers") = currentTable.NumberOfCustomers
                        Exit For
                    End If
                Next
            Else
                For Each oRow In dsOrder.Tables("AvailTables").Rows
                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        oRow("TabID") = currentTable.TabID
                        oRow("TabName") = currentTable.TabName
                        oRow("MethodUse") = currentTable.MethodUse
                        Exit For
                    End If
                Next

            End If
        End If

    End Sub

    Friend Sub ReleaseTableOrTab()
        Dim oRow As DataRow
        Dim bRow As DataRow
        Dim vRow As DataRowView

        If currentTerminal.TermMethod = "Quick" Or currentTable.TicketNumber > 0 Then   ' currentTable.TabID = -888 Then

            For Each oRow In dsOrder.Tables("QuickTickets").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        '   we should add subtotal to show closed
                        oRow("ClosedSubTotal") = currentTable.SubTotal
                        oRow("LastStatusTime") = Now
                        If oRow("AvailForSeating") Is DBNull.Value Then
                            oRow("AvailForSeating") = Now
                        End If
                        oRow("LastStatus") = 10
                        '           oRow("TabName") = "Closed " & currentTable.TableNumber.ToString
                        Exit For
                    End If
                End If
            Next
        ElseIf currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then
            '   default can be ex: Old Table 11
            If currentTable.IsTabNotTable = True Then
                '   *************************
                '   we do not need this for tabs

                For Each oRow In dsOrder.Tables("AvailTabs").Rows
                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        '   we should add subtotal to show closed
                        oRow("ClosedSubTotal") = currentTable.SubTotal
                        oRow("LastStatusTime") = Now
                        If oRow("AvailForSeating") Is DBNull.Value Then
                            oRow("AvailForSeating") = Now
                        End If
                        oRow("LastStatus") = 10
                        '           oRow("TabName") = "Closed " & currentTable.TableNumber.ToString
                    End If
                Next
                '            bRow = (dsBackup.Tables("AvailTabsTerminal").Rows.Find(currentTable.ExperienceNumber))
                '           If Not (bRow Is Nothing) Then
                '           bRow("ClosedSubTotal") = currentTable.SubTotal
                '           bRow("LastStatus") = 1
                ''           bRow("LastStatusTime") = Now
                '     End If

            Else

                For Each oRow In dsOrder.Tables("AvailTables").Rows

                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        '   we should add closed Sub Total
                        '   to indicate table is closed
                        oRow("ClosedSubTotal") = currentTable.SubTotal
                        oRow("LastStatusTime") = Now
                        oRow("TabName") = "Closed " & currentTable.TableNumber.ToString
                        If oRow("AvailForSeating") Is DBNull.Value Then
                            oRow("AvailForSeating") = Now
                        End If
                        oRow("LastStatus") = 10
                        Exit For
                    End If
                Next

                If typeProgram = "Online_Demo" Then
                    Dim aRow As DataRow
                    For Each aRow In dsOrder.Tables("AllTables").Rows
                        '         MsgBox(aRow("TableNumber"))
                        '        MsgBox(oRow("TableNumber"))

                        If aRow("TableNumber") = currentTable.TableNumber Then 'oRow("TableNumber") Then
                            aRow("EmployeeID") = 0
                            aRow("TableStatusID") = 10
                            Exit For
                        End If
                    Next
                End If
                '   GenerateOrderTables.ChangeStatusInDataBase(1, Nothing, 0, Nothing)
            End If
        End If

        'sss     SaveAvailTabsAndTables()
        '222     AddStatusChangeData(currentTable.ExperienceNumber, 1, Nothing, 0, Nothing)


    End Sub

    Friend Sub JustMarkAsCloseNoRelease222()
        Dim oRow As DataRow
        Dim bRow As DataRow
        Dim vRow As DataRowView

        If currentTerminal.TermMethod = "Quick" Or currentTable.TicketNumber > 0 Then   ' currentTable.TabID = -888 Then

            For Each oRow In dsOrder.Tables("QuickTickets").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        oRow("LastStatus") = 10
                        Exit For
                    End If
                End If
            Next
        ElseIf currentTerminal.TermMethod = "Table" Or currentTerminal.TermMethod = "Bar" Then

            If currentTable.IsTabNotTable = True Then
                For Each oRow In dsOrder.Tables("AvailTabs").Rows
                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        oRow("LastStatus") = 10
                        Exit For
                    End If
                Next
            Else

                For Each oRow In dsOrder.Tables("AvailTables").Rows

                    If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                        oRow("LastStatus") = 10
                        Exit For
                    End If
                Next

                If typeProgram = "Online_Demo" Then
                    Dim aRow As DataRow
                    For Each aRow In dsOrder.Tables("AllTables").Rows
                        If aRow("TableNumber") = currentTable.TableNumber Then 'oRow("TableNumber") Then
                            aRow("EmployeeID") = 0
                            aRow("TableStatusID") = 10
                            Exit For
                        End If
                    Next
                End If
            End If
        End If

    End Sub

    Friend Function ResetSeatingChartTableStatus(ByVal ts As Integer, ByVal overrideAvail As Boolean)

        Dim oRow As DataRow
        Dim newStatus As Integer
        '   Dim lastExpNum As Int64
        Dim cc As Color
        Dim i As Integer

        If overrideAvail = True Then
            For Each oRow In dsOrder.Tables("AllTables").Rows '777 ds.Tables("TermsTables").Rows
                If oRow("Active") = True Then
                    If oRow("TableNumber") = ts Then
                        If oRow("Available") = True Then       'avail for seating
                            oRow("Available") = False
                            newStatus = 0
                        Else                                'all other including sat
                            oRow("Available") = True
                            newStatus = 1
                        End If
                        cc = DetermineColor(newStatus)

                        btnTable(i).lblTableNum.BackColor = cc
                        If cc.ToString = c15.ToString Then
                            btnTable(i).IsAvail = True
                        Else
                            btnTable(i).IsAvail = False
                        End If
                        Exit For
                    End If
                End If
                i += 1
            Next

            Try
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                sql.SqlDataAdapterAllTables.Update(dsOrder.Tables("AllTables"))
                '777    sql.SqlTermsTables.Update(ds.Tables("TermsTables"))
                sql.cn.Close()
                dsOrder.Tables("AllTables").AcceptChanges()
                '777      ds.Tables("TermsTables").AcceptChanges()
            Catch ex As Exception
                CloseConnection()
            End Try
        Else
            ' we are only changing this temp, will be perm when when reenter
            For Each oRow In dsOrder.Tables("AllTables").Rows '  For Each tbl In currentPhysicalTables
                ' 777 For Each oRow In dsOrder.Tables("TermsTables").Rows '  For Each tbl In currentPhysicalTables
                '444      If oRow("Active") = True Then
                If oRow("TableNumber") = ts Then

                    oRow("TableStatusID") = 1
                    newStatus = 1

                    cc = DetermineColor(newStatus)
                    btnTable(i).lblTableNum.BackColor = cc
                    If cc.ToString = c15.ToString Then
                        btnTable(i).IsAvail = True
                    Else
                        btnTable(i).IsAvail = False
                    End If
                    '         If Not oRow("MaxExpNumByTable") Is DBNull.Value Then
                    '        lastExpNum = oRow("MaxExpNumByTable")
                    '   Else
                    '      lastExpNum = 0
                    ' End If
                    Exit For
                End If
                '444    End If
                i += 1
            Next
        End If

        Return newStatus

    End Function

    Friend Function DetermineColor(ByVal currentStatus As Integer)
        Dim colorChoice As Color
        '   do not change colors
        '   status is dependant on colors in other parts of program

        If currentStatus = 0 Then      'unavailable
            colorChoice = c5            'dim gray
        ElseIf currentStatus = 1 Or currentStatus = 9 Or currentStatus = 10 Then  'available for seating
            colorChoice = c15            'cornflower blue
        ElseIf currentStatus = 7 Then 'check down
            colorChoice = c1            'yellow
        Else                                'table sat (includes all)
            colorChoice = c9            'red
        End If

        Return colorChoice
    End Function

    Private Sub ChangeTableStatusInCollection222()
        Dim tbl As PhysicalTable

        For Each tbl In currentPhysicalTables
            If tbl.PhysicalTableNumber = currentTable.TableNumber Then
                tbl.CurrentStatus = 1       'avail for seating
            End If
        Next

        '   *** we must do this for every terminal
        '   all terminals have their own collections

    End Sub

    Private Sub RemoveTableFromCollection222()
        Dim btnTabsAndTables As New DataSet_Builder.AvailTableUserControl

        For Each btnTabsAndTables In currentActiveTables
            If btnTabsAndTables.ExperienceNumber = currentTable.ExperienceNumber Then
                currentActiveTables.Remove(btnTabsAndTables)
                Exit Sub
            End If
        Next

    End Sub

    Friend Sub RecalculateCheckNumber(ByVal expNum As Int64, ByVal cnIncrease As Integer)
        Dim oRow As DataRow
        '      Dim bRow As DataRow

        If currentTerminal.TermMethod = "Quick" Or currentTable.TicketNumber > 0 Then
            For Each oRow In dsOrder.Tables("QuickTickets").Rows
                If oRow("ExperienceNumber") = expNum Then
                    oRow("NumberOfChecks") += cnIncrease
                End If
            Next
        Else
            If currentTable.IsTabNotTable = True Then
                For Each oRow In dsOrder.Tables("AvailTabs").Rows
                    If oRow("ExperienceNumber") = expNum Then
                        oRow("NumberOfChecks") += cnIncrease
                    End If
                Next
            Else
                For Each oRow In dsOrder.Tables("AvailTables").Rows
                    If oRow("ExperienceNumber") = expNum Then
                        oRow("NumberOfChecks") += cnIncrease
                    End If
                Next
            End If
        End If



    End Sub


    Friend Function DetermineCnTest(ByVal cn As Integer)
        '   this tests to see how if the new or old customer number has any information and how much
        Dim cnTestValue As Integer

        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
            cnTestValue = (dsOrder.Tables("OpenOrders")).Compute("Count(CustomerNumber)", "CustomerNumber ='" & cn & "'")
        Else : cnTestValue = 0
        End If

        Return cnTestValue

    End Function

    Friend Function DeterminePizzaHalfCount222(ByVal currentSI2 As Integer)
        '   this tests to see how if the new or old customer number has any information and how much
        Dim cnTestValue As Integer

        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
            cnTestValue = (dsOrder.Tables("OpenOrders")).Compute("Count(si2)", "si2 ='" & currentSI2 & "'")
        Else : cnTestValue = 0
        End If

        Return cnTestValue

    End Function

    Friend Function DetermineCheckCount(ByVal ct As Integer)
        Dim cnTestValue As Integer

        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
            cnTestValue = (dsOrder.Tables("OpenOrders")).Compute("Count(CheckNumber)", "CheckNumber ='" & ct & "'")
        Else : cnTestValue = 0
        End If

        Return cnTestValue
    End Function

    Friend Sub GotoNextCheckNumber()

        Dim firstCheckNumber As Integer
        Dim i As Integer
        Dim checkCount As Integer
        Dim maxCN As Integer

        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
            maxCN = (dsOrder.Tables("OpenOrders")).Compute("Max(CheckNumber)", Nothing)
        Else
            Exit Sub
        End If

        If currentTable.CheckNumber >= maxCN Then    'currentTable.NumberOfChecks Then
            firstCheckNumber = 1
        Else
            firstCheckNumber = currentTable.CheckNumber + 1
        End If

        'this will never occur . but putting this here to avoid a forever loop
        If firstCheckNumber > currentTable.NumberOfChecks Then
            currentTable.CheckNumber = 1
            Exit Sub
        End If

        For i = firstCheckNumber To maxCN   ' currentTable.NumberOfChecks
            checkCount = DetermineCheckCount(i)
            If checkCount > 0 Then
                currentTable.CheckNumber = i
                Exit Sub
            End If
        Next

        ' default
        currentTable.CheckNumber = 1

    End Sub

    Friend Function CustomerPanelOneTest()
        Dim oRow As DataRow
        Dim cust1Showing As Boolean

        For Each oRow In dsOrder.Tables("OpenOrders").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                If oRow("sin") = 1 And oRow("ItemID") = 0 Then
                    cust1Showing = True
                    Exit For
                End If
            End If
        Next

        If cust1Showing = False Then
            Dim currentItem As SelectedItemDetail = New SelectedItemDetail
            Dim custNumString As String = "               1   CUSTOMER   1"

            With currentItem
                .Check = currentTable.CheckNumber
                .Customer = 1
                .Course = 2 'currentTable.CourseNumber
                .SIN = 1
                .SII = 1
                .si2 = 0
                .ID = 0
                .FunctionFlag = "N"
                .Name = custNumString
                .TerminalName = custNumString
                .ChitName = custNumString
                .Price = Nothing
                .Category = Nothing
            End With

            currentTable.SIN += 1            '********???????????????????
            If dvOrder.Count > 0 Then
                PopulateDataRowForOpenOrder(currentItem)
            Else
                '444            DisposeDataViewsOrder()
                PopulateDataRowForOpenOrder(currentItem)
                '444           CreateDataViewsOrder()
            End If
        End If

        Return cust1Showing

    End Function



    Friend Sub DeleteOpenOrdersRowTerminal(ByRef dRow As DataRow)

        '   this is repeated below
        '    we can remove if we active below
        If Not (dRow Is Nothing) Then
            If Not dRow.RowState = DataRowState.Deleted Or Not dRow.RowState = DataRowState.Detached Then
                dRow.Delete()
            End If
        End If

        Exit Sub

        '222 the below is for terminal data
        If Not (dRow Is Nothing) Then
            If Not dRow.RowState = DataRowState.Added Then
                '   this means it is probably an old row somewhere in terminal dataset
                '   this is slow (maybe we can find some better)
                '   but not frequent.. only when we delete an item ordered and saved
                Dim terminalData As New DataView
                Dim ri As Integer

                With terminalData
                    .Table = dsBackup.Tables("OpenOrdersTerminal")         ''dtOpenOrdersTerminal
                    .RowFilter = "ExperienceNumber = '" & currentTable.ExperienceNumber & "'"
                    .Sort = "sin"
                End With

                ri = terminalData.Find(dRow("sin"))
                If Not ri = -1 Then
                    '   will not delete a row that is not there
                    terminalData.Delete(ri)
                End If
            End If
            If Not dRow.RowState = DataRowState.Deleted Or Not dRow.RowState = DataRowState.Detached Then
                dRow.Delete()
            End If
        End If

    End Sub

    Friend Sub PopulateCurrentTableData(ByRef oRow As DataRow)
        Dim sRow As DataRow

        currentTable.ExperienceNumber = oRow("ExperienceNumber")
        If oRow("TableNumber") Is DBNull.Value Then
            'this is tab
            currentTable.IsTabNotTable = True
            currentTable.TableNumber = 0
        Else
            currentTable.IsTabNotTable = False
            currentTable.TableNumber = oRow("TableNumber")
        End If
        currentTable.TabID = oRow("TabID")
        currentTable.TabName = oRow("TabName")
        currentTable.TicketNumber = oRow("TicketNumber")
        currentTable.EmployeeID = oRow("EmployeeID")
        currentTable.EmployeeNumber = currentServer.EmployeeNumber
        For Each sRow In dsEmployee.Tables("AllEmployees").Rows
            If sRow("EmployeeID") = currentTable.EmployeeID Then
                currentTable.EmployeeName = sRow("NickName")
                Exit For
            End If
        Next
        currentTable.CurrentMenu = oRow("MenuID")
        currentTable.StartingMenu = oRow("MenuID")
        currentTable.NumberOfChecks = oRow("NumberOfChecks")
        currentTable.NumberOfCustomers = oRow("NumberOfCustomers")
        currentTable.LastStatus = oRow("LastStatus")
        '      currentTable.FoodOrdered = oRow("FoodOrdered")
        currentTable.ItemsOnHold = oRow("ItemsOnHold")
        currentTable.OrderView = oRow("LastView")
        If Not oRow("ClosedSubTotal") Is DBNull.Value Then
            currentTable.IsClosed = True
        Else
            currentTable.IsClosed = False
        End If
        currentTable.AutoGratuity = oRow("AutoGratuity")
        currentTable.MethodUse = oRow("MethodUse")
        DefineMethodDirection()

    End Sub

    Friend Sub DefineMethodDirection()
        Dim vRow As DataRowView
        For Each vRow In dvTerminalsUseOrder
            If vRow("MethodUse") = currentTable.MethodUse Then
                currentTable.MethodDirection = vRow("MethodDirection")
                Exit For
            End If
        Next
    End Sub

    Friend Sub StartOrderProcess(ByVal experienceNumber As Int64)
        Dim tableNumber As Integer = currentTable.TableNumber
        Dim oRow As DataRow
        Dim satTm As DateTime
        'above is not correct

        currentTable.SatTime = Today

        For Each oRow In ds.Tables("MenuChoice").Rows
            If oRow("MenuID") = currentTable.CurrentMenu Then
                currentTable.CurrentMenuName = oRow("MenuName")
                Exit For
            End If
        Next

        If dsOrder.Tables("OpenOrders").Rows.Count = 0 Then
            '   when there is no order history for table

            satTm = DetermineStatusTime(2)
            Dim orderStat As Integer = 2
            currentTable.NumberOfChecks = 1
            currentTable.CheckNumber = 1
            If Not satTm = Nothing Then
                currentTable.SatTime = satTm
            End If

            currentTable.SIN = 2            'lSIN
            currentTable.CustomerNumber = 1
            currentTable.NextCustomerNumber = 1
            '444      currentTable.IsPrimaryMenu = True

            If currentTerminal.currentPrimaryMenuID = currentTerminal.initPrimaryMenuID Then 'currentTable.CurrentMenu Then
                currentTable.IsPrimaryMenu = True
            Else
                currentTable.IsPrimaryMenu = False
            End If
            currentTable.DailyCode = currentTerminal.CurrentDailyCode

            CreateCheck(currentTable.CheckNumber, currentTable.NumberOfCustomers)

        Else

            currentTable.SIN = DetermineSelectedItemNumber() + 2    'add 2 b/c of Customer Panel (subtracts 1)
            If currentTable.NumberOfChecks > 1 Then
                GotoNextCheckNumber()   'sending 0 so next should be 1 unless empty
            Else
                currentTable.CheckNumber = 1
            End If
            currentTable.CustomerNumber = 1
            currentTable.NextCustomerNumber = 1
            currentTable.DailyCode = dsOrder.Tables("OpenOrders").Rows(0)("DailyCode")
            satTm = DetermineStatusTime(2)
            If Not satTm = Nothing Then
                currentTable.SatTime = satTm
            End If
            '444     If primaryMenuID = currentTable.CurrentMenu Then
            If currentTerminal.currentPrimaryMenuID = currentTerminal.initPrimaryMenuID Then
                currentTable.IsPrimaryMenu = True
            Else
                currentTable.IsPrimaryMenu = False
            End If
        End If

        If Not currentTerminal.TermMethod = "Quick" Then
            CreateDataViewsOrder()
        Else
            If Not actingManager Is Nothing Then
                'this is b/c when comming from manager in QUICK we must do this
                'b/c we deleted data views when leaving last order
                CreateDataViewsOrder()
            End If
        End If

        currentTable.OrderView = "Detailed Order"
        currentTable.ReferenceSIN = currentTable.SIN
        currentTable.MiddleOfOrder = False
        currentTable.CourseNumber = 2
        currentTable.Quantity = 1
        currentTable.EmptyCustPanel = 0
        currentTable.si2 = 0        'this is coded for pizza's or split items as 1
        currentTable.Tempsi2 = 0
        currentTable.IsPizza = False

    End Sub

    '*** this is for when we want to list all open orders
    Private Sub CheckForOpenOrderDetail222()
        Dim lon As Int64
        Dim dtr As SqlClient.SqlDataReader
        Dim i As Integer
        Dim oRow As DataRow
        Dim rowCount As Integer

        rowCount = dsOrder.Tables("OrderDetail").Rows.Count

        If rowCount > 0 Then
            For i = (rowCount - 1) To 0 Step -1
                oRow = dsOrder.Tables("OrderDetail").Rows(i)
                MsgBox(oRow("OrderNumber"))
                If oRow("OrderFilled") Is DBNull.Value Then
                    lon = oRow("OrderNumber")
                    Exit For
                End If
            Next
        Else
            lon = 0
        End If

    End Sub

    Private Sub CreateCheck(ByVal chkNum As Integer, ByVal numCust As Integer)
        Dim newCheck As Check
        '   check is a structure defined in term_Tahsc

        newCheck.StructureCheckNumber = chkNum
        newCheck.NumberOfCustomers = numCust

        currentTable._checkCollection.Add(newCheck)


    End Sub

    Friend Function DetermineSelectedItemNumber()
        Dim currentSIN As Integer

        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
            currentSIN = dsOrder.Tables("OpenOrders").Compute("Max(sin)", "")
        End If

        Return currentSIN

    End Function

    Private Function DetermineSelectedItemIndex222()
        Dim currentSII As Integer

        If dsOrder.Tables("OpenOrders").Rows.Count > 0 Then
            currentSII = dsOrder.Tables("OpenOrders").Compute("Max(sii)", "")
        End If

        Return currentSII

    End Function

    Friend Function DetermineStatusTime(ByVal status As Integer)
        Dim statusTime As DateTime
        Dim oRow As DataRow

        '   this will only return the latest time for this status
        '       For Each oRow In dsOrder.Tables("StatusChange").Rows
        '      If Not oRow("TableStatusID") Is DBNull.Value Then
        '     If oRow("TableStatusID") = status Then
        '    statusTime = oRow("StatusTime")
        '   End If
        ''  End If
        'Next

        '   this will only sat time

        If currentTable.IsTabNotTable = True Then
            For Each oRow In dsOrder.Tables("AvailTabs").Rows
                statusTime = oRow("ExperienceDate")
                Exit For
            Next
        Else
            For Each oRow In dsOrder.Tables("AvailTables").Rows
                statusTime = oRow("ExperienceDate")
                Exit For
            Next
        End If

        Return statusTime

    End Function

    Friend Sub PopulateStatusData222(ByVal experienceNumber As Int64)
        'statusChangeSelectCommand (stored procedure)
        dsOrder.Tables("StatusChange").Rows.Clear()

        '      Try
        '      sql.cn.Open()
        '            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        '     sql.SqlSelectCommandESC.Parameters("@LocationID").Value = companyInfo.LocationID
        ''     sql.SqlSelectCommandESC.Parameters("@ExperienceNumber").Value = experienceNumber
        '     sql.SqlDataAdapterESC.Fill(dsOrder.Tables("StatusChange"))
        '     sql.cn.Close()
        '    Catch ex As Exception
        ''        GenerateOrderTables.CloseConnection()
        '        End Try

    End Sub

    Private Sub PopulateStatusDataWhenDown222(ByVal experienceNumber As Int64)

        Dim bRow As DataRow
        Dim oRow As DataRow

        Dim copyRows() As DataRow

        copyRows = dsBackup.Tables("ESCTerminal").Select("ExperienceNumber = '" & experienceNumber & "'")
        If Not copyRows Is Nothing Then
            For Each bRow In copyRows

                oRow = dsOrder.Tables("StatusChange").NewRow
                oRow = CopyOneRowToAnotherExpStatusChange(bRow, oRow)
                dsOrder.Tables("StatusChange").Rows.Add(oRow)

            Next
        Else
            MsgBox("Copy Rows is nothing")
        End If
        dsOrder.Tables("StatusChange").AcceptChanges()

    End Sub

    Friend Sub PopulateOpenOrderData222(ByVal experienceNumber As Int64, ByVal fromStart As Boolean)
        'I only call at beginning to initiate dataset

        dsOrder.Tables("OpenOrders").Rows.Clear()


        Try
            If fromStart = False Then
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            End If
            sql.SqlSelectCommandOpenOrdersSP.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandOpenOrdersSP.Parameters("@ExperienceNumber").Value = experienceNumber
            sql.SqlDataAdapterOpenOrdersSP.Fill(dsOrder.Tables("OpenOrders"))

            sql.SqlSelectCommandPayments.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandPayments.Parameters("@ExperienceNumber").Value = experienceNumber
            sql.SqlDataAdapterPayments.Fill(dsOrder.Tables("PaymentsAndCredits"))

            sql.SqlSelectCommandOrderDetail.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandOrderDetail.Parameters("@ExperienceNumber").Value = experienceNumber
            sql.SqlDataAdapterOrderDetail.Fill(dsOrder.Tables("OrderDetail"))

            '           sql.SqlSelectCommandESC.Parameters("@LocationID").Value = companyInfo.LocationID
            '          sql.SqlSelectCommandESC.Parameters("@ExperienceNumber").Value = experienceNumber
            '         sql.SqlDataAdapterESC.Fill(dsOrder.Tables("StatusChange"))

            sql.SqlSelectCommandAvailTablesSP.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandAvailTablesSP.Parameters("@EmployeeID").Value = -1
            sql.SqlSelectCommandAvailTablesSP.Parameters("@DailyCode").Value = -1
            sql.SqlAvailTablesSP.Fill(dsOrder.Tables("AvailTables"))

            sql.SqlSelectCommandAvailTabsSP.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandAvailTabsSP.Parameters("@DailyCode").Value = -1
            sql.SqlSelectCommandAvailTabsSP.Parameters("@EmployeeID").Value = -1
            sql.SqlAvailTabsSP.Fill(dsOrder.Tables("AvailTabs"))
            If fromStart = False Then
                sql.cn.Close()
            End If

        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
            If Err.Number = "5" Then
                ServerJustWentDown()
            End If
            '          PoplulateOpenOrderDataWhenDown()
        End Try

        '          PoplulateOpenOrderDataWhenDown()



    End Sub

    Private Sub PoplulateOpenOrderDataWhenDown222()
        Dim oRow As DataRow
        Dim bRow As DataRow
        Dim copyRows() As DataRow
        dsOrder.Tables("OpenOrders").Clear()

        copyRows = dsBackup.Tables("OpenOrdersTerminal").Select("ExperienceNumber = '" & currentTable.ExperienceNumber & "'")

        For Each bRow In copyRows
            oRow = dsOrder.Tables("OpenOrders").NewRow
            CopyOneRowToAnotherOpenOrders222(bRow, oRow)
            dsOrder.Tables("OpenOrders").Rows.Add(oRow)
        Next
        dsOrder.Tables("OpenOrders").AcceptChanges()

    End Sub

    Private Sub PopulateOrderDetail(ByVal expNum As Int64)
        dsOrder.Tables("OrderDetail").Rows.Clear()

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            '        sql.SqlSelectCommandOpenOrdersSP.Parameters("@CompanyID").Value = CompanyID
            sql.SqlSelectCommandOrderDetail.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandOrderDetail.Parameters("@ExperienceNumber").Value = expNum
            '     sql.SqlSelectCommandOrderDetail.Parameters("@DailyCode").Value = currentTerminal.currentDailyCode
            sql.SqlDataAdapterOrderDetail.Fill(dsOrder.Tables("OrderDetail"))
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
            If Err.Number = "5" Then
                ServerJustWentDown()
            End If
            '          PoplulateOpenOrderDataWhenDown()
        End Try


    End Sub


    '   **************************
    '   Payments And Credits
    '   **************************

    Friend Sub AddPaymentToCollection(ByRef newpayment As DataSet_Builder.Payment)

        Dim testPay As DataSet_Builder.Payment

        For Each testPay In tabcc
            If testPay.experienceNumber = newpayment.experienceNumber Then
                If testPay.AccountNumber = newpayment.AccountNumber Then
                    ' we have the same payment assc with this account, 
                    '   REMOVE, put in most current info
                    tabcc.Remove(testPay)
                    Exit For
                End If
            End If
        Next

        tabcc.Add(newpayment)

    End Sub

    Friend Sub RemovePaymentFromCollection(ByRef acctNum As String) 'newpayment As DataSet_Builder.Payment)
        Dim testPay As DataSet_Builder.Payment

        For Each testPay In tabcc
            If testPay.experienceNumber = currentTable.ExperienceNumber Then
                If testPay.AccountNumber = acctNum Then
                    ' we have the same payment assc with this account, 
                    '   REMOVE, put in most current info
                    tabcc.Remove(testPay)
                    Exit For
                End If
            End If
        Next
    End Sub

    Friend Function RetreivePaymentFromColloection222()

        Dim storedPayment As New Payment
        tabccThisExperience.Clear()

        For Each storedPayment In tabcc
            If storedPayment.experienceNumber = currentTable.ExperienceNumber Then
                tabccThisExperience.Add(storedPayment)
            End If
        Next

        '      Return storedPayment
    End Function

    Friend Sub PopulatePaymentsAndCredits222(ByVal experienceNumber As Int64)
        dsOrder.Tables("PaymentsAndCredits").Rows.Clear()

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            '          sql.SqlSelectCommandPayments.Parameters("@CompanyID").Value = CompanyID
            sql.SqlSelectCommandPayments.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandPayments.Parameters("@ExperienceNumber").Value = experienceNumber
            sql.SqlDataAdapterPayments.Fill(dsOrder.Tables("PaymentsAndCredits"))
            sql.cn.Close()

        Catch ex As Exception
            CloseConnection()
            If Err.Number = "5" Then
                ServerJustWentDown()
            End If
            '               PopulatePaymentsAndCreditsWhenDown(currentTable.ExperienceNumber)
        End Try

    End Sub

    Friend Function PopulatePaymentsAndCreditsByDaily(ByVal dc As Int64)

        Dim oRow As DataRow
        Dim newvalueAcct As String

        dsOrder.Tables("PaymentsAndCredits").Rows.Clear()

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlSelectCommandPaymentsBatch.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandPaymentsBatch.Parameters("@DailyCode").Value = dc
            sql.SqlDataAdapterPaymentsBatch.Fill(dsOrder.Tables("PaymentsAndCredits"))
            sql.cn.Close()

            For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
                If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                    '   If oRow("PaymentTypeID") > 1 Then
                    If oRow("PaymentFlag") = "cc" Or oRow("PaymentFlag") = "Gift" Or oRow("PaymentFlag") = "Issue" Then

                        If Not oRow("AccountNumber").Substring(0, 4) = "xxxx" And Not oRow("AccountNumber") = "Manual" Then
                            Try
                                If oRow("AccountNumber").ToString.Length > 20 Then
                                    'if encrypt acct# length > 50, then this will account# will be wrong
                                    newvalueAcct = CryOutloud.Decrypt(oRow("AccountNumber"), "test")
                                    oRow("AccountNumber") = newvalueAcct
                                End If

                                'can't encrypt exp date b/c database only holds 4 chars
                                '     newvalueExpDate = CryOutloud.Decrypt(oRow("CCExpiration"), "test")
                                '    oRow("CCExpiration") = newvalueExpDate
                            Catch ex As Exception

                            End Try
                        End If
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            CloseConnection()
            If Err.Number = "5" Then
                ServerJustWentDown()
            End If
            Return False
            '               PopulatePaymentsAndCreditsWhenDown(currentTable.ExperienceNumber)
        End Try

    End Function

    Friend Sub UpdatePaymentsAndCreditsBatch()

        Dim oRow As DataRow
        Dim newvalueAcct As String

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                '      If oRow("PaymentTypeID") > 1 Then
                If oRow("PaymentFlag") = "cc" Or oRow("PaymentFlag") = "Gift" Or oRow("PaymentFlag") = "Issue" Then

                    If Not oRow("AccountNumber").Substring(0, 4) = "xxxx" And Not oRow("AccountNumber") = "Manual" Then
                        Try
                            If oRow("AccountNumber").ToString.Length < 20 Then
                                newvalueAcct = CryOutloud.Encrypt(oRow("AccountNumber"), "test")
                                oRow("AccountNumber") = newvalueAcct
                            End If

                            'can't encrypt exp date b/c database only holds 4 chars
                            '     newvalueExpDate = CryOutloud.Decrypt(oRow("CCExpiration"), "test")
                            '    oRow("CCExpiration") = newvalueExpDate
                        Catch ex As Exception

                        End Try
                    End If
                End If
            End If
        Next

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlDataAdapterPaymentsBatch.Update(dsOrder.Tables("PaymentsAndCredits"))
            sql.cn.Close()

        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

        dsOrder.Tables("PaymentsAndCredits").AcceptChanges()

    End Sub

    Private Sub VerifyPaymentInfo(ByRef newPayment As Payment)

        '   If newPayment.PaymentFlag = "cc" Or newPayment.PaymentFlag = "Issue" Or newPayment.PaymentFlag = "Gift" Then
        If newPayment.PaymentTypeID > 1 Then

            If newPayment.AccountNumber.Length > 50 Then
                newPayment.AccountNumber = newPayment.AccountNumber.Substring(0, 50)
            End If
            If newPayment.ExpDate.Length > 4 Then
                newPayment.ExpDate = newPayment.ExpDate.Substring(0, 4)
            End If
            'CCV
            If Not newPayment.Track2 Is Nothing Then
                If newPayment.Track2.Length > 50 Then
                    newPayment.Track2 = newPayment.Track2.Substring(0, 50)
                End If
            End If

            If newPayment.Name.Length = 0 Then
                newPayment.Name = "Customer"
            ElseIf newPayment.Name.Length > 50 Then
                newPayment.Name = newPayment.Name.Substring(0, 50)
            End If
        End If

    End Sub
    Public Sub AddPaymentToDataRow(ByRef newPayment As DataSet_Builder.Payment, ByVal doApply As Boolean, ByVal en As Int64, ByVal empID As Integer, ByVal cn As Integer, ByVal autoGrat As Boolean)

        VerifyPaymentInfo(newPayment)

        Dim oRow As DataRow = dsOrder.Tables("PaymentsAndCredits").NewRow

        '      oRow("PaymentsAndCreditsID") = DBNull.Value
        oRow("CompanyID") = companyInfo.CompanyID
        oRow("LocationID") = companyInfo.LocationID
        oRow("DailyCode") = currentTerminal.CurrentDailyCode
        oRow("ExperienceNumber") = en   'currentTable.ExperienceNumber
        oRow("PaymentDate") = Now
        oRow("EmployeeID") = empID  ' currentServer.EmployeeID 
        oRow("CheckNumber") = cn    'currentTable.CheckNumber
        oRow("PaymentTypeID") = newPayment.PaymentTypeID
        oRow("PaymentTypeName") = newPayment.PaymentTypeName
        '   *** will change 
        If newPayment.PaymentTypeID = 1 Then 'TypeName = "Cash" Then
            oRow("PaymentFlag") = "Cash"
        ElseIf newPayment.PaymentTypeID = -98 Then 'Gift Certificate
            oRow("PaymentFlag") = "Gift Cert" '"Cash" '
        ElseIf newPayment.PaymentTypeID = 6 Then ' = "MPS Gift" Then
            oRow("PaymentFlag") = "Gift"
            oRow("TransactionType") = "PrePaid"
            oRow("TransactionCode") = newPayment.TranCode
        ElseIf newPayment.PaymentTypeID = -97 Then ' = "Issue Gift" Then
            oRow("PaymentFlag") = "Issue"
            oRow("TransactionType") = "PrePaid"
            '**** is trans code is not correct ????
            oRow("TransactionCode") = newPayment.TranCode
        ElseIf newPayment.PaymentTypeID = 9 Then 'outside credit
            oRow("PaymentFlag") = "outside"
            oRow("TransactionType") = "Credit"
            oRow("TransactionCode") = newPayment.TranCode
        Else
            oRow("PaymentFlag") = "cc"
            oRow("TransactionType") = "Credit"
            oRow("TransactionCode") = newPayment.TranCode
        End If
        oRow("AccountNumber") = newPayment.AccountNumber
        oRow("CCExpiration") = newPayment.ExpDate
        oRow("CVV") = DBNull.Value
        oRow("Track2") = newPayment.Track2
        oRow("CustomerName") = newPayment.Name
        oRow("SwipeType") = newPayment.Swiped
        oRow("PaymentAmount") = newPayment.Purchase
        oRow("Surcharge") = CType(0, Decimal)
        If autoGrat = True Then
            oRow("Tip") = Format(newPayment.Purchase * companyInfo.autoGratuityPercent, "##,###.00")
        Else
            oRow("Tip") = CType(0, Decimal)
        End If

        If doApply = True Then
            If oRow("PaymentFlag") = "Cash" Then
                oRow("AuthCode") = "Cash"
            Else
                oRow("AuthCode") = DBNull.Value
            End If
        Else
            oRow("AuthCode") = DBNull.Value
        End If

        oRow("Applied") = doApply
        oRow("RefNum") = newPayment.RefNo '(currentTable.ExperienceNumber).ToString
        'refNo: & "-" & currentTable.CheckNumber & "-" & paymentRowIndex).ToString
        oRow("BatchCleared") = 0
        oRow("Duplicate") = 0
        oRow("TerminalID") = currentTerminal.TermID
        oRow("TerminalsOpenID") = currentTerminal.TerminalsOpenID
        oRow("AlreadyPrinted") = 0
        oRow("Description") = newPayment.Description
        If mainServerConnected = True Then
            oRow("dbUP") = 1
        Else
            oRow("dbUP") = 0
        End If
        If oRow("PaymentFlag") = "Issue" Or oRow("PaymentFlag") = "Gift" Then
            'leter we can place all swipe codes in payment
            'not doing yet b/c it might be easieer to search w/o clutter
            If Not newPayment.TabID = Nothing Then
                oRow("OpenBigInt1") = newPayment.TabID
            Else
                oRow("OpenBigInt1") = DBNull.Value
            End If
        Else
            oRow("OpenBigInt1") = DBNull.Value
        End If

        If typeProgram = "Online_Demo" Then
            oRow("PaymentsAndCreditsID") = demoPaymentID
            demoPaymentID += 1
        End If

        dsOrder.Tables("PaymentsAndCredits").Rows.Add(oRow)

    End Sub
    Public Function ValidateExpDate(ByVal expDate As String)

        Try
            If expDate.Length < 4 Or expDate = "MMYY" Then
                MsgBox("Expiration date need to be in Format:   MMYY ")
                Return False
            Else
                If CInt((expDate.Substring(2, 2))) < (Now.Year - 2000) Then
                    MsgBox("Customer Card Expired!")
                    Return False
                ElseIf CInt((expDate.Substring(2, 2))) = (Now.Year - 2000) Then
                    If CInt((expDate.Substring(0, 2))) < Now.Month Then
                        MsgBox("Customer Card Expired!")
                        Return False
                    End If
                End If

                Return True
            End If
        Catch ex As Exception
            MsgBox("Expiration date need to be in Format:   MMYY ")
            Return False
        End Try

    End Function

    Public Function DetermineCreditCardName(ByVal newAcctNum As String)
        Dim firstDigit As String
        Dim secondDigit As String
        Dim thirdDigit As String
        Dim TypeName As String
        TypeName = ""

        If Not newAcctNum.Length > 3 Then
            Return TypeName
            Exit Function
        End If

        firstDigit = (newAcctNum.Substring(0, 1))

        Select Case firstDigit
            Case "3"
                secondDigit = (newAcctNum.Substring(1, 1))
                If secondDigit = "4" Or secondDigit = "7" Then
                    TypeName = "AMEX"
                ElseIf secondDigit = "6" Or secondDigit = "0" Or secondDigit = "8" Then
                    '   "0" and "8" are going away soon
                    TypeName = "DCLB"   'diner's club
                End If
            Case "4"
                TypeName = "VISA"
            Case "5"
                'this is for Dinner's Club cards bought by Master Card
                TypeName = "M/C"
            Case "6"
                secondDigit = (newAcctNum.Substring(1, 3))
                If secondDigit = "011" Then
                    TypeName = "DCVR"
                ElseIf secondDigit = "050" Then
                    thirdDigit = (newAcctNum.Substring(4, 3))
                    If thirdDigit = "110" Then
                        TypeName = "MPS Gift"
                        'ElseIf thirdDigit = "000" Then
                        'for other gift types
                    End If
                End If
        End Select

        Return TypeName

    End Function

    Public Function DetermineCreditCardID(ByVal TypeName As String) As Integer
        Dim creditCardID As Integer = 0
        Dim oRow As DataRow

        '   *** ADD FLAG    
        '   this will change when redo credit card crap
        '   right now its backwards

        For Each oRow In ds.Tables("CreditCardDetail").Rows
            If oRow("PaymentTypeName") = TypeName Then
                creditCardID = oRow("PaymentTypeID")
                Exit For
            End If
        Next

        Return creditCardID

    End Function

    Public Function DeterminePaymentFlag222(ByVal TypeName As String)
        Dim PayFlag As String

        Dim oRow As DataRow

        For Each oRow In ds.Tables("CreditCardDetail").Rows
            If oRow("PaymentTypeName") = TypeName Then
                PayFlag = oRow("PaymentFlag")
                Exit For
            End If
        Next

        Return PayFlag

    End Function

    Private Sub PopulatePaymentsAndCreditsWhenDown222(ByVal expNum As Int64)
        Dim bRow As DataRow
        Dim oRow As DataRow
        Dim copyRows() As DataRow
        dsOrder.Tables("PaymentsAndCredits").Rows.Clear()
        Dim numberCopied As Integer

        '   we are copying all rows for this experience number
        '   then deleting any rows from terminal not in SQL Server
        '   then we are setting these to new rows, so they get re-entered
        '   we do this because we must record any changes and we can't keep track w/o ID#
        Try
            CopyPaymentRows(dsOrder.Tables("PaymentsAndCredits"), dsBackup.Tables("PaymentsAndCreditsTerminal"), "ExperienceNumber = '" & expNum & "' AND PaymentsAndCreditsID IS NOT NULL")

            '   must reinitialize our table (this tell us all this info is old)
            dsOrder.Tables("PaymentsAndCredits").AcceptChanges()

            numberCopied = CopyPaymentRows(dsOrder.Tables("PaymentsAndCredits"), dsBackup.Tables("PaymentsAndCreditsTerminal"), "ExperienceNumber = '" & expNum & "' AND PaymentsAndCreditsID IS NULL")

            If numberCopied > 0 Then
                DeleteNonSQLRecordedRowsPayments(dsBackup.Tables("PaymentsAndCreditsTerminal"))
                dsBackup.Tables("PaymentsAndCreditsTerminal").AcceptChanges()
            End If

        Catch ex As Exception
            '   if copying fails    .... we revert to an empty table  ???
            '   can't use here.. since we are deleting terminal rows
            '      dsOrder.Tables("PaymentsAndCredits").Rows.Clear()
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Function CopyPaymentRows(ByRef newdTable As DataTable, ByRef olddTable As DataTable, ByVal selectionString As String)
        Dim bRow As DataRow
        Dim oRow As DataRow
        Dim copyRows() As DataRow

        copyRows = olddTable.Select(selectionString)
        If Not copyRows Is Nothing Then
            For Each bRow In copyRows
                oRow = newdTable.NewRow
                CopyOneRowToAnotherPaymentsAndCredits(bRow, oRow, -1) '  will copy db value
                newdTable.Rows.Add(oRow)
            Next
        Else

        End If

        Return copyRows.Length

    End Function

    Friend Sub PopulatePaymentsAndCreditsWhenClosing()
        '     Dim oRow As DataRow
        '   same as without closing but we don't want to keep openinf and closing database
        '   there is a better way to do this
        Dim newvalueAcct As String
        Dim oRow As DataRow

        dsOrder.Tables("PaymentsAndCredits").Clear()

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            '        For Each oRow In dsOrder.Tables("AvailTables").Rows
            sql.SqlSelectCommandPaymentsByEmployee.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandPaymentsByEmployee.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
            sql.SqlSelectCommandPaymentsByEmployee.Parameters("@EmployeeID").Value = currentClockEmp.EmployeeID 'currentServer.EmployeeID
            sql.SqlDataAdapterPaymentsByEmployee.Fill(dsOrder.Tables("PaymentsAndCredits"))
            '       Next
            '      For Each oRow In dsOrder.Tables("AvailTabs").Rows
            '       sql.SqlSelectCommandPaymentsByEmployee.Parameters("@CompanyID").Value = CompanyID
            '      sql.SqlSelectCommandPaymentsByEmployee.Parameters("@LocationID").Value = LocationID
            '     sql.SqlSelectCommandPaymentsByEmployee.Parameters("@DailyCode").Value = currentTerminal.currentDailyCode
            '    sql.SqlSelectCommandPaymentsByEmployee.Parameters("@EmployeeID").Value = currentServer.EmployeeID
            '   sql.SqlDataAdapterPaymentsByEmployee.Fill(dsOrder.Tables("PaymentsAndCredits"))
            '     Next
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
        End Try

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If Not oRow.RowState = DataRowState.Deleted And Not oRow.RowState = DataRowState.Detached Then
                '   If oRow("PaymentTypeID") > 1 Then
                If oRow("PaymentFlag") = "cc" Or oRow("PaymentFlag") = "Gift" Or oRow("PaymentFlag") = "Issue" Then
                    If Not oRow("AccountNumber").Substring(0, 4) = "xxxx" And Not oRow("AccountNumber") = "Manual" Then
                        Try
                            If oRow("AccountNumber").ToString.Length > 20 Then
                                newvalueAcct = CryOutloud.Decrypt(oRow("AccountNumber"), "test")
                                oRow("AccountNumber") = newvalueAcct
                            End If

                            'can't encrypt exp date b/c database only holds 4 chars
                            '     newvalueExpDate = CryOutloud.Decrypt(oRow("CCExpiration"), "test")
                            '    oRow("CCExpiration") = newvalueExpDate
                        Catch ex As Exception

                        End Try

                    End If
                End If
            End If
        Next

    End Sub

    Friend Sub UpdatePaymentsAndCreditsByEmployee()

        Try
            If typeProgram = "Online_Demo" Then
                dsOrderDemo.Merge(dsOrder.Tables("PaymentsAndCredits"), False, MissingSchemaAction.Add)
                '???   dsOrder.Tables("PaymentsAndCredits").Clear()
            Else
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                sql.SqlDataAdapterPaymentsByEmployee.Update(dsOrder.Tables("PaymentsAndCredits"))
                sql.cn.Close()
            End If

        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

        dsOrder.Tables("PaymentsAndCredits").AcceptChanges()

    End Sub


    Private Sub PopulatePaymentsAndCreditsWhenDownWhenClosing222()
        '*** this is wrong
        '   we need to do this for each closing table
        '   if we even want to close out when were down

        Dim bRow As DataRow
        Dim oRow As DataRow
        Dim copyRows() As DataRow

        Try
            copyRows = dsBackup.Tables("PaymentsAndCreditsTerminal").Select("ExperienceID = '" & currentServer.EmployeeID & "' AND currentTerminal.currentDailyCode = '" & currentTerminal.CurrentDailyCode & "'")
            If Not copyRows Is Nothing Then
                For Each bRow In copyRows

                    oRow = dsOrder.Tables("PaymentsAndCredits").NewRow
                    CopyOneRowToAnotherPaymentsAndCredits(bRow, oRow, -1) '  will copy db value
                    If typeProgram = "Online_Demo" Then
                        oRow("PaymentsAndCreditsID") = demoPaymentID
                        demoPaymentID += 1
                    End If
                    dsOrder.Tables("PaymentsAndCredits").Rows.Add(oRow)
                Next
            Else

            End If
            '   must reinitialize our table (this tell us all this info is old)
            dsOrder.Tables("PaymentsAndCredits").AcceptChanges()
        Catch ex As Exception
            '   if copying fails    .... we revert to an empty table  ???
            dsOrder.Tables("PaymentsAndCredits").Rows.Clear()

        End Try

    End Sub

    Friend Sub PopulateOpenOrdersWhenClosing222()
        Dim oRow As DataRow

        If mainServerConnected = True Then
            Try
                For Each oRow In dsOrder.Tables("AvailTables").Rows
                    sql.cn.Open()
                    sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                    sql.SqlSelectCommandOpenOrdersByEmployee.Parameters("@CompanyID").Value = companyInfo.CompanyID
                    sql.SqlSelectCommandOpenOrdersByEmployee.Parameters("@LocationID").Value = companyInfo.LocationID
                    sql.SqlSelectCommandOpenOrdersByEmployee.Parameters("@ExperienceNumber").Value = oRow("ExperienceNumber")
                    sql.SqlSelectCommandOpenOrdersByEmployee.Parameters("@EmployeeID").Value = currentServer.EmployeeID
                    sql.SqlDataAdapterOpenOrdersByEmployee.Fill(dsOrder.Tables("OpenOrders"))
                    sql.cn.Close()
                Next
                For Each oRow In dsOrder.Tables("AvailTabs").Rows
                    sql.cn.Open()
                    sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                    sql.SqlSelectCommandOpenOrdersByEmployee.Parameters("@CompanyID").Value = companyInfo.CompanyID
                    sql.SqlSelectCommandOpenOrdersByEmployee.Parameters("@LocationID").Value = companyInfo.LocationID
                    sql.SqlSelectCommandOpenOrdersByEmployee.Parameters("@ExperienceNumber").Value = oRow("ExperienceNumber")
                    sql.SqlSelectCommandOpenOrdersByEmployee.Parameters("@EmployeeID").Value = currentServer.EmployeeID
                    sql.SqlDataAdapterOpenOrdersByEmployee.Fill(dsOrder.Tables("OpenOrders"))
                    sql.cn.Close()
                Next
            Catch ex As Exception
                CloseConnection()
                If Err.Number = "5" Then
                    ServerJustWentDown()
                End If
                dsOrder.Tables("OpenOrders").Clear()
                PopulateOpenOrdersWhenDownWhenClosing222()
            End Try
        Else
            PopulateOpenOrdersWhenDownWhenClosing222()
        End If

    End Sub

    Private Sub PopulateOpenOrdersWhenDownWhenClosing222()
        '*** this is wrong
        '   we need to do this for each closing table
        '   if we even want to close out when were down

        Dim bRow As DataRow
        Dim oRow As DataRow
        Dim copyRows() As DataRow

        Try
            copyRows = dsBackup.Tables("OpenOrdersTerminal").Select("ExperienceID = '" & currentServer.EmployeeID & "' AND currentTerminal.currentDailyCode = '" & currentTerminal.CurrentDailyCode & "'")
            If Not copyRows Is Nothing Then
                For Each bRow In copyRows

                    oRow = dsOrder.Tables("OpenOrders").NewRow
                    CopyOneRowToAnotherOpenOrders222(bRow, oRow) '  will copy db value
                    dsOrder.Tables("OpenOrders").Rows.Add(oRow)
                Next
            Else

            End If
            '   must reinitialize our table (this tell us all this info is old)
            dsOrder.Tables("OpenOrders").AcceptChanges()
        Catch ex As Exception
            '   if copying fails    .... we revert to an empty table  ???
            dsOrder.Tables("OpenOrders").Rows.Clear()

        End Try

    End Sub

    Friend Sub UpdatePaymentsAndCredits()

        Try
            If typeProgram = "Online_Demo" Then
                dsOrderDemo.Merge(dsOrder.Tables("PaymentsAndCredits"), False, MissingSchemaAction.Add)
                dsOrder.Tables("PaymentsAndCredits").Clear()
            Else
                sql.cn.Open()
                sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                sql.SqlDataAdapterPayments.Update(dsOrder.Tables("PaymentsAndCredits"))
                sql.cn.Close()
            End If

        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
            If Err.Number = "5" Then
                ServerJustWentDown()
            End If
        End Try

        '     TerminalAddPayments()
        dsOrder.Tables("PaymentsAndCredits").AcceptChanges()

    End Sub

    Private Sub TerminalAddPayments222()
        Dim oRow As DataRow
        Dim bRow As DataRow
        Dim dbUP As Integer
        Dim modifiedPayment As Boolean

        For Each oRow In dsOrder.Tables("PaymentsAndCredits").Rows
            If oRow.RowState = DataRowState.Added Then
                AddNewTerminalPayment222(oRow, bRow)

            ElseIf oRow.RowState = DataRowState.Modified Then

                If oRow("PaymentsAndCreditsID") Is DBNull.Value Then
                    '   meaning this is new data not on SQL Server
                    '   this row was deleted in terminal when copied
                    '   we should never be here... all non ID's are new rows    ????
                    '     dbUP = 0
                    '             AddNewTerminalPayment(oRow, bRow)
                    '           oRow("PaymentsAndCreditsID") = -1
                    '          modifiedPayment = True

                Else
                    '   is old data we just changed
                    If mainServerConnected = True Then
                        dbUP = 1
                    Else
                        dbUP = 2
                    End If

                    For Each bRow In dsBackup.Tables("PaymentsAndCreditsTerminal").Rows
                        If bRow("PaymentsAndCreditsID") = oRow("PaymentsAndCreditsID") Then
                            CopyOneRowToAnotherPaymentsAndCredits(oRow, bRow, dbUP) '  will place db value
                        End If
                    Next

                End If
            End If
        Next

        '        If modifiedPayment = True Then
        '       DeleteNonSQLRecordedRowsPayments(dsOrder.Tables("PaymentsAndCredits"))
        '      End If

    End Sub

    Private Sub DeleteNonSQLRecordedRowsPayments(ByRef dTable As DataTable)
        Dim dRows() As DataRow
        Dim i As Integer

        dRows = dTable.Select("PaymentsAndCreditsID IS NULL")

        For i = 0 To dRows.Length - 1
            dRows(i).Delete()
        Next

    End Sub

    Private Sub AddNewTerminalPayment222(ByRef oRow As DataRow, ByRef bRow As DataRow)
        Dim dbUP As Integer

        If mainServerConnected = True Then
            dbUP = 1
        Else
            dbUP = 0
        End If

        bRow = dsBackup.Tables("PaymentsAndCreditsTerminal").NewRow
        CopyOneRowToAnotherPaymentsAndCredits(oRow, bRow, dbUP) '  will place db value
        dsBackup.Tables("PaymentsAndCreditsTerminal").Rows.Add(bRow)

    End Sub

    Friend Sub UpdateAvailTablesData()

        '   we don't come here unless the db is UP

        '   *** we need to add conflict resolution
        'we we come here after we just came UP
        '
        '  "CurrenTables" below not used
        '     Try
        '    sql.cn.Open()
        '    sql.SqlDataAdapterCurrentTables.Update(dsOrder.Tables("CurrentTables"))
        '    sql.cn.Close()
        ''
        '        dsOrder.Tables("CurrentTables").AcceptChanges()
        '        Catch ex As Exception
        '            MsgBox(ex.Message)
        '            CloseConnection()
        '           ServerJustWentDown()
        '       End Try
    End Sub


    '   **************************
    '   Closed Tables



    Private Sub PopulateClosedTabsAndTablesWhenDown222()
        ' currently can't do because we can not poplate Close Tables for all terminals
        '   when we do we have to create a closedTableDataSet ByEmployee
        Exit Sub

        Dim bRow As DataRow
        Dim oRow As DataRow
        Dim copyRows() As DataRow

        Try
            '   the below would have to use a "ClosedTablesTerminal"
            copyRows = dsBackup.Tables("AvailTablesTerminal").Select("LastStatus < 2")
            If Not copyRows Is Nothing Then
                For Each bRow In copyRows

                    oRow = dsOrder.Tables("ClosedTables").NewRow
                    CopyOneRowToAnotherAvailTabsAndTables222(bRow, oRow)
                    dsOrder.Tables("ClosedTables").Rows.Add(oRow)
                Next
            Else

            End If
            '   the below would have to use a "ClosedTablesTerminal"
            copyRows = dsBackup.Tables("AvailTabsTerminal").Select("LastStatus < 2")
            If Not copyRows Is Nothing Then
                For Each bRow In copyRows

                    oRow = dsOrder.Tables("ClosedTabs").NewRow
                    CopyOneRowToAnotherAvailTabsAndTables222(bRow, oRow)
                    dsOrder.Tables("ClosedTabs").Rows.Add(oRow)
                Next
            Else

            End If
            '   must reinitialize our table (this tell us all this info is old)
            dsOrder.Tables("ClosedTables").AcceptChanges()
            dsOrder.Tables("ClosedTabs").AcceptChanges()
        Catch ex As Exception
            '   if copying fails    .... we revert to an empty table  ???
            '  don't need this here, incomplete info is ok
        End Try

    End Sub

    '   ****************
    '   Daily's

    Friend Sub DetermineOpenBusiness()

        If typeProgram = "Online_Demo" Then
            Exit Sub
        End If

        sql.SqlSelectCommandOpenBusiness.Parameters("@LocationID").Value = companyInfo.LocationID

        Try
            dsOrder.Tables("OpenBusiness").Rows.Clear()
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlDataAdapterOpenBusiness.Fill(dsOrder.Tables("OpenBusiness"))
            sql.cn.Close()

        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
            If Err.Number = "5" Then
                ServerJustWentDown()
            End If
        End Try

    End Sub

    Friend Sub DetermineOpenCashDrawer(ByVal dc As Int64)

        If typeProgram = "Online_Demo" Then
            Exit Sub
            Dim filterString As String
            Dim NotfilterString As String
            filterString = "DailyCode = " & dc
            NotfilterString = "NOT DailyCode = " & dc

            Demo_FilterDemoDataTabble(dsOrderDemo.Tables("OpenBusiness"), dsOrder.Tables("OpenBusiness"), filterString, NotfilterString)
            Exit Sub
        End If

        '   dc = 26
        sql.SqlSelectCommandTermsOpen.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandTermsOpen.Parameters("@DailyCode").Value = dc   'currentTerminal.CurrentDailyCode
        '      sql.SqlSelectCommandTermsOpen.Parameters("@TerminalsPrimaryKey").Value = currentTerminal.TermPrimaryKey

        'in the middle of try, catch
        dsOrder.Tables("TermsOpen").Rows.Clear()
        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlTermsOpen.Fill(dsOrder.Tables("TermsOpen"))
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
        End Try


    End Sub

    Friend Sub DetermineCashTransactions(ByVal activeTerminalsOpenID As Int64)

        dsOrder.Tables("CashIn").Rows.Clear()
        dsOrder.Tables("CashOut").Rows.Clear()

        If typeProgram = "Online_Demo" Then
            Dim filterString As String

            filterString = "PaymentTypeID = 1"
            Demo_FilterDontDelete(dsOrderDemo.Tables("PaymentsAndCredits"), dsOrder.Tables("CashIn"), filterString) ', NotfilterString)

            filterString = "PaymentTypeID = -3"
            Demo_FilterDontDelete(dsOrderDemo.Tables("PaymentsAndCredits"), dsOrder.Tables("CashOut"), filterString) ', NotfilterString)

            MsgBox(dsOrder.Tables("CashIn").Rows(0)("PaymentAmount"))
            MsgBox(dsOrder.Tables("CashOut").Rows(0)("PaymentAmount"))

            Exit Sub
        End If

        sql.SqlSelectCommandTermsCashTransactions.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandTermsCashTransactions.Parameters("@PaymentTypeID").Value = 1
        sql.SqlSelectCommandTermsCashTransactions.Parameters("@TerminalsOpenID").Value = activeTerminalsOpenID

        'in the middle of try, catch
        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        sql.SqlTermsCashTransactions.Fill(dsOrder.Tables("CashIn"))
        sql.SqlSelectCommandTermsCashTransactions.Parameters("@PaymentTypeID").Value = -3
        sql.SqlTermsCashTransactions.Fill(dsOrder.Tables("CashOut"))
        sql.cn.Close()

    End Sub

    Friend Function ReadCashOutData(ByRef cmd As SqlClient.SqlCommand, ByVal cashType As String) ', ByRef cmd As SqlClient.SqlCommand)

        Dim decimalAmount As Decimal
        Dim dtr As SqlClient.SqlDataReader

        dtr = cmd.ExecuteReader

        '    If dtr.HasRows Then 'dtr.HasRows Then
        While dtr.Read()
            Select Case cashType
                Case Is = "Sales"
                    decimalAmount += dtr("PaymentAmount")
                    decimalAmount += dtr("Surcharge")
                Case Is = "Tip"
                    decimalAmount += dtr("Tip")
            End Select
        End While
        '    Else
        '      dtr.Close()
        '     Return 0
        '    Exit Function
        '   End If


        dtr.Close()

        Return decimalAmount

    End Function

    Friend Sub StartDailyBusinessClose(ByVal empID As Integer, ByVal dc As Int64)

        Dim oRow As DataRow
        Dim newRM As RawMaterial

        UpdateDailyBusinessClose(empID, dc)

        Exit Sub
        '222
        'currently below does nothing
        ' was meant to do adjustments in InvFoodCurrent
        ' i think we now just do when we do Physical Inventory
        GenerateOrderTables.TempConnectToPhoenix()
        '******* this is just for testing, later go local

        If currentRawMaterials.Count = 0 Then
            PopluateRawMatLast()
        End If

        For Each oRow In dsInventory.Tables("RawMatLast").Rows

            newRM.RawItemID = oRow("RawItemID")
            newRM.LastRecipeQuantity = oRow("RecipeQuantity")
            newRM.LastUnitCost = oRow("UnitCost")
            currentRawMaterials.Add(newRM)
        Next

        For Each newRM In currentRawMaterials
            'determine adjustments InvFoodCurrent View
        Next

        GenerateOrderTables.ConnectBackFromTempDatabase()

    End Sub

    Friend Sub PopluateRawMatLast()

        dsInventory.Tables("RawMatLast").Rows.Clear()

        sql.SqlSelectCommandRawMatLast.Parameters("@CompanyID").Value = companyInfo.CompanyID
        sql.SqlSelectCommandRawMatLast.Parameters("@LocationID").Value = companyInfo.LocationID 'customLocationString

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlRawMatLast.Fill(dsInventory.Tables("RawMatLast"))
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

    End Sub
    Friend Sub TransactionLoadInventory()


    End Sub

    Friend Sub OpenCashDrawer(ByVal openAmount As Decimal, ByVal termPrimaryKey As Integer)

        If typeProgram = "Online_Demo" Then
            demoCashOpen = openAmount 'NumberPadMedium1.NumberTotal
            Exit Sub
        End If

        Dim oRow As DataRow = dsOrder.Tables("TermsOpen").NewRow

        oRow("CompanyID") = companyInfo.CompanyID
        oRow("LocationID") = companyInfo.LocationID
        oRow("DailyCode") = currentTerminal.CurrentDailyCode
        oRow("TerminalsPrimaryKey") = termPrimaryKey
        If actingManager Is Nothing Then
            oRow("OpenBy") = 0
        Else
            oRow("OpenBy") = actingManager.EmployeeID
        End If
        oRow("OpenTime") = Now
        oRow("OpenCash") = openAmount 'NumberPadMedium1.NumberTotal
        oRow("CloseBy") = DBNull.Value
        oRow("CloseTime") = DBNull.Value
        oRow("CloseCash") = DBNull.Value
        oRow("CashIn") = DBNull.Value
        oRow("CashOut") = DBNull.Value
        oRow("OverShort") = DBNull.Value
        oRow("ReasonShort") = DBNull.Value

        oRow("TerminalsOpenID") = InsertNewTerminalsOpenData(oRow)
        currentTerminal.TerminalsOpenID = oRow("TerminalsOpenID")

        Try
            UpdateTermsOpen()
            dsOrder.Tables("TermsOpen").AcceptChanges()

        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Function InsertNewTerminalsOpenData(ByRef oRow As DataRow) As Int64

        Dim termOpenID As Int64

        sql.SqlInsertCommandTermsOpen.Parameters("@CompanyID").Value = oRow("CompanyID")
        sql.SqlInsertCommandTermsOpen.Parameters("@LocationID").Value = oRow("LocationID")
        sql.SqlInsertCommandTermsOpen.Parameters("@DailyCode").Value = oRow("DailyCode")
        sql.SqlInsertCommandTermsOpen.Parameters("@TerminalsPrimaryKey").Value = oRow("TerminalsPrimaryKey")
        sql.SqlInsertCommandTermsOpen.Parameters("@OpenBy").Value = oRow("OpenBy")
        sql.SqlInsertCommandTermsOpen.Parameters("@OpenTime").Value = oRow("OpenTime")
        sql.SqlInsertCommandTermsOpen.Parameters("@OpenCash").Value = oRow("OpenCash")
        sql.SqlInsertCommandTermsOpen.Parameters("@CloseBy").Value = oRow("CloseBy")
        sql.SqlInsertCommandTermsOpen.Parameters("@CloseTime").Value = oRow("CloseTime")
        sql.SqlInsertCommandTermsOpen.Parameters("@CloseCash").Value = oRow("CloseCash")
        sql.SqlInsertCommandTermsOpen.Parameters("@CashIn").Value = oRow("CashIn")
        sql.SqlInsertCommandTermsOpen.Parameters("@CashOut").Value = oRow("CashOut")
        sql.SqlInsertCommandTermsOpen.Parameters("@OverShort").Value = oRow("OverShort")
        sql.SqlInsertCommandTermsOpen.Parameters("@ReasonShort").Value = oRow("ReasonShort")

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            termOpenID = CType(sql.SqlInsertCommandTermsOpen.ExecuteScalar, Int64)
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

        Return termOpenID

    End Function

    Friend Sub UpdateTermsOpen()

        'in the middle of try, catch
        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        sql.SqlTermsOpen.Update(dsOrder.Tables("TermsOpen"))
        sql.cn.Close()

    End Sub

    Friend Sub UpdateDailyBusinessClose(ByVal empID As Integer, ByVal dc As Int64)

        Dim oRow As DataRow

        Try
            For Each oRow In dsOrder.Tables("OpenBusiness").Rows
                If oRow("DailyCode") = dc Then
                    oRow("EndTime") = Now
                    oRow("EmployeeClosed") = empID
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'there is a xp_mapdown_bitmap problem here
        'this is caused by replication error or something
        'workaround, we gave public permission in Master db
        '   SqlDbType.DateTime 
        '    tName.Columns.Add("EndTime", Type.GetType("System.DateTime"))
        UpdateDailyBusiness()

    End Sub

    Friend Sub SwitchToSecondaryMenu()

        Dim oRow As DataRow

        If currentTerminal.currentPrimaryMenuID = currentTerminal.primaryMenuID Then
            currentTerminal.currentPrimaryMenuID = currentTerminal.secondaryMenuID
            currentTerminal.currentSecondaryMenuID = currentTerminal.primaryMenuID
        Else
            currentTerminal.currentPrimaryMenuID = currentTerminal.primaryMenuID
            currentTerminal.currentSecondaryMenuID = currentTerminal.secondaryMenuID
        End If

        For Each oRow In dsOrder.Tables("OpenBusiness").Rows
            If oRow("DailyCode") = currentTerminal.CurrentDailyCode Then
                oRow("PrimaryMenu") = currentTerminal.currentPrimaryMenuID
                oRow("SecondaryMenu") = currentTerminal.currentSecondaryMenuID
            End If
        Next

        UpdateDailyBusiness()

    End Sub

    Friend Sub UpdateDailyBusiness()

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlDataAdapterOpenBusiness.Update(dsOrder.Tables("OpenBusiness"))
            sql.cn.Close()
            dsOrder.Tables("OpenBusiness").AcceptChanges()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

    End Sub

    Friend Sub StartNewDaily()
        Dim newDailyCode As Int64

        Try
            newDailyCode = CreateNewDaily()
            currentTerminal.CurrentDailyCode = newDailyCode

        Catch ex As Exception
            CloseConnection()
            If Err.Number = "5" Then
                ServerJustWentDown()
            End If
        End Try

    End Sub

    Friend Function CreateNewDaily()
        Dim bi As Int64

        sql.SqlInsertCommandOpenBusiness.Parameters("@CompanyID").Value = companyInfo.CompanyID
        sql.SqlInsertCommandOpenBusiness.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlInsertCommandOpenBusiness.Parameters("@StartTime").Value = Now
        sql.SqlInsertCommandOpenBusiness.Parameters("@EndTime").Value = DBNull.Value
        If actingManager Is Nothing Then
            sql.SqlInsertCommandOpenBusiness.Parameters("@EmployeeOpened").Value = 0
        Else
            sql.SqlInsertCommandOpenBusiness.Parameters("@EmployeeOpened").Value = actingManager.EmployeeID
        End If
        sql.SqlInsertCommandOpenBusiness.Parameters("@EmployeeClosed").Value = DBNull.Value
        sql.SqlInsertCommandOpenBusiness.Parameters("@PrimaryMenu").Value = currentTerminal.primaryMenuID
        sql.SqlInsertCommandOpenBusiness.Parameters("@SecondaryMenu").Value = currentTerminal.secondaryMenuID
        sql.SqlInsertCommandOpenBusiness.Parameters("@ShiftID").Value = currentTerminal.CurrentShift
        sql.SqlInsertCommandOpenBusiness.Parameters("@InvCounted").Value = 0

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            bi = CType(sql.SqlInsertCommandOpenBusiness.ExecuteScalar, Int64)
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
            ServerJustWentDown()
        End Try

        '444    currentTerminal.CurrentMenuID = currentTerminal.primaryMenuID
        '444   currentTerminal.initPrimaryMenuID = currentTerminal.primaryMenuID
        '444  currentTerminal.currentPrimaryMenuID = currentTerminal.primaryMenuID

        Return bi

    End Function

    Friend Sub InsertBatchInfo(ByRef batch As BatchInfo, ByVal closingDailyCode As Int64)
        Exit Sub

        Dim cmd As SqlClient.SqlCommand

        cmd = New SqlClient.SqlCommand("INSERT INTO Batch(CompanyID, LocationID, DailyCode, BatchNetCount, BatchNetDollar, BatchCreditPurchaseCount, BatchCreditPurchaseDollar, BatchCreditReturnCount, BatchCreditReturnDollar, BatchDebitPurchaseCount, BatchDebitPurchaseDollar, BatchDebitReturnCount, BatchDebitReturnDollar) VALUES (@CompanyID, @LocationID, @DailyCode, @BatchNetCount, @BatchNetDollar, @BatchCreditPurchaseCount, @BatchCreditPurchaseDollar, @BatchCreditReturnCount, @BatchCreditReturnDollar, @BatchDebitPurchaseCount, @BatchDebitPurchaseDollar, @BatchDebitReturnCount, @BatchDebitReturnDollar)", sql.cn)  '; SELECT  BatchID, CompanyID, LocationID, DailyCode, BatchNetCount, BatchNetDollar, BatchCreditPurchaseCount, BatchCreditPurcahaseDollar, BatchCreditReturnCount, BatchCreditReturnDollar, BatchDebitPurchaseCount, BatchDebitPurcahaseDollar, BatchDebitReturnCount, BatchDebitReturnDollar WHERE (BatchID = @@IDENTITY)", sql.cn)

        cmd.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.NChar, 6))
        cmd.Parameters("@CompanyID").Value = companyInfo.CompanyID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@LocationID", SqlDbType.NChar, 6))
        cmd.Parameters("@LocationID").Value = companyInfo.LocationID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@DailyCode", SqlDbType.BigInt, 8))
        cmd.Parameters("@DailyCode").Value = closingDailyCode
        cmd.Parameters.Add(New SqlClient.SqlParameter("@BatchNumber", SqlDbType.NVarChar, 10))
        cmd.Parameters("@BatchNumber").Value = batch.batchNumber
        cmd.Parameters.Add(New SqlClient.SqlParameter("@BatchNetCount", SqlDbType.SmallInt, 2))
        cmd.Parameters("@BatchNetCount").Value = batch.NetCount
        cmd.Parameters.Add(New SqlClient.SqlParameter("@BatchNetDollar", SqlDbType.Decimal, 9))
        cmd.Parameters("@BatchNetDollar").Value = batch.NetDollar
        cmd.Parameters.Add(New SqlClient.SqlParameter("@BatchCreditPurchaseCount", SqlDbType.SmallInt, 2))
        cmd.Parameters("@BatchCreditPurchaseCount").Value = batch.CreditPurchaseCount
        cmd.Parameters.Add(New SqlClient.SqlParameter("@BatchCreditPurchaseDollar", SqlDbType.Decimal, 9))
        cmd.Parameters("@BatchCreditPurchaseDollar").Value = batch.CreditPurchaseDollar
        cmd.Parameters.Add(New SqlClient.SqlParameter("@BatchCreditReturnCount", SqlDbType.SmallInt, 2))
        cmd.Parameters("@BatchCreditReturnCount").Value = batch.CreditReturnCount
        cmd.Parameters.Add(New SqlClient.SqlParameter("@BatchCreditReturnDollar", SqlDbType.Decimal, 9))
        cmd.Parameters("@BatchCreditReturnDollar").Value = batch.CreditReturnDollar
        cmd.Parameters.Add(New SqlClient.SqlParameter("@BatchDebitPurchaseCount", SqlDbType.SmallInt, 2))
        cmd.Parameters("@BatchDebitPurchaseCount").Value = batch.DebitPurchaseCount
        cmd.Parameters.Add(New SqlClient.SqlParameter("@BatchDebitPurchaseDollar", SqlDbType.Decimal, 9))
        cmd.Parameters("@BatchDebitPurchaseDollar").Value = batch.DebitPurchaseDollar
        cmd.Parameters.Add(New SqlClient.SqlParameter("@BatchDebitReturnCount", SqlDbType.SmallInt, 2))
        cmd.Parameters("@BatchDebitReturnCount").Value = batch.DebitReturnCount
        cmd.Parameters.Add(New SqlClient.SqlParameter("@BatchDebitReturnDollar", SqlDbType.Decimal, 9))
        cmd.Parameters("@BatchDebitReturnDollar").Value = batch.DebitReturnDollar

        Try
            GenerateOrderTables.TempConnectToPhoenix()
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            cmd.ExecuteNonQuery()
            sql.cn.Close()
            GenerateOrderTables.ConnectBackFromTempDatabase()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

    End Sub

    Friend Function CreateNewOrder(ByRef oDetail As OrderDetailInfo)
        Dim dsChangedDetail As DataTable

        Try
            '       in case there was an order delivered
            dsChangedDetail = dsOrder.Tables("OrderDetail").GetChanges

            If Not dsChangedDetail Is Nothing Then
                If typeProgram = "Online_Demo" Then
                    dsOrderDemo.Merge(dsOrder.Tables("OrderDetail"), False, MissingSchemaAction.Add)
                    dsOrder.Tables("OrderDetail").AcceptChanges()
                Else
                    sql.cn.Open()
                    sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
                    sql.SqlDataAdapterOrderDetail.Update(dsChangedDetail) '(dsOrder, "OrderDetail")
                    sql.cn.Close()
                    dsOrder.Tables("OrderDetail").AcceptChanges()
                End If
            End If

        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
            ServerJustWentDown()
        End Try

        Dim oRow As DataRow
        oRow = dsOrder.Tables("OrderDetail").NewRow

        oRow("CompanyID") = companyInfo.CompanyID
        oRow("LocationID") = companyInfo.LocationID
        oRow("DailyCode") = currentTerminal.CurrentDailyCode
        oRow("ExperienceNumber") = currentTable.ExperienceNumber
        oRow("OrderTime") = oDetail.orderTime
        oRow("OrderFilled") = DBNull.Value      'time filled
        oRow("OrderReady") = DBNull.Value       'time ready
        oRow("NumberOfDinners") = oDetail.NumDinners
        oRow("NumberOfApps") = oDetail.numApps
        oRow("NumberOfDrinks") = oDetail.numDrinks
        oRow("isMainCourse") = oDetail.isMainCourse
        oRow("TotalDollar") = oDetail.totalDollar
        oRow("AvgDollar") = DBNull.Value    'oDetail.totalDollar

        If typeProgram = "Online_Demo" Then
            oRow("OrderNumber") = demoOrderNumber
            demoOrderNumber += 1
            dsOrder.Tables("OrderDetail").Rows.Add(oRow)
            Return oRow("OrderNumber")
            Exit Function
        End If

        dsOrder.Tables("OrderDetail").Rows.Add(oRow)
        oRow("OrderNumber") = InsertNewOrder(oRow)
        dsOrder.Tables("OrderDetail").AcceptChanges()

        Return oRow("OrderNumber")

        'I tried below without saving first and accepting
        'but it adds two orders
        'if we just make oRow = InsertNewOrder, we get concurrent violation
        '      newOrderNum = InsertNewOrder(oRow)
        '     Return newOrderNum

    End Function

    Private Function InsertNewOrder(ByRef oRow As DataRow)
        Dim ordNum As Int64
        Dim cmd As SqlClient.SqlCommand

        sql.SqlInsertCommandOrderDetail.Parameters("@CompanyID").Value = companyInfo.CompanyID
        sql.SqlInsertCommandOrderDetail.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlInsertCommandOrderDetail.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
        sql.SqlInsertCommandOrderDetail.Parameters("@ExperienceNumber").Value = currentTable.ExperienceNumber
        sql.SqlInsertCommandOrderDetail.Parameters("@OrderTime").Value = oRow("OrderTime")
        sql.SqlInsertCommandOrderDetail.Parameters("@OrderFilled").Value = oRow("OrderFilled")
        sql.SqlInsertCommandOrderDetail.Parameters("@OrderReady").Value = oRow("OrderReady")
        sql.SqlInsertCommandOrderDetail.Parameters("@NumberOfDinners").Value = oRow("NumberOfDinners")
        sql.SqlInsertCommandOrderDetail.Parameters("@NumberOfApps").Value = oRow("NumberOfApps")
        sql.SqlInsertCommandOrderDetail.Parameters("@NumberOfDrinks").Value = oRow("NumberOfDrinks")
        sql.SqlInsertCommandOrderDetail.Parameters("@isMainCourse").Value = oRow("isMainCourse")
        sql.SqlInsertCommandOrderDetail.Parameters("@TotalDollar").Value = oRow("TotalDollar")
        sql.SqlInsertCommandOrderDetail.Parameters("@AvgDollar").Value = oRow("AvgDollar")

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            ordNum = CType(sql.SqlInsertCommandOrderDetail.ExecuteScalar, Int64)
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)

        End Try

        Return ordNum

    End Function

    Friend Sub VerifyTabInfoDataLength(ByRef currenttabinfo As TabInfo)

        With currenttabinfo

            If Not .AccountNumber Is Nothing Then
                If .AccountNumber.Length > 10 Then
                    .AccountNumber = .AccountNumber.Substring(0, 10)
                End If
            End If
            If Not .AccountPhone Is Nothing Then
                If .AccountPhone.Length > 10 Then
                    .AccountPhone = .AccountPhone.Substring(0, 10)
                End If
            End If
            If Not .LastName Is Nothing Then
                If .LastName.Length > 20 Then
                    .LastName = .LastName.Substring(0, 20)
                End If
            End If
            If Not .FirstName Is Nothing Then
                If .FirstName.Length > 10 Then
                    .FirstName = .FirstName.Substring(0, 10)
                End If
            End If
            If Not .MiddleName Is Nothing Then
                If .MiddleName.Length > 10 Then
                    .MiddleName = .MiddleName.Substring(0, 10)
                End If
            End If
            If Not .NickName Is Nothing Then
                If .NickName.Length > 20 Then
                    .NickName = .NickName.Substring(0, 20)
                End If
            End If
            If Not .Address1 Is Nothing Then
                If .Address1.Length > 50 Then
                    .Address1 = .Address1.Substring(0, 50)
                End If
            End If
            If Not .Address2 Is Nothing Then
                If .Address2.Length > 50 Then
                    .Address2 = .Address2.Substring(0, 50)
                End If
            End If
            If Not .City Is Nothing Then
                If .City.Length > 15 Then
                    .City = .City.Substring(0, 15)
                End If
            End If
            If Not .State Is Nothing Then
                If .State.Length > 15 Then
                    .State = .State.Substring(0, 15)
                End If
            End If
            If Not .PostalCode Is Nothing Then
                If .PostalCode.Length > 10 Then
                    .PostalCode = .PostalCode.Substring(0, 10)
                End If
            End If
            If Not .Phone1 Is Nothing Then
                If .Phone1.Length > 24 Then
                    .Phone1 = .Phone1.Substring(0, 24)
                End If
            End If
            If Not .Ext1 Is Nothing Then
                If .Ext1.Length > 4 Then
                    .Ext1 = .Ext1.Substring(0, 4)
                End If
            End If
            If Not .Phone2 Is Nothing Then
                If .Phone2.Length > 24 Then
                    .Phone2 = .Phone2.Substring(0, 24)
                End If
            End If
            If Not .Ext2 Is Nothing Then
                If .Ext2.Length > 4 Then
                    .Ext2 = .Ext2.Substring(0, 4)
                End If
            End If
            If Not .CrossRoads Is Nothing Then
                If .CrossRoads.Length > 50 Then
                    .CrossRoads = .CrossRoads.Substring(0, 50)
                End If
            End If

            If Not .SpecialInstructions Is Nothing Then
                If .SpecialInstructions.Length > 255 Then
                    .SpecialInstructions = .SpecialInstructions.Substring(0, 255)
                End If
            End If

        End With

    End Sub
    Friend Function CreateNewTabInfoData(ByVal currentTabInfo As TabInfo, ByVal startInSearch As String)

        Dim dsChangedDetail As DataTable
        dsChangedDetail = dsCustomer.Tables("TabDirectorySearch").GetChanges
        '       in case there was an order delivered
        If Not dsChangedDetail Is Nothing Then
            UpdateTabInfo(startInSearch)
        End If

        VerifyTabInfoDataLength(currentTabInfo)

        Dim oRow As DataRow
        oRow = dsCustomer.Tables("TabDirectorySearch").NewRow

        oRow("CompanyID") = companyInfo.CompanyID
        oRow("LocationID") = companyInfo.LocationID
        If Not currentTabInfo.AccountNumber Is Nothing Then
            If currentTabInfo.AccountNumber.Length = 0 Then
                oRow("AccountNumber") = DBNull.Value
            Else
                oRow("AccountNumber") = currentTabInfo.AccountNumber
            End If
        Else
            oRow("AccountNumber") = DBNull.Value
        End If
        If Not currentTabInfo.AccountPhone Is Nothing Then
            If currentTabInfo.AccountPhone.Length = 0 Then
                oRow("AccountPhone") = DBNull.Value
            Else
                oRow("AccountPhone") = currentTabInfo.AccountPhone
            End If
        Else
            oRow("AccountPhone") = DBNull.Value
        End If

        '  MsgBox(currentTabInfo.LastName)
        oRow("LastName") = currentTabInfo.LastName
        oRow("FirstName") = currentTabInfo.FirstName
        oRow("MiddleName") = currentTabInfo.MiddleName
        oRow("NickName") = currentTabInfo.NickName
        oRow("Address1") = currentTabInfo.Address1
        oRow("Address2") = currentTabInfo.Address2
        oRow("City") = currentTabInfo.City
        oRow("State") = currentTabInfo.State
        oRow("PostalCode") = currentTabInfo.PostalCode
        oRow("Country") = currentTabInfo.Country
        oRow("Phone1") = currentTabInfo.Phone1
        oRow("Ext1") = currentTabInfo.Ext1
        oRow("Phone2") = currentTabInfo.Phone2
        oRow("Ext2") = currentTabInfo.Ext2
        oRow("DeliveryZone") = 0 'currentTabInfo.DeliverZone
        oRow("CrossRoads") = currentTabInfo.CrossRoads
        oRow("SpecialInstructions") = currentTabInfo.SpecialInstructions
        oRow("DoNotDeliver") = currentTabInfo.DoNotDeliver
        oRow("VIP") = currentTabInfo.VIP
        oRow("UpdatedDate") = currentTabInfo.UpdatedDate
        oRow("UpdatedByEmployee") = currentTabInfo.UpdatedByEmployee
        oRow("Active") = currentTabInfo.Active
        oRow("OpenChar1") = currentTabInfo.Email

        If typeProgram = "Online_Demo" Then
            oRow("TabID") = demoTabID
            demoTabID += 1
            dsCustomer.Tables("TabDirectorySearch").Rows.Add(oRow)
            dsCustomerDemo.Merge(dsCustomer.Tables("TabDirectorySearch"), False, MissingSchemaAction.Add)
            dsCustomer.Tables("TabDirectorySearch").AcceptChanges()
            Return oRow("TabID")
            Exit Function
        End If

        '*************
        '   testing below, when Insert Fails
        ' if works - need to replicate for all Inserts
        dsCustomer.Tables("TabDirectorySearch").Rows.Add(oRow)
        oRow("TabID") = InsertNewTabInfo(oRow)
        If oRow("TabID") = -1 Then
            dsCustomer.Tables("TabDirectorySearch").RejectChanges()
        Else
            dsCustomer.Tables("TabDirectorySearch").AcceptChanges()
        End If

        Return oRow("TabID")

    End Function

    Private Function InsertNewTabInfo(ByRef oRow As DataRow)
        Dim tabInfoID As Int64
        Dim cmd As SqlClient.SqlCommand

        sql.SqlInsertCommandTabStorePhone.Parameters("@CompanyID").Value = companyInfo.CompanyID
        sql.SqlInsertCommandTabStorePhone.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlInsertCommandTabStorePhone.Parameters("@AccountNumber").Value = oRow("AccountNumber")
        sql.SqlInsertCommandTabStorePhone.Parameters("@AccountPhone").Value = oRow("AccountPhone")
        sql.SqlInsertCommandTabStorePhone.Parameters("@LastName").Value = oRow("LastName")
        sql.SqlInsertCommandTabStorePhone.Parameters("@FirstName").Value = oRow("FirstName")
        sql.SqlInsertCommandTabStorePhone.Parameters("@MiddleName").Value = oRow("MiddleName")
        sql.SqlInsertCommandTabStorePhone.Parameters("@NickName").Value = oRow("NickName")
        sql.SqlInsertCommandTabStorePhone.Parameters("@Address1").Value = oRow("Address1")
        sql.SqlInsertCommandTabStorePhone.Parameters("@Address2").Value = oRow("Address2")
        sql.SqlInsertCommandTabStorePhone.Parameters("@City").Value = oRow("City")
        sql.SqlInsertCommandTabStorePhone.Parameters("@State").Value = oRow("State")
        sql.SqlInsertCommandTabStorePhone.Parameters("@PostalCode").Value = oRow("PostalCode")
        sql.SqlInsertCommandTabStorePhone.Parameters("@Country").Value = oRow("Country")
        sql.SqlInsertCommandTabStorePhone.Parameters("@Phone1").Value = oRow("Phone1")
        sql.SqlInsertCommandTabStorePhone.Parameters("@Ext1").Value = oRow("Ext1")
        sql.SqlInsertCommandTabStorePhone.Parameters("@Phone2").Value = oRow("Phone2")
        sql.SqlInsertCommandTabStorePhone.Parameters("@Ext2").Value = oRow("Ext2")
        sql.SqlInsertCommandTabStorePhone.Parameters("@DeliveryZone").Value = oRow("DeliveryZone")
        sql.SqlInsertCommandTabStorePhone.Parameters("@CrossRoads").Value = oRow("CrossRoads")
        sql.SqlInsertCommandTabStorePhone.Parameters("@SpecialInstructions").Value = oRow("SpecialInstructions")
        sql.SqlInsertCommandTabStorePhone.Parameters("@DoNotDeliver").Value = oRow("DoNotDeliver")
        sql.SqlInsertCommandTabStorePhone.Parameters("@VIP").Value = oRow("VIP")
        sql.SqlInsertCommandTabStorePhone.Parameters("@UpdatedDate").Value = oRow("UpdatedDate")
        sql.SqlInsertCommandTabStorePhone.Parameters("@UpdatedByEmployee").Value = oRow("UpdatedByEmployee")
        sql.SqlInsertCommandTabStorePhone.Parameters("@Active").Value = oRow("Active")
        sql.SqlInsertCommandTabStorePhone.Parameters("@OpenChar1").Value = oRow("OpenChar1")

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            tabInfoID = CType(sql.SqlInsertCommandTabStorePhone.ExecuteScalar, Int64)
            sql.cn.Close()
            Return tabInfoID
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
            Return -1
        End Try

    End Function

    Friend Sub UpdateTabInfo(ByVal startInSearch As String)

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            If startInSearch = "Account" Then
                sql.SqlTabStoreAccountLocation.Update(dsCustomer.Tables("TabDirectorySearch"))
            ElseIf startInSearch = "Phone" Then
                sql.SqlTabStorePhoneLocation.Update(dsCustomer.Tables("TabDirectorySearch"))
            ElseIf startInSearch = "TabID" Then
                sql.SqlTabStoreTabIDLocation.Update(dsCustomer.Tables("TabDirectorySearch"))
            End If
            sql.cn.Close()

        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)

        End Try

        dsCustomer.Tables("TabDirectorySearch").AcceptChanges()

    End Sub

    Friend Sub PopulateSearchPhone(ByVal searchCriteriaString As String) ', ByVal searchTabID As Int64)


        '     MsgBox(dsCustomer.Tables("TabDirectorySearch").Rows.Count)
        '    MsgBox(dsCustomerDemo.Tables("TabDirectorySearch").Rows.Count)
        '     MsgBox(dsCustomer.Tables("TabDirectorySearch").Rows(0)("TabID"))
        '    MsgBox(dsCustomerDemo.Tables("TabDirectorySearch").Rows(0)("TabID"))

        dsCustomer.Tables("TabDirectorySearch").Rows.Clear()

        If typeProgram = "Online_Demo" Then
            If Not searchCriteriaString = "BlankSearch" Then
                Dim filterString As String
                filterString = "AccountPhone = " & searchCriteriaString
                Demo_FilterDontDelete(dsCustomerDemo.Tables("TabDirectorySearch"), dsCustomer.Tables("TabDirectorySearch"), filterString)
            End If
            Exit Sub
        End If

        sql.SqlSelectCommandTabStorePhone.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandTabStorePhone.Parameters("@AccountPhone").Value = searchCriteriaString
        '   sql.SqlSelectCommandTabStorePhone.Parameters("@TabID").Value = searchTabID
        '  sql.SqlSelectCommandTabStorePhone.Parameters("@AccountNumber").Value = "****----****----"

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlTabStorePhoneLocation.Fill(dsCustomer.Tables("TabDirectorySearch"))
            'ccc       dsCustomer.WriteXml("CustomerData.xml", XmlWriteMode.WriteSchema)
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try


    End Sub

    Friend Function CreateAccountNumber(ByRef newPayment As DataSet_Builder.Payment) 'ByVal lname As String, ByVal anumber As String)

        'this is for TabOverview, Tab account number

        Dim spiderAcct As String
        Dim acctNumLength As Integer

        If newPayment.PaymentTypeName = "MPS Gift" Then
            If newPayment.AccountNumber > 7 Then
                spiderAcct = "MPS" + newPayment.AccountNumber.Substring(newPayment.AccountNumber.Length - 7, 7)
            Else
                spiderAcct = "MPS" + newPayment.AccountNumber
            End If
        Else
            'credit cards
            Select Case newPayment.LastName.Length
                'this takes first 5 didgits of last name
                Case 0
                    spiderAcct = companyInfo.CompanyID.Substring(0, 5) '"xxxx"
                Case 1
                    spiderAcct = newPayment.LastName.Substring(0, 1) + "xxxx"
                Case 2
                    spiderAcct = newPayment.LastName.Substring(0, 2) + "xxx"
                Case 3
                    spiderAcct = newPayment.LastName.Substring(0, 3) + "xx"
                Case 4
                    spiderAcct = newPayment.LastName.Substring(0, 4) + "x"
                Case Is > 4
                    spiderAcct = newPayment.LastName.Substring(0, 5)
            End Select

            acctNumLength = newPayment.AccountNumber.Length

            If acctNumLength < 4 Then
                spiderAcct = spiderAcct + newPayment.AccountNumber
            Else
                'this takes last 4 digits of account number
                spiderAcct = spiderAcct + newPayment.AccountNumber.Substring(acctNumLength - 4, 4)
            End If
        End If

        Return spiderAcct

    End Function

    Friend Sub PopulateSearchAccount(ByVal searchCriteriaString As String) ', ByVal searchTabID As Int64)
        '       If searchCriteriaString = "****----****----" Then
        '      filterString = "TabID = " & searchTabID
        '     Else
        '        filterString = "AccountPhone = " & searchCriteriaString
        '   End If

        dsCustomer.Tables("TabDirectorySearch").Rows.Clear()

        If typeProgram = "Online_Demo" Then
            Dim filterString As String
            filterString = "AccountNumber = " & searchCriteriaString
            Demo_FilterDontDelete(dsCustomerDemo.Tables("TabDirectorySearch"), dsCustomer.Tables("TabDirectorySearch"), filterString)
            Exit Sub
        End If

        sql.SqlSelectCommandAccountLocation.Parameters("@LocationID").Value = companyInfo.LocationID
        '     sql.SqlSelectCommandTabStorePhone.Parameters("@AccountPhone").Value = "****----****----"
        '    sql.SqlSelectCommandTabStorePhone.Parameters("@TabID").Value = searchTabID
        sql.SqlSelectCommandAccountLocation.Parameters("@AccountNumber").Value = searchCriteriaString

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlTabStoreAccountLocation.Fill(dsCustomer.Tables("TabDirectorySearch"))
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

    End Sub

    Friend Sub PopulateSearchTabID(ByVal searchCriteriaString As String) ', ByVal searchTabID As Int64)

        dsCustomer.Tables("TabDirectorySearch").Rows.Clear()

        If typeProgram = "Online_Demo" Then
            Dim filterString As String
            filterString = "TabID = " & searchCriteriaString
            Demo_FilterDontDelete(dsCustomerDemo.Tables("TabDirectorySearch"), dsCustomer.Tables("TabDirectorySearch"), filterString)
            Exit Sub
        End If

        sql.SqlSelectCommandTabStoreTabIDLocation.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlSelectCommandTabStoreTabIDLocation.Parameters("@TabID").Value = searchCriteriaString

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlTabStoreTabIDLocation.Fill(dsCustomer.Tables("TabDirectorySearch"))
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

    End Sub

    Friend Sub CreateTabAcctPlaceInExperience(ByRef newPayment As DataSet_Builder.Payment)
        ' ****   must have experience created before this step

        Dim oRow As DataRow
        Dim changedTabID As Boolean

        If Not currentTable.TruncatedExpNum Is Nothing Then
            'i think this is only for mercury Gift
            newPayment.RefNo = (currentTable.TruncatedExpNum).ToString
        Else : newPayment.RefNo = 0
        End If
       
        'placing account infomation into experience
        'moved to readAuth and MWE
        '444 newPayment.SpiderAcct = CreateAccountNumber(newPayment) '.LastName, newPayment.AccountNumber)

        PopulateSearchAccount(newPayment.SpiderAcct) ', -123456789)

        If dsCustomer.Tables("TabDirectorySearch").Rows.Count = 0 Then
            'must create the account

            Dim currentTabInfo As New TabInfo
            With currentTabInfo
                .AccountNumber = newPayment.SpiderAcct   'AccountNumber
                .LastName = newPayment.LastName
                .FirstName = newPayment.FirstName
                .DeliverZone = 0  '****** needs to be INT **** CInt(Me.lblNewTabDeliveryZone.Text)
                .DoNotDeliver = False
                .VIP = False
                .UpdatedDate = Now
                .UpdatedByEmployee = currentTable.EmployeeID
                .Active = True
            End With
            If Not currentTable.TabID > 0 Then
                currentTable.TabID = CreateNewTabInfoData(currentTabInfo, "TabID")
                TestToReplaceTabName(newPayment.Name)
                newPayment.TabID = currentTable.TabID
                changedTabID = True
            Else
                newPayment.TabID = CreateNewTabInfoData(currentTabInfo, "TabID")
                changedTabID = True
            End If

        ElseIf dsCustomer.Tables("TabDirectorySearch").Rows.Count = 1 Then

            If Not currentTable.TabID = -888 Or Not currentTable.TabID = -777 Then
                ' this is group tabs                'return

            End If

            '*** we are only adjusting Tab Info if nothing there
            ' if we swipe from Seating Tab, we will always adjust in Login.CardReadSuccessful
            ' if we swipe from Delivery, we will always adjust also in Login.TabIDTest (after saving)
            '***
            If Not currentTable.TabID > 0 Then '= dsCustomer.Tables("TabDirectorySearch").Rows(0)("TabID") Then ' > 0 Then
                currentTable.TabID = dsCustomer.Tables("TabDirectorySearch").Rows(0)("TabID")
                newPayment.TabID = currentTable.TabID
                    TestToReplaceTabName(newPayment.Name)
                changedTabID = True
            Else
                newPayment.TabID = dsCustomer.Tables("TabDirectorySearch").Rows(0)("TabID")
            End If

        ElseIf dsCustomer.Tables("TabDirectorySearch").Rows.Count > 1 Then
            '  giving credit to last account if there are mult accounts
            If Not currentTable.TabID = -888 Or Not currentTable.TabID = -777 Then
                ' this is group tabs                'return

            End If
            If Not currentTable.TabID > 0 Then
                currentTable.TabID = dsCustomer.Tables("TabDirectorySearch").Rows(dsCustomer.Tables("TabDirectorySearch").Rows.Count - 1)("TabID")
                TestToReplaceTabName(newPayment.Name)
                newPayment.TabID = currentTable.TabID
                changedTabID = True
            Else
                newPayment.TabID = dsCustomer.Tables("TabDirectorySearch").Rows(dsCustomer.Tables("TabDirectorySearch").Rows.Count - 1)("TabID")
            End If


        End If

            ' **** currently not giving tab Credit for Quick Tickets or Beth's Tabs
            If changedTabID = True Then
                If currentTerminal.TermMethod = "Quick" Then 'Or currentTable.TicketNumber > 0 Then 'currentTable.TabID = -888 Then
                    ' (ticket Number > 0 for both Quick and Beth's Tabs
                    For Each oRow In dsOrder.Tables("QuickTickets").Rows
                        If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                            oRow("TabID") = currentTable.TabID
                            oRow("TabName") = currentTable.TabName
                        End If
                    Next
                Else
                    If currentTable.IsTabNotTable = False Then
                        For Each oRow In dsOrder.Tables("AvailTables").Rows
                            If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                                oRow("TabID") = currentTable.TabID
                                oRow("TabName") = currentTable.TabName '444 not on tables    
                                Exit For
                            End If
                        Next
                        'sss          SaveAvailTabsAndTables()
                    Else
                        For Each oRow In dsOrder.Tables("AvailTabs").Rows
                            If oRow("ExperienceNumber") = currentTable.ExperienceNumber Then
                                oRow("TabID") = currentTable.TabID
                                oRow("TabName") = currentTable.TabName
                                Exit For
                            End If
                        Next
                        'sss            SaveAvailTabsAndTables()
                    End If
                End If
            End If

    End Sub


    Private Sub TestToReplaceTabName(ByVal tn As String)

        If currentTable.TabName.Length = 0 Or currentTable.TabName = "Dine In Tab" Or currentTable.TabName = "Take Out Tab" Or currentTable.TabName = "Pickup Tab" Then
            currentTable.TabName = tn
        End If
    End Sub


    Friend Function CreateNewOrderForceFree222(ByRef ffInfo As ForceFreeInfo)
        Dim bi As Int64

        Dim cmd As SqlClient.SqlCommand

        cmd = New SqlClient.SqlCommand("INSERT INTO OrderForceFree(CompanyID, LocationID, EmployeeID, DailyCode, ExperienceNumber, OpenOrderID, ForceFreeAuth, ForceFreePrice, ForceFreeTaxPrice, ForceFreeTaxID, AmountDiscount, TaxDiscount, AdjID, AdjPrice, CompID, CompPrice, VoidID, VoidPrice, PromoID, PromoPrice, TransferID, TransferPrice, TransferToEmployeeID) VALUES ( @CompanyID, @LocationID, @EmployeeID, @DailyCode, @ExperienceNumber, @OpenOrderID, @ForceFreeAuth, @ForceFreePrice, @ForceFreeTaxPrice, @ForceFreeTaxID, @AmountDiscount, @TaxDiscount, @AdjID, @AdjPrice, @CompID, @CompPrice, @VoidID, @VoidPrice, @PromoID, @PromoPrice, @TransferID, @TransferPrice, @TransferToEmployeeID); SELECT ForceFreeID, CompanyID, LocationID, EmployeeID, DailyCode, OpenOrderID, ForceFreeAuth, ForceFreePrice, ForceFreeTaxPrice, ForceFreeTaxID, CompID, CompPrice, VoidID, VoidPrice, PromoID, PromoPrice, TransferID, TransferPrice, TransferToEmployeeID FROM OrderForceFree WHERE (ForceFreeID = @@IDENTITY)", sql.cn)
        '     cmd.Parameters.Add(New SqlClient.SqlParameter("@DailyCode", SqlDbType.BigInt, 8))
        '    cmd.Parameters("@DailyCode").Value = currentTerminal.currentDailyCode
        cmd.Parameters.Add(New SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.BigInt, 8, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))

        cmd.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.NChar, 6))
        cmd.Parameters("@CompanyID").Value = companyInfo.CompanyID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@LocationID", SqlDbType.NChar, 6))
        cmd.Parameters("@LocationID").Value = companyInfo.LocationID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int, 4))
        cmd.Parameters("@EmployeeID").Value = currentTable.EmployeeID
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DailyCode", System.Data.SqlDbType.BigInt, 8))
        cmd.Parameters("@DailyCode").Value = ffInfo.DailyCode    'currentTerminal.currentDailyCode
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ExperienceNumber", System.Data.SqlDbType.BigInt, 8))
        cmd.Parameters("@ExperienceNumber").Value = ffInfo.ExpNum
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OpenOrderID", System.Data.SqlDbType.BigInt, 8))
        cmd.Parameters("@OpenOrderID").Value = ffInfo.OpenOrderID
        cmd.Parameters.Add(New SqlClient.SqlParameter("@ForceFreeAuth", SqlDbType.Int, 4))
        cmd.Parameters("@ForceFreeAuth").Value = ffInfo.AuthID
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ForceFreePrice", System.Data.SqlDbType.Decimal, 5))
        cmd.Parameters("@ForceFreePrice").Value = ffInfo.Price
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ForceFreeTaxPrice", System.Data.SqlDbType.Decimal, 5))
        cmd.Parameters("@ForceFreeTaxPrice").Value = ffInfo.TaxPrice
        cmd.Parameters.Add(New SqlClient.SqlParameter("@ForceFreeTaxID", SqlDbType.Int, 4))
        cmd.Parameters("@ForceFreeTaxID").Value = ffInfo.TaxID
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AmountDiscount", System.Data.SqlDbType.Decimal, 5))
        cmd.Parameters("@AmountDiscount").Value = ffInfo.AmountDiscount
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TaxDiscount", System.Data.SqlDbType.Decimal, 5))
        cmd.Parameters("@TaxDiscount").Value = ffInfo.TaxDicount
        cmd.Parameters.Add(New SqlClient.SqlParameter("@AdjID", SqlDbType.Int, 4))
        cmd.Parameters("@AdjID").Value = ffInfo.AdjID
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AdjPrice", System.Data.SqlDbType.Decimal, 5))
        cmd.Parameters("@AdjPrice").Value = ffInfo.AdjPrice
        cmd.Parameters.Add(New SqlClient.SqlParameter("@CompID", SqlDbType.Int, 4))
        cmd.Parameters("@CompID").Value = ffInfo.CompID
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CompPrice", System.Data.SqlDbType.Decimal, 5))
        cmd.Parameters("@CompPrice").Value = ffInfo.CompPrice
        cmd.Parameters.Add(New SqlClient.SqlParameter("@VoidID", SqlDbType.Int, 4))
        cmd.Parameters("@VoidID").Value = ffInfo.VoidID
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@VoidPrice", System.Data.SqlDbType.Decimal, 5))
        cmd.Parameters("@VoidPrice").Value = ffInfo.VoidPrice
        cmd.Parameters.Add(New SqlClient.SqlParameter("@PromoID", SqlDbType.Int, 4))
        cmd.Parameters("@PromoID").Value = ffInfo.PromoID
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PromoPrice", System.Data.SqlDbType.Decimal, 5))
        cmd.Parameters("@PromoPrice").Value = ffInfo.PromoPrice
        cmd.Parameters.Add(New SqlClient.SqlParameter("@TransferID", SqlDbType.Int, 4))
        cmd.Parameters("@TransferID").Value = ffInfo.TransferID
        cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TransferPrice", System.Data.SqlDbType.Decimal, 5))
        cmd.Parameters("@TransferPrice").Value = ffInfo.TransferPrice
        cmd.Parameters.Add(New SqlClient.SqlParameter("@TransferToEmployeeID", SqlDbType.Int, 4))
        cmd.Parameters("@TransferToEmployeeID").Value = ffInfo.TransferToEmployeeID

        '      Try
        bi = cmd.ExecuteScalar
        ffInfo.ffID = bi            'need to remove ******
        Return bi
        '     Catch ex As Exception
        '        MsgBox(ex.Message)
        '       End Try

        'we are opening and closing database before this function
        '   this is so we don't have to reopen and close for multiple items
        Try
            '         sql.cn.Open()
            '              sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            '          bi = cmd.ExecuteScalar
            '       sql.cn.Close()
        Catch ex As Exception
            '        MsgBox(ex.Message)
            '      CloseConnection()
        End Try

    End Function

    Friend Function GetDailyBusiness222()

        Dim cmd As SqlClient.SqlCommand
        Dim dtr As SqlClient.SqlDataReader

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            cmd = New SqlClient.SqlCommand("SELECT DailyCode, StartTime, EmployeeOpened, PrimaryMenu, SecondaryMenu, ShiftID FROM DailyBusinessView WHERE LocationID = '" & companyInfo.LocationID & "'", sql.cn)
            dtr = cmd.ExecuteReader
            While dtr.Read()
                currentTerminal.CurrentDailyCode = dtr("DailyCode")
                currentTerminal.primaryMenuID = dtr("PrimaryMenu")
                currentTerminal.secondaryMenuID = dtr("SecondaryMenu")
                currentTerminal.CurrentShift = dtr("ShiftID")
            End While

            dtr.Close()
            sql.cn.Close()
            If currentTerminal.CurrentDailyCode > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            If Not dtr Is Nothing Then
                If dtr.IsClosed = False Then
                    dtr.Close()
                End If
            End If
            CloseConnection()
        End Try

    End Function
    Friend Sub AddAutoSalariedEmployeesToCollection222(ByVal ci As String, ByVal li As String)
        '   ************  not using yet
        '   the problem with this is we don;t know the job code
        '   we also not sure if we want these people in working employee collection

        If ci = "000000" Then   'so we don't redo
            '         sql.SqlSelectCommandClockInJobCodes.Parameters("@CompanyID").Value = ci
            '        sql.SqlSelectCommandClockInJobCodes.Parameters("@LocationID").Value = li
            '       sql.SqlSelectCommandJobCodeInfo.Parameters("@CompanyID").Value = ci
            '      sql.SqlSelectCommandJobCodeInfo.Parameters("@LocationID").Value = li
            Try
                '         sql.cn.Open()
                '        sql.SqlDataAdapterClockInJobCodes.Fill(dsEmployee.Tables("ClockInJobCodes"))
                '       sql.SqlDataAdapterJobCodeInfo.Fill(dsEmployee.Tables("JobCodeInfo"))
                '      sql.cn.Close()
            Catch ex As Exception
                CloseConnection()
            End Try

        End If

        Dim salariedEmployees As New ArrayList
        Dim empID As Integer
        Dim newEmployee As New Employee

        '   Dim empID As String
        '       Dim passcode As Integer
        Dim sqlEmpID As String
        Dim sqlPasscode As String
        Dim opMgmtAll As Boolean
        Dim opMgmtLimit As Boolean
        '     Dim reqClockIn As Boolean
        Dim empName As String

        Dim cmd As SqlClient.SqlCommand
        Dim dtr As SqlClient.SqlDataReader

        Try
            sql.cn.Open()   'cmd should be almost the same as in clockInEmployee sub
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            cmd = New SqlClient.SqlCommand("SELECT EmployeeID, CompanyID, LocationID, EmployeeNumber, LastName, FirstName, NickName, Passcode, ReportMgmtAll, ReportMgmtLimited, OperationMgmtAll, OperationMgmtLimited, SystemMgmtAll, SystemMgmtLimited, EmployeeMgmtAll, EmployeeMgmtLimited, JobCodeID1 FROM Employee WHERE CompanyID = '" & ci & "' AND LocationID = '" & li & "' AND ClockInReq = 0 AND Terminated = 0", sql.cn)
            dtr = cmd.ExecuteReader
            While dtr.Read()

                '     If dtr.HasRows Then '    dtr.HasRows Then
                newEmployee = New Employee

                '   job code # 1 is the default for any salaried employee'  
                '   if they don't have to clock in then they can't define their position
                newEmployee.EmployeeID = dtr("EmployeeID")
                newEmployee.EmployeeNumber = dtr("EmployeeNumber")
                newEmployee.FullName = dtr("FirstName") & " " & dtr("LastName")
                newEmployee.NickName = dtr("NickName")
                If newEmployee.NickName.Length < 1 Then
                    newEmployee.NickName = dtr("FirstName")
                End If
                newEmployee.PasscodeID = dtr("Passcode")
                newEmployee.ReportMgmtAll = dtr("ReportMgmtAll")
                newEmployee.ReportMgmtLimited = dtr("ReportMgmtLimited")
                newEmployee.OperationMgmtAll = dtr("OperationMgmtAll")
                newEmployee.OperationMgmtLimited = dtr("OperationMgmtLimited")
                newEmployee.SystemMgmtAll = dtr("SystemMgmtAll")
                newEmployee.SystemMgmtLimited = dtr("SystemMgmtLimited")
                newEmployee.EmployeeMgmtAll = dtr("EmployeeMgmtAll")
                newEmployee.EmployeeMgmtLimited = dtr("EmployeeMgmtLimited")
                newEmployee.JobCodeID = dtr("JobCodeID1")
                newEmployee.ShiftID = currentTerminal.CurrentShift
                newEmployee.ClockInReq = False

                salariedEmployees.Add(newEmployee)

            End While

            dtr.Close()

            For Each newEmployee In salariedEmployees
                If Not newEmployee.JobCodeID = Nothing Then
                    FillJobCodeInfo(newEmployee, newEmployee.JobCodeID)
                End If
            Next

            sql.cn.Close()

        Catch ex As Exception
            CloseConnection()
        End Try

        For Each newEmployee In salariedEmployees
            AddEmployeeToCollections(newEmployee)
            '   we do not add to current floor personel or current management
            '   they must clock in for that
        Next

    End Sub

    Friend Function AnyOpenTables(ByVal emp As Employee)
        Dim qtCount As Integer
        PopulateTabsAndTables(emp, currentTerminal.CurrentDailyCode, False, False, Nothing)
        CreateDataViews(emp.EmployeeID, True)
        If currentTerminal.TermMethod = "Bar" Then ' Or currentTerminal.TermMethod = "Quick" Then
            'probably want "Quick" as well
            qtCount = dvQuickTickets.Count
        Else
            qtCount = 0
        End If
        '     If currentTerminal.TermMethod = "Quick" Then
        'With dvQuickTickets
        '    .Table = dsOrder.Tables("QuickTickets")
        '444      .RowFilter = "EmployeeID = " & emp.EmployeeID
        '   .Sort = "ExperienceDate ASC"
        '  End With
        ' End If
        If dvAvailTables.Count + dvTransferTables.Count + dvAvailTabs.Count + dvTransferTabs.Count + qtCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Friend Sub CreateClosedDataViews()

        dvClosedTables = New DataView
        dvClosedTabs = New DataView

        With dvClosedTables
            .Table = dsOrder.Tables("AvailTables")
            .RowFilter = "LastStatus = 9 OR LastStatus = 10" '"EmployeeID = " & empActive.EmployeeID   
            .Sort = "ExperienceDate DESC"
        End With
        With dvClosedTabs
            .Table = dsOrder.Tables("AvailTabs")
            .RowFilter = "LastStatus = 9 OR LastStatus = 10" '"EmployeeID = " & empActive.EmployeeID    
            .Sort = "ExperienceDate DESC"
        End With

    End Sub

    Friend Sub CreateAvailDataViews()
        '    MsgBox("here at CreateAvailDataViews222")

        dvAvailTables = New DataView
        dvAvailTabs = New DataView

        With dvAvailTables
            .Table = dsOrder.Tables("OpenTables")
            .RowFilter = "LastStatus < 8"
            .RowStateFilter = DataViewRowState.CurrentRows
            .Sort = "ExperienceDate DESC"
        End With

        With dvAvailTabs
            .Table = dsOrder.Tables("OpenTabs")
            .RowFilter = "LastStatus < 8"
            .RowStateFilter = DataViewRowState.CurrentRows
            .Sort = "ExperienceDate DESC"
        End With

    End Sub
    Friend Sub CreateClosingDataViews(ByVal closingCheck As Integer, ByVal filterCheck As Boolean)
        dvClosingCheck = New DataView
        dvClosingCheckPayments = New DataView
        dvUnAppliedPaymentsAndCredits = New DataView
        dvAppliedPayments = New DataView

        With dvClosingCheck
            .Table = dsOrder.Tables("OpenOrders")
            '      If filterCheck = True Then
            '         If currentTable.IsTabNotTable = False Then
            .RowFilter = "CheckNumber = '" & closingCheck & "'"
            '       Else
            '          .RowFilter = "CheckNumber = '" & closingCheck & "'"   
            '     End If
            '   End If
            '    .RowStateFilter = DataViewRowState.CurrentRows
            .Sort = "CustomerNumber, sii, si2, sin"
        End With

        With dvClosingCheckPayments
            .Table = dsOrder.Tables("PaymentsAndCredits")
            If filterCheck = True Then
                .RowFilter = "CheckNumber = '" & closingCheck & "'"
            End If
            .RowStateFilter = DataViewRowState.CurrentRows
            .Sort = "AuthCode"
        End With

        '     If dsOrder.Tables("PaymentsAndCredits").Rows.Count > 0 Then
        '   we must only apply filter if there are any rows to apply a filter
        '   otherwise when we change our filoter critria program delays
        ' not 100% sure
        With dvUnAppliedPaymentsAndCredits
            .Table = dsOrder.Tables("PaymentsAndCredits")
            If filterCheck = True Then
                .RowFilter = "Applied = False AND CheckNumber = '" & closingCheck & "'"
            Else
                .RowFilter = "Applied = False"
            End If
            .Sort = "PaymentFlag"
        End With
        '           Me.closeCheckAdjustments.grdPaymentTotals.DataSource = dvUnAppliedPaymentsAndCredits
        '    End If
        'this determines the number for invoicing

        With dvAppliedPayments
            .Table = dsOrder.Tables("PaymentsAndCredits")
            .RowFilter = "Applied = True AND PaymentFlag = 'cc'"
        End With

        '  With dvUnAppliedPaymentsAndCredits_MWE
        '    .Table = Login.readAuth_MWE.dtPaymentsAndCreditsUnauthorized_MWE
        '   .RowFilter = "Applied = False AND ExperienceNumber = '" & currentTable.ExperienceNumber & "' AND CheckNumber = '" & currentTable.CheckNumber & "'"
        '  .Sort = "PaymentFlag"
        ' End With
    End Sub

    Friend Sub DetermineTruncatedOrderNumber(ByRef oDetail As OrderDetailInfo)

        Dim trunkOrderNumber As String

        trunkOrderNumber = CType(oDetail.orderNumber, String)

        If trunkOrderNumber.Length > 6 Then
            oDetail.trunkOrderNumber = trunkOrderNumber.Substring(trunkOrderNumber.Length - 6, 6)
        Else : oDetail.trunkOrderNumber = trunkOrderNumber
        End If

    End Sub

    Friend Sub DetermineTruncatedExperienceNumber()

        Dim trunkExpNumber As String

        trunkExpNumber = currentTable.ExperienceNumber.ToString

        If trunkExpNumber.Length > 6 Then
            currentTable.TruncatedExpNum = trunkExpNumber.Substring(trunkExpNumber.Length - 6, 6)
        Else : currentTable.TruncatedExpNum = trunkExpNumber
        End If

    End Sub

    Friend Function DetermineTruncatedExperienceNumberFunction(ByVal expNum As Int64)

        Dim temp As String
        Dim trunkExpNumber As String

        temp = expNum.ToString

        If temp.Length > 6 Then
            trunkExpNumber = temp.Substring(temp.Length - 6, 6)
        Else : trunkExpNumber = temp
        End If

        Return trunkExpNumber

    End Function

    '   ***********************
    '   Credit Transactions
    '   ***********************

    Public Function GiftCardTransaction(ByRef vrow As DataRowView, ByRef newPayment As Payment, ByVal whatWereDoing As String)

        If Not companyInfo.processor = "Mercury" Then
            Return "MPS Gift Card"
        End If

        Dim authStatus As String
        'currently Gift Card using same CLASS as Credit Card
        Dim mpsPreAuth As New TStream
        Dim mpsPreAuthTransaction As New PreAuthTransactionClass
        Dim mpsAmount As New PreAuthAmountClass
        Dim mpsAccount As New AccountClass
        Dim mpsTransInfo As New TranInfoClass

        mpsPreAuthTransaction.IpPort = "9100"
        mpsPreAuthTransaction.OperatorID = companyInfo.operatorID
        mpsPreAuthTransaction.TerminalName = currentServer.FullName
        mpsPreAuthTransaction.MerchantID = "595901" 'companyInfo.merchantID
        mpsPreAuthTransaction.TranType = "PrePaid"

        Select Case whatWereDoing
            Case "Balance"
                'currently doing in DetermineGiftCardBalance
                mpsPreAuthTransaction.TranCode = "Balance"
            Case "Sale"
                mpsPreAuthTransaction.TranCode = "Sale"
            Case "Issue"
                mpsPreAuthTransaction.TerminalName = currentServer.FullName
                mpsPreAuthTransaction.TranCode = "Issue"
            Case "Return"
                mpsPreAuthTransaction.TerminalName = currentServer.FullName
                mpsPreAuthTransaction.TranCode = "Return"
            Case "VoidSale"
                mpsPreAuthTransaction.TerminalName = currentServer.FullName
                mpsPreAuthTransaction.TranCode = "VoidSale"
                mpsTransInfo.AuthCode = vrow("AuthCode")
        End Select

        If whatWereDoing = "Balance" Then
            mpsPreAuthTransaction.InvoiceNo = newPayment.RefNo
            mpsPreAuthTransaction.RefNo = newPayment.RefNo
            mpsPreAuthTransaction.Memo = companyInfo.VersionNumber

            mpsAmount.Purchase = "0.00" 'newPayment.Purchase
            If Not newPayment.Track2 Is Nothing Then
                mpsAccount.Track2 = newPayment.Track2
            Else
                mpsAccount.AcctNo = newPayment.AccountNumber
                mpsAccount.ExpDate = "0199"
            End If

            '  newPayment.pr("PreAuthAmount") = _closeAuthAmount.Authorize
            newPayment.TranCode = mpsPreAuthTransaction.TranCode

        Else
            mpsPreAuthTransaction.InvoiceNo = vrow("RefNum")
            If whatWereDoing = "VoidSale" Then
                mpsPreAuthTransaction.RefNo = vrow("AuthCode")
            Else
                mpsPreAuthTransaction.RefNo = vrow("RefNum")
            End If
            mpsPreAuthTransaction.Memo = companyInfo.VersionNumber

            If whatWereDoing = "Issue" Or whatWereDoing = "Return" Then
                'we are reversing again to send positive to Mercury
                mpsAmount.Purchase = vrow("PaymentAmount") * -1
            Else
                mpsAmount.Purchase = vrow("PaymentAmount")
            End If

            If Not vrow("Track2") Is DBNull.Value Then
                mpsAccount.Track2 = vrow("Track2")
            Else
                mpsAccount.AcctNo = vrow("AccountNumber")
                mpsAccount.ExpDate = "0199"
            End If

            '      vrow("PreAuthAmount") = _closeAuthAmount.Authorize
            vrow("TransactionCode") = mpsPreAuthTransaction.TranCode

        End If

        mpsPreAuthTransaction.Account = mpsAccount
        mpsPreAuthTransaction.Amount = mpsAmount

        If whatWereDoing = "VoidSale" Then
            mpsPreAuthTransaction.TranInfo = mpsTransInfo
        End If

        mpsPreAuth.Transaction = mpsPreAuthTransaction

        Dim output As New StringWriter(New StringBuilder)
        Dim s As New XmlSerializer(GetType(TStream))
        s.Serialize(output, mpsPreAuth)

        ' we send Gift Output to other routine
        authStatus = SendingGift(output.ToString, mpsPreAuthTransaction.TranCode, vrow, newPayment) ', orow, vRow, useVIEW)

        Return authStatus

    End Function

    Private Function SendingGift(ByVal XMLString As String, ByVal transDetails As String, ByRef vRow As DataRowView, ByRef newPayment As Payment) ', ByRef orow As DataRow, ByRef vRow As DataRowView, ByVal useVIEW As Boolean)
        Dim resp As String
        Dim authStatus As String
        Dim dataFileLocation As String
        '444     Dim sWriter1 As StreamWriter
        '444     Dim sWriter2 As StreamWriter

        dsi = New DSICLIENTXLib.DSICLientX

        Try
            '     dsi.ServerIPConfig("g1.mercurypay.com;g2.backuppay.com", 0)
            dsi.ServerIPConfig("g1.mercurydev.net", 0) 'testing only
            resp = dsi.ProcessTransaction(XMLString, 0, "", "")
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox("Could not establish connection to Payment Processor.")
            Exit Function
        End Try

        If transDetails = "Balance" Then
            '           sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendGiftBalance.txt")
            '          sWriter2 = New StreamWriter("c:\Data Files\spiderPOS\ResponseGiftBalance.txt")
        ElseIf transDetails = "Sale" Then
            '         sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendGiftSale.txt")
            '        sWriter2 = New StreamWriter("c:\Data Files\spiderPOS\ResponseGiftSale.txt")
        ElseIf transDetails = "Return" Then
            '       sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendGiftReturn.txt")
            '      sWriter2 = New StreamWriter("c:\Data Files\spiderPOS\ResponseReturn.txt")
        ElseIf transDetails = "Issue" Then
            '            sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendGiftIssue.txt")
            '           sWriter2 = New StreamWriter("c:\Data Files\spiderPOS\ResponseGiftIssue.txt")
        ElseIf transDetails = "VoidSale" Then
            '          sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendVoidSale.txt")
            '         sWriter2 = New StreamWriter("c:\Data Files\spiderPOS\ResponseVoidSale.txt")
        ElseIf transDetails = "NoNSFSale" Then
            'actually not doing now
            '        sWriter1 = New StreamWriter("c:\Data Files\spiderPOS\sendGiftNoNSFSale.txt")
            '       sWriter2 = New StreamWriter("c:\Data Files\spiderPOS\ResponseGiftNoNSFSale.txt")
        Else ' means something wrong
            Exit Function
        End If

        '       sWriter1.Write(XMLString)
        '      sWriter1.Close()
        '
        '     sWriter2.Write(resp)
        '    sWriter2.Close()

        '    MsgBox(resp)
        If transDetails = "Balance" Then
            authStatus = ParseXMLGiftMPS(transDetails, resp, vRow, newPayment)
        ElseIf transDetails = "Sale" Then
            authStatus = ParseXMLGiftMPS(transDetails, resp, vRow, newPayment)

        ElseIf transDetails = "Return" Then
            authStatus = ParseXMLGiftMPS(transDetails, resp, vRow, newPayment)

        ElseIf transDetails = "Issue" Then
            authStatus = ParseXMLGiftMPS(transDetails, resp, vRow, newPayment)
        ElseIf transDetails = "VoidSale" Then
            authStatus = ParseXMLGiftMPS(transDetails, resp, vRow, newPayment)

        ElseIf transDetails = "GiftNoNSFSale" Then
            ' not doing yet
        Else ' means something wrong
            Exit Function
        End If

        Return authStatus


    End Function

    Private Function ParseXMLGiftMPS(ByVal transDetails As String, ByVal resp As String, ByRef vRow As DataRowView, ByRef newPayment As Payment)

        Dim reader As New XmlTextReader(New StringReader(resp))
        Dim someError As Boolean
        Dim isPreAuth As Boolean
        Dim isApproved As Boolean
        Dim authStatus As String
        Dim isDeclined As Boolean
        Dim pRow As DataRow
        Dim looksLikeDup As Boolean
        Dim tempAuthCode As String
        Dim tempAcqRef As String
        Dim isAmexDcvr As Boolean
        Dim tempCardType As String

        Try
            Do Until reader.EOF = True
                reader.Read()
                reader.MoveToContent()
                If reader.NodeType = XmlNodeType.Element Then
                    '         MsgBox(reader.Name)
                    Select Case reader.Name

                        Case "DSIXReturnCode"
                            If String.Compare(reader.ReadInnerXml, "000000", True) <> 0 Then
                                '   false, do not honor case (is not case sensitive)
                                '   a zero means the same (therefore this is not the same)
                                '   there is sometype of error
                                someError = True
                            End If
                            '               MsgBox(reader.ReadInnerXml, , "OperatorID")
                        Case "CmdStatus"
                            Select Case reader.ReadInnerXml
                                Case "Approved"
                                    isApproved = True
                                    authStatus = "Approved"
                                Case "Declined"
                                    isDeclined = True

                                Case "Success"

                                Case "Error"

                            End Select

                        Case "TextResponse"

                            If Not transDetails = "Balance" Then
                                vRow("Description") = reader.ReadInnerXml.ToString
                            End If
                            Select Case reader.ReadInnerXml
                                Case "AP"
                                    '  isApproved = True
                                    authStatus = "Approved"
                                Case "Account Not Issued"
                                    authStatus = "Account Not Issued"
                                Case ""

                            End Select

                        Case "UserTraceData"

                            '                         **************************************
                            '                                 Transaction Response
                            '                         **************************************
                        Case "AuthCode"

                            If Not transDetails = "Balance" And Not transDetails = "VoidSale" Then
                                tempAuthCode = reader.ReadInnerXml.ToString
                                If isApproved = True Then
                                    '   place authcode in database
                                    vRow("AuthCode") = tempAuthCode
                                End If
                            End If

                        Case "AcqRefData"

                            If Not transDetails = "Balance" And Not transDetails = "VoidSale" Then
                                tempAcqRef = reader.ReadInnerXml.ToString
                                If isApproved = True Then
                                    '   place acqRefData in database
                                    vRow("AcqRefData") = tempAcqRef
                                End If
                            End If

                        Case "Balance"

                            If transDetails = "Balance" And Not transDetails = "VoidSale" Then
                                '  tempAcqRef = reader.ReadInnerXml.ToString
                                If isApproved = True Then
                                    newPayment.Balance = CType(reader.ReadInnerXml, Decimal)
                                End If
                            End If
                    End Select
                End If
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            If Not reader Is Nothing Then
                reader.Close()
            End If

        End Try

        Return authStatus

    End Function


    Friend Function TruncateAccountNumber(ByVal oldAccountNumber As String)

        '****************************************
        ' we will not have to do this as much (everytime just the start)
        'after we don't hold account number with Mercury

        Dim numberOfDigits As Integer
        Dim xAmount As Integer
        Dim newAccountNumber As String
        Dim i As Integer

        numberOfDigits = oldAccountNumber.Length

        '       If oldAccountNumber.Substring(0, 4) = "xxxx" Then
        'already truncated
        '     Return oldAccountNumber
        '    Exit Function
        '   End If

        Try
            If numberOfDigits < 4 Or numberOfDigits > 30 Then
                newAccountNumber = "xxxx"
            Else
                xAmount = numberOfDigits - 4

                For i = 1 To xAmount
                    newAccountNumber = newAccountNumber & "x"
                    If i = xAmount Then Exit For
                Next

                newAccountNumber = newAccountNumber & oldAccountNumber.Substring((xAmount), 4)
            End If
        Catch ex As Exception
            newAccountNumber = "xxxx"
        End Try

        Return newAccountNumber

    End Function

    Friend Sub ReadyToProcessPaywarePC(ByRef PaywarePCCharge As SIM.Charge)

        With PaywarePCCharge
            .PaymentEngine = SIM.Charge.PaymentSoftware.RiTA_PAYware
            .ClientID = companyInfo.ClientID '"100010001"
            .UserID = companyInfo.UserID '"Admin"
            .UserPW = companyInfo.UserPW '"PCBeta68$"
            .ServerID = "001"

            .IPAddress = companyInfo.IPAddress '"127.0.0.1"
            .Port = "4532"
            .CommMethod = SIM.Charge.CommType.IP
        End With

    End Sub

    '   **************************
    '   Server Connection Changed
    '   **************************


    Public Function CheckingDatabaseConection()
        'test if connection Just came UP
        Dim isDataBaseConnected As Boolean
        Dim cmd As SqlClient.SqlCommand
        Dim dtr As SqlClient.SqlDataReader

        Try

            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            cmd = New SqlClient.SqlCommand("SELECT DailyCode FROM AAADailyBusiness WHERE CompanyID = '" & companyInfo.CompanyID & "'", sql.cn)
            dtr = cmd.ExecuteReader
            '    dtr.Read()
            While dtr.Read()
                If dtr("DailyCode") Is DBNull.Value Then
                    isDataBaseConnected = False
                Else
                    isDataBaseConnected = True
                End If
                Exit While
            End While

            dtr.Close()
            sql.cn.Close()

        Catch ex As Exception
            'still not UP
            If Not dtr Is Nothing Then
                If dtr.IsClosed = False Then
                    dtr.Close()
                End If
            End If
            CloseConnection()
            isDataBaseConnected = False
        End Try

        Return isDataBaseConnected

    End Function

    Friend Sub ServerJustWentDown()

        '       MsgBox("Server Not Connected. If problem continues, switch to backup server.")
        mainServerConnected = False
        Beep()
        Exit Sub


        Try
            dsBackup.Tables("AvailTablesTerminal").Columns("ExperienceNumber").AutoIncrement = True
            dsBackup.Tables("AvailTabsTerminal").Columns("ExperienceNumber").AutoIncrement = True
            '       dsBackup.Tables("OpenOrdersTerminal").Columns("OpenOrderID").AutoIncrement = True
        Catch ex As Exception

        End Try

    End Sub


    Friend Sub ServerJustCameUp()
        mainServerConnected = True
        Exit Sub


        '   populate Experirence Table
        ServerUPAvailTabsAndTables222()

        '222   UpdateReopenedChecks()

        '   populate ESC from ESCTerminal (StatusChange)
        ServerUPExperienceStatusChange222()

        '   to regenerate ORDER NUMBERS
        '222     ServerUpPlaceInOrderDetail()

        '   populate OpenOrders
        ServerUPOpenOrders222()

        '   populate Payments And Credits
        ServerUpPaymentsAndCredits()

        UpdateAvailTablesData()

        LogInEmployeesEnteredWhenBackUp222()
        'this is different .. we only need this once
        'b/c working collections are the same on every terminal

        dsBackup.Tables("AvailTablesTerminal").Columns("ExperienceNumber").AutoIncrement = False
        dsBackup.Tables("AvailTabsTerminal").Columns("ExperienceNumber").AutoIncrement = False
        '       dsBackup.Tables("OpenOrdersTerminal").Columns("OpenOrderID").AutoIncrement = False

        '   *** for testing
        If mainServerConnected = False Then
            MsgBox(mainServerConnected, , "Server Connected")
        End If

    End Sub

    Private Sub ServerUPAvailTabsAndTables222()

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            '        sql222.SqlDataAdapterAvailTablesTerminal.Update(dsBackup, "AvailTablesTerminal")
            '       sql222.SqlDataAdapterAvailTabsTerminal.Update(dsBackup, "AvailTabsTerminal")
            sql.cn.Close()
            dsBackup.Tables("AvailTablesTerminal").AcceptChanges()
            dsBackup.Tables("AvailTabsTerminal").AcceptChanges()
        Catch ex As Exception
            CloseConnection()
        End Try

        '**********************************************************************
        '  *** do below only for multiple terminals
        Exit Sub

        Dim newRow As DataRow
        Dim oldExpNum As Int64
        Dim terminalDataTables As New DataView
        Dim terminalDataTablesUpdate As New DataView
        Dim terminalDataTabs As New DataView
        Dim terminalDataTabsUpdate As New DataView
        Dim newExpNum As Int64

        Dim bRow As DataRowView

        With terminalDataTables
            .Table = dsBackup.Tables("AvailTablesTerminal")
            .RowFilter = "dbUP = 0" ' or dbUP = 2"
            .Sort = "ExperienceNumber"
        End With

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()

            For Each bRow In terminalDataTables
                If bRow("dbUP") = 0 Then
                    '   this row created when db down
                    '   our experience Number was generated locally
                    '   we must reassign our experienceNumber then replace the new number into OpenOrdersTerminal
                    oldExpNum = bRow("ExperienceNumber")
                    '        bRow("ExperienceNumber") = Nothing

                    newExpNum = AddItemViewToAvailTabsAndTables222(bRow, False)
                    '        MsgBox(newExpNum, , "New Exprience Number")

                    '   *** we need to do this for all
                    '   then we need to delete all oldRows from AvailTabsAndTables
                    If oldExpNum <> newExpNum Then
                        ReassignNewExperienceNumberToOpenOrders(oldExpNum, newExpNum)   ' (newExpNum + 10)(for testing w/ 1 terminal)
                    End If

                ElseIf bRow("dbUP") = 2 Then
                    ' this previously saved row changed when db down
                    '   this is when we close table in Experience Table    ???????

                End If
            Next
            '       sql222.SqlDataAdapterAvailTablesTerminal.Update(dsBackup, "AvailTablesTerminal")
            '      sql222.SqlDataAdapterAvailTabsTerminal.Update(dsBackup, "AvailTabsTerminal")
            sql.cn.Close()
            '   we only change to 1 after data is saved to SQL Server
            For Each bRow In terminalDataTables
                If bRow("dbUP") = 0 Or bRow("dbUP") = 2 Then
                    bRow("dbUP") = 1
                End If
            Next
        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try
        '       MsgBox("at END of table add")

    End Sub

    Private Sub ReassignNewExperienceNumberToOpenOrders(ByVal oldExpNum As Int64, ByVal newExpNum As Int64)
        Dim dvTerminalOO As New DataView
        Dim enRow As DataRowView

        '   we need to do this b/c we assigned an exp num when we were down
        '   they must be consectutive for each location (no duplicates)
        '   we would have duplicates if each terminal is assigning there own
        '   I think we will never have old experience numbers saved in open order data
        '   since we created the experience while we were down


        '   *** the below is not efficient
        '    we are go through the OOTerminal table for every new exp num
        '   it would be good to go through this only once   ?????
        '   this also might provide a sufficient delay for other terminals to get back online
        Dim bRow As DataRow

        For Each bRow In dsBackup.Tables("OpenOrdersTerminal").Rows
            If bRow("ExperienceNumber") = oldExpNum Then
                '          bRow.Delete()
                '         Exit Sub
                bRow("ExperienceNumber") = newExpNum
            End If

        Next

        Exit Sub
        '********************************************************************************************
        '   the following does not work
        '   \i believe it does not allow change to Exp Num b/c this is how we filtered the datatable
        With dvTerminalOO
            .Table = dsBackup.Tables("OpenOrdersTerminal")
            .RowFilter = "ExperienceNumber = '" & oldExpNum & "'"
        End With

        For Each enRow In dvTerminalOO
            enRow("ExperienceNumner") = newExpNum
        Next

    End Sub

    Private Sub UpdateReopenedChecks222()

        '222 
        'no longer using, this is from server just came up
        ' will have to Change stauts??
        '    only do after we close and accept
        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            '       sql222.SqlDataAdapterClosedTabs.Update(dsOrder.Tables("ClosedTabs"))
            '      sql222.SqlDataAdapterClosedTables.Update(dsOrder.Tables("ClosedTables"))
            sql.cn.Close()
            dsOrder.Tables("ClosedTabs").AcceptChanges()
            dsOrder.Tables("ClosedTables").AcceptChanges()
            '   do we not put this in some terminal dataset ???
            '     GenerateOrderTables.AddStatusChangeData(9, Nothing, Nothing, Nothing)

        Catch ex As Exception
            CloseConnection()
            If Err.Number = "5" Then
                ServerJustWentDown()
            End If
        End Try

        '   *** may want to place a 2 then 10   ???
        '     SaveESCStatusChangeData(10, Nothing, Nothing, Nothing)

    End Sub

    Private Sub ServerUPExperienceStatusChange222()
        Dim terminalData As New DataView
        Dim tRow As DataRowView

        With terminalData
            .Table = dsBackup.Tables("ESCTerminal")
            .RowFilter = "dbUP = 0"
        End With

        Try
            '   *** we only change to 1 after data is saved to SQL Server
            '   this should be lower but was giving an error
            For Each tRow In terminalData
                If tRow("dbUP") = 0 Then
                    tRow("dbUP") = 1
                End If
            Next
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            For Each tRow In terminalData
                AddItemViewToESCStatusChange(tRow)
            Next
            sql.cn.Close()

        Catch ex As Exception
            CloseConnection()
            MsgBox(ex.Message)
        End Try

    End Sub

    Friend Sub ServerUpPlaceInOrderDetail222()
        Dim tRow As DataRow
        Dim newOrderNum As Integer

        For Each tRow In dsBackup.Tables("ESCTerminal").Rows
            If tRow("OrderNumber") < 0 Then
                newOrderNum = ServerUpGenerateNewOrderNumbers222(tRow("OrderNumber"))
            End If
        Next

    End Sub

    Private Function ServerUpGenerateNewOrderNumbers222(ByVal oldOrderNumber As Integer)
        '   *** will be different with multiple terminals

        Dim newOrderNumber As Integer
        Dim escID As Integer

        Dim cmdOrderNumber = New SqlClient.SqlCommand("SELECT MAX(OrderNumber) lastOrderNumber FROM ExperienceStatusChange WHERE LocationID = '" & companyInfo.LocationID & "' AND CompanyID = '" & companyInfo.CompanyID & "'", sql.cn)
        Dim cmdESCid = New SqlClient.SqlCommand("SELECT ExperienceStatusChangeID FROM ExperienceStatusChange WHERE LocationID = '" & companyInfo.LocationID & "' AND companyInfo.CompanyID = '" & companyInfo.CompanyID & "' AND OrderNumber = '" & oldOrderNumber & "'", sql.cn)
        '     Dim cmd As SqlClient.SqlCommand
        Dim dtr As SqlClient.SqlDataReader


        Try
            '   must keep database open so nobody else retreives this OrderNumber
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            dtr = cmdOrderNumber.executereader
            dtr.Read()
            If Not dtr("lastOrderNumber") Is DBNull.Value Then
                newOrderNumber = (dtr("lastOrderNumber")) + 1
            Else
                newOrderNumber = 1
            End If
            dtr.Close()

            dtr = cmdESCid.executereader
            dtr.Read()
            escID = dtr("ExperienceStatusChangeID")
            dtr.Close()

            Dim cmdUpdateOrderNumber As SqlClient.SqlCommand
            Dim cmdUpdateInOpenOrders As SqlClient.SqlCommand


            cmdUpdateOrderNumber = New SqlClient.SqlCommand("UPDATE ExperienceStatusChange set OrderNumber = @OrderNumber WHERE (ExperienceStatusChangeID = @Original_ExperienceStatusChangeID)", sql.cn)
            cmdUpdateOrderNumber.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OrderNumber", System.Data.SqlDbType.Int, 4, "OrderNumber"))
            cmdUpdateOrderNumber.Parameters("@OrderNumber").Value = newOrderNumber
            cmdUpdateOrderNumber.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ExperienceStatusChangeID", System.Data.SqlDbType.Int, 4, "ExperienceStatusChangeID"))
            cmdUpdateOrderNumber.Parameters("@Original_ExperienceStatusChangeID").Value = escID

            cmdUpdateInOpenOrders = New SqlClient.SqlCommand("UPDATE OpenOrders set OrderNumber = @OrderNumber WHERE (OrderNumber = @Original_OrderNumber)", sql.cn)
            cmdUpdateInOpenOrders.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OrderNumber", System.Data.SqlDbType.Int, 4, "OrderNumber"))
            cmdUpdateInOpenOrders.Parameters("@OrderNumber").Value = newOrderNumber
            cmdUpdateInOpenOrders.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_OrderNumber", System.Data.SqlDbType.Int, 4, "ExperienceStatusChangeID"))
            cmdUpdateInOpenOrders.Parameters("@Original_OrderNumber").Value = oldOrderNumber

            cmdUpdateOrderNumber.ExecuteNonQuery()
            cmdUpdateInOpenOrders.ExecuteNonQuery()
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
        End Try

    End Function


    Private Sub ServerUPOpenOrders222()
        Dim newRow As DataRow
        Dim oRow As DataRow
        Dim TerminalDataUpdate As New DataView
        Dim terminalData As New DataView
        Dim tRow As DataRowView

        '   we must first do this to get all new unsaved data in one place
        ' in case we are in the middle of an order when db comes back up
        '      MergeNewOpenOrdersToTerminalBackup()
        dsOrder.Tables("OpenOrders").Rows.Clear()               'do we want this here???
        dsOrder.Tables("OpenOrders").AcceptChanges()

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            '          sql.SqlSelectCommandOpenOrdersSP.Parameters("@CompanyID").Value = CompanyID
            sql.SqlSelectCommandOpenOrdersSP.Parameters("@LocationID").Value = companyInfo.LocationID
            sql.SqlSelectCommandOpenOrdersSP.Parameters("@ExperienceNumber").Value = currentTable.ExperienceNumber
            sql.SqlDataAdapterOpenOrdersSP.Fill(dsOrder.Tables("OpenOrders"))
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
        End Try

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            '   sql222.SqlDataAdapterOOTerminal.Update(dsBackup.Tables("OpenOrdersTerminal"))
            sql.cn.Close()
            dsBackup.Tables("OpenOrdersTerminal").AcceptChanges()
        Catch ex As Exception
            CloseConnection()
        End Try

        With TerminalDataUpdate
            .Table = dsBackup.Tables("OpenOrdersTerminal")      'dtOpenOrdersTerminal       '
            .RowFilter = "ExperienceNumber = '" & currentTable.ExperienceNumber & "' AND dbUP = 2"
            .Sort = "sin"
        End With

        For Each tRow In TerminalDataUpdate
            oRow = (dsOrder.Tables("OpenOrders").Rows.Find(tRow("sin")))
            oRow.Delete()
        Next

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlDataAdapterOpenOrdersSP.Update(dsOrder.Tables("OpenOrders"))
            sql.cn.Close()

            dsOrder.Tables("OpenOrders").AcceptChanges()
        Catch ex As Exception
            CloseConnection()
        End Try

        Exit Sub

        '   *****************************************************************************
        sql.cn.Open()
        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        '     sql222.SqlDataAdapterOOTerminal.Update(dsOrder.Tables("OpenOrdersTerminal"))
        sql.cn.Close()

        dsBackup.Tables("OpenOrdersTerminal").AcceptChanges()
        '   *** not sure which way to use
        '   below is another way to do this

        With TerminalDataUpdate
            .Table = dsBackup.Tables("OpenOrdersTerminal")      'dtOpenOrdersTerminal       '
            .RowFilter = "dbUP = 2"
            .Sort = "sin"
        End With

        '   we will need to update directly in the database to make these changes 
        '   (b/c we are updating every exp from this terminal)
        '        sql.cn.Open()
        For Each tRow In TerminalDataUpdate
            '        oRow = dsOrder.Tables("OpenOrders").Rows.Find(tRow("sin"))

        Next
        '       sql.cn.close
        '   we only change to 1 after data is saved to SQL Server
        For Each tRow In TerminalDataUpdate
            If tRow("dbUP") = 2 Then
                tRow("dbUP") = 1
            End If
        Next

        With terminalData
            .Table = dsBackup.Tables("OpenOrdersTerminal")      'dtOpenOrdersTerminal   '
            .RowFilter = "dbUP = 0"
        End With

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            For Each tRow In terminalData
                AddItemViewToOpenOrders222(tRow)

            Next
            sql.cn.Close()
            '   we only change to 1 after data is saved to SQL Server
            For Each tRow In terminalData
                If tRow("dbUP") = 0 Then
                    tRow("dbUP") = 1
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        MsgBox("Updated Open Orders?")

    End Sub

    Private Sub ServerUpPaymentsAndCredits()
        Dim terminalData As New DataView
        Dim tRow As DataRowView

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlDataAdapterPayments.Update(dsBackup.Tables("PaymentsAndCreditsTerminal"))
            sql.cn.Close()

            For Each tRow In terminalData
                If tRow("dbUP") = 0 Or tRow("dbUP") = 2 Then
                    tRow("dbUP") = 1
                End If
            Next
            dsBackup.Tables("PaymentsAndCreditsTerminal").AcceptChanges()

        Catch ex As Exception
            CloseConnection()
        End Try

        Exit Sub

        '************************************************************************
        '   *** below is not used
        Dim terminalDataUpdate As New DataView
        Dim oRow As DataRow
        Dim ri As Integer

        '   Updates will all have PaymentsAndCreditsID
        With terminalDataUpdate
            .Table = dtPaymentsAndCreditsTerminal           'dsBackup.Tables("PaymentsAndCreditsTerminal")
            .RowFilter = "dbUP = 2"
            .Sort = "PaymentsAndCreditsID"
        End With

        '   UpdatePaymentsAndCredits()
        '   *** must find a way to update row in database

        With terminalData
            .Table = dtPaymentsAndCreditsTerminal           'dsBackup.Tables("PaymentsAndCreditsTerminal")
            .RowFilter = "dbUP = 0"
        End With

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            For Each tRow In terminalData
                AddItemViewToPaymentsAndCredits(tRow)
            Next
            sql.cn.Close()
            '   we only change to 1 after data is saved to SQL Server
            For Each tRow In terminalData
                If tRow("dbUP") = 0 Then
                    tRow("dbUP") = 1
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub






    Friend Sub FlagOpenOrdersRow222(ByRef dRow As DataRow)

        If Not (dRow Is Nothing) Then
            If Not dRow.RowState = DataRowState.Added Then
                '   this means it is probably an old row somewhere in terminal dataset
                '   this is slow (maybe we can find some better)
                '   but not frequent.. only when we delete an item ordered and saved
                Dim terminalData As New DataView
                Dim ri As Integer

                With terminalData
                    .Table = dsBackup.Tables("OpenOrdersTerminal")         ''dtOpenOrdersTerminal
                    .RowFilter = "ExperienceNumber = '" & currentTable.ExperienceNumber & "'"
                    .Sort = "sin"
                End With

                ri = terminalData.Find(dRow("sin"))
                If Not ri = -1 Then
                    '   will not delete a row that is not there
                    terminalData(ri)("dbUP") = 2
                End If
            End If
            '          If Not dRow.RowState = DataRowState.Deleted Or Not dRow.RowState = DataRowState.Detached Then
            '         dRow("dbUP") = 2
            '    End If
        End If

    End Sub


    Friend Function DetermineHoursWorked(ByVal empID As Integer, ByVal LogInDate As Date, ByVal endPayPeriod As Date)

        Dim cmd As SqlClient.SqlCommand
        Dim dtr As SqlClient.SqlDataReader

        Dim runningWeekHours As Integer
        Dim dailyMin As Integer

        LogInDate = LogInDate.AddDays(-0)

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            'this counts this logout for said week,
            'even if logout was during new week, so it appears consistant on time sheet
            '   cmd = New SqlClient.SqlCommand("SELECT JobCode, LogInTime, LogOutTime FROM LoginTracking WHERE CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & companyInfo.LocationID & "' AND EmployeeID = '" & empID & "' AND (LogInTime >= '" & LogInDate & "' OR LogOutTime IS NULL)", sql.cn)
            cmd = New SqlClient.SqlCommand("SELECT JobCode, LogInTime, LogOutTime FROM AAALoginTracking WHERE CompanyID = '" & companyInfo.CompanyID & "' AND LocationID = '" & companyInfo.LocationID & "' AND EmployeeID = '" & empID & "' AND LogInTime < '" & endPayPeriod & "' AND (LogInTime >= '" & LogInDate & "' OR LogOutTime IS NULL)", sql.cn)
            dtr = cmd.ExecuteReader
            While dtr.Read()
                If Not dtr("LogOutTime") Is DBNull.Value Then
                    Dim dailyLogIn As DateTime
                    Dim dailyLogOut As DateTime
                    dailyLogIn = dtr("LogInTime")
                    dailyLogOut = dtr("LogOutTime")
                    dailyMin = (DateDiff(DateInterval.Minute, dailyLogIn, dailyLogOut))
                    '   dailyHours = dailyLogOut.AddDay(-dailyLogIn)
                    runningWeekHours += dailyMin
                End If
            End While

            dtr.Close()
            sql.cn.Close()

            Return runningWeekHours
        Catch ex As Exception
            If Not dtr Is Nothing Then
                If dtr.IsClosed = False Then
                    dtr.Close()
                End If
            End If
            CloseConnection()
        End Try


    End Function

    Friend Sub CreatespiderPOSDirectory()

        If typeProgram = "Online_Demo" Then Exit Sub

        If System.IO.Directory.Exists("c:\Data Files") = False Then
            IO.Directory.CreateDirectory("c:\Data Files")
        End If
        If System.IO.Directory.Exists("c:\Data Files\spiderPOS") = False Then
            IO.Directory.CreateDirectory("c:\Data Files\spiderPOS")
        End If
        If System.IO.Directory.Exists("c:\Data Files\spiderPOS\Menu") = False Then
            IO.Directory.CreateDirectory("c:\Data Files\spiderPOS\Menu")
        End If


    End Sub

    Friend Sub DemoThisNotAvail()

        MsgBox("Function NOT avail in Demo. Call for online Demo 404.869.4700")
    End Sub




    '*******************************
    '   Training Module
    '*******************************




End Module
