using ServiceStack;

namespace ContactApp.ServiceModel
{

    [Route("/contactsDetail/PhoneNumber/{ContactID}", "GET")]
    public class GetPhoneNumberByContactID
    {
        public int ContactID { get; set; }
    }

    [Route("/contactsDetail/PhoneNumber", "POST")]
    public class CreatePhoneNumber
    {
        public string Number { get; set; }
        public string Type { get; set; }
        public int ContactID { get; set; }
    }

    [Route("/contactsDetail/PhoneNumber/{ID}", "PUT")]
    public class UpdatePhoneNumber
    {
        public int ID { get; set; }
        public string Number { get; set; }

        public string Type { get; set; }
    }

    [Route("/contactsDetail/PhoneNumber/{ID}", "DELETE")]
    public class DeletePhoneNumber
    {
        public int ID { get; set; }
    }
}
