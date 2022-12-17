Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel.DataAnnotations
Imports System.IO

Namespace DXSample

    Public Class DataSource

        Public Property CurrentItem As Contact

        Public Sub New()
            CurrentItem = New Contact With {.FirstName = "Carolyn", .LastName = "Baker", .Phone = "(555)349-3010", .Address = "1198 Theresa Cir", .City = "Whitinsville", .State = "MA", .Zip = "01582"}
            CurrentItem.Photo = GetPhoto(CurrentItem)
        End Sub

        Private Function GetPhoto(ByVal contact As Contact) As Byte()
            Return GetPhoto(contact.FirstName & contact.LastName & ".jpg")
        End Function

        Private Function GetPhoto(ByVal name As String) As Byte()
            Return File.ReadAllBytes("Images\" & name)
        End Function
    End Class

    <MetadataType(GetType(ContactMetadata))>
    Public Class Contact

        Public Property FirstName As String

        Public Property LastName As String

        Public Property Email As String

        Public Property Phone As String

        Public Property Address As String

        Public Property City As String

        Public Property State As String

        Public Property Zip As String

        Public Property Photo As Object
    End Class

    Public Class ContactMetadata

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of Contact))
            builder.DataFormLayout().Group("<>").ContainsProperty(Function(x) x.Photo).EndGroup().GroupBox("General").ContainsProperty(Function(x) x.FirstName).ContainsProperty(Function(x) x.LastName).EndGroup().TabbedGroup("Info").Group("Contacts").ContainsProperty(Function(x) x.Phone).ContainsProperty(Function(x) x.Email).EndGroup().Group("Address").ContainsProperty(Function(x) x.State).ContainsProperty(Function(x) x.City).ContainsProperty(Function(x) x.Address).ContainsProperty(Function(x) x.Zip).EndGroup().EndGroup()
            builder.Property(Function(x) x.Photo).LayoutControlEditor("ImageTemplate").DisplayName(String.Empty)
            builder.Property(Function(x) x.Email).NullDisplayText("<empty>")
            builder.[Property](Function(x) x.City).RegExMask("\w{1,25}")
        End Sub
    End Class
End Namespace
