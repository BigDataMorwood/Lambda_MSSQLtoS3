using System;
using System.Collections.Generic;
using System.Text;

namespace Lambda_MSSQLtoS3.Models
{
    class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth{ get; set; }
    }
}