using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Entities.Base
{
    public abstract class EntityBase<T>
    {
        public virtual T Id { get; protected set; }
    }
}
