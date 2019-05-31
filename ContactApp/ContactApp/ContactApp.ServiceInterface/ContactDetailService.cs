using ContactApp.ServiceModel;
using ContactApp.ServiceModel.BusinessServices;
using ServiceStack;
using System.Threading.Tasks;

namespace ContactApp.ServiceInterface
{
    public class ContactDetailService : Service
    {
        private readonly IContactDetailBusinessService _iContactDetailBusinessService;

        public ContactDetailService(IContactDetailBusinessService iContactDetailBusinessService)
        {
            _iContactDetailBusinessService = iContactDetailBusinessService;
        }
        public async Task<object> Get(GetPhoneNumberByContactID request)
        {
            var listPhoneNumber = await _iContactDetailBusinessService.GetAllPhoneNumberByContactID(request.ContactID);
            return listPhoneNumber;
        }

        public async Task<object> Post(CreatePhoneNumber request)
        {
            var result = await _iContactDetailBusinessService.AddPhoneNumbeForContact(request);
            return result;
        }

        public async Task<object> PUT(UpdatePhoneNumber request)
        {
            var result = await _iContactDetailBusinessService.UpdatePhoneNumberForContact(request);
            return result;
        }

        public async Task<object> Delete(DeletePhoneNumber request)
        {
            var result = await _iContactDetailBusinessService.DeletePhoneNumberByID(request.ID);
            return result;
        }
    }
}
