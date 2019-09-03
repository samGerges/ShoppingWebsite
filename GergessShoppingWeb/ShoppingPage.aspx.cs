using GergessShoppingWeb.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GergessShoppingWeb
{
    public partial class ShoppingPage : System.Web.UI.Page
    {
        ServiceReference1.DBServicesClient dbservices = new ServiceReference1.DBServicesClient();
        List<Item> cart;
        List<Product> products = new List<Product>();
        Literal ltr = new Literal();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"] == null) {
                Response.Redirect("Home.aspx");
            }

            if (Session["cart"] != null)
            {
                cart = (List<Item>)Session["cart"];
            }
            else
            {
                cart = new List<Item>();
            }

            if (Session["alertMessage"] != null)
            {
                ltr.Text = @"<script type='text/javascript'> Swal.fire('" + Session["alertMessage"].ToString() + "') </script>";
                Session.Remove("alertMessage");
            }

            Page.Controls.Add(ltr);
            TableRow row = new TableRow();
            TableCell cell = new TableCell();

            Table1.CellSpacing = 10;
            Table1.BorderStyle = BorderStyle.None;

            cell = new TableCell();
            cell.Controls.Add(new LiteralControl(""));
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Controls.Add(new LiteralControl("Description"));
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Controls.Add(new LiteralControl("Price"));
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Controls.Add(new LiteralControl("Quantity"));
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Controls.Add(new LiteralControl(" "));
            row.Cells.Add(cell);
            Table1.Rows.Add(row);

            products = dbservices.GetProducts(products);
            foreach (Product item in products)
            {
                row = new TableRow();
                for (int i = 0; i < 5; i++)
                {
                    if (i == 0)
                    {
                        Image img = new Image();
                        img.ImageUrl = "~/" + item.imageURL;
                        
                        img.Attributes.CssStyle.Add("Width", "70px");
                        img.Attributes.CssStyle.Add("Height", "70px");
                        cell = new TableCell();
                        cell.Controls.Add(img);
                        cell.Controls.Add(new LiteralControl("<br/>"));
                        cell.Controls.Add(new LiteralControl(item.name));
                        row.Cells.Add(cell);
                    }
                    else if (i == 1)
                    {
                        cell = new TableCell();
                        cell.Controls.Add(new LiteralControl(item.desc));
                        row.Cells.Add(cell);
                    }
                    else if (i == 2)
                    {
                        cell = new TableCell();
                        cell.Controls.Add(new LiteralControl("$" + item.price.ToString()));
                        row.Cells.Add(cell);
                    }
                    else if (i == 3)
                    {
                        TextBox tb = new TextBox();
                        tb.ID = item.ID;
                        tb.TextMode = TextBoxMode.Number;
                        tb.Attributes.CssStyle.Add("Width", "35px");
                        tb.Attributes.Add("runat", "Server");
                        tb.Text = "1";
                        cell = new TableCell();
                        cell.Controls.Add(tb);
                        row.Cells.Add(cell);
                    }
                    else if (i == 4)
                    {
                        Button b = new Button();
                        b.Text = "Add To Cart";
                        b.CommandName = item.ID;
                        b.Command += new CommandEventHandler(AddToCart_Click);
                        cell = new TableCell();
                        cell.Controls.Add(b);
                        row.Cells.Add(cell);
                    }
                }
                Table1.Rows.Add(row);
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Session["products"] = products;
            Response.Redirect("CartCheckout.aspx");
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }
        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            Session.Clear();
            Response.Redirect("Home.aspx");
        }

        protected void AddToCart_Click(object sender, CommandEventArgs e)
        {
            
            string id = e.CommandName;
            int qty = 0;
            bool exist = false;

            TextBox tb = form1.FindControl(id) as TextBox;
            qty = int.Parse(tb.Text);
            foreach (Item item in cart)
            {
                if (item.productID == e.CommandName)
                {
                    item.qty += qty;
                    exist = true;
                    ltr.Text = @"<script type='text/javascript'> Swal.fire('Item updated in cart') </script>";
                    break;
                }
            }
            if (!exist)
            {
                cart.Add(new Item { productID = id, qty = qty });
                ltr.Text = @"<script type='text/javascript'> Swal.fire('Item added to cart') </script>";
            }
            
            Session["cart"] = cart;

        }

    }
}