using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Application.Domain.Tests._Util
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ArgumentException exception, string mensagem)
        {
            if(exception.Message == mensagem)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(true, $"Esperava a mensagem: '{mensagem}'" );
            }
        }
    }
}
