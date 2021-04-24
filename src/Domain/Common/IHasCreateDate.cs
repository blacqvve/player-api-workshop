using System;

namespace Domain.Common
{
    public interface IHasCreateDate
    {
         public DateTime CreateDate { get; set; }
    }
}