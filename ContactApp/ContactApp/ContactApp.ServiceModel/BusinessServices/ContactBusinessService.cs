using ContactApp.ServiceModel.Data.Models;
using ContactApp.ServiceModel.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactApp.ServiceModel.BusinessServices
{
    //Service tier: Contact business service
    public class ContactBusinessService : IContactBusinessService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Contact> _contactRepo;
        private readonly IRepository<PhoneNumber> _phoneNumberRepo;

        public ContactBusinessService(IUnitOfWork uow, IRepository<Contact> contactRepo, IRepository<PhoneNumber> PhoneNumberRepo)
        {
            _uow = uow;
            _contactRepo = contactRepo;
            _phoneNumberRepo = PhoneNumberRepo;
        }

        public async Task<long> GetTotalContact()
        {
            
            var totalRecord = await _contactRepo.CountAsync();
            return totalRecord;
        }

        public async Task<List<Contact>> GetAllContact()
        {
            var data = await _contactRepo.GetAllAsync();
            return data.ToList();
        }

        public async Task<List<Contact>> GetContactWithPagin(int skip, int take)
        {
            var data = await _contactRepo.GetAllAsync(skip, take);
            return data;
        }

        public async Task<ResponseResult> AddContact(CreateContact contact)
        {
            var checkExistedName = await _contactRepo.ExistsAsync(x => x.Name == contact.Name);
            if (checkExistedName == true)
            {
                return new ResponseResult { Status = "Existed" };
            }
            var newContact = new Contact { Name = contact.Name, Address = contact.Address };

            await _contactRepo.InsertAsync(newContact);
            var result = _uow.Commit();
            return new ResponseResult { Status = result.ToString(), Contact = newContact };
        }

        public async Task<ResponseResult> DeleteContact(int id)
        {
            var contactDelete = await _contactRepo.ExistsAsync(x => x.ID == id);
            if (contactDelete == false)
            {
                return new ResponseResult { Status = "NotExisted" };
            }

            var listPhoneNumberOfContact = (await _phoneNumberRepo.GetAsync(x => x.ContactId == id)).ToList();

            Task taskDeleteListPhoneNumber = DeleteListPhoneNumber(listPhoneNumberOfContact);
            Task taskDeleteContact = DeleteContactByID(id);

            await Task.WhenAll(taskDeleteListPhoneNumber, taskDeleteContact);

            var result = _uow.Commit();
            return new ResponseResult { Status = result.ToString() };
        }

        public async Task<ResponseResult> UpdateContact(UpdateContact contact)
        {
            var contactUpdate = (await _contactRepo.GetAsync(x => x.ID == contact.ID)).FirstOrDefault();
            if (contactUpdate == null)
            {
                return new ResponseResult { Status = "NotExisted" };
            }

            var checkExistedAnotherContact = await _contactRepo.ExistsAsync(x => x.ID != contact.ID && x.Name == contact.Name);
            if (checkExistedAnotherContact)
            {
                return new ResponseResult { Status = "Existed" };
            }

            contactUpdate.Name = contact.Name;
            contactUpdate.Address = contact.Address;
            await _contactRepo.UpdateAsync(contactUpdate);

            var result = _uow.Commit();
            return new ResponseResult { Status = result.ToString() };
        }

        private async Task<int> DeleteContactByID(int id)
        {
            return await _contactRepo.DeleteAsync(x => x.ID == id);
        }

        private async Task DeleteListPhoneNumber(List<PhoneNumber> listPhoneNumber)
        {
            foreach (var item in listPhoneNumber)
            {
                await _phoneNumberRepo.DeleteAsync(x => x.ID == item.ID);
            }
        }
    }

}
