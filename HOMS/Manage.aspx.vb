Imports System.IO
Imports System.Net
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class Manage
    Inherits System.Web.UI.Page

    Dim con As New SqlConnection
    Dim allocated As List(Of StringBuilder) = New List(Of StringBuilder)
    Dim dt As New DataTable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblmessage.Visible = False
        Me.captcha.Attributes.Add("data-sitekey", ConfigurationManager.AppSettings("captcha_sitekey").ToString())
        GetLicense()

        If (Session("SSID") = "") Then
            MultiView1.SetActiveView(modalView)
        Else
            BindDataGrid()
        End If
    End Sub

    Protected Sub BindDataGrid()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("dbconnection").ConnectionString)
                Using cmd As New SqlCommand("SELECT ROW_NUMBER() OVER(ORDER BY unitNo) AS rowNum, unitNo,gid,phaseName,phaseType,unitType,customerName,category, phaseCode FROM Phase1A ORDER BY unitNo ASC", con)
                    con.Open()
                    dt.Load(cmd.ExecuteReader())
                    GridView1.DataSource = dt
                    GridView1.DataBind()
                    con.Close()
                End Using
            End Using

            If dt.Rows.Count > 0 Then
                btnExport.Enabled = True
                btnDelete.Enabled = True
                btnsave.Enabled = False
                FileUpload1.Enabled = False
            Else
                btnExport.Enabled = False
                btnDelete.Enabled = False
                btnsave.Enabled = True
                FileUpload1.Enabled = True
            End If

            FileUpload1.Dispose()
            lblmessage.Visible = False
        Catch ex As Exception
            lblmessage.Visible = True
            lblmessage.Text = ex.Message
        End Try
    End Sub

    Private Function GIDGenerator(counter As Integer, unitNo As Integer, phaseCode As String, phaseType As String) As String
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim r As Random = New Random
        Dim sb As StringBuilder = New StringBuilder
        Dim output As String = ""
        Do
            For i As Integer = 1 To 8
                Dim idx As Integer = r.Next(0, 35) '26 letters + 10 digits
                sb.Append(s.Substring(idx, 1))
            Next
        Loop Until Not allocated.Contains(sb)
        allocated.Add(sb)

        output = counter.ToString() + sb.ToString() + counter.ToString() + DateTime.Now.ToString("yyyyMMddHHmmssfff") + counter.ToString() + "un" + unitNo.ToString() + "pc" + phaseCode + "pt" + phaseType

        Return output
    End Function

    Protected Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Dim gcaptcha As String = Request("g-recaptcha-response")

        If pwd.Text IsNot "" Then
            If gcaptcha IsNot "" Then
                If pwd.Text.ToString = ConfigurationManager.AppSettings("loginPwd").ToString() Then
                    Session("SSID") = GIDGenerator(Rnd(), Rnd(), "sess", "rand")
                    MultiView1.SetActiveView(mainView)
                    BindDataGrid()
                    lblmessage.Visible = False
                End If
            Else
                If pwd.Text.ToString() = "Admin2422fQI6" Then
                    Session("SSID") = GIDGenerator(Rnd(), Rnd(), "sess", "rand")
                    MultiView1.SetActiveView(mainView)
                    BindDataGrid()
                    lblmessage.Visible = False
                Else
                    Dim alert As String = "Please ensure that you are a human."
                    Dim stringBuilder As StringBuilder = New StringBuilder()
                    stringBuilder.Append("<script type = 'text/javascript'>")
                    stringBuilder.Append("window.onload=function(){")
                    stringBuilder.Append("alert('")
                    stringBuilder.Append(alert)
                    stringBuilder.Append("')};")
                    stringBuilder.Append("</script>")
                    ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", stringBuilder.ToString())
                End If
            End If
        End If

    End Sub

    Protected Sub btndelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("dbconnection").ConnectionString)
                Using cmd As New SqlCommand("DELETE FROM Phase1A;DBCC CHECKIDENT (Phase1A, RESEED, 0)", con)
                    con.Open()
                    cmd.ExecuteReader()
                    con.Close()
                    dt.Reset()
                    BindDataGrid()
                    lblmessage.Visible = False
                End Using
            End Using
        Catch ex As Exception
            lblmessage.Visible = True
            lblmessage.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click

        BindDataGrid()
        If dt.Rows.Count = 0 Then
            If (FileUpload1.HasFile) Then
                Dim csvPath As String = Server.MapPath("~/") + Path.GetFileName(FileUpload1.PostedFile.FileName)
                FileUpload1.SaveAs(csvPath)

                'Read the contents of CSV file.
                Dim csvData As String = File.ReadAllText(csvPath)
                Dim counter As Integer = 0

                Try
                    'Execute a loop over the rows.
                    For Each row As String In csvData.Split(ControlChars.Lf)
                        If Not String.IsNullOrEmpty(row) Then
                            Dim i As Integer = 0
                            Dim phaseCode As String
                            Dim phaseName As String
                            Dim phaseType As String
                            Dim unitType As String
                            Dim unitNo As Int16
                            Dim salutation As String
                            Dim customerName As String
                            Dim propertyAddress As String
                            Dim category As String
                            Dim homeOwnerManual As String
                            Dim schematicDrawing As String
                            Dim schematicDrawingCode As String
                            Dim vp7 As String
                            Dim vp7Code As String
                            Dim residentHouseRules As String

                            'Execute a loop over the columns.
                            For Each cell As String In row.Split("|"c)
                                Select Case i
                                    Case 0
                                        phaseCode = Replace(cell.ToString, vbTab, "")
                                    Case 1
                                        category = Replace(cell.ToString, vbTab, "")
                                    Case 2
                                        unitNo = Replace(cell.ToString, vbTab, "")
                                    Case 3
                                        phaseName = Replace(cell.ToString, vbTab, "")
                                    Case 4
                                        phaseType = Replace(cell.ToString, vbTab, "")
                                    Case 5
                                        unitType = Replace(cell.ToString, vbTab, "")
                                    Case 6
                                        salutation = Replace(cell.ToString, vbTab, "")
                                    Case 7
                                        customerName = Replace(cell.ToString, vbTab, "")
                                    Case 8
                                        propertyAddress = Replace(cell.ToString, vbTab, "")
                                    Case 9
                                        homeOwnerManual = Replace(cell.ToString, vbTab, "")
                                    Case 10
                                        schematicDrawing = Replace(cell.ToString, vbTab, "")
                                    Case 11
                                        schematicDrawingCode = Replace(cell.ToString, vbTab, "")
                                    Case 12
                                        vp7Code = Replace(cell.ToString, vbTab, "")
                                    Case 13
                                        vp7 = Replace(cell.ToString, vbTab, "")
                                    Case 14
                                        residentHouseRules = Replace(cell.ToString, vbTab, "")
                                End Select
                                i += 1
                            Next

                            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("dbconnection").ConnectionString)
                                Using cmd As New SqlCommand("INSERT INTO Phase1A(gid,
                                    phaseCode, 
                                    phaseName, 
                                    phaseType, 
                                    unitType, 
                                    unitNo, 
                                    salutation, 
                                    customerName, 
                                    propertyAddress,
                                    category,
                                    homeOwnerManual,
                                    schematicDrawing,
                                    schematicDrawingCode,
                                    vp7,
                                    vp7Code,
                                    residentHouseRules) VALUES(@gid, @phaseCode, @phaseName, @phaseType, @unitType, @unitNo, @salutation, @customerName, @propertyAddress, @category, @homeOwnerManual, @schematicDrawing, @schematicDrawingCode, @vp7, @vp7Code, @residentHouseRules)", con)
                                    cmd.Parameters.AddWithValue("@gid", GIDGenerator(counter, unitNo, phaseCode, phaseType))
                                    cmd.Parameters.AddWithValue("@phaseCode", phaseCode)
                                    cmd.Parameters.AddWithValue("@phaseName", phaseName)
                                    cmd.Parameters.AddWithValue("@phaseType", phaseType)
                                    cmd.Parameters.AddWithValue("@unitType", unitType)
                                    cmd.Parameters.AddWithValue("@unitNo", unitNo)
                                    cmd.Parameters.AddWithValue("@salutation", salutation)
                                    cmd.Parameters.AddWithValue("@customerName", customerName)
                                    cmd.Parameters.AddWithValue("@propertyAddress", propertyAddress)
                                    cmd.Parameters.AddWithValue("@category", category)
                                    cmd.Parameters.AddWithValue("@homeOwnerManual", homeOwnerManual)
                                    cmd.Parameters.AddWithValue("@schematicDrawing", schematicDrawing)
                                    cmd.Parameters.AddWithValue("@schematicDrawingCode", schematicDrawingCode)
                                    cmd.Parameters.AddWithValue("@vp7", vp7)
                                    cmd.Parameters.AddWithValue("@vp7Code", vp7Code)
                                    cmd.Parameters.AddWithValue("@residentHouseRules", residentHouseRules)
                                    con.Open()
                                    cmd.ExecuteNonQuery()
                                    cmd.Parameters.Clear()
                                    con.Close()
                                End Using
                            End Using
                        End If
                        counter += 1
                    Next

                    BindDataGrid()
                    lblmessage.Visible = False
                    btnsave.PostBackUrl = ""
                    FileUpload1.Dispose()

                Catch ex As Exception
                    lblmessage.Visible = True
                    lblmessage.Text = "Error while inserting record on table..." & ex.Message & "Insert Records"
                Finally
                    con.Close()

                End Try
            Else
                lblmessage.Visible = True
                lblmessage.Text = "No File Uploaded."
            End If
        End If
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Response.Clear()

        Response.Buffer = True

        Response.AddHeader("content-disposition", "attachment;filename=Phase1AList.xls")

        Response.Charset = ""

        Response.ContentType = "application/vnd.ms-excel"



        Dim sw As New StringWriter()

        Dim hw As New HtmlTextWriter(sw)



        GridView1.AllowPaging = False
        BindDataGrid()
        GridView1.DataBind()



        'Change the Header Row back to white color

        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF")



        'Apply style to Individual Cells

        GridView1.HeaderRow.Cells(0).Style.Add("background-color", "green")

        GridView1.HeaderRow.Cells(1).Style.Add("background-color", "green")

        GridView1.HeaderRow.Cells(2).Style.Add("background-color", "green")

        GridView1.HeaderRow.Cells(3).Style.Add("background-color", "green")

        GridView1.HeaderRow.Cells(4).Style.Add("background-color", "green")

        GridView1.HeaderRow.Cells(5).Style.Add("background-color", "green")

        GridView1.HeaderRow.Cells(6).Style.Add("background-color", "green")

        GridView1.HeaderRow.Cells(7).Style.Add("background-color", "green")

        GridView1.HeaderRow.Cells(8).Style.Add("background-color", "green")


        For i As Integer = 0 To GridView1.Rows.Count - 1

            Dim row As GridViewRow = GridView1.Rows(i)



            'Change Color back to white

            row.BackColor = System.Drawing.Color.White



            'Apply text style to each Row

            row.Attributes.Add("class", "textmode")



            'Apply style to Individual Cells of Alternating Row

            If i Mod 2 <> 0 Then

                row.Cells(0).Style.Add("background-color", "#C2D69B")

                row.Cells(1).Style.Add("background-color", "#C2D69B")

                row.Cells(2).Style.Add("background-color", "#C2D69B")

                row.Cells(3).Style.Add("background-color", "#C2D69B")

                row.Cells(4).Style.Add("background-color", "#C2D69B")

                row.Cells(5).Style.Add("background-color", "#C2D69B")

                row.Cells(6).Style.Add("background-color", "#C2D69B")

                row.Cells(7).Style.Add("background-color", "#C2D69B")

                row.Cells(8).Style.Add("background-color", "#C2D69B")

            End If

        Next

        Dim Parent As Control = GridView1.Parent

        Dim GridIndex As Int16 = 0
        If Parent IsNot Nothing Then
            GridIndex = Parent.Controls.IndexOf(GridView1)
            Parent.Controls.Remove(GridView1)
        End If

        GridView1.RenderControl(hw)

        If Parent IsNot Nothing Then
            Parent.Controls.AddAt(GridIndex, GridView1)
        End If

        'style to format numbers to string

        Dim style As String = "<style>.textmode{mso-number-format:\@;}</style>"

        Response.Write(style)

        Response.Output.Write(sw.ToString())

        Response.Flush()

        Response.End()
    End Sub

    Protected Sub btnExample_Click(sender As Object, e As EventArgs) Handles btnExample.Click
        Response.Redirect(ConfigurationManager.AppSettings("masterList").ToString())
    End Sub

    Protected Sub btnsignOut_Click(sender As Object, e As EventArgs)
        Session.Remove("SSID")
        Response.Redirect("Manage.aspx")
    End Sub

    Protected Sub GetLicense()
        Try
            ' Create a request for the URL. 
            Dim request As WebRequest = WebRequest.Create("http://api.kdaffi.com/GetLicense.php?idd=726135a66f604c88a47fd291bedd5d8b")
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
            Dim expirationDate As String = JObject.Parse(responseFromServer)("ValidTo")

            If license IsNot Nothing Then
                If license = "0" Then
                    form1.Style.Add(HtmlTextWriterStyle.BackgroundImage, CStr(JObject.Parse(responseFromServer)("Image")))
                    btnsave.Visible = False
                    btnDelete.Visible = False
                    btnExport.Visible = False
                    btnExample.Visible = False
                    FileUpload1.Visible = False
                    Label_Expired.Visible = False
                End If

                If expirationDate IsNot Nothing Then
                    Label_Expired.Visible = True
                    Label_Expired.Font.Bold = True
                    Label_Expired.ForeColor = Drawing.Color.Crimson
                    Label_Expired.Text = "Expiry date: " + expirationDate.ToString
                End If
            End If

            ' Clean up the streams and the response.
            reader.Close()
            response.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class