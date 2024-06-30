using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order
{
    internal class Subtotal
    {
        public String Name;
        public int price;
        public int count;
        public int total;


        public Subtotal(string name, int price, int count, int total)
        {
            this.Name = name;
            this.price = price;
            this.count = count;
            this.total = total;
        }



    }
}
