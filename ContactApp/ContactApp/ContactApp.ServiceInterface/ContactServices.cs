using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ContactApp.ServiceModel;
using ContactApp.ServiceModel.Data.Repositories;
using ContactApp.ServiceModel.Data.Models;
using ContactApp.ServiceModel.BusinessServices;
using System.Threading.Tasks;

namespace ContactApp.ServiceInterface
{
    public class ContactServices : Service
    {
        private readonly IContactBusinessService _iContactBusinessService;

        public ContactServices(IContactBusinessService iContactBusinessService)
        {
            _iContactBusinessService = iContactBusinessService;
        }

        public async Task<object> Get(GetTotalRecordContacts request)
        {
            var totalRecord = await _iContactBusinessService.GetTotalContact();
            return new GetTotalResponse { TotalRecord = totalRecord };
        }

        public async Task<object> Get(GetContactPaging request)
        {
            var data = await _iContactBusinessService.GetContactWithPagin(request.Skip, request.Take);
            return data;
        }

        public async Task<object> Post(CreateContact request)
        {
            var result = await _iContactBusinessService.AddContact(request);

            return new ResponseResult { Status = result.Status, Contact = result.Contact };
        }

        public async Task<object> Put(UpdateContact request)
        {
            var result = await _iContactBusinessService.UpdateContact(request);

            return new ResponseResult { Status = result.Status };
        }

        public async Task<object> Delete(DeleteContact request)
        {
            var result = await _iContactBusinessService.DeleteContact(request.ID);

            return new GetContactsResponse { Status = result.Status };
        }
    }
}