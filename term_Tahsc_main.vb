Imports System.Runtime.InteropServices
Imports DataSet_Builder


Public Module term_Tahsc

    Dim programStart As Login
   
    Public connectserver As String
    Public localConnectServer As String
    Public securityPhoenixEst As Boolean
    Public securityLocalEst As Boolean
    Public connectionDown As Boolean = False

    Public ourComputerName As String
    Public ourServerName As String
    Public pointToServer As Boolean = False

    Public typeProgram As String = "v_1_0"
    '   Public typeProgram As String = "Demo"
    '  Public typeProgram As String = "Online_Demo"
    Public acitveDemo As Boolean = True ' this is so we can shut off demo
    Public demoExpNumID As Int64 = 1
    Public demoNewTicketID As Integer = 100001
    Public demoOpenOrdersID As Int64 = 1
    Public demoPaymentID As Int64 = 1
    Public demoOrderNumber As Int64 = 123001
    Public demoTabID As Int64 = 100001
    Public demoCcNumberAddon As Integer = 1
    Public demoCashOpen As Decimal

    Public dateOfExpiration As DateTime = "03/01/2010" '"02/02/2020"

    '   *** just for testing
    Public time1 As DateTime
    Public time2 As DateTime
    Public timeDiff As TimeSpan

    '   Public Sub Main()
    '       LetsStartProgram()
    '   End Sub

    '    Private Sub LetsStartProgram()
    '       programStart = New Login
    '      programStart.Show()
    '     programStart.DisplayInitialLogon()
    '    End Sub

    Private Sub TestTime()
        time1 = Now
        time2 = Now
        timeDiff = time2.Subtract(time1)
        MsgBox(timeDiff.ToString)
    End Sub

    Friend c1 As Color = Color.FromArgb(249, 200, 7) '(249, 218, 31) 'Color.Yellow '(252, 234, 128)
    Friend c2 As Color = Color.Black
    Friend c3 As Color = Color.White
    Friend c4 As Color = Color.FromArgb(0, 0, 100) ' Color.SlateBlue    ' Color.MediumSlateBlue  '
    Friend c5 As Color = Color.DimGray
    Friend c6 As Color = Color.FromArgb(100, 149, 237)  'Foods
    Friend c7 As Color = Color.SlateGray
    Friend c8 As Color = Color.LightSteelBlue       'Color.AliceBlue   'Color.PowderBlue
    Friend c9 As Color = Color.Firebrick 'Crimson        'Color.Red
    Friend c10 As Color = Color.LightSlateGray
    Friend c11 As Color = Color.DarkGray
    Friend c12 As Color = Color.LightGray
    Friend c13 As Color = Color.Black
    Friend c14 As Color = Color.WhiteSmoke
    Friend c15 As Color = Color.CornflowerBlue
    Friend c16 As Color = Color.FromArgb(59, 96, 141)  'Drinks
    Friend c17 As Color = Color.RoyalBlue
    Friend c18 As Color = Color.IndianRed
    Friend c19 As Color = Color.DodgerBlue
    Friend c20 As Color = Color.FromArgb(0, 0, 240)

    Public Enum floorPlanEnum

        FloorPlan1 = 1
        FloorPlan2 = 2
        FloorPlan3 = 3
        FloorPlan4 = 4
        FloorPlan5 = 5

    End Enum

    Friend wa As Rectangle = Screen.PrimaryScreen.WorkingArea
    Friend totalArea As Rectangle = Screen.PrimaryScreen.Bounds
    Friend ssX As Double = 1024     'wa.Width 
    Friend ssY As Double = 740      'wa.Height '768  
    Friend Const buttonSpace As Integer = 4
    Friend viewOrderWidth As Double = ssX * 0.3
    Friend viewOrderHeight As Double = ssY * 0.7

    Friend isPhoenixComputer As Boolean = True  ' this will only be true is the software for Phoenix
    '  Friend localMerchantID As Integer
    Friend mainServerConnected As Boolean = True
    Friend tablesFilled As Boolean
    Friend timeoutInterval As Integer = 1000   'default is 1000  (1 sec for every interval count)
    Friend allowTableOverride As Boolean = True
    '   Friend timeoutMultiplier As Integer = 30    ' 30 seconds

    Friend printingRouting(20) As Integer
    Friend printingName(20) As String
    Friend printingBoolean(20) As Boolean
    Friend printExpiditer As Boolean = False

    Friend companyInfo As CompanyMainInfo
    Friend currentTable As DinnerTable
    Friend AllEmployees As New EmployeeCollection
    Friend SalariedEmployees As New EmployeeCollection
    Friend SwipeCodeEmployees As New EmployeeCollection
    Friend groupTerminals As New TerminalCollection
    Friend currentRawMaterials As New RawMaterialCollection
    Friend currentTerminal As Terminal
    Friend currentServer As Employee
    Friend currentClockEmp As Employee
    Friend actingManager As Employee
    Friend empActive As Employee
    Friend currentCustomer As Customer
    Friend numberOfTables As Integer = 100
    Friend numberOfWalls As Integer = 50
    Friend btnTable(100) As Seating_Table_UC2
    Friend btnWall(50) As Panel

    Friend workingEmployees As New EmployeeCollection       'everyone currently clocked-in to the system
    Friend currentServers As New EmployeeCollection
    Friend currentBartenders As New EmployeeCollection
    Friend loggedInBartenders As New EmployeeCollection
    Friend currentManagers As New EmployeeCollection
    Friend todaysFloorPersonnel As New EmployeeCollection   'everyone who worked the floor that day
    Friend allFloorPersonnel As New EmployeeCollection   'everyone who worked the floor that day
    Friend currentQuickTicketDataViews As New DataViewBarCollection
    Friend tabcc As New PaymentCollection
    Friend tabccThisExperience As New PaymentCollection
    Friend newItemCollection As New CurrentItemCollection

    '222
    '  below 2 collections not used
    Friend currentPhysicalTables As New PhysicalTableCollection 'this is all tables in restaurant
    Friend currentActiveTables As New ActiveTableCollection     'this is active for current employee

    Friend employeeAuthorization As ManagementAuthorization
    Friend systemAuthorization As OverrideSystemCode

    '    Friend primaryMenuID As Integer          '= 1
    '    Friend secondaryMenuID As Integer            ' = 2
    '   Friend autoChangeMneu As DateTime
    '   Friend initPrimaryMenuID As Integer
    '    Friend currentPrimaryMenuID As Integer
    '   Friend currentSecondaryMenuID As Integer

    Friend mainCategoryIDArrayList As New ArrayList
    Friend secondaryCategoryIDArrayList As New ArrayList

    '   each terminal Bar Group will have its own currentBartenders Collection
    '   any bartender can access their info from any terminal in their terminal group
    '   managers may log into any bartender group
    ' Friend activeCheck As Integer
    Friend activeCheckChanged As Boolean = False
    Friend sinDragSource As Integer
    Friend checkDragTarget As Integer
    Friend movingCustomer As Integer    'this is if we are moving the customer panel in split checks
    Friend newlyCreatedCheck As Boolean
    Friend orderScreenInitialized As Boolean
    ' is a 0 if not
    '   only temp
    '   ***         ***     some of this should be read off the database

    '   should add first two only in CompanyInfo
    '   Friend CompanyID As String '= "001111"
    '  Friend LocationID As String ' = "000001"
    '  Friend currentTerminal.TermID As Integer

    '   Friend merchantID As String = "494901"
    '  Friend operatorID As String = "eGlobal"
    '   Friend currentTerminal.TermMethod As Integer    '(1 -TableService  2 - Qiuck)
    '    Friend currentMenuID As Integer
    '   Friend currentShift As Integer = 1
    '    Friend usingDefaults As Boolean
    '   Friend currentTerminal.currentDailyCode As Int64         '***    ***************** need to change
    '  Friend lastTicketNumber As Integer = 0
    '   Friend autoCloseCheck As Boolean
   '   Friend endOfWeek As DayOfWeek    ' (Monday -1 ... Sunday -7)
    '    Friend begOfWeek As DayOfWeek
    '   Friend onlyOneFloorPlan As Boolean
    '   Friend calculateAvgByEntrees As Boolean = True
    '   Friend usingBartenderMethod As Boolean = True
    '   Friend autoGratuityPercent As Decimal = 0.18
    '    Friend isKitchenExpiditer As Boolean = False    ' read off DailyBusiness

    '   Friend routingIndex1 As Integer
    '   Friend routingIndex2 As Integer
    '   Friend routingIndex3 As Integer
    '  Friend routingIndex4 As Integer
    '  Friend routingIndex5 As Integer

    '************************************************
    '   Card Reader Declarations
    '************************************************

    'from setupapi.h
    Public Const DIGCF_PRESENT As Short = &H2S
    Public Const DIGCF_DEVICEINTERFACE As Short = &H10S

    Public Const FILE_FLAG_OVERLAPPED As Integer = &H40000000
    Public Const FILE_SHARE_READ As Short = &H1S
    Public Const FILE_SHARE_WRITE As Short = &H2S
    Public Const GENERIC_READ As Integer = &H80000000
    Public Const GENERIC_WRITE As Integer = &H40000000
    Public Const OPEN_EXISTING As Short = 3
    Public Const WAIT_TIMEOUT As Integer = &H102
    Public Const WAIT_OBJECT_0 As Short = 0



    <StructLayout(LayoutKind.Sequential)> _
    Public Structure HIDD_ATTRIBUTES
        Dim Size As Integer
        Dim VendorID As Short
        Dim ProductID As Short
        Dim VersionNumber As Short
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SP_DEVICE_INTERFACE_DATA
        Dim cbSize As Integer
        Dim InterfaceClassGuid As System.Guid
        Dim Flags As Integer
        Dim Reserved As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SP_DEVICE_INTERFACE_DETAIL_DATA
        Dim cbSize As Integer
        Dim DevicePath As String
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SECURITY_ATTRIBUTES
        Dim nLength As Integer
        Dim lpSecurityDescriptor As Integer
        Dim bInheritHandle As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
       Public Structure OVERLAPPED
        Dim Internal As Integer
        Dim InternalHigh As Integer
        Dim Offset As Integer
        Dim OffsetHigh As Integer
        Dim hEvent As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
      Public Structure HIDP_CAPS
        Dim Usage As Short
        Dim UsagePage As Short
        Dim InputReportByteLength As Short
        Dim OutputReportByteLength As Short
        Dim FeatureReportByteLength As Short
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=17)> Dim Reserved() As Short
        Dim NumberLinkCollectionNodes As Short
        Dim NumberInputButtonCaps As Short
        Dim NumberInputValueCaps As Short
        Dim NumberInputDataIndices As Short
        Dim NumberOutputButtonCaps As Short
        Dim NumberOutputValueCaps As Short
        Dim NumberOutputDataIndices As Short
        Dim NumberFeatureButtonCaps As Short
        Dim NumberFeatureValueCaps As Short
        Dim NumberFeatureDataIndices As Short

    End Structure





    '   Individual Food/Drink Item Status Listing:
    '   0   Order Taken
    '   1   Hold
    '   2   Sent to Kitchen/Bar
    '   3   Ready For Delivery
    '   4   Delivered


    '   Table Status Listing:   ??????
    '   0   Not Available
    '   1   Available for Seating
    '   2   Sat
    '   3   Food Ordered
    '   4   Food Ready
    '   6   Food Delivered
    '   7   Check Down





    Friend Structure OverrideSystemCode

        Private _voidItem As Integer
        Private _forcePrice As Integer
        Private _compItem As Integer
        Private _taxExempt As Integer
        Private _reprintCheck As Integer
        Private _reprintOrder As Integer
        Private _reopenCheck As Integer
        Private _voidCheck As Integer
        Private _adjustPayment As Integer
        Private _assignComps As Integer
        Private _assignGratuity As Integer
        Private _transferItem As Integer
        Private _transferCheck As Integer
        Private _reprintCredit As Integer


        Friend Property VoidItem() As Integer
            Get
                Return _voidItem
            End Get
            Set(ByVal Value As Integer)
                _voidItem = Value
            End Set
        End Property

        Friend Property ForcePrice() As Integer
            Get
                Return _forcePrice
            End Get
            Set(ByVal Value As Integer)
                _forcePrice = Value
            End Set
        End Property

        Friend Property CompItem() As Integer
            Get
                Return _compItem
            End Get
            Set(ByVal Value As Integer)
                _compItem = Value
            End Set
        End Property

        Friend Property TaxExempt() As Integer
            Get
                Return _taxExempt
            End Get
            Set(ByVal Value As Integer)
                _taxExempt = Value
            End Set
        End Property

        Friend Property ReprintCheck() As Integer
            Get
                Return _reprintCheck
            End Get
            Set(ByVal Value As Integer)
                _reprintCheck = Value
            End Set
        End Property

        Friend Property ReprintOrder() As Integer
            Get
                Return _reprintOrder
            End Get
            Set(ByVal Value As Integer)
                _reprintOrder = Value
            End Set
        End Property

        Friend Property ReopenCheck() As Integer
            Get
                Return _reopenCheck
            End Get
            Set(ByVal Value As Integer)
                _reopenCheck = Value
            End Set
        End Property

        Friend Property VoidCheck() As Integer
            Get
                Return _voidCheck
            End Get
            Set(ByVal Value As Integer)
                _voidCheck = Value
            End Set
        End Property

        Friend Property AdjustPayment() As Integer
            Get
                Return _adjustPayment
            End Get
            Set(ByVal Value As Integer)
                _adjustPayment = Value
            End Set
        End Property

        Friend Property AssignComps() As Integer
            Get
                Return _assignComps
            End Get
            Set(ByVal Value As Integer)
                _assignComps = Value
            End Set
        End Property

        Friend Property AssignGratuity() As Integer
            Get
                Return _assignGratuity
            End Get
            Set(ByVal Value As Integer)
                _assignGratuity = Value
            End Set
        End Property

        Friend Property TransferItem() As Integer
            Get
                Return _transferItem
            End Get
            Set(ByVal Value As Integer)
                _transferItem = Value
            End Set
        End Property

        Friend Property TransferCheck() As Integer
            Get
                Return _transferCheck
            End Get
            Set(ByVal Value As Integer)
                _transferCheck = Value
            End Set
        End Property

        Friend Property ReprintCredit() As Integer
            Get
                Return _reprintCredit
            End Get
            Set(ByVal Value As Integer)
                _reprintCredit = Value
            End Set
        End Property


    End Structure




End Module
