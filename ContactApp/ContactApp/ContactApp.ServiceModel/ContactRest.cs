using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactApp.ServiceModel.Data.Models;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace ContactApp.ServiceModel
{

    [Route("/contacts/totalRecord", "GET")]
    public class GetTotalRecordContacts { }

    public class GetTotalResponse
    {
        public long TotalRecord { get; set; }
    }


    [Route("/contacts", "GET")]
    public class GetContacts { }

    [Route("/contacts/{Name}", "GET")]
    public class GetContact
    {
        public string Name { get; set; }
    }

    [Route("/contacts/{Skip}/{Take}", "GET")]
    public class GetContactPaging
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    [Route("/contacts", "POST")]
    public class CreateContact
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    [Route("/contacts/{ID}", "PUT")]
    public class UpdateContact
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }
    }

    [Route("/contacts/{ID}", "DELETE")]
    public class DeleteContact
    {
        public int ID { get; set; }
    }

    public class GetContactsResponse : ResponseResult
    {
        
    }

    public class ResponseResult
    {
        public Contact Contact { get; set; }
        public string Status { get; set; }
    }
}
