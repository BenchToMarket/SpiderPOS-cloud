Imports System.IO                       '??
Imports System.Xml                      '??
Imports System.Text
Imports System.Xml.Serialization        '??

Public Class TStream

    <XmlElementAttribute(IsNullable:=False)> Public Transaction As PreAuthTransactionClass
    <XmlElementAttribute(IsNullable:=False)> Public Admin As AdminClass

    Public Sub ToXML()
        Dim output As New StringWriter(New StringBuilder)
        Dim s As New XmlSerializer(Me.GetType())
        s.Serialize(output, Me)
    End Sub

End Class

Public Class PrePaidTransactionClass
    'everything is the same for PrePaids, except TranType

    Public IpPort As String
    Public MerchantID As String
    Public OperatorID As String
    Public TranType As String
    <XmlElementAttribute(IsNullable:=False)> Public Duplicate As String
    Public TranCode As String
    Public InvoiceNo As String
    Public RefNo As String
    '   Public Memo As String
    Public Account As AccountClass
    Public Amount As PreAuthAmountClass
    <XmlElementAttribute(IsNullable:=False)> Public TranInfo As TranInfoClass 'used for Voids

End Class

Public Class PreAuthTransactionClass

    <XmlElementAttribute(IsNullable:=False)> Public IpPort As String
    Public MerchantID As String
    Public OperatorID As String
    Public TranType As String
    <XmlElementAttribute(IsNullable:=False)> Public Duplicate As String
    Public TranCode As String
    Public InvoiceNo As String
    Public RefNo As String
    <XmlElementAttribute(IsNullable:=False)> Public TerminalName As String
    Public Memo As String
    Public Account As AccountClass
    Public Amount As PreAuthAmountClass
    <XmlElementAttribute(IsNullable:=False)> Public TranInfo As TranInfoClass

End Class

Public Class PreAuthCaptureTransactionClass

    Public MerchantID As String
    Public OperatorID As String
    Public TranType As String
    <XmlElementAttribute(IsNullable:=False)> Public Duplicate As String
    Public TranCode As String
    Public InvoiceNo As String
    Public RefNo As String
    Public Memo As String
    Public Account As AccountClass
    Public Amount As PreAuthAmountClass
    Public TranInfo As TranInfoClass

End Class

Public Class AccountClass

    <XmlElementAttribute(IsNullable:=False)> Public Track2 As String
    <XmlElementAttribute(IsNullable:=False)> Public Name As String
    <XmlElementAttribute(IsNullable:=False)> Public AcctNo As String
    <XmlElementAttribute(IsNullable:=False)> Public ExpDate As String

End Class

Public Class PreAuthAmountClass

    <XmlElementAttribute(IsNullable:=False)> Public Purchase As String
    <XmlElementAttribute(IsNullable:=False)> Public Gratuity As String
    <XmlElementAttribute(IsNullable:=False)> Public Authorize As String
    <XmlElementAttribute(IsNullable:=False)> Public AuthCode As String
    <XmlElementAttribute(IsNullable:=False)> Public AcqRefData As String

End Class

Public Class TranInfoClass

    Public AuthCode As String
    Public AcqRefData As String

End Class


Public Class AdminClass

    <XmlElementAttribute(IsNullable:=False)> Public MerchantID As String
    <XmlElementAttribute(IsNullable:=False)> Public OperatorID As String
    <XmlElementAttribute(IsNullable:=False)> Public TranCode As String
    <XmlElementAttribute(IsNullable:=False)> Public Memo As String
    <XmlElementAttribute(IsNullable:=False)> Public BatchNo As String
    <XmlElementAttribute(IsNullable:=False)> Public BatchItemCount As String
    <XmlElementAttribute(IsNullable:=False)> Public NetBatchTotal As String
    <XmlElementAttribute(IsNullable:=False)> Public CreditPurchaseCount As String
    <XmlElementAttribute(IsNullable:=False)> Public CreditPurchaseAmount As String
    <XmlElementAttribute(IsNullable:=False)> Public CreditReturnCount As String
    <XmlElementAttribute(IsNullable:=False)> Public CreditReturnAmount As String
    <XmlElementAttribute(IsNullable:=False)> Public DebitPurchaseCount As String
    <XmlElementAttribute(IsNullable:=False)> Public DebitPurchaseAmount As String
    <XmlElementAttribute(IsNullable:=False)> Public DebitReturnCount As String
    <XmlElementAttribute(IsNullable:=False)> Public DebitReturnAmount As String

End Class