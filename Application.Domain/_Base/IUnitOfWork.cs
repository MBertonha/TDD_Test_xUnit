using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain._Base
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
