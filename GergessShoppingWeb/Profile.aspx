<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="GergessShoppingWeb.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Profile - Gergess Shopping</title>
    <style>

        body {
            background: #76b852;
            font-family: "Roboto", sans-serif;
            -webkit-font-smoothing: antialiased;
        }
        .navigation {
            display:grid;
            grid-template-areas: "header navButtons";
        }
        .header {
            margin-top:-100px;
            height:60px;
        }
        .navButtons {
            margin-left:auto;
            width:200px;
        }
        .list {
            margin-top:70px;
        }
        table {
          border-collapse: collapse;
          width:100%;
        }
        td, th {
          border: 1px solid #dddddd;
          text-align: center;
          padding: 8px;
        }

        tr:nth-child(odd) {
          background-color: #dddddd;
          
        }

        .Buttons {
            font-family: "Roboto", sans-serif;
            text-transform: uppercase;
            outline: 0;
            background-color:white;
            width: 100%;
            border: 0;
            padding: 15px;
            color: #76b852;
            font-size: 14px;
            cursor: pointer;
        }

        .Buttons:hover, .Buttons:active, .Buttons:focus {
            background: #43A047;
        }
        .textFields {
            font-family: "Roboto", sans-serif;
            outline: 0;
            background: #f2f2f2;
            border-color:black;
            width: 100%;
            border: 0;
            margin: 0 0 15px;
            padding: 10px;
            box-sizing: border-box;
            font-size: 14px;
        }

        .Content {
            display:grid;
            grid-column-gap: 50px;
            grid-auto-columns: 27% 70%;
            grid-template-areas: "profileSection list";
        }
        .profileSection {
            margin-top:85px;
            background: #FFFFFF;
            padding: 30px 30px 45px 30px;
        }

        .Labels {
            font-family: "Roboto", sans-serif;
            font-size: 35px;
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
    <form id="form1" runat="server">
        <div class="navigation">
            <div class="header">
                <p style="font-size:90px;"><strong>Shopping</strong></p>
            </div>
            <div class="navButtons">
                <asp:Button CssClass="Buttons" ID="Button1" runat="server" Text="Continue Shopping" Width="200px" OnClick="Button1_Click" />
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/Logout.png" Height="60" Width="60" OnClick="ImageButton3_Click" /> 
            </div>
        </div>
        <div class="Content">
            <div class="profileSection">
                <asp:Label ID="Label4" runat="server" Text="Profile" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                <br /><br />
                <asp:Label ID="Label1" class="Labels" runat="server" Text="First Name: "></asp:Label>
                <asp:Label ID="firstNameLabel" class="Labels" runat="server"></asp:Label><br />
                <asp:Label ID="Label2" class="Labels" runat="server" Text="Last Name: "></asp:Label>
                <asp:Label ID="lastNameLabel" class="Labels" runat="server"></asp:Label><br />
                <asp:Label ID="Label3" class="Labels" runat="server" Text="Email: "></asp:Label>
                <asp:Label ID="emailLabel" class="Labels" runat="server"></asp:Label>
            </div>
            <div class="list" runat="server">
                <asp:Table ID="Table1" runat="server"></asp:Table>
            </div>
        </div>
        <br />
    </form>
</body>
</html>
