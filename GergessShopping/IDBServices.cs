using System;
using GergessShoppingWeb;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GergessShoppingWeb
{
   
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDBServices" in both code and config file together.
    [ServiceContract]
    public interface IDBServices
    {
        [OperationContract]
        User GetUserLogin(string email, string pass);
        [OperationContract]
        User GetUserID(string ID);
        [OperationContract]
        int InsertUser(User user);
        [OperationContract]
        int InsertOrder(Order order);
        [OperationContract]
        List<Product> GetProducts(List<Product> product);
        [OperationContract]
        int CreateAnOrder(List<Item> item, string userID, Card card, double price);
        [OperationContract]
        List<Order> GetOrders(string ID);
        [OperationContract]
        List<Item> GetOrderItems(string orderID);
        [OperationContract]
        int deleteOrder(string orderID);
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string password { get; set; }

    }
    [DataContract]
    public class Product
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string desc { get; set; }
        [DataMember]
        public double price { get; set; }
        [DataMember]
        public string imageURL { get; set; }
    }
    [DataContract]
    public class Item
    {
        [DataMember]
        public string orderID { get; set; }
        [DataMember]
        public string productID { get; set; }
        [DataMember]
        public int qty { get; set; }
    }

    [DataContract]
    public class Order
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string userID { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string orderDate { get; set; }
        [DataMember]
        public Card card { get; set; }
        [DataMember]
        public double amountPaid { get; set; }
    }
    [DataContract]
    public class Card {
        [DataMember]
        public string cardType { get; set; }
        [DataMember]
        public float cardNum { get; set; }
        [DataMember]
        public string cardName { get; set; }
    }
}
