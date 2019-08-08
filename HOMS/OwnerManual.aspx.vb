Public Class OwnerManual
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim gid = Session("gid")
        If gid <> "" Then
            form1.Visible = True
        Else
            form1.Visible = False
            Response.Redirect("Unauthorized.aspx")
        End If
    End Sub

End Class