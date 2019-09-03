<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="GergessShoppingWeb.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Signup/Login - Gergess Shopping</title>
    <style>
        @import url(https://fonts.googleapis.com/css?family=Roboto:300);

        .login-page {
            align-content: center;
            border-radius: 25px;
            display: grid;
            grid-template-areas: "  login signup";
            grid-gap: 50px;
            margin: 50px;
            padding: 20px;
        }

        .login {
            margin-top: 50px;
        }

        @media screen and (max-width: 768px) {
            .login {
                margin-top: 0;
            }

            .login-page {
                grid-template-columns: 100%;
                grid-template-areas: "  login   " "  signup    ";
                margin: 5px 5px 5px 5px;
            }
        }

        .form {
            background: #FFFFFF;
            padding: 0px 45px 45px 45px;
            text-align: center;
            box-shadow: 0 0 20px 0 rgba(0, 0, 0, 0.2), 0 5px 5px 0 rgba(0, 0, 0, 0.24);
        }

        .textFields {
            font-family: "Roboto", sans-serif;
            outline: 0;
            background: #f2f2f2;
            width: 100%;
            border: 0;
            margin: 0 0 15px;
            padding: 10px;
            box-sizing: border-box;
            font-size: 14px;
        }

        .Buttons {
            font-family: "Roboto", sans-serif;
            text-transform: uppercase;
            outline: 0;
            background: #4CAF50;
            width: 100%;
            border: 0;
            padding: 15px;
            color: #FFFFFF;
            font-size: 14px;
            cursor: pointer;
        }

        .Buttons:hover, .Buttons:active, .Buttons:focus {
            background: #43A047;
        }

        .form .message {
            margin: 15px 0 0;
            color: #b3b3b3;
            font-size: 12px;
        }

        .form .message a {
            color: #4CAF50;
            text-decoration: none;
        }

        body {
            background: #76b852;
            font-family: "Roboto", sans-serif;
            -webkit-font-smoothing: antialiased;
        }
    </style>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@8"></script>
</head>
<body>
    <h1 style="text-align: center; font-size: 50px; color: white;">Gergess Shopping</h1>
    <form style="width: auto" runat="server">
        <div class="login-page">
            <div class="login">
                <h1><strong>LOGIN</strong></h1>
                <asp:TextBox ID="LoginEmailTB" class="textFields" placeholder="email" TextMode="Email" runat="server"></asp:TextBox>
                <asp:TextBox ID="LoginPassTB" class="textFields" placeholder="password" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="LoginPassTB" ErrorMessage="Password is required" Font-Bold="True" Font-Size="X-Large" ForeColor="Black"></asp:RequiredFieldValidator>
                <asp:Button ID="loginBTN" runat="server" CssClass="Buttons" Text="Login" OnClick="loginBTN_Click" />

            </div>
            <div class="signup">
                <h1><strong>SIGNUP</strong></h1>
                <p>If you don't have am account, you can create one from here</p>
                <asp:TextBox ID="firstnameTB" class="textFields" placeholder="first name" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="firstnameTB" Display="Dynamic" ErrorMessage="first name required" Font-Bold="True" Font-Size="X-Large" ForeColor="Black" ValidationGroup="signup"></asp:RequiredFieldValidator>
                <asp:TextBox ID="lastnameTB" class="textFields" placeholder="last name" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lastnameTB" Display="Dynamic" ErrorMessage="last name required" Font-Bold="True" Font-Size="X-Large" ValidationGroup="signup"></asp:RequiredFieldValidator>
                <asp:TextBox ID="passTB" class="textFields" placeholder="password" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="passTB" Display="Dynamic" ErrorMessage="password required" Font-Bold="True" Font-Size="X-Large" ValidationGroup="signup"></asp:RequiredFieldValidator>
                <asp:TextBox ID="emailTB" class="textFields" placeholder="email address" runat="server" TextMode="Email"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="createBTN" runat="server" CssClass="Buttons" Text="Create" OnClick="createBTN_Click" ValidationGroup="signup" />
            </div>
        </div>
    </form>
</body>
</html>
