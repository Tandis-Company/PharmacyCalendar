﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Framework.Contracts;
using Utilities.Framework.Guards;

namespace Utilities.Framework
{
    public abstract class AggregateRoot : IEntity<Guid>
    {
        public virtual Guid Id
        {
            get;
            protected set;
        }
    }
}
