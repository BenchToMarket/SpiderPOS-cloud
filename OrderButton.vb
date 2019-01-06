Friend Class OrderButton
    Inherits Button

    Private _border As BorderStyle = BorderStyle.None
    Private _borderStyle3D As Border3DStyle
    Private _drinkCategory As Boolean
    Private _subCategory As Boolean
    Private _drinkAdds As Boolean
    Private _text As String
    Private _name As String
    Private _id As Integer
    Private _price As Decimal
    Private _cost As Single
    Private _foodID As Integer      ' carries food id to modifier selection
    Private _numberFree As Integer
    Private _functions As Integer
    Private _functionGroup As Integer
    Private _functionFlag As String
    Private _isPrimary As Boolean
    Private _foodTableIndex As Integer      'which primary or secondary table to use
    Private _halfSplit As Boolean
    Private _InvMultiplier As Decimal
    Private _extended As Boolean
    Private _dpMethod As String

    Private _mainButtonIndex As Integer
    Private _modifierButtonIndex As Integer
    Private _modifierMenuIndex As Integer
    Private _maxMenuIndex As Integer
    Private _categoryID As Integer
    Private _activeDataView As DataView
    Private _previousID As Integer

    Friend Property Border() As BorderStyle
        Get
            Return _border
        End Get
        Set(ByVal Value As BorderStyle)
            _border = Value
        End Set
    End Property
    Friend Property BorderStyle3D() As Border3DStyle
        Get
            Return _borderStyle3D
        End Get
        Set(ByVal value As Border3DStyle)
            _borderStyle3D = value
        End Set
    End Property
    Friend Property DrinkCategory() As Boolean
        Get
            Return _drinkCategory
        End Get
        Set(ByVal Value As Boolean)
            _drinkCategory = Value
        End Set
    End Property

    Friend Property SubCategory() As Boolean
        Get
            Return _subCategory
        End Get
        Set(ByVal Value As Boolean)
            _subCategory = Value
        End Set
    End Property

    Friend Property DrinkAdds() As Boolean
        Get
            Return _drinkAdds
        End Get
        Set(ByVal Value As Boolean)
            _drinkAdds = Value
        End Set
    End Property

    Friend Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal Value As Integer)
            _id = Value
        End Set
    End Property

    Friend Property Price() As Decimal
        Get
            Return _price
        End Get
        Set(ByVal Value As Decimal)
            _price = Value
        End Set
    End Property
    Friend Property Cost() As Single
        Get
            Return _cost
        End Get
        Set(ByVal Value As Single)
            _cost = Value
        End Set
    End Property

    Friend Property CatName() As String
        Get
            Return _name
        End Get
        Set(ByVal Value As String)
            _name = Value
        End Set
    End Property

    Friend Property FoodID() As Integer
        Get
            Return _foodID
        End Get
        Set(ByVal Value As Integer)
            _foodID = Value
        End Set
    End Property

    Friend Property NumberFree() As Integer
        Get
            Return _numberFree
        End Get
        Set(ByVal Value As Integer)
            _numberFree = Value
        End Set
    End Property

    Friend Property Functions() As Integer
        Get
            Return _functions
        End Get
        Set(ByVal Value As Integer)
            _functions = Value
        End Set
    End Property

    Friend Property FunctionGroup() As Integer
        Get
            Return _functionGroup
        End Get
        Set(ByVal Value As Integer)
            _functionGroup = Value
        End Set
    End Property

    Friend Property FunctionFlag() As String
        Get
            Return _functionFlag
        End Get
        Set(ByVal Value As String)
            _functionFlag = Value
        End Set
    End Property

    Friend Property IsPrimary() As Boolean
        Get
            Return _isPrimary
        End Get
        Set(ByVal Value As Boolean)
            _isPrimary = Value
        End Set
    End Property

    Friend Property FoodTableIndex() As Integer
        Get
            Return _foodTableIndex
        End Get
        Set(ByVal Value As Integer)
            _foodTableIndex = Value
        End Set
    End Property

    Friend Property HalfSplit() As Boolean
        Get
            Return _halfSplit
        End Get
        Set(ByVal Value As Boolean)
            _halfSplit = Value
        End Set
    End Property

    Friend Property InvMultiplier() As Decimal
        Get
            Return _InvMultiplier
        End Get
        Set(ByVal Value As Decimal)
            _InvMultiplier = Value
        End Set
    End Property

    Friend Property Extended() As Boolean
        Get
            Return _extended
        End Get
        Set(ByVal value As Boolean)
            _extended = value
        End Set
    End Property

    Friend Property dpMethod() As String
        Get
            Return _dpMethod
        End Get
        Set(ByVal value As String)
            _dpMethod = value
        End Set
    End Property

    Friend Property MainButtonIndex() As Integer
        Get
            Return _mainButtonIndex
        End Get
        Set(ByVal Value As Integer)
            _mainButtonIndex = Value
        End Set
    End Property

    Friend Property ModifierButtonIndex() As Integer
        Get
            Return _modifierButtonIndex
        End Get
        Set(ByVal Value As Integer)
            _modifierButtonIndex = Value
        End Set
    End Property

    Friend Property ModifierMenuIndex() As Integer
        Get
            Return _modifierMenuIndex
        End Get
        Set(ByVal Value As Integer)
            _modifierMenuIndex = Value
        End Set
    End Property

    Friend Property MaxMenuIndex() As Integer
        Get
            Return _maxMenuIndex
        End Get
        Set(ByVal Value As Integer)
            _maxMenuIndex = Value
        End Set
    End Property

    Friend Property CategoryID() As Integer
        Get
            Return _categoryID
        End Get
        Set(ByVal Value As Integer)
            _categoryID = Value
        End Set
    End Property

    Friend Property ActiveDataView() As DataView
        Get
            Return _activeDataView
        End Get
        Set(ByVal Value As DataView)
            _activeDataView = Value
        End Set
    End Property

    Friend Property PreviousID() As Integer
        Get
            Return _previousID
        End Get
        Set(ByVal Value As Integer)
            _previousID = Value
        End Set
    End Property

    Public Sub New(ByVal fontString As String)
        MyBase.New()


        If fontString = "12" Then

            '      Me.FlatAppearance.BorderColor = System.Drawing.Color.SlateGray
            '     Me.FlatAppearance.BorderSize = 2
            '    Me.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            '        Me.Border = BorderStyle.Fixed3D
            '       Me.BorderStyle3D = Border3DStyle.RaisedOuter
            Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf fontString = "10" Then
            Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Else
            Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
        '  Me.Font = New System.Drawing.Font("Comic Sans MS", 10.0, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

    End Sub


End Class
