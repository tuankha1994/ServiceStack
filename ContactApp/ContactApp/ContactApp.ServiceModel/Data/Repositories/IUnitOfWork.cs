using System;
using System.Data;

namespace ContactApp.ServiceModel.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
