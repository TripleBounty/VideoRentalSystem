﻿using VideoRentalSystem.Common.Contracts;

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