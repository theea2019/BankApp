using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Commons.Abstractions
{
    /// <summary>
    ///     <english>
    ///         This Interface helps handling the different type of Loggers in a common interface.
    ///     </english>
    ///     <turkish>
    ///         Bu arayüz farklı tipteki Loglama kaynaklarını ortak bir arayüz üzerinden yönetmemizi sağlar. Liskov yerine geçme kuralı ile işlerimizi kolaylaştırır.
    ///     </turkish>
    /// </summary>
    public abstract class LogBase
    {
        protected readonly object lockObj = new object();
        public abstract void Log(string message, bool isError);
    }
}
