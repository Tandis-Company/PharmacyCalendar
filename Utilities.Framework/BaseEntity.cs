using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Framework.Contracts;
using Utilities.Framework.Guards;

namespace Utilities.Framework
{
    public class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
