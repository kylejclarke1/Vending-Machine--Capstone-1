using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachineItem
    {
        public VendingMachineItem(string slotID, string name, string price)
        {
            SlotID = slotID;
            Name = name;
            Price = decimal.Parse(price);
            Quantity = 5;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string SlotID { get; set; }
        public int Quantity { get; set; }
        public string PrintMessage
        {
            get
            {
                if (SlotID.StartsWith("A"))
                {
                    return "Crunch Crunch, Yum!";
                }
                else if (SlotID.StartsWith("B"))
                {
                    return "Munch Munch, Yum!";
                }
                else if (SlotID.StartsWith("C"))
                {
                    return "Glug Glug, Yum!";
                }
                else if (SlotID.StartsWith("D"))
                {
                    return "Chew Chew, Yum!";
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
