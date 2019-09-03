using GergessShoppingWeb.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GergessShoppingWeb
{
    public partial class CartCheckout : System.Web.UI.Page
    {
        List<Item> cart = new List<Item>();
        List<Product> products = new List<Product>();
        Literal ltr = new Literal();
        ServiceReference1.DBServicesClient dbservices = new ServiceReference1.DBServicesClient();
        double totalpostTax = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"] == null)
            {
                Session["redirectBack"] = "CartCheckout.aspx";
                Response.Redirect("Home.aspx");
                
            }
            if (Session["alertMessage"] != null)
            {
                ltr.Text = @"<script type='text/javascript'> Swal.fire('" + Session["alertMessage"].ToString() + "') </script>";
                Session.Remove("alertMessage");
            }
            cart = (List<Item>)Session["cart"];
            products = dbservices.GetProducts(products);

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

            row = new TableRow();
            double totalpre = 0;

            if (products != null && cart != null)
            {
                foreach (Product item in products)
                {
                    foreach (Item prod in cart)
                    {
                        if (prod.productID == item.ID)
                        {
                            totalpre += prod.qty * item.price;
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
                                    tb.ID = prod.productID;
                                    tb.TextMode = TextBoxMode.Number;
                                    tb.Attributes.CssStyle.Add("Width", "35px");
                                    tb.Attributes.Add("runat", "Server");
                                    tb.Text = prod.qty.ToString();
                                    Button b = new Button();
                                    b.Text = "Update";
                                    b.CommandName = prod.productID;
                                    b.Command += new CommandEventHandler(Update_Click);
                                    cell = new TableCell();
                                    cell.Controls.Add(tb);
                                    cell.Controls.Add(b);
                                    row.Cells.Add(cell);
                                }
                                else if (i == 4)
                                {
                                    Button b = new Button();
                                    b.Text = "Remove";
                                    b.CssClass = "Buttons";
                                    b.CommandName = item.ID;
                                    b.Command += new CommandEventHandler(Remove_Click);
                                    cell = new TableCell();
                                    cell.Controls.Add(b);
                                    row.Cells.Add(cell);
                                }
                            }
                            Table1.Rows.Add(row);

                        }
                        
                    }
                }
                row = new TableRow();
                for (int i = 0; i < 5; i++)
                {
                    if (i == 3)
                    {
                        Label lb = new Label();
                        lb.Text = "Total +tax: $" + totalpre.ToString();
                        cell = new TableCell();
                        cell.Controls.Add(lb);
                        row.Cells.Add(cell);
                    }
                    else if (i == 4)
                    {
                        totalpostTax = Math.Round((totalpre * 1.13), 2);
                        Label lb = new Label();
                        lb.Text = "Total with Tax: $" + totalpostTax.ToString();
                        cell = new TableCell();
                        cell.Controls.Add(lb);
                        row.Cells.Add(cell);
                    }
                    else
                    {
                        cell = new TableCell();
                        cell.Controls.Add(new LiteralControl(" "));
                        row.Cells.Add(cell);
                    }
                }
                Table1.Rows.Add(row);
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }
        protected void Update_Click(object sender, CommandEventArgs e)
        {
            string id = e.CommandName;
            int qty = 0;

            TextBox tb = form1.FindControl(id) as TextBox;
            qty = int.Parse(tb.Text);


            if (qty == 0)
            {
                foreach (Item item in cart)
                {
                    if (item.productID == e.CommandName)
                    {
                        cart.Remove(item);
                        break;
                    }
                }
                Session["cart"] = cart;
                Response.Redirect("CartCheckout.aspx");
            }
            else if (qty < 0)
            {
                ltr.Text = @"<script type='text/javascript'> Swal.fire('Quantity number can't be in negative') </script>";
                tb.Text = "0";
            }
            else
            {
                foreach (Item item in cart)
                {
                    if (id == item.productID)
                    {
                        item.qty = qty;
                    }
                }
                Session["cart"] = cart;
                Response.Redirect("CartCheckout.aspx");
            }
            
        }
        protected void Remove_Click(object sender, CommandEventArgs e)
        {
            foreach (Item item in cart) {
                if (item.productID == e.CommandName) {
                    cart.Remove(item);
                    break;
                }
            }
            Response.Redirect("CartCheckout.aspx");
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
        protected void checkout_Click(object sender, EventArgs e)
        {
            if (cart != null)
            {
                if (IsValid)
                {
                    string name = TextBox1.Text;
                    string cardType = DropDownList1.SelectedValue;
                    float cardnum = float.Parse(TextBox2.Text);

                    Card card = new Card { cardName = name, cardType = cardType, cardNum = cardnum};

                    dbservices.CreateAnOrder(cart, Session["ID"].ToString(), card, totalpostTax);
                    cart.Clear();
                    Session["cart"] = cart;

                    Session["alertMessage"] = "Order is Confirmed";

                    Response.Redirect("CartCheckout.aspx");
                }
            }
            else { ltr.Text = @"<script type='text/javascript'> Swal.fire('Cart is Empty') </script>"; }
        }
    }
}