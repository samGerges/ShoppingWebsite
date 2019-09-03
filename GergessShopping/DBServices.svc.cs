using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GergessShoppingWeb;

namespace GergessShoppingWeb
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DBServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DBServices.svc or DBServices.svc.cs at the Solution Explorer and start debugging.
    public class DBServices : IDBServices
    {
        SqlCommand command;
        SqlConnection conn;
        SqlDataReader reader;
        string querystring;

        List<Item> cart = new List<Item>();

        public DBServices()
        {
            //constructor to initiate the SQL connection
            this.conn = initConnection("SAM-XPS15\\SQLEXPRESS","ProjectDB");
        }
        //function resposible for initiating the connection 
        public SqlConnection initConnection(string serverName, string schema)
        {
            SqlConnection conn = null;

            string connectionString = "Data Source="+ serverName +"; " +
               "Initial Catalog="+schema+"; Integrated Security=SSPI; " +
               "Persist Security Info=false";

            conn = new SqlConnection(connectionString);

            return conn;
        }

        //get the user information from credintials and this is used for loging in
        public User GetUserLogin(string email, String pass)
        {
           
            User user = null;

            conn.Open();
            querystring = "SELECT * FROM USERS";
            command = new SqlCommand(querystring, conn);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader["email"].ToString() == email)
                {
                    if (reader["password"].ToString() == pass)
                    {
                        user = new User { ID = reader["ID"].ToString(), firstName = reader["first_name"].ToString(), lastName = reader["last_name"].ToString(), email = reader["email"].ToString(), password = "" };
                    }
                }
            }
            conn.Close();
            //If user is null then credintials are not good
            return user;
        }
        //get the user info using id instead of credintials 
        public User GetUserID(string ID)
        {

            User user = null;

            conn.Open();
            querystring = "SELECT * FROM USERS";
            command = new SqlCommand(querystring, conn);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader["ID"].ToString() == ID)
                {
                    user = new User { ID = reader["ID"].ToString(), firstName = reader["first_name"].ToString(), lastName = reader["last_name"].ToString(), email = reader["email"].ToString(), password = "" };
                }
            }
            conn.Close();

            return user;
        }
        //inserting the user to the user database and this is used for signup
        public int InsertUser(User user)
        {
            bool emailExists = false;
            int rows = 0;

            conn.Open();
            querystring = "SELECT ID, email FROM Users";
            command = new SqlCommand(querystring, conn);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader["email"].ToString() == user.email)
                {
                    emailExists = true;
                    return 3;
                }
            }
            if (!emailExists)
            {
                while (reader.Read())
                {
                    do
                    {
                        user.ID = IDGenerator(user.firstName + " " + user.lastName);

                    } while (user.ID == reader["ID"].ToString());
                }

                conn.Close();
                conn.Open();
                querystring = "INSERT INTO Users VALUES(@ID,@first_name,@last_name,@email,@password)";
                command = new SqlCommand(querystring, conn);
                command.Parameters.AddWithValue("@ID", user.ID);
                command.Parameters.AddWithValue("@first_name", user.firstName);
                command.Parameters.AddWithValue("@last_name", user.lastName);
                command.Parameters.AddWithValue("@email", user.email);
                command.Parameters.AddWithValue("@password", user.password);
                rows = command.ExecuteNonQuery();
                conn.Close();
            }
            return rows;
        }
        //inserting order to the orders table
        public int InsertOrder(Order order)
        {
            int rows = 0;
            try
            {
                conn.Open();
                querystring = "INSERT INTO Orders VALUES(@id, @user_id, @status, @date_of_order, @cardType, @cardNum, @cardName, @amountPaid)";
                command = new SqlCommand(querystring, conn);
                command.Parameters.AddWithValue("@id", order.ID);
                command.Parameters.AddWithValue("@user_id", order.userID);
                command.Parameters.AddWithValue("@status", order.status);
                command.Parameters.AddWithValue("@date_of_order", order.orderDate);
                command.Parameters.AddWithValue("@cardType", order.card.cardType);
                command.Parameters.AddWithValue("@cardNum", order.card.cardNum);
                command.Parameters.AddWithValue("@cardName", order.card.cardName);
                command.Parameters.AddWithValue("@amountPaid", order.amountPaid);
                rows = command.ExecuteNonQuery();
                conn.Close();

                return 0;
            } catch
            {
                return 1;
            }
        }
        // this function is responsible for inserting the items and quantatity to the orderitems table and calling the insert order function
        public int CreateAnOrder(List<Item> cartItems, string userID, Card card, double price)
        {
            string orderID = "", orderDate = "";

            conn.Open();
            querystring = "SELECT ID FROM Orders";
            command = new SqlCommand(querystring, conn);
            reader = command.ExecuteReader();
            if (!reader.Read())
            {
                while (reader.Read())
                {
                    do
                    {
                        orderID = IDGenerator("or");

                    } while (orderID == reader["ID"].ToString() || orderID == "");
                }
            }
            else { orderID = IDGenerator("or"); }
            conn.Close();
            orderDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");

            Order order = new Order { ID = orderID, userID = userID, orderDate = orderDate, status = "In Process", card = card, amountPaid = price };

            int r = InsertOrder(order);

            try
            {
                conn.Open();
                querystring = "INSERT INTO OrderItems VALUES(@orderID, @productID, @qty)";
                foreach (Item item in cartItems)
                {
                    command = new SqlCommand(querystring, conn);
                    command.Parameters.AddWithValue("@orderID", order.ID);
                    command.Parameters.AddWithValue("@productID", item.productID);
                    command.Parameters.AddWithValue("@qty", item.qty);
                    command.ExecuteNonQuery();

                }
                conn.Close();
            }
            catch
            {
                return 1;
            }

            return 0;
        }
        // getting the products from the Database
        public List<Product> GetProducts(List<Product> products)
        {
            conn.Open();
            querystring = "SELECT * FROM Products";
            command = new SqlCommand(querystring, conn);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product { ID = reader["ID"].ToString(), desc = reader["description"].ToString(), name = reader["name"].ToString(), price = double.Parse(reader["price"].ToString()), imageURL = "/images/"+reader["imageURL"].ToString()});
            }
            conn.Close();
            return products;
        }
       
        //get the orders using user id
        public List<Order> GetOrders(string ID)
        {
            List<Order> orders = new List<Order>();
            Card card;
            conn.Open();
            querystring = "SELECT * FROM Orders";
            command = new SqlCommand(querystring, conn);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                card = new Card { cardName = reader["cardType"].ToString(), cardNum = float.Parse(reader["cardNum"].ToString()), cardType = reader["cardType"].ToString() };
                orders.Add(new Order { ID = reader["ID"].ToString(), orderDate = reader["date_of_order"].ToString(), userID = reader["user_ID"].ToString(), status = reader["status"].ToString(), card = card, amountPaid=double.Parse(reader["amount_paid"].ToString()) });
            }
            conn.Close();
            return orders;
        }
        //get the orderitems of an order using order id
        public List<Item> GetOrderItems(string orderID)
        {
            List<Item> items = new List<Item>();
            conn.Open();
            querystring = "SELECT * FROM OrderItems";
            command = new SqlCommand(querystring, conn);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new Item {orderID= reader["order_id"].ToString(), productID= reader["product_id"].ToString(), qty= int.Parse(reader["quantity"].ToString())});
            }
            conn.Close();
            return items;
        }
        //deleting an order when the user cancels the order
        public int deleteOrder(string orderID)
        {
            try {
                conn.Open();
                command = new SqlCommand("Delete from OrderItems Where order_id = @id", conn);
                command.Parameters.AddWithValue("@id", orderID);

                command.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                command = new SqlCommand("Delete from Orders Where ID = @id", conn);
                command.Parameters.AddWithValue("@id", orderID);

                command.ExecuteNonQuery();
                conn.Close();
                return 0;
            } catch {
                return 0;
            }
        }
        //generates a random ID
        string IDGenerator(string name)
        {
            Random random = new Random();
            int rand = random.Next(1000);
            string id = name.Replace(" ", "") + rand.ToString();
            return id;
        }
    }
}

