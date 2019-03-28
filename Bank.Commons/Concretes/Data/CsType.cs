using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Commons.Concretes.Data
{
    /// <summary>
    ///     <english>
    ///         This enum type helps to convert C# data types to common Database Types.
    ///     </english>
    ///     <turkish>
    ///         Bu enumaration tipi C# veri tiplerini ortak Veritabanı tiplerine dönüştürme işlemlerinde kullanılmaktadır.
    ///     </turkish>
    /// </summary>
    public enum CsType
    {
        Binary,
        Boolean,
        Byte,
        ByteArray,
        Char,
        DateTime,
        Decimal,
        Double,
        Guid,
        Short,
        Int,
        Long,
        String,
        Null
    }
}
