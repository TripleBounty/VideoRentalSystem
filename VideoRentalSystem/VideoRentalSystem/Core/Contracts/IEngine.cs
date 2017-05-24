using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentalSystem.Common.Contracts;

namespace VideoRentalSystem.Core.Contracts
{
    public interface IEngine
    {
        IReader Reader { get; set; }

        IWriter Writer { get; set; }

        IProcessor Processor { get; set; }

        void Start();
    }
}
