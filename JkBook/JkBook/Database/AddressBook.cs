using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Database
{
    public class AddressBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public int MobileNo { get; set; }

    }
}
