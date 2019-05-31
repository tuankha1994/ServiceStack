using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactApp.ServiceModel.Data.Models
{
    public class Contact
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
