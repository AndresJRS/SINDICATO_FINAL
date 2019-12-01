<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Deducciones.aspx.cs" Inherits="Sindicato_v1.Deducciones" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <link href="Content/Site.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet" type="text/css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<style>
    .navbar-default {
        background-color: #04539b;
        border-color: #e7e7e7;
    }
</style>
<body>
    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#inicio">SIICE</a>
            </div>
        </div>
    </nav>
    <form id="form1" runat="server" class="container" enctype="multipart/form-data">
        <br />
        <br />
        <div class="row">
            <asp:Panel ID="panel_1" runat="server" Height="507px" BorderStyle="None" Width="1618px">
                <br />
                <h2 style="text-align: center; color: #04539b; height: 37px; width: 488px; margin-left: 313px;">Gestion de deducciones </h2>
                <br />
                <asp:GridView ID="dgv_Deduc" runat="server" Style="margin-left: 347px; margin-top: 0px" Width="1014px">
                </asp:GridView>
                <br />
                <br />
                <asp:Panel ID="Panel3" runat="server" Height="48px" Width="1570px">
                    <asp:FileUpload ID="fUpload" runat="server" Height="41px" Style="margin-bottom: 4px" Width="1018px" />
                </asp:Panel>
                <br />
                <asp:Panel ID="Panel2" runat="server" Height="45px" Width="1572px">
                    <asp:Button ID="btn_Cargar" runat="server" Height="36px" OnClick="btn_Cargar_Click" Style="margin-top: 0px" Text="Cargar" Width="1018px" />
                </asp:Panel>
                <br />
                <asp:Panel ID="Panel1" runat="server" Height="41px" Width="1572px">
                    <asp:Button ID="btn_Confirmar" runat="server" Height="34px" OnClick="btn_Confirmar_Click" Text="Confirmar registro" Width="1018px" />
                </asp:Panel>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
