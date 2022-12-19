using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderFileParser.Models;

namespace OrderFileParser
{

    public class OrderParser
    {
        public List<Order> Orders { get; private set;}
        public List<Order> ParseOrders(string filePath)
        {
            Orders = new List<Order>();

            Order tmpOrder = null;
  
            foreach (string line in System.IO.File.ReadLines(filePath))
            {  
                string typeIdentifyer = line.Substring(0,3);

                switch (typeIdentifyer)
                {
                    case "100":
                        tmpOrder = ParseOrderHeader(line); 
                        Orders.Add(tmpOrder);  
                        break;

                    case "200":
                        if (tmpOrder != null)
                        {
                            tmpOrder.Address = ParseAddress(line);
                        } 
                        else 
                        {
                            throw new Exception("no order for the address");
                        }
                        break;

                    case "300":
                        if(tmpOrder != null)
                        {
                            tmpOrder.OrderDetails.Add(ParseOrderDetail(line));
                        } 
                        else 
                        {
                            throw new Exception("no order for the order detail");
                        }
                        break;

                    default:
                        throw new Exception("Incoreect type identifier");
                        break;
                }
            }  

            foreach(var order in Orders)
            {
                if(!order.Validate())
                {
                    throw new Exception("Invalid Orders with incorrect total price or quantity");
                }
            }
            
            return Orders;
        }

        public Order ParseOrderHeader(string orderfile)
        {
            try {
                var orderNumber = long.Parse(orderfile.Substring(3, 10));
                var totalItems = int.Parse(orderfile.Substring(13, 5));
                var totalCost = double.Parse(orderfile.Substring(18,10));
                var orderDate = DateTime.Parse(orderfile.Substring(28,19));
                var customerName = orderfile.Substring(47, 50);
                var customerPhone = orderfile.Substring(97, 30);
                var customerEmail = orderfile.Substring(127, 50);
                var isPaid = Convert.ToBoolean(int.Parse(orderfile.Substring(177,1)));
                var isShipped = Convert.ToBoolean(int.Parse(orderfile.Substring(178,1)));
                var isCompleted = Convert.ToBoolean(int.Parse(orderfile.Substring(179,1)));

                return new Order(orderNumber,totalItems, totalCost, orderDate
                , customerName, customerPhone, customerEmail, isPaid, isShipped,isCompleted);

            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Address ParseAddress(string addressfile)
        {
            try{
                var addressLine1 = addressfile.Substring(3,50).Trim();
                var addressLine2 = addressfile.Substring(53, 50).Trim();;
                var city = addressfile.Substring(103,50).Trim();
                var state = addressfile.Substring(153,2).Trim();;
                var zip = addressfile.Substring(155).Trim();
                return new Address(addressLine1, addressLine2, city, state, zip);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public OrderDetail ParseOrderDetail(string orderDetailfile)
        {
            try{
                var lineNumber = int.Parse(orderDetailfile.Substring(3,2));
                var quantity = int.Parse(orderDetailfile.Substring(5,5));
                var unitPrice = double.Parse(orderDetailfile.Substring(10,10));
                var totalCost = double.Parse(orderDetailfile.Substring(20,10));

                if( Convert.ToInt64(quantity*unitPrice*100) != Convert.ToInt64(totalCost*100))
                {
                    throw new Exception($"Incorrect total price in Order Line: {orderDetailfile}");
                }
                var description = orderDetailfile.Substring(30);
                return new OrderDetail(lineNumber, quantity, unitPrice, description);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void WriteToFile(string path)
        {
            using(StreamWriter sw = File.CreateText(path))
            {
                foreach(var order in Orders)
                {
                    sw.WriteLine(order.OrderHeaderToString());
                    sw.WriteLine(order.Address.ToString());
                    
                    foreach(var orderDetail in order.OrderDetails)
                    {
                        sw.WriteLine(orderDetail.ToString());
                    }
                }
            }
        }
    }
}