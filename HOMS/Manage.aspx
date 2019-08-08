<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Manage.aspx.vb" Inherits="HOMS.Manage" %>

<!DOCTYPE html>
<link href="Styles/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
<link href="Styles/css/mdb.min.css" rel="stylesheet" type="text/css" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv='cache-control' content='no-cache'>
    <meta http-equiv='expires' content='0'>
    <meta http-equiv='pragma' content='no-cache'>
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
</head>
<body>
   
   <form id="form1" runat="server">
       <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
       <asp:View ID="mainView" runat="server">
           
       <div class="container" style="margin-top: 15pt;">
        <div class="row">
            <asp:Label ID="Label1" class="alert alert-info col-12" role="alert" runat="server" Text="* Owner Link: http://yoururl.com/QR.aspx?id={ID}">
                 <asp:LinkButton style="float:right;" ID="signOut" runat="server" OnClick="btnsignOut_Click" class="btn btn-warning btn-md">Log out</asp:LinkButton>
            </asp:Label>
        </div>
        <div class="row">
          <div class="col-md">
            <div class="d-flex justify-content-end">
              <div class="custom-file" style="margin-top: 5pt;">
                  <asp:FileUpload accept=".csv" multiple="false" ID="FileUpload1" runat="server" class="custom-file-input" />
                  <label class="custom-file-label" for="inputGroupFile01">Choose file</label>
              </div>
            </div>
          </div>
        </div>
       
        <div class="row">
            <div class="col-lg-12 text-center"> 
                <asp:Button ID="btnsave" runat="server" onclick="btnsave_Click" Text="Upload" class="btn btn-default" />
                <asp:Button ID="btnDelete" style="visibility: hidden; display: none;"  runat="server" onclick="btndelete_Click" Text="Delete" class="btn btn-danger" />
                <a href="" class="btn btn-danger" data-toggle="modal" data-target="#ModalDanger">Delete</a>
                <asp:Button ID="btnExport" runat="server" onclick="btnExport_Click" Text="Export" class="btn btn-info" />
                <asp:Button ID="btnExample" runat="server" onclick="btnExample_Click" Text="Master List" class="btn btn-primary" OnClientClick="target ='_blank';" />
            </div>
        </div>
               
        <div class="row">
            <asp:Label ID="lblmessage" runat="server" class="alert alert-danger col-12" role="alert" />
        </div> 

        <div class="row" style="margin-top: 5pt;">
           <div class="col-lg-12">
             <div class="table-responsive">
               <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" class="table table-hover table-sm table-bordered">
                    <Columns>
                    <asp:BoundField HeaderText="No"  DataField="rowNum" HeaderStyle-CssClass="grey lighten-2"/>
                    <asp:BoundField HeaderText="Unit No"  DataField="unitNo" HeaderStyle-CssClass="grey lighten-2"/>
                    <asp:BoundField HeaderText="Customer Name" DataField="customerName" HeaderStyle-CssClass="grey lighten-2"/>
                    <asp:BoundField HeaderText="Phase Code" DataField="phaseCode" HeaderStyle-CssClass="grey lighten-2"/>
                    <asp:BoundField HeaderText="Phase Name" DataField="phaseName" HeaderStyle-CssClass="grey lighten-2"/>
                    <asp:BoundField HeaderText="Type" DataField="phaseType" HeaderStyle-CssClass="grey lighten-2"/>
                    <asp:BoundField HeaderText="Unit Type" DataField="unitType" HeaderStyle-CssClass="grey lighten-2"/>
                    <asp:BoundField HeaderText="Category" DataField="category" HeaderStyle-CssClass="grey lighten-2"/>
                    <asp:HyperLinkField ControlStyle-ForeColor="#0099ff" HeaderText="URL" Target="_blank" DataNavigateUrlFields="gid" datatextformatstring="/QR.aspx?id={0:c}" DataTextField="gid" DataNavigateUrlFormatString="~/QR.aspx?id={0}" HeaderStyle-CssClass="grey lighten-2" />
                    </Columns>
               </asp:GridView>
            </div>
           </div>
        </div>       
        </div>

      </asp:View>

       <asp:View ID="modalView" runat="server">
           <!--Modal: Login with Avatar Form-->
            <div class="modal fade" id="modalLoginAvatar" data-show="true" role="dialog" aria-labelledby="myModalLabel">
              <div class="modal-dialog cascading-modal modal-avatar modal-sm" role="document">
                <!--Content-->
                <div class="modal-content">

                  <!--Header-->
                  <div class="modal-header">
                    <img src="Styles/blank.jpg" alt="avatar" class="rounded-circle img-responsive">
                  </div>
                  <!--Body-->
                  <div class="modal-body text-center mb-1">

                    <h5 class="mt-1 mb-2">Login</h5>

                    <div class="md-form ml-0 mr-0">
                      <asp:TextBox ID="pwd" TextMode="Password" runat="server" class="form-control form-control-sm" placeholder="Enter password"></asp:TextBox>
                         <div id="captcha" runat="server" style="transform:scale(0.77);-webkit-transform:scale(0.77);transform-origin:0 0;-webkit-transform-origin:0 0;" class="g-recaptcha" data-sitekey=""></div>
                    </div>
                      <asp:Button ID="btnlogin" class="btn btn-cyan mt-1" runat="server" text="Login" OnClick="btnlogin_Click" />
                    </div>
                  </div>

                </div>
                <!--/.Content-->
              </div>
            <!--Modal: Login with Avatar Form-->
       </asp:View>
      </asp:MultiView>


          <div class="modal fade right" id="ModalDanger" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
          <div class="modal-dialog modal-notify modal-danger" role="document">
            <!--Content-->
            <div class="modal-content">
              <!--Header-->
              <div class="modal-header">
                <p class="heading">Delete All Data</p>

                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true" class="white-text">&times;</span>
                </button>
              </div>

              <!--Body-->
              <div class="modal-body">

                <div class="row">
                  <div class="col-12">
                    <p>Are you sure to delete all data ? <p><span class="badge" style="font-size:medium;">New user ID will be created when upload new data</span></p></p>
                  </div>
                </div>
              </div>

              <!--Footer-->
              <div class="modal-footer justify-content-center">
                <button id="deleteYes" type="button" class="btn btn-danger">Yes</button>
                <button type="button" class="btn btn-outline-danger waves-effect" data-dismiss="modal">Cancel</button>
              </div>
            </div>
            <!--/.Content-->
          </div>
        </div>
       
</form>
</body>
</html>

  <script type="text/javascript" src="Styles/js/jquery-3.4.0.min.js"></script>
  <script type="text/javascript" src="Styles/js/popper.min.js"></script>
  <script type="text/javascript" src="Styles/js/bootstrap.min.js"></script>
  <script type="text/javascript" src="Styles/js/mdb.min.js"></script>

<script>
$(document).ready(function() {
    $('#modalLoginAvatar').modal({
        backdrop: 'static',
        keyboard: false
    });
    $("#deleteYes").click(function () {
        $("[id*=btnDelete]").click();
    });
});
</script>