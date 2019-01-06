Public Class Menu

    '  Dim sql As New DataSet_Builder.SQLHelper(connectserver)

    Public Sub New(ByVal mc As Integer, ByVal isPrimary As Boolean)
        MyBase.new()

        Dim oRow As DataRow

        If isPrimary = True Then
            'the follow 2 now in login since we only need to pull once (not when we change menu)
            '       PopulateTables()
            '      PopulateModifierMenus()
            PopulateCurrentMenu(mc, True)
            PopulateModifierMenu()
        Else
            PopulateCurrentMenu(mc, False)
        End If

        If currentTerminal.TermMethod = "Quick" Then
            PopulateQuickCategory(mc, isPrimary)
        Else
            PopulateBartenderCategory(mc, isPrimary)
        End If


        '       If isPrimary = False Then       'false so we do this at end
        '      maximumCategoryID = DetermineMaximumCategoryID("ModifierCategory")
        '     End If

    End Sub

    Private Sub PopulateCurrentMenu(ByVal mc As Integer, ByVal IsPrimary As Boolean)

        Dim oRow As DataRow
        Dim customLocationString As String
        Dim sqlStatement As String
        Dim tableCreating As String

        If companyInfo.usingDefaults = False Then
            customLocationString = companyInfo.LocationID
        Else
            customLocationString = "000000"
        End If

        If IsPrimary = True Then
            tableCreating = "MainCategory"
        Else
            tableCreating = "SecondaryMainCategory"
        End If

        sqlStatement = "SELECT Category.CompanyID, Category.LocationID, Category.CategoryID, Category.CategoryName, Category.CategoryAbrev, Category.FunctionID, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, MenuDetail.MenuID, MenuDetail.OrderIndex, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag FROM Category LEFT OUTER JOIN MenuDetail ON Category.CategoryID = MenuDetail.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE Category.CompanyID = '" & companyInfo.CompanyID & "' AND Category.LocationID = '" & customLocationString & "' AND Category.Active = 1 AND MenuDetail.MenuID = '" & mc & "' AND MenuDetail.CompanyID = '" & companyInfo.CompanyID & "' AND MenuDetail.LocationID = '" & customLocationString & "' AND (AABFunctions.FunctionFlag = 'F' OR AABFunctions.FunctionFlag = 'O' OR AABFunctions.FunctionFlag = 'G') ORDER BY MenuDetail.OrderIndex"     'Panel = 'Main'"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        If IsPrimary = True Then
            tableCreating = "IndividualJoinMain"
        Else
            tableCreating = "IndividualJoinSecondary"
        End If
        'foodjoin for modifiers
        '444      sqlStatement = "SELECT FoodJoin.CompanyID, FoodJoin.LocationID, FoodJoin.FoodID, FoodJoin.ModifierID, FoodJoin.NumberFree, FoodJoin.FreeFlag, FoodJoin.GroupFlag, Foods.CategoryID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, Foods.Surcharge, Foods.TaxID, Foods.FoodDesc, Foods.MenuIndex, Foods.InvMultiplier, Category.FunctionID, Category.Priority, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag FROM FoodJoin LEFT OUTER JOIN Foods ON FoodJoin.ModifierID = Foods.FoodID LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE Foods.MenuIndex > 0 AND FoodJoin.ModifierID > 0 AND AABFunctions.FunctionFlag = 'M' AND FoodJoin.CompanyID = '" & companyInfo.CompanyID & "' AND FoodJoin.LocationID = '" & customLocationString & "' ORDER BY Priority ASC"
        '     ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)
        '     sqlStatement = "SELECT FoodJoin.CompanyID, FoodJoin.LocationID, FoodJoin.FoodID, FoodJoin.ModifierID, FoodJoin.NumberFree, FoodJoin.FreeFlag, FoodJoin.GroupFlag, Foods.CategoryID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, MenuJoin.Surcharge, Foods.TaxID, Foods.FoodDesc, Foods.MenuIndex, Category.FunctionID, Category.Priority, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag FROM FoodJoin LEFT OUTER JOIN Foods ON FoodJoin.ModifierID = Foods.FoodID LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID LEFT OUTER JOIN MenuJoin ON FoodJoin.ModifierID = MenuJoin.FoodID WHERE Foods.MenuIndex > 0 AND FoodJoin.ModifierID > 0 AND MenuJoin.MenuID = '" & mc & "' AND MenuJoin.GeneralMenuID IS NULL AND NOT AABFunctions.FunctionFlag = 'M' AND FoodJoin.CompanyID = '" & companyInfo.CompanyID & "' AND FoodJoin.LocationID = '" & customLocationString & "' ORDER BY Priority ASC"
        '    ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        '**************************
        '   we are using 2 statements
        ' the 1st limits to selected menu, and does not include Modifiers
        '   these item MUST be defined on the menu, or they won't show up 
        '   (only modifiers - 2nd stmt - show on every menu)
        ' the second is any Modifiers linked by Indivual Join (no matter menu)
        ' they are cummlative, therefore 1 does not cancel the other
        'foodjoin for NON modifiers ("F" and "O") 
        'this first one is only selecting joins that are in a Menu
        ' for main foods or (Other Foods) we place the MenuIndex info in the MenuJoin table
        sqlStatement = "SELECT FoodJoin.CompanyID, FoodJoin.LocationID, FoodJoin.FoodID, FoodJoin.ModifierID, FoodJoin.NumberFree, FoodJoin.FreeFlag, FoodJoin.GroupFlag, Foods.CategoryID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, MenuJoin.Surcharge, Foods.TaxID, Foods.FoodDesc, Foods.InvMultiplier, MenuJoin.MenuIndex, Category.FunctionID, Category.Priority, Category.HalfSplit, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag FROM FoodJoin LEFT OUTER JOIN Foods ON FoodJoin.ModifierID = Foods.FoodID LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID LEFT OUTER JOIN MenuJoin ON FoodJoin.ModifierID = MenuJoin.FoodID WHERE MenuJoin.MenuIndex > 0 AND FoodJoin.ModifierID > 0 AND MenuJoin.MenuID = '" & mc & "' AND MenuJoin.GeneralMenuID IS NULL AND NOT AABFunctions.FunctionFlag = 'M' AND FoodJoin.CompanyID = '" & companyInfo.CompanyID & "' AND FoodJoin.LocationID = '" & customLocationString & "' ORDER BY Priority ASC"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)
        '** going back to something before, not sure .....MenuJoin.MenuIndex > 0 did not work
        sqlStatement = "SELECT FoodJoin.CompanyID, FoodJoin.LocationID, FoodJoin.FoodID, FoodJoin.ModifierID, FoodJoin.NumberFree, FoodJoin.FreeFlag, FoodJoin.GroupFlag, Foods.CategoryID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, MenuJoin.Surcharge, Foods.TaxID, Foods.FoodDesc, Foods.InvMultiplier, MenuJoin.MenuIndex, Category.FunctionID, Category.Priority, Category.HalfSplit, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag FROM FoodJoin LEFT OUTER JOIN Foods ON FoodJoin.ModifierID = Foods.FoodID LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID LEFT OUTER JOIN MenuJoin ON FoodJoin.ModifierID = MenuJoin.FoodID WHERE Foods.MenuIndex > 0 AND FoodJoin.ModifierID > 0 AND MenuJoin.GeneralMenuID IS NULL AND AABFunctions.FunctionFlag = 'M' AND FoodJoin.LocationID = '" & customLocationString & "' ORDER BY Priority ASC"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)


        If IsPrimary = True Then
            tableCreating = "DrinkCategory"
        Else
            tableCreating = "SecondaryDrinkCategory"
        End If
        sqlStatement = "SELECT DrinkCategory.DrinkCategoryID, DrinkCategory.CompanyID, DrinkCategory.LocationID, DrinkCategory.DrinkCategoryNumber, DrinkCategory.DrinkCategoryName, DrinkCategory.ButtonColor, DrinkCategory.ButtonForeColor, DrinkCategory.IsALiquorType, MenuDetail.MenuID, MenuDetail.OrderIndex FROM DrinkCategory LEFT OUTER JOIN MenuDetail ON DrinkCategory.DrinkCategoryID = MenuDetail.DrinkCategoryID WHERE DrinkCategory.CompanyID = '" & companyInfo.CompanyID & "' AND DrinkCategory.LocationID = '" & customLocationString & "' AND MenuDetail.MenuID = '" & mc & "'"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)
     
        '   *** I don't think we need both Main and Secodary Tables since
        '   we are using an AllFoodCategory table to fill them..they should be the same
        '   ________________________________________
        '   in these next 2 stmts... we are filling data for the default "000000" and then filling
        '   data for the LocationID for the specific rest.

        If IsPrimary = True Then
            For Each oRow In ds.Tables("AllFoodCategory").Rows  '("MainCategory").Rows
                tableCreating = "MainTable" & oRow("CategoryID")

                If oRow("FunctionFlag") = "G" Then
                    Try
                        '      sqlStatement = "SELECT Foods.FoodID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, Foods.CategoryID, Foods.FoodCost, Foods.FoodDesc, Foods.WineParringID, Foods.PrintPriorityID, Foods.PrepareTime, Foods.InvMultiplier, MenuJoin.FoodID, MenuJoin.MenuID, MenuJoin.Price, MenuJoin.RoutingID, MenuJoin.Surcharge, MenuJoin.MenuIndex, Category.FunctionID, Category.HalfSplit, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionID, AABFunctions.FunctionFlag, AABFunctions.TaxID, AABFunctions.FunctionGroupID FROM Foods INNER JOIN MenuJoin ON Foods.FoodID = MenuJoin.FoodID LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE MenuJoin.MenuIndex > 0 AND MenuJoin.GeneralMenuID = " & oRow("CategoryID") & " AND Foods.Active = 1 AND MenuJoin.Active = 1 AND (MenuJoin.MenuID = '" & mc & "') AND (Foods.CompanyID = '" & companyInfo.CompanyID & "') AND (Foods.LocationID = '" & customLocationString & "')"
                        sqlStatement = "SELECT Foods.FoodID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, Foods.CategoryID, Foods.FoodCost, Foods.TaxExempt, Foods.FoodDesc, Foods.WineParringID, Foods.PrintPriorityID, Foods.PrepareTime, Foods.InvMultiplier, MenuJoin.FoodID, MenuJoin.MenuID, MenuJoin.Price, MenuJoin.RoutingID, MenuJoin.Surcharge, MenuJoin.MenuIndex, Category.FunctionID, Category.HalfSplit, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionID, AABFunctions.FunctionFlag, AABFunctions.TaxID, AABFunctions.FunctionGroupID FROM Foods INNER JOIN MenuJoin ON Foods.FoodID = MenuJoin.FoodID LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE MenuJoin.GeneralMenuID = " & oRow("CategoryID") & " AND Foods.Active = 1 AND MenuJoin.Active = 1 AND (MenuJoin.MenuID = '" & mc & "') AND (Foods.CompanyID = '" & companyInfo.CompanyID & "') AND (Foods.LocationID = '" & customLocationString & "')"
                        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

                        tableCreating = "DrinkMainTable" & oRow("CategoryID")
                        sqlStatement = "SELECT Drinks.DrinkID, Drinks.DrinkName, Drinks.DrinkCategoryID, DrinkCategory.DrinkCategoryNumber, DrinkCategory.ButtonColor, DrinkCategory.ButtonForeColor, MenuJoin.DrinkID, MenuJoin.MenuID, MenuJoin.Price, MenuJoin.RoutingID, MenuJoin.Surcharge, MenuJoin.MenuIndex, AABFunctions.FunctionID, AABFunctions.FunctionFlag, AABFunctions.TaxID, AABFunctions.FunctionGroupID FROM Drinks INNER JOIN MenuJoin ON Drinks.DrinkID = MenuJoin.DrinkID LEFT OUTER JOIN DrinkCategory ON Drinks.DrinkCategoryID = DrinkCategory.DrinkCategoryID LEFT OUTER JOIN AABFunctions ON Drinks.DrinkFunctionID = AABFunctions.FunctionID WHERE MenuJoin.GeneralMenuID = " & oRow("CategoryID") & " AND Drinks.Active = 1 AND MenuJoin.Active = 1 AND (MenuJoin.MenuID = '" & mc & "') AND (Drinks.CompanyID = '" & companyInfo.CompanyID & "') AND (Drinks.LocationID = '" & customLocationString & "')"
                        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                Else
                    '      sqlStatement = "SELECT Foods.FoodID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, Foods.CategoryID, Foods.FoodCost, Foods.FoodDesc, Foods.WineParringID, Foods.PrintPriorityID, Foods.PrepareTime, Foods.InvMultiplier, MenuJoin.FoodID, MenuJoin.MenuID, MenuJoin.Price, MenuJoin.RoutingID, MenuJoin.Surcharge, MenuJoin.MenuIndex, Category.FunctionID, Category.HalfSplit, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionID, AABFunctions.FunctionFlag, AABFunctions.TaxID, AABFunctions.FunctionGroupID FROM Foods INNER JOIN MenuJoin ON Foods.FoodID = MenuJoin.FoodID LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE MenuJoin.MenuIndex > 0 AND Foods.CategoryID = " & oRow("CategoryID") & " AND Foods.Active = 1 AND MenuJoin.Active = 1 AND (MenuJoin.MenuID = '" & mc & "') AND (MenuJoin.GeneralMenuID IS NULL) AND (Foods.CompanyID = '" & companyInfo.CompanyID & "') AND (Foods.LocationID = '" & customLocationString & "')"
                    sqlStatement = "SELECT Foods.FoodID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, Foods.CategoryID, Foods.FoodCost, Foods.TaxExempt, Foods.FoodDesc, Foods.WineParringID, Foods.PrintPriorityID, Foods.PrepareTime, Foods.InvMultiplier, MenuJoin.FoodID, MenuJoin.MenuID, MenuJoin.Price, MenuJoin.RoutingID, MenuJoin.Surcharge, MenuJoin.MenuIndex, Category.FunctionID, Category.HalfSplit, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionID, AABFunctions.FunctionFlag, AABFunctions.TaxID, AABFunctions.FunctionGroupID FROM Foods INNER JOIN MenuJoin ON Foods.FoodID = MenuJoin.FoodID LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE Foods.CategoryID = " & oRow("CategoryID") & " AND Foods.Active = 1 AND MenuJoin.Active = 1 AND (MenuJoin.MenuID = '" & mc & "') AND (MenuJoin.GeneralMenuID IS NULL) AND (Foods.CompanyID = '" & companyInfo.CompanyID & "') AND (Foods.LocationID = '" & customLocationString & "')"
                    ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

                End If
            Next

        Else
            For Each oRow In ds.Tables("AllFoodCategory").Rows  '("SecondaryMainCategory").Rows
                tableCreating = "SecondaryMainTable" & oRow("CategoryID")
                If oRow("FunctionFlag") = "G" Then
                    sqlStatement = "SELECT Foods.FoodID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, Foods.CategoryID, Foods.FoodCost, Foods.TaxExempt, Foods.FoodDesc, Foods.WineParringID, Foods.PrintPriorityID, Foods.PrepareTime, Foods.InvMultiplier, MenuJoin.FoodID, MenuJoin.MenuID, MenuJoin.Price, MenuJoin.RoutingID, MenuJoin.Surcharge, MenuJoin.MenuIndex, Category.FunctionID, Category.HalfSplit, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionID, AABFunctions.FunctionFlag, AABFunctions.TaxID, AABFunctions.FunctionGroupID FROM Foods INNER JOIN MenuJoin ON Foods.FoodID = MenuJoin.FoodID LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE MenuJoin.GeneralMenuID = " & oRow("CategoryID") & " AND Foods.Active = 1 AND MenuJoin.Active = 1 AND (MenuJoin.MenuID = '" & mc & "') AND (Foods.CompanyID = '" & companyInfo.CompanyID & "') AND (Foods.LocationID = '" & customLocationString & "')"
                    ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

                Else
                    sqlStatement = "SELECT Foods.FoodID, Foods.FoodName, Foods.AbrevFoodName, Foods.ChitFoodName, Foods.CategoryID, Foods.FoodCost, Foods.TaxExempt, Foods.FoodDesc, Foods.WineParringID, Foods.PrintPriorityID, Foods.PrepareTime, Foods.InvMultiplier, MenuJoin.FoodID, MenuJoin.MenuID, MenuJoin.Price, MenuJoin.RoutingID, MenuJoin.Surcharge, MenuJoin.MenuIndex, Category.FunctionID, Category.HalfSplit, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, AABFunctions.FunctionID, AABFunctions.FunctionFlag, AABFunctions.TaxID, AABFunctions.FunctionGroupID FROM Foods INNER JOIN MenuJoin ON Foods.FoodID = MenuJoin.FoodID LEFT OUTER JOIN Category ON Foods.CategoryID = Category.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE Foods.CategoryID = " & oRow("CategoryID") & " AND Foods.Active = 1 AND MenuJoin.Active = 1 AND (MenuJoin.MenuID = '" & mc & "') AND (MenuJoin.GeneralMenuID IS NULL) AND (Foods.CompanyID = '" & companyInfo.CompanyID & "') AND (Foods.LocationID = '" & customLocationString & "')"
                    ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)
                End If
            
            Next
        End If

    End Sub

    Private Sub PopulateBartenderCategory(ByVal mc As Integer, ByVal IsPrimary As Boolean)

        Dim sqlStatement As String
        Dim tableCreating As String
        Dim customLocationString As String

        If companyInfo.usingDefaults = False Then
            customLocationString = companyInfo.LocationID
        Else
            customLocationString = "000000"
        End If

        If IsPrimary = True Then
            tableCreating = "BartenderCategory"
        Else
            tableCreating = "SecondaryBartenderCategory"
        End If

        sqlStatement = "SELECT Category.CompanyID, Category.LocationID, Category.CategoryID, Category.CategoryName, Category.CategoryAbrev, Category.FunctionID, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, MenuDetail.BartenderMenuID, MenuDetail.OrderIndex, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag, AABFunctions.TaxID FROM Category LEFT OUTER JOIN MenuDetail ON Category.CategoryID = MenuDetail.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE Category.CompanyID = '" & companyInfo.CompanyID & "' AND Category.LocationID = '" & customLocationString & "' AND Category.Active = 1 AND MenuDetail.BartenderMenuID = '" & mc & "' AND MenuDetail.CompanyID = '" & companyInfo.CompanyID & "' AND MenuDetail.LocationID = '" & customLocationString & "' AND (AABFunctions.FunctionFlag = 'F' OR AABFunctions.FunctionFlag = 'O' OR AABFunctions.FunctionFlag = 'G') ORDER BY MenuDetail.OrderIndex"     'Panel = 'Main'"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        If IsPrimary = True Then
            tableCreating = "BartenderDrinkCategory"
        Else
            tableCreating = "SecondaryBartenderDrinkCategory"
        End If

        sqlStatement = "SELECT DrinkCategory.DrinkCategoryID, DrinkCategory.CompanyID, DrinkCategory.LocationID, DrinkCategory.DrinkCategoryNumber, DrinkCategory.DrinkCategoryName, DrinkCategory.ButtonColor, DrinkCategory.ButtonForeColor, DrinkCategory.IsALiquorType, MenuDetail.BartenderMenuID, MenuDetail.OrderIndex FROM DrinkCategory LEFT OUTER JOIN MenuDetail ON DrinkCategory.DrinkCategoryID = MenuDetail.DrinkCategoryID WHERE DrinkCategory.CompanyID = '" & companyInfo.CompanyID & "' AND DrinkCategory.LocationID = '" & customLocationString & "' AND MenuDetail.BartenderMenuID = '" & mc & "'"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

    End Sub

    Private Sub PopulateQuickCategory(ByVal mc As Integer, ByVal IsPrimary As Boolean)

        Dim sqlStatement As String
        Dim tableCreating As String
        Dim customLocationString As String

        If companyInfo.usingDefaults = False Then
            customLocationString = companyInfo.LocationID
        Else
            customLocationString = "000000"
        End If

        If IsPrimary = True Then
            tableCreating = "QuickCategory"
        Else
            tableCreating = "SecondaryQuickCategory"
        End If

        sqlStatement = "SELECT Category.CompanyID, Category.LocationID, Category.CategoryID, Category.CategoryName, Category.CategoryAbrev, Category.FunctionID, Category.ButtonColor, Category.ButtonForeColor, Category.Extended, MenuDetail.BartenderMenuID, MenuDetail.OrderIndex, AABFunctions.FunctionGroupID, AABFunctions.FunctionFlag, AABFunctions.TaxID FROM Category LEFT OUTER JOIN MenuDetail ON Category.CategoryID = MenuDetail.CategoryID LEFT OUTER JOIN AABFunctions ON Category.FunctionID = AABFunctions.FunctionID WHERE Category.CompanyID = '" & companyInfo.CompanyID & "' AND Category.LocationID = '" & customLocationString & "' AND Category.Active = 1 AND MenuDetail.QuickMenuID = '" & mc & "' AND MenuDetail.CompanyID = '" & companyInfo.CompanyID & "' AND MenuDetail.LocationID = '" & customLocationString & "' AND (AABFunctions.FunctionFlag = 'F' OR AABFunctions.FunctionFlag = 'O' OR AABFunctions.FunctionFlag = 'G') ORDER BY MenuDetail.OrderIndex"     'Panel = 'Main'"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

        If IsPrimary = True Then
            tableCreating = "QuickDrinkCategory"
        Else
            tableCreating = "SecondaryQuickDrinkCategory"
        End If

        sqlStatement = "SELECT DrinkCategory.DrinkCategoryID, DrinkCategory.CompanyID, DrinkCategory.LocationID, DrinkCategory.DrinkCategoryNumber, DrinkCategory.DrinkCategoryName, DrinkCategory.ButtonColor, DrinkCategory.ButtonForeColor, DrinkCategory.IsALiquorType, MenuDetail.BartenderMenuID, MenuDetail.OrderIndex FROM DrinkCategory LEFT OUTER JOIN MenuDetail ON DrinkCategory.DrinkCategoryID = MenuDetail.DrinkCategoryID WHERE DrinkCategory.CompanyID = '" & companyInfo.CompanyID & "' AND DrinkCategory.LocationID = '" & customLocationString & "' AND MenuDetail.QuickMenuID = '" & mc & "'"
        ds = sql.PopulateSelectedItemTable(tableCreating, sqlStatement, ds)

    End Sub

    Private Sub PopulateModifierMenu()
        Dim oRow As DataRow
        Dim sqlStatement As String
        Dim tableCreating As String
        Dim customLocationString As String
        If companyInfo.usingDefaults = False Then
            customLocationString = companyInfo.LocationID
        Else
            customLocationString = "000000"
        End If

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

    Private Function DetermineMaximumCategoryID222(ByRef tablecreating As String)

        Dim MaxID As Integer

        If ds.Tables(tablecreating).Rows.Count > 0 Then
            MaxID = ds.Tables(tablecreating).Compute("Max(CategoryID)", "")
        End If

        Return MaxID

    End Function



    '*********************
    '       old


    Private Sub PopulateTables222()
        '       *****************************
        '       now in login / GeneralOrderTables
        '       *****************************      

    End Sub

    Private Sub PopulateModifierMenus222()
        '       *****************************
        '       now in login / GeneralOrderTables
        '       *****************************      

    End Sub

End Class
