using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactApp.ServiceModel.Data.Models;
using ContactApp.ServiceModel.Data.Repositories;

namespace ContactApp.ServiceModel.BusinessServices
{
    public class ContactDetailBusinessService : IContactDetailBusinessService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Contact> _contactRepo;
        private readonly IRepository<PhoneNumber> _phoneNumberRepo;

        public ContactDetailBusinessService(IUnitOfWork uow, IRepository<Contact> contactRepo, IRepository<PhoneNumber> PhoneNumberRepo)
        {
            _uow = uow;
            _contactRepo = contactRepo;
            _phoneNumberRepo = PhoneNumberRepo;
        }

        public async Task<List<PhoneNumber>> GetAllPhoneNumberByContactID(int contactID)
        {
            var listPhoneNumber = await _phoneNumberRepo.GetAsync(x => x.ContactId == contactID);
            return (listPhoneNumber == null) ? new List<PhoneNumber>() : listPhoneNumber.ToList();
        }

        public async Task<ResponseResult> AddPhoneNumbeForContact(CreatePhoneNumber phoneNumber)
        {
            var checkExistedPhoneNumber = await _phoneNumberRepo.ExistsAsync(x => x.ContactId == phoneNumber.ContactID && x.Number == phoneNumber.Number);
            if (checkExistedPhoneNumber == true)
            {
                return new ResponseResult { Status = "Existed" };
            }
            var newPhoneNumber = new PhoneNumber(phoneNumber.Number, phoneNumber.Type, phoneNumber.ContactID);

            await _phoneNumberRepo.InsertAsync(newPhoneNumber);
            var result = _uow.Commit();

            return new ResponseResult { Status = result.ToString() };
        }

        public async Task<ResponseResult> DeletePhoneNumberByID(int id)
        {
            var existsPhoneNumber = await _phoneNumberRepo.ExistsAsync(x => x.ID == id);
            if (existsPhoneNumber == false)
            {
                return new ResponseResult { Status = "NotExisted" };
            }
            await _phoneNumberRepo.DeleteAsync(x => x.ID == id);

            var result = _uow.Commit();
            return new ResponseResult { Status = result.ToString() };
        }

        public async Task<ResponseResult> UpdatePhoneNumberForContact(UpdatePhoneNumber phoneNumber)
        {
            var phoneNumberUpdate = (await _phoneNumberRepo.GetAsync(x => x.ID == phoneNumber.ID)).FirstOrDefault();

            if (phoneNumberUpdate == null)
            {
                return new ResponseResult { Status = "NotExisted" };
            }

            phoneNumberUpdate.Number = phoneNumber.Number;
            phoneNumberUpdate.Type = phoneNumber.Type;
            await _phoneNumberRepo.UpdateAsync(phoneNumberUpdate);

            var result = _uow.Commit();

            return new ResponseResult { Status = result.ToString() };

        }
    }
}
