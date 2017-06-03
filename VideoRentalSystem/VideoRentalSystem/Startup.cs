using Ninject;
using System;
using VideoRentalSystem.Builder;
using VideoRentalSystem.Core.Contracts;
using VideoRentalSystem.Data;
using VideoRentalSystem.Models;

namespace VideoRentalSystem
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new BuildManager());
            IEngine engine = kernel.Get<IEngine>();

            engine.Start();
        }
    }
}
