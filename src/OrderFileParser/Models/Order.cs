using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace OrderFileParser.Models
{
    public class Order
    {
        public long OrderNumber { get; private set; }
        public int TotalItems { get; private set; }
        public double TotalCost { get; private set; }
        public DateTime OrderDate { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerPhone { get; private set; }
        public string CustomerEmail { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsShipped { get; private set; }
        public bool IsCompleted { get; private set; }

        public Address Address { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetail>();   
        }

        public Order(long orderNumber, int totalItems, double totalCost, DateTime orderDate, string customerName, string customerPhone, string customerEmail, bool isPaid, bool isShipped, bool isCompleted)
        {
            OrderDetails = new List<OrderDetail>(); 

            OrderNumber = orderNumber;
            TotalItems = totalItems;
            TotalCost = totalCost;
            OrderDate = orderDate;
            CustomerName = customerName;
            CustomerPhone = customerPhone;
            CustomerEmail = customerEmail;
            IsPaid = isPaid;
            IsShipped = isShipped;
            IsCompleted = isCompleted;

            
        }   

        public string OrderHeaderToString()
        {
            var sb = new StringBuilder();
            sb.Append("100");
            sb.Append(OrderNumber.ToString().PadLeft(10));
            sb.Append(TotalItems.ToString().PadLeft(5));
            sb.Append(TotalCost.ToString("0.00").PadLeft(10));
            sb.Append(OrderDate.ToString("MM/dd/yyyy HH:mm:ss").PadRight(19));
            sb.Append(CustomerName.PadRight(50));
            sb.Append(CustomerPhone.PadRight(30));
            sb.Append(CustomerEmail.PadRight(50));
            sb.Append(Convert.ToInt32(IsPaid).ToString());
            sb.Append(Convert.ToInt32(IsShipped).ToString());
            sb.Append(Convert.ToInt32(IsCompleted).ToString());
    
            return sb.ToString();
        }

        public bool Validate()
        {
            int totalItems = 0;
            double totalCost = 0.0;

            foreach(var item  in OrderDetails)
            {
                totalItems += item.Quantity;
                totalCost += item.TotalCost;
            }

            return (totalItems == TotalItems && Convert.ToInt64(totalCost*100) == Convert.ToInt64(TotalCost*100));
        }
    }
}