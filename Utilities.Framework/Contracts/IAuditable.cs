using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Framework.Contracts
{
    public interface IAuditable<TUserId>
        where TUserId : struct, IComparable, IComparable<TUserId>
    {

        TUserId CreatedBy { get; }
        DateTime CreatedDate { get; }
        TUserId LastModifiedBy { get; }
        DateTime? LastModifiedDate { get; }
    }
}
