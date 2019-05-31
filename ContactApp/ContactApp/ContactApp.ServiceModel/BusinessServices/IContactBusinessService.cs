using ContactApp.ServiceModel.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.ServiceModel.BusinessServices
{
    public interface IContactBusinessService
    {
        Task<long> GetTotalContact();
        Task<List<Contact>> GetAllContact();
        Task<List<Contact>> GetContactWithPagin(int skip, int take);
        Task<ResponseResult> AddContact(CreateContact contact);
        Task<ResponseResult> UpdateContact(UpdateContact contact);
        Task<ResponseResult> DeleteContact(int id);
    }
}
