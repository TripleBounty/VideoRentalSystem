using System;
using System.Collections.Generic;

namespace VideoRentalSystem
{
    public  class Organisation
    {
        public Organisation()
        {
            this.Award = new HashSet<Award>();
            this.IsDeleted = false;
        }

        public Organisation(string name)
        {
            this.Name = name;
            this.Award = new HashSet<Award>();
            this.IsDeleted = false;
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Award> Award { get; set; }
    }
}
