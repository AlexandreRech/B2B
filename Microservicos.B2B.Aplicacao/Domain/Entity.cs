using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservicos.B2B.Domain
{
    public class Entity
    {        
        public virtual Guid Id { get; set; }
        
        public bool IsTransient()
        {
            return Id == new Guid();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity)) return false;

            if (ReferenceEquals(this, obj)) return true;

            var item = (Entity)obj;

            if (item.IsTransient() || IsTransient()) return false;

            return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (IsTransient()) return base.GetHashCode();

            return Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
        }


    }
}
