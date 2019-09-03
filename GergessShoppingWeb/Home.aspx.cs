using GergessShoppingWeb.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GergessShoppingWeb
{
    public partial class Home : System.Web.UI.Page
    {
        Literal ltr = new Literal();
        ServiceReference1.DBServicesClient dbservices = new DBServicesClient();
        ServiceReference1.User user = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Controls.Add(ltr);
        }
        protected void createBTN_Click(object sender, EventArgs e)
        {
            
            int rows = 0;
            string firstname, lastname, email, pass;
            

            if (Page.IsValid)
            {
                firstname = firstnameTB.Text;
                lastname = lastnameTB.Text;
                email = emailTB.Text;
                pass = passTB.Text;

                user = new User {ID = IDGenerator(firstname), firstName= firstname, lastName= lastname, email = email, password= pass };

                rows = dbservices.InsertUser(user);
                if (rows == 3)
                {
                    ltr.Text = @"<script type='text/javascript'> Swal.fire('Existing Customer with same Email') </script>";
                }
                else
                {
                    user = dbservices.GetUserLogin(user.email, user.password);
                    Session["firstN"] = user.firstName;
                    Session["lastN"] = user.lastName;
                    Session["email"] = user.email;
                    Session["ID"] = user.ID;
                    user.password = "";
                    Session["alertMessage"] = "Account Created";
                    Response.Redirect("ShoppingPage.aspx");
                }
            }
        }

        protected void loginBTN_Click(object sender, EventArgs e)
        {
           string email = LoginEmailTB.Text;
           string pass =  LoginPassTB.Text;

            user = dbservices.GetUserLogin(email, pass);
            if (user == null)
            {
                ltr.Text = @"<script type='text/javascript'> Swal.fire('Login Failed!', 'Check email and password') </script>";
            }
            else
            {
                Session["firstN"] = user.firstName;
                Session["lastN"] = user.lastName;
                Session["email"] = user.email;
                Session["ID"] = user.ID;
                if (Session["redirectBack"] != null)
                {
                    Response.Redirect(Session["redirectBack"].ToString());

                }
                Response.Redirect("ShoppingPage.aspx");
            }
        }
        string IDGenerator(string name)
        {
            Random random = new Random();
            int rand = random.Next(1000);
            string id = name.Replace(" ", "") + rand.ToString();
            return id;
        }
    }
}