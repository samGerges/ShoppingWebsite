using GergessShoppingWeb.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GergessShoppingWeb
{
    public partial class Profile : System.Web.UI.Page
    {
        List<Order> orders = new List<Order>();
        List<Product> products = new List<Product>();
        List<Item> orderItems = new List<Item>();
        Literal ltr = new Literal();
        ServiceReference1.DBServicesClient dbservices = new ServiceReference1.DBServicesClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"] == null)
            {
                Session["redirectBack"] = "Profile.aspx";
                Response.Redirect("Home.aspx");
            }
            User user = dbservices.GetUserID(Session["ID"].ToString());
            firstNameLabel.Text = user.firstName;
            lastNameLabel.Text = user.lastName;
            emailLabel.Text = user.email;

            if (Session["alertMessage"] != null)
            {
                ltr.Text = @"<script type='text/javascript'> Swal.fire('" + Session["alertMessage"].ToString() + "') </script>";
                Session.Remove("alertMessage");
            }
            orders = dbservices.GetOrders(Session["ID"].ToString());
            products = dbservices.GetProducts(products);

            Page.Controls.Add(ltr);
            TableRow row = new TableRow();
            TableCell cell = new TableCell();

            Table1.CellSpacing = 10;
            Table1.BorderStyle = BorderStyle.None;

            cell = new TableCell();
            cell.Controls.Add(new LiteralControl("Order ID"));
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Controls.Add(new LiteralControl("Items"));
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Controls.Add(new LiteralControl("Total Paid"));
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Controls.Add(new LiteralControl(" "));
            row.Cells.Add(cell);
            Table1.Rows.Add(row);

            row = new TableRow();
            /*
             * in the next block of code, I'm looping through orders and then with 
             * the order id I'm getting the items associated with it from Order items table
             * then prints the output to the table
             */
            if (products != null && orders != null)
            {
                foreach (Order ord in orders)
                {
                    if (ord.userID == user.ID)
                    {
                        orderItems = dbservices.GetOrderItems(ord.ID);
                        string items = "";
                        foreach (Item item in orderItems)
                        {
                            foreach (Product prod in products)
                            {
                                if (prod.ID == item.productID && item.orderID == ord.ID)
                                {
                                    items += prod.name + "(quantity: " + item.qty + ")";
                                }
                            }
                        }
                        foreach (Item item in orderItems)
                        {
                            if (item.orderID == ord.ID)
                            {
                                row = new TableRow();
                                for (int i = 0; i < 4; i++)
                                {
                                    if (i == 0)
                                    {
                                        Label lb = new Label();
                                        lb.Text = ord.ID;
                                        cell = new TableCell();
                                        cell.Controls.Add(lb);
                                        row.Cells.Add(cell);
                                    }
                                    else if (i == 1)
                                    {
                                        Label lb = new Label();
                                        lb.Text = items;
                                        cell = new TableCell();
                                        cell.Controls.Add(lb);
                                        row.Cells.Add(cell);
                                    }
                                    else if (i == 2)
                                    {
                                        cell = new TableCell();
                                        cell.Controls.Add(new LiteralControl("$" + ord.amountPaid.ToString()));
                                        row.Cells.Add(cell);
                                    }
                                    else if (i == 3)
                                    {
                                        cell = new TableCell();
                                        if (ord.status.ToUpper() == "In Process".ToUpper())
                                        {
                                            Button b = new Button();
                                            b.Text = "Cancel";
                                            b.CssClass = "Buttons";
                                            b.CommandName = ord.ID;
                                            b.Command += new CommandEventHandler(Cancel_Click);

                                            cell.Controls.Add(b);
                                        }
                                        else
                                        {
                                            cell.Controls.Add(new LiteralControl("Order Already Proccessed"));
                                        }
                                        row.Cells.Add(cell);
                                    }
                                }
                            }
                        }
                        Table1.Rows.Add(row);

                    }
                }
            }

        }
        protected void Cancel_Click(object sender, CommandEventArgs e)
        {
           int outnum =  dbservices.deleteOrder(e.CommandName);
            if (outnum == 1)
            {
                Session["alertMessage"] = "Delete Failed";

            }
            else if(outnum == 0){
                Session["alertMessage"] = "Delete Successful";
            }
            Response.Redirect("Profile.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShoppingPage.aspx");
        }
        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            Session.Clear();
            Response.Redirect("Home.aspx");
        }
    }
}
        