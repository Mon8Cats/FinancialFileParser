using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace OrderFileParser.Models
{
    public class OrderDetail
    {
        public int LineNumber { get; private set; }
        public int Quantity { get; private set; }
        public double UnitPrice { get; private set; }
        public double TotalCost { get; private set; }
        public string Description { get; private set; } 

        public OrderDetail()
        {
            
        }

        public OrderDetail(int lineNumber, int quantity, double unitPrice, string description)
        {
            LineNumber = lineNumber;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalCost = UnitPrice * Quantity;
            Description = description;
            
        }

        public string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("300");
            sb.Append(LineNumber.ToString().PadLeft(2));
            sb.Append(Quantity.ToString().PadLeft(5));
            sb.Append(UnitPrice.ToString("0.00").PadLeft(10));
            sb.Append(TotalCost.ToString("0.00").PadLeft(10));
            sb.Append(Description);

            return sb.ToString();
        }
    }
}