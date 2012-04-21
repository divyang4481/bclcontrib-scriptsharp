<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Standard.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Ajax.InitializeScriptlets(); %>
    <% Ajax.AddScriptReference("Script.WebEx"); %>
    <% Ajax.AddScriptReference("ScriptApp"); %>
    <% Ajax.RenderScriptlets(); %>
    <h2>
        Index</h2>
</asp:Content>
