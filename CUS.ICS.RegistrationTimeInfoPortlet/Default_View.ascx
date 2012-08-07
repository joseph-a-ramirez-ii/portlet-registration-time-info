<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default_View.ascx.cs" Inherits="CUS.ICS.RegistrationTimeInfo.Default_View" %>
<%@ Register 
    TagPrefix="common"
	assembly="Jenzabar.Common"
	Namespace="Jenzabar.Common.Web.UI.Controls"
%>

<div class="pSection">
 	<asp:Label id="lblMessage" runat="server" />
 	<br />
 	<br />
 	<asp:Label ID="lblMsg" runat="server" />
     <br />
     
	    <common:ErrorDisplay visible="false" ID="lblError" runat="server" />
</div>