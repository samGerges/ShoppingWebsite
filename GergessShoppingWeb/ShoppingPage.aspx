<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingPage.aspx.cs" Inherits="GergessShoppingWeb.ShoppingPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Shopping - Gergess Shopping</title>
    <style>
        body {
            background: #76b852;
            font-family: "Roboto", sans-serif;
            -webkit-font-smoothing: antialiased;
        }
        .navigation {
            display:grid;
            height:60px;
            grid-template-areas: "header navButtons";
        }
        .header {
            margin-top:-100px;
            height:60px;
        }
        .navButtons {
            margin-left: auto;
            width:200px;
        }
        .list {
            margin-top:70px;
        }
        table {
          border-collapse: collapse;
          width: 100%;
        }
        td, th {
          border: 1px solid #dddddd;
          text-align: center;
          padding: 8px;
        }

        tr:nth-child(odd) {
          background-color: #dddddd;
        }
        @media screen and (max-width:768px){
            .header {
            }
            .list {
            }
        }
    </style>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@8"></script>
</head>
<body>
    <form id="form1" class="" runat="server">
        <div class="navigation">
            <div class="header">
                <p style="font-size:90px;"><strong>Shopping</strong></p>
            </div>
            <div class="navButtons">
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/Profile-icon.png" Height="60" Width="60" OnClick="ImageButton2_Click" />
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Artboard_8-512.png" Height="60" Width="60" OnClick="ImageButton1_Click" /> 
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/Logout.png" Height="60" Width="60" OnClick="ImageButton3_Click" /> 
            </div>
        </div>
        <div class="list" runat="server">
            <asp:Table ID="Table1" runat="server"></asp:Table>
        </div>
    </form>
</body>
</html>
