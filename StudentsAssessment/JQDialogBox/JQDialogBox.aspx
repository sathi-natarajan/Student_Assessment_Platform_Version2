<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="JQDialogBox.aspx.vb" Inherits="Tryout_VB_4_0.JQDialogBox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <%-- <script src="jquery-3.1.1.min.js"></script>--%>
    <script src="http://code.jquery.com/jquery-1.11.2.min.js"></script>
    <link href="jquery.alerts.css" rel="stylesheet" />
    <script src="jquery.alerts.js"></script>
    <script lang="javascript">
        $(document).ready(function () {
            $("#<%=lnkCustomAlert.ClientID()%>").click(function () {
                jAlert('This is a custom alert box', 'Alert Dialog');
                return false;
            });

             $("#<%=lnkConfirm.ClientID()%>").click(function () {
                 jConfirm('Can you confirm this?', 'Confirmation Dialog', function (r) {
                     jAlert('Confirmed: ' + r, 'Confirmation Results');
                 });
                 return false;
             });

             $("#<%=lnkPromptBox.ClientID()%>").click(function () {
                 jPrompt('Type something:', 'Prefilled value', 'Prompt Dialog', function (r) {
                     if (r) jAlert('You entered ' + r);
                     $("#<%=lnkConfirm.ClientID()%>").css("color", "red"); //you do CSS in jQuery this way
                     $("#<%=lblReturnvalue.ClientID()%>").html(r); //This is not working
                     $("#<%=hdnName.ClientID()%>").val(r); //This is working
                     jAlert('The hidden field contains this: ' + $("#<%=hdnName.ClientID()%>").val(), 'Hidden field Results'); //Working
                     /*Summary
                     .val() is working for:
                        hidden fields
                    .html() is working for:
                        labels
                    */
                        
                 });
                 return false;
            });
            
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:linkbutton id="lnkCustomAlert" runat="server">Custom Alert Box</asp:linkbutton><br />
        <asp:linkbutton id="lnkConfirm" runat="server">Confirm Box</asp:linkbutton><br />
        <asp:linkbutton id="lnkPromptBox" runat="server">Prompt Box</asp:linkbutton><br />
        <asp:HiddenField ID="hdnName" runat="server" />
        Return from popup:<asp:Label ID="lblReturnvalue" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
