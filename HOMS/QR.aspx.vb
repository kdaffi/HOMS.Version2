Imports System.Net
Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class Owner
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetLicense()
        Session("gid") = Request.QueryString("id")
        Response.Redirect("Details.aspx")
    End Sub

    Protected Sub GetLicense()
        Try
            ' Create a request for the URL. 
            'Static Dim idd = "726135a66f604c88a47fd291bedd5d8b" 'PRODUCTION
            Static Dim idd = "c3aea2d7fc6840e6bebe324fcb14c0cd" 'TESTING
            Dim request As WebRequest = WebRequest.Create("http://api.kdaffi.com/GetLicense.php?idd=" + idd.ToString)
            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            ' Get the response.
            Dim response As WebResponse = request.GetResponse()
            ' Display the status.
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            ' Get the stream containing content returned by the server.
            Dim dataStream As Stream = response.GetResponseStream()
            ' Open the stream using a StreamReader for easy access.
            Dim reader As New StreamReader(dataStream)
            ' Read the content.
            Dim responseFromServer As String = reader.ReadToEnd()
            ' Display the content.
            Dim license As String = JObject.Parse(responseFromServer)("Active")

            If license IsNot Nothing Then
                If license = "0" Then
                    Session("bgimg") = CStr(JObject.Parse(responseFromServer)("Image"))
                Else
                    Session("bgimg") = vbEmpty.ToString()
                End If
            Else
                Session("bgimg") = vbEmpty.ToString()
            End If

            ' Clean up the streams and the response.
            reader.Close()
            response.Close()
        Catch ex As Exception

        End Try
    End Sub

End Class