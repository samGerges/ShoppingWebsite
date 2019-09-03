<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CartCheckout.aspx.cs" Inherits="GergessShoppingWeb.CartCheckout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Cart / Checkout - Gergess Shopping</title>
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
            margin-left:auto;
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
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/Profile-icon.png" Height="60" Width="60" OnClick="ImageButton2_Click" /> 
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/Logout.png" Height="60" Width="60" OnClick="ImageButton3_Click" />
                <asp:Button CssClass="Buttons" ID="Button1" runat="server" Text="Continue Shopping" OnClick="Button1_Click" Width="200px" />
            </div>
        </div>
        <div class="list" runat="server">
            <asp:Table ID="Table1" runat="server"></asp:Table>
        </div>
        <br />

        <div style="background-color:white; margin:50px; padding:50px; width:400px;">
            <asp:Label ID="Label1" runat="server" Text="Checkout" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
            <br />
            <br />
            <div>

            </div>
            <div runat="server">
                <asp:DropDownList ID="DropDownList1" class="textFields" runat="server">
                    <asp:ListItem>Mastercard</asp:ListItem>
                    <asp:ListItem>Visa</asp:ListItem>
                    <asp:ListItem>American Express</asp:ListItem>
                    <asp:ListItem>Visa Debit</asp:ListItem>
                    <asp:ListItem>Mastercard Debit</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TextBox1" class="textFields" placeholder="Name on Card" runat="server" ValidationGroup="Checkout"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name on Card is Required" ControlToValidate="TextBox1" ValidationGroup="Checkout"></asp:RequiredFieldValidator>
                <asp:TextBox ID="TextBox2" class="textFields" placeholder="Card Number" runat="server" TextMode="Number" MaxLength="10" ValidationGroup="Checkout"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Card Number is Required" ControlToValidate="TextBox2" ValidationGroup="Checkout"></asp:RequiredFieldValidator>
                <br />
                <asp:Button ID="checkout" class="Buttons" runat="server" Text="Checkout" onClick="checkout_Click" ValidationGroup="Checkout"/>
                
            </div>
        </div>
    </form>
</body>
</html>
