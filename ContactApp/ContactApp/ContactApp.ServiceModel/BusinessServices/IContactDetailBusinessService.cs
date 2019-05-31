using ContactApp.ServiceModel.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.ServiceModel.BusinessServices
{
    public interface IContactDetailBusinessService
    {
        Task<List<PhoneNumber>> GetAllPhoneNumberByContactID(int contactID);
        Task<ResponseResult> AddPhoneNumbeForContact(CreatePhoneNumber phoneNumber);
        Task<ResponseResult> UpdatePhoneNumberForContact(UpdatePhoneNumber phoneNumber);
        Task<ResponseResult> DeletePhoneNumberByID(int id);
    }
}
