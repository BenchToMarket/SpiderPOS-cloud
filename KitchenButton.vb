Friend Class KitchenButton
    Inherits Button

    Private _id As Integer
    Private _index As Integer
    Private _payRate As Decimal

    Sub New(ByVal _text As String, ByVal W As Integer, ByVal H As Integer, ByVal cBack As Color, ByVal cFore As Color)
        Text = _text
        Width = W
        Height = H
        BackColor = cBack
        ForeColor = cFore
        Font = New Font("Comic Sans MS", 12.0)

    End Sub

    Sub New()

    End Sub

    Friend Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal Value As Integer)
            _id = Value
        End Set
    End Property

    Friend Property ButtonIndex() As Integer
        Get
            Return _index
        End Get
        Set(ByVal Value As Integer)
            _index = Value
        End Set
    End Property

    Friend Property PayRate() As Decimal
        Get
            Return _payRate
        End Get
        Set(ByVal Value As Decimal)
            _payRate = Value
        End Set
    End Property

End Class





'********************
'   AvailTableButton

Friend Class AvailTableButton
    Inherits Button

    Private _experienceNumber As Int64
    Private _numberOfCustomers As Integer
    Private _numberOfChecks As Integer
    Private _currentMenu As Integer
    Private _tabID As Int64
    Private _empID As Integer

    Sub New()

    End Sub

    Friend Property ExperienceNumber() As Int64
        Get
            Return _experienceNumber
        End Get
        Set(ByVal Value As Int64)
            _experienceNumber = Value
        End Set
    End Property

    Friend Property NumberOfCustomers() As Integer
        Get
            Return _numberOfCustomers
        End Get
        Set(ByVal Value As Integer)
            _numberOfCustomers = Value
        End Set
    End Property

    Friend Property NumberOfChecks() As Integer
        Get
            Return _numberOfChecks
        End Get
        Set(ByVal Value As Integer)
            _numberOfChecks = Value
        End Set
    End Property

    Friend Property CurrentMenu() As Integer
        Get
            Return _currentMenu
        End Get
        Set(ByVal Value As Integer)
            _currentMenu = Value
        End Set
    End Property

    Friend Property TabID() As Int64
        Get
            Return _tabID
        End Get
        Set(ByVal Value As Int64)
            _tabID = Value
        End Set
    End Property

    Friend Property EmpID() As Integer
        Get
            Return _empID
        End Get
        Set(ByVal Value As Integer)
            _empID = Value
        End Set
    End Property

End Class

'*****************
'   clockIn Button
'   *****************
'   no longer using

Friend Class ClockInButton
    Inherits Button

    Private _empID As Integer
    Private _passcodeID As Integer
    Private _jobCodeID As Integer

    Friend Property EmpID() As Integer
        Get
            Return _empID
        End Get
        Set(ByVal Value As Integer)
            _empID = Value
        End Set
    End Property

    Friend Property PassCodeID() As Integer
        Get
            Return _passcodeID
        End Get
        Set(ByVal Value As Integer)
            _passcodeID = Value
        End Set
    End Property

    Friend Property JobCodeID() As Integer
        Get
            Return _jobCodeID
        End Get
        Set(ByVal Value As Integer)
            _jobCodeID = Value
        End Set
    End Property

End Class