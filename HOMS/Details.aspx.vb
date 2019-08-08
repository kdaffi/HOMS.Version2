Imports System.IO
Imports System.Data.SqlClient
Public Class Landing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim gid = Session("gid")
        Dim bgimg = Session("bgimg")
        Dim sql_result As SqlDataReader

        If bgimg IsNot Nothing Then
            If bgimg <> vbEmpty.ToString() Then
                form1.Style.Add(HtmlTextWriterStyle.BackgroundImage, bgimg)
                form1.Style.Add(HtmlTextWriterStyle.Height, "150em")
                homeOwnerManual.Visible = False
                schematicDrawing.Visible = False
                defectForm.Visible = False
                residentHouseRules.Visible = False
                ownerName.Visible = False
                phaseCode.Visible = False
                lotNo.Visible = False
                address.Visible = False
                unitType.Visible = False
                phaseType.Visible = False
                phaseName.Visible = False
                schematic.Visible = False
            End If
        End If

        If gid <> "" Then
            If Not IsPostBack Then
                homeOwnerManual.Attributes.Add("href", ConfigurationManager.AppSettings("ownerManual").ToString())
                residentHouseRules.Attributes.Add("href", ConfigurationManager.AppSettings("resident_house_rules").ToString())
            End If

            Try
                Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("dbconnection").ConnectionString)
                    Using cmd As New SqlCommand("SELECT a.gid,a.phaseCode,a.phaseName,a.phaseType,a.unitType,a.unitNo,a.salutation,a.customerName,a.category,a.propertyAddress, a.vp7Code, a.vp7, a.schematicDrawing, a.schematicDrawingCode from Phase1A a LEFT JOIN Phase1A b ON a.phaseCode = b.phaseCode AND a.phaseType = b.phaseType AND a.unitNo = b.unitNo AND a.propertyAddress = b.propertyAddress WHERE b.gid = @gid", con)
                        cmd.Parameters.AddWithValue("@gid", gid)
                        con.Open()
                        sql_result = cmd.ExecuteReader()
                        Dim count As Integer = 1
                        Dim ownerNameString As String
                        Dim splitAddress() As String
                        Dim schDrawing As String
                        Dim strRemove As String = "(RENOVATION)"

                        Do While sql_result.Read()
                            If sql_result("customerName") <> "VACANT" Then
                                ownerNameString += count.ToString + ". " + sql_result("salutation") + " " + sql_result("customerName") + "<br />"
                            Else
                                ownerNameString = sql_result("customerName")
                            End If

                            lotNo.Text = sql_result("unitNo")
                            phaseCode.Text = "Phase " + sql_result("phaseCode") + " "
                            schDrawing = sql_result("schematicDrawing")
                            schDrawing = schDrawing.ToLower()
                            schematic.Text = schDrawing
                            phaseType.Text = "Type " + sql_result("phaseType") + " - "
                            phaseName.Text = sql_result("phaseName")
                            unitType.Text = sql_result("unitType")
                            unitType.Text = unitType.Text.Replace(strRemove, "")
                            splitAddress = Split(sql_result("propertyAddress"), ",")
                            address.Text = splitAddress(0) + "," + splitAddress(1) + ",<br/>" + splitAddress(2) + ",<br/>" + splitAddress(3) + "," + splitAddress(4)
                            ownerNameString = ownerNameString.ToLower()
                            ownerName.Text = ownerNameString

                            Select Case sql_result("phaseName").ToString.Trim()
                                Case "DARLINGTON"
                                    imageMain.ImageUrl = ConfigurationManager.AppSettings("imageDARLINGTON").ToString()
                                Case "OAKLEY"
                                    imageMain.ImageUrl = ConfigurationManager.AppSettings("imageOAKLEY").ToString()
                                Case "WINDSOR"
                                    imageMain.ImageUrl = ConfigurationManager.AppSettings("imageWINDSOR").ToString()
                                Case "SHELLEY"
                                    imageMain.ImageUrl = ConfigurationManager.AppSettings("imageSHELLEY").ToString()
                                Case "ALBION"
                                    imageMain.ImageUrl = ConfigurationManager.AppSettings("imageALBION").ToString()
                                Case "TILBURY"
                                    imageMain.ImageUrl = ConfigurationManager.AppSettings("imageTILBURY").ToString()
                            End Select

                            If Not IsPostBack Then
                                Select Case sql_result("vp7Code").ToString.Trim()
                                    Case "A"
                                        Select Case sql_result("vp7").ToString.Trim()
                                            Case "End Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A_CornerEnd").ToString())
                                            Case "Corner Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A_Intermediate").ToString())
                                            Case "Intermediate Lot (RENOVATION) - 1021"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A_1021").ToString())
                                            Case "Intermediate Lot (RENOVATION) - 1038"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A_1038").ToString())
                                            Case "Intermediate Lot (RENOVATION) - 1044"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A_1044").ToString())
                                            Case "Intermediate Lot (RENOVATION) - 1048"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A_1048").ToString())
                                            Case "Intermediate Lot (RENOVATION) - 1049"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A_1049").ToString())
                                        End Select

                                    Case "A1"
                                        Select Case sql_result("vp7").ToString.Trim()
                                            Case "End Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A1_CornerEnd").ToString())
                                            Case "Corner Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A1_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A1_Intermediate").ToString())
                                            Case "End Lot (RENOVATION) - 1037"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A1_1037").ToString())
                                            Case "End Lot (RENOVATION) - 1046"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A1_1046").ToString())
                                            Case "Intermediate Lot (RENOVATION) - 1056"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_A1_1056").ToString())
                                        End Select

                                    Case "B"
                                        Select Case sql_result("vp7").ToString.Trim()
                                            Case "End Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_B_CornerEnd").ToString())
                                            Case "Corner Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_B_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_B_Intermediate").ToString())
                                        End Select

                                    Case "B1"
                                        Select Case sql_result("vp7").ToString.Trim()
                                            Case "End Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_B1_CornerEnd").ToString())
                                            Case "Corner Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_B1_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_B1_Intermediate").ToString())
                                        End Select

                                    Case "C"
                                        Select Case sql_result("vp7").ToString.Trim()
                                            Case "End Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_C_CornerEnd").ToString())
                                            Case "Corner Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_C_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_C_Intermediate").ToString())
                                        End Select

                                    Case "C1"
                                        Select Case sql_result("vp7").ToString.Trim()
                                            Case "End Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_C1_CornerEnd").ToString())
                                            Case "Corner Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_C1_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_C1_Intermediate").ToString())
                                        End Select

                                    Case "D"
                                        Select Case sql_result("vp7").ToString.Trim()
                                            Case "End Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_D_CornerEnd").ToString())
                                            Case "Corner Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_D_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_D_Intermediate").ToString())
                                        End Select

                                    Case "D1"
                                        Select Case sql_result("vp7").ToString.Trim()
                                            Case "End Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_D1_CornerEnd").ToString())
                                            Case "Corner Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_D1_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_D1_Intermediate").ToString())
                                        End Select

                                    Case "E"
                                        Select Case sql_result("vp7").ToString.Trim()
                                            Case "End Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_E_CornerEnd").ToString())
                                            Case "Corner Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_E_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_E_Intermediate").ToString())
                                        End Select

                                    Case "E1"
                                        Select Case sql_result("vp7").ToString.Trim()
                                            Case "End Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_E1_CornerEnd").ToString())
                                            Case "Corner Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_E1_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_E1_Intermediate").ToString())
                                        End Select

                                    Case "F"
                                        Select Case sql_result("vp7").ToString.Trim()
                                            Case "End Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_F_CornerEnd").ToString())
                                            Case "Corner Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_F_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_F_Intermediate").ToString())
                                        End Select

                                    Case "F1"
                                        Select Case sql_result("vp7").ToString.Trim()
                                            Case "End Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_F1_CornerEnd").ToString())
                                            Case "Corner Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_F1_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                defectForm.Attributes.Add("href", ConfigurationManager.AppSettings("defectForm_F1_Intermediate").ToString())
                                        End Select

                                End Select

                                'SCHEMATIC DRAWING'
                                Select Case sql_result("schematicDrawingCode").ToString.Trim()
                                    Case "A/A1"
                                        Select Case sql_result("unitType").ToString.Trim()
                                            Case "Corner Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_A_CornerEnd").ToString())
                                            Case "End Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_A_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_A_Intermediate").ToString())
                                            Case "End Lot (RENOVATION)"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_A_CornerEnd_renovate").ToString())
                                            Case "Intermediate Lot (RENOVATION)"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_A_Intermediate_renovate").ToString())
                                        End Select

                                    Case "B/B1"
                                        Select Case sql_result("unitType").ToString.Trim()
                                            Case "Corner Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_B_CornerEnd").ToString())
                                            Case "End Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_B_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_B1_Intermediate").ToString())
                                        End Select

                                    Case "C/C1"
                                        Select Case sql_result("unitType").ToString.Trim()
                                            Case "Corner Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_C_CornerEnd").ToString())
                                            Case "End Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_C_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_C1_Intermediate").ToString())
                                        End Select

                                    Case "D/D1"
                                        Select Case sql_result("unitType").ToString.Trim()
                                            Case "Corner Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_DD1_CornerEnd").ToString())
                                            Case "End Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_DD1_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_DD1_CornerEnd").ToString())
                                        End Select

                                    Case "E/E1"
                                        Select Case sql_result("unitType").ToString.Trim()
                                            Case "Corner Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_EE1_CornerEnd").ToString())
                                            Case "End Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_EE1_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_EE1_CornerEnd").ToString())
                                        End Select

                                    Case "F/F1"
                                        Select Case sql_result("unitType").ToString.Trim()
                                            Case "Corner Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_FF1_CornerEnd").ToString())
                                            Case "End Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_FF1_CornerEnd").ToString())
                                            Case "Intermediate Lot"
                                                schematicDrawing.Attributes.Add("href", ConfigurationManager.AppSettings("schematic_FF1_CornerEnd").ToString())
                                        End Select
                                End Select

                            End If

                            count += 1
                        Loop

                        cmd.Parameters.Clear()
                        con.Close()
                    End Using
                End Using
            Catch ex As Exception
                lblmessage.Text = ex.Message
            End Try
        Else
            form1.Visible = False
            Response.Redirect("Unauthorized.aspx")
        End If

    End Sub

End Class