using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.ServiceModel.Data.Models
{
    public class PhoneNumber
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public int ContactId { get; set; }

        public PhoneNumber(string number, string type, int contactID)
        {
            Number = number;
            Type = type;
            ContactId = contactID;
        }
    }
}
