using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain._Base
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
